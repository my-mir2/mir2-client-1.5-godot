using System;
using System.Collections.Generic;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;
using Mir2.Shared.Rules;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 M2Server 仓库独立存储与金币存取引擎
/// 对应官方物理地址:
/// - sub_4A5E44 TUserEngine.StorageOpen
/// - 0x4A1AF8 @storage / @getback
/// - ProcessServerMessage case 700~707 (SM_OPENSTORAGEDLG / SM_SAVEITEM_OK / SM_SAVEITEM_FULL / SM_TAKEBACKITEM_OK / SM_TAKEBACKITEM_FULL)
/// 
/// 核心物理真值规则:
/// 1. 独立 40 格保管箱 (区别于人物背包 40/46 格);
/// 2. 二级安全密码保护: 设定二级密码后未输入前禁止一切存取读写;
/// 3. 取出时严格双重防爆检定: 个人背包满仓 40 格或负重超限阻断;
/// 4. 金币存取防溢出与余额原子检定 (上限 10,000,000).
/// </summary>
public static class StorageEngine
{
    public enum StorageResult
    {
        Success = 0,
        NeedPassword = 1,
        WrongPassword = 2,
        StorageFull = 3,
        BagFull = 4,
        OverWeight = 5,
        ItemNotFound = 6,
        InvalidAmount = 7,
        GoldCapExceeded = 8,
        InsufficientGold = 9,
    }

    /// <summary>
    /// 校验仓库是否处于二级密码锁定状态
    /// </summary>
    public static bool IsLocked(PlayerState player)
    {
        return !string.IsNullOrEmpty(player.StoragePassword) && !player.IsStorageUnlocked;
    }

    /// <summary>
    /// 验证仓库二级密码 (对齐官方 1.5 仓库密码解锁)
    /// </summary>
    public static StorageResult VerifyPassword(PlayerState player, string password)
    {
        if (string.IsNullOrEmpty(player.StoragePassword))
        {
            player.IsStorageUnlocked = true;
            return StorageResult.Success;
        }

        if (player.StoragePassword == password)
        {
            player.IsStorageUnlocked = true;
            return StorageResult.Success;
        }

        return StorageResult.WrongPassword;
    }

    /// <summary>
    /// 存入物品到仓库保管箱 (CM_USERSTORAGEDEPOSIT 1031 / CM_STORAGE 1015)
    /// </summary>
    public static (StorageResult result, EquipItem? depositedItem) DepositItem(PlayerState player, int bagSlot, string itemName = "")
    {
        if (IsLocked(player))
        {
            return (StorageResult.NeedPassword, null);
        }

        if (player.StorageItems.Count >= PlayerState.MaxStorageCapacity)
        {
            return (StorageResult.StorageFull, null);
        }

        // 定位背包中的物品
        EquipItem? item = null;
        if (bagSlot >= 0 && bagSlot < player.BagItems.Count)
        {
            item = player.BagItems[bagSlot];
        }
        else if (!string.IsNullOrEmpty(itemName))
        {
            item = player.BagItems.Find(i => i.Name == itemName);
        }

        if (item == null)
        {
            return (StorageResult.ItemNotFound, null);
        }

        // 移出背包
        player.BagItems.Remove(item);
        player.BagWeight = Math.Max(0, player.BagWeight - item.Weight);
        player.RecalculateAttributes();

        // 存入仓库
        player.StorageItems.Add(item);

        return (StorageResult.Success, item);
    }

    /// <summary>
    /// 从仓库保管箱取回物品 (CM_USERSTORAGEWITHDRAW 1032 / CM_TAKEOUTSTORAGE 1016)
    /// </summary>
    public static (StorageResult result, EquipItem? withdrawnItem) WithdrawItem(PlayerState player, int storageSlot)
    {
        if (IsLocked(player))
        {
            return (StorageResult.NeedPassword, null);
        }

        if (storageSlot < 0 || storageSlot >= player.StorageItems.Count)
        {
            return (StorageResult.ItemNotFound, null);
        }

        var item = player.StorageItems[storageSlot];
        if (item == null)
        {
            return (StorageResult.ItemNotFound, null);
        }

        // 背包满仓检查 (官方 40 格)
        if (player.BagItems.Count >= 40)
        {
            return (StorageResult.BagFull, null);
        }

        // 负重超限检查
        if (player.BagWeight + item.Weight > player.MaxBagWeight)
        {
            return (StorageResult.OverWeight, null);
        }

        // 移出仓库，进入背包
        player.StorageItems.RemoveAt(storageSlot);
        player.BagItems.Add(item);
        player.BagWeight += item.Weight;
        player.RecalculateAttributes();

        return (StorageResult.Success, item);
    }

    /// <summary>
    /// 存入金币到仓库 (CM_SAVEGOLD 1017)
    /// </summary>
    public static StorageResult DepositGold(PlayerState player, int amount)
    {
        if (IsLocked(player))
        {
            return StorageResult.NeedPassword;
        }

        if (amount <= 0)
        {
            return StorageResult.InvalidAmount;
        }

        if (player.Gold < amount)
        {
            return StorageResult.InsufficientGold;
        }

        if ((long)player.StorageGold + amount > PlayerState.MaxGoldLimit)
        {
            return StorageResult.GoldCapExceeded;
        }

        player.Gold -= amount;
        player.StorageGold += amount;
        player.RecalculateAuthoritativeWeights(player.Gold);
        return StorageResult.Success;
    }

    /// <summary>
    /// 从仓库支取金币 (CM_TAKEGOLD 1018)
    /// </summary>
    public static StorageResult WithdrawGold(PlayerState player, int amount)
    {
        if (IsLocked(player))
        {
            return StorageResult.NeedPassword;
        }

        if (amount <= 0)
        {
            return StorageResult.InvalidAmount;
        }

        if (player.StorageGold < amount)
        {
            return StorageResult.InsufficientGold;
        }

        if ((long)player.Gold + amount > PlayerState.MaxGoldLimit)
        {
            return StorageResult.GoldCapExceeded;
        }

        // 盛大官方 1.5 金币负重物理检定 (每 10,000 金币折算 1 负重)
        int oldGoldWeight = WeightConstraintEngine.CalcGoldWeight(player.Gold);
        int newGold = player.Gold + amount;
        int newGoldWeight = WeightConstraintEngine.CalcGoldWeight(newGold);
        int deltaWeight = newGoldWeight - oldGoldWeight;

        if (player.BagWeight + deltaWeight > player.MaxBagWeight)
        {
            return StorageResult.OverWeight;
        }

        player.StorageGold -= amount;
        player.Gold += amount;
        player.RecalculateAuthoritativeWeights(player.Gold);
        return StorageResult.Success;
    }
}
