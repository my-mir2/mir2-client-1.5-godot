using System;
using System.Collections.Generic;
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

    private List<string> _optionActions = new();

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
            if (_lblTitle != null) _lblTitle.Visible = false; // 官方原版 sub_48EAC0 无独立 NPC 标题

            _optionsContainer = _panel.GetNodeOrNull<VBoxContainer>("Options");
            if (_optionsContainer != null) _optionsContainer.Visible = false; // 官方无独立选项容器，全由文本流统一驱动

            _lblContent = _panel.GetNodeOrNull<RichTextLabel>("Content");
            if (_lblContent != null)
            {
                // 盛大官方 1.5 NPC 对话框文本区域绝对物理坐标 (IDA sub_48EAC0 [0x48eb7a..0x48ef02]):
                // 起点 (30, 20), 宽 355, 高 145, 行高 16px
                _lblContent.Position = new Vector2(30, 20);
                _lblContent.Size = new Vector2(355, 145);
                _lblContent.BbcodeEnabled = true;
                _lblContent.ScrollActive = false;
                _lblContent.AddThemeFontSizeOverride("normal_font_size", 12);
                _lblContent.AddThemeConstantOverride("line_separation", 2); // 配合12号字体严格对齐16px行高
                _lblContent.AddThemeColorOverride("default_color", new Color(1f, 1f, 1f));
                _lblContent.MetaClicked += OnMetaClicked;
            }

            _btnClose = _panel.GetNodeOrNull<Button>("BtnClose");
            if (_btnClose != null)
            {
                _btnClose.Visible = false; // 隐藏旧节点
            }

            // 官方原生 NPC 对话框关闭按钮 (Prguse_0064 @ 399, 1, 16x23, IDA sub_481DD4 [0x484f32..0x484f65])
            // Prguse_0384 底板在 (399, 1) 处已自带烘焙凸起红叉，TextureNormal 必须为 null，按下态显示凹陷 Prguse_0064
            var texClose = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0064.png");
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

    /// <summary>
    /// 解析并展示官方 NPC 脚本对话文本 (严格 1:1 对齐 IDA sub_48EAC0 语法):
    /// 1. 换行符: '\' (ASCII 92) 或标准换行
    /// 2. 普通文本: 纯白 0xFFFFFF, 无下划线
    /// 3. 超链接段: '<' 与 '>' 包裹, 含 '/' 则前半部分为展示文本、后半部分为 Action 标签;
    ///    默认黄色 0x00FFFF (RGB: 255, 255, 0) 并带下划线; 鼠标悬停高亮
    /// </summary>
    public void ShowDialog(string content)
    {
        if (_lblContent == null) return;
        _optionActions.Clear();

        // 将文本统一按 '\' 或换行符切割
        string normalized = content.Replace("\r\n", "\\").Replace("\n", "\\");
        string[] rawLines = normalized.Split('\\');

        var sb = new System.Text.StringBuilder();
        int actionIndex = 0;

        for (int lineIdx = 0; lineIdx < rawLines.Length; lineIdx++)
        {
            if (lineIdx > 0) sb.Append("\n");
            string line = rawLines[lineIdx];
            if (string.IsNullOrEmpty(line)) continue;

            // 逐字符解析内层超链接 <.../...>
            int cursor = 0;
            while (cursor < line.Length)
            {
                int linkStart = line.IndexOf('<', cursor);
                if (linkStart == -1)
                {
                    // 剩余纯白文本 (0xFFFFFF, IDA 0x48ecfe / 0x48eefd)
                    string rest = line.Substring(cursor);
                    sb.Append("[color=#ffffff]").Append(rest).Append("[/color]");
                    break;
                }

                if (linkStart > cursor)
                {
                    string normalSegment = line.Substring(cursor, linkStart - cursor);
                    sb.Append("[color=#ffffff]").Append(normalSegment).Append("[/color]");
                }

                int linkEnd = line.IndexOf('>', linkStart);
                if (linkEnd == -1)
                {
                    // 未闭合链接，按普通白字处理
                    string rest = line.Substring(linkStart);
                    sb.Append("[color=#ffffff]").Append(rest).Append("[/color]");
                    break;
                }

                string linkContent = line.Substring(linkStart + 1, linkEnd - linkStart - 1);
                string displayText = linkContent;
                string actionTag = linkContent;

                int slashIdx = linkContent.IndexOf('/');
                if (slashIdx != -1)
                {
                    displayText = linkContent.Substring(0, slashIdx);
                    actionTag = linkContent.Substring(slashIdx + 1);
                }

                // 官方超链接样式 (IDA 0x48edb0 byte_48EF8C=4 下划线, 0x48ee50 0x00FFFF 黄色)
                _optionActions.Add(actionTag);
                sb.Append($"[url={actionIndex}][color=#ffff00][u]{displayText}[/u][/color][/url]");
                actionIndex++;

                cursor = linkEnd + 1;
            }
        }

        _lblContent.Text = sb.ToString();
        Visible = true;
    }

    public void ShowDialog(string title, string content, string[]? options = null)
    {
        // 兼容模式: 将 options 合并转化为官方标准 NPC 对话文本流
        var sb = new System.Text.StringBuilder();
        sb.Append(content);
        if (options != null && options.Length > 0)
        {
            sb.Append("\\ \\");
            for (int i = 0; i < options.Length; i++)
            {
                string opt = options[i];
                if (opt.StartsWith("<") && opt.EndsWith(">"))
                {
                    sb.Append("\\").Append(opt);
                }
                else
                {
                    sb.Append($"\\<{opt}/@{i}>");
                }
            }
        }
        ShowDialog(sb.ToString());
    }

    private void OnMetaClicked(Variant meta)
    {
        string metaStr = meta.AsString();
        if (int.TryParse(metaStr, out int idx))
        {
            EmitSignal(SignalName.OptionSelected, idx);
        }
        else
        {
            int foundIdx = _optionActions.IndexOf(metaStr);
            EmitSignal(SignalName.OptionSelected, foundIdx >= 0 ? foundIdx : 0);
        }
    }

    private void OnClose()
    {
        Visible = false;
        EmitSignal(SignalName.DialogClosed);
    }
}
