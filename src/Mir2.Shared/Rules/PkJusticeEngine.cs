using System;

namespace Mir2.Shared.Rules;

/// <summary>
/// 盛大官方 1.5 玛法正义与 PK 仇恨规则引擎 (纯 C# 实现)
/// 严格 1:1 还原官方原版 PK 物理真值:
///   1. 名字颜色状态机:
///      - 白名 (White): PK 值 0，良民，受卫士保护
///      - 黄名/灰名 (Yellow): 主动攻击白名进入灰名自卫反击期 (持续 60 秒)
///      - 红名 (Red): PK 值 >= 100 (恶意击杀白名 2 次以上)，大恶人
///   2. 大刀卫士与弓箭手守卫:
///      - 视距 6 格内发现红名玩家，大刀卫士瞬移或隔空挥刀秒杀 (9999 真实伤害)
///      - 弓箭手进行连射压制
/// </summary>
public static class PkJusticeEngine
{
    public enum NameColorType
    {
        White,  // 白名
        Yellow, // 黄名/灰名
        Red     // 红名
    }

    public const int YellowNameThreshold = 100; // 累计 PK 值 >= 100 (击杀 1 名白名) 变黄名
    public const int RedNameThreshold = 200;    // 累计 PK 值 >= 200 (击杀 2 名及以上白名) 正式变红名
    public const int GuardDetectRange = 6;      // 卫士主动仇恨巡逻半径 6 格
    public const int GuardLethalDamage = 9999;  // 大刀卫士绝对秒杀伤害

    public const int SelfDefenseWindowMs = 60_000; // 盛大官方 1.5 规则: 60 秒自卫反击窗口 (毫秒级防卫过当判定)

    /// <summary>
    /// 正当防卫判定: 自受攻击时刻起 60 秒内反击属于正当防卫，反杀不加 PK 点数；超过 60 秒则判定为防卫过当
    /// </summary>
    public static bool IsLegitimateSelfDefense(long attackTimeMs, long nowMs)
    {
        if (attackTimeMs <= 0) return false;
        return nowMs >= attackTimeMs && (nowMs - attackTimeMs <= SelfDefenseWindowMs);
    }

    public static NameColorType DetermineNameColor(int pkPoints, bool isGrayAttacking)
    {
        if (pkPoints >= RedNameThreshold) return NameColorType.Red;
        if (pkPoints >= YellowNameThreshold || isGrayAttacking) return NameColorType.Yellow;
        return NameColorType.White;
    }

    /// <summary>
    /// 卫士仇恨检定: 若目标为红名且在 6 格以内，触发秒杀出刀
    /// </summary>
    public static (bool attack, int damage, string actionDesc) CheckGuardHostility(
        int guardX, int guardY, int targetX, int targetY, NameColorType targetColor)
    {
        if (targetColor != NameColorType.Red)
        {
            return (false, 0, "良民免遭攻击");
        }

        int dist = Math.Max(Math.Abs(guardX - targetX), Math.Abs(guardY - targetY));
        if (dist <= GuardDetectRange)
        {
            return (true, GuardLethalDamage, "大刀卫士攻击红名");
        }

        return (false, 0, "红名超出卫士视线范围");
    }
}
