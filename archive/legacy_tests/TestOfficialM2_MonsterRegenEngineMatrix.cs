using System;
using Mir2.Server.Game;
using Mir2.Server.Maps;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests;

public class TestOfficialM2_MonsterRegenEngineMatrix
{
    [Fact]
    public void TestOfficialM2_RegenMonsters_Coordinates_Within_Radius_And_Walkable()
    {
        var mapManager = new MapManager();
        var biqi = mapManager.GetMap(0);
        Assert.NotNull(biqi);

        // 在比奇省 (100, 100) 到 (120, 120) 设置大面积水面障碍阻挡
        biqi.SetBlockRect(100, 100, 20, 20, blocked: true);

        var engine = new MonsterRegenEngine(mapManager);
        // 规则: 比奇省沃玛森林入口 (110, 110) 区域刷新半兽人，半径 15，总数 30 只
        engine.AddRule(new MonsterSpawnRule
        {
            RuleId = 1,
            MonsterName = "半兽人",
            MapId = 0,
            CenterX = 110,
            CenterY = 110,
            Radius = 15,
            MaxCount = 30,
            BaseHp = 160
        });

        var rng = new OfficialClientRulesEngine.DelphiRandom(42);
        var (spawned, _) = engine.ProcessRegenTick(rng, enforceTimeSlice: false);

        Assert.Equal(30, spawned);
        Assert.Equal(30, engine.TotalActiveMonsters);

        // 验证每只怪物的坐标必须在半径范围内，且绝对不在阻挡位图内 (IDA 0x4ADF04)
        for (int uid = 100_001; uid <= 100_030; uid++)
        {
            var monster = engine.GetMonster(uid);
            Assert.NotNull(monster);
            Assert.Equal("半兽人", monster.Name);

            // 半径约束
            Assert.InRange(monster.X, 110 - 15, 110 + 15);
            Assert.InRange(monster.Y, 110 - 15, 110 + 15);

            // 阻挡约束 (绝对不可刷在 100..119 的阻挡水面内)
            Assert.True(biqi.IsWalkable(monster.X, monster.Y), $"坐标 ({monster.X}, {monster.Y}) 必须可通行");
        }
    }

    [Fact]
    public void TestOfficialM2_RegenMonsters_Quota_And_Death_Recycle()
    {
        var mapManager = new MapManager();
        var engine = new MonsterRegenEngine(mapManager);

        engine.AddRule(new MonsterSpawnRule
        {
            RuleId = 2,
            MonsterName = "骷髅战士",
            MapId = 0,
            CenterX = 200,
            CenterY = 200,
            Radius = 10,
            MaxCount = 5,
            BaseHp = 100
        });

        var rng = new OfficialClientRulesEngine.DelphiRandom(100);
        // 1. 初次刷新 5 只
        var (s1, _) = engine.ProcessRegenTick(rng);
        Assert.Equal(5, s1);
        Assert.Equal(5, engine.TotalActiveMonsters);

        // 再次心跳，配额已满，不会多刷一只
        var (s2, _) = engine.ProcessRegenTick(rng);
        Assert.Equal(0, s2);
        Assert.Equal(5, engine.TotalActiveMonsters);

        // 2. 模拟玩家击杀 2 只骷髅 (假设 uid 100001 和 100002)
        var d1 = engine.OnMonsterDied(100_001);
        var d2 = engine.OnMonsterDied(100_002);
        Assert.NotNull(d1);
        Assert.NotNull(d2);
        Assert.Equal(3, engine.TotalActiveMonsters);

        // 3. 下一轮心跳，精准补齐差额 2 只
        var (s3, _) = engine.ProcessRegenTick(rng);
        Assert.Equal(2, s3);
        Assert.Equal(5, engine.TotalActiveMonsters);
    }

    [Fact]
    public void TestOfficialM2_RegenMonsters_15ms_TimeSlice_Meltdown_Protection()
    {
        var mapManager = new MapManager();
        var engine = new MonsterRegenEngine(mapManager);

        // 注册 500 个密集刷怪规则，总计需要刷 500,000 只怪物
        for (int i = 0; i < 500; i++)
        {
            engine.AddRule(new MonsterSpawnRule
            {
                RuleId = 1000 + i,
                MonsterName = $"祖玛卫士_{i}",
                MapId = 0,
                CenterX = 200,
                CenterY = 200,
                Radius = 50,
                MaxCount = 1000
            });
        }

        var rng = new OfficialClientRulesEngine.DelphiRandom(777);

        // 执行单次心跳刷新
        long tickStart = Environment.TickCount64;
        var (spawned, sliceTimeout) = engine.ProcessRegenTick(rng);
        long elapsed = Environment.TickCount64 - tickStart;

        // 验证官方 15ms 时间片切片硬熔断保护机制 (IDA 0x4ADF04 @ L470)
        Assert.True(sliceTimeout, "海量刷怪请求必须触发 15ms 官方时间片硬熔断，防止帧率骤降卡顿");
        Assert.True(spawned > 0, "熔断前已成功调度生成了一批怪物");
        Assert.True(elapsed <= 100, $"单次刷怪耗时 ({elapsed}ms) 受到严格限制，绝不允许主循环长时间卡死");
    }
}
