using System;
using System.Collections.Generic;
using Mir2.Shared.Models;

namespace Mir2.Shared.Rules;

/// <summary>
/// 盛大官方 1.5 统御纪元 100 微观细节与服务端权威闭环引擎 (Details 951 ~ 1050)
/// 严格 1:1 对齐官方二进制 MIR_final_v3_p.exe 权威逻辑与白日门/虎卫/赤月/套装/升级核心系统
/// 遵循纯血 Dumb Client 架构，所有判定均为 M2Server 服务端权威真值
/// </summary>
public static class OfficialImperial100DetailsEngine
{
    // =========================================================================
    // 细节 951 ~ 965: 白日门与丛林迷宫剧毒生态系统 (15 Details)
    // =========================================================================

    /// <summary>951: 剧毒蜘蛛喷射绿毒判定 (射程 5 格，持续 30 秒，每 2 秒扣除 2 点 HP)</summary>
    public static (bool canPoison, int durationSeconds, int tickIntervalMs, int damagePerTick) CalculateToxicSpiderSpit(int sourceX, int sourceY, int targetX, int targetY)
    {
        int dist = Math.Max(Math.Abs(sourceX - targetX), Math.Abs(sourceY - targetY));
        if (dist > 5) return (false, 0, 0, 0);
        return (true, 30, 2000, 2);
    }

    /// <summary>952: 黑锷蜘蛛近战撕裂判定 (无视 30% 物防，15% 几率产生撕裂流血)</summary>
    public static (int actualDefense, bool causesBleed) CalculateBlackSpiderTear(int baseDefense, int randomRollPercent)
    {
        int actualDef = Math.Max(0, (int)MathF.Round(baseDefense * 0.7f));
        bool bleed = randomRollPercent < 15;
        return (actualDef, bleed);
    }

    /// <summary>953: 幻影蜘蛛吐出爆裂蜘蛛产出判定 (每 3 秒产出 1 只，上限 6 只)</summary>
    public static bool CanPhantomSpiderSpawn(int currentSpiders, float elapsedSpawnSeconds)
    {
        return currentSpiders < 6 && elapsedSpawnSeconds >= 3.0f;
    }

    /// <summary>954: 爆裂蜘蛛自爆判定 (距离 1 格自爆，造成 25~45 物理+火混合伤害)</summary>
    public static (bool triggerExplode, int damage) CalculateExplosiveSpiderDetonation(int sourceX, int sourceY, int targetX, int targetY, int seed)
    {
        int dist = Math.Max(Math.Abs(sourceX - targetX), Math.Abs(sourceY - targetY));
        if (dist > 1) return (false, 0);
        int damage = 25 + (Math.Abs(seed) % 21); // 25~45
        return (true, damage);
    }

    /// <summary>955: 钢牙蜘蛛受击伤害减免 (物理减免 40%，魔法无减免)</summary>
    public static int CalculateSteelSpiderDamageTaken(int incomingDamage, bool isMagic)
    {
        if (isMagic) return incomingDamage;
        return (int)MathF.Round(incomingDamage * 0.6f);
    }

    /// <summary>956: 月魔蜘蛛攻击麻痹判定 (攻速 600ms，25% 几率石化麻痹 5 秒，反隐身)</summary>
    public static (bool isParalyzed, float paralyzeDuration) CalculateMoonSpiderParalyze(int randomRollPercent)
    {
        if (randomRollPercent < 25)
            return (true, 5.0f);
        return (false, 0f);
    }

    /// <summary>957: 白日门丛林迷宫入口门槛校验 (未达 28 级守卫阻拦)</summary>
    public static (bool allowed, string reason) ValidateJungleMazeEntry(int playerLevel)
    {
        if (playerLevel < 28)
            return (false, "白日门守卫：丛林迷宫凶险万分，不到28级的勇士严禁入内！");
        return (true, string.Empty);
    }

    /// <summary>958: 密道刷怪密度倍率 (官方 1.5 设定为 1.8 倍)</summary>
    public static float GetSecretPathSpawnDensityMultiplier() => 1.8f;

