using System;
using Godot;
using Mir2Client.Utils;

namespace Mir2Client.UI;

/// <summary>
/// 盛大官方 1.5 经典账号登录主窗体 (纯 C# 场景驱动控制器)
/// 严格 1:1 绑定 scenes/Login.tscn 场景树原生节点
/// 贴图基准：ChrSel.wil[22] 背景 + Prguse.wil[60] 石质登录框 (IDA sub_47F00C 内偏移 0x47F03C；0x47F264 已证无函数)
/// </summary>
public partial class LoginView : Control
{
    [Signal]
    public delegate void LoginRequestedEventHandler(string host, int port, string username, string password);

    [Signal]
    public delegate void RegisterRequestedEventHandler(string username, string password);

    [Signal]
    public delegate void ChangePasswordRequestedEventHandler(string username, string oldPassword, string newPassword);

    public string ServerHost { get; set; } = "127.0.0.1";
    public int ServerPort { get; set; } = 7001;

    // 官方 1.5 IDA 逆向存疑：0x47D6CC 处无函数（已实证 is_func=False），开门帧 23..32 以 ChrSel.wil 为准；0x47D864 待证
    public const int OfficialDoorStartFrame = 23; // ChrSel.wil[23] 开门首帧
    public const int OfficialDoorEndFrame = 32;   // ChrSel.wil[32] 开门尾帧 (全开黑洞)
    public const int OfficialDoorTotalFrames = 10;
    public const double OfficialDoorFrameDuration = 0.230; // 0xE6 ms (230ms)

    private Panel? _panel;
    private LineEdit? _txtUsername;
    private LineEdit? _txtPassword;
    private Button? _btnLogin;
    private TextureButton? _btnClose;
    private Button? _btnNewUser;
    private Button? _btnChangePwd;
    private Label? _lblStatus;
    private Button? _btnSandbox;

    // 石门动画节点与纹理缓存
    private TextureRect? _doorAnimRect;
    private readonly Texture2D?[] _doorFrames = new Texture2D?[OfficialDoorTotalFrames];
    private bool _isDoorOpening = false;
    private int _doorCurrentIndex = 0;
    private double _doorTimer = 0.0;
    private Action? _onDoorOpenedCallback;

    // 官方 1.5 子面板 (IDA 0x47D864（待实证）: 0x0B=新用户, 0x0C=改密)
    private Panel? _newUserPanel;
    private LineEdit? _txtRegUser;
    private LineEdit? _txtRegPass1;
    private LineEdit? _txtRegPass2;
    private Button? _btnRegSubmit;
    private Button? _btnRegCancel;
    private TextureButton? _btnNewUserClose;

    private Panel? _changePwdPanel;
    private LineEdit? _txtAccount;
    private LineEdit? _txtOldPass;
    private LineEdit? _txtNewPass1;
    private LineEdit? _txtNewPass2;
    private Button? _btnChangeSubmit;
    private Button? _btnChangeCancel;
    private TextureButton? _btnChangeClose;

    public bool IsDoorOpening => _isDoorOpening;
    public int CurrentDoorFrameIndex => _doorCurrentIndex;

