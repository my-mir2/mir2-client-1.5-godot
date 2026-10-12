using System;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 盛大官方 1.5 统治纪元 100 细节与权威服务器规则自动化测试矩阵 (Details 1051 ~ 1150)
/// 严格 1:1 验证白日门/赤月峡谷/套装极致/武器升级与Dumb Client边界
/// </summary>
public class TestOfficialWave89_100DetailsDominionMatrix
{
    // =========================================================================
    // 细节 1051 ~ 1065: 白日门天尊道观、虎卫堂与道教文化深层交互测试
    // =========================================================================

    [Fact]
    public void TestDetail1051_TaoistTempleSafeZone()
    {
        Assert.True(OfficialSovereignDominion100DetailsEngine.IsTaoistTempleSafeZone(75, 45));
        Assert.False(OfficialSovereignDominion100DetailsEngine.IsTaoistTempleSafeZone(100, 100));
    }

    [Fact]
    public void TestDetail1052_HuWeiQuestRequirement()
    {
        var (succ, _) = OfficialSovereignDominion100DetailsEngine.ValidateHuWeiQuestRequirement(35, 50_000);
        Assert.True(succ);

        var (failLvl, _) = OfficialSovereignDominion100DetailsEngine.ValidateHuWeiQuestRequirement(34, 50_000);
        Assert.False(failLvl);

        var (failGold, _) = OfficialSovereignDominion100DetailsEngine.ValidateHuWeiQuestRequirement(36, 10_000);
        Assert.False(failGold);
    }

    [Fact]
    public void TestDetail1053_SpecialJadeBottleProfile()
    {
        var (rec, interval, ticks) = OfficialSovereignDominion100DetailsEngine.GetSpecialJadeBottleProfile();
        Assert.Equal(12, rec);
        Assert.Equal(1000, interval);
        Assert.Equal(10, ticks);
    }

    [Fact]
    public void TestDetail1054_SpecialPoisonMaxDura()
    {
        Assert.Equal(500, OfficialSovereignDominion100DetailsEngine.GetSpecialPoisonMaxDura());
    }

    [Fact]
    public void TestDetail1055_SecretPassageExit()
    {
        var (x1, y1) = OfficialSovereignDominion100DetailsEngine.GetSecretPassageExit(352, 241);
        Assert.Equal(10, x1);
        Assert.Equal(15, y1);

        var (x2, y2) = OfficialSovereignDominion100DetailsEngine.GetSecretPassageExit(10, 15);
        Assert.Equal(352, x2);
        Assert.Equal(241, y2);
    }

    [Fact]
    public void TestDetail1056_HuWeiRespawnBroadcast()
    {
        string text = OfficialSovereignDominion100DetailsEngine.GetHuWeiRespawnBroadcast();
        Assert.Contains("虎卫已然诞生", text);
    }

    [Fact]
    public void TestDetail1057_YingWeiArrowFlightTime()
    {
        Assert.Equal(45, OfficialSovereignDominion100DetailsEngine.CalculateYingWeiArrowFlightTimeMs(1));
        Assert.Equal(225, OfficialSovereignDominion100DetailsEngine.CalculateYingWeiArrowFlightTimeMs(5));
    }

    [Fact]
    public void TestDetail1058_HuWeiMaxAttackCrit()
    {
        Assert.True(OfficialSovereignDominion100DetailsEngine.IsHuWeiMaxAttackCrit(25, 25));
        Assert.False(OfficialSovereignDominion100DetailsEngine.IsHuWeiMaxAttackCrit(20, 25));
    }

    [Fact]
    public void TestDetail1059_ShinsuFireBreathSector()
    {
        Assert.True(OfficialSovereignDominion100DetailsEngine.IsInShinsuFireBreathSector(10, 10, 12, 11, 2));
        Assert.False(OfficialSovereignDominion100DetailsEngine.IsInShinsuFireBreathSector(10, 10, 15, 15, 2));
    }

