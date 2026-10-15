using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Mir2.Server.Game;
using Mir2.Server.Maps;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 沙巴克皇宫 200+ 极限密度 AOI 降级与广播限流引擎权威压测矩阵 (TestSabukPalaceExtremeConcurrencyMatrix)
/// 针对《工程认知公理 4.0》中揭示的“沙巴克皇宫 15x15 狭小空间 200~300 人同屏铺满火墙冰咆哮广播击穿”坏账，
/// 实证验证 DynamicAoiLodEngine 的削峰降阶能力与 P0 核心逻辑 0 丢包保障。
/// </summary>
public class TestSabukPalaceExtremeConcurrencyMatrix
{
    private static ClientSession CreateTestSession(int sessionId, string name, int x, int y)
    {
        return new ClientSession(sessionId)
        {
            CharacterName = name,
            X = x,
            Y = y,
            CurrentMapId = 2, // 沙巴克皇宫
            State = new PlayerState { Name = name, Hp = 500, MaxHp = 500, Mp = 300, MaxMp = 300 }
        };
    }

    [Fact]
    public void TestSabukPalace_DensityLevelClassification()
    {
        var lodEngine = new DynamicAoiLodEngine();

        // 1. 正常开阔地 (<30人)
        Assert.Equal(DynamicAoiLodEngine.DensityLevel.Normal, lodEngine.EvaluateDensity(10));
        Assert.Equal(DynamicAoiLodEngine.DensityLevel.Normal, lodEngine.EvaluateDensity(29));

        // 2. 拥挤场景 (30~79人)
        Assert.Equal(DynamicAoiLodEngine.DensityLevel.Crowded, lodEngine.EvaluateDensity(30));
        Assert.Equal(DynamicAoiLodEngine.DensityLevel.Crowded, lodEngine.EvaluateDensity(79));

        // 3. 沙巴克皇宫决战极限密度 (>=80人，如 200 人)
        Assert.Equal(DynamicAoiLodEngine.DensityLevel.Extreme, lodEngine.EvaluateDensity(80));
        Assert.Equal(DynamicAoiLodEngine.DensityLevel.Extreme, lodEngine.EvaluateDensity(200));
    }

    [Fact]
    public void TestSabukPalace_PacketPriorityClassification()
    {
        // P0 绝对必达物理与生命周期
        Assert.Equal(DynamicAoiLodEngine.PacketPriority.P0_Critical, DynamicAoiLodEngine.ClassifyPacket(ProtocolConsts.SM_OBJECTSTRUCK_DELPHI));
        Assert.Equal(DynamicAoiLodEngine.PacketPriority.P0_Critical, DynamicAoiLodEngine.ClassifyPacket(ProtocolConsts.SM_DEATH));
        Assert.Equal(DynamicAoiLodEngine.PacketPriority.P0_Critical, DynamicAoiLodEngine.ClassifyPacket(ProtocolConsts.SM_DEATH_ACTION_DELPHI));
        Assert.Equal(DynamicAoiLodEngine.PacketPriority.P0_Critical, DynamicAoiLodEngine.ClassifyPacket(ProtocolConsts.SM_OBJECTSPELL));
        Assert.Equal(DynamicAoiLodEngine.PacketPriority.P0_Critical, DynamicAoiLodEngine.ClassifyPacket(ProtocolConsts.SM_OBJECTSTRUCK));
        Assert.Equal(DynamicAoiLodEngine.PacketPriority.P0_Critical, DynamicAoiLodEngine.ClassifyPacket(ProtocolConsts.SM_MYSTATUS));

        // P1 次要移动位移
        Assert.Equal(DynamicAoiLodEngine.PacketPriority.P1_Secondary, DynamicAoiLodEngine.ClassifyPacket(ProtocolConsts.SM_WALK_DELPHI));
        Assert.Equal(DynamicAoiLodEngine.PacketPriority.P1_Secondary, DynamicAoiLodEngine.ClassifyPacket(ProtocolConsts.SM_RUN_DELPHI));

        // P2 表现层微调 (转向、微粒)
        Assert.Equal(DynamicAoiLodEngine.PacketPriority.P2_Cosmetic, DynamicAoiLodEngine.ClassifyPacket(ProtocolConsts.SM_TURNDIRECTION));
        Assert.Equal(DynamicAoiLodEngine.PacketPriority.P2_Cosmetic, DynamicAoiLodEngine.ClassifyPacket(ProtocolConsts.SM_SPELLEFFECT_DELPHI));
    }

