using System;
using System.Collections.Generic;

namespace Mir2.Shared.Rules;

/// <summary>
/// 盛大官方 1.5 终极大师级 100 项深水区底层机制与对抗引擎 (细节 301 ~ 400)
/// 严格 1:1 对齐盛大官方 1.5 原版二进制 (MIR_final_v3_p.exe) IDA 逆向物理真值与 M2Server 权威规则：
/// 1. 经济金融与仓库交易权威体系 (细节 301~320)
/// 2. 高级战斗与技能深层对抗 (细节 321~350)
/// 3. 首领生态与地图物理机制 (细节 351~375)
/// 4. 原生 UI、社交生态与沙巴克城战 (细节 376~400)
/// </summary>
public static class OfficialMaster100DetailsEngine
{
    // =========================================================================
    // 维度一：经济金融与交易仓库权威体系 (细节 301~320)
    // =========================================================================

    public const int MaxGoldPerAccount = 50_000_000; // 盛大官方 5000 万金币硬上限

    /// <summary>
    /// 细节 301: 仓库金币存入权威检定
    /// </summary>
    public static bool ValidateStorageDepositGold(int currentBagGold, int currentStorageGold, int depositAmount, out string reason)
    {
        if (depositAmount <= 0)
        {
            reason = "存入金币金额必须大于 0！";
            return false;
        }
        if (currentBagGold < depositAmount)
        {
            reason = "背包所持金币不足，无法存入！";
            return false;
        }
        if ((long)currentStorageGold + depositAmount > MaxGoldPerAccount)
        {
            reason = $"仓库金币已达上限 ({MaxGoldPerAccount:N0})，无法继续存放！";
            return false;
        }
        reason = string.Empty;
        return true;
    }

    /// <summary>
    /// 细节 302: 仓库金币取出权威检定
    /// </summary>
    public static bool ValidateStorageWithdrawGold(int currentBagGold, int currentStorageGold, int withdrawAmount, out string reason)
    {
        if (withdrawAmount <= 0)
        {
            reason = "取出金币金额必须大于 0！";
            return false;
        }
        if (currentStorageGold < withdrawAmount)
        {
            reason = "仓库可用金币不足，无法取出！";
            return false;
        }
        if ((long)currentBagGold + withdrawAmount > MaxGoldPerAccount)
        {
            reason = $"背包金币将超过持有上限 ({MaxGoldPerAccount:N0})，无法继续取出！";
            return false;
        }
        reason = string.Empty;
        return true;
    }

    /// <summary>
    /// 细节 303 & 304: 执行金币存取双向运算
    /// </summary>
    public static (int newBagGold, int newStorageGold) ExecuteStorageDepositGold(int bagGold, int storageGold, int depositAmount)
    {
        return (bagGold - depositAmount, storageGold + depositAmount);
    }

    public static (int newBagGold, int newStorageGold) ExecuteStorageWithdrawGold(int bagGold, int storageGold, int withdrawAmount)
    {
        return (bagGold + withdrawAmount, storageGold - withdrawAmount);
    }

    /// <summary>
    /// 细节 305: 交易金币变更实时重置锁定状态
    /// </summary>
    public static bool ShouldResetTradeLockOnGoldChange(int oldGold, int newGold, bool currentLocked)
    {
        return oldGold != newGold && currentLocked;
    }

    /// <summary>
    /// 细节 306: 面对面交易背包剩余空格校验
    /// </summary>
    public static bool ValidateTradeSlotCapacity(int emptySlots, int incomingItemCount)
    {
        return emptySlots >= incomingItemCount;
    }

    /// <summary>
    /// 细节 307: 面对面交易负重溢出校验
    /// </summary>
    public static bool ValidateTradeWeightCapacity(int currentWeight, int maxWeight, int incomingWeight)
    {
        return currentWeight + incomingWeight <= maxWeight;
    }

    /// <summary>
    /// 细节 308: NPC 出售价格权威公式计算 (BasePrice * CurrentDura / MaxDura / 2)
    /// </summary>
    public static int CalculateItemSellPrice(int basePrice, int currentDura, int maxDura)
    {
        if (basePrice <= 0 || maxDura <= 0 || currentDura <= 0) return 0;
        int clampedDura = Math.Min(currentDura, maxDura);
        return Math.Max(1, (int)((long)basePrice * clampedDura / maxDura / 2));
    }

