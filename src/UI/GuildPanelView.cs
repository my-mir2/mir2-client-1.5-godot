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
    private LineEdit? _searchEdit;

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

        // 官方 1.5 原生行会顶部成员查找功能 (IDA sub_481dd4 [0x485a20..0x485a68])
        // 1. 查找按钮热区 (273, 5, 27x14, 对应左侧小凹槽)
        var btnSearch = new Button
        {
            Name = "BtnGuildSearch",
            Position = new Vector2(273, 5),
            Size = new Vector2(27, 14),
            Text = "查找",
            Flat = true,
            FocusMode = FocusModeEnum.None
        };
        btnSearch.AddThemeFontSizeOverride("font_size", 9);
        btnSearch.AddThemeColorOverride("font_color", new Color(0.9f, 0.85f, 0.5f));
        btnSearch.Pressed += () =>
        {
            if (_searchEdit != null && !string.IsNullOrWhiteSpace(_searchEdit.Text))
            {
                ShowMemberList();
            }
        };
        // IDA sub_481dd4 无 (273,5)/(306,5) 控件,查找框为非官方,隐藏
        btnSearch.Visible = false;
        _panel.AddChild(btnSearch);

        // 2. 搜索输入框 (306, 5, 146x14, 对应右侧黑色凹槽内胆)
        _searchEdit = new LineEdit
        {
            Name = "GuildSearchEdit",
            Position = new Vector2(306, 5),
            Size = new Vector2(146, 14),
            PlaceholderText = "输入成员名字...",
            FocusMode = FocusModeEnum.Click
        };
        _searchEdit.AddThemeFontSizeOverride("font_size", 9);
        _searchEdit.AddThemeColorOverride("font_color", new Color(1.0f, 1.0f, 0.8f));
        _searchEdit.AddThemeStyleboxOverride("normal", new StyleBoxEmpty());
        _searchEdit.AddThemeStyleboxOverride("focus", new StyleBoxEmpty());
        _searchEdit.TextSubmitted += (text) =>
        {
            if (!string.IsNullOrWhiteSpace(text)) ShowMemberList();
        };
        _searchEdit.Visible = false;
        _panel.AddChild(_searchEdit);

        // 中央内容区容器 (高光边框内胆 Left=15, Top=35, Width=570, Height=368)
        // 1. 主页/公告视图
        _homeView = new Control
        {
            Name = "GuildHomeView",
            Position = new Vector2(15, 35),
            Size = new Vector2(570, 368),
            MouseFilter = MouseFilterEnum.Pass
        };
        _panel.AddChild(_homeView);

        var lblGuildTitle = new Label
        {
            Name = "LblGuildTitle",
            Position = new Vector2(12, 10),
            Size = new Vector2(546, 24),
            Text = "",
            VerticalAlignment = VerticalAlignment.Center
        };
        lblGuildTitle.AddThemeFontSizeOverride("font_size", 12);
        lblGuildTitle.AddThemeColorOverride("font_color", new Color(1.0f, 0.85f, 0.4f));
        _homeView.AddChild(lblGuildTitle);

        _noticeText = new RichTextLabel
        {
            Name = "GuildNoticeText",
            Position = new Vector2(12, 42),
            Size = new Vector2(546, 315),
            BbcodeEnabled = true,
            Text = ""
        };
        _noticeText.AddThemeFontSizeOverride("normal_font_size", 11);
        _homeView.AddChild(_noticeText);

        // 2. 成员列表视图
        _memberListView = new Control
        {
            Name = "GuildMemberListView",
            Position = new Vector2(15, 35),
            Size = new Vector2(570, 368),
            MouseFilter = MouseFilterEnum.Pass,
            Visible = false
        };
        _panel.AddChild(_memberListView);

        var lblListHeader = new Label
        {
            Name = "LblListHeader",
            Position = new Vector2(12, 10),
            Size = new Vector2(546, 20),
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
            Size = new Vector2(546, 320)
        };
        _memberListContainer.AddThemeConstantOverride("separation", 4);
        _memberListView.AddChild(_memberListContainer);

        // 初始填充默认成员
        // 默认成员为非官方演示数据,已移除
        // 官方 sub_493F58 (0x49401d):灰色 0xC0C0C0 文本 @(18,291),取自窗体字段 +2484;默认提示串见 0x4937E4
        var lblHint = new Label { Name = "LblGuildHint", Position = new Vector2(18, 291 - 3), Text = "请点击 [名单]按钮以调出行会成员信息。" };
        lblHint.AddThemeFontSizeOverride("font_size", 12);
        lblHint.AddThemeColorOverride("font_color", new Color(0.753f, 0.753f, 0.753f));
        _panel.AddChild(lblHint);

        // 官方 1.5 行会底栏 6 大原生操作按钮 (Prguse_0182~0203 64x16 偶数Normal 奇数Pressed):
        // 列 1:
        // - 主页 (198/199 @ 13, 411)
        // - 成员列表 (200/201 @ 13, 429)
        var btnHome = new TextureButton
        {
            Position = new Vector2(13, 411),
            Size = new Vector2(64, 16),
            TextureNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0198.png"),
            TexturePressed = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0199.png")
        };
        btnHome.Pressed += ShowHome;
        _panel.AddChild(btnHome);

        var btnMembers = new TextureButton
        {
            Position = new Vector2(13, 429),
            Size = new Vector2(64, 16),
            TextureNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0200.png"),
            TexturePressed = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0201.png")
        };
        btnMembers.Pressed += ShowMemberList;
        _panel.AddChild(btnMembers);

        // 列 2:
        // - 聊天记录 (190/191 @ 94, 429) -> 注意行会日常聊天/大事记
        var btnNotice = new TextureButton
        {
            Position = new Vector2(94, 429),
            Size = new Vector2(64, 16),
            TextureNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0190.png"),
            TexturePressed = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0191.png")
        };
        _panel.AddChild(btnNotice);

        // 列 3:
        // - 添加成员 (182/183 @ 243, 411)
        // - 删除成员 (192/193 @ 243, 429)
        var btnAddMember = new TextureButton
        {
            Position = new Vector2(243, 411),
            Size = new Vector2(64, 16),
            TextureNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0182.png"),
            TexturePressed = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0183.png")
        };
        _panel.AddChild(btnAddMember);

        var btnDelMember = new TextureButton
        {
            Position = new Vector2(243, 429),
            Size = new Vector2(64, 16),
            TextureNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0192.png"),
            TexturePressed = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0193.png")
        };
        _panel.AddChild(btnDelMember);

        // 列 4:
        // - 编辑公告 (196/197 @ 325, 411)
        var btnEditNotice = new TextureButton
        {
            Position = new Vector2(325, 411),
            Size = new Vector2(64, 16),
            TextureNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0196.png"),
            TexturePressed = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0197.png")
        };
        _panel.AddChild(btnEditNotice);
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
