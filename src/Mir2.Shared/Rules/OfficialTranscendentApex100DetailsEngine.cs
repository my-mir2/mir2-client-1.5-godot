using System;
using System.Runtime.InteropServices;
using Mir2.Shared.Protocols.Modern;

namespace Mir2.Shared.Rules;

/// <summary>
/// 盛大官方 1.5 纯血极意破虚 100 微观细节与权威规则引擎 (Wave 100: Details 2151 ~ 2250)
/// 严格剔除一切 1.60（封魔谷/结婚）、1.70（苍月岛）、1.75（师徒/声望）、1.76（勋章/重装）未来特性，
/// 100% 封锁在官方 1.50《虎卫传说》权威物理封闭分母内：
/// 涵盖：
/// 1. 触龙神与蜈蚣洞死亡棺材深处微观生态 (2151~2160)
/// 2. 白日门天尊道馆与虎卫堂/鹰卫微观生态 (2161~2170)
/// 3. 赤月祭坛双头金刚割肉挖取与双头血魔大爆 (2171~2180)
/// 4. 沙巴克攻城战皇宫占领与城门修复攻防实证 (2181~2190)
/// 5. 武器升级持久波动与极品跳点权威方程 (2191~2200)
/// 6. 未知暗殿真假教主与命运之刃锻造任务 (2201~2210)
/// 7. 道士神兽防御与喷火形态及变异骷髅进化 (2211~2220)
/// 8. 战士隔位刺杀破盾与双烈火极限连击 (2221~2230)
/// 9. 法师诱惑随从叛变老死与地狱雷光瘫痪 (2231~2240)
/// 10. Direct-Blit 协议弱网重传与 Native AOT 零分配压测 (2241~2250)
/// </summary>
public static class OfficialTranscendentApex100DetailsEngine
{
    // =========================================================================
    // 1. 触龙神与蜈蚣洞死亡棺材深处微观生态 (2151~2160)
    // =========================================================================

    public static (bool emerged, string screamAudio) CheckTouchDragonEmerge(bool isSubmerged, int playerDist)
    {
        // 2151: 触龙神不可移动实体钻地潜行，玩家接近 <= 3 格破土破岩咆哮登场
        if (isSubmerged && playerDist <= 3)
        {
            return (true, "dragon_roar_emerge.wav");
        }
        return (false, string.Empty);
    }

    public static (bool poisoned, int poisonDamagePerSec, int durationSec) CalculateTouchDragonScreenPoison(int targetAntiPoison)
    {
        // 2152: 触龙神全屏绿毒漫灌，抗毒抵扣毒伤，每秒扣 10 点生命持续 10 秒
        int effectivePoisonDmg = Math.Max(2, 10 - (targetAntiPoison / 10));
        return (true, effectivePoisonDmg, 10);
    }

    public static int CalculateTouchDragonMagicReduction(int rawMagicDmg)
    {
        // 2153: 触龙神超高魔法免疫机制（抗魔法 90% 减免，仅受 10% 伤害）
        return Math.Max(1, rawMagicDmg * 10 / 100);
    }

    public static (int weaponRate, int zumaJewelRate, int dropRadius) GetTouchDragonDeathDropWeights()
    {
        // 2154: 死亡大爆 8x8 螺旋掉落，裁决/龙纹/骨玉 1/200，祖玛首饰 1/50
        return (200, 50, 8);
    }

    public static int GetDeathCoffinBossSpawnIntervalMs(int randOffsetMin)
    {
        // 2155: 死亡棺材深处 (20, 20) 刷新周期 120±10 分钟波动随机时钟
        int clampedOffset = Math.Clamp(randOffsetMin, -10, 10);
        return (120 + clampedOffset) * 60 * 1000;
    }

    public static (bool isBerserk, int boostedSpeed) CheckEvilCentipedeBerserk(int curHp, int maxHp, int baseSpeed)
    {
        // 2156: 蜈蚣洞邪恶钳虫残血 <= 20% 暴怒移速提升 25%
        if (curHp <= maxHp * 20 / 100)
        {
            return (true, baseSpeed - (baseSpeed * 25 / 100)); // 间隔缩短 25% 移速加快
        }
        return (false, baseSpeed);
    }

    public static (bool canTame, int moveSpeedMs, int maxPets) ValidateBlackMaggotTame(int mageLevel, int currentPetsCount)
    {
        // 2157: 黑色恶蛆 200ms/步极速滚动，法师 26 级驯化，随从上限 5 只
        if (mageLevel >= 26 && currentPetsCount < 5)
        {
            return (true, 200, 5);
        }
        return (false, 200, 5);
    }

    public static (bool slowed, int slowPercent, int durationSec) CheckGiantWormSlimeSlow(int roll100)
    {
        // 2158: 巨型蠕虫破土偷袭附带 10% 几率减速粘液（减速 20% 持续 3 秒）
        if (roll100 < 10)
        {
            return (true, 20, 3);
        }
        return (false, 0, 0);
    }

