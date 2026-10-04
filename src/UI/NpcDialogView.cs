using System;
using Godot;
using Mir2Client.Utils;

namespace Mir2Client.UI;

/// <summary>
/// 盛大官方 1.5 NPC 对话交互窗口 (FrmNpcDlg, 纯 C# 场景驱动控制器)
/// 严格 1:1 绑定 scenes/NpcDialog.tscn (或 Game.tscn 中的 HUD/NpcDialog) 场景树原生节点
/// 贴图基准：Prguse.wil[384] (416x176, IDA sub_481DD4 / sub_484ee2)
/// </summary>
public partial class NpcDialogView : Control, IManagedWindow
{
    [Signal]
    public delegate void OptionSelectedEventHandler(int index);

    [Signal]
    public delegate void DialogClosedEventHandler();

    public const float PanelWidth = 416.0f;
    public const float PanelHeight = 176.0f;

    private Panel? _panel;
    private Label? _lblTitle;
    private RichTextLabel? _lblContent;
    private VBoxContainer? _optionsContainer;
    private Button? _btnClose;

    public string WindowName => "NpcDialogView";
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

        _panel = GetNodeOrNull<Panel>("Panel");
        if (_panel != null)
        {
            var texBg = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0384.png");
            if (texBg != null)
            {
                _panel.AddThemeStyleboxOverride("panel", new StyleBoxTexture { Texture = texBg });
            }

            _lblTitle = _panel.GetNodeOrNull<Label>("Title");
            _lblContent = _panel.GetNodeOrNull<RichTextLabel>("Content");
            _optionsContainer = _panel.GetNodeOrNull<VBoxContainer>("Options");
            _btnClose = _panel.GetNodeOrNull<Button>("BtnClose");

            // 官方原生 NPC 对话框关闭按钮 (Prguse_0064 @ 399, 1, 16x23, IDA sub_481DD4 [0x484f32..0x484f65])
            // Prguse_0384 底板在 (399, 1) 处已自带烘焙凸起红叉，TextureNormal 必须为 null，按下态显示凹陷 Prguse_0064
            var texClose = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0064.png");
            if (_btnClose != null)
            {
                _btnClose.Visible = false; // 隐藏旧节点
            }
            var btnCloseTex = new TextureButton
            {
                Name = "BtnNpcCloseTex",
                Position = new Vector2(399, 1),
                Size = new Vector2(16, 23),
                TextureNormal = null,
                TexturePressed = texClose
            };
            btnCloseTex.Pressed += OnClose;
            _panel.AddChild(btnCloseTex);
        }

        GD.Print("[NpcDialogView] NPC 对话框场景节点绑定就绪");
    }

    public void ShowDialog(string title, string content, string[]? options = null)
    {
        if (_lblTitle != null) _lblTitle.Text = title;
        if (_lblContent != null) _lblContent.Text = content;

        if (_optionsContainer != null)
        {
            foreach (Node child in _optionsContainer.GetChildren())
            {
                child.QueueFree();
            }

            if (options != null)
            {
                for (int i = 0; i < options.Length; i++)
                {
                    int idx = i;
                    string optText = options[i];

                    // 盛大官方 1.5 NPC 脚本交互选项 (IDA sub_484ee2 / sub_49C780):
                    // 默认暗黄带下划线，悬浮高亮纯白，具有原版经典的网页/脚本链接触感
                    var btn = new Button
                    {
                        Text = optText.StartsWith("<") ? optText : $"< {optText} >",
                        Flat = true,
                        Alignment = HorizontalAlignment.Left,
                        CustomMinimumSize = new Vector2(0, 20),
                        MouseDefaultCursorShape = CursorShape.PointingHand
                    };
                    btn.AddThemeFontSizeOverride("font_size", 11);
                    btn.AddThemeColorOverride("font_color", new Color(0.95f, 0.82f, 0.35f)); // 经典官方暗金黄
                    btn.AddThemeColorOverride("font_hover_color", new Color(1.0f, 1.0f, 1.0f)); // 悬浮高亮纯白
                    btn.AddThemeColorOverride("font_pressed_color", new Color(1.0f, 0.6f, 0.2f)); // 按下微橙

                    var btnStyle = new StyleBoxEmpty
                    {
                        ContentMarginLeft = 2.0f,
                        ContentMarginRight = 4.0f,
                        ContentMarginTop = 1.0f,
                        ContentMarginBottom = 1.0f
                    };
                    btn.AddThemeStyleboxOverride("normal", btnStyle);
                    btn.AddThemeStyleboxOverride("hover", btnStyle);
                    btn.AddThemeStyleboxOverride("pressed", btnStyle);
                    btn.AddThemeStyleboxOverride("focus", btnStyle);
                    btn.Pressed += () => OnOptionClicked(idx);
                    _optionsContainer.AddChild(btn);
                }
            }
        }

        Visible = true;
    }

    public void ShowDialog(string content)
    {
        ShowDialog("NPC", content, null);
    }

    private void OnOptionClicked(int index)
    {
        EmitSignal(SignalName.OptionSelected, index);
    }

    private void OnClose()
    {
        Visible = false;
        EmitSignal(SignalName.DialogClosed);
    }
}
