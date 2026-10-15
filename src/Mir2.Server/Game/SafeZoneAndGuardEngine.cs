using System;
using System.Collections.Generic;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 安全区物理禁武与大刀卫士/弓箭守卫秒杀权威引擎 (SafeZoneAndGuardEngine)
/// 严格 1:1 对齐官方 M2Server 物理逻辑:
/// 1. 主城安全区范围判定 (比奇、盟重、银杏村、沙巴克)
/// 2. 安全区内绝对物理禁武: 禁止物理出刀、禁止法术伤害穿透
/// 3. 大刀守卫 (Guard) 与弓箭手 (Archer) 9999 秒杀 AI:
///    - 12 格超远视野锁定红名玩家 (PkPoints >= 200) 并打出 9999 致命真伤
///    - 对靠近城门或安全区的怪物自动挥刀射杀
/// </summary>
public sealed class SafeZoneAndGuardEngine
{
    public readonly record struct SafeZoneRect(int MapId, int MinX, int MinY, int MaxX, int MaxY, bool IsSabuk = false);

    public SabukSiegeEngine? SabukEngine { get; set; }

    private readonly HashSet<int> _safeMapIds = new();

    /// <summary>
    /// 用官方规则整体替换安全区 (M2Server sub_4BEE20 实证): 地图带 SAFE 标记即整图安全；
    /// 其余为 StartPoint.txt 每个出生点 ±10 格的方形区域 (|dx|&lt;=10 且 |dy|&lt;=10)。
    /// 构造函数里的矩形仅为单元测试夹具，生产启动经本方法覆盖。
    /// </summary>
    public void ReplaceWithOfficialSafeZones(IEnumerable<int> safeMapIds, IEnumerable<SafeZoneRect> rects)
    {
        _safeMapIds.Clear();
        foreach (var m in safeMapIds) _safeMapIds.Add(m);
        _safeZones.Clear();
        _safeZones.AddRange(rects);
    }

    private readonly List<SafeZoneRect> _safeZones = new()
    {
        new(0, 320, 260, 345, 285), // 比奇大城安全区
        new(0, 640, 600, 660, 620), // 银杏村安全区
        new(3, 325, 320, 345, 340), // 盟重土城安全区
        new(3, 645, 290, 655, 300, IsSabuk: true) // 沙巴克复活点安全区 (攻城战期间解除)
    };

    public void RegisterSafeZone(int mapId, int minX, int minY, int maxX, int maxY, bool isSabuk = false)
    {
        _safeZones.Add(new SafeZoneRect(mapId, minX, minY, maxX, maxY, isSabuk));
    }

