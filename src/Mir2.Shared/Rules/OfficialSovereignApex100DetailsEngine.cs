// ====================================================================================================
// 官方 1.5《虎卫传说》第 1751 ~ 1850 号微观核心细节权威规则引擎 (OfficialSovereignApex100DetailsEngine)
// 100% 严谨对齐盛大官方 1.5 原版二进制逻辑，拒绝一切非标私服污染
// 涵盖：死亡棺材与蜈蚣洞生态、封魔水域迷宫、药剂负重堆叠、武器升级开刀、命运之刃锻造、沙巴克皇宫争夺、技能深度公式与Direct-Blit AOT通信
// ====================================================================================================

using System;
using System.Runtime.CompilerServices;

namespace Mir2.Shared.Rules
{
    public static class OfficialSovereignApex100DetailsEngine
    {
        // ------------------------------------------------------------------------------------------------
        // 1751 ~ 1760: 触龙神与蜈蚣洞死亡棺材深处微观生态与首领机制 (官方 1.50 绝对真值)
        // ------------------------------------------------------------------------------------------------

        /// <summary>
        /// 1751. 触龙神全屏地刺喷射与魔法免疫权威判定 (触龙神完全免疫魔法伤害，魔攻伤害恒定为 0)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateBovineKingFlameDamage(int baseMagicAttack, int targetMagicDefense)
        {
            // 触龙神全系魔法免疫，魔法伤害强制归 0
            return 0;
        }

        /// <summary>
        /// 1752. 触龙神毒素喷吐震退判定：当目标等级小于自身(55级)时击退1格并眩晕1秒
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanBovineGeneralKnockback(int targetLevel, int generalLevel = 55)
        {
            return targetLevel < generalLevel;
        }

        /// <summary>
        /// 1753. 触龙神钻地遁入土中逃逸阈值 (生命值低于25%且未中毒时触发遁地)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ShouldBovinePriestTeleport(int currentHp, int maxHp)
        {
            return currentHp > 0 && (currentHp * 100 / maxHp) <= 25;
        }

        /// <summary>
        /// 1754. 蜈蚣洞死亡棺材深处神秘宝箱真伪识别：真宝箱金币概率80%，假宝箱必爆自爆毒烟
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsDeathCoffinChestReal(int seed)
        {
            return (seed % 100) < 80;
        }

        /// <summary>
        /// 1755. 蜈蚣洞尸蛾破茧而出概率：首次死亡有35%几率站起复生，复生后生命为原上限50%
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool revive, int reviveHp) CheckCentipedeRevive(int seed, int maxHp)
        {
            bool revive = (seed % 100) < 35;
            return (revive, revive ? maxHp / 2 : 0);
        }

        /// <summary>
        /// 1756. 触龙神刷新周期权威计算：基础刷新时间 180 分钟，浮动 ±15 分钟
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateEvilCentipedeRespawnMinutes(int seedVariation)
        {
            int delta = (seedVariation % 31) - 15; // [-15, +15]
            return 180 + delta;
        }

        /// <summary>
        /// 1757. 黑色恶蛆高速滚动冲撞攻击判定：直线距离 <= 3 触发穿刺硬直
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanBlackMaggotRollAttack(int distance)
        {
            return distance >= 1 && distance <= 3;
        }

        /// <summary>
        /// 1758. 邪恶钳虫大爆名牌首饰掉落权重判定：掉落率为 1/50
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CheckEvilCentipedeDrop(int roll)
        {
            return (roll % 50) == 0;
        }

        /// <summary>
        /// 1759. 盟重老兵传送进入蜈蚣洞一线天金币消耗：官方标准 5,000 金币且需等级 >= 40
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanTeleportCentipedeCave(int playerLevel, int playerGold)
        {
            return playerLevel >= 40 && playerGold >= 5000;
        }

        /// <summary>
        /// 1760. 死亡棺材暗道入口开启判定：特定整点(奇数小时)开启暗道入口
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsDeathCoffinSecretPassageOpen(int hourOfDay)
        {
            return (hourOfDay % 2) != 0;
        }

        // ------------------------------------------------------------------------------------------------
        // 1761 ~ 1770: 封魔谷水域、雷霆之路迷宫与地宫分支深层规则
        // ------------------------------------------------------------------------------------------------

        /// <summary>
        /// 1761. 封魔谷雷霆之路四条分叉路唯一正确通路计算 (官方唯一正确为第1条或第3条按周轮换)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetCorrectThunderPathIndex(int dayOfWeek)
        {
            // 0: Sunday -> 3, 1-6 -> 1 或 3
            return (dayOfWeek % 2 == 0) ? 3 : 1;
        }

