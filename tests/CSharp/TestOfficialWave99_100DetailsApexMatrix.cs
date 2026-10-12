using System;
using Mir2.Shared.Protocols.Modern;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 盛大官方 1.5 极意登峰 100 微观细节测试矩阵 (Wave 99: Details 2051 ~ 2150)
/// 针对 OfficialImmortalApex100DetailsEngine.cs 进行全覆盖严格验证
/// </summary>
public sealed class TestOfficialWave99_100DetailsApexMatrix
{
    // =========================================================================
    // 1. 沃玛寺庙三层与教主狂暴瞬移机制 (2051~2060)
    // =========================================================================

    [Fact]
    public void TestDetail2051_WomaLordBerserk()
    {
        var (berserk1, dc1, ac1) = OfficialImmortalApex100DetailsEngine.CheckWomaLordBerserk(660, 2200, 100, 50);
        Assert.True(berserk1);
        Assert.Equal(150, dc1);
        Assert.Equal(65, ac1);

        var (berserk2, dc2, ac2) = OfficialImmortalApex100DetailsEngine.CheckWomaLordBerserk(661, 2200, 100, 50);
        Assert.False(berserk2);
        Assert.Equal(100, dc2);
        Assert.Equal(50, ac2);
    }

    [Fact]
    public void TestDetail2052_WomaLordTeleportTimer()
    {
        Assert.True(OfficialImmortalApex100DetailsEngine.ShouldWomaLordTeleport(true, 10000, 18000));
        Assert.False(OfficialImmortalApex100DetailsEngine.ShouldWomaLordTeleport(true, 10000, 17999));
        Assert.False(OfficialImmortalApex100DetailsEngine.ShouldWomaLordTeleport(false, 10000, 20000));
    }

    [Fact]
    public void TestDetail2053_WomaChainLightning()
    {
        var (main, chain, maxTargets) = OfficialImmortalApex100DetailsEngine.CalculateWomaChainLightning(100);
        Assert.Equal(100, main);
        Assert.Equal(60, chain);
        Assert.Equal(3, maxTargets);
    }

    [Fact]
    public void TestDetail2054_WomaLevel3EntryValidation()
    {
        var (ok1, _) = OfficialImmortalApex100DetailsEngine.ValidateWomaLevel3Entry(20, 50, 360);
        Assert.True(ok1);

        var (ok2, msg2) = OfficialImmortalApex100DetailsEngine.ValidateWomaLevel3Entry(19, 50, 360);
        Assert.False(ok2);
        Assert.Contains("20 级", msg2);

        var (ok3, _) = OfficialImmortalApex100DetailsEngine.ValidateWomaLevel3Entry(20, 51, 360);
        Assert.False(ok3);
    }

    [Fact]
    public void TestDetail2055_WomaGuardHammerKnockback()
    {
        var (knock1, dist1) = OfficialImmortalApex100DetailsEngine.CheckWomaGuardHammerKnockback(14);
        Assert.True(knock1);
        Assert.Equal(1, dist1);

        var (knock2, dist2) = OfficialImmortalApex100DetailsEngine.CheckWomaGuardHammerKnockback(15);
        Assert.False(knock2);
        Assert.Equal(0, dist2);
    }

    [Fact]
    public void TestDetail2056_WomaWarriorBleed()
    {
        var (bleed1, dmg1, sec1) = OfficialImmortalApex100DetailsEngine.CheckWomaWarriorBleed(24);
        Assert.True(bleed1);
        Assert.Equal(4, dmg1);
        Assert.Equal(6, sec1);

        var (bleed2, _, _) = OfficialImmortalApex100DetailsEngine.CheckWomaWarriorBleed(25);
        Assert.False(bleed2);
    }

    [Fact]
    public void TestDetail2057_DarkWarriorAiProfile()
    {
        var (interval, target) = OfficialImmortalApex100DetailsEngine.GetDarkWarriorAiProfile();
        Assert.Equal(250, interval);
        Assert.Equal("Wizard", target);
    }

    [Fact]
    public void TestDetail2058_FireBatExplosion()
    {
        var (exp, rad, dmg) = OfficialImmortalApex100DetailsEngine.CheckFireBatDeathExplosion(true);
        Assert.True(exp);
        Assert.Equal(1, rad);
        Assert.Equal(30, dmg);
    }

    [Fact]
    public void TestDetail2059_WomaHornDropRule()
    {
        var (drop, name, sec) = OfficialImmortalApex100DetailsEngine.GetWomaHornDropRule();
        Assert.True(drop);
        Assert.Equal("沃玛号角", name);
        Assert.Equal(60, sec);
    }

    [Fact]
    public void TestDetail2060_WomaLevel3RandomScrollBlocked()
    {
        Assert.False(OfficialImmortalApex100DetailsEngine.IsRandomScrollAllowedInWomaLevel3(17));
        Assert.True(OfficialImmortalApex100DetailsEngine.IsRandomScrollAllowedInWomaLevel3(1));
    }

    // =========================================================================
    // 2. 祖玛教主石化唤醒与护卫小怪群召唤 (2061~2070)
    // =========================================================================

    [Fact]
    public void TestDetail2061_ZumaLordWakeUp()
    {
        Assert.True(OfficialImmortalApex100DetailsEngine.CheckZumaLordWakeUp(true, 4, false));
        Assert.True(OfficialImmortalApex100DetailsEngine.CheckZumaLordWakeUp(true, 10, true));
        Assert.False(OfficialImmortalApex100DetailsEngine.CheckZumaLordWakeUp(true, 5, false));
        Assert.False(OfficialImmortalApex100DetailsEngine.CheckZumaLordWakeUp(false, 2, false));
    }

    [Fact]
    public void TestDetail2062_ZumaLordWakeUpShockwave()
    {
        var (rad, dmg, dist) = OfficialImmortalApex100DetailsEngine.GetZumaLordWakeUpShockwave();
        Assert.Equal(2, rad);
        Assert.Equal(50, dmg);
        Assert.Equal(1, dist);
    }

