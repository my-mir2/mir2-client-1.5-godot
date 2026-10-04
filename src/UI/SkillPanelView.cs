using System;
using System.Collections.Generic;
using Godot;
using Mir2Client.Utils;

namespace Mir2Client.UI;

/// <summary>
/// 盛大官方 1.5 技能魔法书面板 (F11 / DStateWnd 技能页, 纯 C# 实现)
/// 严格对齐官方 Prguse_0370 (232x325)
/// </summary>
public partial class SkillPanelView : Control, IManagedWindow
{
    [Signal]
    public delegate void RequestOpenKeySelectEventHandler(int magicId, string magicName, int currentKey);

    [Signal]
    public delegate void PanelClosedEventHandler();

    public const float PanelWidth = 232.0f;
    public const float PanelHeight = 325.0f;

    private Panel? _panel;

    public string WindowName => "SkillPanelView";
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
        Position = new Vector2(800 - PanelWidth * 2, 0); // 盛大官方 1.5 IDA sub_484468 停靠装备面板左侧 (336, 0)

        _panel = new Panel
        {
            Size = new Vector2(PanelWidth, PanelHeight),
            MouseFilter = MouseFilterEnum.Stop
        };

        var texBg = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0370.png");
        if (texBg != null)
        {
            _panel.AddThemeStyleboxOverride("panel", new StyleBoxTexture { Texture = texBg });
        }
        AddChild(_panel);

        // 官方原生技能魔法书核心内衬底板 (Prguse_0383 @ 38, 52, 168x199, IDA sub_488AF4 [0x488af4..0x488bb7])
        // 彻底覆盖底板自带的“状态”字样与 AC/MAC 数值雕刻槽，呈现纯正神龙暗纹魔法书页
        var texBookPage = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0383.png");
        var bookPage = new TextureRect
        {
            Name = "SkillBookPage",
            Position = new Vector2(38, 52),
            Size = new Vector2(168, 199),
            Texture = texBookPage,
            MouseFilter = MouseFilterEnum.Pass
        };
        _panel.AddChild(bookPage);

        // 官方原生左上角关闭按钮 (Prguse_0371 @ 8, 39, 16x23, 底板已烘焙凸起红叉)
        var texClose = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0371.png");
        var btnClose = new TextureButton
        {
            Name = "BtnSkillClose",
            Position = new Vector2(8, 39),
            Size = new Vector2(16, 23),
            TextureNormal = null,
            TexturePressed = texClose
        };
        btnClose.Pressed += OnClose;
        _panel.AddChild(btnClose);

        // 技能页翻页键在右侧 (IDA sub_481dd4 [0x484325..0x48439b]: Prguse_0398 @ (213,113) / Prguse_0396 @ (213,143), 12x21)
        // 技能页不使用左侧 373/372 上下键 (该对仅用于装备/状态/观察页)
        var texUp = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0398.png");
        var texDown = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0396.png");

        var btnUp = new TextureButton
        {
            Name = "BtnSkillPageUp",
            Position = new Vector2(213, 113),
            Size = new Vector2(12, 21),
            TextureNormal = texUp,
            FocusMode = FocusModeEnum.None
        };
        btnUp.Pressed += PrevPage;
        _panel.AddChild(btnUp);

        var btnDown = new TextureButton
        {
            Name = "BtnSkillPageDown",
            Position = new Vector2(213, 143),
            Size = new Vector2(12, 21),
            TextureNormal = texDown,
            FocusMode = FocusModeEnum.None
        };
        btnDown.Pressed += NextPage;
        _panel.AddChild(btnDown);

        _slotsContainer = new Control
        {
            Name = "SkillSlotsContainer",
            Position = Vector2.Zero,
            Size = new Vector2(PanelWidth, PanelHeight),
            MouseFilter = MouseFilterEnum.Pass
        };
        _panel.AddChild(_slotsContainer);

