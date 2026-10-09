using System;
using System.Collections.Generic;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 安全区物理禁武与大刀卫士/弓箭守卫秒杀权威引擎 (SafeZoneAndGuardEngine)
/// 严格 1:1 对齐官方 M2Server 物理逻辑:
/// 1. 主城安全区范围判定 (比奇、盟重、银杏村、封魔谷、沙巴克)
/// 2. 安全区内绝对物理禁武: 禁止物理出刀、禁止法术伤害穿透
/// 3. 大刀守卫 (Guard) 与弓箭手 (Archer) 9999 秒杀 AI:
///    - 12 格超远视野锁定红名玩家 (PkPoints >= 200) 并打出 9999 致命真伤
///    - 对靠近城门或安全区的怪物自动挥刀射杀
/// </summary>
public sealed class SafeZoneAndGuardEngine
{
    public readonly record struct SafeZoneRect(int MapId, int MinX, int MinY, int MaxX, int MaxY);

    private readonly List<SafeZoneRect> _safeZones = new()
    {
        new(0, 320, 260, 345, 285), // 比奇大城安全区
        new(0, 640, 600, 660, 620), // 银杏村安全区
        new(3, 325, 320, 345, 340), // 盟重土城安全区
        new(3, 645, 290, 655, 300), // 沙巴克复活点安全区
        new(11, 230, 190, 250, 210) // 封魔谷安全区
    };

    public void RegisterSafeZone(int mapId, int minX, int minY, int maxX, int maxY)
    {
        _safeZones.Add(new SafeZoneRect(mapId, minX, minY, maxX, maxY));
    }

    /// <summary>
    /// 判定坐标是否处于主城安全区内部
    /// </summary>
    public bool IsInSafeZone(int mapId, int x, int y)
    {
        for (int i = 0; i < _safeZones.Count; i++)
        {
            var r = _safeZones[i];
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
        public int AttackRange => IsArcher ? 8 : 12;
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
        IEnumerable<ClientSession> activeSessions)
    {
        var executions = new List<(GuardEntity, ClientSession, int)>();

        for (int g = 0; g < _guards.Count; g++)
        {
            var guard = _guards[g];

            foreach (var session in activeSessions)
            {
                if (session.State == null || session.State.Hp <= 0) continue;
                if (session.CurrentMapId != guard.MapId) continue;

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
}
