using System;
using Godot;
using Mir2Client.Utils;

namespace Mir2Client.UI;

/// <summary>
/// 盛大官方 1.5 聊天框独立组件 (纯 C# 实现)
/// 对应 Delphi 聊天输入、文字滚屏与频道分流逻辑
/// </summary>
public partial class ChatBoxView : Control
{
    [Signal]
    public delegate void MessageSubmittedEventHandler(string text);

    public const int MaxLines = 100;
    private RichTextLabel? _log;
    private LineEdit? _input;

    public override void _Ready()
    {
        _log = GetNodeOrNull<RichTextLabel>("Log");
        _input = GetNodeOrNull<LineEdit>("Input");

        if (_log != null)
        {
            if (FontManager.OfficialSongti != null)
            {
                _log.AddThemeFontOverride("normal_font", FontManager.OfficialSongti);
                _log.AddThemeFontOverride("bold_font", FontManager.OfficialSongti);
            }
            _log.AddThemeFontSizeOverride("normal_font_size", FontManager.OfficialFontSize);
            _log.AddThemeFontSizeOverride("bold_font_size", FontManager.OfficialFontSize);
        }

        if (_input != null)
        {
            if (FontManager.OfficialSongti != null)
            {
                _input.AddThemeFontOverride("font", FontManager.OfficialSongti);
            }
            _input.AddThemeFontSizeOverride("font_size", FontManager.OfficialFontSize);
            _input.TextSubmitted += OnSubmit;
            _input.FocusExited += () => _input.Visible = false;
        }
    }

    public void AddMessage(string sender, string text, Color color)
    {
        if (_log == null) return;
        string bbcode = $"[color=#{color.ToHtml(false)}]{sender}: {text}[/color]\n";
        _log.AppendText(bbcode);

        var vbar = _log.GetVScrollBar();
        if (vbar != null)
        {
            vbar.Value = vbar.MaxValue;
        }
    }

    public void OpenInput()
    {
        if (_input != null)
        {
            _input.Visible = true;
            _input.GrabFocus();
            _input.Clear();
        }
    }

    private void OnSubmit(string text)
    {
        if (_input != null) _input.Visible = false;
        if (!string.IsNullOrWhiteSpace(text))
        {
            EmitSignal(SignalName.MessageSubmitted, text);
        }
    }
}
