using System;
using Mir2.Shared.Protocols.Modern;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 盛大官方 1.5 纯血极意破虚 100 微观细节测试矩阵 (Wave 100: Details 2151 ~ 2250)
/// 严格对齐官方 1.50《虎卫传说》封闭分母，彻底剔除封魔谷、结婚、师徒、声望、荣誉勋章等未来非标特性。
/// </summary>
public sealed class TestOfficialWave100_100DetailsApexMatrix
{
    // =========================================================================
    // 1. 触龙神与蜈蚣洞死亡棺材深处微观生态 (2151~2160)
    // =========================================================================

    [Fact]
    public void TestDetail2151_TouchDragonEmerge()
    {
        var (emerged1, audio1) = OfficialTranscendentApex100DetailsEngine.CheckTouchDragonEmerge(true, 3);
        Assert.True(emerged1);
        Assert.Equal("dragon_roar_emerge.wav", audio1);

        var (emerged2, _) = OfficialTranscendentApex100DetailsEngine.CheckTouchDragonEmerge(true, 4);
        Assert.False(emerged2);

        var (emerged3, _) = OfficialTranscendentApex100DetailsEngine.CheckTouchDragonEmerge(false, 2);
        Assert.False(emerged3);
    }

    [Fact]
    public void TestDetail2152_TouchDragonScreenPoison()
    {
        var (p1, dmg1, sec1) = OfficialTranscendentApex100DetailsEngine.CalculateTouchDragonScreenPoison(0);
        Assert.True(p1);
        Assert.Equal(10, dmg1);
        Assert.Equal(10, sec1);

        var (p2, dmg2, sec2) = OfficialTranscendentApex100DetailsEngine.CalculateTouchDragonScreenPoison(50);
        Assert.True(p2);
        Assert.Equal(5, dmg2);
        Assert.Equal(10, sec2);

        var (p3, dmg3, _) = OfficialTranscendentApex100DetailsEngine.CalculateTouchDragonScreenPoison(100);
        Assert.True(p3);
        Assert.Equal(2, dmg3); // 保底 2 点
    }

    [Fact]
    public void TestDetail2153_TouchDragonMagicReduction()
    {
        Assert.Equal(10, OfficialTranscendentApex100DetailsEngine.CalculateTouchDragonMagicReduction(100));
        Assert.Equal(5, OfficialTranscendentApex100DetailsEngine.CalculateTouchDragonMagicReduction(50));
        Assert.Equal(1, OfficialTranscendentApex100DetailsEngine.CalculateTouchDragonMagicReduction(5));
    }

    [Fact]
    public void TestDetail2154_TouchDragonDeathDropWeights()
    {
        var (weaponRate, zumaRate, radius) = OfficialTranscendentApex100DetailsEngine.GetTouchDragonDeathDropWeights();
        Assert.Equal(200, weaponRate);
        Assert.Equal(50, zumaRate);
        Assert.Equal(8, radius);
    }

    [Fact]
    public void TestDetail2155_DeathCoffinBossSpawnIntervalMs()
    {
        Assert.Equal(120 * 60 * 1000, OfficialTranscendentApex100DetailsEngine.GetDeathCoffinBossSpawnIntervalMs(0));
        Assert.Equal(130 * 60 * 1000, OfficialTranscendentApex100DetailsEngine.GetDeathCoffinBossSpawnIntervalMs(10));
        Assert.Equal(110 * 60 * 1000, OfficialTranscendentApex100DetailsEngine.GetDeathCoffinBossSpawnIntervalMs(-10));
    }

    [Fact]
    public void TestDetail2156_EvilCentipedeBerserk()
    {
        var (b1, speed1) = OfficialTranscendentApex100DetailsEngine.CheckEvilCentipedeBerserk(20, 100, 400);
        Assert.True(b1);
        Assert.Equal(300, speed1); // 400 - 100

        var (b2, speed2) = OfficialTranscendentApex100DetailsEngine.CheckEvilCentipedeBerserk(21, 100, 400);
        Assert.False(b2);
        Assert.Equal(400, speed2);
    }

    [Fact]
    public void TestDetail2157_BlackMaggotTame()
    {
        var (ok1, speed1, max1) = OfficialTranscendentApex100DetailsEngine.ValidateBlackMaggotTame(26, 4);
        Assert.True(ok1);
        Assert.Equal(200, speed1);
        Assert.Equal(5, max1);

        var (ok2, _, _) = OfficialTranscendentApex100DetailsEngine.ValidateBlackMaggotTame(25, 4);
        Assert.False(ok2);

        var (ok3, _, _) = OfficialTranscendentApex100DetailsEngine.ValidateBlackMaggotTame(26, 5);
        Assert.False(ok3);
    }

    [Fact]
    public void TestDetail2158_GiantWormSlimeSlow()
    {
        var (slow1, pct1, dur1) = OfficialTranscendentApex100DetailsEngine.CheckGiantWormSlimeSlow(5);
        Assert.True(slow1);
        Assert.Equal(20, pct1);
        Assert.Equal(3, dur1);

        var (slow2, _, _) = OfficialTranscendentApex100DetailsEngine.CheckGiantWormSlimeSlow(10);
        Assert.False(slow2);
    }

    [Fact]
    public void TestDetail2159_DeathCoffinTeleportScroll()
    {
        var (ok1, _) = OfficialTranscendentApex100DetailsEngine.ValidateDeathCoffinTeleportScroll("D140", 1);
        Assert.False(ok1);

        var (ok2, _) = OfficialTranscendentApex100DetailsEngine.ValidateDeathCoffinTeleportScroll("死亡棺材", 2);
        Assert.False(ok2);

        var (ok3, _) = OfficialTranscendentApex100DetailsEngine.ValidateDeathCoffinTeleportScroll("D140", 0);
        Assert.True(ok3);

        var (ok4, _) = OfficialTranscendentApex100DetailsEngine.ValidateDeathCoffinTeleportScroll("0", 1);
        Assert.True(ok4);
    }

    [Fact]
    public void TestDetail2160_LineOfSkyOneWayPassage()
    {
        var (ok1, _) = OfficialTranscendentApex100DetailsEngine.ValidateLineOfSkyOneWayPassage(50, 52);
        Assert.True(ok1);

        var (ok2, reason2) = OfficialTranscendentApex100DetailsEngine.ValidateLineOfSkyOneWayPassage(52, 50);
        Assert.False(ok2);
        Assert.Contains("无法由东向西逆向攀爬", reason2);
    }

    // =========================================================================
    // 2. 白日门天尊道馆与虎卫堂/鹰卫微观生态 (2161~2170) [纯血官方 1.5 专属]
    // =========================================================================

