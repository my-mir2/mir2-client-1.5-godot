using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Rules;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 装备生命周期与沙巴克武器升级权威引擎 (ItemLifecycleEngine)
/// 严格 1:1 对齐官方 M2Server 物理反编译代码:
/// - 装备持久物理磨损衰减: 物理攻击 1/30 武器掉持久, 受击衣服 1/15、防具首饰 1/40 掉持久
/// - 武器持久归 0 折断失效 (攻击力降为拳头伤害)
/// - 沙巴克武器升级全生命周期: 提交材料 -> 黑铁矿纯度/首饰极值算率 -> 1小时出炉倒计时 -> 取回开光 -> 第一刀试刀/碎刀
/// - 祝福油与武器诅咒状态机: 喝油幸运 +1~+7、运九套满攻暴击、杀人或喝油失败诅咒 +1~+10
/// </summary>
public static class ItemLifecycleEngine
{
    // ==================== 1. 装备持久物理磨损与折断判定 ====================

    /// <summary>
    /// 物理挥刀或技能攻击时执行手持武器持久磨损 (官方 1/30 几率扣减 1 点持久)
    /// </summary>
    public static (bool deducted, bool broken) DeductWeaponDurabilityOnAttack(PlayerState state, Random rng)
    {
        var weapon = state.Equipment[(int)OfficialEquipSlot.Weapon];
        if (weapon == null) return (false, false);

        if (weapon.Dura <= 0)
        {
            return (false, true); // 已经折断失效
        }

        // 官方 1/30 磨损几率 (0x4BB6C8)
        if (rng.Next(30) == 0)
        {
            weapon.Dura = Math.Max(0, weapon.Dura - 1);
            if (weapon.Dura == 0)
            {
                state.RecalculateAttributes();
                return (true, true); // 本次挥刀导致折断
            }
            return (true, false);
        }

        return (false, false);
    }

    /// <summary>
    /// 角色受到伤害时执行防具与首饰持久磨损 (衣服 1/15, 头盔/手镯/戒指/项链 1/40)
    /// </summary>
    public static List<OfficialEquipSlot> DeductArmorDurabilityOnDamage(PlayerState state, Random rng)
    {
        var wornSlots = new List<OfficialEquipSlot>();

        for (int i = 0; i < 9; i++)
        {
            var slot = (OfficialEquipSlot)i;
            if (slot == OfficialEquipSlot.Weapon || slot == OfficialEquipSlot.Lighting) continue;

            var equip = state.Equipment[i];
            if (equip == null || equip.Dura <= 0) continue;

            int rate = (slot == OfficialEquipSlot.Dress) ? 15 : 40;
            if (rng.Next(rate) == 0)
            {
                equip.Dura = Math.Max(0, equip.Dura - 1);
                wornSlots.Add(slot);
            }
        }

        if (wornSlots.Count > 0)
        {
            state.RecalculateAttributes();
        }

        return wornSlots;
    }

    /// <summary>
    /// 铁匠铺装备修理 (普修掉持久上限，特修完美恢复持久)
    /// </summary>
    public static bool RepairEquip(EquipItem item, bool isSpecialRepair)
    {
        if (item == null) return false;

        if (isSpecialRepair)
        {
            // 特修：完全恢复持久，不伤最大持久
            item.Dura = item.MaxDura;
            return true;
        }
        else
        {
            // 普修：掉 1/10 最大持久上限
            int loss = Math.Max(1, item.MaxDura / 10);
            item.MaxDura = Math.Max(1, item.MaxDura - loss);
            item.Dura = item.MaxDura;
            return true;
        }
    }

    // ==================== 2. 沙巴克武器升级全生命周期管理器 ====================

    public sealed class WeaponUpgradeRecord
    {
        public int SessionId { get; init; }
        public string CharacterName { get; init; } = string.Empty;
        public EquipItem Weapon { get; init; } = null!;
        public long SubmitTimeMs { get; init; }
        public long ReadyTimeMs { get; init; } // 官方 1 小时真实时间 (3600_000 ms)
        public int ResultCode { get; init; }   // 1=碎刀, 10..12=DC, 20..22=MC, 30..32=SC
        public int DuraChange { get; init; }   // 黑铁矿决定的持久增减
    }

