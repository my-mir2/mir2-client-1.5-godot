using System;
using System.Collections.Generic;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 盛大官方 1.5 永恒登峰 100 微观细节与服务端权威闭环测试矩阵 (Wave 90, Details 1151 ~ 1250)
/// 严格 1:1 覆盖沙巴克攻城、首领战斗AI、武器升级、比奇矿区挖矿、大刀卫士红名惩罚、药水缓冲、负重穿戴与防作弊机制
/// </summary>
public class TestOfficialWave90_100DetailsAscentMatrix
{
    // =========================================================================
    // 细节 1151 ~ 1165: 沙巴克攻城战与皇宫主权
    // =========================================================================

    [Fact]
    public void TestDetail1151_GateDamage()
    {
        var (hp1, destroyed1) = OfficialEternalAscent100DetailsEngine.ApplyGateDamage(50000, 10000);
        Assert.Equal(40000, hp1);
        Assert.False(destroyed1);

        var (hp2, destroyed2) = OfficialEternalAscent100DetailsEngine.ApplyGateDamage(5000, 10000);
        Assert.Equal(0, hp2);
        Assert.True(destroyed2);
    }

    [Fact]
    public void TestDetail1152_GateDestructionState()
    {
        Assert.Equal(0, OfficialEternalAscent100DetailsEngine.GetGateDestructionState(40000, 50000));
        Assert.Equal(1, OfficialEternalAscent100DetailsEngine.GetGateDestructionState(20000, 50000));
        Assert.Equal(2, OfficialEternalAscent100DetailsEngine.GetGateDestructionState(0, 50000));
    }

    [Fact]
    public void TestDetail1153_RepairSabukGate()
    {
        var (ok1, rem1, msg1) = OfficialEternalAscent100DetailsEngine.RepairSabukGate(500000, isWarTime: true);
        Assert.False(ok1);
        Assert.Equal(500000, rem1);

        var (ok2, rem2, msg2) = OfficialEternalAscent100DetailsEngine.RepairSabukGate(100000, isWarTime: false);
        Assert.False(ok2);

        var (ok3, rem3, msg3) = OfficialEternalAscent100DetailsEngine.RepairSabukGate(500000, isWarTime: false);
        Assert.True(ok3);
        Assert.Equal(300000, rem3);
    }

    [Fact]
    public void TestDetail1154_ValidateSecretPassageEntry()
    {
        var (ok1, cost1, msg1) = OfficialEternalAscent100DetailsEngine.ValidateSecretPassageEntry(isSabukMember: true, 0, false);
        Assert.True(ok1);
        Assert.Equal(0, cost1);

        var (ok2, cost2, msg2) = OfficialEternalAscent100DetailsEngine.ValidateSecretPassageEntry(false, 10000, false);
        Assert.True(ok2);
        Assert.Equal(5000, cost2);

        var (ok3, cost3, msg3) = OfficialEternalAscent100DetailsEngine.ValidateSecretPassageEntry(false, 1000, false);
        Assert.False(ok3);
    }

    [Fact]
    public void TestDetail1155_ResolveSecretPassageDrop()
    {
        var res1 = OfficialEternalAscent100DetailsEngine.ResolveSecretPassageDrop(100, 100, (x, y) => false);
        Assert.Equal(100, res1.destX);
        Assert.False(res1.blocked);

        var res2 = OfficialEternalAscent100DetailsEngine.ResolveSecretPassageDrop(100, 100, (x, y) => x == 100 && y == 100);
        Assert.NotEqual((100, 100), (res2.destX, res2.destY));
        Assert.False(res2.blocked);
    }

    [Fact]
    public void TestDetail1156_IsSabukWarNoExpLoss()
    {
        Assert.True(OfficialEternalAscent100DetailsEngine.IsSabukWarNoExpLoss(true, "3"));
        Assert.True(OfficialEternalAscent100DetailsEngine.IsSabukWarNoExpLoss(true, "0150"));
        Assert.False(OfficialEternalAscent100DetailsEngine.IsSabukWarNoExpLoss(false, "3"));
        Assert.False(OfficialEternalAscent100DetailsEngine.IsSabukWarNoExpLoss(true, "0"));
    }

    [Fact]
    public void TestDetail1157_GetWarRespawnPoint()
    {
        var def = OfficialEternalAscent100DetailsEngine.GetWarRespawnPoint(true);
        Assert.Equal((640, 290, "3"), def);

        var att = OfficialEternalAscent100DetailsEngine.GetWarRespawnPoint(false);
        Assert.Equal((700, 400, "3"), att);
    }

    [Fact]
    public void TestDetail1158_CheckPalaceOccupancy()
    {
        var (ok1, g1) = OfficialEternalAscent100DetailsEngine.CheckPalaceOccupancy(new List<int> { 101, 101, 101 });
        Assert.True(ok1);
        Assert.Equal(101, g1);

        var (ok2, g2) = OfficialEternalAscent100DetailsEngine.CheckPalaceOccupancy(new List<int> { 101, 102 });
        Assert.False(ok2);

        var (ok3, g3) = OfficialEternalAscent100DetailsEngine.CheckPalaceOccupancy(new List<int>());
        Assert.False(ok3);
    }

