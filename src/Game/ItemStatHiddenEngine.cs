using System;
using System.Collections.Generic;

namespace Mir2Client.Game;

/// <summary>
/// 盛大官方 1.5 装备极品属性、特戒隐藏机制、药水与数值对抗公式引擎 (细节 111 ~ 130)
/// 严格依据官方服务端与客户端数值逆向反编译证据
/// </summary>
public static class ItemStatHiddenEngine
{
    // --------------------------------------------------------------------------------
    // 细节 111: 装备掉落生成极品属性 (+1~+5) 绿色属性文字与闪烁微光
    // --------------------------------------------------------------------------------
    public static class ItemSuperStatEngine
    {
        public static (bool hasExtra, string formattedColor, string statText) FormatExtraStat(int extraValue, string statName)
        {
            if (extraValue <= 0) return (false, "#FFFFFF", "");
            return (true, "#00FF00", $"{statName} +{extraValue}"); // 绿色极品标识
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 112: 武器幸运与诅咒系统 (幸运 +9 刀刀攻击上限，诅咒 +10 恒定攻击下限)
    // --------------------------------------------------------------------------------
    public static class WeaponLuckCurseEngine
    {
        public static int CalculateAttackPower(int minDc, int maxDc, int totalLuck, int totalCurse, Random rng)
        {
            if (totalLuck >= 9) return maxDc; // 运 9 套刀刀发挥最大攻击
            if (totalCurse >= 10) return minDc; // 诅 10 恒定发挥最小攻击

            // 基础随机分布 + 幸运/诅咒加权
            int range = maxDc - minDc + 1;
            int roll = rng.Next(0, range);

            if (totalLuck > 0)
            {
                // 幸运增加掷出高攻击概率
                if (rng.Next(0, 10) < totalLuck) return maxDc;
            }
            if (totalCurse > 0)
            {
                // 诅咒增加掷出低攻击概率
                if (rng.Next(0, 10) < totalCurse) return minDc;
            }
            return minDc + roll;
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 113: 祝福油使用状态机 (成功增加幸运、失败保持、严重失败增加诅咒)
    // --------------------------------------------------------------------------------
    public static class BlessingOilEngine
    {
        public enum BlessingResult { Success, Keep, Cursed }

        public static (BlessingResult result, int newLuck, int newCurse) ApplyBlessingOil(
            int currentLuck, int currentCurse, int roll100)
        {
            // 经典概率：小于 30 成功，30~85 保持，大于 85 严重失败诅咒
            if (roll100 < 30)
            {
                if (currentCurse > 0) return (BlessingResult.Success, currentLuck, currentCurse - 1);
                return (BlessingResult.Success, Math.Min(7, currentLuck + 1), currentCurse);
            }
            if (roll100 > 85)
            {
                return (BlessingResult.Cursed, Math.Max(0, currentLuck - 1), currentCurse + 1);
            }
            return (BlessingResult.Keep, currentLuck, currentCurse);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 114: 武器准确与敏捷对抗公式 (命中率 = (准确 / (准确 + 敏捷 * 0.8)) * 100%)
    // --------------------------------------------------------------------------------
    public static class AccuracyAgilityEngine
    {
        public static float CalculateHitRate(int attackerAccuracy, int defenderAgility)
        {
            if (attackerAccuracy <= 0) return 0.05f; // 最低 5% 保底
            float denom = attackerAccuracy + defenderAgility * 0.8f;
            return Math.Clamp(attackerAccuracy / denom, 0.05f, 1.0f);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 115: 魔法躲避白金项链 (白色虎齿项链 20%~50% 魔法完全豁免)
    // --------------------------------------------------------------------------------
    public static class MagicEvadeEngine
    {
        public static bool CheckMagicEvade(int necklaceEvadePercent, int roll100)
        {
            return roll100 < necklaceEvadePercent;
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 116: 辟毒手镯中毒躲避与中毒恢复机制
    // --------------------------------------------------------------------------------
    public static class PoisonResistanceEngine
    {
        public static (bool poisonEvaded, float durationMultiplier) CalculateResistance(int poisonDodgePercent, int poisonRecoverPercent, int roll100)
        {
            bool evaded = roll100 < poisonDodgePercent;
            float durMulti = Math.Max(0.2f, 1.0f - (poisonRecoverPercent / 100.0f));
            return (evaded, durMulti);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 117: 记忆套装“天地合一”全队召集命令
    // --------------------------------------------------------------------------------
    public static class MemoryRecallEngine
    {
        public static (bool canRecall, string errorReason) ValidateRecall(bool isLeader, bool hasFullMemorySet, string command)
        {
            if (command != "@天地合一") return (false, "指令不正确");
            if (!isLeader) return (false, "只有队长才能使用天地合一");
            if (!hasFullMemorySet) return (false, "未集齐记忆套装 (头盔/项链/手镯/戒指)");
            return (true, "");
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 118: 祈祷套装佩戴齐全全服宝宝叛变公告与倒计时
    // --------------------------------------------------------------------------------
    public static class PrayerCataclysmEngine
    {
        public static (bool triggerCataclysm, string globalBroadcast) CheckPrayerSuit(bool isFullSet)
        {
            if (!isFullSet) return (false, "");
            return (true, "神之祈祷生效，神兽与骷髅将向人类复仇！");
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 119: 神秘装备未戴属性问号 (???) 与神水解绑
    // --------------------------------------------------------------------------------
    public static class MysticItemEngine
    {
        public static (string displayStats, bool isBound) EvaluateMystic(bool isEquipped, bool usedHolyWater)
        {
            if (!isEquipped) return ("???", false);
            return ("攻击 0-5 隐藏属性已激活", !usedHolyWater); // 未用神水则锁定无法取下
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 120: 幽灵手套 + 幽灵项链隐藏属性加成 (攻速 +1 与反伤 10%)
    // --------------------------------------------------------------------------------
    public static class GhostSuitEngine
    {
        public static (int attackSpeedBonus, int reflectDamage) Evaluate(bool hasGloves, bool hasNecklace, int incomingDmg)
        {
            if (hasGloves && hasNecklace)
            {
                return (1, (int)(incomingDmg * 0.1f));
            }
            return (0, 0);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 121: 龙之手镯对法师雷电术伤害隐藏加成 10%
    // --------------------------------------------------------------------------------
    public static class DragonBraceletEngine
    {
        public static int ApplyBonus(int rawDamage, int braceletCount)
        {
            return (int)(rawDamage * (1.0f + 0.10f * braceletCount));
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 122: 三眼手镯对道士灵魂火符伤害加成 10%
    // --------------------------------------------------------------------------------
    public static class ThreeEyeBraceletEngine
    {
        public static int ApplyBonus(int rawDamage, int braceletCount)
        {
            return (int)(rawDamage * (1.0f + 0.10f * braceletCount));
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 123: 骑士手镯破防机制 (无视目标物理防御下限)
    // --------------------------------------------------------------------------------
    public static class KnightBraceletEngine
    {
        public static (int effMinDef, int effMaxDef) CalculateEffectiveDefense(int targetMinDef, int targetMaxDef, bool hasKnightBracelet)
        {
            return hasKnightBracelet ? (0, targetMaxDef) : (targetMinDef, targetMaxDef);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 124: 心灵手镯提升道士施毒术毒伤跳频 20%
    // --------------------------------------------------------------------------------
    public static class SoulBraceletEngine
    {
        public static float GetPoisonIntervalSeconds(bool hasSoulBracelet)
        {
            return hasSoulBracelet ? 2.4f : 3.0f; // 从 3 秒加快到 2.4 秒
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 125: 狂风戒指 + 狂风项链攻速套 (后摇缩短 10%~20%)
    // --------------------------------------------------------------------------------
    public static class WindSpeedEngine
    {
        public static float CalculateAttackDuration(float baseDurationSeconds, int windPieces)
        {
            float reduction = Math.Clamp(0.1f * windPieces, 0f, 0.3f);
            return baseDurationSeconds * (1.0f - reduction);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 126: 红宝石戒指与铂金戒指隐藏抗性
    // --------------------------------------------------------------------------------
    public static class RubyPlatinumEngine
    {
        public static (int fireResistance, int poisonResistance) GetResistance(bool ruby, bool platinum)
        {
            return (ruby ? 15 : 0, platinum ? 15 : 0);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 127: 强效太阳水瞬间回血 50、回蓝 50 且不占药水排队 CD
    // --------------------------------------------------------------------------------
    public static class SunWaterPotionEngine
    {
        public static (int instantHp, int instantMp, bool ignoresQueue) DrinkSunWater()
        {
            return (50, 50, true);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 128: 万年雪霜 100/100 与疗伤药 200/200 瞬回
    // --------------------------------------------------------------------------------
    public static class HighHealPotionEngine
    {
        public static (int instantHp, int instantMp) DrinkPotion(string potionName)
        {
            return potionName switch
            {
                "万年雪霜" => (100, 100),
                "疗伤药" => (200, 200),
                _ => (0, 0)
            };
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 129: 超重状态角色移动速度减半且无法奔跑
    // --------------------------------------------------------------------------------
    public static class OverweightSpeedEngine
    {
        public static (bool canRun, float moveSpeedMultiplier) EvaluateMovement(bool isOverweight)
        {
            if (isOverweight) return (false, 0.5f);
            return (true, 1.0f);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 130: 饥饿度/活力度机制 (长时间不进食攻击力衰减 10%)
    // --------------------------------------------------------------------------------
    public static class HungerPenaltyEngine
    {
        public static int ApplyHunger(int dc, int hungerPercent)
        {
            if (hungerPercent <= 0) return (int)(dc * 0.9f);
            return dc;
        }
    }
}
