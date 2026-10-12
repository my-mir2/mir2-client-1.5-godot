using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using Godot;
using Mir2.Shared.Network.HPSocket;
using Mir2.Shared.Protocols.Modern;
using Mir2Client.Utils;

namespace Mir2Client.Net;

/// <summary>
/// 盛大官方 1.5 客户端高性能网络通信管理器 (基于 HPSocket 源码级组件与现代二进制协议)
/// 彻底淘汰旧世代 ASCII 文本分包与 6-bit 编码，原生兼容 .NET 10 Native AOT 纯静态编译
/// </summary>
public partial class NetManager : Node, IHpClientListener
{
    [Signal]
    public delegate void PacketReceivedEventHandler(short smCode, byte[] data);

    [Signal]
    public delegate void BinaryPacketReceivedEventHandler(ushort msgId, byte[] payload);

    [Signal]
    public delegate void ControlFrameReceivedEventHandler(string frame);

    [Signal]
    public delegate void ConnectedEventHandler();

    [Signal]
    public delegate void DisconnectedEventHandler(string reason);

    [Signal]
    public delegate void ConnectFailedEventHandler();

    public enum ConnectionState
    {
        Disconnected,
        Connecting,
        Connected
    }

    public const int ReadTimeoutMs = 300_000;
    public const float ReconnectDelayS = 3.0f;

    // HPSocket 源码级客户端与队列
    private HpTcpClient? _hpClient;
    private readonly ConcurrentQueue<(ushort msgId, byte[] payload)> _binaryQueue = new();
    private readonly ConcurrentQueue<string> _disconnectQueue = new();
    private bool _connectedEventPending = false;
    private bool _connectFailedPending = false;

    private ConnectionState _state = ConnectionState.Disconnected;
    private string _host = string.Empty;
    private int _port = 0;
    private readonly byte[] _rawRecvBuf = new byte[65536];
    private int _rawRecvLen = 0;
    private readonly object _recvLock = new();

    private ulong _lastRecvTick = 0;
    private float _reconnectTimer = 0.0f;
    private bool _autoReconnect = false;

    // 现代二进制协议与消特征混淆
    public bool UseModernBinaryProtocol { get; set; } = true;
    public PacketCipher? Cipher { get; set; }
    private ushort _sendSequence = 0;

    public ConnectionState State => _state;
    public bool IsConnectedToServer => _state == ConnectionState.Connected && _hpClient != null && _hpClient.IsConnected;

    public override void _Ready()
    {
        GD.Print("[NetManager] HPSocket 源码级 AOT 高性能二进制网络驱动已就绪");
    }

    public void ConnectTo(string host, int port, bool autoReconnect = false)
    {
        _host = host;
        _port = port;
        _autoReconnect = autoReconnect;
        _ = DoConnectAsync();
    }

    private async Task DoConnectAsync()
    {
        DisconnectFromServer();

        _state = ConnectionState.Connecting;

        try
        {
            var hp = new HpTcpClient();
            hp.Listener = this;
            bool ok = await hp.ConnectAsync(_host, (ushort)_port);
            if (!ok)
            {
                _state = ConnectionState.Disconnected;
                _connectFailedPending = true;
                if (_autoReconnect) _reconnectTimer = ReconnectDelayS;
                return;
            }

            _hpClient = hp;
            _state = ConnectionState.Connected;
            _lastRecvTick = Time.GetTicksMsec();

            GD.Print($"[NetManager] HPSocket 已成功连接至 {_host}:{_port}");
            GameLogger.LogSystemEvent($"HPSocket TCP 连接建立成功: {_host}:{_port}");
            _connectedEventPending = true;

            // 主动发起握手意图
            SendBinaryIntent((ushort)BinaryProtocol.MsgType.HandshakeRequest, ReadOnlySpan<byte>.Empty);
        }
        catch (Exception ex)
        {
            GD.PushError($"[NetManager] Connect error: {ex.Message}");
            _state = ConnectionState.Disconnected;
            _connectFailedPending = true;
            if (_autoReconnect) _reconnectTimer = ReconnectDelayS;
        }
    }

    public void DisconnectFromServer()
    {
        _autoReconnect = false;
        try { _hpClient?.Stop(); } catch { }
        try { _hpClient?.Dispose(); } catch { }
        _hpClient = null;
        _state = ConnectionState.Disconnected;
        lock (_recvLock)
        {
            _rawRecvLen = 0;
        }
    }

    /// <summary>
    /// 客户端发送现代紧凑二进制意图帧 (Direct-Blit 零分配，HPSocket 源码级发送)
    /// </summary>
    public void SendBinaryIntent(ushort msgId, ReadOnlySpan<byte> payload)
    {
        if (_state != ConnectionState.Connected || _hpClient == null || !_hpClient.IsConnected) return;

        int totalLen = BinaryProtocol.HeaderSize + payload.Length;
        byte[] buffer = System.Buffers.ArrayPool<byte>.Shared.Rent(totalLen);
        try
        {
            ushort seq = ++_sendSequence;
            var header = new BinaryProtocol.PacketHeader
            {
                Magic = BinaryProtocol.DefaultMagic,
                MsgId = msgId,
                PayloadLength = (ushort)payload.Length,
                Sequence = seq
            };

            var span = buffer.AsSpan(0, totalLen);
            var headerSpan = span[0..BinaryProtocol.HeaderSize];
            var payloadSpan = span[BinaryProtocol.HeaderSize..];

            header.Write(headerSpan);
            payload.CopyTo(payloadSpan);

            // 如果开启了消特征混淆，就地混淆帧头与载荷
            if (Cipher != null)
            {
                Cipher.ProcessHeader(headerSpan);
                Cipher.ProcessPayload(payloadSpan, seq);
            }

            _hpClient.Send(span);
        }
        catch (Exception ex)
        {
            GD.PushError($"[NetManager] SendBinaryIntent error: {ex.Message}");
            HandleDisconnect("send error");
        }
        finally
        {
            System.Buffers.ArrayPool<byte>.Shared.Return(buffer);
        }
    }

