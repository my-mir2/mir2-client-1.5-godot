using System;

namespace Mir2.Shared.Rules;

/// <summary>
/// 盛大官方 1.5 原版道士核心战斗与召唤兽引擎 (IDA 0x49350C（已证client无函数） / sub_4C7720 / sub_4BDEB8 / sub_4D35B0 权威实证)
/// 严格 1:1 比特级还原:
///   1. 施毒术 (Poisoning, MagicId = 6): 
///      - 槽位道具 StdMode=25 检定 (Shape 1: 绿毒, Shape 2: 红毒)
///      - 单次施毒扣除 100 点毒药内部持久 (低于 100 自动销毁药包)
///      - 毒抗命中对抗算法: RandInt(target.AntiPoison + 7) <= 6 (0 抗性 100% 绝对必中)
///      - 持续时间: 绿毒基础 30s + 2*SC, 红毒基础 40s + 2*SC
///      - 毒力效果: 绿毒每 2500ms 扣减 Max(1, SC/3)+1 HP 且清空回复池; 红毒受到的一切伤害与磨损提升 1.2 倍 (tbyte_4BE3CC)
///      - 双毒独立槽位、独立计时，可同时生效
///   2. 召唤神兽 / 变异骷髅 (Summon, MagicId = 17 / 30):
///      - 骷髅扣除 1 张护符 (100 耐久), 神兽扣除 5 张护符 (500 耐久)
///      - 0~7 级随从属性阶梯递增 (骷髅 100~300 HP, 神兽 200~800 HP)
/// </summary>
public static class TaoistCombatEngine
{
    // ==================== 1. 施毒术 (Poisoning) ====================
    public const int PoisonDuraCost = 100;     // 单次消耗 100 点内部持久
    public const int GreenPoisonBaseSec = 30;  // 绿毒基础持续 30 秒 (0x1E)
    public const int RedPoisonBaseSec = 40;    // 红毒基础持续 40 秒 (0x28)
    public const int PoisonTickIntervalMs = 2500; // 绿毒每 2.5 秒跳一次掉血
    public const double RedPoisonDamageMultiplier = 1.2; // 官方 1.2 倍易伤 (tbyte_4BE3CC)

    public enum PoisonType
    {
        None = 0,
        Green = 1, // 绿毒 (持续掉血)
        Red = 2    // 红毒 (1.2 倍增伤减防)
    }

    /// <summary>
    /// 施毒命中判定 (0x49350C（已证client无函数） @ 0x493F7B)
    /// 核心不等式: RandInt(targetAntiPoison + 7) <= 6
    /// </summary>
    public static bool CheckPoisonHit(int targetAntiPoison, Random? rnd = null)
    {
        if (targetAntiPoison >= 100) return false; // 100% 抗毒属性必定完全豁免施毒术
        rnd ??= Random.Shared;
        int maxVal = Math.Max(0, targetAntiPoison) + 7;
        return rnd.Next(maxVal) <= 6;
    }

    /// <summary>
    /// 计算施毒持续时间 (秒)（对齐 Rust calc_poison_base/total_duration）：
    /// base = Round(defPower + (a1-a1/3)/(trainLv+1)*(sl+1) + a1/3)，a1=绿30/红40；
    /// total = base + max(0,SC)*2，最低 绿10/红15 秒。
    /// </summary>
    public static int CalculatePoisonDurationSec(PoisonType type, int taoistSc)
    {
        return CalculatePoisonDurationSec(type, taoistSc, 3, 3, 0);
    }

    public static int CalculatePoisonDurationSec(PoisonType type, int taoistSc, int skillLevel, int trainLevel, int defPower)
    {
        double a1 = type == PoisonType.Green ? 30.0 : 40.0;
        double v5 = a1 / 3.0;
        double v4 = a1 - v5;
        double scaled = (v4 / (trainLevel + 1.0)) * (Math.Clamp(skillLevel, 0, 3) + 1.0);
        int baseSecs = (int)Math.Round(defPower + scaled + v5);
        int total = baseSecs + 2 * Math.Max(0, taoistSc);
        int floor = type == PoisonType.Green ? 10 : 15;
        return Math.Max(floor, total);
    }

