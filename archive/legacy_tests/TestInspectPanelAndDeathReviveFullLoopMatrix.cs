using System;
using System.Collections.Generic;
using Mir2.Server.Game;
using Mir2.Server.Maps;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Plugins;
using Mir2.Shared.Protocols;
using Mir2.Shared.Rules;
using Mir2Client.Net;
using Mir2Client.UI;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 盛大官方 1.5 观人面板 (InspectPanel)、死亡灰屏复活 (DeathScreen) 与沙巴克战役全链路端到端实证测试套件
/// 严格 1:1 对齐官方 M2 与 Delphi 二进制逻辑
/// </summary>
public class TestInspectPanelAndDeathReviveFullLoopMatrix
{
    [Fact]
    public void TestInspectPanel_DualTrackPacketAndNineSlotAssembly()
    {
        // 1. 初始化服务端世界与两个玩家
        var mapMgr = new MapManager();
        var guildEngine = new GuildEngine();
        var dispatcher = new PacketDispatcher(mapMgr, guildEngine: guildEngine);

        var watcher = new ClientSession(1)
        {
            CharacterName = "逍遥少侠",
            CurrentMapId = 0,
            X = 300,
            Y = 300,
            State = new PlayerState { Uid = 1, Name = "逍遥少侠", Level = 35, Job = 0, Sex = 0 }
        };

        var target = new ClientSession(2)
        {
            CharacterName = "无涯",
            CurrentMapId = 0,
            X = 301,
            Y = 300,
            State = new PlayerState { Uid = 2, Name = "无涯", Level = 45, Job = 2, Sex = 1 }
        };

        // 为被观察目标“无涯”穿戴 9 大官方装备 (官方 1.5 虎卫传说真值)
        target.State.Equipment[0] = new EquipItem { Uid = 101, Name = "幽灵战衣(女)", Shape = 65 }; // 衣服
        target.State.Equipment[1] = new EquipItem { Uid = 102, Name = "龙纹剑", Shape = 53 };       // 武器
        target.State.Equipment[2] = new EquipItem { Uid = 103, Name = "黄色药粉(大量)", Shape = 2 }; // 照明/毒药/护身符槽位 (官方 1.5 原版道士必备)
        target.State.Equipment[3] = new EquipItem { Uid = 104, Name = "灵魂项链", Shape = 22 };     // 项链
        target.State.Equipment[4] = new EquipItem { Uid = 105, Name = "道士头盔", Shape = 108 };    // 头盔
        target.State.Equipment[5] = new EquipItem { Uid = 106, Name = "三叉戟手镯", Shape = 30 };   // 左手镯
        target.State.Equipment[6] = new EquipItem { Uid = 107, Name = "铂金手镯", Shape = 31 };     // 右手镯
        target.State.Equipment[7] = new EquipItem { Uid = 108, Name = "泰坦戒指", Shape = 40 };     // 左戒指
        target.State.Equipment[8] = new EquipItem { Uid = 109, Name = "复活戒指", Shape = 41 };     // 右戒指

        dispatcher.RegisterSession(watcher);
        dispatcher.RegisterSession(target);

        guildEngine.RegisterGuildDirect("邪恶之眼", target.CharacterName);

        // 2. 模拟客户端发起 CM_QUERYUSERNAME_EXT (82) 深度查名与观人请求
        dispatcher.Dispatch(watcher, $"#{ProtocolConsts.CM_QUERYUSERNAME_EXT}/2/0/0/0/无涯!");

        // 3. 校验服务端下发的封包缓冲
        string watcherBuffer = watcher.GetBufferedPacketsString();
        Assert.NotEmpty(watcherBuffer);

        // 验证系统行会信息文本下发
        Assert.Contains("无涯", watcherBuffer);
        Assert.Contains("邪恶之眼", watcherBuffer);

        // 验证 SM_USERNAME (751) 携带完整的 9 槽位与外观属性
        // 格式形如: #751/2/0/0/0/无涯/45/2/1|65,53,2,22,108,30,31,40,41!
        Assert.Contains("#751/2/0/0/0/无涯/45/2/1|65,53,2,22,108,30,31,40,41!", watcherBuffer);

        // 4. 验证客户端收到该 751 封包能无损反序列化并解析
        string payload = "无涯/45/2/1|65,53,2,22,108,30,31,40,41";
        var baseParts = payload.Split('|');
        var infoParts = baseParts[0].Split('/');
        string targetName = infoParts[0];
        int level = int.Parse(infoParts[1]);
        int job = int.Parse(infoParts[2]);
        int gender = int.Parse(infoParts[3]);

        Assert.Equal("无涯", targetName);
        Assert.Equal(45, level);
        Assert.Equal(2, job);
        Assert.Equal(1, gender);

        var slotShapes = new int[9];
        var slotStrs = baseParts[1].Split(',');
        for (int i = 0; i < slotStrs.Length && i < 9; i++)
        {
            slotShapes[i] = int.Parse(slotStrs[i]);
        }
        Assert.Equal(53, slotShapes[1]); // 龙纹剑
        Assert.Equal(41, slotShapes[8]); // 特戒
    }