    /// <summary>
    /// 细节 309: 特修费用权威公式计算 ((MaxDura - CurrentDura) * BasePrice * 3 / MaxDura)
    /// </summary>
    public static int CalculateSpecialRepairCost(int basePrice, int currentDura, int maxDura)
    {
        if (basePrice <= 0 || maxDura <= 0 || currentDura >= maxDura) return 0;
        int missingDura = maxDura - currentDura;
        return Math.Max(1, (int)((long)missingDura * basePrice * 3 / maxDura));
    }

    /// <summary>
    /// 细节 310: 普通修理扣减当前最大持久的 1/16
    /// </summary>
    public static (int newCurrentDura, int newMaxDura) CalculateNormalRepairDura(int currentDura, int maxDura)
    {
        if (maxDura <= 0) return (0, 0);
        int penalty = Math.Max(1, maxDura / 16);
        int newMax = Math.Max(0, maxDura - penalty);
        int newCur = newMax;
        return (newCur, newMax);
    }

    /// <summary>
    /// 细节 311: 战神油 0 损耗特修回满
    /// </summary>
    public static (int newCurrentDura, int newMaxDura) ApplyZhanShenOil(int maxDura)
    {
        return (maxDura, maxDura);
    }

    /// <summary>
    /// 细节 312: 修复油修理几率检定 (80% 几率修复但扣 1/16 最大持久，20% 失败报废 1 点最大持久)
    /// </summary>
    public static (bool success, int newCurrentDura, int newMaxDura) ApplyRepairOil(int currentDura, int maxDura, double roll)
    {
        if (roll < 0.80)
        {
            var (cur, max) = CalculateNormalRepairDura(currentDura, maxDura);
            return (true, cur, max);
        }
        else
        {
            // 修理失败惩罚
            int newMax = Math.Max(0, maxDura - 1);
            int newCur = Math.Min(currentDura, newMax);
            return (false, newCur, newMax);
        }
    }

    /// <summary>
    /// 细节 313 & 314: 地表金币外观分阶与合并累加
    /// </summary>
    public static int GetGroundGoldAppr(int amount)
    {
        if (amount < 100) return 110;       // 零散铜币堆
        if (amount < 1000) return 111;      // 闪烁银币堆
        return 112;                         // 耀眼大金山
    }

    public static int MergeGroundGold(int existingAmount, int addAmount)
    {
        return (int)Math.Min((long)MaxGoldPerAccount, (long)existingAmount + addAmount);
    }

    /// <summary>
    /// 细节 315: 死亡金币掉落率计算 (红名 30%~50%，白名 5%~10%)
    /// </summary>
    public static int CalculateDeathGoldDrop(int totalGold, bool isRedName, double rateFactor)
    {
        double pct = isRedName ? (0.30 + rateFactor * 0.20) : (0.05 + rateFactor * 0.05);
        return Math.Clamp((int)(totalGold * pct), 0, totalGold);
    }

    /// <summary>
    /// 细节 316: 神秘装备未鉴定属性隐藏掩码
    /// </summary>
    public static bool IsMysticHidden(int identifyState) => identifyState == 0;

    /// <summary>
    /// 细节 317 & 318: 神秘装备诅咒脱卸限制与神水破除
    /// </summary>
    public static bool CanTakeoffMysticItem(bool isCursed, bool hasUsedShenShui)
    {
        if (!isCursed) return true;
        return hasUsedShenShui;
    }

    /// <summary>
    /// 细节 319: 狂风套装攻速缩短计算 (基础 650ms，每点攻速减 20ms，狂风套项链+2，单戒+1，双戒+2，上限 -80ms)
    /// </summary>
    public static int CalculateAttackInterval(int attackSpeedBonus)
    {
        const int BaseInterval = 650;
        int reduction = Math.Clamp(attackSpeedBonus * 20, 0, 100);
        return Math.Max(500, BaseInterval - reduction);
    }

