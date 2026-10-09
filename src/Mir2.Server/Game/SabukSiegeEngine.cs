using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 沙巴克攻城战与皇宫争夺权威引擎 (SabukSiegeEngine)
/// 严格 1:1 对齐官方 M2Server 物理反编译代码:
/// - 官方实证: IDA sub_4C92B0 / sub_4C93A0 (城门状态与碰撞) & sub_4C9B80 (皇宫占领判定)
/// 核心规则:
/// 1. 城门物理血量与破坏倒塌: 2000 HP, <= 1000 Damaged, <= 0 Broken (动态消除物理阻挡)
/// 2. 攻城战期免 PK 惩罚与红蓝名变色判定: 沙巴克城与皇宫区域交战不红名、不加 PK 点数
/// 3. 皇宫台阶判定、内部清人与占领权更迭: 守城方全部清空且仅存某攻击方行会成员时占领
/// 4. 攻城战结束正式易主并全服下发公告
/// </summary>
public sealed class SabukSiegeEngine
{
    public enum GateState
    {
        Intact = 0,   // 完好无损 (阻挡生效)
        Damaged = 1,  // 遭受重创 (裂开，阻挡依然生效)
        Broken = 2    // 彻底倒塌 (阻挡清除，可自由穿行)
    }

    public enum WarPhase
    {
        Peace = 0,    // 和平期
        InWar = 1,    // 攻城战役激烈交火中 (官方 20:00 ~ 22:00)
        Ended = 2     // 战役结算结束
    }

    public sealed class Point2D
    {
        public int X { get; init; }
        public int Y { get; init; }
        public Point2D(int x, int y) { X = x; Y = y; }
    }

    // ==================== 1. 沙巴克城门管理 ====================

    public int GateMaxHp { get; set; } = 2000;
    public int GateCurrentHp { get; private set; } = 2000;
    public GateState CurrentGateState { get; private set; } = GateState.Intact;

    // 官方沙巴克城门坐标网格 (盟重沙巴克正门 672, 331 及其两侧)
    public List<Point2D> GateTiles { get; } = new()
    {
        new Point2D(672, 331),
        new Point2D(672, 332),
        new Point2D(672, 333)
    };

    public event Action<GateState>? OnGateStateChanged;
    public event Action<int, int>? OnGateHpChanged;

    /// <summary>
    /// 初始化城门血量与阻挡瓦片
    /// </summary>
    public void InitializeGate(int maxHp, IEnumerable<Point2D>? customTiles = null)
    {
        GateMaxHp = maxHp;
        GateCurrentHp = maxHp;
        CurrentGateState = GateState.Intact;
        if (customTiles != null)
        {
            GateTiles.Clear();
            GateTiles.AddRange(customTiles);
        }
    }

    /// <summary>
    /// 攻击城门造成伤害，若倒塌则触发清除阻挡回调
    /// </summary>
    public GateState DamageGate(int damage, Action<int, int, bool>? setTileBlocked = null)
    {
        if (CurrentGateState == GateState.Broken) return GateState.Broken;

        GateCurrentHp = Math.Max(0, GateCurrentHp - damage);
        OnGateHpChanged?.Invoke(GateCurrentHp, GateMaxHp);

        if (GateCurrentHp <= 0)
        {
            CurrentGateState = GateState.Broken;
            OnGateStateChanged?.Invoke(CurrentGateState);

            // 城门倒塌：彻底清除物理阻挡
            if (setTileBlocked != null)
            {
                foreach (var tile in GateTiles)
                {
                    setTileBlocked(tile.X, tile.Y, false);
                }
            }
        }
        else if (GateCurrentHp <= GateMaxHp / 2 && CurrentGateState == GateState.Intact)
        {
            CurrentGateState = GateState.Damaged;
            OnGateStateChanged?.Invoke(CurrentGateState);
        }

        return CurrentGateState;
    }

    /// <summary>
    /// 沙巴克城主修复城门并恢复阻挡
    /// </summary>
    public void RepairGate(Action<int, int, bool>? setTileBlocked = null)
    {
        GateCurrentHp = GateMaxHp;
        CurrentGateState = GateState.Intact;
        OnGateStateChanged?.Invoke(CurrentGateState);
        OnGateHpChanged?.Invoke(GateCurrentHp, GateMaxHp);

        // 恢复物理阻挡
        if (setTileBlocked != null)
        {
            foreach (var tile in GateTiles)
            {
                setTileBlocked(tile.X, tile.Y, true);
            }
        }
    }

    // ==================== 2. 攻城战役与占领权流转 ====================

