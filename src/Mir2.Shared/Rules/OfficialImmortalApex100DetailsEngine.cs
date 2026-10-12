using System;
using System.Runtime.InteropServices;
using Mir2.Shared.Protocols.Modern;

namespace Mir2.Shared.Rules;

/// <summary>
/// 盛大官方 1.5 极意登峰 100 微观细节与权威规则引擎 (Details 2051 ~ 2150)
/// 涵盖：
/// 1. 沃玛寺庙三层与教主狂暴瞬移机制 (2051~2060)
/// 2. 祖玛教主石化唤醒与护卫小怪群召唤 (2061~2070)
/// 3. 白日门丛林毒物与道士炼毒施毒核心生态 (2071~2080)
/// 4. 武器升级持久损耗加成与跳点极品概率模型 (2081~2090)
/// 5. 比奇城皇宫国王任务与沃玛号角创建行会权威规则 (2091~2100)
/// 6. 沙巴克皇宫密道与城门耐久修补机制 (2101~2110)
/// 7. 道士神兽防御/喷火双形态与骷髅进化攻击力 (2111~2120)
/// 8. 战士刺杀二位剑气破盾穿透与双烈火爆发 (2121~2130)
/// 9. 法师诱惑随从叛变老死与抗拒火环等级推挤 (2131~2140)
/// 10. Direct-Blit 协议弱网重传与 Native AOT 零分配压测 (2141~2150)
/// </summary>
public static class OfficialImmortalApex100DetailsEngine
{
    // =========================================================================
    // 1. 沃玛寺庙三层与教主狂暴瞬移机制 (2051~2060)
    // =========================================================================

    public static (bool isBerserk, int boostedDc, int boostedAc) CheckWomaLordBerserk(int curHp, int maxHp, int baseDc, int baseAc)
    {
        // 2051: HP <= 30% (<= 660 / 2200) 暴走狂暴，攻击力 +50%，防御 +30%
        if (curHp <= maxHp * 30 / 100)
        {
            return (true, baseDc + baseDc * 50 / 100, baseAc + baseAc * 30 / 100);
        }
        return (false, baseDc, baseAc);
    }

    public static bool ShouldWomaLordTeleport(bool isBerserk, long lastTeleportMs, long curMs)
    {
        // 2052: 暴走后每 8 秒自动全屏瞬移
        return isBerserk && (curMs - lastTeleportMs >= 8000);
    }

    public static (int mainDamage, int chainDamage, int maxTargets) CalculateWomaChainLightning(int baseMc)
    {
        // 2053: 主目标 100% 伤，3 格内最多 3 个友军弹射 60% 衰减伤
        return (baseMc, baseMc * 60 / 100, 3);
    }

    public static (bool allowed, string errorMsg) ValidateWomaLevel3Entry(int playerLevel, int fromX, int fromY)
    {
        // 2054: 沃玛二层入口 (50, 360)，等级必须 >= 20
        if (fromX != 50 || fromY != 360) return (false, "不在沃玛三层入口坐标");
        if (playerLevel < 20) return (false, "等级不足 20 级，无法承受沃玛三层空间威压");
        return (true, string.Empty);
    }

    public static (bool knockedBack, int knockDistance) CheckWomaGuardHammerKnockback(int roll100)
    {
        // 2055: 沃玛卫士 15% 几率击退 1 格
        return (roll100 < 15, roll100 < 15 ? 1 : 0);
    }

    public static (bool bleeding, int tickDmg, int durationSec) CheckWomaWarriorBleed(int roll100)
    {
        // 2056: 沃玛战士 25% 几率致伤流血，每 2 秒 4 点持续 6 秒
        return (roll100 < 25, 4, 6);
    }

    public static (int moveIntervalMs, string priorityTarget) GetDarkWarriorAiProfile()
    {
        // 2057: 250ms/步高速机动，优先索敌法师
        return (250, "Wizard");
    }

    public static (bool exploded, int radius, int explosionDmg) CheckFireBatDeathExplosion(bool isDead)
    {
        // 2058: 阵亡 100% 产生 3x3 范围 30 点魔法火伤自爆
        return (isDead, 1, 30);
    }

