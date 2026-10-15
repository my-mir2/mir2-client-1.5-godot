using System;
using System.Buffers;
using System.IO;
using System.IO.Pipelines;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Mir2.Server.Network;
using Mir2.Shared.Protocols.Modern;
using Xunit;

namespace Mir2.Tests;

/// <summary>
/// 现代热血传奇高吞吐紧凑二进制协议与 TCP-Pack 传输引擎全方位验证矩阵
/// 涵盖: Direct-Blit 零分配序列化、动态消特征混淆、防重放、粘包/半包重组与恶意探针拦截
/// </summary>
public class TestModernBinaryProtocolMatrix
{
    [Fact]
    public void Test_PacketHeader_DirectBlit_Roundtrip()
    {
        var original = new BinaryProtocol.PacketHeader
        {
            Magic = BinaryProtocol.DefaultMagic,
            MsgId = (ushort)BinaryProtocol.MsgType.IntentMove,
            PayloadLength = 12,
            Sequence = 1024
        };

        Span<byte> buffer = stackalloc byte[BinaryProtocol.HeaderSize];
        original.Write(buffer);

        var restored = BinaryProtocol.PacketHeader.Read(buffer);

        Assert.Equal(original.Magic, restored.Magic);
        Assert.Equal(original.MsgId, restored.MsgId);
        Assert.Equal(original.PayloadLength, restored.PayloadLength);
        Assert.Equal(original.Sequence, restored.Sequence);
    }

    [Fact]
    public void Test_PayloadStructs_ZeroAllocation_MemoryLayout()
    {
        // 验证结构体大小是否完全紧凑（仅数个字节，极度节省带宽与内存）
        Assert.Equal(6, Marshal.SizeOf<BinaryProtocol.MoveIntentPayload>());
        Assert.Equal(6, Marshal.SizeOf<BinaryProtocol.AttackIntentPayload>());
        Assert.Equal(9, Marshal.SizeOf<BinaryProtocol.SpellIntentPayload>());
        Assert.Equal(6, Marshal.SizeOf<BinaryProtocol.PositionSyncPayload>());
        Assert.Equal(8, Marshal.SizeOf<BinaryProtocol.HealthSyncPayload>());
        Assert.Equal(8, Marshal.SizeOf<BinaryProtocol.SplitItemIntentPayload>());

        // 验证 MoveIntentPayload 读写
        var move = new BinaryProtocol.MoveIntentPayload
        {
            TargetX = 330,
            TargetY = 270,
            Direction = 3,
            IsRun = 1 // 跑步
        };
        Span<byte> moveBytes = stackalloc byte[Marshal.SizeOf<BinaryProtocol.MoveIntentPayload>()];
        move.Write(moveBytes);

        var restoredMove = BinaryProtocol.MoveIntentPayload.Read(moveBytes);
        Assert.Equal(330, restoredMove.TargetX);
        Assert.Equal(270, restoredMove.TargetY);
        Assert.Equal(3, restoredMove.Direction);
        Assert.Equal(1, restoredMove.IsRun);

        // 验证 SplitItemIntentPayload 读写
        var splitIntent = new BinaryProtocol.SplitItemIntentPayload
        {
            BagSlot = 3,
            SplitCount = 5
        };
        Span<byte> splitBuf = stackalloc byte[8];
        splitIntent.Write(splitBuf);
        var restoredSplit = BinaryProtocol.SplitItemIntentPayload.Read(splitBuf);
        Assert.Equal(3, restoredSplit.BagSlot);
        Assert.Equal(5, restoredSplit.SplitCount);
    }