    [Fact]
    public void TestDetail1060_ShinsuMoveInterval()
    {
        Assert.Equal(600, OfficialSovereignDominion100DetailsEngine.GetShinsuMoveIntervalMs(0));
        Assert.Equal(404, OfficialSovereignDominion100DetailsEngine.GetShinsuMoveIntervalMs(7));
    }

    [Fact]
    public void TestDetail1061_BerserkLureResistance()
    {
        int normal = OfficialSovereignDominion100DetailsEngine.CalculateBerserkLureResistance(20, false);
        int berserk = OfficialSovereignDominion100DetailsEngine.CalculateBerserkLureResistance(20, true);
        Assert.Equal(20, normal);
        Assert.Equal(30, berserk);
    }

    [Fact]
    public void TestDetail1062_PetSheatheWeaponInSafeZone()
    {
        Assert.True(OfficialSovereignDominion100DetailsEngine.ShouldPetSheatheWeaponInSafeZone(true));
        Assert.False(OfficialSovereignDominion100DetailsEngine.ShouldPetSheatheWeaponInSafeZone(false));
    }

    [Fact]
    public void TestDetail1063_JungleSpecialMonsterAttack()
    {
        Assert.Equal(130, OfficialSovereignDominion100DetailsEngine.GetJungleSpecialMonsterAttack(100));
    }

    [Fact]
    public void TestDetail1064_HuWeiHallGateNarrowChokepoint()
    {
        Assert.True(OfficialSovereignDominion100DetailsEngine.IsHuWeiHallGateNarrowChokepoint(25, 20));
        Assert.False(OfficialSovereignDominion100DetailsEngine.IsHuWeiHallGateNarrowChokepoint(10, 10));
    }

    [Fact]
    public void TestDetail1065_HuWeiIntimidationAura()
    {
        int aura5 = OfficialSovereignDominion100DetailsEngine.ApplyHuWeiIntimidationAura(5, 5);
        int aura2 = OfficialSovereignDominion100DetailsEngine.ApplyHuWeiIntimidationAura(5, 2);
        Assert.Equal(3, aura5);
        Assert.Equal(5, aura2);
    }

    // =========================================================================
    // 细节 1066 ~ 1080: 赤月峡谷老巢深水区与恶魔祭坛测试
    // =========================================================================

    [Fact]
    public void TestDetail1066_ChoicePlacePortal()
    {
        Assert.True(OfficialSovereignDominion100DetailsEngine.IsChoicePlacePortal(145, 110));
        Assert.False(OfficialSovereignDominion100DetailsEngine.IsChoicePlacePortal(100, 100));
    }

    [Fact]
    public void TestDetail1067_ValleySecretPassageExpectedMonsters()
    {
        Assert.Equal(18, OfficialSovereignDominion100DetailsEngine.GetValleySecretPassageExpectedMonsters(10));
    }

    [Fact]
    public void TestDetail1068_AltarEntranceStunDuration()
    {
        Assert.Equal(500, OfficialSovereignDominion100DetailsEngine.GetAltarEntranceStunDurationMs());
    }

    [Fact]
    public void TestDetail1069_RedMoonSpikeFriendlyFire()
    {
        Assert.Equal(50, OfficialSovereignDominion100DetailsEngine.CalculateRedMoonSpikeFriendlyFire(100));
    }

    [Fact]
    public void TestDetail1070_DualBossAggroLink()
    {
        Assert.True(OfficialSovereignDominion100DetailsEngine.ShouldTriggerDualBossAggroLink(10, 10, 15, 15));
        Assert.False(OfficialSovereignDominion100DetailsEngine.ShouldTriggerDualBossAggroLink(10, 10, 30, 30));
    }

    [Fact]
    public void TestDetail1071_DoubleHeadKongPhysMitigation()
    {
        Assert.Equal(20, OfficialSovereignDominion100DetailsEngine.ApplyDoubleHeadKongPhysMitigation(100));
    }

    [Fact]
    public void TestDetail1072_DoubleHeadFiendMagicMitigation()
    {
        Assert.Equal(20, OfficialSovereignDominion100DetailsEngine.ApplyDoubleHeadFiendMagicMitigation(100));
    }

