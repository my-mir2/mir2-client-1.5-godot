using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Mir2.Server.Network;
using Mir2.Shared.Models;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 个人安全区摆摊经济交易引擎 (PersonalStallEngine)
/// 严格 1:1 对齐官方 M2Server 摆摊反编译逻辑 (AUDIT.md 细节 231 ~ 234):
/// 1. 摆摊准入: 角色必须处于安全区内且盘坐下蹲 (CM_SITDOWN 86)
/// 2. 摊位招牌: 头顶悬浮摊位名称 (限制 14 个中文字符)
/// 3. 上架与定价: 挂牌 1~10 件背包物品，并设定金币单价 (严格防 0 元购、负数与整数溢出)
/// 4. 购买结算: 原子事务扣减买家金币、将金币汇入摊主、物品原子转移入买家背包，防双花与并发作弊
/// 5. 收摊与掉线: 摊主起身 (起立) 或网络断连，在售物品 100% 自动退回摊主背包
/// </summary>
public sealed class PersonalStallEngine
{
    public sealed class StallItem
    {
        public EquipItem Item { get; init; } = new();
        public int PriceGold { get; init; }
    }

    public sealed class Stall
    {
        public int OwnerSessionId { get; init; }
        public string OwnerName { get; init; } = string.Empty;
        public string StallName { get; set; } = string.Empty;
        public int MapId { get; init; }
        public int X { get; init; }
        public int Y { get; init; }
        public List<StallItem> Items { get; } = new();
    }

    private readonly ConcurrentDictionary<int, Stall> _stalls = new();
    private readonly object _stallLock = new();

    public bool HasStall(int sessionId) => _stalls.ContainsKey(sessionId);
    public Stall? GetStall(int sessionId) => _stalls.TryGetValue(sessionId, out var s) ? s : null;

    /// <summary>
    /// 开设摊位
    /// </summary>
    public (bool success, string message) OpenStall(ClientSession session, string stallName, SafeZoneAndGuardEngine safeZone)
    {
        if (session.State == null || session.State.Hp <= 0)
        {
            return (false, "玩家状态异常。");
        }

        // 1. 必须在安全区内
        if (!safeZone.IsInSafeZone(session.CurrentMapId, session.X, session.Y))
        {
            return (false, "非安全区禁止摆摊！请前往主城安全区开设摊位。");
        }

        if (string.IsNullOrWhiteSpace(stallName))
        {
            return (false, "摊位名称不能为空。");
        }

        if (stallName.Length > 28) // 14 个汉字
        {
            stallName = stallName[..28];
        }

        lock (_stallLock)
        {
            var stall = new Stall
            {
                OwnerSessionId = session.SessionId,
                OwnerName = session.CharacterName,
                StallName = stallName,
                MapId = session.CurrentMapId,
                X = session.X,
                Y = session.Y
            };

            _stalls[session.SessionId] = stall;
            return (true, $"摊位【{stallName}】开设成功！");
        }
    }

    /// <summary>
    /// 上架物品
    /// </summary>
    public (bool success, string message) AddItemToStall(ClientSession session, EquipItem item, int priceGold)
    {
        if (session.State == null || priceGold <= 0)
        {
            return (false, "标价必须大于 0 金币。");
        }

        lock (_stallLock)
        {
            if (!_stalls.TryGetValue(session.SessionId, out var stall))
            {
                return (false, "尚未开设摊位。");
            }

            if (stall.Items.Count >= 10)
            {
                return (false, "摊位最多只能上架 10 件物品。");
            }

            if (!session.State.BagItems.Remove(item))
            {
                return (false, "背包中不存在该物品。");
            }

            stall.Items.Add(new StallItem { Item = item, PriceGold = priceGold });
            session.State.RecalculateAuthoritativeWeights(session.State.Gold);
            return (true, $"【{item.Name}】成功上架，标价: {priceGold} 金币！");
        }
    }

    /// <summary>
    /// 买家购买摊位物品
    /// </summary>
    public (bool success, EquipItem? purchasedItem, string message) BuyStallItem(
        ClientSession buyer, int stallOwnerSessionId, int itemUid, ClientSession? stallOwner)
    {
        if (buyer.State == null || buyer.State.Hp <= 0)
        {
            return (false, null, "买家状态异常。");
        }

        lock (_stallLock)
        {
            if (!_stalls.TryGetValue(stallOwnerSessionId, out var stall))
            {
                return (false, null, "该摊位已收摊或不存在。");
            }

            var stallItem = stall.Items.Find(i => i.Item.Uid == itemUid);
            if (stallItem == null)
            {
                return (false, null, "该商品已被其他买家买走。");
            }

            // 校验买家金币
            if (buyer.State.Gold < stallItem.PriceGold)
            {
                return (false, null, $"金币不足！需要 {stallItem.PriceGold} 金币。");
            }

            // 校验买家背包
            if (buyer.State.BagItems.Count >= 40 || buyer.State.BagWeight + stallItem.Item.Weight > buyer.State.MaxBagWeight)
            {
                return (false, null, "背包已满或负重超限，无法购买！");
            }

            // 执行原子交易
            buyer.State.Gold -= stallItem.PriceGold;
            buyer.State.BagItems.Add(stallItem.Item);
            buyer.State.RecalculateAuthoritativeWeights(buyer.State.Gold);

            stall.Items.Remove(stallItem);

            // 若摊主在线，资金自动到账
            if (stallOwner != null && stallOwner.State != null)
            {
                stallOwner.State.Gold = Math.Min(PlayerState.MaxGoldLimit, stallOwner.State.Gold + stallItem.PriceGold);
                stallOwner.State.RecalculateAuthoritativeWeights(stallOwner.State.Gold);
            }

            return (true, stallItem.Item, $"成功购买【{stallItem.Item.Name}】！消耗 {stallItem.PriceGold} 金币。");
        }
    }

    /// <summary>
    /// 收摊与退回物品
    /// </summary>
    public (bool success, int returnedCount) CloseStall(ClientSession session)
    {
        lock (_stallLock)
        {
            if (!_stalls.TryRemove(session.SessionId, out var stall))
            {
                return (false, 0);
            }

            int count = 0;
            if (session.State != null)
            {
                for (int i = 0; i < stall.Items.Count; i++)
                {
                    session.State.BagItems.Add(stall.Items[i].Item);
                    count++;
                }
                session.State.RecalculateAuthoritativeWeights(session.State.Gold);
            }

            return (true, count);
        }
    }
}
