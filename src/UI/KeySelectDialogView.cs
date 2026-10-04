using System;
using Godot;
using Mir2Client.Utils;

namespace Mir2Client.UI;

/// <summary>
/// 盛大热血传奇 1.5 官方技能快捷键设置面板 (DKeySelDlg, 纯 C# 实现)
/// 严格对齐官方 IDA 0x4907D8 (Prguse.wil 229, F1~F8 绑定)
/// </summary>
public partial class KeySelectDialogView : Control, IManagedWindow
{
    [Signal]
    public delegate void KeyBindingConfirmedEventHandler(int magicId, int keyNum);

    [Signal]
    public delegate void DialogClosedEventHandler();

    public const float PanelWidth = 376.0f;
    public const float PanelHeight = 179.0f;

    private static readonly float[] FKeysX = [34.0f, 66.0f, 98.0f, 130.0f, 171.0f, 203.0f, 235.0f, 267.0f];

    private Panel? _panel;
    private Label? _lblTitle;
    private TextureRect? _iconRect;
    private int _currentMagicId = 0;
    private int _selectedKey = 0;

    public string WindowName => "KeySelectDialogView";
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
        ZIndex = 60;
        CustomMinimumSize = new Vector2(PanelWidth, PanelHeight);
        Size = new Vector2(PanelWidth, PanelHeight);
        Position = new Vector2((800.0f - PanelWidth) / 2.0f, (600.0f - PanelHeight) / 2.0f);

        _panel = new Panel
        {
            Size = new Vector2(PanelWidth, PanelHeight),
            MouseFilter = MouseFilterEnum.Stop
        };

        var texBg = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0229.png");
        if (texBg != null)
        {
            var sb = new StyleBoxTexture { Texture = texBg };
            _panel.AddThemeStyleboxOverride("panel", sb);
        }
        AddChild(_panel);

        // 官方原生技能图标槽 (IDA 0x4852b6: Left=51, Top=27, 内胆黑色雕刻槽 28x28)
        _iconRect = new TextureRect
        {
            Name = "KeySelectSkillIcon",
            Position = new Vector2(51, 27),
            Size = new Vector2(28, 28),
            ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize,
            StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered,
            MouseFilter = MouseFilterEnum.Ignore
        };
        _panel.AddChild(_iconRect);

        _lblTitle = new Label
        {
            Position = new Vector2(92, 34),
            Size = new Vector2(240, 20),
            Text = "设置快捷键",
            VerticalAlignment = VerticalAlignment.Center
        };
        _lblTitle.AddThemeFontSizeOverride("font_size", 11);
        _lblTitle.AddThemeColorOverride("font_color", new Color(1.0f, 0.95f, 0.7f));
        _panel.AddChild(_lblTitle);

        // 官方原生右上角关闭按钮 (底板 Prguse_0229 无烘焙红叉，默认显示凸起态，按下显示凹陷态)
        var texCloseNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/btn_close_normal.png");
        var texClose = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0064.png");
        var btnClose = new TextureButton
        {
            Name = "BtnKeySelClose",
            Position = new Vector2(352, 6),
            Size = new Vector2(16, 23),
            TextureNormal = texCloseNormal,
            TexturePressed = texClose
        };
        btnClose.Pressed += OnClose;
        _panel.AddChild(btnClose);

        // 官方原生 F1 - F8 按键 (Prguse_0232, 0234, 0236, 0238, 0240, 0242, 0244, 0246 @ Top=83, 32x30)
        for (int i = 0; i < 8; i++)
        {
            int keyNum = i + 1;
            int texIdxNormal = 232 + i * 2;
            int texIdxPressed = texIdxNormal + 1;
            var texNormal = ResPath.LoadImageTexture($"resources/wil/png/Prguse/Prguse_{texIdxNormal:D4}.png");
            var texPressed = ResPath.LoadImageTexture($"resources/wil/png/Prguse/Prguse_{texIdxPressed:D4}.png");

            var btn = new TextureButton
            {
                Name = $"BtnF{keyNum}",
                Position = new Vector2(FKeysX[i], 83),
                Size = new Vector2(32, 30),
                TextureNormal = texNormal,
                TexturePressed = texPressed
            };
            btn.Pressed += () => OnKeySelected(keyNum);
            _panel.AddChild(btn);
        }

        // 官方原生 None/清除 按键 (Prguse_0230 凸起 / 0231 凹陷 @ 299, 83, 40x30)
        var texNoneNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0230.png");
        var texNonePressed = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0231.png");
        var btnNone = new TextureButton
        {
            Name = "BtnKeyNone",
            Position = new Vector2(299, 83),
            Size = new Vector2(40, 30),
            TextureNormal = texNoneNormal,
            TexturePressed = texNonePressed
        };
        btnNone.Pressed += () => OnKeySelected(0);
        _panel.AddChild(btnNone);

        // 官方原生确定按钮 (Prguse_0062 @ 222, 131, 76x33, IDA 0x48555f)
        var texConfirm = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0062.png");
        var btnOk = new TextureButton
        {
            Name = "BtnKeyOk",
            Position = new Vector2(222, 131),
            Size = new Vector2(76, 33),
            TextureNormal = texConfirm
        };
        btnOk.Pressed += OnConfirm;
        _panel.AddChild(btnOk);
    }

    public void OpenForSkill(int magicId, string skillName, int currentKey)
    {
        _currentMagicId = magicId;
        _selectedKey = currentKey;
        if (_lblTitle != null)
        {
            _lblTitle.Text = $"设置【{skillName}】快捷键";
        }

        if (_iconRect != null)
        {
            int iconIdx = 0;
            var meta = Mir2Client.Game.SkillFormulas.GetSkill((short)magicId);
            if (meta.HasValue)
            {
                iconIdx = meta.Value.IconIndex;
            }
            _iconRect.Texture = ResPath.LoadImageTexture($"resources/wil/png/MagIcon/MagIcon_{iconIdx:D4}.png");
        }

        Visible = true;
    }

    private void OnKeySelected(int keyNum)
    {
        _selectedKey = keyNum;
    }

    private void OnConfirm()
    {
        Visible = false;
        EmitSignal(SignalName.KeyBindingConfirmed, _currentMagicId, _selectedKey);
    }

    private void OnClose()
    {
        Visible = false;
    }
}
