// ============================================================================
// Mir2 客户端 - 仓库保管箱与金币管理器 (1:1 盛大 1.5 官方实证)
// ============================================================================

using System;
using System.Collections.Generic;

namespace Mir2Client.Game
{
    public class StorageManager
    {
        public const int MaxStorageCapacity = 50;
        public const int MaxStorageGold = 10_000_000;
        public const int MaxPlayerGold = 5_000_000;

        public event Action? StorageOpened;
        public event Action<List<EquipItem>>? StorageItemsUpdated;
        public event Action<int>? StorageGoldUpdated;
        public event Action<bool>? StorageLockedStatusChanged;

        public List<EquipItem> StorageItems { get; } = new();
        public int StorageGold { get; private set; } = 0;
        public bool HasPasswordProtection { get; private set; } = false;
        public bool IsPasswordUnlocked { get; private set; } = true;
        private string _storagePassword = "";

        public void OpenStorage()
        {
            StorageOpened?.Invoke();
        }

        public void SetPassword(string pwd)
        {
            _storagePassword = pwd;
            HasPasswordProtection = !string.IsNullOrEmpty(pwd);
            IsPasswordUnlocked = !HasPasswordProtection;
            StorageLockedStatusChanged?.Invoke(HasPasswordProtection);
        }

        public bool UnlockWithPassword(string pwd)
        {
            if (!HasPasswordProtection)
            {
                IsPasswordUnlocked = true;
                return true;
            }
            if (pwd == _storagePassword)
            {
                IsPasswordUnlocked = true;
                StorageLockedStatusChanged?.Invoke(false);
                return true;
            }
            IsPasswordUnlocked = false;
            StorageLockedStatusChanged?.Invoke(true);
            return false;
        }

        public bool DepositItem(EquipItem item, out string error)
        {
            if (HasPasswordProtection && !IsPasswordUnlocked)
            {
                error = "仓库已被二级密码锁定，请先输入密码解锁";
                return false;
            }
            if (StorageItems.Count >= MaxStorageCapacity)
            {
                error = "仓库空间已满 (上限50格)，无法继续存放！";
                return false;
            }
            StorageItems.Add(item);
            StorageItemsUpdated?.Invoke(StorageItems);
            error = string.Empty;
            return true;
        }

        public bool WithdrawItem(int index, int playerBagCount, int bagCap, out EquipItem? item, out string error)
        {
            item = null;
            if (HasPasswordProtection && !IsPasswordUnlocked)
            {
                error = "仓库已被二级密码锁定，请先输入密码解锁";
                return false;
            }
            if (index < 0 || index >= StorageItems.Count)
            {
                error = "物品不存在";
                return false;
            }
            if (playerBagCount >= bagCap)
            {
                error = "背包已满，无法取回物品！";
                return false;
            }
            item = StorageItems[index];
            StorageItems.RemoveAt(index);
            StorageItemsUpdated?.Invoke(StorageItems);
            error = string.Empty;
            return true;
        }

        public bool DepositGold(int amount, int playerGold, out int newPlayerGold, out string error)
        {
            newPlayerGold = playerGold;
            if (amount <= 0 || amount > playerGold)
            {
                error = "存入金币数额非法";
                return false;
            }
            if (StorageGold + amount > MaxStorageGold)
            {
                error = "仓库金币即将超出存储上限 (10,000,000)，无法继续存入！";
                return false;
            }
            StorageGold += amount;
            newPlayerGold = playerGold - amount;
            StorageGoldUpdated?.Invoke(StorageGold);
            error = string.Empty;
            return true;
        }

        public bool WithdrawGold(int amount, int playerGold, out int newPlayerGold, out string error)
        {
            newPlayerGold = playerGold;
            if (amount <= 0 || amount > StorageGold)
            {
                error = "仓库中没有足够的金币！";
                return false;
            }
            if (playerGold + amount > MaxPlayerGold)
            {
                error = "身上携带金币已达上限，无法提取！";
                return false;
            }
            StorageGold -= amount;
            newPlayerGold = playerGold + amount;
            StorageGoldUpdated?.Invoke(StorageGold);
            error = string.Empty;
            return true;
        }
    }
}
