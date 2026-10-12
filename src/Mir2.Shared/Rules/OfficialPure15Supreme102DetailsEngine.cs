using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mir2.Shared.Rules;

/// <summary>
/// 盛大官方 1.50《虎卫传说》原版第 2351 ~ 2450 号微观细节规则权威引擎 (Wave 102)
/// 100% 绝对纯血官方 1.50 封死分母！严禁混入任何 1.60 结婚、1.70 苍月岛、1.75 师徒/声望、1.76 荣誉勋章等后续版本代码。
/// 覆盖：未知暗殿神秘装备属性/诅咒/神水、白日门虎卫随从成长/叛变/祈祷暴走、尸王殿钻地隐藏入口与35级神技爆率、
/// 死亡山谷触龙神魔免与全屏毒雾、沙巴克城门损毁修缮/香石密道/皇宫攻防易手、战士刺杀破盾/双烈火/野蛮撞墙、
/// 法师诱惑上限/抗拒火环/冰咆哮必中、道士神兽双形态/骷髅进化/施毒术、红名大盗秒杀/爆装/红名村流放、Direct-Blit 零分配强类型紧凑二进制封包。
/// </summary>
public static class OfficialPure15Supreme102DetailsEngine
{
    // =========================================================================
    // 1. 未知暗殿神秘装备属性生成、诅咒判定与神水净化生态 (2351~2360)
    // =========================================================================

    /// <summary>2351: 神秘头盔属性生成 (覆盖 AC 0-5, MAC 0-5, DC 0-3, MC 0-3, SC 0-3)</summary>
    public static (int ac, int mac, int dc, int mc, int sc) GenerateMysteryHelmetStats(Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        return (rng.Next(0, 6), rng.Next(0, 6), rng.Next(0, 4), rng.Next(0, 4), rng.Next(0, 4));
    }

    /// <summary>2352: 神秘腰带/手镯/戒指属性加权生成</summary>
    public static (int primaryStat, int secondaryStat) GenerateMysteryAccessoryStats(int itemType, Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        // itemType: 1=戒指, 2=手镯, 3=腰带
        int main = rng.Next(1, 5);
        int sub = rng.Next(0, 3);
        return (main, sub);
    }

    /// <summary>2353: 神秘装备佩戴门槛随机生成 (需要攻击力 25 / 魔法 20 / 道术 20 / 等级 28)</summary>
    public static (int reqType, int reqValue) GenerateMysteryRequirement(int seed)
    {
        return (seed % 4) switch
        {
            0 => (1, 25), // 需要攻击力 25
            1 => (2, 20), // 需要魔法力 20
            2 => (3, 20), // 需要道术力 20
            _ => (0, 28)  // 需要等级 28
        };
    }

    /// <summary>2354: 神秘装备诅咒生成 (10% 几率附加诅咒状态，一旦佩戴无法正常卸下)</summary>
    public static bool RollMysteryCurse(Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        return rng.Next(100) < 10;
    }

    /// <summary>2355: 强制脱卸诅咒神秘装备拦截</summary>
    public static (bool canUnequip, string reason) CanUnequipMysteryItem(bool isCurse)
    {
        if (isCurse)
        {
            return (false, "该装备受到神秘力量的诅咒，紧紧贴合在躯体上无法取下！");
        }
        return (true, string.Empty);
    }

    /// <summary>2356: “神水”解药鉴定与消耗 (清除所有已佩戴神秘装备的诅咒)</summary>
    public static bool PurifyMysteryCurseWithHolyWater(ref bool isCurse)
    {
        if (isCurse)
        {
            isCurse = false;
            return true;
        }
        return false;
    }

    /// <summary>2357: 神秘装备死亡掉落爆率 (死亡时 30% 几率掉落并恢复未鉴定状态)</summary>
    public static bool DoesMysteryItemDropOnDeath(Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        return rng.Next(100) < 30;
    }

    /// <summary>2358: 未知暗殿真假沃玛教主 HP 与大爆辨识 (真教主 HP 2200 大爆号角，假教主 HP 100 仅爆鹿血)</summary>
    public static (int maxHp, bool isTrueBoss) GetUnknownDarkHallBossProfile(bool isReal)
    {
        return isReal ? (2200, true) : (100, false);
    }

    /// <summary>2359: 未知暗殿虎卫诱惑等级门槛 (需要 29 级诱惑之光)</summary>
    public static bool CanTameUnknownHallHuWei(int mageLevel)
    {
        return mageLevel >= 29;
    }

