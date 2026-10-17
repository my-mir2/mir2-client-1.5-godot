using System;
using System.Collections.Generic;
using Godot;
using Mir2.Shared.Rules;
using Mir2Client.Audio;
using Mir2Client.Game;
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
    public delegate void UseItemRequestedEventHandler(int slot, string itemName);

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

    [Signal]
    public delegate void StorageDepositRequestedEventHandler(int bagSlot, string itemName);

    [Signal]
    public delegate void StorageWithdrawRequestedEventHandler(int storageSlot);

    [Signal]
    public delegate void StorageDepositGoldRequestedEventHandler(int amount);

    [Signal]
    public delegate void StorageWithdrawGoldRequestedEventHandler(int amount);

    [Signal]
    public delegate void MinimapToggledEventHandler();

    [Signal]
    public delegate void SplitItemRequestedEventHandler(int bagSlot, int splitCount);

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
    private Control? _levelNumberContainer;
    private Label? _lblName;
    private Label? _lblMapCoord;
    private Label? _lblEquipCharName;
    private string _playerName = "热血战士";
    private string _currentMapTitle = "比奇省";
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
    private int _playerGender = 0;
    private bool _hasLearnedMpSkill = false;
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
    private int _currentHp = 100;
    private int _maxHp = 100;
    private float _displayHpRatio = 1.0f;
    private float _targetHpRatio = 1.0f;
    private float _displayMpRatio = 0.0f;
    private float _targetMpRatio = 0.0f;
    private bool _isStunned = false;
    private bool _isFrozen = false;

    /// <summary>盛大官方 1.5 装备槽位挂起事务 (网络防重入互斥与原子回滚快照)</summary>
    public struct PendingEquipAction
    {
        public enum ActionType { Wear, TakeOff }
        public ActionType Type;
        public int BagSlot;
        public int EquipSlot;
        public (string Name, string Desc)? OriginalBagItem;
        public (string Name, string Desc)? OriginalEquipItem;
        public Texture2D? OriginalBagTex;
        public Texture2D? OriginalEquipTex;
        public long Timestamp;
    }
    private readonly Dictionary<int, PendingEquipAction> _pendingEquipActions = new();
    private static bool[,]? _hudBottomBarAlphaMask; // 盛大官方 1.5 原版底栏异形像素命中检测掩码 (IDA 0x48ba74 TFrmDlg_DBottomInRealArea)

    public int CurrentHp => _currentHp;
    public int MaxHp => _maxHp;
    public bool IsDead => _currentHp <= 0;
    public bool IsStunned => _isStunned;
    public bool IsFrozen => _isFrozen;

    public void SetStunned(bool stunned) => _isStunned = stunned;
    public void SetFrozen(bool frozen) => _isFrozen = frozen;

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
    public MessageBoxDialog? MessageBoxDialog => _messageBoxDialog;
    public NpcDialogView? NpcDialog => _npcDialogView;
    public DeathScreenView? DeathScreen => _deathScreenView;
    public MinimapView? Minimap => _minimapView;
    public ItemTooltipView? ItemTooltip => _itemTooltipView;

    public int PendingEquipActionCount => _pendingEquipActions.Count;
    public PendingEquipAction? GetPendingEquipAction(int slot) =>
        _pendingEquipActions.TryGetValue(slot, out var act) ? act : null;
    public void ClearPendingEquipActions() => _pendingEquipActions.Clear();
    public (string Name, string Desc)? GetBagItemDetail(int slot) =>
        slot >= 0 && slot < 40 ? _bagItemDetails[slot] : null;
    public (string Name, string Desc)? GetEquipItemDetail(int slot) =>
        slot >= 0 && slot < 9 ? _equipItemDetails[slot] : null;

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
    private long _lastEatTick = 0; // 盛大官方 1.5 IDA sub_48E33C / sub_48C188 吃药 200ms 全局防连点时钟
#if MIR2_SANDBOX_DEV
    public bool IsSandboxMode { get; set; } = false;
