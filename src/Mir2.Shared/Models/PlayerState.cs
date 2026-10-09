using System;
using System.Collections.Generic;
using Mir2.Shared.Rules;

namespace Mir2.Shared.Models;

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
    public int Id { get => Uid; set => Uid = value; }
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

    // 金币单人严格 500 万上限 (0x4A4A20（已证client无函数）)
    // 金币上限 10,000,000：M2 sub_4DDC8C *(v43+616)=0x989680 与 Rust trade.rs MAX_GOLD_CAP 一致；
    // 原 5M 为误标（Rust 侧背包金 5M/交易金 10M 分轨，此处为交易与持有统一上限）。
    public const int MaxGoldLimit = 10_000_000;
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

    // 官方 1.4/1.5 M2Server 物理真值字段 (IDA sub_4C1EF4 & sub_4D1E54)
    public byte AttackMode { get; set; } = 0; // 0=全体, 1=和平, 2=组队, 3=行会, 4=善恶 (+0x2A1)
    public byte Hit { get; set; } = 15; // 准确 Accuracy (+0x23C)
    public byte Speed { get; set; } = 15; // 敏捷 Agility (+0x26C)
    public byte AntiPoison { get; set; } = 0; // 抗毒抗麻 (+0x24A)
    public bool BoAttackKill { get; set; } // 攻杀剑气蓄力 (+0x52C)
    public bool BoFireHit { get; set; } // 烈火剑法火焰蓄力 (+0x52F)
    public byte HitPlus { get; set; } = 5; // 攻杀附加伤害 (+0x23D)
    public byte FireHitPlus { get; set; } = 14; // 烈火系数 (+0x23E)

    // 角色当前总攻防属性 (基础成长 + 全量装备附加 + 试刀加成)
    public int Dc { get; set; } = 5;
    public int MaxDc { get; set; } = 10;
    public int Mc { get; set; } = 0;
    public int MaxMc { get; set; } = 0;
    public int Sc { get; set; } = 0;
    public int MaxSc { get; set; } = 0;
    public int Ac { get; set; } = 0;
    public int MaxAc { get; set; } = 0;
    public int Mac { get; set; } = 0;
    public int MaxMac { get; set; } = 0;

    // 玩家自由分配的加点属性 (通过 CM_ADJUST_BONUS 1043 永久附加)
    public int BonusDc { get; set; } = 0;
    public int BonusMc { get; set; } = 0;
    public int BonusSc { get; set; } = 0;
    public int BonusAc { get; set; } = 0;
    public int BonusMac { get; set; } = 0;
    public int BonusHp { get; set; } = 0;
    public int BonusMp { get; set; } = 0;

    // ==================== 官方 1.5 战法道权威物理判定引擎实例 ====================
    public WarriorCombatEngine.FireHitTracker FireHitTracker { get; } = new();
    public WarriorCombatEngine.StabbingTracker StabbingTracker { get; } = new();

    // 施毒术状态机 (IDA 0x49350C / sub_4C7720)
    public bool HasGreenPoison { get; set; }
    public long GreenPoisonExpireMs { get; set; }
    public long NextGreenPoisonTickMs { get; set; }
    public int GreenPoisonSc { get; set; }

    public bool HasRedPoison { get; set; }
    public long RedPoisonExpireMs { get; set; }

    // 官方 9 大装备槽位数组
    public EquipItem?[] Equipment { get; } = new EquipItem?[9];

    // 背包 40 格
    public List<EquipItem> BagItems { get; } = new(40);

    // 官方 40 格独立仓库 (IDA sub_4A5E44 MAX_STORAGE_COUNT = 40)
    public const int MaxStorageCapacity = 40;
    public List<EquipItem> StorageItems { get; } = new(MaxStorageCapacity);
    private int _storageGold;
    public int StorageGold
    {
        get => _storageGold;
        set => _storageGold = Math.Clamp(value, 0, MaxGoldLimit);
    }
    public string StoragePassword { get; set; } = string.Empty;
    public bool IsStorageUnlocked { get; set; } = false;

    // 官方 1.4/1.5 药水持续血池与蓝池递增 (IDA sub_4B7100 TCreature.Eat: nIncHealth / nIncSpell)
    public int IncHealth { get; set; } = 0;
    public int IncSpell { get; set; } = 0;
    public long LastPotionTick { get; set; } = 0;

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
            // 盛大官方 1.5 铁律：有持久上限且当前持久磨损归零者彻底破损坏死，不激活特戒与套装隐藏属性
            if (eq.MaxDura > 0 && eq.Dura <= 0) continue;
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

        // 9. 累加全量装备属性与职业基础成长
        int sumAc = 0, sumMac = 0, sumDc = 0, sumMc = 0, sumSc = 0;
        for (int i = 0; i < 9; i++)
        {
            var eq = Equipment[i];
            if (eq == null || (eq.MaxDura > 0 && eq.Dura <= 0)) continue;
            sumAc += eq.Ac;
            sumMac += eq.Mac;
            sumDc += eq.Dc;
            sumMc += eq.Mc;
            sumSc += eq.Sc;
        }

        int baseMinDc = Job == 0 ? 5 + Level / 3 : 2;
        int baseMaxDc = Job == 0 ? 10 + Level : 4;
        Dc = baseMinDc + sumDc + BonusDc;
        MaxDc = baseMaxDc + sumDc + BonusDc;
        Mc = (Job == 1 ? 2 + Level / 3 : 0) + sumMc + BonusMc;
        MaxMc = (Job == 1 ? 5 + Level : 0) + sumMc + BonusMc;
        Sc = (Job == 2 ? 2 + Level / 3 : 0) + sumSc + BonusSc;
        MaxSc = (Job == 2 ? 5 + Level : 0) + sumSc + BonusSc;
        Ac = sumAc + BonusAc;
        MaxAc = sumAc + BonusAc;
        Mac = sumMac + BonusMac;
        MaxMac = sumMac + BonusMac;
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
    public (bool isParalyzed, int durationSec) TryParalyzeTarget(int targetAntiPoison, bool targetAntiParalysis = false, int rngRoll = -1)
    {
        var res = SpecialRings.TriggerParalysisRing(HasParalysisRing, targetAntiPoison, targetAntiParalysis, rngRoll);
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

    /// <summary>
    /// 服务端权威附着红绿毒 (IDA 0x49350C / sub_4C7720)
    /// </summary>
    public void ApplyPoison(TaoistCombatEngine.PoisonType type, int durationSec, int sc, long nowMs)
    {
        if (type == TaoistCombatEngine.PoisonType.Green)
        {
            HasGreenPoison = true;
            GreenPoisonSc = sc;
            GreenPoisonExpireMs = nowMs + durationSec * 1000L;
            NextGreenPoisonTickMs = nowMs + TaoistCombatEngine.PoisonTickIntervalMs;
            IncHealth = 0; // 绿毒立即清空生命回复池 (IDA 0x4B7100)
        }
        else if (type == TaoistCombatEngine.PoisonType.Red)
        {
            HasRedPoison = true;
            RedPoisonExpireMs = nowMs + durationSec * 1000L;
        }
    }

    /// <summary>
    /// 服务端权威绿毒 Tick 结算 (每 2.5 秒触发一次掉血并清空回血池)
    /// </summary>
    public (bool tookDamage, int damage, bool greenExpired, bool redExpired) ProcessPoisonTick(long nowMs)
    {
        bool tookDamage = false;
        int damage = 0;
        bool greenExpired = false;
        bool redExpired = false;

        if (HasGreenPoison)
        {
            if (nowMs >= GreenPoisonExpireMs)
            {
                HasGreenPoison = false;
                greenExpired = true;
            }
            else if (nowMs >= NextGreenPoisonTickMs)
            {
                NextGreenPoisonTickMs = nowMs + TaoistCombatEngine.PoisonTickIntervalMs;
                damage = TaoistCombatEngine.CalculateGreenPoisonTickDamage(GreenPoisonSc);
                IncHealth = 0; // 封禁回血
                Hp = Math.Max(0, Hp - damage);
                tookDamage = true;
            }
        }

        if (HasRedPoison && nowMs >= RedPoisonExpireMs)
        {
            HasRedPoison = false;
            redExpired = true;
        }

        return (tookDamage, damage, greenExpired, redExpired);
    }

    /// <summary>
    /// 服务端权威计算物理攻击力 (结合武器与项链幸运/诅咒与运九套满攻爆发)
    /// </summary>
    public int CalculateAttackDamageWithLuck(int minDc, int maxDc, Random rng)
    {
        int totalLuck = 0;
        int totalCurse = 0;
        var weapon = Equipment[(int)OfficialEquipSlot.Weapon];
        if (weapon != null)
        {
            totalLuck += weapon.Luck;
            totalCurse += weapon.Curse;
        }
        var neck = Equipment[(int)OfficialEquipSlot.Necklace];
        if (neck != null)
        {
            totalLuck += neck.Luck;
        }
        return ItemStatHiddenEngine.WeaponLuckCurseEngine.CalculateAttackPower(minDc, maxDc, totalLuck, totalCurse, rng);
    }

    /// <summary>
    /// 服务端权威重算三大物理负重上限 (结合职业、等级与超负载戒指)
    /// </summary>
    public void RecalculateAuthoritativeWeights(int bagGold = 0)
    {
        BaseMaxHandWeight = WeightConstraintEngine.CalcMaxHandWeight(Level, Job);
        BaseMaxWearWeight = WeightConstraintEngine.CalcMaxWearWeight(Level, Job);
        BaseMaxBagWeight = WeightConstraintEngine.CalcMaxBagWeight(Level, Job);

        var w = SpecialRings.ApplyOverloadRing(BaseMaxBagWeight, BaseMaxWearWeight, BaseMaxHandWeight, HasOverloadRing);
        MaxBagWeight = w.maxWeight;
        MaxWearWeight = w.maxWearWeight;
        MaxHandWeight = w.maxHandWeight;

        // 计算当前穿戴总重
        int currentWear = 0;
        for (int i = 0; i < Equipment.Length; i++)
        {
            var item = Equipment[i];
            if (item != null && i != (int)OfficialEquipSlot.Weapon && i != (int)OfficialEquipSlot.Lighting)
            {
                currentWear += item.Weight;
            }
        }
        WearWeight = currentWear;

        // 计算当前手持重量
        var weapon = Equipment[(int)OfficialEquipSlot.Weapon];
        HandWeight = weapon?.Weight ?? 0;

        // 计算背包负重
        int curBag = WeightConstraintEngine.CalcGoldWeight(bagGold);
        for (int i = 0; i < BagItems.Count; i++)
        {
            curBag += BagItems[i].Weight;
        }
        BagWeight = curBag;
    }

    /// <summary>
    /// 服务端权威校验穿戴装备是否超过腕力或穿戴负重
    /// </summary>
    public bool CheckCanEquipAuthoritative(OfficialEquipSlot slot, int itemWeight, out string reason)
    {
        return WeightConstraintEngine.CheckCanEquip(Level, Job, (int)slot, itemWeight, WearWeight, out reason);
    }

    public long Exp { get; set; } = 0;
    public long MaxExp => ExpTable.ExpForLevel(Level);
    public int BonusPoint { get; set; } = 0;

    /// <summary>
    /// 盛大官方 1.5 权威生命与魔法基础上限成长计算 (按职业与等级)
    /// </summary>
    public void RecalculateBaseHpMp()
    {
        switch (Job)
        {
            case 0: // 战士
                BaseMaxHp = 19 + (int)((Level - 1) * 4.5);
                BaseMaxMp = 14 + (Level - 1) * 2;
                break;
            case 1: // 法师
                BaseMaxHp = 14 + (int)((Level - 1) * 1.8);
                BaseMaxMp = 19 + (int)((Level - 1) * 9.5);
                break;
            case 2: // 道士
                BaseMaxHp = 17 + (int)((Level - 1) * 3.0);
                BaseMaxMp = 17 + (int)((Level - 1) * 6.0);
                break;
            default:
                BaseMaxHp = 20 + Level * 3;
                BaseMaxMp = 20 + Level * 3;
                break;
        }

        RecalculateAuthoritativeWeights(Gold);
        RecalculateAttributes();
    }

    /// <summary>
    /// 经验获取与连续升级权威判定
    /// </summary>
    public bool GainExp(long amount)
    {
        if (amount <= 0) return false;
        Exp += amount;
        bool leveledUp = false;
        while (Level < ExpTable.NeedExp.Length - 1 && Exp >= ExpTable.ExpForLevel(Level))
        {
            Exp -= ExpTable.ExpForLevel(Level);
            Level++;
            BonusPoint += 3; // 每升一级给予 3 点自由属性点
            leveledUp = true;
        }

        if (leveledUp)
        {
            RecalculateBaseHpMp();
            Hp = MaxHp;
            Mp = MaxMp;
        }
        return leveledUp;
    }
}
