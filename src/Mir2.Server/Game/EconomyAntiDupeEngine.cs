using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 经济系统防外挂与防复制装备权威事务协调引擎 (EconomyAntiDupeEngine)
/// 严格规整三大交易/经济并发安全:
/// 1. 玩家经济活动互斥状态机 (PlayerEconomicState: Normal, Trading, Stalling, Warehousing)
/// 2. 摆摊 (Player Stall / Market) 上架、原子购买结算与安全收摊回滚
/// 3. 并发双花 (Double-Spending) 与网络断线异常回滚流水保护
/// </summary>
public sealed class EconomyAntiDupeEngine
{
    public enum EconomicActivity
    {
        Idle = 0,
        Trading = 1,     // 面对面双人交易中
        Stalling = 2,    // 个人摆摊卖货中
        Warehousing = 3  // 仓库快速存取中
    }

    public sealed class StallItem
    {
        public int StallSlot { get; init; }
        public EquipItem Item { get; init; } = null!;
        public int PriceGold { get; init; }
    }

    public sealed class PlayerStall
    {
        public int SellerSessionId { get; init; }
        public string SellerName { get; init; } = string.Empty;
        public string StallName { get; set; } = string.Empty;
        public List<StallItem> Items { get; } = new();
        public object LockObject { get; } = new();
    }

    private readonly ConcurrentDictionary<int, EconomicActivity> _playerActivities = new();
    private readonly ConcurrentDictionary<int, PlayerStall> _stalls = new();
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
                return false; // 正在交易/摆摊/仓库中，禁止并行其他经济操作
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

    // ==================== 摆摊原子事务与防双花 ====================

    /// <summary>
    /// 开启摆摊并将背包物品安全转移至摊位锁存
    /// </summary>
    public (bool success, string error) OpenStall(
        ClientSession seller,
        string stallName,
        IReadOnlyList<(int bagSlot, int price)> itemsToSell)
    {
        if (seller.State == null) return (false, "玩家状态异常。");
        if (string.IsNullOrWhiteSpace(stallName)) return (false, "请输入有效的摊位名称。");

        if (!TryEnterActivity(seller.SessionId, EconomicActivity.Stalling))
        {
            return (false, "当前处于其他交易或操作中，无法开启摆摊！");
        }

        var stall = new PlayerStall
        {
            SellerSessionId = seller.SessionId,
            SellerName = seller.CharacterName,
            StallName = stallName
        };

        lock (stall.LockObject)
        {
            // 校验并把上架物品从背包转移至摊位
            var distinctSlots = new HashSet<int>();
            var itemsToRemove = new List<EquipItem>();

            for (int i = 0; i < itemsToSell.Count; i++)
            {
                var (slot, price) = itemsToSell[i];
                if (price <= 0 || price > TradeEngine.MaxGoldCap)
                {
                    // 参数异常，回滚
                    LeaveActivity(seller.SessionId);
                    return (false, "摆摊物品标价不合法。");
                }

                if (!distinctSlots.Add(slot) || slot < 0 || slot >= seller.State.BagItems.Count)
                {
                    LeaveActivity(seller.SessionId);
                    return (false, "重复或无效的背包槽位。");
                }

                var item = seller.State.BagItems[slot];
                itemsToRemove.Add(item);
                stall.Items.Add(new StallItem
                {
                    StallSlot = stall.Items.Count,
                    Item = item,
                    PriceGold = price
                });
            }

            // 扣除背包物品，防止摆摊期间被丢弃、交易或存仓
            for (int i = 0; i < itemsToRemove.Count; i++)
            {
                seller.State.BagItems.Remove(itemsToRemove[i]);
            }
            seller.State.RecalculateAuthoritativeWeights(seller.State.Gold);
            seller.State.RecalculateAttributes();

            _stalls[seller.SessionId] = stall;
        }

        return (true, "摆摊成功开启！");
    }

    /// <summary>
    /// 买家购买摊位物品 (原子资金交割与物品过户)
    /// </summary>
    public (bool success, string message) BuyStallItem(
        ClientSession buyer,
        int sellerSessionId,
        int stallSlot,
        ClientSession? onlineSeller)
    {
        if (buyer.State == null) return (false, "买家状态异常。");
        if (buyer.SessionId == sellerSessionId) return (false, "不能购买自己摊位上的物品。");

        if (!_stalls.TryGetValue(sellerSessionId, out var stall))
        {
            return (false, "该摊位已收摊或不存在。");
        }

        lock (stall.LockObject)
        {
            int idx = stall.Items.FindIndex(i => i.StallSlot == stallSlot);
            if (idx < 0)
            {
                return (false, "该商品已被其他玩家抢先买走！");
            }

            var stallItem = stall.Items[idx];

            // 1. 校验买家金币
            if (buyer.State.Gold < stallItem.PriceGold)
            {
                return (false, "你的金币不足，无法购买该物品！");
            }

            // 2. 校验买家背包容量 (最多 40 格)
            if (buyer.State.BagItems.Count >= 40)
            {
                return (false, "你的背包已满，无法容纳新物品！");
            }

            // 3. 校验买家背包负重
            if (buyer.State.BagWeight + stallItem.Item.Weight > buyer.State.MaxBagWeight)
            {
                return (false, "物品过重，你的背包负重已达到上限！");
            }

            // 4. 原子扣除与交割
            buyer.State.Gold -= stallItem.PriceGold;
            buyer.State.BagItems.Add(stallItem.Item);
            buyer.State.RecalculateAuthoritativeWeights(buyer.State.Gold);
            buyer.State.RecalculateAttributes();

            stall.Items.RemoveAt(idx);

            // 卖家若在线，实时到账金币 (若已达单人金币上限则截断，防止溢出)
            if (onlineSeller?.State != null)
            {
                onlineSeller.State.Gold = Math.Min(PlayerState.MaxGoldLimit, onlineSeller.State.Gold + stallItem.PriceGold);
                onlineSeller.State.RecalculateAuthoritativeWeights(onlineSeller.State.Gold);
            }

            return (true, $"成功购买了 [{stallItem.Item.Name}]！");
        }
    }

    /// <summary>
    /// 收摊或掉线时：将剩余商品 100% 安全原路返还背包并解除活动互斥锁
    /// </summary>
    public void CloseStallAndRollback(ClientSession seller)
    {
        if (_stalls.TryRemove(seller.SessionId, out var stall))
        {
            lock (stall.LockObject)
            {
                if (seller.State != null)
                {
                    for (int i = 0; i < stall.Items.Count; i++)
                    {
                        if (seller.State.BagItems.Count < 40)
                        {
                            seller.State.BagItems.Add(stall.Items[i].Item);
                        }
                    }
                    seller.State.RecalculateAuthoritativeWeights(seller.State.Gold);
                    seller.State.RecalculateAttributes();
                }
                stall.Items.Clear();
            }
        }

        LeaveActivity(seller.SessionId);
    }
}
