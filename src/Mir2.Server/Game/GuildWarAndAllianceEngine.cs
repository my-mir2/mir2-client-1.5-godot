using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Mir2.Shared.Models;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 行会宣战战争与行会同盟权威引擎 (GuildWarAndAllianceEngine)
/// 严格 1:1 对齐官方 M2Server 规则:
/// 1. 行会宣战 (Guild War):
///    - 掌门人向目标行会宣战，战争持续 3 小时 (10800 秒)
///    - 战争期间双方行会成员互为战争敌对状态 (InWarRelation = true)
///    - 战争敌对双方在任意地图击杀均属于自由合法 PK: 绝对不增加 PK 点数、不红名、不受大刀卫士秒杀
/// 2. 行会同盟 (Guild Alliance):
///    - 双方掌门达成结盟，同盟行会成员在开启行会攻击模式时自动免疫友伤
/// 3. 行会掌门人广播指令 (!~ 行会频道全员广播)
/// </summary>
public sealed class GuildWarAndAllianceEngine
{
    public sealed class GuildWarInfo
    {
        public string GuildA { get; init; } = string.Empty;
        public string GuildB { get; init; } = string.Empty;
        public long StartTimeMs { get; init; }
        public long ExpireTimeMs { get; init; } // 默认 3 小时 (3 * 3600 * 1000 ms)
        public bool IsExpired(long nowMs) => nowMs >= ExpireTimeMs;
    }

    private readonly List<GuildWarInfo> _activeWars = new();
    private readonly HashSet<string> _alliances = new(StringComparer.OrdinalIgnoreCase);
    private readonly object _lock = new();

    private static string AllianceKey(string guildA, string guildB)
    {
        return string.Compare(guildA, guildB, StringComparison.OrdinalIgnoreCase) < 0
            ? $"{guildA}:{guildB}"
            : $"{guildB}:{guildA}";
    }

    /// <summary>
    /// 掌门人发起行会宣战 (持续 3 小时)
    /// </summary>
    public (bool success, string message) DeclareWar(string guildA, string guildB, long nowMs, long durationMs = 3 * 3600 * 1000)
    {
        if (string.IsNullOrWhiteSpace(guildA) || string.IsNullOrWhiteSpace(guildB))
        {
            return (false, "行会名称无效。");
        }
        if (string.Equals(guildA, guildB, StringComparison.OrdinalIgnoreCase))
        {
            return (false, "无法向本行会宣战！");
        }

        lock (_lock)
        {
            // 清理已过期战争
            _activeWars.RemoveAll(w => w.IsExpired(nowMs));

            // 检查是否已处于战争中
            bool alreadyInWar = _activeWars.Exists(w =>
                (string.Equals(w.GuildA, guildA, StringComparison.OrdinalIgnoreCase) && string.Equals(w.GuildB, guildB, StringComparison.OrdinalIgnoreCase)) ||
                (string.Equals(w.GuildA, guildB, StringComparison.OrdinalIgnoreCase) && string.Equals(w.GuildB, guildA, StringComparison.OrdinalIgnoreCase)));

            if (alreadyInWar)
            {
                return (false, $"【{guildA}】与【{guildB}】当前正处于激烈交战中！");
            }

            // 宣战自动瓦解同盟
            _alliances.Remove(AllianceKey(guildA, guildB));

            _activeWars.Add(new GuildWarInfo
            {
                GuildA = guildA,
                GuildB = guildB,
                StartTimeMs = nowMs,
                ExpireTimeMs = nowMs + durationMs
            });

            return (true, $"【战火燃起】行会【{guildA}】正式向【{guildB}】宣战！战争持续 3 小时，双方成员击杀互不计 PK 点数！");
        }
    }

    /// <summary>
    /// 建立行会同盟
    /// </summary>
    public (bool success, string message) FormAlliance(string guildA, string guildB)
    {
        if (string.IsNullOrWhiteSpace(guildA) || string.IsNullOrWhiteSpace(guildB) ||
            string.Equals(guildA, guildB, StringComparison.OrdinalIgnoreCase))
        {
            return (false, "结盟参数无效。");
        }

        lock (_lock)
        {
            string key = AllianceKey(guildA, guildB);
            if (_alliances.Contains(key))
            {
                return (false, "两行会早已结为永恒盟友！");
            }

            _alliances.Add(key);
            return (true, $"【金兰结义】行会【{guildA}】与【{guildB}】正式缔结同盟！共同进退！");
        }
    }

    /// <summary>
    /// 判定两行会是否处于同盟状态
    /// </summary>
    public bool AreGuildsAllied(string? guildA, string? guildB)
    {
        if (string.IsNullOrEmpty(guildA) || string.IsNullOrEmpty(guildB)) return false;
        lock (_lock)
        {
            return _alliances.Contains(AllianceKey(guildA, guildB));
        }
    }

    /// <summary>
    /// 判定两行会是否处于开战状态
    /// </summary>
    public bool AreGuildsAtWar(string? guildA, string? guildB, long nowMs)
    {
        if (string.IsNullOrEmpty(guildA) || string.IsNullOrEmpty(guildB)) return false;
        lock (_lock)
        {
            for (int i = 0; i < _activeWars.Count; i++)
            {
                var w = _activeWars[i];
                if (w.IsExpired(nowMs)) continue;

                if ((string.Equals(w.GuildA, guildA, StringComparison.OrdinalIgnoreCase) && string.Equals(w.GuildB, guildB, StringComparison.OrdinalIgnoreCase)) ||
                    (string.Equals(w.GuildA, guildB, StringComparison.OrdinalIgnoreCase) && string.Equals(w.GuildB, guildA, StringComparison.OrdinalIgnoreCase)))
                {
                    return true;
                }
            }
            return false;
        }
    }

    /// <summary>
    /// 战争期间合法击杀判定: 若双方处于行会战，则绝对免除杀人红名与 PK 点数惩罚
    /// </summary>
    public bool IsKillExemptFromPkPenalty(string? killerGuild, string? victimGuild, long nowMs)
    {
        return AreGuildsAtWar(killerGuild, victimGuild, nowMs);
    }
}
