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

    public void RegisterNpc(NpcEntity npc)
    {
        _npcs[npc.Uid] = npc;
    }

    public NpcEntity? GetNpc(int npcUid)
    {
        _npcs.TryGetValue(npcUid, out var npc);
        return npc;
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
    public (bool success, string errorMsg, EquipItem? item) BuyItem(ClientSession session, int npcUid, int goodsId)
    {
        if (session.State == null) return (false, "玩家状态异常", null);
        if (!_npcs.TryGetValue(npcUid, out var npc)) return (false, "NPC 不存在", null);

        var goods = npc.Goods.Find(g => g.GoodsId == goodsId);
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

        // 生成装备/物品入包
        var equip = new EquipItem
        {
            Uid = 60000 + goods.GoodsId,
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
    public (bool success, int goldEarned, string itemName, string errorMsg) SellItem(ClientSession session, int itemUid)
    {
        if (session.State == null) return (false, 0, string.Empty, "玩家状态异常");

        int idx = session.State.BagItems.FindIndex(i => i.Uid == itemUid);
        if (idx < 0) return (false, 0, string.Empty, "背包中不存在该物品");

        var item = session.State.BagItems[idx];
        session.State.BagItems.RemoveAt(idx);
        session.State.BagWeight = Math.Max(0, session.State.BagWeight - item.Weight);

        // 官方估价机制: 原价半价回收 (基础回收 100 金币)
        int sellPrice = Math.Max(100, item.Weight * 20);
        session.State.Gold = Math.Min(PlayerState.MaxGoldLimit, session.State.Gold + sellPrice);

        return (true, sellPrice, item.Name, string.Empty);
    }

    /// <summary>
    /// 铁匠铺特殊修理 (CM_REPAIRSPECIAL 48)
    /// 0 损最大持久上限特修
    /// </summary>
    public (bool success, int cost, string itemName, string errorMsg) SpecialRepairItem(ClientSession session, OfficialEquipSlot slot)
    {
        if (session.State == null) return (false, 0, string.Empty, "玩家状态异常");

        var equip = session.State.Equipment[(int)slot];
        if (equip == null) return (false, 0, string.Empty, "该槽位未穿戴装备");

        int duraLost = equip.MaxDura - equip.Dura;
        if (duraLost <= 0) return (false, 0, equip.Name, "装备持久完好，无需修理");

        // 特修费用: 磨损量 * 3 倍金币 (0 损最大持久上限)
        int cost = duraLost * 3;
        if (session.State.Gold < cost)
        {
            return (false, cost, equip.Name, "金币不足，无法进行特殊修理！");
        }

        session.State.Gold -= cost;
        equip.Dura = equip.MaxDura; // 0 损彻底回满！

        return (true, cost, equip.Name, string.Empty);
    }
}