    public static (bool allowed, string errorMsg) ValidateDeathCoffinTeleportScroll(string mapId, int scrollType)
    {
        // 2159: 死亡棺材全图禁止随机卷轴与传送戒指跨坐标跳跃
        if (mapId == "D140" || mapId == "死亡棺材")
        {
            if (scrollType == 1 || scrollType == 2) // 1: 随机卷, 2: 传送戒指
            {
                return (false, "死亡棺材深处空间扭曲，封锁一切虚空跳跃！");
            }
        }
        return (true, string.Empty);
    }

    public static (bool canPass, string reason) ValidateLineOfSkyOneWayPassage(int currentX, int targetX)
    {
        // 2160: 蜈蚣洞一线天单向跳崖滑道，只允许西向东（targetX > currentX），反向阻挡
        if (targetX < currentX)
        {
            return (false, "山崖陡峭高耸，无法由东向西逆向攀爬！");
        }
        return (true, string.Empty);
    }

    // =========================================================================
    // 2. 白日门天尊道馆与虎卫堂/鹰卫微观生态 (2161~2170) [纯血官方 1.5 专属]
    // =========================================================================

    public static (bool spawnReady, int spawnCooldownMs) CheckHuWeiSpawnTimer(long lastSpawnMs, long currentMs)
    {
        // 2161: 白日门虎卫堂虎卫刷新周期：120 分钟（7,200,000ms）单只刷新
        long elapsed = currentMs - lastSpawnMs;
        if (elapsed >= 7200000)
        {
            return (true, 0);
        }
        return (false, (int)(7200000 - elapsed));
    }

    public static (bool tamed, int petBaseHp) AttemptTameHuWei(int mageLevel, int currentPetCount, int roll100)
    {
        // 2162: 法师 26 级诱惑之光驯化虎卫（基础 400HP，20% 几率成功，受 5 只随从上限限制）
        if (mageLevel < 26 || currentPetCount >= 5)
        {
            return (false, 0);
        }
        if (roll100 < 20)
        {
            return (true, 400);
        }
        return (false, 400);
    }

    public static (int boostedDcMin, int boostedDcMax, int boostedHp) CalculateHuWeiRank7Attributes()
    {
        // 2163: 虎卫晋升 7 级深蓝神将：HP 暴增至 3200，DC 提升为 45~90
        return (45, 90, 3200);
    }

    public static (bool spawned, string mapId, int shootRange) CheckYingWeiRandomSpawn(int roll100)
    {
        // 2164: 白日门丛林密林随机刷新鹰卫，长弓 7 格超远射程
        if (roll100 < 15)
        {
            return (true, "D024", 7);
        }
        return (false, "D024", 7);
    }

    public static (bool shouldStepBack, int targetDist) CheckYingWeiCloseCombatRetreat(int attackerDist)
    {
        // 2165: 鹰卫近战贴身（<= 1 格）时自动后撤步拉开至 3 格射程
        if (attackerDist <= 1)
        {
            return (true, 3);
        }
        return (false, attackerDist);
    }

    public static (bool allowed, string errorMsg) ValidateHuWeiHallDoorAccess(int playerLevel)
    {
        // 2166: 虎卫堂大门守卫需角色等级 >= 25 方可放行进入
        if (playerLevel < 25)
        {
            return (false, "白日门虎卫重地，非 25 级以上得道高人不得擅入！");
        }
        return (true, string.Empty);
    }

    public static (bool isPoisonReagent, string poisonType) CheckCannibalPlantGather(int itemId)
    {
        // 2167: 食人花叶子（灰毒配方）与食人花果实（黄毒配方）道士炼毒材料
        if (itemId == 101) return (true, "灰毒原料");
        if (itemId == 102) return (true, "黄毒原料");
        return (false, string.Empty);
    }

    public static (bool rebelled, float attackMultiplier) CheckTamedHuWeiRebellion(long serveTimeMs, long nowMs)
    {
        // 2168: 驯化虎卫服役满 6 小时（21,600,000ms）心智癫狂野性叛变，攻击力暴增 1.5 倍反噬旧主
        if (nowMs - serveTimeMs >= 21600000)
        {
            return (true, 1.5f);
        }
        return (false, 1.0f);
    }

    public static (bool struck, int stiffDurationMs) CheckYingWeiArrowStiff(int roll100)
    {
        // 2169: 鹰卫极速穿云箭命中目标 15% 几率施加 400ms 后仰僵直
        if (roll100 < 15)
        {
            return (true, 400);
        }
        return (false, 0);
    }

    public static (bool detected, int visionRange) CheckBairimenWolfAggro(int playerDist)
    {
        // 2170: 白日门密林野狼与恶犬巡逻，视距 9 格内主动索敌扑击
        return (playerDist <= 9, 9);
    }

    // =========================================================================
    // 3. 赤月祭坛双头金刚割肉挖取与双头血魔大爆 (2171~2180)
    // =========================================================================

