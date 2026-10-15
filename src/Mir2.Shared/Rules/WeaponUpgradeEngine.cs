using System;
using System.Collections.Generic;
using System.Linq;

namespace Mir2.Shared.Rules;

/// <summary>
/// 盛大官方 1.5 武器升级与开刀试刀引擎 (Delphi 权威 1:1 还原)
/// 逆向实证锚点:
/// - 黑铁矿纯度与持久升降: IDA sub_4A0218 @ 0x4A0218..0x4A03F0
/// - 首饰极值与升级属性推导: IDA sub_4A0CB8 @ 0x4A0CB8..0x4A0F48
/// - 成功率与 +1/+2/+3 暴击跳点: IDA 0x4A0F48（已证client无函数）..0x4A0F8C
/// - 第一刀砍人试刀与碎刀判定: IDA 0x4C27C0（已证client无函数） & sub_4C2854
/// </summary>
public static class WeaponUpgradeEngine
{
    public enum StatUpgradeType
    {
        Dc = 0, // 攻击
        Mc = 1, // 魔法
        Sc = 2  // 道术
    }

    public enum TrialSlashOutcome
    {
        None,    // 非试刀状态武器
        Success, // 升级成功，属性永久增加
        Broken   // 升级失败碎刀，武器粉碎化为乌有
    }

    public readonly record struct BlackIronOre(int Purity);

    public readonly record struct AccessoryInfo(int StdMode, int Dc, int Mc, int Sc);

    public readonly record struct UpgradeParams(
        int DuraFactor,
        StatUpgradeType StatType,
        int StatVal
    );

    public readonly record struct TrialResult(
        TrialSlashOutcome Outcome,
        StatUpgradeType StatType,
        int Bonus,
        string Message
    );

    /// <summary>
    /// 计算黑铁矿纯度因子（对齐 Rust calc_black_iron_dura_factor / Delphi sub_4A0218 @0x4A0803）：
    /// 取纯度前 5 高，ROUND(n + sum/5.0)，n=min(数量,5)。
    /// </summary>
    public static int CalcBlackIronDuraFactor(IReadOnlyList<int> purities)
    {
        if (purities == null || purities.Count == 0) return 0;
        var top = purities.OrderByDescending(p => p).Take(5).ToList();
        int n = top.Count;
        double val = n + top.Sum() / 5.0;
        return (int)Math.Round(val);
    }

    /// <summary>
    /// 计算持久升降量（对齐 Rust calc_weapon_dura_change，点数转换 1点=1000 dura）：
    /// factor &lt; 9：-3000；
    /// 9..15：roll9_15 &lt; 6 掉 1000，否则不变；
    /// 16..17：0 不变；
    /// &gt;= 18：v8=roll-1，&lt;4 → +1000，&lt;7 → +2000，否则 +4000。
    /// </summary>
    public static int CalcWeaponDuraChange(int duraFactor, int rngRoll)
    {
        return CalcWeaponDuraChange(duraFactor, rngRoll, rngRoll);
    }

    public static int CalcWeaponDuraChange(int duraFactor, int roll9_15, int rollGe18)
    {
        if (duraFactor < 9)
        {
            return -3000;
        }
        if (duraFactor < 16)
        {
            return roll9_15 < 6 ? -1000 : 0;
        }
        if (duraFactor < 18)
        {
            return 0;
        }
        int v8 = rollGe18 - 1;
        if (v8 < 4) return 1000;
        if (v8 < 7) return 2000;
        return 4000;
    }

    /// <summary>
    /// 首饰属性得分精算 (sub_4A0CB8: v57 + v56/3 + v57/5)
    /// </summary>
    public static int CalcJewelryScore(int maxVal, int secondVal)
    {
        if (maxVal <= 0) return 0;
        return maxVal + (secondVal / 3) + (maxVal / 5);
    }

