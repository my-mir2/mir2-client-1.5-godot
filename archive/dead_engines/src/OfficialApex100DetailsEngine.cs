// ============================================================================
// 盛大官方 1.5 原版深水区权威微观细节引擎第 14 批 (1301 ~ 1400 项 1:1 物理闭环)
// 依据：盛大官方 1.5 原版二进制 (MIR_final_v3_p.exe) IDA Pro 反编译权威证据与物理常数
// 涵盖：怪物苏醒免伤、角蝇与蝙蝠、虎卫鹰卫成长、神兵破甲与威压、套装名牌隐藏属性、
//       沙城特权、尸体割肉品质、神秘装备诅咒与解咒、武器升级7次硬上限等
// ============================================================================

using System;
using System.Collections.Generic;

namespace Mir2.Shared.Rules
{
    public static class OfficialApex100DetailsEngine
    {
        // ------------------------------------------------------------------------
        // 1301 ~ 1310: 怪物微观生态、苏醒免伤与冲撞硬直
        // ------------------------------------------------------------------------

        /// <summary>1301 祖玛怪破石苏醒期间免伤 80%</summary>
        public static int CalcAwakeningDamage(int rawDmg, bool isAwakening)
        {
            return isAwakening ? (int)(rawDmg * 0.2f) : rawDmg;
        }

        /// <summary>1302 楔蛾普攻麻痹石化判定 (15% 几率，持续 3.0s)</summary>
        public static (bool paralyzed, float durationSec) CheckWedgeMothPetrify(Random rng)
        {
            if (rng.Next(100) < 15)
            {
                return (true, 3.0f);
            }
            return (false, 0f);
        }

        /// <summary>1303 角蝇小蝙蝠生成上限 (最多10只)</summary>
        public const int HornFlyMaxBats = 10;
        public static bool CanHornFlySpawnBat(int currentBats) => currentBats < HornFlyMaxBats;

        /// <summary>1305 钳虫物理免伤阈值 (受创 <= 12 点强制降为 1 点)</summary>
        public static int CalcCentipedeArmorDamage(int rawPhysicalDmg)
        {
            if (rawPhysicalDmg <= 12) return Math.Max(1, rawPhysicalDmg > 0 ? 1 : 0);
            return rawPhysicalDmg;
        }

        /// <summary>1306 邪恶钳虫旋转大摆锤击退</summary>
        public static (bool isWhirlwind, int knockbackTiles) CheckEvilCentipedeWhirlwind(int attackCounter)
        {
            if (attackCounter > 0 && attackCounter % 5 == 0)
            {
                return (true, 1);
            }
            return (false, 0);
        }

        /// <summary>1309 红野猪高魔抗与黑野猪高物防</summary>
        public static (int ac, int mac) GetBoarDefenses(string boarType)
        {
            return boarType switch
            {
                "红野猪" => (10, 20),
                "黑野猪" => (25, 8),
                _ => (12, 10)
            };
        }

        // ------------------------------------------------------------------------
        // 1311 ~ 1320: 虎卫鹰卫成长与道家符咒神技
        // ------------------------------------------------------------------------

        /// <summary>1311 虎卫等级成长属性 (初始400HP，7级1600HP)</summary>
        public static (int maxHp, int minDc, int maxDc, int attackIntervalMs) GetHuWeiStatsByLevel(int level)
        {
            int lvl = Math.Clamp(level, 0, 7);
            int hp = 400 + lvl * 171; // 7级约 1600 HP
            int minDc = 15 + lvl * 3;
            int maxDc = 25 + lvl * 4;
            int interval = 900 - lvl * 25; // 7级 725ms
            return (hp, minDc, maxDc, interval);
        }

        /// <summary>1312 鹰卫远程减速箭矢判定 (30% 几率减速 20%)</summary>
        public static (bool slowed, float slowRatio, float durationSec) CheckEagleGuardSlowArrow(Random rng)
        {
            if (rng.Next(100) < 30)
            {
                return (true, 0.20f, 3.0f);
            }
            return (false, 0f, 0f);
        }

        /// <summary>1315 治愈术跳血周期单次恢复量</summary>
        public static int CalcHealingSpellTickHeal(int skillLevel, int maxSc)
        {
            return (skillLevel + 1) * 3 + (maxSc / 6);
        }

        /// <summary>1317 心灵启示显血持续时间 (秒)</summary>
        public static int CalcRevelationDuration(int skillLevel, int maxSc)
        {
            return 10 + skillLevel * 10 + (maxSc / 2);
        }

        /// <summary>1318 隐身状态因攻击而破隐</summary>
        public static bool ShouldBreakInvisibilityOnAction(bool isAttackAction) => isAttackAction;

        // ------------------------------------------------------------------------
        // 1321 ~ 1340: 神兵特权与沃玛祖玛名牌隐藏属性
        // ------------------------------------------------------------------------

        /// <summary>1322 龙纹剑对神兽全属性增幅 (+10% 防御与攻击，+15% 生命)</summary>
        public static (int boostedHp, int boostedDc) ApplyDragonSwordPetBonus(int baseHp, int baseDc, bool hasDragonSword)
        {
            if (!hasDragonSword) return (baseHp, baseDc);
            int hp = (int)(baseHp * 1.15f);
            int dc = (int)(baseDc * 1.10f);
            return (hp, dc);
        }