        RenderCurrentPage();
    }

    private Control? _slotsContainer;
    private int _currentPage = 0;

    // 官方 1.5 技能魔法书每页 5 大槽位绝对物理坐标 (IDA sub_481dd4 [0x48419c..0x484305]):
    // 0: Left=46, Top=59, W=31, H=33
    // 1: Left=46, Top=96, W=31, H=33
    // 2: Left=46, Top=134, W=31, H=33
    // 3: Left=46, Top=171, W=31, H=33
    // 4: Left=46, Top=208, W=31, H=33
    private static readonly (int Y, int H)[] SlotRows =
    [
        (59, 33),
        (96, 33),
        (134, 33),
        (171, 33),
        (208, 33)
    ];

    // 全量技能列表定义 (魔改支持战法道全套 1.5 原生技能库，每页 5 项)
    // 全量技能列表定义 (支持战法道全套 1.5 原生技能库，每页 5 项)
    // 包含技能 ID, 名称, 贴图索引, 快捷键, 当前等级, 当前修炼点数, 升级所需修炼点数
    private static readonly (int MagicId, string Name, int IconIdx, string Key, int Level, int CurTrain, int MaxTrain)[] AllSkills =
    [
        // 第一页: 战士核心近战
        (3, "基本剑术", 0, "F1", 3, 1200, 1200),
        (7, "攻杀剑术", 1, "F2", 3, 2400, 2400),
        (12, "刺杀剑术", 2, "F3", 2, 1450, 3000),
        (25, "半月弯刀", 3, "F4", 1, 680, 4000),
        (26, "烈火剑法", 4, "F5", 0, 120, 5000),
        // 第二页: 战士与法道进阶
        (27, "野蛮冲撞", 5, "F6", 1, 350, 2000),
        (1, "火球术", 6, "F7", 3, 1000, 1000),
        (10, "雷电术", 10, "F8", 2, 1800, 3500),
        (2, "治愈术", 20, "", 3, 2000, 2000),
        (13, "灵魂火符", 23, "", 1, 420, 2500)
    ];

    private void RenderCurrentPage()
    {
        if (_slotsContainer == null) return;
        foreach (Node child in _slotsContainer.GetChildren())
        {
            child.QueueFree();
        }

        int startIdx = _currentPage * 5;
        for (int i = 0; i < 5; i++)
        {
            int dataIdx = startIdx + i;
            if (dataIdx >= AllSkills.Length) break;

            var skill = AllSkills[dataIdx];
            var (rowY, rowH) = SlotRows[i];

            // 官方原生 MagIcon 技能贴图 (32x30)
            var texIcon = ResPath.LoadImageTexture($"resources/wil/png/MagIcon/MagIcon_{skill.IconIdx:D4}.png");

            var btnSlot = new SlotButton
            {
                Name = $"SkillSlot_{dataIdx}",
                CurrentSlotType = SlotButton.SlotType.Skill,
                SlotIndex = skill.MagicId,
                Position = new Vector2(46, rowY),
                Size = new Vector2(31, rowH),
                CustomMinimumSize = new Vector2(31, rowH),
                Flat = true,
                FocusMode = FocusModeEnum.None
            };

            if (texIcon != null)
            {
                var iconRect = new TextureRect
                {
                    Name = $"Icon_{dataIdx}",
                    Texture = texIcon,
                    Position = new Vector2(0, 1),
                    Size = new Vector2(31, 30),
                    ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize,
                    StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered,
                    MouseFilter = MouseFilterEnum.Ignore
                };
                btnSlot.AddChild(iconRect);
            }

            // 图标右上角快捷键小角标 (如 F1~F8, 官方经典金黄色白底微标)
            if (!string.IsNullOrEmpty(skill.Key))
            {
                var lblSlotKey = new Label
                {
                    Name = $"SlotKeyBadge_{dataIdx}",
                    Position = new Vector2(15, 0),
                    Size = new Vector2(16, 12),
                    Text = skill.Key,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    MouseFilter = MouseFilterEnum.Ignore
                };
                lblSlotKey.AddThemeFontSizeOverride("font_size", 8);
                lblSlotKey.AddThemeColorOverride("font_color", new Color(1.0f, 0.95f, 0.2f));
                lblSlotKey.AddThemeConstantOverride("outline_size", 2);
                lblSlotKey.AddThemeColorOverride("font_outline_color", new Color(0, 0, 0, 0.9f));
                btnSlot.AddChild(lblSlotKey);
            }

            int magicId = skill.MagicId;
            string magicName = skill.Name;
            int keyNum = 0;
            if (!string.IsNullOrEmpty(skill.Key) && skill.Key.StartsWith("F") && int.TryParse(skill.Key[1..], out int k))
            {
                keyNum = k;
            }
            btnSlot.Pressed += () => EmitSignal(SignalName.RequestOpenKeySelect, magicId, magicName, keyNum);
            _slotsContainer.AddChild(btnSlot);

            // 技能名称 (82, rowY + 1)
            var lblName = new Label
            {
                Name = $"LblSkillName_{dataIdx}",
                Position = new Vector2(82, rowY + 1),
                Size = new Vector2(65, 14),
                Text = skill.Name,
                VerticalAlignment = VerticalAlignment.Center
            };
            lblName.AddThemeFontSizeOverride("font_size", 9);
            lblName.AddThemeColorOverride("font_color", new Color(0.95f, 0.95f, 0.85f));
            _slotsContainer.AddChild(lblName);

            // 技能等级 (150, rowY + 1)
            var lblLevel = new Label
            {
                Name = $"LblSkillLevel_{dataIdx}",
                Position = new Vector2(150, rowY + 1),
                Size = new Vector2(40, 14),
                Text = $"Lv.{skill.Level}",
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center
            };
            lblLevel.AddThemeFontSizeOverride("font_size", 9);
            lblLevel.AddThemeColorOverride("font_color", new Color(0.5f, 0.85f, 1.0f));
            _slotsContainer.AddChild(lblLevel);

            // 官方原生纯文本技能修炼经验 (IDA sub_481dd4, 纯文本呈现，无自绘 ColorRect 进度条)
            var lblTrain = new Label
            {
                Name = $"LblSkillTrain_{dataIdx}",
                Position = new Vector2(82, rowY + 16),
                Size = new Vector2(108, 14),
                Text = skill.Level >= 3 ? "已满级" : $"{skill.CurTrain}/{skill.MaxTrain}",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            lblTrain.AddThemeFontSizeOverride("font_size", 9);
            lblTrain.AddThemeColorOverride("font_color", new Color(0.85f, 0.85f, 0.8f));
            _slotsContainer.AddChild(lblTrain);
        }
    }

    public void PrevPage()
    {
        if (_currentPage > 0)
        {
            _currentPage--;
            RenderCurrentPage();
        }
    }

    public void NextPage()
    {
        if ((_currentPage + 1) * 5 < AllSkills.Length)
        {
            _currentPage++;
            RenderCurrentPage();
        }
    }

    public void UpdateSkillKey(int magicId, int keyNum)
    {
        for (int i = 0; i < AllSkills.Length; i++)
        {
            if (AllSkills[i].MagicId == magicId)
            {
                string keyStr = keyNum > 0 ? $"F{keyNum}" : "";
                var old = AllSkills[i];
                AllSkills[i] = (old.MagicId, old.Name, old.IconIdx, keyStr, old.Level, old.CurTrain, old.MaxTrain);
                RenderCurrentPage();
                break;
            }
        }
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
