using System;
using System.Buffers;
using System.IO;
using System.IO.Pipelines;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Mir2.Shared.Protocols.Modern;

namespace Mir2.Server.Network;

/// <summary>
/// 现代 TCP-Pack 零拷贝解包与封包传输引擎 (内化 HP-Socket / IOCP 核心模型)
/// 彻底根除 TCP 流式传输中的粘包、半包与空包问题
/// 
/// 架构设计:
/// 1. 基于 System.IO.Pipelines 零拷贝内存管线;
/// 2. 帧头定长校验 (8 字节 BinaryProtocol.HeaderSize);
/// 3. 与 PacketCipher 深度集成，在入包完成组装的瞬时执行就地解密校验;
/// 4. 彻底支持千人同屏高并发低延迟吞吐.
/// </summary>
public sealed class TcpPackEngine
{
    public delegate void PacketReceivedHandler(PacketSession session, in BinaryProtocol.PacketHeader header, ReadOnlySpan<byte> payload);

    private readonly PacketReceivedHandler _handler;
    private readonly ushort _expectedMagic;

    public TcpPackEngine(PacketReceivedHandler handler, ushort expectedMagic = BinaryProtocol.DefaultMagic)
    {
        _handler = handler ?? throw new ArgumentNullException(nameof(handler));
        _expectedMagic = expectedMagic;
    }

    /// <summary>
    /// 处理会话接收管道中的连续字节流 (异步循环，无锁无阻塞)
    /// </summary>
    public async Task ProcessReceivePipeAsync(PacketSession session, PipeReader reader, CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            ReadResult result = await reader.ReadAsync(ct).ConfigureAwait(false);
            ReadOnlySequence<byte> buffer = result.Buffer;

            while (TryReadPacket(session, ref buffer))
            {
                // 持续提取完整封包，直到缓冲区剩余数据不足一整包
            }

            // 告知 Pipe 哪些数据已消费，哪些数据仍需保留等待后续拼包
            reader.AdvanceTo(buffer.Start, buffer.End);

            if (result.IsCompleted)
            {
                break;
            }
        }
    }

    /// <summary>
    /// 尝试从流中提取一个合法的完整 TCP-Pack 封包
    /// </summary>
    private bool TryReadPacket(PacketSession session, ref ReadOnlySequence<byte> buffer)
    {
        if (buffer.Length < BinaryProtocol.HeaderSize)
        {
            return false; // 不足 8 字节帧头，等待更多数据
        }

        // 1. 预读取 8 字节帧头
        Span<byte> headerBytes = stackalloc byte[BinaryProtocol.HeaderSize];
        buffer.Slice(0, BinaryProtocol.HeaderSize).CopyTo(headerBytes);

        // 如果开启了消特征混淆，就地解密还原帧头
        session.Cipher?.ProcessHeader(headerBytes);

        var header = BinaryProtocol.PacketHeader.Read(headerBytes);
        if (header.Magic != _expectedMagic)
        {
            // 非法魔数或恶意探针扫描 -> 立即丢弃并阻断
            throw new InvalidDataException($"非法魔数 0x{header.Magic:X4}，已阻断潜在脱机嗅探连接");
        }

        int totalPacketSize = BinaryProtocol.HeaderSize + header.PayloadLength;
        if (buffer.Length < totalPacketSize)
        {
            return false; // 半包状态：载荷数据尚未收齐，等待后续到达
        }

        // 2. 完整包到达：零拷贝提取载荷并执行动态混淆解密
        byte[] rented = ArrayPool<byte>.Shared.Rent(header.PayloadLength);
        try
        {
            var payloadSeq = buffer.Slice(BinaryProtocol.HeaderSize, header.PayloadLength);
            payloadSeq.CopyTo(rented);

            var payloadSpan = rented.AsSpan(0, header.PayloadLength);
            session.Cipher?.ProcessPayload(payloadSpan, header.Sequence);

            // 派发给业务层进行 100% 权威判定与处理
            _handler(session, in header, payloadSpan);
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(rented);
        }

        // 推进缓冲区游标，跳过已消费的整包
        buffer = buffer.Slice(totalPacketSize);
        return true;
    }
}

/// <summary>
/// 现代网络包会话包装实体
/// </summary>
public sealed class PacketSession
{
    public int SessionId { get; }
    public Socket Socket { get; }
    public PacketCipher? Cipher { get; set; }
    public ushort SendSequence { get; set; }

    public PacketSession(int sessionId, Socket socket, uint sessionKey = 0)
    {
        SessionId = sessionId;
        Socket = socket;
        if (sessionKey != 0)
        {
            Cipher = new PacketCipher(sessionKey);
        }
    }

    /// <summary>
    /// 构造并发送现代二进制封包 (就地混淆并下发网卡)
    /// </summary>
    public void SendPacket(ushort msgId, ReadOnlySpan<byte> payload)
    {
        int totalLen = BinaryProtocol.HeaderSize + payload.Length;
        byte[] buffer = ArrayPool<byte>.Shared.Rent(totalLen);
        try
        {
            ushort seq = ++SendSequence;
            var header = new BinaryProtocol.PacketHeader
            {
                Magic = BinaryProtocol.DefaultMagic,
                MsgId = msgId,
                PayloadLength = (ushort)payload.Length,
                Sequence = seq
            };

            var span = buffer.AsSpan(0, totalLen);
            var headerSpan = span[0..BinaryProtocol.HeaderSize];
            var payloadDest = span[BinaryProtocol.HeaderSize..];

            header.Write(headerSpan);
            payload.CopyTo(payloadDest);

            // 就地消特征混淆
            if (Cipher != null)
            {
                Cipher.ProcessHeader(headerSpan);
                Cipher.ProcessPayload(payloadDest, seq);
            }

            // 发送给物理网卡
            Socket.Send(buffer, 0, totalLen, SocketFlags.None);
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(buffer);
        }
    }
}