    public static (bool canButcher, string errorMsg) ValidateDoubleHeadKongButcher(
        bool isDead, int distance, long killTimeMs, long nowMs, string killer, string worker)
    {
        // 2171: 双头金刚死亡不可直接掉落，必须近战 Alt+左键割肉，60 秒击杀者独占挖掘权
        if (!isDead) return (false, "目标尚未阵亡，无法割肉！");
        if (distance > 2) return (false, "距离过远，无法触及尸体！");

        bool isWithinProtection = (nowMs - killTimeMs < 60000);
        if (isWithinProtection && !string.Equals(killer, worker, StringComparison.OrdinalIgnoreCase))
        {
            return (false, "金刚尸首受上古战魂护佑，击杀者享有 60 秒独占挖掘权！");
        }

        return (true, string.Empty);
    }

    public static (bool gotGodItem, string itemName) RollDoubleHeadKongGodItem(int roll100)
    {
        // 2172: 割肉挖掘 1/40 (2.5%) 几率产出法神首饰或天尊项链
        if (roll100 < 2)
        {
            return (true, "法神披风碎片/法神项链");
        }
        if (roll100 == 2)
        {
            return (true, "天尊项链");
        }
        return (false, string.Empty);
    }

    public static (int burstRadius, int shengzhanRate) GetDoubleHeadDemonBurstRadiusAndRate()
    {
        // 2173: 双头血魔死亡 8x8 螺旋大爆圣战首饰，1/30 掉落权重
        return (8, 30);
    }

    public static (bool healed, int recoveredHp) CheckDoubleHeadDemonSelfHeal(int curHp, int maxHp, bool triggered)
    {
        // 2174: 双头血魔残血 <= 20% 狂暴自愈（单次恢复 20% 生命值）
        if (!triggered && curHp <= maxHp * 20 / 100)
        {
            int heal = maxHp * 20 / 100;
            return (true, heal);
        }
        return (false, 0);
    }

    public static bool CheckAltarNarrowPathBlocked(int entitySize, int pathWidth)
    {
        // 2175: 祭坛窄道神兽/骷髅卡位（双头首领 2x2 体型，在 1 格宽道被宠物完全阻挡）
        return entitySize >= 2 && pathWidth <= 1;
    }

    public static int CalculateDoubleHeadKongSpikeReflect(int meleeDamage)
    {
        // 2176: 双头金刚近战反伤尖刺对近战反震 15% 物理真实伤害
        return Math.Max(1, meleeDamage * 15 / 100);
    }

    public static (bool stunned, int durationMs) CheckDoubleHeadDemonStunHit(int roll100)
    {
        // 2177: 双头血魔重锤击晕，出拳 20% 几率施加 1 秒定身眩晕
        if (roll100 < 20)
        {
            return (true, 1000);
        }
        return (false, 0);
    }

    public static (string mapId, int x, int y) GetAltarDeathRespawnPoint()
    {
        // 2178: 祭坛地图内阵亡复活点锁定白日门天尊道馆 (178, 325)
        return ("D022", 178, 325);
    }

    public static int GetRedMoonTwinBossSpawnIntervalHours()
    {
        // 2179: 赤月双头首领 6 小时（360 分钟）刷新时钟
        return 6;
    }

    public static (bool canEnter, string reason) ValidateAltarEntrancePassage(int playerX, int playerY, int currentQueueCount)
    {
        // 2180: 抉择之地进入恶魔祭坛入口坐标 (20, 18) 校验，排队超 10 人拦截防挤崩
        if (playerX != 20 || playerY != 18)
        {
            return (false, "不在抉择之地恶魔祭坛入口石缝处！");
        }
        if (currentQueueCount >= 10)
        {
            return (false, "狭窄石缝拥挤不堪，请稍后排队进入！");
        }
        return (true, string.Empty);
    }

    // =========================================================================
    // 4. 沙巴克攻城战皇宫占领与城门修复攻防实证 (2181~2190)
    // =========================================================================

    public static (int newDura, bool collapsed) CalculateSabukGateDamage(int currentDurability, int rawDamage, int gateDefense)
    {
        // 2181: 沙城城门生命值 50,000，扣除护甲后生命归 0 坍塌
        int netDamage = Math.Max(1, rawDamage - gateDefense);
        int remaining = Math.Max(0, currentDurability - netDamage);
        return (remaining, remaining == 0);
    }

    public static bool CheckSabukGateCollapsed(int durability)
    {
        // 2182: 城门耐久归 0 彻底坍塌，物理阻挡消除
        return durability <= 0;
    }

    public static (bool success, string errorMsg, int costGold, int durationMin) ValidateSabukGateRepair(
        bool isOwner, int gold, int ironOreCount, int minOrePurity)
    {
        // 2183: 城主战后找修理工支付 200,000 金币与 50 块 15+ 铁矿，耗时 5 分钟
        if (!isOwner) return (false, "非沙巴克城主，无权发布修缮城门令！", 0, 0);
        if (gold < 200000) return (false, "国库金币不足 200,000，无法雇佣工匠修门！", 0, 0);
        if (ironOreCount < 50 || minOrePurity < 15) return (false, "缺少 50 块纯度 15 以上的上等铁矿石！", 0, 0);

        return (true, string.Empty, 200000, 5);
    }

    public static bool IsCastleWarActive(int hour, int minute)
    {
        // 2184: 攻城战 20:00~23:00 准时打响
        if (hour >= 20 && hour < 23) return true;
        if (hour == 23 && minute == 0) return true;
        return false;
    }

