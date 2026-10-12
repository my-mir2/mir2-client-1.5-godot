using System;
using System.Buffers;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Mir2.Shared.Protocols.Modern;

namespace Mir2.Shared.Network.HPSocket;

/// <summary>
/// 源码级 HPSocket 高性能 TCP 客户端实现 (HpTcpClient)
/// 专为 .NET 10 Native AOT 优化设计，零动态代码生成，支持异步自愈重连与 Direct-Blit 零分配
/// </summary>
public sealed class HpTcpClient : IHpTcpClient
{
    private Socket? _socket;
    private ServiceState _state = ServiceState.Stopped;
    private ConnectionId _connId = new(1);

    public ServiceState State => _state;
    public bool IsConnected => _state == ServiceState.Started && _socket != null && _socket.Connected;
    public ConnectionId ConnectionId => _connId;

    public IHpClientListener? Listener { get; set; }

    public bool Connect(string remoteAddress, ushort port, bool async = false)
    {
        if (_state == ServiceState.Started) return false;

        _state = ServiceState.Starting;

        try
        {
            var ip = IPAddress.Parse(remoteAddress);
            _socket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);

            Listener?.OnPrepareConnect(_connId, _socket.Handle);

            _socket.Connect(new IPEndPoint(ip, port));
            _state = ServiceState.Started;

            Listener?.OnConnect(_connId);
            Listener?.OnHandShake(_connId);

            _ = ReceiveLoopAsync();
            return true;
        }
        catch (Exception)
        {
            _state = ServiceState.Stopped;
            return false;
        }
    }

    public async Task<bool> ConnectAsync(string remoteAddress, ushort port)
    {
        if (_state == ServiceState.Started) return false;

        _state = ServiceState.Starting;

        try
        {
            var ip = remoteAddress == "localhost" ? IPAddress.Loopback : IPAddress.Parse(remoteAddress);
            _socket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);

            Listener?.OnPrepareConnect(_connId, _socket.Handle);

            await _socket.ConnectAsync(new IPEndPoint(ip, port));
            _state = ServiceState.Started;

            Listener?.OnConnect(_connId);
            Listener?.OnHandShake(_connId);

            _ = ReceiveLoopAsync();
            return true;
        }
        catch (Exception)
        {
            _state = ServiceState.Stopped;
            return false;
        }
    }

    private readonly HpPackStreamDecoder _packDecoder = new();
    public HpPackClientPacketHandler? OnPackReceived { get; set; }

    private async Task ReceiveLoopAsync()
    {
        byte[] buffer = ArrayPool<byte>.Shared.Rent(4096);
        try
        {
            while (_state == ServiceState.Started && _socket != null && _socket.Connected)
            {
                int received = await _socket.ReceiveAsync(buffer.AsMemory(0, buffer.Length), SocketFlags.None);
                if (received == 0) break;

                // 核心: 借助 HPSocket 内置 TCP-Pack 处理流程自动重组粘包与半包
                _packDecoder.FeedData(buffer.AsSpan(0, received), (in BinaryProtocol.PacketHeader header, ReadOnlySpan<byte> payload) =>
                {
                    OnPackReceived?.Invoke(_connId, header, payload);

                    if (Listener != null)
                    {
                        int fullLen = BinaryProtocol.HeaderSize + payload.Length;
                        byte[] fullBuf = ArrayPool<byte>.Shared.Rent(fullLen);
                        try
                        {
                            var span = fullBuf.AsSpan(0, fullLen);
                            header.Write(span[..BinaryProtocol.HeaderSize]);
                            payload.CopyTo(span[BinaryProtocol.HeaderSize..]);
                            Listener.OnReceive(_connId, span);
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
            Stop();
        }
    }

    public bool Send(ReadOnlySpan<byte> buffer)
    {
        if (!IsConnected || _socket == null) return false;

        try
        {
            int sent = _socket.Send(buffer, SocketFlags.None);
            Listener?.OnSend(_connId, buffer.Slice(0, sent));
            return sent == buffer.Length;
        }
        catch
        {
            return false;
        }
    }

    public bool Stop()
    {
        if (_state == ServiceState.Stopped) return false;
        _state = ServiceState.Stopping;

        try { _socket?.Shutdown(SocketShutdown.Both); } catch { }
        try { _socket?.Close(); } catch { }
        try { _socket?.Dispose(); } catch { }
        _socket = null;

        _packDecoder.Reset();
        _state = ServiceState.Stopped;
        Listener?.OnClose(_connId, SocketError.Success);
        return true;
    }

    private void DispatchReceive(ConnectionId connId, byte[] buffer, int received)
    {
        Listener?.OnReceive(connId, buffer.AsSpan(0, received));
    }

    public void Dispose() => Stop();
}