    [Fact]
    public void TestDetail1073_RedMoonSpikeInterval()
    {
        Assert.Equal(1200, OfficialSovereignDominion100DetailsEngine.GetRedMoonSpikeIntervalMs(100, 1000));
        Assert.Equal(2000, OfficialSovereignDominion100DetailsEngine.GetRedMoonSpikeIntervalMs(500, 1000));
    }

    [Fact]
    public void TestDetail1074_ExplosiveSpidersDetonateOnHostDeath()
    {
        Assert.True(OfficialSovereignDominion100DetailsEngine.ShouldExplosiveSpidersDetonateOnHostDeath(true));
        Assert.False(OfficialSovereignDominion100DetailsEngine.ShouldExplosiveSpidersDetonateOnHostDeath(false));
    }

    [Fact]
    public void TestDetail1075_BlackSpiderIgnoreGhostShield()
    {
        Assert.True(OfficialSovereignDominion100DetailsEngine.DoesBlackSpiderIgnoreGhostShield());
    }

    [Fact]
    public void TestDetail1076_DiminishingStoneDuration()
    {
        float d0 = OfficialSovereignDominion100DetailsEngine.CalculateDiminishingStoneDuration(5.0f, 0);
        float d1 = OfficialSovereignDominion100DetailsEngine.CalculateDiminishingStoneDuration(5.0f, 1);
        Assert.Equal(5.0f, d0);
        Assert.Equal(4.0f, d1);
    }

    [Fact]
    public void TestDetail1077_GluttonousSpiderRegenCap()
    {
        int capped = OfficialSovereignDominion100DetailsEngine.CalculateGluttonousSpiderRegenCap(1000, 500);
        Assert.Equal(50, capped); // 500 * 10% = 50
    }

    [Fact]
    public void TestDetail1078_GroupInvisDurationInRedMoon()
    {
        Assert.Equal(15, OfficialSovereignDominion100DetailsEngine.GetGroupInvisDurationInRedMoon(30));
    }

    [Fact]
    public void TestDetail1079_MotaeboPushSpider()
    {
        Assert.True(OfficialSovereignDominion100DetailsEngine.CanMotaeboPushSpider(40, 35));
        Assert.False(OfficialSovereignDominion100DetailsEngine.CanMotaeboPushSpider(30, 35));
    }

    [Fact]
    public void TestDetail1080_RedMoonPoisonMiasmaRegen()
    {
        Assert.Equal(5, OfficialSovereignDominion100DetailsEngine.ApplyRedMoonPoisonMiasmaRegen(10));
    }

    // =========================================================================
    // 细节 1081 ~ 1095: 祈祷/记忆/虹魔/魔血/狂风装备与特戒测试
    // =========================================================================

    [Fact]
    public void TestDetail1081_PrayerBraceletBonus()
    {
        var (agil, dodge) = OfficialSovereignDominion100DetailsEngine.GetPrayerBraceletBonus();
        Assert.Equal(2, agil);
        Assert.Equal(10, dodge);
    }

    [Fact]
    public void TestDetail1082_PrayerRingShatter()
    {
        Assert.True(OfficialSovereignDominion100DetailsEngine.DoesPrayerRingShatterOnDeath());
    }

    [Fact]
    public void TestDetail1083_PrayerBladeBonus()
    {
        var (acc, speed) = OfficialSovereignDominion100DetailsEngine.GetPrayerBladeBonus();
        Assert.Equal(3, acc);
        Assert.Equal(60, speed);
    }

    [Fact]
    public void TestDetail1084_RecallHelmetStats()
    {
        var (min, max, all) = OfficialSovereignDominion100DetailsEngine.GetRecallHelmetStats();
        Assert.Equal(3, min);
        Assert.Equal(3, max);
        Assert.True(all);
    }

    [Fact]
    public void TestDetail1085_RecallBraceletStats()
    {
        var (min, max) = OfficialSovereignDominion100DetailsEngine.GetRecallBraceletStats();
        Assert.Equal(1, min);
        Assert.Equal(1, max);
    }

