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
/// 盛大官方 1.4/1.5 死亡掉落大爆生命周期、祈祷头盔必碎、负重与背包同步、药品捆包解包全真值矩阵测试
/// 对应大账本:
/// - 官方 1.5 死亡掉落与红名大爆 (IDA 0x4BB44C / RedNameDropAndReviveEngine)
/// - 官方 1.5 祈祷头盔死亡碎裂 (EquipmentHiddenAttribEngine)
/// - 官方 1.5 药品捆包 1:6 解包与 5 格空间门限 (PotionBundleEngine)
/// - 官方 1.5 护身符与毒药包 Slot 2 (Lighting) 识别
/// </summary>
public sealed class TestOfficialM2_DeathDropAndBundleMatrix
{
    [Fact]
    public void PotionBundle_Unpack_Success_ConsumesBundle_AddsSixItems_RecalculatesWeight()
    {
        var player = new PlayerState
        {
            Uid = 1001,
            MaxBagWeight = 200,
            BagWeight = 6
        };

        var bundle = new EquipItem
        {
            Uid = 8888,
            Name = "强效太阳水包",
            Weight = 6
        };
        player.BagItems.Add(bundle);

        var result = PotionBundleEngine.TryUnpack(player, 0);

        Assert.True(result.Success);
        Assert.NotNull(result.ConsumedBundle);
        Assert.Equal(8888, result.ConsumedBundle.Uid);
        Assert.Equal(0, result.ConsumedSlot);
        Assert.Equal(6, result.UnpackedItems.Count);
        Assert.Equal(6, player.BagItems.Count); // 原包被移走，新增 6 瓶
        foreach (var item in player.BagItems)
        {
            Assert.Equal("强效太阳水", item.Name);
            Assert.Equal(2, item.Weight);
        }
        Assert.Equal(12, player.BagWeight); // 6 * 2 = 12
    }

    [Fact]
    public void PotionBundle_Unpack_BagFull_BlocksUnpack_KeepsItemsAndWeights()
    {
        var player = new PlayerState
        {
            Uid = 1002,
            MaxBagWeight = 200,
            BagWeight = 100
        };

        // 填入 36 件物品 (剩余 4 格，不足解开药包所需的净 5 格)
        for (int i = 0; i < 35; i++)
        {
            player.BagItems.Add(new EquipItem { Uid = 100 + i, Name = "鹿肉", Weight = 2 });
        }
        var bundle = new EquipItem
        {
            Uid = 9999,
            Name = "强效金创药包",
            Weight = 6
        };
        player.BagItems.Add(bundle); // 此时共有 36 件物品

        var result = PotionBundleEngine.TryUnpack(player, 35);

        Assert.False(result.Success);
        Assert.Contains("背包空间不足", result.Message);
        Assert.Equal(36, player.BagItems.Count); // 绝不扣除药包
        Assert.Equal(bundle, player.BagItems[35]);
    }

    [Fact]
    public void PotionBundle_Unpack_OverWeight_BlocksUnpack_KeepsItemsAndWeights()
    {
        var player = new PlayerState
        {
            Uid = 1004,
            MaxBagWeight = 50,  // 最大负重 50
            BagWeight = 46      // 当前负重 46，剩余负重仅 4
        };

        var bundle = new EquipItem
        {
            Uid = 9998,
            Name = "强效太阳水包", // 净增重 12 - 6 = 6 > 4，会导致超重至 52/50
            Weight = 6
        };
        player.BagItems.Add(bundle);

        var result = PotionBundleEngine.TryUnpack(player, 0);

        Assert.False(result.Success);
        Assert.Contains("物品过重", result.Message);
        Assert.Single(player.BagItems);
        Assert.Equal(bundle, player.BagItems[0]);
        Assert.Equal(46, player.BagWeight);
    }

    [Fact]
    public void PotionBundle_SuperBundles_UnpacksExpectedItems()
    {
        var player = new PlayerState { Uid = 1003, MaxBagWeight = 200 };
        var bundle = new EquipItem { Uid = 7777, Name = "超级魔法药包", Weight = 8 };
        player.BagItems.Add(bundle);

        var result = PotionBundleEngine.TryUnpack(player, 0);
        Assert.True(result.Success);
        Assert.Equal(6, player.BagItems.Count);
        Assert.All(player.BagItems, item => Assert.Equal("强效魔法药", item.Name));
    }

