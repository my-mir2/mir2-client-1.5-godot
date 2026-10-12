using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mir2.Server.Game;
using Mir2.Server.Maps;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 盛大官方 1.5 物理与魔法特殊属性（攻速/准确/敏捷/魔法躲避/抗毒）与反外挂权威矩阵测试
/// </summary>
public sealed class TestOfficialM2_CombatAttributesAndAntiHackMatrix
{
    [Fact]
    public void TestGaleWindSpeed_ReducesAttackInterval_And_ValidatesAntiHack()
    {
        // 1. 无装备角色基础出刀间隔 680ms
        var player = new PlayerState();
        player.RecalculateAttributes();
        Assert.Equal(0, player.AttackSpeed);
        Assert.False(EconomySecurityEngine.AttackSpeedValidatorEngine.IsAttackValid(1500, 1000, player.AttackSpeed)); // 500ms 间隔 < 680ms 拦截
        Assert.True(EconomySecurityEngine.AttackSpeedValidatorEngine.IsAttackValid(1700, 1000, player.AttackSpeed));  // 700ms 间隔 >= 680ms 放行

        // 2. 装备狂风项链 (+2 攻速) 与两枚狂风戒指 (+1 攻速 x 2) -> 总攻速 +4
        var necklace = new EquipItem { Name = "狂风项链", Shape = 20 };
        var ring1 = new EquipItem { Name = "狂风戒指", Shape = 22 };
        var ring2 = new EquipItem { Name = "狂风戒指", Shape = 22 };

        player.WearItem(OfficialEquipSlot.Necklace, necklace);
        player.WearItem(OfficialEquipSlot.RingL, ring1);
        player.WearItem(OfficialEquipSlot.RingR, ring2);

        Assert.Equal(4, player.AttackSpeed);

        // 3. 有效间隔从 680ms 缩短为 Clamp(680 - 4*60, 420, 1000) = 440ms
        // 500ms 间隔此时合法通过！
        Assert.True(EconomySecurityEngine.AttackSpeedValidatorEngine.IsAttackValid(1500, 1000, player.AttackSpeed));
        // 400ms 依然超速被拦截
        Assert.False(EconomySecurityEngine.AttackSpeedValidatorEngine.IsAttackValid(1400, 1000, player.AttackSpeed));
    }

    [Fact]
    public void TestAccuracyAndAgility_EquipmentBonusCalculation()
    {
        var player = new PlayerState();
        player.RecalculateAttributes();
        Assert.Equal(15, player.Hit);
        Assert.Equal(15, player.Speed);

        // 装备夏普儿手镯 (+2 准确) 与辟邪手镯 (+2 敏捷)
        var armL = new EquipItem { Name = "夏普儿手镯", Shape = 30 };
        var armR = new EquipItem { Name = "辟邪手镯", Shape = 31 };

        player.WearItem(OfficialEquipSlot.ArmringL, armL);
        player.WearItem(OfficialEquipSlot.ArmringR, armR);

        Assert.Equal(17, player.Hit);
        Assert.Equal(17, player.Speed);

        // 卸下夏普儿手镯，准确回退至 15
        player.TakeOffItem(OfficialEquipSlot.ArmringL);
        Assert.Equal(15, player.Hit);
        Assert.Equal(17, player.Speed);
    }

