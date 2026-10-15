using System;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 盛大官方 1.5 统御纪元 100 细节与权威服务器规则自动化测试矩阵 (Details 951 ~ 1050)
/// 严格 1:1 验证白日门/虎卫堂/赤月恶魔/套装全服共鸣/武器升级与Dumb Client边界
/// </summary>
public class TestOfficialWave88_100DetailsImperialMatrix
{
    // =========================================================================
    // 细节 951 ~ 965: 白日门与丛林迷宫剧毒生态系统测试
    // =========================================================================

    [Fact]
    public void TestDetail951_ToxicSpiderSpitRangeAndPoisonDuration()
    {
        var (canPoison, duration, interval, dmg) = OfficialImperial100DetailsEngine.CalculateToxicSpiderSpit(10, 10, 14, 13);
        Assert.True(canPoison);
        Assert.Equal(30, duration);
        Assert.Equal(2000, interval);
        Assert.Equal(2, dmg);

        var (outOfRange, _, _, _) = OfficialImperial100DetailsEngine.CalculateToxicSpiderSpit(10, 10, 20, 20);
        Assert.False(outOfRange);
    }

    [Fact]
    public void TestDetail952_BlackSpiderTearDefenseAndBleed()
    {
        var (def, bleed) = OfficialImperial100DetailsEngine.CalculateBlackSpiderTear(100, 10);
        Assert.Equal(70, def); // 忽视 30% 物防
        Assert.True(bleed);

        var (_, noBleed) = OfficialImperial100DetailsEngine.CalculateBlackSpiderTear(100, 50);
        Assert.False(noBleed);
    }

    [Fact]
    public void TestDetail953_PhantomSpiderSpawnCapAndInterval()
    {
        Assert.True(OfficialImperial100DetailsEngine.CanPhantomSpiderSpawn(5, 3.5f));
        Assert.False(OfficialImperial100DetailsEngine.CanPhantomSpiderSpawn(6, 4.0f)); // 上限 6
        Assert.False(OfficialImperial100DetailsEngine.CanPhantomSpiderSpawn(2, 2.0f)); // 间隔不足 3 秒
    }

    [Fact]
    public void TestDetail954_ExplosiveSpiderDetonationDistanceAndDamage()
    {
        var (boom, dmg) = OfficialImperial100DetailsEngine.CalculateExplosiveSpiderDetonation(10, 10, 11, 11, 5);
        Assert.True(boom);
        Assert.InRange(dmg, 25, 45);

        var (noBoom, _) = OfficialImperial100DetailsEngine.CalculateExplosiveSpiderDetonation(10, 10, 15, 15, 5);
        Assert.False(noBoom);
    }

    [Fact]
    public void TestDetail955_SteelSpiderDamageReduction()
    {
        int physDmg = OfficialImperial100DetailsEngine.CalculateSteelSpiderDamageTaken(100, false);
        Assert.Equal(60, physDmg); // 物理减免 40%

        int magicDmg = OfficialImperial100DetailsEngine.CalculateSteelSpiderDamageTaken(100, true);
        Assert.Equal(100, magicDmg); // 魔法无减免
    }

    [Fact]
    public void TestDetail956_MoonSpiderParalyzeChance()
    {
        var (para, dur) = OfficialImperial100DetailsEngine.CalculateMoonSpiderParalyze(20);
        Assert.True(para);
        Assert.Equal(5.0f, dur);

        var (noPara, _) = OfficialImperial100DetailsEngine.CalculateMoonSpiderParalyze(30);
        Assert.False(noPara);
    }

    [Fact]
    public void TestDetail957_JungleMazeEntryLevelConstraint()
    {
        var (allowed1, _) = OfficialImperial100DetailsEngine.ValidateJungleMazeEntry(27);
        Assert.False(allowed1);

        var (allowed2, _) = OfficialImperial100DetailsEngine.ValidateJungleMazeEntry(28);
        Assert.True(allowed2);
    }

    [Fact]
    public void TestDetail958_SecretPathSpawnDensityMultiplier()
    {
        Assert.Equal(1.8f, OfficialImperial100DetailsEngine.GetSecretPathSpawnDensityMultiplier());
    }

    [Fact]
    public void TestDetail959_FlowerSpiderCleavePierce()
    {
        Assert.True(OfficialImperial100DetailsEngine.IsFlowerSpiderCleavePierce(2));
        Assert.False(OfficialImperial100DetailsEngine.IsFlowerSpiderCleavePierce(1));
    }