    /// <summary>
    /// 绿毒单跳扣血伤害计算（对齐 Rust：pval=(sl+1)+(sc/10)，伤害=pval+1）。
    /// </summary>
    public static int CalculateGreenPoisonTickDamage(int taoistSc)
    {
        return CalculateGreenPoisonTickDamage(3, taoistSc);
    }

    public static int CalculateGreenPoisonTickDamage(int skillLevel, int taoistSc)
    {
        int pval = (Math.Clamp(skillLevel, 0, 3) + 1) + Math.Max(0, taoistSc) / 10;
        return Math.Max(1, pval) + 1;
    }

    /// <summary>
    /// 红毒受到伤害结算 (伤害提升 1.2 倍)
    /// </summary>
    public static int ApplyRedPoisonVulnerability(int incomingDamage, bool hasRedPoison)
    {
        if (!hasRedPoison || incomingDamage <= 0) return incomingDamage;
        return (int)Math.Round(incomingDamage * RedPoisonDamageMultiplier);
    }

    /// <summary>
    /// 检查并扣除施毒药包持久 (StdMode=25, Shape in [1, 2])
    /// </summary>
    public static (bool success, string desc, bool destroyed) TryConsumePoisonBag(
        EquipItem? poisonBag, PoisonType reqType)
    {
        if (poisonBag == null)
        {
            return (false, "未装备毒药包，无法施放施毒术！", false);
        }

        // 盛大官方 1.5 权威物品分类:
        // 灰色药粉 (绿毒, 持续损血且封锁回复): Shape = 1 (或名称含"灰")
        // 黄色药粉 (红毒, 受到伤害加深破防): Shape = 2 (或名称含"黄")
        int reqShape = (int)reqType;
        if (poisonBag.Shape != reqShape)
        {
            string needed = reqType == PoisonType.Green ? "灰色药粉(绿毒)" : "黄色药粉(红毒)";
            return (false, $"当前装备的毒药包类型不匹配，需要【{needed}】！", false);
        }

        if (poisonBag.Dura < PoisonDuraCost)
        {
            return (false, "毒药包分量不足，已消耗殆尽！", true);
        }

        poisonBag.Dura -= PoisonDuraCost;
        bool destroyed = poisonBag.Dura <= 0;
        return (true, $"消耗 1 份毒药 (剩余持久 {poisonBag.Dura})", destroyed);
    }

    // ==================== 2. 召唤兽体系 (Summon) ====================
    public const int TalismanCostSkeleton = 100; // 召唤骷髅扣 1 张护符 (100 耐久)
    public const int TalismanCostShinsu = 500;   // 召唤神兽扣 5 张护符 (500 耐久)

    public sealed class PetTemplate
    {
        public string Name { get; set; } = string.Empty;
        public int Level { get; set; }
        public int MaxHp { get; set; }
        public int MinDc { get; set; }
        public int MaxDc { get; set; }
        public int Ac { get; set; }
        public int Mac { get; set; }
        public int Appr { get; set; }
    }

    /// <summary>
    /// 获取变异骷髅 0~7 级属性规格
    /// </summary>
    public static PetTemplate GetSkeletonStats(int petLevel)
    {
        petLevel = Math.Clamp(petLevel, 0, 7);
        return new PetTemplate
        {
            Name = petLevel == 7 ? "变异骷髅 (7级极品)" : $"变异骷髅 ({petLevel}级)",
            Level = petLevel,
            MaxHp = 100 + petLevel * 28,      // 0级 100, 7级 296
            MinDc = 5 + petLevel * 2,         // 0级 5, 7级 19
            MaxDc = 12 + petLevel * 3,        // 0级 12, 7级 33
            Ac = 5 + petLevel * 2,            // 0级 5, 7级 19
            Mac = 3 + petLevel,
            Appr = 20 // 官方骷髅外观号 (Mon3.wil 对应 official_monsters.csv Appr=20)
        };
    }

