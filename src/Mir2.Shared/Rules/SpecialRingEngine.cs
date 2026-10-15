using System;

namespace Mir2.Shared.Rules;

/// <summary>
/// 盛大官方 1.5 七大特戒核心规则与被动触发引擎 (纯 C# 实现)
/// 严格 1:1 还原经典传奇特戒物理真值:
///   1. 麻痹戒指 (Paralysis): 物理攻击命中时 1/15 几率 (约 6.7%) 触发石化麻痹 5 秒 (IDA sub_486DF0)
///   2. 复活戒指 (Revival): 角色致死瞬间满血满蓝原地满状态复活，戒指持久扣减 1 点 (IDA sub_4871A0)
///   3. 护身戒指 (Protection): 受到所有伤害优先扣减 MP，MP 归零前 HP 恒定不受损 (IDA sub_4872C0)
///   4. 隐身戒指 (Invisibility): 物理攻击前处于怪物隐身状态，出刀或施法破隐 2 秒
///   5. 传送戒指 (Teleport): 聊天栏输入 "@move x y" 并在佩戴传送戒指时触发全图瞬移
/// </summary>
public static class SpecialRingEngine
{
    // ==================== 1. 麻痹戒指 ====================
    public static class ParalysisRing
    {
        public const int ParalyzeDurationSec = 5;
        public const int TriggerDenominator = 15; // 1/15 几率

        public static bool CheckTrigger(Random rng)
        {
            return rng.Next(TriggerDenominator) == 0;
        }
    }

    // ==================== 2. 复活戒指 ====================
    public static class RevivalRing
    {
        public static (bool revived, int newHp, int newMp, int remainingDura, string message) ProcessFatalDamage(
            int currentDura, int maxHp, int maxMp)
        {
            return ProcessFatalDamage(currentDura, maxHp, maxMp, 0, 0, 0);
        }

        public static (bool revived, int newHp, int newMp, int remainingDura, string message) ProcessFatalDamage(
            int currentDura, int maxHp, int maxMp, long lastReviveTick, long nowTick, int cooldownSec = 30)
        {
            if (currentDura <= 0)
            {
                return (false, 0, 0, 0, "复活戒指已损坏。");
            }

            if (lastReviveTick > 0 && (nowTick - lastReviveTick) < cooldownSec * 1000L)
            {
                return (false, 0, 0, currentDura, "复活戒指正在冷却中。");
            }

            int nextDura = currentDura - 1;
            return (true, maxHp, maxMp, nextDura, "The Ring made you live");
        }
    }

    // ==================== 3. 护身戒指 ====================
    public static class ProtectionRing
    {
        public const double MpAbsorbRatio = 1.5; // 官方 M2Server sub_4BE3FC @ 0x4BE40B (flt_4BE508 = 1.5)

        /// <summary>
        /// 盛大官方 1.5 护身戒指伤害吸收与空蓝穿透精算 (sub_4BE3FC)
        /// 规则: 受到伤害优先扣除 MP，扣蓝比例为 ROUND(damage * 1.5)；
        /// 若 MP 不足，扣除全部 MP，剩余未吸收伤害按照 damage - (MP / 1.5) 穿透扣除 HP。
        /// </summary>
        public static (int actualHpDamage, int consumedMp, int remainingMp) AbsorbDamage(
            int incomingDamage, int currentMp)
        {
            if (incomingDamage <= 0) return (0, 0, currentMp);

            int mpNeeded = (int)Math.Round(incomingDamage * MpAbsorbRatio);

            if (currentMp >= mpNeeded)
            {
                // 蓝量充足，完全抵扣伤害，HP 不受任何损失
                return (0, mpNeeded, currentMp - mpNeeded);
            }
            else
            {
                // 蓝量耗尽，已抵扣伤害 = Floor(currentMp / 1.5)，余量穿透扣除生命值
                int absorbedDmg = (int)(currentMp / MpAbsorbRatio);
                int penetrativeDamage = Math.Max(0, incomingDamage - absorbedDmg);
                return (penetrativeDamage, currentMp, 0);
            }
        }
    }

