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
        string[] candidates =
        [
            ProjectSettings.GlobalizePath("res://" + clean),
            Path.Combine("/Users/mac/Documents/github/mir2-client1.5-delphi", clean),
            Path.Combine(AppContext.BaseDirectory, clean),
            Path.Combine(AppContext.BaseDirectory, "../Resources", clean),
            Path.Combine(AppContext.BaseDirectory, "../Resources/resources", clean.StartsWith("resources/") ? clean[10..] : clean),
            clean
        ];

        foreach (var c in candidates)
        {
            if (File.Exists(c) || Directory.Exists(c))
            {
                return c;
            }
        }

        return rel;
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
