using System;
using System.Collections.Generic;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 盛大官方 1.5 不朽传奇 100 微观细节与服务端权威闭环测试矩阵 (Wave 93, Details 1451 ~ 1550)
/// 严格 1:1 覆盖封魔殿虹魔教主生态、怪物反隐、药水缓释池、垫刀随机熵、沙城复活与安全守门线
/// </summary>
public class TestOfficialWave93_100DetailsLegendMatrix
{
    // =========================================================================
    // 细节 1451 ~ 1465: 封魔殿虹魔教主深度生态、护卫 AI 与魔血/祈祷套装掉落
    // =========================================================================

    [Fact]
    public void TestDetail1451_IsFengMoHallSafeZone()
    {
        Assert.False(OfficialImmortalLegend100DetailsEngine.IsFengMoHallSafeZone("D024", 50, 50));
    }

    [Fact]
    public void TestDetail1452_CalculateHongMoKingDamage()
    {
        int norm = OfficialImmortalLegend100DetailsEngine.CalculateHongMoKingDamage(100, 500, 1000);
        int rage = OfficialImmortalLegend100DetailsEngine.CalculateHongMoKingDamage(100, 150, 1000);
        Assert.Equal(100, norm);
        Assert.Equal(180, rage);
    }

    [Fact]
    public void TestDetail1453_CalculateHongMoPigGuardKnockback()
    {
        var (kb, nx, ny) = OfficialImmortalLegend100DetailsEngine.CalculateHongMoPigGuardKnockback(10, 10, 11, 10);
        Assert.True(kb);
        Assert.Equal(12, nx);
        Assert.Equal(10, ny);

        var (kb2, nx2, ny2) = OfficialImmortalLegend100DetailsEngine.CalculateHongMoPigGuardKnockback(10, 10, 20, 20);
        Assert.False(kb2);
    }

    [Fact]
    public void TestDetail1454_CalculateHongMoScorpionPoisonDamage()
    {
        int dmg = OfficialImmortalLegend100DetailsEngine.CalculateHongMoScorpionPoisonDamage(60, 20);
        Assert.Equal(44, dmg); // 60 - 20*0.8 = 44
    }

    [Fact]
    public void TestDetail1455_CanHongMoPigGuardAttack()
    {
        long last = 1000L;
        Assert.False(OfficialImmortalLegend100DetailsEngine.CanHongMoPigGuardAttack(last, last + 500L));
        Assert.True(OfficialImmortalLegend100DetailsEngine.CanHongMoPigGuardAttack(last, last + 650L));
    }

    [Fact]
    public void TestDetail1456_CalculateHongMoScorpionStun()
    {
        var rng = new Random(42);
        var (stunned, tick) = OfficialImmortalLegend100DetailsEngine.CalculateHongMoScorpionStun(1000L, rng);
        Assert.True(tick >= 1000L);
    }

    [Fact]
    public void TestDetail1457_IsHongMoKingUltraRareDrop()
    {
        Assert.True(OfficialImmortalLegend100DetailsEngine.IsHongMoKingUltraRareDrop("虹魔戒指"));
        Assert.True(OfficialImmortalLegend100DetailsEngine.IsHongMoKingUltraRareDrop("魔血手镯"));
        Assert.True(OfficialImmortalLegend100DetailsEngine.IsHongMoKingUltraRareDrop("祈祷之刃"));
        Assert.False(OfficialImmortalLegend100DetailsEngine.IsHongMoKingUltraRareDrop("雷霆怒斩"));
    }

    [Fact]
    public void TestDetail1458_IsFengMoMineNarrowPassage()
    {
        Assert.True(OfficialImmortalLegend100DetailsEngine.IsFengMoMineNarrowPassage(23, 28));
        Assert.False(OfficialImmortalLegend100DetailsEngine.IsFengMoMineNarrowPassage(20, 20));
    }

    [Fact]
    public void TestDetail1459_IsFengMoSanctumGroupRecallForbidden()
    {
        Assert.True(OfficialImmortalLegend100DetailsEngine.IsFengMoSanctumGroupRecallForbidden("D024"));
        Assert.False(OfficialImmortalLegend100DetailsEngine.IsFengMoSanctumGroupRecallForbidden("0"));
    }

