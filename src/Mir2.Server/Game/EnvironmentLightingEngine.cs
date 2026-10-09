using System;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 昼夜环境与蜡烛/火把照明衰减权威引擎 (EnvironmentLightingEngine)
/// 严格 1:1 对齐官方 M2Server 规则:
/// 1. 昼夜更替状态机 (Day/Night Cycle): 白昼满光照 (15), 黑夜微光 (3), 地下地牢常暗 (2)
/// 2. 装备槽位 OfficialEquipSlot.Lighting: 蜡烛 (半径 4) 与火把 (半径 7) 持久随时间流逝损耗
/// 3. 持久归 0 熄灭，并向周围广播 SM_LIGHTING (624)
/// </summary>
public sealed class EnvironmentLightingEngine
{
    public enum EnvironmentDayPhase
    {
        Day = 0,    // 白昼 (满光照)
        Dusk = 1,   // 黄昏 (过渡)
        Night = 2,  // 黑夜 (需要火把/蜡烛)
    }

    public EnvironmentDayPhase CurrentPhase { get; set; } = EnvironmentDayPhase.Day;

    /// <summary>
    /// 获取当前地图基础环境光照半径
    /// </summary>
    public int GetMapBaseLightRadius(int mapId)
    {
        // 官方地下地牢/矿洞地图 (如 1, 2, D开头的地牢) 恒定常暗
        if (mapId != 0 && mapId != 3 && mapId != 11)
        {
            return 2; // 地牢常暗视野
        }

        return CurrentPhase switch
        {
            EnvironmentDayPhase.Day => 15,
            EnvironmentDayPhase.Dusk => 8,
            EnvironmentDayPhase.Night => 3,
            _ => 15
        };
    }

    /// <summary>
    /// 计算并更新角色当前光照半径，并扣减火把/蜡烛持久 (每 10 秒调用一次)
    /// </summary>
    public (int lightRadius, bool burnedOut) UpdatePlayerLighting(PlayerState state, int mapId)
    {
        int baseRadius = GetMapBaseLightRadius(mapId);
        var torch = state.Equipment[(int)OfficialEquipSlot.Lighting];

        if (torch == null || torch.Dura <= 0)
        {
            // 未佩戴火把或已燃尽
            return (baseRadius, false);
        }

        // 官方火把/蜡烛持久随时间衰减扣减 1 点
        torch.Dura = Math.Max(0, torch.Dura - 1);
        bool burnedOut = (torch.Dura == 0);

        int torchBonus = 0;
        if (torch.Name.Contains("火把"))
        {
            torchBonus = 7;
        }
        else if (torch.Name.Contains("蜡烛"))
        {
            torchBonus = 4;
        }

        int finalRadius = Math.Max(baseRadius, torchBonus);
        return (finalRadius, burnedOut);
    }
}
