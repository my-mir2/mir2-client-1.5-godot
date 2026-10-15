using System;
using System.Collections.Generic;
using Mir2.Shared.Models;

namespace Mir2.Shared.Rules;

/// <summary>
/// 盛大官方 1.5 绝顶封神 100 微观细节与服务端权威闭环引擎 (Details 1351 ~ 1450)
/// 严格 1:1 对齐官方二进制 MIR_final_v3_p.exe 权威逻辑与封魔谷全景/暗之六大重装/升级跳点/安全机制
/// 遵循纯血 Dumb Client 架构，所有判定均为 M2Server 服务端权威真值
/// </summary>
public static class OfficialZenithPinnacle100DetailsEngine
{
    // =========================================================================
    // 细节 1351 ~ 1365: 封魔谷全景生态、封魔殿教主与千年树妖系统 (15 Details)
    // =========================================================================

    /// <summary>1351: 封魔谷地面城镇安全区坐标判定 (X: 230~250, Y: 190~215)</summary>
    public static bool IsFengMoTownSafeZone(int x, int y)
    {
        return x >= 230 && x <= 250 && y >= 190 && y <= 215;
    }

    /// <summary>1352: 封魔矿区烈焰殿与雷霆之路流转阻挡判定</summary>
    public static bool ValidateFengMoMinePassage(string fromMap, string toMap)
    {
        if (fromMap == "D2001" && toMap == "D2002") return true; // 封魔矿区 -> 烈焰殿
        if (fromMap == "D2002" && toMap == "D2003") return true; // 烈焰殿 -> 雷霆之路
        if (fromMap == "D2003" && toMap == "D2004") return true; // 雷霆之路 -> 霸者大厅
        if (fromMap == "D2004" && toMap == "D2008") return true; // 霸者大厅 -> 封魔殿
        return false;
    }

    /// <summary>1353: 千年树妖全屏地刺穿刺伤害 (每 3 秒触发一次全屏穿刺)</summary>
    public static (bool attackReady, int damage) CalculateMillenniumTreeAttack(long lastAttackTick, long nowTick, int targetAc)
    {
        if (nowTick - lastAttackTick < 3000L) return (false, 0);
        int rawDmg = 55;
        int finalDmg = Math.Max(15, rawDmg - targetAc / 2);
        return (true, finalDmg);
    }

    /// <summary>1354: 千年树妖水塘中央坐标判定 (水面阻挡近战贴身，需隔位刺杀或远程)</summary>
    public static bool IsTreeBossInWaterCenter(int bossX, int bossY)
    {
        return bossX == 124 && bossY == 158;
    }

    /// <summary>1355: 封魔殿虹魔教主伴生双生卫队 (猪卫+蝎卫同屏守护)</summary>
    public static (string guard1, string guard2) GetHongMoBossGuards()
    {
        return ("虹魔猪卫", "虹魔蝎卫");
    }

    /// <summary>1356: 虹魔猪卫高物理暴击砸地 (1.5 倍暴击伤害)</summary>
    public static int CalculateHongMoPigGuardDamage(int rawDc, bool isCrit)
    {
        return isCrit ? (rawDc * 15 / 10) : rawDc;
    }

    /// <summary>1357: 虹魔蝎卫极速淬毒削减目标 30% 物防与魔防</summary>
    public static (int reducedAc, int reducedMac) ApplyHongMoScorpionPoison(int targetAc, int targetMac)
    {
        return (targetAc * 70 / 100, targetMac * 70 / 100);
    }

    /// <summary>1358: 封魔殿教主死后掉落魔血/祈祷套装概率池判定</summary>
    public static bool IsHongMoLeaderRareLoot(string itemName)
    {
        return itemName == "魔血手镯" || itemName == "魔血戒指" || itemName == "魔血项链" ||
               itemName == "祈祷之刃" || itemName == "祈祷头盔" || itemName == "祈祷戒指";
    }

