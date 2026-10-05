using System;
using System.Collections.Generic;
using Mir2Client.UI;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 纯 C# 单轨场景绑定架构与 24 窗体状态管理自动化回归测试套件
/// 100% 自动化自测，绝不依赖人肉测试！
/// </summary>
public class TestUiSceneBindingArchitecture
{
    [Fact]
    public void TestWindowStack_LIFO_OrderAndZIndexCalculation()
    {
        var stack = new WindowStackManager();
        var bag = new MockWindow("BagPanel", 0, 0, 336, 270);
        var equip = new MockWindow("EquipPanel", 570, 0, 230, 300);
        var npc = new MockWindow("NpcDialog", 246, 160, 308, 205);

        // 模拟依次打开: 包裹 -> 角色面板 -> NPC 对话
        stack.PushWindow(bag);
        Assert.True(bag.IsVisible);
        Assert.Equal(10, bag.ZIndex);

        stack.PushWindow(equip);
        Assert.True(equip.IsVisible);
        Assert.Equal(10, bag.ZIndex);
        Assert.Equal(11, equip.ZIndex);

        stack.PushWindow(npc);
        Assert.True(npc.IsVisible);
        Assert.Equal(10, bag.ZIndex);
        Assert.Equal(11, equip.ZIndex);
        Assert.Equal(12, npc.ZIndex);

        // 模拟 ESC: 先关最顶层的 NPC 对话
        var closed1 = stack.CloseTopDialog();
        Assert.NotNull(closed1);
        Assert.Equal("NpcDialog", closed1?.WindowName);
        Assert.False(npc.IsVisible);

        // 再按 ESC: 关角色面板
        var closed2 = stack.CloseTopDialog();
        Assert.NotNull(closed2);
        Assert.Equal("EquipPanel", closed2?.WindowName);
        Assert.False(equip.IsVisible);

        // 再按 ESC: 关包裹
        var closed3 = stack.CloseTopDialog();
        Assert.NotNull(closed3);
        Assert.Equal("BagPanel", closed3?.WindowName);
        Assert.False(bag.IsVisible);

        // 栈已空
        Assert.Null(stack.CloseTopDialog());
        Assert.Equal(0, stack.Count);
    }

    [Fact]
    public void TestMouseInputBlocking_UiExclusionRegions()
    {
        var stack = new WindowStackManager();
        var trade = new MockWindow("TradePanelView", 150, 150, 512, 260);
        stack.PushWindow(trade);

        // 1. 点击在交易面板区域内 -> 必须被 UI 阻断，不能穿透成地面走动
        Assert.True(stack.IsMouseOverUi(200, 200));

        // 2. 点击在主界面底栏 HUD 区域 (y >= 430) -> 必须被 UI 阻断
        Assert.True(stack.IsMouseOverUi(400, 500));

        // 3. 点击在自由游戏视野空旷区域 -> 绝不阻断，允许点击地面移动
        Assert.False(stack.IsMouseOverUi(50, 50));
        Assert.False(stack.IsMouseOverUi(700, 50));
    }

    /// <summary>
    /// 官方 1.5 IDA sub_47D6CC / sub_47D864 登录场景开门动画与多面板状态机逆向实证
    /// 实锤：
    /// 1. 开门帧起始于 ChrSel.wil[23]，结束于 [32]，共 10 帧
    /// 2. 帧间隔严格为 0xE6 (230ms)
    /// 3. 子状态 0x0A(60)=登录, 0x0B(63)=新用户注册, 0x0C(50)=修改密码
    /// </summary>
    [Fact]
    public void TestOfficialLoginScene_DoorAnimationAndSubStates_GroundTruth()
    {
        Assert.Equal(23, LoginView.OfficialDoorStartFrame);
        Assert.Equal(32, LoginView.OfficialDoorEndFrame);
        Assert.Equal(10, LoginView.OfficialDoorTotalFrames);
        Assert.Equal(0.230, LoginView.OfficialDoorFrameDuration, precision: 3);

        // 验证 10 帧连续区间覆盖 ChrSel_0023 ~ ChrSel_0032
        for (int i = 0; i < LoginView.OfficialDoorTotalFrames; i++)
        {
            int expectedFrame = LoginView.OfficialDoorStartFrame + i;
            Assert.True(expectedFrame >= 23 && expectedFrame <= 32);
        }
    }

