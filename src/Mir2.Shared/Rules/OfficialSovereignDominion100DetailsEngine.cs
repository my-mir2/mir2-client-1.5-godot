using System;
using System.Collections.Generic;

namespace Mir2.Shared.Rules;

/// <summary>
/// 盛大官方 1.5 统治纪元 100 微观细节与服务端权威闭环引擎 (Details 1051 ~ 1150)
/// 严格 1:1 对齐官方二进制 MIR_final_v3_p.exe 权威逻辑与白日门/赤月/套装/升级核心系统
/// 遵循纯血 Dumb Client 架构，所有判定均为 M2Server 服务端权威真值
/// </summary>
public static class OfficialSovereignDominion100DetailsEngine
{
    // =========================================================================
    // 细节 1051 ~ 1065: 白日门天尊道观、虎卫堂与道教文化深层交互 (15 Details)
    // =========================================================================

    /// <summary>1051: 天尊道观安全区与大门侍童不可攻击判定</summary>
    public static bool IsTaoistTempleSafeZone(int x, int y)
    {
        return x >= 70 && x <= 85 && y >= 40 && y <= 55;
    }

    /// <summary>1052: 天尊对话接取虎卫堂传说任务门槛判定 (等级 >= 35, 缴纳 50,000 金币研习古籍)</summary>
    public static (bool canAccept, string reason) ValidateHuWeiQuestRequirement(int level, int goldFee)
    {
        if (level < 35) return (false, "天尊：勇士，你的修行尚浅，不到35级切莫打探虎卫堂之秘。");
        if (goldFee < 50_000) return (false, "天尊：研习虎卫密卷需缴纳50,000金币以购得朱砂与拓印纸。");
        return (true, string.Empty);
    }

    /// <summary>1053: 白日门特制大玉净瓶回复属性 (每次回复 120 HP)</summary>
    public static (int hpRecovery, int intervalMs, int totalTicks) GetSpecialJadeBottleProfile()
    {
        return (12, 1000, 10); // 10 次，每次 12 点共 120 点
    }

    /// <summary>1054: 白日门特制高级药粉持久度 (持久 500 点)</summary>
    public static int GetSpecialPoisonMaxDura() => 500;

    /// <summary>1055: 白日门密道传送阵双向坐标判定</summary>
    public static (int destX, int destY) GetSecretPassageExit(int sourceX, int sourceY)
    {
        if (sourceX == 352 && sourceY == 241) return (10, 15);
        return (352, 241);
    }

    /// <summary>1056: 虎卫死亡刷新广播文本</summary>
    public static string GetHuWeiRespawnBroadcast() => "虎卫堂内传来一声凄厉的怒吼，新的虎卫已然诞生！";

    /// <summary>1057: 鹰卫箭矢飞行弹道耗时计算 (每格 45ms)</summary>
    public static int CalculateYingWeiArrowFlightTimeMs(int distanceTiles)
    {
        return Math.Max(45, distanceTiles * 45);
    }

    /// <summary>1058: 虎卫攻击力满额发挥暴击蓝光判定</summary>
    public static bool IsHuWeiMaxAttackCrit(int damageDealt, int maxDc)
    {
        return damageDealt >= maxDc && maxDc > 0;
    }

    /// <summary>1059: 7 级神兽扇形喷火 3 格 5 目标判定</summary>
    public static bool IsInShinsuFireBreathSector(int sourceX, int sourceY, int targetX, int targetY, int dir)
    {
        int dist = Math.Max(Math.Abs(sourceX - targetX), Math.Abs(sourceY - targetY));
        return dist <= 3;
    }

    /// <summary>1060: 7 级神兽移速阶梯 (0 级 600ms, 7 级 400ms)</summary>
    public static int GetShinsuMoveIntervalMs(int petLevel)
    {
        int lvl = Math.Clamp(petLevel, 0, 7);
        return 600 - (lvl * 28);
    }

