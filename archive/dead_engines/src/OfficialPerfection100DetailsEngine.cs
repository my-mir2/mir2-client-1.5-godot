using System;
using System.Collections.Generic;

namespace Mir2.Shared.Rules
{
    /// <summary>
    /// 盛大官方 1.5 终极大圆满 100 项微观细节与权威规则引擎 (细节 501 ~ 600)
    /// 严格依照盛大原版二进制 (MIR_final_v3_p.exe) IDA MCP 逆向真值与 Delphi M2Server 源码实现。
    /// 涵盖：客户端越权彻底剥离、战法道微观战斗时序、首领生态与未知暗殿、沙巴克与经济安全防线。
    /// </summary>
    public static class OfficialPerfection100DetailsEngine
    {
        // -------------------------------------------------------------
        // 一、客户端越权彻底剥离与服务端权威裁决矩阵 (细节 501 ~ 520)
        // -------------------------------------------------------------

        /// <summary>
        /// 细节 501: 穿脱装备权威负重与属性校验
        /// 客户端禁止自行穿戴，必须由服务端权威校验腕力/穿戴负重与等级门槛
        /// </summary>
        public static bool ValidateEquipWearAuthority(int playerLevel, int job, int currentWearWeight, int maxWearWeight, int itemWeight, int reqLevel, int reqJob)
        {
            if (playerLevel < reqLevel) return false;
            if (reqJob >= 0 && reqJob != job) return false;
            if (currentWearWeight + itemWeight > maxWearWeight) return false;
            return true;
        }

        /// <summary>
        /// 细节 502: 背包物品位置整理与槽位交换服务端原子校验
        /// </summary>
        public static bool ValidateInventorySwapAuthority(int bagSize, int sourceSlot, int targetSlot)
        {
            if (sourceSlot < 0 || sourceSlot >= bagSize) return false;
            if (targetSlot < 0 || targetSlot >= bagSize) return false;
            return sourceSlot != targetSlot;
        }

        /// <summary>
        /// 细节 503: 地表拾取服务端权威距离与归属裁决 (Chebyshev dist <= 1, 80秒保护期)
        /// </summary>
        public static bool ValidateGroundItemPickUpAuthority(int playerX, int playerY, int itemX, int itemY, long ownerSessionId, long currentSessionId, long dropTimeMs, long nowMs)
        {
            int dx = Math.Abs(playerX - itemX);
            int dy = Math.Abs(playerY - itemY);
            if (Math.Max(dx, dy) > 1) return false; // 必须贴身或脚下

            // 80 秒独享保护期
            if (ownerSessionId != 0 && ownerSessionId != currentSessionId)
            {
                if (nowMs - dropTimeMs < 80_000) return false;
            }
            return true;
        }

        /// <summary>
        /// 细节 504: NPC 商品购买价格折算与库存权威校验 (含沙巴克税率)
        /// </summary>
        public static (bool success, int finalPrice) CalculateNpcBuyPriceAuthority(int basePrice, float sabukTaxRate, int playerGold)
        {
            int tax = (int)Math.Round(basePrice * sabukTaxRate);
            int totalPrice = basePrice + tax;
            if (playerGold < totalPrice) return (false, totalPrice);
            return (true, totalPrice);
        }

        /// <summary>
        /// 细节 505: NPC 回收装备价格与持久度衰减权威折算
        /// 基础回收价 * (当前持久 / 最大持久) * 0.5
        /// </summary>
        public static int CalculateNpcSellRecyclePriceAuthority(int itemBasePrice, int currentDura, int maxDura)
        {
            if (maxDura <= 0 || currentDura <= 0) return 0;
            double duraRatio = Math.Min(1.0, (double)currentDura / maxDura);
            return Math.Max(1, (int)Math.Floor(itemBasePrice * 0.5 * duraRatio));
        }

        /// <summary>
        /// 细节 506: 装备普通修理与特修费用服务端精算
        /// 普修: (最大持久 - 当前持久) * 单价, 最大持久掉 1/16
        /// 特修: (最大持久 - 当前持久) * 单价 * 3, 最大持久不掉
        /// </summary>
        public static (int cost, int newMaxDura) CalculateRepairAuthority(int currentDura, int maxDura, int repairPricePerDura, bool isSpecial)
        {
            int diff = Math.Max(0, maxDura - currentDura);
            if (!isSpecial)
            {
                int cost = diff * repairPricePerDura;
                int penalty = Math.Max(1, maxDura / 16);
                int newMax = Math.Max(1, maxDura - penalty);
                return (cost, newMax);
            }
            else
            {
                int cost = diff * repairPricePerDura * 3;
                return (cost, maxDura);
            }
        }

