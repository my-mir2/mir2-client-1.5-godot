using System;
using Godot;
using Mir2Client.Utils;

namespace Mir2Client.UI;

/// <summary>
/// 盛大热血传奇 1.5 官方玩家交易系统 (DDealDlg & DDealRemoteDlg, 纯 C# 实现)
/// 严格 1:1 对齐官方双面板并列防大世界假死模型 (IDA sub_481DD4 内偏移 0x4857C7 / 0x485905)
/// 严格遵循防作弊防偷换锁定状态机 (IDA 0x4DDC8C / 0x4DD79C 非函数，乃状态字段引用，逻辑以 TradeManager 状态机为准)
/// </summary>
public partial class TradePanelView : Control, IManagedWindow
{
    [Signal]
    public delegate void AddDealItemRequestedEventHandler(int slot, string itemName);

    [Signal]
    public delegate void DelDealItemRequestedEventHandler(int slot);

    [Signal]
    public delegate void ChangeDealGoldRequestedEventHandler(int gold);

    [Signal]
    public delegate void DealGoldClickedEventHandler();

    [Signal]
    public delegate void EndDealConfirmedEventHandler();

    [Signal]
    public delegate void TradeCancelledEventHandler();

    public const float RemoteWidth = 220.0f;
    public const float MyWidth = 236.0f;
    public const float PanelHeight = 175.0f;

    private Panel? _remotePanel;
    private Panel? _myPanel;

    private Label? _lblRemoteName;
    private Label? _lblMyName;
    private Label? _lblRemoteGold;
    private Label? _lblMyGold;
    private TextureButton? _btnConfirm;
    private TextureButton? _btnCancel;

    private readonly SlotButton[] _remoteSlots = new SlotButton[10];
    private readonly SlotButton[] _mySlots = new SlotButton[10];

    private readonly string?[] _myItems = new string?[10];
    private readonly string?[] _remoteItems = new string?[10];

    private int _myGold = 0;
    private int _remoteGold = 0;
    private bool _isMyLocked = false;
    private bool _isRemoteLocked = false;
    private ulong _lastConfirmTickMsec = 0;

    private Texture2D? _texConfirmNormal;
    private Texture2D? _texConfirmPressed;

    public string WindowName => "TradePanelView";
    public Official24DialogCompendium.DialogId DialogId => Official24DialogCompendium.DialogId.TradePanel;
    public new bool IsVisible
    {
        get => Visible;
        set => Visible = value;
    }

    public bool IsMyLocked => _isMyLocked;
    public bool IsRemoteLocked => _isRemoteLocked;
    public int MyGold => _myGold;
    public int RemoteGold => _remoteGold;

    public const float PanelOverlap = 15.0f;
    // 官方 1.5 铁证 (IDA sub_4915EC @ 0x491643):
    // 对方交易窗 (Prguse_0390 220x175) Top = 0
    // 我方交易窗 (Prguse_0389 236x175) Top = Remote.Height - 15 = 160, 两者上下重叠咬合 15px
    // 总高度 = 160 + 175 = 335px
    public const float TotalTradeHeight = PanelHeight * 2 - PanelOverlap;

    public (int x, int y, int width, int height) Bounds =>
        ((int)Position.X, (int)Position.Y, (int)MyWidth, (int)TotalTradeHeight);