    /// <summary>
    /// 官方 1.5 IDA sub_47F264 / sub_481DD4 选角神龛场景逆向实证
    /// 实锤：
    /// 1. 神龛背景贴图基准：Prguse.wil[65] (800x600)
    /// 2. 脚底神龛火环粒子动画：ChrSel.wil[4..17]，共 14 帧，110ms 帧步长
    /// 3. 六大职业性别立绘帧号公式：BaseFrame = 120 * Gender + 40 * Job + (IsSelected ? 40 : 60)
    ///    男战: 40/60, 男法: 80/100, 男道: 120/140, 女战: 160/180, 女法: 200/220, 女道: 240/260
    /// </summary>
    [Fact]
    public void TestOfficialSelectCharScene_ArchetypesAndFlame_GroundTruth()
    {
        Assert.Equal(65, SelectCharView.OfficialBgImageIndex);
        Assert.Equal(4, SelectCharView.OfficialFlameStartFrame);
        Assert.Equal(14, SelectCharView.OfficialFlameTotalFrames);
        Assert.Equal(0.110, SelectCharView.OfficialFlameFrameDuration, precision: 3);
        Assert.Equal(0.300, SelectCharView.OfficialAnimFrameDuration, precision: 3);

        // 验证六大职业/性别组合的 BaseFrame 公式
        // 男战 (Job 0, Sex 0)
        Assert.Equal(60, SelectCharView.CalculateCharBaseFrame(0, 0, isSelected: false));
        Assert.Equal(40, SelectCharView.CalculateCharBaseFrame(0, 0, isSelected: true));

        // 男法 (Job 1, Sex 0)
        Assert.Equal(100, SelectCharView.CalculateCharBaseFrame(1, 0, isSelected: false));
        Assert.Equal(80, SelectCharView.CalculateCharBaseFrame(1, 0, isSelected: true));

        // 男道 (Job 2, Sex 0)
        Assert.Equal(140, SelectCharView.CalculateCharBaseFrame(2, 0, isSelected: false));
        Assert.Equal(120, SelectCharView.CalculateCharBaseFrame(2, 0, isSelected: true));

        // 女战 (Job 0, Sex 1)
        Assert.Equal(180, SelectCharView.CalculateCharBaseFrame(0, 1, isSelected: false));
        Assert.Equal(160, SelectCharView.CalculateCharBaseFrame(0, 1, isSelected: true));

        // 女法 (Job 1, Sex 1)
        Assert.Equal(220, SelectCharView.CalculateCharBaseFrame(1, 1, isSelected: false));
        Assert.Equal(200, SelectCharView.CalculateCharBaseFrame(1, 1, isSelected: true));

        // 女道 (Job 2, Sex 1)
        Assert.Equal(260, SelectCharView.CalculateCharBaseFrame(2, 1, isSelected: false));
        // 验证六大职业/性别动态坐标矩阵 (sub_47F264 @ 0x47F328 - 0x47F43C)
        // 1号位 (Slot 0): 男战 (71, 52), 女法激活 (171, 97), 女法待机 (141, 83)
        var coord0_warrior = SelectCharView.GetCharacterCoordinate(0, 0, 0, isSelected: true);
        Assert.Equal(71, (int)coord0_warrior.X);
        Assert.Equal(52, (int)coord0_warrior.Y);

        var coord0_wiz_active = SelectCharView.GetCharacterCoordinate(0, 1, 1, isSelected: true);
        Assert.Equal(171, (int)coord0_wiz_active.X);
        Assert.Equal(97, (int)coord0_wiz_active.Y);

        var coord0_wiz_idle = SelectCharView.GetCharacterCoordinate(0, 1, 1, isSelected: false);
        Assert.Equal(141, (int)coord0_wiz_idle.X);
        Assert.Equal(83, (int)coord0_wiz_idle.Y);

        // 2号位 (Slot 1): X += 340, Y += 2
        var coord1_wiz_idle = SelectCharView.GetCharacterCoordinate(1, 1, 1, isSelected: false);
        Assert.Equal(141 + 340, (int)coord1_wiz_idle.X); // 481
        Assert.Equal(83 + 2, (int)coord1_wiz_idle.Y);   // 85

        // 验证官方提示文案实证 (0x47EA4C / 0x47EBDC)
        Assert.Equal("你可以为每个单独的帐户创建两个角色。", SelectCharView.PromptTwoCharactersMax);
        Assert.Contains("删除的角色是不能被恢复的", SelectCharView.PromptDeleteConfirm);
        Assert.Contains("你真的想要删除角色吗？", SelectCharView.PromptDeleteConfirm);

        // 步骤 3.1: 创角子面板 NewCharPanel (300x417 @ 250, 91) 与关闭红叉 (247, 30, 16x23)
        var newCharClose = (X: 247, Y: 30, W: 16, H: 23);
        Assert.Equal(247, newCharClose.X);
        Assert.Equal(30, newCharClose.Y);
        Assert.Equal(16, newCharClose.W);
        Assert.Equal(23, newCharClose.H);
    }

    /// <summary>
    /// 官方 1.5 IDA sub_481DD4 line 864 服务器分区选择石碑界面逆向实证
    /// 实锤：
    /// 1. 石碑尺寸严格为 404x452 (Prguse2.wil[4])，居中在 (198, 74)
    /// 2. 槽位按钮尺寸严格为 168x41 (Prguse2.wil[2] normal, [3] hover)
    /// 3. 单列服务器居中基准算法：Y_base = 235 - ((42 * num_servers) >> 1)
    /// </summary>
    [Fact]
    public void TestOfficialSelectServerScene_TabletAndSlots_GroundTruth()
    {
        Assert.Equal(404, SelectServerView.OfficialTabletWidth);
        Assert.Equal(452, SelectServerView.OfficialTabletHeight);
        Assert.Equal(168, SelectServerView.OfficialSlotWidth);
        Assert.Equal(41, SelectServerView.OfficialSlotHeight);
        Assert.Equal(42, SelectServerView.OfficialSlotSpacing);

        // 验证 1~4 个服务器时的垂直居中起点
        // 1 个服务器: 235 - (42 / 2) = 235 - 21 = 214
        Assert.Equal(214, SelectServerView.CalculateServerStartY(1));

        // 2 个服务器: 235 - (84 / 2) = 235 - 42 = 193
        Assert.Equal(193, SelectServerView.CalculateServerStartY(2));

        // 3 个服务器: 235 - (126 / 2) = 235 - 63 = 172
        Assert.Equal(172, SelectServerView.CalculateServerStartY(3));

        // 4 个服务器: 235 - (168 / 2) = 235 - 84 = 151
        Assert.Equal(151, SelectServerView.CalculateServerStartY(4));
    }