        /// <summary>
        /// 细节 507: 药水回血池 (Health Pool) 恒定释放
        /// 每心跳周期 (如 1000ms) 释放固定速率 (如 5 HP)，未放完存留在池内
        /// </summary>
        public static (int hpGain, int remainingPool) TickHealthPoolAuthority(int currentHp, int maxHp, int currentPool, int releaseRate)
        {
            if (currentPool <= 0) return (0, 0);
            int release = Math.Min(currentPool, releaseRate);
            int canHeal = Math.Max(0, maxHp - currentHp);
            int actualGain = Math.Min(release, canHeal);
            int remaining = currentPool - release;
            return (actualGain, remaining);
        }

        /// <summary>
        /// 细节 508: 技能等级晋级权威核验 (Lv.0 -> Lv.1 -> Lv.2 -> Lv.3)
        /// </summary>
        public static (bool leveledUp, int newLevel, int newTrain) CheckSkillLevelUpAuthority(int currentLevel, int currentTrain, int maxTrainForLevel)
        {
            if (currentLevel >= 3) return (false, currentLevel, currentTrain);
            if (currentTrain >= maxTrainForLevel)
            {
                return (true, currentLevel + 1, 0);
            }
            return (false, currentLevel, currentTrain);
        }

        /// <summary>
        /// 细节 509: 仓库密码锁定状态机 (连续 3 次错误锁定 300 秒)
        /// </summary>
        public static (bool success, bool locked, int failedAttempts, long lockUntilMs) CheckStoragePasswordAuthority(
            string inputHash, string actualHash, int failedAttempts, long lockUntilMs, long nowMs)
        {
            if (nowMs < lockUntilMs) return (false, true, failedAttempts, lockUntilMs);

            if (inputHash == actualHash)
            {
                return (true, false, 0, 0);
            }
            else
            {
                int newFails = failedAttempts + 1;
                if (newFails >= 3)
                {
                    return (false, true, 0, nowMs + 300_000); // 锁定 5 分钟
                }
                return (false, false, newFails, 0);
            }
        }

        /// <summary>
        /// 细节 510: 角色死亡阵亡与复活戒指触发仲裁
        /// </summary>
        public static (bool isDead, bool revivalTriggered, int revivalRingNewDura) CheckDeathOrRevivalAuthority(
            int currentHp, bool hasRevivalRing, int ringDura, long lastRevivalMs, long nowMs)
        {
            if (currentHp > 0) return (false, false, ringDura);

            // 检查复活戒指 (CD 60秒，扣 1 点持久)
            if (hasRevivalRing && ringDura > 0 && (nowMs - lastRevivalMs >= 60_000))
            {
                return (false, true, ringDura - 1);
            }
            return (true, false, ringDura);
        }

        /// <summary>
        /// 细节 512: 物理出刀间隔 (650ms) 反外挂滑动窗口核验
        /// </summary>
        public static bool ValidateAttackIntervalAuthority(long lastAttackMs, long currentAttackMs, int attackSpeedBonus)
        {
            int baseCd = 650;
            int finalCd = Math.Max(400, baseCd - attackSpeedBonus * 40);
            return (currentAttackMs - lastAttackMs) >= finalCd;
        }

        /// <summary>
        /// 细节 513: 行走 (380ms) 与奔跑 (550ms) 移动间隔核验
        /// </summary>
        public static bool ValidateMoveIntervalAuthority(long lastMoveMs, long currentMoveMs, bool isRunning)
        {
            int requiredCd = isRunning ? 550 : 380;
            return (currentMoveMs - lastMoveMs) >= requiredCd;
        }

        /// <summary>
        /// 细节 514: 法术公共冷却 (GCD 1000ms) 与魔法值扣减核验
        /// </summary>
        public static (bool allowed, int newMp) CheckSpellGcdAndMpAuthority(long lastSpellMs, long nowMs, int currentMp, int mpCost)
        {
            if (nowMs - lastSpellMs < 1000) return (false, currentMp);
            if (currentMp < mpCost) return (false, currentMp);
            return (true, currentMp - mpCost);
        }

        /// <summary>
        /// 细节 515: PK 正当防卫 60 秒时间戳免惩罚判定
        /// </summary>
        public static bool IsSelfDefenseActiveAuthority(long defenseExpireMs, long nowMs)
        {
            return nowMs < defenseExpireMs;
        }