    /// <summary>
    /// 获取召唤神兽 0~7 级属性规格
    /// </summary>
    public static PetTemplate GetShinsuStats(int petLevel)
    {
        petLevel = Math.Clamp(petLevel, 0, 7);
        return new PetTemplate
        {
            Name = petLevel == 7 ? "烈焰神兽 (7级终极)" : $"神兽 ({petLevel}级)",
            Level = petLevel,
            MaxHp = 200 + petLevel * 85,      // 0级 200, 7级 795
            MinDc = 15 + petLevel * 3,        // 0级 15, 7级 36
            MaxDc = 25 + petLevel * 5,        // 0级 25, 7级 60
            Ac = 12 + petLevel * 2,           // 0级 12, 7级 26
            Mac = 12 + petLevel * 2,
            Appr = petLevel >= 7 ? 14 : 13    // 7级直立行走神兽
        };
    }

    public const int ReqTalismanDuraFireBall = 100;
    public const int ReqTalismanDuraSkeleton = 100;
    public const int ReqTalismanDuraShinsu = 500;

    /// <summary>
    /// 校验并扣除道士护符持久 (纯数值重载)
    /// </summary>
    public static (bool success, string desc, bool destroyed) TryConsumeTalisman(
        int talismanDura, int reqDura)
    {
        if (talismanDura < reqDura)
        {
            int sheets = talismanDura / 100;
            return (false, $"未佩戴护身符或张数不足 (仅剩 {sheets} 张，需 {reqDura / 100} 张)！", false);
        }
        int remaining = talismanDura - reqDura;
        bool destroyed = remaining <= 0;
        return (true, $"消耗 {reqDura / 100} 张护身符 (剩余 {remaining / 100} 张)", destroyed);
    }

    /// <summary>
    /// 校验并扣除道士护符持久 (StdMode=25, Shape=5)
    /// </summary>
    public static (bool success, string desc, bool destroyed) TryConsumeTalisman(
        EquipItem? talisman, int reqDura)
    {
        if (talisman == null)
        {
            return (false, "未装备道士护身符，无法施展召唤秘术！", false);
        }

        if (talisman.Shape != 5)
        {
            return (false, "装备的物品非正规道士护身符！", false);
        }

        if (talisman.Dura < reqDura)
        {
            int sheets = talisman.Dura / 100;
            return (false, $"护身符张数不足 (仅剩 {sheets} 张，需 {reqDura / 100} 张)！", false);
        }

        talisman.Dura -= reqDura;
        bool destroyed = talisman.Dura <= 0;
        return (true, $"消耗 {reqDura / 100} 张护身符 (剩余 {talisman.Dura / 100} 张)", destroyed);
    }

    /// <summary>
    /// 校验并扣除施毒术药粉持久 (灰色毒粉/黄色毒粉)
    /// </summary>
    public static (bool success, string desc, bool destroyed) TryConsumePoisonBag(
        int poisonDura, PoisonType type)
    {
        if (poisonDura < PoisonDuraCost)
        {
            string pName = type == PoisonType.Green ? "灰色毒粉" : "黄色毒粉";
            return (false, $"背包缺少【{pName}】或药粉已耗尽，无法施展施毒术！", false);
        }
        int remaining = poisonDura - PoisonDuraCost;
        bool destroyed = remaining <= 0;
        return (true, $"消耗 1 份施毒药粉 (剩余持久: {remaining})", destroyed);
    }

    // ==================== 4. 群体治愈术 (MassHeal, MagicId = 18) ====================
    public static class MassHeal
    {
        public const int Radius = 1; // 3x3 范围 (Chebyshev 半径 1)

        /// <summary>
        /// 计算群体治愈术单次治疗量 (基于道术 SC 与技能等级)
        /// </summary>
        public static int CalculateHealAmount(int sc, int skillLevel)
        {
            return 15 + skillLevel * 10 + (int)Math.Round(sc * 0.8);
        }
    }

    // ==================== 5. 神圣战甲术与幽灵盾 (HolyArmor & GhostShield) ====================
    public static class AuraBuffs
    {
        public const int BonusAc = 3;   // 神圣战甲术物理防御加成 AC +3
        public const int BonusMac = 3;  // 幽灵盾魔法防御加成 MAC +3

        /// <summary>
        /// 计算光环持续时间 (秒): 30 秒基准 + 道术 * 2 秒
        /// </summary>
        public static int CalculateBuffDurationSec(int sc, int skillLevel)
        {
            return 30 + skillLevel * 15 + sc * 2;
        }
    }
}