    [Fact]
    public void TestDetail2161_HuWeiSpawnTimer()
    {
        var (ready1, rem1) = OfficialTranscendentApex100DetailsEngine.CheckHuWeiSpawnTimer(0, 7200000);
        Assert.True(ready1);
        Assert.Equal(0, rem1);

        var (ready2, rem2) = OfficialTranscendentApex100DetailsEngine.CheckHuWeiSpawnTimer(0, 7190000);
        Assert.False(ready2);
        Assert.Equal(10000, rem2);
    }

    [Fact]
    public void TestDetail2162_AttemptTameHuWei()
    {
        var (tame1, hp1) = OfficialTranscendentApex100DetailsEngine.AttemptTameHuWei(26, 4, 19);
        Assert.True(tame1);
        Assert.Equal(400, hp1);

        var (tame2, _) = OfficialTranscendentApex100DetailsEngine.AttemptTameHuWei(25, 4, 10);
        Assert.False(tame2);

        var (tame3, _) = OfficialTranscendentApex100DetailsEngine.AttemptTameHuWei(26, 5, 10);
        Assert.False(tame3);

        var (tame4, _) = OfficialTranscendentApex100DetailsEngine.AttemptTameHuWei(26, 4, 20);
        Assert.False(tame4);
    }

    [Fact]
    public void TestDetail2163_HuWeiRank7Attributes()
    {
        var (dcMin, dcMax, hp) = OfficialTranscendentApex100DetailsEngine.CalculateHuWeiRank7Attributes();
        Assert.Equal(45, dcMin);
        Assert.Equal(90, dcMax);
        Assert.Equal(3200, hp);
    }

    [Fact]
    public void TestDetail2164_YingWeiRandomSpawn()
    {
        var (sp1, map1, rng1) = OfficialTranscendentApex100DetailsEngine.CheckYingWeiRandomSpawn(14);
        Assert.True(sp1);
        Assert.Equal("D024", map1);
        Assert.Equal(7, rng1);

        var (sp2, _, _) = OfficialTranscendentApex100DetailsEngine.CheckYingWeiRandomSpawn(15);
        Assert.False(sp2);
    }

    [Fact]
    public void TestDetail2165_YingWeiCloseCombatRetreat()
    {
        var (ret1, dist1) = OfficialTranscendentApex100DetailsEngine.CheckYingWeiCloseCombatRetreat(1);
        Assert.True(ret1);
        Assert.Equal(3, dist1);

        var (ret2, dist2) = OfficialTranscendentApex100DetailsEngine.CheckYingWeiCloseCombatRetreat(2);
        Assert.False(ret2);
        Assert.Equal(2, dist2);
    }

    [Fact]
    public void TestDetail2166_HuWeiHallDoorAccess()
    {
        var (ok1, _) = OfficialTranscendentApex100DetailsEngine.ValidateHuWeiHallDoorAccess(25);
        Assert.True(ok1);

        var (ok2, err2) = OfficialTranscendentApex100DetailsEngine.ValidateHuWeiHallDoorAccess(24);
        Assert.False(ok2);
        Assert.Contains("25 级", err2);
    }

    [Fact]
    public void TestDetail2167_CannibalPlantGather()
    {
        var (p1, type1) = OfficialTranscendentApex100DetailsEngine.CheckCannibalPlantGather(101);
        Assert.True(p1);
        Assert.Equal("灰毒原料", type1);

        var (p2, type2) = OfficialTranscendentApex100DetailsEngine.CheckCannibalPlantGather(102);
        Assert.True(p2);
        Assert.Equal("黄毒原料", type2);

        var (p3, _) = OfficialTranscendentApex100DetailsEngine.CheckCannibalPlantGather(103);
        Assert.False(p3);
    }

    [Fact]
    public void TestDetail2168_TamedHuWeiRebellion()
    {
        var (reb1, mult1) = OfficialTranscendentApex100DetailsEngine.CheckTamedHuWeiRebellion(0, 21600000);
        Assert.True(reb1);
        Assert.Equal(1.5f, mult1);

        var (reb2, mult2) = OfficialTranscendentApex100DetailsEngine.CheckTamedHuWeiRebellion(0, 21599999);
        Assert.False(reb2);
        Assert.Equal(1.0f, mult2);
    }

    [Fact]
    public void TestDetail2169_YingWeiArrowStiff()
    {
        var (stiff1, dur1) = OfficialTranscendentApex100DetailsEngine.CheckYingWeiArrowStiff(14);
        Assert.True(stiff1);
        Assert.Equal(400, dur1);

        var (stiff2, _) = OfficialTranscendentApex100DetailsEngine.CheckYingWeiArrowStiff(15);
        Assert.False(stiff2);
    }

    [Fact]
    public void TestDetail2170_BairimenWolfAggro()
    {
        var (ag1, rng1) = OfficialTranscendentApex100DetailsEngine.CheckBairimenWolfAggro(9);
        Assert.True(ag1);
        Assert.Equal(9, rng1);

        var (ag2, _) = OfficialTranscendentApex100DetailsEngine.CheckBairimenWolfAggro(10);
        Assert.False(ag2);
    }

    // =========================================================================
    // 3. 赤月祭坛双头金刚割肉挖取与双头血魔大爆 (2171~2180)
    // =========================================================================

    [Fact]
    public void TestDetail2171_DoubleHeadKongButcher()
    {
        var (ok1, _) = OfficialTranscendentApex100DetailsEngine.ValidateDoubleHeadKongButcher(
            true, 1, 10000, 20000, "战士A", "战士A");
        Assert.True(ok1);

        var (ok2, err2) = OfficialTranscendentApex100DetailsEngine.ValidateDoubleHeadKongButcher(
            true, 1, 10000, 20000, "战士A", "路人B");
        Assert.False(ok2);
        Assert.Contains("60 秒独占挖掘权", err2);

        var (ok3, _) = OfficialTranscendentApex100DetailsEngine.ValidateDoubleHeadKongButcher(
            true, 1, 10000, 75000, "战士A", "路人B");
        Assert.True(ok3);

        var (ok4, _) = OfficialTranscendentApex100DetailsEngine.ValidateDoubleHeadKongButcher(
            false, 1, 10000, 20000, "战士A", "战士A");
        Assert.False(ok4);
    }

    [Fact]
    public void TestDetail2172_RollDoubleHeadKongGodItem()
    {
        var (got1, item1) = OfficialTranscendentApex100DetailsEngine.RollDoubleHeadKongGodItem(1);
        Assert.True(got1);
        Assert.Contains("法神", item1);

        var (got2, item2) = OfficialTranscendentApex100DetailsEngine.RollDoubleHeadKongGodItem(2);
        Assert.True(got2);
        Assert.Equal("天尊项链", item2);

        var (got3, _) = OfficialTranscendentApex100DetailsEngine.RollDoubleHeadKongGodItem(3);
        Assert.False(got3);
    }

    [Fact]
    public void TestDetail2173_DoubleHeadDemonBurstRadiusAndRate()
    {
        var (radius, rate) = OfficialTranscendentApex100DetailsEngine.GetDoubleHeadDemonBurstRadiusAndRate();
        Assert.Equal(8, radius);
        Assert.Equal(30, rate);
    }