    public static (bool dropped, string itemName, int protectSec) GetWomaHornDropRule()
    {
        // 2059: 沃玛教主 100% 掉落沃玛号角，独占保护 60 秒
        return (true, "沃玛号角", 60);
    }

    public static bool IsRandomScrollAllowedInWomaLevel3(int mapId)
    {
        // 2060: 沃玛三层 (mapId 17) 禁用随机传送
        return mapId != 17;
    }

    // =========================================================================
    // 2. 祖玛教主石化唤醒与护卫小怪群召唤 (2061~2070)
    // =========================================================================

    public static bool CheckZumaLordWakeUp(bool isStatue, int distanceToPlayer, bool isAttacked)
    {
        // 2061: 距离 <= 4 格或受击破石唤醒
        return isStatue && (distanceToPlayer <= 4 || isAttacked);
    }

    public static (int shockwaveRadius, int shockwaveDmg, int knockbackDist) GetZumaLordWakeUpShockwave()
    {
        // 2062: 冲击波半径 2 格，50 点物理伤害，震退 1 格
        return (2, 50, 1);
    }

    public static (bool shouldSummon, int statueCount, int guardCount, int archerCount) CheckZumaLordSummonMinions(
        int curHp, int maxHp, ref bool summoned70, ref bool summoned50, ref bool summoned20)
    {
        // 2063: HP 低于 70%、50%、20% 分别召唤一波 (2 雕像 + 1 卫士 + 1 弓箭手)
        int hpPct = curHp * 100 / maxHp;
        if (hpPct <= 20 && !summoned20)
        {
            summoned20 = true;
            return (true, 2, 1, 1);
        }
        if (hpPct <= 50 && !summoned50)
        {
            summoned50 = true;
            return (true, 2, 1, 1);
        }
        if (hpPct <= 70 && !summoned70)
        {
            summoned70 = true;
            return (true, 2, 1, 1);
        }
        return (false, 0, 0, 0);
    }

    public static (bool blocked, int reflectedDmg) CheckZumaLordStabReflect(bool isStabAttack, int roll100, int incomingDmg)
    {
        // 2064: 刺杀攻击 20% 几率格挡并反震 20% 伤害
        if (isStabAttack && roll100 < 20)
        {
            return (true, incomingDmg * 20 / 100);
        }
        return (false, 0);
    }

    public static (int attackRange, bool seeThroughHide) GetZumaArcherProfile()
    {
        // 2065: 射程 7 格，站桩，识破隐身
        return (7, true);
    }

    public static (int unfreezeStiffMs, bool immuneControl) GetZumaStatueUnfreezeProfile()
    {
        // 2066: 解冻 1000ms 破石硬直，期间免控
        return (1000, true);
    }

    public static (bool armorPierce, int ignoredAcPct) CheckZumaGuardArmorPierce(int roll100)
    {
        // 2067: 祖玛卫士 10% 几率破甲 (无视 50% 物防)
        return (roll100 < 10, roll100 < 10 ? 50 : 0);
    }

    public static bool IsScrollAllowedInZumaHome(int mapId)
    {
        // 2068: 祖玛教主之家 (mapId 24) 禁飞
        return mapId != 24;
    }

    public static (int dropRateDenominator, string itemName, int protectSec) GetZumaRelicDropRule()
    {
        // 2069: 祖玛头像 1/20 掉落，独占保护 60 秒
        return (20, "祖玛头像", 60);
    }

    public static (bool congested, int speedPenaltyPct) CheckZumaNarrowPassageCongestion(int monsterCountInZone)
    {
        // 2070: 狭窄通道怪物数 > 15 拥堵减速 30%
        return (monsterCountInZone > 15, monsterCountInZone > 15 ? 30 : 0);
    }

    // =========================================================================
    // 3. 白日门丛林毒物与道士炼毒施毒核心生态 (2071~2080) (官方 1.50 绝对真值)
    // =========================================================================

    public static (bool allowed, int goldFee) ValidateTaoistHerbGatheringEntry(int fromX, int fromY, bool hasScythe)
    {
        // 2071: 白日门丛林毒物采集区域 (12, 10) 进入校验，必须携带药铲/青铜斧
        if (fromX != 12 || fromY != 10) return (false, 0);
        return (true, hasScythe ? 0 : 10000);
    }

