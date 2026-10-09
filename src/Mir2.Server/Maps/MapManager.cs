using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Mir2.Server.Maps;

/// <summary>
/// 盛大官方 1.5 服务端地图全局管理器 (MapManager)
/// </summary>
public sealed class MapManager
{
    private readonly ConcurrentDictionary<int, GameMap> _maps = new();

    public MapManager()
    {
        // 注册官方 1.5 核心地图世界实例 (对齐官方 800x800 真实坐标范围)
        RegisterMap(new GameMap(0, "比奇省", 800, 800));
        RegisterMap(new GameMap(3, "盟重省", 800, 800));
        RegisterMap(new GameMap(1, "沃玛森林", 400, 400));
        RegisterMap(new GameMap(2, "毒蛇山谷", 600, 600));
        RegisterMap(new GameMap(11, "祖玛寺庙", 200, 200));

        // 在比奇省初始化一段自然河道阻挡 (例如 X: 100..105, Y: 100..150 为水面不可通行)
        var biqi = GetMap(0);
        biqi?.SetBlockRect(100, 100, 5, 50, blocked: true);

        // 注册盛大官方经典跨地图跳点传送通道 (Transfer Passage)
        // 1. 比奇省 (0) <-> 毒蛇山谷 (2)
        AddPassage(new MapPassage { FromMapId = 0, FromX = 650, FromY = 460, ToMapId = 2, ToX = 50, ToY = 500, Description = "比奇省通往毒蛇山谷" });
        AddPassage(new MapPassage { FromMapId = 2, FromX = 50, FromY = 500, ToMapId = 0, ToX = 650, ToY = 460, Description = "毒蛇山谷通往比奇省" });

        // 2. 毒蛇山谷 (2) <-> 盟重省 (3)
        AddPassage(new MapPassage { FromMapId = 2, FromX = 500, FromY = 50, ToMapId = 3, ToX = 140, ToY = 89, Description = "毒蛇山谷通往盟重省" });
        AddPassage(new MapPassage { FromMapId = 3, FromX = 140, FromY = 89, ToMapId = 2, ToX = 500, ToY = 50, Description = "盟重省通往毒蛇山谷" });

        // 3. 比奇省 (0) <-> 沃玛森林 (1)
        AddPassage(new MapPassage { FromMapId = 0, FromX = 140, FromY = 20, ToMapId = 1, ToX = 310, ToY = 50, Description = "比奇省通往沃玛森林" });
        AddPassage(new MapPassage { FromMapId = 1, FromX = 310, FromY = 50, ToMapId = 0, ToX = 140, ToY = 20, Description = "沃玛森林通往比奇省" });

        // 4. 盟重省 (3) <-> 祖玛寺庙 (11)
        AddPassage(new MapPassage { FromMapId = 3, FromX = 860, FromY = 175, ToMapId = 11, ToX = 20, ToY = 20, Description = "盟重省通往祖玛寺庙" });
        AddPassage(new MapPassage { FromMapId = 11, FromX = 20, FromY = 20, ToMapId = 3, ToX = 860, ToY = 175, Description = "祖玛寺庙通往盟重省" });
    }

    private readonly ConcurrentDictionary<long, MapPassage> _passages = new();

    public static long MakePassageKey(int mapId, int x, int y) => ((long)mapId << 48) | ((long)x << 24) | (uint)y;

    public void AddPassage(MapPassage passage)
    {
        long key = MakePassageKey(passage.FromMapId, passage.FromX, passage.FromY);
        _passages[key] = passage;
    }

    public MapPassage? FindPassage(int mapId, int x, int y)
    {
        long key = MakePassageKey(mapId, x, y);
        _passages.TryGetValue(key, out var passage);
        return passage;
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

    public IEnumerable<GameMap> AllMaps => _maps.Values;

    public GameMap DefaultMap => GetMap(0) ?? _maps.Values.GetEnumerator().Current;
}