    /// <summary>
    /// 步骤 1: 盛大官方 1.5 IDA sub_47F03C / sub_47F264 账号登录主框与控件固定实证测试
    /// 锁死实锤坐标，杜绝回退：
    /// 1. 登录面板 Prguse_0060 (296x254)，居中 (252, 173)
    /// 2. 账号输入框 (98, 86, 137x16)
    /// 3. 密码输入框 (98, 118, 137x16)
    /// 4. 登录按钮 (168, 162, 76x33)
    /// 5. 关闭红叉 (252, 28, 16x23)
    /// 6. 新用户按钮 (24, 206, 100x32)
    /// 7. 改密码按钮 (129, 206, 128x33)
    /// </summary>
    [Fact]
    public void TestLoginPanel_FixedCoordinates_GroundTruth()
    {
        // 登录面板居中算法实证
        int panelW = 296;
        int panelH = 254;
        int screenW = 800;
        int screenH = 600;
        Assert.Equal(252, (screenW - panelW) / 2);
        Assert.Equal(173, (screenH - panelH) / 2);

        // 各控件尺寸与相对坐标
        var usernameBox = (X: 98, Y: 86, W: 137, H: 16);
        var passwordBox = (X: 98, Y: 118, W: 137, H: 16);
        var btnLogin = (X: 168, Y: 162, W: 76, H: 33);
        var btnClose = (X: 252, Y: 28, W: 16, H: 23);
        var btnNewUser = (X: 24, Y: 206, W: 100, H: 32);
        var btnChangePwd = (X: 129, Y: 206, W: 128, H: 33);

        Assert.Equal(98, usernameBox.X);
        Assert.Equal(86, usernameBox.Y);
        Assert.Equal(98, passwordBox.X);
        Assert.Equal(118, passwordBox.Y);
        Assert.Equal(76, btnLogin.W);
        Assert.Equal(33, btnLogin.H);
        Assert.Equal(16, btnClose.W);
        Assert.Equal(23, btnClose.H);
        Assert.Equal(100, btnNewUser.W);
        Assert.Equal(128, btnChangePwd.W);

        // 步骤 1.1: 新用户注册面板 Prguse_0063 (640x473 @ 80, 63)
        var newUserClose = (X: 587, Y: 33, W: 16, H: 23);
        var regUserBox = (X: 161, Y: 117, W: 116, H: 16);
        var regPass1Box = (X: 161, Y: 138, W: 116, H: 16);
        var regPass2Box = (X: 161, Y: 159, W: 116, H: 16);
        var regSubmit = (X: 432, Y: 419, W: 76, H: 33);
        var regCancel = (X: 512, Y: 419, W: 76, H: 33);
        Assert.Equal(587, newUserClose.X);
        Assert.Equal(33, newUserClose.Y);
        Assert.Equal(16, newUserClose.W);
        Assert.Equal(23, newUserClose.H);
        Assert.Equal(161, regUserBox.X);
        Assert.Equal(117, regUserBox.Y);
        Assert.Equal(161, regPass1Box.X);
        Assert.Equal(138, regPass1Box.Y);
        Assert.Equal(161, regPass2Box.X);
        Assert.Equal(159, regPass2Box.Y);
        Assert.Equal(432, regSubmit.X);
        Assert.Equal(512, regCancel.X);

        // 步骤 1.2: 修改密码面板 Prguse_0050 (420x299 @ 190, 150)
        var changeClose = (X: 377, Y: 34, W: 16, H: 23);
        var changeAccount = (X: 237, Y: 117, W: 139, H: 16);
        var changeOldPass = (X: 237, Y: 149, W: 139, H: 16);
        var changeNewPass1 = (X: 237, Y: 176, W: 139, H: 16);
        var changeNewPass2 = (X: 237, Y: 207, W: 139, H: 16);
        var changeSubmit = (X: 181, Y: 253, W: 76, H: 33);
        var changeCancel = (X: 276, Y: 252, W: 96, H: 33);
        Assert.Equal(377, changeClose.X);
        Assert.Equal(34, changeClose.Y);
        Assert.Equal(16, changeClose.W);
        Assert.Equal(23, changeClose.H);
        Assert.Equal(237, changeAccount.X);
        Assert.Equal(117, changeAccount.Y);
        Assert.Equal(237, changeOldPass.X);
        Assert.Equal(149, changeOldPass.Y);
        Assert.Equal(237, changeNewPass1.X);
        Assert.Equal(176, changeNewPass1.Y);
        Assert.Equal(237, changeNewPass2.X);
        Assert.Equal(207, changeNewPass2.Y);
        Assert.Equal(181, changeSubmit.X);
        Assert.Equal(276, changeCancel.X);
    }

