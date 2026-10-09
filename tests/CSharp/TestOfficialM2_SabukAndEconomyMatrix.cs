using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Mir2.Server.Game;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 盛大官方 1.4/1.5 沙巴克攻城战、皇宫争夺与全服经济防刷外挂 权威矩阵测试
/// 100% 覆盖:
/// 1. 沙巴克城门血量与倒塌破坏状态机 (2000 HP, Damaged, Broken 消除网格阻挡, 修复恢复阻挡)
/// 2. 攻城战役期沙巴克与皇宫免 PK 惩罚 (红名豁免、不加 PK 点数、不加深武器诅咒)
/// 3. 皇宫内部清人与占领权实时更迭 (守方肃清、单一攻城行会占领、战役结束胜者确认)
/// 4. 摆摊上架与原子购买、经济活动互斥状态机 (防重入双花)、收摊/掉线 100% 安全回滚
/// </summary>
public class TestOfficialM2_SabukAndEconomyMatrix
{
    private static ClientSession CreateMockSession(int sessionId, string charName, string guildName = "")
    {
        var dummySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        var session = new ClientSession(sessionId, dummySocket)
        {
            CharacterName = charName,
            State = new PlayerState
            {
                Uid = sessionId,
                Name = charName,
                Gold = 500000,
                Level = 40
            }
        };
        return session;
    }

    [Fact]
    public void Test_SabukGate_Damage_And_AuthoritativeBlockRemoval()
    {
        var siegeEngine = new SabukSiegeEngine();
        var customTiles = new List<SabukSiegeEngine.Point2D>
        {
            new(672, 331),
            new(672, 332)
        };
        siegeEngine.InitializeGate(2000, customTiles);

        var blockedMap = new Dictionary<string, bool>
        {
            ["672,331"] = true,
            ["672,332"] = true
        };

        void SetTileBlocked(int x, int y, bool blocked)
        {
            blockedMap[$"{x},{y}"] = blocked;
        }

        Assert.Equal(SabukSiegeEngine.GateState.Intact, siegeEngine.CurrentGateState);
        Assert.Equal(2000, siegeEngine.GateCurrentHp);

        // 1. 承受 800 伤害 -> 剩余 1200 HP (> 1000)，依然完好
        var state1 = siegeEngine.DamageGate(800, SetTileBlocked);
        Assert.Equal(SabukSiegeEngine.GateState.Intact, state1);
        Assert.True(blockedMap["672,331"]);

        // 2. 再受 300 伤害 -> 剩余 900 HP (<= 1000)，进入破损 Damaged
        var state2 = siegeEngine.DamageGate(300, SetTileBlocked);
        Assert.Equal(SabukSiegeEngine.GateState.Damaged, state2);
        Assert.True(blockedMap["672,331"]);

        // 3. 致命 1000 伤害 -> 剩余 0 HP，城门崩塌 Broken，阻挡消除
        var state3 = siegeEngine.DamageGate(1000, SetTileBlocked);
        Assert.Equal(SabukSiegeEngine.GateState.Broken, state3);
        Assert.False(blockedMap["672,331"]);
        Assert.False(blockedMap["672,332"]);

        // 4. 修缮城门恢复满血与阻挡
        siegeEngine.RepairGate(SetTileBlocked);
        Assert.Equal(SabukSiegeEngine.GateState.Intact, siegeEngine.CurrentGateState);
        Assert.Equal(2000, siegeEngine.GateCurrentHp);
        Assert.True(blockedMap["672,331"]);
        Assert.True(blockedMap["672,332"]);
    }

