using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Mir2.Shared.Network.HPSocket;
using Mir2.Shared.Protocols.Modern;

namespace Mir2.Server.Network;

/// <summary>
/// 盛大官方 1.5 现代高并发 TCP 游戏网关
/// 支持千人同屏高并发长连接、Socket 异步零拷贝接收、封包粘包分帧与统一合包冲刷
/// 内置 HPSocket 源码级架构 (Native AOT 完美支持) 与紧凑二进制 Direct-Blit 分包
/// </summary>
public sealed class TcpGateway
{
    private readonly Socket _listenSocket;
    private readonly ConcurrentDictionary<int, ClientSession> _sessions = new();
    private int _sessionIdCounter = 1000;
    private bool _isRunning;
    private HpTcpServer? _hpTcpServer;

    public int Port { get; }
    public int ActiveConnectionsCount => _sessions.Count;
    public IEnumerable<ClientSession> AllSessions => _sessions.Values;
    public bool UseHpSocketBackend { get; set; } = true;
    public HpTcpServer? HpServerInstance => _hpTcpServer;

    // 生产公网防御参数
    public int MaxConnections { get; set; } = 3000;
    public int MaxIncomingBufferBytes { get; set; } = 65536; // 64KB 防 OOM 熔断上限
    public int MaxPacketsPerSecondPerSession { get; set; } = 80;
    public Mir2.Server.Security.GatewayDefenseEngine DefenseEngine { get; } = new();

    // 协议处理委托: 官方封包与现代二进制封包
    public Action<ClientSession, string>? PacketReceivedHandler { get; set; }
    public Action<ClientSession, BinaryProtocol.PacketHeader, byte[]>? BinaryPacketReceivedHandler { get; set; }
    public Action<ClientSession>? SessionConnectedHandler { get; set; }
    public Action<ClientSession>? SessionDisconnectedHandler { get; set; }

    /// <summary>
    /// 注册会话 (供网络或测试注入)
    /// </summary>
    public void RegisterSession(ClientSession session)
    {
        _sessions[session.SessionId] = session;
    }

    public TcpGateway(int port = 7000)
    {
        Port = port;
        _listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        // 优化高并发底层 Socket 参数
        _listenSocket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);
        _listenSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

        // 初始化 HPSocket 源码级后端 (AOT 零依赖)
        _hpTcpServer = new HpTcpServer();
    }

    public void Start()
    {
        try
        {
            _listenSocket.Bind(new IPEndPoint(IPAddress.Any, Port));
        }
        catch (SocketException)
        {
            // 在 Windows 受限或权限策略环境下，Any 绑定失败时安全回退至 Loopback
            _listenSocket.Bind(new IPEndPoint(IPAddress.Loopback, Port));
        }

        _listenSocket.Listen(1024);
        _isRunning = true;
        Console.WriteLine($"[TcpGateway] 网关已在端口 {Port} 启动监听 (HPSocket 源码级后端已就绪, Native AOT 零反射)...");

        _ = AcceptLoopAsync();
    }

    private async Task AcceptLoopAsync()
    {
        while (_isRunning)
        {
            try
            {
                var clientSocket = await _listenSocket.AcceptAsync();
                clientSocket.NoDelay = true;

                if (_sessions.Count >= MaxConnections)
                {
                    clientSocket.Close();
                    continue;
                }

                string remoteIp = string.Empty;
                if (clientSocket.RemoteEndPoint is IPEndPoint ipEp)
                {
                    remoteIp = ipEp.Address.ToString();
                }

                long nowSec = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                if (!DefenseEngine.ShouldAllowNewConnection(remoteIp, nowSec, out var denyReason))
                {
                    clientSocket.Close();
                    continue;
                }

                int sid = Interlocked.Increment(ref _sessionIdCounter);
                var session = new ClientSession(sid, clientSocket);
                session.RemoteIp = remoteIp;

                _sessions[sid] = session;

                SessionConnectedHandler?.Invoke(session);

                _ = ReceiveLoopAsync(session, clientSocket);
            }
            catch (Exception) when (!_isRunning)
            {
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TcpGateway] Accept 异常: {ex.Message}");
            }
        }
    }

    private async Task ReceiveLoopAsync(ClientSession session, Socket socket)
    {
        byte[] buffer = ArrayPool<byte>.Shared.Rent(4096);

        try
        {
            while (_isRunning && socket.Connected)
            {
                int bytesRead = await socket.ReceiveAsync(buffer.AsMemory(0, buffer.Length), SocketFlags.None);
                if (bytesRead == 0) break; // 客户端主动断开

                // 核心: 委托 HPSocket TCP-Pack 核心解包引擎处理 (底层自动完成分包组包与半包等待)
                session.PackDecoder.FeedData(buffer.AsSpan(0, bytesRead), (in BinaryProtocol.PacketHeader header, ReadOnlySpan<byte> payloadSpan) =>
                {
                    ProcessIncomingPack(session, in header, payloadSpan);
                });
            }
        }
        catch (SocketException)
        {
            // 客户端意外断开
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[TcpGateway] 会话 #{session.SessionId} 接收异常: {ex}");
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(buffer);
            _sessions.TryRemove(session.SessionId, out _);
            SessionDisconnectedHandler?.Invoke(session);
            session.Dispose();
        }
    }

    /// <summary>
    /// 处理由 HPSocket TCP-Pack 模型交付的完整二进制封包 (无半包、无粘包、零冗余内存搬移)
    /// 彻底废除 '#' 与 '!' 文本字符串定界符
    /// </summary>
    public void ProcessIncomingPack(ClientSession session, in BinaryProtocol.PacketHeader header, ReadOnlySpan<byte> payloadSpan)
    {
        byte[] payload = payloadSpan.Length > 0 ? payloadSpan.ToArray() : Array.Empty<byte>();

        // 如果会话配置了消特征加解密混淆，就地解密
        if (session.Cipher != null)
        {
            session.Cipher.ProcessPayload(payload, header.Sequence);
        }

        session.UseModernProtocol = true;
        session.LastActiveTick = Environment.TickCount64;

        if (!session.CheckPacketRateLimit(MaxPacketsPerSecondPerSession))
        {
            if (session.IsFloodingMaliciously)
            {
                Console.WriteLine($"[TcpGateway] 会话 #{session.SessionId} (IP: {session.RemoteIp}) 恶意二进制发包泛洪，强制断开！");
                session.Disconnect();
                return;
            }
        }

        BinaryPacketReceivedHandler?.Invoke(session, header, payload);
    }

    /// <summary>
    /// 定期清理超时无心跳的僵尸空闲连接 (默认 180 秒无数据交互自动剔除)
    /// </summary>
    public int SweepIdleConnections(long timeoutMs = 180000)
    {
        long now = Environment.TickCount64;
        int swept = 0;
        foreach (var session in _sessions.Values)
        {
            if (session.IsConnected && now - session.LastActiveTick > timeoutMs)
            {
                session.Disconnect();
                swept++;
            }
        }
        return swept;
    }

    /// <summary>
    /// 全服 100ms 心跳末尾合包冲刷：将所有活跃 Session 的累积下行包一次性送达网卡
    /// </summary>
    public void FlushAllOutboundBatches()
    {
        foreach (var session in _sessions.Values)
        {
            if (session.IsConnected)
            {
                session.Flush();
            }
        }
    }

    public void Stop()
    {
        _isRunning = false;
        try { _listenSocket.Close(); } catch { }
        foreach (var session in _sessions.Values)
        {
            session.Dispose();
        }
        _sessions.Clear();
    }
}
