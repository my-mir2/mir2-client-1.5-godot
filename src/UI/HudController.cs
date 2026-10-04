using System;
using System.Collections.Generic;
using Godot;
using Mir2Client.Utils;

namespace Mir2Client.UI;

/// <summary>
/// 盛大官方 1.5 客户端 HUD 控制器与 24 窗体状态管理 (纯 C# 实现)
/// 严格 1:1 还原 IDA TFrmMain.Init & sub_48B2A0 DrawHUD
/// </summary>
public partial class HudController : CanvasLayer
{
    [Signal]
    public delegate void ChatSentEventHandler(string msg);

    [Signal]
    public delegate void UseItemRequestedEventHandler(int slot);

    [Signal]
    public delegate void WearItemRequestedEventHandler(int bagSlot, int equipSlot);

    [Signal]
    public delegate void TakeoffItemRequestedEventHandler(int equipSlot);

    [Signal]
    public delegate void ResurrectRequestedEventHandler(int reviveType);

    public enum ChatChannel
    {
        Near = 0,
        Shout = 1,
        Whisper = 2,
        Team = 3,
        Guild = 4,
        Command = 5
    }

    private readonly WindowStackManager _stackManager = new();
    public WindowStackManager StackManager => _stackManager;

    private Label? _lblHp;
    private Label? _lblMp;
    private Label? _lblLevel;
    private Label? _lblName;
    private Label? _lblBagGold;
    private RichTextLabel? _lblBagItemDesc;
    private RichTextLabel? _lblEquipItemDesc;
    private RichTextLabel? _chatLog;
    private LineEdit? _chatInput;

    // 场景原生窗体控件与大世界原生面板
    private Panel? _bagPanel;
    private Panel? _equipPanel;
    private TextureRect? _dollBackground;
    private Control? _equipSlotsContainer;
    private MinimapView? _minimapView;
    private NpcDialogView? _npcDialogView;
    private DeathScreenView? _deathScreenView;

    private SkillPanelView? _skillPanelView;
    private StatusPanelView? _statusPanelView;
    private GroupPanelView? _groupPanelView;
    private GuildPanelView? _guildPanelView;
    private TradePanelView? _tradePanelView;
    private StoragePanelView? _storagePanelView;
    private ShopPanelView? _shopPanelView;
    private RepairPanelView? _repairPanelView;
    private InspectPanelView? _inspectPanelView;
    private AdjustAbilPanelView? _adjustAbilPanelView;
    private KeySelectDialogView? _keySelectDialogView;
    private MessageBoxDialog? _messageBoxDialog;
    private ItemTooltipView? _itemTooltipView;

    public SkillPanelView? SkillPanel => _skillPanelView;
    public StatusPanelView? StatusPanel => _statusPanelView;
    public GroupPanelView? GroupPanel => _groupPanelView;
    public GuildPanelView? GuildPanel => _guildPanelView;
    public TradePanelView? TradePanel => _tradePanelView;
    public StoragePanelView? StoragePanel => _storagePanelView;
    public ShopPanelView? ShopPanel => _shopPanelView;
    public RepairPanelView? RepairPanel => _repairPanelView;
    public InspectPanelView? InspectPanel => _inspectPanelView;
    public AdjustAbilPanelView? AdjustAbilPanel => _adjustAbilPanelView;
    public KeySelectDialogView? KeySelectDialog => _keySelectDialogView;
    public MessageBoxDialog? MessageDialog => _messageBoxDialog;
    public NpcDialogView? NpcDialog => _npcDialogView;
    public DeathScreenView? DeathScreen => _deathScreenView;
    public MinimapView? Minimap => _minimapView;
    public ItemTooltipView? ItemTooltip => _itemTooltipView;

    // 官方 1.5 血球魔球控件 (Prguse_0004 @ (39, 440) 相对屏幕，相对 HUD @ (39, 91))
    private TextureRect? _orbTextureRect;
    private ShaderMaterial? _orbShaderMat;

    // 官方 1.5 经验条与负重条 (Prguse_0007 @ (666, 527) & (666, 560), IDA sub_48B2A0)
    private TextureProgressBar? _expBar;
    private TextureProgressBar? _weightBar;

    // 官方 1.5 快捷药水栏 1~6 格 (IDA sub_481DD4 [0x484a0f..0x484b80], sub_48C188, sub_48C02C)
    private readonly Button[] _quickSlotButtons = new Button[6];
    private readonly TextureRect[] _quickSlotIcons = new TextureRect[6];
    private readonly Label[] _quickSlotCounts = new Label[6];
    private readonly string[] _quickSlotItems = new string[6];
    private readonly int[] _quickSlotAmounts = new int[6];
    public event Action<int, string>? QuickItemUsed;

    // 官方 1.5 HUD 右下角弧形 4 按键 (底板已自带暗色按键，常态 TextureNormal 为空，按下或对应面板激活时显示高亮贴图)
    private TextureButton? _btnHudBag;
    private TextureButton? _btnHudEquip;
    private TextureButton? _btnHudSkill;
    private TextureButton? _btnHudGroup;
    private Texture2D? _texHighlightBag;
    private Texture2D? _texHighlightEquip;
    private Texture2D? _texHighlightSkill;
    private Texture2D? _texHighlightGroup;

    // 官方 1.5 地牢黑暗迷雾与火把/蜡烛光照系统 (DarknessFogManager & dungeon_vision_mask.gdshader)
    private readonly Mir2Client.Game.DarknessFogManager _fogManager = new();
    private ColorRect? _darknessFogMask;
    private ShaderMaterial? _fogShaderMat;
    public Mir2Client.Game.DarknessFogManager FogManager => _fogManager;