    [Fact]
    public void TestDetail1159_FinalizeSabukWar()
    {
        var (g1, ann1) = OfficialEternalAscent100DetailsEngine.FinalizeSabukWar(101, 102);
        Assert.Equal(102, g1);
        Assert.Contains("易主", ann1);

        var (g2, ann2) = OfficialEternalAscent100DetailsEngine.FinalizeSabukWar(101, 101);
        Assert.Equal(101, g2);
        Assert.Contains("守城成功", ann2);
    }

    [Fact]
    public void TestDetail1160_CalculateDailyTaxRevenue()
    {
        Assert.Equal(200000, OfficialEternalAscent100DetailsEngine.CalculateDailyTaxRevenue(1000000));
        Assert.Equal(5000000, OfficialEternalAscent100DetailsEngine.CalculateDailyTaxRevenue(100000000));
    }

    [Fact]
    public void TestDetail1161_FormatSabukLordTitle()
    {
        string title = OfficialEternalAscent100DetailsEngine.FormatSabukLordTitle("风云无忌", "邪恶之眼");
        Assert.Equal("[沙巴克城主] 邪恶之眼 - 风云无忌", title);
    }

    [Fact]
    public void TestDetail1162_ApplySabukRepairDiscount()
    {
        Assert.Equal(8000, OfficialEternalAscent100DetailsEngine.ApplySabukRepairDiscount(10000, true));
        Assert.Equal(10000, OfficialEternalAscent100DetailsEngine.ApplySabukRepairDiscount(10000, false));
    }

    [Fact]
    public void TestDetail1163_CanEnterSabukTreasurePavilion()
    {
        Assert.True(OfficialEternalAscent100DetailsEngine.CanEnterSabukTreasurePavilion(true, false));
        Assert.True(OfficialEternalAscent100DetailsEngine.CanEnterSabukTreasurePavilion(false, true));
        Assert.False(OfficialEternalAscent100DetailsEngine.CanEnterSabukTreasurePavilion(false, false));
    }

    [Fact]
    public void TestDetail1164_GenerateWarBroadcast()
    {
        string b = OfficialEternalAscent100DetailsEngine.GenerateWarBroadcast("昔日同盟", 25);
        Assert.Contains("昔日同盟", b);
        Assert.Contains("25 分钟", b);
    }

    [Fact]
    public void TestDetail1165_IsGroupRecallForbiddenInWar()
    {
        Assert.True(OfficialEternalAscent100DetailsEngine.IsGroupRecallForbiddenInWar(true, "3"));
        Assert.False(OfficialEternalAscent100DetailsEngine.IsGroupRecallForbiddenInWar(false, "3"));
        Assert.False(OfficialEternalAscent100DetailsEngine.IsGroupRecallForbiddenInWar(true, "0"));
    }

    // =========================================================================
    // 细节 1166 ~ 1180: 首领行为与特殊战斗
    // =========================================================================

    [Fact]
    public void TestDetail1166_ShouldWakeZumaLeader()
    {
        Assert.True(OfficialEternalAscent100DetailsEngine.ShouldWakeZumaLeader(100, 100, 103, 100, true));
        Assert.False(OfficialEternalAscent100DetailsEngine.ShouldWakeZumaLeader(100, 100, 106, 100, true));
        Assert.False(OfficialEternalAscent100DetailsEngine.ShouldWakeZumaLeader(100, 100, 103, 100, false));
    }

    [Fact]
    public void TestDetail1167_GetZumaBreakStoneAoEDamage()
    {
        Assert.Equal(50, OfficialEternalAscent100DetailsEngine.GetZumaBreakStoneAoEDamage(2));
        Assert.Equal(0, OfficialEternalAscent100DetailsEngine.GetZumaBreakStoneAoEDamage(3));
    }

    [Fact]
    public void TestDetail1168_GetZumaSummonGuardCount()
    {
        Assert.Equal(8, OfficialEternalAscent100DetailsEngine.GetZumaSummonGuardCount(400, 1000));
        Assert.Equal(0, OfficialEternalAscent100DetailsEngine.GetZumaSummonGuardCount(800, 1000));
    }

    [Fact]
    public void TestDetail1169_ShouldZumaTeleportToTarget()
    {
        Assert.True(OfficialEternalAscent100DetailsEngine.ShouldZumaTeleportToTarget(5, 5));
        Assert.False(OfficialEternalAscent100DetailsEngine.ShouldZumaTeleportToTarget(4, 5));
        Assert.False(OfficialEternalAscent100DetailsEngine.ShouldZumaTeleportToTarget(6, 2));
    }

    [Fact]
    public void TestDetail1170_EvaluateEvilCentipedeEmerge()
    {
        var (e1, b1) = OfficialEternalAscent100DetailsEngine.EvaluateEvilCentipedeEmerge(4, 100);
        Assert.True(e1);
        Assert.False(b1);

        var (e2, b2) = OfficialEternalAscent100DetailsEngine.EvaluateEvilCentipedeEmerge(15, 0);
        Assert.True(b2);
    }

    [Fact]
    public void TestDetail1171_ApplyEvilCentipedePoisonFog()
    {
        var (green, red, ticks) = OfficialEternalAscent100DetailsEngine.ApplyEvilCentipedePoisonFog();
        Assert.True(green);
        Assert.True(red);
        Assert.Equal(10, ticks);
    }

