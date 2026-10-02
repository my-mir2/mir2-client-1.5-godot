using System;

namespace Mir2Client.Net;

/// <summary>
/// 盛大官方 1.5 原版 SM 数据包反序列化器 (IDA sub_458D3C 逆推实证)
/// 包结构:
///   [2B ident] + [4B recog] + [2B param] + [2B tag] + [nB +60 编码 Extra]
/// </summary>
public static class PacketDecoder
{
    public static ServerPacket? Parse(ReadOnlySpan<byte> raw)
    {
        if (raw.Length < 10) return null;

        // 1. 读取头部 10 字节 (LE 序)
        short ident = (short)(raw[0] | (raw[1] << 8));
        int recog = raw[2] | (raw[3] << 8) | (raw[4] << 16) | (raw[5] << 24);
        short param = (short)(raw[6] | (raw[7] << 8));
        short tag = (short)(raw[8] | (raw[9] << 8));

        // 2. 解码 +60 附加数据
        byte[] extraDecoded;
        if (raw.Length > 10)
        {
            ReadOnlySpan<byte> extraEncoded = raw[10..];
            // 过滤结尾可能存在的 '\n' 或 '\r'
            int end = extraEncoded.Length;
            while (end > 0 && (extraEncoded[end - 1] == 0x0A || extraEncoded[end - 1] == 0x0D))
            {
                end--;
            }
            extraDecoded = PacketCodec.DecodePlus60(extraEncoded[..end]);
        }
        else
        {
            extraDecoded = Array.Empty<byte>();
        }

        return new ServerPacket(ident, recog, param, tag, extraDecoded);
    }
}
