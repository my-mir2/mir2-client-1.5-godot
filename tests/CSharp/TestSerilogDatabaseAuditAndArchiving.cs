using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Mir2.Server.Core;
using Mir2.Server.Logging;
using Mir2.Server.Persistence;
using Npgsql;
using Serilog;
using Xunit;

namespace Mir2.Tests;

/// <summary>
/// Serilog 工业级日志引擎、PostgreSQL 数据库审计日志与数据归档全生命周期测试
/// </summary>
public sealed class TestSerilogDatabaseAuditAndArchiving
{
    private readonly string _pgConn;

    public TestSerilogDatabaseAuditAndArchiving()
    {
        _pgConn = Environment.GetEnvironmentVariable("MIR2_PG_CONN")
            ?? "Host=127.0.0.1;Port=5432;Database=mir2_world;Username=postgres;Password=postgres";
    }

    [Fact]
    public void Test_Serilog_Local_File_And_Audit_Formatting()
    {
        // 1. 初始化临时测试日志目录
        string tempDir = Path.Combine(Path.GetTempPath(), "mir2_serilog_test_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        try
        {
            var config = new ServerConfig
            {
                LogDirectory = tempDir,
                MaxLogFileSizeMb = 10,
                LogRetentionDays = 7,
                EnableDatabaseLogging = false // 本项仅测 Serilog 文件与格式化
            };

            ServerLogger.Initialize(config, null);

            // 写入日志
            Log.Information("测试服务启动就绪，当前玩家在线数: {OnlineCount}", 42);
            ServerLogger.Audit("TEST_ACTION", "test_user", "烈火英雄", "拾取裁决之杖", "ItemUid", 99999);

            ServerLogger.CloseAndFlush();

            // 验证生成的日志文件
            string[] logFiles = Directory.GetFiles(tempDir, "server-*.log");
            Assert.NotEmpty(logFiles);

            string content = File.ReadAllText(logFiles[0]);
            Assert.Contains("测试服务启动就绪", content);
            Assert.Contains("42", content);
            Assert.Contains("烈火英雄", content);
            Assert.Contains("拾取裁决之杖", content);
        }
        finally
        {
            try { Directory.Delete(tempDir, true); } catch { }
        }
    }

    [Fact]
    public async Task Test_PostgreSql_Audit_Log_Sink_And_Archiving_Lifecycle()
    {
        // 1. 初始化数据库表结构
        var pgEngine = new PostgreSqlPersistenceEngine(_pgConn);
        pgEngine.InitializeSchema();

        string testAcc = "audit_tester_" + Guid.NewGuid().ToString("N")[..8];
        string testChar = "狂战_" + Guid.NewGuid().ToString("N")[..6];

        // 2. 直接测试 PostgreSqlAuditLogSink 批量入库
        using (var sink = new PostgreSqlAuditLogSink(_pgConn, queueCapacity: 1000, batchSize: 5, flushIntervalMs: 100))
        {
            var logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Sink(sink)
                .CreateLogger();

            // 写入普通系统日志
            logger.Information("测试系统网关启动成功");

            // 写入结构化安全审计日志
            logger.ForContext("Action", "LOGIN_SUCCESS")
                  .ForContext("Account", testAcc)
                  .ForContext("CharacterName", testChar)
                  .ForContext("ClientIp", "192.168.1.100")
                  .Information("玩家登录验证通过");

            logger.ForContext("Action", "BUY_ITEM")
                  .ForContext("Account", testAcc)
                  .ForContext("CharacterName", testChar)
                  .ForContext("ItemName", "屠龙刀")
                  .Information("向比奇铁匠购买神兵");

            // 等待入库冲刷
            await Task.Delay(500);
        }

        // 3. 验证数据库活跃表 server_audit_logs 中的记录与字段
        await using (var conn = new NpgsqlConnection(_pgConn))
        {
            await conn.OpenAsync();
            await using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT action, account, character_name, properties FROM server_audit_logs WHERE account = @acc ORDER BY id ASC;";
            cmd.Parameters.AddWithValue("acc", testAcc);

            await using var reader = await cmd.ExecuteReaderAsync();
            
            // 第一条: LOGIN_SUCCESS
            Assert.True(await reader.ReadAsync(), "未在 server_audit_logs 中查询到审计记录！");
            Assert.Equal("LOGIN_SUCCESS", reader.GetString(0));
            Assert.Equal(testAcc, reader.GetString(1));
            Assert.Equal(testChar, reader.GetString(2));
            string props1 = reader.IsDBNull(3) ? "" : reader.GetString(3);
            Assert.Contains("ClientIp", props1);

            // 第二条: BUY_ITEM
            Assert.True(await reader.ReadAsync());
            Assert.Equal("BUY_ITEM", reader.GetString(0));
            string props2 = reader.IsDBNull(3) ? "" : reader.GetString(3);
            Assert.Contains("屠龙刀", props2);
        }

        // 4. 测试数据库日志归档引擎 (DatabaseLogArchiveEngine)
        var archiveEngine = new DatabaseLogArchiveEngine(_pgConn);

        // 4.1 模拟插入 3 条 40 天前的旧日志，以及 1 条当前的新日志
        DateTime oldDateUtc = DateTime.UtcNow.AddDays(-40);
        await using (var conn = new NpgsqlConnection(_pgConn))
        {
            await conn.OpenAsync();
            await using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO server_audit_logs (timestamp, level, message, action, account, character_name)
                VALUES 
                    (@oldDate, 'Information', '40天前旧日志1', 'TRADE', @acc, @chr),
                    (@oldDate, 'Information', '40天前旧日志2', 'DROP', @acc, @chr),
                    (@oldDate, 'Information', '40天前旧日志3', 'KILL', @acc, @chr),
                    (NOW(), 'Information', '今日新日志', 'CHAT', @acc, @chr);
            ";
            cmd.Parameters.AddWithValue("oldDate", oldDateUtc);
            cmd.Parameters.AddWithValue("acc", testAcc);
            cmd.Parameters.AddWithValue("chr", testChar);
            await cmd.ExecuteNonQueryAsync();
        }

        // 4.2 触发归档: 归档 30 天前的旧日志
        long archivedCount = await archiveEngine.ArchiveOldLogsAsync(retentionDays: 30, batchSize: 100);
        Assert.True(archivedCount >= 3, $"归档行数预期至少为 3，实际为 {archivedCount}");

        // 4.3 验证活跃表与归档表中的分布状态
        await using (var conn = new NpgsqlConnection(_pgConn))
        {
            await conn.OpenAsync();

            // 检查活跃表: 40 天前的旧日志已被删除，今日新日志依然存在
            await using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT COUNT(*) FROM server_audit_logs WHERE account = @acc AND message LIKE '%40天前旧日志%';";
                cmd.Parameters.AddWithValue("acc", testAcc);
                long remainingOld = (long)(await cmd.ExecuteScalarAsync() ?? 0);
                Assert.Equal(0, remainingOld);
            }

            // 检查归档表: 40 天前的旧日志已成功转入归档表
            await using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT COUNT(*) FROM server_audit_logs_archive WHERE account = @acc AND message LIKE '%40天前旧日志%';";
                cmd.Parameters.AddWithValue("acc", testAcc);
                long archivedOld = (long)(await cmd.ExecuteScalarAsync() ?? 0);
                Assert.Equal(3, archivedOld);
            }
        }

        // 4.4 验证统计接口
        var stats = await archiveEngine.GetLogStatisticsAsync();
        Assert.True(stats.ActiveLogCount >= 1);
        Assert.True(stats.ArchiveLogCount >= 3);

        // 4.5 测试导出 CSV
        string csvPath = Path.Combine(Path.GetTempPath(), $"audit_export_{Guid.NewGuid():N}.csv");
        try
        {
            int exported = await archiveEngine.ExportLogsToCsvAsync(csvPath, includeArchive: true);
            Assert.True(exported >= 3);
            Assert.True(File.Exists(csvPath));
            string csvHeader = (await File.ReadAllLinesAsync(csvPath))[0];
            Assert.Contains("Timestamp", csvHeader);
            Assert.Contains("Action", csvHeader);
            Assert.Contains("Account", csvHeader);
        }
        finally
        {
            try { File.Delete(csvPath); } catch { }
        }

        // 4.6 测试归档清理 (Purge)
        // 传入 35 天作为清理门槛，之前 40 天前的旧日志应被物理清除
        long purged = await archiveEngine.PurgeArchiveLogsAsync(archiveRetentionDays: 35);
        Assert.True(purged >= 3, $"清理行数应至少为 3，实际为 {purged}");
    }
}