    [Fact]
    public void TestDetail1086_RecallRingLocateGroup()
    {
        Assert.True(OfficialSovereignDominion100DetailsEngine.DoesRecallRingEnableLocateGroup());
    }

    [Fact]
    public void TestDetail1087_RainbowNecklaceLifeSteal()
    {
        Assert.Equal(5, OfficialSovereignDominion100DetailsEngine.GetRainbowNecklaceLifeStealPercent());
    }

    [Fact]
    public void TestDetail1088_RainbowBraceletLifeSteal()
    {
        Assert.Equal(5, OfficialSovereignDominion100DetailsEngine.GetRainbowBraceletLifeStealPercent());
    }

    [Fact]
    public void TestDetail1089_RainbowRingLifeSteal()
    {
        Assert.Equal(5, OfficialSovereignDominion100DetailsEngine.GetRainbowRingLifeStealPercent());
    }

    [Fact]
    public void TestDetail1090_DemonBloodBraceletModifiers()
    {
        var (hp, mp) = OfficialSovereignDominion100DetailsEngine.GetDemonBloodBraceletModifiers();
        Assert.Equal(15, hp);
        Assert.Equal(15, mp);
    }

    [Fact]
    public void TestDetail1091_DemonBloodNecklaceModifiers()
    {
        var (hp, mp) = OfficialSovereignDominion100DetailsEngine.GetDemonBloodNecklaceModifiers();
        Assert.Equal(25, hp);
        Assert.Equal(25, mp);
    }

    [Fact]
    public void TestDetail1092_DemonBloodRingModifiers()
    {
        var (hp, mp) = OfficialSovereignDominion100DetailsEngine.GetDemonBloodRingModifiers();
        Assert.Equal(15, hp);
        Assert.Equal(15, mp);
    }

    [Fact]
    public void TestDetail1093_GaleNecklaceAttackDelay()
    {
        Assert.Equal(160, OfficialSovereignDominion100DetailsEngine.GetGaleNecklaceAttackDelayMs());
    }

    [Fact]
    public void TestDetail1094_GaleRingAttackDelay()
    {
        Assert.Equal(80, OfficialSovereignDominion100DetailsEngine.GetGaleRingAttackDelayMs());
    }

    [Fact]
    public void TestDetail1095_ParalyzeRingOnBoss()
    {
        var (paraBoss, durBoss) = OfficialSovereignDominion100DetailsEngine.EvaluateParalyzeRingOnBoss(true, 5);
        Assert.True(paraBoss);
        Assert.Equal(1.0f, durBoss);

        var (paraNormal, durNormal) = OfficialSovereignDominion100DetailsEngine.EvaluateParalyzeRingOnBoss(false, 30);
        Assert.True(paraNormal);
        Assert.Equal(5.0f, durNormal);
    }

    // =========================================================================
    // 细节 1096 ~ 1110: 武器升级高阶概率测试
    // =========================================================================

    [Fact]
    public void TestDetail1096_SabukOwnerRepairCost()
    {
        Assert.Equal(50, OfficialSovereignDominion100DetailsEngine.CalculateSabukOwnerRepairCost(100, true));
        Assert.Equal(100, OfficialSovereignDominion100DetailsEngine.CalculateSabukOwnerRepairCost(100, false));
    }

    [Fact]
    public void TestDetail1097_WeaponUpgradeGoldFee()
    {
        Assert.Equal(80000, OfficialSovereignDominion100DetailsEngine.GetWeaponUpgradeGoldFee(true));
        Assert.Equal(100000, OfficialSovereignDominion100DetailsEngine.GetWeaponUpgradeGoldFee(false));
    }

    [Fact]
    public void TestDetail1098_WeaponUpgradeClaimExpired()
    {
        Assert.True(OfficialSovereignDominion100DetailsEngine.IsWeaponUpgradeClaimExpired(604800f));
        Assert.False(OfficialSovereignDominion100DetailsEngine.IsWeaponUpgradeClaimExpired(500000f));
    }

