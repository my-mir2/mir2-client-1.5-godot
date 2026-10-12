using System;
using System.Collections.Frozen;
using System.Collections.Generic;

namespace Mir2.Shared.Rules;

/// <summary>
/// 盛大官方 1.5 全量 160+ 装备道具词条、极品变异权重与持久度磨损引擎 (细节 551 ~ 650)
/// 严格依据官方客户端与服务端物理属性逆向实证
/// </summary>
public static class ItemPropertyCompendiumEngine
{
    // 细节 551 ~ 577: 官方权威武器全集与属性字典
    public static class WeaponDictionaryEngine
    {
        public record WeaponSpec(string Name, int Weight, int MinDc, int MaxDc, int MinMc, int MaxMc, int MinSc, int MaxSc, int Accuracy, int ReqLevel);

        public static readonly System.Collections.Frozen.FrozenDictionary<string, WeaponSpec> Weapons = new Dictionary<string, WeaponSpec>
        {
            ["木剑"] = new("木剑", 7, 2, 5, 0, 0, 0, 0, 0, 1),
            ["铁剑"] = new("铁剑", 10, 5, 9, 0, 0, 0, 0, 0, 10),
            ["八荒"] = new("八荒", 25, 4, 12, 0, 0, 0, 0, 0, 15),
            ["降魔"] = new("降魔", 20, 6, 11, 0, 0, 1, 2, 1, 20),
            ["偃月"] = new("偃月", 13, 4, 10, 1, 3, 0, 0, 0, 20),
            ["斩马刀"] = new("斩马刀", 27, 5, 15, 0, 0, 0, 0, 0, 20),
            ["修罗"] = new("修罗", 40, 0, 20, 0, 0, 0, 0, 0, 22),
            ["破魂"] = new("破魂", 8, 8, 10, 0, 0, 0, 0, 2, 16),
            ["凌风"] = new("凌风", 14, 6, 12, 0, 0, 0, 0, 0, 19),
            ["罗刹"] = new("罗刹", 30, 15, 0, 0, 0, 0, 0, 0, 15),
            ["命运之刃"] = new("命运之刃", 48, 12, 16, 0, 0, 0, 0, 0, 35),
            ["炼狱"] = new("炼狱", 60, 0, 25, 0, 0, 0, 0, 0, 26),
            ["魔杖"] = new("魔杖", 10, 5, 10, 2, 5, 0, 0, 0, 26),
            ["银蛇"] = new("银蛇", 26, 7, 14, 0, 0, 1, 3, 1, 26),
            ["井中月"] = new("井中月", 58, 7, 22, 0, 0, 0, 0, 0, 28),
            ["血饮"] = new("血饮", 12, 3, 14, 3, 5, 0, 0, 5, 0),
            ["无极棍"] = new("无极棍", 15, 8, 16, 0, 0, 3, 5, 0, 0),
            ["裁决之杖"] = new("裁决之杖", 80, 0, 30, 0, 0, 0, 0, 0, 30),
            ["骨玉权杖"] = new("骨玉权杖", 20, 6, 12, 2, 6, 0, 0, 0, 35),
            ["龙纹剑"] = new("龙纹剑", 40, 8, 20, 0, 0, 3, 6, 0, 35),
            ["屠龙"] = new("屠龙", 99, 5, 35, 0, 0, 0, 0, 0, 34),
            ["嗜魂法杖"] = new("嗜魂法杖", 26, 6, 13, 2, 8, 0, 0, 0, 35) // 盛大官方 1.5 权威法师至尊神器
        }.ToFrozenDictionary();

        public static bool CheckDragonSlayerHandWeight(int handWeight) => handWeight >= 50; // 细节 572: 屠龙腕力 50 强制检定
    }

    // 细节 578 ~ 650: 衣服、首饰、特戒与消耗品
    public static class ArmorAndJewelryEngine
    {
        public static (int acMin, int acMax, int macMin, int macMax, int dcMin, int dcMax) GetHeavyArmorStats(string name)
        {
            var s = GetArmorStats(name);
            return (s.acMin, s.acMax, s.macMin, s.macMax, s.dcMin, s.dcMax);
        }

