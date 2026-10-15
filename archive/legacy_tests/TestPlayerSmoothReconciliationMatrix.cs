// ============================================================================
// Mir2 客户端 - 主角本地航向推算与弱网纠偏平滑阻尼插值测试矩阵
// (TestPlayerSmoothReconciliationMatrix)
// 实证验证《工程认知公理 4.0》中揭示的“公网弱网高延迟下频繁拉扯抽搐”坏账的修复效果：
// 1. 1 格容差内位置纠偏零跳跃，通过 SmoothOffset 指数衰减阻尼无感贴合
// 2. 超出容差绝对硬瞬移拉回，打断动画并恢复站立
// 3. 移动进行中的微调保持位移连续性
// ============================================================================

using System;
using Godot;
using Mir2Client.Game;
using Xunit;

namespace Mir2Client.Tests;

public class TestPlayerSmoothReconciliationMatrix
{
    [Fact]
    public void Test_ReconcilePosition_WithinTolerance_ZeroVisualJumpAndSmoothDamping()
    {
        var state = new DeadReckoningEngine.EntityState();
        DeadReckoningEngine.InitializeEntity(state, 10, 10, 2); // 世界坐标 (480, 320)

        Assert.Equal(new Vector2(480, 320), state.VisualPos);
        Assert.Equal(new Vector2(480, 320), DeadReckoningEngine.GetRenderPosition(state));
        Assert.Equal(Vector2.Zero, state.SmoothOffset);

        // 模拟客户端由于网络延时，当前视觉停留在 (488, 324) (偏移约 9 像素，小于 64 像素容差)
        state.VisualPos = new Vector2(488, 324);
        var initialVisualPos = DeadReckoningEngine.GetRenderPosition(state); // (488, 324)

        // 服务端下发权威校准包 (10, 10)，方向 2
        DeadReckoningEngine.ReconcilePlayerPosition(state, 10, 10, 2, snapThreshold: 64.0f);

        // 关键物理断言：纠偏发生后的视觉渲染坐标必须严格连续，杜绝突兀瞬间跳跃！
        var immediateRenderPos = DeadReckoningEngine.GetRenderPosition(state);
        Assert.Equal(initialVisualPos.X, immediateRenderPos.X, 0.01f);
        Assert.Equal(initialVisualPos.Y, immediateRenderPos.Y, 0.01f);
        Assert.Equal(new Vector2(8, 4), state.SmoothOffset);

        // 模拟时间推进 (调用 Update 驱动阻尼衰减)
        DeadReckoningEngine.Update(state, 0.05f); // 50ms 后
        Assert.True(state.SmoothOffset.Length() < 8.0f, "SmoothOffset 必须开始阻尼指数衰减");

        DeadReckoningEngine.Update(state, 0.15f); // 再推进 150ms (总计 200ms)
        DeadReckoningEngine.Update(state, 0.10f); // 再推进 100ms (总计 300ms)

        // 300ms 后误差必须完全收敛至 0，最终渲染坐标完美贴合服务端权威瓦片 (480, 320)
        Assert.Equal(Vector2.Zero, state.SmoothOffset);
        Assert.Equal(new Vector2(480, 320), DeadReckoningEngine.GetRenderPosition(state));
    }

    [Fact]
    public void Test_ReconcilePosition_BeyondTolerance_HardSnapAndReset()
    {
        var state = new DeadReckoningEngine.EntityState();
        DeadReckoningEngine.InitializeEntity(state, 10, 10, 2);

        // 服务端下发大距离拉回校准 (30, 30) (距离超过 SnapToleranceDistance 64 像素)
        DeadReckoningEngine.ReconcilePlayerPosition(state, 30, 30, 4, snapThreshold: 64.0f);

        // 关键断言：超出容差必须触发绝对硬瞬移
        Assert.Equal(30, state.CurrentTileX);
        Assert.Equal(30, state.CurrentTileY);
        Assert.Equal(4, state.Direction);
        Assert.Equal(Vector2.Zero, state.SmoothOffset);
        Assert.Equal(new Vector2(30 * 48, 30 * 32), DeadReckoningEngine.GetRenderPosition(state));
        Assert.False(state.IsMoving);
    }

    [Fact]
    public void Test_MovingReconcile_MaintainsMotionContinuity()
    {
        var state = new DeadReckoningEngine.EntityState();
        DeadReckoningEngine.InitializeEntity(state, 10, 10, 2);

        // 启动向 (11, 10) 移动
        DeadReckoningEngine.StartMovement(state, 11, 10, 2, false);
        Assert.True(state.IsMoving);

        // 步进移动半程 (0.24s)
        DeadReckoningEngine.Update(state, 0.24f);
        var halfWayPos = DeadReckoningEngine.GetRenderPosition(state);
        Assert.True(halfWayPos.X > 480 && halfWayPos.X < 528);

        // 在半程中收到微小校准 (10, 10)
        DeadReckoningEngine.ReconcilePlayerPosition(state, 10, 10, 2, snapThreshold: 64.0f);

        // 移动状态不应被粗暴破坏，视觉位置无突变
        var afterReconcilePos = DeadReckoningEngine.GetRenderPosition(state);
        Assert.Equal(halfWayPos.X, afterReconcilePos.X, 0.1f);
        Assert.Equal(halfWayPos.Y, afterReconcilePos.Y, 0.1f);
    }
}