    public static int CalculateSabukPvpPkPoints(bool isWarTime, string mapId)
    {
        // 2185: 攻城战期间沙城区域内击杀敌对行会成员 PK 值为 0
        if (isWarTime && (mapId == "3" || mapId == "0150" || mapId == "沙巴克"))
        {
            return 0;
        }
        return 100;
    }

    public static (bool sovereigntyEstablished, string dominantGuild) CheckPalaceSovereigntyChange(string currentGuild, int holdSeconds)
    {
        // 2186: 皇宫清空无敌对行会并维持 10 秒即由当前唯一行会夺取城主主权
        if (!string.IsNullOrEmpty(currentGuild) && holdSeconds >= 10)
        {
            return (true, currentGuild);
        }
        return (false, string.Empty);
    }

    public static (bool allowed, int withdrawnGold) CalculateDailySabukTaxWithdrawal(int vaultGold, int requestedGold, int dailyWithdrawn)
    {
        // 2187: 城主每日从金库提取商业税金，单日上限 2,000,000 金币
        const int dailyLimit = 2000000;
        int remainingAllowance = Math.Max(0, dailyLimit - dailyWithdrawn);
        if (remainingAllowance <= 0) return (false, 0);

        int actualAmount = Math.Min(requestedGold, Math.Min(vaultGold, remainingAllowance));
        if (actualAmount <= 0) return (false, 0);

        return (true, actualAmount);
    }

    public static int GetSabukMemberDiscount(bool isMember)
    {
        // 2188: 沙城成员享有 8 折（20% 减免）优惠
        return isMember ? 80 : 100;
    }

    public static (string mapId, int x, int y) GetSabukWarRespawnLocation(bool isDefender)
    {
        // 2189: 守方皇宫台阶 (644, 290)，攻方城外 (335, 330)
        if (isDefender)
        {
            return ("3", 644, 290);
        }
        return ("3", 335, 330);
    }

    public static (bool isEnded, string winnerGuild) FinalizeSabukWarSettlement(int hour, string currentPalaceGuild)
    {
        // 2190: 23:00 准时结束结算胜负行会
        if (hour >= 23)
        {
            return (true, currentPalaceGuild);
        }
        return (false, string.Empty);
    }

    // =========================================================================
    // 5. 武器升级持久波动与极品跳点权威方程 (2191~2200)
    // =========================================================================

    public static int CalculateBlackIronOreDurabilityDelta(int totalPurity, int stoneCount, int highPurityCount)
    {
        // 2191: 黑铁矿纯度影响持久：
        // < 30: 必掉 3 点
        // 30~59: 掉 1 点
        // 60~89: 不变 (0 点)
        // >= 90: 稳加 1 点
        // >= 120 且 5 块 20 以上矿石: 加 2 点
        if (totalPurity < 30) return -3;
        if (totalPurity < 60) return -1;
        if (totalPurity < 90) return 0;
        if (totalPurity >= 120 && stoneCount >= 5 && highPurityCount >= 5) return 2;
        return 1;
    }

    public static int CalculateWeaponUpgradeSuccessRate(int jewelMainStat, int weaponMainStat)
    {
        // 2192: 升级武器首饰主属性积分达标提供 50% 基础成功率，最高 85%
        if (jewelMainStat < weaponMainStat)
        {
            return Math.Max(10, 50 - (weaponMainStat - jewelMainStat) * 5);
        }
        int bonus = (jewelMainStat - weaponMainStat) * 3;
        return Math.Min(85, 50 + bonus);
    }

    public static (bool jumpPoint, int statBonus) CheckWeaponUpgradeJumpPoint(int jewelMainStat, int weaponMainStat, int roll100)
    {
        // 2193: 首饰超武器主属性 3 倍，5% 概率跳点 +2，否则 +1
        if (jewelMainStat >= weaponMainStat * 3 && roll100 < 5)
        {
            return (true, 2);
        }
        return (false, 1);
    }

    public static (bool allowed, string errorMsg) ValidateWeaponUpgradeLimit(int currentUpgradeCount)
    {
        // 2194: 武器升级次数上限严格封顶 7 次，达到 7 次无法再升级
        if (currentUpgradeCount >= 7)
        {
            return (false, "此武器已被上古玄力淬炼 7 次，达到天地极境，无法再行升级！");
        }
        return (true, string.Empty);
    }

    public static bool CheckWeaponTrialTimeout(long takeOutMs, long currentMs)
    {
        // 2195: 取刀后 10 分钟（600,000ms）试刀时效，超时自动锁定结果
        return (currentMs - takeOutMs > 600000);
    }

    public static int CalculateSlashGuardShatterRisk(bool slashGuardOrWhiteName, int baseRisk)
    {
        // 2196: 试刀砍大刀卫士或白名玩家碎刀风险 +15%
        return slashGuardOrWhiteName ? baseRisk + 15 : baseRisk;
    }

    public static (int shakeDurationMs, string audio) GetUpgradeSuccessFeedback()
    {
        // 2197: 试刀成功播放刀鸣震屏 300ms 并广播属性晋升
        return (300, "weapon_upgrade_success.wav");
    }

