using System;

namespace Mir2Client.Game;

/// <summary>
/// 盛大官方 1.5 原版战士三大特技核心判定引擎 (IDA sub_49B8E0 / sub_49B310 / sub_49BC20 权威实证)
/// 严格 1:1 比特级还原:
///   1. 烈火剑法 (FireHit): 10 秒蓄力倒计时，8 秒技能冷却，官方合法 "双烈火" 核心时钟，爆发 260% 物理攻击 (sub_49B8E0)
///   2. 刺杀剑术 (Stabbing): 开关状态本地记忆 (AutoStab)，第二格隔位剑气完全无视目标物理防御与魔法盾减伤 (sub_49B310)
///   3. 野蛮冲撞 (ShoulderDash): 碰撞等级比拼 (Self.Level > Target.Level)，成功推开目标硬直 1 秒，撞墙或撞高等级自身反弹顿挫 1 秒 (sub_49BC20)
/// </summary>
public static class WarriorCombatEngine
{
    // ==================== 1. 烈火剑法 (FireHit, MagicId = 26) ====================
    public const int FireHitReadyDurationMs = 10000; // 蓄力持续 10 秒
    public const int FireHitCooldownMs = 8000;       // 技能 CD 8 秒
    public const double FireHitDamageMultiplier = 2.6; // 官方 260% 物理伤害爆发

    public sealed class FireHitTracker
    {
        public bool IsReady { get; private set; }
        public long ReadyStartTime { get; private set; }
        public long LastCastTime { get; private set; }

        /// <summary>
        /// 施放烈火剑法凝聚火焰 (CD 8 秒，蓄力状态维持 10 秒)
        /// </summary>
        public (bool success, string reason) Cast(long nowTick)
        {
            if (LastCastTime > 0 && nowTick - LastCastTime < FireHitCooldownMs)
            {
                int remainingSec = (int)Math.Ceiling((FireHitCooldownMs - (nowTick - LastCastTime)) / 1000.0);
                return (false, $"烈火剑法凝聚冷却中，还需 {remainingSec} 秒");
            }

            IsReady = true;
            ReadyStartTime = nowTick;
            LastCastTime = nowTick;
            return (true, "烈火剑法已蓄力！刀刃燃起炽热烈焰！");
        }

        /// <summary>
        /// 物理出刀结算，若处于蓄力期且未超时，打出 260% 烈火爆发
        /// </summary>
        public (bool isFireHit, int damage, string desc) ConsumeOnHit(int baseDamage, long nowTick)
        {
            if (!IsReady)
            {
                return (false, baseDamage, "普通物理攻击");
            }

            // 检查是否在 10 秒蓄力有效窗口内
            if (nowTick - ReadyStartTime <= FireHitReadyDurationMs)
            {
                IsReady = false;
                int fireDmg = (int)Math.Round(baseDamage * FireHitDamageMultiplier);
                return (true, fireDmg, "烈火剑法爆发！打出 2.6 倍烈焰烈火斩！");
            }

            // 蓄力超时火焰熄灭
            IsReady = false;
            return (false, baseDamage, "烈火蓄力已超时自然熄灭");
        }

        /// <summary>
        /// 判定当前是否处于合法的 "双烈火" 施法窗口期 (第 8.01 ~ 9.99 秒)
        /// </summary>
        public bool CanCastSecondFireHit(long nowTick)
        {
            if (!IsReady) return false;
            long elapsed = nowTick - ReadyStartTime;
            // 技能冷却已转好 (>= 8000ms) 且第一刀蓄力尚未超时 (<= 10000ms)
            return elapsed >= FireHitCooldownMs && elapsed <= FireHitReadyDurationMs;
        }
    }

    // ==================== 2. 刺杀剑术 (Stabbing, MagicId = 12) ====================
    public sealed class StabbingTracker
    {
        public bool AutoStabEnabled { get; set; } = true; // 官方默认开启刺杀开关

        public enum HitDistanceType
        {
            None,
            MeleeClose,  // 贴身第 1 格 (受常规防御与魔法盾减免)
            StabbingFar  // 隔位第 2 格 (剑气无视防御与魔法盾)
        }

        /// <summary>
        /// 判定攻击距离与刺杀剑气类型 (必须为正向 8 方向直线)
        /// </summary>
        public static HitDistanceType CheckHitDistance(int attackerX, int attackerY, int targetX, int targetY, int dir)
        {
            var offset = PlayerAuditEngine.DirOffsets[dir & 7];
            // 第 1 格: 正前方 1 格
            if (attackerX + offset.X == targetX && attackerY + offset.Y == targetY)
            {
                return HitDistanceType.MeleeClose;
            }
            // 第 2 格: 正前方 2 格
            if (attackerX + offset.X * 2 == targetX && attackerY + offset.Y * 2 == targetY)
            {
                return HitDistanceType.StabbingFar;
            }
            return HitDistanceType.None;
        }