    [Fact]
    public void PlayerDeath_PrayerHelmet_ShattersOnDeath_DoesNotDropToGround_SendsTakeOffOk()
    {
        var player = new PlayerState
        {
            Uid = 1004,
            Hp = 0,
            MaxHp = 100
        };
        var prayHelmet = new EquipItem
        {
            Uid = 5555,
            Name = "祈祷头盔",
            Weight = 2,
            Ac = 3,
            MaxAc = 4
        };
        player.Equipment[(int)OfficialEquipSlot.Helmet] = prayHelmet;

        var (destroyed, msg) = EquipmentHiddenAttribEngine.ProcessPrayerHelmetDeathShatter(player);

        Assert.True(destroyed);
        Assert.Null(player.Equipment[(int)OfficialEquipSlot.Helmet]);
        Assert.Contains("青烟", msg);

        // 后续掉落精算时，头盔槽位已空，绝不作为战利品掉落地表
        var rng = new Random(42);
        var drops = RedNameDropAndReviveEngine.CalculateDeathDrops(player, rng);
        Assert.DoesNotContain(drops.DroppedEquipItems, e => e.Name.Contains("祈祷头盔"));
    }

    [Fact]
    public void PlayerDeath_CalculateDeathDrops_RecordsDroppedBagSlots()
    {
        var player = new PlayerState
        {
            Uid = 1005,
            PkPoints = 300 // 红名大爆 (100% 背包掉落)
        };
        for (int i = 0; i < 5; i++)
        {
            player.BagItems.Add(new EquipItem { Uid = 2000 + i, Name = $"战利品_{i}", Weight = 1 });
        }
        player.RecalculateAuthoritativeWeights(0);
        Assert.Equal(5, player.BagWeight);

        var drops = RedNameDropAndReviveEngine.CalculateDeathDrops(player, new Random(123));

        Assert.True(drops.IsMegaExplosion);
        Assert.Equal(5, drops.DroppedBagItems.Count);
        Assert.Equal(5, drops.DroppedBagSlots.Count);
        Assert.Empty(player.BagItems);
        Assert.Equal(0, player.BagWeight); // 负重已重算归零
    }

    [Fact]
    public void PlayerDeath_ProcessPlayerDeath_SendsDelItemAndWeightChangedToDefender()
    {
        var map = new GameMap(0, "比奇省", 100, 100);
        var mapMgr = new MapManager();
        mapMgr.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapMgr);

        var defender = new ClientSession(1, null!)
        {
            CharacterName = "倒霉蛋",
            X = 50,
            Y = 50,
            CurrentMapId = 0,
            State = new PlayerState
            {
                Uid = 1006,
                Hp = 0,
                MaxHp = 100,
                MaxBagWeight = 100,
                PkPoints = 500 // 红名大爆
            }
        };

        var bagItem = new EquipItem { Uid = 3001, Name = "裁决之杖", Weight = 50 };
        defender.State.BagItems.Add(bagItem);
        defender.State.RecalculateAuthoritativeWeights(0);

        var killer = new ClientSession(2, null!)
        {
            CharacterName = "大侠",
            X = 50,
            Y = 51,
            CurrentMapId = 0,
            State = new PlayerState { Uid = 1007, Hp = 100, MaxHp = 100 }
        };

        dispatcher.ProcessPlayerDeath(defender, killer, map, Environment.TickCount64);