    [Fact]
    public void Test_ServerAuthoritative_SplitItem_SuccessAndFail()
    {
        var dispatcher = new Mir2.Server.Game.PacketDispatcher(new Mir2.Server.Maps.MapManager());
        var session = new Mir2.Server.Network.ClientSession(1, null!)
        {
            State = new Mir2.Shared.Models.PlayerState
            {
                Hp = 100,
                MaxHp = 100
            }
        };

        // 添加一个可堆叠物品 (金创药 10 份)
        session.State.BagItems.Add(new Mir2.Shared.Models.EquipItem
        {
            Uid = 8888,
            Name = "金创药(小量)",
            Dura = 10,
            MaxDura = 10
        });

        // 构造 IntentSplitItem 封包: 从 slot 0 拆出 4 份
        var split = new BinaryProtocol.SplitItemIntentPayload { BagSlot = 0, SplitCount = 4 };
        byte[] payload = new byte[8];
        split.Write(payload);

        var header = new BinaryProtocol.PacketHeader
        {
            Magic = BinaryProtocol.DefaultMagic,
            MsgId = (ushort)BinaryProtocol.MsgType.IntentSplitItem,
            PayloadLength = (ushort)payload.Length,
            Sequence = 1
        };

        // 调度分发
        dispatcher.DispatchBinary(session, in header, payload);

        // 校验权威拆分结果
        Assert.Equal(2, session.State.BagItems.Count);
        Assert.Equal(6, session.State.BagItems[0].Dura); // 原槽位剩余 6
        Assert.Equal(4, session.State.BagItems[1].Dura); // 新槽位获得 4
        Assert.Equal("金创药(小量)", session.State.BagItems[1].Name);
    }

    [Fact]
    public void Test_PacketCipher_消特征与全对称还原()
    {
        uint sessionKey = 0x9E3779B9; // 黄金分割魔数种子
        var cipher = new PacketCipher(sessionKey);

        byte[] headerOriginal = new byte[] { 0x4D, 0x59, 0x01, 0x01, 0x0C, 0x00, 0x01, 0x00 };
        byte[] headerWork = (byte[])headerOriginal.Clone();

        // 混淆 Header
        cipher.ProcessHeader(headerWork);
        Assert.False(headerOriginal.AsSpan().SequenceEqual(headerWork)); // 已彻底抹除明文特征

        // 还原 Header
        cipher.ProcessHeader(headerWork);
        Assert.True(headerOriginal.AsSpan().SequenceEqual(headerWork)); // 精确还原

        // 混淆 Payload
        byte[] payloadOriginal = new byte[32];
        Random.Shared.NextBytes(payloadOriginal);
        byte[] payloadWork = (byte[])payloadOriginal.Clone();

        ushort seq = 42;
        cipher.ProcessPayload(payloadWork, seq);
        Assert.False(payloadOriginal.AsSpan().SequenceEqual(payloadWork));

        // 还原 Payload
        cipher.ProcessPayload(payloadWork, seq);
        Assert.True(payloadOriginal.AsSpan().SequenceEqual(payloadWork));
    }

    [Fact]
    public void Test_PacketCipher_动态滚动防重放特征()
    {
        uint sessionKey = 0x12345678;
        var cipher = new PacketCipher(sessionKey);

        byte[] samplePayload = System.Text.Encoding.UTF8.GetBytes("SameMoveIntentPayloadDataToTest");

        // 连续发送 5 包相同的数据，但 Sequence 自增
        byte[][] cipherTexts = new byte[5][];
        for (ushort seq = 1; seq <= 5; seq++)
        {
            cipherTexts[seq - 1] = (byte[])samplePayload.Clone();
            cipher.ProcessPayload(cipherTexts[seq - 1], seq);
        }

        // 验证每包在网络物理层捕获到的密文全部互不相同 (杜绝外挂抓包脱机分析)
        for (int i = 0; i < 5; i++)
        {
            for (int j = i + 1; j < 5; j++)
            {
                Assert.False(cipherTexts[i].AsSpan().SequenceEqual(cipherTexts[j]));
            }
        }
    }

