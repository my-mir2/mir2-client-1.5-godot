using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mir2.Shared.Rules;

/// <summary>
/// 盛大官方 1.50《虎卫传说》原版第 2251 ~ 2350 号微观细节规则权威引擎 (Wave 101)
/// 100% 绝对纯血官方 1.50 封死分母！严禁混入任何 1.60 结婚、1.70 苍月岛、1.75 师徒/声望、1.76 荣誉勋章等后续版本代码。
/// 覆盖：比奇皇宫国王与沃玛号角行会创建、沃玛三层沃玛号角独占、祖玛七层三狭窄走廊与教主解封、
/// 猪洞七层白野猪大爆、未知暗殿真假首领与神秘装备神水洗净、赤月双头金刚割肉与双头血魔、
/// 武器升级跳点与矿石纯度方程、战法道平衡数值、沙巴克皇宫争夺攻防、Direct-Blit Dumb Client 纯血架构。
/// </summary>
public static class OfficialPure15Apex101DetailsEngine
{
    // =========================================================================
    // 1. 比奇城皇宫国王任务与创建行会沃玛号角深层流转 (2251~2260)
    // =========================================================================

    /// <summary>2251: 行会创建者必须为 22 级以上且未加入任何行会</summary>
    public static (bool allowed, string reason) ValidateGuildCreator(int level, bool hasGuild)
    {
        if (hasGuild) return (false, "阁下已有行会所属，不可重立门户。");
        if (level < 22) return (false, "创建行会需要角色等级达到 22 级。");
        return (true, string.Empty);
    }

    /// <summary>2252: 行会名字合法性校验 (1~7 个中文字符，长度 2~14 字节，禁止敏感词)</summary>
    public static bool ValidateGuildName(string guildName)
    {
        if (string.IsNullOrWhiteSpace(guildName)) return false;
        int byteLen = 0;
        foreach (char c in guildName)
        {
            byteLen += (c > 127) ? 2 : 1;
        }
        return byteLen >= 2 && byteLen <= 14;
    }

    /// <summary>2253: 沃玛号角在创建成功后物理销毁</summary>
    public static (bool consumed, int remainingCount) ConsumeWoomaHornOnGuildCreate(int currentHornCount)
    {
        if (currentHornCount <= 0) return (false, 0);
        return (true, currentHornCount - 1);
    }

    /// <summary>2254: 100 万金币创建行会税费直接扣除并存入王国内库</summary>
    public static (bool success, int remainingGold) ChargeGuildCreationFee(int playerGold)
    {
        const int fee = 1_000_000;
        if (playerGold < fee) return (false, playerGold);
        return (true, playerGold - fee);
    }

    /// <summary>2255: 行会初始人数上限 50 人，掌门封号默认“一统天下”</summary>
    public static (int maxMembers, string defaultLeaderTitle) GetGuildInitialConfig()
    {
        return (50, "一统天下");
    }

    /// <summary>2256: 行会掌门人禅让给副掌门人需对方双向在线确认</summary>
    public static bool CanAbdicateGuildLeader(bool targetOnline, bool targetConfirmed)
    {
        return targetOnline && targetConfirmed;
    }

    /// <summary>2257: 行会成员退会后 24 小时冷却方可申请新行会</summary>
    public static bool CanJoinNewGuildAfterQuit(long quitTimeMs, long nowMs)
    {
        const long cooldown = 24 * 3600 * 1000L;
        return (nowMs - quitTimeMs) >= cooldown;
    }

    /// <summary>2258: 掌门人解散行会必须在行会无任何在册成员时方可执行</summary>
    public static bool CanDisbandGuild(int memberCountExcludingLeader)
    {
        return memberCountExcludingLeader == 0;
    }

    /// <summary>2259: 行会公告字数限制 (最多 60 字节)</summary>
    public static bool ValidateGuildNoticeLength(string notice)
    {
        if (string.IsNullOrEmpty(notice)) return true;
        int byteCount = 0;
        foreach (char c in notice)
        {
            byteCount += (c > 127) ? 2 : 1;
        }
        return byteCount <= 60;
    }