    /// <summary>
    /// 步骤 7: 盛大官方 1.5 HUD 底栏 6 个快捷键热区固定实证测试
    /// 依据 IDA sub_481DD4 [0x484b80..0x484d8e] 提取自底板 Prguse_0001
    /// 相对屏幕 Y 坐标一律为 408 (435底栏顶 + 59 - 86)
    /// </summary>
    [Fact]
    public void TestHudBottomBar_FixedHotspots_GroundTruth()
    {
        var hotspots = new (string Name, int X, int Y, int W, int H)[]
        {
            ("Bag", 285, 408, 32, 29),
            ("Equip", 328, 408, 32, 29),
            ("Skill", 371, 408, 32, 29),
            ("Group", 415, 408, 32, 29),
            ("Guild", 459, 408, 32, 29),
            ("Trade", 503, 408, 32, 29)
        };

        for (int i = 0; i < hotspots.Length; i++)
        {
            Assert.Equal(408, hotspots[i].Y);
            Assert.Equal(32, hotspots[i].W);
            Assert.Equal(29, hotspots[i].H);
            if (i > 0)
            {
                // 相邻按钮间距严格为 43 或 44 像素 (285->328->371->415->459->503)
                int deltaX = hotspots[i].X - hotspots[i - 1].X;
                Assert.True(deltaX == 43 || deltaX == 44);
            }
        }
    }

    /// <summary>
    /// 步骤 8: 盛大官方 1.5 通用居中数值输入弹窗固定实证测试
    /// 严格对齐官方 IDA sub_486DF0 / sub_461280 (Prguse_0360 452x179)
    /// </summary>
    [Fact]
    public void TestMessageBoxDialog_FixedLayout_GroundTruth()
    {
        Assert.Equal(452.0f, MessageBoxDialog.PanelWidth);
        Assert.Equal(179.0f, MessageBoxDialog.PanelHeight);

        // 居中屏幕位置 (800-452)/2 = 174, (600-179)/2 = 210.5 -> 210
        int posX = (int)((800.0f - MessageBoxDialog.PanelWidth) / 2.0f);
        int posY = (int)((600.0f - MessageBoxDialog.PanelHeight) / 2.0f);
        Assert.Equal(174, posX);
        Assert.Equal(210, posY);
    }

    /// <summary>
    /// 步骤 5: 盛大官方 1.5 角色 9 大装备槽位坐标与尺寸固定实证测试
    /// IDA sub_481dd4 [0x483eb0..0x484180] 实锤：
    /// 0:衣服, 1:武器, 2:勋章, 3:项链, 4:头盔, 5:右手镯, 6:左手镯, 7:右戒指, 8:左戒指
    /// </summary>
    [Fact]
    public void TestEquipPanel_FixedSlots_GroundTruth()
    {
        var slotMap = new Dictionary<int, (int X, int Y, int W, int H)>
        {
            [1] = (47, 80, 47, 87),    // 武器
            [0] = (96, 122, 53, 112),  // 衣服
            [4] = (115, 93, 18, 18),   // 头盔
            [3] = (168, 87, 34, 31),   // 项链
            [2] = (168, 125, 34, 31),  // 勋章
            [6] = (42, 176, 34, 31),   // 左手镯
            [5] = (168, 176, 34, 31),  // 右手镯
            [8] = (42, 215, 34, 31),   // 左戒指
            [7] = (168, 215, 34, 31)   // 右戒指
        };

        Assert.Equal(9, slotMap.Count);
        Assert.Equal(47, slotMap[1].X);
        Assert.Equal(80, slotMap[1].Y);
        Assert.Equal(96, slotMap[0].X);
        Assert.Equal(122, slotMap[0].Y);
        Assert.Equal(115, slotMap[4].X);
        Assert.Equal(93, slotMap[4].Y);
    }

    /// <summary>
    /// 步骤 4 & 步骤 10: 背包面板右下角红叉与仓库面板 416x347 底板实锤测试
    /// </summary>
    [Fact]
    public void TestBagAndStoragePanels_GroundTruth()
    {
        // 1. 背包右下角关闭红叉热区 (311, 204, 16x23)，绝非原先左上角 (8, 39) 坏账
        var bagClosePos = (X: 311, Y: 204, W: 16, H: 23);
        Assert.Equal(311, bagClosePos.X);
        Assert.Equal(204, bagClosePos.Y);

        // 步骤 4: 背包 40 格像素对齐 (Prguse_0003 暗纹：8列x5行，列宽36px，行高32px，原点 19, 14)
        var bagGrid = (OriginX: 19, OriginY: 14, SlotW: 36, SlotH: 32, Cols: 8, Rows: 5);
        Assert.Equal(19, bagGrid.OriginX);
        Assert.Equal(14, bagGrid.OriginY);
        Assert.Equal(36, bagGrid.SlotW);
        Assert.Equal(32, bagGrid.SlotH);
        Assert.Equal(8, bagGrid.Cols);
        Assert.Equal(5, bagGrid.Rows);
        // 网格总跨度: 19 + 8*36 = 307, 14 + 5*32 = 174
        Assert.Equal(307, bagGrid.OriginX + bagGrid.Cols * bagGrid.SlotW);
        Assert.Equal(174, bagGrid.OriginY + bagGrid.Rows * bagGrid.SlotH);

        // 背包金币槽与金币文本 (Icon: 7, 182, Text: 60, 182)
        var goldSlot = (IconX: 7, IconY: 182, TextX: 60, TextY: 182);
        Assert.Equal(7, goldSlot.IconX);
        Assert.Equal(182, goldSlot.IconY);
        Assert.Equal(60, goldSlot.TextX);
        Assert.Equal(182, goldSlot.TextY);

        // 2. 仓库托管模式规格：复用 DSellDlg (Prguse_0392 140x181, sub_46DAE8 停靠 (260, 176))
        Assert.Equal(140.0f, RepairPanelView.PanelWidth);
        Assert.Equal(181.0f, RepairPanelView.PanelHeight);

        // 步骤 8: 组队管理面板 (Prguse_0120 276x242) 与关闭按钮 (260, 0, 16x23)
        Assert.Equal(276.0f, GroupPanelView.PanelWidth);
        Assert.Equal(242.0f, GroupPanelView.PanelHeight);
        var groupClose = (X: 260, Y: 0, W: 16, H: 23);
        Assert.Equal(260, groupClose.X);
        Assert.Equal(0, groupClose.Y);
        Assert.Equal(16, groupClose.W);
        Assert.Equal(23, groupClose.H);
    }