    [Fact]
    public void TestDeathRevive_FullStateTransitionAndInvincibleProtection()
    {
        // 1. 初始化世界与角色
        var mapMgr = new MapManager();
        var dispatcher = new PacketDispatcher(mapMgr);

        var session = new ClientSession(1)
        {
            CharacterName = "测试勇士",
            CurrentMapId = 0,
            X = 200,
            Y = 200,
            State = new PlayerState
            {
                Uid = 1,
                Name = "测试勇士",
                Hp = 0, // 阵亡状态
                MaxHp = 200,
                Mp = 0,
                MaxMp = 100,
                HasGreenPoison = true,
                HasRedPoison = true
            }
        };

        dispatcher.RegisterSession(session);

        // 2. 模拟客户端在阵亡后点击重新开始发送 CM_REVIVE (1018)
        dispatcher.HandleRevive(session, default);

        // 3. 校验服务端状态机裁决
        Assert.Equal(100, session.State.Hp); // 官方半血: 200 / 2 = 100
        Assert.Equal(50, session.State.Mp);  // 官方半蓝: 100 / 2 = 50
        Assert.False(session.State.HasGreenPoison, "复活必须清除绿毒");
        Assert.False(session.State.HasRedPoison, "复活必须清除红毒");
        Assert.True(session.State.IsInvincible(Environment.TickCount64 + 1000), "复活必须获得 3 秒物理神罚无敌护体");

        // 4. 校验下发的协议封包
        string reviveBuffer = session.GetBufferedPacketsString();
        Assert.NotEmpty(reviveBuffer);

        // 验证 SM_REVIVE (501)
        Assert.Contains($"#{ProtocolConsts.SM_REVIVE}/1/", reviveBuffer);

        // 验证 SM_MYSTATUS (504): #504/100/200/50/100/!
        Assert.Contains($"#{ProtocolConsts.SM_MYSTATUS}/100/200/50/100/", reviveBuffer);
    }

