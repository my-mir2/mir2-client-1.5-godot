param(
    [string]$PgBinDir = "d:\tools\pgsql_bin\pgsql\bin",
    [string]$BackupDir = "backups",
    [int]$RetentionDays = 7
)

$dateStr = Get-Date -Format "yyyyMMdd_HHmmss"
if (!(Test-Path $BackupDir)) {
    New-Item -ItemType Directory -Path $BackupDir -Force | Out-Null
}

$dumpExe = Join-Path $PgBinDir "pg_dump.exe"
$backupFile = Join-Path $BackupDir "mir2_world_$dateStr.dump"

Write-Host "=== 开始执行 PostgreSQL 工业级数据库物理快照备份 ===" -ForegroundColor Cyan
Write-Host "目标备份文件: $backupFile" -ForegroundColor Yellow

& $dumpExe -h 127.0.0.1 -p 5432 -U postgres -d mir2_world -F c -b -v -f $backupFile

if ($LASTEXITCODE -eq 0 -and (Test-Path $backupFile)) {
    $size = (Get-Item $backupFile).Length
    Write-Host "备份成功！文件大小: $size 字节" -ForegroundColor Green
} else {
    Write-Host "备份失败！请检查数据库是否正常运行与权限！" -ForegroundColor Red
    exit 1
}

# 清理超期备份
$cutoff = (Get-Date).AddDays(-$RetentionDays)
Get-ChildItem -Path $BackupDir -Filter "mir2_world_*.dump" | Where-Object { $_.LastWriteTime -lt $cutoff } | ForEach-Object {
    Write-Host "清理过期备份: $($_.FullName)" -ForegroundColor DarkGray
    Remove-Item $_.FullName -Force
}

Write-Host "=== 数据库备份与生命周期轮转完成 100% ===" -ForegroundColor Cyan
