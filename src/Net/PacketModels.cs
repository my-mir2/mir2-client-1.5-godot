using System;
using System.Text;

namespace Mir2Client.Net;

/// <summary>
/// 客户端上行 (CM) 与服务端下行 (SM) 基础数据包结构
/// </summary>
public readonly struct ClientPacket
{
    public readonly short Ident;
    public readonly int Recog;
    public readonly short Param;
    public readonly short Tag;
    public readonly byte[] Extra;

    public ClientPacket(short ident, int recog = 0, short param = 0, short tag = 0, byte[]? extra = null)
    {
        Ident = ident;
        Recog = recog;
        Param = param;
        Tag = tag;
        Extra = extra ?? Array.Empty<byte>();
    }
}

public readonly struct ServerPacket
{
    public readonly short Ident;
    public readonly int Recog;
    public readonly short Param;
    public readonly short Tag;
    public readonly byte[] Extra;
    public readonly string ExtraStr;

    public ServerPacket(short ident, int recog, short param, short tag, byte[] extra)
    {
        Ident = ident;
        Recog = recog;
        Param = param;
        Tag = tag;
        Extra = extra;
        ExtraStr = SafeUtf8String(extra);
    }

    private static string SafeUtf8String(ReadOnlySpan<byte> data)
    {
        if (data.IsEmpty) return string.Empty;
        try
        {
            return Encoding.UTF8.GetString(data);
        }
        catch
        {
            return string.Empty;
        }
    }
}
