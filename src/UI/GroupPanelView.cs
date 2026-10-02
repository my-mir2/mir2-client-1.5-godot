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

        // 官方 1.5 允许组队勾选框 (IDA sub_481dd4 [0x485670..0x485696]: Left=20, Top=18, 嵌入底板凹槽)
        var texCheckUnchecked = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0121.png");
        var texCheckChecked = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0122.png");
        var btnAllowGroup = new TextureButton
        {
            Name = "BtnAllowGroup",
            Position = new Vector2(20, 18),
            Size = new Vector2(20, 19),
            TextureNormal = _groupModeEnabled ? texCheckChecked : texCheckUnchecked
        };
        btnAllowGroup.Pressed += () =>
        {
            _groupModeEnabled = !_groupModeEnabled;
            btnAllowGroup.TextureNormal = _groupModeEnabled ? texCheckChecked : texCheckUnchecked;
            EmitSignal(SignalName.GroupModeToggled, _groupModeEnabled);
        };
        _panel.AddChild(btnAllowGroup);

        // 官方 1.5 组队成员列表容器 (Prguse_0120 内部黑区: Left=25, Top=76, Width=226, Height: 115)
        var memberList = new VBoxContainer
        {
            Name = "MemberListContainer",
            Position = new Vector2(25, 76),
            Size = new Vector2(226, 115)
        };
        _panel.AddChild(memberList);

        // 创建编组 (0x7B = 123 -> Prguse_0123.png @ 22, 203)
        var texCreate = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0123.png");
        var btnCreate = new TextureButton
        {
            Name = "BtnGroupCreate",
            Position = new Vector2(22, 203),
            Size = new Vector2(72, 19),
            TextureNormal = texCreate
        };
        btnCreate.Pressed += () => EmitSignal(SignalName.CreateGroupRequested, "我");
        _panel.AddChild(btnCreate);

        // 允许组队模式 (0x7C = 124 -> Prguse_0124.png @ 97, 203)
        var texMode = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0124.png");
        var btnMode = new TextureButton
        {
            Name = "BtnGroupMode",
            Position = new Vector2(97, 203),
            Size = new Vector2(72, 19),
            TextureNormal = texMode
        };
        btnMode.Pressed += () =>
        {
            _groupModeEnabled = !_groupModeEnabled;
            btnAllowGroup.TextureNormal = _groupModeEnabled ? texCheckChecked : texCheckUnchecked;
            EmitSignal(SignalName.GroupModeToggled, _groupModeEnabled);
        };
        _panel.AddChild(btnMode);

        // 退出编组 (0x7D = 125 -> Prguse_0125.png @ 172, 203)
        var texLeave = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0125.png");
        var btnLeave = new TextureButton
        {
            Name = "BtnGroupLeave",
            Position = new Vector2(172, 203),
            Size = new Vector2(72, 19),
            TextureNormal = texLeave
        };
        btnLeave.Pressed += () => EmitSignal(SignalName.DelMemberRequested, "我");
        _panel.AddChild(btnLeave);
    }

    public void TogglePanel()
    {
        Visible = !Visible;
    }

    private void OnClose()
    {
        Visible = false;
        EmitSignal(SignalName.PanelClosed);
    }
}