    private static readonly ConcurrentDictionary<string, WeaponUpgradeRecord> _upgradeRecords = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// 提交武器升级 (校验材料、计算持久因子与成功率、收走材料并开启 1 小时倒计时)
    /// </summary>
    public static (bool success, string message) SubmitWeaponUpgrade(
        ClientSession session,
        int weaponSlot,
        IReadOnlyList<int> oreBagSlots,
        IReadOnlyList<int> accessoryBagSlots,
        Random rng,
        long nowMs,
        long durationMs = 3600_000) // 默认 1 小时出炉 (单测可自定义更短时间)
    {
        if (session.State == null) return (false, "角色状态异常。");
        if (_upgradeRecords.ContainsKey(session.CharacterName))
        {
            return (false, "你已经有一把武器在铁匠火炉中淬炼，请先取回！");
        }

        // 1. 校验武器
        if (weaponSlot < 0 || weaponSlot >= session.State.BagItems.Count)
        {
            return (false, "请指定要升级的武器。");
        }
        var weapon = session.State.BagItems[weaponSlot];
        // 盛大官方 1.5 铁律: 沙巴克武器升级累计最高 7 点属性 (UpgradeCount >= 7)
        if (weapon.UpgradeCount >= 7)
        {
            return (false, "该武器已达到累计 7 次修炼极限，无法继续升级！");
        }

        // 2. 收集黑铁矿石与首饰
        var ores = new List<WeaponUpgradeEngine.BlackIronOre>();
        var oreItems = new List<EquipItem>();
        for (int i = 0; i < oreBagSlots.Count; i++)
        {
            int slot = oreBagSlots[i];
            if (slot >= 0 && slot < session.State.BagItems.Count)
            {
                var item = session.State.BagItems[slot];
                if (item.Name.Contains("黑铁矿"))
                {
                    // 黑铁矿纯度取 Shape 或 Weight，默认 10~25
                    int purity = item.Shape > 0 ? item.Shape : Math.Max(5, item.Weight);
                    ores.Add(new WeaponUpgradeEngine.BlackIronOre(purity));
                    oreItems.Add(item);
                }
            }
        }

        if (ores.Count == 0)
        {
            return (false, "武器升级必须至少放入 1 块黑铁矿石以保证持久稳定！");
        }

        var accessories = new List<WeaponUpgradeEngine.AccessoryInfo>();
        var accItems = new List<EquipItem>();
        for (int i = 0; i < accessoryBagSlots.Count; i++)
        {
            int slot = accessoryBagSlots[i];
            if (slot >= 0 && slot < session.State.BagItems.Count)
            {
                var item = session.State.BagItems[slot];
                if (item != weapon && !oreItems.Contains(item))
                {
                    // 首饰: 项链/手镯/戒指
                    accessories.Add(new WeaponUpgradeEngine.AccessoryInfo(item.Shape, item.Dc, item.Mc, item.Sc));
                    accItems.Add(item);
                }
            }
        }

        if (accessories.Count == 0)
        {
            return (false, "武器升级必须至少放入 1 件首饰以引导属性淬炼方向！");
        }

        // 3. 权威精算持久升降与升级结果代码 (对齐 WeaponUpgradeEngine)
        var upgradeParams = WeaponUpgradeEngine.CalcWeaponUpgradeParams(ores, accessories, tieBreakerRoll: rng.Next(3));
        int duraChange = WeaponUpgradeEngine.CalcWeaponDuraChange(upgradeParams.DuraFactor, rng.Next(10), rng.Next(10));
        int rate = WeaponUpgradeEngine.CalcWeaponUpgradeRate(weapon.Luck, weapon.UpgradeCount, upgradeParams.StatVal, session.State.PkPoints);
        int resultCode = WeaponUpgradeEngine.DetermineUpgradeResultCode(rate, rng.Next(100), rng.Next(30), rng.Next(200), upgradeParams.StatType);

        // 4. 从背包扣除武器与材料
        session.State.BagItems.Remove(weapon);
        for (int i = 0; i < oreItems.Count; i++) session.State.BagItems.Remove(oreItems[i]);
        for (int i = 0; i < accItems.Count; i++) session.State.BagItems.Remove(accItems[i]);

        // 5. 保存升级大账本记录
        var record = new WeaponUpgradeRecord
        {
            SessionId = session.SessionId,
            CharacterName = session.CharacterName,
            Weapon = weapon,
            SubmitTimeMs = nowMs,
            ReadyTimeMs = nowMs + durationMs,
            ResultCode = resultCode,
            DuraChange = duraChange
        };
        _upgradeRecords[session.CharacterName] = record;

        return (true, "武器与淬炼材料已送入沙巴克炼炉！请在 1 小时后前来取回！");
    }

