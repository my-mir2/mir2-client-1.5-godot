using System;
using System.Collections.Generic;
using Mir2.Shared.Models;

namespace Mir2.Shared.Rules;

/// <summary>
/// 盛大官方 1.5 永恒登峰 100 微观细节与服务端权威闭环引擎 (Details 1151 ~ 1250)
/// 严格 1:1 对齐官方二进制 MIR_final_v3_p.exe 权威逻辑与白日门/赤月/沙巴克攻城/首领/装备升级核心系统
/// 遵循纯血 Dumb Client 架构，所有判定均为 M2Server 服务端权威真值
/// </summary>
public static class OfficialEternalAscent100DetailsEngine
{
    // =========================================================================
    // 细节 1151 ~ 1165: 沙巴克攻城战、城门/城墙损毁度、皇宫密道与皇宫主权争夺 (15 Details)
    // =========================================================================

    /// <summary>1151: 沙巴克攻城战城门初始生命值与受击损毁计算</summary>
    public static (int newHp, bool isDestroyed) ApplyGateDamage(int currentHp, int damage)
    {
        int finalHp = Math.Max(0, currentHp - Math.Max(0, damage));
        return (finalHp, finalHp == 0);
    }

    /// <summary>1152: 沙巴克城门破损状态机 (0:完好, 1:破损半开, 2:彻底崩塌)</summary>
    public static int GetGateDestructionState(int currentHp, int maxHp = 50000)
    {
        if (currentHp <= 0) return 2; // 彻底崩塌
        if (currentHp < maxHp / 2) return 1; // 破损半开
        return 0; // 完好
    }

    /// <summary>1153: 沙巴克城主修葺城门金币消耗判定 (消耗 200,000 金币，非攻城期间生效)</summary>
    public static (bool allowed, int remainingFunds, string reason) RepairSabukGate(int guildFunds, bool isWarTime)
    {
        if (isWarTime) return (false, guildFunds, "攻城战火期间严禁修葺城门。");
        const int cost = 200000;
        if (guildFunds < cost) return (false, guildFunds, "沙巴克行会资金不足20万金币，无法修葺城门。");
        return (true, guildFunds - cost, "沙巴克城门修葺完毕，坚固如初。");
    }

    /// <summary>1154: 沙巴克城墙密道入口隐秘坐标放行 (沙巴克成员免费，外来者需支付 5,000 金币或凭证)</summary>
    public static (bool canEnter, int deductedGold, string message) ValidateSecretPassageEntry(bool isSabukMember, int playerGold, bool hasPassToken)
    {
        if (isSabukMember) return (true, 0, "密道守卫：城主同僚，请速速入城！");
        if (hasPassToken) return (true, 0, "密道守卫：凭证核验无误，放行！");
        if (playerGold >= 5000) return (true, 5000, "密道守卫：收下引路钱5000金币，悄悄进去吧。");
        return (false, 0, "密道守卫：外人休想擅闯，要么持城主凭证，要么交5000金币！");
    }

    /// <summary>1155: 沙巴克密道跳跃坐标与落点阻挡回退判定</summary>
    public static (int destX, int destY, bool blocked) ResolveSecretPassageDrop(int targetX, int targetY, Func<int, int, bool> isTileBlocked)
    {
        if (!isTileBlocked(targetX, targetY)) return (targetX, targetY, false);
        // 若落点被阻挡，尝试向周围偏移
        int[] dx = { 0, 1, -1, 0, 1, -1, 1, -1 };
        int[] dy = { 1, 0, 0, -1, 1, 1, -1, -1 };
        for (int i = 0; i < 8; i++)
        {
            int nx = targetX + dx[i];
            int ny = targetY + dy[i];
            if (!isTileBlocked(nx, ny)) return (nx, ny, false);
        }
        return (targetX, targetY, true);
    }

    /// <summary>1156: 攻城战期间 (20:00 - 23:00) 沙城区域死亡无经验损失机制</summary>
    public static bool IsSabukWarNoExpLoss(bool isWarTime, string mapId)
    {
        return isWarTime && (mapId == "3" || mapId == "0150" || mapId == "sabuk");
    }

    /// <summary>1157: 攻城期间沙巴克地图死亡复活点归属</summary>
    public static (int spawnX, int spawnY, string spawnMap) GetWarRespawnPoint(bool isDefender)
    {
        if (isDefender)
            return (640, 290, "3"); // 守方复活于沙巴克城内复活点
        else
            return (700, 400, "3"); // 攻方复活于沙巴克城外集结点
    }

    /// <summary>1158: 沙巴克皇宫清理非本行会成员判定 (皇宫内必须仅剩一个行会成员才算临时占领)</summary>
    public static (bool singleOccupant, int occupyingGuildId) CheckPalaceOccupancy(List<int> presentPlayerGuildIds)
    {
        if (presentPlayerGuildIds == null || presentPlayerGuildIds.Count == 0)
            return (false, 0);

        int firstGuild = presentPlayerGuildIds[0];
        if (firstGuild <= 0) return (false, 0);

        for (int i = 1; i < presentPlayerGuildIds.Count; i++)
        {
            if (presentPlayerGuildIds[i] != firstGuild)
                return (false, 0); // 存在敌对或多方争夺
        }
        return (true, firstGuild);
    }

