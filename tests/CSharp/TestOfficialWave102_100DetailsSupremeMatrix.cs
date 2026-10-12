using System;
using System.Text;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 盛大官方 1.50《虎卫传说》原版第 2351 ~ 2450 号微观细节规则权威测试矩阵 (Wave 102)
/// 100% 封闭分母实测：未知暗殿、白日门虎卫、尸王殿神技爆率、触龙神绝杀、沙城城门修缮与密道、
/// 战士刺杀双烈火、法师随从冰咆哮、道士神兽双形态、红名大盗大爆、Direct-Blit 协议。
/// </summary>
public class TestOfficialWave102_100DetailsSupremeMatrix
{
    // =========================================================================
    // 1. 未知暗殿神秘装备属性生成、诅咒判定与神水净化生态 (2351~2360)
    // =========================================================================

    [Fact]
    public void TestDetail2351_GenerateMysteryHelmetStats()
    {
        var rng = new Random(42);
        var (ac, mac, dc, mc, sc) = OfficialPure15Supreme102DetailsEngine.GenerateMysteryHelmetStats(rng);
        Assert.InRange(ac, 0, 5);
        Assert.InRange(mac, 0, 5);
        Assert.InRange(dc, 0, 3);
        Assert.InRange(mc, 0, 3);
        Assert.InRange(sc, 0, 3);
    }

    [Fact]
    public void TestDetail2352_GenerateMysteryAccessoryStats()
    {
        var rng = new Random(42);
        var (main, sub) = OfficialPure15Supreme102DetailsEngine.GenerateMysteryAccessoryStats(1, rng);
        Assert.InRange(main, 1, 4);
        Assert.InRange(sub, 0, 2);
    }

    [Fact]
    public void TestDetail2353_GenerateMysteryRequirement()
    {
        var (type0, val0) = OfficialPure15Supreme102DetailsEngine.GenerateMysteryRequirement(0);
        Assert.Equal(1, type0);
        Assert.Equal(25, val0);

        var (type3, val3) = OfficialPure15Supreme102DetailsEngine.GenerateMysteryRequirement(3);
        Assert.Equal(0, type3);
        Assert.Equal(28, val3);
    }

    [Fact]
    public void TestDetail2354_RollMysteryCurse()
    {
        var rng = new Random(1);
        bool cursed = OfficialPure15Supreme102DetailsEngine.RollMysteryCurse(rng);
        // Random(1).Next(100)
        Assert.False(cursed);
    }

    [Fact]
    public void TestDetail2355_CanUnequipMysteryItem()
    {
        var (ok, _) = OfficialPure15Supreme102DetailsEngine.CanUnequipMysteryItem(false);
        Assert.True(ok);

        var (fail, reason) = OfficialPure15Supreme102DetailsEngine.CanUnequipMysteryItem(true);
        Assert.False(fail);
        Assert.Contains("诅咒", reason);
    }

    [Fact]
    public void TestDetail2356_PurifyMysteryCurseWithHolyWater()
    {
        bool curse = true;
        bool purified = OfficialPure15Supreme102DetailsEngine.PurifyMysteryCurseWithHolyWater(ref curse);
        Assert.True(purified);
        Assert.False(curse);

        bool curse2 = false;
        bool purified2 = OfficialPure15Supreme102DetailsEngine.PurifyMysteryCurseWithHolyWater(ref curse2);
        Assert.False(purified2);
    }

    [Fact]
    public void TestDetail2357_DoesMysteryItemDropOnDeath()
    {
        var rng = new Random(42);
        bool drop = OfficialPure15Supreme102DetailsEngine.DoesMysteryItemDropOnDeath(rng);
        Assert.False(drop);
    }

    [Fact]
    public void TestDetail2358_GetUnknownDarkHallBossProfile()
    {
        var (realHp, isReal) = OfficialPure15Supreme102DetailsEngine.GetUnknownDarkHallBossProfile(true);
        Assert.Equal(2200, realHp);
        Assert.True(isReal);

        var (fakeHp, isFake) = OfficialPure15Supreme102DetailsEngine.GetUnknownDarkHallBossProfile(false);
        Assert.Equal(100, fakeHp);
        Assert.False(isFake);
    }

    [Fact]
    public void TestDetail2359_CanTameUnknownHallHuWei()
    {
        Assert.False(OfficialPure15Supreme102DetailsEngine.CanTameUnknownHallHuWei(28));
        Assert.True(OfficialPure15Supreme102DetailsEngine.CanTameUnknownHallHuWei(29));
        Assert.True(OfficialPure15Supreme102DetailsEngine.CanTameUnknownHallHuWei(35));
    }

    [Fact]
    public void TestDetail2360_ShouldEvictFromUnknownHall()
    {
        Assert.False(OfficialPure15Supreme102DetailsEngine.ShouldEvictFromUnknownHall(179));
        Assert.True(OfficialPure15Supreme102DetailsEngine.ShouldEvictFromUnknownHall(180));
        Assert.True(OfficialPure15Supreme102DetailsEngine.ShouldEvictFromUnknownHall(200));
    }

    // =========================================================================
    // 2. 白日门虎卫堂诱惑驯服、叛变与等级晋升成长模型 (2361~2370)
    // =========================================================================

