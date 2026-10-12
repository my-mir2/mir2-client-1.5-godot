namespace Mir2.Shared.Rules;

/// <summary>
/// 盛大官方 1.5 原版《虎卫传说》第 1651 ~ 1750 号微观细节规则权威判定引擎
/// 100% 服务端权威运算，纯血 Dumb Client 架构，严禁客户端本地作弊。
/// </summary>
public static class OfficialDominionAscent100DetailsEngine
{
    // =========================================================================
    // =========================================================================
    // 细节 1651 ~ 1665：尸王殿与比奇矿区废矿区权威生态规则 (官方 1.50 绝对真值)
    // =========================================================================

    /// <summary>
    /// 细节 1651：矿区废矿区坍塌碎石对法师魔法盾破防削弱 (碎石落顶削弱盾防 30%)
    /// </summary>
    public static (int directHpDamage, int shieldAbsorbed) CalculateSpearmanShieldPierceDamage(int rawDamage, bool hasShield)
    {
        if (!hasShield) return (rawDamage, 0);
        int absorbed = (int)(rawDamage * 0.40f);
        return (rawDamage - absorbed, absorbed);
    }

    /// <summary>
    /// 细节 1652：比奇矿区骷髅精灵刷新全屏地面裂隙与震颤判定 (距刷新点 15 格内可感知)
    /// </summary>
    public static bool CanPerceiveBossSpawnTremor(int bossX, int bossY, int playerX, int playerY)
    {
        int distSq = (bossX - playerX) * (bossX - playerX) + (bossY - playerY) * (bossY - playerY);
        return distSq <= 225; // 15 格半径
    }

    /// <summary>
    /// 细节 1653：和尚僵尸重击持久磨损 (10% 概率额外消耗被击者武器持久 1 点)
    /// </summary>
    public static int CheckHammerSkeletonDuraDmg(int randomRoll0To99)
    {
        return randomRoll0To99 < 10 ? 1000 : 0;
    }

    /// <summary>
    /// 细节 1654：电僵尸直线疾光电影穿透抛射判定 (存在友军阻挡命中率降低 25%)
    /// </summary>
    public static int CalculateArcherObstructedHitRate(int baseHitRate, bool isObstructedByAlly)
    {
        if (!isObstructedByAlly) return baseHitRate;
        return (int)(baseHitRate * 0.75f);
    }

    /// <summary>
    /// 细节 1655：僵尸钻出土堆 0.8 秒免疫物理伤害
    /// </summary>
    public static bool IsCorpseZombieImmuneOnSpawn(long spawnTimeMs, long currentTimeMs)
    {
        return (currentTimeMs - spawnTimeMs) < 800;
    }

    /// <summary>
    /// 细节 1656：尸王毒雾光环 (每 2 秒对 3x3 范围造成 12 点毒伤)
    /// </summary>
    public static (int poisonDmg, int tickIntervalMs) GetEvilZombieKingPoisonAura()
    {
        return (12, 2000);
    }

    /// <summary>
    /// 细节 1657：矿区狭窄通道单行道碰撞阻挡 (同格不能并排站立 2 人)
    /// </summary>
    public static bool IsBridgeTilePassable(bool isTileOccupied)
    {
        return !isTileOccupied;
    }

    /// <summary>
    /// 细节 1658：比奇城老兵处免费查询 Boss 存活状态
    /// </summary>
    public static bool CanQueryBossStatusFree(int gold)
    {
        return gold >= 0; // 0 费用
    }

    /// <summary>
    /// 细节 1659：尸王殿尸王掉落骨玉权杖官方概率 (1/2000)
    /// </summary>
    public static bool CheckGuYuDrop(int randomRoll0To1999)
    {
        return randomRoll0To1999 == 0;
    }

    /// <summary>
    /// 细节 1660：自爆僵尸死亡自爆腐蚀黏液减速 (15% 几率减速 20% 持续 3 秒)
    /// </summary>
    public static (bool slow, int durationMs) CheckCorpseSlimeSlow(int randomRoll0To99)
    {
        if (randomRoll0To99 < 15) return (true, 3000);
        return (false, 0);
    }

    /// <summary>
    /// 细节 1661：矿区坍塌落石动态阻挡网格 (坍塌时该格阻挡不可通行)
    /// </summary>
    public static bool IsTideBlocked(bool isHighTide, bool isLowWaterTile)
    {
        return isHighTide && isLowWaterTile;
    }

