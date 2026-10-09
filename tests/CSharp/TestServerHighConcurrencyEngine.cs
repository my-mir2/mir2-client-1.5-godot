using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Mir2.Server.Core;
using Mir2.Server.Maps;
using Mir2.Server.Network;
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
}
