using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using Godot;
using Mir2.Shared.Protocols.Modern;
using Mir2Client.Utils;

namespace Mir2Client.Net;

/// <summary>
/// 盛大官方 1.5 客户端 TCP 网络通信管理器 (基于 .NET 原生 TcpClient 与 Godot 信号)
/// 彻底解耦 Godot 引擎层 StreamPeerTcp，原生支持现代紧凑二进制 TCP-Pack 与消特征动态混淆
/// </summary>
public partial class NetManager : Node
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

    private TcpClient? _client;
    private NetworkStream? _stream;
    private ConnectionState _state = ConnectionState.Disconnected;
    private string _host = string.Empty;
    private int _port = 0;
    private readonly PacketBuffer _packetBuffer = new();
    private readonly byte[] _readBuffer = new byte[8192];
    private ulong _lastRecvTick = 0;
    private float _reconnectTimer = 0.0f;
    private bool _autoReconnect = false;

    // 现代二进制协议与消特征状态机
    public bool UseModernBinaryProtocol { get; set; } = true;
    public PacketCipher? Cipher { get; set; }
    private ushort _sendSequence = 0;
    private readonly MemoryStream _recvStream = new();

    public ConnectionState State => _state;
    public bool IsConnectedToServer => _state == ConnectionState.Connected;

    public override void _Ready()
    {
        GD.Print("[NetManager] 现代二进制 TCP-Pack 与原生通信引擎已就绪");
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

        _packetBuffer.Clear();
        _state = ConnectionState.Connecting;

        try
        {
            var tcp = new TcpClient();
            await tcp.ConnectAsync(_host, _port);
            _client = tcp;
            _stream = tcp.GetStream();
            _state = ConnectionState.Connected;
            _lastRecvTick = Time.GetTicksMsec();

            GD.Print($"[NetManager] Connected to {_host}:{_port}");
            GameLogger.LogSystemEvent($"TCP 连接建立成功: {_host}:{_port}");
            EmitSignal(SignalName.Connected);
        }
        catch (Exception ex)
        {
            GD.PushError($"[NetManager] Connect error: {ex.Message}");
            _state = ConnectionState.Disconnected;
            EmitSignal(SignalName.ConnectFailed);
            if (_autoReconnect)
            {
                _reconnectTimer = ReconnectDelayS;
            }
        }
    }

    public void DisconnectFromServer()
    {
        _autoReconnect = false;
        try { _stream?.Close(); } catch { }
        try { _client?.Close(); } catch { }
        _stream = null;
        _client = null;
        _state = ConnectionState.Disconnected;
        _packetBuffer.Clear();
        _recvStream.SetLength(0);
    }

    /// <summary>
    /// 客户端发送现代紧凑二进制意图帧 (Direct-Blit 零分配，就地消特征混淆)
    /// </summary>
    public void SendBinaryIntent(ushort msgId, ReadOnlySpan<byte> payload)
    {
        if (_state != ConnectionState.Connected || _stream == null) return;

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

            _stream.Write(buffer, 0, totalLen);
            _stream.Flush();
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

    public void SendPacket(byte[] data)
    {
        if (_state != ConnectionState.Connected || _stream == null)
        {
            // 脱机演练模式或未连接状态下静默忽略
            return;
        }

        if (data.Length >= 10)
        {
            int offset = (data.Length >= 11 && data[0] >= 0x30 && data[0] <= 0x39) ? 1 : 0;
            short cmCode = (short)(data[offset] | (data[offset + 1] << 8));
            int recog = data[offset + 2] | (data[offset + 3] << 8) | (data[offset + 4] << 16) | (data[offset + 5] << 24);
            short param = (short)(data[offset + 6] | (data[offset + 7] << 8));
            short tag = (short)(data[offset + 8] | (data[offset + 9] << 8));

            int extraLen = Math.Max(0, data.Length - offset - 10 - 1);
            GameLogger.LogNetSend($"CM_{cmCode}", cmCode, recog, param, tag, extraLen);
        }

        try
        {
            _stream.Write(data, 0, data.Length);
            _stream.Flush();
        }
        catch (Exception ex)
        {
            GD.PushError($"[NetManager] SendPacket error: {ex.Message}");
            GameLogger.LogNetError($"SendPacket failed: {ex.Message}");
            HandleDisconnect("send error");
        }
    }

    public override void _Process(double delta)
    {
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

        if (_state != ConnectionState.Connected || _stream == null) return;

        // 非阻塞读取可用数据
        try
        {
            while (_stream.DataAvailable)
            {
                int read = _stream.Read(_readBuffer, 0, _readBuffer.Length);
                if (read > 0)
                {
                    _lastRecvTick = Time.GetTicksMsec();

                    // 1. 判断是否为现代二进制 TCP-Pack 流
                    if (UseModernBinaryProtocol)
                    {
                        _recvStream.Write(_readBuffer, 0, read);
                        ProcessBinaryRecvBuf();
                    }
                    else
                    {
                        byte[] bytes = new byte[read];
                        Buffer.BlockCopy(_readBuffer, 0, bytes, 0, read);
                        _packetBuffer.Append(bytes);
                        ProcessRecvBuf();
                    }
                }
                else
                {
                    HandleDisconnect("remote closed");
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            HandleDisconnect($"read error: {ex.Message}");
            return;
        }

        if (_lastRecvTick > 0)
        {
            ulong elapsed = Time.GetTicksMsec() - _lastRecvTick;
            if (elapsed > ReadTimeoutMs)
            {
                HandleDisconnect("read timeout");
            }
        }
    }

    private void ProcessBinaryRecvBuf()
    {
        var rawBytes = _recvStream.GetBuffer();
        int available = (int)_recvStream.Length;
        int consumed = 0;
        Span<byte> headerBytes = stackalloc byte[BinaryProtocol.HeaderSize];

        while (available - consumed >= BinaryProtocol.HeaderSize)
        {
            rawBytes.AsSpan(consumed, BinaryProtocol.HeaderSize).CopyTo(headerBytes);

            Cipher?.ProcessHeader(headerBytes);
            var header = BinaryProtocol.PacketHeader.Read(headerBytes);

            if (header.Magic != BinaryProtocol.DefaultMagic)
            {
                // 魔数不匹配，可能为传统文本包回退
                break;
            }

            int totalPacketSize = BinaryProtocol.HeaderSize + header.PayloadLength;
            if (available - consumed < totalPacketSize)
            {
                // 半包等待
                break;
            }

            // 完整包提取
            byte[] payload = new byte[header.PayloadLength];
            if (header.PayloadLength > 0)
            {
                rawBytes.AsSpan(consumed + BinaryProtocol.HeaderSize, header.PayloadLength).CopyTo(payload);
                Cipher?.ProcessPayload(payload, header.Sequence);
            }

            consumed += totalPacketSize;
            EmitSignal(SignalName.BinaryPacketReceived, header.MsgId, payload);
        }

        if (consumed > 0)
        {
            int remaining = available - consumed;
            if (remaining > 0)
            {
                Buffer.BlockCopy(rawBytes, consumed, rawBytes, 0, remaining);
            }
            _recvStream.SetLength(remaining);
        }
    }

    private void ProcessRecvBuf()
    {
        while (true)
        {
            byte[]? rawPacket = _packetBuffer.PopPacket();
            if (rawPacket == null) break;

            DispatchPacket(rawPacket);
        }
    }

    private void DispatchPacket(byte[] raw)
    {
        // 控制帧 '+' 或 '='
        if (raw.Length >= 2 && (raw[0] == 0x2B || raw[0] == 0x3D))
        {
            string frameStr = System.Text.Encoding.ASCII.GetString(raw);
            GameLogger.LogSystemEvent($"收到官方控制帧: '{frameStr}'");
            EmitSignal(SignalName.ControlFrameReceived, frameStr);
            return;
        }

        if (raw.Length < 10) return;

        // 去掉前导序号字符 '0'..'9'
        byte[] pkt = (raw[0] >= 0x30 && raw[0] <= 0x39) ? raw[1..] : raw;
        if (pkt.Length < 10) return;

        short smCode = (short)(pkt[0] | (pkt[1] << 8));
        int recog = pkt[2] | (pkt[3] << 8) | (pkt[4] << 16) | (pkt[5] << 24);
        short param = (short)(pkt[6] | (pkt[7] << 8));
        short tag = (short)(pkt[8] | (pkt[9] << 8));

        string extraText = string.Empty;
        if (pkt.Length > 10)
        {
            byte[] decoded = PacketCodec.DecodePlus60(pkt.AsSpan(10));
            extraText = System.Text.Encoding.UTF8.GetString(decoded);
        }

        GameLogger.LogNetRecv($"SM_{smCode}", smCode, recog, param, tag, Math.Max(0, pkt.Length - 10), extraText);
        EmitSignal(SignalName.PacketReceived, smCode, pkt);
    }

    private void HandleDisconnect(string reason)
    {
        GD.Print($"[NetManager] Disconnected: {reason}");
        GameLogger.LogSystemEvent($"网络连接断开: {reason}");
        _state = ConnectionState.Disconnected;
        _packetBuffer.Clear();
        try { _stream?.Close(); } catch { }
        try { _client?.Close(); } catch { }
        _stream = null;
        _client = null;
        EmitSignal(SignalName.Disconnected, reason);
        if (_autoReconnect)
        {
            _reconnectTimer = ReconnectDelayS;
        }
    }
}
