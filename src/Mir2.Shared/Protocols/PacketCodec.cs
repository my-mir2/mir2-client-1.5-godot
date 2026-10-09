using System;
using System.Text;

namespace Mir2.Shared.Protocols;

/// <summary>
/// 盛大官方 1.5 原版 Base64+60 快速指针与 Span 编解码器 (IDA sub_45AF90 & sub_45B030 铁证)
/// 算法规范 (与 Delphi 客户端 100% 比特级吻合):
///   - 编码: 将输入按 6-bit 分组，每个 6-bit 值加 60 得到一个 ASCII 字符 (范围 60~123)
///   - 解码: 将输入字符减 60 还原 6-bit 值，重新拼装为 8-bit 字节流
/// </summary>
public static class PacketCodec
{
    public const byte Bias = 60; // 0x3C

    /// <summary>
    /// 将字节流按 6-bit 编码为 +60 字节数组
    /// </summary>
    public static byte[] EncodePlus60(ReadOnlySpan<byte> data)
    {
        if (data.IsEmpty) return Array.Empty<byte>();

        int bitBuf = 0;
        int bitCount = 0;
        int maxLen = (data.Length * 8 + 5) / 6;
        Span<byte> outBuf = maxLen <= 512 ? stackalloc byte[maxLen] : new byte[maxLen];
        int outIdx = 0;

        for (int i = 0; i < data.Length; i++)
        {
            bitBuf = (bitBuf << 8) | data[i];
            bitCount += 8;
            while (bitCount >= 6)
            {
                bitCount -= 6;
                byte val = (byte)((bitBuf >> bitCount) & 0x3F);
                outBuf[outIdx++] = (byte)(val + Bias);
            }
        }

        if (bitCount > 0)
        {
            byte val = (byte)((bitBuf << (6 - bitCount)) & 0x3F);
            outBuf[outIdx++] = (byte)(val + Bias);
        }

        return outBuf[..outIdx].ToArray();
    }

    /// <summary>
    /// 将 +60 编码字节流还原为原始二进制字节数组
    /// </summary>
    public static byte[] DecodePlus60(ReadOnlySpan<byte> data)
    {
        if (data.IsEmpty) return Array.Empty<byte>();

        int bitBuf = 0;
        int bitCount = 0;
        int maxLen = (data.Length * 6) / 8;
        Span<byte> outBuf = maxLen <= 512 ? stackalloc byte[maxLen] : new byte[maxLen];
        int outIdx = 0;

        for (int i = 0; i < data.Length; i++)
        {
            byte b = data[i];
            if (b < 60 || b > 123) break;
            int val = b - Bias;
            bitBuf = (bitBuf << 6) | val;
            bitCount += 6;
            if (bitCount >= 8)
            {
                bitCount -= 8;
                outBuf[outIdx++] = (byte)((bitBuf >> bitCount) & 0xFF);
            }
        }

        return outBuf[..outIdx].ToArray();
    }
}