    [Fact]
    public void TestDetail2174_DoubleHeadDemonSelfHeal()
    {
        var (h1, val1) = OfficialTranscendentApex100DetailsEngine.CheckDoubleHeadDemonSelfHeal(1000, 5000, false);
        Assert.True(h1);
        Assert.Equal(1000, val1);

        var (h2, _) = OfficialTranscendentApex100DetailsEngine.CheckDoubleHeadDemonSelfHeal(1001, 5000, false);
        Assert.False(h2);

        var (h3, _) = OfficialTranscendentApex100DetailsEngine.CheckDoubleHeadDemonSelfHeal(800, 5000, true);
        Assert.False(h3);
    }

    [Fact]
    public void TestDetail2175_AltarNarrowPathBlocked()
    {
        Assert.True(OfficialTranscendentApex100DetailsEngine.CheckAltarNarrowPathBlocked(2, 1));
        Assert.False(OfficialTranscendentApex100DetailsEngine.CheckAltarNarrowPathBlocked(1, 1));
        Assert.False(OfficialTranscendentApex100DetailsEngine.CheckAltarNarrowPathBlocked(2, 2));
    }

    [Fact]
    public void TestDetail2176_DoubleHeadKongSpikeReflect()
    {
        Assert.Equal(15, OfficialTranscendentApex100DetailsEngine.CalculateDoubleHeadKongSpikeReflect(100));
        Assert.Equal(7, OfficialTranscendentApex100DetailsEngine.CalculateDoubleHeadKongSpikeReflect(50));
        Assert.Equal(1, OfficialTranscendentApex100DetailsEngine.CalculateDoubleHeadKongSpikeReflect(5));
    }

    [Fact]
    public void TestDetail2177_DoubleHeadDemonStunHit()
    {
        var (s1, dur1) = OfficialTranscendentApex100DetailsEngine.CheckDoubleHeadDemonStunHit(19);
        Assert.True(s1);
        Assert.Equal(1000, dur1);

        var (s2, _) = OfficialTranscendentApex100DetailsEngine.CheckDoubleHeadDemonStunHit(20);
        Assert.False(s2);
    }

    [Fact]
    public void TestDetail2178_AltarDeathRespawnPoint()
    {
        var (map, x, y) = OfficialTranscendentApex100DetailsEngine.GetAltarDeathRespawnPoint();
        Assert.Equal("D022", map);
        Assert.Equal(178, x);
        Assert.Equal(325, y);
    }

    [Fact]
    public void TestDetail2179_RedMoonTwinBossSpawnIntervalHours()
    {
        Assert.Equal(6, OfficialTranscendentApex100DetailsEngine.GetRedMoonTwinBossSpawnIntervalHours());
    }

    [Fact]
    public void TestDetail2180_AltarEntrancePassage()
    {
        var (ok1, _) = OfficialTranscendentApex100DetailsEngine.ValidateAltarEntrancePassage(20, 18, 9);
        Assert.True(ok1);

        var (ok2, err2) = OfficialTranscendentApex100DetailsEngine.ValidateAltarEntrancePassage(20, 18, 10);
        Assert.False(ok2);
        Assert.Contains("排队进入", err2);

        var (ok3, _) = OfficialTranscendentApex100DetailsEngine.ValidateAltarEntrancePassage(21, 18, 5);
        Assert.False(ok3);
    }

    // =========================================================================
    // 4. 沙巴克攻城战皇宫占领与城门修复攻防实证 (2181~2190)
    // =========================================================================

    [Fact]
    public void TestDetail2181_SabukGateDamage()
    {
        var (rem1, col1) = OfficialTranscendentApex100DetailsEngine.CalculateSabukGateDamage(50000, 100, 20);
        Assert.Equal(49920, rem1);
        Assert.False(col1);

        var (rem2, col2) = OfficialTranscendentApex100DetailsEngine.CalculateSabukGateDamage(50, 100, 0);
        Assert.Equal(0, rem2);
        Assert.True(col2);
    }

    [Fact]
    public void TestDetail2182_SabukGateCollapsed()
    {
        Assert.True(OfficialTranscendentApex100DetailsEngine.CheckSabukGateCollapsed(0));
        Assert.False(OfficialTranscendentApex100DetailsEngine.CheckSabukGateCollapsed(1));
    }

    [Fact]
    public void TestDetail2183_SabukGateRepair()
    {
        var (ok1, _, cost1, min1) = OfficialTranscendentApex100DetailsEngine.ValidateSabukGateRepair(true, 200000, 50, 15);
        Assert.True(ok1);
        Assert.Equal(200000, cost1);
        Assert.Equal(5, min1);

        var (ok2, _, _, _) = OfficialTranscendentApex100DetailsEngine.ValidateSabukGateRepair(false, 200000, 50, 15);
        Assert.False(ok2);

        var (ok3, _, _, _) = OfficialTranscendentApex100DetailsEngine.ValidateSabukGateRepair(true, 199999, 50, 15);
        Assert.False(ok3);

        var (ok4, _, _, _) = OfficialTranscendentApex100DetailsEngine.ValidateSabukGateRepair(true, 200000, 49, 15);
        Assert.False(ok4);
    }

    [Fact]
    public void TestDetail2184_CastleWarActive()
    {
        Assert.True(OfficialTranscendentApex100DetailsEngine.IsCastleWarActive(20, 0));
        Assert.True(OfficialTranscendentApex100DetailsEngine.IsCastleWarActive(22, 59));
        Assert.True(OfficialTranscendentApex100DetailsEngine.IsCastleWarActive(23, 0));
        Assert.False(OfficialTranscendentApex100DetailsEngine.IsCastleWarActive(19, 59));
        Assert.False(OfficialTranscendentApex100DetailsEngine.IsCastleWarActive(23, 1));
    }

    [Fact]
    public void TestDetail2185_SabukPvpPkPoints()
    {
        Assert.Equal(0, OfficialTranscendentApex100DetailsEngine.CalculateSabukPvpPkPoints(true, "3"));
        Assert.Equal(0, OfficialTranscendentApex100DetailsEngine.CalculateSabukPvpPkPoints(true, "0150"));
        Assert.Equal(100, OfficialTranscendentApex100DetailsEngine.CalculateSabukPvpPkPoints(false, "3"));
        Assert.Equal(100, OfficialTranscendentApex100DetailsEngine.CalculateSabukPvpPkPoints(true, "0"));
    }

    [Fact]
    public void TestDetail2186_PalaceSovereigntyChange()
    {
        var (ok1, guild1) = OfficialTranscendentApex100DetailsEngine.CheckPalaceSovereigntyChange("布拉格", 10);
        Assert.True(ok1);
        Assert.Equal("布拉格", guild1);

        var (ok2, _) = OfficialTranscendentApex100DetailsEngine.CheckPalaceSovereigntyChange("布拉格", 9);
        Assert.False(ok2);

        var (ok3, _) = OfficialTranscendentApex100DetailsEngine.CheckPalaceSovereigntyChange(string.Empty, 12);
        Assert.False(ok3);
    }