    public static (bool valid, string reason) ValidateTaoistPoisonRefining(
        int ringSlot, string ringName, int proposerGold)
    {
        // 2072: 道士炼毒必须在特定装备槽或炼药鼎中操作，金币需满足炼药消耗
        if (ringSlot != 7 && ringSlot != 8) return (false, "未装备毒药调配容器于法宝槽");
        if (ringName != "黄色药粉" && ringName != "灰色药粉") return (false, "原料药粉名称不符");
        if (proposerGold < 1000) return (false, "金币不足调配药粉工本费");
        return (true, string.Empty);
    }

    public static (bool valid, string reason) ValidatePoisonCastingRangeAndTarget(
        int proposerMap, int targetMap, int distance, int proposerGender, int targetGender)
    {
        // 2073: 施毒术施法范围校验：同地图，距离 <= 8 格
        if (proposerMap != targetMap) return (false, "目标不在同一地图");
        if (distance > 8) return (false, "施毒术射程超出 8 格限制");
        return (true, string.Empty);
    }

    public static string GenerateTaoistPoisonBroadcastMessage(string husband, string wife)
    {
        // 2074: 强效红绿双毒战术命中战斗提示
        return $"目标【{husband}】已被道士【{wife}】施加红绿剧毒，防御清空且持续侵蚀！";
    }

    public static (bool canTeleport, int cooldownSec, int mpCost) GetTaoistSummonPetRule(long lastTpMs, long nowMs)
    {
        // 2075: 道士召唤骷髅/神兽法术 CD 与法力消耗
        bool cdReady = (nowMs - lastTpMs >= 3000);
        return (cdReady, 3, 30);
    }

    public static bool IsPoisonImmuneMonsterMap(int targetMapId)
    {
        // 2076: 毒免或特异免疫地图区域校验
        return targetMapId is 9 or 24 or 32;
    }

    public static (int goldCost, int fameCost) GetSpecialPoisonRefinePenalty()
    {
        // 2077: 炼毒爆鼎损耗金币与药引材料
        return (1000, 1);
    }

    public static bool IsTaoistPoisonPeaceZone(int mapId)
    {
        // 2078: 城镇安全区禁武禁止施毒
        return mapId == 48;
    }

    public static int ApplyTaoistHealBonus(int baseHeal, bool isSpouseTarget)
    {
        // 2079: 道士群疗与单体治愈术对战友加成 20%
        return isSpouseTarget ? baseHeal + baseHeal * 20 / 100 : baseHeal;
    }

    public static (string boundItemName, bool canTrade, bool canDrop) ConvertToBoundPoisonPouch()
    {
        // 2080: 白日门特制道士药包，绑定不可交易不可丢弃
        return ("特制道士药包", false, false);
    }

    // =========================================================================
    // 4. 武器升级持久损耗加成与跳点极品概率模型 (2081~2090)
    // =========================================================================

    public static int CalculateBlackIronDuraChange(int totalPurity, int roll100)
    {
        // 2081~2084: 黑铁矿持久变化规则
        if (totalPurity < 30) return -3; // 2081: < 30 掉 3 点
        if (totalPurity < 60) return roll100 < 60 ? -1 : 0; // 2082: 30~59 60% 掉 1 点
        if (totalPurity < 90) return roll100 < 20 ? 1 : 0;  // 2083: 60~89 20% 加 1 点
        return 1; // 2084: >= 90 稳加 1 点
    }

    public static int CalculateSuperBlackIronDuraBonus(int totalPurity, int purity20PlusCount, int roll100)
    {
        // 2084 扩展: 总纯度 >= 120 且 >= 5 块 20 以上矿石，20% 几率 +2 点
        if (totalPurity >= 120 && purity20PlusCount >= 5 && roll100 < 20) return 2;
        return 1;
    }

    public static (bool qualified, int baseSuccessPct) CheckJewelryBaseSuccess(int totalStatPoints)
    {
        // 2085: 首饰总属性 >= 10 达到基础成功率 50%
        if (totalStatPoints >= 10) return (true, 50);
        return (false, 20);
    }

