using System;
using Mir2.Server.Maps;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 传送卷轴权威位移引擎 (TeleportScrollEngine)
/// 严格 1:1 对齐官方 M2Server 瞬移机制:
/// 1. 回城卷 (TownTeleport): 瞬移至当前所属安全区中心 (比奇: 330, 270; 盟重: 330, 330)
/// 2. 随机传送卷 (RandomTeleport): 在当前地图寻址 10 次寻找可行走合法网格瞬间闪烁，避免卡墙
/// 3. 地牢逃脱卷 (DungeonEscape): 从地下矿洞、祖玛寺庙等洞穴地图直接拉脱至地表主城入口
/// </summary>
public sealed class TeleportScrollEngine
{
    public static (bool success, int targetMapId, int targetX, int targetY, string message) UseTownTeleport(
        ClientSession session, MapManager mapMgr, SabukSiegeEngine? sabukEngine = null, string? playerGuild = null)
    {
        if (session.State == null || session.State.Hp <= 0)
        {
            return (false, session.CurrentMapId, session.X, session.Y, "死亡状态下无法使用回城卷。");
        }

        // 盛大官方 1.4/1.5 规则: 沙巴克攻城期间，守城行会成员在盟重或沙巴克皇宫使用回城卷直达沙巴克城堡
        if (sabukEngine != null && sabukEngine.Phase == SabukSiegeEngine.WarPhase.InWar)
        {
            if (session.CurrentMapId == SabukSiegeEngine.SabukCastleMapId || session.CurrentMapId == SabukSiegeEngine.SabukPalaceMapId)
            {
                if (!string.IsNullOrWhiteSpace(playerGuild) &&
                    playerGuild.Equals(sabukEngine.OwnerGuild, StringComparison.OrdinalIgnoreCase))
                {
                    return (true, SabukSiegeEngine.SabukCastleMapId, 644, 290, "返回沙巴克城池阵地。");
                }
            }
        }

        // 根据当前地图决定回城目的地:
        // 地图 0 (比奇省周边) -> 回比奇安全区 (330, 270)
        // 其余地图 (盟重/白日门/毒蛇/沃玛) -> 回盟重土城 (330, 330)
        int targetMap = session.CurrentMapId == 0 ? 0 : 3;
        int targetX = targetMap == 0 ? 330 : 330;
        int targetY = targetMap == 0 ? 270 : 330;

        return (true, targetMap, targetX, targetY, string.Empty);
    }

    public static (bool success, int targetX, int targetY, string message) UseRandomTeleport(
        ClientSession session, GameMap currentMap, Random rng)
    {
        if (session.State == null || session.State.Hp <= 0)
        {
            return (false, session.X, session.Y, "死亡状态下无法使用随机传送卷。");
        }

        // 尝试最多 10 次随机寻址当前地图的可行走坐标
        for (int i = 0; i < 10; i++)
        {
            int rx = rng.Next(50, Math.Max(51, currentMap.Width - 50));
            int ry = rng.Next(50, Math.Max(51, currentMap.Height - 50));

            if (currentMap.IsWalkable(rx, ry))
            {
                return (true, rx, ry, string.Empty);
            }
        }

        return (false, session.X, session.Y, "传送失败。");
    }

    public static (bool success, int targetMapId, int targetX, int targetY, string message) UseDungeonEscape(
        ClientSession session)
    {
        if (session.State == null || session.State.Hp <= 0)
        {
            return (false, session.CurrentMapId, session.X, session.Y, "死亡状态下无法使用地牢逃脱卷。");
        }

        // 地牢逃脱卷强制回比奇地表大地图
        return (true, 0, 330, 270, string.Empty);
    }
}
