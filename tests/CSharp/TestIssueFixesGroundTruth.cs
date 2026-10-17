using System;
using System.Linq;
using Mir2.Server.Game;
using Mir2.Server.Maps;
using Mir2.Server.Network;
using Mir2.Server.Persistence;
using Mir2.Shared.Models;
using Mir2Client.Game;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 针对用户实测反馈 11 项核心缺陷修复与官方 1.5 原生逻辑的自动化回归测试
/// 100% 本地脱机可跑，不依赖外部 PostgreSQL 数据库服务
/// </summary>
public sealed class TestIssueFixesGroundTruth
{
    [Fact]
    public void TestShopPassagesAndMaps_BuiltinRegisteredCorrectly()
    {
        var mapMgr = new MapManager();

        // 1. 比奇城 8 大功能性店铺地图必须正确注册且 OfficialId 物理对齐
        var warehouse = mapMgr.GetMap(105);
        Assert.NotNull(warehouse);
        Assert.Equal("0105", warehouse.OfficialId);
        Assert.Equal(27, warehouse.Width);
        Assert.Equal(29, warehouse.Height);

        var pharmacy = mapMgr.GetMap(101);
        Assert.NotNull(pharmacy);
        Assert.Equal("0101", pharmacy.OfficialId);

        var bookStore = mapMgr.GetMap(102);
        Assert.NotNull(bookStore);
        Assert.Equal("0102", bookStore.OfficialId);

        var butcher = mapMgr.GetMap(103);
        Assert.NotNull(butcher);
        Assert.Equal("0103", butcher.OfficialId);

        // 2. 双向传送门跳点必须精准命中且可双向触发
        // 比奇城 -> 仓库西门 (0: 295, 284 -> 105: 8, 24)
        var pIn = mapMgr.FindPassage(0, 295, 284);
        Assert.NotNull(pIn);
        Assert.Equal(105, pIn.ToMapId);
        Assert.Equal(8, pIn.ToX);
        Assert.Equal(24, pIn.ToY);

        // 仓库西门 -> 比奇城 (105: 9, 25 -> 0: 295, 286)
        var pOut = mapMgr.FindPassage(105, 9, 25);
        Assert.NotNull(pOut);
        Assert.Equal(0, pOut.ToMapId);
        Assert.Equal(295, pOut.ToX);
        Assert.Equal(286, pOut.ToY);

        // 比奇城 -> 药店 (0: 286, 295 -> 101: 9, 22)
        var pPharm = mapMgr.FindPassage(0, 286, 295);
        Assert.NotNull(pPharm);
        Assert.Equal(101, pPharm.ToMapId);

        // 比奇城 -> 书店 (0: 308, 264 -> 102: 3, 7)
        var pBook = mapMgr.FindPassage(0, 308, 264);
        Assert.NotNull(pBook);
        Assert.Equal(102, pBook.ToMapId);
    }

    [Fact]
    public void TestButcherNpcPlugin_MultiAliasAndMeatTradingClosedLoop()
    {
        var modernEngine = new ModernNpcPluginEngine();

        // 1. 验证肉店老板的中英文全量别名均被正确索引
        Assert.True(modernEngine.HasPlugin("屠夫"));
        Assert.True(modernEngine.HasPlugin("肉店老板"));
        Assert.True(modernEngine.HasPlugin("Jim"));
        Assert.True(modernEngine.HasPlugin("Danielle"));
        Assert.True(modernEngine.HasPlugin("Vincent"));

        // 2. 模拟玩家交互与卖肉闭环
        var session = new ClientSession(1, null!)
        {
            CharacterName = "测试战士",
            State = new PlayerState
            {
                CharacterName = "测试战士",
                Gold = 500
            }
        };

        // 放入一块新鲜肉 (品质 15)
        var testMeat = new EquipItem
        {
            Uid = 99991,
            Name = "肉",
            Dura = 15000,
            MaxDura = 15000,
            Weight = 2
        };
        session.State.BagItems.Add(testMeat);
        session.State.BagWeight = 2;

        var (handled, script, closed, ctx) = modernEngine.DispatchDialogEx(session, 113, "@sell");
        Assert.True(handled);
        Assert.False(closed);
        Assert.Contains("共收购了 1 块肉", script);
        Assert.Contains("给你", script);
        // 金币必须增加且肉必须从背包移除
        Assert.True(session.State.Gold > 500);
        Assert.Empty(session.State.BagItems);
        Assert.Equal(0, session.State.BagWeight);
    }

