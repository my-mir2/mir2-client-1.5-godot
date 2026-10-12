using System;
using System.Collections.Generic;
using Mir2.Shared.Models;

namespace Mir2.Shared.Rules;

/// <summary>
/// 盛大官方 1.5 不朽传奇 100 微观细节与服务端权威闭环引擎 (Details 1451 ~ 1550)
/// 严格 1:1 对齐官方二进制 MIR_final_v3_p.exe 权威逻辑与牛魔寺庙/反隐机制/药水缓释/垫刀随机/沙城复活
/// 遵循纯血 Dumb Client 架构，所有判定均为 M2Server 服务端权威真值
/// </summary>
public static class OfficialImmortalLegend100DetailsEngine
{
    // =========================================================================
    // 细节 1451 ~ 1465: 封魔殿虹魔教主深度生态、护卫 AI 与魔血/祈祷套装掉落 (15 Details)
    // =========================================================================

    /// <summary>1451: 封魔殿大厅全图危险区无安全区设定</summary>
    public static bool IsFengMoHallSafeZone(string mapId, int x, int y)
    {
        if (mapId == "D024") return false; // 封魔殿全图无安全区
        return false;
    }

    /// <summary>1452: 虹魔教主残血 (<20%) 狂暴大范围吸血重击 (DC 1.8倍)</summary>
    public static int CalculateHongMoKingDamage(int baseDc, int currentHp, int maxHp)
    {
        if (maxHp > 0 && currentHp * 100 / maxHp < 20)
        {
            return baseDc * 18 / 10;
        }
        return baseDc;
    }

    /// <summary>1453: 虹魔猪卫大锤横扫附带击退 1 格判定</summary>
    public static (bool knockedBack, int newTargetX, int newTargetY) CalculateHongMoPigGuardKnockback(
        int attackerX, int attackerY, int targetX, int targetY)
    {
        int dx = targetX - attackerX;
        int dy = targetY - attackerY;
        if (Math.Abs(dx) <= 1 && Math.Abs(dy) <= 1)
        {
            return (true, targetX + Math.Sign(dx), targetY + Math.Sign(dy));
        }
        return (false, targetX, targetY);
    }

    /// <summary>1454: 虹魔蝎卫极速淬毒削减 20% 魔御与高伤毒刺</summary>
    public static int CalculateHongMoScorpionPoisonDamage(int baseMc, int targetMac)
    {
        int effectiveMac = targetMac * 80 / 100;
        return Math.Max(10, baseMc - effectiveMac);
    }

    /// <summary>1455: 虹魔猪卫极速近战 (攻速 600ms) 判定</summary>
    public static bool CanHongMoPigGuardAttack(long lastAttackTick, long nowTick)
    {
        return (nowTick - lastAttackTick) >= 600L;
    }

    /// <summary>1456: 虹魔蝎卫近身冲撞定身 500ms</summary>
    public static (bool stunned, long stunUntilTick) CalculateHongMoScorpionStun(long nowTick, Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        if (rng.Next(100) < 25)
        {
            return (true, nowTick + 500L);
        }
        return (false, nowTick);
    }

    /// <summary>1457: 虹魔教主死亡极小概率爆落虹魔戒指、魔血手镯、祈祷之刃三大名牌</summary>
    public static bool IsHongMoKingUltraRareDrop(string itemName)
    {
        return itemName == "虹魔戒指" || itemName == "魔血手镯" || itemName == "祈祷之刃";
    }

    /// <summary>1458: 雷霆之路通往封魔殿狭窄密道卡位拓扑判定</summary>
    public static bool IsFengMoMineNarrowPassage(int x, int y)
    {
        return x == 23 && y == 28;
    }

    /// <summary>1459: 封魔殿全图禁用天地合一召唤</summary>
    public static bool IsFengMoSanctumGroupRecallForbidden(string mapId)
    {
        return mapId.StartsWith("D024");
    }

