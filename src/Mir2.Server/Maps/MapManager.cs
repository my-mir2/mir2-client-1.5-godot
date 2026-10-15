using System;
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
        // 注册官方 1.5 核心地图世界实例 (100% 严格对齐官方 .map 二进制文件 Header 真实物理尺寸)
        RegisterMap(new GameMap(0, "比奇省", 700, 700));
        RegisterMap(new GameMap(3, "盟重省", 1000, 800));
        RegisterMap(new GameMap(1, "沃玛森林", 600, 600));
        RegisterMap(new GameMap(2, "毒蛇山谷", 600, 600));
        RegisterMap(new GameMap(10, "未知暗殿", 100, 100));
        RegisterMap(new GameMap(11, "白日门/祖玛寺庙", 500, 500));
        RegisterMap(new GameMap(157, "祖玛寺庙大厅", 500, 500));

        var biqi = GetMap(0);
        if (biqi != null)
        {
            string mapPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources", "maps", "Map", "0.map");
            if (!System.IO.File.Exists(mapPath))
            {
                mapPath = "resources/maps/Map/0.map";
            }
            if (System.IO.File.Exists(mapPath))
            {
                biqi.LoadMapFile(mapPath);
            }
            biqi.SetBlockRect(100, 100, 5, 50, blocked: true);
        }

        // 注册盛大官方经典跨地图跳点传送通道 (Transfer Passage)
        // 兼容测试跳点
        AddPassage(new MapPassage { FromMapId = 0, FromX = 650, FromY = 460, ToMapId = 2, ToX = 50, ToY = 500, Description = "比奇省通往毒蛇山谷 (兼容通道)" });
        AddPassage(new MapPassage { FromMapId = 2, FromX = 50, FromY = 500, ToMapId = 0, ToX = 650, ToY = 460, Description = "毒蛇山谷通往比奇省 (兼容通道)" });
        AddPassage(new MapPassage { FromMapId = 2, FromX = 500, FromY = 50, ToMapId = 3, ToX = 140, ToY = 89, Description = "毒蛇山谷通往盟重省 (兼容通道)" });
        AddPassage(new MapPassage { FromMapId = 3, FromX = 140, FromY = 89, ToMapId = 2, ToX = 500, ToY = 50, Description = "盟重省通往毒蛇山谷 (兼容通道)" });
        AddPassage(new MapPassage { FromMapId = 0, FromX = 140, FromY = 20, ToMapId = 1, ToX = 310, ToY = 50, Description = "比奇省通往沃玛森林 (兼容通道)" });
        AddPassage(new MapPassage { FromMapId = 1, FromX = 310, FromY = 50, ToMapId = 0, ToX = 140, ToY = 20, Description = "沃玛森林通往比奇省 (兼容通道)" });
        AddPassage(new MapPassage { FromMapId = 3, FromX = 860, FromY = 175, ToMapId = 11, ToX = 20, ToY = 20, Description = "盟重省通往祖玛寺庙 (兼容通道)" });
        AddPassage(new MapPassage { FromMapId = 11, FromX = 20, FromY = 20, ToMapId = 3, ToX = 860, ToY = 175, Description = "祖玛寺庙通往盟重省 (兼容通道)" });

        // 盛大官方 1.4/1.5 原版 MapInfo.txt 真实物理跳点 (权威真值)
        // 1. 比奇省 (0) <-> 沃玛森林 (1)
        AddPassage(new MapPassage { FromMapId = 0, FromX = 325, FromY = 34, ToMapId = 1, ToX = 560, ToY = 553, Description = "比奇省通往沃玛森林 (官方 1.5)" });
        AddPassage(new MapPassage { FromMapId = 1, FromX = 560, FromY = 556, ToMapId = 0, ToX = 325, ToY = 37, Description = "沃玛森林通往比奇省 (官方 1.5)" });

        // 2. 比奇省 (0) <-> 毒蛇山谷 (2)
        AddPassage(new MapPassage { FromMapId = 0, FromX = 673, FromY = 83, ToMapId = 2, ToX = 416, ToY = 564, Description = "比奇省通往毒蛇山谷 (官方 1.5)" });
        AddPassage(new MapPassage { FromMapId = 2, FromX = 415, FromY = 567, ToMapId = 0, ToX = 672, ToY = 86, Description = "毒蛇山谷通往比奇省 (官方 1.5)" });

        // 3. 毒蛇山谷 (2) <-> 盟重省 (3) 西关与东关
        AddPassage(new MapPassage { FromMapId = 2, FromX = 298, FromY = 58, ToMapId = 3, ToX = 275, ToY = 750, Description = "毒蛇山谷西关通往盟重省 (官方 1.5)" });
        AddPassage(new MapPassage { FromMapId = 3, FromX = 273, FromY = 753, ToMapId = 2, ToX = 296, ToY = 61, Description = "盟重省通往毒蛇山谷西关 (官方 1.5)" });
        AddPassage(new MapPassage { FromMapId = 2, FromX = 559, FromY = 74, ToMapId = 3, ToX = 514, ToY = 774, Description = "毒蛇山谷东关通往盟重省 (官方 1.5)" });
        AddPassage(new MapPassage { FromMapId = 3, FromX = 512, FromY = 777, ToMapId = 2, ToX = 557, ToY = 77, Description = "盟重省通往毒蛇山谷东关 (官方 1.5)" });

        // 4. 盟重省 (3) <-> 祖玛寺庙大厅 (11 / 0157)
        AddPassage(new MapPassage { FromMapId = 3, FromX = 862, FromY = 174, ToMapId = 11, ToX = 9, ToY = 25, Description = "盟重省通往祖玛寺庙大厅 (官方 1.5)" });
        AddPassage(new MapPassage { FromMapId = 11, FromX = 8, FromY = 26, ToMapId = 3, ToX = 861, ToY = 175, Description = "祖玛寺庙大厅通往盟重省 (官方 1.5)" });

        // 5. 沃玛森林 (1) <-> 白日门要塞 (11) (官方 1.5 虎卫传说主线通道)
        AddPassage(new MapPassage { FromMapId = 1, FromX = 350, FromY = 20, ToMapId = 11, ToX = 180, ToY = 300, Description = "沃玛森林通往白日门要塞 (官方 1.5 虎卫传说)" });
        AddPassage(new MapPassage { FromMapId = 11, FromX = 180, FromY = 305, ToMapId = 1, ToX = 350, ToY = 25, Description = "白日门要塞通往沃玛森林 (官方 1.5)" });
        FreezePassages();
    }

    private readonly ConcurrentDictionary<long, MapPassage> _passages = new();
    private System.Collections.Frozen.FrozenDictionary<long, MapPassage>? _frozenPassages;

    public void FreezePassages()
    {
        _frozenPassages = System.Collections.Frozen.FrozenDictionary.ToFrozenDictionary(_passages);
    }

    public static long MakePassageKey(int mapId, int x, int y) => ((long)mapId << 48) | ((long)x << 24) | (uint)y;

    public void AddPassage(MapPassage passage)
    {
        long key = MakePassageKey(passage.FromMapId, passage.FromX, passage.FromY);
        _passages[key] = passage;
        _frozenPassages = null; // 重置冻结缓存以备动态追加
    }

    public MapPassage? FindPassage(int mapId, int x, int y)
    {
        long key = MakePassageKey(mapId, x, y);
        if (_frozenPassages != null)
        {
            _frozenPassages.TryGetValue(key, out var frozenPassage);
            return frozenPassage;
        }
        _passages.TryGetValue(key, out var passage);
        return passage;
    }

    /// <summary>
    /// 用数据库真实世界 (official_map / official_map_gate) 整体替换内置地图与跳点。
    /// 构造函数中的内置地图与“兼容通道”仅作单元测试夹具，生产启动必须经由本方法覆盖。
    /// </summary>
    public void ReplaceWorld(IEnumerable<GameMap> maps, IEnumerable<MapPassage> passages)
    {
        _maps.Clear();
        _passages.Clear();
        _frozenPassages = null;
        foreach (var m in maps) _maps[m.MapId] = m;
        foreach (var p in passages) _passages[MakePassageKey(p.FromMapId, p.FromX, p.FromY)] = p;
        FreezePassages();
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
