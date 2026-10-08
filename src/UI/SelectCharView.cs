using System;
using System.Collections.Generic;
using Godot;
using Mir2Client.Audio;
using Mir2Client.Utils;

namespace Mir2Client.UI;

/// <summary>
/// 盛大官方 1.5 选角与创角神龛界面 (纯 C# 场景驱动控制器)
/// 严格 1:1 对齐官方二进制 MIR_final_v3_p.exe 逆向实证 (IDA TSelChrScene 0x47F264（已证无函数） / sub_481DD4 / sub_47E7CC / sub_47EA80)
/// 
/// 官方核心规范实证：
/// 1. 神龛背景：Prguse.wil[65] 800x600 (sub_47F264 @ 0x47F29D)
/// 2. 双席位角色立绘与动态坐标体系 (sub_47F264 @ 0x47F328 - 0x47F43C):
///    - 男战: (71, 52)
///    - 女战: (65, 55)
///    - 男法: (77, 46)
///    - 女法: 激活(171, 97), 石雕待机(141, 83)
///    - 男道: (85, 63)
///    - 女道: 激活(164, 103), 石雕待机(141, 83)
///    - 2号位 (Slot 1) 坐标偏移: X += 340, Y += 2
/// 3. 六大职业与性别立绘帧号算法 (sub_47F264 @ 0x47F48A / 0x47F6B8 / 0x47F829):
///    BaseFrame = 120 * Gender + 40 * Job + (IsSelected ? 40 : 60)
/// 4. 脚底神龛火环粒子动画：ChrSel.wil[4..17]，共 14 帧，110ms 帧步长 (0x47F57D)
///    Slot 0: (90, 58), Slot 1: (430, 60)
/// 5. 官方石质交互按钮 (Prguse.wil[66..72], sub_481DD4):
///    - DscSelect1 (恢复/选定1): Prguse[66], (133, 453), 76x33
///    - DscSelect2 (恢复/选定2): Prguse[67], (685, 454), 76x33
///    - DscStart   (开始游戏):  Prguse[68], (385, 456), 44x21
///    - DscNewChr  (新建角色):  Prguse[69], (348, 486), 120x21
///    - DscEraseChr(删除角色):  Prguse[70], (347, 506), 120x21
///    - DscCredits (制作者群):  Prguse[71], (362, 527), 92x20
///    - DscExit    (退出游戏):  Prguse[72], (379, 547), 56x20
/// 6. 创角面板 (DCreateChr, Prguse[73] @ 250, 91):
///    职业(74/75/76)、性别(77/78)、发型(79/80)、确认(62)、关闭(64)
/// 7. 槽位音效与交互提示：
///    - 点击席位播放 101 音效 (sub_47E7CC @ 0x47E838)
///    - 满角色提示: '你可以为每个单独的帐户创建两个角色。' (0x47EA4C)
///    - 删角提示: '删除的角色是不能被恢复的。一段时间内，你将不能使用相同的角色名。你真的想要删除角色吗？' (0x47EBDC)
/// </summary>
public partial class SelectCharView : Control
{
    // 官方 1.5 逆向实证常量定义
    public const int OfficialBgImageIndex = 65; // Prguse.wil[65]
    public const int OfficialFlameStartFrame = 4; // ChrSel.wil[4]
    public const int OfficialFlameTotalFrames = 14; // ChrSel.wil[4..17]
    public const double OfficialFlameFrameDuration = 0.110; // 110ms (0x6E)
    public const double OfficialAnimFrameDuration = 0.300; // 300ms (0x12C)

    public const string PromptTwoCharactersMax = "你可以为每个单独的帐户创建两个角色。";
    public const string PromptDeleteConfirm = "删除的角色是不能被恢复的。\n一段时间内，你将不能使用相同的角色名。\n你真的想要删除角色吗？";

    [Signal]
    public delegate void CharSelectedEventHandler(int index, string charName, int level);

    [Signal]
    public delegate void NewCharRequestedEventHandler(string name, int job, int sex);

    [Signal]
    public delegate void DelCharRequestedEventHandler(int index);

    public readonly record struct CharEntry(string Name, int Job, int Sex, int Level);

    private readonly List<CharEntry> _characters = new();
    private int _selectedSlot = 0;

    // 视觉与动画渲染节点
    private TextureRect? _bgTexRect;
    private TextureRect? _flameSlot0;
    private TextureRect? _flameSlot1;
    private TextureRect? _charSlot0;
    private TextureRect? _charSlot1;
    private Label? _serverTitle;