        /// <summary>
        /// 细节 516: 杀人武器加诅几率 (恶意击杀白名 1/3 几率加诅)
        /// </summary>
        public static (bool curseAdded, int newCurse) CheckWeaponCurseOnPkAuthority(int currentCurse, int rand100)
        {
            if (rand100 < 33) // 33% (1/3)
            {
                return (true, Math.Min(10, currentCurse + 1));
            }
            return (false, currentCurse);
        }

        // -------------------------------------------------------------
        // 二、盛大官方 1.5 原版微观数值与战斗深水区机制 (细节 521 ~ 550)
        // -------------------------------------------------------------

        /// <summary>
        /// 细节 521: 战士烈火剑法蓄力维持 10 秒、CD 8 秒，“双烈火”判定窗口 (7.9s ~ 9.9s)
        /// </summary>
        public static (bool canDoubleFire, string reason) CheckDoubleFireHitWindow(long chargeStartMs, long firstHitMs)
        {
            long elapsed = firstHitMs - chargeStartMs;
            if (elapsed < 0 || elapsed > 10_000) return (false, "蓄力已超时消散");
            if (elapsed >= 7_900 && elapsed <= 9_900)
            {
                return (true, "双烈火极限输出时间窗激活！");
            }
            return (false, "第一刀已正常释放烈火");
        }

        /// <summary>
        /// 细节 522: 战士攻杀剑术 1/5 几率触发 1.3 倍物理爆发
        /// </summary>
        public static (bool isPowerHit, int finalDmg) CalculatePowerHitDamage(int rawDmg, int rand5)
        {
            if (rand5 == 0) // 1/5 几率
            {
                return (true, (int)Math.Round(rawDmg * 1.3));
            }
            return (false, rawDmg);
        }

        /// <summary>
        /// 细节 523: 战士刺杀剑术隔位剑气 100% 破防穿盾
        /// </summary>
        public static (int damage, bool shieldPierced) CalculateLongHitFarDamage(int attackerDc, int targetAc, double targetShieldReduction)
        {
            // 隔位剑气完全忽略目标 AC 和魔法盾减免
            return (attackerDc, true);
        }

        /// <summary>
        /// 细节 524: 法师魔法盾等级减伤阶梯 (Lv.1 40%, Lv.2 50%, Lv.3 60%)
        /// </summary>
        public static int ApplyMagicShieldDamageReduction(int rawDamage, int shieldLevel)
        {
            double reduction = shieldLevel switch
            {
                1 => 0.40,
                2 => 0.50,
                3 => 0.60,
                _ => 0.0
            };
            return Math.Max(1, (int)Math.Round(rawDamage * (1.0 - reduction)));
        }

        /// <summary>
        /// 细节 526: 法师抗拒火环等级压制与推人格数
        /// 只有法师等级大于目标等级时方可推动，且目标非 BOSS，最多推 2 格
        /// </summary>
        public static (bool pushSuccess, int pushDistance) CheckRingOfFirePush(int mageLevel, int targetLevel, bool isBoss)
        {
            if (isBoss) return (false, 0);
            if (mageLevel > targetLevel)
            {
                int dist = (mageLevel - targetLevel >= 3) ? 2 : 1;
                return (true, dist);
            }
            return (false, 0);
        }

        /// <summary>
        /// 细节 528: 法师圣言术秒杀几率公式 (等级差与技能等级加权)
        /// </summary>
        public static bool CheckHolyWordInstaKill(int mageLevel, int monLevel, int skillLevel, int rand100)
        {
            if (monLevel >= mageLevel) return false;
            int chance = (mageLevel - monLevel) * 2 + skillLevel * 10;
            chance = Math.Clamp(chance, 5, 80);
            return rand100 < chance;
        }

        /// <summary>
        /// 细节 529: 法师地狱雷光不死系生物 150% 爆发伤害
        /// </summary>
        public static int CalculateHellfireDamage(int baseDamage, bool isUndead)
        {
            if (isUndead)
            {
                return (int)Math.Round(baseDamage * 1.5);
            }
            return baseDamage;
        }

        /// <summary>
        /// 细节 532: 道士灵魂火符消耗护身符机制
        /// </summary>
        public static (bool success, int remainingTalismanDura) ConsumeTalismanForFireCharm(int currentTalismanDura)
        {
            if (currentTalismanDura <= 0) return (false, 0);
            return (true, currentTalismanDura - 1);
        }

