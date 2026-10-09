using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Mir2.Server.Core;
using Mir2.Server.Maps;
using Mir2.Server.Network;
using System.Text;
using Xunit;

namespace Mir2Client.Tests;

public class TestServerHighConcurrencyEngine
{
    private sealed class MockPlayer
    {
        public int Id { get; init; }
        public string Name { get; init; } = "";
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
        var gateway = new TcpGateway(17001); // 测试网关端口
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
        int testPort = 17002;
        var aoi = new NinePatchAoi<ClientSession>(400, 400);
        var dispatcher = new Mir2.Server.Game.PacketDispatcher(aoi);
        var gateway = new TcpGateway(testPort);
        gateway.PacketReceivedHandler = (session, pkt) => dispatcher.Dispatch(session, pkt);
        gateway.Start();

        var tickEngine = new GameWorldTick(gateway);
        tickEngine.Start();

        try
        {
            using var client = new System.Net.Sockets.TcpClient();
            await client.ConnectAsync("127.0.0.1", testPort);
            var stream = client.GetStream();

            byte[] readBuf = new byte[2048];

            // 1. 客户端发送 CM_QUERYVERSION (2001)
            byte[] qv = Encoding.ASCII.GetBytes("#2001/0/0/0/0/!");
            await stream.WriteAsync(qv);
            await stream.FlushAsync();

            // 等待一个 100ms Tick 合包冲刷回传
            await Task.Delay(150);
            int readLen1 = await stream.ReadAsync(readBuf);
            string resp1 = Encoding.ASCII.GetString(readBuf, 0, readLen1);
            Assert.Contains("#500/", resp1); // SM_PASSOK (500)

            // 2. 客户端发送 CM_LOGIN (2) 账号密码登录
            byte[] loginPkt = Encoding.ASCII.GetBytes("#2/hero123/pass888/!");
            await stream.WriteAsync(loginPkt);
            await stream.FlushAsync();

            await Task.Delay(150);
            int readLen2 = await stream.ReadAsync(readBuf);
            string resp2 = Encoding.ASCII.GetString(readBuf, 0, readLen2);
            Assert.Contains("#530/", resp2); // SM_LOGINSUCCESS (530)

            // 3. 客户端发送 CM_QUERYCHRINFO (119) 查询角色
            byte[] queryPkt = Encoding.ASCII.GetBytes("#119/!");
            await stream.WriteAsync(queryPkt);
            await stream.FlushAsync();

            await Task.Delay(150);
            int readLen3 = await stream.ReadAsync(readBuf);
            string resp3 = Encoding.ASCII.GetString(readBuf, 0, readLen3);
            Assert.Contains($"#{ProtocolConsts.SM_QUERYCHAR_OK}/", resp3); // SM_QUERYCHAR_OK (523 官方实证)

            // 4. 客户端发送 CM_SELECTCHAR (103) 选择角色进入游戏
            byte[] selCharPkt = Encoding.ASCII.GetBytes("#103/hero123_战神!");
            await stream.WriteAsync(selCharPkt);
            await stream.FlushAsync();

            await Task.Delay(150);
            int readLen4 = await stream.ReadAsync(readBuf);
            string resp4 = Encoding.ASCII.GetString(readBuf, 0, readLen4);
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
        int testPort = 17003;
        var aoi = new NinePatchAoi<ClientSession>(400, 400);
        var dispatcher = new Mir2.Server.Game.PacketDispatcher(aoi);
        var gateway = new TcpGateway(testPort);
        gateway.PacketReceivedHandler = (session, pkt) => dispatcher.Dispatch(session, pkt);
        gateway.Start();

        var tickEngine = new GameWorldTick(gateway);
        tickEngine.Start();

        try
        {
            // 客户端 A (在比奇省中心 160, 160)
            using var clientA = new System.Net.Sockets.TcpClient();
            await clientA.ConnectAsync("127.0.0.1", testPort);
            var streamA = clientA.GetStream();
            await streamA.WriteAsync(Encoding.ASCII.GetBytes("#103/玩家A/160/160!")); // 选角进入并定位在 160, 160
            await streamA.FlushAsync();

            // 客户端 B (同屏邻近 162, 160)
            using var clientB = new System.Net.Sockets.TcpClient();
            await clientB.ConnectAsync("127.0.0.1", testPort);
            var streamB = clientB.GetStream();
            await streamB.WriteAsync(Encoding.ASCII.GetBytes("#103/玩家B/162/160!"));
            await streamB.FlushAsync();

            // 客户端 C (远端银杏山谷 330, 270)
            using var clientC = new System.Net.Sockets.TcpClient();
            await clientC.ConnectAsync("127.0.0.1", testPort);
            var streamC = clientC.GetStream();
            await streamC.WriteAsync(Encoding.ASCII.GetBytes("#103/远端玩家C/330/270!"));
            await streamC.FlushAsync();

            // 等待入场包消化
            await Task.Delay(200);

            // 清空各自的接收缓冲
            byte[] flushBuf = new byte[4096];
            while (streamA.DataAvailable) _ = await streamA.ReadAsync(flushBuf, 0, flushBuf.Length);
            while (streamB.DataAvailable) _ = await streamB.ReadAsync(flushBuf, 0, flushBuf.Length);
            while (streamC.DataAvailable) _ = await streamC.ReadAsync(flushBuf, 0, flushBuf.Length);

            // 客户端 A 发起行走动作 CM_WALK: 移动到 161, 160, 朝向 2 (右)
            byte[] walkPkt = Encoding.ASCII.GetBytes("#3011/161/160/2!");
            await streamA.WriteAsync(walkPkt);
            await streamA.FlushAsync();

            // 等待 100ms 心跳末尾合包冲刷下发广播
            await Task.Delay(150);

            // 1. 同屏客户端 B 必须收到客户端 A 的移动广播 SM_OBJECTWALK (241)
            Assert.True(streamB.DataAvailable, "同屏邻近客户端 B 必须收到九宫格广播");
            byte[] readB = new byte[2048];
            int lenB = await streamB.ReadAsync(readB, 0, readB.Length);
            string respB = Encoding.ASCII.GetString(readB, 0, lenB);
            Assert.Contains($"#{ProtocolConsts.SM_OBJECTWALK}/", respB);

            // 2. 远端客户端 C 必须未收到任何广播 (九宫格严格隔离，0 冗余下行带宽)
            Assert.False(streamC.DataAvailable, "远端客户端 C 坚决不能收到其他 Zone 的移动广播");
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
        int testPort = 17004;
        var aoi = new NinePatchAoi<ClientSession>(400, 400);
        var dispatcher = new Mir2.Server.Game.PacketDispatcher(aoi);
        var gateway = new TcpGateway(testPort);
        gateway.PacketReceivedHandler = (session, pkt) => dispatcher.Dispatch(session, pkt);
        gateway.Start();

        var tickEngine = new GameWorldTick(gateway);
        tickEngine.Start();

        try
        {
            using var clientA = new System.Net.Sockets.TcpClient();
            await clientA.ConnectAsync("127.0.0.1", testPort);
            var streamA = clientA.GetStream();
            await streamA.WriteAsync(Encoding.UTF8.GetBytes("#103/战士A/200/200!"));
            await streamA.FlushAsync();

            using var clientB = new System.Net.Sockets.TcpClient();
            await clientB.ConnectAsync("127.0.0.1", testPort);
            var streamB = clientB.GetStream();
            await streamB.WriteAsync(Encoding.UTF8.GetBytes("#103/道士B/201/200!"));
            await streamB.FlushAsync();

            await Task.Delay(200);

            byte[] flushBuf = new byte[4096];
            while (streamA.DataAvailable) _ = await streamA.ReadAsync(flushBuf, 0, flushBuf.Length);
            while (streamB.DataAvailable) _ = await streamB.ReadAsync(flushBuf, 0, flushBuf.Length);

            // 1. 尝试瞬移 (从 200, 200 瞬移到 250, 250) -> 触发服务端防外挂拉回
            byte[] cheatTeleportPkt = Encoding.UTF8.GetBytes("#3011/250/250/0!");
            await streamA.WriteAsync(cheatTeleportPkt);
            await streamA.FlushAsync();

            await Task.Delay(150);

            // 客户端 A 必须收到 SM_SENDUSERSTATE (250) 坐标强制重置为原本的 (200, 200)
            Assert.True(streamA.DataAvailable, "瞬移必须被服务器拉回并发送校正包");
            byte[] rollbackBuf = new byte[2048];
            int rollbackLen = await streamA.ReadAsync(rollbackBuf, 0, rollbackBuf.Length);
            string rollbackResp = Encoding.UTF8.GetString(rollbackBuf, 0, rollbackLen);
            Assert.Contains($"#{ProtocolConsts.SM_SENDUSERSTATE}/", rollbackResp);
            Assert.Contains("/200/200/", rollbackResp); // 坐标未被篡改，依然是 200, 200

            // 2. 战士 A 发起普通挥刀 CM_HIT (3014) 朝向 1
            byte[] hitPkt = Encoding.UTF8.GetBytes("#3014/201/200/1!");
            await streamA.WriteAsync(hitPkt);
            await streamA.FlushAsync();

            await Task.Delay(150);

            // 同屏道士 B 必须收到挥刀动作广播 SM_OBJECTHIT (242)
            Assert.True(streamB.DataAvailable, "同屏道士 B 必须收到挥刀动作广播");
            byte[] hitBuf = new byte[2048];
            int hitLen = await streamB.ReadAsync(hitBuf, 0, hitBuf.Length);
            string hitResp = Encoding.UTF8.GetString(hitBuf, 0, hitLen);
            Assert.Contains($"#{ProtocolConsts.SM_OBJECTHIT}/", hitResp);
            Assert.Contains("/200/200/1/战士A", hitResp);
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
        int testPort = 17005;
        var aoi = new NinePatchAoi<ClientSession>(400, 400);
        var dispatcher = new Mir2.Server.Game.PacketDispatcher(aoi);
        var gateway = new TcpGateway(testPort);
        gateway.PacketReceivedHandler = (session, pkt) => dispatcher.Dispatch(session, pkt);
        gateway.Start();

        var tickEngine = new GameWorldTick(gateway);
        tickEngine.Start();

        try
        {
            // 客户端 A: 法师 (300, 300)
            using var clientA = new System.Net.Sockets.TcpClient();
            await clientA.ConnectAsync("127.0.0.1", testPort);
            var streamA = clientA.GetStream();
            await streamA.WriteAsync(Encoding.UTF8.GetBytes("#103/法师A/300/300!"));
            await streamA.FlushAsync();

            // 客户端 B: 目标怪/玩家 (302, 300)
            using var clientB = new System.Net.Sockets.TcpClient();
            await clientB.ConnectAsync("127.0.0.1", testPort);
            var streamB = clientB.GetStream();
            await streamB.WriteAsync(Encoding.UTF8.GetBytes("#103/稻草人B/302/300!"));
            await streamB.FlushAsync();

            await Task.Delay(200);

            byte[] flushBuf = new byte[4096];
            while (streamA.DataAvailable) _ = await streamA.ReadAsync(flushBuf, 0, flushBuf.Length);
            while (streamB.DataAvailable) _ = await streamB.ReadAsync(flushBuf, 0, flushBuf.Length);

            // 1. 法师施展魔法盾 MagicId=31 (CM_SPELL 3017: targetUid/magicId/targetX/targetY)
            byte[] shieldPkt = Encoding.UTF8.GetBytes("#3017/0/31/300/300!");
            await streamA.WriteAsync(shieldPkt);
            await streamA.FlushAsync();

            await Task.Delay(150);

            // 法师自身收到 MP 扣除与状态同步 SM_MYSTATUS (708)
            Assert.True(streamA.DataAvailable, "施展魔法盾后必须同步法师自身血蓝");
            byte[] shieldRespBuf = new byte[2048];
            int shieldRespLen = await streamA.ReadAsync(shieldRespBuf, 0, shieldRespBuf.Length);
            string shieldResp = Encoding.UTF8.GetString(shieldRespBuf, 0, shieldRespLen);
            Assert.Contains($"#{ProtocolConsts.SM_OBJECTSPELL}/", shieldResp);
            Assert.Contains($"#{ProtocolConsts.SM_MYSTATUS}/", shieldResp);

            // 2. 等待公共 CD (550ms) 后，法师使用雷电术 MagicId=10 轰击稻草人B (302, 300)
            await Task.Delay(550);
            while (streamB.DataAvailable) _ = await streamB.ReadAsync(flushBuf, 0, flushBuf.Length);

            byte[] thunderPkt = Encoding.UTF8.GetBytes("#3017/0/10/302/300!");
            await streamA.WriteAsync(thunderPkt);
            await streamA.FlushAsync();

            await Task.Delay(150);

            // 同屏稻草人 B 必须收到施法动作 SM_OBJECTSPELL (243) 与受击后仰 SM_OBJECTSTRUCK (244)
            Assert.True(streamB.DataAvailable, "同屏稻草人 B 必须收到雷电轰击受击广播");
            byte[] thunderBBuf = new byte[2048];
            int thunderBLen = await streamB.ReadAsync(thunderBBuf, 0, thunderBBuf.Length);
            string thunderBResp = Encoding.UTF8.GetString(thunderBBuf, 0, thunderBLen);
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
        int testPort = 17006;
        var mapManager = new Mir2.Server.Maps.MapManager();
        var dispatcher = new Mir2.Server.Game.PacketDispatcher(mapManager);
        var gateway = new TcpGateway(testPort);
        gateway.PacketReceivedHandler = (session, pkt) => dispatcher.Dispatch(session, pkt);
        gateway.Start();

        var tickEngine = new GameWorldTick(gateway);
        tickEngine.Start();

        try
        {
            // 客户端连入比奇省 (99, 100)，紧邻水面阻挡 (100, 100)
            using var client = new System.Net.Sockets.TcpClient();
            await client.ConnectAsync("127.0.0.1", testPort);
            var stream = client.GetStream();
            await stream.WriteAsync(Encoding.UTF8.GetBytes("#103/探索者/99/100!"));
            await stream.FlushAsync();

            await Task.Delay(200);

            byte[] flushBuf = new byte[4096];
            while (stream.DataAvailable) _ = await stream.ReadAsync(flushBuf, 0, flushBuf.Length);

            // 1. 尝试向右走向水面阻挡 (100, 100, 朝向 2)
            byte[] walkIntoWaterPkt = Encoding.UTF8.GetBytes("#3011/100/100/2!");
            await stream.WriteAsync(walkIntoWaterPkt);
            await stream.FlushAsync();

            await Task.Delay(150);

            // 服务端物理阻挡必须生效，拉回原坐标 (99, 100)
            Assert.True(stream.DataAvailable, "撞入阻挡格子必须被服务器立即拉回");
            byte[] rollbackBuf = new byte[2048];
            int rollbackLen = await stream.ReadAsync(rollbackBuf, 0, rollbackBuf.Length);
            string rollbackResp = Encoding.UTF8.GetString(rollbackBuf, 0, rollbackLen);
            Assert.Contains($"#{ProtocolConsts.SM_SENDUSERSTATE}/", rollbackResp);
            Assert.Contains("/99/100/", rollbackResp); // 坐标未被篡改，依然是 99, 100

            // 2. 尝试向下方无阻挡的自由区域移动 (99, 101, 朝向 4)
            await Task.Delay(600); // 移动冷却
            while (stream.DataAvailable) _ = await stream.ReadAsync(flushBuf, 0, flushBuf.Length);

            byte[] validWalkPkt = Encoding.UTF8.GetBytes("#3011/99/101/4!");
            await stream.WriteAsync(validWalkPkt);
            await stream.FlushAsync();

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
}
