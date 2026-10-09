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
    // 对齐 Rust combat.rs：DEFAULT_COOLDOWN 200 ticks=10s，LIFESPAN 400 ticks=20s（@20TPS）。
    public const int FireHitReadyDurationMs = 20000; // 蓄力持续 20 秒
    public const int FireHitCooldownMs = 10000;      // 技能 CD 10 秒
    public const double FireHitDamageMultiplier = 2.6; // 满级 260% 物理伤害爆发

    public sealed class FireHitTracker
    {
        public bool IsReady { get; private set; }
        public long ReadyStartTime { get; private set; }
        public long LastCastTime { get; private set; }

        /// <summary>
        /// 施放烈火剑法凝聚火焰 (CD 10 秒，蓄力状态维持 20 秒)
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

            // 检查是否在 20 秒蓄力有效窗口内
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
        /// 判定当前是否处于合法的 "双烈火" 施法窗口期 (CD 转好 10s 后、蓄力 20s 超时前，对齐 Rust is_double_fire_hit_window)
        /// </summary>
        public bool CanCastSecondFireHit(long nowTick)
        {
            if (!IsReady) return false;
            long elapsed = nowTick - ReadyStartTime;
            // 技能冷却已转好 (>= 10000ms) 且第一刀蓄力尚未超时 (<= 20000ms)
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
        /// 若为隔位第 2 格且开启刺杀，无视目标物理防御 AC 与法师魔法盾减伤；
        /// 剑气按修炼等级缩放（对齐 Rust calc_stabbing_sec_power：Round(raw/(trainLv+2)*(skillLv+2))，trainLv 缺省 3）。
        /// </summary>
        public static (int finalDamage, bool isArmorPiercing, string desc) CalculateDamage(
            int rawDamage, HitDistanceType distType, bool autoStab, int targetAc, double targetShieldReduction = 0.0)
        {
            return CalculateDamage(rawDamage, distType, autoStab, targetAc, 3, 3, targetShieldReduction);
        }

        public static (int finalDamage, bool isArmorPiercing, string desc) CalculateDamage(
            int rawDamage, HitDistanceType distType, bool autoStab, int targetAc,
            int trainLevel, int skillLevel, double targetShieldReduction = 0.0)
        {
            if (distType == HitDistanceType.StabbingFar && autoStab)
            {
                // 隔位剑气：完全无视防御与护盾，但按等级缩放（0级/train3=40%，满级=100%）
                int divisor = Math.Max(1, trainLevel + 2);
                int multiplier = Math.Clamp(skillLevel, 0, 3) + 2;
                int thrust = Math.Max(1, (int)Math.Round((double)rawDamage / divisor * multiplier));
                return (thrust, true, "刺杀剑气穿透！完全无视物理防御与魔法盾！");
            }

            // 常规结算：扣减物理防御，并受魔法盾减免（服务端最小 0，见 Rust calc_hit_struck_damage）
            int defDmg = Math.Max(0, rawDamage - targetAc);
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
        public const float StunDurationSec = 0.8f; // 每格硬直 0.8 秒（Rust SHORT_WEAPON_STUN_PER_STEP_MS）

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
            public int SelfDamage { get; set; } // 撞墙反噬自伤（Rust calc_short_weapon_self_damage）
            public string Message { get; set; } = string.Empty;
        }

        /// <summary>
        /// 执行野蛮冲撞判定
        /// </summary>
        public static DashResult ExecuteDash(
            int selfLevel, int selfX, int selfY, int dir,
            (bool hasEntity, int level, int x, int y)? obstacleEntity,
            Func<int, int, bool> isTileBlocked,
            int skillLevel = 0, int roll20 = 0, int rollExtraDamage = 0)
        {
            // 对齐 Rust combat.rs 野蛮三式：
            // 距离 Max(2, sl+1)；推动阈值 diff+3*sl+6（roll20<[0,20) 成功）；
            // 硬直 800ms/格；撞墙自伤 5*(sl+1)+rollExtra。
            // 缺省 skillLevel=0/roll20=0 时退化为旧二格确定性行为（兼容既有调用）。
            int sl = Math.Clamp(skillLevel, 0, 3);
            int steps = Math.Max(2, sl + 1);
            var offset = PlayerAuditEngine.DirOffsets[dir & 7];

            bool TileFree(int s) => !isTileBlocked(selfX + offset.X * s, selfY + offset.Y * s);

            // 1. 第 1 格地形阻挡：撞墙反弹 + 自伤
            if (!TileFree(1))
            {
                return new DashResult
                {
                    Success = false,
                    SelfFinalX = selfX,
                    SelfFinalY = selfY,
                    SelfStunned = true,
                    StunSec = 0.8f,
                    SelfDamage = 5 * (sl + 1) + Math.Max(0, rollExtraDamage),
                    Message = "冲撞撞击墙体障碍！受到碰撞反作用力陷入顿挫硬直！"
                };
            }

            // 2. 第 1 格有实体：等级压制 + 概率推动
            if (obstacleEntity != null && obstacleEntity.Value.hasEntity)
            {
                var target = obstacleEntity.Value;
                if (target.x == selfX + offset.X && target.y == selfY + offset.Y)
                {
                    int diff = selfLevel - target.level;
                    int threshold = diff + 3 * sl + 6;
                    if (diff > 0 && roll20 < threshold)
                    {
                        // 目标沿冲撞方向被推至第 steps 格（遇阻则停前一格），自身进第 steps-1 格
                        int targetStep = steps;
                        while (targetStep > 1 && !TileFree(targetStep)) targetStep--;
                        int selfStep = Math.Max(1, targetStep - 1);
                        while (selfStep > 1 && !TileFree(selfStep)) selfStep--;
                        if (targetStep <= 1)
                        {
                            return new DashResult
                            {
                                Success = true,
                                SelfFinalX = selfX,
                                SelfFinalY = selfY,
                                TargetPushed = false,
                                TargetFinalX = selfX + offset.X,
                                TargetFinalY = selfY + offset.Y,
                                SelfStunned = false,
                                StunSec = 0.8f,
                                Message = "目标身后受墙壁阻挡无法后退，撞击造成目标原地硬直！"
                            };
                        }

                        return new DashResult
                        {
                            Success = true,
                            SelfFinalX = selfX + offset.X * selfStep,
                            SelfFinalY = selfY + offset.Y * selfStep,
                            TargetPushed = true,
                            TargetFinalX = selfX + offset.X * targetStep,
                            TargetFinalY = selfY + offset.Y * targetStep,
                            SelfStunned = false,
                            StunSec = 0.8f * targetStep,
                            Message = $"等级压制成功！将目标撞飞至 ({selfX + offset.X * targetStep}, {selfY + offset.Y * targetStep}) 并造成眩晕！"
                        };
                    }

                    return new DashResult
                    {
                        Success = false,
                        SelfFinalX = selfX,
                        SelfFinalY = selfY,
                        SelfStunned = true,
                        StunSec = 0.8f,
                        Message = "目标等级不低于自身，冲撞撞击失败！自身陷入反弹硬直！"
                    };
                }
            }

            // 3. 无障碍物：顺畅位移 steps 格（遇阻停前一格）
            int go = steps;
            while (go > 1 && !TileFree(go)) go--;
            return new DashResult
            {
                Success = true,
                SelfFinalX = selfX + offset.X * go,
                SelfFinalY = selfY + offset.Y * go,
                Message = go >= steps ? $"顺畅位移 {steps} 格" : "顺畅位移 1 格到达障碍边缘"
            };
        }
    }

    // ==================== 4. 半月弯刀 (HalfMoon / WideHit, MagicId = 25) ====================
    public static class HalfMoon
    {
        public const int MpCost = 7;

        /// <summary>
        /// 获取半月弯刀以攻击者为中心、朝向 dir 的扇面 4 个攻击目标瓦片坐标:
        /// 1. 正前方 (主目标, dir)
        /// 2. 正前方顺时针侧格 ((dir + 1) % 8)
        /// 3. 正前方逆时针侧格 ((dir + 7) % 8)
        /// 4. 侧面顺侧格 ((dir + 2) % 8)
        /// </summary>
        public static (int x, int y, bool isMain)[] GetAffectedTiles(int px, int py, int dir)
        {
            var tiles = new (int x, int y, bool isMain)[4];
            int d0 = dir & 7;
            int dR1 = (dir + 1) & 7;
            int dL1 = (dir + 7) & 7;
            int dR2 = (dir + 2) & 7;

            var off0 = PlayerAuditEngine.DirOffsets[d0];
            var offR1 = PlayerAuditEngine.DirOffsets[dR1];
            var offL1 = PlayerAuditEngine.DirOffsets[dL1];
            var offR2 = PlayerAuditEngine.DirOffsets[dR2];

            tiles[0] = (px + off0.X, py + off0.Y, true);      // 正前方主目标
            tiles[1] = (px + offR1.X, py + offR1.Y, false);   // 侧翼副目标 1
            tiles[2] = (px + offL1.X, py + offL1.Y, false);   // 侧翼副目标 2
            tiles[3] = (px + offR2.X, py + offR2.Y, false);   // 侧翼副目标 3
            return tiles;
        }

        /// <summary>
        /// 计算半月弯刀物理攻击伤害:
        /// 主目标承受 100% 伤害，副目标根据技能等级 (0~3 级) 衰减至 35%~50% 伤害
        /// </summary>
        public static int CalculateDamage(int rawDamage, bool isMainTarget, int skillLevel)
        {
            if (isMainTarget) return rawDamage;
            double ratio = skillLevel switch
            {
                1 => 0.40,
                2 => 0.45,
                3 => 0.50,
                _ => 0.35
            };
            return Math.Max(1, (int)Math.Round(rawDamage * ratio));
        }
    }
}