    [Fact]
    public void TestDetail2361_CalculateHuWeiMaxHp()
    {
        Assert.Equal(400, OfficialPure15Supreme102DetailsEngine.CalculateHuWeiMaxHp(0));
        Assert.Equal(600, OfficialPure15Supreme102DetailsEngine.CalculateHuWeiMaxHp(1));
        Assert.Equal(1800, OfficialPure15Supreme102DetailsEngine.CalculateHuWeiMaxHp(7));
    }

    [Fact]
    public void TestDetail2362_GetHuWeiCombatAttributes()
    {
        var (ac0, mac0, minDc0, maxDc0) = OfficialPure15Supreme102DetailsEngine.GetHuWeiCombatAttributes(0);
        Assert.Equal(15, ac0);
        Assert.Equal(10, mac0);
        Assert.Equal(20, minDc0);
        Assert.Equal(35, maxDc0);

        var (ac7, mac7, minDc7, maxDc7) = OfficialPure15Supreme102DetailsEngine.GetHuWeiCombatAttributes(7);
        Assert.Equal(29, ac7);
        Assert.Equal(24, mac7);
        Assert.Equal(41, minDc7);
        Assert.Equal(70, maxDc7);
    }

    [Fact]
    public void TestDetail2363_GetHuWeiTameSuccessRate()
    {
        Assert.Equal(0, OfficialPure15Supreme102DetailsEngine.GetHuWeiTameSuccessRate(28));
        Assert.Equal(5, OfficialPure15Supreme102DetailsEngine.GetHuWeiTameSuccessRate(29));
        Assert.Equal(10, OfficialPure15Supreme102DetailsEngine.GetHuWeiTameSuccessRate(31));
        Assert.Equal(20, OfficialPure15Supreme102DetailsEngine.GetHuWeiTameSuccessRate(35));
    }

    [Fact]
    public void TestDetail2364_CanSpawnHuWeiInHall()
    {
        Assert.False(OfficialPure15Supreme102DetailsEngine.CanSpawnHuWeiInHall(1, 300));
        Assert.False(OfficialPure15Supreme102DetailsEngine.CanSpawnHuWeiInHall(0, 239));
        Assert.True(OfficialPure15Supreme102DetailsEngine.CanSpawnHuWeiInHall(0, 240));
    }

    [Fact]
    public void TestDetail2365_GetYingWeiProfile()
    {
        var (range, respawn) = OfficialPure15Supreme102DetailsEngine.GetYingWeiProfile();
        Assert.Equal(8, range);
        Assert.Equal(180, respawn);
    }

    [Fact]
    public void TestDetail2366_UpdateHuWeiLevelByKills()
    {
        var (lvl1, rem1) = OfficialPure15Supreme102DetailsEngine.UpdateHuWeiLevelByKills(0, 40, 15);
        Assert.Equal(1, lvl1);
        Assert.Equal(5, rem1);

        var (lvlMax, _) = OfficialPure15Supreme102DetailsEngine.UpdateHuWeiLevelByKills(6, 40, 200);
        Assert.Equal(7, lvlMax);
    }

    [Fact]
    public void TestDetail2367_CheckHuWeiNaturalBetrayal()
    {
        Assert.False(OfficialPure15Supreme102DetailsEngine.CheckHuWeiNaturalBetrayal(359));
        Assert.True(OfficialPure15Supreme102DetailsEngine.CheckHuWeiNaturalBetrayal(360));
    }

    [Fact]
    public void TestDetail2368_CheckHuWeiPrayerBetrayal()
    {
        Assert.False(OfficialPure15Supreme102DetailsEngine.CheckHuWeiPrayerBetrayal(false));
        Assert.True(OfficialPure15Supreme102DetailsEngine.CheckHuWeiPrayerBetrayal(true));
    }

    [Fact]
    public void TestDetail2369_IsHuWeiPreservedOnReconnect()
    {
        Assert.True(OfficialPure15Supreme102DetailsEngine.IsHuWeiPreservedOnReconnect(599));
        Assert.True(OfficialPure15Supreme102DetailsEngine.IsHuWeiPreservedOnReconnect(600));
        Assert.False(OfficialPure15Supreme102DetailsEngine.IsHuWeiPreservedOnReconnect(601));
    }

    [Fact]
    public void TestDetail2370_ShouldHuWeiBlinkToMaster()
    {
        Assert.False(OfficialPure15Supreme102DetailsEngine.ShouldHuWeiBlinkToMaster(100, 100, 110, 100));
        Assert.True(OfficialPure15Supreme102DetailsEngine.ShouldHuWeiBlinkToMaster(100, 100, 120, 100));
    }

    // =========================================================================
    // 3. 尸王殿隐藏地穴钻地刷新与三大 35 级神技爆率 (2371~2380)
    // =========================================================================

    [Fact]
    public void TestDetail2371_IsCorpseChamberEntranceTile()
    {
        Assert.True(OfficialPure15Supreme102DetailsEngine.IsCorpseChamberEntranceTile(100, 138, 107));
        Assert.False(OfficialPure15Supreme102DetailsEngine.IsCorpseChamberEntranceTile(100, 137, 107));
    }

    [Fact]
    public void TestDetail2372_TryEnterCorpseHole()
    {
        var (ok1, count1) = OfficialPure15Supreme102DetailsEngine.TryEnterCorpseHole(14, 50);
        Assert.True(ok1);
        Assert.Equal(15, count1);

        var (failCount, _) = OfficialPure15Supreme102DetailsEngine.TryEnterCorpseHole(15, 50);
        Assert.False(failCount);

        var (failTime, _) = OfficialPure15Supreme102DetailsEngine.TryEnterCorpseHole(5, 61);
        Assert.False(failTime);
    }