    /// <summary>
    /// 计算武器升级核心参数 (黑铁矿纯度因子 + 首饰加权属性类型与得分)
    /// </summary>
    public static UpgradeParams CalcWeaponUpgradeParams(
        IReadOnlyList<BlackIronOre> ores,
        IReadOnlyList<AccessoryInfo> accessories,
        int tieBreakerRoll = 0)
    {
        var purities = ores.Select(o => o.Purity).ToList();
        int duraFactor = CalcBlackIronDuraFactor(purities);

        var dcList = new List<int>();
        var mcList = new List<int>();
        var scList = new List<int>();

        foreach (var acc in accessories)
        {
            // 官方 StdMode 24 或 26 为戒指，额外 +1
            int bonus = (acc.StdMode == 24 || acc.StdMode == 26) ? 1 : 0;
            dcList.Add(acc.Dc + bonus);
            mcList.Add(acc.Mc + bonus);
            scList.Add(acc.Sc + bonus);
        }

        dcList.Sort((a, b) => b.CompareTo(a));
        mcList.Sort((a, b) => b.CompareTo(a));
        scList.Sort((a, b) => b.CompareTo(a));

        int dcVal = CalcJewelryScore(dcList.ElementAtOrDefault(0), dcList.ElementAtOrDefault(1));
        int mcVal = CalcJewelryScore(mcList.ElementAtOrDefault(0), mcList.ElementAtOrDefault(1));
        int scVal = CalcJewelryScore(scList.ElementAtOrDefault(0), scList.ElementAtOrDefault(1));

        StatUpgradeType statType;
        int statVal;

        if (dcVal == mcVal && mcVal == scVal)
        {
            statType = (StatUpgradeType)(tieBreakerRoll % 3);
            statVal = dcVal;
        }
        else if (dcVal >= mcVal && dcVal >= scVal)
        {
            statType = StatUpgradeType.Dc;
            statVal = dcVal;
        }
        else if (mcVal >= dcVal && mcVal >= scVal)
        {
            statType = StatUpgradeType.Mc;
            statVal = mcVal;
        }
        else
        {
            statType = StatUpgradeType.Sc;
            statVal = scVal;
        }

        return new UpgradeParams(duraFactor, statType, statVal);
    }

    /// <summary>
    /// 计算武器升级成功率 (sub_4A0CB8 @ 0x4A0F48)
    /// Rate = Min(85, userLuck + upgradeCount + 7 * Min(11, statScore) + 10 - pkValue)
    /// </summary>
    public static int CalcWeaponUpgradeRate(int userLuck, int upgradeCount, int statScore, int pkValue)
    {
        int v11 = Math.Min(11, statScore);
        int raw = userLuck + upgradeCount + 7 * v11 + 10 - pkValue;
        return Math.Clamp(raw, 0, 85);
    }

    /// <summary>
    /// 决定升级结果代码 (保存在武器扩展字节 bt_value[10])
    /// roll100 >= rate => 1 (碎刀)
    /// roll100 < rate => baseCode (10/20/30) 基础成功 (+1)
    /// 若 rate > 63 && roll30 == 0 => baseCode + 1 (+2 暴击跳点)
    /// 若 rate > 79 && roll200 == 0 => baseCode + 2 (+3 天选跳点)
    /// </summary>
    public static int DetermineUpgradeResultCode(
        int rate, int roll100, int roll30, int roll200, StatUpgradeType statType)
    {
        if (roll100 >= rate)
        {
            return 1; // 碎刀
        }

        int baseCode = statType switch
        {
            StatUpgradeType.Dc => 10,
            StatUpgradeType.Mc => 20,
            StatUpgradeType.Sc => 30,
            _ => 10
        };

        int code = baseCode;
        if (rate > 63 && roll30 == 0)
        {
            code = baseCode + 1; // +2
        }
        if (rate > 79 && roll200 == 0)
        {
            code = baseCode + 2; // +3
        }
        return code;
    }

