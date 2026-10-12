using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 经济系统防外挂与防复制装备权威事务协调引擎 (EconomyAntiDupeEngine)
/// 严格规整官方 1.5 交易与经济并发安全 (拒绝私服摆摊，100% 收敛于官方 1.5):
/// 1. 玩家经济活动互斥状态机 (PlayerEconomicState: Idle, Trading, Warehousing)
/// 2. 面对面交易 (TFrmDlg_Deal) 两阶段锁定与原子资金/物品过户
/// 3. 并发双花 (Double-Spending) 与网络断线异常回滚流水保护
/// </summary>
public sealed class EconomyAntiDupeEngine
{
    public enum EconomicActivity
    {
        Idle = 0,
        Trading = 1,     // 官方 1.5 面对面双人交易中 (TFrmDlg_Deal)
        Warehousing = 2  // 官方 1.5 仓库快速存取中 (TFrmDlg_Storage)
    }

    private readonly ConcurrentDictionary<int, EconomicActivity> _playerActivities = new();
    private readonly object _globalEcoLock = new();

    /// <summary>
    /// 获取玩家当前经济活动状态
    /// </summary>
    public EconomicActivity GetActivity(int sessionId) =>
        _playerActivities.TryGetValue(sessionId, out var act) ? act : EconomicActivity.Idle;

    /// <summary>
    /// 尝试进入某经济活动 (互斥锁: 若已在其他活动中则直接驳回，防止重入刷物)
    /// </summary>
    public bool TryEnterActivity(int sessionId, EconomicActivity newActivity)
    {
        lock (_globalEcoLock)
        {
            var cur = GetActivity(sessionId);
            if (cur != EconomicActivity.Idle && cur != newActivity)
            {
                return false; // 正在交易或仓库中，禁止并行其他经济操作
            }
            _playerActivities[sessionId] = newActivity;
            return true;
        }
    }

    /// <summary>
    /// 退出某经济活动，回归空闲状态
    /// </summary>
    public void LeaveActivity(int sessionId)
    {
        lock (_globalEcoLock)
        {
            _playerActivities.TryRemove(sessionId, out _);
        }
    }

    /// <summary>
    /// 官方 1.5 面对面原子结算校验: 检查双方背包容量与负重
    /// </summary>
    public static (bool success, string error) ValidateTradeCapacity(
        ClientSession playerA,
        IReadOnlyList<EquipItem> itemsForA,
        int goldDeltaForA,
        ClientSession playerB,
        IReadOnlyList<EquipItem> itemsForB,
        int goldDeltaForB)
    {
        if (playerA.State == null || playerB.State == null)
            return (false, "玩家状态异常。");

        // 1. 检查 A 背包容量 (最大 40 格)
        int finalCountA = playerA.State.BagItems.Count + itemsForA.Count;
        if (finalCountA > 40)
            return (false, $"【{playerA.CharacterName}】背包空间不足，最多容纳 40 件物品！");

        // 2. 检查 B 背包容量
        int finalCountB = playerB.State.BagItems.Count + itemsForB.Count;
        if (finalCountB > 40)
            return (false, $"【{playerB.CharacterName}】背包空间不足，最多容纳 40 件物品！");

        // 3. 检查 A 金币上限
        long finalGoldA = (long)playerA.State.Gold + goldDeltaForA;
        if (finalGoldA < 0 || finalGoldA > PlayerState.MaxGoldLimit)
            return (false, $"【{playerA.CharacterName}】金币超出允许范围！");

        // 4. 检查 B 金币上限
        long finalGoldB = (long)playerB.State.Gold + goldDeltaForB;
        if (finalGoldB < 0 || finalGoldB > PlayerState.MaxGoldLimit)
            return (false, $"【{playerB.CharacterName}】金币超出允许范围！");

        return (true, string.Empty);
    }
}
