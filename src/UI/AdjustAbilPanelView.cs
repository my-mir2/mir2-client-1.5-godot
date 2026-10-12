using System;
using System.Collections.Generic;
using Godot;
using Mir2Client.Utils;

namespace Mir2Client.UI;

/// <summary>
/// 经典传奇 1.5 角色升级属性点分配面板 (AdjustAbilDlg, 纯 C# 实现)
/// 严格对齐官方客户端二进制 (IDA sub_485eeb ~ 0x486241, Prguse_0226 332x347 @ 234, 126)
/// 包含 9 大属性（HP、MP、AC、MAC、DC、MC、SC 等）加减微调按钮对 (Left=217/227, Top 101~261 每隔 20px)
/// </summary>
public partial class AdjustAbilPanelView : Control, IManagedWindow
{
    [Signal]
    public delegate void StatSubmittedEventHandler(int bonusType);

    [Signal]
    public delegate void ClosedEventHandler();

    public static readonly Vector2 PanelSize = new(332, 347);
    public static readonly Vector2 DefaultPos = new(234, 126);

    public string WindowName => "AdjustAbilPanelView";
    public Official24DialogCompendium.DialogId DialogId => Official24DialogCompendium.DialogId.AdjustAbil;
    public new bool IsVisible
    {
        get => Visible;
        set => Visible = value;
    }
    public (int x, int y, int width, int height) Bounds =>
        ((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

    private int _remainingPoints = 5;
    private Label? _lblRemain;
    private readonly int[] _statAllocations = new int[9];
    private readonly Label[] _lblStatAllocations = new Label[9];
    private readonly Label[] _lblBaseStats = new Label[9];

    // 官方 9 大属性名称与行索引 (严格对齐底板 Prguse_0226 真实印制顺序与 IDA sub_494304)
    public static readonly string[] StatNames =
    [
        "DC", "MC", "SC", "AC", "MAC", "HP", "MP", "HIT", "SPD"
    ];

    // 官方 1.5 默认角色基础属性参考值 (战士典型模板: 攻击/魔法/道术/防御/魔御/生命/魔法/命中/敏捷)
    public static readonly string[] DefaultBaseStats =
    [
        "10-25", "0-0", "0-0", "5-10", "4-7", "210", "45", "15", "10"
    ];

    // IDA sub_481dd4 0x485fb8~0x486471: 加/减按钮 Top 逐项 101,121,140,160,181,201,220,240,261 (非等距)
    private static readonly float[] ButtonTops = [101f, 121f, 140f, 160f, 181f, 201f, 220f, 240f, 261f];

    private static readonly float[] RowTops =
    [
        99.0f, 119.0f, 139.0f, 159.0f, 179.0f, 199.0f, 219.0f, 239.0f, 259.0f
    ];

    public override void _Ready()
    {
        Size = PanelSize;
        Position = DefaultPos;
        MouseFilter = MouseFilterEnum.Stop;
        Visible = false;

        var panel = new Panel
        {
            Name = "Panel",
            Size = PanelSize,
            MouseFilter = MouseFilterEnum.Stop
        };
        var texBg = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0226.png");
        if (texBg != null)
        {
            panel.AddThemeStyleboxOverride("panel", new StyleBoxTexture { Texture = texBg });
        }
        AddChild(panel);

        // 官方原生右上角关闭按钮 (IDA sub_481dd4 0x485f2c: left: 316, top: 1)
        // 底板 Prguse_0226 在 (316, 1) 处自带烘焙凸起红叉，TextureNormal = null，按下显示凹陷 Prguse_0064
        var texClose = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0064.png");
        var btnClose = new TextureButton
        {
            Name = "BtnAdjustClose",
            Position = new Vector2(316, 1),
            Size = new Vector2(16, 23),
            TextureNormal = null,
            TexturePressed = texClose
        };
        btnClose.Pressed += OnClose;
        panel.AddChild(btnClose);

        // 剩余点数标签 (底板 Available 文字右侧黑底凹槽: Left 100, Top 278, W 50, H 16, IDA sub_494304 0x494497)
        _lblRemain = new Label
        {
            Name = "LblRemainPoints",
            Position = new Vector2(100, 278),
            Size = new Vector2(50, 16),
            Text = _remainingPoints.ToString(),
            VerticalAlignment = VerticalAlignment.Center
        };
        _lblRemain.AddThemeFontSizeOverride("font_size", 12);
        _lblRemain.AddThemeColorOverride("font_color", new Color(1.0f, 0.95f, 0.4f));
        panel.AddChild(_lblRemain);

        // 官方加点页提示文本 (IDA sub_494304 0x494412~0x494465: 起点 (窗体+36, 窗体+22),4 行行距 14,颜色 0xC0C0C0;
        // 字符串 GBK 取自 0x494EC0/0x494EE8/0x494F08/0x494F2C)
        string[] introLines =
        {
            "恭喜: 你已经到了下一个等级了!.",
            "选择你想提高的能力。",
            "这样的选择只可以做一次。",
            "最好能很小心的选择。."
        };
        for (int k = 0; k < introLines.Length; k++)
        {
            var lblIntro = new Label
            {
                Name = $"LblAdjustIntro_{k}",
                Text = introLines[k],
                Position = new Vector2(36, 22 + 14 * k),
                Size = new Vector2(280, 14),
                MouseFilter = MouseFilterEnum.Ignore
            };
            lblIntro.AddThemeFontSizeOverride("font_size", 12);
            lblIntro.AddThemeColorOverride("font_color", new Color(0xC0 / 255f, 0xC0 / 255f, 0xC0 / 255f));
            panel.AddChild(lblIntro);
        }

        // 官方 9 属性加减按键矩阵 (IDA sub_481dd4 0x485ffe ~ 0x486471: Left=216 为加号，Left=227 为减号)
        var texPlus = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0227.png");
        var texMinus = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0228.png");

        for (int i = 0; i < 9; i++)
        {
            int statIdx = i;
            float top = 99f + 20f * i; // 官方 Prguse_0226 黑色凹槽物理区间 y=[99..119], 步长 20

            // 1. 官方属性基础当前数值 (IDA sub_494304 0x494497: x=窗体+100, W=52, H=20, 垂直居中)
            var lblBase = new Label
            {
                Name = $"LblBaseStat_{statIdx}",
                Position = new Vector2(100, top),
                Size = new Vector2(52, 20),
                Text = DefaultBaseStats[statIdx],
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            lblBase.AddThemeFontSizeOverride("font_size", 11);
            lblBase.AddThemeColorOverride("font_color", new Color(0xC0 / 255f, 0xC0 / 255f, 0xC0 / 255f));
            panel.AddChild(lblBase);
            _lblBaseStats[statIdx] = lblBase;

            // 2. 属性分配增量数值显示 (精准咬合于黑底凹槽 155..214, W=58, H=20, 垂直居中)
            var lblVal = new Label
            {
                Name = $"LblStat_{statIdx}",
                Position = new Vector2(156, top), // IDA 0x494994: 加点增量列 x=窗体+155
                Size = new Vector2(58, 20),
                Text = "0",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            lblVal.AddThemeFontSizeOverride("font_size", 11);
            lblVal.AddThemeColorOverride("font_color", new Color(0.95f, 0.95f, 0.8f));
            panel.AddChild(lblVal);
            _lblStatAllocations[statIdx] = lblVal;

            // 官方底板 Prguse_0226 已经精细烘焙了印制好的微调凹槽底图！
            // 平时 TextureNormal = null 透出底板原生质感，按下时 TexturePressed 显示高亮+-号，杜绝贴片感！
            // 加点按钮 (Left=216, Top=top+2, 12x11)
            var btnInc = new TextureButton
            {
                Name = $"BtnInc_{statIdx}",
                Position = new Vector2(217, ButtonTops[statIdx]),
                Size = new Vector2(12, 11),
                TextureNormal = null,
                TexturePressed = texPlus
            };
            btnInc.Pressed += () => OnModifyStat(statIdx, +1);
            panel.AddChild(btnInc);

            // 减点按钮 (Left=227, Top=top+2, 12x11)
            var btnDec = new TextureButton
            {
                Name = $"BtnDec_{statIdx}",
                Position = new Vector2(227, ButtonTops[statIdx]),
                Size = new Vector2(12, 11),
                TextureNormal = null,
                TexturePressed = texMinus
            };
            btnDec.Pressed += () => OnModifyStat(statIdx, -1);
            panel.AddChild(btnDec);
        }

        // 官方原生确认加点按钮 (IDA sub_481dd4 0x485f72: 严格对齐底板金属 OK 框 221, 299, 88x30)
        // 底板已自带 OK 贴图，TextureNormal 为空透明响应，按下显示轻微按下态
        var btnConfirm = new TextureButton
        {
            Name = "BtnAdjustConfirm",
            Position = new Vector2(220, 298),
            Size = new Vector2(88, 30)
        };
        btnConfirm.Pressed += OnConfirm;
        panel.AddChild(btnConfirm);
    }

    private void OnModifyStat(int statIdx, int delta)
    {
        if (delta > 0)
        {
            if (_remainingPoints > 0)
            {
                _remainingPoints--;
                _statAllocations[statIdx]++;
            }
        }
        else if (delta < 0)
        {
            if (_statAllocations[statIdx] > 0)
            {
                _remainingPoints++;
                _statAllocations[statIdx]--;
            }
        }

        if (_lblRemain != null) _lblRemain.Text = _remainingPoints.ToString();
        if (_lblStatAllocations[statIdx] != null)
        {
            int val = _statAllocations[statIdx];
            _lblStatAllocations[statIdx].Text = val > 0 ? $"+{val}" : "0";
            _lblStatAllocations[statIdx].AddThemeColorOverride("font_color", val > 0 ? new Color(1.0f, 1.0f, 0.3f) : new Color(0.95f, 0.95f, 0.8f));
        }
    }

    /// <summary>
    /// 外部属性微调接口 (用于协议下发属性增量或 E2E 自动化测试)
    /// </summary>
    public void AllocateStat(int statIdx, int delta)
    {
        if (statIdx >= 0 && statIdx < 9)
        {
            OnModifyStat(statIdx, delta);
        }
    }

    /// <summary>
    /// 设置 9 大基础属性值 (DC, MC, SC, AC, MAC, HP, MP, HIT, SPD)
    /// </summary>
    public void SetBaseStats(IReadOnlyList<string> stats)
    {
        if (stats == null) return;
        for (int i = 0; i < Math.Min(stats.Count, 9); i++)
        {
            if (_lblBaseStats[i] != null)
            {
                _lblBaseStats[i].Text = stats[i];
            }
        }
    }

    public void Open(int points)
    {
        _remainingPoints = points;
        if (_lblRemain != null) _lblRemain.Text = points.ToString();
        for (int i = 0; i < 9; i++)
        {
            _statAllocations[i] = 0;
            if (_lblStatAllocations[i] != null) _lblStatAllocations[i].Text = "0";
        }
        Visible = true;
    }

    private void OnClose()
    {
        Visible = false;
        EmitSignal(SignalName.Closed);
    }

    private void OnConfirm()
    {
        Visible = false;
        bool hasAlloc = false;
        for (int i = 0; i < 9; i++)
        {
            if (_statAllocations[i] > 0)
            {
                hasAlloc = true;
                for (int c = 0; c < _statAllocations[i]; c++)
                {
                    EmitSignal(SignalName.StatSubmitted, i);
                }
            }
        }
        if (!hasAlloc)
        {
            // 若未调整点数，默认不发包
        }
    }

    /// <summary>
    /// 重置所有分配的属性点并返还到剩余点数池
    /// </summary>
    public void ResetAllocations()
    {
        for (int i = 0; i < 9; i++)
        {
            if (_statAllocations[i] > 0)
            {
                _remainingPoints += _statAllocations[i];
                _statAllocations[i] = 0;
                if (_lblStatAllocations[i] != null)
                {
                    _lblStatAllocations[i].Text = "0";
                    _lblStatAllocations[i].AddThemeColorOverride("font_color", new Color(0.95f, 0.95f, 0.8f));
                }
            }
        }
        if (_lblRemain != null) _lblRemain.Text = _remainingPoints.ToString();
    }
}
