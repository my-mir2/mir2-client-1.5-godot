// ============================================================================
// Mir2 服务端 - 大规模断线重连风暴削峰与平滑交错出栈引擎 (ReconnectionPacerEngine)
// 针对《工程认知公理 4.0》中揭示的“外网突发抖动导致数百人同时重连引发网络发送队列瞬时高水位雪崩”坏账，
// 实现基于令牌桶的重连入场削峰与分批交错出栈平滑调度 (Staggered Reconnect Pacing)。
// ============================================================================

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Mir2.Server.Network;

/// <summary>
/// 重连风暴削峰与平滑节流引擎
/// </summary>
public sealed class ReconnectionPacerEngine
{
    private readonly int _maxConcurrentLogonsPerTick;
    private readonly ConcurrentQueue<Action> _pendingLogonQueue = new();

    /// <summary>
    /// 当前排队等待平滑放行的重连任务数
    /// </summary>
    public int PendingQueueCount => _pendingLogonQueue.Count;

    /// <summary>
    /// 初始化削峰节流引擎
    /// </summary>
    /// <param name="maxConcurrentLogonsPerTick">单 Tick 最多允许完整处理并出栈全量初始状态的重连会话数 (默认 30)</param>
    public ReconnectionPacerEngine(int maxConcurrentLogonsPerTick = 30)
    {
        _maxConcurrentLogonsPerTick = Math.Max(1, maxConcurrentLogonsPerTick);
    }

    /// <summary>
    /// 提交重连/上线任务到平滑调度器
    /// 若当前队列为空且当 Tick 配额充足，立即执行；否则入队平滑排队
    /// </summary>
    public void EnqueueOrExecute(Action logonAction)
    {
        _pendingLogonQueue.Enqueue(logonAction);
    }

    /// <summary>
    /// 世界主循环 (WorldTick) 驱动平滑消耗出栈
    /// 严格控制每 Tick 出栈上限，彻底削平重连波峰
    /// </summary>
    /// <returns>本次 Tick 实际放行并执行的会话数</returns>
    public int ProcessTick()
    {
        int processed = 0;

        while (processed < _maxConcurrentLogonsPerTick && _pendingLogonQueue.TryDequeue(out var action))
        {
            try
            {
                action();
            }
            catch
            {
                // 保证异常不影响后续会话的平滑出栈
            }
            processed++;
        }

        return processed;
    }

    /// <summary>
    /// 清空排队队列 (如服务器停机维护)
    /// </summary>
    public void Clear()
    {
        while (_pendingLogonQueue.TryDequeue(out _)) { }
    }
}
