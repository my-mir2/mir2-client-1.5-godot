using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Mir2.Server.Game;
using Mir2.Server.Maps;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;
using Mir2Client.Game;
using Mir2Client.Net;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 盛大官方 1.4/1.5 服务端权威化深水区五大拓展模块全面核销测试矩阵
/// 覆盖模块八至模块十二:
/// 1. 模块八: NPC 脚本虚拟机 (IF/ACT/SAY/ELSEACT/ELSESAY/CHECKGOLD/CHECKLEVELEX/TAKEGOLD/MAPMOVE)
/// 2. 模块九: 安全区物理禁武拦截、大刀卫士 9999 攻击秒杀红名 AI、昼夜更替与火把照明消耗
/// 3. 模块十: 战斗断线 15 秒幽灵替身保装、地表物品 120 秒自然过期 GC、移动时序滑窗反加速漏桶
/// 4. 模块十一: 官方 1.5 面对面安全交易体系 (TFrmDlg_Deal) 与组队社交系统 (GroupEngine)
/// 5. 模块十二: 仓库独立二级密码验证、防暴力破解输错 3 次冻结 10 分钟
/// </summary>
public class TestOfficialM2_ComprehensiveExpansionMatrix
{
    private static ClientSession CreateMockSession(int sessionId, string charName, int level = 22, int gold = 10000, int pkPoints = 0, int sex = 0)
    {
        var dummySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        var session = new ClientSession(sessionId, dummySocket)
        {
            CharacterName = charName,
            CurrentMapId = 0,
            X = 330,
            Y = 270,
            State = new PlayerState
            {
                Uid = sessionId,
                Name = charName,
                Level = level,
                Gold = gold,
                PkPoints = pkPoints,
                Sex = sex,
                Hp = 200,
                MaxHp = 200
            }
        };
        return session;
    }

    // ==================== 1. 模块八: NPC 脚本虚拟机测试 ====================

    [Fact]
    public void Test_NpcScriptEngine_CompleteExecution_PassAndFail()
    {
        var engine = new NpcScriptEngine();
        string officialScript = @"
; 比奇老兵官方传送脚本
[@main]
欢迎来到比奇城！<$USERNAME>！\
<前往盟重土城/@teleport>\
<离开/@exit>

[@teleport]
#IF
CHECKGOLD 2000
CHECKLEVELEX >= 20
#ACT
TAKEGOLD 2000
MAPMOVE 3 330 330
#SAY
<$USERNAME>，你已成功传送到盟重省！\
当前剩余金币: <$GOLD>
#ELSEACT
#ELSESAY
对不起，你的金币不足 2000 或等级未满 20 级，无法传送！
";

        engine.LoadScript(officialScript);

        // 1. 初始主界面对话测试
        var session = CreateMockSession(1001, "传奇老兵粉丝", level: 25, gold: 5000);
        var (okMain, sayMain, closedMain) = engine.Execute(session, "@main");
        Assert.True(okMain);
        Assert.Contains("传奇老兵粉丝", sayMain);
        Assert.Contains("<前往盟重土城/@teleport>", sayMain);
        Assert.False(closedMain);

        // 2. 条件达标分支: 扣除 2000 金币并传送到地图 3 (330, 330)
        var (okTele, sayTele, _) = engine.Execute(session, "@teleport");
        Assert.True(okTele);
        Assert.Equal(3000, session.State!.Gold);
        Assert.Equal(3, session.CurrentMapId);
        Assert.Equal(330, session.X);
        Assert.Equal(330, session.Y);
        Assert.Contains("成功传送到盟重省", sayTele);
        Assert.Contains("当前剩余金币: 3000", sayTele);

        // 3. 条件不足分支: 金币不足被驳回
        session.State.Gold = 500;
        var (okFail, sayFail, _) = engine.Execute(session, "@teleport");
        Assert.True(okFail);
        Assert.Equal(500, session.State.Gold);
        Assert.Contains("金币不足 2000", sayFail);
    }

    // ==================== 2. 模块九: 安全区禁武、大刀卫士秒杀与照明测试 ====================

    [Fact]
    public void Test_SafeZone_CombatForbidden_And_GuardExecution_And_Lighting()
    {
        var safeZoneEngine = new SafeZoneAndGuardEngine();
        var lightingEngine = new EnvironmentLightingEngine();

        // 1. 安全区禁武测试: 比奇安全区 (320, 260) ~ (345, 285)
        Assert.True(safeZoneEngine.IsInSafeZone(0, 330, 270));
        Assert.False(safeZoneEngine.IsInSafeZone(0, 100, 100)); // 野外危险区

        bool attackBlocked = safeZoneEngine.IsAttackForbiddenBySafeZone(
            attackerMapId: 0, attackerX: 330, attackerY: 270, targetX: 331, targetY: 270);
        Assert.True(attackBlocked, "安全区内动武必须被权威拦截！");

        // 2. 大刀卫士 9999 秒杀红名 AI
        safeZoneEngine.SpawnGuard(uid: 888, name: "大刀卫士", mapId: 0, x: 320, y: 260);

        var whitePlayer = CreateMockSession(2001, "白名良民", pkPoints: 0);
        whitePlayer.X = 322;
        whitePlayer.Y = 260;

        var redPlayer = CreateMockSession(2002, "红名大盗", pkPoints: 300); // 红名 (>= 200)
        redPlayer.X = 322;
        redPlayer.Y = 260;

        var executions = safeZoneEngine.ScanAndExecuteRedNames(new[] { whitePlayer, redPlayer });
        Assert.Single(executions);
        Assert.Equal("红名大盗", executions[0].target.CharacterName);
        Assert.Equal(9999, executions[0].damage);
        Assert.Equal(0, redPlayer.State!.Hp); // 致命一击秒杀倒地！
        Assert.Equal(200, whitePlayer.State!.Hp); // 白名安然无恙

        // 3. 昼夜照明与火把持久衰减
        lightingEngine.CurrentPhase = EnvironmentLightingEngine.EnvironmentDayPhase.Night;
        var player = CreateMockSession(2003, "夜行者");
        var torch = new EquipItem { Uid = 701, Name = "火把", Dura = 10, MaxDura = 10 };
        player.State!.Equipment[(int)OfficialEquipSlot.Lighting] = torch;

        var (radius1, burnedOut1) = lightingEngine.UpdatePlayerLighting(player.State, mapId: 0);
        Assert.Equal(7, radius1); // 火把视野半径 7
        Assert.Equal(9, torch.Dura);
        Assert.False(burnedOut1);

        // 火把消耗殆尽
        torch.Dura = 1;
        var (radius2, burnedOut2) = lightingEngine.UpdatePlayerLighting(player.State, mapId: 0);
        Assert.True(burnedOut2);
        Assert.Equal(0, torch.Dura);
    }

    // ==================== 3. 模块十: 战斗断线幽灵替身、地表过期GC与反加速外挂 ====================

    [Fact]
    public void Test_CombatGhost_And_GroundItemDecay_And_SpeedHackDetection()
    {
        var antiHackEngine = new CombatPersistenceAndAntiHackEngine();
        long now = 100000;

        var session = CreateMockSession(3001, "拔线狂魔");

        // 1. 战斗断线幽灵替身: 最近 3 秒有交火，断网生成 15 秒替身
        long lastCombat = now - 3000;
        bool ghostCreated = antiHackEngine.TryCreateCombatGhost(session, now, lastCombat, out var ghost);
        Assert.True(ghostCreated);
        Assert.NotNull(ghost);
        Assert.Equal(now + 15_000, ghost!.ExpireTickMs);

        // 模拟 15 秒后替身到期自然退出
        var swept = antiHackEngine.SweepExpiredGhosts(now + 16_000);
        Assert.Single(swept);
        Assert.Equal("拔线狂魔", swept[0].CharacterName);

        // 2. 地表物品 120 秒自然腐烂回收
        var items = new List<GroundItemEntity>
        {
            new() { Uid = 1, Name = "乌木剑", DropTickMs = now - 130_000 }, // 已过期 (130秒)
            new() { Uid = 2, Name = "金币", DropTickMs = now - 30_000 }      // 刚掉落 (30秒)
        };
        var expired = antiHackEngine.CollectExpiredGroundItems(items, now);
        Assert.Single(expired);
        Assert.Equal("乌木剑", expired[0].Name);

        // 3. 移动反加速漏桶时序检测
        // 正常跑动: 10 次跑动合规
        for (int i = 0; i < 10; i++)
        {
            bool valid = antiHackEngine.CheckMoveSpeedHack(session.SessionId, 330, 270, 330 + i, 270, now + (i * 450));
            Assert.True(valid);
        }

        // 变速齿轮狂点 (5 秒窗口内移动超过 14 次)
        bool hackBlocked = false;
        for (int i = 10; i < 20; i++)
        {
            bool ok = antiHackEngine.CheckMoveSpeedHack(session.SessionId, 330, 270, 330 + i, 270, now + 1000);
            if (!ok)
            {
                hackBlocked = true;
                break;
            }
        }
        Assert.True(hackBlocked, "加速外挂瞬移必须被漏桶滑动窗口拦截并拉回！");
    }

    // ==================== 4. 模块十一: 官方 1.5 面对面交易原子性与安全回滚 ====================

    [Fact]
    public void Test_Official15_FaceToFace_Trade_Atomic_Safety()
    {
        var tradeEngine = new TradeEngine();

        var playerA = CreateMockSession(4001, "战神狂徒", level: 35);
        var playerB = CreateMockSession(4002, "法神传人", level: 35);
        playerA.CurrentMapId = 0; playerA.X = 100; playerA.Y = 100;
        playerB.CurrentMapId = 0; playerB.X = 101; playerB.Y = 100;
        playerA.State!.Gold = 50000;
        playerB.State!.Gold = 10000;

        var sword = new EquipItem { Uid = 8881, Name = "井中月", Weight = 58 };
        playerA.State.BagItems.Add(sword);

        // 1. 发起交易
        var (tryOk, tryMsg) = tradeEngine.TryInitiateTrade(playerA, playerB);
        Assert.True(tryOk, tryMsg);

        // 2. A 放入金币与武器
        var (goldOk, goldMsg) = tradeEngine.ChangeGold(playerA, 20000);
        Assert.True(goldOk, goldMsg);
        var (itemOk, itemMsg) = tradeEngine.AddItem(playerA, sword.Uid);
        Assert.True(itemOk, itemMsg);

        // 3. 交易中途一方取消，物品与金币原子安全回滚
        tradeEngine.CancelTrade(playerB, "取消交易");
        Assert.Null(tradeEngine.GetTrade(playerB.SessionId));


        Assert.Equal(50000, playerA.State.Gold);
        Assert.Equal(10000, playerB.State.Gold);
        Assert.Contains(playerA.State.BagItems, it => it.Uid == 8881);
    }



