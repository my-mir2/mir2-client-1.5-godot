using System;
using Godot;
using Mir2Client.Utils;

namespace Mir2Client.UI;

/// <summary>
/// 盛大官方 1.5 观察他人装备面板 (纯 C# 实现)
/// 严格 1:1 对齐官方 9 装备槽位 (IDA sub_48A9BC / Prguse_0370 232x325)
/// </summary>
public partial class InspectPanelView : Control, IManagedWindow
{
    [Signal]
    public delegate void PanelClosedEventHandler();

    public const float PanelWidth = 232.0f;
    public const float PanelHeight = 325.0f;

    private Panel? _panel;
    private Label? _lblTitle;

    public string WindowName => "InspectPanelView";
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
        // 官方原生停靠位置：自身装备面板左侧 (IDA sub_481dd4 [0x4844b0..0x4844dc]: 800 - 232 - 232 = 336, Top = 0)
        Position = new Vector2(800 - PanelWidth * 2, 0);

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

        // 官方原生装备面板角色名标签 (Left=60, Top=20, 112x16, 居中宋体)
        _lblTitle = new Label
        {
            Position = new Vector2(60, 20),
            Size = new Vector2(112, 16),
            Text = "",
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        _lblTitle.AddThemeFontSizeOverride("font_size", 11);
        _panel.AddChild(_lblTitle);

        // 官方原生观人面板关闭红叉 (Prguse_0371 @ 8, 39, IDA sub_481dd4 [0x48479a..0x4847ce])
        // Prguse_0370 底板在 (8, 39) 处已自带烘焙凸起红叉，TextureNormal 必须为 null，按下态显示凹陷 Prguse_0371
        var texClose = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0371.png");
        var btnClose = new TextureButton
        {
            Position = new Vector2(8, 39),
            Size = new Vector2(16, 23),
            TextureNormal = null,
            TexturePressed = texClose
        };
        btnClose.Pressed += OnClose;
        _panel.AddChild(btnClose);

        // 官方实证 (IDA DUserState1 0x480734..0x4807e1 & 0x49219c): 观人面板为纯粹 9 装备展示，绝无切页/翻页按键

        // 官方原生观察他人纸娃娃立绘背景 (Prguse_0376 @ Left=38, Top=52, 168x199, IDA sub_488AF4)
        var texDoll = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0376.png");
        var dollBg = new TextureRect
        {
            Name = "InspectDollBg",
            Position = new Vector2(38, 52),
            Size = new Vector2(168, 199),
            Texture = texDoll,
            MouseFilter = MouseFilterEnum.Ignore
        };
        _panel.AddChild(dollBg);

        // 盛大官方 1.5 角色 9 大装备槽位 (IDA sub_481dd4 [0x483eb0..0x484180] & sub_48A9BC 官方实证)
        // 与自身装备面板完全一致，基于 InspectPanel (232x325) 原点 (0, 0) 的绝对局部坐标：
        // 0:衣服, 1:武器, 2:勋章, 3:项链, 4:头盔, 5:右手镯, 6:左手镯, 7:右戒指, 8:左戒指
        var slotDefs = new (int Slot, float X, float Y, float W, float H)[]
        {
            (1, 47, 80, 47, 87),    // 武器 (0x398, IDA 0x4840f6..0x484134: Left=47, Top=80, W=47, H=87)
            (0, 96, 122, 53, 112),  // 衣服 (0x39C, IDA 0x484142..0x484180: Left=96, Top=122, W=53, H=112)
            (4, 115, 93, 18, 18),   // 头盔 (0x3A0, IDA 0x483f2e..0x483f6c: Left=115, Top=93, W=18, H=18)
            (3, 168, 87, 34, 31),   // 项链 (0x380, IDA 0x483ee2..0x483f20: Left=168, Top=87, W=34, H=31)
            (2, 168, 125, 34, 31),  // 勋章/右项链 (0x384, IDA 0x483f7a..0x483fb8: Left=168, Top=125, W=34, H=31)
            (6, 42, 176, 34, 31),   // 左手镯 (0x388, IDA 0x483fc6..0x484004: Left=42, Top=176, W=34, H=31)
            (5, 168, 176, 34, 31),  // 右手镯 (0x38C, IDA 0x484012..0x484050: Left=168, Top=176, W=34, H=31)
            (8, 42, 215, 34, 31),   // 左戒指/符 (0x390, IDA 0x48405e..0x48409c: Left=42, Top=215, W=34, H=31)
            (7, 168, 215, 34, 31),  // 右戒指 (0x394, IDA 0x4840aa..0x4840e8: Left=168, Top=215, W=34, H=31)
        };

        foreach (var s in slotDefs)
        {
            var btn = new SlotButton
            {
                Name = $"InspectSlot_{s.Slot}",
                CurrentSlotType = SlotButton.SlotType.Equip,
                SlotIndex = s.Slot,
                Position = new Vector2(s.X, s.Y),
                Size = new Vector2(s.W, s.H),
                CustomMinimumSize = new Vector2(s.W, s.H),
                Flat = true
            };
            _panel.AddChild(btn);
        }
    }

    public void Inspect(string targetName)
    {
        if (_lblTitle != null) _lblTitle.Text = targetName;
        Visible = true;
    }

    private void OnClose()
    {
        Visible = false;
        EmitSignal(SignalName.PanelClosed);
    }
}
