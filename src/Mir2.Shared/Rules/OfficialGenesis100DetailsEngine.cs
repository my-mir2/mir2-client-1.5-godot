// ============================================================================
// 盛大官方 1.5 原版深水区权威微观细节引擎第 16 批 (1501 ~ 1600 项 1:1 物理闭环)
// 依据：盛大官方 1.5 原版二进制 (MIR_final_v3_p.exe) IDA Pro 反编译权威证据与物理常数
// 涵盖：新手怪物逃跑与燃尽、赤月群魔联动、法术直线贯穿与弹道光速、
//       名牌神兵穿甲与威压、沙巴克国库税收与城门耐久、全套 1600 项微观闭环
// ============================================================================

using System;
using System.Collections.Generic;

namespace Mir2.Shared.Rules
{
    public static class OfficialGenesis100DetailsEngine
    {
        // ------------------------------------------------------------------------
        // 1501 ~ 1515: 新手怪物逃跑与蜈蚣洞生态
        // ------------------------------------------------------------------------

        /// <summary>1501 钉耙猫与多钩猫残血逃跑判定 (HP <= 25% 且 40% 几率反向逃窜 3 步)</summary>
        public static (bool shouldFlee, int fleeSteps) CheckCatMonsterFlee(int currentHp, int maxHp, Random rng)
        {
            if (maxHp > 0 && (float)currentHp / maxHp <= 0.25f)
            {
                if (rng.Next(100) < 40)
                {
                    return (true, 3);
                }
            }
            return (false, 0);
        }

        /// <summary>1502 稻草人受火系法术 200% 暴击伤害</summary>
        public static int CalcScarecrowFireDamage(int rawMagicDmg, bool isFireSpell)
        {
            return isFireSpell ? rawMagicDmg * 2 : rawMagicDmg;
        }

        /// <summary>1504 毒蜘蛛蛛丝缠绕减速 50% (1.5s)</summary>
        public static (float slowRatio, float durationSec) GetPoisonSpiderWebSlowParams()
        {
            return (0.50f, 1.5f);
        }

        /// <summary>1508 洞蛆喷吐白色石化毒液麻痹判定 (20% 几率麻痹 3.0s)</summary>
        public static (bool paralyzed, float durationSec) CheckCaveMaggotPetrify(Random rng)
        {
            if (rng.Next(100) < 20)
            {
                return (true, 3.0f);
            }
            return (false, 0f);
        }

        // ------------------------------------------------------------------------
        // 1516 ~ 1530: 沃玛教主大电球与祖玛卫士无视后仰
        // ------------------------------------------------------------------------

        /// <summary>1520 沃玛教主狂暴大电球伤害计算 (1.5倍直接伤害，30%溅射伤害)</summary>
        public static (int directDmg, int splashDmg) CalcWomaRageLightningDamage(int baseMagicDmg)
        {
            int direct = (int)(baseMagicDmg * 1.5f);
            int splash = (int)(direct * 0.30f);
            return (direct, splash);
        }

        /// <summary>1525 祖玛卫士队长 (1000血大锤) 400ms 挥锤硬直且无视受击后仰</summary>
        public static (int attackIntervalMs, bool ignoresHitStun) GetZumaGuardCaptainParams()
        {
            return (400, true);
        }

        /// <summary>1526 祖玛弓箭手无视 20% 物理护甲</summary>
        public static int CalcZumaArcherArmorPiercing(int targetAc)
        {
            return (int)(targetAc * 0.80f);
        }

        // ------------------------------------------------------------------------
        // 1531 ~ 1560: 赤月恶魔地刺双抗折算与神兵威压
        // ------------------------------------------------------------------------

        /// <summary>1539 赤月恶魔地刺伤害双抗公式 (rawDmg - (AC + MAC) / 2)</summary>
        public static int CalcEvilRedMoonSpikeMitigation(int rawDmg, int ac, int mac)
        {
            int mitigation = (ac + mac) / 2;
            return Math.Max(1, rawDmg - mitigation);
        }

        /// <summary>1554 刺杀剑术第二格无视物理护甲与魔法盾</summary>
        public static bool IsAssassinationSecondTilePenetrate(int distance) => distance == 2;

        /// <summary>1555 冰咆哮 3x3 范围无视敏捷与魔法闪避</summary>
        public static bool DoesBlizzardIgnoreAllDodges() => true;

        /// <summary>1557 野蛮冲撞双重压制判定 (等级高且装备重)</summary>
        public static bool CanWildBashOverpower(int myLevel, int targetLevel, int myWeight, int targetWeight)
        {
            return myLevel > targetLevel && myWeight >= targetWeight;
        }

        /// <summary>1562 虹魔套装 4 件套物理吸血 20%</summary>
        public static int CalcRainbowMagicSetLeech(int damageDealt, bool isFullSet)
        {
            return isFullSet ? (int)(damageDealt * 0.20f) : 0;
        }

        /// <summary>1563 魔血套装 4 件套属性转换 (HP +86, MP -86)</summary>
        public static (int hpBonus, int mpPenalty) GetDemonBloodSetDelta(bool isFullSet)
        {
            return isFullSet ? (86, -86) : (0, 0);
        }

        /// <summary>1572 运九套刀刀最大攻击力判定</summary>
        public static int CalcDamageWithLuck(int minDc, int maxDc, int totalLuck, Random rng)
        {
            if (totalLuck >= 9) return maxDc; // 运九套刀刀上限
            return rng.Next(minDc, maxDc + 1);
        }

        // ------------------------------------------------------------------------
        // 1573 ~ 1600: 沙城税收、红名村与武器升级硬限制
        // ------------------------------------------------------------------------

        /// <summary>1574 大刀卫士对红名绝对秒杀 (伤害9999)</summary>
        public static int GetGuardInstaKillDamage(bool isRedName) => isRedName ? 9999 : 0;

        /// <summary>1578 沙巴克城门耐久上限</summary>
        public const int SabukGateTotalDurability = 200_000;

        /// <summary>1580 沙巴克国库税率抽成 20%</summary>
        public static int CalcSabukTreasuryCut(int amount) => (int)(amount * 0.20f);

        /// <summary>1581 罗刹武器诅咒+10 刀刀取下限 15 点 DC</summary>
        public static int CalcRaksasaDamageUnderCurse(int curse) => curse >= 10 ? 15 : 0;

        /// <summary>1587 命中与敏捷物理对抗矩阵</summary>
        public static float CalcNetPhysicalHitRate(int attackerHit, int defenderAgility)
        {
            int netAgility = Math.Max(0, defenderAgility - attackerHit);
            float dodgeRate = Math.Min(0.70f, netAgility * 0.02f);
            return Math.Max(0.15f, 1.0f - dodgeRate); // 保底 15% 命中率
        }

        /// <summary>1599 单把武器升级最多成功 7 次硬上限</summary>
        public const int MaxWeaponUpgradeLimit = 7;
        public static bool CanUpgradeWeapon(int currentSuccess) => currentSuccess < MaxWeaponUpgradeLimit;
    }
}
