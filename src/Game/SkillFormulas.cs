using System;
using System.Collections.Generic;

namespace Mir2Client.Game;

/// <summary>
/// 技能静态元数据
/// </summary>
public readonly record struct SkillMeta(
    short Id,
    string Name,
    int IconIndex,
    int MpCost,
    int Job // 0=战士, 1=法师, 2=道士, 3=通用
);

/// <summary>
/// 盛大官方 1.5 原版 32 门技能与魔法伤害数值公式库 (Magic ID 1~32 连续封闭分母)
/// 严格 1:1 还原 IDA Pro Delphi 逻辑与 M2Server 逆向实证 (sub_4BDD20 / sub_4C6E4C / sub_4CBCEC)
/// </summary>
public static class SkillFormulas
{
    private static readonly Dictionary<string, SkillMeta> SkillsByName = new(StringComparer.OrdinalIgnoreCase);
    private static readonly Dictionary<short, SkillMeta> SkillsById = new();

    static SkillFormulas()
    {
        Register(new SkillMeta(1, "火球术", 6, 5, 1));
        Register(new SkillMeta(2, "治愈术", 20, 5, 2));
        Register(new SkillMeta(3, "基本剑术", 0, 0, 0));
        Register(new SkillMeta(4, "精神力战法", 21, 0, 2));
        Register(new SkillMeta(5, "大火球", 12, 15, 1));
        Register(new SkillMeta(6, "施毒术", 22, 8, 2));
        Register(new SkillMeta(7, "攻杀剑术", 1, 0, 0));
        Register(new SkillMeta(8, "抗拒火环", 7, 8, 1));
        Register(new SkillMeta(9, "地狱火", 9, 12, 1));
        Register(new SkillMeta(10, "雷电术", 10, 12, 1));
        Register(new SkillMeta(11, "瞬息移动", 11, 15, 1));
        Register(new SkillMeta(12, "刺杀剑术", 2, 0, 0));
        Register(new SkillMeta(13, "灵魂火符", 23, 10, 2));
        Register(new SkillMeta(14, "幽灵盾", 24, 12, 2));
        Register(new SkillMeta(15, "神圣战甲术", 25, 12, 2));
        Register(new SkillMeta(16, "困魔咒", 26, 15, 2));
        Register(new SkillMeta(17, "召唤骷髅", 27, 18, 2));
        Register(new SkillMeta(18, "隐身术", 28, 10, 2));
        Register(new SkillMeta(19, "集体隐身术", 29, 15, 2));
        Register(new SkillMeta(20, "诱惑之光", 8, 10, 1));
        Register(new SkillMeta(21, "地狱雷光", 16, 25, 1));
        Register(new SkillMeta(22, "火墙", 14, 20, 1));
        Register(new SkillMeta(23, "疾光电影", 15, 22, 1));
        Register(new SkillMeta(24, "圣言术", 18, 35, 1));
        Register(new SkillMeta(25, "半月弯刀", 3, 5, 0));
        Register(new SkillMeta(26, "烈火剑法", 4, 15, 0));
        Register(new SkillMeta(27, "野蛮冲撞", 5, 8, 0));
        Register(new SkillMeta(28, "心灵启示", 30, 10, 2));
        Register(new SkillMeta(29, "群体治愈术", 31, 25, 2));
        Register(new SkillMeta(30, "召唤神兽", 32, 30, 2));
        Register(new SkillMeta(31, "魔法盾", 17, 30, 1));
        Register(new SkillMeta(32, "冰咆哮", 19, 40, 1));
    }

    private static void Register(SkillMeta meta)
    {
        SkillsByName[meta.Name] = meta;
        SkillsById[meta.Id] = meta;
    }

    public static SkillMeta? GetSkill(string name) =>
        SkillsByName.TryGetValue(name, out var meta) ? meta : null;

    public static SkillMeta? GetSkill(short id) =>
        SkillsById.TryGetValue(id, out var meta) ? meta : null;

    public static int TotalSkillsCount => SkillsById.Count;

    /// <summary>
    /// 魔法盾减伤计算 (IDA sub_4BDD20 @ 0x4bddc9 & flt_4BDDE4=100.0, flt_4BDDE8=8.0)
    /// actual_damage = ROUND( (base_damage / 100.0) * (shield_level + 2) * 8.0 )
    /// </summary>
    public static (int actualDamage, int absorbedDamage, int newRemainingSec, bool isBroken)
        CalculateMagicShieldDamage(int baseDamage, int shieldLevel, int curRemainingSec)
    {
        if (baseDamage <= 0 || shieldLevel < 0)
        {
            return (0, 0, curRemainingSec, curRemainingSec <= 0);
        }

        int actual = (int)Math.Round((double)baseDamage / 100.0 * (shieldLevel + 2) * 8.0);
        int absorbed = Math.Max(0, baseDamage - actual);

        // 每次吸收伤害扣除 3 秒盾持续时间 (sub_4C6ED0 @ 0x4c6ee9)
        int newRemainingSec = curRemainingSec <= 3 ? 1 : curRemainingSec - 3;
        bool isBroken = newRemainingSec <= 0;

        return (actual, absorbed, newRemainingSec, isBroken);
    }

    /// <summary>
    /// 道士神圣战甲术 (物防) 与幽灵盾 (魔防) 增益精算 (IDA sub_4C03B0 @ 0x4c0bd2, 0x4c0c1c)
    /// bonus = (level / 7) + 2
    /// </summary>
    public static (int finalAc2, int finalMac2, int bonus)
        CalculateTaoistDefenseBuff(int level, int baseAc2, int baseMac2, bool hasHolyShield, bool hasGhostShield)
    {
        int bonus = (level / 7) + 2;
        int finalAc2 = baseAc2 + (hasHolyShield ? bonus : 0);
        int finalMac2 = baseMac2 + (hasGhostShield ? bonus : 0);
        return (finalAc2, finalMac2, bonus);
    }

    /// <summary>
    /// 烈火剑法伤害爆发精算 (sub_4CBCEC: 150% ~ 260% 爆发系数)
    /// level 0: 150%, level 1: 180%, level 2: 220%, level 3: 260%
    /// </summary>
    public static int CalculateFireSwordDamage(int baseDc, int skillLevel)
    {
        double multiplier = skillLevel switch
        {
            0 => 1.5,
            1 => 1.8,
            2 => 2.2,
            _ => 2.6
        };
        return (int)Math.Round(baseDc * multiplier);
    }
}
