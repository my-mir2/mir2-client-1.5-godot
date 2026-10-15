using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;

namespace Mir2.Server.Game;

/// <summary>
/// 商人售卖货品模型
/// </summary>
public sealed class MerchantGoods
{
    public int GoodsId { get; init; }
    public string Name { get; init; } = string.Empty;
    public int Price { get; init; }
    public int Weight { get; init; } = 1;
    public int MaxDura { get; init; } = 1000;
}

/// <summary>
/// 盛大官方 1.4/1.5 NPC 实体定义 (TNormNpc / TMerchant)
/// </summary>
public sealed class NpcEntity
{
    public int Uid { get; init; }
    public string Name { get; init; } = string.Empty;
    public int MapId { get; init; }
    public int X { get; init; }
    public int Y { get; init; }
    public int Appr { get; init; } = 0;

    // 官方脚本页面字典: 键为 "@main", "@buy", "@sell", "@s_repair" 等
    public Dictionary<string, string> ScriptPages { get; } = new(StringComparer.OrdinalIgnoreCase);

    // 商店在售货品列表
    public List<MerchantGoods> Goods { get; } = new();
}

/// <summary>
/// 盛大官方 1.4/1.5 NPC 脚本对话与商人买卖特修引擎 (NpcMerchantEngine)
/// 严格 1:1 对齐官方 M2Server 物理反编译代码:
/// - 脚本分支与选项选择: TMerchant.UserSelect @ 0x4A1B5C / 0x4A4C50
/// - 商店购买与金币扣减: CM_USERBUYITEM (1014) -> SM_BUY_OK_DELPHI (648)
/// - 商店出售与折价回收: CM_USERSELLITEM (1013) -> SM_SELL_OK_DELPHI (650)
/// - 铁匠铺特修 0 损最大持久: CM_REPAIRSPECIAL (48) -> SM_REPAIROK_DELPHI (669)
/// </summary>
public sealed class NpcMerchantEngine
{
    private readonly ConcurrentDictionary<int, NpcEntity> _npcs = new();
    private int _nextMerchantItemUid = 60000;

    public void RegisterNpc(NpcEntity npc)
    {
        _npcs[npc.Uid] = npc;
        EnsureDefaultGoods(npc);
    }

    public void RegisterNpc(int uid, string name, int mapId, int x, int y)
    {
        var npc = new NpcEntity { Uid = uid, Name = name, MapId = mapId, X = x, Y = y };
        _npcs[uid] = npc;
        EnsureDefaultGoods(npc);
    }

    private void EnsureDefaultGoods(NpcEntity npc)
    {
        if (npc.Goods.Count == 0 && (npc.Name.Contains("屠夫") || npc.Name.Contains("肉")))
        {
            npc.Goods.Add(new MerchantGoods { GoodsId = 1, Name = "鸡肉", Price = 100, Weight = 1, MaxDura = 10 });
            npc.Goods.Add(new MerchantGoods { GoodsId = 2, Name = "肉", Price = 220, Weight = 2, MaxDura = 15 });
            npc.Goods.Add(new MerchantGoods { GoodsId = 3, Name = "优质肉", Price = 450, Weight = 2, MaxDura = 20 });
        }
    }

    public NpcEntity? GetNpc(int npcUid)
    {
        _npcs.TryGetValue(npcUid, out var npc);
        return npc;
    }

    public void AddGoodsToNpc(int npcUid, MerchantGoods goods)
    {
        if (_npcs.TryGetValue(npcUid, out var npc))
        {
            npc.Goods.Add(goods);
        }
    }

    public MerchantGoods? GetGoods(int npcUid, int goodsId)
    {
        if (_npcs.TryGetValue(npcUid, out var npc))
        {
            return npc.Goods.Find(g => g.GoodsId == goodsId);
        }
        return null;
    }

    public NpcEntity? FindNpcAt(int mapId, int x, int y)
    {
        foreach (var kvp in _npcs)
        {
            var npc = kvp.Value;
            if (npc.MapId == mapId && npc.X == x && npc.Y == y)
            {
                return npc;
            }
        }
        return null;
    }

    public IEnumerable<NpcEntity> AllNpcs => _npcs.Values;

    public NpcEntity? FindNpcByName(string name)
    {
        foreach (var kvp in _npcs)
        {
            if (string.Equals(kvp.Value.Name, name, StringComparison.OrdinalIgnoreCase))
                return kvp.Value;
        }
        return null;
    }


    /// <summary>
    /// 处理点击 NPC 打开对话框主页 (CM_MERCHANTHOMEPAGE 1010 -> SM_NPCDIALOG 54)
    /// </summary>
    public string GetNpcHomePage(int npcUid)
    {
        if (_npcs.TryGetValue(npcUid, out var npc))
        {
            if (npc.ScriptPages.TryGetValue("@main", out var script))
            {
                return script;
            }
            return $"你好，我是{npc.Name}。\\ \\<离开/@exit>";
        }
        return "无法找到该 NPC。";
    }

