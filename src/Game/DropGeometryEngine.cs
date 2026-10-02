using System;
using System.Collections.Generic;

namespace Mir2Client.Game;

/// <summary>
/// 盛大官方 1.5 原版掉落物品逐层螺旋散落与单格 8 件堆叠上限几何引擎
/// 依据 M2Server.exe sub_4C5238 @ 0x4C5238 ~ 0x4C536A 实证:
///   1. 从半径 1 开始向外逐层螺旋检测；
///   2. 优先空格 (count == 0)；
///   3. 次优贪心选择堆叠数最少且 < 8 的格子；
///   4. 若全满 >= 8 则强制回退至中心原点。
/// </summary>
public static class DropGeometryEngine
{
    public readonly record struct Point2D(int X, int Y);

    public static (Point2D dropPos, bool foundEmpty, int minStack, int maxStackLimit)
        EvaluateDropScatter(int centerX, int centerY, int maxRadius, IReadOnlyDictionary<Point2D, int> tileItemsMap)
    {
        Point2D bestPos = new(centerX, centerY);
        int minStack = 999; // 官方 0x4c5247: mov v12, 999
        bool foundEmpty = false;

        for (int r = 1; r <= maxRadius; r++)
        {
            for (int dy = -r; dy <= r; dy++)
            {
                for (int dx = -r; dx <= r; dx++)
                {
                    Point2D checkPos = new(centerX + dx, centerY + dy);
                    tileItemsMap.TryGetValue(checkPos, out int count);

                    if (count == 0)
                    {
                        bestPos = checkPos;
                        foundEmpty = true;
                        break;
                    }
                    else
                    {
                        if (count < minStack)
                        {
                            minStack = count;
                            bestPos = checkPos;
                        }
                    }
                }
                if (foundEmpty) break;
            }
            if (foundEmpty) break;
        }

        if (!foundEmpty)
        {
            // 官方 0x4c5343: cmp v12, 8
            if (minStack >= 8)
            {
                bestPos = new(centerX, centerY); // 堆满强制回退中心原点
            }
        }

        return (bestPos, foundEmpty, minStack, 8);
    }

    /// <summary>
    /// 官方 8 方向切比雪夫对齐与偏角映射算法实证 (M2Server sub_4B2C38 @ 0x4B2C38 ~ 0x4B2D6A)
    /// 方向: 0=北, 1=东北, 2=东, 3=东南, 4=南, 5=西南, 6=西, 7=西北
    /// </summary>
    public static int EvaluateDirection(int srcX, int srcY, int dstX, int dstY)
    {
        int dx = 0;
        if (srcX < dstX) dx = 1;
        else if (srcX > dstX) dx = -1;

        int dy = 0;
        if (srcY < dstY) dy = 1;
        else if (srcY > dstY) dy = -1;

        // 官方近轴修正逻辑 (0x4b2c92 & 0x4b2ce0)
        if (Math.Abs(srcY - dstY) > 2 && Math.Abs(srcX - dstX) <= 1)
        {
            dx = 0;
        }
        if (Math.Abs(srcX - dstX) > 2 && Math.Abs(srcY - dstY) <= 1)
        {
            dy = 0;
        }

        return (dx, dy) switch
        {
            (0, -1) => 0,  // 北
            (1, -1) => 1,  // 东北
            (1, 0) => 2,   // 东
            (1, 1) => 3,   // 东南
            (0, 1) => 4,   // 南
            (-1, 1) => 5,  // 西南
            (-1, 0) => 6,  // 西
            (-1, -1) => 7, // 西北
            _ => 4         // 默认南
        };
    }
}