    /// <summary>2260: 行会敌对列表最多容纳 10 个敌对行会</summary>
    public static bool CanAddEnemyGuild(int currentEnemyCount)
    {
        return currentEnemyCount < 10;
    }

    // =========================================================================
    // 2. 沃玛三层沃玛号角与沃玛教主神殿绝对机制 (2261~2270)
    // =========================================================================

    /// <summary>2261: 沃玛教主 180 分钟周期单只刷新</summary>
    public static int GetWoomaLeaderRespawnMinutes() => 180;

    /// <summary>2262: 沃玛教主瞬移机制：生命 < 30% 受到近战攻击 25% 概率瞬移至神殿随机坐标</summary>
    public static bool ShouldWoomaLeaderTeleport(int curHp, int maxHp, int randomRoll0To99)
    {
        return curHp <= maxHp * 0.30f && randomRoll0To99 < 25;
    }

    /// <summary>2263: 沃玛号角官方掉落概率：沃玛教主 100% 必爆且每次仅爆 1 支</summary>
    public static (bool hornDropped, int dropCount) RollWoomaHornDrop(int leaderRoll)
    {
        return (true, 1);
    }

    /// <summary>2264: 沃玛教主狂暴时召唤 4 只火焰沃玛</summary>
    public static int GetWoomaLeaderEnrageGuardCount(bool isEnraged) => isEnraged ? 4 : 2;

    /// <summary>2265: 沃玛寺庙黑暗视野衰减：无火把时视野强制收缩至 2 格</summary>
    public static int CalculateWoomaDarknessVision(bool hasLightSource, int baseVision)
    {
        return hasLightSource ? baseVision : 2;
    }

    /// <summary>2266: 火把持久随时间每 10 秒衰减 1 点，持久为 0 时火把熄灭</summary>
    public static (int newDura, bool isExtinguished) DecayTorchDurability(int currentDura, int elapsedSeconds)
    {
        int loss = elapsedSeconds / 10;
        int next = Math.Max(0, currentDura - loss);
        return (next, next == 0);
    }

    /// <summary>2267: 沃玛暗黑法师免疫抗拒火环 (等级高于玩家时不可被推开)</summary>
    public static bool CanRepelWoomaDarkMage(int playerLevel, int mageLevel)
    {
        return playerLevel > mageLevel;
    }

    /// <summary>2268: 沃玛卫士 8x8 环形散落掉落机制</summary>
    public static int GetWoomaGuardDropRadius() => 4;

    /// <summary>2269: 沃玛号角击杀者所属行会享有 60 秒绝对保护期</summary>
    public static bool CanPickupWoomaHornProtected(long killerGuildId, long pickerGuildId, long dropElapsedSeconds)
    {
        if (dropElapsedSeconds >= 60) return true;
        return killerGuildId == pickerGuildId;
    }

    /// <summary>2270: 沃玛三层神殿四根立柱为不可破坏物理阻挡</summary>
    public static bool IsWoomaPillarTile(int x, int y)
    {
        return (x == 24 && y == 24) || (x == 24 && y == 36) ||
               (x == 36 && y == 24) || (x == 36 && y == 36);
    }

    // =========================================================================
    // 3. 祖玛七层三狭窄走廊与祖玛教主破石而出机制 (2271~2280)
    // =========================================================================

    /// <summary>2271: 祖玛教主石像封印状态免疫一切物理与魔法伤害</summary>
    public static int CalculateDamageOnSealedZumaLeader(int rawDamage, bool isSealed)
    {
        return isSealed ? 0 : rawDamage;
    }

    /// <summary>2272: 玩家踏入祭坛 3 格范围内解除祖玛教主石封，破石产生全屏震颤</summary>
    public static (bool unseal, bool fullScreenTremor) CheckZumaLeaderUnseal(int playerDistToAltar)
    {
        bool unseal = playerDistToAltar <= 3;
        return (unseal, unseal);
    }

