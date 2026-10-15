using System;
using System.Collections.Generic;
using System.Linq;
using Mir2.Server.Game;
using Mir2.Server.Maps;
using Mir2.Server.Persistence;
using Mir2.Shared.Rules;
using Npgsql;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 数据库真实世界集成测试：不使用任何夹具或硬编码，直接读取 mir2_world 里由
/// tools/import_official_envir_to_pg.py 导入的官方地图/刷怪/掉落/NPC，走生产同一条装配路径 (OfficialWorldApplier)。
/// 需要本机 PostgreSQL (可用环境变量 MIR2_PG 覆盖连接串)；数据库不可用时测试失败而不是静默通过。
/// 注意: 本测试刻意不替换进程内静态的怪物属性表与掉落表，避免污染并行运行的其它测试。
/// </summary>
public sealed class TestOfficialWorldFromDatabase
{
    private static OfficialWorldRepository.WorldData LoadWorld(out MapManager maps, out MonsterRegenEngine regen,
        out SafeZoneAndGuardEngine safe)
    {
        string cs = Environment.GetEnvironmentVariable("MIR2_PG")
                    ?? "Host=127.0.0.1;Port=5432;Database=mir2_world;Username=postgres;Password=postgres";
        var repo = new OfficialWorldRepository(NpgsqlDataSource.Create(cs));
        Assert.True(repo.HasWorldData(), "数据库里没有官方世界数据，请先执行 python tools/import_official_envir_to_pg.py");
        var world = repo.Load();
        maps = new MapManager();
        regen = new MonsterRegenEngine(maps);
        safe = new SafeZoneAndGuardEngine();
        OfficialWorldApplier.Apply(world, maps, regen, new NpcMerchantEngine(), safe, null, applyDropTable: false);
        return world;
    }

    [Fact]
    public void TestWorld_MapsPassagesAndSafeZones_AreOfficialData()
    {
        var world = LoadWorld(out var maps, out _, out var safe);

        Assert.True(world.Maps.Count >= 400, $"官方地图应装载 400+ 张，实际 {world.Maps.Count}");
        var m0 = maps.GetMap(0)!;
        Assert.Equal(700, m0.Width);
        Assert.Equal(700, m0.Height);
        Assert.Equal("0", m0.OfficialId);

        // 夹具里虚构的 "盟重省 1000x800 / 沃玛森林 600x600" 必须已被官方数据覆盖
        Assert.Equal("3", maps.GetMap(3)!.OfficialId);
        Assert.DoesNotContain(maps.AllMaps, m => string.IsNullOrEmpty(m.OfficialId));

        // 每条传送门的落点必须是该地图内真实可走的格子 (落进墙里会把玩家卡死)
        Assert.True(world.Passages.Count > 1500, $"传送门应有 1500+ 条，实际 {world.Passages.Count}");
        foreach (var p in world.Passages)
        {
            var dst = maps.GetMap(p.ToMapId);
            Assert.NotNull(dst);
            Assert.True(dst!.IsWalkable(p.ToX, p.ToY), $"传送落点不可走: {p.Description} -> ({p.ToX},{p.ToY})");
        }

        // 安全区 = 出生点 (0,334,266) ±10 的方形 (IDA sub_4BEE20)，以及带 SAFE 标记的整张地图
        Assert.True(safe.IsInSafeZone(0, 334, 266));
        Assert.True(safe.IsInSafeZone(0, 344, 276));
        Assert.True(safe.IsInSafeZone(0, 324, 256));
        Assert.False(safe.IsInSafeZone(0, 345, 266));
        Assert.False(safe.IsInSafeZone(0, 334, 277));
        var safeMap = world.Maps.First(m => m.IsSafe);
        Assert.True(safe.IsInSafeZone(safeMap.MapId, 1, 1), $"SAFE 地图 {safeMap.OfficialId} 应整图安全");
        Assert.False(safe.IsInSafeZone(0, 600, 100), "夹具中虚构的安全区矩形不应残留");
    }

    [Fact]
    public void TestWorld_Spawns_UseOfficialStatsWalkableTilesAndLazyMapActivation()
    {
        var world = LoadWorld(out var maps, out var regen, out _);
        var rng = new OfficialClientRulesEngine.DelphiRandom(7);
        for (int i = 0; i < 5; i++) regen.ProcessRegenTick(rng, enforceTimeSlice: false);

        var m0 = maps.GetMap(0)!;
        var onMap0 = regen.AllActiveMonsters.Where(m => m.MapId == 0).ToList();
        Assert.True(onMap0.Count > 1000, $"地图 0 官方刷怪约 3000 只，实际刷出 {onMap0.Count}");
        foreach (var m in onMap0)
        {
            Assert.True(m0.IsWalkable(m.X, m.Y), $"{m.Name} 刷在不可走格 ({m.X},{m.Y})");
            Assert.False(string.IsNullOrEmpty(m.OfficialKey), $"{m.Name} 缺少官方键");
        }

        // 血量必须等于数据库 Monster.DB，且名称为汉化名而非英文键
        var henRule = world.Spawns.First(s => s.OfficialKey == "Hen" && s.MapNum == 0);
        var hen = onMap0.First(m => m.OfficialKey == "Hen");
        Assert.Equal(henRule.Hp, hen.MaxHp);
        Assert.Equal(henRule.NameCn, hen.Name);
        Assert.NotEqual("Hen", hen.Name);

        // 无玩家且非预热的地图不应被模拟 (官方全图约 3 万只怪，只模拟有人或预热地图)
        var startMaps = world.StartPoints.Select(s => s.MapNum).ToHashSet();
        var coldMap = world.Spawns.First(s => !startMaps.Contains(s.MapNum)).MapNum;
        Assert.DoesNotContain(regen.AllActiveMonsters, m => m.MapId == coldMap);
    }

