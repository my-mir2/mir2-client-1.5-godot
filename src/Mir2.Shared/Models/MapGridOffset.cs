namespace Mir2.Shared.Models;

/// <summary>
/// 二维地图格子坐标偏移 (跨平台纯 C# 零依赖，兼容服务端与客户端)
/// </summary>
public readonly record struct MapGridOffset(int X, int Y)
{
    public static implicit operator (int X, int Y)(MapGridOffset offset) => (offset.X, offset.Y);
    public static implicit operator MapGridOffset((int X, int Y) tuple) => new(tuple.X, tuple.Y);
}