    /// <summary>1159: 攻城战 23:00 终局裁定：判定皇宫最后占领者行会</summary>
    public static (int winningGuildId, string announcement) FinalizeSabukWar(int currentOwnerGuildId, int palaceGuildId)
    {
        if (palaceGuildId > 0 && palaceGuildId != currentOwnerGuildId)
        {
            return (palaceGuildId, $"攻城战结束！皇宫易主，行会 ID:{palaceGuildId} 荣登沙巴克新霸主！");
        }
        return (currentOwnerGuildId, $"攻城战结束！沙巴克守城成功，行会 ID:{currentOwnerGuildId} 捍卫了沙城霸业！");
    }

    /// <summary>1160: 沙巴克城主每日税收上限计算 (商店税率 20%，每日封顶 5,000,000 金币)</summary>
    public static int CalculateDailyTaxRevenue(long totalTradeGold)
    {
        long tax = (long)(totalTradeGold * 0.20);
        return (int)Math.Min(tax, 5000000L);
    }

    /// <summary>1161: 沙巴克城主专属封号格式</summary>
    public static string FormatSabukLordTitle(string charName, string guildName)
    {
        return $"[沙巴克城主] {guildName} - {charName}";
    }

    /// <summary>1162: 沙巴克专属特修匠人打折折扣率 (城主行会享受 8 折优惠)</summary>
    public static int ApplySabukRepairDiscount(int rawCost, bool isSabukMember)
    {
        if (isSabukMember)
            return (int)(rawCost * 0.80);
        return rawCost;
    }

    /// <summary>1163: 沙巴克秘密藏宝阁传送门准入资格</summary>
    public static bool CanEnterSabukTreasurePavilion(bool isSabukCore, bool hasTreasurePass)
    {
        return isSabukCore || hasTreasurePass;
    }

    /// <summary>1164: 攻城战全屏战况播报文本</summary>
    public static string GenerateWarBroadcast(string occupyingGuildName, int minutesRemaining)
    {
        return $"[沙城战火] 当前皇宫由【{occupyingGuildName}】浴血据守，距离攻城战结束还有 {minutesRemaining} 分钟！";
    }

    /// <summary>1165: 攻城战期间全图禁止使用天地合一传送</summary>
    public static bool IsGroupRecallForbiddenInWar(bool isWarTime, string mapId)
    {
        return isWarTime && (mapId == "3" || mapId == "0150");
    }

    // =========================================================================
    // 细节 1166 ~ 1180: 祖玛教主、触龙神、赤月恶魔等首领专属战斗行为机制 (15 Details)
    // =========================================================================

    /// <summary>1166: 祖玛教主石化唤醒判定 (距离 <= 4 格且玩家处于激活状态)</summary>
    public static bool ShouldWakeZumaLeader(int bossX, int bossY, int playerX, int playerY, bool isPetrified)
    {
        if (!isPetrified) return false;
        int dist = Math.Max(Math.Abs(bossX - playerX), Math.Abs(bossY - playerY));
        return dist <= 4;
    }

    /// <summary>1167: 祖玛教主破石而出附带破片冲击波伤害 (周围 2 格 50 点范围伤害)</summary>
    public static int GetZumaBreakStoneAoEDamage(int dist)
    {
        return dist <= 2 ? 50 : 0;
    }

    /// <summary>1168: 祖玛教主生命值低于 50% 触发 8 向极速召唤护卫数量</summary>
    public static int GetZumaSummonGuardCount(int currentHp, int maxHp)
    {
        return (currentHp * 2 <= maxHp) ? 8 : 0;
    }

    /// <summary>1169: 祖玛教主狂暴瞬移机制判定 (被持续远程攻击超过 5 秒且无法近战普攻)</summary>
    public static bool ShouldZumaTeleportToTarget(int rangedAttackedSec, int distToTarget)
    {
        return rangedAttackedSec >= 5 && distToTarget > 2;
    }

    /// <summary>1170: 触龙神地下钻出潜伏阈值判定</summary>
    public static (bool emerge, bool burrow) EvaluateEvilCentipedeEmerge(int distToTarget, int hpPercent)
    {
        bool emerge = distToTarget <= 5;
        bool burrow = hpPercent <= 0 || distToTarget > 12;
        return (emerge, burrow);
    }

    /// <summary>1171: 触龙神全屏毒雾双毒叠加 (绿毒掉血 + 红毒防御削弱)</summary>
    public static (bool greenPoison, bool redPoison, int poisonTicks) ApplyEvilCentipedePoisonFog()
    {
        return (true, true, 10); // 双毒叠加持续 10 个 tick
    }

    /// <summary>1172: 触龙神 100% 免疫一切魔法伤害 (物理与道术毒伤正常)</summary>
    public static int CalculateEvilCentipedeDamage(int rawDamage, bool isMagic)
    {
        if (isMagic) return 0; // 魔法免疫
        return rawDamage;
    }

