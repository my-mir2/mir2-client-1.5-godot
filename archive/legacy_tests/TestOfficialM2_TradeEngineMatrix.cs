using System;
using System.Text;
using System.Threading.Tasks;
using Mir2.Server.Core;
using Mir2.Server.Game;
using Mir2.Server.Maps;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;
using Mir2.Shared.Network.HPSocket;
using Mir2.Shared.Protocols.Modern;
using Xunit;

namespace Mir2Client.Tests;

public class TestOfficialM2_TradeEngineMatrix
{
    [Fact]
    public void TestOfficialM2_Trade_AntiFastDeal_1000ms_Meltdown_Protection()
    {
        var engine = new TradeEngine();

        var playerA = new ClientSession(1, null!)
        {
            CharacterName = "买家A",
            X = 100,
            Y = 100,
            CurrentMapId = 0,
            State = new PlayerState { Gold = 10000 }
        };
        var playerB = new ClientSession(2, null!)
        {
            CharacterName = "卖家B",
            X = 101,
            Y = 100,
            CurrentMapId = 0,
            State = new PlayerState { Gold = 20000 }
        };

        // 发起面对面交易 (距离 1 格)
        var (initiated, _) = engine.TryInitiateTrade(playerA, playerB);
        Assert.True(initiated);

        var trade = engine.GetTrade(playerA.SessionId);
        Assert.NotNull(trade);

        // 模拟刚修改完交易金币后不到 1000ms (例如仅仅过去了 100ms)
        trade.LastChangeTickA = Environment.TickCount64 - 100;

        // 买家 A 急切点击确认 Deal
        var (complete, canceled, reason) = engine.ConfirmDeal(playerA);

        // 验证官方 1:1 防刷物防快点连击熔断机制 (IDA sub_4DDC8C @ 0x4DDCF5)
        Assert.False(complete);
        Assert.True(canceled);
        Assert.Equal("Hit Deal Too Fast!", reason);

        // 交易已被服务器直接强制注销释放
        Assert.Null(engine.GetTrade(playerA.SessionId));
        Assert.Null(engine.GetTrade(playerB.SessionId));
    }

    [Fact]
    public void TestOfficialM2_Trade_OverCapacity_Or_GoldLimit_Meltdown()
    {
        var engine = new TradeEngine();

        var playerA = new ClientSession(1, null!)
        {
            CharacterName = "满包A",
            X = 200,
            Y = 200,
            CurrentMapId = 0,
            State = new PlayerState { Gold = 5000 }
        };
        // A 的背包已经塞满 46 件物品 (达到官方交易 46 格满包防撑爆红线)
        for (int i = 0; i < 46; i++)
        {
            playerA.State.BagItems.Add(new EquipItem { Uid = 1000 + i, Name = $"杂物_{i}" });
        }

        var playerB = new ClientSession(2, null!)
        {
            CharacterName = "卖家B",
            X = 201,
            Y = 200,
            CurrentMapId = 0,
            State = new PlayerState { Gold = 5000 }
        };
        var dragonSlayer = new EquipItem { Uid = 8888, Name = "屠龙", Weight = 50 };
        playerB.State.BagItems.Add(dragonSlayer);

        engine.TryInitiateTrade(playerA, playerB);

        // 卖家 B 放入屠龙刀
        engine.AddItem(playerB, 8888);

        // 跨过 1000ms 冷却
        var trade = engine.GetTrade(playerA.SessionId)!;
        trade.LastChangeTickA = Environment.TickCount64 - 2000;
        trade.LastChangeTickB = Environment.TickCount64 - 2000;

        // 双方点击确认锁定
        engine.ConfirmDeal(playerA);
        var (complete, canceled, reason) = engine.ConfirmDeal(playerB);

        // 验证 46 格防撑爆检定 (IDA sub_4DDC8C @ 0x4DDD52)
        Assert.False(complete);
        Assert.True(canceled);
        Assert.Contains("背包空间不足", reason);

        // 屠龙刀必须安全原路退回 B 的背包中！
        Assert.Single(playerB.State.BagItems);
        Assert.Equal("屠龙", playerB.State.BagItems[0].Name);
    }