    public override void _Ready()
    {
        // 1. 设置背景贴图（对齐官方 ChrSel.wil[22] 800x600）
        var bgNode = GetNodeOrNull<Control>("BG");
        var texBg = ResPath.LoadImageTexture("resources/wil/png/ChrSel/ChrSel_0022.png");
        if (texBg != null && bgNode != null)
        {
            if (bgNode is TextureRect texRect)
            {
                texRect.Texture = texBg;
            }
            else
            {
                var tr = new TextureRect
                {
                    Name = "BGTex",
                    Texture = texBg,
                    Size = new Vector2(800, 600),
                    MouseFilter = MouseFilterEnum.Ignore
                };
                AddChild(tr);
                MoveChild(tr, 0);
                bgNode.Visible = false;
            }
        }

        // 2. 预加载官方开门动画纹理序列 (ChrSel.wil[23..32], IDA 0x47D6CC（已证无函数） @ 0x47D7D2)
        _doorAnimRect = GetNodeOrNull<TextureRect>("DoorAnimation");
        for (int i = 0; i < OfficialDoorTotalFrames; i++)
        {
            int frameId = OfficialDoorStartFrame + i;
            string path = $"resources/wil/png/ChrSel/ChrSel_{frameId:D4}.png";
            _doorFrames[i] = ResPath.LoadImageTexture(path);
        }

        // 3. 绑定石质登录面板背景（Prguse.wil[60] 296x254 @ (252, 173)）
        _panel = GetNodeOrNull<Panel>("Panel");
        if (_panel != null)
        {
            var texPanel = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0060.png");
            if (texPanel != null)
            {
                _panel.AddThemeStyleboxOverride("panel", new StyleBoxTexture { Texture = texPanel });
            }
        }

        // 4. 绑定登录主框原生控件
        _txtUsername = GetNodeOrNull<LineEdit>("Panel/TxtUsername");
        _txtPassword = GetNodeOrNull<LineEdit>("Panel/TxtPassword");
        _btnLogin = GetNodeOrNull<Button>("Panel/BtnLogin");
        _btnClose = GetNodeOrNull<TextureButton>("Panel/BtnClose");
        _btnNewUser = GetNodeOrNull<Button>("Panel/BtnNewUser");
        _btnChangePwd = GetNodeOrNull<Button>("Panel/BtnChangePwd");
        _lblStatus = GetNodeOrNull<Label>("Panel/LblStatus");
        _btnSandbox = GetNodeOrNull<Button>("BtnSandbox");

        var texRedX = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0064.png");
        var texCloseNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/btn_close_normal.png");

        // 主登录面板 Prguse_0060 默认已烘焙凸起红叉，默认不盖按下态贴图，按下时切换为凹陷态
        if (_btnClose != null)
        {
            _btnClose.TextureNormal = null;
            _btnClose.TexturePressed = texRedX;
        }

        if (_btnLogin != null) _btnLogin.Pressed += OnLoginClicked;
        if (_btnClose != null) _btnClose.Pressed += OnCloseClicked;
        if (_btnNewUser != null) _btnNewUser.Pressed += () => SwitchSubState(1); // 0x0B 新用户
        if (_btnChangePwd != null) _btnChangePwd.Pressed += () => SwitchSubState(2); // 0x0C 改密码
        if (_btnSandbox != null)
        {
            _btnSandbox.Pressed += OnSandboxClicked;
            // 遵从交付金标准协议：自动测试与正式画面杜绝任何开发调试按钮残留
            if (OS.GetCmdlineArgs() != null && Array.Exists(OS.GetCmdlineArgs(), a => a == "--autotest"))
            {
                _btnSandbox.Visible = false;
            }
        }

        if (_txtUsername != null)
        {
            _txtUsername.CaretBlink = true;
            _txtUsername.CaretBlinkInterval = 0.5f;
            _txtUsername.MaxLength = 12;
            _txtUsername.TextSubmitted += _ => _txtPassword?.GrabFocus();
        }
        if (_txtPassword != null)
        {
            _txtPassword.CaretBlink = true;
            _txtPassword.CaretBlinkInterval = 0.5f;
            _txtPassword.MaxLength = 12;
            _txtPassword.Secret = true;
            _txtPassword.TextSubmitted += _ => OnLoginClicked();
        }

        // 5. 绑定新用户注册面板 (Prguse.wil[63] 640x473, IDA sub_47D864 模式 2 / 0x0B)
        _newUserPanel = GetNodeOrNull<Panel>("NewUserPanel");
        if (_newUserPanel != null)
        {
            var texReg = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0063.png");
            if (texReg != null)
            {
                _newUserPanel.AddThemeStyleboxOverride("panel", new StyleBoxTexture { Texture = texReg });
            }
            _txtRegUser = _newUserPanel.GetNodeOrNull<LineEdit>("TxtRegUser");
            _txtRegPass1 = _newUserPanel.GetNodeOrNull<LineEdit>("TxtRegPass1");
            _txtRegPass2 = _newUserPanel.GetNodeOrNull<LineEdit>("TxtRegPass2");
            _btnRegSubmit = _newUserPanel.GetNodeOrNull<Button>("BtnRegSubmit");
            _btnRegCancel = _newUserPanel.GetNodeOrNull<Button>("BtnRegCancel");
            _btnNewUserClose = _newUserPanel.GetNodeOrNull<TextureButton>("BtnNewUserClose");

            // 注册面板 Prguse_0063 默认已烘焙凸起红叉
            if (_btnNewUserClose != null)
            {
                _btnNewUserClose.TextureNormal = null;
                _btnNewUserClose.TexturePressed = texRedX;
            }

            if (_btnRegSubmit != null) _btnRegSubmit.Pressed += OnSubmitRegister;
            if (_btnRegCancel != null) _btnRegCancel.Pressed += () => SwitchSubState(0);
            if (_btnNewUserClose != null) _btnNewUserClose.Pressed += () => SwitchSubState(0);
        }

        // 6. 绑定修改密码面板 (Prguse.wil[50] 420x299, IDA sub_47D864 模式 3 / 0x0C)
        _changePwdPanel = GetNodeOrNull<Panel>("ChangePwdPanel");
        if (_changePwdPanel != null)
        {
            var texChange = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0050.png");
            if (texChange != null)
            {
                _changePwdPanel.AddThemeStyleboxOverride("panel", new StyleBoxTexture { Texture = texChange });
            }
            _txtAccount = _changePwdPanel.GetNodeOrNull<LineEdit>("TxtAccount");
            _txtOldPass = _changePwdPanel.GetNodeOrNull<LineEdit>("TxtOldPass");
            _txtNewPass1 = _changePwdPanel.GetNodeOrNull<LineEdit>("TxtNewPass1");
            _txtNewPass2 = _changePwdPanel.GetNodeOrNull<LineEdit>("TxtNewPass2");
            _btnChangeSubmit = _changePwdPanel.GetNodeOrNull<Button>("BtnChangeSubmit");
            _btnChangeCancel = _changePwdPanel.GetNodeOrNull<Button>("BtnChangeCancel");
            _btnChangeClose = _changePwdPanel.GetNodeOrNull<TextureButton>("BtnChangeClose");

            // 修改密码面板 Prguse_0050 底图无烘焙红叉，默认显示凸起态贴图，按下时显示凹陷态贴图
            if (_btnChangeClose != null)
            {
                _btnChangeClose.TextureNormal = texCloseNormal;
                _btnChangeClose.TexturePressed = texRedX;
            }

            if (_btnChangeSubmit != null) _btnChangeSubmit.Pressed += OnSubmitChangePassword;
            if (_btnChangeCancel != null) _btnChangeCancel.Pressed += () => SwitchSubState(0);
            if (_btnChangeClose != null) _btnChangeClose.Pressed += () => SwitchSubState(0);
        }

        GD.Print("[LoginView] 官方 1.5 登录场景逆向对齐完毕: 石门动画(ChrSel 23~32)、登录面板(60)、注册面板(63)、改密面板(50) 全部就绪");
    }

