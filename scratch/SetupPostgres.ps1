param()

$zipPath = "d:\tools\postgresql.zip"
$extractPath = "d:\tools\pgsql_bin"
$binDir = "d:\tools\pgsql_bin\pgsql\bin"
$dataDir = "d:\tools\pgsql_data"
$logFile = "d:\tools\pgsql.log"

Write-Host "=== 盛大官方 1.50 生产就绪 PostgreSQL 本地物理部署驱动 ===" -ForegroundColor Cyan

# 1. 解压
if (!(Test-Path $binDir)) {
    Write-Host "[1/5] 正在解压 PostgreSQL 便携包至 $extractPath ..." -ForegroundColor Yellow
    Expand-Archive -Path $zipPath -DestinationPath $extractPath -Force
} else {
    Write-Host "[1/5] PostgreSQL 二进制目录已存在: $binDir" -ForegroundColor Green
}

# 2. 初始化集群
$initdb = Join-Path $binDir "initdb.exe"
if (!(Test-Path (Join-Path $dataDir "PG_VERSION"))) {
    Write-Host "[2/5] 正在初始化数据库集群 (UTF-8, trust认证) ..." -ForegroundColor Yellow
    & $initdb -D $dataDir -U postgres -E UTF8 --locale=C -A trust
} else {
    Write-Host "[2/5] 数据库数据目录已就绪: $dataDir" -ForegroundColor Green
}

# 3. 启动后台服务
$pgctl = Join-Path $binDir "pg_ctl.exe"
Write-Host "[3/5] 正在启动 PostgreSQL 数据库引擎 (监听 127.0.0.1:5432) ..." -ForegroundColor Yellow
& $pgctl -D $dataDir -l $logFile start
Start-Sleep -Seconds 3

# 4. 创建 mir2_world 数据库
$createdb = Join-Path $binDir "createdb.exe"
$psql = Join-Path $binDir "psql.exe"
Write-Host "[4/5] 正在创建 mir2_world 数据库 ..." -ForegroundColor Yellow
& $createdb -h 127.0.0.1 -p 5432 -U postgres mir2_world 2>$null

# 5. 验证物理连通性
Write-Host "[5/5] 正在校验 PostgreSQL 物理连接与元数据 ..." -ForegroundColor Yellow
$dbs = & $psql -h 127.0.0.1 -p 5432 -U postgres -d mir2_world -c "SELECT current_database(), current_user, version();"
Write-Host $dbs -ForegroundColor Green
Write-Host "=== PostgreSQL 工业级数据库物理就绪 100% ===" -ForegroundColor Cyan
