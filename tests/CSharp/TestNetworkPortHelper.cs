using System;
using System.Threading;

namespace Mir2Client.Tests;

/// <summary>
/// 测试套件高并发全局网络端口分配器
/// 彻底根除并发单测中 TcpListener(0) 释放与复用导致的端口碰撞和串包坏账
/// </summary>
public static class TestNetworkPortHelper
{
    private static int _globalPort = 30000 + (Random.Shared.Next(100) * 100);

    public static int GetNextPort()
    {
        return Interlocked.Increment(ref _globalPort);
    }
}