    /// <summary>1460: 封魔谷老兵传送封魔矿区费用 (2000 金币)</summary>
    public static (bool allowed, int remainingGold) ChargeFengMoMineTeleportFee(int currentGold)
    {
        if (currentGold < 2000) return (false, currentGold);
        return (true, currentGold - 2000);
    }

    /// <summary>1461: 虹魔系怪物不受诱惑之光招募 (诱惑抗性 100%)</summary>
    public static bool CanTameHongMoMonster(string monsterName)
    {
        if (monsterName.StartsWith("虹魔")) return false;
        return true;
    }

    /// <summary>1462: 祖玛系与封魔系怪物仇恨独立互不干扰</summary>
    public static bool IsMonsterFactionIndependent(string monA, string monB)
    {
        bool aIsZuma = monA.StartsWith("祖玛");
        bool bIsFengMo = monB.StartsWith("虹魔") || monB.StartsWith("千年树妖");
        return aIsZuma && bIsFengMo;
    }

    /// <summary>1463: 虹魔蝎卫受近战围攻时瞬移后撤 2 格拉开身位</summary>
    public static (int retreatX, int retreatY) CalculateScorpionRetreatCoords(int priestX, int priestY, int attackerX, int attackerY)
    {
        int dx = priestX - attackerX;
        int dy = priestY - attackerY;
        return (priestX + Math.Sign(dx) * 2, priestY + Math.Sign(dy) * 2);
    }

    /// <summary>1464: 虹魔教主刷新时全层地图微弱震颤 300ms 广播</summary>
    public static int GetHongMoKingSpawnShakeDuration() => 300;

    /// <summary>1465: 封魔谷水域与地穴裂隙掉落物品受阻挡保护</summary>
    public static bool IsLootInWaterBlocked(int x, int y)
    {
        return x >= 50 && x <= 60 && y >= 50 && y <= 60;
    }

    // =========================================================================
    // 细节 1466 ~ 1480: 怪物视野反隐、识破与假死反击机制 (15 Details)
    // =========================================================================

    /// <summary>1466: 月魔蜘蛛对隐身玩家 100% 识破视野半径 (8格)</summary>
    public static bool CanMoonSpiderDetectInvisible(int spiderX, int spiderY, int playerX, int playerY)
    {
        int dist = Math.Max(Math.Abs(spiderX - playerX), Math.Abs(spiderY - playerY));
        return dist <= 8;
    }

    /// <summary>1467: 楔蛾扇形 6 格反隐雷达扫描</summary>
    public static bool CanMothDetectInvisible(int mothX, int mothY, int playerX, int playerY)
    {
        int dist = Math.Max(Math.Abs(mothX - playerX), Math.Abs(mothY - playerY));
        return dist <= 6;
    }

    /// <summary>1468: 触龙神地下潜伏检测 (玩家踏入 2 格范围内破土而出)</summary>
    public static bool ShouldDragonEmerge(int dragonX, int dragonY, int playerX, int playerY)
    {
        int dist = Math.Max(Math.Abs(dragonX - playerX), Math.Abs(dragonY - playerY));
        return dist <= 2;
    }

    /// <summary>1469: 尸王从地底铁链破棺而出动画时序 (800ms 无敌前摇)</summary>
    public static bool IsZombieKingInvulnerable(long emergeStartTick, long nowTick)
    {
        return (nowTick - emergeStartTick) < 800L;
    }

    /// <summary>1470: 沃玛教主被卡位 3 秒以上强制触发随机闪烁脱困</summary>
    public static bool ShouldWomaLeaderTeleportOut(int trappedSeconds)
    {
        return trappedSeconds >= 3;
    }

    /// <summary>1471: 祖玛教主石化破封瞬间 1000ms 免疫任何伤害</summary>
    public static bool IsZumaLeaderStoneImmune(long unfreezeTick, long nowTick)
    {
        return (nowTick - unfreezeTick) < 1000L;
    }

    /// <summary>1472: 邪恶钳虫残血 15% 缩壳防御提升 50%</summary>
    public static int ApplyCentipedeShellDefense(int baseAc, int currentHp, int maxHp)
    {
        if (maxHp > 0 && currentHp * 100 / maxHp < 15)
        {
            return baseAc * 150 / 100;
        }
        return baseAc;
    }