    /// <summary>2360: 未知暗殿限时驱逐机制 (在殿内停留满 180 分钟自动传送至毒蛇山谷)</summary>
    public static bool ShouldEvictFromUnknownHall(long elapsedMinutes)
    {
        return elapsedMinutes >= 180L;
    }

    // =========================================================================
    // 2. 白日门虎卫堂诱惑驯服、叛变与等级晋升成长模型 (2361~2370)
    // =========================================================================

    /// <summary>2361: 虎卫初始血量 400 点，每升 1 级增加 200 HP (7 级极限 1600 HP)</summary>
    public static int CalculateHuWeiMaxHp(int petLevel)
    {
        int lvl = Math.Clamp(petLevel, 0, 7);
        return 400 + lvl * 200;
    }

    /// <summary>2362: 虎卫物理防御与攻击力成长模型</summary>
    public static (int ac, int mac, int minDc, int maxDc) GetHuWeiCombatAttributes(int petLevel)
    {
        int lvl = Math.Clamp(petLevel, 0, 7);
        return (15 + lvl * 2, 10 + lvl * 2, 20 + lvl * 3, 35 + lvl * 5);
    }

    /// <summary>2363: 虎卫诱惑之光成功率梯度 (29 级 5%，30 级 10%，32 级 20%)</summary>
    public static int GetHuWeiTameSuccessRate(int mageLevel)
    {
        if (mageLevel < 29) return 0;
        if (mageLevel == 29) return 5;
        if (mageLevel <= 31) return 10;
        return 20;
    }

    /// <summary>2364: 虎卫堂刷新周期 (每 240 分钟刷新 1 只，全堂存活上限 1 只)</summary>
    public static bool CanSpawnHuWeiInHall(int currentLivingCount, long elapsedMinutes)
    {
        return currentLivingCount == 0 && elapsedMinutes >= 240L;
    }

    /// <summary>2365: 鹰卫白日门荒野刷新与超远射程 (每 180 分钟刷新，物理箭矢射程 8 格)</summary>
    public static (int attackRange, int respawnMinutes) GetYingWeiProfile()
    {
        return (8, 180);
    }

    /// <summary>2366: 虎卫击杀怪物累计经验晋级 (从 0 级白名到 7 级深蓝，每级需击杀 50 只怪物)</summary>
    public static (int newLevel, int remainingKills) UpdateHuWeiLevelByKills(int currentLevel, int currentKills, int newKills)
    {
        int totalKills = currentKills + newKills;
        int lvl = currentLevel;
        while (lvl < 7 && totalKills >= 50)
        {
            totalKills -= 50;
            lvl++;
        }
        return (lvl, totalKills);
    }

    /// <summary>2367: 虎卫自然叛变时效 (连续存活 360 分钟忠诚度归零，触发叛变红名)</summary>
    public static bool CheckHuWeiNaturalBetrayal(long survivalMinutes)
    {
        return survivalMinutes >= 360L;
    }

    /// <summary>2368: 祈祷套装全服天罚 (祈祷生效时，虎卫 100% 强制叛变为敌对红名)</summary>
    public static bool CheckHuWeiPrayerBetrayal(bool isPrayerSetFrenzyActive)
    {
        return isPrayerSetFrenzyActive;
    }

    /// <summary>2369: 虎卫小退托管保护 (法师小退 10 分钟内重新上线宠物不消失)</summary>
    public static bool IsHuWeiPreservedOnReconnect(long offlineSeconds)
    {
        return offlineSeconds <= 600L;
    }

    /// <summary>2370: 虎卫超距跟随瞬移算法 (主人距离超过 15 格时自动闪烁至主人身边 3x3)</summary>
    public static bool ShouldHuWeiBlinkToMaster(int masterX, int masterY, int petX, int petY)
    {
        int dist = Math.Max(Math.Abs(masterX - petX), Math.Abs(masterY - petY));
        return dist > 15;
    }

    // =========================================================================
    // 3. 尸王殿隐藏地穴钻地刷新与三大 35 级神技爆率 (2371~2380)
    // =========================================================================

    /// <summary>2371: 废矿区东部僵尸钻地形成 60 秒黄色地洞入口坐标判定</summary>
    public static bool IsCorpseChamberEntranceTile(int mapId, int x, int y)
    {
        return mapId == 100 && x == 138 && y == 107;
    }