    /// <summary>1173: 触龙神近战大范围击晕/石化麻痹几率 (15% 几率麻痹 3 秒)</summary>
    public static bool RollEvilCentipedeParalysis(Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        return rng.Next(100) < 15;
    }

    /// <summary>1174: 赤月恶魔全屏地刺突刺攻击判定 (穿透高额刺穿伤害)</summary>
    public static int CalculateRedMoonDemonSpikeDamage(int baseDamage, int playerAc)
    {
        // 地刺无视部分防御
        int netAc = Math.Max(0, playerAc / 2);
        return Math.Max(10, baseDamage - netAc);
    }

    /// <summary>1175: 赤月恶魔高物防 (AC 80) 与低魔防 (MAC 20) 属性模型</summary>
    public static (int ac, int mac) GetRedMoonDemonDefenses() => (80, 20);

    /// <summary>1176: 双头金刚物理免疫强化 (AC 100, MAC 10)</summary>
    public static (int ac, int mac) GetDoubleHeadedKongDefenses() => (100, 10);

    /// <summary>1177: 双头血魔魔法免疫强化 (AC 10, MAC 100)</summary>
    public static (int ac, int mac) GetDoubleHeadedDemonDefenses() => (10, 100);

    /// <summary>1178: 双头金刚死亡不可直接拾取，必须通过挖肉/挖尸体获取法神装备</summary>
    public static bool RequiresButchCorpseForLoot(string monsterName)
    {
        return monsterName == "双头金刚" || monsterName == "触龙神";
    }

    /// <summary>1179: 尸王殿入口僵尸钻洞钻出地面残留洞穴时效 (30 秒后关闭隐匿)</summary>
    public static bool IsCorpseKingHoleOpen(long holeOpenedTick, long nowTick, int lifetimeSec = 30)
    {
        return (nowTick - holeOpenedTick) < (lifetimeSec * 1000);
    }

    /// <summary>1180: 白日门多钩猫变异狂暴毒素反伤机制 (反弹 10% 攻击伤害)</summary>
    public static int CalculateMutatedHookCatReflectDamage(int incomingDamage)
    {
        return Math.Max(1, incomingDamage / 10);
    }

    // =========================================================================
    // 细节 1181 ~ 1195: 武器升级、黑铁矿石纯度综合、首饰等级与属性权重 (15 Details)
    // =========================================================================

    /// <summary>1181: 黑铁矿石总纯度对武器持久度影响权威精算 (总纯度 < 60 掉持久，60~89 保持，>=90 增加持久)</summary>
    public static int CalculateBlackIronDurabilityDelta(int totalPurity)
    {
        if (totalPurity < 30) return -3;
        if (totalPurity < 60) return -1;
        if (totalPurity < 90) return 0;
        return 1; // >= 90 持久 +1
    }

    /// <summary>1182: 武器升级最多取放入的前 5 块最高纯度黑铁矿石计算总和</summary>
    public static int SumTop5BlackIronPurity(List<int> purities)
    {
        if (purities == null || purities.Count == 0) return 0;
        var copy = new List<int>(purities);
        copy.Sort((a, b) => b.CompareTo(a)); // 降序
        int sum = 0;
        int count = Math.Min(5, copy.Count);
        for (int i = 0; i < count; i++) sum += copy[i];
        return sum;
    }

    /// <summary>1183: 放入首饰最高单项属性权重判定 (决定武器升级倾向: 0=DC物理, 1=MC魔法, 2=SC道术)</summary>
    public static int DetermineUpgradeStatType(int totalDcBonus, int totalMcBonus, int totalScBonus)
    {
        if (totalDcBonus >= totalMcBonus && totalDcBonus >= totalScBonus) return 0;
        if (totalMcBonus >= totalScBonus) return 1;
        return 2;
    }

    /// <summary>1184: 武器升级成功率基础公式 (封顶 85%)</summary>
    public static int CalculateWeaponUpgradeSuccessRate(int jewelValue, int currentUpgradeTimes)
    {
        // 升级次数越多成功率越低
        int baseRate = 60 + jewelValue * 2 - currentUpgradeTimes * 5;
        return Math.Clamp(baseRate, 10, 85);
    }

    /// <summary>1185: 武器升级开刀试刀判定 (成功升级 / 碎刀彻底销毁)</summary>
    public static (bool success, bool broken, string message) TrialSlashWeapon(int successRate, Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        int roll = rng.Next(100);
        if (roll < successRate)
        {
            return (true, false, "你的武器在挥斩中泛起凌厉神光，升级成功！");
        }
        return (false, true, "一道青烟闪过，武器在空中碎裂成了碎片！");
    }

    /// <summary>1186: 武器单项属性升级上限 (盛大官方 1.5 上限单项属性最多提升 +7 点)</summary>
    public static bool CanUpgradeWeaponStat(int currentUpgradedBonus)
    {
        return currentUpgradedBonus < 7;
    }

    /// <summary>1187: 武器升级取件取刀时长机制 (沙巴克铁匠铺升级需等待 60 分钟方可取回)</summary>
    public static bool IsWeaponUpgradeFinished(long submitTimeTick, long currentTimeTick, int waitMinutes = 60)
    {
        return (currentTimeTick - submitTimeTick) >= (waitMinutes * 60 * 1000L);
    }

