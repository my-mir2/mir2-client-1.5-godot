using System;
using System.IO;

namespace Mir2Client.Render;

/// <summary>
/// 盛大官方 1.5 地图单元格数据结构 (12 字节列优先排列, IDA 0x47009C（已证client无函数） 实证)
/// </summary>
public readonly record struct MapCell(
    short FloorRaw,
    short MidRaw,
    short ObjRaw,
    byte FileIdx,
    byte Light
)
{
    public int FloorIdx => (FloorRaw & 0x7FFF) > 0 ? (FloorRaw & 0x7FFF) - 1 : -1;
    public int SmTileIdx => (MidRaw & 0x7FFF) > 0 ? (MidRaw & 0x7FFF) - 1 : -1;
    public int ObjIdx => (ObjRaw & 0x7FFF) > 0 ? (ObjRaw & 0x7FFF) - 1 : -1;

    // 最高位 MSB=1 表示阻挡不可通行
    public bool Passable => (FloorRaw & 0x8000) == 0;
}

/// <summary>
/// 官方 .map 文件解析器 (Header=52B, Cells=W*H*12B)
/// </summary>
public sealed class MapData
{
    public const int HeaderSize = 52;
    public const int CellSize = 12;

    public int Width { get; private set; }
    public int Height { get; private set; }
    private byte[] _cellData = Array.Empty<byte>();

    public bool Load(string mapFilePath)
    {
        if (!File.Exists(mapFilePath)) return false;

        using var fs = new FileStream(mapFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        if (fs.Length < HeaderSize) return false;

        byte[] header = new byte[HeaderSize];
        fs.ReadExactly(header);

        Width = header[0] | (header[1] << 8);
        Height = header[2] | (header[3] << 8);

        if (Width <= 0 || Height <= 0 || Width > 4000 || Height > 4000)
        {
            return false;
        }

        int totalBytes = Width * Height * CellSize;
        _cellData = new byte[totalBytes];
        int read = fs.Read(_cellData, 0, totalBytes);
        return read == totalBytes;
    }

    public MapCell? GetCell(int x, int y)
    {
        if (x < 0 || y < 0 || x >= Width || y >= Height) return null;
        int offset = (x * Height + y) * CellSize;
        if (offset + CellSize > _cellData.Length) return null;

        short floorRaw = (short)(_cellData[offset] | (_cellData[offset + 1] << 8));
        short midRaw = (short)(_cellData[offset + 2] | (_cellData[offset + 3] << 8));
        short objRaw = (short)(_cellData[offset + 4] | (_cellData[offset + 5] << 8));
        byte fileIdx = _cellData[offset + 10];
        byte light = _cellData[offset + 11];

        return new MapCell(floorRaw, midRaw, objRaw, fileIdx, light);
    }

    public bool IsPassable(int x, int y)
    {
        var cell = GetCell(x, y);
        return cell.HasValue && cell.Value.Passable;
    }

    public bool SetPassable(int x, int y, bool passable)
    {
        if (x < 0 || y < 0 || x >= Width || y >= Height) return false;
        int offset = (x * Height + y) * CellSize;
        if (offset + CellSize > _cellData.Length) return false;

        short floorRaw = (short)(_cellData[offset] | (_cellData[offset + 1] << 8));
        if (passable)
        {
            floorRaw = (short)(floorRaw & 0x7FFF); // 清除 MSB，设为可通行
        }
        else
        {
            floorRaw = (short)(floorRaw | unchecked((short)0x8000)); // 设定 MSB，设为不可通行
        }

        _cellData[offset] = (byte)(floorRaw & 0xFF);
        _cellData[offset + 1] = (byte)((floorRaw >> 8) & 0xFF);
        return true;
    }
}