    [Fact]
    public void TestOfficialM2_Trade_GoldOverWeight_Meltdown()
    {
        var engine = new TradeEngine();

        var playerA = new ClientSession(1, null!)
        {
            CharacterName = "法师A",
            X = 200,
            Y = 200,
            CurrentMapId = 0,
            State = new PlayerState
            {
                Gold = 10_000,
                MaxBagWeight = 200,
                BagWeight = 190 // 负重剩余空间仅剩 10 点
            }
        };

        var playerB = new ClientSession(2, null!)
        {
            CharacterName = "土豪B",
            X = 201,
            Y = 200,
            CurrentMapId = 0,
            State = new PlayerState
            {
                Gold = 5_000_000,
                MaxBagWeight = 500,
                BagWeight = 50
            }
        };

        engine.TryInitiateTrade(playerA, playerB);

        // 土豪 B 尝试交易 2,000,000 金币 (折算重量 200 点，导致法师A 190 + 200 = 390 > 200 严重超重)
        engine.ChangeGold(playerB, 2_000_000);

        var trade = engine.GetTrade(playerA.SessionId)!;
        trade.LastChangeTickA = Environment.TickCount64 - 2000;
        trade.LastChangeTickB = Environment.TickCount64 - 2000;

        // 双方确认锁定
        engine.ConfirmDeal(playerA);
        var (complete, canceled, reason) = engine.ConfirmDeal(playerB);

        // 负重超限强制熔断取消
        Assert.False(complete);
        Assert.True(canceled);
        Assert.Contains("负重", reason);

        // 双方资产未被篡改
        Assert.Equal(10_000, playerA.State.Gold);
        Assert.Equal(5_000_000, playerB.State.Gold);
    }

    private static int GetRandomTestPort() => TestNetworkPortHelper.GetNextPort();

    private static byte[] BuildPack(ushort msgId, string payloadStr = "", ushort seq = 0)
    {
        byte[] payloadBytes = string.IsNullOrEmpty(payloadStr) ? Array.Empty<byte>() : Encoding.UTF8.GetBytes(payloadStr);
        byte[] packet = new byte[BinaryProtocol.HeaderSize + payloadBytes.Length];
        var span = packet.AsSpan();
        System.Buffers.Binary.BinaryPrimitives.WriteUInt16LittleEndian(span[0..2], BinaryProtocol.DefaultMagic);
        System.Buffers.Binary.BinaryPrimitives.WriteUInt16LittleEndian(span[2..4], msgId);
        System.Buffers.Binary.BinaryPrimitives.WriteUInt16LittleEndian(span[4..6], (ushort)payloadBytes.Length);
        System.Buffers.Binary.BinaryPrimitives.WriteUInt16LittleEndian(span[6..8], seq);
        if (payloadBytes.Length > 0)
        {
            payloadBytes.CopyTo(packet, BinaryProtocol.HeaderSize);
        }
        return packet;
    }

    private static async Task SendPackAsync(System.Net.Sockets.NetworkStream stream, ushort msgId, string payloadStr = "")
    {
        byte[] packet = BuildPack(msgId, payloadStr);
        await stream.WriteAsync(packet);
        await stream.FlushAsync();
    }

    private static async Task<string> ReadUntilContainsPackAsync(System.Net.Sockets.NetworkStream stream, string target, int timeoutMs = 2500)
    {
        var decoder = new HpPackStreamDecoder();
        var sb = new StringBuilder();
        byte[] buf = new byte[4096];
        long deadline = Environment.TickCount64 + timeoutMs;
        while (Environment.TickCount64 < deadline)
        {
            if (stream.DataAvailable)
            {
                int n = await stream.ReadAsync(buf);
                if (n > 0)
                {
                    decoder.FeedData(buf.AsSpan(0, n), (in BinaryProtocol.PacketHeader header, ReadOnlySpan<byte> payload) =>
                    {
                        int recog = 0, param = 0, tag = 0, series = 0;
                        string extra = string.Empty;
                        if (payload.Length >= 16)
                        {
                            recog = System.Buffers.Binary.BinaryPrimitives.ReadInt32LittleEndian(payload[0..4]);
                            param = System.Buffers.Binary.BinaryPrimitives.ReadInt32LittleEndian(payload[4..8]);
                            tag = System.Buffers.Binary.BinaryPrimitives.ReadInt32LittleEndian(payload[8..12]);
                            series = System.Buffers.Binary.BinaryPrimitives.ReadInt32LittleEndian(payload[12..16]);
                            if (payload.Length > 16)
                            {
                                extra = Encoding.UTF8.GetString(payload[16..]);
                            }
                        }
                        else if (payload.Length > 0)
                        {
                            extra = Encoding.UTF8.GetString(payload);
                        }
                        string packSummary = $"#{header.MsgId}/{recog}/{param}/{tag}/{series}/{extra}!";
                        sb.Append(packSummary);
                        if (!string.IsNullOrEmpty(extra)) sb.Append(extra);
                    });

                    if (!string.IsNullOrEmpty(target) && sb.ToString().Contains(target))
                        return sb.ToString();
                }
            }
            await Task.Delay(10);
        }
        return sb.ToString();
    }