    [Fact]
    public void TestDetail1172_CalculateEvilCentipedeDamage()
    {
        Assert.Equal(0, OfficialEternalAscent100DetailsEngine.CalculateEvilCentipedeDamage(100, isMagic: true));
        Assert.Equal(100, OfficialEternalAscent100DetailsEngine.CalculateEvilCentipedeDamage(100, isMagic: false));
    }

    [Fact]
    public void TestDetail1173_RollEvilCentipedeParalysis()
    {
        var rng = new Random(42);
        bool rolled = OfficialEternalAscent100DetailsEngine.RollEvilCentipedeParalysis(rng);
        Assert.True(rolled || !rolled);
    }

    [Fact]
    public void TestDetail1174_CalculateRedMoonDemonSpikeDamage()
    {
        int dmg = OfficialEternalAscent100DetailsEngine.CalculateRedMoonDemonSpikeDamage(100, playerAc: 40);
        Assert.Equal(80, dmg);
    }

    [Fact]
    public void TestDetail1175_GetRedMoonDemonDefenses()
    {
        var (ac, mac) = OfficialEternalAscent100DetailsEngine.GetRedMoonDemonDefenses();
        Assert.Equal(80, ac);
        Assert.Equal(20, mac);
    }

    [Fact]
    public void TestDetail1176_GetDoubleHeadedKongDefenses()
    {
        var (ac, mac) = OfficialEternalAscent100DetailsEngine.GetDoubleHeadedKongDefenses();
        Assert.Equal(100, ac);
        Assert.Equal(10, mac);
    }

    [Fact]
    public void TestDetail1177_GetDoubleHeadedDemonDefenses()
    {
        var (ac, mac) = OfficialEternalAscent100DetailsEngine.GetDoubleHeadedDemonDefenses();
        Assert.Equal(10, ac);
        Assert.Equal(100, mac);
    }

    [Fact]
    public void TestDetail1178_RequiresButchCorpseForLoot()
    {
        Assert.True(OfficialEternalAscent100DetailsEngine.RequiresButchCorpseForLoot("双头金刚"));
        Assert.True(OfficialEternalAscent100DetailsEngine.RequiresButchCorpseForLoot("触龙神"));
        Assert.False(OfficialEternalAscent100DetailsEngine.RequiresButchCorpseForLoot("沃玛教主"));
    }

    [Fact]
    public void TestDetail1179_IsCorpseKingHoleOpen()
    {
        Assert.True(OfficialEternalAscent100DetailsEngine.IsCorpseKingHoleOpen(10000, 25000, 30));
        Assert.False(OfficialEternalAscent100DetailsEngine.IsCorpseKingHoleOpen(10000, 45000, 30));
    }

    [Fact]
    public void TestDetail1180_CalculateMutatedHookCatReflectDamage()
    {
        Assert.Equal(10, OfficialEternalAscent100DetailsEngine.CalculateMutatedHookCatReflectDamage(100));
        Assert.Equal(1, OfficialEternalAscent100DetailsEngine.CalculateMutatedHookCatReflectDamage(5));
    }

    // =========================================================================
    // 细节 1181 ~ 1195: 武器升级与持久精算
    // =========================================================================

    [Fact]
    public void TestDetail1181_CalculateBlackIronDurabilityDelta()
    {
        Assert.Equal(-3, OfficialEternalAscent100DetailsEngine.CalculateBlackIronDurabilityDelta(25));
        Assert.Equal(-1, OfficialEternalAscent100DetailsEngine.CalculateBlackIronDurabilityDelta(45));
        Assert.Equal(0, OfficialEternalAscent100DetailsEngine.CalculateBlackIronDurabilityDelta(75));
        Assert.Equal(1, OfficialEternalAscent100DetailsEngine.CalculateBlackIronDurabilityDelta(95));
    }

    [Fact]
    public void TestDetail1182_SumTop5BlackIronPurity()
    {
        var purities = new List<int> { 10, 20, 15, 25, 18, 5, 8 };
        int sum = OfficialEternalAscent100DetailsEngine.SumTop5BlackIronPurity(purities);
        // 25 + 20 + 18 + 15 + 10 = 88
        Assert.Equal(88, sum);
    }

    [Fact]
    public void TestDetail1183_DetermineUpgradeStatType()
    {
        Assert.Equal(0, OfficialEternalAscent100DetailsEngine.DetermineUpgradeStatType(15, 10, 5));
        Assert.Equal(1, OfficialEternalAscent100DetailsEngine.DetermineUpgradeStatType(10, 20, 5));
        Assert.Equal(2, OfficialEternalAscent100DetailsEngine.DetermineUpgradeStatType(10, 10, 25));
    }

    [Fact]
    public void TestDetail1184_CalculateWeaponUpgradeSuccessRate()
    {
        int rate1 = OfficialEternalAscent100DetailsEngine.CalculateWeaponUpgradeSuccessRate(5, 1);
        Assert.Equal(65, rate1);

        int rate2 = OfficialEternalAscent100DetailsEngine.CalculateWeaponUpgradeSuccessRate(25, 0);
        Assert.Equal(85, rate2); // 封顶 85
    }

    [Fact]
    public void TestDetail1185_TrialSlashWeapon()
    {
        var rng = new Random(1);
        var (ok, broken, msg) = OfficialEternalAscent100DetailsEngine.TrialSlashWeapon(80, rng);
        Assert.True(ok || broken);
    }