    [Fact]
    public void TestDetail1099_BlackIronIncreaseMaxDura()
    {
        Assert.True(OfficialSovereignDominion100DetailsEngine.DoesBlackIronIncreaseMaxDura(95, 10));
        Assert.False(OfficialSovereignDominion100DetailsEngine.DoesBlackIronIncreaseMaxDura(85, 10));
    }

    [Fact]
    public void TestDetail1100_UpgradeStatGain()
    {
        Assert.Equal(2, OfficialSovereignDominion100DetailsEngine.CalculateUpgradeStatGain(250, 5));
        Assert.Equal(1, OfficialSovereignDominion100DetailsEngine.CalculateUpgradeStatGain(150, 5));
    }

    [Fact]
    public void TestDetail1101_ClampWeaponStatBonus()
    {
        Assert.Equal(7, OfficialSovereignDominion100DetailsEngine.ClampWeaponStatBonus(6, 2));
        Assert.Equal(5, OfficialSovereignDominion100DetailsEngine.ClampWeaponStatBonus(3, 2));
    }

    [Fact]
    public void TestDetail1102_AttackCauseCurseOnTeammate()
    {
        Assert.False(OfficialSovereignDominion100DetailsEngine.ShouldAttackCauseCurseOnTeammate(true));
        Assert.True(OfficialSovereignDominion100DetailsEngine.ShouldAttackCauseCurseOnTeammate(false));
    }

    [Fact]
    public void TestDetail1103_TrialSlashTargetValid()
    {
        Assert.True(OfficialSovereignDominion100DetailsEngine.IsTrialSlashTargetValid("大刀卫士"));
        Assert.False(OfficialSovereignDominion100DetailsEngine.IsTrialSlashTargetValid(""));
    }

    [Fact]
    public void TestDetail1104_BlessingOilWithCurseResolution()
    {
        var (l1, c1) = OfficialSovereignDominion100DetailsEngine.ApplyBlessingOilWithCurseResolution(0, 2);
        Assert.Equal(0, l1);
        Assert.Equal(1, c1); // 先消除 1 点诅咒

        var (l2, c2) = OfficialSovereignDominion100DetailsEngine.ApplyBlessingOilWithCurseResolution(0, 0);
        Assert.Equal(1, l2);
        Assert.Equal(0, c2); // 无诅咒加 1 点幸运
    }

    [Fact]
    public void TestDetail1105_LuoChaDamage()
    {
        Assert.Equal(15, OfficialSovereignDominion100DetailsEngine.GetLuoChaDamage(15, 0, 10));
    }

    [Fact]
    public void TestDetail1106_DestinyBladeStats()
    {
        var (can, ac) = OfficialSovereignDominion100DetailsEngine.GetDestinyBladeStats(35);
        Assert.True(can);
        Assert.Equal(3, ac);
    }

    [Fact]
    public void TestDetail1107_DragonSlayerEvaluation()
    {
        var (can, mult) = OfficialSovereignDominion100DetailsEngine.EvaluateDragonSlayer(50);
        Assert.True(can);
        Assert.Equal(1.10f, mult);
    }

    [Fact]
    public void TestDetail1108_SoulStaffTameZumaGuard()
    {
        Assert.True(OfficialSovereignDominion100DetailsEngine.CanSoulStaffTameZumaGuard(true, 35));
        Assert.False(OfficialSovereignDominion100DetailsEngine.CanSoulStaffTameZumaGuard(false, 35));
    }

    [Fact]
    public void TestDetail1109_DragonSwordShinsuHpBonus()
    {
        Assert.Equal(120, OfficialSovereignDominion100DetailsEngine.ApplyDragonSwordShinsuHpBonus(100, true));
        Assert.Equal(100, OfficialSovereignDominion100DetailsEngine.ApplyDragonSwordShinsuHpBonus(100, false));
    }

    [Fact]
    public void TestDetail1110_BoneStaffShieldDuration()
    {
        Assert.Equal(39, OfficialSovereignDominion100DetailsEngine.ApplyBoneStaffShieldDuration(30, true));
    }

