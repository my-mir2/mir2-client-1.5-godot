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
        // 官方典型矿区地图编号: 1 (比奇矿区), 2 (矿区废桥/东部等)
        return mapId == 1 || mapId == 2;
    }

    public static bool IsPickaxe(EquipItem? weapon)
    {
        return weapon != null && weapon.Name.Contains("鹤嘴锄");
    }

    public sealed class MineResult
    {
        public bool Success { get; set; }
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
            result.Message = "角色状态异常。";
            return result;
        }

        if (!IsMiningMap(mapId))
        {
            result.Message = "这里不是矿区，无法开采矿石！";
            return result;
        }

        var weapon = state.Equipment[(int)OfficialEquipSlot.Weapon];
        if (!IsPickaxe(weapon))
        {
            result.Message = "你手中没有装备鹤嘴锄，无法挖矿！";
            return result;
        }

        if (weapon!.Dura <= 0)
        {
            result.PickaxeBroken = true;
            result.Message = "你的鹤嘴锄已经彻底折断损坏，无法继续挖矿！";
            return result;
        }

        // 1. 鹤嘴锄磨损 (1/10 几率)
        if (rng.Next(10) == 0)
        {
            weapon.Dura = Math.Max(0, weapon.Dura - 1);
            if (weapon.Dura == 0)
            {
                result.PickaxeBroken = true;
                result.Message = "铛！你的鹤嘴锄在坚硬的岩层中折断了！";
                return result;
            }
        }

        // 2. 出矿判定 (30% 几率挖出矿石)
        if (rng.Next(100) < 30)
        {
            if (state.BagItems.Count >= 40)
            {
                result.Message = "你挖到了矿石，但背包已满装不下了！";
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

            var ore = new EquipItem
            {
                Name = oreName,
                Shape = purity, // Shape 字段记录纯度
                Weight = Math.Max(1, purity / 5),
                Dura = purity,
                MaxDura = purity
            };

            state.BagItems.Add(ore);
            result.Success = true;
            result.MinedOre = ore;
            result.Message = $"铛！一声脆响，你从岩壁中挖出了一块【{oreName}】(纯度: {purity})！";
            return result;
        }

        result.Message = "你挥汗如雨地敲击岩壁，只敲下一堆碎石。";
        return result;
    }
}