    [Fact]
    public void TestDetail1460_ChargeFengMoMineTeleportFee()
    {
        var (ok1, rem1) = OfficialImmortalLegend100DetailsEngine.ChargeFengMoMineTeleportFee(3000);
        Assert.True(ok1);
        Assert.Equal(1000, rem1);

        var (ok2, rem2) = OfficialImmortalLegend100DetailsEngine.ChargeFengMoMineTeleportFee(1500);
        Assert.False(ok2);
        Assert.Equal(1500, rem2);
    }

    [Fact]
    public void TestDetail1461_CanTameHongMoMonster()
    {
        Assert.False(OfficialImmortalLegend100DetailsEngine.CanTameHongMoMonster("虹魔猪卫"));
        Assert.True(OfficialImmortalLegend100DetailsEngine.CanTameHongMoMonster("黑色恶蛆"));
    }

    [Fact]
    public void TestDetail1462_IsMonsterFactionIndependent()
    {
        Assert.True(OfficialImmortalLegend100DetailsEngine.IsMonsterFactionIndependent("祖玛雕像", "虹魔猪卫"));
        Assert.False(OfficialImmortalLegend100DetailsEngine.IsMonsterFactionIndependent("祖玛雕像", "祖玛卫士"));
    }

    [Fact]
    public void TestDetail1463_CalculateScorpionRetreatCoords()
    {
        var (rx, ry) = OfficialImmortalLegend100DetailsEngine.CalculateScorpionRetreatCoords(10, 10, 8, 10);
        Assert.Equal(12, rx);
        Assert.Equal(10, ry);
    }

    [Fact]
    public void TestDetail1464_GetHongMoKingSpawnShakeDuration()
    {
        Assert.Equal(300, OfficialImmortalLegend100DetailsEngine.GetHongMoKingSpawnShakeDuration());
    }

    [Fact]
    public void TestDetail1465_IsLootInWaterBlocked()
    {
        Assert.True(OfficialImmortalLegend100DetailsEngine.IsLootInWaterBlocked(55, 55));
        Assert.False(OfficialImmortalLegend100DetailsEngine.IsLootInWaterBlocked(10, 10));
    }

    // =========================================================================
    // 细节 1466 ~ 1480: 怪物视野反隐、识破与假死反击机制
    // =========================================================================

    [Fact]
    public void TestDetail1466_CanMoonSpiderDetectInvisible()
    {
        Assert.True(OfficialImmortalLegend100DetailsEngine.CanMoonSpiderDetectInvisible(100, 100, 105, 105));
        Assert.False(OfficialImmortalLegend100DetailsEngine.CanMoonSpiderDetectInvisible(100, 100, 110, 110));
    }

    [Fact]
    public void TestDetail1467_CanMothDetectInvisible()
    {
        Assert.True(OfficialImmortalLegend100DetailsEngine.CanMothDetectInvisible(100, 100, 104, 104));
        Assert.False(OfficialImmortalLegend100DetailsEngine.CanMothDetectInvisible(100, 100, 108, 108));
    }

    [Fact]
    public void TestDetail1468_ShouldDragonEmerge()
    {
        Assert.True(OfficialImmortalLegend100DetailsEngine.ShouldDragonEmerge(50, 50, 51, 51));
        Assert.False(OfficialImmortalLegend100DetailsEngine.ShouldDragonEmerge(50, 50, 55, 55));
    }

    [Fact]
    public void TestDetail1469_IsZombieKingInvulnerable()
    {
        long start = 1000L;
        Assert.True(OfficialImmortalLegend100DetailsEngine.IsZombieKingInvulnerable(start, start + 500L));
        Assert.False(OfficialImmortalLegend100DetailsEngine.IsZombieKingInvulnerable(start, start + 900L));
    }

    [Fact]
    public void TestDetail1470_ShouldWomaLeaderTeleportOut()
    {
        Assert.True(OfficialImmortalLegend100DetailsEngine.ShouldWomaLeaderTeleportOut(3));
        Assert.False(OfficialImmortalLegend100DetailsEngine.ShouldWomaLeaderTeleportOut(2));
    }

    [Fact]
    public void TestDetail1471_IsZumaLeaderStoneImmune()
    {
        long unfreeze = 1000L;
        Assert.True(OfficialImmortalLegend100DetailsEngine.IsZumaLeaderStoneImmune(unfreeze, unfreeze + 800L));
        Assert.False(OfficialImmortalLegend100DetailsEngine.IsZumaLeaderStoneImmune(unfreeze, unfreeze + 1200L));
    }