    // 席位角色信息标签
    private Label? _char1Name;
    private Label? _char1Level;
    private Label? _char1Job;
    private Label? _char2Name;
    private Label? _char2Level;
    private Label? _char2Job;

    // 官方石质交互按钮
    private TextureButton? _btnChar1;
    private TextureButton? _btnChar2;
    private TextureButton? _btnStart;
    private TextureButton? _btnNew;
    private TextureButton? _btnDel;
    private TextureButton? _btnCredits;
    private TextureButton? _btnExit;
    private Label? _lblInfo;

    // 创角子面板 (DCreateChr)
    private Panel? _newCharPanel;
    private LineEdit? _newCharName;
    private TextureButton? _btnWarrior;
    private TextureButton? _btnWizzard;
    private TextureButton? _btnMonk;
    private TextureButton? _btnMale;
    private TextureButton? _btnFemale;
    private TextureButton? _btnLeftHair;
    private TextureButton? _btnRightHair;
    private TextureButton? _btnCreate;
    private TextureButton? _btnCancel;

    private int _newCharJob = 0;
    private int _newCharSex = 0;
    private int _newCharHair = 0;

    // 官方模态弹窗 (MsgModal)
    private Panel? _msgModal;
    private Label? _msgText;
    private TextureButton? _btnMsgOk;

    // 动画状态机
    private double _flameTimer = 0.0;
    private int _flameFrame = 0;
    private double _charAnimTimer = 0.0;
    private int _charAnimFrame = 0;

