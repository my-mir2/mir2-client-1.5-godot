using System;
using Mir2.Shared.Models;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 道士心灵启示技能精准显血引擎 (RevelationSkillEngine)
/// 严格 1:1 对齐官方 M2Server 技能 16 (Revelation / 心灵启示):
/// 1. 技能施法消耗: 蓝耗 10 点，施放至怪物或玩家目标
/// 2. 持续时间精算: 基础 20 秒 + 技能等级 * 10 秒 + SC / 2 秒 (最高 60 秒)
/// 3. 下发精准当前生命值与最大生命值: 供客户端头顶数字血条精准呈现
/// </summary>
public static class RevelationSkillEngine
{
    public const int RevelationSkillId = 16;
    public const int MpCost = 10;

    public sealed class RevelationTargetInfo
    {
        public int TargetUid { get; init; }
        public string TargetName { get; init; } = string.Empty;
        public int CurrentHp { get; set; }
        public int MaxHp { get; set; }
        public long ExpireTimeMs { get; set; }
        public bool IsExpired(long nowMs) => nowMs >= ExpireTimeMs;
    }

    /// <summary>
    /// 对怪物施放心灵启示
    /// </summary>
    public static (bool success, RevelationTargetInfo? info, string message) CastRevelationOnMonster(
        PlayerState caster,
        MonsterInstance targetMonster,
        int skillLevel,
        long nowMs)
    {
        if (caster == null || targetMonster == null)
        {
            return (false, null, "目标无效。");
        }

        if (caster.Mp < MpCost)
        {
            return (false, null, "魔法值不足，无法施展心灵启示！");
        }

        caster.Mp -= MpCost;

        // 计算持续时间
        int durationSec = Math.Min(60, 20 + (skillLevel * 10) + (caster.Sc / 2));
        var info = new RevelationTargetInfo
        {
            TargetUid = targetMonster.Uid,
            TargetName = targetMonster.Name,
            CurrentHp = targetMonster.Hp,
            MaxHp = targetMonster.MaxHp,
            ExpireTimeMs = nowMs + (durationSec * 1000L)
        };

        return (true, info, $"心灵启示: {targetMonster.Name} 生命值 {targetMonster.Hp}/{targetMonster.MaxHp}");
    }

    /// <summary>
    /// 对玩家施放心灵启示
    /// </summary>
    public static (bool success, RevelationTargetInfo? info, string message) CastRevelationOnPlayer(
        PlayerState caster,
        PlayerState targetPlayer,
        int skillLevel,
        long nowMs)
    {
        if (caster == null || targetPlayer == null)
        {
            return (false, null, "目标无效。");
        }

        if (caster.Mp < MpCost)
        {
            return (false, null, "魔法值不足，无法施展心灵启示！");
        }

        caster.Mp -= MpCost;

        int durationSec = Math.Min(60, 20 + (skillLevel * 10) + (caster.Sc / 2));
        var info = new RevelationTargetInfo
        {
            TargetUid = targetPlayer.Uid,
            TargetName = targetPlayer.Name,
            CurrentHp = targetPlayer.Hp,
            MaxHp = targetPlayer.MaxHp,
            ExpireTimeMs = nowMs + (durationSec * 1000L)
        };

        return (true, info, $"心灵启示: {targetPlayer.Name} 生命值 {targetPlayer.Hp}/{targetPlayer.MaxHp}");
    }
}
