using System;
using System.Buffers.Binary;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mir2.Server.Persistence;

/// <summary>
/// 盛大官方 1.5 预写式事务日志引擎 (Write-Ahead Logging / Redo Log Engine)
/// 针对《工程认知公理 4.0》中揭示的“异步持久化在极端断电或进程 SIGKILL 下存在内存丢失窗口”坏账，
/// 实现 ACID 级别的顺序追加预写物理刷盘 (Sequential Append-Only + fsync) 与崩溃自动重放机制。
/// </summary>
public sealed class WriteAheadLogEngine : IDisposable
{
    public const uint WalMagic = 0x4D495257; // "MIRW"
    public const byte WalVersion = 1;

    private static readonly uint[] CrcTable = new uint[256];

    static WriteAheadLogEngine()
    {
        const uint poly = 0xEDB88320;
        for (uint i = 0; i < 256; i++)
        {
            uint crc = i;
            for (int j = 8; j > 0; j--)
            {
                if ((crc & 1) == 1) crc = (crc >> 1) ^ poly;
                else crc >>= 1;
            }
            CrcTable[i] = crc;
        }
    }

    public static uint ComputeCrc32(ReadOnlySpan<byte> data)
    {
        uint crc = 0xFFFFFFFF;
        for (int i = 0; i < data.Length; i++)
        {
            byte tableIndex = (byte)((crc & 0xFF) ^ data[i]);
            crc = (crc >> 8) ^ CrcTable[tableIndex];
        }
        return ~crc;
    }

    public enum TransactionType : byte
    {
        Checkpoint = 0,
        ItemTraded = 1,   // 面对面交易完成 (双方法人与道具移交)
        ItemPicked = 2,   // 极品神兵拾取
        GoldUpdated = 3,  // 金币增减变动
        PlayerSnapshot = 4 // 核心角色属性快照
    }

    public sealed record WalRecord(
        ulong Lsn,
        long TimestampMs,
        TransactionType Type,
        byte[] Payload
    );

    // 结构化业务负载模型
    public sealed record TradePayload(
        string SenderName,
        string ReceiverName,
        int ItemUid,
        string ItemName,
        int GoldAmount
    );

    public sealed record PickUpPayload(
        string PlayerName,
        int ItemUid,
        string ItemName,
        int MapId,
        int X,
        int Y
    );

    public sealed record GoldPayload(
        string PlayerName,
        int DeltaGold,
        int FinalGold,
        string Reason
    );

    private readonly string _walFilePath;
    private FileStream? _fileStream;
    private readonly object _writeLock = new();
    private ulong _currentLsn = 0;
    private bool _isDisposed = false;
    private int _recordsSinceLastCheckpoint = 0;

    public ulong CurrentLsn => _currentLsn;
    public string WalFilePath => _walFilePath;

    /// <summary>WAL 单文件容量上限阈值 (默认 32MB，超出自动轮转截断)</summary>
    public long MaxWalFileSizeBytes { get; set; } = 32 * 1024 * 1024;

    /// <summary>触发 Checkpoint 的事务计数门限 (默认 5000 条)</summary>
    public int CheckpointRecordInterval { get; set; } = 5000;

    public WriteAheadLogEngine(string walFilePath)
    {
        _walFilePath = walFilePath;
        EnsureInitialized();
    }