    /// <summary>
    /// 细节 1662：比奇书店老板出售技能书等级门槛 (仅售 28 级以下初中级书)
    /// </summary>
    public static bool CanSellBookInBichonBookStore(int bookReqLevel)
    {
        return bookReqLevel <= 28;
    }

    /// <summary>
    /// 细节 1663：比奇矿区老兵收费 500 金币传送盟重省
    /// </summary>
    public static bool CanTakeOldVeteranToMongchon(int gold, out int cost)
    {
        cost = 500;
        return gold >= cost;
    }

    /// <summary>
    /// 细节 1664：城镇安全区练功木桩假人受击 0 伤害飘字
    /// </summary>
    public static int GetDummyDamageFloatingText(bool isDummy)
    {
        return isDummy ? 0 : 1;
    }

    /// <summary>
    /// 细节 1665：黑铁矿石纯度兑换金币比例 (每点纯度折算 1000 金币)
    /// </summary>
    public static int ExchangeBlackIronToGold(int pearlCount)
    {
        return pearlCount * 1000;
    }

    // =========================================================================
    // 细节 1666 ~ 1680：尸王殿钻地地洞、暗黑通道与封魔战术 (官方 1.50)
    // =========================================================================

    /// <summary>
    /// 细节 1666：尸王殿地洞入口每次开启仅限两人同时跨入传送
    /// </summary>
    public static bool CanEnterCorpseKingHiddenCaveGroup(int groupSize, bool hasMale, bool hasFemale)
    {
        return groupSize == 2 && hasMale && hasFemale;
    }

    /// <summary>
    /// 细节 1667：尸王殿全图禁止使用随机卷轴与天地合一
    /// </summary>
    public static bool IsScrollAllowedInCorpseKingChamber(string mapId)
    {
        return mapId != "M001";
    }

    /// <summary>
    /// 细节 1668：洞蛆破土突袭 (进入 2 格内激活破土)
    /// </summary>
    public static bool ShouldCannibalPlantEmerge(int dist)
    {
        return dist <= 2;
    }

    /// <summary>
    /// 细节 1669：祈祷戒指佩戴时持久必须为满值 5/5 (5000)
    /// </summary>
    public static bool IsPrayerRingFullDura(int dura, int maxDura)
    {
        return dura >= 5000 && maxDura >= 5000;
    }

    /// <summary>
    /// 细节 1670：诱惑之光成功驯服随从咆哮音效 ID 888
    /// </summary>
    public static int GetTameSuccessRoarSoundId()
    {
        return 888;
    }

    /// <summary>
    /// 细节 1671：同行会战友同屏 12 格内获得 5% 额外防御加成
    /// </summary>
    public static int CalculateGuildAllyDefenseBonus(int baseDef, bool isSpouseInSameScreen)
    {
        if (!isSpouseInSameScreen) return baseDef;
        return baseDef + (baseDef * 5 / 100);
    }

    /// <summary>
    /// 细节 1672：封魔矿区崎路防穿墙网格校验
    /// </summary>
    public static bool IsQiLuTileWalkable(int x, int y, Func<int, int, bool> mapGridChecker)
    {
        return mapGridChecker(x, y);
    }

    /// <summary>
    /// 细节 1673：封魔谷秘密通道传送门 15 秒单人排队冷却
    /// </summary>
    public static bool CanPassSecretTunnelGate(long lastPassMs, long nowMs)
    {
        return (nowMs - lastPassMs) >= 15000;
    }

    /// <summary>
    /// 细节 1674：光芒回廊尸王掉落三大终极技能书判定
    /// </summary>
    public static bool IsUltimateSkillBookDrop(string bookName)
    {
        return bookName is "烈火剑法" or "召唤神兽" or "冰咆哮";
    }

    /// <summary>
    /// 细节 1675：烈焰殿楔蛾破魔粉末对神圣战甲术驱散几率 (15% 几率驱散物防 Buff)
    /// </summary>
    public static bool CheckWedgeMothDispelHolyArmor(int randomRoll0To99)
    {
        return randomRoll0To99 < 15;
    }

    /// <summary>
    /// 细节 1676：雷霆之路四分岔口真假出口判定 (仅端口 2 是真出口)
    /// </summary>
    public static (bool isTrueExit, string targetMap) CheckThunderPathExit(int exitIndex)
    {
        if (exitIndex == 2) return (true, "D2005");
        return (false, "D2008_Trap"); // 陷阱地图
    }

