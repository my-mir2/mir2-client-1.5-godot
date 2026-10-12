using System;
using System.Collections.Generic;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 盛大官方 1.5 绝顶封神 100 微观细节与服务端权威闭环测试矩阵 (Wave 92, Details 1351 ~ 1450)
/// 严格 1:1 覆盖封魔谷全景生态、暗之六大重装、升级跳点、沙城攻城与防外挂
/// </summary>
public class TestOfficialWave92_100DetailsZenithMatrix
{
    // =========================================================================
    // 细节 1351 ~ 1365: 封魔谷全景生态、封魔殿教主与千年树妖系统
    // =========================================================================

    [Fact]
    public void TestDetail1351_IsFengMoTownSafeZone()
    {
        Assert.True(OfficialZenithPinnacle100DetailsEngine.IsFengMoTownSafeZone(240, 200));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.IsFengMoTownSafeZone(220, 200));
    }

    [Fact]
    public void TestDetail1352_ValidateFengMoMinePassage()
    {
        Assert.True(OfficialZenithPinnacle100DetailsEngine.ValidateFengMoMinePassage("D2001", "D2002"));
        Assert.True(OfficialZenithPinnacle100DetailsEngine.ValidateFengMoMinePassage("D2004", "D2008"));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.ValidateFengMoMinePassage("D2001", "D2008"));
    }

    [Fact]
    public void TestDetail1353_CalculateMillenniumTreeAttack()
    {
        long last = 1000L;
        var (ready1, dmg1) = OfficialZenithPinnacle100DetailsEngine.CalculateMillenniumTreeAttack(last, last + 2000L, 20);
        Assert.False(ready1);
        Assert.Equal(0, dmg1);

        var (ready2, dmg2) = OfficialZenithPinnacle100DetailsEngine.CalculateMillenniumTreeAttack(last, last + 3500L, 20);
        Assert.True(ready2);
        Assert.Equal(45, dmg2); // 55 - 10
    }

    [Fact]
    public void TestDetail1354_IsTreeBossInWaterCenter()
    {
        Assert.True(OfficialZenithPinnacle100DetailsEngine.IsTreeBossInWaterCenter(124, 158));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.IsTreeBossInWaterCenter(120, 150));
    }

    [Fact]
    public void TestDetail1355_GetHongMoBossGuards()
    {
        var (g1, g2) = OfficialZenithPinnacle100DetailsEngine.GetHongMoBossGuards();
        Assert.Equal("虹魔猪卫", g1);
        Assert.Equal("虹魔蝎卫", g2);
    }

    [Fact]
    public void TestDetail1356_CalculateHongMoPigGuardDamage()
    {
        int norm = OfficialZenithPinnacle100DetailsEngine.CalculateHongMoPigGuardDamage(50, false);
        int crit = OfficialZenithPinnacle100DetailsEngine.CalculateHongMoPigGuardDamage(50, true);
        Assert.Equal(50, norm);
        Assert.Equal(75, crit);
    }

    [Fact]
    public void TestDetail1357_ApplyHongMoScorpionPoison()
    {
        var (ac, mac) = OfficialZenithPinnacle100DetailsEngine.ApplyHongMoScorpionPoison(40, 30);
        Assert.Equal(28, ac);
        Assert.Equal(21, mac);
    }

    [Fact]
    public void TestDetail1358_IsHongMoLeaderRareLoot()
    {
        Assert.True(OfficialZenithPinnacle100DetailsEngine.IsHongMoLeaderRareLoot("魔血项链"));
        Assert.True(OfficialZenithPinnacle100DetailsEngine.IsHongMoLeaderRareLoot("祈祷之刃"));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.IsHongMoLeaderRareLoot("裁决之杖"));
    }

    [Fact]
    public void TestDetail1359_CalculateEvilGiantDamage()
    {
        var rng = new Random(42);
        var (dmg, stunned) = OfficialZenithPinnacle100DetailsEngine.CalculateEvilGiantDamage(60, rng);
        Assert.Equal(60, dmg);
    }

    [Fact]
    public void TestDetail1360_IsBaZheHallNpcLocation()
    {
        Assert.True(OfficialZenithPinnacle100DetailsEngine.IsBaZheHallNpcLocation(23, 27));
        Assert.True(OfficialZenithPinnacle100DetailsEngine.IsBaZheHallNpcLocation(35, 42));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.IsBaZheHallNpcLocation(10, 10));
    }

    [Fact]
    public void TestDetail1361_EvaluateThunderRoadExit()
    {
        var (isTrue, map) = OfficialZenithPinnacle100DetailsEngine.EvaluateThunderRoadExit(2);
        Assert.True(isTrue);
        Assert.Equal("D2004", map);

        var (isFake, fakeMap) = OfficialZenithPinnacle100DetailsEngine.EvaluateThunderRoadExit(1);
        Assert.False(isFake);
        Assert.Equal("D2002", fakeMap);
    }

    [Fact]
    public void TestDetail1362_EvaluateZombieResurrection()
    {
        var rng = new Random(10);
        var (res, hp, exp) = OfficialZenithPinnacle100DetailsEngine.EvaluateZombieResurrection(rng, 100);
        Assert.False(exp);
        if (res)
        {
            Assert.Equal(50, hp);
        }
        else
        {
            Assert.Equal(0, hp);
        }
    }

    [Fact]
    public void TestDetail1363_ValidateWomaHornAuthenticity()
    {
        Assert.True(OfficialZenithPinnacle100DetailsEngine.ValidateWomaHornAuthenticity("沃玛号角", true));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.ValidateWomaHornAuthenticity("沃玛号角", false));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.ValidateWomaHornAuthenticity("回城卷", true));
    }

    [Fact]
    public void TestDetail1364_CanGatherTreeFruit()
    {
        Assert.True(OfficialZenithPinnacle100DetailsEngine.CanGatherTreeFruit(120, 150));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.CanGatherTreeFruit(100, 100));
    }

    [Fact]
    public void TestDetail1365_GetFengMoGuardDamage()
    {
        Assert.Equal(99999, OfficialZenithPinnacle100DetailsEngine.GetFengMoGuardDamage(true));
        Assert.Equal(0, OfficialZenithPinnacle100DetailsEngine.GetFengMoGuardDamage(false));
    }

    // =========================================================================
    // 细节 1366 ~ 1380: 白日门丛林迷宫、赤月首领产出与官方 1.5 三大终极战袍权威深水区
    // =========================================================================

    [Fact]
    public void TestDetail1366_GetDarkBossArmorLoot()
    {
        Assert.Equal("法神首饰", OfficialZenithPinnacle100DetailsEngine.GetDarkBossArmorLoot("双头金刚"));
        Assert.Equal("圣战首饰", OfficialZenithPinnacle100DetailsEngine.GetDarkBossArmorLoot("双头血魔"));
        Assert.Equal(string.Empty, OfficialZenithPinnacle100DetailsEngine.GetDarkBossArmorLoot("白野猪"));
    }

    [Fact]
    public void TestDetail1367_RequiresButchForTianZunTao()
    {
        Assert.True(OfficialZenithPinnacle100DetailsEngine.RequiresButchForTianZunTao("双头金刚"));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.RequiresButchForTianZunTao("白野猪"));
    }

    [Fact]
    public void TestDetail1368_IsDarkAbyssWellEntrance()
    {
        Assert.True(OfficialZenithPinnacle100DetailsEngine.IsDarkAbyssWellEntrance(428, 474));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.IsDarkAbyssWellEntrance(400, 400));
    }

    [Fact]
    public void TestDetail1369_IsDarkBossSpawnDue()
    {
        long last = 1000L;
        Assert.False(OfficialZenithPinnacle100DetailsEngine.IsDarkBossSpawnDue(last, last + 5 * 3600 * 1000L));
        Assert.True(OfficialZenithPinnacle100DetailsEngine.IsDarkBossSpawnDue(last, last + 6 * 3600 * 1000L));
    }

    [Fact]
    public void TestDetail1370_ValidateLevel40Armor()
    {
        var (ok1, _) = OfficialZenithPinnacle100DetailsEngine.ValidateLevel40Armor(35, 2, 0, "幽灵战衣(男)");
        Assert.True(ok1);

        var (ok2, _) = OfficialZenithPinnacle100DetailsEngine.ValidateLevel40Armor(20, 2, 0, "幽灵战衣(男)");
        Assert.False(ok2); // 等级不够

        var (ok3, _) = OfficialZenithPinnacle100DetailsEngine.ValidateLevel40Armor(35, 2, 1, "幽灵战衣(男)");
        Assert.False(ok3); // 性别不符
    }

    [Fact]
    public void TestDetail1371_GetTianZunTaoStats()
    {
        var (minAc, maxAc, minMac, maxMac, minSc, maxSc, weight, dura) = OfficialZenithPinnacle100DetailsEngine.GetTianZunTaoStats();
        Assert.Equal(4, minAc);
        Assert.Equal(7, maxAc);
        Assert.Equal(26, weight);
    }

    [Fact]
    public void TestDetail1372_GetTianShiChangPaoStats()
    {
        var (minAc, maxAc, minMac, maxMac, minSc, maxSc, weight, dura) = OfficialZenithPinnacle100DetailsEngine.GetTianShiChangPaoStats();
        Assert.Equal(4, minAc);
        Assert.Equal(4, maxSc);
        Assert.Equal(28, dura);
    }

    [Fact]
    public void TestDetail1373_GetShengZhanBaoJiaStats()
    {
        var (minAc, maxAc, minMac, maxMac, minDc, maxDc, weight, dura) = OfficialZenithPinnacle100DetailsEngine.GetShengZhanBaoJiaStats();
        Assert.Equal(5, minAc);
        Assert.Equal(9, maxAc);
        Assert.Equal(40, weight);
    }

    [Fact]
    public void TestDetail1374_GetShengZhanZhanJiaStats()
    {
        var (minAc, maxAc, minMac, maxMac, minDc, maxDc, weight, dura) = OfficialZenithPinnacle100DetailsEngine.GetShengZhanZhanJiaStats();
        Assert.Equal(5, minAc);
        Assert.Equal(0, maxDc);
        Assert.Equal(30, dura);
    }

    [Fact]
    public void TestDetail1375_GetFaShenPiFengStats()
    {
        var (minAc, maxAc, minMac, maxMac, minMc, maxMc, weight, dura) = OfficialZenithPinnacle100DetailsEngine.GetFaShenPiFengStats();
        Assert.Equal(4, minAc);
        Assert.Equal(4, maxMc);
        Assert.Equal(19, weight);
    }

    [Fact]
    public void TestDetail1376_GetNiShangYuYiStats()
    {
        var (minAc, maxAc, minMac, maxMac, minMc, maxMc, weight, dura) = OfficialZenithPinnacle100DetailsEngine.GetNiShangYuYiStats();
        Assert.Equal(4, minAc);
        Assert.Equal(4, maxMac);
        Assert.Equal(25, dura);
    }

    [Fact]
    public void TestDetail1377_IsJungleMazeEntrance()
    {
        Assert.True(OfficialZenithPinnacle100DetailsEngine.IsJungleMazeEntrance(350, 240));
        Assert.True(OfficialZenithPinnacle100DetailsEngine.IsJungleMazeEntrance(320, 560));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.IsJungleMazeEntrance(100, 100));
    }

    [Fact]
    public void TestDetail1378_CalculateRottenSpiderPoisonTick()
    {
        Assert.Equal(8, OfficialZenithPinnacle100DetailsEngine.CalculateRottenSpiderPoisonTick());
    }

    [Fact]
    public void TestDetail1379_IsJungleMazeForbiddenTeleport()
    {
        Assert.True(OfficialZenithPinnacle100DetailsEngine.IsJungleMazeForbiddenTeleport("D401"));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.IsJungleMazeForbiddenTeleport("0"));
    }

    [Fact]
    public void TestDetail1380_ChargeMazeGuideFee()
    {
        var (ok1, gold1) = OfficialZenithPinnacle100DetailsEngine.ChargeMazeGuideFee(2000);
        Assert.True(ok1);
        Assert.Equal(1000, gold1);

        var (ok2, gold2) = OfficialZenithPinnacle100DetailsEngine.ChargeMazeGuideFee(500);
        Assert.False(ok2);
        Assert.Equal(500, gold2);
    }

    // =========================================================================
    // 细节 1381 ~ 1395: 武器升级黑铁矿持久公式、极品属性首饰与取刀机制闭环
    // =========================================================================

    [Fact]
    public void TestDetail1381_CalculateBlackIronDurabilityDeltaAdvanced()
    {
        Assert.Equal(2, OfficialZenithPinnacle100DetailsEngine.CalculateBlackIronDurabilityDeltaAdvanced(125));
        Assert.Equal(1, OfficialZenithPinnacle100DetailsEngine.CalculateBlackIronDurabilityDeltaAdvanced(95));
        Assert.Equal(0, OfficialZenithPinnacle100DetailsEngine.CalculateBlackIronDurabilityDeltaAdvanced(75));
        Assert.Equal(-1, OfficialZenithPinnacle100DetailsEngine.CalculateBlackIronDurabilityDeltaAdvanced(40));
        Assert.Equal(-3, OfficialZenithPinnacle100DetailsEngine.CalculateBlackIronDurabilityDeltaAdvanced(20));
    }

    [Fact]
    public void TestDetail1382_CalculateJewelryUpgradeScore()
    {
        int score = OfficialZenithPinnacle100DetailsEngine.CalculateJewelryUpgradeScore(30, 2);
        Assert.Equal(66, score); // 30*2 + 2*3
    }

    [Fact]
    public void TestDetail1383_ResolveUpgradeStatTie()
    {
        Assert.Equal(0, OfficialZenithPinnacle100DetailsEngine.ResolveUpgradeStatTie(10, 10, 10)); // DC优先
        Assert.Equal(1, OfficialZenithPinnacle100DetailsEngine.ResolveUpgradeStatTie(5, 10, 10));  // MC优先
        Assert.Equal(2, OfficialZenithPinnacle100DetailsEngine.ResolveUpgradeStatTie(5, 5, 10));   // SC
    }

    [Fact]
    public void TestDetail1384_CanSubmitAnotherUpgrade()
    {
        Assert.True(OfficialZenithPinnacle100DetailsEngine.CanSubmitAnotherUpgrade(false));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.CanSubmitAnotherUpgrade(true));
    }

    [Fact]
    public void TestDetail1385_IsUpgradeAbandonedTimeout()
    {
        long submit = 1000L;
        Assert.False(OfficialZenithPinnacle100DetailsEngine.IsUpgradeAbandonedTimeout(submit, submit + 6L * 24 * 3600 * 1000L));
        Assert.True(OfficialZenithPinnacle100DetailsEngine.IsUpgradeAbandonedTimeout(submit, submit + 8L * 24 * 3600 * 1000L));
    }

    [Fact]
    public void TestDetail1386_DoesSlashTargetInfluenceOutcome()
    {
        Assert.False(OfficialZenithPinnacle100DetailsEngine.DoesSlashTargetInfluenceOutcome("大刀卫士"));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.DoesSlashTargetInfluenceOutcome("红名玩家"));
    }

    [Fact]
    public void TestDetail1387_GetUpgradeSuccessSpellEffectId()
    {
        Assert.Equal(26, OfficialZenithPinnacle100DetailsEngine.GetUpgradeSuccessSpellEffectId());
    }

    [Fact]
    public void TestDetail1388_RollUpgradeAttributeIncrement()
    {
        var rng = new Random(100);
        int inc = OfficialZenithPinnacle100DetailsEngine.RollUpgradeAttributeIncrement(rng);
        Assert.InRange(inc, 1, 2);
    }

    [Fact]
    public void TestDetail1389_GetDetailedBlessingOilSuccessRate()
    {
        Assert.Equal(70, OfficialZenithPinnacle100DetailsEngine.GetDetailedBlessingOilSuccessRate(0));
        Assert.Equal(3, OfficialZenithPinnacle100DetailsEngine.GetDetailedBlessingOilSuccessRate(6));
        Assert.Equal(0, OfficialZenithPinnacle100DetailsEngine.GetDetailedBlessingOilSuccessRate(7));
    }

    [Fact]
    public void TestDetail1390_RollBlessingOilCursePenalty()
    {
        var rng = new Random(42);
        bool curse = OfficialZenithPinnacle100DetailsEngine.RollBlessingOilCursePenalty(rng);
        Assert.False(curse); // 确定性种子检查
    }

    [Fact]
    public void TestDetail1391_CapWeaponCurse()
    {
        Assert.Equal(10, OfficialZenithPinnacle100DetailsEngine.CapWeaponCurse(15));
        Assert.Equal(5, OfficialZenithPinnacle100DetailsEngine.CapWeaponCurse(5));
    }

    [Fact]
    public void TestDetail1392_CalculateLuoShaDamage()
    {
        Assert.Equal(15, OfficialZenithPinnacle100DetailsEngine.CalculateLuoShaDamage(10));
        Assert.Equal(0, OfficialZenithPinnacle100DetailsEngine.CalculateLuoShaDamage(5));
    }

    [Fact]
    public void TestDetail1393_RollBraceletRareDefense()
    {
        var rng = new Random(50);
        int bonus = OfficialZenithPinnacle100DetailsEngine.RollBraceletRareDefense(rng);
        Assert.InRange(bonus, 1, 3);
    }

    [Fact]
    public void TestDetail1394_RollRingRareBonus()
    {
        var rng = new Random(50);
        int bonus = OfficialZenithPinnacle100DetailsEngine.RollRingRareBonus(rng);
        Assert.InRange(bonus, 1, 4);
    }

    [Fact]
    public void TestDetail1395_PerformSpecialRepair()
    {
        var (newDura, newMaxDura) = OfficialZenithPinnacle100DetailsEngine.PerformSpecialRepair(30);
        Assert.Equal(30, newDura);
        Assert.Equal(30, newMaxDura);
    }

    // =========================================================================
    // 细节 1396 ~ 1410: 沙巴克攻城、皇宫旗帜夺取、城墙密道与行会战火
    // =========================================================================

    [Fact]
    public void TestDetail1396_ValidateSabukApplication()
    {
        var (ok1, _) = OfficialZenithPinnacle100DetailsEngine.ValidateSabukApplication(true, true, 1000000);
        Assert.True(ok1);

        var (ok2, _) = OfficialZenithPinnacle100DetailsEngine.ValidateSabukApplication(false, true, 1000000);
        Assert.False(ok2);
    }

    [Fact]
    public void TestDetail1397_CalculateWarStartTime()
    {
        long apply = 1000L;
        long start = OfficialZenithPinnacle100DetailsEngine.CalculateWarStartTime(apply);
        Assert.Equal(apply + 3L * 24 * 3600 * 1000L, start);
    }

    [Fact]
    public void TestDetail1398_IsWarHour()
    {
        Assert.True(OfficialZenithPinnacle100DetailsEngine.IsWarHour(20));
        Assert.True(OfficialZenithPinnacle100DetailsEngine.IsWarHour(22));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.IsWarHour(23));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.IsWarHour(19));
    }

    [Fact]
    public void TestDetail1399_IsPassageOpenAfterGateDestroyed()
    {
        Assert.True(OfficialZenithPinnacle100DetailsEngine.IsPassageOpenAfterGateDestroyed(true));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.IsPassageOpenAfterGateDestroyed(false));
    }

    [Fact]
    public void TestDetail1400_GetPalaceGateMaxHp()
    {
        Assert.Equal(20000, OfficialZenithPinnacle100DetailsEngine.GetPalaceGateMaxHp());
    }

    [Fact]
    public void TestDetail1401_IsPalaceScanDue()
    {
        long last = 1000L;
        Assert.False(OfficialZenithPinnacle100DetailsEngine.IsPalaceScanDue(last, last + 4000L));
        Assert.True(OfficialZenithPinnacle100DetailsEngine.IsPalaceScanDue(last, last + 5500L));
    }

    [Fact]
    public void TestDetail1402_GetWarPkWorth()
    {
        Assert.Equal(0, OfficialZenithPinnacle100DetailsEngine.GetWarPkWorth(true));
        Assert.Equal(100, OfficialZenithPinnacle100DetailsEngine.GetWarPkWorth(false));
    }

    [Fact]
    public void TestDetail1403_ApplyWarDeathDropProtection()
    {
        Assert.Equal(15, OfficialZenithPinnacle100DetailsEngine.ApplyWarDeathDropProtection(30));
    }

    [Fact]
    public void TestDetail1404_ChargeGuildRecallFee()
    {
        var (ok1, rem1) = OfficialZenithPinnacle100DetailsEngine.ChargeGuildRecallFee(60000);
        Assert.True(ok1);
        Assert.Equal(10000, rem1);

        var (ok2, rem2) = OfficialZenithPinnacle100DetailsEngine.ChargeGuildRecallFee(30000);
        Assert.False(ok2);
        Assert.Equal(30000, rem2);
    }

    [Fact]
    public void TestDetail1405_FormatWarGuildTitle()
    {
        Assert.Equal("[战争敌对] 邪恶之眼", OfficialZenithPinnacle100DetailsEngine.FormatWarGuildTitle("邪恶之眼", true));
        Assert.Equal("[联盟盟友] 兄弟军团", OfficialZenithPinnacle100DetailsEngine.FormatWarGuildTitle("兄弟军团", false));
    }

    [Fact]
    public void TestDetail1406_IsGuildWarTie()
    {
        Assert.True(OfficialZenithPinnacle100DetailsEngine.IsGuildWarTie(10, 10));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.IsGuildWarTie(12, 10));
    }

    [Fact]
    public void TestDetail1407_CanJoinNewGuild()
    {
        long quit = 1000L;
        Assert.False(OfficialZenithPinnacle100DetailsEngine.CanJoinNewGuild(quit, quit + 12L * 3600 * 1000L));
        Assert.True(OfficialZenithPinnacle100DetailsEngine.CanJoinNewGuild(quit, quit + 25L * 3600 * 1000L));
    }

    [Fact]
    public void TestDetail1408_ChargeAbdicateFee()
    {
        var (ok1, rem1) = OfficialZenithPinnacle100DetailsEngine.ChargeAbdicateFee(600000);
        Assert.True(ok1);
        Assert.Equal(100000, rem1);

        var (ok2, rem2) = OfficialZenithPinnacle100DetailsEngine.ChargeAbdicateFee(400000);
        Assert.False(ok2);
        Assert.Equal(400000, rem2);
    }

    [Fact]
    public void TestDetail1409_CalculateSabukTaxRate()
    {
        Assert.Equal(20000, OfficialZenithPinnacle100DetailsEngine.CalculateSabukTaxRate(100000));
    }

    [Fact]
    public void TestDetail1410_ValidateTreasurePavilionPass()
    {
        Assert.True(OfficialZenithPinnacle100DetailsEngine.ValidateTreasurePavilionPass(true, false));
        Assert.True(OfficialZenithPinnacle100DetailsEngine.ValidateTreasurePavilionPass(false, true));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.ValidateTreasurePavilionPass(false, false));
    }

    // =========================================================================
    // 细节 1411 ~ 1425: 状态机判定、中毒防御剥离、药水并发缓冲与特戒
    // =========================================================================

    [Fact]
    public void TestDetail1411_FreezeNaturalRegenUnderGreenPoison()
    {
        Assert.Equal(0, OfficialZenithPinnacle100DetailsEngine.FreezeNaturalRegenUnderGreenPoison(10, true));
        Assert.Equal(10, OfficialZenithPinnacle100DetailsEngine.FreezeNaturalRegenUnderGreenPoison(10, false));
    }

    [Fact]
    public void TestDetail1412_ApplyRedPoisonAcReduction()
    {
        Assert.Equal(16, OfficialZenithPinnacle100DetailsEngine.ApplyRedPoisonAcReduction(20));
    }

    [Fact]
    public void TestDetail1413_ApplyRedPoisonMacReduction()
    {
        Assert.Equal(24, OfficialZenithPinnacle100DetailsEngine.ApplyRedPoisonMacReduction(30));
    }

    [Fact]
    public void TestDetail1414_CanCoexistDoublePoison()
    {
        Assert.True(OfficialZenithPinnacle100DetailsEngine.CanCoexistDoublePoison(true, true));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.CanCoexistDoublePoison(true, false));
    }

    [Fact]
    public void TestDetail1415_ValidateAmuletEquipped()
    {
        Assert.True(OfficialZenithPinnacle100DetailsEngine.ValidateAmuletEquipped(true, 100));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.ValidateAmuletEquipped(true, 0));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.ValidateAmuletEquipped(false, 100));
    }

    [Fact]
    public void TestDetail1416_ProcessPotionBufferTick()
    {
        var (p1, out1) = OfficialZenithPinnacle100DetailsEngine.ProcessPotionBufferTick(10, 20);
        Assert.Equal(25, p1);
        Assert.Equal(5, out1);

        var (p2, out2) = OfficialZenithPinnacle100DetailsEngine.ProcessPotionBufferTick(2, 0);
        Assert.Equal(0, p2);
        Assert.Equal(2, out2);
    }

    [Fact]
    public void TestDetail1417_GetSunWaterInstantRestore()
    {
        var (hp, mp) = OfficialZenithPinnacle100DetailsEngine.GetSunWaterInstantRestore();
        Assert.Equal(50, hp);
        Assert.Equal(50, mp);
    }

    [Fact]
    public void TestDetail1418_GetSnowFrostInstantRestore()
    {
        var (hp, mp) = OfficialZenithPinnacle100DetailsEngine.GetSnowFrostInstantRestore();
        Assert.Equal(120, hp);
        Assert.Equal(120, mp);
    }

    [Fact]
    public void TestDetail1419_GetHealingPotionInstantRestore()
    {
        var (hp, mp) = OfficialZenithPinnacle100DetailsEngine.GetHealingPotionInstantRestore();
        Assert.Equal(250, hp);
        Assert.Equal(250, mp);
    }

    [Fact]
    public void TestDetail1420_RollParalysisRing()
    {
        var rng = new Random(15);
        bool p = OfficialZenithPinnacle100DetailsEngine.RollParalysisRing(rng);
        Assert.False(p);
    }

    [Fact]
    public void TestDetail1421_TriggerReviveRing()
    {
        long now = 1000000L;
        var (res1, hp1, dura1, next1) = OfficialZenithPinnacle100DetailsEngine.TriggerReviveRing(400, 5, 0, now);
        Assert.True(res1);
        Assert.Equal(400, hp1);
        Assert.Equal(4, dura1);
        Assert.Equal(now, next1);

        // 冷却期中
        var (res2, hp2, dura2, next2) = OfficialZenithPinnacle100DetailsEngine.TriggerReviveRing(400, 4, now, now + 60000L);
        Assert.False(res2);
    }

    [Fact]
    public void TestDetail1422_ApplyProtectRingDmg()
    {
        var (abs1, rem1, newMp1) = OfficialZenithPinnacle100DetailsEngine.ApplyProtectRingDmg(60, 100);
        Assert.Equal(60, abs1);
        Assert.Equal(0, rem1);
        Assert.Equal(40, newMp1);

        var (abs2, rem2, newMp2) = OfficialZenithPinnacle100DetailsEngine.ApplyProtectRingDmg(60, 20);
        Assert.Equal(20, abs2);
        Assert.Equal(40, rem2);
        Assert.Equal(0, newMp2);
    }

    [Fact]
    public void TestDetail1423_GrantDefenseRingHealSkill()
    {
        Assert.True(OfficialZenithPinnacle100DetailsEngine.GrantDefenseRingHealSkill());
    }

    [Fact]
    public void TestDetail1424_GrantFlameRingFireballSkill()
    {
        Assert.True(OfficialZenithPinnacle100DetailsEngine.GrantFlameRingFireballSkill());
    }

    [Fact]
    public void TestDetail1425_DoesInvisibilityRingHoldDuringMove()
    {
        Assert.True(OfficialZenithPinnacle100DetailsEngine.DoesInvisibilityRingHoldDuringMove());
    }

    // =========================================================================
    // 细节 1426 ~ 1440: 传送卷轴、超重慢走、防爆装与割肉纯度
    // =========================================================================

    [Fact]
    public void TestDetail1426_InterruptTownTeleportOnHit()
    {
        Assert.True(OfficialZenithPinnacle100DetailsEngine.InterruptTownTeleportOnHit(true));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.InterruptTownTeleportOnHit(false));
    }

    [Fact]
    public void TestDetail1427_ResolveRedNameTownScroll()
    {
        var (x1, y1, map1) = OfficialZenithPinnacle100DetailsEngine.ResolveRedNameTownScroll(true);
        Assert.Equal(850, x1);
        Assert.Equal(670, y1);
        Assert.Equal("3", map1);

        var (x2, y2, map2) = OfficialZenithPinnacle100DetailsEngine.ResolveRedNameTownScroll(false);
        Assert.Equal(330, x2);
        Assert.Equal("0", map2);
    }

    [Fact]
    public void TestDetail1428_ResolveDungeonSurfaceCity()
    {
        Assert.Equal("11", OfficialZenithPinnacle100DetailsEngine.ResolveDungeonSurfaceCity("D401"));
        Assert.Equal("3", OfficialZenithPinnacle100DetailsEngine.ResolveDungeonSurfaceCity("D2001"));
        Assert.Equal("0", OfficialZenithPinnacle100DetailsEngine.ResolveDungeonSurfaceCity("D1001"));
    }

    [Fact]
    public void TestDetail1429_IsFlightForbiddenMap()
    {
        Assert.True(OfficialZenithPinnacle100DetailsEngine.IsFlightForbiddenMap("0150"));
        Assert.True(OfficialZenithPinnacle100DetailsEngine.IsFlightForbiddenMap("D2008"));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.IsFlightForbiddenMap("0"));
    }

    [Fact]
    public void TestDetail1430_CalculateMaxBagWeightFormula()
    {
        int w = OfficialZenithPinnacle100DetailsEngine.CalculateMaxBagWeightFormula(30, 10);
        Assert.Equal(180, w); // 50 + 30*4 + 10
    }

    [Fact]
    public void TestDetail1431_CanPickItemByWeight()
    {
        Assert.True(OfficialZenithPinnacle100DetailsEngine.CanPickItemByWeight(100, 100));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.CanPickItemByWeight(101, 100));
    }

    [Fact]
    public void TestDetail1432_CanRunByWeight()
    {
        Assert.True(OfficialZenithPinnacle100DetailsEngine.CanRunByWeight(105, 100));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.CanRunByWeight(106, 100));
    }

    [Fact]
    public void TestDetail1433_CheckWeaponArmStrength()
    {
        Assert.True(OfficialZenithPinnacle100DetailsEngine.CheckWeaponArmStrength(30, 35));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.CheckWeaponArmStrength(40, 35));
    }

    [Fact]
    public void TestDetail1434_CheckArmorBodyWeight()
    {
        Assert.True(OfficialZenithPinnacle100DetailsEngine.CheckArmorBodyWeight(60, 62));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.CheckArmorBodyWeight(65, 62));
    }

    [Fact]
    public void TestDetail1435_CheckHelmetHeadWeight()
    {
        Assert.True(OfficialZenithPinnacle100DetailsEngine.CheckHelmetHeadWeight(15, 20));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.CheckHelmetHeadWeight(25, 20));
    }

    [Fact]
    public void TestDetail1436_ApplyRareWeightOffset()
    {
        Assert.Equal(19, OfficialZenithPinnacle100DetailsEngine.ApplyRareWeightOffset(20, true));
        Assert.Equal(20, OfficialZenithPinnacle100DetailsEngine.ApplyRareWeightOffset(20, false));
    }

    [Fact]
    public void TestDetail1437_GetAttributesByDurability()
    {
        var (dc1, ac1, mac1) = OfficialZenithPinnacle100DetailsEngine.GetAttributesByDurability(10, 30, 10, 5);
        Assert.Equal(30, dc1);

        var (dc2, ac2, mac2) = OfficialZenithPinnacle100DetailsEngine.GetAttributesByDurability(0, 30, 10, 5);
        Assert.Equal(0, dc2);
    }

    [Fact]
    public void TestDetail1438_CalculateCritDurabilityLoss()
    {
        Assert.Equal(2, OfficialZenithPinnacle100DetailsEngine.CalculateCritDurabilityLoss(true));
        Assert.Equal(1, OfficialZenithPinnacle100DetailsEngine.CalculateCritDurabilityLoss(false));
    }

    [Fact]
    public void TestDetail1439_GetDeathDropRateByPkStatus()
    {
        Assert.Equal(100, OfficialZenithPinnacle100DetailsEngine.GetDeathDropRateByPkStatus(2));
        Assert.Equal(30, OfficialZenithPinnacle100DetailsEngine.GetDeathDropRateByPkStatus(1));
        Assert.Equal(10, OfficialZenithPinnacle100DetailsEngine.GetDeathDropRateByPkStatus(0));
    }

    [Fact]
    public void TestDetail1440_CalculateMeatDecayQuality()
    {
        int q = OfficialZenithPinnacle100DetailsEngine.CalculateMeatDecayQuality(16, 2);
        Assert.Equal(12, q);
    }

    // =========================================================================
    // 细节 1441 ~ 1450: 仓库安全、时间戳单调、反外挂与Direct-Blit 协议完整性
    // =========================================================================

    [Fact]
    public void TestDetail1441_EvaluateStorageLock()
    {
        long now = 1000L;
        var (locked1, tick1) = OfficialZenithPinnacle100DetailsEngine.EvaluateStorageLock(3, now);
        Assert.True(locked1);
        Assert.Equal(now + 600000L, tick1);

        var (locked2, _) = OfficialZenithPinnacle100DetailsEngine.EvaluateStorageLock(2, now);
        Assert.False(locked2);
    }

    [Fact]
    public void TestDetail1442_GetGoldCapLimits()
    {
        var (bag, storage) = OfficialZenithPinnacle100DetailsEngine.GetGoldCapLimits();
        Assert.Equal(10000000, bag);
        Assert.Equal(50000000, storage);
    }

    [Fact]
    public void TestDetail1443_PreventGoldWrapAround()
    {
        Assert.True(OfficialZenithPinnacle100DetailsEngine.PreventGoldWrapAround(5000000, 2000000));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.PreventGoldWrapAround(9000000, 2000000));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.PreventGoldWrapAround(1000, -2000));
    }

    [Fact]
    public void TestDetail1444_IsBuybackListExpired()
    {
        long sold = 1000L;
        Assert.False(OfficialZenithPinnacle100DetailsEngine.IsBuybackListExpired(sold, sold + 30 * 60 * 1000L));
        Assert.True(OfficialZenithPinnacle100DetailsEngine.IsBuybackListExpired(sold, sold + 61 * 60 * 1000L));
    }

    [Fact]
    public void TestDetail1445_GetChickenMeatQuality()
    {
        Assert.Equal(5, OfficialZenithPinnacle100DetailsEngine.GetChickenMeatQuality(5));
        Assert.Equal(3, OfficialZenithPinnacle100DetailsEngine.GetChickenMeatQuality(1));
        Assert.Equal(10, OfficialZenithPinnacle100DetailsEngine.GetChickenMeatQuality(20));
    }

    [Fact]
    public void TestDetail1446_IsHighQualityDeerMeatQuestEligible()
    {
        Assert.True(OfficialZenithPinnacle100DetailsEngine.IsHighQualityDeerMeatQuestEligible(15));
        Assert.True(OfficialZenithPinnacle100DetailsEngine.IsHighQualityDeerMeatQuestEligible(18));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.IsHighQualityDeerMeatQuestEligible(14));
    }

    [Fact]
    public void TestDetail1447_CalculateSpecialRepairGold()
    {
        int cost = OfficialZenithPinnacle100DetailsEngine.CalculateSpecialRepairGold(1000, 10, 20);
        Assert.Equal(1500, cost); // 1000 * 10 * 3 / 20 = 1500

        Assert.Equal(0, OfficialZenithPinnacle100DetailsEngine.CalculateSpecialRepairGold(1000, 20, 20));
    }

    [Fact]
    public void TestDetail1448_CheckTimestampMonotonicity()
    {
        Assert.True(OfficialZenithPinnacle100DetailsEngine.CheckTimestampMonotonicity(100, 105));
        Assert.True(OfficialZenithPinnacle100DetailsEngine.CheckTimestampMonotonicity(100, 100));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.CheckTimestampMonotonicity(100, 95));
    }

    [Fact]
    public void TestDetail1449_VerifyDirectBlitMagic()
    {
        Assert.True(OfficialZenithPinnacle100DetailsEngine.VerifyDirectBlitMagic(0x594D));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.VerifyDirectBlitMagic(0x1234));
    }

    [Fact]
    public void TestDetail1450_VerifyDirectBlitSequence()
    {
        Assert.True(OfficialZenithPinnacle100DetailsEngine.VerifyDirectBlitSequence(100, 101));
        Assert.False(OfficialZenithPinnacle100DetailsEngine.VerifyDirectBlitSequence(101, 100));
        Assert.True(OfficialZenithPinnacle100DetailsEngine.VerifyDirectBlitSequence(65530, 50)); // 回绕放行
    }
}
