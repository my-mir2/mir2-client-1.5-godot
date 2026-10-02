using System;
using System.Collections.Generic;
using Godot;
using Mir2Client.Utils;

namespace Mir2Client.UI;

/// <summary>
/// 盛大热血传奇 1.5 官方行会面板 (TGuildDlg, 纯 C# 实现)
/// 严格对齐官方 Prguse_0180 (628x453)
/// </summary>
public partial class GuildPanelView : Control, IManagedWindow
{
    [Signal]
    public delegate void HomeRequestedEventHandler();

    [Signal]
    public delegate void MemberListRequestedEventHandler();

    [Signal]
    public delegate void PanelClosedEventHandler();

    public const float PanelWidth = 628.0f;
    public const float PanelHeight = 453.0f;

    private Panel? _panel;
    private Control? _homeView;
    private Control? _memberListView;
    private RichTextLabel? _noticeText;
    private VBoxContainer? _memberListContainer;

    public string WindowName => "GuildPanelView";
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
        Position = new Vector2(86, 73);

        _panel = new Panel
        {
            Size = new Vector2(PanelWidth, PanelHeight),
            MouseFilter = MouseFilterEnum.Stop
        };

        var texBg = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0180.png");
        if (texBg != null)
        {
            _panel.AddThemeStyleboxOverride("panel", new StyleBoxTexture { Texture = texBg });
        }
        AddChild(_panel);

        // 官方原生行会关闭按钮 (Prguse_0064 @ 584, 6, IDA sub_481dd4 [0x485a6c..0x485a9f])
        var texClose = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0064.png");
        var btnClose = new TextureButton
        {
            Position = new Vector2(584, 6),
            Size = new Vector2(16, 23),
            TextureNormal = null,
            TexturePressed = texClose
        };
        btnClose.Pressed += OnClose;
        _panel.AddChild(btnClose);

        // 中央内容区容器 (Left=20, Top=40, Width=566, Height=355)
        // 1. 主页/公告视图
        _homeView = new Control
        {
            Name = "GuildHomeView",
            Position = new Vector2(20, 40),
            Size = new Vector2(566, 355),
            MouseFilter = MouseFilterEnum.Pass
        };
        _panel.AddChild(_homeView);

        var lblGuildTitle = new Label
        {
            Name = "LblGuildTitle",
            Position = new Vector2(12, 10),
            Size = new Vector2(540, 24),
            Text = "【沙巴克行会】 掌门人：无涯   成员总数：88 人",
            VerticalAlignment = VerticalAlignment.Center
        };
        lblGuildTitle.AddThemeFontSizeOverride("font_size", 12);
        lblGuildTitle.AddThemeColorOverride("font_color", new Color(1.0f, 0.85f, 0.4f));
        _homeView.AddChild(lblGuildTitle);

        _noticeText = new RichTextLabel
        {
            Name = "GuildNoticeText",
            Position = new Vector2(12, 42),
            Size = new Vector2(540, 300),
            BbcodeEnabled = true,
            Text = "[color=#c0b090]行会公告：[/color]\n\n" +
                   "[color=#ffff88]1. 本周六晚八点准时集结沙巴克攻城战，请所有兄弟备足疗伤药与随机传送卷！[/color]\n" +
                   "[color=#a0d8ef]2. 掌门与各位堂主已在祖玛教主大厅布置伏击，行会成员享有优先补给权。[/color]\n" +
                   "[color=#88ff88]3. 严禁在本会练级区域恶意抢怪PK，违者严惩不贷！[/color]\n\n" +
                   "[color=#d0c0a0]行会同盟：邪风军团、昔日同盟\n敌对行会：不败神话[/color]"
        };
        _noticeText.AddThemeFontSizeOverride("normal_font_size", 11);
        _homeView.AddChild(_noticeText);

        // 2. 成员列表视图
        _memberListView = new Control
        {
            Name = "GuildMemberListView",
            Position = new Vector2(20, 40),
            Size = new Vector2(566, 355),
            MouseFilter = MouseFilterEnum.Pass,
            Visible = false
        };
        _panel.AddChild(_memberListView);

        var lblListHeader = new Label
        {
            Name = "LblListHeader",
            Position = new Vector2(12, 10),
            Size = new Vector2(540, 20),
            Text = "职务         角色名         职业       等级       在线状态",
            VerticalAlignment = VerticalAlignment.Center
        };
        lblListHeader.AddThemeFontSizeOverride("font_size", 11);
        lblListHeader.AddThemeColorOverride("font_color", new Color(0.9f, 0.8f, 0.3f));
        _memberListView.AddChild(lblListHeader);

