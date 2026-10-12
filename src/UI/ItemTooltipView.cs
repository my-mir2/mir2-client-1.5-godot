using System;
using Godot;

namespace Mir2Client.UI;

/// <summary>
/// 盛大官方 1.5 物品信息浮动提示框 (纯 C# 实现)
/// 严格对齐官方 IDA sub_48C3C8 & sub_488AF4 物品属性文字颜色与排版：
/// 1. 标题金黄色 [color=#ffff40]
/// 2. 基础属性浅灰色 [color=#c0c0c0]
/// 3. 极品加成属性鲜黄色/特殊高亮 [color=#00ffff] / [color=#ffff00]
/// 4. 黑底金边半透明自适应悬浮框，严格依据内容动态贴合，消除底部多余大黑块
/// </summary>
public partial class ItemTooltipView : Control
{
    private Panel? _panel;
    private RichTextLabel? _lblContent;

    public override void _Ready()
    {
        Visible = false;
        MouseFilter = MouseFilterEnum.Ignore;
        ZIndex = 150;

        _panel = new Panel
        {
            Name = "Panel",
            MouseFilter = MouseFilterEnum.Ignore
        };

        var sb = new StyleBoxFlat
        {
            BgColor = new Color(0.02f, 0.02f, 0.03f, 0.94f),
            BorderColor = new Color(0.65f, 0.52f, 0.28f, 0.95f),
            BorderWidthLeft = 1,
            BorderWidthTop = 1,
            BorderWidthRight = 1,
            BorderWidthBottom = 1,
            ContentMarginLeft = 8.0f,
            ContentMarginTop = 6.0f,
            ContentMarginRight = 8.0f,
            ContentMarginBottom = 6.0f
        };
        _panel.AddThemeStyleboxOverride("panel", sb);
        AddChild(_panel);

        _lblContent = new RichTextLabel
        {
            Name = "Content",
            BbcodeEnabled = true,
            FitContent = true,
            ScrollActive = false,
            AutowrapMode = TextServer.AutowrapMode.Off,
            MouseFilter = MouseFilterEnum.Ignore,
            Position = new Vector2(8, 6)
        };
        _lblContent.AddThemeFontSizeOverride("normal_font_size", 12);
        _lblContent.AddThemeConstantOverride("line_separation", 2);
        _panel.AddChild(_lblContent);
    }

    public int CompareSlotIndex { get; private set; } = 0; // 盛大官方 1.5: 0=左槽位, 1=右槽位 (按 Alt 切换)

    /// <summary>
    /// 盛大官方 1.5 IDA sub_48C3C8: 极品追加属性天蓝色高亮 [color=#00ffff]
    /// </summary>
    public static string HighlightSuperAttributes(string desc)
    {
        if (string.IsNullOrEmpty(desc)) return string.Empty;
        var lines = desc.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            if (line.Contains("(+") || line.Contains("极品") || line.Contains("幸运 +") || line.Contains("诅咒 +"))
            {
                int idx = line.IndexOf("(+");
                if (idx >= 0)
                {
                    string basePart = line.Substring(0, idx);
                    string plusPart = line.Substring(idx);
                    lines[i] = $"{basePart}[color=#00ffff]{plusPart}[/color]";
                }
                else
                {
                    lines[i] = $"[color=#00ffff]{line}[/color]";
                }
            }
        }
        return string.Join("\n", lines);
    }

    public void ToggleCompareSlot()
    {
        CompareSlotIndex = 1 - CompareSlotIndex;
    }

    public void ShowTooltip(string itemName, string desc, Vector2 globalMousePos)
    {
        string highlightedDesc = HighlightSuperAttributes(desc);
        if (_lblContent != null)
        {
            _lblContent.Text = $"[b][color=#ffff40]{itemName}[/color][/b]\n{highlightedDesc}";
        }

        // 计算文本行数以动态适配精确面板高度
        int lines = 1;
        if (!string.IsNullOrEmpty(highlightedDesc))
        {
            var split = highlightedDesc.Split('\n');
            lines += split.Length;
        }

        // 每行字体 12px + 2px 行距约 18px，内边距上下各 8px + 底部留白 12px
        float targetWidth = 175f;
        float targetHeight = 16f + (lines * 18f) + 12f;

        UpdateBounds(targetWidth, targetHeight, globalMousePos);
        Visible = true;
    }

    /// <summary>
    /// 装备对比浮动提示框：展示新装备属性并与当前已穿戴同槽位装备对比 (支持 Alt 键切换左右手镯/戒指)
    /// </summary>
    public void ShowComparisonTooltip(string currentEquipName, string currentDesc, string newEquipName, string newDesc, Vector2 globalMousePos)
    {
        string hCurrent = HighlightSuperAttributes(currentDesc);
        string hNew = HighlightSuperAttributes(newDesc);
        string slotTag = CompareSlotIndex == 0 ? "[对比-左]" : "[对比-右]";
        string fullText = $"[b][color=#ffff40]{newEquipName}[/color][/b] [color=#00ffff]{slotTag}[/color]\n{hNew}\n[color=#808080]------------------------[/color]\n[color=#a0a0a0]已装备: [/color][b][color=#ffff40]{currentEquipName}[/color][/b]\n{hCurrent}";
        if (_lblContent != null)
        {
            _lblContent.Text = fullText;
        }

        var split = fullText.Split('\n');
        int lines = split.Length;

        // 对比模式包含对比标记与分隔线，宽度拓宽至 195f，高度每行 18px 留足充裕空间 (底边安全余量 24px)
        float targetWidth = 195f;
        float targetHeight = 16f + (lines * 18f) + 24f;

        UpdateBounds(targetWidth, targetHeight, globalMousePos);
        Visible = true;
    }

    private void UpdateBounds(float targetWidth, float targetHeight, Vector2 globalMousePos)
    {
        Size = new Vector2(targetWidth, targetHeight);
        CustomMinimumSize = Size;
        if (_panel != null)
        {
            _panel.Position = Vector2.Zero;
            _panel.Size = Size;
        }
        if (_lblContent != null)
        {
            _lblContent.Position = new Vector2(8, 6);
            _lblContent.Size = new Vector2(targetWidth - 16, targetHeight - 14);
        }

        // 依据屏幕宽高限制边界与智能翻转，避免遮挡悬停图标与超出 800x600 视口
        float x = globalMousePos.X + 15;
        if (x + targetWidth > 795)
        {
            x = globalMousePos.X - targetWidth - 8;
        }
        x = Math.Clamp(x, 5, 800 - targetWidth - 5);

        float y = globalMousePos.Y + 15;
        if (y + targetHeight > 595)
        {
            y = globalMousePos.Y - targetHeight - 8;
        }
        y = Math.Clamp(y, 5, 600 - targetHeight - 5);

        GlobalPosition = new Vector2(x, y);
    }

    public void HideTooltip()
    {
        Visible = false;
    }
}

