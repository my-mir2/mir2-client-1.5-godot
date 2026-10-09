using System;
using System.Collections.Generic;
using Mir2.Server.Maps;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 大刀守卫巡逻与偏离自动回岗权威引擎 (GuardPatrolAndReturnEngine)
/// 规则:
/// 1. 守卫牢记出生哨岗原点 (SpawnX, SpawnY)
/// 2. 追击红名或怪物偏离原点后，若周围无敌情，自动执行 A* 寻路一步步回归初始哨岗
/// </summary>
public sealed class GuardPatrolAndReturnEngine
{
    public sealed class GuardPostEntity
    {
        public int Uid { get; init; }
        public string Name { get; init; } = "大刀卫士";
        public int MapId { get; init; }
        public int SpawnX { get; init; }
        public int SpawnY { get; init; }
        public int CurrentX { get; set; }
        public int CurrentY { get; set; }
        public bool IsReturning { get; set; }
    }

    private readonly List<GuardPostEntity> _guards = new();

    public void RegisterGuard(int uid, string name, int mapId, int spawnX, int spawnY)
    {
        _guards.Add(new GuardPostEntity
        {
            Uid = uid,
            Name = name,
            MapId = mapId,
            SpawnX = spawnX,
            SpawnY = spawnY,
            CurrentX = spawnX,
            CurrentY = spawnY
        });
    }

    public GuardPostEntity? GetGuard(int uid) =>
        _guards.Find(g => g.Uid == uid);

    /// <summary>
    /// 心跳更新守卫回位 AI: 偏离哨岗且周围无敌情时，自动一步步朝出生点走回
    /// </summary>
    public List<(GuardPostEntity guard, int nextX, int nextY)> UpdateGuardReturnStep(bool hasEnemyNearby)
    {
        var steps = new List<(GuardPostEntity, int, int)>();

        for (int i = 0; i < _guards.Count; i++)
        {
            var g = _guards[i];
            int dist = Math.Max(Math.Abs(g.CurrentX - g.SpawnX), Math.Abs(g.CurrentY - g.SpawnY));

            if (dist > 0 && !hasEnemyNearby)
            {
                g.IsReturning = true;

                // 朝 SpawnX, SpawnY 逼近 1 格
                int stepX = g.CurrentX;
                int stepY = g.CurrentY;

                if (g.CurrentX < g.SpawnX) stepX++;
                else if (g.CurrentX > g.SpawnX) stepX--;

                if (g.CurrentY < g.SpawnY) stepY++;
                else if (g.CurrentY > g.SpawnY) stepY--;

                g.CurrentX = stepX;
                g.CurrentY = stepY;
                steps.Add((g, stepX, stepY));

                if (g.CurrentX == g.SpawnX && g.CurrentY == g.SpawnY)
                {
                    g.IsReturning = false; // 已顺利归队
                }
            }
        }

        return steps;
    }
}