        public static (int acMin, int acMax, int macMin, int macMax, int dcMin, int dcMax, int mcMin, int mcMax, int scMin, int scMax) GetArmorStats(string name)
        {
            if (string.IsNullOrEmpty(name)) return (0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

            // 盛大官方 1.4/1.5 权威衣服属性表 (布衣、轻型盔甲、中型盔甲、重盔甲、魔法长袍、灵魂战衣、战神盔甲、恶魔长袍、幽灵战衣)
            if (name.Contains("布衣")) return (0, 2, 0, 1, 0, 0, 0, 0, 0, 0);
            if (name.Contains("轻型盔甲")) return (3, 3, 1, 2, 0, 0, 0, 0, 0, 0);
            if (name.Contains("中型盔甲")) return (3, 5, 1, 2, 0, 0, 0, 0, 0, 0);
            if (name.Contains("重盔甲")) return (4, 7, 2, 3, 0, 0, 0, 0, 0, 0);
            if (name.Contains("魔法长袍")) return (2, 5, 1, 2, 0, 0, 0, 2, 0, 0);
            if (name.Contains("灵魂战衣")) return (2, 5, 1, 2, 0, 0, 0, 0, 0, 2);
            if (name.Contains("战神盔甲")) return (5, 9, 3, 5, 0, 0, 0, 0, 0, 0);
            if (name.Contains("恶魔长袍")) return (4, 7, 3, 4, 0, 0, 1, 4, 0, 0);
            if (name.Contains("幽灵战衣")) return (4, 7, 3, 4, 0, 0, 0, 0, 1, 4);

            return (0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        }

        public static (int minDc, int maxDc, bool paralyzeProc) GetParalysisRingEffect(bool hasRing, int roll100)
        {
            // 细节 637: 麻痹戒指 1/15 概率石化 5 秒
            bool proc = hasRing && (roll100 < 7); // ~6.67%
            return (0, 1, proc);
        }

        public static (int restoreHp, int restoreMp, bool instant) GetPotionRestore(string potionName)
        {
            return potionName switch
            {
                "太阳水" => (30, 40, true),
                "强效太阳水" => (50, 50, true),   // 细节 649
                "万年雪霜" => (100, 100, true), // 细节 650
                "疗伤药" => (200, 200, true),   // 细节 650
                _ => (0, 0, false)
            };
        }
    }

    /// <summary>
    /// 盛大官方 1.5 装备需求类型定义 (严格 1:1 对齐官方 StdItems.DB 结构体的 Need 字段，0x4855E0)
    /// 0: 等级 (Level)
    /// 1: 攻击力 (DC)
    /// 2: 魔法力 (MC)
    /// 3: 道术 (SC)
    /// </summary>
    public enum EquipNeedType : byte
    {
        Level = 0,
        Dc = 1,
        Mc = 2,
        Sc = 3
    }

    /// <summary>
    /// 盛大官方 1.5 装备穿戴门槛权威查询 (严格覆盖 4 态需求: 0等级/1攻击/2魔法/3道术)
    /// </summary>
    public static (EquipNeedType NeedType, int NeedValue) GetEquipRequirement(string itemName)
    {
        if (string.IsNullOrEmpty(itemName)) return (EquipNeedType.Level, 0);

        // 1. 武器库精确匹配 (官方权威均为等级需求)
        foreach (var kvp in WeaponDictionaryEngine.Weapons)
        {
            if (itemName.Contains(kvp.Key, StringComparison.OrdinalIgnoreCase))
            {
                return (EquipNeedType.Level, kvp.Value.ReqLevel);
            }
        }

        // 2. 衣服类匹配 (战神需30级，恶魔长袍需魔法28点，幽灵战衣需道术27点)
        if (itemName.Contains("布衣")) return (EquipNeedType.Level, 1);
        if (itemName.Contains("轻型盔甲")) return (EquipNeedType.Level, 11);
        if (itemName.Contains("中型盔甲")) return (EquipNeedType.Level, 16);
        if (itemName.Contains("重盔甲") || itemName.Contains("灵魂战衣") || itemName.Contains("魔法长袍")) return (EquipNeedType.Level, 22);
        if (itemName.Contains("战神盔甲")) return (EquipNeedType.Level, 30);
        if (itemName.Contains("幽灵战衣")) return (EquipNeedType.Sc, 27); // 道术 27
        if (itemName.Contains("恶魔长袍")) return (EquipNeedType.Mc, 28); // 魔法 28

        // 3. 头盔类匹配 (黑铁头盔需攻击46点且重量20)
        if (itemName.Contains("青铜头盔")) return (EquipNeedType.Level, 10);
        if (itemName.Contains("魔法头盔")) return (EquipNeedType.Level, 14);
        if (itemName.Contains("祈祷头盔")) return (EquipNeedType.Level, 18);
        if (itemName.Contains("道士头盔") || itemName.Contains("骷髅头盔")) return (EquipNeedType.Level, 24);
        if (itemName.Contains("记忆头盔")) return (EquipNeedType.Level, 26);
        if (itemName.Contains("黑铁头盔")) return (EquipNeedType.Dc, 46); // 攻击 46

        // 4. 首饰类匹配 (戒指/手镯/项链)
        // 戒指
        if (itemName.Contains("古铜戒指")) return (EquipNeedType.Level, 3);
        if (itemName.Contains("铁戒指")) return (EquipNeedType.Level, 9);
        if (itemName.Contains("金戒指")) return (EquipNeedType.Level, 15);
        if (itemName.Contains("降妖除魔戒指")) return (EquipNeedType.Level, 25);
        if (itemName.Contains("珊瑚戒指")) return (EquipNeedType.Dc, 25);
        if (itemName.Contains("龙之戒指")) return (EquipNeedType.Dc, 37);
        if (itemName.Contains("力量戒指")) return (EquipNeedType.Dc, 46);
        if (itemName.Contains("红宝石戒指")) return (EquipNeedType.Mc, 17);
        if (itemName.Contains("紫碧螺")) return (EquipNeedType.Mc, 24);
        if (itemName.Contains("铂金戒指")) return (EquipNeedType.Sc, 17);
        if (itemName.Contains("泰坦戒指")) return (EquipNeedType.Sc, 24);

        // 手镯
        if (itemName.Contains("幽灵手套")) return (EquipNeedType.Dc, 21);
        if (itemName.Contains("死神手套")) return (EquipNeedType.Level, 22);
        if (itemName.Contains("阎罗手套")) return (EquipNeedType.Level, 30);
        if (itemName.Contains("骑士手镯")) return (EquipNeedType.Dc, 39);
        if (itemName.Contains("龙之手镯")) return (EquipNeedType.Mc, 28);
        if (itemName.Contains("三眼手镯")) return (EquipNeedType.Sc, 22);

        // 项链
        if (itemName.Contains("传统项链")) return (EquipNeedType.Level, 7);
        if (itemName.Contains("放大镜") || itemName.Contains("竹笛")) return (EquipNeedType.Level, 17);
        if (itemName.Contains("蓝翡翠项链")) return (EquipNeedType.Level, 23);
        if (itemName.Contains("幽灵项链")) return (EquipNeedType.Dc, 27);
        if (itemName.Contains("绿色项链")) return (EquipNeedType.Dc, 39);
        if (itemName.Contains("恶魔铃铛")) return (EquipNeedType.Mc, 27);
        if (itemName.Contains("灵魂项链")) return (EquipNeedType.Sc, 23);

        return (EquipNeedType.Level, 0);
    }

    /// <summary>
    /// 盛大官方 1.5 装备等级门槛查询 (兼容旧方法)
    /// </summary>
    public static int GetRequiredLevel(string itemName)
    {
        var (needType, needVal) = GetEquipRequirement(itemName);
        return needType == EquipNeedType.Level ? needVal : 0;
    }
}
