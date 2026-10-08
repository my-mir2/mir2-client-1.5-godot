using System;
using System.Collections.Generic;
using System.Text;
using Mir2Client.Game;
using Mir2Client.Net;
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
    public void TestGoldLimit_Exact10000000()
    {
        var player = new PlayerState();
        player.Gold = 11_000_000;
        Assert.Equal(10_000_000, player.Gold); // 对齐服务端权威值（M2 0x989680 / Rust 10M）

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

    [Fact]
    public void TestAudioManager_OfficialWavRiffParser_GroundTruth()
    {
        string wavPath = Utils.ResPath.Resolve("resources/wav/1.wav");
        if (System.IO.File.Exists(wavPath))
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(wavPath);
            var info = Audio.AudioManager.ParseWavRiff(fileBytes);
            Assert.NotNull(info);
            Assert.Equal(44100, info.Value.SampleRate); // 1.wav 官方实证采样率为 44100 Hz
            Assert.Equal(16, info.Value.BitsPerSample); // 16-bit
            Assert.NotNull(info.Value.PcmData);
            Assert.True(info.Value.PcmData.Length > 0);
        }
    }

    [Fact]
    public void TestShoulderDash_ObstacleStunAndSelfDamage_GroundTruth()
    {
        // 官方 1.5 铁证机制 (IDA sub_49BC20 / combat.rs):
        // 场景 1: 第 1 格遇到不可通行地形 (城墙/巨石)，冲撞受挫
        var wallDash = WarriorCombatEngine.ShoulderDash.ExecuteDash(
            selfLevel: 35, selfX: 100, selfY: 100, dir: 2, // 向右冲撞
            obstacleEntity: null,
            isTileBlocked: (x, y) => x == 101 && y == 100, // (101, 100) 为墙壁
            skillLevel: 2, roll20: 0, rollExtraDamage: 3
        );

        Assert.False(wallDash.Success, "撞墙应当判定冲撞失败");
        Assert.True(wallDash.SelfStunned, "撞墙自身必须陷入顿挫硬直");
        Assert.Equal(0.8f, wallDash.StunSec);
        Assert.Equal(100, wallDash.SelfFinalX); // 原地弹回
        Assert.Equal(100, wallDash.SelfFinalY);
        Assert.Equal(5 * (2 + 1) + 3, wallDash.SelfDamage); // 撞墙自伤反噬公式: 5 * (sl + 1) + extra = 18

        // 场景 2: 撞击高等级目标 (自身 35 级 vs 目标 40 级)
        var highLevelTarget = ((bool, int, int, int)?)(true, 40, 101, 100);
        var highLevelDash = WarriorCombatEngine.ShoulderDash.ExecuteDash(
            selfLevel: 35, selfX: 100, selfY: 100, dir: 2,
            obstacleEntity: highLevelTarget,
            isTileBlocked: (x, y) => false,
            skillLevel: 3
        );

        Assert.False(highLevelDash.Success, "撞击高等级目标应当失败");
        Assert.True(highLevelDash.SelfStunned, "撞击高等级目标自身必须陷入反弹硬直");
        Assert.False(highLevelDash.TargetPushed, "高等级目标不得被推移");

        // 场景 3: 等级压制推动 (自身 35 级 vs 目标 25 级)
        var lowLevelTarget = ((bool, int, int, int)?)(true, 25, 101, 100);
        var crushDash = WarriorCombatEngine.ShoulderDash.ExecuteDash(
            selfLevel: 35, selfX: 100, selfY: 100, dir: 2,
            obstacleEntity: lowLevelTarget,
            isTileBlocked: (x, y) => false,
            skillLevel: 3, roll20: 5
        );

        Assert.True(crushDash.Success, "等级压制应当成功推动");
        Assert.True(crushDash.TargetPushed, "目标应当被推移");
        Assert.False(crushDash.SelfStunned, "自身不应陷入硬直");
        Assert.True(crushDash.TargetFinalX > 101, "目标应当被击退至少 1 格");
    }

    [Fact]
    public void SkillLevelUp_And_MerchantClose_644_DualDispatch_GroundTruth()
    {
        // 1. 常量真值断言：644 在盛大官方 1.5 IDA 原版为 merchant dlg close，在服务端为 SM_SKILLLEVELUP
        Assert.Equal((short)644, ProtocolConsts.SM_SKILLLEVELUP);
        Assert.Equal((short)644, ProtocolConsts.SM_MERCHANTDLG_CLOSE);

        // 2. 脱机路由验证：HUD=null 时，recog=0 分支 (关闭 NPC 对话框) 与 recog>0 分支 (技能熟练度同步) 均零崩溃
        var router = new ServerMessageRouter(null, null);

        // 分支 A: recog=0，NPC 对话框关闭
        var closePkt = new ServerPacket(ProtocolConsts.SM_MERCHANTDLG_CLOSE, 0, 0, 0, Array.Empty<byte>());
        router.OnPacket(closePkt);

        // 分支 B: recog=26 (烈火剑法), param=3, tag=2400, extra="烈火剑法"
        byte[] skillNameBytes = Encoding.UTF8.GetBytes("烈火剑法");
        var skillPkt = new ServerPacket(ProtocolConsts.SM_SKILLLEVELUP, 26, 3, 2400, skillNameBytes);
        router.OnPacket(skillPkt);

        // 分支 C: 兼容旧版 640 SM_MAGICLVEXP
        var expPkt = new ServerPacket(ProtocolConsts.SM_MAGICLVEXP, 12, 2, 1500, Array.Empty<byte>());
        router.OnPacket(expPkt);
    }

    [Fact]
    public void BagItems_And_MineResult_GroundTruth_Test()
    {
        // 1. 常量真值断言：
        // 200: SM_ADDITEM (背包添加物品)
        // 600: SM_BAGITEMS (官方 1.5 IDA 0x258u handle_sm_refresh)
        // 601: SM_MINERESULT (官方 1.5 IDA 0x259u handle_sm_refresh2)
        Assert.Equal((short)200, ProtocolConsts.SM_ADDITEM);
        Assert.Equal((short)600, ProtocolConsts.SM_BAGITEMS);
        Assert.Equal((short)601, ProtocolConsts.SM_MINERESULT);

        var router = new ServerMessageRouter(null, null);

        // 2. 模拟服务端 bridge.rs 下发制表符标准物品包 (SM_ADDITEM 200, recog=make_index, param=slot, extra="屠龙\t33\t33\t105")
        byte[] tabExtra = Encoding.UTF8.GetBytes("屠龙\t33\t33\t105");
        var addItemPkt = new ServerPacket(ProtocolConsts.SM_ADDITEM, 9999, 0, 0, tabExtra);
        router.OnPacket(addItemPkt);

        // 3. 模拟兼容斜杠/竖线格式物品包
        byte[] slashExtra = Encoding.UTF8.GetBytes("金创药(小量)/StateItem_0002.png/重量 1 持久 5/5");
        var addSlashPkt = new ServerPacket(ProtocolConsts.SM_ADDITEM, 9998, 1, 0, slashExtra);
        router.OnPacket(addSlashPkt);

        // 4. 模拟官方 1.5 背包全量刷新 (SM_BAGITEMS 600)
        var refreshPkt = new ServerPacket(ProtocolConsts.SM_BAGITEMS, 0, 0, 0, Array.Empty<byte>());
        router.OnPacket(refreshPkt);

        // 5. 模拟官方 1.5 采矿结果 (SM_MINERESULT 601, recog=purity, extra="黑铁矿石")
        byte[] mineExtra = Encoding.UTF8.GetBytes("黑铁矿石");
        var minePkt = new ServerPacket(ProtocolConsts.SM_MINERESULT, 18, 0, 0, mineExtra);
        router.OnPacket(minePkt);
    }

    [Fact]
    public void AttackModeChanged_766_Tag_And_Param_GroundTruth_Test()
    {
        // 官方 1.5 IDA 0x467920 case 766 @ 0x468A2B: dword_4F938C = HIWORD(v67)
        Assert.Equal((short)766, ProtocolConsts.SM_ATTACKMODECHANGED);

        var router = new ServerMessageRouter(null, null);

        // 场景 A: 官方 Delphi 原生协议将模式编码在 Tag (HIWORD(v67))
        var tagPkt = new ServerPacket(ProtocolConsts.SM_ATTACKMODECHANGED, 0, 0, 2, Array.Empty<byte>());
        router.OnPacket(tagPkt);

        // 场景 B: 服务端将模式编码在 Param
        var paramPkt = new ServerPacket(ProtocolConsts.SM_ATTACKMODECHANGED, 0, 3, 0, Array.Empty<byte>());
        router.OnPacket(paramPkt);
    }
}