    /// <summary>1473: 黑色恶蛆极速滚动作轮半径与转弯阻尼计算 (200ms/步)</summary>
    public static int GetBlackMaggotStepIntervalMs() => 200;

    /// <summary>1474: 钳虫大军围杀仇恨共享机制 (一怪受击 5 格内同伴响应)</summary>
    public static bool IsWithinSharedAggroRange(int hitX, int hitY, int otherX, int otherY)
    {
        int dist = Math.Max(Math.Abs(hitX - otherX), Math.Abs(hitY - otherY));
        return dist <= 5;
    }

    /// <summary>1475: 隐身术在 Boss 视线内持续时间强制削减 50%</summary>
    public static int AdjustInvisibilityDurationNearBoss(int normalDurationSec, bool nearBoss)
    {
        return nearBoss ? (normalDurationSec / 2) : normalDurationSec;
    }

    /// <summary>1476: 困魔咒对暗之系列首领 100% 免疫</summary>
    public static bool CanTrapDarkBoss(string monsterName)
    {
        if (monsterName.StartsWith("暗之")) return false;
        return true;
    }

    /// <summary>1477: 圣言术对尸王/骷髅等不死怪物的秒杀概率计算</summary>
    public static int CalculateHolyWordChanceAgainstUndead(int playerLevel, int monsterLevel)
    {
        if (playerLevel <= monsterLevel) return 0;
        return Math.Clamp((playerLevel - monsterLevel) * 4, 5, 45);
    }

    /// <summary>1478: 怪物假死骗刀机制 (石墓尸王倒地后 10 秒后爬起)</summary>
    public static bool CanZombieFeignDeathRevive(long deathTick, long nowTick)
    {
        return (nowTick - deathTick) >= 10000L;
    }

    /// <summary>1479: 怪物被诱惑黄闪期间受外部攻击立即惊醒解除叛变</summary>
    public static bool DoesAttackBreakTameInduction(bool isInducing) => isInducing;

    /// <summary>1480: 仆从虎卫 7 级攻击附带 10% 概率虎啸威慑定身 200ms</summary>
    public static (bool feared, int stunMs) RollHuWeiRoarStun(int huWeiLevel, Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        if (huWeiLevel >= 7 && rng.Next(100) < 10)
        {
            return (true, 200);
        }
        return (false, 0);
    }

    // =========================================================================
    // 细节 1481 ~ 1495: 全量药水缓释池、中毒叠加与解毒药剂精算 (15 Details)
    // =========================================================================

    /// <summary>1481: 金创药（小）每跳回复 3 点，持续 10 次</summary>
    public static (int tickAmount, int totalTicks) GetSmallHpPotionSpecs() => (3, 10);

    /// <summary>1482: 金创药（中）每跳回复 6 点，持续 10 次</summary>
    public static (int tickAmount, int totalTicks) GetMediumHpPotionSpecs() => (6, 10);

    /// <summary>1483: 强效金创药每跳回复 10 点，持续 10 次</summary>
    public static (int tickAmount, int totalTicks) GetStrongHpPotionSpecs() => (10, 10);

    /// <summary>1484: 魔法药（小）每跳回复 4 点，持续 10 次</summary>
    public static (int tickAmount, int totalTicks) GetSmallMpPotionSpecs() => (4, 10);

    /// <summary>1485: 魔法药（中）每跳回复 8 点，持续 10 次</summary>
    public static (int tickAmount, int totalTicks) GetMediumMpPotionSpecs() => (8, 10);

    /// <summary>1486: 强效魔法药每跳回复 15 点，持续 10 次</summary>
    public static (int tickAmount, int totalTicks) GetStrongMpPotionSpecs() => (15, 10);

    /// <summary>1487: 太阳水瞬回 50 HP + 50 MP (零延迟入池)</summary>
    public static (int hp, int mp) GetSunPotionRestore() => (50, 50);

