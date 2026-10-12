using System;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 M2Server 药水使用与瞬回/持续回复引擎
/// 对应官方物理地址:
/// - sub_4B7100 TCreature.Eat
/// - 官方变量: nIncHealth (+0x228), nIncSpell (+0x22C)
/// - 官方封包: CM_EATITEM (1006) / CM_EAT (1002) -> SM_EAT_OK (606) + SM_HEALTHSPELLCHANGED (53)
/// 
/// 核心物理真值规则:
/// 1. 瞬回药剂 (太阳水/强效太阳水/万年雪霜/疗伤药): 直接注入当前 HP/MP, 不走血池;
/// 2. 持续药剂 (金创药/魔法药): 药效追加进入 IncHealth / IncSpell 蓄积池;
/// 3. 每 Tick (心跳周期) 匀速抽取 (每次 5 点) 注入当前生命与魔法, 达到上限停止溢出;
/// 4. 角色死亡 (HP <= 0) 时禁止使用任何药水并强制清空残余血池.
/// </summary>
public static class PotionEngine
{
    public const int DefaultTickTransferRate = 5; // 官方每次心跳释放 5 点血/蓝

    public enum PotionResult
    {
        Success = 0,
        Dead = 1,
        ItemNotFound = 2,
        NotEdible = 3,
    }

    public record PotionEffect(
        int InstantHp,
        int InstantMp,
        int ContinuousHp,
        int ContinuousMp,
        bool ClearPoison = false
    );

    /// <summary>
    /// 解析药水或消耗品名称获得其官方 1.5 效果
    /// </summary>
    public static PotionEffect? GetPotionEffect(string itemName)
    {
        if (string.IsNullOrWhiteSpace(itemName)) return null;

        var name = itemName.Trim();

        // 1. 瞬回药剂
        if (name.Contains("强效太阳水"))
        {
            return new PotionEffect(InstantHp: 50, InstantMp: 50, ContinuousHp: 0, ContinuousMp: 0);
        }
        if (name.Contains("太阳水"))
        {
            return new PotionEffect(InstantHp: 30, InstantMp: 40, ContinuousHp: 0, ContinuousMp: 0);
        }
        if (name.Contains("万年雪霜"))
        {
            return new PotionEffect(InstantHp: 80, InstantMp: 120, ContinuousHp: 0, ContinuousMp: 0);
        }
        if (name.Contains("疗伤药"))
        {
            return new PotionEffect(InstantHp: 130, InstantMp: 130, ContinuousHp: 0, ContinuousMp: 0);
        }

        // 2. 持续回复药剂 - 金创药 (生命)
        if (name.Contains("强效金创药"))
        {
            return new PotionEffect(InstantHp: 0, InstantMp: 0, ContinuousHp: 90, ContinuousMp: 0);
        }
        if (name.Contains("中量金创药") || name.Contains("金创药(中量)"))
        {
            return new PotionEffect(InstantHp: 0, InstantMp: 0, ContinuousHp: 60, ContinuousMp: 0);
        }
        if (name.Contains("金创药"))
        {
            return new PotionEffect(InstantHp: 0, InstantMp: 0, ContinuousHp: 30, ContinuousMp: 0);
        }

        // 3. 持续回复药剂 - 魔法药 (魔法)
        if (name.Contains("强效魔法药"))
        {
            return new PotionEffect(InstantHp: 0, InstantMp: 0, ContinuousHp: 0, ContinuousMp: 90);
        }
        if (name.Contains("中量魔法药") || name.Contains("魔法药(中量)"))
        {
            return new PotionEffect(InstantHp: 0, InstantMp: 0, ContinuousHp: 0, ContinuousMp: 60);
        }
        if (name.Contains("魔法药"))
        {
            return new PotionEffect(InstantHp: 0, InstantMp: 0, ContinuousHp: 0, ContinuousMp: 30);
        }

        // 4. 特殊解毒药剂与神水 (IDA 0x4B7100 / sub_4C7720)
        if (name.Contains("解毒") || name.Contains("神水") || name.Contains("鹿茸"))
        {
            return new PotionEffect(InstantHp: 0, InstantMp: 0, ContinuousHp: 0, ContinuousMp: 0, ClearPoison: true);
        }

        return null;
    }

