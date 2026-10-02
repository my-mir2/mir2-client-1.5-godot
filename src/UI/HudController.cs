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

    // 官方 1.5 血球魔球控件 (Prguse_0004 @ (39, 440) 相对屏幕，相对 HUD @ (39, 91))
    private TextureRect? _orbTextureRect;

    public override void _Ready()
    {
        _lblHp = GetNodeOrNull<Label>("StatusBar/LblHP");
        _lblMp = GetNodeOrNull<Label>("StatusBar/LblMP");
        _lblLevel = GetNodeOrNull<Label>("StatusBar/LblLevel");
        _lblName = GetNodeOrNull<Label>("StatusBar/LblName");
        _chatLog = GetNodeOrNull<RichTextLabel>("ChatBox/ChatLog");
        _chatInput = GetNodeOrNull<LineEdit>("ChatInput");

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
            _orbTextureRect = new TextureRect
            {
                Name = "HudOrb",
                Texture = orbTex,
                Position = new Vector2(39, 440),
                Size = new Vector2(92, 90),
                MouseFilter = Control.MouseFilterEnum.Ignore,
                ZIndex = 0 // 位于 HUD 顶层且在底图上方，完美严丝合缝嵌入金属圆环
            };
            AddChild(_orbTextureRect);
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
            if (grid != null && grid.GetChildCount() == 0)
            {
                for (int i = 0; i < 40; i++)
                {
                    int slot = i;
                    var btn = new SlotButton
                    {
                        Name = $"Slot_{slot}",
                        CurrentSlotType = SlotButton.SlotType.Bag,
                        SlotIndex = slot,
                        CustomMinimumSize = new Vector2(35.75f, 27f),
                        Flat = true
                    };
                    btn.Pressed += () => EmitSignal(SignalName.UseItemRequested, slot);
                    grid.AddChild(btn);
                }
            }

            // 绑定背包面板原生关闭按钮 (Prguse_0371 @ 311, 204, 16x23, IDA sub_481dd4 [0x484e86..0x484e99] 底板已烘焙凸起红叉)
            var texBagClose = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0371.png");
            var btnBagClose = new TextureButton
            {
                Name = "BtnBagClose",
                Position = new Vector2(311, 204),
                Size = new Vector2(16, 23),
                TextureNormal = null,
                TexturePressed = texBagClose
            };
            btnBagClose.Pressed += () => _bagPanel.Visible = false;
            _bagPanel.AddChild(btnBagClose);

            // 绑定背包金币图标与金币数量标签 (IDA sub_481dd4 [0x484db3: ecx=1Dh 即 Prguse_0029])
            // 金属圆环范围 x:[0,54], y:[170,224], 中心 (27, 197)，金币 36x21 居中对齐为 (9, 187)
            // 纯黑文本内胆严格为 x:[64, 205], y:[181, 199]，文本框严格收纳于 (64, 182, 140x16)，严禁触碰烘焙高光线
            var texGoldBag = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0029.png");
            var goldIcon = new TextureRect
            {
                Name = "GoldIcon",
                Position = new Vector2(9, 187),
                Size = new Vector2(36, 21),
                Texture = texGoldBag,
                MouseFilter = Control.MouseFilterEnum.Pass
            };
            _bagPanel.AddChild(goldIcon);

            var lblGold = new Label
            {
                Name = "LblBagGold",
                Position = new Vector2(64, 182),
                Size = new Vector2(140, 16),
                Text = "0",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            lblGold.AddThemeFontSizeOverride("font_size", 11);
            lblGold.AddThemeColorOverride("font_color", new Color(1.0f, 0.9f, 0.3f));
            _bagPanel.AddChild(lblGold);

            // 背包底部物品描述内胆 (IDA sub_48DC24 从 (70, 215) 绘制属性，对应底板凹槽 (50, 210, 230, 48))
            var lblBagDesc = new Label
            {
                Name = "LblBagItemDesc",
                Position = new Vector2(50, 210),
                Size = new Vector2(230, 48),
                Text = "",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                AutowrapMode = TextServer.AutowrapMode.WordSmart
            };
            lblBagDesc.AddThemeFontSizeOverride("font_size", 10);
            lblBagDesc.AddThemeColorOverride("font_color", new Color(0.85f, 0.85f, 0.75f));
            _bagPanel.AddChild(lblBagDesc);

            // USE 按钮 (Prguse_0026 @ 254, 183, 44x16, IDA 0x484e1a)
            var texUse = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0026.png");
            if (texUse != null)
            {
                var btnUse = new TextureButton
                {
                    Name = "BtnBagUse",
                    Position = new Vector2(254, 183),
                    Size = new Vector2(44, 16),
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

            // 装备槽位容器
            _equipSlotsContainer = new Control
            {
                Name = "EquipSlotsContainer",
                Position = Vector2.Zero,
                Size = new Vector2(232, 325)
            };
            _equipPanel.AddChild(_equipSlotsContainer);

            // 盛大官方 1.5 角色装备面板向下按钮 (切状态面板 Tab 2): Prguse_0396 (正常) / 0397 (按压), Left=213, Top=143, Size=12x21 (IDA 0x48435d..0x484391)
            var texTabDown = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0396.png");
            var texTabDownPressed = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0397.png");

            var btnTab2 = new TextureButton
            {
                Name = "BtnEquipTab2",
                Position = new Vector2(213, 143),
                Size = new Vector2(12, 21),
                TextureNormal = texTabDown,
                TexturePressed = texTabDownPressed,
                FocusMode = Control.FocusModeEnum.None
            };
            btnTab2.Pressed += () => SwitchEquipToStatus();
            _equipPanel.AddChild(btnTab2);

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
                var btn = new SlotButton
                {
                    Name = $"EquipSlot_{equipSlot}",
                    CurrentSlotType = SlotButton.SlotType.Equip,
                    SlotIndex = equipSlot,
                    Position = new Vector2(s.X, s.Y),
                    Size = new Vector2(s.W, s.H),
                    Flat = true
                };
                btn.Pressed += () => EmitSignal(SignalName.TakeoffItemRequested, equipSlot);
                _equipSlotsContainer?.AddChild(btn);
            }
        }

        _minimapView = GetNodeOrNull<MinimapView>("Minimap");
        _npcDialogView = GetNodeOrNull<NpcDialogView>("NpcDialog");
        _deathScreenView = GetNodeOrNull<DeathScreenView>("DeathScreen");

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

        _messageBoxDialog = new MessageBoxDialog();
        AddChild(_messageBoxDialog);

        // 官方 1.5 HUD 底栏 6 大原生功能按钮 (Prguse_0001 对应包裹/装备/技能/组队/行会/交易)
        // 依据 IDA sub_481DD4 实证热区分布，为底栏添加交互热区
        SetupBottomBarButtons();

        GD.Print("[HudController] 官方 1.5 C# HUD 控制器初始化就绪 (全量大世界原生窗体 100% 挂载完成)");
    }

    private void SetupBottomBarButtons()
    {
        // 盛大原版底栏右侧功能按键热区 (IDA sub_481DD4 [0x484b80..0x484d8e]):
        // 1. 包裹 (285, 59, 32x29 相对底板，即屏幕 285, 408) -> F9
        // 2. 装备 (328, 59, 32x29) -> F10
        // 3. 技能 (371, 59, 32x29) -> F11
        // 4. 组队 (415, 59, 32x29) -> Group
        // 5. 行会 (459, 59, 32x29) -> Guild
        // 6. 交易 (503, 59, 32x29) -> Trade
        var btnDefs = new (string Name, float X, float Y, float W, float H, Action OnClick)[]
        {
            ("BtnHudBag", 285, 408, 32, 29, ToggleBag),
            ("BtnHudEquip", 328, 408, 32, 29, ToggleEquip),
            ("BtnHudSkill", 371, 408, 32, 29, ToggleSkill),
            ("BtnHudGroup", 415, 408, 32, 29, ToggleGroup),
            ("BtnHudGuild", 459, 408, 32, 29, ToggleGuild),
            ("BtnHudTrade", 503, 408, 32, 29, ToggleTrade)
        };

        var bottomBarContainer = new Control
        {
            Name = "BottomBarContainer",
            ZIndex = 0,
            MouseFilter = Control.MouseFilterEnum.Ignore
        };
        AddChild(bottomBarContainer);

        foreach (var def in btnDefs)
        {
            var btn = new Button
            {
                Name = def.Name,
                Position = new Vector2(def.X, def.Y),
                Size = new Vector2(def.W, def.H),
                Flat = true,
                FocusMode = Control.FocusModeEnum.None
            };
            btn.Pressed += def.OnClick;
            bottomBarContainer.AddChild(btn);
        }
    }

    public void UpdateHp(int hp, int maxHp)
    {
        if (_lblHp != null) _lblHp.Text = $"{hp}/{maxHp}";
    }

    public void UpdateMp(int mp, int maxMp)
    {
        if (_lblMp != null) _lblMp.Text = $"{mp}/{maxMp}";
    }

    public void UpdateLevel(int level)
    {
        if (_lblLevel != null) _lblLevel.Text = $"Lv.{level}";
    }

    public void UpdateName(string name)
    {
        if (_lblName != null) _lblName.Text = name;
    }

    public void UpdateGold(int gold)
    {
    }

    public void AddChat(string channel, string msg, Color color)
    {
        if (_chatLog != null)
        {
            string colorHex = color.ToHtml(false);
            _chatLog.AppendText($"[color=#{colorHex}][{channel}] {msg}[/color]\n");
        }
    }

    public void ToggleBag()
    {
        if (_bagPanel != null)
        {
            _bagPanel.Visible = !_bagPanel.Visible;
            GD.Print($"[HudController] 切换背包面板显示状态: {_bagPanel.Visible}");
        }
    }

    public void ToggleEquip()
    {
        if (_equipPanel != null)
        {
            _equipPanel.Visible = !_equipPanel.Visible;
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
        }
    }

    public void ToggleSkill()
    {
        if (_skillPanelView != null)
        {
            _stackManager.ToggleWindow(_skillPanelView);
            GD.Print($"[HudController] 切换技能面板显示状态: {_skillPanelView.Visible}");
        }
    }

    public void ToggleGroup()
    {
        if (_groupPanelView != null)
        {
            _stackManager.ToggleWindow(_groupPanelView);
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
                case Key.Tab:
                {
                    _minimapView?.CycleMode();
                    GetViewport().SetInputAsHandled();
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
}
