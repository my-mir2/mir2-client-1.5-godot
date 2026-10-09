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

    /// <summary>
    /// 盛大官方 1.5 原始 .map 二进制文件物理碰撞网格加载器
    /// 文件结构: 52 字节 Header + W*H*12 字节列优先单元格
    /// FloorRaw & 0x8000 != 0 表示该瓦片为不可通行物理阻挡 (山体、建筑、墙体、河流)
    /// </summary>
    public bool LoadMapFile(string mapFilePath)
    {
        if (!System.IO.File.Exists(mapFilePath)) return false;

        try
        {
            using var fs = new System.IO.FileStream(mapFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
            if (fs.Length < 52) return false;

            byte[] header = new byte[52];
            fs.ReadExactly(header);

            int fileWidth = header[0] | (header[1] << 8);
            int fileHeight = header[2] | (header[3] << 8);

            int totalCells = fileWidth * fileHeight;
            int totalBytes = totalCells * 12;
            if (fs.Length < 52 + totalBytes) return false;

            byte[] cellBuffer = new byte[totalBytes];
            fs.ReadExactly(cellBuffer);

            // 按照列优先布局将阻挡状态灌入高吞吐 BitArray
            int checkWidth = Math.Min(Width, fileWidth);
            int checkHeight = Math.Min(Height, fileHeight);

            for (int x = 0; x < checkWidth; x++)
            {
                for (int y = 0; y < checkHeight; y++)
                {
                    int offset = (x * fileHeight + y) * 12;
                    short floorRaw = (short)(cellBuffer[offset] | (cellBuffer[offset + 1] << 8));
                    bool isBlocked = (floorRaw & unchecked((short)0x8000)) != 0;
                    SetWalkable(x, y, !isBlocked);
                }
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Bresenham 视线射线穿透检测 (Line of Sight)
    /// 判定从 (x0, y0) 到 (x1, y1) 之间是否有不可通行的物理墙体阻挡 (用于法师雷电术、道士火符防隔墙打击)
    /// </summary>
    public bool CheckLineOfSight(int x0, int y0, int x1, int y1)
    {
        int dx = Math.Abs(x1 - x0);
        int dy = Math.Abs(y1 - y0);
        int sx = x0 < x1 ? 1 : -1;
        int sy = y0 < y1 ? 1 : -1;
        int err = dx - dy;

        int currX = x0;
        int currY = y0;

        while (true)
        {
            if (currX == x1 && currY == y1)
            {
                return true; // 射线成功到达终点，无障碍阻挡
            }

            // 中途经过的格子必须为可通行地面
            if ((currX != x0 || currY != y0) && !IsWalkable(currX, currY))
            {
                return false; // 被物理山体或墙体遮挡
            }

            int e2 = 2 * err;
            if (e2 > -dy)
            {
                err -= dy;
                currX += sx;
            }
            if (e2 < dx)
            {
                err += dx;
                currY += sy;
            }
        }
    }

    // ==================== 官方 1.4/1.5 地表火墙实体容器 (FireWall) ====================
    private readonly System.Collections.Concurrent.ConcurrentDictionary<long, List<FireWallEntity>> _fireWalls = new();

    public void AddFireWall(FireWallEntity fire)
    {
        long key = MakeCoordKey(fire.X, fire.Y);
        var list = _fireWalls.GetOrAdd(key, _ => new List<FireWallEntity>());
        lock (list)
        {
            list.Add(fire);
        }
    }

    public List<FireWallEntity> GetFireWallsAt(int x, int y)
    {
        long key = MakeCoordKey(x, y);
        if (_fireWalls.TryGetValue(key, out var list))
        {
            lock (list)
            {
                return new List<FireWallEntity>(list);
            }
        }
        return new List<FireWallEntity>();
    }

    public int CleanExpiredFireWalls(long nowMs)
    {
        int cleaned = 0;
        foreach (var kvp in _fireWalls)
        {
            var list = kvp.Value;
            lock (list)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (nowMs >= list[i].ExpireTimeMs)
                    {
                        list.RemoveAt(i);
                        cleaned++;
                    }
                }
            }
            if (list.Count == 0)
            {
                _fireWalls.TryRemove(kvp.Key, out _);
            }
        }
        return cleaned;
    }

    // ==================== 官方 1.4/1.5 地图门状态机与 5 秒自动关门 (IDA sub_49F2FC/sub_49F420) ====================
    private readonly System.Collections.Concurrent.ConcurrentDictionary<long, DoorEntity> _doors = new();

    public void AddDoor(DoorEntity door)
    {
        long key = MakeCoordKey(door.X, door.Y);
        _doors[key] = door;
        if (!door.IsOpen)
        {
            SetWalkable(door.X, door.Y, false); // 默认关门阻挡
        }
    }

    public DoorEntity? GetDoor(int x, int y)
    {
        long key = MakeCoordKey(x, y);
        _doors.TryGetValue(key, out var door);
        return door;
    }

    public bool OpenDoor(int x, int y, long nowMs)
    {
        long key = MakeCoordKey(x, y);
        if (_doors.TryGetValue(key, out var door) && !door.IsOpen)
        {
            door.IsOpen = true;
            door.AutoCloseTickMs = nowMs + 5000; // 官方 5 秒自动关门
            SetWalkable(x, y, true); // 移除阻挡允许通行
            return true;
        }
        return false;
    }

    public List<DoorEntity> ProcessDoorTick(long nowMs)
    {
        var closedDoors = new List<DoorEntity>();
        foreach (var kvp in _doors)
        {
            var d = kvp.Value;
            if (d.IsOpen && nowMs >= d.AutoCloseTickMs)
            {
                d.IsOpen = false;
                SetWalkable(d.X, d.Y, false); // 恢复关门物理阻挡
                closedDoors.Add(d);
            }
        }
        return closedDoors;
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

    public List<GroundItemEntity> GetGroundItemsAt(int x, int y)
    {
        long key = MakeCoordKey(x, y);
        if (_groundItems.TryGetValue(key, out var list))
        {
            lock (list)
            {
                return new List<GroundItemEntity>(list);
            }
        }
        return new List<GroundItemEntity>();
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

    /// <summary>
    /// 清理存活超过 maxLifeMs (默认2分钟) 的地面物品，防止内存堆积与泄露
    /// </summary>
    public int CleanExpiredGroundItems(long nowMs, long maxLifeMs = 120_000)
    {
        int removedCount = 0;
        foreach (var kvp in _groundItems)
        {
            var list = kvp.Value;
            lock (list)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (nowMs - list[i].DropTickMs > maxLifeMs)
                    {
                        list.RemoveAt(i);
                        removedCount++;
                    }
                }
            }
            if (list.Count == 0)
            {
                _groundItems.TryRemove(kvp.Key, out _);
            }
        }
        return removedCount;
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

/// <summary>
/// 盛大官方 1.5 地表火墙实体
/// </summary>
public sealed class FireWallEntity
{
    public int Uid { get; init; }
    public int X { get; init; }
    public int Y { get; init; }
    public int CasterSessionId { get; init; }
    public int Damage { get; init; }
    public long ExpireTimeMs { get; init; }
}

/// <summary>
/// 盛大官方 1.5 地图门实体 (Door)
/// 包含开关阻挡动态切换与 5 秒自动关门状态机 (IDA sub_49F2FC/sub_49F420)
/// </summary>
public sealed class DoorEntity
{
    public int DoorId { get; init; }
    public int X { get; init; }
    public int Y { get; init; }
    public bool IsOpen { get; set; }
    public long AutoCloseTickMs { get; set; }
}

/// <summary>
/// 盛大官方 1.5 地图跳点传送通道 (Transfer Passage)
/// </summary>
public sealed class MapPassage
{
    public int FromMapId { get; init; }
    public int FromX { get; init; }
    public int FromY { get; init; }
    public int ToMapId { get; init; }
    public int ToX { get; init; }
    public int ToY { get; init; }
    public string Description { get; init; } = string.Empty;
}

