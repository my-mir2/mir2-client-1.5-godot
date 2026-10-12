using System;
using System.Buffers;
using Mir2.Shared.Protocols.Modern;

namespace Mir2.Shared.Network.HPSocket;

/// <summary>
/// HPSocket 现代紧凑二进制 Pack 封包回调委托 (零堆分配传递完整封包)
/// </summary>
public delegate void HpPackPacketCallback(in BinaryProtocol.PacketHeader header, ReadOnlySpan<byte> payload);
public delegate void HpPackServerPacketHandler(ConnectionId connId, in BinaryProtocol.PacketHeader header, ReadOnlySpan<byte> payload);
public delegate void HpPackClientPacketHandler(ConnectionId connId, in BinaryProtocol.PacketHeader header, ReadOnlySpan<byte> payload);

/// <summary>
/// HPSocket 现代紧凑二进制 Pack 分帧解包引擎
/// 100% 遵守 HPSocket TCP-Pack 模型规范：自动处理粘包、半包、分包与畸形流熔断
/// 基于 8 字节固定头实现纯内存切片，零额外堆分配
/// </summary>
public sealed class HpPackStreamDecoder
{
    private readonly byte[] _buffer;
    private int _length;
    public int MaxPackSize { get; }

    public HpPackStreamDecoder(int maxPackSize = 65536)
    {
        MaxPackSize = maxPackSize;
        _buffer = new byte[maxPackSize];
        _length = 0;
    }

    /// <summary>
    /// 向解包器压入新到达的原始流字节，并在组件内部自动切包组包
    /// 当且仅当组装出完整的合法封包时，才通过回调向上层交付
    /// </summary>
    public void FeedData(ReadOnlySpan<byte> incoming, HpPackPacketCallback packetCallback)
    {
        if (incoming.IsEmpty) return;

        // 检查缓冲区是否会溢出
        if (_length + incoming.Length > _buffer.Length)
        {
            // 畸形流堆积过大无法分帧，强制重置并抛出异常供上层熔断
            Reset();
            throw new InvalidOperationException($"[HPSocket TCP-Pack] 接收缓冲区溢出 (> {MaxPackSize} 字节)，触发防爆熔断");
        }

        incoming.CopyTo(_buffer.AsSpan(_length));
        _length += incoming.Length;

        while (_length >= BinaryProtocol.HeaderSize)
        {
            var span = _buffer.AsSpan(0, _length);

            // 1. 验证 HPSocket Pack 特征魔数 (0x594D 即 'M','Y')
            ushort magic = (ushort)(span[0] | (span[1] << 8));
            if (magic != BinaryProtocol.DefaultMagic)
            {
                // 非法魔数，滑动搜索下一个魔数字节，防止网络抖动导致的偶发错位
                int nextMagicIdx = -1;
                for (int i = 1; i < _length - 1; i++)
                {
                    if (span[i] == 0x4D && span[i + 1] == 0x59)
                    {
                        nextMagicIdx = i;
                        break;
                    }
                }

                if (nextMagicIdx > 0)
                {
                    int remain = _length - nextMagicIdx;
                    Buffer.BlockCopy(_buffer, nextMagicIdx, _buffer, 0, remain);
                    _length = remain;
                    continue;
                }
                else
                {
                    // 彻底无合法魔数，清空缓冲并中止
                    Reset();
                    break;
                }
            }

            // 2. 读取 8 字节 PacketHeader
            var header = BinaryProtocol.PacketHeader.Read(span[..BinaryProtocol.HeaderSize]);
            int fullPacketSize = BinaryProtocol.HeaderSize + header.PayloadLength;

            if (header.PayloadLength > MaxPackSize)
            {
                // 超大非法包体，阻断攻击
                Reset();
                throw new InvalidOperationException($"[HPSocket TCP-Pack] 非法巨型包长 ({header.PayloadLength} 字节)，触发防御熔断");
            }

            if (_length < fullPacketSize)
            {
                // 半包状态：载荷尚未全部到达，退出等待更多数据
                break;
            }

            // 3. 完整 Pack 到达：零堆分配切片并触发回调
            var payloadSpan = span.Slice(BinaryProtocol.HeaderSize, header.PayloadLength);
            packetCallback(in header, payloadSpan);

            // 4. 移位消费当前数据包
            int remainingBytes = _length - fullPacketSize;
            if (remainingBytes > 0)
            {
                Buffer.BlockCopy(_buffer, fullPacketSize, _buffer, 0, remainingBytes);
            }
            _length = remainingBytes;
        }
    }

    public void Reset()
    {
        _length = 0;
    }
}
