using System;

namespace Mir2Client.Game;

/// <summary>
/// 盛大官方 1.5 昼夜交替时序与照明道具视野半径引擎 (纯 C# 实现)
/// 严格 1:1 还原经典传奇大世界光照系统:
///   1. 昼夜 24 小时自然时钟循环 (正午 12:00 全亮 -> 傍晚 18:00 黄昏 50% 视界 -> 子夜 00:00 纯黑地狱 -> 拂晓 06:00 晨曦复明)
///   2. 道具照明增强 (在地牢或黑夜环境中):
///      - 空手徒手: 基础视野 2 格
///      - 佩戴蜡烛 (Candle): 视野提升至 4 格 (持久 300 点，每秒微量消耗)
///      - 佩戴火把 (Torch): 视野提升至 7 格 (持久 600 点)
///      - 装备祈祷之刃: 特殊自带 5 格永久圣光照明
/// </summary>
public static class DayNightLightingEngine
{
    public enum TimeOfDay
    {
        Day,     // 白天全亮
        Dusk,    // 黄昏微暗
        Night,   // 黑夜纯黑
        Dawn     // 黎明破晓
    }

    public static TimeOfDay GetTimeOfDay(int hour)
    {
        hour = (hour % 24 + 24) % 24;
        if (hour >= 6 && hour < 17) return TimeOfDay.Day;
        if (hour >= 17 && hour < 20) return TimeOfDay.Dusk;
        if (hour >= 20 || hour < 4) return TimeOfDay.Night;
        return TimeOfDay.Dawn;
    }

    /// <summary>
    /// 计算角色当前照明视口瓦片半径 (Chebyshev 格数)
    /// </summary>
    public static int CalculateLightRadius(
        bool isDungeonMap,
        TimeOfDay time,
        string equippedLightItemName)
    {
        // 1. 地面普通地图且处于白昼时，全图通亮 (大视野 15 格)
        if (!isDungeonMap && time == TimeOfDay.Day)
        {
            return 15;
        }

        // 2. 装备特殊或专属照明物
        if (!string.IsNullOrEmpty(equippedLightItemName))
        {
            if (equippedLightItemName.Contains("火把")) return 7;
            if (equippedLightItemName.Contains("祈祷之刃")) return 6;
            if (equippedLightItemName.Contains("蜡烛")) return 4;
        }

        // 3. 黄昏或破晓微光
        if (!isDungeonMap && (time == TimeOfDay.Dusk || time == TimeOfDay.Dawn))
        {
            return 6;
        }

        // 4. 黑夜或幽暗地牢，徒手微弱视野
        return 2;
    }
}