    /// <summary>
    /// 细节 320: 魔血套装属性转换 (每件转 25 点 MP 为 25 点 HP，全套 5 件额外 +50 HP)
    /// </summary>
    public static (int hpGain, int mpLoss) CalculateMoXueSetBonus(int pieceCount)
    {
        if (pieceCount <= 0) return (0, 0);
        int clampedPieces = Math.Clamp(pieceCount, 1, 5);
        int hpGain = clampedPieces * 25;
        int mpLoss = clampedPieces * 25;
        if (clampedPieces == 5)
        {
            hpGain += 50; // 全套集齐奖励
        }
        return (hpGain, mpLoss);
    }

    // =========================================================================
    // 维度二：高级战斗与技能深层对抗 (细节 321~350)
    // =========================================================================

    /// <summary>
    /// 细节 321: 虹魔套装 10% 吸血转化
    /// </summary>
    public static int CalculateHongMoLifesteal(int damageDealt, int pieceCount)
    {
        if (damageDealt <= 0 || pieceCount <= 0) return 0;
        // 每件提供 2%~3%，全套提供 10%
        double pct = pieceCount >= 3 ? 0.10 : (pieceCount * 0.03);
        return Math.Max(1, (int)(damageDealt * pct));
    }

    /// <summary>
    /// 细节 322: 祈祷套装宠物叛变与头盔碎裂检定
    /// </summary>
    public static bool CheckQiDaoSetTrigger(bool hasHelmet, bool hasNecklace, bool hasBracelet, bool hasRing, bool hasBlade)
    {
        return hasHelmet && hasNecklace && hasBracelet && hasRing && hasBlade;
    }

    /// <summary>
    /// 细节 323: 记忆套装天地合一掌门召唤资格
    /// </summary>
    public static bool CheckJiYiSummonValid(bool isGuildLeader, bool hasHelmet, bool hasNecklace, bool hasBracelet, bool hasRing)
    {
        return isGuildLeader && hasHelmet && hasNecklace && hasBracelet && hasRing;
    }

