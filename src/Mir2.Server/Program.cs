using System;
using System.Runtime.CompilerServices;

namespace Mir2.Server;

public static class Program
{
    public static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("====================================================================");
        Console.WriteLine("  热血传奇 1.5 原版 .NET 10 高并发 M2 服务端引擎 (Mir2.Server)");
        Console.WriteLine($"  运行框架: .NET 10 (CLR / Native AOT: {IsNativeAot()})");
        Console.WriteLine("  架构设计: 100ms 心跳末尾合包 + 九宫格 AOI 空间分区 + 零分配缓冲池");
        Console.WriteLine("====================================================================");
        Console.ResetColor();

        Console.WriteLine("[Mir2.Server] 服务端核心已就绪，正在初始化世界地图与网络网关...");
    }

    private static bool IsNativeAot()
    {
        return !RuntimeFeature.IsDynamicCodeCompiled;
    }
}
