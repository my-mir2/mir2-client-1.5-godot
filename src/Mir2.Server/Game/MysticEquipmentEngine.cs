using System;
using Mir2.Shared.Models;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 未知暗殿神秘装备解封与诅咒状态机引擎 (MysticEquipmentEngine)
/// 严格 1:1 对齐官方 M2Server 规则:
/// 1. 神秘头盔、神秘腰带、神秘戒指产出时为未鉴定状态 (属性全隐藏为 ???)
/// 2. 玩家穿戴时触发鉴定:
///    - 随机骰子生成 DC/MC/SC/AC/MAC (0~5 点浮动，可能极品也可能全 0 垃圾)
///    - 20% 几率触发“神秘诅咒”附着 (IsCursed = true)
/// 3. 被诅咒的神秘装备脱下拦截: 客户端脱装备被 M2Server 拦截拒绝，提示“装备被神秘力量诅咒，无法取下！”
/// 4. “神水 / 清洗水”道具净化: 使用神水可清洗驱散装备上的诅咒，恢复正常脱卸
/// </summary>
public static class MysticEquipmentEngine
{
    public static bool IsMysticItem(string itemName)
    {
        return itemName.Contains("神秘头盔") ||
               itemName.Contains("神秘腰带") ||
               itemName.Contains("神秘戒指");
    }

    /// <summary>
    /// 当玩家首次佩戴未鉴定的神秘装备时，服务端权威掷骰生成随机属性与诅咒
    /// </summary>
    public static (bool identified, bool isCursed, string message) IdentifyMysticEquipment(EquipItem item, Random rng)
    {
        if (item == null || !IsMysticItem(item.Name))
        {
            return (false, false, "非神秘装备。");
        }

        // 已经鉴定过则不再重复掷骰
        if (item.BonusDc > 0 || item.BonusMc > 0 || item.BonusSc > 0 || item.BonusAc > 0 || item.BonusMac > 0 || item.Curse > 0)
        {
            return (false, item.Curse > 0, "装备早已解封鉴定。");
        }

        // 官方 0~5 点随机词条浮动
        item.BonusDc = rng.Next(6);
        item.BonusMc = rng.Next(6);
        item.BonusSc = rng.Next(6);
        item.BonusAc = rng.Next(4);
        item.BonusMac = rng.Next(4);

        // 20% 几率触发诅咒附着 (Curse = 1)
        bool cursed = rng.Next(100) < 20;
        if (cursed)
        {
            item.Curse = 1;
        }

        string msg = cursed
            ? $"【诅咒封印】你戴上了{item.Name}，一股冰冷邪恶的气息锁死了装备！它被诅咒了！"
            : $"【解封鉴定】你戴上了{item.Name}，神秘符文显现！攻/魔/道/防属性已全部浮现！";

        return (true, cursed, msg);
    }

    /// <summary>
    /// 脱下装备校验: 检查装备是否带神秘诅咒 (若带诅咒则禁止脱下)
    /// </summary>
    public static (bool allowTakeOff, string reason) CheckCanTakeOff(EquipItem item)
    {
        if (item == null) return (true, string.Empty);

        if (IsMysticItem(item.Name) && item.Curse > 0)
        {
            return (false, "这件神秘装备已被邪恶力量深深诅咒，死死吸附在身上，无法取下！请使用神水清洗！");
        }

        return (true, string.Empty);
    }

    /// <summary>
    /// 使用“神水”洗涤驱散神秘装备上的诅咒
    /// </summary>
    public static (bool success, string message) PurifyMysticCurseWithHolyWater(EquipItem item)
    {
        if (item == null || !IsMysticItem(item.Name))
        {
            return (false, "请对神秘装备使用神水！");
        }

        if (item.Curse <= 0)
        {
            return (false, "该神秘装备并未受到诅咒，无需清洗。");
        }

        item.Curse = 0; // 驱散诅咒
        return (true, $"神水散发着晶莹的圣光，渗透进{item.Name}，彻底洗净了邪恶诅咒！你现在可以自由取下它了！");
    }
}