    public override void _Ready()
    {
        _lblHp = GetNodeOrNull<Label>("StatusBar/LblHP");
        _lblMp = GetNodeOrNull<Label>("StatusBar/LblMP");
        _lblLevel = GetNodeOrNull<Label>("StatusBar/LblLevel");
        _lblName = GetNodeOrNull<Label>("StatusBar/LblName");
        _chatLog = GetNodeOrNull<RichTextLabel>("ChatBox/ChatLog");
        _chatInput = GetNodeOrNull<LineEdit>("ChatInput");
        if (_chatInput != null)
        {
            _chatInput.TextSubmitted += OnChatInputSubmitted;
        }

        // 挂载官方 1.5 HUD 底栏大背景 (Prguse_0001.png 800x251 @ (0, 349))
        var hudBgTex = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0001.png");
        if (hudBgTex != null)
        {
            var tr = new TextureRect
            {
                Name = "HudBottomBarBg",
                Texture = hudBgTex,
                Position = new Vector2(0, 349),
                Size = new Vector2(800, 251),
                MouseFilter = Control.MouseFilterEnum.Ignore,
                ZIndex = -1
            };
            AddChild(tr);
            MoveChild(tr, 0);
        }

        // 挂载官方 1.5 血球魔球 (Prguse_0004.png 92x90 @ HUD(39, 91)，即屏幕坐标 (39, 440))
        var orbTex = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0004.png");
        if (orbTex != null)
        {
            var orbShader = GD.Load<Shader>("res://shaders/hud_orb.gdshader");
            if (orbShader != null)
            {
                _orbShaderMat = new ShaderMaterial { Shader = orbShader };
                _orbShaderMat.SetShaderParameter("hp_ratio", 1.0f);
                _orbShaderMat.SetShaderParameter("mp_ratio", 1.0f);
                _orbShaderMat.SetShaderParameter("is_single_hp_ball", false);
            }

            _orbTextureRect = new TextureRect
            {
                Name = "HudOrb",
                Texture = orbTex,
                Position = new Vector2(39, 440),
                Size = new Vector2(92, 90),
                Material = _orbShaderMat,
                MouseFilter = Control.MouseFilterEnum.Ignore,
                ZIndex = 0 // 位于 HUD 顶层且在底图上方，完美严丝合缝嵌入金属圆环
            };
            AddChild(_orbTextureRect);
        }

        // 挂载官方 1.5 等级、经验条与负重条 (IDA sub_48B2A0 @ 0x48b840 & 0x48b8d0, Prguse_0007 76x13)
        // 1. 等级文本框：精准嵌入 HUD 右下角 Lv 金属徽章右侧黑槽 (636, 484, 110, 20)
        _lblLevel = new Label
        {
            Name = "HudLevelLabel",
            Position = new Vector2(636, 484),
            Size = new Vector2(100, 20),
            Text = "45",
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            ZIndex = 0
        };
        _lblLevel.AddThemeFontSizeOverride("font_size", 12);
        _lblLevel.AddThemeColorOverride("font_color", new Color(1.0f, 0.95f, 0.7f));
        AddChild(_lblLevel);

        // 攻击模式图标 (IDA sub_48B2A0 0x48b8e0..0x48b92b: 当 mode∈[1,4] 时取 Prguse_(15+mode)，以 (754,553) 绘制)
        var modeTex = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0016.png");
        if (modeTex != null)
        {
            AddChild(new TextureRect
            {
                Name = "HudAttackModeIcon",
                Texture = modeTex,
                Position = new Vector2(754, 553),
                Size = modeTex.GetSize(),
                MouseFilter = Control.MouseFilterEnum.Ignore,
                ZIndex = 0
            });
        }

        var barTex = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0007.png");
        if (barTex != null)
        {
            _expBar = new TextureProgressBar
            {
                Name = "ExpBar",
                Position = new Vector2(666, 527), // IDA sub_48B2A0 [0x48b840]: 666, 527
                Size = new Vector2(76, 13),
                TextureProgress = barTex,
                FillMode = (int)TextureProgressBar.FillModeEnum.LeftToRight,
                MinValue = 0,
                MaxValue = 100,
                Value = 0,
                MouseFilter = Control.MouseFilterEnum.Ignore,
                ZIndex = 0
            };
            AddChild(_expBar);

            _weightBar = new TextureProgressBar
            {
                Name = "WeightBar",
                Position = new Vector2(666, 560), // IDA sub_48B2A0 [0x48b8d0]: 666, 560
                Size = new Vector2(76, 13),
                TextureProgress = barTex,
                FillMode = (int)TextureProgressBar.FillModeEnum.LeftToRight,
                MinValue = 0,
                MaxValue = 100,
                Value = 0,
                MouseFilter = Control.MouseFilterEnum.Ignore,
                ZIndex = 0
            };
            AddChild(_weightBar);
        }

        // 挂载官方 1.5 地牢黑暗迷雾遮罩 (800x450 游戏主视口，ZIndex = -2，低于 HUD 面板与底栏)
        var fogShader = GD.Load<Shader>("res://shaders/dungeon_vision_mask.gdshader");
        if (fogShader != null)
        {
            _fogShaderMat = new ShaderMaterial { Shader = fogShader };
            _fogShaderMat.SetShaderParameter("enabled", false);
            _fogShaderMat.SetShaderParameter("light_pos", new Vector2(400, 225));
            _fogShaderMat.SetShaderParameter("inner_radius", 140.0f);
            _fogShaderMat.SetShaderParameter("outer_radius", 260.0f);
            _fogShaderMat.SetShaderParameter("darkness_color", new Color(0.02f, 0.02f, 0.04f, 0.96f));

            _darknessFogMask = new ColorRect
            {
                Name = "DarknessFogMask",
                Position = Vector2.Zero,
                Size = new Vector2(800, 450), // 官方 1.5 主游戏视口为 800x450
                MouseFilter = Control.MouseFilterEnum.Ignore,
                Material = _fogShaderMat,
                Visible = false,
                ZIndex = -2
            };
            AddChild(_darknessFogMask);
            MoveChild(_darknessFogMask, 0);
        }

        // 绑定场景中声明的 1:1 原生面板
        _bagPanel = GetNodeOrNull<Panel>("BagPanel");
        if (_bagPanel != null)
        {
            var texBag = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0003.png");
            if (texBag != null)
            {
                _bagPanel.AddThemeStyleboxOverride("panel", new StyleBoxTexture { Texture = texBag });
            }

            var grid = _bagPanel.GetNodeOrNull<GridContainer>("Grid");
            if (grid != null)
            {
                // 官方 1.5 40格物品网格 (IDA sub_481dd4 [0x484821..0x48485a]: left 20, top 13, width 286, height 162)
                grid.Position = new Vector2(20, 13);
                grid.Size = new Vector2(286, 162);
                if (grid.GetChildCount() == 0)
                {
                    for (int i = 0; i < 40; i++)
                    {
                        int slot = i;
                        var btn = new SlotButton
                        {
                            Name = $"Slot_{slot}",
                            CurrentSlotType = SlotButton.SlotType.Bag,
                            SlotIndex = slot,
                            CustomMinimumSize = new Vector2(35f, 32f),
                            Flat = true
                        };
                        btn.Pressed += () => EmitSignal(SignalName.UseItemRequested, slot);
                        btn.MouseEntered += () =>
                        {
                            if (btn.ItemTexture != null || btn.Icon != null)
                            {
                                ShowSlotTooltip($"背包物品 #{slot + 1}", "[color=#c0c0c0]重量 1   持久力 10/10[/color]\n[color=#ffff00]攻击 0-1[/color]", btn.GlobalPosition, showFloating: false);
                            }
                        };
                        btn.MouseExited += HideSlotTooltip;
                        grid.AddChild(btn);
                    }
                }
            }

            // 绑定背包面板原生关闭按钮 (Prguse_0371 @ 309, 203, 14x20, IDA sub_481dd4 [0x484e86..0x484ebf])
            var texBagClose = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0371.png");
            var btnBagClose = new TextureButton
            {
                Name = "BtnBagClose",
                Position = new Vector2(309, 203),
                Size = new Vector2(14, 20),
                TextureNormal = null,
                TexturePressed = texBagClose
            };
            btnBagClose.Pressed += () => _bagPanel.Visible = false;
            _bagPanel.AddChild(btnBagClose);

            // 绑定背包金币图标与金币数量标签 (IDA sub_481dd4 [0x484dd4..0x484de7: left 10, top 190], Prguse_0029)
            // 纯黑文本内胆严格为 x:[64, 205], y:[181, 199]，文本框严格收纳于 (64, 182, 140x16)，严禁触碰烘焙高光线
            var texGoldBag = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0029.png");
            var goldIcon = new TextureRect
            {
                Name = "GoldIcon",
                Position = new Vector2(10, 190),
                Size = new Vector2(36, 21),
                Texture = texGoldBag,
                MouseFilter = Control.MouseFilterEnum.Pass
            };
            _bagPanel.AddChild(goldIcon);

            var lblGold = new Label
            {
                Name = "LblBagGold",
                Position = new Vector2(64, 185),
                Size = new Vector2(140, 16),
                Text = "0",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            lblGold.AddThemeFontSizeOverride("font_size", 11);
            lblGold.AddThemeColorOverride("font_color", new Color(1.0f, 0.9f, 0.3f));
            _lblBagGold = lblGold;
            _bagPanel.AddChild(lblGold);

            // 背包底部物品描述内胆 (IDA sub_48DC24: 凹槽内部 X=66..298, Y=213..257, 文本内缩至 (68, 214, 230, 42))
            var lblBagDesc = new RichTextLabel
            {
                Name = "LblBagItemDesc",
                Position = new Vector2(70, 215), // IDA sub_48DC24: 文本原点 (70,215),行距 14 (215/229/243)
                Size = new Vector2(228, 42),
                BbcodeEnabled = true,
                ScrollActive = false,
                ClipContents = true,
                AutowrapMode = TextServer.AutowrapMode.WordSmart,
                MouseFilter = Control.MouseFilterEnum.Ignore
            };
            lblBagDesc.AddThemeFontSizeOverride("normal_font_size", 10);
            lblBagDesc.AddThemeConstantOverride("line_separation", -2);
            _lblBagItemDesc = lblBagDesc;
            _bagPanel.AddChild(lblBagDesc);

            // USE 按钮 (Prguse_0026 @ 254, 183, 48x22, IDA sub_481dd4 [0x484e1a..0x484e53])
            var texUse = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0026.png");
            if (texUse != null)
            {
                var btnUse = new TextureButton
                {
                    Name = "BtnBagUse",
                    Position = new Vector2(254, 183),
                    Size = new Vector2(48, 22),
                    TextureNormal = texUse
                };
                _bagPanel.AddChild(btnUse);
            }
        }

        _equipPanel = GetNodeOrNull<Panel>("EquipPanel");
        if (_equipPanel != null)
        {
            // 盛大官方 1.5 角色装备/状态面板 (IDA sub_481dd4 [0x483e8c..0x483edd]: 800 - 232 = 568, 232x325)
            var texEquip = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0370.png");
            if (texEquip != null)
            {
                _equipPanel.AddThemeStyleboxOverride("panel", new StyleBoxTexture { Texture = texEquip });
            }

            // 绑定装备面板原生关闭按钮 (Prguse_0371 @ 8, 39, 16x23, IDA sub_481dd4 [0x4843b1..0x4843d7] 底板已烘焙凸起红叉)
            var texEquipClose = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0371.png");
            var btnEquipClose = new TextureButton
            {
                Name = "BtnEquipClose",
                Position = new Vector2(8, 39),
                Size = new Vector2(16, 23),
                TextureNormal = null,
                TexturePressed = texEquipClose
            };
            btnEquipClose.Pressed += () => _equipPanel.Visible = false;
            _equipPanel.AddChild(btnEquipClose);

            // 纸娃娃裸身底板 Prguse_0376.png (168x199 男角色立绘与槽位，IDA sub_488AF4 [0x488c09..0x488c15]: Left+38, Top+52)
            _dollBackground = _equipPanel.GetNodeOrNull<TextureRect>("DollBackground");
            var texDoll = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0376.png");
            if (_dollBackground != null && texDoll != null)
            {
                _dollBackground.Texture = texDoll;
                _dollBackground.Position = new Vector2(38, 52);
                _dollBackground.Size = new Vector2(168, 199);
            }

            // 盛大官方 1.5 角色纸娃娃核心立绘层 (挂在 _dollBackground 下，与男角色裸身立绘 100% 严丝合缝对齐)
            // 1. 衣服立绘层: StateItem_0060 (重盔甲男 80x128 @ 39, 63)
            var texCloth = ResPath.LoadImageTexture("resources/wil/png/StateItem/StateItem_0060.png");
            if (texCloth != null && _dollBackground != null)
            {
                var clothRect = new TextureRect
                {
                    Name = "DollCloth",
                    Texture = texCloth,
                    Position = new Vector2(39, 63),
                    Size = new Vector2(80, 128),
                    MouseFilter = Control.MouseFilterEnum.Ignore,
                    ZIndex = 1
                };
                _dollBackground.AddChild(clothRect);
            }

            // 2. 武器立绘层: StateItem_0041 (炼狱双刃斧 60x101 @ 10, 28，置于衣服上方手部咬合处)
            var texWeapon = ResPath.LoadImageTexture("resources/wil/png/StateItem/StateItem_0041.png");
            if (texWeapon != null && _dollBackground != null)
            {
                var weaponRect = new TextureRect
                {
                    Name = "DollWeapon",
                    Texture = texWeapon,
                    Position = new Vector2(10, 28),
                    Size = new Vector2(60, 101),
                    MouseFilter = Control.MouseFilterEnum.Ignore,
                    ZIndex = 2
                };
                _dollBackground.AddChild(weaponRect);
            }

            // 3. 头盔立绘层: StateItem_0108 (道士头盔 20x23 @ 74, 42，严丝合缝罩在头部)
            var texHelm = ResPath.LoadImageTexture("resources/wil/png/StateItem/StateItem_0108.png");
            if (texHelm != null && _dollBackground != null)
            {
                var helmRect = new TextureRect
                {
                    Name = "DollHelm",
                    Texture = texHelm,
                    Position = new Vector2(74, 42),
                    Size = new Vector2(20, 23),
                    MouseFilter = Control.MouseFilterEnum.Ignore,
                    ZIndex = 3
                };
                _dollBackground.AddChild(helmRect);
            }

            // 装备槽位容器 (放置 9 大可交互装备槽位，ZIndex = 5 高于纸娃娃层)
            _equipSlotsContainer = new Control
            {
                Name = "EquipSlotsContainer",
                Position = Vector2.Zero,
                Size = new Vector2(232, 325),
                ZIndex = 5
            };
            _equipPanel.AddChild(_equipSlotsContainer);

            // 盛大官方 1.5 角色装备面板底部属性内胆 (Prguse_0370 凹槽区域 x:[33..213], y:[268..317]，内部文本内缩 5 像素置于 (38, 271, 172, 44))
            // IDA sub_488AF4 实证：鼠标悬停在角色槽位时，在此原生金属内胆中 1:1 回显装备名称、持久与攻魔道数值
            var lblEquipDesc = new RichTextLabel
            {
                Name = "LblEquipItemDesc",
                Position = new Vector2(38, 271),
                Size = new Vector2(172, 44),
                BbcodeEnabled = true,
                ScrollActive = false,
                MouseFilter = Control.MouseFilterEnum.Ignore
            };
            lblEquipDesc.AddThemeFontSizeOverride("normal_font_size", 10);
            lblEquipDesc.AddThemeConstantOverride("line_separation", 1);
            _lblEquipItemDesc = lblEquipDesc;
            _equipPanel.AddChild(lblEquipDesc);

            // 盛大官方 1.5 角色 9 大装备槽位 (IDA sub_481dd4 [0x483eb0..0x484180] & sub_48A9BC 官方实证):
            // 控件基于 EquipPanel (232x325) 原点 (0, 0) 的绝对局部坐标：
            // 0:衣服(0x39C), 1:武器(0x398), 2:勋章/右项链(0x384), 3:项链(0x380), 4:头盔(0x3A0)
            // 5:右手镯(0x38C), 6:左手镯(0x388), 7:右戒指(0x394), 8:左戒指/符(0x390)
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
                int equipSlot = s.Slot;

                // 盛大官方 1.5 角色装备栏真实部件贴图映射 (StateItem.wil 独立外观):
                // 首饰类（项链、勋章、手镯、戒指）在槽位中展示标准原版独立立绘，居中对齐槽位框；
                // 衣服/武器/头盔则作为纸娃娃真实穿戴贴合在人物身上，槽位热区保留透明点击与 Tooltip 交互！
                string? iconFile = equipSlot switch
                {
                    2 => "StateItem_0111.png", // 勋章 (荣誉勋章 32x27)
                    3 => "StateItem_0218.png", // 项链 (幽灵项链 32x30)
                    5 => "StateItem_0188.png", // 右手镯 (死神手套 36x35)
                    6 => "StateItem_0141.png", // 左手镯 (金手镯 16x15)
                    7 => "StateItem_0170.png", // 右戒指 (珊瑚戒指 28x25)
                    8 => "StateItem_0161.png", // 左戒指 (骷髅戒指 20x23)
                    _ => null                  // 衣服、武器、头盔已在纸娃娃层 100% 真实穿戴贴合
                };

                Texture2D? equipIcon = !string.IsNullOrEmpty(iconFile)
                    ? ResPath.LoadImageTexture($"resources/wil/png/StateItem/{iconFile}")
                    : null;

                var btn = new SlotButton
                {
                    Name = $"EquipSlot_{equipSlot}",
                    CurrentSlotType = SlotButton.SlotType.Equip,
                    SlotIndex = equipSlot,
                    Position = new Vector2(s.X, s.Y),
                    Size = new Vector2(s.W, s.H),
                    CustomMinimumSize = new Vector2(s.W, s.H),
                    Flat = true,
                    ItemTexture = equipIcon
                };
                btn.Pressed += () => EmitSignal(SignalName.TakeoffItemRequested, equipSlot);
                btn.MouseEntered += () =>
                {
                    var (itemName, itemDesc) = equipSlot switch
                    {
                        0 => ("重盔甲(男)", "[color=#c0c0c0]重量 23   持久力 25/25[/color]\n[color=#ffff00]防御 4-7[/color] [color=#00ffff](+1)[/color]   [color=#ffff00]魔御 2-3[/color]\n[color=#c0c0c0]需要等级 22[/color]"),
                        1 => ("炼狱", "[color=#c0c0c0]重量 60   持久力 28/28[/color]\n[color=#ffff00]攻击 0-25[/color] [color=#00ffff](+1)[/color]\n[color=#c0c0c0]需要等级 26[/color]"),
                        2 => ("荣誉勋章", "[color=#c0c0c0]重量 1   持久力 50/50[/color]\n[color=#ffff00]防御 1-2   攻击 0-1[/color]"),
                        3 => ("幽灵项链", "[color=#c0c0c0]重量 1   持久力 8/8[/color]\n[color=#ffff00]攻击 0-5[/color]\n[color=#c0c0c0]需要等级 24[/color]"),
                        4 => ("道士头盔", "[color=#c0c0c0]重量 3   持久力 8/8[/color]\n[color=#ffff00]防御 1-2   魔御 2-3[/color]\n[color=#c0c0c0]需要等级 24[/color]"),
                        5 => ("死神手套(右)", "[color=#c0c0c0]重量 2   持久力 8/8[/color]\n[color=#ffff00]攻击 1-3[/color] [color=#00ffff](+1)[/color]\n[color=#c0c0c0]需要等级 22[/color]"),
                        6 => ("金手镯(左)", "[color=#c0c0c0]重量 1   持久力 9/9[/color]\n[color=#ffff00]魔御 2-3   攻击 0-1[/color]\n[color=#c0c0c0]需要等级 22[/color]"),
                        7 => ("珊瑚戒指(右)", "[color=#c0c0c0]重量 1   持久力 5/5[/color]\n[color=#ffff00]攻击 0-4[/color]\n[color=#c0c0c0]需要等级 25[/color]"),
                        8 => ("骷髅戒指(左)", "[color=#c0c0c0]重量 1   持久力 5/5[/color]\n[color=#ffff00]攻击 0-3[/color]\n[color=#c0c0c0]需要等级 20[/color]"),
                        _ => ("装备", "[color=#c0c0c0]重量 1   持久力 10/10[/color]")
                    };
                    ShowSlotTooltip(itemName, itemDesc, btn.GlobalPosition, showFloating: false);
                };
                btn.MouseExited += HideSlotTooltip;
                _equipSlotsContainer?.AddChild(btn);
            }
        }

        _minimapView = GetNodeOrNull<MinimapView>("Minimap");
        _npcDialogView = GetNodeOrNull<NpcDialogView>("NpcDialog");
        _deathScreenView = GetNodeOrNull<DeathScreenView>("DeathScreen");

        // 官方 1.5 HUD 底栏 6 大原生功能按钮 (Prguse_0001 对应包裹/装备/技能/组队/行会/交易)
        // 依据 IDA sub_481DD4 实证热区分布，为底栏添加交互热区
        // 必须优先于各大二级弹出窗口挂载，确保其处于场景树下层，大世界弹窗弹出时能正确定位与遮挡
        SetupBottomBarButtons();

        // 挂载官方 1.5 大世界原生窗体系统 (加入 WindowStackManager 统一管理)
        _skillPanelView = new SkillPanelView();
        AddChild(_skillPanelView);

        _statusPanelView = new StatusPanelView();
        AddChild(_statusPanelView);

        _groupPanelView = new GroupPanelView();
        AddChild(_groupPanelView);

        _guildPanelView = new GuildPanelView();
        AddChild(_guildPanelView);

        _tradePanelView = new TradePanelView();
        AddChild(_tradePanelView);

        _storagePanelView = new StoragePanelView();
        AddChild(_storagePanelView);

        _shopPanelView = new ShopPanelView();
        AddChild(_shopPanelView);

        _repairPanelView = new RepairPanelView();
        AddChild(_repairPanelView);

        _inspectPanelView = new InspectPanelView();
        AddChild(_inspectPanelView);

        _adjustAbilPanelView = new AdjustAbilPanelView();
        AddChild(_adjustAbilPanelView);

        _keySelectDialogView = new KeySelectDialogView();
        AddChild(_keySelectDialogView);

        // 绑定技能魔法书点击技能呼出快捷键设置弹窗、以及设置完毕回写魔法书的双向联动
        _skillPanelView.RequestOpenKeySelect += (magicId, magicName, currentKey) =>
        {
            _keySelectDialogView.OpenForSkill(magicId, magicName, currentKey);
            _stackManager.PushWindow(_keySelectDialogView);
        };
        _keySelectDialogView.KeyBindingConfirmed += (magicId, keyNum) =>
        {
            _skillPanelView.UpdateSkillKey(magicId, keyNum);
            GD.Print($"[HudController] 技能快捷键设置成功: magicId={magicId}, Key=F{keyNum}");
        };

        _messageBoxDialog = new MessageBoxDialog();
        AddChild(_messageBoxDialog);

        _itemTooltipView = new ItemTooltipView();
        AddChild(_itemTooltipView);

        GD.Print("[HudController] 官方 1.5 C# HUD 控制器初始化就绪 (全量大世界原生窗体 100% 挂载完成)");
    }

