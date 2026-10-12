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
    public Official24DialogCompendium.DialogId DialogId => Official24DialogCompendium.DialogId.SkillBook;
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
        Position = new Vector2(800 - PanelWidth * 2, 0); // 盛大官方 1.5 IDA sub_481DD4 内偏移 0x484468：技能窗停靠装备面板左侧 (336, 0)

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

        // 技能页翻页键在右侧 (IDA sub_481dd4 [0x484325..0x48439b]: 向上 399/398 @ (213,113), 向下 397/396 @ (213,143), 12x21)
        // 官方 1:1 实证：常态为暗色贴图 (399/397)，按下态才显示高亮贴图 (398/396)
        var texUpNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0399.png");
        var texUpPressed = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0398.png");
        var texDownNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0397.png");
        var texDownPressed = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0396.png");

        var btnUp = new TextureButton
        {
            Name = "BtnSkillPageUp",
            Position = new Vector2(213, 113),
            Size = new Vector2(12, 21),
            TextureNormal = texUpNormal,
            TexturePressed = texUpPressed,
            FocusMode = FocusModeEnum.None
        };
        btnUp.Pressed += PrevPage;
        _panel.AddChild(btnUp);

        var btnDown = new TextureButton
        {
            Name = "BtnSkillPageDown",
            Position = new Vector2(213, 143),
            Size = new Vector2(12, 21),
            TextureNormal = texDownNormal,
            TexturePressed = texDownPressed,
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

        // 官方原生底部技能描述与修炼信息区域 (IDA sub_488AF4 [0x489d6c..0x489e80]: Left=37, Top=260, 8-9号字, 浅黄)
        // 彻底消除魔法书底板底部凹槽黑洞死区
        _lblSkillDesc = new Label
        {
            Name = "LblSkillDesc",
            Position = new Vector2(37, 260),
            Size = new Vector2(158, 48),
            Text = "",
            AutowrapMode = TextServer.AutowrapMode.WordSmart,
            VerticalAlignment = VerticalAlignment.Top
        };
        _lblSkillDesc.AddThemeFontSizeOverride("font_size", 9);
        _lblSkillDesc.AddThemeColorOverride("font_color", new Color(0.95f, 0.9f, 0.5f));
        _panel.AddChild(_lblSkillDesc);

        RenderCurrentPage();
    }

    private Control? _slotsContainer;
    private Label? _lblSkillDesc;
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

    public record SkillItemData(int MagicId, string Name, int IconIdx, string Key, int Level, int CurTrain, int MaxTrain, string Desc);

    // 全量技能列表定义 (支持战法道全套 1.5 原生技能库，每页 5 项)
    // 包含技能 ID, 名称, 贴图索引, 快捷键, 当前等级, 当前修炼点数, 升级所需修炼点数, 技能说明
    private static readonly SkillItemData[] DefaultSkills =
    [
        // 第一页: 战士核心近战
        new(3, "基本剑术", 0, "F1", 3, 1200, 1200, "战士基本剑术，随修炼等级提升命中率与攻击稳定性。"),
        new(7, "攻杀剑术", 1, "F2", 3, 2400, 2400, "战士被动攻击剑术，攻击时有一定几率造成额外破坏力。"),
        new(12, "刺杀剑术", 2, "F3", 2, 1450, 3000, "战士隔位强攻技能，剑气可穿透目标防御与魔法盾。"),
        new(25, "半月弯刀", 3, "F4", 1, 680, 4000, "战士群体剑气技能，对扇形范围内的多个目标造成伤害。"),
        new(26, "烈火剑法", 4, "F5", 0, 120, 5000, "战士终极近战爆发技能，凝聚烈火精灵造成超强暴击。"),
        // 第二页: 战士与法道进阶
        new(27, "野蛮冲撞", 5, "F6", 1, 350, 2000, "利用强健的体魄冲撞前方的敌人，使其陷入短暂僵直。"),
        new(1, "火球术", 6, "F7", 3, 1000, 1000, "法师基础攻击魔法，凝聚火焰对单个敌人造成魔法伤害。"),
        new(10, "雷电术", 10, "F8", 2, 1800, 3500, "召唤九天神雷劈击敌人，对亡灵不死系怪物有极高克制。"),
        new(2, "治愈术", 20, "", 3, 2000, 2000, "道士基础回血魔法，消耗精神力回复自身或队友的生命值。"),
        new(13, "灵魂火符", 23, "", 1, 420, 2500, "道士核心远程攻击符咒，消耗护身符与法力掷出爆破火符。")
    ];

    // 盛大官方 1.5 35 级战士初始技能魔法书预载 (基本剑术、攻杀、刺杀、半月、烈火，避免空状态黑洞)
    private readonly List<SkillItemData> _skills = new(DefaultSkills);
    public IReadOnlyList<SkillItemData> Skills => _skills;

    /// <summary>
    /// 设置并刷新技能列表 (由 SM_SENDUSERMAGIC / SM_SENDMYMAGIC 驱动)
    /// </summary>
    public void SetSkills(IEnumerable<SkillItemData> skills)
    {
        _skills.Clear();
        _skills.AddRange(skills);
        _currentPage = 0;
        RenderCurrentPage();
    }

    /// <summary>
    /// 添加或更新单项技能 (由 SM_ADDMAGIC 驱动)
    /// </summary>
    public void AddOrUpdateSkill(SkillItemData skill)
    {
        int idx = _skills.FindIndex(s => s.MagicId == skill.MagicId);
        if (idx >= 0)
        {
            _skills[idx] = skill;
        }
        else
        {
            _skills.Add(skill);
        }
        RenderCurrentPage();
    }

    /// <summary>
    /// 更新技能经验与熟练度 (由 SM_UPDATEUSERMAGIC / SM_MAGICLVEXP 驱动)
    /// </summary>
    public void UpdateSkillExp(int magicId, int level, int curTrain, int maxTrain)
    {
        int idx = _skills.FindIndex(s => s.MagicId == magicId);
        if (idx >= 0)
        {
            var old = _skills[idx];
            _skills[idx] = old with { Level = level, CurTrain = curTrain, MaxTrain = maxTrain };
            RenderCurrentPage();
        }
    }

    private void RenderCurrentPage()
    {
        if (_slotsContainer == null) return;
        foreach (Node child in _slotsContainer.GetChildren())
        {
            child.QueueFree();
        }

        int startIdx = _currentPage * 5;
        if (startIdx < _skills.Count && _lblSkillDesc != null)
        {
            _lblSkillDesc.Text = _skills[startIdx].Desc;
        }

        for (int i = 0; i < 5; i++)
        {
            int dataIdx = startIdx + i;
            if (dataIdx >= _skills.Count) break;

            var skill = _skills[dataIdx];
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
            string skillDesc = skill.Desc;
            int keyNum = 0;
            if (!string.IsNullOrEmpty(skill.Key) && skill.Key.StartsWith("F") && int.TryParse(skill.Key[1..], out int k))
            {
                keyNum = k;
            }
            btnSlot.Pressed += () =>
            {
                if (_lblSkillDesc != null) _lblSkillDesc.Text = skillDesc;
                EmitSignal(SignalName.RequestOpenKeySelect, magicId, magicName, keyNum);
            };
            btnSlot.MouseEntered += () =>
            {
                if (_lblSkillDesc != null) _lblSkillDesc.Text = skillDesc;
            };
            _slotsContainer.AddChild(btnSlot);

            // 官方原生快捷键贴图 (IDA 0x48999d..0x489a37: F1~F8 对应 Prguse_0248..0255, 坐标 x0+145=183, y0+8+slot*37=60+slot*37)
            if (keyNum >= 1 && keyNum <= 8)
            {
                int keyImgIdx = 247 + keyNum; // F1 -> 248, F8 -> 255
                var texKey = ResPath.LoadImageTexture($"resources/wil/png/Prguse/Prguse_{keyImgIdx:D4}.png");
                if (texKey != null)
                {
                    var rectKey = new TextureRect
                    {
                        Name = $"KeyImg_{dataIdx}",
                        Texture = texKey,
                        Position = new Vector2(183, 60 + i * 37),
                        Size = new Vector2(16, 16),
                        MouseFilter = MouseFilterEnum.Ignore
                    };
                    _slotsContainer.AddChild(rectKey);
                }
            }

            // 官方技能名称 (IDA sub_488AF4 [0x489b8f..0x489ba4]: x0+48=86, y0+8+slot*37=60+slot*37)
            var lblName = new Label
            {
                Name = $"LblSkillName_{dataIdx}",
                Position = new Vector2(86, 60 + i * 37),
                Size = new Vector2(90, 14),
                Text = skill.Name,
                VerticalAlignment = VerticalAlignment.Center
            };
            lblName.AddThemeFontSizeOverride("font_size", 9);
            lblName.AddThemeColorOverride("font_color", new Color(0xC0 / 255f, 0xC0 / 255f, 0xC0 / 255f));
            _slotsContainer.AddChild(lblName);

            // 官方原生修炼经验/满级文本 (IDA sub_488AF4 [0x489be0..0x489ca4]: x0+48+16=102, y0+23+slot*37=75+slot*37)
            // 满级(Lv3)官方字符串为 "-" (dword_489FE8), 未满级为 "cur/max" (dword_489FF4 为 "/")
            string trainText = skill.Level >= 3 ? "-" : $"{skill.CurTrain}/{skill.MaxTrain}";
            var lblTrain = new Label
            {
                Name = $"LblSkillTrain_{dataIdx}",
                Position = new Vector2(86, 75 + i * 37),
                Size = new Vector2(100, 14),
                Text = trainText,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            lblTrain.AddThemeFontSizeOverride("font_size", 9);
            lblTrain.AddThemeColorOverride("font_color", new Color(0xC0 / 255f, 0xC0 / 255f, 0xC0 / 255f));
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
        if ((_currentPage + 1) * 5 < _skills.Count)
        {
            _currentPage++;
            RenderCurrentPage();
        }
    }

    public void UpdateSkillKey(int magicId, int keyNum)
    {
        string keyStr = keyNum > 0 ? $"F{keyNum}" : "";
        for (int i = 0; i < _skills.Count; i++)
        {
            // 盛大官方 1.5 快捷键互斥排他机制：若其他技能已占用该按键，自动解除旧绑定
            if (!string.IsNullOrEmpty(keyStr) && _skills[i].Key == keyStr && _skills[i].MagicId != magicId)
            {
                var conflict = _skills[i];
                _skills[i] = conflict with { Key = "" };
            }
            if (_skills[i].MagicId == magicId)
            {
                var old = _skills[i];
                _skills[i] = old with { Key = keyStr };
            }
        }
        RenderCurrentPage();
    }

    /// <summary>
    /// 根据快捷键编号 (1..8 对应 F1..F8) 查询当前绑定的技能 ID
    /// </summary>
    public int GetMagicIdForKey(int keyNum)
    {
        if (keyNum <= 0) return 0;
        string targetKey = $"F{keyNum}";
        for (int i = 0; i < _skills.Count; i++)
        {
            if (_skills[i].Key == targetKey)
            {
                return _skills[i].MagicId;
            }
        }
        return 0;
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