    public override void _Ready()
    {
        // 1. 设置神龛背景贴图 (Prguse.wil[65] 800x600)
        var bgNode = GetNodeOrNull<Control>("BG");
        var texBg = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0065.png")
                 ?? ResPath.LoadImageTexture("resources/wil/png/ChrSel/ChrSel_0022.png");

        if (texBg != null && bgNode != null)
        {
            _bgTexRect = new TextureRect
            {
                Name = "BGTex",
                Texture = texBg,
                Size = new Vector2(800, 600),
                MouseFilter = MouseFilterEnum.Ignore
            };
            AddChild(_bgTexRect);
            MoveChild(_bgTexRect, 0);
            bgNode.Visible = false;
        }

        // 2. 绑定火环、角色立绘节点与顶部标题
        _serverTitle = GetNodeOrNull<Label>("ServerTitle");
        _flameSlot0 = GetNodeOrNull<TextureRect>("FlameSlot0");
        _flameSlot1 = GetNodeOrNull<TextureRect>("FlameSlot1");
        _charSlot0 = GetNodeOrNull<TextureRect>("CharSlot0");
        _charSlot1 = GetNodeOrNull<TextureRect>("CharSlot1");

        // 3. 绑定 1号位 / 2号位 角色信息控件 (sub_47F264 @ 0x47FAAD / 0x47FB93)
        _char1Name = GetNodeOrNull<Label>("Char1Info/Name");
        _char1Level = GetNodeOrNull<Label>("Char1Info/Level");
        _char1Job = GetNodeOrNull<Label>("Char1Info/Job");

        _char2Name = GetNodeOrNull<Label>("Char2Info/Name");
        _char2Level = GetNodeOrNull<Label>("Char2Info/Level");
        _char2Job = GetNodeOrNull<Label>("Char2Info/Job");

        // 4. 绑定主控按钮并绑定官方 Prguse.wil[66..72] 石质贴图 (sub_481DD4)
        _btnChar1 = BindTextureButton("BtnChar1", 66);
        _btnChar2 = BindTextureButton("BtnChar2", 67);
        _btnStart = BindTextureButton("BtnStart", 68);
        _btnNew = BindTextureButton("BtnNew", 69);
        _btnDel = BindTextureButton("BtnDel", 70);
        _btnCredits = BindTextureButton("BtnCredits", 71);
        _btnExit = BindTextureButton("BtnExit", 72);
        _lblInfo = GetNodeOrNull<Label>("LblInfo");

        // 5. 绑定创角子面板 (Prguse.wil[73] 300x417 及各控制按钮)
        _newCharPanel = GetNodeOrNull<Panel>("NewCharPanel");
        if (_newCharPanel != null)
        {
            var texNew = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0073.png");
            if (texNew != null)
            {
                _newCharPanel.AddThemeStyleboxOverride("panel", new StyleBoxTexture { Texture = texNew });
            }
            _newCharName = _newCharPanel.GetNodeOrNull<LineEdit>("TxtName");

            // 职业按钮：战士 (74/55), 法师 (75/56), 道士 (76/57)
            _btnWarrior = BindTextureButton("NewCharPanel/BtnWarrior", 74, 55);
            _btnWizzard = BindTextureButton("NewCharPanel/BtnWizzard", 75, 56);
            _btnMonk = BindTextureButton("NewCharPanel/BtnMonk", 76, 57);

            // 性别按钮：男 (77/58), 女 (78/59)
            _btnMale = BindTextureButton("NewCharPanel/BtnMale", 77, 58);
            _btnFemale = BindTextureButton("NewCharPanel/BtnFemale", 78, 59);

            _btnLeftHair = BindTextureButton("NewCharPanel/BtnLeftHair", 79);
            _btnRightHair = BindTextureButton("NewCharPanel/BtnRightHair", 80);
            _btnCreate = BindTextureButton("NewCharPanel/BtnCreate", 62, 81);
            _btnCancel = BindTextureButton("NewCharPanel/BtnCancel", 64);

            if (_btnWarrior != null) _btnWarrior.Pressed += () => SelectJob(0);
            if (_btnWizzard != null) _btnWizzard.Pressed += () => SelectJob(1);
            if (_btnMonk != null) _btnMonk.Pressed += () => SelectJob(2);
            if (_btnMale != null) _btnMale.Pressed += () => SelectSex(0);
            if (_btnFemale != null) _btnFemale.Pressed += () => SelectSex(1);
            if (_btnLeftHair != null) _btnLeftHair.Pressed += () => ChangeHair(-1);
            if (_btnRightHair != null) _btnRightHair.Pressed += () => ChangeHair(1);
            if (_btnCreate != null) _btnCreate.Pressed += OnConfirmCreateChar;
            if (_btnCancel != null) _btnCancel.Pressed += () => _newCharPanel.Visible = false;

            UpdateJobAndSexButtonVisuals();
        }

        // 6. 绑定官方石质提示模态弹窗 (MsgModal，严格对齐官方 IDA sub_486DF0 / Prguse_0360 452x179 居中)
        _msgModal = GetNodeOrNull<Panel>("MsgModal");
        if (_msgModal != null)
        {
            var texBox = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0360.png");
            if (texBox != null)
            {
                _msgModal.AddThemeStyleboxOverride("panel", new StyleBoxTexture { Texture = texBox });
            }
            _msgModal.Position = new Vector2(174, 210);
            _msgModal.Size = new Vector2(452, 179);

            _msgText = _msgModal.GetNodeOrNull<Label>("MsgText");
            if (_msgText != null)
            {
                _msgText.Position = new Vector2(30, 25);
                _msgText.Size = new Vector2(392, 95);
            }

            _btnMsgOk = BindTextureButton("MsgModal/BtnMsgOk", 62);
            if (_btnMsgOk != null)
            {
                _btnMsgOk.Position = new Vector2(188, 126);
                _btnMsgOk.Size = new Vector2(76, 33);
                _btnMsgOk.Pressed += () =>
                {
                    _msgModal.Visible = false;
                    _pendingOkAction?.Invoke();
                    _pendingOkAction = null;
                };
            }
        }

        // 7. 连接主按钮事件 (对齐 sub_4886C0 虚表分发)
        if (_btnChar1 != null) _btnChar1.Pressed += () => SelectSlot(0);
        if (_btnChar2 != null) _btnChar2.Pressed += () => SelectSlot(1);
        if (_btnStart != null) _btnStart.Pressed += OnStartGame;
        if (_btnNew != null) _btnNew.Pressed += OnOpenCreateChar;
        if (_btnDel != null) _btnDel.Pressed += OnDeleteChar;
        if (_btnExit != null) _btnExit.Pressed += () => GetTree().Quit();

        RefreshCharacterDisplay();
        UpdateAnimations(0);
        GD.Print("[SelectCharView] 官方 1.5 选角神龛与 Prguse[65..73] 全细节绑定就绪");
    }