    // ==================== 4. 隐身戒指 ====================
    public static class InvisibilityRing
    {
        public const int BreakDurationSec = 2; // 出刀或施法破隐 2 秒

        public static bool IsInvisible(bool isWearing, long lastAttackTick, long nowTick)
        {
            if (!isWearing) return false;
            // 距离上次攻击超过 2 秒即恢复隐身状态
            return (nowTick - lastAttackTick) >= (BreakDurationSec * 1000);
        }
    }

    // ==================== 5. 传送戒指 ====================
    public static class TeleportRing
    {
        public static (bool valid, int targetX, int targetY, string error) ParseCommand(
            string cmd, int mapWidth, int mapHeight, Func<int, int, bool> isTileBlocked)
        {
            if (string.IsNullOrWhiteSpace(cmd))
                return (false, 0, 0, "指令为空");

            string trimmed = cmd.Trim();
            if (!trimmed.StartsWith("@move", StringComparison.OrdinalIgnoreCase) &&
                !trimmed.StartsWith("@传送", StringComparison.OrdinalIgnoreCase) &&
                !trimmed.StartsWith("@飞", StringComparison.OrdinalIgnoreCase))
                return (false, 0, 0, "非传送指令");

            var parts = trimmed.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 3 || !int.TryParse(parts[1], out int tx) || !int.TryParse(parts[2], out int ty))
            {
                return (false, 0, 0, "传送指令格式错误，正确格式: @move x y 或 @传送 x y");
            }

            if (tx < 0 || tx >= mapWidth || ty < 0 || ty >= mapHeight)
            {
                return (false, 0, 0, $"目标坐标 ({tx},{ty}) 超出当前地图边界");
            }

            if (isTileBlocked(tx, ty))
            {
                return (false, 0, 0, $"目标坐标 ({tx},{ty}) 为不可通行的障碍墙体");
            }

            return (true, tx, ty, string.Empty);
        }
    }

    // ==================== 6. 火焰戒指 ====================
    public static class FlameRing
    {
        public const int GrantedMagicId = 1; // 火球术

        /// <summary>
        /// 盛大官方 1.5 规则: 战士(0)或道士(2)佩戴火焰戒指，获得运转火球术施法权限
        /// </summary>
        public static bool CanCastFireball(int job, bool hasFlameRing)
        {
            if (job == 1) return true; // 法师本职
            return hasFlameRing;
        }
    }

    // ==================== 7. 防御戒指 ====================
    public static class DefenseRing
    {
        public const int GrantedMagicId = 2; // 治愈术

        /// <summary>
        /// 盛大官方 1.5 规则: 战士(0)或法师(1)佩戴防御戒指，获得运转神圣治愈术施法权限
        /// </summary>
        public static bool CanCastHealing(int job, bool hasHealingRing)
        {
            if (job == 2) return true; // 道士本职
            return hasHealingRing;
        }
    }

    // ==================== 8. 超负载戒指 ====================
    public static class LoadRing
    {
        public static (int maxBag, int maxWear, int maxHand) ApplyLoadDoubling(
            int baseBag, int baseWear, int baseHand, bool hasLoadRing)
        {
            if (hasLoadRing)
            {
                return (baseBag * 2, baseWear * 2, baseHand * 2);
            }
            return (baseBag, baseWear, baseHand);
        }
    }

    /// <summary>
    /// 盛大官方 1.5 特戒跨职业技能权限统一裁决
    /// </summary>
    public static (bool allowed, string failReason) ValidateRingSkillPermission(
        int job, int magicId, bool hasFlameRing, bool hasHealingRing)
    {
        if (magicId == FlameRing.GrantedMagicId && job != 1 && !hasFlameRing)
        {
            return (false, "需要佩戴火焰戒指方可施放。");
        }
        if (magicId == DefenseRing.GrantedMagicId && job != 2 && !hasHealingRing)
        {
            return (false, "需要佩戴防御戒指方可施放。");
        }
        return (true, string.Empty);
    }
}

