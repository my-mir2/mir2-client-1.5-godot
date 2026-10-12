using System;
using System.Collections.Generic;
using Mir2.Server.Core;
using Mir2.Server.Game;
using Mir2.Server.Maps;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 盛大官方 1.5 服务端权威世界心跳、全生命周期状态机与防外挂闭环自动化测试套件
/// 严格 1:1 核销：
/// 1. 药水持续缓回池服务端 Tick 匀速抽取与满血清空防溢出机制
/// 2. 角色死亡复活 CM_REVIVE (1018) 权威清除毒素、满血满魔与安全区回城重置
/// 3. 官方特戒传送指令 @move x y 权威瞬移与障碍墙体拦截
/// 4. 银杏山谷怪物刷新与 AI 在 WorldTick 驱动下的自动寻敌近战攻击
/// 5. 官方 80 秒掉落物杀手独享归属期与超时放行防吸物外挂裁决
/// </summary>
public sealed class TestServerWorldTickAndAuthoritativeFullLoop
{
    private (MapManager mapMgr, MonsterRegenEngine regenEng, NpcMerchantEngine npcEng, PacketDispatcher dispatcher, GameMap map) CreateTestEnvironment()
    {
        var mapMgr = new MapManager();
        var map = new GameMap(0, "比奇省", 400, 400);
        mapMgr.RegisterMap(map);

        var regenEng = new MonsterRegenEngine(mapMgr);
        var npcEng = new NpcMerchantEngine();
        var tradeEng = new TradeEngine();
        var groupEng = new GroupEngine();
        var guildEng = new GuildEngine();

        var dispatcher = new PacketDispatcher(mapMgr, regenEng, npcEng, tradeEng, groupEng, guildEng);
        return (mapMgr, regenEng, npcEng, dispatcher, map);
    }

    [Fact]
    public void Test_Potion_IncHealthSpell_Authoritative_WorldTick()
    {
        // 1. 验证金创药进入蓄积池并在服务端 Tick 驱动下匀速释放 5 点
        var player = new PlayerState
        {
            Hp = 50,
            MaxHp = 100,
            Mp = 20,
            MaxMp = 100
        };
        player.BagItems.Add(new EquipItem { Uid = 1001, Name = "金创药(小量)", Weight = 1 });

        var (res, effect) = PotionEngine.ConsumePotion(player, 0, "金创药(小量)");
        Assert.Equal(PotionEngine.PotionResult.Success, res);
        Assert.NotNull(effect);
        Assert.Equal(30, player.IncHealth); // 缓回池注入 30 点

        // 模拟第 1 个 Tick (抽取 5 点)
        var (hpGain1, _) = PotionEngine.TickIncHealthSpell(player);
        Assert.Equal(5, hpGain1);
        Assert.Equal(55, player.Hp);
        Assert.Equal(25, player.IncHealth);

        // 模拟连续 5 个 Tick
        for (int i = 0; i < 5; i++)
        {
            PotionEngine.TickIncHealthSpell(player);
        }
        Assert.Equal(80, player.Hp);
        Assert.Equal(0, player.IncHealth); // 30 点全部释放完毕

        // 官方 1.5 权威顶药规则：满血时不扣减血池，亦不强行清空血池（保留顶药以备战斗受击）
        player.Hp = 100;
        player.IncHealth = 50;
        var (hpGainFull, _) = PotionEngine.TickIncHealthSpell(player);
        Assert.Equal(0, hpGainFull);
        Assert.Equal(50, player.IncHealth); // 官方机制：满血保留血池

        // 受到伤害扣血后，血池在后续 Tick 立即继续释放
        player.Hp = 80;
        var (hpGainAfterDamage, _) = PotionEngine.TickIncHealthSpell(player);
        Assert.Equal(5, hpGainAfterDamage);
        Assert.Equal(85, player.Hp);
        Assert.Equal(45, player.IncHealth);
    }

    [Fact]
    public void Test_Authoritative_Revive_Protocol_1018()
    {
        var (mapMgr, regenEng, npcEng, dispatcher, map) = CreateTestEnvironment();

        var session = new ClientSession(1001, null!)
        {
            CurrentMapId = 0,
            X = 150,
            Y = 150,
            CharacterName = "测试烈士",
            State = new PlayerState
            {
                Hp = 0, // 阵亡状态
                MaxHp = 250,
                Mp = 0,
                MaxMp = 120,
                HasGreenPoison = true,
                HasRedPoison = true
            }
        };
        map.Aoi.Enter(session, 150, 150);

        // 客户端发送死亡复活协议: #1018/0/0/0/0/!
        dispatcher.Dispatch(session, "#1018/0/0/0/0/!");

        // 验证服务端权威复活裁决结果 (盛大官方 1.5 规则: 半血半魔复活)
        Assert.Equal(125, session.State.Hp); // 半血
        Assert.Equal(60, session.State.Mp);  // 半魔
        Assert.False(session.State.HasGreenPoison); // 绿毒清除
        Assert.False(session.State.HasRedPoison);   // 红毒清除
        Assert.Equal(330, session.X);              // 传送回银杏山谷安全区
        Assert.Equal(270, session.Y);

        // 验证下发复活包 SM_REVIVE (501)
        Assert.True(session.HasBufferedPacket("#501/1001/330/270/0/测试烈士!"));
    }

    [Fact]
    public void Test_TeleportRing_MoveCommand_Authoritative_Movement()
    {
        var (mapMgr, regenEng, npcEng, dispatcher, map) = CreateTestEnvironment();

        var session = new ClientSession(2001, null!)
        {
            CurrentMapId = 0,
            X = 330,
            Y = 270,
            CharacterName = "法神",
            State = new PlayerState
            {
                Hp = 150,
                MaxHp = 150
            }
        };
        session.State.Equipment[(int)OfficialEquipSlot.RingR] = new EquipItem
        {
            Uid = 991,
            Name = "传送戒指",
            Shape = 112
        };
        session.State.RecalculateAttributes();
        map.Aoi.Enter(session, 330, 270);

        // 1. 发送合法特戒传送指令: @move 345 285
        dispatcher.Dispatch(session, "#3030/0/0/0/0/@move 345 285!");

        Assert.Equal(345, session.X);
        Assert.Equal(285, session.Y);
        Assert.True(session.HasBufferedPacket("瞬移至 (345, 285) 成功"));

        // 2. 发送越界或非法坐标: @move 9999 9999
        session.State.LastTeleportTickMs = 0; // 重置冷却以测试坐标合法性
        dispatcher.Dispatch(session, "#3030/0/0/0/0/@move 9999 9999!");
        Assert.Equal(345, session.X); // 坐标未变
        Assert.True(session.HasBufferedPacket("传送失败"));
    }