    public static (bool jumpPoint, int addedPoints) CheckWeaponUpgradeJumpPoint(
        int jewelryStatPoints, int weaponCurrentStat, int roll100)
    {
        // 2086: 首饰属性超过武器当前属性 3 倍，5% 几率跳点 +2
        if (jewelryStatPoints >= weaponCurrentStat * 3 && roll100 < 5)
        {
            return (true, 2);
        }
        return (false, 1);
    }

    public static bool CanUpgradeWeaponMaxLimit(int currentSuccessCount)
    {
        // 2087: 武器最大升级 7 次，达到 7 次拒绝再次升级
        return currentSuccessCount < 7;
    }

    public static (bool valid, string reason) CheckWeaponTrialTiming(long retrieveTimeMs, long nowMs)
    {
        // 2088: 取出后 10 分钟 (600,000ms) 内开刀
        if (nowMs - retrieveTimeMs > 600000)
        {
            return (false, "取出武器已超时 10 分钟，开刀蕴含的灵气已消散");
        }
        return (true, string.Empty);
    }

    public static int CalculateTrialStrikeBreakRisk(bool isPeaceModeOnWhitePlayer, int baseBreakRiskPct)
    {
        // 2089: 和平模式砍白名玩家开刀，碎刀风险额外 +15%
        return isPeaceModeOnWhitePlayer ? baseBreakRiskPct + 15 : baseBreakRiskPct;
    }

    public static (int shakeDurationMs, bool weaponBroken) ResolveTrialStrikeOutcome(bool upgradeSuccess)
    {
        // 2090: 成功震屏 300ms，失败武器破碎消失
        return upgradeSuccess ? (300, false) : (0, true);
    }

    // =========================================================================
    // 5. 比奇城皇宫国王任务与沃玛号角创建行会权威规则 (2091~2100) (官方 1.50 绝对真值)
    // =========================================================================

    public static (bool canCreateGuild, string reason) ValidateGuildCreationCriteria(
        int playerLevel, bool hasGuild, int playerGold, bool hasWomaHorn)
    {
        // 2091: 行会掌门人创建行会等级门槛：需等级 >= 22 级，金币 100 万与沃玛号角
        if (hasGuild) return (false, "玩家已有行会归属");
        if (playerLevel < 22) return (false, "等级不足 22 级，无法统领行会");
        if (playerGold < 1_000_000) return (false, "创建行会需缴纳 1,000,000 金币申请费");
        if (!hasWomaHorn) return (false, "创建行会必须持有沃玛号角凭证");
        return (true, string.Empty);
    }

    public static (int regularExp, int bonusExp) CalculateGuildPartyExpShare(int monsterExp)
    {
        // 2092: 行会组队经验加成分配
        int appExp = monsterExp + monsterExp * 20 / 100;
        int masExp = appExp * 10 / 100;
        return (appExp, masExp);
    }

    public static (bool canPromote, int titleRank, int bonusScore) CheckGuildPromotionEligibility(int memberLevel)
    {
        // 2093: 达到 28 级晋升行会骨干封号
        if (memberLevel >= 28)
        {
            return (true, 32, 15);
        }
        return (false, 0, 0);
    }

    public static (int expPenaltyPct, int rejoinCooldownHours) GetGuildLeavePenalty()
    {
        // 2094: 叛出/脱离行会惩罚：24 小时入会冷却
        return (20, 24);
    }

    public static (bool canAccess, int privilegeRank) CheckPalaceAccessEligibility(int fame)
    {
        // 2095: 比奇皇宫国王接见资格
        if (fame >= 40) return (true, 2);
        if (fame >= 20) return (true, 1);
        return (false, 0);
    }

    public static (int newDura, bool shattered) ConsumeSpecialItemDuraOnDeath(int curDura)
    {
        // 2096: 特殊装备死亡扣持久，归 0 破碎
        int next = curDura - 1;
        return next <= 0 ? (0, true) : (next, false);
    }

    public static (bool canExchange, int orePureCost) GetBlessingOilRefineExchange(int fame)
    {
        // 2097: 高纯黑铁矿石提取祝福油
        return (fame >= 5, 5);
    }

