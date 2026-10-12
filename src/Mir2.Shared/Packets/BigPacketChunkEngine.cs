using System;
using System.Buffers.Binary;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Mir2.Shared.Protocols.Modern;

namespace Mir2.Shared.Packets;

/// <summary>
/// 盛大官方 1.5 现代网络通信 64KB 超大载荷分片与重组引擎 (Big Packet Chunking & Multiplexing Engine)
/// 专为解决 HPSocket TCP-Pack 单包 64KB (ushort) 物理上限而设计。
/// 支持沙巴克全服花名册、大地图连续瓦片、复杂 NPC 对话脚本等超大数据的零损坏切片与原子重组。
/// </summary>
public static class BigPacketChunkEngine
{
    public const ushort MsgIdChunkData = 0x00FF; // 大包切片专用协议码
    public const int DefaultChunkSize = 32768;   // 默认单片 32KB，留足帧头余量
    public const int ChunkHeaderSize = 16;       // 切片头元数据固定 16 字节

    /// <summary>
    /// 切片帧头定义 (16 字节对齐)
    /// +--------------------------------------------------------------------------------+
    /// | TransferId (4B) | OriginalMsgId (2B) | TotalChunks (2B) | ChunkIndex (2B) | Reserved (2B) | TotalLength (4B) |
    /// +--------------------------------------------------------------------------------+
    /// </summary>
    public readonly record struct ChunkMeta(
        uint TransferId,
        ushort OriginalMsgId,
        ushort TotalChunks,
        ushort ChunkIndex,
        int TotalLength)
    {
        public void WriteTo(Span<byte> destination)
        {
            if (destination.Length < ChunkHeaderSize)
                throw new ArgumentException("目标缓冲区不足 16 字节切片头");

            BinaryPrimitives.WriteUInt32LittleEndian(destination[0..4], TransferId);
            BinaryPrimitives.WriteUInt16LittleEndian(destination[4..6], OriginalMsgId);
            BinaryPrimitives.WriteUInt16LittleEndian(destination[6..8], TotalChunks);
            BinaryPrimitives.WriteUInt16LittleEndian(destination[8..10], ChunkIndex);
            BinaryPrimitives.WriteUInt16LittleEndian(destination[10..12], 0); // 保留位对齐
            BinaryPrimitives.WriteInt32LittleEndian(destination[12..16], TotalLength);
        }

        public static ChunkMeta ReadFrom(ReadOnlySpan<byte> source)
        {
            if (source.Length < ChunkHeaderSize)
                throw new ArgumentException("源数据不足 16 字节切片头");

            return new ChunkMeta(
                TransferId: BinaryPrimitives.ReadUInt32LittleEndian(source[0..4]),
                OriginalMsgId: BinaryPrimitives.ReadUInt16LittleEndian(source[4..6]),
                TotalChunks: BinaryPrimitives.ReadUInt16LittleEndian(source[6..8]),
                ChunkIndex: BinaryPrimitives.ReadUInt16LittleEndian(source[8..10]),
                TotalLength: BinaryPrimitives.ReadInt32LittleEndian(source[12..16])
            );
        }
    }

    /// <summary>
    /// 大包切片生成器 (Chunk Splitter)
    /// 将超大载荷分割为带切片头的多个合法切片
    /// </summary>
    public static List<byte[]> SplitPayload(uint transferId, ushort originalMsgId, ReadOnlySpan<byte> fullPayload, int chunkSize = DefaultChunkSize)
    {
        if (chunkSize <= 0 || chunkSize > 60000)
            chunkSize = DefaultChunkSize;

        int totalLen = fullPayload.Length;
        ushort totalChunks = (ushort)Math.Max(1, (totalLen + chunkSize - 1) / chunkSize);
        var chunks = new List<byte[]>(totalChunks);

        for (ushort i = 0; i < totalChunks; i++)
        {
            int offset = i * chunkSize;
            int length = Math.Min(chunkSize, totalLen - offset);

            byte[] chunkBuffer = new byte[ChunkHeaderSize + length];
            var meta = new ChunkMeta(transferId, originalMsgId, totalChunks, i, totalLen);
            meta.WriteTo(chunkBuffer.AsSpan(0, ChunkHeaderSize));

            fullPayload.Slice(offset, length).CopyTo(chunkBuffer.AsSpan(ChunkHeaderSize));
            chunks.Add(chunkBuffer);
        }

        return chunks;
    }