    [Fact]
    public void TestDetail960_TeleportProhibitedInRedMoonNest()
    {
        Assert.True(OfficialImperial100DetailsEngine.IsTeleportProhibitedInRedMoonNest("D1001"));
        Assert.True(OfficialImperial100DetailsEngine.IsTeleportProhibitedInRedMoonNest("恶魔祭坛"));
        Assert.False(OfficialImperial100DetailsEngine.IsTeleportProhibitedInRedMoonNest("0"));
    }

    [Fact]
    public void TestDetail961_MinimapJammedInJungle()
    {
        Assert.True(OfficialImperial100DetailsEngine.IsMinimapJammedInJungle("丛林迷宫"));
        Assert.False(OfficialImperial100DetailsEngine.IsMinimapJammedInJungle("比奇城"));
    }

    [Fact]
    public void TestDetail962_TianZunSpiderPoisonMitigation()
    {
        Assert.Equal(10, OfficialImperial100DetailsEngine.ApplyTianZunSpiderPoisonMitigation(20, true));
        Assert.Equal(20, OfficialImperial100DetailsEngine.ApplyTianZunSpiderPoisonMitigation(20, false));
    }

    [Fact]
    public void TestDetail963_HarvestSpiderCorpseDropBag()
    {
        var (succ, item) = OfficialImperial100DetailsEngine.HarvestSpiderCorpse(2);
        Assert.True(succ);
        Assert.Equal("蜘蛛毒囊", item);

        var (fail, _) = OfficialImperial100DetailsEngine.HarvestSpiderCorpse(10);
        Assert.False(fail);
    }

    [Fact]
    public void TestDetail964_ArcherGuardShotEvilTarget()
    {
        var (canShoot, dmg) = OfficialImperial100DetailsEngine.CalculateArcherGuardShot(10, 10, 15, 10, true);
        Assert.True(canShoot);
        Assert.Equal(100, dmg);

        var (peace, _) = OfficialImperial100DetailsEngine.CalculateArcherGuardShot(10, 10, 15, 10, false);
        Assert.False(peace);
    }

    [Fact]
    public void TestDetail965_JungleVineObstacle()
    {
        Assert.True(OfficialImperial100DetailsEngine.IsJungleVineObstacle(0x04));
        Assert.False(OfficialImperial100DetailsEngine.IsJungleVineObstacle(0x00));
    }

    // =========================================================================
    // 细节 966 ~ 980: 虎卫堂招募与极品仆从战斗生态测试
    // =========================================================================

    [Fact]
    public void TestDetail966_HuWeiRespawnDue()
    {
        Assert.True(OfficialImperial100DetailsEngine.IsHuWeiRespawnDue(14400f));
        Assert.False(OfficialImperial100DetailsEngine.IsHuWeiRespawnDue(10000f));
    }

    [Fact]
    public void TestDetail967_HuWeiTameRateCalculation()
    {
        var (canAttempt, rate) = OfficialImperial100DetailsEngine.CalculateHuWeiTameRate(1, 25, 3);
        Assert.True(canAttempt);
        Assert.InRange(rate, 5, 45);

        var (warriorAttempt, _) = OfficialImperial100DetailsEngine.CalculateHuWeiTameRate(0, 30, 0);
        Assert.False(warriorAttempt);
    }

    [Fact]
    public void TestDetail968_HuWeiBaseAttributes()
    {
        var (hp, minAc, maxAc, minDc, maxDc, speed) = OfficialImperial100DetailsEngine.GetHuWeiBaseAttributes();
        Assert.Equal(400, hp);
        Assert.Equal(10, minAc);
        Assert.Equal(15, maxAc);
        Assert.Equal(1000, speed);
    }

    [Fact]
    public void TestDetail969_HuWeiLevelUpAttributes()
    {
        var (hp0, min0, max0) = OfficialImperial100DetailsEngine.CalculateHuWeiLevelAttributes(0);
        Assert.Equal(400, hp0);
        Assert.Equal(15, min0);
        Assert.Equal(25, max0);

        var (hp7, min7, max7) = OfficialImperial100DetailsEngine.CalculateHuWeiLevelAttributes(7);
        Assert.Equal(960, hp7);
        Assert.Equal(36, min7);
        Assert.Equal(67, max7);
    }

    [Fact]
    public void TestDetail970_HuWeiRoarStunChance()
    {
        var (roar, stun) = OfficialImperial100DetailsEngine.CalculateHuWeiRoar(7, 5);
        Assert.True(roar);
        Assert.Equal(200, stun);

        var (noRoar, _) = OfficialImperial100DetailsEngine.CalculateHuWeiRoar(5, 5);
        Assert.False(noRoar);
    }