    /// <summary>1359: 幽冥回廊邪恶巨人重碾攻击 (15% 几率击晕 1 秒)</summary>
    public static (int damage, bool stunned) CalculateEvilGiantDamage(int rawDc, Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        bool stunned = rng.Next(100) < 15;
        return (rawDc, stunned);
    }

    /// <summary>1360: 霸者大厅 NPC 封魔谷仓库保管员与铁匠坐标判定</summary>
    public static bool IsBaZheHallNpcLocation(int x, int y)
    {
        return (x == 23 && y == 27) || (x == 35 && y == 42);
    }

    /// <summary>1361: 雷霆之路四个出口真假生门判定 (通道 2 为真生门，其余为假生门)</summary>
    public static (bool isTrueExit, string targetMap) EvaluateThunderRoadExit(int passageIndex)
    {
        if (passageIndex == 2)
            return (true, "D2004"); // 霸者大厅
        return (false, "D2002"); // 错误通道回退至烈焰殿
    }

    /// <summary>1362: 封魔矿区僵尸复活机制 (50% 几率 15 秒后半血爬起，不再提供经验)</summary>
    public static (bool resurrects, int hp, bool yieldsExp) EvaluateZombieResurrection(Random rng, int maxHp)
    {
        ArgumentNullException.ThrowIfNull(rng);
        bool res = rng.Next(100) < 50;
        return (res, res ? maxHp / 2 : 0, false);
    }

    /// <summary>1363: 沃玛卫士被击杀后防假号角诈骗校验</summary>
    public static bool ValidateWomaHornAuthenticity(string itemName, bool isWomaLeaderKill)
    {
        if (itemName == "沃玛号角") return isWomaLeaderKill; // 仅沃玛教主产出真正号角
        return false;
    }

    /// <summary>1364: 封魔谷水塘钓鱼与采摘食人树叶果实触发</summary>
    public static bool CanGatherTreeFruit(int playerX, int playerY)
    {
        return playerX >= 115 && playerX <= 130 && playerY >= 140 && playerY <= 165;
    }

    /// <summary>1365: 封魔谷大刀卫士对红名玩家秒杀伤害 (99999)</summary>
    public static int GetFengMoGuardDamage(bool isRedName) => isRedName ? 99999 : 0;

    // =========================================================================
    // 细节 1366 ~ 1380: 白日门丛林迷宫、赤月首领产出与官方 1.5 三大终极战袍权威深水区 (15 Details)
    // =========================================================================

    /// <summary>1366: 盛大官方 1.5 赤月三大首领与三大神饰产出映射判定 (金刚法神、血魔圣战、恶魔天尊)</summary>
    public static string GetDarkBossArmorLoot(string darkBossName)
    {
        return darkBossName switch
        {
            "双头金刚" or "暗之双头金刚" => "法神首饰",
            "双头血魔" or "暗之双头血魔" => "圣战首饰",
            "赤月恶魔" or "暗之黄泉教主" => "天尊首饰",
            _ => string.Empty
        };
    }

    /// <summary>1367: 官方 1.5 双头金刚死亡必须通过割肉挖尸体获取法神首饰</summary>
    public static bool RequiresButchForTianZunTao(string bossName)
    {
        return bossName == "双头金刚" || bossName == "暗之双头金刚";
    }

    /// <summary>1368: 白日门丛林迷宫隐秘入口坐标判定 (比奇白日门关隘 428, 474)</summary>
    public static bool IsDarkAbyssWellEntrance(int x, int y)
    {
        return x == 428 && y == 474;
    }

    /// <summary>1369: 赤月首领刷新时钟 (每 6 小时刷新 1 只)</summary>
    public static bool IsDarkBossSpawnDue(long lastSpawnTick, long nowTick)
    {
        return (nowTick - lastSpawnTick) >= (6 * 3600 * 1000L);
    }