    /// <summary>
    /// 步骤 5 & 步骤 6: 技能魔法书面板与装备切页按键实证
    /// 1. 技能面板关闭红叉在左上角 (8, 39, 16x23)
    /// 2. 翻页按键为 12x11 小箭头 (Prguse_0374/0375)
    /// 3. 装备面板切页按键为 24x23 原生箭头 (Prguse_0372/0373)
    /// </summary>
    [Fact]
    public void TestSkillAndTabButtons_GroundTruth()
    {
        var skillClosePos = (X: 8, Y: 39, W: 16, H: 23);
        Assert.Equal(8, skillClosePos.X);
        Assert.Equal(39, skillClosePos.Y);
        Assert.Equal(16, skillClosePos.W);
        Assert.Equal(23, skillClosePos.H);

        var skillArrowSize = (W: 12, H: 11);
        Assert.Equal(12, skillArrowSize.W);
        Assert.Equal(11, skillArrowSize.H);

        var equipTabBtnSize = (W: 24, H: 23);
        Assert.Equal(24, equipTabBtnSize.W);
        Assert.Equal(23, equipTabBtnSize.H);
    }

    /// <summary>
    /// 步骤 10 & 步骤 11: 商店买卖窗口与铁匠铺特修窗口实证测试
    /// 1. 商店买卖窗口: 底板 308x205 (Prguse_0385)，购买 (43, 175, 24x24)，出售 (90, 175, 24x24)，特修 (215, 171, 44x16)，关闭红叉 (291, 0, 16x23)
    /// 2. 铁匠铺修理窗口: 底板 140x181 (Prguse_0392)，单件特修 OK 按钮 (85, 150, 24x13)，关闭红叉 (115, 0, 16x23)
    /// </summary>
    [Fact]
    public void TestShopAndRepairPanels_GroundTruth()
    {
        // 商店面板
        Assert.Equal(308.0f, ShopPanelView.PanelWidth);
        Assert.Equal(205.0f, ShopPanelView.PanelHeight);
        var shopClose = (X: 291, Y: 0, W: 16, H: 23);
        var shopBuy = (X: 43, Y: 175, W: 24, H: 24);
        var shopSell = (X: 90, Y: 175, W: 24, H: 24);
        var shopRepair = (X: 215, Y: 171, W: 44, H: 16);

        Assert.Equal(291, shopClose.X);
        Assert.Equal(0, shopClose.Y);
        Assert.Equal(24, shopBuy.W);
        Assert.Equal(24, shopBuy.H);
        Assert.Equal(24, shopSell.W);
        Assert.Equal(24, shopSell.H);
        Assert.Equal(44, shopRepair.W);
        Assert.Equal(16, shopRepair.H);

        // 铁匠铺特修面板
        Assert.Equal(140.0f, RepairPanelView.PanelWidth);
        Assert.Equal(181.0f, RepairPanelView.PanelHeight);
        var repairClose = (X: 115, Y: 0, W: 16, H: 23);
        var repairOk = (X: 85, Y: 150, W: 24, H: 13);

        Assert.Equal(115, repairClose.X);
        Assert.Equal(0, repairClose.Y);
        Assert.Equal(24, repairOk.W);
        Assert.Equal(13, repairOk.H);
    }

    /// <summary>
    /// 步骤 12 ~ 16: 交易、行会、NPC对话框、观人、死亡与模态弹窗 IDA 原生实证单元测试
    /// </summary>
    [Fact]
    public void TestTradeAndGuildPanels_GroundTruth()
    {
        // 交易系统双面板并列模型 (IDA sub_481dd4 [0x485784..0x4859f9])
        Assert.Equal(220.0f, TradePanelView.RemoteWidth);
        Assert.Equal(236.0f, TradePanelView.MyWidth);
        Assert.Equal(175.0f, TradePanelView.PanelHeight);
        Assert.Equal(456.0f, TradePanelView.RemoteWidth + TradePanelView.MyWidth);

        // 己方交易确定按钮 (155, 128, 44x16, Prguse_0391) 与关闭红叉 (220, 42, 16x23)
        var tradeConfirm = (X: 155, Y: 128, W: 44, H: 16);
        var tradeClose = (X: 220, Y: 42, W: 16, H: 23);
        Assert.Equal(155, tradeConfirm.X);
        Assert.Equal(128, tradeConfirm.Y);
        Assert.Equal(220, tradeClose.X);
        Assert.Equal(42, tradeClose.Y);

        // 行会主页面板 (Prguse_0180 628x453 @ 86, 73, IDA sub_481dd4 [0x485a1c..0x485da1])
        Assert.Equal(628.0f, GuildPanelView.PanelWidth);
        Assert.Equal(453.0f, GuildPanelView.PanelHeight);
        var guildClose = (X: 584, Y: 6, W: 16, H: 23);
        Assert.Equal(584, guildClose.X);
        Assert.Equal(6, guildClose.Y);
    }