    [Fact]
    public async Task Test_TcpPackEngine_粘包与半包重组解析()
    {
        var pipe = new Pipe();
        int receivedPackets = 0;
        uint sessionKey = 0xDEADBEEF;

        var engine = new TcpPackEngine((session, in header, payload) =>
        {
            receivedPackets++;
            Assert.Equal((ushort)BinaryProtocol.MsgType.IntentMove, header.MsgId);
            Assert.Equal(6, header.PayloadLength);
            Assert.Equal(receivedPackets, header.Sequence);

            var move = BinaryProtocol.MoveIntentPayload.Read(payload);
            Assert.Equal(300 + receivedPackets, move.TargetX);
        });

        var session = new PacketSession(1, null!, sessionKey);
        var cts = new CancellationTokenSource();

        // 启动后台管道消费任务
        var processTask = Task.Run(async () =>
        {
            try
            {
                await engine.ProcessReceivePipeAsync(session, pipe.Reader, cts.Token);
            }
            catch (OperationCanceledException) { }
        });

        // 构造 3 个带有消特征混淆的完整封包
        byte[][] packets = new byte[3][];
        for (ushort i = 1; i <= 3; i++)
        {
            var move = new BinaryProtocol.MoveIntentPayload
            {
                TargetX = (short)(300 + i),
                TargetY = 300,
                Direction = 1,
                IsRun = 0
            };

            int totalLen = BinaryProtocol.HeaderSize + 6;
            packets[i - 1] = new byte[totalLen];

            var header = new BinaryProtocol.PacketHeader
            {
                Magic = BinaryProtocol.DefaultMagic,
                MsgId = (ushort)BinaryProtocol.MsgType.IntentMove,
                PayloadLength = 6,
                Sequence = i
            };

            var span = packets[i - 1].AsSpan();
            header.Write(span[0..BinaryProtocol.HeaderSize]);
            move.Write(span[BinaryProtocol.HeaderSize..]);

            // 执行消特征加密
            session.Cipher!.ProcessHeader(span[0..BinaryProtocol.HeaderSize]);
            session.Cipher!.ProcessPayload(span[BinaryProtocol.HeaderSize..], i);
        }

        // 1. 模拟粘包: 将包 1 和包 2 粘在一起一次性写入 Pipe
        await pipe.Writer.WriteAsync(packets[0]);
        await pipe.Writer.WriteAsync(packets[1]);
        await pipe.Writer.FlushAsync();

        // 动态等待处理完成
        for (int w = 0; w < 50 && Volatile.Read(ref receivedPackets) < 2; w++)
        {
            await Task.Delay(10);
        }
        Assert.Equal(2, Volatile.Read(ref receivedPackets));

        // 2. 模拟半包: 将包 3 拆分为两截写入 (先写 5 字节，再写剩余字节)
        await pipe.Writer.WriteAsync(packets[2].AsMemory(0, 5));
        await pipe.Writer.FlushAsync();
        await Task.Delay(30);
        // 此时半包尚未凑齐，包数不应增加
        Assert.Equal(2, Volatile.Read(ref receivedPackets));

        // 写入包 3 剩余部分
        await pipe.Writer.WriteAsync(packets[2].AsMemory(5));
        await pipe.Writer.FlushAsync();
        for (int w = 0; w < 50 && Volatile.Read(ref receivedPackets) < 3; w++)
        {
            await Task.Delay(10);
        }

        // 此时包 3 成功重组并触发解析
        Assert.Equal(3, Volatile.Read(ref receivedPackets));

        cts.Cancel();
        pipe.Reader.CancelPendingRead();
        await processTask;
    }

    [Fact]
    public async Task Test_TcpPackEngine_非法魔数探针拦截阻断()
    {
        var pipe = new Pipe();
        var engine = new TcpPackEngine((session, in header, payload) => { });
        var session = new PacketSession(1, null!, 0); // 无混淆模式

        // 伪造非法网络扫描探测包 (如 HTTP GET 或 随机嗅探)
        byte[] fakeProbe = new byte[] { 0x47, 0x45, 0x54, 0x20, 0x2F, 0x20, 0x48, 0x54 }; // "GET / HT"
        await pipe.Writer.WriteAsync(fakeProbe);
        await pipe.Writer.FlushAsync();

        var cts = new CancellationTokenSource(500);

        // 验证引擎必须抛出 InvalidDataException 阻断连接
        await Assert.ThrowsAsync<InvalidDataException>(async () =>
        {
            await engine.ProcessReceivePipeAsync(session, pipe.Reader, cts.Token);
        });
    }