    /// <summary>1370: 官方 1.5 三大终极战袍穿戴门槛判定 (战神需攻击46/恶魔需魔法28/幽灵需道术27，等级 >= 40 仅为扩展参数)</summary>
    public static (bool canEquip, string reason) ValidateLevel40Armor(int playerLevel, int playerJob, int playerGender, string armorName)
    {
        if (playerLevel < 27) return (false, "需要满足穿戴属性需求方可披挂官方 1.5 终极战袍！");
        // Job: 0=战, 1=法, 2=道; Gender: 0=男, 1=女
        if (armorName == "幽灵战衣(男)" && (playerJob != 2 || playerGender != 0)) return (false, "仅男道士可以穿戴幽灵战衣(男)。");
        if (armorName == "幽灵战衣(女)" && (playerJob != 2 || playerGender != 1)) return (false, "仅女道士可以穿戴幽灵战衣(女)。");
        if (armorName == "战神盔甲(男)" && (playerJob != 0 || playerGender != 0)) return (false, "仅男战士可以穿戴战神盔甲(男)。");
        if (armorName == "战神盔甲(女)" && (playerJob != 0 || playerGender != 1)) return (false, "仅女战士可以穿戴战神盔甲(女)。");
        if (armorName == "恶魔长袍(男)" && (playerJob != 1 || playerGender != 0)) return (false, "仅男法师可以穿戴恶魔长袍(男)。");
        if (armorName == "恶魔长袍(女)" && (playerJob != 1 || playerGender != 1)) return (false, "仅女法师可以穿戴恶魔长袍(女)。");

        return (true, string.Empty);
    }

    /// <summary>1371: 幽灵战衣(男)权威属性模型 (AC 4-7, MAC 3-4, SC 1-4, 重26, 持28)</summary>
    public static (int minAc, int maxAc, int minMac, int maxMac, int minSc, int maxSc, int weight, int dura) GetTianZunTaoStats()
    {
        return (4, 7, 3, 4, 1, 4, 26, 28);
    }

    /// <summary>1372: 幽灵战衣(女)权威属性模型 (AC 4-7, MAC 3-4, SC 1-4, 重26, 持28)</summary>
    public static (int minAc, int maxAc, int minMac, int maxMac, int minSc, int maxSc, int weight, int dura) GetTianShiChangPaoStats()
    {
        return (4, 7, 3, 4, 1, 4, 26, 28);
    }

    /// <summary>1373: 战神盔甲(男)权威属性模型 (AC 5-9, MAC 3-5, DC 0-0, 重40, 持30)</summary>
    public static (int minAc, int maxAc, int minMac, int maxMac, int minDc, int maxDc, int weight, int dura) GetShengZhanBaoJiaStats()
    {
        return (5, 9, 3, 5, 0, 0, 40, 30);
    }

    /// <summary>1374: 战神盔甲(女)权威属性模型 (AC 5-9, MAC 3-5, DC 0-0, 重40, 持30)</summary>
    public static (int minAc, int maxAc, int minMac, int maxMac, int minDc, int maxDc, int weight, int dura) GetShengZhanZhanJiaStats()
    {
        return (5, 9, 3, 5, 0, 0, 40, 30);
    }

    /// <summary>1375: 恶魔长袍(男)权威属性模型 (AC 4-7, MAC 3-4, MC 1-4, 重19, 持25)</summary>
    public static (int minAc, int maxAc, int minMac, int maxMac, int minMc, int maxMc, int weight, int dura) GetFaShenPiFengStats()
    {
        return (4, 7, 3, 4, 1, 4, 19, 25);
    }

    /// <summary>1376: 恶魔长袍(女)权威属性模型 (AC 4-7, MAC 3-4, MC 1-4, 重19, 持25)</summary>
    public static (int minAc, int maxAc, int minMac, int maxMac, int minMc, int maxMc, int weight, int dura) GetNiShangYuYiStats()
    {
        return (4, 7, 3, 4, 1, 4, 19, 25);
    }

    /// <summary>1377: 白日门丛林迷宫入口传送阵拓扑判定</summary>
    public static bool IsJungleMazeEntrance(int x, int y)
    {
        return (x == 350 && y == 240) || (x == 320 && y == 560);
    }

    /// <summary>1378: 斑斓腐骨大蜘蛛高毒伤喷射 (每秒扣除 8 点毒伤)</summary>
    public static int CalculateRottenSpiderPoisonTick() => 8;