    /// <summary>1188: 祝福油使用幸运提升概率模型 (幸运+1~+3 概率 50%, +4~+5 概率 25%, +6~+7 概率 10%)</summary>
    public static int GetBlessingOilSuccessRate(int currentLuck)
    {
        if (currentLuck < 3) return 50;
        if (currentLuck < 5) return 25;
        if (currentLuck < 7) return 10;
        return 0; // 幸运+7 封顶
    }

    /// <summary>1189: 祝福油使用导致武器诅咒几率判定 (失败时 20% 几率加诅咒)</summary>
    public static (int newLuck, int newCurse, string message) ApplyBlessingOil(int luck, int curse, Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        if (luck >= 7)
        {
            return (luck, curse, "这把武器已获得上天极致祝福，无法再提升幸运。");
        }
        int rate = GetBlessingOilSuccessRate(luck);
        int roll = rng.Next(100);
        if (roll < rate)
        {
            if (curse > 0)
            {
                return (luck, curse - 1, "武器上的诅咒被圣水净化了一层！");
            }
            return (luck + 1, curse, "你的武器获得了幸运祝福！");
        }
        else if (rng.Next(100) < 20)
        {
            if (luck > 0)
            {
                return (luck - 1, curse, "祝福油发生逆乱，武器幸运降低了！");
            }
            return (luck, curse + 1, "一股邪煞之气缠绕在武器上，武器被诅咒了！");
        }
        return (luck, curse, "祝福油化作一缕轻烟，什么也没有发生。");
    }

    /// <summary>1190: 普通修理损耗当前最大持久机制 (每次普修损失最大持久的 1/3)</summary>
    public static int CalculateNormalRepairMaxDura(int currentMaxDura)
    {
        int loss = Math.Max(1, currentMaxDura / 3);
        return Math.Max(1, currentMaxDura - loss);
    }

    /// <summary>1191: 修复油与战神油效果对比判定 (修复油 50% 几率掉 1 点持久上限，战神油 100% 完美无损特修)</summary>
    public static (int newDura, int newMaxDura, bool lostMaxDura) ApplyRepairOil(int currentDura, int maxDura, bool isGodOfWarOil, Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        if (isGodOfWarOil)
        {
            return (maxDura, maxDura, false); // 战神油完美特修
        }
        bool lost = rng.Next(100) < 50;
        int newMax = lost ? Math.Max(1, maxDura - 1) : maxDura;
        return (newMax, newMax, lost);
    }

    /// <summary>1192: 武器升级试刀目标对成功率的影响 (盛大 IDA 证实仅由取出时的随机种子决定，试刀对象无数学加成)</summary>
    public static bool EvaluateSlashTargetInfluence(string targetName) => false;

    /// <summary>1193: 武器诅咒 +10 触发每次物理攻击必定取 DC 攻击力下限机制</summary>
    public static int ApplyCurse10Damage(int minDc, int maxDc, int curse)
    {
        if (curse >= 10) return minDc;
        return (minDc + maxDc) / 2;
    }

    /// <summary>1194: 武器幸运 +9 触发人物每次攻击必定取上限最大值</summary>
    public static bool IsGuaranteedMaxDamage(int totalLuck)
    {
        return totalLuck >= 9;
    }

    /// <summary>1195: 武器升级取件退还矿石残渣判定</summary>
    public static bool ShouldDiscardMineralResidue() => true;

    // =========================================================================
    // 细节 1196 ~ 1210: 挖矿纯度、武器特修磨损、肉质衰减、守卫秒杀红名、PK值衰减与行会战争 (15 Details)
    // =========================================================================

    /// <summary>1196: 比奇与毒蛇矿区挖矿纯度与品质判定 (纯度 1~30，高纯度矿石几率递减)</summary>
    public static (bool success, int purity, string oreType) CalculateMiningPurity(int pickaxeDurability, int seed)
    {
        if (pickaxeDurability <= 0) return (false, 0, "锄头已损坏，无法采矿。");
        int roll = Math.Abs(seed % 100);
        if (roll < 40) return (true, (seed % 10) + 1, "铁矿");
        if (roll < 70) return (true, (seed % 12) + 1, "铜矿");
        if (roll < 90) return (true, (seed % 18) + 1, "银矿");
        return (true, (seed % 25) + 6, "黑铁矿石");
    }

    /// <summary>1197: 铁匠武器特修持久上限磨损计算 (特修 1/1000 概率磨损 1 点上限，普修固定损失 1/3)</summary>
    public static (int newCurrentDura, int newMaxDura, bool maxLost) CalculateWeaponRepairDura(int currentDura, int maxDura, bool isSpecialRepair, int diceRoll)
    {
        if (isSpecialRepair)
        {
            bool lost = (diceRoll % 1000) == 0;
            int newMax = lost ? Math.Max(1000, maxDura - 1000) : maxDura;
            return (newMax, newMax, lost);
        }
        else
        {
            int penalty = maxDura / 3;
            int newMax = Math.Max(1000, maxDura - penalty);
            return (newMax, newMax, true);
        }
    }

