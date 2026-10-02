using System;

namespace Mir2Client.Game;

/// <summary>
/// 盛大热血传奇 1.5 官方三大物理独立负重体系 (纯 C# 实现)
/// 严格 1:1 对齐官方 IDA sub_4BF764 / sub_4C50E9 / sub_4C5114:
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

    public static int CalcTotalBagWeight(int itemsTotalWeight, int gold)
    {
        int goldWeight = gold / GoldWeightUnit;
        return itemsTotalWeight + goldWeight;
    }

    public static bool CanRun(int curBagWeight, int maxBagWeight)
    {
        if (maxBagWeight <= 0) return true;
        return (curBagWeight * 100 / maxBagWeight) < RunRestrictedPercent;
    }
}