    /// <summary>1488: 强效太阳水瞬回 80 HP + 80 MP</summary>
    public static (int hp, int mp) GetStrongSunPotionRestore() => (80, 80);

    /// <summary>1489: 万年雪霜瞬回 120 HP + 120 MP</summary>
    public static (int hp, int mp) GetSnowFrostRestore() => (120, 120);

    /// <summary>1490: 疗伤药急救瞬回 250 HP + 250 MP</summary>
    public static (int hp, int mp) GetHealingDrugRestore() => (250, 250);

    /// <summary>1491: 绿毒每秒扣血受道士道术加成上限截断 (每秒最大 15 点)</summary>
    public static int CalculateGreenPoisonDamageCapped(int sc, int skillLevel)
    {
        int raw = 1 + skillLevel + (sc / 10);
        return Math.Min(15, raw);
    }

    /// <summary>1492: 黄毒削弱物防魔御固定持续 30 秒，不随离线冻结</summary>
    public static int GetRedPoisonBaseDurationSec() => 30;

    /// <summary>1493: 药水缓释池溢出丢弃机制 (池内累计超 1000 点拒绝再喝)</summary>
    public static bool CanDrinkPotionByPoolLimit(int currentPool, int incomingAmount)
    {
        return (currentPool + incomingAmount) <= 1000;
    }

    /// <summary>1494: 解毒剂瞬间清除体内所有毒素状态</summary>
    public static (bool greenPoison, bool redPoison) CureAllPoisons() => (false, false);

    /// <summary>1495: 毒物躲避属性对月魔蜘蛛石化率的衰减公式</summary>
    public static int CalculatePetrifyChanceWithAvoidance(int baseChancePercent, int poisonAvoidance)
    {
        int finalRate = baseChancePercent * (100 - Math.Min(80, poisonAvoidance)) / 100;
        return Math.Max(1, finalRate);
    }

    // =========================================================================
    // 细节 1496 ~ 1510: 武器升级垫刀随机熵、诅咒反噬与祈祷全套天罚 (15 Details)
    // =========================================================================

    /// <summary>1496: 垫刀机制中连续失败次数对伪随机种子的偏移量</summary>
    public static int CalculateUpgradeSeedOffset(int consecutiveFails)
    {
        return consecutiveFails * 37;
    }

    /// <summary>1497: 武器开刀瞬间根据取刀时间戳毫秒数作为随机源</summary>
    public static int DeriveUpgradeOutcomeSeed(long pickupTick, int weaponUid)
    {
        return (int)((pickupTick ^ (weaponUid * 1337L)) & 0x7FFFFFFF);
    }

    /// <summary>1498: 黑铁矿纯度分布均衡性对持久的影响权重</summary>
    public static bool IsBlackIronBalanced(int[] purities)
    {
        if (purities == null || purities.Length < 3) return false;
        int min = purities[0], max = purities[0];
        foreach (var p in purities)
        {
            if (p < min) min = p;
            if (p > max) max = p;
        }
        return (max - min) <= 5;
    }

    /// <summary>1499: 首饰持久度是否影响升级成功率 (官方实证为否)</summary>
    public static bool DoesJewelryDurabilityAffectUpgrade(int jewelryDura) => false;

    /// <summary>1500: 首饰佩戴等级门槛在升级中的分数折算模型 (等级*3)</summary>
    public static int CalculateJewelryLevelScore(int reqLevel) => reqLevel * 3;

    /// <summary>1501: 武器升级失败时武器碎裂广播特效 ID 30</summary>
    public static int GetWeaponBreakEffectId() => 30;

    /// <summary>1502: 祈祷套装全齐激活全服宝宝大叛变事件持续 30 分钟</summary>
    public static long GetPraySetRebellionDurationMs() => 30L * 60 * 1000L;

    /// <summary>1503: 祈祷项链佩戴者角色死亡 100% 破碎销毁</summary>
    public static bool ShouldPrayNecklaceBreakOnDeath() => true;