    [Fact]
    public void TestSabukPalace200Players_P0ZeroDrop_And_P2Decimation()
    {
        // 1. 构建 15x15 沙巴克皇宫地图与 200 个并发实体
        var palaceMap = new GameMap(2, "沙巴克皇宫", 15, 15);
        var mapMgr = new MapManager();
        mapMgr.RegisterMap(palaceMap);

        var dispatcher = new PacketDispatcher(mapMgr);
        var lod = dispatcher.AoiLodEngine;
        lod.MaxP2PacketsPerTickExtreme = 5;

        var sessions = new List<ClientSession>(200);
        for (int i = 0; i < 200; i++)
        {
            // 实体随机散布在 15x15 的皇宫空间内 (全部在同一个九宫格视野内)
            int x = 2 + (i % 11);
            int y = 2 + ((i / 11) % 11);
            var s = CreateTestSession(1000 + i, $"沙巴克勇士{i}", x, y);
            palaceMap.Aoi.Enter(s, x, y);
            dispatcher.RegisterSession(s);
            sessions.Add(s);
        }

        // 2. 模拟沙巴克激战：1 号玩家连续发出 50 次微小朝向偏转 (SM_TURNDIRECTION 3010)
        // 在 200 人同屏无 LOD 下，单次广播产生 200 包，50 次将产生 10,000 包！
        var actor = sessions[0];
        long currentTick = 100;
        dispatcher.UpdateWorldTick(currentTick, Environment.TickCount64);

        int targetSessionId = sessions[1].SessionId;
        int p2DeliveredToTarget = 0;
        for (int i = 0; i < 50; i++)
        {
            bool delivered = lod.ShouldDeliverPacket(
                DynamicAoiLodEngine.DensityLevel.Extreme,
                DynamicAoiLodEngine.PacketPriority.P2_Cosmetic,
                actor.SessionId,
                targetSessionId,
                p2DeliveredToTarget,
                currentTick
            );
            if (delivered)
            {
                p2DeliveredToTarget++;
            }
        }

        // 验证 P2 表现层封包被严格限制在 MaxP2PacketsPerTickExtreme (5 包) 以内，削峰率高达 90%
        Assert.Equal(5, p2DeliveredToTarget);
        Assert.True(lod.TotalFilteredP2Packets >= 45, "超额的 P2 表现层封包必须被削峰拦截");

        // 3. 关键验证：在极端密度下施放冰咆哮 (SM_OBJECTSPELL 243) 与受击掉血 (SM_STRUCK 244)
        // 必须 100% 保证 P0 核心逻辑 0 丢包！
        int p0DeliveredToTarget = 0;
        for (int i = 0; i < 50; i++)
        {
            bool delivered = lod.ShouldDeliverPacket(
                DynamicAoiLodEngine.DensityLevel.Extreme,
                DynamicAoiLodEngine.PacketPriority.P0_Critical,
                actor.SessionId,
                targetSessionId,
                p2DeliveredToTarget,
                currentTick
            );
            if (delivered)
            {
                p0DeliveredToTarget++;
            }
        }

        // P0 绝对无损放行
        Assert.Equal(50, p0DeliveredToTarget);
    }

    [Fact]
    public void TestSabukPalace200Players_ExtremeConcurrencyLoad_NoDeadlock()
    {
        // 验证 200 个实体在 15x15 皇宫小空间内多线程并发广播与伤害结算
        var palaceMap = new GameMap(2, "沙巴克皇宫", 15, 15);
        var mapMgr = new MapManager();
        mapMgr.RegisterMap(palaceMap);

        var dispatcher = new PacketDispatcher(mapMgr);
        var sessions = new List<ClientSession>(200);
        for (int i = 0; i < 200; i++)
        {
            int x = 2 + (i % 11);
            int y = 2 + ((i / 11) % 11);
            var s = new ClientSession(2000 + i)
            {
                CharacterName = $"沙战战士{i}",
                CurrentMapId = 2,
                X = x,
                Y = y,
                State = new PlayerState { Name = $"沙战战士{i}", Hp = 600, MaxHp = 600, Mp = 200, MaxMp = 200 }
            };
            palaceMap.Aoi.Enter(s, x, y);
            dispatcher.RegisterSession(s);
            sessions.Add(s);
        }

        var sw = Stopwatch.StartNew();

        // 模拟多线程同时向周围实体广播动作与施法 (高并发测试)
        Parallel.For(0, 200, new ParallelOptions { MaxDegreeOfParallelism = 8 }, i =>
        {
            var s = sessions[i];
            // 施法动作广播
            dispatcher.Dispatch(s, $"#3017/0/33/{s.X}/{s.Y}!"); // 冰咆哮
        });

        sw.Stop();

        // 200 人并发施法全量广播耗时必须在 1000ms 以内，无死锁、无异常
        Assert.True(sw.ElapsedMilliseconds < 1000, $"沙巴克皇宫 200 人并发广播耗时 ({sw.ElapsedMilliseconds}ms) 过长");
    }
}