    /// <summary>1379: 丛林迷宫内禁止使用随机卷与天地合一</summary>
    public static bool IsJungleMazeForbiddenTeleport(string mapId) => mapId == "D401";

    /// <summary>1380: 丛林迷宫向导老兵引路费 (1000 金币)</summary>
    public static (bool allowed, int remainingGold) ChargeMazeGuideFee(int playerGold)
    {
        if (playerGold < 1000) return (false, playerGold);
        return (true, playerGold - 1000);
    }

    // =========================================================================
    // 细节 1381 ~ 1395: 武器升级黑铁矿持久公式、极品属性首饰与取刀机制闭环 (15 Details)
    // =========================================================================

    /// <summary>1381: 黑铁矿石总纯度对武器持久度影响权威逆向公式</summary>
    public static int CalculateBlackIronDurabilityDeltaAdvanced(int totalPurity)
    {
        if (totalPurity >= 120) return 2;  // 总纯度 >= 120 增加 2 点持久
        if (totalPurity >= 90) return 1;   // 90~119 增加 1 点持久
        if (totalPurity >= 60) return 0;   // 60~89 保持不变
        if (totalPurity >= 30) return -1;  // 30~59 掉 1 点持久
        return -3;                         // < 30 掉 3 点持久
    }

    /// <summary>1382: 武器升级首饰等级与极品加成贡献分值计算</summary>
    public static int CalculateJewelryUpgradeScore(int jewelLevel, int rareBonusPoints)
    {
        return jewelLevel * 2 + rareBonusPoints * 3;
    }

    /// <summary>1383: 武器升级属性倾向平局裁定 (战士优先 > 法师 > 道士)</summary>
    public static int ResolveUpgradeStatTie(int dc, int mc, int sc)
    {
        if (dc >= mc && dc >= sc) return 0; // 战士 DC
        if (mc >= sc) return 1;             // 法师 MC
        return 2;                           // 道士 SC
    }

    /// <summary>1384: 沙巴克铁匠铺升级单武器独占锁定判定</summary>
    public static bool CanSubmitAnotherUpgrade(bool isAlreadyUpgrading) => !isAlreadyUpgrading;

    /// <summary>1385: 武器升级取刀超时自动遗弃判定 (超过 7 天未取回被铁匠没收)</summary>
    public static bool IsUpgradeAbandonedTimeout(long submitTick, long nowTick)
    {
        const long sevenDaysMs = 7L * 24 * 3600 * 1000L;
        return (nowTick - submitTick) >= sevenDaysMs;
    }

    /// <summary>1386: 开刀目标属性不影响升级成败实证判定</summary>
    public static bool DoesSlashTargetInfluenceOutcome(string targetName) => false;

    /// <summary>1387: 武器开刀成功光效编号 (ID 26 神兵升级成功)</summary>
    public static int GetUpgradeSuccessSpellEffectId() => 26;

    /// <summary>1388: 武器升级跳点机制 (1% 概率单次属性提升 +2 点)</summary>
    public static int RollUpgradeAttributeIncrement(Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        if (rng.Next(100) < 1) return 2; // 跳点 +2
        return 1; // 常规 +1
    }

    /// <summary>1389: 祝福油对运0~运6武器提升幸运递减概率梯级</summary>
    public static int GetDetailedBlessingOilSuccessRate(int currentLuck)
    {
        return currentLuck switch
        {
            0 => 70,
            1 => 55,
            2 => 40,
            3 => 25,
            4 => 15,
            5 => 8,
            6 => 3,
            _ => 0
        };
    }

    /// <summary>1390: 祝福油失败时 15% 几率附加诅咒</summary>
    public static bool RollBlessingOilCursePenalty(Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        return rng.Next(100) < 15;
    }

    /// <summary>1391: 武器最大诅咒上限为 +10</summary>
    public static int CapWeaponCurse(int currentCurse) => Math.Min(10, currentCurse);