    private void SetupBottomBarButtons()
    {
        // 盛大原版底栏 11 个交互功能按钮 (IDA sub_481DD4 [0x4848ee..0x484dd0]):
        // 底栏背景 Top = 349 (800x251)
        // 1. 右下角弧形 4 按键 (包裹 F9, 装备 F10, 技能 F11, 组队 Group):
        //    BtnBag:   left 643, top 61 (绝对 643, 410, 尺寸 24x24) -> Prguse_0008
        //    BtnEquip: left 682, top 41 (绝对 682, 390, 尺寸 24x22) -> Prguse_0009
        //    BtnSkill: left 722, top 21 (绝对 722, 370, 尺寸 24x23) -> Prguse_0010
        //    BtnGroup: left 764, top 11 (绝对 764, 360, 尺寸 24x23) -> Prguse_0011
        // 2. 聊天控制条 7 按钮 (left 219, 249, 279, 309, 339, 530, 560; top 104 -> 绝对 453, 尺寸 28x13 / 36x13):
        //    BtnTrade: 切换交易/商聊 (left 339, top 104, 绝对 339, 453, 尺寸 36x13) -> Prguse_0140 / 0141
        //    BtnGuild: 切换行会 (left 279, top 104, 绝对 279, 453, 尺寸 28x13) -> Prguse_0134 / 0135
        var btnDefs = new (string Name, float X, float Y, float W, float H, string NormalPng, string? PressedPng, Action OnClick)[]
        {
            ("BtnHudBag", 643, 410, 24, 24, "Prguse_0008.png", null, ToggleBag),
            ("BtnHudEquip", 682, 390, 24, 22, "Prguse_0009.png", null, ToggleEquip),
            ("BtnHudSkill", 722, 370, 24, 23, "Prguse_0010.png", null, ToggleSkill),
            ("BtnHudGroup", 764, 360, 24, 23, "Prguse_0011.png", null, ToggleGroup),
            ("BtnHudGuild", 279, 453, 28, 13, "Prguse_0134.png", "Prguse_0135.png", ToggleGuild),
            ("BtnHudTrade", 339, 453, 36, 13, "Prguse_0140.png", "Prguse_0141.png", ToggleTrade),
            // 聊天辅助按键
            ("BtnChatNear", 219, 453, 28, 13, "Prguse_0130.png", "Prguse_0131.png", () => {}),
            ("BtnChatWhisper", 249, 453, 28, 13, "Prguse_0132.png", "Prguse_0133.png", () => {}),
            ("BtnChatTeam", 309, 453, 28, 13, "Prguse_0128.png", "Prguse_0129.png", () => {}),
            ("BtnChatFilter", 530, 453, 28, 13, "Prguse_0136.png", "Prguse_0137.png", () => {}),
            ("BtnChatHistory", 560, 453, 28, 13, "Prguse_0138.png", "Prguse_0139.png", () => {})
        };

        var bottomBarContainer = new Control
        {
            Name = "BottomBarContainer",
            ZIndex = 0,
            MouseFilter = Control.MouseFilterEnum.Ignore
        };
        AddChild(bottomBarContainer);

        // 加载弧形 4 按键官方高亮贴图 (Prguse_0008 ~ Prguse_0011)
        _texHighlightBag = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0008.png");
        _texHighlightEquip = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0009.png");
        _texHighlightSkill = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0010.png");
        _texHighlightGroup = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0011.png");

        foreach (var def in btnDefs)
        {
            Texture2D? texNormal = null;
            Texture2D? texPressed = null;

            if (def.Name is "BtnHudBag" or "BtnHudEquip" or "BtnHudSkill" or "BtnHudGroup")
            {
                // 官方底板 Prguse_0001 已烘焙暗色按键！平时 TextureNormal 为 null 透出底图，按下时显示高亮
                texNormal = null;
                texPressed = ResPath.LoadImageTexture($"resources/wil/png/Prguse/{def.NormalPng}");
            }
            else
            {
                texNormal = ResPath.LoadImageTexture($"resources/wil/png/Prguse/{def.NormalPng}");
                texPressed = !string.IsNullOrEmpty(def.PressedPng)
                    ? ResPath.LoadImageTexture($"resources/wil/png/Prguse/{def.PressedPng}")
                    : null;
            }

            var btn = new TextureButton
            {
                Name = def.Name,
                Position = new Vector2(def.X, def.Y),
                Size = new Vector2(def.W, def.H),
                TextureNormal = texNormal,
                TexturePressed = texPressed,
                FocusMode = Control.FocusModeEnum.None
            };
            btn.Pressed += def.OnClick;
            bottomBarContainer.AddChild(btn);

            if (def.Name == "BtnHudBag") _btnHudBag = btn;
            else if (def.Name == "BtnHudEquip") _btnHudEquip = btn;
            else if (def.Name == "BtnHudSkill") _btnHudSkill = btn;
            else if (def.Name == "BtnHudGroup") _btnHudGroup = btn;
        }

        // 挂载官方 1.5 快捷药水栏 1~6 格 (IDA sub_481DD4 [0x484be6..0x484da1], left: 285, 328, 371, 415, 459, 503, top: 408, 32x29)
        SetupQuickItemBar(bottomBarContainer);
    }