    private TextureButton? BindTextureButton(string nodePath, int normalIndex, int pressedIndex = -1)
    {
        var btn = GetNodeOrNull<TextureButton>(nodePath);
        if (btn != null)
        {
            var tex = ResPath.LoadImageTexture($"resources/wil/png/Prguse/Prguse_{normalIndex:D4}.png");
            if (tex != null)
            {
                btn.TextureNormal = tex;
                btn.CustomMinimumSize = tex.GetSize();
            }

            int pIdx = pressedIndex >= 0 ? pressedIndex : (normalIndex == 62 ? 81 : -1);
            if (pIdx >= 0)
            {
                var texPressed = ResPath.LoadImageTexture($"resources/wil/png/Prguse/Prguse_{pIdx:D4}.png");
                if (texPressed != null)
                {
                    btn.TexturePressed = texPressed;
                }
            }
        }
        return btn;
    }

    public override void _Process(double delta)
    {
        // 1. 脚底神龛火环粒子动画 (110ms / 0x6E 帧步长, sub_47F264 @ 0x47F57D)
        _flameTimer += delta;
        if (_flameTimer >= OfficialFlameFrameDuration)
        {
            _flameTimer = 0.0;
            _flameFrame = (_flameFrame + 1) % OfficialFlameTotalFrames;
            UpdateFlameTextures();
        }

        // 2. 角色呼吸动作循环动画 (300ms / 0x12C 帧步长, sub_47F264 @ 0x47F9EB)
        _charAnimTimer += delta;
        if (_charAnimTimer >= OfficialAnimFrameDuration)
        {
            _charAnimTimer = 0.0;
            _charAnimFrame = (_charAnimFrame + 1) % 13; // 呼吸待机循环为 13 帧
            UpdateCharTextures();
        }
    }

    private void UpdateFlameTextures()
    {
        int flameIdx = OfficialFlameStartFrame + _flameFrame;
        var texFlame = ResPath.LoadImageTexture($"resources/wil/png/ChrSel/ChrSel_{flameIdx:D4}.png");

        // 只有当前激活选中的席位或存在角色的席位才展现火环
        if (_flameSlot0 != null)
        {
            bool show0 = _characters.Count > 0 && _selectedSlot == 0;
            _flameSlot0.Visible = show0;
            if (show0 && texFlame != null)
            {
                _flameSlot0.Texture = texFlame;
                _flameSlot0.Size = texFlame.GetSize();
                _flameSlot0.Position = new Vector2(90, 58);
            }
        }

        if (_flameSlot1 != null)
        {
            bool show1 = _characters.Count > 1 && _selectedSlot == 1;
            _flameSlot1.Visible = show1;
            if (show1 && texFlame != null)
            {
                _flameSlot1.Texture = texFlame;
                _flameSlot1.Size = texFlame.GetSize();
                _flameSlot1.Position = new Vector2(430, 60);
            }
        }
    }

    private void UpdateCharTextures()
    {
        // Slot 0 (Left)
        if (_characters.Count > 0 && _charSlot0 != null)
        {
            var c0 = _characters[0];
            bool isSelected = (_selectedSlot == 0);
            int baseFrame = CalculateCharBaseFrame(c0.Job, c0.Sex, isSelected);
            int frame = isSelected ? (baseFrame + _charAnimFrame) : baseFrame;
            var tex0 = ResPath.LoadImageTexture($"resources/wil/png/ChrSel/ChrSel_{frame:D4}.png");
            if (tex0 != null)
            {
                _charSlot0.Texture = tex0;
                _charSlot0.Size = tex0.GetSize();
                _charSlot0.Position = GetCharacterCoordinate(0, c0.Job, c0.Sex, isSelected);
                _charSlot0.Visible = true;
            }
        }
        else if (_charSlot0 != null)
        {
            _charSlot0.Visible = false;
        }

        // Slot 1 (Right)
        if (_characters.Count > 1 && _charSlot1 != null)
        {
            var c1 = _characters[1];
            bool isSelected = (_selectedSlot == 1);
            int baseFrame = CalculateCharBaseFrame(c1.Job, c1.Sex, isSelected);
            int frame = isSelected ? (baseFrame + _charAnimFrame) : baseFrame;
            var tex1 = ResPath.LoadImageTexture($"resources/wil/png/ChrSel/ChrSel_{frame:D4}.png");
            if (tex1 != null)
            {
                _charSlot1.Texture = tex1;
                _charSlot1.Size = tex1.GetSize();
                _charSlot1.Position = GetCharacterCoordinate(1, c1.Job, c1.Sex, isSelected);
                _charSlot1.Visible = true;
            }
        }
        else if (_charSlot1 != null)
        {
            _charSlot1.Visible = false;
        }
    }

