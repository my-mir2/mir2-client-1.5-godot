using System;
using System.Collections.Generic;

namespace Mir2Client.Game;

/// <summary>
/// 盛大官方 1.5 客户端本地数值、Delphi 伪随机数发生器、命中波动、持久与药水权威全集 (Official 1.5 Client Rules Engine)
/// 严格 1:1 对齐官方 Delphi 原版客户端本地运算逻辑。
/// </summary>
public static class OfficialClientRulesEngine
{
    // ============================================================================
    // 1. Delphi 原生线性同余随机数发生器 (Delphi System.pas Random 1:1 复刻)
    // 种子迭代公式: RandSeed := RandSeed * 0x08088405 + 1
    // ============================================================================
    public sealed class DelphiRandom
    {
        public static DelphiRandom Shared { get; } = new();

        public static int RandomRange(int minVal, int maxVal) => Shared.NextRange(minVal, maxVal);

        private uint _randSeed;

        public DelphiRandom(uint seed = 0)
        {
            _randSeed = seed != 0 ? seed : (uint)Environment.TickCount;
        }

        public uint RandSeed => _randSeed;

        public void SetSeed(uint seed) => _randSeed = seed;

        /// <summary>
        /// 生成 [0, range - 1] 的整数 (1:1 Delphi 原生 Random(range) 汇编指令)
        /// </summary>
        public int Next(int range)
        {
            if (range <= 0) return 0;
            _randSeed = _randSeed * 0x08088405 + 1;
            return (int)(((ulong)_randSeed * (ulong)range) >> 32);
        }

        /// <summary>
        /// 在 [minVal, maxVal] 闭区间内波动取值
        /// </summary>
        public int NextRange(int minVal, int maxVal)
        {
            if (maxVal <= minVal) return minVal;
            return minVal + Next(maxVal - minVal + 1);
        }
    }

    // ============================================================================
    // 2. 角色四维属性波动与命中闪避判定
    // ============================================================================
    public static class CombatMath
    {
        /// <summary>
        /// 物理攻击命中判定 (对齐 IDA sub_4D6E80 / sub_4D7120)
        /// 命中率 = 攻击方准确 / (攻击方准确 + 防守方敏捷 * 0.85)
        /// </summary>
        public static bool CheckPhysicalHit(int attackerAccuracy, int targetAgility, DelphiRandom rng)
        {
            if (attackerAccuracy <= 0) attackerAccuracy = 1;
            if (targetAgility <= 0) targetAgility = 1;

            int hitRatePct = (int)(attackerAccuracy * 100.0 / (attackerAccuracy + targetAgility * 0.85));
            hitRatePct = Math.Clamp(hitRatePct, 15, 95); // 原版存在 5% 绝对闪避与 15% 保底命中

            int roll = rng.Next(100);
            return roll < hitRatePct;
        }

        /// <summary>
        /// 物理伤害计算 (含破防与下限随机浮动)
        /// </summary>
        public static int CalculatePhysicalDamage(int dcMin, int dcMax, int targetAcMin, int targetAcMax, DelphiRandom rng, bool ignoreDefense = false)
        {
            int rawDc = rng.NextRange(dcMin, dcMax);
            if (ignoreDefense) return Math.Max(1, rawDc);

            int targetAc = rng.NextRange(targetAcMin, targetAcMax);
            int finalDmg = rawDc - targetAc;
            return Math.Max(1, finalDmg);
        }

        /// <summary>
        /// 魔法伤害计算 (含目标魔防抵扣)
        /// </summary>
        public static int CalculateMagicDamage(int mcMin, int mcMax, int targetMacMin, int targetMacMax, DelphiRandom rng)
        {
            int rawMc = rng.NextRange(mcMin, mcMax);
            int targetMac = rng.NextRange(targetMacMin, targetMacMax);
            int finalDmg = rawMc - targetMac;
            return Math.Max(1, finalDmg);
        }
    }

    // ============================================================================
    // 3. 装备耐久度与铁匠铺特修/普修折损公式
    // ============================================================================
    public static class DurabilityRules
    {
        /// <summary>
        /// 每次挥刀或受击扣减持久几率 (普通装备 1/30 几率消耗 1 点持久)
        /// </summary>
        public static (bool decayed, int newDura) TryDecayItemDurability(int curDura, DelphiRandom rng, int decayRateDenom = 30)
        {
            if (curDura <= 0) return (false, 0);
            if (rng.Next(decayRateDenom) == 0)
            {
                int nextDura = Math.Max(0, curDura - 1);
                return (true, nextDura);
            }
            return (false, curDura);
        }

