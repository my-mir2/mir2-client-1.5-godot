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
    public int UpgradeState { get; set; } // 官方 bt_value[10] 升级结果试刀码 (1=碎刀, 10..12=DC, 20..22=MC, 30..32=SC)
    public int UpgradeCount { get; set; } // 累计升级成功次数 (达到25次必碎)
}

/// <summary>
/// 盛大官方 1.5 核心玩家实体状态机与属性模型
/// 集成官方 7 大特戒 (护身/复活/麻痹/超负载/隐身/传送/技能戒指)
/// 与 4 大套装 (记忆/祈祷/魔血/虹魔) 底层判定引擎
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

    // 基础 HP/MP (供魔血套装转化计算)
    public int BaseMaxHp { get; set; } = 100;
    public int BaseMaxMp { get; set; } = 100;

    public int MapX { get; set; }
    public int MapY { get; set; }
    public byte Direction { get; set; } // 0~7

    // 官方三大负重体系 (背包负重、穿戴负重、腕力)
    public int BagWeight { get; set; }
    public int BaseMaxBagWeight { get; set; } = 200;
    public int MaxBagWeight { get; set; } = 200;

    public int WearWeight { get; set; }
    public int BaseMaxWearWeight { get; set; } = 60;
    public int MaxWearWeight { get; set; } = 60;

    public int HandWeight { get; set; }
    public int BaseMaxHandWeight { get; set; } = 40;
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
    public int PkPoint
    {
        get => PkPoints;
        set => PkPoints = value;
    }
    public int PkLevel => PkRuleEngine.GetPkLevel(PkPoints);
    public bool IsRedName => PkRuleEngine.GetPkStatus(PkPoints) >= PkRuleEngine.PkStatus.Red;
    public bool IsYellowName => PkRuleEngine.GetPkStatus(PkPoints) == PkRuleEngine.PkStatus.Yellow;
    public bool IsGrayName { get; set; }
    public long GrayNameExpireTick { get; set; }

    // 官方 9 大装备槽位数组
    public EquipItem?[] Equipment { get; } = new EquipItem?[9];

    // 背包 40 格
    public List<EquipItem> BagItems { get; } = new(40);

    // ==================== 官方 7 大特戒与 4 大套装运行时状态 ====================
    public bool HasProtectRing { get; private set; }
    public bool HasReviveRing { get; private set; }
    public int ReviveRingDura { get; set; } = 5000; // 内部持久
    public long LastReviveTick { get; set; }

    public bool HasParalysisRing { get; private set; }
    public bool HasOverloadRing { get; private set; }
    public bool HasInvisibleRing { get; private set; }

    public bool HasTeleportRing { get; private set; }
    public long LastTeleportTick { get; set; }

    public bool HasRecallSet { get; private set; }
    public long LastRecallTick { get; set; }

    public bool HasPrayerSet { get; private set; }
    public bool HasMagicBloodSet { get; private set; }
    public bool HasRainbowSet { get; private set; }

    public int LeechWeight { get; private set; }
    public int BonusHit { get; private set; }
    public double LifestealAccumPool { get; set; }

    // 特戒赋予的技能列表 (如火焰戒指赋予火球术 1，防御戒指赋予治愈术 2)
    public List<int> GrantedRingSkills { get; } = new();

    // 12 状态位掩码 (0: 绿毒, 1: 红毒, 5: 麻痹, 8: 隐身 等)
    public bool[] StatusBits { get; } = new bool[12];
    public uint BaseFlags { get; set; } = 0;

    public uint GetCurrentStatusMask()
    {
        return PlayerAuditEngine.AuditCalcStatusMask(StatusBits, BaseFlags);
    }

    public bool WearItem(OfficialEquipSlot slot, EquipItem item, long nowTick = 0)
    {
        int slotIdx = (int)slot;
        if (slotIdx < 0 || slotIdx >= 9) return false;
        Equipment[slotIdx] = item;
        RecalculateAttributes(nowTick);
        return true;
    }

    public EquipItem? TakeOffItem(OfficialEquipSlot slot, long nowTick = 0)
    {
        int slotIdx = (int)slot;
        if (slotIdx < 0 || slotIdx >= 9) return null;
        var item = Equipment[slotIdx];
        Equipment[slotIdx] = null;
        RecalculateAttributes(nowTick);
        return item;
    }

    /// <summary>
    /// 全量重算装备附加属性、7 大特戒状态与 4 大套装加成
    /// </summary>
    public void RecalculateAttributes(long nowTick = 0)
    {
        // 1. 重置特戒标志
        HasProtectRing = false;
        HasReviveRing = false;
        HasParalysisRing = false;
        HasOverloadRing = false;
        HasInvisibleRing = false;
        HasTeleportRing = false;

        // 收集 Shape 列表
        var pieceShapes = new List<int>();

        for (int i = 0; i < 9; i++)
        {
            var eq = Equipment[i];
            if (eq == null) continue;
            pieceShapes.Add(eq.Shape);

            // 护身戒指
            if (eq.Shape == 118 || eq.Name.Contains("护身戒指"))
            {
                HasProtectRing = true;
            }
            // 复活戒指
            if (eq.Shape == 114 || eq.Name.Contains("复活戒指"))
            {
                HasReviveRing = true;
                if (ReviveRingDura <= 0 && eq.Dura > 0)
                    ReviveRingDura = eq.Dura * 1000;
            }
            // 麻痹戒指
            if (eq.Shape == 113 || eq.Name.Contains("麻痹戒指"))
            {
                HasParalysisRing = true;
            }
            // 超负载戒指
            if (eq.Shape == 119 || eq.Name.Contains("超负载戒指"))
            {
                HasOverloadRing = true;
            }
            // 隐身戒指
            if (eq.Shape == 111 || eq.Name.Contains("隐身戒指"))
            {
                HasInvisibleRing = true;
            }
            // 传送戒指
            if (eq.Shape == 112 || eq.Name.Contains("传送戒指"))
            {
                HasTeleportRing = true;
            }
        }

        // 2. 超负载戒指负重翻倍
        var w = SpecialRings.ApplyOverloadRing(BaseMaxBagWeight, BaseMaxWearWeight, BaseMaxHandWeight, HasOverloadRing);
        MaxBagWeight = w.maxWeight;
        MaxWearWeight = w.maxWearWeight;
        MaxHandWeight = w.maxHandWeight;

        // 3. 隐身戒指状态位同步
        StatusBits[8] = HasInvisibleRing;

        // 4. 技能戒指检测
        GrantedRingSkills.Clear();
        GrantedRingSkills.AddRange(SpecialRings.GetSpecialRingGrantedSkills(Equipment, Job));

        // 5. 魔血套装重算
        var mb = SpecialRings.CalculateMagicBloodBonus(pieceShapes.ToArray(), BaseMaxHp, BaseMaxMp);
        HasMagicBloodSet = mb.hasFullSet;
        MaxHp = mb.finalHp;
        MaxMp = mb.finalMp;
        Hp = Math.Min(Hp, MaxHp);
        Mp = Math.Min(Mp, MaxMp);

        // 6. 虹魔套装重算
        var rm = SpecialRings.CalculateRainbowMagicBonus(pieceShapes.ToArray(), 0, 0);
        HasRainbowSet = rm.hasFullSet;
        LeechWeight = rm.leechWeight;
        BonusHit = rm.bonusHit;

        // 7. 记忆套装检测
        HasRecallSet = SpecialRings.CheckRecallSet(Equipment);

        // 8. 祈祷套装检测
        var ps = SpecialRings.CheckPrayerSet(Equipment);
        HasPrayerSet = ps.isFullSet;
    }

    /// <summary>
    /// 执行传送戒指瞬移指令 (@move x y / @飞 x y)
    /// </summary>
    public (bool success, int targetX, int targetY, string message) TryExecuteTeleport(
        string command, Func<int, int, bool>? isTileBlocked = null, int mapW = 1000, int mapH = 1000, long nowTick = 0)
    {
        var res = SpecialRings.ExecuteTeleportRing(HasTeleportRing, command, MapX, MapY, isTileBlocked, mapW, mapH, LastTeleportTick, nowTick);
        if (res.success)
        {
            LastTeleportTick = nowTick;
            MapX = res.targetX;
            MapY = res.targetY;
        }
        return res;
    }

    /// <summary>
    /// 执行记忆套装全队召集指令 (@天地合一 / @grouprecall)
    /// </summary>
    public (bool success, List<(int uid, int targetX, int targetY)> movedMembers, string message) TryExecuteRecall(
        string command, bool isLeader, List<(int uid, string name, int x, int y)> teammates,
        Func<int, int, bool>? isTileBlocked = null, long nowTick = 0)
    {
        var res = SpecialRings.TriggerGroupRecall(HasRecallSet, isLeader, command, MapX, MapY, Direction, teammates, isTileBlocked, LastRecallTick, nowTick);
        if (res.success)
        {
            LastRecallTick = nowTick;
        }
        return res;
    }

    /// <summary>
    /// 触发祈祷套装天谴事件 (集齐时)
    /// </summary>
    public (bool triggered, string broadcastMsg, bool petRebellion, OfficialEquipSlot brokenSlot) TryTriggerPrayerApocalypse(int rngSlotRoll = -1)
    {
        var ps = SpecialRings.CheckPrayerSet(Equipment);
        if (!ps.isFullSet)
        {
            return (false, string.Empty, false, OfficialEquipSlot.Dress);
        }

        var res = SpecialRings.TriggerPrayerApocalypse(true, ps.equippedSlots, rngSlotRoll);
        if (res.triggered)
        {
            // 碎裂卸下该装备
            TakeOffItem(res.brokenSlot);
        }
        return res;
    }

    /// <summary>
    /// 实体物理攻击造成伤害时触发虹魔吸血
    /// </summary>
    public int ProcessAttackLifesteal(int realDamage)
    {
        if (LeechWeight <= 0 || realDamage <= 0) return 0;
        double pool = LifestealAccumPool;
        int heal = SpecialRings.AccumulateLifesteal(ref pool, realDamage, LeechWeight);
        LifestealAccumPool = pool;
        if (heal > 0)
        {
            Hp = Math.Min(MaxHp, Hp + heal);
        }
        return heal;
    }

    /// <summary>
    /// 实体攻击目标时触发麻痹戒指判定
    /// </summary>
    public (bool isParalyzed, int durationSec) TryParalyzeTarget(int targetPoisonRecover, bool targetAntiParalysis = false, int rngRoll = -1)
    {
        var res = SpecialRings.TriggerParalysisRing(HasParalysisRing, targetPoisonRecover, targetAntiParalysis, rngRoll);
        return (res.isParalyzed, res.durationSec);
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
    /// 受到物理或魔法攻击扣血结算 (兼容重载，支持显式传入特戒状态)
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

    // ==================== 法师魔法盾运行时状态 ====================
    public bool HasMagicShield { get; private set; }
    public int MagicShieldLevel { get; private set; } = 1;
    public int MagicShieldRemainingSec { get; private set; } = 0;

    public void ActivateMagicShield(int level, int durationSec = 30)
    {
        HasMagicShield = true;
        MagicShieldLevel = Math.Clamp(level, 1, 3);
        MagicShieldRemainingSec = durationSec;
    }

    public void DeactivateMagicShield()
    {
        HasMagicShield = false;
        MagicShieldRemainingSec = 0;
    }

    /// <summary>
    /// 受到物理或魔法攻击扣血结算 (向后兼容经典 4 元素元组)
    /// </summary>
    public (int finalHp, int finalMp, bool died, bool revived) TakeCombatDamage(int rawDamage, long nowTick = 0)
    {
        var res = TakeCombatDamageFull(rawDamage, nowTick, false);
        return (res.finalHp, res.finalMp, res.died, res.revived);
    }

    /// <summary>
    /// 受到物理或魔法攻击扣血结算 (集成法师魔法盾减伤吸收、护身戒指 1:1.5 蓝量抵扣与复活戒指完整明细)
    /// </summary>
    public (int finalHp, int finalMp, bool died, bool revived, int shieldAbsorbed, bool shieldBroken) TakeCombatDamageFull(
        int rawDamage, long nowTick = 0, bool isArmorPiercing = false)
    {
        int actualDamage = rawDamage;
        int absorbed = 0;
        bool shieldBroken = false;

        // 1. 法师魔法盾判定 (若为隔位刺杀 isArmorPiercing 则直接无视击穿)
        if (!isArmorPiercing && HasMagicShield && MagicShieldRemainingSec > 0)
        {
            var shieldRes = SkillFormulas.CalculateMagicShieldDamage(actualDamage, MagicShieldLevel, MagicShieldRemainingSec);
            actualDamage = shieldRes.actualDamage;
            absorbed = shieldRes.absorbedDamage;
            MagicShieldRemainingSec = shieldRes.newRemainingSec;
            if (shieldRes.isBroken)
            {
                shieldBroken = true;
                DeactivateMagicShield();
            }
        }

        // 2. 护身戒指
        if (HasProtectRing && Mp > 0)
        {
            var res = SpecialRings.CalculateProtectRingDamage(true, actualDamage, Mp, Hp);
            Mp = res.remainingMp;
            actualDamage = res.remainingDamage;
        }

        Hp = Math.Max(0, Hp - actualDamage);
        if (Hp > 0)
        {
            return (Hp, Mp, false, false, absorbed, shieldBroken);
        }

        // 3. 濒死判定复活戒指
        if (HasReviveRing)
        {
            var revRes = SpecialRings.TriggerReviveRing(true, Hp, MaxHp, ReviveRingDura, LastReviveTick, nowTick);
            if (revRes.canRevive)
            {
                Hp = revRes.newHp;
                ReviveRingDura = revRes.newDura;
                LastReviveTick = nowTick;

                // 若持久耗尽，戒指破碎
                if (revRes.isBroken)
                {
                    // 查找复活戒指槽位并移除
                    for (int i = 7; i <= 8; i++)
                    {
                        var ring = Equipment[i];
                        if (ring != null && (ring.Shape == 114 || ring.Name.Contains("复活戒指")))
                        {
                            TakeOffItem((OfficialEquipSlot)i, nowTick);
                            break;
                        }
                    }
                }

                return (Hp, Mp, false, true, absorbed, shieldBroken);
            }
        }

        return (0, Mp, true, false, absorbed, shieldBroken);
    }

    public void AddPkMurder()
    {
        PkPoint += PkRuleEngine.KillHumanAddPkPoint;
    }

    /// <summary>
    /// 第一刀挥砍攻击时执行武器开刀试刀判定
    /// </summary>
    public WeaponUpgradeEngine.TrialResult TrialSlashEquippedWeapon()
    {
        var weapon = Equipment[(int)OfficialEquipSlot.Weapon];
        if (weapon == null || weapon.UpgradeState == 0)
        {
            return new WeaponUpgradeEngine.TrialResult(
                WeaponUpgradeEngine.TrialSlashOutcome.None,
                WeaponUpgradeEngine.StatUpgradeType.Dc,
                0,
                "未装备待试刀武器。"
            );
        }

        var res = WeaponUpgradeEngine.ExecuteTrialSlash(weapon, weapon.UpgradeCount);
        if (res.Outcome == WeaponUpgradeEngine.TrialSlashOutcome.Broken)
        {
            // 碎刀：从装备槽卸下并彻底销毁武器
            Equipment[(int)OfficialEquipSlot.Weapon] = null;
            RecalculateAttributes();
        }
        else if (res.Outcome == WeaponUpgradeEngine.TrialSlashOutcome.Success)
        {
            weapon.UpgradeCount++;
            RecalculateAttributes();
        }
        return res;
    }
}