    private void SetupQuickItemBar(Control parent)
    {
        // 官方 1.5 快捷药水槽位坐标 (IDA sub_481DD4 [0x484be6..0x484da1]):
        // left: 285, 328, 371, 415, 459, 503
        // top: 59 (相对底栏 349 -> 绝对 Y = 408)
        // width: 32, height: 29
        float[] slotXs = { 285f, 328f, 371f, 415f, 459f, 503f };
        for (int i = 0; i < 6; i++)
        {
            int slotIdx = i;
            var btn = new Button
            {
                Name = $"QuickSlot_{slotIdx + 1}",
                Position = new Vector2(slotXs[i], 408f),
                Size = new Vector2(32f, 29f),
                Flat = true,
                FocusMode = Control.FocusModeEnum.None
            };
            btn.Pressed += () => UseQuickSlot(slotIdx);
            btn.MouseEntered += () =>
            {
                if (!string.IsNullOrEmpty(_quickSlotItems[slotIdx]))
                {
                    ShowSlotTooltip(_quickSlotItems[slotIdx], "[color=#c0c0c0]快捷药水 (快捷键 1~6)[/color]", btn.GlobalPosition, showFloating: true);
                }
            };
            btn.MouseExited += HideSlotTooltip;

            var icon = new TextureRect
            {
                Name = "Icon",
                Size = new Vector2(24f, 22f),
                Position = new Vector2(4f, 3f),
                ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize,
                StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered,
                MouseFilter = Control.MouseFilterEnum.Ignore
            };
            btn.AddChild(icon);

            var countLbl = new Label
            {
                Name = "CountLbl",
                Position = new Vector2(10f, 13f),
                Size = new Vector2(20f, 14f),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                MouseFilter = Control.MouseFilterEnum.Ignore
            };
            countLbl.AddThemeFontSizeOverride("font_size", 9);
            countLbl.AddThemeColorOverride("font_color", new Color(1.0f, 1.0f, 1.0f));
            btn.AddChild(countLbl);

            _quickSlotButtons[i] = btn;
            _quickSlotIcons[i] = icon;
            _quickSlotCounts[i] = countLbl;
            parent.AddChild(btn);
        }

        // 默认预设 1~6 官方常见药水（采用 Items.wil 官方药水真实图标）
        SetQuickSlot(0, "强效金创药", 6, ResPath.LoadImageTexture("resources/wil/png/Items/Items_0014.png"));
        SetQuickSlot(1, "强效魔法药", 6, ResPath.LoadImageTexture("resources/wil/png/Items/Items_0015.png"));
        SetQuickSlot(2, "太阳水", 4, ResPath.LoadImageTexture("resources/wil/png/Items/Items_0016.png"));
    }