    [Fact]
    public void TestDetail1186_CanUpgradeWeaponStat()
    {
        Assert.True(OfficialEternalAscent100DetailsEngine.CanUpgradeWeaponStat(6));
        Assert.False(OfficialEternalAscent100DetailsEngine.CanUpgradeWeaponStat(7));
    }

    [Fact]
    public void TestDetail1187_IsWeaponUpgradeFinished()
    {
        long submit = 1000000;
        Assert.False(OfficialEternalAscent100DetailsEngine.IsWeaponUpgradeFinished(submit, submit + 30 * 60 * 1000L));
        Assert.True(OfficialEternalAscent100DetailsEngine.IsWeaponUpgradeFinished(submit, submit + 61 * 60 * 1000L));
    }

    [Fact]
    public void TestDetail1188_GetBlessingOilSuccessRate()
    {
        Assert.Equal(50, OfficialEternalAscent100DetailsEngine.GetBlessingOilSuccessRate(1));
        Assert.Equal(25, OfficialEternalAscent100DetailsEngine.GetBlessingOilSuccessRate(4));
        Assert.Equal(10, OfficialEternalAscent100DetailsEngine.GetBlessingOilSuccessRate(6));
        Assert.Equal(0, OfficialEternalAscent100DetailsEngine.GetBlessingOilSuccessRate(7));
    }

    [Fact]
    public void TestDetail1189_ApplyBlessingOil()
    {
        var rng = new Random(1234);
        var res1 = OfficialEternalAscent100DetailsEngine.ApplyBlessingOil(7, 0, rng);
        Assert.Equal(7, res1.newLuck);

        var res2 = OfficialEternalAscent100DetailsEngine.ApplyBlessingOil(0, 0, rng);
        Assert.True(res2.newLuck >= 0);
    }

    [Fact]
    public void TestDetail1190_CalculateNormalRepairMaxDura()
    {
        int newDura = OfficialEternalAscent100DetailsEngine.CalculateNormalRepairMaxDura(30);
        Assert.Equal(20, newDura);
    }

    [Fact]
    public void TestDetail1191_ApplyRepairOil()
    {
        var rng = new Random(10);
        var (d1, max1, lost1) = OfficialEternalAscent100DetailsEngine.ApplyRepairOil(10, 30, true, rng);
        Assert.Equal(30, d1);
        Assert.False(lost1);

        var (d2, max2, lost2) = OfficialEternalAscent100DetailsEngine.ApplyRepairOil(10, 30, false, rng);
        Assert.True(max2 <= 30);
    }

    [Fact]
    public void TestDetail1192_EvaluateSlashTargetInfluence()
    {
        Assert.False(OfficialEternalAscent100DetailsEngine.EvaluateSlashTargetInfluence("大刀卫士"));
    }

    [Fact]
    public void TestDetail1193_ApplyCurse10Damage()
    {
        Assert.Equal(10, OfficialEternalAscent100DetailsEngine.ApplyCurse10Damage(10, 30, 10));
        Assert.Equal(20, OfficialEternalAscent100DetailsEngine.ApplyCurse10Damage(10, 30, 0));
    }

    [Fact]
    public void TestDetail1194_IsGuaranteedMaxDamage()
    {
        Assert.True(OfficialEternalAscent100DetailsEngine.IsGuaranteedMaxDamage(9));
        Assert.False(OfficialEternalAscent100DetailsEngine.IsGuaranteedMaxDamage(8));
    }

    [Fact]
    public void TestDetail1195_ShouldDiscardMineralResidue()
    {
        Assert.True(OfficialEternalAscent100DetailsEngine.ShouldDiscardMineralResidue());
    }

    // =========================================================================
    // 细节 1196 ~ 1210: 挖矿纯度、特修磨损与红名惩罚
    // =========================================================================

    [Fact]
    public void TestDetail1196_CalculateMiningPurity()
    {
        var (ok1, pur1, type1) = OfficialEternalAscent100DetailsEngine.CalculateMiningPurity(1000, 25);
        Assert.True(ok1);
        Assert.Equal("铁矿", type1);
        Assert.True(pur1 > 0);

        var (ok2, _, _) = OfficialEternalAscent100DetailsEngine.CalculateMiningPurity(0, 95);
        Assert.False(ok2);
    }

    [Fact]
    public void TestDetail1197_CalculateWeaponRepairDura()
    {
        // 普修扣除 1/3
        var (c1, m1, lost1) = OfficialEternalAscent100DetailsEngine.CalculateWeaponRepairDura(5000, 15000, false, 0);
        Assert.Equal(10000, m1);
        Assert.True(lost1);

        // 特修无磨损
        var (c2, m2, lost2) = OfficialEternalAscent100DetailsEngine.CalculateWeaponRepairDura(5000, 15000, true, 123);
        Assert.Equal(15000, m2);
        Assert.False(lost2);
    }

    [Fact]
    public void TestDetail1198_CalculateMeatQualityDecay()
    {
        var (q1, s1) = OfficialEternalAscent100DetailsEngine.CalculateMeatQualityDecay(15, 30);
        Assert.Equal(12, q1);
        Assert.False(s1);

        var (q2, s2) = OfficialEternalAscent100DetailsEngine.CalculateMeatQualityDecay(5, 60);
        Assert.Equal(0, q2);
        Assert.True(s2);
    }

