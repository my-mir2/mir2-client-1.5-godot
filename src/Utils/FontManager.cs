using System;
using Godot;

namespace Mir2Client.Utils;

/// <summary>
/// 盛大官方 1.5 客户端全局权威字体与排版管理器
/// 严格物理真值：
/// 1. 官方中文字体: 宋体 (SimSun) - 从 resources/fonts/Songti.ttc (或 .res) 加载
/// 2. 官方标准字号: 12px (Font.Height = -12, 9pt)
/// 3. 官方标准描边: 1px 纯黑描边 (OutlineSize = 1, Color = #000000)
/// 4. 艺术贴图字: 仅 HUD 等级数字 (Lv) 与快捷数字允许使用 Prguse.wil 切片 30~39
/// </summary>
public static class FontManager
{
    private static Font? _officialSongti;
    private static bool _loaded = false;

    public const string OfficialFontName = "SimSun"; // 盛大官方 1.5 核心字体宋体 (SimSun)
    public const int OfficialFontSize = 12;          // 盛大官方 1.5 核心字号 12px
    public const double OfficialFontSizeInPoints = 9.0; // 盛大官方 1.5 核心字号 9pt (Font.Height = -12)
    public const int OfficialOutlineSize = 1;       // 盛大官方 1.5 核心描边 1px

    public static Font? OfficialSongti
    {
        get
        {
            if (!_loaded)
            {
                _loaded = true;
                try
                {
                    var ff = new FontFile();
                    string ttcPath = ResourceLoader.Exists("res://resources/fonts/SimSun.ttc")
                        ? "res://resources/fonts/SimSun.ttc"
                        : "res://resources/fonts/Songti.ttc";
                    if (ff.LoadDynamicFont(ttcPath) == Error.Ok)
                    {
                        _officialSongti = ff;
                    }
                    else if (ResourceLoader.Exists("res://resources/fonts/Songti.res"))
                    {
                        _officialSongti = GD.Load<Font>("res://resources/fonts/Songti.res");
                    }

                    if (_officialSongti is FontFile fontFile)
                    {
                        // 盛大官方 1.5 权威宋体渲染: 开启 TrueType 字节码微调 (Normal Hinting) 与灰度平滑
                        // 确保 12px (9pt) 超小字号下“量”、“即”、“微”等密集汉字笔画清晰饱满，杜绝矢量挤压粘连
                        fontFile.Antialiasing = TextServer.FontAntialiasing.Gray;
                        fontFile.SubpixelPositioning = TextServer.SubpixelPositioning.Disabled;
                        fontFile.GenerateMipmaps = false;
                        fontFile.MultichannelSignedDistanceField = false;
                        fontFile.Hinting = TextServer.Hinting.Normal;
                    }
                }
                catch (Exception ex)
                {
                    GD.PrintErr($"[FontManager] 加载官方宋体失败: {ex.Message}");
                }
            }
            return _officialSongti;
        }
    }

    /// <summary>
    /// 一键将任意 Label 规范化为盛大官方 1.5 权威 12px 纯净宋体 (面板与对话框默认无 outline，杜绝笔画粘连)
    /// </summary>
    public static void ApplyOfficialStyle(Label label, Color? fontColor = null, int fontSize = OfficialFontSize, bool hasOutline = false)
    {
        if (label == null) return;

        if (OfficialSongti != null)
        {
            label.AddThemeFontOverride("font", OfficialSongti);
        }

        label.AddThemeFontSizeOverride("font_size", fontSize);

        if (fontColor.HasValue)
        {
            label.AddThemeColorOverride("font_color", fontColor.Value);
        }

        if (hasOutline)
        {
            label.AddThemeConstantOverride("outline_size", OfficialOutlineSize);
            label.AddThemeColorOverride("font_outline_color", Colors.Black);
        }
        else
        {
            label.AddThemeConstantOverride("outline_size", 0);
        }
    }

    /// <summary>
    /// 盛大官方 1.5 大世界实体/掉落物头顶名牌权威样式 (严格对齐 Delphi BoldTextOut / 1px 黑色像素投影)
    /// 彻底消除破坏 12px 宋体笔画的矢量 outline 膨胀，确保密集笔画 (如“量”、“统”、“猫”) 100% 刀刻般清晰可读！
    /// </summary>
    public static void ApplyOfficialWorldNameplateStyle(Label label, Color? fontColor = null, int fontSize = OfficialFontSize)
    {
        if (label == null) return;

        if (OfficialSongti != null)
        {
            label.AddThemeFontOverride("font", OfficialSongti);
        }

        label.AddThemeFontSizeOverride("font_size", fontSize);

        if (fontColor.HasValue)
        {
            label.AddThemeColorOverride("font_color", fontColor.Value);
        }

        // 彻底禁用 outline 轮廓膨胀
        label.AddThemeConstantOverride("outline_size", 0);

        // 盛大原版 GDI 1px 黑色微投影 (下移 1px, 右移 1px)
        label.AddThemeConstantOverride("shadow_offset_x", 1);
        label.AddThemeConstantOverride("shadow_offset_y", 1);
        label.AddThemeColorOverride("font_shadow_color", new Color(0f, 0f, 0f, 0.95f));
    }