    [Fact]
    public void TestWhiteTigerNecklace_MagicEvade_ExemptsSingleTargetSpellDamage()
    {
        var map = new GameMap(0, "比奇省", 100, 100);
        var mapManager = new MapManager();
        mapManager.RegisterMap(map);
        var aoi = map.Aoi;
        var dispatcher = new PacketDispatcher(mapManager, null);

        // 施法者法师 session
        var casterSession = new ClientSession(9101, null!)
        {
            CharacterName = "法师雷神",
            CurrentMapId = 0,
            X = 20,
            Y = 20,
            State = new PlayerState { Job = 1, Level = 35, CharacterName = "法师雷神", AttackMode = 0 }
        };
        casterSession.State.RecalculateAttributes();

        // 受害者道士 session，佩戴极品白色虎齿项链 (100% 必定躲避用于测试)
        var tigerNecklace = new EquipItem { Name = "白色虎齿项链", MagicEvade = 100 };
        var targetSession = new ClientSession(9102, null!)
        {
            CharacterName = "逍遥道尊",
            CurrentMapId = 0,
            X = 21,
            Y = 20,
            State = new PlayerState { Job = 2, Level = 35, CharacterName = "逍遥道尊", Hp = 200, MaxHp = 200, AttackMode = 0 }
        };
        targetSession.State.WearItem(OfficialEquipSlot.Necklace, tigerNecklace);

        aoi.Enter(casterSession, 20, 20);
        aoi.Enter(targetSession, 21, 20);

        int hpBefore = targetSession.State.Hp;
        long nowMs = Environment.TickCount64;

        // 法师施放雷电术 (MagicId = 10) 直击受害者道士
        dispatcher.ResolveAuthoritativeSpell(casterSession, magicId: 10, targetUid: targetSession.SessionId, targetX: targetSession.X, targetY: targetSession.Y, nowMs: nowMs);

        // 验证受害者因 100% 魔法躲避，生命值完全未被扣减！
        Assert.Equal(hpBefore, targetSession.State.Hp);

        // 验证受到魔法躲避回显提示
        Assert.True(targetSession.HasBufferedPacket("魔法躲避"));
    }

    [Fact]
    public void TestPoisonBracelet_AntiPoison_ExemptsTaoistPoison()
    {
        var player = new PlayerState();
        player.RecalculateAttributes();
        Assert.Equal(0, player.AntiPoison);

        // 佩戴避毒手镯 (+20 抗毒)
        var poisonBracelet = new EquipItem { Name = "避毒手镯", Shape = 35 };
        player.WearItem(OfficialEquipSlot.ArmringL, poisonBracelet);

        Assert.Equal(20, player.AntiPoison);

        // 极品 100 抗毒
        var superBracelet = new EquipItem { Name = "极品辟邪手镯", AntiPoison = 100 };
        player.WearItem(OfficialEquipSlot.ArmringR, superBracelet);
        Assert.Equal(100, player.AntiPoison);

        // 100% 抗毒检定必定不中毒
        bool hit = TaoistCombatEngine.CheckPoisonHit(player.AntiPoison, Random.Shared);
        Assert.False(hit, "100 点抗毒抗性必定豁免施毒术！");
    }

    [Fact]
    public void TestOverweight_RunBlocked_And_PullbackTriggered()
    {
        var map = new GameMap(0, "比奇省", 100, 100);
        var mapManager = new MapManager();
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager, null);

        var session = new ClientSession(9201, null!)
        {
            CharacterName = "重甲战士",
            CurrentMapId = 0,
            X = 50,
            Y = 50,
            Direction = 2,
            State = new PlayerState
            {
                Level = 35,
                Hp = 200,
                MaxHp = 200,
                BagWeight = 250,      // 严重超重 (大于 200)
                MaxBagWeight = 200
            }
        };
        map.Aoi.Enter(session, 50, 50);

        // 尝试发包跑步跨 2 格 (从 50, 50 跑到 52, 50)
        dispatcher.Dispatch(session, "#3013/52/50/2!");