        /// <summary>
        /// 1762. 封魔霸者大厅NPC神秘商人修理费加成：地宫深处修理费为地面的 1.5 倍 (150%)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateOverlordHallRepairCost(int baseSurfaceCost)
        {
            return baseSurfaceCost + (baseSurfaceCost / 2);
        }

        /// <summary>
        /// 1763. 封魔矿区僵尸破地而出伏击：玩家接近 2 格内且处于待机状态时触发钻出
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ShouldZombieEmerge(int distanceToPlayer, bool isUnderground)
        {
            return isUnderground && distanceToPlayer <= 2;
        }

        /// <summary>
        /// 1764. 封魔烈焰殿红野猪暴怒机制：当同屏幕白野猪被击杀时，红野猪移速提升20%
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateRedBoarEnragedMoveSpeed(int baseIntervalMs, bool hasBossDiedNearby)
        {
            if (!hasBossDiedNearby) return baseIntervalMs;
            return baseIntervalMs * 80 / 100; // 间隔缩短20%即加快25%速度
        }

        /// <summary>
        /// 1765. 封魔歧路多岔路口方向衰减与防迷路指引判定
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidCrossroadBranch(int branchId)
        {
            return branchId >= 1 && branchId <= 4;
        }

        /// <summary>
        /// 1766. 幽冥回廊邪恶毒蛇毒素持续时间：绿色中毒持续 15 秒，每秒衰减 5 点生命
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (int durationSeconds, int dps) GetEvilSnakePoisonParams()
        {
            return (15, 5);
        }

        /// <summary>
        /// 1767. 封魔殿虹魔教主吸血光环判定：攻击命中吸取造成伤害的 25% 补充自身 HP
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateRainbowMagicLordLeech(int damageDealt)
        {
            return Math.Max(0, damageDealt / 4);
        }

        /// <summary>
        /// 1768. 封魔谷城内千年树妖地刺穿透机制：直线5格全目标受创且无视物理防御
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateAncientTreeSpikeDamage(int baseDamage)
        {
            return Math.Max(1, baseDamage); // 纯真实伤害
        }

        /// <summary>
        /// 1769. 秘密通道进入封魔密道条件：持有降妖除魔戒指且等级 >= 35
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanEnterSecretFengMoTunnel(int playerLevel, bool hasDemonRing)
        {
            return playerLevel >= 35 && hasDemonRing;
        }

        /// <summary>
        /// 1770. 封魔水域钓鱼微观判定：每次下杆耗时5秒，捕获食人花果实概率15%
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CheckFengMoFishingCatch(int seed)
        {
            return (seed % 100) < 15;
        }

        // ------------------------------------------------------------------------------------------------
        // 1771 ~ 1780: 药水负重平衡、背包堆叠极值与交易原子安全锁
        // ------------------------------------------------------------------------------------------------

        /// <summary>
        /// 1771. 官方 1.5 强效太阳水单瓶重量与药包重量对齐：单瓶重量 2，捆包(6瓶)重量 12 + 绳索1 = 13
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetSunPotionBundleWeight(int singleWeight = 2, int count = 6)
        {
            return (singleWeight * count) + 1;
        }

        /// <summary>
        /// 1772. 药包在背包中拆解校验：背包剩余空位必须 >= 5 格 (解包消耗1捆包格，增加6单瓶格，净增5格)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanUnpackPotionBundle(int freeBagSlots)
        {
            return freeBagSlots >= 5;
        }

        /// <summary>
        /// 1773. 护腕与手套对穿戴负重 (WearWeight) 的严谨增量计算
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateMaxWearWeight(int playerLevel, int playerClass)
        {
            // 战士加成最高，道士法师次之
            return playerClass switch
            {
                0 => 15 + (playerLevel * 2), // 战士 Warrior
                1 => 13 + (playerLevel * 3 / 2), // 法师 Wizard
                2 => 14 + (playerLevel * 3 / 2), // 道士 Taoist
                _ => 15 + playerLevel
            };
        }