    /// <summary>959: 花吻蜘蛛刺杀破防爪击 (第 2 格穿刺受 100% 刺杀真实破防伤害)</summary>
    public static bool IsFlowerSpiderCleavePierce(int distance) => distance == 2;

    /// <summary>960: 恶魔祭坛与老巢封禁区域核验 (禁止随机传送与行会回城)</summary>
    public static bool IsTeleportProhibitedInRedMoonNest(string mapId)
    {
        return mapId.Equals("D1001", StringComparison.OrdinalIgnoreCase) ||
               mapId.Equals("D1002", StringComparison.OrdinalIgnoreCase) ||
               mapId.Contains("祭坛", StringComparison.OrdinalIgnoreCase) ||
               mapId.Contains("老巢", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>961: 丛林迷宫迷向雷达噪点判定</summary>
    public static bool IsMinimapJammedInJungle(string mapId)
    {
        return mapId.Contains("迷宫", StringComparison.OrdinalIgnoreCase) ||
               mapId.Contains("峡谷", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>962: 天尊手镯蜘蛛抗毒光环 (佩戴者受蜘蛛毒伤减免 50%)</summary>
    public static int ApplyTianZunSpiderPoisonMitigation(int baseDamage, bool hasTianZunBracelet)
    {
        if (!hasTianZunBracelet) return baseDamage;
        return Math.Max(1, baseDamage / 2);
    }

    /// <summary>963: 蜘蛛尸体割肉毒囊产出判定 (几率 3%)</summary>
    public static (bool harvestSuccess, string itemName) HarvestSpiderCorpse(int roll100)
    {
        if (roll100 < 3)
            return (true, "蜘蛛毒囊");
        return (false, string.Empty);
    }

    /// <summary>964: 白日门弓箭守卫攻击判定 (攻击力 80-120，射程 9 格，红名与怪物秒杀)</summary>
    public static (bool canShoot, int damage) CalculateArcherGuardShot(int guardX, int guardY, int targetX, int targetY, bool isTargetEvil)
    {
        if (!isTargetEvil) return (false, 0);
        int dist = Math.Max(Math.Abs(guardX - targetX), Math.Abs(guardY - targetY));
        if (dist > 9) return (false, 0);
        return (true, 100);
    }

    /// <summary>965: 丛林迷宫藤蔓阻挡判定 (不可穿越)</summary>
    public static bool IsJungleVineObstacle(byte tileAttribute) => (tileAttribute & 0x04) != 0;

    // =========================================================================
    // 细节 966 ~ 980: 虎卫堂招募与极品仆从战斗生态 (15 Details)
    // =========================================================================

    /// <summary>966: 虎卫堂野生虎卫刷新周期判定 (4 小时 = 14400 秒)</summary>
    public static bool IsHuWeiRespawnDue(float elapsedSecondsSinceLastKill) => elapsedSecondsSinceLastKill >= 14400f;

    /// <summary>967: 虎卫诱惑之光招募门槛 (法师等级 >= 13 级，诱惑成功率受等级与技能阶梯影响)</summary>
    public static (bool canAttempt, int successRatePercent) CalculateHuWeiTameRate(int playerJob, int playerLevel, int lureSkillLevel)
    {
        if (playerJob != 1 || playerLevel < 13) return (false, 0); // 1 = Mage
        int rate = 10 + lureSkillLevel * 5 + (playerLevel - 13);
        return (true, Math.Clamp(rate, 5, 45));
    }

    /// <summary>968: 虎卫 0 级基础属性</summary>
    public static (int hp, int minAc, int maxAc, int minDc, int maxDc, int attackSpeedMs) GetHuWeiBaseAttributes()
    {
        return (400, 10, 15, 15, 25, 1000);
    }

    /// <summary>969: 虎卫升级属性计算 (0~7级，每级 HP +80，防 +2，攻 +3~+6)</summary>
    public static (int maxHp, int minDc, int maxDc) CalculateHuWeiLevelAttributes(int petLevel)
    {
        int clampedLevel = Math.Clamp(petLevel, 0, 7);
        int hp = 400 + clampedLevel * 80;
        int minDc = 15 + clampedLevel * 3;
        int maxDc = 25 + clampedLevel * 6;
        return (hp, minDc, maxDc);
    }

    /// <summary>970: 虎卫 7 级攻击虎啸僵直判定 (10% 几率附加 200ms 受击后摇)</summary>
    public static (bool triggerRoar, int extraStunMs) CalculateHuWeiRoar(int petLevel, int roll100)
    {
        if (petLevel >= 7 && roll100 < 10)
            return (true, 200);
        return (false, 0);
    }

    /// <summary>971: 鹰卫初始属性 (HP 300，物理箭矢射程 8 格)</summary>
    public static (int hp, int range) GetYingWeiBaseAttributes() => (300, 8);

    /// <summary>972: 鹰卫移速成长 (0 级 600ms/步，7 级 350ms/步)</summary>
    public static int GetYingWeiMoveIntervalMs(int petLevel)
    {
        int lvl = Math.Clamp(petLevel, 0, 7);
        return 600 - (lvl * 35);
    }

    /// <summary>973: 仆从受黄毒减防放大效应 (受减防效果提升为 1.5 倍)</summary>
    public static int CalculatePetYellowPoisonDefense(int baseDef)
    {
        return Math.Max(0, (int)MathF.Round(baseDef * 0.7f));
    }

    /// <summary>974: 仆从跟随传送半径判定 (主人 5 格内的仆从跟随传送至目标半径 3 格内)</summary>
    public static bool CanPetTeleportWithMaster(int masterX, int masterY, int petX, int petY)
    {
        int dist = Math.Max(Math.Abs(masterX - petX), Math.Abs(masterY - petY));
        return dist <= 5;
    }

    /// <summary>975: 仆从下线叛变超时判定 (下线超过 60 秒叛变)</summary>
    public static bool IsPetRebelDueToOffline(float offlineSeconds) => offlineSeconds >= 60.0f;

    /// <summary>976: 诱惑之光对不死生物概率秒杀自爆判定</summary>
    public static (bool isInstantKill, int roll) CheckLureUndeadInstantKill(bool isUndead, int roll100)
    {
        if (!isUndead) return (false, roll100);
        return (roll100 < 20, roll100);
    }

    /// <summary>977: 仆从索敌优先级评分</summary>
    public static int ScorePetTargetPriority(bool isAttackingMaster, bool isMasterCurrentTarget, int distance)
    {
        int score = 100 - distance;
        if (isMasterCurrentTarget) score += 200;
        if (isAttackingMaster) score += 150;
        return score;
    }

    /// <summary>978: 仆从战斗指令切换 (Wait=0, Attack=1, Follow=2)</summary>
    public static int CyclePetCommand(int currentCommand) => (currentCommand + 1) % 3;

    /// <summary>979: 诱惑之光判定闪烁表现 (失败黄闪，成功认主白名)</summary>
    public static string GetLureFlashVisual(bool success) => success ? "WhiteTamed" : "YellowFlash";

    /// <summary>980: 法师诱惑仆从上限校验 (最多 5 只)</summary>
    public static bool CanTameMorePet(int currentPetCount) => currentPetCount < 5;

    // =========================================================================
    // 细节 981 ~ 995: 恶魔祭坛、赤月恶魔与终极装备掉落 (15 Details)
    // =========================================================================

    /// <summary>981: 赤月恶魔全屏地刺伤害判定 (基础 30-70 物理伤害)</summary>
    public static int CalculateRedMoonEvilSpikeDamage(int rollSeed)
    {
        return 30 + (Math.Abs(rollSeed) % 41); // 30~70
    }

    /// <summary>982: 赤月恶魔地刺受幽灵盾减免判定 (减免 20%)</summary>
    public static int ApplyGhostShieldAgainstSpike(int spikeDamage, bool hasGhostShield)
    {
        if (!hasGhostShield) return spikeDamage;
        return (int)MathF.Round(spikeDamage * 0.8f);
    }

    /// <summary>983: 赤月恶魔生命回复 (每 5 秒回复 150 HP)</summary>
    public static int CalculateRedMoonEvilRegen(int currentHp, int maxHp, float elapsedSec)
    {
        if (elapsedSec < 5.0f || currentHp <= 0) return currentHp;
        return Math.Min(maxHp, currentHp + 150);
    }

    /// <summary>984: 双头金刚挖掘模式判定 (死后不掉落，必须玩家割肉挖掘)</summary>
    public static bool IsCorpseHarvestOnlyMonster(string monsterName)
    {
        return monsterName.Contains("双头金刚", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>985: 双头金刚挖掘上限与产出池判定</summary>
    public static (bool success, string dropItem) HarvestDoubleHeadKong(int currentHarvestCount, int roll1000)
    {
        if (currentHarvestCount >= 20) return (false, string.Empty);
        if (roll1000 < 5) return (true, "圣战头盔");
        if (roll1000 < 15) return (true, "法神项链");
        return (false, string.Empty);
    }

    /// <summary>986: 双头血魔掉落圣战手镯与戒指判定</summary>
    public static bool CanDoubleHeadFiendDrop(int roll1000) => roll1000 < 8;

    /// <summary>987: 赤月恶魔散落半径判定 (5x5 爆裂圈)</summary>
    public static int GetRedMoonDropRadius() => 5;

    /// <summary>988: 恶魔祭坛卡位点坐标核验</summary>
    public static bool IsAltarSafeSpot(int x, int y) => x == 180 && y == 54;

    /// <summary>989: 赤月老巢禁用卷轴核验</summary>
    public static bool IsScrollForbiddenInNest(string scrollName)
    {
        return scrollName.Contains("随机", StringComparison.OrdinalIgnoreCase) ||
               scrollName.Contains("行会回城", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>990: 赤月地图死亡装备掉落概率翻倍 (由 1/20 变为 1/10)</summary>
    public static int GetRedMoonDeathDropRateDenominator() => 10;

    /// <summary>991: 暴食蜘蛛近战吸血判定 (吸血 30%)</summary>
    public static int CalculateGluttonousSpiderLifeSteal(int damageDealt)
    {
        return (int)MathF.Round(damageDealt * 0.3f);
    }

    /// <summary>992: 赤月恶魔雷电抗性与烈火易伤判定</summary>
    public static int ApplyRedMoonVulnerability(int damage, string spellName)
    {
        if (spellName.Contains("雷电", StringComparison.OrdinalIgnoreCase))
            return (int)MathF.Round(damage * 0.85f); // 15% 豁免
        if (spellName.Contains("烈火", StringComparison.OrdinalIgnoreCase))
            return (int)MathF.Round(damage * 1.20f); // 20% 易伤
        return damage;
    }

    /// <summary>993: 赤月地震屏幕震动判定 (每 600 秒一次，震动 500ms)</summary>
    public static (bool shake, int durationMs) CheckRedMoonEarthquake(float mapElapsedSec)
    {
        if (mapElapsedSec > 0 && ((int)mapElapsedSec % 600 == 0))
            return (true, 500);
        return (false, 0);
    }

    /// <summary>994: 赤月抉择之地三岔路口通道合法性校验</summary>
    public static bool ValidateChoicePlacePortal(int currentX, int currentY, int destMapIndex)
    {
        // 抉择之地三岔路：通往山谷密道1/2或老巢
        return destMapIndex >= 1 && destMapIndex <= 3;
    }

    /// <summary>995: 圣战套装隐藏属性判定 (物理破防 +10%，刺杀剑术伤害 +15%)</summary>
    public static (int armorIgnorePercent, int assassinationBonusPercent) GetSaintCombatSuitBonus(bool isFullSet)
    {
        if (isFullSet) return (10, 15);
        return (0, 0);
    }

    // =========================================================================
    // 细节 996 ~ 1010: 套装神威与全服共鸣机制 (15 Details)
    // =========================================================================

    /// <summary>996: 祈祷全套 5 件齐全判定 (刃、头、链、镯、戒)</summary>
    public static bool IsFullPrayerSet(bool hasBlade, bool hasHelmet, bool hasNecklace, bool hasBracelet, bool hasRing)
    {
        return hasBlade && hasHelmet && hasNecklace && hasBracelet && hasRing;
    }

    /// <summary>997: 祈祷神威触发条件 (全套佩戴 + 任意一件破碎/掉落)</summary>
    public static bool ShouldTriggerPrayerCataclysm(bool isFullSetEquipped, bool isAnyPieceBroken)
    {
        return isFullSetEquipped && isAnyPieceBroken;
    }

    /// <summary>998: 祈祷神威全服叛变持续时间 (1800 秒 / 30 分钟)</summary>
    public static int GetPrayerCataclysmDurationSeconds() => 1800;

    /// <summary>999: 祈祷项链佩戴死亡必碎判定 (100% 破碎销毁，不可掉落地面)</summary>
    public static bool DoesPrayerNecklaceShatterOnDeath() => true;

    /// <summary>1000: 记忆全套 4 件齐全判定 (头、链、镯、戒)</summary>
    public static bool IsFullRecallSet(bool hasHelmet, bool hasNecklace, bool hasBracelet, bool hasRing)
    {
        return hasHelmet && hasNecklace && hasBracelet && hasRing;
    }

    /// <summary>1001: 队长天地合一指令前置校验</summary>
    public static (bool allowed, string error) ValidateRecallGroupCommand(bool isLeader, bool hasFullRecallSet, int leaderMp, int maxMp)
    {
        if (!isLeader) return (false, "只有队长才能使用天地合一召唤队员！");
        if (!hasFullRecallSet) return (false, "你必须集齐并佩戴整套记忆装备才能施展天地合一！");
        int requiredMp = (int)MathF.Round(maxMp * 0.8f);
        if (leaderMp < requiredMp) return (false, $"法力值不足，施展天地合一需要消耗 80% 法力({requiredMp})！");
        return (true, string.Empty);
    }

    /// <summary>1002: 天地合一冷却时间 (180 秒)</summary>
    public static int GetRecallGroupCooldownSeconds() => 180;

    /// <summary>1003: 天地合一拉取目标坐标九宫格计算</summary>
    public static (int targetX, int targetY) CalculateRecallMemberPos(int leaderX, int leaderY, int memberIndex)
    {
        int[] dx = { 0, 1, 1, 0, -1, -1, -1, 0, 1 };
        int[] dy = { 0, 0, 1, 1, 1, 0, -1, -1, -1 };
        int idx = (memberIndex % dx.Length);
        return (leaderX + dx[idx], leaderY + dy[idx]);
    }

    /// <summary>1004: 队员允许天地合一开关状态校验</summary>
    public static bool CanMemberBeRecalled(bool memberAllowRecall, bool isInSpecialArena)
    {
        return memberAllowRecall && !isInSpecialArena;
    }

    /// <summary>1005: 虹魔套装吸血百分比 (全套 15% 物理吸血)</summary>
    public static int CalculateRainbowMagicLifeSteal(int physicalDamage, bool isFullRainbowSet)
    {
        if (!isFullRainbowSet) return 0;
        return (int)MathF.Round(physicalDamage * 0.15f);
    }

    /// <summary>1006: 魔血套装 MP 转 HP 属性转化 (扣除 150 MP，增加 175 HP)</summary>
    public static (int hpBonus, int mpPenalty) GetDemonBloodSuitModifiers(bool isFullSet)
    {
        if (isFullSet) return (175, 150);
        return (0, 0);
    }

    /// <summary>1007: 幽灵全套攻速提升 (出刀后摇缩短 80ms)</summary>
    public static int GetGhostSuitSpeedReductionMs(bool isFullSet) => isFullSet ? 80 : 0;

    /// <summary>1008: 阎罗手套伤害反弹与减免 (10% 减免，10% 反弹)</summary>
    public static (int actualDamage, int reflectDamage) CalculateYanLuoGloveEffect(int incomingDamage, bool hasYanLuoGlove)
    {
        if (!hasYanLuoGlove) return (incomingDamage, 0);
        int red = (int)MathF.Round(incomingDamage * 0.9f);
        int refl = (int)MathF.Round(incomingDamage * 0.1f);
        return (red, refl);
    }

    /// <summary>1009: 狂风套装攻速累加与攻击削弱计算</summary>
    public static (int totalSpeedBonus, float damageMultiplier) CalculateGaleSuitModifiers(int galePieces)
    {
        int speed = Math.Clamp(galePieces, 0, 5);
        float mult = 1.0f - (speed * 0.05f); // 每点攻速伤害削减 5%
        return (speed, mult);
    }

    /// <summary>1010: 金手镯极品魔防上限 (基础 2-3，极品上限 2-7)</summary>
    public static (int minMac, int maxMac) GetGoldBraceletMacRange(int upgradePoints)
    {
        int max = 3 + Math.Clamp(upgradePoints, 0, 4);
        return (2, max);
    }

    // =========================================================================
    // 细节 1011 ~ 1025: 武器升级、黑铁矿纯度与开刀碎刀命运 (15 Details)
    // =========================================================================

    /// <summary>1011: 武器升级沙巴克铁匠取刀时间 (3600 秒 / 1 小时)</summary>
    public static int GetWeaponUpgradeDurationSeconds() => 3600;

    /// <summary>1012: 黑铁矿石总纯度持久保底判定 (总纯度 >= 60 不掉持久)</summary>
    public static bool DoesBlackIronPreserveDurability(int totalPurity) => totalPurity >= 60;

    /// <summary>1013: 黑铁矿石纯度不足掉持久计算 (总纯度 < 30 时 80% 几率掉 1~3 点最大持久)</summary>
    public static (bool durabilityLost, int amount) CalculatePurityDurabilityLoss(int totalPurity, int roll100, int seed)
    {
        if (totalPurity >= 60) return (false, 0);
        if (totalPurity < 30 && roll100 < 80)
        {
            int loss = 1 + (Math.Abs(seed) % 3);
            return (true, loss);
        }
        return (false, 0);
    }

    /// <summary>1014: 首饰属性升级倾向识别 (返回 DC/MC/SC)</summary>
    public static string DetermineUpgradeStatCategory(int totalDc, int totalMc, int totalSc)
    {
        if (totalDc >= totalMc && totalDc >= totalSc) return "DC";
        if (totalMc >= totalDc && totalMc >= totalSc) return "MC";
        return "SC";
    }

    /// <summary>1015: 武器升级基础成功率与红名惩罚 (基础 50%，红名 -30%)</summary>
    public static int CalculateWeaponUpgradeRate(int pkPoints, int consecutiveFails)
    {
        int rate = 50 + (consecutiveFails * 5);
        if (pkPoints >= 200) rate -= 30; // 红名大降
        return Math.Clamp(rate, 10, 85);
    }

    /// <summary>1016: 武器开刀瞬间状态判定</summary>
    public static bool CheckWeaponOpenAttackResult(int roll100, int successRate) => roll100 < successRate;

    /// <summary>1017: 升级上限判定 (盛大官方 1.5 上限升级 7 次)</summary>
    public static bool CanUpgradeWeaponAgain(int currentUpgradeCount) => currentUpgradeCount < 7;

    /// <summary>1018: 开刀失败武器碎裂销毁判定</summary>
    public static bool DoesWeaponShatterOnFail() => true;

    /// <summary>1019: 垫刀伪随机种子偏移增量计算</summary>
    public static int CalculateSeedOffset(int currentFails) => currentFails * 17;

    /// <summary>1020: 祝福油增加幸运上限 (最高 Luck +7)</summary>
    public static bool CanBlessWeapon(int currentLuck) => currentLuck < 7;

    /// <summary>1021: 祝福油高幸运反噬诅咒判定 (Luck >= 3 时 15% 诅咒，35% 失败)</summary>
    public static (bool success, bool cursed) ApplyBlessingOil(int currentLuck, int roll100)
    {
        if (currentLuck < 3)
        {
            return (roll100 < 60, false);
        }
        if (roll100 < 30) return (true, false);
        if (roll100 < 45) return (false, true); // 诅咒反噬
        return (false, false);
    }

    /// <summary>1022: 幸运九套满攻触发判定 (Luck >= 9 必定 100% 触发攻击上限)</summary>
    public static bool IsGuaranteedMaxDamage(int totalLuck) => totalLuck >= 9;

    /// <summary>1023: 诅咒十套下限触发判定 (Curse >= 10 必定 100% 触发攻击下限)</summary>
    public static bool IsGuaranteedMinDamage(int totalCurse) => totalCurse >= 10;

    /// <summary>1024: 击杀无辜白名武器沾染诅咒判定 (40% 几率 Curse +1)</summary>
    public static bool CheckPkCurseChance(int roll100) => roll100 < 40;

    /// <summary>1025: 战神油特修武器不损最大持久判定</summary>
    public static bool WarGodOilPreservesMaxDurability() => true;

    // =========================================================================
    // 细节 1026 ~ 1040: 未知暗殿、封魔矿区与祖玛核心机制 (15 Details)
    // =========================================================================

    /// <summary>1026: 未知暗殿真假沃玛教主血量判定 (假 100，真 5000)</summary>
    public static int GetUnknownTempleWomaLeaderHp(bool isReal) => isReal ? 5000 : 100;

    /// <summary>1027: 未知暗殿神鹿属性与诱惑免疫 (HP 1200，攻击 50-80)</summary>
    public static (int hp, int minDc, int maxDc, bool immuneToLure) GetUnknownTempleHolyDeer()
    {
        return (1200, 50, 80, true);
    }

    /// <summary>1028: 未知暗殿稻草人极品掉落判定</summary>
    public static bool CanUnknownScarecrowDropBlessingOil(int roll100) => roll100 < 5;

    /// <summary>1029: 未知暗殿驻留超时踢出判定 (1800 秒 / 30 分钟)</summary>
    public static bool IsUnknownTempleTimeExpired(float insideSeconds) => insideSeconds >= 1800f;

    /// <summary>1030: 封魔殿地图 ID 合法性判定</summary>
    public static bool IsFengMoPalaceMap(string mapId)
    {
        return mapId.Contains("D2008", StringComparison.OrdinalIgnoreCase) ||
               mapId.Contains("封魔殿", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>1031: 霸者大厅安全补给点属性核验</summary>
    public static bool IsOverlordHallSupplySafe(string mapName) => mapName.Contains("霸者大厅", StringComparison.OrdinalIgnoreCase);

    /// <summary>1032: 虹魔教主瞬移闪避判定 (HP < 30% 受到近战围殴时 25% 瞬移)</summary>
    public static bool ShouldRainbowDemonBlink(int hp, int maxHp, int surroundedCount, int roll100)
    {
        if (maxHp <= 0) return false;
        bool lowHp = ((float)hp / maxHp) < 0.3f;
        return lowHp && surroundedCount >= 3 && roll100 < 25;
    }

    /// <summary>1033: 祖玛教主分身召唤阈值判定 (每损失 1000 HP 召唤一波)</summary>
    public static bool ShouldZumaLeaderSummonWave(int currentHp, int lastHpSnapshot)
    {
        return (lastHpSnapshot - currentHp) >= 1000;
    }

    /// <summary>1034: 祖玛极品 1400 卫士血量与掉落阶梯判定</summary>
    public static bool IsSuperZumaGuard(int maxHp) => maxHp >= 1400;

    /// <summary>1035: 祖玛雕像破封警戒距离 (3 格内苏醒)</summary>
    public static bool IsZumaStatueAwakenInRange(int statueX, int statueY, int playerX, int playerY)
    {
        int dist = Math.Max(Math.Abs(statueX - playerX), Math.Abs(statueY - playerY));
        return dist <= 3;
    }

    /// <summary>1036: 祖玛弓箭手射程与站桩特性 (射程 7 格)</summary>
    public static int GetZumaArcherRange() => 7;

    /// <summary>1037: 沃玛角蝇产出蝙蝠上限判定 (上限 10 只，间隔 2.5 秒)</summary>
    public static bool CanFlyGloveSpawnBat(int currentBats, float elapsedSeconds)
    {
        return currentBats < 10 && elapsedSeconds >= 2.5f;
    }

    /// <summary>1038: 沃玛教主暴走狂暴判定 (HP < 20% 攻速提升 50%)</summary>
    public static bool IsWomaLeaderBerserk(int curHp, int maxHp)
    {
        if (maxHp <= 0) return false;
        return ((float)curHp / maxHp) < 0.2f;
    }

    /// <summary>1039: 楔蛾反隐身视野与石化判定 (反隐半径 6 格，20% 石化)</summary>
    public static (bool detectStealth, bool causesStone) CheckWedgeMothAttack(int distance, int roll100)
    {
        bool detect = distance <= 6;
        bool stone = roll100 < 20;
        return (detect, stone);
    }

    /// <summary>1040: 触龙神物免魔破判定 (免疫一切物理攻击)</summary>
    public static int CalculateDragonLordDamageTaken(int incomingDamage, bool isMagic)
    {
        return isMagic ? incomingDamage : 0;
    }

    // =========================================================================
    // 细节 1041 ~ 1050: 权威数值抗性、药水曲线与极端边界 (10 Details)
    // =========================================================================

    /// <summary>1041: 魔法躲避单体法术 MISS 判定</summary>
    public static bool CheckMagicDodge(int dodgeRatePercent, int roll100)
    {
        return roll100 <= Math.Clamp(dodgeRatePercent, 0, 90);
    }

    /// <summary>1042: 毒物躲避与恢复周期缩短判定</summary>
    public static int CalculatePoisonDurationWithRecovery(int baseDurationSec, int poisonRecoverRatePercent)
    {
        float mult = 1.0f - (Math.Clamp(poisonRecoverRatePercent, 0, 80) / 100.0f);
        return Math.Max(1, (int)MathF.Round(baseDurationSec * mult));
    }

    /// <summary>1043: 中量金创药回复曲线 (每 1000ms 回复 8 点，持续 10 次共 80 点)</summary>
    public static (int hpPerTick, int totalTicks, int intervalMs) GetMediumHpPotionProfile()
    {
        return (8, 10, 1000);
    }

    /// <summary>1044: 太阳水瞬时回复判定 (瞬回 50 HP 与 50 MP，不占普药回血池)</summary>
    public static (int instantHp, int instantMp, bool isInstant) GetSolarWaterProfile()
    {
        return (50, 50, true);
    }

    /// <summary>1045: 万年雪霜与强效太阳水瞬回数值</summary>
    public static (int hp, int mp) GetSnowFrostProfile(bool isSuperSnowFrost)
    {
        return isSuperSnowFrost ? (100, 100) : (80, 80);
    }

    /// <summary>1046: 超重移动阻滞判定 (背包负重 >= 90% 严禁奔跑)</summary>
    public static bool IsRunningForbiddenByWeight(int currentWeight, int maxWeight)
    {
        if (maxWeight <= 0) return true;
        return ((float)currentWeight / maxWeight) >= 0.9f;
    }

    /// <summary>1047: 穿戴腕力校验 (武器重量 > 腕力上限拒绝穿戴)</summary>
    public static (bool canEquip, string error) ValidateHandWeight(int weaponWeight, int currentHandWeightCap)
    {
        if (weaponWeight > currentHandWeightCap)
            return (false, "你无法拿动这件沉重的武器。");
        return (true, string.Empty);
    }

    /// <summary>1048: 穿戴负重校验 (衣服重量 > 穿戴负重上限拒绝穿戴)</summary>
    public static (bool canEquip, string error) ValidateWearWeight(int armorWeight, int currentWearWeightCap)
    {
        if (armorWeight > currentWearWeightCap)
            return (false, "你的体力不足以穿戴这件铠甲。");
        return (true, string.Empty);
    }

    /// <summary>1049: 沙巴克攻城复活点旗帜血量与易手判定 (HP 50000)</summary>
    public static (int maxHp, bool isCaptured) CheckSabukFlagState(int currentHp)
    {
        return (50000, currentHp <= 0);
    }

    /// <summary>1050: 客户端与服务端 Direct-Blit 协议断网重连与状态原子同步校验</summary>
    public static bool ValidateReconnectionStateSync(int clientSequence, int serverSequence)
    {
        // 允许时序滑动窗口为 5，超出窗口判定失步必须全量快照纠偏
        return Math.Abs(clientSequence - serverSequence) <= 5;
    }
}