        // 验证因超重无法助跑，被服务器强制拉回原坐标 (50, 50)
        Assert.Equal(50, session.X);
        Assert.Equal(50, session.Y);
        Assert.True(session.HasBufferedPacket("包裹或穿戴负重过重"));
        Assert.True(session.HasBufferedPacket($"#{ProtocolConsts.SM_SENDUSERSTATE}/"));
    }

    [Fact]
    public void TestZumaLeader_InitialStoneForm_And_RageSummonGuardsOnPhysicalHit()
    {
        var map = new GameMap(0, "祖玛教主之家", 100, 100);
        var mapManager = new MapManager();
        mapManager.RegisterMap(map);
        var regen = new MonsterRegenEngine(mapManager);
        var dispatcher = new PacketDispatcher(mapManager, regen);

        // 注册祖玛教主刷新规则
        regen.AddRule(new MonsterSpawnRule
        {
            RuleId = 8801,
            MonsterName = "祖玛教主",
            MapId = 0,
            CenterX = 30,
            CenterY = 30,
            Radius = 0,
            MaxCount = 1,
            BaseHp = 3000
        });
        regen.ProcessRegenTick(new OfficialClientRulesEngine.DelphiRandom(1));
        var boss = regen.FindMonsterAt(0, 30, 30);
        Assert.NotNull(boss);

        // 验证祖玛教主初始进入石化雕像休眠状态
        Assert.True(boss.IsStoneForm, "祖玛教主初始刷新必须处于石化雕像休眠状态！");

        var warrior = new ClientSession(9301, null!)
        {
            CharacterName = "屠龙战狂",
            CurrentMapId = 0,
            X = 30,
            Y = 31,
            State = new PlayerState { Job = 0, Level = 45, Hp = 500, MaxHp = 500 }
        };
        map.Aoi.Enter(warrior, 30, 31);

        long nowMs = Environment.TickCount64;

        // 物理攻击将教主直接打至残血 15% (3000 * 0.15 = 450 HP)
        int burstDmg = 2550;
        dispatcher.ApplyDamageToMonster(warrior, boss, burstDmg, nowMs);

        // 验证狂暴召唤已触发且解除狂暴重复
        Assert.True(boss.IsMad, "祖玛教主残血必须进入狂暴状态！");
        Assert.True(warrior.HasBufferedPacket("祖玛教主残血暴怒咆哮"));

        // 验证地图上被刷出了祖玛卫士亲卫
        var summoned = regen.FindMonsterAt(0, 31, 31);
        Assert.NotNull(summoned);
        Assert.Contains("祖玛", summoned.Name);
    }

    [Fact]
    public void TestRedMoonDemon_GroundSpikeDamage_CalculatesDualReduction()
    {
        var player = new PlayerState
        {
            Ac = 20,
            Mac = 20
        };

        // 原始地刺伤害 80
        int rawDmg = 80;
        int finalDmg = BossEncounterEngine.CalculateRedMoonDemonGroundSpikeDamage(rawDmg, player);

        // 官方公式: 80 - (20 + 20) / 2 = 80 - 20 = 60
        Assert.Equal(60, finalDmg);
    }

    [Fact]
    public void TestSpecialRings_FlameAndDefense_CrossJobSpellPermissionAndCasting()
    {
        var map = new GameMap(0, "比奇省", 100, 100);
        var mapManager = new MapManager();
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var warrior = new ClientSession(9401, null!)
        {
            CharacterName = "烈火战神",
            CurrentMapId = 0,
            X = 50,
            Y = 50,
            State = new PlayerState
            {
                Job = 0, // 战士
                Level = 40,
                BaseMaxHp = 400,
                MaxHp = 400,
                Hp = 100,
                BaseMaxMp = 100,
                MaxMp = 100,
                Mp = 100
            }
        };
        map.Aoi.Enter(warrior, 50, 50);

        // 1. 战士裸装未戴火焰戒指尝试施展火球术 (MagicId 1: CM_SPELL 3017) -> 必须被服务端坚决拦截
        dispatcher.Dispatch(warrior, "#3017/0/1/52/50/!");
        Assert.True(warrior.HasBufferedPacket("你未佩戴火焰戒指，无法运转真气施展火球术"));
        Assert.Equal(100, warrior.State.Mp); // MP 毫发无损

        // 2. 战士穿戴火焰戒指 (Shape 115) 再次施法
        warrior.ClearBufferedPackets();
        warrior.LastSpellTimeMs = 0; // 重置施法冷却
        warrior.State.Equipment[(int)OfficialEquipSlot.RingL] = new EquipItem
        {
            Uid = 771,
            Name = "火焰戒指",
            Shape = 115,
            Dura = 10,
            MaxDura = 10
        };
        warrior.State.RecalculateAttributes();
        Assert.True(warrior.State.HasFlameRing);

        dispatcher.Dispatch(warrior, "#3017/0/1/52/50/!");
        Assert.False(warrior.HasBufferedPacket("你未佩戴火焰戒指"));
        Assert.True(warrior.State.Mp < 100); // 扣减蓝量施法成功

        // 3. 战士未戴防御戒指尝试施展治愈术 (MagicId 2) -> 必须被拦截
        warrior.ClearBufferedPackets();
        warrior.LastSpellTimeMs = 0;
        dispatcher.Dispatch(warrior, "#3017/0/2/50/50/!");
        Assert.True(warrior.HasBufferedPacket("你未佩戴防御戒指，无法运转神圣力量施展治愈术"));
        Assert.Equal(100, warrior.State.Hp); // HP 未增加

        // 4. 战士佩戴防御戒指 (Shape 116) 再次施展治愈术
        warrior.ClearBufferedPackets();
        warrior.LastSpellTimeMs = 0;
        warrior.State.Equipment[(int)OfficialEquipSlot.RingR] = new EquipItem
        {
            Uid = 772,
            Name = "防御戒指",
            Shape = 116,
            Dura = 10,
            MaxDura = 10
        };
        warrior.State.RecalculateAttributes();
        Assert.True(warrior.State.HasHealingRing);

        dispatcher.Dispatch(warrior, "#3017/0/2/50/50/!");
        Assert.False(warrior.HasBufferedPacket("你未佩戴防御戒指"));
        Assert.True(warrior.State.Hp > 100); // 治愈加血成功
    }

    [Fact]
    public void TestOverloadRing_TakeOff_RestoresWeightLimit_And_BlocksRun()
    {
        var map = new GameMap(0, "比奇省", 100, 100);
        var mapManager = new MapManager();
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var player = new ClientSession(9402, null!)
        {
            CharacterName = "负重测试员",
            CurrentMapId = 0,
            X = 50,
            Y = 50,
            State = new PlayerState
            {
                Job = 1, // 法师
                Level = 30,
                Hp = 200,
                MaxHp = 200
            }
        };
        map.Aoi.Enter(player, 50, 50);

        // 放入真实的 250 负重物品到背包并重算物理负重
        player.State.BagItems.Add(new EquipItem { Uid = 888, Name = "重金矿石", Weight = 250 });
        player.State.RecalculateAuthoritativeWeights(0);

        // 佩戴超负载戒指 (Shape 119)
        player.State.Equipment[(int)OfficialEquipSlot.RingL] = new EquipItem
        {
            Uid = 991,
            Name = "超负载戒指",
            Shape = 119,
            Dura = 5,
            MaxDura = 5
        };
        player.State.RecalculateAttributes();

        // 验证负重上限严格翻倍: 官方 30 级法师基准 230 -> 翻倍为 460
        Assert.True(player.State.HasOverloadRing);
        Assert.Equal(460, player.State.MaxBagWeight);
        Assert.Equal(250, player.State.BagWeight);

        // 奔跑 2 格到 (52, 50) -> 在 460 范围内，正常通过
        dispatcher.Dispatch(player, "#3013/52/50/2!");
        Assert.Equal(52, player.X);
        Assert.False(player.HasBufferedPacket("无法助跑"));

        // 卸下超负载戒指放入背包 (CM_TAKEOFFITEM 3012: slot 7)
        player.ClearBufferedPackets();
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_TAKEOFFITEM}/7/!");
        Assert.Null(player.State.Equipment[(int)OfficialEquipSlot.RingL]);
        Assert.False(player.State.HasOverloadRing);
        Assert.Equal(230, player.State.MaxBagWeight); // 负重上限恢复为官方基准 230

        // 此时背包 250 大于上限 200，处于超重状态！再次奔跑 -> 必须被强制拉回并提示
        dispatcher.Dispatch(player, "#3013/54/50/2!");
        Assert.Equal(52, player.X); // 被拉回原坐标 52, 50
        Assert.True(player.HasBufferedPacket("背包过重"));
    }

    [Fact]
    public void TestPet_PartyKillExpDistribution_And_LevelUpEvolution()
    {
        var map = new GameMap(0, "白日门", 100, 100);
        var mapManager = new MapManager();
        mapManager.RegisterMap(map);
        var regen = new MonsterRegenEngine(mapManager);
        var groupEngine = new GroupEngine();
        var dispatcher = new PacketDispatcher(mapManager, regen, groupEngine: groupEngine);

        var master = new ClientSession(9403, null!)
        {
            CharacterName = "带狗道长",
            CurrentMapId = 0,
            X = 20,
            Y = 20,
            State = new PlayerState { Job = 2, Level = 40, Hp = 300, MaxHp = 300 }
        };
        var partner = new ClientSession(9404, null!)
        {
            CharacterName = "开路狂战",
            CurrentMapId = 0,
            X = 21,
            Y = 20,
            State = new PlayerState { Job = 0, Level = 40, Hp = 500, MaxHp = 500 }
        };
        map.Aoi.Enter(master, 20, 20);
        map.Aoi.Enter(partner, 21, 20);

        // 道长携带一只 0 级虎卫宝宝 (白名, 400 HP, 经验 0)
        var pet = HuWeiPetEngine.CreateHuWei(8801, master.SessionId, 0, 20, 21);
        master.Pets.Add(pet);
        Assert.Equal(0, pet.Level);
        Assert.Equal("White", pet.NameColor);
        Assert.Equal(400, pet.MaxHp);

        // 两者组队
        GroupEngine.ToggleGroupMode(master, true);
        GroupEngine.ToggleGroupMode(partner, true);
        groupEngine.CreateParty(master);
        groupEngine.AddMember(master, partner);

        // 狂战击杀一只经验为 400 点的怪物 (基础经验 400)
        var targetMonster = new MonsterInstance
        {
            Uid = 9991,
            Name = "红野猪",
            MapId = 0,
            X = 21,
            Y = 21,
            Hp = 0,
            MaxHp = 200 // baseExp = MaxHp * 2 = 400
        };

        dispatcher.HandleMonsterDeathAndLoot(partner, targetMonster, map, Environment.TickCount64);

        // 验证道长分到组队经验 (400 / 2 = 200)
        // 随从宝宝分到经验 200 / 2 = 100 点，满足 0->1 级 (100经验门槛)，晋升为 1 级黄名！
        Assert.Equal(1, pet.Level);
        Assert.Equal("Yellow", pet.NameColor);
        Assert.True(pet.MaxHp > 400); // 虎卫血量按官方比例晋升至 540
        Assert.True(master.HasBufferedPacket("已晋升至 1 级（Yellow）"));
    }

    [Fact]
    public void TestSpecialRings_RevivalRing_BrokenAndCooldown_StrictDamageBlock()
    {
        var player = new PlayerState { Hp = 50, MaxHp = 500, Mp = 10, MaxMp = 200 };
        var ring = new EquipItem
        {
            Shape = 114,
            Name = "复活戒指",
            Dura = 2,
            MaxDura = 5
        };
        player.Equipment[(int)OfficialEquipSlot.RingL] = ring;
        player.RecalculateAttributes();

        Assert.True(player.HasReviveRing);
        Assert.Equal(2000, player.ReviveRingDura);

        long t0 = 100_000;

        // 1. 受到致死打击 100 点 (50 -> 濒死)
        var res1 = player.TakeCombatDamageFull(100, t0);
        Assert.True(res1.revived, "第一次致死应触发复活戒指");
        Assert.False(res1.died, "触发复活不可死亡");
        Assert.Equal(500, player.Hp);
        Assert.Equal(200, player.Mp); // 满血满蓝
        Assert.Equal(1000, player.ReviveRingDura); // 扣减 1000 内部持久
        Assert.Equal(1, ring.Dura); // 装备槽位持久同步扣减为 1

        // 2. 在 30 秒冷却时间内 (例如 10 秒后 t0 + 10000) 再次受到致死打击
        var res2 = player.TakeCombatDamageFull(1000, t0 + 10_000);
        Assert.False(res2.revived, "30秒冷却期内不可重复复活");
        Assert.True(res2.died, "冷却未好受到致死伤害角色必须阵亡");
        Assert.Equal(0, player.Hp);

        // 3. 重置血量并在 CD 结束后 (t0 + 35000) 再次受到致死伤害，此时持久降为 0 并破碎
        player.Hp = 50;
        var res3 = player.TakeCombatDamageFull(100, t0 + 35_000);
        Assert.True(res3.revived, "冷却结束后可触发第二次复活");
        Assert.Equal(0, player.ReviveRingDura);
        Assert.Null(player.Equipment[(int)OfficialEquipSlot.RingL]); // 持久用尽破碎脱下

        // 4. 持久为 0 后再次受到致死打击，绝不可再复活
        player.Hp = 50;
        var res4 = player.TakeCombatDamageFull(100, t0 + 70_000);
        Assert.False(res4.revived, "已破碎脱下的复活戒指绝不可再复活");
        Assert.True(res4.died);
    }

    [Fact]
    public void TestPacketDispatcher_PoisonAndFireWall_RevivalRingInterception()
    {
        var map = new GameMap(0, "比奇省", 100, 100);
        var mapManager = new MapManager();
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var playerSession = new ClientSession(9501, null!)
        {
            CharacterName = "测试道士",
            CurrentMapId = 0,
            X = 50,
            Y = 50,
            State = new PlayerState { Level = 40, Hp = 5, MaxHp = 400, Mp = 0, MaxMp = 300 }
        };
        var ring = new EquipItem { Shape = 114, Name = "复活戒指", Dura = 5, MaxDura = 5 };
        playerSession.State.Equipment[(int)OfficialEquipSlot.RingL] = ring;
        playerSession.State.RecalculateAttributes();
        map.Aoi.Enter(playerSession, 50, 50);

        long nowMs = 200_000;

        // 1. 中绿毒且血量仅剩 5，毒素 Tick 扣除 8 点导致濒死
        playerSession.State.HasGreenPoison = true;
        playerSession.State.GreenPoisonExpireMs = nowMs + 10_000;
        playerSession.State.NextGreenPoisonTickMs = nowMs - 1;
        playerSession.State.GreenPoisonSc = 30; // 伤害 8 点

        dispatcher.TickWorldPoisons(nowMs, new[] { playerSession });

        // 验证毒素致死被复活戒指拦截并成功复活，血蓝全满，装备持久扣减
        Assert.Equal(400, playerSession.State.Hp);
        Assert.Equal(300, playerSession.State.Mp);
        Assert.True(playerSession.HasBufferedPacket("【神戒护佑】复活戒指释放神圣光芒，生命值完全恢复！"));
        Assert.False(playerSession.HasBufferedPacket("SM_DEATH_ACTION_DELPHI"), "复活成功不可下发死亡包");
    }

    [Fact]
    public void TestTaoistInvisibility_ClearsNormalMonsterTargetAndThreat_DemonImmune()
    {
        var map = new GameMap(0, "盟重省", 100, 100);
        var mapManager = new MapManager();
        mapManager.RegisterMap(map);
        var regen = new MonsterRegenEngine(mapManager);
        var dispatcher = new PacketDispatcher(mapManager) { RegenEngine = regen };

        var taoist = new ClientSession(9601, null!)
        {
            CharacterName = "天尊道长",
            CurrentMapId = 0,
            X = 40,
            Y = 40,
            State = new PlayerState { Job = 2, Level = 45, Hp = 400, MaxHp = 400, Sc = 30, Mp = 100, MaxMp = 100 }
        };
        // 佩戴道士施法耗材护身符
        taoist.State.Equipment[(int)OfficialEquipSlot.Lighting] = new EquipItem { Name = "护身符", Dura = 10, MaxDura = 10 };
        taoist.State.RecalculateAttributes();
        map.Aoi.Enter(taoist, 40, 40);

        // 1. 放置一只普通怪物 (半兽战士，不可看破隐身) 锁定追击该道士
        var normalOrc = new MonsterInstance
        {
            Uid = 8801,
            Name = "半兽战士",
            MapId = 0,
            X = 41,
            Y = 40,
            Hp = 300,
            MaxHp = 300,
            SeeInvisibility = false,
            TargetSessionId = taoist.SessionId
        };
        normalOrc.ThreatTable.AddHate(taoist.SessionId, 500);
        regen.AddMonster(normalOrc);

        // 2. 放置一只顶级 Boss (赤月恶魔，看破隐身) 锁定追击该道士
        var demonBoss = new MonsterInstance
        {
            Uid = 8802,
            Name = "赤月恶魔",
            MapId = 0,
            X = 40,
            Y = 42,
            Hp = 5000,
            MaxHp = 5000,
            SeeInvisibility = true,
            TargetSessionId = taoist.SessionId
        };
        demonBoss.ThreatTable.AddHate(taoist.SessionId, 1000);
        regen.AddMonster(demonBoss);

        // 3. 道士施放隐身术 (MagicId = 18)
        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_SPELL}/0/18/40/40/!");

        // 4. 验证道士进入隐身状态
        Assert.True(taoist.State.IsInvisible);

        // 5. 验证普通半兽战士瞬间脱战清空仇恨，而赤月恶魔看破隐身继续锁定！
        Assert.Equal(0, normalOrc.TargetSessionId);
        Assert.Equal(0, normalOrc.ThreatTable.PrimaryTargetId);

        Assert.Equal(taoist.SessionId, demonBoss.TargetSessionId);
        Assert.Equal(taoist.SessionId, demonBoss.ThreatTable.PrimaryTargetId);
    }

    [Fact]
    public void TestTaoistHeal_SingleAndMassHeal_HealsPetsAndUsesTargetCoordinates()
    {
        var map = new GameMap(0, "盟重省", 100, 100);
        var mapManager = new MapManager();
        mapManager.RegisterMap(map);
        var regen = new MonsterRegenEngine(mapManager);
        var dispatcher = new PacketDispatcher(mapManager) { RegenEngine = regen };

        var taoist = new ClientSession(9701, null!)
        {
            CharacterName = "逍遥道尊",
            CurrentMapId = 0,
            X = 30,
            Y = 30,
            State = new PlayerState { Job = 2, Level = 45, Hp = 400, MaxHp = 400, Sc = 35, Mp = 500, MaxMp = 500 }
        };
        taoist.State.Equipment[(int)OfficialEquipSlot.Lighting] = new EquipItem { Name = "护身符", Dura = 50, MaxDura = 50 };
        taoist.State.RecalculateAttributes();
        map.Aoi.Enter(taoist, 30, 30);

        // 1. 道士携带一只残血神兽 (200 / 1200 HP)
        var shinsu = new MonsterInstance
        {
            Uid = 7701,
            Name = "神兽",
            MapId = 0,
            X = 31,
            Y = 30,
            Hp = 200,
            MaxHp = 1200,
            MasterSessionId = taoist.SessionId
        };
        regen.AddMonster(shinsu);
        var slave = new HuWeiPetEngine.SlavePet
        {
            Uid = shinsu.Uid,
            Name = shinsu.Name,
            MasterSessionId = taoist.SessionId,
            Hp = shinsu.Hp,
            MaxHp = shinsu.MaxHp
        };
        taoist.Pets.Add(slave);

        // 2. 道士锁定神兽施放单体治愈术 (MagicId = 2)
        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_SPELL}/{shinsu.Uid}/2/{shinsu.X}/{shinsu.Y}/!");

        // 验证神兽血量增加且 slave.Hp 同步更新
        Assert.True(shinsu.Hp > 200, "神兽受到单体治愈术必须回血");
        Assert.Equal(shinsu.Hp, slave.Hp);
        Assert.True(taoist.HasBufferedPacket("你为随从【神兽】恢复了"));

        // 3. 施法视距内目标点 (35, 35) (距离 5 格 <= 12 格限制) 处放置一位残血队友战士与神兽
        var warrior = new ClientSession(9702, null!)
        {
            CharacterName = "冲锋战士",
            CurrentMapId = 0,
            X = 35,
            Y = 36,
            State = new PlayerState { Job = 0, Level = 40, Hp = 100, MaxHp = 600 }
        };
        map.Aoi.Enter(warrior, 35, 36);
        shinsu.X = 35;
        shinsu.Y = 34;
        int petHpBeforeMass = shinsu.Hp;

        // 道士重置公共施法 CD 后向 (35, 35) 施放群体治愈术 (MagicId = 29)
        taoist.LastSpellTimeMs = 0;
        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_SPELL}/0/29/35/35/!");

        // 验证队友战士与神兽在 (35, 35) 3x3 范围内均获得回血
        Assert.True(warrior.State.Hp > 100, "远程目标点 3x3 范围内的队友战士必须获得群疗回血");
        Assert.True(shinsu.Hp > petHpBeforeMass, "远程目标点 3x3 范围内的神兽宝宝必须获得群疗回血");
        Assert.Equal(shinsu.Hp, slave.Hp);
    }
}