    [Fact]
    public void TestDetail2187_DailySabukTaxWithdrawal()
    {
        var (ok1, gold1) = OfficialTranscendentApex100DetailsEngine.CalculateDailySabukTaxWithdrawal(5000000, 1000000, 500000);
        Assert.True(ok1);
        Assert.Equal(1000000, gold1);

        var (ok2, gold2) = OfficialTranscendentApex100DetailsEngine.CalculateDailySabukTaxWithdrawal(5000000, 2000000, 1500000);
        Assert.True(ok2);
        Assert.Equal(500000, gold2);

        var (ok3, _) = OfficialTranscendentApex100DetailsEngine.CalculateDailySabukTaxWithdrawal(5000000, 100000, 2000000);
        Assert.False(ok3);
    }

    [Fact]
    public void TestDetail2188_SabukMemberDiscount()
    {
        Assert.Equal(80, OfficialTranscendentApex100DetailsEngine.GetSabukMemberDiscount(true));
        Assert.Equal(100, OfficialTranscendentApex100DetailsEngine.GetSabukMemberDiscount(false));
    }

    [Fact]
    public void TestDetail2189_SabukWarRespawnLocation()
    {
        var (mapD, xD, yD) = OfficialTranscendentApex100DetailsEngine.GetSabukWarRespawnLocation(true);
        Assert.Equal("3", mapD);
        Assert.Equal(644, xD);
        Assert.Equal(290, yD);

        var (mapA, xA, yA) = OfficialTranscendentApex100DetailsEngine.GetSabukWarRespawnLocation(false);
        Assert.Equal("3", mapA);
        Assert.Equal(335, xA);
        Assert.Equal(330, yA);
    }

    [Fact]
    public void TestDetail2190_FinalizeSabukWarSettlement()
    {
        var (end1, g1) = OfficialTranscendentApex100DetailsEngine.FinalizeSabukWarSettlement(23, "龙腾盛世");
        Assert.True(end1);
        Assert.Equal("龙腾盛世", g1);

        var (end2, _) = OfficialTranscendentApex100DetailsEngine.FinalizeSabukWarSettlement(22, "龙腾盛世");
        Assert.False(end2);
    }

    // =========================================================================
    // 5. 武器升级持久波动与极品跳点权威方程 (2191~2200)
    // =========================================================================

    [Fact]
    public void TestDetail2191_BlackIronOreDurabilityDelta()
    {
        Assert.Equal(-3, OfficialTranscendentApex100DetailsEngine.CalculateBlackIronOreDurabilityDelta(29, 3, 0));
        Assert.Equal(-1, OfficialTranscendentApex100DetailsEngine.CalculateBlackIronOreDurabilityDelta(55, 4, 1));
        Assert.Equal(0, OfficialTranscendentApex100DetailsEngine.CalculateBlackIronOreDurabilityDelta(75, 4, 2));
        Assert.Equal(1, OfficialTranscendentApex100DetailsEngine.CalculateBlackIronOreDurabilityDelta(95, 5, 2));
        Assert.Equal(2, OfficialTranscendentApex100DetailsEngine.CalculateBlackIronOreDurabilityDelta(125, 5, 5));
    }

    [Fact]
    public void TestDetail2192_WeaponUpgradeSuccessRate()
    {
        Assert.Equal(50, OfficialTranscendentApex100DetailsEngine.CalculateWeaponUpgradeSuccessRate(10, 10));
        Assert.Equal(65, OfficialTranscendentApex100DetailsEngine.CalculateWeaponUpgradeSuccessRate(15, 10));
        Assert.Equal(85, OfficialTranscendentApex100DetailsEngine.CalculateWeaponUpgradeSuccessRate(30, 10));
        Assert.Equal(40, OfficialTranscendentApex100DetailsEngine.CalculateWeaponUpgradeSuccessRate(8, 10));
    }

    [Fact]
    public void TestDetail2193_WeaponUpgradeJumpPoint()
    {
        var (jump1, bonus1) = OfficialTranscendentApex100DetailsEngine.CheckWeaponUpgradeJumpPoint(30, 10, 4);
        Assert.True(jump1);
        Assert.Equal(2, bonus1);

        var (jump2, bonus2) = OfficialTranscendentApex100DetailsEngine.CheckWeaponUpgradeJumpPoint(30, 10, 5);
        Assert.False(jump2);
        Assert.Equal(1, bonus2);

        var (jump3, bonus3) = OfficialTranscendentApex100DetailsEngine.CheckWeaponUpgradeJumpPoint(29, 10, 0);
        Assert.False(jump3);
        Assert.Equal(1, bonus3);
    }

    [Fact]
    public void TestDetail2194_WeaponUpgradeLimit()
    {
        var (ok1, _) = OfficialTranscendentApex100DetailsEngine.ValidateWeaponUpgradeLimit(6);
        Assert.True(ok1);

        var (ok2, err2) = OfficialTranscendentApex100DetailsEngine.ValidateWeaponUpgradeLimit(7);
        Assert.False(ok2);
        Assert.Contains("7 次", err2);
    }

    [Fact]
    public void TestDetail2195_WeaponTrialTimeout()
    {
        Assert.False(OfficialTranscendentApex100DetailsEngine.CheckWeaponTrialTimeout(1000, 600000));
        Assert.True(OfficialTranscendentApex100DetailsEngine.CheckWeaponTrialTimeout(1000, 602000));
    }

    [Fact]
    public void TestDetail2196_SlashGuardShatterRisk()
    {
        Assert.Equal(25, OfficialTranscendentApex100DetailsEngine.CalculateSlashGuardShatterRisk(true, 10));
        Assert.Equal(10, OfficialTranscendentApex100DetailsEngine.CalculateSlashGuardShatterRisk(false, 10));
    }

    [Fact]
    public void TestDetail2197_UpgradeSuccessFeedback()
    {
        var (ms, audio) = OfficialTranscendentApex100DetailsEngine.GetUpgradeSuccessFeedback();
        Assert.Equal(300, ms);
        Assert.Equal("weapon_upgrade_success.wav", audio);
    }

    [Fact]
    public void TestDetail2198_ProcessUpgradeFailure()
    {
        int slot = 1;
        Assert.True(OfficialTranscendentApex100DetailsEngine.ProcessUpgradeFailure(ref slot));
        Assert.Equal(0, slot);
    }