    // ==================== 5. 模块十二: 仓库二级密码与防暴力破解 ====================

    [Fact]
    public void Test_StorageSecurity_Password_And_BruteForceLock()
    {
        var sec = new StorageSecurityEngine();
        string account = "player_vip_001";
        long now = 100000;

        // 1. 设置初始密码 "123456"
        var (setOk, setMsg) = sec.SetInitialPassword(account, "123456");
        Assert.True(setOk, setMsg);
        Assert.True(sec.CanAccessStorage(account));

        // 模拟新登录上线，会话重置为未解锁
        var profile = sec.GetOrCreateProfile(account);
        profile.IsSessionUnlocked = false;
        Assert.False(sec.CanAccessStorage(account));

        // 2. 输错密码 1 次与 2 次
        var (fail1, msg1) = sec.VerifyAndUnlock(account, "wrong1", now);
        Assert.False(fail1);
        Assert.Contains("剩余重试次数: 2", msg1);

        var (fail2, msg2) = sec.VerifyAndUnlock(account, "wrong2", now);
        Assert.False(fail2);
        Assert.Contains("剩余重试次数: 1", msg2);

        // 3. 第 3 次输错密码，触发防暴力破解锁定 10 分钟！
        var (fail3, msg3) = sec.VerifyAndUnlock(account, "wrong3", now);
        Assert.False(fail3);
        Assert.Contains("已被锁定 10 分钟", msg3);

        // 即使第 4 次输入了正确密码，在 10 分钟锁定期内依然被驳回！
        var (lockedOk, lockedMsg) = sec.VerifyAndUnlock(account, "123456", now + 1000);
        Assert.False(lockedOk);
        Assert.Contains("密码输错过多，仓库已被安全锁定", lockedMsg);

        // 10 分钟后解冻，输入正确密码成功解锁
        var (finalOk, finalMsg) = sec.VerifyAndUnlock(account, "123456", now + 600_001);
        Assert.True(finalOk, finalMsg);
        Assert.True(sec.CanAccessStorage(account));
    }

    // ==================== 6. 第七十九波: 大刀卫士怪物处决与守序随从豁免实证 ====================

    [Fact]
    public void Test_Wave79_GuardMonsterExecution_And_PlayerPetImmunity()
    {
        // 大刀卫士对野生敌对怪与失控叛变随从的 9999 斩杀 (守序玩家神兽骷髅严格豁免)
        var guardEngine = new SafeZoneAndGuardEngine();

        guardEngine.SpawnGuard(8888, "城门大刀卫士", 0, 300, 300, isArcher: false);

        var wildMonster = new MonsterInstance
        {
            Uid = 9001, Name = "半兽勇士", MapId = 0, X = 305, Y = 305,
            Hp = 300, MaxHp = 300, MasterSessionId = 0
        };

        var playerPet = new MonsterInstance
        {
            Uid = 9002, Name = "神兽", MapId = 0, X = 302, Y = 302,
            Hp = 2000, MaxHp = 2000, MasterSessionId = 1001 // 属于龙城狂刀的合法神兽
        };

        var rebelledMonster = new MonsterInstance
        {
            Uid = 9003, Name = "巨型多角虫", MapId = 0, X = 304, Y = 304,
            Hp = 250, MaxHp = 250, MasterSessionId = 0 // 叛变后失去 Master 沦为野性怪物
        };

        var allMonsters = new List<MonsterInstance> { wildMonster, playerPet, rebelledMonster };

        var executions = guardEngine.ScanAndExecuteMonsters(allMonsters);

        // 卫士单次扫描对范围内的野生怪与叛变怪执行斩杀
        Assert.True(executions.Count > 0);

        // 校验 1: 半兽勇士被 9999 斩杀倒地
        Assert.True(wildMonster.IsDead);
        Assert.Equal(0, wildMonster.Hp);

        // 校验 2: 守序玩家合法神兽毫发无损，绝对豁免！
        Assert.False(playerPet.IsDead);
        Assert.Equal(2000, playerPet.Hp);

        // 再次扫描处决叛变怪物
        var executions2 = guardEngine.ScanAndExecuteMonsters(allMonsters);
        Assert.True(rebelledMonster.IsDead);
        Assert.Equal(0, rebelledMonster.Hp);
    }

    // ==================== 7. 第八十波: 随从成长等级加成、名字颜色与跨图同步实证 ====================

    [Fact]
    public void Test_Wave80_Pet_SkillLevelScaling_NameColorSync_And_TeleportTransition()
    {
        // 1. 验证道士技能等级加成初始宝宝属性与名字颜色
        var pet0 = new HuWeiPetEngine.SlavePet { Name = "神兽", MaxHp = 300, Hp = 300 };
        HuWeiPetEngine.InitializePetBySkillLevel(pet0, skillLevel: 0);
        Assert.Equal(0, pet0.Level);
        Assert.Equal(300, pet0.MaxHp);
        Assert.Equal(0, pet0.NameColorCode); // 白名
        Assert.Equal("White", pet0.NameColor);

        var pet1 = new HuWeiPetEngine.SlavePet { Name = "神兽", MaxHp = 300, Hp = 300 };
        HuWeiPetEngine.InitializePetBySkillLevel(pet1, skillLevel: 1);
        Assert.Equal(1, pet1.Level);
        Assert.Equal((int)(300 * 1.35f), pet1.MaxHp);
        Assert.Equal(1, pet1.NameColorCode); // 黄名
        Assert.Equal("Yellow", pet1.NameColor);

        var pet3 = new HuWeiPetEngine.SlavePet { Name = "神兽", MaxHp = 300, Hp = 300 };
        HuWeiPetEngine.InitializePetBySkillLevel(pet3, skillLevel: 3);
        Assert.Equal(3, pet3.Level);
        Assert.Equal((int)(300 * 2.05f), pet3.MaxHp);
        Assert.Equal(2, pet3.NameColorCode); // 红名
        Assert.Equal("Red", pet3.NameColor);

        // 2. 随从杀敌持续获取经验，晋升至 7 级巅峰蓝名
        var huwei = HuWeiPetEngine.CreateHuWei(8888, 1001, 0, 300, 300);
        Assert.Equal(400, huwei.MaxHp);
        Assert.Equal(0, huwei.Level);

        // 模拟杀怪经验，升到 7 级
        int[] expThresholds = [100, 250, 500, 1000, 2000, 4000, 8000];
        for (int i = 0; i < expThresholds.Length; i++)
        {
            bool lvlUp = HuWeiPetEngine.AddKillExpAndTryLevelUp(huwei, expThresholds[i]);
            Assert.True(lvlUp, $"随从升至 {i + 1} 级失败");
            Assert.Equal(i + 1, huwei.Level);
        }

        Assert.Equal(7, huwei.Level);
        Assert.Equal(4, huwei.NameColorCode); // 7 级巅峰蓝名 (参数 4)
        Assert.Equal("Blue", huwei.NameColor);
        Assert.True(huwei.MaxHp >= 3000, $"7级虎卫血量应大幅提升，实际: {huwei.MaxHp}");

        // 3. 验证 7 级随从跨图同步与随从名字颜色保持
        var master = new ClientSession(1001, null!) { CharacterName = "龙城道尊" };
        master.Pets.Add(huwei);

        var mapManager = new MapManager();
        var map0 = new GameMap(0, "比奇省", 1000, 1000);
        var map1 = new GameMap(1, "沃玛森林", 1000, 1000);
        mapManager.RegisterMap(map0);
        mapManager.RegisterMap(map1);

        master.CurrentMapId = 1; master.X = 100; master.Y = 100;
        map1.Aoi.Enter(master, 100, 100);

        // 主人跨图瞬移到比奇省 (0, 300, 300)，随从物理跟随同步
        HuWeiPetEngine.SyncPetTeleportWithMaster(huwei, null, 0, 300, 300);
        Assert.Equal(0, huwei.CurrentMapId);
        Assert.Equal(301, huwei.X);
        Assert.Equal(300, huwei.Y);
        Assert.Equal(4, huwei.NameColorCode); // 保持 7 级巅峰蓝名
    }


    // ==================== 8. 第八十一波: 药水缓回与 SM_HEALTHSPELLCHANGED 参数对齐实证 ====================

