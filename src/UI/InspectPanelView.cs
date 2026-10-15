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
    public Official24DialogCompendium.DialogId DialogId => Official24DialogCompendium.DialogId.EquipPanel;
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
        _dollBg = new TextureRect
        {
            Name = "InspectDollBg",
            Position = new Vector2(38, 52),
            Size = new Vector2(168, 199),
            Texture = texDoll,
            MouseFilter = MouseFilterEnum.Ignore
        };
        _panel.AddChild(_dollBg);

        // 盛大官方 1.5 观察他人纸娃娃核心立绘层 (挂在 dollBg 下，展示被观察者道士神装纸娃娃)
        // 0. 发型层: 官方男角色发型 Prguse_0441 (24x16 @ 73, 39)
        var texHair = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0441.png");
        _dollHair = new TextureRect
        {
            Name = "InspectDollHair",
            Texture = texHair,
            Position = new Vector2(73, 39),
            Size = new Vector2(24, 16),
            MouseFilter = MouseFilterEnum.Ignore,
            ZIndex = 1,
            Visible = true
        };
        _dollBg?.AddChild(_dollHair);

        // 1. 衣服立绘层: StateItem_0064 (幽灵战衣男 80x129 @ 39, 63)
        var texCloth = ResPath.LoadImageTexture("resources/wil/png/StateItem/StateItem_0064.png");
        _dollCloth = new TextureRect
        {
            Name = "InspectDollCloth",
            Texture = texCloth,
            Position = new Vector2(39, 63),
            Size = new Vector2(80, 129),
            MouseFilter = MouseFilterEnum.Ignore,
            ZIndex = 2,
            Visible = false
        };
        _dollBg?.AddChild(_dollCloth);

        // 2. 武器立绘层: StateItem_0053 (龙纹剑 44x112 @ 14, 20，置于衣服上方手部咬合处)
        var texWeapon = ResPath.LoadImageTexture("resources/wil/png/StateItem/StateItem_0053.png");
        _dollWeapon = new TextureRect
        {
            Name = "InspectDollWeapon",
            Texture = texWeapon,
            Position = new Vector2(14, 20),
            Size = new Vector2(44, 112),
            MouseFilter = MouseFilterEnum.Ignore,
            ZIndex = 3,
            Visible = false
        };
        _dollBg?.AddChild(_dollWeapon);

        // 3. 头盔立绘层: StateItem_0108 (道士头盔 20x23 @ 74, 42，严丝合缝罩在头部)
        var texHelm = ResPath.LoadImageTexture("resources/wil/png/StateItem/StateItem_0108.png");
        _dollHelm = new TextureRect
        {
            Name = "InspectDollHelm",
            Texture = texHelm,
            Position = new Vector2(74, 42),
            Size = new Vector2(20, 23),
            MouseFilter = MouseFilterEnum.Ignore,
            ZIndex = 4,
            Visible = false
        };
        _dollBg?.AddChild(_dollHelm);

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
        // 0:衣服, 1:武器, 2:照明/右持物(火把/蜡烛/护符/毒药/药粉), 3:项链, 4:头盔, 5:右手镯, 6:左手镯, 7:右戒指, 8:左戒指
        var slotDefs = new (int Slot, float X, float Y, float W, float H)[]
        {
            (1, 47, 80, 47, 87),    // 武器 (0x398, IDA 0x4840f6..0x484134: Left=47, Top=80, W=47, H=87)
            (0, 96, 122, 53, 112),  // 衣服 (0x39C, IDA 0x484142..0x484180: Left=96, Top=122, W=53, H=112)
            (4, 115, 93, 18, 18),   // 头盔 (0x3A0, IDA 0x483f2e..0x483f6c: Left=115, Top=93, W=18, H=18)
            (3, 168, 87, 34, 31),   // 项链 (0x380, IDA 0x483ee2..0x483f20: Left=168, Top=87, W=34, H=31)
            (2, 168, 125, 34, 31),  // 照明/右持物 (0x384, IDA 0x483f7a..0x483fb8: Left=168, Top=125, W=34, H=31)
            (6, 42, 176, 34, 31),   // 左手镯 (0x388, IDA 0x483fc6..0x484004: Left=42, Top=176, W=34, H=31)
            (5, 168, 176, 34, 31),  // 右手镯 (0x38C, IDA 0x484012..0x484050: Left=168, Top=176, W=34, H=31)
            (8, 42, 215, 34, 31),   // 左戒指/符 (0x390, IDA 0x48405e..0x48409c: Left=42, Top=215, W=34, H=31)
            (7, 168, 215, 34, 31),  // 右戒指 (0x394, IDA 0x4840aa..0x4840e8: Left=168, Top=215, W=34, H=31)
        };

        foreach (var s in slotDefs)
        {
            int slot = s.Slot;

            var btn = new SlotButton
            {
                Name = $"InspectSlot_{slot}",
                CurrentSlotType = SlotButton.SlotType.Equip,
                SlotIndex = slot,
                Position = new Vector2(s.X, s.Y),
                Size = new Vector2(s.W, s.H),
                CustomMinimumSize = new Vector2(s.W, s.H),
                Flat = true,
                ItemTexture = null,
                ZIndex = 5
            };

            btn.MouseEntered += () =>
            {
                if (_lblInspectDesc != null && _slotDesc.TryGetValue(slot, out var d))
                {
                    _lblInspectDesc.Text = d;
                }
            };
            btn.MouseExited += () =>
            {
                if (_lblInspectDesc != null) _lblInspectDesc.Text = "";
            };

            _slotButtons[slot] = btn;
            _panel.AddChild(btn);
        }
    }

    private TextureRect? _dollHair;
    private TextureRect? _dollCloth;
    private TextureRect? _dollWeapon;
    private TextureRect? _dollHelm;
    private TextureRect? _dollBg;
    private readonly System.Collections.Generic.Dictionary<int, SlotButton> _slotButtons = new();
    private readonly System.Collections.Generic.Dictionary<int, string> _slotDesc = new();
    private RichTextLabel? _lblInspectDesc;

    /// <summary>
    /// 设置纸娃娃发型。资源位于 Prguse WIL (男440, 女480)。
    /// </summary>
    public void SetDollHair(int hairIndex = 1, bool isFemale = false)
    {
        if (_dollHair == null) return;
        string hairFile;
        Vector2 hairPos;
        if (isFemale)
        {
            int imgIdx = (hairIndex == 2) ? 483 : 482;
            hairFile = $"resources/wil/png/Prguse/Prguse_{imgIdx:D4}.png";
            hairPos = new Vector2(75, 42);
        }
        else
        {
            int baseIdx = 440;
            int imgIdx = baseIdx + Math.Max(1, hairIndex);
            hairFile = $"resources/wil/png/Prguse/Prguse_{imgIdx:D4}.png";
            hairPos = new Vector2(76, 45);
        }
        var tex = ResPath.LoadImageTexture(hairFile);
        if (tex != null)
        {
            _dollHair.Texture = tex;
            _dollHair.Size = tex.GetSize();
            _dollHair.Position = hairPos;
            _dollHair.Visible = true;
        }
    }

    /// <summary>
    /// 设置纸娃娃立绘(衣服、武器、头盔)。资源位于 StateItem WIL。
    /// </summary>
    public void SetDollCloth(string? clothFile)
    {
        if (_dollCloth != null)
        {
            if (!string.IsNullOrEmpty(clothFile))
            {
                _dollCloth.Texture = ResPath.LoadImageTexture($"resources/wil/png/StateItem/{clothFile}");
                _dollCloth.Visible = _dollCloth.Texture != null;
            }
            else
            {
                _dollCloth.Texture = null;
                _dollCloth.Visible = false;
            }
        }
    }

    public void SetDollWeapon(string? weaponFile)
    {
        if (_dollWeapon != null)
        {
            if (!string.IsNullOrEmpty(weaponFile))
            {
                _dollWeapon.Texture = ResPath.LoadImageTexture($"resources/wil/png/StateItem/{weaponFile}");
                _dollWeapon.Visible = _dollWeapon.Texture != null;
            }
            else
            {
                _dollWeapon.Texture = null;
                _dollWeapon.Visible = false;
            }
        }
    }

    public void SetDollHelm(string? helmFile)
    {
        if (_dollHelm != null)
        {
            if (!string.IsNullOrEmpty(helmFile))
            {
                _dollHelm.Texture = ResPath.LoadImageTexture($"resources/wil/png/StateItem/{helmFile}");
                _dollHelm.Visible = _dollHelm.Texture != null;
                if (_dollHair != null) _dollHair.Visible = false; // 戴头盔覆盖头发
            }
            else
            {
                _dollHelm.Texture = null;
                _dollHelm.Visible = false;
                if (_dollHair != null) _dollHair.Visible = true; // 脱头盔显露头发
            }
        }
    }

    /// <summary>
    /// 设置纸娃娃立绘(衣服、武器、头盔)。资源位于 StateItem WIL。
    /// </summary>
    public void SetDoll(string? clothFile, string? weaponFile, string? helmFile)
    {
        SetDollCloth(clothFile);
        SetDollWeapon(weaponFile);
        SetDollHelm(helmFile);
    }

    /// <summary>
    /// 设置目标角色性别 (0=男 Prguse_0376, 1=女 Prguse_0377, IDA sub_49219C [0x49226f])
    /// </summary>
    public void SetGender(int gender)
    {
        string pic = gender == 1 ? "Prguse_0377.png" : "Prguse_0376.png";
        if (_dollBg != null)
        {
            _dollBg.Texture = ResPath.LoadImageTexture($"resources/wil/png/Prguse/{pic}");
        }
    }

    /// <summary>设置观察目标某装备位(来源:观察协议解析结果或 e2e mock);iconFile 为 StateItem 资源名。</summary>
    public void SetSlotItem(int slot, string? iconFile, string? bbDesc)
    {
        if (_slotButtons.TryGetValue(slot, out var btn))
        {
            // 盛大官方 1.5 原生规范：衣服(0)、武器(1)、头盔(4)属于纸娃娃(Doll)全身穿戴，
            // 槽位热区保持透明仅用于鼠标悬停 Tooltip 交互，严禁在槽位按钮重复贴图，彻底杜绝幽灵双影重叠！
            if (slot == 0)
            {
                btn.ItemTexture = null;
                if (!string.IsNullOrEmpty(iconFile)) SetDollCloth(iconFile);
            }
            else if (slot == 1)
            {
                btn.ItemTexture = null;
                if (!string.IsNullOrEmpty(iconFile)) SetDollWeapon(iconFile);
            }
            else if (slot == 4)
            {
                btn.ItemTexture = null;
                if (!string.IsNullOrEmpty(iconFile)) SetDollHelm(iconFile);
            }
            else
            {
                btn.ItemTexture = string.IsNullOrEmpty(iconFile) ? null : ResPath.LoadImageTexture($"resources/wil/png/StateItem/{iconFile}");
            }
        }
        if (string.IsNullOrEmpty(bbDesc)) _slotDesc.Remove(slot); else _slotDesc[slot] = bbDesc;
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
