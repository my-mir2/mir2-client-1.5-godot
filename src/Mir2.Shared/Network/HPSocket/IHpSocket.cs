using System;
using System.Net;
using System.Net.Sockets;

namespace Mir2.Shared.Network.HPSocket;

/// <summary>
/// HPSocket 源码级组件服务状态
/// </summary>
public enum ServiceState
{
    Starting = 0,
    Started = 1,
    Stopping = 2,
    Stopped = 3
}

/// <summary>
/// HPSocket 源码级传输操作结果
/// </summary>
public enum HandleResult
{
    Ok = 0,
    Ignore = 1,
    Error = 2
}

/// <summary>
/// HPSocket 统一连接句柄
/// </summary>
public readonly struct ConnectionId : IEquatable<ConnectionId>
{
    public readonly IntPtr Value;
    public ConnectionId(IntPtr value) => Value = value;
    public ConnectionId(long value) => Value = new IntPtr(value);

    public bool Equals(ConnectionId other) => Value == other.Value;
    public override bool Equals(object? obj) => obj is ConnectionId other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value.ToString();

    public static bool operator ==(ConnectionId left, ConnectionId right) => left.Equals(right);
    public static bool operator !=(ConnectionId left, ConnectionId right) => !left.Equals(right);
}

/// <summary>
/// 源码级 HPSocket 服务端事件监听器契约 (100% 兼容 Native AOT，零反射)
/// </summary>
public interface IHpServerListener
{
    HandleResult OnPrepareListen(IntPtr soListen);
    HandleResult OnAccept(ConnectionId connId, IntPtr clientSocket);
    HandleResult OnHandShake(ConnectionId connId);
    HandleResult OnReceive(ConnectionId connId, ReadOnlySpan<byte> data);
    HandleResult OnSend(ConnectionId connId, ReadOnlySpan<byte> data);
    HandleResult OnClose(ConnectionId connId, SocketError error);
    HandleResult OnShutdown();
}

/// <summary>
/// 源码级 HPSocket 客户端事件监听器契约
/// </summary>
public interface IHpClientListener
{
    HandleResult OnPrepareConnect(ConnectionId connId, IntPtr clientSocket);
    HandleResult OnConnect(ConnectionId connId);
    HandleResult OnHandShake(ConnectionId connId);
    HandleResult OnReceive(ConnectionId connId, ReadOnlySpan<byte> data);
    HandleResult OnSend(ConnectionId connId, ReadOnlySpan<byte> data);
    HandleResult OnClose(ConnectionId connId, SocketError error);
}

/// <summary>
/// HPSocket 核心服务端接口
/// </summary>
public interface IHpTcpServer : IDisposable
{
    ServiceState State { get; }
    int Port { get; }
    uint ConnectionCount { get; }
    bool Start(string bindAddress, ushort port);
    bool Stop();
    bool Send(ConnectionId connId, ReadOnlySpan<byte> buffer);
    bool Disconnect(ConnectionId connId, bool force = false);
}

/// <summary>
/// HPSocket 核心客户端接口
/// </summary>
public interface IHpTcpClient : IDisposable
{
    ServiceState State { get; }
    bool IsConnected { get; }
    ConnectionId ConnectionId { get; }
    bool Connect(string remoteAddress, ushort port, bool async = false);
    bool Stop();
    bool Send(ReadOnlySpan<byte> buffer);
}
