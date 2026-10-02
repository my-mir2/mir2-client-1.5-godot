using System;
using Godot;

namespace Mir2Client.Game;

/// <summary>
/// 盛大热血传奇 1.5 环境光照、地牢幽暗氛围与蜡烛火把视界独立管理器 (纯 C# 实现)
/// </summary>
public static class EnvironmentLightingManager
{
    public static readonly Color DungeonDarkColor = new(0.42f, 0.42f, 0.52f, 1.0f);
    public static readonly Color TorchLitColor = new(1.2f, 1.15f, 0.9f, 1.0f);
    public static readonly Color OutdoorDayColor = new(1.0f, 1.0f, 1.0f, 1.0f);

    public static bool IsDungeonMap(string mapName)
    {
        if (string.IsNullOrEmpty(mapName)) return false;
        string m = mapName.ToUpperInvariant();
        return m.StartsWith('D') ||
               m.Contains("洞") || m.Contains("殿") || m.Contains("寺庙") ||
               m.Contains("矿") || m.Contains("墓") || m.Contains("巢穴") ||
               m.Contains("峡谷") || m.Contains("暗殿") || m.Contains("陵") || m.Contains("窟");
    }

    public static Color CalculateAmbientColor(string mapName, bool hasTorch, bool noCandleMode)
    {
        if (noCandleMode) return OutdoorDayColor;
        if (!IsDungeonMap(mapName)) return OutdoorDayColor;

        return hasTorch ? TorchLitColor : DungeonDarkColor;
    }
}
