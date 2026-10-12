using System;
using System.Collections.Generic;

namespace Mir2.Shared.Rules;

/// <summary>
/// 盛大官方 1.4/1.5 沙巴克攻城战与皇宫争夺权威规则引擎 (SabukCastleRulesEngine)
/// 严格 1:1 对齐官方 M2Server: sub_4C92B0 / sub_4C93A0 (城门状态与碰撞) & sub_4C9B80 (皇宫占领判定)
/// </summary>
public static class SabukCastleRulesEngine
{
    public enum GateState
    {
        Intact = 0,   // 完好无损 (阻挡开启)
        Damaged = 1,  // 遭受重创 (裂开，阻挡依然开启)
        Broken = 2    // 彻底倒塌 (阻挡消除，可自由穿行)
    }

    public enum WarPhase
    {
        Peace = 0,    // 和平期
        InWar = 1,    // 攻城战役激烈交火中 (官方 2 小时)
        Ended = 2     // 战役结算结束
    }

    public const int DefaultGateMaxHp = 2000;

    /// <summary>
    /// 计算城门受到伤害后的状态与剩余生命
    /// </summary>
    public static (int newHp, GateState newState, bool becameBroken, bool becameDamaged) EvaluateGateDamage(
        int currentHp, int maxHp, GateState currentState, int damage)
    {
        if (currentState == GateState.Broken)
        {
            return (0, GateState.Broken, false, false);
        }

        int newHp = Math.Max(0, currentHp - damage);
        GateState newState = currentState;
        bool becameBroken = false;
        bool becameDamaged = false;

        if (newHp <= 0)
        {
            newState = GateState.Broken;
            becameBroken = true;
        }
        else if (newHp <= maxHp / 2 && currentState == GateState.Intact)
        {
            newState = GateState.Damaged;
            becameDamaged = true;
        }

        return (newHp, newState, becameBroken, becameDamaged);
    }

    /// <summary>
    /// 判定皇宫大殿当前占领者 (官方实证 sub_4C9B80：皇宫内清空敌对势力时方可占领)
    /// </summary>
    public static (bool changed, string newOccupier) EvaluatePalaceOccupancy(
        WarPhase phase, string currentOccupier, IEnumerable<(string guild, int uid)> playersInPalace)
    {
        if (phase != WarPhase.InWar) return (false, currentOccupier);

        string? dominantGuild = null;
        bool hasConflict = false;

        foreach (var (guild, _) in playersInPalace)
        {
            if (string.IsNullOrWhiteSpace(guild))
            {
                // 无行会散人存在，视为混战未清理完毕
                hasConflict = true;
                break;
            }

            if (dominantGuild == null)
            {
                dominantGuild = guild;
            }
            else if (!dominantGuild.Equals(guild, StringComparison.OrdinalIgnoreCase))
            {
                // 存在两个以上不同行会的成员在皇宫内厮杀
                hasConflict = true;
                break;
            }
        }

        // 皇宫内只剩下某一个合法行会的成员
        if (!hasConflict && !string.IsNullOrEmpty(dominantGuild))
        {
            if (!currentOccupier.Equals(dominantGuild, StringComparison.OrdinalIgnoreCase))
            {
                return (true, dominantGuild);
            }
        }

        return (false, currentOccupier);
    }
}