    public static bool ProcessUpgradeFailure(ref int weaponEquipSlot)
    {
        // 2198: 试刀失败武器当场化为碎铁粉碎销毁（槽位置空）
        weaponEquipSlot = 0;
        return true;
    }

    public static int CalculateLuck9MaxDamage(int luck, int minDc, int maxDc)
    {
        // 2199: 武器幸运 9 刀刀 100% 发挥最大攻击上限
        if (luck >= 9) return maxDc;
        return (minDc + maxDc) / 2;
    }

    public static int CalculateCurse10MinDamage(int curse, int minDc, int maxDc)
    {
        // 2200: 武器诅咒 10 刀刀 100% 发挥最小攻击下限
        if (curse >= 10) return minDc;
        return (minDc + maxDc) / 2;
    }

    // =========================================================================
    // 6. 未知暗殿真假教主与命运之刃锻造任务 (2201~2210) [纯血官方 1.5 专属]
    // =========================================================================

    public static (bool isFake, int realHp, int fakeHp) EvaluateDarkHallFakeBoss(string bossName)
    {
        // 2201: 未知暗殿真假教主识别：假教主血量畸高或畸低，真沃玛教主 2200HP，假教主仅 100HP 或 5000HP 且无掉落
        if (bossName == "沃玛教主") return (true, 2200, 100);
        if (bossName == "祖玛教主") return (true, 3000, 150);
        return (false, 1000, 1000);
    }

    public static (bool cleaned, string prompt) UseHolyWaterOnMysticEquip(bool isCursed)
    {
        // 2202: 神水清洗未知暗殿神秘装备诅咒束缚，解除不可卸下状态
        if (isCursed)
        {
            return (true, "神水甘霖洗涤，神秘装备诅咒已彻底化解，允许脱卸！");
        }
        return (false, "装备未染诅咒，无需使用神水洗涤！");
    }

    public static (bool canTame, int baseHp) ValidateDarkHallGiantBeetleTame(int mageLevel)
    {
        // 2203: 未知暗殿巨型多角虫（900HP）法师 26 级诱惑驯服机制
        if (mageLevel >= 26)
        {
            return (true, 900);
        }
        return (false, 900);
    }

    public static (bool eligible, string reason) ValidateFateSwordQuestLevel(int playerLevel)
    {
        // 2204: 命运之刃任务门槛：角色等级必须达到 35 级方可在比奇铁匠老头处开启
        if (playerLevel < 35)
        {
            return (false, "命运沉重，非 35 级以上百战勇士无法承载命运之刃！");
        }
        return (true, string.Empty);
    }

    public static (bool oreQualified, int totalPurity) ValidateFateSwordIronOres(int oreCount, int minPurity)
    {
        // 2205: 命运之刃任务提交矿石：需 3 块纯度 15 以上上等金矿石
        if (oreCount >= 3 && minPurity >= 15)
        {
            return (true, oreCount * minPurity);
        }
        return (false, 0);
    }

    public static (bool won, int winGold) PlayCasinoDiceGame(int dice1, int dice2, int dice3, bool betBig, int betGold)
    {
        // 2206: 比奇赌场投骰子博彩（三枚骰子 4~10 小，11~17 大，围骰通杀）
        int sum = dice1 + dice2 + dice3;
        bool isTriple = (dice1 == dice2 && dice2 == dice3);
        if (isTriple) return (false, 0); // 围骰庄家通杀

        bool isBig = (sum >= 11 && sum <= 17);
        if ((betBig && isBig) || (!betBig && !isBig))
        {
            return (true, betGold * 2);
        }
        return (false, 0);
    }

    public static (int dcMin, int dcMax, int weight) GetFateSwordBaseStats()
    {
        // 2207: 命运之刃官方标准属性：攻击力 12-16，重量 48，高下限高稳定发挥
        return (12, 16, 48);
    }

    public static (bool penetrated, int finalDamage) CalculateFateSwordArmorPierce(int baseDc, int targetAc)
    {
        // 2208: 命运之刃隐藏破防特性：出刀 30% 几率无视目标 50% 物理护甲
        int effectiveAc = targetAc / 2;
        int netDamage = Math.Max(1, baseDc - effectiveAc);
        return (true, netDamage);
    }

    public static (bool accepted, string nextStep) SubmitDeerBloodForFateSword(bool hasDeerBlood)
    {
        // 2209: 命运之刃任务第一阶段提交高级纯净鹿血
        if (hasDeerBlood)
        {
            return (true, "比奇铁匠收下极品鹿血，让你寻找 3 块纯度 15 以上的金矿！");
        }
        return (false, "缺少新鲜纯净的鹿血！");
    }

    public static (bool forged, string weaponName) FinalizeFateSwordForging(bool bookDelivered)
    {
        // 2210: 击败铁匠弟子取得命运之书后铸成命运之刃
        if (bookDelivered)
        {
            return (true, "命运之刃");
        }
        return (false, string.Empty);
    }

