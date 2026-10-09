using System;
using System.Collections;
using System.Collections.Generic;
using Mir2.Server.Network;

namespace Mir2.Server.Maps;

/// <summary>
/// 盛大官方 1.5 服务端物理地图实例 (GameMap)
/// 集成九宫格空间分区 (AOI) 与超高性能纳秒级阻挡位图 (Collision BitGrid)
/// </summary>
public sealed class GameMap
{
    public int MapId { get; }
    public string Name { get; }
    public int Width { get; }
    public int Height { get; }

    public NinePatchAoi<ClientSession> Aoi { get; }

    // 采用紧凑 BitArray 存储阻挡，0=阻挡不可通行，1=可通行
    // 400x400 地图仅占用 20KB 内存，4000x4000 超大地图仅 2MB，单次阻挡检测耗时 < 3ns
    private readonly BitArray _walkableGrid;

    public GameMap(int mapId, string name, int width, int height)
    {
        MapId = mapId;
        Name = name;
        Width = width;
        Height = height;
        Aoi = new NinePatchAoi<ClientSession>(width, height);
        _walkableGrid = new BitArray(width * height, true); // 默认全图可通行
    }

    /// <summary>
    /// 判定坐标 (x, y) 是否位于地图边界内且无阻挡可通行
    /// </summary>
    public bool IsWalkable(int x, int y)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
        {
            return false; // 超出地图物理边界，绝对不可通行
        }

        int index = x * Height + y;
        return _walkableGrid.Get(index);
    }

    /// <summary>
    /// 设置指定坐标点的通行能力 (true=可通行, false=阻挡)
    /// </summary>
    public void SetWalkable(int x, int y, bool walkable)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height) return;
        int index = x * Height + y;
        _walkableGrid.Set(index, walkable);
    }

    /// <summary>
    /// 设置矩形区域阻挡 (如水池、建筑、城墙)
    /// </summary>
    public void SetBlockRect(int startX, int startY, int width, int height, bool blocked = true)
    {
        for (int x = startX; x < startX + width && x < Width; x++)
        {
            for (int y = startY; y < startY + height && y < Height; y++)
            {
                SetWalkable(x, y, !blocked);
            }
        }
    }

    // ==================== 官方 1.4/1.5 地面物品容器与 80 秒保护期 (IDA 0x4C5CB0) ====================
    private readonly System.Collections.Concurrent.ConcurrentDictionary<long, List<GroundItemEntity>> _groundItems = new();

    public static long MakeCoordKey(int x, int y) => ((long)x << 32) | (uint)y;

    public void AddGroundItem(GroundItemEntity item)
    {
        long key = MakeCoordKey(item.X, item.Y);
        var list = _groundItems.GetOrAdd(key, _ => new List<GroundItemEntity>());
        lock (list)
        {
            list.Add(item);
        }
    }

    public (bool success, GroundItemEntity? item, string errorMsg) TryPickGroundItem(int x, int y, int sessionId, long nowMs)
    {
        long key = MakeCoordKey(x, y);
        if (!_groundItems.TryGetValue(key, out var list))
        {
            return (false, null, "地上没有任何物品");
        }

        lock (list)
        {
            if (list.Count == 0)
            {
                return (false, null, "地上没有任何物品");
            }

            // 优先拾取列表顶部物品
            var candidate = list[0];

            // 官方保护期判定: 80,000 毫秒 = 80秒 (IDA 0x4C5CB0 @ 0x13880)
            if (nowMs - candidate.DropTickMs <= 80000 && candidate.OwnerSessionId != 0 && candidate.OwnerSessionId != sessionId)
            {
                return (false, null, "物品受保护中，无法拾取");
            }

            list.RemoveAt(0);
            if (list.Count == 0)
            {
                _groundItems.TryRemove(key, out _);
            }
            return (true, candidate, string.Empty);
        }
    }
}

/// <summary>
/// 地面掉落物实体
/// </summary>
public sealed class GroundItemEntity
{
    public int Uid { get; init; }
    public string Name { get; init; } = string.Empty;
    public int X { get; init; }
    public int Y { get; init; }
    public int Count { get; init; } = 1;
    public bool IsGold { get; init; }
    public int Weight { get; init; } = 1;
    public int OwnerSessionId { get; set; }
    public long DropTickMs { get; init; }
}

