using System;
using Godot;
using Mir2Client.Utils;

namespace Mir2Client.UI;

/// <summary>
/// 盛大热血传奇 1.5 官方通用模态确认框 (DMsgDlg, 纯 C# 实现)
/// 严格对齐官方 IDA sub_486DF0 / sub_461280 (Prguse_0360 452x179)
/// </summary>
public partial class MessageBoxDialog : Control, IManagedWindow
{
    [Signal]
    public delegate void ConfirmedEventHandler();

    [Signal]
    public delegate void CancelledEventHandler();

    [Signal]
    public delegate void InputConfirmedEventHandler(string inputVal);

    public const float PanelWidth = 452.0f;
    public const float PanelHeight = 179.0f;

    private Label? _lblMsg;
    private LineEdit? _txtInput;
    private TextureButton? _btnOk;
    private TextureButton? _btnCancel;
    private Action<string>? _inputCallback;
    private Action? _confirmCallback;
    private bool _isInputMode = false;
    private bool _isNumericMode = false;
    private int _maxNumericValue = int.MaxValue;
    private Action<int>? _numericCallback;
    private int _lockoutSeconds = 0;
    private float _lockoutTimer = 0f;

    public string WindowName => "MessageBoxDialog";
    public Official24DialogCompendium.DialogId DialogId => Official24DialogCompendium.DialogId.ModalMessageBox;
    public new bool IsVisible
    {
        get => Visible;
        set => Visible = value;
    }
    public (int x, int y, int width, int height) Bounds =>
        ((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

    public override void _Ready()
    {
        Name = "MessageBoxDialog";
        Visible = false;
        ZIndex = 100;

        CustomMinimumSize = new Vector2(PanelWidth, PanelHeight);
        Size = new Vector2(PanelWidth, PanelHeight);
        Position = new Vector2((800.0f - PanelWidth) / 2.0f, (600.0f - PanelHeight) / 2.0f);
        MouseFilter = MouseFilterEnum.Stop;

        var panel = new Panel
        {
            Name = "Panel",
            Size = new Vector2(PanelWidth, PanelHeight),
            MouseFilter = MouseFilterEnum.Pass
        };

        var texBg = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0360.png");
        if (texBg != null)
        {
            var sb = new StyleBoxTexture { Texture = texBg };
            panel.AddThemeStyleboxOverride("panel", sb);
        }
        AddChild(panel);

        _lblMsg = new Label
        {
            Position = new Vector2(20, 20),
            Size = new Vector2(412, 60),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            AutowrapMode = TextServer.AutowrapMode.Arbitrary
        };
        _lblMsg.AddThemeColorOverride("font_color", new Color(1.0f, 0.9f, 0.6f));
        _lblMsg.AddThemeFontSizeOverride("font_size", 12);
        panel.AddChild(_lblMsg);

        // 官方 1.5 模态输入框 (IDA sub_486DF0 @ 0x4872f0: Width=452-70=382, Height=16, Left=35, Top=75)
        // 官方 1.5 模态输入框 (IDA sub_486DF0 @ 0x4872f0: Width=452-70=382, Height=16, Left=35, Top=75)
        var styleBoxInput = new StyleBoxFlat
        {
            BgColor = new Color(0.04f, 0.04f, 0.04f, 0.95f),
            BorderWidthLeft = 1,
            BorderWidthTop = 1,
            BorderWidthRight = 1,
            BorderWidthBottom = 1,
            BorderColor = new Color(0.35f, 0.3f, 0.22f, 0.8f),
            ContentMarginLeft = 6.0f,
            ContentMarginTop = 1.0f,
            ContentMarginRight = 6.0f,
            ContentMarginBottom = 1.0f
        };
        _txtInput = new LineEdit
        {
            Name = "TxtInput",
            Visible = false
        };
        _txtInput.AddThemeStyleboxOverride("normal", styleBoxInput);
        _txtInput.AddThemeStyleboxOverride("focus", styleBoxInput);
        _txtInput.AddThemeStyleboxOverride("read_only", styleBoxInput);
        _txtInput.AddThemeFontSizeOverride("font_size", 11);
        _txtInput.AddThemeColorOverride("font_color", new Color(1, 1, 1, 1));
        _txtInput.AddThemeColorOverride("selection_color", new Color(0.35f, 0.25f, 0.15f, 0.8f));
        _txtInput.Notification((int)Control.NotificationThemeChanged);
        _txtInput.OffsetLeft = 35.0f;
        _txtInput.OffsetTop = 75.0f;
        _txtInput.OffsetRight = 35.0f + 382.0f;
        _txtInput.OffsetBottom = 75.0f + 16.0f;
        _txtInput.CustomMinimumSize = new Vector2(382, 16);
        _txtInput.Size = new Vector2(382, 16);
        _txtInput.TextSubmitted += (val) => OnOkPressed();
        panel.AddChild(_txtInput);

        // 官方原生确定按钮 (Prguse_0361 @ 130, 126, IDA sub_481DD4 [0x481f05..0x481fb1] & sub_486DF0)
        _btnOk = new TextureButton
        {
            Position = new Vector2(130, 126),
            Size = new Vector2(80, 34)
        };
        var texOk = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0361.png");
        if (texOk != null) _btnOk.TextureNormal = texOk;
        _btnOk.Pressed += OnOkPressed;
        panel.AddChild(_btnOk);

        // 官方原生取消按钮 (Prguse_0365 @ 242, 126, IDA sub_481DD4 [0x481f45..0x481fb1] & sub_486DF0)
        _btnCancel = new TextureButton
        {
            Position = new Vector2(242, 126),
            Size = new Vector2(80, 34)
        };
        var texCancel = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0365.png");
        if (texCancel != null) _btnCancel.TextureNormal = texCancel;
        _btnCancel.Pressed += OnCancelPressed;
        panel.AddChild(_btnCancel);
    }

    /// <summary>
    /// 盛大官方 1.5 Delphi 客户端弹窗换行符净化 (IDA 0x46AFDC show_dialog_message)
    /// 原版 Delphi 代码中使用字符 '\' 作为换行标记，在此统一映射为标准换行符 '\n'
    /// </summary>
    public static string NormalizeOfficialDialogText(string text)
    {
        if (string.IsNullOrEmpty(text)) return string.Empty;
        return text.Replace("\\\\", "\n").Replace('\\', '\n');
    }

    public void ShowMessage(string message, bool hasCancel = true)
    {
        ZIndex = 200;
        _isInputMode = false;
        _inputCallback = null;
        if (_lblMsg != null)
        {
            _lblMsg.Text = NormalizeOfficialDialogText(message);
            _lblMsg.Size = new Vector2(400, 80);
        }
        if (_txtInput != null) _txtInput.Visible = false;
        if (_btnCancel != null) _btnCancel.Visible = hasCancel;
        if (_btnOk != null)
        {
            _btnOk.Position = hasCancel ? new Vector2(130, 126) : new Vector2(186, 126);
        }
        Visible = true;
    }

    public void ShowMessage(string message, bool hasCancel, Action? onConfirm)
    {
        _confirmCallback = onConfirm;
        ShowMessage(message, hasCancel);
    }

    public void ShowInput(string prompt, string defaultValue = "", Action<string>? onConfirm = null, bool isPassword = false)
    {
        ZIndex = 200;
        _isInputMode = true;
        _inputCallback = onConfirm;
        if (_lblMsg != null)
        {
            _lblMsg.Text = prompt;
            _lblMsg.Size = new Vector2(400, 45);
        }
        if (_txtInput != null)
        {
            _txtInput.Text = defaultValue;
            _txtInput.Secret = isPassword;
            if (isPassword)
            {
                _txtInput.SecretCharacter = "*";
            }
            _txtInput.Visible = true;
            _txtInput.OffsetTop = 75.0f;
            _txtInput.OffsetBottom = 75.0f + 16.0f;
            _txtInput.Size = new Vector2(382, 16);
            _txtInput.GrabFocus();
            _txtInput.Deselect();
            _txtInput.CaretColumn = _txtInput.Text.Length;
        }
        if (_btnCancel != null) _btnCancel.Visible = true;
        if (_btnOk != null) _btnOk.Position = new Vector2(130, 126);
        Visible = true;
    }

    public void ShowNumericInput(string prompt, int maxVal, int defaultVal = 100, Action<int>? onConfirm = null)
    {
        _isNumericMode = true;
        _maxNumericValue = Math.Max(1, maxVal);
        _numericCallback = onConfirm;
        int initial = Math.Clamp(defaultVal, 1, _maxNumericValue);
        ShowInput(prompt, initial.ToString(), (val) =>
        {
            if (int.TryParse(val.Trim(), out int res))
            {
                int clamped = Math.Clamp(res, 1, _maxNumericValue);
                _numericCallback?.Invoke(clamped);
            }
        });
        _txtInput?.SelectAll();
    }

    /// <summary>
    /// 盛大官方 1.5 二级密码输错 3 次锁定状态倒计时 (IDA sub_487420)
    /// </summary>
    public void ShowLockout(int seconds)
    {
        _lockoutSeconds = seconds;
        _lockoutTimer = 1.0f;
        ShowMessage($"二级密码输入错误已达 3 次！账户已临时安全锁定，请等待 {_lockoutSeconds} 秒后再试...", hasCancel: false);
        if (_btnOk != null) _btnOk.Disabled = true;
    }

    public override void _Process(double delta)
    {
        if (Visible && _lockoutSeconds > 0)
        {
            _lockoutTimer -= (float)delta;
            if (_lockoutTimer <= 0f)
            {
                _lockoutTimer = 1.0f;
                _lockoutSeconds--;
                if (_lockoutSeconds > 0)
                {
                    if (_lblMsg != null)
                    {
                        _lblMsg.Text = $"二级密码输入错误已达 3 次！账户已临时安全锁定，请等待 {_lockoutSeconds} 秒后再试...";
                    }
                }
                else
                {
                    if (_lblMsg != null)
                    {
                        _lblMsg.Text = "安全锁定已解除，请重新输入二级密码。";
                    }
                    if (_btnOk != null) _btnOk.Disabled = false;
                }
            }
        }
    }

    private void OnOkPressed()
    {
        if (_lockoutSeconds > 0) return;
        Visible = false;
        if (_isInputMode)
        {
            string val = _txtInput?.Text ?? string.Empty;
            _inputCallback?.Invoke(val);
            EmitSignal(SignalName.InputConfirmed, val);
        }
        else
        {
            _confirmCallback?.Invoke();
            _confirmCallback = null;
            EmitSignal(SignalName.Confirmed);
        }
    }

    private void OnCancelPressed()
    {
        if (_lockoutSeconds > 0) return;
        Visible = false;
        EmitSignal(SignalName.Cancelled);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (!Visible) return;
        if (@event is InputEventKey key && key.Pressed && !key.Echo)
        {
            if (key.Keycode == Key.Enter)
            {
                OnOkPressed();
                GetViewport().SetInputAsHandled();
            }
            else if (key.Keycode == Key.Escape)
            {
                OnCancelPressed();
                GetViewport().SetInputAsHandled();
            }
        }
    }
}