        /// <summary>
        /// 装备普通修理 (Normal Repair): 扣减最大持久度上限 1/16
        /// </summary>
        public static (int newDura, int newMaxDura, int goldCost) PerformNormalRepair(int curDura, int maxDura, int itemBasePrice)
        {
            if (maxDura <= 0) return (0, 0, 0);
            int loss = Math.Max(1, maxDura / 16);
            int newMax = Math.Max(1, maxDura - loss);
            int cost = (int)((maxDura - curDura) / (float)maxDura * itemBasePrice * 0.2f);
            return (newMax, newMax, Math.Max(10, cost));
        }

        /// <summary>
        /// 装备特殊修理 (Special Repair): 消耗 3 倍金币，最大持久上限 100% 保持不掉
        /// </summary>
        public static (int newDura, int newMaxDura, int goldCost) PerformSpecialRepair(int curDura, int maxDura, int itemBasePrice)
        {
            int cost = (int)((maxDura - curDura) / (float)Math.Max(1, maxDura) * itemBasePrice * 0.6f);
            return (maxDura, maxDura, Math.Max(50, cost));
        }
    }

    // ============================================================================
    // 4. 药水双轨回复曲线 (瞬回与持续缓回池)
    // ============================================================================
    public static class PotionMetabolism
    {
        public sealed class PotionEffect
        {
            public int InstantHp { get; init; }
            public int InstantMp { get; init; }
            public int HotHpTotal { get; init; }  // Heal-over-time 总量
            public int HotMpTotal { get; init; }
            public int HotDurationSec { get; init; } = 5;
        }

        public static PotionEffect GetPotionProfile(string itemName) => itemName switch
        {
            "强效金创药" => new PotionEffect { InstantHp = 0, HotHpTotal = 90, HotDurationSec = 6 },
            "中量金创药" => new PotionEffect { InstantHp = 0, HotHpTotal = 50, HotDurationSec = 5 },
            "金创药(小量)" => new PotionEffect { InstantHp = 0, HotHpTotal = 25, HotDurationSec = 4 },
            "强效魔法药" => new PotionEffect { InstantMp = 0, HotMpTotal = 120, HotDurationSec = 6 },
            "中量魔法药" => new PotionEffect { InstantMp = 0, HotMpTotal = 70, HotDurationSec = 5 },
            "魔法药(小量)" => new PotionEffect { InstantMp = 0, HotMpTotal = 35, HotDurationSec = 4 },
            "太阳水" => new PotionEffect { InstantHp = 30, InstantMp = 40 },
            "强效太阳水" => new PotionEffect { InstantHp = 50, InstantMp = 80 },
            "万年雪霜" => new PotionEffect { InstantHp = 120, InstantMp = 150 },
            "疗伤药" => new PotionEffect { InstantHp = 250, InstantMp = 250 },
            _ => new PotionEffect()
        };
    }

    // ============================================================================
    // 5. 负重衰减、PK红黄名与守卫仇恨
    // ============================================================================
    public static class JusticeAndLoad
    {
        /// <summary>
        /// 负重对移动与跑动的硬限制
        /// </summary>
        public static (bool canRun, float moveSpeedMultiplier) EvaluateWeightConstraints(
            int curBagWeight, int maxBagWeight, int curWearWeight, int maxWearWeight)
        {
            bool overBag = curBagWeight > maxBagWeight;
            bool overWear = curWearWeight > maxWearWeight;

            if (overBag || overWear)
            {
                // 超重无法助跑，仅能慢速走动，移速衰减 40%
                return (false, 0.60f);
            }
            return (true, 1.0f);
        }

        /// <summary>
        /// 判定角色名颜色 (IDA 0x46A157 SM_CHANGENAMECOLOR)
        /// 0~99 白名, 100~199 黄名, >=200 红名
        /// </summary>
        public static (string colorHex, bool guardHostile) GetPkStatus(int pkPoints)
        {
            if (pkPoints >= 200)
            {
                return ("#FF0000", true); // 红名，大刀守卫秒杀攻击
            }
            if (pkPoints >= 100)
            {
                return ("#FFFF00", false); // 黄名自卫/灰名
            }
            return ("#FFFFFF", false); // 白名
        }
    }

