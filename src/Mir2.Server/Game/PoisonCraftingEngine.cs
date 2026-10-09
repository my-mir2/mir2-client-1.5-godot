using System;
using System.Collections.Generic;
using Mir2.Shared.Models;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 传统道士制毒配方合成权威引擎 (PoisonCraftingEngine)
/// 严格 1:1 对齐官方 M2Server 银杏村老道士/比奇药店制毒生态:
/// 1. 绿毒配方 (灰色药粉 - 造成周期扣血与封锁回血):
///    - 食人树叶 4 片 + 食人树果实 1 个 + 毒蜘蛛牙齿 2 个
///    - 合成: 灰色药粉(中) (持久 50 点)
/// 2. 红毒配方 (黄色药粉 - 造成破甲易伤 20%):
///    - 食人树叶 4 片 + 蝎子尾巴 1 个 + 蛆卵 4 个
///    - 合成: 黄色药粉(中) (持久 50 点)
/// </summary>
public static class PoisonCraftingEngine
{
    public enum PoisonType
    {
        GreenPoisonGrayPowder = 0, // 灰色药粉 (绿毒)
        RedPoisonYellowPowder = 1  // 黄色药粉 (红毒)
    }

    /// <summary>
    /// 检查并扣除背包材料合成道士毒药
    /// </summary>
    public static (bool success, EquipItem? craftedPoison, string message) CraftPoison(
        PlayerState player,
        PoisonType type)
    {
        if (player == null) return (false, null, "角色状态异常。");

        if (player.BagItems.Count >= 40)
        {
            return (false, null, "背包已满，无法容纳调制好的毒药！");
        }

        if (type == PoisonType.GreenPoisonGrayPowder)
        {
            // 需要: 食人树叶 4, 食人树果实 1, 毒蜘蛛牙齿 2
            int leafCount = CountItems(player.BagItems, "食人树叶");
            int fruitCount = CountItems(player.BagItems, "食人树果实");
            int toothCount = CountItems(player.BagItems, "毒蜘蛛牙齿");

            if (leafCount < 4 || fruitCount < 1 || toothCount < 2)
            {
                return (false, null, $"制作灰色药粉材料不足！需要: 食人树叶(4)当前{leafCount}, 食人树果实(1)当前{fruitCount}, 毒蜘蛛牙齿(2)当前{toothCount}！");
            }

            RemoveItems(player.BagItems, "食人树叶", 4);
            RemoveItems(player.BagItems, "食人树果实", 1);
            RemoveItems(player.BagItems, "毒蜘蛛牙齿", 2);

            var grayPowder = new EquipItem
            {
                Name = "灰色药粉(中)",
                Dura = 50,
                MaxDura = 50,
                Weight = 1
            };
            player.BagItems.Add(grayPowder);

            return (true, grayPowder, "毒汁在药臼中泛起诡异的暗灰泡沫，成功调制出一包【灰色药粉(中)】(持久 50 点)！");
        }
        else
        {
            // 需要: 食人树叶 4, 蝎子尾巴 1, 蛆卵 4
            int leafCount = CountItems(player.BagItems, "食人树叶");
            int tailCount = CountItems(player.BagItems, "蝎子尾巴");
            int eggCount = CountItems(player.BagItems, "蛆卵");

            if (leafCount < 4 || tailCount < 1 || eggCount < 4)
            {
                return (false, null, $"制作黄色药粉材料不足！需要: 食人树叶(4)当前{leafCount}, 蝎子尾巴(1)当前{tailCount}, 蛆卵(4)当前{eggCount}！");
            }

            RemoveItems(player.BagItems, "食人树叶", 4);
            RemoveItems(player.BagItems, "蝎子尾巴", 1);
            RemoveItems(player.BagItems, "蛆卵", 4);

            var yellowPowder = new EquipItem
            {
                Name = "黄色药粉(中)",
                Dura = 50,
                MaxDura = 50,
                Weight = 1
            };
            player.BagItems.Add(yellowPowder);

            return (true, yellowPowder, "黄雾升腾，刺鼻的硫磺味四溢，成功调制出一包【黄色药粉(中)】(持久 50 点)！");
        }
    }

    private static int CountItems(List<EquipItem> items, string name)
    {
        int c = 0;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Name.Contains(name)) c++;
        }
        return c;
    }

    private static void RemoveItems(List<EquipItem> items, string name, int countToRemove)
    {
        for (int i = items.Count - 1; i >= 0 && countToRemove > 0; i--)
        {
            if (items[i].Name.Contains(name))
            {
                items.RemoveAt(i);
                countToRemove--;
            }
        }
    }
}
