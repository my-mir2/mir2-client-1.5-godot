using System;
using Godot;
using Mir2Client.Utils;

namespace Mir2Client.UI;

/// <summary>
/// 盛大热血传奇 1.5 官方玩家交易系统 (DDealDlg & DDealRemoteDlg, 纯 C# 实现)
/// 严格 1:1 对齐官方双面板并列防大世界假死模型 (IDA sub_4857C7 / sub_485905)
/// </summary>
public partial class TradePanelView : Control, IManagedWindow
{
    [Signal]
    public delegate void AddDealItemRequestedEventHandler(int slot, string itemName);

    [Signal]
    public delegate void ChangeDealGoldRequestedEventHandler(int gold);

    [Signal]
    public delegate void EndDealConfirmedEventHandler();

    [Signal]
    public delegate void TradeCancelledEventHandler();

    public const float RemoteWidth = 220.0f;
    public const float MyWidth = 236.0f;
    public const float PanelHeight = 175.0f;

    private Panel? _remotePanel;
    private Panel? _myPanel;

    public string WindowName => "TradePanelView";
    public new bool IsVisible
    {
        get => Visible;
        set => Visible = value;
    }

    // 独立包围盒: 官方原生左右并列停靠模型 (IDA sub_481dd4 [0x485795/0x48592c])
    // 对方在左 (344, 0)，我方在右 (564, 0)，总尺寸 456x175
    public (int x, int y, int width, int height) Bounds =>
        ((int)Position.X, (int)Position.Y, (int)(RemoteWidth + MyWidth), (int)PanelHeight);