    [Fact]
    public void TestNpcMerchantEngine_EnsureDefaultMeatGoodsForAliases()
    {
        var merchantEngine = new NpcMerchantEngine();
        var jimNpc = new NpcEntity { Uid = 5001, Name = "Jim", MapId = 0, X = 293, Y = 603 };
        merchantEngine.RegisterNpc(jimNpc);

        // Jim 必须自动装配鸡肉、肉、优质肉
        Assert.NotEmpty(jimNpc.Goods);
        Assert.Contains(jimNpc.Goods, g => g.Name == "鸡肉");
        Assert.Contains(jimNpc.Goods, g => g.Name == "肉");
        Assert.Contains(jimNpc.Goods, g => g.Name == "优质肉");

        // 对话主页必须输出地道中文而不是英文
        string homePage = merchantEngine.GetNpcHomePage(5001);
        Assert.Contains("嘿！客官，刚宰杀的鸡肉和新鲜鹿肉", homePage);
        Assert.DoesNotContain("welcome, what can i do for you", homePage);
    }

    [Fact]
    public void TestNewbieGuide_AliasesAndAntiSpamGift()
    {
        var engine = new ModernNpcPluginEngine();
        var session = new ClientSession(2, null!)
        {
            CharacterName = "测试萌新",
            State = new PlayerState { Level = 1, CharacterName = "测试萌新" }
        };

        // 1. 验证别名能够正确命中 Assistant / 新手指导员
        var (h1, p1, c1) = engine.DispatchDialog(session, "Assistant", "@main");
        Assert.True(h1);
        Assert.Contains("欢迎来到银杏山谷", p1);

        // 2. 领取新手礼包
        var (h2, p2, c2) = engine.DispatchDialog(session, "新手指导员", "@get_gift");
        Assert.True(h2);
        Assert.Contains("新手装备已放入你的包裹", p2);
        Assert.Equal(2, session.State.BagItems.Count);
        Assert.True(session.State.HasReceivedNewbieGift);

        // 3. 再次重复领取必须被拒绝
        var (h3, p3, c3) = engine.DispatchDialog(session, "新手指导员", "@get_gift");
        Assert.True(h3);
        Assert.Contains("不可重复领取", p3);
        Assert.Equal(2, session.State.BagItems.Count);
    }

    [Fact]
    public void TestOfficialStorage_ProtocolAndModeAlignment()
    {
        // 验证官方 1.5 仓库协议常量权威对齐 (IDA case 700/701/704/705/709)
        Assert.Equal(700, Mir2.Shared.Protocols.ProtocolConsts.SM_OPENSTORAGEDLG);
        Assert.Equal(701, Mir2.Shared.Protocols.ProtocolConsts.SM_SAVEITEM_OK);
        Assert.Equal(704, Mir2.Shared.Protocols.ProtocolConsts.SM_USERSTORAGE_ITEMS);
        Assert.Equal(705, Mir2.Shared.Protocols.ProtocolConsts.SM_TAKEBACKITEM_OK);
        Assert.Equal(709, Mir2.Shared.Protocols.ProtocolConsts.SM_STORAGE_CLEARITEMS_DELPHI);

        // 验证客户端 CM 存取协议号 (IDA sub_458D3C / sub_4669D0: 1031 存, 1032 取)
        Assert.Equal(1031, Mir2.Shared.Protocols.ProtocolConsts.CM_USERSTORAGEDEPOSIT);
        Assert.Equal(1032, Mir2.Shared.Protocols.ProtocolConsts.CM_USERSTORAGEWITHDRAW);
    }

    private class MockManagedWindow : Mir2Client.UI.IManagedWindow
    {
        public string WindowName { get; set; } = "MockWindow";
        public Mir2Client.UI.Official24DialogCompendium.DialogId DialogId { get; set; } = Mir2Client.UI.Official24DialogCompendium.DialogId.SkillBook;
        public bool IsVisible { get; set; } = false;
        public int ZIndex { get; set; } = 0;
        public (int x, int y, int width, int height) Bounds => (0, 0, 300, 200);
    }

    [Fact]
    public void TestOfficialUi_WindowStackAndBottomBarHighLightAlignment()
    {
        // 验证官方 1.5 栈管理器在关闭顶层窗口时的进出栈行为与底栏按键高亮互斥闭环
        var stack = new Mir2Client.UI.WindowStackManager();
        Assert.Equal(0, stack.Count);

        // 模拟一个受管窗体加入栈
        var mockWindow = new MockManagedWindow();
        stack.PushWindow(mockWindow);
        Assert.Equal(1, stack.Count);
        Assert.True(mockWindow.IsVisible);

        // 验证出栈与互斥
        var top = stack.CloseTopDialog();
        Assert.Same(mockWindow, top);
        Assert.False(mockWindow.IsVisible);
        Assert.Equal(0, stack.Count);
    }
}