    [Fact]
    public void TestDetail2373_GetCorpseKingRespawnDelay()
    {
        Assert.Equal(120, OfficialPure15Supreme102DetailsEngine.GetCorpseKingRespawnDelay(true));
        Assert.Equal(20, OfficialPure15Supreme102DetailsEngine.GetCorpseKingRespawnDelay(false));
    }

    [Fact]
    public void TestDetail2374_CalculateCorpseKingChainDamage()
    {
        var rng = new Random(42);
        var (hit1, dmg1) = OfficialPure15Supreme102DetailsEngine.CalculateCorpseKingChainDamage(2, 0, rng);
        Assert.True(hit1);
        Assert.InRange(dmg1, 35, 50);

        var (hitFail, _) = OfficialPure15Supreme102DetailsEngine.CalculateCorpseKingChainDamage(3, 3, rng);
        Assert.False(hitFail);
    }

    [Fact]
    public void TestDetail2375_IsCorpseChamberScrollBlocked()
    {
        Assert.True(OfficialPure15Supreme102DetailsEngine.IsCorpseChamberScrollBlocked("D101"));
        Assert.True(OfficialPure15Supreme102DetailsEngine.IsCorpseChamberScrollBlocked("D1011"));
        Assert.False(OfficialPure15Supreme102DetailsEngine.IsCorpseChamberScrollBlocked("0"));
    }

    [Fact]
    public void TestDetail2376_RollCorpseKingUltimateSkillBook()
    {
        var rng = new Random(42);
        bool dropped = OfficialPure15Supreme102DetailsEngine.RollCorpseKingUltimateSkillBook(rng);
        Assert.False(dropped);
    }

    [Fact]
    public void TestDetail2377_GetCorpseKingAttackInterval()
    {
        Assert.Equal(800, OfficialPure15Supreme102DetailsEngine.GetCorpseKingAttackInterval(600, 1000));
        Assert.Equal(500, OfficialPure15Supreme102DetailsEngine.GetCorpseKingAttackInterval(500, 1000));
    }

    [Fact]
    public void TestDetail2378_IsCorpseChamberWaterObstacle()
    {
        Assert.True(OfficialPure15Supreme102DetailsEngine.IsCorpseChamberWaterObstacle(21, 21));
        Assert.False(OfficialPure15Supreme102DetailsEngine.IsCorpseChamberWaterObstacle(10, 10));
    }

    [Fact]
    public void TestDetail2379_ApplyHellThunderParalysisToUndead()
    {
        var (paralyzed, dur) = OfficialPure15Supreme102DetailsEngine.ApplyHellThunderParalysisToUndead(true);
        Assert.True(paralyzed);
        Assert.Equal(300L, dur);

        var (fail, _) = OfficialPure15Supreme102DetailsEngine.ApplyHellThunderParalysisToUndead(false);
        Assert.False(fail);
    }

    [Fact]
    public void TestDetail2380_GenerateCorpseKingBlackIronPurity()
    {
        var rng = new Random(42);
        int purity = OfficialPure15Supreme102DetailsEngine.GenerateCorpseKingBlackIronPurity(rng);
        Assert.InRange(purity, 10, 18);
    }

    // =========================================================================
    // 4. 死亡山谷触龙神百毒不侵与全屏毒雾绝杀机制 (2381~2390)
    // =========================================================================

    [Fact]
    public void TestDetail2381_ShouldEvilCentipedeAwaken()
    {
        Assert.True(OfficialPure15Supreme102DetailsEngine.ShouldEvilCentipedeAwaken(50, 50, 52, 53));
        Assert.False(OfficialPure15Supreme102DetailsEngine.ShouldEvilCentipedeAwaken(50, 50, 55, 55));
    }

    [Fact]
    public void TestDetail2382_IsEvilCentipedeImmuneToMagic()
    {
        Assert.True(OfficialPure15Supreme102DetailsEngine.IsEvilCentipedeImmuneToMagic());
    }

    [Fact]
    public void TestDetail2383_CalculateEvilCentipedePhysicalDamage()
    {
        int dmg = OfficialPure15Supreme102DetailsEngine.CalculateEvilCentipedePhysicalDamage(50, 30);
        Assert.Equal(20, dmg);

        int minDmg = OfficialPure15Supreme102DetailsEngine.CalculateEvilCentipedePhysicalDamage(20, 30);
        Assert.Equal(1, minDmg);
    }

    [Fact]
    public void TestDetail2384_GetEvilCentipedePoisonProfile()
    {
        var (dmg, dur) = OfficialPure15Supreme102DetailsEngine.GetEvilCentipedePoisonProfile();
        Assert.Equal(8, dmg);
        Assert.Equal(30, dur);
    }

    [Fact]
    public void TestDetail2385_RollCentipedeParalysis()
    {
        var rng = new Random(42);
        var (paralyzed, dur) = OfficialPure15Supreme102DetailsEngine.RollCentipedeParalysis(0, rng);
        Assert.False(paralyzed);
        Assert.Equal(0, dur);
    }

    [Fact]
    public void TestDetail2386_ShouldEvilCentipedeSubmerge()
    {
        Assert.False(OfficialPure15Supreme102DetailsEngine.ShouldEvilCentipedeSubmerge(59));
        Assert.True(OfficialPure15Supreme102DetailsEngine.ShouldEvilCentipedeSubmerge(60));
    }