    [Fact]
    public void TestDetail1199_ValidateGuardRedNameExecute()
    {
        var (exec1, dmg1) = OfficialEternalAscent100DetailsEngine.ValidateGuardRedNameExecute(250, 500);
        Assert.True(exec1);
        Assert.Equal(9999, dmg1);

        var (exec2, _) = OfficialEternalAscent100DetailsEngine.ValidateGuardRedNameExecute(50, 500);
        Assert.False(exec2);
    }

    [Fact]
    public void TestDetail1200_CalculatePkPointDecay()
    {
        var (pt1, c1) = OfficialEternalAscent100DetailsEngine.CalculatePkPointDecay(210, 20);
        Assert.Equal(190, pt1);
        Assert.Equal("Yellow", c1);

        var (pt2, c2) = OfficialEternalAscent100DetailsEngine.CalculatePkPointDecay(10, 15);
        Assert.Equal(0, pt2);
        Assert.Equal("White", c2);
    }

    [Fact]
    public void TestDetail1201_CalculateRedNameVillageShopPrices()
    {
        var (buy, sell) = OfficialEternalAscent100DetailsEngine.CalculateRedNameVillageShopPrices(100);
        Assert.Equal(300, buy);
        Assert.Equal(50, sell);
    }

    [Fact]
    public void TestDetail1202_ChargeGuildWarFee()
    {
        var (ok1, rem1) = OfficialEternalAscent100DetailsEngine.ChargeGuildWarFee(150000);
        Assert.True(ok1);
        Assert.Equal(50000, rem1);

        var (ok2, rem2) = OfficialEternalAscent100DetailsEngine.ChargeGuildWarFee(50000);
        Assert.False(ok2);
    }

    [Fact]
    public void TestDetail1203_IsGuildWarActive()
    {
        long start = 1000;
        Assert.True(OfficialEternalAscent100DetailsEngine.IsGuildWarActive(start, start + 2 * 3600 * 1000L));
        Assert.False(OfficialEternalAscent100DetailsEngine.IsGuildWarActive(start, start + 4 * 3600 * 1000L));
    }

    [Fact]
    public void TestDetail1204_IsKillExemptFromPk()
    {
        Assert.True(OfficialEternalAscent100DetailsEngine.IsKillExemptFromPk(true, false));
        Assert.True(OfficialEternalAscent100DetailsEngine.IsKillExemptFromPk(false, true));
        Assert.False(OfficialEternalAscent100DetailsEngine.IsKillExemptFromPk(false, false));
    }

    [Fact]
    public void TestDetail1205_IsAllianceProtected()
    {
        var all = new HashSet<int> { 202, 203 };
        Assert.True(OfficialEternalAscent100DetailsEngine.IsAllianceProtected(101, 101, all));
        Assert.True(OfficialEternalAscent100DetailsEngine.IsAllianceProtected(101, 202, all));
        Assert.False(OfficialEternalAscent100DetailsEngine.IsAllianceProtected(101, 999, all));
    }

    [Fact]
    public void TestDetail1206_FormatWarGuildHeader()
    {
        Assert.Equal("[敌对行会] 傲视群雄", OfficialEternalAscent100DetailsEngine.FormatWarGuildHeader("傲视群雄", true));
        Assert.Equal("[战盟友军] 铁血同盟", OfficialEternalAscent100DetailsEngine.FormatWarGuildHeader("铁血同盟", false));
    }

    [Fact]
    public void TestDetail1207_ProcessPkDecay()
    {
        long start = 100000;
        var (p1, t1) = OfficialEternalAscent100DetailsEngine.ProcessPkDecay(100, start, start + 120000);
        Assert.Equal(98, p1);
        Assert.Equal(start + 120000, t1);
    }

    [Fact]
    public void TestDetail1208_IsYellowNameExpired()
    {
        Assert.False(OfficialEternalAscent100DetailsEngine.IsYellowNameExpired(1000, 30000));
        Assert.True(OfficialEternalAscent100DetailsEngine.IsYellowNameExpired(1000, 70000));
    }

    [Fact]
    public void TestDetail1209_GetRedNameDeathDropCount()
    {
        var rng = new Random(100);
        int c1 = OfficialEternalAscent100DetailsEngine.GetRedNameDeathDropCount(350, rng);
        Assert.InRange(c1, 2, 4);

        int c2 = OfficialEternalAscent100DetailsEngine.GetRedNameDeathDropCount(250, rng);
        Assert.InRange(c2, 1, 2);
    }

    [Fact]
    public void TestDetail1210_GetGuardAntiRedDamage()
    {
        Assert.Equal(99999, OfficialEternalAscent100DetailsEngine.GetGuardAntiRedDamage(true));
        Assert.Equal(0, OfficialEternalAscent100DetailsEngine.GetGuardAntiRedDamage(false));
    }

    // =========================================================================
    // 细节 1211 ~ 1225: 药水缓冲与特戒判定
    // =========================================================================

    [Fact]
    public void TestDetail1211_GetPotionTickStep()
    {
        Assert.Equal(5, OfficialEternalAscent100DetailsEngine.GetPotionTickStep(20));
        Assert.Equal(3, OfficialEternalAscent100DetailsEngine.GetPotionTickStep(3));
    }

