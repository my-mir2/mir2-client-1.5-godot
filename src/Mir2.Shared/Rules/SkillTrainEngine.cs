using System;
using System.Collections.Generic;
using Mir2.Shared.Models;

namespace Mir2.Shared.Rules;

/// <summary>
/// 盛大官方 1.5 战法道全量 33 门技能修炼与熟练度升级权威引擎 (SkillTrainEngine)
/// 严格 1:1 对齐官方 M2Server 技能修炼升级规则 (IDA Delphi 逆向实证):
/// 1. 每门技能 0~3 级修炼所需角色等级与熟练度上限 (MaxTrain);
/// 2. 施法/攻击成功时增加熟练度 (CurTrain++);
/// 3. 当 CurTrain >= MaxTrain 且角色等级达到升下一级要求时自动升级并重置 CurTrain = 0;
/// 4. 角色等级未达到下一级要求时熟练度卡在 MaxTrain - 1，不再虚增。
/// </summary>
public static class SkillTrainEngine
{
    public readonly record struct TrainRequirement(int ReqLevel, int MaxTrain);

    public sealed class SkillTrainRule
    {
        public int MagicId { get; init; }
        public string Name { get; init; } = string.Empty;
        public TrainRequirement L1 { get; init; }
        public TrainRequirement L2 { get; init; }
        public TrainRequirement L3 { get; init; }

        public TrainRequirement GetRequirement(int targetLevel) => targetLevel switch
        {
            1 => L1,
            2 => L2,
            3 => L3,
            _ => new TrainRequirement(999, 999999)
        };
    }

    private static readonly System.Collections.Frozen.FrozenDictionary<int, SkillTrainRule> Rules;
    private static readonly Dictionary<int, SkillTrainRule> _initRules = new();

    static SkillTrainEngine()
    {
        // 战士 6 门技能
        Register(new SkillTrainRule { MagicId = 3,  Name = "基本剑术",   L1 = new(7, 100),   L2 = new(11, 200),  L3 = new(16, 300) });
        Register(new SkillTrainRule { MagicId = 7,  Name = "攻杀剑术",   L1 = new(19, 300),  L2 = new(22, 500),  L3 = new(24, 800) });
        Register(new SkillTrainRule { MagicId = 12, Name = "刺杀剑术",   L1 = new(25, 500),  L2 = new(27, 800),  L3 = new(29, 1200) });
        Register(new SkillTrainRule { MagicId = 25, Name = "半月弯刀",   L1 = new(28, 600),  L2 = new(31, 1000), L3 = new(34, 1500) });
        Register(new SkillTrainRule { MagicId = 26, Name = "烈火剑法",   L1 = new(35, 1000), L2 = new(37, 1500), L3 = new(40, 2500) });
        Register(new SkillTrainRule { MagicId = 27, Name = "野蛮冲撞",   L1 = new(30, 600),  L2 = new(34, 1000), L3 = new(39, 1600) });

        // 法师 14 门技能
        Register(new SkillTrainRule { MagicId = 1,  Name = "火球术",     L1 = new(7, 100),   L2 = new(11, 200),  L3 = new(16, 300) });
        Register(new SkillTrainRule { MagicId = 5,  Name = "大火球",     L1 = new(19, 300),  L2 = new(23, 500),  L3 = new(25, 800) });
        Register(new SkillTrainRule { MagicId = 8,  Name = "抗拒火环",   L1 = new(12, 150),  L2 = new(15, 250),  L3 = new(19, 400) });
        Register(new SkillTrainRule { MagicId = 9,  Name = "地狱火",     L1 = new(16, 200),  L2 = new(20, 350),  L3 = new(24, 500) });
        Register(new SkillTrainRule { MagicId = 10, Name = "雷电术",     L1 = new(17, 250),  L2 = new(20, 450),  L3 = new(23, 700) });
        Register(new SkillTrainRule { MagicId = 11, Name = "瞬息移动",   L1 = new(19, 250),  L2 = new(22, 400),  L3 = new(25, 600) });
        Register(new SkillTrainRule { MagicId = 20, Name = "诱惑之光",   L1 = new(13, 150),  L2 = new(18, 300),  L3 = new(24, 500) });
        Register(new SkillTrainRule { MagicId = 21, Name = "地狱雷光",   L1 = new(30, 600),  L2 = new(32, 900),  L3 = new(34, 1400) });
        Register(new SkillTrainRule { MagicId = 22, Name = "火墙",       L1 = new(24, 400),  L2 = new(29, 700),  L3 = new(33, 1100) });
        Register(new SkillTrainRule { MagicId = 23, Name = "疾光电影",   L1 = new(26, 450),  L2 = new(29, 750),  L3 = new(32, 1200) });
        Register(new SkillTrainRule { MagicId = 24, Name = "圣言术",     L1 = new(32, 600),  L2 = new(35, 1000), L3 = new(38, 1500) });
        Register(new SkillTrainRule { MagicId = 31, Name = "魔法盾",     L1 = new(31, 600),  L2 = new(34, 1000), L3 = new(38, 1600) });
        Register(new SkillTrainRule { MagicId = 32, Name = "冰咆哮",     L1 = new(35, 1000), L2 = new(37, 1500), L3 = new(40, 2500) });
        Register(new SkillTrainRule { MagicId = 33, Name = "爆裂火焰",   L1 = new(22, 350),  L2 = new(27, 600),  L3 = new(31, 1000) });

        // 道士 13 门技能
        Register(new SkillTrainRule { MagicId = 2,  Name = "治愈术",     L1 = new(7, 100),   L2 = new(11, 200),  L3 = new(16, 300) });
        Register(new SkillTrainRule { MagicId = 4,  Name = "精神力战法", L1 = new(9, 150),   L2 = new(13, 250),  L3 = new(19, 350) });
        Register(new SkillTrainRule { MagicId = 6,  Name = "施毒术",     L1 = new(14, 200),  L2 = new(17, 350),  L3 = new(20, 500) });
        Register(new SkillTrainRule { MagicId = 13, Name = "灵魂火符",   L1 = new(18, 250),  L2 = new(21, 450),  L3 = new(24, 700) });
        Register(new SkillTrainRule { MagicId = 14, Name = "幽灵盾",     L1 = new(22, 300),  L2 = new(24, 500),  L3 = new(26, 800) });
        Register(new SkillTrainRule { MagicId = 15, Name = "神圣战甲术", L1 = new(25, 400),  L2 = new(27, 650),  L3 = new(29, 900) });
        Register(new SkillTrainRule { MagicId = 16, Name = "困魔咒",     L1 = new(28, 450),  L2 = new(30, 700),  L3 = new(32, 1000) });
        Register(new SkillTrainRule { MagicId = 17, Name = "召唤骷髅",   L1 = new(19, 300),  L2 = new(23, 500),  L3 = new(26, 800) });
        Register(new SkillTrainRule { MagicId = 18, Name = "隐身术",     L1 = new(20, 300),  L2 = new(23, 500),  L3 = new(26, 800) });
        Register(new SkillTrainRule { MagicId = 19, Name = "集体隐身术", L1 = new(21, 300),  L2 = new(24, 500),  L3 = new(27, 800) });
        Register(new SkillTrainRule { MagicId = 28, Name = "心灵启示",   L1 = new(26, 400),  L2 = new(30, 700),  L3 = new(35, 1100) });
        Register(new SkillTrainRule { MagicId = 29, Name = "群体治愈术", L1 = new(33, 700),  L2 = new(35, 1100), L3 = new(38, 1600) });
        Register(new SkillTrainRule { MagicId = 30, Name = "召唤神兽",   L1 = new(35, 1000), L2 = new(37, 1500), L3 = new(40, 2500) });

        Rules = System.Collections.Frozen.FrozenDictionary.ToFrozenDictionary(_initRules);
    }

