using System;
using System.Collections.Generic;
using Godot;
using Mir2Client.Utils;

namespace Mir2Client.UI;

/// <summary>
/// 盛大热血传奇 1.5 官方行会封号编辑子窗体 (DGuildEditRankDlg, 纯 C# 实现)
/// 严格对齐官方 Prguse_0181 (352x250)
/// </summary>
public partial class GuildEditRankDialog : Control, IManagedWindow
{
    [Signal]
    public delegate void RankSubmittedEventHandler(string memberName, string newRank);

    [Signal]
    public delegate void DialogClosedEventHandler();

    public const float PanelWidth = 352.0f;
    public const float PanelHeight = 250.0f;

    private Panel? _panel;
    private Label? _lblTargetMember;
    private LineEdit? _txtRank;
    private ItemList? _memberList;
    private TextureButton? _btnOk;
    private TextureButton? _btnClose;

    private string _currentMember = "";

    public string WindowName => "GuildEditRankDialog";
    public Official24DialogCompendium.DialogId DialogId => Official24DialogCompendium.DialogId.GuildPanel;
    public new bool IsVisible
    {
        get => Visible;
        set => Visible = value;
    }
    public (int x, int y, int width, int height) Bounds =>
        ((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

    public override void _Ready()
    {
        Name = "GuildEditRankDialog";
        Visible = false;
        ZIndex = 50;

        CustomMinimumSize = new Vector2(PanelWidth, PanelHeight);
        Size = new Vector2(PanelWidth, PanelHeight);
        // 在母板 Prguse_0180 (628x453) 内部水平垂直居中: (628-352)/2 = 138, (453-250)/2 = 101.5 -> 101, 全局坐标严格为 (224, 174)
        Position = new Vector2(138, 101);
        MouseFilter = MouseFilterEnum.Stop;

        _panel = new Panel
        {
            Name = "Panel",
            Size = new Vector2(PanelWidth, PanelHeight),
            MouseFilter = MouseFilterEnum.Pass
        };

        var texBg = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0181.png");
        if (texBg != null)
        {
            _panel.AddThemeStyleboxOverride("panel", new StyleBoxTexture { Texture = texBg });
        }
        AddChild(_panel);

        // 官方原生右上角关闭按钮 (Prguse_0064 @ 326, 4, 16x23)
        var texClose = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0064.png");
        _btnClose = new TextureButton
        {
            Name = "BtnClose",
            Position = new Vector2(326, 4),
            Size = new Vector2(16, 23),
            TextureNormal = null,
            TexturePressed = texClose
        };
        _btnClose.Pressed += OnClose;
        _panel.AddChild(_btnClose);

        // 官方原生右下角确定按钮 (Prguse_0062/0063 @ 258, 206, 76x33)
        var texOkNorm = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0062.png");
        var texOkPress = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0063.png");
        _btnOk = new TextureButton
        {
            Name = "BtnOk",
            Position = new Vector2(258, 206),
            Size = new Vector2(76, 33),
            TextureNormal = texOkNorm,
            TexturePressed = texOkPress
        };
        _btnOk.Pressed += OnSubmit;
        _panel.AddChild(_btnOk);

        // 目标成员名字展示框 (Left=58, Top=6, Width=32, Height=16, 对应方块凹槽)
        _lblTargetMember = new Label
        {
            Name = "LblTargetMember",
            Position = new Vector2(58, 6),
            Size = new Vector2(32, 16),
            Text = "",
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        _lblTargetMember.AddThemeFontSizeOverride("font_size", 10);
        _lblTargetMember.AddThemeColorOverride("font_color", new Color(1.0f, 0.9f, 0.4f));
        _panel.AddChild(_lblTargetMember);

        // “被确定封号”输入凹槽 (Left=178, Top=6, Width=142, Height=16)
        _txtRank = new LineEdit
        {
            Name = "TxtRank",
            Position = new Vector2(178, 6),
            Size = new Vector2(142, 16),
            PlaceholderText = "输入新封号...",
            MaxLength = 14,
            FocusMode = FocusModeEnum.Click
        };
        _txtRank.AddThemeFontSizeOverride("font_size", 10);
        _txtRank.AddThemeColorOverride("font_color", new Color(0.95f, 0.95f, 0.85f));
        _txtRank.AddThemeStyleboxOverride("normal", new StyleBoxEmpty());
        _txtRank.AddThemeStyleboxOverride("focus", new StyleBoxEmpty());
        _txtRank.TextSubmitted += (text) => OnSubmit();
        _panel.AddChild(_txtRank);

        // 中央成员选择列表 (Left=16, Top=52, Width=320, Height=148)
        _memberList = new ItemList
        {
            Name = "MemberList",
            Position = new Vector2(16, 52),
            Size = new Vector2(320, 148),
            SelectMode = ItemList.SelectModeEnum.Single
        };
        _memberList.AddThemeFontSizeOverride("font_size", 11);
        _memberList.AddThemeColorOverride("font_color", new Color(0.85f, 0.85f, 0.85f));
        _memberList.AddThemeColorOverride("font_selected_color", new Color(1.0f, 1.0f, 0.3f));
        _memberList.AddThemeStyleboxOverride("panel", new StyleBoxEmpty());
        _memberList.AddThemeStyleboxOverride("focus", new StyleBoxEmpty());
        _memberList.ItemSelected += OnMemberSelected;
        _panel.AddChild(_memberList);
    }

    public void Open(string memberName = "", string currentRank = "", IEnumerable<string>? members = null)
    {
        _currentMember = memberName;
        if (_lblTargetMember != null)
        {
            _lblTargetMember.Text = string.IsNullOrEmpty(memberName) ? "-" : (memberName.Length > 2 ? memberName[..2] : memberName);
            _lblTargetMember.TooltipText = memberName;
        }
        if (_txtRank != null)
        {
            _txtRank.Text = currentRank;
        }

        if (members != null)
        {
            SetMembers(members);
        }

        Visible = true;
        _txtRank?.GrabFocus();
    }

    public void SetMembers(IEnumerable<string> members)
    {
        if (_memberList == null) return;
        _memberList.Clear();
        int idx = 0;
        int selectedIdx = -1;
        foreach (var m in members)
        {
            _memberList.AddItem(m);
            if (m == _currentMember) selectedIdx = idx;
            idx++;
        }
        if (selectedIdx >= 0)
        {
            _memberList.Select(selectedIdx);
        }
    }

    private void OnMemberSelected(long index)
    {
        if (_memberList == null) return;
        string name = _memberList.GetItemText((int)index);
        _currentMember = name;
        if (_lblTargetMember != null)
        {
            _lblTargetMember.Text = name.Length > 2 ? name[..2] : name;
            _lblTargetMember.TooltipText = name;
        }
    }

    public void OnClose()
    {
        Visible = false;
        EmitSignal(SignalName.DialogClosed);
    }

    private void OnSubmit()
    {
        string newRank = _txtRank?.Text.Trim() ?? "";
        if (!string.IsNullOrEmpty(_currentMember))
        {
            EmitSignal(SignalName.RankSubmitted, _currentMember, newRank);
        }
        Visible = false;
    }
}