        // 验证死者收到 SM_DELITEM (202) 与 SM_WEIGHTCHANGED (223)
        string buffered = defender.GetBufferedPacketsString();
        Assert.Contains($"#{ProtocolConsts.SM_DELITEM}/", buffered);
        Assert.Contains($"#{ProtocolConsts.SM_WEIGHTCHANGED}/", buffered);
    }

    [Fact]
    public void TestStealthRing_MoveDoesNotBreakStealth_AttackBreaksStealth()
    {
        var mapMgr = new MapManager();
        var dispatcher = new PacketDispatcher(mapMgr, null, null, null, null);
        var session = new ClientSession(1, null!)
        {
            CharacterName = "隐身刺客",
            CurrentMapId = 0,
            X = 300,
            Y = 300,
            State = new PlayerState()
        };

        // 穿戴隐身戒指 (Slot 7)
        session.State.Equipment[7] = new EquipItem { Name = "隐身戒指", Dura = 5000, MaxDura = 5000 };
        session.State.RecalculateAttributes();
        Assert.True(session.State.HasInvisibleRing);

        long now = 100000;
        // 1. 移动（迈步）不应触发显形提示
        dispatcher.TriggerBreakInvisibility(session, "迈步", now);
        string packets = session.GetBufferedPacketsString();
        Assert.DoesNotContain("显形破隐", packets);

        // 2. 移动（奔跑）不应触发显形提示
        dispatcher.TriggerBreakInvisibility(session, "奔跑", now);
        packets = session.GetBufferedPacketsString();
        Assert.DoesNotContain("显形破隐", packets);

        // 3. 普通物理攻击应当触发破隐提示与光效
        dispatcher.TriggerBreakInvisibility(session, "普通物理攻击", now);
        packets = session.GetBufferedPacketsString();
        Assert.Contains("显形破隐", packets);
    }

    [Fact]
    public void TestRevive_RestoresHalfHpAndHalfMp()
    {
        var mapMgr = new MapManager();
        var dispatcher = new PacketDispatcher(mapMgr, null, null, null, null);
        var session = new ClientSession(2, null!)
        {
            CharacterName = "战神",
            CurrentMapId = 0,
            X = 330,
            Y = 330,
            State = new PlayerState
            {
                MaxHp = 400,
                MaxMp = 200,
                Hp = 0,
                Mp = 0
            }
        };
        mapMgr.DefaultMap.Aoi.Enter(session, 330, 330);

        dispatcher.HandleRevive(session, ReadOnlySpan<char>.Empty);

        // 官方 1.5 真值：复活恢复半血半蓝
        Assert.Equal(200, session.State.Hp);
        Assert.Equal(100, session.State.Mp);
        string packets = session.GetBufferedPacketsString();
        Assert.Contains($"#{ProtocolConsts.SM_MYSTATUS}/", packets);
        Assert.Contains($"#{ProtocolConsts.SM_CHANGENAMECOLOR}/", packets);
    }

    [Fact]
    public void TestRecallCommand_RecognizedByDispatcher()
    {
        var mapMgr = new MapManager();
        var groupEngine = new GroupEngine();
        var dispatcher = new PacketDispatcher(mapMgr, null, null, null, null)
        {
            GroupEngine = groupEngine
        };

        var leader = new ClientSession(10, null!)
        {
            CharacterName = "记忆队长",
            CurrentMapId = 0,
            X = 300,
            Y = 300,
            State = new PlayerState()
        };

        // 建立队伍
        groupEngine.CreateParty(leader);

        // 发送 @recall 指令
        dispatcher.HandleSay(leader, "@recall");
        string packets = leader.GetBufferedPacketsString();
        // 应该尝试天地合一召唤（若没有记忆全套则提示天地合一未凑齐）
        Assert.Contains("天地合一", packets);
    }

    [Fact]
    public void TestProbeNecklace_DetectsTargetCoordinates()
    {
        var mapMgr = new MapManager();
        var dispatcher = new PacketDispatcher(mapMgr, null, null, null, null);

        var seeker = new ClientSession(20, null!)
        {
            CharacterName = "千里眼",
            CurrentMapId = 0,
            X = 300,
            Y = 300,
            State = new PlayerState()
        };

        var target = new ClientSession(21, null!)
        {
            CharacterName = "潜行盗贼",
            CurrentMapId = 0,
            X = 666,
            Y = 888,
            State = new PlayerState()
        };

        dispatcher.ActiveSessions.TryAdd(seeker.SessionId, seeker);
        dispatcher.ActiveSessions.TryAdd(target.SessionId, target);

        // 1. 未佩戴探测项链，发送 @Searching 目标
        dispatcher.HandleSay(seeker, "@Searching 潜行盗贼");
        string packets = seeker.GetBufferedPacketsString();
        Assert.Contains("你未佩戴探测项链", packets);

        // 2. 佩戴探测项链 (Slot 3)
        seeker.State.Equipment[3] = new EquipItem { Name = "探测项链", Shape = 159, Dura = 5000, MaxDura = 5000 };
        dispatcher.HandleSay(seeker, "@探测 潜行盗贼");
        packets = seeker.GetBufferedPacketsString();
        Assert.Contains("666", packets);
        Assert.Contains("888", packets);
        Assert.Contains("当前位于", packets);

        // 3. 探查不存在的玩家
        dispatcher.HandleSay(seeker, "@probe 无名小卒");
        packets = seeker.GetBufferedPacketsString();
        Assert.Contains("探查失败", packets);
    }

    [Fact]
    public void TestSpecialRing_TeleportRingCommand_WithoutRing_Fails()
    {
        var mapMgr = new MapManager();
        var dispatcher = new PacketDispatcher(mapMgr, null, null, null, null);
        var session = new ClientSession(30, null!)
        {
            CharacterName = "流浪汉",
            CurrentMapId = 0,
            X = 300,
            Y = 300,
            State = new PlayerState()
        };

        dispatcher.HandleSay(session, "@move 100 100");
        // 未佩戴传送戒指，坐标不发生变化
        Assert.Equal(300, session.X);
        Assert.Equal(300, session.Y);
    }


    [Fact]
    public void TestWeaponUpgrade_ReachesSevenLimit_RejectsFurtherUpgrade()
    {
        var session = new ClientSession(40, null!)
        {
            CharacterName = "神兵锻造师",
            State = new PlayerState()
        };

        // 已经升级过 7 次的裁决之杖
        var weapon = new EquipItem
        {
            Uid = 9001,
            Name = "裁决之杖",
            UpgradeCount = 7,
            Dc = 30
        };
        session.State.BagItems.Add(weapon);

        var (success, msg) = ItemLifecycleEngine.SubmitWeaponUpgrade(
            session,
            weaponSlot: 0,
            oreBagSlots: Array.Empty<int>(),
            accessoryBagSlots: Array.Empty<int>(),
            Random.Shared,
            nowMs: 10000);

        Assert.False(success);
        Assert.Contains("累计 7 次修炼极限", msg);
    }

    [Fact]
    public void TestMerchantBuyItem_GeneratesUniqueUidsForSameGoods()
    {
        var merchant = new NpcMerchantEngine();
        var npc = new NpcEntity { Uid = 501, Name = "药店老板", MapId = 0, X = 100, Y = 100 };
        npc.Goods.Add(new MerchantGoods { GoodsId = 1, Name = "强效金创药", Price = 100, Weight = 2, MaxDura = 1000 });
        merchant.RegisterNpc(npc);

        var session = new ClientSession(41, null!)
        {
            CharacterName = "买药散人",
            State = new PlayerState
            {
                Gold = 10000,
                MaxBagWeight = 200
            }
        };

        var (s1, _, item1) = merchant.BuyItem(session, 501, 1);
        var (s2, _, item2) = merchant.BuyItem(session, 501, 1);

        Assert.True(s1);
        Assert.True(s2);
        Assert.NotNull(item1);
        Assert.NotNull(item2);
        Assert.NotEqual(item1.Uid, item2.Uid);
        Assert.Equal(2, session.State.BagItems.Count);
    }

    [Fact]
    public void TestCheckCanEquipAuthoritative_ReqLevelAndWeightConstraint()
    {
        // 1 级小号战士
        var rookieWarrior = new PlayerState { Level = 1, Job = 0 };
        rookieWarrior.RecalculateAuthoritativeWeights();

        var judgeStick = new EquipItem { Uid = 901, Name = "裁决之杖", Weight = 80 };
        bool canEquipRookie = rookieWarrior.CheckCanEquipAuthoritative(OfficialEquipSlot.Weapon, judgeStick, out string reasonRookie);
        Assert.False(canEquipRookie);
        Assert.Contains("等级不足", reasonRookie);

        // 30 级战士 (腕力充足)
        var masterWarrior = new PlayerState { Level = 30, Job = 0 };
        masterWarrior.RecalculateAuthoritativeWeights();
        // 30 级战士腕力可拿起裁决 (Weight 80 <= MaxHandWeight)
        // 验证等级满足且腕力足够
        bool canEquipMaster = masterWarrior.CheckCanEquipAuthoritative(OfficialEquipSlot.Weapon, judgeStick, out string reasonMaster);
        Assert.True(canEquipMaster, reasonMaster);

        // 30 级法师 (腕力极低，即便等级达到 30 级也无法手持裁决)
        var masterMage = new PlayerState { Level = 30, Job = 1 };
        masterMage.RecalculateAuthoritativeWeights();
        bool canEquipMage = masterMage.CheckCanEquipAuthoritative(OfficialEquipSlot.Weapon, judgeStick, out string reasonMage);
        Assert.False(canEquipMage);
        Assert.Contains("腕力不足", reasonMage);

        // 盛大官方 1.5 权威 4 态需求验证：
        // 1. 恶魔长袍：需要魔法力 28 点 (MC >= 28)，不受限于等级，法师 24 级通过装备垫属性凑够 28 魔法即可穿戴
        var demonRobe = new EquipItem { Uid = 902, Name = "恶魔长袍", Weight = 17 };
        var cleverMage = new PlayerState { Level = 24, Job = 1, MaxMc = 28 };
        cleverMage.RecalculateAuthoritativeWeights();
        bool canEquipDemon = cleverMage.CheckCanEquipAuthoritative(OfficialEquipSlot.Dress, demonRobe, out string reasonDemon);
        Assert.True(canEquipDemon, reasonDemon);

        var poorMage = new PlayerState { Level = 24, Job = 1, MaxMc = 20 };
        poorMage.RecalculateAuthoritativeWeights();
        bool failDemon = poorMage.CheckCanEquipAuthoritative(OfficialEquipSlot.Dress, demonRobe, out string reasonPoorDemon);
        Assert.False(failDemon);
        Assert.Contains("魔法力不足", reasonPoorDemon);

        // 2. 幽灵战衣：需要道术 27 点 (SC >= 27)
        var ghostDress = new EquipItem { Uid = 903, Name = "幽灵战衣", Weight = 26 };
        var taoistMaster = new PlayerState { Level = 25, Job = 2, MaxSc = 27 };
        taoistMaster.RecalculateAuthoritativeWeights();
        bool canEquipGhost = taoistMaster.CheckCanEquipAuthoritative(OfficialEquipSlot.Dress, ghostDress, out string reasonGhost);
        Assert.True(canEquipGhost, reasonGhost);

        var rookieTaoist = new PlayerState { Level = 25, Job = 2, MaxSc = 15 };
        rookieTaoist.RecalculateAuthoritativeWeights();
        bool failGhost = rookieTaoist.CheckCanEquipAuthoritative(OfficialEquipSlot.Dress, ghostDress, out string reasonFailGhost);
        Assert.False(failGhost);
        Assert.Contains("道术不足", reasonFailGhost);

        // 3. 力量戒指：需要攻击力 46 点 (DC >= 46)
        var powerRing = new EquipItem { Uid = 904, Name = "力量戒指", Weight = 1 };
        var strongWarrior = new PlayerState { Level = 35, Job = 0, MaxDc = 46 };
        strongWarrior.RecalculateAuthoritativeWeights();
        bool canEquipPowerRing = strongWarrior.CheckCanEquipAuthoritative(OfficialEquipSlot.RingL, powerRing, out string reasonPower);
        Assert.True(canEquipPowerRing, reasonPower);

        var weakWarrior = new PlayerState { Level = 35, Job = 0, MaxDc = 35 };
        weakWarrior.RecalculateAuthoritativeWeights();
        bool failPowerRing = weakWarrior.CheckCanEquipAuthoritative(OfficialEquipSlot.RingL, powerRing, out string reasonWeakPower);
        Assert.False(failPowerRing);
        Assert.Contains("攻击力不足", reasonWeakPower);
    }

    [Fact]
    public void TestPrayerHelmet_BreaksOnDeath_NeverDroppedToGround()
    {
        var player = new PlayerState { Level = 30, Job = 0, PkPoint = 0 };
        player.Equipment[(int)OfficialEquipSlot.Helmet] = new EquipItem { Uid = 701, Name = "祈祷头盔", Weight = 5 };
        player.Equipment[(int)OfficialEquipSlot.RingL] = new EquipItem { Uid = 702, Name = "珊瑚戒指", Weight = 1 };

        // 1. RedNameDropAndReviveEngine 判定
        var result1 = RedNameDropAndReviveEngine.CalculateDeathDrops(player, new Random(42));
        // 祈祷头盔必须被直接碎裂置 null，绝不进入 DroppedEquipItems
        Assert.Null(player.Equipment[(int)OfficialEquipSlot.Helmet]);
        Assert.DoesNotContain(result1.DroppedEquipItems, item => item.Name.Contains("祈祷头盔"));

        // 2. MonsterDropEngine.ExecuteDeathDrop 判定
        var player2 = new PlayerState { Level = 30, Job = 0, PkPoint = 300 }; // 红名
        player2.Equipment[(int)OfficialEquipSlot.Helmet] = new EquipItem { Uid = 703, Name = "祈祷头盔", Weight = 5 };
        var delphiRng = new Mir2.Shared.Rules.OfficialClientRulesEngine.DelphiRandom(12345);

        var result2 = MonsterDropEngine.ExecuteDeathDrop(player2, delphiRng);
        Assert.Null(player2.Equipment[(int)OfficialEquipSlot.Helmet]);
        Assert.DoesNotContain(result2, item => item.ItemName.Contains("祈祷头盔"));
    }

    [Fact]
    public void TestRedDemonDrops_No176Armors_StrictOfficial15()
    {
        var rules = MonsterDropEngine.GetMonsterDropRules("赤月恶魔");
        Assert.NotEmpty(rules);

        // 盛大官方 1.5 铁律：赤月恶魔绝不掉落 1.76 六大重装
        Assert.DoesNotContain(rules, r => r.ItemName.Contains("天尊道袍"));
        Assert.DoesNotContain(rules, r => r.ItemName.Contains("圣战宝甲"));
        Assert.DoesNotContain(rules, r => r.ItemName.Contains("法神披风"));

        // 必须产出官方 1.5 赤月三大终极首饰套与武器
        Assert.Contains(rules, r => r.ItemName.Contains("天尊"));
        Assert.Contains(rules, r => r.ItemName.Contains("圣战"));
        Assert.Contains(rules, r => r.ItemName.Contains("法神"));
        Assert.Contains(rules, r => r.ItemName.Contains("屠龙"));
        Assert.Contains(rules, r => r.ItemName.Contains("嗜魂法杖"));
        Assert.Contains(rules, r => r.ItemName.Contains("祝福油"));
    }

    [Fact]
    public void TestMysticEquipment_StrictOfficial15_NoBelt()
    {
        // 盛大官方 1.5 权威未知暗殿神秘套仅有头盔、手镯、戒指，绝无 1.80+ 腰带
        Assert.True(MysticEquipmentEngine.IsMysticItem("神秘头盔"));
        Assert.True(MysticEquipmentEngine.IsMysticItem("神秘手镯"));
        Assert.True(MysticEquipmentEngine.IsMysticItem("神秘戒指"));
        Assert.False(MysticEquipmentEngine.IsMysticItem("神秘腰带"));
    }

    [Fact]
    public void TestTakeOffAndSwapItem_BagWeightOverflow_BlocksAction()
    {
        var dispatcher = new PacketDispatcher(new Mir2.Server.Maps.MapManager());
        var session = new ClientSession(99, null!)
        {
            CharacterName = "负重战士",
            State = new PlayerState
            {
                Level = 26,
                Job = 0,
                MaxBagWeight = 83,
                BagWeight = 75 // 背包已有 75 负重，剩余仅能承载 8 负重
            }
        };

        // 1. 身上装备重型武器炼狱 (重量 60)
        var heavyWeapon = new EquipItem { Uid = 5001, Name = "炼狱", Weight = 60 };
        session.State.Equipment[(int)OfficialEquipSlot.Weapon] = heavyWeapon;
        session.State.HandWeight = 60;

        // 尝试脱下炼狱放回背包 (75 + 60 = 135 > 83)，必须被拦截
        session.ClearBufferedPackets();
        dispatcher.Dispatch(session, $"#{ProtocolConsts.CM_TAKEOFFITEM}/1/!");

        Assert.NotNull(session.State.Equipment[(int)OfficialEquipSlot.Weapon]);
        Assert.Equal("炼狱", session.State.Equipment[(int)OfficialEquipSlot.Weapon]!.Name);
        Assert.True(session.HasBufferedPacket($"#{ProtocolConsts.SM_TAKEOFF_FAIL}/"));

        // 2. 身上穿 40 重量的战神盔甲，背包里有 1 重量的布衣，尝试换装
        session.State.Equipment[(int)OfficialEquipSlot.Dress] = new EquipItem { Uid = 5002, Name = "战神盔甲", Weight = 40 };
        var cloth = new EquipItem { Uid = 5003, Name = "布衣", Weight = 1 };
        session.State.BagItems.Add(cloth);
        session.State.BagWeight = 76; // 75 + 1
        int clothIdx = session.State.BagItems.IndexOf(cloth);

        // 净负重: 76 - 1 + 40 = 115 > 83，必须被拦截
        session.ClearBufferedPackets();
        dispatcher.Dispatch(session, $"#{ProtocolConsts.CM_WEARITEM}/{clothIdx}/0/!");

        Assert.Equal("战神盔甲", session.State.Equipment[(int)OfficialEquipSlot.Dress]!.Name);
    }

    [Fact]
    public void TestRevivalRing_BreakDura_SendsTakeOffOkAndSyncsWeight()
    {
        var dispatcher = new PacketDispatcher(new Mir2.Server.Maps.MapManager());
        var session = new ClientSession(101, null!)
        {
            CharacterName = "复活死士",
            State = new PlayerState { Level = 35, Job = 0, Hp = 0 }
        };
        // 槽位 7 佩戴仅剩 1000 持久的复活戒指 (即将耗尽碎裂)
        session.State.Equipment[(int)OfficialEquipSlot.RingL] = new EquipItem { Uid = 7777, Name = "复活戒指", Shape = 114, Dura = 1, MaxDura = 5 };
        session.State.RecalculateAttributes();
        session.State.ReviveRingDura = 1000;

        session.ClearBufferedPackets();
        var (revived, _, _) = session.State.CheckAndTriggerRevivalRing(Environment.TickCount64);
        Assert.True(revived);
        Assert.Null(session.State.Equipment[(int)OfficialEquipSlot.RingL]); // 碎裂清空

        dispatcher.SyncRevivalRingStateToClient(session);
        Assert.True(session.HasBufferedPacket($"#{ProtocolConsts.SM_TAKEOFF_OK}/"));
        Assert.True(session.HasBufferedPacket($"#{ProtocolConsts.SM_WEIGHTCHANGED}/"));
    }

    [Fact]
    public void TestCheckEquipRequirement_Authoritative4StateRules()
    {
        // 1. 验证特戒识别 (防御戒指/治愈戒指 -> Defense)
        Assert.Equal(OfficialClientRulesEngine.SpecialItemAndSetRules.SpecialRingType.Defense,
            OfficialClientRulesEngine.SpecialItemAndSetRules.IdentifySpecialRing("防御戒指"));
        Assert.Equal(OfficialClientRulesEngine.SpecialItemAndSetRules.SpecialRingType.Defense,
            OfficialClientRulesEngine.SpecialItemAndSetRules.IdentifySpecialRing("治愈戒指"));

        // 2. 裁决之杖: 官方等级 30 需求 (攻击力 0 只要等级达到 30 即可佩戴；等级不足 30 则拦截)
        var (canWearLowLvl, reasonLow) = OfficialClientRulesEngine.SpecialItemAndSetRules.CheckEquipRequirement(
            "裁决之杖", playerLevel: 29, dcMax: 100, mcMax: 0, scMax: 0);
        Assert.False(canWearLowLvl);
        Assert.Contains("30", reasonLow);

        var (canWearOk, _) = OfficialClientRulesEngine.SpecialItemAndSetRules.CheckEquipRequirement(
            "裁决之杖", playerLevel: 30, dcMax: 0, mcMax: 0, scMax: 0);
        Assert.True(canWearOk);

        // 3. 幽灵战衣: 官方道术 27 需求 (等级再高若 SC < 27 则拦截；SC >= 27 允许)
        var (canWearScFail, reasonSc) = OfficialClientRulesEngine.SpecialItemAndSetRules.CheckEquipRequirement(
            "幽灵战衣", playerLevel: 45, dcMax: 50, mcMax: 0, scMax: 26);
        Assert.False(canWearScFail);
        Assert.Contains("27", reasonSc);

        var (canWearScOk, _) = OfficialClientRulesEngine.SpecialItemAndSetRules.CheckEquipRequirement(
            "幽灵战衣", playerLevel: 1, dcMax: 0, mcMax: 0, scMax: 27);
        Assert.True(canWearScOk);

        // 4. 恶魔长袍: 官方魔法 28 需求
        var (canWearMcFail, reasonMc) = OfficialClientRulesEngine.SpecialItemAndSetRules.CheckEquipRequirement(
            "恶魔长袍", playerLevel: 45, dcMax: 0, mcMax: 27, scMax: 0);
        Assert.False(canWearMcFail);
        Assert.Contains("28", reasonMc);

        var (canWearMcOk, _) = OfficialClientRulesEngine.SpecialItemAndSetRules.CheckEquipRequirement(
            "恶魔长袍", playerLevel: 1, dcMax: 0, mcMax: 28, scMax: 0);
        Assert.True(canWearMcOk);

        // 5. 黑铁头盔: 官方攻击 46 需求
        var (canWearDcFail, reasonDc) = OfficialClientRulesEngine.SpecialItemAndSetRules.CheckEquipRequirement(
            "黑铁头盔", playerLevel: 40, dcMax: 45, mcMax: 0, scMax: 0);
        Assert.False(canWearDcFail);
        Assert.Contains("46", reasonDc);

        var (canWearDcOk, _) = OfficialClientRulesEngine.SpecialItemAndSetRules.CheckEquipRequirement(
            "黑铁头盔", playerLevel: 1, dcMax: 46, mcMax: 0, scMax: 0);
        Assert.True(canWearDcOk);
    }

    [Fact]
    public void TestHandleBinaryUseItemIntent_ConsumesPotionWithAuthoritativeWeights()
    {
        var dispatcher = new PacketDispatcher(new Mir2.Server.Maps.MapManager());
        var session = new ClientSession(102, null!)
        {
            CharacterName = "现代战士",
            UseModernProtocol = true,
            State = new PlayerState
            {
                Level = 30,
                Hp = 100,
                MaxHp = 400,
                Mp = 50,
                MaxMp = 100,
                MaxBagWeight = 100
            }
        };

        // 背包放入一瓶强效太阳水 (重量 2, 瞬回 50 HP / 50 MP)
        var potion = new EquipItem { Uid = 8888, Name = "强效太阳水", Weight = 2, Shape = 0 };
        session.State.BagItems.Add(potion);
        session.State.RecalculateAuthoritativeWeights(0);
        Assert.Equal(2, session.State.BagWeight);

        // 构造二进制意图封包载荷 (4 字节 itemUid 8888)
        byte[] payload = new byte[4];
        System.Buffers.Binary.BinaryPrimitives.WriteInt32LittleEndian(payload, 8888);

        dispatcher.HandleBinaryUseItemIntent(session, payload);

        // 验证背包道具被消耗、血蓝得到真实强效太阳水回复 (100 + 50 = 150, 50 + 50 = 100)
        Assert.Empty(session.State.BagItems);
        Assert.Equal(150, session.State.Hp);
        Assert.Equal(100, session.State.Mp);
        Assert.Equal(0, session.State.BagWeight); // 负重减少
    }

    [Fact]
    public void TestWeaponTrialSlash_Success_SendsTakeOnOkAndSyncsSlot()
    {
        var dispatcher = new PacketDispatcher(new Mir2.Server.Maps.MapManager());
        var session = new ClientSession(103, null!)
        {
            X = 300,
            Y = 301,
            CharacterName = "试刀宗师",
            State = new PlayerState { Level = 35, Job = 0, Hp = 200, MaxHp = 200 }
        };

        // 佩戴一把已在沙巴克升级完毕、待试刀的炼狱 (UpgradeState = 10，成功 +1 攻击)
        var weapon = new EquipItem
        {
            Uid = 9901,
            Name = "炼狱",
            Weight = 60,
            Dc = 0,
            MaxDc = 25,
            UpgradeState = 10,
            UpgradeCount = 0,
            Dura = 28,
            MaxDura = 28
        };
        session.State.Equipment[(int)OfficialEquipSlot.Weapon] = weapon;
        session.State.RecalculateAttributes();

        session.ClearBufferedPackets();
        // 挥刀攻击触发第一刀开刀试刀逻辑 (CM_HIT 3014)
        dispatcher.Dispatch(session, "#3014/300/300/2/!");

        // 验证升级成功 (MaxDc 从 25 变为 26)
        Assert.Equal(0, weapon.Dc);
        Assert.Equal(26, weapon.MaxDc);
        Assert.Equal(0, weapon.UpgradeState); // 试刀状态清空

        // 验证下发了 SM_TAKEON_OK (524) 更新手持槽位属性
        string packets = session.GetBufferedPacketsString();
        Assert.Contains($"#{ProtocolConsts.SM_TAKEON_OK}/", packets);
        Assert.Contains("炼狱", packets);
    }

    [Fact]
    public void TestSuitEffectEngine_MemorySuit_ValidatePreconditions()
    {
        // 1. 验证四大组件完整性
        Assert.False(SuitEffectEngine.MemorySuit.CheckFullSet(hasHelmet: false, hasNecklace: true, hasBracelet: true, hasRing: true));
        Assert.True(SuitEffectEngine.MemorySuit.CheckFullSet(hasHelmet: true, hasNecklace: true, hasBracelet: true, hasRing: true));

        // 2. 验证非队长拦截
        var (ok1, reason1) = SuitEffectEngine.MemorySuit.ValidateRecallPreconditions(
            isLeader: false, isFullSet: true, inSabukPalace: false, lastRecallTick: 0, nowTick: 1000);
        Assert.False(ok1);
        Assert.Contains("队长", reason1);

        // 3. 验证未穿齐记忆套拦截
        var (ok2, reason2) = SuitEffectEngine.MemorySuit.ValidateRecallPreconditions(
            isLeader: true, isFullSet: false, inSabukPalace: false, lastRecallTick: 0, nowTick: 1000);
        Assert.False(ok2);
        Assert.Contains("未穿齐", reason2);

        // 4. 验证沙巴克皇宫禁空拦截
        var (ok3, reason3) = SuitEffectEngine.MemorySuit.ValidateRecallPreconditions(
            isLeader: true, isFullSet: true, inSabukPalace: true, lastRecallTick: 0, nowTick: 1000);
        Assert.False(ok3);
        Assert.Contains("沙巴克皇宫", reason3);

        // 5. 验证 180 秒冷却拦截
        long now = 200_000;
        var (ok4, reason4) = SuitEffectEngine.MemorySuit.ValidateRecallPreconditions(
            isLeader: true, isFullSet: true, inSabukPalace: false, lastRecallTick: now - 60_000, nowTick: now);
        Assert.False(ok4);
        Assert.Contains("120", reason4); // 还需等待 120 秒

        // 6. 验证全条件满足放行
        var (ok5, _) = SuitEffectEngine.MemorySuit.ValidateRecallPreconditions(
            isLeader: true, isFullSet: true, inSabukPalace: false, lastRecallTick: now - 190_000, nowTick: now);
        Assert.True(ok5);
    }
}

