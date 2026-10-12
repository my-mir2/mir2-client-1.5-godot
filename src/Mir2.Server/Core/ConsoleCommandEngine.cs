using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Mir2.Server.Game;
using Mir2.Server.Maps;
using Mir2.Server.Network;

namespace Mir2.Server.Core;

/// <summary>
/// 服务端管理员控制台交互 CLI 与优雅关机调度器 (ConsoleCommandEngine)
/// </summary>
public sealed class ConsoleCommandEngine
{
    private readonly TcpGateway _gateway;
    private readonly PacketDispatcher _dispatcher;
    private readonly GameWorldTick _worldTick;
    private readonly MapManager _mapManager;
    private readonly MonsterRegenEngine? _regenEngine;
    private readonly ModernNpcPluginEngine _pluginEngine;
    private readonly NpcPluginDirectoryWatcher? _pluginWatcher;
    private bool _isShuttingDown;

    public ConsoleCommandEngine(
        TcpGateway gateway,
        PacketDispatcher dispatcher,
        GameWorldTick worldTick,
        MapManager mapManager,
        MonsterRegenEngine? regenEngine,
        ModernNpcPluginEngine pluginEngine,
        NpcPluginDirectoryWatcher? pluginWatcher = null)
    {
        _gateway = gateway;
        _dispatcher = dispatcher;
        _worldTick = worldTick;
        _mapManager = mapManager;
        _regenEngine = regenEngine;
        _pluginEngine = pluginEngine;
        _pluginWatcher = pluginWatcher;
    }

    /// <summary>
    /// 执行控制台单个命令
    /// </summary>
    public string ExecuteCommand(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;

        string[] parts = input.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        return parts switch
        {
            ["save"] => ExecuteSave(),
            ["reload"] => ExecuteReload(),
            ["stats"] => ExecuteStats(),
            ["kick", var target] => ExecuteKick(target),
            ["kick"] => "[CLI] 用法: kick <玩家名称>",
            ["clean"] => ExecuteClean(),
            ["archive_logs", var daysStr] when int.TryParse(daysStr, out int days) => ExecuteArchiveLogs(days),
            ["archive_logs"] => ExecuteArchiveLogs(30),
            ["log_stats"] => ExecuteLogStats(),
            ["stop" or "exit"] => ExecuteStop(),
            _ => "[CLI] 可用指令: save (全服存盘) | reload (热更插件) | stats (性能指标) | kick <玩家> (踢人) | clean (清理地表) | archive_logs [天数] (归档日志) | log_stats (日志态势) | stop (安全关机)"
        };
    }

    private string ExecuteSave()
    {
        int savedPlayers = _dispatcher.SaveAllActiveSessions(_gateway.AllSessions);
        bool worldSaved = _dispatcher.PersistenceEngine.SaveWorld(_dispatcher.TreasuryEngine, _dispatcher.GuildEngine);
        return $"[CLI] 全量存盘完成: 已保存在线玩家 {savedPlayers} 人, 世界状态: {(worldSaved ? "成功" : "失败")}";
    }

    private string ExecuteReload()
    {
        string pluginPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins", "Mir2.ScriptPlugin.dll");
        if (!File.Exists(pluginPath))
        {
            pluginPath = Path.Combine(Directory.GetCurrentDirectory(), "plugins", "Mir2.ScriptPlugin.dll");
        }
        if (File.Exists(pluginPath))
        {
            var res = _pluginEngine.LoadPluginsFromFile(pluginPath);
            return $"[CLI] 脚本插件手动热更成功: 装载 {res.LoadedCount} 个 NPC 行为树";
        }
        return $"[CLI] 未找到插件文件: {pluginPath}";
    }

    private string ExecuteStats()
    {
        int online = _gateway.ActiveConnectionsCount;
        int monsters = _regenEngine?.TotalActiveMonsters ?? 0;
        long totalMem = GC.GetTotalMemory(false) / 1024 / 1024;
        var map0 = _mapManager.GetMap(0);
        int groundCount = map0?.GetAllGroundItems().Count ?? 0;
        return $"[CLI] 在线: {online}人 | 怪物: {monsters}只 | 地表物: {groundCount}件 | 内存: {totalMem}MB | Tick: {_worldTick.LastTickDurationMs:F2}ms (峰值: {_worldTick.MaxTickDurationMs:F2}ms)";
    }

