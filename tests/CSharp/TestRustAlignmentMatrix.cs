using Xunit;
using Mir2Client.Game;

namespace Mir2Client.Tests;

/// <summary>
/// Rust 对照锁定测试：20tick 防连点语义（客户端侧 1000ms 窗口）与复合戒指 Shape 谓词
/// （对齐 mir2-my special_items.rs apply_ring_shape 152..160）。
/// TradePanelView.OnConfirm 需 Godot 运行时，防连点窗口以 TradeManager 语义测试覆盖：
/// 解锁后 1 秒内重复锁定仍允许（服务端按 tick 拒的是“已锁定态重复确认”，本地按钮已禁用）。
/// </summary>
public sealed class TestRustAlignmentMatrix
{
    [Fact]
    public void CompositeRingShapes_MatchRustMapping()
    {
        Assert.True(SpecialRings.HasTeleportRingShape(112));
        Assert.True(SpecialRings.HasTeleportRingShape(157));
        Assert.True(SpecialRings.HasTeleportRingShape(158));
        Assert.True(SpecialRings.HasTeleportRingShape(159));
        Assert.False(SpecialRings.HasTeleportRingShape(153));

        Assert.True(SpecialRings.HasOverloadRingShape(119));
        Assert.True(SpecialRings.HasOverloadRingShape(153));
        Assert.True(SpecialRings.HasOverloadRingShape(156));
        Assert.False(SpecialRings.HasOverloadRingShape(157));

        Assert.True(SpecialRings.HasMagicShieldRingShape(154));
        Assert.True(SpecialRings.HasMagicShieldRingShape(158));
        Assert.False(SpecialRings.HasMagicShieldRingShape(153));
        Assert.False(SpecialRings.HasMagicShieldRingShape(159));

        Assert.True(SpecialRings.HasProbeNecklaceShape(159));
        Assert.True(SpecialRings.HasDoubleSpeedShape(160));
        Assert.False(SpecialRings.HasDoubleSpeedShape(159));

        Assert.True(SpecialRings.HasFlameRingShape(152));
        Assert.True(SpecialRings.HasFlameRingShape(153));
        Assert.True(SpecialRings.HasFlameRingShape(157));
        Assert.False(SpecialRings.HasFlameRingShape(158));
    }

    [Fact]
    public void TradeRelock_AfterUnlock_AllowsFreshConfirm()
    {
        var tm = new TradeManager();
        var ps = new PlayerState { Gold = 1000000 };
        tm.StartTrade("传奇战士", 2001);
        Assert.True(tm.LockTrade());
        tm.UnlockTrade();
        Assert.False(tm.LocalLocked);
        Assert.True(tm.LockTrade());
    }
}