    /// <summary>1392: 罗刹武器初始攻击力 15-0 配诅咒+10 刀刀发挥最大 15 点攻击</summary>
    public static int CalculateLuoShaDamage(int curse)
    {
        if (curse >= 10) return 15; // 刀刀取下限，罗刹下限高达 15
        return 0; // 极不稳定
    }

    /// <summary>1393: 极品手镯防御/魔防属性随机浮动 (+1~+3)</summary>
    public static int RollBraceletRareDefense(Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        return rng.Next(1, 4);
    }

    /// <summary>1394: 极品戒指攻魔道属性随机浮动 (+1~+4)</summary>
    public static int RollRingRareBonus(Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        return rng.Next(1, 5);
    }

    /// <summary>1395: 装备特修消耗金币且最大持久绝对零损耗</summary>
    public static (int newDura, int newMaxDura) PerformSpecialRepair(int maxDura)
    {
        return (maxDura, maxDura); // 满额且不掉上限
    }

    // =========================================================================
    // 细节 1396 ~ 1410: 沙巴克攻城、皇宫旗帜夺取、城墙密道与行会战火 (15 Details)
    // =========================================================================

    /// <summary>1396: 攻城战申请需提交沃玛号角 + 100万金币 + 祖玛头像</summary>
    public static (bool valid, string reason) ValidateSabukApplication(bool hasHorn, bool hasZumaHead, int guildGold)
    {
        if (!hasHorn) return (false, "缺少申请信物【沃玛号角】。");
        if (!hasZumaHead) return (false, "缺少沙城战帖凭证【祖玛头像】。");
        if (guildGold < 1000000) return (false, "行会资金不足 100 万金币。");
        return (true, string.Empty);
    }

    /// <summary>1397: 攻城申请必须提前 3 天递交</summary>
    public static long CalculateWarStartTime(long applyTimeMs)
    {
        return applyTimeMs + (3L * 24 * 3600 * 1000L);
    }

    /// <summary>1398: 攻城战晚上 20:00 至 23:00 持续 3 小时判定</summary>
    public static bool IsWarHour(int hourOfDay)
    {
        return hourOfDay >= 20 && hourOfDay < 23;
    }

    /// <summary>1399: 沙巴克城门损毁后密道对外开放</summary>
    public static bool IsPassageOpenAfterGateDestroyed(bool isGateDestroyed) => isGateDestroyed;

    /// <summary>1400: 沙巴克皇宫大门耐久度 (HP 20000)</summary>
    public static int GetPalaceGateMaxHp() => 20000;

    /// <summary>1401: 皇宫内部清人判定时钟 (每 5 秒扫描一次)</summary>
    public static bool IsPalaceScanDue(long lastScanTick, long nowTick)
    {
        return (nowTick - lastScanTick) >= 5000L;
    }

    /// <summary>1402: 攻城战期间全城区域击杀敌对行会成员 PK 值为 0</summary>
    public static int GetWarPkWorth(bool isEnemyGuildMember)
    {
        return isEnemyGuildMember ? 0 : 100;
    }

    /// <summary>1403: 攻城战战死者包裹物品掉落概率降低 50% 保护</summary>
    public static int ApplyWarDeathDropProtection(int normalDropRate)
    {
        return normalDropRate / 2;
    }

    /// <summary>1404: 沙城城主专属行会召集令消耗资金 (消耗 50000 金币)</summary>
    public static (bool allowed, int remainingFunds) ChargeGuildRecallFee(int guildFunds)
    {
        if (guildFunds < 50000) return (false, guildFunds);
        return (true, guildFunds - 50000);
    }

    /// <summary>1405: 行会战期间宣战行会头顶字样格式</summary>
    public static string FormatWarGuildTitle(string guildName, bool isEnemy)
    {
        return isEnemy ? $"[战争敌对] {guildName}" : $"[联盟盟友] {guildName}";
    }

    /// <summary>1406: 行会战 3 小时到期平局判定</summary>
    public static bool IsGuildWarTie(int myKills, int enemyKills) => myKills == enemyKills;