    [Fact]
    public void TestWorld_RespawnInterval_DelaysRefillUntilOfficialTimeElapsed()
    {
        var world = LoadWorld(out _, out var regen, out _);
        var rng = new OfficialClientRulesEngine.DelphiRandom(11);
        for (int i = 0; i < 5; i++) regen.ProcessRegenTick(rng, enforceTimeSlice: false);

        // 取一条地图 0 上、有间隔且数量 >= 3 的官方规则
        var row = world.Spawns.First(s => s.MapNum == 0 && s.IntervalMin > 0 && s.Count >= 3);
        var rule = regen.FindRule(row.RowId)!;
        Assert.Equal(row.IntervalMin * 60_000L, rule.IntervalMs);
        Assert.True(rule.InitialFilled);

        int Alive() => regen.AllActiveMonsters.Count(m => m.SpawnRuleId == row.RowId && !m.IsDead);
        int before = Alive();
        var victim = regen.AllActiveMonsters.First(m => m.SpawnRuleId == row.RowId);
        regen.OnMonsterDied(victim.Uid);
        Assert.Equal(before - 1, Alive());

        // 间隔未到: 立即心跳不得补刷
        regen.ProcessRegenTick(rng, enforceTimeSlice: false);
        Assert.Equal(before - 1, Alive());

        // 官方间隔到达后 (模拟时间流逝) 才补刷
        rule.NextRefillMs = Environment.TickCount64 - 1;
        regen.ProcessRegenTick(rng, enforceTimeSlice: false);
        Assert.Equal(before, Alive());
    }

    [Fact]
    public void TestWorld_DropTables_AndNpcs_AreOfficialData()
    {
        var world = LoadWorld(out _, out _, out _);

        Assert.True(world.Drops.Count > 100, $"应有 100+ 种怪的官方掉落表，实际 {world.Drops.Count}");
        // 官方 Hen.txt 只有一行 "1/1 Chicken"：没有金币，物品名必须已换成 StdItems 的汉化名 (不再是英文 Chicken)
        Assert.True(world.Drops.TryGetValue("Hen", out var henDrops) && henDrops.Count == 1, "鸡的官方掉落表应恰为 1 行");
        Assert.Equal(1, henDrops[0].Numerator);
        Assert.Equal(1, henDrops[0].Denominator);
        Assert.False(henDrops[0].IsGold);
        Assert.NotEqual("Chicken", henDrops[0].ItemName);
        Assert.False(string.IsNullOrWhiteSpace(henDrops[0].ItemName));

        // 官方 Oma.txt 第 1 行 "1/4 gold 70"、第 2 行 "1/10000 gold 10000": 金币数量来自文件而不是硬编码
        Assert.True(world.Drops.TryGetValue("Oma", out var omaDrops) && omaDrops.Count >= 20, "半兽人应有 20 行官方掉落");
        Assert.True(omaDrops[0].IsGold);
        Assert.Equal(70, omaDrops[0].Count);
        Assert.Equal(4, omaDrops[0].Denominator);
        Assert.Equal(10000, omaDrops[1].Count);

        // 真实投掷: 3000 次击杀半兽人，金币 (1/4) 期望 ~750 次，必须落在合理区间，且所有掉落物名字非空
        var rng = new OfficialClientRulesEngine.DelphiRandom(3);
        int goldDrops = 0, total = 0;
        for (int i = 0; i < 3000; i++)
        {
            var drops = MonsterDropEngine.RollMonsterDrops(100, 100, 1, 0, omaDrops, rng);
            foreach (var d in drops)
            {
                Assert.False(string.IsNullOrWhiteSpace(d.Name));
                total++;
                if (d.IsGold && d.Count == 70) goldDrops++;
            }
        }
        Assert.InRange(goldDrops, 600, 900);
        Assert.True(total > goldDrops, "除金币外 3000 次击杀应至少掉出过一件装备/药品");

        // NPC: 官方摆放 + 货品价格取自 StdItems
        Assert.True(world.Npcs.Count > 300, $"NPC 应有 300+ 个，实际 {world.Npcs.Count}");
        var shop = world.Npcs.FirstOrDefault(n => n.Goods.Count > 0);
        Assert.NotNull(shop);
        Assert.All(shop!.Goods, g => { Assert.False(string.IsNullOrEmpty(g.Name)); Assert.True(g.Price > 0 || g.Weight > 0); });
    }
}
