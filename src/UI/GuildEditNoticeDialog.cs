using System;
using Godot;
using Mir2Client.Utils;

namespace Mir2Client.UI;

/// <summary>
/// 盛大热血传奇 1.5 官方行会公告编辑子窗体 (DGuildEditNoticeDlg, 纯 C# 实现)
/// 严格对齐官方 IDA sub_481dd4 [0x485e4d..0x485ec6] (Prguse_0204 608x327)
/// </summary>
public partial class GuildEditNoticeDialog : Control, IManagedWindow
{
    [Signal]
    public delegate void NoticeSubmittedEventHandler(string newNotice);

    [Signal]
    public delegate void DialogClosedEventHandler();

    public const float PanelWidth = 608.0f;
    public const float PanelHeight = 327.0f;

    private Panel? _panel;
    private TextEdit? _txtNotice;
    private TextureButton? _btnOk;
    private TextureButton? _btnClose;

    public string WindowName => "GuildEditNoticeDialog";
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
        Name = "GuildEditNoticeDialog";
        Visible = false;
        ZIndex = 50;

        CustomMinimumSize = new Vector2(PanelWidth, PanelHeight);
        Size = new Vector2(PanelWidth, PanelHeight);
        // 在母板 Prguse_0180 (628x453) 内部水平垂直居中: (628-608)/2 = 10, (453-327)/2 = 63, 全局坐标严格为 (96, 136)
        Position = new Vector2(10, 63);
        MouseFilter = MouseFilterEnum.Stop;

        _panel = new Panel
        {
            Name = "Panel",
            Size = new Vector2(PanelWidth, PanelHeight),
            MouseFilter = MouseFilterEnum.Pass
        };

        var texBg = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0204.png");
        if (texBg != null)
        {
            _panel.AddThemeStyleboxOverride("panel", new StyleBoxTexture { Texture = texBg });
        }
        AddChild(_panel);

        // 官方原生右上角关闭按钮 (Prguse_0064 @ 584, 6, 16x23, IDA sub_481dd4 0x485ec6)
        var texClose = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0064.png");
        _btnClose = new TextureButton
        {
            Name = "BtnClose",
            Position = new Vector2(584, 6),
            Size = new Vector2(16, 23),
            TextureNormal = null,
            TexturePressed = texClose
        };
        _btnClose.Pressed += OnClose;
        _panel.AddChild(_btnClose);

        // 官方原生右下角确定按钮 (Prguse_0062/0063 @ 514, 287, 76x33, IDA sub_481dd4 0x485e80)
        var texOkNorm = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0062.png");
        var texOkPress = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0063.png");
        _btnOk = new TextureButton
        {
            Name = "BtnOk",
            Position = new Vector2(514, 287),
            Size = new Vector2(76, 33),
            TextureNormal = texOkNorm,
            TexturePressed = texOkPress
        };
        _btnOk.Pressed += OnSubmit;
        _panel.AddChild(_btnOk);

        // 中央大文本编辑区域 (Left=22, Top=42, Width=564, Height=235)
        _txtNotice = new TextEdit
        {
            Name = "TxtNotice",
            Position = new Vector2(22, 42),
            Size = new Vector2(564, 235),
            WrapMode = TextEdit.LineWrappingMode.Boundary,
            FocusMode = FocusModeEnum.Click
        };
        _txtNotice.AddThemeFontSizeOverride("font_size", 12);
        _txtNotice.AddThemeColorOverride("font_color", new Color(0.92f, 0.92f, 0.88f));
        _txtNotice.AddThemeColorOverride("selection_color", new Color(0.2f, 0.4f, 0.7f, 0.6f));
        _txtNotice.AddThemeStyleboxOverride("normal", new StyleBoxEmpty());
        _txtNotice.AddThemeStyleboxOverride("focus", new StyleBoxEmpty());
        _panel.AddChild(_txtNotice);
    }

    public void Open(string initialNotice = "")
    {
        if (_txtNotice != null)
        {
            _txtNotice.Text = initialNotice;
        }
        Visible = true;
        _txtNotice?.GrabFocus();
    }

    public void OnClose()
    {
        Visible = false;
        EmitSignal(SignalName.DialogClosed);
    }

    private void OnSubmit()
    {
        string text = _txtNotice?.Text.Trim() ?? "";
        EmitSignal(SignalName.NoticeSubmitted, text);
        Visible = false;
    }
}