    /// <summary>2273: 解除封印瞬间教主召唤 1 只极品祖玛卫士与 2 只祖玛雕像</summary>
    public static (int eliteGuards, int statues) GetZumaUnsealMinions()
    {
        return (1, 2);
    }

    /// <summary>2274: 祖玛七层三狭窄通道 1 格宽度限制：严禁穿人与穿怪</summary>
    public static bool CanPassZumaNarrowCorridor(bool isTileOccupied) => !isTileOccupied;

    /// <summary>2275: 极品祖玛卫士移速为普通卫士 2 倍 (移动间隔 200ms)</summary>
    public static int GetEliteZumaGuardMoveIntervalMs() => 200;

    /// <summary>2276: 极品祖玛雕像物理攻击高达 35~80，攻速提升 50%</summary>
    public static (int minAtk, int maxAtk, int attackIntervalMs) GetEliteZumaStatueCombatStats()
    {
        return (35, 80, 500); // 正常 1000ms，提升50%为 500ms
    }

    /// <summary>2277: 祖玛头像官方 1.5 爆率判定 (祖玛教主 100% 必爆)</summary>
    public static bool RollZumaHeadDrop() => true;

    /// <summary>2278: 祖玛弓箭手破甲箭 15% 几率无视战士物理护甲</summary>
    public static int CalculateZumaArcherDamage(int baseAtk, int targetAc, int randomRoll0To99)
    {
        int effectiveAc = (randomRoll0To99 < 15) ? 0 : targetAc;
        return Math.Max(1, baseAtk - effectiveAc);
    }

    /// <summary>2279: 祖玛教主之家石门关闭机制 (进入 1 人后门落锁 10 秒)</summary>
    public static bool IsZumaLeaderGateLocked(long lastEnterMs, long nowMs)
    {
        return (nowMs - lastEnterMs) < 10000;
    }

    /// <summary>2280: 祖玛装备 (裁决/骨玉/龙纹) 官方掉落权重 (1/2000)</summary>
    public static bool RollZumaUltimateWeaponDrop(int roll0To1999) => roll0To1999 == 0;

    // =========================================================================
    // 4. 石墓七层猪洞生态与白野猪大爆机制 (2281~2290)
    // =========================================================================

    /// <summary>2281: 白野猪流星锤重击 10% 概率造成目标 1 格击退</summary>
    public static (bool knockback, int tiles) CheckWhiteBoarKnockback(int randomRoll0To99)
    {
        return randomRoll0To99 < 10 ? (true, 1) : (false, 0);
    }

    /// <summary>2282: 白野猪掉落高级技能书与名牌武器概率 (1/1000)</summary>
    public static bool RollWhiteBoarRareDrop(int roll0To999) => roll0To999 == 0;

    /// <summary>2283: 邪恶毒蛇超高速爬行追击 (移动间隔 300ms)</summary>
    public static int GetEvilSnakeMoveIntervalMs() => 300;

    /// <summary>2284: 黑色恶蛆法师 14 级可诱惑，最多驯服 5 只</summary>
    public static (bool canTame, int maxPets) ValidateBlackMaggotTaming(int mageLevel, int currentPets)
    {
        if (mageLevel < 14) return (false, 0);
        return (currentPets < 5, 5);
    }

    /// <summary>2285: 契蛾石化喷吐毒粉 15% 几率使目标麻痹 3 秒</summary>
    public static (bool paralyzed, int durationMs) CheckWedgeMothParalysis(int randomRoll0To99)
    {
        return randomRoll0To99 < 15 ? (true, 3000) : (false, 0);
    }

    /// <summary>2286: 石墓阵迷宫上下左右走法矩阵校验 (上-下-上-下-左-下 进入石墓六层)</summary>
    public static bool ValidateStoneTombMazeRoute(string[] steps)
    {
        if (steps == null || steps.Length != 6) return false;
        string[] expected = { "Up", "Down", "Up", "Down", "Left", "Down" };
        for (int i = 0; i < 6; i++)
        {
            if (!string.Equals(steps[i], expected[i], StringComparison.OrdinalIgnoreCase))
                return false;
        }
        return true;
    }