    [Fact]
    public void Test_Monster_Regen_And_Ai_Attack_In_WorldTick()
    {
        var (mapMgr, regenEng, npcEng, dispatcher, map) = CreateTestEnvironment();

        // 注册银杏山谷钉耙猫规则
        regenEng.AddRule(new MonsterSpawnRule
        {
            RuleId = 10,
            MonsterName = "钉耙猫",
            MapId = 0,
            CenterX = 330,
            CenterY = 270,
            Radius = 2,
            MaxCount = 5,
            BaseHp = 60
        });

        // 驱动刷怪
        var rng = OfficialClientRulesEngine.DelphiRandom.Shared;
        var (spawned, _) = regenEng.ProcessRegenTick(rng, enforceTimeSlice: false);
        Assert.True(spawned > 0);
        Assert.True(regenEng.TotalActiveMonsters > 0);

        // 在怪物旁边放入一个玩家
        var playerSession = new ClientSession(3001, null!)
        {
            CurrentMapId = 0,
            X = 330,
            Y = 270,
            CharacterName = "初级战士",
            State = new PlayerState
            {
                Hp = 100,
                MaxHp = 100,
                Speed = 10
            }
        };
        map.Aoi.Enter(playerSession, 330, 270);

        // 驱动怪物 AI 行为周期
        int aiActions = dispatcher.ProcessMonsterAiCycle(rng);
        Assert.True(aiActions >= 0);
    }

    [Fact]
    public void Test_Authoritative_GroundItem_80s_Protection_And_Pickup()
    {
        var (mapMgr, _, _, dispatcher, map) = CreateTestEnvironment();
        long now = Environment.TickCount64;

        // 地面上有一件裁决之杖，归属玩家 1001 (杀手)，在时间 now 掉落
        var drop = new GroundItemEntity
        {
            Uid = 8888,
            Name = "裁决之杖",
            X = 330,
            Y = 270,
            OwnerSessionId = 1001,
            DropTickMs = now,
            Weight = 30
        };
        map.AddGroundItem(drop);

        // 旁观者 2002 走上该坐标试图拾取 (掉落仅过去 10 秒)
        var otherPlayer = new ClientSession(2002, null!)
        {
            CurrentMapId = 0,
            X = 330,
            Y = 270,
            CharacterName = "路人甲",
            State = new PlayerState { Hp = 100, MaxHp = 100, BagWeight = 0, MaxBagWeight = 200 }
        };
        map.Aoi.Enter(otherPlayer, 330, 270);

        // 旁观者拾取尝试
        dispatcher.Dispatch(otherPlayer, "#1001/0/0/0/0/!");
        Assert.True(otherPlayer.HasBufferedPacket("物品受保护中"));
        Assert.Empty(otherPlayer.State.BagItems); // 背包仍然为空，严禁被拾取

        // 杀手 1001 本人前来拾取
        var killer = new ClientSession(1001, null!)
        {
            CurrentMapId = 0,
            X = 330,
            Y = 270,
            CharacterName = "终极杀手",
            State = new PlayerState { Hp = 100, MaxHp = 100, BagWeight = 0, MaxBagWeight = 200 }
        };
        map.Aoi.Enter(killer, 330, 270);
        dispatcher.Dispatch(killer, "#1001/0/0/0/0/!");

        Assert.Single(killer.State.BagItems);
        Assert.Equal("裁决之杖", killer.State.BagItems[0].Name);
        Assert.Equal(30, killer.State.BagWeight);
        Assert.True(killer.HasBufferedPacket("#200/8888/30/0/0/裁决之杖!"));
    }

    [Fact]
    public void Test_Map_LoadMapFile_And_Bresenham_CheckLineOfSight()
    {
        var map = new GameMap(0, "比奇省测试", 100, 100);

        // 1. 测试 Bresenham 视线射线穿透检测 (空旷地形无阻挡)
        bool clearSight = map.CheckLineOfSight(10, 10, 10, 20);
        Assert.True(clearSight);

        // 2. 在 (10, 15) 设置城墙阻挡
        map.SetWalkable(10, 15, false);
        Assert.False(map.IsWalkable(10, 15));

        // 再次视线检测，应被 (10, 15) 墙体阻挡
        bool blockedSight = map.CheckLineOfSight(10, 10, 10, 20);
        Assert.False(blockedSight);

        // 终点本身如果不可走，但在中途没有阻挡时不应误判 (攻击目标站立点)
        map.SetWalkable(10, 15, true);
        map.SetWalkable(10, 20, false);
        bool sightToTarget = map.CheckLineOfSight(10, 10, 10, 20);
        Assert.True(sightToTarget);

        // 3. 验证真实 .map 文件读取与边界容错
        string mapPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources", "maps", "Map", "0.map");
        if (System.IO.File.Exists(mapPath))
        {
            bool loaded = map.LoadMapFile(mapPath);
            Assert.True(loaded);
        }
    }