    [Fact]
    public void TestDetail2387_IsCentipedeSignatureDrop()
    {
        Assert.True(OfficialPure15Supreme102DetailsEngine.IsCentipedeSignatureDrop("炼狱"));
        Assert.True(OfficialPure15Supreme102DetailsEngine.IsCentipedeSignatureDrop("魔杖"));
        Assert.True(OfficialPure15Supreme102DetailsEngine.IsCentipedeSignatureDrop("银蛇"));
        Assert.False(OfficialPure15Supreme102DetailsEngine.IsCentipedeSignatureDrop("裁决之杖"));
    }

    [Fact]
    public void TestDetail2388_IsEvilCentipedeUnmovable()
    {
        Assert.True(OfficialPure15Supreme102DetailsEngine.IsEvilCentipedeUnmovable());
    }

    [Fact]
    public void TestDetail2389_GetCentipedeSpawnConfig()
    {
        var (respawn, shake) = OfficialPure15Supreme102DetailsEngine.GetCentipedeSpawnConfig();
        Assert.Equal(180, respawn);
        Assert.Equal(500, shake);
    }

    [Fact]
    public void TestDetail2390_ApplyRedPoisonToCentipede()
    {
        int originalAc = 40;
        int debuffed = OfficialPure15Supreme102DetailsEngine.ApplyRedPoisonToCentipede(originalAc, true);
        Assert.Equal(32, debuffed); // 40 * 0.8 = 32

        int untouched = OfficialPure15Supreme102DetailsEngine.ApplyRedPoisonToCentipede(originalAc, false);
        Assert.Equal(40, untouched);
    }

    // =========================================================================
    // 5. 沙巴克城门损毁修缮、密道奇袭与皇宫旗帜易手机制 (2391~2400)
    // =========================================================================

    [Fact]
    public void TestDetail2391_DamageSabukGate()
    {
        var (hp1, destroyed1) = OfficialPure15Supreme102DetailsEngine.DamageSabukGate(50000, 10000);
        Assert.Equal(40000, hp1);
        Assert.False(destroyed1);

        var (hp2, destroyed2) = OfficialPure15Supreme102DetailsEngine.DamageSabukGate(1000, 2000);
        Assert.Equal(0, hp2);
        Assert.True(destroyed2);
    }

    [Fact]
    public void TestDetail2392_ValidateSabukGateRepairMaterials()
    {
        var (ok, _) = OfficialPure15Supreme102DetailsEngine.ValidateSabukGateRepairMaterials(200_000, 50, 15);
        Assert.True(ok);

        var (failGold, _) = OfficialPure15Supreme102DetailsEngine.ValidateSabukGateRepairMaterials(199_999, 50, 15);
        Assert.False(failGold);

        var (failIron, _) = OfficialPure15Supreme102DetailsEngine.ValidateSabukGateRepairMaterials(200_000, 49, 15);
        Assert.False(failIron);
    }

    [Fact]
    public void TestDetail2393_CheckSabukGateRepairProgress()
    {
        Assert.True(OfficialPure15Supreme102DetailsEngine.CheckSabukGateRepairProgress(300, false));
        Assert.False(OfficialPure15Supreme102DetailsEngine.CheckSabukGateRepairProgress(299, false));
        Assert.False(OfficialPure15Supreme102DetailsEngine.CheckSabukGateRepairProgress(350, true)); // 被攻击中断
    }

    [Fact]
    public void TestDetail2394_CanPassSecretPassageForFree()
    {
        Assert.True(OfficialPure15Supreme102DetailsEngine.CanPassSecretPassageForFree(true));
        Assert.False(OfficialPure15Supreme102DetailsEngine.CanPassSecretPassageForFree(false));
    }

    [Fact]
    public void TestDetail2395_ChargeSecretPassageFee()
    {
        var (okToken, rem1) = OfficialPure15Supreme102DetailsEngine.ChargeSecretPassageFee(true, 1000);
        Assert.True(okToken);
        Assert.Equal(1000, rem1);

        var (okGold, rem2) = OfficialPure15Supreme102DetailsEngine.ChargeSecretPassageFee(false, 6000);
        Assert.True(okGold);
        Assert.Equal(1000, rem2);

        var (failGold, rem3) = OfficialPure15Supreme102DetailsEngine.ChargeSecretPassageFee(false, 4000);
        Assert.False(failGold);
        Assert.Equal(4000, rem3);
    }

    [Fact]
    public void TestDetail2396_CanCapturePalace()
    {
        Assert.True(OfficialPure15Supreme102DetailsEngine.CanCapturePalace(5, 0, 10));
        Assert.False(OfficialPure15Supreme102DetailsEngine.CanCapturePalace(5, 1, 10));
        Assert.False(OfficialPure15Supreme102DetailsEngine.CanCapturePalace(5, 0, 9));
    }

    [Fact]
    public void TestDetail2397_IsSabukWarTime()
    {
        Assert.False(OfficialPure15Supreme102DetailsEngine.IsSabukWarTime(19, 59));
        Assert.True(OfficialPure15Supreme102DetailsEngine.IsSabukWarTime(20, 0));
        Assert.True(OfficialPure15Supreme102DetailsEngine.IsSabukWarTime(22, 59));
        Assert.True(OfficialPure15Supreme102DetailsEngine.IsSabukWarTime(23, 0));
        Assert.False(OfficialPure15Supreme102DetailsEngine.IsSabukWarTime(23, 1));
    }