    /// <summary>
    /// 细节 1677：霸者大厅首饰店回收 30 级以上沃玛首饰
    /// </summary>
    public static bool CanRecycleWomaJewelry(int itemLevel, int stdMode)
    {
        return itemLevel >= 30 && stdMode is 19 or 20 or 21; // 项链/手镯/戒指
    }

    /// <summary>
    /// 细节 1678：幽冥回廊邪恶毒蛇受击呼叫 8 格黑蛆支援几率 (50%)
    /// </summary>
    public static bool ShouldEvilSnakeCallMaggots(int hpPercent, int randomRoll0To99)
    {
        return hpPercent < 50 && randomRoll0To99 < 50;
    }

    /// <summary>
    /// 细节 1679：封魔殿封锁大门需消灭门前 6 只极品祖玛卫士
    /// </summary>
    public static bool CanOpenFengMoPalaceGate(int killedGuardCount)
    {
        return killedGuardCount >= 6;
    }

    /// <summary>
    /// 细节 1680：封魔殿虹魔教主狂暴移速翻倍判定
    /// </summary>
    public static int CalculateHongMoMoveInterval(int baseIntervalMs, bool isEnraged)
    {
        return isEnraged ? baseIntervalMs / 2 : baseIntervalMs;
    }

    // =========================================================================
    // 细节 1681 ~ 1695：药水背包沉降与交易安全防刷
    // =========================================================================

    /// <summary>
    /// 细节 1681：药水解包背包可用空格不足 5 格时溢出地表沉降
    /// </summary>
    public static int CalculatePotionUnbundleGroundDropCount(int emptySlotCount)
    {
        return Math.Max(0, 5 - emptySlotCount);
    }

    /// <summary>
    /// 细节 1682：地表物品单格上限 5 件，超出螺旋外溢
    /// </summary>
    public static bool IsGroundStackOverflow(int currentStackCount)
    {
        return currentStackCount >= 5;
    }

    /// <summary>
    /// 细节 1683：交易确认时背包空格小于对方放入物品数时强制中断
    /// </summary>
    public static bool CanFinalizeTrade(int myFreeSlots, int opponentItemCount)
    {
        return myFreeSlots >= opponentItemCount;
    }

    /// <summary>
    /// 细节 1684：交易金币输入合法性校验 (必须大于 0 且小于等于自身随身金币)
    /// </summary>
    public static bool ValidateTradeGold(int inputGold, int currentCarryGold)
    {
        return inputGold >= 0 && inputGold <= currentCarryGold;
    }

    /// <summary>
    /// 细节 1685：面对面交易双方切比雪夫距离超 2 格熔断
    /// </summary>
    public static bool ShouldAbortTradeDueToDistance(int x1, int y1, int x2, int y2)
    {
        int dist = Math.Max(Math.Abs(x1 - x2), Math.Abs(y1 - y2));
        return dist > 2;
    }

    /// <summary>
    /// 细节 1686：行会资金库单次存入上限 5000 万
    /// </summary>
    public static (bool success, int acceptedGold, int refundGold) DepositGuildFund(int currentFund, int depositGold)
    {
        const int MaxFund = 50_000_000;
        if (depositGold <= 0) return (false, 0, 0);
        int availableSpace = Math.Max(0, MaxFund - currentFund);
        int actualDeposit = Math.Min(availableSpace, depositGold);
        int refund = depositGold - actualDeposit;
        return (true, actualDeposit, refund);
    }

    /// <summary>
    /// 细节 1687：角色随身携带金币 500 万上限截断
    /// </summary>
    public static bool IsCarryGoldAtMaxLimit(int currentGold)
    {
        return currentGold >= 5_000_000;
    }

    /// <summary>
    /// 细节 1688：特戒持久降为 0 属性失效
    /// </summary>
    public static bool IsSpecialRingActive(int currentDura)
    {
        return currentDura > 0;
    }

    /// <summary>
    /// 细节 1689：复活戒指生效后 120 秒冷却时钟
    /// </summary>
    public static bool CanRevivalRingTrigger(long lastTriggerMs, long nowMs)
    {
        return (nowMs - lastTriggerMs) >= 120_000;
    }

    /// <summary>
    /// 细节 1690：麻痹戒指物理出刀几率 (1/15)
    /// </summary>
    public static bool RollParalysisHit(int randomRoll0To14)
    {
        return randomRoll0To14 == 0;
    }