    // =========================================================================
    // 细节 1111 ~ 1125: 未知暗殿、封魔矿区与祖玛机制测试
    // =========================================================================

    [Fact]
    public void TestDetail1111_UnknownTempleBossPoison()
    {
        var (g, r) = OfficialSovereignDominion100DetailsEngine.GetUnknownTempleBossPoisonProfile(true);
        Assert.True(g);
        Assert.True(r);
    }

    [Fact]
    public void TestDetail1112_WomaHornDropRate()
    {
        Assert.Equal(5, OfficialSovereignDominion100DetailsEngine.GetWomaHornDropRateDenominator(true));
        Assert.Equal(10, OfficialSovereignDominion100DetailsEngine.GetWomaHornDropRateDenominator(false));
    }

    [Fact]
    public void TestDetail1113_UnknownScarecrowStats()
    {
        var (speed, min, max) = OfficialSovereignDominion100DetailsEngine.GetUnknownScarecrowStats();
        Assert.Equal(300, speed);
        Assert.Equal(40, min);
        Assert.Equal(70, max);
    }

    [Fact]
    public void TestDetail1114_UnknownDeerStats()
    {
        var (speed, armor) = OfficialSovereignDominion100DetailsEngine.GetUnknownDeerStats();
        Assert.Equal(400, speed);
        Assert.Equal(80, armor);
    }

    [Fact]
    public void TestDetail1115_RandomScrollForbiddenInFengMoMine()
    {
        Assert.True(OfficialSovereignDominion100DetailsEngine.IsRandomScrollForbiddenInFengMoMine("D2001"));
        Assert.False(OfficialSovereignDominion100DetailsEngine.IsRandomScrollForbiddenInFengMoMine("0"));
    }

    [Fact]
    public void TestDetail1116_FengMoArcherDamageOnEvil()
    {
        Assert.Equal(200, OfficialSovereignDominion100DetailsEngine.GetFengMoArcherDamageOnEvil());
    }

    [Fact]
    public void TestDetail1117_CanEvilPlayerTradeInOverlordHall()
    {
        Assert.True(OfficialSovereignDominion100DetailsEngine.CanEvilPlayerTradeInOverlordHall("封魔谷霸者大厅"));
        Assert.False(OfficialSovereignDominion100DetailsEngine.CanEvilPlayerTradeInOverlordHall("比奇城"));
    }

    [Fact]
    public void TestDetail1118_ThunderRoadCorrectPassage()
    {
        Assert.True(OfficialSovereignDominion100DetailsEngine.IsThunderRoadCorrectPassage(2));
        Assert.False(OfficialSovereignDominion100DetailsEngine.IsThunderRoadCorrectPassage(1));
    }

    [Fact]
    public void TestDetail1119_RainbowDemonDropRadius()
    {
        Assert.Equal(5, OfficialSovereignDominion100DetailsEngine.GetRainbowDemonDropRadius());
    }

    [Fact]
    public void TestDetail1120_ValidateZumaMazeStep()
    {
        Assert.True(OfficialSovereignDominion100DetailsEngine.ValidateZumaMazeStep(0, "Left"));
        Assert.True(OfficialSovereignDominion100DetailsEngine.ValidateZumaMazeStep(1, "Up"));
        Assert.False(OfficialSovereignDominion100DetailsEngine.ValidateZumaMazeStep(0, "Right"));
    }

    [Fact]
    public void TestDetail1121_SuperZumaGuardMovement()
    {
        var (speed, immune) = OfficialSovereignDominion100DetailsEngine.GetSuperZumaGuardMovement();
        Assert.Equal(250, speed);
        Assert.True(immune);
    }

    [Fact]
    public void TestDetail1122_ZumaStatueArmorPenetration()
    {
        Assert.Equal(80, OfficialSovereignDominion100DetailsEngine.CalculateZumaStatueArmorPenetration(100));
    }

    [Fact]
    public void TestDetail1123_ZumaLeaderSummonCount()
    {
        Assert.Equal(4, OfficialSovereignDominion100DetailsEngine.GetZumaLeaderSummonCount());
    }

