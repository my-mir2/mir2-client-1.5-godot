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

        // 官方原生技能魔法书 5 大技能槽位与快捷键说明 (Prguse_0383 左侧黑色雕刻方槽，单格 30x30 @ X=46, Y=[57, 95, 132, 169, 206])
        var skillSlots = new (int Idx, int Y, string Name, string Key)[]
        {
            (0, 57, "基础剑术", "F1"),
            (1, 95, "攻杀剑术", "F2"),
            (2, 132, "刺杀剑术", "F3"),
            (3, 169, "半月弯刀", "F4"),
            (4, 206, "烈火剑法", "F5")
        };

        foreach (var s in skillSlots)
        {
            var btnSlot = new SlotButton
            {
                Name = $"SkillSlot_{s.Idx}",
                CurrentSlotType = SlotButton.SlotType.Skill,
                SlotIndex = s.Idx,
                Position = new Vector2(46, s.Y),
                Size = new Vector2(29, 30),
                CustomMinimumSize = new Vector2(29, 30),
                Flat = true
            };
            _panel.AddChild(btnSlot);

            var lblName = new Label
            {
                Name = $"LblSkillName_{s.Idx}",
                Position = new Vector2(82, s.Y + 2),
                Size = new Vector2(80, 14),
                Text = s.Name,
                VerticalAlignment = VerticalAlignment.Center
            };
            lblName.AddThemeFontSizeOverride("font_size", 10);
            lblName.AddThemeColorOverride("font_color", new Color(0.95f, 0.95f, 0.85f));
            _panel.AddChild(lblName);

            var lblKey = new Label
            {
                Name = $"LblSkillKey_{s.Idx}",
                Position = new Vector2(168, s.Y + 2),
                Size = new Vector2(30, 14),
                Text = s.Key,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            lblKey.AddThemeFontSizeOverride("font_size", 9);
            lblKey.AddThemeColorOverride("font_color", new Color(0.3f, 0.9f, 1.0f));
            _panel.AddChild(lblKey);
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
