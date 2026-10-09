using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mir2.Server.Network;

/// <summary>
/// 盛大官方 1.5 现代高并发 TCP 游戏网关
/// 支持千人同屏高并发长连接、Socket 异步零拷贝接收、封包粘包分帧与统一合包冲刷
/// </summary>
public sealed class TcpGateway
{
    private readonly Socket _listenSocket;
    private readonly ConcurrentDictionary<int, ClientSession> _sessions = new();
    private int _sessionIdCounter = 1000;
    private bool _isRunning;

    public int Port { get; }
    public int ActiveConnectionsCount => _sessions.Count;
    public IEnumerable<ClientSession> AllSessions => _sessions.Values;

    // 协议处理委托: (Session, CM_包完整字符串)
    public Action<ClientSession, string>? PacketReceivedHandler { get; set; }
    public Action<ClientSession>? SessionConnectedHandler { get; set; }
    public Action<ClientSession>? SessionDisconnectedHandler { get; set; }

    public TcpGateway(int port = 7000)
    {
        Port = port;
        _listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        // 优化高并发底层 Socket 参数
        _listenSocket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);
        _listenSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
    }

    public void Start()
    {
        _listenSocket.Bind(new IPEndPoint(IPAddress.Any, Port));
        _listenSocket.Listen(1024);
        _isRunning = true;
        Console.WriteLine($"[TcpGateway] 网关已在端口 {Port} 启动监听 (TCP NoDelay=True, Backlog=1024)...");

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

                int sid = Interlocked.Increment(ref _sessionIdCounter);
                var session = new ClientSession(sid, clientSocket);
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
        var sb = new StringBuilder(512);

        try
        {
            while (_isRunning && socket.Connected)
            {
                int bytesRead = await socket.ReceiveAsync(buffer.AsMemory(0, buffer.Length), SocketFlags.None);
                if (bytesRead == 0) break; // 客户端主动断开

                string rawChunk = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                sb.Append(rawChunk);

                // 处理分包与粘包 (官方 1.5 封包以 '!' 结尾)
                while (true)
                {
                    string current = sb.ToString();
                    int endIdx = current.IndexOf('!');
                    if (endIdx < 0) break; // 数据尚未接收完整，继续等待

                    int startIdx = current.IndexOf('#');
                    if (startIdx >= 0 && startIdx < endIdx)
                    {
                        string packet = current.Substring(startIdx, endIdx - startIdx + 1);
                        PacketReceivedHandler?.Invoke(session, packet);
                    }

                    sb.Remove(0, endIdx + 1);
                }
            }
        }
        catch (SocketException)
        {
            // 客户端意外断开
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[TcpGateway] 会话 #{session.SessionId} 接收异常: {ex.Message}");
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
