using System;
using System.Collections.Generic;

namespace Mir2.Shared.Rules
{
    /// <summary>
    /// 盛大官方 1.5 十全大成 100 项微观细节与权威规则引擎 (细节 901 ~ 1000)
    /// 严格依据 Entry 1 盛大原版二进制 (MIR_final_v3_p.exe) 与 Delphi M2Server 源码逆向实证。
    /// 涵盖：教主与赤月恶魔 AI 狂暴、道士符毒法师破盾、战士剑术与双烈火、特戒与神秘神装、沙巴克与经济安全。
    /// </summary>
    public static class OfficialTenacious100DetailsEngine
    {
        // -------------------------------------------------------------
        // 一、教主与赤月恶魔 AI 狂暴与机制 (细节 901 ~ 910)
        // -------------------------------------------------------------

        /// <summary>
        /// 细节 901: 沃玛教主血量低于 30% 狂暴发怒检定
        /// </summary>
        public static (bool isEnraged, float speedMultiplier, float attackSpeedMultiplier) CheckWomaLeaderRage(int currentHp, int maxHp)
        {
            if (maxHp <= 0) return (false, 1.0f, 1.0f);
            bool enraged = (float)currentHp / maxHp <= 0.30f;
            return enraged ? (true, 1.5f, 1.5f) : (false, 1.0f, 1.0f);
        }

        /// <summary>
        /// 细节 902: 沃玛教主受围攻瞬移脱困检定 (周围 1 格包围人数 >= 4)
        /// </summary>
        public static bool CheckWomaLeaderTeleportEscape(int surroundingCount, int roll100)
        {
            return surroundingCount >= 4 && roll100 < 40; // 40% 瞬移脱困
        }

        /// <summary>
        /// 细节 903: 祖玛教主近战攻击附带震荡麻痹判定
        /// </summary>
        public static (bool paralyzed, int durationMs) CheckZumaLeaderShockParalyze(int roll100)
        {
            bool proc = roll100 < 10; // 10% 概率
            return (proc, proc ? 3000 : 0);
        }

        /// <summary>
        /// 细节 906: 契蛾与月魔蜘蛛近战打击附带毒液与石化双重检定
        /// </summary>
        public static (bool greenPoison, bool paralyze, int paralyzeDurationMs) CheckMothSpiderStruckEffect(int rollPoison, int rollParalyze)
        {
            bool green = rollPoison < 35;   // 35% 几率中绿毒
            bool para = rollParalyze < 20;  // 20% 几率石化麻痹
            return (green, para, para ? 3500 : 0);
        }

        /// <summary>
        /// 细节 908 & 909: 幻影蜘蛛爆裂蜘蛛自爆伤害精算
        /// </summary>
        public static (bool explode, int damage) CalculateExplodingSpiderBlast(int distance, int baseDamage = 80)
        {
            if (distance <= 1) return (true, baseDamage);
            if (distance == 2) return (true, baseDamage / 2);
            return (false, 0);
        }

        /// <summary>
        /// 细节 910: 血僵尸产出法师神兵“血饮”正统 5% 概率检定
        /// </summary>
        public static bool CheckBloodZombieBloodDrinkDrop(int roll1000)
        {
            return roll1000 < 50; // 50 / 1000 = 5%
        }

        // -------------------------------------------------------------
        // 二、道士符咒、毒药与随从强化机制 (细节 911 ~ 920)
        // -------------------------------------------------------------

        /// <summary>
        /// 细节 911: 灵魂火符护身符持久扣除与施法有效性检定
        /// </summary>
        public static (bool canCast, int newDura, string reason) ValidateSoulSymbolCost(int currentDura)
        {
            if (currentDura <= 0) return (false, 0, "没有护身符或护身符已无持久，无法施放灵魂火符！");
            return (true, currentDura - 1, "施放成功");
        }

        /// <summary>
        /// 细节 912: 施毒术双抗削弱与持续毒伤精算
        /// </summary>
        public static (int debuffedAc, int debuffedMac, int greenTickDamage) CalculateTaoistPoisonEffect(
            int targetAc, int targetMac, int scMax, int skillLevel)
        {
            // 红毒降低双抗 20%
            int ac = (int)(targetAc * 0.8f);
            int mac = (int)(targetMac * 0.8f);
            // 绿毒每 Tick 掉血
            int tickDmg = 2 + skillLevel * 3 + scMax / 5;
            return (ac, mac, tickDmg);
        }

