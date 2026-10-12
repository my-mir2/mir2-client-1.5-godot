using System;
using System.Collections.Generic;
using Mir2.Server.Maps;
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
    public static (bool triggered, string broadcastMessage, string brokenItemName, int brokenSlot, int brokenItemUid) TriggerGodPrayerRebellion(
        PlayerState wearer,
        IEnumerable<MonsterInstance> allMonsters,
        Random rng)
    {
        if (!HasCompleteGodPrayerSuit(wearer))
        {
            return (false, string.Empty, string.Empty, -1, 0);
        }

        // 1. 全服所有召唤物变异为红名发狂怪，锁定反噬其原主人
        foreach (var m in allMonsters)
        {
            if (m.MasterSessionId > 0 && !m.IsDead)
            {
                int oldMasterId = m.MasterSessionId;
                m.MasterSessionId = 0; // 脱离主人掌控
                m.TamedTickMs = 0;
                m.TargetSessionId = oldMasterId; // 目标锁定原主人
                m.ThreatTable.AddHate(oldMasterId, 2000); // 注入强烈反噬仇恨
                if (!m.Name.StartsWith("发狂的"))
                {
                    m.Name = "发狂的" + m.Name;
                }
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
        int brokenSlot = -1;
        int brokenUid = 0;
        if (prayerSlots.Count > 0)
        {
            brokenSlot = prayerSlots[rng.Next(prayerSlots.Count)];
            var broken = wearer.Equipment[brokenSlot];
            if (broken != null)
            {
                brokenName = broken.Name;
                brokenUid = broken.Uid;
                wearer.Equipment[brokenSlot] = null; // 彻底粉碎销毁
                wearer.RecalculateAuthoritativeWeights(wearer.Gold);
                wearer.RecalculateAttributes();
            }
        }

        string msg = "神之祈祷，天地变色！玛法大陆所有召唤生物已陷入癫狂，反噬其主！";
        return (true, msg, brokenName, brokenSlot, brokenUid);
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

    private static readonly (int dx, int dy)[] SummonScatterOffsets = new (int, int)[]
    {
        (0, 1), (1, 0), (0, -1), (-1, 0),
        (1, 1), (-1, 1), (1, -1), (-1, -1),
        (0, 2), (2, 0), (0, -2), (-2, 0),
        (1, 2), (-1, 2), (1, -2), (-1, -2),
        (2, 1), (-2, 1), (2, -1), (-2, -1),
        (2, 2), (-2, 2), (2, -2), (-2, -2)
    };

    /// <summary>
    /// 记忆套队长施展 [@天地合一] 全服瞬间召唤队友
    /// </summary>
    public static (bool success, int summonedCount, string message) ExecuteMemorySummonAll(
        ClientSession leader,
        IEnumerable<ClientSession> teamMembers,
        MapManager? mapManager = null,
        TradeEngine? tradeEngine = null,
        long nowMs = 0,
        MonsterRegenEngine? regenEngine = null)
    {
        if (leader.State == null || leader.State.Hp <= 0) return (false, 0, "玩家状态异常或已倒地阵亡。");

        if (!HasCompleteMemorySuit(leader.State))
        {
            return (false, 0, "你尚未穿齐记忆全套 (记忆头盔/项链/手镯/戒指)，无法使用天地合一！");
        }

        // 盛大官方 1.5 铁律: 沙巴克皇宫受神秘禁空法阵保护，严禁施展天地合一召唤
        if (leader.CurrentMapId == SabukSiegeEngine.SabukPalaceMapId)
        {
            return (false, 0, "沙巴克皇宫受禁空结界封锁，神力无法穿透，无法施展天地合一！");
        }

        if (nowMs <= 0) nowMs = Environment.TickCount64;
        if (leader.State.LastMemoryRecallTickMs > 0 && nowMs - leader.State.LastMemoryRecallTickMs < 180_000)
        {
            long remSec = (180_000 - (nowMs - leader.State.LastMemoryRecallTickMs)) / 1000;
            return (false, 0, $"记忆套装神力正在沉睡中，还需等待 {remSec} 秒方可再次施展天地合一！");
        }

        int targetMapId = leader.CurrentMapId;
        GameMap? newMap = mapManager?.GetMap(targetMapId) ?? mapManager?.DefaultMap;

        var occupiedPositions = new HashSet<(int x, int y)> { (leader.X, leader.Y) };
        int count = 0;
        int refusedCount = 0;

        foreach (var member in teamMembers)
        {
            if (member.SessionId == leader.SessionId) continue;
            if (member.State == null || member.State.Hp <= 0) continue;

            // 位于沙巴克皇宫的队员无法被拉扯
            if (member.CurrentMapId == SabukSiegeEngine.SabukPalaceMapId) continue;

            // 队员自主开启了拒绝天地合一开关
            if (!member.State.AllowGroupRecall)
            {
                refusedCount++;
                continue;
            }

            // 熔断被拉扯队员正在进行的交易，杜绝跨地图隔空刷物漏洞
            if (tradeEngine != null && tradeEngine.GetTrade(member.SessionId) != null)
            {
                tradeEngine.CancelTrade(member, "因队伍天地合一召唤，交易中断！");
            }

            // 智能开阔地面散布寻点，杜绝实体穿人叠位碰撞
            int spawnX = leader.X;
            int spawnY = leader.Y;
            bool foundSlot = false;
            if (newMap != null)
            {
                foreach (var (dx, dy) in SummonScatterOffsets)
                {
                    int candX = leader.X + dx;
                    int candY = leader.Y + dy;
                    if (!occupiedPositions.Contains((candX, candY)) && 
                        newMap.IsWalkable(candX, candY) && 
                        !newMap.HasEntityCollision(candX, candY, member.SessionId))
                    {
                        spawnX = candX;
                        spawnY = candY;
                        occupiedPositions.Add((candX, candY));
                        foundSlot = true;
                        break;
                    }
                }
            }

            if (!foundSlot)
            {
                occupiedPositions.Add((spawnX, spawnY));
            }

            var oldMap = mapManager?.GetMap(member.CurrentMapId);
            int oldX = member.X;
            int oldY = member.Y;

            if (newMap != null)
            {
                if (oldMap != null)
                {
                    oldMap.Aoi.Leave(member, oldX, oldY);
                    var leaveBuf = new List<ClientSession>();
                    oldMap.Aoi.GetEntitiesInNinePatch(oldX, oldY, leaveBuf);
                    foreach (var nearby in leaveBuf)
                    {
                        if (nearby != member)
                        {
                            nearby.EnqueuePacket(ProtocolConsts.SM_ENTITYDISAPPEAR, member.SessionId, oldX, oldY, 0, member.CharacterName);
                        }
                    }
                }

                member.CurrentMapId = newMap.MapId;
                member.X = spawnX;
                member.Y = spawnY;

                member.EnqueuePacket(ProtocolConsts.SM_NEWMAP_DELPHI, newMap.MapId, spawnX, spawnY, 0);
                member.EnqueuePacket(ProtocolConsts.SM_SENDUSERSTATE, member.SessionId, spawnX, spawnY, member.Direction, member.CharacterName);

                newMap.Aoi.Enter(member, spawnX, spawnY);
                var enterBuf = new List<ClientSession>();
                newMap.Aoi.GetEntitiesInNinePatch(spawnX, spawnY, enterBuf);
                foreach (var nearby in enterBuf)
                {
                    if (nearby != member)
                    {
                        nearby.EnqueuePacket(ProtocolConsts.SM_ENTITYAPPEAR, member.SessionId, spawnX, spawnY, member.Direction, member.CharacterName);
                    }
                }
            }
            else
            {
                member.CurrentMapId = targetMapId;
                member.X = spawnX;
                member.Y = spawnY;
                member.EnqueuePacket(ProtocolConsts.SM_NEWMAP_DELPHI, targetMapId, spawnX, spawnY, 0);
            }

            // 随从宝宝 (Pets) 同步拉扯至主人身边
            if (member.Pets != null && member.Pets.Count > 0)
            {
                foreach (var pet in member.Pets)
                {
                    if (pet.IsDead) continue;
                    int petOldX = pet.X;
                    int petOldY = pet.Y;
                    if (oldMap != null)
                    {
                        var petLeaveBuf = new List<ClientSession>();
                        oldMap.Aoi.GetEntitiesInNinePatch(petOldX, petOldY, petLeaveBuf);
                        foreach (var nearby in petLeaveBuf)
                        {
                            nearby.EnqueuePacket(ProtocolConsts.SM_ENTITYDISAPPEAR, pet.Uid, petOldX, petOldY, 0, pet.Name);
                        }
                    }

                    HuWeiPetEngine.SyncPetTeleportWithMaster(pet, regenEngine?.FindMonsterByUid(pet.Uid), targetMapId, spawnX, spawnY);

                    if (newMap != null)
                    {
                        var petEnterBuf = new List<ClientSession>();
                        newMap.Aoi.GetEntitiesInNinePatch(pet.X, pet.Y, petEnterBuf);
                        foreach (var nearby in petEnterBuf)
                        {
                            nearby.EnqueuePacket(ProtocolConsts.SM_ENTITYAPPEAR, pet.Uid, pet.X, pet.Y, 0, pet.Name);
                        }
                    }
                }
            }

            member.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "队长施展了【天地合一】，你已被时空法阵召唤至队长身边！");
            count++;
        }

        if (count > 0)
        {
            leader.State.LastMemoryRecallTickMs = nowMs;
        }

        if (count == 0 && refusedCount > 0)
        {
            return (false, 0, $"召唤未生效：队伍中 {refusedCount} 名队友均已设置【拒绝天地合一】！");
        }

        string extraRefusedNote = refusedCount > 0 ? $" (另有 {refusedCount} 名队友设置了拒绝天地合一)" : string.Empty;
        return (true, count, $"天地合一！成功召唤了队伍中的 {count} 名队友！{extraRefusedNote}");
    }
}