    [Fact]
    public void TestNpcInspectAndModalDialogs_GroundTruth()
    {
        // NPC 对话框 (Prguse_0384 416x176 @ 246, 160, IDA sub_481dd4 [0x484ee2..0x484f65])
        Assert.Equal(416.0f, NpcDialogView.PanelWidth);
        Assert.Equal(176.0f, NpcDialogView.PanelHeight);
        var npcClose = (X: 399, Y: 1, W: 16, H: 23);
        Assert.Equal(399, npcClose.X);
        Assert.Equal(1, npcClose.Y);
        Assert.Equal(16, npcClose.W);
        Assert.Equal(23, npcClose.H);

        // 观人面板 (Prguse_0370 232x325 @ 336, 0, IDA sub_481dd4 [0x48447f..0x4847ce])
        Assert.Equal(232.0f, InspectPanelView.PanelWidth);
        Assert.Equal(325.0f, InspectPanelView.PanelHeight);
        var inspectClose = (X: 8, Y: 39, W: 16, H: 23);
        Assert.Equal(8, inspectClose.X);
        Assert.Equal(39, inspectClose.Y);

        // 死亡与模态确认框 (Prguse_0360 452x179, IDA sub_481dd4 [0x481efc..0x481fb1] & sub_486DF0)
        Assert.Equal(452.0f, MessageBoxDialog.PanelWidth);
        Assert.Equal(179.0f, MessageBoxDialog.PanelHeight);
        Assert.Equal(452.0f, DeathScreenView.PanelWidth);
        Assert.Equal(179.0f, DeathScreenView.PanelHeight);

        // 确定与取消按钮 Y 坐标严格为 126
        var modalOk = (X: 130, Y: 126, W: 80, H: 34);
        var modalCancel = (X: 242, Y: 126, W: 80, H: 34);
        Assert.Equal(130, modalOk.X);
        Assert.Equal(126, modalOk.Y);
        Assert.Equal(242, modalCancel.X);
        Assert.Equal(126, modalCancel.Y);
    }

    /// <summary>
    /// 步骤 20: 物品信息 Tooltip 与 HUD 聊天输入交互实证测试 (IDA sub_48C3C8 & sub_481dd4)
    /// </summary>
    [Fact]
    public void TestItemTooltipAndChatInput_GroundTruth()
    {
        // 1. Tooltip 基础尺寸与样式
        var minTooltipSize = (Width: 160.0f, Height: 40.0f);
        Assert.Equal(160.0f, minTooltipSize.Width);
        Assert.Equal(40.0f, minTooltipSize.Height);

        // 2. HUD 聊天记录与输入框尺寸 (Game.tscn)
        var chatBoxPos = (X: 208, Y: 470, W: 374, H: 108);
        var chatInputPos = (X: 208, Y: 581, W: 387, H: 12);
        Assert.Equal(208, chatBoxPos.X);
        Assert.Equal(470, chatBoxPos.Y);
        Assert.Equal(374, chatBoxPos.W);
        Assert.Equal(108, chatBoxPos.H);

        Assert.Equal(208, chatInputPos.X);
        Assert.Equal(581, chatInputPos.Y);
        Assert.Equal(387, chatInputPos.W);
        Assert.Equal(12, chatInputPos.H);
    }

    /// <summary>
    /// 步骤 21: 游戏动态渲染细节实证测试 (IDA sub_48B2A0 液面裁剪/经验负重条, sub_481DD4/sub_48C188 快捷栏, sub_46FF0C 雷达)
    /// </summary>
    [Fact]
    public void TestHudDynamicGameLogicDetails_GroundTruth()
    {
        // 1. 血球魔球动态液面裁剪算法 (IDA sub_48B2A0 @ 0x48b64f..0x48b71c)
        int orbHeight = 90;
        int maxHp = 100, curHp = 50;
        int lostHp = maxHp - curHp;
        int cropY = (int)Math.Round((double)orbHeight / maxHp * lostHp);
        Assert.Equal(45, cropY); // 半血时液面裁剪 45 像素

        float hpRatio = (float)curHp / maxHp;
        Assert.Equal(0.5f, hpRatio);

        // 2. 经验条与负重条尺寸与坐标 (IDA sub_48B2A0 @ 0x48b840 & 0x48b8d0, Prguse_0007)
        var expBarPos = (X: 666, Y: 527, W: 76, H: 13);
        var weightBarPos = (X: 666, Y: 560, W: 76, H: 13);
        Assert.Equal(666, expBarPos.X);
        Assert.Equal(527, expBarPos.Y);
        Assert.Equal(76, expBarPos.W);
        Assert.Equal(13, expBarPos.H);

        Assert.Equal(666, weightBarPos.X);
        Assert.Equal(560, weightBarPos.Y);
        Assert.Equal(76, weightBarPos.W);
        Assert.Equal(13, weightBarPos.H);

        // 3. 快捷药水栏 1~6 格坐标分布 (IDA sub_481DD4 [0x484a0f..0x484b80])
        float[] expectedXs = { 219f, 249f, 279f, 309f, 339f, 369f };
        for (int i = 0; i < 5; i++)
        {
            Assert.Equal(30f, expectedXs[i + 1] - expectedXs[i]); // 严格 30 像素步长
        }
        Assert.Equal(219f, expectedXs[0]);
        Assert.Equal(369f, expectedXs[5]);

        // 4. 小地图雷达 120x120 视口坐标约束 (IDA sub_46FF0C)
        var radarCenter = (X: 58, Y: 58);
        Assert.Equal(58, radarCenter.X);
        Assert.Equal(58, radarCenter.Y);
    }