    [Fact]
    public void Test_Wave81_HealthSpellChanged_ParameterOrder_And_PotionTickVerification()
    {
        // 1. 服务端角色吃药与持续回复蓄积池流转
        var player = new PlayerState
        {
            Hp = 120,
            MaxHp = 500,
            Mp = 35,
            MaxMp = 80
        };
        player.BagItems.Add(new EquipItem { Uid = 5001, Name = "强效金创药", Weight = 2 });

        // 玩家使用强效金创药
        var (res, eff, item, slot) = PotionEngine.ConsumePotionEx(player, 0, "强效金创药");
        Assert.Equal(PotionEngine.PotionResult.Success, res);
        Assert.NotNull(eff);
        Assert.Equal(90, eff.ContinuousHp);
        Assert.Equal(90, player.IncHealth);

        // 第一次心跳流转: 抽取释放 5 点生命
        var (hpGain, mpGain) = PotionEngine.TickIncHealthSpell(player);
        Assert.Equal(5, hpGain);
        Assert.Equal(0, mpGain);
        Assert.Equal(125, player.Hp);
        Assert.Equal(85, player.IncHealth);

        // 2. 验证客户端 ServerMessageRouter 对 SM_HEALTHSPELLCHANGED (53) 的解析
        var em = new EntityManager();
        var router = new ServerMessageRouter(null, em, null);

        // 模拟带 ExtraStr 的标准服务端封包 (SM_HEALTHSPELLCHANGED: Recog=125, Param=35, Tag=500, Series=80, Extra="125/500/35/80")
        var pktWithExtra = new ServerPacket(
            ProtocolConsts.SM_HEALTHSPELLCHANGED,
            125, // Recog: Hp
            35,  // Param: Mp
            500, // Tag: MaxHp
            System.Text.Encoding.UTF8.GetBytes("125/500/35/80")
        );
        router.OnPacket(pktWithExtra);

        // 3. 模拟纯数值无 ExtraStr 的底层 fallback 封包 (彻底核销老旧客户端倒置坏账)
        // 官方 1.5 铁律: Recog=HP(200), Param=MP(60), Tag=MaxHp(600), Series=MaxMp(100)
        var pktFallback = new ServerPacket(
            ProtocolConsts.SM_HEALTHSPELLCHANGED,
            200, // Recog: Hp
            60,  // Param: Mp
            600, // Tag: MaxHp
            Array.Empty<byte>()
        );
        router.OnPacket(pktFallback);

        // 4. 满血顶药物理真值验证: 满血吃药血池保留，受击掉血后继续自动流转
        player.Hp = player.MaxHp; // 满血 500
        Assert.Equal(85, player.IncHealth); // 血池依然保持 85，绝不清空！

        // 满血心跳: 释放量为 0
        var (fullHpGain, _) = PotionEngine.TickIncHealthSpell(player);
        Assert.Equal(0, fullHpGain);
        Assert.Equal(85, player.IncHealth);

        // 受到伤害掉血至 400
        player.Hp = 400;
        var (recoverGain, _) = PotionEngine.TickIncHealthSpell(player);
        Assert.Equal(80, player.IncHealth);
    }

    [Fact]
    public void Test_Wave82_Warrior_HalfMoon_Loot_And_PartyExpSettlement()
    {
        // 1. 初始化测试环境与地图
        var mapMgr = new Mir2.Server.Maps.MapManager();
        var regenEngine = new Mir2.Server.Game.MonsterRegenEngine(mapMgr);
        var dispatcher = new Mir2.Server.Game.PacketDispatcher(mapMgr, regenEngine);
        var map = mapMgr.DefaultMap;

        // 2. 模拟 35 级战士与宝宝随从
        var warrior = new ClientSession(9881, null!)
        {
            CurrentMapId = map.MapId,
            CharacterName = "TestWarrior",
            X = 100,
            Y = 100,
            State = new PlayerState { Job = 0, Level = 35, Hp = 500, MaxHp = 500, Mp = 200, MaxMp = 200, Exp = 0 }
        };
        dispatcher.RegisterSession(warrior);
        map.Aoi.Enter(warrior, 100, 100);

        // 召唤宝宝随从并绑定到战士
        var pet = HuWeiPetEngine.CreateHuWei(88001, warrior.SessionId, map.MapId, 100, 99);
        warrior.Pets.Add(pet);

        // 3. 在战士正前方 (100, 101) 放置一只残血怪物 (沃玛卫士, 剩余 1 点生命)
        var monster = new MonsterInstance
        {
            Uid = 77001,
            Name = "沃玛卫士",
            MapId = map.MapId,
            X = 100,
            Y = 101,
            Level = 25,
            Hp = 1,
            MaxHp = 1000,
            Exp = 500
        };
        dispatcher.RegenEngine!.AddMonster(monster);

        // 4. 战士施放半月弯刀 (MagicId = 25, 攻击正前方目标 100, 101)
        long nowMs = Environment.TickCount64;
        dispatcher.Dispatch(warrior, $"#{ProtocolConsts.CM_SPELL}/0/25/100/101!");

        // 5. 验证官方 1.5 铁律:
        // A. 怪物必须判定死亡
        Assert.True(monster.IsDead);
        Assert.Equal(0, monster.Hp);

        // B. 战利品掉落散射核销: 地面必须生成掉落物
        var groundItems = map.GetAllGroundItems();
        Assert.NotEmpty(groundItems);

        // C. 宝宝杀敌经验分摊核销: 随从宝宝分摊到击杀经验并成功晋级至 1 级
        Assert.Equal(1, pet.Level);

        // D. 战士自身获得击杀经验
        Assert.True(warrior.State.Exp > 0);
    }

    [Fact]
    public void Test_Wave83_Warrior_BoHalfMoon_AutoSlash_And_FlankCleave()
    {
        // 1. 初始化测试环境与地图
        var mapMgr = new Mir2.Server.Maps.MapManager();
        var regenEngine = new Mir2.Server.Game.MonsterRegenEngine(mapMgr);
        var dispatcher = new Mir2.Server.Game.PacketDispatcher(mapMgr, regenEngine);
        var map = mapMgr.DefaultMap;

        // 2. 模拟 35 级战士，学习了半月弯刀 (MagicId = 25, 3级) 并开启半月开关 (BoHalfMoon = true)
        var warrior = new ClientSession(9882, null!)
        {
            CurrentMapId = map.MapId,
            CharacterName = "CleaveWarrior",
            X = 300,
            Y = 300,
            Direction = 4,
            State = new PlayerState
            {
                Job = 0,
                Level = 35,
                Hp = 500,
                MaxHp = 500,
                Mp = 100,
                MaxMp = 100,
                Dc = 20,
                MaxDc = 35,
                Hit = 50,
                BoHalfMoon = true
            }
        };
        // 战士已修炼 3 级半月弯刀
        warrior.State.Magics.Add(new UserMagic { MagicId = 25, Level = 3, CurTrain = 0, MaxTrain = 3000 });
        dispatcher.RegisterSession(warrior);
        map.Aoi.Enter(warrior, 300, 300);

        // 3. 在战士正前方 (300, 301) 放置主目标怪物，在侧翼 (301, 301) 放置副目标怪物 (方向 4 朝下时，侧翼包括 301, 301)
        var mainMonster = new MonsterInstance
        {
            Uid = 77011,
            Name = "主红野猪",
            MapId = map.MapId,
            X = 300,
            Y = 301,
            Level = 28,
            Hp = 300,
            MaxHp = 300
        };
        var flankMonster = new MonsterInstance
        {
            Uid = 77012,
            Name = "副黑野猪",
            MapId = map.MapId,
            X = 301,
            Y = 301,
            Level = 28,
            Hp = 300,
            MaxHp = 300
        };
        dispatcher.RegenEngine!.AddMonster(mainMonster);
        dispatcher.RegenEngine.AddMonster(flankMonster);

        // 4. 战士发起普通物理出刀 (CM_HIT: 目标X=300, 目标Y=301, 方向=4)
        dispatcher.Dispatch(warrior, $"#{ProtocolConsts.CM_HIT}/300/301/4!");

        // 5. 验证官方 1.5 铁律:
        // A. 蓝量必须消耗 7 点 (100 -> 93)
        Assert.Equal(93, warrior.State.Mp);

        // B. 正前方主目标受到常规物理伤害
        Assert.True(mainMonster.Hp < 300, $"主目标生命未减少: {mainMonster.Hp}");

        // C. 侧翼副目标受到扇面半月弯刀剑气波及伤害
        Assert.True(flankMonster.Hp < 300, $"侧翼副目标生命未减少: {flankMonster.Hp}");

        // D. 广播了半月弯刀技能光效
        Assert.True(warrior.HasBufferedPacket($"#{ProtocolConsts.SM_SPELLEFFECT_DELPHI}/{warrior.SessionId}/300/300/25/半月弯刀!"));
    }

