using System;

namespace Mir2.Shared.Protocols;

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

    /// <summary>
    /// 解析现代 HPSocket TCP-Pack 下行封包载荷 (16 字节标准整数字段: Recog, Param, Tag, Series + UTF-8 Extra)
    /// </summary>
    public static ServerPacket ParseModernPayload(short ident, ReadOnlySpan<byte> payload)
    {
        if (payload.Length < 16)
        {
            byte[] rawExtra = payload.Length > 0 ? payload.ToArray() : Array.Empty<byte>();
            return new ServerPacket(ident, 0, 0, 0, rawExtra);
        }

        int recog = System.Buffers.Binary.BinaryPrimitives.ReadInt32LittleEndian(payload[0..4]);
        int param = System.Buffers.Binary.BinaryPrimitives.ReadInt32LittleEndian(payload[4..8]);
        int tag = System.Buffers.Binary.BinaryPrimitives.ReadInt32LittleEndian(payload[8..12]);
        int series = System.Buffers.Binary.BinaryPrimitives.ReadInt32LittleEndian(payload[12..16]);
        byte[] extra = payload.Length > 16 ? payload[16..].ToArray() : Array.Empty<byte>();

        return new ServerPacket(ident, recog, param, tag, series, extra);
    }

    /// <summary>
    /// 解析官方 1.5 仓库物品列表 (SM_USERSTORAGE_ITEMS 704)
    /// 格式：制表符与换行符分隔条目 (name \t dura \t maxDura [\t itemIndex])
    /// </summary>
    public static System.Collections.Generic.List<StorageItemInfo> DecodeStorageItems(string extraText)
    {
        var list = new System.Collections.Generic.List<StorageItemInfo>();
        if (string.IsNullOrWhiteSpace(extraText))
            return list;

        var lines = extraText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in lines)
        {
            var trimmed = line.Trim();
            if (string.IsNullOrEmpty(trimmed))
                continue;

            var parts = trimmed.Split('\t');
            string name = parts[0].Trim();
            int dura = 0;
            int maxDura = 0;
            int index = 0;
            if (parts.Length > 1 && int.TryParse(parts[1], out int d))
                dura = d;
            if (parts.Length > 2 && int.TryParse(parts[2], out int md))
                maxDura = md;
            if (parts.Length > 3 && int.TryParse(parts[3], out int idx))
                index = idx;

            list.Add(new StorageItemInfo(name, dura, maxDura, index));
        }
        return list;
    }

    /// <summary>
    /// 解析盛大官方 1.5 商店商品条目列表 (SM_MERCHANT_ITEM 645, SM_MERCHANT_SELL_LIST 652)
    /// Extra 包含制表符、斜杠或换行符分隔的商品条目:
    /// 每行格式：名称 [\t 或 / 或 空格] 价格 [\t 或 / 或 空格] 重量或当前持久 [\t 或 / 或 空格] 最大持久
    /// </summary>
    public static System.Collections.Generic.List<ShopItemEntry> DecodeShopItems(string extraText)
    {
        var list = new System.Collections.Generic.List<ShopItemEntry>();
        if (string.IsNullOrWhiteSpace(extraText))
            return list;

        var lines = extraText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in lines)
        {
            var trimmed = line.Trim();
            if (string.IsNullOrEmpty(trimmed))
                continue;

            // 优先按制表符 \t 分隔，若无则按 / 分隔，最后按空格分隔
            char separator = trimmed.Contains('\t') ? '\t' : (trimmed.Contains('/') ? '/' : ' ');
            var parts = trimmed.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0)
                continue;

            string name = parts[0].Trim();
            int price = 0;
            int weightOrDura = 0;
            int maxDura = 0;

            if (parts.Length > 1 && int.TryParse(parts[1].Trim(), out int p))
                price = p;
            if (parts.Length > 2 && int.TryParse(parts[2].Trim(), out int w))
                weightOrDura = w;
            if (parts.Length > 3 && int.TryParse(parts[3].Trim(), out int md))
                maxDura = md;

            var entry = new ShopItemEntry(name, weightOrDura, price);
            if (maxDura > 0)
            {
                entry.Dura = weightOrDura;
                entry.MaxDura = maxDura;
            }
            list.Add(entry);
        }
        return list;
    }
}