    [Fact]
    public void TestDetail2063_ZumaLordSummonMinions()
    {
        bool s70 = false, s50 = false, s20 = false;
        var (sum1, stat1, guard1, arch1) = OfficialImmortalApex100DetailsEngine.CheckZumaLordSummonMinions(700, 1000, ref s70, ref s50, ref s20);
        Assert.True(sum1);
        Assert.Equal(2, stat1);
        Assert.Equal(1, guard1);
        Assert.Equal(1, arch1);
        Assert.True(s70);

        var (sumRepeat, _, _, _) = OfficialImmortalApex100DetailsEngine.CheckZumaLordSummonMinions(690, 1000, ref s70, ref s50, ref s20);
        Assert.False(sumRepeat);

        var (sum2, _, _, _) = OfficialImmortalApex100DetailsEngine.CheckZumaLordSummonMinions(500, 1000, ref s70, ref s50, ref s20);
        Assert.True(sum2);
        Assert.True(s50);

        var (sum3, _, _, _) = OfficialImmortalApex100DetailsEngine.CheckZumaLordSummonMinions(200, 1000, ref s70, ref s50, ref s20);
        Assert.True(sum3);
        Assert.True(s20);
    }

    [Fact]
    public void TestDetail2064_ZumaLordStabReflect()
    {
        var (blk1, ref1) = OfficialImmortalApex100DetailsEngine.CheckZumaLordStabReflect(true, 19, 100);
        Assert.True(blk1);
        Assert.Equal(20, ref1);

        var (blk2, ref2) = OfficialImmortalApex100DetailsEngine.CheckZumaLordStabReflect(true, 20, 100);
        Assert.False(blk2);
        Assert.Equal(0, ref2);

        var (blk3, _) = OfficialImmortalApex100DetailsEngine.CheckZumaLordStabReflect(false, 5, 100);
        Assert.False(blk3);
    }

    [Fact]
    public void TestDetail2065_ZumaArcherProfile()
    {
        var (range, seeThrough) = OfficialImmortalApex100DetailsEngine.GetZumaArcherProfile();
        Assert.Equal(7, range);
        Assert.True(seeThrough);
    }

    [Fact]
    public void TestDetail2066_ZumaStatueUnfreeze()
    {
        var (stiff, immune) = OfficialImmortalApex100DetailsEngine.GetZumaStatueUnfreezeProfile();
        Assert.Equal(1000, stiff);
        Assert.True(immune);
    }

    [Fact]
    public void TestDetail2067_ZumaGuardArmorPierce()
    {
        var (p1, pct1) = OfficialImmortalApex100DetailsEngine.CheckZumaGuardArmorPierce(9);
        Assert.True(p1);
        Assert.Equal(50, pct1);

        var (p2, pct2) = OfficialImmortalApex100DetailsEngine.CheckZumaGuardArmorPierce(10);
        Assert.False(p2);
        Assert.Equal(0, pct2);
    }

    [Fact]
    public void TestDetail2068_ZumaHomeScrollBlocked()
    {
        Assert.False(OfficialImmortalApex100DetailsEngine.IsScrollAllowedInZumaHome(24));
        Assert.True(OfficialImmortalApex100DetailsEngine.IsScrollAllowedInZumaHome(0));
    }

    [Fact]
    public void TestDetail2069_ZumaRelicDropRule()
    {
        var (den, name, sec) = OfficialImmortalApex100DetailsEngine.GetZumaRelicDropRule();
        Assert.Equal(20, den);
        Assert.Equal("祖玛头像", name);
        Assert.Equal(60, sec);
    }

    [Fact]
    public void TestDetail2070_ZumaPassageCongestion()
    {
        var (c1, p1) = OfficialImmortalApex100DetailsEngine.CheckZumaNarrowPassageCongestion(16);
        Assert.True(c1);
        Assert.Equal(30, p1);

        var (c2, p2) = OfficialImmortalApex100DetailsEngine.CheckZumaNarrowPassageCongestion(15);
        Assert.False(c2);
        Assert.Equal(0, p2);
    }

    // =========================================================================
    // 3. 白日门丛林毒物与道士炼毒施毒核心生态 (2071~2080)
    // =========================================================================

    [Fact]
    public void TestDetail2071_TaoistHerbGatheringEntry()
    {
        var (ok1, fee1) = OfficialImmortalApex100DetailsEngine.ValidateTaoistHerbGatheringEntry(12, 10, true);
        Assert.True(ok1);
        Assert.Equal(0, fee1);

        var (ok2, fee2) = OfficialImmortalApex100DetailsEngine.ValidateTaoistHerbGatheringEntry(12, 10, false);
        Assert.True(ok2);
        Assert.Equal(10000, fee2);

        var (ok3, _) = OfficialImmortalApex100DetailsEngine.ValidateTaoistHerbGatheringEntry(11, 10, false);
        Assert.False(ok3);
    }

    [Fact]
    public void TestDetail2072_TaoistPoisonRefining()
    {
        var (ok1, _) = OfficialImmortalApex100DetailsEngine.ValidateTaoistPoisonRefining(7, "黄色药粉", 5000);
        Assert.True(ok1);

        var (ok2, msg2) = OfficialImmortalApex100DetailsEngine.ValidateTaoistPoisonRefining(6, "黄色药粉", 5000);
        Assert.False(ok2);
        Assert.Contains("法宝槽", msg2);

        var (ok3, msg3) = OfficialImmortalApex100DetailsEngine.ValidateTaoistPoisonRefining(7, "金戒指", 5000);
        Assert.False(ok3);

        var (ok4, msg4) = OfficialImmortalApex100DetailsEngine.ValidateTaoistPoisonRefining(8, "灰色药粉", 500);
        Assert.False(ok4);
        Assert.Contains("工本费", msg4);
    }