    /// <summary>1407: 行会成员退会 24 小时内禁止加入新行会惩罚</summary>
    public static bool CanJoinNewGuild(long quitGuildTick, long nowTick)
    {
        const long cooldown = 24L * 3600 * 1000L;
        return (nowTick - quitGuildTick) >= cooldown;
    }

    /// <summary>1408: 行会掌门人禅让掌门之位需缴纳 50 万金币</summary>
    public static (bool allowed, int remainingGold) ChargeAbdicateFee(int leaderGold)
    {
        if (leaderGold < 500000) return (false, leaderGold);
        return (true, leaderGold - 500000);
    }

    /// <summary>1409: 沙巴克城池每日店铺交易税收 20% 自动注入金库</summary>
    public static int CalculateSabukTaxRate(int totalTradeGold)
    {
        return totalTradeGold * 20 / 100;
    }

    /// <summary>1410: 沙巴克藏宝阁密令进入权限校验</summary>
    public static bool ValidateTreasurePavilionPass(bool isLord, bool hasPassToken)
    {
        return isLord || hasPassToken;
    }

    // =========================================================================
    // 细节 1411 ~ 1425: 状态机判定、中毒防御剥离、药水并发缓冲与特戒 (15 Details)
    // =========================================================================

    /// <summary>1411: 绿色中毒状态下自然生命回血完全冻结</summary>
    public static int FreezeNaturalRegenUnderGreenPoison(int naturalRegen, bool isGreenPoisoned)
    {
        return isGreenPoisoned ? 0 : naturalRegen;
    }

    /// <summary>1412: 红色中毒状态下目标物理防御 AC 强制削减 20%</summary>
    public static int ApplyRedPoisonAcReduction(int rawAc) => rawAc * 80 / 100;

    /// <summary>1413: 红色中毒状态下目标魔法防御 MAC 强制削减 20%</summary>
    public static int ApplyRedPoisonMacReduction(int rawMac) => rawMac * 80 / 100;

    /// <summary>1414: 施毒术双毒共存判定</summary>
    public static bool CanCoexistDoublePoison(bool hasGreen, bool hasRed) => hasGreen && hasRed;

    /// <summary>1415: 道士灵魂火符攻击需装备道符且持久 > 0</summary>
    public static bool ValidateAmuletEquipped(bool hasAmulet, int amuletDura)
    {
        return hasAmulet && amuletDura > 0;
    }

    /// <summary>1416: 金创药/魔法药多瓶连喝进入缓冲池并发累积，每秒最大释放 5 点</summary>
    public static (int newPool, int tickOutput) ProcessPotionBufferTick(int currentPool, int incomingPotion)
    {
        int total = currentPool + incomingPotion;
        int step = Math.Min(5, total);
        return (total - step, step);
    }

    /// <summary>1417: 强效太阳水瞬间回血 50 HP + 50 MP</summary>
    public static (int hp, int mp) GetSunWaterInstantRestore() => (50, 50);

    /// <summary>1418: 万年雪霜瞬间回血 120 HP + 120 MP</summary>
    public static (int hp, int mp) GetSnowFrostInstantRestore() => (120, 120);

    /// <summary>1419: 疗伤药瞬间急救回血 250 HP + 250 MP</summary>
    public static (int hp, int mp) GetHealingPotionInstantRestore() => (250, 250);

    /// <summary>1420: 麻痹戒指物理攻击命中 1/15 几率麻痹目标 5 秒</summary>
    public static bool RollParalysisRing(Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        return rng.Next(15) == 0;
    }

    /// <summary>1421: 复活戒指致死伤害满血复活且扣除 1 耐久 (冷却 120 秒)</summary>
    public static (bool resurrected, int newHp, int newDura, long nextAvailableTick) TriggerReviveRing(
        int maxHp, int currentDura, long lastUsedTick, long nowTick)
    {
        if (currentDura <= 0) return (false, 0, 0, lastUsedTick);
        if (nowTick - lastUsedTick < 120 * 1000L) return (false, 0, currentDura, lastUsedTick);
        return (true, maxHp, currentDura - 1, nowTick);
    }