    [Fact]
    public void TestDetail1472_ApplyCentipedeShellDefense()
    {
        int def1 = OfficialImmortalLegend100DetailsEngine.ApplyCentipedeShellDefense(20, 100, 1000);
        int def2 = OfficialImmortalLegend100DetailsEngine.ApplyCentipedeShellDefense(20, 500, 1000);
        Assert.Equal(30, def1); // 20 * 1.5 = 30
        Assert.Equal(20, def2);
    }

    [Fact]
    public void TestDetail1473_GetBlackMaggotStepIntervalMs()
    {
        Assert.Equal(200, OfficialImmortalLegend100DetailsEngine.GetBlackMaggotStepIntervalMs());
    }

    [Fact]
    public void TestDetail1474_IsWithinSharedAggroRange()
    {
        Assert.True(OfficialImmortalLegend100DetailsEngine.IsWithinSharedAggroRange(10, 10, 13, 14));
        Assert.False(OfficialImmortalLegend100DetailsEngine.IsWithinSharedAggroRange(10, 10, 18, 10));
    }

    [Fact]
    public void TestDetail1475_AdjustInvisibilityDurationNearBoss()
    {
        Assert.Equal(15, OfficialImmortalLegend100DetailsEngine.AdjustInvisibilityDurationNearBoss(30, true));
        Assert.Equal(30, OfficialImmortalLegend100DetailsEngine.AdjustInvisibilityDurationNearBoss(30, false));
    }

    [Fact]
    public void TestDetail1476_CanTrapDarkBoss()
    {
        Assert.False(OfficialImmortalLegend100DetailsEngine.CanTrapDarkBoss("暗之双头金刚"));
        Assert.True(OfficialImmortalLegend100DetailsEngine.CanTrapDarkBoss("祖玛弓箭手"));
    }

    [Fact]
    public void TestDetail1477_CalculateHolyWordChanceAgainstUndead()
    {
        int c1 = OfficialImmortalLegend100DetailsEngine.CalculateHolyWordChanceAgainstUndead(40, 35);
        Assert.Equal(20, c1); // (40-35)*4 = 20

        int c2 = OfficialImmortalLegend100DetailsEngine.CalculateHolyWordChanceAgainstUndead(30, 35);
        Assert.Equal(0, c2);
    }

    [Fact]
    public void TestDetail1478_CanZombieFeignDeathRevive()
    {
        long death = 1000L;
        Assert.False(OfficialImmortalLegend100DetailsEngine.CanZombieFeignDeathRevive(death, death + 8000L));
        Assert.True(OfficialImmortalLegend100DetailsEngine.CanZombieFeignDeathRevive(death, death + 11000L));
    }

    [Fact]
    public void TestDetail1479_DoesAttackBreakTameInduction()
    {
        Assert.True(OfficialImmortalLegend100DetailsEngine.DoesAttackBreakTameInduction(true));
        Assert.False(OfficialImmortalLegend100DetailsEngine.DoesAttackBreakTameInduction(false));
    }

    [Fact]
    public void TestDetail1480_RollHuWeiRoarStun()
    {
        var rng = new Random(10);
        var (feared, ms) = OfficialImmortalLegend100DetailsEngine.RollHuWeiRoarStun(7, rng);
        if (feared) Assert.Equal(200, ms);
    }

    // =========================================================================
    // 细节 1481 ~ 1495: 全量药水缓释池、中毒叠加与解毒药剂精算
    // =========================================================================

    [Fact]
    public void TestDetail1481_GetSmallHpPotionSpecs()
    {
        var (amt, ticks) = OfficialImmortalLegend100DetailsEngine.GetSmallHpPotionSpecs();
        Assert.Equal(3, amt);
        Assert.Equal(10, ticks);
    }

    [Fact]
    public void TestDetail1482_GetMediumHpPotionSpecs()
    {
        var (amt, ticks) = OfficialImmortalLegend100DetailsEngine.GetMediumHpPotionSpecs();
        Assert.Equal(6, amt);
        Assert.Equal(10, ticks);
    }

    [Fact]
    public void TestDetail1483_GetStrongHpPotionSpecs()
    {
        var (amt, ticks) = OfficialImmortalLegend100DetailsEngine.GetStrongHpPotionSpecs();
        Assert.Equal(10, amt);
        Assert.Equal(10, ticks);
    }

