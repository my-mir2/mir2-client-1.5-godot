using System;
using System.Collections.Generic;
using Godot;
using Mir2Client.Utils;

namespace Mir2Client.UI;

/// <summary>
/// 盛大官方 1.5 角色状态面板 (StatusPanelView, 独立窗体实现)
/// 严格 1:1 对齐官方客户端二进制 IDA TFrmMain.Init & sub_481dd4 [0x483e8c..0x484391]
/// 底板贴图：Prguse_0370.png (232x325 @ (568, 0))
/// 关闭按钮：Prguse_0371.png @ (8, 39, 16x23)
/// 切回装备按钮：Prguse_0398/0399 @ (213, 113, 12x21)
/// 七大属性内胆：X=107..178 (W=72, H=14)，Y=[97, 117, 137, 157, 177, 197, 217]
/// 底部说明文字：X=34, Y=264, 178x51
/// </summary>
public partial class StatusPanelView : Control, IManagedWindow
{
    public static readonly Vector2 PanelSize = new(232, 325);
    public static readonly Vector2 DefaultPos = new(568, 0);

    public string WindowName => "StatusPanelView";
    public new bool IsVisible
    {
        get => Visible;
        set => Visible = value;
    }
    public (int x, int y, int width, int height) Bounds =>
        ((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

    private readonly Dictionary<string, Label> _statLabels = new();
    private Label? _lblDetail;

    public event Action? SwitchToEquipRequested;

    public override void _Ready()
    {
        Size = PanelSize;
        Position = DefaultPos;
        MouseFilter = MouseFilterEnum.Stop;
        Visible = false;

        // 1. 底板贴图 Prguse_0370.png (官方 232x325)
        var bgTex = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0370.png");
        if (bgTex != null)
        {
            var bg = new TextureRect
            {
                Name = "StatusBg",
                Size = PanelSize,
                Texture = bgTex,
                MouseFilter = MouseFilterEnum.Pass
            };
            AddChild(bg);
        }

        // 2. 原生关闭按钮 (Prguse_0371 @ 8, 39, 16x23, IDA sub_481dd4 [0x4843b1..0x4843d7])
        var texClose = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0371.png");
        var btnClose = new TextureButton
        {
            Name = "BtnStatusClose",
            Position = new Vector2(8, 39),
            Size = new Vector2(16, 23),
            TextureNormal = null,
            TexturePressed = texClose,
            FocusMode = FocusModeEnum.None
        };
        btnClose.Pressed += () => Visible = false;
        AddChild(btnClose);

        // 3. 向上按钮 (切回装备页 Tab 1): Prguse_0398 (正常) / 0399 (按压), Left=213, Top=113, Size=12x21
        var texTabUp = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0398.png");
        var texTabUpPressed = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0399.png");
        var btnTab1 = new TextureButton
        {
            Name = "BtnStatusTab1",
            Position = new Vector2(213, 113),
            Size = new Vector2(12, 21),
            TextureNormal = texTabUp,
            TexturePressed = texTabUpPressed,
            FocusMode = FocusModeEnum.None
        };
        btnTab1.Pressed += () => SwitchToEquipRequested?.Invoke();
        AddChild(btnTab1);

        // 4. 七大属性数值槽位 (AC/MAC/DC/MC/SC/HP/MP)
        // 官方 Prguse_0370 底板自带艺术字标题，数值槽位严格位于纯黑长条内胆: X=107, W=72, H=14
        var statSlotDefs = new (string StatName, int Y, string DefaultVal)[]
        {
            ("AC", 97, "0-0"),
            ("MAC", 117, "0-0"),
            ("DC", 137, "0-0"),
            ("MC", 157, "0-0"),
            ("SC", 177, "0-0"),
            ("HP", 197, "100/100"),
            ("MP", 217, "50/50")
        };

        foreach (var def in statSlotDefs)
        {
            var lblVal = new Label
            {
                Name = $"LblStatVal_{def.StatName}",
                Position = new Vector2(107, def.Y),
                Size = new Vector2(72, 14),
                Text = def.DefaultVal,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            lblVal.AddThemeFontSizeOverride("font_size", 10);
            lblVal.AddThemeColorOverride("font_color", new Color(0.95f, 0.95f, 0.95f));
            AddChild(lblVal);
            _statLabels[def.StatName] = lblVal;
        }

        // 5. 下方状态说明框文本区域 (底板自身凹槽内胆暗纹实证 @ (34, 264, 178x51))
        _lblDetail = new Label
        {
            Name = "LblStatusDetail",
            Position = new Vector2(34, 264),
            Size = new Vector2(178, 51),
            Text = "热血传奇 1.5 虎卫传说\n玛法大陆 战法道属性全集",
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        _lblDetail.AddThemeFontSizeOverride("font_size", 10);
        _lblDetail.AddThemeColorOverride("font_color", new Color(0.85f, 0.85f, 0.75f));
        AddChild(_lblDetail);
    }

    public void UpdateStat(string statName, string val)
    {
        if (_statLabels.TryGetValue(statName, out var lbl))
        {
            lbl.Text = val;
        }
    }

    public void UpdateDetailText(string text)
    {
        if (_lblDetail != null)
        {
            _lblDetail.Text = text;
        }
    }
}