    [Fact]
    public void TestDetail2073_PoisonCastingRangeAndTarget()
    {
        var (ok1, _) = OfficialImmortalApex100DetailsEngine.ValidatePoisonCastingRangeAndTarget(48, 48, 2, 0, 1);
        Assert.True(ok1);

        var (ok2, msg2) = OfficialImmortalApex100DetailsEngine.ValidatePoisonCastingRangeAndTarget(48, 49, 1, 0, 1);
        Assert.False(ok2);
        Assert.Contains("地图", msg2);

        var (ok3, msg3) = OfficialImmortalApex100DetailsEngine.ValidatePoisonCastingRangeAndTarget(48, 48, 9, 0, 1);
        Assert.False(ok3);
        Assert.Contains("8 格", msg3);
    }

    [Fact]
    public void TestDetail2074_TaoistPoisonBroadcastMessage()
    {
        string msg = OfficialImmortalApex100DetailsEngine.GenerateTaoistPoisonBroadcastMessage("邪恶毒蛇", "青云道长");
        Assert.Contains("邪恶毒蛇", msg);
        Assert.Contains("青云道长", msg);
        Assert.Contains("红绿剧毒", msg);
    }

    [Fact]
    public void TestDetail2075_TaoistSummonPetRule()
    {
        var (can1, cd, mp) = OfficialImmortalApex100DetailsEngine.GetTaoistSummonPetRule(100000, 103000);
        Assert.True(can1);
        Assert.Equal(3, cd);
        Assert.Equal(30, mp);

        var (can2, _, _) = OfficialImmortalApex100DetailsEngine.GetTaoistSummonPetRule(100000, 102999);
        Assert.False(can2);
    }

    [Fact]
    public void TestDetail2076_PoisonImmuneMonsterMap()
    {
        Assert.True(OfficialImmortalApex100DetailsEngine.IsPoisonImmuneMonsterMap(9));
        Assert.True(OfficialImmortalApex100DetailsEngine.IsPoisonImmuneMonsterMap(24));
        Assert.True(OfficialImmortalApex100DetailsEngine.IsPoisonImmuneMonsterMap(32));
        Assert.False(OfficialImmortalApex100DetailsEngine.IsPoisonImmuneMonsterMap(0));
    }

    [Fact]
    public void TestDetail2077_SpecialPoisonRefinePenalty()
    {
        var (gold, fame) = OfficialImmortalApex100DetailsEngine.GetSpecialPoisonRefinePenalty();
        Assert.Equal(1000, gold);
        Assert.Equal(1, fame);
    }

    [Fact]
    public void TestDetail2078_TaoistPoisonPeaceZone()
    {
        Assert.True(OfficialImmortalApex100DetailsEngine.IsTaoistPoisonPeaceZone(48));
        Assert.False(OfficialImmortalApex100DetailsEngine.IsTaoistPoisonPeaceZone(0));
    }

    [Fact]
    public void TestDetail2079_TaoistHealBonus()
    {
        int heal1 = OfficialImmortalApex100DetailsEngine.ApplyTaoistHealBonus(100, true);
        Assert.Equal(120, heal1);

        int heal2 = OfficialImmortalApex100DetailsEngine.ApplyTaoistHealBonus(100, false);
        Assert.Equal(100, heal2);
    }

    [Fact]
    public void TestDetail2080_ConvertToBoundPoisonPouch()
    {
        var (name, canTrade, canDrop) = OfficialImmortalApex100DetailsEngine.ConvertToBoundPoisonPouch();
        Assert.Equal("特制道士药包", name);
        Assert.False(canTrade);
        Assert.False(canDrop);
    }

    // =========================================================================
    // 4. 武器升级持久损耗加成与跳点极品概率模型 (2081~2090)
    // =========================================================================

    [Fact]
    public void TestDetail2081_BlackIronDuraChange_Under30()
    {
        Assert.Equal(-3, OfficialImmortalApex100DetailsEngine.CalculateBlackIronDuraChange(29, 0));
    }

    [Fact]
    public void TestDetail2082_BlackIronDuraChange_30To59()
    {
        Assert.Equal(-1, OfficialImmortalApex100DetailsEngine.CalculateBlackIronDuraChange(45, 59));
        Assert.Equal(0, OfficialImmortalApex100DetailsEngine.CalculateBlackIronDuraChange(45, 60));
    }

    [Fact]
    public void TestDetail2083_BlackIronDuraChange_60To89()
    {
        Assert.Equal(1, OfficialImmortalApex100DetailsEngine.CalculateBlackIronDuraChange(75, 19));
        Assert.Equal(0, OfficialImmortalApex100DetailsEngine.CalculateBlackIronDuraChange(75, 20));
    }

    [Fact]
    public void TestDetail2084_BlackIronDuraChange_90Plus()
    {
        Assert.Equal(1, OfficialImmortalApex100DetailsEngine.CalculateBlackIronDuraChange(90, 50));
        Assert.Equal(2, OfficialImmortalApex100DetailsEngine.CalculateSuperBlackIronDuraBonus(120, 5, 19));
        Assert.Equal(1, OfficialImmortalApex100DetailsEngine.CalculateSuperBlackIronDuraBonus(120, 5, 20));
        Assert.Equal(1, OfficialImmortalApex100DetailsEngine.CalculateSuperBlackIronDuraBonus(119, 5, 10));
    }

    [Fact]
    public void TestDetail2085_JewelryBaseSuccess()
    {
        var (q1, pct1) = OfficialImmortalApex100DetailsEngine.CheckJewelryBaseSuccess(10);
        Assert.True(q1);
        Assert.Equal(50, pct1);

        var (q2, pct2) = OfficialImmortalApex100DetailsEngine.CheckJewelryBaseSuccess(9);
        Assert.False(q2);
        Assert.Equal(20, pct2);
    }

    [Fact]
    public void TestDetail2086_WeaponUpgradeJumpPoint()
    {
        var (jump1, pts1) = OfficialImmortalApex100DetailsEngine.CheckWeaponUpgradeJumpPoint(30, 10, 4);
        Assert.True(jump1);
        Assert.Equal(2, pts1);

        var (jump2, pts2) = OfficialImmortalApex100DetailsEngine.CheckWeaponUpgradeJumpPoint(30, 10, 5);
        Assert.False(jump2);
        Assert.Equal(1, pts2);

        var (jump3, pts3) = OfficialImmortalApex100DetailsEngine.CheckWeaponUpgradeJumpPoint(29, 10, 2);
        Assert.False(jump3);
        Assert.Equal(1, pts3);
    }