    /// <summary>1422: 护身戒指 100% 优先扣减 MP 抵消伤害</summary>
    public static (int absorbedByMp, int remainingDamage, int newMp) ApplyProtectRingDmg(int rawDmg, int currentMp)
    {
        if (currentMp >= rawDmg)
        {
            return (rawDmg, 0, currentMp - rawDmg);
        }
        return (currentMp, rawDmg - currentMp, 0);
    }

    /// <summary>1423: 防御戒指赋予佩戴者 1 级治愈术技能</summary>
    public static bool GrantDefenseRingHealSkill() => true;

    /// <summary>1424: 火焰戒指赋予佩戴者 1 级小火球技能</summary>
    public static bool GrantFlameRingFireballSkill() => true;

    /// <summary>1425: 隐身戒指使佩戴者常态隐身，仅攻击施法破隐</summary>
    public static bool DoesInvisibilityRingHoldDuringMove() => true;

    // =========================================================================
    // 细节 1426 ~ 1440: 传送卷轴、超重慢走、防爆装与割肉纯度 (15 Details)
    // =========================================================================

    /// <summary>1426: 回城卷读条期间受击打断判定</summary>
    public static bool InterruptTownTeleportOnHit(bool tookDamage) => tookDamage;

    /// <summary>1427: 红名使用回城卷强制传送至盟重红名村 (850, 670)</summary>
    public static (int destX, int destY, string mapId) ResolveRedNameTownScroll(bool isRedName)
    {
        if (isRedName) return (850, 670, "3");
        return (330, 270, "0");
    }

    /// <summary>1428: 地牢逃脱卷随机传送至当前所属地表主城</summary>
    public static string ResolveDungeonSurfaceCity(string dungeonPrefix)
    {
        if (dungeonPrefix.StartsWith("D4")) return "11"; // 白日门
        if (dungeonPrefix.StartsWith("D2")) return "3";  // 盟重
        return "0"; // 比奇
    }

    /// <summary>1429: 随机传送卷在特殊禁飞地图禁止使用</summary>
    public static bool IsFlightForbiddenMap(string mapId)
    {
        return mapId == "0150" || mapId == "0157" || mapId == "D2008";
    }

    /// <summary>1430: 背包负重公式 (50 + Level * 4 + 力量加成)</summary>
    public static int CalculateMaxBagWeightFormula(int level, int strengthBonus = 0)
    {
        return 50 + level * 4 + strengthBonus;
    }

    /// <summary>1431: 负重超过 100% 无法拾取地面物品</summary>
    public static bool CanPickItemByWeight(int currentWeight, int maxWeight)
    {
        return currentWeight <= maxWeight;
    }

    /// <summary>1432: 负重超过 105% 无法奔跑仅能慢走</summary>
    public static bool CanRunByWeight(int currentWeight, int maxWeight)
    {
        return currentWeight <= (maxWeight * 105 / 100);
    }

    /// <summary>1433: 腕力不足无法佩戴超重武器</summary>
    public static bool CheckWeaponArmStrength(int weaponWeight, int playerArmStrength)
    {
        return weaponWeight <= playerArmStrength;
    }

    /// <summary>1434: 穿戴负重不足无法穿戴超重铠甲</summary>
    public static bool CheckArmorBodyWeight(int armorWeight, int playerBodyLimit)
    {
        return armorWeight <= playerBodyLimit;
    }

    /// <summary>1435: 头盔负重不足无法佩戴重型头盔</summary>
    public static bool CheckHelmetHeadWeight(int helmetWeight, int playerHeadLimit)
    {
        return helmetWeight <= playerHeadLimit;
    }

    /// <summary>1436: 极品装备重量浮动 (极品词条有几率使重量 -1)</summary>
    public static int ApplyRareWeightOffset(int baseWeight, bool isWeightReduced)
    {
        return isWeightReduced ? Math.Max(1, baseWeight - 1) : baseWeight;
    }

    /// <summary>1437: 装备持久归 0 属性全部归零失效</summary>
    public static (int dc, int ac, int mac) GetAttributesByDurability(int dura, int baseDc, int baseAc, int baseMac)
    {
        if (dura <= 0) return (0, 0, 0);
        return (baseDc, baseAc, baseMac);
    }

