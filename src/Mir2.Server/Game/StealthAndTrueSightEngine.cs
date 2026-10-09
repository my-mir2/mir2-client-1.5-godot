using System;
using System.Collections.Generic;
using Mir2.Shared.Models;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 隐身术与怪物真视反隐权威引擎 (StealthAndTrueSightEngine)
/// 严格 1:1 对齐官方 M2Server 怪物仇恨与隐身逻辑:
/// 1. 隐身术 (Skill 14) 与集体隐身术 (Skill 18):
///    - 目标进入隐身状态 (IsStealthed = true)
///    - 持续时间根据道士道术精算 (通常 15~45 秒)
/// 2. 怪物仇恨判定:
///    - 普通怪物 (僵尸、骷髅、野猪、沃玛怪) 无法察觉隐身目标，自动丢失仇恨，不会主动攻击
/// 3. 官方真视怪物 (True Sight Monsters):
///    - 楔蛾 (Moth)、月魔蜘蛛 (MoonSpider)、暗之双头血魔等具备真视之眼，能够无视隐身直接锁定攻击
/// 4. 破隐触发条件:
///    - 隐身目标主动移动 (Walk/Run)、发起物理挥刀或释放技能，隐身状态立即破除失效
/// </summary>
public static class StealthAndTrueSightEngine
{
    public static readonly HashSet<string> TrueSightMonsters = new(StringComparer.OrdinalIgnoreCase)
    {
        "楔蛾",
        "月魔蜘蛛",
        "暗之双头血魔",
        "暗之骷髅精灵",
        "暗之黄泉教主",
        "暗之沃玛教主",
        "暗之双头金刚",
        "赤月恶魔",
        "触龙神"
    };

    /// <summary>
    /// 判定怪物是否具备真视反隐能力
    /// </summary>
    public static bool HasTrueSight(string monsterName)
    {
        if (string.IsNullOrWhiteSpace(monsterName)) return false;
        foreach (var tsm in TrueSightMonsters)
        {
            if (monsterName.Contains(tsm, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 怪物是否能够察觉并锁定目标
    /// </summary>
    public static bool CanMonsterDetectTarget(string monsterName, bool targetIsStealthed)
    {
        if (!targetIsStealthed) return true; // 未隐身，必可锁定
        return HasTrueSight(monsterName);     // 隐身状态下仅真视怪物能洞察
    }

    /// <summary>
    /// 计算道士施放隐身术的持续时间 (毫秒)
    /// </summary>
    public static long CalculateStealthDurationMs(int sc, int skillLevel)
    {
        // 官方公式: 基础 15 秒 + 技能等级 * 5 秒 + SC 浮动加成
        int baseSec = 15 + (skillLevel * 5) + Math.Min(15, sc / 2);
        return baseSec * 1000L;
    }

    /// <summary>
    /// 角色做出破坏隐身的动作时破除隐身
    /// </summary>
    public static bool BreakStealthOnAction(ref bool isStealthed)
    {
        if (isStealthed)
        {
            isStealthed = false;
            return true; // 触发了破隐
        }
        return false;
    }
}