    /// <summary>2372: 地洞每次最多允许 15 名玩家通过，超员或超时自动塌陷</summary>
    public static (bool allowed, int currentCount) TryEnterCorpseHole(int currentCount, long elapsedSeconds)
    {
        if (elapsedSeconds > 60L || currentCount >= 15) return (false, currentCount);
        return (true, currentCount + 1);
    }

    /// <summary>2373: 尸王殿刷新周期 (每 120 分钟第一波刷新 10 只尸王，后续每 20 分钟补刷)</summary>
    public static int GetCorpseKingRespawnDelay(bool isFirstWave)
    {
        return isFirstWave ? 120 : 20;
    }

    /// <summary>2374: 尸王铁链重砸前方直线 2 格穿透伤害 (DC 35-50)</summary>
    public static (bool hit, int damage) CalculateCorpseKingChainDamage(int dx, int dy, Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        if (Math.Abs(dx) <= 2 && dy == 0 || Math.Abs(dy) <= 2 && dx == 0)
        {
            return (true, rng.Next(35, 51));
        }
        return (false, 0);
    }

    /// <summary>2375: 尸王殿全图禁止随机传送卷轴与地牢逃脱卷</summary>
    public static bool IsCorpseChamberScrollBlocked(string mapId)
    {
        return mapId == "D101" || mapId == "D1011";
    }

    /// <summary>2376: 尸王大爆 5x5 掉落烈火剑法、冰咆哮、召唤神兽三大神技 (1/50 爆率)</summary>
    public static bool RollCorpseKingUltimateSkillBook(Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        return rng.Next(50) == 0;
    }

    /// <summary>2377: 尸王半血以下狂暴攻速加成 (移速与攻速缩短至 500ms)</summary>
    public static int GetCorpseKingAttackInterval(int curHp, int maxHp)
    {
        if (maxHp > 0 && curHp * 100 / maxHp <= 50) return 500;
        return 800;
    }

    /// <summary>2378: 尸王殿中央水洼不可通行阻挡判定</summary>
    public static bool IsCorpseChamberWaterObstacle(int x, int y)
    {
        return x >= 20 && x <= 22 && y >= 20 && y <= 22;
    }

    /// <summary>2379: 尸王受地狱雷光不死系电击瘫痪定身 300ms</summary>
    public static (bool paralyzed, long durationMs) ApplyHellThunderParalysisToUndead(bool isUndead)
    {
        return isUndead ? (true, 300L) : (false, 0L);
    }

    /// <summary>2380: 尸王爆落黑铁矿石纯度区间 (纯度 10~18)</summary>
    public static int GenerateCorpseKingBlackIronPurity(Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        return rng.Next(10, 19);
    }

    // =========================================================================
    // 4. 死亡山谷触龙神百毒不侵与全屏毒雾绝杀机制 (2381~2390)
    // =========================================================================

    /// <summary>2381: 死亡棺材触龙神 3 格破土而出苏醒坐标判定</summary>
    public static bool ShouldEvilCentipedeAwaken(int bossX, int bossY, int playerX, int playerY)
    {
        int dist = Math.Max(Math.Abs(bossX - playerX), Math.Abs(bossY - playerY));
        return dist <= 3;
    }

    /// <summary>2382: 触龙神魔法防御 999 (绝对免疫一切魔法与道术伤害)</summary>
    public static bool IsEvilCentipedeImmuneToMagic() => true;

    /// <summary>2383: 触龙神仅受物理攻击伤害与防御减免计算 (AC 25-35, HP 1000)</summary>
    public static int CalculateEvilCentipedePhysicalDamage(int incomingDc, int defenseAc)
    {
        int realDmg = Math.Max(1, incomingDc - defenseAc);
        return realDmg;
    }

    /// <summary>2384: 触龙神全屏绿毒喷雾 (每秒扣除 8 点生命值，持续 30 秒)</summary>
    public static (int poisonDmgPerSec, int durationSec) GetEvilCentipedePoisonProfile()
    {
        return (8, 30);
    }

    /// <summary>2385: 触龙神剧毒附带 15% 几率石化麻痹目标 3 秒</summary>
    public static (bool paralyzed, int durationSec) RollCentipedeParalysis(int poisonDodgePercent, Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        int rate = Math.Max(0, 15 - poisonDodgePercent);
        if (rng.Next(100) < rate)
        {
            return (true, 3);
        }
        return (false, 0);
    }

