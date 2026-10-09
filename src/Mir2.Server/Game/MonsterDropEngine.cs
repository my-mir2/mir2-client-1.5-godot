using System;
using System.Collections.Generic;
using Mir2.Shared.Models;
using Mir2.Shared.Rules;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 死亡掉落权威引擎 (MonsterDropEngine)
/// 严格 1:1 对齐官方 M2Server 物理反编译代码:
/// - 死亡爆背包: TCreature.ScatterBagItems @ 0x4BB44C (白名 1/3, 红名 100%)
/// - 死亡爆穿戴: TCreature.DropUseItems @ 0x4BB6C8 (白名 1/30, 红名 1/15, 严格遍历 9 大槽位)
/// </summary>
public static class MonsterDropEngine
{
    public readonly record struct DroppedItem(
        int ItemUid,
        string ItemName,
        int OriginSlot,
        bool FromEquipment // true=穿戴装备, false=背包物品
    );

    /// <summary>
    /// 执行实体死亡掉落判定
    /// </summary>
    public static List<DroppedItem> ExecuteDeathDrop(PlayerState state, OfficialClientRulesEngine.DelphiRandom rng)
    {
        var dropped = new List<DroppedItem>();
        bool isRedName = state.IsRedName; // PK >= 200

        // 1. 爆背包物品 (0x4BB44C): 白名 1/3 几率，红名 100% 必爆
        for (int i = state.BagItems.Count - 1; i >= 0; i--)
        {
            var item = state.BagItems[i];
            if (isRedName || rng.Next(3) == 0)
            {
                dropped.Add(new DroppedItem(item.Uid, item.Name, i, FromEquipment: false));
                state.BagItems.RemoveAt(i);
            }
        }

        // 2. 爆身上 9 大装备槽位 (0x4BB6C8): 白名 1/30 几率，红名 1/15 几率 (暴增 100%)
        int equipDropRate = isRedName ? 15 : 30;

        for (int slot = 0; slot < 9; slot++)
        {
            var equip = state.Equipment[slot];
            if (equip == null) continue;

            // 掉落几率命中
            if (rng.Next(equipDropRate) == 0)
            {
                dropped.Add(new DroppedItem(equip.Uid, equip.Name, slot, FromEquipment: true));
                state.TakeOffItem((OfficialEquipSlot)slot);
            }
        }

        return dropped;
    }
}
