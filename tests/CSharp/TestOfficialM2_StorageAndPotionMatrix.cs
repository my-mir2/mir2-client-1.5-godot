using System;
using System.Collections.Generic;
using Mir2.Server.Game;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;
using Xunit;

namespace Mir2.Tests;

/// <summary>
/// 盛大官方 1.4/1.5 M2Server 仓库独立存储与药水回复全真值矩阵测试
/// 对应大账本:
/// - 子系统 18: 仓库 40 格独立存储与金币存取 (IDA 0x4A5E44 / 0x4A1AF8 @storage / @getback)
/// - 子系统 20: 药水使用与瞬回及持续回复曲线 (IDA sub_4B7100 TCreature.Eat: nIncHealth / nIncSpell)
/// </summary>
public sealed class TestOfficialM2_StorageAndPotionMatrix
{
    [Fact]
    public void Storage_NormalDepositAndWithdraw_UpdatesBagAndStorageWeights()
    {
        var player = new PlayerState
        {
            Uid = 1001,
            MaxBagWeight = 200,
            BagWeight = 50
        };

        var item = new EquipItem
        {
            Uid = 9999,
            Name = "龙纹剑",
            Weight = 30
        };
        player.BagItems.Add(item);
        player.BagWeight += item.Weight;

        // 1. 存入仓库
        var (depRes, depItem) = StorageEngine.DepositItem(player, 0);
        Assert.Equal(StorageEngine.StorageResult.Success, depRes);
        Assert.NotNull(depItem);
        Assert.Empty(player.BagItems);
        Assert.Equal(50, player.BagWeight); // 80 - 30 = 50
        Assert.Single(player.StorageItems);
        Assert.Equal(9999, player.StorageItems[0].Uid);

        // 2. 取回仓库
        var (wdRes, wdItem) = StorageEngine.WithdrawItem(player, 0);
        Assert.Equal(StorageEngine.StorageResult.Success, wdRes);
        Assert.NotNull(wdItem);
        Assert.Empty(player.StorageItems);
        Assert.Single(player.BagItems);
        Assert.Equal(80, player.BagWeight); // 50 + 30 = 80
    }

    [Fact]
    public void Storage_FullCapacity40_BlocksDeposit()
    {
        var player = new PlayerState();

        // 填满仓库 40 格
        for (int i = 0; i < PlayerState.MaxStorageCapacity; i++)
        {
            player.StorageItems.Add(new EquipItem { Uid = i + 1, Name = $"回城卷_{i}" });
        }

        var extraItem = new EquipItem { Uid = 8888, Name = "裁决之杖" };
        player.BagItems.Add(extraItem);

        // 尝试存入第 41 个物品
        var (res, item) = StorageEngine.DepositItem(player, 0);
        Assert.Equal(StorageEngine.StorageResult.StorageFull, res);
        Assert.Null(item);
        Assert.Single(player.BagItems); // 背包物品完整无损
        Assert.Equal(40, player.StorageItems.Count);
    }

    [Fact]
    public void Storage_BagFullOrOverWeight_BlocksWithdraw()
    {
        var player = new PlayerState
        {
            MaxBagWeight = 100,
            BagWeight = 90
        };

        var heavyItem = new EquipItem { Uid = 5555, Name = "屠龙", Weight = 35 };
        player.StorageItems.Add(heavyItem);

        // 负重超限阻断 (90 + 35 = 125 > 100)
        var (res, item) = StorageEngine.WithdrawItem(player, 0);
        Assert.Equal(StorageEngine.StorageResult.OverWeight, res);
        Assert.Null(item);
        Assert.Single(player.StorageItems); // 仓库未丢失
        Assert.Empty(player.BagItems);

        // 背包 40 格满仓阻断
        player.BagWeight = 0;
        for (int i = 0; i < 40; i++)
        {
            player.BagItems.Add(new EquipItem { Uid = i + 1, Name = "金创药" });
        }

        var (fullRes, fullItem) = StorageEngine.WithdrawItem(player, 0);
        Assert.Equal(StorageEngine.StorageResult.BagFull, fullRes);
        Assert.Null(fullItem);
    }

    [Fact]
    public void Storage_SecondaryPasswordLock_EnforcesAuthentication()
    {
        var player = new PlayerState
        {
            StoragePassword = "safe_password_123",
            IsStorageUnlocked = false
        };
        player.BagItems.Add(new EquipItem { Uid = 111, Name = "骨玉权杖" });

        // 未输入密码前禁止存入
        var (depRes, _) = StorageEngine.DepositItem(player, 0);
        Assert.Equal(StorageEngine.StorageResult.NeedPassword, depRes);

        // 错误密码拒绝
        var wrongRes = StorageEngine.VerifyPassword(player, "wrong_pass");
        Assert.Equal(StorageEngine.StorageResult.WrongPassword, wrongRes);
        Assert.False(player.IsStorageUnlocked);

        // 正确密码解锁
        var rightRes = StorageEngine.VerifyPassword(player, "safe_password_123");
        Assert.Equal(StorageEngine.StorageResult.Success, rightRes);
        Assert.True(player.IsStorageUnlocked);

        // 解锁后存入成功
        var (succRes, _) = StorageEngine.DepositItem(player, 0);
        Assert.Equal(StorageEngine.StorageResult.Success, succRes);
    }

