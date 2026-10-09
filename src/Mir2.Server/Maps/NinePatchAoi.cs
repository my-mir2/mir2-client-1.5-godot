using System;
using System.Collections.Generic;

namespace Mir2.Server.Maps;

/// <summary>
/// 盛大官方 1.5 权威九宫格 AOI (Area of Interest) 空间分区系统
/// 将地图划分为固定尺寸的 Zone (默认 16x16 格子)，在 100ms 心跳末尾执行九宫格广播，
/// 将千人同屏的 O(N^2) 广播复杂度骤降为 O(1) 局部常数级。
/// </summary>
public sealed class NinePatchAoi<TEntity> where TEntity : class
{
    public const int ZoneSize = 16; // 每个 Zone 16x16 格子 (对齐经典客户端半屏/全屏视野 16x12)

    private readonly int _width;
    private readonly int _height;
    private readonly int _zonesX;
    private readonly int _zonesY;
    private readonly HashSet<TEntity>[] _zones;

    public NinePatchAoi(int mapWidth, int mapHeight)
    {
        _width = mapWidth;
        _height = mapHeight;
        _zonesX = (mapWidth + ZoneSize - 1) / ZoneSize;
        _zonesY = (mapHeight + ZoneSize - 1) / ZoneSize;
        _zones = new HashSet<TEntity>[_zonesX * _zonesY];
        for (int i = 0; i < _zones.Length; i++)
        {
            _zones[i] = new HashSet<TEntity>();
        }
    }

    public int GetZoneIndex(int tileX, int tileY)
    {
        int zx = Math.Clamp(tileX / ZoneSize, 0, _zonesX - 1);
        int zy = Math.Clamp(tileY / ZoneSize, 0, _zonesY - 1);
        return zy * _zonesX + zx;
    }

    public (int zx, int zy) GetZoneCoord(int tileX, int tileY)
    {
        int zx = Math.Clamp(tileX / ZoneSize, 0, _zonesX - 1);
        int zy = Math.Clamp(tileY / ZoneSize, 0, _zonesY - 1);
        return (zx, zy);
    }

    /// <summary>
    /// 实体进入地图或加入 AOI
    /// </summary>
    public void Enter(TEntity entity, int tileX, int tileY)
    {
        int idx = GetZoneIndex(tileX, tileY);
        lock (_zones[idx])
        {
            _zones[idx].Add(entity);
        }
    }

    /// <summary>
    /// 实体离开地图或注销 AOI
    /// </summary>
    public void Leave(TEntity entity, int tileX, int tileY)
    {
        int idx = GetZoneIndex(tileX, tileY);
        lock (_zones[idx])
        {
            _zones[idx].Remove(entity);
        }
    }

    /// <summary>
    /// 实体位置更新，如果跨越 Zone，计算增量与退订列表 (Enter/Leave 差集)
    /// </summary>
    public bool UpdatePosition(TEntity entity, int oldX, int oldY, int newX, int newY, out List<int>? enteredZones, out List<int>? leftZones)
    {
        var (oldZx, oldZy) = GetZoneCoord(oldX, oldY);
        var (newZx, newZy) = GetZoneCoord(newX, newY);

        if (oldZx == newZx && oldZy == newZy)
        {
            enteredZones = null;
            leftZones = null;
            return false; // 未跨越 Zone，不需要重新订阅视野
        }

        int oldIdx = oldZy * _zonesX + oldZx;
        int newIdx = newZy * _zonesX + newZx;

        lock (_zones[oldIdx])
        {
            _zones[oldIdx].Remove(entity);
        }
        lock (_zones[newIdx])
        {
            _zones[newIdx].Add(entity);
        }

        // 计算周围九宫格差集
        enteredZones = new List<int>(5);
        leftZones = new List<int>(5);

        for (int dy = -1; dy <= 1; dy++)
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                int nzX = newZx + dx;
                int nzY = newZy + dy;
                if (nzX >= 0 && nzX < _zonesX && nzY >= 0 && nzY < _zonesY)
                {
                    // 如果旧九宫格不包含它，说明是新进入的视野 Zone
                    if (Math.Abs(nzX - oldZx) > 1 || Math.Abs(nzY - oldZy) > 1)
                    {
                        enteredZones.Add(nzY * _zonesX + nzX);
                    }
                }

                int ozX = oldZx + dx;
                int ozY = oldZy + dy;
                if (ozX >= 0 && ozX < _zonesX && ozY >= 0 && ozY < _zonesY)
                {
                    // 如果新九宫格不包含它，说明是离开的视野 Zone
                    if (Math.Abs(ozX - newZx) > 1 || Math.Abs(ozY - newZy) > 1)
                    {
                        leftZones.Add(ozY * _zonesX + ozX);
                    }
                }
            }
        }

        return true;
    }

    /// <summary>
    /// 获取当前坐标周围九宫格内的所有实体 (广播目标集合)
    /// </summary>
    public void GetEntitiesInNinePatch(int tileX, int tileY, List<TEntity> result)
    {
        result.Clear();
        var (zx, zy) = GetZoneCoord(tileX, tileY);

        for (int dy = -1; dy <= 1; dy++)
        {
            int currY = zy + dy;
            if (currY < 0 || currY >= _zonesY) continue;

            for (int dx = -1; dx <= 1; dx++)
            {
                int currX = zx + dx;
                if (currX < 0 || currX >= _zonesX) continue;

                int idx = currY * _zonesX + currX;
                lock (_zones[idx])
                {
                    foreach (var entity in _zones[idx])
                    {
                        result.Add(entity);
                    }
                }
            }
        }
    }
}