    private string ExecuteKick(string target)
    {
        foreach (var s in _gateway.AllSessions)
        {
            if (s.CharacterName.Equals(target, StringComparison.OrdinalIgnoreCase))
            {
                if (s.State != null)
                {
                    _dispatcher.PersistenceEngine.SavePlayer(s.CharacterName, s.State, s.CurrentMapId, s.X, s.Y);
                }
                s.Dispose();
                return $"[CLI] 玩家 {target} 已安全存盘并强制踢下线。";
            }
        }
        return $"[CLI] 未找到在线玩家: {target}";
    }

    private string ExecuteClean()
    {
        long now = Environment.TickCount64;
        int cleaned = 0;
        var map = _mapManager.GetMap(0);
        if (map != null)
        {
            cleaned = map.CleanExpiredGroundItems(now, 0);
        }
        return $"[CLI] 立即清理地表过期物品 {cleaned} 件";
    }

    private string ExecuteArchiveLogs(int retentionDays)
    {
        if (Mir2.Server.Logging.ServerLogger.ArchiveEngine != null)
        {
            long archived = Mir2.Server.Logging.ServerLogger.ArchiveEngine.ArchiveOldLogsAsync(retentionDays).GetAwaiter().GetResult();
            return $"[CLI] 日志归档完成: 已将早于 {retentionDays} 天前的 {archived} 条审计日志原子转移至归档表。";
        }
        return "[CLI] 数据库日志归档引擎未启用或未连接 PostgreSQL。";
    }

    private string ExecuteLogStats()
    {
        if (Mir2.Server.Logging.ServerLogger.ArchiveEngine != null)
        {
            var ls = Mir2.Server.Logging.ServerLogger.ArchiveEngine.GetLogStatisticsAsync().GetAwaiter().GetResult();
            return $"[CLI] 日志态势: 活跃日志 {ls.ActiveLogCount} 条 (范围: {ls.OldestActiveLogTime:yyyy-MM-dd HH:mm:ss} ~ {ls.NewestActiveLogTime:yyyy-MM-dd HH:mm:ss}) | 归档日志 {ls.ArchiveLogCount} 条 (最早: {ls.OldestArchiveLogTime:yyyy-MM-dd HH:mm:ss})";
        }
        return "[CLI] 数据库日志引擎未启用。";
    }

    private string ExecuteStop()
    {
        ShutdownGracefully();
        return "[CLI] 正在执行优雅关机并落盘...";
    }

    /// <summary>
    /// 执行全服优雅停机与数据安全落盘
    /// </summary>
    public void ShutdownGracefully()
    {
        if (_isShuttingDown) return;
        _isShuttingDown = true;

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n[服务端] 正在启动优雅关机流程，准备全服数据原子落盘...");
        Console.ResetColor();

        try
        {
            // 1. 停止监听器与插件监听
            _pluginWatcher?.Stop();

            // 2. 全量在线玩家数据安全存盘并排空落盘队列
            int saved = _dispatcher.SaveAllActiveSessions(_gateway.AllSessions);
            Console.WriteLine($"[服务端] 在线玩家数据已落盘保存: {saved} 人。");
            if (_dispatcher.PgPersistenceEngine != null)
            {
                _dispatcher.PgPersistenceEngine.FlushQueue();
                _dispatcher.PgPersistenceEngine.SaveWorld(_dispatcher.TreasuryEngine, _dispatcher.GuildEngine);
            }

            // 3. 全局沙巴克与行会世界数据文件备份存盘
            bool worldSaved = _dispatcher.PersistenceEngine.SaveWorld(_dispatcher.TreasuryEngine, _dispatcher.GuildEngine);
            Console.WriteLine($"[服务端] 全服世界与沙巴克金库数据已落盘: {(worldSaved ? "成功" : "失败")}");

            // 4. 停止心跳引擎与网络网关
            _worldTick.Stop();
            _gateway.Stop();

            // 5. 刷新排空并关闭 Serilog 审计日志通道
            Mir2.Server.Logging.ServerLogger.CloseAndFlush();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[服务端] 全链路安全落盘完成，服务正常退出。");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[服务端] 关机落盘异常: {ex.Message}");
        }
    }
}