    /// <summary>2386: 触龙神脱战 60 秒钻回地下并回满血</summary>
    public static bool ShouldEvilCentipedeSubmerge(long outOfCombatSeconds)
    {
        return outOfCombatSeconds >= 60L;
    }

    /// <summary>2387: 触龙神名牌武器与沃玛级首饰掉落判定</summary>
    public static bool IsCentipedeSignatureDrop(string itemName)
    {
        return itemName == "炼狱" || itemName == "魔杖" || itemName == "银蛇" ||
               itemName == "龙之戒指" || itemName == "红宝石戒指" || itemName == "铂金戒指";
    }

    /// <summary>2388: 触龙神不可被击退、不可被抗拒火环推动、不可被诱惑</summary>
    public static bool IsEvilCentipedeUnmovable() => true;

    /// <summary>2389: 触龙神刷新周期为 180 分钟，死亡震屏 500ms</summary>
    public static (int respawnMinutes, int shakeDurationMs) GetCentipedeSpawnConfig()
    {
        return (180, 500);
    }

    /// <summary>2390: 道士施毒术削减触龙神防御 20% (红毒破防有效)</summary>
    public static int ApplyRedPoisonToCentipede(int originalAc, bool hasRedPoison)
    {
        return hasRedPoison ? originalAc * 80 / 100 : originalAc;
    }

    // =========================================================================
    // 5. 沙巴克城门损毁修缮、密道奇袭与皇宫旗帜易手机制 (2391~2400)
    // =========================================================================

    /// <summary>2391: 沙巴克城门耐久度 50,000，耐久归 0 变为完全破损可通行</summary>
    public static (int currentHp, bool isDestroyed) DamageSabukGate(int currentHp, int damage)
    {
        int newHp = Math.Max(0, currentHp - damage);
        return (newHp, newHp == 0);
    }

    /// <summary>2392: 城主提交 200,000 金币与 50 块纯度 15 铁矿开启修缮城门</summary>
    public static (bool allowed, string reason) ValidateSabukGateRepairMaterials(int gold, int ironCount, int minPurity)
    {
        if (gold < 200_000) return (false, "修缮城门需要 200,000 沙巴克国库资金。");
        if (ironCount < 50 || minPurity < 15) return (false, "修缮城门需要 50 块纯度 15 以上的纯铁矿石。");
        return (true, string.Empty);
    }

    /// <summary>2393: 城门修缮耗时 5 分钟，受击则中断修缮</summary>
    public static bool CheckSabukGateRepairProgress(long elapsedSeconds, bool isAttacked)
    {
        if (isAttacked) return false;
        return elapsedSeconds >= 300L;
    }

    /// <summary>2394: 香石墓穴密道守卫仅守城方行会成员免费通行</summary>
    public static bool CanPassSecretPassageForFree(bool isDefenderGuild)
    {
        return isDefenderGuild;
    }

    /// <summary>2395: 攻城方进入密道需消耗潜入令牌或支付 5,000 金币</summary>
    public static (bool allowed, int remainingGold) ChargeSecretPassageFee(bool hasToken, int gold)
    {
        if (hasToken) return (true, gold);
        if (gold < 5000) return (false, gold);
        return (true, gold - 5000);
    }

    /// <summary>2396: 沙巴克皇宫占领判定：殿内仅存一个行会成员且持续 10 秒</summary>
    public static bool CanCapturePalace(int livingAttackerCount, int livingDefenderCount, long holdSeconds)
    {
        if (livingDefenderCount == 0 && livingAttackerCount > 0 && holdSeconds >= 10L)
        {
            return true;
        }
        return false;
    }

    /// <summary>2397: 攻城战时间严格为 20:00:00 至 23:00:00 (180 分钟)</summary>
    public static bool IsSabukWarTime(int hour, int minute)
    {
        if (hour >= 20 && hour < 23) return true;
        if (hour == 23 && minute == 0) return true;
        return false;
    }

    /// <summary>2398: 攻城期间沙城全境 PK 值为 0，不红名不黄名</summary>
    public static int CalculateSabukWarPkGain(bool isWarActive, bool isInsideSabuk)
    {
        if (isWarActive && isInsideSabuk) return 0;
        return 100;
    }