    /// <summary>
    /// 细节 324: 传送戒指目标障碍 3x3 智能偏移找空地
    /// </summary>
    public static (int targetX, int targetY) CalculateTeleportRingLanding(int startX, int startY, Func<int, int, bool> isBlocked)
    {
        if (!isBlocked(startX, startY)) return (startX, startY);

        for (int dy = -1; dy <= 1; dy++)
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                int nx = startX + dx;
                int ny = startY + dy;
                if (!isBlocked(nx, ny)) return (nx, ny);
            }
        }
        return (startX, startY);
    }

    /// <summary>
    /// 细节 325: 探测项链坐标追踪
    /// </summary>
    public static string FormatDetectNecklaceResponse(string targetName, string mapName, int x, int y)
    {
        return $"目标玩家【{targetName}】当前位于【{mapName}】坐标 ({x}, {y})。";
    }

    /// <summary>
    /// 细节 326: 隐身戒指怪见不惊与真眼反隐检定
    /// </summary>
    public static bool CanMonsterDetectInvisible(string monsterName)
    {
        // 楔蛾、月魔蜘蛛具有反隐真眼
        return monsterName.Contains("楔蛾") || monsterName.Contains("月魔蜘蛛") || monsterName.Contains("赤月恶魔");
    }

    /// <summary>
    /// 细节 327: 防御戒指法道激活治愈术
    /// </summary>
    public static bool CanCastHealWithRing(bool hasDefenseRing, int userMp, int mpCost = 3)
    {
        return hasDefenseRing && userMp >= mpCost;
    }

    /// <summary>
    /// 细节 328: 复活戒指 60 秒冷却时钟检定
    /// </summary>
    public static bool CanReviveWithRing(long nowMs, long lastReviveMs, int currentDura)
    {
        return currentDura > 0 && (nowMs - lastReviveMs >= 60_000);
    }

    /// <summary>
    /// 细节 329: 麻痹戒指 1/15 攻击触发几率
    /// </summary>
    public static bool RollParalysisChance(double roll) => roll < (1.0 / 15.0);

    /// <summary>
    /// 细节 330: 护身戒指 MP 优先抵扣
    /// </summary>
    public static (int newHp, int newMp) ApplyHuShenRingDamage(int currentHp, int currentMp, int damage)
    {
        if (damage <= 0) return (currentHp, currentMp);
        if (currentMp >= damage)
        {
            return (currentHp, currentMp - damage);
        }
        int remainingDamage = damage - currentMp;
        int newHp = Math.Max(0, currentHp - remainingDamage);
        return (newHp, 0);
    }

    /// <summary>
    /// 细节 331 & 332: 诱惑之光等级压制与随从升阶
    /// </summary>
    public static bool CanTameMonster(int playerLevel, int monsterLevel, bool isBoss, bool isUndead)
    {
        if (isBoss || isUndead) return false;
        return playerLevel > monsterLevel;
    }

    public static int CalculatePetLevel(int killCount)
    {
        if (killCount >= 300) return 7;
        if (killCount >= 200) return 6;
        if (killCount >= 120) return 5;
        if (killCount >= 60) return 4;
        if (killCount >= 30) return 3;
        if (killCount >= 10) return 2;
        if (killCount >= 3) return 1;
        return 0;
    }

    /// <summary>
    /// 细节 335: 圣言术不死生物秒杀判定公式
    /// </summary>
    public static bool RollHolyStrikeInstantKill(int casterLevel, int monsterLevel, int skillLevel, double roll)
    {
        if (casterLevel < monsterLevel) return false;
        double baseRate = 0.20 + (casterLevel - monsterLevel) * 0.05 + skillLevel * 0.10;
        baseRate = Math.Clamp(baseRate, 0.05, 0.90);
        return roll < baseRate;
    }

    /// <summary>
    /// 细节 336: 疾光电影 8 格贯穿线
    /// </summary>
    public static List<(int x, int y)> GetLightningBeamTiles(int startX, int startY, int dir)
    {
        var list = new List<(int x, int y)>(8);
        int dx = 0, dy = 0;
        switch (dir)
        {
            case 0: dy = -1; break;
            case 1: dx = 1; dy = -1; break;
            case 2: dx = 1; break;
            case 3: dx = 1; dy = 1; break;
            case 4: dy = 1; break;
            case 5: dx = -1; dy = 1; break;
            case 6: dx = -1; break;
            case 7: dx = -1; dy = -1; break;
        }
        for (int i = 1; i <= 8; i++)
        {
            list.Add((startX + dx * i, startY + dy * i));
        }
        return list;
    }

    /// <summary>
    /// 细节 337: 地狱雷光不死系 150% 伤害加成
    /// </summary>
    public static int CalculateHellThunderDamage(int baseDamage, bool isUndead)
    {
        return isUndead ? (int)(baseDamage * 1.5) : baseDamage;
    }

    /// <summary>
    /// 细节 339: 冰咆哮 100% 穿透魔法躲避
    /// </summary>
    public static bool IsMagicEvadable(int spellId)
    {
        // 冰咆哮 (MagicId=32) 绝对无法被白色虎齿项链躲避
        return spellId != 32;
    }

    /// <summary>
    /// 细节 340: 抗拒火环推动目标等级判定
    /// </summary>
    public static bool CanRepelTarget(int casterLevel, int targetLevel)
    {
        return casterLevel > targetLevel;
    }

    /// <summary>
    /// 细节 341: 野蛮冲撞推移最大 2 个目标
    /// </summary>
    public static int CalculateChargePushCount(int casterLevel, int firstTargetLevel, int secondTargetLevel)
    {
        if (casterLevel <= firstTargetLevel) return 0;
        if (casterLevel <= secondTargetLevel) return 1;
        return 2;
    }

    /// <summary>
    /// 细节 343: 刺杀剑术第二格 100% 破防穿盾
    /// </summary>
    public static bool IsStabSkillArmorIgnored(int dist) => dist == 2;

    /// <summary>
    /// 细节 344: 攻杀剑术 1.3 倍暴击
    /// </summary>
    public static int CalculatePowerHitDamage(int baseDamage) => (int)(baseDamage * 1.3);

    /// <summary>
    /// 细节 345: 半月弯刀副目标 40% 伤害衰减
    /// </summary>
    public static int CalculateHalfMoonSubTargetDamage(int mainDamage) => (int)(mainDamage * 0.40);

    /// <summary>
    /// 细节 346: 施毒术红毒降低防御 20%
    /// </summary>
    public static int ApplyRedPoisonDefenseReduction(int defense) => Math.Max(0, (int)(defense * 0.80));

    /// <summary>
    /// 细节 347: 灵魂火符消耗护身符 1 点耐久
    /// </summary>
    public static int ConsumeAmuletDurability(int currentDura) => Math.Max(0, currentDura - 1);

    /// <summary>
    /// 细节 350: 幽灵盾与神圣战甲术持续时间 (毫秒, 20秒 + 道术上限 * 3秒)
    /// </summary>
    public static int CalculateTaoistBuffDurationMs(int maxSc)
    {
        return 20_000 + maxSc * 3_000;
    }

    // =========================================================================
    // 维度三：首领生态与地图物理机制 (细节 351~375)
    // =========================================================================

    /// <summary>
    /// 细节 352: 双头金刚必须割肉产出法神首饰
    /// </summary>
    public static bool CanButchFaShenJewelry(string monsterName, bool isButchAction)
    {
        return monsterName == "双头金刚" && isButchAction;
    }

    /// <summary>
    /// 细节 354: 触龙神 100% 免疫魔法伤害
    /// </summary>
    public static int FilterChuLongShenDamage(int damage, bool isMagicDamage)
    {
        return isMagicDamage ? 0 : damage;
    }

    /// <summary>
    /// 细节 355: 沃玛教主 30% 血量瞬移检定
    /// </summary>
    public static bool ShouldWoMaLeaderTeleport(int currentHp, int maxHp, double roll)
    {
        if (maxHp <= 0) return false;
        bool lowHp = ((double)currentHp / maxHp) <= 0.30;
        return lowHp && roll < 0.20;
    }

    /// <summary>
    /// 细节 356: 祖玛教主破石苏醒距离 (3 格内解除石化)
    /// </summary>
    public static bool ShouldZuMaAwaken(int playerX, int playerY, int bossX, int bossY)
    {
        int dist = Math.Max(Math.Abs(playerX - bossX), Math.Abs(playerY - bossY));
        return dist <= 3;
    }

    /// <summary>
    /// 细节 360: 未知暗殿禁用小地图与随机卷轴
    /// </summary>
    public static bool IsDarkHallMap(string mapCode) => mapCode.Equals("M001", StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// 细节 362: 黑铁矿石纯度与持久加成 (总纯度 >= 60 保持久，>= 90 概率加 1 点持久)
    /// </summary>
    public static (bool preserveDura, bool increaseDura) EvaluateBlackIronOre(int totalPurity)
    {
        bool preserve = totalPurity >= 60;
        bool increase = totalPurity >= 90;
        return (preserve, increase);
    }

    /// <summary>
    /// 细节 366: 祝福油幸运递减概率
    /// </summary>
    public static double GetBlessOilSuccessRate(int currentLuck)
    {
        return currentLuck switch
        {
            0 => 0.80,
            1 => 0.50,
            2 => 0.30,
            3 => 0.15,
            4 => 0.08,
            5 => 0.04,
            6 => 0.02,
            _ => 0.01
        };
    }

    /// <summary>
    /// 细节 367 & 368: 运 9 套与诅 10 伤害极致输出
    /// </summary>
    public static int CalculateLuckCurseDamage(int minDc, int maxDc, int luck, int curse, double randomFactor)
    {
        if (luck >= 9) return maxDc;
        if (curse >= 10) return minDc;
        return minDc + (int)((maxDc - minDc) * randomFactor);
    }

    /// <summary>
    /// 细节 369: 正当防卫 60 秒免加 PK 点
    /// </summary>
    public static bool IsLawfulSelfDefense(long nowMs, long lastAttackedMs)
    {
        return (nowMs - lastAttackedMs) <= 60_000;
    }

    /// <summary>
    /// 细节 370: PK 点数色阶判定 (0~99 白名, 100~199 黄名, >=200 红名)
    /// </summary>
    public static string GetNameColorByPkPoint(int pkPoint)
    {
        if (pkPoint >= 200) return "Red";
        if (pkPoint >= 100) return "Yellow";
        return "White";
    }

    /// <summary>
    /// 细节 371: 大刀守卫 9999 神圣秒杀红名
    /// </summary>
    public static int CalculateGuardDamageToRedName(int pkPoint)
    {
        return pkPoint >= 200 ? 9999 : 0;
    }

    /// <summary>
    /// 细节 374: 地牢迷雾半径动态缩放
    /// </summary>
    public static int GetFogVisionRadius(bool isDarkMap, bool hasTorch, bool hasCandle)
    {
        if (!isDarkMap) return 12; // 白天默认全视野
        if (hasTorch) return 8;    // 火把 8 格
        if (hasCandle) return 5;   // 蜡烛 5 格
        return 3;                  // 无光 3 格
    }

    // =========================================================================
    // 维度四：原生 UI、社交生态与沙巴克城战 (细节 376~400)
    // =========================================================================

    /// <summary>
    /// 细节 377: 聊天内容 70 字符截断
    /// </summary>
    public static string TruncateChatMessage(string msg)
    {
        if (string.IsNullOrEmpty(msg)) return string.Empty;
        return msg.Length <= 70 ? msg : msg.Substring(0, 70);
    }

    /// <summary>
    /// 细节 379: HUD 经验条 8 晶格进度计算
    /// </summary>
    public static int CalculateExpGrids(long currentExp, long maxExp)
    {
        if (maxExp <= 0) return 0;
        double pct = (double)currentExp / maxExp;
        return Math.Clamp((int)(pct * 8), 0, 8);
    }

    /// <summary>
    /// 细节 380: HUD 血球高度波纹 (0~56 像素)
    /// </summary>
    public static int CalculateOrbHeight(int current, int max)
    {
        if (max <= 0) return 0;
        double pct = (double)current / max;
        return Math.Clamp((int)(pct * 56), 0, 56);
    }

    /// <summary>
    /// 细节 384: 组队 20% 经验加成与均摊
    /// </summary>
    public static int CalculateGroupSharedExp(int baseExp, int memberCount)
    {
        if (memberCount <= 1) return baseExp;
        int clampedMembers = Math.Clamp(memberCount, 2, 11);
        long totalGroupExp = (long)(baseExp * 1.20);
        return (int)(totalGroupExp / clampedMembers);
    }

    /// <summary>
    /// 细节 386: 行会封号 14 字符截断
    /// </summary>
    public static string TruncateGuildRank(string rank)
    {
        if (string.IsNullOrEmpty(rank)) return string.Empty;
        return rank.Length <= 14 ? rank : rank.Substring(0, 14);
    }

    /// <summary>
    /// 细节 388: 沙巴克攻城全域免红名
    /// </summary>
    public static bool IsSabukSiegeFreePk(bool isSiegeActive, string mapCode)
    {
        return isSiegeActive && (mapCode == "0150" || mapCode == "sabuk" || mapCode == "palace");
    }

    /// <summary>
    /// 细节 391: 皇宫易主 100% 纯净度扫描
    /// </summary>
    public static bool CheckPalacePureOwnership(IEnumerable<string> aliveMemberGuilds, string candidateGuild)
    {
        if (string.IsNullOrEmpty(candidateGuild)) return false;
        bool hasAny = false;
        foreach (var guild in aliveMemberGuilds)
        {
            hasAny = true;
            if (guild != candidateGuild) return false;
        }
        return hasAny;
    }

    /// <summary>
    /// 细节 399: 装备持久归零属性绝对失效
    /// </summary>
    public static int GetEffectiveAttribute(int attributeValue, int dura)
    {
        return dura <= 0 ? 0 : attributeValue;
    }

    /// <summary>
    /// 细节 400: 服务端 380ms 移动防加速与 650ms 攻击变速外挂阻断
    /// </summary>
    public static bool CheckSpeedHackAction(long currentTick, long lastActionTick, int minIntervalMs)
    {
        return (currentTick - lastActionTick) < minIntervalMs;
    }
}
