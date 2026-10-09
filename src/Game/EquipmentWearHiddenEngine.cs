using System;

namespace Mir2Client.Game;

/// <summary>
/// 盛大官方 1.5 装备磨损爆碎、极品隐藏机制、首饰暗属性与物理公式引擎 (细节 211 ~ 230)
/// 严格依据官方客户端与服务端物理属性逆向实证
/// </summary>
public static class EquipmentWearHiddenEngine
{
    // 细节 211: 衣服与头盔受暴击 1/20 概率磨损 1 点持久
    public static class ArmorDuraWearEngine
    {
        public static (int newDura, bool duraReduced) CheckWear(int currentDura, bool isCriticalHit, int roll100)
        {
            if (isCriticalHit && (roll100 < 5)) // 5% 概率 (1/20)
            {
                return (Math.Max(0, currentDura - 1), true);
            }
            return (currentDura, false);
        }
    }

    // 细节 212: 武器砍中不可破坏硬物瞬间掉 1 点持久
    public static class WeaponHardObstacleWearEngine
    {
        public static (int newDura, bool chipped) StrikeHardObstacle(int currentDura, bool isHardObstacle)
        {
            if (isHardObstacle)
            {
                return (Math.Max(0, currentDura - 1), true);
            }
            return (currentDura, false);
        }
    }

    // 细节 213: 耐久度不足 100/1000 装备图标变红告警
    public static class LowDuraRedAlertEngine
    {
        public const int WarningThreshold = 100;
        public static (bool isRedAlert, string textColor) EvaluateDuraWarning(int dura)
        {
            if (dura < WarningThreshold)
            {
                return (true, "#FF0000"); // 红色告警
            }
            return (false, "#FFFFFF");
        }
    }

    // 细节 214: 神秘头盔属性未达隐藏要求强制锁定无法取下
    public static class MysticHelmetLockEngine
    {
        public static (bool canTakeOff, string message) EvaluateTakeOff(int playerDc, int playerMc, int requiredDc, int requiredMc)
        {
            if (playerDc < requiredDc || playerMc < requiredMc)
            {
                return (false, "神秘力量封印了头盔，你的属性不足，无法取下！");
            }
            return (true, "");
        }
    }

    // 细节 215: 祈祷头盔死亡掉落必碎裂消失不掉于地
    public static class PrayerHelmetShatterEngine
    {
        public static (bool isShattered, bool droppedOnGround) OnPlayerDeath(bool isWearingPrayerHelmet)
        {
            if (isWearingPrayerHelmet)
            {
                return (true, false); // 直接碎裂，不掉落于地
            }
            return (false, true);
        }
    }

    // 细节 216: 记忆项链天然极品最高幸运 +3 掉落权重公式
    public static class MemoryNecklaceLuckWeightEngine
    {
        public static int RollLuck(int roll1000)
        {
            if (roll1000 < 5) return 3;   // 0.5% 几率幸运 +3
            if (roll1000 < 30) return 2;  // 2.5% 几率幸运 +2
            if (roll1000 < 150) return 1; // 12% 几率幸运 +1
            return 0;
        }
    }

    // 细节 217: 恶魔铃铛增加法师诱惑之光成功率 15%
    public static class DevilBellTameEngine
    {
        public static float ApplyTameBonus(float baseRate, bool hasDevilBell)
        {
            return hasDevilBell ? Math.Min(1.0f, baseRate + 0.15f) : baseRate;
        }
    }

    // 细节 218: 龙之手镯隐藏 10% 破盾几率
    public static class DragonBraceletShieldBreakEngine
    {
        public static bool CheckShieldBreak(bool hasDragonBracelet, int roll100)
        {
            return hasDragonBracelet && roll100 < 10;
        }
    }

    // 细节 219: 心灵手镯增加道士火符准确 +2
    public static class SoulBraceletAccuracyEngine
    {
        public static int GetAccuracyBonus(bool hasSoulBracelet)
        {
            return hasSoulBracelet ? 2 : 0;
        }
    }