    private static void Register(SkillTrainRule rule)
    {
        _initRules[rule.MagicId] = rule;
    }

    public static SkillTrainRule? GetRule(int magicId)
    {
        if (Rules.TryGetValue(magicId, out var rule)) return rule;
        return null;
    }

    /// <summary>
    /// 获取技能下一级的修炼要求 (所需等级与熟练度)
    /// </summary>
    public static TrainRequirement GetNextLevelRequirement(int magicId, byte currentLevel)
    {
        var rule = GetRule(magicId);
        if (rule == null)
        {
            return new TrainRequirement(currentLevel switch { 0 => 7, 1 => 11, 2 => 16, _ => 999 }, 1000);
        }
        return rule.GetRequirement(currentLevel + 1);
    }

    /// <summary>
    /// 为玩家添加或初始化已学技能
    /// </summary>
    public static UserMagic LearnSkill(PlayerState player, int magicId, string name = "", byte initialLevel = 0)
    {
        var existing = player.Magics.Find(m => m.MagicId == magicId);
        if (existing != null)
        {
            return existing;
        }

        var rule = GetRule(magicId);
        string skillName = string.IsNullOrEmpty(name) ? (rule?.Name ?? $"技能_{magicId}") : name;
        var nextReq = GetNextLevelRequirement(magicId, initialLevel);

        var newMagic = new UserMagic
        {
            MagicId = magicId,
            Name = skillName,
            Level = initialLevel,
            Key = 255,
            CurTrain = 0,
            MaxTrain = nextReq.MaxTrain
        };
        player.Magics.Add(newMagic);
        return newMagic;
    }

    /// <summary>
    /// 增加技能熟练度并触发权威升级
    /// </summary>
    public static (bool trained, bool leveledUp, UserMagic? magic, string message) TrainSkill(
        PlayerState player, int magicId, int addExp = 1)
    {
        var magic = player.Magics.Find(m => m.MagicId == magicId);
        if (magic == null)
        {
            return (false, false, null, "尚未习得该技能");
        }

        // 官方 1.5 满级为 3 级
        if (magic.Level >= 3)
        {
            return (false, false, magic, "该技能已修炼至最高境界 (Lv.3)");
        }

        var req = GetNextLevelRequirement(magicId, magic.Level);
        magic.MaxTrain = req.MaxTrain;

        // 角色等级未达到下一级修炼要求门槛时，熟练度卡在 MaxTrain - 1
        if (player.Level < req.ReqLevel)
        {
            if (magic.CurTrain < magic.MaxTrain - 1)
            {
                magic.CurTrain = Math.Min(magic.MaxTrain - 1, magic.CurTrain + addExp);
                return (true, false, magic, $"熟练度提升 ({magic.CurTrain}/{magic.MaxTrain})");
            }
            return (false, false, magic, $"等级不足 (需达到 {req.ReqLevel} 级方可突破至 Lv.{magic.Level + 1})");
        }

        // 正常增加熟练度
        magic.CurTrain += addExp;
        if (magic.CurTrain >= magic.MaxTrain)
        {
            // 技能升级
            magic.Level++;
            magic.CurTrain = 0;
            if (magic.Level < 3)
            {
                var nextReq = GetNextLevelRequirement(magicId, magic.Level);
                magic.MaxTrain = nextReq.MaxTrain;
            }
            else
            {
                magic.MaxTrain = 0;
            }

            return (true, true, magic, $"【{magic.Name}】技能修炼大成，晋升至 Lv.{magic.Level}！");
        }

        return (true, false, magic, $"熟练度提升 ({magic.CurTrain}/{magic.MaxTrain})");
    }
}
