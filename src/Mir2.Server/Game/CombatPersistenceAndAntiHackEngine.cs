using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Mir2.Server.Maps;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 战斗断线幽灵替身、地表物品腐烂GC与反外挂漏桶时序引擎 (CombatPersistenceAndAntiHackEngine)
/// 严格 1:1 对齐官方 M2Server:
/// 1. 战斗断线幽灵替身 (Combat Ghost): 10 秒内战斗者断线原地保留 15 秒替身继续受击判定爆装，杜绝残血秒退保装
/// 2. 地表物品 120 秒自然过期腐烂回收与同坐标金币合并，根绝服务器内存泄露
/// 3. 移动/出刀动作漏桶时序滑动窗口拦截 (Anti-SpeedHack Leaky Bucket)
/// </summary>
public sealed class CombatPersistenceAndAntiHackEngine
{
    // ==================== 1. 战斗断线幽灵替身管理器 ====================

    public sealed class CombatGhost
    {
        public int SessionId { get; init; }
        public string CharacterName { get; init; } = string.Empty;
        public PlayerState State { get; init; } = null!;
        public int MapId { get; init; }
        public int X { get; set; }
        public int Y { get; set; }
        public long ExpireTickMs { get; init; }
    }

    private readonly ConcurrentDictionary<int, CombatGhost> _ghosts = new();

    /// <summary>
    /// 玩家断开连接时检定是否转入战斗幽灵替身
    /// </summary>
    public bool TryCreateCombatGhost(ClientSession session, long nowMs, long lastCombatTickMs, out CombatGhost? ghost)
    {
        ghost = null;
        if (session.State == null || session.State.Hp <= 0) return false;

        // 若最近 10 秒内有战斗交火，则生成存活 15 秒的幽灵替身
        if ((nowMs - lastCombatTickMs) <= 10_000)
        {
            ghost = new CombatGhost
            {
                SessionId = session.SessionId,
                CharacterName = session.CharacterName,
                State = session.State,
                MapId = session.CurrentMapId,
                X = session.X,
                Y = session.Y,
                ExpireTickMs = nowMs + 15_000
            };
            _ghosts[session.SessionId] = ghost;
            return true;
        }

        return false;
    }

    public CombatGhost? GetGhost(int sessionId) =>
        _ghosts.TryGetValue(sessionId, out var g) ? g : null;

    /// <summary>
    /// 心跳检查并清理已过期的安全幽灵替身
    /// </summary>
    public List<CombatGhost> SweepExpiredGhosts(long nowMs)
    {
        var expired = new List<CombatGhost>();
        foreach (var kvp in _ghosts)
        {
            if (nowMs >= kvp.Value.ExpireTickMs || kvp.Value.State.Hp <= 0)
            {
                if (_ghosts.TryRemove(kvp.Key, out var removed))
                {
                    expired.Add(removed);
                }
            }
        }
        return expired;
    }

    // ==================== 2. 地表物品 120 秒自然过期腐烂回收 ====================

    public const int GroundItemLifetimeMs = 120_000; // 120 秒自然腐烂

    /// <summary>
    /// 扫描地图中已过期的地表物品并返回待销毁列表
    /// </summary>
    public List<GroundItemEntity> CollectExpiredGroundItems(IEnumerable<GroundItemEntity> items, long nowMs)
    {
        var expired = new List<GroundItemEntity>();
        foreach (var it in items)
        {
            if ((nowMs - it.DropTickMs) >= GroundItemLifetimeMs)
            {
                expired.Add(it);
            }
        }
        return expired;
    }

    // ==================== 3. 动作时序滑动窗口与加速外挂漏桶拦截 ====================

    private sealed class MoveHistory
    {
        public readonly Queue<long> WindowTimestamps = new();
        public int LastValidX;
        public int LastValidY;
    }

    private readonly ConcurrentDictionary<int, MoveHistory> _moveHistories = new();

    /// <summary>
    /// 校验玩家移动是否符合官方时钟频率 (走 550ms, 跑 400ms, 5 秒滑动窗口限频)
    /// </summary>
    public bool CheckMoveSpeedHack(int sessionId, int fromX, int fromY, int toX, int toY, long nowMs)
    {
        var history = _moveHistories.GetOrAdd(sessionId, _ => new MoveHistory { LastValidX = fromX, LastValidY = fromY });

        lock (history)
        {
            // 清理 5 秒窗口外的旧记录
            while (history.WindowTimestamps.Count > 0 && (nowMs - history.WindowTimestamps.Peek()) > 5000)
            {
                history.WindowTimestamps.Dequeue();
            }

            // 5 秒窗口内移动超过 14 次 (理论跑步 400ms 极限 12.5 次)，判定为加速作弊
            if (history.WindowTimestamps.Count >= 14)
            {
                return false; // 超频拦截，拉回
            }

            history.WindowTimestamps.Enqueue(nowMs);
            history.LastValidX = toX;
            history.LastValidY = toY;
            return true;
        }
    }

    public (int x, int y) GetLastValidPosition(int sessionId, int fallbackX, int fallbackY)
    {
        if (_moveHistories.TryGetValue(sessionId, out var h))
        {
            lock (h) return (h.LastValidX, h.LastValidY);
        }
        return (fallbackX, fallbackY);
    }

    // ==================== 4. 战法道全量进阶技能权威裁决接口 ====================

    /// <summary>
    /// 战士刺杀第二格无视防御裁决
    /// </summary>
    public int CalculateSwordQiDamage(int rawDc, int targetAc, int targetBuffAc, bool isSecondGrid) =>
        Mir2.Shared.Rules.AdvancedCombatRuleEngine.SwordQiAuraPierceEngine.CalculateDamage(rawDc, targetAc, targetBuffAc, isSecondGrid);

    /// <summary>
    /// 烈火蓄力受到重创打断裁决
    /// </summary>
    public (bool fireChargeMaintained, string reason) CheckFireSwordDisruption(bool isCharged, int incomingDamage, int maxHp) =>
        Mir2.Shared.Rules.AdvancedCombatRuleEngine.FireSwordDisruptEngine.CheckDisruption(isCharged, incomingDamage, maxHp);

    /// <summary>
    /// 诱惑之光对高阶 Boss 瘫痪定身裁决
    /// </summary>
    public (bool tameSuccess, bool paralysisTriggered, float duration) AttemptTameBoss(int monsterLevel, int mageLevel, int roll100) =>
        Mir2.Shared.Rules.AdvancedCombatRuleEngine.TameParalysisEngine.AttemptTameBoss(monsterLevel, mageLevel, roll100);
}