        /// <summary>
        /// 刺杀剑术伤害结算:
        /// 若为隔位第 2 格且开启刺杀，无视目标物理防御 AC 与法师魔法盾减伤
        /// </summary>
        public static (int finalDamage, bool isArmorPiercing, string desc) CalculateDamage(
            int rawDamage, HitDistanceType distType, bool autoStab, int targetAc, double targetShieldReduction = 0.0)
        {
            if (distType == HitDistanceType.StabbingFar && autoStab)
            {
                // 隔位剑气：完全无视防御与护盾
                return (rawDamage, true, "刺杀剑气穿透！完全无视物理防御与魔法盾！");
            }

            // 常规结算：扣减物理防御，并受魔法盾减免
            int defDmg = Math.Max(1, rawDamage - targetAc);
            if (targetShieldReduction > 0.0 && targetShieldReduction < 1.0)
            {
                defDmg = (int)Math.Round(defDmg * (1.0 - targetShieldReduction));
            }

            return (defDmg, false, "常规近身攻击命中");
        }
    }

    // ==================== 3. 野蛮冲撞 (ShoulderDash, MagicId = 27) ====================
    public static class ShoulderDash
    {
        public const int CooldownMs = 3000;       // 冷却 3 秒
        public const int MpCost = 10;             // 蓝耗 10 点
        public const float StunDurationSec = 1.0f; // 眩晕硬直 1 秒

        public sealed class DashResult
        {
            public bool Success { get; set; }
            public int SelfFinalX { get; set; }
            public int SelfFinalY { get; set; }
            public bool TargetPushed { get; set; }
            public int TargetFinalX { get; set; }
            public int TargetFinalY { get; set; }
            public bool SelfStunned { get; set; }
            public float StunSec { get; set; }
            public string Message { get; set; } = string.Empty;
        }

        /// <summary>
        /// 执行野蛮冲撞判定
        /// </summary>
        public static DashResult ExecuteDash(
            int selfLevel, int selfX, int selfY, int dir,
            (bool hasEntity, int level, int x, int y)? obstacleEntity,
            Func<int, int, bool> isTileBlocked)
        {
            var offset = PlayerAuditEngine.DirOffsets[dir & 7];
            int step1X = selfX + offset.X;
            int step1Y = selfY + offset.Y;
            int step2X = selfX + offset.X * 2;
            int step2Y = selfY + offset.Y * 2;

            // 1. 检查第 1 格是否有地形阻挡 (撞墙反弹)
            if (isTileBlocked(step1X, step1Y))
            {
                return new DashResult
                {
                    Success = false,
                    SelfFinalX = selfX,
                    SelfFinalY = selfY,
                    SelfStunned = true,
                    StunSec = StunDurationSec,
                    Message = "冲撞撞击墙体障碍！受到碰撞反作用力陷入顿挫硬直！"
                };
            }

            // 2. 检查第 1 格是否有实体
            if (obstacleEntity != null && obstacleEntity.Value.hasEntity)
            {
                var target = obstacleEntity.Value;
                // 实体在第 1 格
                if (target.x == step1X && target.y == step1Y)
                {
                    // 等级比拼: Self.Level > Target.Level 才能撞开
                    if (selfLevel > target.level)
                    {
                        // 目标被推到第 2 格，自身进入第 1 格
                        int targetPushX = step2X;
                        int targetPushY = step2Y;

                        // 目标背后若受阻挡，目标就位第 1 格受撞硬直，自身保持原地
                        if (isTileBlocked(targetPushX, targetPushY))
                        {
                            return new DashResult
                            {
                                Success = true,
                                SelfFinalX = selfX,
                                SelfFinalY = selfY,
                                TargetPushed = false,
                                TargetFinalX = step1X,
                                TargetFinalY = step1Y,
                                SelfStunned = false,
                                Message = "目标身后受墙壁阻挡无法后退，撞击造成目标原地硬直！"
                            };
                        }

                        return new DashResult
                        {
                            Success = true,
                            SelfFinalX = step1X,
                            SelfFinalY = step1Y,
                            TargetPushed = true,
                            TargetFinalX = targetPushX,
                            TargetFinalY = targetPushY,
                            SelfStunned = false,
                            StunSec = StunDurationSec,
                            Message = $"等级压制成功！将目标撞飞至 ({targetPushX}, {targetPushY}) 并造成眩晕！"
                        };
                    }
                    else
                    {
                        // 等级不足或相等：自身反弹顿挫
                        return new DashResult
                        {
                            Success = false,
                            SelfFinalX = selfX,
                            SelfFinalY = selfY,
                            SelfStunned = true,
                            StunSec = StunDurationSec,
                            Message = "目标等级不低于自身，冲撞撞击失败！自身陷入反弹硬直！"
                        };
                    }
                }
            }

            // 3. 无障碍物：顺畅位移 2 格 (若第 2 格阻挡则停在第 1 格)
            if (isTileBlocked(step2X, step2Y))
            {
                return new DashResult
                {
                    Success = true,
                    SelfFinalX = step1X,
                    SelfFinalY = step1Y,
                    Message = "顺畅位移 1 格到达障碍边缘"
                };
            }

            return new DashResult
            {
                Success = true,
                SelfFinalX = step2X,
                SelfFinalY = step2Y,
                Message = $"野蛮冲撞狂暴突进！瞬间位移至 ({step2X}, {step2Y})！"
            };
        }
    }
}