    [Fact]
    public void TestDetail971_YingWeiBaseAttributes()
    {
        var (hp, range) = OfficialImperial100DetailsEngine.GetYingWeiBaseAttributes();
        Assert.Equal(300, hp);
        Assert.Equal(8, range);
    }

    [Fact]
    public void TestDetail972_YingWeiMoveInterval()
    {
        Assert.Equal(600, OfficialImperial100DetailsEngine.GetYingWeiMoveIntervalMs(0));
        Assert.Equal(355, OfficialImperial100DetailsEngine.GetYingWeiMoveIntervalMs(7));
    }

    [Fact]
    public void TestDetail973_PetYellowPoisonDefense()
    {
        Assert.Equal(70, OfficialImperial100DetailsEngine.CalculatePetYellowPoisonDefense(100));
    }

    [Fact]
    public void TestDetail974_PetTeleportWithMaster()
    {
        Assert.True(OfficialImperial100DetailsEngine.CanPetTeleportWithMaster(10, 10, 13, 12));
        Assert.False(OfficialImperial100DetailsEngine.CanPetTeleportWithMaster(10, 10, 20, 20));
    }

    [Fact]
    public void TestDetail975_PetRebelDueToOffline()
    {
        Assert.True(OfficialImperial100DetailsEngine.IsPetRebelDueToOffline(60f));
        Assert.False(OfficialImperial100DetailsEngine.IsPetRebelDueToOffline(50f));
    }

    [Fact]
    public void TestDetail976_LureUndeadInstantKill()
    {
        var (kill, _) = OfficialImperial100DetailsEngine.CheckLureUndeadInstantKill(true, 15);
        Assert.True(kill);

        var (noKill, _) = OfficialImperial100DetailsEngine.CheckLureUndeadInstantKill(false, 15);
        Assert.False(noKill);
    }

    [Fact]
    public void TestDetail977_PetTargetPriorityScoring()
    {
        int scoreMasterTarget = OfficialImperial100DetailsEngine.ScorePetTargetPriority(false, true, 2);
        int scoreOther = OfficialImperial100DetailsEngine.ScorePetTargetPriority(false, false, 2);
        Assert.True(scoreMasterTarget > scoreOther);
    }

    [Fact]
    public void TestDetail978_PetCommandCycle()
    {
        int c1 = OfficialImperial100DetailsEngine.CyclePetCommand(0);
        Assert.Equal(1, c1);
        int c2 = OfficialImperial100DetailsEngine.CyclePetCommand(2);
        Assert.Equal(0, c2);
    }

    [Fact]
    public void TestDetail979_LureFlashVisualRepresentation()
    {
        Assert.Equal("WhiteTamed", OfficialImperial100DetailsEngine.GetLureFlashVisual(true));
        Assert.Equal("YellowFlash", OfficialImperial100DetailsEngine.GetLureFlashVisual(false));
    }

    [Fact]
    public void TestDetail980_PetCapLimit()
    {
        Assert.True(OfficialImperial100DetailsEngine.CanTameMorePet(4));
        Assert.False(OfficialImperial100DetailsEngine.CanTameMorePet(5));
    }

    // =========================================================================
    // 细节 981 ~ 995: 恶魔祭坛与赤月恶魔测试
    // =========================================================================

    [Fact]
    public void TestDetail981_RedMoonEvilSpikeDamage()
    {
        int dmg = OfficialImperial100DetailsEngine.CalculateRedMoonEvilSpikeDamage(12345);
        Assert.InRange(dmg, 30, 70);
    }

    [Fact]
    public void TestDetail982_GhostShieldMitigatesSpike()
    {
        int normal = OfficialImperial100DetailsEngine.ApplyGhostShieldAgainstSpike(100, false);
        int shielded = OfficialImperial100DetailsEngine.ApplyGhostShieldAgainstSpike(100, true);
        Assert.Equal(100, normal);
        Assert.Equal(80, shielded);
    }

    [Fact]
    public void TestDetail983_RedMoonRegen()
    {
        int regenerated = OfficialImperial100DetailsEngine.CalculateRedMoonEvilRegen(4000, 5000, 5.0f);
        Assert.Equal(4150, regenerated);

        int notYet = OfficialImperial100DetailsEngine.CalculateRedMoonEvilRegen(4000, 5000, 3.0f);
        Assert.Equal(4000, notYet);
    }

