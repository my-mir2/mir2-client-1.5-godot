using System;
using System.Collections.Generic;
using System.Linq;
using Mir2.Server.Game;
using Mir2.Server.Maps;

namespace Mir2.Server.Persistence;

/// <summary>
/// 把 <see cref="OfficialWorldRepository.WorldData"/> (数据库真实世界) 装配进各运行时引擎。
/// 宿主服务与集成测试共用同一份装配逻辑，保证测试验证的就是生产路径。
/// </summary>
public static class OfficialWorldApplier
{
    /// <param name="activeMapProvider">当前有玩家的地图号集合 (可为 null，表示不做按图激活)。</param>
    /// <returns>预热地图集合 (出生点所在地图)。</returns>
    public static HashSet<int> Apply(OfficialWorldRepository.WorldData world, MapManager mapManager, MonsterRegenEngine regenEngine,
        NpcMerchantEngine npcEngine, SafeZoneAndGuardEngine safeZoneEngine, Func<IEnumerable<int>>? activeMapProvider,
        bool applyDropTable = true)
    {
        mapManager.ReplaceWorld(world.Maps, world.Passages);

        regenEngine.ClearRules();
        foreach (var sp in world.Spawns)
        {
            regenEngine.AddRule(new MonsterSpawnRule
            {
                RuleId = sp.RowId,
                MonsterName = string.IsNullOrEmpty(sp.NameCn) ? sp.MonsterName : sp.NameCn,
                OfficialKey = sp.OfficialKey,
                MapId = sp.MapNum,
                CenterX = sp.X,
                CenterY = sp.Y,
                Radius = sp.Scope,
                MaxCount = sp.Count,
                BaseHp = sp.Hp,
                IntervalMs = sp.IntervalMin * 60_000L
            });
        }

        // 官方安全区 (M2Server sub_4BEE20 实证): SAFE 地图整图安全 + 每个出生点 ±10 格方形；
        // 另有一处写死在二进制里的特例: 地图 "9" 的 (33,33) ±10。
        var safeRects = world.StartPoints
            .Select(sp => new SafeZoneAndGuardEngine.SafeZoneRect(sp.MapNum, sp.X - 10, sp.Y - 10, sp.X + 10, sp.Y + 10))
            .ToList();
        var map9 = world.Maps.FirstOrDefault(m => m.OfficialId == "9");
        if (map9 != null) safeRects.Add(new SafeZoneAndGuardEngine.SafeZoneRect(map9.MapId, 23, 23, 43, 43));
        safeZoneEngine.ReplaceWithOfficialSafeZones(world.Maps.Where(m => m.IsSafe).Select(m => m.MapId), safeRects);

        // 预热地图: 官方出生点所在地图始终保持刷怪，其余地图有玩家时才激活
        var prewarm = new HashSet<int>(world.StartPoints.Select(sp => sp.MapNum));
        regenEngine.ActiveMapProvider = activeMapProvider == null
            ? () => prewarm
            : () => activeMapProvider().Concat(prewarm);

        if (applyDropTable) MonsterDropEngine.ReplaceRuntimeDropRules(world.Drops);

        foreach (var npc in world.Npcs) npcEngine.RegisterNpc(npc);
        return prewarm;
    }
}