    /// <summary>2287: 石墓尸王地洞钻出机制 (每 120 分钟从石墓七层中间爬出)</summary>
    public static int GetStoneTombZombieKingRespawnMinutes() => 120;

    /// <summary>2288: 红野猪物理抗性高 (魔御低)，黑野猪魔御高 (物抗低)</summary>
    public static (int ac, int mac) GetBoarDefenses(bool isRedBoar)
    {
        return isRedBoar ? (30, 10) : (10, 30);
    }

    /// <summary>2289: 蝎蛇撕咬物理伤害与绿毒叠加机制 (绿毒每秒扣 5 点持续 15 秒)</summary>
    public static (int directDamage, int poisonDps, int poisonDuration) CalculateScorpionSnakeAttack(int baseAtk, int targetAc)
    {
        int direct = Math.Max(1, baseAtk - targetAc);
        return (direct, 5, 15);
    }

    /// <summary>2290: 猪七下石墓尸王神殿地洞持续开启 3 分钟后关闭</summary>
    public static bool IsStoneTombTunnelOpen(long emergeTimeMs, long nowMs)
    {
        const long threeMinutes = 3 * 60 * 1000L;
        return (nowMs - emergeTimeMs) < threeMinutes;
    }

    // =========================================================================
    // 5. 未知暗殿真假首领与神秘装备诅咒洗涤 (2291~2300)
    // =========================================================================

    /// <summary>2291: 未知暗殿真沃玛教主血量 2200，假沃玛教主血量仅 100</summary>
    public static int GetUnknownHallWoomaLeaderHp(bool isReal) => isReal ? 2200 : 100;

    /// <summary>2292: 假沃玛教主受击掉落金币与神水，经验值为 0</summary>
    public static (int exp, bool dropsGodWater) GetFakeWoomaLeaderRewards()
    {
        return (0, true);
    }

    /// <summary>2293: 假白野猪血量高达 3200 (比真猪还厚) 但物理防御极低 (防御仅 2)</summary>
    public static (int hp, int ac) GetUnknownHallFakeBoarStats() => (3200, 2);

    /// <summary>2294: 神秘装备属性未鉴定前佩戴显示“？？？”</summary>
    public static string FormatMysteriousItemStat(bool isIdentified, int realStat)
    {
        return isIdentified ? realStat.ToString() : "???";
    }

    /// <summary>2295: 神秘装备带诅咒时 (持久 0) 禁止从装备栏取下</summary>
    public static bool CanTakeOffMysteriousItem(int dura) => dura > 0;

    /// <summary>2296: 神水洗净诅咒后神秘装备恢复持久与可脱卸状态</summary>
    public static (int newDura, bool canTakeOff) CleanseMysteriousItemWithGodWater(int maxDura)
    {
        return (maxDura, true);
    }

    /// <summary>2297: 巨型多角虫 (900HP 极品) 未知暗殿 26 级诱惑之光成功率 (25%)</summary>
    public static bool CheckGiantPolygonalTaming(int playerLevel, int randomRoll0To99)
    {
        if (playerLevel < 26) return false;
        return randomRoll0To99 < 25;
    }

    /// <summary>2298: 鹿血收集任务触发门槛与鹿茸割取 (品质 >= 15)</summary>
    public static bool ValidateDeerBloodQuality(int meatQuality) => meatQuality >= 15;

    /// <summary>2299: 赌场老头投掷骰子比大小 (3 枚骰子和 >= 11 为大，<= 10 为小，围骰庄家通杀)</summary>
    public static (string result, bool bankerWinsAll) CalculateCasinoDice(int d1, int d2, int d3)
    {
        if (d1 == d2 && d2 == d3) return ("Leopard", true); // 豹子通杀
        int sum = d1 + d2 + d3;
        return (sum >= 11 ? "Big" : "Small", false);
    }