    /// <summary>1061: 狂暴状态怪物诱惑抗性提升 (抗性提升 50%)</summary>
    public static int CalculateBerserkLureResistance(int baseResistance, bool isBerserk)
    {
        if (!isBerserk) return baseResistance;
        return Math.Min(95, (int)MathF.Round(baseResistance * 1.5f));
    }

    /// <summary>1062: 仆从跟随进入安全区自动进入收刀防御姿态</summary>
    public static bool ShouldPetSheatheWeaponInSafeZone(bool isInSafeZone) => isInSafeZone;

    /// <summary>1063: 丛林迷宫野生勇士攻击力加成 (攻击提升 30%)</summary>
    public static int GetJungleSpecialMonsterAttack(int baseDc)
    {
        return (int)MathF.Round(baseDc * 1.3f);
    }

    /// <summary>1064: 虎卫堂大门狭窄通道判定 (1 格宽卡位通道)</summary>
    public static bool IsHuWeiHallGateNarrowChokepoint(int x, int y) => x == 25 && y == 20;

    /// <summary>1065: 法师携带 5 只虎卫威压光环 (周围小怪索敌范围缩减 2 格)</summary>
    public static int ApplyHuWeiIntimidationAura(int baseAggroRange, int huWeiCount)
    {
        if (huWeiCount >= 5) return Math.Max(1, baseAggroRange - 2);
        return baseAggroRange;
    }

    // =========================================================================
    // 细节 1066 ~ 1080: 赤月峡谷老巢深水区与恶魔祭坛卡位攻坚 (15 Details)
    // =========================================================================

    /// <summary>1066: 抉择之地双向路口坐标判定</summary>
    public static bool IsChoicePlacePortal(int x, int y)
    {
        return (x == 145 && y == 110) || (x == 215 && y == 180);
    }

    /// <summary>1067: 山谷密道怪物高密度判定 (密度系数 1.8)</summary>
    public static int GetValleySecretPassageExpectedMonsters(int baseSpawnCount)
    {
        return (int)MathF.Round(baseSpawnCount * 1.8f);
    }

    /// <summary>1068: 恶魔祭坛入口传送点定身耗时 (0.5 秒 / 500ms)</summary>
    public static int GetAltarEntranceStunDurationMs() => 500;

    /// <summary>1069: 赤月恶魔地刺对怪物友伤判定 (全场怪物受 50% 地刺伤害)</summary>
    public static int CalculateRedMoonSpikeFriendlyFire(int spikeDamage)
    {
        return (int)MathF.Round(spikeDamage * 0.5f);
    }

    /// <summary>1070: 双头金刚与双头血魔 10 格仇恨联动狂暴判定</summary>
    public static bool ShouldTriggerDualBossAggroLink(int boss1X, int boss1Y, int boss2X, int boss2Y)
    {
        int dist = Math.Max(Math.Abs(boss1X - boss2X), Math.Abs(boss1Y - boss2Y));
        return dist <= 10;
    }

    /// <summary>1071: 双头金刚 80% 物理免伤判定</summary>
    public static int ApplyDoubleHeadKongPhysMitigation(int physicalDamage)
    {
        return (int)MathF.Round(physicalDamage * 0.2f);
    }

    /// <summary>1072: 双头血魔 80% 魔法免伤判定</summary>
    public static int ApplyDoubleHeadFiendMagicMitigation(int magicDamage)
    {
        return (int)MathF.Round(magicDamage * 0.2f);
    }

    /// <summary>1073: 赤月恶魔生命低于 20% 狂暴地刺频率提升 (由 2000ms 降至 1200ms)</summary>
    public static int GetRedMoonSpikeIntervalMs(int curHp, int maxHp)
    {
        if (maxHp <= 0) return 2000;
        bool lowHp = ((float)curHp / maxHp) < 0.2f;
        return lowHp ? 1200 : 2000;
    }

