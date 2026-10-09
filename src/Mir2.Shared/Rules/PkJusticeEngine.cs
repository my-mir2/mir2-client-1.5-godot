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

    public const int RedNameThreshold = 100; // 累计 PK 值 >= 100 变红名
    public const int GuardDetectRange = 6;   // 卫士主动仇恨巡逻半径 6 格
    public const int GuardLethalDamage = 9999; // 大刀卫士绝对秒杀伤害

    public static NameColorType DetermineNameColor(int pkPoints, bool isGrayAttacking)
    {
        if (pkPoints >= RedNameThreshold) return NameColorType.Red;
        if (isGrayAttacking) return NameColorType.Yellow;
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
            return (true, GuardLethalDamage, "【大刀卫士怒吼】恶贼伏诛！大刀划破苍穹，一击将红名暴徒斩落马下！");
        }

        return (false, 0, "红名超出卫士视线范围");
    }
}