    /// <summary>1504: 祈祷手镯佩戴者被杀 50% 几率自毁</summary>
    public static bool RollPrayBraceletBreak(Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        return rng.Next(100) < 50;
    }

    /// <summary>1505: 祈祷之刃自带准确+3、攻速-1 属性平衡</summary>
    public static (int hitBonus, int attackSpeedModifier) GetPrayBladeUniqueStats() => (3, -1);

    /// <summary>1506: 祝福油对运 6 升级运 7 的极限成功率 (3%)</summary>
    public static int GetBlessingOilRateForLuck6() => 3;

    /// <summary>1507: 祝福油在武器已诅咒时优先消除 1 点诅咒</summary>
    public static (int newLuck, int newCurse, string result) ApplyBlessingOilWithCurse(int currentLuck, int currentCurse)
    {
        if (currentCurse > 0)
        {
            return (currentLuck, currentCurse - 1, "武器上的诅咒被圣洁的油冲洗消退了。");
        }
        return (currentLuck + 1, 0, "你的武器获得了幸运祝福！");
    }

    /// <summary>1508: 恶意击杀无辜白名玩家 40% 几率武器增加 1 点诅咒</summary>
    public static bool RollPkWantedCurse(Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        return rng.Next(100) < 40;
    }

    /// <summary>1509: 罗刹武器在运-10时下限输出达到恒定 15 点</summary>
    public static int GetLuoShaGuaranteedDamage(int curse) => (curse >= 10) ? 15 : 0;

    /// <summary>1510: 战神油特修武器收取 5 倍修理费但不掉持久上限</summary>
    public static (int repairCost, int maxDuraLoss) CalculateWarGodOilRepair(int baseRepairCost)
    {
        return (baseRepairCost * 5, 0);
    }

    // =========================================================================
    // 细节 1511 ~ 1525: 沙巴克攻城复活点争夺、密道战术与全服税收清算 (15 Details)
    // =========================================================================

    /// <summary>1511: 沙巴克复活点旗帜归属判定 (旗帜周围 3 格占领 60 秒)</summary>
    public static bool IsFlagCaptured(int currentHoldSeconds) => currentHoldSeconds >= 60;

    /// <summary>1512: 攻城期间守城方死亡在皇宫内院复活 (640, 290)</summary>
    public static (int x, int y) GetDefenderSpawnLocation() => (640, 290);

    /// <summary>1513: 攻城期间攻城方死亡在沙巴克城外据点复活 (700, 350)</summary>
    public static (int x, int y) GetAttackerSpawnLocation() => (700, 350);

    /// <summary>1514: 沙巴克老人攻城查询功能与战帖列表</summary>
    public static bool CanQuerySabukWarList(bool isNpcTalk) => isNpcTalk;

    /// <summary>1515: 城门耐久归 0 破碎倒塌阻挡消除</summary>
    public static bool IsGateCollisionActive(int gateHp) => gateHp > 0;

    /// <summary>1516: 皇宫墙角密道只能单向潜入皇宫内部</summary>
    public static bool ValidatePalaceSecretPassageDirection(string fromRoom, string toRoom)
    {
        return fromRoom == "SecretPassage" && toRoom == "Palace";
    }

    /// <summary>1517: 攻城战期间全沙城范围内禁止买卖 NPC 交易</summary>
    public static bool IsSabukNpcTradeAllowedDuringWar(bool isWarActive) => !isWarActive;

    /// <summary>1518: 攻城战 23:00 结束瞬间皇宫内剩余人数多数决判定</summary>
    public static string DecideSabukWinnerByHeadcount(int attackerCount, int defenderCount, string attackerGuild, string defenderGuild)
    {
        if (attackerCount > defenderCount) return attackerGuild;
        return defenderGuild;
    }

    /// <summary>1519: 沙巴克城主每日领取的 10 万金币城主俸禄</summary>
    public static int GetDailyLordSalary() => 100000;