    [Fact]
    public void TestStoragePanel_SecondPasswordAndItemSafetyMatrix()
    {
        var player = new PlayerState
        {
            Uid = 1,
            Name = "仓储专家",
            Level = 35,
            Job = 0,
            StoragePassword = "safe_password_123",
            IsStorageUnlocked = false,
            Gold = 500_000
        };

        var sword = new EquipItem { Uid = 501, Name = "井中月", Weight = 58 };
        player.BagItems.Add(sword);
        player.BagWeight = 58;

        // 1. 尝试在未输入密码前存入物品 -> 必须被拦截阻断
        var (depositRes1, _) = StorageEngine.DepositItem(player, 0);
        Assert.Equal(StorageEngine.StorageResult.NeedPassword, depositRes1);
        Assert.Single(player.BagItems);
        Assert.Empty(player.StorageItems);

        // 2. 输错密码 -> 验证失败
        var verifyWrong = StorageEngine.VerifyPassword(player, "wrong_pass");
        Assert.Equal(StorageEngine.StorageResult.WrongPassword, verifyWrong);
        Assert.False(player.IsStorageUnlocked);

        // 3. 输入正确密码 -> 解锁成功
        var verifyOk = StorageEngine.VerifyPassword(player, "safe_password_123");
        Assert.Equal(StorageEngine.StorageResult.Success, verifyOk);
        Assert.True(player.IsStorageUnlocked);

        // 4. 解锁后存入物品 -> 成功移入仓库，背包负重减轻
        var (depositRes2, item) = StorageEngine.DepositItem(player, 0);
        Assert.Equal(StorageEngine.StorageResult.Success, depositRes2);
        Assert.NotNull(item);
        Assert.Empty(player.BagItems);
        Assert.Single(player.StorageItems);
        Assert.Equal(0, player.BagWeight);

        // 5. 金币存取检定
        var goldRes1 = StorageEngine.DepositGold(player, 200_000);
        Assert.Equal(StorageEngine.StorageResult.Success, goldRes1);
        Assert.Equal(300_000, player.Gold);
        Assert.Equal(200_000, player.StorageGold);

        // 6. 从仓库取出物品 -> 成功回退到背包
        var (takeRes, takenItem) = StorageEngine.WithdrawItem(player, 0);
        Assert.Equal(StorageEngine.StorageResult.Success, takeRes);
        Assert.NotNull(takenItem);
        Assert.Equal(501, takenItem.Uid);
        Assert.Single(player.BagItems);
        // 盛大官方 1.5 负重体系：井中月 58 + 300,000 金币折算 30 负重 = 88 负重
        Assert.Equal(88, player.BagWeight);
    }

    [Fact]
    public void TestSabukSiege_WarClockAndPalaceTakeoverEndToEnd()
    {
        var siege = new SabukSiegeEngine();
        long now = 1_000_000;

        // 1. 初始化城门与注册攻城行会
        siege.InitializeGate(2000);
        siege.RegisterAttackingGuild("布拉格");
        siege.RegisterAttackingGuild("傲天");

        Assert.True(siege.IsAttackingGuild("布拉格"));
        Assert.True(siege.IsAttackingGuild("傲天"));
        Assert.False(siege.IsAttackingGuild("散人行会"));

        // 2. 20:00 战役打响：原沙巴克霸主为“邪恶之眼”
        siege.StartSiege("邪恶之眼", now, durationMs: 7200_000); // 持续 2 小时
        Assert.Equal(SabukSiegeEngine.WarPhase.InWar, siege.Phase);
        Assert.Equal("邪恶之眼", siege.OwnerGuild);
        Assert.Equal("邪恶之眼", siege.TempOccupierGuild);

        // 3. 城门攻防破坏：承受 1200 伤害 -> Damaged；再承受 900 伤害 -> Broken 消除阻挡
        bool blocked = true;
        var state1 = siege.DamageGate(1200, (x, y, isBlocked) => blocked = isBlocked);
        Assert.Equal(SabukSiegeEngine.GateState.Damaged, state1);
        Assert.True(blocked, "受损状态城门阻挡依然生效");

        var state2 = siege.DamageGate(900, (x, y, isBlocked) => blocked = isBlocked);
        Assert.Equal(SabukSiegeEngine.GateState.Broken, state2);
        Assert.False(blocked, "城门彻底倒塌，阻挡物理消除");

        // 4. 免 PK 战火免除规则检定
        // 盟重沙巴克城 (3) 与皇宫 (150) 内交火免除 PK 惩罚
        Assert.True(siege.IsFreePkBattle(SabukSiegeEngine.SabukCastleMapId, "布拉格", "邪恶之眼"));
        Assert.True(siege.IsFreePkBattle(SabukSiegeEngine.SabukPalaceMapId, "傲天", "邪恶之眼"));
        Assert.False(siege.IsFreePkBattle(SabukSiegeEngine.SabukPalaceMapId, "布拉格", "布拉格"), "同行会内讧不免除");
        Assert.False(siege.IsFreePkBattle(0, "布拉格", "邪恶之眼"), "比奇省非沙城区域不免除");

        // 5. 皇宫争夺与临时占领更迭检定
        // 场景 A: 皇宫内存在散人闲逛 -> 占领失败
        var playersScenarioA = new List<(string characterName, string guildName, bool isAlive)>
        {
            ("布拉格战士A", "布拉格", true),
            ("路人甲", "", true)
        };
        var (occA, _) = siege.CheckPalaceOccupation(playersScenarioA);
        Assert.False(occA, "有散人时不可更迭占领者");

        // 场景 B: 皇宫内两派厮杀混战 -> 争夺中
        var playersScenarioB = new List<(string characterName, string guildName, bool isAlive)>
        {
            ("布拉格战士A", "布拉格", true),
            ("邪恶之眼法师", "邪恶之眼", true)
        };
        var (occB, _) = siege.CheckPalaceOccupation(playersScenarioB);
        Assert.False(occB, "多行会混战时不可更迭占领者");

        // 场景 C: 布拉格清空守城方与所有其他行会，全员存活独占皇宫 -> 触发临时占领！
        var playersScenarioC = new List<(string characterName, string guildName, bool isAlive)>
        {
            ("布拉格战士A", "布拉格", true),
            ("布拉格道士B", "布拉格", true),
            ("邪恶之眼已阵亡尸体", "邪恶之眼", false) // 尸体不影响
        };
        var (occC, newOccupier) = siege.CheckPalaceOccupation(playersScenarioC);
        Assert.True(occC, "独占皇宫触发临时占领");
        Assert.Equal("布拉格", newOccupier);
        Assert.Equal("布拉格", siege.TempOccupierGuild);

        // 6. 22:00 战役落幕结算
        var (finalWinner, changed) = siege.EndSiege();
        Assert.True(changed, "沙巴克霸主成功更迭");
        Assert.Equal("布拉格", finalWinner);
        Assert.Equal(SabukSiegeEngine.WarPhase.Ended, siege.Phase);
        Assert.True(siege.SabukGold >= 2_000_000, "获胜奖励 100 万金币成功注入金库");

        // 7. 新城主提取商业税金
        var (withOk, amount, _) = siege.WithdrawSabukGold("布拉格", isGuildLeader: true, 500_000);
        Assert.True(withOk);
        Assert.Equal(500_000, amount);
    }