    /// <summary>
    /// 客户端发送强类型 Direct-Blit 二进制载荷 (零拷贝、零堆分配，AOT 友好)
    /// </summary>
    public unsafe void SendBinaryIntent<T>(ushort msgId, in T payload) where T : unmanaged
    {
        fixed (T* ptr = &payload)
        {
            var span = new ReadOnlySpan<byte>((byte*)ptr, sizeof(T));
            SendBinaryIntent(msgId, span);
        }
    }

    /// <summary>
    /// 兼容接口：传统字节流统一尝试转换为二进制或静默下发
    /// </summary>
    public void SendPacket(byte[] data)
    {
        if (_state != ConnectionState.Connected || _hpClient == null || !_hpClient.IsConnected) return;

        try
        {
            _hpClient.Send(data.AsSpan());
        }
        catch (Exception ex)
        {
            GD.PushError($"[NetManager] SendPacket error: {ex.Message}");
            HandleDisconnect("send error");
        }
    }

    public override void _Process(double delta)
    {
        // 1. 处理连接与断开事件
        if (_connectedEventPending)
        {
            _connectedEventPending = false;
            EmitSignal(SignalName.Connected);
        }

        if (_connectFailedPending)
        {
            _connectFailedPending = false;
            EmitSignal(SignalName.ConnectFailed);
        }

        while (_disconnectQueue.TryDequeue(out var reason))
        {
            HandleDisconnect(reason);
        }

        if (_state == ConnectionState.Disconnected)
        {
            if (_autoReconnect)
            {
                _reconnectTimer -= (float)delta;
                if (_reconnectTimer <= 0.0f)
                {
                    _ = DoConnectAsync();
                }
            }
            return;
        }

        // 2. 主线程派发接收到的二进制数据包 (同时派发现代二进制与标准 SM 协议视图)
        while (_binaryQueue.TryDequeue(out var item))
        {
            EmitSignal(SignalName.BinaryPacketReceived, item.msgId, item.payload);
            EmitSignal(SignalName.PacketReceived, (short)item.msgId, item.payload);
        }

        // 3. 超时检查
        if (_lastRecvTick > 0)
        {
            ulong elapsed = Time.GetTicksMsec() - _lastRecvTick;
            if (elapsed > ReadTimeoutMs)
            {
                HandleDisconnect("read timeout");
            }
        }
    }

    // =========================================================================
    // IHpClientListener 接口实现 (HPSocket 网络事件回调)
    // =========================================================================

    public HandleResult OnPrepareConnect(ConnectionId connId, IntPtr clientSocket) => HandleResult.Ok;

    public HandleResult OnConnect(ConnectionId connId)
    {
        _lastRecvTick = Time.GetTicksMsec();
        return HandleResult.Ok;
    }

    public HandleResult OnHandShake(ConnectionId connId) => HandleResult.Ok;

    public HandleResult OnReceive(ConnectionId connId, ReadOnlySpan<byte> buffer)
    {
        _lastRecvTick = Time.GetTicksMsec();

        // 核心: HPSocket TCP-Pack 驱动已在底层自动完成分包组包，保证传入数据必定为完整 Pack
        if (buffer.Length < BinaryProtocol.HeaderSize) return HandleResult.Ok;

        Span<byte> headerBytes = stackalloc byte[BinaryProtocol.HeaderSize];
        buffer[..BinaryProtocol.HeaderSize].CopyTo(headerBytes);
        Cipher?.ProcessHeader(headerBytes);

        var header = BinaryProtocol.PacketHeader.Read(headerBytes);
        if (header.Magic != BinaryProtocol.DefaultMagic) return HandleResult.Ignore;

        byte[] payload = header.PayloadLength > 0
            ? buffer.Slice(BinaryProtocol.HeaderSize, header.PayloadLength).ToArray()
            : Array.Empty<byte>();

        if (payload.Length > 0 && Cipher != null)
        {
            Cipher.ProcessPayload(payload, header.Sequence);
        }

        _binaryQueue.Enqueue((header.MsgId, payload));
        return HandleResult.Ok;
    }

    public HandleResult OnSend(ConnectionId connId, ReadOnlySpan<byte> buffer) => HandleResult.Ok;

    public HandleResult OnClose(ConnectionId connId, SocketError error)
    {
        _disconnectQueue.Enqueue($"Socket closed: {error}");
        return HandleResult.Ok;
    }

    private void HandleDisconnect(string reason)
    {
        if (_state == ConnectionState.Disconnected) return;
        GD.Print($"[NetManager] HPSocket Disconnected: {reason}");
        GameLogger.LogSystemEvent($"HPSocket 网络连接断开: {reason}");
        _state = ConnectionState.Disconnected;
        try { _hpClient?.Stop(); } catch { }
        try { _hpClient?.Dispose(); } catch { }
        _hpClient = null;
        EmitSignal(SignalName.Disconnected, reason);
        if (_autoReconnect)
        {
            _reconnectTimer = ReconnectDelayS;
        }
    }
}