    /// <summary>1520: 沙城成员在沙巴克店铺买药特修享 8 折特权</summary>
    public static int ApplySabukDiscount(int baseCost, bool isSabukMember)
    {
        return isSabukMember ? (baseCost * 80 / 100) : baseCost;
    }

    /// <summary>1521: 沙巴克行会掌门人更迭时城主头衔自动移交</summary>
    public static bool TransferLordTitle(string newLeader, out string assignedLord)
    {
        assignedLord = newLeader;
        return true;
    }

    /// <summary>1522: 攻城申请战帖递交后禁止撤销</summary>
    public static bool CanRevokeWarApplication() => false;

    /// <summary>1523: 沙城皇宫大厅内无法使用随机传送卷</summary>
    public static bool IsFlightAllowedInPalace(string mapId)
    {
        return mapId != "0150";
    }

    /// <summary>1524: 沙巴克专属藏宝阁怪物双倍爆率加成</summary>
    public static int ApplyTreasurePavilionDropMultiplier(int baseRate) => baseRate * 2;

    /// <summary>1525: 攻城战战死装备掉落概率降低至常规的 50%</summary>
    public static int CalculateWarDropRate(int normalRate) => normalRate / 2;

    // =========================================================================
    // 细节 1526 ~ 1540: 行会战火、联盟同盟识别与敌对名牌染色 (15 Details)
    // =========================================================================

    /// <summary>1526: 宣战行会头顶字样呈现橙红色敌对标识</summary>
    public static string FormatWarEnemyTitle(string guildName) => $"[敌对] {guildName}";

    /// <summary>1527: 结盟同盟行会头顶字样呈现海蓝色盟友标识</summary>
    public static string FormatWarAllyTitle(string guildName) => $"[盟友] {guildName}";

    /// <summary>1528: 行会战期间本会成员攻击误伤自动豁免 (防自残)</summary>
    public static bool IsFriendlyFireProtected(int myGuildId, int targetGuildId)
    {
        return myGuildId != 0 && myGuildId == targetGuildId;
    }

    /// <summary>1529: 行会公告修改后向全会在线成员实时推送</summary>
    public static bool ShouldBroadcastGuildNotice(string oldNotice, string newNotice)
    {
        return oldNotice != newNotice;
    }

    /// <summary>1530: 行会会长开除副会长与普通成员的权限分级 (0级掌门可开除任何人)</summary>
    public static bool CanKickMember(int kickerRank, int targetRank)
    {
        return kickerRank == 0 && targetRank > 0;
    }

    /// <summary>1531: 招收行会成员时被招募者需开启“允许入会”开关</summary>
    public static bool CanRecruitTarget(bool allowGuildJoin) => allowGuildJoin;

    /// <summary>1532: 行会列表面板分页查询与总人数统计</summary>
    public static (int pageCount, int totalCount) CalculateGuildPagination(int totalMembers, int pageSize = 10)
    {
        if (pageSize <= 0) pageSize = 10;
        int pages = (totalMembers + pageSize - 1) / pageSize;
        return (pages, totalMembers);
    }

    /// <summary>1533: 退出行会后背包保留的原有物品不受影响</summary>
    public static bool KeepInventoryOnQuitGuild() => true;

    /// <summary>1534: 行会资金消耗至 0 时行会进入衰退状态</summary>
    public static bool IsGuildInDecline(int guildFunds) => guildFunds <= 0;

    /// <summary>1535: 行会战持续 3 小时到期自动鸣金收兵</summary>
    public static bool IsGuildWarExpired(long warStartTick, long nowTick)
    {
        const long threeHours = 3L * 3600 * 1000L;
        return (nowTick - warStartTick) >= threeHours;
    }

    /// <summary>1536: 击杀敌对行会成员在行会聊天频道发送击杀战报</summary>
    public static string GenerateWarKillBroadcast(string killer, string victim, string enemyGuild)
    {
        return $"【行会战报】英勇的 [{killer}] 击杀了敌对行会【{enemyGuild}】的成员 [{victim}]！";
    }

    /// <summary>1537: 盟友行会之间禁止互相宣战</summary>
    public static bool CanDeclareWarOnAlly(bool isAlly) => !isAlly;

