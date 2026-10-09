using System;
using System.Collections.Generic;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 神装共鸣全服异象事件引擎 (SuitWorldEventEngine)
/// 严格 1:1 对齐官方 M2Server 隐藏规则:
/// 1. 祈祷全套“神之祈祷全服大叛变”事件:
///    - 穿齐祈祷头盔/项链/手镯/戒指/刃，全服广播“神之祈祷，天地变色，所有召唤物陷入癫狂！”
///    - 全服道士骷髅、神兽、法师宠物叛变反噬，装备随机粉碎一件
/// 2. 记忆全套队长“@天地合一”全服瞬移召唤:
///    - 记忆头/链/镯/戒穿齐，跨地图瞬间拉取队伍全员至身边
/// </summary>
public sealed class SuitWorldEventEngine
{
    /// <summary>
    /// 检定角色是否穿戴齐整套祈祷神装 (5 件)
    /// </summary>
    public static bool HasCompleteGodPrayerSuit(PlayerState state)
    {
        bool hasHelm = false, hasNeck = false, hasArm = false, hasRing = false, hasWeapon = false;

        for (int i = 0; i < 9; i++)
        {
            var item = state.Equipment[i];
            if (item == null) continue;

            if (item.Name.Contains("祈祷头盔")) hasHelm = true;
            else if (item.Name.Contains("祈祷项链")) hasNeck = true;
            else if (item.Name.Contains("祈祷手镯")) hasArm = true;
            else if (item.Name.Contains("祈祷戒指")) hasRing = true;
            else if (item.Name.Contains("祈祷之刃") || item.Name.Contains("祈祷刀")) hasWeapon = true;
        }

        return hasHelm && hasNeck && hasArm && hasRing && hasWeapon;
    }

    /// <summary>
    /// 触发祈祷全套神之大叛变事件
    /// </summary>
    public static (bool triggered, string broadcastMessage, string brokenItemName) TriggerGodPrayerRebellion(
        PlayerState wearer,
        IEnumerable<MonsterInstance> allMonsters,
        Random rng)
    {
        if (!HasCompleteGodPrayerSuit(wearer))
        {
            return (false, string.Empty, string.Empty);
        }

        // 1. 全服所有召唤物变异为红名发狂怪，反噬其主人
        foreach (var m in allMonsters)
        {
            if (m.MasterSessionId > 0 && !m.IsDead)
            {
                m.MasterSessionId = 0; // 脱离主人掌控
                m.Name = "发狂的" + m.Name;
            }
        }

        // 2. 穿戴者身上随机粉碎一件祈祷装备 (官方反噬代价)
        var prayerSlots = new List<int>();
        for (int i = 0; i < 9; i++)
        {
            var item = wearer.Equipment[i];
            if (item != null && item.Name.Contains("祈祷"))
            {
                prayerSlots.Add(i);
            }
        }

        string brokenName = "祈祷首饰";
        if (prayerSlots.Count > 0)
        {
            int slotToBreak = prayerSlots[rng.Next(prayerSlots.Count)];
            var broken = wearer.Equipment[slotToBreak];
            if (broken != null)
            {
                brokenName = broken.Name;
                wearer.Equipment[slotToBreak] = null; // 彻底粉碎销毁
                wearer.RecalculateAttributes();
            }
        }

        string msg = "神之祈祷，天地变色！玛法大陆所有召唤生物已陷入癫狂，反噬其主！";
        return (true, msg, brokenName);
    }

    /// <summary>
    /// 检定角色是否穿戴齐整套记忆神装 (4 件: 头/链/镯/戒)
    /// </summary>
    public static bool HasCompleteMemorySuit(PlayerState state)
    {
        bool hasHelm = false, hasNeck = false, hasArm = false, hasRing = false;

        for (int i = 0; i < 9; i++)
        {
            var item = state.Equipment[i];
            if (item == null) continue;

            if (item.Name.Contains("记忆头盔")) hasHelm = true;
            else if (item.Name.Contains("记忆项链")) hasNeck = true;
            else if (item.Name.Contains("记忆手镯")) hasArm = true;
            else if (item.Name.Contains("记忆戒指")) hasRing = true;
        }

        return hasHelm && hasNeck && hasArm && hasRing;
    }

    /// <summary>
    /// 记忆套队长施展 [@天地合一] 全服瞬间召唤队友
    /// </summary>
    public static (bool success, int summonedCount, string message) ExecuteMemorySummonAll(
        ClientSession leader,
        IEnumerable<ClientSession> teamMembers)
    {
        if (leader.State == null) return (false, 0, "玩家状态异常。");

        if (!HasCompleteMemorySuit(leader.State))
        {
            return (false, 0, "你尚未穿齐记忆全套 (记忆头盔/项链/手镯/戒指)，无法使用天地合一！");
        }

        int count = 0;
        foreach (var member in teamMembers)
        {
            if (member.SessionId == leader.SessionId) continue;
            if (member.State == null || member.State.Hp <= 0) continue;

            // 跨地图瞬移至队长身边
            member.CurrentMapId = leader.CurrentMapId;
            member.X = leader.X;
            member.Y = leader.Y;
            member.EnqueuePacket(ProtocolConsts.SM_NEWMAP_DELPHI, leader.CurrentMapId, leader.X, leader.Y, 0);
            member.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "队长施展了【天地合一】，你已被时空法阵召唤至队长身边！");
            count++;
        }

        return (true, count, $"天地合一！成功召唤了队伍中的 {count} 名队友！");
    }
}