    /// <summary>2399: 沙巴克城主每日商业税收结算 (交易额 20%，上限 200 万金币)</summary>
    public static int CalculateSabukDailyTax(int totalDailyCommerceGold)
    {
        int tax = totalDailyCommerceGold * 20 / 100;
        return Math.Min(2_000_000, tax);
    }

    /// <summary>2400: 沙巴克成员在沙城商铺享受 8 折特修与购药优惠</summary>
    public static int ApplySabukMemberDiscount(int originalPrice, bool isSabukMember)
    {
        if (isSabukMember)
        {
            return originalPrice * 80 / 100;
        }
        return originalPrice;
    }

    // =========================================================================
    // 6. 战士刺杀破盾、双烈火与野蛮冲撞物理硬碰撞 (2401~2410)
    // =========================================================================

    /// <summary>2401: 刺杀剑术 1 格普通物理攻击，2 格破空剑气 100% 绝对破防</summary>
    public static (int damage, bool isPiercing) CalculateStabbingSwordDamage(int baseDc, int dist, int targetAc)
    {
        if (dist == 2)
        {
            return (baseDc, true); // 隔位剑气 100% 破防
        }
        int dmg = Math.Max(1, baseDc - targetAc);
        return (dmg, false);
    }

    /// <summary>2402: 刺杀剑气完全穿透魔法盾，法师护盾不吸收伤害</summary>
    public static int ApplyMagicShieldToStab(int swordDamage, bool isPiercing, double shieldAbsorption)
    {
        if (isPiercing) return swordDamage; // 穿透剑气完全不吸收
        return (int)(swordDamage * (1.0 - shieldAbsorption));
    }

    /// <summary>2403: 烈火剑法蓄力状态持续 10 秒，超时未挥刀自然熄灭</summary>
    public static bool IsFireHitStillCharged(long chargeTick, long nowTick)
    {
        return (nowTick - chargeTick) <= 10_000L;
    }

    /// <summary>2404: 双烈火爆发：8~10 秒窗口可打出连续两记 2.6 倍物理伤害</summary>
    public static (bool canDoubleHit, int totalDamage) CalculateDoubleFireHitDamage(int baseDc, long secondSinceFirstHit)
    {
        if (secondSinceFirstHit >= 8 && secondSinceFirstHit <= 10)
        {
            int singleDmg = (int)(baseDc * 2.6);
            return (true, singleDmg * 2);
        }
        return (false, (int)(baseDc * 2.6));
    }

    /// <summary>2405: 野蛮冲撞推击判定：自身等级必须严格大于目标等级</summary>
    public static (bool canTackle, long selfStunMs) CanTackleTarget(int attackerLvl, int targetLvl)
    {
        if (attackerLvl > targetLvl)
        {
            return (true, 0L);
        }
        return (false, 1000L); // 等级不足反噬自身眩晕 1000ms
    }

    /// <summary>2406: 野蛮冲撞推击 2 格并附加 500ms 定身硬直</summary>
    public static (int pushDistance, long targetStunMs) GetTacklePushEffect()
    {
        return (2, 500L);
    }

    /// <summary>2407: 野蛮冲撞撞墙或撞击不可通行障碍，自身眩晕 1500ms</summary>
    public static long GetTackleWallCrashStunDuration() => 1500L;

    /// <summary>2408: 半月弯刀横扫 4 个目标，主目标 100% 伤害，副目标 35% 递减</summary>
    public static int CalculateHalfMoonDamage(int baseDc, bool isMainTarget)
    {
        return isMainTarget ? baseDc : baseDc * 35 / 100;
    }

    /// <summary>2409: 攻杀剑术 25% 几率触发暴击，造成 1.5 倍爆发伤害</summary>
    public static (bool triggered, int damage) RollAssassinateStrike(int baseDc, Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        if (rng.Next(100) < 25)
        {
            return (true, (int)(baseDc * 1.5));
        }
        return (false, baseDc);
    }

    /// <summary>2410: 战士每点攻速缩短 50ms 挥刀间隔 (基准 800ms，极限 650ms)</summary>
    public static int CalculateWarriorAttackInterval(int attackSpeedBonus)
    {
        int interval = 800 - attackSpeedBonus * 50;
        return Math.Max(650, interval);
    }

    // =========================================================================
    // 7. 法师诱惑随从上限、抗拒火环与冰咆哮绝对命中 (2411~2420)
    // =========================================================================