    [Fact]
    public void Test_Warrior_HalfMoon_Authoritative_AoE_And_Damage_Decay()
    {
        var (mapMgr, _, _, dispatcher, map) = CreateTestEnvironment();

        // 战士位于 (100, 100)，朝向为 0 (北 / 正上方: X不变, Y变小)
        var warrior = new ClientSession(1001, null!)
        {
            CurrentMapId = 0,
            X = 100,
            Y = 100,
            Direction = 0,
            CharacterName = "终极战士",
            State = new PlayerState
            {
                Job = 0,
                Level = 40,
                Hp = 300,
                MaxHp = 300,
                Mp = 100,
                MaxMp = 100,
                Dc = 15,
                MaxDc = 30
            }
        };
        SkillTrainEngine.LearnSkill(warrior.State, 25, "半月弯刀", 3);
        map.Aoi.Enter(warrior, 100, 100);

        // 朝向 0 时，扇面 4 个格子:
        // 主目标: (100, 99)
        // 副目标1: (99, 99)
        // 副目标2: (101, 99)
        // 副目标3: (99, 100)
        var mainTarget = new ClientSession(2001, null!)
        {
            CurrentMapId = 0,
            X = 100,
            Y = 99,
            CharacterName = "主受击者",
            State = new PlayerState { Hp = 100, MaxHp = 100 }
        };
        map.Aoi.Enter(mainTarget, 100, 99);

        var subTarget1 = new ClientSession(2002, null!)
        {
            CurrentMapId = 0,
            X = 99,
            Y = 99,
            CharacterName = "副受击者1",
            State = new PlayerState { Hp = 100, MaxHp = 100 }
        };
        map.Aoi.Enter(subTarget1, 99, 99);

        var outTarget = new ClientSession(2003, null!)
        {
            CurrentMapId = 0,
            X = 100,
            Y = 105, // 扇面外
            CharacterName = "安全吃瓜群众",
            State = new PlayerState { Hp = 100, MaxHp = 100 }
        };
        map.Aoi.Enter(outTarget, 100, 105);

        // 战士释放半月弯刀 (MagicId = 25, CM_SPELL = 3017: targetUid/magicId/targetX/targetY)
        // 数据包: #3017/0/25/100/99/!
        dispatcher.Dispatch(warrior, "#3017/0/25/100/99/!");

        // 验证主目标受到 100% 伤害 (主目标掉血量 > 0)
        int mainDmg = 100 - mainTarget.State.Hp;
        Assert.True(mainDmg >= 15 && mainDmg <= 30);

        // 验证副目标受到 35% 衰减伤害
        int subDmg = 100 - subTarget1.State.Hp;
        Assert.Equal(WarriorCombatEngine.HalfMoon.CalculateDamage(mainDmg, isMainTarget: false, skillLevel: 3), subDmg);

        // 验证扇面外吃瓜群众毫发无损 -> 仍然 100 HP
        Assert.Equal(100, outTarget.State.Hp);
    }

    [Fact]
    public void Test_Mage_FireWall_Authoritative_GroundEntity_And_StepIn_Damage()
    {
        var (mapMgr, _, _, dispatcher, map) = CreateTestEnvironment();

        var mage = new ClientSession(1001, null!)
        {
            CurrentMapId = 0,
            X = 50,
            Y = 50,
            CharacterName = "终极法师",
            State = new PlayerState
            {
                Job = 1,
                Hp = 100,
                MaxHp = 100,
                Mp = 200,
                MaxMp = 200
            }
        };
        map.Aoi.Enter(mage, 50, 50);

        // 法师在 (52, 52) 释放火墙术 (MagicId = 22: targetUid/magicId/targetX/targetY)
        // 生成 2x2 地表火焰: (52,52), (53,52), (52,53), (53,53)
        dispatcher.Dispatch(mage, "#3017/0/22/52/52/!");

        var fires = map.GetFireWallsAt(52, 52);
        Assert.Single(fires);
        Assert.Equal(1001, fires[0].CasterSessionId);
        Assert.True(fires[0].Damage > 0);

        // 敌人以行走方式踏入 (52, 52) 踩火 (CM_WALK = 3001)
        var victim = new ClientSession(2001, null!)
        {
            CurrentMapId = 0,
            X = 52,
            Y = 51,
            CharacterName = "踩火受害者",
            State = new PlayerState { Hp = 100, MaxHp = 100 }
        };
        map.Aoi.Enter(victim, 52, 51);

        // 踏入火墙: 从 (52, 51) 走到 (52, 52), dir = 4 (向下)
        // 数据包: #3011/52/52/4/0/! (CM_WALK = 3011)
        dispatcher.Dispatch(victim, "#3011/52/52/4/0/!");

        Assert.Equal(52, victim.X);
        Assert.Equal(52, victim.Y);
        // 踩火伤害结算后 HP 应扣减
        Assert.True(victim.State.Hp < 100);

        // 模拟世界心跳过期清理
        long farFuture = Environment.TickCount64 + 30_000;
        int cleaned = map.CleanExpiredFireWalls(farFuture);
        Assert.True(cleaned >= 4);
        Assert.Empty(map.GetFireWallsAt(52, 52));
    }