    /// <summary>
    /// 客户端/服务端单端分片装配器 (Thread-Safe Chunk Assembler)
    /// 负责按 TransferId 收集乱序切片，超时自动回收，全部到齐后原子组装并交付
    /// </summary>
    public sealed class Assembler
    {
        private sealed class TransferSession
        {
            public uint TransferId { get; }
            public ushort OriginalMsgId { get; }
            public ushort TotalChunks { get; }
            public int TotalLength { get; }
            public long CreatedTickMs { get; set; }
            public byte[][] Chunks { get; }
            public int ReceivedCount { get; set; }

            public TransferSession(ChunkMeta meta, long nowMs)
            {
                TransferId = meta.TransferId;
                OriginalMsgId = meta.OriginalMsgId;
                TotalChunks = meta.TotalChunks;
                TotalLength = meta.TotalLength;
                CreatedTickMs = nowMs;
                Chunks = new byte[meta.TotalChunks][];
                ReceivedCount = 0;
            }
        }

        private readonly ConcurrentDictionary<uint, TransferSession> _transfers = new();
        private readonly int _timeoutMs;

        public Assembler(int timeoutMs = 15000)
        {
            _timeoutMs = timeoutMs;
        }

        /// <summary>
        /// 处理到达的一个切片数据。如果组装完毕，返回 (OriginalMsgId, AssembledPayload)，否则返回 null
        /// </summary>
        public (ushort OriginalMsgId, byte[] Payload)? ProcessIncomingChunk(ReadOnlySpan<byte> chunkBytes, long nowMs)
        {
            if (chunkBytes.Length < ChunkHeaderSize)
                return null;

            var meta = ChunkMeta.ReadFrom(chunkBytes[..ChunkHeaderSize]);
            if (meta.TotalChunks == 0 || meta.ChunkIndex >= meta.TotalChunks)
                return null;

            // 清理超时的旧会话
            CleanupExpired(nowMs);

            var session = _transfers.GetOrAdd(meta.TransferId, _ => new TransferSession(meta, nowMs));

            lock (session)
            {
                if (session.Chunks[meta.ChunkIndex] == null)
                {
                    int payloadLen = chunkBytes.Length - ChunkHeaderSize;
                    byte[] data = new byte[payloadLen];
                    chunkBytes.Slice(ChunkHeaderSize, payloadLen).CopyTo(data);

                    session.Chunks[meta.ChunkIndex] = data;
                    session.ReceivedCount++;
                }

                if (session.ReceivedCount == session.TotalChunks)
                {
                    _transfers.TryRemove(meta.TransferId, out _);

                    // 组装最终完整载荷
                    byte[] assembled = new byte[session.TotalLength];
                    int writeOffset = 0;
                    for (int i = 0; i < session.TotalChunks; i++)
                    {
                        var part = session.Chunks[i];
                        if (part != null && writeOffset + part.Length <= assembled.Length)
                        {
                            Buffer.BlockCopy(part, 0, assembled, writeOffset, part.Length);
                            writeOffset += part.Length;
                        }
                    }

                    return (session.OriginalMsgId, assembled);
                }
            }

            return null;
        }

        private void CleanupExpired(long nowMs)
        {
            foreach (var kvp in _transfers)
            {
                if (nowMs - kvp.Value.CreatedTickMs > _timeoutMs)
                {
                    _transfers.TryRemove(kvp.Key, out _);
                }
            }
        }

        public void Clear() => _transfers.Clear();
    }
}