    [Fact]
    public void TestDetail1484_GetSmallMpPotionSpecs()
    {
        var (amt, ticks) = OfficialImmortalLegend100DetailsEngine.GetSmallMpPotionSpecs();
        Assert.Equal(4, amt);
        Assert.Equal(10, ticks);
    }

    [Fact]
    public void TestDetail1485_GetMediumMpPotionSpecs()
    {
        var (amt, ticks) = OfficialImmortalLegend100DetailsEngine.GetMediumMpPotionSpecs();
        Assert.Equal(8, amt);
        Assert.Equal(10, ticks);
    }

    [Fact]
    public void TestDetail1486_GetStrongMpPotionSpecs()
    {
        var (amt, ticks) = OfficialImmortalLegend100DetailsEngine.GetStrongMpPotionSpecs();
        Assert.Equal(15, amt);
        Assert.Equal(10, ticks);
    }

    [Fact]
    public void TestDetail1487_GetSunPotionRestore()
    {
        var (hp, mp) = OfficialImmortalLegend100DetailsEngine.GetSunPotionRestore();
        Assert.Equal(50, hp);
        Assert.Equal(50, mp);
    }

    [Fact]
    public void TestDetail1488_GetStrongSunPotionRestore()
    {
        var (hp, mp) = OfficialImmortalLegend100DetailsEngine.GetStrongSunPotionRestore();
        Assert.Equal(80, hp);
        Assert.Equal(80, mp);
    }

    [Fact]
    public void TestDetail1489_GetSnowFrostRestore()
    {
        var (hp, mp) = OfficialImmortalLegend100DetailsEngine.GetSnowFrostRestore();
        Assert.Equal(120, hp);
        Assert.Equal(120, mp);
    }

    [Fact]
    public void TestDetail1490_GetHealingDrugRestore()
    {
        var (hp, mp) = OfficialImmortalLegend100DetailsEngine.GetHealingDrugRestore();
        Assert.Equal(250, hp);
        Assert.Equal(250, mp);
    }

    [Fact]
    public void TestDetail1491_CalculateGreenPoisonDamageCapped()
    {
        int d1 = OfficialImmortalLegend100DetailsEngine.CalculateGreenPoisonDamageCapped(50, 3);
        Assert.Equal(9, d1); // 1 + 3 + 5 = 9

        int d2 = OfficialImmortalLegend100DetailsEngine.CalculateGreenPoisonDamageCapped(200, 3);
        Assert.Equal(15, d2); // 截断到 15
    }

    [Fact]
    public void TestDetail1492_GetRedPoisonBaseDurationSec()
    {
        Assert.Equal(30, OfficialImmortalLegend100DetailsEngine.GetRedPoisonBaseDurationSec());
    }

    [Fact]
    public void TestDetail1493_CanDrinkPotionByPoolLimit()
    {
        Assert.True(OfficialImmortalLegend100DetailsEngine.CanDrinkPotionByPoolLimit(800, 100));
        Assert.False(OfficialImmortalLegend100DetailsEngine.CanDrinkPotionByPoolLimit(950, 100));
    }

    [Fact]
    public void TestDetail1494_CureAllPoisons()
    {
        var (g, r) = OfficialImmortalLegend100DetailsEngine.CureAllPoisons();
        Assert.False(g);
        Assert.False(r);
    }

    [Fact]
    public void TestDetail1495_CalculatePetrifyChanceWithAvoidance()
    {
        int r = OfficialImmortalLegend100DetailsEngine.CalculatePetrifyChanceWithAvoidance(20, 50);
        Assert.Equal(10, r); // 20 * (100-50)/100 = 10
    }

    // =========================================================================
    // 细节 1496 ~ 1510: 武器升级垫刀随机熵、诅咒反噬与祈祷全套天罚
    // =========================================================================

    [Fact]
    public void TestDetail1496_CalculateUpgradeSeedOffset()
    {
        Assert.Equal(111, OfficialImmortalLegend100DetailsEngine.CalculateUpgradeSeedOffset(3));
    }

    [Fact]
    public void TestDetail1497_DeriveUpgradeOutcomeSeed()
    {
        int seed = OfficialImmortalLegend100DetailsEngine.DeriveUpgradeOutcomeSeed(123456789L, 1001);
        Assert.True(seed >= 0);
    }

    [Fact]
    public void TestDetail1498_IsBlackIronBalanced()
    {
        Assert.True(OfficialImmortalLegend100DetailsEngine.IsBlackIronBalanced(new[] { 15, 16, 17 }));
        Assert.False(OfficialImmortalLegend100DetailsEngine.IsBlackIronBalanced(new[] { 5, 20, 25 }));
    }