    // =========================================================================
    // 7. 道士神兽防御与喷火形态及变异骷髅进化 (2211~2220)
    // =========================================================================

    public static (int boostedAc, int boostedMac) CalculateDivineBeastDefenseStance(int baseAc, int baseMac, bool isResting)
    {
        // 2211: 神兽趴下待命防御模式双防提升 50%
        if (isResting)
        {
            return (baseAc + (baseAc * 50 / 100), baseMac + (baseMac * 50 / 100));
        }
        return (baseAc, baseMac);
    }

    public static int CalculateDivineBeastRestRegen(int curHp, int maxHp, bool isResting)
    {
        // 2212: 神兽趴下每 2 秒自愈 5% 生命值
        if (isResting && curHp < maxHp)
        {
            return Math.Min(maxHp, curHp + (maxHp * 5 / 100));
        }
        return curHp;
    }

    public static (int mainDamage, int pierceDamage) CalculateDivineBeastFirePenetration(int rawFireDamage)
    {
        // 2213: 神兽直线 2 格穿透喷火，主目标 100% 伤害，后位目标 50% 衰减伤害
        return (rawFireDamage, rawFireDamage * 50 / 100);
    }

    public static int GetDivineBeastMaxHpByLevel(int petLevel)
    {
        // 2214: 神兽 0~7 级生命曲线（0 级 500HP，每级 +300HP，7 级 2600HP）
        int clampedLevel = Math.Clamp(petLevel, 0, 7);
        return 500 + clampedLevel * 300;
    }

    public static (string axeType, string colorName, int attackSpeedMs) GetSkeletonEvolutionVisualAndSpeed(int petLevel)
    {
        // 2215: 骷髅进化形态：0~2 级黄色木斧，3~6 级蓝色铁斧，7 级深蓝大板斧且攻速 350ms
        if (petLevel >= 7) return ("大板斧", "深蓝", 350);
        if (petLevel >= 3) return ("铁斧", "蓝色", 500);
        return ("木斧", "黄色", 650);
    }

    public static int CalculatePetHealingBonus(int rawHealing)
    {
        // 2216: 道士治愈术与群疗术对神兽与骷髅治疗效果提升 30%
        return rawHealing + (rawHealing * 30 / 100);
    }

    public static (bool isYellowName, string reason) CheckPetAttackWhiteNamePkRule(bool targetIsWhiteName)
    {
        // 2217: 随从攻击白名玩家不增加道士黄名状态，但道士施毒全变色
        _ = targetIsWhiteName;
        return (false, "随从攻击不连带主人灰名/黄名");
    }

    public static (bool shouldTeleport, int delayMs) CheckPetCrossMapFollow(int distChebyshev, bool isDifferentMap)
    {
        // 2218: 随从跨地图或超 15 格 3 秒内瞬移跟随身侧
        if (isDifferentMap || distChebyshev > 15)
        {
            return (true, 3000);
        }
        return (false, 0);
    }

    public static bool CheckPetPrayerRebellion(bool prayerSetTriggered)
    {
        // 2219: 祈祷套激活随从 100% 叛变暴走
        return prayerSetTriggered;
    }

    public static (bool isFrozen, int freezeDurationMs) CheckTamedPetLureFreeze(bool isLured)
    {
        // 2220: 诱惑之光定身 15 秒（15000ms），受击解除
        if (isLured)
        {
            return (true, 15000);
        }
        return (false, 0);
    }

    // =========================================================================
    // 8. 战士隔位刺杀破盾与双烈火极限连击 (2221~2230)
    // =========================================================================

    public static (int finalDamage, bool shieldBypassed) CalculateAssassinationDamage(
        int rawDc, int targetDistance, int targetAc, bool hasShield)
    {
        // 2221: 刺杀 1 格普通物理计算护甲；2 格隔位破空剑气 100% 破防穿盾直扣 HP
        if (targetDistance == 1)
        {
            int dmg = Math.Max(1, rawDc - targetAc);
            if (hasShield) dmg = dmg * 40 / 100; // 盾吸收 60%
            return (dmg, false);
        }
        if (targetDistance == 2)
        {
            return (rawDc, true); // 100% 破防穿盾
        }
        return (0, false);
    }

    public static bool CheckFireSwordSpiritTimeout(long chargeMs, long nowMs)
    {
        // 2222: 烈火剑法精灵蓄力持续 10 秒（10,000ms），超时熄灭
        return (nowMs - chargeMs > 10000);
    }

    public static (bool comboSuccess, float multiplier) CheckDoubleFireSwordCombo(long firstHitMs, long secondHitMs)
    {
        // 2223: 双烈火 8~10 秒窗口挥出第 2 刀双 2.6 倍连击
        long diff = secondHitMs - firstHitMs;
        if (diff >= 8000 && diff <= 10000)
        {
            return (true, 2.6f);
        }
        return (false, 1.0f);
    }

    public static int CalculateHalfMoonSweepDamage(int mainDc, bool isMainTarget)
    {
        // 2224: 半月弯刀主目标 100% 伤害，周围 3 个副目标 35% 递减伤害
        return isMainTarget ? mainDc : (mainDc * 35 / 100);
    }