    /// <summary>
    /// 判定坐标是否处于主城安全区内部 (攻城战役打响时沙城区域安全区自动解除)
    /// </summary>
    public bool IsInSafeZone(int mapId, int x, int y)
    {
        if (_safeMapIds.Contains(mapId)) return true;
        bool isSabukInWar = SabukEngine != null && SabukEngine.Phase == SabukSiegeEngine.WarPhase.InWar;

        for (int i = 0; i < _safeZones.Count; i++)
        {
            var r = _safeZones[i];
            // 盛大官方 1.5 铁律: 沙巴克攻城战役打响时，沙城安全区禁武全面解除，全城为自由交火区
            if (r.IsSabuk && isSabukInWar)
            {
                continue;
            }

            if (r.MapId == mapId && x >= r.MinX && x <= r.MaxX && y >= r.MinY && y <= r.MaxY)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 判定攻击是否被安全区禁武规则拦截 (攻击方或受击方任一处于安全区内均被严格禁止)
    /// </summary>
    public bool IsAttackForbiddenBySafeZone(int attackerMapId, int attackerX, int attackerY, int targetX, int targetY)
    {
        return IsInSafeZone(attackerMapId, attackerX, attackerY) ||
               IsInSafeZone(attackerMapId, targetX, targetY);
    }

    // ==================== 守卫 9999 秒杀 AI ====================

    public sealed class GuardEntity
    {
        public int Uid { get; init; }
        public string Name { get; init; } = "大刀卫士";
        public int MapId { get; init; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsArcher { get; init; }
        // 盛大官方 1.5 权威射程: 弓箭守卫与大刀卫士全屏 12 格远程刀光/射箭秒杀 (索敌即斩)
        public int AttackRange => SightRange;
        public int SightRange => 12; // 守卫索敌视野 12 格
    }

    private readonly List<GuardEntity> _guards = new();

    public void SpawnGuard(int uid, string name, int mapId, int x, int y, bool isArcher = false)
    {
        _guards.Add(new GuardEntity
        {
            Uid = uid,
            Name = name,
            MapId = mapId,
            X = x,
            Y = y,
            IsArcher = isArcher
        });
    }

    /// <summary>
    /// 大刀卫士/弓箭手心跳扫描: 自动锁定并处决红名玩家与怪物
    /// </summary>
    public List<(GuardEntity guard, ClientSession target, int damage)> ScanAndExecuteRedNames(
        IEnumerable<ClientSession> activeSessions, long nowMs = 0)
    {
        if (nowMs <= 0) nowMs = Environment.TickCount64;
        var executions = new List<(GuardEntity, ClientSession, int)>();

        for (int g = 0; g < _guards.Count; g++)
        {
            var guard = _guards[g];

            foreach (var session in activeSessions)
            {
                if (session.State == null || session.State.Hp <= 0) continue;
                if (session.CurrentMapId != guard.MapId) continue;
                if (session.State.IsInvincible(nowMs)) continue;

                // 仅处决红名玩家 (PkPoints >= 200)
                if (session.State.PkPoints < 200) continue;

                int dx = Math.Abs(session.X - guard.X);
                int dy = Math.Abs(session.Y - guard.Y);
                int dist = Math.Max(dx, dy);

                if (dist <= guard.AttackRange)
                {
                    // 官方守卫秒杀 9999 物理伤害 (IDA 0x4B36D4)
                    int dmg = 9999;
                    session.State.Hp = Math.Max(0, session.State.Hp - dmg);
                    executions.Add((guard, session, dmg));
                    break; // 单次 Tick 一个守卫专注处决一个目标
                }
            }
        }

        return executions;
    }

    /// <summary>
    /// 大刀卫士/弓箭手心跳扫描: 自动锁定并处决靠近城门与城区的敌对野生怪物与大叛变失控随从 (守序玩家随从严格豁免)
    /// Delphi 官方 1.5 物理真值: TGuardUnit.SearchTarget (IDA 0x4B36D4 / 0x4B3A80)
    /// </summary>
    public List<(GuardEntity guard, MonsterInstance target, int damage)> ScanAndExecuteMonsters(
        IEnumerable<MonsterInstance> activeMonsters, long nowMs = 0)
    {
        if (nowMs <= 0) nowMs = Environment.TickCount64;
        var executions = new List<(GuardEntity, MonsterInstance, int)>();

        for (int g = 0; g < _guards.Count; g++)
        {
            var guard = _guards[g];

            foreach (var monster in activeMonsters)
            {
                if (monster.IsDead || monster.Hp <= 0) continue;
                if (monster.MapId != guard.MapId) continue;

                // 盛大官方 1.5 铁律: 玩家当前驯服或召唤的合法宠物 (MasterSessionId > 0) 卫士严格豁免，不斩神兽、骷髅与虎卫
                if (monster.MasterSessionId > 0) continue;

                int dx = Math.Abs(monster.X - guard.X);
                int dy = Math.Abs(monster.Y - guard.Y);
                int dist = Math.Max(dx, dy);

                if (dist <= guard.AttackRange)
                {
                    // 官方守卫秒杀 9999 神圣伤害 (IDA 0x4B36D4)
                    int dmg = 9999;
                    monster.Hp = Math.Max(0, monster.Hp - dmg);
                    executions.Add((guard, monster, dmg));
                    break; // 单次 Tick 一个守卫专注处决一个目标
                }
            }
        }

        return executions;
    }
}