    [Fact]
    public void TestDetail1499_DoesJewelryDurabilityAffectUpgrade()
    {
        Assert.False(OfficialImmortalLegend100DetailsEngine.DoesJewelryDurabilityAffectUpgrade(5));
        Assert.False(OfficialImmortalLegend100DetailsEngine.DoesJewelryDurabilityAffectUpgrade(1));
    }

    [Fact]
    public void TestDetail1500_CalculateJewelryLevelScore()
    {
        Assert.Equal(90, OfficialImmortalLegend100DetailsEngine.CalculateJewelryLevelScore(30));
    }

    [Fact]
    public void TestDetail1501_GetWeaponBreakEffectId()
    {
        Assert.Equal(30, OfficialImmortalLegend100DetailsEngine.GetWeaponBreakEffectId());
    }

    [Fact]
    public void TestDetail1502_GetPraySetRebellionDurationMs()
    {
        Assert.Equal(1800000L, OfficialImmortalLegend100DetailsEngine.GetPraySetRebellionDurationMs());
    }

    [Fact]
    public void TestDetail1503_ShouldPrayNecklaceBreakOnDeath()
    {
        Assert.True(OfficialImmortalLegend100DetailsEngine.ShouldPrayNecklaceBreakOnDeath());
    }

    [Fact]
    public void TestDetail1504_RollPrayBraceletBreak()
    {
        var rng = new Random(42);
        bool b = OfficialImmortalLegend100DetailsEngine.RollPrayBraceletBreak(rng);
        Assert.False(b); // 确定性种子
    }

    [Fact]
    public void TestDetail1505_GetPrayBladeUniqueStats()
    {
        var (hit, spd) = OfficialImmortalLegend100DetailsEngine.GetPrayBladeUniqueStats();
        Assert.Equal(3, hit);
        Assert.Equal(-1, spd);
    }

    [Fact]
    public void TestDetail1506_GetBlessingOilRateForLuck6()
    {
        Assert.Equal(3, OfficialImmortalLegend100DetailsEngine.GetBlessingOilRateForLuck6());
    }

    [Fact]
    public void TestDetail1507_ApplyBlessingOilWithCurse()
    {
        var (l1, c1, _) = OfficialImmortalLegend100DetailsEngine.ApplyBlessingOilWithCurse(0, 2);
        Assert.Equal(0, l1);
        Assert.Equal(1, c1); // 优先消诅咒

        var (l2, c2, _) = OfficialImmortalLegend100DetailsEngine.ApplyBlessingOilWithCurse(2, 0);
        Assert.Equal(3, l2);
        Assert.Equal(0, c2); // 无诅咒加幸运
    }

    [Fact]
    public void TestDetail1508_RollPkWantedCurse()
    {
        var rng = new Random(100);
        int hits = 0;
        for (int i = 0; i < 100; i++)
        {
            if (OfficialImmortalLegend100DetailsEngine.RollPkWantedCurse(rng)) hits++;
        }
        Assert.InRange(hits, 25, 55); // 理论期望 40 次，允许正常统计波动
    }

    [Fact]
    public void TestDetail1509_GetLuoShaGuaranteedDamage()
    {
        Assert.Equal(15, OfficialImmortalLegend100DetailsEngine.GetLuoShaGuaranteedDamage(10));
        Assert.Equal(0, OfficialImmortalLegend100DetailsEngine.GetLuoShaGuaranteedDamage(5));
    }

    [Fact]
    public void TestDetail1510_CalculateWarGodOilRepair()
    {
        var (cost, loss) = OfficialImmortalLegend100DetailsEngine.CalculateWarGodOilRepair(1000);
        Assert.Equal(5000, cost);
        Assert.Equal(0, loss); // 0 损耗
    }

    // =========================================================================
    // 细节 1511 ~ 1525: 沙巴克攻城复活点争夺、密道战术与全服税收清算
    // =========================================================================

    [Fact]
    public void TestDetail1511_IsFlagCaptured()
    {
        Assert.True(OfficialImmortalLegend100DetailsEngine.IsFlagCaptured(60));
        Assert.False(OfficialImmortalLegend100DetailsEngine.IsFlagCaptured(59));
    }