    [Fact]
    public void TestDetail2087_CanUpgradeWeaponMaxLimit()
    {
        Assert.True(OfficialImmortalApex100DetailsEngine.CanUpgradeWeaponMaxLimit(6));
        Assert.False(OfficialImmortalApex100DetailsEngine.CanUpgradeWeaponMaxLimit(7));
    }

    [Fact]
    public void TestDetail2088_WeaponTrialTiming()
    {
        var (ok1, _) = OfficialImmortalApex100DetailsEngine.CheckWeaponTrialTiming(100000, 700000);
        Assert.True(ok1);

        var (ok2, msg2) = OfficialImmortalApex100DetailsEngine.CheckWeaponTrialTiming(100000, 700001);
        Assert.False(ok2);
        Assert.Contains("超时", msg2);
    }

    [Fact]
    public void TestDetail2089_TrialStrikeBreakRisk()
    {
        Assert.Equal(25, OfficialImmortalApex100DetailsEngine.CalculateTrialStrikeBreakRisk(true, 10));
        Assert.Equal(10, OfficialImmortalApex100DetailsEngine.CalculateTrialStrikeBreakRisk(false, 10));
    }

    [Fact]
    public void TestDetail2090_ResolveTrialStrikeOutcome()
    {
        var (shake1, broken1) = OfficialImmortalApex100DetailsEngine.ResolveTrialStrikeOutcome(true);
        Assert.Equal(300, shake1);
        Assert.False(broken1);

        var (shake2, broken2) = OfficialImmortalApex100DetailsEngine.ResolveTrialStrikeOutcome(false);
        Assert.Equal(0, shake2);
        Assert.True(broken2);
    }

    // =========================================================================
    // =========================================================================
    // 5. 比奇城皇宫国王任务与沃玛号角创建行会权威规则 (2091~2100)
    // =========================================================================

    [Fact]
    public void TestDetail2091_GuildCreationCriteria()
    {
        var (ok1, _) = OfficialImmortalApex100DetailsEngine.ValidateGuildCreationCriteria(25, false, 1_000_000, true);
        Assert.True(ok1);

        var (ok2, _) = OfficialImmortalApex100DetailsEngine.ValidateGuildCreationCriteria(21, false, 1_000_000, true);
        Assert.False(ok2);

        var (ok3, _) = OfficialImmortalApex100DetailsEngine.ValidateGuildCreationCriteria(25, true, 1_000_000, true);
        Assert.False(ok3);

        var (ok4, _) = OfficialImmortalApex100DetailsEngine.ValidateGuildCreationCriteria(25, false, 999_999, true);
        Assert.False(ok4);

        var (ok5, _) = OfficialImmortalApex100DetailsEngine.ValidateGuildCreationCriteria(25, false, 1_000_000, false);
        Assert.False(ok5);
    }

    [Fact]
    public void TestDetail2092_GuildPartyExpShare()
    {
        var (appExp, masExp) = OfficialImmortalApex100DetailsEngine.CalculateGuildPartyExpShare(1000);
        Assert.Equal(1200, appExp);
        Assert.Equal(120, masExp);
    }

    [Fact]
    public void TestDetail2093_GuildPromotionEligibility()
    {
        var (grad1, masFame1, appFame1) = OfficialImmortalApex100DetailsEngine.CheckGuildPromotionEligibility(28);
        Assert.True(grad1);
        Assert.Equal(32, masFame1);
        Assert.Equal(15, appFame1);

        var (grad2, _, _) = OfficialImmortalApex100DetailsEngine.CheckGuildPromotionEligibility(27);
        Assert.False(grad2);
    }

    [Fact]
    public void TestDetail2094_GuildLeavePenalty()
    {
        var (expPct, banHours) = OfficialImmortalApex100DetailsEngine.GetGuildLeavePenalty();
        Assert.Equal(20, expPct);
        Assert.Equal(24, banHours);
    }

    [Fact]
    public void TestDetail2095_PalaceAccessEligibility()
    {
        var (ok1, rank1) = OfficialImmortalApex100DetailsEngine.CheckPalaceAccessEligibility(40);
        Assert.True(ok1);
        Assert.Equal(2, rank1);

        var (ok2, rank2) = OfficialImmortalApex100DetailsEngine.CheckPalaceAccessEligibility(20);
        Assert.True(ok2);
        Assert.Equal(1, rank2);

        var (ok3, rank3) = OfficialImmortalApex100DetailsEngine.CheckPalaceAccessEligibility(19);
        Assert.False(ok3);
        Assert.Equal(0, rank3);
    }

    [Fact]
    public void TestDetail2096_ConsumeSpecialItemDuraOnDeath()
    {
        var (dura1, shat1) = OfficialImmortalApex100DetailsEngine.ConsumeSpecialItemDuraOnDeath(5);
        Assert.Equal(4, dura1);
        Assert.False(shat1);

        var (dura2, shat2) = OfficialImmortalApex100DetailsEngine.ConsumeSpecialItemDuraOnDeath(1);
        Assert.Equal(0, dura2);
        Assert.True(shat2);
    }

    [Fact]
    public void TestDetail2097_BlessingOilRefineExchange()
    {
        var (ok1, cost1) = OfficialImmortalApex100DetailsEngine.GetBlessingOilRefineExchange(5);
        Assert.True(ok1);
        Assert.Equal(5, cost1);

        var (ok2, _) = OfficialImmortalApex100DetailsEngine.GetBlessingOilRefineExchange(4);
        Assert.False(ok2);
    }