    /// <summary>
    /// 取回升级出炉的武器 (1 小时倒计时校验，附带已开光待试刀标记)
    /// </summary>
    public static (bool success, EquipItem? weapon, string message) RetrieveUpgradedWeapon(ClientSession session, long nowMs)
    {
        if (session.State == null) return (false, null, "角色状态异常。");
        if (!_upgradeRecords.TryGetValue(session.CharacterName, out var record))
        {
            return (false, null, "你没有寄存升级任何武器。");
        }

        if (nowMs < record.ReadyTimeMs)
        {
            long remainSec = Math.Max(1, (record.ReadyTimeMs - nowMs) / 1000);
            return (false, null, $"武器仍在猛火淬炼中，还需等待 {remainSec / 60} 分 {remainSec % 60} 秒出炉！");
        }

        if (session.State.BagItems.Count >= 40)
        {
            return (false, null, "背包已满，无法取回武器！请先清理背包空位。");
        }

        // 移除升级记录并交付武器
        _upgradeRecords.TryRemove(session.CharacterName, out _);
        var weapon = record.Weapon;
        weapon.UpgradeState = record.ResultCode; // 注入升级结果码 (1=碎刀, 10..12=DC, 20..22=MC, 30..32=SC)
        weapon.MaxDura = Math.Max(1, weapon.MaxDura + record.DuraChange);
        weapon.Dura = Math.Min(weapon.Dura, weapon.MaxDura);

        session.State.BagItems.Add(weapon);
        return (true, weapon, "你的武器已经淬炼完毕出炉！请拿好，第一刀即可见分晓！");
    }

    // ==================== 3. 祝福油与武器诅咒状态机 ====================

    /// <summary>
    /// 使用背包中的祝福油为手持武器涂抹开光
    /// </summary>
    public static (bool success, string message) ApplyBlessOilToEquippedWeapon(ClientSession session, Random rng)
    {
        var (succ, msg, _, _) = ApplyBlessOilToEquippedWeaponEx(session, rng);
        return (succ, msg);
    }

    /// <summary>
    /// 使用背包中的祝福油为手持武器涂抹开光扩展版本 (返回被消耗的祝福油物品与背包槽位，供网络层下发 SM_DELITEM 物理清格)
    /// </summary>
    public static (bool success, string message, EquipItem? oilItem, int oilSlot) ApplyBlessOilToEquippedWeaponEx(ClientSession session, Random rng)
    {
        if (session.State == null) return (false, "角色状态异常。", null, -1);

        // 1. 校验手持武器
        var weapon = session.State.Equipment[(int)OfficialEquipSlot.Weapon];
        if (weapon == null)
        {
            return (false, "你手中未握持武器，无法使用祝福油！", null, -1);
        }

        // 2. 校验并扣除背包祝福油与负重
        int oilSlot = session.State.BagItems.FindIndex(i => i.Name.Contains("祝福油"));
        if (oilSlot < 0)
        {
            return (false, "背包中没有祝福油！", null, -1);
        }
        var oilItem = session.State.BagItems[oilSlot];
        session.State.BagItems.RemoveAt(oilSlot);
        session.State.BagWeight = Math.Max(0, session.State.BagWeight - oilItem.Weight);

        // 3. 代入官方 1.5 祝福油判定引擎 (PlayerAuditEngine.AuditDrinkBlessOil)
        int maxDc = session.State.MaxDc;
        int minDc = session.State.Dc;
        int roll100 = rng.Next(100);
        int rollSub = rng.Next(1000);

        var res = PlayerAuditEngine.AuditDrinkBlessOil(maxDc, minDc, weapon.Luck, weapon.Curse, roll100, rollSub);

        if (res.Cursed)
        {
            weapon.Curse = res.NewCurse;
            weapon.Luck = res.NewLuck;
            session.State.RecalculateAttributes();
            return (false, "一股不祥的灰雾笼罩了刀身！武器被诅咒了！", oilItem, oilSlot);
        }
        else if (res.CurseCleared)
        {
            weapon.Curse = res.NewCurse;
            session.State.RecalculateAttributes();
            return (true, "神圣的油香驱散了刀身上的邪气！武器诅咒被净化了！", oilItem, oilSlot);
        }
        else if (res.Success)
        {
            weapon.Luck = res.NewLuck;
            session.State.RecalculateAttributes();
            return (true, $"圣光闪耀！武器被祝福了！当前幸运: +{weapon.Luck}！", oilItem, oilSlot);
        }
        else
        {
            return (true, "药油渗透进刀纹，刀身毫无变化。", oilItem, oilSlot);
        }
    }

    /// <summary>
    /// 恶意击杀白名玩家增加 PK 点数时，手持武器有几率被加深诅咒 (IDA 0x4C1EF4)
    /// </summary>
    public static bool CheckWeaponCurseOnMurder(PlayerState killerState, Random rng)
    {
        var weapon = killerState.Equipment[(int)OfficialEquipSlot.Weapon];
        if (weapon == null) return false;

        // 25% 几率加深诅咒
        if (rng.Next(4) == 0)
        {
            weapon.Curse = Math.Min(10, weapon.Curse + 1);
            weapon.Luck = Math.Max(0, weapon.Luck - 1);
            killerState.RecalculateAttributes();
            return true;
        }
        return false;
    }
}