    /// <summary>2411: 法师诱惑随从数量严格上限为 5 只</summary>
    public static bool CanTameMoreMinions(int currentMinionCount)
    {
        return currentMinionCount < 5;
    }

    /// <summary>2412: 随从存活 360 分钟自然老死</summary>
    public static bool IsMinionExpired(long aliveMinutes)
    {
        return aliveMinutes >= 360L;
    }

    /// <summary>2413: 抗拒火环等级压制：自身等级高于目标方可弹开 2 格，首领 100% 免疫</summary>
    public static (bool pushed, int pushDistance) CanRingOfFirePush(int casterLvl, int targetLvl, bool isBoss)
    {
        if (isBoss) return (false, 0);
        if (casterLvl > targetLvl) return (true, 2);
        return (false, 0);
    }

    /// <summary>2414: 冰咆哮 100% 绝对无视目标的魔法躲避属性</summary>
    public static bool DoesBlizzardIgnoreMagicDodge() => true;

    /// <summary>2415: 地狱雷光对不死系怪物造成 200% 伤害与 500ms 瘫痪</summary>
    public static (int damage, long paralysisMs) CalculateHellThunderDamage(int baseMc, bool isUndead)
    {
        if (isUndead)
        {
            return (baseMc * 2, 500L);
        }
        return (baseMc, 0L);
    }

    /// <summary>2416: 疾光电影直线 8 格，第 8 格末端激增 25% 伤害</summary>
    public static int CalculateLaserDamage(int baseMc, int stepIndex)
    {
        if (stepIndex == 7) // 第 8 格 (0-indexed 7)
        {
            return baseMc * 125 / 100;
        }
        return baseMc;
    }

    /// <summary>2417: 火墙术持续时间公式：(技能等级 * 10) + (自身魔法力 / 2) 秒</summary>
    public static int CalculateFireWallDuration(int skillLvl, int maxMc)
    {
        return skillLvl * 10 + maxMc / 2;
    }

    /// <summary>2418: 魔法盾吸收伤害比例：0级40%, 1级50%, 2级60%, 3级68%</summary>
    public static double GetMagicShieldAbsorption(int shieldLevel)
    {
        return shieldLevel switch
        {
            0 => 0.40,
            1 => 0.50,
            2 => 0.60,
            _ => 0.68
        };
    }

    /// <summary>2419: 圣言术对不死系秒杀几率：(法师等级 - 怪物等级) * 4%，上限 45%</summary>
    public static int CalculateHolyWordChance(int mageLvl, int monLvl, bool isUndead)
    {
        if (!isUndead || mageLvl <= monLvl) return 0;
        int chance = (mageLvl - monLvl) * 4;
        return Math.Clamp(chance, 0, 45);
    }

    /// <summary>2420: 诱惑黄闪状态受击立即解除定身并建立仇恨</summary>
    public static bool DoesDamageWakeUpTamedMonster(bool isTamedYellowGlow)
    {
        return isTamedYellowGlow;
    }

    // =========================================================================
    // 8. 道士神兽防御/喷火双形态、骷髅进化与施毒术生态 (2421~2430)
    // =========================================================================

    /// <summary>2421: 召唤神兽 0 级 HP 500，升至 7 级 HP 2400</summary>
    public static int GetDivineBeastMaxHp(int beastLevel)
    {
        int lvl = Math.Clamp(beastLevel, 0, 7);
        return 500 + lvl * (1900 / 7);
    }

    /// <summary>2422: 神兽趴下待机双防提升 50%，每 2 秒自愈 5% 最大生命值</summary>
    public static (int buffedAc, int buffedMac, int healAmount) GetDivineBeastDefensiveMode(int baseAc, int baseMac, int maxHp)
    {
        return (baseAc * 150 / 100, baseMac * 150 / 100, maxHp * 5 / 100);
    }

    /// <summary>2423: 神兽站立喷火穿透 2 格：主目标 100%，后位 50%</summary>
    public static (int mainDamage, int pierceDamage) CalculateDivineBeastFlameDamage(int baseFireDc)
    {
        return (baseFireDc, baseFireDc / 2);
    }

    /// <summary>2424: 召唤骷髅三阶进化：0~2级木斧黄名，3~5级铁斧深蓝名，7级大板斧攻速 350ms</summary>
    public static (string weaponName, int attackIntervalMs) GetSkeletonEvolutionProfile(int skelLevel)
    {
        int lvl = Math.Clamp(skelLevel, 0, 7);
        if (lvl <= 2) return ("木斧", 600);
        if (lvl <= 5) return ("铁斧", 450);
        return ("大板斧", 350);
    }