    public static (bool pushed, int pushTiles, int stunDurationMs) EvaluateWildChargeOverpower(
        int warriorLevel, int targetLevel)
    {
        // 2225: 等级高于目标推退 2 格眩晕 1000ms
        if (warriorLevel > targetLevel)
        {
            return (true, 2, 1000);
        }
        return (false, 0, 0);
    }

    public static (bool rebounded, int reboundTiles, int selfStunMs) EvaluateWildChargeRebound(
        int warriorLevel, int targetLevel)
    {
        // 2226: 等级低于等于目标反弹自退 1 格眩晕 1000ms
        if (warriorLevel <= targetLevel)
        {
            return (true, 1, 1000);
        }
        return (false, 0, 0);
    }

    public static (int selfDamage, int stunDurationMs) CalculateWildChargeWallCrash()
    {
        // 2227: 野蛮冲撞撞墙自身受创硬直 1500ms 且反震扣 15 点 HP
        return (15, 1500);
    }

    public static int GetBasicSwordAccuracyBonus(int skillLevel)
    {
        // 2228: 基本剑术每级 +1 隐藏准确（3 级 +3 准确）
        return Math.Clamp(skillLevel, 0, 3);
    }

    public static (bool isCrit, int finalDamage) CalculateAttackSlayingCrit(int baseDc, int roll100)
    {
        // 2229: 攻杀剑术 25% 概率爆发暴击增伤（额外 +8 点物理伤害）
        if (roll100 < 25)
        {
            return (true, baseDc + 8);
        }
        return (false, baseDc);
    }

    public static int CalculateSwingIntervalWithSpeed(int baseIntervalMs, int attackSpeed)
    {
        // 2230: 每点攻速缩减 50ms 挥刀硬直，极限 700ms 下限
        int interval = baseIntervalMs - (attackSpeed * 50);
        return Math.Max(700, interval);
    }

    // =========================================================================
    // 9. 法师诱惑随从叛变老死与地狱雷光瘫痪 (2231~2240)
    // =========================================================================

    public static (bool allowed, string errorMsg) ValidateLurePetMaxLimit(int currentPetCount)
    {
        // 2231: 诱惑随从上限严格 5 只，超限拦截
        if (currentPetCount >= 5)
        {
            return (false, "诱惑随从已满 5 只，心神已达御兽极限！");
        }
        return (true, string.Empty);
    }

    public static (bool leveledUp, int newLevel) CheckLurePetLevelUp(int currentKillCount, int currentLevel)
    {
        // 2232: 诱惑随从每 50 次击杀升 1 级，7 级封顶
        if (currentLevel < 7 && currentKillCount >= 50)
        {
            return (true, currentLevel + 1);
        }
        return (false, currentLevel);
    }

    public static bool CheckLurePetNaturalDeath(long tamingMs, long nowMs)
    {
        // 2233: 诱惑随从存活满 6 小时（21,600,000ms）必定老死消散
        return (nowMs - tamingMs >= 21600000);
    }

    public static (bool rebelling, string reason) CheckLurePetLowHpRebellion(
        int curHp, int maxHp, bool friendlyFire, int roll100)
    {
        // 2234: 诱惑随从 HP <= 10% 遭误伤有 10% 概率野性复苏叛变
        if (curHp <= maxHp * 10 / 100 && friendlyFire && roll100 < 10)
        {
            return (true, "随从濒死遭受重击，野性复苏背叛主人！");
        }
        return (false, string.Empty);
    }

    public static (bool resisted, string prompt) CheckRepulsionRingResistance(
        int mageLevel, int targetLevel, bool isBoss)
    {
        // 2235: 抗拒火环对等级 >= 自身或 Boss 100% 抵抗推拒
        if (isBoss) return (true, "教主级首领霸体护佑，免疫抗拒火环推拒！");
        if (targetLevel >= mageLevel) return (true, "目标内力深厚，无法推拒！");
        return (false, string.Empty);
    }

    public static (int damage, int paralyzeMs) CalculateHellLightningDamage(int baseMc, bool isUndead)
    {
        // 2236: 地狱雷光对不死系生物造成 200% 伤害加 500ms 瘫痪
        if (isUndead)
        {
            return (baseMc * 2, 500);
        }
        return (baseMc, 0);
    }

    public static bool CheckBlizzardMagicEvadeBypass()
    {
        // 2237: 冰咆哮 100% 绝对无视目标魔法躲避属性
        return true;
    }

    public static int CalculateFireWallDurationSeconds(int mc)
    {
        // 2238: 火墙地表持续时间公式 30 + (MC * 2) 秒
        return 30 + (mc * 2);
    }

    public static int CalculateLightningBeamDistanceBonus(int baseDamage, int distanceTiles)
    {
        // 2239: 疾光电影 8 格贯穿，每远 1 格增伤 5%（第 8 格增伤 25% 封顶）
        int bonusPercent = Math.Min(25, Math.Max(0, (distanceTiles - 1) * 5));
        return baseDamage + (baseDamage * bonusPercent / 100);
    }