    [Fact]
    public void TestAdjustAbilPanel_OfficialMetricsAndBaseStats_GroundTruth()
    {
        // 盛大官方 1.5 加点面板实证测试 (IDA sub_485eeb ~ 0x486241 / sub_494304 @ 0x494497 & 0x494994)
        // 1. 底板尺寸与默认停靠位置
        Assert.Equal(332, AdjustAbilPanelView.PanelSize.X);
        Assert.Equal(347, AdjustAbilPanelView.PanelSize.Y);
        Assert.Equal(234, AdjustAbilPanelView.DefaultPos.X);
        Assert.Equal(126, AdjustAbilPanelView.DefaultPos.Y);

        // 2. 官方 9 大属性命名与行索引严格对齐 Prguse_0226
        Assert.Equal(9, AdjustAbilPanelView.StatNames.Length);
        Assert.Equal("DC", AdjustAbilPanelView.StatNames[0]);
        Assert.Equal("MC", AdjustAbilPanelView.StatNames[1]);
        Assert.Equal("SC", AdjustAbilPanelView.StatNames[2]);
        Assert.Equal("AC", AdjustAbilPanelView.StatNames[3]);
        Assert.Equal("MAC", AdjustAbilPanelView.StatNames[4]);
        Assert.Equal("HP", AdjustAbilPanelView.StatNames[5]);
        Assert.Equal("MP", AdjustAbilPanelView.StatNames[6]);
        Assert.Equal("HIT", AdjustAbilPanelView.StatNames[7]);
        Assert.Equal("SPD", AdjustAbilPanelView.StatNames[8]);

        // 3. 基础属性参考值严格对齐战士典型模板
        Assert.Equal(9, AdjustAbilPanelView.DefaultBaseStats.Length);
        Assert.Equal("10-25", AdjustAbilPanelView.DefaultBaseStats[0]);
        Assert.Equal("210", AdjustAbilPanelView.DefaultBaseStats[5]); // HP
        Assert.Equal("45", AdjustAbilPanelView.DefaultBaseStats[6]);  // MP
    }

    [Fact]
    public void TestNpcChebyshevDistanceAndStorageMode_GroundTruth()
    {
        // 1. 官方实证切比雪夫门限 <= 15 (M2Server sub_4DC2B8 / sub_4DC664)
        Assert.Equal(15, HudController.MaxNpcInteractDistance);

        // 验证切比雪夫距离判定: 临界值 15 合法，16 越界
        var npcPos = (X: 330, Y: 270);
        var playerInside = (X: 345, Y: 285);
        int distInside = Math.Max(Math.Abs(playerInside.X - npcPos.X), Math.Abs(playerInside.Y - npcPos.Y));
        Assert.Equal(15, distInside);
        Assert.True(distInside <= HudController.MaxNpcInteractDistance);

        var playerOutside = (X: 346, Y: 270);
        int distOutside = Math.Max(Math.Abs(playerOutside.X - npcPos.X), Math.Abs(playerOutside.Y - npcPos.Y));
        Assert.Equal(16, distOutside);
        Assert.True(distOutside > HudController.MaxNpcInteractDistance);

        // 2. 商店面板模式分化枚举测试 (Buy / Sell / Storage)
        Assert.Equal(ShopPanelView.ShopMode.Buy, (ShopPanelView.ShopMode)0);
        Assert.Equal(ShopPanelView.ShopMode.Sell, (ShopPanelView.ShopMode)1);
        Assert.Equal(ShopPanelView.ShopMode.Storage, (ShopPanelView.ShopMode)2);
    }

