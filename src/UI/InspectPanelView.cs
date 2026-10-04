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

        // 盛大官方 1.5 观察他人纸娃娃核心立绘层 (挂在 dollBg 下，展示被观察者道士神装纸娃娃)
        // 1. 衣服立绘层: StateItem_0064 (幽灵战衣男 80x129 @ 39, 63)
        var texCloth = ResPath.LoadImageTexture("resources/wil/png/StateItem/StateItem_0064.png");
        if (texCloth != null)
        {
            var clothRect = new TextureRect
            {
                Name = "InspectDollCloth",
                Texture = texCloth,
                Position = new Vector2(39, 63),
                Size = new Vector2(80, 129),
                MouseFilter = MouseFilterEnum.Ignore,
                ZIndex = 1
            };
            dollBg.AddChild(clothRect);
        }

        // 2. 武器立绘层: StateItem_0053 (龙纹剑 44x112 @ 14, 20，置于衣服上方手部咬合处)
        var texWeapon = ResPath.LoadImageTexture("resources/wil/png/StateItem/StateItem_0053.png");
        if (texWeapon != null)
        {
            var weaponRect = new TextureRect
            {
                Name = "InspectDollWeapon",
                Texture = texWeapon,
                Position = new Vector2(14, 20),
                Size = new Vector2(44, 112),
                MouseFilter = MouseFilterEnum.Ignore,
                ZIndex = 2
            };
            dollBg.AddChild(weaponRect);
        }

        // 3. 头盔立绘层: StateItem_0108 (道士头盔 20x23 @ 74, 42，严丝合缝罩在头部)
        var texHelm = ResPath.LoadImageTexture("resources/wil/png/StateItem/StateItem_0108.png");
        if (texHelm != null)
        {
            var helmRect = new TextureRect
            {
                Name = "InspectDollHelm",
                Texture = texHelm,
                Position = new Vector2(74, 42),
                Size = new Vector2(20, 23),
                MouseFilter = MouseFilterEnum.Ignore,
                ZIndex = 3
            };
            dollBg.AddChild(helmRect);
        }

        // 盛大官方 1.5 观人面板底部属性内胆 (Prguse_0370 凹槽区域 x:[33..213], y:[268..317]，内部文本内缩置于 (38, 271, 172, 44))
        _lblInspectDesc = new RichTextLabel
        {
            Name = "LblInspectItemDesc",
            Position = new Vector2(38, 271),
            Size = new Vector2(172, 44),
            BbcodeEnabled = true,
            ScrollActive = false,
            MouseFilter = MouseFilterEnum.Ignore
        };
        _lblInspectDesc.AddThemeFontSizeOverride("normal_font_size", 10);
        _lblInspectDesc.AddThemeConstantOverride("line_separation", 1);
        _panel.AddChild(_lblInspectDesc);

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
            int slot = s.Slot;

            // 观察目标（无涯 - 道士会长）全套道术神装首饰贴图
            string? iconFile = slot switch
            {
                2 => "StateItem_0111.png", // 勋章 (荣誉勋章)
                3 => "StateItem_0219.png", // 项链 (天珠项链)
                5 => "StateItem_0189.png", // 右手镯 (三眼手镯)
                6 => "StateItem_0189.png", // 左手镯 (三眼手镯)
                7 => "StateItem_0171.png", // 右戒指 (铂金戒指)
                8 => "StateItem_0171.png", // 左戒指 (铂金戒指)
                _ => null                  // 幽灵战衣、龙纹剑、道士头盔在纸娃娃层贴合
            };

            Texture2D? equipIcon = !string.IsNullOrEmpty(iconFile)
                ? ResPath.LoadImageTexture($"resources/wil/png/StateItem/{iconFile}")
                : null;

            var btn = new SlotButton
            {
                Name = $"InspectSlot_{slot}",
                CurrentSlotType = SlotButton.SlotType.Equip,
                SlotIndex = slot,
                Position = new Vector2(s.X, s.Y),
                Size = new Vector2(s.W, s.H),
                CustomMinimumSize = new Vector2(s.W, s.H),
                Flat = true,
                ItemTexture = equipIcon,
                ZIndex = 5
            };

            btn.MouseEntered += () =>
            {
                var (itemName, itemDesc) = slot switch
                {
                    0 => ("幽灵战衣(男)", "[color=#c0c0c0]重量 26   持久力 28/28[/color]\n[color=#ffff00]防御 4-7   魔御 3-4   道术 1-4[/color]\n[color=#c0c0c0]需要精神力 27[/color]"),
                    1 => ("龙纹剑", "[color=#c0c0c0]重量 40   持久力 22/22[/color]\n[color=#ffff00]攻击 8-20   道术 3-6[/color] [color=#00ffff]幸运+7[/color]\n[color=#c0c0c0]需要等级 35[/color]"),
                    2 => ("荣誉勋章", "[color=#c0c0c0]重量 1   持久力 50/50[/color]\n[color=#ffff00]道术 1-3[/color]"),
                    3 => ("天珠项链", "[color=#c0c0c0]重量 1   持久力 7/7[/color]\n[color=#ffff00]道术 1-5[/color]\n[color=#c0c0c0]需要精神力 22[/color]"),
                    4 => ("道士头盔", "[color=#c0c0c0]重量 3   持久力 8/8[/color]\n[color=#ffff00]防御 1-2   魔御 2-3[/color] [color=#00ffff](道术+1)[/color]\n[color=#c0c0c0]需要等级 24[/color]"),
                    5 => ("三眼手镯(右)", "[color=#c0c0c0]重量 1   持久力 7/7[/color]\n[color=#ffff00]防御 1-1   道术 1-3[/color]\n[color=#c0c0c0]需要精神力 22[/color]"),
                    6 => ("三眼手镯(左)", "[color=#c0c0c0]重量 1   持久力 7/7[/color]\n[color=#ffff00]防御 1-1   道术 1-3[/color]\n[color=#c0c0c0]需要精神力 22[/color]"),
                    7 => ("铂金戒指(右)", "[color=#c0c0c0]重量 1   持久力 5/5[/color]\n[color=#ffff00]防御 0-2   道术 0-4[/color]\n[color=#c0c0c0]需要精神力 17[/color]"),
                    8 => ("铂金戒指(左)", "[color=#c0c0c0]重量 1   持久力 5/5[/color]\n[color=#ffff00]防御 0-2   道术 0-4[/color]\n[color=#c0c0c0]需要精神力 17[/color]"),
                    _ => ("装备", "[color=#c0c0c0]重量 1   持久力 10/10[/color]")
                };
                if (_lblInspectDesc != null)
                {
                    _lblInspectDesc.Text = $"[color=#ffff40]{itemName}[/color]\n{itemDesc}";
                }
            };
            btn.MouseExited += () =>
            {
                if (_lblInspectDesc != null) _lblInspectDesc.Text = "";
            };

            _panel.AddChild(btn);
        }

        // 默认在底部内胆显示被观察玩家核心神装概览
        if (_lblInspectDesc != null)
        {
            _lblInspectDesc.Text = "[color=#ffff40]龙纹剑[/color]\n[color=#c0c0c0]持久 22/22[/color]  [color=#ffff00]道术 3-6[/color]  [color=#00ffff]幸运+7[/color]";
        }
    }

    private RichTextLabel? _lblInspectDesc;

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