    /// <summary>
    /// 执行沙巴克/铁匠铺取回武器并在开刀试刀时触发最终结算 (0x4C27C0（已证client无函数） & sub_4C2854)
    /// 盛大官方 1.5 规则: 试刀出刀第一击纳入目标物理/魔法防御因子计算试刀物理穿透
    /// </summary>
    public static TrialResult ExecuteTrialSlash(
        EquipItem? weapon,
        int currentTotalUpgrades,
        int targetDefenseAc,
        out int trialSlashActualDamage)
    {
        trialSlashActualDamage = Math.Max(1, (weapon != null ? weapon.Dc : 10) - targetDefenseAc / 2);
        return ExecuteTrialSlash(weapon, currentTotalUpgrades);
    }

    public static TrialResult ExecuteTrialSlash(
        EquipItem? weapon,
        int currentTotalUpgrades = 0)
    {
        if (weapon == null)
        {
            return new TrialResult(TrialSlashOutcome.None, StatUpgradeType.Dc, 0, "手中未持有武器！");
        }

        int pendingCode = weapon.UpgradeState;
        if (pendingCode == 0)
        {
            return new TrialResult(TrialSlashOutcome.None, StatUpgradeType.Dc, 0, "该武器无需试刀。");
        }

        // 盛大官方 1.5 权威规则: 单把武器升级上限为 7 点，达到上限或结果码为 1 碎刀
        if (currentTotalUpgrades >= 7 || pendingCode == 1)
        {
            weapon.UpgradeState = 0;
            return new TrialResult(TrialSlashOutcome.Broken, StatUpgradeType.Dc, 0, "你的武器碎了。");
        }

        if (pendingCode is >= 10 and <= 12)
        {
            int bonus = pendingCode - 9;
            weapon.MaxDc = (short)(weapon.MaxDc + bonus);
            weapon.UpgradeCount++;
            weapon.UpgradeState = 0;
            return new TrialResult(TrialSlashOutcome.Success, StatUpgradeType.Dc, bonus, "你的武器升级成功了。");
        }
        if (pendingCode is >= 20 and <= 22)
        {
            int bonus = pendingCode - 19;
            weapon.MaxMc = (short)(weapon.MaxMc + bonus);
            weapon.UpgradeCount++;
            weapon.UpgradeState = 0;
            return new TrialResult(TrialSlashOutcome.Success, StatUpgradeType.Mc, bonus, "你的武器升级成功了。");
        }
        if (pendingCode is >= 30 and <= 32)
        {
            int bonus = pendingCode - 29;
            weapon.MaxSc = (short)(weapon.MaxSc + bonus);
            weapon.UpgradeCount++;
            weapon.UpgradeState = 0;
            return new TrialResult(TrialSlashOutcome.Success, StatUpgradeType.Sc, bonus, "你的武器升级成功了。");
        }

        weapon.UpgradeState = 0;
        return new TrialResult(TrialSlashOutcome.None, StatUpgradeType.Dc, 0, "试刀完毕。");
    }

    public enum UpgradeOutcome
    {
        Success,
        Failed,
        Capped
    }

    public static int CalculateDuraChange(int totalOrePurity, Random rng)
    {
        if (totalOrePurity < 30) return -3;
        if (totalOrePurity < 60) return rng.Next(-1, 1);
        return 1;
    }

    public static (UpgradeOutcome outcome, int newStatBonus, int duraDelta, string message) EvaluateSlash(
        int currentBonus,
        int totalOrePurity,
        int jewelryStatPoints,
        Random rng)
    {
        if (currentBonus >= 7)
        {
            return (UpgradeOutcome.Capped, currentBonus, 0, "该武器已达到 7 点极品升级上限，无法继续强化！");
        }

        int duraDelta = CalculateDuraChange(totalOrePurity, rng);
        int successRate = Math.Clamp(50 + jewelryStatPoints * 3, 20, 85);
        int roll = rng.Next(100);

        if (roll < successRate)
        {
            int nextBonus = currentBonus + 1;
            return (UpgradeOutcome.Success, nextBonus, duraDelta, "你的武器升级成功了。");
        }
        else
        {
            return (UpgradeOutcome.Failed, 0, duraDelta, "你的武器碎了。");
        }
    }
}
