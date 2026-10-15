using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Mir2.Server.Game;
using Mir2.Server.Maps;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 盛大官方 1.5 沙巴克攻城战、皇宫争夺与全服经济防刷（面对面交易与仓库互斥）矩阵测试
/// 100% 覆盖:
/// 1. 沙巴克城门血量与倒塌破坏状态机 (2000 HP, Damaged, Broken 消除网格阻挡, 修复恢复阻挡)
/// 2. 攻城战役期沙巴克与皇宫免 PK 惩罚 (红名豁免、不加 PK 点数、不加深武器诅咒)
/// 3. 皇宫内部清人与占领权实时更迭 (守方肃清、单一攻城行会占领、战役结束胜者确认)
/// 4. 官方 1.5 面对面交易与仓库经济活动互斥状态机 (防重入双花)、交易容量原子校验 (背包 40 格与金币超限拦截)
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
        // 守方被肃清，但皇宫内有散人闲逛，不可占领
        var palacePlayersWithWanderer = new List<(string, string, bool)>
        {
            ("昔日战士A", "昔日同盟", true),
            ("路人甲", "", true)
        };
        var (occWanderer, _) = siegeEngine.CheckPalaceOccupation(palacePlayersWithWanderer);
        Assert.False(occWanderer);

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

        // 原守城方“邪皇殿”组织主力反扑，全歼攻城方，成功夺回皇宫！
        var palacePlayersCounterAttack = new List<(string, string, bool)>
        {
            ("邪皇殿掌门", "邪皇殿", true),
            ("邪皇殿法神", "邪皇殿", true)
        };
        var (occCounter, counterOccupier) = siegeEngine.CheckPalaceOccupation(palacePlayersCounterAttack);
        Assert.True(occCounter);
        Assert.Equal("邪皇殿", counterOccupier);
        Assert.Equal("邪皇殿", siegeEngine.TempOccupierGuild);

        // 3. 攻城战役结算结束: 确认守城方成功保住沙巴克城主宝座
        var (winner, changed) = siegeEngine.EndSiege();
        Assert.False(changed);
        Assert.Equal("邪皇殿", winner);
        Assert.Equal("邪皇殿", siegeEngine.OwnerGuild);
        Assert.Equal(SabukSiegeEngine.WarPhase.Ended, siegeEngine.Phase);
    }

    [Fact]
    public void Test_Economy_ActivityMutualExclusion_And_TradeCapacityValidation()
    {
        var ecoEngine = new EconomyAntiDupeEngine();
        var playerA = CreateMockSession(8001, "战士豪杰");
        var playerB = CreateMockSession(8002, "道士法师");

        var sword = new EquipItem { Uid = 501, Name = "炼狱", Weight = 25, Dc = 25 };
        var helmet = new EquipItem { Uid = 502, Name = "黑铁头盔", Weight = 20, Ac = 5 };

        playerA.State!.BagItems.Add(sword);
        playerA.State.BagItems.Add(helmet);

        // 1. 经济活动互斥锁测试: 正在面对面交易时禁止进入仓库操作
        bool enterTrade = ecoEngine.TryEnterActivity(playerA.SessionId, EconomyAntiDupeEngine.EconomicActivity.Trading);
        Assert.True(enterTrade);
        Assert.Equal(EconomyAntiDupeEngine.EconomicActivity.Trading, ecoEngine.GetActivity(playerA.SessionId));

        bool enterWarehouseWhileTrading = ecoEngine.TryEnterActivity(playerA.SessionId, EconomyAntiDupeEngine.EconomicActivity.Warehousing);
        Assert.False(enterWarehouseWhileTrading, "正在交易中必须被互斥锁拦截，无法重入仓库存取");

        // 退出交易活动后恢复 Idle
        ecoEngine.LeaveActivity(playerA.SessionId);
        Assert.Equal(EconomyAntiDupeEngine.EconomicActivity.Idle, ecoEngine.GetActivity(playerA.SessionId));

        // 进入仓库后，禁止再发起或接受面对面交易
        bool enterWarehouse = ecoEngine.TryEnterActivity(playerA.SessionId, EconomyAntiDupeEngine.EconomicActivity.Warehousing);
        Assert.True(enterWarehouse);
        bool enterTradeWhileWarehouse = ecoEngine.TryEnterActivity(playerA.SessionId, EconomyAntiDupeEngine.EconomicActivity.Trading);
        Assert.False(enterTradeWhileWarehouse, "正在仓库操作中必须被互斥锁拦截，无法进入交易");
        ecoEngine.LeaveActivity(playerA.SessionId);

        // 2. 盛大官方 1.5 面对面交易容量与金币合法性校验
        var itemsForA = new List<EquipItem> { new EquipItem { Uid = 601, Name = "井中月" } };
        var itemsForB = new List<EquipItem> { sword };

        var (validOk, validErr) = EconomyAntiDupeEngine.ValidateTradeCapacity(
            playerA, itemsForA, -10000,
            playerB, itemsForB, +10000);
        Assert.True(validOk, validErr);

        // 背包超出 40 格拦截测试
        for (int i = 0; i < 40; i++)
        {
            playerB.State!.BagItems.Add(new EquipItem { Uid = 700 + i, Name = "金创药(小量)" });
        }
        var (overBagOk, overBagErr) = EconomyAntiDupeEngine.ValidateTradeCapacity(
            playerA, itemsForA, 0,
            playerB, itemsForB, 0);
        Assert.False(overBagOk);
        Assert.Contains("背包空间不足", overBagErr);
    }

    [Fact]
    public void Test_SabukSiege_ReviveAndTownTeleportRouting_DistinguishesDefenderAndAttacker()
    {
        var siegeEngine = new SabukSiegeEngine();
        long now = 1000000;
        siegeEngine.StartSiege("邪风军团", now, durationMs: 7200_000);
        siegeEngine.RegisterAttackingGuild("兄弟盟");

        var defenderState = new PlayerState { CharacterName = "沙巴克大将", PkPoints = 0 };
        var attackerState = new PlayerState { CharacterName = "攻城勇士", PkPoints = 0 };
        var redNameState = new PlayerState { CharacterName = "恶贯满盈", PkPoints = 300 };

        // 1. 沙城守军在沙巴克城 (MapId 3) 阵亡复活 -> 复活在沙巴克城池内部 (3, 644, 290)
        var (defMap, defX, defY, defMsg) = RedNameDropAndReviveEngine.GetReviveLocation(
            defenderState, currentMapId: 3, siegeEngine, "邪风军团");
        Assert.Equal(3, defMap);
        Assert.Equal(644, defX);
        Assert.Equal(290, defY);
        Assert.Contains("沙巴克", defMsg);

        // 2. 攻城方在沙皇宫 (MapId 150) 阵亡复活 -> 回盟重土城 (3, 335, 330)
        var (attMap, attX, attY, attMsg) = RedNameDropAndReviveEngine.GetReviveLocation(
            attackerState, currentMapId: 150, siegeEngine, "兄弟盟");
        Assert.Equal(3, attMap);
        Assert.Equal(335, attX);
        Assert.Equal(330, attY);
        Assert.Contains("盟重土城", attMsg);

        // 3. 红名玩家无论身处何处阵亡 -> 强制流放红名村 (3, 850, 660)
        var (redMap, redX, redY, redMsg) = RedNameDropAndReviveEngine.GetReviveLocation(
            redNameState, currentMapId: 150, siegeEngine, "邪风军团");
        Assert.Equal(3, redMap);
        Assert.Equal(850, redX);
        Assert.Equal(660, redY);
        Assert.Contains("盟重红名村", redMsg);

        // 4. 回城卷轴路由: 守城方攻城期间在沙城使用回城卷返回沙巴克 (3, 644, 290)
        var defSession = CreateMockSession(8101, "守城卫士");
        defSession.CurrentMapId = 3;
        defSession.State!.Hp = 100;
        var (tOk, tMap, tX, tY, tMsg) = TeleportScrollEngine.UseTownTeleport(
            defSession, new MapManager(), siegeEngine, "邪风军团");
        Assert.True(tOk);
        Assert.Equal(3, tMap);
        Assert.Equal(644, tX);
        Assert.Equal(290, tY);
        Assert.Contains("沙巴克城池阵地", tMsg);
    }

    [Fact]
    public void Test_Official15_Blacksmith_NormalRepair_DuraLoss_And_SpecialRepair_ZeroLoss()
    {
        var merchant = new NpcMerchantEngine();
        var session = CreateMockSession(8201, "铁血战士");
        session.State!.Gold = 100000;

        var weapon = new EquipItem
        {
            Uid = 999,
            Name = "裁决之杖",
            Dura = 10,
            MaxDura = 32
        };
        session.State.Equipment[(int)OfficialEquipSlot.Weapon] = weapon;

        // 1. 铁匠铺普通修理: 消耗金币，但扣减 MaxDura 持久上限 (掉上限为损失量的 1/3，至少 1 点)
        int initialMaxDura = weapon.MaxDura;
        var (nOk, nCost, nName, nErr) = merchant.NormalRepairItem(session, (int)OfficialEquipSlot.Weapon);
        Assert.True(nOk, nErr);
        Assert.True(weapon.MaxDura < initialMaxDura, $"普通修理必须衰减最大持久上限，当前上限: {weapon.MaxDura}");
        Assert.Equal(weapon.MaxDura, weapon.Dura);

        // 2. 模拟再次磨损
        weapon.Dura = 15;
        int currentMaxDura = weapon.MaxDura;

        // 3. 铁匠铺特修: 消耗 3 倍金币，持久 100% 满额无损回满！
        int beforeGold = session.State.Gold;
        var (sOk, sCost, sName, sErr) = merchant.SpecialRepairItem(session, (int)OfficialEquipSlot.Weapon);
        Assert.True(sOk, sErr);
        Assert.Equal(currentMaxDura, weapon.Dura);
        Assert.Equal(currentMaxDura, weapon.MaxDura); // 特修 0 损耗
        Assert.Equal(beforeGold - (currentMaxDura - 15) * 3, session.State.Gold);
    }


    [Fact]
    public void Test_PetDeregistration_OnReviveAndDisconnect()
    {
        var map = new GameMap(0, "比奇省", 500, 500);
        var mapMgr = new MapManager();
        mapMgr.RegisterMap(map);
        var regenEngine = new MonsterRegenEngine(mapMgr);
        var dispatcher = new PacketDispatcher(mapMgr, regenEngine);

        var master = CreateMockSession(8301, "道家仙尊");
        master.CurrentMapId = 0;
        master.X = 330;
        master.Y = 270;
        master.State!.Hp = 0; // 模拟已阵亡

        // 刷出神兽随从
        regenEngine.AddRule(new MonsterSpawnRule
        {
            RuleId = 1,
            MonsterName = "神兽",
            MapId = 0,
            CenterX = 330,
            CenterY = 270,
            Radius = 0,
            MaxCount = 1,
            BaseHp = 500
        });
        regenEngine.ProcessRegenTick(new OfficialClientRulesEngine.DelphiRandom(1));
        var petMonster = regenEngine.FindMonsterAt(0, 330, 270);
        Assert.NotNull(petMonster);
        int petUid = petMonster.Uid;

        master.Pets.Add(new HuWeiPetEngine.SlavePet
        {
            Uid = petUid,
            Name = "神兽",
            MasterSessionId = master.SessionId,
            CurrentMapId = 0,
            X = 330,
            Y = 270
        });
        Assert.Single(master.Pets);

        // 执行主人死亡复活 -> 随从宝宝消散并从怪物世界彻底注销
        dispatcher.HandleRevive(master, ReadOnlySpan<char>.Empty);
        Assert.Empty(master.Pets);
        Assert.Null(regenEngine.GetMonster(petUid)); // 彻底注销，杜绝幽灵怪！
    }

    [Fact]
    public void Test_SabukPalace_AntiTeleportBarrier_BlocksMemorySummonAndTeleportRing()
    {
        var mapMgr = new MapManager();
        var palaceMap = new GameMap(150, "沙巴克皇宫", 100, 100);
        var bimap = new GameMap(0, "比奇省", 500, 500);
        mapMgr.RegisterMap(palaceMap);
        mapMgr.RegisterMap(bimap);

        // 1. 记忆套 [@天地合一]: 队长在沙巴克皇宫施展必被结界拦截

        var leader = CreateMockSession(8403, "记忆套掌门");
        leader.CurrentMapId = 150;
        leader.State!.Equipment[0] = new EquipItem { Name = "记忆头盔" };
        leader.State.Equipment[1] = new EquipItem { Name = "记忆项链" };
        leader.State.Equipment[2] = new EquipItem { Name = "记忆手镯" };
        leader.State.Equipment[3] = new EquipItem { Name = "记忆戒指" };

        var member1 = CreateMockSession(8404, "先锋队员");
        member1.CurrentMapId = 0;

        var (mOk, mCount, mMsg) = SuitWorldEventEngine.ExecuteMemorySummonAll(leader, new[] { leader, member1 }, mapMgr);
        Assert.False(mOk);
        Assert.Contains("沙巴克皇宫受禁空结界封锁", mMsg);

        // 4. 特戒传送 [@move]: 皇宫内施展必被拦截
        var dispatcher = new PacketDispatcher(mapMgr);
        var teleporter = CreateMockSession(8405, "传送法师");
        teleporter.CurrentMapId = 150;
        teleporter.State!.Equipment[7] = new EquipItem { Name = "传送戒指" };
        teleporter.State.RecalculateAttributes();

        dispatcher.HandleSay(teleporter, "@move 50 50");
        Assert.NotEqual(50, teleporter.X); // 坐标未变动，成功拦截
    }

    [Fact]
    public void Test_Official15_NpcMerchant_BulkBuySell_WeightLimit_And_InventoryFull()
    {
        var merchant = new NpcMerchantEngine();
        var session = CreateMockSession(9001, "传奇商贾");
        session.State!.Gold = 500000;
        session.State.BagWeight = 0;
        session.State.MaxBagWeight = 200;

        // 注册比奇铁匠铺 NPC (NPC 101)
        var npc = new NpcEntity { Uid = 101, Name = "铁匠铺老板", MapId = 0, X = 330, Y = 330 };
        npc.Goods.Add(new MerchantGoods { GoodsId = 1, Name = "乌木剑", Price = 1000, Weight = 8, MaxDura = 10 });
        npc.Goods.Add(new MerchantGoods { GoodsId = 2, Name = "青铜剑", Price = 2000, Weight = 15, MaxDura = 12 });
        merchant.RegisterNpc(npc);

        // 1. 成功购买一件乌木剑
        var (ok1, err1, item1) = merchant.BuyItem(session, 101, 1);
        Assert.True(ok1, err1);
        Assert.NotNull(item1);
        Assert.Equal("乌木剑", item1.Name);
        Assert.Equal(499000, session.State.Gold);
        Assert.Equal(8, session.State.BagWeight);
        Assert.Contains(item1, session.State.BagItems);

        // 2. 负重超限测试: 将当前负重设为 195，再买 15 重量的青铜剑必被拦截
        session.State.BagWeight = 195;
        var (ok2, err2, item2) = merchant.BuyItem(session, 101, 2);
        Assert.False(ok2);
        Assert.Contains("重", err2);
        Assert.Equal(499000, session.State.Gold); // 扣款回滚

        // 3. 背包满仓测试: 40 格占满后购买必被拦截
        session.State.BagWeight = 20;
        while (session.State.BagItems.Count < 40)
        {
            session.State.BagItems.Add(new EquipItem { Uid = 10000 + session.State.BagItems.Count, Name = "金创药", Weight = 1 });
        }
        Assert.Equal(40, session.State.BagItems.Count);

        var (ok3, err3, item3) = merchant.BuyItem(session, 101, 1);
        Assert.False(ok3);
        Assert.Contains("满", err3);

        // 4. 出售物品回收金币
        var swordToSell = session.State.BagItems[0];
        int goldBefore = session.State.Gold;
        var (sellOk, sellGold, itemName, sellErr) = merchant.SellItem(session, swordToSell.Uid);
        Assert.True(sellOk, sellErr);
        Assert.True(session.State.Gold > goldBefore);
        Assert.DoesNotContain(swordToSell, session.State.BagItems);
        Assert.Equal(39, session.State.BagItems.Count);
    }
}