    [Fact]
    public void TestDetail2199_Luck9MaxDamage()
    {
        Assert.Equal(30, OfficialTranscendentApex100DetailsEngine.CalculateLuck9MaxDamage(9, 10, 30));
        Assert.Equal(30, OfficialTranscendentApex100DetailsEngine.CalculateLuck9MaxDamage(10, 10, 30));
        Assert.Equal(20, OfficialTranscendentApex100DetailsEngine.CalculateLuck9MaxDamage(8, 10, 30));
    }

    [Fact]
    public void TestDetail2200_Curse10MinDamage()
    {
        Assert.Equal(10, OfficialTranscendentApex100DetailsEngine.CalculateCurse10MinDamage(10, 10, 30));
        Assert.Equal(10, OfficialTranscendentApex100DetailsEngine.CalculateCurse10MinDamage(11, 10, 30));
        Assert.Equal(20, OfficialTranscendentApex100DetailsEngine.CalculateCurse10MinDamage(9, 10, 30));
    }

    // =========================================================================
    // 6. 未知暗殿真假教主与命运之刃锻造任务 (2201~2210) [纯血官方 1.5 专属]
    // =========================================================================

    [Fact]
    public void TestDetail2201_EvaluateDarkHallFakeBoss()
    {
        var (isFake1, real1, fake1) = OfficialTranscendentApex100DetailsEngine.EvaluateDarkHallFakeBoss("沃玛教主");
        Assert.True(isFake1);
        Assert.Equal(2200, real1);
        Assert.Equal(100, fake1);

        var (isFake2, real2, fake2) = OfficialTranscendentApex100DetailsEngine.EvaluateDarkHallFakeBoss("祖玛教主");
        Assert.True(isFake2);
        Assert.Equal(3000, real2);
        Assert.Equal(150, fake2);

        var (isFake3, _, _) = OfficialTranscendentApex100DetailsEngine.EvaluateDarkHallFakeBoss("稻草人");
        Assert.False(isFake3);
    }

    [Fact]
    public void TestDetail2202_UseHolyWaterOnMysticEquip()
    {
        var (c1, msg1) = OfficialTranscendentApex100DetailsEngine.UseHolyWaterOnMysticEquip(true);
        Assert.True(c1);
        Assert.Contains("允许脱卸", msg1);

        var (c2, msg2) = OfficialTranscendentApex100DetailsEngine.UseHolyWaterOnMysticEquip(false);
        Assert.False(c2);
        Assert.Contains("无需使用", msg2);
    }

    [Fact]
    public void TestDetail2203_ValidateDarkHallGiantBeetleTame()
    {
        var (ok1, hp1) = OfficialTranscendentApex100DetailsEngine.ValidateDarkHallGiantBeetleTame(26);
        Assert.True(ok1);
        Assert.Equal(900, hp1);

        var (ok2, _) = OfficialTranscendentApex100DetailsEngine.ValidateDarkHallGiantBeetleTame(25);
        Assert.False(ok2);
    }

    [Fact]
    public void TestDetail2204_ValidateFateSwordQuestLevel()
    {
        var (ok1, _) = OfficialTranscendentApex100DetailsEngine.ValidateFateSwordQuestLevel(35);
        Assert.True(ok1);

        var (ok2, reason2) = OfficialTranscendentApex100DetailsEngine.ValidateFateSwordQuestLevel(34);
        Assert.False(ok2);
        Assert.Contains("35 级", reason2);
    }

    [Fact]
    public void TestDetail2205_ValidateFateSwordIronOres()
    {
        var (ok1, total1) = OfficialTranscendentApex100DetailsEngine.ValidateFateSwordIronOres(3, 15);
        Assert.True(ok1);
        Assert.Equal(45, total1);

        var (ok2, _) = OfficialTranscendentApex100DetailsEngine.ValidateFateSwordIronOres(2, 16);
        Assert.False(ok2);

        var (ok3, _) = OfficialTranscendentApex100DetailsEngine.ValidateFateSwordIronOres(3, 14);
        Assert.False(ok3);
    }

    [Fact]
    public void TestDetail2206_PlayCasinoDiceGame()
    {
        // 押大：4+5+6 = 15 大，获胜
        var (won1, gold1) = OfficialTranscendentApex100DetailsEngine.PlayCasinoDiceGame(4, 5, 6, true, 1000);
        Assert.True(won1);
        Assert.Equal(2000, gold1);

        // 押小：1+2+3 = 6 小，获胜
        var (won2, gold2) = OfficialTranscendentApex100DetailsEngine.PlayCasinoDiceGame(1, 2, 3, false, 1000);
        Assert.True(won2);
        Assert.Equal(2000, gold2);

        // 围骰：4+4+4 = 庄家通杀
        var (won3, _) = OfficialTranscendentApex100DetailsEngine.PlayCasinoDiceGame(4, 4, 4, true, 1000);
        Assert.False(won3);
    }

    [Fact]
    public void TestDetail2207_GetFateSwordBaseStats()
    {
        var (min, max, weight) = OfficialTranscendentApex100DetailsEngine.GetFateSwordBaseStats();
        Assert.Equal(12, min);
        Assert.Equal(16, max);
        Assert.Equal(48, weight);
    }

    [Fact]
    public void TestDetail2208_CalculateFateSwordArmorPierce()
    {
        var (hit, dmg) = OfficialTranscendentApex100DetailsEngine.CalculateFateSwordArmorPierce(20, 10);
        Assert.True(hit);
        Assert.Equal(15, dmg); // 20 - (10 / 2) = 15
    }

    [Fact]
    public void TestDetail2209_SubmitDeerBloodForFateSword()
    {
        var (ok1, msg1) = OfficialTranscendentApex100DetailsEngine.SubmitDeerBloodForFateSword(true);
        Assert.True(ok1);
        Assert.Contains("极品鹿血", msg1);

        var (ok2, _) = OfficialTranscendentApex100DetailsEngine.SubmitDeerBloodForFateSword(false);
        Assert.False(ok2);
    }

    [Fact]
    public void TestDetail2210_FinalizeFateSwordForging()
    {
        var (ok1, name1) = OfficialTranscendentApex100DetailsEngine.FinalizeFateSwordForging(true);
        Assert.True(ok1);
        Assert.Equal("命运之刃", name1);

        var (ok2, _) = OfficialTranscendentApex100DetailsEngine.FinalizeFateSwordForging(false);
        Assert.False(ok2);
    }

    // =========================================================================
    // 7. 道士神兽防御与喷火形态及变异骷髅进化 (2211~2220)
    // =========================================================================

    [Fact]
    public void TestDetail2211_DivineBeastDefenseStance()
    {
        var (ac1, mac1) = OfficialTranscendentApex100DetailsEngine.CalculateDivineBeastDefenseStance(50, 40, true);
        Assert.Equal(75, ac1);
        Assert.Equal(60, mac1);

        var (ac2, mac2) = OfficialTranscendentApex100DetailsEngine.CalculateDivineBeastDefenseStance(50, 40, false);
        Assert.Equal(50, ac2);
        Assert.Equal(40, mac2);
    }

