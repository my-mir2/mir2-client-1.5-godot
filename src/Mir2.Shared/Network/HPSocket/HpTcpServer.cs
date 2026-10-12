using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Mir2.Shared.Protocols.Modern;

namespace Mir2.Shared.Network.HPSocket;

/// <summary>
/// 源码级 HPSocket 高性能 TCP 服务端实现 (HpTcpServer)
/// 专为 .NET 10 Native AOT 优化设计，零动态代码生成 (No Emit)，基于 SocketAsyncEventArgs 与对象池架构
/// </summary>
public sealed class HpTcpServer : IHpTcpServer
{
    private Socket? _listenSocket;
    private readonly ConcurrentDictionary<long, ServerConnectionContext> _connections = new();
    private long _connIdCounter = 1000;
    private ServiceState _state = ServiceState.Stopped;

    public ServiceState State => _state;
    public int Port { get; private set; }
    public uint ConnectionCount => (uint)_connections.Count;

    public IHpServerListener? Listener { get; set; }

    private sealed class ServerConnectionContext : IDisposable
    {
        public ConnectionId ConnId { get; }
        public Socket Socket { get; }
        public HpPackStreamDecoder PackDecoder { get; } = new();

        public ServerConnectionContext(ConnectionId connId, Socket socket)
        {
            ConnId = connId;
            Socket = socket;
        }

        public void Dispose()
        {
            try { Socket.Shutdown(SocketShutdown.Both); } catch { }
            try { Socket.Close(); } catch { }
            try { Socket.Dispose(); } catch { }
            PackDecoder.Reset();
        }
    }

    public bool Start(string bindAddress, ushort port)
    {
        if (_state != ServiceState.Stopped) return false;

        _state = ServiceState.Starting;
        Port = port;

        try
        {
            var ip = string.IsNullOrEmpty(bindAddress) || bindAddress == "0.0.0.0"
                ? IPAddress.Any
                : IPAddress.Parse(bindAddress);

            _listenSocket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _listenSocket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);
            _listenSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            Listener?.OnPrepareListen(_listenSocket.Handle);

            _listenSocket.Bind(new IPEndPoint(ip, port));
            _listenSocket.Listen(1024);

            _state = ServiceState.Started;
            _ = AcceptLoopAsync();
            return true;
        }
        catch (Exception)
        {
            _state = ServiceState.Stopped;
            return false;
        }
    }

    private async Task AcceptLoopAsync()
    {
        while (_state == ServiceState.Started && _listenSocket != null)
        {
            try
            {
                var clientSocket = await _listenSocket.AcceptAsync();
                clientSocket.NoDelay = true;

                long rawId = Interlocked.Increment(ref _connIdCounter);
                var connId = new ConnectionId(rawId);
                var ctx = new ServerConnectionContext(connId, clientSocket);
                _connections[rawId] = ctx;

                Listener?.OnAccept(connId, clientSocket.Handle);
                Listener?.OnHandShake(connId);

                _ = ReceiveLoopAsync(ctx);
            }
            catch (Exception) when (_state != ServiceState.Started)
            {
                break;
            }
            catch (Exception)
            {
                // 网络监听瞬时异常重试
            }
        }
    }

    public HpPackServerPacketHandler? OnPackReceived { get; set; }

    private async Task ReceiveLoopAsync(ServerConnectionContext ctx)
    {
        byte[] buffer = ArrayPool<byte>.Shared.Rent(4096);
        try
        {
            while (_state == ServiceState.Started && ctx.Socket.Connected)
            {
                int received = await ctx.Socket.ReceiveAsync(buffer.AsMemory(0, buffer.Length), SocketFlags.None);
                if (received == 0) break; // 对端正常关闭

                // 核心: 借助 HPSocket 内置 TCP-Pack 处理流程自动重组粘包与半包
                // 仅当一个完整的数据包完全就绪时，才向上层派发
                ctx.PackDecoder.FeedData(buffer.AsSpan(0, received), (in BinaryProtocol.PacketHeader header, ReadOnlySpan<byte> payload) =>
                {
                    OnPackReceived?.Invoke(ctx.ConnId, header, payload);

                    if (Listener != null)
                    {
                        int fullLen = Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize + payload.Length;
                        byte[] fullBuf = ArrayPool<byte>.Shared.Rent(fullLen);
                        try
                        {
                            var span = fullBuf.AsSpan(0, fullLen);
                            header.Write(span[..Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize]);
                            payload.CopyTo(span[Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize..]);
                            Listener.OnReceive(ctx.ConnId, span);
                        }
                        finally
                        {
                            ArrayPool<byte>.Shared.Return(fullBuf);
                        }
                    }
                });
            }
        }
        catch (SocketException) { }
        catch (Exception) { }
        finally
        {
            ArrayPool<byte>.Shared.Return(buffer);
            Disconnect(ctx.ConnId, true);
        }
    }

    public bool Send(ConnectionId connId, ReadOnlySpan<byte> buffer)
    {
        if (!_connections.TryGetValue(connId.Value.ToInt64(), out var ctx))
            return false;

        try
        {
            int sent = ctx.Socket.Send(buffer, SocketFlags.None);
            Listener?.OnSend(connId, buffer.Slice(0, sent));
            return sent == buffer.Length;
        }
        catch
        {
            return false;
        }
    }

    public bool Disconnect(ConnectionId connId, bool force = false)
    {
        if (_connections.TryRemove(connId.Value.ToInt64(), out var ctx))
        {
            Listener?.OnClose(connId, SocketError.Success);
            ctx.Dispose();
            return true;
        }
        return false;
    }

    public bool Stop()
    {
        if (_state != ServiceState.Started) return false;
        _state = ServiceState.Stopping;

        try { _listenSocket?.Close(); } catch { }
        _listenSocket = null;

        foreach (var kvp in _connections)
        {
            Listener?.OnClose(kvp.Value.ConnId, SocketError.Shutdown);
            kvp.Value.Dispose();
        }
        _connections.Clear();

        _state = ServiceState.Stopped;
        Listener?.OnShutdown();
        return true;
    }

    private void DispatchReceive(ConnectionId connId, byte[] buffer, int received)
    {
        Listener?.OnReceive(connId, buffer.AsSpan(0, received));
    }

    public void Dispose() => Stop();
}
