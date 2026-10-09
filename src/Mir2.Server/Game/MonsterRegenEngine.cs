using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Mir2.Server.Maps;
using Mir2.Shared.Models;
using Mir2.Shared.Rules;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 怪物刷新规则配置模型
/// 对齐 IDA 0x4ADF04 (TUserEngine.RegenMonsters)
/// </summary>
public sealed class MonsterSpawnRule
{
    public int RuleId { get; init; }
    public string MonsterName { get; init; } = string.Empty;
    public int MapId { get; init; }
    public int CenterX { get; init; }
    public int CenterY { get; init; }
    public int Radius { get; init; }
    public int MaxCount { get; init; }
    public int ActiveCount { get; set; }
    public int BaseHp { get; init; } = 100;
}

/// <summary>
/// 服务端怪物运行时实体
/// </summary>
public sealed class MonsterInstance
{
    public int Uid { get; init; }
    public string Name { get; init; } = string.Empty;
    public int MapId { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public byte Direction { get; set; }
    public int Hp { get; set; }
    public int MaxHp { get; set; }
    public int SpawnRuleId { get; init; }
    public bool IsDead => Hp <= 0;
}

/// <summary>
/// 盛大官方 1.4/1.5 怪物刷新与时间片切片保护引擎 (MonsterRegenEngine)
/// 严格 1:1 对齐官方 M2Server 物理反编译代码:
/// - 刷新心跳与时间片熔断: TUserEngine.RegenMonsters @ 0x4ADF04 (MAX_TIME_SLICE = 15ms)
/// - 区域随机分布坐标公式: Center - Radius + RandInt(2 * Radius + 1)
/// </summary>
public sealed class MonsterRegenEngine
{
    // 官方 15 毫秒硬时间片保护 (防止成千上万只怪瞬发导致单帧 CPU 挂起)
    public const int MaxRegenTimeSliceMs = 15;

    private readonly MapManager _mapManager;
    private readonly List<MonsterSpawnRule> _rules = new();
    private readonly ConcurrentDictionary<int, MonsterInstance> _monsters = new();
    private int _nextMonsterUid = 100_000;

    public IReadOnlyList<MonsterSpawnRule> Rules => _rules;
    public int TotalActiveMonsters => _monsters.Count;

    public MonsterRegenEngine(MapManager mapManager)
    {
        _mapManager = mapManager;
    }

    /// <summary>
    /// 注册刷怪区域规则
    /// </summary>
    public void AddRule(MonsterSpawnRule rule)
    {
        lock (_rules)
        {
            _rules.Add(rule);
        }
    }

    /// <summary>
    /// 执行全服刷怪心跳处理 (包含官方 15ms 时间片硬熔断保护)
    /// 返回本次刷出的怪物数量及是否因耗时超限而触发熔断
    /// </summary>
    public (int spawnedCount, bool sliceTimeout) ProcessRegenTick(OfficialClientRulesEngine.DelphiRandom rng)
    {
        long startTick = Environment.TickCount64;
        int totalSpawned = 0;
        bool sliceTimeout = false;

        lock (_rules)
        {
            for (int r = 0; r < _rules.Count; r++)
            {
                var rule = _rules[r];
                var map = _mapManager.GetMap(rule.MapId);
                if (map == null) continue;

                int needSpawn = rule.MaxCount - rule.ActiveCount;
                while (needSpawn > 0)
                {
                    // 官方坐标随机公式 (0x4ADF04 @ L460~L466):
                    // targetX = CenterX - Radius + RandInt(2 * Radius + 1)
                    // targetY = CenterY - Radius + RandInt(2 * Radius + 1)
                    int span = 2 * rule.Radius + 1;
                    int targetX = rule.CenterX - rule.Radius + rng.Next(span);
                    int targetY = rule.CenterY - rule.Radius + rng.Next(span);

                    // 配合地图物理阻挡检测 (水面/墙壁/边界不可刷怪)
                    if (map.IsWalkable(targetX, targetY))
                    {
                        int uid = System.Threading.Interlocked.Increment(ref _nextMonsterUid);
                        var monster = new MonsterInstance
                        {
                            Uid = uid,
                            Name = rule.MonsterName,
                            MapId = rule.MapId,
                            X = targetX,
                            Y = targetY,
                            Direction = (byte)rng.Next(8),
                            Hp = rule.BaseHp,
                            MaxHp = rule.BaseHp,
                            SpawnRuleId = rule.RuleId
                        };

                        _monsters[uid] = monster;
                        rule.ActiveCount++;
                        needSpawn--;
                        totalSpawned++;
                    }

                    // 官方时间片硬熔断检测 (0x4ADF04 @ L470: GetTickCount() - TickCount > *off_4E9FD4)
                    if (Environment.TickCount64 - startTick > MaxRegenTimeSliceMs)
                    {
                        sliceTimeout = true;
                        return (totalSpawned, sliceTimeout);
                    }
                }
            }
        }

        return (totalSpawned, sliceTimeout);
    }

    /// <summary>
    /// 处理怪物死亡下线并释放刷新配额
    /// </summary>
    public MonsterInstance? OnMonsterDied(int monsterUid)
    {
        if (_monsters.TryRemove(monsterUid, out var monster))
        {
            lock (_rules)
            {
                var rule = _rules.Find(r => r.RuleId == monster.SpawnRuleId);
                if (rule != null && rule.ActiveCount > 0)
                {
                    rule.ActiveCount--;
                }
            }
            return monster;
        }
        return null;
    }

    public MonsterInstance? GetMonster(int monsterUid)
    {
        _monsters.TryGetValue(monsterUid, out var m);
        return m;
    }
}