    /// <summary>
    /// 使用/吃药 (sub_4B7100 TCreature.Eat)
    /// </summary>
    public static (PotionResult result, PotionEffect? effect) ConsumePotion(PlayerState player, int bagSlot, string itemName = "")
    {
        var (res, eff, _, _) = ConsumePotionEx(player, bagSlot, itemName);
        return (res, eff);
    }

    /// <summary>
    /// 使用/吃药扩展版本 (返回消耗的具体物品对象与背包槽位，供网络层下发 SM_DELITEM 物理清格)
    /// </summary>
    public static (PotionResult result, PotionEffect? effect, EquipItem? consumedItem, int consumedSlot) ConsumePotionEx(PlayerState player, int bagSlot, string itemName = "")
    {
        if (player.Hp <= 0)
        {
            player.IncHealth = 0;
            player.IncSpell = 0;
            return (PotionResult.Dead, null, null, -1);
        }

        // 从背包中定位药水
        EquipItem? item = null;
        int targetSlot = -1;
        if (bagSlot >= 0 && bagSlot < player.BagItems.Count)
        {
            item = player.BagItems[bagSlot];
            targetSlot = bagSlot;
        }
        else if (!string.IsNullOrEmpty(itemName))
        {
            targetSlot = player.BagItems.FindIndex(i => i.Name == itemName);
            if (targetSlot >= 0)
            {
                item = player.BagItems[targetSlot];
            }
        }

        string actualName = item?.Name ?? itemName;
        var effect = GetPotionEffect(actualName);
        if (effect == null)
        {
            return (PotionResult.NotEdible, null, null, -1);
        }

        // 背包中扣除该药水
        if (item != null)
        {
            player.BagItems.Remove(item);
            player.BagWeight = Math.Max(0, player.BagWeight - item.Weight);
        }

        // 1. 瞬回应用
        if (effect.InstantHp > 0)
        {
            player.Hp = Math.Min(player.MaxHp, player.Hp + effect.InstantHp);
        }
        if (effect.InstantMp > 0)
        {
            player.Mp = Math.Min(player.MaxMp, player.Mp + effect.InstantMp);
        }

        // 2. 持续蓄积池注入
        if (effect.ContinuousHp > 0)
        {
            player.IncHealth += effect.ContinuousHp;
        }
        if (effect.ContinuousMp > 0)
        {
            player.IncSpell += effect.ContinuousMp;
        }

        // 3. 解毒剂应用：彻底解除红绿毒状态机 (IDA sub_4C7720)
        if (effect.ClearPoison)
        {
            player.ClearPoison();
        }

        return (PotionResult.Success, effect, item, targetSlot);
    }

    /// <summary>
    /// 世界心跳驱动持续回复流转 (每 Tick 释放 5 点)
    /// </summary>
    public static (int hpGain, int mpGain) TickIncHealthSpell(PlayerState player, int transferRate = DefaultTickTransferRate)
    {
        if (player.Hp <= 0)
        {
            player.IncHealth = 0;
            player.IncSpell = 0;
            return (0, 0);
        }

        int hpGain = 0;
        int mpGain = 0;

        // 1. 转移生命血池 (盛大官方 1.5 物理真值: 满血顶药绝不清空血池，保留在蓄积池等待受击掉血时自动释放)
        if (player.IncHealth > 0 && player.Hp < player.MaxHp)
        {
            int step = Math.Min(player.IncHealth, transferRate);
            step = Math.Min(step, player.MaxHp - player.Hp);

            player.Hp += step;
            player.IncHealth -= step;
            hpGain = step;
        }

        // 2. 转移魔法蓝池 (满魔顶药同样保留蓝池，等待施法耗蓝时释放)
        if (player.IncSpell > 0 && player.Mp < player.MaxMp)
        {
            int step = Math.Min(player.IncSpell, transferRate);
            step = Math.Min(step, player.MaxMp - player.Mp);

            player.Mp += step;
            player.IncSpell -= step;
            mpGain = step;
        }

        return (hpGain, mpGain);
    }
}