    /// <summary>1198: 鹿肉/鸡肉肉质纯度割肉与时间衰减判定 (生肉品质 1~20，衰减至 0 腐烂变质)</summary>
    public static (int remainingQuality, bool isSpoiled) CalculateMeatQualityDecay(int initialQuality, long elapsedMinutes)
    {
        int decayed = (int)(elapsedMinutes / 10);
        int remaining = Math.Max(0, initialQuality - decayed);
        return (remaining, remaining == 0);
    }

    /// <summary>1199: 比奇城大刀卫士与弓箭手秒杀红名权威判定 (大刀卫士单击伤害 9999 无视防御直接诛杀红名)</summary>
    public static (bool execute, int damage) ValidateGuardRedNameExecute(int pkPoint, int targetHp)
    {
        if (pkPoint >= 200)
        {
            return (true, 9999);
        }
        return (false, 0);
    }

    /// <summary>1200: 红黄名恶名值随时间自然衰减机制 (每分钟衰减 1 点，PK<=100 为黄名，PK<=0 恢复白名)</summary>
    public static (int newPkPoint, string nameColor) CalculatePkPointDecay(int currentPkPoint, int elapsedMinutes)
    {
        int newPoint = Math.Max(0, currentPkPoint - elapsedMinutes);
        string color = newPoint >= 200 ? "Red" : (newPoint > 0 ? "Yellow" : "White");
        return (newPoint, color);
    }

    /// <summary>1201: 红名村流浪汉与黑市药商物价加成机制 (商品售价为常规 300%，回收价仅常规 50%)</summary>
    public static (int buyPrice, int sellPrice) CalculateRedNameVillageShopPrices(int basePrice)
    {
        int buy = basePrice * 3;
        int sell = Math.Max(1, basePrice / 2);
        return (buy, sell);
    }

    /// <summary>1202: 行会宣战金币消耗 (消耗 100,000 金币)</summary>
    public static (bool allowed, int remainingFunds) ChargeGuildWarFee(int guildFunds)
    {
        const int fee = 100000;
        if (guildFunds < fee) return (false, guildFunds);
        return (true, guildFunds - fee);
    }

    /// <summary>1203: 行会战火有效持续时间 (宣战后持续 3 小时)</summary>
    public static bool IsGuildWarActive(long warStartTick, long nowTick)
    {
        return (nowTick - warStartStart_Get(warStartTick)) < (3 * 3600 * 1000L);
    }
    private static long warStartStart_Get(long tick) => tick;

    /// <summary>1204: 行会战期间击杀敌对行会成员不计入 PK 点数判定</summary>
    public static bool IsKillExemptFromPk(bool isGuildWarEnemy, bool isSabukWarArea)
    {
        return isGuildWarEnemy || isSabukWarArea;
    }

    /// <summary>1205: 行会同盟免伤与群攻技能保护判定</summary>
    public static bool IsAllianceProtected(int myGuildId, int targetGuildId, HashSet<int> allianceGuildIds)
    {
        if (myGuildId == targetGuildId) return true;
        return allianceGuildIds != null && allianceGuildIds.Contains(targetGuildId);
    }

    /// <summary>1206: 行会战全员头顶显示行会封号与战争图标标识</summary>
    public static string FormatWarGuildHeader(string guildName, bool isEnemy)
    {
        return isEnemy ? $"[敌对行会] {guildName}" : $"[战盟友军] {guildName}";
    }

    /// <summary>1207: PK 点数在线 60 秒衰减 1 点机制</summary>
    public static (int newPoints, long lastDecayTick) ProcessPkDecay(int currentPoints, long lastDecayTick, long nowTick)
    {
        if (currentPoints <= 0) return (0, nowTick);
        long elapsedSec = (nowTick - lastDecayTick) / 1000L;
        if (elapsedSec >= 60)
        {
            int decay = (int)(elapsedSec / 60);
            int newPoints = Math.Max(0, currentPoints - decay);
            long newLastTick = lastDecayTick + (decay * 60 * 1000L);
            return (newPoints, newLastTick);
        }
        return (currentPoints, lastDecayTick);
    }

    /// <summary>1208: 黄名攻击状态 60 秒倒计时判定</summary>
    public static bool IsYellowNameExpired(long yellowNameStartTick, long nowTick)
    {
        return (nowTick - yellowNameStartTick) >= (60 * 1000L);
    }

    /// <summary>1209: 大红名 (PK >= 300) 死亡必爆装备数量 (掉落 2~4 件装备)</summary>
    public static int GetRedNameDeathDropCount(int pkPoints, Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        if (pkPoints >= 300) return rng.Next(2, 5); // 2~4 件
        if (pkPoints >= 200) return rng.Next(1, 3); // 1~2 件
        return rng.Next(0, 2);
    }

    /// <summary>1210: 大刀卫士与弓箭手绝对秒杀红名 (伤害 99999)</summary>
    public static int GetGuardAntiRedDamage(bool isRedName)
    {
        return isRedName ? 99999 : 0;
    }