    public override void _Ready()
    {
        Visible = false;
        Size = new Vector2(MyWidth, TotalTradeHeight);
        // 官方原版 IDA sub_4915EC (0x4915f5, 0x491618): 对方与我方面板 Left 严格均为 0x1D0 (464), Top=0
        Position = new Vector2(464, 0);
        MouseFilter = MouseFilterEnum.Pass;

        // 1. 上方对方面板：对方面板 (Prguse_0390 220x175, IDA sub_4915EC 0x4915f5 left=464, 0x491608 top=0)
        _remotePanel = new Panel
        {
            Name = "RemoteTradePanel",
            Position = Vector2.Zero, // 与我方面板左侧绝对对齐 (Left 均为 464)
            Size = new Vector2(RemoteWidth, PanelHeight),
            MouseFilter = MouseFilterEnum.Stop
        };
        var texRemote = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0390.png");
        if (texRemote != null)
        {
            _remotePanel.AddThemeStyleboxOverride("panel", new StyleBoxTexture { Texture = texRemote });
        }
        AddChild(_remotePanel);

        // 对方名字框：官方原生顶部黑色牌匾凹槽 (平坦可用区 Left=58, Top=2, Width=110, Height=20)
        var remoteNameBox = new Panel
        {
            Name = "RemoteNameBox",
            Position = new Vector2(58, 2),
            Size = new Vector2(110, 20),
            MouseFilter = MouseFilterEnum.Ignore
        };
        remoteNameBox.AddThemeStyleboxOverride("panel", new StyleBoxEmpty());
        _remotePanel.AddChild(remoteNameBox);

        _lblRemoteName = new Label
        {
            Name = "LblRemoteName",
            Text = "交易对方",
            Position = Vector2.Zero,
            Size = new Vector2(110, 20),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        _lblRemoteName.AddThemeFontSizeOverride("font_size", 10);
        _lblRemoteName.AddThemeColorOverride("font_color", new Color(0.95f, 0.9f, 0.6f));
        remoteNameBox.AddChild(_lblRemoteName);

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
                CurrentSlotType = SlotButton.SlotType.Trade,
                SlotIndex = i,
                CustomMinimumSize = new Vector2(36, 33),
                Flat = true,
                Icon = null
            };
            _remoteSlots[i] = slot;
            remoteGrid.AddChild(slot);
        }

        // 对方金币图标与输入显示框 (金币图标 36x21 居中于圆环圆心 (29, 147.5) -> (11, 137)；黑色内胆精确咬合 (60, 127, 83x17))
        var texGold = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0028.png");
        var remoteGoldIcon = new TextureRect
        {
            Name = "RemoteGoldIcon",
            Position = new Vector2(11, 137),
            Size = new Vector2(36, 21),
            Texture = texGold
        };
        _remotePanel.AddChild(remoteGoldIcon);

        // 对方金币输入凹槽 (Prguse_0390 物理凹槽区间 X: 60..159, Y: 126..154, 高度 29px, 几何中心 140.0)
        var remoteGoldBox = new Panel
        {
            Name = "RemoteGoldBox",
            Position = new Vector2(62, 126),
            Size = new Vector2(80, 28),
            MouseFilter = MouseFilterEnum.Ignore
        };
        remoteGoldBox.AddThemeStyleboxOverride("panel", new StyleBoxEmpty());
        _remotePanel.AddChild(remoteGoldBox);

        _lblRemoteGold = new Label
        {
            Name = "LblRemoteGold",
            Position = new Vector2(4, 0),
            Size = new Vector2(76, 28),
            Text = "0",
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Center
        };
        _lblRemoteGold.AddThemeFontSizeOverride("font_size", 11);
        _lblRemoteGold.AddThemeColorOverride("font_color", new Color(1.0f, 0.9f, 0.3f));
        remoteGoldBox.AddChild(_lblRemoteGold);

        // 2. 右侧/下方我方面板：我方面板 (Prguse_0389 236x175, IDA sub_481dd4 [0x485784..0x4857b5])
        _myPanel = new Panel
        {
            Name = "MyTradePanel",
            Position = new Vector2(0, PanelHeight - 15), // IDA sub_4915EC 0x491643: 我方 top=对方高度-15
            Size = new Vector2(MyWidth, PanelHeight),
            MouseFilter = MouseFilterEnum.Stop
        };
        var texMy = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0389.png");
        if (texMy != null)
        {
            _myPanel.AddThemeStyleboxOverride("panel", new StyleBoxTexture { Texture = texMy });
        }
        AddChild(_myPanel);

        // 我方名字框：官方原生顶部黑色牌匾凹槽 (平坦可用区 Left=58, Top=2, Width=110, Height=20)
        var myNameBox = new Panel
        {
            Name = "MyNameBox",
            Position = new Vector2(58, 2),
            Size = new Vector2(110, 20),
            MouseFilter = MouseFilterEnum.Ignore
        };
        myNameBox.AddThemeStyleboxOverride("panel", new StyleBoxEmpty());
        _myPanel.AddChild(myNameBox);

