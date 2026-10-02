using System;
using System.Collections.Generic;

namespace Mir2Client.Game;

/// <summary>
/// 盛大官方 1.5 权威 9 大装备槽位编号 (MIR_final_v3_p.exe 实证)
/// 彻底肃清私服自造 14 槽位（无靴子、腰带、宝石、勋章）
/// </summary>
public enum OfficialEquipSlot : byte
{
    Dress = 0,     // 衣服
    Weapon = 1,    // 武器
    Lighting = 2,  // 照明 (火把/蜡烛/护符/毒药)
    Necklace = 3,  // 项链
    Helmet = 4,    // 头盔
    ArmringL = 5,  // 左手镯
    ArmringR = 6,  // 右手镯
    RingL = 7,     // 左戒指
    RingR = 8      // 右戒指
}

/// <summary>
/// 装备物品结构模型
/// </summary>
public sealed class EquipItem
{
    public int Uid { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Shape { get; set; }
    public int Weight { get; set; }
    public int Dura { get; set; }
    public int MaxDura { get; set; }
    public int Ac { get; set; }
    public int Mac { get; set; }
    public int Dc { get; set; }
    public int Mc { get; set; }
    public int Sc { get; set; }
    public int Luck { get; set; }
    public int Curse { get; set; }
}

/// <summary>
/// 盛大官方 1.5 核心玩家实体状态机与属性模型
/// </summary>
public sealed class PlayerState
{
    public int Uid { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Job { get; set; } // 0=战士, 1=法师, 2=道士
    public int Sex { get; set; } // 0=男, 1=女
    public int Level { get; set; } = 1;

    public int Hp { get; set; } = 100;
    public int MaxHp { get; set; } = 100;
    public int Mp { get; set; } = 100;
    public int MaxMp { get; set; } = 100;

    public int MapX { get; set; }
    public int MapY { get; set; }
    public byte Direction { get; set; } // 0~7

    // 官方三大负重体系 (背包负重、穿戴负重、腕力)
    public int BagWeight { get; set; }
    public int MaxBagWeight { get; set; } = 200;
    public int WearWeight { get; set; }
    public int MaxWearWeight { get; set; } = 60;
    public int HandWeight { get; set; }
    public int MaxHandWeight { get; set; } = 40;

    // 金币单人严格 500 万上限 (sub_4A4A20)
    public const int MaxGoldLimit = 5_000_000;
    private int _gold;
    public int Gold
    {
        get => _gold;
        set => _gold = Math.Clamp(value, 0, MaxGoldLimit);
    }

    // PK 与红名机制 (PK >= 200 为红名)
    public int PkPoints { get; set; }
    public bool IsRedName => PkPoints >= 200;
    public bool IsGrayName { get; set; }
    public long GrayNameExpireTick { get; set; }

    // 官方 9 大装备槽位数组
    public EquipItem?[] Equipment { get; } = new EquipItem?[9];

    // 背包 40 格
    public List<EquipItem> BagItems { get; } = new(40);

    public bool WearItem(OfficialEquipSlot slot, EquipItem item)
    {
        int slotIdx = (int)slot;
        if (slotIdx < 0 || slotIdx >= 9) return false;
        Equipment[slotIdx] = item;
        return true;
    }

    public EquipItem? TakeOffItem(OfficialEquipSlot slot)
    {
        int slotIdx = (int)slot;
        if (slotIdx < 0 || slotIdx >= 9) return null;
        var item = Equipment[slotIdx];
        Equipment[slotIdx] = null;
        return item;
    }

    // 12 状态位掩码 (0: 绿毒, 1: 红毒, 5: 麻痹, 8: 隐身 等)
    public bool[] StatusBits { get; } = new bool[12];
    public uint BaseFlags { get; set; } = 0;

    public uint GetCurrentStatusMask()
    {
        return PlayerAuditEngine.AuditCalcStatusMask(StatusBits, BaseFlags);
    }

    /// <summary>
    /// 实体使用祝福油喝油
    /// </summary>
    public PlayerAuditEngine.BlessOilResult DrinkBlessOil(int maxDc, int minDc, int rngRoll100, int rngRollSub)
    {
        var weap = Equipment[(int)OfficialEquipSlot.Weapon];
        int luck = weap?.Luck ?? 0;
        int curse = weap?.Curse ?? 0;

        var res = PlayerAuditEngine.AuditDrinkBlessOil(maxDc, minDc, luck, curse, rngRoll100, rngRollSub);
        if (weap != null)
        {
            weap.Luck = res.NewLuck;
            weap.Curse = res.NewCurse;
        }
        return res;
    }

    /// <summary>
    /// 受到物理或魔法攻击扣血结算 (集成护身戒指 1:1.5 蓝量抵扣与复活戒指)
    /// </summary>
    public (int finalHp, int finalMp, bool died, bool revived) TakeCombatDamage(int rawDamage, bool hasProtectRing, bool hasReviveRing, ref int reviveRingDura, long lastReviveTick, long nowTick)
    {
        int actualDamage = rawDamage;

        // 护身戒指
        if (hasProtectRing && Mp > 0)
        {
            var res = SpecialRings.CalculateProtectRingDamage(true, rawDamage, Mp, Hp);
            Mp = res.remainingMp;
            actualDamage = res.remainingDamage;
        }

        Hp = Math.Max(0, Hp - actualDamage);
        if (Hp > 0)
        {
            return (Hp, Mp, false, false);
        }

        // 濒死判定复活戒指
        if (hasReviveRing)
        {
            var revRes = SpecialRings.TriggerReviveRing(true, Hp, MaxHp, reviveRingDura, lastReviveTick, nowTick);
            if (revRes.canRevive)
            {
                Hp = revRes.newHp;
                reviveRingDura = revRes.newDura;
                return (Hp, Mp, false, true);
            }
        }

        return (0, Mp, true, false);
    }
}

