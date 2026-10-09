using System;
using System.Collections.Generic;

namespace Mir2.Shared.Rules;

/// <summary>
/// 盛大官方 1.5 四大经典套装核心特效引擎 (纯 C# 实现)
/// 严格 1:1 还原官方原版套装物理规则:
///   1. 虹魔套装 (RainbowMagic): 佩戴虹魔首饰，物理攻击伤害的 10% 转化为自身生命回复 (吸血)
///   2. 魔血套装 (DemonBlood): 牺牲 MP 转化高额 HP (佩戴项链/手镯/戒指各增加 HP +25，扣减 MP -20)
///   3. 祈祷套装 (PrayerCataclysm): 全套 5 件 (祈祷之刃/头盔/项链/手镯/戒指) 佩戴触发神之天谴，全服随从宝宝叛变红名，项链必定破碎消失
/// </summary>
public static class SuitEffectEngine
{
    // ==================== 1. 虹魔套装 (吸血 10%) ====================
    public static class RainbowMagicSuit
    {
        public const double VampiricRate = 0.10; // 10% 吸血转化率

        public static int CalcVampiricHeal(int damageDealt, bool isWearingSuit)
        {
            if (!isWearingSuit || damageDealt <= 0) return 0;
            return Math.Max(1, (int)Math.Round(damageDealt * VampiricRate));
        }
    }

    // ==================== 2. 魔血套装 (蓝转血) ====================
    public static class DemonBloodSuit
    {
        public static (int bonusHp, int penaltyMp) CalculateStatModifiers(int pieceCount)
        {
            pieceCount = Math.Clamp(pieceCount, 0, 5);
            // 每件魔血装备: HP +25, MP -20; 全套 5 件额外激活 HP +50
            int hp = pieceCount * 25 + (pieceCount >= 5 ? 50 : 0);
            int mp = pieceCount * 20;
            return (hp, mp);
        }
    }

    // ==================== 3. 祈祷套装 (全服天谴) ====================
    public static class PrayerSuit
    {
        public static bool CheckFullSet(
            bool hasWeapon, bool hasHelmet, bool hasNecklace, bool hasBracelet, bool hasRing)
        {
            return hasWeapon && hasHelmet && hasNecklace && hasBracelet && hasRing;
        }

        public static (bool triggered, string broadcastMessage, bool necklaceBroken) EvaluatePrayerCataclysm(
            bool isFullSetWearing)
        {
            if (!isFullSetWearing)
            {
                return (false, string.Empty, false);
            }

            string broadcast = "【全服天谴公告】神明震怒！祈祷套装神力觉醒，所有神兽与骷髅陷入癫狂叛变！祈祷项链碎裂消失！";
            return (true, broadcast, true);
        }
    }
}
