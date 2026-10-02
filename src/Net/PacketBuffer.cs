using System;
using System.Collections.Generic;

namespace Mir2Client.Net;

/// <summary>
/// 盛大官方 1.5 原版 TCP 数据包流分包与环形缓冲区 (IDA sub_458D3C 铁证)
/// 算法规则:
///   1. 控制帧: 以 '+' (0x2B) 或 '=' (0x3D) 开头，遇到 '\n' (0x0A) 截断，纯 ASCII 控制协议帧
///   2. 普通包: [1B 序号字符 '0'..'9'][10B 固定头][nB +60 extra][\n] 或无序号版本 [10B 固定头][nB +60 extra][\n]
///   3. 避免头中二进制 0x0A 误判为换行符:
///      - 有序号时从 index >= 11 开始查找 '\n'
///      - 无序号时从 index >= 10 开始查找 '\n'
/// </summary>
public sealed class PacketBuffer
{
    private readonly List<byte> _buffer = new(4096);
    public const int MaxPacketSize = 65535;

    public int AvailableBytes => _buffer.Count;

    public void Append(ReadOnlySpan<byte> data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            _buffer.Add(data[i]);
        }
    }

    public void Clear() => _buffer.Clear();

    /// <summary>
    /// 从缓冲区提取下一个完整的数据包 (去除结尾的 '\n')
    /// 若无完整包则返回 null
    /// </summary>
    public byte[]? PopPacket()
    {
        if (_buffer.Count == 0) return null;

        byte first = _buffer[0];
        bool isCtrl = (first == 0x2B || first == 0x3D); // '+' or '='

        int minNlIdx = 1;
        if (!isCtrl)
        {
            if (first >= 0x30 && first <= 0x39) // '0'..'9' 序号
            {
                minNlIdx = 11;
            }
            else
            {
                minNlIdx = 10;
            }
        }

        if (_buffer.Count <= minNlIdx)
        {
            return null; // 数据不足以容纳包头
        }

        int nlPos = -1;
        for (int i = minNlIdx; i < _buffer.Count; i++)
        {
            if (_buffer[i] == 0x0A) // '\n'
            {
                nlPos = i;
                break;
            }
        }

        if (nlPos < 0)
        {
            if (_buffer.Count > MaxPacketSize)
            {
                _buffer.Clear();
                throw new InvalidOperationException($"Packet buffer overflow: {_buffer.Count} bytes");
            }
            return null;
        }

        byte[] packet = new byte[nlPos];
        _buffer.CopyTo(0, packet, 0, nlPos);

        // 移除已消费的数据（包括 '\n'）
        _buffer.RemoveRange(0, nlPos + 1);

        return packet;
    }
}