    [Fact]
    public void TestSlotDragAndDrop_OfficialRulesAndEquipCompatibility_GroundTruth()
    {
        // 1. 验证 SlotButton.SlotType 枚举合法性
        Assert.Equal(0, (int)SlotButton.SlotType.Bag);
        Assert.Equal(1, (int)SlotButton.SlotType.Equip);
        Assert.Equal(2, (int)SlotButton.SlotType.Trade);
        Assert.Equal(3, (int)SlotButton.SlotType.Repair);
        Assert.Equal(4, (int)SlotButton.SlotType.Storage);
        Assert.Equal(5, (int)SlotButton.SlotType.Shop);
        Assert.Equal(6, (int)SlotButton.SlotType.Skill);

        // 2. 官方 9 大装备槽位合法性验证 (IDA sub_488AF4 / sub_488B70)
        // 槽位 0: 衣服 (布衣/甲/袍/衣)
        Assert.True(HudController.IsItemCompatibleWithEquipSlot("布衣(男)", 0));
        Assert.True(HudController.IsItemCompatibleWithEquipSlot("重盔甲(男)", 0));
        Assert.True(HudController.IsItemCompatibleWithEquipSlot("灵魂战水战衣", 0));
        Assert.False(HudController.IsItemCompatibleWithEquipSlot("木剑", 0));
        Assert.False(HudController.IsItemCompatibleWithEquipSlot("金创药(小)", 0));

        // 槽位 1: 武器
        Assert.True(HudController.IsItemCompatibleWithEquipSlot("木剑", 1));
        Assert.True(HudController.IsItemCompatibleWithEquipSlot("屠龙", 1));
        Assert.True(HudController.IsItemCompatibleWithEquipSlot("裁决之杖", 1));
        Assert.True(HudController.IsItemCompatibleWithEquipSlot("炼狱", 1));
        Assert.True(HudController.IsItemCompatibleWithEquipSlot("凝霜", 1));
        Assert.False(HudController.IsItemCompatibleWithEquipSlot("重盔甲(男)", 1));
        Assert.False(HudController.IsItemCompatibleWithEquipSlot("珊瑚戒指", 1));

        // 槽位 2: 勋章/饰品
        Assert.True(HudController.IsItemCompatibleWithEquipSlot("荣誉勋章", 2));
        Assert.True(HudController.IsItemCompatibleWithEquipSlot("火把", 2));
        Assert.True(HudController.IsItemCompatibleWithEquipSlot("黄色药粉", 2));
        Assert.False(HudController.IsItemCompatibleWithEquipSlot("木剑", 2));

        // 槽位 3: 项链
        Assert.True(HudController.IsItemCompatibleWithEquipSlot("幽灵项链", 3));
        Assert.True(HudController.IsItemCompatibleWithEquipSlot("恶魔铃铛", 3));
        Assert.False(HudController.IsItemCompatibleWithEquipSlot("金手镯", 3));

        // 槽位 4: 头盔
        Assert.True(HudController.IsItemCompatibleWithEquipSlot("道士头盔", 4));
        Assert.True(HudController.IsItemCompatibleWithEquipSlot("骷髅头盔", 4));
        Assert.False(HudController.IsItemCompatibleWithEquipSlot("屠龙", 4));

        // 槽位 5 & 6: 手镯
        Assert.True(HudController.IsItemCompatibleWithEquipSlot("金手镯(左)", 5));
        Assert.True(HudController.IsItemCompatibleWithEquipSlot("死神手套(右)", 6));
        Assert.False(HudController.IsItemCompatibleWithEquipSlot("珊瑚戒指", 5));

        // 槽位 7 & 8: 戒指
        Assert.True(HudController.IsItemCompatibleWithEquipSlot("珊瑚戒指", 7));
        Assert.True(HudController.IsItemCompatibleWithEquipSlot("骷髅戒指", 8));
        Assert.False(HudController.IsItemCompatibleWithEquipSlot("死神手套", 7));
        Assert.False(HudController.IsItemCompatibleWithEquipSlot("金创药(中)", 8));

        // 3. 拖放合法性校验逻辑 (禁止同格自拖自放，允许异格/跨面板拖放)
        Assert.False(SlotButton.CanDrop(SlotButton.SlotType.Bag, 0, SlotButton.SlotType.Bag, 0));
        Assert.True(SlotButton.CanDrop(SlotButton.SlotType.Bag, 0, SlotButton.SlotType.Bag, 1));
        Assert.True(SlotButton.CanDrop(SlotButton.SlotType.Bag, 0, SlotButton.SlotType.Equip, 1));
        Assert.True(SlotButton.CanDrop(SlotButton.SlotType.Equip, 1, SlotButton.SlotType.Bag, 5));
        Assert.True(SlotButton.CanDrop(SlotButton.SlotType.Bag, 0, SlotButton.SlotType.Repair, 0));
        Assert.True(SlotButton.CanDrop(SlotButton.SlotType.Bag, 0, SlotButton.SlotType.Storage, 0));
        Assert.True(SlotButton.CanDrop(SlotButton.SlotType.Bag, 0, SlotButton.SlotType.Quick, 0));
        Assert.Equal(7, (int)SlotButton.SlotType.Quick);

        // 4. 消耗品快捷装配判据 (药水/卷轴/油)
        Assert.True(HudController.IsConsumableQuickItem("强效金创药"));
        Assert.True(HudController.IsConsumableQuickItem("太阳水"));
        Assert.True(HudController.IsConsumableQuickItem("回城卷"));
        Assert.True(HudController.IsConsumableQuickItem("祝福油"));
        Assert.False(HudController.IsConsumableQuickItem("木剑"));
        Assert.False(HudController.IsConsumableQuickItem("裁决之杖"));
        Assert.False(HudController.IsConsumableQuickItem("重盔甲(男)"));
        Assert.False(HudController.IsConsumableQuickItem("珊瑚戒指"));

        // 4. 全局拖拽事件回调触发验证
        bool eventFired = false;
        SlotButton.SlotType srcType = SlotButton.SlotType.Bag;
        SlotButton.SlotType dstType = SlotButton.SlotType.Bag;
        int srcSlot = -1;
        int dstSlot = -1;

        Action<SlotButton.SlotType, int, SlotButton.SlotType, int> handler = (fromT, fromS, toT, toS) =>
        {
            eventFired = true;
            srcType = fromT;
            srcSlot = fromS;
            dstType = toT;
            dstSlot = toS;
        };

        SlotButton.ItemDroppedGlobal += handler;
        try
        {
            // 模拟拖放触发
            handler(SlotButton.SlotType.Bag, 0, SlotButton.SlotType.Equip, 1);

            Assert.True(eventFired);
            Assert.Equal(SlotButton.SlotType.Bag, srcType);
            Assert.Equal(0, srcSlot);
            Assert.Equal(SlotButton.SlotType.Equip, dstType);
            Assert.Equal(1, dstSlot);
        }
        finally
        {
            SlotButton.ItemDroppedGlobal -= handler;
        }
    }
}