    /// <summary>
    /// 处理选择 NPC 对话选项标签 (CM_NPCSELECT 1011)
    /// </summary>
    public string SelectScriptOption(int npcUid, string label)
    {
        if (_npcs.TryGetValue(npcUid, out var npc))
        {
            if (npc.ScriptPages.TryGetValue(label, out var script))
            {
                return script;
            }
            return "选项不存在。\\ \\<返回/@main>";
        }
        return "无法找到该 NPC。";
    }

    /// <summary>
    /// 商店购买物品结算 (CM_USERBUYITEM 1014)
    /// 包含金币、40格满仓、最大背包负重精算
    /// </summary>
    public (bool success, string errorMsg, EquipItem? item) BuyItem(ClientSession session, int npcUid, int goodsId, string? itemName = null)
    {
        if (session.State == null) return (false, "玩家状态异常", null);
        if (!_npcs.TryGetValue(npcUid, out var npc)) return (false, "NPC 不存在", null);

        var goods = npc.Goods.Find(g => g.GoodsId == goodsId);
        if (goods == null && !string.IsNullOrWhiteSpace(itemName))
        {
            string sName = itemName.Trim();
            goods = npc.Goods.Find(g => g.Name.Equals(sName, StringComparison.OrdinalIgnoreCase) || g.Name.Contains(sName));
        }
        if (goods == null) return (false, "商店未售卖此物品", null);

        // 1. 金币检测
        if (session.State.Gold < goods.Price)
        {
            return (false, "金币不足，无法购买！", null);
        }

        // 2. 背包格子上限 (官方 40 格)
        if (session.State.BagItems.Count >= 40)
        {
            return (false, "背包已满，无法容纳新物品！", null);
        }

        // 3. 负重检测
        if (session.State.BagWeight + goods.Weight > session.State.MaxBagWeight)
        {
            return (false, "包裹太重，无法拿起此物品！", null);
        }

        // 扣除金币
        session.State.Gold -= goods.Price;

        // 生成装备/物品入包 (盛大官方 1.5 规则: 每个入包物品赋予全局唯一 Uid)
        var equip = new EquipItem
        {
            Uid = System.Threading.Interlocked.Increment(ref _nextMerchantItemUid),
            Name = goods.Name,
            Weight = goods.Weight,
            Dura = goods.MaxDura,
            MaxDura = goods.MaxDura
        };
        session.State.BagItems.Add(equip);
        session.State.BagWeight += goods.Weight;

        return (true, string.Empty, equip);
    }

    /// <summary>
    /// 商店出售回收结算 (CM_USERSELLITEM 1013)
    /// 官方半价回收折算
    /// </summary>
    public (bool success, int goldEarned, string itemName, string errorMsg) SellItem(ClientSession session, int itemUidOrSlot)
    {
        if (session.State == null) return (false, 0, string.Empty, "玩家状态异常");

        int idx = session.State.BagItems.FindIndex(i => i.Uid == itemUidOrSlot);
        if (idx < 0 && itemUidOrSlot >= 0 && itemUidOrSlot < session.State.BagItems.Count)
        {
            idx = itemUidOrSlot;
        }
        if (idx < 0) return (false, 0, string.Empty, "背包中不存在该物品");

        var item = session.State.BagItems[idx];
        int itemUid = item.Uid;

        // 盛大官方 1.5 铁律: 出售实际回收金额严格等于事前估价计算 (CalculateSellPrice)
        var (sellPrice, _) = CalculateSellPrice(session, itemUid);
        if (sellPrice <= 0)
        {
            sellPrice = Math.Max(100, item.Weight * 20);
        }

        session.State.BagItems.RemoveAt(idx);
        session.State.BagWeight = Math.Max(0, session.State.BagWeight - item.Weight);
        session.State.Gold = Math.Min(PlayerState.MaxGoldLimit, session.State.Gold + sellPrice);

        return (true, sellPrice, item.Name, string.Empty);
    }

    /// <summary>
    /// 根据 itemUid 或槽位编号智能查找身上或背包中的装备
    /// </summary>
    public static EquipItem? FindEquipOrBagItem(ClientSession session, int slotOrUid)
    {
        if (session.State == null) return null;
        if (slotOrUid >= 0 && slotOrUid < session.State.Equipment.Length)
        {
            var onBody = session.State.Equipment[slotOrUid];
            if (onBody != null) return onBody;
        }

        for (int i = 0; i < session.State.Equipment.Length; i++)
        {
            var eq = session.State.Equipment[i];
            if (eq != null && eq.Uid == slotOrUid) return eq;
        }

        for (int i = 0; i < session.State.BagItems.Count; i++)
        {
            var bagItem = session.State.BagItems[i];
            if (bagItem != null && (bagItem.Uid == slotOrUid || i == slotOrUid))
            {
                return bagItem;
            }
        }

        return null;
    }