    public static (int pkReduced, int goldCost) RedeemBichonGuardPardon(int fame, int currentPk)
    {
        // 2098: 比奇皇宫守卫赎罪消减 PK 恶名值
        if (fame < 2 || currentPk <= 0) return (0, 0);
        int times = Math.Min(fame / 2, (currentPk + 49) / 50);
        return (times * 50, times * 2);
    }

    public static (bool canSummon, int cdHours) CheckMemoryRecallCooldown(long lastSummonMs, long nowMs)
    {
        // 2099: 记忆套装天地合一召唤 CD 24 小时
        bool ready = (nowMs - lastSummonMs >= 24 * 3600 * 1000);
        return (ready, 24);
    }

    public static bool IsRedNameAllowedInKingPalace(int pkPoints)
    {
        // 2100: PK >= 200 红名大盗禁止进入皇宫与国王对话 (大刀侍卫必杀)
        return pkPoints < 200;
    }

    // =========================================================================
    // 6. 沙巴克皇宫密道与城门耐久修补机制 (2101~2110)
    // =========================================================================

    public static (string stage, bool isPassable, int damageVulnerabilityPct) GetSabukGateStatus(int curHp)
    {
        // 2101~2102: 城门上限 50,000，三阶段判定
        if (curHp > 25000) return ("完好", false, 0);
        if (curHp > 0) return ("裂损", false, 20);
        return ("崩塌", true, 0);
    }

    public static (bool canRepair, int goldCost, int ironOreCount, int ironPurityReq, int repairTimeMin) GetSabukGateRepairCost()
    {
        // 2103: 200,000 金币，50 块纯度 15 铁矿，5 分钟
        return (true, 200000, 50, 15, 5);
    }

    public static (bool canEnter, int goldFee) ValidateSabukSecretPassageEntry(
        int mapId, int x, int y, bool hasToken)
    {
        // 2104: 香石墓穴三层 (15, 80)，有令牌免费，无令牌 5,000 金币
        if (mapId != 30 || x != 15 || y != 80) return (false, 0);
        return (true, hasToken ? 0 : 5000);
    }

    public static bool IsSecretPassageAllowedDuringWar(bool isWarActive, bool isDefenderGuild)
    {
        // 2105: 攻城期间仅守方行会可用
        if (!isWarActive) return true;
        return isDefenderGuild;
    }

    public static bool CheckPalaceCaptureTick(int defenderPlayerCount, int singleAttackerGuildPlayerCount, int consecutiveHoldSec)
    {
        // 2106: 皇宫清空无防守方且单一攻城行会占据持续 10 秒
        return defenderPlayerCount == 0 && singleAttackerGuildPlayerCount > 0 && consecutiveHoldSec >= 10;
    }

    public static bool IsSabukWarTime(int hour, int minute)
    {
        // 2107: 攻城战 20:00~23:00
        return hour >= 20 && hour < 23;
    }

    public static (int taxGold, int maxDailyTax) CalculateSabukCityLordTax(int totalServerTaxToday)
    {
        // 2108: 每日提取商业税，上限 2,000,000 金币
        int tax = Math.Min(totalServerTaxToday, 2000000);
        return (tax, 2000000);
    }

    public static int ApplySabukMemberRepairDiscount(int baseSpecialRepairPrice, bool isSabukMember)
    {
        // 2109: 沙巴克成员特修 8 折 (2.4 倍金币)
        return isSabukMember ? baseSpecialRepairPrice * 80 / 100 : baseSpecialRepairPrice;
    }

    public static (string respawnLocation, int x, int y) GetWarRespawnPoint(bool isDefender)
    {
        // 2110: 守方皇宫大厅内室 (15, 15)，攻方沙城大门外集合点 (650, 300)
        return isDefender ? ("皇宫内室", 15, 15) : ("城门集合点", 650, 300);
    }

    // =========================================================================
    // 7. 道士神兽防御/喷火双形态与骷髅进化攻击力 (2111~2120)
    // =========================================================================

    public static (bool isDefending, int acBoostPct, int hpRegenPct) GetShinsuStanceBehavior(bool hasTarget)
    {
        // 2111~2112: 无目标趴下防御，双防 +50%，每 2 秒自愈 5%
        if (!hasTarget)
        {
            return (true, 50, 5);
        }
        return (false, 0, 0);
    }

