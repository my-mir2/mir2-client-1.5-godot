using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Mir2.Server.Core;
using Mir2.Server.Maps;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Server.Game;
using Mir2.Shared.Protocols;

namespace Mir2.Server;

public static class Program
{
    private static TcpGateway? _gateway;
    private static GameWorldTick? _worldTick;
    private static NinePatchAoi<ClientSession>? _mainMapAoi;
    private static PacketDispatcher? _dispatcher;

    public static async Task Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("====================================================================");
        Console.WriteLine("  热血传奇 1.5 官方原版 .NET 10 高并发 M2 服务端引擎 (Mir2.Server)");
        Console.WriteLine($"  运行框架: .NET 10 (CLR / Native AOT 模式: {IsNativeAot()})");
        Console.WriteLine("  架构设计: 100ms 心跳末尾合包 + 九宫格 AOI 空间分区 + 零分配缓冲池");
        Console.WriteLine("====================================================================");
        Console.ResetColor();

        // 1. 初始化比奇省主地图九宫格 AOI (比奇省 400x400 格子)
        _mainMapAoi = new NinePatchAoi<ClientSession>(400, 400);
        _dispatcher = new PacketDispatcher(_mainMapAoi);
        Console.WriteLine("[Mir2.Server] 比奇省地图 AOI 空间索引初始化完成 (尺寸: 400x400, 划分 25x25=625 个 Zone).");

        // 2. 初始化现代异步网络网关 (默认端口 7000)
        _gateway = new TcpGateway(7000);
        _gateway.SessionConnectedHandler = OnSessionConnected;
        _gateway.SessionDisconnectedHandler = OnSessionDisconnected;
        _gateway.PacketReceivedHandler = OnPacketReceived;
        _gateway.Start();

        // 3. 启动 100ms 物理世界心跳引擎
        _worldTick = new GameWorldTick(_gateway);
        _worldTick.WorldUpdateHandler = OnWorldTick;
        _worldTick.Start();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("[Mir2.Server] 服务端引擎启动完毕，千人同屏架构已就绪。正在监听玩家连接...");
        Console.ResetColor();

        // 4. 统计监控循环 (每 5 秒输出一次性能指标)
        var monitorEntities = new List<ClientSession>(100);
        while (true)
        {
            await Task.Delay(5000);
            double tickMs = _worldTick.LastTickDurationMs;
            double maxTickMs = _worldTick.MaxTickDurationMs;
            int online = _gateway.ActiveConnectionsCount;

            Console.WriteLine($"[监控] 在线会话: {online} | 当前Tick耗时: {tickMs:F3}ms | 峰值Tick耗时: {maxTickMs:F3}ms | 运行周期: {_worldTick.CurrentTick}");
        }
    }

    private static void OnSessionConnected(ClientSession session)
    {
        Console.WriteLine($"[网关] 玩家连接建立: 会话 #{session.SessionId}");
        // 初始出生点: 比奇省银杏山谷 (330, 270)
        session.CurrentMapId = 0;
        session.X = 330;
        session.Y = 270;
        session.State = new PlayerState();
        _mainMapAoi?.Enter(session, session.X, session.Y);

        // 下发官方 1.5 登录就绪与欢迎包
        session.EnqueuePacket(ProtocolConsts.SM_LOGON_DELPHI, 1, 0, 0, 0, "盛大传奇1.5官方服务器");
    }

    private static void OnSessionDisconnected(ClientSession session)
    {
        Console.WriteLine($"[网关] 玩家连接断开: 会话 #{session.SessionId}");
        _mainMapAoi?.Leave(session, session.X, session.Y);
    }

    private static void OnPacketReceived(ClientSession session, string packet)
    {
        _dispatcher?.Dispatch(session, packet);
    }

    private static void OnWorldTick(long tick, double deltaMs)
    {
        // 每 100ms 世界更新逻辑:
        // 1. 怪物巡逻与仇恨搜索
        // 2. 技能火墙持续时间倒计时与伤害结算
        // 3. 药水每 100ms 持续回复判定
    }

    private static bool IsNativeAot()
    {
        return !RuntimeFeature.IsDynamicCodeCompiled;
    }
}