    [Fact]
    public void TestDetail2398_CalculateSabukWarPkGain()
    {
        Assert.Equal(0, OfficialPure15Supreme102DetailsEngine.CalculateSabukWarPkGain(true, true));
        Assert.Equal(100, OfficialPure15Supreme102DetailsEngine.CalculateSabukWarPkGain(true, false));
        Assert.Equal(100, OfficialPure15Supreme102DetailsEngine.CalculateSabukWarPkGain(false, true));
    }

    [Fact]
    public void TestDetail2399_CalculateSabukDailyTax()
    {
        Assert.Equal(200_000, OfficialPure15Supreme102DetailsEngine.CalculateSabukDailyTax(1_000_000));
        Assert.Equal(2_000_000, OfficialPure15Supreme102DetailsEngine.CalculateSabukDailyTax(20_000_000)); // 达到上限封顶
    }

    [Fact]
    public void TestDetail2400_ApplySabukMemberDiscount()
    {
        Assert.Equal(800, OfficialPure15Supreme102DetailsEngine.ApplySabukMemberDiscount(1000, true));
        Assert.Equal(1000, OfficialPure15Supreme102DetailsEngine.ApplySabukMemberDiscount(1000, false));
    }

    // =========================================================================
    // 6. 战士刺杀破盾、双烈火与野蛮冲撞物理硬碰撞 (2401~2410)
    // =========================================================================

    [Fact]
    public void TestDetail2401_CalculateStabbingSwordDamage()
    {
        var (dmg1, pierce1) = OfficialPure15Supreme102DetailsEngine.CalculateStabbingSwordDamage(100, 1, 30);
        Assert.Equal(70, dmg1);
        Assert.False(pierce1);

        var (dmg2, pierce2) = OfficialPure15Supreme102DetailsEngine.CalculateStabbingSwordDamage(100, 2, 30);
        Assert.Equal(100, dmg2);
        Assert.True(pierce2);
    }

    [Fact]
    public void TestDetail2402_ApplyMagicShieldToStab()
    {
        int normalDmg = OfficialPure15Supreme102DetailsEngine.ApplyMagicShieldToStab(100, false, 0.60);
        Assert.Equal(40, normalDmg);

        int pierceDmg = OfficialPure15Supreme102DetailsEngine.ApplyMagicShieldToStab(100, true, 0.60);
        Assert.Equal(100, pierceDmg);
    }

    [Fact]
    public void TestDetail2403_IsFireHitStillCharged()
    {
        Assert.True(OfficialPure15Supreme102DetailsEngine.IsFireHitStillCharged(1000L, 10_000L));
        Assert.False(OfficialPure15Supreme102DetailsEngine.IsFireHitStillCharged(1000L, 11_500L));
    }

    [Fact]
    public void TestDetail2404_CalculateDoubleFireHitDamage()
    {
        var (canDouble, dmgDouble) = OfficialPure15Supreme102DetailsEngine.CalculateDoubleFireHitDamage(100, 9);
        Assert.True(canDouble);
        Assert.Equal(520, dmgDouble); // 260 * 2

        var (failDouble, singleDmg) = OfficialPure15Supreme102DetailsEngine.CalculateDoubleFireHitDamage(100, 5);
        Assert.False(failDouble);
        Assert.Equal(260, singleDmg);
    }

    [Fact]
    public void TestDetail2405_CanTackleTarget()
    {
        var (canPush, stunSelf1) = OfficialPure15Supreme102DetailsEngine.CanTackleTarget(36, 35);
        Assert.True(canPush);
        Assert.Equal(0L, stunSelf1);

        var (failPush, stunSelf2) = OfficialPure15Supreme102DetailsEngine.CanTackleTarget(35, 35);
        Assert.False(failPush);
        Assert.Equal(1000L, stunSelf2);
    }

    [Fact]
    public void TestDetail2406_GetTacklePushEffect()
    {
        var (dist, stun) = OfficialPure15Supreme102DetailsEngine.GetTacklePushEffect();
        Assert.Equal(2, dist);
        Assert.Equal(500L, stun);
    }

    [Fact]
    public void TestDetail2407_GetTackleWallCrashStunDuration()
    {
        Assert.Equal(1500L, OfficialPure15Supreme102DetailsEngine.GetTackleWallCrashStunDuration());
    }

    [Fact]
    public void TestDetail2408_CalculateHalfMoonDamage()
    {
        Assert.Equal(100, OfficialPure15Supreme102DetailsEngine.CalculateHalfMoonDamage(100, true));
        Assert.Equal(35, OfficialPure15Supreme102DetailsEngine.CalculateHalfMoonDamage(100, false));
    }

    [Fact]
    public void TestDetail2409_RollAssassinateStrike()
    {
        var rng = new Random(42);
        var (triggered, dmg) = OfficialPure15Supreme102DetailsEngine.RollAssassinateStrike(100, rng);
        Assert.False(triggered);
        Assert.Equal(100, dmg);
    }

    [Fact]
    public void TestDetail2410_CalculateWarriorAttackInterval()
    {
        Assert.Equal(800, OfficialPure15Supreme102DetailsEngine.CalculateWarriorAttackInterval(0));
        Assert.Equal(750, OfficialPure15Supreme102DetailsEngine.CalculateWarriorAttackInterval(1));
        Assert.Equal(650, OfficialPure15Supreme102DetailsEngine.CalculateWarriorAttackInterval(5)); // 触达下限
    }

