using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.5 服务端插件自动化目录热监听器 (NpcPluginDirectoryWatcher)
/// 零阻塞、写锁防抖 (Debounce)、无缝热重载外部 C# 脚本插件动态库
/// </summary>
public sealed class NpcPluginDirectoryWatcher : IDisposable
{
    private readonly ModernNpcPluginEngine _pluginEngine;
    private readonly string _watchDirectory;
    private FileSystemWatcher? _watcher;
    private readonly object _lock = new();
    private CancellationTokenSource? _debounceCts;
    private bool _isDisposed;

    public event Action<string, int>? OnPluginHotReloaded;
    public event Action<string, Exception>? OnPluginLoadError;

    public NpcPluginDirectoryWatcher(ModernNpcPluginEngine pluginEngine, string watchDirectory = "plugins")
    {
        _pluginEngine = pluginEngine;
        _watchDirectory = Path.GetFullPath(watchDirectory);
        if (!Directory.Exists(_watchDirectory))
        {
            Directory.CreateDirectory(_watchDirectory);
        }
    }

    /// <summary>
    /// 启动文件目录热更监听
    /// </summary>
    public void Start()
    {
        lock (_lock)
        {
            if (_watcher != null) return;

            _watcher = new FileSystemWatcher(_watchDirectory, "*.dll")
            {
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.Size | NotifyFilters.CreationTime,
                IncludeSubdirectories = false,
                EnableRaisingEvents = true
            };

            _watcher.Changed += OnFileEvent;
            _watcher.Created += OnFileEvent;
            Console.WriteLine($"[NpcPluginWatcher] 插件热更监听器已启动，正在监听: {_watchDirectory}/*.dll");
        }
    }

    /// <summary>
    /// 停止监听
    /// </summary>
    public void Stop()
    {
        lock (_lock)
        {
            if (_watcher != null)
            {
                _watcher.EnableRaisingEvents = false;
                _watcher.Changed -= OnFileEvent;
                _watcher.Created -= OnFileEvent;
                _watcher.Dispose();
                _watcher = null;
            }

            _debounceCts?.Cancel();
            _debounceCts?.Dispose();
            _debounceCts = null;
        }
    }

    private void OnFileEvent(object sender, FileSystemEventArgs e)
    {
        if (_isDisposed) return;
        string fullPath = e.FullPath;

        lock (_lock)
        {
            _debounceCts?.Cancel();
            _debounceCts?.Dispose();
            _debounceCts = new CancellationTokenSource();
            var token = _debounceCts.Token;

            // 500ms 写锁防抖检测，避免文件复制传输中途读取导致 BadImageFormatException
            Task.Run(async () =>
            {
                try
                {
                    await Task.Delay(500, token);
                    if (token.IsCancellationRequested) return;

                    // 检查并等待写锁彻底释放
                    if (!WaitForFileUnlock(fullPath, maxWaitMs: 3000, token))
                    {
                        Console.WriteLine($"[NpcPluginWatcher] 警告: 插件文件 {fullPath} 超时仍被占用，跳过本次加载。");
                        return;
                    }

                    // 调用内存流无锁沙箱加载
                    var res = _pluginEngine.LoadPluginsFromFile(fullPath);
                    Console.WriteLine($"[NpcPluginWatcher] 自动化热重载成功: {Path.GetFileName(fullPath)} -> 装载 {res.LoadedCount} 个 NPC 强类型行为树。");
                    OnPluginHotReloaded?.Invoke(fullPath, res.LoadedCount);
                }
                catch (OperationCanceledException)
                {
                    // 防抖被新事件重置，正常忽略
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[NpcPluginWatcher] 热重载插件 {fullPath} 异常: {ex.Message}");
                    OnPluginLoadError?.Invoke(fullPath, ex);
                }
            }, token);
        }
    }

    /// <summary>
    /// 循环探针检测文件写锁是否释放 (避免 Partial Write 损坏)
    /// </summary>
    public static bool WaitForFileUnlock(string filePath, int maxWaitMs, CancellationToken token)
    {
        long start = Environment.TickCount64;
        while (Environment.TickCount64 - start < maxWaitMs)
        {
            if (token.IsCancellationRequested) return false;

            try
            {
                using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None);
                if (fs.Length > 0)
                {
                    return true;
                }
            }
            catch (IOException)
            {
                // 文件仍被写入进程占用
            }
            catch (UnauthorizedAccessException)
            {
                // 权限临时锁定
            }

            Thread.Sleep(100);
        }

        return false;
    }

    public void Dispose()
    {
        if (_isDisposed) return;
        _isDisposed = true;
        Stop();
    }
}