    [Fact]
    public void TestDetail1512_GetDefenderSpawnLocation()
    {
        var (x, y) = OfficialImmortalLegend100DetailsEngine.GetDefenderSpawnLocation();
        Assert.Equal(640, x);
        Assert.Equal(290, y);
    }

    [Fact]
    public void TestDetail1513_GetAttackerSpawnLocation()
    {
        var (x, y) = OfficialImmortalLegend100DetailsEngine.GetAttackerSpawnLocation();
        Assert.Equal(700, x);
        Assert.Equal(350, y);
    }

    [Fact]
    public void TestDetail1514_CanQuerySabukWarList()
    {
        Assert.True(OfficialImmortalLegend100DetailsEngine.CanQuerySabukWarList(true));
        Assert.False(OfficialImmortalLegend100DetailsEngine.CanQuerySabukWarList(false));
    }

    [Fact]
    public void TestDetail1515_IsGateCollisionActive()
    {
        Assert.True(OfficialImmortalLegend100DetailsEngine.IsGateCollisionActive(100));
        Assert.False(OfficialImmortalLegend100DetailsEngine.IsGateCollisionActive(0));
    }

    [Fact]
    public void TestDetail1516_ValidatePalaceSecretPassageDirection()
    {
        Assert.True(OfficialImmortalLegend100DetailsEngine.ValidatePalaceSecretPassageDirection("SecretPassage", "Palace"));
        Assert.False(OfficialImmortalLegend100DetailsEngine.ValidatePalaceSecretPassageDirection("Palace", "SecretPassage"));
    }

    [Fact]
    public void TestDetail1517_IsSabukNpcTradeAllowedDuringWar()
    {
        Assert.False(OfficialImmortalLegend100DetailsEngine.IsSabukNpcTradeAllowedDuringWar(true));
        Assert.True(OfficialImmortalLegend100DetailsEngine.IsSabukNpcTradeAllowedDuringWar(false));
    }

    [Fact]
    public void TestDetail1518_DecideSabukWinnerByHeadcount()
    {
        string w1 = OfficialImmortalLegend100DetailsEngine.DecideSabukWinnerByHeadcount(20, 10, "进攻会", "防守会");
        Assert.Equal("进攻会", w1);

        string w2 = OfficialImmortalLegend100DetailsEngine.DecideSabukWinnerByHeadcount(10, 10, "进攻会", "防守会");
        Assert.Equal("防守会", w2);
    }

    [Fact]
    public void TestDetail1519_GetDailyLordSalary()
    {
        Assert.Equal(100000, OfficialImmortalLegend100DetailsEngine.GetDailyLordSalary());
    }

    [Fact]
    public void TestDetail1520_ApplySabukDiscount()
    {
        int p1 = OfficialImmortalLegend100DetailsEngine.ApplySabukDiscount(1000, true);
        int p2 = OfficialImmortalLegend100DetailsEngine.ApplySabukDiscount(1000, false);
        Assert.Equal(800, p1);
        Assert.Equal(1000, p2);
    }

    [Fact]
    public void TestDetail1521_TransferLordTitle()
    {
        bool ok = OfficialImmortalLegend100DetailsEngine.TransferLordTitle("新城主", out string assigned);
        Assert.True(ok);
        Assert.Equal("新城主", assigned);
    }

    [Fact]
    public void TestDetail1522_CanRevokeWarApplication()
    {
        Assert.False(OfficialImmortalLegend100DetailsEngine.CanRevokeWarApplication());
    }

    [Fact]
    public void TestDetail1523_IsFlightAllowedInPalace()
    {
        Assert.False(OfficialImmortalLegend100DetailsEngine.IsFlightAllowedInPalace("0150"));
        Assert.True(OfficialImmortalLegend100DetailsEngine.IsFlightAllowedInPalace("0"));
    }

    [Fact]
    public void TestDetail1524_ApplyTreasurePavilionDropMultiplier()
    {
        Assert.Equal(20, OfficialImmortalLegend100DetailsEngine.ApplyTreasurePavilionDropMultiplier(10));
    }

    [Fact]
    public void TestDetail1525_CalculateWarDropRate()
    {
        Assert.Equal(15, OfficialImmortalLegend100DetailsEngine.CalculateWarDropRate(30));
    }

    // =========================================================================
    // 细节 1526 ~ 1540: 行会战火、联盟同盟识别与敌对名牌染色
    // =========================================================================

