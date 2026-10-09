using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 师徒声望与姻缘神殿结婚权威引擎 (SocialRelationsEngine)
/// 严格 1:1 对齐官方 M2Server 1.5 "热血神鹰" 规则:
/// 1. 姻缘神殿结婚系统: 封魔谷月老处提交求婚戒指、双向绑定夫妻、夫妻同心传唤传送与离婚
/// 2. 师徒系统: 徒弟等级 < 28 拜师, 师傅等级 >= 35, 徒弟达到 35 级出师奖励师傅声望点数
/// </summary>
public sealed class SocialRelationsEngine
{
    // ==================== 1. 婚姻系统 ====================

    public sealed class MarriageRelation
    {
        public string HusbandName { get; init; } = string.Empty;
        public string WifeName { get; init; } = string.Empty;
        public long MarriageDateMs { get; init; }
    }

    private readonly ConcurrentDictionary<string, MarriageRelation> _marriagesByPlayer = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// 申请结婚 (在姻缘神殿由男方手持求婚戒指发起)
    /// </summary>
    public (bool success, string message) Marry(ClientSession groom, ClientSession bride)
    {
        if (groom.State == null || bride.State == null) return (false, "玩家状态异常。");
        if (groom.State.Sex != 0 || bride.State.Sex != 1) return (false, "必须由男方向女方发起结婚！");

        if (_marriagesByPlayer.ContainsKey(groom.CharacterName) || _marriagesByPlayer.ContainsKey(bride.CharacterName))
        {
            return (false, "双方中有人已经处于婚姻状态，无法重复结婚！");
        }

        // 校验男方背包中是否有 [求婚戒指]
        int ringIdx = groom.State.BagItems.FindIndex(i => i.Name.Contains("求婚戒指"));
        if (ringIdx < 0)
        {
            return (false, "男方必须携带求婚戒指方可成婚！");
        }

        // 扣除求婚戒指
        groom.State.BagItems.RemoveAt(ringIdx);
        groom.State.RecalculateAuthoritativeWeights(groom.State.Gold);

        var relation = new MarriageRelation
        {
            HusbandName = groom.CharacterName,
            WifeName = bride.CharacterName,
            MarriageDateMs = Environment.TickCount64
        };
        _marriagesByPlayer[groom.CharacterName] = relation;
        _marriagesByPlayer[bride.CharacterName] = relation;

        return (true, $"恭喜 [{groom.CharacterName}] 与 [{bride.CharacterName}] 在姻缘神殿喜结良缘！百年好合！");
    }

    /// <summary>
    /// 夫妻同心传唤 (将配偶传送到自己身边)
    /// </summary>
    public (bool success, string message) TeleportToSpouse(ClientSession caller, ClientSession spouse)
    {
        if (!_marriagesByPlayer.TryGetValue(caller.CharacterName, out var rel))
        {
            return (false, "你尚未结婚，无法使用夫妻同心传送！");
        }

        string expectedSpouse = caller.CharacterName.Equals(rel.HusbandName, StringComparison.OrdinalIgnoreCase) ? rel.WifeName : rel.HusbandName;
        if (!spouse.CharacterName.Equals(expectedSpouse, StringComparison.OrdinalIgnoreCase))
        {
            return (false, "目标并非你的合法配偶！");
        }

        spouse.CurrentMapId = caller.CurrentMapId;
        spouse.X = caller.X;
        spouse.Y = caller.Y;
        spouse.EnqueuePacket(ProtocolConsts.SM_NEWMAP_DELPHI, caller.CurrentMapId, caller.X, caller.Y, 0);

        return (true, $"夫妻同心！[{spouse.CharacterName}] 已传送至配偶身边！");
    }

    /// <summary>
    /// 协议离婚
    /// </summary>
    public bool Divorce(string playerName)
    {
        if (_marriagesByPlayer.TryRemove(playerName, out var rel))
        {
            _marriagesByPlayer.TryRemove(rel.HusbandName, out _);
            _marriagesByPlayer.TryRemove(rel.WifeName, out _);
            return true;
        }
        return false;
    }

    // ==================== 2. 师徒系统与武尊声望 ====================

    public sealed class MasterApprenticeRelation
    {
        public string MasterName { get; init; } = string.Empty;
        public string ApprenticeName { get; init; } = string.Empty;
        public bool Graduated { get; set; }
    }

    private readonly ConcurrentDictionary<string, MasterApprenticeRelation> _mentorships = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, int> _reputationPoints = new(StringComparer.OrdinalIgnoreCase);

    public int GetReputation(string playerName) =>
        _reputationPoints.TryGetValue(playerName, out var rep) ? rep : 0;

    /// <summary>
    /// 拜师操作 (徒弟 < 28 级, 师傅 >= 35 级)
    /// </summary>
    public (bool success, string message) BecomeApprentice(ClientSession master, ClientSession apprentice)
    {
        if (master.State == null || apprentice.State == null) return (false, "玩家状态异常。");
        if (master.State.Level < 35) return (false, "师傅等级必须达到 35 级方可招收徒弟！");
        if (apprentice.State.Level >= 28) return (false, "徒弟等级已超过 27 级，无法拜师！");

        if (_mentorships.ContainsKey(apprentice.CharacterName))
        {
            return (false, "该玩家已经有师傅了！");
        }

        var rel = new MasterApprenticeRelation
        {
            MasterName = master.CharacterName,
            ApprenticeName = apprentice.CharacterName,
            Graduated = false
        };
        _mentorships[apprentice.CharacterName] = rel;

        return (true, $"恭喜！[{apprentice.CharacterName}] 正式拜入 [{master.CharacterName}] 门下！");
    }

    /// <summary>
    /// 徒弟达到 35 级出师，奖励师傅声望点数
    /// </summary>
    public (bool success, int gainedReputation, string message) Graduate(ClientSession apprentice, ClientSession? onlineMaster)
    {
        if (apprentice.State == null || apprentice.State.Level < 35)
        {
            return (false, 0, "徒弟等级尚未达到 35 级，无法出师！");
        }

        if (!_mentorships.TryGetValue(apprentice.CharacterName, out var rel) || rel.Graduated)
        {
            return (false, 0, "未找到有效的在教拜师记录。");
        }

        rel.Graduated = true;
        int repBonus = 30; // 官方出师奖励 30 点声望
        _reputationPoints.AddOrUpdate(rel.MasterName, repBonus, (_, old) => old + repBonus);

        if (onlineMaster?.State != null)
        {
            onlineMaster.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"你的徒弟 [{apprentice.CharacterName}] 学成出师！你获得了 {repBonus} 点声望！");
        }

        return (true, repBonus, $"恭喜 [{apprentice.CharacterName}] 学有所成，正式出师！");
    }
}