    [Fact]
    public void TestDetail984_CorpseHarvestOnlyMonster()
    {
        Assert.True(OfficialImperial100DetailsEngine.IsCorpseHarvestOnlyMonster("赤月双头金刚"));
        Assert.False(OfficialImperial100DetailsEngine.IsCorpseHarvestOnlyMonster("沃玛教主"));
    }

    [Fact]
    public void TestDetail985_DoubleHeadKongHarvest()
    {
        var (succ, item) = OfficialImperial100DetailsEngine.HarvestDoubleHeadKong(5, 3);
        Assert.True(succ);
        Assert.Equal("圣战头盔", item);

        var (fail, _) = OfficialImperial100DetailsEngine.HarvestDoubleHeadKong(20, 3);
        Assert.False(fail); // 达到上限 20 次
    }

    [Fact]
    public void TestDetail986_DoubleHeadFiendDrop()
    {
        Assert.True(OfficialImperial100DetailsEngine.CanDoubleHeadFiendDrop(5));
        Assert.False(OfficialImperial100DetailsEngine.CanDoubleHeadFiendDrop(20));
    }

    [Fact]
    public void TestDetail987_RedMoonDropRadius()
    {
        Assert.Equal(5, OfficialImperial100DetailsEngine.GetRedMoonDropRadius());
    }

    [Fact]
    public void TestDetail988_AltarSafeSpot()
    {
        Assert.True(OfficialImperial100DetailsEngine.IsAltarSafeSpot(180, 54));
        Assert.False(OfficialImperial100DetailsEngine.IsAltarSafeSpot(100, 100));
    }

    [Fact]
    public void TestDetail989_ScrollForbiddenInNest()
    {
        Assert.True(OfficialImperial100DetailsEngine.IsScrollForbiddenInNest("随机传送卷轴"));
        Assert.True(OfficialImperial100DetailsEngine.IsScrollForbiddenInNest("行会回城卷"));
        Assert.False(OfficialImperial100DetailsEngine.IsScrollForbiddenInNest("地牢逃脱卷"));
    }

    [Fact]
    public void TestDetail990_RedMoonDeathDropRate()
    {
        Assert.Equal(10, OfficialImperial100DetailsEngine.GetRedMoonDeathDropRateDenominator());
    }

    [Fact]
    public void TestDetail991_GluttonousSpiderLifeSteal()
    {
        Assert.Equal(30, OfficialImperial100DetailsEngine.CalculateGluttonousSpiderLifeSteal(100));
    }

    [Fact]
    public void TestDetail992_RedMoonVulnerability()
    {
        int thunder = OfficialImperial100DetailsEngine.ApplyRedMoonVulnerability(100, "雷电术");
        Assert.Equal(85, thunder); // 15% 减免

        int fire = OfficialImperial100DetailsEngine.ApplyRedMoonVulnerability(100, "烈火剑法");
        Assert.Equal(120, fire); // 20% 易伤
    }

    [Fact]
    public void TestDetail993_RedMoonEarthquake()
    {
        var (shake, dur) = OfficialImperial100DetailsEngine.CheckRedMoonEarthquake(600f);
        Assert.True(shake);
        Assert.Equal(500, dur);

        var (noShake, _) = OfficialImperial100DetailsEngine.CheckRedMoonEarthquake(550f);
        Assert.False(noShake);
    }

    [Fact]
    public void TestDetail994_ChoicePlacePortal()
    {
        Assert.True(OfficialImperial100DetailsEngine.ValidateChoicePlacePortal(10, 10, 2));
        Assert.False(OfficialImperial100DetailsEngine.ValidateChoicePlacePortal(10, 10, 5));
    }

    [Fact]
    public void TestDetail995_SaintCombatSuitBonus()
    {
        var (armorIgnore, bonusDmg) = OfficialImperial100DetailsEngine.GetSaintCombatSuitBonus(true);
        Assert.Equal(10, armorIgnore);
        Assert.Equal(15, bonusDmg);

        var (none1, none2) = OfficialImperial100DetailsEngine.GetSaintCombatSuitBonus(false);
        Assert.Equal(0, none1);
        Assert.Equal(0, none2);
    }

    // =========================================================================
    // 细节 996 ~ 1010: 套装神威与全服共鸣测试
    // =========================================================================

    [Fact]
    public void TestDetail996_PrayerSetFullCheck()
    {
        Assert.True(OfficialImperial100DetailsEngine.IsFullPrayerSet(true, true, true, true, true));
        Assert.False(OfficialImperial100DetailsEngine.IsFullPrayerSet(true, true, true, true, false));
    }

