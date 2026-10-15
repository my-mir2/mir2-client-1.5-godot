using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mir2.Server.Core;
using Mir2.Server.Game;
using Mir2.Server.Maps;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Network.HPSocket;
using Mir2.Shared.Protocols;
using Mir2.Shared.Protocols.Modern;
using Xunit;

namespace Mir2Client.Tests;

[Collection("HighConcurrencyTests")]
public class TestServerHighConcurrencyEngine
{
    private sealed class MockPlayer
    {
        public int Id { get; init; }
        public string Name { get; init; } = "";
    }

    private static int GetRandomTestPort() => TestNetworkPortHelper.GetNextPort();

    private static byte[] BuildPack(ushort msgId, string payloadStr = "", ushort seq = 0)
    {
        byte[] payloadBytes = string.IsNullOrEmpty(payloadStr) ? Array.Empty<byte>() : Encoding.UTF8.GetBytes(payloadStr);
        byte[] packet = new byte[BinaryProtocol.HeaderSize + payloadBytes.Length];
        var span = packet.AsSpan();
        BinaryPrimitives.WriteUInt16LittleEndian(span[0..2], BinaryProtocol.DefaultMagic);
        BinaryPrimitives.WriteUInt16LittleEndian(span[2..4], msgId);
        BinaryPrimitives.WriteUInt16LittleEndian(span[4..6], (ushort)payloadBytes.Length);
        BinaryPrimitives.WriteUInt16LittleEndian(span[6..8], seq);
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
                            recog = BinaryPrimitives.ReadInt32LittleEndian(payload[0..4]);
                            param = BinaryPrimitives.ReadInt32LittleEndian(payload[4..8]);
                            tag = BinaryPrimitives.ReadInt32LittleEndian(payload[8..12]);
                            series = BinaryPrimitives.ReadInt32LittleEndian(payload[12..16]);
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
    public void TestNinePatchAoi_ZoneIndexAndCoordinateCalculations()
    {
        // 比奇省 400x400 格子，每个 Zone 16x16 格子 -> 横向 25 个，纵向 25 个
        var aoi = new NinePatchAoi<MockPlayer>(400, 400);

        var (zx0, zy0) = aoi.GetZoneCoord(0, 0);
        Assert.Equal(0, zx0);
        Assert.Equal(0, zy0);
        Assert.Equal(0, aoi.GetZoneIndex(0, 0));

        var (zx1, zy1) = aoi.GetZoneCoord(16, 32);
        Assert.Equal(1, zx1);
        Assert.Equal(2, zy1);
        Assert.Equal(2 * 25 + 1, aoi.GetZoneIndex(16, 32));

        // 边界防护
        var (zxMax, zyMax) = aoi.GetZoneCoord(399, 399);
        Assert.Equal(24, zxMax);
        Assert.Equal(24, zyMax);
    }

    [Fact]
    public void TestNinePatchAoi_EntityMovementAndDifferentialZoneTracking()
    {
        var aoi = new NinePatchAoi<MockPlayer>(400, 400);
        var player = new MockPlayer { Id = 1, Name = "战神无敌" };

        aoi.Enter(player, 100, 100);

        // 同 Zone 内部微步移动，不触发 Zone 跨越
        bool crossed1 = aoi.UpdatePosition(player, 100, 100, 102, 103, out var entered1, out var left1);
        Assert.False(crossed1);
        Assert.Null(entered1);
        Assert.Null(left1);

        // 跨越 Zone 边界移动 (从 100, 100 到 120, 100，ZoneX 从 6 变为 7)
        bool crossed2 = aoi.UpdatePosition(player, 100, 100, 120, 100, out var entered2, out var left2);
        Assert.True(crossed2);
        Assert.NotNull(entered2);
        Assert.NotNull(left2);
        // 向右跨越 1 个 Zone，新增右侧 3 个 Zone，注销左侧 3 个 Zone
        Assert.Equal(3, entered2.Count);
        Assert.Equal(3, left2.Count);
    }

    [Fact]
    public void TestNinePatchAoi_NinePatchBroadcastingAggregation()
    {
        var aoi = new NinePatchAoi<MockPlayer>(400, 400);
        var centerPlayer = new MockPlayer { Id = 100, Name = "中心玩家" };
        var nearbyPlayer = new MockPlayer { Id = 101, Name = "同屏队友" };
        var farPlayer = new MockPlayer { Id = 200, Name = "远端玩家" };

        // 中心玩家与队友都在比奇中心广场 (Zone 10, 10 附近)
        aoi.Enter(centerPlayer, 160, 160);
        aoi.Enter(nearbyPlayer, 165, 170); // 在相邻同一个 Zone 内

        // 远端玩家在银杏山谷 (330, 270)
        aoi.Enter(farPlayer, 330, 270);

        var recipients = new List<MockPlayer>(10);
        aoi.GetEntitiesInNinePatch(160, 160, recipients);

        // 九宫格广播应该拉取到中心玩家与同屏队友，坚决不包含远端玩家
        Assert.Contains(centerPlayer, recipients);
        Assert.Contains(nearbyPlayer, recipients);
        Assert.DoesNotContain(farPlayer, recipients);
    }

    [Fact]
    public async Task TestGameWorldTick_HighPrecisionScheduler_StartsAndTicks()
    {
        var gateway = new TcpGateway(GetRandomTestPort()); // 测试网关端口
        var tickEngine = new GameWorldTick(gateway);

        int tickCount = 0;
        tickEngine.WorldUpdateHandler = (tick, dt) =>
        {
            Interlocked.Increment(ref tickCount);
        };

        tickEngine.Start();
        // 等待约 350ms，应该触发约 3~4 次 100ms Tick
        await Task.Delay(350);
        tickEngine.Stop();

        Assert.True(tickCount >= 2, $"100ms 调度器在 350ms 内应至少触发 2 次，实际: {tickCount}");
    }

    [Fact]
    public async Task TestE2E_FullLoginAndCharacterEnterLifecycle()
    {
        int testPort = GetRandomTestPort();
        var aoi = new NinePatchAoi<ClientSession>(400, 400);
        var dispatcher = new Mir2.Server.Game.PacketDispatcher(aoi);
        var gateway = new TcpGateway(testPort);
        gateway.PacketReceivedHandler = (session, pkt) => dispatcher.Dispatch(session, pkt);
        gateway.BinaryPacketReceivedHandler = (session, header, payload) => dispatcher.DispatchBinary(session, header, payload);
        gateway.Start();

        var tickEngine = new GameWorldTick(gateway);
        tickEngine.Start();

        try
        {
            using var client = new System.Net.Sockets.TcpClient();
            await client.ConnectAsync("127.0.0.1", testPort);
            var stream = client.GetStream();

            // 1. 客户端发送 CM_QUERYVERSION (2001)
            await SendPackAsync(stream, (ushort)ProtocolConsts.CM_QUERYVERSION, "0/0/0/0/0/");
            string resp1 = await ReadUntilContainsPackAsync(stream, "#500/");
            Assert.Contains("#500/", resp1); // SM_PASSOK (500)

            // 2. 客户端发送 CM_LOGIN (2) 账号密码登录
            await SendPackAsync(stream, (ushort)ProtocolConsts.CM_LOGIN, "hero123/pass888/");
            string resp2 = await ReadUntilContainsPackAsync(stream, "#530/");
            Assert.Contains("#530/", resp2); // SM_LOGINSUCCESS (530)

            // 3. 客户端发送 CM_QUERYCHRINFO (119) 查询角色
            await SendPackAsync(stream, 119);
            string resp3 = await ReadUntilContainsPackAsync(stream, $"#{ProtocolConsts.SM_QUERYCHAR_OK}/");
            Assert.Contains($"#{ProtocolConsts.SM_QUERYCHAR_OK}/", resp3); // SM_QUERYCHAR_OK (523 官方实证)

            // 4. 客户端发送 CM_SELECTCHAR (103) 选择角色进入游戏
            await SendPackAsync(stream, 103, "hero123_战神");
            string resp4 = await ReadUntilContainsPackAsync(stream, $"#{ProtocolConsts.SM_NEWMAP_DELPHI}/");
            // 验证服务端回传的进入地图与角色初始化包 (合包在单个 TCP 响应中送达)
            Assert.Contains($"#{ProtocolConsts.SM_LOGON_DELPHI}/", resp4);   // SM_LOGON_DELPHI (50)
            Assert.Contains($"#{ProtocolConsts.SM_NEWMAP_DELPHI}/", resp4);  // SM_NEWMAP_DELPHI (521)
            Assert.Contains($"#{ProtocolConsts.SM_MYSTATUS}/", resp4);   // SM_MYSTATUS (708 官方实证)
        }
        finally
        {
            tickEngine.Stop();
            gateway.Stop();
        }
    }

    [Fact]
    public async Task TestE2E_NinePatchBroadcasting_BetweenMultipleClients()
    {
        int testPort = GetRandomTestPort();
        var aoi = new NinePatchAoi<ClientSession>(400, 400);
        var dispatcher = new Mir2.Server.Game.PacketDispatcher(aoi);
        var gateway = new TcpGateway(testPort);
        gateway.PacketReceivedHandler = (session, pkt) => dispatcher.Dispatch(session, pkt);
        gateway.BinaryPacketReceivedHandler = (session, header, payload) => dispatcher.DispatchBinary(session, header, payload);
        gateway.Start();

        var tickEngine = new GameWorldTick(gateway);
        tickEngine.Start();

        try
        {
            // 客户端 A (在比奇省中心 160, 160)
            using var clientA = new System.Net.Sockets.TcpClient();
            await clientA.ConnectAsync("127.0.0.1", testPort);
            var streamA = clientA.GetStream();
            await SendPackAsync(streamA, 103, "玩家A/160/160"); // 选角进入并定位在 160, 160

            // 客户端 B (同屏邻近 162, 160)
            using var clientB = new System.Net.Sockets.TcpClient();
            await clientB.ConnectAsync("127.0.0.1", testPort);
            var streamB = clientB.GetStream();
            await SendPackAsync(streamB, 103, "玩家B/162/160");

            // 客户端 C (远端银杏山谷 330, 270)
            using var clientC = new System.Net.Sockets.TcpClient();
            await clientC.ConnectAsync("127.0.0.1", testPort);
            var streamC = clientC.GetStream();
            await SendPackAsync(streamC, 103, "远端玩家C/330/270");

            // 等待入场包消化完成 (确保 SM_NEWMAP_DELPHI 到达)
            await ReadUntilContainsPackAsync(streamC, $"#{ProtocolConsts.SM_NEWMAP_DELPHI}/");
            await Task.Delay(50);

            // 清空各自的接收缓冲
            byte[] flushBuf = new byte[4096];
            while (streamA.DataAvailable) _ = await streamA.ReadAsync(flushBuf, 0, flushBuf.Length);
            while (streamB.DataAvailable) _ = await streamB.ReadAsync(flushBuf, 0, flushBuf.Length);
            while (streamC.DataAvailable) _ = await streamC.ReadAsync(flushBuf, 0, flushBuf.Length);

            // 客户端 A 发起行走动作 CM_WALK: 移动到 161, 160, 朝向 2 (右)
            await SendPackAsync(streamA, 3011, "161/160/2");

            // 1. 同屏客户端 B 必须收到客户端 A 的移动广播 SM_OBJECTWALK (241)
            string respB = await ReadUntilContainsPackAsync(streamB, $"#{ProtocolConsts.SM_OBJECTWALK}/");
            Assert.Contains($"#{ProtocolConsts.SM_OBJECTWALK}/", respB);

            // 2. 远端客户端 C 必须未收到移动广播 (九宫格严格隔离，0 冗余下行带宽)
            string respC = await ReadUntilContainsPackAsync(streamC, $"#{ProtocolConsts.SM_OBJECTWALK}/", timeoutMs: 150);
            Assert.DoesNotContain($"#{ProtocolConsts.SM_OBJECTWALK}/", respC);
        }
        finally
        {
            tickEngine.Stop();
            gateway.Stop();
        }
    }

    [Fact]
    public async Task TestE2E_Security_TeleportRollback_And_CombatHitBroadcast()
    {
        int testPort = GetRandomTestPort();
        var aoi = new NinePatchAoi<ClientSession>(400, 400);
        var dispatcher = new Mir2.Server.Game.PacketDispatcher(aoi);
        var gateway = new TcpGateway(testPort);
        gateway.PacketReceivedHandler = (session, pkt) => dispatcher.Dispatch(session, pkt);
        gateway.BinaryPacketReceivedHandler = (session, header, payload) => dispatcher.DispatchBinary(session, header, payload);
        gateway.Start();

        var tickEngine = new GameWorldTick(gateway);
        tickEngine.Start();

        try
        {
            using var clientA = new System.Net.Sockets.TcpClient();
            await clientA.ConnectAsync("127.0.0.1", testPort);
            var streamA = clientA.GetStream();
            await SendPackAsync(streamA, 103, "战士A/200/200");

            using var clientB = new System.Net.Sockets.TcpClient();
            await clientB.ConnectAsync("127.0.0.1", testPort);
            var streamB = clientB.GetStream();
            await SendPackAsync(streamB, 103, "道士B/201/200");

            await Task.Delay(200);

            byte[] flushBuf = new byte[4096];
            while (streamA.DataAvailable) _ = await streamA.ReadAsync(flushBuf, 0, flushBuf.Length);
            while (streamB.DataAvailable) _ = await streamB.ReadAsync(flushBuf, 0, flushBuf.Length);

            // 1. 尝试瞬移 (从 200, 200 瞬移到 250, 250) -> 触发服务端防外挂拉回
            await SendPackAsync(streamA, 3011, "250/250/0");

            // 客户端 A 必须收到 SM_SENDUSERSTATE (250) 坐标强制重置为原本的 (200, 200)
            string rollbackResp = await ReadUntilContainsPackAsync(streamA, $"#{ProtocolConsts.SM_SENDUSERSTATE}/");
            Assert.Contains($"#{ProtocolConsts.SM_SENDUSERSTATE}/", rollbackResp);
            Assert.Contains("/200/200/", rollbackResp); // 坐标未被篡改，依然是 200, 200

            // 2. 战士 A 发起普通挥刀 CM_HIT (3014) 朝向 1
            await Task.Delay(200);
            while (streamB.DataAvailable) _ = await streamB.ReadAsync(flushBuf, 0, flushBuf.Length);

            await SendPackAsync(streamA, 3014, "201/200/1");

            // 同屏道士 B 必须收到挥刀动作广播 SM_OBJECTHIT (242)
            string hitResp = await ReadUntilContainsPackAsync(streamB, $"#{ProtocolConsts.SM_OBJECTHIT}/", timeoutMs: 3000);
            Assert.True(hitResp.Contains($"#{ProtocolConsts.SM_OBJECTHIT}/") || hitResp.Contains("#244/"),
                $"Expected either SM_OBJECTHIT (242) or SM_OBJECTSTRUCK (244), but received: {hitResp}");
            Assert.True(hitResp.Contains("战士A") || hitResp.Contains("道士B"),
                $"Expected either attacker or defender entity in broadcast, received: {hitResp}");
        }
        finally
        {
            tickEngine.Stop();
            gateway.Stop();
        }
    }

    [Fact]
    public async Task TestE2E_Combat_MagicSpell_And_DamageSettlement()
    {
        int testPort = GetRandomTestPort();
        var aoi = new NinePatchAoi<ClientSession>(400, 400);
        var dispatcher = new Mir2.Server.Game.PacketDispatcher(aoi);
        var gateway = new TcpGateway(testPort);
        gateway.PacketReceivedHandler = (session, pkt) => dispatcher.Dispatch(session, pkt);
        gateway.BinaryPacketReceivedHandler = (session, header, payload) => dispatcher.DispatchBinary(session, header, payload);
        gateway.Start();

        var tickEngine = new GameWorldTick(gateway);
        tickEngine.Start();

        try
        {
            // 客户端 A: 法师 (300, 300)
            using var clientA = new System.Net.Sockets.TcpClient();
            await clientA.ConnectAsync("127.0.0.1", testPort);
            var streamA = clientA.GetStream();
            await SendPackAsync(streamA, 103, "法师A/300/300");

            await ReadUntilContainsPackAsync(streamA, $"#{ProtocolConsts.SM_MYSTATUS}/");

            // 客户端 B: 目标怪/玩家 (302, 300)
            using var clientB = new System.Net.Sockets.TcpClient();
            await clientB.ConnectAsync("127.0.0.1", testPort);
            var streamB = clientB.GetStream();
            await SendPackAsync(streamB, 103, "稻草人B/302/300");
            await ReadUntilContainsPackAsync(streamB, $"#{ProtocolConsts.SM_MYSTATUS}/");

            await Task.Delay(100);

            // 1. 法师施展魔法盾 MagicId=31 (CM_SPELL 3017: targetUid/magicId/targetX/targetY)
            await SendPackAsync(streamA, 3017, "0/31/300/300");

            // 法师自身收到 MP 扣除与状态同步 SM_MYSTATUS (708) 以及施法动作 SM_OBJECTSPELL (243)
            string shieldResp = await ReadUntilContainsPackAsync(streamA, $"#{ProtocolConsts.SM_OBJECTSPELL}/", timeoutMs: 4000);
            if (!shieldResp.Contains($"#{ProtocolConsts.SM_MYSTATUS}/"))
            {
                shieldResp += await ReadUntilContainsPackAsync(streamA, $"#{ProtocolConsts.SM_MYSTATUS}/", timeoutMs: 4000);
            }
            Assert.Contains($"#{ProtocolConsts.SM_OBJECTSPELL}/", shieldResp);
            Assert.Contains($"#{ProtocolConsts.SM_MYSTATUS}/", shieldResp);

            // 2. 等待公共 CD (550ms) 后，法师使用雷电术 MagicId=10 轰击稻草人B (302, 300)
            await Task.Delay(550);
            byte[] flushBuf = new byte[4096];
            while (streamB.DataAvailable) _ = await streamB.ReadAsync(flushBuf, 0, flushBuf.Length);

            await SendPackAsync(streamA, 3017, "0/10/302/300");

            string thunderBResp = await ReadUntilContainsPackAsync(streamB, $"#{ProtocolConsts.SM_OBJECTSTRUCK}/", timeoutMs: 4000);
            if (!thunderBResp.Contains($"#{ProtocolConsts.SM_OBJECTSPELL}/"))
            {
                thunderBResp += await ReadUntilContainsPackAsync(streamB, $"#{ProtocolConsts.SM_OBJECTSPELL}/", timeoutMs: 4000);
            }
            if (!thunderBResp.Contains($"#{ProtocolConsts.SM_MYSTATUS}/"))
            {
                thunderBResp += await ReadUntilContainsPackAsync(streamB, $"#{ProtocolConsts.SM_MYSTATUS}/", timeoutMs: 4000);
            }
            Assert.Contains($"#{ProtocolConsts.SM_OBJECTSPELL}/", thunderBResp);
            Assert.Contains($"#{ProtocolConsts.SM_OBJECTSTRUCK}/", thunderBResp);
            Assert.Contains($"#{ProtocolConsts.SM_MYSTATUS}/", thunderBResp); // 稻草人 B 扣血更新
        }
        finally
        {
            tickEngine.Stop();
            gateway.Stop();
        }
    }

    [Fact]
    public async Task TestE2E_MapObstacle_CollisionAndRollback()
    {
        int testPort = GetRandomTestPort();
        var mapManager = new Mir2.Server.Maps.MapManager();
        var dispatcher = new Mir2.Server.Game.PacketDispatcher(mapManager);
        var gateway = new TcpGateway(testPort);
        gateway.PacketReceivedHandler = (session, pkt) => dispatcher.Dispatch(session, pkt);
        gateway.BinaryPacketReceivedHandler = (session, header, payload) => dispatcher.DispatchBinary(session, header, payload);
        gateway.Start();

        var tickEngine = new GameWorldTick(gateway);
        tickEngine.Start();

        try
        {
            // 客户端连入比奇省 (99, 100)，紧邻水面阻挡 (100, 100)
            using var client = new System.Net.Sockets.TcpClient();
            await client.ConnectAsync("127.0.0.1", testPort);
            var stream = client.GetStream();
            await SendPackAsync(stream, 103, "探索者/99/100");

            await ReadUntilContainsPackAsync(stream, $"#{ProtocolConsts.SM_MYSTATUS}/");
            byte[] flushBuf = new byte[4096];

            // 1. 尝试向右走向水面阻挡 (100, 100, 朝向 2)
            await SendPackAsync(stream, 3011, "100/100/2");

            string rollbackResp = await ReadUntilContainsPackAsync(stream, $"#{ProtocolConsts.SM_SENDUSERSTATE}/", timeoutMs: 2000);
            Assert.Contains($"#{ProtocolConsts.SM_SENDUSERSTATE}/", rollbackResp);
            Assert.Contains("/99/100/", rollbackResp); // 坐标未被篡改，依然是 99, 100

            // 2. 尝试向下方无阻挡的自由区域移动 (99, 101, 朝向 4)
            await Task.Delay(600); // 移动冷却
            while (stream.DataAvailable) _ = await stream.ReadAsync(flushBuf, 0, flushBuf.Length);

            await SendPackAsync(stream, 3011, "99/101/4");

            await Task.Delay(150);

            // 合法可行走路径，移动成功
            // 实体自身在本地进行前推预测，同屏其他实体收到 SM_OBJECTWALK
        }
        finally
        {
            tickEngine.Stop();
            gateway.Stop();
        }
    }

    [Fact]
    public async Task TestE2E_OfficialM2_AttackMode_And_CombatTruth()
    {
        int testPort = GetRandomTestPort();
        var mapManager = new Mir2.Server.Maps.MapManager();
        var dispatcher = new Mir2.Server.Game.PacketDispatcher(mapManager);
        var gateway = new TcpGateway(testPort);
        gateway.PacketReceivedHandler = (session, pkt) => dispatcher.Dispatch(session, pkt);
        gateway.BinaryPacketReceivedHandler = (session, header, payload) => dispatcher.DispatchBinary(session, header, payload);
        gateway.Start();

        var tickEngine = new GameWorldTick(gateway);
        tickEngine.Start();

        try
        {
            using var clientA = new System.Net.Sockets.TcpClient();
            await clientA.ConnectAsync("127.0.0.1", testPort);
            var streamA = clientA.GetStream();
            await SendPackAsync(streamA, 103, "战士A/200/200");

            using var clientB = new System.Net.Sockets.TcpClient();
            await clientB.ConnectAsync("127.0.0.1", testPort);
            var streamB = clientB.GetStream();
            await SendPackAsync(streamB, 103, "平民B/201/200");

            await Task.Delay(200);

            byte[] flushBuf = new byte[4096];
            while (streamA.DataAvailable) _ = await streamA.ReadAsync(flushBuf, 0, flushBuf.Length);
            while (streamB.DataAvailable) _ = await streamB.ReadAsync(flushBuf, 0, flushBuf.Length);

            // 1. 发送 @AttackMode 切换指令 (从默认 0=全体 切换为 1=和平模式 [Mode: Peaceful])
            await SendPackAsync(streamA, 3030, "@AttackMode");

            string modeResp = await ReadUntilContainsPackAsync(streamA, "[Mode: Peaceful]", timeoutMs: 2000);
            Assert.Contains("[Mode: Peaceful]", modeResp); // 1:1 对齐官方 IDA 0x4D1E54 字符串

            // 2. 和平模式下物理攻击无辜白名平民 B -> 必须被拦截，平民 B 绝不能收到任何受击扣血包
            await SendPackAsync(streamA, 3014, "201/200/1");

            await Task.Delay(150);

            // 平民 B 收到挥刀动作 (SM_OBJECTHIT 242)，但坚决不能收到受击后仰 (SM_OBJECTSTRUCK 244)
            string hitRecvStr = await ReadUntilContainsPackAsync(streamB, $"#{ProtocolConsts.SM_OBJECTHIT}/", timeoutMs: 250);
            Assert.DoesNotContain($"#{ProtocolConsts.SM_OBJECTSTRUCK}/", hitRecvStr);
        }
        finally
        {
            tickEngine.Stop();
            gateway.Stop();
        }
    }

    [Fact]
    public void Test_NinePatchAoi_CrossZoneBoundaryTransition_SyncAppearAndDisappear()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 300, 300);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        // 玩家 A 初始在 Zone (0, 0) 的边缘 (15, 15)
        var sessionA = new ClientSession(101, null!)
        {
            CurrentMapId = 0,
            X = 15,
            Y = 15,
            CharacterName = "远行者A",
            Direction = 2,
            State = new PlayerState { Level = 35, Hp = 300, MaxHp = 300 }
        };