    /// <summary>2425: 道士治愈术对己方召唤随从回复效果提升 30%</summary>
    public static int CalculateTaoistPetHealAmount(int baseHeal, bool isPet)
    {
        return isPet ? baseHeal * 130 / 100 : baseHeal;
    }

    /// <summary>2426: 施毒术红毒削减目标 20% 物防与魔御，持续 60 秒</summary>
    public static (int debuffedAc, int debuffedMac, int durationSec) ApplyTaoistRedPoison(int ac, int mac)
    {
        return (ac * 80 / 100, mac * 80 / 100, 60);
    }

    /// <summary>2427: 施毒术绿毒每秒扣血受道术加成，且完全冻结自然回血</summary>
    public static (int tickDamage, bool freezeRegen) ApplyTaoistGreenPoison(int maxSc)
    {
        int dmg = Math.Max(2, maxSc / 4);
        return (dmg, true);
    }

    /// <summary>2428: 施毒术必须消耗毒药包耐久，归 0 施毒失败</summary>
    public static (bool success, int remainingDura) ConsumePoisonPouchDura(int currentDura)
    {
        if (currentDura <= 0) return (false, 0);
        return (true, currentDura - 1);
    }

    /// <summary>2429: 灵魂火符施放消耗 1 点护身符持久</summary>
    public static (bool success, int remainingAmuletDura) ConsumeAmuletDura(int currentDura)
    {
        if (currentDura <= 0) return (false, 0);
        return (true, currentDura - 1);
    }

    /// <summary>2430: 隐身术在移动或攻击后立即失效，且对识破怪无效</summary>
    public static bool IsInvisibilityCancelled(bool isMoving, bool isAttacking, bool isAntiInvisMonster)
    {
        return isMoving || isAttacking || isAntiInvisMonster;
    }

    // =========================================================================
    // 9. 红名大盗大刀卫士秒杀、死亡大爆与红名村流放机制 (2431~2440)
    // =========================================================================

    /// <summary>2431: 主动攻击白名变黄名 60 秒，杀白名 +100 PK 点，PK >= 200 变红名</summary>
    public static (string nameColor, int newPkPoints) UpdatePkStatusOnKill(int currentPkPoints, bool isVictimWhite)
    {
        int newPoints = isVictimWhite ? currentPkPoints + 100 : currentPkPoints;
        string color = newPoints >= 200 ? "红名" : (newPoints > 0 ? "黄名" : "白名");
        return (color, newPoints);
    }

    /// <summary>2432: 大刀卫士与弓箭守卫对 8 格内红名玩家发动 99999 真实伤害秒杀</summary>
    public static (bool shouldExecute, int damage) GuardExecuteRedName(int dist, bool isRedName)
    {
        if (dist <= 8 && isRedName)
        {
            return (true, 99999);
        }
        return (false, 0);
    }

    /// <summary>2433: 红名玩家死亡身上装备 50% 几率掉落，背包物品必定掉落 2~4 件</summary>
    public static (bool dropEquip, int bagDropCount) CalculateRedNameDeathLoot(Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        bool dropEquip = rng.Next(100) < 50;
        int bagDrops = rng.Next(2, 5); // 2, 3, 4
        return (dropEquip, bagDrops);
    }

    /// <summary>2434: 红名玩家使用回城卷强制传送至盟重红名村 (850, 670)</summary>
    public static (string mapId, int x, int y) GetRecallDestination(bool isRedName)
    {
        if (isRedName)
        {
            return ("3", 850, 670); // 盟重省红名村
        }
        return ("0", 330, 270); // 比奇主城
    }

    /// <summary>2435: PK 点野外在线每 60 秒扣除 1 点，安全区站街不扣</summary>
    public static int DecayPkPoints(int currentPk, bool isInSafeZone, long onlineSeconds)
    {
        if (isInSafeZone || currentPk <= 0) return currentPk;
        int decay = (int)(onlineSeconds / 60L);
        return Math.Max(0, currentPk - decay);
    }

    /// <summary>2436: 正当防卫机制：反击并击杀黄名玩家不增加任何 PK 值</summary>
    public static int CalculateSelfDefensePk(bool isVictimYellowName)
    {
        return isVictimYellowName ? 0 : 100;
    }

