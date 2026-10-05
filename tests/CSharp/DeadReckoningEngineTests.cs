// ============================================================================
// Mir2 客户端 - 多人网络航位推测与弱网平滑阻尼插值引擎单元测试
// 官方实证：IDA sub_461A5C (TActor.Walk), sub_461BF6 (TActor.Run), sub_461E80 (Shift)
// ============================================================================

using System;
using Godot;
using Mir2Client.Game;
using Xunit;

namespace Mir2Client.Tests
{
    public class DeadReckoningEngineTests
    {
        [Fact]
        public void Test_WalkMovement_SmoothInterpolation()
        {
            var state = new DeadReckoningEngine.EntityState();
            DeadReckoningEngine.InitializeEntity(state, 10, 10, 2);

            Assert.Equal(new Vector2(480f, 320f), state.VisualPos);
            Assert.False(state.IsMoving);

            // 走向 (11, 10)
            DeadReckoningEngine.StartMovement(state, 11, 10, 2, false);
            Assert.True(state.IsMoving);
            Assert.False(state.IsRun);
            Assert.Equal(DeadReckoningEngine.DefaultWalkDuration, state.MoveDuration);

            // 推进半程时间
            float halfDuration = state.MoveDuration * 0.5f;
            DeadReckoningEngine.Update(state, halfDuration);

            // 视觉坐标应正好在两瓦片中点 (504, 320)
            Assert.True(state.IsMoving);
            Assert.Equal(504f, state.VisualPos.X, 0.1f);
            Assert.Equal(320f, state.VisualPos.Y, 0.1f);

            // 推进完成
            DeadReckoningEngine.Update(state, halfDuration + 0.05f);
            Assert.False(state.IsMoving);
            Assert.Equal(11, state.CurrentTileX);
            Assert.Equal(10, state.CurrentTileY);
            Assert.Equal(new Vector2(528f, 320f), state.VisualPos);
        }

        [Fact]
        public void Test_RunMovement_DurationAndTwoTiles()
        {
            var state = new DeadReckoningEngine.EntityState();
            DeadReckoningEngine.InitializeEntity(state, 20, 20, 4);

            // 奔跑 2 格到 (20, 22)
            DeadReckoningEngine.StartMovement(state, 20, 22, 4, true);
            Assert.True(state.IsMoving);
            Assert.True(state.IsRun);
            Assert.Equal(DeadReckoningEngine.DefaultRunDuration, state.MoveDuration);

            // 完整走完
            DeadReckoningEngine.Update(state, state.MoveDuration + 0.01f);
            Assert.False(state.IsMoving);
            Assert.Equal(20, state.CurrentTileX);
            Assert.Equal(22, state.CurrentTileY);
            Assert.Equal(new Vector2(20 * 48f, 22 * 32f), state.VisualPos);
        }

        [Fact]
        public void Test_Reconciliation_SmoothDamping_NoVisualSnap()
        {
            var state = new DeadReckoningEngine.EntityState();
            DeadReckoningEngine.InitializeEntity(state, 10, 10, 0);

            // 假设客户端当前视觉由于网络延迟漂移在 (485, 325)
            state.VisualPos = new Vector2(485f, 325f);

            // 服务端下发权威矫正 (10, 10)，对应 (480, 320)
            DeadReckoningEngine.ReconcileServerPosition(state, 10, 10, 0);

            // 关键验证：纠偏瞬间的 RenderPosition 必须与原视觉位置无缝贴合，杜绝突兀瞬间跳跃
            var renderPosImmediate = DeadReckoningEngine.GetRenderPosition(state);
            Assert.Equal(485f, renderPosImmediate.X, 0.01f);
            Assert.Equal(325f, renderPosImmediate.Y, 0.01f);
            Assert.Equal(new Vector2(5f, 5f), state.SmoothOffset);

            // 随着时间推移，阻尼衰减将 SmoothOffset 吸收收敛至 0
            DeadReckoningEngine.Update(state, 0.1f);
            Assert.True(state.SmoothOffset.Length() < 5f);

            DeadReckoningEngine.Update(state, 0.2f);
            // 300ms 后误差几乎归零，视觉完全平滑收敛到权威 (480, 320)
            var renderPosFinal = DeadReckoningEngine.GetRenderPosition(state);
            Assert.Equal(480f, renderPosFinal.X, 0.1f);
            Assert.Equal(320f, renderPosFinal.Y, 0.1f);
            Assert.Equal(Vector2.Zero, state.SmoothOffset);
        }

        [Fact]
        public void Test_Reconciliation_FarDistance_HardSnap()
        {
            var state = new DeadReckoningEngine.EntityState();
            DeadReckoningEngine.InitializeEntity(state, 10, 10, 0);

            // 远距离跳跃 (如传送或大跨步过图) 超过 SnapThresholdDistance (144)
            DeadReckoningEngine.ReconcileServerPosition(state, 30, 30, 0);

            Assert.Equal(30, state.CurrentTileX);
            Assert.Equal(30, state.CurrentTileY);
            Assert.Equal(new Vector2(30 * 48f, 30 * 32f), state.VisualPos);
            Assert.Equal(Vector2.Zero, state.SmoothOffset);
        }

        [Fact]
        public void Test_DeadReckoning_PredictionExtrapolation()
        {
            var state = new DeadReckoningEngine.EntityState();
            DeadReckoningEngine.InitializeEntity(state, 10, 10, 2);

            // 启动移动
            DeadReckoningEngine.StartMovement(state, 11, 10, 2, false);
            // 走完步进
            DeadReckoningEngine.Update(state, state.MoveDuration);
            Assert.False(state.IsMoving);

            // 弱网触发航位推测
            DeadReckoningEngine.TriggerDeadReckoning(state);
            Assert.True(state.IsPredicting);

            Vector2 posBefore = state.VisualPos;
            // 预测推移 0.1 秒
            DeadReckoningEngine.Update(state, 0.1f);
            Assert.True(state.VisualPos.X > posBefore.X); // 沿原有朝向匀速外推

            // 预测超时 0.5 秒后自动停止外推，等待真实报文
            DeadReckoningEngine.Update(state, 0.5f);
            Assert.False(state.IsPredicting);
        }
    }
}