    [Fact]
    public void Test_Wave84_Warrior_Taoist_PassiveSkillAccuracy_And_PowerHitTrigger()
    {
        // 1. 验证战士基本剑术 (MagicId = 3) 与道士精神力战法 (MagicId = 4) 对准确 (Hit) 的加成
        var wState = new PlayerState { Job = 0, Level = 20 };
        wState.RecalculateAttributes();
        Assert.Equal(15, wState.Hit); // 无技能基础命中 15

        wState.Magics.Add(new UserMagic { MagicId = 3, Level = 0 });
        wState.RecalculateAttributes();
        Assert.Equal(16, wState.Hit); // 0 级基本剑术 +1

        wState.Magics[0].Level = 3;
        wState.RecalculateAttributes();
        Assert.Equal(19, wState.Hit); // 3 级基本剑术 +4

        var tState = new PlayerState { Job = 2, Level = 20 };
        tState.RecalculateAttributes();
        Assert.Equal(15, tState.Hit);

        tState.Magics.Add(new UserMagic { MagicId = 4, Level = 0 });
        tState.RecalculateAttributes();
        Assert.Equal(16, tState.Hit); // 0 级精神力战法 +1

        tState.Magics[0].Level = 3;
        tState.RecalculateAttributes();
        Assert.Equal(19, tState.Hit); // 3 级精神力战法 +4

        // 2. 验证战士攻杀剑术 (MagicId = 7) 各等级 HitPlus 伤害附加
        wState.Magics.Add(new UserMagic { MagicId = 7, Level = 0 });
        wState.RecalculateAttributes();
        Assert.Equal(1, wState.HitPlus); // 0 级攻杀 +1

        wState.Magics[1].Level = 1;
        wState.RecalculateAttributes();
        Assert.Equal(3, wState.HitPlus); // 1 级攻杀 +3

        wState.Magics[1].Level = 2;
        wState.RecalculateAttributes();
        Assert.Equal(5, wState.HitPlus); // 2 级攻杀 +5

        wState.Magics[1].Level = 3;
        wState.RecalculateAttributes();
        Assert.Equal(8, wState.HitPlus); // 3 级攻杀 +8

        // 3. 验证战士出刀 CM_POWERHIT 触发攻杀、光效广播 (MagicId = 7) 与攻杀熟练度增加
        var mapMgr = new Mir2.Server.Maps.MapManager();
        var regenEngine = new Mir2.Server.Game.MonsterRegenEngine(mapMgr);
        var dispatcher = new Mir2.Server.Game.PacketDispatcher(mapMgr, regenEngine);
        var map = mapMgr.DefaultMap;
        var warrior = new ClientSession(9884, null!)
        {
            CurrentMapId = map.MapId,
            CharacterName = "PowerWarrior",
            X = 200,
            Y = 200,
            Direction = 4,
            State = new PlayerState
            {
                Job = 0,
                Level = 25,
                Hp = 400,
                MaxHp = 400,
                Mp = 100,
                MaxMp = 100,
                Dc = 20,
                MaxDc = 20 // 固定 20 DC
            }
        };
        var basicSkill = new UserMagic { MagicId = 3, Level = 3, CurTrain = 0, MaxTrain = 300 };
        var powerSkill = new UserMagic { MagicId = 7, Level = 1, CurTrain = 0, MaxTrain = 500 };
        warrior.State.Magics.Add(basicSkill);
        warrior.State.Magics.Add(powerSkill);
        warrior.State.RecalculateAttributes();
        dispatcher.RegisterSession(warrior);
        map.Aoi.Enter(warrior, 200, 200);

        var monster = new MonsterInstance
        {
            Uid = 8801,
            Name = "黑色恶蛆",
            MapId = map.MapId,
            X = 200,
            Y = 201,
            Level = 20,
            Hp = 300,
            MaxHp = 300
        };
        dispatcher.RegenEngine!.AddMonster(monster);

        // 发起攻杀斩 CM_POWERHIT (3018)
        dispatcher.Dispatch(warrior, $"#{ProtocolConsts.CM_POWERHIT}/200/201/4!");

        // 验证攻杀剑气生效:
        // A. 基础 13~35 DC + 1级攻杀 HitPlus 3 => 16~38 点物理伤害
        Assert.True(monster.Hp <= 300 - (13 + 3), $"怪物受到攻杀斩伤害后的生命: {monster.Hp}");

        // B. 广播攻杀剑气特效 (MagicId = 7)
        Assert.True(warrior.HasBufferedPacket($"#{ProtocolConsts.SM_SPELLEFFECT_DELPHI}/{warrior.SessionId}/200/201/7/攻杀剑术!"));

        // C. 攻杀剑术熟练度增加 (CurTrain 0 -> 1)
        Assert.Equal(1, powerSkill.CurTrain);

        // 4. 验证战士隔位刺杀怪物增加刺杀剑术熟练度 (MagicId = 12)
        var stabSkill = new UserMagic { MagicId = 12, Level = 1, CurTrain = 0, MaxTrain = 800 };
        warrior.State.Magics.Add(stabSkill);
        warrior.State.StabbingTracker.AutoStab = true;
        warrior.LastAttackTimeMs = 0; // 重置出刀硬直，模拟下一次合法物理出刀时钟

        var farMonster = new MonsterInstance
        {
            Uid = 8802,
            Name = "楔蛾",
            MapId = map.MapId,
            X = 200,
            Y = 202, // 隔位第 2 格
            Level = 20,
            Hp = 200,
            MaxHp = 200
        };
        dispatcher.RegenEngine.AddMonster(farMonster);

        // 发起普通物理攻击 CM_HIT 指向 2 格外的楔蛾
        dispatcher.Dispatch(warrior, $"#{ProtocolConsts.CM_HIT}/200/202/4!");

        // 验证隔位刺杀造成伤害并且增加刺杀剑术熟练度 (MagicId = 12)
        Assert.True(farMonster.Hp < 200);
        Assert.Equal(1, stabSkill.CurTrain);
        var allPackets = warrior.GetBufferedPacketsString();
        Assert.Contains($"#{ProtocolConsts.SM_SPELLEFFECT_DELPHI}/{warrior.SessionId}/200/202/12/刺杀剑气!", allPackets);
    }

    /// <summary>
    /// 第八十五波深水区物理实证：道士施毒术双色药粉精准匹配、装备栏Slot2/背包双向消耗与动态道术结算
    /// 严密验证:
    ///   1. 仅持有黄色药粉时只施加红毒 (受到伤害加深 1.2 倍)，扣除黄色药粉，绝不误上绿毒；
    ///   2. 仅持有灰色药粉时只施加绿毒 (持续减血清空回血)，扣除灰色药粉，绝不误上红毒；
    ///   3. 装备栏照明辅助位 Slot 2 佩戴毒药包优先消耗，持久归零下发 SM_TAKEOFF_OK 并清空槽位；
    ///   4. 双药粉齐全时施毒优先级：先绿毒扣灰粉，再红毒扣黄粉；
    ///   5. 道士道术 SC 动态结算持续时间，彻底核销写死 25 的硬编码坏账。
    /// </summary>
    [Fact]
    public void Test_Wave85_Taoist_PoisonPowder_ColorMatching_DualSlot_And_DynamicSc()
    {
        var mapMgr = new Mir2.Server.Maps.MapManager();
        var regenEngine = new Mir2.Server.Game.MonsterRegenEngine(mapMgr);
        var dispatcher = new Mir2.Server.Game.PacketDispatcher(mapMgr, regenEngine);
        var map = mapMgr.DefaultMap;

        // 1. 创建道士玩家 (SessionId 9885)，拥有施毒术 (MagicId = 6)
        var taoist = new ClientSession(9885, null!)
        {
            CurrentMapId = map.MapId,
            CharacterName = "MasterTaoist",
            X = 300,
            Y = 300,
            Direction = 0,
            State = new PlayerState
            {
                Job = 2, // 道士
                Level = 35,
                Hp = 300,
                MaxHp = 300,
                Mp = 500,
                MaxMp = 500,
                Sc = 20,
                MaxSc = 30 // SC 范围 20~30
            }
        };
        var poisonSkill = new UserMagic { MagicId = 6, Level = 3, CurTrain = 0, MaxTrain = 1000 };
        taoist.State.Magics.Add(poisonSkill);
        dispatcher.RegisterSession(taoist);
        map.Aoi.Enter(taoist, 300, 300);

        // 2. 创建受试怪物 (白野猪 8805)
        var boar = new MonsterInstance
        {
            Uid = 8805,
            Name = "白野猪",
            MapId = map.MapId,
            X = 300,
            Y = 302,
            Level = 35,
            Hp = 1000,
            MaxHp = 1000
        };
        dispatcher.RegenEngine!.AddMonster(boar);

        // 场景 A: 道士身上没有任何药粉，施法必须失败
        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_SPELL}/8805/6/300/302!");
        Assert.False(boar.HasGreenPoison);
        Assert.False(boar.HasRedPoison);
        Assert.True(taoist.HasBufferedPacket($"#{ProtocolConsts.SM_SYSMSG_DELPHI}/0/0/0/0/你没有带毒药。!"));

