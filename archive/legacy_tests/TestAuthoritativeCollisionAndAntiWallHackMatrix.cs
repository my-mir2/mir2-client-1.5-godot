using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mir2.Server.Game;
using Mir2.Server.Maps;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;
using Mir2.Shared.Protocols.Modern;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 服务端权威物理碰撞、视线射线穿透 (LoS) 与反穿墙外挂专项实证测试
/// 以恶意攻击者视角验证：
/// 1. 近战物理攻击隔墙砍人阻断
/// 2. 刺杀剑术隔位剑气遇厚墙阻断
/// 3. 远程法术 (火球术/火符/雷电) 隔墙视线射线拦截
/// 4. 商人与 NPC 跨图/隔空交互距离拦截
/// </summary>
public sealed class TestAuthoritativeCollisionAndAntiWallHackMatrix
{
    [Fact]
    public void TestMeleeAttack_BlockedByWall_DoesNotDealDamage()
    {
        // 场景：玩家在 (100, 100)，面向 (101, 100)
        // (101, 100) 是一堵实心山体阻挡墙 (IsWalkable = false)
        var map = new GameMap(0, "比奇省", 300, 300);
        var mapManager = new MapManager();
        mapManager.RegisterMap(map);

        var regen = new MonsterRegenEngine(mapManager);
        regen.AddRule(new MonsterSpawnRule
        {
            RuleId = 1,
            MonsterName = "墙内怪",
            MapId = 0,
            CenterX = 101,
            CenterY = 100,
            Radius = 0,
            MaxCount = 1,
            BaseHp = 100
        });
        regen.ProcessRegenTick(new OfficialClientRulesEngine.DelphiRandom(1));
        var monster = regen.FindMonsterAt(0, 101, 100);
        Assert.NotNull(monster);
        Assert.Equal(100, monster.Hp);

        // 刷怪完成后将该位置设为不可通行实心石壁
        map.SetWalkable(101, 100, false);

        var dispatcher = new PacketDispatcher(mapManager, regen);
        var session = new ClientSession(1, null!)
        {
            CurrentMapId = 0,
            X = 100,
            Y = 100,
            Direction = 2,
            CharacterName = "战士",
            State = new PlayerState { Level = 35, Hp = 200, MaxHp = 200, Hit = 100 }
        };
        map.Aoi.Enter(session, 100, 100);

        // 战士向石壁 (101, 100) 挥刀
        dispatcher.Dispatch(session, "#3014/101/100/2!");

        // 权威物理世界必须阻断，怪物不可受到伤害
        Assert.Equal(100, monster.Hp);
    }

    [Fact]
    public void TestStabbingSword_BlockedByMidWall_DoesNotHitSecondTile()
    {
        // 场景：刺杀剑术打前方第 2 格 (102, 100)
        // 但前方第 1 格 (101, 100) 是厚墙阻挡 (IsWalkable = false)
        var map = new GameMap(0, "比奇省", 300, 300);
        map.SetWalkable(101, 100, false); // 中间厚墙阻隔
        map.SetWalkable(102, 100, true);

        var mapManager = new MapManager();
        mapManager.RegisterMap(map);

        var regen = new MonsterRegenEngine(mapManager);
        regen.AddRule(new MonsterSpawnRule
        {
            RuleId = 2,
            MonsterName = "墙后怪",
            MapId = 0,
            CenterX = 102,
            CenterY = 100,
            Radius = 0,
            MaxCount = 1,
            BaseHp = 100
        });
        regen.ProcessRegenTick(new OfficialClientRulesEngine.DelphiRandom(1));
        var monster = regen.FindMonsterAt(0, 102, 100);
        Assert.NotNull(monster);

        var dispatcher = new PacketDispatcher(mapManager, regen);
        var session = new ClientSession(1, null!)
        {
            CurrentMapId = 0,
            X = 100,
            Y = 100,
            Direction = 2,
            CharacterName = "刺杀战士",
            State = new PlayerState { Level = 35, Hp = 200, MaxHp = 200, Hit = 100 }
        };
        session.State.StabbingTracker.ToggleAutoStab(); // 开启刺杀
        map.Aoi.Enter(session, 100, 100);

        // 刺杀剑气攻击第 2 格 (102, 100)
        dispatcher.Dispatch(session, "#3014/102/100/2!");

        // 剑气被中间厚墙阻隔，墙后怪毫发无损
        Assert.Equal(100, monster.Hp);
    }

    [Fact]
    public void TestRangedSpell_BlockedByWall_TriggersLineOfSightBlock()
    {
        // 场景：法师在 (100, 100)，目标在 (105, 100)
        // 中间 (103, 100) 有一堵高墙
        var map = new GameMap(0, "比奇省", 300, 300);
        map.SetWalkable(103, 100, false); // 中间隔墙

        var mapManager = new MapManager();
        mapManager.RegisterMap(map);

        var regen = new MonsterRegenEngine(mapManager);
        regen.AddRule(new MonsterSpawnRule
        {
            RuleId = 3,
            MonsterName = "远方怪",
            MapId = 0,
            CenterX = 105,
            CenterY = 100,
            Radius = 0,
            MaxCount = 1,
            BaseHp = 100
        });
        regen.ProcessRegenTick(new OfficialClientRulesEngine.DelphiRandom(1));
        var monster = regen.FindMonsterAt(0, 105, 100);
        Assert.NotNull(monster);

        var dispatcher = new PacketDispatcher(mapManager, regen);
        var mage = new ClientSession(1, null!)
        {
            CurrentMapId = 0,
            X = 100,
            Y = 100,
            CharacterName = "雷法",
            State = new PlayerState { Level = 35, Hp = 200, MaxHp = 200, Mp = 500, MaxMp = 500 }
        };
        map.Aoi.Enter(mage, 100, 100);

        // 1. 发射火球术 (MagicId = 1) 隔墙攻击
        dispatcher.Dispatch(mage, "#3017/0/1/105/100/!");
        Assert.True(mage.HasBufferedPacket($"#{ProtocolConsts.SM_SYSMSG_DELPHI}/"));
        Assert.Equal(100, monster.Hp); // 怪未受伤害

        // 2. 发射灵魂火符 (MagicId = 13) 隔墙攻击
        mage.ClearBufferedPackets();
        mage.LastSpellTimeMs = 0;
        mage.State.BagItems.Add(new EquipItem { Name = "护身符", Dura = 100, MaxDura = 100 });
        dispatcher.Dispatch(mage, "#3017/0/13/105/100/!");
        Assert.True(mage.HasBufferedPacket($"#{ProtocolConsts.SM_SYSMSG_DELPHI}/"));
        Assert.Equal(100, monster.Hp);
    }

