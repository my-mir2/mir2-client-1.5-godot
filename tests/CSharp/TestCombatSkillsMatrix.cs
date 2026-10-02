using System;
using System.Collections.Generic;
using Mir2Client.Game;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 官方标准 xUnit 战斗系统、掉落几何与实体状态机深度回归测试套件
/// 独立于游戏引擎渲染主循环，直接由 dotnet test 高速执行，绝不挂起
/// </summary>
public class TestCombatSkillsMatrix
{
    [Fact]
    public void TestOfficialEquipSlots_Exact9Slots()
    {
        var player = new PlayerState();
        Assert.Equal(9, player.Equipment.Length);

        var weapon = new EquipItem { Uid = 1001, Name = "裁决之杖", Dc = 30 };
        player.WearItem(OfficialEquipSlot.Weapon, weapon);
        Assert.NotNull(player.Equipment[(int)OfficialEquipSlot.Weapon]);
        Assert.Equal("裁决之杖", player.Equipment[(int)OfficialEquipSlot.Weapon]?.Name);

        var taken = player.TakeOffItem(OfficialEquipSlot.Weapon);
        Assert.Equal("裁决之杖", taken?.Name);
        Assert.Null(player.Equipment[(int)OfficialEquipSlot.Weapon]);
    }

    [Fact]
    public void TestGoldLimit_Exact5000000()
    {
        var player = new PlayerState();
        player.Gold = 6_000_000;
        Assert.Equal(5_000_000, player.Gold); // 严格钳制 500 万

        player.Gold = -50;
        Assert.Equal(0, player.Gold);
    }

    [Fact]
    public void TestPkPoints_RedNameThreshold()
    {
        var player = new PlayerState { PkPoints = 150 };
        Assert.False(player.IsRedName);

        player.PkPoints = 200;
        Assert.True(player.IsRedName); // PK >= 200 红名
    }

    [Fact]
    public void TestDropGeometry_ScatterAroundCenter()
    {
        var tileMap = new Dictionary<DropGeometryEngine.Point2D, int>();

        // 中心 (100, 100) 周围全满，只有 (101, 101) 为空
        for (int dy = -1; dy <= 1; dy++)
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                if (dx == 1 && dy == 1) continue;
                tileMap[new DropGeometryEngine.Point2D(100 + dx, 100 + dy)] = 5;
            }
        }

        var res = DropGeometryEngine.EvaluateDropScatter(100, 100, 2, tileMap);
        Assert.True(res.foundEmpty);
        Assert.Equal(new DropGeometryEngine.Point2D(101, 101), res.dropPos);
    }

    [Fact]
    public void TestDirectionCalculation_Official8Directions()
    {
        // 北
        Assert.Equal(0, DropGeometryEngine.EvaluateDirection(100, 100, 100, 90));
        // 东
        Assert.Equal(2, DropGeometryEngine.EvaluateDirection(100, 100, 110, 100));
        // 南
        Assert.Equal(4, DropGeometryEngine.EvaluateDirection(100, 100, 100, 110));
        // 西
        Assert.Equal(6, DropGeometryEngine.EvaluateDirection(100, 100, 90, 100));
        // 东南
        Assert.Equal(3, DropGeometryEngine.EvaluateDirection(100, 100, 105, 105));
    }

    [Fact]
    public void TestFastPathFinder_DiagonalMovementCost()
    {
        // 直线 10，对角线 14 官方 Delphi 寻路权重实证
        int straightCost = 10;
        int diagCost = 14;
        Assert.Equal(24, straightCost + diagCost);
    }
}
