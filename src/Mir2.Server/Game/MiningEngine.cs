using System;
using Mir2.Shared.Models;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 矿区挖矿与矿石纯度产出引擎 (MiningEngine)
/// 严格 1:1 对齐官方 M2Server 规则:
/// 1. 挖掘门槛:
///    - 必须处于矿区地图 (如比奇废矿区 MapId 1/2)
///    - 手中握持武器必须为“鹤嘴锄” (铁鹤嘴锄)
/// 2. 挖掘心跳磨损:
///    - 每次挖掘有 1/10 几率扣除鹤嘴锄 1 点持久
/// 3. 矿石种类与纯度产出:
///    - 铜矿 (纯度 1~15)
///    - 铁矿 (纯度 1~18)
///    - 银矿 (纯度 1~20)
///    - 金矿 (纯度 1~25, 稀有)
///    - 黑铁矿石 (纯度 5~30, 极为珍贵，武器升级决定持久的唯一材料)
/// </summary>
public static class MiningEngine
{
    public static bool IsMiningMap(int mapId)
    {
        // 官方矿区地图编号: 1 (比奇矿区), 2 (矿区废桥/东部等), 15 (封魔矿区)
        return mapId == 1 || mapId == 2 || mapId == 15;
    }

    public static bool IsMiningMap(string? mapName)
    {
        if (string.IsNullOrWhiteSpace(mapName)) return false;
        // 盛大官方 1.5 权威矿区地图编号前缀: D021~D024 (比奇矿区各层), D401 (封魔矿区), 矿区
        return mapName.StartsWith("D02", StringComparison.OrdinalIgnoreCase) ||
               mapName.StartsWith("D401", StringComparison.OrdinalIgnoreCase) ||
               mapName.Contains("矿区");
    }

    public static bool IsPickaxe(EquipItem? weapon)
    {
        return weapon != null && weapon.Name.Contains("鹤嘴锄");
    }

    public sealed class MineResult
    {
        public bool Success { get; set; }
        public bool PickaxeWorn { get; set; }
        public int PickaxeDura { get; set; }
        public int PickaxeMaxDura { get; set; }
        public EquipItem? MinedOre { get; set; }
        public bool PickaxeBroken { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    /// <summary>
    /// 执行一次挖矿动作结算
    /// </summary>
    public static MineResult DigOnce(PlayerState state, int mapId, Random rng)
    {
        var result = new MineResult();
        if (state == null)
        {
            result.Message = "状态异常。";
            return result;
        }

        if (!IsMiningMap(mapId))
        {
            result.Message = "这里不是矿区，无法挖矿。";
            return result;
        }

        var weapon = state.Equipment[(int)OfficialEquipSlot.Weapon];
        if (!IsPickaxe(weapon))
        {
            result.Message = "没有装备鹤嘴锄，无法挖矿。";
            return result;
        }

        result.PickaxeDura = weapon!.Dura;
        result.PickaxeMaxDura = weapon.MaxDura;

        if (weapon.Dura <= 0)
        {
            result.PickaxeBroken = true;
            result.Message = "鹤嘴锄已损坏，无法继续挖矿。";
            return result;
        }

        // 1. 鹤嘴锄磨损 (1/10 几率)
        if (rng.Next(10) == 0)
        {
            weapon.Dura = Math.Max(0, weapon.Dura - 1);
            result.PickaxeWorn = true;
            result.PickaxeDura = weapon.Dura;
            result.PickaxeMaxDura = weapon.MaxDura;
            if (weapon.Dura == 0)
            {
                result.PickaxeBroken = true;
                result.Message = "鹤嘴锄损坏了。";
                return result;
            }
        }

        // 2. 出矿判定 (30% 几率挖出矿石)
        if (rng.Next(100) < 30)
        {
            if (state.BagItems.Count >= 40)
            {
                result.Message = "背包已满。";
                return result;
            }

            // 矿石种类骰子: 40% 铜矿, 30% 铁矿, 18% 银矿, 8% 金矿, 4% 黑铁矿石
            int oreRoll = rng.Next(100);
            string oreName;
            int purity;

            if (oreRoll < 40)
            {
                oreName = "铜矿";
                purity = 1 + rng.Next(15);
            }
            else if (oreRoll < 70)
            {
                oreName = "铁矿";
                purity = 1 + rng.Next(18);
            }
            else if (oreRoll < 88)
            {
                oreName = "银矿";
                purity = 1 + rng.Next(20);
            }
            else if (oreRoll < 96)
            {
                oreName = "金矿";
                purity = 1 + rng.Next(25);
            }
            else
            {
                oreName = "黑铁矿石";
                purity = 5 + rng.Next(26); // 纯度 5~30
            }

            int oreWeight = Math.Max(1, purity / 5);
            // 盛大官方 1.5 铁律：背包负重严格限制，超重无法将矿石放入包裹
            if (state.BagWeight + oreWeight > state.MaxBagWeight)
            {
                result.Message = "太重了拿不动，负重已满。";
                return result;
            }

            var ore = new EquipItem
            {
                Uid = rng.Next(100000, 999999),
                Name = oreName,
                Shape = purity, // Shape 字段记录纯度
                Weight = oreWeight,
                Dura = purity,
                MaxDura = purity
            };

            state.BagItems.Add(ore);
            state.BagWeight += oreWeight;
            result.Success = true;
            result.MinedOre = ore;
            result.Message = $"获得了 {oreName}(纯度 {purity})。";
            return result;
        }

        result.Message = string.Empty; // 官方原版未挖到时不产生文本刷屏
        return result;
    }
}