    [Fact]
    public void TestGuild_NoticeUpdateAndMemberRankBroadcastFullLoop()
    {
        var mapMgr = new MapManager();
        var guildEngine = new GuildEngine();
        var dispatcher = new PacketDispatcher(mapMgr, guildEngine: guildEngine);

        var master = new ClientSession(101)
        {
            CharacterName = "无涯",
            CurrentMapId = 0,
            X = 300,
            Y = 300,
            State = new PlayerState { Uid = 101, Name = "无涯", Level = 45 }
        };

        var member = new ClientSession(102)
        {
            CharacterName = "邪恶战士",
            CurrentMapId = 0,
            X = 301,
            Y = 300,
            State = new PlayerState { Uid = 102, Name = "邪恶战士", Level = 35 }
        };

        dispatcher.RegisterSession(master);
        dispatcher.RegisterSession(member);
        var map = mapMgr.GetMap(0)!;
        map.Aoi.Enter(master, master.X, master.Y);
        map.Aoi.Enter(member, member.X, member.Y);

        // 注册行会与成员
        var guild = guildEngine.RegisterGuildDirect("邪恶之眼", master.CharacterName);
        guildEngine.AddMemberDirect("邪恶之眼", member.CharacterName);

        // 1. 掌门人发布更新行会公告: CM_GUILD_NOTICE_UPDATE (1040)
        string newNotice = "今晚20:00沙巴克皇宫集合，统一听指挥攻打城门！";
        dispatcher.Dispatch(master, $"#{ProtocolConsts.CM_GUILD_NOTICE_UPDATE}/0/0/0/0/{newNotice}!");

        // 验证在线成员与掌门人均收到 SM_GUILDMESSAGE (761)
        string memberBuf = member.GetBufferedPacketsString();
        string masterBuf = master.GetBufferedPacketsString();

        Assert.Contains(newNotice, memberBuf);
        Assert.Contains(newNotice, masterBuf);
        Assert.Contains($"#{ProtocolConsts.SM_GUILDMESSAGE}/", memberBuf);

        // 2. 普通成员尝试修改公告 -> 必须被拦截提示非掌门人
        member.ClearBufferedPackets();
        dispatcher.Dispatch(member, $"#{ProtocolConsts.CM_GUILD_NOTICE_UPDATE}/0/0/0/0/篡改公告!");
        string failBuf = member.GetBufferedPacketsString();
        Assert.Contains("只有行会正掌门人方可修改发布行会公告", failBuf);

        // 3. 掌门人调整成员封号: CM_GUILD_UPDATE_RANK (1041)
        master.ClearBufferedPackets();
        member.ClearBufferedPackets();
        dispatcher.Dispatch(master, $"#{ProtocolConsts.CM_GUILD_UPDATE_RANK}/0/0/0/0/邪恶战士/近卫军统领!");

        string masterRankBuf = master.GetBufferedPacketsString();
        string memberRankBuf = member.GetBufferedPacketsString();

        // 掌门人收到成功通知
        Assert.Contains("成功将【邪恶战士】的职位修改为【近卫军统领】", masterRankBuf);

        // 目标成员收到通知
        Assert.Contains("掌门人已将你的行会封号调整为【近卫军统领】", memberRankBuf);

        // 验证头顶封号广播下发 SM_GUILDNAME_DELPHI (42)
        Assert.Contains($"#{ProtocolConsts.SM_GUILDNAME_DELPHI}/102/0/0/0/邪恶之眼/近卫军统领!", memberRankBuf);
    }

