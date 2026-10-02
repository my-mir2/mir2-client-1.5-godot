using System;
using System.Collections.Generic;
using Godot;

namespace Mir2Client.Core;

/// <summary>
/// 高性能 C# 8 方向 A* 寻路引擎 (支持 Windows Native AOT 极限展开)
/// 严格对齐官方客户端 8 方向寻路步长与代价 (Straight=10, Diagonal=14)
/// </summary>
public static class FastPathFinder
{
    private const int StraightCost = 10;
    private const int DiagonalCost = 14;
    private const int MaxSearchSteps = 2000;

    private static readonly Vector2I[] DirOffsets = new Vector2I[]
    {
        new(0, -1),  // 0: 上
        new(1, -1),  // 1: 右上
        new(1, 0),   // 2: 右
        new(1, 1),   // 3: 右下
        new(0, 1),   // 4: 下
        new(-1, 1),  // 5: 左下
        new(-1, 0),  // 6: 左
        new(-1, -1)  // 7: 左上
    };

    private sealed class Node : IComparable<Node>
    {
        public Vector2I Pos;
        public int GCost;
        public int HCost;
        public Node? Parent;

        public int FCost => GCost + HCost;

        public int CompareTo(Node? other)
        {
            if (other == null) return 1;
            int cmp = FCost.CompareTo(other.FCost);
            return cmp != 0 ? cmp : HCost.CompareTo(other.HCost);
        }
    }

    private static int CalcHeuristic(Vector2I a, Vector2I b)
    {
        int dx = Math.Abs(a.X - b.X);
        int dy = Math.Abs(a.Y - b.Y);
        return StraightCost * (dx + dy) + (DiagonalCost - 2 * StraightCost) * Math.Min(dx, dy);
    }

    public static List<Vector2I> FindPath(Vector2I start, Vector2I goal, Func<int, int, bool> isPassable, int maxRange = 60)
    {
        var result = new List<Vector2I>();
        if (start == goal) return result;

        int distSq = (goal.X - start.X) * (goal.X - start.X) + (goal.Y - start.Y) * (goal.Y - start.Y);
        if (distSq > maxRange * maxRange) return result;

        var openSet = new Dictionary<Vector2I, Node>();
        var closedSet = new HashSet<Vector2I>();

        var startNode = new Node
        {
            Pos = start,
            GCost = 0,
            HCost = CalcHeuristic(start, goal)
        };
        openSet[start] = startNode;

        int steps = 0;
        Node closestNode = startNode;
        int minH = startNode.HCost;

        while (openSet.Count > 0 && steps < MaxSearchSteps)
        {
            steps++;

            // 提取 F 最小的节点
            Node current = null!;
            int lowestF = int.MaxValue;
            foreach (var kvp in openSet)
            {
                if (kvp.Value.FCost < lowestF)
                {
                    lowestF = kvp.Value.FCost;
                    current = kvp.Value;
                }
            }

            if (current.Pos == goal)
            {
                closestNode = current;
                break;
            }

            openSet.Remove(current.Pos);
            closedSet.Add(current.Pos);

            if (current.HCost < minH)
            {
                minH = current.HCost;
                closestNode = current;
            }

            for (int i = 0; i < 8; i++)
            {
                Vector2I nextPos = current.Pos + DirOffsets[i];
                if (closedSet.Contains(nextPos)) continue;

                // 地砖阻挡检查
                if (!isPassable(nextPos.X, nextPos.Y)) continue;

                // 对角线拐角阻挡检查 (官方经典防穿角)
                if (i % 2 == 1)
                {
                    int prevDir = (i + 7) % 8;
                    int nextDir = (i + 1) % 8;
                    if (!isPassable(current.Pos.X + DirOffsets[prevDir].X, current.Pos.Y + DirOffsets[prevDir].Y) ||
                        !isPassable(current.Pos.X + DirOffsets[nextDir].X, current.Pos.Y + DirOffsets[nextDir].Y))
                    {
                        continue;
                    }
                }

                int stepCost = (i % 2 == 0) ? StraightCost : DiagonalCost;
                int newGCost = current.GCost + stepCost;

                if (!openSet.TryGetValue(nextPos, out var neighborNode))
                {
                    neighborNode = new Node
                    {
                        Pos = nextPos,
                        GCost = newGCost,
                        HCost = CalcHeuristic(nextPos, goal),
                        Parent = current
                    };
                    openSet[nextPos] = neighborNode;
                }
                else if (newGCost < neighborNode.GCost)
                {
                    neighborNode.GCost = newGCost;
                    neighborNode.Parent = current;
                }
            }
        }

        // 回溯生成正向路径
        Node? curr = closestNode;
        while (curr != null && curr.Pos != start)
        {
            result.Add(curr.Pos);
            curr = curr.Parent;
        }
        result.Reverse();
        return result;
    }
}