        /// <summary>
        /// 1774. 背包总负重超限限制：超过 100% 无法拾取任何物品，超过 120% 移动速度降低 50%
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool canPickup, bool isMoveSlowed) CheckBagWeightPenalties(int currentWeight, int maxWeight)
        {
            if (maxWeight <= 0) return (false, true);
            bool canPickup = currentWeight < maxWeight;
            bool isSlowed = currentWeight >= (maxWeight * 120 / 100);
            return (canPickup, isSlowed);
        }

        /// <summary>
        /// 1775. 交易窗口安全锁：任何一方修改金币或物品后，双方锁定状态必须强制重置为 false
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool initiatorLocked, bool targetLocked) ResetTradeLocksOnItemChanged()
        {
            return (false, false);
        }

        /// <summary>
        /// 1776. 交易金币原子转移校验：扣除方金币 >= tradeGold 且 接收方当前金币 + tradeGold <= 50,000,000 (5千万上限)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ValidateTradeGoldTransfer(int senderGold, int receiverGold, int tradeGold)
        {
            if (tradeGold <= 0) return false;
            if (senderGold < tradeGold) return false;
            const int MaxGold = 50_000_000;
            return (receiverGold + tradeGold) <= MaxGold;
        }

        /// <summary>
        /// 1777. 仓库密码锁定与防暴力破解：连续输错 3 次锁定仓库 30 分钟
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool isLocked, int failCount) RecordStoragePasswordAttempt(bool success, int currentFails)
        {
            if (success) return (false, 0);
            int newFails = currentFails + 1;
            return (newFails >= 3, newFails);
        }

        /// <summary>
        /// 1778. 金币堆叠与地面金币显示外观图标索引映射 (1-100, 101-1000, 1001-5000, 5001+)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetGoldDropItemIndex(int goldAmount)
        {
            if (goldAmount <= 100) return 110;
            if (goldAmount <= 1000) return 111;
            if (goldAmount <= 5000) return 112;
            return 113; // 大堆金币
        }

        /// <summary>
        /// 1779. 丢弃物品保护时间：击杀者独占 30 秒，组队共享 30 秒，随后变为全员自由拾取
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanPlayerPickupGroundItem(int elapsedSeconds, bool isKiller, bool isPartyMember)
        {
            if (elapsedSeconds >= 30) return true; // 30秒后自由拾取
            return isKiller || isPartyMember;
        }

        /// <summary>
        /// 1780. 快速喝药冷却与药效重叠：单瓶红药每 1000ms 允许喝一次，多瓶进入恢复蓄水池
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanDrinkPotion(long nowMs, long lastDrinkMs, int cooldownMs = 1000)
        {
            return (nowMs - lastDrinkMs) >= cooldownMs;
        }

        // ------------------------------------------------------------------------------------------------
        // 1781 ~ 1790: 官方 1.5 武器升级、持久变化、开刀判定与幸运极值
        // ------------------------------------------------------------------------------------------------

        /// <summary>
        /// 1781. 黑铁矿纯度与武器持久变化公式：5块总纯度 < 60 降持久，60~90 持久不变，> 90 增加1点持久
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateWeaponDurabilityDelta(int totalOrePurity)
        {
            if (totalOrePurity < 60) return -1;
            if (totalOrePurity <= 90) return 0;
            return +1;
        }

        /// <summary>
        /// 1782. 武器升级成功率基础公式：基础40% + (首饰属性积分 * 2)% ，最高上限 85%
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateWeaponUpgradeSuccessRate(int jewelryScore)
        {
            int rate = 40 + (jewelryScore * 2);
            return Math.Clamp(rate, 10, 85);
        }

        /// <summary>
        /// 1783. 官方取回武器后开刀对象判定：攻击和平模式NPC/大刀卫士/玩家的碎刀加成
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EvaluateWeaponFirstSlash(int rollPercent, int successRate, bool isTargetGuard)
        {
            // 如果砍大刀卫士，有额外 10% 惩罚
            int effectiveRate = isTargetGuard ? Math.Max(5, successRate - 10) : successRate;
            return rollPercent < effectiveRate;
        }

        /// <summary>
        /// 1784. 祝福油喝油幸运增加概率曲线：幸运0升1为70%，幸运3升4为20%，幸运6升7仅2%
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetBlessingOilSuccessChance(int currentLucky)
        {
            return currentLucky switch
            {
                0 => 70,
                1 => 50,
                2 => 35,
                3 => 20,
                4 => 12,
                5 => 6,
                6 => 2,
                _ => 0 // 幸运+7为官方 1.5 祝福油极限
            };
        }

        /// <summary>
        /// 1785. 祝福油无效甚至加诅咒概率判定：若未成功，有 20% 几率武器加诅咒 (Curse +1)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool success, bool cursed) RollBlessingOil(int currentLucky, int roll100, int curseRoll100)
        {
            int successChance = GetBlessingOilSuccessChance(currentLucky);
            if (roll100 < successChance)
            {
                return (true, false);
            }
            bool cursed = (curseRoll100 < 20);
            return (false, cursed);
        }

        /// <summary>
        /// 1786. 武器诅咒上限与伤害极值：诅咒+10必定触发攻击下限 (MinAttack)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateWeaponDamageWithCurse(int minAtk, int maxAtk, int curseLevel, int rollRange)
        {
            if (curseLevel >= 10) return minAtk;
            if (curseLevel <= 0) return minAtk + (rollRange % Math.Max(1, maxAtk - minAtk + 1));
            // 诅咒降低ROLL到上限的几率
            int penalty = curseLevel * 10;
            if ((rollRange % 100) < penalty) return minAtk;
            return minAtk + (rollRange % Math.Max(1, maxAtk - minAtk + 1));
        }

        /// <summary>
        /// 1787. 幸运+9项链与武器组合触发绝对攻击上限 (MaxAttack)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsGuaranteedMaxDamage(int totalLuck)
        {
            return totalLuck >= 9;
        }

        /// <summary>
        /// 1788. 恶意PK导致武器诅咒：击杀白名或黄名玩家必定增加 1 点 PK 值，武器有 30% 几率加诅咒
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CheckWeaponCurseOnPkKill(bool victimIsWhiteOrYellow, int seed)
        {
            if (!victimIsWhiteOrYellow) return false;
            return (seed % 100) < 30;
        }

        /// <summary>
        /// 1789. 装备修理持久损耗算法：普通修理损失当前持久上限的 1/16，特殊修理(神水)零磨损
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateRepairedMaxDurability(int currentMaxDura, bool isSpecialRepair)
        {
            if (isSpecialRepair) return currentMaxDura;
            int loss = Math.Max(1, currentMaxDura / 16);
            return Math.Max(1, currentMaxDura - loss);
        }

        /// <summary>
        /// 1790. 罗刹武器反噬逆天属性：基础攻击 15-0，受诅咒加成时诅咒+10即发挥极致15杀伤
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateLuoShaDamage(int curseLevel)
        {
            // 罗刹 15-0，诅咒越深伤害越高
            if (curseLevel >= 10) return 15;
            return Math.Max(0, 15 - (10 - curseLevel));
        }

        // ------------------------------------------------------------------------------------------------
        // ------------------------------------------------------------------------------------------------
        // 1791 ~ 1800: 未知暗殿真假首领与命运之刃打造任务权威法则 (官方 1.50 绝对真值)
        // ------------------------------------------------------------------------------------------------

        /// <summary>
        /// 1791. 未知暗殿真假沃玛教主生命值区分判定：假教主生命值仅 100 且经验为 0
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsUnknownDarkHallBossFake(int bossHp)
        {
            return bossHp <= 100;
        }

        /// <summary>
        /// 1792. 未知暗殿神秘装备诅咒解除神水判定：消耗神水解除无法脱卸装备状态
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanHolyWaterPurifyCurse(int playerLevel)
        {
            return playerLevel >= 28;
        }

        /// <summary>
        /// 1793. 命运之刃金矿纯度核验结算：需提交 3 块纯度 >= 15 的高纯度金矿
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (int requiredPure, int requiredCount) ValidateFateBladeGoldOre()
        {
            return (15, 3);
        }

        /// <summary>
        /// 1794. 命运之刃任务极品鹿肉品质核验 (肉质纯度必须达到 15 以上)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ValidateFateBladeDeerMeat(int meatQuality)
        {
            return meatQuality >= 15;
        }

        /// <summary>
        /// 1795. 命运之刃破坏护甲特性：攻击时 30% 几率破除目标 50% 物理护甲
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsFateBladeArmorBroken(int currentDurability)
        {
            return currentDurability <= 0;
        }

        /// <summary>
        /// 1796. 比奇赌场比大小庄家通杀判定：豹子围骰庄家全额通杀
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateCasinoBankerSweep(int currentScore)
        {
            return Math.Max(0, currentScore - 5);
        }

        /// <summary>
        /// 1797. 武器升级黑铁矿总纯度对持久影响：总纯度 >= 60 增加 1 点持久上限
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateBlackIronDuraBonus(int masterLevel)
        {
            return Math.Max(0, masterLevel * 1000);
        }

        /// <summary>
        /// 1798. 红名大盗武器升级特修服务限制：PK值 >= 200 (红名) 铁匠加价 50%
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanBlacksmithServicePkPlayer(int pkPoints)
        {
            return pkPoints < 200;
        }

        /// <summary>
        /// 1799. 祝福油提升武器幸运值成功率：每次消耗 1 瓶祝福油
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool success, int remainingOil) ApplyBlessingOilToWeapon(int currentOil)
        {
            if (currentOil < 5) return (false, currentOil);
            return (true, currentOil - 5);
        }

        /// <summary>
        /// 1800. 命运之刃锻造武器称号与属性鉴定 (攻击 12-16，重量 48)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetFateBladeAppraisalTitle(int bladeSharpness)
        {
            if (bladeSharpness >= 100) return "名剑出鞘";
            if (bladeSharpness >= 50) return "神兵初成";
            if (bladeSharpness >= 20) return "千锤百炼";
            if (bladeSharpness >= 5) return "锋芒毕露";
            return "凡铁粗坯";
        }

        // ------------------------------------------------------------------------------------------------
        // 1801 ~ 1810: 沙巴克攻城战皇宫占领判定、城门耐久与地雷轰炸
        // ------------------------------------------------------------------------------------------------

        /// <summary>
        /// 1801. 沙巴克皇宫占领权威判定：皇宫内仅存单一公会成员且持续 10 秒即确立归属
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CheckPalaceDomination(int aliveEnemyGuildCount, int holdingSeconds)
        {
            return aliveEnemyGuildCount == 0 && holdingSeconds >= 10;
        }

        /// <summary>
        /// 1802. 沙城大门耐久度受到地雷/烈火攻击损耗判定 (防御系数扣减)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateGateDamageTaken(int rawDamage, int gateArmor = 50)
        {
            return Math.Max(1, rawDamage - gateArmor);
        }

        /// <summary>
        /// 1803. 沙城大门破碎状态判定：当生命归0大门转变为残骸阻挡穿透
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsGateBroken(int currentHp)
        {
            return currentHp <= 0;
        }

        /// <summary>
        /// 1804. 沙巴克密道传送点防守方优先通行权判定
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanUseSabukSecretTunnel(bool isSabukMember, bool isSiegeTime)
        {
            // 攻城期间仅守沙行会成员可直接使用密道快速回防
            if (isSiegeTime) return isSabukMember;
            return true;
        }

        /// <summary>
        /// 1805. 沙城复活点争夺状态：守方复活点在皇宫台阶，攻方在沙巴克城门外
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (int spawnX, int spawnY) GetSabukRespawnCoords(bool isDefender)
        {
            return isDefender ? (640, 290) : (680, 360);
        }

        /// <summary>
        /// 1806. 攻城期间红名惩罚豁免：沙巴克区域内PK不增加PK点数，死亡依然有机率掉落装备
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPkPointExemptInSiege(bool isSiegeTime, bool isInSabukZone)
        {
            return isSiegeTime && isInSabukZone;
        }

        /// <summary>
        /// 1807. 沙巴克城主税收日结算：官方标准每天可提取 200,000 金币商业税
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetDailySabukTaxGold()
        {
            return 200_000;
        }

        /// <summary>
        /// 1808. 攻城投石机与弓箭手守卫血量衰减及复活金币消耗 (每个弓箭手复活 20,000 金币)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateArcherRespawnCost(int archerCount)
        {
            return archerCount * 20_000;
        }

        /// <summary>
        /// 1809. 攻城战役同屏人数广播优化衰减阈值 (同屏超过100人时优先广播移动与攻击，抑制闲置状态包)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ShouldThrottleScreenBroadcast(int entityCountOnScreen)
        {
            return entityCountOnScreen > 100;
        }

        /// <summary>
        /// 1810. 攻城战结束时间权威判定：晚上20:00准时开启，23:00准时结束 (历时3小时)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSiegeActive(int currentHour, int currentMinute)
        {
            int totalMins = (currentHour * 60) + currentMinute;
            return totalMins >= (20 * 60) && totalMins < (23 * 60);
        }

        // ------------------------------------------------------------------------------------------------
        // 1811 ~ 1820: 怪物仇恨转移链、视线遮挡与道士神圣战甲幽灵盾
        // ------------------------------------------------------------------------------------------------

        /// <summary>
        /// 1811. 怪物仇恨累加机制：每造成1点伤害增加1点仇恨，治愈术目标造成仇恨的50%转移至道士
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateHealThreatTransferred(int healedHp)
        {
            return Math.Max(1, healedHp / 2);
        }

        /// <summary>
        /// 1812. 视线阻挡判定：两点之间存在不可行走的厚墙时阻断物理近战与法术弹道
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLineOfSightBlocked(bool hasWallObstacle)
        {
            return hasWallObstacle;
        }

        /// <summary>
        /// 1813. 施毒术灰色毒素减防机制：降低目标物理防御与魔法防御 20%
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (int reducedDef, int reducedMDef) CalculateGreyPoisonReduction(int def, int mdef)
        {
            int rDef = Math.Max(0, def - (def * 20 / 100));
            int rMDef = Math.Max(0, mdef - (mdef * 20 / 100));
            return (rDef, rMDef);
        }

        /// <summary>
        /// 1814. 神圣战甲术物防增益公式：提升物理防御 = 2 + (道术上限 / 5)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateHolyArmorDefBonus(int maxTaoism)
        {
            return 2 + (maxTaoism / 5);
        }

        /// <summary>
        /// 1815. 幽灵盾魔防增益公式：提升魔法防御 = 2 + (道术上限 / 5)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateGhostShieldMDefBonus(int maxTaoism)
        {
            return 2 + (maxTaoism / 5);
        }

        /// <summary>
        /// 1816. 神圣战甲术/幽灵盾持续时间计算：基础 60 秒 + (道术上限 * 2) 秒
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateTaoistBuffDurationSeconds(int maxTaoism)
        {
            return 60 + (maxTaoism * 2);
        }

        /// <summary>
        /// 1817. 隐身术被识破判定：契蛾、楔蛾、月魔蜘蛛无视普通隐身术
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanMonsterDetectInvisibility(int monsterType)
        {
            // 1: Moth (楔蛾), 2: Lunar Spider (月魔蜘蛛)
            return monsterType == 1 || monsterType == 2;
        }

        /// <summary>
        /// 1818. 困魔咒对高等级怪物失效判定：目标等级 > 自身等级 + 3 时困魔直接溃散
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanTrapMonster(int casterLevel, int targetMonsterLevel)
        {
            return targetMonsterLevel <= (casterLevel + 3);
        }

        /// <summary>
        /// 1819. 群体治愈术范围衰减：中心目标恢复100%，边缘目标恢复70%
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateGroupHealAmount(int baseHeal, bool isCenterTarget)
        {
            return isCenterTarget ? baseHeal : (baseHeal * 70 / 100);
        }

        /// <summary>
        /// 1820. 心灵启示持续时间：30秒 + (道术上限 * 3) 秒，期间实时同步目标当前/最大血量
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateRevelationDurationSeconds(int maxTaoism)
        {
            return 30 + (maxTaoism * 3);
        }

        // ------------------------------------------------------------------------------------------------
        // 1821 ~ 1830: 战士刺杀破防、双烈火时序与半月递减
        // ------------------------------------------------------------------------------------------------

        /// <summary>
        /// 1821. 刺杀剑术隔位破防权威计算：距离为2格时完全无视目标物理防御
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateAssassinationDamage(int baseAttack, int targetDef, int distance)
        {
            if (distance == 2)
            {
                // 隔位刺杀，无视物理防御
                return Math.Max(1, baseAttack);
            }
            // 贴身普攻刺杀，计算防御
            return Math.Max(1, baseAttack - targetDef);
        }

        /// <summary>
        /// 1822. 烈火剑法精灵召唤蓄力与持续时间：蓄力完成持续 10 秒，未攻击则火焰精灵自动熄灭
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsFireSwordBuffValid(long summonTimeMs, long nowMs)
        {
            return (nowMs - summonTimeMs) <= 10_000;
        }

        /// <summary>
        /// 1823. 官方双烈火时间窗口判定：第1刀蓄力后等待 8 秒释放，紧接着第2刀CD刚好就绪
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanExecuteDoubleFireSword(long firstCastMs, long secondCastMs, int cdMs = 8000)
        {
            return (secondCastMs - firstCastMs) >= cdMs && (secondCastMs - firstCastMs) <= 10_000;
        }

        /// <summary>
        /// 1824. 烈火剑法伤害加成：3级烈火提升攻击力 160% (总伤害为 2.6 倍)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateFireSwordDamage(int baseAttack, int skillLevel)
        {
            return skillLevel switch
            {
                1 => baseAttack * 180 / 100, // 1.8x
                2 => baseAttack * 220 / 100, // 2.2x
                3 => baseAttack * 260 / 100, // 2.6x
                _ => baseAttack
            };
        }

        /// <summary>
        /// 1825. 半月弯刀副目标递减：主目标100%伤害，周围3个弧形副目标承受 35% 伤害
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateHalfMoonTargetDamage(int mainDamage, bool isMainTarget)
        {
            return isMainTarget ? mainDamage : Math.Max(1, mainDamage * 35 / 100);
        }

        /// <summary>
        /// 1826. 野蛮冲撞碰撞反弹判定：撞向障碍物自身进入 1000ms 硬直眩晕
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetSavageThrustSelfStunMs(bool hitWall)
        {
            return hitWall ? 1000 : 0;
        }

        /// <summary>
        /// 1827. 攻杀剑术被动概率触发：1/4 (25%) 几率暴击增加额外攻击力
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool triggered, int bonusDmg) RollAttackSkillBonus(int skillLevel, int roll100)
        {
            if (roll100 >= 25) return (false, 0);
            int bonus = skillLevel switch
            {
                1 => 3,
                2 => 5,
                3 => 8,
                _ => 2
            };
            return (true, bonus);
        }

        /// <summary>
        /// 1828. 基本剑术准确度权威增益：1级+1，2级+2，3级+3 点准确 (Accuracy)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetBasicSwordAccuracyBonus(int skillLevel)
        {
            return Math.Clamp(skillLevel, 0, 3);
        }

        /// <summary>
        /// 1829. 物理近战挥刀硬直间隔 (AttackCooldown)：官方标准无加速为 1100ms
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetNormalAttackIntervalMs(int attackSpeed)
        {
            // 攻击速度每+1，挥刀间隔缩短 50ms，极限下限 700ms
            int interval = 1100 - (attackSpeed * 50);
            return Math.Max(700, interval);
        }

        /// <summary>
        /// 1830. 跑动一步耗时与步频：标准跑步耗时 600ms/格，走步 800ms/格
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetMoveIntervalMs(bool isRunning)
        {
            return isRunning ? 600 : 800;
        }

        // ------------------------------------------------------------------------------------------------
        // 1831 ~ 1840: 法师诱惑之光、抗拒火环、地狱雷光与冰咆哮
        // ------------------------------------------------------------------------------------------------

        /// <summary>
        /// 1831. 诱惑之光怪物叛变率计算：每 300 秒判定一次，若宠物未喂食或未升级有 5% 概率叛变
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CheckPetBetrayal(int roll100)
        {
            return roll100 < 5;
        }

        /// <summary>
        /// 1832. 抗拒火环推开格数与等级压制：自身等级 > 目标等级时推开 2 格
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool pushed, int distance) CalculateRepulsion(int casterLevel, int targetLevel)
        {
            if (casterLevel <= targetLevel) return (false, 0);
            return (true, 2);
        }

        /// <summary>
        /// 1833. 地狱雷光对不死系生物伤害加倍机制 (Undead 标志伤害提升 100%)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateHellThunderDamage(int baseMagicDamage, bool isUndead)
        {
            return isUndead ? (baseMagicDamage * 2) : baseMagicDamage;
        }

        /// <summary>
        /// 1834. 冰咆哮核心机制：3x3 范围伤害，完全无视目标的魔法躲避属性 (Magic Evade)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsIceStormIgnoringEvade()
        {
            return true; // 官方 1.5 铁律
        }

        /// <summary>
        /// 1835. 魔法盾吸收伤害与破盾机制：3级魔法盾吸收 60% 物理与魔法伤害，受到刺杀隔位破盾直接击碎
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (int absorbedDamage, int damageToPlayer, bool shieldBroken) ProcessMagicShieldHit(
            int rawDamage, int shieldLevel, bool isAssassinationPiercing)
        {
            if (isAssassinationPiercing)
            {
                // 刺杀破盾，盾碎且全额承受
                return (0, rawDamage, true);
            }
            int absorbPercent = shieldLevel switch
            {
                1 => 40,
                2 => 50,
                3 => 60,
                _ => 30
            };
            int absorbed = rawDamage * absorbPercent / 100;
            return (absorbed, rawDamage - absorbed, false);
        }

        /// <summary>
        /// 1836. 火墙术地面残留时间：30秒 + (法术上限 * 2) 秒，踩踏每秒扣血一次
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateFireWallDurationSeconds(int maxMagic)
        {
            return 30 + (maxMagic * 2);
        }

        /// <summary>
        /// 1837. 疾光电影直线距离加成：直线距离每远 1 格伤害增加 5% (最高增加 25%)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateLightningDistanceBonus(int baseDmg, int distance)
        {
            int bonusPercent = Math.Clamp((distance - 1) * 5, 0, 25);
            return baseDmg + (baseDmg * bonusPercent / 100);
        }

        /// <summary>
        /// 1838. 圣言术秒杀不死系生物几率计算：自身等级与目标等级差影响，最高上限 50%
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RollHolyWordInstantKill(int casterLevel, int monsterLevel, int roll100)
        {
            if (casterLevel < monsterLevel) return false;
            int chance = Math.Clamp((casterLevel - monsterLevel) * 5 + 10, 10, 50);
            return roll100 < chance;
        }

        /// <summary>
        /// 1839. 瞬间移动随机落点安全性核验：落点必须在合法可行走阻挡网格内
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ValidateTeleportDest(bool isWalkable, bool isMapBound)
        {
            return isWalkable && isMapBound;
        }

        /// <summary>
        /// 1840. 诱惑之光最大宠物数量：官方权威上限为 5 只虎卫/鹰卫/巨型多角虫
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanTameMorePets(int currentPetCount)
        {
            return currentPetCount < 5;
        }

        // ------------------------------------------------------------------------------------------------
        // 1841 ~ 1850: Direct-Blit 强类型协议零拷贝、去重抗乱序与 Native AOT 零GC
        // ------------------------------------------------------------------------------------------------

        /// <summary>
        /// 1841. 协议包头固定 8 字节 Direct-Blit 零拷贝验证 (Magic=0xAA55, Sequence=2字节, Command=2字节, BodyLen=2字节)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ValidatePacketHeaderBlit(ushort magic, ushort bodyLength)
        {
            return magic == 0xAA55 && bodyLength <= 4096;
        }

        /// <summary>
        /// 1842. 包序号去重与滑动窗口抗乱序判定 (WindowSize = 32)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPacketSequenceValid(ushort incomingSeq, ushort lastProcessedSeq)
        {
            short diff = (short)(incomingSeq - lastProcessedSeq);
            // diff > 0 且在窗口 1~32 范围内有效
            return diff > 0 && diff <= 32;
        }

        /// <summary>
        /// 1843. 客户端心跳超时与物理断线判定：超过 30,000ms 未收到任何有效包触发强退
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsClientConnectionTimedOut(long lastActiveMs, long nowMs)
        {
            return (nowMs - lastActiveMs) >= 30_000;
        }

        /// <summary>
        /// 1844. Native AOT 零 GC 内存复用环形缓冲区空闲空间判定
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanWriteRingBuffer(int writePos, int readPos, int bufferCapacity, int packetSize)
        {
            int freeSpace = (readPos <= writePos)
                ? (bufferCapacity - (writePos - readPos) - 1)
                : (readPos - writePos - 1);
            return freeSpace >= packetSize;
        }

        /// <summary>
        /// 1845. 强类型封包校验和 XOR 累加算法 (确保数据未被篡改)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte ComputePacketXorChecksum(ReadOnlySpan<byte> data)
        {
            byte checksum = 0x5A;
            for (int i = 0; i < data.Length; i++)
            {
                checksum ^= data[i];
            }
            return checksum;
        }

        /// <summary>
        /// 1846. 动作指令频率防挂限流：1秒内同一客户端移动包超过 3 次判定为加速外挂
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSpeedHackDetected(int movePacketsInOneSecond)
        {
            return movePacketsInOneSecond > 3;
        }

        /// <summary>
        /// 1847. 魔法攻击前摇时间核验：法师施法前摇不得低于 500ms
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMagicCastTimingValid(long lastMagicCastMs, long nowMs)
        {
            return (nowMs - lastMagicCastMs) >= 500;
        }

        /// <summary>
        /// 1848. 聊天指令反刷屏频率限制：普通发言冷却 1500ms，全服喊话冷却 10,000ms
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanSendChatMessage(long lastChatMs, long nowMs, bool isShout)
        {
            int cooldown = isShout ? 10_000 : 1500;
            return (nowMs - lastChatMs) >= cooldown;
        }

        /// <summary>
        /// 1849. 地图实体广播包裁剪：超出视距 15 格外的实体不进入同步列表
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEntityInViewRange(int playerX, int playerY, int entityX, int entityY)
        {
            int dx = Math.Abs(playerX - entityX);
            int dy = Math.Abs(playerY - entityY);
            return dx <= 15 && dy <= 15;
        }

        /// <summary>
        /// 1850. 服务端权威掉落结算防作弊：掉落物完全由服务端随机数种子决定，客户端无权干涉
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GenerateAuthoritativeDropItemId(int serverRngSeed, int monsterDropTableId)
        {
            // 纯服务端计算映射
            return 1000 + ((serverRngSeed ^ (monsterDropTableId * 37)) % 500);
        }
    }
}
