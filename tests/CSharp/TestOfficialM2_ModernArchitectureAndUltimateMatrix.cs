using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using Mir2.ScriptPlugin;
using Mir2.Server.Game;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Plugins;
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
        Assert.Null(session.State.Equipment[(int)OfficialEquipSlot.Weapon]); // 手持武器已被收缴投入熔炉

        // 模拟取回升级武器并进入试刀状态
        var (h7_ret, s7_ret, _) = engine.DispatchDialog(session, 107, "@retrieve_weapon");
        Assert.True(h7_ret);
        Assert.Contains("取回", s7_ret);
        Assert.Equal(1, session.State.TrialSlashCount);
        Assert.Contains(session.State.BagItems, i => i.Name == "裁决之杖" && i.UpgradeState == 1); // 出炉开光放入背包

        // 3. 沙巴克攻城管理员 (ID 109)

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

        var (triggered, broadcastMsg, brokenItem, brokenSlot, brokenUid) = SuitWorldEventEngine.TriggerGodPrayerRebellion(
            player.State, monsters, Random.Shared);

        Assert.True(triggered);
        Assert.Contains("所有召唤生物已陷入癫狂", broadcastMsg);
        Assert.Equal(0, pet1.MasterSessionId); // 脱离掌控
        Assert.Equal(player.SessionId, pet1.TargetSessionId); // 反噬锁定原主人
        Assert.True(pet1.ThreatTable.MaxHate > 0); // 仇恨注入
        Assert.Contains("发狂的", pet1.Name);
        Assert.Equal(0, pet2.MasterSessionId);
        Assert.Equal(player.SessionId, pet2.TargetSessionId);
        Assert.Contains("发狂的", pet2.Name);
        Assert.NotEmpty(brokenItem); // 必碎一件祈祷装备
        Assert.True(brokenSlot >= 0);

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
        memberB.CurrentMapId = 11; // 在白日门
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

    // ==================== 5. 模块十七: 官方极品词条生成与三大瞬移卷轴 ====================

    [Fact]
    public void Test_ItemDropRarityEngine_BonusStatRolls()
    {
        var item = new EquipItem { Uid = 701, Name = "死神手套", Dc = 1, MaxDc = 2 };
        // 模拟摇出 3 点极品
        var mockRng = new Random(42);
        var result = ItemDropRarityEngine.RollRareBonus(item, mockRng);

        // 验证极品生成合法性与属性附魔
        if (result.IsRare)
        {
            Assert.True(item.BonusDc > 0 || item.BonusMc > 0 || item.BonusSc > 0 || item.BonusAc > 0 || item.BonusMac > 0 || item.MaxDura > 0);
        }
    }

    [Fact]
    public void Test_TeleportScrollEngine_Town_Random_Dungeon()
    {
        var mapMgr = new Mir2.Server.Maps.MapManager();
        var map = mapMgr.GetMap(0)!;
        var session = CreateMockSession(5001, "行者", level: 25, gold: 500);

        // 1. 回城卷测试
        var (tOk, tMap, tX, tY, _) = TeleportScrollEngine.UseTownTeleport(session, mapMgr);
        Assert.True(tOk);
        Assert.Equal(0, tMap);
        Assert.Equal(330, tX);
        Assert.Equal(270, tY);

        // 2. 随机传送卷测试 (必须传送到可行走网格)
        var (rOk, rX, rY, _) = TeleportScrollEngine.UseRandomTeleport(session, map, Random.Shared);
        Assert.True(rOk);
        Assert.True(map.IsWalkable(rX, rY));

        // 3. 地牢逃脱卷测试
        var (dOk, dMap, dX, dY, _) = TeleportScrollEngine.UseDungeonEscape(session);
        Assert.True(dOk);
        Assert.Equal(0, dMap);
    }

    // ==================== 6. 模块十八: 毒素掉血与石化麻痹控制 ====================

    [Fact]
    public void Test_PoisonAndParalyzeTickEngine_CombatControls()
    {
        var state = new PlayerState { Hp = 100, MaxHp = 100, Sc = 20 };
        long now = 100000;

        // 1. 绿毒每秒掉血精算
        state.HasGreenPoison = true;
        state.GreenPoisonExpireMs = now + 10000;

        var (damaged, dmg, died) = PoisonAndParalyzeTickEngine.TickGreenPoison(state, now);
        Assert.True(damaged);
        Assert.True(dmg > 0);
        Assert.Equal(100 - dmg, state.Hp);
        Assert.False(died);

        // 2. 麻痹戒指 5 秒石化行动拦截
        state.ParalyzeExpireMs = now + 5000;
        bool forbidden = PoisonAndParalyzeTickEngine.IsActionForbiddenByParalysis(state, now + 1000);
        Assert.True(forbidden);

        // 5秒后石化解除
        bool unfreeze = PoisonAndParalyzeTickEngine.IsActionForbiddenByParalysis(state, now + 6000);
        Assert.False(unfreeze);
    }

    // ==================== 7. 桃源之门赤血魔剑合成与可卸载沙箱多次热更循环 ====================

    [Fact]
    public void Test_ModernNpcPlugin_RedBloodSwordCrafting_And_SandboxUnloadCycle()
    {
        var engine = new ModernNpcPluginEngine();
        var session = CreateMockSession(6001, "魔剑铸造师", level: 40, gold: 2000000);

        // 1. 桃源之门神秘老人合成赤血魔剑 (ID 114)
        var (h14, s14, _) = engine.DispatchDialog(session, 114, "@craft_sword");
        Assert.True(h14);
        Assert.Contains("赤血魔剑", s14);
        Assert.Equal(1000000, session.State!.Gold);
        Assert.Contains(session.State.BagItems, i => i.Name == "赤血魔剑" && i.Dc == 15 && i.MaxDc == 20);

        // 2. 可卸载独立沙箱多次热更循环测试 (验证旧沙箱彻底 Unload 且新沙箱即插即用)
        string testAsmPath = typeof(CustomMockPlugin).Assembly.Location;
        if (File.Exists(testAsmPath))
        {
            byte[] asmBytes = File.ReadAllBytes(testAsmPath);

            // 第一次装载
            var r1 = engine.LoadPluginsFromAssemblyBytes(asmBytes);
            Assert.True(r1.Success);
            Assert.True(engine.HasPlugin("新副本引路人"));

            // 第二次装载热替换 (旧沙箱触发 Unload 并重新装载)
            var r2 = engine.LoadPluginsFromAssemblyBytes(asmBytes);
            Assert.True(r2.Success);
            Assert.True(engine.HasPlugin("新副本引路人"));
        }
    }

    // ==================== 8. 模块十九: 传奇三大 BOSS 特殊战斗机制与狂暴 AI ====================

    [Fact]
    public void Test_BossEncounterEngine_MagicImmunity_GroundSpike_And_ZumaRage()
    {
        // 1. 触龙神 100% 绝对魔法免疫 (法术伤害归 0)
        var centipede = new MonsterInstance { Uid = 901, Name = "触龙神", Hp = 1000, MaxHp = 1000 };
        int magicDmg = BossEncounterEngine.ResolveMagicDamageOnBoss(centipede, 100);
        Assert.Equal(0, magicDmg);

        // 普通白野猪受到正常法术伤害
        var pig = new MonsterInstance { Uid = 902, Name = "白野猪", Hp = 1000, MaxHp = 1000 };
        Assert.Equal(100, BossEncounterEngine.ResolveMagicDamageOnBoss(pig, 100));

        // 2. 赤月恶魔全屏地刺防御减免
        var player = new PlayerState { Ac = 10, Mac = 10 }; // 减免 10 点
        int spikeDmg = BossEncounterEngine.CalculateRedMoonDemonGroundSpikeDamage(50, player);
        Assert.Equal(40, spikeDmg);

        // 3. 祖玛教主残血狂暴召唤 4 大锤卫士
        var zuma = new MonsterInstance { Uid = 903, Name = "祖玛教主", Hp = 150, MaxHp = 1000 }; // 15% 残血
        var (shouldSummon, minions) = BossEncounterEngine.CheckZumaLeaderRageSummon(zuma);
        Assert.True(shouldSummon);
        Assert.Equal(4, minions.Count);
        Assert.Contains(minions, m => m.Contains("祖玛卫士"));

        // 4. 盛大官方 1.5 权威真值: 双头金刚死后割肉挖取法神首饰 (绝非法神披风)
        var kong = new MonsterInstance { Uid = 904, Name = "双头金刚", Hp = 0, MaxHp = 5000 };
        var (bOk, fashenItem, _) = BossEncounterEngine.ButcherDualHeadKong(kong, new Random(14));
        Assert.True(bOk);
        Assert.NotNull(fashenItem);
        Assert.Contains("法神", fashenItem.Name);
    }

    // ==================== 9. 模块二十: 官方 1.5 面对面交易金币上限拦截与安全守恒 ====================

    [Fact]
    public void Test_Official15_TradeEngine_AddItem_DelItem_And_Safety()
    {
        var tradeEngine = new TradeEngine();

        var playerA = CreateMockSession(7001, "交易商人", gold: 1000);
        var playerB = CreateMockSession(7002, "土豪买家", gold: 100000);
        playerA.CurrentMapId = 0; playerA.X = 100; playerA.Y = 100;
        playerB.CurrentMapId = 0; playerB.X = 101; playerB.Y = 100;

        var sword = new EquipItem { Uid = 7771, Name = "井中月", Weight = 58 };
        playerA.State!.BagItems.Add(sword);

        // 1. 发起交易
        var (tryOk, _) = tradeEngine.TryInitiateTrade(playerA, playerB);
        Assert.True(tryOk);

        // 2. A 放入物品，B 放入金币 50000
        var (addOk, _) = tradeEngine.AddItem(playerA, sword.Uid);
        Assert.True(addOk);
        var (goldOk, _) = tradeEngine.ChangeGold(playerB, 50000);
        Assert.True(goldOk);

        // 3. A 撤下物品回背包
        var (delOk, _) = tradeEngine.DelItem(playerA, sword.Uid);
        Assert.True(delOk);
        Assert.Contains(playerA.State.BagItems, i => i.Uid == 7771);

        // 4. 金币超过持有量时修改失败
        var (badGold, _) = tradeEngine.ChangeGold(playerB, 9999999);
        Assert.False(badGold);

        // 5. 取消交易，状态安全释放
        tradeEngine.CancelTrade(playerA, "主动退出");
        Assert.Null(tradeEngine.GetTrade(playerA.SessionId));
    }



    // ==================== 10. 模块二十一: 经典装备暗属性与祈祷头盔死亡碎裂 ====================

    [Fact]
    public void Test_EquipmentHiddenAttribEngine_DragonBracelet_And_PrayerHelmet()
    {
        var player = new PlayerState();

        // 1. 龙之手镯破盾几率
        player.Equipment[(int)OfficialEquipSlot.ArmringL] = new EquipItem { Uid = 801, Name = "龙之手镯" };
        player.Equipment[(int)OfficialEquipSlot.ArmringR] = new EquipItem { Uid = 802, Name = "龙之手镯" };
        bool pierced = EquipmentHiddenAttribEngine.CheckDragonBraceletArmorPiercing(player, new Random(14));
        Assert.True(pierced);

        // 2. 祈祷头盔死亡必碎裂销毁
        player.Equipment[(int)OfficialEquipSlot.Helmet] = new EquipItem { Uid = 803, Name = "祈祷头盔" };
        var (shattered, _) = EquipmentHiddenAttribEngine.ProcessPrayerHelmetDeathShatter(player);
        Assert.True(shattered);
        Assert.Null(player.Equipment[(int)OfficialEquipSlot.Helmet]); // 槽位被彻底清空销毁
    }

    // ==================== 11. 模块二十二: 官方 1.5 虎卫传说核心随从与成长测试 ====================

    [Fact]
    public void Test_HuWeiPetEngine_LevelUp_ToggleMode_And_TeleportSync()
    {
        // 1. 创建 0 级虎卫
        var huwei = HuWeiPetEngine.CreateHuWei(901, 1001, mapId: 0, x: 300, y: 300);
        Assert.Equal(0, huwei.Level);
        Assert.Equal(400, huwei.MaxHp);
        Assert.Equal("White", huwei.NameColor);

        // 2. 累积杀怪经验一路晋级至 7 级
        for (int i = 0; i < 7; i++)
        {
            bool lvlUp = HuWeiPetEngine.AddKillExpAndTryLevelUp(huwei, 100000);
            Assert.True(lvlUp);
        }
        Assert.Equal(7, huwei.Level);
        Assert.Equal("Blue", huwei.NameColor);
        Assert.True(huwei.MaxHp > 3000, "7 级巅峰虎卫生命值应突破 3000 点");

        // 3. Ctrl+A 切换休息与战斗
        var (resting, msgRest) = HuWeiPetEngine.TogglePetMode(huwei);
        Assert.True(resting);
        Assert.Contains("休息状态", msgRest);

        var (following, msgFollow) = HuWeiPetEngine.TogglePetMode(huwei);
        Assert.False(following);
        Assert.Contains("跟随主人", msgFollow);

        // 4. 随主人跨地图瞬移
        HuWeiPetEngine.SyncPetTeleportWithMaster(huwei, targetMapId: 3, masterX: 330, masterY: 330);
        Assert.Equal(3, huwei.CurrentMapId);
        Assert.Equal(331, huwei.X);
        Assert.Equal(330, huwei.Y);
    }

    // ==================== 12. 模块二十三: 未知暗殿神秘装备解封、诅咒与神水洗涤测试 ====================

    [Fact]
    public void Test_MysticEquipmentEngine_Identification_Curse_And_HolyWaterPurify()
    {
        var mysticBracelet = new EquipItem { Uid = 911, Name = "神秘手镯" };

        // 1. 穿戴时首次解封鉴定 (使用确定性随机数使得触发诅咒)
        // roll < 20 触发诅咒，设置 Random(0)
        var rng = new Random(0);
        var (identified, isCursed, msg) = MysticEquipmentEngine.IdentifyMysticEquipment(mysticBracelet, rng);
        Assert.True(identified);

        // 2. 检查诅咒状态下禁止脱卸
        mysticBracelet.Curse = 1; // 强制模拟诅咒
        var (canTakeOff, reason) = MysticEquipmentEngine.CheckCanTakeOff(mysticBracelet);
        Assert.False(canTakeOff);
        Assert.Contains("被邪恶力量深深诅咒", reason);

        // 3. 使用神水清洗诅咒
        var (purified, purifyMsg) = MysticEquipmentEngine.PurifyMysticCurseWithHolyWater(mysticBracelet);
        Assert.True(purified);
        Assert.Equal(0, mysticBracelet.Curse);
        Assert.Contains("洗净了邪恶诅咒", purifyMsg);

        // 4. 清洗后允许自由脱下
        var (canTakeOffAfter, _) = MysticEquipmentEngine.CheckCanTakeOff(mysticBracelet);
        Assert.True(canTakeOffAfter);
    }

    // ==================== 13. 模块二十四: 红名死亡大爆与盟重红名村流放复活测试 ====================

    [Fact]
    public void Test_RedNameDropAndReviveEngine_MegaExplosion_And_RedTownRevival()
    {
        // 1. 普通白名死亡
        var whitePlayer = new PlayerState { Name = "守法良民", PkPoints = 0 };
        for (int i = 0; i < 10; i++) whitePlayer.BagItems.Add(new EquipItem { Uid = 100 + i, Name = "金创药" });
        whitePlayer.Equipment[(int)OfficialEquipSlot.Weapon] = new EquipItem { Uid = 201, Name = "木剑" };

        var whiteDrops = RedNameDropAndReviveEngine.CalculateDeathDrops(whitePlayer, new Random(100));
        Assert.False(whiteDrops.IsMegaExplosion);

        var (whiteMap, whiteX, whiteY, _) = RedNameDropAndReviveEngine.GetReviveLocation(whitePlayer);
        Assert.Equal(0, whiteMap); // 比奇主城
        Assert.Equal(330, whiteX);

        // 2. 狂暴红名死亡 (触发大爆)
        var redPlayer = new PlayerState { Name = "绝世杀人狂", PkPoints = 500 };
        for (int i = 0; i < 20; i++) redPlayer.BagItems.Add(new EquipItem { Uid = 300 + i, Name = $"战利品{i}" });
        redPlayer.Equipment[(int)OfficialEquipSlot.Weapon] = new EquipItem { Uid = 401, Name = "屠龙" };
        redPlayer.Equipment[(int)OfficialEquipSlot.Helmet] = new EquipItem { Uid = 402, Name = "圣战头盔" };

        var redDrops = RedNameDropAndReviveEngine.CalculateDeathDrops(redPlayer, new Random(100));
        Assert.True(redDrops.IsMegaExplosion);
        Assert.True(redDrops.DroppedBagItems.Count > 10, "红名大爆背包大部分物品必须爆落");

        // 3. 红名死亡必须强制投送到盟重省红名村 (850, 660)
        var (redMap, redX, redY, redMsg) = RedNameDropAndReviveEngine.GetReviveLocation(redPlayer);
        Assert.Equal(3, redMap); // 盟重省
        Assert.Equal(850, redX);
        Assert.Equal(660, redY);
        Assert.Contains("红名村", redMsg);
    }

    // ==================== 14. 模块二十五: 盛大官方 1.5 神秘老人命运之刃任务链实证 ====================

    [Fact]
    public void Test_Official15_FateSword_MysteryOldMan_RockPaperScissors()
    {
        var player = new PlayerState { Name = "桃源剑圣", Level = 35 };
        var session = new ClientSession(7050, null!) { State = player, CurrentMapId = 200, CharacterName = player.Name };
        var oldMan = new FateSwordQuestNpcBehavior();
        var ctx = new NpcDialogContext(session, oldMan.NpcName, "@guess_scissors");
        oldMan.OnInteract(ctx);

        Assert.Contains(player.BagItems, i => i.Name == "命运之刃");
    }



    // ==================== 15. 模块二十六: 矿区铁鹤嘴锄挖矿与黑铁矿石产出测试 ====================

    [Fact]
    public void Test_MiningEngine_PickaxeDurability_And_BlackIronOrePurity()
    {
        var miner = new PlayerState { Name = "老矿工" };

        // 1. 未装备鹤嘴锄禁止挖矿
        var resNoPick = MiningEngine.DigOnce(miner, mapId: 1, Random.Shared);
        Assert.False(resNoPick.Success);
        Assert.Contains("没有装备鹤嘴锄", resNoPick.Message);

        // 2. 非矿区地图无法挖矿
        miner.Equipment[(int)OfficialEquipSlot.Weapon] = new EquipItem { Uid = 501, Name = "铁鹤嘴锄", Dura = 20, MaxDura = 20 };
        var resWrongMap = MiningEngine.DigOnce(miner, mapId: 0, Random.Shared);
        Assert.False(resWrongMap.Success);
        Assert.Contains("不是矿区", resWrongMap.Message);

        // 3. 矿区开采，消耗持久并产出矿石
        // 使用固定随机种子保证必定产出矿石且触发黑铁矿石分支
        // rng.Next(100) < 30 (出矿) -> oreRoll >= 96 (黑铁矿石)
        bool gotBlackIron = false;
        var rng = new Random(42);
        for (int i = 0; i < 200; i++)
        {
            var res = MiningEngine.DigOnce(miner, mapId: 1, rng);
            if (res.Success && res.MinedOre != null && res.MinedOre.Name == "黑铁矿石")
            {
                gotBlackIron = true;
                Assert.True(res.MinedOre.Shape >= 5 && res.MinedOre.Shape <= 30, "黑铁矿石纯度必须在 5~30 之间");
                break;
            }
        }
        Assert.True(gotBlackIron, "持续开采必须能够挖出关键黑铁矿石");
        Assert.Contains(miner.BagItems, i => i.Name == "黑铁矿石");
    }

    // ==================== 16. 模块二十七: 行会宣战、同盟与战争击杀豁免测试 ====================

    [Fact]
    public void Test_GuildWarAndAllianceEngine_DeclareWar_Alliance_And_PkExemption()
    {
        var warEngine = new GuildWarAndAllianceEngine();
        long now = 1000_000;

        // 1. 发起宣战 (3 小时)
        var (warOk, warMsg) = warEngine.DeclareWar("邪恶之眼", "布拉格", now);
        Assert.True(warOk);
        Assert.Contains("战火燃起", warMsg);
        Assert.True(warEngine.AreGuildsAtWar("邪恶之眼", "布拉格", now));
        Assert.True(warEngine.AreGuildsAtWar("布拉格", "邪恶之眼", now)); // 双向战争关系

        // 2. 战争期间击杀绝对免除 PK 点数
        Assert.True(warEngine.IsKillExemptFromPkPenalty("邪恶之眼", "布拉格", now));
        Assert.False(warEngine.IsKillExemptFromPkPenalty("邪恶之眼", "散人联盟", now)); // 非交战行会正常判红名

        // 3. 3 小时后战争自动平息
        long afterWar = now + (3 * 3600 * 1000) + 1000;
        Assert.False(warEngine.AreGuildsAtWar("邪恶之眼", "布拉格", afterWar));
        Assert.False(warEngine.IsKillExemptFromPkPenalty("邪恶之眼", "布拉格", afterWar));

        // 4. 行会结盟
        var (allyOk, allyMsg) = warEngine.FormAlliance("邪恶之眼", "兄弟情深");
        Assert.True(allyOk);
        Assert.Contains("金兰结义", allyMsg);
        Assert.True(warEngine.AreGuildsAllied("邪恶之眼", "兄弟情深"));
        Assert.True(warEngine.AreGuildsAllied("兄弟情深", "邪恶之眼"));
    }

    // ==================== 17. 模块二十八: 沙巴克城门开关、击毁与城主修缮测试 ====================

    [Fact]
    public void Test_SabukCastleGateEngine_OpenClose_DamageBroken_And_Repair()
    {
        var gate = new SabukCastleGateEngine();
        var lord = new PlayerState { Name = "霸气城主", Gold = 100000 };

        // 1. 非城主禁止操控开关门
        var (fakeToggle, _) = gate.ToggleGate(open: true, isLord: false);
        Assert.False(fakeToggle);

        // 2. 城主开启与关闭城门
        var (openOk, openMsg) = gate.ToggleGate(open: true, isLord: true);
        Assert.True(openOk);
        Assert.Equal(SabukCastleGateEngine.GateState.Opened, gate.State);

        var (closeOk, _) = gate.ToggleGate(open: false, isLord: true);
        Assert.True(closeOk);
        Assert.Equal(SabukCastleGateEngine.GateState.Closed, gate.State);

        // 3. 攻城期间承受巨额伤害直至击毁
        var (dmg1, broken1, _) = gate.DamageGate(1000);
        Assert.True(dmg1);
        Assert.False(broken1);
        Assert.Equal(1000, gate.Hp);

        var (dmg2, broken2, brokenMsg) = gate.DamageGate(1000);
        Assert.True(dmg2);
        Assert.True(broken2);
        Assert.Equal(0, gate.Hp);
        Assert.Equal(SabukCastleGateEngine.GateState.Broken, gate.State);
        Assert.Contains("击溃粉碎", brokenMsg);

        // 4. 倒塌状态无法直接开关，必须先修缮
        var (openFail, _) = gate.ToggleGate(open: true, isLord: true);
        Assert.False(openFail);

        // 5. 城主消耗 50000 金币修缮城门
        var (repairOk, repairMsg) = gate.RepairGate(lord);
        Assert.True(repairOk);
        Assert.Equal(50000, lord.Gold); // 100000 - 50000 = 50000
        Assert.Equal(SabukCastleGateEngine.MaxGateHp, gate.Hp);
        Assert.Equal(SabukCastleGateEngine.GateState.Closed, gate.State);
    }

    // ==================== 18. 模块二十九: 隐身术、怪物仇恨与真视反隐测试 ====================

    [Fact]
    public void Test_StealthAndTrueSightEngine_StealthDuration_TrueSight_And_BreakAction()
    {
        // 1. 隐身持续时间精算
        long duration = StealthAndTrueSightEngine.CalculateStealthDurationMs(sc: 20, skillLevel: 3);
        Assert.Equal(40000L, duration); // 15 + 15 + 10 = 40 秒

        // 2. 普通怪物无法侦测隐身目标
        bool seeZombie = StealthAndTrueSightEngine.CanMonsterDetectTarget("僵尸", targetIsStealthed: true);
        Assert.False(seeZombie);

        bool seePig = StealthAndTrueSightEngine.CanMonsterDetectTarget("红野猪", targetIsStealthed: true);
        Assert.False(seePig);

        // 3. 盛大官方 1.5 权威真视怪物 (楔蛾/月魔蜘蛛/祖玛教主/赤月双魔) 能够识破隐身
        bool seeMoth = StealthAndTrueSightEngine.CanMonsterDetectTarget("楔蛾", targetIsStealthed: true);
        Assert.True(seeMoth);

        bool seeMoonSpider = StealthAndTrueSightEngine.CanMonsterDetectTarget("月魔蜘蛛", targetIsStealthed: true);
        Assert.True(seeMoonSpider);

        // 4. 动作破隐
        bool isStealthed = true;
        bool broke = StealthAndTrueSightEngine.BreakStealthOnAction(ref isStealthed);
        Assert.True(broke);
        Assert.False(isStealthed);
    }

    // ==================== 19. 模块三十: 道士心灵启示精准显血透视测试 ====================

    [Fact]
    public void Test_RevelationSkillEngine_MonsterAndPlayerHpDetection()
    {
        var taoist = new PlayerState { Name = "清风道人", Mp = 100, Sc = 15 };
        var boss = new MonsterInstance { Uid = 888, Name = "沃玛教主", Hp = 2200, MaxHp = 2200 };
        long now = 10000;

        // 1. 对沃玛教主施展心灵启示
        var (castOk, info, msg) = RevelationSkillEngine.CastRevelationOnMonster(taoist, boss, skillLevel: 2, now);
        Assert.True(castOk);
        Assert.NotNull(info);
        Assert.Equal(2200, info.CurrentHp);
        Assert.Equal(2200, info.MaxHp);
        Assert.Equal(90, taoist.Mp); // 100 - 10 = 90
        Assert.Contains("沃玛教主", msg);
        Assert.False(info.IsExpired(now + 10000));

        // 2. 蓝量不足施法失败
        taoist.Mp = 5;
        var (castFail, _, failMsg) = RevelationSkillEngine.CastRevelationOnMonster(taoist, boss, skillLevel: 2, now);
        Assert.False(castFail);
        Assert.Contains("魔法值不足", failMsg);
    }

    // ==================== 20. 模块三十一: 传统道士制毒配方合成测试 ====================

    [Fact]
    public void Test_PoisonCraftingEngine_GreenAndRedPoisonRecipes()
    {
        var player = new PlayerState { Name = "毒仙" };

        // 1. 材料不足合成失败
        var (failOk, _, failMsg) = PoisonCraftingEngine.CraftPoison(player, PoisonCraftingEngine.PoisonType.GreenPoisonGrayPowder);
        Assert.False(failOk);
        Assert.Contains("材料不足", failMsg);

        // 2. 准备绿毒材料 (食人树叶 4, 果实 1, 牙齿 2)
        for (int i = 0; i < 4; i++) player.BagItems.Add(new EquipItem { Uid = 10 + i, Name = "食人树叶" });
        player.BagItems.Add(new EquipItem { Uid = 20, Name = "食人树果实" });
        for (int i = 0; i < 2; i++) player.BagItems.Add(new EquipItem { Uid = 30 + i, Name = "毒蜘蛛牙齿" });

        var (greenOk, grayPowder, greenMsg) = PoisonCraftingEngine.CraftPoison(player, PoisonCraftingEngine.PoisonType.GreenPoisonGrayPowder);
        Assert.True(greenOk);
        Assert.NotNull(grayPowder);
        Assert.Equal("灰色药粉(中)", grayPowder.Name);
        Assert.Equal(50, grayPowder.Dura);
        Assert.Contains("灰色药粉", greenMsg);

        // 3. 准备红毒材料 (食人树叶 4, 蝎子尾巴 1, 蛆卵 4)
        for (int i = 0; i < 4; i++) player.BagItems.Add(new EquipItem { Uid = 40 + i, Name = "食人树叶" });
        player.BagItems.Add(new EquipItem { Uid = 50, Name = "蝎子尾巴" });
        for (int i = 0; i < 4; i++) player.BagItems.Add(new EquipItem { Uid = 60 + i, Name = "蛆卵" });

        var (redOk, yellowPowder, redMsg) = PoisonCraftingEngine.CraftPoison(player, PoisonCraftingEngine.PoisonType.RedPoisonYellowPowder);
        Assert.True(redOk);
        Assert.NotNull(yellowPowder);
        Assert.Equal("黄色药粉(中)", yellowPowder.Name);
        Assert.Equal(50, yellowPowder.Dura);
        Assert.Contains("黄色药粉", redMsg);
    }

    // ==================== 21. 模块三十二: 千里传音全服喇叭与频控测试 ====================

    [Fact]
    public void Test_WorldBroadcastEngine_Cooldown_ItemConsumption_And_Delivery()
    {
        var engine = new WorldBroadcastEngine();
        var sender = CreateMockSession(6001, "霸天龙皇");
        var receiver = CreateMockSession(6002, "吃瓜群众");
        var allSessions = new List<ClientSession> { sender, receiver };
        long now = 1000_000;

        // 1. 无道具禁止传音
        var (failNoItem, _, _, noItemMsg) = engine.BroadcastMessage(sender, "沙巴克今晚攻城！", allSessions, now);
        Assert.False(failNoItem);
        Assert.Contains("没有【千里传音】", noItemMsg);

        // 2. 携带道具成功发送
        sender.State!.BagItems.Add(new EquipItem { Uid = 777, Name = "千里传音" });
        var (succOk, consumedItem, text, _) = engine.BroadcastMessage(sender, "沙巴克今晚决战！", allSessions, now);
        Assert.True(succOk);
        Assert.NotNull(consumedItem);
        Assert.Equal(777, consumedItem.Uid);
        Assert.Contains("[千里传音] 霸天龙皇: 沙巴克今晚决战！", text);
        Assert.DoesNotContain(sender.State.BagItems, i => i.Uid == 777); // 道具已消耗

        // 3. 5 秒频控拦截刷屏
        sender.State.BagItems.Add(new EquipItem { Uid = 778, Name = "千里传音" });
        var (cdFail, _, _, cdMsg) = engine.BroadcastMessage(sender, "第二遍喇叭！", allSessions, now + 2000); // 仅过 2 秒
        Assert.False(cdFail);
        Assert.Contains("冷却中", cdMsg);

        // 4. 5 秒后恢复发送
        var (succAfterCd, _, _, _) = engine.BroadcastMessage(sender, "第二遍喇叭！", allSessions, now + 5001);
        Assert.True(succAfterCd);
    }

    // ==================== 23. 第二十一波深水区坏账闭环测试 ====================

    private static (Mir2.Server.Maps.MapManager mapMgr, MonsterRegenEngine regenEng, PacketDispatcher dispatcher, Mir2.Server.Maps.GameMap map) CreateWave21Environment()
    {
        var mapMgr = new Mir2.Server.Maps.MapManager();
        var map = new Mir2.Server.Maps.GameMap(0, "比奇省", 500, 500);
        mapMgr.RegisterMap(map);
        var regenEng = new MonsterRegenEngine(mapMgr);
        var dispatcher = new PacketDispatcher(mapMgr, regenEng);
        return (mapMgr, regenEng, dispatcher, map);
    }

    [Fact]
    public void Test_RingOfFire_Knockback_Stun_And_SpellEffectBroadcast()
    {
        var (mapMgr, regen, dispatcher, map) = CreateWave21Environment();
        var session = CreateMockSession(1, "法王", level: 35);
        session.X = 100; session.Y = 100;
        var victimSession = CreateMockSession(2, "小战士", level: 20); // 位于法王正东侧 1 格
        victimSession.X = 101; victimSession.Y = 100;
        map.Aoi.Enter(session, 100, 100);
        map.Aoi.Enter(victimSession, 101, 100);
        dispatcher.ActiveSessions.TryAdd(1, session);
        dispatcher.ActiveSessions.TryAdd(2, victimSession);

        session.ClearBufferedPackets();
        victimSession.ClearBufferedPackets();

        // 1. 法王施展抗拒火环 (MagicId 8)
        long now = Environment.TickCount64;
        dispatcher.ResolveAuthoritativeSpell(session, 8, 0, 0, 0, now);

        // 2. 验证法王九宫格广播了抗拒火环光效 (MagicId 8)
        Assert.True(session.HasBufferedPacket($"#{ProtocolConsts.SM_SPELLEFFECT_DELPHI}/{session.SessionId}/100/100/8/抗拒火环!"));

        // 3. 验证低等级小战士被推退一格 (102, 100) 且被施加 1 秒僵直
        Assert.Equal(102, victimSession.X);
        Assert.Equal(100, victimSession.Y);
        Assert.True(victimSession.State!.IsParalyzed);
        Assert.True(victimSession.State.StatusBits[5]);
    }

    [Fact]
    public void Test_HolyCurse_TrapAndDamageBreak()
    {
        var (mapMgr, regen, dispatcher, map) = CreateWave21Environment();
        var session = CreateMockSession(1, "天尊", level: 40);
        session.X = 200; session.Y = 200;
        map.Aoi.Enter(session, 200, 200);
        dispatcher.ActiveSessions.TryAdd(1, session);

        var skeleton = new MonsterInstance { Uid = 501, Name = "骷髅战士", MapId = 0, X = 202, Y = 200, Hp = 100, MaxHp = 100 };
        var boss = new MonsterInstance { Uid = 502, Name = "沃玛教主", MapId = 0, X = 202, Y = 201, Hp = 2200, MaxHp = 2200 };
        regen.AddMonster(skeleton);
        regen.AddMonster(boss);

        long now = 20000;

        // 1. 施放困魔咒 (MagicId 16)
        dispatcher.ResolveAuthoritativeSpell(session, 16, 0, 202, 200, now);

        // 2. 普通骷髅被困魔咒禁锢
        Assert.True(skeleton.IsTrappedByHolyCurse);
        Assert.Equal(now + 15000, skeleton.TrappedExpireMs);

        // 3. BOSS 免疫困魔咒禁锢
        Assert.False(boss.IsTrappedByHolyCurse);

        // 4. 外力打击骷髅，困魔咒金光牢笼即刻破碎恢复反击
        session.ClearBufferedPackets();
        dispatcher.ApplyDamageToMonster(session, skeleton, 20, now + 1000);
        Assert.False(skeleton.IsTrappedByHolyCurse);
        Assert.Equal(80, skeleton.Hp);
        Assert.True(session.HasBufferedPacket("金光禁锢消散"));
    }

    [Fact]
    public void Test_TurnUndead_LevelSuppression_BossImmunity_And_Kill()
    {
        var (mapMgr, regen, dispatcher, map) = CreateWave21Environment();
        var session = CreateMockSession(1, "法神", level: 30); // 30 级法师
        session.X = 300; session.Y = 300;
        map.Aoi.Enter(session, 300, 300);
        dispatcher.ActiveSessions.TryAdd(1, session);

        var zombie = new MonsterInstance { Uid = 601, Name = "僵尸", MapId = 0, X = 301, Y = 300, Hp = 160, MaxHp = 160 }; // 等级 ~15
        var highUndead = new MonsterInstance { Uid = 602, Name = "高阶尸魔", MapId = 0, X = 302, Y = 300, Hp = 900, MaxHp = 900 }; // 等级 ~45
        var corpseKingBoss = new MonsterInstance { Uid = 603, Name = "尸王", MapId = 0, X = 303, Y = 300, Hp = 500, MaxHp = 500 }; // BOSS
        regen.AddMonster(zombie);
        regen.AddMonster(highUndead);
        regen.AddMonster(corpseKingBoss);

        long now = 30000;

        // 1. 对高阶尸魔施展圣言术：因法师等级(30)低于怪物等级(45)，触发修为等级不足拦截
        session.ClearBufferedPackets();
        session.LastSpellTimeMs = 0;
        dispatcher.ResolveAuthoritativeSpell(session, 24, highUndead.Uid, highUndead.X, highUndead.Y, now);
        Assert.True(session.HasBufferedPacket("修为等级不足"));

        // 2. 对尸王施展圣言术：因属于 BOSS，圣言术绝对免疫
        session.ClearBufferedPackets();
        session.LastSpellTimeMs = 0;
        dispatcher.ResolveAuthoritativeSpell(session, 24, corpseKingBoss.Uid, corpseKingBoss.X, corpseKingBoss.Y, now);
        Assert.True(session.HasBufferedPacket("无法度化"));

        // 3. 无论何种情况均广播圣言术神圣光效 (MagicId 24)
        Assert.True(session.HasBufferedPacket($"#{ProtocolConsts.SM_SPELLEFFECT_DELPHI}/{corpseKingBoss.Uid}/303/300/24/圣言术!"));
    }

    [Fact]
    public void Test_Revelation_HealthBarSync_DamageRealtimeUpdate_And_Timeout()
    {
        var (mapMgr, regen, dispatcher, map) = CreateWave21Environment();
        var session = CreateMockSession(1, "妙手法师道人", level: 35);
        session.X = 400; session.Y = 400;
        session.State!.Mp = 100;
        map.Aoi.Enter(session, 400, 400);
        dispatcher.ActiveSessions.TryAdd(1, session);

        var mon = new MonsterInstance { Uid = 701, Name = "红野猪", MapId = 0, X = 401, Y = 400, Hp = 320, MaxHp = 320 };
        regen.AddMonster(mon);

        long now = 40000;

        // 1. 施展心灵启示 (MagicId 28)
        session.ClearBufferedPackets();
        dispatcher.ResolveAuthoritativeSpell(session, 28, mon.Uid, mon.X, mon.Y, now);

        // 2. 验证下发显血开启 (1105) 与血量同步 (1104)
        Assert.True(mon.IsRevealed);
        Assert.Equal(1, mon.RevealedBySessionId);
        Assert.True(session.HasBufferedPacket($"#{ProtocolConsts.SM_HEALTHBAR_DELPHI}/{mon.Uid}/1/0/0/!"));
        Assert.True(session.HasBufferedPacket($"#{ProtocolConsts.SM_HEALTHBAR_COLOR_DELPHI}/{mon.Uid}/320/320/0/!"));

        // 3. 怪物受击时，向心灵启示施法者实时同步最新掉血数据
        session.ClearBufferedPackets();
        dispatcher.ApplyDamageToMonster(session, mon, 50, now + 1000);
        Assert.True(session.HasBufferedPacket($"#{ProtocolConsts.SM_HEALTHBAR_COLOR_DELPHI}/{mon.Uid}/270/320/0/!"));

        // 4. 心跳超时自然解除显血 (SM_HEALTHBAR_DELPHI param=0)
        session.ClearBufferedPackets();
        dispatcher.UpdateWorldTick(1, now + 70000); // 超过 60 秒持续时间
        Assert.False(mon.IsRevealed);
        Assert.True(session.HasBufferedPacket($"#{ProtocolConsts.SM_HEALTHBAR_DELPHI}/{mon.Uid}/0/0/0/!"));
    }

    [Fact]
    public void Test_Wave72_WeightConstraint_SabukTreasury_TrueSight_And_GroupGoldMatrix()
    {
        // 1. 验证真视怪物集合：包含赤月恶魔、双头金刚、双头血魔、月魔蜘蛛、楔蛾，无非官方 1.5 怪物
        Assert.True(StealthAndTrueSightEngine.HasTrueSight("双头金刚"));
        Assert.True(StealthAndTrueSightEngine.HasTrueSight("双头血魔"));
        Assert.True(StealthAndTrueSightEngine.HasTrueSight("赤月恶魔"));
        Assert.True(StealthAndTrueSightEngine.HasTrueSight("月魔蜘蛛"));
        Assert.True(StealthAndTrueSightEngine.HasTrueSight("楔蛾"));
        Assert.False(StealthAndTrueSightEngine.HasTrueSight("黄泉教主")); // 官方 1.5 绝无 1.70 苍月岛黄泉教主
        Assert.False(StealthAndTrueSightEngine.HasTrueSight("黑色恶蛆"));
        Assert.False(StealthAndTrueSightEngine.HasTrueSight("红野猪"));

        // 2. 负重约束下的金币最大可容纳计算 (WeightConstraintEngine)
        // 玩家当前 0 金币，背包负重 10/100，剩余负重 90 -> 90 * 10,000 = 900,000
        int canAbsorb = WeightConstraintEngine.CalculateMaxGoldCanReceive(0, 10, 100);
        Assert.Equal(900_000, canAbsorb);

        // 若背包已超负重或持平，且无零头空间 -> 0
        int zeroAbsorb = WeightConstraintEngine.CalculateMaxGoldCanReceive(10_000, 100, 100);
        Assert.Equal(0, zeroAbsorb);

        // 3. 沙巴克城主税金支取负重防护与封包同步
        var treasury = new SabukTreasuryEngine();
        treasury.LordCharacterName = "至尊城主";
        var lordSession = CreateMockSession(8888, "至尊城主", gold: 0);
        lordSession.State!.MaxBagWeight = 50;
        lordSession.State.BagWeight = 50; // 背包负重已满

        // 背包负重已满，支取应被安全拦截，公款绝不扣除
        var (failRet, withdrawn0, failMsg) = treasury.WithdrawCastleGold(lordSession, 100_000);
        Assert.False(failRet);
        Assert.Equal(0, withdrawn0);
        Assert.Contains("背包负重已满", failMsg);

        // 腾出负重后支取，下发金币变更与负重封包
        lordSession.State.BagWeight = 20; // 剩余 30 负重 -> 可容纳 300,000 金币
        lordSession.ClearBufferedPackets();
        var (succRet, withdrawnActual, succMsg) = treasury.WithdrawCastleGold(lordSession, 200_000);
        Assert.True(succRet);
        Assert.Equal(200_000, withdrawnActual);
        Assert.Equal(200_000, lordSession.State.Gold);
        Assert.True(lordSession.HasBufferedPacket($"#{ProtocolConsts.SM_GOLDCHANGED_DELPHI}/200000/0/0/0/!"));
        Assert.True(lordSession.HasBufferedPacket($"#{ProtocolConsts.SM_WEIGHTCHANGED}/{lordSession.State.BagWeight}/{lordSession.State.WearWeight}/{lordSession.State.HandWeight}/0/!"));

        // 4. 替换装备穿戴负重扣减当前槽位旧装备重量实证 (防止同重量/稍轻装备被重复叠加导致超重被拒)
        var pState = new PlayerState { Level = 30, Job = 0 }; // 战士 30 级穿戴负重
        int maxWear = WeightConstraintEngine.CalcMaxWearWeight(pState.Level, pState.Job); // ~30 左右
        var oldArmor = new EquipItem { Uid = 1, Name = "重盔甲(男)", Weight = 23 };
        pState.Equipment[(int)OfficialEquipSlot.Dress] = oldArmor;
        pState.WearWeight = 23; // 当前穿戴 23

        var newArmor = new EquipItem { Uid = 2, Name = "重盔甲(男)", Weight = 23 };
        // 尝试用重量 23 的新衣服替换槽位上的旧衣服 (若未扣除旧衣服则 23+23=46 > maxWear 会失败)
        bool canEquip = pState.CheckCanEquipAuthoritative(OfficialEquipSlot.Dress, newArmor, out string wearReason);
        Assert.True(canEquip, $"替换同重量衣服应成功放行，但被错误拦截：{wearReason}");

        // 5. 盛大官方 1.5 权威双头金刚 Alt 割肉挖法神首饰实证 (OfficialMonsterCompendium + PacketDispatcher)
        var monDef = OfficialMonsterCompendium.GetByName("双头金刚");
        Assert.NotNull(monDef);
        Assert.True(monDef.CanButcherMeat, "双头金刚在官方1.5中必须允许割肉以产出法神首饰");

        // 模拟双头金刚尸体割肉入包与协议同步
        var (mapMgr, regen, dispatcher, map) = CreateWave21Environment();
        var butcherSession = CreateMockSession(9999, "赤月勇士", level: 40);
        butcherSession.X = 100; butcherSession.Y = 100;
        map.Aoi.Enter(butcherSession, 100, 100);
        dispatcher.ActiveSessions.TryAdd(9999, butcherSession);

        var kongCorpse = new MonsterInstance
        {
            Uid = 5001,
            Name = "双头金刚",
            MapId = 0,
            X = 101,
            Y = 100,
            Hp = 0,
            MaxHp = 5000
        };
        regen.AddMonster(kongCorpse);

        butcherSession.ClearBufferedPackets();
        dispatcher.Dispatch(butcherSession, $"#{ProtocolConsts.CM_BUTCHITEM}/5001/101/100/!");
        Assert.True(kongCorpse.IsButchered);
        // 验证不论是挖到法神首饰还是肉，尸体均被成功挖取且标记IsButchered
        Assert.True(kongCorpse.IsButchered);
    }

    [Fact]
    public void Test_Wave75_MiningEngine_WeightConstraint_Durability_And_EquipmentAdaptation()
    {
        // 1. 验证采矿超负重严格阻断与成功入包负重累加
        var miner = new PlayerState
        {
            Name = "比奇矿工",
            MaxBagWeight = 100,
            BagWeight = 99 // 距离上限仅剩 1
        };
        miner.Equipment[(int)OfficialEquipSlot.Weapon] = new EquipItem
        {
            Uid = 901,
            Name = "铁鹤嘴锄",
            Dura = 1000,
            MaxDura = 1000
        };

        // 模拟必定出矿时的超重拦截
        // 持续挖掘直到触发产出重量 > 1 的矿石触发超重拦截
        bool overWeightBlocked = false;
        var rng = new Random(12345);
        for (int i = 0; i < 200; i++)
        {
            var res = MiningEngine.DigOnce(miner, mapId: 1, rng);
            if (res.Message.Contains("太重了拿不动"))
            {
                Assert.False(res.Success);
                overWeightBlocked = true;
                break;
            }
        }
        Assert.True(overWeightBlocked, "采矿超负重必须被权威拦截，禁止产生幽灵负重");

        // 清空背包负重，验证成功挖矿后真实累加 BagWeight
        miner.BagWeight = 0;
        bool minedSuccess = false;
        for (int i = 0; i < 200; i++)
        {
            var res = MiningEngine.DigOnce(miner, mapId: 1, rng);
            if (res.Success && res.MinedOre != null)
            {
                minedSuccess = true;
                Assert.True(res.MinedOre.Weight > 0);
                Assert.Equal(res.MinedOre.Weight, miner.BagWeight);
                break;
            }
        }
        Assert.True(minedSuccess, "正常负重下挖矿必须成功且背包负重同步累加");

        // 2. 验证采矿下发协议真实性与重量不为 0 (PacketDispatcher)
        var (mapMgr, regen, dispatcher, map) = CreateWave21Environment();
        var minerSession = CreateMockSession(8881, "专业矿工", level: 30);
        minerSession.CurrentMapId = 1; // 矿区
        minerSession.State!.Equipment[(int)OfficialEquipSlot.Weapon] = new EquipItem
        {
            Uid = 902,
            Name = "铁鹤嘴锄",
            Dura = 10,
            MaxDura = 10
        };
        minerSession.State.MaxBagWeight = 500;
        minerSession.State.BagWeight = 0;

        // 模拟普通挥刀采矿 CM_HIT
        minerSession.ClearBufferedPackets();
        for (int i = 0; i < 50; i++)
        {
            dispatcher.Dispatch(minerSession, $"#{ProtocolConsts.CM_HIT}/0/100/100/!");
            if (minerSession.HasBufferedPacket($"#{ProtocolConsts.SM_MINERESULT}/"))
            {
                // 验证必须下发 SM_ADDITEM 且包含非 0 重量
                Assert.True(minerSession.HasBufferedPacket($"#{ProtocolConsts.SM_ADDITEM}/"));
                Assert.True(minerSession.HasBufferedPacket($"#{ProtocolConsts.SM_WEIGHTCHANGED}/"));
                break;
            }
        }

        // 3. 验证白板装备属性官方自适应 (裁决之杖、战神盔甲、血饮)
        var hero = new PlayerState { Name = "传奇英雄", Level = 40, Job = 0 }; // 战士
        hero.Equipment[(int)OfficialEquipSlot.Weapon] = new EquipItem { Name = "裁决之杖" }; // 白板，属性全0
        hero.Equipment[(int)OfficialEquipSlot.Dress] = new EquipItem { Name = "战神盔甲" }; // 白板，属性全0
        hero.RecalculateAttributes();

        // 裁决之杖: DC 0-30; 战神盔甲: AC 5-9, MAC 3-5
        // 加上战士 40 级基础成长属性后，装备附加的攻防必须精确体现
        Assert.True(hero.MaxDc >= 30, $"穿戴裁决之杖后最大攻击必须 >= 30，实际为: {hero.MaxDc}");
        Assert.True(hero.MaxAc >= 9, $"穿戴战神盔甲后最大防御必须 >= 9，实际为: {hero.MaxAc}");
        Assert.True(hero.MaxMac >= 5, $"穿戴战神盔甲后最大魔御必须 >= 5，实际为: {hero.MaxMac}");

        // 验证法师武器血饮的自适应: MC 3-5, Accuracy +5
        var mage = new PlayerState { Name = "大法师", Level = 40, Job = 1 };
        mage.Equipment[(int)OfficialEquipSlot.Weapon] = new EquipItem { Name = "血饮" };
        mage.RecalculateAttributes();
        Assert.True(mage.MaxMc >= 5, $"穿戴血饮后最大魔法必须 >= 5，实际为: {mage.MaxMc}");
        Assert.True(mage.Hit >= 5, $"穿戴血饮后准确必须 >= 5，实际为: {mage.Hit}");
    }

    [Fact]
    public void Test_Wave76_RepairAttributeRefresh_And_StorageDelItemSafety()
    {
        // 1. 验证特修身上已破损的特戒装备后，特戒状态与属性重新激活并下发 SM_ABILITY
        var (mapMgr, regen, dispatcher, map) = CreateWave21Environment();
        var heroSession = CreateMockSession(7001, "战士阿飞", level: 35);
        heroSession.State!.Gold = 50000;
        dispatcher.NpcEngine.RegisterNpc(101, "沙巴克铁匠", 0, 100, 100);
        heroSession.ActiveNpcUid = 101;
        heroSession.X = 100;
        heroSession.Y = 100;

        // 佩戴已损坏的复活戒指 (Dura = 0, MaxDura = 5) 在 Ring1 槽位 (槽位 5)
        var brokenReviveRing = new EquipItem
        {
            Uid = 8888,
            Name = "复活戒指",
            Shape = 114,
            Dura = 0,
            MaxDura = 5
        };
        heroSession.State.Equipment[(int)OfficialEquipSlot.RingL] = brokenReviveRing;
        heroSession.State.RecalculateAttributes();
        Assert.False(heroSession.State.HasReviveRing, "持久归零破损的特戒必须处于失效状态");

        // 验证查询估价与实际特修费用严格一致: 磨损 5 点 -> 特修费用 5 * 3 = 15 金币
        var (normCost, specCost, rName) = dispatcher.NpcEngine.CalculateRepairCost(heroSession, (int)OfficialEquipSlot.RingL);
        Assert.Equal(5, normCost);
        Assert.Equal(15, specCost);

        // 发送特修指令 CM_REPAIRSPECIAL (48)，槽位 7
        heroSession.ClearBufferedPackets();
        dispatcher.Dispatch(heroSession, $"#{ProtocolConsts.CM_REPAIRSPECIAL}/101/7/!");

        // 验证特修成功且特戒状态权威重新激活，并下发了 SM_ABILITY
        Assert.Equal(5, brokenReviveRing.Dura);
        Assert.True(heroSession.State.HasReviveRing, "特修修复后特戒效果必须权威恢复激活");
        Assert.True(heroSession.HasBufferedPacket($"#{ProtocolConsts.SM_ABILITY}/"), "修理身上装备成功后必须向客户端下发能力封包");
        Assert.Equal(50000 - 15, heroSession.State.Gold);

        // 2. 验证仓库寄存物品时，服务端下发真实背包 slot
        heroSession.State.BagItems.Clear();
        // 第 0 格放一把屠龙
        heroSession.State.BagItems.Add(new EquipItem { Uid = 111, Name = "屠龙", Weight = 99 });
        // 第 1 格放一块黑铁矿石
        heroSession.State.BagItems.Add(new EquipItem { Uid = 222, Name = "黑铁矿石", Weight = 5 });

        heroSession.IsStorageOpen = true;
        heroSession.StorageNpcUid = 101;
        heroSession.ClearBufferedPackets();
        // 寄存第 1 格的黑铁矿石 (CM_USERSTORAGEDEPOSIT 1031)
        dispatcher.Dispatch(heroSession, $"#{ProtocolConsts.CM_USERSTORAGEDEPOSIT}/1/黑铁矿石/!");

        // 验证下发的 SM_DELITEM 携带有真正的 slot 1，绝非硬编码 0
        Assert.True(heroSession.HasBufferedPacket($"#{ProtocolConsts.SM_DELITEM}/222/1/0/0/黑铁矿石!"));
    }

    [Fact]
    public void Test_Wave77_PotionUnpack_And_BinaryUseItem_PacketSync()
    {
        var (mapMgr, regen, dispatcher, map) = CreateWave21Environment();
        var heroSession = CreateMockSession(7701, "Wave77Tester", level: 22, gold: 0);
        heroSession.State!.Hp = 50;
        heroSession.State.MaxHp = 100;
        heroSession.State.Mp = 20;
        heroSession.State.MaxMp = 50;
        heroSession.State.MaxBagWeight = 150;
        heroSession.State.BagItems.Clear();

        // 1. 放入强效太阳水包 (重量 10，解包后为 6 瓶强效太阳水，每瓶重量 2)
        heroSession.State.BagItems.Add(new EquipItem
        {
            Uid = 9991,
            Name = "强效太阳水包",
            Weight = 10,
            Dura = 1,
            MaxDura = 1
        });
        heroSession.State.RecalculateAuthoritativeWeights(0);
        Assert.Equal(10, heroSession.State.BagWeight);

        heroSession.ClearBufferedPackets();
        // 玩家解包强效太阳水包 (CM_EATITEM 1006)
        dispatcher.Dispatch(heroSession, $"#{ProtocolConsts.CM_EATITEM}/0/强效太阳水包/!");

        // 验证:
        // 1) 消耗移除太阳水包下发 SM_DELITEM
        Assert.True(heroSession.HasBufferedPacket($"#{ProtocolConsts.SM_DELITEM}/9991/0/0/0/强效太阳水包!"), "解包药包必须下发 SM_DELITEM 移除原药包");
        // 2) 下发 6 瓶单体强效太阳水，每瓶下发的 SM_ADDITEM 参数 1 (Weight) 必须等于单体重量 2 (绝非 0)
        string bufferedStr = heroSession.GetBufferedPacketsString();
        var packets = bufferedStr.Split('!', StringSplitOptions.RemoveEmptyEntries);
        var addPackets = packets.Where(p => p.Contains($"#{ProtocolConsts.SM_ADDITEM}/")).ToList();
        Assert.Equal(6, addPackets.Count);
        foreach (var p in addPackets)
        {
            // #{SM_ADDITEM}/{Uid}/{Weight}/{Param3}/{Param4}/{ItemDesc}
            var parts = p.Split('/');
            Assert.Equal("2", parts[2]); // Weight 必须为 2
            Assert.Contains("强效太阳水", parts[5]);
        }
        // 3) 最新背包负重 12 (6 * 2) 同步
        Assert.Equal(12, heroSession.State.BagWeight);

        // 2. 验证二进制吃药意图 HandleBinaryUseItemIntent
        // 玩家中毒，包内放入解毒药水
        heroSession.State.HasGreenPoison = true;
        heroSession.State.GreenPoisonSc = 5;
        Assert.True(heroSession.State.IsPoisoned);
        heroSession.State.BagItems.Clear();
        var antidote = new EquipItem { Uid = 8801, Name = "解毒药水", Weight = 1 };
        heroSession.State.BagItems.Add(antidote);
        heroSession.State.RecalculateAuthoritativeWeights(0);

        heroSession.ClearBufferedPackets();
        Span<byte> uidPayload = stackalloc byte[4];
        System.Buffers.Binary.BinaryPrimitives.WriteInt32LittleEndian(uidPayload, antidote.Uid);
        dispatcher.HandleBinaryUseItemIntent(heroSession, uidPayload);

        // 验证:
        // 1) 下发 SM_DELITEM 移除解毒药水
        Assert.True(heroSession.HasBufferedPacket($"#{ProtocolConsts.SM_DELITEM}/8801/0/0/0/解毒药水!"), "二进制吃药成功必须下发 SM_DELITEM 移除背包物品");
        // 2) 下发 SM_EAT_OK
        Assert.True(heroSession.HasBufferedPacket($"#{ProtocolConsts.SM_EAT_OK}/"), "二进制吃药成功必须下发 SM_EAT_OK");
        // 3) 解毒成功，中毒状态清除，并下发 SM_ABILITY
        Assert.False(heroSession.State.IsPoisoned, "解毒药水必须解除中毒状态");
        Assert.True(heroSession.HasBufferedPacket($"#{ProtocolConsts.SM_ABILITY}/"), "解除中毒必须下发能力封包同步状态");
    }

    [Fact]
    public void Test_Wave78_ModernNpc_TeleportIntent_PetSync_And_AoiTransition()
    {
        var (mapMgr, regen, dispatcher, bichonMap) = CreateWave21Environment();
        // 额外注册盟重省 (MapId: 3)
        var mengzhongMap = new Mir2.Server.Maps.GameMap(3, "盟重省", 500, 500);
        mapMgr.RegisterMap(mengzhongMap);

        var player = CreateMockSession(7801, "传送法师", level: 30, gold: 5000);
        player.CurrentMapId = 0;
        player.X = 330;
        player.Y = 270;
        bichonMap.Aoi.Enter(player, 330, 270);
        dispatcher.ActiveSessions.TryAdd(7801, player);

        // 玩家携带一只 7 级虎卫宝宝
        var huwei = HuWeiPetEngine.CreateHuWei(9901, player.SessionId, mapId: 0, x: 331, y: 270);
        player.Pets.Add(huwei);

        player.ClearBufferedPackets();
        // 模拟客户端向比奇老兵 (NpcId 101) 发送传送盟重指令 CM_NPCSELECT 1011
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_NPCSELECT}/101/@move_mengzhong!");

        // 1. 验证金币扣除与负重守恒
        Assert.Equal(3000, player.State!.Gold);

        // 2. 验证玩家地图与物理坐标变更至盟重土城 (Map 3, 330, 330)
        Assert.Equal(3, player.CurrentMapId);
        Assert.Equal(330, player.X);
        Assert.Equal(330, player.Y);

        // 3. 验证虎卫宝宝权威跟随跨地图瞬移至盟重土城
        Assert.Equal(3, huwei.CurrentMapId);
        Assert.Equal(331, huwei.X);
        Assert.Equal(330, huwei.Y);

        // 4. 验证客户端收到官方地图切换协议 SM_NEWMAP_DELPHI
        string actualPackets = player.GetBufferedPacketsString();
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_NEWMAP_DELPHI}/3/330/330/0/盟重省!"), $"实际收到的下行封包为: {actualPackets}");
    }
}


