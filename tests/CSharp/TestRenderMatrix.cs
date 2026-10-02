using System;
using System.IO;
using Mir2Client.Render;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 官方标准 xUnit 渲染引擎与地图数据读取测试套件
/// 独立于游戏引擎渲染主循环，直接由 dotnet test 高速执行，绝不挂起
/// </summary>
public class TestRenderMatrix
{
    [Fact]
    public void TestMapData_HeaderAndCellParsing()
    {
        // 构造一个模拟 2x2 的官方 .map 二进制数据流
        int width = 2;
        int height = 2;
        byte[] buffer = new byte[MapData.HeaderSize + width * height * MapData.CellSize];

        // Header: width, height
        buffer[0] = (byte)(width & 0xFF);
        buffer[1] = (byte)((width >> 8) & 0xFF);
        buffer[2] = (byte)(height & 0xFF);
        buffer[3] = (byte)((height >> 8) & 0xFF);

        // 设置 cell (0, 0): floor_raw = 10 (passable = true)
        int offset00 = MapData.HeaderSize + (0 * height + 0) * MapData.CellSize;
        buffer[offset00] = 10;
        buffer[offset00 + 1] = 0;

        // 设置 cell (1, 1): floor_raw = 0x8005 (MSB=1, passable = false 阻挡)
        int offset11 = MapData.HeaderSize + (1 * height + 1) * MapData.CellSize;
        buffer[offset11] = 0x05;
        buffer[offset11 + 1] = 0x80;

        string tempPath = Path.Combine(Path.GetTempPath(), $"mir2_test_{Guid.NewGuid():N}.map");
        try
        {
            File.WriteAllBytes(tempPath, buffer);

            var map = new MapData();
            bool loaded = map.Load(tempPath);
            Assert.True(loaded);
            Assert.Equal(2, map.Width);
            Assert.Equal(2, map.Height);

            Assert.True(map.IsPassable(0, 0));
            Assert.False(map.IsPassable(1, 1)); // 阻挡点不可通行

            var cell00 = map.GetCell(0, 0);
            Assert.NotNull(cell00);
            Assert.Equal(9, cell00.Value.FloorIdx); // 10 - 1 = 9
        }
        finally
        {
            if (File.Exists(tempPath))
            {
                File.Delete(tempPath);
            }
        }
    }
}