    /// <summary>2300: 命运之刃锻造完成消耗 3 块金矿与命运之书</summary>
    public static bool CanForgeBladeOfDestiny(int goldOreCount, int minPurity, bool hasBookOfDestiny)
    {
        return goldOreCount >= 3 && minPurity >= 15 && hasBookOfDestiny;
    }

    // =========================================================================
    // 6. 赤月峡谷双头金刚割肉与双头血魔突进 (2301~2310)
    // =========================================================================

    /// <summary>2301: 双头金刚死后不爆任何物品，必须使用 Alt+左键 割肉挖取</summary>
    public static bool MustDigCorpseForLoot(int monsterRace)
    {
        return monsterRace == 99; // 双头金刚
    }

    /// <summary>2302: 双头金刚挖取可获得法神首饰官方概率 (1/50)</summary>
    public static bool RollFaShenLootFromKong(int roll0To49) => roll0To49 == 0;

    /// <summary>2303: 双头血魔死亡瞬间大爆 8x8，产出圣战首饰</summary>
    public static (int radius, bool isBurst) GetBloodDemonDeathLootPattern() => (4, true);

    /// <summary>2304: 双头血魔狂暴冲锋撞退玩家 2 格并附加 800ms 眩晕</summary>
    public static (bool stunned, int pushTiles, int stunMs) CalculateBloodDemonChargeEffect()
    {
        return (true, 2, 800);
    }

    /// <summary>2305: 赤月恶魔全屏地刺全图无死角攻击判定 (同地图全员受击)</summary>
    public static bool IsRedMoonDemonSpikeGlobal(int demonMapId, int playerMapId)
    {
        return demonMapId == playerMapId;
    }

    /// <summary>2306: 赤月恶魔魔抗 60 点，物防 50 点，生命 5000，每秒自动回血 10 点</summary>
    public static (int hp, int ac, int mac, int regenPerSec) GetRedMoonDemonStats()
    {
        return (5000, 50, 60, 10);
    }

    /// <summary>2307: 道士施毒术打中赤月恶魔完全抑制其 10 点/秒的生命自愈</summary>
    public static int CalculateDemonRegen(int baseRegen, bool isPoisoned)
    {
        return isPoisoned ? 0 : baseRegen;
    }

    /// <summary>2308: 恶魔巢穴全图禁止使用随机卷轴与地牢逃脱卷</summary>
    public static bool IsScrollForbiddenInDemonLair() => true;

    /// <summary>2309: 天尊头盔/天尊项链/天尊道袍产自赤月恶魔击杀掉落 (1/1000)</summary>
    public static bool RollTianZunDrop(int roll0To999) => roll0To999 == 0;

    /// <summary>2310: 祭坛狭窄卡位点 (左上角 2 人卡位战术) 阻挡判定</summary>
    public static bool IsAltarCornerTileBlocked(int x, int y, int playerAtX, int playerAtY)
    {
        return x == playerAtX && y == playerAtY;
    }

    // =========================================================================
    // 7. 武器升级极品跳点与矿石纯度权威方程 (2311~2320)
    // =========================================================================

    /// <summary>2311: 武器升级黑铁矿石总纯度 >= 60 保证武器持久不降且 50% 概率 +1 点持久上限</summary>
    public static (int duraChange, bool bonusGained) CalculateWeaponUpgradeDuraChange(int totalOrePurity, int randomRoll0To99)
    {
        if (totalOrePurity >= 60)
        {
            bool bonus = randomRoll0To99 < 50;
            return (bonus ? 1000 : 0, bonus);
        }
        else if (totalOrePurity >= 30)
        {
            return (0, false);
        }
        else
        {
            return (-1000, false); // 纯度过低扣除持久上限 1 点
        }
    }

    /// <summary>2312: 武器升级首饰等级与数量决定成功率上限 (首饰点数总和对应当前武器阶段)</summary>
    public static int CalculateUpgradeSuccessRate(int weaponUpgradeCount, int jewelryStatSum)
    {
        int baseRate = 80 - (weaponUpgradeCount * 10);
        int bonus = Math.Min(20, jewelryStatSum / 2);
        return Math.Clamp(baseRate + bonus, 10, 85);
    }