    /// <summary>
    /// 铁匠铺特殊修理 (CM_REPAIRSPECIAL 48) - 智能支持身上或背包装备
    /// 0 损最大持久上限特修
    /// </summary>
    public (bool success, int cost, string itemName, string errorMsg) SpecialRepairItem(ClientSession session, int slotOrUid)
    {
        if (session.State == null) return (false, 0, string.Empty, "玩家状态异常");

        var equip = FindEquipOrBagItem(session, slotOrUid);
        if (equip == null) return (false, 0, string.Empty, "未找到待修理的装备");

        int duraLost = equip.MaxDura - equip.Dura;
        if (duraLost <= 0) return (false, 0, equip.Name, "装备持久完好，无需修理");

        int cost = duraLost * 3;
        if (session.State.Gold < cost)
        {
            return (false, cost, equip.Name, "金币不足，无法进行特殊修理！");
        }

        session.State.Gold -= cost;
        equip.Dura = equip.MaxDura; // 0 损彻底回满！

        return (true, cost, equip.Name, string.Empty);
    }

    public (bool success, int cost, string itemName, string errorMsg) SpecialRepairItem(ClientSession session, OfficialEquipSlot slot)
        => SpecialRepairItem(session, (int)slot);

    /// <summary>
    /// 铁匠铺普通修理 (CM_USERREPAIRITEM 47 / 1014) - 智能支持身上或背包装备
    /// 官方物理规则: 费用低廉 (磨损量 * 1 金币)，但会永久损失最大持久上限 MaxDura (掉持久上限为损失量的 1/3，至少 1 点)
    /// </summary>
    public (bool success, int cost, string itemName, string errorMsg) NormalRepairItem(ClientSession session, int slotOrUid)
    {
        if (session.State == null) return (false, 0, string.Empty, "玩家状态异常");

        var equip = FindEquipOrBagItem(session, slotOrUid);
        if (equip == null) return (false, 0, string.Empty, "未找到待修理的装备");

        int duraLost = equip.MaxDura - equip.Dura;
        if (duraLost <= 0)
        {
            // 盛大原版铁匠铺交互: 装备持久完好时直接返回原持久与当前金币，确认修理成功且扣费 0
            return (true, 0, equip.Name, string.Empty);
        }

        int cost = duraLost;
        if (session.State.Gold < cost)
        {
            return (false, cost, equip.Name, "金币不足，无法进行普通修理！");
        }

        session.State.Gold -= cost;

        // 官方持久衰减法则: 掉最大持久上限 (损失量的 1/3，至少 1 点)
        int duraDec = Math.Max(1, duraLost / 3);
        equip.MaxDura = Math.Max(1, equip.MaxDura - duraDec);
        equip.Dura = equip.MaxDura; // 恢复至新的上限

        return (true, cost, equip.Name, string.Empty);
    }

    public (bool success, int cost, string itemName, string errorMsg) NormalRepairItem(ClientSession session, OfficialEquipSlot slot)
        => NormalRepairItem(session, (int)slot);

    /// <summary>
    /// 官方 sub_4A1F30 装备修理费用估算 (CM_QUERYREPAIRCOST 1024)
    /// 估价公式: 普通修理 = duraLost，特修 = duraLost * 3
    /// </summary>
    public (int normalCost, int specialCost, string itemName) CalculateRepairCost(ClientSession session, int slotOrUid)
    {
        var equip = FindEquipOrBagItem(session, slotOrUid);
        if (equip == null) return (0, 0, string.Empty);

        int duraLost = Math.Max(0, equip.MaxDura - equip.Dura);
        if (duraLost == 0 || equip.MaxDura <= 0) return (0, 0, equip.Name);

        int normalCost = duraLost;
        int specialCost = duraLost * 3;

        return (normalCost, specialCost, equip.Name);
    }

    /// <summary>
    /// 官方商店回收估价公式 (CM_QUERYSELLPRICE 1012)
    /// 估价公式: (basePrice / 2) * Dura / MaxDura
    /// </summary>
    public (int sellPrice, string itemName) CalculateSellPrice(ClientSession session, int slotOrUid)
    {
        var equip = FindEquipOrBagItem(session, slotOrUid);
        if (equip == null) return (0, string.Empty);

        if (equip.MaxDura <= 0) return (10, equip.Name);

        int basePrice = equip.Price > 0 ? equip.Price : 2000;
        int sellPrice = Math.Max(1, (int)((long)(basePrice / 2) * Math.Max(1, equip.Dura) / equip.MaxDura));
        return (sellPrice, equip.Name);
    }
}