        _memberListContainer = new VBoxContainer
        {
            Name = "MemberListContainer",
            Position = new Vector2(12, 36),
            Size = new Vector2(540, 310)
        };
        _memberListContainer.AddThemeConstantOverride("separation", 4);
        _memberListView.AddChild(_memberListContainer);

        // 初始填充默认成员
        AddMemberRow("掌门", "无涯", "道士", 45, true);
        AddMemberRow("副掌门", "风之影", "战士", 44, true);
        AddMemberRow("堂主", "法神天下", "法师", 43, true);
        AddMemberRow("精英", "烈火狂人", "战士", 42, false);
        AddMemberRow("会员", "碧海潮生", "法师", 40, true);

        // 行会主页/公告 (0xC6 = 198 -> Prguse_0198 @ 13, 411, IDA sub_481dd4 [0x485ab2..0x485ae5])
        var texHome = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0198.png");
        var btnHome = new TextureButton
        {
            Position = new Vector2(13, 411),
            Size = new Vector2(64, 16),
            TextureNormal = texHome
        };
        btnHome.Pressed += ShowHome;
        _panel.AddChild(btnHome);

        // 行会成员列表 (0xC8 = 200 -> Prguse_0200 @ 13, 429, IDA sub_481dd4 [0x485af8..0x485b2b])
        var texMembers = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0200.png");
        var btnMembers = new TextureButton
        {
            Position = new Vector2(13, 429),
            Size = new Vector2(64, 16),
            TextureNormal = texMembers
        };
        btnMembers.Pressed += ShowMemberList;
        _panel.AddChild(btnMembers);

        // 掌门信息/修改公告 (0xBE = 190 -> Prguse_0190 @ 94, 429, IDA sub_481dd4 [0x485b3e..0x485b71])
        var texNotice = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0190.png");
        var btnNotice = new TextureButton
        {
            Position = new Vector2(94, 429),
            Size = new Vector2(64, 16),
            TextureNormal = texNotice
        };
        _panel.AddChild(btnNotice);

        // 联盟信息 (0xB6 = 182 -> Prguse_0182 @ 243, 411, IDA sub_481dd4 [0x485b84..0x485bb7])
        var texAlly = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0182.png");
        var btnAlly = new TextureButton
        {
            Position = new Vector2(243, 411),
            Size = new Vector2(64, 16),
            TextureNormal = texAlly
        };
        _panel.AddChild(btnAlly);

        // 战事状态 (0xC0 = 192 -> Prguse_0192 @ 243, 429, IDA sub_481dd4 [0x485bca..0x485bfd])
        var texWar = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0192.png");
        var btnWar = new TextureButton
        {
            Position = new Vector2(243, 429),
            Size = new Vector2(64, 16),
            TextureNormal = texWar
        };
        _panel.AddChild(btnWar);

        // 退会/离开 (0xC4 = 196 -> Prguse_0196 @ 325, 411, IDA sub_481dd4 [0x485c10..0x485c43])
        var texLeave = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0196.png");
        var btnLeave = new TextureButton
        {
            Position = new Vector2(325, 411),
            Size = new Vector2(64, 16),
            TextureNormal = texLeave
        };
        btnLeave.Pressed += OnClose;
        _panel.AddChild(btnLeave);
    }

    private void AddMemberRow(string role, string name, string job, int level, bool online)
    {
        if (_memberListContainer == null) return;
        var row = new Label
        {
            Text = $"{role,-10} {name,-14} {job,-8} {level,-8} {(online ? "在线" : "离线"),-8}",
            Size = new Vector2(540, 18)
        };
        row.AddThemeFontSizeOverride("font_size", 10);
        row.AddThemeColorOverride("font_color", online ? new Color(0.9f, 0.9f, 0.9f) : new Color(0.55f, 0.55f, 0.55f));
        _memberListContainer.AddChild(row);
    }

    public void ShowHome()
    {
        if (_homeView != null) _homeView.Visible = true;
        if (_memberListView != null) _memberListView.Visible = false;
        EmitSignal(SignalName.HomeRequested);
    }

    public void ShowMemberList()
    {
        if (_homeView != null) _homeView.Visible = false;
        if (_memberListView != null) _memberListView.Visible = true;
        EmitSignal(SignalName.MemberListRequested);
    }

    public void TogglePanel()
    {
        Visible = !Visible;
        if (Visible)
        {
            ShowHome();
        }
    }

    private void OnClose()
    {
        Visible = false;
        EmitSignal(SignalName.PanelClosed);
    }
}