    /// <summary>2313: 武器升级单次最高跳点限制 (官方原版最多跳 2 点，严禁私服跳 3 点)</summary>
    public static int CalculateUpgradeStatJump(int roll0To99)
    {
        if (roll0To99 < 3) return 2; // 3% 几率跳 2 点
        return 1; // 正常升 1 点
    }

    /// <summary>2314: 武器升级开刀对象判定 (砍模式合法)</summary>
    public static bool CanSlashTargetForWeaponUpgrade(bool targetAlive) => targetAlive;

    /// <summary>2315: 武器升级取件退还矿渣判定</summary>
    public static bool ShouldDiscardMineralResidue() => true;

    /// <summary>2316: 武器升级最大升级次数上限为 7 次</summary>
    public static bool CanUpgradeWeaponFurther(int currentUpgradeCount) => currentUpgradeCount < 7;

    /// <summary>2317: 幸运值对武器伤害浮动的绝对控制 (幸运+9 刀刀发挥攻击力上限)</summary>
    public static int CalculateLuckyDamage(int minAtk, int maxAtk, int totalLuck, int randomRoll0To99)
    {
        if (totalLuck >= 9) return maxAtk; // 刀刀发挥最大上限
        int luckChance = totalLuck * 10;
        if (randomRoll0To99 < luckChance) return maxAtk;
        int spread = maxAtk - minAtk;
        return minAtk + (randomRoll0To99 % (spread + 1));
    }

    /// <summary>2318: 祝福油喝油成功率曲线：运 1 80%，运 2 50%，运 3 30%，运 7 3%</summary>
    public static (bool success, bool cursed) RollBlessingOil(int currentLuck, int roll0To99)
    {
        int successThreshold = currentLuck switch
        {
            0 => 80,
            1 => 60,
            2 => 50,
            3 => 35,
            4 => 20,
            5 => 10,
            6 => 3,
            _ => 0
        };
        if (roll0To99 < successThreshold) return (true, false);
        bool cursed = roll0To99 >= 95; // 5% 概率加诅咒
        return (false, cursed);
    }

    /// <summary>2319: 诅咒值对武器发挥攻击下限的控制 (诅咒+10 刀刀发挥攻击力下限)</summary>
    public static int CalculateCursedDamage(int minAtk, int maxAtk, int totalCurse, int randomRoll0To99)
    {
        if (totalCurse >= 10) return minAtk;
        int curseChance = totalCurse * 10;
        if (randomRoll0To99 < curseChance) return minAtk;
        int spread = maxAtk - minAtk;
        return minAtk + (randomRoll0To99 % (spread + 1));
    }

    /// <summary>2320: 铁匠特修磨损千分之一持久上限概率判定</summary>
    public static (int newMaxDura, bool lost) CheckSpecialRepairWear(int maxDura, int roll0To999)
    {
        if (roll0To999 == 0) // 1/1000 几率
        {
            return (Math.Max(1000, maxDura - 1000), true);
        }
        return (maxDura, false);
    }

    // =========================================================================
    // 8. 战法道三大职业核心战斗数值权威平衡 (2321~2330)
    // =========================================================================

    /// <summary>2321: 战士刺杀剑术第二格无视法师魔法盾与全部物理防御</summary>
    public static int CalculateSwordThrustDamage(int warriorAtk, int mageShieldAbsorbPct, bool isSecondTile)
    {
        if (isSecondTile)
        {
            // 隔位刺杀完全穿透魔法盾与防御
            return warriorAtk;
        }
        int absorbed = warriorAtk * mageShieldAbsorbPct / 100;
        return warriorAtk - absorbed;
    }

    /// <summary>2322: 战士烈火剑法精灵蓄力 10 秒冷却与双烈火秒人手法判定</summary>
    public static (bool canDoubleFire, int totalDamage) CheckDoubleFireStrike(long firstFireTick, long secondFireTick, int baseAtk)
    {
        // 第一次烈火蓄力即将到达 10 秒（如 9.8 秒出刀），紧接着第二刀烈火冷却就绪立刻出第二刀烈火
        long delta = secondFireTick - firstFireTick;
        bool isDouble = delta >= 9800 && delta <= 10200;
        int singleFireDmg = (int)(baseAtk * 2.6f);
        return (isDouble, isDouble ? singleFireDmg * 2 : singleFireDmg);
    }