    public void SetQuickSlot(int slot, string itemName, int count, Texture2D? icon)
    {
        if (slot < 0 || slot >= 6) return;
        _quickSlotItems[slot] = itemName;
        _quickSlotAmounts[slot] = count;
        if (_quickSlotIcons[slot] != null)
        {
            _quickSlotIcons[slot].Texture = icon;
            _quickSlotIcons[slot].Visible = icon != null;
        }
        if (_quickSlotCounts[slot] != null)
        {
            _quickSlotCounts[slot].Text = count > 1 ? count.ToString() : "";
            _quickSlotCounts[slot].Visible = count > 0;
        }
    }

    public void UseQuickSlot(int slot)
    {
        if (slot < 0 || slot >= 6) return;
        if (_quickSlotAmounts[slot] <= 0 || string.IsNullOrEmpty(_quickSlotItems[slot])) return;

        string itemName = _quickSlotItems[slot];
        _quickSlotAmounts[slot]--;
        if (_quickSlotAmounts[slot] <= 0)
        {
            _quickSlotItems[slot] = "";
            if (_quickSlotIcons[slot] != null) _quickSlotIcons[slot].Texture = null;
            if (_quickSlotCounts[slot] != null) _quickSlotCounts[slot].Text = "";
        }
        else
        {
            if (_quickSlotCounts[slot] != null)
                _quickSlotCounts[slot].Text = _quickSlotAmounts[slot] > 1 ? _quickSlotAmounts[slot].ToString() : "";
        }

        GD.Print($"[HudController] 使用快捷药水栏 {slot + 1} 物品: {itemName}, 剩余数量: {_quickSlotAmounts[slot]}");
        QuickItemUsed?.Invoke(slot, itemName);
    }

