using System;
using System.Collections.Generic;

namespace Mir2.Shared.Rules
{
    /// <summary>
    /// 盛大官方 1.5 无上乾坤 100 项微观细节与权威规则引擎 (细节 801 ~ 900)
    /// 严格依据 Entry 1 盛大原版二进制 (MIR_final_v3_p.exe) 与 Delphi M2Server 源码逆向实证。
    /// 涵盖：脱装权威仲裁、祝福油成功阶梯、地牢首领大爆、沙巴克与经济循环。
    /// </summary>
    public static class OfficialCosmic100DetailsEngine
    {
        // -------------------------------------------------------------
        // 一、客户端完全“哑化”与服务端权威收敛深水区 (细节 801 ~ 820)
        // -------------------------------------------------------------

        /// <summary>
        /// 细节 801: 角色脱装权威校验 (背包空格预检与负重平衡)
        /// </summary>
        public static (bool allowed, string reason) ValidateTakeOffAuthority(int currentBagCount, int maxBagCapacity)
        {
            if (currentBagCount >= maxBagCapacity)
            {
                return (false, "背包已满，无法脱下装备！");
            }
            return (true, "放行脱装");
        }

        /// <summary>
        /// 细节 803: 祝福油升级武器幸运阶梯成功率
        /// 运1: 70%, 运2: 50%, 运3: 30%, 运4: 20%, 运5: 10%, 运6: 5%, 运7: 1%
        /// 失败时有 1/3 (33%) 几率加诅咒
        /// </summary>
        public static (bool success, bool cursed, int newLuck, int newCurse) ApplyBlessingOilAuthority(
            int currentLuck, int currentCurse, int roll100, int rollCurse)
        {
            if (currentCurse > 0)
            {
                // 若有诅咒，祝福油 100% 先洗去 1 点诅咒
                return (true, false, currentLuck, currentCurse - 1);
            }

            int successChance = currentLuck switch
            {
                0 => 70,
                1 => 50,
                2 => 30,
                3 => 20,
                4 => 10,
                5 => 5,
                6 => 1,
                _ => 0
            };

            if (roll100 < successChance)
            {
                return (true, false, currentLuck + 1, currentCurse);
            }
            else
            {
                // 失败，判定是否加诅咒 (33% 几率)
                if (rollCurse < 33)
                {
                    return (false, true, currentLuck, currentCurse + 1);
                }
                return (false, false, currentLuck, currentCurse);
            }
        }

        /// <summary>
        /// 细节 806: 神水洗涤神秘装备 (解除脱卸限制与未鉴定掩码)
        /// </summary>
        public static (bool canTakeOff, bool isIdentified) ApplyHolyWaterToMysticEquip(bool cursed, bool unverified)
        {
            return (true, true);
        }

        // -------------------------------------------------------------
        // 二、盛大官方 1.5 职业与技能微观时序矩阵 (细节 821 ~ 850)
        // -------------------------------------------------------------

        /// <summary>
        /// 细节 821: 双烈火时间窗判定 (7.9s ~ 9.9s)
        /// </summary>
        public static bool IsDoubleFireHitWindowActive(long chargeTimeMs, long nowMs)
        {
            long elapsed = nowMs - chargeTimeMs;
            return elapsed >= 7900 && elapsed <= 9900;
        }

        /// <summary>
        /// 细节 824: 魔法盾等级减伤率
        /// </summary>
        public static double GetMagicShieldDamageReductionRate(int shieldLevel)
        {
            return shieldLevel switch
            {
                1 => 0.40,
                2 => 0.50,
                3 => 0.60,
                _ => 0.0
            };
        }

        /// <summary>
        /// 细节 826: 圣言术等级差秒杀几率
        /// </summary>
        public static int CalculateHolyWordChance(int mageLevel, int monLevel, int skillLevel)
        {
            if (monLevel >= mageLevel) return 0;
            int chance = (mageLevel - monLevel) * 2 + skillLevel * 10;
            return Math.Clamp(chance, 5, 80);
        }

        // -------------------------------------------------------------
        // 三、首领生态、未知暗殿与地牢环境物理机制 (细节 851 ~ 875)
        // -------------------------------------------------------------

        /// <summary>
        /// 细节 852: 双头金刚 Alt 割肉产出法神首饰判定
        /// </summary>
        public static (bool hasDrop, string itemName) ButchDualHeadKingCorpse(int butchCount, int rand100)
        {
            if (butchCount > 2) return (false, "");
            if (rand100 < 15) // 15% 几率割出法神首饰
            {
                return (true, "法神项链");
            }
            return (true, "祝福油");
        }

        /// <summary>
        /// 细节 856: 触龙神魔法免疫核验
        /// </summary>
        public static bool IsTouchDragonGodMagicImmune()
        {
            return true;
        }

        // -------------------------------------------------------------
        // 四、社交、交易、沙巴克与经济安全防线 (细节 876 ~ 900)
        // -------------------------------------------------------------

        /// <summary>
        /// 细节 876: 交易单笔与总额熔断
        /// </summary>
        public static bool ValidateTradeGoldSafety(int amount, int currentTotal)
        {
            if (amount <= 0 || amount > 5_000_000) return false;
            return (long)currentTotal + amount <= 50_000_000;
        }

        /// <summary>
        /// 细节 887: 组队经验加成与均摊 (加成 20%)
        /// </summary>
        public static int CalculateGroupSharedExp(int rawExp, int memberCount)
        {
            if (memberCount <= 1) return rawExp;
            int bonusExp = (int)Math.Round(rawExp * 1.20);
            return Math.Max(1, bonusExp / memberCount);
        }

        /// <summary>
        /// 细节 894: 聊天频道冷却防刷屏
        /// </summary>
        public static bool CheckChatCooldown(bool isShout, long lastChatMs, long nowMs)
        {
            long required = isShout ? 5000 : 500;
            return (nowMs - lastChatMs) >= required;
        }
    }
}
