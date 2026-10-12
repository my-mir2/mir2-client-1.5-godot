using System;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 盛大官方 1.5 原版第 2251 ~ 2350 号微观细节规则权威测试矩阵 (Wave 101)
/// 100% 官方 1.50 绝对真值，零非标 1.5+ 残留，覆盖全部 100 个微观核心细节
/// </summary>
public class TestOfficialWave101_100DetailsApexMatrix
{
    // =========================================================================
    // 1. 比奇城皇宫国王任务与创建行会沃玛号角深层流转 (2251~2260)
    // =========================================================================

    [Fact]
    public void Test2251_ValidateGuildCreator()
    {
        var (ok1, _) = OfficialPure15Apex101DetailsEngine.ValidateGuildCreator(22, false);
        Assert.True(ok1);

        var (ok2, _) = OfficialPure15Apex101DetailsEngine.ValidateGuildCreator(21, false);
        Assert.False(ok2);

        var (ok3, _) = OfficialPure15Apex101DetailsEngine.ValidateGuildCreator(25, true);
        Assert.False(ok3);
    }

    [Fact]
    public void Test2252_ValidateGuildName()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.ValidateGuildName("昔日同盟"));
        Assert.False(OfficialPure15Apex101DetailsEngine.ValidateGuildName(""));
        Assert.False(OfficialPure15Apex101DetailsEngine.ValidateGuildName("超长名字超过十四个字节不可通过"));
    }

    [Fact]
    public void Test2253_ConsumeWoomaHornOnGuildCreate()
    {
        var (ok, rem) = OfficialPure15Apex101DetailsEngine.ConsumeWoomaHornOnGuildCreate(1);
        Assert.True(ok);
        Assert.Equal(0, rem);

        var (fail, _) = OfficialPure15Apex101DetailsEngine.ConsumeWoomaHornOnGuildCreate(0);
        Assert.False(fail);
    }

    [Fact]
    public void Test2254_ChargeGuildCreationFee()
    {
        var (ok, rem) = OfficialPure15Apex101DetailsEngine.ChargeGuildCreationFee(1_500_000);
        Assert.True(ok);
        Assert.Equal(500_000, rem);

        var (fail, _) = OfficialPure15Apex101DetailsEngine.ChargeGuildCreationFee(900_000);
        Assert.False(fail);
    }

    [Fact]
    public void Test2255_GetGuildInitialConfig()
    {
        var (max, title) = OfficialPure15Apex101DetailsEngine.GetGuildInitialConfig();
        Assert.Equal(50, max);
        Assert.Equal("一统天下", title);
    }

    [Fact]
    public void Test2256_CanAbdicateGuildLeader()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.CanAbdicateGuildLeader(true, true));
        Assert.False(OfficialPure15Apex101DetailsEngine.CanAbdicateGuildLeader(true, false));
    }

    [Fact]
    public void Test2257_CanJoinNewGuildAfterQuit()
    {
        const long OneDay = 24 * 3600 * 1000L;
        Assert.True(OfficialPure15Apex101DetailsEngine.CanJoinNewGuildAfterQuit(1000, 1000 + OneDay + 10));
        Assert.False(OfficialPure15Apex101DetailsEngine.CanJoinNewGuildAfterQuit(1000, 1000 + OneDay - 10));
    }

    [Fact]
    public void Test2258_CanDisbandGuild()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.CanDisbandGuild(0));
        Assert.False(OfficialPure15Apex101DetailsEngine.CanDisbandGuild(1));
    }

    [Fact]
    public void Test2259_ValidateGuildNoticeLength()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.ValidateGuildNoticeLength("今晚八点沙巴克准时集合！"));
        Assert.False(OfficialPure15Apex101DetailsEngine.ValidateGuildNoticeLength(new string('玛', 35))); // 70 字节 > 60
    }

    [Fact]
    public void Test2260_CanAddEnemyGuild()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.CanAddEnemyGuild(9));
        Assert.False(OfficialPure15Apex101DetailsEngine.CanAddEnemyGuild(10));
    }

    // =========================================================================
    // 2. 沃玛三层沃玛号角与沃玛教主神殿绝对机制 (2261~2270)
    // =========================================================================

    [Fact]
    public void Test2261_GetWoomaLeaderRespawnMinutes()
    {
        Assert.Equal(180, OfficialPure15Apex101DetailsEngine.GetWoomaLeaderRespawnMinutes());
    }

    [Fact]
    public void Test2262_ShouldWoomaLeaderTeleport()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.ShouldWoomaLeaderTeleport(500, 2000, 10));
        Assert.False(OfficialPure15Apex101DetailsEngine.ShouldWoomaLeaderTeleport(1000, 2000, 10));
    }

    [Fact]
    public void Test2263_RollWoomaHornDrop()
    {
        var (dropped, count) = OfficialPure15Apex101DetailsEngine.RollWoomaHornDrop(0);
        Assert.True(dropped);
        Assert.Equal(1, count);
    }

    [Fact]
    public void Test2264_GetWoomaLeaderEnrageGuardCount()
    {
        Assert.Equal(4, OfficialPure15Apex101DetailsEngine.GetWoomaLeaderEnrageGuardCount(true));
        Assert.Equal(2, OfficialPure15Apex101DetailsEngine.GetWoomaLeaderEnrageGuardCount(false));
    }

    [Fact]
    public void Test2265_CalculateWoomaDarknessVision()
    {
        Assert.Equal(8, OfficialPure15Apex101DetailsEngine.CalculateWoomaDarknessVision(true, 8));
        Assert.Equal(2, OfficialPure15Apex101DetailsEngine.CalculateWoomaDarknessVision(false, 8));
    }

    [Fact]
    public void Test2266_DecayTorchDurability()
    {
        var (dura, ext) = OfficialPure15Apex101DetailsEngine.DecayTorchDurability(10, 30);
        Assert.Equal(7, dura);
        Assert.False(ext);

        var (dura0, ext0) = OfficialPure15Apex101DetailsEngine.DecayTorchDurability(2, 50);
        Assert.Equal(0, dura0);
        Assert.True(ext0);
    }

    [Fact]
    public void Test2267_CanRepelWoomaDarkMage()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.CanRepelWoomaDarkMage(35, 30));
        Assert.False(OfficialPure15Apex101DetailsEngine.CanRepelWoomaDarkMage(28, 30));
    }

    [Fact]
    public void Test2268_GetWoomaGuardDropRadius()
    {
        Assert.Equal(4, OfficialPure15Apex101DetailsEngine.GetWoomaGuardDropRadius());
    }

    [Fact]
    public void Test2269_CanPickupWoomaHornProtected()
    {
        Assert.False(OfficialPure15Apex101DetailsEngine.CanPickupWoomaHornProtected(101, 102, 30));
        Assert.True(OfficialPure15Apex101DetailsEngine.CanPickupWoomaHornProtected(101, 101, 30));
        Assert.True(OfficialPure15Apex101DetailsEngine.CanPickupWoomaHornProtected(101, 102, 65));
    }

    [Fact]
    public void Test2270_IsWoomaPillarTile()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.IsWoomaPillarTile(24, 24));
        Assert.False(OfficialPure15Apex101DetailsEngine.IsWoomaPillarTile(20, 20));
    }

    // =========================================================================
    // 3. 祖玛七层三狭窄走廊与祖玛教主破石而出机制 (2271~2280)
    // =========================================================================

    [Fact]
    public void Test2271_CalculateDamageOnSealedZumaLeader()
    {
        Assert.Equal(0, OfficialPure15Apex101DetailsEngine.CalculateDamageOnSealedZumaLeader(100, true));
        Assert.Equal(100, OfficialPure15Apex101DetailsEngine.CalculateDamageOnSealedZumaLeader(100, false));
    }

    [Fact]
    public void Test2272_CheckZumaLeaderUnseal()
    {
        var (unseal, tremor) = OfficialPure15Apex101DetailsEngine.CheckZumaLeaderUnseal(2);
        Assert.True(unseal);
        Assert.True(tremor);

        var (fail, _) = OfficialPure15Apex101DetailsEngine.CheckZumaLeaderUnseal(5);
        Assert.False(fail);
    }

    [Fact]
    public void Test2273_GetZumaUnsealMinions()
    {
        var (guards, statues) = OfficialPure15Apex101DetailsEngine.GetZumaUnsealMinions();
        Assert.Equal(1, guards);
        Assert.Equal(2, statues);
    }

    [Fact]
    public void Test2274_CanPassZumaNarrowCorridor()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.CanPassZumaNarrowCorridor(false));
        Assert.False(OfficialPure15Apex101DetailsEngine.CanPassZumaNarrowCorridor(true));
    }

    [Fact]
    public void Test2275_GetEliteZumaGuardMoveIntervalMs()
    {
        Assert.Equal(200, OfficialPure15Apex101DetailsEngine.GetEliteZumaGuardMoveIntervalMs());
    }

    [Fact]
    public void Test2276_GetEliteZumaStatueCombatStats()
    {
        var (min, max, speed) = OfficialPure15Apex101DetailsEngine.GetEliteZumaStatueCombatStats();
        Assert.Equal(35, min);
        Assert.Equal(80, max);
        Assert.Equal(500, speed);
    }

    [Fact]
    public void Test2277_RollZumaHeadDrop()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.RollZumaHeadDrop());
    }

    [Fact]
    public void Test2278_CalculateZumaArcherDamage()
    {
        int dmgPierced = OfficialPure15Apex101DetailsEngine.CalculateZumaArcherDamage(50, 20, 5);
        Assert.Equal(50, dmgPierced); // 无视防御

        int dmgNormal = OfficialPure15Apex101DetailsEngine.CalculateZumaArcherDamage(50, 20, 50);
        Assert.Equal(30, dmgNormal);
    }

    [Fact]
    public void Test2279_IsZumaLeaderGateLocked()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.IsZumaLeaderGateLocked(1000, 5000));
        Assert.False(OfficialPure15Apex101DetailsEngine.IsZumaLeaderGateLocked(1000, 15000));
    }

    [Fact]
    public void Test2280_RollZumaUltimateWeaponDrop()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.RollZumaUltimateWeaponDrop(0));
        Assert.False(OfficialPure15Apex101DetailsEngine.RollZumaUltimateWeaponDrop(1));
    }

    // =========================================================================
    // 4. 石墓七层猪洞生态与白野猪大爆机制 (2281~2290)
    // =========================================================================

    [Fact]
    public void Test2281_CheckWhiteBoarKnockback()
    {
        var (kb1, tiles1) = OfficialPure15Apex101DetailsEngine.CheckWhiteBoarKnockback(5);
        Assert.True(kb1);
        Assert.Equal(1, tiles1);

        var (kb2, _) = OfficialPure15Apex101DetailsEngine.CheckWhiteBoarKnockback(20);
        Assert.False(kb2);
    }

    [Fact]
    public void Test2282_RollWhiteBoarRareDrop()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.RollWhiteBoarRareDrop(0));
        Assert.False(OfficialPure15Apex101DetailsEngine.RollWhiteBoarRareDrop(10));
    }

    [Fact]
    public void Test2283_GetEvilSnakeMoveIntervalMs()
    {
        Assert.Equal(300, OfficialPure15Apex101DetailsEngine.GetEvilSnakeMoveIntervalMs());
    }

    [Fact]
    public void Test2284_ValidateBlackMaggotTaming()
    {
        var (can1, max1) = OfficialPure15Apex101DetailsEngine.ValidateBlackMaggotTaming(14, 2);
        Assert.True(can1);
        Assert.Equal(5, max1);

        var (can2, _) = OfficialPure15Apex101DetailsEngine.ValidateBlackMaggotTaming(13, 0);
        Assert.False(can2);
    }

    [Fact]
    public void Test2285_CheckWedgeMothParalysis()
    {
        var (para1, dur1) = OfficialPure15Apex101DetailsEngine.CheckWedgeMothParalysis(10);
        Assert.True(para1);
        Assert.Equal(3000, dur1);

        var (para2, _) = OfficialPure15Apex101DetailsEngine.CheckWedgeMothParalysis(50);
        Assert.False(para2);
    }

    [Fact]
    public void Test2286_ValidateStoneTombMazeRoute()
    {
        string[] valid = { "Up", "Down", "Up", "Down", "Left", "Down" };
        Assert.True(OfficialPure15Apex101DetailsEngine.ValidateStoneTombMazeRoute(valid));

        string[] invalid = { "Up", "Up", "Up", "Down", "Left", "Down" };
        Assert.False(OfficialPure15Apex101DetailsEngine.ValidateStoneTombMazeRoute(invalid));
    }

    [Fact]
    public void Test2287_GetStoneTombZombieKingRespawnMinutes()
    {
        Assert.Equal(120, OfficialPure15Apex101DetailsEngine.GetStoneTombZombieKingRespawnMinutes());
    }

    [Fact]
    public void Test2288_GetBoarDefenses()
    {
        var (acRed, macRed) = OfficialPure15Apex101DetailsEngine.GetBoarDefenses(true);
        Assert.Equal(30, acRed);
        Assert.Equal(10, macRed);

        var (acBlack, macBlack) = OfficialPure15Apex101DetailsEngine.GetBoarDefenses(false);
        Assert.Equal(10, acBlack);
        Assert.Equal(30, macBlack);
    }

    [Fact]
    public void Test2289_CalculateScorpionSnakeAttack()
    {
        var (dir, dps, dur) = OfficialPure15Apex101DetailsEngine.CalculateScorpionSnakeAttack(30, 10);
        Assert.Equal(20, dir);
        Assert.Equal(5, dps);
        Assert.Equal(15, dur);
    }

    [Fact]
    public void Test2290_IsStoneTombTunnelOpen()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.IsStoneTombTunnelOpen(1000, 1000 + 60000));
        Assert.False(OfficialPure15Apex101DetailsEngine.IsStoneTombTunnelOpen(1000, 1000 + 200000));
    }

    // =========================================================================
    // 5. 未知暗殿真假首领与神秘装备诅咒洗涤 (2291~2300)
    // =========================================================================

    [Fact]
    public void Test2291_GetUnknownHallWoomaLeaderHp()
    {
        Assert.Equal(2200, OfficialPure15Apex101DetailsEngine.GetUnknownHallWoomaLeaderHp(true));
        Assert.Equal(100, OfficialPure15Apex101DetailsEngine.GetUnknownHallWoomaLeaderHp(false));
    }

    [Fact]
    public void Test2292_GetFakeWoomaLeaderRewards()
    {
        var (exp, water) = OfficialPure15Apex101DetailsEngine.GetFakeWoomaLeaderRewards();
        Assert.Equal(0, exp);
        Assert.True(water);
    }

    [Fact]
    public void Test2293_GetUnknownHallFakeBoarStats()
    {
        var (hp, ac) = OfficialPure15Apex101DetailsEngine.GetUnknownHallFakeBoarStats();
        Assert.Equal(3200, hp);
        Assert.Equal(2, ac);
    }

    [Fact]
    public void Test2294_FormatMysteriousItemStat()
    {
        Assert.Equal("15", OfficialPure15Apex101DetailsEngine.FormatMysteriousItemStat(true, 15));
        Assert.Equal("???", OfficialPure15Apex101DetailsEngine.FormatMysteriousItemStat(false, 15));
    }

    [Fact]
    public void Test2295_CanTakeOffMysteriousItem()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.CanTakeOffMysteriousItem(500));
        Assert.False(OfficialPure15Apex101DetailsEngine.CanTakeOffMysteriousItem(0));
    }

    [Fact]
    public void Test2296_CleanseMysteriousItemWithGodWater()
    {
        var (dura, can) = OfficialPure15Apex101DetailsEngine.CleanseMysteriousItemWithGodWater(5000);
        Assert.Equal(5000, dura);
        Assert.True(can);
    }

    [Fact]
    public void Test2297_CheckGiantPolygonalTaming()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.CheckGiantPolygonalTaming(26, 10));
        Assert.False(OfficialPure15Apex101DetailsEngine.CheckGiantPolygonalTaming(25, 10));
    }

    [Fact]
    public void Test2298_ValidateDeerBloodQuality()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.ValidateDeerBloodQuality(15));
        Assert.False(OfficialPure15Apex101DetailsEngine.ValidateDeerBloodQuality(14));
    }

    [Fact]
    public void Test2299_CalculateCasinoDice()
    {
        var (res1, banker1) = OfficialPure15Apex101DetailsEngine.CalculateCasinoDice(4, 4, 4);
        Assert.Equal("Leopard", res1);
        Assert.True(banker1);

        var (res2, _) = OfficialPure15Apex101DetailsEngine.CalculateCasinoDice(3, 4, 5); // 12
        Assert.Equal("Big", res2);

        var (res3, _) = OfficialPure15Apex101DetailsEngine.CalculateCasinoDice(1, 2, 4); // 7
        Assert.Equal("Small", res3);
    }

    [Fact]
    public void Test2300_CanForgeBladeOfDestiny()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.CanForgeBladeOfDestiny(3, 15, true));
        Assert.False(OfficialPure15Apex101DetailsEngine.CanForgeBladeOfDestiny(2, 15, true));
        Assert.False(OfficialPure15Apex101DetailsEngine.CanForgeBladeOfDestiny(3, 14, true));
    }

    // =========================================================================
    // 6. 赤月峡谷双头金刚割肉与双头血魔突进 (2301~2310)
    // =========================================================================

    [Fact]
    public void Test2301_MustDigCorpseForLoot()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.MustDigCorpseForLoot(99));
        Assert.False(OfficialPure15Apex101DetailsEngine.MustDigCorpseForLoot(10));
    }

    [Fact]
    public void Test2302_RollFaShenLootFromKong()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.RollFaShenLootFromKong(0));
        Assert.False(OfficialPure15Apex101DetailsEngine.RollFaShenLootFromKong(1));
    }

    [Fact]
    public void Test2303_GetBloodDemonDeathLootPattern()
    {
        var (rad, burst) = OfficialPure15Apex101DetailsEngine.GetBloodDemonDeathLootPattern();
        Assert.Equal(4, rad);
        Assert.True(burst);
    }

    [Fact]
    public void Test2304_CalculateBloodDemonChargeEffect()
    {
        var (stun, tiles, ms) = OfficialPure15Apex101DetailsEngine.CalculateBloodDemonChargeEffect();
        Assert.True(stun);
        Assert.Equal(2, tiles);
        Assert.Equal(800, ms);
    }

    [Fact]
    public void Test2305_IsRedMoonDemonSpikeGlobal()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.IsRedMoonDemonSpikeGlobal(12, 12));
        Assert.False(OfficialPure15Apex101DetailsEngine.IsRedMoonDemonSpikeGlobal(12, 15));
    }

    [Fact]
    public void Test2306_GetRedMoonDemonStats()
    {
        var (hp, ac, mac, reg) = OfficialPure15Apex101DetailsEngine.GetRedMoonDemonStats();
        Assert.Equal(5000, hp);
        Assert.Equal(50, ac);
        Assert.Equal(60, mac);
        Assert.Equal(10, reg);
    }

    [Fact]
    public void Test2307_CalculateDemonRegen()
    {
        Assert.Equal(0, OfficialPure15Apex101DetailsEngine.CalculateDemonRegen(10, true));
        Assert.Equal(10, OfficialPure15Apex101DetailsEngine.CalculateDemonRegen(10, false));
    }

    [Fact]
    public void Test2308_IsScrollForbiddenInDemonLair()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.IsScrollForbiddenInDemonLair());
    }

    [Fact]
    public void Test2309_RollTianZunDrop()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.RollTianZunDrop(0));
        Assert.False(OfficialPure15Apex101DetailsEngine.RollTianZunDrop(1));
    }

    [Fact]
    public void Test2310_IsAltarCornerTileBlocked()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.IsAltarCornerTileBlocked(10, 10, 10, 10));
        Assert.False(OfficialPure15Apex101DetailsEngine.IsAltarCornerTileBlocked(10, 10, 11, 10));
    }

    // =========================================================================
    // 7. 武器升级极品跳点与矿石纯度权威方程 (2311~2320)
    // =========================================================================

    [Fact]
    public void Test2311_CalculateWeaponUpgradeDuraChange()
    {
        var (d1, b1) = OfficialPure15Apex101DetailsEngine.CalculateWeaponUpgradeDuraChange(65, 30);
        Assert.Equal(1000, d1);
        Assert.True(b1);

        var (d2, b2) = OfficialPure15Apex101DetailsEngine.CalculateWeaponUpgradeDuraChange(40, 30);
        Assert.Equal(0, d2);
        Assert.False(b2);

        var (d3, _) = OfficialPure15Apex101DetailsEngine.CalculateWeaponUpgradeDuraChange(20, 30);
        Assert.Equal(-1000, d3);
    }

    [Fact]
    public void Test2312_CalculateUpgradeSuccessRate()
    {
        int rate1 = OfficialPure15Apex101DetailsEngine.CalculateUpgradeSuccessRate(0, 20);
        Assert.Equal(85, rate1);

        int rate2 = OfficialPure15Apex101DetailsEngine.CalculateUpgradeSuccessRate(5, 10);
        Assert.Equal(35, rate2);
    }

    [Fact]
    public void Test2313_CalculateUpgradeStatJump()
    {
        Assert.Equal(2, OfficialPure15Apex101DetailsEngine.CalculateUpgradeStatJump(2));
        Assert.Equal(1, OfficialPure15Apex101DetailsEngine.CalculateUpgradeStatJump(50));
    }

    [Fact]
    public void Test2314_CanSlashTargetForWeaponUpgrade()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.CanSlashTargetForWeaponUpgrade(true));
        Assert.False(OfficialPure15Apex101DetailsEngine.CanSlashTargetForWeaponUpgrade(false));
    }

    [Fact]
    public void Test2315_ShouldDiscardMineralResidue()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.ShouldDiscardMineralResidue());
    }

    [Fact]
    public void Test2316_CanUpgradeWeaponFurther()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.CanUpgradeWeaponFurther(6));
        Assert.False(OfficialPure15Apex101DetailsEngine.CanUpgradeWeaponFurther(7));
    }

    [Fact]
    public void Test2317_CalculateLuckyDamage()
    {
        int maxAtk = OfficialPure15Apex101DetailsEngine.CalculateLuckyDamage(10, 30, 9, 20);
        Assert.Equal(30, maxAtk);
    }

    [Fact]
    public void Test2318_RollBlessingOil()
    {
        var (s1, c1) = OfficialPure15Apex101DetailsEngine.RollBlessingOil(0, 50);
        Assert.True(s1);
        Assert.False(c1);

        var (s2, c2) = OfficialPure15Apex101DetailsEngine.RollBlessingOil(6, 96);
        Assert.False(s2);
        Assert.True(c2);
    }

    [Fact]
    public void Test2319_CalculateCursedDamage()
    {
        int minAtk = OfficialPure15Apex101DetailsEngine.CalculateCursedDamage(10, 30, 10, 50);
        Assert.Equal(10, minAtk);
    }

    [Fact]
    public void Test2320_CheckSpecialRepairWear()
    {
        var (m1, l1) = OfficialPure15Apex101DetailsEngine.CheckSpecialRepairWear(10000, 0);
        Assert.Equal(9000, m1);
        Assert.True(l1);

        var (m2, l2) = OfficialPure15Apex101DetailsEngine.CheckSpecialRepairWear(10000, 500);
        Assert.Equal(10000, m2);
        Assert.False(l2);
    }

    // =========================================================================
    // 8. 战法道三大职业核心战斗数值权威平衡 (2321~2330)
    // =========================================================================

    [Fact]
    public void Test2321_CalculateSwordThrustDamage()
    {
        int secondTileDmg = OfficialPure15Apex101DetailsEngine.CalculateSwordThrustDamage(100, 60, true);
        Assert.Equal(100, secondTileDmg); // 隔位刺杀完全穿透

        int firstTileDmg = OfficialPure15Apex101DetailsEngine.CalculateSwordThrustDamage(100, 60, false);
        Assert.Equal(40, firstTileDmg);
    }

    [Fact]
    public void Test2322_CheckDoubleFireStrike()
    {
        var (isDbl, dmg) = OfficialPure15Apex101DetailsEngine.CheckDoubleFireStrike(0, 9900, 100);
        Assert.True(isDbl);
        Assert.Equal(520, dmg);
    }

    [Fact]
    public void Test2323_CalculateBarbarianThrust()
    {
        var (p1, t1) = OfficialPure15Apex101DetailsEngine.CalculateBarbarianThrust(40, 35, 3);
        Assert.True(p1);
        Assert.Equal(2, t1);

        var (p2, _) = OfficialPure15Apex101DetailsEngine.CalculateBarbarianThrust(35, 40, 3);
        Assert.False(p2);
    }

    [Fact]
    public void Test2324_CalculateMageMagicShieldAbsorption()
    {
        var (abs, hp) = OfficialPure15Apex101DetailsEngine.CalculateMageMagicShieldAbsorption(100, 3);
        Assert.Equal(60, abs);
        Assert.Equal(40, hp);
    }

    [Fact]
    public void Test2325_CanTameMorePets()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.CanTameMorePets(4));
        Assert.False(OfficialPure15Apex101DetailsEngine.CanTameMorePets(5));
    }

    [Fact]
    public void Test2326_CheckPetRebellion()
    {
        const long SixHours = 6 * 3600 * 1000L;
        Assert.True(OfficialPure15Apex101DetailsEngine.CheckPetRebellion(1000, 1000 + SixHours + 10));
        Assert.False(OfficialPure15Apex101DetailsEngine.CheckPetRebellion(1000, 1000 + SixHours - 10));
    }

    [Fact]
    public void Test2327_GetSevenGradeDivineBeastStats()
    {
        var (hp, min, max, ac) = OfficialPure15Apex101DetailsEngine.GetSevenGradeDivineBeastStats();
        Assert.Equal(2400, hp);
        Assert.Equal(35, min);
        Assert.Equal(75, max);
        Assert.Equal(40, ac);
    }

    [Fact]
    public void Test2328_GetSevenGradeSkeletonAttackIntervalMs()
    {
        Assert.Equal(800, OfficialPure15Apex101DetailsEngine.GetSevenGradeSkeletonAttackIntervalMs());
    }

    [Fact]
    public void Test2329_CheckInvisibilityBreak()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.CheckInvisibilityBreak(true, false));
        Assert.True(OfficialPure15Apex101DetailsEngine.CheckInvisibilityBreak(false, true));
        Assert.False(OfficialPure15Apex101DetailsEngine.CheckInvisibilityBreak(false, false));
    }

    [Fact]
    public void Test2330_IsInMassInvisibilityRadius()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.IsInMassInvisibilityRadius(20, 20, 21, 21));
        Assert.False(OfficialPure15Apex101DetailsEngine.IsInMassInvisibilityRadius(20, 20, 22, 20));
    }

    // =========================================================================
    // 9. 沙巴克攻城战皇宫占领与城门箭塔攻防 (2331~2340)
    // =========================================================================

    [Fact]
    public void Test2331_GetSabukGateHealthStage()
    {
        Assert.Equal("Intact", OfficialPure15Apex101DetailsEngine.GetSabukGateHealthStage(45000));
        Assert.Equal("MediumDamage", OfficialPure15Apex101DetailsEngine.GetSabukGateHealthStage(25000));
        Assert.Equal("HeavyDamage", OfficialPure15Apex101DetailsEngine.GetSabukGateHealthStage(10000));
        Assert.Equal("Destroyed", OfficialPure15Apex101DetailsEngine.GetSabukGateHealthStage(0));
    }

    [Fact]
    public void Test2332_CalculateGateRepairCost()
    {
        var (hp, cost) = OfficialPure15Apex101DetailsEngine.CalculateGateRepairCost(10000);
        Assert.Equal(10000, hp);
        Assert.Equal(100000, cost);
    }

    [Fact]
    public void Test2333_IsValidPalaceEntrance()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.IsValidPalaceEntrance(123, 160));
        Assert.True(OfficialPure15Apex101DetailsEngine.IsValidPalaceEntrance(120, 155));
        Assert.False(OfficialPure15Apex101DetailsEngine.IsValidPalaceEntrance(100, 100));
    }

    [Fact]
    public void Test2334_CheckPalaceVictory()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.CheckPalaceVictory(10, 0));
        Assert.False(OfficialPure15Apex101DetailsEngine.CheckPalaceVictory(10, 1));
    }

    [Fact]
    public void Test2335_IsSafeZoneActiveDuringSiege()
    {
        Assert.False(OfficialPure15Apex101DetailsEngine.IsSafeZoneActiveDuringSiege(true));
        Assert.True(OfficialPure15Apex101DetailsEngine.IsSafeZoneActiveDuringSiege(false));
    }

    [Fact]
    public void Test2336_CanRedNameEnterSabukTreasureRoom()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.CanRedNameEnterSabukTreasureRoom(150));
        Assert.False(OfficialPure15Apex101DetailsEngine.CanRedNameEnterSabukTreasureRoom(200));
    }

    [Fact]
    public void Test2337_CanExtractSabukTax()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.CanExtractSabukTax(true, false));
        Assert.False(OfficialPure15Apex101DetailsEngine.CanExtractSabukTax(false, false));
        Assert.False(OfficialPure15Apex101DetailsEngine.CanExtractSabukTax(true, true));
    }

    [Fact]
    public void Test2338_CalculateSiegeDeathDropChance()
    {
        Assert.Equal(25, OfficialPure15Apex101DetailsEngine.CalculateSiegeDeathDropChance(50, true));
        Assert.Equal(50, OfficialPure15Apex101DetailsEngine.CalculateSiegeDeathDropChance(50, false));
    }

    [Fact]
    public void Test2339_CalculateSabukShopDiscountPrice()
    {
        Assert.Equal(800, OfficialPure15Apex101DetailsEngine.CalculateSabukShopDiscountPrice(1000));
    }

    [Fact]
    public void Test2340_IsArcherArrowUndodgeable()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.IsArcherArrowUndodgeable());
    }

    // =========================================================================
    // 10. Direct-Blit 强类型封包与 Dumb Client 纯血架构 (2341~2350)
    // =========================================================================

    [Fact]
    public void Test2341_ValidateDirectBlitMagic()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.ValidateDirectBlitMagic(0xAA55));
        Assert.False(OfficialPure15Apex101DetailsEngine.ValidateDirectBlitMagic(0x1234));
    }

    [Fact]
    public void Test2342_ValidatePacketSequence()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.ValidatePacketSequence(100, 101));
        Assert.False(OfficialPure15Apex101DetailsEngine.ValidatePacketSequence(100, 100));
        Assert.False(OfficialPure15Apex101DetailsEngine.ValidatePacketSequence(100, 99));
    }

    [Fact]
    public void Test2343_IsDamageCalculatedByServerOnly()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.IsDamageCalculatedByServerOnly());
    }

    [Fact]
    public void Test2344_IsDropCalculatedByServerOnly()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.IsDropCalculatedByServerOnly());
    }

    [Fact]
    public void Test2345_IsPotionPoolManagedByServerOnly()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.IsPotionPoolManagedByServerOnly());
    }

    [Fact]
    public void Test2346_IsMiningCalculatedByServerOnly()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.IsMiningCalculatedByServerOnly());
    }

    [Fact]
    public void Test2347_GetDirectBlitHeaderSize()
    {
        Assert.Equal(8, OfficialPure15Apex101DetailsEngine.GetDirectBlitHeaderSize());
    }

    [Fact]
    public void Test2348_CheckHeartbeatTimeout()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.CheckHeartbeatTimeout(1000, 1000 + 16000));
        Assert.False(OfficialPure15Apex101DetailsEngine.CheckHeartbeatTimeout(1000, 1000 + 14000));
    }

    [Fact]
    public void Test2349_IsNativeAotCompliant()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.IsNativeAotCompliant());
    }

    [Fact]
    public void Test2350_IsReleaseSandboxPermanentlyRemoved()
    {
        Assert.True(OfficialPure15Apex101DetailsEngine.IsReleaseSandboxPermanentlyRemoved());
    }
}
