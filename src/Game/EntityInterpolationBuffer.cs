using System;
using System.Collections.Generic;
using Godot;

namespace Mir2Client.Game;

/// <summary>
/// 客户端远端实体（其他玩家、怪物、NPC）弱网抖动平滑插值缓冲池 (Snapshot Interpolation Buffer)
/// 专为解决公网 50~150ms 延迟波动与偶发丢包引发的怪物/玩家“瞬移瞬跳、闪烁拉扯”而设计。
/// 原理：在客户端维护轻量快照历史环形队列，以固定的插值延迟（如 80ms~100ms）在两个历史快照之间进行平滑过渡插值。
/// </summary>
public sealed class EntityInterpolationBuffer
{
    public readonly record struct Snapshot(
        double Timestamp,
        Vector2 WorldPosition,
        byte Direction,
        bool IsMoving,
        bool IsRunning);

    private readonly List<Snapshot> _snapshots = new(16);
    private readonly int _maxCapacity;
    public double InterpolationDelaySeconds { get; set; } = 0.080; // 80ms 柔性插值窗口

    public EntityInterpolationBuffer(int maxCapacity = 16)
    {
        _maxCapacity = maxCapacity;
    }

    /// <summary>
    /// 压入从服务端下发的最新实体位置快照
    /// </summary>
    public void PushSnapshot(double receiveTimestamp, Vector2 worldPos, byte direction, bool isMoving, bool isRunning)
    {
        // 保证时间戳单调递增，剔除网络乱序包
        if (_snapshots.Count > 0 && receiveTimestamp <= _snapshots[^1].Timestamp)
        {
            return;
        }

        if (_snapshots.Count >= _maxCapacity)
        {
            _snapshots.RemoveAt(0);
        }

        _snapshots.Add(new Snapshot(receiveTimestamp, worldPos, direction, isMoving, isRunning));
    }

    /// <summary>
    /// 根据当前客户端时钟计算出平滑渲染的世界坐标与朝向
    /// </summary>
    public (Vector2 RenderPos, byte Direction, bool IsMoving) Sample(double currentClientTime)
    {
        if (_snapshots.Count == 0)
        {
            return (Vector2.Zero, 0, false);
        }

        if (_snapshots.Count == 1)
        {
            var single = _snapshots[0];
            return (single.WorldPosition, single.Direction, single.IsMoving);
        }

        double targetRenderTime = currentClientTime - InterpolationDelaySeconds;

        // 如果渲染时间比最旧的快照还要早，直接返回最旧的快照
        if (targetRenderTime <= _snapshots[0].Timestamp)
        {
            var oldest = _snapshots[0];
            return (oldest.WorldPosition, oldest.Direction, oldest.IsMoving);
        }

        // 如果渲染时间比最新的快照还要晚（网络严重拥塞），进行推演或返回最新
        if (targetRenderTime >= _snapshots[^1].Timestamp)
        {
            var newest = _snapshots[^1];
            return (newest.WorldPosition, newest.Direction, newest.IsMoving);
        }

        // 在快照序列中寻找左右包围的两个快照 [prev, next] 进行线性插值
        for (int i = 0; i < _snapshots.Count - 1; i++)
        {
            var prev = _snapshots[i];
            var next = _snapshots[i + 1];

            if (targetRenderTime >= prev.Timestamp && targetRenderTime <= next.Timestamp)
            {
                double duration = next.Timestamp - prev.Timestamp;
                float t = duration > 0.0001 ? (float)((targetRenderTime - prev.Timestamp) / duration) : 0f;
                t = Math.Clamp(t, 0f, 1f);

                // 距离过大（如传送、跨图拉回）超过 200 像素时直接跳跃，不做生硬长途滑行
                float dist = prev.WorldPosition.DistanceTo(next.WorldPosition);
                if (dist > 200.0f)
                {
                    return (t < 0.5f ? prev.WorldPosition : next.WorldPosition, next.Direction, next.IsMoving);
                }

                // 物理坐标平滑线性插值
                Vector2 interpolatedPos = prev.WorldPosition.Lerp(next.WorldPosition, t);
                return (interpolatedPos, next.Direction, next.IsMoving);
            }
        }

        var fallback = _snapshots[^1];
        return (fallback.WorldPosition, fallback.Direction, fallback.IsMoving);
    }

    public void Clear() => _snapshots.Clear();
    public int Count => _snapshots.Count;
}