    [Fact]
    public void TestDetail2212_DivineBeastRestRegen()
    {
        Assert.Equal(1100, OfficialTranscendentApex100DetailsEngine.CalculateDivineBeastRestRegen(1000, 2000, true));
        Assert.Equal(2000, OfficialTranscendentApex100DetailsEngine.CalculateDivineBeastRestRegen(1950, 2000, true));
        Assert.Equal(1000, OfficialTranscendentApex100DetailsEngine.CalculateDivineBeastRestRegen(1000, 2000, false));
    }

    [Fact]
    public void TestDetail2213_DivineBeastFirePenetration()
    {
        var (main, pierce) = OfficialTranscendentApex100DetailsEngine.CalculateDivineBeastFirePenetration(100);
        Assert.Equal(100, main);
        Assert.Equal(50, pierce);
    }

    [Fact]
    public void TestDetail2214_DivineBeastMaxHpByLevel()
    {
        Assert.Equal(500, OfficialTranscendentApex100DetailsEngine.GetDivineBeastMaxHpByLevel(0));
        Assert.Equal(1400, OfficialTranscendentApex100DetailsEngine.GetDivineBeastMaxHpByLevel(3));
        Assert.Equal(2600, OfficialTranscendentApex100DetailsEngine.GetDivineBeastMaxHpByLevel(7));
    }

    [Fact]
    public void TestDetail2215_SkeletonEvolutionVisualAndSpeed()
    {
        var (axe1, col1, spd1) = OfficialTranscendentApex100DetailsEngine.GetSkeletonEvolutionVisualAndSpeed(1);
        Assert.Equal("木斧", axe1);
        Assert.Equal("黄色", col1);
        Assert.Equal(650, spd1);

        var (axe2, col2, spd2) = OfficialTranscendentApex100DetailsEngine.GetSkeletonEvolutionVisualAndSpeed(4);
        Assert.Equal("铁斧", axe2);
        Assert.Equal("蓝色", col2);
        Assert.Equal(500, spd2);

        var (axe3, col3, spd3) = OfficialTranscendentApex100DetailsEngine.GetSkeletonEvolutionVisualAndSpeed(7);
        Assert.Equal("大板斧", axe3);
        Assert.Equal("深蓝", col3);
        Assert.Equal(350, spd3);
    }

    [Fact]
    public void TestDetail2216_PetHealingBonus()
    {
        Assert.Equal(130, OfficialTranscendentApex100DetailsEngine.CalculatePetHealingBonus(100));
        Assert.Equal(65, OfficialTranscendentApex100DetailsEngine.CalculatePetHealingBonus(50));
    }

    [Fact]
    public void TestDetail2217_PetAttackWhiteNamePkRule()
    {
        var (isY, reason) = OfficialTranscendentApex100DetailsEngine.CheckPetAttackWhiteNamePkRule(true);
        Assert.False(isY);
        Assert.Contains("不连带", reason);
    }

    [Fact]
    public void TestDetail2218_PetCrossMapFollow()
    {
        var (f1, delay1) = OfficialTranscendentApex100DetailsEngine.CheckPetCrossMapFollow(16, false);
        Assert.True(f1);
        Assert.Equal(3000, delay1);

        var (f2, delay2) = OfficialTranscendentApex100DetailsEngine.CheckPetCrossMapFollow(2, true);
        Assert.True(f2);
        Assert.Equal(3000, delay2);

        var (f3, _) = OfficialTranscendentApex100DetailsEngine.CheckPetCrossMapFollow(5, false);
        Assert.False(f3);
    }

    [Fact]
    public void TestDetail2219_PetPrayerRebellion()
    {
        Assert.True(OfficialTranscendentApex100DetailsEngine.CheckPetPrayerRebellion(true));
        Assert.False(OfficialTranscendentApex100DetailsEngine.CheckPetPrayerRebellion(false));
    }

    [Fact]
    public void TestDetail2220_TamedPetLureFreeze()
    {
        var (frz1, dur1) = OfficialTranscendentApex100DetailsEngine.CheckTamedPetLureFreeze(true);
        Assert.True(frz1);
        Assert.Equal(15000, dur1);

        var (frz2, _) = OfficialTranscendentApex100DetailsEngine.CheckTamedPetLureFreeze(false);
        Assert.False(frz2);
    }

    // =========================================================================
    // 8. 战士隔位刺杀破盾与双烈火极限连击 (2221~2230)
    // =========================================================================

    [Fact]
    public void TestDetail2221_AssassinationDamage()
    {
        var (dmg1, bypass1) = OfficialTranscendentApex100DetailsEngine.CalculateAssassinationDamage(100, 1, 20, false);
        Assert.Equal(80, dmg1);
        Assert.False(bypass1);

        var (dmg2, bypass2) = OfficialTranscendentApex100DetailsEngine.CalculateAssassinationDamage(100, 1, 20, true);
        Assert.Equal(32, dmg2);
        Assert.False(bypass2);

        var (dmg3, bypass3) = OfficialTranscendentApex100DetailsEngine.CalculateAssassinationDamage(100, 2, 50, true);
        Assert.Equal(100, dmg3);
        Assert.True(bypass3);
    }

    [Fact]
    public void TestDetail2222_FireSwordSpiritTimeout()
    {
        Assert.False(OfficialTranscendentApex100DetailsEngine.CheckFireSwordSpiritTimeout(1000, 11000));
        Assert.True(OfficialTranscendentApex100DetailsEngine.CheckFireSwordSpiritTimeout(1000, 11001));
    }

    [Fact]
    public void TestDetail2223_DoubleFireSwordCombo()
    {
        var (ok1, mult1) = OfficialTranscendentApex100DetailsEngine.CheckDoubleFireSwordCombo(1000, 9500);
        Assert.True(ok1);
        Assert.Equal(2.6f, mult1);

        var (ok2, _) = OfficialTranscendentApex100DetailsEngine.CheckDoubleFireSwordCombo(1000, 8500);
        Assert.False(ok2);

        var (ok3, _) = OfficialTranscendentApex100DetailsEngine.CheckDoubleFireSwordCombo(1000, 12000);
        Assert.False(ok3);
    }

    [Fact]
    public void TestDetail2224_HalfMoonSweepDamage()
    {
        Assert.Equal(100, OfficialTranscendentApex100DetailsEngine.CalculateHalfMoonSweepDamage(100, true));
        Assert.Equal(35, OfficialTranscendentApex100DetailsEngine.CalculateHalfMoonSweepDamage(100, false));
    }

    [Fact]
    public void TestDetail2225_WildChargeOverpower()
    {
        var (p1, tiles1, stun1) = OfficialTranscendentApex100DetailsEngine.EvaluateWildChargeOverpower(40, 38);
        Assert.True(p1);
        Assert.Equal(2, tiles1);
        Assert.Equal(1000, stun1);

        var (p2, _, _) = OfficialTranscendentApex100DetailsEngine.EvaluateWildChargeOverpower(40, 40);
        Assert.False(p2);
    }