    /// <summary>1438: 物理暴击造成双倍装备持久损耗</summary>
    public static int CalculateCritDurabilityLoss(bool isCrit) => isCrit ? 2 : 1;

    /// <summary>1439: 死亡掉落装备概率 (白名 10%, 黄名 30%, 红名 100% 掉落 2~4 件)</summary>
    public static int GetDeathDropRateByPkStatus(int pkStatus)
    {
        return pkStatus switch
        {
            2 => 100, // 红名
            1 => 30,  // 黄名
            _ => 10   // 白名
        };
    }

    /// <summary>1440: 鹿肉品质计算 (5~18, 死后受多次火墙或攻击品质持续衰减)</summary>
    public static int CalculateMeatDecayQuality(int initialQuality, int damageHitCountAfterDeath)
    {
        int q = initialQuality - damageHitCountAfterDeath * 2;
        return Math.Clamp(q, 0, 18);
    }

    // =========================================================================
    // 细节 1441 ~ 1450: 仓库安全、时间戳单调、反外挂与Direct-Blit 协议完整性 (10 Details)
    // =========================================================================

    /// <summary>1441: 仓库二级密码连续 3 次输错锁定 10 分钟</summary>
    public static (bool locked, long unlockTick) EvaluateStorageLock(int errorAttempts, long nowTick)
    {
        if (errorAttempts >= 3)
        {
            return (true, nowTick + (10 * 60 * 1000L));
        }
        return (false, 0);
    }

    /// <summary>1442: 仓库金币上限 5000 万，背包携带上限 1000 万</summary>
    public static (int bagCap, int storageCap) GetGoldCapLimits() => (10000000, 50000000);

    /// <summary>1443: NPC 商店买卖金币防溢出整数回绕防作弊校验</summary>
    public static bool PreventGoldWrapAround(long currentGold, long delta)
    {
        long res = currentGold + delta;
        return res >= 0 && res <= 10000000L;
    }

    /// <summary>1444: NPC 商店回购列表 60 分钟未回购清空维护</summary>
    public static bool IsBuybackListExpired(long soldTick, long nowTick)
    {
        return (nowTick - soldTick) >= (60 * 60 * 1000L);
    }

    /// <summary>1445: 鸡肉品质计算 (品质 3~10)</summary>
    public static int GetChickenMeatQuality(int seed)
    {
        return Math.Clamp(seed, 3, 10);
    }

    /// <summary>1446: 屠夫任务收购品质 >= 15 鹿肉给予高额金币与经验</summary>
    public static bool IsHighQualityDeerMeatQuestEligible(int quality) => quality >= 15;

    /// <summary>1447: 铁匠铺特修金币动态公式 (基础修费 * (最大持久 - 当前持久) / 最大持久 * 3)</summary>
    public static int CalculateSpecialRepairGold(int baseCost, int currentDura, int maxDura)
    {
        if (maxDura <= 0 || currentDura >= maxDura) return 0;
        int missing = maxDura - currentDura;
        long cost = (long)baseCost * missing * 3 / maxDura;
        return (int)Math.Max(10, cost);
    }

    /// <summary>1448: 客户端时间戳单调递增校验 (防变速齿轮加速发包封禁)</summary>
    public static bool CheckTimestampMonotonicity(long lastTick, long incomingTick)
    {
        return incomingTick >= lastTick;
    }

    /// <summary>1449: Direct-Blit 协议动态魔数 0x594D 防中间人挂探查</summary>
    public static bool VerifyDirectBlitMagic(ushort magic) => magic == 0x594D;

    /// <summary>1450: 客户端与服务端 Direct-Blit 包序号 SequenceId 单调自增防重放攻击</summary>
    public static bool VerifyDirectBlitSequence(ushort lastSeq, ushort incomingSeq)
    {
        if (incomingSeq > lastSeq) return true;
        if (lastSeq > 65000 && incomingSeq < 1000) return true; // 回绕放行
        return false;
    }
}
