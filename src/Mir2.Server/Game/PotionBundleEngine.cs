using System;
using System.Collections.Generic;
using Mir2.Shared.Models;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 药品捆包与解包权威引擎 (PotionBundleEngine)
/// 严格 1:1 对齐官方 M2Server 规则:
/// 1. 支持各类药水捆包解包:
///    - 强效太阳水包 -> 6 瓶强效太阳水
///    - 太阳水包 -> 6 瓶太阳水
///    - 万年雪霜包 -> 6 瓶万年雪霜
///    - 疗伤药包 -> 6 瓶疗伤药
///    - 强效金创药包 / 金创药包(强效) -> 6 瓶强效金创药
///    - 强效魔法药包 / 魔法药包(强效) -> 6 瓶强效魔法药
///    - 中量金创药包 / 金创药包(中) -> 6 瓶中量金创药
///    - 中量魔法药包 / 魔法药包(中) -> 6 瓶中量魔法药
///    - 超级金创药包 -> 6 瓶超级金创药
///    - 超级魔法药包 -> 6 瓶超级魔法药
/// 2. 背包空间门禁:
///    - 1 包药水解开后生成 6 瓶单体药水，消耗 1 格药包，净需 5 格可用空间
///    - 若 BagItems.Count + 5 > 40，官方拒绝解包，提示“背包空间不足，无法解开药包！”
/// 3. 重量变化:
///    - 单体药水重量按官方标准累加 (如强效太阳水重量 2，6 瓶总重 12)
/// </summary>
public static class PotionBundleEngine
{
    public const int ItemsPerBundle = 6;
    public const int NetSlotsRequired = ItemsPerBundle - 1; // 5 格
    public const int MaxBagCapacity = 40;

    public record BundleDefinition(
        string BundleName,
        string SingleItemName,
        int SingleItemWeight,
        int BundleWeight
    );

    private static readonly Dictionary<string, BundleDefinition> Bundles = new(StringComparer.OrdinalIgnoreCase)
    {
        ["强效太阳水包"] = new("强效太阳水包", "强效太阳水", 2, 6),
        ["太阳水包"] = new("太阳水包", "太阳水", 2, 6),
        ["万年雪霜包"] = new("万年雪霜包", "万年雪霜", 2, 6),
        ["疗伤药包"] = new("疗伤药包", "疗伤药", 2, 6),
        ["强效金创药包"] = new("强效金创药包", "强效金创药", 2, 6),
        ["金创药包(强效)"] = new("金创药包(强效)", "强效金创药", 2, 6),
        ["强效魔法药包"] = new("强效魔法药包", "强效魔法药", 2, 6),
        ["魔法药包(强效)"] = new("魔法药包(强效)", "强效魔法药", 2, 6),
        ["中量金创药包"] = new("中量金创药包", "中量金创药", 2, 5),
        ["金创药包(中)"] = new("金创药包(中)", "中量金创药", 2, 5),
        ["中量魔法药包"] = new("中量魔法药包", "中量魔法药", 2, 5),
        ["魔法药包(中)"] = new("魔法药包(中)", "中量魔法药", 2, 5),
        ["超级金创药包"] = new("超级金创药包", "强效金创药", 2, 8),
        ["超级魔法药包"] = new("超级魔法药包", "强效魔法药", 2, 8),
    };

    public static bool IsPotionBundle(string itemName)
    {
        if (string.IsNullOrWhiteSpace(itemName)) return false;
        foreach (var key in Bundles.Keys)
        {
            if (itemName.Contains(key, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }
        return false;
    }

    public static BundleDefinition? FindBundleDefinition(string itemName)
    {
        if (string.IsNullOrWhiteSpace(itemName)) return null;
        foreach (var kvp in Bundles)
        {
            if (itemName.Contains(kvp.Key, StringComparison.OrdinalIgnoreCase))
            {
                return kvp.Value;
            }
        }
        return null;
    }

    public sealed class UnpackResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public EquipItem? ConsumedBundle { get; set; }
        public int ConsumedSlot { get; set; } = -1;
        public List<EquipItem> UnpackedItems { get; } = new();
    }

    /// <summary>
    /// 权威解开药包逻辑
    /// </summary>
    public static UnpackResult TryUnpack(PlayerState player, int slot, string itemName = "")
    {
        var result = new UnpackResult();
        if (player == null || player.Hp <= 0)
        {
            result.Message = "角色状态异常。";
            return result;
        }

        // 定位背包中的药包
        EquipItem? bundleItem = null;
        int targetSlot = -1;
        if (slot >= 0 && slot < player.BagItems.Count)
        {
            bundleItem = player.BagItems[slot];
            targetSlot = slot;
        }
        else if (!string.IsNullOrEmpty(itemName))
        {
            targetSlot = player.BagItems.FindIndex(i => IsPotionBundle(i.Name));
            if (targetSlot >= 0)
            {
                bundleItem = player.BagItems[targetSlot];
            }
        }

        if (bundleItem == null)
        {
            result.Message = "背包中未找到该药包。";
            return result;
        }

        var def = FindBundleDefinition(bundleItem.Name);
        if (def == null)
        {
            result.Message = "该物品并非受支持的药水捆包。";
            return result;
        }

        // 盛大官方 1.5 铁律: 检查背包剩余格子，解包净需 5 格
        if (player.BagItems.Count + NetSlotsRequired > MaxBagCapacity)
        {
            result.Message = "背包空间不足，无法解开药包！";
            return result;
        }

        // 盛大官方 1.5 铁律: 检查解包后净增重量是否会导致背包超重
        int netWeightGain = (ItemsPerBundle * def.SingleItemWeight) - bundleItem.Weight;
        if (netWeightGain > 0 && player.BagWeight + netWeightGain > player.MaxBagWeight)
        {
            result.Message = "你身上携带的物品过重，无法解开药包！";
            return result;
        }

        // 移除原药包
        player.BagItems.RemoveAt(targetSlot);
        player.BagWeight = Math.Max(0, player.BagWeight - bundleItem.Weight);
        result.ConsumedBundle = bundleItem;
        result.ConsumedSlot = targetSlot;

        // 生成 6 瓶单体药水
        int baseUid = bundleItem.Uid > 0 ? bundleItem.Uid * 10 : 80000;
        for (int i = 0; i < ItemsPerBundle; i++)
        {
            var single = new EquipItem
            {
                Uid = baseUid + i + 1,
                Name = def.SingleItemName,
                Weight = def.SingleItemWeight,
                Dura = 1,
                MaxDura = 1
            };
            player.BagItems.Add(single);
            player.BagWeight += single.Weight;
            result.UnpackedItems.Add(single);
        }

        player.RecalculateAuthoritativeWeights(player.Gold);
        result.Success = true;
        result.Message = $"解开了一包【{bundleItem.Name}】，获得了 {ItemsPerBundle} 瓶【{def.SingleItemName}】。";
        return result;
    }
}