    [Fact]
    public void TestDetail2098_RedeemBichonGuardPardon()
    {
        var (pkRed1, cost1) = OfficialImmortalApex100DetailsEngine.RedeemBichonGuardPardon(4, 100);
        Assert.Equal(100, pkRed1);
        Assert.Equal(4, cost1);

        var (pkRed2, cost2) = OfficialImmortalApex100DetailsEngine.RedeemBichonGuardPardon(2, 30);
        Assert.Equal(50, pkRed2);
        Assert.Equal(2, cost2);

        var (pkRed3, cost3) = OfficialImmortalApex100DetailsEngine.RedeemBichonGuardPardon(1, 100);
        Assert.Equal(0, pkRed3);
        Assert.Equal(0, cost3);
    }

    [Fact]
    public void TestDetail2099_CheckMemoryRecallCooldown()
    {
        var (ok1, hours1) = OfficialImmortalApex100DetailsEngine.CheckMemoryRecallCooldown(0, 24 * 3600 * 1000);
        Assert.True(ok1);
        Assert.Equal(24, hours1);

        var (ok2, _) = OfficialImmortalApex100DetailsEngine.CheckMemoryRecallCooldown(0, 24 * 3600 * 1000 - 1);
        Assert.False(ok2);
    }

    [Fact]
    public void TestDetail2100_IsRedNameAllowedInKingPalace()
    {
        Assert.True(OfficialImmortalApex100DetailsEngine.IsRedNameAllowedInKingPalace(199));
        Assert.False(OfficialImmortalApex100DetailsEngine.IsRedNameAllowedInKingPalace(200));
    }

    // =========================================================================
    // 6. 沙巴克皇宫密道与城门耐久修补机制 (2101~2110)
    // =========================================================================

    [Fact]
    public void TestDetail2101_SabukGateMaxHp()
    {
        var (stage1, pass1, vul1) = OfficialImmortalApex100DetailsEngine.GetSabukGateStatus(50000);
        Assert.Equal("完好", stage1);
        Assert.False(pass1);
        Assert.Equal(0, vul1);
    }

    [Fact]
    public void TestDetail2102_SabukGateBreakStages()
    {
        var (stage2, pass2, vul2) = OfficialImmortalApex100DetailsEngine.GetSabukGateStatus(10000);
        Assert.Equal("裂损", stage2);
        Assert.False(pass2);
        Assert.Equal(20, vul2);

        var (stage3, pass3, vul3) = OfficialImmortalApex100DetailsEngine.GetSabukGateStatus(0);
        Assert.Equal("崩塌", stage3);
        Assert.True(pass3);
        Assert.Equal(0, vul3);
    }

    [Fact]
    public void TestDetail2103_SabukGateRepairCost()
    {
        var (can, gold, ore, pur, min) = OfficialImmortalApex100DetailsEngine.GetSabukGateRepairCost();
        Assert.True(can);
        Assert.Equal(200000, gold);
        Assert.Equal(50, ore);
        Assert.Equal(15, pur);
        Assert.Equal(5, min);
    }

    [Fact]
    public void TestDetail2104_SabukSecretPassageEntry()
    {
        var (ok1, fee1) = OfficialImmortalApex100DetailsEngine.ValidateSabukSecretPassageEntry(30, 15, 80, true);
        Assert.True(ok1);
        Assert.Equal(0, fee1);

        var (ok2, fee2) = OfficialImmortalApex100DetailsEngine.ValidateSabukSecretPassageEntry(30, 15, 80, false);
        Assert.True(ok2);
        Assert.Equal(5000, fee2);

        var (ok3, _) = OfficialImmortalApex100DetailsEngine.ValidateSabukSecretPassageEntry(29, 15, 80, true);
        Assert.False(ok3);
    }

    [Fact]
    public void TestDetail2105_SecretPassageAllowedDuringWar()
    {
        Assert.True(OfficialImmortalApex100DetailsEngine.IsSecretPassageAllowedDuringWar(false, false));
        Assert.True(OfficialImmortalApex100DetailsEngine.IsSecretPassageAllowedDuringWar(true, true));
        Assert.False(OfficialImmortalApex100DetailsEngine.IsSecretPassageAllowedDuringWar(true, false));
    }

    [Fact]
    public void TestDetail2106_PalaceCaptureTick()
    {
        Assert.True(OfficialImmortalApex100DetailsEngine.CheckPalaceCaptureTick(0, 5, 10));
        Assert.False(OfficialImmortalApex100DetailsEngine.CheckPalaceCaptureTick(1, 5, 10));
        Assert.False(OfficialImmortalApex100DetailsEngine.CheckPalaceCaptureTick(0, 0, 10));
        Assert.False(OfficialImmortalApex100DetailsEngine.CheckPalaceCaptureTick(0, 5, 9));
    }

    [Fact]
    public void TestDetail2107_SabukWarTime()
    {
        Assert.True(OfficialImmortalApex100DetailsEngine.IsSabukWarTime(20, 0));
        Assert.True(OfficialImmortalApex100DetailsEngine.IsSabukWarTime(22, 59));
        Assert.False(OfficialImmortalApex100DetailsEngine.IsSabukWarTime(19, 59));
        Assert.False(OfficialImmortalApex100DetailsEngine.IsSabukWarTime(23, 0));
    }

    [Fact]
    public void TestDetail2108_SabukCityLordTax()
    {
        var (t1, max1) = OfficialImmortalApex100DetailsEngine.CalculateSabukCityLordTax(1500000);
        Assert.Equal(1500000, t1);
        Assert.Equal(2000000, max1);

        var (t2, _) = OfficialImmortalApex100DetailsEngine.CalculateSabukCityLordTax(2500000);
        Assert.Equal(2000000, t2);
    }

    [Fact]
    public void TestDetail2109_SabukMemberRepairDiscount()
    {
        int p1 = OfficialImmortalApex100DetailsEngine.ApplySabukMemberRepairDiscount(3000, true);
        Assert.Equal(2400, p1);

        int p2 = OfficialImmortalApex100DetailsEngine.ApplySabukMemberRepairDiscount(3000, false);
        Assert.Equal(3000, p2);
    }