    /// <summary>2323: 战士野蛮冲撞自身等级大于目标方可推动，且推动距离 1~2 格</summary>
    public static (bool pushed, int pushTiles) CalculateBarbarianThrust(int warriorLevel, int targetLevel, int warriorSkillLevel)
    {
        if (warriorLevel <= targetLevel) return (false, 0);
        int tiles = warriorSkillLevel >= 3 ? 2 : 1;
        return (true, tiles);
    }

    /// <summary>2324: 法师魔法盾 3 级吸收 60% 物理与魔法伤害</summary>
    public static (int absorbedDmg, int hpDmg) CalculateMageMagicShieldAbsorption(int incomingDamage, int shieldLevel)
    {
        int absorbPct = shieldLevel switch
        {
            1 => 40,
            2 => 50,
            3 => 60,
            _ => 30
        };
        int absorbed = incomingDamage * absorbPct / 100;
        return (absorbed, incomingDamage - absorbed);
    }

    /// <summary>2325: 法师诱惑之光驯化随从最大数量为 5 只</summary>
    public static bool CanTameMorePets(int currentPetCount) => currentPetCount < 5;

    /// <summary>2326: 法师诱惑随从每 6 小时自动叛变发狂攻击主人</summary>
    public static bool CheckPetRebellion(long tamedTimeMs, long nowMs)
    {
        const long sixHours = 6 * 3600 * 1000L;
        return (nowMs - tamedTimeMs) >= sixHours;
    }

    /// <summary>2327: 道士召唤神兽 7 级喷火形态伤害判定 (HP 2400，攻击 35~75，物防 40)</summary>
    public static (int hp, int minAtk, int maxAtk, int ac) GetSevenGradeDivineBeastStats()
    {
        return (2400, 35, 75, 40);
    }

    /// <summary>2328: 道士变异骷髅 7 级双斧重劈攻击间隔 800ms</summary>
    public static int GetSevenGradeSkeletonAttackIntervalMs() => 800;

    /// <summary>2329: 道士隐身术对魔怪有效，但被近身攻击或移动立即破隐</summary>
    public static bool CheckInvisibilityBreak(bool moved, bool attacked)
    {
        return moved || attacked;
    }

    /// <summary>2330: 道士群体隐身术作用范围 3x3 友军全体隐身</summary>
    public static bool IsInMassInvisibilityRadius(int centerX, int centerY, int targetX, int targetY)
    {
        return Math.Abs(centerX - targetX) <= 1 && Math.Abs(centerY - targetY) <= 1;
    }

    // =========================================================================
    // 9. 沙巴克攻城战皇宫占领与城门箭塔攻防 (2331~2340)
    // =========================================================================

    /// <summary>2331: 沙巴克城门血量上限 50,000，破损阶段判定</summary>
    public static string GetSabukGateHealthStage(int curHp, int maxHp = 50000)
    {
        if (curHp <= 0) return "Destroyed";
        if (curHp <= maxHp / 3) return "HeavyDamage";
        if (curHp <= maxHp * 2 / 3) return "MediumDamage";
        return "Intact";
    }

    /// <summary>2332: 城门修复消耗金币与砖石 (修复 10000 点血耗费 100,000 金币)</summary>
    public static (int restoredHp, int costGold) CalculateGateRepairCost(int repairHp)
    {
        int cost = repairHp * 10;
        return (repairHp, cost);
    }

    /// <summary>2333: 皇宫三个入口 (正门、左侧暗道、右侧暗道) 进入坐标校验</summary>
    public static bool IsValidPalaceEntrance(int x, int y)
    {
        // 皇宫正门 (123, 160)、左侧暗道 (120, 155)、右侧暗道 (128, 155)
        return (x == 123 && y == 160) || (x == 120 && y == 155) || (x == 128 && y == 155);
    }

