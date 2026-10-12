using System;
using System.IO;
using Mir2.Server.Core;
using Npgsql;
using Serilog;
using Serilog.Events;

namespace Mir2.Server.Logging;

/// <summary>
/// 盛大官方 1.5 工业级全服统一日志门面 (全面融入 .NET 8/9/10 与 C# 12 现代化特性)
/// 特性矩阵：
/// 1. Serilog 4.4 门面聚合 Console、File 与 PostgreSQL Audit Sink；
/// 2. NpgsqlDataSource 物理连接池统一复用；
/// 3. TimeProvider 现代化高精度时间采样；
/// 4. 结构化审计 API (Audit) 记录不可抵赖证据链。
/// </summary>
public static class ServerLogger
{
    private static NpgsqlDataSource? _dataSource;
    private static PostgreSqlAuditLogSink? _pgSink;
    private static DatabaseLogArchiveEngine? _archiveEngine;
    private static bool _isInitialized;

    public static DatabaseLogArchiveEngine? ArchiveEngine => _archiveEngine;
    public static NpgsqlDataSource? DataSource => _dataSource;

    public static void Initialize(ServerConfig config, string? pgConnectionString = null, TimeProvider? timeProvider = null)
    {
        if (_isInitialized) return;

        timeProvider ??= TimeProvider.System;
        string logDir = Path.GetFullPath(config.LogDir);
        Directory.CreateDirectory(logDir);

        long maxSizeBytes = (long)config.LogMaxSizeMb * 1024 * 1024;
        string filePattern = Path.Combine(logDir, "server-.log");

        var loggerConfig = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.FromLogContext()
            .WriteTo.Console(
                restrictedToMinimumLevel: LogEventLevel.Information,
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
            )
            .WriteTo.File(
                path: filePattern,
                restrictedToMinimumLevel: LogEventLevel.Information,
                rollingInterval: RollingInterval.Day,
                fileSizeLimitBytes: maxSizeBytes,
                rollOnFileSizeLimit: true,
                retainedFileCountLimit: config.LogRetentionDays,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}"
            );

        if (!string.IsNullOrWhiteSpace(pgConnectionString))
        {
            _dataSource = NpgsqlDataSource.Create(pgConnectionString);
            _pgSink = new PostgreSqlAuditLogSink(_dataSource);
            _archiveEngine = new DatabaseLogArchiveEngine(_dataSource);
            loggerConfig.WriteTo.Sink(_pgSink, restrictedToMinimumLevel: LogEventLevel.Information);
        }

        Log.Logger = loggerConfig.CreateLogger();
        _isInitialized = true;

        Log.Information("[Serilog] 现代化日志引擎启动完毕 (目标: Console, RollingFile: {LogDir}, PostgreSQL: {HasPg})", 
            logDir, !string.IsNullOrWhiteSpace(pgConnectionString));
    }

    /// <summary>
    /// 记录结构化安全与业务审计日志 (C# 12 优化)
    /// </summary>
    public static void Audit(string action, string account, string characterName, string message, string? extraKey = null, object? extraVal = null)
    {
        var logger = Log.ForContext("Action", action)
                        .ForContext("Account", account)
                        .ForContext("CharacterName", characterName);

        if (!string.IsNullOrEmpty(extraKey) && extraVal is not null)
        {
            logger = logger.ForContext(extraKey, extraVal);
        }

        logger.Information("[审计/{Action}] 账号: {Account} | 角色: {CharacterName} | {Message}", action, account, characterName, message);
    }

    /// <summary>
    /// 优雅刷新并关闭日志资源
    /// </summary>
    public static void CloseAndFlush()
    {
        Log.Information("[Serilog] 正在优雅关闭日志系统并排空队列...");
        Log.CloseAndFlush();
        _pgSink?.Dispose();
        _dataSource?.Dispose();
        _pgSink = null;
        _dataSource = null;
        _archiveEngine = null;
        _isInitialized = false;
    }
}