    [Fact]
    public void TestDetail2226_WildChargeRebound()
    {
        var (r1, tiles1, stun1) = OfficialTranscendentApex100DetailsEngine.EvaluateWildChargeRebound(40, 42);
        Assert.True(r1);
        Assert.Equal(1, tiles1);
        Assert.Equal(1000, stun1);

        var (r2, _, _) = OfficialTranscendentApex100DetailsEngine.EvaluateWildChargeRebound(40, 40);
        Assert.True(r2);

        var (r3, _, _) = OfficialTranscendentApex100DetailsEngine.EvaluateWildChargeRebound(42, 40);
        Assert.False(r3);
    }

    [Fact]
    public void TestDetail2227_WildChargeWallCrash()
    {
        var (dmg, stun) = OfficialTranscendentApex100DetailsEngine.CalculateWildChargeWallCrash();
        Assert.Equal(15, dmg);
        Assert.Equal(1500, stun);
    }

    [Fact]
    public void TestDetail2228_BasicSwordAccuracyBonus()
    {
        Assert.Equal(0, OfficialTranscendentApex100DetailsEngine.GetBasicSwordAccuracyBonus(0));
        Assert.Equal(1, OfficialTranscendentApex100DetailsEngine.GetBasicSwordAccuracyBonus(1));
        Assert.Equal(3, OfficialTranscendentApex100DetailsEngine.GetBasicSwordAccuracyBonus(3));
    }

    [Fact]
    public void TestDetail2229_AttackSlayingCrit()
    {
        var (crit1, dmg1) = OfficialTranscendentApex100DetailsEngine.CalculateAttackSlayingCrit(30, 24);
        Assert.True(crit1);
        Assert.Equal(38, dmg1);

        var (crit2, dmg2) = OfficialTranscendentApex100DetailsEngine.CalculateAttackSlayingCrit(30, 25);
        Assert.False(crit2);
        Assert.Equal(30, dmg2);
    }

    [Fact]
    public void TestDetail2230_SwingIntervalWithSpeed()
    {
        Assert.Equal(950, OfficialTranscendentApex100DetailsEngine.CalculateSwingIntervalWithSpeed(1100, 3));
        Assert.Equal(700, OfficialTranscendentApex100DetailsEngine.CalculateSwingIntervalWithSpeed(1100, 10));
    }

    // =========================================================================
    // 9. 法师诱惑随从叛变老死与地狱雷光瘫痪 (2231~2240)
    // =========================================================================

    [Fact]
    public void TestDetail2231_LurePetMaxLimit()
    {
        var (ok1, _) = OfficialTranscendentApex100DetailsEngine.ValidateLurePetMaxLimit(4);
        Assert.True(ok1);

        var (ok2, err2) = OfficialTranscendentApex100DetailsEngine.ValidateLurePetMaxLimit(5);
        Assert.False(ok2);
        Assert.Contains("5 只", err2);
    }

    [Fact]
    public void TestDetail2232_LurePetLevelUp()
    {
        var (up1, lvl1) = OfficialTranscendentApex100DetailsEngine.CheckLurePetLevelUp(50, 3);
        Assert.True(up1);
        Assert.Equal(4, lvl1);

        var (up2, lvl2) = OfficialTranscendentApex100DetailsEngine.CheckLurePetLevelUp(49, 3);
        Assert.False(up2);
        Assert.Equal(3, lvl2);

        var (up3, lvl3) = OfficialTranscendentApex100DetailsEngine.CheckLurePetLevelUp(100, 7);
        Assert.False(up3);
        Assert.Equal(7, lvl3);
    }

    [Fact]
    public void TestDetail2233_LurePetNaturalDeath()
    {
        Assert.True(OfficialTranscendentApex100DetailsEngine.CheckLurePetNaturalDeath(0, 21600000));
        Assert.False(OfficialTranscendentApex100DetailsEngine.CheckLurePetNaturalDeath(0, 21599999));
    }

    [Fact]
    public void TestDetail2234_LurePetLowHpRebellion()
    {
        var (reb1, _) = OfficialTranscendentApex100DetailsEngine.CheckLurePetLowHpRebellion(10, 100, true, 9);
        Assert.True(reb1);

        var (reb2, _) = OfficialTranscendentApex100DetailsEngine.CheckLurePetLowHpRebellion(11, 100, true, 5);
        Assert.False(reb2);

        var (reb3, _) = OfficialTranscendentApex100DetailsEngine.CheckLurePetLowHpRebellion(10, 100, false, 5);
        Assert.False(reb3);

        var (reb4, _) = OfficialTranscendentApex100DetailsEngine.CheckLurePetLowHpRebellion(10, 100, true, 10);
        Assert.False(reb4);
    }

    [Fact]
    public void TestDetail2235_RepulsionRingResistance()
    {
        var (res1, msg1) = OfficialTranscendentApex100DetailsEngine.CheckRepulsionRingResistance(35, 40, true);
        Assert.True(res1);
        Assert.Contains("首领霸体", msg1);

        var (res2, _) = OfficialTranscendentApex100DetailsEngine.CheckRepulsionRingResistance(35, 35, false);
        Assert.True(res2);

        var (res3, _) = OfficialTranscendentApex100DetailsEngine.CheckRepulsionRingResistance(35, 34, false);
        Assert.False(res3);
    }

    [Fact]
    public void TestDetail2236_HellLightningDamage()
    {
        var (dmg1, stun1) = OfficialTranscendentApex100DetailsEngine.CalculateHellLightningDamage(50, true);
        Assert.Equal(100, dmg1);
        Assert.Equal(500, stun1);

        var (dmg2, stun2) = OfficialTranscendentApex100DetailsEngine.CalculateHellLightningDamage(50, false);
        Assert.Equal(50, dmg2);
        Assert.Equal(0, stun2);
    }

    [Fact]
    public void TestDetail2237_BlizzardMagicEvadeBypass()
    {
        Assert.True(OfficialTranscendentApex100DetailsEngine.CheckBlizzardMagicEvadeBypass());
    }

    [Fact]
    public void TestDetail2238_FireWallDurationSeconds()
    {
        Assert.Equal(50, OfficialTranscendentApex100DetailsEngine.CalculateFireWallDurationSeconds(10));
        Assert.Equal(70, OfficialTranscendentApex100DetailsEngine.CalculateFireWallDurationSeconds(20));
    }

    [Fact]
    public void TestDetail2239_LightningBeamDistanceBonus()
    {
        Assert.Equal(100, OfficialTranscendentApex100DetailsEngine.CalculateLightningBeamDistanceBonus(100, 1));
        Assert.Equal(110, OfficialTranscendentApex100DetailsEngine.CalculateLightningBeamDistanceBonus(100, 3));
        Assert.Equal(125, OfficialTranscendentApex100DetailsEngine.CalculateLightningBeamDistanceBonus(100, 8));
        Assert.Equal(125, OfficialTranscendentApex100DetailsEngine.CalculateLightningBeamDistanceBonus(100, 10));
    }