    [Fact]
    public void TestDetail2110_WarRespawnPoint()
    {
        var (loc1, x1, y1) = OfficialImmortalApex100DetailsEngine.GetWarRespawnPoint(true);
        Assert.Equal("皇宫内室", loc1);
        Assert.Equal(15, x1);
        Assert.Equal(15, y1);

        var (loc2, x2, y2) = OfficialImmortalApex100DetailsEngine.GetWarRespawnPoint(false);
        Assert.Equal("城门集合点", loc2);
        Assert.Equal(650, x2);
        Assert.Equal(300, y2);
    }

    // =========================================================================
    // 7. 道士神兽防御/喷火双形态与骷髅进化攻击力 (2111~2120)
    // =========================================================================

    [Fact]
    public void TestDetail2111_ShinsuDefendStance()
    {
        var (def1, _, _) = OfficialImmortalApex100DetailsEngine.GetShinsuStanceBehavior(false);
        Assert.True(def1);

        var (def2, _, _) = OfficialImmortalApex100DetailsEngine.GetShinsuStanceBehavior(true);
        Assert.False(def2);
    }

    [Fact]
    public void TestDetail2112_ShinsuDefendStanceBuffs()
    {
        var (_, ac1, hp1) = OfficialImmortalApex100DetailsEngine.GetShinsuStanceBehavior(false);
        Assert.Equal(50, ac1);
        Assert.Equal(5, hp1);

        var (_, ac2, hp2) = OfficialImmortalApex100DetailsEngine.GetShinsuStanceBehavior(true);
        Assert.Equal(0, ac2);
        Assert.Equal(0, hp2);
    }

    [Fact]
    public void TestDetail2113_ShinsuFireBreath()
    {
        var (main, pierce, dist) = OfficialImmortalApex100DetailsEngine.CalculateShinsuFireBreath(100);
        Assert.Equal(100, main);
        Assert.Equal(50, pierce);
        Assert.Equal(2, dist);
    }

    [Fact]
    public void TestDetail2114_ShinsuStatsByLevel()
    {
        var (hp0, min0, max0) = OfficialImmortalApex100DetailsEngine.GetShinsuStatsByLevel(0);
        Assert.Equal(500, hp0);
        Assert.Equal(15, min0);
        Assert.Equal(25, max0);

        var (hp7, min7, max7) = OfficialImmortalApex100DetailsEngine.GetShinsuStatsByLevel(7);
        Assert.Equal(2400, hp7);
        Assert.Equal(45, min7);
        Assert.Equal(65, max7);
    }

    [Fact]
    public void TestDetail2115_SkeletonVisualProfile()
    {
        var (axe1, col1) = OfficialImmortalApex100DetailsEngine.GetSkeletonVisualProfile(1);
        Assert.Equal("木柄小斧", axe1);
        Assert.Equal("黄名", col1);

        var (axe4, col4) = OfficialImmortalApex100DetailsEngine.GetSkeletonVisualProfile(4);
        Assert.Equal("铁斧", axe4);
        Assert.Equal("蓝名", col4);

        var (axe7, col7) = OfficialImmortalApex100DetailsEngine.GetSkeletonVisualProfile(7);
        Assert.Equal("双刃大板斧", axe7);
        Assert.Equal("深蓝/红名", col7);
    }

    [Fact]
    public void TestDetail2116_Rank7SkeletonCombatProfile()
    {
        var (spd, minDc, maxDc, move) = OfficialImmortalApex100DetailsEngine.GetRank7SkeletonCombatProfile();
        Assert.Equal(350, spd);
        Assert.Equal(25, minDc);
        Assert.Equal(40, maxDc);
        Assert.Equal(300, move);
    }

    [Fact]
    public void TestDetail2117_TaoistPetHealBonus()
    {
        Assert.Equal(130, OfficialImmortalApex100DetailsEngine.ApplyTaoistPetHealBonus(100));
    }

    [Fact]
    public void TestDetail2118_PetAttackPkStatus()
    {
        var (p1, m1) = OfficialImmortalApex100DetailsEngine.CheckPetAttackWhitePlayerPkStatus(false);
        Assert.False(p1);
        Assert.False(m1);

        var (p2, m2) = OfficialImmortalApex100DetailsEngine.CheckPetAttackWhitePlayerPkStatus(true);
        Assert.True(p2);
        Assert.True(m2);
    }

    [Fact]
    public void TestDetail2119_PetTeleportFollowRule()
    {
        var (dist, sec) = OfficialImmortalApex100DetailsEngine.GetPetTeleportFollowRule();
        Assert.Equal(1, dist);
        Assert.Equal(3, sec);
    }

    [Fact]
    public void TestDetail2120_PetLoyaltyDecay()
    {
        var (l1, r1) = OfficialImmortalApex100DetailsEngine.DecayPetLoyalty(30);
        Assert.Equal(25, l1);
        Assert.Equal(75, r1);

        var (l2, r2) = OfficialImmortalApex100DetailsEngine.DecayPetLoyalty(20);
        Assert.Equal(15, l2);
        Assert.Equal(100, r2);
    }

    // =========================================================================
    // 8. 战士刺杀二位剑气破盾穿透与双烈火爆发 (2121~2130)
    // =========================================================================

    [Fact]
    public void TestDetail2121_StabbingSwordPos1()
    {
        var (p1, ac1, sh1) = OfficialImmortalApex100DetailsEngine.CheckStabbingSwordPos(1);
        Assert.False(p1);
        Assert.False(ac1);
        Assert.False(sh1);
    }

    [Fact]
    public void TestDetail2122_StabbingSwordPos2()
    {
        var (p2, ac2, sh2) = OfficialImmortalApex100DetailsEngine.CheckStabbingSwordPos(2);
        Assert.True(p2);
        Assert.True(ac2);
        Assert.True(sh2);
    }

    [Fact]
    public void TestDetail2123_FireSwordCharging()
    {
        var (act1, sec1) = OfficialImmortalApex100DetailsEngine.CheckFireSwordCharging(1000, 11000);
        Assert.True(act1);
        Assert.Equal(10, sec1);

        var (act2, _) = OfficialImmortalApex100DetailsEngine.CheckFireSwordCharging(1000, 11001);
        Assert.False(act2);
    }