    [Fact]
    public void TestDetail1124_WomaLeaderTeleportBlink()
    {
        Assert.True(OfficialSovereignDominion100DetailsEngine.ShouldWomaLeaderTeleportBlink(100, 1000, 60));
        Assert.False(OfficialSovereignDominion100DetailsEngine.ShouldWomaLeaderTeleportBlink(500, 1000, 60));
    }

    [Fact]
    public void TestDetail1125_DragonLordAttributes()
    {
        var (p, m, b) = OfficialSovereignDominion100DetailsEngine.GetDragonLordAttributes();
        Assert.True(p);
        Assert.True(m);
        Assert.True(b);
    }

    // =========================================================================
    // 细节 1126 ~ 1140: 战斗动作前后摇与音效时序测试
    // =========================================================================

    [Fact]
    public void TestDetail1126_WarriorSlashTimings()
    {
        var (wind, rec) = OfficialSovereignDominion100DetailsEngine.GetWarriorSlashTimings();
        Assert.Equal(150, wind);
        Assert.Equal(250, rec);
    }

    [Fact]
    public void TestDetail1127_FireSwordTimings()
    {
        var (chg, snd) = OfficialSovereignDominion100DetailsEngine.GetFireSwordTimings();
        Assert.Equal(200, chg);
        Assert.Equal(104, snd);
    }

    [Fact]
    public void TestDetail1128_HalfMoonSoundId()
    {
        Assert.Equal(103, OfficialSovereignDominion100DetailsEngine.GetHalfMoonSoundId());
    }

    [Fact]
    public void TestDetail1129_MotaeboStepDuration()
    {
        Assert.Equal(150, OfficialSovereignDominion100DetailsEngine.GetMotaeboStepDurationMs());
    }

    [Fact]
    public void TestDetail1130_ThunderSpellProfile()
    {
        var (stun, snd) = OfficialSovereignDominion100DetailsEngine.GetThunderSpellProfile();
        Assert.Equal(300, stun);
        Assert.Equal(110, snd);
    }

    [Fact]
    public void TestDetail1131_FireWallDuration()
    {
        int dur = OfficialSovereignDominion100DetailsEngine.CalculateFireWallDurationSec(20, 3);
        Assert.Equal(55, dur); // 10 + 15 + 30 = 55
    }

    [Fact]
    public void TestDetail1132_MagicShieldShatterProfile()
    {
        var (stun, snd) = OfficialSovereignDominion100DetailsEngine.GetMagicShieldShatterProfile();
        Assert.Equal(500, stun);
        Assert.Equal(115, snd);
    }

    [Fact]
    public void TestDetail1133_IceRoarProfile()
    {
        var (bypass, snd) = OfficialSovereignDominion100DetailsEngine.GetIceRoarProfile();
        Assert.True(bypass);
        Assert.Equal(118, snd);
    }

    [Fact]
    public void TestDetail1134_HellThunderProfile()
    {
        var (para, mp) = OfficialSovereignDominion100DetailsEngine.GetHellThunderProfile();
        Assert.Equal(1500, para);
        Assert.Equal(40, mp);
    }

    [Fact]
    public void TestDetail1135_FireCharmFlightMs()
    {
        Assert.Equal(180, OfficialSovereignDominion100DetailsEngine.CalculateFireCharmFlightMs(3));
    }

    [Fact]
    public void TestDetail1136_TaoistDualPoisonDurations()
    {
        var (g, r) = OfficialSovereignDominion100DetailsEngine.GetTaoistDualPoisonDurations();
        Assert.Equal(30, g);
        Assert.Equal(20, r);
    }

    [Fact]
    public void TestDetail1137_SacredArmorBonus()
    {
        var (ac, dur) = OfficialSovereignDominion100DetailsEngine.GetSacredArmorBonus(3);
        Assert.Equal(9, ac);
        Assert.Equal(120, dur);
    }

