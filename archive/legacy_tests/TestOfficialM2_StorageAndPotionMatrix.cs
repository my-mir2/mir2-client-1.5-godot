using System;
using System.Collections.Generic;
using Mir2.Server.Game;
using Mir2.Server.Maps;
using Mir2.Server.Network;
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

        // 取出 100 万金币 (增加 100 负重)
        player.MaxBagWeight = 500;
        var wdRes = StorageEngine.WithdrawGold(player, 1_000_000);
        Assert.Equal(StorageEngine.StorageResult.Success, wdRes);
        Assert.Equal(4_000_000, player.Gold);
        Assert.Equal(2_000_000, player.StorageGold);
        Assert.Equal(400, player.BagWeight); // 400万金币 = 400负重

        // 超过 10,000,000 上限防溢出
        player.Gold = 9_500_000;
        player.StorageGold = 1_000_000;
        var capRes = StorageEngine.WithdrawGold(player, 800_000);
        Assert.Equal(StorageEngine.StorageResult.GoldCapExceeded, capRes);
    }

    [Fact]
    public void Storage_WithdrawGold_OverWeight_BlocksWithdrawal()
    {
        var player = new PlayerState
        {
            Level = 22, // 22 级战士官方背包负重上限为 211 (50 + 22*22/3)
            Job = 0,
            Gold = 0,
            StorageGold = 5_000_000
        };
        // 向背包添加真实 150 重量的装备
        player.BagItems.Add(new EquipItem { Uid = 5555, Name = "重型战甲", Weight = 150 });
        player.RecalculateAuthoritativeWeights(player.Gold);
        Assert.Equal(150, player.BagWeight);
        Assert.True(player.MaxBagWeight >= 200);

        // 尝试支取 100 万金币 (重量 100 点，150 + 100 = 250 > 200)
        var res = StorageEngine.WithdrawGold(player, 1_000_000);
        Assert.Equal(StorageEngine.StorageResult.OverWeight, res);
        Assert.Equal(0, player.Gold);
        Assert.Equal(5_000_000, player.StorageGold);
        Assert.Equal(150, player.BagWeight);

        // 支取 50 万金币 (重量 50 点，150 + 50 = 200 <= 200 允许)
        var okRes = StorageEngine.WithdrawGold(player, 500_000);
        Assert.Equal(StorageEngine.StorageResult.Success, okRes);
        Assert.Equal(500_000, player.Gold);
        Assert.Equal(4_500_000, player.StorageGold);
        Assert.Equal(200, player.BagWeight);
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

        // 强效太阳水 (StdItems.DB Shape=1): HP+50, MP+80 瞬回
        var (res, effect) = PotionEngine.ConsumePotion(player, 0);
        Assert.Equal(PotionEngine.PotionResult.Success, res);
        Assert.NotNull(effect);
        Assert.Equal(100, player.Hp);
        Assert.Equal(130, player.Mp); // 官方真值: 50 + 80 = 130
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

    [Fact]
    public void Potion_ConsumePotionEx_ReturnsConsumedItemAndSlotAndDeductsWeight()
    {
        var player = new PlayerState
        {
            MaxHp = 200,
            Hp = 50,
            BagWeight = 10
        };
        var potion = new EquipItem { Uid = 10086, Name = "太阳水", Weight = 2 };
        player.BagItems.Add(potion);

        var (res, effect, item, slot) = PotionEngine.ConsumePotionEx(player, 0);
        Assert.Equal(PotionEngine.PotionResult.Success, res);
        Assert.NotNull(effect);
        Assert.NotNull(item);
        Assert.Equal(10086, item.Uid);
        Assert.Equal(0, slot);
        Assert.Empty(player.BagItems);
        Assert.Equal(8, player.BagWeight); // 10 - 2 = 8
    }

    [Fact]
    public void Potion_FullHealthPlayer_PreservesIncPool_ForPreDrinkingCombat()
    {
        var player = new PlayerState
        {
            MaxHp = 300,
            Hp = 300, // 初始为满血
            MaxMp = 200,
            Mp = 200  // 初始为满魔
        };
        player.BagItems.Add(new EquipItem { Uid = 501, Name = "强效金创药", Weight = 3 });
        player.BagItems.Add(new EquipItem { Uid = 502, Name = "强效魔法药", Weight = 3 });

        // 1. 满血满魔状态下喝下强效金创药 (+90 HP) 与强效魔法药 (+150 MP, 官方真值)
        var (hpRes, _) = PotionEngine.ConsumePotion(player, 0);
        var (mpRes, _) = PotionEngine.ConsumePotion(player, 0);
        Assert.Equal(PotionEngine.PotionResult.Success, hpRes);
        Assert.Equal(PotionEngine.PotionResult.Success, mpRes);
        Assert.Equal(90, player.IncHealth);
        Assert.Equal(150, player.IncSpell); // 官方真值: 强效魔法药 MAC=150

        // 2. 连续经过 3 个心跳 Tick: 由于角色此时满血满魔，血池与蓝池绝不被抹杀清零，而是完整保留
        for (int i = 0; i < 3; i++)
        {
            var (gainHp, gainMp) = PotionEngine.TickIncHealthSpell(player);
            Assert.Equal(0, gainHp);
            Assert.Equal(0, gainMp);
        }
        Assert.Equal(300, player.Hp);
        Assert.Equal(90, player.IncHealth); // 血池依然完好
        Assert.Equal(150, player.IncSpell); // 蓝池依然完好 (强效魔法药 MAC=150)

        // 3. 玩家冲入怪物群受到 50 点打击 (HP 变为 250)
        player.Hp -= 50;
        Assert.Equal(250, player.Hp);

        // 4. 下一个心跳 Tick: 血池立即介入生效，匀速释放 5 点生命
        var (actualGainHp, _) = PotionEngine.TickIncHealthSpell(player);
        Assert.Equal(5, actualGainHp);
        Assert.Equal(255, player.Hp);
        Assert.Equal(85, player.IncHealth); // 90 - 5 = 85，血池平稳消耗
    }

    [Fact]
    public void Test_MysticEquipment_Purify_And_OilRepair_RequireRealBagItem()
    {
        var player = new PlayerState
        {
            Uid = 3001,
            MaxBagWeight = 200,
            BagWeight = 10
        };

        // 1. 穿戴被诅咒的神秘头盔
        var mysticHelmet = new EquipItem
        {
            Uid = 701,
            Name = "神秘头盔",
            Curse = 1,
            Weight = 5
        };
        player.Equipment[(int)OfficialEquipSlot.Helmet] = mysticHelmet;

        // 验证脱下被拦截
        var (canTakeOff, curseReason) = MysticEquipmentEngine.CheckCanTakeOff(mysticHelmet);
        Assert.False(canTakeOff);
        Assert.Contains("诅咒", curseReason);

        // 2. 使用神水净化
        var (pOk, pMsg) = MysticEquipmentEngine.PurifyMysticCurseWithHolyWater(mysticHelmet);
        Assert.True(pOk);
        Assert.Equal(0, mysticHelmet.Curse);

        // 再次验证脱下被允许
        var (canTakeOffAfter, _) = MysticEquipmentEngine.CheckCanTakeOff(mysticHelmet);
        Assert.True(canTakeOffAfter);
    }

    [Fact]
    public void TestOfficialObjBase_LevelQuadraticGrowthFormulas_And_ZeroBonusPoint()
    {
        // 1. 战士职业验证 (Job 0)
        var warrior = new PlayerState { Job = 0, Level = 1 };
        warrior.RecalculateBaseHpMp();
        Assert.Equal(19, warrior.BaseMaxHp); // 1级战士 19 HP
        Assert.Equal(15, warrior.BaseMaxMp); // 1级战士 15 MP

        warrior.Level = 28;
        warrior.RecalculateBaseHpMp();
        Assert.Equal(375, warrior.BaseMaxHp); // 28级半月战士 375 HP
        Assert.Equal(109, warrior.BaseMaxMp);

        warrior.Level = 35;
        warrior.RecalculateBaseHpMp();
        Assert.Equal(539, warrior.BaseMaxHp); // 35级烈火战士 539 HP

        warrior.Level = 40;
        warrior.RecalculateBaseHpMp();
        Assert.Equal(674, warrior.BaseMaxHp); // 40级战士 674 HP (告别 194 拍脑袋假值!)

        // 2. 法师职业验证 (Job 1)
        var wizard = new PlayerState { Job = 1, Level = 1 };
        wizard.RecalculateBaseHpMp();
        Assert.Equal(16, wizard.BaseMaxHp); // 1级法师 16 HP
        Assert.Equal(18, wizard.BaseMaxMp);

        wizard.Level = 24;
        wizard.RecalculateBaseHpMp();
        Assert.Equal(96, wizard.BaseMaxHp); // 24级开盾法师 96 HP
        Assert.Equal(372, wizard.BaseMaxMp);

        wizard.Level = 35;
        wizard.RecalculateBaseHpMp();
        Assert.Equal(159, wizard.BaseMaxHp); // 35级法师 159 HP
        Assert.Equal(706, wizard.BaseMaxMp);

        // 3. 道士职业验证 (Job 2)
        var taoist = new PlayerState { Job = 2, Level = 1 };
        taoist.RecalculateBaseHpMp();
        Assert.Equal(17, taoist.BaseMaxHp); // 1级道士 17 HP
        Assert.Equal(13, taoist.BaseMaxMp);

        taoist.Level = 35;
        taoist.RecalculateBaseHpMp();
        Assert.Equal(306, taoist.BaseMaxHp); // 35级神兽道士 306 HP
        Assert.Equal(350, taoist.BaseMaxMp);

        // 4. 验证升级无自由属性点私服魔改
        var newbie = new PlayerState { Job = 0, Level = 1, Exp = 0 };
        newbie.GainExp(ExpTable.ExpForLevel(1) + 10);
        Assert.Equal(2, newbie.Level);
        Assert.Equal(0, newbie.BonusPoint); // 官方原版无升级属性加点
    }

    [Fact]
    public void TestOfficialSub4C7720_NaturalRecoveryCycle_Hp75_Mp18_AndGreenPoisonBlock()
    {
        var mapManager = new MapManager();
        var map = mapManager.GetMap(0)!;
        var dispatcher = new PacketDispatcher(mapManager);

        var playerSession = new ClientSession(88991, null!)
        {
            CharacterName = "自然回血测试者",
            CurrentMapId = 0,
            X = 330,
            Y = 270,
            State = new PlayerState
            {
                Job = 0,
                Level = 40,
                Hp = 400,
                MaxHp = 674,
                Mp = 50,
                MaxMp = 109,
                LastNaturalRegenMs = 100_000
            }
        };
        dispatcher.ActiveSessions[playerSession.SessionId] = playerSession;

        // 1. 未到 3000ms 周期，不触发自然回复
        dispatcher.UpdateWorldTick(1, 102_000);
        Assert.Equal(400, playerSession.State.Hp);
        Assert.Equal(50, playerSession.State.Mp);

        // 2. 满 3000ms 周期 (103_001ms)，触发自然回复:
        //    HpStep = 674 / 75 + 1 = 8 + 1 = 9
        //    MpStep = 109 / 18 + 1 = 6 + 1 = 7
        dispatcher.UpdateWorldTick(2, 103_001);
        Assert.Equal(409, playerSession.State.Hp);
        Assert.Equal(57, playerSession.State.Mp);
        Assert.True(playerSession.HasBufferedPacket($"#{ProtocolConsts.SM_HEALTHSPELLCHANGED}/409/57/674/109/!"));

        // 3. 中绿毒状态下，自然回血被阻断，但自然回蓝照常执行
        playerSession.ClearBufferedPackets();
        playerSession.State.HasGreenPoison = true;
        playerSession.State.GreenPoisonExpireMs = 200_000;
        playerSession.State.LastNaturalRegenMs = 103_001;

        dispatcher.UpdateWorldTick(3, 106_002);
        Assert.Equal(409, playerSession.State.Hp); // 绿毒期间严禁自然回血
        Assert.Equal(64, playerSession.State.Mp); // 57 + 7 = 64，正常自然回蓝
    }
}