    public override void _Ready()
    {
        Visible = false;
        Size = new Vector2(RemoteWidth + MyWidth, PanelHeight);
        // 左右并列顶格停靠于屏幕右上角 (IDA 0x48592c: 564 - 220 = 344, Top = 0)
        Position = new Vector2(344, 0);
        MouseFilter = MouseFilterEnum.Pass;

        // 1. 左侧面板：对方面板 (Prguse_0390 220x175, IDA sub_481dd4 [0x48590f..0x485947])
        _remotePanel = new Panel
        {
            Name = "RemoteTradePanel",
            Position = new Vector2(0, 0),
            Size = new Vector2(RemoteWidth, PanelHeight),
            MouseFilter = MouseFilterEnum.Stop
        };
        var texRemote = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0390.png");
        if (texRemote != null)
        {
            _remotePanel.AddThemeStyleboxOverride("panel", new StyleBoxTexture { Texture = texRemote });
        }
        AddChild(_remotePanel);

        // 对方名字框：官方原生顶部黑色牌匾凹槽 (平坦可用区 Left=60, Top=4, Width=104, Height=14)
        var remoteNameBox = new Panel
        {
            Name = "RemoteNameBox",
            Position = new Vector2(60, 4),
            Size = new Vector2(104, 14),
            MouseFilter = MouseFilterEnum.Ignore
        };
        remoteNameBox.AddThemeStyleboxOverride("panel", new StyleBoxEmpty());
        _remotePanel.AddChild(remoteNameBox);

        var lblRemoteName = new Label
        {
            Name = "LblRemoteName",
            Text = "交易对方",
            Position = Vector2.Zero,
            Size = new Vector2(104, 14),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        lblRemoteName.AddThemeFontSizeOverride("font_size", 10);
        lblRemoteName.AddThemeColorOverride("font_color", new Color(0.9f, 0.9f, 0.9f));
        remoteNameBox.AddChild(lblRemoteName);

        // 对方 10 格物品表格网格 (Left=21, Top=56, Width=180, Height=66, 2行5列，单格36x33无缝咬合独立线框表格)
        var remoteGrid = new GridContainer
        {
            Name = "RemoteGridContainer",
            Position = new Vector2(21, 56),
            Size = new Vector2(180, 66),
            Columns = 5
        };
        remoteGrid.AddThemeConstantOverride("h_separation", 0);
        remoteGrid.AddThemeConstantOverride("v_separation", 0);
        _remotePanel.AddChild(remoteGrid);
        for (int i = 0; i < 10; i++)
        {
            var slot = new SlotButton
            {
                Name = $"RemoteSlot_{i}",
                CurrentSlotType = SlotButton.SlotType.Bag,
                SlotIndex = i,
                CustomMinimumSize = new Vector2(36, 33),
                Flat = true
            };
            remoteGrid.AddChild(slot);
        }

        // 对方金币图标与输入显示框 (IDA sub_481dd4 [0x4859e6]: Left=11, Top=137, 36x21; 黑色金币内胆槽 Left=60, Top=129, 86x16)
        var texGold = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0028.png");
        var remoteGoldIcon = new TextureRect
        {
            Name = "RemoteGoldIcon",
            Position = new Vector2(11, 137),
            Size = new Vector2(36, 21),
            Texture = texGold
        };
        _remotePanel.AddChild(remoteGoldIcon);

        var remoteGoldBox = new Panel
        {
            Name = "RemoteGoldBox",
            Position = new Vector2(65, 127),
            Size = new Vector2(82, 17),
            MouseFilter = MouseFilterEnum.Ignore
        };
        remoteGoldBox.AddThemeStyleboxOverride("panel", new StyleBoxEmpty());
        _remotePanel.AddChild(remoteGoldBox);

        var lblRemoteGold = new Label
        {
            Name = "LblRemoteGold",
            Position = new Vector2(4, 0),
            Size = new Vector2(74, 17),
            Text = "0",
            VerticalAlignment = VerticalAlignment.Center
        };
        lblRemoteGold.AddThemeFontSizeOverride("font_size", 10);
        lblRemoteGold.AddThemeColorOverride("font_color", new Color(1.0f, 0.9f, 0.3f));
        remoteGoldBox.AddChild(lblRemoteGold);

        // 2. 右侧面板：我方面板 (Prguse_0389 236x175, IDA sub_481dd4 [0x485784..0x4857b5])
        _myPanel = new Panel
        {
            Name = "MyTradePanel",
            Position = new Vector2(RemoteWidth, 0),
            Size = new Vector2(MyWidth, PanelHeight),
            MouseFilter = MouseFilterEnum.Stop
        };
        var texMy = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0389.png");
        if (texMy != null)
        {
            _myPanel.AddThemeStyleboxOverride("panel", new StyleBoxTexture { Texture = texMy });
        }
        AddChild(_myPanel);

        // 我方名字框：官方原生顶部黑色牌匾凹槽 (平坦可用区 Left=60, Top=4, Width=104, Height=14)
        var myNameBox = new Panel
        {
            Name = "MyNameBox",
            Position = new Vector2(60, 4),
            Size = new Vector2(104, 14),
            MouseFilter = MouseFilterEnum.Ignore
        };
        myNameBox.AddThemeStyleboxOverride("panel", new StyleBoxEmpty());
        _myPanel.AddChild(myNameBox);

        var lblMyName = new Label
        {
            Name = "LblMyName",
            Text = "我",
            Position = Vector2.Zero,
            Size = new Vector2(104, 14),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        lblMyName.AddThemeFontSizeOverride("font_size", 10);
        lblMyName.AddThemeColorOverride("font_color", new Color(0.9f, 0.9f, 0.9f));
        myNameBox.AddChild(lblMyName);

        // 我方 10 格物品表格网格 (Left=21, Top=56, Width=180, Height=66, 2行5列，单格36x33无缝咬合独立线框表格)
        var myGrid = new GridContainer
        {
            Name = "MyGridContainer",
            Position = new Vector2(21, 56),
            Size = new Vector2(180, 66),
            Columns = 5
        };
        myGrid.AddThemeConstantOverride("h_separation", 0);
        myGrid.AddThemeConstantOverride("v_separation", 0);
        _myPanel.AddChild(myGrid);
        for (int i = 0; i < 10; i++)
        {
            var slot = new SlotButton
            {
                Name = $"MySlot_{i}",
                CurrentSlotType = SlotButton.SlotType.Bag,
                SlotIndex = i,
                CustomMinimumSize = new Vector2(36, 33),
                Flat = true
            };
            myGrid.AddChild(slot);
        }

        // 我方金币图标与输入框 (IDA sub_481dd4 [0x4858e0]: Left=11, Top=137, 36x21; 黑色金币内胆槽 Left=65, Top=127, 88x17 紧邻交易按键)
        var myGoldIcon = new TextureRect
        {
            Name = "MyGoldIcon",
            Position = new Vector2(11, 137),
            Size = new Vector2(36, 21),
            Texture = texGold
        };
        _myPanel.AddChild(myGoldIcon);

        var myGoldBox = new Panel
        {
            Name = "MyGoldBox",
            Position = new Vector2(65, 127),
            Size = new Vector2(88, 17),
            MouseFilter = MouseFilterEnum.Ignore
        };
        myGoldBox.AddThemeStyleboxOverride("panel", new StyleBoxEmpty());
        _myPanel.AddChild(myGoldBox);

        var lblMyGold = new Label
        {
            Name = "LblMyGold",
            Position = new Vector2(4, 0),
            Size = new Vector2(80, 17),
            Text = "0",
            VerticalAlignment = VerticalAlignment.Center
        };
        lblMyGold.AddThemeFontSizeOverride("font_size", 10);
        lblMyGold.AddThemeColorOverride("font_color", new Color(1.0f, 0.9f, 0.3f));
        myGoldBox.AddChild(lblMyGold);

        // 官方原生交易锁定按钮 (Prguse_0391 @ 155, 128, 44x16, IDA sub_481dd4 [0x485854..0x485867])
        // 未按下显示立体凸起 trade_btn_raised，按下或锁定态显示凹陷 Prguse_0391，消除凹陷黑斑感
        var texConfirmNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/trade_btn_raised.png");
        var texConfirmPressed = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0391.png");
        var btnConfirm = new TextureButton
        {
            Name = "BtnTradeLock",
            Position = new Vector2(155, 128),
            Size = new Vector2(44, 16),
            TextureNormal = texConfirmNormal,
            TexturePressed = texConfirmPressed
        };
        btnConfirm.Pressed += OnConfirm;
        _myPanel.AddChild(btnConfirm);

        // 官方原生交易取消/关闭按钮 (Prguse_0064 @ 220, 42, 16x23, IDA sub_481dd4 [0x48589a..0x4858cd])
        // Prguse_0389 底板在 (220, 42) 处已自带烘焙凸起红叉，TextureNormal 必须为 null，按下态显示凹陷 Prguse_0064
        var texCancel = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0064.png");
        var btnCancel = new TextureButton
        {
            Name = "BtnTradeClose",
            Position = new Vector2(220, 42),
            Size = new Vector2(16, 23),
            TextureNormal = null,
            TexturePressed = texCancel
        };
        btnCancel.Pressed += OnCancel;
        _myPanel.AddChild(btnCancel);
    }

    public void OpenTrade(string targetName)
    {
        Visible = true;
    }

    private void OnConfirm()
    {
        EmitSignal(SignalName.EndDealConfirmed);
    }

    private void OnCancel()
    {
        Visible = false;
        EmitSignal(SignalName.TradeCancelled);
    }
}
