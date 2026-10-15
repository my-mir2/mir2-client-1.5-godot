param(
    [Parameter(Mandatory=$true)]
    [string]$BackupFile,
    [string]$PgBinDir = "d:\tools\pgsql_bin\pgsql\bin"
)

if (!(Test-Path $BackupFile)) {
    Write-Host "错误: 备份文件不存在: $BackupFile" -ForegroundColor Red
    exit 1
}

$restoreExe = Join-Path $PgBinDir "pg_restore.exe"

Write-Host "=== 开始执行 PostgreSQL 物理快照原子还原 ===" -ForegroundColor Cyan
Write-Host "快照源: $BackupFile" -ForegroundColor Yellow

# 执行 pg_restore (clean 清理旧表后重新建表与导入数据)
& $restoreExe -h 127.0.0.1 -p 5432 -U postgres -d mir2_world --clean --if-exists -v $BackupFile

if ($LASTEXITCODE -eq 0 -or $LASTEXITCODE -eq 1) {
    Write-Host "还原完成！" -ForegroundColor Green
} else {
    Write-Host "还原遇到错误，退出码: $LASTEXITCODE" -ForegroundColor Red
    exit 1
}
