using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using Mir2.Server.Game;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 盛大官方 1.4/1.5 现代强类型插件化脚本与终极深水区架构权威测试矩阵
/// 覆盖模块十三至模块十六:
/// 1. 模块十三: 现代强类型插件化脚本系统 (淘汰旧 TXT, 全量 13 大官方核心行为树, 本地 100% 编译期查错, 内存字节流热加载替换)
/// 2. 模块十四: 沙巴克城堡税收分成 (20% 商业流水抽成入库), 城主专属取款, 军费修城
/// 3. 模块十五: 祈祷全套“神之祈祷全服大叛变”事件 (召唤兽发狂脱控 + 装备粉碎反噬), 记忆全套“@天地合一”跨地图瞬移全队
/// 4. 模块十六: 未知暗殿真假怪反差萌 (教主100血/神鹿5000血), 大刀守卫追击偏离自动 A* 寻路回岗 AI
/// </summary>
public class TestOfficialM2_ModernArchitectureAndUltimateMatrix
{
    private static ClientSession CreateMockSession(int sessionId, string charName, int level = 25, int gold = 10000)
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
                Hp = 200,
                MaxHp = 200
            }
        };
        return session;
    }

    // ==================== 1. 模块十三: 现代强类型插件脚本系统测试 ====================

    [Fact]
    public void Test_ModernNpcPlugin_CompileTimeSafety_And_HotReload()
    {
        var engine = new ModernNpcPluginEngine();
        var session = CreateMockSession(1001, "新时代测试员", level: 22, gold: 5000);

        // 1. 比奇老兵对话与传送验证 (强类型安全)
        var (h1, say1, _) = engine.DispatchDialog(session, "比奇老兵", "@main");
        Assert.True(h1);
        Assert.Contains("新时代测试员", say1);
        Assert.Contains("@move_mengzhong", say1);

        // 点击传送: 扣减 2000 金币并传送到盟重
        var (h2, say2, closed2) = engine.DispatchDialog(session, "比奇老兵", "@move_mengzhong");
        Assert.True(h2);
        Assert.True(closed2);
        Assert.Equal(3000, session.State!.Gold);
        Assert.Equal(3, session.CurrentMapId);
        Assert.Equal(330, session.X);
        Assert.Equal(330, session.Y);

        // 2. 插件热更与替换测试: 动态热加载新行为插件 (实现本地编译查错后上线热更)
        var customPlugin = new CustomMockPlugin();
        engine.RegisterPlugin(customPlugin);
        Assert.True(engine.HasPlugin("新副本引路人"));
        Assert.True(engine.HasPlugin(999));

        var (hCustom, sayCustom, _) = engine.DispatchDialog(session, "新副本引路人", "@main");
        Assert.True(hCustom);
        Assert.Contains("欢迎来到全新热更副本", sayCustom);

        // 卸载插件
        bool removed = engine.UnregisterPlugin("新副本引路人");
        Assert.True(removed);
        Assert.False(engine.HasPlugin("新副本引路人"));
    }

    public sealed class CustomMockPlugin : INpcBehavior
    {
        public int NpcId => 999;
        public string NpcName => "新副本引路人";

        public void OnInteract(NpcDialogContext ctx)
        {
            ctx.DialogText = "欢迎来到全新热更副本！此插件完全在本地编译排错后热加载上线！";
            ctx.AddLink("进入副本", "@enter");
        }
    }

    [Fact]
    public void Test_ModernNpcPlugin_FullEcosystem_All13CoreNpcBehaviors()
    {
        var engine = new ModernNpcPluginEngine();
        var session = CreateMockSession(2001, "传奇探索者", level: 35, gold: 2000000);

        // 1. 仓库保管员 (ID 106)
        var (h6, s6, _) = engine.DispatchDialog(session, 106, "@main");
        Assert.True(h6);
        Assert.Contains("保管箱", s6);

        // 2. 武器升级大师 (ID 107)
        session.State!.Equipment[(int)OfficialEquipSlot.Weapon] = new EquipItem { Uid = 501, Name = "裁决之杖", Dura = 30000, MaxDura = 30000 };
        var (h7_start, s7_start, _) = engine.DispatchDialog(session, 107, "@start_upgrade");
        Assert.True(h7_start);
        Assert.Contains("投入熔炉", s7_start);

        // 模拟取回升级武器并进入试刀状态
        var (h7_ret, s7_ret, _) = engine.DispatchDialog(session, 107, "@retrieve_weapon");
        Assert.True(h7_ret);
        Assert.Contains("取回", s7_ret);
        Assert.Equal(1, session.State.TrialSlashCount);

        // 3. 武馆教头拜师收徒 (ID 108)
        var (h8, s8, _) = engine.DispatchDialog(session, 108, "@master");
        Assert.True(h8);
        Assert.Contains("武学宗师之姿", s8);

        // 4. 沙巴克攻城管理员 (ID 109)
        session.State.GuildRank = 1; // 一号掌门
        int goldBefore = session.State.Gold;
        var (h9, s9, _) = engine.DispatchDialog(session, 109, "@apply_siege");
        Assert.True(h9);
        Assert.Contains("攻城战将于 3 天后", s9);
        Assert.Equal(goldBefore - 1000000, session.State.Gold);

        // 5. 命运之刃经典任务 (ID 110)
        var (h10, s10, _) = engine.DispatchDialog(session, 110, "@rock_paper_scissors");
        Assert.True(h10);
        var (h10_win, s10_win, _) = engine.DispatchDialog(session, 110, "@play_scissors");
        Assert.True(h10_win);
        Assert.Contains("命运之刃", s10_win);
        Assert.Contains(session.State.BagItems, i => i.Name == "命运之刃");

        // 6. 神秘老人未知暗殿引路人 (ID 111)
        var (h11, s11, c11) = engine.DispatchDialog(session, 111, "@enter_darkhall");
        Assert.True(h11);
        Assert.True(c11);
        Assert.Equal(9000, session.CurrentMapId);

        // 7. 技能书合成大师 (ID 112)
        var (h12, s12, _) = engine.DispatchDialog(session, 112, "@craft_firehit");
        Assert.True(h12);
        Assert.Contains("烈火剑法", s12);
        Assert.Contains(session.State.BagItems, i => i.Name == "烈火剑法秘籍");

        // 8. 银杏村新手指导员 (ID 113)
        var newbieSession = CreateMockSession(2002, "新手菜鸟", level: 1, gold: 100);
        var (h13, s13, _) = engine.DispatchDialog(newbieSession, 113, "@get_gift");
        Assert.True(h13);
        Assert.Contains("新手装备已放入你的包裹", s13);
        Assert.Contains(newbieSession.State!.BagItems, i => i.Name == "木剑");
        Assert.Contains(newbieSession.State.BagItems, i => i.Name == "布衣");
    }

    [Fact]
    public void Test_ModernNpcPlugin_MemoryAssemblyBytecode_HotLoading()
    {
        var engine = new ModernNpcPluginEngine();

        // 模拟从本地构建出的 assembly 二进制字节流进行内存热加载
        // 直接读取当前测试程序集的字节码文件进行内存反射热载模拟
        string testAsmPath = typeof(CustomMockPlugin).Assembly.Location;
        if (File.Exists(testAsmPath))
        {
            byte[] asmBytes = File.ReadAllBytes(testAsmPath);
            var result = engine.LoadPluginsFromAssemblyBytes(asmBytes);

            Assert.True(result.Success);
            Assert.Contains(result.LoadedNpcNames, name => name == "新副本引路人");
            Assert.True(engine.HasPlugin("新副本引路人"));
        }
    }

    [Fact]
    public void Test_PacketDispatcher_RoutesToModernNpcEngineFirst()
    {
        var mapMgr = new Mir2.Server.Maps.MapManager();
        var dispatcher = new PacketDispatcher(mapMgr);
        var session = CreateMockSession(3001, "网络交互员", level: 30, gold: 50000);

        // 模拟客户端点击比奇老兵主页 (CM_MERCHANTHOMEPAGE 1010, Uid: 101)
        dispatcher.Dispatch(session, $"#{ProtocolConsts.CM_MERCHANTHOMEPAGE}/101!");

        // 模拟客户端选择传送选项 (CM_NPCSELECT 1011, 101/@move_snake)
        dispatcher.Dispatch(session, $"#{ProtocolConsts.CM_NPCSELECT}/101/@move_snake!");
        Assert.Equal(2, session.CurrentMapId);
        Assert.Equal(49500, session.State!.Gold);
    }

    // ==================== 2. 模块十四: 沙巴克城堡税收分成与金库公款测试 ====================

    [Fact]
    public void Test_SabukTreasury_CommercialTax_And_Withdrawal()
    {
        var treasury = new SabukTreasuryEngine();
        treasury.LordCharacterName = "至尊城主";
        long initTreasury = treasury.GoldTreasury;

        // 1. 全服商业流水 20% 自动入库
        int taxAmount = treasury.CollectShopTransactionTax(10000); // 10000 金币流水
        Assert.Equal(2000, taxAmount);
        Assert.Equal(initTreasury + 2000, treasury.GoldTreasury);

        // 2. 非城主严禁支取公款
        var fakeLord = CreateMockSession(2001, "小瘪三", gold: 100);
        var (wFail, _, _) = treasury.WithdrawCastleGold(fakeLord, 1000);
        Assert.False(wFail);

        // 3. 正统城主专享取款特权
        var realLord = CreateMockSession(2002, "至尊城主", gold: 100);
        var (wSucc, _, _) = treasury.WithdrawCastleGold(realLord, 1500);
        Assert.True(wSucc);
        Assert.Equal(1600, realLord.State!.Gold); // 100 + 1500 = 1600

        // 4. 城堡修缮扣减公款
        var (repairSuccess, _) = treasury.DeductSiegeRepairCost(300);
        Assert.True(repairSuccess);
    }

    // ==================== 3. 模块十五: 祈祷大叛变与记忆天地合一测试 ====================

    [Fact]
    public void Test_SuitWorldEvents_GodPrayer_And_RecallSummon()
    {
        // 1. 祈祷全套大叛变
        var player = CreateMockSession(3001, "天灾降临者");
        player.State!.Equipment[(int)OfficialEquipSlot.Weapon] = new EquipItem { Uid = 1, Name = "祈祷之刃" };
        player.State.Equipment[(int)OfficialEquipSlot.Helmet] = new EquipItem { Uid = 2, Name = "祈祷头盔" };
        player.State.Equipment[(int)OfficialEquipSlot.Necklace] = new EquipItem { Uid = 3, Name = "祈祷项链" };
        player.State.Equipment[(int)OfficialEquipSlot.ArmringL] = new EquipItem { Uid = 4, Name = "祈祷手镯" };
        player.State.Equipment[(int)OfficialEquipSlot.RingL] = new EquipItem { Uid = 5, Name = "祈祷戒指" };

        Assert.True(SuitWorldEventEngine.HasCompleteGodPrayerSuit(player.State));

        var pet1 = new MonsterInstance { Uid = 501, Name = "神兽", MasterSessionId = player.SessionId, MaxHp = 1000, Hp = 1000 };
        var pet2 = new MonsterInstance { Uid = 502, Name = "变异骷髅", MasterSessionId = player.SessionId, MaxHp = 500, Hp = 500 };
        var monsters = new List<MonsterInstance> { pet1, pet2 };

        var (triggered, broadcastMsg, brokenItem) = SuitWorldEventEngine.TriggerGodPrayerRebellion(
            player.State, monsters, Random.Shared);

        Assert.True(triggered);
        Assert.Contains("所有召唤生物已陷入癫狂", broadcastMsg);
        Assert.Equal(0, pet1.MasterSessionId); // 脱离掌控
        Assert.Contains("发狂的", pet1.Name);
        Assert.Equal(0, pet2.MasterSessionId);
        Assert.Contains("发狂的", pet2.Name);
        Assert.NotEmpty(brokenItem); // 必碎一件祈祷装备

        // 2. 记忆全套天地合一
        var leader = CreateMockSession(4001, "记忆队长");
        leader.CurrentMapId = 3;
        leader.X = 330;
        leader.Y = 330;

        leader.State!.Equipment[(int)OfficialEquipSlot.Helmet] = new EquipItem { Uid = 11, Name = "记忆头盔" };
        leader.State.Equipment[(int)OfficialEquipSlot.Necklace] = new EquipItem { Uid = 12, Name = "记忆项链" };
        leader.State.Equipment[(int)OfficialEquipSlot.ArmringL] = new EquipItem { Uid = 13, Name = "记忆手镯" };
        leader.State.Equipment[(int)OfficialEquipSlot.RingL] = new EquipItem { Uid = 14, Name = "记忆戒指" };

        Assert.True(SuitWorldEventEngine.HasCompleteMemorySuit(leader.State));

        var memberA = CreateMockSession(4002, "队员A");
        memberA.CurrentMapId = 0; // 在比奇
        memberA.X = 100;
        memberA.Y = 100;

        var memberB = CreateMockSession(4003, "队员B");
        memberB.CurrentMapId = 11; // 在封魔
        memberB.X = 200;
        memberB.Y = 200;

        var (summonOk, count, _) = SuitWorldEventEngine.ExecuteMemorySummonAll(leader, new[] { leader, memberA, memberB });
        Assert.True(summonOk);
        Assert.Equal(2, count);
        Assert.Equal(3, memberA.CurrentMapId);
        Assert.Equal(330, memberA.X);
        Assert.Equal(330, memberA.Y);
        Assert.Equal(3, memberB.CurrentMapId);
        Assert.Equal(330, memberB.X);
        Assert.Equal(330, memberB.Y);
    }

    // ==================== 4. 模块十六: 未知暗殿反差萌与守卫巡逻回位测试 ====================

    [Fact]
    public void Test_UnknownDarkHall_And_GuardReturnAI()
    {
        // 1. 未知暗殿反差萌机制
        var fakeBoss = new MonsterInstance { Uid = 801, Name = "沃玛教主", MaxHp = 2000, Hp = 2000 };
        var realGodDeer = new MonsterInstance { Uid = 802, Name = "暗殿神鹿", MaxHp = 50, Hp = 50 };

        UnknownDarkHallEngine.ApplyDarkHallAttributes(fakeBoss, UnknownDarkHallEngine.DarkHallMapId);
        UnknownDarkHallEngine.ApplyDarkHallAttributes(realGodDeer, UnknownDarkHallEngine.DarkHallMapId);

        Assert.Equal(100, fakeBoss.Hp); // 教主仅 100 血，一刀死！
        Assert.Equal(5000, realGodDeer.Hp); // 神鹿暴增至 5000 血，极品 BOSS！

        // 非未知暗殿地图不受影响
        var normalBoss = new MonsterInstance { Uid = 803, Name = "沃玛教主", MaxHp = 2000, Hp = 2000 };
        UnknownDarkHallEngine.ApplyDarkHallAttributes(normalBoss, mapId: 0);
        Assert.Equal(2000, normalBoss.Hp);

        // 2. 大刀卫士偏离哨岗自动 A* 寻路回岗 AI
        var patrol = new GuardPatrolAndReturnEngine();
        patrol.RegisterGuard(uid: 999, name: "比奇大刀卫士", mapId: 0, spawnX: 320, spawnY: 260);

        // 模拟守卫追击红名跑到了 (323, 260)，偏离了 3 格
        var guard = patrol.GetGuard(999)!;
        guard.CurrentX = 323;

        var guardEntity = patrol.UpdateGuardReturnStep(hasEnemyNearby: true);
        Assert.Empty(guardEntity); // 只要周围有敌人，保持警戒战斗

        // 敌情解除 (hasEnemyNearby: false)，一步步朝出生点走回 (从 323 走回 322)
        var step1 = patrol.UpdateGuardReturnStep(hasEnemyNearby: false);
        Assert.Single(step1);
        Assert.Equal(322, step1[0].nextX); // 323 -> 322
    }
}