    [Fact]
    public void TestDetail1526_FormatWarEnemyTitle()
    {
        Assert.Equal("[敌对] 战神殿", OfficialImmortalLegend100DetailsEngine.FormatWarEnemyTitle("战神殿"));
    }

    [Fact]
    public void TestDetail1527_FormatWarAllyTitle()
    {
        Assert.Equal("[盟友] 兄弟连", OfficialImmortalLegend100DetailsEngine.FormatWarAllyTitle("兄弟连"));
    }

    [Fact]
    public void TestDetail1528_IsFriendlyFireProtected()
    {
        Assert.True(OfficialImmortalLegend100DetailsEngine.IsFriendlyFireProtected(101, 101));
        Assert.False(OfficialImmortalLegend100DetailsEngine.IsFriendlyFireProtected(101, 102));
        Assert.False(OfficialImmortalLegend100DetailsEngine.IsFriendlyFireProtected(0, 0));
    }

    [Fact]
    public void TestDetail1529_ShouldBroadcastGuildNotice()
    {
        Assert.True(OfficialImmortalLegend100DetailsEngine.ShouldBroadcastGuildNotice("旧公告", "新公告"));
        Assert.False(OfficialImmortalLegend100DetailsEngine.ShouldBroadcastGuildNotice("相同公告", "相同公告"));
    }

    [Fact]
    public void TestDetail1530_CanKickMember()
    {
        Assert.True(OfficialImmortalLegend100DetailsEngine.CanKickMember(0, 1));
        Assert.False(OfficialImmortalLegend100DetailsEngine.CanKickMember(1, 2));
    }

    [Fact]
    public void TestDetail1531_CanRecruitTarget()
    {
        Assert.True(OfficialImmortalLegend100DetailsEngine.CanRecruitTarget(true));
        Assert.False(OfficialImmortalLegend100DetailsEngine.CanRecruitTarget(false));
    }

    [Fact]
    public void TestDetail1532_CalculateGuildPagination()
    {
        var (pages, total) = OfficialImmortalLegend100DetailsEngine.CalculateGuildPagination(25, 10);
        Assert.Equal(3, pages);
        Assert.Equal(25, total);
    }

    [Fact]
    public void TestDetail1533_KeepInventoryOnQuitGuild()
    {
        Assert.True(OfficialImmortalLegend100DetailsEngine.KeepInventoryOnQuitGuild());
    }

    [Fact]
    public void TestDetail1534_IsGuildInDecline()
    {
        Assert.True(OfficialImmortalLegend100DetailsEngine.IsGuildInDecline(0));
        Assert.False(OfficialImmortalLegend100DetailsEngine.IsGuildInDecline(5000));
    }

    [Fact]
    public void TestDetail1535_IsGuildWarExpired()
    {
        long start = 1000L;
        Assert.False(OfficialImmortalLegend100DetailsEngine.IsGuildWarExpired(start, start + 2L * 3600 * 1000L));
        Assert.True(OfficialImmortalLegend100DetailsEngine.IsGuildWarExpired(start, start + 4L * 3600 * 1000L));
    }

    [Fact]
    public void TestDetail1536_GenerateWarKillBroadcast()
    {
        string b = OfficialImmortalLegend100DetailsEngine.GenerateWarKillBroadcast("战狂", "弱法", "圣域");
        Assert.Contains("战狂", b);
        Assert.Contains("弱法", b);
        Assert.Contains("圣域", b);
    }

    [Fact]
    public void TestDetail1537_CanDeclareWarOnAlly()
    {
        Assert.False(OfficialImmortalLegend100DetailsEngine.CanDeclareWarOnAlly(true));
        Assert.True(OfficialImmortalLegend100DetailsEngine.CanDeclareWarOnAlly(false));
    }

    [Fact]
    public void TestDetail1538_FormatGuildChatMessage()
    {
        Assert.Equal("[行会]会长: 今晚八点攻城集合！", OfficialImmortalLegend100DetailsEngine.FormatGuildChatMessage("会长", "今晚八点攻城集合！"));
    }

    [Fact]
    public void TestDetail1539_CalculateMuteDurationTick()
    {
        long now = 1000L;
        Assert.Equal(now + 600000L, OfficialImmortalLegend100DetailsEngine.CalculateMuteDurationTick(now));
    }