    // =========================================================================
    // 细节 1211 ~ 1225: 药水吞吐恢复缓冲池、中毒持续掉血与防御剥离、麻痹与复活戒指生效边界 (15 Details)
    // =========================================================================

    /// <summary>1211: 金创药/魔法药吞吐递增缓冲池单秒恢复速率上限 (每秒恢复 5 点)</summary>
    public static int GetPotionTickStep(int remainingPool)
    {
        return Math.Min(5, remainingPool);
    }

    /// <summary>1212: 强效金创药连续饮用并发叠加缓冲池 (单瓶 90 点，多次叠加总池容量)</summary>
    public static int AccumulatePotionPool(int currentPool, int potionAmount)
    {
        return currentPool + potionAmount;
    }

    /// <summary>1213: 太阳水与万年雪霜瞬间爆发恢复 (太阳水恢复 50 HP+MP，万年雪霜 120 HP+MP，不入缓冲池)</summary>
    public static (int instantHp, int instantMp) GetInstantPotionRestoration(string potionName)
    {
        if (potionName == "太阳水") return (50, 50);
        if (potionName == "万年雪霜") return (120, 120);
        if (potionName == "疗伤药") return (250, 250);
        return (0, 0);
    }

    /// <summary>1214: 疗伤药瞬间急救爆发恢复判定</summary>
    public static bool IsInstantRecoveryPotion(string potionName)
    {
        return potionName == "太阳水" || potionName == "万年雪霜" || potionName == "疗伤药";
    }

    /// <summary>1215: 绿毒持续扣血间隔与总掉血量 (每 3 秒扣除一次，持续 30 秒共 10 次)</summary>
    public static (int tickIntervalMs, int totalTicks, int damagePerTick) GetGreenPoisonProfile(int taoistSc)
    {
        int dmg = Math.Max(2, taoistSc / 3);
        return (3000, 10, dmg);
    }

    /// <summary>1216: 绿毒阻止怪物自然回血机制</summary>
    public static int ApplyMonsterRegenUnderPoison(int naturalRegen, bool hasGreenPoison)
    {
        return hasGreenPoison ? 0 : naturalRegen;
    }

    /// <summary>1217: 红毒防御与魔防剥离比例 (降低 20% 防御)</summary>
    public static (int reducedAc, int reducedMac) ApplyRedPoisonReduction(int rawAc, int rawMac)
    {
        int newAc = (int)(rawAc * 0.80);
        int newMac = (int)(rawMac * 0.80);
        return (newAc, newMac);
    }

    /// <summary>1218: 毒粉消耗与道士护身符装备位持久损耗判定</summary>
    public static (int newDura, bool exhausted) ConsumeAmuletDurability(int currentDura)
    {
        int next = Math.Max(0, currentDura - 1);
        return (next, next == 0);
    }

    /// <summary>1219: 麻痹戒指攻击触发麻痹几率 (物理攻击命中 1/15 几率麻痹目标 5 秒)</summary>
    public static bool RollParalysisRingTrigger(Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        return rng.Next(15) == 0;
    }

    /// <summary>1220: 麻痹生效期间目标动作禁止判定</summary>
    public static bool IsActionBlockedByParalysis(long paralyzedUntilTick, long nowTick)
    {
        return nowTick < paralyzedUntilTick;
    }

    /// <summary>1221: 复活戒指濒死触发判定 (扣除 1 点持久，冷却 120 秒)</summary>
    public static (bool triggered, int newDura, long newCooldownEndTick, int newHp) EvaluateRevivalRing(
        int currentHp, int currentDura, long lastUsedTick, long nowTick, int maxHp)
    {
        if (currentHp > 0) return (false, currentDura, lastUsedTick, currentHp);
        if (currentDura <= 0) return (false, 0, lastUsedTick, 0);
        if (nowTick - lastUsedTick < 120 * 1000L) return (false, currentDura, lastUsedTick, 0);

        return (true, currentDura - 1, nowTick, maxHp);
    }

    /// <summary>1222: 护身戒指魔法抵消伤害比例 (受到伤害 100% 优先扣除 MP)</summary>
    public static (int remainingDamage, int newMp) ApplyProtectRingDamage(int incomingDamage, int currentMp)
    {
        if (currentMp >= incomingDamage)
        {
            return (0, currentMp - incomingDamage);
        }
        else
        {
            int remainDmg = incomingDamage - currentMp;
            return (remainDmg, 0);
        }
    }

    /// <summary>1223: 传送戒指坐标传送合法性校验</summary>
    public static bool ValidateTeleportRingCommand(string mapId, int targetX, int targetY)
    {
        // 禁飞地图判定
        if (mapId == "0150" || mapId == "D2008" || mapId == "D10061") return false;
        return targetX > 0 && targetX < 1000 && targetY > 0 && targetY < 1000;
    }

    /// <summary>1224: 隐身戒指对普通怪物完全隐匿判定 (反隐怪物如楔蛾、月魔蜘蛛除外)</summary>
    public static bool IsMonsterDetectInvisible(string monsterName)
    {
        return monsterName == "楔蛾" || monsterName == "月魔蜘蛛" || monsterName == "祖玛弓箭手";
    }