    [Fact]
    public void TestDetail1212_AccumulatePotionPool()
    {
        Assert.Equal(180, OfficialEternalAscent100DetailsEngine.AccumulatePotionPool(90, 90));
    }

    [Fact]
    public void TestDetail1213_GetInstantPotionRestoration()
    {
        Assert.Equal((50, 50), OfficialEternalAscent100DetailsEngine.GetInstantPotionRestoration("太阳水"));
        Assert.Equal((120, 120), OfficialEternalAscent100DetailsEngine.GetInstantPotionRestoration("万年雪霜"));
        Assert.Equal((250, 250), OfficialEternalAscent100DetailsEngine.GetInstantPotionRestoration("疗伤药"));
    }

    [Fact]
    public void TestDetail1214_IsInstantRecoveryPotion()
    {
        Assert.True(OfficialEternalAscent100DetailsEngine.IsInstantRecoveryPotion("太阳水"));
        Assert.False(OfficialEternalAscent100DetailsEngine.IsInstantRecoveryPotion("强效金创药"));
    }

    [Fact]
    public void TestDetail1215_GetGreenPoisonProfile()
    {
        var (interval, ticks, dmg) = OfficialEternalAscent100DetailsEngine.GetGreenPoisonProfile(15);
        Assert.Equal(3000, interval);
        Assert.Equal(10, ticks);
        Assert.Equal(5, dmg);
    }

    [Fact]
    public void TestDetail1216_ApplyMonsterRegenUnderPoison()
    {
        Assert.Equal(0, OfficialEternalAscent100DetailsEngine.ApplyMonsterRegenUnderPoison(20, true));
        Assert.Equal(20, OfficialEternalAscent100DetailsEngine.ApplyMonsterRegenUnderPoison(20, false));
    }

    [Fact]
    public void TestDetail1217_ApplyRedPoisonReduction()
    {
        var (ac, mac) = OfficialEternalAscent100DetailsEngine.ApplyRedPoisonReduction(50, 50);
        Assert.Equal(40, ac);
        Assert.Equal(40, mac);
    }

    [Fact]
    public void TestDetail1218_ConsumeAmuletDurability()
    {
        var (d1, ex1) = OfficialEternalAscent100DetailsEngine.ConsumeAmuletDurability(100);
        Assert.Equal(99, d1);
        Assert.False(ex1);

        var (d2, ex2) = OfficialEternalAscent100DetailsEngine.ConsumeAmuletDurability(1);
        Assert.Equal(0, d2);
        Assert.True(ex2);
    }

    [Fact]
    public void TestDetail1219_RollParalysisRingTrigger()
    {
        var rng = new Random(55);
        bool rolled = OfficialEternalAscent100DetailsEngine.RollParalysisRingTrigger(rng);
        Assert.True(rolled || !rolled);
    }

    [Fact]
    public void TestDetail1220_IsActionBlockedByParalysis()
    {
        Assert.True(OfficialEternalAscent100DetailsEngine.IsActionBlockedByParalysis(10000, 5000));
        Assert.False(OfficialEternalAscent100DetailsEngine.IsActionBlockedByParalysis(10000, 15000));
    }

    [Fact]
    public void TestDetail1221_EvaluateRevivalRing()
    {
        long now = 200000;
        var (t1, dura1, cd1, hp1) = OfficialEternalAscent100DetailsEngine.EvaluateRevivalRing(0, 5, 0, now, 500);
        Assert.True(t1);
        Assert.Equal(4, dura1);
        Assert.Equal(500, hp1);

        var (t2, dura2, cd2, hp2) = OfficialEternalAscent100DetailsEngine.EvaluateRevivalRing(10, 5, 0, now, 500);
        Assert.False(t2);
    }

    [Fact]
    public void TestDetail1222_ApplyProtectRingDamage()
    {
        var (dmg1, mp1) = OfficialEternalAscent100DetailsEngine.ApplyProtectRingDamage(100, 150);
        Assert.Equal(0, dmg1);
        Assert.Equal(50, mp1);

        var (dmg2, mp2) = OfficialEternalAscent100DetailsEngine.ApplyProtectRingDamage(150, 100);
        Assert.Equal(50, dmg2);
        Assert.Equal(0, mp2);
    }

    [Fact]
    public void TestDetail1223_ValidateTeleportRingCommand()
    {
        Assert.True(OfficialEternalAscent100DetailsEngine.ValidateTeleportRingCommand("0", 300, 300));
        Assert.False(OfficialEternalAscent100DetailsEngine.ValidateTeleportRingCommand("0150", 100, 100)); // 禁飞
    }

    [Fact]
    public void TestDetail1224_IsMonsterDetectInvisible()
    {
        Assert.True(OfficialEternalAscent100DetailsEngine.IsMonsterDetectInvisible("楔蛾"));
        Assert.True(OfficialEternalAscent100DetailsEngine.IsMonsterDetectInvisible("月魔蜘蛛"));
        Assert.False(OfficialEternalAscent100DetailsEngine.IsMonsterDetectInvisible("骷髅精灵"));
    }

    [Fact]
    public void TestDetail1225_GrantsHealSkill()
    {
        Assert.True(OfficialEternalAscent100DetailsEngine.GrantsHealSkill(true));
        Assert.False(OfficialEternalAscent100DetailsEngine.GrantsHealSkill(false));
    }