    /// <summary>
    /// 官方 1.5 IDA 动态席位坐标体系实证 (sub_47F264 @ 0x47F328 - 0x47F43C):
    /// 男战: (71, 52), 女战: (65, 55), 男法: (77, 46)
    /// 女法: 激活(171, 97), 待机(141, 83)
    /// 男道: (85, 63)
    /// 女道: 激活(164, 103), 待机(141, 83)
    /// 2号位: X += 340, Y += 2
    /// </summary>
    public static Vector2 GetCharacterCoordinate(int slotIndex, int job, int sex, bool isSelected)
    {
        int x, y;
        if (job == 0) // Warrior
        {
            x = (sex != 0) ? 65 : 71;
            y = (sex != 0) ? 55 : 52;
        }
        else if (job == 1) // Wizard
        {
            if (sex != 0) // Female
            {
                x = isSelected ? 171 : 141;
                y = isSelected ? 97 : 83;
            }
            else // Male
            {
                x = 77;
                y = 46;
            }
        }
        else // Taoist
        {
            if (sex != 0) // Female
            {
                x = isSelected ? 164 : 141;
                y = isSelected ? 103 : 83;
            }
            else // Male
            {
                x = 85;
                y = 63;
            }
        }

        if (slotIndex == 1)
        {
            x += 340;
            y += 2;
        }

        return new Vector2(x, y);
    }

    /// <summary>
    /// 官方 1.5 IDA 帧号算法实证 (sub_47F264 @ 0x47F48A / 0x47F6B8 / 0x47F829)：
    /// Base = 120 * Gender + 40 * Job + (IsSelected ? 40 : 60)
    /// </summary>
    public static int CalculateCharBaseFrame(int job, int sex, bool isSelected)
    {
        int genderBase = 120 * (sex != 0 ? 1 : 0);
        int jobBase = 40 * Math.Clamp(job, 0, 2);
        int stateBase = isSelected ? 40 : 60;
        return genderBase + jobBase + stateBase;
    }

    private void UpdateAnimations(int forceFrame = 0)
    {
        _flameFrame = forceFrame % OfficialFlameTotalFrames;
        _charAnimFrame = forceFrame % 13;
        UpdateFlameTextures();
        UpdateCharTextures();
    }

    public void SetCharacters(IEnumerable<CharEntry> chars)
    {
        _characters.Clear();
        _characters.AddRange(chars);
        RefreshCharacterDisplay();
        UpdateAnimations(0);
    }

    public void SelectSlot(int slot)
    {
        _selectedSlot = slot;
        GD.Print($"[SelectCharView] 切换选中角色槽位: {slot}");
        
        // 官方 1.5 席位切换音效实证: sub_47E7CC / sub_47E840 @ 0x47E838: sub_495794(101)
        AudioManager.Instance?.PlaySoundId(101);

        if (_lblInfo != null)
        {
            if (slot < _characters.Count)
            {
                _lblInfo.Text = $"当前选中角色: {_characters[slot].Name} (Lv.{_characters[slot].Level})";
            }
            else
            {
                _lblInfo.Text = $"槽位 {slot + 1} 为空，可点击创建角色";
            }
        }
        UpdateAnimations(0);
    }

    private void RefreshCharacterDisplay()
    {
        // 1 号位角色信息 (对齐 sub_47F264 @ 0x47FAAD)
        if (_characters.Count > 0)
        {
            var c1 = _characters[0];
            if (_char1Name != null) _char1Name.Text = c1.Name;
            if (_char1Level != null) _char1Level.Text = $"{c1.Level} 级";
            if (_char1Job != null) _char1Job.Text = GetJobName(c1.Job);
        }
        else
        {
            if (_char1Name != null) _char1Name.Text = "";
            if (_char1Level != null) _char1Level.Text = "";
            if (_char1Job != null) _char1Job.Text = "";
        }

        // 2 号位角色信息 (对齐 sub_47F264 @ 0x47FB93)
        if (_characters.Count > 1)
        {
            var c2 = _characters[1];
            if (_char2Name != null) _char2Name.Text = c2.Name;
            if (_char2Level != null) _char2Level.Text = $"{c2.Level} 级";
            if (_char2Job != null) _char2Job.Text = GetJobName(c2.Job);
        }
        else
        {
            if (_char2Name != null) _char2Name.Text = "";
            if (_char2Level != null) _char2Level.Text = "";
            if (_char2Job != null) _char2Job.Text = "";
        }
    }