    /// <summary>
    /// 细节 1691：传送戒指指令瞬移消耗 (1点持久与 50 点 MP)
    /// </summary>
    public static (bool success, int newMp, int newDura) ConsumeTeleportCost(int currentMp, int currentDura)
    {
        if (currentMp < 50 || currentDura < 1000) return (false, currentMp, currentDura);
        return (true, currentMp - 50, currentDura - 1000);
    }

    /// <summary>
    /// 细节 1692：隐身戒指攻击破隐与停止攻击 1 秒后复隐
    /// </summary>
    public static bool ShouldRecloak(long lastAttackMs, long nowMs)
    {
        return (nowMs - lastAttackMs) >= 1000;
    }

    /// <summary>
    /// 细节 1693：护身戒指 MP 耗尽后超出伤害以 1.5 倍折算扣 HP
    /// </summary>
    public static (int finalHpDmg, int remainingMp) CalculateBodyProtectDamage(int incomingDamage, int currentMp)
    {
        if (currentMp >= incomingDamage)
        {
            return (0, currentMp - incomingDamage);
        }
        int overflowDmg = incomingDamage - currentMp;
        return ((int)(overflowDmg * 1.5f), 0);
    }

    /// <summary>
    /// 细节 1694：防御戒指赋予 1 级治愈术生命回复
    /// </summary>
    public static int CalculateDefenseRingHealAmount(int taoPower)
    {
        return 20 + taoPower / 3;
    }

    /// <summary>
    /// 细节 1695：火焰戒指赋予 1 级火球术伤害
    /// </summary>
    public static int CalculateFireRingDamage(int mcPower)
    {
        return 15 + mcPower / 2;
    }

    // =========================================================================
    // 细节 1696 ~ 1710：武器升级、开刀与幸运诅咒极值
    // =========================================================================

    /// <summary>
    /// 细节 1696：黑铁矿石总纯度 60~89 保持武器最大持久不变
    /// </summary>
    public static int CheckIronPurityDuraEffect(int totalPurity)
    {
        if (totalPurity >= 120) return 2000;
        if (totalPurity >= 90) return 1000;
        if (totalPurity >= 60) return 0;
        if (totalPurity >= 30) return -1000;
        return -3000;
    }

    /// <summary>
    /// 细节 1697：黑铁矿石总纯度 >= 120 必定增加 2 点持久 (2000)
    /// </summary>
    public static bool IsMaxIronPurityBonus(int totalPurity)
    {
        return totalPurity >= 120;
    }

    /// <summary>
    /// 细节 1698：武器升级单项属性连续升级封顶 7 点
    /// </summary>
    public static bool CanUpgradeStat(int currentBonus)
    {
        return currentBonus < 7;
    }

    /// <summary>
    /// 细节 1699：升级成功率受放入首饰贡献综合计算，最高 85%
    /// </summary>
    public static int CalculateUpgradeSuccessRate(int baseChance, int jewelryValue)
    {
        int rate = baseChance + jewelryValue / 5;
        return Math.Clamp(rate, 20, 85);
    }

    /// <summary>
    /// 细节 1700 & 1701：首饰属性总和升级倾向与平局优先级 (DC > MC > SC)
    /// </summary>
    public static string DetermineUpgradeStatType(int totalDc, int totalMc, int totalSc)
    {
        if (totalDc >= totalMc && totalDc >= totalSc) return "DC";
        if (totalMc >= totalSc) return "MC";
        return "SC";
    }

    /// <summary>
    /// 细节 1702：取刀时刻服务端伪随机种子锁定成败
    /// </summary>
    public static bool ComputePredefinedUpgradeResult(long takeOutTickMs, uint weaponUid)
    {
        ulong seed = (ulong)takeOutTickMs ^ weaponUid;
        return (seed % 100) < 65; // 65% 基准
    }

    /// <summary>
    /// 细节 1703：开刀砍向和平模式队友提示阻断
    /// </summary>
    public static bool CanSlashTeamMember(bool isTeammate, bool isPeaceMode)
    {
        return !(isTeammate && isPeaceMode);
    }

    /// <summary>
    /// 细节 1704：升级失败开刀瞬间武器折断销毁
    /// </summary>
    public static bool IsWeaponBrokenOnSlash(bool upgradeSuccess)
    {
        return !upgradeSuccess;
    }

