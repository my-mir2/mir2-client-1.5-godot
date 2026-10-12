using System;

namespace Mir2.Shared.Rules;

/// <summary>
/// 盛大官方 1.4/1.5 仓库保管箱与金币存储权威规则 (StorageRulesEngine)
/// 严格 1:1 对齐官方 M2Server sub_4B92C0
/// 规则常数：
/// 1. 仓库容量上限：严格 50 格
/// 2. 仓库金币存储上限：严格 10,000,000 (一千万金币)
/// 3. 玩家随身携带金币上限：严格 5,000,000 (五百万金币)
/// 4. 二级密码保护状态机
/// </summary>
public static class StorageRulesEngine
{
    public const int MaxStorageCapacity = 50;
    public const int MaxStorageGold = 10_000_000;
    public const int MaxPlayerGold = 5_000_000;

    /// <summary>
    /// 校验二级密码输入是否匹配
    /// </summary>
    public static bool VerifyPassword(string? actualPassword, string inputPassword)
    {
        if (string.IsNullOrEmpty(actualPassword)) return true;
        return string.Equals(actualPassword, inputPassword, StringComparison.Ordinal);
    }

    /// <summary>
    /// 校验物品是否可存入仓库
    /// </summary>
    public static (bool allowed, string error) ValidateDepositItem(int currentStorageCount, bool isLocked)
    {
        if (isLocked)
        {
            return (false, "仓库已被二级密码锁定，请先输入密码解锁");
        }
        if (currentStorageCount >= MaxStorageCapacity)
        {
            return (false, "仓库空间已满 (上限50格)，无法继续存放！");
        }
        return (true, string.Empty);
    }

    /// <summary>
    /// 校验物品是否可从仓库取出
    /// </summary>
    public static (bool allowed, string error) ValidateWithdrawItem(int itemIndex, int currentStorageCount, int playerBagCount, int playerBagCapacity, bool isLocked)
    {
        if (isLocked)
        {
            return (false, "仓库已被二级密码锁定，请先输入密码解锁");
        }
        if (itemIndex < 0 || itemIndex >= currentStorageCount)
        {
            return (false, "物品不存在");
        }
        if (playerBagCount >= playerBagCapacity)
        {
            return (false, "背包已满，无法取回物品！");
        }
        return (true, string.Empty);
    }

    /// <summary>
    /// 校验金币存入规则
    /// </summary>
    public static (bool allowed, int newPlayerGold, int newStorageGold, string error) ValidateDepositGold(
        int amount, int currentStorageGold, int playerGold, bool isLocked)
    {
        if (isLocked)
        {
            return (false, playerGold, currentStorageGold, "仓库已被二级密码锁定，请先输入密码解锁");
        }
        if (amount <= 0 || amount > playerGold)
        {
            return (false, playerGold, currentStorageGold, "存入金币数额非法");
        }
        if ((long)currentStorageGold + amount > MaxStorageGold)
        {
            return (false, playerGold, currentStorageGold, "仓库金币即将超出存储上限 (10,000,000)，无法继续存入！");
        }
        return (true, playerGold - amount, currentStorageGold + amount, string.Empty);
    }

    /// <summary>
    /// 校验金币提取规则
    /// </summary>
    public static (bool allowed, int newPlayerGold, int newStorageGold, string error) ValidateWithdrawGold(
        int amount, int currentStorageGold, int playerGold, bool isLocked)
    {
        if (isLocked)
        {
            return (false, playerGold, currentStorageGold, "仓库已被二级密码锁定，请先输入密码解锁");
        }
        if (amount <= 0 || amount > currentStorageGold)
        {
            return (false, playerGold, currentStorageGold, "仓库中没有足够的金币！");
        }
        if ((long)playerGold + amount > MaxPlayerGold)
        {
            return (false, playerGold, currentStorageGold, "身上携带金币已达上限，无法提取！");
        }
        return (true, playerGold + amount, currentStorageGold - amount, string.Empty);
    }
}