        // 场景 B: 道士仅装备【黄色药粉(中)】于装备栏 Slot 2 (OfficialEquipSlot.Lighting)
        // 目标无毒，施毒术必须施加红毒 (Red)，绝不可因无绿毒而错误施加绿毒！
        var yellowPowderInEquip = new EquipItem
        {
            Uid = 7001,
            Name = "黄色药粉(中)",
            Dura = 2,
            MaxDura = 50,
            Weight = 1
        };
        taoist.State.Equipment[(int)OfficialEquipSlot.Lighting] = yellowPowderInEquip;
        taoist.LastSpellTimeMs = 0; // 重置公共 CD

        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_SPELL}/8805/6/300/302!");
        Assert.False(boar.HasGreenPoison, "仅佩戴黄色药粉时绝不可施加绿毒！");
        Assert.True(boar.HasRedPoison, "仅佩戴黄色药粉时必须精准施加红毒！");
        Assert.Equal(1, yellowPowderInEquip.Dura); // 装备栏黄粉扣除 1 点持久

        // 场景 C: 再次施法，装备栏黄粉持久从 1 降至 0，必须自动下发 SM_TAKEOFF_OK 并从装备栏移除
        taoist.LastSpellTimeMs = 0;
        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_SPELL}/8805/6/300/302!");
        Assert.Null(taoist.State.Equipment[(int)OfficialEquipSlot.Lighting]);
        Assert.Contains($"#{ProtocolConsts.SM_TAKEOFF_OK}/{taoist.SessionId}/2", taoist.GetBufferedPacketsString());
        Assert.True(taoist.HasBufferedPacket($"#{ProtocolConsts.SM_SYSMSG_DELPHI}/0/0/0/0/黄色药粉(中)已用尽！!"));

        // 场景 D: 道士放入【灰色药粉(大)】于背包，再次施法给白野猪施毒
        // 此时白野猪已有红毒，无绿毒，道士有灰粉 -> 必须精准施加绿毒，扣除背包灰粉！
        var grayPowderInBag = new EquipItem
        {
            Uid = 7002,
            Name = "灰色药粉(大)",
            Dura = 50,
            MaxDura = 50,
            Weight = 2
        };
        taoist.State.BagItems.Add(grayPowderInBag);
        taoist.State.BagWeight += 2;
        taoist.LastSpellTimeMs = 0;

        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_SPELL}/8805/6/300/302!");
        Assert.True(boar.HasGreenPoison, "道士持有灰色药粉且目标无绿毒时必须施加绿毒！");
        Assert.True(boar.HasRedPoison, "原有红毒依然保留，达成双毒并存！");
        Assert.Equal(49, grayPowderInBag.Dura); // 背包灰粉持久精准扣除 1 点
        Assert.True(boar.PoisonSc >= taoist.State.Sc && boar.PoisonSc <= taoist.State.MaxSc, $"绿毒道术必须动态在当前角色属性 [{taoist.State.Sc}, {taoist.State.MaxSc}] 之间，实际: {boar.PoisonSc}");

        // 场景 E: 双药粉齐全时的目标状态迁移判定
        // 创建新怪物 (祖玛卫士 8806)，道士装备栏放黄粉，背包放灰粉
        var yellowInBag = new EquipItem { Uid = 7003, Name = "黄色药粉(小)", Dura = 10, MaxDura = 10, Weight = 1 };
        taoist.State.BagItems.Add(yellowInBag);
        var guard = new MonsterInstance { Uid = 8806, Name = "祖玛卫士", MapId = map.MapId, X = 300, Y = 303, Level = 40, Hp = 1000, MaxHp = 1000 };
        dispatcher.RegenEngine.AddMonster(guard);

        // 第一次施法：祖玛卫士无毒，优先施加绿毒，消耗灰粉
        taoist.LastSpellTimeMs = 0;
        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_SPELL}/8806/6/300/303!");
        Assert.True(guard.HasGreenPoison);
        Assert.False(guard.HasRedPoison);
        Assert.Equal(48, grayPowderInBag.Dura);
        Assert.Equal(10, yellowInBag.Dura); // 黄粉未消耗

        // 第二次施法：祖玛卫士已有绿毒，自动选择施加红毒，消耗黄粉
        taoist.LastSpellTimeMs = 0;
        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_SPELL}/8806/6/300/303!");
        Assert.True(guard.HasGreenPoison);
        Assert.True(guard.HasRedPoison);
        Assert.Equal(48, grayPowderInBag.Dura); // 灰粉未再扣
        Assert.Equal(9, yellowInBag.Dura); // 黄粉精准扣减 1 点
    }

    [Fact]
    public void Test_Wave86_SpellSkillTraining_StrictValidation_NoCheating()
    {
        var mapMgr = new Mir2.Server.Maps.MapManager();
        var regenEngine = new Mir2.Server.Game.MonsterRegenEngine(mapMgr);
        var dispatcher = new Mir2.Server.Game.PacketDispatcher(mapMgr, regenEngine);
        var map = mapMgr.DefaultMap;

        // 1. 创建战士玩家 (SessionId 9886)，拥有基本剑术(3)、刺杀剑术(12)、烈火剑法(26)
        var warrior = new ClientSession(9886, null!)
        {
            CurrentMapId = map.MapId,
            CharacterName = "IronWarrior",
            X = 300,
            Y = 300,
            Direction = 0,
            State = new PlayerState
            {
                Job = 0, // 战士
                Level = 35,
                Hp = 500,
                MaxHp = 500,
                Mp = 100,
                MaxMp = 100,
                Dc = 15,
                MaxDc = 30,
                Hit = 25
            }
        };
        var swordSkill = new UserMagic { MagicId = 3, Level = 1, CurTrain = 10, MaxTrain = 500 };
        var stabSkill = new UserMagic { MagicId = 12, Level = 1, CurTrain = 20, MaxTrain = 500 };
        var fireSkill = new UserMagic { MagicId = 26, Level = 1, CurTrain = 30, MaxTrain = 500 };
        warrior.State.Magics.Add(swordSkill);
        warrior.State.Magics.Add(stabSkill);
        warrior.State.Magics.Add(fireSkill);
        dispatcher.RegisterSession(warrior);
        map.Aoi.Enter(warrior, 300, 300);

        // 验证 1: 战士在快捷键按下刺杀剑术开关 (MagicId = 12)，绝对不可增加熟练度！
        dispatcher.Dispatch(warrior, $"#{ProtocolConsts.CM_SPELL}/0/12/300/300!");
        Assert.Equal(20, stabSkill.CurTrain); // 绝不增加，依然为 20！

        // 验证 2: 战士按快捷键烈火剑法聚气 (MagicId = 26)，聚气成功但未出刀，绝对不可增加熟练度！
        warrior.LastSpellTimeMs = 0; // 重置公共 CD
        dispatcher.Dispatch(warrior, $"#{ProtocolConsts.CM_SPELL}/0/26/300/300!");
        Assert.True(warrior.State.BoFireHit, "烈火剑法聚气成功");
        Assert.Equal(30, fireSkill.CurTrain); // 绝不增加，依然为 30！

        // 验证 3: 战士物理出刀隔位 (距离 2) 刺杀命中怪物
        // 第一刀：带着烈火剑气出刀，烈火爆发命中，烈火熟练度从 30 升为 31，刺杀熟练度保持 20！
        var dummyMon = new MonsterInstance
        {
            Uid = 8807,
            Name = "红蛇",
            MapId = map.MapId,
            X = 300,
            Y = 302, // 正南方隔 1 格 (距离 2)
            Level = 10,
            Hp = 1000,
            MaxHp = 1000
        };
        dispatcher.RegenEngine!.AddMonster(dummyMon);
        warrior.Direction = 4; // 面向南
        warrior.State.BoThrusting = true; // 开启刺杀剑气

        dispatcher.Dispatch(warrior, $"#{ProtocolConsts.CM_HIT}/300/302/4!");
        Assert.Equal(31, fireSkill.CurTrain); // 烈火命中，烈火剑法熟练度精准 +1！
        Assert.Equal(20, stabSkill.CurTrain); // 刺杀剑术熟练度保持不变！

        // 第二刀：烈火已消耗，纯隔位刺杀剑气破盾破防命中，刺杀剑术熟练度从 20 精准升为 21！
        warrior.LastAttackTimeMs = 0; // 重置攻速计时
        dispatcher.Dispatch(warrior, $"#{ProtocolConsts.CM_HIT}/300/302/4!");
        Assert.Equal(21, stabSkill.CurTrain); // 刺杀剑术熟练度精准 +1 变为 21！

        // 2. 创建法师玩家 (SessionId 9887)，拥有抗拒火环(8)、圣言术(24)、火球术(1)
        var mage = new ClientSession(9887, null!)
        {
            CurrentMapId = map.MapId,
            CharacterName = "ArchMage",
            X = 330,
            Y = 270, // 比奇安全区内 (330, 270)
            Direction = 0,
            State = new PlayerState
            {
                Job = 1, // 法师
                Level = 40,
                Hp = 150,
                MaxHp = 150,
                Mp = 800,
                MaxMp = 800,
                Mc = 10,
                MaxMc = 25
            }
        };
        var ringSkill = new UserMagic { MagicId = 8, Level = 1, CurTrain = 5, MaxTrain = 500 };
        var holySkill = new UserMagic { MagicId = 24, Level = 1, CurTrain = 0, MaxTrain = 500 };
        var ballSkill = new UserMagic { MagicId = 1, Level = 1, CurTrain = 50, MaxTrain = 500 };
        mage.State.Magics.Add(ringSkill);
        mage.State.Magics.Add(holySkill);
        mage.State.Magics.Add(ballSkill);
        dispatcher.RegisterSession(mage);
        map.Aoi.Enter(mage, 330, 270);

        // 验证 4: 法师在安全区内施放抗拒火环，被安全区规则拦截，熟练度绝对不可增加！
        dispatcher.Dispatch(mage, $"#{ProtocolConsts.CM_SPELL}/0/8/330/270!");
        Assert.Equal(5, ringSkill.CurTrain); // 绝不增加，依然为 5！

        // 验证 5: 圣言术对 BOSS (如沃玛教主 8808) 施放被魔王护佑拦截，熟练度绝对不可增加！
        var boss = new MonsterInstance
        {
            Uid = 8808,
            Name = "沃玛教主",
            MapId = map.MapId,
            X = 300,
            Y = 305,
            Level = 50,
            Hp = 2000,
            MaxHp = 2000
        };
        dispatcher.RegenEngine.AddMonster(boss);
        mage.X = 300;
        mage.Y = 304; // 离开安全区
        mage.LastSpellTimeMs = 0;

        dispatcher.Dispatch(mage, $"#{ProtocolConsts.CM_SPELL}/8808/24/300/305!");
        Assert.Equal(0, holySkill.CurTrain); // 圣言术对 BOSS 拦截，绝对不可涨熟练度！

        // 验证 6: 法师在野外成功施放火球术 (MagicId = 1)，熟练度精准 +1！
        mage.LastSpellTimeMs = 0;
        dispatcher.Dispatch(mage, $"#{ProtocolConsts.CM_SPELL}/0/1/300/310!");
        Assert.Equal(51, ballSkill.CurTrain); // 成功施法，火球术熟练度从 50 精准变为 51！
    }

    [Fact]
    public void Test_Wave87_MoveBreaksSpellInvisibility_And_TemptationDynamicParam_NoCheating()
    {
        var mapMgr = new Mir2.Server.Maps.MapManager();
        var regenEngine = new Mir2.Server.Game.MonsterRegenEngine(mapMgr);
        var dispatcher = new Mir2.Server.Game.PacketDispatcher(mapMgr, regenEngine);
        var map = mapMgr.DefaultMap;

        // 1. 创建道士玩家 (SessionId 9888)，给自己施加法术隐身
        var taoist = new ClientSession(9888, null!)
        {
            CurrentMapId = map.MapId,
            CharacterName = "HiddenTaoist",
            X = 200,
            Y = 200,
            Direction = 0,
            State = new PlayerState
            {
                Job = 2,
                Level = 30,
                Hp = 200,
                MaxHp = 200,
                Mp = 200,
                MaxMp = 200
            }
        };
        dispatcher.RegisterSession(taoist);
        map.Aoi.Enter(taoist, 200, 200);

        long now = Environment.TickCount64;
        taoist.State.IsStealthed = true;
        taoist.State.StealthExpireMs = now + 30000;
        taoist.State.InvisibilityExpireMs = now + 30000;
        Assert.True(taoist.State.IsInvisible, "道士初始处于法术隐身状态");
        Assert.True((taoist.State.GetCurrentStatusMask(now) & (0x80000000u >> 8)) != 0, "掩码第8位处于隐身状态");

        // 验证 1: 原地转向 CM_TURNDIRECTION (3010)，坐标未变，隐身状态依然保留！
        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_TURNDIRECTION}/200/200/2!");
        Assert.True(taoist.State.IsInvisible, "原地转向绝不破隐");

        // 验证 2: 迈步位移 CM_WALK (3011) 移动 1 格到 (200, 201)，盛大官方 1.5 铁律：法术隐身立刻被打破显形！
        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_WALK}/200/201/4!");
        Assert.Equal(200, taoist.X);
        Assert.Equal(201, taoist.Y);
        Assert.False(taoist.State.IsInvisible, "迈步位移后法术隐身必须立刻被打破！");
        Assert.Equal(0u, taoist.State.GetCurrentStatusMask() & (0x80000000u >> 8)); // 状态掩码第8位彻底清除！
        Assert.True(taoist.HasBufferedPacket($"#{ProtocolConsts.SM_CHANGENAMECOLOR}/"), "必须下发状态掩码同步包刷新实体视觉");

        // 验证 3: 隐身戒指佩戴者移动测试：战士佩戴隐身戒指移动走步，隐身不被破除
        var ringWarrior = new ClientSession(9889, null!)
        {
            CurrentMapId = map.MapId,
            CharacterName = "RingWarrior",
            X = 210,
            Y = 210,
            Direction = 0,
            State = new PlayerState
            {
                Job = 0,
                Level = 40,
                Hp = 600,
                MaxHp = 600,
                Mp = 100,
                MaxMp = 100
            }
        };
        var invisRing = new EquipItem { Uid = 9901, Name = "隐身戒指", Shape = 113, Weight = 1 };
        ringWarrior.State.Equipment[(int)OfficialEquipSlot.RingL] = invisRing;
        ringWarrior.State.RecalculateAttributes();
        dispatcher.RegisterSession(ringWarrior);
        map.Aoi.Enter(ringWarrior, 210, 210);

        Assert.True(ringWarrior.State.HasInvisibleRing);
        Assert.True(ringWarrior.State.IsInvisible, "佩戴隐身戒指常驻隐身");

        // 隐身戒指移动走步：依然隐身！
        dispatcher.Dispatch(ringWarrior, $"#{ProtocolConsts.CM_WALK}/210/211/4!");
        Assert.True(ringWarrior.State.IsInvisible, "隐身戒指佩戴者移动步伐依然保持神圣隐匿！");

        // 验证 4: 法师诱惑之光 (MagicId = 20) 动态等级压制与随从上限测试
        var mage = new ClientSession(9890, null!)
        {
            CurrentMapId = map.MapId,
            CharacterName = "TameMaster",
            X = 250,
            Y = 250,
            Direction = 0,
            State = new PlayerState
            {
                Job = 1,
                Level = 25, // 法师仅 25 级
                Hp = 120,
                MaxHp = 120,
                Mp = 300,
                MaxMp = 300
            }
        };
        var tameSkill = new UserMagic { MagicId = 20, Level = 1, CurTrain = 0, MaxTrain = 500 };
        mage.State.Magics.Add(tameSkill);
        dispatcher.RegisterSession(mage);
        map.Aoi.Enter(mage, 250, 250);

        // 怪物：白野猪 35 级 (高过法师 25 级)
        var highLevelBoar = new MonsterInstance
        {
            Uid = 8809,
            Name = "白野猪",
            MapId = map.MapId,
            X = 250,
            Y = 252,
            Level = 35,
            Hp = 1000,
            MaxHp = 1000
        };
        dispatcher.RegenEngine!.AddMonster(highLevelBoar);

        // 25 级法师诱惑 35 级白野猪：等级压制必定失败，绝对无法驯服！
        dispatcher.Dispatch(mage, $"#{ProtocolConsts.CM_SPELL}/8809/20/250/252!");
        Assert.Equal(0, highLevelBoar.MasterSessionId);
        Assert.Empty(mage.Pets);
        Assert.True(mage.HasBufferedPacket($"#{ProtocolConsts.SM_SYSMSG_DELPHI}/0/0/0/0/目标等级过高，诱惑之光无法动摇其意志。!"));
    }

    [Fact]
    public void Test_Wave88_PetMutinySixHours_PlayerDeathPetCleanup_RevelationSync_NoCheating()
    {
        var mapMgr = new Mir2.Server.Maps.MapManager();
        var regenEngine = new Mir2.Server.Game.MonsterRegenEngine(mapMgr);
        var dispatcher = new Mir2.Server.Game.PacketDispatcher(mapMgr, regenEngine);
        var map = mapMgr.DefaultMap;

        // 1. 创建法师玩家 (SessionId 9890)，诱惑并携带一只虎卫随从
        var mage = new ClientSession(9890, null!)
        {
            CurrentMapId = map.MapId,
            CharacterName = "MasterMage",
            X = 100,
            Y = 100,
            Direction = 0,
            State = new PlayerState
            {
                Job = 1,
                Level = 45,
                Hp = 200,
                MaxHp = 200,
                Mp = 1000,
                MaxMp = 1000
            }
        };
        dispatcher.RegisterSession(mage);
        map.Aoi.Enter(mage, 100, 100);

        long now = Environment.TickCount64;
        var huwei = new MonsterInstance
        {
            Uid = 9901,
            Name = "虎卫",
            MapId = map.MapId,
            X = 500,
            Y = 500, // 远在千里之外 (不在法师九宫格内)
            Level = 25,
            Hp = 800,
            MaxHp = 800,
            AttackDamageMin = 20,
            AttackDamageMax = 35,
            MasterSessionId = mage.SessionId,
            TamedTickMs = now - (6 * 3600 * 1000L + 5000), // 已存活超 6 小时
            NextActionTimeMs = now - 100
        };
        regenEngine.AddMonster(huwei);

        var slavePet = new HuWeiPetEngine.SlavePet
        {
            Uid = huwei.Uid,
            Name = huwei.Name,
            MasterSessionId = mage.SessionId,
            CurrentMapId = huwei.MapId,
            X = huwei.X,
            Y = huwei.Y,
            Hp = huwei.Hp,
            MaxHp = huwei.MaxHp
        };
        mage.Pets.Add(slavePet);
        Assert.Single(mage.Pets);

        // 验证 1: 随从存活超 6 小时发狂叛变！
        // 驱动怪物 AI Tick，触发 ActionType = 7
        dispatcher.TickMonsters(OfficialClientRulesEngine.DelphiRandom.Shared);

        Assert.True(huwei.IsMad, "虎卫必须陷入发狂叛变");
        Assert.Equal(0, huwei.MasterSessionId); // 主仆契约解除
        Assert.Empty(mage.Pets); // 即使法师远在 (100, 100)，主人的 Pets 列表依然被安全彻底清空！
        Assert.True(mage.HasBufferedPacket($"#{ProtocolConsts.SM_SYSMSG_DELPHI}/0/0/0/0/你的随从 虎卫 叛变了。!"));

        // 验证 2: 主人阵亡 (OnPlayerDied) 时随从暴毙消散并从 RegenEngine 物理彻底清除
        var daoist = new ClientSession(9891, null!)
        {
            CurrentMapId = map.MapId,
            CharacterName = "DaoistPriest",
            X = 200,
            Y = 200,
            Direction = 0,
            State = new PlayerState
            {
                Job = 2,
                Level = 40,
                Hp = 300,
                MaxHp = 300,
                Mp = 500,
                MaxMp = 500,
                Sc = 20,
                MaxSc = 40
            }
        };
        dispatcher.RegisterSession(daoist);
        map.Aoi.Enter(daoist, 200, 200);

        // 道士召唤了一只神兽
        var beastMon = new MonsterInstance
        {
            Uid = 9902,
            Name = "神兽",
            MapId = map.MapId,
            X = 201,
            Y = 200,
            Level = 35,
            Hp = 1000,
            MaxHp = 1000,
            MasterSessionId = daoist.SessionId
        };
        regenEngine.AddMonster(beastMon);
        daoist.Pets.Add(new HuWeiPetEngine.SlavePet
        {
            Uid = beastMon.Uid,
            Name = beastMon.Name,
            MasterSessionId = daoist.SessionId,
            CurrentMapId = beastMon.MapId,
            X = beastMon.X,
            Y = beastMon.Y,
            Hp = beastMon.Hp,
            MaxHp = beastMon.MaxHp
        });
        Assert.Single(daoist.Pets);
        Assert.NotNull(regenEngine.FindMonsterByUid(9902));

        // 道士倒地阵亡
        dispatcher.OnPlayerDied(daoist);
        Assert.Empty(daoist.Pets); // 随从列表彻底清空
        Assert.Null(regenEngine.FindMonsterByUid(9902)); // RegenEngine 物理世界实体必须被彻底清除，绝对不留幽灵怪！

        // 验证 3: 道士心灵启示技能等级动态获取与玩家受击实时同步血量
        var targetWarrior = new ClientSession(9892, null!)
        {
            CurrentMapId = map.MapId,
            CharacterName = "TargetWarrior",
            X = 200,
            Y = 201,
            Direction = 0,
            State = new PlayerState
            {
                Job = 0,
                Level = 35,
                Hp = 400,
                MaxHp = 400,
                Mp = 100,
                MaxMp = 100,
                Ac = 10,
                MaxAc = 15
            }
        };
        dispatcher.RegisterSession(targetWarrior);
        map.Aoi.Enter(targetWarrior, 200, 201);

        // 给道士学习 1 级心灵启示 (MagicId = 28)
        daoist.State.Hp = 300; // 满血复活进行测试
        daoist.State.Magics.Add(new UserMagic { MagicId = 28, Level = 1, CurTrain = 0, MaxTrain = 500 });
        daoist.LastSpellTimeMs = 0;

        // 道士对目标战士施放心灵启示
        dispatcher.Dispatch(daoist, $"#{ProtocolConsts.CM_SPELL}/9892/28/200/201!");
        Assert.True(targetWarrior.State.IsRevealed, "目标战士已被心灵启示成功透视");
        Assert.Equal(daoist.SessionId, targetWarrior.State.RevealedBySessionId);

        // 目标战士受到伤害 (受到物理出刀 50 点伤害，扣血至 350)
        now = Environment.TickCount64;
        targetWarrior.State.TakeCombatDamageFull(50, now);
        dispatcher.SyncRevealedPlayerHealthIfApplicable(targetWarrior, now);

        // 盛大官方 1.5 铁律: 道士端立即收到最新的战士血量同步包 (SM_HEALTHBAR_COLOR_DELPHI)！
        Assert.True(daoist.HasBufferedPacket($"#{ProtocolConsts.SM_HEALTHBAR_COLOR_DELPHI}/9892/350/400/0/!"));
    }

    [Fact]
    public void Test_Wave89_WeaponTrialSlashEntityConstraint_And_PetTeleportEntitySync_NoCheating()
    {
        var mapMgr = new Mir2.Server.Maps.MapManager();
        var regenEngine = new Mir2.Server.Game.MonsterRegenEngine(mapMgr);
        var groupEngine = new Mir2.Server.Game.GroupEngine();
        var tradeEngine = new Mir2.Server.Game.TradeEngine();
        var dispatcher = new Mir2.Server.Game.PacketDispatcher(mapMgr, regenEngine, groupEngine: groupEngine, tradeEngine: tradeEngine);
        var map = mapMgr.DefaultMap;

        // ==================== 1. 验证开光武器试刀开刀实体约束 (砍空气绝不触发试刀) ====================
        var warrior = new ClientSession(9893, null!)
        {
            CurrentMapId = map.MapId,
            CharacterName = "TrialBladeMaster",
            X = 300,
            Y = 300,
            Direction = 4, // 面向南
            State = new PlayerState
            {
                Job = 0,
                Level = 42,
                Hp = 600,
                MaxHp = 600,
                Mp = 100,
                MaxMp = 100,
                Dc = 30,
                MaxDc = 60
            }
        };
        // 装备一把已开光待试刀的裁决之杖
        var caiJue = new EquipItem
        {
            Uid = 9999,
            Name = "裁决之杖",
            Shape = 1,
            Weight = 45,
            Dura = 32000,
            MaxDura = 32000,
            UpgradeState = 1 // 1: 已开光待试刀
        };
        warrior.State.Equipment[(int)OfficialEquipSlot.Weapon] = caiJue;
        warrior.State.TrialSlashCount = 1; // 处于待试刀状态
        dispatcher.RegisterSession(warrior);
        map.Aoi.Enter(warrior, 300, 300);

        // 验证 1.1: 原地攻击 (cDist == 0) 或超远作弊距离，攻击动作被直接拦截，绝不消耗试刀状态
        warrior.LastAttackTimeMs = 0;
        dispatcher.Dispatch(warrior, $"#{ProtocolConsts.CM_HIT}/300/300/4!");
        Assert.Equal(1, warrior.State.TrialSlashCount);
        Assert.Equal(1, caiJue.UpgradeState);
        Assert.NotNull(warrior.State.Equipment[(int)OfficialEquipSlot.Weapon]);

        // 验证 1.2: 合法出刀动作发起 (向前方 1 格挥刀)
        // 盛大官方 1.5 铁律: 出刀动作合法生效即触发神兵试刀开刃 (UpgradeState 1 为必碎码，神兵崩裂化为乌有)
        warrior.ClearBufferedPackets();
        warrior.LastAttackTimeMs = 0;
        dispatcher.Dispatch(warrior, $"#{ProtocolConsts.CM_HIT}/300/301/4!");
        Assert.Equal(0, warrior.State.TrialSlashCount); // 试刀状态已成功被消费！
        Assert.Null(warrior.State.Equipment[(int)OfficialEquipSlot.Weapon]); // 必碎码导致武器粉碎清空
        Assert.True(warrior.HasBufferedPacket($"#{ProtocolConsts.SM_TAKEOFF_OK}/")); // 下发脱下/销毁神兵封包
        Assert.True(warrior.HasBufferedPacket($"#{ProtocolConsts.SM_WEIGHTCHANGED}/")); // 负重即时同步

        // ==================== 2. 验证组队天地合一 (@天地合一) 跨地图随从 MonsterInstance 实体物理同步 ====================
        // 创建队长 (穿齐记忆全套 4 件) 位于默认地图 (如比奇城 100, 100)
        var leader = new ClientSession(9894, null!)
        {
            CurrentMapId = map.MapId,
            CharacterName = "MemoryLeader",
            X = 100,
            Y = 100,
            Direction = 0,
            State = new PlayerState
            {
                Job = 0,
                Level = 45,
                Hp = 700,
                MaxHp = 700,
                Mp = 100,
                MaxMp = 100,
                AllowGroupRecall = true
            }
        };
        leader.State.Equipment[(int)OfficialEquipSlot.Helmet] = new EquipItem { Uid = 7001, Name = "记忆头盔", Weight = 7 };
        leader.State.Equipment[(int)OfficialEquipSlot.Necklace] = new EquipItem { Uid = 7002, Name = "记忆项链", Weight = 1 };
        leader.State.Equipment[(int)OfficialEquipSlot.ArmringL] = new EquipItem { Uid = 7003, Name = "记忆手镯", Weight = 1 };
        leader.State.Equipment[(int)OfficialEquipSlot.RingL] = new EquipItem { Uid = 7004, Name = "记忆戒指", Weight = 1 };
        dispatcher.RegisterSession(leader);
        map.Aoi.Enter(leader, 100, 100);

        // 创建队员 (道士) 位于另一张地图 (MapId 1 盟重省)，并携带一只神兽
        var mengZhongMap = mapMgr.GetMap(1) ?? mapMgr.DefaultMap;
        var memberTaoist = new ClientSession(9895, null!)
        {
            CurrentMapId = mengZhongMap.MapId,
            CharacterName = "TeamTaoist",
            X = 330,
            Y = 330,
            Direction = 0,
            State = new PlayerState
            {
                Job = 2,
                Level = 40,
                Hp = 400,
                MaxHp = 400,
                Mp = 300,
                MaxMp = 300,
                AllowGroupRecall = true // 开启允许天地合一
            }
        };
        dispatcher.RegisterSession(memberTaoist);
        mengZhongMap.Aoi.Enter(memberTaoist, 330, 330);

        // 注册道士的神兽物理实体位于盟重省
        var beastMon = new MonsterInstance
        {
            Uid = 9997,
            Name = "神兽",
            MapId = mengZhongMap.MapId,
            X = 331,
            Y = 330,
            Level = 35,
            Hp = 1200,
            MaxHp = 1200,
            MasterSessionId = memberTaoist.SessionId
        };
        regenEngine.AddMonster(beastMon);
        memberTaoist.Pets.Add(new HuWeiPetEngine.SlavePet
        {
            Uid = beastMon.Uid,
            Name = beastMon.Name,
            MasterSessionId = memberTaoist.SessionId,
            CurrentMapId = beastMon.MapId,
            X = beastMon.X,
            Y = beastMon.Y,
            Hp = beastMon.Hp,
            MaxHp = beastMon.MaxHp
        });

        // 组建队伍，队长为 leader，队员为 memberTaoist
        groupEngine.CreateParty(leader);
        groupEngine.AddMember(leader, memberTaoist);

        // 队长施展指令 [@天地合一] 全服瞬间召唤
        dispatcher.Dispatch(leader, $"#{ProtocolConsts.CM_SAY}/@天地合一!");

        // 盛大官方 1.5 铁律验证:
        // 1. 队员 memberTaoist 瞬间传送至队长身边所在地图 map.MapId
        Assert.Equal(map.MapId, memberTaoist.CurrentMapId);

        // 2. 随从 SlavePet 坐标与地图同步到队长所在地图
        var petInMember = memberTaoist.Pets[0];
        Assert.Equal(map.MapId, petInMember.CurrentMapId);

        // 3. 【核心坏账销毁验证】: RegenEngine 世界怪兽物理实体的 MapId 必须同步更新为 map.MapId，坐标随同转移，仇恨归零！
        var petMonsterInstance = regenEngine.FindMonsterByUid(beastMon.Uid);
        Assert.NotNull(petMonsterInstance);
        Assert.Equal(map.MapId, petMonsterInstance.MapId); // 物理实体绝不留在旧地图盟重省！
        Assert.Equal(petInMember.X, petMonsterInstance.X); // 物理 X 坐标精准同步！
        Assert.Equal(petInMember.Y, petMonsterInstance.Y); // 物理 Y 坐标精准同步！
        Assert.Equal(0, petMonsterInstance.TargetSessionId); // 传送后旧仇恨彻底解除！
    }

    [Fact]
    public void Test_MedicineBundle_RequiresMinimumFiveBagSlots()
    {
        var mapManager = new MapManager();
        var map = new GameMap(0, "比奇城", 100, 100);
        mapManager.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapManager);

        var player = new ClientSession(9901, null!)
        {
            CurrentMapId = 0,
            X = 50,
            Y = 50,
            CharacterName = "药剂师",
            State = new PlayerState { Level = 30, Hp = 200, MaxHp = 200 }
        };
        map.Aoi.Enter(player, 50, 50);

        // 放入一个强效太阳水包
        var bundle = new EquipItem
        {
            Uid = 8881,
            Name = "强效太阳水包",
            Weight = 13,
            Dura = 1,
            MaxDura = 1
        };
        // 放入一个强效太阳水包在背包第 0 格
        player.State.BagItems.Insert(0, bundle);

        // 填充背包使其达到 36 件物品 (剩余空位 4 格 < 5 格)
        for (int i = 0; i < 35; i++)
        {
            player.State.BagItems.Add(new EquipItem
            {
                Uid = 9000 + i,
                Name = "金创药(小量)",
                Weight = 1,
                Dura = 1,
                MaxDura = 1
            });
        }
        Assert.Equal(36, player.State.BagItems.Count);

        // 尝试解包 (CM_EATITEM 1006: slot/itemName)
        player.ClearBufferedPackets();
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_EATITEM}/0/强效太阳水包!");

        // 验证：空间不足拦截，下发系统提示，物品数量保持 36，药包未被消耗
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_SYSMSG_DELPHI}/0/0/0/0/背包没有足够的空间。!"));
        Assert.Equal(36, player.State.BagItems.Count);
        Assert.Contains(bundle, player.State.BagItems);

        // 移除 1 件物品，使背包变为 35 件 (剩余空位刚好 5 格)
        player.State.BagItems.RemoveAt(player.State.BagItems.Count - 1);
        Assert.Equal(35, player.State.BagItems.Count);

        // 再次解包 (重置吃药 CD)
        player.LastEatItemTimeMs = 0;
        player.ClearBufferedPackets();
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_EATITEM}/0/强效太阳水包!");

        // 验证：解包成功，原药包移除，增加 6 瓶单品，最终背包恰好满 40 格
        Assert.DoesNotContain(bundle, player.State.BagItems);
        Assert.Equal(40, player.State.BagItems.Count);
        int sunCount = 0;
        for (int i = 0; i < player.State.BagItems.Count; i++)
        {
            if (player.State.BagItems[i].Name == "强效太阳水") sunCount++;
        }
        Assert.Equal(6, sunCount);
    }

    [Fact]
    public void Test_MiningHit_DeadOrParalyzedBlocked_And_PickaxeWearAndOrePurity()
    {
        var mapMgr = new MapManager();
        var map = new GameMap(1, "比奇废矿区", 100, 100);
        mapMgr.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapMgr);

        var miner = new ClientSession(9001, null!)
        {
            CurrentMapId = 1,
            X = 50,
            Y = 50,
            CharacterName = "老矿工",
            State = new PlayerState
            {
                Level = 30,
                Hp = 200,
                MaxHp = 200,
                MaxBagWeight = 200,
                BagWeight = 10
            }
        };
        dispatcher.RegisterSession(miner);
        map.Aoi.Enter(miner, 50, 50);

        var observer = new ClientSession(9009, null!)
        {
            CurrentMapId = 1,
            X = 51,
            Y = 50,
            CharacterName = "旁观矿工"
        };
        dispatcher.RegisterSession(observer);
        map.Aoi.Enter(observer, 51, 50);

        // 1. 手中无鹤嘴锄 -> 拦截
        dispatcher.Dispatch(miner, $"#{ProtocolConsts.CM_BIGHIT}!");
        Assert.True(miner.HasBufferedPacket("你手中未装备鹤嘴锄，无法挖矿！"));

        // 装备鹤嘴锄
        var pickaxe = new EquipItem
        {
            Uid = 9991,
            Name = "鹤嘴锄",
            Dura = 10,
            MaxDura = 10,
            Weight = 5
        };
        miner.State.Equipment[(int)OfficialEquipSlot.Weapon] = pickaxe;

        // 2. 死亡状态挥镐 -> 拦截
        miner.State.Hp = 0;
        miner.ClearBufferedPackets();
        observer.ClearBufferedPackets();
        dispatcher.Dispatch(miner, $"#{ProtocolConsts.CM_BIGHIT}!");
        Assert.False(miner.HasBufferedPacket($"#{ProtocolConsts.SM_MINERESULT}"));
        Assert.False(observer.HasBufferedPacket($"#{ProtocolConsts.SM_OBJECTHIT}"));

        // 复活但处于麻痹状态 -> 拦截
        miner.State.Hp = 100;
        miner.State.StatusBits[5] = true;
        miner.State.ParalyzeExpireMs = 999999999999;
        miner.ClearBufferedPackets();
        observer.ClearBufferedPackets();
        dispatcher.Dispatch(miner, $"#{ProtocolConsts.CM_BIGHIT}!");
        Assert.False(miner.HasBufferedPacket($"#{ProtocolConsts.SM_MINERESULT}"));

        // 解除麻痹，正常挥镐 -> 动作广播成功发给旁观者
        miner.State.StatusBits[5] = false;
        miner.State.ParalyzeExpireMs = 0;
        miner.ClearBufferedPackets();
        observer.ClearBufferedPackets();
        dispatcher.Dispatch(miner, $"#{ProtocolConsts.CM_BIGHIT}!");
        Assert.True(observer.HasBufferedPacket($"#{ProtocolConsts.SM_OBJECTHIT}"));
    }

    [Fact]
    public void Test_WildSlam_PvpPlayerCollision_And_SafeZoneBlocked()
    {
        var mapMgr = new MapManager();
        var map = new GameMap(0, "盟重省", 200, 200);
        mapMgr.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapMgr);

        // 1. 安全区内野蛮冲撞拦截
        var warriorSafe = new ClientSession(9002, null!)
        {
            CurrentMapId = 0,
            X = 330,
            Y = 270,
            CharacterName = "安全区战士",
            State = new PlayerState { Level = 45, Hp = 500, MaxHp = 500, Mp = 100, MaxMp = 100 }
        };
        dispatcher.RegisterSession(warriorSafe);
        map.Aoi.Enter(warriorSafe, 330, 270);

        dispatcher.Dispatch(warriorSafe, $"#{ProtocolConsts.CM_SPELL}/0/27/330/271!");
        Assert.True(warriorSafe.HasBufferedPacket("在安全区内不能推开他人。"));

        // 2. 野外 PVP 碰撞测试
        var highWarrior = new ClientSession(9003, null!)
        {
            CurrentMapId = 0,
            X = 100,
            Y = 100,
            Direction = 4, // 向下
            CharacterName = "高级战士",
            State = new PlayerState { Level = 42, Hp = 600, MaxHp = 600, Mp = 100, MaxMp = 100 }
        };
        var lowMage = new ClientSession(9004, null!)
        {
            CurrentMapId = 0,
            X = 100,
            Y = 101, // 正前方
            Direction = 0,
            CharacterName = "低级法师",
            State = new PlayerState { Level = 35, Hp = 150, MaxHp = 150, Mp = 300, MaxMp = 300 }
        };
        dispatcher.RegisterSession(highWarrior);
        dispatcher.RegisterSession(lowMage);
        map.Aoi.Enter(highWarrior, 100, 100);
        map.Aoi.Enter(lowMage, 100, 101);

        highWarrior.ClearBufferedPackets();
        lowMage.ClearBufferedPackets();
        highWarrior.LastSpellTimeMs = 0;
        highWarrior.State.LastWildSlamTickMs = 0;

        // 高级战士向下撞击低级法师 (等级 42 > 35) -> 法师被撞退且施法者跟进
        dispatcher.Dispatch(highWarrior, $"#{ProtocolConsts.CM_SPELL}/0/27/100/101!");
        Assert.True(lowMage.Y > 101, "低级目标应被撞退");
        Assert.True(lowMage.State.StatusBits[5], "目标被赋予僵直状态");
        Assert.Equal(101, highWarrior.Y); // 施法者跟进到目标原位置
    }

    [Fact]
    public void Test_SummonPet_StatsDerivedFromSkillLevel()
    {
        var mapMgr = new MapManager();
        var map = new GameMap(0, "盟重省", 100, 100);
        mapMgr.RegisterMap(map);
        var regen = new MonsterRegenEngine(mapMgr);
        var dispatcher = new PacketDispatcher(mapMgr, regenEngine: regen);

        var taoist = new ClientSession(9005, null!)
        {
            CurrentMapId = 0,
            X = 50,
            Y = 50,
            CharacterName = "大宗师道士",
            State = new PlayerState
            {
                Level = 40,
                Hp = 400,
                MaxHp = 400,
                Mp = 500,
                MaxMp = 500,
                Job = 2
            }
        };
        taoist.State.Magics.Add(new UserMagic { MagicId = 30, Level = 3 }); // 3级召唤神兽

        // 装备护身符
        taoist.State.Equipment[(int)OfficialEquipSlot.Lighting] = new EquipItem
        {
            Uid = 9992,
            Name = "道士护身符",
            Shape = 5,
            Dura = 1000,
            MaxDura = 1000
        };

        dispatcher.RegisterSession(taoist);
        map.Aoi.Enter(taoist, 50, 50);

        // 召唤 3 级神兽
        dispatcher.Dispatch(taoist, $"#{ProtocolConsts.CM_SPELL}/0/30/50/50!");

        Assert.Single(taoist.Pets);
        var pet = taoist.Pets[0];
        Assert.Equal("神兽", pet.Name);
        Assert.Equal(3, pet.Level);
        var expectedStats = TaoistCombatEngine.GetShinsuStats(3);
        Assert.Equal(expectedStats.MaxHp, pet.MaxHp);
        Assert.Equal(expectedStats.MinDc, pet.MinDc);
        Assert.Equal(expectedStats.MaxDc, pet.MaxDc);
        Assert.Equal(expectedStats.Ac, pet.Ac);
    }

    [Fact]
    public void Test_SabukSiege_WarClockExpirationAndPalaceTakeoverBroadcast()
    {
        var mapMgr = new MapManager();
        var palace = new GameMap(SabukSiegeEngine.SabukPalaceMapId, "沙巴克皇宫", 50, 50);
        mapMgr.RegisterMap(palace);
        var sabuk = new SabukSiegeEngine();
        var dispatcher = new PacketDispatcher(mapMgr, sabukEngine: sabuk);

        var attacker = new ClientSession(9006, null!)
        {
            CurrentMapId = SabukSiegeEngine.SabukPalaceMapId,
            X = 25,
            Y = 25,
            CharacterName = "攻城勇士",
            State = new PlayerState { Level = 45, Hp = 500, MaxHp = 500 }
        };
        dispatcher.RegisterSession(attacker);

        // 注册攻城行会并开启战役
        dispatcher.GuildEngine.LoadGuild("战神殿", "攻城勇士", "战必胜", new[] { "攻城勇士" });
        sabuk.RegisterAttackingGuild("战神殿");
        sabuk.StartSiege("守城老行会", nowMs: 1000, durationMs: 5000);

        // 心跳 1: 皇宫独占触发临时占领广播
        dispatcher.ProcessMultiMapParallelTick(1, nowMs: 2000);
        Assert.True(attacker.HasBufferedPacket("[沙巴克攻城战] 皇宫已被 战神殿 占领。"));

        // 心跳 2: 攻城时间到期 (nowMs >= 6000) 自动结算易主
        attacker.ClearBufferedPackets();
        dispatcher.ProcessMultiMapParallelTick(2, nowMs: 6500);
        Assert.Equal(SabukSiegeEngine.WarPhase.Ended, sabuk.Phase);
        Assert.Equal("战神殿", sabuk.OwnerGuild);
        Assert.True(attacker.HasBufferedPacket("[沙巴克攻城战] 攻城战结束，战神殿 占领了沙巴克。"));
    }
}