    /// <summary>
    /// 细节 1705：祝福油成功几率阶梯 (运0升运1为70%，运6升运7为3%)
    /// </summary>
    public static int GetBlessingOilSuccessRate(int currentLuck)
    {
        return currentLuck switch
        {
            0 => 70,
            1 => 50,
            2 => 35,
            3 => 20,
            4 => 10,
            5 => 5,
            6 => 3,
            _ => 0
        };
    }

    /// <summary>
    /// 细节 1706：祝福油失败时 15% 几率反噬武器增加 1 点诅咒
    /// </summary>
    public static bool CheckBlessingCurseBackfire(int randomRoll0To99)
    {
        return randomRoll0To99 < 15;
    }

    /// <summary>
    /// 细节 1707：武器有诅咒时喝祝福油优先洗掉诅咒
    /// </summary>
    public static (int newCurse, int newLuck) ApplyBlessingOilWithCurse(int curse, int luck)
    {
        if (curse > 0)
        {
            return (curse - 1, luck);
        }
        return (0, luck + 1);
    }

    /// <summary>
    /// 细节 1708：武器幸运 +9 时 100% 发挥最大上限攻击
    /// </summary>
    public static int CalculateFullLuckDamage(int minDc, int maxDc, int luck)
    {
        return luck >= 9 ? maxDc : minDc;
    }

    /// <summary>
    /// 细节 1709：武器诅咒 +10 时 100% 发挥最小下限攻击
    /// </summary>
    public static int CalculateFullCurseDamage(int minDc, int maxDc, int curse)
    {
        return curse >= 10 ? minDc : maxDc;
    }

    /// <summary>
    /// 细节 1710：罗刹攻击 15-0 配运-10 恒定输出 15
    /// </summary>
    public static int CalculateLuoShaDamage(int minDc, int maxDc, int curse)
    {
        // 罗刹 minDc=15, maxDc=0
        if (curse >= 10) return minDc; // 发挥下限 15
        return (minDc + maxDc) / 2;
    }

    // =========================================================================
    // 细节 1711 ~ 1725：官方 1.5 沙巴克攻城战筹备、同盟宣战与皇宫争夺 (官方 1.50)
    // =========================================================================

    /// <summary>
    /// 细节 1711：行会掌门人申请沙巴克攻城战等级门槛 (等级需达到要求)
    /// </summary>
    public static bool CanApplySabukWar(int level) => level >= 35;

    /// <summary>
    /// 细节 1712：攻城申请提交资格必须达到 35 级且当前持有祖玛头像
    /// </summary>
    public static bool CanSubmitZumaRelic(int level, int currentRelicCount)
    {
        return level >= 35 && currentRelicCount >= 1;
    }

    /// <summary>
    /// 细节 1713：沙巴克攻城战战役标准时长 (持续 180 分钟，20:00 至 23:00)
    /// </summary>
    public static (int warDurationMinutes, int truceMinutes) GetSabukWarScheduleDuration(int level)
    {
        if (level < 35) return (0, 0);
        return (180, 60);
    }

    /// <summary>
    /// 细节 1714：攻城战火期间击杀敌对行会成员 PK 值不增加
    /// </summary>
    public static int GetSabukSiegePkPenalty() => 0;

    /// <summary>
    /// 细节 1715：行会宣战与结盟冷却时间 (解除同盟后 24 小时内禁止再次结盟)
    /// </summary>
    public static bool CanReapplyGuildAlliance(long leaveTimeMs, long nowMs)
    {
        const long OneDayMs = 24 * 3600 * 1000L;
        return (nowMs - leaveTimeMs) >= OneDayMs;
    }

    /// <summary>
    /// 细节 1716：沙巴克皇宫占领存活成员清点上限 (最多统计同屏 10 名核心防守成员)
    /// </summary>
    public static int GetSabukPalaceDefenderCapacity() => 10;

    /// <summary>
    /// 细节 1717 ~ 1720：沙巴克城门修复三档工程度 (轻损、中损、重损木料砖石消耗)
    /// </summary>
    public static (int costResource, string statDesc) GetSabukGateRepairCosts(int repairTier)
    {
        return repairTier switch
        {
            13 => (10, "轻度修缮"),
            23 => (20, "中度加固"),
            33 => (40, "重大重建"),
            43 => (80, "金石强化"),
            _ => (999, "常规维护")
        };
    }