        _lblMyName = new Label
        {
            Name = "LblMyName",
            Text = "我",
            Position = Vector2.Zero,
            Size = new Vector2(110, 20),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        _lblMyName.AddThemeFontSizeOverride("font_size", 10);
        _lblMyName.AddThemeColorOverride("font_color", new Color(0.95f, 0.9f, 0.6f));
        myNameBox.AddChild(_lblMyName);

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
                CurrentSlotType = SlotButton.SlotType.Trade,
                SlotIndex = i,
                CustomMinimumSize = new Vector2(36, 33),
                Flat = true,
                Icon = null
            };
            _mySlots[i] = slot;
            myGrid.AddChild(slot);
        }

        // 我方金币图标与输入框 (金币图标 36x21 居中于圆环圆心 (29, 147.5) -> (11, 137)；黑色内胆精确咬合 (60, 127, 92x17))
        var myGoldIcon = new TextureRect
        {
            Name = "MyGoldIcon",
            Position = new Vector2(11, 137),
            Size = new Vector2(36, 21),
            Texture = texGold,
            MouseFilter = MouseFilterEnum.Stop
        };
        myGoldIcon.GuiInput += OnGoldGuiInput;
        _myPanel.AddChild(myGoldIcon);

        // 我方金币输入凹槽 (Prguse_0389 物理凹槽区间 X: 60..159, Y: 126..154, 高度 29px, 几何中心 140.0)
        var myGoldBox = new Panel
        {
            Name = "MyGoldBox",
            Position = new Vector2(62, 126),
            Size = new Vector2(90, 28),
            MouseFilter = MouseFilterEnum.Stop
        };
        myGoldBox.AddThemeStyleboxOverride("panel", new StyleBoxEmpty());
        myGoldBox.GuiInput += OnGoldGuiInput;
        _myPanel.AddChild(myGoldBox);

        _lblMyGold = new Label
        {
            Name = "LblMyGold",
            Position = new Vector2(4, 0),
            Size = new Vector2(86, 28),
            Text = "0",
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Center,
            MouseFilter = MouseFilterEnum.Ignore
        };
        _lblMyGold.AddThemeFontSizeOverride("font_size", 11);
        _lblMyGold.AddThemeColorOverride("font_color", new Color(1.0f, 0.9f, 0.3f));
        myGoldBox.AddChild(_lblMyGold);

        // 官方原生交易锁定按钮 (Prguse_0391 @ 155, 128, 44x16, IDA sub_481dd4 [0x485846..0x485867]: Left=155 (0x9B), Top=128 (0x80))
        // 未按下显示立体凸起 trade_btn_raised，按下或锁定态显示凹陷 Prguse_0391，消除凹陷黑斑感
        _texConfirmNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/trade_btn_raised.png");
        _texConfirmPressed = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0391.png");
        _btnConfirm = new TextureButton
        {
            Name = "BtnTradeLock",
            Position = new Vector2(155, 128),
            Size = new Vector2(44, 16),
            TextureNormal = _texConfirmNormal,
            TexturePressed = _texConfirmPressed
        };
        _btnConfirm.Pressed += OnConfirm;
        _myPanel.AddChild(_btnConfirm);

        // 官方原生交易取消/关闭按钮 (Prguse_0064 @ 220, 42, 16x23, IDA sub_481dd4 [0x48589a..0x4858cd])
        // Prguse_0389 底板在 (220, 42) 处已自带烘焙凸起红叉，TextureNormal 必须为 null，按下态显示凹陷 Prguse_0064
        var texCancel = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0064.png");
        _btnCancel = new TextureButton
        {
            Name = "BtnTradeClose",
            Position = new Vector2(220, 42),
            Size = new Vector2(16, 23),
            TextureNormal = null,
            TexturePressed = texCancel
        };
        _btnCancel.Pressed += OnCancel;
        _myPanel.AddChild(_btnCancel);
    }

    public void OpenTrade(string targetName)
    {
        ResetTrade();
        SetRemoteName(targetName);
        // 盛大官方 1.5 铁证 (IDA sub_4915EC 0x4915f5/0x491618): 强制复位右上角停靠 (464, 0)
        Position = new Vector2(464, 0);
        Visible = true;
    }

    public void CloseTrade()
    {
        OnCancel();
    }

    public void ResetTrade()
    {
        _myGold = 0;
        _remoteGold = 0;
        _isMyLocked = false;
        _isRemoteLocked = false;

        if (_lblMyGold != null) _lblMyGold.Text = "0";
        if (_lblRemoteGold != null) _lblRemoteGold.Text = "0";

        for (int i = 0; i < 10; i++)
        {
            _myItems[i] = null;
            _remoteItems[i] = null;
            if (_mySlots[i] != null) _mySlots[i].ItemTexture = null;
            if (_remoteSlots[i] != null) _remoteSlots[i].ItemTexture = null;
        }

        UpdateLockVisual();
    }

    public void SetRemoteName(string targetName)
    {
        if (_lblRemoteName != null)
        {
            _lblRemoteName.Text = string.IsNullOrEmpty(targetName) ? "交易对方" : targetName;
        }
    }

    public void SetMyName(string myName)
    {
        if (_lblMyName != null)
        {
            _lblMyName.Text = string.IsNullOrEmpty(myName) ? "我" : myName;
        }
    }

    private void BreakLocksOnDataChange()
    {
        if (_isMyLocked || _isRemoteLocked)
        {
            _isMyLocked = false;
            _isRemoteLocked = false;
            UpdateLockVisual();
            GD.Print("[TradePanelView] 交易物品或金币变动，官方防掉包机制触发：已强制重置双向锁定！");
        }
    }

    public void SetMyGold(int gold)
    {
        BreakLocksOnDataChange();
        _myGold = Math.Max(0, gold);
        if (_lblMyGold != null)
        {
            _lblMyGold.Text = $"{_myGold:N0}";
        }
    }

    public void SetRemoteGold(int gold)
    {
        BreakLocksOnDataChange();
        _remoteGold = Math.Max(0, gold);
        if (_lblRemoteGold != null)
        {
            _lblRemoteGold.Text = $"{_remoteGold:N0}";
        }
    }

    public void SetMyItem(int slot, string? itemName, Texture2D? icon = null)
    {
        if (slot < 0 || slot >= 10) return;
        BreakLocksOnDataChange();
        _myItems[slot] = itemName;
        if (_mySlots[slot] != null)
        {
            _mySlots[slot].ItemTexture = icon;
        }
    }

    public string? GetMyItem(int slot)
    {
        if (slot < 0 || slot >= 10) return null;
        return _myItems[slot];
    }

    public void ClearMyItem(int slot)
    {
        SetMyItem(slot, null, null);
    }

    public void SetRemoteItem(int slot, string? itemName, Texture2D? icon = null)
    {
        if (slot < 0 || slot >= 10) return;
        BreakLocksOnDataChange();
        _remoteItems[slot] = itemName;
        if (_remoteSlots[slot] != null)
        {
            _remoteSlots[slot].ItemTexture = icon;
        }
    }

    public string? GetRemoteItem(int slot)
    {
        if (slot < 0 || slot >= 10) return null;
        return _remoteItems[slot];
    }

    public void ClearRemoteItem(int slot)
    {
        SetRemoteItem(slot, null, null);
    }

    public void SetLocked(bool isMine, bool locked)
    {
        if (isMine)
        {
            _isMyLocked = locked;
        }
        else
        {
            _isRemoteLocked = locked;
        }
        UpdateLockVisual();
    }

    private void UpdateLockVisual()
    {
        if (_btnConfirm != null)
        {
            // 锁定态：显示凹陷图标且禁止重复点击
            _btnConfirm.TextureNormal = _isMyLocked ? _texConfirmPressed : _texConfirmNormal;
            _btnConfirm.Disabled = _isMyLocked;
        }
    }

    private void OnConfirm()
    {
        if (_isMyLocked) return;
        // 对齐服务端 20 tick（1000ms@20TPS）防连点：重复确认直接吞掉（Rust trade.rs sub_4DDC8C 0x4DDCF5）
        ulong now = Time.GetTicksMsec();
        if (now - _lastConfirmTickMsec < 1000)
        {
            return;
        }
        _lastConfirmTickMsec = now;
        SetLocked(true, true);
        EmitSignal(SignalName.EndDealConfirmed);
    }

    private void OnCancel()
    {
        Visible = false;
        ResetTrade();
        EmitSignal(SignalName.TradeCancelled);
    }

    /// <summary>
    /// 官方 1.5 交易面板点击修改金币 (IDA TFrmDlg_DDGoldClick 0x491fb0)
    /// </summary>
    private void OnGoldGuiInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mb && mb.Pressed && mb.ButtonIndex == MouseButton.Left)
        {
            if (_isMyLocked) return;
            EmitSignal(SignalName.DealGoldClicked);
        }
    }
}