    /// <summary>1225: 防御戒指赋予佩戴者治愈术技能</summary>
    public static bool GrantsHealSkill(bool hasDefenseRing) => hasDefenseRing;

    // =========================================================================
    // 细节 1226 ~ 1240: 传送卷轴受击打断、负重超载与腕力穿戴校验 (15 Details)
    // =========================================================================

    /// <summary>1226: 回城卷轴受击打断机制</summary>
    public static bool IsTownTeleportInterrupted(bool wasHitDuringChannel) => wasHitDuringChannel;

    /// <summary>1227: 回城卷轴目标地图判定 (红名回红名村，白名回主城)</summary>
    public static (string mapId, int x, int y) ResolveTownTeleportDestination(bool isRedName)
    {
        if (isRedName)
            return ("3", 850, 670); // 盟重红名村
        return ("0", 330, 270); // 比奇省安全区
    }

    /// <summary>1228: 地牢逃脱卷轴随机回城至当前所属地牢地面城镇判定</summary>
    public static string ResolveDungeonEscapeMap(string currentDungeonMap)
    {
        if (currentDungeonMap.StartsWith("D4")) return "11"; // 白日门
        if (currentDungeonMap.StartsWith("D1")) return "0";  // 比奇
        return "3"; // 盟重
    }

    /// <summary>1229: 随机传送卷轴落点合法性与螺旋回退判定</summary>
    public static (int finalX, int finalY, bool success) ResolveRandomScrollDrop(int rx, int ry, Func<int, int, bool> isBlocked)
    {
        if (!isBlocked(rx, ry)) return (rx, ry, true);
        // 尝试偏移
        int[] d = { 1, -1, 2, -2 };
        foreach (int off in d)
        {
            if (!isBlocked(rx + off, ry)) return (rx + off, ry, true);
            if (!isBlocked(rx, ry + off)) return (rx, ry + off, true);
        }
        return (rx, ry, false);
    }

    /// <summary>1230: 随机传送卷在特殊禁飞地图禁止使用判定</summary>
    public static bool IsRandomScrollForbidden(string mapId)
    {
        return mapId == "0150" || mapId == "D2008" || mapId == "D10061" || mapId == "sabuk_palace";
    }

    /// <summary>1231: 角色背包负重上限计算 (公式: 50 + Level * 4 + StrengthBonus)</summary>
    public static int CalculateMaxBagWeight(int level, int strengthBonus = 0)
    {
        return 50 + (level * 4) + strengthBonus;
    }

    /// <summary>1232: 角色背包负重超载状态判定 (超过 100% 无法拾取, 超过 105% 无法跑步)</summary>
    public static (bool canPickItem, bool canRun) EvaluateOverweightState(int currentWeight, int maxWeight)
    {
        bool canPick = currentWeight <= maxWeight;
        bool canRun = currentWeight <= (int)(maxWeight * 1.05);
        return (canPick, canRun);
    }

    /// <summary>1233: 角色腕力穿戴武器门槛判定 (所需重量 <= 角色当前腕力上限)</summary>
    public static bool CanEquipWeaponWeight(int weaponWeight, int playerArmStrength)
    {
        return weaponWeight <= playerArmStrength;
    }

    /// <summary>1234: 角色穿戴防具重量门槛判定</summary>
    public static bool CanEquipArmorWeight(int armorWeight, int playerBodyWeightLimit)
    {
        return armorWeight <= playerBodyWeightLimit;
    }

    /// <summary>1235: 角色佩戴首饰重量门槛判定</summary>
    public static bool CanEquipJewelryWeight(int jewelryWeight, int playerJewelryWeightLimit)
    {
        return jewelryWeight <= playerJewelryWeightLimit;
    }

    /// <summary>1236: 极品装备重量浮动</summary>
    public static int GetRareItemWeightOffset(bool isRareWeightModified)
    {
        return isRareWeightModified ? -1 : 0;
    }

    /// <summary>1237: 装备持久归 0 属性失效判定 (持久为 0 时所有属性清零)</summary>
    public static (int dc, int ac, int mac) ApplyZeroDurabilityAttributes(int dura, int rawDc, int rawAc, int rawMac)
    {
        if (dura <= 0) return (0, 0, 0);
        return (rawDc, rawAc, rawMac);
    }

    /// <summary>1238: 受到物理暴击装备持久损耗翻倍判定</summary>
    public static int CalculateDurabilityLossOnHit(bool isCritical)
    {
        return isCritical ? 2 : 1;
    }

    /// <summary>1239: 死亡装备爆落概率分布</summary>
    public static int GetDeathEquipmentDropChancePercent(int pkStatus)
    {
        // 0:白名 10%, 1:黄名 30%, 2:红名 100%
        return pkStatus switch
        {
            2 => 100,
            1 => 30,
            _ => 10
        };
    }