    /// <summary>2437: 红名村黑市商人双倍黑心物价 (正常价格的 200%)</summary>
    public static int GetRedNameVillageItemPrice(int normalPrice)
    {
        return normalPrice * 2;
    }

    /// <summary>2438: 杀白名 40% 几率武器增加 1 点诅咒 (最高 +10)</summary>
    public static int RollWeaponCurseOnKill(int currentCurse, Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        if (currentCurse >= 10) return 10;
        if (rng.Next(100) < 40)
        {
            return currentCurse + 1;
        }
        return currentCurse;
    }

    /// <summary>2439: 武器诅咒 +10 强制发挥物理攻击绝对下限</summary>
    public static int CalculateCurse10Damage(int minDc, int maxDc, int curseLevel)
    {
        if (curseLevel >= 10) return minDc;
        return maxDc;
    }

    /// <summary>2440: 武器与项链幸运 +9 刀刀发挥物理攻击绝对最高上限</summary>
    public static int CalculateLuck9Damage(int minDc, int maxDc, int luckLevel)
    {
        if (luckLevel >= 9) return maxDc;
        return minDc;
    }

    // =========================================================================
    // 10. Direct-Blit 零分配强类型紧凑二进制封包与 Dumb Client 权威覆盖 (2441~2450)
    // =========================================================================

    /// <summary>2441: 网络传输魔数严格为 0x594D ('M', 'Y')</summary>
    public static bool ValidatePacketMagic(ushort magic)
    {
        return magic == 0x594D;
    }

    /// <summary>2442: 包序号 64 步滑动窗口防重放与乱序校验</summary>
    public static bool ValidateSlidingWindowSequence(uint lastSeq, uint newSeq)
    {
        if (newSeq <= lastSeq) return false;
        return (newSeq - lastSeq) <= 64;
    }

    /// <summary>2443: 封包 CRC16 零堆分配校验计算</summary>
    public static ushort CalculateCrc16(ReadOnlySpan<byte> buffer)
    {
        ushort crc = 0xFFFF;
        for (int i = 0; i < buffer.Length; i++)
        {
            crc ^= buffer[i];
            for (int j = 0; j < 8; j++)
            {
                if ((crc & 1) != 0) crc = (ushort)((crc >> 1) ^ 0xA001);
                else crc >>= 1;
            }
        }
        return crc;
    }

    /// <summary>2444: 紧凑包头长度严格为 8 字节</summary>
    public static int GetCompactHeaderSize() => 8;

    /// <summary>2445: Direct-Blit 内存结构零拷贝对齐校验</summary>
    public static bool ValidateDirectBlitPayloadSize<T>(int expectedSize) where T : unmanaged
    {
        return Unsafe.SizeOf<T>() == expectedSize;
    }

    /// <summary>2446: Dumb Client 客户端绝对无状态原则：生命值/魔法值以服务端为准</summary>
    public static (int authoritativeHp, int authoritativeMp) ApplyServerAuthoritativeGauges(int serverHp, int serverMp)
    {
        return (serverHp, serverMp);
    }

    /// <summary>2447: 客户端挥刀动作前摇 350ms 后摇 450ms 锁帧防抽刀</summary>
    public static (long preDelayMs, long postDelayMs) GetAttackActionLockTimers()
    {
        return (350L, 450L);
    }

    /// <summary>2448: 客户端位移移动步长校验：走步 1 格，跑步 2 格，超距强制拉回</summary>
    public static bool ValidateMoveStep(int startX, int startY, int destX, int destY, bool isRunning)
    {
        int maxDist = isRunning ? 2 : 1;
        int dx = Math.Abs(destX - startX);
        int dy = Math.Abs(destY - startY);
        return dx <= maxDist && dy <= maxDist && (dx > 0 || dy > 0);
    }

    /// <summary>2449: 物品拾取距离强校验：角色必须站在掉落物同 1 格坐标 (距离 0)</summary>
    public static bool CanPickUpGroundItem(int playerX, int playerY, int itemX, int itemY)
    {
        return playerX == itemX && playerY == itemY;
    }

    /// <summary>2450: 客户端交易距离超 3 格自动熔断回滚</summary>
    public static bool IsTradeDistanceValid(int x1, int y1, int x2, int y2)
    {
        int dist = Math.Max(Math.Abs(x1 - x2), Math.Abs(y1 - y2));
        return dist <= 3;
    }
}