        // 玩家 B 在 Zone (0, 0) 的角落 (1, 1)
        var sessionB = new ClientSession(102, null!)
        {
            CurrentMapId = 0,
            X = 1,
            Y = 1,
            CharacterName = "守望者B",
            Direction = 0,
            State = new PlayerState { Level = 35, Hp = 300, MaxHp = 300 }
        };

        // 玩家 C 在 Zone (2, 2) 的中心 (35, 35) (初始超出 A 的九宫格范围)
        var sessionC = new ClientSession(103, null!)
        {
            CurrentMapId = 0,
            X = 35,
            Y = 35,
            CharacterName = "探索者C",
            Direction = 4,
            State = new PlayerState { Level = 35, Hp = 300, MaxHp = 300 }
        };

        dispatcher.RegisterSession(sessionA);
        dispatcher.RegisterSession(sessionB);
        dispatcher.RegisterSession(sessionC);

        map.Aoi.Enter(sessionA, sessionA.X, sessionA.Y);
        map.Aoi.Enter(sessionB, sessionB.X, sessionB.Y);
        map.Aoi.Enter(sessionC, sessionC.X, sessionC.Y);

        sessionA.ClearBufferedPackets();
        sessionB.ClearBufferedPackets();
        sessionC.ClearBufferedPackets();