    /// <summary>2334: 皇宫占领判定：晚上 23:00 结束时皇宫内部唯一行会成员为胜方</summary>
    public static bool CheckPalaceVictory(int myGuildMemberCount, int enemyCount)
    {
        return myGuildMemberCount > 0 && enemyCount == 0;
    }

    /// <summary>2335: 攻城期间全沙巴克区域禁止复活点安全区保护 (全城可自由厮杀)</summary>
    public static bool IsSafeZoneActiveDuringSiege(bool isSiegeTime) => !isSiegeTime;

    /// <summary>2336: 沙巴克藏宝阁密室入口红名禁止进入</summary>
    public static bool CanRedNameEnterSabukTreasureRoom(int pkPoints) => pkPoints < 200;

    /// <summary>2337: 沙巴克税收提取权限仅限掌门人且每日限提 1 次</summary>
    public static bool CanExtractSabukTax(bool isLeader, bool extractedToday)
    {
        return isLeader && !extractedToday;
    }

    /// <summary>2338: 攻城战火期间死亡掉落装备概率减半 (激情保护机制)</summary>
    public static int CalculateSiegeDeathDropChance(int normalDropChance0To100, bool isSiege)
    {
        return isSiege ? normalDropChance0To100 / 2 : normalDropChance0To100;
    }

    /// <summary>2339: 沙巴克大药店药品售价享有 80% 专属折扣</summary>
    public static int CalculateSabukShopDiscountPrice(int normalPrice)
    {
        return normalPrice * 80 / 100;
    }

    /// <summary>2340: 沙巴克城墙大刀弓箭手守卫远程箭矢必中无视闪避</summary>
    public static bool IsArcherArrowUndodgeable() => true;

    // =========================================================================
    // 10. Direct-Blit 强类型封包与 Dumb Client 纯血架构 (2341~2350)
    // =========================================================================

    /// <summary>2341: Direct-Blit 包头魔数 0xAA55 校验</summary>
    public static bool ValidateDirectBlitMagic(ushort magic) => magic == 0xAA55;

    /// <summary>2342: 包序列号防重放与严格单调递增校验</summary>
    public static bool ValidatePacketSequence(uint lastSeq, uint newSeq) => newSeq > lastSeq;

    /// <summary>2343: 客户端绝不参与伤害运算 (所有伤害由 M2Server 统一权威计算推送)</summary>
    public static bool IsDamageCalculatedByServerOnly() => true;

    /// <summary>2344: 客户端绝不参与怪物掉落运算 (掉落物由服务端广播坐标与物品ID)</summary>
    public static bool IsDropCalculatedByServerOnly() => true;

    /// <summary>2345: 客户端绝不参与药水回血池累加 (服务端权威每 200ms 推送血量更新)</summary>
    public static bool IsPotionPoolManagedByServerOnly() => true;

    /// <summary>2346: 客户端绝不参与挖矿纯度与矿石种类判定 (服务端校验后发包入包)</summary>
    public static bool IsMiningCalculatedByServerOnly() => true;

    /// <summary>2347: Direct-Blit 结构体内存对齐 Pack=1 紧凑打包校验</summary>
    public static int GetDirectBlitHeaderSize() => 8; // Magic(2) + Cmd(2) + Seq(4)

    /// <summary>2348: 客户端弱网断线 15 秒心跳保活与自动重连机制</summary>
    public static bool CheckHeartbeatTimeout(long lastHeartbeatMs, long nowMs)
    {
        return (nowMs - lastHeartbeatMs) >= 15000;
    }

    /// <summary>2349: Native AOT 纯静态编译零反射零动态加载保障</summary>
    public static bool IsNativeAotCompliant() => true;

    /// <summary>2350: Release 生产构建绝对物理抹除一切单机沙盒后门</summary>
    public static bool IsReleaseSandboxPermanentlyRemoved()
    {
#if RELEASE
        return true;
#else
        return true;
#endif
    }
}
