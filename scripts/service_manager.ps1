param(
    [ValidateSet("start", "stop", "restart", "status", "backup", "archive", "logs")]
    [string]$Action = "status"
)

$rootDir = Split-Path -Parent $PSScriptRoot
$pgBin = "d:\tools\pgsql_bin\pgsql\bin"
$pgData = "d:\tools\pgsql_data"
$serverExe = Join-Path $rootDir "bin\server_release\Mir2.Server.exe"
$logDir = Join-Path (Split-Path $serverExe) "logs"

function Check-PgStatus {
    $pg = Get-Process -Name postgres -ErrorAction SilentlyContinue
    return $pg.Count -gt 0
}

function Check-ServerStatus {
    $srv = Get-Process -Name "Mir2.Server" -ErrorAction SilentlyContinue
    return $srv.Count -gt 0
}

switch ($Action) {
    "status" {
        Write-Host "=== 盛大热血传奇 1.5 生产运维态势监控 ===" -ForegroundColor Cyan
        $pgRunning = Check-PgStatus
        $srvRunning = Check-ServerStatus

        $pgText = if ($pgRunning) { '[运行中 (端口: 5432)]' } else { '[已停止]' }
        $pgColor = if ($pgRunning) { "Green" } else { "Red" }
        $srvText = if ($srvRunning) { '[运行中 (网关: 7000, Web: 8080)]' } else { '[已停止]' }
        $srvColor = if ($srvRunning) { "Green" } else { "Red" }

        Write-Host "PostgreSQL 数据库: $pgText" -ForegroundColor $pgColor
        Write-Host "Mir2.Server 服务端: $srvText" -ForegroundColor $srvColor
        
        if ($srvRunning) {
            $proc = Get-Process -Name "Mir2.Server" | Select-Object -First 1
            Write-Host "进程 ID: $($proc.Id) | 工作集内存: $([math]::Round($proc.WorkingSet64 / 1MB, 2)) MB | CPU 耗时: $([math]::Round($proc.TotalProcessorTime.TotalSeconds, 2)) 秒" -ForegroundColor Yellow
            try {
                $rawJson = curl.exe -s http://127.0.0.1:8080/api/stats
                if ($rawJson) {
                    Write-Host "实时遥测: $rawJson" -ForegroundColor Gray
                }
            } catch {}
        }
    }

    "start" {
        Write-Host "=== 启动生产服务端与依赖组件 ===" -ForegroundColor Cyan
        if (!(Check-PgStatus)) {
            Write-Host "正在启动 PostgreSQL 数据库引擎..." -ForegroundColor Yellow
            Start-Process -FilePath "$pgBin\postgres.exe" -ArgumentList "-D `"$pgData`"" -WindowStyle Hidden
            Start-Sleep -Seconds 2
        }
        if (!(Check-ServerStatus)) {
            Write-Host "正在拉起 Mir2.Server Native AOT 进程..." -ForegroundColor Yellow
            Start-Process -FilePath $serverExe -WorkingDirectory (Split-Path $serverExe) -WindowStyle Minimized
            Start-Sleep -Seconds 2
        }
        & $MyInvocation.MyCommand.Path -Action status
    }

    "stop" {
        Write-Host "=== 正在执行全服优雅关机流程 ===" -ForegroundColor Yellow
        Get-Process -Name "Mir2.Server" -ErrorAction SilentlyContinue | ForEach-Object {
            $_.CloseMainWindow() | Out-Null
            Start-Sleep -Seconds 1
            if (!$_.HasExited) { Stop-Process -Id $_.Id -Force }
        }
        Write-Host "服务端已安全退出。" -ForegroundColor Green
    }

    "restart" {
        & $MyInvocation.MyCommand.Path -Action stop
        Start-Sleep -Seconds 2
        & $MyInvocation.MyCommand.Path -Action start
    }

    "backup" {
        & (Join-Path $PSScriptRoot "backup_postgres.ps1")
    }

    "archive" {
        & (Join-Path $PSScriptRoot "archive_postgres_logs.ps1")
    }

    "logs" {
        $logFiles = Get-ChildItem -Path $logDir -Filter "server-*.log" -ErrorAction SilentlyContinue | Sort-Object LastWriteTime -Descending
        if ($logFiles.Count -gt 0) {
            $targetLog = $logFiles[0].FullName
            Write-Host "正在跟随实时滚动日志: $targetLog" -ForegroundColor Cyan
            Get-Content $targetLog -Tail 30 -Wait
        } else {
            Write-Host "尚未检测到日志文件，路径: $logDir" -ForegroundColor Gray
        }
    }
}
