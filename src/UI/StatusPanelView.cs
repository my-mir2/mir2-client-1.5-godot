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

        // 4. 七大属性数值槽位 (官方 Prguse_0370 底板自带中文艺术字标题"攻击/魔御/攻击力/魔法/道术/生命/魔法")
        // 数值显示在纯黑长条内胆右侧: X=107, W=72, H=14
        // IDA sub_488AF4 [0x4892c2..0x4896e5] 实证：官方以 dword_48A000/48A01C/48A030... 标识各属性字符串
        // 官方属性标签与顺序（依据 Prguse_0370 底板铭文从上到下）：
        // 攻击(AC) / 魔御(MAC) / 攻击力(DC) / 魔法(MC) / 道术(SC) / 生命(HP) / 魔法值(MP)
        var statSlotDefs = new (string StatKey, string DisplayName, int Y, string DefaultVal)[]
        {
            ("AC",  "攻击",  97,  "0-0"),
            ("MAC", "魔御",  117, "0-0"),
            ("DC",  "攻击力",137, "0-0"),
            ("MC",  "魔法",  157, "0-0"),
            ("SC",  "道术",  177, "0-0"),
            ("HP",  "生命",  197, "100/100"),
            ("MP",  "魔法值",217, "50/50")
        };

        foreach (var def in statSlotDefs)
        {
            var lblVal = new Label
            {
                Name = $"LblStatVal_{def.StatKey}",
                Position = new Vector2(107, def.Y),
                Size = new Vector2(72, 14),
                Text = def.DefaultVal,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            lblVal.AddThemeFontSizeOverride("font_size", 10);
            lblVal.AddThemeColorOverride("font_color", new Color(0.95f, 0.95f, 0.95f));
            AddChild(lblVal);
            _statLabels[def.StatKey] = lblVal;
        }

        // 5. 下方状态说明框文本区域 (底板自身凹槽内胆暗纹实证 @ (34, 266, 178x48))
        // 盛大原版此区域为纯净内胆凹槽，默认不渲染任何硬编码描述，仅在特定技能/状态交互时动态显示
        _lblDetail = new Label
        {
            Name = "LblStatusDetail",
            Position = new Vector2(34, 266),
            Size = new Vector2(178, 48),
            Text = "",
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