    [Fact]
    public void Test_AuthoritativeMovement_防穿墙防瞬移与拉回()
    {
        var mapManager = new Mir2.Server.Maps.MapManager();
        var dispatcher = new Mir2.Server.Game.PacketDispatcher(mapManager);

        using var dummySocket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
        var session = new ClientSession(101, dummySocket)
        {
            UseModernProtocol = true,
            X = 330,
            Y = 270,
            Direction = 1,
            CurrentMapId = 0
        };

        var map = mapManager.GetMap(0)!;
        map.Aoi.Enter(session, session.X, session.Y);

        // 1. 正常行走 1 格 (从 330, 270 走到 331, 270)
        Span<byte> normalMove = stackalloc byte[6];
        new BinaryProtocol.MoveIntentPayload
        {
            TargetX = 331,
            TargetY = 270,
            Direction = 2,
            IsRun = 0
        }.Write(normalMove);

        dispatcher.HandleBinaryMoveIntent(session, normalMove);
        // 权威状态更新
        Assert.Equal(331, session.X);
        Assert.Equal(270, session.Y);
        Assert.Equal(2, session.Direction);

        // 2. 瞬移外挂测试: 企图从 (331, 270) 一步直接跳到 (350, 290)
        Span<byte> teleportHack = stackalloc byte[6];
        new BinaryProtocol.MoveIntentPayload
        {
            TargetX = 350,
            TargetY = 290,
            Direction = 3,
            IsRun = 1
        }.Write(teleportHack);

        dispatcher.HandleBinaryMoveIntent(session, teleportHack);
        // 瞬移被直接否决！权威坐标仍保持在 (331, 270)
        Assert.Equal(331, session.X);
        Assert.Equal(270, session.Y);
    }

    [Fact]
    public void Test_AuthoritativeAttackAndGCD_防一秒十刀加速外挂()
    {
        var mapManager = new Mir2.Server.Maps.MapManager();
        var dispatcher = new Mir2.Server.Game.PacketDispatcher(mapManager);

        using var dummySocket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
        var attacker = new ClientSession(1, dummySocket)
        {
            UseModernProtocol = true,
            X = 300,
            Y = 300,
            CurrentMapId = 0,
            State = new Mir2.Shared.Models.PlayerState { Hp = 500, MaxHp = 500, Hit = 100 }
        };

        var defender = new ClientSession(2, dummySocket)
        {
            UseModernProtocol = true,
            X = 301,
            Y = 300,
            CurrentMapId = 0,
            State = new Mir2.Shared.Models.PlayerState { Hp = 500, MaxHp = 500 }
        };

        var map = mapManager.GetMap(0)!;
        map.Aoi.Enter(attacker, attacker.X, attacker.Y);
        map.Aoi.Enter(defender, defender.X, defender.Y);

        Span<byte> attackPayload = stackalloc byte[6];
        new BinaryProtocol.AttackIntentPayload
        {
            TargetUid = 2,
            Direction = 2,
            SkillType = 0
        }.Write(attackPayload);

        // 第 1 刀正常挥砍
        dispatcher.HandleBinaryAttackIntent(attacker, attackPayload);
        int hpAfterHit1 = defender.State.Hp;
        Assert.True(hpAfterHit1 < 500); // 造成了权威伤害

        // 模拟加速外挂 1 毫秒后立即挥出第 2 刀 (一秒十刀外挂)
        dispatcher.HandleBinaryAttackIntent(attacker, attackPayload);
        // 第 2 刀必须被 GCD 权威攻速锁硬性拦截，血量不得继续减少
        Assert.Equal(hpAfterHit1, defender.State.Hp);
    }

