using System;
using System.Collections.Generic;
using Godot;
using Mir2Client.Utils;

namespace Mir2Client.UI;

/// <summary>
/// 盛大热血传奇 1.5 官方组队面板 (DGroupDlg, 纯 C# 实现)
/// 严格对齐官方 IDA sub_4855b2 (Prguse_0120 276x242)
/// </summary>
public partial class GroupPanelView : Control, IManagedWindow
{
    [Signal]
    public delegate void CreateGroupRequestedEventHandler(string targetName);

    [Signal]
    public delegate void AddMemberRequestedEventHandler(string targetName);

    [Signal]
    public delegate void DelMemberRequestedEventHandler(string targetName);

    [Signal]
    public delegate void GroupModeToggledEventHandler(bool enabled);

    [Signal]
    public delegate void PanelClosedEventHandler();

    public const float PanelWidth = 276.0f;
    public const float PanelHeight = 242.0f;

    private Panel? _panel;
    private Control? _membersContainer;
    private bool _groupModeEnabled = true;

    public string WindowName => "GroupPanelView";
    public new bool IsVisible
    {
        get => Visible;
        set => Visible = value;
    }
    public (int x, int y, int width, int height) Bounds =>
        ((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

    public override void _Ready()
    {
        Visible = false;
        Size = new Vector2(PanelWidth, PanelHeight);
        Position = new Vector2(262, 179);

        _panel = new Panel
        {
            Size = new Vector2(PanelWidth, PanelHeight),
            MouseFilter = MouseFilterEnum.Stop
        };

        var texBg = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0120.png");
        if (texBg != null)
        {
            _panel.AddThemeStyleboxOverride("panel", new StyleBoxTexture { Texture = texBg });
        }
        AddChild(_panel);

        var texClose = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0064.png");
        var btnClose = new TextureButton
        {
            Name = "BtnGroupClose",
            Position = new Vector2(260, 0),
            Size = new Vector2(16, 23),
            TextureNormal = null,
            TexturePressed = texClose
        };
        btnClose.Pressed += OnClose;
        _panel.AddChild(btnClose);

        // 官方 1.5 允许组队勾选框 (IDA sub_481dd4 [0x485670..0x485696] & sub_491008 原生机制实证):
        // 1. 底板 Prguse_0120 在 (20, 18) 处自带蓝色空凹槽;
        // 2. 勾选态 (Checked) 绘制 Prguse_0122 (打勾实心金属贴图);
        // 3. 按下态 (Pressed) 绘制 Prguse_0121 (凹陷过渡贴图, sub_491008: imgIdx - 1);
        // 4. 未勾选态 (Unchecked) 为 null, 直接透出底板原生凹槽.
        var texCheckChecked = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0122.png");
        var texCheckPressed = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0121.png");
        var btnAllowGroup = new TextureButton
        {
            Name = "BtnAllowGroup",
            Position = new Vector2(20, 18),
            Size = new Vector2(20, 19),
            TextureNormal = _groupModeEnabled ? texCheckChecked : null,
            TexturePressed = texCheckPressed
        };
        btnAllowGroup.Pressed += () =>
        {
            _groupModeEnabled = !_groupModeEnabled;
            btnAllowGroup.TextureNormal = _groupModeEnabled ? texCheckChecked : null;
            EmitSignal(SignalName.GroupModeToggled, _groupModeEnabled);
        };
        _panel.AddChild(btnAllowGroup);

        // 官方 1.5 组队成员列表容器 (IDA sub_490DFC 绝对坐标绘制: 队长 @ (28, 80); 队员双列 @ (28/128, 96 + row*16))
        _membersContainer = new Control
        {
            Name = "MembersContainer",
            Position = Vector2.Zero,
            Size = new Vector2(PanelWidth, PanelHeight),
            MouseFilter = MouseFilterEnum.Ignore
        };
        _panel.AddChild(_membersContainer);

        // 官方 1.5 原生底栏 3 按钮交互热区 (底板 Prguse_0120 已精细烘焙 新建/添加/删除 金属文字凹槽)
        // 采用精准无边框透明热区咬合:
        // - 新建: (22, 203, 72, 19) -> Prguse_0123
        // - 添加: (97, 203, 72, 19) -> Prguse_0124
        // - 删除: (172, 203, 72, 19) -> Prguse_0125
        var btnCreate = new TextureButton
        {
            Name = "BtnGroupCreate",
            TextureNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0123.png"),
            Position = new Vector2(22, 203),
            Size = new Vector2(72, 19),
            FocusMode = FocusModeEnum.None
        };
        btnCreate.Pressed += () => EmitSignal(SignalName.CreateGroupRequested, "我");
        _panel.AddChild(btnCreate);

        var btnAdd = new TextureButton
        {
            Name = "BtnGroupAdd",
            TextureNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0124.png"),
            Position = new Vector2(97, 203),
            Size = new Vector2(72, 19),
            FocusMode = FocusModeEnum.None
        };
        btnAdd.Pressed += () => EmitSignal(SignalName.AddMemberRequested, "新队友");
        _panel.AddChild(btnAdd);

        var btnLeave = new TextureButton
        {
            Name = "BtnGroupLeave",
            TextureNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0125.png"),
            Position = new Vector2(172, 203),
            Size = new Vector2(72, 19),
            FocusMode = FocusModeEnum.None
        };
        btnLeave.Pressed += () => EmitSignal(SignalName.DelMemberRequested, "我");
        _panel.AddChild(btnLeave);
    }

    public void TogglePanel()
    {
        Visible = !Visible;
    }

    /// <summary>
    /// 盛大官方 1.5 组队成员绘制 (IDA sub_490DFC 绝对坐标实证):
    /// 1. 队长 (索引 0): (Left + 28, Top + 80), 银灰文字 0xC0C0C0;
    /// 2. 队员 (索引 1..10): 双列排布, 列宽 100px ((i-1)%2), 行距 16px ((i-1)/2), 起始 (28/128, 96), 银灰文字 0xC0C0C0.
    /// </summary>
    public void SetMembers(string leader, IList<string>? members = null)
    {
        if (_membersContainer == null) return;
        foreach (Node child in _membersContainer.GetChildren())
        {
            child.QueueFree();
        }

        if (string.IsNullOrEmpty(leader)) return;

        var colorGray = new Color(0.753f, 0.753f, 0.753f); // 0xC0C0C0

        // 队长名字绘制
        var lblLeader = new Label
        {
            Name = "LblLeader",
            Position = new Vector2(28, 80),
            Text = leader
        };
        lblLeader.AddThemeFontSizeOverride("font_size", 12);
        lblLeader.AddThemeColorOverride("font_color", colorGray);
        _membersContainer.AddChild(lblLeader);

        if (members == null) return;

        // 队员名字双列绘制
        for (int i = 0; i < members.Count && i < 10; i++)
        {
            int col = i % 2;
            int row = i / 2;
            var lblMember = new Label
            {
                Name = $"LblMember_{i}",
                Position = new Vector2(28 + col * 100, 96 + row * 16),
                Text = members[i]
            };
            lblMember.AddThemeFontSizeOverride("font_size", 12);
            lblMember.AddThemeColorOverride("font_color", colorGray);
            _membersContainer.AddChild(lblMember);
        }
    }

    private void OnClose()
    {
        Visible = false;
        EmitSignal(SignalName.PanelClosed);
    }
}
