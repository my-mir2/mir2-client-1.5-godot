using System;
using System.Collections.Generic;
using Mir2.Shared.Rules;

namespace Mir2Client.Game
{
    /// <summary>
    /// 盛大官方 1.5 客户端仓库保管箱代理与 UI 数据容器 (1:1 官方实证)
    /// 核心业务规则 (上限50格、金币上限一千万、密码锁) 权威下沉至 Mir2.Shared.Rules.StorageRulesEngine 与服务端 StorageEngine
    /// 客户端只负责 UI 呈现与网络发包代理
    /// </summary>
    public class StorageManager
    {
        public const int MaxStorageCapacity = StorageRulesEngine.MaxStorageCapacity;
        public const int MaxStorageGold = StorageRulesEngine.MaxStorageGold;
        public const int MaxPlayerGold = StorageRulesEngine.MaxPlayerGold;

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
            if (StorageRulesEngine.VerifyPassword(_storagePassword, pwd))
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
            var (allowed, err) = StorageRulesEngine.ValidateDepositItem(StorageItems.Count, HasPasswordProtection && !IsPasswordUnlocked);
            if (!allowed)
            {
                error = err;
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
            var (allowed, err) = StorageRulesEngine.ValidateWithdrawItem(index, StorageItems.Count, playerBagCount, bagCap, HasPasswordProtection && !IsPasswordUnlocked);
            if (!allowed)
            {
                error = err;
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
            var (allowed, npg, nsg, err) = StorageRulesEngine.ValidateDepositGold(
                amount, StorageGold, playerGold, HasPasswordProtection && !IsPasswordUnlocked);
            if (!allowed)
            {
                newPlayerGold = playerGold;
                error = err;
                return false;
            }
            StorageGold = nsg;
            newPlayerGold = npg;
            StorageGoldUpdated?.Invoke(StorageGold);
            error = string.Empty;
            return true;
        }

        public bool WithdrawGold(int amount, int playerGold, out int newPlayerGold, out string error)
        {
            var (allowed, npg, nsg, err) = StorageRulesEngine.ValidateWithdrawGold(
                amount, StorageGold, playerGold, HasPasswordProtection && !IsPasswordUnlocked);
            if (!allowed)
            {
                newPlayerGold = playerGold;
                error = err;
                return false;
            }
            StorageGold = nsg;
            newPlayerGold = npg;
            StorageGoldUpdated?.Invoke(StorageGold);
            error = string.Empty;
            return true;
        }
    }
}