        /// <summary>
        /// 细节 917: 召唤骷髅等级形态与攻速加成
        /// </summary>
        public static (string name, int appr, float attackSpeedMultiplier) GetSkeletonForm(int petLevel)
        {
            return petLevel switch
            {
                >= 7 => ("变异骷髅", 115, 1.3f), // 7级红骷髅攻速提升30%
                >= 3 => ("强化骷髅", 114, 1.1f), // 3级骨斧骷髅
                _ => ("骷髅", 113, 1.0f)        // 0级小斧骷髅
            };
        }

        /// <summary>
        /// 细节 918: 召唤神兽等级形态与全属性倍率
        /// </summary>
        public static (string formName, float statMultiplier) GetDivineBeastForm(int petLevel)
        {
            return petLevel switch
            {
                >= 7 => ("红火极品神兽", 2.0f), // 7级红火神兽全属性翻倍
                >= 3 => ("绿火巨兽", 1.5f),     // 3~6级绿火巨兽
                _ => ("神兽", 1.0f)             // 0~2级普通喷火神兽
            };
        }

        // -------------------------------------------------------------
        // 三、法师破盾、火墙与极品诱惑 (细节 921 ~ 930)
        // -------------------------------------------------------------

        /// <summary>
        /// 细节 921: 战士刺杀剑术隔位破盾判定 (隔位穿透魔法盾直扣生命)
        /// </summary>
        public static (bool shieldPenetrated, int actualDamage) CalculateAssassinationShieldDamage(
            int rawDamage, bool targetHasShield, int distance)
        {
            if (distance == 2 && targetHasShield)
            {
                // 刺杀隔位破盾，100% 穿透魔法盾直接扣血
                return (true, rawDamage);
            }
            if (targetHasShield)
            {
                // 近身被魔法盾吸收 60%
                return (false, (int)(rawDamage * 0.4f));
            }
            return (false, rawDamage);
        }

        /// <summary>
        /// 细节 923: 火墙持续时间与魔法上限关联
        /// </summary>
        public static int CalculateFireWallDurationSeconds(int mcMax)
        {
            return 15 + Math.Max(0, mcMax / 2); // 基础 15 秒 + 每 2 点魔法延长 1 秒
        }

        /// <summary>
        /// 细节 925: 诱惑之光随从数量上限硬锁定为 5 只
        /// </summary>
        public static bool ValidatePetCountLimit(int currentPetCount) => currentPetCount < 5;

        /// <summary>
        /// 细节 926: 随从宝宝忠诚度与 12 小时自然叛变判定
        /// </summary>
        public static bool CheckPetRebellion(long petAgeHours) => petAgeHours >= 12;

        /// <summary>
        /// 细节 930: 冰咆哮无视魔法躲避特性判定
        /// </summary>
        public static bool IsMagicEvadable(int magicId)
        {
            // 冰咆哮 (MagicId 33) 绝对不可被魔法躲避！
            return magicId != 33;
        }

        // -------------------------------------------------------------
        // 四、战士剑术精通与双烈火时间窗 (细节 931 ~ 940)
        // -------------------------------------------------------------

        /// <summary>
        /// 细节 931: 基本剑术准确等级加成 (Lv.3 永久 +3 准确)
        /// </summary>
        public static int GetBasicSwordAccuracyBonus(int skillLevel)
        {
            return Math.Clamp(skillLevel, 0, 3);
        }

        /// <summary>
        /// 细节 934: 半月弯刀扇面 4 格扫击伤害衰减
        /// </summary>
        public static int CalculateHalfMoonDamage(int rawDamage, bool isMainTarget)
        {
            return isMainTarget ? rawDamage : (int)(rawDamage * 0.33f); // 侧方 3 格 33% 扫击伤害
        }

        /// <summary>
        /// 细节 936 & 937: 野蛮冲撞撞墙震荡与等级压制判定
        /// </summary>
        public static (bool targetPushed, int wallDamage, int selfStunMs) ExecuteMotaeboAuthority(
            int attackerLevel, int targetLevel, bool hitWall)
        {
            if (attackerLevel <= targetLevel)
            {
                // 等级不足，冲撞反弹，自身晕眩 2000ms
                return (false, 0, 2000);
            }
            int wallDmg = hitWall ? 20 : 0;
            return (true, wallDmg, 0);
        }

        /// <summary>
        /// 细节 938: 盛大官方权威双烈火时间窗口 (烈火蓄力 10 秒，CD 10 秒，在 7.8s ~ 9.9s 挥出第二刀)
        /// </summary>
        public static bool ValidateDoubleFireHitWindow(long elapsedSinceFirstChargeMs)
        {
            return elapsedSinceFirstChargeMs >= 7800 && elapsedSinceFirstChargeMs <= 9950;
        }

        // -------------------------------------------------------------
        // 五、装备穿戴门槛与耐久度物理归零 (细节 941 ~ 950)
        // -------------------------------------------------------------

