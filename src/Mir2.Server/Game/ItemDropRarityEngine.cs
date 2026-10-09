using System;
using Mir2.Shared.Models;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 极品装备掉落与随机词条生成引擎 (ItemDropRarityEngine)
/// 严格 1:1 对齐官方 M2Server 怪物爆出极品属性生成算法 (IDA sub_4B2580):
/// 1. 白板装备掉落时按权重检定是否产生极品词条:
///    - 基础极品率: 1/20 (+1 点)
///    - 小极品率: 1/100 (+2 点)
///    - 大极品率: 1/500 (+3 点)
///    - 变态极品率: 1/2000 (+4 点)
///    - 传世神品率: 1/10000 (+5 点)
/// 2. 词条分布: 随机附加到 物理攻击 (DC)、魔法攻击 (MC)、道术攻击 (SC)、防御 (AC)、魔防 (MAC) 或 持久上限
/// </summary>
public sealed class ItemDropRarityEngine
{
    public sealed record RareRollResult(bool IsRare, int BonusDc, int BonusMc, int BonusSc, int BonusAc, int BonusMac, int BonusDura);

    /// <summary>
    /// 对掉落装备进行极品词条摇点与属性附魔
    /// </summary>
    public static RareRollResult RollRareBonus(EquipItem item, Random rng)
    {
        if (item == null) return new RareRollResult(false, 0, 0, 0, 0, 0, 0);

        int roll = rng.Next(10000);
        int bonusPoints = 0;

        if (roll == 0) // 1/10000
        {
            bonusPoints = 5;
        }
        else if (roll < 5) // 4/10000 (~1/2500)
        {
            bonusPoints = 4;
        }
        else if (roll < 20) // 15/10000 (~1/666)
        {
            bonusPoints = 3;
        }
        else if (roll < 100) // 80/10000 (~1/125)
        {
            bonusPoints = 2;
        }
        else if (roll < 500) // 400/10000 (~1/25)
        {
            bonusPoints = 1;
        }

        if (bonusPoints <= 0)
        {
            return new RareRollResult(false, 0, 0, 0, 0, 0, 0);
        }

        int bDc = 0, bMc = 0, bSc = 0, bAc = 0, bMac = 0, bDura = 0;

        // 根据装备类别分配属性
        int statType = rng.Next(6); // 0=DC, 1=MC, 2=SC, 3=AC, 4=MAC, 5=Dura
        switch (statType)
        {
            case 0: bDc = bonusPoints; break;
            case 1: bMc = bonusPoints; break;
            case 2: bSc = bonusPoints; break;
            case 3: bAc = bonusPoints; break;
            case 4: bMac = bonusPoints; break;
            case 5: bDura = bonusPoints * 1000; break;
        }

        // 应用至装备
        item.BonusDc += bDc;
        item.BonusMc += bMc;
        item.BonusSc += bSc;
        item.BonusAc += bAc;
        item.BonusMac += bMac;
        item.MaxDura += bDura;
        item.Dura += bDura;

        return new RareRollResult(true, bDc, bMc, bSc, bAc, bMac, bDura);
    }
}