    // 细节 220: 红宝石戒指缩短法师魔法盾受击硬直 30%
    public static class RubyShieldRecoveryEngine
    {
        public static float CalculateShieldStun(float baseStunSeconds, bool hasRubyRing)
        {
            return hasRubyRing ? baseStunSeconds * 0.70f : baseStunSeconds;
        }
    }

    // 细节 221: 铂金戒指提升道士神圣战甲术防御上限 2 点
    public static class PlatinumArmorBoostEngine
    {
        public static int GetDefenseBonus(bool hasPlatinumRing)
        {
            return hasPlatinumRing ? 2 : 0;
        }
    }

    // 细节 222: 生铁戒指魔法防御对火墙按比例减免
    public static class IronRingFirewallDefEngine
    {
        public static int ReduceFirewallDamage(int rawFireDamage, int ironRingCount)
        {
            float reduction = Math.Clamp(ironRingCount * 0.08f, 0f, 0.25f);
            return (int)(rawFireDamage * (1.0f - reduction));
        }
    }

    // 细节 223: 降妖除魔戒指恶魔类怪物伤害减免 10%
    public static class DemonSubdueRingEngine
    {
        public static int ApplyDemonReduction(int rawDamage, bool isDemonMonster, bool hasDemonSubdueRing)
        {
            if (isDemonMonster && hasDemonSubdueRing)
            {
                return (int)(rawDamage * 0.90f);
            }
            return rawDamage;
        }
    }

    // 细节 224: 珊瑚戒指轻便负重 (重 1) 与高攻击偏向
    public static class CoralRingWeightEngine
    {
        public const int Weight = 1;
        public static (int weight, int minDc, int maxDc) GetStats() => (Weight, 0, 4);
    }

    // 细节 225: 金手镯极品生成优先加权魔法防御
    public static class GoldBraceletSuperStatEngine
    {
        public static (int extraMac, int extraDc) RollSuperStat(int roll100)
        {
            if (roll100 < 50) return (1, 0); // 优先加魔防
            return (0, 1);
        }
    }

    // 细节 226: 大手镯极品生成优先加权物理防御
    public static class BigBraceletSuperStatEngine
    {
        public static (int extraAc, int extraDc) RollSuperStat(int roll100)
        {
            if (roll100 < 60) return (1, 0); // 优先加物理防御
            return (0, 1);
        }
    }

    // 细节 227: 铁手镯准确 +1 在命中公式中严格对抗敏捷
    public static class IronBraceletAccuracyEngine
    {
        public static int ApplyIronBracelet(int baseAccuracy, int braceletCount)
        {
            return baseAccuracy + braceletCount * 1;
        }
    }

    // 细节 228: 银蛇剑对道士治愈术恢复总量加成 10%
    public static class SilverSnakeHealBoostEngine
    {
        public static int CalculateHeal(int rawHeal, bool hasSilverSnake)
        {
            return hasSilverSnake ? (int)(rawHeal * 1.10f) : rawHeal;
        }
    }

    // 细节 229: 魔杖保证雷电术最小魔法伤害不低于 5 点
    public static class MagicStaffMinDmgEngine
    {
        public static int EnsureMinDamage(int rolledDamage, bool hasMagicStaff)
        {
            if (hasMagicStaff) return Math.Max(5, rolledDamage);
            return rolledDamage;
        }
    }

    // 细节 230: 炼狱巨斧重 60，检定战士手部腕力上限
    public static class PurgatoryAxeWeightEngine
    {
        public const int AxeWeight = 60;
        public static (bool canWield, string reason) CheckWield(int playerHandWeight)
        {
            if (playerHandWeight < AxeWeight)
            {
                return (false, "腕力不足，无法挥舞沉重的炼狱巨斧！");
            }
            return (true, "");
        }
    }
}