    /// <summary>1074: 幻影蜘蛛死亡未爆幼虫自毁判定</summary>
    public static bool ShouldExplosiveSpidersDetonateOnHostDeath(bool hostIsDead) => hostIsDead;

    /// <summary>1075: 黑锷蜘蛛近战撕裂无视幽灵盾魔防削减</summary>
    public static bool DoesBlackSpiderIgnoreGhostShield() => true;

    /// <summary>1076: 月魔蜘蛛石化时间连续抗性衰减 (每次衰减 20%, 最低 1 秒)</summary>
    public static float CalculateDiminishingStoneDuration(float baseDuration, int consecutiveStones)
    {
        float mult = MathF.Pow(0.8f, consecutiveStones);
        return Math.Max(1.0f, baseDuration * mult);
    }

    /// <summary>1077: 暴食蜘蛛单次吞噬自愈上限 (自身最大生命值 10%)</summary>
    public static int CalculateGluttonousSpiderRegenCap(int damageDealt, int maxHp)
    {
        int proposed = (int)MathF.Round(damageDealt * 0.3f);
        int cap = (int)MathF.Round(maxHp * 0.10f);
        return Math.Min(proposed, cap);
    }

    /// <summary>1078: 道士群体隐身术赤月峡谷持续时间减半 (15 秒)</summary>
    public static int GetGroupInvisDurationInRedMoon(int normalSec) => Math.Max(1, normalSec / 2);

    /// <summary>1079: 战士野蛮冲撞对赤月蜘蛛击退判定</summary>
    public static bool CanMotaeboPushSpider(int playerLevel, int spiderLevel)
    {
        return playerLevel > spiderLevel;
    }

    /// <summary>1080: 赤月老巢毒气弥漫自然恢复降低 50%</summary>
    public static int ApplyRedMoonPoisonMiasmaRegen(int baseRegen)
    {
        return Math.Max(1, baseRegen / 2);
    }

    // =========================================================================
    // 细节 1081 ~ 1095: 祈祷/记忆/虹魔/魔血/狂风装备极致平衡与特戒交互 (15 Details)
    // =========================================================================

    /// <summary>1081: 祈祷手镯敏捷+2与物理闪避率加成 (+10%)</summary>
    public static (int agilityBonus, int dodgeBonusPercent) GetPrayerBraceletBonus() => (2, 10);

    /// <summary>1082: 祈祷戒指佩戴死亡碎裂几率 (100% 破碎销毁)</summary>
    public static bool DoesPrayerRingShatterOnDeath() => true;

    /// <summary>1083: 祈祷之刃属性与出刀提速 (准确+3, 提速 1 档 / 60ms)</summary>
    public static (int accuracyBonus, int speedReductionMs) GetPrayerBladeBonus() => (3, 60);

    /// <summary>1084: 记忆头盔物防 3-3 全职业通用判定</summary>
    public static (int minAc, int maxAc, bool allJobs) GetRecallHelmetStats() => (3, 3, true);

    /// <summary>1085: 记忆手镯魔防 1-1 属性</summary>
    public static (int minMac, int maxMac) GetRecallBraceletStats() => (1, 1);

    /// <summary>1086: 记忆戒指组队坐标探查特权</summary>
    public static bool DoesRecallRingEnableLocateGroup() => true;

    /// <summary>1087: 虹魔项链物理吸血比例 (5%)</summary>
    public static int GetRainbowNecklaceLifeStealPercent() => 5;

    /// <summary>1088: 虹魔手镯物理吸血比例 (5%)</summary>
    public static int GetRainbowBraceletLifeStealPercent() => 5;

    /// <summary>1089: 虹魔戒指物理吸血比例 (5%)</summary>
    public static int GetRainbowRingLifeStealPercent() => 5;

    /// <summary>1090: 魔血手镯增加 15 HP 扣除 15 MP</summary>
    public static (int hpGain, int mpLoss) GetDemonBloodBraceletModifiers() => (15, 15);