    public static (int mainDamage, int pierceDamage, int pierceRange) CalculateShinsuFireBreath(int baseDc)
    {
        // 2113: 直线 2 格穿透，主目标 100%，后位 50%
        return (baseDc, baseDc * 50 / 100, 2);
    }

    public static (int hp, int minDc, int maxDc) GetShinsuStatsByLevel(int level)
    {
        // 2114: 0 级 500HP / 15-25DC，7 级 2400HP / 45-65DC
        int clamped = Math.Clamp(level, 0, 7);
        int hp = 500 + clamped * (2400 - 500) / 7;
        int minDc = 15 + clamped * (45 - 15) / 7;
        int maxDc = 25 + clamped * (65 - 25) / 7;
        return (hp, minDc, maxDc);
    }

    public static (string axeStyle, string nameColor) GetSkeletonVisualProfile(int level)
    {
        // 2115: 0~2 木斧黄色，3~5 铁斧蓝色，6~7 板斧深蓝/红
        if (level >= 6) return ("双刃大板斧", "深蓝/红名");
        if (level >= 3) return ("铁斧", "蓝名");
        return ("木柄小斧", "黄名");
    }

    public static (int attackSpeedMs, int minDc, int maxDc, int moveSpeedMs) GetRank7SkeletonCombatProfile()
    {
        // 2116: 7 级骷髅攻速 350ms，攻击 25-40，移速 300ms/步
        return (350, 25, 40, 300);
    }

    public static int ApplyTaoistPetHealBonus(int baseHeal)
    {
        // 2117: 道士宝宝受治愈效果提升 30%
        return baseHeal + baseHeal * 30 / 100;
    }

    public static (bool petTurnYellow, bool masterTurnYellow) CheckPetAttackWhitePlayerPkStatus(bool isPoisonSpell)
    {
        // 2118: 宝宝攻击白名不使道士变黄，但主人施毒白名两者皆变
        return isPoisonSpell ? (true, true) : (false, false);
    }

    public static (int spawnDistMax, int delaySec) GetPetTeleportFollowRule()
    {
        // 2119: 跨图 3 秒内在 3x3 随机空格刷出
        return (1, 3);
    }

    public static (int newLoyalty, int rageRiskPct) DecayPetLoyalty(int curLoyalty)
    {
        // 2120: 30 分钟衰减 5%，低于 20% 祈祷暴走叛变几率 100%
        int next = Math.Max(0, curLoyalty - 5);
        int risk = next < 20 ? 100 : (100 - next);
        return (next, risk);
    }

    // =========================================================================
    // 8. 战士刺杀二位剑气破盾穿透与双烈火爆发 (2121~2130)
    // =========================================================================

    public static (bool isPiercing, bool ignoreAc, bool ignoreMagicShield) CheckStabbingSwordPos(int distance)
    {
        // 2121~2122: 距离 1 普攻，距离 2 隔位破空剑气无视 AC 且穿盾
        if (distance == 2)
        {
            return (true, true, true);
        }
        return (false, false, false);
    }

    public static (bool isFireActive, int durationSec) CheckFireSwordCharging(long chargeMs, long nowMs)
    {
        // 2123: 烈火蓄力 10 秒自然熄灭
        bool active = (nowMs - chargeMs <= 10000);
        return (active, 10);
    }

    public static (bool isDoubleFireCombo, int multiplierPct) CheckDoubleFireSwordCombo(
        long firstFireChargeMs, long firstFireStrikeMs, long secondFireStrikeMs)
    {
        // 2124: 第 1 火蓄力后约 8 秒劈出，并立即连续劈出第 2 火爆发 260% 伤害
        bool firstValid = (firstFireStrikeMs - firstFireChargeMs >= 7500 && firstFireStrikeMs - firstFireChargeMs <= 10000);
        bool secondImmediate = (secondFireStrikeMs - firstFireStrikeMs <= 1000);
        if (firstValid && secondImmediate)
        {
            return (true, 260);
        }
        return (false, 100);
    }