    [Fact]
    public void TestDetail2240_HolyCurseTurnUndeadSuccess()
    {
        var (kill1, rate1) = OfficialTranscendentApex100DetailsEngine.CheckHolyCurseTurnUndeadSuccess(35, 30, true, false, 49);
        Assert.True(kill1);
        Assert.Equal(50, rate1);

        var (kill2, _) = OfficialTranscendentApex100DetailsEngine.CheckHolyCurseTurnUndeadSuccess(35, 30, true, false, 50);
        Assert.False(kill2);

        var (kill3, _) = OfficialTranscendentApex100DetailsEngine.CheckHolyCurseTurnUndeadSuccess(35, 35, true, false, 10);
        Assert.False(kill3);

        var (kill4, _) = OfficialTranscendentApex100DetailsEngine.CheckHolyCurseTurnUndeadSuccess(35, 30, true, true, 10);
        Assert.False(kill4);
    }

    // =========================================================================
    // 10. Direct-Blit 协议弱网重传与 Native AOT 零分配压测 (2241~2250)
    // =========================================================================

    [Fact]
    public void TestDetail2241_DirectBlitMagicHeader()
    {
        var (v1, _) = OfficialTranscendentApex100DetailsEngine.ValidateDirectBlitMagicHeader(0x594D, 128);
        Assert.True(v1);

        var (v2, _) = OfficialTranscendentApex100DetailsEngine.ValidateDirectBlitMagicHeader(0x1234, 128);
        Assert.False(v2);

        var (v3, _) = OfficialTranscendentApex100DetailsEngine.ValidateDirectBlitMagicHeader(0x594D, 4097);
        Assert.False(v3);
    }

    [Fact]
    public void TestDetail2242_SlidingWindowSequence()
    {
        var (a1, _) = OfficialTranscendentApex100DetailsEngine.ValidateSlidingWindowSequence(100, 100, 64);
        Assert.True(a1);

        var (a2, _) = OfficialTranscendentApex100DetailsEngine.ValidateSlidingWindowSequence(100, 163, 64);
        Assert.True(a2);

        var (a3, _) = OfficialTranscendentApex100DetailsEngine.ValidateSlidingWindowSequence(100, 99, 64);
        Assert.False(a3);

        var (a4, _) = OfficialTranscendentApex100DetailsEngine.ValidateSlidingWindowSequence(100, 164, 64);
        Assert.False(a4);
    }

    [Fact]
    public void TestDetail2243_ExponentialBackoffMs()
    {
        Assert.Equal(200, OfficialTranscendentApex100DetailsEngine.CalculateExponentialBackoffMs(0));
        Assert.Equal(400, OfficialTranscendentApex100DetailsEngine.CalculateExponentialBackoffMs(1));
        Assert.Equal(800, OfficialTranscendentApex100DetailsEngine.CalculateExponentialBackoffMs(2));
        Assert.Equal(1600, OfficialTranscendentApex100DetailsEngine.CalculateExponentialBackoffMs(3));
        Assert.Equal(3200, OfficialTranscendentApex100DetailsEngine.CalculateExponentialBackoffMs(4));
        Assert.Equal(3200, OfficialTranscendentApex100DetailsEngine.CalculateExponentialBackoffMs(5));
    }

    [Fact]
    public void TestDetail2244_NetworkDropCircuitBreaker()
    {
        var (b1, _) = OfficialTranscendentApex100DetailsEngine.CheckNetworkDropCircuitBreaker(5);
        Assert.True(b1);

        var (b2, _) = OfficialTranscendentApex100DetailsEngine.CheckNetworkDropCircuitBreaker(4);
        Assert.False(b2);
    }

    [Fact]
    public void TestDetail2245_DirectBlitCrc16ZeroAlloc()
    {
        byte[] payload = [0x01, 0x02, 0x03, 0x04];
        ushort crc1 = OfficialTranscendentApex100DetailsEngine.ComputeDirectBlitCrc16ZeroAlloc(payload);
        ushort crc2 = OfficialTranscendentApex100DetailsEngine.ComputeDirectBlitCrc16ZeroAlloc(payload);
        Assert.Equal(crc1, crc2);
        Assert.NotEqual(0, crc1);
    }

    [Fact]
    public void TestDetail2246_BlittablePacketAlignment()
    {
        Assert.True(OfficialTranscendentApex100DetailsEngine.ValidateBlittablePacketAlignment(8));
        Assert.True(OfficialTranscendentApex100DetailsEngine.ValidateBlittablePacketAlignment(16));
        Assert.True(OfficialTranscendentApex100DetailsEngine.ValidateBlittablePacketAlignment(64));
        Assert.False(OfficialTranscendentApex100DetailsEngine.ValidateBlittablePacketAlignment(10));
    }

    [Fact]
    public void TestDetail2247_RingBufferBoundary()
    {
        var (f1, rem1) = OfficialTranscendentApex100DetailsEngine.ValidateRingBufferBoundary(10000, 2000);
        Assert.True(f1);
        Assert.Equal(6384, rem1);

        var (f2, rem2) = OfficialTranscendentApex100DetailsEngine.ValidateRingBufferBoundary(15000, 2000);
        Assert.False(f2);
        Assert.Equal(1384, rem2);
    }

    [Fact]
    public void TestDetail2248_SessionHeartbeatTimeout()
    {
        var (to1, prb1) = OfficialTranscendentApex100DetailsEngine.CheckSessionHeartbeatTimeout(1000, 61000);
        Assert.True(to1);
        Assert.False(prb1);

        var (to2, prb2) = OfficialTranscendentApex100DetailsEngine.CheckSessionHeartbeatTimeout(1000, 32000);
        Assert.False(to2);
        Assert.True(prb2);

        var (to3, prb3) = OfficialTranscendentApex100DetailsEngine.CheckSessionHeartbeatTimeout(1000, 20000);
        Assert.False(to3);
        Assert.False(prb3);
    }

    [Fact]
    public void TestDetail2249_DumbClientAuthoritativeSync()
    {
        Assert.Equal(50, OfficialTranscendentApex100DetailsEngine.ApplyDumbClientAuthoritativeSync(100, 50));
        Assert.Equal(80, OfficialTranscendentApex100DetailsEngine.ApplyDumbClientAuthoritativeSync(0, 80));
    }

    [Fact]
    public void TestDetail2250_AotZeroAllocEncodeDecode()
    {
        var (iters, alloc) = OfficialTranscendentApex100DetailsEngine.SimulateAotZeroAllocEncodeDecode(10000);
        Assert.Equal(10000, iters);
        Assert.Equal(0, alloc);
    }
}