    private void EnsureInitialized()
    {
        lock (_writeLock)
        {
            var dir = Path.GetDirectoryName(_walFilePath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            bool fileExists = File.Exists(_walFilePath) && new FileInfo(_walFilePath).Length >= 5;
            _fileStream = new FileStream(_walFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read, 4096, FileOptions.WriteThrough);

            if (!fileExists)
            {
                // 写入文件头: Magic(4B) + Version(1B)
                Span<byte> header = stackalloc byte[5];
                BinaryPrimitives.WriteUInt32LittleEndian(header[..4], WalMagic);
                header[4] = WalVersion;
                _fileStream.Write(header);
                _fileStream.Flush(flushToDisk: true);
                _currentLsn = 0;
            }
            else
            {
                _fileStream.Seek(0, SeekOrigin.End);
            }
        }
    }

    /// <summary>
    /// 触发安全 Checkpoint 与 WAL 截断轮转
    /// 1. 刷入 Checkpoint 标记物理帧
    /// 2. 触发持久化快照保存回调 (如写 JSON 存档)
    /// 3. 安全截断当前 WAL 文件至 5 字节文件头，重置游标与计数器，彻底解决磁盘膨胀坏账
    /// </summary>
    public bool CreateCheckpointAndTruncate(Func<bool>? snapshotCallback = null)
    {
        lock (_writeLock)
        {
            if (_isDisposed || _fileStream == null) return false;

            // 1. 写入 Checkpoint 记录并刷盘
            AppendRecordInternal(TransactionType.Checkpoint, Array.Empty<byte>(), syncToDisk: true);

            // 2. 调用外部快照落地回调
            if (snapshotCallback != null)
            {
                bool snapshotSuccess = snapshotCallback();
                if (!snapshotSuccess)
                {
                    // 快照保存失败，绝不截断 WAL，保障最高安全性
                    return false;
                }
            }

            // 3. 安全截断 WAL 文件并重写文件头
            _fileStream.Seek(0, SeekOrigin.Begin);
            Span<byte> header = stackalloc byte[5];
            BinaryPrimitives.WriteUInt32LittleEndian(header[..4], WalMagic);
            header[4] = WalVersion;
            _fileStream.Write(header);
            _fileStream.SetLength(5);
            _fileStream.Flush(flushToDisk: true);

            _recordsSinceLastCheckpoint = 0;
            return true;
        }
    }

    /// <summary>
    /// 检查并自动执行滚动 Checkpoint (当文件过大或记录过多时)
    /// </summary>
    public bool CheckAndTriggerAutoCheckpoint(Func<bool>? snapshotCallback = null)
    {
        lock (_writeLock)
        {
            if (_fileStream == null) return false;

            if (_fileStream.Length >= MaxWalFileSizeBytes || _recordsSinceLastCheckpoint >= CheckpointRecordInterval)
            {
                return CreateCheckpointAndTruncate(snapshotCallback);
            }
            return false;
        }
    }

    /// <summary>
    /// 预写单条日志记录并调用物理 fsync 刷盘 (Durable)
    /// 每条物理帧格式:
    /// [Lsn: 8B] [TimestampMs: 8B] [TxType: 1B] [PayloadLen: 4B] [Payload: N B] [Crc32: 4B]
    /// </summary>
    public ulong AppendRecord(TransactionType type, ReadOnlySpan<byte> payload, bool syncToDisk = true)
    {
        lock (_writeLock)
        {
            if (_isDisposed || _fileStream == null)
            {
                throw new ObjectDisposedException(nameof(WriteAheadLogEngine));
            }

            _recordsSinceLastCheckpoint++;
            return AppendRecordInternal(type, payload, syncToDisk);
        }
    }

    private ulong AppendRecordInternal(TransactionType type, ReadOnlySpan<byte> payload, bool syncToDisk)
    {
        ulong lsn = ++_currentLsn;
        long nowMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        int payloadLen = payload.Length;
        int recordSize = 8 + 8 + 1 + 4 + payloadLen + 4;

        byte[] buffer = new byte[recordSize];
        Span<byte> span = buffer.AsSpan();

        BinaryPrimitives.WriteUInt64LittleEndian(span[..8], lsn);
        BinaryPrimitives.WriteInt64LittleEndian(span.Slice(8, 8), nowMs);
        span[16] = (byte)type;
        BinaryPrimitives.WriteInt32LittleEndian(span.Slice(17, 4), payloadLen);
        payload.CopyTo(span.Slice(21, payloadLen));

        // 计算 CRC32 校验码 (覆盖 LSN 到 Payload 末尾)
        uint crc = ComputeCrc32(span[..(21 + payloadLen)]);
        BinaryPrimitives.WriteUInt32LittleEndian(span.Slice(21 + payloadLen, 4), crc);

        _fileStream!.Write(buffer);
        if (syncToDisk)
        {
            _fileStream.Flush(flushToDisk: true);
        }

        return lsn;
    }

    /// <summary>
    /// 记录面对面交易事务物理日志 (极品防丢)
    /// </summary>
    public ulong LogItemTrade(string sender, string receiver, int itemUid, string itemName, int gold, bool syncToDisk = true)
    {
        var data = new TradePayload(sender, receiver, itemUid, itemName, gold);
        byte[] jsonBytes = JsonSerializer.SerializeToUtf8Bytes(data, WalJsonContext.Default.TradePayload);
        return AppendRecord(TransactionType.ItemTraded, jsonBytes, syncToDisk);
    }

    /// <summary>
    /// 记录神兵地表拾取事务物理日志
    /// </summary>
    public ulong LogItemPickUp(string player, int itemUid, string itemName, int mapId, int x, int y, bool syncToDisk = true)
    {
        var data = new PickUpPayload(player, itemUid, itemName, mapId, x, y);
        byte[] jsonBytes = JsonSerializer.SerializeToUtf8Bytes(data, WalJsonContext.Default.PickUpPayload);
        return AppendRecord(TransactionType.ItemPicked, jsonBytes, syncToDisk);
    }

    /// <summary>
    /// 记录金币物理结算变动日志
    /// </summary>
    public ulong LogGoldUpdate(string player, int delta, int finalGold, string reason, bool syncToDisk = true)
    {
        var data = new GoldPayload(player, delta, finalGold, reason);
        byte[] jsonBytes = JsonSerializer.SerializeToUtf8Bytes(data, WalJsonContext.Default.GoldPayload);
        return AppendRecord(TransactionType.GoldUpdated, jsonBytes, syncToDisk);
    }

    /// <summary>
    /// 灾难恢复重放器：扫描 WAL 文件并自动将未提交/崩溃丢失的事务补偿重放
    /// 若扫描到末尾因断电产生的半写损坏记录，自动熔断并保证数据一致性。
    /// </summary>
    public static int ReplayWal(string walFilePath, Action<WalRecord> onRecord)
    {
        if (!File.Exists(walFilePath)) return 0;

        using var fs = new FileStream(walFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        if (fs.Length < 5) return 0;

        byte[] header = new byte[5];
        if (fs.Read(header, 0, 5) != 5) return 0;

        uint magic = BinaryPrimitives.ReadUInt32LittleEndian(header.AsSpan(0, 4));
        byte version = header[4];
        if (magic != WalMagic || version != WalVersion)
        {
            throw new InvalidDataException($"无效的 WAL 文件魔数: 0x{magic:X8} 版本: {version}");
        }

        int replayedCount = 0;
        byte[] metaBuf = new byte[21]; // Lsn(8) + Time(8) + Type(1) + Len(4)

        while (fs.Position + 25 <= fs.Length) // 至少需要 21B 元数据 + 4B CRC
        {
            long recordStartPos = fs.Position;
            if (fs.Read(metaBuf, 0, 21) != 21) break;

            ulong lsn = BinaryPrimitives.ReadUInt64LittleEndian(metaBuf.AsSpan(0, 8));
            long timeMs = BinaryPrimitives.ReadInt64LittleEndian(metaBuf.AsSpan(8, 8));
            var type = (TransactionType)metaBuf[16];
            int payloadLen = BinaryPrimitives.ReadInt32LittleEndian(metaBuf.AsSpan(17, 4));

            if (payloadLen < 0 || fs.Position + payloadLen + 4 > fs.Length)
            {
                // 检测到断电残缺截断记录，停止扫描
                break;
            }

            byte[] payload = new byte[payloadLen];
            if (fs.Read(payload, 0, payloadLen) != payloadLen) break;

            byte[] crcBuf = new byte[4];
            if (fs.Read(crcBuf, 0, 4) != 4) break;
            uint expectedCrc = BinaryPrimitives.ReadUInt32LittleEndian(crcBuf);

            // 验证 CRC32
            byte[] verifyBuf = new byte[21 + payloadLen];
            Buffer.BlockCopy(metaBuf, 0, verifyBuf, 0, 21);
            Buffer.BlockCopy(payload, 0, verifyBuf, 21, payloadLen);
            uint actualCrc = ComputeCrc32(verifyBuf);

            if (actualCrc != expectedCrc)
            {
                // CRC 校验不通过，判定遭遇断电破坏，安全截断退出
                break;
            }

            onRecord(new WalRecord(lsn, timeMs, type, payload));
            replayedCount++;
        }

        return replayedCount;
    }

    public void Dispose()
    {
        lock (_writeLock)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                _fileStream?.Flush(flushToDisk: true);
                _fileStream?.Dispose();
                _fileStream = null;
            }
        }
    }
}

[JsonSerializable(typeof(WriteAheadLogEngine.TradePayload))]
[JsonSerializable(typeof(WriteAheadLogEngine.PickUpPayload))]
[JsonSerializable(typeof(WriteAheadLogEngine.GoldPayload))]
internal partial class WalJsonContext : JsonSerializerContext
{
}