    private static string GetJobName(int job) => job switch
    {
        0 => "战士",
        1 => "法师",
        2 => "道士",
        _ => "未知"
    };

    private void OnStartGame()
    {
        // 官方 1.5 开始游戏实证 (sub_47E8BC @ 0x47E935):
        // 若没有选中角色，则弹出提示 '一开始你应该创建一个角色'
        if (_characters.Count > _selectedSlot)
        {
            var c = _characters[_selectedSlot];
            EmitSignal(SignalName.CharSelected, _selectedSlot, c.Name, c.Level);
        }
        else
        {
            ShowMessageModal("一开始你应该创建一个角色。");
        }
    }

    private void OnOpenCreateChar()
    {
        // 官方 1.5 席位满员校验实证 (sub_47E9F0 @ 0x47EA03):
        // 两个角色槽位均有角色时，弹出 '你可以为每个单独的帐户创建两个角色。'
        if (_characters.Count >= 2)
        {
            ShowMessageModal(PromptTwoCharactersMax);
            return;
        }

        if (_newCharPanel != null)
        {
            _newCharPanel.Visible = true;
            _newCharName?.GrabFocus();
        }
    }

    private void SelectJob(int job)
    {
        _newCharJob = job;
        UpdateJobAndSexButtonVisuals();
        GD.Print($"[SelectCharView] 选择创角职业: {GetJobName(job)}");
    }

    private void SelectSex(int sex)
    {
        _newCharSex = sex;
        UpdateJobAndSexButtonVisuals();
        GD.Print($"[SelectCharView] 选择创角性别: {(sex == 0 ? "男" : "女")}");
    }

    private void UpdateJobAndSexButtonVisuals()
    {
        // 职业选中态 (选中的显示凹陷按下态贴图，未选中的显示凸起正常态贴图)
        var texWarNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0074.png");
        var texWarPressed = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0055.png");
        var texWizNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0075.png");
        var texWizPressed = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0056.png");
        var texMonkNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0076.png");
        var texMonkPressed = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0057.png");

        if (_btnWarrior != null) _btnWarrior.TextureNormal = (_newCharJob == 0) ? texWarPressed : texWarNormal;
        if (_btnWizzard != null) _btnWizzard.TextureNormal = (_newCharJob == 1) ? texWizPressed : texWizNormal;
        if (_btnMonk != null) _btnMonk.TextureNormal = (_newCharJob == 2) ? texMonkPressed : texMonkNormal;

        // 性别选中态
        var texMaleNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0077.png");
        var texMalePressed = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0058.png");
        var texFemaleNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0078.png");
        var texFemalePressed = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0059.png");

        if (_btnMale != null) _btnMale.TextureNormal = (_newCharSex == 0) ? texMalePressed : texMaleNormal;
        if (_btnFemale != null) _btnFemale.TextureNormal = (_newCharSex == 1) ? texFemalePressed : texFemaleNormal;
    }

    private void ChangeHair(int delta)
    {
        _newCharHair = Math.Clamp(_newCharHair + delta, 0, 1);
        GD.Print($"[SelectCharView] 切换发型: {_newCharHair}");
    }

    private void OnConfirmCreateChar()
    {
        string name = _newCharName?.Text?.Trim() ?? string.Empty;
        if (string.IsNullOrEmpty(name))
        {
            ShowMessageModal("请输入新角色名称！");
            return;
        }

        EmitSignal(SignalName.NewCharRequested, name, _newCharJob, _newCharSex);
        if (_newCharPanel != null) _newCharPanel.Visible = false;
    }

    private void OnDeleteChar()
    {
        // 官方 1.5 删除角色实证 (sub_47EA80 @ 0x47EAF9 / 0x47EBDC):
        // 弹出模态确认框: PromptDeleteConfirm
        if (_selectedSlot < _characters.Count)
        {
            ShowMessageModal(PromptDeleteConfirm, () =>
            {
                EmitSignal(SignalName.DelCharRequested, _selectedSlot);
            });
        }
    }

    private Action? _pendingOkAction;

    public void ShowMessageModal(string text, Action? onOk = null)
    {
        _pendingOkAction = onOk;
        if (_newCharPanel != null) _newCharPanel.Visible = false;
        if (_msgModal != null && _msgText != null)
        {
            _msgText.Text = text;
            _msgModal.Visible = true;
        }
    }
}
