param(
    [int]$RetentionDays = 30,
    [int]$PurgeArchiveDays = 180
)

$pgBin = "d:\tools\pgsql_bin\pgsql\bin"
$env:PGPASSWORD = "postgres"

Write-Host "=== PostgreSQL 审计日志归档与生命周期运维 ===" -ForegroundColor Cyan
Write-Host "活跃表留存门槛: $RetentionDays 天 | 归档表清理门槛: $PurgeArchiveDays 天" -ForegroundColor Gray

# 1. 执行原子分批归档 SQL
$archiveSql = @"
WITH moved_rows AS (
    DELETE FROM server_audit_logs
    WHERE timestamp < NOW() - INTERVAL '$RetentionDays days'
    RETURNING id, timestamp, level, message, exception, source_context, action, account, character_name, properties
)
INSERT INTO server_audit_logs_archive (id, timestamp, level, message, exception, source_context, action, account, character_name, properties, archived_at)
SELECT id, timestamp, level, message, exception, source_context, action, account, character_name, properties, NOW()
FROM moved_rows
ON CONFLICT (id) DO NOTHING;

DELETE FROM server_audit_logs_archive WHERE timestamp < NOW() - INTERVAL '$PurgeArchiveDays days';
"@

$psqlExe = Join-Path $pgBin "psql.exe"
if (Test-Path $psqlExe) {
    & $psqlExe -h 127.0.0.1 -p 5432 -U postgres -d mir2_world -c $archiveSql | Out-Null
    
    # 2. 查询当前日志大盘统计
    $statsSql = "SELECT '活跃审计日志' AS 分类, COUNT(*) AS 条数, MIN(timestamp) AS 最早记录, MAX(timestamp) AS 最新记录 FROM server_audit_logs UNION ALL SELECT '历史归档日志', COUNT(*), MIN(timestamp), MAX(timestamp) FROM server_audit_logs_archive;"
    Write-Host "`n[数据库日志态势大盘]:" -ForegroundColor Green
    & $psqlExe -h 127.0.0.1 -p 5432 -U postgres -d mir2_world -c $statsSql
} else {
    Write-Host "未找到 psql.exe，请检查 PostgreSQL 安装路径: $psqlExe" -ForegroundColor Red
}
