using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;
using Mir2.Shared.Rules;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 面对面双人安全交易会话模型
/// 对齐 IDA 0x4DDC8C (TPlayObject.Deal)
/// </summary>
public sealed class ActiveTradeContext
{
    public ClientSession PlayerA { get; }
    public ClientSession PlayerB { get; }

    public List<EquipItem> ItemsA { get; } = new();
    public List<EquipItem> ItemsB { get; } = new();

    public int GoldA { get; set; }
    public int GoldB { get; set; }

    public bool LockedA { get; set; }
    public bool LockedB { get; set; }

    public long LastChangeTickA { get; set; }
    public long LastChangeTickB { get; set; }

    public ActiveTradeContext(ClientSession a, ClientSession b)
    {
        PlayerA = a;
        PlayerB = b;
        long now = Environment.TickCount64;
        LastChangeTickA = now;
        LastChangeTickB = now;
    }

    public bool IsParticipant(int sessionId) =>
        PlayerA.SessionId == sessionId || PlayerB.SessionId == sessionId;

    public (ClientSession me, ClientSession partner, List<EquipItem> myItems, List<EquipItem> partnerItems, bool isPlayerA) GetSides(int sessionId)
    {
        if (PlayerA.SessionId == sessionId)
        {
            return (PlayerA, PlayerB, ItemsA, ItemsB, isPlayerA: true);
        }
        return (PlayerB, PlayerA, ItemsB, ItemsA, isPlayerA: false);
    }
}

/// <summary>
/// 盛大官方 1.4/1.5 面对面双人安全交易核心引擎 (TradeEngine)
/// 严格 1:1 对齐官方 M2Server 物理反编译代码:
/// - 1000ms 防快连点防刷物强制取消门限: sub_4DDC8C @ 0x4DDCF5 ("Hit Deal Too Fast!")
/// - 背包 46 格与负重双向四重安全检定: sub_4DDC8C @ 0x4DDD52 ~ 0x4DDDD1
/// - 金币 1000 万 (0x989680) 上限溢出熔断: sub_4DDC8C @ 0x4DDD74
/// - 双向锁定与原子交换交割: sub_4DDC8C @ 0x4DDE08 ~ 0x4DE432
/// - 取消交易原路安全退回: sub_4DD394 / sub_4DD270
/// </summary>
public sealed class TradeEngine
{
    public const int MaxGoldCap = 10_000_000; // 官方 1000 万金币上限 (0x989680)
    public const int MaxBagCapacity = 40;     // 官方客户端 1:1 实际 40 格背包硬上限 (防止越界溢出)
    public const int AntiFastDealThresholdMs = 1000; // 官方 1000ms 防快速连点门限 (0x4DDCF5)

    private readonly ConcurrentDictionary<int, ActiveTradeContext> _tradesBySession = new();
    public Persistence.WriteAheadLogEngine? WalEngine { get; set; }

    public ActiveTradeContext? GetTrade(int sessionId)
    {
        _tradesBySession.TryGetValue(sessionId, out var ctx);
        return ctx;
    }

    /// <summary>
    /// 发起面对面交易请求 (CM_DEALTRY 1025)
    /// </summary>
    public (bool success, string errorMsg) TryInitiateTrade(ClientSession requester, ClientSession target)
    {
        if (requester.State == null || requester.State.Hp <= 0) return (false, "自身处于死亡或异常状态");
        if (target.State == null || target.State.Hp <= 0) return (false, "对方处于死亡或异常状态");

        if (_tradesBySession.ContainsKey(requester.SessionId)) return (false, "自身正在进行其他交易");
        if (_tradesBySession.ContainsKey(target.SessionId)) return (false, "对方正在进行其他交易");

        // 验证切比雪夫距离必须为 1 格 (面对面邻接)
        int dist = Math.Max(Math.Abs(requester.X - target.X), Math.Abs(requester.Y - target.Y));
        if (dist != 1 || requester.CurrentMapId != target.CurrentMapId)
        {
            return (false, "双方必须面对面站立 (距离1格) 方可发起交易");
        }

        var context = new ActiveTradeContext(requester, target);
        _tradesBySession[requester.SessionId] = context;
        _tradesBySession[target.SessionId] = context;

        // 双方弹出交易窗口: SM_DEALMENU (673)
        requester.EnqueuePacket(ProtocolConsts.SM_DEALMENU, target.SessionId, 0, 0, 0, target.CharacterName);
        target.EnqueuePacket(ProtocolConsts.SM_DEALMENU, requester.SessionId, 0, 0, 0, requester.CharacterName);

        return (true, string.Empty);
    }