    public void UpdateHp(int hp, int maxHp)
    {
        if (_lblHp != null) _lblHp.Text = $"{hp}/{maxHp}";
        if (_orbShaderMat != null)
        {
            float ratio = maxHp > 0 ? Mathf.Clamp((float)hp / maxHp, 0f, 1f) : 0f;
            _orbShaderMat.SetShaderParameter("hp_ratio", ratio);
        }
    }

    public void UpdateMp(int mp, int maxMp)
    {
        if (_lblMp != null) _lblMp.Text = $"{mp}/{maxMp}";
        if (_orbShaderMat != null)
        {
            float ratio = maxMp > 0 ? Mathf.Clamp((float)mp / maxMp, 0f, 1f) : 0f;
            _orbShaderMat.SetShaderParameter("mp_ratio", ratio);
        }
    }

    public void SetSingleHpOrbMode(bool isSingle)
    {
        if (_orbShaderMat != null)
        {
            _orbShaderMat.SetShaderParameter("is_single_hp_ball", isSingle);
        }
    }

    public void UpdateExp(long curExp, long maxExp)
    {
        if (_expBar != null)
        {
            _expBar.MaxValue = maxExp > 0 ? maxExp : 1;
            _expBar.Value = Math.Clamp(curExp, 0, (long)_expBar.MaxValue);
        }
    }

