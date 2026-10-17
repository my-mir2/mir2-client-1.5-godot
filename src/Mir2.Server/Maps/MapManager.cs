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
        RegisterMap(new GameMap(0, "比奇省", 700, 700) { OfficialId = "0" });
        RegisterMap(new GameMap(3, "盟重省", 1000, 800) { OfficialId = "3" });
        RegisterMap(new GameMap(1, "沃玛森林", 600, 600) { OfficialId = "1" });
        RegisterMap(new GameMap(2, "毒蛇山谷", 600, 600) { OfficialId = "2" });
        RegisterMap(new GameMap(10, "未知暗殿", 100, 100) { OfficialId = "10" });
        RegisterMap(new GameMap(11, "白日门/祖玛寺庙", 500, 500) { OfficialId = "11" });
        RegisterMap(new GameMap(157, "祖玛寺庙大厅", 500, 500) { OfficialId = "0157" });

        // 盛大官方 1.5 比奇城核心功能性店铺建筑室内地图 (100% 官方 MapInfo / official_map 物理尺寸)
        RegisterMap(new GameMap(101, "比奇药店", 20, 28) { OfficialId = "0101" });
        RegisterMap(new GameMap(102, "比奇书店", 15, 15) { OfficialId = "0102" });
        RegisterMap(new GameMap(103, "比奇肉店", 27, 29) { OfficialId = "0103" });
        RegisterMap(new GameMap(104, "比奇首饰店", 13, 16) { OfficialId = "0104" });
        RegisterMap(new GameMap(105, "比奇仓库", 27, 29) { OfficialId = "0105" });
        RegisterMap(new GameMap(106, "比奇铁匠铺", 27, 29) { OfficialId = "0106" });
        RegisterMap(new GameMap(107, "比奇服装店", 15, 18) { OfficialId = "0107" });
        RegisterMap(new GameMap(108, "比奇杂货铺", 15, 18) { OfficialId = "0108" });

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

        // 6. 盛大官方 1.4/1.5 比奇城核心功能性店铺建筑传送门 (100% 严格对齐 official_map_gate)
        // (1) 比奇仓库 (0 <-> 0105 / 105)
        AddPassage(new MapPassage { FromMapId = 0, FromX = 295, FromY = 284, ToMapId = 105, ToX = 8, ToY = 24, Description = "比奇城进入仓库西门" });
        AddPassage(new MapPassage { FromMapId = 0, FromX = 295, FromY = 285, ToMapId = 105, ToX = 9, ToY = 23, Description = "比奇城进入仓库西门" });
        AddPassage(new MapPassage { FromMapId = 0, FromX = 305, FromY = 275, ToMapId = 105, ToX = 20, ToY = 12, Description = "比奇城进入仓库东门" });
        AddPassage(new MapPassage { FromMapId = 0, FromX = 305, FromY = 276, ToMapId = 105, ToX = 21, ToY = 11, Description = "比奇城进入仓库东门" });
        AddPassage(new MapPassage { FromMapId = 105, FromX = 9, FromY = 25, ToMapId = 0, ToX = 295, ToY = 286, Description = "比奇仓库离开西门" });
        AddPassage(new MapPassage { FromMapId = 105, FromX = 9, FromY = 24, ToMapId = 0, ToX = 296, ToY = 285, Description = "比奇仓库离开西门" });
        AddPassage(new MapPassage { FromMapId = 105, FromX = 21, FromY = 12, ToMapId = 0, ToX = 306, ToY = 276, Description = "比奇仓库离开东门" });
        AddPassage(new MapPassage { FromMapId = 105, FromX = 21, FromY = 13, ToMapId = 0, ToX = 306, ToY = 277, Description = "比奇仓库离开东门" });

        // (2) 比奇药店 (0 <-> 0101 / 101)
        AddPassage(new MapPassage { FromMapId = 0, FromX = 286, FromY = 295, ToMapId = 101, ToX = 9, ToY = 22, Description = "比奇城进入药店正门" });
        AddPassage(new MapPassage { FromMapId = 0, FromX = 289, FromY = 292, ToMapId = 101, ToX = 17, ToY = 14, Description = "比奇城进入药店侧门" });
        AddPassage(new MapPassage { FromMapId = 101, FromX = 10, FromY = 22, ToMapId = 0, ToX = 287, ToY = 296, Description = "比奇药店离开正门" });
        AddPassage(new MapPassage { FromMapId = 101, FromX = 18, FromY = 14, ToMapId = 0, ToX = 290, ToY = 293, Description = "比奇药店离开侧门" });

        // (3) 比奇书店 (0 <-> 0102 / 102)
        AddPassage(new MapPassage { FromMapId = 0, FromX = 308, FromY = 264, ToMapId = 102, ToX = 3, ToY = 7, Description = "比奇城进入书店" });
        AddPassage(new MapPassage { FromMapId = 102, FromX = 3, FromY = 8, ToMapId = 0, ToX = 308, ToY = 265, Description = "比奇书店离开大门" });

        // (4) 比奇肉店 (0 <-> 0103 / 103)
        AddPassage(new MapPassage { FromMapId = 0, FromX = 335, FromY = 299, ToMapId = 103, ToX = 5, ToY = 12, Description = "比奇城进入肉店大门" });
        AddPassage(new MapPassage { FromMapId = 0, FromX = 335, FromY = 300, ToMapId = 103, ToX = 6, ToY = 12, Description = "比奇城进入肉店大门" });
        AddPassage(new MapPassage { FromMapId = 0, FromX = 344, FromY = 308, ToMapId = 103, ToX = 17, ToY = 24, Description = "比奇城进入肉店后门" });
        AddPassage(new MapPassage { FromMapId = 0, FromX = 345, FromY = 308, ToMapId = 103, ToX = 18, ToY = 24, Description = "比奇城进入肉店后门" });
        AddPassage(new MapPassage { FromMapId = 103, FromX = 4, FromY = 13, ToMapId = 0, ToX = 334, ToY = 300, Description = "比奇肉店离开大门" });
        AddPassage(new MapPassage { FromMapId = 103, FromX = 5, FromY = 13, ToMapId = 0, ToX = 334, ToY = 301, Description = "比奇肉店离开大门" });
        AddPassage(new MapPassage { FromMapId = 103, FromX = 16, FromY = 25, ToMapId = 0, ToX = 345, ToY = 310, Description = "比奇肉店离开后门" });
        AddPassage(new MapPassage { FromMapId = 103, FromX = 17, FromY = 25, ToMapId = 0, ToX = 344, ToY = 309, Description = "比奇肉店离开后门" });

        // (5) 比奇铁匠铺 / 武器店 (0 <-> 0106 / 106)
        AddPassage(new MapPassage { FromMapId = 0, FromX = 307, FromY = 310, ToMapId = 106, ToX = 8, ToY = 24, Description = "比奇城进入铁匠铺西门" });
        AddPassage(new MapPassage { FromMapId = 0, FromX = 307, FromY = 311, ToMapId = 106, ToX = 9, ToY = 23, Description = "比奇城进入铁匠铺西门" });
        AddPassage(new MapPassage { FromMapId = 0, FromX = 317, FromY = 301, ToMapId = 106, ToX = 21, ToY = 11, Description = "比奇城进入铁匠铺东门" });
        AddPassage(new MapPassage { FromMapId = 0, FromX = 317, FromY = 302, ToMapId = 106, ToX = 20, ToY = 12, Description = "比奇城进入铁匠铺东门" });
        AddPassage(new MapPassage { FromMapId = 106, FromX = 9, FromY = 25, ToMapId = 0, ToX = 307, ToY = 312, Description = "比奇铁匠铺离开西门" });
        AddPassage(new MapPassage { FromMapId = 106, FromX = 9, FromY = 24, ToMapId = 0, ToX = 308, ToY = 311, Description = "比奇铁匠铺离开西门" });
        AddPassage(new MapPassage { FromMapId = 106, FromX = 21, FromY = 13, ToMapId = 0, ToX = 318, ToY = 302, Description = "比奇铁匠铺离开东门" });
        AddPassage(new MapPassage { FromMapId = 106, FromX = 21, FromY = 12, ToMapId = 0, ToX = 318, ToY = 303, Description = "比奇铁匠铺离开东门" });

        // (6) 比奇首饰店 (0 <-> 0104 / 104)
        AddPassage(new MapPassage { FromMapId = 0, FromX = 323, FromY = 247, ToMapId = 104, ToX = 4, ToY = 10, Description = "比奇城进入首饰店" });
        AddPassage(new MapPassage { FromMapId = 104, FromX = 4, FromY = 11, ToMapId = 0, ToX = 323, ToY = 248, Description = "比奇首饰店离开大门" });

        // (7) 比奇服装店 (0 <-> 0107 / 107)
        AddPassage(new MapPassage { FromMapId = 0, FromX = 354, FromY = 319, ToMapId = 107, ToX = 9, ToY = 13, Description = "比奇城进入服装店" });
        AddPassage(new MapPassage { FromMapId = 107, FromX = 10, FromY = 13, ToMapId = 0, ToX = 354, ToY = 320, Description = "比奇服装店离开大门" });

        // (8) 比奇杂货铺 (0 <-> 0108 / 108)
        AddPassage(new MapPassage { FromMapId = 0, FromX = 326, FromY = 288, ToMapId = 108, ToX = 10, ToY = 12, Description = "比奇城进入杂货铺" });
        AddPassage(new MapPassage { FromMapId = 108, FromX = 10, FromY = 13, ToMapId = 0, ToX = 326, ToY = 289, Description = "比奇杂货铺离开大门" });
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