    [Fact]
    public void TestDetail997_PrayerCataclysmTrigger()
    {
        Assert.True(OfficialImperial100DetailsEngine.ShouldTriggerPrayerCataclysm(true, true));
        Assert.False(OfficialImperial100DetailsEngine.ShouldTriggerPrayerCataclysm(true, false));
    }

    [Fact]
    public void TestDetail998_PrayerCataclysmDuration()
    {
        Assert.Equal(1800, OfficialImperial100DetailsEngine.GetPrayerCataclysmDurationSeconds());
    }

    [Fact]
    public void TestDetail999_PrayerNecklaceShatter()
    {
        Assert.True(OfficialImperial100DetailsEngine.DoesPrayerNecklaceShatterOnDeath());
    }

    [Fact]
    public void TestDetail1000_RecallSetFullCheck()
    {
        Assert.True(OfficialImperial100DetailsEngine.IsFullRecallSet(true, true, true, true));
        Assert.False(OfficialImperial100DetailsEngine.IsFullRecallSet(true, false, true, true));
    }

    [Fact]
    public void TestDetail1001_RecallGroupCommandValidation()
    {
        var (succ, _) = OfficialImperial100DetailsEngine.ValidateRecallGroupCommand(true, true, 800, 1000);
        Assert.True(succ);

        var (failLeader, _) = OfficialImperial100DetailsEngine.ValidateRecallGroupCommand(false, true, 800, 1000);
        Assert.False(failLeader);

        var (failMp, _) = OfficialImperial100DetailsEngine.ValidateRecallGroupCommand(true, true, 500, 1000);
        Assert.False(failMp);
    }

    [Fact]
    public void TestDetail1002_RecallCooldown()
    {
        Assert.Equal(180, OfficialImperial100DetailsEngine.GetRecallGroupCooldownSeconds());
    }

    [Fact]
    public void TestDetail1003_RecallMemberPosCalculation()
    {
        var (x0, y0) = OfficialImperial100DetailsEngine.CalculateRecallMemberPos(100, 100, 0);
        Assert.Equal(100, x0);
        Assert.Equal(100, y0);

        var (x1, y1) = OfficialImperial100DetailsEngine.CalculateRecallMemberPos(100, 100, 1);
        Assert.Equal(101, x1);
        Assert.Equal(100, y1);
    }

    [Fact]
    public void TestDetail1004_CanMemberBeRecalled()
    {
        Assert.True(OfficialImperial100DetailsEngine.CanMemberBeRecalled(true, false));
        Assert.False(OfficialImperial100DetailsEngine.CanMemberBeRecalled(false, false));
        Assert.False(OfficialImperial100DetailsEngine.CanMemberBeRecalled(true, true));
    }

    [Fact]
    public void TestDetail1005_RainbowMagicLifeSteal()
    {
        Assert.Equal(15, OfficialImperial100DetailsEngine.CalculateRainbowMagicLifeSteal(100, true));
        Assert.Equal(0, OfficialImperial100DetailsEngine.CalculateRainbowMagicLifeSteal(100, false));
    }

    [Fact]
    public void TestDetail1006_DemonBloodSuitModifiers()
    {
        var (hp, mp) = OfficialImperial100DetailsEngine.GetDemonBloodSuitModifiers(true);
        Assert.Equal(175, hp);
        Assert.Equal(150, mp);
    }

    [Fact]
    public void TestDetail1007_GhostSuitSpeedReduction()
    {
        Assert.Equal(80, OfficialImperial100DetailsEngine.GetGhostSuitSpeedReductionMs(true));
        Assert.Equal(0, OfficialImperial100DetailsEngine.GetGhostSuitSpeedReductionMs(false));
    }

    [Fact]
    public void TestDetail1008_YanLuoGloveEffect()
    {
        var (dmg, refl) = OfficialImperial100DetailsEngine.CalculateYanLuoGloveEffect(100, true);
        Assert.Equal(90, dmg);
        Assert.Equal(10, refl);
    }

    [Fact]
    public void TestDetail1009_GaleSuitModifiers()
    {
        var (speed, mult) = OfficialImperial100DetailsEngine.CalculateGaleSuitModifiers(3);
        Assert.Equal(3, speed);
        Assert.Equal(0.85f, mult);
    }

    [Fact]
    public void TestDetail1010_GoldBraceletMacRange()
    {
        var (min, max) = OfficialImperial100DetailsEngine.GetGoldBraceletMacRange(4);
        Assert.Equal(2, min);
        Assert.Equal(7, max);
    }