    [Fact]
    public void TestDetail2124_DoubleFireSwordCombo()
    {
        var (combo1, mul1) = OfficialImmortalApex100DetailsEngine.CheckDoubleFireSwordCombo(0, 8000, 8500);
        Assert.True(combo1);
        Assert.Equal(260, mul1);

        var (combo2, mul2) = OfficialImmortalApex100DetailsEngine.CheckDoubleFireSwordCombo(0, 7000, 7500);
        Assert.False(combo2);
        Assert.Equal(100, mul2);

        var (combo3, mul3) = OfficialImmortalApex100DetailsEngine.CheckDoubleFireSwordCombo(0, 8000, 9500);
        Assert.False(combo3);
        Assert.Equal(100, mul3);
    }

    [Fact]
    public void TestDetail2125_HalfMoonSwordDistribution()
    {
        var (main, sub, maxSub) = OfficialImmortalApex100DetailsEngine.GetHalfMoonSwordDamageDistribution();
        Assert.Equal(100, main);
        Assert.Equal(35, sub);
        Assert.Equal(3, maxSub);
    }

    [Fact]
    public void TestDetail2126_SavageThrustLevelPush()
    {
        var (push1, dist1, reb1, stun1) = OfficialImmortalApex100DetailsEngine.ResolveSavageThrustLevelPush(35, 30);
        Assert.True(push1);
        Assert.Equal(2, dist1);
        Assert.False(reb1);
        Assert.Equal(0, stun1);

        var (push2, dist2, reb2, stun2) = OfficialImmortalApex100DetailsEngine.ResolveSavageThrustLevelPush(30, 35);
        Assert.False(push2);
        Assert.Equal(0, dist2);
        Assert.True(reb2);
        Assert.Equal(1000, stun2);
    }

    [Fact]
    public void TestDetail2127_SavageThrustWallCollision()
    {
        var (hit1, ms1) = OfficialImmortalApex100DetailsEngine.CheckSavageThrustWallCollision(true);
        Assert.True(hit1);
        Assert.Equal(1500, ms1);

        var (hit2, ms2) = OfficialImmortalApex100DetailsEngine.CheckSavageThrustWallCollision(false);
        Assert.False(hit2);
        Assert.Equal(0, ms2);
    }

    [Fact]
    public void TestDetail2128_BasicSwordAccuracyBonus()
    {
        Assert.Equal(0, OfficialImmortalApex100DetailsEngine.GetBasicSwordAccuracyBonus(0));
        Assert.Equal(1, OfficialImmortalApex100DetailsEngine.GetBasicSwordAccuracyBonus(1));
        Assert.Equal(3, OfficialImmortalApex100DetailsEngine.GetBasicSwordAccuracyBonus(3));
        Assert.Equal(3, OfficialImmortalApex100DetailsEngine.GetBasicSwordAccuracyBonus(4));
    }

    [Fact]
    public void TestDetail2129_SlayingSwordStrike()
    {
        var (trig1, bonus1) = OfficialImmortalApex100DetailsEngine.CalculateSlayingSwordStrike(24, 40);
        Assert.True(trig1);
        Assert.Equal(15, bonus1); // 40 / 4 + 5 = 15

        var (trig2, bonus2) = OfficialImmortalApex100DetailsEngine.CalculateSlayingSwordStrike(25, 40);
        Assert.False(trig2);
        Assert.Equal(0, bonus2);
    }

    [Fact]
    public void TestDetail2130_AttackIntervalWithSpeed()
    {
        Assert.Equal(1050, OfficialImmortalApex100DetailsEngine.CalculateAttackIntervalMsWithSpeed(1100, 1));
        Assert.Equal(700, OfficialImmortalApex100DetailsEngine.CalculateAttackIntervalMsWithSpeed(1100, 10)); // 700 极限
    }

    // =========================================================================
    // 9. 法师诱惑随从叛变老死与抗拒火环等级推挤 (2131~2140)
    // =========================================================================

    [Fact]
    public void TestDetail2131_CanTameMorePets()
    {
        Assert.True(OfficialImmortalApex100DetailsEngine.CanTameMorePets(4));
        Assert.False(OfficialImmortalApex100DetailsEngine.CanTameMorePets(5));
    }

    [Fact]
    public void TestDetail2132_PetKillLevelUp()
    {
        var (lvl1, hp1, dc1) = OfficialImmortalApex100DetailsEngine.CheckPetKillLevelUp(0, 100);
        Assert.Equal(2, lvl1);
        Assert.Equal(40, hp1);
        Assert.Equal(30, dc1);

        var (lvl2, hp2, dc2) = OfficialImmortalApex100DetailsEngine.CheckPetKillLevelUp(0, 400);
        Assert.Equal(7, lvl2);
        Assert.Equal(140, hp2);
        Assert.Equal(105, dc2);
    }

    [Fact]
    public void TestDetail2133_PetNaturalDeath()
    {
        Assert.True(OfficialImmortalApex100DetailsEngine.CheckPetNaturalDeath(21600));
        Assert.False(OfficialImmortalApex100DetailsEngine.CheckPetNaturalDeath(21599));
    }

    [Fact]
    public void TestDetail2134_PetBetrayOnFriendlyFire()
    {
        var (b1, r1) = OfficialImmortalApex100DetailsEngine.CheckPetBetrayOnFriendlyFire(9, 100, true);
        Assert.True(b1);
        Assert.Equal(10, r1);

        var (b2, _) = OfficialImmortalApex100DetailsEngine.CheckPetBetrayOnFriendlyFire(10, 100, true);
        Assert.False(b2);

        var (b3, _) = OfficialImmortalApex100DetailsEngine.CheckPetBetrayOnFriendlyFire(5, 100, false);
        Assert.False(b3);
    }

    [Fact]
    public void TestDetail2135_RingOfFireLevelPush()
    {
        var (p1, d1) = OfficialImmortalApex100DetailsEngine.ResolveRingOfFirePush(35, 30, false);
        Assert.True(p1);
        Assert.Equal(2, d1);

        var (p2, _) = OfficialImmortalApex100DetailsEngine.ResolveRingOfFirePush(30, 35, false);
        Assert.False(p2);
    }