    // ============================================================================
    // 6. 官方 1.5 七大特戒与装备佩戴门槛本地判定引擎
    // ============================================================================
    public static class SpecialItemAndSetRules
    {
        public enum SpecialRingType
        {
            None = 0,
            Paralysis = 1,   // 麻痹戒指 (攻击 1/15 几率麻痹目标 5 秒)
            Protection = 2,  // 护身戒指 (受击以 MP 代扣 HP，比例 1:1.5)
            Revive = 3,      // 复活戒指 (致命伤瞬间恢复满 HP，消耗 1 点持久)
            Invisibility = 4,// 隐身戒指 (怪物完全无视，移动不破隐)
            Teleport = 5,    // 传送戒指 (@move / 客户端点击小地图瞬移)
            Load = 6,        // 超负载戒指 (背包腕力负重上限翻倍)
            Fire = 7         // 火焰戒指 (战士/道士可直接施放小火球)
        }

        public static SpecialRingType IdentifySpecialRing(string itemName) => itemName switch
        {
            "麻痹戒指" => SpecialRingType.Paralysis,
            "护身戒指" => SpecialRingType.Protection,
            "复活戒指" => SpecialRingType.Revive,
            "隐身戒指" => SpecialRingType.Invisibility,
            "传送戒指" => SpecialRingType.Teleport,
            "超负载戒指" => SpecialRingType.Load,
            "火焰戒指" => SpecialRingType.Fire,
            _ => SpecialRingType.None
        };

        /// <summary>
        /// 麻痹戒指攻击判定 (1/15 概率触发 5 秒麻痹硬直)
        /// </summary>
        public static bool CheckParalysisTrigger(DelphiRandom rng) => rng.Next(15) == 0;

        /// <summary>
        /// 护身戒指伤害抵扣计算 (以 MP 吸收伤害)
        /// </summary>
        public static (int absorbedDmg, int remainingDmg, int mpConsumed) CalculateProtectionDamage(int damage, int curMp)
        {
            if (damage <= 0 || curMp <= 0) return (0, damage, 0);
            int neededMp = (int)(damage * 1.5f);
            if (curMp >= neededMp)
            {
                return (damage, 0, neededMp);
            }
            else
            {
                int absorb = (int)(curMp / 1.5f);
                return (absorb, damage - absorb, curMp);
            }
        }

        /// <summary>
        /// 装备穿戴门槛判定 (根据角色等级与三维属性检查是否满足佩戴要求)
        /// </summary>
        public static (bool canWear, string rejectReason) CheckEquipRequirement(
            string itemName, int playerLevel, int dcMax, int mcMax, int scMax)
        {
            if (string.IsNullOrEmpty(itemName)) return (true, string.Empty);

            // 盛大官方 1.5 代表性高阶神兵与重装门槛 (对齐 IDA ItemDB 与 Delphi sub_48A7BC)
            if (itemName.Contains("裁决之杖") && dcMax < 30) return (false, "需要攻击力达到 30 点");
            if (itemName.Contains("骨玉权杖") && mcMax < 17) return (false, "需要魔法力达到 17 点");
            if (itemName.Contains("龙纹剑") && scMax < 17) return (false, "需要道术力达到 17 点");
            if (itemName.Contains("屠龙") && playerLevel < 35) return (false, "需要等级达到 35 级");
            if (itemName.Contains("嗜魂法杖") && playerLevel < 35) return (false, "需要等级达到 35 级");
            if (itemName.Contains("霸者之刃") && playerLevel < 38) return (false, "需要等级达到 38 级");

            // 六大重装 (天魔神甲/圣战宝甲/法神披风/霓裳羽衣/天尊道袍/天师长袍)
            if ((itemName.Contains("天魔神甲") || itemName.Contains("圣战宝甲") ||
                 itemName.Contains("法神披风") || itemName.Contains("霓裳羽衣") ||
                 itemName.Contains("天尊道袍") || itemName.Contains("天师长袍")) && playerLevel < 40)
            {
                return (false, "需要等级达到 40 级");
            }

            return (true, string.Empty);
        }
    }
}