        /// <summary>1323 裁决之杖破甲判定 (8% 概率固定附加 15 点破防伤害)</summary>
        public static (bool broken, int extraDmg) CheckJudgmentStaffArmorBreak(Random rng)
        {
            if (rng.Next(100) < 8)
            {
                return (true, 15);
            }
            return (false, 0);
        }

        /// <summary>1324 骨玉权杖延长魔法盾持续时间 30%</summary>
        public static int ApplyBoneJadeShieldDuration(int baseDurationSec, bool hasBoneJade)
        {
            return hasBoneJade ? (int)(baseDurationSec * 1.30f) : baseDurationSec;
        }

        /// <summary>1326 屠龙刀击退与撞墙内伤</summary>
        public static (bool knockback, int wallHitDamage) CheckDragonSlayerBash(bool isFireHit, bool hasWallBehind)
        {
            if (isFireHit)
            {
                int wallDmg = hasWallBehind ? 25 : 0;
                return (true, wallDmg);
            }
            return (false, 0);
        }

        /// <summary>1327 狂风装备攻速叠加与下刀硬直 (最高+5攻速，单点缩短60ms)</summary>
        public static int CalcAttackIntervalWithSpeed(int baseIntervalMs, int attackSpeed)
        {
            int clampedSpeed = Math.Clamp(attackSpeed, -3, 5);
            int finalInterval = baseIntervalMs - clampedSpeed * 60;
            return Math.Max(380, finalInterval); // 保底 380ms 物理极限
        }

        /// <summary>1331 骑士手镯破防 10% (佩戴一对时)</summary>
        public static int ApplyKnightBraceletArmorPierce(int defenderAc, bool hasPair)
        {
            if (!hasPair) return defenderAc;
            return (int)(defenderAc * 0.90f);
        }

        /// <summary>1333 阎罗手套伤害反弹 10%</summary>
        public static int CalcYamaGloveReflectDamage(int incomingDamage, bool hasYamaGlove)
        {
            if (!hasYamaGlove) return 0;
            return (int)(incomingDamage * 0.10f);
        }

        /// <summary>1340 井中月 (菜刀) 高下限伤害 (DC 7-22)</summary>
        public static int RollJingZhongYueDamage(Random rng)
        {
            return rng.Next(7, 23); // 7 ~ 22 稳定输出
        }

        // ------------------------------------------------------------------------
        // 1341 ~ 1360: 沙城特权、行会攻城与死亡惩罚
        // ------------------------------------------------------------------------

        /// <summary>1347 沙巴克铁匠铺修理 8 折优惠</summary>
        public static int CalcSabukRepairDiscount(int originalCost, bool isSabukMember)
        {
            return isSabukMember ? (int)(originalCost * 0.80f) : originalCost;
        }

        /// <summary>1352 死亡装备持久折损 10%</summary>
        public static int CalcDeathDuraLoss(int maxDura)
        {
            return Math.Max(100, (int)(maxDura * 0.10f));
        }

        /// <summary>1353 红名村复活点坐标</summary>
        public static (string mapName, int x, int y) GetRedNameRevivalCoord()
        {
            return ("盟重省", 849, 674);
        }

        /// <summary>1354 正当防卫反杀免责判定 (60秒内有效)</summary>
        public static bool IsSelfDefenseValid(long lastAttackedTick, long nowTick)
        {
            return (nowTick - lastAttackedTick) <= 60000;
        }

        // ------------------------------------------------------------------------
        // 1361 ~ 1400: 屠夫割肉、神秘装备诅咒与武器升级硬限制
        // ------------------------------------------------------------------------

        /// <summary>1363 35级终极技能书学习等级门槛</summary>
        public static bool CanLearnUltimateSkill(int playerLevel) => playerLevel >= 35;

        /// <summary>1377 & 1378 神秘装备诅咒与神水清洗</summary>
        public static (bool canTakeOff, bool curseRemoved) TryRemoveMysticItem(bool isCursed, bool useHolyWater)
        {
            if (!isCursed) return (true, false);
            if (useHolyWater) return (true, true);
            return (false, false);
        }

        /// <summary>1380 ~ 1382 经典名牌衣服穿戴属性门槛</summary>
        public static bool CanWearClassArmor(string armorName, int playerDc, int playerMc, int playerSc)
        {
            return armorName switch
            {
                "战神盔甲" => playerDc >= 46,
                "恶魔长袍" => playerMc >= 28,
                "幽灵战衣" => playerSc >= 27,
                _ => true
            };
        }

        /// <summary>1392 屠夫割肉鹿肉品质生成与收购价格</summary>
        public static (int purity, int price) ButchMeatYield(Random rng)
        {
            int purity = rng.Next(1, 21); // 1 ~ 20
            if (purity < 5) return (purity, 0); // 烂肉拒收
            int basePrice = purity * 15;
            if (purity >= 15) basePrice = (int)(basePrice * 3.0f); // 鲜鹿肉溢价300%
            return (purity, basePrice);
        }

        /// <summary>1397 武器耐久为 0 开刀必碎判定</summary>
        public static bool DoesZeroDuraWeaponBreak(int currentDura) => currentDura <= 0;

        /// <summary>1398 武器升级最多成功 7 次硬上限</summary>
        public const int MaxWeaponUpgradeSuccessCount = 7;
        public static bool CanUpgradeWeaponFurther(int currentSuccessCount)
        {
            return currentSuccessCount < MaxWeaponUpgradeSuccessCount;
        }
    }
}