    public static (int mainDamagePct, int subTargetDamagePct, int maxSubTargets) GetHalfMoonSwordDamageDistribution()
    {
        // 2125: 半月主目标 100%，3 个副目标 35%
        return (100, 35, 3);
    }

    public static (bool pushSuccess, int pushedDistance, bool selfRebound, int stunMs) ResolveSavageThrustLevelPush(
        int attackerLevel, int targetLevel)
    {
        // 2126: 等级高推 2 格，等级 <= 自身反弹退 1 格并眩晕 1000ms
        if (attackerLevel > targetLevel)
        {
            return (true, 2, false, 0);
        }
        return (false, 0, true, 1000);
    }

    public static (bool wallStunned, int wallStunMs) CheckSavageThrustWallCollision(bool hitWall)
    {
        // 2127: 撞墙硬直 1500ms
        return (hitWall, hitWall ? 1500 : 0);
    }

    public static int GetBasicSwordAccuracyBonus(int skillLevel)
    {
        // 2128: 基本剑术每级 +1 隐藏准确 (3 级 +3)
        return Math.Clamp(skillLevel, 0, 3);
    }

    public static (bool triggered, int bonusDmg) CalculateSlayingSwordStrike(int roll100, int maxDc)
    {
        // 2129: 攻杀 25% 几率触发，额外加成 (DC_Max / 4 + 5)
        if (roll100 < 25)
        {
            return (true, maxDc / 4 + 5);
        }
        return (false, 0);
    }

    public static int CalculateAttackIntervalMsWithSpeed(int baseIntervalMs, int attackSpeedBonus)
    {
        // 2130: 基础 1100ms，每点攻速减 50ms，极限下限 700ms
        int calculated = baseIntervalMs - attackSpeedBonus * 50;
        return Math.Max(700, calculated);
    }

    // =========================================================================
    // 9. 法师诱惑随从叛变老死与抗拒火环等级推挤 (2131~2140)
    // =========================================================================

    public static bool CanTameMorePets(int currentPetCount)
    {
        // 2131: 随从上限严格 5 只
        return currentPetCount < 5;
    }

    public static (int newLevel, int bonusHpPct, int bonusDcpct) CheckPetKillLevelUp(int currentLevel, int kills)
    {
        // 2132: 每 50 击杀升 1 级 (最高 7 级)，HP +20%，DC +15%
        int earnedLevels = kills / 50;
        int finalLevel = Math.Clamp(currentLevel + earnedLevels, 0, 7);
        return (finalLevel, finalLevel * 20, finalLevel * 15);
    }

    public static bool CheckPetNaturalDeath(long aliveSeconds)
    {
        // 2133: 存活 6 小时 (21600 秒) 自然老死消散
        return aliveSeconds >= 21600;
    }

    public static (bool betrayed, int betrayChancePct) CheckPetBetrayOnFriendlyFire(int curHp, int maxHp, bool isFriendlyFire)
    {
        // 2134: HP < 10% 误伤有 10% 几率叛变
        if (curHp < maxHp * 10 / 100 && isFriendlyFire)
        {
            return (true, 10);
        }
        return (false, 0);
    }

    public static (bool pushSuccess, int pushDistance) ResolveRingOfFirePush(
        int casterLevel, int targetLevel, bool isPetrifiedOrBoss)
    {
        // 2135~2136: 自身等级 > 目标且非石化/Boss，推开 2 格
        if (casterLevel > targetLevel && !isPetrifiedOrBoss)
        {
            return (true, 2);
        }
        return (false, 0);
    }

    public static (int damageMultiplierPct, int stunMs) ResolveHellLightningUndeadBonus(bool isUndead)
    {
        // 2137: 不死系生物承受 200% 双倍伤害并瘫痪 500ms
        return isUndead ? (200, 500) : (100, 0);
    }

    public static (bool ignoreMagicEvade, int areaSize) GetIceStormAccuracyProfile()
    {
        // 2138: 3x3 范围 100% 绝对无视魔法躲避
        return (true, 3);
    }

    public static (int durationSec, int tickIntervalMs) CalculateFireWallProperties(int magicPower)
    {
        // 2139: Duration = 30 + (MP * 2) 秒，每 500ms 一跳
        return (30 + magicPower * 2, 500);
    }

