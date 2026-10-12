// ============================================================================
// Mir2 客户端 - 多人网络航位推测与弱网平滑阻尼插值引擎 (1:1 盛大 1.5 官方实证)
// 官方实证：IDA sub_461A5C (TActor.Walk), sub_461BF6 (TActor.Run), sub_461E80 (Shift)
// 解决丢包抖动、网络拉扯瞬移 (Rubber-banding) 与平滑预测过渡
// ============================================================================

using System;
using Godot;

namespace Mir2Client.Game
{
    /// <summary>
    /// 航位推测与平滑插值引擎
    /// </summary>
    public class DeadReckoningEngine
    {
        public const float TileWidth = 48.0f;
        public const float TileHeight = 32.0f;

        // 默认行走与奔跑单步时长 (对齐官方 1.5 动作帧节奏)
        public const float DefaultWalkDuration = 0.48f;
        public const float DefaultRunDuration = 0.40f;

        // 弱网容差与平滑参数
        public const float SnapThresholdDistance = 144.0f; // 超过 3 格直接瞬移校准
        public const float ReconciliationDecayRate = 20.0f; // 指数衰减阻尼系数 (约 150ms 吸收 95% 误差)
        public const float MaxPredictionTime = 0.45f;      // 弱网丢包最长航位推测时长

        /// <summary>
        /// 实体运动状态结构体
        /// </summary>
        public class EntityState
        {
            public int CurrentTileX { get; set; }
            public int CurrentTileY { get; set; }
            public int TargetTileX { get; set; }
            public int TargetTileY { get; set; }
            public byte Direction { get; set; }

            public bool IsMoving { get; set; }
            public bool IsRun { get; set; }
            public float MoveDuration { get; set; } = DefaultWalkDuration;
            public float MoveTimer { get; set; }

            // 屏幕/世界像素坐标
            public Vector2 StartWorldPos { get; set; }
            public Vector2 TargetWorldPos { get; set; }
            public Vector2 VisualPos { get; set; }

            // 弱网丢包误差平滑修正向量
            public Vector2 SmoothOffset { get; set; } = Vector2.Zero;

            // 航位推测状态
            public bool IsPredicting { get; set; }
            public float PredictionTimer { get; set; }
            public Vector2 Velocity { get; set; } = Vector2.Zero;
        }

        public static Vector2 TileToWorld(int tx, int ty)
        {
            return new Vector2(tx * TileWidth, ty * TileHeight);
        }

        public static Vector2I WorldToTile(Vector2 world)
        {
            return new Vector2I((int)MathF.Round(world.X / TileWidth), (int)MathF.Round(world.Y / TileHeight));
        }

        /// <summary>
        /// 初始化实体状态至指定瓦片
        /// </summary>
        public static void InitializeEntity(EntityState state, int tx, int ty, byte dir)
        {
            state.CurrentTileX = tx;
            state.CurrentTileY = ty;
            state.TargetTileX = tx;
            state.TargetTileY = ty;
            state.Direction = dir;
            state.IsMoving = false;
            state.IsRun = false;
            state.IsPredicting = false;
            state.MoveTimer = 0f;
            state.PredictionTimer = 0f;
            state.SmoothOffset = Vector2.Zero;
            state.Velocity = Vector2.Zero;

            Vector2 pos = TileToWorld(tx, ty);
            state.StartWorldPos = pos;
            state.TargetWorldPos = pos;
            state.VisualPos = pos;
        }

        /// <summary>
        /// 开启步进移动 (收到服务端 SM_WALK / SM_RUN 或本地输入指令)
        /// </summary>
        public static void StartMovement(EntityState state, int targetX, int targetY, byte dir, bool isRun)
        {
            Vector2 fromPos = state.VisualPos;
            Vector2 toPos = TileToWorld(targetX, targetY);

            state.CurrentTileX = state.TargetTileX;
            state.CurrentTileY = state.TargetTileY;
            state.TargetTileX = targetX;
            state.TargetTileY = targetY;
            state.Direction = dir;
            state.IsMoving = true;
            state.IsRun = isRun;
            state.IsPredicting = false;
            state.PredictionTimer = 0f;

            state.StartWorldPos = fromPos;
            state.TargetWorldPos = toPos;
            state.MoveDuration = isRun ? DefaultRunDuration : DefaultWalkDuration;
            state.MoveTimer = 0f;

            // 计算移动速度向量
            Vector2 delta = toPos - fromPos;
            state.Velocity = state.MoveDuration > 0f ? (delta / state.MoveDuration) : Vector2.Zero;
        }