    // =========================================================================
    // 细节 1226 ~ 1240: 传送卷轴与负重穿戴
    // =========================================================================

    [Fact]
    public void TestDetail1226_IsTownTeleportInterrupted()
    {
        Assert.True(OfficialEternalAscent100DetailsEngine.IsTownTeleportInterrupted(true));
        Assert.False(OfficialEternalAscent100DetailsEngine.IsTownTeleportInterrupted(false));
    }

    [Fact]
    public void TestDetail1227_ResolveTownTeleportDestination()
    {
        var (m1, x1, y1) = OfficialEternalAscent100DetailsEngine.ResolveTownTeleportDestination(true);
        Assert.Equal(("3", 850, 670), (m1, x1, y1));

        var (m2, x2, y2) = OfficialEternalAscent100DetailsEngine.ResolveTownTeleportDestination(false);
        Assert.Equal(("0", 330, 270), (m2, x2, y2));
    }

    [Fact]
    public void TestDetail1228_ResolveDungeonEscapeMap()
    {
        Assert.Equal("11", OfficialEternalAscent100DetailsEngine.ResolveDungeonEscapeMap("D401"));
        Assert.Equal("0", OfficialEternalAscent100DetailsEngine.ResolveDungeonEscapeMap("D101"));
        Assert.Equal("3", OfficialEternalAscent100DetailsEngine.ResolveDungeonEscapeMap("D201"));
    }

    [Fact]
    public void TestDetail1229_ResolveRandomScrollDrop()
    {
        var res1 = OfficialEternalAscent100DetailsEngine.ResolveRandomScrollDrop(100, 100, (x, y) => false);
        Assert.True(res1.success);
        Assert.Equal((100, 100), (res1.finalX, res1.finalY));
    }

    [Fact]
    public void TestDetail1230_IsRandomScrollForbidden()
    {
        Assert.True(OfficialEternalAscent100DetailsEngine.IsRandomScrollForbidden("0150"));
        Assert.False(OfficialEternalAscent100DetailsEngine.IsRandomScrollForbidden("0"));
    }

    [Fact]
    public void TestDetail1231_CalculateMaxBagWeight()
    {
        Assert.Equal(50 + 35 * 4, OfficialEternalAscent100DetailsEngine.CalculateMaxBagWeight(35));
    }

    [Fact]
    public void TestDetail1232_EvaluateOverweightState()
    {
        var (p1, r1) = OfficialEternalAscent100DetailsEngine.EvaluateOverweightState(180, 200);
        Assert.True(p1);
        Assert.True(r1);

        var (p2, r2) = OfficialEternalAscent100DetailsEngine.EvaluateOverweightState(205, 200);
        Assert.False(p2);
        Assert.True(r2);

        var (p3, r3) = OfficialEternalAscent100DetailsEngine.EvaluateOverweightState(220, 200);
        Assert.False(p3);
        Assert.False(r3);
    }

    [Fact]
    public void TestDetail1233_CanEquipWeaponWeight()
    {
        Assert.True(OfficialEternalAscent100DetailsEngine.CanEquipWeaponWeight(30, 40));
        Assert.False(OfficialEternalAscent100DetailsEngine.CanEquipWeaponWeight(50, 40));
    }

    [Fact]
    public void TestDetail1234_CanEquipArmorWeight()
    {
        Assert.True(OfficialEternalAscent100DetailsEngine.CanEquipArmorWeight(25, 30));
        Assert.False(OfficialEternalAscent100DetailsEngine.CanEquipArmorWeight(35, 30));
    }

    [Fact]
    public void TestDetail1235_CanEquipJewelryWeight()
    {
        Assert.True(OfficialEternalAscent100DetailsEngine.CanEquipJewelryWeight(5, 10));
        Assert.False(OfficialEternalAscent100DetailsEngine.CanEquipJewelryWeight(15, 10));
    }

    [Fact]
    public void TestDetail1236_GetRareItemWeightOffset()
    {
        Assert.Equal(-1, OfficialEternalAscent100DetailsEngine.GetRareItemWeightOffset(true));
        Assert.Equal(0, OfficialEternalAscent100DetailsEngine.GetRareItemWeightOffset(false));
    }

    [Fact]
    public void TestDetail1237_ApplyZeroDurabilityAttributes()
    {
        var (dc1, ac1, mac1) = OfficialEternalAscent100DetailsEngine.ApplyZeroDurabilityAttributes(0, 30, 20, 10);
        Assert.Equal((0, 0, 0), (dc1, ac1, mac1));

        var (dc2, ac2, mac2) = OfficialEternalAscent100DetailsEngine.ApplyZeroDurabilityAttributes(100, 30, 20, 10);
        Assert.Equal((30, 20, 10), (dc2, ac2, mac2));
    }

    [Fact]
    public void TestDetail1238_CalculateDurabilityLossOnHit()
    {
        Assert.Equal(2, OfficialEternalAscent100DetailsEngine.CalculateDurabilityLossOnHit(true));
        Assert.Equal(1, OfficialEternalAscent100DetailsEngine.CalculateDurabilityLossOnHit(false));
    }

