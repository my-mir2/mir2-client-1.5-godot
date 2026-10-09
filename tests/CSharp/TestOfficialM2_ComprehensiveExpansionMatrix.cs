using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Mir2.Server.Game;
using Mir2.Server.Maps;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 盛大官方 1.4/1.5 服务端权威化深水区五大拓展模块全面核销测试矩阵
/// 覆盖模块八至模块十二:
/// 1. 模块八: NPC 脚本虚拟机 (IF/ACT/SAY/ELSEACT/ELSESAY/CHECKGOLD/CHECKLEVELEX/TAKEGOLD/MAPMOVE)
/// 2. 模块九: 安全区物理禁武拦截、大刀卫士 9999 攻击秒杀红名 AI、昼夜更替与火把照明消耗
/// 3. 模块十: 战斗断线 15 秒幽灵替身保装、地表物品 120 秒自然过期 GC、移动时序滑窗反加速漏桶
/// 4. 模块十一: 封魔谷姻缘神殿求婚戒指结婚、夫妻传送、拜师与 35 级出师奖励声望
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

    // ==================== 4. 模块十一: 师徒系统与结婚系统 ====================

    [Fact]
    public void Test_SocialRelations_Marriage_And_Mentorship()
    {
        var social = new SocialRelationsEngine();

        var groom = CreateMockSession(4001, "新郎", sex: 0); // 男
        var bride = CreateMockSession(4002, "新娘", sex: 1); // 女

        // 未带求婚戒指成婚失败
        var (marryFail, marryFailMsg) = social.Marry(groom, bride);
        Assert.False(marryFail);
        Assert.Contains("求婚戒指", marryFailMsg);

        // 放入求婚戒指并成婚
        groom.State!.BagItems.Add(new EquipItem { Uid = 999, Name = "求婚戒指", Weight = 1 });
        var (marryOk, marryMsg) = social.Marry(groom, bride);
        Assert.True(marryOk, marryMsg);
        Assert.Empty(groom.State.BagItems); // 戒指已扣除

        // 夫妻同心传唤
        groom.CurrentMapId = 3;
        groom.X = 600;
        groom.Y = 600;
        var (teleOk, teleMsg) = social.TeleportToSpouse(groom, bride);
        Assert.True(teleOk, teleMsg);
        Assert.Equal(3, bride.CurrentMapId);
        Assert.Equal(600, bride.X);
        Assert.Equal(600, bride.Y);

        // 师徒系统: 师傅 40 级，徒弟 20 级拜师
        var master = CreateMockSession(5001, "一代宗师", level: 40);
        var apprentice = CreateMockSession(5002, "萌新学徒", level: 20);

        var (apOk, apMsg) = social.BecomeApprentice(master, apprentice);
        Assert.True(apOk, apMsg);

        // 徒弟升至 35 级学成出师，奖励师傅 30 点声望
        apprentice.State!.Level = 35;
        var (gradOk, rep, gradMsg) = social.Graduate(apprentice, master);
        Assert.True(gradOk, gradMsg);
        Assert.Equal(30, rep);
        Assert.Equal(30, social.GetReputation(master.CharacterName));
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
}