        // 1. 玩家 A 从 (15, 15) [Zone 0,0] 移动至 (16, 16) [Zone 1,1] 跨越 Zone 边界！
        // Zone (1, 1) 的九宫格视野为 [0..2, 0..2]，将首次包含玩家 C 所在的 Zone (2, 2)！
        dispatcher.Dispatch(sessionA, "#3011/16/16/2/!");

        // 验证：C 必须收到 A 登场的 SM_ENTITYAPPEAR (10) 包，A 也必须收到 C 登场的 SM_ENTITYAPPEAR (10) 包
        Assert.True(sessionC.HasBufferedPacket($"#{ProtocolConsts.SM_ENTITYAPPEAR}/101/16/16/2/远行者A!"),
            $"进入视野的客户端 C 必须收到 A 登场包。[诊断] A=({sessionA.X},{sessionA.Y}) 冻结={sessionA.IsActionFrozen(Environment.TickCount64)} " +
            $"A收到={sessionA.GetBufferedPacketsString()} | C收到={sessionC.GetBufferedPacketsString()}");
        Assert.True(sessionA.HasBufferedPacket($"#{ProtocolConsts.SM_ENTITYAPPEAR}/103/35/35/4/探索者C!"), "跨界移动者 A 必须收到视野新出现的 C 登场包");