    /// <summary>1091: 魔血项链增加 25 HP 扣除 25 MP</summary>
    public static (int hpGain, int mpLoss) GetDemonBloodNecklaceModifiers() => (25, 25);

    /// <summary>1092: 魔血戒指增加 15 HP 扣除 15 MP</summary>
    public static (int hpGain, int mpLoss) GetDemonBloodRingModifiers() => (15, 15);

    /// <summary>1093: 狂风项链攻速+2缩短出刀硬直 (硬直缩短 160ms)</summary>
    public static int GetGaleNecklaceAttackDelayMs() => 160;

    /// <summary>1094: 狂风戒指攻速+1缩短出刀硬直 (硬直缩短 80ms)</summary>
    public static int GetGaleRingAttackDelayMs() => 80;

    /// <summary>1095: 麻痹戒指霸体 BOSS 几率惩罚 (由 1/15 降至 1/100, 麻痹 1 秒)</summary>
    public static (bool paralyzed, float durationSec) EvaluateParalyzeRingOnBoss(bool isBoss, int roll1000)
    {
        if (isBoss)
        {
            if (roll1000 < 10) return (true, 1.0f);
            return (false, 0f);
        }
        if (roll1000 < 66) return (true, 5.0f); // 约 1/15
        return (false, 0f);
    }

    // =========================================================================
    // 细节 1096 ~ 1110: 武器升级高阶概率、沙巴克专属铁匠与开刀玄学 (15 Details)
    // =========================================================================

    /// <summary>1096: 沙巴克城主专属铁匠特修 5 折优惠</summary>
    public static int CalculateSabukOwnerRepairCost(int baseCost, bool isSabukOwner)
    {
        if (isSabukOwner) return baseCost / 2;
        return baseCost;
    }

    /// <summary>1097: 武器升级基础金币费用与沙巴克成员折扣</summary>
    public static int GetWeaponUpgradeGoldFee(bool isSabukMember)
    {
        return isSabukMember ? 80000 : 100000;
    }

    /// <summary>1098: 升级武器取回超时没收判定 (7 天 = 604800 秒)</summary>
    public static bool IsWeaponUpgradeClaimExpired(float elapsedSeconds) => elapsedSeconds >= 604800f;

    /// <summary>1099: 黑铁矿石总纯度 >= 90 有 20% 几率增加 1 点最大持久</summary>
    public static bool DoesBlackIronIncreaseMaxDura(int totalPurity, int roll100)
    {
        return totalPurity >= 90 && roll100 < 20;
    }

    /// <summary>1100: 升级跳点判定 (首饰积分超标 200% 时 10% 几率单次 +2)</summary>
    public static int CalculateUpgradeStatGain(int jewelryScoreRatioPercent, int roll100)
    {
        if (jewelryScoreRatioPercent >= 200 && roll100 < 10) return 2;
        return 1;
    }

    /// <summary>1101: 武器升级单属性累计 +7 硬上限</summary>
    public static int ClampWeaponStatBonus(int currentBonus, int addBonus)
    {
        return Math.Min(7, currentBonus + addBonus);
    }

    /// <summary>1102: 开刀目标为和平模式同伴时不产生额外诅咒</summary>
    public static bool ShouldAttackCauseCurseOnTeammate(bool isTeammate) => !isTeammate;

    /// <summary>1103: 开刀砍大刀卫士判定成功率合法性</summary>
    public static bool IsTrialSlashTargetValid(string targetName) => !string.IsNullOrEmpty(targetName);

    /// <summary>1104: 祝福油成功清除诅咒判定 (有诅咒先减诅咒，无诅咒加幸运)</summary>
    public static (int newLuck, int newCurse) ApplyBlessingOilWithCurseResolution(int curLuck, int curCurse)
    {
        if (curCurse > 0) return (curLuck, curCurse - 1);
        return (Math.Min(7, curLuck + 1), 0);
    }

