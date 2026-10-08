// ============================================================================
// Mir2 客户端 - 地牢黑暗迷雾与火把/蜡烛视野光照系统 (1:1 盛大 1.5 官方实证)
// ============================================================================

using System;
using System.Collections.Generic;

namespace Mir2Client.Game
{
    public class DarknessFogManager
    {
        public static readonly string[] DarkMaps = new[]
        {
            "d401", "d402", "d717", "d601", "d024", "d1001", "d2001"
        };

        public const int BaseDarkRadius = 1;
        public const int CandleRadius = 3;
        public const int TorchRadius = 5;
        public const int DaylightRadius = 15;

        public event Action<int, string>? LightRadiusChanged;
        public event Action<int>? TorchDepleted;

        public bool IsDarkMap(string mapId)
        {
            string mLower = mapId.ToLowerInvariant();
            foreach (var dm in DarkMaps)
            {
                if (mLower == dm || mLower.StartsWith(dm))
                    return true;
            }
            return false;
        }

        public readonly struct VisionResult
        {
            public int Radius { get; init; }
            public string Source { get; init; }
            public bool IsDark { get; init; }
            public int Dura { get; init; }
        }

        public VisionResult CalculateVisionRadius(PlayerState? player, string mapId)
        {
            if (!IsDarkMap(mapId))
            {
                return new VisionResult { Radius = DaylightRadius, Source = "地表日光", IsDark = false, Dura = 0 };
            }

            if (player == null)
            {
                return new VisionResult { Radius = BaseDarkRadius, Source = "无玩家", IsDark = true, Dura = 0 };
            }

            var lightItem = player.Equipment[(int)OfficialEquipSlot.Lighting];
            if (lightItem == null || string.IsNullOrEmpty(lightItem.Name))
            {
                return new VisionResult { Radius = BaseDarkRadius, Source = "徒手摸黑", IsDark = true, Dura = 0 };
            }

            if (lightItem.Dura <= 0)
            {
                TorchDepleted?.Invoke(lightItem.Uid);
                return new VisionResult { Radius = BaseDarkRadius, Source = "火把熄灭", IsDark = true, Dura = 0 };
            }

            VisionResult res;
            if (lightItem.Name.Contains("火把"))
            {
                res = new VisionResult { Radius = TorchRadius, Source = "燃烧的火把", IsDark = true, Dura = lightItem.Dura };
            }
            else if (lightItem.Name.Contains("蜡烛"))
            {
                res = new VisionResult { Radius = CandleRadius, Source = "微弱的蜡烛", IsDark = true, Dura = lightItem.Dura };
            }
            else
            {
                res = new VisionResult { Radius = BaseDarkRadius, Source = "未知照明道具", IsDark = true, Dura = lightItem.Dura };
            }

            if (lightItem.Dura <= 0)
            {
                TorchDepleted?.Invoke(lightItem.Uid);
            }

            LightRadiusChanged?.Invoke(res.Radius, res.Source);
            return res;
        }
    }
}