    [Fact]
    public void TestDetail2136_RingOfFireBossImmune()
    {
        var (p3, _) = OfficialImmortalApex100DetailsEngine.ResolveRingOfFirePush(35, 30, true);
        Assert.False(p3);
    }

    [Fact]
    public void TestDetail2137_HellLightningUndeadBonus()
    {
        var (mul1, stun1) = OfficialImmortalApex100DetailsEngine.ResolveHellLightningUndeadBonus(true);
        Assert.Equal(200, mul1);
        Assert.Equal(500, stun1);

        var (mul2, stun2) = OfficialImmortalApex100DetailsEngine.ResolveHellLightningUndeadBonus(false);
        Assert.Equal(100, mul2);
        Assert.Equal(0, stun2);
    }

    [Fact]
    public void TestDetail2138_IceStormAccuracyProfile()
    {
        var (ignoreEvade, size) = OfficialImmortalApex100DetailsEngine.GetIceStormAccuracyProfile();
        Assert.True(ignoreEvade);
        Assert.Equal(3, size);
    }

    [Fact]
    public void TestDetail2139_FireWallProperties()
    {
        var (dur, tick) = OfficialImmortalApex100DetailsEngine.CalculateFireWallProperties(10);
        Assert.Equal(50, dur);
        Assert.Equal(500, tick);
    }

    [Fact]
    public void TestDetail2140_LighteningBeamMultiplier()
    {
        Assert.Equal(100, OfficialImmortalApex100DetailsEngine.CalculateLighteningBeamDamageMultiplier(1));
        Assert.Equal(125, OfficialImmortalApex100DetailsEngine.CalculateLighteningBeamDamageMultiplier(8));
    }

    // =========================================================================
    // 10. Direct-Blit 协议弱网重传与 Native AOT 零分配压测 (2141~2150)
    // =========================================================================

    [Fact]
    public void TestDetail2141_DirectBlitMagicValidation()
    {
        Assert.True(OfficialImmortalApex100DetailsEngine.ValidateDirectBlitMagic(0x594D));
        Assert.False(OfficialImmortalApex100DetailsEngine.ValidateDirectBlitMagic(0x1234));
    }

    [Fact]
    public void TestDetail2142_SequenceSlidingWindow()
    {
        var (ok1, seq1) = OfficialImmortalApex100DetailsEngine.ValidateSequenceSlidingWindow(10, 5);
        Assert.True(ok1);
        Assert.Equal(10, seq1);

        var (ok2, _) = OfficialImmortalApex100DetailsEngine.ValidateSequenceSlidingWindow(70, 5);
        Assert.False(ok2);

        var (ok3, _) = OfficialImmortalApex100DetailsEngine.ValidateSequenceSlidingWindow(4, 5);
        Assert.False(ok3);
    }

    [Fact]
    public void TestDetail2143_ExponentialBackoffTimeout()
    {
        Assert.Equal(200, OfficialImmortalApex100DetailsEngine.CalculateExponentialBackoffTimeout(0));
        Assert.Equal(400, OfficialImmortalApex100DetailsEngine.CalculateExponentialBackoffTimeout(1));
        Assert.Equal(800, OfficialImmortalApex100DetailsEngine.CalculateExponentialBackoffTimeout(2));
        Assert.Equal(1600, OfficialImmortalApex100DetailsEngine.CalculateExponentialBackoffTimeout(3));
        Assert.Equal(3200, OfficialImmortalApex100DetailsEngine.CalculateExponentialBackoffTimeout(4));
        Assert.Equal(3200, OfficialImmortalApex100DetailsEngine.CalculateExponentialBackoffTimeout(10));
    }

    [Fact]
    public void TestDetail2144_ConsecutiveLossCircuitBreaker()
    {
        var (cut1, msg1) = OfficialImmortalApex100DetailsEngine.CheckConsecutiveLossCircuitBreaker(6);
        Assert.True(cut1);
        Assert.Contains("中断", msg1);

        var (cut2, _) = OfficialImmortalApex100DetailsEngine.CheckConsecutiveLossCircuitBreaker(5);
        Assert.False(cut2);
    }

    [Fact]
    public void TestDetail2145_ComputeFastCrc16()
    {
        byte[] data = new byte[] { 0x4D, 0x59, 0x01, 0x00 };
        ushort crc = OfficialImmortalApex100DetailsEngine.ComputeFastCrc16(data);
        Assert.NotEqual(0, crc);
    }

    [Fact]
    public void TestDetail2146_NativeAotBlitPackingAlignment()
    {
        Assert.True(OfficialImmortalApex100DetailsEngine.CheckNativeAotBlitPackingAlignment());
    }

    [Fact]
    public void TestDetail2147_HPSocketSendBufferCapacity()
    {
        var (cap, suf) = OfficialImmortalApex100DetailsEngine.GetHPSocketSendBufferCapacity();
        Assert.Equal(16384, cap);
        Assert.True(suf);
    }

    [Fact]
    public void TestDetail2148_HeartbeatLiveness()
    {
        var (to1, probe, dead) = OfficialImmortalApex100DetailsEngine.CheckHeartbeatLiveness(0, 60000);
        Assert.True(to1);
        Assert.Equal(30, probe);
        Assert.Equal(60, dead);

        var (to2, _, _) = OfficialImmortalApex100DetailsEngine.CheckHeartbeatLiveness(0, 59999);
        Assert.False(to2);
    }

    [Fact]
    public void TestDetail2149_DumbClientOverrideLocalState()
    {
        Assert.True(OfficialImmortalApex100DetailsEngine.ShouldDumbClientOverrideLocalState(true));
        Assert.False(OfficialImmortalApex100DetailsEngine.ShouldDumbClientOverrideLocalState(false));
    }

    [Fact]
    public void TestDetail2150_BenchmarkDirectBlitZeroAllocation()
    {
        long allocated = OfficialImmortalApex100DetailsEngine.BenchmarkDirectBlitZeroAllocation(10000);
        Assert.Equal(0, allocated);
    }
}