    // =========================================================================
    // 7. 法师诱惑随从上限、抗拒火环与冰咆哮绝对命中 (2411~2420)
    // =========================================================================

    [Fact]
    public void TestDetail2411_CanTameMoreMinions()
    {
        Assert.True(OfficialPure15Supreme102DetailsEngine.CanTameMoreMinions(4));
        Assert.False(OfficialPure15Supreme102DetailsEngine.CanTameMoreMinions(5));
    }

    [Fact]
    public void TestDetail2412_IsMinionExpired()
    {
        Assert.False(OfficialPure15Supreme102DetailsEngine.IsMinionExpired(359));
        Assert.True(OfficialPure15Supreme102DetailsEngine.IsMinionExpired(360));
    }

    [Fact]
    public void TestDetail2413_CanRingOfFirePush()
    {
        var (pushed1, dist1) = OfficialPure15Supreme102DetailsEngine.CanRingOfFirePush(35, 30, false);
        Assert.True(pushed1);
        Assert.Equal(2, dist1);

        var (pushedBoss, _) = OfficialPure15Supreme102DetailsEngine.CanRingOfFirePush(40, 30, true);
        Assert.False(pushedBoss);
    }

    [Fact]
    public void TestDetail2414_DoesBlizzardIgnoreMagicDodge()
    {
        Assert.True(OfficialPure15Supreme102DetailsEngine.DoesBlizzardIgnoreMagicDodge());
    }

    [Fact]
    public void TestDetail2415_CalculateHellThunderDamage()
    {
        var (dmg1, stun1) = OfficialPure15Supreme102DetailsEngine.CalculateHellThunderDamage(50, true);
        Assert.Equal(100, dmg1);
        Assert.Equal(500L, stun1);

        var (dmg2, stun2) = OfficialPure15Supreme102DetailsEngine.CalculateHellThunderDamage(50, false);
        Assert.Equal(50, dmg2);
        Assert.Equal(0L, stun2);
    }

    [Fact]
    public void TestDetail2416_CalculateLaserDamage()
    {
        Assert.Equal(60, OfficialPure15Supreme102DetailsEngine.CalculateLaserDamage(60, 0));
        Assert.Equal(75, OfficialPure15Supreme102DetailsEngine.CalculateLaserDamage(60, 7)); // 60 * 1.25
    }

    [Fact]
    public void TestDetail2417_CalculateFireWallDuration()
    {
        int dur = OfficialPure15Supreme102DetailsEngine.CalculateFireWallDuration(3, 40);
        Assert.Equal(50, dur); // 3*10 + 40/2 = 50
    }

    [Fact]
    public void TestDetail2418_GetMagicShieldAbsorption()
    {
        Assert.Equal(0.40, OfficialPure15Supreme102DetailsEngine.GetMagicShieldAbsorption(0));
        Assert.Equal(0.50, OfficialPure15Supreme102DetailsEngine.GetMagicShieldAbsorption(1));
        Assert.Equal(0.60, OfficialPure15Supreme102DetailsEngine.GetMagicShieldAbsorption(2));
        Assert.Equal(0.68, OfficialPure15Supreme102DetailsEngine.GetMagicShieldAbsorption(3));
    }

    [Fact]
    public void TestDetail2419_CalculateHolyWordChance()
    {
        int c1 = OfficialPure15Supreme102DetailsEngine.CalculateHolyWordChance(35, 30, true);
        Assert.Equal(20, c1); // (35-30)*4 = 20

        int c2 = OfficialPure15Supreme102DetailsEngine.CalculateHolyWordChance(35, 30, false);
        Assert.Equal(0, c2);
    }

    [Fact]
    public void TestDetail2420_DoesDamageWakeUpTamedMonster()
    {
        Assert.True(OfficialPure15Supreme102DetailsEngine.DoesDamageWakeUpTamedMonster(true));
        Assert.False(OfficialPure15Supreme102DetailsEngine.DoesDamageWakeUpTamedMonster(false));
    }

    // =========================================================================
    // 8. 道士神兽防御/喷火双形态、骷髅进化与施毒术生态 (2421~2430)
    // =========================================================================

    [Fact]
    public void TestDetail2421_GetDivineBeastMaxHp()
    {
        Assert.Equal(500, OfficialPure15Supreme102DetailsEngine.GetDivineBeastMaxHp(0));
        Assert.Equal(2397, OfficialPure15Supreme102DetailsEngine.GetDivineBeastMaxHp(7));
    }

    [Fact]
    public void TestDetail2422_GetDivineBeastDefensiveMode()
    {
        var (ac, mac, heal) = OfficialPure15Supreme102DetailsEngine.GetDivineBeastDefensiveMode(20, 20, 1000);
        Assert.Equal(30, ac);
        Assert.Equal(30, mac);
        Assert.Equal(50, heal);
    }

    [Fact]
    public void TestDetail2423_CalculateDivineBeastFlameDamage()
    {
        var (main, pierce) = OfficialPure15Supreme102DetailsEngine.CalculateDivineBeastFlameDamage(60);
        Assert.Equal(60, main);
        Assert.Equal(30, pierce);
    }

    [Fact]
    public void TestDetail2424_GetSkeletonEvolutionProfile()
    {
        var (w1, i1) = OfficialPure15Supreme102DetailsEngine.GetSkeletonEvolutionProfile(1);
        Assert.Equal("木斧", w1);
        Assert.Equal(600, i1);

        var (w7, i7) = OfficialPure15Supreme102DetailsEngine.GetSkeletonEvolutionProfile(7);
        Assert.Equal("大板斧", w7);
        Assert.Equal(350, i7);
    }