    // =========================================================================
    // 细节 1011 ~ 1025: 武器升级与开刀测试
    // =========================================================================

    [Fact]
    public void TestDetail1011_WeaponUpgradeDuration()
    {
        Assert.Equal(3600, OfficialImperial100DetailsEngine.GetWeaponUpgradeDurationSeconds());
    }

    [Fact]
    public void TestDetail1012_BlackIronPreserveDurability()
    {
        Assert.True(OfficialImperial100DetailsEngine.DoesBlackIronPreserveDurability(60));
        Assert.False(OfficialImperial100DetailsEngine.DoesBlackIronPreserveDurability(59));
    }

    [Fact]
    public void TestDetail1013_PurityDurabilityLoss()
    {
        var (loss, amt) = OfficialImperial100DetailsEngine.CalculatePurityDurabilityLoss(25, 50, 10);
        Assert.True(loss);
        Assert.InRange(amt, 1, 3);

        var (noLoss, _) = OfficialImperial100DetailsEngine.CalculatePurityDurabilityLoss(65, 50, 10);
        Assert.False(noLoss);
    }

    [Fact]
    public void TestDetail1014_DetermineUpgradeStatCategory()
    {
        Assert.Equal("DC", OfficialImperial100DetailsEngine.DetermineUpgradeStatCategory(10, 5, 2));
        Assert.Equal("MC", OfficialImperial100DetailsEngine.DetermineUpgradeStatCategory(5, 12, 2));
        Assert.Equal("SC", OfficialImperial100DetailsEngine.DetermineUpgradeStatCategory(2, 5, 15));
    }

    [Fact]
    public void TestDetail1015_WeaponUpgradeRate()
    {
        int normalRate = OfficialImperial100DetailsEngine.CalculateWeaponUpgradeRate(0, 0);
        Assert.Equal(50, normalRate);

        int redNameRate = OfficialImperial100DetailsEngine.CalculateWeaponUpgradeRate(250, 0);
        Assert.Equal(20, redNameRate);
    }

    [Fact]
    public void TestDetail1016_CheckWeaponOpenAttackResult()
    {
        Assert.True(OfficialImperial100DetailsEngine.CheckWeaponOpenAttackResult(30, 50));
        Assert.False(OfficialImperial100DetailsEngine.CheckWeaponOpenAttackResult(60, 50));
    }

    [Fact]
    public void TestDetail1017_CanUpgradeWeaponAgain()
    {
        Assert.True(OfficialImperial100DetailsEngine.CanUpgradeWeaponAgain(6));
        Assert.False(OfficialImperial100DetailsEngine.CanUpgradeWeaponAgain(7));
    }

    [Fact]
    public void TestDetail1018_DoesWeaponShatterOnFail()
    {
        Assert.True(OfficialImperial100DetailsEngine.DoesWeaponShatterOnFail());
    }

    [Fact]
    public void TestDetail1019_CalculateSeedOffset()
    {
        Assert.Equal(34, OfficialImperial100DetailsEngine.CalculateSeedOffset(2));
    }

    [Fact]
    public void TestDetail1020_CanBlessWeapon()
    {
        Assert.True(OfficialImperial100DetailsEngine.CanBlessWeapon(6));
        Assert.False(OfficialImperial100DetailsEngine.CanBlessWeapon(7));
    }

    [Fact]
    public void TestDetail1021_ApplyBlessingOil()
    {
        var (succLow, _) = OfficialImperial100DetailsEngine.ApplyBlessingOil(2, 50);
        Assert.True(succLow);

        var (succHigh, cursed) = OfficialImperial100DetailsEngine.ApplyBlessingOil(5, 40);
        Assert.False(succHigh);
        Assert.True(cursed);
    }

    [Fact]
    public void TestDetail1022_IsGuaranteedMaxDamage()
    {
        Assert.True(OfficialImperial100DetailsEngine.IsGuaranteedMaxDamage(9));
        Assert.False(OfficialImperial100DetailsEngine.IsGuaranteedMaxDamage(8));
    }

    [Fact]
    public void TestDetail1023_IsGuaranteedMinDamage()
    {
        Assert.True(OfficialImperial100DetailsEngine.IsGuaranteedMinDamage(10));
        Assert.False(OfficialImperial100DetailsEngine.IsGuaranteedMinDamage(9));
    }