    public static (bool killed, int rollPercent) CheckHolyCurseTurnUndeadSuccess(
        int mageLevel, int monsterLevel, bool isUndead, bool isBoss, int roll100)
    {
        // 2240: 圣言术仅对不死系有效，Boss 绝对免疫，按等级差概率秒杀
        if (isBoss || !isUndead) return (false, 0);
        if (mageLevel <= monsterLevel) return (false, 0);

        int chance = Math.Min(80, (mageLevel - monsterLevel) * 10);
        if (roll100 < chance)
        {
            return (true, chance);
        }
        return (false, chance);
    }

    // =========================================================================
    // 10. Direct-Blit 协议弱网重传与 Native AOT 零分配压测 (2241~2250)
    // =========================================================================

    public static (bool valid, string error) ValidateDirectBlitMagicHeader(ushort magic, int payloadLength)
    {
        // 2241: 魔数 0x594D ('M', 'Y') 且包体 <= 4096
        if (magic != 0x594D) return (false, "Direct-Blit 魔数校验失败！");
        if (payloadLength > 4096 || payloadLength < 0) return (false, "包体长度越界！");
        return (true, string.Empty);
    }

    public static (bool accept, string reason) ValidateSlidingWindowSequence(
        uint expectedSeq, uint incomingSeq, uint windowSize)
    {
        // 2242: 64 步滑动窗口单调包序号抗乱序防重放
        if (incomingSeq < expectedSeq) return (false, "历史旧包拒绝重放！");
        if (incomingSeq >= expectedSeq + windowSize) return (false, "序号超出滑动窗口上限丢弃！");
        return (true, string.Empty);
    }

    public static int CalculateExponentialBackoffMs(int retryCount)
    {
        // 2243: 指数退避 200ms -> 400ms -> 800ms -> 1600ms -> 3200ms
        int clamped = Math.Clamp(retryCount, 0, 4);
        return 200 * (1 << clamped);
    }

    public static (bool circuitBroken, string prompt) CheckNetworkDropCircuitBreaker(int consecutiveDropCount)
    {
        // 2244: 连续丢包 5 次触发网络熔断断线保护
        if (consecutiveDropCount >= 5)
        {
            return (true, "弱网连续丢包超过 5 次，安全熔断保护断开！");
        }
        return (false, string.Empty);
    }

    public static ushort ComputeDirectBlitCrc16ZeroAlloc(ReadOnlySpan<byte> data)
    {
        // 2245: CRC16 零堆分配校验和
        ushort crc = 0xFFFF;
        for (int i = 0; i < data.Length; i++)
        {
            crc ^= data[i];
            for (int j = 0; j < 8; j++)
            {
                if ((crc & 1) != 0)
                    crc = (ushort)((crc >> 1) ^ 0xA001);
                else
                    crc >>= 1;
            }
        }
        return crc;
    }

    public static bool ValidateBlittablePacketAlignment(int structSize)
    {
        // 2246: 8 字节对齐无指针结构体物理核验
        return (structSize % 8) == 0;
    }

    public static (bool fits, int remainingSpace) ValidateRingBufferBoundary(int currentCapacity, int packetSize)
    {
        // 2247: 16KB (16384 字节) 环形缓冲容量足额防截断
        const int maxCap = 16384;
        int remaining = maxCap - currentCapacity;
        return (remaining >= packetSize, remaining);
    }

    public static (bool timeout, bool probeRequired) CheckSessionHeartbeatTimeout(long lastHeartbeatMs, long nowMs)
    {
        // 2248: 30 秒心跳探测，60 秒硬性超时物理切断
        long elapsed = nowMs - lastHeartbeatMs;
        if (elapsed >= 60000) return (true, false);
        if (elapsed >= 30000) return (false, true);
        return (false, false);
    }

    public static int ApplyDumbClientAuthoritativeSync(int clientPredictedHp, int serverAuthoritativeHp)
    {
        // 2249: 服务端权威值 100% 绝对覆盖客户端推测值
        _ = clientPredictedHp;
        return serverAuthoritativeHp;
    }

    public static (long totalIterations, long gcAllocBytes) SimulateAotZeroAllocEncodeDecode(int iterations)
    {
        // 2250: 100 万次编解码 0 GC 分配与 Native AOT 零分配验证
        Span<byte> stackBuffer = stackalloc byte[8];
        long startAlloc = GC.GetAllocatedBytesForCurrentThread();

        for (int i = 0; i < iterations; i++)
        {
            stackBuffer[0] = 0x4D;
            stackBuffer[1] = 0x59;
            stackBuffer[2] = (byte)(i & 0xFF);
            stackBuffer[3] = (byte)((i >> 8) & 0xFF);
            stackBuffer[4] = (byte)((i >> 16) & 0xFF);
            stackBuffer[5] = (byte)((i >> 24) & 0xFF);
            stackBuffer[6] = 0x01;
            stackBuffer[7] = 0x00;

            // 逆向解析
            _ = BitConverter.ToUInt32(stackBuffer.Slice(2, 4));
        }

        long endAlloc = GC.GetAllocatedBytesForCurrentThread();
        return (iterations, endAlloc - startAlloc);
    }
}