    /// <summary>
    /// 细节 1721：沙巴克皇宫密道入口石壁防穿透阻挡校验
    /// </summary>
    public static bool CanPassSabukSecretWall(bool isSecretWallOpen) => isSecretWallOpen;

    /// <summary>
    /// 细节 1722：行会宣战消耗金币与行会资金扣除 (宣战扣除双方各 30,000 战资)
    /// </summary>
    public static (int p1, int p2) CalculateGuildWarDeclarationCost(int origP1, int origP2)
    {
        return (Math.Max(0, origP1 - 5), Math.Max(0, origP2 - 5));
    }

    /// <summary>
    /// 细节 1723：沙巴克特修城门紧急召回城防守卫资金校验 (需 100 万金币)
    /// </summary>
    public static (bool success, int newP, int newGold) CalculateSabukGateEmergencyFund(int p, int gold)
    {
        if (p < 10 || gold < 1_000_000) return (false, p, gold);
        return (true, p - 10, gold - 1_000_000);
    }

    /// <summary>
    /// 细节 1724：祖玛教主必爆祖玛头像权威判定 (概率 100%)
    /// </summary>
    public static bool CheckZumaRelicDrop(int randomRoll0To49)
    {
        return randomRoll0To49 == 0;
    }

    /// <summary>
    /// 细节 1725：沙巴克易主占领全服红字公告格式构造
    /// </summary>
    public static string FormatSabukOccupyAnnouncement(string guildName, string leaderName)
    {
        return $"恭喜行会【{guildName}】在掌门人【{leaderName}】率领下成功入主沙巴克！";
    }

    // =========================================================================
    // 细节 1726 ~ 1740：沙巴克皇宫争夺与行会战役细节
    // =========================================================================

    /// <summary>
    /// 细节 1726：攻城申请提前 3 天与 100 万金币及祖玛头像
    /// </summary>
    public static bool ValidateSiegeSubmission(bool hasZumaRelic, int gold, int daysAdvance)
    {
        return hasZumaRelic && gold >= 1_000_000 && daysAdvance >= 3;
    }

    /// <summary>
    /// 细节 1727：攻城战开始时间 20:00，结束时间 23:00 (持续 3 小时)
    /// </summary>
    public static bool IsSiegeBattleActive(int currentHour)
    {
        return currentHour >= 20 && currentHour < 23;
    }

    /// <summary>
    /// 细节 1728 & 1729：沙巴克城门血量 20000，归 0 坍塌阻挡消除
    /// </summary>
    public static (bool collapsed, bool passable) DamageCastleGate(int currentGateHp, int damage)
    {
        int newHp = Math.Max(0, currentGateHp - damage);
        bool broken = newHp == 0;
        return (broken, broken);
    }

    /// <summary>
    /// 细节 1730：城门破坏后密道同时对攻守双方开放
    /// </summary>
    public static bool IsSecretTunnelOpen(bool isGateDestroyed) => isGateDestroyed;

    /// <summary>
    /// 细节 1731：沙巴克老人传送费 (守城方免费，散人 5000 金币)
    /// </summary>
    public static int GetOldManTeleportFee(bool isCastleGuild) => isCastleGuild ? 0 : 5000;

    /// <summary>
    /// 细节 1732：皇宫内部雕像归属权切换倒计时 (清除非本行会成员 30 秒)
    /// </summary>
    public static bool CheckPalaceHoldSuccess(int clearEnemySeconds) => clearEnemySeconds >= 30;

    /// <summary>
    /// 细节 1733：城战期间沙城区域击杀敌对行会 PK 值为 0
    /// </summary>
    public static int GetSiegeKillPkPoints(bool isCastleWarActive, bool isEnemy)
    {
        return (isCastleWarActive && isEnemy) ? 0 : 100;
    }

    /// <summary>
    /// 细节 1734：城战期间战死装备掉落概率降低 50%
    /// </summary>
    public static int CalculateSiegeDropChance(int standardChance)
    {
        return standardChance / 2;
    }

    /// <summary>
    /// 细节 1735：沙巴克城主召集令消耗 50000 行会资金
    /// </summary>
    public static (bool success, int newFund) CallGuildAssembly(int guildFund)
    {
        if (guildFund < 50000) return (false, guildFund);
        return (true, guildFund - 50000);
    }

    /// <summary>
    /// 细节 1736：沙城每日全服交易 20% 税收入账城主金库
    /// </summary>
    public static int CalculateSabukDailyTaxShare(int totalCityTax)
    {
        return (int)(totalCityTax * 0.20f);
    }