    public WarPhase Phase { get; private set; } = WarPhase.Peace;
    public string OwnerGuild { get; private set; } = string.Empty;
    public string TempOccupierGuild { get; private set; } = string.Empty; // 皇宫临时占领行会
    public long WarEndTimeMs { get; private set; }
    public const int SabukPalaceMapId = 150; // 沙巴克皇宫地图 ID (0150)
    public const int SabukCastleMapId = 3;   // 沙巴克城所在盟重地图 ID

    // 攻城战注册报名的行会列表
    private readonly HashSet<string> _attackingGuilds = new(StringComparer.OrdinalIgnoreCase);

    public void RegisterAttackingGuild(string guildName)
    {
        if (!string.IsNullOrWhiteSpace(guildName))
        {
            _attackingGuilds.Add(guildName);
        }
    }

    public bool IsAttackingGuild(string guildName) =>
        _attackingGuilds.Contains(guildName);

    /// <summary>
    /// 开启沙巴克攻城战 (官方持续 2 小时)
    /// </summary>
    public void StartSiege(string currentOwnerGuild, long nowMs, long durationMs = 7200_000)
    {
        OwnerGuild = currentOwnerGuild;
        TempOccupierGuild = currentOwnerGuild;
        Phase = WarPhase.InWar;
        WarEndTimeMs = nowMs + durationMs;
    }

    /// <summary>
    /// 判定是否处于沙巴克战火区域且双方属于参战行会 (免 PK 惩罚)
    /// </summary>
    public bool IsFreePkBattle(int mapId, string? attackerGuild, string? victimGuild)
    {
        if (Phase != WarPhase.InWar) return false;
        if (mapId != SabukCastleMapId && mapId != SabukPalaceMapId) return false;

        if (string.IsNullOrWhiteSpace(attackerGuild) || string.IsNullOrWhiteSpace(victimGuild))
        {
            return false;
        }

        bool attackerInWar = attackerGuild.Equals(OwnerGuild, StringComparison.OrdinalIgnoreCase) || _attackingGuilds.Contains(attackerGuild);
        bool victimInWar = victimGuild.Equals(OwnerGuild, StringComparison.OrdinalIgnoreCase) || _attackingGuilds.Contains(victimGuild);

        // 两方均为攻守城参战行会成员，且非同一行会，免 PK 惩罚
        return attackerInWar && victimInWar && !attackerGuild.Equals(victimGuild, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 皇宫人员实时清算与临时占领判定 (IDA sub_4C9B80)
    /// 规则:
    /// 1. 扫描皇宫内所有存活玩家；
    /// 2. 若存在守城方成员，攻击方无法夺取；
    /// 3. 若守城方全无，且内部仅剩唯一一个攻击方行会的成员，则该行会成为临时占领者！
    /// </summary>
    public (bool occupied, string newOccupier) CheckPalaceOccupation(
        IEnumerable<(string characterName, string guildName, bool isAlive)> palacePlayers)
    {
        if (Phase != WarPhase.InWar)
        {
            return (false, TempOccupierGuild);
        }

        bool hasCurrentDefender = false;
        string? dominantGuild = null;
        bool multipleAttackingGuildsPresent = false;

        foreach (var p in palacePlayers)
        {
            if (!p.isAlive || string.IsNullOrWhiteSpace(p.guildName)) continue;

            if (p.guildName.Equals(OwnerGuild, StringComparison.OrdinalIgnoreCase))
            {
                hasCurrentDefender = true;
                break; // 只要皇宫里还有守城方活人，就不可被占领
            }

            if (_attackingGuilds.Contains(p.guildName))
            {
                if (dominantGuild == null)
                {
                    dominantGuild = p.guildName;
                }
                else if (!dominantGuild.Equals(p.guildName, StringComparison.OrdinalIgnoreCase))
                {
                    // 皇宫内存在多个不同攻城行会混战，尚无单一主导者
                    multipleAttackingGuildsPresent = true;
                }
            }
        }

        if (!hasCurrentDefender && !multipleAttackingGuildsPresent && dominantGuild != null)
        {
            if (!dominantGuild.Equals(TempOccupierGuild, StringComparison.OrdinalIgnoreCase))
            {
                TempOccupierGuild = dominantGuild;
                return (true, dominantGuild);
            }
        }

        return (false, TempOccupierGuild);
    }

    /// <summary>
    /// 攻城战结算结束，正式宣布最终胜者
    /// </summary>
    public (string winnerGuild, bool changed) EndSiege()
    {
        Phase = WarPhase.Ended;
        bool changed = !string.IsNullOrWhiteSpace(TempOccupierGuild) &&
                       !TempOccupierGuild.Equals(OwnerGuild, StringComparison.OrdinalIgnoreCase);

        if (changed)
        {
            OwnerGuild = TempOccupierGuild;
        }

        return (OwnerGuild, changed);
    }
}
