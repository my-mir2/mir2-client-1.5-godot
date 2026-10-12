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

        /// <summary>
        /// 盛大官方 1.5 祈祷天谴与随机粉碎结算 (对齐 SuitWorldEventEngine 与 SpecialRings)
        /// </summary>
        public static (bool triggered, string broadcastMessage, string brokenPieceName) EvaluatePrayerCataclysm(
            bool isFullSetWearing, int rngRoll = 0)
        {
            if (!isFullSetWearing)
            {
                return (false, string.Empty, string.Empty);
            }

            string[] pieces = ["祈祷头盔", "祈祷项链", "祈祷手镯", "祈祷戒指", "祈祷之刃"];
            int idx = Math.Abs(rngRoll) % pieces.Length;
            string broken = pieces[idx];

            string broadcast = "神之祈祷，天地变色！玛法大陆所有召唤生物已陷入癫狂，反噬其主！";
            return (true, broadcast, broken);
        }

        /// <summary>
        /// 向后兼容老签名
        /// </summary>
        public static (bool triggered, string broadcastMessage, bool pieceBroken) EvaluatePrayerCataclysmLegacy(
            bool isFullSetWearing)
        {
            var res = EvaluatePrayerCataclysm(isFullSetWearing, 0);
            return (res.triggered, res.broadcastMessage, res.triggered);
        }
    }

    // ==================== 4. 记忆套装 (天地合一) ====================
    public static class MemorySuit
    {
        public const int CooldownSeconds = 180; // 盛大官方 1.5 权威 180 秒神力冷却

        public static bool CheckFullSet(bool hasHelmet, bool hasNecklace, bool hasBracelet, bool hasRing)
        {
            return hasHelmet && hasNecklace && hasBracelet && hasRing;
        }

        public static (bool canRecall, string failReason) ValidateRecallPreconditions(
            bool isLeader, bool isFullSet, bool inSabukPalace, long lastRecallTick, long nowTick)
        {
            if (!isLeader) return (false, "只有编组队长方能施展【天地合一】召唤队伍！");
            if (!isFullSet) return (false, "你尚未穿齐记忆全套 (记忆头盔/项链/手镯/戒指)，无法使用天地合一！");
            if (inSabukPalace) return (false, "沙巴克皇宫受禁空结界封锁，神力无法穿透，无法施展天地合一！");
            if (lastRecallTick > 0 && nowTick - lastRecallTick < CooldownSeconds * 1000L)
            {
                long remainSec = Math.Max(1, (CooldownSeconds * 1000L - (nowTick - lastRecallTick)) / 1000L);
                return (false, $"记忆套装神力正在沉睡中，还需等待 {remainSec} 秒方可再次施展天地合一！");
            }
            return (true, string.Empty);
        }
    }
}