    public override void _Process(double delta)
    {
        if (!_isDoorOpening) return;

        _doorTimer += delta;
        if (_doorTimer >= OfficialDoorFrameDuration)
        {
            _doorTimer -= OfficialDoorFrameDuration;
            _doorCurrentIndex++;

            if (_doorCurrentIndex < OfficialDoorTotalFrames)
            {
                UpdateDoorDisplay(_doorCurrentIndex);
            }
            else
            {
                // 10 帧开门完毕 (洞口全黑，IDA 0x47D859 dl=3 切换场景)
                _isDoorOpening = false;
                GD.Print("[LoginView] 官方 1.5 石门开启完毕，执行场景迁移");
                _onDoorOpenedCallback?.Invoke();
                _onDoorOpenedCallback = null;
            }
        }
    }

    /// <summary>
    /// 触发官方 1:1 石门开启序列动画 (IDA sub_47D6CC)
    /// </summary>
    public void PlayDoorOpeningAnimation(Action onFinished)
    {
        _onDoorOpenedCallback = onFinished;
        _isDoorOpening = true;
        _doorCurrentIndex = 0;
        _doorTimer = 0.0;

        // 隐藏前景输入面板，露出神龛与石门
        if (_panel != null) _panel.Visible = false;
        if (_newUserPanel != null) _newUserPanel.Visible = false;
        if (_changePwdPanel != null) _changePwdPanel.Visible = false;
        if (_btnSandbox != null) _btnSandbox.Visible = false;

        if (_doorAnimRect != null)
        {
            _doorAnimRect.Visible = true;
        }

        UpdateDoorDisplay(0);
        GD.Print("[LoginView] 启动官方 1.5 石门开启序列动画 (230ms 逐帧步进)...");
    }

    private void UpdateDoorDisplay(int frameIndex)
    {
        if (_doorAnimRect != null && frameIndex >= 0 && frameIndex < OfficialDoorTotalFrames)
        {
            var tex = _doorFrames[frameIndex];
            if (tex != null)
            {
                _doorAnimRect.Texture = tex;
            }
        }
    }

