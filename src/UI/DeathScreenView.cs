using System;
using Godot;
using Mir2Client.Utils;

namespace Mir2Client.UI;

/// <summary>
/// 盛大热血传奇 1.5 死亡灰屏与回城复活面板 (纯 C# 场景驱动控制器)
/// 严格 1:1 绑定 scenes/Game.tscn 中 HUD/DeathScreen 场景树原生节点
/// 贴图基准：Prguse.wil[360] 面板 + [361] 原地复活 + [365] 回城复活 (IDA sub_481DD4 / sub_486DF0)
/// </summary>
public partial class DeathScreenView : Control, IManagedWindow
{
    [Signal]
    public delegate void ReviveAtStartRequestedEventHandler();

    [Signal]
    public delegate void ExitGameRequestedEventHandler();

    public const float PanelWidth = 452.0f;
    public const float PanelHeight = 179.0f;

    private Panel? _panel;
    private Label? _lblMsg;

    public string WindowName => "DeathScreenView";
    public new bool IsVisible
    {
        get => Visible;
        set => Visible = value;
    }
    public (int x, int y, int width, int height) Bounds =>
        ((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

    private ColorRect? _grayscaleOverlay;
    private TextureRect? _bloodDropRect;
    private ColorRect? _vignetteOverlay;
    private double _animTimer = 0.0;
    private int _bloodFrame = 0;
    private readonly Texture2D?[] _bloodFrames = new Texture2D?[11];

    public override void _Ready()
    {
        Visible = false;
        ZIndex = 200;

        // 盛大原版死亡全屏灰度遮罩后处理 (800x600, 挂载 grayscale_death.gdshader)
        var shader = GD.Load<Shader>("res://shaders/grayscale_death.gdshader");
        var shaderMat = new ShaderMaterial { Shader = shader };
        shaderMat.SetShaderParameter("gray_amount", 1.0f);

        _grayscaleOverlay = new ColorRect
        {
            Name = "GrayscaleOverlay",
            Size = new Vector2(800, 600),
            Position = Vector2.Zero,
            MouseFilter = MouseFilterEnum.Ignore,
            Material = shaderMat
        };
        AddChild(_grayscaleOverlay);

        // 死亡暗红晕影边缘 (Vignette)
        _vignetteOverlay = new ColorRect
        {
            Name = "VignetteOverlay",
            Size = new Vector2(800, 600),
            Position = Vector2.Zero,
            MouseFilter = MouseFilterEnum.Ignore,
            Color = new Color(0.4f, 0.05f, 0.05f, 0.25f)
        };
        AddChild(_vignetteOverlay);

        // 预加载官方经典血滴动画帧 (Prguse_0380 ~ 0390)
        for (int i = 0; i < 11; i++)
        {
            int idx = 380 + i;
            _bloodFrames[i] = ResPath.LoadImageTexture($"resources/wil/png/Prguse/Prguse_{idx:D4}.png");
        }

        // 血滴/血迹动画挂载在居中显眼位置
        _bloodDropRect = new TextureRect
        {
            Name = "BloodDropRect",
            Position = new Vector2(272, 100),
            Size = new Vector2(256, 160),
            ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize,
            StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered,
            MouseFilter = MouseFilterEnum.Ignore,
            Visible = true,
            ZIndex = 2
        };
        if (_bloodFrames[0] != null) _bloodDropRect.Texture = _bloodFrames[0];
        AddChild(_bloodDropRect);

        _panel = GetNodeOrNull<Panel>("Panel");
        if (_panel != null)
        {
            _panel.ZIndex = 1;
            var texBg = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0360.png");
            if (texBg != null)
            {
                _panel.AddThemeStyleboxOverride("panel", new StyleBoxTexture { Texture = texBg });
            }

            _lblMsg = _panel.GetNodeOrNull<Label>("LblMsg");
            if (_lblMsg != null)
            {
                _lblMsg.Text = "你已经阵亡了，是否想要重新开始？";
                _lblMsg.AddThemeColorOverride("font_color", new Color(1.0f, 0.9f, 0.5f));
            }

            // 官方原生确定按钮 (Prguse_0361 凸起 / 0363 凹陷 @ 130, 126, 80x34, IDA sub_481DD4 [0x481f05..0x481fb1])
            var texOkNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0361.png");
            var texOkPressed = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0363.png");
            var btnOkTex = new TextureButton
            {
                Name = "BtnOkTex",
                Position = new Vector2(130, 126),
                Size = new Vector2(80, 34),
                TextureNormal = texOkNormal,
                TexturePressed = texOkPressed
            };
            btnOkTex.Pressed += OnRevivePressed;
            _panel.AddChild(btnOkTex);

            // 官方原生取消按钮 (Prguse_0365 凸起 / 0367 凹陷 @ 242, 126, 80x34, IDA sub_481DD4 [0x481f45..0x481fb1])
            var texCancelNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0365.png");
            var texCancelPressed = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0367.png");
            var btnCancelTex = new TextureButton
            {
                Name = "BtnCancelTex",
                Position = new Vector2(242, 126),
                Size = new Vector2(80, 34),
                TextureNormal = texCancelNormal,
                TexturePressed = texCancelPressed
            };
            btnCancelTex.Pressed += OnExitPressed;
            _panel.AddChild(btnCancelTex);
        }

        GD.Print("[DeathScreenView] 官方 1.5 死亡提示确认框与全屏灰度滤镜 (TMsgDlg 360, 确定 361/363, 取消 365/367) 绑定就绪");
    }

    public override void _Process(double delta)
    {
        if (!Visible || _bloodDropRect == null) return;

        _animTimer += delta;
        // 每 0.09 秒切换一帧血滴动画
        if (_animTimer >= 0.09)
        {
            _animTimer = 0.0;
            _bloodFrame = (_bloodFrame + 1) % 11;
            var curTex = _bloodFrames[_bloodFrame];
            if (curTex != null)
            {
                _bloodDropRect.Texture = curTex;
            }
        }
    }

    public void ShowDeath()
    {
        Visible = true;
        _bloodFrame = 0;
        _animTimer = 0.0;
        if (_bloodDropRect != null && _bloodFrames[0] != null)
        {
            _bloodDropRect.Texture = _bloodFrames[0];
        }
        if (_grayscaleOverlay?.Material is ShaderMaterial mat)
        {
            mat.SetShaderParameter("gray_amount", 1.0f);
        }
    }

    private void OnRevivePressed()
    {
        Visible = false;
        EmitSignal(SignalName.ReviveAtStartRequested);
    }

    private void OnExitPressed()
    {
        Visible = false;
        EmitSignal(SignalName.ExitGameRequested);
    }
}