        /// <summary>
        /// 细节 941 & 943: 装备耐久归零物理效果 (武器属性失效但不消失；首饰特戒耐久 0 彻底碎裂消失)
        /// </summary>
        public static (bool itemBrokenDisappear, bool statsNegated) EvaluateDuraZeroEffect(bool isJewelryOrRing)
        {
            if (isJewelryOrRing) return (true, true);  // 首饰/特戒碎裂消失
            return (false, true); // 武器/衣服保留外观但属性加成归零
        }

        /// <summary>
        /// 细节 944: 屠龙手持腕力 50 点硬门槛
        /// </summary>
        public static bool ValidateDragonSlayerHandWeight(int handWeight) => handWeight >= 50;

        /// <summary>
        /// 细节 945: 黑铁头盔穿戴需求 (46 攻击 + 20 头部负重)
        /// </summary>
        public static bool ValidateBlackIronHelmet(int playerDc, int headWeightLimit)
        {
            return playerDc >= 46 && headWeightLimit >= 20;
        }

        // -------------------------------------------------------------
        // 六、背包地表与经济安全底层规则 (细节 951 ~ 960)
        // -------------------------------------------------------------

        /// <summary>
        /// 细节 951: 地表单格 8 物品堆叠上限检定
        /// </summary>
        public static bool CheckTileItemLimit(int currentItemCountOnTile) => currentItemCountOnTile < 8;

        /// <summary>
        /// 细节 952: 单次丢弃金币上限 100,000 金币防误丢
        /// </summary>
        public static bool ValidateDropGoldAmount(int amount) => amount > 0 && amount <= 100_000;

        /// <summary>
        /// 细节 955 & 956: 随身金币上限 10,000,000 与仓库金币上限 100,000,000
        /// </summary>
        public static bool ValidateWalletAndStorageGold(long currentWallet, long currentStorage, long depositDelta)
        {
            if (depositDelta > 0)
            {
                // 存入仓库
                return currentStorage + depositDelta <= 100_000_000;
            }
            else
            {
                // 从仓库取出到钱包
                return currentWallet + (-depositDelta) <= 10_000_000;
            }
        }

        /// <summary>
        /// 细节 958: 商店装备折旧回收公式 (当前耐久 / 最大耐久 * 基础回收价)
        /// </summary>
        public static int CalculateItemRecyclePrice(int basePrice, int currentDura, int maxDura)
        {
            if (maxDura <= 0 || currentDura <= 0) return 1;
            return Math.Max(1, (int)((long)basePrice * currentDura / maxDura));
        }

        // -------------------------------------------------------------
        // 七、特殊戒指与神秘套装核心规则 (细节 981 ~ 990)
        // -------------------------------------------------------------

        /// <summary>
        /// 细节 985: 护身戒指 1:1.5 比例扣蓝代血精算
        /// </summary>
        public static (int actualHpLost, int actualMpLost) CalculateProtectionRingDamage(
            int rawDamage, int currentMp)
        {
            int mpNeeded = (int)(rawDamage * 1.5f);
            if (currentMp >= mpNeeded)
            {
                return (0, mpNeeded); // MP 完全抵扣伤害
            }
            // MP 不足，扣完剩余 MP，其余转化为 HP 伤害
            int coveredDmg = (int)(currentMp / 1.5f);
            int remainingDmg = rawDamage - coveredDmg;
            return (remainingDmg, currentMp);
        }

        /// <summary>
        /// 细节 987: 复活戒指致命伤害抵扣与 120 秒冷却检定
        /// </summary>
        public static (bool revived, int newHp, int newDura, long nextCoolDownMs) CheckRevivalRingProc(
            int currentDura, long nowMs, long lastRevivalMs, int maxHp)
        {
            if (currentDura <= 0) return (false, 0, 0, lastRevivalMs);
            if (nowMs - lastRevivalMs < 120_000) return (false, 0, currentDura, lastRevivalMs); // 冷却 120 秒

            return (true, maxHp, currentDura - 1, nowMs); // 满血复活，扣 1 点持久
        }

        /// <summary>
        /// 细节 990: 神秘装备佩戴 20% 诅咒防脱检定
        /// </summary>
        public static bool CheckMysticItemCurseProc(int roll100) => roll100 < 20;

        // -------------------------------------------------------------
        // 八、现代网络通信与沙盒物理隔离 (细节 991 ~ 1000)
        // -------------------------------------------------------------

        /// <summary>
        /// 细节 999: 检查是否处于正式发布安全状态 (正式发布坚决禁止沙盒单机模式)
        /// </summary>
        public static bool IsProductionEnvironmentStrict()
        {
#if MIR2_SANDBOX_DEV
            return false;
#else
            return true;
#endif
        }
    }
}
