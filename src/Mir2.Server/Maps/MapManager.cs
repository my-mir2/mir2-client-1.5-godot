using System.Collections.Concurrent;

namespace Mir2.Server.Maps;

/// <summary>
/// 盛大官方 1.5 服务端地图全局管理器 (MapManager)
/// </summary>
public sealed class MapManager
{
    private readonly ConcurrentDictionary<int, GameMap> _maps = new();

    public MapManager()
    {
        // 注册官方 1.5 核心地图世界实例
        RegisterMap(new GameMap(0, "比奇省", 400, 400));
        RegisterMap(new GameMap(3, "盟重省", 800, 800));
        RegisterMap(new GameMap(1, "沃玛森林", 300, 300));
        RegisterMap(new GameMap(2, "毒蛇山谷", 400, 400));
        RegisterMap(new GameMap(11, "祖玛寺庙", 100, 100));

        // 在比奇省初始化一段自然河道阻挡 (例如 X: 100..105, Y: 100..150 为水面不可通行)
        var biqi = GetMap(0);
        biqi?.SetBlockRect(100, 100, 5, 50, blocked: true);
    }

    public void RegisterMap(GameMap map)
    {
        _maps[map.MapId] = map;
    }

    public GameMap? GetMap(int mapId)
    {
        _maps.TryGetValue(mapId, out var map);
        return map;
    }

    public GameMap DefaultMap => GetMap(0) ?? _maps.Values.GetEnumerator().Current;
}