        /// <summary>
        /// 权威服务端位置纠偏 (Reconciliation)，在弱网丢包恢复或位置重置时调用
        /// </summary>
        public static void ReconcileServerPosition(EntityState state, int serverX, int serverY, byte dir)
        {
            Vector2 serverPos = TileToWorld(serverX, serverY);
            Vector2 currentVisual = state.VisualPos + state.SmoothOffset;
            float dist = currentVisual.DistanceTo(serverPos);

            if (dist > SnapThresholdDistance)
            {
                // 超出阈值 (如传送、大距离位移)，硬重置
                InitializeEntity(state, serverX, serverY, dir);
            }
            else
            {
                // 误差在容差范围内：将当前视觉坐标的差值赋给 SmoothOffset，并平滑衰减
                // 使得视觉位置瞬间保持不变 (currentVisual = serverPos + SmoothOffset)，消除突兀瞬移跳跃
                state.CurrentTileX = serverX;
                state.CurrentTileY = serverY;
                state.TargetTileX = serverX;
                state.TargetTileY = serverY;
                state.Direction = dir;
                state.IsMoving = false;
                state.IsPredicting = false;
                state.StartWorldPos = serverPos;
                state.TargetWorldPos = serverPos;
                state.VisualPos = serverPos;

                state.SmoothOffset = currentVisual - serverPos;
            }
        }

        /// <summary>
        /// 本地主角权威服务端位置纠偏 (Player Reconciliation)
        /// 针对公网抖动与微小延迟拉扯，在 1 格容差以内保持平滑阻尼指数衰减，不打断行走；
        /// 超出容差（如传送、被撞退、撞墙硬阻挡）执行硬瞬移拉回
        /// </summary>
        public static void ReconcilePlayerPosition(EntityState state, int serverX, int serverY, byte dir, float snapThreshold = 64.0f)
        {
            Vector2 serverPos = TileToWorld(serverX, serverY);
            Vector2 currentVisual = state.VisualPos + state.SmoothOffset;
            float dist = currentVisual.DistanceTo(serverPos);

            if (dist > snapThreshold)
            {
                InitializeEntity(state, serverX, serverY, dir);
            }
            else
            {
                state.CurrentTileX = serverX;
                state.CurrentTileY = serverY;
                state.Direction = dir;

                if (state.IsMoving)
                {
                    state.StartWorldPos = serverPos;
                    state.VisualPos = serverPos;
                    state.MoveTimer = 0f;
                    state.MoveDuration = Math.Max(0.08f, state.MoveDuration - state.MoveTimer);
                }
                else
                {
                    state.TargetTileX = serverX;
                    state.TargetTileY = serverY;
                    state.VisualPos = serverPos;
                }

                state.SmoothOffset = currentVisual - serverPos;
            }
        }

        /// <summary>
        /// 弱网航位推测激活：步进结束但下一帧网络包未到达时，沿上一动作速度平滑外推
        /// </summary>
        public static void TriggerDeadReckoning(EntityState state)
        {
            if (state.Velocity.LengthSquared() > 0.01f && !state.IsPredicting)
            {
                state.IsPredicting = true;
                state.PredictionTimer = 0f;
            }
        }

        /// <summary>
        /// 每帧更新平滑插值与阻尼收敛
        /// </summary>
        public static void Update(EntityState state, float dt)
        {
            // 1. 步进移动插值
            if (state.IsMoving)
            {
                state.MoveTimer += dt;
                float progress = Math.Clamp(state.MoveTimer / state.MoveDuration, 0f, 1f);
                state.VisualPos = state.StartWorldPos.Lerp(state.TargetWorldPos, progress);

                if (progress >= 1.0f)
                {
                    state.IsMoving = false;
                    state.CurrentTileX = state.TargetTileX;
                    state.CurrentTileY = state.TargetTileY;
                    state.VisualPos = state.TargetWorldPos;
                }
            }
            // 2. 弱网丢包航位推测 (Dead Reckoning)
            else if (state.IsPredicting)
            {
                state.PredictionTimer += dt;
                if (state.PredictionTimer < MaxPredictionTime)
                {
                    // 匀速外推并逐渐衰减速度
                    float damp = 1.0f - (state.PredictionTimer / MaxPredictionTime);
                    state.VisualPos += state.Velocity * dt * damp;
                }
                else
                {
                    // 预测超时，停止外推，等待真实服务器报文
                    state.IsPredicting = false;
                    state.Velocity = Vector2.Zero;
                }
            }

            // 3. 误差平滑阻尼吸收 (指数衰减收敛至 0)
            if (state.SmoothOffset.LengthSquared() > 0.005f)
            {
                float decayFactor = MathF.Exp(-ReconciliationDecayRate * dt);
                state.SmoothOffset *= decayFactor;
                if (state.SmoothOffset.LengthSquared() < 0.01f)
                {
                    state.SmoothOffset = Vector2.Zero;
                }
            }
        }

        /// <summary>
        /// 获取当前最终用于屏幕渲染的视觉坐标
        /// </summary>
        public static Vector2 GetRenderPosition(EntityState state)
        {
            return state.VisualPos + state.SmoothOffset;
        }
    }
}