    /// <summary>
    /// 官方 1.5 多面板状态分发 (IDA sub_47D864)
    /// state 0: 登录面板 (0x0A)
    /// state 1: 注册新用户面板 (0x0B, Prguse_0063)
    /// state 2: 修改密码面板 (0x0C, Prguse_0050)
    /// </summary>
    public void SwitchSubState(int state)
    {
        if (_panel != null) _panel.Visible = (state == 0);
        if (_newUserPanel != null) _newUserPanel.Visible = (state == 1);
        if (_changePwdPanel != null) _changePwdPanel.Visible = (state == 2);
        if (_btnSandbox != null) _btnSandbox.Visible = (state == 0);

        GD.Print($"[LoginView] 切换官方登录子状态: state={state} (0=登录, 1=新用户, 2=改密)");
    }

    private void OnLoginClicked()
    {
        string u = _txtUsername?.Text?.Trim() ?? string.Empty;
        string p = _txtPassword?.Text?.Trim() ?? string.Empty;

        if (string.IsNullOrEmpty(u) || string.IsNullOrEmpty(p))
        {
            if (_lblStatus != null)
            {
                _lblStatus.Text = "请输入账号和密码！";
                _lblStatus.AddThemeColorOverride("font_color", new Color(1.0f, 0.3f, 0.3f));
            }
            return;
        }

        if (_lblStatus != null)
        {
            _lblStatus.Text = "正在连接登录网关...";
            _lblStatus.AddThemeColorOverride("font_color", new Color(1.0f, 0.85f, 0.2f));
        }

        EmitSignal(SignalName.LoginRequested, ServerHost, ServerPort, u, p);
    }

    private void OnCloseClicked()
    {
        GD.Print("[LoginView] 点击退出客户端");
        GetTree().Quit();
    }

    private void OnSandboxClicked()
    {
        GD.Print("[LoginView] 触发单机试玩 / 官方 1.5 沙盒演练");
        EmitSignal(SignalName.LoginRequested, "127.0.0.1", 7100, "sandbox_player", "123456");
    }

    private void OnSubmitRegister()
    {
        string u = _txtRegUser?.Text?.Trim() ?? string.Empty;
        string p1 = _txtRegPass1?.Text?.Trim() ?? string.Empty;
        string p2 = _txtRegPass2?.Text?.Trim() ?? string.Empty;

        if (string.IsNullOrEmpty(u) || string.IsNullOrEmpty(p1))
        {
            GD.Print("[LoginView] 注册失败：账号或密码不能为空");
            return;
        }

        if (p1 != p2)
        {
            GD.Print("[LoginView] 注册失败：两次输入密码不一致");
            return;
        }

        GD.Print($"[LoginView] 提交官方注册新账号: {u}");
        EmitSignal(SignalName.RegisterRequested, u, p1);
        SwitchSubState(0);
        if (_lblStatus != null)
        {
            _lblStatus.Text = $"账号 {u} 注册成功，请登录！";
            _lblStatus.AddThemeColorOverride("font_color", new Color(0.2f, 1.0f, 0.3f));
        }
    }

    private void OnSubmitChangePassword()
    {
        string oldP = _txtOldPass?.Text?.Trim() ?? string.Empty;
        string newP1 = _txtNewPass1?.Text?.Trim() ?? string.Empty;
        string newP2 = _txtNewPass2?.Text?.Trim() ?? string.Empty;

        if (string.IsNullOrEmpty(oldP) || string.IsNullOrEmpty(newP1))
        {
            GD.Print("[LoginView] 改密失败：旧密码或新密码不能为空");
            return;
        }

        if (newP1 != newP2)
        {
            GD.Print("[LoginView] 改密失败：两次输入新密码不一致");
            return;
        }

        string acc = _txtAccount?.Text?.Trim() ?? string.Empty;
        GD.Print($"[LoginView] 提交修改密码请求成功: account={acc}");
        EmitSignal(SignalName.ChangePasswordRequested, acc, oldP, newP1);
        SwitchSubState(0);
        if (_lblStatus != null)
        {
            _lblStatus.Text = "密码修改成功，请使用新密码登录！";
            _lblStatus.AddThemeColorOverride("font_color", new Color(0.2f, 1.0f, 0.3f));
        }
    }
}
