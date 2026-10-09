using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Mir2.Shared.Models;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.5 师徒系统与荣誉勋章权威引擎 (MasterApprenticeHonorEngine)
/// 严格 1:1 对齐官方 M2Server 规则:
/// 1. 拜师门槛: 师傅等级 >= 35 级，徒弟等级 <= 28 级
/// 2. 声望积累:
///    - 徒弟每提升 1 级，师傅获得 1 点声望值 (Prestige)
///    - 徒弟升至 35 级正式出师，师傅获得 20 点丰厚声望大奖，徒弟获得 10 点声望
/// 3. 比奇武馆教头处兑换荣誉勋章:
///    - 11号勋章 (战 1-1, 需声望 10)
///    - 12号勋章 (法 1-1, 需声望 10)
///    - 13号勋章 (道 1-1, 需声望 10)
///    - 14号勋章 (全属性 1-2, 需声望 25)
///    - 15号勋章 (极品 2-3, 需声望 50)
/// </summary>
public sealed class MasterApprenticeHonorEngine
{
    public sealed class MasterRelation
    {
        public string MasterName { get; init; } = string.Empty;
        public string ApprenticeName { get; init; } = string.Empty;
        public int ApprenticeStartLevel { get; init; }
        public bool IsGraduated { get; set; }
    }

    private static readonly ConcurrentDictionary<string, MasterRelation> _apprenticeToMaster = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// 申请拜师建立师徒关系
    /// </summary>
    public static (bool success, string message) RegisterApprenticeship(PlayerState master, PlayerState apprentice)
    {
        if (master == null || apprentice == null) return (false, "玩家状态异常。");

        if (master.Level < 35)
        {
            return (false, "只有达到 35 级的大师方有资格为人师表！");
        }

        if (apprentice.Level > 28)
        {
            return (false, "你已等级过高 (> 28 级)，已不再需要拜师学艺！");
        }

        if (_apprenticeToMaster.ContainsKey(apprentice.CharacterName))
        {
            return (false, "你已拜入他人门下，不可三心二意！");
        }

        var rel = new MasterRelation
        {
            MasterName = master.CharacterName,
            ApprenticeName = apprentice.CharacterName,
            ApprenticeStartLevel = apprentice.Level,
            IsGraduated = false
        };

        _apprenticeToMaster[apprentice.CharacterName] = rel;
        return (true, $"恭喜！{apprentice.CharacterName}正式拜入{master.CharacterName}门下！传道受业，薪火相传！");
    }

    /// <summary>
    /// 徒弟升级时检测师傅声望奖励与 35 级出师
    /// </summary>
    public static (bool isGraduated, int masterPrestigeAward, int apprenticePrestigeAward, string message) OnApprenticeLevelUp(
        PlayerState apprentice,
        PlayerState? masterIfOnline)
    {
        if (apprentice == null) return (false, 0, 0, string.Empty);

        if (!_apprenticeToMaster.TryGetValue(apprentice.CharacterName, out var rel) || rel.IsGraduated)
        {
            return (false, 0, 0, string.Empty);
        }

        if (apprentice.Level >= 35)
        {
            rel.IsGraduated = true;
            int masterGain = 20;
            int apprenticeGain = 10;

            if (masterIfOnline != null)
            {
                masterIfOnline.Prestige += masterGain;
            }
            apprentice.Prestige += apprenticeGain;

            return (true, masterGain, apprenticeGain, $"恭喜！高徒{apprentice.CharacterName}功德圆满，达到 35 级正式出师！师傅获得 {masterGain} 点声望，徒弟获得 {apprenticeGain} 点声望！");
        }

        // 常规升级，师傅获得 1 点声望
        if (masterIfOnline != null)
        {
            masterIfOnline.Prestige += 1;
        }

        return (false, 1, 0, $"徒弟{apprentice.CharacterName}勤学苦练升级，师傅声望 +1！");
    }

    /// <summary>
    /// 武馆教头处使用声望兑换官方荣誉勋章
    /// </summary>
    public static (bool success, EquipItem? badge, string message) ExchangeHonorBadge(PlayerState player, int badgeId)
    {
        if (player == null) return (false, null, "角色状态异常。");

        int cost = badgeId switch
        {
            11 => 10,
            12 => 10,
            13 => 10,
            14 => 25,
            15 => 50,
            _ => -1
        };

        if (cost < 0) return (false, null, "该勋章编号不存在！");
        if (player.Prestige < cost)
        {
            return (false, null, $"你的江湖声望不足！兑换 {badgeId}号勋章 需要 {cost} 点声望，当前仅有 {player.Prestige} 点。");
        }

        if (player.BagItems.Count >= 40)
        {
            return (false, null, "背包已满，请清理空位后再兑换勋章！");
        }

        player.Prestige -= cost;

        var badge = new EquipItem
        {
            Name = $"荣誉勋章{badgeId}号",
            Dura = 50,
            MaxDura = 50,
            Dc = (badgeId == 11 || badgeId == 14) ? 1 : (badgeId == 15 ? 2 : 0),
            MaxDc = (badgeId == 11) ? 2 : (badgeId == 14 ? 3 : (badgeId == 15 ? 4 : 0)),
            Mc = (badgeId == 12 || badgeId == 14) ? 1 : (badgeId == 15 ? 2 : 0),
            MaxMc = (badgeId == 12) ? 2 : (badgeId == 14 ? 3 : (badgeId == 15 ? 4 : 0)),
            Sc = (badgeId == 13 || badgeId == 14) ? 1 : (badgeId == 15 ? 2 : 0),
            MaxSc = (badgeId == 13) ? 2 : (badgeId == 14 ? 3 : (badgeId == 15 ? 4 : 0)),
            Ac = (badgeId >= 14) ? 1 : 0,
            MaxAc = (badgeId >= 14) ? 2 : 0
        };

        player.BagItems.Add(badge);
        return (true, badge, $"成功消耗 {cost} 点声望，兑换了【{badge.Name}】！佩戴彰显宗师荣耀！");
    }
}