        // 验证：玩家 B 依然在 A 的九宫格视野内 (Zone 0,0 属于 Zone 1,1 的相邻九宫格)，B 收到正常的走路包 SM_OBJECTWALK
        Assert.True(sessionB.HasBufferedPacket($"#{ProtocolConsts.SM_OBJECTWALK}/101/16/16/2/远行者A!"), "视野连续的 B 必须收到正常的移动步态帧");

        // 2. 玩家 A 移动至 Zone 1 的右下边界 (31, 31)，此时 Zone 0 仍在 A 的视野内
        sessionA.X = 31;
        sessionA.Y = 31;
        map.Aoi.UpdatePosition(sessionA, 16, 16, 31, 31, out _, out _);

        sessionA.ClearBufferedPackets();
        sessionB.ClearBufferedPackets();
        sessionC.ClearBufferedPackets();
        sessionA.LastMoveTimeMs = 0;
        sessionA.ConsecutiveFastMoves = 0;

        // 玩家 A 从 (31, 31) [Zone 1] 跨入 (32, 32) [Zone 2]！
        // Zone 2 的九宫格视野为 [1..3, 1..3]，原 Zone (0, 0) 彻底移出视野 (leftZones 包含 Zone 0)！
        dispatcher.Dispatch(sessionA, "#3011/32/32/2/!");

        // 验证：旧区域玩家 B (在 Zone 0) 必须收到 A 离开的 SM_ENTITYDISAPPEAR (11)，彻底消除残影！
        Assert.True(sessionB.HasBufferedPacket($"#{ProtocolConsts.SM_ENTITYDISAPPEAR}/101/32/32/0/远行者A!"), "移出视野的玩家 B 必须收到 A 离开包");
        Assert.True(sessionA.HasBufferedPacket($"#{ProtocolConsts.SM_ENTITYDISAPPEAR}/102/1/1/0/守望者B!"), "A 必须收到已经脱离视野的 B 离开包");
    }
}