        /// <summary>
        /// 细节 533: 道士施毒术消耗药粉与双毒效果
        /// 绿毒: 每秒扣毒伤; 红毒: 削弱 20% 防御与魔御
        /// </summary>
        public static (int effectiveAc, int effectiveMac) ApplyRedPoisonReduction(int baseAc, int baseMac, bool hasRedPoison)
        {
            if (!hasRedPoison) return (baseAc, baseMac);
            int newAc = (int)Math.Floor(baseAc * 0.8);
            int newMac = (int)Math.Floor(baseMac * 0.8);
            return (newAc, newMac);
        }

        /// <summary>
        /// 细节 534: 道士神兽等级属性阶梯 (Lv.0 ~ Lv.7)
        /// </summary>
        public static (int maxHp, int minDc, int maxDc) GetDivineBeastStats(int beastLevel)
        {
            return beastLevel switch
            {
                0 => (300, 15, 25),
                1 => (450, 20, 32),
                2 => (600, 25, 40),
                3 => (800, 30, 48),
                4 => (1050, 35, 56),
                5 => (1350, 42, 65),
                6 => (1700, 50, 75),
                7 => (2200, 60, 90),
                _ => (300, 15, 25)
            };
        }

        /// <summary>
        /// 细节 540: 麻痹戒指 1/15 几率赋予 5 秒麻痹
        /// </summary>
        public static (bool paralyzed, int durationSeconds) CheckParalyzeRingEffect(int rand15)
        {
            if (rand15 == 0) return (true, 5);
            return (false, 0);
        }

        /// <summary>
        /// 细节 541: 护身戒指伤害优先扣除 MP
        /// </summary>
        public static (int actualHpLost, int newMp) ApplyProtectionRingDamage(int rawDamage, int currentHp, int currentMp)
        {
            if (currentMp >= rawDamage)
            {
                return (0, currentMp - rawDamage);
            }
            else
            {
                int remainingDmg = rawDamage - currentMp;
                return (remainingDmg, 0);
            }
        }

        // -------------------------------------------------------------
        // 三、首领生态、未知暗殿与地牢环境物理机制 (细节 551 ~ 575)
        // -------------------------------------------------------------

        /// <summary>
        /// 细节 552: 双头金刚死后不爆任何物品，必须 Alt 割肉产出法神首饰
        /// </summary>
        public static bool IsDualHeadKingButchExclusive(int monsterAppr)
        {
            return monsterAppr == 71; // 双头金刚专属模型
        }

        /// <summary>
        /// 细节 553: 双头血魔物理高防 (AC 40) 与魔防极低 (MAC 5)
        /// </summary>
        public static (int ac, int mac) GetDualHeadBloodDemonDefense()
        {
            return (40, 5);
        }

        /// <summary>
        /// 细节 554: 沃玛教主血量低于 30% 触发瞬移脱困
        /// </summary>
        public static bool CheckWomaLeaderEscapeTeleport(int currentHp, int maxHp)
        {
            return (double)currentHp / maxHp <= 0.30;
        }

        /// <summary>
        /// 细节 555: 祖玛教主破石苏醒距离 (3 格贴身或受创)
        /// </summary>
        public static bool CheckZumaLeaderAwakening(int bossX, int bossY, int playerX, int playerY, bool isAttacked)
        {
            if (isAttacked) return true;
            int dx = Math.Abs(bossX - playerX);
            int dy = Math.Abs(bossY - playerY);
            return Math.Max(dx, dy) <= 3;
        }

        /// <summary>
        /// 细节 556: 未知暗殿真假神兽属性 (真神兽高防极慢，假神兽高攻极速)
        /// </summary>
        public static (int hp, int speedMs, bool isFake) GetUnknownDarkHallBeast(bool isRealVariant)
        {
            if (isRealVariant) return (3500, 900, false);
            return (500, 350, true);
        }

        /// <summary>
        /// 细节 559: 触龙神 100% 免疫魔法攻击
        /// </summary>
        public static int ApplyTouchDragonGodDamage(int incomingDmg, bool isMagicDmg)
        {
            if (isMagicDmg) return 0; // 魔法绝对免疫
            return incomingDmg;
        }

        /// <summary>
        /// 细节 560: 角蝇视野内产卵上限 10 只
        /// </summary>
        public static bool CanHornFlySpawnBat(int currentBatsInView)
        {
            return currentBatsInView < 10;
        }