    [Fact]
    public void TestDetail1138_GhostShieldBonus()
    {
        var (mac, dur) = OfficialSovereignDominion100DetailsEngine.GetGhostShieldBonus(3);
        Assert.Equal(9, mac);
        Assert.Equal(120, dur);
    }

    [Fact]
    public void TestDetail1139_RevelationDuration()
    {
        Assert.Equal(30, OfficialSovereignDominion100DetailsEngine.GetRevelationDurationSec());
    }

    [Fact]
    public void TestDetail1140_RunSpeedProfile()
    {
        var (start, step) = OfficialSovereignDominion100DetailsEngine.GetRunSpeedProfile();
        Assert.Equal(100, start);
        Assert.Equal(200, step);
    }

    // =========================================================================
    // 细节 1141 ~ 1150: 经济系统与极端重连测试
    // =========================================================================

    [Fact]
    public void TestDetail1141_MaxCarriedGold()
    {
        Assert.Equal(10000000, OfficialSovereignDominion100DetailsEngine.GetMaxCarriedGold());
    }

    [Fact]
    public void TestDetail1142_MaxStorageGold()
    {
        Assert.Equal(50000000, OfficialSovereignDominion100DetailsEngine.GetMaxStorageGold());
    }

    [Fact]
    public void TestDetail1143_MaxDropGoldOnce()
    {
        Assert.Equal(100000, OfficialSovereignDominion100DetailsEngine.GetMaxDropGoldOnce());
    }

    [Fact]
    public void TestDetail1144_IsBagFull()
    {
        Assert.True(OfficialSovereignDominion100DetailsEngine.IsBagFull(40));
        Assert.False(OfficialSovereignDominion100DetailsEngine.IsBagFull(39));
    }

    [Fact]
    public void TestDetail1145_ValidateGuildCreation()
    {
        var (succ, _) = OfficialSovereignDominion100DetailsEngine.ValidateGuildCreation(true, 1000000, 22);
        Assert.True(succ);

        var (failHorn, _) = OfficialSovereignDominion100DetailsEngine.ValidateGuildCreation(false, 1000000, 22);
        Assert.False(failHorn);

        var (failGold, _) = OfficialSovereignDominion100DetailsEngine.ValidateGuildCreation(true, 500000, 22);
        Assert.False(failGold);
    }

    [Fact]
    public void TestDetail1146_GuildWarEnemyNameColor()
    {
        Assert.Equal("DeepBlue", OfficialSovereignDominion100DetailsEngine.GetGuildWarEnemyNameColor());
    }

    [Fact]
    public void TestDetail1147_GuildWarKillExemptFromPkPenalty()
    {
        Assert.True(OfficialSovereignDominion100DetailsEngine.IsGuildWarKillExemptFromPkPenalty(true));
        Assert.False(OfficialSovereignDominion100DetailsEngine.IsGuildWarKillExemptFromPkPenalty(false));
    }

    [Fact]
    public void TestDetail1148_SabukSiegeExemptFromPk()
    {
        Assert.True(OfficialSovereignDominion100DetailsEngine.IsSabukSiegeExemptFromPk(true, "0150"));
        Assert.False(OfficialSovereignDominion100DetailsEngine.IsSabukSiegeExemptFromPk(false, "0150"));
    }

    [Fact]
    public void TestDetail1149_ShouldTradeAbortDueToDistance()
    {
        Assert.True(OfficialSovereignDominion100DetailsEngine.ShouldTradeAbortDueToDistance(10, 10, 15, 10));
        Assert.False(OfficialSovereignDominion100DetailsEngine.ShouldTradeAbortDueToDistance(10, 10, 12, 11));
    }

    [Fact]
    public void TestDetail1150_ValidateDirectBlitSequence()
    {
        var (succ, rec1) = OfficialSovereignDominion100DetailsEngine.ValidateDirectBlitSequence(100, 105);
        Assert.True(succ);
        Assert.False(rec1);

        var (fail, rec2) = OfficialSovereignDominion100DetailsEngine.ValidateDirectBlitSequence(100, 115);
        Assert.False(fail);
        Assert.True(rec2);
    }
}