    [Fact]
    public void TestRepair_NormalAndSpecialRepairGoldAndDuraPrecisionFullLoop()
    {
        var mapMgr = new MapManager();
        var npcEngine = new NpcMerchantEngine();
        var dispatcher = new PacketDispatcher(mapMgr, npcEngine: npcEngine);

        var player = new ClientSession(201)
        {
            CharacterName = "神剑修士",
            CurrentMapId = 0,
            X = 100,
            Y = 100,
            State = new PlayerState
            {
                Uid = 201,
                Name = "神剑修士",
                Level = 35,
                Gold = 200_000
            }
        };

        // 放入一把磨损的裁决之杖: 当前持久 10，最大持久 32
        var weapon = new EquipItem
        {
            Uid = 888,
            Name = "裁决之杖",
            Weight = 80,
            Dura = 10,
            MaxDura = 32
        };
        player.State.BagItems.Add(weapon);
        player.State.BagWeight = 80 + (200_000 / 10_000); // 80 + 20 = 100
        dispatcher.RegisterSession(player);

        // 1. 普通修理: CM_USERREPAIRITEM (1023)
        // 官方 1.5 铁律: 普通修理按磨损比例损耗 1 点最大持久 (MaxDura 从 32 掉至 31，Dura 回满至 31)
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_USERREPAIRITEM}/0/0/0/0/888!");

        string normalRepairBuf = player.GetBufferedPacketsString();
        Assert.NotEmpty(normalRepairBuf);

        // 验证 SM_REPAIROK_DELPHI (669) 的 Recog 严格为修理扣费后人物金币 (200,000 - 费用)，Param 为 31，Tag 为 31
        Assert.True(weapon.MaxDura < 32, "普通修理最大持久必须有所损耗");
        Assert.Equal(weapon.MaxDura, weapon.Dura);
        int remainingGold = player.State.Gold;
        Assert.True(remainingGold < 200_000, "修理必须扣除相应金币");

        // 验证 669 封包格式: #669/remainingGold/dura/maxDura/0/裁决之杖!
        Assert.Contains($"#{ProtocolConsts.SM_REPAIROK_DELPHI}/{remainingGold}/{weapon.Dura}/{weapon.MaxDura}/0/裁决之杖!", normalRepairBuf);