    /// <summary>
    /// 放入交易物品 (CM_DEALADDITEM 1026)
    /// 智能支持按 itemUid、背包槽位 (slot) 或物品名称检索放入
    /// </summary>
    public (bool success, string errorMsg) AddItem(ClientSession session, int itemUidOrSlot, string? itemName = null)
    {
        var ctx = GetTrade(session.SessionId);
        if (ctx == null) return (false, "未处于交易中");

        var (me, partner, myItems, _, isA) = ctx.GetSides(session.SessionId);
        if (isA ? ctx.LockedA : ctx.LockedB) return (false, "交易已锁定，禁止添加物品");

        if (me.State == null) return (false, "玩家状态异常");

        // 1. 优先按 Uid 查找
        int bagIdx = me.State.BagItems.FindIndex(i => i.Uid == itemUidOrSlot);
        // 2. 若未按 Uid 查到且 itemUidOrSlot 在合法槽位范围内 (0..Count-1)
        if (bagIdx < 0 && itemUidOrSlot >= 0 && itemUidOrSlot < me.State.BagItems.Count)
        {
            bagIdx = itemUidOrSlot;
        }
        // 3. 若提供物品名称，按物品名查找
        if (bagIdx < 0 && !string.IsNullOrWhiteSpace(itemName))
        {
            string searchName = itemName.Trim();
            bagIdx = me.State.BagItems.FindIndex(i => i.Name.Equals(searchName, StringComparison.OrdinalIgnoreCase) || i.Name.Contains(searchName));
        }

        if (bagIdx < 0) return (false, "背包中不存在该物品");

        var item = me.State.BagItems[bagIdx];
        me.State.BagItems.RemoveAt(bagIdx);
        me.State.RecalculateAuthoritativeWeights(me.State.Gold);

        myItems.Add(item);
        long now = Environment.TickCount64;
        if (isA) ctx.LastChangeTickA = now; else ctx.LastChangeTickB = now;

        // 己方确认: SM_DEALADDITEM_OK (675)
        me.EnqueuePacket(ProtocolConsts.SM_DEALADDITEM_OK, item.Uid, item.Weight, 0, 0, item.Name);
        me.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, me.State.BagWeight, me.State.WearWeight, me.State.HandWeight, 0);
        // 对方同步: SM_DEALREMOTEADDITEM (682)
        partner.EnqueuePacket(ProtocolConsts.SM_DEALREMOTEADDITEM, item.Uid, item.Weight, 0, 0, item.Name);