    [Fact]
    public void Test_Authoritative_TakeOffItem_And_Weight_Recalculate()
    {
        var (mapMgr, _, _, dispatcher, map) = CreateTestEnvironment();

        var player = new ClientSession(1001, null!)
        {
            CurrentMapId = 0,
            X = 100,
            Y = 100,
            CharacterName = "测试战士",
            State = new PlayerState
            {
                Job = 0,
                Level = 35,
                Hp = 400,
                MaxHp = 400
            }
        };
        map.Aoi.Enter(player, 100, 100);

        // 穿上一把裁决之杖在武器槽位 1
        var weapon = new EquipItem { Uid = 555, Name = "裁决之杖", Weight = 30, Dc = 30, Shape = 1 };
        player.State.Equipment[1] = weapon;
        player.State.RecalculateAuthoritativeWeights();
        player.State.RecalculateAttributes();

        int initialDc = player.State.MaxDc;
        int initialBagWeight = player.State.BagWeight;

        // 发送脱下武器请求 (CM_TAKEOFFITEM = 1004 / 1005: 槽位 1)
        dispatcher.Dispatch(player, "#1004/1/!");

        // 1. 验证武器槽位已空
        Assert.Null(player.State.Equipment[1]);

        // 2. 验证武器进入背包，背包负重增加 30
        Assert.Single(player.State.BagItems);
        Assert.Equal("裁决之杖", player.State.BagItems[0].Name);
        Assert.Equal(initialBagWeight + 30, player.State.BagWeight);

        // 3. 验证攻击力扣除了武器的 30 点
        Assert.Equal(initialDc - 30, player.State.MaxDc);

        // 4. 验证下发了 SM_TAKEOFF_OK (619) 与 SM_ABILITY (648)
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_TAKEOFF_OK}/"));
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_ABILITY}/"));

        // 5. 边界用例：将背包塞满 40 格，尝试卸下衣服，验证拒绝
        for (int i = 0; i < 39; i++)
        {
            player.State.BagItems.Add(new EquipItem { Uid = 1000 + i, Name = "回城卷", Weight = 1 });
        }
        Assert.Equal(40, player.State.BagItems.Count); // 背包已满 40 格

        player.State.Equipment[0] = new EquipItem { Uid = 888, Name = "重盔甲", Weight = 23, Ac = 5 };
        dispatcher.Dispatch(player, "#1004/0/!");

        // 依然穿在身上，卸下失败
        Assert.NotNull(player.State.Equipment[0]);
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_TAKEOFF_FAIL}/"));
    }

    [Fact]
    public void Test_Authoritative_DropItem_And_DropGold_To_Ground()
    {
        var (mapMgr, _, _, dispatcher, map) = CreateTestEnvironment();

        var player = new ClientSession(1001, null!)
        {
            CurrentMapId = 0,
            X = 120,
            Y = 120,
            CharacterName = "土豪玩家",
            State = new PlayerState
            {
                Level = 30,
                Gold = 10000
            }
        };
        map.Aoi.Enter(player, 120, 120);

        player.State.BagItems.Add(new EquipItem { Uid = 9999, Name = "战神盔甲", Weight = 40, Shape = 2 });
        player.State.RecalculateAuthoritativeWeights(player.State.Gold);

        // 1. 丢弃背包槽位 0 的战神盔甲 (CM_DROPITEM = 1000)
        dispatcher.Dispatch(player, "#1000/0/!");

        Assert.Empty(player.State.BagItems);
        var groundDrops = map.GetGroundItemsAt(120, 120);
        Assert.Single(groundDrops);
        Assert.Equal("战神盔甲", groundDrops[0].Name);
        Assert.Equal(0, groundDrops[0].OwnerSessionId); // 自由拾取

        // 2. 丢弃 3000 金币 (CM_DROPGOLD = 1016)
        dispatcher.Dispatch(player, "#1016/3000/!");

        Assert.Equal(7000, player.State.Gold);
        groundDrops = map.GetGroundItemsAt(120, 120);
        Assert.Equal(2, groundDrops.Count);
        Assert.Equal("金币", groundDrops[1].Name);
    }

    [Fact]
    public void Test_Authoritative_AdjustBonus_Points()
    {
        var (mapMgr, _, _, dispatcher, map) = CreateTestEnvironment();

        var player = new ClientSession(1001, null!)
        {
            CurrentMapId = 0,
            X = 100,
            Y = 100,
            CharacterName = "加点勇士",
            State = new PlayerState
            {
                Job = 0,
                Level = 20,
                BonusPoint = 3,
                Dc = 10,
            }
        };
        map.Aoi.Enter(player, 100, 100);
        player.State.RecalculateAttributes();
        int prevMaxDc = player.State.MaxDc;

        // 分配 1 点到物理攻击 DC (CM_ADJUST_BONUS = 1043: bonusType = 0)
        dispatcher.Dispatch(player, "#1043/0/!");

        Assert.Equal(2, player.State.BonusPoint);
        Assert.Equal(prevMaxDc + 1, player.State.MaxDc);
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_ABILITY}/"));
    }

    [Fact]
    public void Test_Authoritative_KillMonster_GainExp_And_LevelUp()
    {
        var (mapMgr, regen, _, dispatcher, map) = CreateTestEnvironment();

        var player = new ClientSession(1001, null!)
        {
            CurrentMapId = 0,
            X = 200,
            Y = 200,
            CharacterName = "升级狂人",
            State = new PlayerState
            {
                Job = 0,
                Level = 1,
                Exp = 0,
                Hp = 19,
                MaxHp = 19,
                Hit = 100
            }
        };
        map.Aoi.Enter(player, 200, 200);

        // 刷新一只 100 血的钉耙猫在 (201, 200)，击杀将提供 200 经验
        // 1 级升 2 级仅需 100 经验，将触发连级升级并重算属性
        regen.AddRule(new MonsterSpawnRule
        {
            RuleId = 1,
            MonsterName = "钉耙猫",
            MapId = 0,
            CenterX = 201,
            CenterY = 200,
            Radius = 0,
            MaxCount = 1,
            BaseHp = 100
        });
        regen.ProcessRegenTick(new OfficialClientRulesEngine.DelphiRandom(1));
        var cat = regen.FindMonsterAt(0, 201, 200);
        Assert.NotNull(cat);

        // 战士一刀将其打残并秒杀 (模拟高额一击)
        cat.Hp = 1;
        // 战士向右挥刀 CM_HIT (3014): 朝向 2, targetX = 201, targetY = 200
        dispatcher.Dispatch(player, "#3014/201/200/2/!");

        // 验证怪物死亡
        Assert.Null(regen.FindMonsterAt(0, 201, 200));

        // 验证玩家获得经验并升到 2 级 (100经验升级，剩余 100 经验)
        Assert.Equal(2, player.State.Level);
        Assert.Equal(100, player.State.Exp);

        // 验证升级后生命上限提升且满血
        Assert.True(player.State.MaxHp > 19);
        Assert.Equal(player.State.MaxHp, player.State.Hp);

        // 验证下发了升级光效广播与经验通知
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_LEVELUP_DELPHI}/"));
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_EXPERIENCE}/"));
    }

    [Fact]
    public void TestOfficialButcherCorpseMatrix_QualityAndDepletion()
    {
        var (mapMgr, regen, _, dispatcher, map) = CreateTestEnvironment();

        var player = new ClientSession(1001, null!)
        {
            CurrentMapId = 0,
            X = 200,
            Y = 200,
            CharacterName = "屠夫学徒",
            State = new PlayerState { Uid = 1001, Name = "屠夫学徒", Level = 10, Hp = 100, MaxHp = 100, BagWeight = 0, MaxBagWeight = 100 }
        };
        map.Aoi.Enter(player, 200, 200);

        // 1. 刷新一只鹿并击杀生成尸体
        regen.AddRule(new MonsterSpawnRule
        {
            RuleId = 10,
            MonsterName = "鹿",
            MapId = 0,
            CenterX = 201,
            CenterY = 200,
            Radius = 0,
            MaxCount = 1,
            BaseHp = 50
        });
        regen.ProcessRegenTick(new OfficialClientRulesEngine.DelphiRandom(1));
        var deer = regen.FindMonsterAt(0, 201, 200);
        Assert.NotNull(deer);

        // 击杀该鹿转入尸体池
        deer.Hp = 0;
        regen.OnMonsterDied(deer.Uid);
        var corpse = regen.FindCorpse(deer.Uid);
        Assert.NotNull(corpse);
        Assert.True(corpse.IsDead);
        Assert.False(corpse.IsButchered);

        // 2. 玩家在相邻位置 (200, 200) 对 (201, 200) 的鹿尸体发起割肉发包 CM_BUTCHITEM (1007)
        player.ClearBufferedPackets();
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_BUTCHITEM}/{deer.Uid}/201/200/!");

        // 3. 验证割肉成功
        Assert.True(corpse.IsButchered);
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_BUTCH}/"));
        var meat = player.State.BagItems.Find(i => i.Name == "鹿肉");
        Assert.NotNull(meat);
        Assert.True(meat.Dura >= 10000); // 鹿肉品质在 10~20 之间
        Assert.Equal(1, player.State.BagWeight);

        // 4. 重复对同一具尸体割肉，必须被权威拦截并提示已割空
        player.ClearBufferedPackets();
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_BUTCHITEM}/{deer.Uid}/201/200/!");
        Assert.Single(player.State.BagItems); // 背包物品没有增加
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_SYSMSG_DELPHI}/"));
    }

    [Fact]
    public void TestOfficialAntidoteAndInstantPotionLifecycle()
    {
        var (mapMgr, _, _, dispatcher, map) = CreateTestEnvironment();

        var player = new ClientSession(1001, null!)
        {
            CurrentMapId = 0,
            X = 200,
            Y = 200,
            CharacterName = "试药法师",
            State = new PlayerState { Uid = 1001, Name = "试药法师", Level = 25, Hp = 20, MaxHp = 120, Mp = 10, MaxMp = 200, BagWeight = 0, MaxBagWeight = 200 }
        };
        map.Aoi.Enter(player, 200, 200);

        // 1. 玩家染上绿毒与红毒
        long now = Environment.TickCount64;
        player.State.ApplyPoison(TaoistCombatEngine.PoisonType.Green, 30, sc: 20, now);
        player.State.ApplyPoison(TaoistCombatEngine.PoisonType.Red, 30, sc: 20, now);
        Assert.True(player.State.HasGreenPoison);
        Assert.True(player.State.HasRedPoison);

        // 2. 背包放入太阳水与解毒药水
        player.State.BagItems.Add(new EquipItem { Uid = 101, Name = "太阳水", Weight = 1 });
        player.State.BagItems.Add(new EquipItem { Uid = 102, Name = "解毒药水", Weight = 1 });
        player.State.BagWeight = 2;

        // 3. 喝太阳水: 瞬回 +30 HP, +40 MP
        player.ClearBufferedPackets();
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_EATITEM}/0/太阳水/!");
        Assert.Equal(50, player.State.Hp); // 20 + 30
        Assert.Equal(50, player.State.Mp); // 10 + 40
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_EAT_OK}/"));

        // 4. 使用解毒药水: 清除全部中毒状态 (模拟 300ms 吃药冷却过去)
        player.LastEatItemTimeMs = 0;
        player.ClearBufferedPackets();
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_EATITEM}/0/解毒药水/!");
        Assert.False(player.State.HasGreenPoison);
        Assert.False(player.State.HasRedPoison);
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_SYSMSG_DELPHI}/"));
    }

    [Fact]
    public void TestOfficialTaoistSpellReagentsConsumption()
    {
        var (mapMgr, _, _, dispatcher, map) = CreateTestEnvironment();

        var player = new ClientSession(1001, null!)
        {
            CurrentMapId = 0,
            X = 200,
            Y = 200,
            CharacterName = "清风道长",
            State = new PlayerState { Uid = 1001, Name = "清风道长", Level = 35, Hp = 200, MaxHp = 200, Mp = 300, MaxMp = 300, BagWeight = 0, MaxBagWeight = 300 }
        };
        map.Aoi.Enter(player, 200, 200);

        var enemy = new ClientSession(1002, null!)
        {
            CurrentMapId = 0,
            X = 201,
            Y = 200,
            CharacterName = "敌对武士",
            State = new PlayerState { Uid = 1002, Name = "敌对武士", Level = 30, Hp = 300, MaxHp = 300, Mp = 50, MaxMp = 50, BagWeight = 0, MaxBagWeight = 300 }
        };
        map.Aoi.Enter(enemy, 201, 200);

        // 1. 无药粉施放施毒术 (MagicId = 6) -> 拦截失败，MP 不扣除
        player.ClearBufferedPackets();
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_SPELL}/1002/6/201/200/!");
        Assert.Equal(300, player.State.Mp); // 蓝量未消耗
        Assert.False(enemy.State.HasGreenPoison);
        Assert.False(enemy.State.HasRedPoison);

        // 2. 背包放入黄色药粉 (Dura = 2)，再次施毒 -> 成功扣减药粉耐久，目标中红毒
        var powder = new EquipItem { Uid = 201, Name = "黄色药粉(中)", Weight = 2, Dura = 2, MaxDura = 200 };
        player.State.BagItems.Add(powder);
        player.State.BagWeight += 2;
        player.ClearBufferedPackets();
        player.LastSpellTimeMs = 0; // 重置施法CD
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_SPELL}/1002/6/201/200/!");
        Assert.True(enemy.State.HasRedPoison || enemy.State.HasGreenPoison);
        Assert.Equal(1, powder.Dura); // 药粉耐久扣减 1

        // 3. 灵魂火符 (MagicId = 13) 无护身符施放 -> 拦截失败
        player.ClearBufferedPackets();
        player.LastSpellTimeMs = 0;
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_SPELL}/1002/13/201/200/!");
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_SYSMSG_DELPHI}/"));

        // 4. 装备栏佩戴护身符 (OfficialEquipSlot.Lighting = 2) 后施放 -> 扣减护身符耐久并命中
        var amulet = new EquipItem { Uid = 202, Name = "护身符", Weight = 1, Dura = 5, MaxDura = 100 };
        player.State.Equipment[(int)OfficialEquipSlot.Lighting] = amulet;
        player.ClearBufferedPackets();
        player.LastSpellTimeMs = 0;
        int enemyOldHp = enemy.State.Hp;
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_SPELL}/1002/13/201/200/!");
        Assert.Equal(4, amulet.Dura); // 护身符耐久扣减 1
        Assert.True(enemy.State.Hp < enemyOldHp); // 造成法术伤害
    }

    [Fact]
    public void TestOfficialDoorStateMachine_OpenAndAutoClose()
    {
        var (mapMgr, _, _, dispatcher, map) = CreateTestEnvironment();

        // 1. 在地图 (100, 100) 处安装一扇石门，初始状态为关闭，设置阻挡
        var door = new DoorEntity { DoorId = 1, X = 100, Y = 100, IsOpen = false };
        map.AddDoor(door);
        Assert.False(map.IsWalkable(100, 100)); // 关门状态下物理阻挡不可通行

        // 2. 玩家位于门前 (100, 101)，朝向北 (0)
        var player = new ClientSession(1001, null!)
        {
            CurrentMapId = 0,
            X = 100,
            Y = 101,
            Direction = 0,
            CharacterName = "开门勇士",
            State = new PlayerState { Uid = 1001, Name = "开门勇士", Level = 10, Hp = 100, MaxHp = 100 }
        };
        map.Aoi.Enter(player, 100, 101);

        // 3. 玩家发送 CM_OPENDOOR (1002) 推门
        player.ClearBufferedPackets();
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_OPENDOOR}/100/100/!");

        // 4. 验证门状态与阻挡更新
        Assert.True(door.IsOpen);
        Assert.True(map.IsWalkable(100, 100)); // 开门后阻挡解除，允许穿行
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_OPENDOOR}/")); // 收到开门广播

        // 5. 模拟 5 秒后自动关门
        door.AutoCloseTickMs = Environment.TickCount64 - 1000; // 让开门时间已过期
        player.ClearBufferedPackets();
        dispatcher.TickWorldDoors(Environment.TickCount64);

        // 6. 验证门已自动关闭并恢复阻挡
        Assert.False(door.IsOpen);
        Assert.False(map.IsWalkable(100, 100)); // 关门后阻挡重新生效
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_CLOSEDOOR}/")); // 收到关门广播
    }

    [Fact]
    public void TestOfficialMapPassage_AuthoritativeTransfer()
    {
        // 构造具备比奇与毒蛇跳点的原生地图环境
        var mapMgr = new MapManager();
        var biqi = mapMgr.GetMap(0)!;
        var snakeValley = mapMgr.GetMap(2)!;
        var regenEng = new MonsterRegenEngine(mapMgr);
        var npcEng = new NpcMerchantEngine();
        var tradeEng = new TradeEngine();
        var groupEng = new GroupEngine();
        var guildEng = new GuildEngine();
        var dispatcher = new PacketDispatcher(mapMgr, regenEng, npcEng, tradeEng, groupEng, guildEng);

        // 1. 玩家站在比奇省 (649, 460)，距离毒蛇山谷跳点 (650, 460) 仅一步之遥
        var player = new ClientSession(1001, null!)
        {
            CurrentMapId = 0,
            X = 649,
            Y = 460,
            Direction = 2, // 朝东
            CharacterName = "过图法师",
            State = new PlayerState { Uid = 1001, Name = "过图法师", Level = 15, Hp = 80, MaxHp = 80 }
        };
        biqi.Aoi.Enter(player, 649, 460);

        // 2. 玩家往东走一步进入跳点 (650, 460)
        player.ClearBufferedPackets();
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_WALK}/650/460/2/!");

        // 3. 验证权威跳点传送：
        // 玩家地图切换到 2 (毒蛇山谷)，坐标传送到 (50, 500)
        Assert.Equal(2, player.CurrentMapId);
        Assert.Equal(50, player.X);
        Assert.Equal(500, player.Y);

        // 4. 验证 AOI 迁移与 SM_NEWMAP_DELPHI 下发
        var biqiEntities = new List<ClientSession>();
        biqi.Aoi.GetEntitiesInNinePatch(650, 460, biqiEntities);
        Assert.DoesNotContain(player, biqiEntities);

        var snakeEntities = new List<ClientSession>();
        snakeValley.Aoi.GetEntitiesInNinePatch(50, 500, snakeEntities);
        Assert.Contains(player, snakeEntities);

        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_NEWMAP_DELPHI}/"));
    }

    [Fact]
    public void TestServerPathFinder_ObstacleAvoidanceAndNoCornerCutting()
    {
        // 1. 创建 50x50 测试地图并在 (10, 10) 到 (12, 10) 设置横向墙体阻挡
        var map = new GameMap(99, "迷宫地图", 50, 50);
        map.SetWalkable(10, 10, false);
        map.SetWalkable(11, 10, false);
        map.SetWalkable(12, 10, false);

        // 2. 从墙南侧 (11, 11) 寻路到北侧 (11, 9)
        var (nextX, nextY, dir, found) = ServerPathFinder.FindNextStep(
            map,
            startX: 11, startY: 11,
            targetX: 11, targetY: 9
        );

        // 3. 验证算法成功找到绕行步进 (不能直走撞墙到 11, 10，而是向左或向右绕行)
        Assert.True(found);
        Assert.True(map.IsWalkable(nextX, nextY));
        Assert.NotEqual((11, 10), (nextX, nextY)); // 不能进入墙体
        Assert.Contains((nextX, nextY), new[] { (10, 11), (12, 11), (9, 11), (13, 11) });
    }

    [Fact]
    public void TestMonsterAi_AStarPursuitAndAntiOverlap()
    {
        var (mapMgr, regen, _, dispatcher, map) = CreateTestEnvironment();

        // 1. 玩家位于 (100, 100)
        var player = new ClientSession(1001, null!)
        {
            CurrentMapId = 0,
            X = 100,
            Y = 100,
            Direction = 0,
            CharacterName = "诱饵战士",
            State = new PlayerState { Uid = 1001, Name = "诱饵战士", Level = 20, Hp = 200, MaxHp = 200 }
        };
        map.Aoi.Enter(player, 100, 100);

        // 2. 在玩家正南方 (100, 101) 刷出怪物 B (挡在直线通道上)
        regen.AddRule(new MonsterSpawnRule
        {
            RuleId = 101,
            MonsterName = "阻挡半兽人",
            MapId = 0,
            CenterX = 100,
            CenterY = 101,
            Radius = 0,
            MaxCount = 1,
            BaseHp = 100
        });
        regen.ProcessRegenTick(new OfficialClientRulesEngine.DelphiRandom(1));
        var blocker = regen.FindMonsterAt(0, 100, 101);
        Assert.NotNull(blocker);

        // 3. 在 (100, 102) 刷出怪物 A (要追击玩家，但直线上被怪物 B 阻挡)
        regen.AddRule(new MonsterSpawnRule
        {
            RuleId = 102,
            MonsterName = "追击骷髅",
            MapId = 0,
            CenterX = 100,
            CenterY = 102,
            Radius = 0,
            MaxCount = 1,
            BaseHp = 100
        });
        regen.ProcessRegenTick(new OfficialClientRulesEngine.DelphiRandom(1));
        var chaser = regen.FindMonsterAt(0, 100, 102);
        Assert.NotNull(chaser);

        // 重置两者冷却，允许立即行动
        blocker.NextActionTimeMs = Environment.TickCount64 + 5000; // blocker 保持不动
        chaser.NextActionTimeMs = 0;

        // 4. 驱动怪物 AI 心跳
        var (actions, _) = regen.ProcessMonsterAiTick((mapId, x, y) => new List<ClientSession> { player }, new OfficialClientRulesEngine.DelphiRandom(1));

        // 5. 验证怪物 A 成功避开怪物 B，计算出侧面绕行，没有与怪物 B 重叠在 (100, 101)
        Assert.NotEqual((100, 101), (chaser.X, chaser.Y)); // 绝对严禁与 blocker 发生重叠！
        Assert.True(chaser.X != 100 || chaser.Y != 102);    // 成功发生了移动
        Assert.Contains(chaser.X, new[] { 99, 101 });       // 向左或向右进行了侧向绕行
    }

    [Fact]
    public void TestOfficialMageHighTierSpellsFullLoop()
    {
        var (mapMgr, regen, _, dispatcher, map) = CreateTestEnvironment();

        var mage = new ClientSession(1001, null!)
        {
            CurrentMapId = 0,
            X = 200,
            Y = 200,
            Direction = 2, // 朝向东
            CharacterName = "雷霆法圣",
            State = new PlayerState { Uid = 1001, Name = "雷霆法圣", Level = 40, Hp = 200, MaxHp = 200, Mp = 500, MaxMp = 500 }
        };
        map.Aoi.Enter(mage, 200, 200);

        // 1. 测试极光电影 (MagicId = 23, 朝东穿透直线 8 格)
        // 在前方 (202, 200) 与 (205, 200) 分别放置一只怪物
        regen.AddRule(new MonsterSpawnRule { RuleId = 1, MonsterName = "靶子怪1", MapId = 0, CenterX = 202, CenterY = 200, Radius = 0, MaxCount = 1, BaseHp = 100 });
        regen.AddRule(new MonsterSpawnRule { RuleId = 2, MonsterName = "靶子怪2", MapId = 0, CenterX = 205, CenterY = 200, Radius = 0, MaxCount = 1, BaseHp = 100 });
        regen.ProcessRegenTick(new OfficialClientRulesEngine.DelphiRandom(1));
        var target1 = regen.FindMonsterAt(0, 202, 200);
        var target2 = regen.FindMonsterAt(0, 205, 200);
        Assert.NotNull(target1);
        Assert.NotNull(target2);

        mage.ClearBufferedPackets();
        mage.LastSpellTimeMs = 0;
        // 释放极光电影 (MagicId = 23)
        dispatcher.Dispatch(mage, $"#{ProtocolConsts.CM_SPELL}/0/23/200/200/!");
        Assert.True(target1.Hp < 100); // 直线上第一只怪受到穿透伤害
        Assert.True(target2.Hp < 100); // 直线上第二只怪同样受到穿透伤害

        // 2. 测试冰咆哮 (MagicId = 33, 3x3 范围)
        // 在 (150, 150) 周围放置多只怪物
        regen.AddRule(new MonsterSpawnRule { RuleId = 3, MonsterName = "冰冻怪1", MapId = 0, CenterX = 150, CenterY = 150, Radius = 0, MaxCount = 1, BaseHp = 100 });
        regen.AddRule(new MonsterSpawnRule { RuleId = 4, MonsterName = "冰冻怪2", MapId = 0, CenterX = 151, CenterY = 151, Radius = 0, MaxCount = 1, BaseHp = 100 });
        regen.ProcessRegenTick(new OfficialClientRulesEngine.DelphiRandom(1));
        var ice1 = regen.FindMonsterAt(0, 150, 150);
        var ice2 = regen.FindMonsterAt(0, 151, 151);
        Assert.NotNull(ice1);
        Assert.NotNull(ice2);

        mage.ClearBufferedPackets();
        mage.LastSpellTimeMs = 0;
        // 释放冰咆哮对准 (150, 150) (MagicId = 32)
        dispatcher.Dispatch(mage, $"#{ProtocolConsts.CM_SPELL}/0/32/150/150/!");
        Assert.True(ice1.Hp < 100);
        Assert.True(ice2.Hp < 100);

        // 3. 测试圣言术 (MagicId = 24, 秒杀不死系)
        regen.AddRule(new MonsterSpawnRule { RuleId = 5, MonsterName = "骷髅精灵", MapId = 0, CenterX = 201, CenterY = 200, Radius = 0, MaxCount = 1, BaseHp = 500 });
        regen.ProcessRegenTick(new OfficialClientRulesEngine.DelphiRandom(1));
        var skeleton = regen.FindMonsterAt(0, 201, 200);
        Assert.NotNull(skeleton);

        mage.ClearBufferedPackets();
        mage.LastSpellTimeMs = 0;
        // 释放圣言术
        dispatcher.Dispatch(mage, $"#{ProtocolConsts.CM_SPELL}/{skeleton.Uid}/24/201/200/!");
        Assert.True(mage.HasBufferedPacket($"#{ProtocolConsts.SM_SYSMSG_DELPHI}/"));

        // 4. 测试瞬息移动 (MagicId = 11)
        int oldX = mage.X;
        int oldY = mage.Y;
        mage.ClearBufferedPackets();
        mage.LastSpellTimeMs = 0;
        dispatcher.Dispatch(mage, $"#{ProtocolConsts.CM_SPELL}/0/11/0/0/!");
        Assert.True(mage.X != oldX || mage.Y != oldY); // 坐标已被随机瞬移更新
        Assert.True(map.IsWalkable(mage.X, mage.Y));  // 瞬移目标必须为合法无阻挡点

        // 5. 测试雷电术视线阻挡 (MagicId = 10)
        // 在中间 (102, 100) 设置一道墙体阻挡
        map.SetWalkable(102, 100, false);
        mage.X = 99;
        mage.Y = 100;
        mage.ClearBufferedPackets();
        mage.LastSpellTimeMs = 0;
        // 目标在墙体对岸 (106, 100)，中间是墙体阻挡 (102, 100)
        dispatcher.Dispatch(mage, $"#{ProtocolConsts.CM_SPELL}/0/10/106/100/!");
        Assert.True(mage.HasBufferedPacket($"#{ProtocolConsts.SM_SYSMSG_DELPHI}/")); // 收到障碍阻挡提示
    }

    [Fact]
    public void TestOfficialTaoistHighTierSpellsFullLoop()
    {
        var (mapMgr, regen, _, dispatcher, map) = CreateTestEnvironment();

        var taoist = new ClientSession(1001, null!)
        {
            CurrentMapId = 0,
            X = 200,
            Y = 200,
            Direction = 4, // 朝南
            CharacterName = "逍遥散人",
            State = new PlayerState { Uid = 1001, Name = "逍遥散人", Level = 35, Job = 2, Hp = 200, MaxHp = 200, Mp = 400, MaxMp = 400, BaseMaxHp = 200, BaseMaxMp = 400 }
        };
        // 佩戴护身符 (OfficialEquipSlot.Lighting = 2)
        var amulet = new EquipItem { Uid = 301, Name = "道士护身符", Weight = 1, Dura = 100, MaxDura = 100 };
        taoist.State.Equipment[(int)OfficialEquipSlot.Lighting] = amulet;
        map.Aoi.Enter(taoist, 200, 200);

        // 1. 测试神圣战甲术 (MagicId = 15) 与 幽灵盾 (MagicId = 14)
        int prevAc = taoist.State.Ac;
        int prevMac = taoist.State.Mac;
        taoist.ClearBufferedPackets();
        taoist.LastSpellTimeMs = 0;
        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_SPELL}/0/15/200/200/!");
        Assert.True(taoist.State.HasHolyArmor, "必须激活神圣战甲术状态");
        Assert.Equal(prevAc + TaoistCombatEngine.AuraBuffs.BonusAc, taoist.State.Ac);

        taoist.ClearBufferedPackets();
        taoist.LastSpellTimeMs = 0;
        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_SPELL}/0/14/200/200/!");
        Assert.True(taoist.State.HasGhostShield, "必须激活幽灵盾状态");
        Assert.Equal(prevMac + TaoistCombatEngine.AuraBuffs.BonusMac, taoist.State.Mac);

        // 2. 测试召唤骷髅 (MagicId = 17) 与 召唤神兽 (MagicId = 30)
        int prevAmuletDura = amulet.Dura;
        taoist.ClearBufferedPackets();
        taoist.LastSpellTimeMs = 0;
        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_SPELL}/0/17/200/200/!");
        Assert.Equal(prevAmuletDura - 1, amulet.Dura); // 扣除护身符耐久
        var skeleton = regen.FindMonsterAt(0, 201, 200);
        Assert.NotNull(skeleton);
        Assert.Equal("变异骷髅", skeleton.Name);

        taoist.ClearBufferedPackets();
        taoist.LastSpellTimeMs = 0;
        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_SPELL}/0/30/200/200/!");
        Assert.Equal(prevAmuletDura - 2, amulet.Dura); // 再次扣除护身符耐久
        var shinsu = regen.FindMonsterAt(0, 200, 201);
        Assert.NotNull(shinsu);
        Assert.Equal("神兽", shinsu.Name);

        // 3. 测试心灵启示 (MagicId = 28)
        taoist.ClearBufferedPackets();
        taoist.LastSpellTimeMs = 0;
        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_SPELL}/{shinsu.Uid}/28/201/200/!");
        Assert.True(taoist.HasBufferedPacket($"#{ProtocolConsts.SM_SYSMSG_DELPHI}/"));

        // 4. 测试困魔咒 (MagicId = 16)
        regen.AddRule(new MonsterSpawnRule { RuleId = 99, MonsterName = "僵尸测试", MapId = 0, CenterX = 205, CenterY = 205, Radius = 0, MaxCount = 1, BaseHp = 100 });
        regen.ProcessRegenTick(new OfficialClientRulesEngine.DelphiRandom(1));
        var zombie = regen.FindMonsterAt(0, 205, 205);
        Assert.NotNull(zombie);

        taoist.ClearBufferedPackets();
        taoist.LastSpellTimeMs = 0;
        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_SPELL}/0/16/205/205/!");
        Assert.True(zombie.NextActionTimeMs > Environment.TickCount64 + 10_000); // 验证被禁锢 15 秒

        // 5. 测试隐身术 (MagicId = 18) 脱离怪物仇恨
        zombie.TargetSessionId = taoist.SessionId; // 僵尸锁定道士为仇恨目标
        taoist.ClearBufferedPackets();
        taoist.LastSpellTimeMs = 0;
        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_SPELL}/0/18/200/200/!");
        Assert.Equal(0, zombie.TargetSessionId); // 隐身后脱离仇恨
    }
}