    [Fact]
    public void Storage_GoldDepositAndWithdraw_RespectsLimitAndAtomicBalance()
    {
        var player = new PlayerState
        {
            Gold = 5_000_000,
            StorageGold = 1_000_000
        };

        // 存入 200 万金币
        var depRes = StorageEngine.DepositGold(player, 2_000_000);
        Assert.Equal(StorageEngine.StorageResult.Success, depRes);
        Assert.Equal(3_000_000, player.Gold);
        Assert.Equal(3_000_000, player.StorageGold);

        // 余额不足存入拒绝
        var overDep = StorageEngine.DepositGold(player, 4_000_000);
        Assert.Equal(StorageEngine.StorageResult.InsufficientGold, overDep);

        // 取出 100 万金币
        var wdRes = StorageEngine.WithdrawGold(player, 1_000_000);
        Assert.Equal(StorageEngine.StorageResult.Success, wdRes);
        Assert.Equal(4_000_000, player.Gold);
        Assert.Equal(2_000_000, player.StorageGold);

        // 超过 10,000,000 上限防溢出
        player.Gold = 9_500_000;
        player.StorageGold = 1_000_000;
        var capRes = StorageEngine.WithdrawGold(player, 800_000);
        Assert.Equal(StorageEngine.StorageResult.GoldCapExceeded, capRes);
    }

    [Fact]
    public void Potion_InstantSunPotions_DirectlyRestoresHpMpWithoutIncPool()
    {
        var player = new PlayerState
        {
            MaxHp = 200,
            Hp = 50,
            MaxMp = 200,
            Mp = 50
        };
        player.BagItems.Add(new EquipItem { Uid = 1, Name = "强效太阳水", Weight = 2 });

        // 强效太阳水: HP+50, MP+50 瞬回
        var (res, effect) = PotionEngine.ConsumePotion(player, 0);
        Assert.Equal(PotionEngine.PotionResult.Success, res);
        Assert.NotNull(effect);
        Assert.Equal(100, player.Hp);
        Assert.Equal(100, player.Mp);
        Assert.Equal(0, player.IncHealth); // 瞬回不进血池
        Assert.Equal(0, player.IncSpell);
        Assert.Empty(player.BagItems);
    }

    [Fact]
    public void Potion_ContinuousGoldenMedicine_InjectsIncPoolAndTicksGradually()
    {
        var player = new PlayerState
        {
            MaxHp = 200,
            Hp = 50,
            MaxMp = 200,
            Mp = 50
        };
        player.BagItems.Add(new EquipItem { Uid = 2, Name = "强效金创药", Weight = 3 });

        // 强效金创药: +90 HP 进入 IncHealth 血池
        var (res, effect) = PotionEngine.ConsumePotion(player, 0);
        Assert.Equal(PotionEngine.PotionResult.Success, res);
        Assert.NotNull(effect);
        Assert.Equal(50, player.Hp); // 当前血量尚未递增
        Assert.Equal(90, player.IncHealth);

        // 模拟 1 个心跳 Tick (释放 5 点)
        var (hpGain1, _) = PotionEngine.TickIncHealthSpell(player);
        Assert.Equal(5, hpGain1);
        Assert.Equal(55, player.Hp);
        Assert.Equal(85, player.IncHealth);

        // 模拟持续释放 17 个 Tick (17 * 5 = 85 点)
        for (int i = 0; i < 17; i++)
        {
            PotionEngine.TickIncHealthSpell(player);
        }
        Assert.Equal(140, player.Hp); // 50 + 90 = 140
        Assert.Equal(0, player.IncHealth); // 血池完全耗尽释放
    }

    [Fact]
    public void Potion_DeadPlayer_CannotDrinkAndClearsIncPool()
    {
        var player = new PlayerState
        {
            MaxHp = 200,
            Hp = 0, // 阵亡
            IncHealth = 50
        };
        player.BagItems.Add(new EquipItem { Uid = 3, Name = "强效金创药" });

        var (res, effect) = PotionEngine.ConsumePotion(player, 0);
        Assert.Equal(PotionEngine.PotionResult.Dead, res);
        Assert.Null(effect);
        Assert.Equal(0, player.IncHealth); // 阵亡清空血池
        Assert.Single(player.BagItems); // 药水不被消耗
    }
}