    public void UpdateWeight(int curWeight, int maxWeight)
    {
        if (_weightBar != null)
        {
            _weightBar.MaxValue = maxWeight > 0 ? maxWeight : 1;
            _weightBar.Value = Math.Clamp(curWeight, 0, (int)_weightBar.MaxValue);
        }
    }

    public void UpdateLevel(int level)
    {
        // 盛大原版官方 HUD 嵌入 Lv 徽章右侧黑槽 (636, 484)
        if (_lblLevel != null)
        {
            _lblLevel.Text = $"{level}";
            _lblLevel.Visible = true;
        }
    }

    public void UpdateName(string name)
    {
        if (_lblName != null) _lblName.Text = name;
    }

    public void UpdateGold(int gold)
    {
        if (_lblBagGold != null)
        {
            _lblBagGold.Text = gold.ToString("N0"); // 官方千分位或数字回显
        }
    }

    public void AddChat(string channel, string msg, Color color)
    {
        if (_chatLog != null)
        {
            string colorHex = color.ToHtml(false);
            _chatLog.AppendText($"[color=#{colorHex}][{channel}] {msg}[/color]\n");
        }
    }

    private void RefreshBottomBarButtonStates()
    {
        if (_btnHudBag != null)
        {
            _btnHudBag.TextureNormal = (_bagPanel != null && _bagPanel.Visible) ? _texHighlightBag : null;
        }
        if (_btnHudEquip != null)
        {
            bool isEquipOrStatusOpen = (_equipPanel != null && _equipPanel.Visible) || (_statusPanelView != null && _statusPanelView.Visible);
            _btnHudEquip.TextureNormal = isEquipOrStatusOpen ? _texHighlightEquip : null;
        }
        if (_btnHudSkill != null)
        {
            _btnHudSkill.TextureNormal = (_skillPanelView != null && _skillPanelView.Visible) ? _texHighlightSkill : null;
        }
        if (_btnHudGroup != null)
        {
            _btnHudGroup.TextureNormal = (_groupPanelView != null && _groupPanelView.Visible) ? _texHighlightGroup : null;
        }
    }

    public void ToggleBag()
    {
        if (_bagPanel != null)
        {
            _bagPanel.Visible = !_bagPanel.Visible;
            if (!_bagPanel.Visible) HideSlotTooltip();
            RefreshBottomBarButtonStates();
            GD.Print($"[HudController] 切换背包面板显示状态: {_bagPanel.Visible}");
        }
    }

    public void ToggleEquip()
    {
        if (_equipPanel != null)
        {
            _equipPanel.Visible = !_equipPanel.Visible;
            if (!_equipPanel.Visible) HideSlotTooltip();
            RefreshBottomBarButtonStates();
            GD.Print($"[HudController] 切换装备面板显示状态: {_equipPanel.Visible}");
        }
    }

    public void SwitchEquipToStatus()
    {
        if (_equipPanel != null)
        {
            _equipPanel.Visible = false;
        }
        if (_statusPanelView != null)
        {
            _stackManager.PushWindow(_statusPanelView);
        }
        RefreshBottomBarButtonStates();
        GD.Print("[HudController] 角色装备面板切换至独立状态面板");
    }

    public void SwitchStatusToEquip()
    {
        if (_statusPanelView != null)
        {
            _stackManager.PopWindow(_statusPanelView);
        }
        if (_equipPanel != null)
        {
            _equipPanel.Visible = true;
        }
        RefreshBottomBarButtonStates();
        GD.Print("[HudController] 角色状态面板切回装备面板");
    }

    public void ToggleStatus()
    {
        if (_statusPanelView != null)
        {
            _stackManager.ToggleWindow(_statusPanelView);
            if (_statusPanelView.Visible && _equipPanel != null)
            {
                _equipPanel.Visible = false;
            }
            RefreshBottomBarButtonStates();
        }
    }

    public void ToggleSkill()
    {
        if (_skillPanelView != null)
        {
            _stackManager.ToggleWindow(_skillPanelView);
            RefreshBottomBarButtonStates();
            GD.Print($"[HudController] 切换技能面板显示状态: {_skillPanelView.Visible}");
        }
    }

    public void ToggleGroup()
    {
        if (_groupPanelView != null)
        {
            _stackManager.ToggleWindow(_groupPanelView);
            RefreshBottomBarButtonStates();
            GD.Print($"[HudController] 切换组队面板显示状态: {_groupPanelView.Visible}");
        }
    }

    public void ToggleGuild()
    {
        if (_guildPanelView != null)
        {
            _stackManager.ToggleWindow(_guildPanelView);
            GD.Print($"[HudController] 切换行会面板显示状态: {_guildPanelView.Visible}");
        }
    }

    public void ToggleTrade()
    {
        if (_tradePanelView != null)
        {
            _stackManager.ToggleWindow(_tradePanelView);
            GD.Print($"[HudController] 切换交易面板显示状态: {_tradePanelView.Visible}");
        }
    }

    public void ToggleStorage()
    {
        if (_storagePanelView != null)
        {
            _stackManager.ToggleWindow(_storagePanelView);
            GD.Print($"[HudController] 切换仓库保管箱显示状态: {_storagePanelView.Visible}");
        }
    }

    public void ToggleShop(int npcUid = 0)
    {
        if (_shopPanelView != null)
        {
            if (_shopPanelView.Visible)
            {
                _stackManager.PopWindow(_shopPanelView);
            }
            else
            {
                _shopPanelView.OpenForNpc(npcUid);
                _stackManager.PushWindow(_shopPanelView);
            }
        }
    }

    public void ToggleRepair()
    {
        if (_repairPanelView != null)
        {
            _stackManager.ToggleWindow(_repairPanelView);
        }
    }

    public void ToggleAdjustAbil(int points = 5)
    {
        if (_adjustAbilPanelView != null)
        {
            if (_adjustAbilPanelView.Visible)
            {
                _stackManager.PopWindow(_adjustAbilPanelView);
            }
            else
            {
                _adjustAbilPanelView.Open(points);
                _stackManager.PushWindow(_adjustAbilPanelView);
            }
            GD.Print($"[HudController] 切换属性点分配面板显示状态: {_adjustAbilPanelView.Visible}");
        }
    }

