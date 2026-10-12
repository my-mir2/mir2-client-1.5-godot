using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Mir2.Server.Network;

namespace Mir2.Server.Core;

/// <summary>
/// 盛大官方 1.5 权威 100ms 物理世界心跳与主循环调度器 (GameWorldTick)
/// 核心职责：
/// 1. 严格以 100ms (10Hz) 为周期驱动物理世界世界状态机、怪物 AI、技能光效与伤害判定；
/// 2. 在每个 Tick 末尾统一调用网关执行【出包批量合并冲刷 (Batch Packet Flushing)】；
/// 3. 全链路低延迟与微秒级精准度监控 (监控单 Tick 耗时与 CPU 负荷)。
/// </summary>
public sealed class GameWorldTick
{
    public const int TargetTickIntervalMs = 100; // 官方 100ms 心跳

    private readonly TcpGateway _gateway;
    private bool _isRunning;
    private long _tickCount;
    private double _lastTickDurationMs;
    private double _maxTickDurationMs;

    public long CurrentTick => _tickCount;
    public double LastTickDurationMs => _lastTickDurationMs;
    public double MaxTickDurationMs => _maxTickDurationMs;

    // 每一帧的业务逻辑回调: (当前Tick号, 距离上一帧的时间间隔毫秒)
    public Action<long, double>? WorldUpdateHandler { get; set; }

    public GameWorldTick(TcpGateway gateway)
    {
        _gateway = gateway;
    }

    public GameWorldTick(TcpGateway gateway, Game.PacketDispatcher dispatcher)
    {
        _gateway = gateway;
        WorldUpdateHandler = (tick, dt) => dispatcher.UpdateWorldTick(tick, Environment.TickCount64);
    }

    public void Start()
    {
        _isRunning = true;
        var thread = new Thread(RunLoop)
        {
            Name = "Mir2.WorldTickThread",
            IsBackground = true,
            Priority = ThreadPriority.Highest // 提升主循环线程优先级，确保微秒级稳定
        };
        thread.Start();
        Console.WriteLine($"[GameWorldTick] 100ms 物理世界心跳引擎已启动 (调度精度: 100ms/Tick, 线程优先级: Highest)...");
    }

    private void RunLoop()
    {
        var sw = new Stopwatch();
        var tickSw = new Stopwatch();

        sw.Start();
        long nextTickTime = sw.ElapsedMilliseconds;

        while (_isRunning)
        {
            long now = sw.ElapsedMilliseconds;
            if (now >= nextTickTime)
            {
                _tickCount++;
                tickSw.Restart();

                // 1. 驱动世界状态机、技能与实体更新
                try
                {
                    WorldUpdateHandler?.Invoke(_tickCount, TargetTickIntervalMs);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[GameWorldTick] Tick #{_tickCount} 逻辑异常: {ex.Message}");
                }

                // 2. 【核心架构死穴】100ms 末尾统一合包冲刷网卡
                _gateway.FlushAllOutboundBatches();

                tickSw.Stop();
                _lastTickDurationMs = tickSw.Elapsed.TotalMilliseconds;
                if (_lastTickDurationMs > _maxTickDurationMs)
                {
                    _maxTickDurationMs = _lastTickDurationMs;
                }

                // 规划下一次 Tick 时间（追赶时钟，防止漂移）
                nextTickTime += TargetTickIntervalMs;
                if (now - nextTickTime > TargetTickIntervalMs * 5)
                {
                    // 若发生严重掉帧卡顿，重置下一次 Tick 时间
                    nextTickTime = now + TargetTickIntervalMs;
                }
            }
            else
            {
                int sleepMs = (int)(nextTickTime - now);
                if (sleepMs > 1)
                {
                    Thread.Sleep(1);
                }
                else
                {
                    Thread.Yield();
                }
            }
        }
    }

    public void Stop()
    {
        _isRunning = false;
    }
}