    /// <summary>
    /// 一键将 RichTextLabel 规范化为盛大官方 1.5 权威 12px 宋体
    /// </summary>
    public static void ApplyOfficialStyleToRichText(RichTextLabel rtl, Color? defaultColor = null, int fontSize = OfficialFontSize)
    {
        if (rtl == null) return;

        if (OfficialSongti != null)
        {
            rtl.AddThemeFontOverride("normal_font", OfficialSongti);
            rtl.AddThemeFontOverride("bold_font", OfficialSongti);
            rtl.AddThemeFontOverride("italics_font", OfficialSongti);
            rtl.AddThemeFontOverride("bold_italics_font", OfficialSongti);
            rtl.AddThemeFontOverride("mono_font", OfficialSongti);
        }

        rtl.AddThemeFontSizeOverride("normal_font_size", fontSize);
        rtl.AddThemeFontSizeOverride("bold_font_size", fontSize);
        rtl.AddThemeFontSizeOverride("italics_font_size", fontSize);
        rtl.AddThemeFontSizeOverride("bold_italics_font_size", fontSize);
        rtl.AddThemeFontSizeOverride("mono_font_size", fontSize);

        if (defaultColor.HasValue)
        {
            rtl.AddThemeColorOverride("default_color", defaultColor.Value);
        }
    }

    private static Theme? _cachedTheme;

    /// <summary>
    /// 获取盛大官方 1.5 全局统一 Theme 对象 (注入宋体与 12px 基础尺寸)
    /// </summary>
    public static Theme GetOfficialTheme()
    {
        if (_cachedTheme != null) return _cachedTheme;

        var t = new Theme();
        if (OfficialSongti != null)
        {
            t.DefaultFont = OfficialSongti;
            t.DefaultFontSize = OfficialFontSize;

            t.SetFont("font", "Label", OfficialSongti);
            t.SetFontSize("font_size", "Label", OfficialFontSize);

            t.SetFont("font", "Button", OfficialSongti);
            t.SetFontSize("font_size", "Button", OfficialFontSize);

            t.SetFont("font", "LineEdit", OfficialSongti);
            t.SetFontSize("font_size", "LineEdit", OfficialFontSize);

            t.SetFont("normal_font", "RichTextLabel", OfficialSongti);
            t.SetFont("bold_font", "RichTextLabel", OfficialSongti);
            t.SetFontSize("normal_font_size", "RichTextLabel", OfficialFontSize);
            t.SetFontSize("bold_font_size", "RichTextLabel", OfficialFontSize);
        }
        _cachedTheme = t;
        return _cachedTheme;
    }

    /// <summary>
    /// 递归将指定节点树下所有 UI 控件全量应用官方宋体
    /// </summary>
    public static void ApplyRecursively(Node node)
    {
        if (node == null) return;

        if (OfficialSongti != null)
        {
            if (node is Label lbl)
            {
                lbl.AddThemeFontOverride("font", OfficialSongti);
                // 确保至少有默认 12px 字号
                if (!lbl.HasThemeFontSizeOverride("font_size"))
                {
                    lbl.AddThemeFontSizeOverride("font_size", OfficialFontSize);
                }
            }
            else if (node is Button btn)
            {
                btn.AddThemeFontOverride("font", OfficialSongti);
                if (!btn.HasThemeFontSizeOverride("font_size"))
                {
                    btn.AddThemeFontSizeOverride("font_size", OfficialFontSize);
                }
            }
            else if (node is LineEdit le)
            {
                le.AddThemeFontOverride("font", OfficialSongti);
                if (!le.HasThemeFontSizeOverride("font_size"))
                {
                    le.AddThemeFontSizeOverride("font_size", OfficialFontSize);
                }
            }
            else if (node is RichTextLabel rtl)
            {
                rtl.AddThemeFontOverride("normal_font", OfficialSongti);
                rtl.AddThemeFontOverride("bold_font", OfficialSongti);
                rtl.AddThemeFontOverride("italics_font", OfficialSongti);
                rtl.AddThemeFontOverride("bold_italics_font", OfficialSongti);
                rtl.AddThemeFontOverride("mono_font", OfficialSongti);
                if (!rtl.HasThemeFontSizeOverride("normal_font_size"))
                {
                    rtl.AddThemeFontSizeOverride("normal_font_size", OfficialFontSize);
                }
            }
        }

        foreach (Node child in node.GetChildren())
        {
            ApplyRecursively(child);
        }
    }

    private static bool _treeStylerAttached = false;

    /// <summary>
    /// 全局挂载场景树自动样式拦截器 (监听 NodeAdded 事件，任何新挂载的 UI 控件 100% 自动注入官方宋体)
    /// 彻底封死盛大官方 1.5 全生命周期 24 个原生窗体及任意动态模态框的字体退化
    /// </summary>
    public static void AttachTreeAutoStyler(SceneTree tree)
    {
        if (_treeStylerAttached || tree == null) return;
        _treeStylerAttached = true;

        tree.NodeAdded += (node) =>
        {
            if (node is Control ctrl)
            {
                ApplyRecursively(ctrl);
            }
        };
    }
}
