using System;

namespace Mir2Client.Game;

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
            if (currentDura <= 0)
            {
                return (false, 0, 0, 0, "复活戒指已破损失效！无法触发神圣复生！");
            }

            int nextDura = currentDura - 1;
            return (true, maxHp, maxMp, nextDura, $"【复活戒指】神力觉醒！生命与魔法完全恢复！(戒指剩余持久: {nextDura})");
        }
    }

    // ==================== 3. 护身戒指 ====================
    public static class ProtectionRing
    {
        public static (int actualHpDamage, int consumedMp, int remainingMp) AbsorbDamage(
            int incomingDamage, int currentMp)
        {
            if (incomingDamage <= 0) return (0, 0, currentMp);

            if (currentMp >= incomingDamage)
            {
                // 蓝量完全抵消所有伤害，生命值不受任何损失
                return (0, incomingDamage, currentMp - incomingDamage);
            }
            else
            {
                // 蓝量耗尽，剩余伤害穿透扣除生命值
                int absorbed = currentMp;
                int penetrativeDamage = incomingDamage - currentMp;
                return (penetrativeDamage, absorbed, 0);
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
            if (!trimmed.StartsWith("@move", StringComparison.OrdinalIgnoreCase))
                return (false, 0, 0, "非传送指令");

            var parts = trimmed.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 3 || !int.TryParse(parts[1], out int tx) || !int.TryParse(parts[2], out int ty))
            {
                return (false, 0, 0, "传送指令格式错误，正确格式: @move x y");
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
}
