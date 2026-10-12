using System;

namespace Mir2.Shared.Rules;

/// <summary>
/// 盛大热血传奇 1.5 官方三大物理独立负重体系 (纯 C# 实现)
/// 严格 1:1 对齐官方 IDA 0x4BF764（已证client无函数） / sub_4C50E9 / sub_4C5114:
///   1. 腕力限制 (MaxHandWeight): 武器与照明物/副手专属检定
///   2. 穿戴负重 (MaxWearWeight): 除武器/照明外 7 大防具饰品总重检定
///   3. 背包负重 (MaxBagWeight): 40 格物品真实重量 + 每 10,000 金币折算 1 负重
/// </summary>
public static class WeightConstraintEngine
{
    public const int GoldWeightUnit = 10_000;
    public const int RunRestrictedPercent = 95;

    public static int CalcMaxHandWeight(int level, int job)
    {
        double divisor = job switch
        {
            1 => 90.0, // 法师
            2 => 42.0, // 道士
            _ => 13.0  // 战士
        };
        return (int)(12.0 + Math.Round((level / divisor) * level));
    }

    public static int CalcMaxWearWeight(int level, int job)
    {
        double divisor = job switch
        {
            1 => 100.0, // 法师
            2 => 50.0,  // 道士
            _ => 20.0   // 战士
        };
        return (int)(15.0 + Math.Round((level / divisor) * level));
    }

    public static int CalcMaxBagWeight(int level, int job)
    {
        double divisor = job switch
        {
            1 => 5.0, // 法师
            2 => 4.0, // 道士
            _ => 3.0  // 战士
        };
        return (int)(50.0 + Math.Round((level / divisor) * level));
    }

    public static int CalcGoldWeight(int gold)
    {
        return gold > 0 ? gold / GoldWeightUnit : 0;
    }

    public static int CalcTotalBagWeight(int itemsTotalWeight, int gold)
    {
        int goldWeight = CalcGoldWeight(gold);
        return itemsTotalWeight + goldWeight;
    }

    /// <summary>
    /// 结合金币单人上限 (10,000,000) 与当前背包负重上限，计算角色当前最多可容纳增加的金币数
    /// </summary>
    public static int CalculateMaxGoldCanReceive(int currentGold, int currentBagWeight, int maxBagWeight)
    {
        if (currentGold >= 10_000_000) return 0;
        int remainingBagWeight = maxBagWeight - currentBagWeight;
        if (remainingBagWeight <= 0) return 0;

        int goldRemainder = currentGold % GoldWeightUnit;
        int goldSlack = goldRemainder == 0 ? 0 : (GoldWeightUnit - goldRemainder);
        long maxGoldByWeight = (long)remainingBagWeight * GoldWeightUnit + goldSlack;

        long maxAllowedTotal = Math.Min(10_000_000L, (long)currentGold + maxGoldByWeight);
        return (int)Math.Max(0, maxAllowedTotal - currentGold);
    }

    public static bool CanRun(int curBagWeight, int maxBagWeight)
    {
        if (maxBagWeight <= 0) return true;
        return (curBagWeight * 100 / maxBagWeight) < RunRestrictedPercent;
    }

    /// <summary>
    /// 盛大官方 1.5 装备穿戴物理负重与腕力检定 (IDA sub_4C50E9 / sub_4C5114)
    /// </summary>
    public static bool CheckCanEquip(
        int level,
        int job,
        int equipSlot,
        int itemWeight,
        int currentTotalWearWeight,
        out string reason)
    {
        // 1. 武器槽位 (Slot 1): 校验手部腕力上限 (MaxHandWeight)
        if (equipSlot == 1)
        {
            int maxHand = CalcMaxHandWeight(level, job);
            if (itemWeight > maxHand)
            {
                reason = $"你的腕力不足！装备该武器需要腕力 {itemWeight} 点，当前腕力上限为 {maxHand} 点。";
                return false;
            }
        }
        // 2. 其余防具与首饰槽位 (Slot 0 衣服, 2 头盔, 3 项链, 4/5 手镯, 6/7 戒指, 8 护符): 校验穿戴总负重 (MaxWearWeight)
        else
        {
            int maxWear = CalcMaxWearWeight(level, job);
            if (currentTotalWearWeight + itemWeight > maxWear)
            {
                reason = $"穿戴总重量超出负重上限！新增重量 {itemWeight} 点，当前已穿戴 {currentTotalWearWeight}/{maxWear} 点。";
                return false;
            }
        }

        reason = string.Empty;
        return true;
    }

    /// <summary>
    /// 从物品名称与描述中提取重量属性
    /// </summary>
    public static int ExtractItemWeight(string itemName, string? desc)
    {
        if (!string.IsNullOrEmpty(desc))
        {
            int idx = desc.IndexOf("重量");
            if (idx >= 0)
            {
                int numStart = idx + 2;
                while (numStart < desc.Length && (desc[numStart] == ':' || desc[numStart] == ' ' || desc[numStart] == '：'))
                    numStart++;
                int numEnd = numStart;
                while (numEnd < desc.Length && char.IsDigit(desc[numEnd]))
                    numEnd++;
                if (numEnd > numStart && int.TryParse(desc.Substring(numStart, numEnd - numStart), out int w))
                {
                    return w;
                }
            }
        }

        // 默认官方装备重量基准表 (IDA ItemDB / StdItems.def)
        if (itemName.Contains("屠龙")) return 99;
        if (itemName.Contains("裁决")) return 80;
        if (itemName.Contains("炼狱")) return 60;
        if (itemName.Contains("井中月")) return 58;
        if (itemName.Contains("修罗")) return 40;
        if (itemName.Contains("银蛇")) return 26;
        if (itemName.Contains("骨玉")) return 20;
        if (itemName.Contains("嗜魂")) return 26;
        if (itemName.Contains("龙纹")) return 40;
        if (itemName.Contains("斩马刀")) return 27;
        if (itemName.Contains("乌木剑")) return 8;
        if (itemName.Contains("木剑")) return 7;

        if (itemName.Contains("战神盔甲")) return 40;
        if (itemName.Contains("重盔甲")) return 23;
        if (itemName.Contains("魔法长袍") || itemName.Contains("灵魂战衣")) return 12;
        if (itemName.Contains("轻型盔甲")) return 8;
        if (itemName.Contains("布衣")) return 5;

        if (itemName.Contains("黑铁头盔")) return 20;
        if (itemName.Contains("青铜头盔")) return 4;
        if (itemName.Contains("骷髅头盔")) return 5;

        return 1;
    }
}
