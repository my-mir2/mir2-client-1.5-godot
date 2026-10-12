using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Mir2.Server.Game;
using Mir2.Shared.Models;

namespace Mir2.Server.Persistence;

/// <summary>
/// 无锁异步持久化工作管道 (基于 System.Threading.Channels)
/// 将磁盘 IO 彻底从 50ms 游戏主世界心跳中解耦，彻底杜绝 NVMe 写入抖动引发的卡帧
/// </summary>
public sealed class AsyncPersistenceWorker : IDisposable
{
    private enum SaveTaskType
    {
        Player,
        World
    }

    private sealed class SaveTask
    {
        public SaveTaskType Type { get; init; }
        public string Key { get; init; } = string.Empty;
        public PlayerState? PlayerSnapshot { get; init; }
        public int MapId { get; init; }
        public int X { get; init; }
        public int Y { get; init; }
        public SabukTreasuryEngine? Treasury { get; init; }
        public GuildEngine? Guilds { get; init; }
        public TaskCompletionSource<bool>? Completion { get; init; }
    }

    private readonly DataPersistenceEngine _persistenceEngine;
    private readonly Channel<SaveTask> _channel;
    private readonly CancellationTokenSource _cts;
    private readonly Task _workerTask;
    private readonly ConcurrentDictionary<string, long> _pendingTasks = new(StringComparer.OrdinalIgnoreCase);
    private bool _isDisposed;

    public int PendingCount => _pendingTasks.Count;

    public AsyncPersistenceWorker(DataPersistenceEngine persistenceEngine, int capacity = 10000)
    {
        _persistenceEngine = persistenceEngine;
        _channel = Channel.CreateBounded<SaveTask>(new BoundedChannelOptions(capacity)
        {
            FullMode = BoundedChannelFullMode.Wait,
            SingleReader = true,
            SingleWriter = false
        });
        _cts = new CancellationTokenSource();
        _workerTask = Task.Run(ProcessQueueAsync);
    }

    /// <summary>
    /// 将玩家存盘请求异步推入落盘队列 (主线程纳秒级返回，零阻塞)
    /// </summary>
    public bool EnqueueSavePlayer(string playerName, PlayerState state, int mapId, int x, int y, TaskCompletionSource<bool>? completion = null)
    {
        if (_isDisposed || string.IsNullOrWhiteSpace(playerName) || state == null) return false;

        // 去重防抖记录
        _pendingTasks[playerName] = Environment.TickCount64;

        var task = new SaveTask
        {
            Type = SaveTaskType.Player,
            Key = playerName,
            PlayerSnapshot = state, // 引用或浅克隆
            MapId = mapId,
            X = x,
            Y = y,
            Completion = completion
        };

        return _channel.Writer.TryWrite(task);
    }

    /// <summary>
    /// 将世界大盘数据异步推入落盘队列
    /// </summary>
    public bool EnqueueSaveWorld(SabukTreasuryEngine treasury, GuildEngine guildEngine, TaskCompletionSource<bool>? completion = null)
    {
        if (_isDisposed) return false;

        var task = new SaveTask
        {
            Type = SaveTaskType.World,
            Key = "__WORLD__",
            Treasury = treasury,
            Guilds = guildEngine,
            Completion = completion
        };

        return _channel.Writer.TryWrite(task);
    }

    private async Task ProcessQueueAsync()
    {
        var reader = _channel.Reader;
        while (!_cts.Token.IsCancellationRequested)
        {
            try
            {
                if (await reader.WaitToReadAsync(_cts.Token))
                {
                    while (reader.TryRead(out var task))
                    {
                        bool success = false;
                        try
                        {
                            if (task.Type == SaveTaskType.Player && task.PlayerSnapshot != null)
                            {
                                success = _persistenceEngine.SavePlayer(task.Key, task.PlayerSnapshot, task.MapId, task.X, task.Y);
                            }
                            else if (task.Type == SaveTaskType.World && task.Treasury != null && task.Guilds != null)
                            {
                                success = _persistenceEngine.SaveWorld(task.Treasury, task.Guilds);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[AsyncPersistence] 异步落盘任务处理异常: {ex.Message}");
                        }
                        finally
                        {
                            _pendingTasks.TryRemove(task.Key, out _);
                            task.Completion?.TrySetResult(success);
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AsyncPersistence] 队列通道异常: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 强制冲刷等待所有已入队任务落盘完毕 (供优雅关机使用)
    /// </summary>
    public async Task FlushAsync(int timeoutMs = 5000)
    {
        long start = Environment.TickCount64;
        while (_pendingTasks.Count > 0 && Environment.TickCount64 - start < timeoutMs)
        {
            await Task.Delay(20);
        }
    }

    public void Dispose()
    {
        if (_isDisposed) return;
        _isDisposed = true;
        _channel.Writer.Complete();
        _cts.Cancel();
        try
        {
            _workerTask.Wait(1000);
        }
        catch
        {
            // 忽略任务中断
        }
        _cts.Dispose();
    }
}