#endif
    private int _lastUsedQuickSlot = -1;

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
    private int _selectedBagSlot = -1;

    // 官方 1.5 聊天输入历史记录 (IDA TFrmMain_FormKeyDown 上下方向键回滚 / BtnChatHistory)
    private readonly List<string> _chatHistory = new();
    private int _historyIndex = 0;

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
    private Label? _lblExpValue;
    private Label? _lblWeightValue;

    private int _currentAttackMode = 0;
    public int CurrentAttackMode => _currentAttackMode;

    private int _currentGold = 0;
    public int CurrentGold => _currentGold;

    /// <summary>攻击模式图标(官方 sub_48B2A0 0x48b8e0..0x48b92b): mode∈[1,4] 取 Prguse_(15+mode), 其余(全体模式0)绝不绘制月亮。</summary>
    public void SetAttackMode(int mode)
    {
        _currentAttackMode = mode;
        if (_attackModeIcon == null) return;
        if (mode <= 0 || mode > 4) { _attackModeIcon.Visible = false; return; }
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

    /// <summary>快捷键 Ctrl+H 攻击模式循环切换意图上报 (0全体 -> 1和平 -> 2组队 -> 3行会 -> 4善恶)</summary>
    public void CycleNextAttackMode()
    {
        int nextMode = _currentAttackMode >= 4 ? 0 : _currentAttackMode + 1;
        // 现代服务端权威架构：客户端只向服务端上报切换意图，由服务端权威仲裁并回发 SM_ATTACKMODECHANGED 驱动同步
        EmitSignal(SignalName.ChangeAttackModeRequested, nextMode);
    }

    /// <summary>官方 1.5 小退/换人 (IDA TFrmDlg_DBotLogoutClick 0x491474 / sub_486DF0 模态确认防误触)</summary>
    public void RequestSoftClose()
    {
        ShowModalMessage("小退提示", "你真的想要重新选择角色吗？", hasCancel: true, onConfirm: () =>
        {
            AddChat("系统", "[系统] 正在注销当前角色并返回选角神龛...", new Color(1f, 0.8f, 0.4f));
            EmitSignal(SignalName.SoftCloseRequested);
        });
    }

    /// <summary>官方 1.5 退出游戏 (IDA TFrmDlg_DBotExitClick 0x491520 / sub_486DF0 模态确认防误触)</summary>
    public void RequestExitGame()
    {
        ShowModalMessage("退出提示", "你真的想要退出游戏吗？", hasCancel: true, onConfirm: () =>
        {
            EmitSignal(SignalName.ExitGameRequested);
        });
    }

    /// <summary>官方 1.5 丢弃金币意图上报 (IDA TFrmDlg_DGoldClick 0x48e564, 发包 CM_DROPGOLD 1016)</summary>
    public void RequestDropGold(int amount)
    {
        if (amount <= 0 || amount > _currentGold) return;
        // 现代服务端权威架构：严禁客户端本地擅自扣减金币！仅发出丢弃意图，由服务端成功交割后权威下发同步
        EmitSignal(SignalName.DropGoldRequested, amount);
    }

    /// <summary>服务端 SM_ATTACKMODECHANGED (766, IDA sub_467920 case 766 @ 0x468A2B) 权威驱动回显</summary>
    public void OnServerAttackModeChanged(int mode)
    {
        SetAttackMode(mode);
        string modeDesc = mode switch
        {
            0 => "全体攻击模式",
            1 => "和平攻击模式",
            2 => "组队攻击模式",
            3 => "行会攻击模式",
            4 => "善恶攻击模式",
            _ => "全体攻击模式"
        };
        AddChat("系统", $"[系统] 攻击模式已切换为: {modeDesc}。", new Color(1f, 0.95f, 0.3f));
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

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (_orbShaderMat != null)
        {
            if (Math.Abs(_displayHpRatio - _targetHpRatio) > 0.001f)
            {
                _displayHpRatio = Mathf.MoveToward(_displayHpRatio, _targetHpRatio, (float)delta * 2.5f);
                _orbShaderMat.SetShaderParameter("hp_ratio", _displayHpRatio);
            }
            if (Math.Abs(_displayMpRatio - _targetMpRatio) > 0.001f)
            {
                _displayMpRatio = Mathf.MoveToward(_displayMpRatio, _targetMpRatio, (float)delta * 2.5f);
                _orbShaderMat.SetShaderParameter("mp_ratio", _displayMpRatio);
            }
        }
    }

    public override void _Ready()
    {
        _lblHp = GetNodeOrNull<Label>("StatusBar/LblHP");
        _lblMp = GetNodeOrNull<Label>("StatusBar/LblMP");
        _lblLevel = GetNodeOrNull<Label>("StatusBar/LblLevel");
        _lblName = GetNodeOrNull<Label>("StatusBar/LblName");
        _chatLog = GetNodeOrNull<RichTextLabel>("ChatBox/ChatLog");
        if (_chatLog != null)
        {
            // 盛大官方 1.5 聊天框点阵字体锐化 (彻底消除双线性滤波抗锯齿导致的发虚麻呼)
            _chatLog.TextureFilter = CanvasItem.TextureFilterEnum.Nearest;
            if (FontManager.OfficialSongti != null)
            {
                _chatLog.AddThemeFontOverride("normal_font", FontManager.OfficialSongti);
                _chatLog.AddThemeFontOverride("bold_font", FontManager.OfficialSongti);
            }
            _chatLog.AddThemeFontSizeOverride("normal_font_size", FontManager.OfficialFontSize);
            _chatLog.AddThemeFontSizeOverride("bold_font_size", FontManager.OfficialFontSize);
            var themeFont = _chatLog.GetThemeFont("normal_font");
            if (themeFont is FontFile fontFile)
            {
                fontFile.Antialiasing = TextServer.FontAntialiasing.None;
                fontFile.Hinting = TextServer.Hinting.None;
                fontFile.SubpixelPositioning = TextServer.SubpixelPositioning.Disabled;
            }
        }
        _chatInput = GetNodeOrNull<LineEdit>("ChatInput");
        if (_chatInput != null)
        {
            if (FontManager.OfficialSongti != null)
            {
                _chatInput.AddThemeFontOverride("font", FontManager.OfficialSongti);
            }
            _chatInput.AddThemeFontSizeOverride("font_size", FontManager.OfficialFontSize);
            _chatInput.MaxLength = 70; // 盛大官方 1.5 聊天单行最大 70 字符规约 (ClMain.pas / IDA sub_461DEC)
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

            // 盛大官方 1.5 原版底栏异形像素命中检测 (IDA 0x48ba74 TFrmDlg_DBottomInRealArea 权威实证)
            // 提取 Prguse_0001 的非透明像素矩阵，镂空/透明区域放行大世界点击寻路
            if (_hudBottomBarAlphaMask == null)
            {
                try
                {
                    var img = hudBgTex.GetImage();
                    if (img != null)
                    {
                        int w = img.GetWidth();
                        int h = img.GetHeight();
                        _hudBottomBarAlphaMask = new bool[w, h];
                        for (int y = 0; y < h; y++)
                        {
                            for (int x = 0; x < w; x++)
                            {
                                _hudBottomBarAlphaMask[x, y] = img.GetPixel(x, y).A > 0.05f;
                            }
                        }
                    }
                }
                catch
                {
                    // 降级兜底
                }
            }
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
                _orbShaderMat.SetShaderParameter("mp_ratio", (_playerJob == 0 && !_hasLearnedMpSkill) ? 0.0f : 1.0f);
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
        // 官方 1.5 原版等级艺术字贴图数字容器 (IDA 0x48b749 @ sub_48B2A0: (660, 496), Prguse_0030..0039)
        _levelNumberContainer = new Control
        {
            Name = "HudLevelNumberContainer",
            Position = new Vector2(660, 496),
            MouseFilter = Control.MouseFilterEnum.Ignore,
            ZIndex = 2
        };
        AddChild(_levelNumberContainer);
        UpdateLevelTextureDisplay(_playerLevel);

        _lblLevel = new Label
        {
            Name = "HudLevelLabel",
            Position = new Vector2(660, 496),
            Size = new Vector2(40, 16),
            Text = "1",
            ZIndex = 2,
            Visible = false // 盛大官方 1.5 铁律：原版使用 Prguse_0030..0039 贴图艺术字，普通 Label 隐蔽防重叠
        };
        AddChild(_lblLevel);

        // 盛大官方 1.5 左下角地图名称与角色坐标常显标签 (DrawScrn.pas 0x47AA09: mov ecx, 0x244; mov edx, 8 -> BoldTextOut(MSurface, 8, 580, clWhite, clBlack, MapTitle + ' ' + XX + ':' + YY))
        _lblMapCoord = new Label
        {
            Name = "HudMapCoordLabel",
            Position = new Vector2(8, 578),
            Size = new Vector2(180, 16),
            Text = "比奇省 330:270",
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Center,
            MouseFilter = Control.MouseFilterEnum.Ignore,
            ZIndex = 3
        };
        FontManager.ApplyOfficialStyle(_lblMapCoord, Colors.White, 12, true);
        AddChild(_lblMapCoord);

        // 攻击模式图标 (IDA sub_48B2A0 0x48b8e0..0x48b92b: 当 mode∈[1,4] 时取 Prguse_(15+mode)，以 (754,553) 叠加绘制于日夜齿轮上层)
        _attackModeIcon = new TextureRect
        {
            Name = "HudAttackModeIcon",
            Position = new Vector2(754, 553),
            MouseFilter = Control.MouseFilterEnum.Ignore,
            ZIndex = 1
        };
        AddChild(_attackModeIcon);
        SetAttackMode(0);

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
                MouseFilter = Control.MouseFilterEnum.Pass,
                ZIndex = 1
            };
            _expBar.MouseEntered += () =>
            {
                ShowSlotTooltip("经验值", $"[color=#5cd8ff]当前经验: {_expBar.Value} / {_expBar.MaxValue}[/color]\n[color=#c0c0c0]升级进度: {(_expBar.MaxValue > 0 ? (_expBar.Value * 100.0 / _expBar.MaxValue) : 0):F2}%[/color]", _expBar.GlobalPosition, showFloating: true);
            };
            _expBar.MouseExited += HideSlotTooltip;
            AddChild(_expBar);

            _lblExpValue = new Label
            {
                Name = "ExpValueLabel",
                Position = new Vector2(666, 526),
                Size = new Vector2(76, 14),
                Text = "0.00%",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                MouseFilter = Control.MouseFilterEnum.Ignore,
                ZIndex = 2,
                Visible = false // 盛大官方 1.5 原版底盘绝无常显粗体文字，仅悬停提示
            };
            AddChild(_lblExpValue);

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
                MouseFilter = Control.MouseFilterEnum.Pass,
                ZIndex = 1
            };
            _weightBar.MouseEntered += () =>
            {
                ShowSlotTooltip("背包负重", $"[color=#55ff77]当前负重: {_weightBar.Value} / {_weightBar.MaxValue}[/color]\n[color=#c0c0c0]负重超限时行动力将受阻[/color]", _weightBar.GlobalPosition, showFloating: true);
            };
            _weightBar.MouseExited += HideSlotTooltip;
            AddChild(_weightBar);

            _lblWeightValue = new Label
            {
                Name = "WeightValueLabel",
                Position = new Vector2(666, 559),
                Size = new Vector2(76, 14),
                Text = "0/15",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                MouseFilter = Control.MouseFilterEnum.Ignore,
                ZIndex = 2,
                Visible = false // 盛大官方 1.5 原版底盘绝无常显粗体文字，仅悬停提示
            };
            AddChild(_lblWeightValue);
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
                        // 单击选中格子 (对齐官方 1.5 IDA 0x48e118 TFrmDlg_DItemGridGridSelect，双击才吃药/穿戴)
                        btn.Pressed += () => _selectedBagSlot = slot;
                        btn.MouseEntered += () =>
                        {
                            if (btn.ItemTexture != null || btn.Icon != null)
                            {
                                var details = _bagItemDetails[slot];
                                string name = details?.Name ?? $"背包物品 #{slot + 1}";
                                string desc = details?.Desc ?? "[color=#c0c0c0]重量 1   持久力 10/10[/color]\n[color=#ffff00]攻击 0-1[/color]";

                                // 盛大官方 1.5 装备悬停对比：若按住 Shift 且身上有对应槽位穿戴，呼出 ShowComparisonTooltip
                                if (Input.IsKeyPressed(Key.Shift))
                                {
                                    int targetEquipSlot = GetBestEquipSlotForBagItem(name);
                                    if (targetEquipSlot >= 0 && targetEquipSlot < 9 && _equipItemDetails[targetEquipSlot] != null)
                                    {
                                        var curEquip = _equipItemDetails[targetEquipSlot]!.Value;
                                        ShowComparisonTooltip(curEquip.Name, curEquip.Desc, name, desc, btn.GlobalPosition);
                                        return;
                                    }
                                }
                                ShowSlotTooltip(name, desc, btn.GlobalPosition, showFloating: false, slotType: SlotButton.SlotType.Bag);
                            }
                        };
                        btn.MouseExited += () => HideSlotTooltip(SlotButton.SlotType.Bag);
                        btn.GuiInput += (ev) =>
                        {
                            // 官方 1.5 Ctrl+点击堆叠物品触发拆分输入模态框 (SplitDlg)
                            if (ev is InputEventMouseButton mb && mb.Pressed && mb.ButtonIndex == MouseButton.Left && mb.CtrlPressed)
                            {
                                var item = _bagItemDetails[slot];
                                if (item != null)
                                {
                                    int curCount = ExtractItemCountOrDura(item.Value.Desc ?? "");
                                    if (curCount > 1)
                                    {
                                        _messageBoxDialog?.ShowInput($"请输入拆分数量 (1 ~ {curCount - 1})：", "1", (input) =>
                                        {
                                            if (int.TryParse(input.Trim(), out var cnt) && cnt > 0)
                                            {
                                                SplitBagItem(slot, cnt);
                                            }
                                        });
                                    }
                                    else
                                    {
                                        AddChat("系统", $"[背包] [{item.Value.Name}] 数量不足，无法继续拆分。", new Color(1f, 0.4f, 0.4f));
                                    }
                                }
                            }

                            // 官方 1.5 原生微交互：右键点击背包物品直接嗑药或装备上身
                            if (ev is InputEventMouseButton rmb && rmb.Pressed && rmb.ButtonIndex == MouseButton.Right)
                            {
                                var item = _bagItemDetails[slot];
                                if (item != null)
                                {
                                    HandleBagSlotDoubleClick(slot);
                                }
                            }
                        };
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
                            string raw = (input ?? string.Empty).Trim();
                            if (string.IsNullOrEmpty(raw)) return;

                            // 全角数字字符规约为半角数字
                            char[] chars = raw.ToCharArray();
                            for (int i = 0; i < chars.Length; i++)
                            {
                                if (chars[i] >= '０' && chars[i] <= '９')
                                    chars[i] = (char)(chars[i] - '０' + '0');
                            }
                            string normalized = new string(chars);

                            if (int.TryParse(normalized, out var dropAmount))
                            {
                                if (dropAmount <= 0)
                                {
                                    AddChat("系统", "[系统] 丢弃金币数量必须大于 0。", new Color(1f, 0.4f, 0.4f));
                                    return;
                                }
                                int realDrop = Math.Min(dropAmount, _currentGold);
                                RequestDropGold(realDrop);
                            }
                            else
                            {
                                AddChat("系统", "[系统] 请输入合法的金币数量。", new Color(1f, 0.4f, 0.4f));
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
            FontManager.ApplyOfficialStyleToRichText(lblBagDesc, new Color(0.9f, 0.9f, 0.9f), 12);
            lblBagDesc.AddThemeConstantOverride("line_separation", 2);
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

            // 盛大官方 1.5 装备面板顶部拱门角色名字居中显示 (FrmDlg.pas 0x489EBE..0x489F00: Left + 122 - TextWidth(UserName)/2, Top + 23)
            _lblEquipCharName = new Label
            {
                Name = "LblEquipCharName",
                Position = new Vector2(36, 17), // 36 + 172/2 = 122，字形中心对齐 Top+23
                Size = new Vector2(172, 18),
                Text = _playerName,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                MouseFilter = Control.MouseFilterEnum.Ignore,
                ZIndex = 6
            };
            FontManager.ApplyOfficialStyle(_lblEquipCharName, Colors.White, 12, false);
            _equipPanel.AddChild(_lblEquipCharName);

            // 纸娃娃裸身底板 (0=男 Prguse_0376, 1=女 Prguse_0377, IDA sub_488AF4 [0x488c09..0x488c15]: Left+38, Top+52)
            _dollBackground = _equipPanel.GetNodeOrNull<TextureRect>("DollBackground");
            string dollPng = _playerGender != 0 ? "Prguse_0377.png" : "Prguse_0376.png";
            var texDoll = ResPath.LoadImageTexture($"resources/wil/png/Prguse/{dollPng}");
            if (_dollBackground != null && texDoll != null)
            {
                _dollBackground.Texture = texDoll;
                _dollBackground.Position = new Vector2(38, 52);
                _dollBackground.Size = new Vector2(168, 199);
            }

            // 盛大官方 1.5 角色纸娃娃核心立绘层 (挂在 _dollBackground 下，依据 StateItem.wil & Prguse.wil 原生 Origin 绝对对齐)
            // 0. 发型层: 官方实证：男基准发型 0441 @ (76, 45)；女基准发型 0482 @ (75, 42) (红棕盘发自然垂落耳际，严丝合缝露脸)
            string initialHair = _playerGender != 0 ? "Prguse_0482.png" : "Prguse_0441.png";
            var texHair = ResPath.LoadImageTexture($"resources/wil/png/Prguse/{initialHair}");
            if (_dollBackground != null)
            {
                _dollHair = new TextureRect
                {
                    Name = "DollHair",
                    Texture = texHair,
                    Position = _playerGender != 0 ? new Vector2(75, 42) : new Vector2(76, 45),
                    Size = texHair != null ? texHair.GetSize() : new Vector2(24, 16),
                    MouseFilter = Control.MouseFilterEnum.Ignore,
                    Visible = true,
                    ZIndex = 1
                };
                _dollBackground.AddChild(_dollHair);
            }

            // 1. 衣服立绘层: 盛大官方 1.5 正宗布衣 (男布衣 StateItem_0060 @ (41, 65); 女布衣 StateItem_0080 @ (42, 65))
            // 物理对齐：V领紧扣胸膛与锁骨，长袖完全盖住手腕，裙摆靴尖严密咬合，彻底杜绝多重残肢与恐怖谷效应
            string initialCloth = _playerGender != 0 ? "StateItem_0080.png" : "StateItem_0060.png";
            var texCloth = ResPath.LoadImageTexture($"resources/wil/png/StateItem/{initialCloth}");
            if (_dollBackground != null)
            {
                _dollCloth = new TextureRect
                {
                    Name = "DollCloth",
                    Texture = texCloth,
                    Position = _playerGender != 0 ? new Vector2(42, 65) : new Vector2(41, 65),
                    Size = texCloth != null ? texCloth.GetSize() : new Vector2(80, 128),
                    MouseFilter = Control.MouseFilterEnum.Ignore,
                    ZIndex = 2
                };
                _dollBackground.AddChild(_dollCloth);
            }

            // 2. 武器立绘层: 初始为木剑 StateItem_0030 (28x57 @ (25, 56)，剑柄 100% 紧扣右手手心)
            var texWeapon = ResPath.LoadImageTexture("resources/wil/png/StateItem/StateItem_0030.png");
            if (_dollBackground != null)
            {
                _dollWeapon = new TextureRect
                {
                    Name = "DollWeapon",
                    Texture = texWeapon,
                    Position = new Vector2(25, 56),
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
            FontManager.ApplyOfficialStyleToRichText(lblEquipDesc, new Color(0.9f, 0.9f, 0.9f), 12);
            lblEquipDesc.AddThemeConstantOverride("line_separation", 2);
            _lblEquipItemDesc = lblEquipDesc;
            _equipPanel.AddChild(lblEquipDesc);

            // 盛大官方 1.5 角色 9 大装备槽位 (IDA sub_481dd4 [0x483eb0..0x484180] & sub_48A9BC 官方实证):
            // 控件基于 EquipPanel (232x325) 原点 (0, 0) 的绝对局部坐标：
            // 0:衣服(0x39C), 1:武器(0x398), 2:照明/右持物(火把/蜡烛/护符/毒药/药粉)(0x384), 3:项链(0x380), 4:头盔(0x3A0)
            // 5:右手镯(0x38C), 6:左手镯(0x388), 7:右戒指(0x394), 8:左戒指/符(0x390)
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

            // 预设新手默认穿戴的衣服(0)与武器(1)官方属性描述，其余首饰槽位(2~8)初始为空，严禁硬编码假图标或假属性！
            _equipItemDetails[0] = ("布衣(女)", "[color=#c0c0c0]重量 5   持久力 5/5[/color]\n[color=#ffff00]防御 0-2   魔御 0-1[/color]\n[color=#c0c0c0]需要等级 1[/color]");
            _equipItemDetails[1] = ("木剑", "[color=#c0c0c0]重量 7   持久力 4/4[/color]\n[color=#ffff00]攻击 2-5[/color]\n[color=#c0c0c0]需要等级 1[/color]");

            foreach (var s in slotDefs)
            {
                int equipSlot = s.Slot;

                var btn = new SlotButton
                {
                    Name = $"EquipSlot_{equipSlot}",
                    CurrentSlotType = SlotButton.SlotType.Equip,
                    SlotIndex = equipSlot,
                    Position = new Vector2(s.X, s.Y),
                    Size = new Vector2(s.W, s.H),
                    CustomMinimumSize = new Vector2(s.W, s.H),
                    Flat = true,
                    ItemTexture = null
                };
                _equipSlots[equipSlot] = btn;
                btn.GuiInput += (ev) =>
                {
                    // 盛大官方 1.5 微交互：右键点击穿戴中装备直接脱下放回背包 (调用完整状态机 TakeoffEquipSlotToBag)
                    if (ev is InputEventMouseButton rmb && rmb.Pressed && rmb.ButtonIndex == MouseButton.Right)
                    {
                        TakeoffEquipSlotToBag(equipSlot, -1);
                    }
                };
                btn.MouseEntered += () =>
                {
                    var details = _equipItemDetails[equipSlot];
                    if (details != null && !string.IsNullOrEmpty(details.Value.Name))
                    {
                        ShowSlotTooltip(details.Value.Name, details.Value.Desc, btn.GlobalPosition, showFloating: false, slotType: SlotButton.SlotType.Equip);
                    }
                };
                btn.MouseExited += () => HideSlotTooltip(SlotButton.SlotType.Equip);
                _equipSlotsContainer?.AddChild(btn);
            }
        }

        _minimapView = GetNodeOrNull<MinimapView>("Minimap");
        _npcDialogView = GetNodeOrNull<NpcDialogView>("NpcDialog");
        if (_npcDialogView != null)
        {
            _npcDialogView.DialogClosed += () =>
            {
                _stackManager.PopWindow(_npcDialogView);
            };
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
            _stackManager.PopWindow(_tradePanelView);
            RollbackTradeItemsToBag();
            EmitSignal(SignalName.TradeCancelRequested);
            AddChat("系统", "[交易] 交易已取消，物品原路退回背包。", new Color(1f, 0.4f, 0.4f));
        };
        _tradePanelView.ChangeDealGoldRequested += (gold) =>
        {
            EmitSignal(SignalName.TradeChangeGoldRequested, gold);
            AddChat("系统", $"[交易] 修改出资交易金币为 {gold:N0}。", new Color(1f, 0.9f, 0.3f));
        };
        // 官方 1.5 交易面板点击修改金币 (IDA TFrmDlg_DDGoldClick 0x491fb0):
        _tradePanelView.DealGoldClicked += () =>
        {
            if (_currentGold <= 0)
            {
                AddChat("系统", "[交易] 背包中没有多余金币可投入交易。", new Color(1f, 0.4f, 0.4f));
                return;
            }
            if (_messageBoxDialog != null)
            {
                _messageBoxDialog.ShowInput($"请输入要交易的金币数量 (最多 {_currentGold:N0})：", "1000", (input) =>
                {
                    if (int.TryParse(input.Trim(), out var gold) && gold >= 0)
                    {
                        int realGold = Math.Min(gold, _currentGold);
                        // 交易金币上限 10,000,000 (sub_4DDC8C 0x989680 / trade.rs MAX_GOLD_CAP)
                        realGold = Math.Min(realGold, 10_000_000);
                        _tradePanelView.SetMyGold(realGold);
                        _tradePanelView.EmitSignal(TradePanelView.SignalName.ChangeDealGoldRequested, realGold);
                    }
                });
            }
            else
            {
                int defaultGold = Math.Min(1000, _currentGold);
                _tradePanelView.SetMyGold(defaultGold);
                _tradePanelView.EmitSignal(TradePanelView.SignalName.ChangeDealGoldRequested, defaultGold);
            }
        };
        _tradePanelView.AddDealItemRequested += (slot, name) =>
        {
            EmitSignal(SignalName.TradeAddItemRequested, slot, name);
        };
        _tradePanelView.DelDealItemRequested += (slot) =>
        {
            TakeTradeItemToBagSlot(slot, -1);
        };
        AddChild(_tradePanelView);

        _storagePanelView = new StoragePanelView();
        _storagePanelView.DepositRequested += (slot) =>
        {
            EmitSignal(SignalName.StorageDepositRequested, slot, "");
        };
        _storagePanelView.WithdrawRequested += (slot) =>
        {
            EmitSignal(SignalName.StorageWithdrawRequested, slot);
        };
        _storagePanelView.DepositGoldRequested += (amount) =>
        {
            EmitSignal(SignalName.StorageDepositGoldRequested, amount);
        };
        _storagePanelView.WithdrawGoldRequested += (amount) =>
        {
            EmitSignal(SignalName.StorageWithdrawGoldRequested, amount);
        };
        _storagePanelView.PanelClosed += () =>
        {
            if (_storagePanelView != null)
                _stackManager.PopWindow(_storagePanelView);
        };
        AddChild(_storagePanelView);

        _shopPanelView = new ShopPanelView();
        _shopPanelView.BuyRequested += (npcUid, itemIdx, count) =>
        {
            AddChat("商店", $"[交易] 正在向 NPC 购买第 {itemIdx + 1} 项商品，数量: {count}...", new Color(1f, 0.9f, 0.4f));
            GD.Print($"[HudController] 商店购买请求: npcUid={npcUid}, itemIdx={itemIdx}, count={count}");
        };
        _shopPanelView.TakeBackRequested += (storageSlot, itemName) =>
        {
            // 向外界派发官方取回仓库物品事件 (CM_USERSTORAGEWITHDRAW 1032)
            EmitSignal(SignalName.StorageWithdrawRequested, storageSlot);
            AddChat("系统", $"[系统] 正在申请从仓库取回保管物品 [{itemName}]...", new Color(1f, 1f, 0f));
            GD.Print($"[HudController] 仓库取回请求: slot={storageSlot}, item={itemName}");
        };
        _shopPanelView.PanelClosed += () =>
        {
            if (_shopPanelView != null)
                _stackManager.PopWindow(_shopPanelView);
            if (_shopPanelView?.CurrentMode == ShopPanelView.ShopMode.Storage)
            {
                if (_repairPanelView != null && _repairPanelView.Visible && _repairPanelView.IsStorageMode)
                {
                    _stackManager.PopWindow(_repairPanelView);
                }
                if (_bagPanel != null)
                {
                    _bagPanel.Position = _bagPosBeforeStorage;
                    _bagPanel.Visible = false;
                }
            }
        };
        AddChild(_shopPanelView);

        _repairPanelView = new RepairPanelView();
        _repairPanelView.RepairConfirmed += (bagSlot, isSpecial) =>
        {
            string modeStr = isSpecial ? "特殊修理" : "普通修理";
            AddChat("修理", $"[铁匠铺] 正在申请对背包第 {bagSlot + 1} 格装备进行{modeStr}...", new Color(0.4f, 1f, 0.5f));
            GD.Print($"[HudController] 装备修理请求: slot={bagSlot}, isSpecial={isSpecial}");
        };
        _repairPanelView.StorageDepositConfirmed += (bagSlot, itemName) =>
        {
            // 向外界派发官方存入仓库物品事件 (CM_USERSTORAGEDEPOSIT 1031)
            EmitSignal(SignalName.StorageDepositRequested, bagSlot, itemName);
            AddChat("系统", $"[系统] 正在向仓库申请寄存物品 [{itemName}]...", new Color(1f, 1f, 0f));
            GD.Print($"[HudController] 仓库寄存请求: slot={bagSlot}, item={itemName}");
        };
        _repairPanelView.PanelClosed += () =>
        {
            if (_repairPanelView != null)
                _stackManager.PopWindow(_repairPanelView);
            if (_repairPanelView?.IsStorageMode == true)
            {
                if (_shopPanelView != null && _shopPanelView.Visible && _shopPanelView.CurrentMode == ShopPanelView.ShopMode.Storage)
                {
                    _stackManager.PopWindow(_shopPanelView);
                }
                if (_bagPanel != null)
                {
                    _bagPanel.Position = _bagPosBeforeStorage;
                    _bagPanel.Visible = false;
                }
            }
        };
        AddChild(_repairPanelView);

        _inspectPanelView = new InspectPanelView();
        _inspectPanelView.PanelClosed += () =>
        {
            if (_inspectPanelView != null)
                _stackManager.PopWindow(_inspectPanelView);
        };
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
        _messageBoxDialog.Confirmed += () => _stackManager.PopWindow(_messageBoxDialog);
        _messageBoxDialog.Cancelled += () => _stackManager.PopWindow(_messageBoxDialog);
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

        // 官方 1.5 默认纯净初始化 1 级基础数值 (实际数据由服务端 SM_SENDUSERSTATE / SM_MYSTATUS 权威下发)
        UpdateExp(0, 100);
        UpdateWeight(0, 15);
        UpdateHp(19, 19);
        UpdateMp(11, 11);
        UpdateLevel(1);
        UpdateGold(0);

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
            // 聊天辅助按键 (IDA 0x4915cc TFrmDlg_DChatHelperClick)
            ("BtnChatNear", 219, 453, 28, 13, "Prguse_0130.png", "Prguse_0131.png", () => SetChatPrefix("")),
            ("BtnChatWhisper", 249, 453, 28, 13, "Prguse_0132.png", "Prguse_0133.png", () => SetChatPrefix("/")),
            ("BtnChatTeam", 309, 453, 28, 13, "Prguse_0128.png", "Prguse_0129.png", () => SetChatPrefix("!!")),
            ("BtnChatFilter", 530, 453, 28, 13, "Prguse_0136.png", "Prguse_0137.png", () => AddChat("系统", "[系统] 聊天过滤功能切换。", new Color(0.8f, 0.8f, 0.8f))),
            ("BtnChatHistory", 560, 453, 28, 13, "Prguse_0138.png", "Prguse_0139.png", TriggerChatHistoryClick),
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

            // 盛大官方 1.5 快捷槽位纯黑不透明底垫 (彻底阻断大世界地面怪物与名字从 Prguse_0001 透明槽孔穿透视效)
            var slotBg = new ColorRect
            {
                Name = "SlotBg",
                Size = new Vector2(32f, 29f),
                Position = Vector2.Zero,
                Color = new Color(0f, 0f, 0f, 1.0f),
                MouseFilter = Control.MouseFilterEnum.Ignore
            };
            btn.AddChild(slotBg);

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

            // 盛大官方 1.5 DBottonBeltDirectPaint (IDA 0x48BE80) 物理真值：
            // 腰带快捷栏 6 格右下角 (13, 19) 固定绘制 Prguse.wil 31..36 艺术贴图序号 1..6，严禁绘制白色 GDI 药水堆叠数字！
            var hotkeyTexRect = new TextureRect
            {
                Name = "BeltKeyIdx",
                Position = new Vector2(13f, 19f),
                Texture = ResPath.LoadImageTexture($"resources/wil/png/Prguse/Prguse_{31 + i:D4}.png"),
                StretchMode = TextureRect.StretchModeEnum.Keep,
                MouseFilter = Control.MouseFilterEnum.Ignore
            };
            btn.AddChild(hotkeyTexRect);

            var countLbl = new Label
            {
                Name = "CountLbl",
                Position = new Vector2(10f, 13f),
                Size = new Vector2(20f, 14f),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Visible = false,
                MouseFilter = Control.MouseFilterEnum.Ignore
            };
            btn.AddChild(countLbl);

            _quickSlotButtons[i] = btn;
            _quickSlotIcons[i] = icon;
            _quickSlotCounts[i] = countLbl;
            parent.AddChild(btn);
        }

        // 默认预设 1~6 官方常见药水（严格对齐 Items.wil: 28 强效金创药, 29 强效魔法药, 16 太阳水）
        SetQuickSlot(0, "强效金创药", 6, ResPath.LoadImageTexture($"resources/wil/png/Items/{ItemIconMapper.GetIconFile("强效金创药")}"));
        SetQuickSlot(1, "强效魔法药", 6, ResPath.LoadImageTexture($"resources/wil/png/Items/{ItemIconMapper.GetIconFile("强效魔法药")}"));
        SetQuickSlot(2, "太阳水", 4, ResPath.LoadImageTexture($"resources/wil/png/Items/{ItemIconMapper.GetIconFile("太阳水")}"));
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
            _quickSlotCounts[slot].Text = "";
            _quickSlotCounts[slot].Visible = false;
        }
    }

    public int GetQuickSlotCount(int slot) => (slot >= 0 && slot < 6) ? _quickSlotAmounts[slot] : 0;
    public string GetQuickSlotItem(int slot) => (slot >= 0 && slot < 6) ? _quickSlotItems[slot] : string.Empty;

    public void UseQuickSlot(int slot)
    {
        if (slot < 0 || slot >= 6) return;
        if (_quickSlotAmounts[slot] <= 0 || string.IsNullOrEmpty(_quickSlotItems[slot])) return;

        long nowTick = System.Environment.TickCount64;
        if (nowTick - _lastEatTick < 200) return; // 盛大官方 1.5 200ms 吃药冷却节流防连点 (IDA sub_48C188)
        _lastEatTick = nowTick;

        if (IsDead)
        {
            AddChat("系统", "[系统] 角色已经死亡，无法使用物品。", new Color(1f, 0.4f, 0.4f));
            return;
        }
        if (IsStunned || IsFrozen)
        {
            AddChat("系统", "[系统] 当前处于眩晕/麻痹硬直状态，无法使用物品。", new Color(1f, 0.4f, 0.4f));
            return;
        }

        string itemName = _quickSlotItems[slot];
        _lastUsedQuickSlot = slot;

        // 播放经典吃药音效 (药水吞咽音: 官方 sound 107, 对齐 Delphi SoundUtil.pas:s_eat_drug)
        AudioManager.Instance?.PlaySoundId(AudioManager.SoundId.EatDrug);

        GD.Print($"[HudController] 使用快捷药水栏 {slot + 1} 物品: {itemName}, 剩余数量: {_quickSlotAmounts[slot]}");
        QuickItemUsed?.Invoke(slot, itemName);

        // 盛大官方 1.5 权威架构铁律：联网模式下物品消耗与扣减由服务端权威驱动 (SM_DELITEM / SM_EAT_OK)
        // 客户端本地仅在离线沙盒模式下模拟扣减与自动补给
#if MIR2_SANDBOX_DEV
        if (IsSandboxMode)
        {
            ApplyQuickSlotConsume(slot);
        }
#endif
    }

    /// <summary>
    /// 执行快捷栏槽位扣减与自动从背包补充药水
    /// </summary>
    public void ApplyQuickSlotConsume(int slot)
    {
        if (slot < 0 || slot >= 6) return;
        if (_quickSlotAmounts[slot] <= 0 || string.IsNullOrEmpty(_quickSlotItems[slot])) return;

        string itemName = _quickSlotItems[slot];
        _quickSlotAmounts[slot]--;
        if (_quickSlotAmounts[slot] <= 0)
        {
            // 盛大官方 1.5 原版人性化药水补给机制:
            // 当快捷栏某格药水耗尽时，优先检索背包中同名消耗品自动补充至该快捷格
            int refillBagSlot = FindBagItemByName(itemName);
            if (refillBagSlot >= 0)
            {
                var refillTex = _bagSlots[refillBagSlot]?.ItemTexture;
                ClearBagItem(refillBagSlot);
                _quickSlotItems[slot] = itemName;
                _quickSlotAmounts[slot] = 1;
                if (_quickSlotIcons[slot] != null)
                {
                    _quickSlotIcons[slot].Texture = refillTex;
                    _quickSlotIcons[slot].Visible = refillTex != null;
                }
                if (_quickSlotCounts[slot] != null)
                {
                    _quickSlotCounts[slot].Text = "";
                    _quickSlotCounts[slot].Visible = false;
                }
                AddChat("包裹", $"[包裹] 从背包自动补充 1 瓶 [{itemName}] 至快捷栏 #{slot + 1} 格。", new Color(0.7f, 0.95f, 0.7f));
            }
            else
            {
                _quickSlotItems[slot] = "";
                if (_quickSlotIcons[slot] != null) _quickSlotIcons[slot].Texture = null;
                if (_quickSlotCounts[slot] != null) _quickSlotCounts[slot].Text = "";
            }
        }
        else
        {
            if (_quickSlotCounts[slot] != null)
                _quickSlotCounts[slot].Text = _quickSlotAmounts[slot] > 1 ? _quickSlotAmounts[slot].ToString() : "";
        }
    }

    /// <summary>
    /// 服务端 SM_EAT_OK (606) 权威确认服药成功，驱动快捷栏扣减
    /// </summary>
    public void OnServerEatOk()
    {
        if (_lastUsedQuickSlot >= 0 && _lastUsedQuickSlot < 6)
        {
            int slot = _lastUsedQuickSlot;
            _lastUsedQuickSlot = -1;
            ApplyQuickSlotConsume(slot);
        }
    }

    public void UpdateHp(int hp, int maxHp)
    {
        _currentHp = hp;
        _maxHp = maxHp;
        if (_lblHp != null) _lblHp.Text = $"{hp}/{maxHp}";
        _targetHpRatio = maxHp > 0 ? Mathf.Clamp((float)hp / maxHp, 0f, 1f) : 0f;
        // 若血量瞬间清零或初始化，立即对齐显示比率，无需缓动爬升
        if (hp <= 0 || maxHp <= 0 || _displayHpRatio < 0)
        {
            _displayHpRatio = _targetHpRatio;
            _orbShaderMat?.SetShaderParameter("hp_ratio", _displayHpRatio);
        }
        else if (_orbShaderMat == null)
        {
            _displayHpRatio = _targetHpRatio;
        }
    }

    /// <summary>
    /// 盛大官方 1.5 即时治愈血量增量驱动 (SM_INSTANCEHEALGAUGE 659)
    /// </summary>
    public void ApplyHeal(int amount)
    {
        if (amount <= 0 || IsDead) return;
        int newHp = Math.Min(_currentHp + amount, _maxHp);
        UpdateHp(newHp, _maxHp);
    }

    private int _currentMp = 0;
    private int _maxMpInternal = 0;

    public void SetHasLearnedMpSkill(bool learned)
    {
        _hasLearnedMpSkill = learned;
        UpdateMp(_currentMp, _maxMpInternal);
    }

    public void UpdateMp(int mp, int maxMp)
    {
        _currentMp = mp;
        _maxMpInternal = maxMp;
        if (_lblMp != null) _lblMp.Text = $"{mp}/{maxMp}";
        
        // 盛大官方 1.5 铁律：战士在未学会耗蓝技能（如半月弯刀）之前，魔球右侧保持空水状态（无蓝）
        if (_playerJob == 0 && !_hasLearnedMpSkill)
        {
            _targetMpRatio = 0f;
            _displayMpRatio = 0f;
            _orbShaderMat?.SetShaderParameter("mp_ratio", 0.0f);
            return;
        }

        _targetMpRatio = maxMp > 0 ? Mathf.Clamp((float)mp / maxMp, 0f, 1f) : 0f;
        if (mp <= 0 || maxMp <= 0 || _displayMpRatio < 0)
        {
            _displayMpRatio = _targetMpRatio;
            _orbShaderMat?.SetShaderParameter("mp_ratio", _displayMpRatio);
        }
        else if (_orbShaderMat == null)
        {
            _displayMpRatio = _targetMpRatio;
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
            _expBar.Modulate = new Color(0.35f, 0.9f, 1.0f); // 官方经典青蓝色经验条
        }
        string expPercentStr = maxExp > 0 ? $"{100.0 * curExp / maxExp:F2}%" : "0.00%";
        if (_lblExpValue != null)
        {
            _lblExpValue.Text = expPercentStr;
        }
        // 官方能力页第 0 行 (IDA sub_488AF4 0x48935e: 100*[+48h]/[+4Ch],保留 2 位小数并追加 "%")
        _statusPanelView?.UpdateAbilityValue(0, expPercentStr);
    }

    public void UpdateWeight(int curWeight, int maxWeight)
    {
        if (_weightBar != null)
        {
            _weightBar.MaxValue = maxWeight > 0 ? maxWeight : 1;
            _weightBar.Value = Math.Clamp(curWeight, 0, (int)_weightBar.MaxValue);
            // 盛大官方 1.5 规则：负重超限时呈鲜红警示色，未超限时为翠绿色
            _weightBar.Modulate = curWeight > maxWeight ? new Color(1f, 0.25f, 0.25f) : new Color(0.25f, 0.95f, 0.35f);
        }
        if (_lblWeightValue != null)
        {
            _lblWeightValue.Text = $"{curWeight}/{maxWeight}";
            _lblWeightValue.AddThemeColorOverride("font_color", curWeight > maxWeight ? new Color(1f, 0.3f, 0.3f) : new Color(1f, 1f, 1f));
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

    private int _playerLevel = 1;
    private int _playerJob = 0; // 0: 战, 1: 法, 2: 道
    private int _dcMax = 35;
    private int _mcMax = 20;
    private int _scMax = 20;

    public void UpdateAbilities(int dcMax, int mcMax, int scMax)
    {
        _dcMax = dcMax;
        _mcMax = mcMax;
        _scMax = scMax;
    }

    public void SetPlayerJobAndLevel(int job, int level)
    {
        _playerJob = job;
        _playerLevel = Math.Max(1, level);
        UpdateLevel(_playerLevel);
        if (_playerJob == 0 && !_hasLearnedMpSkill)
        {
            _targetMpRatio = 0f;
            _displayMpRatio = 0f;
            _orbShaderMat?.SetShaderParameter("mp_ratio", 0.0f);
        }
        RefreshHpMpOrbDisplay();
    }

    public int GetCurrentWearWeight()
    {
        int total = 0;
        for (int i = 0; i < 9; i++)
        {
            if (i == 1) continue; // 武器算腕力，不算防具负重
            if (_equipItemDetails[i] != null)
            {
                var item = _equipItemDetails[i]!.Value;
                total += WeightConstraintEngine.ExtractItemWeight(item.Name, item.Desc);
            }
        }
        return total;
    }

    public int GetTalismanOrPoisonDura(int requiredDura)
    {
        // 优先检查 Slot 2 (官方照明/护身符/毒药包槽位 Lighting = 2)
        if (_equipItemDetails[2] != null)
        {
            var item = _equipItemDetails[2]!.Value;
            int dura = ExtractItemCountOrDura(item.Desc);
            if (dura >= requiredDura) return dura;
        }
        // 其次检查背包中是否有护身符或药粉
        for (int i = 0; i < 40; i++)
        {
            if (_bagItemDetails[i] != null)
            {
                var item = _bagItemDetails[i]!.Value;
                if (item.Name.Contains("护身符") || item.Name.Contains("毒粉") || item.Name.Contains("药粉"))
                {
                    int dura = ExtractItemCountOrDura(item.Desc);
                    if (dura >= requiredDura) return dura;
                }
            }
        }
        return 0;
    }

    public void UpdateLevel(int level)
    {
        _playerLevel = level;
        if (_lblLevel != null)
        {
            _lblLevel.Text = $"{level}";
            _lblLevel.Visible = false; // 隐藏普通字体，官方使用贴图艺术字
        }
        UpdateLevelTextureDisplay(level);
        RefreshHpMpOrbDisplay();
    }

    /// <summary>
    /// 盛大官方 1.5 原版等级艺术字贴图数字渲染 (IDA 0x48b749 @ sub_48B2A0 & 0x45ec70)
    /// 官方使用 Prguse.wil 切片 30~39 (Prguse_0030..0039)，每个字符宽 8 像素，起始于 (660, 496)
    /// </summary>
    public void UpdateLevelTextureDisplay(int level)
    {
        if (_levelNumberContainer == null) return;
        foreach (Node child in _levelNumberContainer.GetChildren())
        {
            child.QueueFree();
        }

        string s = level.ToString();
        for (int i = 0; i < s.Length; i++)
        {
            char c = s[i];
            int imgIndex = (c >= '0' && c <= '9') ? (30 + (c - '0')) : 40;
            var tex = ResPath.LoadImageTexture($"resources/wil/png/Prguse/Prguse_{imgIndex:D4}.png");
            if (tex != null)
            {
                var tr = new TextureRect
                {
                    Texture = tex,
                    Position = new Vector2(i * 8, 0),
                    Size = new Vector2(12, 10),
                    MouseFilter = Control.MouseFilterEnum.Ignore,
                    ZIndex = 2
                };
                _levelNumberContainer.AddChild(tr);
            }
        }
    }

    /// <summary>
    /// 盛大官方 1.5 原版血球魔球物理真值刷新 (IDA 0x48b4b4..0x48b721 @ sub_48B2A0)
    /// 铁律实证：
    /// 1. 战士且等级 < 28 级 (0x1c)：
    ///    底槽加载 Prguse_0005 (96x92 @ 38, 439)，血液加载 Prguse_0006 (96x92 纯红满血单球 @ 38, 439)
    ///    启用 is_single_hp_ball = true，无蓝条、无中线分割、无半边缺失。
    /// 2. 法师/道士或战士等级 >= 28 级：
    ///    血液/魔法加载 Prguse_0004 (92x90 红蓝双球 @ 40, 440)
    ///    启用 is_single_hp_ball = false，左红右蓝。
    /// </summary>
    public void RefreshHpMpOrbDisplay()
    {
        bool isSingleHp = (_playerJob == 0 && _playerLevel < 28);
        if (_orbShaderMat != null)
        {
            _orbShaderMat.SetShaderParameter("is_single_hp_ball", isSingleHp);
        }

        if (isSingleHp)
        {
            var singleBgTex = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0005.png");
            if (_orbBgTextureRect != null && singleBgTex != null)
            {
                _orbBgTextureRect.Texture = singleBgTex;
                _orbBgTextureRect.Position = new Vector2(38, 439);
                _orbBgTextureRect.Size = new Vector2(96, 92);
            }

            var singleFillTex = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0006.png");
            if (_orbTextureRect != null && singleFillTex != null)
            {
                _orbTextureRect.Texture = singleFillTex;
                _orbTextureRect.Position = new Vector2(38, 439);
                _orbTextureRect.Size = new Vector2(96, 92);
            }
        }
        else
        {
            var doubleFillTex = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0004.png");
            if (_orbTextureRect != null && doubleFillTex != null)
            {
                _orbTextureRect.Texture = doubleFillTex;
                _orbTextureRect.Position = new Vector2(40, 440);
                _orbTextureRect.Size = new Vector2(92, 90);
            }
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
        _lampDura = Math.Max(0, dura);
        GD.Print($"[HudController] 蜡烛/油灯持久更新 (SM_LAMPCHANGEDURA=655): {_lampDura}");

        // 盛大官方 1.5 蜡烛火把持久度同步与燃尽熄灭 (IDA case 655 & sub_48A0F0 DSWLightDirectPaint)
        if (_equipItemDetails[2] is { } eq && (eq.Name.Contains("火把") || eq.Name.Contains("蜡烛")))
        {
            string newDesc = $"重量 1   持久力 {_lampDura}/10";
            _equipItemDetails[2] = (eq.Name, newDesc);
            UpdateEquipDurability(2, _lampDura, 10);

            if (_lampDura <= 0)
            {
                AddChat("系统", "[提示] 你的火把已经燃尽熄灭了。", new Color(0.9f, 0.7f, 0.3f));
                UpdateVisionFog(true, Mir2Client.Game.DarknessFogManager.BaseDarkRadius, new Vector2(400, 225));
            }
        }
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

    /// <summary>盛大官方 1.5 脱下装备失败与手持状态原子回滚 (SM_TAKEOFF_FAIL 620, IDA sub_4687C8)</summary>
    public void OnTakeOffFail(int slot)
    {
        int targetSlot = slot >= 0 && slot < 9 ? slot : -1;
        if (targetSlot == -1 && _pendingEquipActions.Count > 0)
        {
            foreach (var kvp in _pendingEquipActions)
            {
                if (kvp.Value.Type == PendingEquipAction.ActionType.TakeOff)
                {
                    targetSlot = kvp.Key;
                    break;
                }
            }
        }

        if (targetSlot >= 0 && _pendingEquipActions.TryGetValue(targetSlot, out var act) && act.Type == PendingEquipAction.ActionType.TakeOff)
        {
            // 恢复装备回身上
            _equipItemDetails[act.EquipSlot] = act.OriginalEquipItem;
            var btnEquip = _equipSlots[act.EquipSlot];
            if (btnEquip != null) btnEquip.ItemTexture = act.OriginalEquipTex;

            // 清空临时放入背包的槽位
            if (act.BagSlot >= 0 && act.BagSlot < 40)
            {
                _bagItemDetails[act.BagSlot] = null;
                var btnBag = _bagSlots[act.BagSlot];
                if (btnBag != null) btnBag.ItemTexture = null;
            }

            // 恢复纸娃娃立绘图层
            if (act.EquipSlot == 0) _dollCloth?.SetVisible(true);
            else if (act.EquipSlot == 1) _dollWeapon?.SetVisible(true);
            else if (act.EquipSlot == 4) _dollHelm?.SetVisible(true);

            _pendingEquipActions.Remove(targetSlot);
            AddChat("装备", $"[装备] 脱下装备失败 (槽位: {act.EquipSlot})，已原子回滚至原位。", new Color(1f, 0.4f, 0.4f));
        }
        else
        {
            AddChat("装备", $"[装备] 脱下装备失败 (槽位: {slot})，已安全复位。", new Color(1f, 0.4f, 0.4f));
        }
    }

    /// <summary>盛大官方 1.5 穿戴装备失败与手持状态原子回滚 (SM_TAKEON_FAIL 616, IDA sub_49F874)</summary>
    public void OnTakeOnFail(int slot)
    {
        int targetSlot = slot >= 0 && slot < 9 ? slot : -1;
        if (targetSlot == -1 && _pendingEquipActions.Count > 0)
        {
            foreach (var kvp in _pendingEquipActions)
            {
                if (kvp.Value.Type == PendingEquipAction.ActionType.Wear)
                {
                    targetSlot = kvp.Key;
                    break;
                }
            }
        }

        if (targetSlot >= 0 && _pendingEquipActions.TryGetValue(targetSlot, out var act) && act.Type == PendingEquipAction.ActionType.Wear)
        {
            // 回滚装备栏为原来的装备
            _equipItemDetails[act.EquipSlot] = act.OriginalEquipItem;
            var btnEquip = _equipSlots[act.EquipSlot];
            if (btnEquip != null) btnEquip.ItemTexture = act.OriginalEquipTex;

            // 回滚背包格为原来的物品
            if (act.BagSlot >= 0 && act.BagSlot < 40)
            {
                _bagItemDetails[act.BagSlot] = act.OriginalBagItem;
                var btnBag = _bagSlots[act.BagSlot];
                if (btnBag != null) btnBag.ItemTexture = act.OriginalBagTex;
            }

            // 回滚纸娃娃立绘图层
            if (act.EquipSlot == 0) _dollCloth?.SetVisible(act.OriginalEquipItem != null);
            else if (act.EquipSlot == 1) _dollWeapon?.SetVisible(act.OriginalEquipItem != null);
            else if (act.EquipSlot == 4) _dollHelm?.SetVisible(act.OriginalEquipItem != null);

            _pendingEquipActions.Remove(targetSlot);
            AddChat("装备", $"[装备] 穿戴装备失败 (槽位: {act.EquipSlot})，已解除锁定并原子归还原位。", new Color(1f, 0.4f, 0.4f));
        }
        else
        {
            string slotDesc = slot >= 0 ? $" (槽位: {slot})" : "";
            AddChat("装备", $"[装备] 穿戴装备失败{slotDesc}，已解除锁定并原位归还。", new Color(1f, 0.4f, 0.4f));
        }
    }

    /// <summary>盛大官方 1.5 小地图雷达标记 (SM_MAPMARK_DELPHI 612)</summary>
    public void SetMapMarker(int x, int y)
    {
        if (x > 0 && y > 0)
        {
            _minimapView?.SetTargetMarker(x, y);
            AddChat("系统", $"[雷达] 目标标记更新至坐标 ({x}, {y})。", new Color(0.4f, 1f, 0.8f));
        }
        else
        {
            _minimapView?.ClearTarget();
            AddChat("系统", "[雷达] 目标标记已清除。", new Color(0.8f, 0.8f, 0.8f));
        }
    }

    /// <summary>盛大官方 1.5 武器损毁提示 (SM_BREAK_WEAPON 1102)</summary>
    public void OnBreakWeapon()
    {
        AddChat("装备", "[装备] 你的武器在试刀或激烈战斗中损毁破裂！", new Color(1f, 0.2f, 0.2f));
    }

    /// <summary>盛大官方 1.5 战斗消息广播 (SM_COMBAT_BROADCAST 634)</summary>
    public void OnCombatBroadcast(string msg)
    {
        AddChat("战斗", msg, new Color(1f, 0.6f, 0.3f));
    }

    /// <summary>
    /// 盛大官方 1.5 战斗姿态解除 (SM_COMBAT_END 635, IDA case 635 @ 0x469B80: byte_4F9410 = 0)
    /// </summary>
    public void OnCombatEnd()
    {
        AddChat("战斗", "[战斗] 战斗状态解除，恢复常态。", new Color(0.6f, 0.8f, 1f));
    }

    public void UpdateName(string name)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            _playerName = name;
        }
        if (_lblName != null) _lblName.Text = _playerName;
        if (_lblEquipCharName != null) _lblEquipCharName.Text = _playerName;
    }

    public void UpdateMapAndCoords(string mapTitle, int x, int y)
    {
        if (!string.IsNullOrWhiteSpace(mapTitle))
        {
            _currentMapTitle = mapTitle;
        }
        if (_lblMapCoord != null)
        {
            _lblMapCoord.Text = $"{_currentMapTitle} {x}:{y}";
        }
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
            string prefix = (!string.IsNullOrEmpty(channel) && !msg.StartsWith($"[{channel}]") && !msg.StartsWith($"【{channel}】")) ? $"[{channel}] " : "";
            _chatLog.AppendText($"[color=#{colorHex}]{prefix}{msg}[/color]\n");
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
            if (_tradePanelView.Visible)
            {
                // 关闭交易面板：触发取消并退回交易栏物品
                _tradePanelView.CloseTrade();
                _stackManager.PopWindow(_tradePanelView);
            }
            else
            {
                _stackManager.PushWindow(_tradePanelView);
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

    /// <summary>官方 1.5 观人面板 (IDA TFrmDlg_DUserState1DirectPaint 0x49219c, DCloseUS1Click 0x492ae8)</summary>
    public void OpenInspect(string targetName)
    {
        if (_inspectPanelView != null)
        {
            _inspectPanelView.Inspect(targetName);
            if (!_inspectPanelView.Visible)
            {
                _inspectPanelView.Visible = true;
            }
            _stackManager.PushWindow(_inspectPanelView);
            GD.Print($"[HudController] 打开观人面板: {targetName}");
        }
    }

    public void CloseInspect()
    {
        if (_inspectPanelView != null && _inspectPanelView.Visible)
        {
            _inspectPanelView.Visible = false;
            _stackManager.PopWindow(_inspectPanelView);
            GD.Print("[HudController] 关闭观人面板");
        }
    }

    public void ToggleInspect()
    {
        if (_inspectPanelView != null)
        {
            _stackManager.ToggleWindow(_inspectPanelView);
            GD.Print($"[HudController] 切换观人面板显示状态: {_inspectPanelView.Visible}");
        }
    }

    /// <summary>官方 1.5 底栏小地图按键切换 (IDA TFrmDlg_DBotMiniMapClick 0x490c80)</summary>
    public void ToggleMinimap()
    {
        _minimapView?.CycleMode();
        EmitSignal(SignalName.MinimapToggled);
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
        if (_lblMapCoord != null)
        {
            string expected = $"{_currentMapTitle} {playerTileX}:{playerTileY}";
            if (_lblMapCoord.Text != expected)
            {
                _lblMapCoord.Text = expected;
            }
        }

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

    public void OpenStorageList(IEnumerable<ShopItemEntry> items, int npcUid = 0)
    {
        if (_shopPanelView != null)
        {
            _shopPanelView.OpenForStorage(items, npcUid);
            _stackManager.PushWindow(_shopPanelView);
        }
        if (_repairPanelView != null && !_repairPanelView.Visible)
        {
            _repairPanelView.SetStorageMode(true);
            _stackManager.PushWindow(_repairPanelView);
        }
        if (_bagPanel != null)
        {
            if (!_bagPanel.Visible)
            {
                _bagPosBeforeStorage = _bagPanel.Position;
            }
            _bagPanel.Position = new Vector2(475, 0);
            _bagPanel.Visible = true;
        }
    }

    /// <summary>
    /// 官方 1.5 仓库数据驱动开启与列表刷新 (SM_USERSTORAGE_ITEMS 704)
    /// </summary>
    public void OpenStorageList(IEnumerable<StorageItemInfo> items, int npcUid = 0)
    {
        if (_shopPanelView != null)
        {
            _shopPanelView.OpenForStorage(items, npcUid);
            _stackManager.PushWindow(_shopPanelView);
        }
        if (_repairPanelView != null && !_repairPanelView.Visible)
        {
            _repairPanelView.SetStorageMode(true);
            _stackManager.PushWindow(_repairPanelView);
        }
        if (_bagPanel != null)
        {
            if (!_bagPanel.Visible)
            {
                _bagPosBeforeStorage = _bagPanel.Position;
            }
            _bagPanel.Position = new Vector2(475, 0);
            _bagPanel.Visible = true;
        }
    }

    /// <summary>
    /// 清空仓库物品列表 (SM_STORAGE_CLEARITEMS_DELPHI 709)
    /// </summary>
    public void ClearStorageList()
    {
        _shopPanelView?.ClearItems();
    }

    /// <summary>
    /// 官方 1.5 存入仓库成功权威确认 (SM_SAVEITEM_OK 701)
    /// </summary>
    public void OnServerStorageDepositOk(int bagSlot)
    {
        if (bagSlot >= 0 && bagSlot < 40)
        {
            ClearBagItem(bagSlot);
        }
        AddChat("保管箱", $"[保管箱] 物品寄存成功 (背包槽位:{bagSlot})。", new Color(0.5f, 1f, 0.5f));
    }

    /// <summary>
    /// 官方 1.5 取回仓库物品成功权威确认 (SM_TAKEBACKITEM_OK 705)
    /// </summary>
    public void OnServerStorageWithdrawOk(int storageSlot)
    {
        _shopPanelView?.RemoveItemAt(storageSlot);
        AddChat("保管箱", $"[保管箱] 物品取回成功 (仓库槽位:{storageSlot})。", new Color(0.5f, 1f, 0.5f));
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

    public void CloseRepairPanel()
    {
        if (_repairPanelView != null && _repairPanelView.Visible)
        {
            _stackManager.PopWindow(_repairPanelView);
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

        // 记录历史发言（最多保留 20 条，避免重复连续记录）
        if (_chatHistory.Count == 0 || _chatHistory[^1] != trimmed)
        {
            _chatHistory.Add(trimmed);
            if (_chatHistory.Count > 20) _chatHistory.RemoveAt(0);
        }
        _historyIndex = _chatHistory.Count;

        // 解析官方聊天频道指令 (必须按长前缀优先匹配，避免 ! 截断 !~ 与 !!)
        if (trimmed.StartsWith("!~"))
        {
            // 行会聊天 (Guild)
            EmitSignal(SignalName.ChatSent, trimmed);
            AddChat("行会", trimmed[2..], new Color(0.3f, 0.85f, 1.0f));
        }
        else if (trimmed.StartsWith("!!"))
        {
            // 组队聊天 (Group)
            EmitSignal(SignalName.ChatSent, trimmed);
            AddChat("组队", trimmed[2..], new Color(0.2f, 0.65f, 1.0f));
        }
        else if (trimmed.StartsWith("!"))
        {
            // 全服喊话 (Shout)
            EmitSignal(SignalName.ChatSent, trimmed);
            AddChat("喊话", trimmed[1..], new Color(1.0f, 0.95f, 0.4f));
        }
        else if (trimmed.StartsWith("/"))
        {
            // 私聊 (Whisper)
            EmitSignal(SignalName.ChatSent, trimmed);
            AddChat("私聊", trimmed[1..], new Color(0.4f, 1.0f, 1.0f));
        }
        else if (trimmed.StartsWith("@"))
        {
            // 系统命令 / GM 命令
            EmitSignal(SignalName.ChatSent, trimmed);
            AddChat("命令", trimmed, new Color(0.2f, 1.0f, 0.4f));
        }
        else
        {
            // 普通视野说话
            EmitSignal(SignalName.ChatSent, trimmed);
            AddChat("说话", trimmed, new Color(1.0f, 1.0f, 1.0f));
        }
    }

    private void SetChatPrefix(string prefix)
    {
        if (_chatInput == null) return;
        _chatInput.Text = prefix;
        _chatInput.GrabFocus();
        _chatInput.CaretColumn = _chatInput.Text.Length;
    }

    /// <summary>
    /// 盛大官方 1.5 聊天框快捷私聊: 双击玩家名字或点击角色名，自动填充 "/{targetName} " 并聚焦输入框 (IDA sub_4915CC)
    /// </summary>
    public void StartWhisperToPlayer(string playerName)
    {
        if (string.IsNullOrWhiteSpace(playerName)) return;
        SetChatPrefix($"/{playerName.Trim()} ");
    }

    private void TriggerChatHistoryClick()
    {
        if (_chatInput == null) return;
        _chatInput.GrabFocus();
        NavigateChatHistory(true);
    }

    public void NavigateChatHistory(bool up)
    {
        if (_chatHistory.Count == 0 || _chatInput == null) return;
        if (up)
        {
            if (_historyIndex > 0) _historyIndex--;
        }
        else
        {
            if (_historyIndex < _chatHistory.Count) _historyIndex++;
        }

        if (_historyIndex >= 0 && _historyIndex < _chatHistory.Count)
        {
            _chatInput.Text = _chatHistory[_historyIndex];
            _chatInput.CaretColumn = _chatInput.Text.Length;
        }
        else
        {
            _chatInput.Text = "";
        }
    }

    public void ShowSlotTooltip(string title, string desc, Vector2 globalPos, bool showFloating = true, SlotButton.SlotType slotType = SlotButton.SlotType.Bag)
    {
        // 盛大官方 1.5 规则：当装备持久力为 0 时，显示鲜红警示字 (IDA sub_488AF4 / DrawItemHint)
        string warningDesc = desc;
        if (warningDesc.Contains("持久力 0/"))
        {
            warningDesc = warningDesc.Replace("持久力 0/", "[color=#ff3333]持久力 0/[/color]");
        }

        string displayText = warningDesc.StartsWith(title, StringComparison.OrdinalIgnoreCase) ||
                             warningDesc.Contains($"[color=#ffffff]{title}[/color]") ||
                             warningDesc.Contains($"[color=#ffff40]{title}[/color]") ||
                             warningDesc.Contains(title)
            ? warningDesc
            : $"[color=#ffff40]{title}[/color]\n{warningDesc}";

        // 盛大官方 1.5 原版严密面板隔离铁律 (杜绝“纸娃娃显示药量”或“背包显示装备属性”乱窜):
        // 1. 只有在背包槽位悬停时，且背包开启时，更新背包底部的物品描述内胆
        if (slotType == SlotButton.SlotType.Bag)
        {
            if (_bagPanel != null && _bagPanel.Visible && _lblBagItemDesc != null)
            {
                _lblBagItemDesc.Text = displayText;
            }
        }
        // 2. 只有在角色装备槽位悬停时，且装备面板开启时，更新装备面板底部的属性内胆
        else if (slotType == SlotButton.SlotType.Equip)
        {
            if (_equipPanel != null && _equipPanel.Visible && _lblEquipItemDesc != null)
            {
                _lblEquipItemDesc.Text = displayText;
            }
        }

        // 3. 浮动 Tooltip
        if (showFloating)
        {
            _itemTooltipView?.ShowTooltip(title, warningDesc, globalPos);
        }
        else
        {
            _itemTooltipView?.HideTooltip();
        }
    }

    public void HideSlotTooltip() => HideSlotTooltip(null);

    public void HideSlotTooltip(SlotButton.SlotType? slotType)
    {
        _itemTooltipView?.HideTooltip();
        if (!slotType.HasValue || slotType == SlotButton.SlotType.Bag)
        {
            if (_lblBagItemDesc != null) _lblBagItemDesc.Text = "";
        }
        if (!slotType.HasValue || slotType == SlotButton.SlotType.Equip)
        {
            if (_lblEquipItemDesc != null) _lblEquipItemDesc.Text = "";
        }
    }

    public void ShowComparisonTooltip(string currentEquipName, string currentDesc, string newEquipName, string newDesc, Vector2 globalPos)
    {
        _itemTooltipView?.ShowComparisonTooltip(currentEquipName, currentDesc, newEquipName, newDesc, globalPos);
    }

    /// <summary>
    /// 盛大官方 1.5 聊天输入框激活与提交 (Enter / KpEnter 响应)
    /// </summary>
    public void FocusOrSubmitChat()
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
        }
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
                    FocusOrSubmitChat();
                    GetViewport().SetInputAsHandled();
                    break;
                }
                // 盛大官方 1.5 聊天输入历史记录上下键导航 (IDA TFrmMain_FormKeyDown)
                case Key.Up:
                {
                    if (_chatInput != null && _chatInput.HasFocus())
                    {
                        NavigateChatHistory(true);
                        GetViewport().SetInputAsHandled();
                    }
                    break;
                }
                case Key.Down:
                {
                    if (_chatInput != null && _chatInput.HasFocus())
                    {
                        NavigateChatHistory(false);
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
                case Key.X: // 官方 1.5 Alt+X 退出游戏确认 (sub_461DEC @ 0x461244)
                {
                    if (keyEvent.AltPressed)
                    {
                        RequestExitGame();
                        GetViewport().SetInputAsHandled();
                    }
                    break;
                }
                case Key.Q: // 官方 1.5 Alt+Q 小退换人确认 (sub_461DEC @ 0x461338)
                {
                    if (keyEvent.AltPressed)
                    {
                        RequestSoftClose();
                        GetViewport().SetInputAsHandled();
                    }
                    break;
                }
                // 盛大官方 1.5 主键盘数字键 1~6 快捷药水栏 (IDA TFrmMain_FormKeyDown 0x461DEC @ 0x46226A)
                case Key.Key1:
                case Key.Key2:
                case Key.Key3:
                case Key.Key4:
                case Key.Key5:
                case Key.Key6:
                {
                    if (!keyEvent.CtrlPressed && !keyEvent.AltPressed && !keyEvent.ShiftPressed &&
                        (_chatInput == null || !_chatInput.HasFocus()))
                    {
                        int slot = (int)keyEvent.Keycode - (int)Key.Key1;
                        UseQuickSlot(slot);
                        GetViewport().SetInputAsHandled();
                    }
                    break;
                }
                // 盛大官方 1.5 小键盘数字键 1~6 快捷药水栏兼容
                case Key.Kp1:
                case Key.Kp2:
                case Key.Kp3:
                case Key.Kp4:
                case Key.Kp5:
                case Key.Kp6:
                {
                    if (!keyEvent.CtrlPressed && !keyEvent.AltPressed && !keyEvent.ShiftPressed &&
                        (_chatInput == null || !_chatInput.HasFocus()))
                    {
                        int slot = (int)keyEvent.Keycode - (int)Key.Kp1;
                        UseQuickSlot(slot);
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
            if (closed == _tradePanelView)
            {
                _tradePanelView?.CloseTrade();
            }
            GD.Print($"[HudController] CloseTopDialog 关闭了: {closed.WindowName}, 剩余StackCount={_stackManager.Count}");
            return true;
        }

        if (_messageBoxDialog != null && _messageBoxDialog.Visible)
        {
            _messageBoxDialog.Visible = false;
            return true;
        }

        if (_tradePanelView != null && _tradePanelView.Visible)
        {
            CloseTrade();
            return true;
        }

        if (_npcDialogView != null && _npcDialogView.Visible)
        {
            CloseNpcDialog();
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
        // 1. 窗口栈管理器判定已打开的模态/可拖拽/可堆叠窗体
        if (_stackManager != null && _stackManager.IsMouseOverUi((int)mousePos.X, (int)mousePos.Y)) return true;

        // 2. 检查 HUD 自带面板 (背包、装备、状态、技能、加点、交易、修理、商店、NPC 对话框、小地图等)
        if (_bagPanel != null && _bagPanel.Visible && _bagPanel.GetGlobalRect().HasPoint(mousePos)) return true;
        if (_equipPanel != null && _equipPanel.Visible && _equipPanel.GetGlobalRect().HasPoint(mousePos)) return true;
        if (_skillPanelView != null && _skillPanelView.Visible && _skillPanelView.GetGlobalRect().HasPoint(mousePos)) return true;
        if (_statusPanelView != null && _statusPanelView.Visible && _statusPanelView.GetGlobalRect().HasPoint(mousePos)) return true;
        if (_adjustAbilPanelView != null && _adjustAbilPanelView.Visible && _adjustAbilPanelView.GetGlobalRect().HasPoint(mousePos)) return true;
        if (_groupPanelView != null && _groupPanelView.Visible && _groupPanelView.GetGlobalRect().HasPoint(mousePos)) return true;
        if (_guildPanelView != null && _guildPanelView.Visible && _guildPanelView.GetGlobalRect().HasPoint(mousePos)) return true;
        if (_tradePanelView != null && _tradePanelView.Visible && _tradePanelView.GetGlobalRect().HasPoint(mousePos)) return true;
        if (_repairPanelView != null && _repairPanelView.Visible && _repairPanelView.GetGlobalRect().HasPoint(mousePos)) return true;
        if (_shopPanelView != null && _shopPanelView.Visible && _shopPanelView.GetGlobalRect().HasPoint(mousePos)) return true;
        if (_npcDialogView != null && _npcDialogView.Visible && _npcDialogView.GetGlobalRect().HasPoint(mousePos)) return true;
        if (_inspectPanelView != null && _inspectPanelView.Visible && _inspectPanelView.GetGlobalRect().HasPoint(mousePos)) return true;
        if (_minimapView != null && _minimapView.Visible && _minimapView.GetGlobalRect().HasPoint(mousePos)) return true;
        if (_messageBoxDialog != null && _messageBoxDialog.Visible && _messageBoxDialog.GetGlobalRect().HasPoint(mousePos)) return true;

        // 3. 聊天输入框与聊天历史滚动区
        if (_chatInput != null && _chatInput.Visible && _chatInput.GetGlobalRect().HasPoint(mousePos)) return true;

        // 4. 盛大官方 1.5 原版底栏异形像素命中检测 (IDA 0x48ba74 TFrmDlg_DBottomInRealArea 权威物理真值)
        // 废除 mousePos.Y >= 349 的粗暴一刀切，仅在 Prguse_0001 非透明像素上判定为 UI，透明镂空处放行大世界点击
        int mx = (int)mousePos.X;
        int my = (int)mousePos.Y;
        if (mx >= 0 && mx < 800 && my >= 349 && my < 600)
        {
            if (_hudBottomBarAlphaMask != null)
            {
                int relX = mx;
                int relY = my - 349;
                if (relX >= 0 && relX < _hudBottomBarAlphaMask.GetLength(0) &&
                    relY >= 0 && relY < _hudBottomBarAlphaMask.GetLength(1))
                {
                    return _hudBottomBarAlphaMask[relX, relY];
                }
            }
            // 若掩码未就绪，使用官方 DFM DBottom (Left=12, Top=416) 保守判定，绝不切断 349~416 的大世界视野
            return my >= 416;
        }

        return false;
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
            if (string.IsNullOrEmpty(iconFile) || iconFile.StartsWith("StateItem"))
            {
                iconFile = ItemIconMapper.GetIconFile(itemName);
            }
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
        if (!string.IsNullOrEmpty(itemName) && !IsItemCompatibleWithEquipSlot(itemName, equipSlot)) return;
        _pendingEquipActions.Remove(equipSlot); // 确认成功，提交事务，解除互斥锁定
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
                // 盛大官方 1.5 铁律：衣服(0)、武器(1)、头盔(4) 穿戴在纸娃娃全身立绘上，槽位按钮上绝不可重复绘制独立贴图，否则会导致两重立绘严重错位！
                // 仅首饰与副手类（2:照明/药粉, 3:项链, 5:右手镯, 6:左手镯, 7:右戒指, 8:左戒指）才在槽位按钮上显示图标！
                bool isJewelryOrLight = (equipSlot != 0 && equipSlot != 1 && equipSlot != 4);
                btn.ItemTexture = (isJewelryOrLight && !string.IsNullOrEmpty(iconFile))
                    ? ResPath.LoadImageTexture($"resources/wil/png/StateItem/{iconFile}")
                    : null;
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
        }
        else
        {
            btn.LowDurabilityWarning = false;
        }
    }

    /// <summary>
    /// 盛大官方 1.5 角色性别纸娃娃底板切换 (0=男 Prguse_0376, 1=女 Prguse_0377, IDA sub_488AF4)
    /// </summary>
    public void SetDollGender(int gender)
    {
        _playerGender = gender;
        GD.Print($"[HudController] SetDollGender 被调用: gender={gender}, _dollBackground={(_dollBackground != null ? "有效" : "null")}");
        if (_dollBackground == null) return;
        string dollPng = gender != 0 ? "Prguse_0377.png" : "Prguse_0376.png";
        var texDoll = ResPath.LoadImageTexture($"resources/wil/png/Prguse/{dollPng}");
        if (texDoll != null)
        {
            _dollBackground.Texture = texDoll;
        }
        SetDollHair(1, gender != 0);

        // 如果当前穿戴了新手衣服，联动刷新衣服贴图 (盛大官方 1.5 正宗女布衣为 StateItem_0080 @ (42, 65), 男布衣为 StateItem_0060 @ (41, 65))
        if (_dollCloth != null)
        {
            string clothFile = gender != 0 ? "StateItem_0080.png" : "StateItem_0060.png";
            var texCloth = ResPath.LoadImageTexture($"resources/wil/png/StateItem/{clothFile}");
            if (texCloth != null)
            {
                _dollCloth.Texture = texCloth;
                _dollCloth.Size = texCloth.GetSize();
                _dollCloth.Position = gender != 0 ? new Vector2(42, 65) : new Vector2(41, 65);
                _dollCloth.Visible = true;
            }
        }
    }

    /// <summary>
    /// 盛大官方 1.5 角色发型更新接口 (官方 sub_488AF4: 男发基准440, 女发基准480)
    /// </summary>
    public void SetDollHair(int hairIndex = 1, bool isFemale = false)
    {
        if (_dollHair == null) return;
        string hairFile;
        Vector2 hairPos;
        if (isFemale || _playerGender != 0)
        {
            // 盛大官方 1.5 原版女性发型：Prguse_0482 为经典红棕发 (与大世界完全对齐)
            // 依据 Prguse.wil Origin 绝对物理偏移：(75, 42) 严丝合缝扣在女性头顶，完全露出五官与双眼
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
                string actualCloth = clothFile;
                if (_playerGender != 0)
                {
                    // 女性角色自适应：如果是布衣系列(0060/0061)，映射为盛大官方正品女布衣 StateItem_0080.png
                    if (actualCloth.Contains("0060") || actualCloth.Contains("0061"))
                    {
                        actualCloth = "StateItem_0080.png";
                    }
                }
                var tex = ResPath.LoadImageTexture($"resources/wil/png/StateItem/{actualCloth}");
                if (tex != null)
                {
                    _dollCloth.Texture = tex;
                    _dollCloth.Size = tex.GetSize();
                    _dollCloth.Position = _playerGender != 0 ? new Vector2(42, 65) : new Vector2(41, 65);
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
                    // 木剑握把在手心 (25, 56)，其他武器在 (10, 28)
                    _dollWeapon.Position = weaponFile.Contains("0030") ? new Vector2(25, 56) : new Vector2(10, 28);
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
            _stackManager.PushWindow(_npcDialogView);
        }
    }

    /// <summary>
    /// 打开并展示商店货架 (由 SM_MERCHANTDLG / SM_SENDGOODSLIST 驱动)
    /// </summary>
    public void ShowShop(string npcName, System.Collections.Generic.IEnumerable<ShopItemEntry> items)
    {
        if (_shopPanelView != null)
        {
            _shopPanelView.SetItems(items);
            _stackManager.PushWindow(_shopPanelView);
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

        // 4.1 从背包拖入商店面板出售 (Bag -> Shop)
        if (fromType == SlotButton.SlotType.Bag && toType == SlotButton.SlotType.Shop)
        {
            if (_shopPanelView != null && _shopPanelView.Visible)
            {
                var item = _bagItemDetails[fromSlot];
                if (item != null)
                {
                    _shopPanelView.RequestSellItem(fromSlot, item.Value.Name);
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
#if MIR2_SANDBOX_DEV
        if (IsSandboxMode)
        {
            ClearBagItem(bagSlot);
        }
#endif

        EmitSignal(SignalName.TradeAddItemRequested, bagSlot, bagItem.Value.Name);
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
#if MIR2_SANDBOX_DEV
        if (IsSandboxMode)
        {
            SetBagItem(bagSlot, itemName, "", origin.Value.Desc);
        }
#endif
        _tradeItemOrigins[tradeSlot] = null;
        _tradePanelView.ClearMyItem(tradeSlot);

        EmitSignal(SignalName.TradeDelItemRequested, tradeSlot);
        AddChat("系统", $"[交易] 取回交易物品 [{itemName}]，等待服务端确认...", new Color(0.8f, 0.8f, 1f));
    }

    /// <summary>
    /// 官方交易取消闭环 (IDA sub_467920 @ 0x46A5E3 与 sub_49FE38 实证: 遍历 10 格交易栏退回背包)
    /// 联网模式下背包物品完全由服务端权威 SM_ADDITEM / SM_DELITEM 恢复
    /// </summary>
    public void RollbackTradeItemsToBag()
    {
        if (_tradePanelView == null) return;
        for (int i = 0; i < 10; i++)
        {
            if (_tradeItemOrigins[i] != null)
            {
#if MIR2_SANDBOX_DEV
                if (IsSandboxMode)
                {
                    var origin = _tradeItemOrigins[i]!.Value;
                    int bagSlot = (_bagItemDetails[origin.OriginBagSlot] == null) ? origin.OriginBagSlot : FindFirstEmptyBagSlot();
                    if (bagSlot >= 0)
                    {
                        SetBagItem(bagSlot, origin.Name, "", origin.Desc);
                    }
                }
#endif
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
            _stackManager.PushWindow(_tradePanelView);
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
            _stackManager.PopWindow(_tradePanelView);
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
    /// 服务端对方锁定交易栏同步 (由 SM_DEALLOCKED 678 驱动, IDA case 678 sub_49F9E4)
    /// </summary>
    public void OnServerTradeRemoteLocked(bool locked)
    {
        if (_tradePanelView != null)
        {
            _tradePanelView.SetLocked(false, locked);
            AddChat("系统", "[交易] 对方锁定了交易栏，等待最终交割确认。", new Color(1f, 0.9f, 0.3f));
            AudioManager.Instance?.PlaySoundId(106);
        }
    }

    /// <summary>
    /// 服务端己方放入物品失败自动回退 (由 SM_DEALADDITEM_FAIL 676 驱动, IDA case 676 sub_49F874)
    /// </summary>
    public void OnServerTradeAddItemFail(int slot)
    {
        if (slot >= 0 && slot < 10 && _tradeItemOrigins[slot] != null)
        {
            TakeTradeItemToBagSlot(slot, -1);
            AddChat("系统", $"[交易] 放入物品失败，物品已自动原路退回背包。", new Color(1f, 0.4f, 0.4f));
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
    /// 0: 衣服, 1: 武器, 2: 照明/右持物(火把/蜡烛/护符/毒药/药粉), 3: 项链, 4: 头盔, 5/6: 手镯, 7/8: 戒指
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
            2 => itemName.Contains("火把") || itemName.Contains("蜡烛") ||
                 itemName.Contains("护身符") || itemName.Contains("药粉") || itemName.Contains("毒药"), // 官方 1.5 权威照明/右持物
            3 => itemName.Contains("项链") || itemName.Contains("铃铛"),
            4 => itemName.Contains("头盔") || itemName.Contains("头巾") || itemName.Contains("冠"),
            5 or 6 => itemName.Contains("手镯") || itemName.Contains("手套"),
            7 or 8 => itemName.Contains("戒指") || itemName.Contains("指环"),
            _ => false
        };
    }

    /// <summary>
    /// 盛大官方 1.5 装备拖拽合法性即时高亮反馈 (合法槽位淡金高亮，非法暗淡)
    /// </summary>
    public void HighlightCompatibleEquipSlots(string itemName)
    {
        for (int slot = 0; slot < 9; slot++)
        {
            if (_equipSlots[slot] != null)
            {
                bool isCompat = IsItemCompatibleWithEquipSlot(itemName, slot);
                _equipSlots[slot]!.Modulate = isCompat ? new Color(1.3f, 1.3f, 0.7f) : new Color(0.5f, 0.5f, 0.5f);
            }
        }
    }

    /// <summary>
    /// 清除装备槽位高亮反馈，复位原生纯白色
    /// </summary>
    public void ClearEquipSlotHighlights()
    {
        for (int slot = 0; slot < 9; slot++)
        {
            if (_equipSlots[slot] != null)
            {
                _equipSlots[slot]!.Modulate = Colors.White;
            }
        }
    }

    /// <summary>
    /// 背包穿戴装备到装备槽位
    /// </summary>
    public void WearBagItemToEquipSlot(int bagSlot, int equipSlot)
    {
        if (bagSlot < 0 || bagSlot >= 40 || equipSlot < 0 || equipSlot >= 9) return;
        if (IsDead)
        {
            AddChat("系统", "[系统] 角色已经死亡，无法更换装备。", new Color(1f, 0.4f, 0.4f));
            return;
        }
        var bagItem = _bagItemDetails[bagSlot];
        if (bagItem == null) return;

        if (!IsItemCompatibleWithEquipSlot(bagItem.Value.Name, equipSlot))
        {
            AddChat("系统", "[系统] 该物品无法装备到此位置。", new Color(1f, 0.3f, 0.3f));
            return;
        }

        // 盛大官方 1.5 腕力与穿戴负重物理检定 (IDA sub_4C50E9 / sub_4C5114)
        int itemWeight = WeightConstraintEngine.ExtractItemWeight(bagItem.Value.Name, bagItem.Value.Desc);
        int curWearWeight = GetCurrentWearWeight();
        if (equipSlot != 1 && _equipItemDetails[equipSlot] != null)
        {
            var oldItem = _equipItemDetails[equipSlot]!.Value;
            curWearWeight = Math.Max(0, curWearWeight - WeightConstraintEngine.ExtractItemWeight(oldItem.Name, oldItem.Desc));
        }

        if (!WeightConstraintEngine.CheckCanEquip(_playerLevel, _playerJob, equipSlot, itemWeight, curWearWeight, out string weightReason))
        {
            AddChat("系统", $"[负重拦截] {weightReason}", new Color(1f, 0.3f, 0.3f));
            return;
        }

        // 盛大官方 1.5 装备等级与三维属性门槛检定 (如屠龙35级、战神盔甲30级、裁决攻击30等)
        var (canWearAttr, reqReason) = OfficialClientRulesEngine.SpecialItemAndSetRules.CheckEquipRequirement(
            bagItem.Value.Name, _playerLevel, _dcMax, _mcMax, _scMax);
        if (!canWearAttr)
        {
            AddChat("系统", $"[穿戴拦截] 该装备无法佩戴: {reqReason}", new Color(1f, 0.3f, 0.3f));
            return;
        }

        // 防重入互斥锁校验: 若该装备槽位正在等待服务端回执且未超时 (2000ms)，阻止重复穿戴
        long now = (long)Time.GetTicksMsec();
        if (_pendingEquipActions.TryGetValue(equipSlot, out var pending) && now - pending.Timestamp < 2000)
        {
            AddChat("系统", "[系统] 正在等待服务端穿戴确认，请勿频繁操作。", new Color(1f, 0.6f, 0.2f));
            return;
        }

        var curEquip = _equipItemDetails[equipSlot];
        var btnBag = _bagSlots[bagSlot];
        var btnEquip = _equipSlots[equipSlot];

        var bagTex = btnBag?.ItemTexture;
        var equipTex = btnEquip?.ItemTexture;

        // 记录原子回滚快照
        _pendingEquipActions[equipSlot] = new PendingEquipAction
        {
            Type = PendingEquipAction.ActionType.Wear,
            BagSlot = bagSlot,
            EquipSlot = equipSlot,
            OriginalBagItem = bagItem,
            OriginalEquipItem = curEquip,
            OriginalBagTex = bagTex,
            OriginalEquipTex = equipTex,
            Timestamp = now
        };

        // 派发穿戴请求信号
        EmitSignal(SignalName.WearItemRequested, bagSlot, equipSlot);
        PlayEquipSlotSound(equipSlot);

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
        if (IsDead)
        {
            AddChat("系统", "[系统] 角色已经死亡，无法卸下装备。", new Color(1f, 0.4f, 0.4f));
            return;
        }
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

        // 防重入互斥锁校验: 若该装备槽位正在等待服务端脱装回执且未超时 (2000ms)，阻止重复脱装
        long now = (long)Time.GetTicksMsec();
        if (_pendingEquipActions.TryGetValue(equipSlot, out var pending) && now - pending.Timestamp < 2000)
        {
            AddChat("系统", "[系统] 正在等待服务端卸下确认，请勿频繁操作。", new Color(1f, 0.6f, 0.2f));
            return;
        }

        var btnEquip = _equipSlots[equipSlot];
        var btnBag = _bagSlots[destSlot];

        // 记录原子回滚快照
        _pendingEquipActions[equipSlot] = new PendingEquipAction
        {
            Type = PendingEquipAction.ActionType.TakeOff,
            BagSlot = destSlot,
            EquipSlot = equipSlot,
            OriginalBagItem = null,
            OriginalEquipItem = equipItem,
            OriginalBagTex = null,
            OriginalEquipTex = btnEquip?.ItemTexture,
            Timestamp = now
        };

        // 派发脱装请求信号
        EmitSignal(SignalName.TakeoffItemRequested, equipSlot);
        PlayEquipSlotSound(equipSlot);

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
    /// 将背包物品拖拽至大世界地面丢弃 (官方 1.5 IDA 0x46AFDC / 0x48E33C 装备防误丢二次确认保护)
    /// </summary>
    public void DropBagItemToGround(int bagSlot, Vector2 screenPos)
    {
        if (bagSlot < 0 || bagSlot >= 40) return;
        var item = _bagItemDetails[bagSlot];
        if (item == null) return;

        string itemName = item.Value.Name;

        // 官方 1.5 贵重装备防误丢二次确认 (IDA 0x46AFDC / 0x48E33C)
        if (!IsConsumableItem(itemName))
        {
            ShowModalMessage("丢弃提示", $"你真的要丢弃【{itemName}】吗？", hasCancel: true, onConfirm: () =>
            {
                ExecuteDropItem(bagSlot, itemName, screenPos);
            });
            return;
        }

        ExecuteDropItem(bagSlot, itemName, screenPos);
    }

    private void ExecuteDropItem(int bagSlot, string itemName, Vector2 screenPos)
    {
#if MIR2_SANDBOX_DEV
        if (IsSandboxMode)
        {
            // 离线沙盒单机模式回退
            ClearBagItem(bagSlot);
            AddChat("系统", $"[系统] 物品 [{itemName}] 已丢弃至地面。", new Color(1f, 0.8f, 0.4f));
        }
#endif
        // 派发信号上报服务端丢弃意图 (由服务端权威核验并下发 SM_DELITEM 物理清空)
        EmitSignal(SignalName.ItemDropToGroundRequested, bagSlot, itemName, screenPos);
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
            int bestEquipSlot = GetBestEquipSlotForBagItem(itemName);
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

    /// <summary>
    /// 盛大官方 1.5 智能成对槽位判定：如果左手镯/左戒指已有装备且右手镯/右戒指为空，优先穿戴至右侧空槽
    /// </summary>
    public int GetBestEquipSlotForBagItem(string itemName)
    {
        int baseSlot = GetBestEquipSlotForItem(itemName);
        if (baseSlot == 5) // 手镯
        {
            if (_equipSlots[5]?.HasItem == true && (_equipSlots[6] == null || !_equipSlots[6]!.HasItem))
                return 6;
        }
        else if (baseSlot == 7) // 戒指
        {
            if (_equipSlots[7]?.HasItem == true && (_equipSlots[8] == null || !_equipSlots[8]!.HasItem))
                return 8;
        }
        return baseSlot;
    }

    /// <summary>
    /// 在背包 40 格中检索指定名称的物品槽位 (未找到返回 -1)
    /// </summary>
    public int FindBagItemByName(string name)
    {
        if (string.IsNullOrEmpty(name)) return -1;
        for (int i = 0; i < 40; i++)
        {
            if (_bagItemDetails[i]?.Name == name)
                return i;
        }
        return -1;
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
        if (itemName.Contains("甲") || itemName.Contains("袍") || itemName.Contains("衣") || 
            itemName.Contains("布衣") || itemName.Contains("披风") || itemName.Contains("铠") || itemName.Contains("重盔")) return (int)OfficialEquipSlot.Dress; // 0: 衣服 (战神盔甲/恶魔长袍/幽灵战衣等)
        if (itemName.Contains("剑") || itemName.Contains("刀") || itemName.Contains("斧") || itemName.Contains("杖") || 
            itemName.Contains("棍") || itemName.Contains("匕首") || itemName.Contains("刃") || itemName.Contains("月") || 
            itemName.Contains("屠龙") || itemName.Contains("炼狱") || itemName.Contains("银蛇") || itemName.Contains("降魔") || 
            itemName.Contains("凝霜") || itemName.Contains("修罗") || itemName.Contains("破魂") || itemName.Contains("凌风")) return (int)OfficialEquipSlot.Weapon; // 1: 武器
        if (itemName.Contains("符") || itemName.Contains("毒") || itemName.Contains("粉") || itemName.Contains("火把") || itemName.Contains("蜡烛") || itemName.Contains("佩")) return (int)OfficialEquipSlot.Lighting; // 2: 照明/护身符/毒药/药粉
        if (itemName.Contains("项链") || itemName.Contains("链")) return (int)OfficialEquipSlot.Necklace; // 3: 项链
        if (itemName.Contains("头盔") || itemName.Contains("冠") || itemName.Contains("帽") || itemName.Contains("头巾")) return (int)OfficialEquipSlot.Helmet; // 4: 头盔
        if (itemName.Contains("手镯") || itemName.Contains("手套") || itemName.Contains("镯")) return (int)OfficialEquipSlot.ArmringL; // 5: 手镯 (默认左手镯)
        if (itemName.Contains("戒") || itemName.Contains("指")) return (int)OfficialEquipSlot.RingL; // 7: 戒指 (默认左戒指)
        return -1;
    }

    public void ConsumeBagItem(int bagSlot, string itemName)
    {
        if (bagSlot < 0 || bagSlot >= 40) return;

        long nowTick = System.Environment.TickCount64;
        if (nowTick - _lastEatTick < 200) return; // 盛大官方 1.5 200ms 吃药冷却节流防连点 (IDA sub_48E33C)
        _lastEatTick = nowTick;

        if (IsDead)
        {
            AddChat("系统", "[系统] 角色已经死亡，无法使用物品。", new Color(1f, 0.4f, 0.4f));
            return;
        }
        if (IsStunned || IsFrozen)
        {
            AddChat("系统", "[系统] 当前处于眩晕/麻痹硬直状态，无法使用物品。", new Color(1f, 0.4f, 0.4f));
            return;
        }

        // 播放经典吃药吞咽音效 (官方 sound 107, SoundId.EatDrug)
        AudioManager.Instance?.PlaySoundId(AudioManager.SoundId.EatDrug);
        
        // 派发信号供网络层发包 CM_EATITEM (1006)，携带完整物品名称对齐官方协议
        EmitSignal(SignalName.UseItemRequested, bagSlot, itemName);

        // 盛大官方 1.5 权威架构铁律：联网模式下背包物品扣减完全由服务端 SM_DELITEM 驱动
        // 客户端本地仅在离线沙盒模式下作为单机回退模拟清空
#if MIR2_SANDBOX_DEV
        if (IsSandboxMode)
        {
            ClearBagItem(bagSlot);
            AddChat("系统", $"[系统] 使用了 [{itemName}]。", new Color(0.4f, 1f, 0.4f));
        }
#endif
    }

    /// <summary>
    /// 服务端 SM_MERCHANTDLG_CLOSE (644) 驱动关闭 NPC 窗体
    /// </summary>
    public void CloseNpcDialog()
    {
        if (_npcDialogView != null && _npcDialogView.Visible)
        {
            _stackManager.PopWindow(_npcDialogView);
            _npcDialogView.Visible = false;
            AddChat("系统", "[系统] NPC 对话已关闭。", new Color(0.7f, 0.7f, 0.7f));
        }
        if (_shopPanelView != null && _shopPanelView.Visible)
        {
            _stackManager.PopWindow(_shopPanelView);
            _shopPanelView.Visible = false;
        }
        if (_repairPanelView != null && _repairPanelView.Visible)
        {
            _stackManager.PopWindow(_repairPanelView);
            _repairPanelView.Visible = false;
        }
        if (_storagePanelView != null && _storagePanelView.Visible)
        {
            _stackManager.PopWindow(_storagePanelView);
            _storagePanelView.Visible = false;
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
            _stackManager.PushWindow(_messageBoxDialog);
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
            _stackManager.PushWindow(_messageBoxDialog);
        }
        else
        {
            AddChat("系统", $"[{title}] {msg}", new Color(1f, 0.3f, 0.3f));
            onConfirm?.Invoke();
        }
    }

    /// <summary>
    /// 解析物品描述中的持久力或数量
    /// </summary>
    public static int ExtractItemCountOrDura(string desc)
    {
        if (string.IsNullOrWhiteSpace(desc)) return 1;
        // 匹配 "持久力 X/Y"
        int duraIdx = desc.IndexOf("持久力");
        if (duraIdx >= 0)
        {
            int slashIdx = desc.IndexOf('/', duraIdx);
            if (slashIdx > duraIdx)
            {
                int numStart = duraIdx + 3;
                while (numStart < slashIdx && !char.IsDigit(desc[numStart])) numStart++;
                int numEnd = numStart;
                while (numEnd < slashIdx && char.IsDigit(desc[numEnd])) numEnd++;
                if (numEnd > numStart && int.TryParse(desc[numStart..numEnd], out var d)) return d;
            }
        }
        // 匹配 "数量 X"
        int cntIdx = desc.IndexOf("数量");
        if (cntIdx >= 0)
        {
            int numStart = cntIdx + 2;
            while (numStart < desc.Length && !char.IsDigit(desc[numStart])) numStart++;
            int numEnd = numStart;
            while (numEnd < desc.Length && char.IsDigit(desc[numEnd])) numEnd++;
            if (numEnd > numStart && int.TryParse(desc[numStart..numEnd], out var c)) return c;
        }
        return 1;
    }

    /// <summary>
    /// 更新物品描述中的持久力或数量
    /// </summary>
    public static string UpdateItemCountOrDura(string desc, int newCount)
    {
        if (string.IsNullOrWhiteSpace(desc)) return desc;
        int duraIdx = desc.IndexOf("持久力 ");
        if (duraIdx >= 0)
        {
            int slashIdx = desc.IndexOf('/', duraIdx);
            if (slashIdx > duraIdx)
            {
                string prefix = desc[..(duraIdx + 4)];
                string suffix = desc[slashIdx..];
                return $"{prefix}{newCount}{suffix}";
            }
        }
        return desc;
    }

    /// <summary>
    /// 盛大官方 1.5 背包物品拆分 (SplitDlg, 对齐 IDA sub_48DE20 TFrmDlg_DItemGridSplit)
    /// 按住 Ctrl 单击背包物品弹出拆分输入框，将物品数量/持久拆分为两份放入背包空槽
    /// </summary>
    public bool SplitBagItem(int slot, int splitCount)
    {
        if (slot < 0 || slot >= 40) return false;
        var item = _bagItemDetails[slot];
        if (item == null || splitCount <= 0) return false;

        // 盛大官方 1.5 权威架构铁律：向服务端提交 IntentSplitItem 二进制意图，由服务端全权裁决
        EmitSignal(SignalName.SplitItemRequested, slot, splitCount);

#if MIR2_SANDBOX_DEV
        if (IsSandboxMode)
        {
            int emptySlot = FindFirstEmptyBagSlot();
            if (emptySlot == -1)
            {
                AddChat("系统", "[背包] 背包已满，无法拆分物品！", new Color(1f, 0.4f, 0.4f));
                return false;
            }

            string desc = item.Value.Desc ?? "";
            int currentCount = ExtractItemCountOrDura(desc);
            if (currentCount <= 1 || splitCount >= currentCount)
            {
                AddChat("系统", $"[背包] 该物品无法拆分为 {splitCount} 份 (当前最大可拆分数量: {currentCount - 1})。", new Color(1f, 0.4f, 0.4f));
                return false;
            }

            int remainingCount = currentCount - splitCount;
            string updatedDesc = UpdateItemCountOrDura(desc, remainingCount);
            string newDesc = UpdateItemCountOrDura(desc, splitCount);

            var tex = _bagSlots[slot]?.ItemTexture;

            // 刷新原槽位
            _bagItemDetails[slot] = (item.Value.Name, updatedDesc);

            // 放入新槽位
            _bagItemDetails[emptySlot] = (item.Value.Name, newDesc);
            if (_bagSlots[emptySlot] != null && tex != null)
            {
                _bagSlots[emptySlot]!.ItemTexture = tex;
            }

            AudioManager.Instance?.PlaySoundId(AudioManager.SoundId.ItemClick);
            AddChat("系统", $"[背包] 成功将 [{item.Value.Name}] 拆分出 {splitCount} 份至背包第 {emptySlot + 1} 格。", new Color(0.4f, 1f, 0.5f));
            return true;
        }
#endif
        return true;
    }

    /// <summary>
    /// 盛大官方 1.5 铁匠铺修理成功回写装备实际耐久与最大耐久 (SM_REPAIROK_DELPHI 669)
    /// </summary>
    public void OnServerItemRepaired(int dura, int duraMax, string? itemName)
    {
        // 1. 若当前选中了背包槽位，更新对应物品描述中的持久力
        if (_selectedBagSlot >= 0 && _selectedBagSlot < 40 && _bagItemDetails[_selectedBagSlot] != null)
        {
            var item = _bagItemDetails[_selectedBagSlot]!.Value;
            string updatedDesc = $"[color=#c0c0c0]重量 1   持久力 {dura}/{duraMax}[/color]\n[color=#ffff00]攻击 0-1[/color]";
            _bagItemDetails[_selectedBagSlot] = (item.Name, updatedDesc);
            AddChat("修理", $"[铁匠铺] 背包第 {_selectedBagSlot + 1} 格 [{item.Name}] 耐久度已刷新为 {dura}/{duraMax}。", new Color(0.3f, 1f, 0.5f));
        }
        else
        {
            // 2. 否则查找背包中同名物品或第一个非空物品更新
            for (int i = 0; i < 40; i++)
            {
                if (_bagItemDetails[i] != null && (string.IsNullOrEmpty(itemName) || _bagItemDetails[i]!.Value.Name == itemName))
                {
                    var item = _bagItemDetails[i]!.Value;
                    string updatedDesc = $"[color=#c0c0c0]重量 1   持久力 {dura}/{duraMax}[/color]\n[color=#ffff00]攻击 0-1[/color]";
                    _bagItemDetails[i] = (item.Name, updatedDesc);
                    AddChat("修理", $"[铁匠铺] 背包第 {i + 1} 格 [{item.Name}] 耐久度已刷新为 {dura}/{duraMax}。", new Color(0.3f, 1f, 0.5f));
                    break;
                }
            }
        }
    }
}