    [Fact]
    public void TestDetail2425_CalculateTaoistPetHealAmount()
    {
        Assert.Equal(130, OfficialPure15Supreme102DetailsEngine.CalculateTaoistPetHealAmount(100, true));
        Assert.Equal(100, OfficialPure15Supreme102DetailsEngine.CalculateTaoistPetHealAmount(100, false));
    }

    [Fact]
    public void TestDetail2426_ApplyTaoistRedPoison()
    {
        var (ac, mac, dur) = OfficialPure15Supreme102DetailsEngine.ApplyTaoistRedPoison(20, 20);
        Assert.Equal(16, ac);
        Assert.Equal(16, mac);
        Assert.Equal(60, dur);
    }

    [Fact]
    public void TestDetail2427_ApplyTaoistGreenPoison()
    {
        var (dmg, freeze) = OfficialPure15Supreme102DetailsEngine.ApplyTaoistGreenPoison(24);
        Assert.Equal(6, dmg);
        Assert.True(freeze);
    }

    [Fact]
    public void TestDetail2428_ConsumePoisonPouchDura()
    {
        var (succ, rem) = OfficialPure15Supreme102DetailsEngine.ConsumePoisonPouchDura(10);
        Assert.True(succ);
        Assert.Equal(9, rem);

        var (fail, _) = OfficialPure15Supreme102DetailsEngine.ConsumePoisonPouchDura(0);
        Assert.False(fail);
    }

    [Fact]
    public void TestDetail2429_ConsumeAmuletDura()
    {
        var (succ, rem) = OfficialPure15Supreme102DetailsEngine.ConsumeAmuletDura(100);
        Assert.True(succ);
        Assert.Equal(99, rem);

        var (fail, _) = OfficialPure15Supreme102DetailsEngine.ConsumeAmuletDura(0);
        Assert.False(fail);
    }

    [Fact]
    public void TestDetail2430_IsInvisibilityCancelled()
    {
        Assert.True(OfficialPure15Supreme102DetailsEngine.IsInvisibilityCancelled(true, false, false));
        Assert.True(OfficialPure15Supreme102DetailsEngine.IsInvisibilityCancelled(false, true, false));
        Assert.True(OfficialPure15Supreme102DetailsEngine.IsInvisibilityCancelled(false, false, true));
        Assert.False(OfficialPure15Supreme102DetailsEngine.IsInvisibilityCancelled(false, false, false));
    }

    // =========================================================================
    // 9. 红名大盗大刀卫士秒杀、死亡大爆与红名村流放机制 (2431~2440)
    // =========================================================================

    [Fact]
    public void TestDetail2431_UpdatePkStatusOnKill()
    {
        var (c1, p1) = OfficialPure15Supreme102DetailsEngine.UpdatePkStatusOnKill(0, true);
        Assert.Equal("黄名", c1);
        Assert.Equal(100, p1);

        var (c2, p2) = OfficialPure15Supreme102DetailsEngine.UpdatePkStatusOnKill(100, true);
        Assert.Equal("红名", c2);
        Assert.Equal(200, p2);
    }

    [Fact]
    public void TestDetail2432_GuardExecuteRedName()
    {
        var (exec, dmg) = OfficialPure15Supreme102DetailsEngine.GuardExecuteRedName(5, true);
        Assert.True(exec);
        Assert.Equal(99999, dmg);

        var (failDist, _) = OfficialPure15Supreme102DetailsEngine.GuardExecuteRedName(9, true);
        Assert.False(failDist);

        var (failWhite, _) = OfficialPure15Supreme102DetailsEngine.GuardExecuteRedName(5, false);
        Assert.False(failWhite);
    }

    [Fact]
    public void TestDetail2433_CalculateRedNameDeathLoot()
    {
        var rng = new Random(42);
        var (dropEquip, bagCount) = OfficialPure15Supreme102DetailsEngine.CalculateRedNameDeathLoot(rng);
        Assert.InRange(bagCount, 2, 4);
    }

    [Fact]
    public void TestDetail2434_GetRecallDestination()
    {
        var (mRed, xRed, yRed) = OfficialPure15Supreme102DetailsEngine.GetRecallDestination(true);
        Assert.Equal("3", mRed);
        Assert.Equal(850, xRed);
        Assert.Equal(670, yRed);

        var (mWhite, xWhite, yWhite) = OfficialPure15Supreme102DetailsEngine.GetRecallDestination(false);
        Assert.Equal("0", mWhite);
        Assert.Equal(330, xWhite);
        Assert.Equal(270, yWhite);
    }

    [Fact]
    public void TestDetail2435_DecayPkPoints()
    {
        Assert.Equal(80, OfficialPure15Supreme102DetailsEngine.DecayPkPoints(100, false, 1200L)); // 1200s / 60 = 20 pts
        Assert.Equal(100, OfficialPure15Supreme102DetailsEngine.DecayPkPoints(100, true, 1200L)); // 安全区不扣
    }

    [Fact]
    public void TestDetail2436_CalculateSelfDefensePk()
    {
        Assert.Equal(0, OfficialPure15Supreme102DetailsEngine.CalculateSelfDefensePk(true));
        Assert.Equal(100, OfficialPure15Supreme102DetailsEngine.CalculateSelfDefensePk(false));
    }

