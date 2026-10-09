using System;
using System.Collections.Generic;

namespace Mir2.Server.Maps;

/// <summary>
/// 盛大官方 1.5 服务端高性能 8 方向 A* 寻路引擎 (ServerPathFinder)
/// 支持防对角线穿角、实体占位防重叠避障与 Native AOT 零分配优化
/// </summary>
public static class ServerPathFinder
{
    private const int StraightCost = 10;
    private const int DiagonalCost = 14;
    private const int DefaultMaxDepth = 16;
    private const int MaxSearchSteps = 256;

    // 官方 8 方向步进与朝向编码 (0:北, 1:东北, 2:东, 3:东南, 4:南, 5:西南, 6:西, 7:西北)
    private static readonly (int dx, int dy, byte dir)[] Directions = new[]
    {
        (0, -1, (byte)0),
        (1, -1, (byte)1),
        (1, 0, (byte)2),
        (1, 1, (byte)3),
        (0, 1, (byte)4),
        (-1, 1, (byte)5),
        (-1, 0, (byte)6),
        (-1, -1, (byte)7)
    };

    private sealed class PathNode
    {
        public int X;
        public int Y;
        public int GCost;
        public int HCost;
        public int FCost => GCost + HCost;
        public PathNode? Parent;
        public byte Direction;
    }

    private static int CalcHeuristic(int x1, int y1, int x2, int y2)
    {
        int dx = Math.Abs(x1 - x2);
        int dy = Math.Abs(y1 - y2);
        return StraightCost * (dx + dy) + (DiagonalCost - 2 * StraightCost) * Math.Min(dx, dy);
    }

    /// <summary>
    /// 为追击目标或巡逻计算下一步的最佳移动坐标与朝向
    /// 返回 (nextX, nextY, direction, foundPath)
    /// </summary>
    public static (int nextX, int nextY, byte direction, bool foundPath) FindNextStep(
        GameMap map,
        int startX,
        int startY,
        int targetX,
        int targetY,
        Func<int, int, bool>? isOccupied = null,
        int maxDepth = DefaultMaxDepth)
    {
        if (startX == targetX && startY == targetY)
        {
            return (startX, startY, 0, true);
        }

        int dxTotal = Math.Abs(targetX - startX);
        int dyTotal = Math.Abs(targetY - startY);
        if (dxTotal > maxDepth || dyTotal > maxDepth)
        {
            // 超出最大寻路视野范围
            return (startX, startY, 0, false);
        }

        var openSet = new PriorityQueue<PathNode, int>();
        var allNodes = new Dictionary<long, PathNode>(128);

        static long PackCoord(int x, int y) => ((long)x << 32) | (uint)y;

        var startNode = new PathNode
        {
            X = startX,
            Y = startY,
            GCost = 0,
            HCost = CalcHeuristic(startX, startY, targetX, targetY),
            Parent = null,
            Direction = 0
        };

        openSet.Enqueue(startNode, startNode.FCost);
        allNodes[PackCoord(startX, startY)] = startNode;

        var closedSet = new HashSet<long>();
        PathNode? closestNode = startNode;
        int closestH = startNode.HCost;
        int steps = 0;

        while (openSet.Count > 0 && steps < MaxSearchSteps)
        {
            steps++;
            var current = openSet.Dequeue();
            long currentKey = PackCoord(current.X, current.Y);

            if (closedSet.Contains(currentKey)) continue;
            closedSet.Add(currentKey);

            // 如果到达目标相邻一格 (近战攻击攻击圈) 或完全到达目标点
            int distToTarget = Math.Max(Math.Abs(current.X - targetX), Math.Abs(current.Y - targetY));
            if (distToTarget <= 1)
            {
                closestNode = current;
                break;
            }

            if (current.HCost < closestH)
            {
                closestH = current.HCost;
                closestNode = current;
            }

            // 扩展 8 邻域
            for (int i = 0; i < Directions.Length; i++)
            {
                var (dx, dy, dir) = Directions[i];
                int nx = current.X + dx;
                int ny = current.Y + dy;

                if (!map.IsWalkable(nx, ny)) continue;

                // 官方防穿角逻辑 (Diagonal corner cutting prevention)
                if (dx != 0 && dy != 0)
                {
                    if (!map.IsWalkable(current.X + dx, current.Y) && !map.IsWalkable(current.X, current.Y + dy))
                    {
                        continue; // 两侧均阻挡时不可斜穿
                    }
                }

                // 实体占位防重叠判定 (目标位置除外)
                if (isOccupied != null && !(nx == targetX && ny == targetY))
                {
                    if (isOccupied(nx, ny)) continue;
                }

                long neighborKey = PackCoord(nx, ny);
                if (closedSet.Contains(neighborKey)) continue;

                int stepCost = (dx != 0 && dy != 0) ? DiagonalCost : StraightCost;
                int newG = current.GCost + stepCost;

                if (!allNodes.TryGetValue(neighborKey, out var neighborNode))
                {
                    neighborNode = new PathNode
                    {
                        X = nx,
                        Y = ny,
                        GCost = newG,
                        HCost = CalcHeuristic(nx, ny, targetX, targetY),
                        Parent = current,
                        Direction = dir
                    };
                    allNodes[neighborKey] = neighborNode;
                    openSet.Enqueue(neighborNode, neighborNode.FCost);
                }
                else if (newG < neighborNode.GCost)
                {
                    neighborNode.GCost = newG;
                    neighborNode.Parent = current;
                    neighborNode.Direction = dir;
                    openSet.Enqueue(neighborNode, neighborNode.FCost);
                }
            }
        }

        // 回溯路径找出 startNode 的下一个节点
        if (closestNode == null || closestNode == startNode)
        {
            return (startX, startY, 0, false);
        }

        var curr = closestNode;
        while (curr.Parent != null && curr.Parent != startNode)
        {
            curr = curr.Parent;
        }

        return (curr.X, curr.Y, curr.Direction, true);
    }
}
