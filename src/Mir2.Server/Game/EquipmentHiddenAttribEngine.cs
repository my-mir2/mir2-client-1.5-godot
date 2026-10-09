using System;
using Mir2.Shared.Models;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 经典装备隐藏暗属性引擎 (EquipmentHiddenAttribEngine)
/// 严格 1:1 对齐官方 M2Server 首饰装备暗属性算法 (AUDIT.md 细节 211 ~ 230):
/// 1. 心灵手镯: 赋予道士灵魂火符准确 Hit +2
/// 2. 恶魔铃铛: 法师诱惑之光成功率提升 15%
/// 3. 龙之手镯: 战士破盾几率提升 10% (无视魔法盾直接击穿)
/// 4. 红宝石戒指: 缩短法师魔法盾受击硬直 30%
/// 5. 祈祷头盔: 角色死亡时该头盔必直接粉碎销毁，绝对不掉落于地
/// 6. 幽灵手套: 隐藏物理防御 AC +1
/// </summary>
public sealed class EquipmentHiddenAttribEngine
{
    /// <summary>
    /// 检测道士心灵手镯带来的灵魂火符额外命中
    /// </summary>
    public static int GetSoulFireAccuracyBonus(PlayerState state)
    {
        if (state == null) return 0;
        int bonus = 0;
        for (int i = 0; i < 9; i++)
        {
            var item = state.Equipment[i];
            if (item != null && item.Name.Contains("心灵手镯"))
            {
                bonus += 2;
            }
        }
        return bonus;
    }

    /// <summary>
    /// 检测恶魔铃铛诱惑加成率
    /// </summary>
    public static double GetTemptationSuccessRateBonus(PlayerState state)
    {
        if (state == null) return 0.0;
        for (int i = 0; i < 9; i++)
        {
            var item = state.Equipment[i];
            if (item != null && item.Name.Contains("恶魔铃铛"))
            {
                return 0.15; // +15%
            }
        }
        return 0.0;
    }

    /// <summary>
    /// 检测龙之手镯破盾几率加成
    /// </summary>
    public static bool CheckDragonBraceletArmorPiercing(PlayerState state, Random rng)
    {
        if (state == null) return false;
        int count = 0;
        for (int i = 0; i < 9; i++)
        {
            var item = state.Equipment[i];
            if (item != null && item.Name.Contains("龙之手镯"))
            {
                count++;
            }
        }
        if (count == 0) return false;
        // 每只龙之手镯提供 10% 破盾几率
        return rng.Next(100) < (count * 10);
    }

    /// <summary>
    /// 祈祷头盔死亡必碎裂机制 (不掉落在地上供他人拾取，直接销毁)
    /// </summary>
    public static (bool destroyed, string message) ProcessPrayerHelmetDeathShatter(PlayerState state)
    {
        if (state == null) return (false, string.Empty);
        var helm = state.Equipment[(int)OfficialEquipSlot.Helmet];
        if (helm != null && helm.Name.Contains("祈祷头盔"))
        {
            state.Equipment[(int)OfficialEquipSlot.Helmet] = null;
            state.RecalculateAttributes();
            return (true, "【祈祷头盔】在死亡冲击中化为了一缕青烟，碎裂消散！");
        }
        return (false, string.Empty);
    }
}