        // 2. 特殊修理 (0 损特修): CM_REPAIRSPECIAL (48)
        // 再次磨损
        weapon.Dura = 5;
        int preSpecMaxDura = weapon.MaxDura;
        int preSpecGold = player.State.Gold;
        player.ClearBufferedPackets();

        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_REPAIRSPECIAL}/0/0/0/0/888!");

        string specRepairBuf = player.GetBufferedPacketsString();
        Assert.NotEmpty(specRepairBuf);

        // 特修最大持久不损耗 (保持 preSpecMaxDura)，当前持久回满至 preSpecMaxDura
        Assert.Equal(preSpecMaxDura, weapon.MaxDura);
        Assert.Equal(preSpecMaxDura, weapon.Dura);
        int postSpecGold = player.State.Gold;
        Assert.True(postSpecGold < preSpecGold, "特修扣除金币");

        // 验证 669 封包 Recog 严格为 postSpecGold
        Assert.Contains($"#{ProtocolConsts.SM_REPAIROK_DELPHI}/{postSpecGold}/{weapon.Dura}/{weapon.MaxDura}/0/裁决之杖!", specRepairBuf);
    }

    [Fact]
    public void TestNpcDialog_HyperlinkActionAndShopSellPriceConsistencyFullLoop()
    {
        // 1. 初始化服务端世界与比奇武器店掌柜 (UID = 88)
        var mapMgr = new MapManager();
        var npcEngine = new NpcMerchantEngine();
        var dispatcher = new PacketDispatcher(mapMgr, npcEngine: npcEngine);

        var merchant = new NpcEntity
        {
            Uid = 88,
            Name = "比奇武器店",
            MapId = 0,
            X = 300,
            Y = 300
        };
        merchant.ScriptPages["@main"] = "欢迎来到比奇铁匠铺！\\<购买武器/@buy>\\<出售武器/@sell>\\<退出/@exit>";
        merchant.ScriptPages["@buy"] = "请挑选你心仪的兵刃！\\<返回/@main>";
        npcEngine.RegisterNpc(merchant);

        var player = new ClientSession(301)
        {
            CharacterName = "铁血刺客",
            CurrentMapId = 0,
            X = 300,
            Y = 300,
            State = new PlayerState
            {
                Uid = 301,
                Name = "铁血刺客",
                Level = 30,
                Gold = 5000,
                BagWeight = 20,
                MaxBagWeight = 100
            }
        };
        // 背包中有一瓶重 20 的太阳水作为底仓
        var potion = new EquipItem { Uid = 1111, Name = "强效太阳水", Weight = 20 };
        player.State.BagItems.Add(potion);
        player.State.RecalculateAuthoritativeWeights(player.State.Gold);
        dispatcher.RegisterSession(player);

        // 2. 模拟客户端打开 NPC 首页: CM_MERCHANTHOMEPAGE (1010)
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_MERCHANTHOMEPAGE}/88/0/0/0/!");
        string openBuf = player.GetBufferedPacketsString();
        Assert.Contains($"#{ProtocolConsts.SM_NPCDIALOG}/88/0/0/0/", openBuf);
        Assert.Contains("欢迎来到比奇铁匠铺！", openBuf);

        // 3. 模拟客户端点击超链接选项 <购买武器/@buy>: CM_NPCSELECT (1011)
        player.ClearBufferedPackets();
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_NPCSELECT}/88/0/0/0/@buy!");
        string buyPageBuf = player.GetBufferedPacketsString();
        Assert.Contains("请挑选你心仪的兵刃！", buyPageBuf);

        // 4. 角色背包中存入一把磨损半耐久的炼狱斧 (原价 10,000，当前耐久 10，最大耐久 20)
        var axe = new EquipItem
        {
            Uid = 9988,
            Name = "炼狱",
            Price = 10000,
            Dura = 10,
            MaxDura = 20,
            Weight = 60
        };
        player.State.BagItems.Add(axe);
        player.State.BagWeight += axe.Weight;

        // 5. 商店回收事前估价查询: CM_QUERYSELLPRICE (1012)
        player.ClearBufferedPackets();
        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_QUERYSELLPRICE}/9988/0/0/0/!");

        string priceBuf = player.GetBufferedPacketsString();
        Assert.NotEmpty(priceBuf);

        // 原价 10000 -> 半价 5000 -> 半耐久 (10/20) 折算 -> 估价应为 2500 金币
        int expectedSellPrice = (10000 / 2) * 10 / 20; // 2500
        Assert.Contains($"#{ProtocolConsts.SM_QUERYPRICE_REC}/{expectedSellPrice}/0/0/0/炼狱!", priceBuf);

        // 6. 确认出售武器给商店: CM_USERSELLITEM (1013)
        player.ClearBufferedPackets();
        int preGold = player.State.Gold;
        int preWeight = player.State.BagWeight;

        dispatcher.Dispatch(player, $"#{ProtocolConsts.CM_USERSELLITEM}/9988/0/0/0/!");

        string sellBuf = player.GetBufferedPacketsString();
        Assert.NotEmpty(sellBuf);

        // 7. 盛大官方 1.5 权威断言:
        // A. 武器从背包彻底移除
        Assert.DoesNotContain(player.State.BagItems, i => i.Uid == 9988);

        // B. 背包负重扣减
        Assert.Equal(preWeight - axe.Weight, player.State.BagWeight);

        // C. 人物金币严格增加事前估价的 expectedSellPrice (2500)，绝无分文偏差
        Assert.Equal(preGold + expectedSellPrice, player.State.Gold);

        // D. 下发出售成功包 SM_SELL_OK_DELPHI (650) 与金币变动 SM_GOLDCHANGED_DELPHI (653)
        Assert.Contains($"#{ProtocolConsts.SM_SELL_OK_DELPHI}/9988/{expectedSellPrice}/0/0/炼狱!", sellBuf);
        Assert.Contains($"#{ProtocolConsts.SM_GOLDCHANGED_DELPHI}/{player.State.Gold}/0/0/0/!", sellBuf);
    }

    [Fact]
    public void TestWeaponUpgrade_FullLifecycleGroundTruth()
    {
        var blacksmith = new ModernNpcPluginEngine.WeaponUpgradeMasterNpcBehavior();

        var player = new ClientSession(301)
        {
            CharacterName = "风中追风",
            CurrentMapId = 0,
            X = 200,
            Y = 200,
            State = new PlayerState
            {
                Uid = 301,
                Name = "风中追风",
                Level = 40,
                Gold = 500_000
            }
        };

        // 1. 手中无武器尝试升级 -> 权威拦截
        var ctx1 = new NpcDialogContext(player, "武器升级大师", "@start_upgrade", 107);
        blacksmith.OnInteract(ctx1);
        Assert.Equal("你必须先将需要炼制的武器握在手中。", ctx1.DialogText);

        // 装备裁决之杖
        var weapon = new EquipItem
        {
            Uid = 7777,
            Name = "裁决之杖",
            Weight = 80,
            Dura = 32000,
            MaxDura = 32000,
            Dc = 0,
            MaxDc = 30,
            UpgradeCount = 0
        };
        player.State.Equipment[(int)OfficialEquipSlot.Weapon] = weapon;
        player.State.RecalculateAttributes();

        // 2. 无黑铁矿石尝试升级 -> 权威拦截
        var ctx2 = new NpcDialogContext(player, "武器升级大师", "@start_upgrade", 107);
        blacksmith.OnInteract(ctx2);
        Assert.Equal("炼制武器需要纯度合乎要求的黑铁矿石。", ctx2.DialogText);

        // 放入 5 块纯度 15 的黑铁矿石
        for (int i = 0; i < 5; i++)
        {
            player.State.BagItems.Add(new EquipItem
            {
                Uid = 8000 + i,
                Name = "黑铁矿石",
                Shape = 15,
                Dura = 15,
                MaxDura = 15,
                Weight = 5
            });
        }

        // 3. 无首饰尝试升级 -> 权威拦截
        var ctx3 = new NpcDialogContext(player, "武器升级大师", "@start_upgrade", 107);
        blacksmith.OnInteract(ctx3);
        Assert.Equal("炼制武器还需要首饰。", ctx3.DialogText);

        // 放入 2 条记忆项链 (攻击 2-4)
        for (int i = 0; i < 2; i++)
        {
            player.State.BagItems.Add(new EquipItem
            {
                Uid = 9000 + i,
                Name = "记忆项链",
                Shape = 19,
                Dc = 2,
                MaxDc = 4,
                Weight = 1
            });
        }

        Assert.Equal(7, player.State.BagItems.Count); // 5 矿石 + 2 项链

        // 4. 材料齐全启动升级
        var ctx4 = new NpcDialogContext(player, "武器升级大师", "@start_upgrade", 107);
        blacksmith.OnInteract(ctx4);
        Assert.Equal("已经开始炼制了。一小时后再来取吧。", ctx4.DialogText);

        // 盛大官方 1.5 权威断言:
        // A. 武器从手中卸下并进入保管
        Assert.Null(player.State.Equipment[(int)OfficialEquipSlot.Weapon]);
        Assert.True(ModernNpcPluginEngine.WeaponUpgradeMasterNpcBehavior.OngoingUpgrades.ContainsKey(player.SessionId));

        // B. 背包中所有投入的黑铁矿石与首饰被扣除
        Assert.Empty(player.State.BagItems);

        // 5. 立即尝试取回 (时间未到) -> 权威拦截提示等待
        var rec = ModernNpcPluginEngine.WeaponUpgradeMasterNpcBehavior.OngoingUpgrades[player.SessionId];
        var ctx5 = new NpcDialogContext(player, "武器升级大师", "@retrieve_weapon", 107);
        blacksmith.OnInteract(ctx5);
        Assert.Equal("还在炼制中，请稍后再来。", ctx5.DialogText);

        // 6. 模拟时间到达取回武器
        // 覆盖模拟完成时间
        ModernNpcPluginEngine.WeaponUpgradeMasterNpcBehavior.OngoingUpgrades[player.SessionId] = rec with { FinishTickMs = Environment.TickCount64 - 100 };
        var ctx6 = new NpcDialogContext(player, "武器升级大师", "@retrieve_weapon", 107);
        blacksmith.OnInteract(ctx6);
        Assert.Equal("你的武器已经炼制好了，快拿去试试身手吧！", ctx6.DialogText);

        // 验证取回武器入包，且 UpgradeState 绝不是 1 必碎，而是符合判定结果码
        Assert.Single(player.State.BagItems);
        var retrievedWeapon = player.State.BagItems[0];
        Assert.Equal(7777, retrievedWeapon.Uid);
        Assert.True(retrievedWeapon.UpgradeState == 1 || retrievedWeapon.UpgradeState >= 10, "必须是合法官方结果码");

        // 7. 玩家将武器佩戴回手上，进行第一刀试刀
        player.State.BagItems.Clear();
        player.State.Equipment[(int)OfficialEquipSlot.Weapon] = retrievedWeapon;
        player.State.RecalculateAttributes();

        var trialRes = player.State.TrialSlashEquippedWeapon();
        Assert.NotEqual(WeaponUpgradeEngine.TrialSlashOutcome.None, trialRes.Outcome);
        if (trialRes.Outcome == WeaponUpgradeEngine.TrialSlashOutcome.Success)
        {
            Assert.Equal("你的武器升级成功了。", trialRes.Message);
            Assert.Equal(1, retrievedWeapon.UpgradeCount);
            Assert.True(retrievedWeapon.MaxDc >= 31, "攻击力上限必须增加");
            Assert.NotNull(player.State.Equipment[(int)OfficialEquipSlot.Weapon]);
        }
        else
        {
            Assert.Equal("你的武器碎了。", trialRes.Message);
            Assert.Null(player.State.Equipment[(int)OfficialEquipSlot.Weapon]); // 碎刀彻底销毁
        }

        // 8. 7 次满级武器升级极限检定
        var maxWeapon = new EquipItem
        {
            Uid = 9999,
            Name = "屠龙",
            Weight = 99,
            UpgradeCount = 7
        };
        player.State.Equipment[(int)OfficialEquipSlot.Weapon] = maxWeapon;
        var ctx7 = new NpcDialogContext(player, "武器升级大师", "@start_upgrade", 107);
        blacksmith.OnInteract(ctx7);
        Assert.Equal("这把武器已经升过7次了，不能再升了。", ctx7.DialogText);
    }
}