        return (true, string.Empty);
    }

    /// <summary>
    /// 取下交易物品 (CM_DEALDELITEM 1027)
    /// 智能支持按 itemUid、交易栏索引或物品名称检索取下
    /// </summary>
    public (bool success, string errorMsg) DelItem(ClientSession session, int itemUidOrIndex, string? itemName = null)
    {
        var ctx = GetTrade(session.SessionId);
        if (ctx == null) return (false, "未处于交易中");

        var (me, partner, myItems, _, isA) = ctx.GetSides(session.SessionId);
        if (isA ? ctx.LockedA : ctx.LockedB) return (false, "交易已锁定，禁止取下物品");

        if (me.State == null) return (false, "玩家状态异常");

        int idx = myItems.FindIndex(i => i.Uid == itemUidOrIndex);
        if (idx < 0 && itemUidOrIndex >= 0 && itemUidOrIndex < myItems.Count)
        {
            idx = itemUidOrIndex;
        }
        if (idx < 0 && !string.IsNullOrWhiteSpace(itemName))
        {
            string searchName = itemName.Trim();
            idx = myItems.FindIndex(i => i.Name.Equals(searchName, StringComparison.OrdinalIgnoreCase) || i.Name.Contains(searchName));
        }

        if (idx < 0) return (false, "交易栏中无此物品");

        var item = myItems[idx];
        myItems.RemoveAt(idx);

        me.State.BagItems.Add(item);
        me.State.RecalculateAuthoritativeWeights(me.State.Gold);

        long now = Environment.TickCount64;
        if (isA) ctx.LastChangeTickA = now; else ctx.LastChangeTickB = now;

        // 己方确认: SM_DEALDELITEM_OK (677)
        me.EnqueuePacket(ProtocolConsts.SM_DEALDELITEM_OK, item.Uid, 0, 0, 0, item.Name);
        me.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, me.State.BagWeight, me.State.WearWeight, me.State.HandWeight, 0);
        // 对方同步: SM_DEALREMOTEDELITEM (683)
        partner.EnqueuePacket(ProtocolConsts.SM_DEALREMOTEDELITEM, item.Uid, 0, 0, 0, item.Name);

        return (true, string.Empty);
    }

    /// <summary>
    /// 修改交易金币数量 (CM_DEALCHGGOLD 1029)
    /// </summary>
    public (bool success, string errorMsg) ChangeGold(ClientSession session, int gold)
    {
        var ctx = GetTrade(session.SessionId);
        if (ctx == null) return (false, "未处于交易中");

        var (me, partner, _, _, isA) = ctx.GetSides(session.SessionId);
        if (isA ? ctx.LockedA : ctx.LockedB) return (false, "交易已锁定，禁止修改金币");

        if (gold < 0 || gold > me.State!.Gold || gold > MaxGoldCap)
        {
            return (false, "金币数额不合法或超出持有上限");
        }

        if (isA) ctx.GoldA = gold; else ctx.GoldB = gold;

        long now = Environment.TickCount64;
        if (isA) ctx.LastChangeTickA = now; else ctx.LastChangeTickB = now;

        // 己方确认与对方同步
        partner.EnqueuePacket(ProtocolConsts.SM_DEALREMOTECHGGOLD, gold, 0, 0, 0);

        return (true, string.Empty);
    }

    /// <summary>
    /// 点击确认/锁定交易 (CM_DEALEND 1030)
    /// 包含官方 1000ms 防快连点强制取消与四重满包/负重/金币上限安全熔断
    /// </summary>
    public (bool complete, bool canceled, string reason) ConfirmDeal(ClientSession session)
    {
        var ctx = GetTrade(session.SessionId);
        if (ctx == null) return (false, true, "未处于交易中");

        long now = Environment.TickCount64;
        var (me, partner, _, _, isA) = ctx.GetSides(session.SessionId);
        long lastChange = isA ? ctx.LastChangeTickA : ctx.LastChangeTickB;

        // 1. 官方 1000ms 防快刷连击门限 (IDA sub_4DDC8C @ 0x4DDCF5)
        // 若在变动 1000ms 内连点确认，判定作弊，强制取消整个交易并退回物品！
        if (now - lastChange < AntiFastDealThresholdMs)
        {
            CancelTrade(session, "Hit Deal Too Fast!");
            return (false, true, "Hit Deal Too Fast!");
        }

        // 2. 单方锁定状态设置 (0x4DDD0A)
        if (isA) ctx.LockedA = true; else ctx.LockedB = true;

        // 向对方广播锁定状态: SM_DEALLOCKED (678)
        partner.EnqueuePacket(ProtocolConsts.SM_DEALLOCKED, me.SessionId, 0, 0, 0, $"{me.CharacterName} 已锁定交易栏");

        // 3. 双方均已确认锁定，执行终极原子交割 (0x4DDD52 ~ 0x4DDE08)
        if (ctx.LockedA && ctx.LockedB)
        {
            // 四重安全检定:
            // ① 双方剩余背包格子 >= 对方交易物品数 (46格防撑爆)
            if (MaxBagCapacity - ctx.PlayerA.State!.BagItems.Count < ctx.ItemsB.Count ||
                MaxBagCapacity - ctx.PlayerB.State!.BagItems.Count < ctx.ItemsA.Count)
            {
                CancelTrade(session, "一方背包空间不足以容纳交易物品！");
                return (false, true, "一方背包空间不足");
            }

            // ② 双方剩余负重 >= 对方物品与金币净增总重量
            int weightFromB = 0; foreach (var it in ctx.ItemsB) weightFromB += it.Weight;
            int weightFromA = 0; foreach (var it in ctx.ItemsA) weightFromA += it.Weight;

            int newGoldA = ctx.PlayerA.State.Gold - ctx.GoldA + ctx.GoldB;
            int newGoldB = ctx.PlayerB.State.Gold - ctx.GoldB + ctx.GoldA;

            int goldDeltaWeightA = WeightConstraintEngine.CalcGoldWeight(newGoldA) - WeightConstraintEngine.CalcGoldWeight(ctx.PlayerA.State.Gold);
            int goldDeltaWeightB = WeightConstraintEngine.CalcGoldWeight(newGoldB) - WeightConstraintEngine.CalcGoldWeight(ctx.PlayerB.State.Gold);

            if (ctx.PlayerA.State.BagWeight + weightFromB + goldDeltaWeightA > ctx.PlayerA.State.MaxBagWeight ||
                ctx.PlayerB.State.BagWeight + weightFromA + goldDeltaWeightB > ctx.PlayerB.State.MaxBagWeight)
            {
                CancelTrade(session, "一方负重不足以承载交易物品或金币！");
                return (false, true, "一方负重超限");
            }

            // ③ 金币防溢出 (1000万上限)
            if (ctx.PlayerA.State.Gold - ctx.GoldA + ctx.GoldB > MaxGoldCap ||
                ctx.PlayerB.State.Gold - ctx.GoldB + ctx.GoldA > MaxGoldCap)
            {
                CancelTrade(session, "交易导致金币超出 1000 万上限！");
                return (false, true, "金币超出上限");
            }

            // ④ 全部检定通过，原子双向资产交割！
            // 移交金币
            ctx.PlayerA.State.Gold = newGoldA;
            ctx.PlayerB.State.Gold = newGoldB;

            // 预写 WAL 物理事务日志 (ACID 防断电)
            if (WalEngine != null)
            {
                foreach (var item in ctx.ItemsB)
                {
                    WalEngine.LogItemTrade(ctx.PlayerB.CharacterName, ctx.PlayerA.CharacterName, item.Id, item.Name, ctx.GoldB, syncToDisk: true);
                }
                foreach (var item in ctx.ItemsA)
                {
                    WalEngine.LogItemTrade(ctx.PlayerA.CharacterName, ctx.PlayerB.CharacterName, item.Id, item.Name, ctx.GoldA, syncToDisk: true);
                }
                WalEngine.CheckAndTriggerAutoCheckpoint();
            }

            // 移交物品到对方背包并向客户端下发入包封包 SM_ADDITEM (200)
            foreach (var item in ctx.ItemsB)
            {
                ctx.PlayerA.State.BagItems.Add(item);
                ctx.PlayerA.EnqueuePacket(ProtocolConsts.SM_ADDITEM, item.Id, item.Weight, 0, 0, item.Name);
            }
            foreach (var item in ctx.ItemsA)
            {
                ctx.PlayerB.State.BagItems.Add(item);
                ctx.PlayerB.EnqueuePacket(ProtocolConsts.SM_ADDITEM, item.Id, item.Weight, 0, 0, item.Name);
            }

            // 权威重算并同步双方物理负重
            ctx.PlayerA.State.RecalculateAuthoritativeWeights(ctx.PlayerA.State.Gold);
            ctx.PlayerB.State.RecalculateAuthoritativeWeights(ctx.PlayerB.State.Gold);

            // 清理交易上下文
            _tradesBySession.TryRemove(ctx.PlayerA.SessionId, out _);
            _tradesBySession.TryRemove(ctx.PlayerB.SessionId, out _);

            // 双方下发交易最终完成: SM_DEALSUCCESS (687)
            ctx.PlayerA.EnqueuePacket(ProtocolConsts.SM_DEALSUCCESS, ctx.PlayerB.SessionId, 0, 0, 0, "Successful");
            ctx.PlayerB.EnqueuePacket(ProtocolConsts.SM_DEALSUCCESS, ctx.PlayerA.SessionId, 0, 0, 0, "Successful");

            // 刷新金币
            ctx.PlayerA.EnqueuePacket(ProtocolConsts.SM_GOLDCHANGED_DELPHI, ctx.PlayerA.State.Gold, 0, 0, 0);
            ctx.PlayerB.EnqueuePacket(ProtocolConsts.SM_GOLDCHANGED_DELPHI, ctx.PlayerB.State.Gold, 0, 0, 0);

            // 同步物理负重
            ctx.PlayerA.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, ctx.PlayerA.State.BagWeight, ctx.PlayerA.State.WearWeight, ctx.PlayerA.State.HandWeight, 0);
            ctx.PlayerB.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, ctx.PlayerB.State.BagWeight, ctx.PlayerB.State.WearWeight, ctx.PlayerB.State.HandWeight, 0);

            return (complete: true, canceled: false, "Successful");
        }

        return (complete: false, canceled: false, "Waiting for partner");
    }

    /// <summary>
    /// 取消交易并原路安全退回所有物品与金币 (CM_DEALCANCEL 1025 / sub_4DD394)
    /// </summary>
    public void CancelTrade(ClientSession session, string reason = "Canceled")
    {
        if (_tradesBySession.TryRemove(session.SessionId, out var ctx))
        {
            _tradesBySession.TryRemove(ctx.PlayerA.SessionId, out _);
            _tradesBySession.TryRemove(ctx.PlayerB.SessionId, out _);

            // 原路退回 A 的物品并同步背包与负重
            foreach (var item in ctx.ItemsA)
            {
                ctx.PlayerA.State?.BagItems.Add(item);
                ctx.PlayerA.EnqueuePacket(ProtocolConsts.SM_ADDITEM, item.Id, item.Weight, 0, 0, item.Name);
            }
            if (ctx.PlayerA.State != null)
            {
                ctx.PlayerA.State.RecalculateAuthoritativeWeights(ctx.PlayerA.State.Gold);
                ctx.PlayerA.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, ctx.PlayerA.State.BagWeight, ctx.PlayerA.State.WearWeight, ctx.PlayerA.State.HandWeight, 0);
            }

            // 原路退回 B 的物品并同步背包与负重
            foreach (var item in ctx.ItemsB)
            {
                ctx.PlayerB.State?.BagItems.Add(item);
                ctx.PlayerB.EnqueuePacket(ProtocolConsts.SM_ADDITEM, item.Id, item.Weight, 0, 0, item.Name);
            }
            if (ctx.PlayerB.State != null)
            {
                ctx.PlayerB.State.RecalculateAuthoritativeWeights(ctx.PlayerB.State.Gold);
                ctx.PlayerB.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, ctx.PlayerB.State.BagWeight, ctx.PlayerB.State.WearWeight, ctx.PlayerB.State.HandWeight, 0);
            }

            // 双方下发交易取消协议: SM_DEALCANCEL_DELPHI (681)
            ctx.PlayerA.EnqueuePacket(ProtocolConsts.SM_DEALCANCEL_DELPHI, 0, 0, 0, 0, reason);
            ctx.PlayerB.EnqueuePacket(ProtocolConsts.SM_DEALCANCEL_DELPHI, 0, 0, 0, 0, reason);
        }
    }

    /// <summary>
    /// 检查交易双方空间距离，若发生位移偏离（超 1 格或换地图）自动熔断取消交易
    /// 杜绝隔空交易外挂与骗物
    /// </summary>
    public bool CheckAndCancelIfMoved(ClientSession session)
    {
        var ctx = GetTrade(session.SessionId);
        if (ctx == null) return false;

        var (me, partner, _, _, _) = ctx.GetSides(session.SessionId);
        int dist = Math.Max(Math.Abs(me.X - partner.X), Math.Abs(me.Y - partner.Y));
        if (dist > 1 || me.CurrentMapId != partner.CurrentMapId)
        {
            CancelTrade(session, "双方距离过远，交易已自动取消");
            return true;
        }
        return false;
    }
}