    public void ShowMessageBox(string msg, bool hasCancel = true)
    {
        if (_messageBoxDialog != null)
        {
            _messageBoxDialog.ShowMessage(msg, hasCancel);
            _stackManager.PushWindow(_messageBoxDialog);
        }
    }

    public void ShowInputBox(string prompt, string defaultValue = "", Action<string>? onConfirm = null)
    {
        if (_messageBoxDialog != null)
        {
            _messageBoxDialog.ShowInput(prompt, defaultValue, onConfirm);
            _stackManager.PushWindow(_messageBoxDialog);
        }
    }

    public void OnChatInputSubmitted(string newText)
    {
        if (string.IsNullOrWhiteSpace(newText))
        {
            _chatInput?.ReleaseFocus();
            return;
        }

        string trimmed = newText.Trim();
        if (_chatInput != null)
        {
            _chatInput.Text = "";
            _chatInput.ReleaseFocus();
        }

        // 解析官方聊天频道指令
        if (trimmed.StartsWith("!"))
        {
            // 全服喊话
            EmitSignal(SignalName.ChatSent, trimmed);
            AddChat("喊话", trimmed[1..], new Color(1.0f, 0.95f, 0.4f));
        }
        else if (trimmed.StartsWith("/"))
        {
            // 私聊
            EmitSignal(SignalName.ChatSent, trimmed);
            AddChat("私聊", trimmed[1..], new Color(0.4f, 1.0f, 1.0f));
        }
        else
        {
            // 普通视野说话
            EmitSignal(SignalName.ChatSent, trimmed);
            AddChat("说话", trimmed, new Color(1.0f, 1.0f, 1.0f));
        }
    }

    public void ShowSlotTooltip(string title, string desc, Vector2 globalPos, bool showFloating = true)
    {
        // 1. 如果背包面板处于打开状态，同步更新背包底部的官方原生物品属性内胆
        if (_bagPanel != null && _bagPanel.Visible && _lblBagItemDesc != null)
        {
            _lblBagItemDesc.Text = $"[color=#ffff40]{title}[/color]\n{desc}";
        }

        // 2. 如果装备面板处于打开状态，同步更新装备面板底部的官方原生物品属性内胆
        if (_equipPanel != null && _equipPanel.Visible && _lblEquipItemDesc != null)
        {
            _lblEquipItemDesc.Text = $"[color=#ffff40]{title}[/color]\n{desc}";
        }

        // 3. 浮动 Tooltip
        if (showFloating)
        {
            _itemTooltipView?.ShowTooltip(title, desc, globalPos);
        }
        else
        {
            _itemTooltipView?.HideTooltip();
        }
    }

    public void HideSlotTooltip()
    {
        _itemTooltipView?.HideTooltip();
        if (_lblBagItemDesc != null) _lblBagItemDesc.Text = "";
        if (_lblEquipItemDesc != null) _lblEquipItemDesc.Text = "";
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventKey keyEvent && keyEvent.Pressed && !keyEvent.Echo)
        {
            switch (keyEvent.Keycode)
            {
                case Key.Escape:
                {
                    if (CloseTopDialog())
                    {
                        GetViewport().SetInputAsHandled();
                    }
                    break;
                }
                case Key.F9:
                {
                    ToggleBag();
                    GetViewport().SetInputAsHandled();
                    break;
                }
                case Key.F10:
                {
                    ToggleEquip();
                    GetViewport().SetInputAsHandled();
                    break;
                }
                case Key.F11:
                {
                    ToggleSkill();
                    GetViewport().SetInputAsHandled();
                    break;
                }
                case Key.P:
                {
                    ToggleGroup();
                    GetViewport().SetInputAsHandled();
                    break;
                }
                case Key.G:
                {
                    ToggleGuild();
                    GetViewport().SetInputAsHandled();
                    break;
                }
                case Key.T:
                {
                    ToggleTrade();
                    GetViewport().SetInputAsHandled();
                    break;
                }
                case Key.Enter:
                case Key.KpEnter:
                {
                    if (_chatInput != null)
                    {
                        if (!_chatInput.HasFocus())
                        {
                            _chatInput.GrabFocus();
                        }
                        else
                        {
                            OnChatInputSubmitted(_chatInput.Text);
                        }
                        GetViewport().SetInputAsHandled();
                    }
                    break;
                }
                case Key.Tab:
                {
                    _minimapView?.CycleMode();
                    GetViewport().SetInputAsHandled();
                    break;
                }
                case Key.A:
                {
                    if (keyEvent.CtrlPressed || keyEvent.ShiftPressed || keyEvent.AltPressed)
                    {
                        ToggleAdjustAbil(5);
                        GetViewport().SetInputAsHandled();
                    }
                    break;
                }
            }
        }
    }

    public bool CloseTopDialog()
    {
        var closed = _stackManager.CloseTopDialog();
        if (closed != null)
        {
            return true;
        }

        if (_equipPanel != null && _equipPanel.Visible)
        {
            _equipPanel.Visible = false;
            return true;
        }
        if (_bagPanel != null && _bagPanel.Visible)
        {
            _bagPanel.Visible = false;
            return true;
        }
        return false;
    }

    public bool IsMouseOverUi(Vector2 mousePos)
    {
        return _stackManager.IsMouseOverUi((int)mousePos.X, (int)mousePos.Y);
    }

    /// <summary>
    /// 盛大官方 1.5 昼夜与地牢黑暗光照视野更新 (1:1 联动 dungeon_vision_mask Shader)
    /// </summary>
    public void UpdateVisionFog(bool isDark, int radiusTiles, Vector2 screenLightPos)
    {
        if (_darknessFogMask == null || _fogShaderMat == null) return;

        if (!isDark)
        {
            _darknessFogMask.Visible = false;
            _fogShaderMat.SetShaderParameter("enabled", false);
            return;
        }

        _darknessFogMask.Visible = true;
        _fogShaderMat.SetShaderParameter("enabled", true);
        _fogShaderMat.SetShaderParameter("light_pos", screenLightPos);

        // 依据瓦片半径计算屏幕像素羽化半径 (1 瓦片约 48x32 像素，按平均半径 40 像素换算)
        float innerPx = Math.Max(20.0f, radiusTiles * 28.0f);
        float outerPx = Math.Max(40.0f, radiusTiles * 52.0f);
        _fogShaderMat.SetShaderParameter("inner_radius", innerPx);
        _fogShaderMat.SetShaderParameter("outer_radius", outerPx);
    }
}