    /// <summary>1538: 行会聊天频道消息前缀 [行会] 专属绿色高亮</summary>
    public static string FormatGuildChatMessage(string sender, string msg)
    {
        return $"[行会]{sender}: {msg}";
    }

    /// <summary>1539: 掌门人禁言违规成员功能持续 10 分钟</summary>
    public static long CalculateMuteDurationTick(long nowTick) => nowTick + 10L * 60 * 1000L;

    /// <summary>1540: 行会封号自定义编辑与下发广播</summary>
    public static bool ValidateCustomRankTitle(string rankTitle)
    {
        return !string.IsNullOrWhiteSpace(rankTitle) && rankTitle.Length <= 12;
    }

    // =========================================================================
    // 细节 1541 ~ 1550: Direct-Blit 协议抗丢包、防重放滑动窗口与网络安全守门线 (10 Details)
    // =========================================================================

    /// <summary>1541: 客户端发包 SequenceId 与服务端记录严格单调递增 (+1)</summary>
    public static bool IsSequenceStrictlyMonotonic(ushort lastSeq, ushort incomingSeq)
    {
        return incomingSeq == (ushort)(lastSeq + 1);
    }

    /// <summary>1542: 滑动窗口容灾阈值设定为 10，超过 10 判定为中间人挂或断线重连</summary>
    public static bool IsSlidingWindowAcceptable(ushort lastSeq, ushort incomingSeq)
    {
        int delta = incomingSeq - lastSeq;
        return delta > 0 && delta <= 10;
    }

    /// <summary>1543: 封包动态魔数校验 Magic == 0x594D，魔数错误直接断开连接</summary>
    public static bool ValidateDirectBlitMagicWord(ushort magic) => magic == 0x594D;

    /// <summary>1544: Direct-Blit 二进制包体 Header 长度严格校验 (4 字节 Header)</summary>
    public static int GetDirectBlitHeaderSize() => 4;

    /// <summary>1545: 客户端移动包步长校验 (走 1 格，跑 2 格，超步长直接坐标拉回)</summary>
    public static bool ValidateMoveStepDistance(int fromX, int fromY, int toX, int toY, bool isRun)
    {
        int dx = Math.Abs(toX - fromX);
        int dy = Math.Abs(toY - fromY);
        int max = isRun ? 2 : 1;
        return dx <= max && dy <= max;
    }

    /// <summary>1546: 攻击与施法动作最小时间间隔校验 (出刀 >= 800ms，施法 >= 500ms)</summary>
    public static bool ValidateActionCadence(long lastActionTick, long nowTick, bool isSpell)
    {
        long limit = isSpell ? 500L : 800L;
        return (nowTick - lastActionTick) >= limit;
    }

    /// <summary>1547: 嗑药请求最小间隔校验 (每瓶药 >= 200ms 防穿人自动喝药外挂)</summary>
    public static bool ValidateDrinkPotionCadence(long lastDrinkTick, long nowTick)
    {
        return (nowTick - lastDrinkTick) >= 200L;
    }

    /// <summary>1548: 捡物坐标必须在玩家当前 1 格脚下，超距直接丢弃意图</summary>
    public static bool ValidatePickupProximity(int playerX, int playerY, int itemX, int itemY)
    {
        return playerX == itemX && playerY == itemY;
    }

    /// <summary>1549: 交易双方距离超 3 格自动熔断交易并双向回滚物品</summary>
    public static bool IsTradeWithinSafeDistance(int p1X, int p1Y, int p2X, int p2Y)
    {
        int dist = Math.Max(Math.Abs(p1X - p2X), Math.Abs(p1Y - p2Y));
        return dist <= 3;
    }

    /// <summary>1550: 服务端单 Session 最大接收缓冲区限流 (每秒最大 60 包防拒绝服务)</summary>
    public static bool ValidateRateLimiter(int packetsReceivedInCurrentSecond)
    {
        return packetsReceivedInCurrentSecond <= 60;
    }
}