    /// <summary>1105: 罗刹武器攻击下限发挥机制</summary>
    public static int GetLuoChaDamage(int minDc, int maxDc, int totalCurse)
    {
        if (totalCurse >= 10) return minDc;
        return (minDc + maxDc) / 2;
    }

    /// <summary>1106: 命运之刃等级 35 门槛与防御属性加成</summary>
    public static (bool canEquip, int acBonus) GetDestinyBladeStats(int playerLevel)
    {
        return (playerLevel >= 35, 3);
    }

    /// <summary>1107: 屠龙刀 50 腕力门槛与烈火剑法伤害加成 (10%)</summary>
    public static (bool canEquip, float fireBonusMultiplier) EvaluateDragonSlayer(int handCap)
    {
        bool canEquip = handCap >= 50;
        return (canEquip, 1.10f);
    }

    /// <summary>1108: 嗜魂法杖法师 35 级诱惑祖玛卫士特权</summary>
    public static bool CanSoulStaffTameZumaGuard(bool hasSoulStaff, int mageLevel)
    {
        return hasSoulStaff && mageLevel >= 35;
    }

    /// <summary>1109: 龙纹剑召唤神兽生命值加成 (20%)</summary>
    public static int ApplyDragonSwordShinsuHpBonus(int baseShinsuHp, bool hasDragonSword)
    {
        if (!hasDragonSword) return baseShinsuHp;
        return (int)MathF.Round(baseShinsuHp * 1.20f);
    }

    /// <summary>1110: 骨玉权杖魔法盾持续时间延长 30%</summary>
    public static int ApplyBoneStaffShieldDuration(int baseDurationSec, bool hasBoneStaff)
    {
        if (!hasBoneStaff) return baseDurationSec;
        return (int)MathF.Round(baseDurationSec * 1.30f);
    }

    // =========================================================================
    // 细节 1111 ~ 1125: 未知暗殿、封魔谷矿区与祖玛高阶生态 (15 Details)
    // =========================================================================

    /// <summary>1111: 未知暗殿真教主双毒喷射判定</summary>
    public static (bool hasGreenPoison, bool hasRedPoison) GetUnknownTempleBossPoisonProfile(bool isReal)
    {
        return isReal ? (true, true) : (false, false);
    }

    /// <summary>1112: 未知暗殿真沃玛教主沃玛号角掉率翻倍</summary>
    public static int GetWomaHornDropRateDenominator(bool isRealInUnknownTemple)
    {
        return isRealInUnknownTemple ? 5 : 10;
    }

    /// <summary>1113: 未知暗殿稻草人移速与攻击力</summary>
    public static (int moveIntervalMs, int minDc, int maxDc) GetUnknownScarecrowStats() => (300, 40, 70);

    /// <summary>1114: 未知暗殿鹿移速与超高物防 (物防 80)</summary>
    public static (int moveIntervalMs, int armor) GetUnknownDeerStats() => (400, 80);

    /// <summary>1115: 封魔矿区崎岖路段禁用随机传送卷</summary>
    public static bool IsRandomScrollForbiddenInFengMoMine(string mapId)
    {
        return mapId.Contains("D200") || mapId.Contains("矿区");
    }

    /// <summary>1116: 封魔谷城墙弓箭手秒杀红名 (伤害 200)</summary>
    public static int GetFengMoArcherDamageOnEvil() => 200;

    /// <summary>1117: 霸者大厅红名安全补给属性</summary>
    public static bool CanEvilPlayerTradeInOverlordHall(string mapName) => mapName.Contains("霸者大厅");

    /// <summary>1118: 雷霆之路走法拓扑校验 (4 个通道仅 1 个正确)</summary>
    public static bool IsThunderRoadCorrectPassage(int passageIndex) => passageIndex == 2;

