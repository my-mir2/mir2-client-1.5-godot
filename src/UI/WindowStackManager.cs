using System;
using System.Collections.Generic;

namespace Mir2Client.UI;

public interface IManagedWindow
{
    string WindowName { get; }
    Official24DialogCompendium.DialogId DialogId => Official24DialogCompendium.DialogId.LoginMain;
    bool IsVisible { get; set; }
    int ZIndex { get; set; }
    (int x, int y, int width, int height) Bounds { get; }
}

/// <summary>
/// 盛大官方 1.5 原版多窗口 LIFO ESC 关闭栈、Z-Index 重排与鼠标物理防穿透管理器
/// </summary>
public sealed class WindowStackManager
{
    private readonly List<IManagedWindow> _stack = new();

    public int Count => _stack.Count;
    public IReadOnlyList<IManagedWindow> Stack => _stack;

    public void PushWindow(IManagedWindow win)
    {
        if (win == null) return;

        // 依据官方 24 原生窗体大账本自动处理互斥窗体关闭
        var def = Official24DialogCompendium.Get(win.DialogId);
        if (def != null && def.MutexWindows.Length > 0)
        {
            for (int i = _stack.Count - 1; i >= 0; i--)
            {
                var existing = _stack[i];
                if (Array.IndexOf(def.MutexWindows, existing.DialogId) >= 0)
                {
                    PopWindow(existing);
                }
            }
        }

        _stack.Remove(win);
        _stack.Add(win);
        win.IsVisible = true;

        // 重新排布 ZIndex (10 + idx)
        for (int i = 0; i < _stack.Count; i++)
        {
            _stack[i].ZIndex = 10 + i;
        }
    }

    public void PopWindow(IManagedWindow win)
    {
        if (win == null) return;
        _stack.Remove(win);
        win.IsVisible = false;

        for (int i = 0; i < _stack.Count; i++)
        {
            _stack[i].ZIndex = 10 + i;
        }
    }

    public IManagedWindow? CloseTopDialog()
    {
        if (_stack.Count == 0) return null;
        var top = _stack[^1];
        PopWindow(top);
        return top;
    }

    public void ToggleWindow(IManagedWindow win)
    {
        if (win.IsVisible)
        {
            PopWindow(win);
        }
        else
        {
            PushWindow(win);
        }
    }

    /// <summary>
    /// 物理鼠标防穿透判定 (底栏 y >= 430 以及所有可见窗口包围盒)
    /// </summary>
    public bool IsMouseOverUi(int mouseX, int mouseY)
    {
        // 官方 HUD 底栏严格占据 y >= 430
        if (mouseY >= 430) return true;

        for (int i = 0; i < _stack.Count; i++)
        {
            var win = _stack[i];
            if (!win.IsVisible) continue;

            var (x, y, w, h) = win.Bounds;
            if (mouseX >= x && mouseX <= x + w && mouseY >= y && mouseY <= y + h)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 盛大官方 1.5 角色位移时自动关闭可中断窗体 (交易、仓库、商店、NPC 对话框等)
    /// </summary>
    public void CloseWindowsOnMovement()
    {
        for (int i = _stack.Count - 1; i >= 0; i--)
        {
            var win = _stack[i];
            var def = Official24DialogCompendium.Get(win.DialogId);
            if ((def != null && def.ClosesOnMove) || win.WindowName is "Trade" or "Storage" or "Shop" or "Repair" or "NpcDialog")
            {
                PopWindow(win);
            }
        }
    }
}

