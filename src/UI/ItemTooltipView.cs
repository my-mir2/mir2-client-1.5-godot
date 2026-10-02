using System;
using Godot;

namespace Mir2Client.UI;

/// <summary>
/// 盛大官方 1.5 物品信息浮动提示框 (纯 C# 实现)
/// 黑底金边自适应尺寸与 BBCode 极品属性高亮渲染
/// </summary>
public partial class ItemTooltipView : PanelContainer
{
    private RichTextLabel? _lblContent;

    public override void _Ready()
    {
        Visible = false;
        MouseFilter = MouseFilterEnum.Ignore;
        CustomMinimumSize = new Vector2(160, 60);
        ZIndex = 150;

        var sb = new StyleBoxFlat
        {
            BgColor = new Color(0.05f, 0.05f, 0.07f, 0.88f),
            BorderColor = new Color(0.5f, 0.42f, 0.28f, 0.95f),
            BorderWidthLeft = 1,
            BorderWidthTop = 1,
            BorderWidthRight = 1,
            BorderWidthBottom = 1,
            ContentMarginLeft = 6.0f,
            ContentMarginTop = 6.0f,
            ContentMarginRight = 6.0f,
            ContentMarginBottom = 6.0f
        };
        AddThemeStyleboxOverride("panel", sb);

        _lblContent = new RichTextLabel
        {
            BbcodeEnabled = true,
            FitContent = true,
            ScrollActive = false,
            CustomMinimumSize = new Vector2(160, 60),
            MouseFilter = MouseFilterEnum.Ignore
        };
        _lblContent.AddThemeFontSizeOverride("normal_font_size", 12);
        AddChild(_lblContent);
    }

    public void ShowTooltip(string itemName, string desc, Vector2 globalMousePos)
    {
        if (_lblContent != null)
        {
            _lblContent.Text = $"[b][color=#ffff40]{itemName}[/color][/b]\n{desc}";
        }

        GlobalPosition = new Vector2(
            Math.Min(globalMousePos.X + 15, 800 - Size.X - 10),
            Math.Min(globalMousePos.Y + 15, 600 - Size.Y - 10)
        );
        Visible = true;
    }

    public void HideTooltip()
    {
        Visible = false;
    }
}