    [Fact]
    public void TestDetail1024_CheckPkCurseChance()
    {
        Assert.True(OfficialImperial100DetailsEngine.CheckPkCurseChance(30));
        Assert.False(OfficialImperial100DetailsEngine.CheckPkCurseChance(50));
    }

    [Fact]
    public void TestDetail1025_WarGodOilPreservesMaxDurability()
    {
        Assert.True(OfficialImperial100DetailsEngine.WarGodOilPreservesMaxDurability());
    }

    // =========================================================================
    // 细节 1026 ~ 1040: 未知暗殿、封魔矿区与祖玛机制测试
    // =========================================================================

    [Fact]
    public void TestDetail1026_UnknownTempleWomaLeaderHp()
    {
        Assert.Equal(5000, OfficialImperial100DetailsEngine.GetUnknownTempleWomaLeaderHp(true));
        Assert.Equal(100, OfficialImperial100DetailsEngine.GetUnknownTempleWomaLeaderHp(false));
    }

    [Fact]
    public void TestDetail1027_UnknownTempleHolyDeer()
    {
        var (hp, min, max, immune) = OfficialImperial100DetailsEngine.GetUnknownTempleHolyDeer();
        Assert.Equal(1200, hp);
        Assert.Equal(50, min);
        Assert.Equal(80, max);
        Assert.True(immune);
    }

    [Fact]
    public void TestDetail1028_CanUnknownScarecrowDropBlessingOil()
    {
        Assert.True(OfficialImperial100DetailsEngine.CanUnknownScarecrowDropBlessingOil(3));
        Assert.False(OfficialImperial100DetailsEngine.CanUnknownScarecrowDropBlessingOil(10));
    }

    [Fact]
    public void TestDetail1029_UnknownTempleTimeExpired()
    {
        Assert.True(OfficialImperial100DetailsEngine.IsUnknownTempleTimeExpired(1800f));
        Assert.False(OfficialImperial100DetailsEngine.IsUnknownTempleTimeExpired(1700f));
    }

    [Fact]
    public void TestDetail1030_IsFengMoPalaceMap()
    {
        Assert.True(OfficialImperial100DetailsEngine.IsFengMoPalaceMap("D2008"));
        Assert.False(OfficialImperial100DetailsEngine.IsFengMoPalaceMap("D001"));
    }

    [Fact]
    public void TestDetail1031_IsOverlordHallSupplySafe()
    {
        Assert.True(OfficialImperial100DetailsEngine.IsOverlordHallSupplySafe("封魔霸者大厅"));
        Assert.False(OfficialImperial100DetailsEngine.IsOverlordHallSupplySafe("封魔殿"));
    }

    [Fact]
    public void TestDetail1032_ShouldRainbowDemonBlink()
    {
        Assert.True(OfficialImperial100DetailsEngine.ShouldRainbowDemonBlink(1000, 5000, 4, 10));
        Assert.False(OfficialImperial100DetailsEngine.ShouldRainbowDemonBlink(3000, 5000, 4, 10));
    }

    [Fact]
    public void TestDetail1033_ShouldZumaLeaderSummonWave()
    {
        Assert.True(OfficialImperial100DetailsEngine.ShouldZumaLeaderSummonWave(2000, 3000));
        Assert.False(OfficialImperial100DetailsEngine.ShouldZumaLeaderSummonWave(2500, 3000));
    }

    [Fact]
    public void TestDetail1034_IsSuperZumaGuard()
    {
        Assert.True(OfficialImperial100DetailsEngine.IsSuperZumaGuard(1400));
        Assert.False(OfficialImperial100DetailsEngine.IsSuperZumaGuard(330));
    }

    [Fact]
    public void TestDetail1035_IsZumaStatueAwakenInRange()
    {
        Assert.True(OfficialImperial100DetailsEngine.IsZumaStatueAwakenInRange(10, 10, 12, 11));
        Assert.False(OfficialImperial100DetailsEngine.IsZumaStatueAwakenInRange(10, 10, 15, 15));
    }

    [Fact]
    public void TestDetail1036_GetZumaArcherRange()
    {
        Assert.Equal(7, OfficialImperial100DetailsEngine.GetZumaArcherRange());
    }

    [Fact]
    public void TestDetail1037_CanFlyGloveSpawnBat()
    {
        Assert.True(OfficialImperial100DetailsEngine.CanFlyGloveSpawnBat(8, 3.0f));
        Assert.False(OfficialImperial100DetailsEngine.CanFlyGloveSpawnBat(10, 3.0f));
    }