    [Fact]
    public void TestDetail1540_ValidateCustomRankTitle()
    {
        Assert.True(OfficialImmortalLegend100DetailsEngine.ValidateCustomRankTitle("护法长老"));
        Assert.False(OfficialImmortalLegend100DetailsEngine.ValidateCustomRankTitle(string.Empty));
        Assert.False(OfficialImmortalLegend100DetailsEngine.ValidateCustomRankTitle("超级无敌大将军统帅护法元老长门"));
    }

    // =========================================================================
    // 细节 1541 ~ 1550: Direct-Blit 协议抗丢包、防重放滑动窗口与网络安全守门线
    // =========================================================================

    [Fact]
    public void TestDetail1541_IsSequenceStrictlyMonotonic()
    {
        Assert.True(OfficialImmortalLegend100DetailsEngine.IsSequenceStrictlyMonotonic(10, 11));
        Assert.False(OfficialImmortalLegend100DetailsEngine.IsSequenceStrictlyMonotonic(10, 12));
    }

    [Fact]
    public void TestDetail1542_IsSlidingWindowAcceptable()
    {
        Assert.True(OfficialImmortalLegend100DetailsEngine.IsSlidingWindowAcceptable(10, 15));
        Assert.False(OfficialImmortalLegend100DetailsEngine.IsSlidingWindowAcceptable(10, 25));
        Assert.False(OfficialImmortalLegend100DetailsEngine.IsSlidingWindowAcceptable(10, 10));
    }

    [Fact]
    public void TestDetail1543_ValidateDirectBlitMagicWord()
    {
        Assert.True(OfficialImmortalLegend100DetailsEngine.ValidateDirectBlitMagicWord(0x594D));
        Assert.False(OfficialImmortalLegend100DetailsEngine.ValidateDirectBlitMagicWord(0x1234));
    }

    [Fact]
    public void TestDetail1544_GetDirectBlitHeaderSize()
    {
        Assert.Equal(4, OfficialImmortalLegend100DetailsEngine.GetDirectBlitHeaderSize());
    }

    [Fact]
    public void TestDetail1545_ValidateMoveStepDistance()
    {
        Assert.True(OfficialImmortalLegend100DetailsEngine.ValidateMoveStepDistance(10, 10, 11, 11, false));
        Assert.False(OfficialImmortalLegend100DetailsEngine.ValidateMoveStepDistance(10, 10, 12, 10, false));
        Assert.True(OfficialImmortalLegend100DetailsEngine.ValidateMoveStepDistance(10, 10, 12, 10, true));
        Assert.False(OfficialImmortalLegend100DetailsEngine.ValidateMoveStepDistance(10, 10, 13, 10, true));
    }

    [Fact]
    public void TestDetail1546_ValidateActionCadence()
    {
        long last = 1000L;
        Assert.False(OfficialImmortalLegend100DetailsEngine.ValidateActionCadence(last, last + 600L, isSpell: false));
        Assert.True(OfficialImmortalLegend100DetailsEngine.ValidateActionCadence(last, last + 850L, isSpell: false));
        Assert.True(OfficialImmortalLegend100DetailsEngine.ValidateActionCadence(last, last + 550L, isSpell: true));
    }

    [Fact]
    public void TestDetail1547_ValidateDrinkPotionCadence()
    {
        long last = 1000L;
        Assert.False(OfficialImmortalLegend100DetailsEngine.ValidateDrinkPotionCadence(last, last + 150L));
        Assert.True(OfficialImmortalLegend100DetailsEngine.ValidateDrinkPotionCadence(last, last + 250L));
    }

    [Fact]
    public void TestDetail1548_ValidatePickupProximity()
    {
        Assert.True(OfficialImmortalLegend100DetailsEngine.ValidatePickupProximity(100, 100, 100, 100));
        Assert.False(OfficialImmortalLegend100DetailsEngine.ValidatePickupProximity(100, 100, 101, 100));
    }

    [Fact]
    public void TestDetail1549_IsTradeWithinSafeDistance()
    {
        Assert.True(OfficialImmortalLegend100DetailsEngine.IsTradeWithinSafeDistance(50, 50, 52, 53));
        Assert.False(OfficialImmortalLegend100DetailsEngine.IsTradeWithinSafeDistance(50, 50, 55, 50));
    }

    [Fact]
    public void TestDetail1550_ValidateRateLimiter()
    {
        Assert.True(OfficialImmortalLegend100DetailsEngine.ValidateRateLimiter(45));
        Assert.False(OfficialImmortalLegend100DetailsEngine.ValidateRateLimiter(65));
    }
}