    [Fact]
    public void TestDetail2437_GetRedNameVillageItemPrice()
    {
        Assert.Equal(2000, OfficialPure15Supreme102DetailsEngine.GetRedNameVillageItemPrice(1000));
    }

    [Fact]
    public void TestDetail2438_RollWeaponCurseOnKill()
    {
        var rng = new Random(42);
        int curse = OfficialPure15Supreme102DetailsEngine.RollWeaponCurseOnKill(0, rng);
        Assert.Equal(0, curse);
    }

    [Fact]
    public void TestDetail2439_CalculateCurse10Damage()
    {
        Assert.Equal(15, OfficialPure15Supreme102DetailsEngine.CalculateCurse10Damage(15, 30, 10));
        Assert.Equal(30, OfficialPure15Supreme102DetailsEngine.CalculateCurse10Damage(15, 30, 5));
    }

    [Fact]
    public void TestDetail2440_CalculateLuck9Damage()
    {
        Assert.Equal(35, OfficialPure15Supreme102DetailsEngine.CalculateLuck9Damage(10, 35, 9));
        Assert.Equal(10, OfficialPure15Supreme102DetailsEngine.CalculateLuck9Damage(10, 35, 5));
    }

    // =========================================================================
    // 10. Direct-Blit 零分配强类型紧凑二进制封包与 Dumb Client 权威覆盖 (2441~2450)
    // =========================================================================

    [Fact]
    public void TestDetail2441_ValidatePacketMagic()
    {
        Assert.True(OfficialPure15Supreme102DetailsEngine.ValidatePacketMagic(0x594D));
        Assert.False(OfficialPure15Supreme102DetailsEngine.ValidatePacketMagic(0x1234));
    }

    [Fact]
    public void TestDetail2442_ValidateSlidingWindowSequence()
    {
        Assert.True(OfficialPure15Supreme102DetailsEngine.ValidateSlidingWindowSequence(100, 101));
        Assert.True(OfficialPure15Supreme102DetailsEngine.ValidateSlidingWindowSequence(100, 164));
        Assert.False(OfficialPure15Supreme102DetailsEngine.ValidateSlidingWindowSequence(100, 100)); // 重放
        Assert.False(OfficialPure15Supreme102DetailsEngine.ValidateSlidingWindowSequence(100, 165)); // 乱序超出窗口
    }

    [Fact]
    public void TestDetail2443_CalculateCrc16()
    {
        byte[] data = [1, 2, 3, 4, 5];
        ushort crc1 = OfficialPure15Supreme102DetailsEngine.CalculateCrc16(data);
        ushort crc2 = OfficialPure15Supreme102DetailsEngine.CalculateCrc16(data);
        Assert.Equal(crc1, crc2);
        Assert.NotEqual((ushort)0, crc1);
    }

    [Fact]
    public void TestDetail2444_GetCompactHeaderSize()
    {
        Assert.Equal(8, OfficialPure15Supreme102DetailsEngine.GetCompactHeaderSize());
    }

    [Fact]
    public void TestDetail2445_ValidateDirectBlitPayloadSize()
    {
        Assert.True(OfficialPure15Supreme102DetailsEngine.ValidateDirectBlitPayloadSize<int>(4));
        Assert.True(OfficialPure15Supreme102DetailsEngine.ValidateDirectBlitPayloadSize<long>(8));
    }

    [Fact]
    public void TestDetail2446_ApplyServerAuthoritativeGauges()
    {
        var (hp, mp) = OfficialPure15Supreme102DetailsEngine.ApplyServerAuthoritativeGauges(500, 200);
        Assert.Equal(500, hp);
        Assert.Equal(200, mp);
    }

    [Fact]
    public void TestDetail2447_GetAttackActionLockTimers()
    {
        var (pre, post) = OfficialPure15Supreme102DetailsEngine.GetAttackActionLockTimers();
        Assert.Equal(350L, pre);
        Assert.Equal(450L, post);
    }

    [Fact]
    public void TestDetail2448_ValidateMoveStep()
    {
        Assert.True(OfficialPure15Supreme102DetailsEngine.ValidateMoveStep(10, 10, 11, 10, false)); // 走 1 格
        Assert.True(OfficialPure15Supreme102DetailsEngine.ValidateMoveStep(10, 10, 12, 10, true));  // 跑 2 格
        Assert.False(OfficialPure15Supreme102DetailsEngine.ValidateMoveStep(10, 10, 13, 10, true)); // 超距跑 3 格拦截
        Assert.False(OfficialPure15Supreme102DetailsEngine.ValidateMoveStep(10, 10, 12, 10, false)); // 走步跑 2 格拦截
    }

    [Fact]
    public void TestDetail2449_CanPickUpGroundItem()
    {
        Assert.True(OfficialPure15Supreme102DetailsEngine.CanPickUpGroundItem(100, 100, 100, 100));
        Assert.False(OfficialPure15Supreme102DetailsEngine.CanPickUpGroundItem(100, 100, 101, 100));
    }

    [Fact]
    public void TestDetail2450_IsTradeDistanceValid()
    {
        Assert.True(OfficialPure15Supreme102DetailsEngine.IsTradeDistanceValid(10, 10, 12, 12));
        Assert.False(OfficialPure15Supreme102DetailsEngine.IsTradeDistanceValid(10, 10, 15, 10)); // 超出 3 格自动熔断
    }
}