    [Fact]
    public void TestDetail1038_IsWomaLeaderBerserk()
    {
        Assert.True(OfficialImperial100DetailsEngine.IsWomaLeaderBerserk(300, 2000));
        Assert.False(OfficialImperial100DetailsEngine.IsWomaLeaderBerserk(800, 2000));
    }

    [Fact]
    public void TestDetail1039_CheckWedgeMothAttack()
    {
        var (detect, stone) = OfficialImperial100DetailsEngine.CheckWedgeMothAttack(5, 10);
        Assert.True(detect);
        Assert.True(stone);
    }

    [Fact]
    public void TestDetail1040_CalculateDragonLordDamageTaken()
    {
        Assert.Equal(100, OfficialImperial100DetailsEngine.CalculateDragonLordDamageTaken(100, true));
        Assert.Equal(0, OfficialImperial100DetailsEngine.CalculateDragonLordDamageTaken(100, false));
    }

    // =========================================================================
    // 细节 1041 ~ 1050: 权威数值抗性与极端边界测试
    // =========================================================================

    [Fact]
    public void TestDetail1041_CheckMagicDodge()
    {
        Assert.True(OfficialImperial100DetailsEngine.CheckMagicDodge(50, 40));
        Assert.False(OfficialImperial100DetailsEngine.CheckMagicDodge(50, 60));
    }

    [Fact]
    public void TestDetail1042_CalculatePoisonDurationWithRecovery()
    {
        int dur = OfficialImperial100DetailsEngine.CalculatePoisonDurationWithRecovery(100, 50);
        Assert.Equal(50, dur);
    }

    [Fact]
    public void TestDetail1043_GetMediumHpPotionProfile()
    {
        var (tick, count, interval) = OfficialImperial100DetailsEngine.GetMediumHpPotionProfile();
        Assert.Equal(8, tick);
        Assert.Equal(10, count);
        Assert.Equal(1000, interval);
    }

    [Fact]
    public void TestDetail1044_GetSolarWaterProfile()
    {
        var (hp, mp, instant) = OfficialImperial100DetailsEngine.GetSolarWaterProfile();
        Assert.Equal(50, hp);
        Assert.Equal(50, mp);
        Assert.True(instant);
    }

    [Fact]
    public void TestDetail1045_GetSnowFrostProfile()
    {
        var (hpSuper, mpSuper) = OfficialImperial100DetailsEngine.GetSnowFrostProfile(true);
        Assert.Equal(100, hpSuper);
        Assert.Equal(100, mpSuper);

        var (hpNorm, mpNorm) = OfficialImperial100DetailsEngine.GetSnowFrostProfile(false);
        Assert.Equal(80, hpNorm);
        Assert.Equal(80, mpNorm);
    }

    [Fact]
    public void TestDetail1046_IsRunningForbiddenByWeight()
    {
        Assert.True(OfficialImperial100DetailsEngine.IsRunningForbiddenByWeight(95, 100));
        Assert.False(OfficialImperial100DetailsEngine.IsRunningForbiddenByWeight(85, 100));
    }

    [Fact]
    public void TestDetail1047_ValidateHandWeight()
    {
        var (canEquip, _) = OfficialImperial100DetailsEngine.ValidateHandWeight(30, 40);
        Assert.True(canEquip);

        var (tooHeavy, err) = OfficialImperial100DetailsEngine.ValidateHandWeight(50, 40);
        Assert.False(tooHeavy);
        Assert.Contains("无法拿动", err);
    }

    [Fact]
    public void TestDetail1048_ValidateWearWeight()
    {
        var (canEquip, _) = OfficialImperial100DetailsEngine.ValidateWearWeight(25, 30);
        Assert.True(canEquip);

        var (tooHeavy, err) = OfficialImperial100DetailsEngine.ValidateWearWeight(35, 30);
        Assert.False(tooHeavy);
        Assert.Contains("不足以穿戴", err);
    }

    [Fact]
    public void TestDetail1049_CheckSabukFlagState()
    {
        var (hp, cap) = OfficialImperial100DetailsEngine.CheckSabukFlagState(0);
        Assert.Equal(50000, hp);
        Assert.True(cap);

        var (_, alive) = OfficialImperial100DetailsEngine.CheckSabukFlagState(1000);
        Assert.False(alive);
    }

    [Fact]
    public void TestDetail1050_ValidateReconnectionStateSync()
    {
        Assert.True(OfficialImperial100DetailsEngine.ValidateReconnectionStateSync(100, 103));
        Assert.False(OfficialImperial100DetailsEngine.ValidateReconnectionStateSync(100, 110));
    }
}
