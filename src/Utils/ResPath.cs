using System;
using System.IO;
using Godot;

namespace Mir2Client.Utils;

/// <summary>
/// 跨环境统一资源路径解析器 (纯 C# 实现)
/// 支持 Godot 编辑器环境、独立发布包与当前系统绝对路径
/// </summary>
public static class ResPath
{
    public static string Resolve(string rel)
    {
        if (File.Exists(rel) || Directory.Exists(rel))
        {
            return rel;
        }

        string clean = rel.StartsWith("res://") ? rel[6..] : (rel.StartsWith('/') ? rel[1..] : rel);
        string projectRoot = FindProjectRoot();

        string candidate1 = Path.Combine(projectRoot, clean);
        if (File.Exists(candidate1) || Directory.Exists(candidate1)) return candidate1;

        string candidate2 = Path.Combine(AppContext.BaseDirectory, clean);
        if (File.Exists(candidate2) || Directory.Exists(candidate2)) return candidate2;

        string candidate3 = Path.Combine(AppContext.BaseDirectory, "../Resources", clean);
        if (File.Exists(candidate3) || Directory.Exists(candidate3)) return candidate3;

        string candidate4 = Path.Combine(AppContext.BaseDirectory, "../Resources/resources", clean.StartsWith("resources/") ? clean[10..] : clean);
        if (File.Exists(candidate4) || Directory.Exists(candidate4)) return candidate4;

        try
        {
            string globalized = ProjectSettings.GlobalizePath("res://" + clean);
            if (File.Exists(globalized) || Directory.Exists(globalized)) return globalized;
        }
        catch
        {
            // 忽略非 Godot 进程主线程环境（如 xUnit 测试运行器）中的 interop 异常
        }

        return rel;
    }

    private static string? _cachedProjectRoot;
    private static string FindProjectRoot()
    {
        if (_cachedProjectRoot != null) return _cachedProjectRoot;

        string current = AppContext.BaseDirectory;
        while (!string.IsNullOrEmpty(current))
        {
            if (File.Exists(Path.Combine(current, "project.godot")))
            {
                _cachedProjectRoot = current;
                return current;
            }
            string? parent = Directory.GetParent(current)?.FullName;
            if (parent == null || parent == current) break;
            current = parent;
        }

        current = Directory.GetCurrentDirectory();
        while (!string.IsNullOrEmpty(current))
        {
            if (File.Exists(Path.Combine(current, "project.godot")))
            {
                _cachedProjectRoot = current;
                return current;
            }
            string? parent = Directory.GetParent(current)?.FullName;
            if (parent == null || parent == current) break;
            current = parent;
        }

        _cachedProjectRoot = Directory.GetCurrentDirectory();
        return _cachedProjectRoot;
    }

    public static ImageTexture? LoadImageTexture(string rel)
    {
        string path = Resolve(rel);
        if (File.Exists(path))
        {
            try
            {
                using var img = Image.LoadFromFile(path);
                if (img != null)
                {
                    return ImageTexture.CreateFromImage(img);
                }
                else
                {
                    GD.PrintErr($"[ResPath] Image.LoadFromFile returned null for: {path}");
                }
            }
            catch (Exception ex)
            {
                GD.PrintErr($"[ResPath] Exception loading {path}: {ex.Message}");
            }
        }
        else
        {
            GD.PrintErr($"[ResPath] File not found: rel='{rel}', resolved='{path}'");
        }
        return null;
    }
}