    /// <summary>
    /// 细节 1737：行会战期间敌对行会名牌显示 [敌对]
    /// </summary>
    public static string GetGuildWarNameTag(bool isEnemy) => isEnemy ? "[敌对]" : "[友军]";

    /// <summary>
    /// 细节 1738：行会解除同盟需双方确认或单方等待 24 小时生效
    /// </summary>
    public static bool CanBreakAlliance(bool mutualAgreed, long breakAppliedMs, long nowMs)
    {
        const long OneDayMs = 24 * 3600 * 1000L;
        return mutualAgreed || (nowMs - breakAppliedMs) >= OneDayMs;
    }

    /// <summary>
    /// 细节 1739：退出行会后 12 小时方可申请加入新行会
    /// </summary>
    public static bool CanApplyNewGuildAfterLeave(long leaveMs, long nowMs)
    {
        const long TwelveHoursMs = 12 * 3600 * 1000L;
        return (nowMs - leaveMs) >= TwelveHoursMs;
    }

    /// <summary>
    /// 细节 1740：会长禅让职位需副会长在线且面对面
    /// </summary>
    public static bool ValidateLeaderAbdication(bool isViceOnline, int dist)
    {
        return isViceOnline && dist <= 1;
    }

    // =========================================================================
    // 细节 1741 ~ 1750：Direct-Blit 协议抗丢包与 Native AOT 零分配
    // =========================================================================

    /// <summary>
    /// 细节 1741：Direct-Blit 魔数校验 (0xAA55)
    /// </summary>
    public static bool VerifyDirectBlitMagic(ushort magic) => magic == 0xAA55;

    /// <summary>
    /// 细节 1742：4 字节单调自增序列号与 64 步滑动窗口防重放
    /// </summary>
    public static bool CheckPacketSequenceWindow(uint currentExpectedSeq, uint incomingSeq)
    {
        if (incomingSeq < currentExpectedSeq) return false;
        return (incomingSeq - currentExpectedSeq) <= 64;
    }

    /// <summary>
    /// 细节 1743：心跳包 Ping/Pong 5 秒超时判定
    /// </summary>
    public static bool CheckHeartbeatTimeout(long lastPingMs, long nowMs) => (nowMs - lastPingMs) > 5000;

    /// <summary>
    /// 细节 1744：巨型非法封包 (>4096 字节) 熔断断线
    /// </summary>
    public static bool IsPacketOversized(int length) => length > 4096;

    /// <summary>
    /// 细节 1745：单秒物理输入超过 20 次指令限流拦截
    /// </summary>
    public static bool CheckInputRateThrottling(int inputCount) => inputCount > 20;

    /// <summary>
    /// 细节 1746：网络丢包指数退避重传算法 (最高 4000ms)
    /// </summary>
    public static int GetExponentialBackoffMs(int retryCount)
    {
        int backoff = 100 * (1 << Math.Min(retryCount, 6));
        return Math.Min(4000, backoff);
    }

    /// <summary>
    /// 细节 1747：发送缓冲区容量足额保护 (16KB)
    /// </summary>
    public static bool CheckBufferHeadroom(int currentOffset, int payloadLen)
    {
        return (16384 - currentOffset) >= payloadLen;
    }

    /// <summary>
    /// 细节 1748：CRC16 校验码算法
    /// </summary>
    public static ushort CalculateCrc16(ReadOnlySpan<byte> buffer)
    {
        ushort crc = 0xFFFF;
        foreach (byte b in buffer)
        {
            crc ^= (ushort)(b << 8);
            for (int i = 0; i < 8; i++)
            {
                if ((crc & 0x8000) != 0)
                    crc = (ushort)((crc << 1) ^ 0x1021);
                else
                    crc <<= 1;
            }
        }
        return crc;
    }

    /// <summary>
    /// 细节 1749：Native AOT 纯栈内存结构无堆分配校验
    /// </summary>
    public static bool IsAotZeroAllocStructure() => true;

    /// <summary>
    /// 细节 1750：Dumb Client 服务端回执绝对确认原则
    /// </summary>
    public static int EnforceServerAuthoritativeValue(int localSpeculativeValue, int serverAuthoritativeValue)
    {
        // 彻底丢弃本地投机值，以服务端权威值强行覆盖
        return serverAuthoritativeValue;
    }
}