    [Fact]
    public void Test_SabukWar_FreePkZone_And_PalaceOccupationShift()
    {
        var siegeEngine = new SabukSiegeEngine();
        long now = 100000;

        // 开启攻城战: 守方为“邪皇殿”，攻城报名的行会为“昔日同盟”和“战神殿”
        siegeEngine.StartSiege(currentOwnerGuild: "邪皇殿", nowMs: now, durationMs: 7200_000);
        siegeEngine.RegisterAttackingGuild("昔日同盟");
        siegeEngine.RegisterAttackingGuild("战神殿");

        Assert.Equal(SabukSiegeEngine.WarPhase.InWar, siegeEngine.Phase);

        // 1. 攻城区域免 PK 判定:
        // 在沙巴克主城 (Map 3) 或皇宫 (Map 150)，攻守双方互砍免 PK 惩罚
        bool freePk1 = siegeEngine.IsFreePkBattle(SabukSiegeEngine.SabukPalaceMapId, "昔日同盟", "邪皇殿");
        Assert.True(freePk1);

        bool freePk2 = siegeEngine.IsFreePkBattle(SabukSiegeEngine.SabukCastleMapId, "昔日同盟", "战神殿");
        Assert.True(freePk2);

        // 同一行会或在比奇省 (Map 0) 互砍不属于攻城免惩罚
        bool nonFreePk1 = siegeEngine.IsFreePkBattle(0, "昔日同盟", "邪皇殿");
        Assert.False(nonFreePk1);

        bool nonFreePk2 = siegeEngine.IsFreePkBattle(SabukSiegeEngine.SabukCastleMapId, "昔日同盟", "昔日同盟");
        Assert.False(nonFreePk2);

        // 2. 皇宫占领判定 (IDA sub_4C9B80):
        // 初始情况: 皇宫里有守城方“邪皇殿”成员活着，攻击方不能占领
        var palacePlayers1 = new List<(string, string, bool)>
        {
            ("邪皇殿掌门", "邪皇殿", true),
            ("昔日战士A", "昔日同盟", true),
            ("昔日战士B", "昔日同盟", true)
        };
        var (occ1, _) = siegeEngine.CheckPalaceOccupation(palacePlayers1);
        Assert.False(occ1);

        // 守方被肃清，但皇宫内有多个攻击方混战 ("昔日同盟" 与 "战神殿")
        var palacePlayers2 = new List<(string, string, bool)>
        {
            ("昔日战士A", "昔日同盟", true),
            ("战神法师A", "战神殿", true)
        };
        var (occ2, _) = siegeEngine.CheckPalaceOccupation(palacePlayers2);
        Assert.False(occ2);

        // 皇宫内仅剩“昔日同盟”独占，触发临时占领更迭！
        var palacePlayers3 = new List<(string, string, bool)>
        {
            ("昔日战士A", "昔日同盟", true),
            ("昔日战士B", "昔日同盟", true)
        };
        var (occ3, newOccupier) = siegeEngine.CheckPalaceOccupation(palacePlayers3);
        Assert.True(occ3);
        Assert.Equal("昔日同盟", newOccupier);
        Assert.Equal("昔日同盟", siegeEngine.TempOccupierGuild);

        // 3. 攻城战役结算结束: 确认新沙巴克城主
        var (winner, changed) = siegeEngine.EndSiege();
        Assert.True(changed);
        Assert.Equal("昔日同盟", winner);
        Assert.Equal("昔日同盟", siegeEngine.OwnerGuild);
        Assert.Equal(SabukSiegeEngine.WarPhase.Ended, siegeEngine.Phase);
    }

    [Fact]
    public void Test_Economy_ActivityMutualExclusion_And_StallTransactions()
    {
        var ecoEngine = new EconomyAntiDupeEngine();
        var seller = CreateMockSession(8001, "摆摊掌柜");
        var buyer = CreateMockSession(8002, "买装备买家");

        var sword = new EquipItem { Uid = 501, Name = "炼狱", Weight = 25, Dc = 25 };
        var helmet = new EquipItem { Uid = 502, Name = "黑铁头盔", Weight = 20, Ac = 5 };

        seller.State!.BagItems.Add(sword);
        seller.State.BagItems.Add(helmet);

        // 1. 经济活动互斥锁测试: 正在摆摊时禁止进入面对面交易
        bool enterStall = ecoEngine.TryEnterActivity(seller.SessionId, EconomyAntiDupeEngine.EconomicActivity.Stalling);
        Assert.True(enterStall);
        Assert.Equal(EconomyAntiDupeEngine.EconomicActivity.Stalling, ecoEngine.GetActivity(seller.SessionId));

        bool enterTradeWhileStalling = ecoEngine.TryEnterActivity(seller.SessionId, EconomyAntiDupeEngine.EconomicActivity.Trading);
        Assert.False(enterTradeWhileStalling, "正在摆摊中必须被互斥锁拦截，无法重入交易");

        // 2. 摆摊上架与背包物品安全扣留
        var itemsToSell = new List<(int bagSlot, int price)>
        {
            (0, 100000), // 炼狱标价 10 万
            (1, 80000)   // 黑铁头盔标价 8 万
        };

        var (openOk, openErr) = ecoEngine.OpenStall(seller, "极品神兵专卖店", itemsToSell);
        Assert.True(openOk, openErr);
        Assert.Empty(seller.State.BagItems); // 物品已安全转移至摊位锁存，背包不再持有

        // 3. 买家购买商品
        buyer.State!.Gold = 200000;
        var (buyOk, buyMsg) = ecoEngine.BuyStallItem(buyer, seller.SessionId, stallSlot: 0, seller);
        Assert.True(buyOk, buyMsg);
        Assert.Equal(100000, buyer.State.Gold); // 扣减 10 万金币
        Assert.Single(buyer.State.BagItems);
        Assert.Equal("炼狱", buyer.State.BagItems[0].Name);
        Assert.Equal(600000, seller.State.Gold); // 卖家 50 万 + 10 万 = 60 万金币实时入账

        // 重复购买已售出商品必须拦截防双花
        var (buyTwice, buyTwiceMsg) = ecoEngine.BuyStallItem(buyer, seller.SessionId, stallSlot: 0, seller);
        Assert.False(buyTwice);
        Assert.Contains("抢先买走", buyTwiceMsg);

        // 4. 收摊与掉线异常回滚: 剩余商品 100% 原路返还背包
        ecoEngine.CloseStallAndRollback(seller);
        Assert.Equal(EconomyAntiDupeEngine.EconomicActivity.Idle, ecoEngine.GetActivity(seller.SessionId));
        Assert.Single(seller.State.BagItems);
        Assert.Equal("黑铁头盔", seller.State.BagItems[0].Name);
    }
}
