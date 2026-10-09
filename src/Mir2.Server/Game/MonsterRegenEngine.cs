using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Mir2.Server.Maps;
using Mir2.Server.Network;
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

    // 官方怪物 AI 属性 (IDA 0x4B966C)
    public int ViewRange { get; set; } = 5; // 官方默认视野 5 格 (0x4B966C @ +484)
    public int AttackDamageMin { get; set; } = 6;
    public int AttackDamageMax { get; set; } = 14;
    public long NextActionTimeMs { get; set; }
    public int TargetSessionId { get; set; }
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

    /// <summary>
    /// 根据地图与坐标检索处于该位置的存活怪物
    /// </summary>
    public MonsterInstance? FindMonsterAt(int mapId, int x, int y)
    {
        foreach (var kvp in _monsters)
        {
            var m = kvp.Value;
            if (!m.IsDead && m.MapId == mapId && m.X == x && m.Y == y)
            {
                return m;
            }
        }
        return null;
    }

    public readonly record struct MonsterAiActionResult(
        int MonsterUid,
        string MonsterName,
        int ActionType, // 0=追击移动, 1=近战攻击, 2=闲置巡逻
        int X,
        int Y,
        byte Direction,
        int TargetSessionId,
        int DamageDealt
    );

    /// <summary>
    /// 执行全服怪物 AI 核心心跳 (IDA 0x4B1638 ProcessMonsters & 0x4B966C)
    /// 包含视野索敌、追击移动、邻近攻击判定与 15ms 时间片硬熔断保护
    /// </summary>
    public (List<MonsterAiActionResult> actions, bool sliceTimeout) ProcessMonsterAiTick(
        Func<int, int, int, List<ClientSession>> getSessionsInNinePatch,
        OfficialClientRulesEngine.DelphiRandom rng)
    {
        long startTick = Environment.TickCount64;
        var actions = new List<MonsterAiActionResult>();
        bool sliceTimeout = false;

        foreach (var kvp in _monsters)
        {
            var monster = kvp.Value;
            if (monster.IsDead) continue;

            long now = Environment.TickCount64;
            if (now < monster.NextActionTimeMs) continue;

            var map = _mapManager.GetMap(monster.MapId);
            if (map == null) continue;

            // 1. 视野索敌 (扫描周围九宫格内的玩家, 默认 5 格视野)
            var nearbyPlayers = getSessionsInNinePatch(monster.MapId, monster.X, monster.Y);
            ClientSession? targetPlayer = null;
            int closestDist = int.MaxValue;

            for (int i = 0; i < nearbyPlayers.Count; i++)
            {
                var p = nearbyPlayers[i];
                if (p.State == null || p.State.Hp <= 0) continue;

                int dx = Math.Abs(p.X - monster.X);
                int dy = Math.Abs(p.Y - monster.Y);
                int chebyshevDist = Math.Max(dx, dy);

                if (chebyshevDist <= monster.ViewRange && chebyshevDist < closestDist)
                {
                    closestDist = chebyshevDist;
                    targetPlayer = p;
                }
            }

            if (targetPlayer != null)
            {
                monster.TargetSessionId = targetPlayer.SessionId;
                int dx = targetPlayer.X - monster.X;
                int dy = targetPlayer.Y - monster.Y;
                int dist = Math.Max(Math.Abs(dx), Math.Abs(dy));

                if (dist == 1)
                {
                    // 2. 邻接物理近战攻击 (IDA 0x4B966C)
                    monster.Direction = GetDirection(dx, dy);
                    int dmg = rng.Next(monster.AttackDamageMax - monster.AttackDamageMin + 1) + monster.AttackDamageMin;

                    // 结算目标玩家受击扣血
                    targetPlayer.State!.Hp = Math.Max(0, targetPlayer.State.Hp - dmg);

                    monster.NextActionTimeMs = now + 1000; // 攻击后摇 1000ms
                    actions.Add(new MonsterAiActionResult(
                        monster.Uid, monster.Name, ActionType: 1,
                        monster.X, monster.Y, monster.Direction,
                        targetPlayer.SessionId, DamageDealt: dmg
                    ));
                }
                else if (dist > 1)
                {
                    // 3. 追击步进 (朝目标方向移动 1 格)
                    int stepX = monster.X + Math.Sign(dx);
                    int stepY = monster.Y + Math.Sign(dy);

                    if (map.IsWalkable(stepX, stepY))
                    {
                        monster.Direction = GetDirection(Math.Sign(dx), Math.Sign(dy));
                        monster.X = stepX;
                        monster.Y = stepY;
                        monster.NextActionTimeMs = now + 800; // 移动冷却 800ms
                        actions.Add(new MonsterAiActionResult(
                            monster.Uid, monster.Name, ActionType: 0,
                            monster.X, monster.Y, monster.Direction,
                            targetPlayer.SessionId, DamageDealt: 0
                        ));
                    }
                }
            }
            else
            {
                monster.TargetSessionId = 0;
                // 4. 闲置巡逻 (20% 几率随机朝 8 方向漫步一格)
                if (rng.Next(5) == 0)
                {
                    int randDir = rng.Next(8);
                    var (offX, offY) = GetDirOffset((byte)randDir);
                    int roamX = monster.X + offX;
                    int roamY = monster.Y + offY;

                    // 保证漫步不超过刷新半径
                    var rule = _rules.Find(r => r.RuleId == monster.SpawnRuleId);
                    bool withinRadius = rule == null || (Math.Abs(roamX - rule.CenterX) <= rule.Radius && Math.Abs(roamY - rule.CenterY) <= rule.Radius);

                    if (withinRadius && map.IsWalkable(roamX, roamY))
                    {
                        monster.X = roamX;
                        monster.Y = roamY;
                        monster.Direction = (byte)randDir;
                        monster.NextActionTimeMs = now + 1200;
                        actions.Add(new MonsterAiActionResult(
                            monster.Uid, monster.Name, ActionType: 2,
                            monster.X, monster.Y, monster.Direction,
                            TargetSessionId: 0, DamageDealt: 0
                        ));
                    }
                }
            }

            // 时间片熔断检测 (IDA 0x4B1638)
            if (Environment.TickCount64 - startTick > MaxRegenTimeSliceMs)
            {
                sliceTimeout = true;
                break;
            }
        }

        return (actions, sliceTimeout);
    }

    /// <summary>
    /// 盛大官方 1.5 8方向计算函数 (0=上, 1=右上, 2=右, 3=右下, 4=下, 5=左下, 6=左, 7=左上)
    /// </summary>
    public static byte GetDirection(int dx, int dy)
    {
        int sx = Math.Sign(dx);
        int sy = Math.Sign(dy);
        return (sx, sy) switch
        {
            (0, -1) => 0,
            (1, -1) => 1,
            (1, 0) => 2,
            (1, 1) => 3,
            (0, 1) => 4,
            (-1, 1) => 5,
            (-1, 0) => 6,
            (-1, -1) => 7,
            _ => 0
        };
    }

    public static (int dx, int dy) GetDirOffset(byte dir) => dir switch
    {
        0 => (0, -1),
        1 => (1, -1),
        2 => (1, 0),
        3 => (1, 1),
        4 => (0, 1),
        5 => (-1, 1),
        6 => (-1, 0),
        7 => (-1, -1),
        _ => (0, 0)
    };
}