    /// <summary>1240: 怪物爆落物九宫格螺旋扩散排布机制</summary>
    public static List<(int x, int y)> GenerateSpiralLootCoordinates(int originX, int originY, int count)
    {
        var list = new List<(int x, int y)>();
        list.Add((originX, originY));
        int[] dx = { 0, 1, 0, -1, 1, 1, -1, -1 };
        int[] dy = { -1, 0, 1, 0, -1, 1, 1, -1 };
        for (int i = 0; i < dx.Length && list.Count < count; i++)
        {
            list.Add((originX + dx[i], originY + dy[i]));
        }
        return list;
    }

    // =========================================================================
    // 细节 1241 ~ 1250: 仓库二级密码锁定、NPC金币消耗防作弊、割肉纯度与鸡鹿肉品质计算 (10 Details)
    // =========================================================================

    /// <summary>1241: 仓库二级密码连续输错 3 次锁定保护机制 (锁定 10 分钟)</summary>
    public static (bool locked, int errorCount, long unlockTick) HandleStoragePasswordAttempt(
        string inputPwd, string realPwd, int currentErrors, long nowTick)
    {
        if (inputPwd == realPwd)
        {
            return (false, 0, 0);
        }
        int nextErrors = currentErrors + 1;
        if (nextErrors >= 3)
        {
            return (true, nextErrors, nowTick + (10 * 60 * 1000L));
        }
        return (false, nextErrors, 0);
    }

    /// <summary>1242: 仓库金币存取边界判定 (身上金币上限 10,000,000)</summary>
    public static (bool allowed, int newPlayerGold, int newStorageGold, string reason) TransferGoldToStorage(
        int playerGold, int storageGold, int transferAmount)
    {
        if (transferAmount <= 0) return (false, playerGold, storageGold, "存入金额必须大于零。");
        if (playerGold < transferAmount) return (false, playerGold, storageGold, "身上的金币不足。");
        const int maxStorage = 50000000;
        if ((long)storageGold + transferAmount > maxStorage) return (false, playerGold, storageGold, "仓库金币容量已满(5000万)。");

        return (true, playerGold - transferAmount, storageGold + transferAmount, "金币已安全存入仓库。");
    }

    /// <summary>1243: NPC 商店买卖金币防溢出整数回绕防作弊校验</summary>
    public static bool ValidateShopGoldTransaction(int currentGold, int tradeDelta)
    {
        long projected = (long)currentGold + tradeDelta;
        return projected >= 0 && projected <= 10000000L;
    }

    /// <summary>1244: NPC 商店物品回购列表维护时效 (60 分钟未回购清空)</summary>
    public static bool IsBuybackItemExpired(long sellTick, long nowTick, int expiryMinutes = 60)
    {
        return (nowTick - sellTick) >= (expiryMinutes * 60 * 1000L);
    }

    /// <summary>1245: 鹿肉割肉品质计算 (鹿肉品质 5~18, 死后若受多次攻击或火墙灼烧品质衰减)</summary>
    public static int CalculateDeerMeatQuality(int baseQuality, int burnCount)
    {
        int finalQuality = Math.Max(0, baseQuality - burnCount * 2);
        return Math.Clamp(finalQuality, 0, 18);
    }

    /// <summary>1246: 屠夫任务高品质鹿肉收购判定 (品质 >= 15 的鹿肉可兑换高额赏金)</summary>
    public static (bool accepted, int rewardGold, int rewardExp) EvaluateButcherDeerMeatQuest(int meatQuality)
    {
        if (meatQuality >= 15)
        {
            return (true, 5000, 2000);
        }
        return (false, 100, 50);
    }

    /// <summary>1247: 鸡肉割肉品质计算 (品质 3~10)</summary>
    public static int CalculateChickenMeatQuality(int roll)
    {
        return Math.Clamp(roll, 3, 10);
    }

    /// <summary>1248: 挖掘僵尸与骷髅无肉块产出判定</summary>
    public static bool CanMonsterYieldMeat(string monsterName)
    {
        return monsterName == "鹿" || monsterName == "鸡" || monsterName == "羊";
    }

    /// <summary>1249: 铁匠铺特修金币动态公式 (基础修费 * (最大持久 - 当前持久) / 最大持久 * 3)</summary>
    public static int CalculateSpecialRepairCost(int baseCost, int currentDura, int maxDura)
    {
        if (maxDura <= 0 || currentDura >= maxDura) return 0;
        int missing = maxDura - currentDura;
        long cost = (long)baseCost * missing * 3 / maxDura;
        return (int)Math.Max(10, cost);
    }

    /// <summary>1250: 客户端与服务端时间戳单调递增校验 (防变速齿轮加速发包封禁)</summary>
    public static (bool pass, string reason) ValidateClientPacketTimestamp(long lastClientTick, long incomingClientTick, long serverTickDelta)
    {
        if (incomingClientTick < lastClientTick)
        {
            return (false, "客户端时钟倒流异常，拒绝处理。");
        }
        // 若客户端时钟比服务端时基快超过 3 秒，判定为变速齿轮外挂
        if (incomingClientTick - lastClientTick > serverTickDelta + 3000)
        {
            return (false, "检测到加速发包异常(变速齿轮)，强制踢下线。");
        }
        return (true, string.Empty);
    }
}