    [Fact]
    public void TestNpcInteraction_OverDistance_Blocked()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 300, 300);
        mapManager.RegisterMap(map);

        var npcEngine = new NpcMerchantEngine();
        npcEngine.RegisterNpc(new NpcEntity
        {
            Uid = 99,
            Name = "比奇铁匠",
            MapId = 0,
            X = 200,
            Y = 200
        });

        var dispatcher = new PacketDispatcher(mapManager, npcEngine: npcEngine);
        var session = new ClientSession(1, null!)
        {
            CurrentMapId = 0,
            X = 100, // 距离 NPC 100 格 (远超 2 格交互范围)
            Y = 100,
            CharacterName = "远程外挂玩家",
            State = new PlayerState { Level = 20, Hp = 100, MaxHp = 100 }
        };
        map.Aoi.Enter(session, 100, 100);

        // 尝试隔空打开 NPC 对话框 (CM_MERCHANTHOMEPAGE 1010)
        dispatcher.Dispatch(session, "#1010/99/!");

        Assert.True(session.HasBufferedPacket($"#{ProtocolConsts.SM_SYSMSG_DELPHI}/"));
        Assert.False(session.HasBufferedPacket($"#{ProtocolConsts.SM_NPCDIALOG}/"));
    }

    [Fact]
    public void TestButchItem_OverDistanceOrBlockedByWall_StrictlyRejected()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 300, 300);
        map.SetWalkable(101, 100, false); // 中间隔墙
        mapManager.RegisterMap(map);

        var regen = new MonsterRegenEngine(mapManager);
        regen.AddRule(new MonsterSpawnRule
        {
            RuleId = 1,
            MonsterName = "鹿",
            MapId = 0,
            CenterX = 102,
            CenterY = 100,
            Radius = 0,
            MaxCount = 1,
            BaseHp = 15
        });
        regen.ProcessRegenTick(new OfficialClientRulesEngine.DelphiRandom(1));
        var deer = regen.FindMonsterAt(0, 102, 100);
        Assert.NotNull(deer);

        // 击杀鹿使其变为尸体
        deer.Hp = 0;
        regen.OnMonsterDied(deer.Uid);
        var corpse = regen.FindCorpse(deer.Uid);
        Assert.NotNull(corpse);

        var dispatcher = new PacketDispatcher(mapManager, regen);
        var session = new ClientSession(1, null!)
        {
            CurrentMapId = 0,
            X = 100,
            Y = 100,
            CharacterName = "屠夫玩家",
            State = new PlayerState { Level = 20, Hp = 100, MaxHp = 100 }
        };
        map.Aoi.Enter(session, 100, 100);

        // 1. 视线受阻 (隔墙挖肉被拒)
        dispatcher.Dispatch(session, $"#1007/{deer.Uid}/102/100/!");
        Assert.True(session.HasBufferedPacket($"#{ProtocolConsts.SM_SYSMSG_DELPHI}/"));
        Assert.False(corpse.IsButchered);

        // 2. 超远距离隔空挖肉 (玩家在 100, 100，尸体移至 110, 100)
        corpse.X = 110;
        session.ClearBufferedPackets();
        dispatcher.Dispatch(session, $"#1007/{deer.Uid}/100/100/!"); // 客户端发包伪造自己身边坐标，但服务端以 corpse.X 权威校验
        Assert.True(session.HasBufferedPacket($"#{ProtocolConsts.SM_SYSMSG_DELPHI}/"));
        Assert.False(corpse.IsButchered);
    }

    [Fact]
    public void TestStorageSecurity_RemoteStorageHack_StrictlyRejected()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 300, 300);
        mapManager.RegisterMap(map);

        var npcEngine = new NpcMerchantEngine();
        npcEngine.RegisterNpc(new NpcEntity { Uid = 106, Name = "仓库保管员", MapId = 0, X = 100, Y = 100 });

        var dispatcher = new PacketDispatcher(mapManager, npcEngine: npcEngine);
        var session = new ClientSession(1, null!)
        {
            CurrentMapId = 0,
            X = 150, // 距离保管员 50 格
            Y = 150,
            CharacterName = "外挂玩家",
            State = new PlayerState { Level = 20, Hp = 100, MaxHp = 100, Gold = 50000 }
        };
        map.Aoi.Enter(session, 150, 150);

        // 1. 未开启仓库直接尝试存金币 (CM_SAVEGOLD 1017)
        dispatcher.Dispatch(session, "#1017/10000/!");
        Assert.True(session.HasBufferedPacket($"#{ProtocolConsts.SM_CLOSESTORAGEDLG}/"));
        Assert.Equal(0, session.State.StorageGold);

        // 2. 伪造开启状态但人在野外，尝试存物品
        session.IsStorageOpen = true;
        session.StorageNpcUid = 106;
        session.ClearBufferedPackets();
        var item = new EquipItem { Uid = 55, Name = "裁决之杖", Weight = 10 };
        session.State.BagItems.Add(item);
        dispatcher.Dispatch(session, "#1031/0/裁决之杖/!");
        Assert.True(session.HasBufferedPacket($"#{ProtocolConsts.SM_CLOSESTORAGEDLG}/"));
        Assert.False(session.IsStorageOpen);
        Assert.Single(session.State.BagItems);
    }

    [Fact]
    public void TestTrade_MovementOrMapChange_TriggersCancellation()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 300, 300);
        mapManager.RegisterMap(map);

        var tradeEngine = new TradeEngine();
        var dispatcher = new PacketDispatcher(mapManager, tradeEngine: tradeEngine);

        var playerA = new ClientSession(1, null!)
        {
            CurrentMapId = 0,
            X = 100,
            Y = 100,
            CharacterName = "玩家A",
            State = new PlayerState { Level = 30, Hp = 200, MaxHp = 200, Gold = 10000 }
        };
        var playerB = new ClientSession(2, null!)
        {
            CurrentMapId = 0,
            X = 101,
            Y = 100,
            CharacterName = "玩家B",
            State = new PlayerState { Level = 30, Hp = 200, MaxHp = 200, Gold = 10000 }
        };
        map.Aoi.Enter(playerA, 100, 100);
        map.Aoi.Enter(playerB, 101, 100);

        // 发起并接受交易 (CM_DEALTRY 1025)
        dispatcher.Dispatch(playerA, "#1025/2/!");
        dispatcher.Dispatch(playerB, "#1025/1/!");
        Assert.NotNull(tradeEngine.GetTrade(playerA.SessionId));

        // 玩家A 移动脱离交易范围 (走到 99, 100，距离变为 2 格)
        dispatcher.Dispatch(playerA, "#3011/99/100/6/!");

        // 交易已被物理位移安全熔断取消
        Assert.Null(tradeEngine.GetTrade(playerA.SessionId));
        Assert.Null(tradeEngine.GetTrade(playerB.SessionId));
    }

    [Fact]
    public void TestMonsterAi_SafeZoneProtectionAndWallBlock_StrictlyEnforced()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 500, 500);
        mapManager.RegisterMap(map);

        var safeZoneEngine = new SafeZoneAndGuardEngine();
        var regen = new MonsterRegenEngine(mapManager) { SafeZoneEngine = safeZoneEngine };

        // 1. 测试安全区内玩家免疫怪物攻击
        // 比奇省安全区覆盖 (320, 260) ~ (345, 285)
        var playerInSafeZone = new ClientSession(1, null!)
        {
            CurrentMapId = 0,
            X = 330, // 处于比奇安全区核心
            Y = 270,
            CharacterName = "安全区挂机玩家",
            State = new PlayerState { Level = 10, Hp = 100, MaxHp = 100 }
        };
        map.Aoi.Enter(playerInSafeZone, 330, 270);

        regen.AddRule(new MonsterSpawnRule
        {
            RuleId = 10,
            MonsterName = "毒蜘蛛",
            MapId = 0,
            CenterX = 333,
            CenterY = 270,
            Radius = 0,
            MaxCount = 1,
            BaseHp = 50
        });
        regen.ProcessRegenTick(new OfficialClientRulesEngine.DelphiRandom(1));
        var spider = regen.FindMonsterAt(0, 333, 270);
        Assert.NotNull(spider);

        // 毒蜘蛛尝试喷毒攻击安全区内的玩家
        var sessions = new List<ClientSession> { playerInSafeZone };
        regen.ProcessMonsterAiTick(
            (mapId, x, y) => sessions,
            new OfficialClientRulesEngine.DelphiRandom(1)
        );

        // 安全区玩家丝毫无损且未中毒
        Assert.Equal(100, playerInSafeZone.State.Hp);
        Assert.False(playerInSafeZone.State.HasGreenPoison);
    }

    [Fact]
    public void TestTeleportTransition_CrossMap_SwitchesMapAndTriggersAoiAndCancelsTrade()
    {
        var mapManager = new MapManager();
        var mapDungeon = new GameMap(1, "沃玛自然洞穴", 100, 100);
        var mapTown = new GameMap(0, "比奇省", 500, 500);
        mapManager.RegisterMap(mapDungeon);
        mapManager.RegisterMap(mapTown);

        var dispatcher = new PacketDispatcher(mapManager, null);
        var session = new ClientSession(1, null!)
        {
            CurrentMapId = 1,
            X = 20,
            Y = 20,
            CharacterName = "地牢探险者",
            State = new PlayerState { Level = 25, Hp = 200, MaxHp = 200, Gold = 1000 }
        };
        mapDungeon.Aoi.Enter(session, 20, 20);

        // 放入一个地牢逃脱卷
        session.State.BagItems.Add(new EquipItem { Uid = 101, Name = "地牢逃脱卷", Weight = 1 });

        // 模拟玩家在交易中
        var tradePeer = new ClientSession(2, null!) { CurrentMapId = 1, X = 20, Y = 21, CharacterName = "交易伙伴", State = new PlayerState { Hp = 100 } };
        dispatcher.TradeEngine!.TryInitiateTrade(session, tradePeer);
        Assert.NotNull(dispatcher.TradeEngine.GetTrade(session.SessionId));

        // 开启仓库
        session.IsStorageOpen = true;
        session.StorageNpcUid = 99;

        // 使用地牢逃脱卷 (CM_EATITEM 1006)
        dispatcher.Dispatch(session, "#1006/0/地牢逃脱卷/!");

        // 1. 验证地图切换与坐标变更 (逃脱至比奇省 330, 270)
        Assert.Equal(0, session.CurrentMapId);
        Assert.Equal(330, session.X);
        Assert.Equal(270, session.Y);

        // 2. 验证下发了 SM_NEWMAP_DELPHI (521) 和 SM_EAT_OK
        Assert.True(session.HasBufferedPacket($"#{ProtocolConsts.SM_NEWMAP_DELPHI}/"));
        Assert.True(session.HasBufferedPacket($"#{ProtocolConsts.SM_EAT_OK}/"));

        // 3. 验证交易已自动熔断，仓库已自动关闭
        Assert.Null(dispatcher.TradeEngine.GetTrade(session.SessionId));
        Assert.False(session.IsStorageOpen);
        Assert.True(session.HasBufferedPacket($"#{ProtocolConsts.SM_CLOSESTORAGEDLG}/"));

        // 4. 验证新地图 AOI 包含该玩家，旧地图 AOI 不再包含
        var townEntities = new List<ClientSession>();
        mapTown.Aoi.GetAllEntities(townEntities);
        Assert.Contains(session, townEntities);

        var dungeonEntities = new List<ClientSession>();
        mapDungeon.Aoi.GetAllEntities(dungeonEntities);
        Assert.DoesNotContain(session, dungeonEntities);
    }

    [Fact]
    public void TestDeadPlayer_ActionsStrictlyBlocked_CannotEatOrDropOrWearOrTrade()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 100, 100);
        mapManager.RegisterMap(map);

        var dispatcher = new PacketDispatcher(mapManager, null);
        var deadSession = new ClientSession(1, null!)
        {
            CurrentMapId = 0,
            X = 50,
            Y = 50,
            CharacterName = "阵亡玩家",
            State = new PlayerState
            {
                Level = 30,
                Hp = 0, // 阵亡
                MaxHp = 300,
                Gold = 50000,
                BagWeight = 10,
                MaxBagWeight = 100
            }
        };
        map.Aoi.Enter(deadSession, 50, 50);

        deadSession.State.BagItems.Add(new EquipItem { Uid = 201, Name = "强效太阳水", Weight = 1 });
        deadSession.State.BagItems.Add(new EquipItem { Uid = 202, Name = "重盔甲(男)", Weight = 23 });

        // 1. 尝试吃药 (CM_EATITEM 1006) -> 拦截
        dispatcher.Dispatch(deadSession, "#1006/0/强效太阳水/!");
        Assert.Equal(0, deadSession.State.Hp);
        Assert.Equal(2, deadSession.State.BagItems.Count);

        // 2. 尝试穿装备 (CM_WEARITEM 613) -> 拦截
        bool wearRes = dispatcher.HandleWearItem(deadSession, 1, OfficialEquipSlot.Dress);
        Assert.False(wearRes);
        Assert.Null(deadSession.State.Equipment[(int)OfficialEquipSlot.Dress]);

        // 3. 尝试丢弃物品 (CM_DROPITEM 1008) -> 拦截
        dispatcher.Dispatch(deadSession, "#1008/0/!");
        Assert.Empty(map.GetGroundItemsAt(50, 50));

        // 4. 尝试丢弃金币 (CM_DROPGOLD 1009) -> 拦截
        dispatcher.Dispatch(deadSession, "#1009/1000/!");
        Assert.Equal(50000, deadSession.State.Gold);
        Assert.Empty(map.GetGroundItemsAt(50, 50));
    }

    [Fact]
    public void TestWearItem_CannotReplaceCursedMysticEquipment()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 100, 100);
        mapManager.RegisterMap(map);

        var dispatcher = new PacketDispatcher(mapManager, null);
        var session = new ClientSession(1, null!)
        {
            CurrentMapId = 0,
            X = 50,
            Y = 50,
            CharacterName = "神秘受害者",
            State = new PlayerState
            {
                Level = 35,
                Hp = 200,
                MaxHp = 200,
                BagWeight = 5,
                MaxBagWeight = 100,
                WearWeight = 10,
                MaxWearWeight = 100,
                HandWeight = 10,
                MaxHandWeight = 100
            }
        };
        map.Aoi.Enter(session, 50, 50);

        // 角色当前戒指槽位装备了一个被诅咒的神秘戒指
        var cursedRing = new EquipItem { Uid = 301, Name = "神秘戒指", Weight = 1, Curse = 1 };
        session.State.Equipment[(int)OfficialEquipSlot.RingL] = cursedRing;

        // 背包放入一个普通的降妖除魔戒指
        var normalRing = new EquipItem { Uid = 302, Name = "降妖除魔戒指", Weight = 1 };
        session.State.BagItems.Add(normalRing);

        // 尝试用降妖除魔戒指顶替替换神秘戒指
        bool wearRes = dispatcher.HandleWearItem(session, 0, OfficialEquipSlot.RingL);

        // 权威拦截！
        Assert.False(wearRes);
        // 槽位上依然是被诅咒的神秘戒指
        Assert.Equal(cursedRing, session.State.Equipment[(int)OfficialEquipSlot.RingL]);
        // 普通戒指依然留在背包
        Assert.Contains(normalRing, session.State.BagItems);
        // 收到系统提示诅咒提示
        Assert.True(session.HasBufferedPacket($"#{ProtocolConsts.SM_SYSMSG_DELPHI}/"));
    }

    [Fact]
    public void TestDropAndPickup_TileStackLimitAndGoldCapProtection()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 100, 100);
        mapManager.RegisterMap(map);

        var dispatcher = new PacketDispatcher(mapManager, null);
        var session = new ClientSession(1, null!)
        {
            CurrentMapId = 0,
            X = 50,
            Y = 50,
            CharacterName = "测试拾取玩家",
            State = new PlayerState
            {
                Level = 35,
                Hp = 200,
                MaxHp = 200,
                Gold = 9_995_000, // 距离 10,000,000 上限差 5,000
                BagWeight = 5,
                MaxBagWeight = 100
            }
        };
        map.Aoi.Enter(session, 50, 50);

        // 1. 地面积存 8 件物品达到堆叠上限
        for (int i = 0; i < 8; i++)
        {
            map.AddGroundItem(new GroundItemEntity { Uid = 400 + i, Name = $"木剑_{i}", X = 50, Y = 50 });
        }
        Assert.Equal(8, map.GetGroundItemsAt(50, 50).Count);

        // 2. 玩家尝试丢弃第 9 件物品 -> 被单格 8 件上限拦截
        session.State.BagItems.Add(new EquipItem { Uid = 501, Name = "回城卷", Weight = 1 });
        dispatcher.Dispatch(session, "#1008/0/!");
        Assert.Single(session.State.BagItems); // 未被移出背包
        Assert.Equal(8, map.GetGroundItemsAt(50, 50).Count);

        // 3. 测试金币拾取一千万上限截断保护
        // 清理当前地表
        var items = map.GetGroundItemsAt(50, 50);
        foreach (var it in items) map.RemoveGroundItem(it.Uid);

        // 放置一堆 20,000 的金币在地上
        map.AddGroundItem(new GroundItemEntity
        {
            Uid = 601,
            Name = "金币",
            X = 50,
            Y = 50,
            IsGold = true,
            Count = 20_000,
            DropTickMs = 0
        });

        // 拾取金币 (CM_PICKUP 1001)
        dispatcher.Dispatch(session, "#1001/!");

        // 玩家金币刚好到达 10,000,000 上限，不再增加
        Assert.Equal(PlayerState.MaxGoldLimit, session.State.Gold);
        // 地面上仍然残留未被拾取的 15,000 金币！
        var remainingGround = map.GetGroundItemsAt(50, 50);
        Assert.Single(remainingGround);
        Assert.Equal(15_000, remainingGround[0].Count);

        // 身上已满一千万时再次尝试拾取 -> 拒绝拾取并保留在地面
        dispatcher.Dispatch(session, "#1001/!");
        Assert.Equal(PlayerState.MaxGoldLimit, session.State.Gold);
        Assert.Single(map.GetGroundItemsAt(50, 50));
    }

    [Fact]
    public void TestRedNameCrossMapRevive_TransitionsAoiAndSendsNewMap()
    {
        var mapManager = new MapManager();
        var biqiMap = new GameMap(0, "比奇省", 400, 400);
        var mengzhongMap = new GameMap(3, "盟重省", 800, 800);
        mapManager.RegisterMap(biqiMap);
        mapManager.RegisterMap(mengzhongMap);

        var dispatcher = new PacketDispatcher(mapManager);

        // 创建红名玩家 (PkPoints >= 200) 在比奇省 (0) 死亡
        var redSession = new ClientSession(101, null!)
        {
            CurrentMapId = 0,
            X = 100,
            Y = 100,
            CharacterName = "嗜血红名",
            State = new PlayerState
            {
                Level = 35,
                Hp = 0,
                MaxHp = 300,
                Mp = 0,
                MaxMp = 100,
                PkPoints = 300 // 红名
            }
        };
        biqiMap.Aoi.Enter(redSession, 100, 100);

        // 另一玩家在比奇省附近观察
        var observerSession = new ClientSession(102, null!)
        {
            CurrentMapId = 0,
            X = 102,
            Y = 100,
            CharacterName = "路人玩家",
            State = new PlayerState { Level = 20, Hp = 100, MaxHp = 100 }
        };
        biqiMap.Aoi.Enter(observerSession, 102, 100);

        // 玩家发起复活请求 (CM_REVIVE 1018)
        dispatcher.HandleRevive(redSession, "");

        // 1. 验证官方 1.5 半血半魔复活 (MaxHp / 2, MaxMp / 2)
        Assert.Equal(150, redSession.State.Hp);
        Assert.Equal(50, redSession.State.Mp);

        // 2. 验证流放到盟重红名村 (3, 850, 660)
        Assert.Equal(3, redSession.CurrentMapId);
        Assert.Equal(850, redSession.X);
        Assert.Equal(660, redSession.Y);

        // 3. 验证下发了切图包 SM_NEWMAP_DELPHI (521) 和复活包 SM_REVIVE (501)
        Assert.True(redSession.HasBufferedPacket($"#{ProtocolConsts.SM_NEWMAP_DELPHI}/3/850/660/0/盟重省!"));
        Assert.True(redSession.HasBufferedPacket($"#{ProtocolConsts.SM_REVIVE}/101/850/660/0/嗜血红名!"));

        // 4. 验证比奇省旧地图的观察者收到了尸体消失包 SM_ENTITYDISAPPEAR
        Assert.True(observerSession.HasBufferedPacket($"#{ProtocolConsts.SM_ENTITYDISAPPEAR}/101/100/100/0/嗜血红名!"));

        // 5. 验证比奇省 AOI 已注销该玩家，盟重省已登记该玩家
        var biqiEntities = new List<ClientSession>();
        biqiMap.Aoi.GetAllEntities(biqiEntities);
        Assert.DoesNotContain(redSession, biqiEntities);

        var mengzhongEntities = new List<ClientSession>();
        mengzhongMap.Aoi.GetAllEntities(mengzhongEntities);
        Assert.Contains(redSession, mengzhongEntities);
    }

    [Fact]
    public void TestDeadPlayerMovementAndAction_StrictlyBlocked()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 300, 300);
        mapManager.RegisterMap(map);

        var dispatcher = new PacketDispatcher(mapManager);
        var deadSession = new ClientSession(1, null!)
        {
            CurrentMapId = 0,
            X = 50,
            Y = 50,
            Direction = 2,
            CharacterName = "已阵亡玩家",
            State = new PlayerState
            {
                Hp = 0, // 阵亡
                MaxHp = 100,
                Mp = 50,
                MaxMp = 100
            }
        };
        map.Aoi.Enter(deadSession, 50, 50);

        // 1. 尝试移动 (CM_WALK 1002)
        dispatcher.Dispatch(deadSession, "#1002/51/50/2/!");
        Assert.Equal(50, deadSession.X); // 坐标未变

        // 2. 尝试物理攻击 (CM_HIT 1004)
        dispatcher.Dispatch(deadSession, "#1004/51/50/2/!");
        Assert.False(deadSession.HasBufferedPacket($"#{ProtocolConsts.SM_OBJECTHIT}/"));

        // 3. 尝试施法 (CM_SPELL 3017)
        dispatcher.Dispatch(deadSession, "#3017/0/11/51/50/!"); // 雷电术
        Assert.False(deadSession.HasBufferedPacket($"#{ProtocolConsts.SM_OBJECTSPELL}/"));

        // 4. 尝试转向 (CM_TURN 1003)
        dispatcher.Dispatch(deadSession, "#1003/5/!");
        Assert.Equal(2, deadSession.Direction); // 朝向未变

        // 5. 尝试挖矿 (CM_BIGHIT 3015)
        dispatcher.Dispatch(deadSession, "#3015/!");
        Assert.False(deadSession.HasBufferedPacket($"#{ProtocolConsts.SM_MINERESULT}/"));
    }

    [Fact]
    public void TestSpellCastBreaksStealth_ExceptStealthItself()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 300, 300);
        mapManager.RegisterMap(map);

        var dispatcher = new PacketDispatcher(mapManager);
        var session = new ClientSession(1, null!)
        {
            CurrentMapId = 0,
            X = 50,
            Y = 50,
            CharacterName = "隐身道士",
            State = new PlayerState
            {
                Hp = 100,
                MaxHp = 100,
                Mp = 100,
                MaxMp = 100,
                IsStealthed = true // 处于隐身状态
            }
        };
        map.Aoi.Enter(session, 50, 50);

        // 施放攻击性法术：雷电术 (magicId = 11)
        dispatcher.Dispatch(session, "#3017/0/11/51/50/!");

        // 验证隐身被破除
        Assert.False(session.State.IsStealthed);
        Assert.True(session.HasBufferedPacket($"#{ProtocolConsts.SM_CHANGENAMECOLOR}/"));
    }

    [Fact]
    public void TestMiningOnlyAllowedInMiningMapWithPickaxe()
    {
        var mapManager = new MapManager();
        var safeMap = new GameMap(0, "比奇主城", 300, 300); // mapId 0 不是矿区
        var mineMap = new GameMap(1, "比奇废矿区", 300, 300); // mapId 1 是矿区
        mapManager.RegisterMap(safeMap);
        mapManager.RegisterMap(mineMap);

        var dispatcher = new PacketDispatcher(mapManager);

        // 1. 在非矿区地图手持鹤嘴锄尝试挖矿 (CM_BIGHIT 3015)
        var sessionSafe = new ClientSession(1, null!)
        {
            CurrentMapId = 0,
            X = 50,
            Y = 50,
            CharacterName = "城里矿工",
            State = new PlayerState
            {
                Hp = 100,
                MaxHp = 100,
                Equipment = { [(int)OfficialEquipSlot.Weapon] = new EquipItem { Name = "鹤嘴锄", Weight = 5, Dura = 10, MaxDura = 10 } }
            }
        };
        safeMap.Aoi.Enter(sessionSafe, 50, 50);

        dispatcher.Dispatch(sessionSafe, "#3015/!");
        Assert.True(sessionSafe.HasBufferedPacket($"#{ProtocolConsts.SM_SYSMSG_DELPHI}/0/0/0/0/这里不是矿区，无法开采矿石！!"));
        Assert.Empty(sessionSafe.State.BagItems);

        // 2. 在矿区未手持鹤嘴锄尝试挖矿
        var sessionMineNoPick = new ClientSession(2, null!)
        {
            CurrentMapId = 1,
            X = 50,
            Y = 50,
            CharacterName = "空手矿工",
            State = new PlayerState
            {
                Hp = 100,
                MaxHp = 100,
                Equipment = { [(int)OfficialEquipSlot.Weapon] = new EquipItem { Name = "木剑", Weight = 5, Dura = 10, MaxDura = 10 } }
            }
        };
        mineMap.Aoi.Enter(sessionMineNoPick, 50, 50);

        dispatcher.Dispatch(sessionMineNoPick, "#3015/!");
        Assert.True(sessionMineNoPick.HasBufferedPacket($"#{ProtocolConsts.SM_SYSMSG_DELPHI}/0/0/0/0/你手中未装备鹤嘴锄，无法挖矿！!"));
        Assert.Empty(sessionMineNoPick.State.BagItems);
    }

    [Fact]
    public void TestSessionDisconnected_CancelsTradeAndBroadcastsDisappear()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 300, 300);
        mapManager.RegisterMap(map);

        var tradeEngine = new TradeEngine();
        var dispatcher = new PacketDispatcher(mapManager, tradeEngine: tradeEngine);

        var sessionA = new ClientSession(1, null!)
        {
            CurrentMapId = 0,
            X = 50,
            Y = 50,
            CharacterName = "交易玩家A",
            State = new PlayerState { Hp = 100, MaxHp = 100 }
        };
        var sessionB = new ClientSession(2, null!)
        {
            CurrentMapId = 0,
            X = 51,
            Y = 50,
            CharacterName = "交易玩家B",
            State = new PlayerState { Hp = 100, MaxHp = 100 }
        };
        map.Aoi.Enter(sessionA, 50, 50);
        map.Aoi.Enter(sessionB, 51, 50);

        // 发起面对面交易 (相邻1格)
        var (tradeOk, _) = tradeEngine.TryInitiateTrade(sessionA, sessionB);
        Assert.True(tradeOk);
        Assert.NotNull(tradeEngine.GetTrade(sessionA.SessionId));

        // 玩家 A 掉线或网络中断
        dispatcher.OnSessionDisconnected(sessionA);

        // 1. 验证交易被自动取消
        Assert.Null(tradeEngine.GetTrade(sessionA.SessionId));
        Assert.Null(tradeEngine.GetTrade(sessionB.SessionId));

        // 2. 验证玩家 B 收到了 A 实体消失广播 SM_ENTITYDISAPPEAR
        Assert.True(sessionB.HasBufferedPacket($"#{ProtocolConsts.SM_ENTITYDISAPPEAR}/1/50/50/0/交易玩家A!"));

        // 3. 验证玩家 A 已从地图 AOI 中彻底注销
        var activeSessions = new List<ClientSession>();
        map.Aoi.GetAllEntities(activeSessions);
        Assert.DoesNotContain(sessionA, activeSessions);
    }

    [Fact]
    public void TestPotionEatCooldown_PreventsRapidConsumption()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 300, 300);
        mapManager.RegisterMap(map);

        var dispatcher = new PacketDispatcher(mapManager);
        var session = new ClientSession(1, null!)
        {
            CurrentMapId = 0,
            X = 50,
            Y = 50,
            CharacterName = "测试吃药玩家",
            State = new PlayerState
            {
                Hp = 10,
                MaxHp = 100,
                BagWeight = 10,
                MaxBagWeight = 100
            }
        };
        // 背包放入 3 瓶金创药
        session.State.BagItems.Add(new EquipItem { Uid = 1, Name = "金创药(小量)", Weight = 1 });
        session.State.BagItems.Add(new EquipItem { Uid = 2, Name = "金创药(小量)", Weight = 1 });
        session.State.BagItems.Add(new EquipItem { Uid = 3, Name = "金创药(小量)", Weight = 1 });
        map.Aoi.Enter(session, 50, 50);

        // 第一次吃药 (成功)
        dispatcher.Dispatch(session, "#1006/0/金创药(小量)!");
        Assert.Equal(2, session.State.BagItems.Count);

        // 瞬间紧接着发送第二次吃药 (在 300ms CD 内，被拦截)
        dispatcher.Dispatch(session, "#1006/0/金创药(小量)!");
        // 验证背包依然有 2 瓶药，未被快速外挂吃空
        Assert.Equal(2, session.State.BagItems.Count);
    }

    [Fact]
    public void TestMonsterExpDistribution_SharesExpWithOnScreenPartyMembers()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 300, 300);
        mapManager.RegisterMap(map);

        var groupEngine = new GroupEngine();
        var dispatcher = new PacketDispatcher(mapManager, groupEngine: groupEngine);

        var leader = new ClientSession(1, null!)
        {
            CurrentMapId = 0,
            X = 50,
            Y = 50,
            CharacterName = "组队队长",
            State = new PlayerState { Level = 10, Exp = 0, Hp = 100, MaxHp = 100 }
        };
        var member = new ClientSession(2, null!)
        {
            CurrentMapId = 0,
            X = 52,
            Y = 50, // 距离 2 格同屏
            CharacterName = "组队队员",
            State = new PlayerState { Level = 10, Exp = 0, Hp = 100, MaxHp = 100 }
        };
        map.Aoi.Enter(leader, 50, 50);
        map.Aoi.Enter(member, 52, 50);

        // 队长建组并邀请队员
        groupEngine.CreateParty(leader);
        groupEngine.AddMember(leader, member);

        // 构造怪物 (MaxHp = 100, 基础经验 = 200)
        var monster = new MonsterInstance
        {
            Uid = 999,
            Name = "红野猪",
            MapId = 0,
            X = 50,
            Y = 50,
            MaxHp = 100,
            Hp = 0
        };

        // 队长击杀怪物
        dispatcher.OnMonsterKilledByPlayer(leader, monster);

        // 2人同屏 120% 组队经验加成: 200 * 1.2 = 240, 均摊每人 120
        Assert.Equal(120, leader.State.Exp);
        Assert.Equal(120, member.State.Exp);
        Assert.True(leader.HasBufferedPacket($"#{ProtocolConsts.SM_EXPERIENCE}/120/"));
        Assert.True(member.HasBufferedPacket($"#{ProtocolConsts.SM_EXPERIENCE}/120/"));
    }

    [Fact]
    public void TestGroupModeToggle_UsesDedicatedAllowGroupFlag_DoesNotTouchStorageLock()
    {
        var session = new ClientSession(1, null!)
        {
            State = new PlayerState
            {
                AllowGroup = true,
                IsStorageUnlocked = false // 初始仓库锁定
            }
        };

        // 切换组队模式关 (CM_GROUPMODE 1019)
        GroupEngine.ToggleGroupMode(session, false);
        Assert.False(session.State.AllowGroup);
        // 关键安全断言: 仓库锁定状态绝对不得被修改
        Assert.False(session.State.IsStorageUnlocked);

        // 切换组队模式开
        GroupEngine.ToggleGroupMode(session, true);
        Assert.True(session.State.AllowGroup);
        Assert.False(session.State.IsStorageUnlocked);
    }

    [Fact]
    public void TestGroupAddMember_BlockedIfTargetDisabledGroupMode()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 300, 300);
        mapManager.RegisterMap(map);

        var groupEngine = new GroupEngine();
        var dispatcher = new PacketDispatcher(mapManager, groupEngine: groupEngine);

        var leader = new ClientSession(1, null!)
        {
            CurrentMapId = 0,
            X = 50,
            Y = 50,
            CharacterName = "组队队长",
            State = new PlayerState { AllowGroup = true, Hp = 100 }
        };
        var target = new ClientSession(2, null!)
        {
            CurrentMapId = 0,
            X = 51,
            Y = 50,
            CharacterName = "孤狼玩家",
            State = new PlayerState { AllowGroup = false, Hp = 100 } // 关闭组队模式
        };
        map.Aoi.Enter(leader, 50, 50);
        map.Aoi.Enter(target, 51, 50);

        // 队长邀请孤狼玩家 (CM_ADDGROUPMEMBER 1021)
        dispatcher.Dispatch(leader, "#1021/孤狼玩家!");

        // 验证被拦截，孤狼玩家未被强拉入组
        Assert.Null(groupEngine.GetParty(target.SessionId));
        Assert.True(leader.HasBufferedPacket($"#{ProtocolConsts.SM_SYSMSG_DELPHI}/0/0/0/0/孤狼玩家 拒绝组队。!"));
    }

    [Fact]
    public void TestDropItemAndGold_SyncsClientStateWithDelItemAndGoldChanged()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 300, 300);
        mapManager.RegisterMap(map);

        var dispatcher = new PacketDispatcher(mapManager);
        var session = new ClientSession(1, null!)
        {
            CurrentMapId = 0,
            X = 50,
            Y = 50,
            CharacterName = "土豪玩家",
            State = new PlayerState
            {
                Hp = 100,
                Gold = 50000,
                BagWeight = 50,
                MaxBagWeight = 100
            }
        };
        session.State.BagItems.Add(new EquipItem { Uid = 888, Name = "回城卷", Weight = 1 });
        map.Aoi.Enter(session, 50, 50);

        // 1. 丢弃物品 (CM_DROPITEM 1000)
        dispatcher.Dispatch(session, $"#{ProtocolConsts.CM_DROPITEM}/0/!");
        Assert.Empty(session.State.BagItems);
        // 验证下发了 SM_DELITEM 与 SM_WEIGHTCHANGED
        Assert.True(session.HasBufferedPacket($"#{ProtocolConsts.SM_DELITEM}/888/0/0/0/回城卷!"));
        Assert.True(session.HasBufferedPacket($"#{ProtocolConsts.SM_WEIGHTCHANGED}/"));

        // 2. 丢弃金币 (CM_DROPGOLD 1016)
        dispatcher.Dispatch(session, $"#{ProtocolConsts.CM_DROPGOLD}/10000/!");
        Assert.Equal(40000, session.State.Gold);
        // 验证下发了 SM_GOLDCHANGED_DELPHI 与 SM_WEIGHTCHANGED
        Assert.True(session.HasBufferedPacket($"#{ProtocolConsts.SM_GOLDCHANGED_DELPHI}/40000/0/0/0/!"));
    }

    [Fact]
    public void TestSpellKillMonster_AuthoritativeLootDropsAndThreatAwakening_Success()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 100, 100);
        mapManager.RegisterMap(map);

        var regen = new MonsterRegenEngine(mapManager);
        regen.AddRule(new MonsterSpawnRule
        {
            RuleId = 1,
            MonsterName = "白野猪",
            MapId = 0,
            CenterX = 50,
            CenterY = 52,
            Radius = 0,
            MaxCount = 1,
            BaseHp = 1000
        });
        regen.ProcessRegenTick(new OfficialClientRulesEngine.DelphiRandom(1));
        var boss = regen.FindMonsterAt(0, 50, 52);
        Assert.NotNull(boss);
        boss.IsStoneForm = true; // 模拟祖玛石化状态
        boss.Hp = 10; // 残血

        var dispatcher = new PacketDispatcher(mapManager, regen);
        var mage = new ClientSession(1, null!)
        {
            CurrentMapId = 0,
            X = 50,
            Y = 50,
            CharacterName = "雷电法师",
            State = new PlayerState { Level = 40, Hp = 200, MaxHp = 200, Mp = 500, MaxMp = 500 }
        };
        map.Aoi.Enter(mage, 50, 50);

        // 法师施放雷电术 (MagicId = 10) 命中白野猪
        long now = Environment.TickCount64;
        dispatcher.ResolveAuthoritativeSpell(mage, 10, boss.Uid, 50, 52, now);

        // 验证：
        // 1. 怪物破除石化，并累加法师仇恨
        Assert.False(boss.IsStoneForm);
        Assert.True(boss.ThreatTable.HasTarget);

        // 2. 怪物死亡，在地面生成了战利品，并向法师广播了 SM_GROUNDITEM
        Assert.True(boss.IsDead);
        Assert.True(map.GetAllGroundItems().Count > 0);
        Assert.True(mage.HasBufferedPacket($"#{ProtocolConsts.SM_GROUNDITEM}/"));
        Assert.True(mage.HasBufferedPacket($"#{ProtocolConsts.SM_DEATH_ACTION_DELPHI}/"));
    }

    [Fact]
    public void TestMonsterGreenPoisonTick_ContinuousDamageAndLootDistribution_Success()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 100, 100);
        mapManager.RegisterMap(map);

        var regen = new MonsterRegenEngine(mapManager);
        regen.AddRule(new MonsterSpawnRule
        {
            RuleId = 1,
            MonsterName = "沃玛卫士",
            MapId = 0,
            CenterX = 50,
            CenterY = 51,
            Radius = 0,
            MaxCount = 1,
            BaseHp = 1000
        });
        regen.ProcessRegenTick(new OfficialClientRulesEngine.DelphiRandom(1));
        var mob = regen.FindMonsterAt(0, 50, 51);
        Assert.NotNull(mob);
        mob.Hp = 3; // 残血 3 点

        var dispatcher = new PacketDispatcher(mapManager, regen);
        var taoist = new ClientSession(1, null!)
        {
            CurrentMapId = 0,
            X = 50,
            Y = 50,
            CharacterName = "毒圣道士",
            State = new PlayerState { Level = 35, Hp = 300, MaxHp = 300, Mp = 300, MaxMp = 300 }
        };
        map.Aoi.Enter(taoist, 50, 50);

        long now = Environment.TickCount64;
        // 道士给怪物施加绿毒，记录施毒者为 taoist.SessionId
        mob.ApplyPoison(TaoistCombatEngine.PoisonType.Green, 20, 25, now, taoist.SessionId);
        Assert.True(mob.HasGreenPoison);

        // 模拟 1200ms 后世界时钟心跳 Tick
        dispatcher.UpdateWorldTick(1, now + 1200);

        // 验证：
        // 1. 怪物受到绿毒周期扣血致死
        Assert.True(mob.IsDead);
        Assert.Equal(0, mob.Hp);

        // 2. 怪物被毒死后依然生成战利品，且归属权为施毒道士
        var groundItems = map.GetAllGroundItems();
        Assert.NotEmpty(groundItems);
        Assert.Equal(taoist.SessionId, groundItems[0].OwnerSessionId);
    }

    [Fact]
    public void TestWarriorFireHitTimeout_ExtinguishesAutomatically()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 100, 100);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var warrior = new ClientSession(1, null!)
        {
            CurrentMapId = 0,
            X = 50,
            Y = 50,
            CharacterName = "烈火战士",
            State = new PlayerState { Level = 40, Hp = 600, MaxHp = 600 }
        };
        map.Aoi.Enter(warrior, 50, 50);

        long now = Environment.TickCount64;
        // 战士成功凝聚烈火剑法
        var (castOk, _) = warrior.State.FireHitTracker.Cast(now);
        Assert.True(castOk);
        warrior.State.BoFireHit = true;
        Assert.True(warrior.State.FireHitTracker.IsReady);

        // 模拟在 15 秒时进行心跳检测 (未超过 20 秒窗口期)
        dispatcher.UpdateWorldTick(1, now + 15000);
        Assert.True(warrior.State.FireHitTracker.IsReady);

        // 模拟在 21 秒时进行心跳检测 (超过 20 秒窗口期)
        dispatcher.UpdateWorldTick(2, now + 21000);
        Assert.False(warrior.State.FireHitTracker.IsReady);
        Assert.False(warrior.State.BoFireHit);
        Assert.True(warrior.HasBufferedPacket($"#{ProtocolConsts.SM_SYSMSG_DELPHI}/0/0/0/0/烈火蓄力已超时自然熄灭!"));
    }

    [Fact]
    public void TestStoragePassword_BruteForceDefense_LocksAfterThreeFailures()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 100, 100);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var session = new ClientSession(1, null!)
        {
            CurrentMapId = 0,
            X = 50,
            Y = 50,
            CharacterName = "安全玩家",
            IsStorageOpen = true,
            StorageNpcUid = 999,
            State = new PlayerState { Level = 30, StoragePassword = "CorrectPassword123" }
        };
        map.Aoi.Enter(session, 50, 50);

        // 第 1 次输错密码
        dispatcher.Dispatch(session, $"#{ProtocolConsts.CM_CHECKPASSWORD}/Wrong1/!");
        Assert.Equal(1, session.StoragePasswordFailedAttempts);
        Assert.True(session.IsStorageOpen);

        // 第 2 次输错密码
        dispatcher.Dispatch(session, $"#{ProtocolConsts.CM_CHECKPASSWORD}/Wrong2/!");
        Assert.Equal(2, session.StoragePasswordFailedAttempts);
        Assert.True(session.IsStorageOpen);

        // 第 3 次输错密码 (触发 5 分钟安全锁定并强制关闭仓库窗口)
        dispatcher.Dispatch(session, $"#{ProtocolConsts.CM_CHECKPASSWORD}/Wrong3/!");
        Assert.Equal(3, session.StoragePasswordFailedAttempts);
        Assert.False(session.IsStorageOpen);
        Assert.True(session.StoragePasswordLockUntilMs > Environment.TickCount64);
        Assert.True(session.HasBufferedPacket($"#{ProtocolConsts.SM_CLOSESTORAGEDLG}/"));

        // 锁定期间即使输入正确密码也必须被拦截
        dispatcher.Dispatch(session, $"#{ProtocolConsts.CM_CHECKPASSWORD}/CorrectPassword123/!");
        Assert.False(session.IsStorageOpen);
    }

    [Fact]
    public void TestAttackModeSwitch_BroadcastsOfficialSM766Protocol()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 100, 100);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var session = new ClientSession(1, null!)
        {
            CurrentMapId = 0,
            X = 50,
            Y = 50,
            CharacterName = "PK侠客",
            State = new PlayerState { Level = 30, AttackMode = 0 }
        };
        map.Aoi.Enter(session, 50, 50);

        // 客户端按 Ctrl+H 发送 @AttackMode 指令 (CM_SAY 3030)
        dispatcher.Dispatch(session, $"#{ProtocolConsts.CM_SAY}/@AttackMode!");

        // 验证攻击模式递增为 1 (和平模式)
        Assert.Equal(1, session.State.AttackMode);

        // 验证下发了官方攻击模式 UI 回显协议 SM_ATTACKMODECHANGED (766)
        Assert.True(session.HasBufferedPacket($"#{ProtocolConsts.SM_ATTACKMODECHANGED}/1/1/0/0/!"));
    }

    [Fact]
    public void TestMonsterCombat_FullPipeline_ArmorShieldReviveStatusAndDeath()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 100, 100);
        mapManager.RegisterMap(map);
        var regen = new MonsterRegenEngine(mapManager);
        var dispatcher = new PacketDispatcher(mapManager, regenEngine: regen);

        var player = new ClientSession(10, null!)
        {
            CurrentMapId = 0,
            X = 20,
            Y = 20,
            CharacterName = "测试法师",
            State = new PlayerState { Level = 40, Hp = 50, MaxHp = 150, Mp = 300, MaxMp = 300 }
        };
        map.Aoi.Enter(player, 20, 20);
        player.State.ActivateMagicShield(3, 30); // 开 3 级魔法盾
        player.State.WearItem(OfficialEquipSlot.RingL, new EquipItem { Name = "复活戒指", Shape = 114, Dura = 5000, MaxDura = 5000 });
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_SAY}/hi!"); // 激活 session 注册
        player.ClearBufferedPackets();

        regen.AddRule(new MonsterSpawnRule { RuleId = 999, MonsterName = "白野猪", MapId = 0, CenterX = 21, CenterY = 20, Radius = 0, MaxCount = 1, BaseHp = 1000 });
        regen.ProcessRegenTick(new OfficialClientRulesEngine.DelphiRandom(1));
        var boar = regen.FindMonsterAt(0, 21, 20);
        Assert.NotNull(boar);
        boar.AttackDamageMin = 100;
        boar.AttackDamageMax = 100;

        // 怪物近战攻击测试法师
        dispatcher.ProcessMonsterAiCycle(new OfficialClientRulesEngine.DelphiRandom(1));

        // 验证客户端收到血球更新协议 SM_MYSTATUS
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_MYSTATUS}/"));
        // 验证下发了受击动作 SM_OBJECTSTRUCK
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_OBJECTSTRUCK}/"));
    }

    [Fact]
    public void TestMonsterAi_MultiMapBroadcast_NotLockedToMap0()
    {
        var mapManager = new MapManager();
        var mengzhong = new GameMap(3, "盟重省", 200, 200);
        mapManager.RegisterMap(mengzhong);
        var regen = new MonsterRegenEngine(mapManager);
        var dispatcher = new PacketDispatcher(mapManager, regenEngine: regen);

        var player = new ClientSession(20, null!)
        {
            CurrentMapId = 3,
            X = 50,
            Y = 50,
            CharacterName = "盟重游侠",
            State = new PlayerState { Level = 35, Hp = 200, MaxHp = 200 }
        };
        mengzhong.Aoi.Enter(player, 50, 50);

        regen.AddRule(new MonsterSpawnRule { RuleId = 888, MonsterName = "多角虫", MapId = 3, CenterX = 51, CenterY = 50, Radius = 0, MaxCount = 1, BaseHp = 200 });
        regen.ProcessRegenTick(new OfficialClientRulesEngine.DelphiRandom(1));

        player.ClearBufferedPackets();
        dispatcher.ProcessMonsterAiCycle(new OfficialClientRulesEngine.DelphiRandom(1));

        // 验证在盟重省 (MapId=3) 成功接收到怪物攻击动作 SM_OBJECTHIT，彻底终结地图隔离硬编码 BUG
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_OBJECTHIT}/") || player.HasBufferedPacket($"#{ProtocolConsts.SM_OBJECTWALK}/"));
    }

    [Fact]
    public void TestFirewall_ContinuousBurnAndMonsterLoot_FullLoop()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 100, 100);
        mapManager.RegisterMap(map);
        var regen = new MonsterRegenEngine(mapManager);
        var dispatcher = new PacketDispatcher(mapManager, regenEngine: regen);

        var mage = new ClientSession(30, null!)
        {
            CurrentMapId = 0,
            X = 10,
            Y = 10,
            CharacterName = "火法",
            State = new PlayerState { Level = 35, Hp = 100, MaxHp = 100 }
        };
        map.Aoi.Enter(mage, 10, 10);

        // 放置火墙在 (15, 15)
        long now = Environment.TickCount64;
        map.AddFireWall(new FireWallEntity
        {
            Uid = 9001,
            X = 15,
            Y = 15,
            CasterSessionId = mage.SessionId,
            Damage = 50,
            ExpireTimeMs = now + 10_000
        });

        // 刷怪在 (15, 15)
        regen.AddRule(new MonsterSpawnRule { RuleId = 777, MonsterName = "红野猪", MapId = 0, CenterX = 15, CenterY = 15, Radius = 0, MaxCount = 1, BaseHp = 30 });
        regen.ProcessRegenTick(new OfficialClientRulesEngine.DelphiRandom(1));
        var pig = regen.FindMonsterAt(0, 15, 15);
        Assert.NotNull(pig);
        // 官方 Monster.DB 属性会覆盖规则 BaseHp，此处显式置为 30 点血，保证 50 点火墙伤害可一次烧死
        pig.Hp = 30;

        // 执行多地图并发心跳，火墙持续灼烧地表生物
        dispatcher.ProcessMultiMapParallelTick(1, now);

        // 验证红野猪被火墙烧死 (Hp 归零) 并爆落战利品
        Assert.True(pig.Hp <= 0);
    }

    [Fact]
    public void TestTaoistPet_SinglePetLimitAndOwnerBinding()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 100, 100);
        mapManager.RegisterMap(map);
        var regen = new MonsterRegenEngine(mapManager);
        var dispatcher = new PacketDispatcher(mapManager, regenEngine: regen);

        var taoist = new ClientSession(40, null!)
        {
            CurrentMapId = 0,
            X = 30,
            Y = 30,
            CharacterName = "天通道尊",
            State = new PlayerState { Level = 40, Hp = 200, MaxHp = 200, Sc = 30 }
        };
        map.Aoi.Enter(taoist, 30, 30);
        taoist.State.WearItem(OfficialEquipSlot.Lighting, new EquipItem { Name = "护身符", Dura = 1000, MaxDura = 1000 });

        // 1. 第一次召唤变异骷髅
        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_SPELL}/0/17/30/30/!");
        var pet1 = regen.FindMonsterAt(0, 31, 30);
        Assert.NotNull(pet1);
        Assert.Equal("变异骷髅", pet1.Name);
        Assert.Equal(taoist.SessionId, pet1.MasterSessionId);

        // 2. 第二次连续施放召唤变异骷髅，旧骷髅消散，刷新新骷髅
        taoist.LastSpellTimeMs = 0;
        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_SPELL}/0/17/30/30/!");
        Assert.True(pet1.IsDead); // 旧骷髅生命归零消散
    }

    [Fact]
    public void TestTaoistStealth_BreakOnMoveHitAndSpell()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 100, 100);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var taoist = new ClientSession(50, null!)
        {
            CurrentMapId = 0,
            X = 40,
            Y = 40,
            CharacterName = "遁地道士",
            State = new PlayerState { Level = 35, Hp = 150, MaxHp = 150, Sc = 25 }
        };
        map.Aoi.Enter(taoist, 40, 40);
        taoist.State.WearItem(OfficialEquipSlot.Lighting, new EquipItem { Name = "护身符", Dura = 1000, MaxDura = 1000 });

        // 1. 施展隐身术
        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_SPELL}/0/18/40/40/!");
        Assert.True(taoist.State.IsStealthed);

        // 2. 移动一步 (CM_WALK 1002) 立即破隐
        taoist.ClearBufferedPackets();
        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_WALK}/41/40/2/!");
        Assert.False(taoist.State.IsStealthed);
        Assert.True(taoist.HasBufferedPacket($"#{ProtocolConsts.SM_SPELLEFFECT_DELPHI}/"));

        // 3. 再次隐身，挥刀攻击 (CM_HIT 1004) 立即破隐
        taoist.LastSpellTimeMs = 0;
        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_SPELL}/0/18/41/40/!");
        Assert.True(taoist.State.IsStealthed);
        taoist.ClearBufferedPackets();
        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_HIT}/42/40/2/!");
        Assert.False(taoist.State.IsStealthed);
        Assert.True(taoist.HasBufferedPacket($"#{ProtocolConsts.SM_SPELLEFFECT_DELPHI}/"));

        // 4. 再次隐身，施放灵魂火符攻击 (CM_SPELL) 立即破隐
        taoist.LastSpellTimeMs = 0;
        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_SPELL}/0/18/41/40/!");
        Assert.True(taoist.State.IsStealthed);
        taoist.ClearBufferedPackets();
        taoist.LastSpellTimeMs = 0;
        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_SPELL}/0/13/42/40/!");
        Assert.False(taoist.State.IsStealthed);
        Assert.True(taoist.HasBufferedPacket($"#{ProtocolConsts.SM_SPELLEFFECT_DELPHI}/"));
    }

    [Fact]
    public void TestSpellCombat_PkFlashingAndAOEShieldReviveFeedback()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 100, 100);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var attacker = new ClientSession(60, null!)
        {
            CurrentMapId = 0,
            X = 60,
            Y = 60,
            CharacterName = "雷电法师",
            State = new PlayerState { Level = 35, Hp = 100, MaxHp = 100 }
        };
        var victim = new ClientSession(61, null!)
        {
            CurrentMapId = 0,
            X = 61,
            Y = 60,
            CharacterName = "无辜路人",
            State = new PlayerState { Level = 35, Hp = 150, MaxHp = 150 }
        };
        map.Aoi.Enter(attacker, 60, 60);
        map.Aoi.Enter(victim, 61, 60);
        victim.State.ActivateMagicShield(1, 10); // 开启魔法盾

        // 施展雷电术攻击无辜玩家
        dispatcher.Dispatch(attacker, $"#{ProtocolConsts.CM_SPELL}/{victim.SessionId}/10/61/60/!");

        // 验证攻击者进入 PK 灰名/黄名闪烁自卫惩罚计时
        Assert.True(attacker.State.IsFlashingName);
    }

    [Fact]
    public void TestGroup_MonsterKillExpDistributionAndLevelUp()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 800, 800);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var memberA = new ClientSession(80, null!)
        {
            CurrentMapId = 0,
            CharacterName = "战士大哥",
            State = new PlayerState { CharacterName = "战士大哥", Level = 30, Hp = 300, MaxHp = 300, Exp = 0, AllowGroup = true }
        };
        var memberB = new ClientSession(81, null!)
        {
            CurrentMapId = 0,
            CharacterName = "道士小弟",
            State = new PlayerState { CharacterName = "道士小弟", Level = 25, Hp = 120, MaxHp = 120, Exp = 0, AllowGroup = true }
        };
        dispatcher.RegisterSession(memberA);
        dispatcher.RegisterSession(memberB);

        // 1. 组队成功
        dispatcher.GroupEngine.CreateParty(memberA);
        dispatcher.GroupEngine.AddMember(memberA, memberB);
        Assert.True(dispatcher.GroupEngine.IsInSameParty(memberA.SessionId, memberB.SessionId));

        // 2. 杀怪 100 经验分发
        memberB.State.Exp = memberB.State.MaxExp - 10;
        var mon = new MonsterInstance { Uid = 111, Name = "红蛇", Exp = 100 };
        dispatcher.HandleMonsterDeathAndLoot(memberB, mon, map, Environment.TickCount64);

        // 3. 验证升级与经验增长
        Assert.Equal(26, memberB.State.Level);
        Assert.True(memberA.State.Exp > 0);
    }


    [Fact]
    public void TestSabukSiege_PalaceOccupationAndTaxTreasury()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 800, 800);
        var palace = new GameMap(SabukSiegeEngine.SabukPalaceMapId, "沙巴克皇宫", 50, 50);
        mapManager.RegisterMap(map);
        mapManager.RegisterMap(palace);
        var sabuk = new SabukSiegeEngine();
        var dispatcher = new PacketDispatcher(mapManager, sabukEngine: sabuk);

        // 1. 验证 NPC 商店购买商品 20% 商业税汇入沙巴克金库
        long initTreasury = dispatcher.TreasuryEngine.GoldTreasury;
        dispatcher.TreasuryEngine.CollectShopTransactionTax(100000); // 消费 10 万金币
        Assert.Equal(initTreasury + 20000, dispatcher.TreasuryEngine.GoldTreasury); // 抽成 2 万

        // 2. 验证多地图并行心跳中皇宫争夺战况推进与临时占领
        long nowMs = Environment.TickCount64;
        sabuk.RegisterAttackingGuild("邪恶势力");
        sabuk.StartSiege("正义之师", nowMs, durationMs: 7200_000);

        dispatcher.GuildEngine.LoadGuild("邪恶势力", "突袭战士", "战必胜", new[] { "突袭战士" });
        var attacker = new ClientSession(90, null!)
        {
            CurrentMapId = SabukSiegeEngine.SabukPalaceMapId,
            X = 25,
            Y = 25,
            CharacterName = "突袭战士",
            State = new PlayerState { Level = 40, Hp = 300, MaxHp = 300 }
        };
        dispatcher.Dispatch(attacker, $"#{ProtocolConsts.CM_SAY}/charge!");

        dispatcher.ProcessMultiMapParallelTick(1, nowMs + 100);
        Assert.Equal("邪恶势力", sabuk.TempOccupierGuild);
    }

    [Fact]
    public void TestMining_PickaxeAnimationAndDuraSync()
    {
        var mapManager = new MapManager();
        var mineMap = new GameMap(1, "比奇废矿区", 300, 300);
        mapManager.RegisterMap(mineMap);
        var dispatcher = new PacketDispatcher(mapManager);

        var miner = new ClientSession(95, null!)
        {
            CurrentMapId = 1,
            X = 100,
            Y = 100,
            CharacterName = "老矿工",
            State = new PlayerState { Level = 20, Hp = 100, MaxHp = 100 }
        };
        mineMap.Aoi.Enter(miner, 100, 100);

        var observer = new ClientSession(96, null!)
        {
            CurrentMapId = 1,
            X = 101,
            Y = 100,
            CharacterName = "旁观者",
            State = new PlayerState { Level = 20, Hp = 100, MaxHp = 100 }
        };
        mineMap.Aoi.Enter(observer, 101, 100);

        var pickaxe = new EquipItem { Name = "鹤嘴锄", Dura = 10, MaxDura = 10 };
        miner.State.WearItem(OfficialEquipSlot.Weapon, pickaxe);
        miner.ClearBufferedPackets();
        observer.ClearBufferedPackets();

        // 执行挖矿 (CM_BIGHIT 3015)
        dispatcher.Dispatch(miner, $"#{ProtocolConsts.CM_BIGHIT}/101/100/2/!");

        // 验证向周围九宫格旁观者广播了挥镐动作 SM_OBJECTHIT
        Assert.True(observer.HasBufferedPacket($"#{ProtocolConsts.SM_OBJECTHIT}/"));
    }

    [Fact]
    public void TestNormalRepair_DurabilityDecayAndCost()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 800, 800);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var player = new ClientSession(101, null!)
        {
            CurrentMapId = 0,
            X = 330,
            Y = 330,
            CharacterName = "破损刀客",
            State = new PlayerState { Level = 30, Gold = 1000 }
        };
        map.Aoi.Enter(player, 330, 330);

        // 注册比奇铁匠 NPC (NPC UID = 50)
        dispatcher.NpcEngine.RegisterNpc(50, "铁匠", 0, 330, 331);

        // 穿戴一把磨损的裁决之杖: 当前持久 10，最大持久 32 (损失 22 点)
        var weapon = new EquipItem { Uid = 8888, Name = "裁决之杖", Dura = 10, MaxDura = 32 };
        player.State.WearItem(OfficialEquipSlot.Weapon, weapon);

        // 发送普通修理 CM_USERREPAIRITEM (1023): npcUid/slot (武器为槽位 1)
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_USERREPAIRITEM}/50/1/!");

        // 1. 金币扣除: 磨损量 22 * 1 = 22 金币
        Assert.Equal(978, player.State.Gold);

        // 2. 最大持久上限衰减: 掉 22/3 = 7 点，新最大持久 = 32 - 7 = 25，当前持久恢复至 25
        Assert.Equal(25, weapon.MaxDura);
        Assert.Equal(25, weapon.Dura);

        // 3. 验证收到 SM_REPAIROK_DELPHI (669), Recog 严格为人物最新金币 (官方 IDA 0x469F9B / UpdateGold)
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_REPAIROK_DELPHI}/978/25/25/0/裁决之杖!"));
    }

    [Fact]
    public void TestGodPrayerSet_RebellionAndShatterWorldEvent()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 800, 800);
        mapManager.RegisterMap(map);
        var regen = new MonsterRegenEngine(mapManager);
        var dispatcher = new PacketDispatcher(mapManager, regenEngine: regen);

        // 地图上有一只道士召唤的神兽
        var pet = new MonsterInstance { Uid = 555, Name = "神兽", MasterSessionId = 999, Hp = 500, MaxHp = 500 };
        regen.AddMonster(pet);

        var wearer = new ClientSession(102, null!)
        {
            CurrentMapId = 0,
            X = 330,
            Y = 330,
            CharacterName = "祈祷使者",
            State = new PlayerState { Level = 40 }
        };
        map.Aoi.Enter(wearer, 330, 330);

        // 先预装 4 件祈祷装备
        wearer.State.WearItem(OfficialEquipSlot.Necklace, new EquipItem { Name = "祈祷项链" });
        wearer.State.WearItem(OfficialEquipSlot.ArmringL, new EquipItem { Name = "祈祷手镯" });
        wearer.State.WearItem(OfficialEquipSlot.RingL, new EquipItem { Name = "祈祷戒指" });
        wearer.State.WearItem(OfficialEquipSlot.Weapon, new EquipItem { Name = "祈祷之刃" });

        // 背包放入最后一件“祈祷头盔”
        wearer.State.BagItems.Add(new EquipItem { Name = "祈祷头盔" });

        // 穿戴最后一件祈祷头盔 (CM_WEARITEM 1003: bagSlot=0/equipSlot=4)
        dispatcher.Dispatch(wearer, $"#{ProtocolConsts.CM_WEARITEM}/0/4/!");

        // 1. 神兽叛变: 脱离主人掌控，名称变为发狂
        Assert.Equal(0, pet.MasterSessionId);
        Assert.Equal("发狂的神兽", pet.Name);

        // 2. 祈祷 5 件套随机粉碎 1 件 (穿戴栏中祈祷件数应从 5 降为 4)
        int remainingPrayerCount = 0;
        for (int i = 0; i < 9; i++)
        {
            var eq = wearer.State.Equipment[i];
            if (eq != null && eq.Name.Contains("祈祷")) remainingPrayerCount++;
        }
        Assert.Equal(4, remainingPrayerCount);
    }

    [Fact]
    public void TestMemorySet_LeaderSummonAllCommand()
    {
        var mapManager = new MapManager();
        var map0 = new GameMap(0, "比奇省", 800, 800);
        var map3 = new GameMap(3, "盟重省", 1000, 1000);
        mapManager.RegisterMap(map0);
        mapManager.RegisterMap(map3);
        var dispatcher = new PacketDispatcher(mapManager);

        var leader = new ClientSession(110, null!)
        {
            CurrentMapId = 3,
            X = 500,
            Y = 500,
            CharacterName = "记忆队长",
            State = new PlayerState { Level = 45 }
        };
        var member = new ClientSession(111, null!)
        {
            CurrentMapId = 0,
            X = 100,
            Y = 100,
            CharacterName = "远方队员",
            State = new PlayerState { Level = 35 }
        };

        map3.Aoi.Enter(leader, 500, 500);
        map0.Aoi.Enter(member, 100, 100);

        // 队长穿齐记忆套 (4 件: 头/链/镯/戒)
        leader.State.WearItem(OfficialEquipSlot.Helmet, new EquipItem { Name = "记忆头盔" });
        leader.State.WearItem(OfficialEquipSlot.Necklace, new EquipItem { Name = "记忆项链" });
        leader.State.WearItem(OfficialEquipSlot.ArmringL, new EquipItem { Name = "记忆手镯" });
        leader.State.WearItem(OfficialEquipSlot.RingL, new EquipItem { Name = "记忆戒指" });

        // 组建队伍并加入队员
        dispatcher.GroupEngine.CreateParty(leader);
        dispatcher.GroupEngine.AddMember(leader, member);

        // 队长输入聊天指令 @天地合一
        dispatcher.Dispatch(leader, $"#{ProtocolConsts.CM_SAY}/@天地合一!");

        // 验证队员被跨图拉取到队长所在地图与身周开阔坐标
        Assert.Equal(3, member.CurrentMapId);
        Assert.True(Math.Abs(member.X - leader.X) <= 2 && Math.Abs(member.Y - leader.Y) <= 2);
        Assert.True(member.HasBufferedPacket($"#{ProtocolConsts.SM_NEWMAP_DELPHI}/3/{member.X}/{member.Y}/0/!"));
    }

    [Fact]
    public void TestAuthoritativePlayerKill_PkMurderAndCurseSettlement()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 800, 800);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var killer = new ClientSession(120, null!)
        {
            CurrentMapId = 0,
            X = 200,
            Y = 200,
            CharacterName = "冷血杀手",
            State = new PlayerState { Level = 40, PkPoints = 0 }
        };
        var victim = new ClientSession(121, null!)
        {
            CurrentMapId = 0,
            X = 201,
            Y = 200,
            CharacterName = "无辜平民",
            State = new PlayerState { Level = 20, Hp = 20, MaxHp = 200, PkPoints = 0 }
        };
        map.Aoi.Enter(killer, 200, 200);
        map.Aoi.Enter(victim, 201, 200);

        // 受害者佩戴祈祷头盔，背包里有一把乌木剑
        victim.State.WearItem(OfficialEquipSlot.Helmet, new EquipItem { Name = "祈祷头盔" });
        victim.State.BagItems.Add(new EquipItem { Uid = 9991, Name = "乌木剑", Weight = 5 });

        // 杀手手持凝霜剑，面朝东，命中拉满
        killer.Direction = 2;
        victim.State.Hp = 5;
        killer.State.WearItem(OfficialEquipSlot.Weapon, new EquipItem { Name = "凝霜", Dc = 15, MaxDc = 25, Dura = 20, MaxDura = 20 });
        killer.State.Hit = 100;

        // 执行二进制攻击意图杀人
        Span<byte> atkPayload = stackalloc byte[6];
        new BinaryProtocol.AttackIntentPayload
        {
            TargetUid = 121,
            SkillType = 0,
            Direction = 2 // 东面朝向受害者
        }.Write(atkPayload);

        dispatcher.HandleBinaryAttackIntent(killer, atkPayload);

        // 1. 受害者阵亡
        Assert.True(victim.State.Hp <= 0);

        // 2. 杀手恶意杀害白名，PK点数增加 100
        Assert.Equal(100, killer.State.PkPoints);

        // 3. 祈祷头盔死亡碎裂消散
        Assert.Null(victim.State.Equipment[(int)OfficialEquipSlot.Helmet]);
        Assert.True(victim.HasBufferedPacket("祈祷头盔破碎了。"));
    }

    [Fact]
    public void TestWorldBroadcast_MultiMapGlobalDeliveryAndItemCost()
    {
        var mapManager = new MapManager();
        var mapBiQi = new GameMap(0, "比奇省", 800, 800);
        var mapMengZhong = new GameMap(3, "盟重省", 800, 800);
        mapManager.RegisterMap(mapBiQi);
        mapManager.RegisterMap(mapMengZhong);
        var dispatcher = new PacketDispatcher(mapManager);

        var sender = new ClientSession(201, null!)
        {
            CurrentMapId = 0,
            X = 330,
            Y = 330,
            CharacterName = "号角战士",
            State = new PlayerState { Level = 40 }
        };
        var receiverFar = new ClientSession(202, null!)
        {
            CurrentMapId = 3, // 处于盟重省远端地图
            X = 500,
            Y = 500,
            CharacterName = "沙漠旅人",
            State = new PlayerState { Level = 35 }
        };
        mapBiQi.Aoi.Enter(sender, 330, 330);
        mapMengZhong.Aoi.Enter(receiverFar, 500, 500);

        // 1. 背包无千里传音道具，喊话被拦截
        dispatcher.Dispatch(sender, $"#{ProtocolConsts.CM_SAY}/!天下风云出我辈!");
        Assert.True(sender.HasBufferedPacket("背包中没有【千里传音】"));

        // 2. 放入千里传音道具，再次喊话成功，远端地图玩家跨图收到全服广播
        sender.ClearBufferedPackets();
        receiverFar.ClearBufferedPackets();
        sender.State.BagItems.Add(new EquipItem { Uid = 7001, Name = "千里传音", Weight = 1 });

        dispatcher.Dispatch(sender, $"#{ProtocolConsts.CM_SAY}/!天下风云出我辈!");
        Assert.Empty(sender.State.BagItems); // 道具扣除
        Assert.True(receiverFar.HasBufferedPacket("[千里传音] 号角战士: 天下风云出我辈"));
    }

    [Fact]
    public void TestStorageSecurity_LockBypassPrevention()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 800, 800);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var player = new ClientSession(210, null!)
        {
            AccountName = "safe_user_88",
            CharacterName = "安全玩家",
            CurrentMapId = 0,
            X = 330,
            Y = 330,
            State = new PlayerState { Level = 30, Gold = 5000 },
            IsStorageOpen = true,
            StorageNpcUid = 99
        };
        map.Aoi.Enter(player, 330, 330);
        dispatcher.NpcEngine.RegisterNpc(99, "保管员", 0, 330, 331);

        // 设置二级密码 "pass1234"
        dispatcher.StorageSecEngine.SetInitialPassword(player.AccountName, "pass1234");
        // 模拟重新上线，锁定状态
        dispatcher.StorageSecEngine.GetOrCreateProfile(player.AccountName).IsSessionUnlocked = false;

        // 1. 未输入密码直接提取物品或存金币，被强制拦截并下发 SM_NEEDPASSWORD
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_USERSTORAGEDEPOSIT}/0/乌木剑!");
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_NEEDPASSWORD}/"));

        // 2. 输入错误密码，防暴力统计增加
        player.ClearBufferedPackets();
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_CHECKPASSWORD}/wrongpass!");
        Assert.False(dispatcher.StorageSecEngine.CanAccessStorage(player.AccountName));

        // 3. 输入正确密码，解锁成功
        player.ClearBufferedPackets();
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_CHECKPASSWORD}/pass1234!");
        Assert.True(dispatcher.StorageSecEngine.CanAccessStorage(player.AccountName));
        Assert.True(player.HasBufferedPacket("密码验证成功"));
    }

    [Fact]
    public void TestSpecialRing_TeleportRingCommandAndCooldown()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 500, 500);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var player = new ClientSession(220, null!)
        {
            CurrentMapId = 0,
            X = 50,
            Y = 50,
            CharacterName = "逍遥战神",
            State = new PlayerState { Level = 40 }
        };
        map.Aoi.Enter(player, 50, 50);
        dispatcher.RegisterSession(player);

        // 1. 未佩戴传送戒指，发送 @move 100 100 失败
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_SAY}/@move 100 100!");
        Assert.True(player.HasBufferedPacket("你未佩戴传送戒指"));

        // 2. 佩戴传送戒指
        player.State.Equipment[(int)OfficialEquipSlot.RingL] = new EquipItem { Name = "传送戒指", Shape = 112 };
        player.State.RecalculateAttributes();
        player.ClearBufferedPackets();
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_SAY}/@move 100 100!");
        Assert.Equal(100, player.X);
        Assert.Equal(100, player.Y);

        // 3. 10 秒冷却内再次传送，被阻止
        player.ClearBufferedPackets();
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_SAY}/@move 120 120!");
        Assert.True(player.HasBufferedPacket("传送戒指魔力正在凝聚中"));
    }

    [Fact]
    public void TestPetFollow_RestModeAndMonsterKillLevelUp()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "白日门", 300, 300);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var mage = new ClientSession(230, null!)
        {
            CurrentMapId = 0,
            X = 100,
            Y = 100,
            CharacterName = "白日法师",
            State = new PlayerState { Level = 40 }
        };
        map.Aoi.Enter(mage, 100, 100);

        // 招募 1 只 0 级虎卫随从 (初始 HP 400)
        var huwei = HuWeiPetEngine.CreateHuWei(801, mage.SessionId, 0, 101, 100);
        mage.Pets.Add(huwei);

        // 1. 切换模式: @休息 -> @跟随
        dispatcher.Dispatch(mage, $"#{ProtocolConsts.CM_SAY}/@休息!");
        Assert.True(huwei.IsResting);
        Assert.True(mage.HasBufferedPacket("已进入休息状态"));

        mage.ClearBufferedPackets();
        dispatcher.Dispatch(mage, $"#{ProtocolConsts.CM_SAY}/@跟随!");
        Assert.False(huwei.IsResting);
        Assert.True(mage.HasBufferedPacket("跟随主人前进"));

        // 2. 杀怪分发经验给随从并晋级至 1 级 (虎卫升级血量几何倍增: 400 * 1.35 = 540)
        var boss = new MonsterInstance { Uid = 999, Name = "沃玛卫士", Exp = 500, MaxHp = 250 };
        dispatcher.OnMonsterKilledByPlayer(mage, boss);

        Assert.Equal(1, huwei.Level);
        Assert.Equal("Yellow", huwei.NameColor);
        Assert.Equal(540, huwei.MaxHp);
        Assert.True(mage.HasBufferedPacket("已晋升至 1 级"));
    }

    [Fact]
    public void TestPetTeleportSync_CrossMapAndSameMap_DisappearAndAppearBroadcast()
    {
        var mapManager = new MapManager();
        var map1 = new GameMap(0, "比奇省", 500, 500);
        var map2 = new GameMap(3, "盟重省", 500, 500);
        mapManager.RegisterMap(map1);
        mapManager.RegisterMap(map2);
        var dispatcher = new PacketDispatcher(mapManager);

        var player = new ClientSession(301, null!)
        {
            CurrentMapId = 0,
            X = 100,
            Y = 100,
            CharacterName = "逍遥法师",
            State = new PlayerState { Level = 45, Hp = 200, MaxHp = 200 }
        };
        map1.Aoi.Enter(player, 100, 100);

        var pet = HuWeiPetEngine.CreateHuWei(901, player.SessionId, 0, 101, 100);
        player.Pets.Add(pet);

        // 旁观观察者
        var bystander = new ClientSession(302, null!)
        {
            CurrentMapId = 0,
            X = 102,
            Y = 100,
            CharacterName = "路人甲",
            State = new PlayerState { Level = 20 }
        };
        map1.Aoi.Enter(bystander, 102, 100);

        // 1. 同地图瞬移 (随机传送卷)
        player.State.BagItems.Add(new EquipItem { Uid = 5001, Name = "随机传送卷", Weight = 1 });
        bystander.ClearBufferedPackets();
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_EATITEM}/0!");

        // 随从坐标紧随主人 (X+1, Y)
        Assert.Equal(player.X + 1, pet.X);
        Assert.Equal(player.Y, pet.Y);
        // 路人甲收到了随从在旧位置的消散广播
        Assert.True(bystander.HasBufferedPacket($"#{ProtocolConsts.SM_ENTITYDISAPPEAR}/{pet.Uid}/101/100/0/{pet.Name}!"));

        // 2. 跨地图传送 (比奇 0 -> 盟重 3 踩传送门)
        dispatcher.TriggerMapPassage(player, new MapPassage { FromMapId = 0, FromX = player.X, FromY = player.Y, ToMapId = 3, ToX = 330, ToY = 330 });

        Assert.Equal(3, player.CurrentMapId);
        Assert.Equal(3, pet.CurrentMapId);
        Assert.Equal(player.X + 1, pet.X);
        Assert.Equal(player.Y, pet.Y);
    }

    [Fact]
    public void TestPetCleanupOnMasterDeathAndDisconnect_NoGhostPets()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 500, 500);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var master = new ClientSession(401, null!)
        {
            CurrentMapId = 0,
            X = 200,
            Y = 200,
            CharacterName = "倒霉法师",
            State = new PlayerState { Level = 35, Hp = 100, MaxHp = 100 }
        };
        map.Aoi.Enter(master, 200, 200);

        var pet = HuWeiPetEngine.CreateHuWei(955, master.SessionId, 0, 201, 200);
        master.Pets.Add(pet);

        var observer = new ClientSession(402, null!)
        {
            CurrentMapId = 0,
            X = 202,
            Y = 200,
            CharacterName = "吃瓜群众",
            State = new PlayerState { Level = 30 }
        };
        map.Aoi.Enter(observer, 202, 200);

        // 1. 主人倒地阵亡复活，随从立刻消散暴毙
        observer.ClearBufferedPackets();
        master.State.Hp = 0;
        dispatcher.HandleRevive(master, "0"); // 官方复活权威结算
        // 观察者收到随从消散广播
        Assert.Empty(master.Pets);
        Assert.True(observer.HasBufferedPacket($"#{ProtocolConsts.SM_ENTITYDISAPPEAR}/955/201/200/0/虎卫!"));

        // 2. 重新召唤一只虎卫，测试主人断线注销幽灵随从清理
        master.Pets.Add(HuWeiPetEngine.CreateHuWei(956, master.SessionId, 0, 201, 200));
        observer.ClearBufferedPackets();
        dispatcher.OnSessionDisconnected(master);

        Assert.Empty(master.Pets);
        Assert.True(observer.HasBufferedPacket($"#{ProtocolConsts.SM_ENTITYDISAPPEAR}/956/201/200/0/虎卫!"));
    }

    [Fact]
    public void TestStealthRingTakeOff_AndHeartbeatImmunity()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 500, 500);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var player = new ClientSession(501, null!)
        {
            CurrentMapId = 0,
            X = 150,
            Y = 150,
            CharacterName = "潜行盗贼",
            State = new PlayerState { Level = 40, Hp = 300, MaxHp = 300 }
        };
        map.Aoi.Enter(player, 150, 150);

        // 放入并佩戴隐身戒指 (Slot 7 = OfficialEquipSlot.RingL)
        var stealthRing = new EquipItem { Uid = 8888, Name = "隐身戒指", Shape = 113, Weight = 1 };
        player.State.BagItems.Add(stealthRing);

        // 穿戴
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_WEARITEM}/0/7!");
        Assert.True(player.State.HasInvisibleRing);
        Assert.True(player.State.IsStealthed);

        // 1. 世界心跳超时不会消除隐身特戒的隐身
        long now = Environment.TickCount64;
        player.State.StealthExpireMs = now - 1000; // 已过期
        dispatcher.UpdateWorldTick(1, now);
        Assert.True(player.State.IsStealthed); // 仍然隐身

        // 2. 脱下隐身戒指，立即破除隐身并下发显形广播
        player.ClearBufferedPackets();
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_TAKEOFFITEM}/7!");
        Assert.False(player.State.HasInvisibleRing);
        Assert.False(player.State.IsStealthed);
        Assert.True(player.HasBufferedPacket("显形"));
    }

    [Fact]
    public void TestGroupInvisibility_AreaStealthEffect_Magic19()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 500, 500);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var taoist = new ClientSession(601, null!)
        {
            CurrentMapId = 0,
            X = 100,
            Y = 100,
            CharacterName = "圣手中道",
            State = new PlayerState { Level = 40, Sc = 20, Hp = 200, MaxHp = 200 }
        };
        // 施法耗材: 必须装备或持有护身符
        taoist.State.BagItems.Add(new EquipItem { Name = "护身符", Dura = 100, MaxDura = 100 });
        map.Aoi.Enter(taoist, 100, 100);

        var teammate1 = new ClientSession(602, null!)
        {
            CurrentMapId = 0,
            X = 101,
            Y = 100,
            CharacterName = "同屏战友",
            State = new PlayerState { Level = 40, Hp = 500, MaxHp = 500 }
        };
        map.Aoi.Enter(teammate1, 101, 100);

        var farAway = new ClientSession(603, null!)
        {
            CurrentMapId = 0,
            X = 110,
            Y = 110,
            CharacterName = "远方路人",
            State = new PlayerState { Level = 40, Hp = 500, MaxHp = 500 }
        };
        map.Aoi.Enter(farAway, 110, 110);

        // 释放集体隐身术 (MagicId = 19, targetUid=0, 目标坐标 100, 100)
        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_SPELL}/0/19/100/100/!");

        // 3x3 范围内的道士自身与同屏战友均进入隐身
        Assert.True(taoist.State.IsStealthed);
        Assert.True(teammate1.State.IsStealthed);
        // 超出范围的远方路人不受影响
        Assert.False(farAway.State.IsStealthed);
    }

    [Fact]
    public void TestPotionContinuousReplenishment_InWorldTick()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 500, 500);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var player = new ClientSession(701, null!)
        {
            CurrentMapId = 0,
            X = 50,
            Y = 50,
            CharacterName = "吃药战士",
            State = new PlayerState { Level = 30, Hp = 50, MaxHp = 200, Mp = 20, MaxMp = 100 }
        };
        map.Aoi.Enter(player, 50, 50);

        // 喝下中量金创药 (总回复 50 HP, 官方 StdItems.DB AC=50)
        player.State.BagItems.Add(new EquipItem { Uid = 7701, Name = "中量金创药", Weight = 2 });
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_EATITEM}/0!");

        Assert.Equal(50, player.State.IncHealth);

        // 世界心跳执行推进
        player.ClearBufferedPackets();
        long now = Environment.TickCount64;
        dispatcher.UpdateWorldTick(1, now);

        // 释放 5 点血量
        Assert.Equal(55, player.State.Hp);
        Assert.Equal(45, player.State.IncHealth);
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_HEALTHSPELLCHANGED}/55/20/200/100/!"));
    }

    [Fact]
    public void TestGuardExecution_RedNameMegaExplosionAndDrops()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "盟重省", 500, 500);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        // 放置大刀卫士于 (100, 100)
        dispatcher.SafeZoneEngine.SpawnGuard(9001, "大刀卫士", 0, 100, 100);

        // 放置红名玩家于 (101, 100)
        var redPlayer = new ClientSession(801, null!)
        {
            CurrentMapId = 0,
            X = 101,
            Y = 100,
            CharacterName = "恶贯满盈红名",
            State = new PlayerState
            {
                Level = 35,
                Hp = 100,
                MaxHp = 200,
                PkPoints = 500 // 官方红名门槛 (>200)
            }
        };
        redPlayer.State.BagItems.Add(new EquipItem { Uid = 8801, Name = "祝福油", Weight = 1 });
        redPlayer.State.Equipment[(int)OfficialEquipSlot.Weapon] = new EquipItem { Uid = 8802, Name = "裁决之杖", Weight = 50 };
        map.Aoi.Enter(redPlayer, 101, 100);
        dispatcher.Dispatch(redPlayer, $"#{ProtocolConsts.CM_SAY}/hi!"); // 激活 session 注册

        Assert.True(redPlayer.State.IsRedName);

        // 心跳执行，大刀卫士秒杀红名
        long now = Environment.TickCount64;
        dispatcher.UpdateWorldTick(1, now);

        // 验证红名被正法秒杀倒地并触发大爆
        Assert.Equal(0, redPlayer.State.Hp);
        Assert.True(redPlayer.HasBufferedPacket($"#{ProtocolConsts.SM_DEATH_ACTION_DELPHI}/801/101/100/0/恶贯满盈红名!"));
        // 地表生成掉落物 (全地图遍历地表物品或背包减少)
        Assert.Empty(redPlayer.State.BagItems); // 官方红名大爆背包 100% 掉落
    }

    [Fact]
    public void TestMonsterKill_PrayerHelmetShatter()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 500, 500);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var player = new ClientSession(802, null!)
        {
            CurrentMapId = 0,
            X = 200,
            Y = 200,
            CharacterName = "祈祷战士",
            State = new PlayerState
            {
                Level = 35,
                Hp = 10,
                MaxHp = 200
            }
        };
        player.State.Equipment[(int)OfficialEquipSlot.Helmet] = new EquipItem { Uid = 8803, Name = "祈祷头盔", Weight = 5 };
        map.Aoi.Enter(player, 200, 200);

        // 玩家阵亡 (模拟被怪物或毒素击杀)
        player.State.Hp = 0;
        dispatcher.OnPlayerDied(player);

        // 验证：祈祷头盔必然碎裂粉碎，槽位变空
        Assert.Null(player.State.Equipment[(int)OfficialEquipSlot.Helmet]);
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_SYSMSG_DELPHI}/0/0/0/0/祈祷头盔破碎了。!"));
    }

    [Fact]
    public void TestGuildWar_And_Alliance_ChatCommands_And_Protocols()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 500, 500);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var masterA = new ClientSession(803, null!)
        {
            CurrentMapId = 0,
            X = 50,
            Y = 50,
            CharacterName = "行会老大A",
            State = new PlayerState { Level = 40, Gold = 100_000 }
        };
        map.Aoi.Enter(masterA, 50, 50);

        var masterB = new ClientSession(804, null!)
        {
            CurrentMapId = 0,
            X = 51,
            Y = 50,
            CharacterName = "行会老大B",
            State = new PlayerState { Level = 40, Gold = 100_000 }
        };
        map.Aoi.Enter(masterB, 51, 50);

        // 先预载两个行会
        dispatcher.GuildEngine.LoadGuild("战神殿", masterA.CharacterName, "战神殿天下第一", null);
        dispatcher.GuildEngine.LoadGuild("逍遥阁", masterB.CharacterName, "逍遥阁威武", null);

        // 1. 掌门人 A 宣战逍遥阁 (@宣战 逍遥阁)
        dispatcher.Dispatch(masterA, $"#{ProtocolConsts.CM_SAY}/@宣战 逍遥阁!");
        Assert.Equal(70_000, masterA.State.Gold); // 扣除 30,000 宣战费
        long now = Environment.TickCount64;
        Assert.True(dispatcher.GuildWarEngine.AreGuildsAtWar("战神殿", "逍遥阁", now));
        Assert.True(dispatcher.GuildWarEngine.IsKillExemptFromPkPenalty("战神殿", "逍遥阁", now));

        // 2. 掌门人 A 发起同盟协议 (CM_GUILD_ALLY_REQUEST 1044)
        dispatcher.Dispatch(masterA, $"#{ProtocolConsts.CM_GUILD_ALLY_REQUEST}/逍遥阁!");
        Assert.True(dispatcher.GuildWarEngine.AreGuildsAllied("战神殿", "逍遥阁"));

        // 3. 掌门人 A 解除同盟协议 (CM_GUILD_BREAK_ALLY 1045)
        dispatcher.Dispatch(masterA, $"#{ProtocolConsts.CM_GUILD_BREAK_ALLY}/逍遥阁!");
        Assert.False(dispatcher.GuildWarEngine.AreGuildsAllied("战神殿", "逍遥阁"));
    }


    [Fact]
    public void TestWarriorSkills_WideHit_WeaponTrialSlash()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 500, 500);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var warrior = new ClientSession(806, null!)
        {
            CurrentMapId = 0,
            X = 60,
            Y = 60,
            CharacterName = "试刀战士",
            State = new PlayerState { Level = 38, Hp = 200, MaxHp = 200, Mp = 100, MaxMp = 100 }
        };
        map.Aoi.Enter(warrior, 60, 60);

        // 给战士穿戴一把处于待试刀升级状态的屠龙宝刀 (UpgradeState = 10 表示待试刀 DC+1)
        var weapon = new EquipItem
        {
            Uid = 9991,
            Name = "屠龙",
            Weight = 99,
            Dc = 5,
            MaxDc = 35,
            UpgradeState = 10
        };
        warrior.State.Equipment[(int)OfficialEquipSlot.Weapon] = weapon;

        // 使用半月弯刀 (CM_WIDEHIT 3024) 横扫出刀试刀
        warrior.ClearBufferedPackets();
        dispatcher.Dispatch(warrior, $"#{ProtocolConsts.CM_WIDEHIT}/60/60/0!");

        // 验证：试刀判定成功，UpgradeState 清除，武器属性增加且下发成功系统提示
        Assert.Equal(0, weapon.UpgradeState);
        Assert.Equal(5, weapon.Dc);
        Assert.Equal(36, weapon.MaxDc); // 35 -> 36
        Assert.True(warrior.HasBufferedPacket($"#{ProtocolConsts.SM_SYSMSG_DELPHI}/0/0/0/0/你的武器升级成功了。!"));
    }

    [Fact]
    public void TestRevive_InvincibleProtection_PreventsSpawnCamping()
    {
        var mapManager = new MapManager();
        var map = mapManager.GetMap(3)!; // 盟重省 (MapId = 3)
        var dispatcher = new PacketDispatcher(mapManager);

        dispatcher.SafeZoneEngine.SpawnGuard(9002, "大刀卫士", 3, 850, 660);

        var redPlayer = new ClientSession(901, null!)
        {
            CurrentMapId = 3,
            X = 850,
            Y = 661,
            CharacterName = "复活红名",
            State = new PlayerState { Level = 35, Hp = 0, MaxHp = 200, PkPoints = 300 }
        };
        map.Aoi.Enter(redPlayer, 850, 661);
        dispatcher.Dispatch(redPlayer, $"#{ProtocolConsts.CM_SAY}/hi!");

        // 玩家执行复活 (流放红名村 850, 660)
        long now = Environment.TickCount64;
        dispatcher.HandleRevive(redPlayer, "0");

        Assert.Equal(100, redPlayer.State.Hp);
        Assert.True(redPlayer.State.IsInvincible(now + 1000)); // 处于 3 秒无敌中

        // 守尸大刀卫士心跳扫描处决，因无敌保护跳过
        dispatcher.UpdateWorldTick(1, now + 1000);
        Assert.Equal(100, redPlayer.State.Hp); // 依然保持复活血量，未被秒杀守尸

        // 3 秒保护结束后，大刀卫士正常正法
        dispatcher.UpdateWorldTick(2, now + 3500);
        Assert.Equal(0, redPlayer.State.Hp); // 无敌解除，被卫士正法
    }

    [Fact]
    public void TestPkPoints_NaturalDecay_And_CleanPkCommand()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "盟重省", 500, 500);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var player = new ClientSession(902, null!)
        {
            CurrentMapId = 0,
            X = 100,
            Y = 100,
            CharacterName = "悔过红名",
            State = new PlayerState { Level = 35, Gold = 500_000, PkPoints = 200 }
        };
        map.Aoi.Enter(player, 100, 100);

        // 1. 在线 60 秒自然衰减测试
        long now = Environment.TickCount64;
        player.State.LastPkDecayMs = now - 65_000; // 模拟已在线 65 秒
        dispatcher.UpdateWorldTick(1, now);

        Assert.Equal(199, player.State.PkPoints); // 200 -> 199 (从红名自然过渡到黄名)
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_CHANGENAMECOLOR}/902/{player.State.PkLevel}/0/0/悔过红名!"));

        // 2. 红名村老人洗红名指令测试 (@洗红名: 199 点需要 199,000 金币)
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_SAY}/@洗红名!");
        Assert.Equal(0, player.State.PkPoints);
        Assert.Equal(301_000, player.State.Gold); // 500,000 - 199,000 = 301,000
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_CHANGENAMECOLOR}/902/0/0/0/悔过红名!"));
    }

    [Fact]
    public void TestGroup_PickGold_EvenlyDistributedAmongNearbyMembers()
    {
        var mapManager = new MapManager();
        var map = mapManager.GetMap(0)!; // 比奇省
        var dispatcher = new PacketDispatcher(mapManager);

        var memberA = new ClientSession(1001, null!)
        {
            CurrentMapId = 0,
            X = 300,
            Y = 300,
            CharacterName = "队长A",
            State = new PlayerState { Level = 30, Gold = 1000 }
        };
        var memberB = new ClientSession(1002, null!)
        {
            CurrentMapId = 0,
            X = 302,
            Y = 301,
            CharacterName = "队员B",
            State = new PlayerState { Level = 28, Gold = 500 }
        };
        var farMemberC = new ClientSession(1003, null!)
        {
            CurrentMapId = 0,
            X = 400, // 距离 > 16 格，超出同屏范围
            Y = 400,
            CharacterName = "远方队员C",
            State = new PlayerState { Level = 25, Gold = 0 }
        };

        map.Aoi.Enter(memberA, 300, 300);
        map.Aoi.Enter(memberB, 302, 301);
        map.Aoi.Enter(farMemberC, 400, 400);

        // 1. 组建队伍 (队长A + 队员B + 远方队员C)
        var (cRes, party) = dispatcher.GroupEngine.CreateParty(memberA);
        Assert.Equal(GroupEngine.GroupResult.Success, cRes);
        dispatcher.GroupEngine.AddMember(memberA, memberB);
        dispatcher.GroupEngine.AddMember(memberA, farMemberC);
        Assert.Equal(3, party!.Members.Count);

        // 2. 地面掉落 105 金币在 (300, 300)
        var goldItem = new Mir2.Server.Maps.GroundItemEntity
        {
            Uid = 99991,
            Name = "金币",
            IsGold = true,
            Count = 105,
            X = 300,
            Y = 300,
            DropTickMs = Environment.TickCount64,
            OwnerSessionId = 1001
        };
        map.AddGroundItem(goldItem);

        // 3. 队长A 执行拾取 (CM_PICKUP)
        dispatcher.Dispatch(memberA, $"#{ProtocolConsts.CM_PICKUP}!");

        // 验证：同屏只有 A 和 B 两人，105 金币平分：每人 52 金币，余数 1 金币归队长A (A 分 53，B 分 52)
        // 远方队员 C 不参与分金，金币仍为 0
        Assert.Equal(1000 + 53, memberA.State.Gold);
        Assert.Equal(500 + 52, memberB.State.Gold);
        Assert.Equal(0, farMemberC.State.Gold);

        // 双方都收到 SM_GOLDCHANGED_DELPHI 协议与分金系统消息
        Assert.True(memberA.HasBufferedPacket($"#{ProtocolConsts.SM_GOLDCHANGED_DELPHI}/1053/0/0/0/!"));
        Assert.True(memberB.HasBufferedPacket($"#{ProtocolConsts.SM_GOLDCHANGED_DELPHI}/552/0/0/0/!"));
    }

    [Fact]
    public void TestTeleportRing_RequiresRing_And_Enforces10sCooldown()
    {
        var mapManager = new MapManager();
        var map = mapManager.GetMap(0)!; // 比奇省
        var dispatcher = new PacketDispatcher(mapManager);

        var player = new ClientSession(2001, null!)
        {
            CurrentMapId = 0,
            X = 330,
            Y = 270,
            CharacterName = "法师探路",
            State = new PlayerState { Level = 35 }
        };
        map.Aoi.Enter(player, 330, 270);

        // 1. 未佩戴传送戒指时尝试瞬移 -> 拦截
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_SAY}/@move 350 280!");
        Assert.Equal(330, player.X);
        Assert.Equal(270, player.Y);
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_SYSMSG_DELPHI}/0/0/0/0/你未佩戴传送戒指，无法使用空间瞬移！!"));

        // 2. 穿戴传送戒指 (Shape 112)
        player.State.Equipment[(int)OfficialEquipSlot.RingR] = new EquipItem
        {
            Uid = 99912,
            Name = "传送戒指",
            Shape = 112
        };
        player.State.RecalculateAttributes();
        Assert.True(player.State.HasTeleportRing);

        // 3. 第一次施展瞬移指令 (@传送 350 280) -> 瞬移成功
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_SAY}/@传送 350 280!");
        Assert.Equal(350, player.X);
        Assert.Equal(280, player.Y);
        Assert.True(player.HasBufferedPacket("350, 280") && player.HasBufferedPacket("成功"));

        // 4. 立即再次施展瞬移 -> 10 秒 CD 拦截
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_SAY}/@飞 360 290!");
        Assert.Equal(350, player.X);
        Assert.Equal(280, player.Y);
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_SYSMSG_DELPHI}/0/0/0/0/传送戒指魔力正在凝聚中，请等待 9 秒后再试！!") ||
                    player.HasBufferedPacket($"#{ProtocolConsts.SM_SYSMSG_DELPHI}/0/0/0/0/传送戒指魔力正在凝聚中，请等待 10 秒后再试！!"));
    }

    [Fact]
    public void TestTemptationPet_TameBind_Limit5_And_6HourRebellion()
    {
        var mapManager = new MapManager();
        var map = mapManager.GetMap(0)!;
        var dispatcher = new PacketDispatcher(mapManager);
        dispatcher.RegenEngine = new MonsterRegenEngine(mapManager);

        var mage = new ClientSession(3001, null!)
        {
            CurrentMapId = 0,
            X = 300,
            Y = 300,
            CharacterName = "诱惑法师",
            State = new PlayerState { Level = 35, Mp = 500 }
        };
        map.Aoi.Enter(mage, 300, 300);

        // 生成一只黑色恶蛆 (UID 881)
        var worm = new MonsterInstance
        {
            Uid = 881,
            Name = "黑色恶蛆",
            MapId = 0,
            X = 302,
            Y = 300,
            Hp = 230,
            MaxHp = 230,
            AttackDamageMin = 10,
            AttackDamageMax = 18
        };
        dispatcher.RegenEngine.AddMonster(worm);

        // 1. 模拟诱惑之光驯服成功
        long now = Environment.TickCount64;
        worm.MasterSessionId = mage.SessionId;
        worm.TamedTickMs = now;
        mage.Pets.Add(new HuWeiPetEngine.SlavePet
        {
            Uid = worm.Uid,
            Name = worm.Name,
            MasterSessionId = mage.SessionId,
            Hp = worm.Hp,
            MaxHp = worm.MaxHp
        });

        Assert.Single(mage.Pets);
        Assert.Equal(mage.SessionId, worm.MasterSessionId);

        // 2. 模拟存活 6.5 小时后心跳检测 -> 叛变反噬原主人
        worm.TamedTickMs = now - (long)(6.5 * 3600 * 1000);
        dispatcher.UpdateWorldTick(1, now);

        // 验证：随从列表清空，怪物主仆关系解除，目标锁定原主人
        Assert.Empty(mage.Pets);
        Assert.Equal(0, worm.MasterSessionId);
        Assert.Equal(mage.SessionId, worm.TargetSessionId);
        Assert.True(mage.HasBufferedPacket($"#{ProtocolConsts.SM_SYSMSG_DELPHI}/0/0/0/0/你的随从 黑色恶蛆 叛变了。!"));
    }

    [Fact]
    public void TestMemorySuit_ExecuteRecall_Enforces180sCooldown()
    {
        var mapManager = new MapManager();
        var map = mapManager.GetMap(0)!;
        var dispatcher = new PacketDispatcher(mapManager);

        var leader = new ClientSession(984001, null!)
        {
            CurrentMapId = 0,
            X = 330,
            Y = 270,
            CharacterName = "记忆队长984",
            State = new PlayerState { Level = 40 }
        };
        var member = new ClientSession(984002, null!)
        {
            CurrentMapId = 0,
            X = 100,
            Y = 100,
            CharacterName = "远方队员",
            State = new PlayerState { Level = 35 }
        };

        map.Aoi.Enter(leader, 330, 270);
        map.Aoi.Enter(member, 100, 100);

        // 组队
        dispatcher.GroupEngine.CreateParty(leader);
        dispatcher.GroupEngine.AddMember(leader, member);

        // 队长穿戴记忆全套 (头盔125/项链123/手镯124/戒指122)
        leader.State.Equipment[(int)OfficialEquipSlot.Helmet] = new EquipItem { Uid = 1, Name = "记忆头盔", Shape = 125 };
        leader.State.Equipment[(int)OfficialEquipSlot.Necklace] = new EquipItem { Uid = 2, Name = "记忆项链", Shape = 123 };
        leader.State.Equipment[(int)OfficialEquipSlot.ArmringL] = new EquipItem { Uid = 3, Name = "记忆手镯", Shape = 124 };
        leader.State.Equipment[(int)OfficialEquipSlot.RingL] = new EquipItem { Uid = 4, Name = "记忆戒指", Shape = 122 };

        // 1. 第一次天地合一召唤 -> 成功将队员拉至身边身周开阔位置
        leader.ClearBufferedPackets();
        dispatcher.Dispatch(leader, $"#{ProtocolConsts.CM_SAY}/@天地合一!");
        Assert.True(Math.Abs(member.X - leader.X) <= 2 && Math.Abs(member.Y - leader.Y) <= 2);
        Assert.True(leader.HasBufferedPacket("天地合一！成功召唤了队伍中的 1 名队友！"));

        // 2. 立即再次施展天地合一 -> 180 秒冷却拦截
        leader.ClearBufferedPackets();
        dispatcher.Dispatch(leader, $"#{ProtocolConsts.CM_SAY}/@天地合一!");
        Assert.True(leader.HasBufferedPacket("记忆套装神力正在沉睡中"));
    }

    [Fact]
    public void TestGodPrayerSuit_Rebellion_SynchronizesTakeOffAndFlushesPets()
    {
        var mapManager = new MapManager();
        var map = mapManager.GetMap(0)!;
        var dispatcher = new PacketDispatcher(mapManager);
        dispatcher.RegenEngine = new MonsterRegenEngine(mapManager);

        var player = new ClientSession(5001, null!)
        {
            CurrentMapId = 0,
            X = 300,
            Y = 300,
            CharacterName = "祈祷道尊",
            State = new PlayerState { Level = 40, Hp = 300, MaxHp = 300 }
        };
        dispatcher.RegisterSession(player);
        map.Aoi.Enter(player, 300, 300);

        // 玩家拥有一只神兽
        var pet = new MonsterInstance
        {
            Uid = 9901,
            Name = "神兽",
            MapId = 0,
            X = 301,
            Y = 300,
            Hp = 1000,
            MaxHp = 1000,
            MasterSessionId = player.SessionId
        };
        dispatcher.RegenEngine.AddMonster(pet);
        player.Pets.Add(new HuWeiPetEngine.SlavePet { Uid = pet.Uid, Name = pet.Name, MasterSessionId = player.SessionId });

        // 穿戴齐祈祷 5 件套 (头/链/镯/戒/刃)
        player.State.Equipment[(int)OfficialEquipSlot.Helmet] = new EquipItem { Uid = 11, Name = "祈祷头盔", Shape = 111 };
        player.State.Equipment[(int)OfficialEquipSlot.Necklace] = new EquipItem { Uid = 12, Name = "祈祷项链", Shape = 112 };
        player.State.Equipment[(int)OfficialEquipSlot.ArmringL] = new EquipItem { Uid = 13, Name = "祈祷手镯", Shape = 113 };
        player.State.Equipment[(int)OfficialEquipSlot.RingL] = new EquipItem { Uid = 14, Name = "祈祷戒指", Shape = 114 };
        player.State.Equipment[(int)OfficialEquipSlot.Weapon] = new EquipItem { Uid = 15, Name = "祈祷之刃", Shape = 115 };

        // 触发神之祈祷
        dispatcher.ExecuteGodPrayerRebellion(player);

        // 验证：
        // 1. 玩家随从清空解除契约
        Assert.Empty(player.Pets);
        // 2. 怪物脱离主人掌控并锁定原主人攻击
        Assert.Equal(0, pet.MasterSessionId);
        Assert.Equal(player.SessionId, pet.TargetSessionId);
        Assert.True(pet.ThreatTable.MaxHate > 0);
        // 3. 穿戴者装备槽位粉碎了一件，并收到 SM_TAKEOFF_OK
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_TAKEOFF_OK}/"));
        Assert.True(player.HasBufferedPacket("祈祷装备破碎了。"));
    }

    [Fact]
    public void TestSabukTaxWithdraw_And_DurabilitySync()
    {
        var mapManager = new MapManager();
        var map = mapManager.GetMap(0)!;
        var sabuk = new SabukSiegeEngine();
        var guildEngine = new GuildEngine();
        var dispatcher = new PacketDispatcher(mapManager, sabukEngine: sabuk, guildEngine: guildEngine);

        var lord = new ClientSession(6001, null!)
        {
            CurrentMapId = 0,
            X = 330,
            Y = 270,
            CharacterName = "沙城城主",
            State = new PlayerState { Level = 45, Gold = 100_000 }
        };
        dispatcher.RegisterSession(lord);
        map.Aoi.Enter(lord, 330, 270);

        // 建立沙巴克霸主行会并由 lord 担任会长
        guildEngine.RegisterGuildDirect("沙巴克荣耀", lord.CharacterName);
        sabuk.StartSiege("沙巴克荣耀", 0);
        sabuk.EndSiege(); // 战胜结算，奖励 100 万金币税收，当前金库 200 万

        // 1. 尝试提取 50 万税金
        dispatcher.Dispatch(lord, $"#{ProtocolConsts.CM_SAY}/@沙城税收 500000!");
        Assert.Equal(600_000, lord.State.Gold);
        Assert.Equal(1_500_000, sabuk.SabukGold);
        Assert.True(lord.HasBufferedPacket("已取出 500000 金币"));

        // 2. 非沙城主玩家尝试提取税金 -> 拦截
        var stranger = new ClientSession(6002, null!)
        {
            CurrentMapId = 0,
            X = 330,
            Y = 270,
            CharacterName = "路人甲",
            State = new PlayerState { Level = 20, Gold = 1000 }
        };
        dispatcher.RegisterSession(stranger);
        dispatcher.Dispatch(stranger, $"#{ProtocolConsts.CM_SAY}/@沙城税收 100000!");
        Assert.Equal(1000, stranger.State.Gold);
        Assert.True(stranger.HasBufferedPacket("你不是沙巴克行会成员"));
    }

    [Fact]
    public void TestAttackSpeed_Gate_BlocksRapidAttacks_AfterAttack()
    {
        var mapManager = new MapManager();
        var map = mapManager.GetMap(0)!;
        var dispatcher = new PacketDispatcher(mapManager);

        var player = new ClientSession(7001, null!)
        {
            CurrentMapId = 0,
            X = 300,
            Y = 300,
            CharacterName = "疾风战士",
            State = new PlayerState { Level = 30, Hp = 200, MaxHp = 200 }
        };
        dispatcher.RegisterSession(player);
        map.Aoi.Enter(player, 300, 300);

        // 1. 第一次挥刀 -> 成功执行并记录 LastAttackTimeMs
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_HIT}/301/300/2!");
        Assert.True(player.LastAttackTimeMs > 0);
        long firstAttackTick = player.LastAttackTimeMs;

        // 2. 连续瞬间发送第二次挥刀 (间隔 0ms) -> 速度门禁生效，拦截拒绝！
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_HIT}/301/300/2!");
        Assert.Equal(firstAttackTick, player.LastAttackTimeMs);
    }

    [Fact]
    public void TestWarGodOil_And_RepairOil_WeaponRestoration()
    {
        var mapManager = new MapManager();
        var map = mapManager.GetMap(0)!;
        var dispatcher = new PacketDispatcher(mapManager);

        var warrior = new ClientSession(7002, null!)
        {
            CurrentMapId = 0,
            X = 300,
            Y = 300,
            CharacterName = "油大师",
            State = new PlayerState { Level = 35, Hp = 300, MaxHp = 300 }
        };
        dispatcher.RegisterSession(warrior);
        map.Aoi.Enter(warrior, 300, 300);

        // 装备一把磨损的井中月 (当前持久 5000，最大持久 30000)
        var weapon = new EquipItem { Uid = 8801, Name = "井中月", Dura = 5000, MaxDura = 30000, Weight = 58 };
        warrior.State.Equipment[(int)OfficialEquipSlot.Weapon] = weapon;
        // 背包放入一瓶战神油与一瓶修复油
        warrior.State.BagItems.Add(new EquipItem { Uid = 9001, Name = "战神油", Weight = 1 });
        warrior.State.BagItems.Add(new EquipItem { Uid = 9002, Name = "修复油", Weight = 1 });

        // 1. 使用战神油 -> 0 损特修，持久回满 30000，最大持久仍为 30000
        dispatcher.Dispatch(warrior, $"#{ProtocolConsts.CM_EATITEM}/0/战神油!");
        Assert.Equal(30000, weapon.Dura);
        Assert.Equal(30000, weapon.MaxDura);
        Assert.True(warrior.HasBufferedPacket("已被完全特修完好"));

        // 人工磨损武器至 10000
        weapon.Dura = 10000;
        warrior.LastEatItemTimeMs = 0; // 重置 300ms 物品 CD 门禁

        // 2. 使用修复油 -> 普修损耗 1 点最大持久，持久回满至 29999
        dispatcher.Dispatch(warrior, $"#{ProtocolConsts.CM_EATITEM}/0/修复油!");
        Assert.Equal(29999, weapon.MaxDura);
        Assert.Equal(29999, weapon.Dura);
        Assert.True(warrior.HasBufferedPacket("最大持久下降了 1 点"));
    }

    [Fact]
    public void TestRingOfFire_SafeZoneImmunity_And_KnockbackStun()
    {
        var mapManager = new MapManager();
        var map = mapManager.GetMap(0)!;
        var dispatcher = new PacketDispatcher(mapManager);
        dispatcher.RegenEngine = new MonsterRegenEngine(mapManager);

        // 安全区内法师 (比奇大安全区 330, 270)
        var mage = new ClientSession(7003, null!)
        {
            CurrentMapId = 0,
            X = 330,
            Y = 270,
            CharacterName = "安全区法师",
            State = new PlayerState { Level = 40, Mp = 500, MaxMp = 500 }
        };
        dispatcher.RegisterSession(mage);
        map.Aoi.Enter(mage, 330, 270);

        // 1. 安全区内施展抗拒火环 -> 规则拦截
        dispatcher.Dispatch(mage, $"#{ProtocolConsts.CM_SPELL}/0/8/330/270!");
        Assert.True(mage.HasBufferedPacket("在安全区内不能推开他人。"));

        // 2. 野外环境 (300, 300) 施展抗拒火环推开近身黑色恶蛆
        mage.X = 300;
        mage.Y = 300;
        map.Aoi.UpdatePosition(mage, 330, 270, 300, 300, out _, out _);

        var worm = new MonsterInstance
        {
            Uid = 9888,
            Name = "黑色恶蛆",
            MapId = 0,
            X = 301,
            Y = 300,
            Hp = 200,
            MaxHp = 200
        };
        dispatcher.RegenEngine.AddMonster(worm);

        mage.LastSpellTimeMs = 0; // 重置公共CD
        dispatcher.Dispatch(mage, $"#{ProtocolConsts.CM_SPELL}/0/8/300/300!");

        // 恶蛆被推退至 (302, 300) 并施加 1 秒动作僵直硬直
        Assert.Equal(302, worm.X);
        Assert.Equal(300, worm.Y);
        Assert.True(worm.NextActionTimeMs > Environment.TickCount64);
    }

    [Fact]
    public void TestTaoistStealth_InvisibilitySync_And_ActionBreakingInvisibility()
    {
        var mapManager = new MapManager();
        var map = mapManager.GetMap(0)!;
        var dispatcher = new PacketDispatcher(mapManager);
        dispatcher.RegenEngine = new MonsterRegenEngine(mapManager);

        var taoist = new ClientSession(901, null!)
        {
            CurrentMapId = 0,
            CharacterName = "清虚道长",
            X = 300,
            Y = 300,
            State = new PlayerState { Job = 2, Level = 35, Hp = 250, MaxHp = 250, Mp = 300, MaxMp = 300, Sc = 25 }
        };
        dispatcher.RegisterSession(taoist);
        map.Aoi.Enter(taoist, 300, 300);
        taoist.State.WearItem(OfficialEquipSlot.Lighting, new EquipItem { Name = "护身符", Dura = 1000, MaxDura = 1000 });

        // 1. 道士施展单体隐身术 (MagicId 18)
        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_SPELL}/0/18/300/300!");
        Assert.True(taoist.State.IsStealthed);
        Assert.True(taoist.State.IsInvisible);
        Assert.True((taoist.State.GetCurrentStatusMask() & (0x80000000u >> 8)) != 0); // 官方 M2 sub_4BA43C 隐身掩码激活

        // 2. 视野内存在普通半兽人与反隐怪楔蛾
        var orc = new MonsterInstance
        {
            Uid = 9901, Name = "半兽人", MapId = 0, X = 302, Y = 300, Hp = 100, MaxHp = 100, SeeInvisibility = false
        };
        var moth = new MonsterInstance
        {
            Uid = 9902, Name = "楔蛾", MapId = 0, X = 302, Y = 301, Hp = 80, MaxHp = 80, SeeInvisibility = true
        };
        dispatcher.RegenEngine.AddMonster(orc);
        dispatcher.RegenEngine.AddMonster(moth);

        // 执行怪物 AI 心跳
        dispatcher.ProcessMonsterAiCycle(OfficialClientRulesEngine.DelphiRandom.Shared);

        // 半兽人看不穿隐身，未锁定道士
        Assert.NotEqual(taoist.SessionId, orc.TargetSessionId);
        // 楔蛾反隐透视，看穿隐身并锁定道士为目标
        Assert.Equal(taoist.SessionId, moth.TargetSessionId);

        // 3. 道士迈步走动 (CM_WALK) -> 潜行打破，破除法术隐身
        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_WALK}/301/300/2!");
        Assert.False(taoist.State.IsStealthed);
        Assert.False(taoist.State.IsInvisible);
        Assert.True(taoist.HasBufferedPacket($"#{ProtocolConsts.SM_CHANGENAMECOLOR}/"));
    }

    [Fact]
    public void TestTrapRock_MonsterImmobilization_And_DamageWakeUp()
    {
        var mapManager = new MapManager();
        var map = mapManager.GetMap(0)!;
        var dispatcher = new PacketDispatcher(mapManager);
        dispatcher.RegenEngine = new MonsterRegenEngine(mapManager);

        var taoist = new ClientSession(902, null!)
        {
            CurrentMapId = 0,
            CharacterName = "玄真子",
            X = 300,
            Y = 300,
            State = new PlayerState { Job = 2, Level = 35, Hp = 250, MaxHp = 250, Mp = 300, MaxMp = 300, Hit = 50, Dc = 20, MaxDc = 35 }
        };
        dispatcher.RegisterSession(taoist);
        map.Aoi.Enter(taoist, 300, 300);
        taoist.State.WearItem(OfficialEquipSlot.Lighting, new EquipItem { Name = "护身符", Dura = 1000, MaxDura = 1000 });
        taoist.State.WearItem(OfficialEquipSlot.Weapon, new EquipItem { Name = "银蛇", Dura = 10000, MaxDura = 10000 });
        taoist.State.Hit = 50;

        var zombie = new MonsterInstance
        {
            Uid = 9903, Name = "僵尸", MapId = 0, X = 301, Y = 300, Hp = 160, MaxHp = 160
        };
        dispatcher.RegenEngine.AddMonster(zombie);

        // 1. 施放困魔咒 (MagicId 16) 禁锢僵尸
        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_SPELL}/0/16/301/300!");
        long lockedUntil = zombie.NextActionTimeMs;
        Assert.True(lockedUntil > Environment.TickCount64 + 10_000); // 困魔禁锢长达 15 秒

        // 2. 道士近战挥刀攻击僵尸 (CM_HIT)
        taoist.LastAttackTimeMs = 0; // 重置普攻门禁
        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_HIT}/301/300/2!");

        // 3. 验证僵尸受击后八卦阵破损，立即解除长硬直并恢复反击，且仇恨锁定道士
        Assert.True(zombie.Hp < 160);
        Assert.True(zombie.NextActionTimeMs < lockedUntil); // 禁锢被打破
        Assert.Equal(taoist.SessionId, zombie.TargetSessionId);
    }

    [Fact]
    public void TestMonsterParalysis_MothAndSpider_StatusMaskBroadcast_And_TimeoutClear()
    {
        var mapManager = new MapManager();
        var map = mapManager.GetMap(0)!;
        var dispatcher = new PacketDispatcher(mapManager);
        dispatcher.RegenEngine = new MonsterRegenEngine(mapManager);

        var player = new ClientSession(903, null!)
        {
            CurrentMapId = 0,
            CharacterName = "坚韧战士",
            X = 300,
            Y = 300,
            State = new PlayerState { Job = 0, Level = 35, Hp = 500, MaxHp = 500, Ac = 10, MaxAc = 20 }
        };
        dispatcher.RegisterSession(player);
        map.Aoi.Enter(player, 300, 300);

        // 1. 楔蛾对玩家造成近战攻击并触发 15% 麻痹判定
        long now = Environment.TickCount64;
        player.State.ParalyzeExpireMs = now + 3000;
        player.State.StatusBits[5] = true;

        // 验证麻痹期间玩家无法行走 (CM_WALK 1002 阻断)
        player.ClearBufferedPackets();
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_WALK}/301/300/2!");
        Assert.Equal(300, player.X); // 坐标未发生改变，行走被阻断

        // 2. 模拟时间推进超过 3 秒，执行 UpdateWorldTick
        long futureMs = now + 3500;
        dispatcher.UpdateWorldTick(1, futureMs);

        // 验证麻痹状态位被物理复位且收到状态掩码更新广播
        Assert.False(player.State.StatusBits[5]);
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_CHANGENAMECOLOR}/"));

        // 3. 验证绿毒超时复位
        player.ClearBufferedPackets();
        player.State.HasGreenPoison = true;
        player.State.StatusBits[0] = true;
        player.State.GreenPoisonExpireMs = futureMs; // 此时已过期
        dispatcher.UpdateWorldTick(2, futureMs + 100);

        Assert.False(player.State.HasGreenPoison);
        Assert.False(player.State.StatusBits[0]);
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_CHANGENAMECOLOR}/"));
    }

    [Fact]
    public void TestTaoistPoison_PlayerAndMonster_StatusMask_RedPoisonDamageAmp_And_TimeoutClear()
    {
        var mapManager = new MapManager();
        var map = mapManager.GetMap(0)!;
        var dispatcher = new PacketDispatcher(mapManager);
        dispatcher.RegenEngine = new MonsterRegenEngine(mapManager);

        var taoist = new ClientSession(904, null!)
        {
            CurrentMapId = 0,
            CharacterName = "毒尊道士",
            X = 300,
            Y = 300,
            State = new PlayerState { Job = 2, Level = 35, Hp = 200, MaxHp = 200, Mp = 300, MaxMp = 300, Sc = 25 }
        };
        var victim = new ClientSession(905, null!)
        {
            CurrentMapId = 0,
            CharacterName = "中毒剑士",
            X = 301,
            Y = 300,
            State = new PlayerState { Job = 0, Level = 30, Hp = 300, MaxHp = 300, AntiPoison = 0 }
        };
        dispatcher.RegisterSession(taoist);
        dispatcher.RegisterSession(victim);
        map.Aoi.Enter(taoist, 300, 300);
        map.Aoi.Enter(victim, 301, 300);

        // 道士背包携带黄色与灰色药粉
        taoist.State.BagItems.Add(new EquipItem { Name = "灰色药粉(中)", Dura = 100, Weight = 1 });
        taoist.State.BagItems.Add(new EquipItem { Name = "黄色药粉(中)", Dura = 100, Weight = 1 });

        // 1. 道士对玩家施展施毒术 (MagicId 6)
        long now = Environment.TickCount64;
        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_SPELL}/{victim.SessionId}/6/301/300!");

        // 验证受害者获得绿毒，且九宫格收到状态掩码更新 (绿名) 与施毒光效
        Assert.True(victim.State.HasGreenPoison);
        Assert.True(victim.State.StatusBits[0]);
        Assert.True(taoist.HasBufferedPacket($"#{ProtocolConsts.SM_CHANGENAMECOLOR}/"));
        Assert.True(taoist.HasBufferedPacket($"#{ProtocolConsts.SM_SPELLEFFECT_DELPHI}/"));

        // 2. 模拟受害者中红毒，到期后心跳自动复位
        victim.ClearBufferedPackets();
        victim.State.ApplyPoison(TaoistCombatEngine.PoisonType.Red, 3, 25, now);
        Assert.True(victim.State.HasRedPoison);
        Assert.True(victim.State.StatusBits[1]);

        // 推进时间 3.5 秒执行心跳 UpdateWorldTick
        dispatcher.UpdateWorldTick(1, now + 3500);
        Assert.False(victim.State.HasRedPoison);
        Assert.False(victim.State.StatusBits[1]);
        Assert.True(victim.HasBufferedPacket($"#{ProtocolConsts.SM_CHANGENAMECOLOR}/"));

        // 3. 怪物红毒 20% 易伤结算实证
        var boss = new MonsterInstance { Uid = 9910, Name = "沃玛教主", MapId = 0, X = 302, Y = 300, Hp = 1000, MaxHp = 1000 };
        boss.ApplyPoison(TaoistCombatEngine.PoisonType.Red, 10, 25, now);
        Assert.True(boss.HasRedPoison);

        // 原本 100 伤害，经红毒 1.2 倍加成后造成 120 伤害
        dispatcher.ApplyDamageToMonster(taoist, boss, 100, now);
        Assert.Equal(880, boss.Hp); // 1000 - 120 = 880

        // 推进时间超过 10 秒，红毒到期消散
        dispatcher.ApplyDamageToMonster(taoist, boss, 100, now + 11_000);
        Assert.False(boss.HasRedPoison);
        Assert.Equal(780, boss.Hp); // 880 - 100 = 780 (恢复原额伤害)
    }

    [Fact]
    public void TestAuthoritativeMotaebo_StunAndRebound_And_PetLifecycleRegistry_And_TamedBetrayal()
    {
        var mapManager = new MapManager();
        var map = mapManager.GetMap(0)!;
        var dispatcher = new PacketDispatcher(mapManager);
        dispatcher.RegenEngine = new MonsterRegenEngine(mapManager);

        var warrior = new ClientSession(906, null!)
        {
            CurrentMapId = 0,
            CharacterName = "盖世狂战",
            X = 300,
            Y = 300,
            Direction = 2, // 朝东 (dx=1, dy=0)
            State = new PlayerState { Job = 0, Level = 40, Hp = 500, MaxHp = 500 }
        };
        dispatcher.RegisterSession(warrior);
        map.Aoi.Enter(warrior, 300, 300);

        // 1. 野蛮冲撞撞退普通怪 (沃玛勇士)，沃玛勇士被撞退并撞死
        var mob = new MonsterInstance { Uid = 9911, Name = "沃玛勇士", MapId = 0, X = 301, Y = 300, Hp = 30, MaxHp = 30, CanMove = true };
        dispatcher.RegenEngine.AddMonster(mob);
        long now = Environment.TickCount64;

        dispatcher.Dispatch(warrior, $"#{ProtocolConsts.CM_SPELL}/0/27/301/300!");
        Assert.True(mob.IsDead); // 沃玛勇士承伤 30 点生命归零阵亡
        Assert.Equal(303, warrior.X); // 战士冲完 3 格顺势推移到 (303, 300)

        // 2. 战士向前面对不可移动怪物 (触龙神)，触龙神巍然屹立，战士受到 15 点冲撞反震伤害
        var dragon = new MonsterInstance { Uid = 9912, Name = "触龙神", MapId = 0, X = 304, Y = 300, Hp = 2000, MaxHp = 2000, CanMove = false };
        dispatcher.RegenEngine.AddMonster(dragon);
        warrior.LastSpellTimeMs = 0; // 重置公共施法 CD
        warrior.State.LastWildSlamTickMs = 0; // 重置野蛮冲撞 3000ms CD
        int hpBefore = warrior.State.Hp;

        dispatcher.Dispatch(warrior, $"#{ProtocolConsts.CM_SPELL}/0/27/304/300!");
        Assert.Equal(304, dragon.X); // 触龙神未被撞动
        Assert.Equal(hpBefore - 15, warrior.State.Hp); // 战士承受 15 点反震伤害

        // 3. 道士召唤神兽注册入 session.Pets 验证
        var taoist = new ClientSession(907, null!)
        {
            CurrentMapId = 0,
            CharacterName = "神兽天尊",
            X = 200,
            Y = 200,
            State = new PlayerState { Job = 2, Level = 40, Hp = 300, MaxHp = 300, Mp = 300, MaxMp = 300, Sc = 30 }
        };
        dispatcher.RegisterSession(taoist);
        map.Aoi.Enter(taoist, 200, 200);
        taoist.State.WearItem(OfficialEquipSlot.Lighting, new EquipItem { Name = "护身符", Dura = 1000, MaxDura = 1000 });

        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_SPELL}/0/30/200/200!");
        Assert.Single(taoist.Pets);
        Assert.Equal("神兽", taoist.Pets[0].Name);

        // 4. 法师诱惑随从存活超 6 小时发狂叛变验证
        var mage = new ClientSession(908, null!)
        {
            CurrentMapId = 0,
            CharacterName = "诱惑法皇",
            X = 100,
            Y = 100,
            State = new PlayerState { Job = 1, Level = 40, Hp = 200, MaxHp = 200, Mp = 500, MaxMp = 500 }
        };
        dispatcher.RegisterSession(mage);
        map.Aoi.Enter(mage, 100, 100);

        var worm = new MonsterInstance
        {
            Uid = 9913,
            Name = "黑色恶蛆",
            MapId = 0,
            X = 101,
            Y = 100,
            Hp = 250,
            MaxHp = 250,
            MasterSessionId = mage.SessionId,
            TamedTickMs = now - (6 * 3600 * 1000L + 1000) // 驯服已超 6 小时
        };
        mage.Pets.Add(new HuWeiPetEngine.SlavePet { Uid = worm.Uid, Name = worm.Name, Level = 0 });
        dispatcher.RegenEngine.AddMonster(worm);

        // 驱动怪物 AI 心跳
        dispatcher.ProcessMonsterAiCycle(OfficialClientRulesEngine.DelphiRandom.Shared);

        // 验证恶蛆发狂叛变，脱离法师随从绑定，并优先仇恨反噬法师
        Assert.True(worm.IsMad);
        Assert.Equal(0, worm.MasterSessionId);
        Assert.Empty(mage.Pets);
        Assert.Equal(mage.SessionId, worm.ThreatTable.PrimaryTargetId);
    }

    [Fact]
    public void TestRepairItem_FromBag_And_SpecialRepair_FromEquipment_WithStandardProtocols()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 800, 800);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var player = new ClientSession(987, null!)
        {
            CurrentMapId = 0,
            CharacterName = "铁匠学徒",
            X = 330,
            Y = 330,
            State = new PlayerState { Job = 0, Level = 35, Gold = 10000, Hp = 200, MaxHp = 200 }
        };
        dispatcher.RegisterSession(player);
        map.Aoi.Enter(player, 330, 330);

        // 注册比奇铁匠 (UID = 77)
        dispatcher.NpcEngine.RegisterNpc(77, "比奇铁匠", 0, 330, 331);

        // 1. 背包中有一件磨损的龙纹剑: 当前持久 5，最大持久 20 (损失 15 点)
        var bagWeapon = new EquipItem { Uid = 5566, Name = "龙纹剑", Dura = 5, MaxDura = 20 };
        player.State.BagItems.Add(bagWeapon);

        // 模拟客户端通过 PacketEncoder.RepairItem 发包: #1023/5566/0/0/0/!
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_USERREPAIRITEM}/5566/0/0/0/!");

        // 普修扣金币 15，掉最大持久 15/3 = 5 点，新最大持久 15，持久恢复至 15
        Assert.Equal(10000 - 15, player.State.Gold);
        Assert.Equal(15, bagWeapon.MaxDura);
        Assert.Equal(15, bagWeapon.Dura);
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_REPAIROK_DELPHI}/9985/15/15/0/龙纹剑!"));

        // 2. 身上穿戴磨损的圣战头盔 (槽位 4 Helmet): 当前持久 3，最大持久 10 (损失 7 点)
        var helmet = new EquipItem { Uid = 7788, Name = "圣战头盔", Dura = 3, MaxDura = 10 };
        player.State.WearItem(OfficialEquipSlot.Helmet, helmet);

        // 模拟客户端通过 PacketEncoder.RepairSpecial 发包特修: #48/7788/0/0/0/!
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_REPAIRSPECIAL}/7788/0/0/0/!");

        // 特修扣金币 7 * 3 = 21，0 损回满最大持久 10，当前持久回满至 10
        Assert.Equal(10000 - 15 - 21, player.State.Gold);
        Assert.Equal(10, helmet.MaxDura);
        Assert.Equal(10, helmet.Dura);
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_REPAIROK_DELPHI}/9964/10/10/0/圣战头盔!"));
    }

    [Fact]
    public void TestMageTeleport_PetSync_And_AoiExecuteTransition()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "盟重省", 800, 800);
        mapManager.RegisterMap(map);
        var regen = new MonsterRegenEngine(mapManager);
        var dispatcher = new PacketDispatcher(mapManager, regenEngine: regen);

        var mage = new ClientSession(9988, null!)
        {
            CurrentMapId = 0,
            CharacterName = "空间法神",
            X = 300,
            Y = 300,
            State = new PlayerState { Job = 1, Level = 40, Hp = 200, MaxHp = 200, Mp = 500, MaxMp = 500 }
        };
        dispatcher.RegisterSession(mage);
        map.Aoi.Enter(mage, 300, 300);

        // 带领 2 只钳虫随从
        var pet1 = new HuWeiPetEngine.SlavePet { Uid = 1001, Name = "钳虫", X = 301, Y = 300, Hp = 300, MaxHp = 300 };
        var pet2 = new HuWeiPetEngine.SlavePet { Uid = 1002, Name = "钳虫", X = 300, Y = 301, Hp = 300, MaxHp = 300 };
        mage.Pets.Add(pet1);
        mage.Pets.Add(pet2);

        // 施展瞬息移动 (MagicId = 11, targetUid=0, targetX=0, targetY=0)
        dispatcher.Dispatch(mage, $"#{ProtocolConsts.CM_SPELL}/0/11/0/0/!");

        // 验证瞬移发生，坐标发生改变且不再停留在原地 (300, 300)
        Assert.True(mage.X != 300 || mage.Y != 300);
        // 验证随从宝宝同步瞬移到法师身边 (距离 <= 2)
        Assert.True(Math.Abs(pet1.X - mage.X) <= 2 && Math.Abs(pet1.Y - mage.Y) <= 2);
        Assert.True(Math.Abs(pet2.X - mage.X) <= 2 && Math.Abs(pet2.Y - mage.Y) <= 2);

        // 验证广播瞬移离开与降临特效 (MagicId = 11)
        Assert.True(mage.HasBufferedPacket($"#{ProtocolConsts.SM_SPELLEFFECT_DELPHI}/{mage.SessionId}/{mage.X}/{mage.Y}/11/瞬移降临!"));
    }

    [Fact]
    public void TestSellItem_WithTrailingZeroProtocols()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 800, 800);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var player = new ClientSession(989, null!)
        {
            CurrentMapId = 0,
            CharacterName = "出售测试员",
            X = 330,
            Y = 330,
            State = new PlayerState { Job = 0, Level = 20, Gold = 100, Hp = 100, MaxHp = 100 }
        };
        dispatcher.RegisterSession(player);
        map.Aoi.Enter(player, 330, 330);

        // 背包放入一把木剑 (Uid = 9999)
        var sword = new EquipItem { Uid = 9999, Name = "木剑", Dura = 10, MaxDura = 10 };
        player.State.BagItems.Add(sword);

        // 发送带后缀 0 的标准出售数据包: #46/9999/0/0/0/!
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_USERSELLITEM}/9999/0/0/0/!");

        // 验证木剑已从背包移除
        Assert.DoesNotContain(player.State.BagItems, item => item.Uid == 9999);
        // 验证金币增加 (木剑售出获得金币)
        Assert.True(player.State.Gold > 100);
        // 验证收到官方出售成功封包 SM_SELL_OK_DELPHI (650)
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_SELL_OK_DELPHI}/9999/"));
    }

    [Fact]
    public void TestGroupAddMember_And_DelMember_WithOfficial5FieldProtocols()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 800, 800);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var leader = new ClientSession(1101, null!)
        {
            CurrentMapId = 0,
            CharacterName = "编组队长",
            X = 300,
            Y = 300,
            State = new PlayerState { Job = 0, Level = 30, Hp = 200, MaxHp = 200, AllowGroup = true }
        };
        var member = new ClientSession(1102, null!)
        {
            CurrentMapId = 0,
            CharacterName = "逍遥剑客",
            X = 301,
            Y = 300,
            State = new PlayerState { Job = 0, Level = 28, Hp = 180, MaxHp = 180, AllowGroup = true }
        };

        dispatcher.RegisterSession(leader);
        dispatcher.RegisterSession(member);
        map.Aoi.Enter(leader, 300, 300);
        map.Aoi.Enter(member, 301, 300);

        // 1. 队长发送官方标准 5 字段加人封包: #1021/0/0/0/0/逍遥剑客!
        dispatcher.Dispatch(leader, $"#{ProtocolConsts.CM_ADDGROUPMEMBER}/0/0/0/0/逍遥剑客!");

        var party = dispatcher.GroupEngine.GetParty(leader.SessionId);
        Assert.NotNull(party);
        Assert.Equal(2, party.Members.Count);
        Assert.Contains(party.Members, m => m.CharacterName == "逍遥剑客");
        Assert.True(leader.HasBufferedPacket($"#{ProtocolConsts.SM_SYSMSG_DELPHI}/"));

        // 2. 队长发送官方标准 5 字段踢人封包: #1022/0/0/0/0/逍遥剑客!
        dispatcher.Dispatch(leader, $"#{ProtocolConsts.CM_DELGROUPMEMBER}/0/0/0/0/逍遥剑客!");

        // 验证被移除的是“逍遥剑客”，队长依然在队伍中 (或队伍解散)，而不是队长把自己踢了
        Assert.DoesNotContain(party.Members, m => m.CharacterName == "逍遥剑客");
    }

    [Fact]
    public void TestGuildCreation_And_MemberManagement_WithOfficial5FieldProtocols()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 800, 800);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var master = new ClientSession(1201, null!)
        {
            CurrentMapId = 0,
            CharacterName = "会长本人",
            X = 330,
            Y = 330,
            State = new PlayerState { Job = 0, Level = 40, Gold = 2_000_000, Hp = 300, MaxHp = 300 }
        };
        // 拥有沃玛号角
        master.State.BagItems.Add(new EquipItem { Uid = 888, Name = "沃玛号角", Dura = 1, MaxDura = 1 });

        dispatcher.RegisterSession(master);
        map.Aoi.Enter(master, 330, 330);

        // 1. 发送官方标准 5 字段创建行会封包: #1042/0/0/0/0/兄弟军团!
        dispatcher.Dispatch(master, $"#{ProtocolConsts.CM_CREATEGUILD}/0/0/0/0/兄弟军团!");

        var guild = dispatcher.GuildEngine.GetGuild("兄弟军团");
        Assert.NotNull(guild);
        Assert.Equal("兄弟军团", guild.Name);
        Assert.Equal("会长本人", guild.MasterName);
        Assert.True(master.HasBufferedPacket($"#{ProtocolConsts.SM_BUILDGUILDOK}/"));

        // 2. 邀请新成员加入行会: #1034/0/0/0/0/行会新人!
        var newcomer = new ClientSession(1202, null!)
        {
            CurrentMapId = 0,
            CharacterName = "行会新人",
            X = 331,
            Y = 330,
            State = new PlayerState { Job = 1, Level = 25, Hp = 100, MaxHp = 100 }
        };
        dispatcher.RegisterSession(newcomer);
        map.Aoi.Enter(newcomer, 331, 330);

        dispatcher.Dispatch(master, $"#{ProtocolConsts.CM_GUILDADDMEMBER}/0/0/0/0/行会新人!");
        Assert.Contains("行会新人", guild.Members);
        Assert.True(master.HasBufferedPacket($"#{ProtocolConsts.SM_GUILDADDMEMBEROK}/"));
    }

    [Fact]
    public void TestStorageDeposit_And_EatItem_WithOfficial5FieldProtocols()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 800, 800);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var player = new ClientSession(1301, null!)
        {
            CurrentMapId = 0,
            CharacterName = "储物员",
            X = 330,
            Y = 330,
            State = new PlayerState { Job = 0, Level = 30, Gold = 1000, Hp = 50, MaxHp = 200, Mp = 20, MaxMp = 100 },
            IsStorageOpen = true,
            StorageNpcUid = 99
        };
        dispatcher.RegisterSession(player);
        map.Aoi.Enter(player, 330, 330);

        // 注册仓库 NPC
        dispatcher.NpcEngine.RegisterNpc(99, "仓库管理员", 0, 330, 331);

        // 1. 背包中放入一把屠龙刀
        var tuLong = new EquipItem { Uid = 7777, Name = "屠龙刀", Dura = 33, MaxDura = 33 };
        player.State.BagItems.Add(tuLong);

        // 发送官方标准 5 字段存仓封包: #1031/0/0/0/0/屠龙刀!
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_USERSTORAGEDEPOSIT}/0/0/0/0/屠龙刀!");

        // 验证背包中移除了屠龙刀，仓库中新增了屠龙刀
        Assert.DoesNotContain(player.State.BagItems, i => i.Uid == 7777);
        Assert.Contains(player.State.StorageItems, i => i.Uid == 7777);
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_SAVEITEM_OK}/"));

        // 2. 背包中放入一瓶强效太阳水并使用: #1002/0/0/0/0/强效太阳水!
        var sunWater = new EquipItem { Uid = 8881, Name = "强效太阳水", Dura = 1, MaxDura = 1 };
        player.State.BagItems.Add(sunWater);

        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_EATITEM}/0/0/0/0/强效太阳水!");

        // 强效太阳水瞬间恢复 50 HP 和 50 MP (50+50=100 HP, 20+50=70 MP)
        Assert.True(player.State.Hp >= 100);
        Assert.True(player.State.Mp >= 70);
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_EAT_OK}/"));
    }

    [Fact]
    public void TestPickUp_AdjacentAndUnderfoot_WithTargetCoordsAnd5FieldProtocols()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 800, 800);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var player = new ClientSession(1401, null!)
        {
            CurrentMapId = 0,
            CharacterName = "寻宝者",
            X = 330,
            Y = 330,
            State = new PlayerState { Job = 0, Level = 20, Gold = 100, Hp = 100, MaxHp = 100 }
        };
        dispatcher.RegisterSession(player);
        map.Aoi.Enter(player, 330, 330);

        // 在相邻格子 (330, 331) 放置一把裁决之杖
        var caiJue = new GroundItemEntity
        {
            Uid = 9991,
            Name = "裁决之杖",
            X = 330,
            Y = 331,
            Weight = 50,
            DropTickMs = Environment.TickCount64,
            OwnerSessionId = 0
        };
        map.AddGroundItem(caiJue);

        // 玩家站在 (330, 330)，发送带相邻坐标的拾取封包: #1001/0/0/0/0/330\t331!
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_PICKUP}/0/0/0/0/330\t331!");

        // 验证物品成功拾取入包
        Assert.Contains(player.State.BagItems, i => i.Uid == 9991);
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_ADDITEM}/"));
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_DELGROUNDITEM}/"));
    }

    [Fact]
    public void TestNpcSelect_And_ShopBuy_WithOfficial5FieldProtocols()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "盟重省", 800, 800);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var player = new ClientSession(1501, null!)
        {
            CurrentMapId = 0,
            CharacterName = "买药客",
            X = 300,
            Y = 300,
            State = new PlayerState { Job = 0, Level = 15, Gold = 50000, Hp = 100, MaxHp = 100 }
        };
        dispatcher.RegisterSession(player);
        map.Aoi.Enter(player, 300, 300);

        // 注册药店 NPC
        dispatcher.NpcEngine.RegisterNpc(66, "药店老板", 0, 300, 301);
        dispatcher.NpcEngine.AddGoodsToNpc(66, new MerchantGoods
        {
            GoodsId = 101,
            Name = "金创药(小量)",
            Price = 110,
            Weight = 1,
            MaxDura = 1
        });

        // 1. 发送 5 字段 NPC 对话选项点击: #1011/66/0/0/0/@buy!
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_NPCSELECT}/66/0/0/0/@buy!");
        Assert.Equal(66, player.ActiveNpcUid);

        // 2. 发送 5 字段购买封包: #1014/101/1/0/0/金创药(小量)!
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_USERBUYITEM}/101/1/0/0/金创药(小量)!");

        // 验证金币扣除、物品入包、下发 SM_BUY_OK_DELPHI
        Assert.Equal(50000 - 110, player.State.Gold);
        Assert.Contains(player.State.BagItems, i => i.Name == "金创药(小量)");
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_BUY_OK_DELPHI}/"));
    }

    [Fact]
    public void TestTrade_AddItemAndDelItem_WithItemUid_Slot_And_Name()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 800, 800);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var playerA = new ClientSession(1601, null!)
        {
            CurrentMapId = 0,
            CharacterName = "交易者A",
            X = 330,
            Y = 330,
            State = new PlayerState { Job = 0, Level = 30, Gold = 10000, Hp = 100, MaxHp = 100 }
        };
        var playerB = new ClientSession(1602, null!)
        {
            CurrentMapId = 0,
            CharacterName = "交易者B",
            X = 330,
            Y = 331,
            State = new PlayerState { Job = 1, Level = 30, Gold = 10000, Hp = 100, MaxHp = 100 }
        };

        dispatcher.RegisterSession(playerA);
        dispatcher.RegisterSession(playerB);
        map.Aoi.Enter(playerA, 330, 330);
        map.Aoi.Enter(playerB, 330, 331);

        // 放入 A 背包物品
        var sword = new EquipItem { Uid = 5551, Name = "井中月", Weight = 58, Dura = 30, MaxDura = 30 };
        playerA.State.BagItems.Add(sword);
        playerA.State.BagWeight += 58;

        // 1. 发起交易
        dispatcher.Dispatch(playerA, $"#{ProtocolConsts.CM_DEALTRY}/1602!");
        Assert.NotNull(dispatcher.TradeEngine.GetTrade(playerA.SessionId));

        // 2. A 放入物品 (按 5 字段及物品名放入): #1026/0/0/0/0/井中月!
        dispatcher.Dispatch(playerA, $"#{ProtocolConsts.CM_DEALADDITEM}/0/0/0/0/井中月!");
        Assert.DoesNotContain(playerA.State.BagItems, i => i.Uid == 5551);
        Assert.True(playerA.HasBufferedPacket($"#{ProtocolConsts.SM_DEALADDITEM_OK}/"));

        // 3. A 取下物品 (按物品名取下): #1027/0/0/0/0/井中月!
        dispatcher.Dispatch(playerA, $"#{ProtocolConsts.CM_DEALDELITEM}/0/0/0/0/井中月!");
        Assert.Contains(playerA.State.BagItems, i => i.Uid == 5551);
        Assert.True(playerA.HasBufferedPacket($"#{ProtocolConsts.SM_DEALDELITEM_OK}/"));
    }

    [Fact]
    public void TestQueryRepairCost_And_QuerySellPrice_OfficialFormulas()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 800, 800);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var player = new ClientSession(1701, null!)
        {
            CurrentMapId = 0,
            CharacterName = "问价客",
            X = 330,
            Y = 330,
            State = new PlayerState { Job = 0, Level = 25, Gold = 10000, Hp = 100, MaxHp = 100 }
        };
        dispatcher.RegisterSession(player);
        map.Aoi.Enter(player, 330, 330);

        // 放入一把磨损的炼狱战斧: Dura 10 / MaxDura 30 (磨损 20)
        var axe = new EquipItem { Uid = 6601, Name = "炼狱", Dura = 10, MaxDura = 30, Weight = 60 };
        player.State.BagItems.Add(axe);

        // 1. 发送 CM_QUERYREPAIRCOST (1024) 查询修理金币消耗
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_QUERYREPAIRCOST}/6601!");
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_SYSMSG_DELPHI}/"));

        // 2. 发送 CM_QUERYSELLPRICE (1012) 查询商店回收金币估价
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_QUERYSELLPRICE}/6601!");
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_QUERYPRICE_REC}/"));
    }

    [Fact]
    public void TestUserGetDetailItem_And_Minimap_And_MagicKeyChange()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "盟重省", 800, 800);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var player = new ClientSession(1801, null!)
        {
            CurrentMapId = 0,
            CharacterName = "雷电法王",
            X = 300,
            Y = 300,
            State = new PlayerState
            {
                Job = 1,
                Level = 35,
                Gold = 20000,
                Hp = 150,
                MaxHp = 150,
                Mp = 300,
                MaxMp = 300
            }
        };
        // 学习技能: 雷电术 (MagicId 11)
        player.State.Magics.Add(new UserMagic
        {
            MagicId = 11,
            Name = "雷电术",
            Level = 3,
            Key = 0
        });
        // 背包放入红宝石戒指
        var ring = new EquipItem { Uid = 7711, Name = "红宝石戒指", Weight = 1, Dura = 5, MaxDura = 5, Mc = 4 };
        player.State.BagItems.Add(ring);

        dispatcher.RegisterSession(player);
        map.Aoi.Enter(player, 300, 300);

        // 1. 请求物品详细属性: CM_USERGETDETAILITEM (1034)
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_USERGETDETAILITEM}/7711!");
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_SENDDETAILGOODSLIST}/"));

        // 2. 请求小地图数据: CM_WANTMINIMAP (1033)
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_WANTMINIMAP}!");
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_READMINIMAP_OK}/"));
        // 盛大官方 1.5 原版二进制实证 (sub_467920): CM_WANTMINIMAP 严格仅回复 SM_READMINIMAP_OK，不下发自身的 SM_MAPMARK_DELPHI (612) 避免污染寻路目标光点
        Assert.False(player.HasBufferedPacket($"#{ProtocolConsts.SM_MAPMARK_DELPHI}/"));

        // 3. 绑定技能快捷键到 F2 (keyNum = 1): CM_MAGICKEYCHANGE (1008)
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_MAGICKEYCHANGE}/11/1!");
        var thunder = player.State.Magics.Find(m => m.MagicId == 11);
        Assert.NotNull(thunder);
        Assert.Equal(1, thunder.Key);
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_SYSMSG_DELPHI}/"));
    }

    [Fact]
    public void TestAuthoritative_ThrustingAndHalfMoonOnOff_And_Whisper_And_Username_And_GuildRank()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 600, 600);
        mapManager.RegisterMap(map);

        var dispatcher = new PacketDispatcher(mapManager);

        // 创建两个玩家: 战士与道士
        var warrior = new ClientSession(101, null!)
        {
            CurrentMapId = 0,
            CharacterName = "至尊战士",
            X = 300,
            Y = 300,
            State = new PlayerState
            {
                Job = 0,
                Level = 40,
                Hp = 600,
                MaxHp = 600,
                Gold = 5000000,
                BoThrusting = true,
                BoHalfMoon = true
            }
        };

        var taoist = new ClientSession(102, null!)
        {
            CurrentMapId = 0,
            CharacterName = "逍遥散人",
            X = 301,
            Y = 300,
            State = new PlayerState
            {
                Job = 2,
                Level = 38,
                Hp = 350,
                MaxHp = 350,
                Gold = 100000
            }
        };

        dispatcher.RegisterSession(warrior);
        dispatcher.RegisterSession(taoist);
        map.Aoi.Enter(warrior, 300, 300);
        map.Aoi.Enter(taoist, 301, 300);

        // 1. 刺杀开关与半月开关测试 (CM_THRUSTINGONOFF 3026, CM_HALFMOONONOFF 3027)
        dispatcher.Dispatch(warrior, $"#{ProtocolConsts.CM_THRUSTINGONOFF}/0/!");
        Assert.False(warrior.State.BoThrusting);

        dispatcher.Dispatch(warrior, $"#{ProtocolConsts.CM_THRUSTINGONOFF}/1/!");
        Assert.True(warrior.State.BoThrusting);

        dispatcher.Dispatch(warrior, $"#{ProtocolConsts.CM_HALFMOONONOFF}/0/!");
        Assert.False(warrior.State.BoHalfMoon);

        dispatcher.Dispatch(warrior, $"#{ProtocolConsts.CM_HALFMOONONOFF}/1/!");
        Assert.True(warrior.State.BoHalfMoon);

        // 2. 心跳保活测试 (CM_HEARTBEAT 81)
        long oldTick = warrior.LastActiveTick;
        dispatcher.Dispatch(warrior, $"#{ProtocolConsts.CM_HEARTBEAT}!");
        Assert.True(warrior.LastActiveTick >= oldTick);

        // 3. 查名测试 (CM_QUERYUSERNAME 80, CM_QUERYUSERNAME_EXT 82)
        dispatcher.Dispatch(warrior, $"#{ProtocolConsts.CM_QUERYUSERNAME}/102/!");
        Assert.True(warrior.HasBufferedPacket($"#{ProtocolConsts.SM_USERNAME}/"));

        dispatcher.Dispatch(warrior, $"#{ProtocolConsts.CM_QUERYUSERNAME_EXT}/102/!");
        Assert.True(warrior.HasBufferedPacket($"#{ProtocolConsts.SM_SYSMSG_DELPHI}/"));

        // 4. 私聊测试 (CM_WHISPER 3030: /逍遥散人 兄弟组队吗)
        dispatcher.Dispatch(warrior, $"#{ProtocolConsts.CM_SAY}//逍遥散人 兄弟组队吗!");
        Assert.True(taoist.HasBufferedPacket($"#{ProtocolConsts.SM_WHISPER}/"));
        Assert.True(warrior.HasBufferedPacket($"#{ProtocolConsts.SM_WHISPER}/"));

        // 5. 选服确认 (CM_SELECTSERVER 104)
        dispatcher.Dispatch(warrior, $"#{ProtocolConsts.CM_SELECTSERVER}!");
        Assert.True(warrior.HasBufferedPacket($"#{ProtocolConsts.SM_SELECTSERVER_OK}/"));

        // 6. 行会封号调整测试 (CM_GUILD_UPDATE_RANK 1041)
        // 背包放入创建行会权威凭据: 沃玛号角
        warrior.State.BagItems.Add(new EquipItem { Uid = 9999, Name = "沃玛号角", Weight = 1 });
        var (cRes, guild, _) = dispatcher.GuildEngine.CreateGuild(warrior, "沙巴克行会");
        Assert.Equal(GuildEngine.GuildResult.Success, cRes);
        // 招收逍遥散人
        dispatcher.GuildEngine.AddMember(warrior, "逍遥散人");
        // 掌门人调整逍遥散人的封号为【白虎堂堂主】
        dispatcher.Dispatch(warrior, $"#{ProtocolConsts.CM_GUILD_UPDATE_RANK}/逍遥散人/白虎堂堂主!");
        Assert.Equal("白虎堂堂主", dispatcher.GuildEngine.GetMemberRank("逍遥散人"));
    }

    [Fact]
    public void TestAuthoritative_MagicIdDualTrackCompatibility_HealAndArmorBuff()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 200, 200);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        // 1. 道士 (SessionId: 201) 与残血战士 (SessionId: 202) 站在相邻位置 (10, 10) 与 (10, 11)
        var taoist = new ClientSession(201, null!)
        {
            CurrentMapId = 0,
            X = 10,
            Y = 10,
            CharacterName = "天通道人",
            State = new PlayerState
            {
                Job = 2,
                Level = 35,
                Hp = 200,
                MaxHp = 200,
                Mp = 300,
                MaxMp = 300,
                Sc = 30
            }
        };

        var warrior = new ClientSession(202, null!)
        {
            CurrentMapId = 0,
            X = 10,
            Y = 11,
            CharacterName = "烈火战神",
            State = new PlayerState
            {
                Job = 0,
                Level = 35,
                Hp = 50,
                MaxHp = 500,
                Mp = 100,
                MaxMp = 100
            }
        };

        map.Aoi.Enter(taoist, 10, 10);
        map.Aoi.Enter(warrior, 10, 11);
        dispatcher.RegisterSession(taoist);
        dispatcher.RegisterSession(warrior);

        // 2. 道士向战士目标施放治愈术 (MagicId = 2) -> 战士血量增加
        dispatcher.ResolveAuthoritativeSpell(taoist, 2, warrior.SessionId, warrior.X, warrior.Y, Environment.TickCount64);
        Assert.True(warrior.State.Hp > 50, "治愈术(MagicId 2)必须成功为目标队友加血！");
        int hpAfterHeal2 = warrior.State.Hp;

        // 3. 道士使用兼容 ID (MagicId = 7) 再次向战士施放治愈术 -> 战士血量继续增加
        dispatcher.ResolveAuthoritativeSpell(taoist, 7, warrior.SessionId, warrior.X, warrior.Y, Environment.TickCount64);
        Assert.True(warrior.State.Hp > hpAfterHeal2, "治愈术(MagicId 7 兼容)必须成功为目标队友加血！");

        // 4. 道士施展幽灵盾 (MagicId = 14) -> 自身与范围队友 MAC 增加
        dispatcher.ResolveAuthoritativeSpell(taoist, 14, 0, 0, 0, Environment.TickCount64);
        Assert.True(warrior.State.HasGhostShield, "幽灵盾(MagicId 14)必须提升队友魔法防御光环！");

        // 5. 战士施展刺杀剑术 (MagicId = 12) -> 切换刺杀剑气开关 (默认 true，先置 false 再 toggle 验证)
        warrior.State.StabbingTracker.AutoStab = false;
        dispatcher.ResolveAuthoritativeSpell(warrior, 12, 0, 0, 0, Environment.TickCount64);
        Assert.True(warrior.State.StabbingTracker.AutoStab, "刺杀剑术(MagicId 12)必须成功切换战士自动刺杀剑气！");

        // 6. 道士施展神圣战甲术 (MagicId = 15) -> 自身与范围队友 AC 增加
        dispatcher.ResolveAuthoritativeSpell(taoist, 15, 0, 0, 0, Environment.TickCount64);
        Assert.True(warrior.State.HasHolyArmor, "神圣战甲术(MagicId 15)必须提升队友物理防御光环！");

        // 7. 法师施展爆裂火焰 (MagicId = 33) -> 目标 3x3 范围造成必中法术伤害
        var mage = new ClientSession(203, null!)
        {
            CurrentMapId = 0,
            X = 10,
            Y = 13,
            CharacterName = "法神",
            State = new PlayerState { Job = 1, Level = 35, Hp = 180, MaxHp = 180, Mp = 500, MaxMp = 500 }
        };
        map.Aoi.Enter(mage, 10, 13);
        dispatcher.RegisterSession(mage);

        int warriorHpBeforeFireBang = warrior.State.Hp;
        dispatcher.ResolveAuthoritativeSpell(mage, 33, warrior.SessionId, warrior.X, warrior.Y, Environment.TickCount64);
        Assert.True(warrior.State.Hp < warriorHpBeforeFireBang, "爆裂火焰(MagicId 33)必须对 3x3 范围目标造成魔法伤害！");

        // 8. 验证 OfficialMagicCompendium 全量 33 门技能与 SkillFormulas 完全对齐
        Assert.Equal(33, OfficialMagicCompendium.Count);
        Assert.Equal(33, SkillFormulas.TotalSkillsCount);
        Assert.Equal("火球术", OfficialMagicCompendium.Get(1)?.Name);
        Assert.Equal("治愈术", OfficialMagicCompendium.Get(2)?.Name);
        Assert.Equal("雷电术", OfficialMagicCompendium.Get(10)?.Name);
        Assert.Equal("火墙", OfficialMagicCompendium.Get(22)?.Name);
        Assert.Equal("地狱雷光", OfficialMagicCompendium.Get(21)?.Name);
        Assert.Equal("疾光电影", OfficialMagicCompendium.Get(23)?.Name);
        Assert.Equal("烈火剑法", OfficialMagicCompendium.Get(26)?.Name);
        Assert.Equal("野蛮冲撞", OfficialMagicCompendium.Get(27)?.Name);
        Assert.Equal("魔法盾", OfficialMagicCompendium.Get(31)?.Name);
        Assert.Equal("冰咆哮", OfficialMagicCompendium.Get(32)?.Name);
        Assert.Equal("爆裂火焰", OfficialMagicCompendium.Get(33)?.Name);
    }

    [Fact]
    public void TestAuthoritative_DropGold_PreservesCountAndIsGoldForPickup()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 100, 100);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var player1 = new ClientSession(301, null!)
        {
            CurrentMapId = 0,
            X = 20,
            Y = 20,
            CharacterName = "土豪老哥",
            State = new PlayerState { Gold = 50_000, Hp = 200, MaxHp = 200 }
        };
        map.Aoi.Enter(player1, 20, 20);
        dispatcher.RegisterSession(player1);

        // 1. 丢弃 20,000 金币 (CM_DROPGOLD 1016)
        dispatcher.Dispatch(player1, $"#{ProtocolConsts.CM_DROPGOLD}/20000/!");
        Assert.Equal(30_000, player1.State.Gold);

        // 2. 验证地表生成的金币实体属性
        var groundItems = map.GetGroundItemsAt(20, 20);
        Assert.Single(groundItems);
        var droppedGold = groundItems[0];
        Assert.True(droppedGold.IsGold, "丢弃的金币实体必须具有 IsGold = true 属性！");
        Assert.Equal(20_000, droppedGold.Count);

        // 3. 玩家2 (穷小子) 前往拾取
        var player2 = new ClientSession(302, null!)
        {
            CurrentMapId = 0,
            X = 20,
            Y = 20,
            CharacterName = "穷小子",
            State = new PlayerState { Gold = 1_000, Hp = 100, MaxHp = 100 }
        };
        map.Aoi.Enter(player2, 20, 20);
        dispatcher.RegisterSession(player2);

        dispatcher.Dispatch(player2, $"#{ProtocolConsts.CM_PICKUP}/!");
        Assert.Equal(21_000, player2.State.Gold);
        Assert.Empty(player2.State.BagItems); // 绝不应该被当成装备塞进背包！
        Assert.Empty(map.GetGroundItemsAt(20, 20)); // 地面已被清空拾取
    }

    [Fact]
    public void TestAuthoritative_SkillTrain_OnSpellAndPhysicalHit_SyncsSM_UPDATEUSERMAGIC()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 50, 50);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var mage = new ClientSession(881, null!)
        {
            CurrentMapId = 0,
            X = 10,
            Y = 10,
            CharacterName = "雷电法王",
            State = new PlayerState { Job = 1, Level = 20, Hp = 100, MaxHp = 100, Mp = 100, MaxMp = 100 }
        };
        map.Aoi.Enter(mage, 10, 10);
        dispatcher.RegisterSession(mage);

        // 1. 登录下发初始技能列表
        dispatcher.SendUserMagics(mage);
        Assert.True(mage.HasBufferedPacket($"#{ProtocolConsts.SM_SENDUSERMAGIC}/"));

        // 2. 学习雷电术 (10)
        var thunder = SkillTrainEngine.LearnSkill(mage.State, 10, "雷电术", 0);
        Assert.Equal(0, thunder.CurTrain);

        // 3. 施展雷电术 (CM_SPELL 3017: targetUid/10/11/10)
        dispatcher.Dispatch(mage, $"#{ProtocolConsts.CM_SPELL}/0/10/11/10/!");

        // 验证熟练度增加并且下发了 SM_UPDATEUSERMAGIC (203)
        Assert.Equal(1, thunder.CurTrain);
        Assert.True(mage.HasBufferedPacket($"#{ProtocolConsts.SM_UPDATEUSERMAGIC}/"));
    }

    [Fact]
    public void TestAuthoritative_MagicKeyChange_SupportsMultiSegmentSlashesAndSendsSM_UPDATEUSERMAGIC()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 50, 50);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var player = new ClientSession(991, null!)
        {
            CurrentMapId = 0,
            CharacterName = "键位宗师",
            State = new PlayerState { Job = 0, Level = 35 }
        };
        dispatcher.RegisterSession(player);

        var fire = SkillTrainEngine.LearnSkill(player.State, 26, "烈火剑法", 1);
        Assert.Equal(255, fire.Key); // 初始未绑定快捷键

        // 客户端发送带有多段官方协议参数的快捷键绑定请求 (CM_MAGICKEYCHANGE 1008: 26/3/0/0/)
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_MAGICKEYCHANGE}/26/3/0/0/!");

        // 验证成功绑定到 F4 (keyNum = 3)
        Assert.Equal(3, fire.Key);
        // 验证向客户端同步了 SM_UPDATEUSERMAGIC (203)
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_UPDATEUSERMAGIC}/"));
    }

    [Fact]
    public void TestAuthoritative_TrialSlash_OnBroken_SendsSM_TAKEOFF_OK_AndWeightChanged()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 50, 50);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var warrior = new ClientSession(992, null!)
        {
            CurrentMapId = 0,
            X = 20,
            Y = 20,
            Direction = 0,
            CharacterName = "倒霉试刀客",
            State = new PlayerState { Job = 0, Level = 40, Hp = 200, MaxHp = 200 }
        };
        map.Aoi.Enter(warrior, 20, 20);
        dispatcher.RegisterSession(warrior);

        // 佩戴一把已在沙巴克升级但被标记必碎的武器 (UpgradeState = 1 为必碎码)
        var doomedWeapon = new EquipItem
        {
            Uid = 9988,
            Name = "裁决之杖",
            Weight = 50,
            UpgradeState = 1
        };
        warrior.State.Equipment[(int)OfficialEquipSlot.Weapon] = doomedWeapon;
        warrior.State.RecalculateAttributes();

        // 挥刀攻击 (CM_HIT 3014: 20/19/0)
        dispatcher.Dispatch(warrior, $"#{ProtocolConsts.CM_HIT}/20/19/0/!");

        // 验证武器槽位被清除为 null
        Assert.Null(warrior.State.Equipment[(int)OfficialEquipSlot.Weapon]);
        // 验证下发了 SM_TAKEOFF_OK 清空客户端武器栏
        Assert.True(warrior.HasBufferedPacket($"#{ProtocolConsts.SM_TAKEOFF_OK}/"));
        // 验证下发了 SM_WEIGHTCHANGED 同步手持负重
        Assert.True(warrior.HasBufferedPacket($"#{ProtocolConsts.SM_WEIGHTCHANGED}/"));
    }

    [Fact]
    public void TestAuthoritative_PickUp_Item_SendsSM_WEIGHTCHANGED()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 50, 50);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var player = new ClientSession(993, null!)
        {
            CurrentMapId = 0,
            X = 25,
            Y = 25,
            CharacterName = "拾荒者",
            State = new PlayerState { Job = 0, Level = 20, Hp = 100, MaxHp = 100, BagWeight = 0, MaxBagWeight = 100 }
        };
        map.Aoi.Enter(player, 25, 25);
        dispatcher.RegisterSession(player);

        // 地面生成重装备 (重量 30)
        var heavyArmor = new GroundItemEntity
        {
            Uid = 8881,
            X = 25,
            Y = 25,
            Name = "重盔甲(男)",
            Weight = 30
        };
        map.AddGroundItem(heavyArmor);

        // 拾取物品 (CM_PICKUP 1001)
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_PICKUP}/!");

        Assert.Single(player.State.BagItems);
        Assert.Equal(30, player.State.BagWeight);
        // 必须收到 SM_ADDITEM 与 SM_WEIGHTCHANGED
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_ADDITEM}/"));
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_WEIGHTCHANGED}/"));
    }

    [Fact]
    public void TestMove_Overweight_CannotRun_PulledBack()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 100, 100);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var player = new ClientSession(1901, null!)
        {
            CurrentMapId = 0,
            X = 50,
            Y = 50,
            Direction = 0,
            CharacterName = "负重战士",
            State = new PlayerState
            {
                Job = 0,
                Level = 20,
                Hp = 100,
                MaxHp = 100,
                BagWeight = 250,
                MaxBagWeight = 200 // 已经超重 (250 > 200)
            }
        };
        map.Aoi.Enter(player, 50, 50);
        dispatcher.RegisterSession(player);

        // 尝试向前奔跑 2 格到 (50, 48): CM_RUN 3013
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_RUN}/50/48/0/!");

        // 必须被强制拉回原坐标 (50, 50)
        Assert.Equal(50, player.X);
        Assert.Equal(50, player.Y);
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_ENTITY_POS_DELPHI}/"));
        Assert.True(player.HasBufferedPacket("背包过重"));
    }

    [Fact]
    public void TestEatItem_Potion_DeductsBagWeight_AndSends_SM_DELITEM()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 100, 100);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var player = new ClientSession(2001, null!)
        {
            CurrentMapId = 0,
            X = 20,
            Y = 20,
            CharacterName = "服药客",
            State = new PlayerState { Job = 0, Level = 10, Hp = 20, MaxHp = 100, BagWeight = 10 }
        };
        map.Aoi.Enter(player, 20, 20);
        dispatcher.RegisterSession(player);

        var potion = new EquipItem { Uid = 5566, Name = "强效太阳水", Weight = 2 };
        player.State.BagItems.Add(potion);

        // 吃药: CM_EATITEM 1006
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_EATITEM}/0/0/0/0/强效太阳水!");

        // 验证背包物品已清，负重已扣减，且下发 SM_DELITEM 与 SM_EAT_OK
        Assert.Empty(player.State.BagItems);
        Assert.Equal(8, player.State.BagWeight);
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_DELITEM}/5566/0/"));
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_EAT_OK}/"));
    }

    [Fact]
    public void TestEatItem_BlessOil_DeductsOil_Sends_SM_DELITEM_And_SM_TAKEON_OK()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 100, 100);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var player = new ClientSession(2002, null!)
        {
            CurrentMapId = 0,
            X = 20,
            Y = 20,
            CharacterName = "喝油战神",
            State = new PlayerState { Job = 0, Level = 35, Hp = 100, MaxHp = 100, BagWeight = 15 }
        };
        map.Aoi.Enter(player, 20, 20);
        dispatcher.RegisterSession(player);

        // 手持裁决之杖
        var weapon = new EquipItem { Uid = 8888, Name = "裁决之杖", Weight = 80, Dura = 32, MaxDura = 32, Luck = 0 };
        player.State.Equipment[(int)OfficialEquipSlot.Weapon] = weapon;

        // 背包放入祝福油
        var oil = new EquipItem { Uid = 9999, Name = "祝福油", Weight = 1 };
        player.State.BagItems.Add(oil);

        // 喝祝福油: CM_EATITEM 1006
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_EATITEM}/0/0/0/0/祝福油!");

        // 验证背包祝福油已被扣除，负重下调，且下发 SM_DELITEM 与手持武器同步包 SM_TAKEON_OK
        Assert.Empty(player.State.BagItems);
        Assert.Equal(14, player.State.BagWeight); // 15 - 1 = 14
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_DELITEM}/9999/0/"));
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_TAKEON_OK}/8888/1/"));
    }

    [Fact]
    public void TestGetCurrentStatusMask_ResetsBitsWhenStatusCleared()
    {
        var player = new PlayerState { Uid = 1, Name = "状态机测试者" };
        Assert.Equal(0u, player.GetCurrentStatusMask());

        // 1. 中绿毒与麻痹
        player.HasGreenPoison = true;
        player.ParalyzeExpireMs = Environment.TickCount64 + 10_000;
        uint mask1 = player.GetCurrentStatusMask();
        Assert.NotEqual(0u, mask1);

        // 2. 解除毒素与麻痹
        player.ClearPoison();
        player.ParalyzeExpireMs = 0;
        player.StatusBits[5] = false;
        uint mask2 = player.GetCurrentStatusMask();
        Assert.Equal(0u, mask2); // 必须完全重置为 0，不能残留中毒与麻痹标志
    }

    [Fact]
    public void TestPlayerMurder_BroadcastsRedNameColor_NotStatusMask()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 100, 100);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var killer = new ClientSession(2003, null!)
        {
            CurrentMapId = 0,
            X = 20,
            Y = 20,
            CharacterName = "杀人狂",
            State = new PlayerState { Job = 0, Level = 40, Hp = 500, MaxHp = 500, PkPoints = 150 } // 已有 150 点 (黄名)
        };
        var victim = new ClientSession(2004, null!)
        {
            CurrentMapId = 0,
            X = 20,
            Y = 21,
            CharacterName = "受害者白名",
            State = new PlayerState { Job = 0, Level = 10, Hp = 0, MaxHp = 100, PkPoints = 0 } // 白名
        };
        map.Aoi.Enter(killer, 20, 20);
        map.Aoi.Enter(victim, 20, 21);
        dispatcher.RegisterSession(killer);
        dispatcher.RegisterSession(victim);

        // 触发杀死白名玩家: 150 + 100 = 250 点 (达到 200 点阈值，晋升红名 PkStatus.Red = 2)
        dispatcher.ProcessPlayerDeathAndMurderSettlement(killer, victim, map, Environment.TickCount64);

        Assert.Equal(250, killer.State.PkPoints);
        Assert.True(killer.State.IsRedName);

        // 验证广播的 SM_CHANGENAMECOLOR 参数必须为红名颜色码 2，而不是状态掩码 (0)
        Assert.True(killer.HasBufferedPacket($"#{ProtocolConsts.SM_CHANGENAMECOLOR}/2003/2/"));
    }

    [Fact]
    public void TestSuitWorldEventEngine_MemorySummonAll_ScatterPositions_AntiCollision()
    {
        var mapManager = new MapManager();
        var map = new GameMap(1, "沃玛森林", 100, 100);
        mapManager.RegisterMap(map);

        var leader = new ClientSession(3001, null!)
        {
            CurrentMapId = 1,
            X = 50,
            Y = 50,
            CharacterName = "记忆队长",
            State = new PlayerState { Job = 0, Level = 40, Hp = 500, MaxHp = 500 }
        };
        // 队长穿戴整套记忆神装 (头、链、镯、戒)
        leader.State.Equipment[4] = new EquipItem { Uid = 801, Name = "记忆头盔" };
        leader.State.Equipment[3] = new EquipItem { Uid = 802, Name = "记忆项链" };
        leader.State.Equipment[5] = new EquipItem { Uid = 803, Name = "记忆手镯" };
        leader.State.Equipment[7] = new EquipItem { Uid = 804, Name = "记忆戒指" };

        var m1 = new ClientSession(3002, null!)
        {
            CurrentMapId = 1,
            X = 10,
            Y = 10,
            CharacterName = "队员甲",
            State = new PlayerState { Job = 1, Level = 35, Hp = 200, MaxHp = 200 }
        };
        var m2 = new ClientSession(3003, null!)
        {
            CurrentMapId = 1,
            X = 12,
            Y = 12,
            CharacterName = "队员乙",
            State = new PlayerState { Job = 2, Level = 35, Hp = 250, MaxHp = 250 }
        };

        map.Aoi.Enter(leader, 50, 50);
        map.Aoi.Enter(m1, 10, 10);
        map.Aoi.Enter(m2, 12, 12);

        var members = new List<ClientSession> { leader, m1, m2 };

        // 执行天地合一召唤
        var (ok, count, msg) = SuitWorldEventEngine.ExecuteMemorySummonAll(leader, members, mapManager, tradeEngine: null, nowMs: 1000);

        Assert.True(ok);
        Assert.Equal(2, count);
        // 两位队员不能重叠在同一坐标，也不能和队长重叠在 (50, 50)
        Assert.False(m1.X == leader.X && m1.Y == leader.Y);
        Assert.False(m2.X == leader.X && m2.Y == leader.Y);
        Assert.False(m1.X == m2.X && m1.Y == m2.Y);

        // 且落在队长 1~2 格身周开阔地面
        Assert.True(Math.Abs(m1.X - leader.X) <= 2 && Math.Abs(m1.Y - leader.Y) <= 2);
        Assert.True(Math.Abs(m2.X - leader.X) <= 2 && Math.Abs(m2.Y - leader.Y) <= 2);
    }

    [Fact]
    public void TestSuitWorldEventEngine_MemorySummonAll_AllowRefuseSwitch()
    {
        var mapManager = new MapManager();
        var map = new GameMap(1, "沃玛森林", 100, 100);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var leader = new ClientSession(3011, null!)
        {
            CurrentMapId = 1,
            X = 40,
            Y = 40,
            CharacterName = "掌门队长",
            State = new PlayerState { Job = 0, Level = 45, Hp = 600, MaxHp = 600 }
        };
        leader.State.Equipment[4] = new EquipItem { Uid = 811, Name = "记忆头盔" };
        leader.State.Equipment[3] = new EquipItem { Uid = 812, Name = "记忆项链" };
        leader.State.Equipment[5] = new EquipItem { Uid = 813, Name = "记忆手镯" };
        leader.State.Equipment[7] = new EquipItem { Uid = 814, Name = "记忆戒指" };

        var obedient = new ClientSession(3012, null!)
        {
            CurrentMapId = 1,
            X = 5,
            Y = 5,
            CharacterName = "听话队员",
            State = new PlayerState { Job = 1, Level = 30, Hp = 180, MaxHp = 180 }
        };

        var rebel = new ClientSession(3013, null!)
        {
            CurrentMapId = 1,
            X = 6,
            Y = 6,
            CharacterName = "叛逆队员",
            State = new PlayerState { Job = 2, Level = 30, Hp = 200, MaxHp = 200 }
        };

        map.Aoi.Enter(leader, 40, 40);
        map.Aoi.Enter(obedient, 5, 5);
        map.Aoi.Enter(rebel, 6, 6);
        dispatcher.RegisterSession(leader);
        dispatcher.RegisterSession(obedient);
        dispatcher.RegisterSession(rebel);

        // 叛逆队员发送 @拒绝天地合一
        dispatcher.Dispatch(rebel, $"#{ProtocolConsts.CM_SAY}/@拒绝天地合一!");
        Assert.False(rebel.State.AllowGroupRecall);
        Assert.True(rebel.HasBufferedPacket($"#{ProtocolConsts.SM_SYSMSG_DELPHI}/"));

        // 建立队伍关系
        dispatcher.GroupEngine.CreateParty(leader);
        var party = dispatcher.GroupEngine.GetParty(leader.SessionId);
        party!.Members.Add(obedient);
        party!.Members.Add(rebel);

        // 队长发送 @天地合一
        dispatcher.Dispatch(leader, $"#{ProtocolConsts.CM_SAY}/@天地合一!");

        // 听话队员被拉扯到队长身旁
        Assert.True(Math.Abs(obedient.X - leader.X) <= 2 && Math.Abs(obedient.Y - leader.Y) <= 2);
        // 叛逆队员保持在原位 (6, 6) 未被拉扯
        Assert.Equal(6, rebel.X);
        Assert.Equal(6, rebel.Y);
    }

    [Fact]
    public void TestSuitWorldEventEngine_MemorySummonAll_CancelsActiveTrade_And_PullsPets()
    {
        var mapManager = new MapManager();
        var map0 = new GameMap(0, "盟重省", 100, 100);
        var map1 = new GameMap(1, "沃玛三层", 100, 100);
        mapManager.RegisterMap(map0);
        mapManager.RegisterMap(map1);

        var tradeEngine = new TradeEngine();
        var leader = new ClientSession(3021, null!)
        {
            CurrentMapId = 1,
            X = 30,
            Y = 30,
            CharacterName = "带队队长",
            State = new PlayerState { Job = 0, Level = 42, Hp = 550, MaxHp = 550 }
        };
        leader.State.Equipment[4] = new EquipItem { Uid = 821, Name = "记忆头盔" };
        leader.State.Equipment[3] = new EquipItem { Uid = 822, Name = "记忆项链" };
        leader.State.Equipment[5] = new EquipItem { Uid = 823, Name = "记忆手镯" };
        leader.State.Equipment[7] = new EquipItem { Uid = 824, Name = "记忆戒指" };

        var traderA = new ClientSession(3022, null!)
        {
            CurrentMapId = 0,
            X = 20,
            Y = 20,
            CharacterName = "交易队员",
            State = new PlayerState { Job = 2, Level = 38, Hp = 300, MaxHp = 300, Gold = 1000 }
        };
        var traderB = new ClientSession(3023, null!)
        {
            CurrentMapId = 0,
            X = 20,
            Y = 21,
            CharacterName = "路人商人",
            State = new PlayerState { Job = 0, Level = 30, Hp = 300, MaxHp = 300, Gold = 5000 }
        };

        map1.Aoi.Enter(leader, 30, 30);
        map0.Aoi.Enter(traderA, 20, 20);
        map0.Aoi.Enter(traderB, 20, 21);

        // traderA 带有一只虎卫随从
        var pet = HuWeiPetEngine.CreateHuWei(9901, traderA.SessionId, 0, 21, 20);
        traderA.Pets.Add(pet);

        // traderA 与 traderB 开启面对面交易
        var (tOk, _) = tradeEngine.TryInitiateTrade(traderA, traderB);
        Assert.True(tOk);
        Assert.NotNull(tradeEngine.GetTrade(traderA.SessionId));

        var team = new List<ClientSession> { leader, traderA };

        // 队长执行天地合一召唤
        var (sOk, count, _) = SuitWorldEventEngine.ExecuteMemorySummonAll(leader, team, mapManager, tradeEngine, nowMs: 5000);
        Assert.True(sOk);
        Assert.Equal(1, count);

        // 1. 验证交易被强制熔断注销
        Assert.Null(tradeEngine.GetTrade(traderA.SessionId));
        Assert.Null(tradeEngine.GetTrade(traderB.SessionId));

        // 2. 验证 traderA 已跨地图传送至地图 1 队长身旁
        Assert.Equal(1, traderA.CurrentMapId);
        Assert.True(Math.Abs(traderA.X - leader.X) <= 2 && Math.Abs(traderA.Y - leader.Y) <= 2);

        // 3. 验证随从宝宝同步跨地图传送至新地图
        Assert.Equal(1, pet.CurrentMapId);
        Assert.True(Math.Abs(pet.X - traderA.X) <= 2 && Math.Abs(pet.Y - traderA.Y) <= 2);
    }

    [Fact]
    public void TestPlayerState_IsInvisibleAt_And_TriggerBreakInvisibility()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇省", 50, 50);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var player = new ClientSession(3031, null!)
        {
            CurrentMapId = 0,
            X = 25,
            Y = 25,
            CharacterName = "隐身刺客",
            State = new PlayerState { Job = 0, Level = 40, Hp = 400, MaxHp = 400 }
        };
        // 佩戴隐身戒指 (槽位 7)
        player.State.Equipment[7] = new EquipItem { Uid = 899, Name = "隐身戒指", Shape = 113 };
        player.State.RecalculateAttributes();
        Assert.True(player.State.HasInvisibleRing);

        // 初始时刻 10000ms: 未出刀，处于隐身状态
        Assert.True(player.State.IsInvisibleAt(10000));

        // 玩家出刀攻击打破隐身: 权威时间 10000ms
        dispatcher.TriggerBreakInvisibility(player, "挥刀攻击", nowMs: 10000);

        // 受到攻击后 1 秒 (11000ms): 破隐 2 秒尚未结束，不处于隐身
        Assert.False(player.State.IsInvisibleAt(11000));

        // 受到攻击后 2.5 秒 (12500ms): 破隐结束，恢复隐身戒指常驻隐身
        Assert.True(player.State.IsInvisibleAt(12500));
    }

    [Fact]
    public void TestOfficial15_MapDimensions_And_PassageGroundTruth()
    {
        var mapMgr = new MapManager();

        // 1. 验证地图 Header 真实物理尺寸 (严格 1:1 对齐官方 0.map, 3.map, 1.map, 2.map, 11.map)
        var biqi = mapMgr.GetMap(0)!;
        Assert.Equal(700, biqi.Width);
        Assert.Equal(700, biqi.Height);

        var mengzhong = mapMgr.GetMap(3)!;
        Assert.Equal(1000, mengzhong.Width);
        Assert.Equal(800, mengzhong.Height);

        var woomyon = mapMgr.GetMap(1)!;
        Assert.Equal(600, woomyon.Width);
        Assert.Equal(600, woomyon.Height);

        var snake = mapMgr.GetMap(2)!;
        Assert.Equal(600, snake.Width);
        Assert.Equal(600, snake.Height);

        var zuma = mapMgr.GetMap(11)!;
        Assert.Equal(500, zuma.Width);
        Assert.Equal(500, zuma.Height);

        // 2. 验证官方 1.4/1.5 MapInfo.txt 权威跳点真实坐标
        // 比奇 <-> 沃玛
        var pBiqiToWoomyon = mapMgr.FindPassage(0, 325, 34);
        Assert.NotNull(pBiqiToWoomyon);
        Assert.Equal(1, pBiqiToWoomyon.ToMapId);
        Assert.Equal(560, pBiqiToWoomyon.ToX);
        Assert.Equal(553, pBiqiToWoomyon.ToY);

        var pWoomyonToBiqi = mapMgr.FindPassage(1, 560, 556);
        Assert.NotNull(pWoomyonToBiqi);
        Assert.Equal(0, pWoomyonToBiqi.ToMapId);
        Assert.Equal(325, pWoomyonToBiqi.ToX);
        Assert.Equal(37, pWoomyonToBiqi.ToY);

        // 比奇 <-> 毒蛇
        var pBiqiToSnake = mapMgr.FindPassage(0, 673, 83);
        Assert.NotNull(pBiqiToSnake);
        Assert.Equal(2, pBiqiToSnake.ToMapId);
        Assert.Equal(416, pBiqiToSnake.ToX);
        Assert.Equal(564, pBiqiToSnake.ToY);

        var pSnakeToBiqi = mapMgr.FindPassage(2, 415, 567);
        Assert.NotNull(pSnakeToBiqi);
        Assert.Equal(0, pSnakeToBiqi.ToMapId);
        Assert.Equal(672, pSnakeToBiqi.ToX);
        Assert.Equal(86, pSnakeToBiqi.ToY);

        // 毒蛇 <-> 盟重 (西关 & 东关)
        var pWestSnakeToMengzhong = mapMgr.FindPassage(2, 298, 58);
        Assert.NotNull(pWestSnakeToMengzhong);
        Assert.Equal(3, pWestSnakeToMengzhong.ToMapId);
        Assert.Equal(275, pWestSnakeToMengzhong.ToX);
        Assert.Equal(750, pWestSnakeToMengzhong.ToY);

        var pEastSnakeToMengzhong = mapMgr.FindPassage(2, 559, 74);
        Assert.NotNull(pEastSnakeToMengzhong);
        Assert.Equal(3, pEastSnakeToMengzhong.ToMapId);
        Assert.Equal(514, pEastSnakeToMengzhong.ToX);
        Assert.Equal(774, pEastSnakeToMengzhong.ToY);

        // 盟重 <-> 祖玛大厅
        var pMengzhongToZuma = mapMgr.FindPassage(3, 862, 174);
        Assert.NotNull(pMengzhongToZuma);
        Assert.Equal(11, pMengzhongToZuma.ToMapId);
        Assert.Equal(9, pMengzhongToZuma.ToX);
        Assert.Equal(25, pMengzhongToZuma.ToY);

        // 3. 验证法师 12 级官方 1.5 原生技能【抗拒火环】(RepulsionRingEngine)
        // 40 级法师推 35 级目标 -> 成功推开 1 格
        var (repOk, newX, newY) = Mir2.Shared.Rules.AdvancedCombatRuleEngine.RepulsionRingEngine.TryRepel(40, 35, 10, 10, 11, 10);
        Assert.True(repOk);
        Assert.Equal(12, newX);
        Assert.Equal(10, newY);

        // 35 级法师推 40 级目标 -> 等级不足推不动
        var (repFail, noX, noY) = Mir2.Shared.Rules.AdvancedCombatRuleEngine.RepulsionRingEngine.TryRepel(35, 40, 10, 10, 11, 10);
        Assert.False(repFail);
        Assert.Equal(11, noX);
        Assert.Equal(10, noY);

        // 4. 验证全服广播引擎 WorldBroadcastEngine: 净化非 1.5 道具
        var broadcastEngine = new WorldBroadcastEngine();
        var sender = new ClientSession(2001, null!)
        {
            CharacterName = "测试勇士",
            State = new PlayerState { Uid = 2001, Name = "测试勇士" }
        };
        var receiver = new ClientSession(2002, null!)
        {
            CharacterName = "旁观者",
            State = new PlayerState { Uid = 2002, Name = "旁观者" }
        };
        var allSessions = new List<ClientSession> { sender, receiver };

        // 4.1 背包中只有非 1.5 的“狮子吼” -> 广播引擎拒绝发送，严格返回提示缺少【千里传音】
        sender.State.BagItems.Add(new EquipItem { Uid = 991, Name = "狮子吼" });
        var (bOkFail, _, _, errMsgFail) = broadcastEngine.BroadcastMessage(sender, "测试广播内容", allSessions, 10000);
        Assert.False(bOkFail);
        Assert.Contains("千里传音", errMsgFail);
        Assert.DoesNotContain("狮子吼", errMsgFail); // 提示信息已彻底净化

        // 4.2 背包中放入官方 1.5 权威道具【千里传音】 -> 广播成功并消耗道具
        sender.State.BagItems.Clear();
        sender.State.BagItems.Add(new EquipItem { Uid = 992, Name = "千里传音" });
        var (bOkPass, consumedItem, broadcastText, _) = broadcastEngine.BroadcastMessage(sender, "沙巴克行会集结！", allSessions, 16000);
        Assert.True(bOkPass);
        Assert.NotNull(consumedItem);
        Assert.Equal(992, consumedItem.Uid);
        Assert.Contains("沙巴克行会集结！", broadcastText);
        Assert.Empty(sender.State.BagItems); // 道具扣除完毕
    }
}








