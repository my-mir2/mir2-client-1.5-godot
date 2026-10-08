using System;
using System.Collections.Generic;
using Godot;
using Mir2Client.Audio;
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

    [Signal]
    public delegate void ItemDropToGroundRequestedEventHandler(int bagSlot, string itemName, Vector2 dropScreenPos);

    [Signal]
    public delegate void TradeAddItemRequestedEventHandler(int slot, string itemName);

    [Signal]
    public delegate void TradeDelItemRequestedEventHandler(int slot);

    [Signal]
    public delegate void TradeChangeGoldRequestedEventHandler(int gold);

    [Signal]
    public delegate void TradeConfirmRequestedEventHandler();

    [Signal]
    public delegate void TradeCancelRequestedEventHandler();

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

    private readonly SlotButton?[] _bagSlots = new SlotButton?[40];
    private readonly (string Name, string Desc)?[] _bagItemDetails = new (string, string)?[40];
    private readonly SlotButton?[] _equipSlots = new SlotButton?[9];
    private readonly (string Name, string Desc)?[] _equipItemDetails = new (string, string)?[9];
    private readonly (string Name, string Desc, int OriginBagSlot)?[] _tradeItemOrigins = new (string, string, int)?[10];

    // 场景原生窗体控件与大世界原生面板
    private Panel? _bagPanel;
    private Panel? _equipPanel;
    private TextureRect? _dollBackground;
    private TextureRect? _dollHair;
    private TextureRect? _dollCloth;
    private TextureRect? _dollWeapon;
    private TextureRect? _dollHelm;
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

    public Panel? EquipPanel => _equipPanel;
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
    public MessageBoxDialog? MessageBox => _messageBoxDialog;
    public NpcDialogView? NpcDialog => _npcDialogView;
    public DeathScreenView? DeathScreen => _deathScreenView;
    public MinimapView? Minimap => _minimapView;
    public ItemTooltipView? ItemTooltip => _itemTooltipView;

    // 官方 1.5 血球魔球控件 (Prguse_0004 @ (39, 440) 相对屏幕，相对 HUD @ (39, 91))
    private TextureRect? _orbBgTextureRect;
    private TextureRect? _orbTextureRect;
    private ShaderMaterial? _orbShaderMat;

    // 官方 1.5 经验条与负重条 (Prguse_0007 @ (666, 527) & (666, 560), IDA sub_48B2A0)
    private TextureProgressBar? _expBar;
    private TextureProgressBar? _weightBar;

    // 官方 1.5 快捷药水栏 1~6 格 (IDA sub_481DD4 [0x484a0f..0x484b80], sub_48C188, sub_48C02C)
    private readonly SlotButton[] _quickSlotButtons = new SlotButton[6];
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
    private TextureButton? _btnHudPlusAbil;
    private TextureButton? _btnHudMiniMap;
    private TextureButton? _btnHudLogout;
    private TextureButton? _btnHudExit;
    private TextureButton? _btnHudMyState;
    private TextureButton? _btnHudOption;

    public TextureButton? BtnHudBag => _btnHudBag;
    public TextureButton? BtnHudEquip => _btnHudEquip;
    public TextureButton? BtnHudSkill => _btnHudSkill;
    public TextureButton? BtnHudGroup => _btnHudGroup;
    public TextureButton? BtnHudPlusAbil => _btnHudPlusAbil;
    public TextureButton? BtnHudMiniMap => _btnHudMiniMap;
    public TextureButton? BtnHudLogout => _btnHudLogout;
    public TextureButton? BtnHudExit => _btnHudExit;
    public TextureButton? BtnHudMyState => _btnHudMyState;
    public TextureButton? BtnHudOption => _btnHudOption;
    public TextureButton? BtnEquipToStatus { get; private set; }
    private TextureRect? _attackModeIcon;

    private int _currentAttackMode = 1;
    public int CurrentAttackMode => _currentAttackMode;

    private int _currentGold = 0;
    public int CurrentGold => _currentGold;

    /// <summary>攻击模式图标(官方 sub_48B2A0 0x48b8e0..0x48b92b): mode∈[1,4] 取 Prguse_(15+mode),其余不绘制。</summary>
    public void SetAttackMode(int mode)
    {
        _currentAttackMode = mode;
        if (_attackModeIcon == null) return;
        if (mode < 1 || mode > 4) { _attackModeIcon.Visible = false; return; }
        var tex = ResPath.LoadImageTexture($"resources/wil/png/Prguse/Prguse_{15 + mode:D4}.png");
        _attackModeIcon.Texture = tex;
        if (tex != null) _attackModeIcon.Size = tex.GetSize();
        _attackModeIcon.Visible = tex != null;
    }

    [Signal]
    public delegate void ChangeAttackModeRequestedEventHandler(int mode);

    [Signal]
    public delegate void NpcActionSelectedEventHandler(string actionTag);

    [Signal]
    public delegate void NpcOptionSelectedEventHandler(int index);

    [Signal]
    public delegate void ToggleThrustingRequestedEventHandler(bool enabled);

    [Signal]
    public delegate void ToggleHalfMoonRequestedEventHandler(bool enabled);

    [Signal]
    public delegate void DropGoldRequestedEventHandler(int amount);

    [Signal]
    public delegate void SoftCloseRequestedEventHandler();

    [Signal]
    public delegate void ExitGameRequestedEventHandler();

    private bool _isThrusting = true;
    private bool _isHalfMoon = false;
    public bool IsThrustingEnabled => _isThrusting;
    public bool IsHalfMoonEnabled => _isHalfMoon;

    /// <summary>官方 1.5 快捷键 Ctrl+F 刺杀剑术开关切换 (CM_THRUSTINGONOFF 3026)</summary>
    public void ToggleThrusting()
    {
        _isThrusting = !_isThrusting;
        string statusText = _isThrusting ? "打开刺杀剑术" : "关闭刺杀剑术";
        AddChat("系统", $"[系统] {statusText}。", new Color(0.4f, 1f, 0.4f));
        EmitSignal(SignalName.ToggleThrustingRequested, _isThrusting);
    }

    /// <summary>官方 1.5 快捷键 Ctrl+B 半月弯刀开关切换 (CM_HALFMOONONOFF 3027)</summary>
    public void ToggleHalfMoon()
    {
        _isHalfMoon = !_isHalfMoon;
        string statusText = _isHalfMoon ? "打开半月弯刀" : "关闭半月弯刀";
        AddChat("系统", $"[系统] {statusText}。", new Color(0.4f, 1f, 0.4f));
        EmitSignal(SignalName.ToggleHalfMoonRequested, _isHalfMoon);
    }

    /// <summary>快捷键 Ctrl+H 攻击模式循环切换 (全体 -> 和平 -> 夫妻/组队 -> 行会)</summary>
    public void CycleNextAttackMode()
    {
        int nextMode = _currentAttackMode >= 4 ? 1 : _currentAttackMode + 1;
        SetAttackMode(nextMode);
        string modeDesc = nextMode switch
        {
            1 => "全体攻击模式",
            2 => "和平攻击模式",
            3 => "夫妻/组队攻击模式",
            4 => "行会攻击模式",
            _ => "全体攻击模式"
        };
        AddChat("系统", $"[系统] 切换为{modeDesc}。", new Color(1f, 1f, 0f));
        EmitSignal(SignalName.ChangeAttackModeRequested, nextMode);
    }

    /// <summary>官方 1.5 小退/换人 (IDA TFrmDlg_DBotLogoutClick 0x491474, 发包 CM_SOFTCLOSE 1009)</summary>
    public void RequestSoftClose()
    {
        AddChat("系统", "[系统] 正在注销当前角色并返回选角神龛...", new Color(1f, 0.8f, 0.4f));
        EmitSignal(SignalName.SoftCloseRequested);
    }

    /// <summary>官方 1.5 退出游戏 (IDA TFrmDlg_DBotExitClick 0x491520)</summary>
    public void RequestExitGame()
    {
        EmitSignal(SignalName.ExitGameRequested);
    }

    /// <summary>官方 1.5 丢弃金币 (IDA TFrmDlg_DGoldClick 0x48e564, 发包 CM_DROPGOLD 1007)</summary>
    public void RequestDropGold(int amount)
    {
        AddChat("系统", $"[系统] 丢弃金币 {amount} 枚至地面。", new Color(1f, 1f, 0.4f));
        EmitSignal(SignalName.DropGoldRequested, amount);
    }

    /// <summary>服务端 SM_ATTACKMODECHANGED (766, IDA sub_467920 case 766 @ 0x468A2B) 驱动回显</summary>
    public void OnServerAttackModeChanged(int mode)
    {
        SetAttackMode(mode);
        string modeDesc = mode switch
        {
            1 => "全体攻击模式",
            2 => "和平攻击模式",
            3 => "夫妻/组队攻击模式",
            4 => "行会攻击模式",
            _ => "全体攻击模式"
        };
        AddChat("系统", $"[系统] 攻击模式同步为: {modeDesc}。", new Color(1f, 0.95f, 0.3f));
    }

    private TextureRect? _dayNightIcon;

    /// <summary>昼夜状态(官方 off_4A3DD0):0=Prguse_0015,1/2/3=0012/0013/0014。</summary>
    public void SetDayNightState(int state)
    {
        if (_dayNightIcon == null) return;
        int idx = state switch { 1 => 12, 2 => 13, 3 => 14, _ => 15 };
        _dayNightIcon.Texture = ResPath.LoadImageTexture($"resources/wil/png/Prguse/Prguse_{idx:D4}.png");
    }

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

        // 挂载大世界地面拖拽放置捕获层 (接收从背包拖出到游戏视野的丢弃事件)
        var dropCatcher = new GroundDropCatcher(this);
        AddChild(dropCatcher);
        MoveChild(dropCatcher, 0);

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

        // 挂载官方 1.5 血球魔球底座 (IDA sub_48B2A0: Prguse_0005.png 96x92 @ (38, 439) 空槽金属暗底)
        var orbBgTex = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0005.png");
        if (orbBgTex != null)
        {
            _orbBgTextureRect = new TextureRect
            {
                Name = "HudOrbBg",
                Texture = orbBgTex,
                Position = new Vector2(38, 439),
                Size = new Vector2(96, 92),
                MouseFilter = Control.MouseFilterEnum.Ignore,
                ZIndex = 0
            };
            AddChild(_orbBgTextureRect);
        }

        // 挂载官方 1.5 血球魔球 (Prguse_0004.png 92x90 @ HUD(40, 91)，即屏幕坐标 (40, 440))
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
                Position = new Vector2(40, 440),
                Size = new Vector2(92, 90),
                Material = _orbShaderMat,
                MouseFilter = Control.MouseFilterEnum.Ignore,
                ZIndex = 1 // 位于底座上方，裁切透明部分自然显露金属空槽
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
            ZIndex = 0,
            Visible = false // IDA sub_48B2A0 不绘制等级数字,无官方依据的 (636,484) 文本按铁律隐藏
        };
        _lblLevel.AddThemeFontSizeOverride("font_size", 12);
        _lblLevel.AddThemeColorOverride("font_color", new Color(1.0f, 0.95f, 0.7f));
        AddChild(_lblLevel);

        // 攻击模式图标 (IDA sub_48B2A0 0x48b8e0..0x48b92b: 当 mode∈[1,4] 时取 Prguse_(15+mode)，以 (754,553) 叠加绘制于日夜齿轮上层)
        _attackModeIcon = new TextureRect
        {
            Name = "HudAttackModeIcon",
            Position = new Vector2(754, 553),
            MouseFilter = Control.MouseFilterEnum.Ignore,
            ZIndex = 1
        };
        AddChild(_attackModeIcon);
        SetAttackMode(1);

        // 昼夜月相轮盘 (盛大官方 1.5 IDA sub_48B2A0 0x48b3bd..0x48b476 实证:
        // Prguse_0001 停靠于屏幕 Top=349, 右上半月金属D槽相对偏移为 (748, 79), 全局绝对坐标为 (748, 349+79=428),
        // 与右下攻击模式按钮 (754, 553) 严格上下分离，严丝合缝嵌入金属凹槽)
        _dayNightIcon = new TextureRect
        {
            Name = "HudDayNightIcon",
            Position = new Vector2(748, 428),
            Size = new Vector2(32, 54),
            MouseFilter = Control.MouseFilterEnum.Ignore,
            ZIndex = 0
        };
        AddChild(_dayNightIcon);
        SetDayNightState(0);

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
                            CustomMinimumSize = new Vector2(36f, 32f), // 严格对齐官方 IDA sub_457D88 (CellWidth=36, CellHeight=32)
                            Flat = true
                        };
                        _bagSlots[slot] = btn;
                        btn.Pressed += () => EmitSignal(SignalName.UseItemRequested, slot);
                        btn.MouseEntered += () =>
                        {
                            if (btn.ItemTexture != null || btn.Icon != null)
                            {
                                var details = _bagItemDetails[slot];
                                string name = details?.Name ?? $"背包物品 #{slot + 1}";
                                string desc = details?.Desc ?? "[color=#c0c0c0]重量 1   持久力 10/10[/color]\n[color=#ffff00]攻击 0-1[/color]";
                                ShowSlotTooltip(name, desc, btn.GlobalPosition, showFloating: false);
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
                MouseFilter = Control.MouseFilterEnum.Stop
            };
            goldIcon.GuiInput += (ev) =>
            {
                if (ev is InputEventMouseButton mb && mb.Pressed && mb.ButtonIndex == MouseButton.Left)
                {
                    // 官方 1.5 IDA TFrmDlg_DGoldClick (0x48e564): 点击背包金币弹出丢弃输入模态框
                    if (_currentGold <= 0)
                    {
                        AddChat("系统", "[系统] 身上没有金币可以丢弃。", new Color(1f, 0.4f, 0.4f));
                        return;
                    }
                    if (_messageBoxDialog != null)
                    {
                        _messageBoxDialog.ShowInput($"请输入要丢弃的金币数量 (最多 {_currentGold:N0})：", "100", (input) =>
                        {
                            if (int.TryParse(input.Trim(), out var dropAmount) && dropAmount > 0)
                            {
                                int realDrop = Math.Min(dropAmount, _currentGold);
                                RequestDropGold(realDrop);
                            }
                        });
                    }
                    else
                    {
                        RequestDropGold(Math.Min(100, _currentGold));
                    }
                }
            };
            _bagPanel.AddChild(goldIcon);

            // IDA sub_48DC24 @ 0x48dd26..0x48dd53: TextOut(窗体+64, 窗体+185, 金币串)，左对齐，字体约 12px 高；
            // 文字视觉中心 ≈ 185+6=191，与金币凹槽 (y≈181..199, 中心≈190) 对齐。
            // Godot Label 采用 y=180/h=20 + 垂直居中，使字形中心落在 ≈190，不再偏下。
            var lblGold = new Label
            {
                Name = "LblBagGold",
                Position = new Vector2(64, 179),
                Size = new Vector2(142, 18),
                Text = "0",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            lblGold.AddThemeFontSizeOverride("font_size", 11);
            // IDA 0x48dd01: Font.Color=0xFFFFFF(白)，金币串由 sub_49F640 做千分位(",")
            lblGold.AddThemeColorOverride("font_color", Colors.White);
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

            // 绑定装备面板原生向下翻页切至状态面板按钮 (Prguse_0396/0397 @ 213, 143, 12x21, IDA sub_481dd4 [0x484325..0x48439b] / 0x48a97c)
            var texDownNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0396.png");
            var texDownPressed = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0397.png");
            var btnEquipToStatus = new TextureButton
            {
                Name = "BtnEquipToStatus",
                Position = new Vector2(213, 143),
                Size = new Vector2(12, 21),
                TextureNormal = texDownNormal,
                TexturePressed = texDownPressed,
                FocusMode = Control.FocusModeEnum.None
            };
            btnEquipToStatus.Pressed += SwitchEquipToStatus;
            BtnEquipToStatus = btnEquipToStatus;
            _equipPanel.AddChild(btnEquipToStatus);

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
            // 0. 发型层: 官方 IDA sub_488AF4 [0x488c73..0x488caf] 实证：男角色基准发型 Prguse_0441 (24x16 @ 73, 44)，完全覆盖额头发际线与头顶
            var texHair = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0441.png");
            if (_dollBackground != null)
            {
                _dollHair = new TextureRect
                {
                    Name = "DollHair",
                    Texture = texHair,
                    Position = new Vector2(73, 44),
                    Size = new Vector2(24, 16),
                    MouseFilter = Control.MouseFilterEnum.Ignore,
                    Visible = true,
                    ZIndex = 1
                };
                _dollBackground.AddChild(_dollHair);
            }

            // 1. 衣服立绘层: 盛大官方 1.5 正宗男布衣 StateItem_0060.png (80x128 @ 39, 63，白袍金边深蓝长裤男战士经典布衣)
            var texCloth = ResPath.LoadImageTexture("resources/wil/png/StateItem/StateItem_0060.png");
            if (_dollBackground != null)
            {
                _dollCloth = new TextureRect
                {
                    Name = "DollCloth",
                    Texture = texCloth,
                    Position = new Vector2(39, 63),
                    Size = new Vector2(80, 128),
                    MouseFilter = Control.MouseFilterEnum.Ignore,
                    ZIndex = 2
                };
                _dollBackground.AddChild(_dollCloth);
            }

            // 2. 武器立绘层: 初始为木剑 StateItem_0030 (28x57 @ 20, 48，剑柄紧紧握在右手手心 (45, 101)，剑尖斜指长空)
            var texWeapon = ResPath.LoadImageTexture("resources/wil/png/StateItem/StateItem_0030.png");
            if (_dollBackground != null)
            {
                _dollWeapon = new TextureRect
                {
                    Name = "DollWeapon",
                    Texture = texWeapon,
                    Position = new Vector2(20, 48),
                    Size = new Vector2(28, 57),
                    MouseFilter = Control.MouseFilterEnum.Ignore,
                    ZIndex = 3
                };
                _dollBackground.AddChild(_dollWeapon);
            }

            // 3. 头盔立绘层: 初始无头盔 (隐藏状态)
            if (_dollBackground != null)
            {
                _dollHelm = new TextureRect
                {
                    Name = "DollHelm",
                    Position = new Vector2(74, 42),
                    Size = new Vector2(20, 23),
                    MouseFilter = Control.MouseFilterEnum.Ignore,
                    Visible = false,
                    ZIndex = 4
                };
                _dollBackground.AddChild(_dollHelm);
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
                _equipSlots[equipSlot] = btn;
                btn.Pressed += () => EmitSignal(SignalName.TakeoffItemRequested, equipSlot);
                btn.MouseEntered += () =>
                {
                    var (fallbackName, fallbackDesc) = equipSlot switch
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
                    var details = _equipItemDetails[equipSlot];
                    string itemName = details?.Name ?? fallbackName;
                    string itemDesc = details?.Desc ?? fallbackDesc;
                    ShowSlotTooltip(itemName, itemDesc, btn.GlobalPosition, showFloating: false);
                };
                btn.MouseExited += HideSlotTooltip;
                _equipSlotsContainer?.AddChild(btn);
            }
        }

        _minimapView = GetNodeOrNull<MinimapView>("Minimap");
        _npcDialogView = GetNodeOrNull<NpcDialogView>("NpcDialog");
        if (_npcDialogView != null)
        {
            _npcDialogView.ActionTriggered += (actionTag) =>
            {
                GD.Print($"[HudController] NPC 对话点击 Action: {actionTag}");
                string lower = actionTag.ToLowerInvariant();
                if (lower.Contains("storage") || lower.Contains("deposit") || lower.Contains("保管"))
                {
                    ToggleStorage();
                }
                else if (lower.Contains("buy") || lower.Contains("sell") || lower.Contains("shop") || lower.Contains("买") || lower.Contains("卖"))
                {
                    ToggleShop();
                }
                else if (lower.Contains("repair") || lower.Contains("修理"))
                {
                    ToggleRepair();
                }
                EmitSignal(SignalName.NpcActionSelected, actionTag);
            };
            // NPC 数字选项直通服务端（bridge 903：CM 61|1011，recog=NPC id，param=选项序号）。
            _npcDialogView.OptionSelected += (index) =>
            {
                EmitSignal(SignalName.NpcOptionSelected, index);
            };
        }
        _deathScreenView = GetNodeOrNull<DeathScreenView>("DeathScreen");

        // 官方 1.5 HUD 底栏 6 大原生功能按钮 (Prguse_0001 对应包裹/装备/技能/组队/行会/交易)
        // 依据 IDA sub_481DD4 实证热区分布，为底栏添加交互热区
        // 必须优先于各大二级弹出窗口挂载，确保其处于场景树下层，大世界弹窗弹出时能正确定位与遮挡
        SetupBottomBarButtons();

        // 挂载官方 1.5 大世界原生窗体系统 (加入 WindowStackManager 统一管理)
        _skillPanelView = new SkillPanelView();
        AddChild(_skillPanelView);

        _statusPanelView = new StatusPanelView();
        _statusPanelView.SwitchToEquipRequested += SwitchStatusToEquip;
        AddChild(_statusPanelView);

        _groupPanelView = new GroupPanelView();
        AddChild(_groupPanelView);

        _guildPanelView = new GuildPanelView();
        AddChild(_guildPanelView);

        _tradePanelView = new TradePanelView();
        _tradePanelView.EndDealConfirmed += () =>
        {
            EmitSignal(SignalName.TradeConfirmRequested);
            AddChat("系统", "[交易] 你锁定了本次交易，等待对方确认。", new Color(1f, 0.9f, 0.3f));
        };
        _tradePanelView.TradeCancelled += () =>
        {
            RollbackTradeItemsToBag();
            EmitSignal(SignalName.TradeCancelRequested);
            AddChat("系统", "[交易] 交易已取消，物品原路退回背包。", new Color(1f, 0.4f, 0.4f));
        };
        _tradePanelView.ChangeDealGoldRequested += (gold) =>
        {
            EmitSignal(SignalName.TradeChangeGoldRequested, gold);
            AddChat("系统", $"[交易] 修改出资交易金币为 {gold:N0}。", new Color(1f, 0.9f, 0.3f));
        };
        _tradePanelView.AddDealItemRequested += (slot, name) =>
        {
            EmitSignal(SignalName.TradeAddItemRequested, slot, name);
        };
        _tradePanelView.DelDealItemRequested += (slot) =>
        {
            EmitSignal(SignalName.TradeDelItemRequested, slot);
        };
        AddChild(_tradePanelView);

        // 官方无独立仓库窗体 (IDA: sub_4579F0 全量调用者仅 sub_481DD4/sub_486DF0,无仓库底板);
        // 服务端协议将仓库作为商人窗口的 Storage 模式,故不再创建非官方的 StoragePanelView (Prguse_0402)。

        _shopPanelView = new ShopPanelView();
        _shopPanelView.TakeBackRequested += (storageSlot, itemName) =>
        {
            // 取回仓库物品：加入背包并下发系统提示
            AddBagItem(itemName, "Items_0059.png", "屠龙");
            AddChat("系统", $"[系统] 成功从仓库取回保管物品 [{itemName}]。", new Color(1f, 1f, 0f));
            GD.Print($"[HudController] 仓库取回成功: slot={storageSlot}, item={itemName}");
        };
        AddChild(_shopPanelView);

        _repairPanelView = new RepairPanelView();
        _repairPanelView.StorageDepositConfirmed += (bagSlot, itemName) =>
        {
            // 存入仓库：清空该格背包物品并发送系统提示
            if (bagSlot >= 0 && bagSlot < 40)
            {
                ClearBagItem(bagSlot);
            }
            AddChat("系统", $"[系统] 物品 [{itemName}] 已成功交付仓库保管员保管。", new Color(1f, 1f, 0f));
            GD.Print($"[HudController] 仓库保管成功: slot={bagSlot}, item={itemName}");
        };
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

        _guildPanelView.SetMessageBoxDialog(_messageBoxDialog);
        _guildPanelView.AddMemberRequested += (targetName) =>
        {
            AddChat("行会", $"[行会管理] 向角色 [{targetName}] 发送加入行会邀请。", new Color(1f, 0.9f, 0.3f));
            GD.Print($"[HudController] 请求添加行会成员: {targetName}");
        };
        _guildPanelView.DelMemberRequested += (targetName) =>
        {
            AddChat("行会", $"[行会管理] 已将成员 [{targetName}] 逐出行会。", new Color(1f, 0.4f, 0.4f));
            GD.Print($"[HudController] 请求删除行会成员: {targetName}");
        };
        _guildPanelView.AllyRequested += (targetGuild) =>
        {
            AddChat("行会", $"[行会外交] 向行会 [{targetGuild}] 发送结盟申请。", new Color(0.4f, 0.9f, 1f));
            GD.Print($"[HudController] 请求结盟行会: {targetGuild}");
        };
        _guildPanelView.CancelAllyRequested += (targetGuild) =>
        {
            AddChat("行会", $"[行会外交] 已解除与行会 [{targetGuild}] 的同盟关系。", new Color(1f, 0.5f, 0.2f));
            GD.Print($"[HudController] 请求解除结盟: {targetGuild}");
        };
        _guildPanelView.WarRequested += (targetGuild) =>
        {
            AddChat("行会", $"[行会战争] 正式向行会 [{targetGuild}] 宣战！", new Color(1f, 0.2f, 0.2f));
            GD.Print($"[HudController] 请求行会宣战: {targetGuild}");
        };
        _guildPanelView.CancelWarRequested += (targetGuild) =>
        {
            AddChat("行会", $"[行会战争] 已向行会 [{targetGuild}] 提议停战。", new Color(0.7f, 1f, 0.4f));
            GD.Print($"[HudController] 请求行会停战: {targetGuild}");
        };
        _guildPanelView.NoticeUpdated += (notice) =>
        {
            AddChat("行会", "[行会公告] 行会公告已成功发布并向全体成员广播。", new Color(1f, 0.85f, 0.4f));
            GD.Print($"[HudController] 行会公告更新: {notice}");
        };
        _guildPanelView.RankUpdated += (memberName, rank) =>
        {
            AddChat("行会", $"[行会封号] 成员 [{memberName}] 封号已成功调整为 [{rank}]。", new Color(0.9f, 0.9f, 0.2f));
            GD.Print($"[HudController] 行会封号调整: member={memberName}, rank={rank}");
        };

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
            ("BtnChatHistory", 560, 453, 28, 13, "Prguse_0138.png", "Prguse_0139.png", () => {}),
            // 官方 1.5 原生底栏功能按钮 (IDA dialog_truth_table 0x4915cc/0x490c80/0x491474/0x491520/0x48bdec/0x48be70 实证):
            ("BtnHudPlusAbil", 605, 410, 24, 24, "", null, () => ToggleAdjustAbil(5)),
            ("BtnHudMiniMap", 748, 410, 24, 24, "", null, ToggleMinimap),
            ("BtnHudLogout", 600, 453, 28, 13, "", null, RequestSoftClose),
            ("BtnHudExit", 630, 453, 28, 13, "", null, RequestExitGame),
            ("BtnHudMyState", 682, 414, 24, 22, "", null, ToggleStatus),
            ("BtnHudOption", 660, 453, 28, 13, "", null, () => AddChat("系统", "[系统] 呼出系统选项设置。", new Color(1f, 1f, 0.4f)))
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
            else if (!string.IsNullOrEmpty(def.NormalPng))
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
            else if (def.Name == "BtnHudPlusAbil") _btnHudPlusAbil = btn;
            else if (def.Name == "BtnHudMiniMap") _btnHudMiniMap = btn;
            else if (def.Name == "BtnHudLogout") _btnHudLogout = btn;
            else if (def.Name == "BtnHudExit") _btnHudExit = btn;
            else if (def.Name == "BtnHudMyState") _btnHudMyState = btn;
            else if (def.Name == "BtnHudOption") _btnHudOption = btn;
        }

        // 挂载官方 1.5 快捷药水栏 1~6 格 (IDA sub_481DD4 [0x484be6..0x484da1], left: 285, 328, 371, 415, 459, 503, top: 408, 32x29)
        SetupQuickItemBar(bottomBarContainer);

        // 注册全生命周期槽位拖拽放置响应 (Bag, Equip, Repair, Storage)
        SlotButton.ItemDroppedGlobal += HandleSlotItemDrop;
        SlotButton.SlotDoubleClickedGlobal += HandleSlotDoubleClick;
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        SlotButton.ItemDroppedGlobal -= HandleSlotItemDrop;
        SlotButton.SlotDoubleClickedGlobal -= HandleSlotDoubleClick;
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
            var btn = new SlotButton
            {
                Name = $"QuickSlot_{slotIdx + 1}",
                CurrentSlotType = SlotButton.SlotType.Quick,
                SlotIndex = slotIdx,
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

        // 播放经典吃药音效 (药水吞咽音: 官方 sound 107, 对齐 Delphi SoundUtil.pas:s_eat_drug)
        AudioManager.Instance?.PlaySoundId(AudioManager.SoundId.EatDrug);

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
        // 官方能力页第 0 行 (IDA sub_488AF4 0x48935e: 100*[+48h]/[+4Ch],保留 2 位小数并追加 "%")
        _statusPanelView?.UpdateAbilityValue(0, maxExp > 0 ? $"{100.0 * curExp / maxExp:F2}%" : "0.00%");
    }

    public void UpdateWeight(int curWeight, int maxWeight)
    {
        if (_weightBar != null)
        {
            _weightBar.MaxValue = maxWeight > 0 ? maxWeight : 1;
            _weightBar.Value = Math.Clamp(curWeight, 0, (int)_weightBar.MaxValue);
        }
        // 官方能力页第 1 行 背包重量 (IDA 0x4893f4: [+50h]/[+52h] 两个 word,超限时红色 0x0000FF)
        _statusPanelView?.UpdateAbilityValue(1, $"{curWeight}/{maxWeight}", curWeight > maxWeight);
    }

    /// <summary>
    /// 盛大官方 1.5 角色能力页 (IDA sub_488AF4 @ 0x4892c2..0x489869) 全量 11 行属性更新接口
    /// </summary>
    public void UpdateFullAbilities(
        int wearWeight, int maxWearWeight,
        int handWeight, int maxHandWeight,
        int accuracy, int agility,
        string magicDef, string poisonDef,
        string poisonRecover, string hpRecover, string mpRecover)
    {
        if (_statusPanelView == null) return;
        _statusPanelView.UpdateAbilityValue(2, $"{wearWeight}/{maxWearWeight}", wearWeight > maxWearWeight);
        _statusPanelView.UpdateAbilityValue(3, $"{handWeight}/{maxHandWeight}", handWeight > maxHandWeight);
        _statusPanelView.UpdateAbilityValue(4, $"{accuracy}");
        _statusPanelView.UpdateAbilityValue(5, $"{agility}");
        _statusPanelView.UpdateAbilityValue(6, magicDef);
        _statusPanelView.UpdateAbilityValue(7, poisonDef);
        _statusPanelView.UpdateAbilityValue(8, poisonRecover);
        _statusPanelView.UpdateAbilityValue(9, hpRecover);
        _statusPanelView.UpdateAbilityValue(10, mpRecover);
    }

    public void UpdateLevel(int level)
    {
        // 盛大原版官方 HUD 嵌入 Lv 徽章右侧黑槽 (636, 484)
        if (_lblLevel != null)
        {
            _lblLevel.Text = $"{level}";
        }
    }

    private int _hunger = 0;
    public int CurrentHunger => _hunger;

    public void UpdateHunger(int hunger)
    {
        _hunger = hunger;
        GD.Print($"[HudController] 角色体态/饥饿度更新 (SM_MYSTATUS=708): {_hunger}");
    }

    private int _lampDura = 0;
    public int CurrentLampDura => _lampDura;

    public void UpdateLampDura(int dura)
    {
        _lampDura = dura;
        GD.Print($"[HudController] 蜡烛/油灯持久更新 (SM_LAMPCHANGEDURA=655): {_lampDura}");
    }

    public void UpdateItemPrice(int price)
    {
        AddChat("商店", $"[商店] 物品估价：{price:N0} 金币", new Color(1f, 0.9f, 0.4f));
    }

    public void UpdateItemDurability(int dura)
    {
        AddChat("装备", $"[装备] 物品持久度变动：{dura}", new Color(0.7f, 0.9f, 1f));
    }

    public void OnActorParam(int actorId, int param)
    {
        GD.Print($"[HudController] 实体属性/体态更新 (SM_ACTOR_PARAM=654): actor={actorId}, param={param}");
    }

    public void OnKickMemberOk()
    {
        AddChat("组队", "[组队] 成员已被成功移出编组。", new Color(0.4f, 1f, 0.5f));
    }

    public void OnTakeOffFail(int slot)
    {
        AddChat("装备", $"[装备] 脱下装备失败 (槽位: {slot})，已原子回滚。", new Color(1f, 0.4f, 0.4f));
    }

    public void UpdateName(string name)
    {
        if (_lblName != null) _lblName.Text = name;
    }

    public void UpdateGold(int gold)
    {
        _currentGold = gold;
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
            if (_equipPanel.Visible)
            {
                // 盛大官方 1.5 DStateWnd 互斥原则：装备、状态、能力、技能面板同属 DStateWnd 四大 Page，绝对互斥
                if (_skillPanelView != null && _skillPanelView.Visible) _stackManager.PopWindow(_skillPanelView);
                if (_statusPanelView != null && _statusPanelView.Visible) _stackManager.PopWindow(_statusPanelView);
            }
            else
            {
                HideSlotTooltip();
            }
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
            if (_statusPanelView.Visible)
            {
                if (_equipPanel != null) _equipPanel.Visible = false;
                if (_skillPanelView != null && _skillPanelView.Visible) _stackManager.PopWindow(_skillPanelView);
            }
            RefreshBottomBarButtonStates();
        }
    }

    public void ToggleSkill()
    {
        if (_skillPanelView != null)
        {
            _stackManager.ToggleWindow(_skillPanelView);
            if (_skillPanelView.Visible)
            {
                if (_equipPanel != null) _equipPanel.Visible = false;
                if (_statusPanelView != null && _statusPanelView.Visible) _stackManager.PopWindow(_statusPanelView);
            }
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
            if (_tradePanelView.Visible)
            {
                // 依据官方 1.5 IDA sub_4915EC 实证：打开交易面板时自动呼出背包面板 (DialogManager_ShowHide(a1[179], 1))
                if (_bagPanel != null)
                {
                    _bagPanel.Position = Vector2.Zero;
                    _bagPanel.Visible = true;
                }
            }
            GD.Print($"[HudController] 切换交易面板显示状态: {_tradePanelView.Visible}");
        }
    }

    /// <summary>官方 1.5 底栏小地图按键切换 (IDA TFrmDlg_DBotMiniMapClick 0x490c80)</summary>
    public void ToggleMinimap()
    {
        _minimapView?.CycleMode();
    }

    private Vector2 _bagPosBeforeStorage;

    public void ToggleStorage()
    {
        // 官方仓库流程(服务端注释 + IDA sub_46DAE8): 复用 DSellDlg(Prguse_0392) 置 " 托管物品" 模式于 (260,176),
        // 背包 DItemBag 移至 (475,0)。存取协议(CM1031/1032, SM704/705)尚未接入。
        if (_repairPanelView == null) return;
        bool open = !_repairPanelView.Visible;
        _repairPanelView.SetStorageMode(open);
        _stackManager.ToggleWindow(_repairPanelView);
        if (_bagPanel != null)
        {
            if (open) { _bagPosBeforeStorage = _bagPanel.Position; _bagPanel.Position = new Vector2(475, 0); _bagPanel.Visible = true; }
            else { _bagPanel.Position = _bagPosBeforeStorage; _bagPanel.Visible = false; }
        }
        if (!open && _shopPanelView != null && _shopPanelView.Visible && _shopPanelView.CurrentMode == ShopPanelView.ShopMode.Storage)
        {
            _stackManager.PopWindow(_shopPanelView);
        }
        GD.Print($"[HudController] 切换仓库(DSellDlg 托管模式)显示状态: {_repairPanelView.Visible}");
    }

    public const int MaxNpcInteractDistance = 15; // 官方实证切比雪夫门限 <= 15 (M2Server sub_4DC2B8 / sub_4DC664)
    private Vector2I? _currentNpcTile;

    public void SetCurrentInteractingNpc(int npcUid, int tileX, int tileY)
    {
        _currentNpcTile = new Vector2I(tileX, tileY);
    }

    public void OnPlayerPositionChanged(int playerTileX, int playerTileY)
    {
        if (!_currentNpcTile.HasValue) return;

        int deltaX = Math.Abs(playerTileX - _currentNpcTile.Value.X);
        int deltaY = Math.Abs(playerTileY - _currentNpcTile.Value.Y);
        int distance = Math.Max(deltaX, deltaY); // 切比雪夫距离 (Chebyshev Distance)

        if (distance > MaxNpcInteractDistance)
        {
            CloseNpcBusinessWindows();
        }
    }

    public void CloseNpcBusinessWindows()
    {
        bool closedAny = false;
        if (_npcDialogView != null && _npcDialogView.Visible)
        {
            _stackManager.PopWindow(_npcDialogView);
            closedAny = true;
        }
        if (_shopPanelView != null && _shopPanelView.Visible)
        {
            _stackManager.PopWindow(_shopPanelView);
            closedAny = true;
        }
        if (_repairPanelView != null && _repairPanelView.Visible)
        {
            _stackManager.PopWindow(_repairPanelView);
            if (_bagPanel != null)
            {
                _bagPanel.Position = _bagPosBeforeStorage;
                _bagPanel.Visible = false;
            }
            closedAny = true;
        }

        if (closedAny)
        {
            _currentNpcTile = null;
            AddChat("系统", "[系统] 距离商人过远，对话已自动结束。", new Color(1f, 0.4f, 0.4f));
            GD.Print("[HudController] 超出官方 15 格距离限制，已自动关闭 NPC 交互窗口");
        }
    }

    public void ToggleShop(int npcUid = 0)
    {
        if (_shopPanelView != null)
        {
            if (_shopPanelView.Visible)
            {
                if (_shopPanelView.CurrentMode == ShopPanelView.ShopMode.Storage)
                {
                    _shopPanelView.SetMode(ShopPanelView.ShopMode.Buy);
                    _shopPanelView.OpenForNpc(npcUid);
                }
                else
                {
                    _stackManager.PopWindow(_shopPanelView);
                }
            }
            else
            {
                _shopPanelView.SetMode(ShopPanelView.ShopMode.Buy);
                _shopPanelView.OpenForNpc(npcUid);
                _stackManager.PushWindow(_shopPanelView);
            }
        }
    }

    public void OpenStorageList(IEnumerable<ShopPanelView.ShopItemEntry> items, int npcUid = 0)
    {
        if (_shopPanelView != null)
        {
            _shopPanelView.OpenForStorage(items, npcUid);
            _stackManager.PushWindow(_shopPanelView);
        }
    }

    public void ToggleRepair()
    {
        if (_repairPanelView != null)
        {
            _stackManager.ToggleWindow(_repairPanelView);
        }
    }

    public void OpenRepairPanel()
    {
        if (_repairPanelView != null && !_repairPanelView.Visible)
        {
            _repairPanelView.SetStorageMode(false);
            _stackManager.PushWindow(_repairPanelView);
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

    /// <summary>
    /// 官方 1.5 二级安全密码输入弹窗 (对齐 Delphi DPassword 窗口与 SM_NEEDPASSWORD=2014)
    /// </summary>
    public void ShowSecurityPasswordDialog(string prompt = "请输入仓库安全密码：", string defaultValue = "", Action<string>? onConfirm = null)
    {
        if (_messageBoxDialog != null)
        {
            _messageBoxDialog.ShowInput(prompt, defaultValue, onConfirm, isPassword: true);
            _stackManager.PushWindow(_messageBoxDialog);
        }
    }

    /// <summary>
    /// 处理服务端下发的 SM_NEEDPASSWORD 验证请求
    /// </summary>
    public void OnNeedPassword()
    {
        ShowSecurityPasswordDialog("该操作需要二次密码验证，请输入安全密码：", "", (pwd) =>
        {
            GD.Print($"[HudController] 二级安全密码已输入 (密文长度: {pwd.Length}), 提交验证");
        });
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

    public void ShowComparisonTooltip(string currentEquipName, string currentDesc, string newEquipName, string newDesc, Vector2 globalPos)
    {
        _itemTooltipView?.ShowComparisonTooltip(currentEquipName, currentDesc, newEquipName, newDesc, globalPos);
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
                case Key.V:
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
                case Key.H:
                {
                    if (keyEvent.CtrlPressed)
                    {
                        CycleNextAttackMode();
                        GetViewport().SetInputAsHandled();
                    }
                    break;
                }
                case Key.F:
                {
                    if (keyEvent.CtrlPressed)
                    {
                        ToggleThrusting();
                        GetViewport().SetInputAsHandled();
                    }
                    break;
                }
                case Key.B:
                {
                    if (keyEvent.CtrlPressed)
                    {
                        ToggleHalfMoon();
                        GetViewport().SetInputAsHandled();
                    }
                    break;
                }
            }
        }
    }

    public bool CloseTopDialog()
    {
        if (_minimapView != null && _minimapView.CurrentMode == MinimapView.MinimapMode.Big)
        {
            _minimapView.SetMode(MinimapView.MinimapMode.Closed);
            return true;
        }
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

    /// <summary>
    /// 设置背包指定槽位物品与属性 (由 SM_ADDITEM / SM_BAGITEMS 驱动)
    /// </summary>
    public void SetBagItem(int slot, string itemName, string iconFile, string desc)
    {
        if (slot < 0 || slot >= 40) return;
        _bagItemDetails[slot] = (itemName, desc);
        var btn = _bagSlots[slot];
        if (btn != null)
        {
            string folder = iconFile.StartsWith("Items") ? "Items" : (iconFile.StartsWith("DnItems") ? "DnItems" : "StateItem");
            btn.ItemTexture = string.IsNullOrEmpty(iconFile) ? null : ResPath.LoadImageTexture($"resources/wil/png/{folder}/{iconFile}");
        }
    }

    /// <summary>
    /// 添加物品到背包首个空闲槽位
    /// </summary>
    public bool AddBagItem(string itemName, string iconFile = "", string desc = "")
    {
        for (int i = 0; i < 40; i++)
        {
            if (_bagItemDetails[i] == null)
            {
                SetBagItem(i, itemName, iconFile, desc);
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 清空背包指定槽位 (由 SM_DELITEM 驱动)
    /// </summary>
    public void ClearBagItem(int slot)
    {
        if (slot < 0 || slot >= 40) return;
        _bagItemDetails[slot] = null;
        var btn = _bagSlots[slot];
        if (btn != null)
        {
            btn.ItemTexture = null;
        }
    }

    /// <summary>
    /// 清空背包全部 40 格
    /// </summary>
    public void ClearAllBagItems()
    {
        for (int i = 0; i < 40; i++)
        {
            ClearBagItem(i);
        }
    }

    /// <summary>
    /// 设置已穿戴装备槽位 (由 SM_EQUIPINFO / SM_TAKEON_OK / SM_TAKEOFF_OK 驱动)
    /// </summary>
    public void SetEquipSlot(int equipSlot, string? itemName, string? iconFile, string? desc)
    {
        if (equipSlot < 0 || equipSlot >= 9) return;
        if (string.IsNullOrEmpty(itemName))
        {
            _equipItemDetails[equipSlot] = null;
            var btn = _equipSlots[equipSlot];
            if (btn != null) btn.ItemTexture = null;

            if (equipSlot == 0) UpdateDollVisuals("", null, null);
            else if (equipSlot == 1) UpdateDollVisuals(null, "", null);
            else if (equipSlot == 4) UpdateDollVisuals(null, null, "");
        }
        else
        {
            _equipItemDetails[equipSlot] = (itemName, desc ?? string.Empty);
            var btn = _equipSlots[equipSlot];
            if (btn != null)
            {
                btn.ItemTexture = string.IsNullOrEmpty(iconFile) ? null : ResPath.LoadImageTexture($"resources/wil/png/StateItem/{iconFile}");
            }

            if (equipSlot == 0 && !string.IsNullOrEmpty(iconFile)) UpdateDollVisuals(iconFile, null, null);
            else if (equipSlot == 1 && !string.IsNullOrEmpty(iconFile)) UpdateDollVisuals(null, iconFile, null);
            else if (equipSlot == 4 && !string.IsNullOrEmpty(iconFile)) UpdateDollVisuals(null, null, iconFile);
        }
    }

    /// <summary>
    /// 盛大官方 1.5 装备低持久红字报警与危急频闪 (IDA sub_488AF4 / DStateWinDirectPaint)
    /// 当持久低于 3 点时启动槽位红色呼吸警报；持久降为 0 时提示即将碎裂。
    /// </summary>
    public void UpdateEquipDurability(int equipSlot, int curDura, int maxDura)
    {
        if (equipSlot < 0 || equipSlot >= 9) return;
        var btn = _equipSlots[equipSlot];
        if (btn == null) return;

        if (curDura <= 2)
        {
            btn.LowDurabilityWarning = true;
            string eqName = _equipItemDetails[equipSlot]?.Name ?? "装备";
            if (curDura == 0)
            {
                AddChat("装备", $"[装备] 危险：您的【{eqName}】持久已降为 0，继续受到物理攻击将可能彻底碎裂！", new Color(1f, 0.2f, 0.2f));
            }
            else
            {
                AddChat("装备", $"[装备] 警告：您的【{eqName}】持久仅剩 {curDura}/{maxDura} 点，请尽快修理！", new Color(1f, 0.7f, 0.2f));
            }
        }
        else
        {
            btn.LowDurabilityWarning = false;
        }
    }

    /// <summary>
    /// 盛大官方 1.5 角色发型更新接口 (官方 sub_488AF4: 男发基准440, 女发基准480)
    /// </summary>
    public void SetDollHair(int hairIndex = 1, bool isFemale = false)
    {
        if (_dollHair == null) return;
        int baseIdx = isFemale ? 480 : 440;
        int imgIdx = baseIdx + Math.Max(1, hairIndex);
        var tex = ResPath.LoadImageTexture($"resources/wil/png/Prguse/Prguse_{imgIdx:D4}.png");
        if (tex != null)
        {
            _dollHair.Texture = tex;
            _dollHair.Size = tex.GetSize();
            _dollHair.Position = isFemale ? new Vector2(74, 38) : new Vector2(73, 39);
            _dollHair.Visible = true;
        }
    }

    /// <summary>
    /// 盛大官方 1.5 角色纸娃娃立绘图层动态更新接口
    /// </summary>
    public void UpdateDollVisuals(string? clothFile, string? weaponFile, string? helmFile)
    {
        if (_dollCloth != null && clothFile != null)
        {
            if (string.IsNullOrEmpty(clothFile))
            {
                _dollCloth.Visible = false;
            }
            else
            {
                var tex = ResPath.LoadImageTexture($"resources/wil/png/StateItem/{clothFile}");
                if (tex != null)
                {
                    _dollCloth.Texture = tex;
                    _dollCloth.Size = tex.GetSize();
                    _dollCloth.Visible = true;
                }
            }
        }

        if (_dollWeapon != null && weaponFile != null)
        {
            if (string.IsNullOrEmpty(weaponFile))
            {
                _dollWeapon.Visible = false;
            }
            else
            {
                var tex = ResPath.LoadImageTexture($"resources/wil/png/StateItem/{weaponFile}");
                if (tex != null)
                {
                    _dollWeapon.Texture = tex;
                    _dollWeapon.Size = tex.GetSize();
                    // 木剑握把在手心 (20, 48)，其他武器 (如裁决/屠龙/龙纹) 在 (10, 28)
                    _dollWeapon.Position = weaponFile.Contains("0030") ? new Vector2(20, 48) : new Vector2(10, 28);
                    _dollWeapon.Visible = true;
                }
            }
        }

        if (_dollHelm != null && helmFile != null)
        {
            if (string.IsNullOrEmpty(helmFile))
            {
                _dollHelm.Visible = false;
                if (_dollHair != null) _dollHair.Visible = true; // 脱下头盔显露头发
            }
            else
            {
                var tex = ResPath.LoadImageTexture($"resources/wil/png/StateItem/{helmFile}");
                if (tex != null)
                {
                    _dollHelm.Texture = tex;
                    _dollHelm.Size = tex.GetSize();
                    _dollHelm.Visible = true;
                    // 全封闭或罩住头部的头盔覆盖头发
                    if (_dollHair != null) _dollHair.Visible = false;
                }
            }
        }
    }

    /// <summary>
    /// 打开并显示 NPC 交互脚本对话 (由 SM_NPCDIALOG 驱动)
    /// </summary>
    public void ShowNpcDialog(string content)
    {
        if (_npcDialogView != null)
        {
            _npcDialogView.ShowDialog(content);
            if (!_npcDialogView.Visible)
            {
                _stackManager.ToggleWindow(_npcDialogView);
            }
        }
    }

    /// <summary>
    /// 打开并展示商店货架 (由 SM_MERCHANTDLG / SM_SENDGOODSLIST 驱动)
    /// </summary>
    public void ShowShop(string npcName, System.Collections.Generic.IEnumerable<ShopPanelView.ShopItemEntry> items)
    {
        if (_shopPanelView != null)
        {
            _shopPanelView.SetItems(items);
            if (!_shopPanelView.Visible)
            {
                _stackManager.ToggleWindow(_shopPanelView);
            }
        }
    }

    /// <summary>
    /// 全局槽位拖拽放置响应分发 (支持 Bag->Bag, Bag->Equip, Equip->Bag, Bag->Repair, Bag->Storage)
    /// </summary>
    public void HandleSlotItemDrop(SlotButton.SlotType fromType, int fromSlot, SlotButton.SlotType toType, int toSlot)
    {
        // 1. 背包内部调换格子 (Bag -> Bag)
        if (fromType == SlotButton.SlotType.Bag && toType == SlotButton.SlotType.Bag)
        {
            SwapBagItems(fromSlot, toSlot);
            return;
        }

        // 2. 从背包穿戴装备到身上 (Bag -> Equip)
        if (fromType == SlotButton.SlotType.Bag && toType == SlotButton.SlotType.Equip)
        {
            WearBagItemToEquipSlot(fromSlot, toSlot);
            return;
        }

        // 3. 从装备脱下放回背包 (Equip -> Bag)
        if (fromType == SlotButton.SlotType.Equip && toType == SlotButton.SlotType.Bag)
        {
            TakeoffEquipSlotToBag(fromSlot, toSlot);
            return;
        }

        // 4. 从背包放入特修/托管面板 (Bag -> Repair / Storage)
        if (fromType == SlotButton.SlotType.Bag && (toType == SlotButton.SlotType.Repair || toType == SlotButton.SlotType.Storage))
        {
            if (_repairPanelView != null && _repairPanelView.Visible)
            {
                var item = _bagItemDetails[fromSlot];
                if (item != null)
                {
                    _repairPanelView.SetSlotItem(_bagSlots[fromSlot]?.ItemTexture, fromSlot, item.Value.Name);
                    string actionDesc = toType == SlotButton.SlotType.Storage ? "准备交付仓库保管" : "放置于铁匠修理台";
                    AddChat("系统", $"[系统] 物品 [{item.Value.Name}] 已{actionDesc}。", new Color(0.8f, 0.8f, 1f));
                }
            }
            return;
        }

        // 5. 从背包拖入快捷药水栏 1~6 格 (Bag -> Quick)
        if (fromType == SlotButton.SlotType.Bag && toType == SlotButton.SlotType.Quick)
        {
            EquipBagItemToQuickSlot(fromSlot, toSlot);
            return;
        }

        // 6. 从背包放入交易栏 (Bag -> Trade)
        if (fromType == SlotButton.SlotType.Bag && toType == SlotButton.SlotType.Trade)
        {
            PutBagItemToTradeSlot(fromSlot, toSlot);
            return;
        }

        // 7. 从交易栏取回背包 (Trade -> Bag)
        if (fromType == SlotButton.SlotType.Trade && toType == SlotButton.SlotType.Bag)
        {
            TakeTradeItemToBagSlot(fromSlot, toSlot);
            return;
        }
    }

    /// <summary>
    /// 背包放入交易栏 (IDA sub_4DD79C: 交易栏 10 格硬上限，对方/己方锁定防作弊门禁)
    /// </summary>
    public void PutBagItemToTradeSlot(int bagSlot, int tradeSlot)
    {
        if (_tradePanelView == null || !_tradePanelView.Visible)
        {
            AddChat("系统", "[交易] 交易面板未打开，无法放置物品。", new Color(1f, 0.4f, 0.4f));
            return;
        }

        if (_tradePanelView.IsMyLocked)
        {
            AddChat("系统", "[交易] 交易已锁定，不可添加物品！", new Color(1f, 0.4f, 0.4f));
            return;
        }

        if (tradeSlot < 0 || tradeSlot >= 10) return;
        if (bagSlot < 0 || bagSlot >= 40) return;

        var bagItem = _bagItemDetails[bagSlot];
        if (bagItem == null) return;

        if (_tradeItemOrigins[tradeSlot] != null)
        {
            AddChat("系统", "[交易] 该交易栏槽位已有物品！", new Color(1f, 0.4f, 0.4f));
            return;
        }

        var btn = _bagSlots[bagSlot];
        var tex = btn?.ItemTexture;

        _tradeItemOrigins[tradeSlot] = (bagItem.Value.Name, bagItem.Value.Desc, bagSlot);
        _tradePanelView.SetMyItem(tradeSlot, bagItem.Value.Name, tex);
        ClearBagItem(bagSlot);

        EmitSignal(SignalName.TradeAddItemRequested, tradeSlot, bagItem.Value.Name);
        AddChat("系统", $"[交易] 放入物品 [{bagItem.Value.Name}] 到交易栏 #{tradeSlot + 1} 格。", new Color(0.8f, 1f, 0.8f));
    }

    /// <summary>
    /// 交易栏物品取回背包 (IDA 0x4DD958（已证client无函数）: 锁定防作弊门禁与背包回退)
    /// </summary>
    public void TakeTradeItemToBagSlot(int tradeSlot, int targetBagSlot)
    {
        if (_tradePanelView == null || !_tradePanelView.Visible) return;

        if (_tradePanelView.IsMyLocked)
        {
            AddChat("系统", "[交易] 交易已锁定，不可取下物品！", new Color(1f, 0.4f, 0.4f));
            return;
        }

        if (tradeSlot < 0 || tradeSlot >= 10) return;
        var origin = _tradeItemOrigins[tradeSlot];
        if (origin == null) return;

        int bagSlot = (targetBagSlot >= 0 && targetBagSlot < 40 && _bagItemDetails[targetBagSlot] == null)
            ? targetBagSlot
            : (_bagItemDetails[origin.Value.OriginBagSlot] == null ? origin.Value.OriginBagSlot : FindFirstEmptyBagSlot());

        if (bagSlot < 0)
        {
            AddChat("系统", "[交易] 背包已满，无法取回物品！", new Color(1f, 0.4f, 0.4f));
            return;
        }

        string itemName = origin.Value.Name;
        SetBagItem(bagSlot, itemName, "", origin.Value.Desc);
        _tradeItemOrigins[tradeSlot] = null;
        _tradePanelView.ClearMyItem(tradeSlot);

        EmitSignal(SignalName.TradeDelItemRequested, tradeSlot);
        AddChat("系统", $"[交易] 取回交易物品 [{itemName}] 到背包 #{bagSlot + 1} 格。", new Color(0.8f, 0.8f, 1f));
    }

    /// <summary>
    /// 官方交易取消闭环 (IDA sub_467920 @ 0x46A5E3 与 sub_49FE38 实证: 遍历 10 格交易栏退回背包)
    /// </summary>
    public void RollbackTradeItemsToBag()
    {
        if (_tradePanelView == null) return;
        for (int i = 0; i < 10; i++)
        {
            if (_tradeItemOrigins[i] != null)
            {
                var origin = _tradeItemOrigins[i]!.Value;
                int bagSlot = (_bagItemDetails[origin.OriginBagSlot] == null) ? origin.OriginBagSlot : FindFirstEmptyBagSlot();
                if (bagSlot >= 0)
                {
                    SetBagItem(bagSlot, origin.Name, "", origin.Desc);
                }
                _tradeItemOrigins[i] = null;
                _tradePanelView.ClearMyItem(i);
            }
        }
        _tradePanelView.ResetTrade();
    }

    /// <summary>
    /// 寻找背包首个空闲槽位
    /// </summary>
    public int FindFirstEmptyBagSlot()
    {
        for (int i = 0; i < 40; i++)
        {
            if (_bagItemDetails[i] == null) return i;
        }
        return -1;
    }

    /// <summary>
    /// 打开面对面交易窗体 (由 SM_DEALMENU 673 驱动)
    /// <summary>
    /// 打开面对面交易窗体
    /// 盛大官方 1.5 铁证 (IDA sub_4915EC 0x491653/0x491663/0x491673):
    /// 交易开启时，背包面板 DItemBag 联动定位到 (0, 0) 并强制显示，与右上角 (464, 0) 交易面板形成左右对称联动
    /// </summary>
    public void OpenTrade(string targetName)
    {
        if (_tradePanelView != null)
        {
            _tradePanelView.OpenTrade(targetName);
            if (!_tradePanelView.Visible)
            {
                _stackManager.ToggleWindow(_tradePanelView);
            }
            if (_bagPanel != null)
            {
                _bagPanel.Position = Vector2.Zero;
                _bagPanel.Visible = true;
            }
            AddChat("系统", $"[交易] 与 [{targetName}] 开启面对面安全交易。", new Color(1f, 0.9f, 0.5f));
        }
    }

    /// <summary>
    /// 关闭交易窗体
    /// </summary>
    public void CloseTrade()
    {
        if (_tradePanelView != null && _tradePanelView.Visible)
        {
            _tradePanelView.CloseTrade();
        }
    }

    /// <summary>
    /// 服务端交易成功交割 (由 SM_DEALSUCCESS 687 / SM_DEALCOMPLETE 678 驱动)
    /// </summary>
    public void OnServerTradeSuccess()
    {
        if (_tradePanelView != null)
        {
            // 清空本地待取回暂存，因为物品已原子转移给对方
            for (int i = 0; i < 10; i++)
            {
                _tradeItemOrigins[i] = null;
            }
            _tradePanelView.Visible = false;
            _tradePanelView.ResetTrade();
            AddChat("系统", "[交易] 交易成功完成！", new Color(0.2f, 1f, 0.2f));
        }
    }

    /// <summary>
    /// 服务端交易取消回退 (由 SM_DEALCANCEL 688 / 674 / 681 驱动)
    /// </summary>
    public void OnServerTradeCancel(string reason)
    {
        RollbackTradeItemsToBag();
        if (_tradePanelView != null)
        {
            _tradePanelView.Visible = false;
        }
        string reasonStr = string.IsNullOrEmpty(reason) ? "交易已取消。" : $"交易取消: {reason}";
        AddChat("系统", $"[交易] {reasonStr}", new Color(1f, 0.4f, 0.4f));
    }

    /// <summary>
    /// 服务端确认己方放入物品成功 (由 SM_DEALADDITEM_OK 675 驱动)
    /// </summary>
    public void OnServerTradeAddItemOk(int slot, string itemName)
    {
        AddChat("交易", $"[交易] 确认放入物品 [{itemName}] 到交易栏 #{slot + 1} 格。", new Color(0.8f, 1f, 0.8f));
    }

    /// <summary>
    /// 服务端确认己方取下物品成功 (由 SM_DEALDELITEM_OK 677 驱动)
    /// </summary>
    public void OnServerTradeDelItemOk(int slot)
    {
        AddChat("交易", $"[交易] 确认从交易栏 #{slot + 1} 格取下物品。", new Color(0.8f, 0.8f, 1f));
    }

    /// <summary>
    /// 对方放入交易物品 (由 SM_DEALREMOTEADDITEM 682 驱动)
    /// </summary>
    public void OnServerTradeRemoteAddItem(int slot, string itemName)
    {
        if (_tradePanelView != null)
        {
            var tex = ResPath.LoadImageTexture("resources/wil/png/Items/Items_0002.png");
            _tradePanelView.SetRemoteItem(slot, itemName, tex);
            AddChat("系统", $"[交易] 对方放入了物品 [{itemName}]。", new Color(1f, 0.9f, 0.6f));
        }
    }

    /// <summary>
    /// 对方取回交易物品 (由 SM_DEALREMOTEDELITEM 683 驱动)
    /// </summary>
    public void OnServerTradeRemoteDelItem(int slot)
    {
        if (_tradePanelView != null)
        {
            string? name = _tradePanelView.GetRemoteItem(slot);
            _tradePanelView.ClearRemoteItem(slot);
            AddChat("系统", $"[交易] 对方取回了物品 [{name ?? $"槽位#{slot + 1}"}]。", new Color(1f, 0.7f, 0.5f));
        }
    }

    /// <summary>
    /// 交易金币变更同步 (由 SM_DEALCHGGOLD_OK 684 / SM_DEALREMOTECHGGOLD 686 驱动)
    /// </summary>
    public void OnServerTradeChgGold(int gold, bool isMine)
    {
        if (_tradePanelView != null)
        {
            if (isMine)
            {
                _tradePanelView.SetMyGold(gold);
            }
            else
            {
                _tradePanelView.SetRemoteGold(gold);
                AddChat("系统", $"[交易] 对方修改出资交易金币为 {gold:N0}。", new Color(1f, 0.9f, 0.5f));
            }
        }
    }

    /// <summary>
    /// 背包物品内部交换或移动
    /// </summary>
    public void SwapBagItems(int slotA, int slotB)
    {
        if (slotA < 0 || slotA >= 40 || slotB < 0 || slotB >= 40 || slotA == slotB) return;
        var tempDetails = _bagItemDetails[slotA];
        _bagItemDetails[slotA] = _bagItemDetails[slotB];
        _bagItemDetails[slotB] = tempDetails;

        var btnA = _bagSlots[slotA];
        var btnB = _bagSlots[slotB];
        if (btnA != null && btnB != null)
        {
            var tempTex = btnA.ItemTexture;
            btnA.ItemTexture = btnB.ItemTexture;
            btnB.ItemTexture = tempTex;
        }
        AddChat("系统", $"[系统] 物品已调整至背包 #{slotB + 1} 格。", new Color(0.8f, 0.8f, 0.8f));
    }

    /// <summary>
    /// 判断物品是否符合指定装备槽位规约 (IDA sub_488AF4 槽位合法性验证)
    /// 0: 衣服, 1: 武器, 2: 勋章/饰品, 3: 项链, 4: 头盔, 5/6: 手镯, 7/8: 戒指
    /// </summary>
    public static bool IsItemCompatibleWithEquipSlot(string itemName, int equipSlot)
    {
        if (string.IsNullOrEmpty(itemName)) return false;

        return equipSlot switch
        {
            0 => itemName.Contains("甲") || itemName.Contains("袍") || itemName.Contains("衣") || itemName.Contains("布衣"),
            1 => itemName.Contains("剑") || itemName.Contains("刀") || itemName.Contains("斧") ||
                 itemName.Contains("杖") || itemName.Contains("棍") || itemName.Contains("扇") ||
                 itemName.Contains("枪") || itemName.Contains("匕首") || itemName.Contains("镐") ||
                 itemName.Contains("锤") || itemName.Contains("裁决") || itemName.Contains("屠龙") ||
                 itemName.Contains("骨玉") || itemName.Contains("龙纹") || itemName.Contains("凝霜") ||
                 itemName.Contains("炼狱") || itemName.Contains("降魔") || itemName.Contains("银蛇") ||
                 itemName.Contains("井中月") || itemName.Contains("海魂") || itemName.Contains("修罗") ||
                 itemName.Contains("偃月"),
            2 => itemName.Contains("勋章") || itemName.Contains("火把") || itemName.Contains("蜡烛") ||
                 itemName.Contains("护身符") || itemName.Contains("药粉") || itemName.Contains("毒药"),
            3 => itemName.Contains("项链") || itemName.Contains("铃铛"),
            4 => itemName.Contains("头盔") || itemName.Contains("头巾") || itemName.Contains("冠"),
            5 or 6 => itemName.Contains("手镯") || itemName.Contains("手套"),
            7 or 8 => itemName.Contains("戒指") || itemName.Contains("指环"),
            _ => false
        };
    }

    /// <summary>
    /// 背包穿戴装备到装备槽位
    /// </summary>
    public void WearBagItemToEquipSlot(int bagSlot, int equipSlot)
    {
        if (bagSlot < 0 || bagSlot >= 40 || equipSlot < 0 || equipSlot >= 9) return;
        var bagItem = _bagItemDetails[bagSlot];
        if (bagItem == null) return;

        if (!IsItemCompatibleWithEquipSlot(bagItem.Value.Name, equipSlot))
        {
            AddChat("系统", "[系统] 该物品无法装备到此位置。", new Color(1f, 0.3f, 0.3f));
            return;
        }

        // 派发穿戴请求信号
        EmitSignal(SignalName.WearItemRequested, bagSlot, equipSlot);
        PlayEquipSlotSound(equipSlot);

        var curEquip = _equipItemDetails[equipSlot];
        var btnBag = _bagSlots[bagSlot];
        var btnEquip = _equipSlots[equipSlot];

        var bagTex = btnBag?.ItemTexture;
        var equipTex = btnEquip?.ItemTexture;

        // 互换或装上
        _equipItemDetails[equipSlot] = bagItem;
        if (btnEquip != null) btnEquip.ItemTexture = bagTex;

        if (curEquip != null)
        {
            _bagItemDetails[bagSlot] = curEquip;
            if (btnBag != null) btnBag.ItemTexture = equipTex;
        }
        else
        {
            _bagItemDetails[bagSlot] = null;
            if (btnBag != null) btnBag.ItemTexture = null;
        }

        // 纸娃娃立绘图层同步
        if (equipSlot == 0)
        {
            _dollCloth?.SetVisible(true);
        }
        else if (equipSlot == 1)
        {
            _dollWeapon?.SetVisible(true);
        }
        else if (equipSlot == 4)
        {
            _dollHelm?.SetVisible(true);
        }

        AddChat("系统", $"[系统] 成功穿戴装备 [{bagItem.Value.Name}]。", new Color(0.3f, 1f, 0.3f));
    }

    /// <summary>
    /// 从身上脱下装备放回背包
    /// </summary>
    public void TakeoffEquipSlotToBag(int equipSlot, int targetBagSlot)
    {
        if (equipSlot < 0 || equipSlot >= 9) return;
        var equipItem = _equipItemDetails[equipSlot];
        if (equipItem == null) return;

        // 如果指定的 targetBagSlot 已经被占，寻找首个空格
        int destSlot = targetBagSlot;
        if (destSlot < 0 || destSlot >= 40 || _bagItemDetails[destSlot] != null)
        {
            destSlot = -1;
            for (int i = 0; i < 40; i++)
            {
                if (_bagItemDetails[i] == null)
                {
                    destSlot = i;
                    break;
                }
            }
        }

        if (destSlot == -1)
        {
            AddChat("系统", "[系统] 背包已满，无法卸下装备。", new Color(1f, 0.3f, 0.3f));
            return;
        }

        // 派发脱装请求信号
        EmitSignal(SignalName.TakeoffItemRequested, equipSlot);
        PlayEquipSlotSound(equipSlot);

        var btnEquip = _equipSlots[equipSlot];
        var btnBag = _bagSlots[destSlot];

        _bagItemDetails[destSlot] = equipItem;
        if (btnBag != null) btnBag.ItemTexture = btnEquip?.ItemTexture;

        _equipItemDetails[equipSlot] = null;
        if (btnEquip != null) btnEquip.ItemTexture = null;

        // 纸娃娃立绘清除
        if (equipSlot == 0) UpdateDollVisuals("", null, null);
        else if (equipSlot == 1) UpdateDollVisuals(null, "", null);
        else if (equipSlot == 4) UpdateDollVisuals(null, null, "");

        AddChat("系统", $"[系统] 卸下装备 [{equipItem.Value.Name}] 至背包 #{destSlot + 1} 格。", new Color(1f, 0.9f, 0.4f));
    }

    private static void PlayEquipSlotSound(int equipSlot)
    {
        int sid = equipSlot switch
        {
            0 => AudioManager.SoundId.ClickArmor,
            1 => AudioManager.SoundId.ClickWeapon,
            3 => AudioManager.SoundId.ClickNecklace,
            4 => AudioManager.SoundId.ClickHelmet,
            5 or 6 => AudioManager.SoundId.ClickArmring,
            7 or 8 => AudioManager.SoundId.ClickRing,
            _ => AudioManager.SoundId.ItemClick
        };
        AudioManager.Instance?.PlaySoundId(sid);
    }

    /// <summary>
    /// 判断物品是否属于可快捷使用的消耗品 (药水、卷轴、油类)
    /// </summary>
    public static bool IsConsumableQuickItem(string itemName)
    {
        if (string.IsNullOrEmpty(itemName)) return false;
        return itemName.Contains("药") || itemName.Contains("水") || 
               itemName.Contains("卷") || itemName.Contains("油");
    }

    /// <summary>
    /// 从背包拖拽药水或消耗品装配至快捷栏 1~6 格
    /// </summary>
    public void EquipBagItemToQuickSlot(int bagSlot, int quickSlot)
    {
        if (bagSlot < 0 || bagSlot >= 40 || quickSlot < 0 || quickSlot >= 6) return;
        var item = _bagItemDetails[bagSlot];
        if (item == null) return;

        if (!IsConsumableQuickItem(item.Value.Name))
        {
            AddChat("系统", "[系统] 快捷栏只能放置药水或卷轴。", new Color(1f, 0.3f, 0.3f));
            return;
        }

        var btn = _bagSlots[bagSlot];
        var tex = btn?.ItemTexture;

        // 设置到快捷栏 (默认数量 1，若已存在同名物品则累加)
        int newCount = 1;
        if (_quickSlotItems[quickSlot] == item.Value.Name)
        {
            newCount = _quickSlotAmounts[quickSlot] + 1;
        }
        SetQuickSlot(quickSlot, item.Value.Name, newCount, tex);

        // 从背包扣除物品
        ClearBagItem(bagSlot);

        AddChat("系统", $"[系统] 物品 [{item.Value.Name}] 已装配至快捷栏 #{quickSlot + 1} 格。", new Color(0.4f, 1f, 0.4f));
    }

    public sealed partial class GroundDropCatcher : Control
    {
        private readonly HudController _owner;
        public GroundDropCatcher(HudController owner)
        {
            _owner = owner;
            Name = "GroundDropCatcher";
            Size = new Vector2(800, 600);
            Position = Vector2.Zero;
            MouseFilter = MouseFilterEnum.Pass;
            ZIndex = -2;
        }

        public override bool _CanDropData(Vector2 atPosition, Variant data)
        {
            if (data.VariantType != Variant.Type.Dictionary) return false;
            var dict = data.AsGodotDictionary();
            if (!dict.ContainsKey("type") || dict["type"].AsString() != "item_drag") return false;
            int fromType = dict["from_type"].AsInt32();
            return fromType == (int)SlotButton.SlotType.Bag;
        }

        public override void _DropData(Vector2 atPosition, Variant data)
        {
            if (!_CanDropData(atPosition, data)) return;
            var dict = data.AsGodotDictionary();
            int fromType = dict["from_type"].AsInt32();
            int fromSlot = dict["from_slot"].AsInt32();
            if (fromType == (int)SlotButton.SlotType.Bag)
            {
                _owner.DropBagItemToGround(fromSlot, atPosition);
            }
        }
    }

    /// <summary>
    /// 将背包物品拖拽至大世界地面丢弃
    /// </summary>
    public void DropBagItemToGround(int bagSlot, Vector2 screenPos)
    {
        if (bagSlot < 0 || bagSlot >= 40) return;
        var item = _bagItemDetails[bagSlot];
        if (item == null) return;

        string itemName = item.Value.Name;

        // 扣减清空背包槽位
        ClearBagItem(bagSlot);

        // 派发信号通知大世界场景生成战利品
        EmitSignal(SignalName.ItemDropToGroundRequested, bagSlot, itemName, screenPos);

        AddChat("系统", $"[系统] 物品 [{itemName}] 已丢弃至地面。", new Color(1f, 0.8f, 0.4f));
    }

    /// <summary>
    /// 槽位双击全局响应分发 (盛大官方 1.5 IDA sub_48E33C / sub_48C188)
    /// </summary>
    public void HandleSlotDoubleClick(SlotButton.SlotType slotType, int slotIndex)
    {
        switch (slotType)
        {
            case SlotButton.SlotType.Bag:
                HandleBagSlotDoubleClick(slotIndex);
                break;
            case SlotButton.SlotType.Equip:
                // 双击装备栏：脱装至背包
                TakeoffEquipSlotToBag(slotIndex, -1);
                break;
            case SlotButton.SlotType.Quick:
                // 双击快捷栏：使用药水
                UseQuickSlot(slotIndex);
                break;
        }
    }

    /// <summary>
    /// 背包物品双击快捷操作 (盛大官方 1.5 IDA sub_48E33C / TFrmDlg_DItemGridDblClick):
    /// 1. 消耗品/药水/食物/卷轴: 直接使用 (发送 CM_EATITEM, 播放音效 107, 扣减物品)
    /// 2. 装备类 (武器/衣服/首饰等): 自动寻找最优槽位穿戴 (发送 CM_WEARITEM, 互换/装上)
    /// </summary>
    public void HandleBagSlotDoubleClick(int bagSlot)
    {
        if (bagSlot < 0 || bagSlot >= 40) return;
        var item = _bagItemDetails[bagSlot];
        if (item == null || string.IsNullOrEmpty(item.Value.Name)) return;

        string itemName = item.Value.Name;

        if (IsConsumableItem(itemName))
        {
            // 消耗品/吃药使用
            ConsumeBagItem(bagSlot, itemName);
        }
        else
        {
            // 装备穿戴
            int bestEquipSlot = GetBestEquipSlotForItem(itemName);
            if (bestEquipSlot >= 0)
            {
                WearBagItemToEquipSlot(bagSlot, bestEquipSlot);
            }
            else
            {
                AddChat("系统", $"[系统] 该物品 [{itemName}] 无法直接装备。", new Color(1f, 0.4f, 0.4f));
            }
        }
    }

    public static bool IsConsumableItem(string itemName)
    {
        if (string.IsNullOrEmpty(itemName)) return false;
        return itemName.Contains("药") || itemName.Contains("水") || itemName.Contains("卷") || 
               itemName.Contains("肉") || itemName.Contains("油") || itemName.Contains("丹") ||
               itemName.Contains("包") || itemName.Contains("酒");
    }

    public static int GetBestEquipSlotForItem(string itemName)
    {
        if (string.IsNullOrEmpty(itemName)) return -1;
        if (itemName.Contains("甲") || itemName.Contains("袍") || itemName.Contains("衣") || itemName.Contains("布衣")) return 0; // 衣服
        if (itemName.Contains("剑") || itemName.Contains("刀") || itemName.Contains("斧") || itemName.Contains("杖") || 
            itemName.Contains("扇") || itemName.Contains("枪") || itemName.Contains("棍") || itemName.Contains("弓") || 
            itemName.Contains("匕首") || itemName.Contains("刃") || itemName.Contains("月") || itemName.Contains("屠龙") || 
            itemName.Contains("炼狱") || itemName.Contains("银蛇") || itemName.Contains("降魔") || itemName.Contains("凝霜") || 
            itemName.Contains("修罗") || itemName.Contains("破魂") || itemName.Contains("凌风")) return 1; // 武器
        if (itemName.Contains("头盔") || itemName.Contains("冠") || itemName.Contains("帽") || itemName.Contains("头巾")) return 4; // 头盔
        if (itemName.Contains("项链") || itemName.Contains("链")) return 3; // 项链
        if (itemName.Contains("手镯") || itemName.Contains("手套") || itemName.Contains("镯")) return 5; // 手镯 (默认左手镯)
        if (itemName.Contains("戒") || itemName.Contains("指")) return 7; // 戒指 (默认左戒指)
        if (itemName.Contains("勋章") || itemName.Contains("佩") || itemName.Contains("符") || itemName.Contains("饰品")) return 2; // 勋章/饰品
        return -1;
    }

    public void ConsumeBagItem(int bagSlot, string itemName)
    {
        if (bagSlot < 0 || bagSlot >= 40) return;
        // 播放经典吃药吞咽音效 (官方 sound 107, SoundId.EatDrug)
        AudioManager.Instance?.PlaySoundId(AudioManager.SoundId.EatDrug);
        
        // 派发信号供网络层发包 CM_EATITEM (1006)
        EmitSignal(SignalName.UseItemRequested, bagSlot);

        // 清理/扣减背包槽位
        ClearBagItem(bagSlot);

        AddChat("系统", $"[系统] 使用了 [{itemName}]。", new Color(0.4f, 1f, 0.4f));
    }

    /// <summary>
    /// 服务端 SM_MERCHANTDLG_CLOSE (644) 驱动关闭 NPC 窗体
    /// </summary>
    public void CloseNpcDialog()
    {
        if (_npcDialogView != null && _npcDialogView.Visible)
        {
            _npcDialogView.Visible = false;
            AddChat("系统", "[系统] NPC 对话已关闭。", new Color(0.7f, 0.7f, 0.7f));
        }
    }

    /// <summary>
    /// 展示官方系统模态提示框 (对齐 sub_467920 show_dialog_message)
    /// </summary>
    public void ShowModalMessage(string title, string msg)
    {
        if (_messageBoxDialog != null)
        {
            _messageBoxDialog.ShowMessage($"[{title}]\n\n{msg}", hasCancel: false);
        }
        else
        {
            AddChat("系统", $"[{title}] {msg}", new Color(1f, 0.3f, 0.3f));
        }
    }

    /// <summary>
    /// 展示官方系统模态提示框 (对齐 sub_467920 show_dialog_message，带确认回调与取消按钮)
    /// </summary>
    public void ShowModalMessage(string title, string msg, bool hasCancel, Action? onConfirm)
    {
        if (_messageBoxDialog != null)
        {
            _messageBoxDialog.ShowMessage($"[{title}]\n\n{msg}", hasCancel: hasCancel, onConfirm: onConfirm);
        }
        else
        {
            AddChat("系统", $"[{title}] {msg}", new Color(1f, 0.3f, 0.3f));
            onConfirm?.Invoke();
        }
    }
}