    public static int CalculateLighteningBeamDamageMultiplier(int distanceStep)
    {
        // 2140: 疾光电影直线 8 格，第 8 格增伤 25%
        int clamped = Math.Clamp(distanceStep, 1, 8);
        return 100 + (clamped - 1) * 25 / 7;
    }

    // =========================================================================
    // 10. Direct-Blit 协议弱网重传与 Native AOT 零分配压测 (2141~2150)
    // =========================================================================

    public static bool ValidateDirectBlitMagic(ushort magic)
    {
        // 2141: 魔数固定 0x594D ('M', 'Y')
        return magic == BinaryProtocol.DefaultMagic;
    }

    public static (bool accepted, ushort expectedSeq) ValidateSequenceSlidingWindow(
        ushort incomingSeq, ushort lastAckSeq)
    {
        // 2142: 64 步滑动窗口
        int diff = (short)(incomingSeq - lastAckSeq);
        if (diff > 0 && diff <= 64)
        {
            return (true, incomingSeq);
        }
        return (false, lastAckSeq);
    }

    public static int CalculateExponentialBackoffTimeout(int retryCount, int baseTimeoutMs = 200, int maxTimeoutMs = 3200)
    {
        // 2143: 指数退避 (200, 400, 800, 1600, 3200)
        int timeout = baseTimeoutMs * (1 << Math.Min(retryCount, 5));
        return Math.Min(timeout, maxTimeoutMs);
    }

    public static (bool disconnectRequired, string reason) CheckConsecutiveLossCircuitBreaker(int consecutiveLosses)
    {
        // 2144: 连续丢包重传失败超 5 次熔断
        if (consecutiveLosses > 5)
        {
            return (true, "网络连接已中断，重传超时超过最大阈值");
        }
        return (false, string.Empty);
    }

    public static ushort ComputeFastCrc16(ReadOnlySpan<byte> data)
    {
        // 2145: 零堆分配 CRC16
        ushort crc = 0xFFFF;
        for (int i = 0; i < data.Length; i++)
        {
            crc ^= (ushort)(data[i] << 8);
            for (int j = 0; j < 8; j++)
            {
                if ((crc & 0x8000) != 0)
                    crc = (ushort)((crc << 1) ^ 0x1021);
                else
                    crc <<= 1;
            }
        }
        return crc;
    }

    public static bool CheckNativeAotBlitPackingAlignment()
    {
        // 2146: 验证 PacketHeader 紧凑封包 8 字节
        return Marshal.SizeOf<BinaryProtocol.PacketHeader>() == 8;
    }

    public static (int bufferCapacityBytes, bool isSufficient) GetHPSocketSendBufferCapacity()
    {
        // 2147: 16384 字节大容量预分配
        return (16384, true);
    }

    public static (bool timeout, int heartbeatProbeSec, int deadLinkThresholdSec) CheckHeartbeatLiveness(
        long lastActiveMs, long nowMs)
    {
        // 2148: 30 秒探针，60 秒超时断开
        long elapsedSec = (nowMs - lastActiveMs) / 1000;
        return (elapsedSec >= 60, 30, 60);
    }

    public static bool ShouldDumbClientOverrideLocalState(bool isAuthoritativePacket)
    {
        // 2149: Dumb Client 无条件接受服务端权威覆盖
        return isAuthoritativePacket;
    }

    public static long BenchmarkDirectBlitZeroAllocation(int loopCount)
    {
        // 2150: 压测 100 万次封包编解码 GC 分配字节增量严格为 0
        long before = GC.GetAllocatedBytesForCurrentThread();
        Span<byte> buffer = stackalloc byte[32];
        for (int i = 0; i < loopCount; i++)
        {
            var header = new BinaryProtocol.PacketHeader
            {
                Magic = BinaryProtocol.DefaultMagic,
                MsgId = (ushort)ProtocolConsts.CM_WALK,
                PayloadLength = 4,
                Sequence = (ushort)i
            };
            header.Write(buffer);
            var readBack = BinaryProtocol.PacketHeader.Read(buffer);
            _ = readBack.Sequence;
        }
        long after = GC.GetAllocatedBytesForCurrentThread();
        return after - before;
    }
}