    [Fact]
    public void Test_ThousandPlayerAOI_LOD视距降采样广播()
    {
        var mapManager = new Mir2.Server.Maps.MapManager();
        var dispatcher = new Mir2.Server.Game.PacketDispatcher(mapManager);
        var map = mapManager.GetMap(3)!; // 盟重省 (800x800)

        using var dummySocket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
        
        // 核心玩家 A (坐标 400, 400)
        var playerA = new ClientSession(1, dummySocket) { UseModernProtocol = true, X = 400, Y = 400, CurrentMapId = 3 };
        // 近距离玩家 B (距离 3 格，坐标 403, 400)
        var playerNearB = new ClientSession(2, dummySocket) { UseModernProtocol = true, X = 403, Y = 400, CurrentMapId = 3 };
        // 远距离玩家 C (距离 20 格，同在盟重但切比雪夫距离 > 15 格)
        var playerFarC = new ClientSession(3, dummySocket) { UseModernProtocol = true, X = 420, Y = 400, CurrentMapId = 3 };

        map.Aoi.Enter(playerA, playerA.X, playerA.Y);
        map.Aoi.Enter(playerNearB, playerNearB.X, playerNearB.Y);
        map.Aoi.Enter(playerFarC, playerFarC.X, playerFarC.Y);

        ushort bSeqBefore = playerNearB.SendSequence;
        ushort cSeqBefore = playerFarC.SendSequence;

        // 模拟 A 走动 1 步并触发千人同屏九宫格 LOD 广播
        Span<byte> syncPayload = stackalloc byte[6];
        new BinaryProtocol.PositionSyncPayload
        {
            X = 401,
            Y = 400,
            Direction = 2,
            ForcePullback = 0
        }.Write(syncPayload);

        dispatcher.BroadcastBinaryToNinePatch(playerA, (ushort)BinaryProtocol.MsgType.BroadcastEntityMove, syncPayload, includeSelf: false, maxDist: 15);

        // 验证:
        // 1. 近距离玩家 B 顺利收到广播 (SendSequence 增加)
        Assert.True(playerNearB.SendSequence > bSeqBefore);
        // 2. 远距离玩家 C 被 LOD 算法视距降采样有效剔除 (SendSequence 维持原样，节省网络带宽)
        Assert.Equal(cSeqBefore, playerFarC.SendSequence);
    }

    [Fact]
    public void Test_ServerAuthoritative_ShopSell_DirectBlit_Roundtrip()
    {
        // 1. 结构体大小契约
        Assert.Equal(8, Marshal.SizeOf<BinaryProtocol.ShopSellIntentPayload>());

        var sellPayload = new BinaryProtocol.ShopSellIntentPayload
        {
            NpcUid = 2001,
            ItemUid = 9999
        };
        Span<byte> buf = stackalloc byte[8];
        sellPayload.Write(buf);

        var restored = BinaryProtocol.ShopSellIntentPayload.Read(buf);
        Assert.Equal(2001, restored.NpcUid);
        Assert.Equal(9999, restored.ItemUid);

        // 2. 服务端权威出售逻辑闭环验证
        var dispatcher = new Mir2.Server.Game.PacketDispatcher(new Mir2.Server.Maps.MapManager());
        var session = new Mir2.Server.Network.ClientSession(1, null!)
        {
            State = new Mir2.Shared.Models.PlayerState
            {
                Hp = 100,
                MaxHp = 100,
                Gold = 500
            }
        };

        // 放入一件生铁戒指
        var ring = new Mir2.Shared.Models.EquipItem
        {
            Uid = 9999,
            Name = "生铁戒指",
            Weight = 1,
            Dura = 5,
            MaxDura = 5
        };
        session.State.BagItems.Add(ring);
        session.State.BagWeight = ring.Weight;

        // 构造二进制帧并分发
        byte[] payload = new byte[8];
        sellPayload.Write(payload);

        var header = new BinaryProtocol.PacketHeader
        {
            Magic = BinaryProtocol.DefaultMagic,
            MsgId = (ushort)BinaryProtocol.MsgType.IntentShopSell,
            PayloadLength = (ushort)payload.Length,
            Sequence = 1
        };

        dispatcher.DispatchBinary(session, in header, payload);

        // 验证背包物品已被服务端权威移除
        Assert.DoesNotContain(session.State.BagItems, i => i.Uid == 9999);
        // 验证金币获得增加
        Assert.True(session.State.Gold > 500);
    }
}