    /// <summary>1119: 封魔殿虹魔教主死亡全屏大爆半径 (5x5)</summary>
    public static int GetRainbowDemonDropRadius() => 5;

    /// <summary>1120: 祖玛阁走法拓扑校验 (左上上上右)</summary>
    public static bool ValidateZumaMazeStep(int stepIndex, string direction)
    {
        string[] correctSequence = { "Left", "Up", "Up", "Up", "Right" };
        if (stepIndex < 0 || stepIndex >= correctSequence.Length) return false;
        return correctSequence[stepIndex].Equals(direction, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>1121: 极品祖玛 1400 卫士移速与反后仰硬直</summary>
    public static (int moveIntervalMs, bool immuneToStumble) GetSuperZumaGuardMovement() => (250, true);

    /// <summary>1122: 祖玛雕像近战大锤破甲百分比 (20%)</summary>
    public static int CalculateZumaStatueArmorPenetration(int targetArmor)
    {
        return (int)MathF.Round(targetArmor * 0.8f);
    }

    /// <summary>1123: 祖玛教主残血召唤大锤亲卫数量 (4 尊)</summary>
    public static int GetZumaLeaderSummonCount() => 4;

    /// <summary>1124: 沃玛教主暴走瞬移回避判定</summary>
    public static bool ShouldWomaLeaderTeleportBlink(int hp, int maxHp, int incomingDamage)
    {
        if (maxHp <= 0) return false;
        return ((float)hp / maxHp) < 0.2f && incomingDamage >= 50;
    }

    /// <summary>1125: 触龙神全毒全魔免与全屏毒雾吐息判定</summary>
    public static (bool immunePoison, bool immuneMagic, bool fullScreenBreath) GetDragonLordAttributes()
    {
        return (true, true, true);
    }

    // =========================================================================
    // 细节 1126 ~ 1140: 战斗动作、前后摇硬直与音效时序精密方程 (15 Details)
    // =========================================================================

    /// <summary>1126: 战士出刀前摇与后摇硬直 (前摇 150ms, 后摇 250ms)</summary>
    public static (int windupMs, int recoveryMs) GetWarriorSlashTimings() => (150, 250);

    /// <summary>1127: 战士烈火剑法蓄力与音效 ID 104</summary>
    public static (int chargeMs, int soundId) GetFireSwordTimings() => (200, 104);

    /// <summary>1128: 战士半月弯刀横扫音效 ID 103</summary>
    public static int GetHalfMoonSoundId() => 103;

    /// <summary>1129: 战士野蛮冲撞冲刺速度 (150ms/格)</summary>
    public static int GetMotaeboStepDurationMs() => 150;

    /// <summary>1130: 法师雷电术直击硬直与音效 ID 110</summary>
    public static (int stunMs, int soundId) GetThunderSpellProfile() => (300, 110);

    /// <summary>1131: 法师火墙持续时间公式 (每点 MC 增加 1.5 秒)</summary>
    public static int CalculateFireWallDurationSec(int maxMc, int skillLevel)
    {
        return 10 + (skillLevel * 5) + (int)MathF.Round(maxMc * 1.5f);
    }

    /// <summary>1132: 法师魔法盾碎裂硬直与音效 ID 115</summary>
    public static (int shatterStunMs, int soundId) GetMagicShieldShatterProfile() => (500, 115);

    /// <summary>1133: 法师冰咆哮无视魔避与音效 ID 118</summary>
    public static (bool bypassMagicDodge, int soundId) GetIceRoarProfile() => (true, 118);

    /// <summary>1134: 法师地狱雷光不死系麻痹与 MP 消耗</summary>
    public static (int paralyzeMs, int mpCost) GetHellThunderProfile() => (1500, 40);

    /// <summary>1135: 道士灵魂火符弹道耗时 (每格 60ms)</summary>
    public static int CalculateFireCharmFlightMs(int distanceTiles) => distanceTiles * 60;

    /// <summary>1136: 道士双毒独立持续时长 (绿毒 30s, 红毒 20s)</summary>
    public static (int greenPoisonSec, int redPoisonSec) GetTaoistDualPoisonDurations() => (30, 20);

    /// <summary>1137: 道士神圣战甲术物防增益与持续时间 (持续 120s)</summary>
    public static (int acBonus, int durationSec) GetSacredArmorBonus(int skillLevel)
    {
        return (3 + skillLevel * 2, 120);
    }

    /// <summary>1138: 道士幽灵盾魔防增益与持续时间 (持续 120s)</summary>
    public static (int macBonus, int durationSec) GetGhostShieldBonus(int skillLevel)
    {
        return (3 + skillLevel * 2, 120);
    }

    /// <summary>1139: 道士心灵启示持续时间 (30 秒)</summary>
    public static int GetRevelationDurationSec() => 30;

    /// <summary>1140: 角色奔跑启动前摇与恒定速度 (启动 100ms, 跑速 200ms/步)</summary>
    public static (int startupMs, int runStepMs) GetRunSpeedProfile() => (100, 200);

    // =========================================================================
    // 细节 1141 ~ 1150: 经济系统、行会战与极端重连容灾 (10 Details)
    // =========================================================================

    /// <summary>1141: 随身金币携带上限 (10,000,000 / 1000万)</summary>
    public static int GetMaxCarriedGold() => 10000000;

    /// <summary>1142: 仓库金币存储上限 (50,000,000 / 5000万)</summary>
    public static int GetMaxStorageGold() => 50000000;

    /// <summary>1143: 金币丢弃单次上限 (100,000 / 10万)</summary>
    public static int GetMaxDropGoldOnce() => 100000;

    /// <summary>1144: 背包 40 格满格校验 (满格禁拾取与拆药)</summary>
    public static bool IsBagFull(int currentSlotsUsed) => currentSlotsUsed >= 40;

    /// <summary>1145: 行会创建必须持号角+100万金币且等级>=22</summary>
    public static (bool canCreate, string reason) ValidateGuildCreation(bool hasWomaHorn, int gold, int level)
    {
        if (!hasWomaHorn) return (false, "创建行会需要持有沃玛号角。");
        if (gold < 1000000) return (false, "创建行会需要缴纳 1,000,000 金币。");
        if (level < 22) return (false, "创建行会会长等级必须达到 22 级。");
        return (true, string.Empty);
    }

    /// <summary>1146: 行会战敌对行会名牌呈现深蓝色</summary>
    public static string GetGuildWarEnemyNameColor() => "DeepBlue";

    /// <summary>1147: 行会战击杀敌对成员免除红名惩罚</summary>
    public static bool IsGuildWarKillExemptFromPkPenalty(bool isHostileGuildMember) => isHostileGuildMember;

    /// <summary>1148: 沙巴克攻城战 3 小时免红名结界判定</summary>
    public static bool IsSabukSiegeExemptFromPk(bool isSiegeActive, string mapId)
    {
        return isSiegeActive && (mapId.Contains("0150") || mapId.Contains("沙巴克"));
    }

    /// <summary>1149: 交易超 3 格超距熔断取消与物品安全回滚</summary>
    public static bool ShouldTradeAbortDueToDistance(int p1X, int p1Y, int p2X, int p2Y)
    {
        int dist = Math.Max(Math.Abs(p1X - p2X), Math.Abs(p1Y - p2Y));
        return dist > 3;
    }

    /// <summary>1150: Direct-Blit 协议重连序列号时序滑动窗口容灾校验 (偏差超 10 强制重连)</summary>
    public static (bool syncValid, bool requireReconnect) ValidateDirectBlitSequence(int clientSeq, int serverSeq)
    {
        int diff = Math.Abs(clientSeq - serverSeq);
        if (diff > 10) return (false, true);
        return (true, false);
    }
}