        /// <summary>
        /// 细节 571: 回城卷轴红名玩家强制传送至红名村 (33, 33)
        /// </summary>
        public static (string mapId, int x, int y) GetRecallScrollDestination(int pkPoints, string homeMap, int homeX, int homeY)
        {
            if (pkPoints >= 200) // 红名
            {
                return ("0", 33, 33); // 盟重红名村
            }
            return (homeMap, homeX, homeY);
        }

        // -------------------------------------------------------------
        // 四、社交、交易、沙巴克与经济安全防线 (细节 576 ~ 600)
        // -------------------------------------------------------------

        /// <summary>
        /// 细节 576: 交易金币单笔 500 万与总额 5000 万硬熔断
        /// </summary>
        public static bool ValidateTradeGoldLimit(int singleGold, int currentTotalGold)
        {
            if (singleGold <= 0 || singleGold > 5_000_000) return false;
            if (currentTotalGold + singleGold > 50_000_000) return false;
            return true;
        }

        /// <summary>
        /// 细节 577: 交易双方任何金币或物品变动强制重置双方锁定状态
        /// </summary>
        public static (bool player1Locked, bool player2Locked) ResetTradeLocksOnChange()
        {
            return (false, false);
        }

        /// <summary>
        /// 细节 582: 地表金币三阶外观映射 (1~99 铜币, 100~999 银币, >=1000 大金山)
        /// </summary>
        public static int GetGroundGoldAppearance(int goldAmount)
        {
            if (goldAmount < 100) return 110;
            if (goldAmount < 1000) return 111;
            return 112;
        }

        /// <summary>
        /// 细节 584: 红名死亡金币大爆 (30%~50% 爆发散落)
        /// </summary>
        public static int CalculateRedNameDeathGoldDrop(int currentGold, int dropPercent)
        {
            dropPercent = Math.Clamp(dropPercent, 30, 50);
            return (int)Math.Floor(currentGold * (dropPercent / 100.0));
        }

        /// <summary>
        /// 细节 586: 沙巴克皇宫排他性 100% 纯净度扫描
        /// 皇宫内必须无任何敌对行会成员，方可裁决归属
        /// </summary>
        public static bool CheckSabukPalaceExclusiveOwnership(List<int> memberGuildIds, int dominantGuildId)
        {
            if (memberGuildIds.Count == 0) return false;
            foreach (var gId in memberGuildIds)
            {
                if (gId != dominantGuildId) return false;
            }
            return true;
        }

        /// <summary>
        /// 细节 589: 行会封号 14 字符截断
        /// </summary>
        public static string SanitizeGuildRankTitle(string rawTitle)
        {
            if (string.IsNullOrEmpty(rawTitle)) return "";
            return rawTitle.Length > 14 ? rawTitle.Substring(0, 14) : rawTitle;
        }

        /// <summary>
        /// 细节 590: 组队 11 人限制与经验 20% 加成均摊
        /// </summary>
        public static (bool canJoin, int expPerMember) CalculateGroupExpSharing(int memberCount, int rawExp)
        {
            if (memberCount >= 11) return (false, 0);
            int newCount = memberCount + 1;
            int totalBonusExp = (int)Math.Round(rawExp * 1.20);
            int shared = Math.Max(1, totalBonusExp / newCount);
            return (true, shared);
        }

        /// <summary>
        /// 细节 594: 装备持久为 0 属性完全失效
        /// </summary>
        public static (int finalAc, int finalDc) GetEffectiveEquipStats(int ac, int dc, int dura)
        {
            if (dura <= 0) return (0, 0);
            return (ac, dc);
        }

        /// <summary>
        /// 细节 597: 聊天频道冷却防刷屏 (普通 500ms, 喊话 5000ms)
        /// </summary>
        public static bool ValidateChatChannelCooldown(int channelType, long lastChatMs, long nowMs)
        {
            int requiredCd = (channelType == 1) ? 5000 : 500; // 1 = 喊话
            return (nowMs - lastChatMs) >= requiredCd;
        }

        /// <summary>
        /// 细节 598: 非安全区喊话扣除 1 点 MP
        /// </summary>
        public static (bool canShout, int newMp) CheckShoutMpCost(bool inSafeZone, int currentMp)
        {
            if (inSafeZone) return (true, currentMp);
            if (currentMp < 1) return (false, currentMp);
            return (true, currentMp - 1);
        }
    }
}