    [Fact]
    public async Task TestE2E_Trade_FullAtomicExchange_BetweenTwoPlayers()
    {
        int testPort = GetRandomTestPort();
        var mapManager = new MapManager();
        var tradeEngine = new TradeEngine();
        var dispatcher = new PacketDispatcher(mapManager, regenEngine: null, npcEngine: null, tradeEngine: tradeEngine);

        var gateway = new TcpGateway(testPort);
        gateway.PacketReceivedHandler = (session, pkt) => dispatcher.Dispatch(session, pkt);
        gateway.BinaryPacketReceivedHandler = (session, header, payload) => dispatcher.DispatchBinary(session, header, payload);
        gateway.Start();

        var tickEngine = new GameWorldTick(gateway);
        tickEngine.Start();

        try
        {
            // 客户端 A: 位于 (300, 300)
            using var clientA = new System.Net.Sockets.TcpClient();
            await clientA.ConnectAsync("127.0.0.1", testPort);
            var streamA = clientA.GetStream();
            await SendPackAsync(streamA, 103, "富豪A/300/300");

            // 客户端 B: 面对面位于 (301, 300)
            using var clientB = new System.Net.Sockets.TcpClient();
            await clientB.ConnectAsync("127.0.0.1", testPort);
            var streamB = clientB.GetStream();
            await SendPackAsync(streamB, 103, "神兵B/301/300");

            await Task.Delay(200);

            var map = mapManager.GetMap(0);
            var buffer = new System.Collections.Generic.List<ClientSession>();
            map?.Aoi.GetEntitiesInNinePatch(300, 300, buffer);
            var sessionA = buffer.Find(s => s.CharacterName == "富豪A");
            var sessionB = buffer.Find(s => s.CharacterName == "神兵B");
            Assert.NotNull(sessionA);
            Assert.NotNull(sessionB);

            // A 持有 500,000 金币；B 持有裁决之杖
            sessionA.State!.Gold = 500_000;
            sessionB.State!.Gold = 10_000;
            sessionB.State.BagItems.Add(new EquipItem { Uid = 7777, Name = "裁决之杖", Weight = 50 });

            byte[] flushBuf = new byte[4096];
            while (streamA.DataAvailable) _ = await streamA.ReadAsync(flushBuf, 0, flushBuf.Length);
            while (streamB.DataAvailable) _ = await streamB.ReadAsync(flushBuf, 0, flushBuf.Length);

            // 1. A 发起交易邀请: CM_DEALTRY (1025)
            await SendPackAsync(streamA, 1025, $"{sessionB.SessionId}");

            // 双方弹出交易面板 SM_DEALMENU (673)
            await ReadUntilContainsPackAsync(streamA, $"#{ProtocolConsts.SM_DEALMENU}/");
            await ReadUntilContainsPackAsync(streamB, $"#{ProtocolConsts.SM_DEALMENU}/");

            while (streamA.DataAvailable) _ = await streamA.ReadAsync(flushBuf, 0, flushBuf.Length);
            while (streamB.DataAvailable) _ = await streamB.ReadAsync(flushBuf, 0, flushBuf.Length);

            // 2. A 放入 300,000 交易金币: CM_DEALCHGGOLD (1029)
            await SendPackAsync(streamA, 1029, "300000");

            // 3. B 放入裁决之杖: CM_DEALADDITEM (1026)
            await SendPackAsync(streamB, 1026, "7777");

            await Task.Delay(150);

            // 等待超过 1000ms 跨过防快点门限
            await Task.Delay(1100);

            while (streamA.DataAvailable) _ = await streamA.ReadAsync(flushBuf, 0, flushBuf.Length);
            while (streamB.DataAvailable) _ = await streamB.ReadAsync(flushBuf, 0, flushBuf.Length);

            // 4. 双方点击确认锁定: CM_DEALEND (1030)
            await SendPackAsync(streamA, 1030);

            await Task.Delay(100);

            await SendPackAsync(streamB, 1030);

            // 双方必须收到交易完成协议 SM_DEALSUCCESS (687) 与金币变动包
            string respA = await ReadUntilContainsPackAsync(streamA, $"#{ProtocolConsts.SM_DEALSUCCESS}/", timeoutMs: 3000);
            string respB = await ReadUntilContainsPackAsync(streamB, $"#{ProtocolConsts.SM_DEALSUCCESS}/", timeoutMs: 3000);

            Assert.Contains($"#{ProtocolConsts.SM_DEALSUCCESS}/", respA);
            Assert.Contains($"#{ProtocolConsts.SM_DEALSUCCESS}/", respB);

            // 5. 验证原子资产交割真值:
            // A 扣除 300,000 金币，获得裁决之杖
            Assert.Equal(200_000, sessionA.State.Gold);
            Assert.Contains(sessionA.State.BagItems, item => item.Name == "裁决之杖");

            // B 得到 300,000 金币，失去裁决之杖
            Assert.Equal(310_000, sessionB.State.Gold);
            Assert.DoesNotContain(sessionB.State.BagItems, item => item.Name == "裁决之杖");
        }
        finally
        {
            tickEngine.Stop();
            gateway.Stop();
        }
    }
}
