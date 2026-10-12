using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Mir2.Server.Core;
using Mir2.Server.Logging;

namespace Mir2.Server;

/// <summary>
/// 盛大官方 1.5 现代高并发 M2 服务端微内核启动入口
/// 基于 Microsoft.Extensions.Hosting、标准依赖注入 (IoC/DI) 与 Native AOT 零反射编译
/// </summary>
public static class Program
{
    public static async Task Main(string[] args)
    {
        var config = ServerConfig.Load();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("====================================================================");
        Console.WriteLine("  热血传奇 1.5 官方原版 .NET 10 高并发 M2 服务端引擎 (Mir2.Server)");
        Console.WriteLine($"  运行框架: .NET 10 (CLR / Native AOT 模式: {IsNativeAot()})");
        Console.WriteLine("  架构设计: Microsoft.Extensions.Hosting + 依赖注入 (DI) + 零分配流水线");
        Console.WriteLine("====================================================================");
        Console.ResetColor();

        // 0. 初始化 Serilog 工业级日志引擎与 PostgreSQL 审计 Sink
        ServerLogger.Initialize(config, config.EnableDatabaseLogging ? config.DatabaseConnectionString : null);

        AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
        {
            Serilog.Log.Fatal("[FATAL] 进程未处理致命异常: {Exception}", e.ExceptionObject);
            Serilog.Log.CloseAndFlush();
        };

        // 1. 基于现代 HostApplicationBuilder 构建容器并注册全栈服务
        var builder = Host.CreateApplicationBuilder(args);
        builder.Services.AddMir2ServerEngine(config);

        // 2. 启动宿主生命周期并在进程信号中断时自动优雅关机
        try
        {
            var host = builder.Build();
            await host.RunAsync();
        }
        catch (Exception ex)
        {
            Serilog.Log.Fatal(ex, "[Mir2.Server] 服务端宿主生命周期遭遇非正常中断");
        }
        finally
        {
            Serilog.Log.CloseAndFlush();
        }
    }

    private static bool IsNativeAot() => !RuntimeFeature.IsDynamicCodeCompiled;
}