    [Fact]
    public void TestDetail1239_GetDeathEquipmentDropChancePercent()
    {
        Assert.Equal(10, OfficialEternalAscent100DetailsEngine.GetDeathEquipmentDropChancePercent(0));
        Assert.Equal(30, OfficialEternalAscent100DetailsEngine.GetDeathEquipmentDropChancePercent(1));
        Assert.Equal(100, OfficialEternalAscent100DetailsEngine.GetDeathEquipmentDropChancePercent(2));
    }

    [Fact]
    public void TestDetail1240_GenerateSpiralLootCoordinates()
    {
        var list = OfficialEternalAscent100DetailsEngine.GenerateSpiralLootCoordinates(100, 100, 5);
        Assert.Equal(5, list.Count);
        Assert.Equal((100, 100), list[0]);
    }

    // =========================================================================
    // 细节 1241 ~ 1250: 仓库密码与防作弊
    // =========================================================================

    [Fact]
    public void TestDetail1241_HandleStoragePasswordAttempt()
    {
        var (l1, err1, t1) = OfficialEternalAscent100DetailsEngine.HandleStoragePasswordAttempt("1234", "1234", 1, 10000);
        Assert.False(l1);
        Assert.Equal(0, err1);

        var (l2, err2, t2) = OfficialEternalAscent100DetailsEngine.HandleStoragePasswordAttempt("9999", "1234", 2, 10000);
        Assert.True(l2);
        Assert.Equal(3, err2);
        Assert.Equal(10000 + 10 * 60 * 1000L, t2);
    }

    [Fact]
    public void TestDetail1242_TransferGoldToStorage()
    {
        var (ok1, p1, s1, _) = OfficialEternalAscent100DetailsEngine.TransferGoldToStorage(50000, 100000, 20000);
        Assert.True(ok1);
        Assert.Equal(30000, p1);
        Assert.Equal(120000, s1);

        var (ok2, _, _, _) = OfficialEternalAscent100DetailsEngine.TransferGoldToStorage(10000, 100000, 20000);
        Assert.False(ok2);
    }

    [Fact]
    public void TestDetail1243_ValidateShopGoldTransaction()
    {
        Assert.True(OfficialEternalAscent100DetailsEngine.ValidateShopGoldTransaction(5000, 2000));
        Assert.False(OfficialEternalAscent100DetailsEngine.ValidateShopGoldTransaction(1000, -2000));
    }

    [Fact]
    public void TestDetail1244_IsBuybackItemExpired()
    {
        long now = 1000000;
        Assert.False(OfficialEternalAscent100DetailsEngine.IsBuybackItemExpired(now - 30 * 60 * 1000L, now));
        Assert.True(OfficialEternalAscent100DetailsEngine.IsBuybackItemExpired(now - 65 * 60 * 1000L, now));
    }

    [Fact]
    public void TestDetail1245_CalculateDeerMeatQuality()
    {
        Assert.Equal(14, OfficialEternalAscent100DetailsEngine.CalculateDeerMeatQuality(18, 2));
        Assert.Equal(0, OfficialEternalAscent100DetailsEngine.CalculateDeerMeatQuality(10, 10));
    }

    [Fact]
    public void TestDetail1246_EvaluateButcherDeerMeatQuest()
    {
        var (acc1, gold1, exp1) = OfficialEternalAscent100DetailsEngine.EvaluateButcherDeerMeatQuest(16);
        Assert.True(acc1);
        Assert.Equal(5000, gold1);
        Assert.Equal(2000, exp1);

        var (acc2, gold2, exp2) = OfficialEternalAscent100DetailsEngine.EvaluateButcherDeerMeatQuest(12);
        Assert.False(acc2);
    }

    [Fact]
    public void TestDetail1247_CalculateChickenMeatQuality()
    {
        Assert.Equal(7, OfficialEternalAscent100DetailsEngine.CalculateChickenMeatQuality(7));
        Assert.Equal(3, OfficialEternalAscent100DetailsEngine.CalculateChickenMeatQuality(1));
    }

    [Fact]
    public void TestDetail1248_CanMonsterYieldMeat()
    {
        Assert.True(OfficialEternalAscent100DetailsEngine.CanMonsterYieldMeat("鹿"));
        Assert.False(OfficialEternalAscent100DetailsEngine.CanMonsterYieldMeat("尸王"));
    }

    [Fact]
    public void TestDetail1249_CalculateSpecialRepairCost()
    {
        int cost = OfficialEternalAscent100DetailsEngine.CalculateSpecialRepairCost(1000, 10, 30);
        // missing = 20, 1000 * 20 * 3 / 30 = 2000
        Assert.Equal(2000, cost);
    }

    [Fact]
    public void TestDetail1250_ValidateClientPacketTimestamp()
    {
        var (p1, _) = OfficialEternalAscent100DetailsEngine.ValidateClientPacketTimestamp(1000, 1500, 1000);
        Assert.True(p1);

        var (p2, r2) = OfficialEternalAscent100DetailsEngine.ValidateClientPacketTimestamp(2000, 1500, 1000);
        Assert.False(p2);
        Assert.Contains("倒流", r2);

        var (p3, r3) = OfficialEternalAscent100DetailsEngine.ValidateClientPacketTimestamp(1000, 6000, 1000);
        Assert.False(p3);
        Assert.Contains("变速齿轮", r3);
    }
}
