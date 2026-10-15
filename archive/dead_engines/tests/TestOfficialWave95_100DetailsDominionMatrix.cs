using System;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 盛大官方 1.5 原版第 1651 ~ 1750 号微观细节规则权威测试矩阵 (Wave 95)
/// </summary>
public class TestOfficialWave95_100DetailsDominionMatrix
{
    // =========================================================================
    // 细节 1651 ~ 1665：比奇矿区与尸王殿微观生态
    // =========================================================================

    [Fact]
    public void Test1651_CalculateSpearmanShieldPierceDamage()
    {
        var (hp1, shield1) = OfficialDominionAscent100DetailsEngine.CalculateSpearmanShieldPierceDamage(100, false);
        Assert.Equal(100, hp1);
        Assert.Equal(0, shield1);

        var (hp2, shield2) = OfficialDominionAscent100DetailsEngine.CalculateSpearmanShieldPierceDamage(100, true);
        Assert.Equal(60, hp2); // 削弱后仅吸收40%，受到60点伤害
        Assert.Equal(40, shield2);
    }

    [Fact]
    public void Test1652_CanPerceiveBossSpawnTremor()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.CanPerceiveBossSpawnTremor(100, 100, 110, 100)); // 10格 <= 15格
        Assert.False(OfficialDominionAscent100DetailsEngine.CanPerceiveBossSpawnTremor(100, 100, 120, 100)); // 20格 > 15格
    }

    [Fact]
    public void Test1653_CheckHammerSkeletonDuraDmg()
    {
        Assert.Equal(1000, OfficialDominionAscent100DetailsEngine.CheckHammerSkeletonDuraDmg(5));
        Assert.Equal(0, OfficialDominionAscent100DetailsEngine.CheckHammerSkeletonDuraDmg(50));
    }

    [Fact]
    public void Test1654_CalculateArcherObstructedHitRate()
    {
        Assert.Equal(80, OfficialDominionAscent100DetailsEngine.CalculateArcherObstructedHitRate(80, false));
        Assert.Equal(60, OfficialDominionAscent100DetailsEngine.CalculateArcherObstructedHitRate(80, true)); // 80 * 0.75
    }

    [Fact]
    public void Test1655_IsCorpseZombieImmuneOnSpawn()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.IsCorpseZombieImmuneOnSpawn(1000, 1500)); // 500ms < 800ms
        Assert.False(OfficialDominionAscent100DetailsEngine.IsCorpseZombieImmuneOnSpawn(1000, 2000)); // 1000ms >= 800ms
    }

    [Fact]
    public void Test1656_GetEvilZombieKingPoisonAura()
    {
        var (dmg, interval) = OfficialDominionAscent100DetailsEngine.GetEvilZombieKingPoisonAura();
        Assert.Equal(12, dmg);
        Assert.Equal(2000, interval);
    }

    [Fact]
    public void Test1657_IsBridgeTilePassable()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.IsBridgeTilePassable(false));
        Assert.False(OfficialDominionAscent100DetailsEngine.IsBridgeTilePassable(true));
    }

    [Fact]
    public void Test1658_CanQueryBossStatusFree()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.CanQueryBossStatusFree(0));
    }

    [Fact]
    public void Test1659_CheckGuYuDrop()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.CheckGuYuDrop(0));
        Assert.False(OfficialDominionAscent100DetailsEngine.CheckGuYuDrop(1));
    }

    [Fact]
    public void Test1660_CheckCorpseSlimeSlow()
    {
        var (slow1, dur1) = OfficialDominionAscent100DetailsEngine.CheckCorpseSlimeSlow(10);
        Assert.True(slow1);
        Assert.Equal(3000, dur1);

        var (slow2, dur2) = OfficialDominionAscent100DetailsEngine.CheckCorpseSlimeSlow(30);
        Assert.False(slow2);
        Assert.Equal(0, dur2);
    }

    [Fact]
    public void Test1661_IsTideBlocked()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.IsTideBlocked(true, true));
        Assert.False(OfficialDominionAscent100DetailsEngine.IsTideBlocked(false, true));
        Assert.False(OfficialDominionAscent100DetailsEngine.IsTideBlocked(true, false));
    }

    [Fact]
    public void Test1662_CanSellBookInBichonBookStore()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.CanSellBookInBichonBookStore(28));
        Assert.False(OfficialDominionAscent100DetailsEngine.CanSellBookInBichonBookStore(35));
    }

    [Fact]
    public void Test1663_CanTakeOldVeteranToMongchon()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.CanTakeOldVeteranToMongchon(500, out int cost));
        Assert.Equal(500, cost);
        Assert.False(OfficialDominionAscent100DetailsEngine.CanTakeOldVeteranToMongchon(400, out _));
    }

    [Fact]
    public void Test1664_GetDummyDamageFloatingText()
    {
        Assert.Equal(0, OfficialDominionAscent100DetailsEngine.GetDummyDamageFloatingText(true));
        Assert.Equal(1, OfficialDominionAscent100DetailsEngine.GetDummyDamageFloatingText(false));
    }

    [Fact]
    public void Test1665_ExchangeBlackIronToGold()
    {
        Assert.Equal(5000, OfficialDominionAscent100DetailsEngine.ExchangeBlackIronToGold(5));
    }

    // =========================================================================
    // 细节 1666 ~ 1680：尸王殿钻地地洞、暗黑通道与封魔战术
    // =========================================================================

    [Fact]
    public void Test1666_CanEnterCorpseKingHiddenCaveGroup()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.CanEnterCorpseKingHiddenCaveGroup(2, true, true));
        Assert.False(OfficialDominionAscent100DetailsEngine.CanEnterCorpseKingHiddenCaveGroup(2, true, false));
        Assert.False(OfficialDominionAscent100DetailsEngine.CanEnterCorpseKingHiddenCaveGroup(3, true, true));
    }

    [Fact]
    public void Test1667_IsScrollAllowedInCorpseKingChamber()
    {
        Assert.False(OfficialDominionAscent100DetailsEngine.IsScrollAllowedInCorpseKingChamber("M001"));
        Assert.True(OfficialDominionAscent100DetailsEngine.IsScrollAllowedInCorpseKingChamber("0"));
    }

    [Fact]
    public void Test1668_ShouldCannibalPlantEmerge()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.ShouldCannibalPlantEmerge(2));
        Assert.False(OfficialDominionAscent100DetailsEngine.ShouldCannibalPlantEmerge(3));
    }

    [Fact]
    public void Test1669_IsPrayerRingFullDura()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.IsPrayerRingFullDura(5000, 5000));
        Assert.False(OfficialDominionAscent100DetailsEngine.IsPrayerRingFullDura(4999, 5000));
    }

    [Fact]
    public void Test1670_GetTameSuccessRoarSoundId()
    {
        Assert.Equal(888, OfficialDominionAscent100DetailsEngine.GetTameSuccessRoarSoundId());
    }

    [Fact]
    public void Test1671_CalculateGuildAllyDefenseBonus()
    {
        Assert.Equal(105, OfficialDominionAscent100DetailsEngine.CalculateGuildAllyDefenseBonus(100, true));
        Assert.Equal(100, OfficialDominionAscent100DetailsEngine.CalculateGuildAllyDefenseBonus(100, false));
    }

    [Fact]
    public void Test1672_IsQiLuTileWalkable()
    {
        bool Grid(int x, int y) => !(x == 5 && y == 5);
        Assert.True(OfficialDominionAscent100DetailsEngine.IsQiLuTileWalkable(4, 5, Grid));
        Assert.False(OfficialDominionAscent100DetailsEngine.IsQiLuTileWalkable(5, 5, Grid));
    }

    [Fact]
    public void Test1673_CanPassSecretTunnelGate()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.CanPassSecretTunnelGate(1000, 16001));
        Assert.False(OfficialDominionAscent100DetailsEngine.CanPassSecretTunnelGate(1000, 15000));
    }

    [Fact]
    public void Test1674_IsUltimateSkillBookDrop()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.IsUltimateSkillBookDrop("烈火剑法"));
        Assert.True(OfficialDominionAscent100DetailsEngine.IsUltimateSkillBookDrop("召唤神兽"));
        Assert.True(OfficialDominionAscent100DetailsEngine.IsUltimateSkillBookDrop("冰咆哮"));
        Assert.False(OfficialDominionAscent100DetailsEngine.IsUltimateSkillBookDrop("雷电术"));
    }

    [Fact]
    public void Test1675_CheckWedgeMothDispelHolyArmor()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.CheckWedgeMothDispelHolyArmor(10));
        Assert.False(OfficialDominionAscent100DetailsEngine.CheckWedgeMothDispelHolyArmor(20));
    }

    [Fact]
    public void Test1676_CheckThunderPathExit()
    {
        var (isTrue, map) = OfficialDominionAscent100DetailsEngine.CheckThunderPathExit(2);
        Assert.True(isTrue);
        Assert.Equal("D2005", map);

        var (isFalse, trap) = OfficialDominionAscent100DetailsEngine.CheckThunderPathExit(1);
        Assert.False(isFalse);
        Assert.Contains("Trap", trap);
    }

    [Fact]
    public void Test1677_CanRecycleWomaJewelry()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.CanRecycleWomaJewelry(30, 20));
        Assert.False(OfficialDominionAscent100DetailsEngine.CanRecycleWomaJewelry(25, 20));
        Assert.False(OfficialDominionAscent100DetailsEngine.CanRecycleWomaJewelry(30, 5));
    }

    [Fact]
    public void Test1678_ShouldEvilSnakeCallMaggots()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.ShouldEvilSnakeCallMaggots(40, 20));
        Assert.False(OfficialDominionAscent100DetailsEngine.ShouldEvilSnakeCallMaggots(60, 20));
        Assert.False(OfficialDominionAscent100DetailsEngine.ShouldEvilSnakeCallMaggots(40, 80));
    }

    [Fact]
    public void Test1679_CanOpenFengMoPalaceGate()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.CanOpenFengMoPalaceGate(6));
        Assert.False(OfficialDominionAscent100DetailsEngine.CanOpenFengMoPalaceGate(5));
    }

    [Fact]
    public void Test1680_CalculateHongMoMoveInterval()
    {
        Assert.Equal(300, OfficialDominionAscent100DetailsEngine.CalculateHongMoMoveInterval(600, true));
        Assert.Equal(600, OfficialDominionAscent100DetailsEngine.CalculateHongMoMoveInterval(600, false));
    }

    // =========================================================================
    // 细节 1681 ~ 1695：药水背包沉降与交易安全防刷
    // =========================================================================

    [Fact]
    public void Test1681_CalculatePotionUnbundleGroundDropCount()
    {
        Assert.Equal(3, OfficialDominionAscent100DetailsEngine.CalculatePotionUnbundleGroundDropCount(2)); // 5 - 2 = 3
        Assert.Equal(0, OfficialDominionAscent100DetailsEngine.CalculatePotionUnbundleGroundDropCount(6));
    }

    [Fact]
    public void Test1682_IsGroundStackOverflow()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.IsGroundStackOverflow(5));
        Assert.False(OfficialDominionAscent100DetailsEngine.IsGroundStackOverflow(4));
    }

    [Fact]
    public void Test1683_CanFinalizeTrade()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.CanFinalizeTrade(5, 3));
        Assert.False(OfficialDominionAscent100DetailsEngine.CanFinalizeTrade(2, 3));
    }

    [Fact]
    public void Test1684_ValidateTradeGold()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.ValidateTradeGold(1000, 5000));
        Assert.False(OfficialDominionAscent100DetailsEngine.ValidateTradeGold(-100, 5000));
        Assert.False(OfficialDominionAscent100DetailsEngine.ValidateTradeGold(6000, 5000));
    }

    [Fact]
    public void Test1685_ShouldAbortTradeDueToDistance()
    {
        Assert.False(OfficialDominionAscent100DetailsEngine.ShouldAbortTradeDueToDistance(10, 10, 11, 12)); // 切比雪夫 2 格
        Assert.True(OfficialDominionAscent100DetailsEngine.ShouldAbortTradeDueToDistance(10, 10, 13, 10));  // 3 格超距
    }

    [Fact]
    public void Test1686_DepositGuildFund()
    {
        var (ok, accepted, refund) = OfficialDominionAscent100DetailsEngine.DepositGuildFund(49_000_000, 2_000_000);
        Assert.True(ok);
        Assert.Equal(1_000_000, accepted);
        Assert.Equal(1_000_000, refund);
    }

    [Fact]
    public void Test1687_IsCarryGoldAtMaxLimit()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.IsCarryGoldAtMaxLimit(5_000_000));
        Assert.False(OfficialDominionAscent100DetailsEngine.IsCarryGoldAtMaxLimit(4_999_999));
    }

    [Fact]
    public void Test1688_IsSpecialRingActive()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.IsSpecialRingActive(1));
        Assert.False(OfficialDominionAscent100DetailsEngine.IsSpecialRingActive(0));
    }

    [Fact]
    public void Test1689_CanRevivalRingTrigger()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.CanRevivalRingTrigger(1000, 121_001));
        Assert.False(OfficialDominionAscent100DetailsEngine.CanRevivalRingTrigger(1000, 100_000));
    }

    [Fact]
    public void Test1690_RollParalysisHit()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.RollParalysisHit(0));
        Assert.False(OfficialDominionAscent100DetailsEngine.RollParalysisHit(1));
    }

    [Fact]
    public void Test1691_ConsumeTeleportCost()
    {
        var (ok, mp, dura) = OfficialDominionAscent100DetailsEngine.ConsumeTeleportCost(100, 5000);
        Assert.True(ok);
        Assert.Equal(50, mp);
        Assert.Equal(4000, dura);

        var (fail, _, _) = OfficialDominionAscent100DetailsEngine.ConsumeTeleportCost(40, 5000);
        Assert.False(fail);
    }

    [Fact]
    public void Test1692_ShouldRecloak()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.ShouldRecloak(1000, 2000));
        Assert.False(OfficialDominionAscent100DetailsEngine.ShouldRecloak(1000, 1500));
    }

    [Fact]
    public void Test1693_CalculateBodyProtectDamage()
    {
        var (hp1, mp1) = OfficialDominionAscent100DetailsEngine.CalculateBodyProtectDamage(50, 100);
        Assert.Equal(0, hp1);
        Assert.Equal(50, mp1);

        var (hp2, mp2) = OfficialDominionAscent100DetailsEngine.CalculateBodyProtectDamage(100, 60);
        Assert.Equal(60, hp2); // (100 - 60) * 1.5 = 60
        Assert.Equal(0, mp2);
    }

    [Fact]
    public void Test1694_CalculateDefenseRingHealAmount()
    {
        Assert.Equal(30, OfficialDominionAscent100DetailsEngine.CalculateDefenseRingHealAmount(30));
    }

    [Fact]
    public void Test1695_CalculateFireRingDamage()
    {
        Assert.Equal(25, OfficialDominionAscent100DetailsEngine.CalculateFireRingDamage(20));
    }

    // =========================================================================
    // 细节 1696 ~ 1710：武器升级、开刀与幸运诅咒极值
    // =========================================================================

    [Fact]
    public void Test1696_CheckIronPurityDuraEffect()
    {
        Assert.Equal(2000, OfficialDominionAscent100DetailsEngine.CheckIronPurityDuraEffect(120));
        Assert.Equal(1000, OfficialDominionAscent100DetailsEngine.CheckIronPurityDuraEffect(95));
        Assert.Equal(0, OfficialDominionAscent100DetailsEngine.CheckIronPurityDuraEffect(75));
        Assert.Equal(-1000, OfficialDominionAscent100DetailsEngine.CheckIronPurityDuraEffect(40));
        Assert.Equal(-3000, OfficialDominionAscent100DetailsEngine.CheckIronPurityDuraEffect(20));
    }

    [Fact]
    public void Test1697_IsMaxIronPurityBonus()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.IsMaxIronPurityBonus(120));
        Assert.False(OfficialDominionAscent100DetailsEngine.IsMaxIronPurityBonus(119));
    }

    [Fact]
    public void Test1698_CanUpgradeStat()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.CanUpgradeStat(6));
        Assert.False(OfficialDominionAscent100DetailsEngine.CanUpgradeStat(7));
    }

    [Fact]
    public void Test1699_CalculateUpgradeSuccessRate()
    {
        Assert.Equal(80, OfficialDominionAscent100DetailsEngine.CalculateUpgradeSuccessRate(50, 150));
        Assert.Equal(85, OfficialDominionAscent100DetailsEngine.CalculateUpgradeSuccessRate(50, 300)); // 85% 封顶
    }

    [Fact]
    public void Test1700_DetermineUpgradeStatType()
    {
        Assert.Equal("DC", OfficialDominionAscent100DetailsEngine.DetermineUpgradeStatType(30, 20, 10));
        Assert.Equal("MC", OfficialDominionAscent100DetailsEngine.DetermineUpgradeStatType(10, 30, 20));
        Assert.Equal("SC", OfficialDominionAscent100DetailsEngine.DetermineUpgradeStatType(10, 20, 30));
        Assert.Equal("DC", OfficialDominionAscent100DetailsEngine.DetermineUpgradeStatType(20, 20, 20)); // 平局 DC 优先
    }

    [Fact]
    public void Test1701_DetermineUpgradeStatType_TieBreakerPriority()
    {
        Assert.Equal("DC", OfficialDominionAscent100DetailsEngine.DetermineUpgradeStatType(15, 15, 15));
        Assert.Equal("MC", OfficialDominionAscent100DetailsEngine.DetermineUpgradeStatType(10, 15, 15)); // MC 优先于 SC
    }

    [Fact]
    public void Test1702_ComputePredefinedUpgradeResult()
    {
        bool r1 = OfficialDominionAscent100DetailsEngine.ComputePredefinedUpgradeResult(1000, 555);
        bool r2 = OfficialDominionAscent100DetailsEngine.ComputePredefinedUpgradeResult(1000, 555);
        Assert.Equal(r1, r2); // 确定性种子重现
    }

    [Fact]
    public void Test1703_CanSlashTeamMember()
    {
        Assert.False(OfficialDominionAscent100DetailsEngine.CanSlashTeamMember(true, true));
        Assert.True(OfficialDominionAscent100DetailsEngine.CanSlashTeamMember(true, false));
        Assert.True(OfficialDominionAscent100DetailsEngine.CanSlashTeamMember(false, true));
    }

    [Fact]
    public void Test1704_IsWeaponBrokenOnSlash()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.IsWeaponBrokenOnSlash(false));
        Assert.False(OfficialDominionAscent100DetailsEngine.IsWeaponBrokenOnSlash(true));
    }

    [Fact]
    public void Test1705_GetBlessingOilSuccessRate()
    {
        Assert.Equal(70, OfficialDominionAscent100DetailsEngine.GetBlessingOilSuccessRate(0));
        Assert.Equal(3, OfficialDominionAscent100DetailsEngine.GetBlessingOilSuccessRate(6));
        Assert.Equal(0, OfficialDominionAscent100DetailsEngine.GetBlessingOilSuccessRate(7));
    }

    [Fact]
    public void Test1706_CheckBlessingCurseBackfire()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.CheckBlessingCurseBackfire(10));
        Assert.False(OfficialDominionAscent100DetailsEngine.CheckBlessingCurseBackfire(25));
    }

    [Fact]
    public void Test1707_ApplyBlessingOilWithCurse()
    {
        var (c1, l1) = OfficialDominionAscent100DetailsEngine.ApplyBlessingOilWithCurse(2, 0);
        Assert.Equal(1, c1);
        Assert.Equal(0, l1);

        var (c2, l2) = OfficialDominionAscent100DetailsEngine.ApplyBlessingOilWithCurse(0, 3);
        Assert.Equal(0, c2);
        Assert.Equal(4, l2);
    }

    [Fact]
    public void Test1708_CalculateFullLuckDamage()
    {
        Assert.Equal(50, OfficialDominionAscent100DetailsEngine.CalculateFullLuckDamage(10, 50, 9));
        Assert.Equal(10, OfficialDominionAscent100DetailsEngine.CalculateFullLuckDamage(10, 50, 5));
    }

    [Fact]
    public void Test1709_CalculateFullCurseDamage()
    {
        Assert.Equal(10, OfficialDominionAscent100DetailsEngine.CalculateFullCurseDamage(10, 50, 10));
        Assert.Equal(50, OfficialDominionAscent100DetailsEngine.CalculateFullCurseDamage(10, 50, 5));
    }

    [Fact]
    public void Test1710_CalculateLuoShaDamage()
    {
        Assert.Equal(15, OfficialDominionAscent100DetailsEngine.CalculateLuoShaDamage(15, 0, 10));
    }

    // =========================================================================
    // 细节 1711 ~ 1725：官方 1.5 沙巴克攻城战筹备、同盟宣战与皇宫争夺
    // =========================================================================

    [Fact]
    public void Test1711_CanApplySabukWar()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.CanApplySabukWar(35));
        Assert.False(OfficialDominionAscent100DetailsEngine.CanApplySabukWar(34));
    }

    [Fact]
    public void Test1712_CanSubmitZumaRelic()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.CanSubmitZumaRelic(35, 1));
        Assert.False(OfficialDominionAscent100DetailsEngine.CanSubmitZumaRelic(34, 1));
        Assert.False(OfficialDominionAscent100DetailsEngine.CanSubmitZumaRelic(35, 0));
    }

    [Fact]
    public void Test1713_GetSabukWarScheduleDuration()
    {
        var (war, truce) = OfficialDominionAscent100DetailsEngine.GetSabukWarScheduleDuration(35);
        Assert.Equal(180, war);
        Assert.Equal(60, truce);
    }

    [Fact]
    public void Test1714_GetSabukSiegePkPenalty()
    {
        Assert.Equal(0, OfficialDominionAscent100DetailsEngine.GetSabukSiegePkPenalty());
    }

    [Fact]
    public void Test1715_CanReapplyGuildAlliance()
    {
        const long OneDayMs = 24 * 3600 * 1000L;
        Assert.True(OfficialDominionAscent100DetailsEngine.CanReapplyGuildAlliance(1000, 1000 + OneDayMs + 10));
        Assert.False(OfficialDominionAscent100DetailsEngine.CanReapplyGuildAlliance(1000, 1000 + OneDayMs - 10));
    }

    [Fact]
    public void Test1716_GetSabukPalaceDefenderCapacity()
    {
        Assert.Equal(10, OfficialDominionAscent100DetailsEngine.GetSabukPalaceDefenderCapacity());
    }

    [Fact]
    public void Test1717_GetSabukGateRepairCosts()
    {
        var (cost, desc) = OfficialDominionAscent100DetailsEngine.GetSabukGateRepairCosts(13);
        Assert.Equal(10, cost);
        Assert.Equal("轻度修缮", desc);
    }

    [Fact]
    public void Test1718_GetSabukGateRepairCosts_MediumRank()
    {
        var (cost, desc) = OfficialDominionAscent100DetailsEngine.GetSabukGateRepairCosts(23);
        Assert.Equal(20, cost);
        Assert.Equal("中度加固", desc);
    }

    [Fact]
    public void Test1719_GetSabukGateRepairCosts_HighRank()
    {
        var (cost, desc) = OfficialDominionAscent100DetailsEngine.GetSabukGateRepairCosts(33);
        Assert.Equal(40, cost);
        Assert.Equal("重大重建", desc);
    }

    [Fact]
    public void Test1720_GetSabukGateRepairCosts_Rank43Stat()
    {
        var (cost, desc) = OfficialDominionAscent100DetailsEngine.GetSabukGateRepairCosts(43);
        Assert.Equal(80, cost);
        Assert.Equal("金石强化", desc);
    }

    [Fact]
    public void Test1721_CanPassSabukSecretWall()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.CanPassSabukSecretWall(true));
        Assert.False(OfficialDominionAscent100DetailsEngine.CanPassSabukSecretWall(false));
    }

    [Fact]
    public void Test1722_CalculateGuildWarDeclarationCost()
    {
        var (p1, p2) = OfficialDominionAscent100DetailsEngine.CalculateGuildWarDeclarationCost(10, 8);
        Assert.Equal(5, p1);
        Assert.Equal(3, p2);
    }

    [Fact]
    public void Test1723_CalculateSabukGateEmergencyFund()
    {
        var (ok, p, g) = OfficialDominionAscent100DetailsEngine.CalculateSabukGateEmergencyFund(15, 1_500_000);
        Assert.True(ok);
        Assert.Equal(5, p);
        Assert.Equal(500_000, g);
    }

    [Fact]
    public void Test1724_CheckZumaRelicDrop()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.CheckZumaRelicDrop(0));
        Assert.False(OfficialDominionAscent100DetailsEngine.CheckZumaRelicDrop(1));
    }

    [Fact]
    public void Test1725_FormatSabukOccupyAnnouncement()
    {
        string text = OfficialDominionAscent100DetailsEngine.FormatSabukOccupyAnnouncement("昔日同盟", "小虾米");
        Assert.Contains("昔日同盟", text);
        Assert.Contains("小虾米", text);
        Assert.Contains("入主沙巴克", text);
    }

    // =========================================================================
    // 细节 1726 ~ 1740：沙巴克皇宫争夺与行会战役细节
    // =========================================================================

    [Fact]
    public void Test1726_ValidateSiegeSubmission()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.ValidateSiegeSubmission(true, 1_000_000, 3));
        Assert.False(OfficialDominionAscent100DetailsEngine.ValidateSiegeSubmission(false, 1_000_000, 3));
        Assert.False(OfficialDominionAscent100DetailsEngine.ValidateSiegeSubmission(true, 900_000, 3));
        Assert.False(OfficialDominionAscent100DetailsEngine.ValidateSiegeSubmission(true, 1_000_000, 2));
    }

    [Fact]
    public void Test1727_IsSiegeBattleActive()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.IsSiegeBattleActive(20));
        Assert.True(OfficialDominionAscent100DetailsEngine.IsSiegeBattleActive(22));
        Assert.False(OfficialDominionAscent100DetailsEngine.IsSiegeBattleActive(19));
        Assert.False(OfficialDominionAscent100DetailsEngine.IsSiegeBattleActive(23));
    }

    [Fact]
    public void Test1728_DamageCastleGate()
    {
        var (c1, p1) = OfficialDominionAscent100DetailsEngine.DamageCastleGate(20000, 5000);
        Assert.False(c1);
        Assert.False(p1);

        var (c2, p2) = OfficialDominionAscent100DetailsEngine.DamageCastleGate(5000, 5000);
        Assert.True(c2);
        Assert.True(p2);
    }

    [Fact]
    public void Test1729_DamageCastleGate_CollapsedRemovesBlock()
    {
        var (collapsed, passable) = OfficialDominionAscent100DetailsEngine.DamageCastleGate(1000, 1500);
        Assert.True(collapsed);
        Assert.True(passable); // 坍塌后阻挡解除，可自由通行
    }

    [Fact]
    public void Test1730_IsSecretTunnelOpen()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.IsSecretTunnelOpen(true));
        Assert.False(OfficialDominionAscent100DetailsEngine.IsSecretTunnelOpen(false));
    }

    [Fact]
    public void Test1731_GetOldManTeleportFee()
    {
        Assert.Equal(0, OfficialDominionAscent100DetailsEngine.GetOldManTeleportFee(true));
        Assert.Equal(5000, OfficialDominionAscent100DetailsEngine.GetOldManTeleportFee(false));
    }

    [Fact]
    public void Test1732_CheckPalaceHoldSuccess()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.CheckPalaceHoldSuccess(30));
        Assert.False(OfficialDominionAscent100DetailsEngine.CheckPalaceHoldSuccess(29));
    }

    [Fact]
    public void Test1733_GetSiegeKillPkPoints()
    {
        Assert.Equal(0, OfficialDominionAscent100DetailsEngine.GetSiegeKillPkPoints(true, true));
        Assert.Equal(100, OfficialDominionAscent100DetailsEngine.GetSiegeKillPkPoints(false, true));
    }

    [Fact]
    public void Test1734_CalculateSiegeDropChance()
    {
        Assert.Equal(25, OfficialDominionAscent100DetailsEngine.CalculateSiegeDropChance(50));
    }

    [Fact]
    public void Test1735_CallGuildAssembly()
    {
        var (ok, fund) = OfficialDominionAscent100DetailsEngine.CallGuildAssembly(60000);
        Assert.True(ok);
        Assert.Equal(10000, fund);

        var (fail, _) = OfficialDominionAscent100DetailsEngine.CallGuildAssembly(40000);
        Assert.False(fail);
    }

    [Fact]
    public void Test1736_CalculateSabukDailyTaxShare()
    {
        Assert.Equal(200_000, OfficialDominionAscent100DetailsEngine.CalculateSabukDailyTaxShare(1_000_000));
    }

    [Fact]
    public void Test1737_GetGuildWarNameTag()
    {
        Assert.Equal("[敌对]", OfficialDominionAscent100DetailsEngine.GetGuildWarNameTag(true));
        Assert.Equal("[友军]", OfficialDominionAscent100DetailsEngine.GetGuildWarNameTag(false));
    }

    [Fact]
    public void Test1738_CanBreakAlliance()
    {
        const long OneDayMs = 24 * 3600 * 1000L;
        Assert.True(OfficialDominionAscent100DetailsEngine.CanBreakAlliance(true, 0, 0));
        Assert.True(OfficialDominionAscent100DetailsEngine.CanBreakAlliance(false, 1000, 1000 + OneDayMs + 1));
        Assert.False(OfficialDominionAscent100DetailsEngine.CanBreakAlliance(false, 1000, 1000 + OneDayMs - 1));
    }

    [Fact]
    public void Test1739_CanApplyNewGuildAfterLeave()
    {
        const long TwelveHoursMs = 12 * 3600 * 1000L;
        Assert.True(OfficialDominionAscent100DetailsEngine.CanApplyNewGuildAfterLeave(1000, 1000 + TwelveHoursMs + 10));
        Assert.False(OfficialDominionAscent100DetailsEngine.CanApplyNewGuildAfterLeave(1000, 1000 + TwelveHoursMs - 10));
    }

    [Fact]
    public void Test1740_ValidateLeaderAbdication()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.ValidateLeaderAbdication(true, 1));
        Assert.False(OfficialDominionAscent100DetailsEngine.ValidateLeaderAbdication(false, 1));
        Assert.False(OfficialDominionAscent100DetailsEngine.ValidateLeaderAbdication(true, 2));
    }

    // =========================================================================
    // 细节 1741 ~ 1750：Direct-Blit 协议抗丢包与 Native AOT 零分配
    // =========================================================================

    [Fact]
    public void Test1741_VerifyDirectBlitMagic()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.VerifyDirectBlitMagic(0xAA55));
        Assert.False(OfficialDominionAscent100DetailsEngine.VerifyDirectBlitMagic(0x1234));
    }

    [Fact]
    public void Test1742_CheckPacketSequenceWindow()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.CheckPacketSequenceWindow(100, 100));
        Assert.True(OfficialDominionAscent100DetailsEngine.CheckPacketSequenceWindow(100, 160));
        Assert.False(OfficialDominionAscent100DetailsEngine.CheckPacketSequenceWindow(100, 99)); // 旧包丢弃
        Assert.False(OfficialDominionAscent100DetailsEngine.CheckPacketSequenceWindow(100, 170)); // 超过 64 步
    }

    [Fact]
    public void Test1743_CheckHeartbeatTimeout()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.CheckHeartbeatTimeout(1000, 6001));
        Assert.False(OfficialDominionAscent100DetailsEngine.CheckHeartbeatTimeout(1000, 5000));
    }

    [Fact]
    public void Test1744_IsPacketOversized()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.IsPacketOversized(4097));
        Assert.False(OfficialDominionAscent100DetailsEngine.IsPacketOversized(4096));
    }

    [Fact]
    public void Test1745_CheckInputRateThrottling()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.CheckInputRateThrottling(21));
        Assert.False(OfficialDominionAscent100DetailsEngine.CheckInputRateThrottling(20));
    }

    [Fact]
    public void Test1746_GetExponentialBackoffMs()
    {
        Assert.Equal(100, OfficialDominionAscent100DetailsEngine.GetExponentialBackoffMs(0));
        Assert.Equal(200, OfficialDominionAscent100DetailsEngine.GetExponentialBackoffMs(1));
        Assert.Equal(1600, OfficialDominionAscent100DetailsEngine.GetExponentialBackoffMs(4));
        Assert.Equal(3200, OfficialDominionAscent100DetailsEngine.GetExponentialBackoffMs(5));
        Assert.Equal(4000, OfficialDominionAscent100DetailsEngine.GetExponentialBackoffMs(6)); // 4000ms 封顶
    }

    [Fact]
    public void Test1747_CheckBufferHeadroom()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.CheckBufferHeadroom(10000, 5000));
        Assert.False(OfficialDominionAscent100DetailsEngine.CheckBufferHeadroom(15000, 2000));
    }

    [Fact]
    public void Test1748_CalculateCrc16()
    {
        byte[] payload = [0x55, 0xAA, 0x01, 0x02];
        ushort crc1 = OfficialDominionAscent100DetailsEngine.CalculateCrc16(payload);
        ushort crc2 = OfficialDominionAscent100DetailsEngine.CalculateCrc16(payload);
        Assert.Equal(crc1, crc2);
        Assert.NotEqual(0, crc1);
    }

    [Fact]
    public void Test1749_IsAotZeroAllocStructure()
    {
        Assert.True(OfficialDominionAscent100DetailsEngine.IsAotZeroAllocStructure());
    }

    [Fact]
    public void Test1750_EnforceServerAuthoritativeValue()
    {
        int speculativeClientHp = 350;
        int authoritativeServerHp = 220;
        int reconciled = OfficialDominionAscent100DetailsEngine.EnforceServerAuthoritativeValue(speculativeClientHp, authoritativeServerHp);
        Assert.Equal(authoritativeServerHp, reconciled);
    }
}
