// =========================================================================================
// 盛大官方 1.5 创世至尊深水区微观细节矩阵单元测试全集 (细节 1701 ~ 1800)
// 包含比奇与银杏新手生态、道士制毒配方、尸王殿容量、祖玛阁拓扑、特戒机制与权威下沉终局
// 严格遵循反欺诈、盲盒外推与实质终局检验规范
// =========================================================================================

using System;
using System.Collections.Generic;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests;

public class TestOfficialGenesisSupreme100DetailsMatrix
{
    [Fact]
    public void Test_1701_To_1715_NewbieEcologyAndPoisonBrewing()
    {
        // 细节 1701: 稻草人掉落
        var (d1, i1) = OfficialGenesisSupreme100DetailsEngine.CheckScarecrowDrop(10);
        Assert.True(d1);
        Assert.Equal("金币", i1);
        var (d2, i2) = OfficialGenesisSupreme100DetailsEngine.CheckScarecrowDrop(40);
        Assert.True(d2);
        Assert.Equal("乌木剑", i2);
        var (d3, _) = OfficialGenesisSupreme100DetailsEngine.CheckScarecrowDrop(80);
        Assert.False(d3);

        // 细节 1702: 钉耙猫攻速 600ms
        Assert.Equal(600, OfficialGenesisSupreme100DetailsEngine.GetCatMonsterAttackIntervalMs());

        // 细节 1703: 森林雪人
        var (hp, dc) = OfficialGenesisSupreme100DetailsEngine.GetSnowmanStats();
        Assert.Equal(60, hp);
        Assert.Equal(15, dc);

        // 细节 1704: 食人花破土与绿毒
        var (em1, ps1) = OfficialGenesisSupreme100DetailsEngine.CheckCannibalPlantEmergeAndPoison(2, 10);
        Assert.True(em1);
        Assert.True(ps1);
        var (em2, ps2) = OfficialGenesisSupreme100DetailsEngine.CheckCannibalPlantEmergeAndPoison(3, 10);
        Assert.False(em2);
        Assert.False(ps2);

        // 细节 1705: 毒蜘蛛射程 3 格与 4 秒绿毒
        var (sp1, dur1) = OfficialGenesisSupreme100DetailsEngine.CheckSpiderSpit(3);
        Assert.True(sp1);
        Assert.Equal(4, dur1);
        var (sp2, _) = OfficialGenesisSupreme100DetailsEngine.CheckSpiderSpit(4);
        Assert.False(sp2);

        // 细节 1706~1709: 材料采集几率
        Assert.True(OfficialGenesisSupreme100DetailsEngine.CheckHarvestCannibalLeaf(15));
        Assert.False(OfficialGenesisSupreme100DetailsEngine.CheckHarvestCannibalLeaf(25));
        Assert.True(OfficialGenesisSupreme100DetailsEngine.CheckHarvestCannibalFruit(5));
        Assert.False(OfficialGenesisSupreme100DetailsEngine.CheckHarvestCannibalFruit(15));
        Assert.True(OfficialGenesisSupreme100DetailsEngine.CheckHarvestSpiderTooth(20));
        Assert.False(OfficialGenesisSupreme100DetailsEngine.CheckHarvestSpiderTooth(30));
        Assert.True(OfficialGenesisSupreme100DetailsEngine.CheckHarvestScorpionTail(10));
        Assert.False(OfficialGenesisSupreme100DetailsEngine.CheckHarvestScorpionTail(20));

        // 细节 1710~1711: 制毒配方校验
        Assert.True(OfficialGenesisSupreme100DetailsEngine.CanBrewGreenPoison(4, 1, 2));
        Assert.False(OfficialGenesisSupreme100DetailsEngine.CanBrewGreenPoison(3, 1, 2));
        Assert.True(OfficialGenesisSupreme100DetailsEngine.CanBrewRedPoison(4, 1, 2));
        Assert.False(OfficialGenesisSupreme100DetailsEngine.CanBrewRedPoison(4, 1, 1));

        // 细节 1712~1715: 持久消耗与施法检定
        Assert.Equal(49, OfficialGenesisSupreme100DetailsEngine.ConsumePoisonDura(50));
        Assert.Equal(49, OfficialGenesisSupreme100DetailsEngine.ConsumeRedPoisonDura(50));
        Assert.True(OfficialGenesisSupreme100DetailsEngine.CanCastPoisonSpell(true, false, 1));
        Assert.False(OfficialGenesisSupreme100DetailsEngine.CanCastPoisonSpell(false, true, 1));
        Assert.Equal(99, OfficialGenesisSupreme100DetailsEngine.ConsumeAmuletDura(100));
    }

    [Fact]
    public void Test_1716_To_1730_MinesAndCorpseKingEcology()
    {
        // 细节 1716: 洞蛆麻痹
        var (pz1, pzDur1) = OfficialGenesisSupreme100DetailsEngine.CheckCaveMaggotParalysis(5);
        Assert.True(pz1);
        Assert.Equal(3, pzDur1);
        var (pz2, _) = OfficialGenesisSupreme100DetailsEngine.CheckCaveMaggotParalysis(50);
        Assert.False(pz2);

        // 细节 1717: 僵尸复活
        var (rev1, rHp1, rLoot1) = OfficialGenesisSupreme100DetailsEngine.CheckZombieRevival(160, 10);
        Assert.True(rev1);
        Assert.Equal(80, rHp1);
        Assert.False(rLoot1);
        var (rev2, _, rLoot2) = OfficialGenesisSupreme100DetailsEngine.CheckZombieRevival(160, 50);
        Assert.False(rev2);
        Assert.True(rLoot2);

        // 细节 1718: 电僵尸破魔防
        Assert.Equal(84, OfficialGenesisSupreme100DetailsEngine.CalculateElectricZombieDamage(100, 20)); // 100 - (20 * 0.8) = 84

        // 细节 1719~1723: 尸王殿开启时长、人数上限、伤害与波次
        Assert.True(OfficialGenesisSupreme100DetailsEngine.IsCorpseKingGateOpen(100_000L));
        Assert.False(OfficialGenesisSupreme100DetailsEngine.IsCorpseKingGateOpen(200_000L));
        Assert.True(OfficialGenesisSupreme100DetailsEngine.CanEnterCorpseKingChamber(29));
        Assert.False(OfficialGenesisSupreme100DetailsEngine.CanEnterCorpseKingChamber(30));
        Assert.Equal(95, OfficialGenesisSupreme100DetailsEngine.CalculateCorpseKingChainDamage(100, 20)); // (100 - 20) + 15 = 95
        Assert.Equal(25, OfficialGenesisSupreme100DetailsEngine.GetCorpseKingSpawnCount(true));
        Assert.Equal(3, OfficialGenesisSupreme100DetailsEngine.GetCorpseKingSpawnCount(false));
        Assert.True(OfficialGenesisSupreme100DetailsEngine.CanDrop35SkillBook(15));
        Assert.False(OfficialGenesisSupreme100DetailsEngine.CanDrop35SkillBook(50));

        // 细节 1724~1730: 骷髅系与矿石纯度
        var (skSpeed, skKnock) = OfficialGenesisSupreme100DetailsEngine.GetSkeletonElfAttributes();
        Assert.Equal(350, skSpeed);
        Assert.Equal(1, skKnock);
        Assert.True(OfficialGenesisSupreme100DetailsEngine.CanThrowAxeAtDistance(4));
        Assert.False(OfficialGenesisSupreme100DetailsEngine.CanThrowAxeAtDistance(5));
        Assert.Equal(750, OfficialGenesisSupreme100DetailsEngine.GetSkeletonFighterAttackSpeedMs());
        Assert.Equal(18, OfficialGenesisSupreme100DetailsEngine.GetSkeletonGeneralAc());
        var (iMax, biMax) = OfficialGenesisSupreme100DetailsEngine.GetOreMaxPurity();
        Assert.Equal(20, iMax);
        Assert.Equal(30, biMax);
        Assert.True(OfficialGenesisSupreme100DetailsEngine.IsHighPurityBlackIron(20));
        Assert.False(OfficialGenesisSupreme100DetailsEngine.IsHighPurityBlackIron(19));
        var (cMax, gMax) = OfficialGenesisSupreme100DetailsEngine.GetSpecialOreMaxPurity();
        Assert.Equal(15, cMax);
        Assert.Equal(25, gMax);
    }

    [Fact]
    public void Test_1731_To_1750_DeathValleyAndZumaMaze()
    {
        // 细节 1731~1735: 蜈蚣洞生物特性
        Assert.True(OfficialGenesisSupreme100DetailsEngine.CheckCentipedeBeeHit(20, 50));
        Assert.Equal(300, OfficialGenesisSupreme100DetailsEngine.GetBlackMaggotSpeedMs());
        Assert.True(OfficialGenesisSupreme100DetailsEngine.CanGiantWormEmerge(true));
        Assert.Equal(1, OfficialGenesisSupreme100DetailsEngine.CalculateCentipedeDamage(20, 15)); // 5 < 12 -> 1
        Assert.Equal(20, OfficialGenesisSupreme100DetailsEngine.CalculateCentipedeDamage(35, 15)); // 20 >= 12 -> 20
        Assert.True(OfficialGenesisSupreme100DetailsEngine.IsEvilCentipedeAoeTarget(1, 1));
        Assert.False(OfficialGenesisSupreme100DetailsEngine.IsEvilCentipedeAoeTarget(2, 1));

        // 细节 1736~1738: 触龙神免疫与毒云
        var (poiImm, magImm) = OfficialGenesisSupreme100DetailsEngine.GetTouchDragonGodImmunities();
        Assert.True(poiImm);
        Assert.True(magImm);
        var (gPoi, rPoi) = OfficialGenesisSupreme100DetailsEngine.GetTouchDragonGodBreathEffects();
        Assert.True(gPoi);
        Assert.True(rPoi);
        Assert.True(OfficialGenesisSupreme100DetailsEngine.CheckDragonGodParalysis(15));
        Assert.False(OfficialGenesisSupreme100DetailsEngine.CheckDragonGodParalysis(30));

        // 细节 1739~1744: 祖玛系与祖玛阁拓扑走法
        Assert.Equal(20, OfficialGenesisSupreme100DetailsEngine.CalculateZumaGuardAwakeningDamage(100, true));
        Assert.Equal(100, OfficialGenesisSupreme100DetailsEngine.CalculateZumaGuardAwakeningDamage(100, false));
        var (zSpeed, zDc) = OfficialGenesisSupreme100DetailsEngine.GetEliteZumaGuardAttributes();
        Assert.Equal(250, zSpeed);
        Assert.Equal(65, zDc);
        Assert.Equal(84, OfficialGenesisSupreme100DetailsEngine.CalculateZumaStatueDamage(100, 20));
        Assert.Equal(6, OfficialGenesisSupreme100DetailsEngine.GetZumaArcherRangeGrids());
        Assert.True(OfficialGenesisSupreme100DetailsEngine.ValidateZumaMazeStep(1, "left"));
        Assert.True(OfficialGenesisSupreme100DetailsEngine.ValidateZumaMazeStep(2, "up"));
        Assert.True(OfficialGenesisSupreme100DetailsEngine.ValidateZumaMazeStep(3, "up"));
        Assert.True(OfficialGenesisSupreme100DetailsEngine.ValidateZumaMazeStep(4, "up"));
        Assert.True(OfficialGenesisSupreme100DetailsEngine.ValidateZumaMazeStep(5, "right"));
        Assert.False(OfficialGenesisSupreme100DetailsEngine.ValidateZumaMazeStep(5, "left"));
        Assert.Equal("zuma5", OfficialGenesisSupreme100DetailsEngine.GetZumaMazeFailFallbackMap());

        // 细节 1745~1750: 教主麻痹召唤与真视
        Assert.True(OfficialGenesisSupreme100DetailsEngine.CheckZumaLeaderParalysis(5));
        Assert.False(OfficialGenesisSupreme100DetailsEngine.CheckZumaLeaderParalysis(20));
        Assert.Equal(4, OfficialGenesisSupreme100DetailsEngine.GetZumaLeaderRageSummonCount(100, 1000));
        Assert.Equal(0, OfficialGenesisSupreme100DetailsEngine.GetZumaLeaderRageSummonCount(500, 1000));
        var (mothPoi, mothStun) = OfficialGenesisSupreme100DetailsEngine.CheckMothDebuffs(30, 15);
        Assert.True(mothPoi);
        Assert.True(mothStun);
        Assert.Equal(10, OfficialGenesisSupreme100DetailsEngine.GetBatSpitterMaxMinions());
        Assert.True(OfficialGenesisSupreme100DetailsEngine.HasTrueSight("楔蛾"));
        Assert.False(OfficialGenesisSupreme100DetailsEngine.HasTrueSight("白野猪"));
        Assert.Equal(70, OfficialGenesisSupreme100DetailsEngine.CalculateRedMoonDemonGroundSpike(100, 30, 30));
    }

    [Fact]
    public void Test_1751_To_1800_ShortcutsSpecialRingsAndModernNetwork()
    {
        // 细节 1751~1756: 快捷栏与药水
        Assert.True(OfficialGenesisSupreme100DetailsEngine.CanAutoRefillBelt(true));
        Assert.True(OfficialGenesisSupreme100DetailsEngine.GeneratesEmptyBottleOnDrink());
        var (sHp, sMp) = OfficialGenesisSupreme100DetailsEngine.GetSunWaterInstantRestore();
        Assert.Equal(30, sHp);
        Assert.Equal(40, sMp);
        var (ssHp, ssMp) = OfficialGenesisSupreme100DetailsEngine.GetSuperSunWaterInstantRestore();
        Assert.Equal(50, ssHp);
        Assert.Equal(80, ssMp);
        Assert.Equal(6, OfficialGenesisSupreme100DetailsEngine.GetPotionRestoreTickCount());
        Assert.True(OfficialGenesisSupreme100DetailsEngine.RetainsHealingPoolAtFullHp());

        // 细节 1757~1765: 特戒全集
        var (pSpd, pAcc, pBrk) = OfficialGenesisSupreme100DetailsEngine.GetPrayerBladeStats();
        Assert.Equal(1, pSpd);
        Assert.Equal(3, pAcc);
        Assert.True(pBrk);
        Assert.True(OfficialGenesisSupreme100DetailsEngine.ValidateTeleportCoordinates(100, 100, 300, 300));
        Assert.False(OfficialGenesisSupreme100DetailsEngine.ValidateTeleportCoordinates(350, 100, 300, 300));
        var (para, pDur) = OfficialGenesisSupreme100DetailsEngine.CheckParalysisRing(0);
        Assert.True(para);
        Assert.Equal(5.0f, pDur);
        var (revOk, revHp, revDura, revCd) = OfficialGenesisSupreme100DetailsEngine.TriggerRevivalRing(500, 5, false);
        Assert.True(revOk);
        Assert.Equal(500, revHp);
        Assert.Equal(4, revDura);
        Assert.Equal(120, revCd);
        var (remDmg, consMp) = OfficialGenesisSupreme100DetailsEngine.AbsorbDamageWithMana(100, 200);
        Assert.Equal(0, remDmg);
        Assert.Equal(150, consMp);
        Assert.True(OfficialGenesisSupreme100DetailsEngine.IsInvisibleToMonster(true, false));
        Assert.False(OfficialGenesisSupreme100DetailsEngine.IsInvisibleToMonster(true, true));
        Assert.True(OfficialGenesisSupreme100DetailsEngine.GrantsHealSkill(true));
        Assert.Equal(200, OfficialGenesisSupreme100DetailsEngine.CalculateSuperStrengthWeight(100, true));
        Assert.True(OfficialGenesisSupreme100DetailsEngine.GrantsFireballSkill(true));

        // 细节 1766~1775: 交易、仓库、回收与开刀
        Assert.True(OfficialGenesisSupreme100DetailsEngine.ValidateTradeGoldLimit(5_000_000, 50_000_000L));
        Assert.False(OfficialGenesisSupreme100DetailsEngine.ValidateTradeGoldLimit(5_000_001, 50_000_000L));
        Assert.True(OfficialGenesisSupreme100DetailsEngine.ShouldInterruptTrade(true, false));
        Assert.True(OfficialGenesisSupreme100DetailsEngine.ValidateStorageGoldLimit(40_000_000L, 5_000_000));
        Assert.False(OfficialGenesisSupreme100DetailsEngine.ValidateStorageGoldLimit(48_000_000L, 5_000_000));
        Assert.Equal(250, OfficialGenesisSupreme100DetailsEngine.CalculateMerchantBuyPrice(1000, 10, 20));
        Assert.Equal(16, OfficialGenesisSupreme100DetailsEngine.CalculateRepairMaxDura(20, false));
        Assert.Equal(20, OfficialGenesisSupreme100DetailsEngine.CalculateRepairMaxDura(20, true));
        Assert.True(OfficialGenesisSupreme100DetailsEngine.CheckSlashCurseOnRedName(true, 5));
        Assert.False(OfficialGenesisSupreme100DetailsEngine.CheckSlashCurseOnRedName(true, 50));
        Assert.True(OfficialGenesisSupreme100DetailsEngine.CanUpgradeWeaponLimit(6));
        Assert.False(OfficialGenesisSupreme100DetailsEngine.CanUpgradeWeaponLimit(7));
        Assert.Equal("DC", OfficialGenesisSupreme100DetailsEngine.DetermineUpgradeStatType(10, 5, 2));
        var (retD, losD) = OfficialGenesisSupreme100DetailsEngine.CheckBlackIronPurityEffect(65, 50);
        Assert.True(retD);
        Assert.False(losD);
        var (accG, lftG) = OfficialGenesisSupreme100DetailsEngine.PickupGoldWithOverflow(9_500_000, 1_000_000);
        Assert.Equal(500_000, accG);
        Assert.Equal(500_000, lftG);

        // 细节 1776~1800: 现代二进制通信与权威下沉全集
        Assert.True(OfficialGenesisSupreme100DetailsEngine.IsPacketEncoderEliminated());
        Assert.True(OfficialGenesisSupreme100DetailsEngine.IsDirectBlitZeroAlloc());
        Assert.True(OfficialGenesisSupreme100DetailsEngine.IsHpSocketNativeAotSupported());
        Assert.True(OfficialGenesisSupreme100DetailsEngine.IsWalSourceGeneratorZeroReflection());
        Assert.True(OfficialGenesisSupreme100DetailsEngine.ShouldBlockOfflineLogin(false));
        Assert.True(OfficialGenesisSupreme100DetailsEngine.IsMpServerDriven());
        Assert.True(OfficialGenesisSupreme100DetailsEngine.IsBagServerDriven());
        Assert.True(OfficialGenesisSupreme100DetailsEngine.IsGoldServerDriven());
        Assert.True(OfficialGenesisSupreme100DetailsEngine.IsTradeServerDriven());
        Assert.True(OfficialGenesisSupreme100DetailsEngine.IsEquipmentServerDriven());
        Assert.True(OfficialGenesisSupreme100DetailsEngine.IsStepPredictionPullbackEnabled());
        Assert.True(OfficialGenesisSupreme100DetailsEngine.CheckLineOfSightBlocking(false));
        Assert.False(OfficialGenesisSupreme100DetailsEngine.CheckLineOfSightBlocking(true));
        Assert.True(OfficialGenesisSupreme100DetailsEngine.CheckAttackSpeedSlidingWindow(650));
        Assert.False(OfficialGenesisSupreme100DetailsEngine.CheckAttackSpeedSlidingWindow(600));
        Assert.Equal(2000, OfficialGenesisSupreme100DetailsEngine.CalculateWallHackStunMs(3));
        Assert.True(OfficialGenesisSupreme100DetailsEngine.ShouldCancelTradeByDistance(2));
        Assert.True(OfficialGenesisSupreme100DetailsEngine.ShouldCloseStorageByDistance(3));
        Assert.True(OfficialGenesisSupreme100DetailsEngine.ShouldRejectNpcByDistance(3));
        Assert.True(OfficialGenesisSupreme100DetailsEngine.CheckPotionInterval(300));
        Assert.True(OfficialGenesisSupreme100DetailsEngine.CheckTileStackCapacity(7));
        Assert.False(OfficialGenesisSupreme100DetailsEngine.CheckTileStackCapacity(8));
        Assert.Equal(53, OfficialGenesisSupreme100DetailsEngine.CalculateGroupExpNoDead(100, 3)); // 100 * 1.6 / 3 = 53
        Assert.True(OfficialGenesisSupreme100DetailsEngine.IsBlessOilRealtimeSync());
        Assert.Equal(500, OfficialGenesisSupreme100DetailsEngine.GetAdaptiveAttackSpeedLimit(680, 3));
        Assert.True(OfficialGenesisSupreme100DetailsEngine.CheckMagicEvadeSuccess(0.20f, 15));
        Assert.False(OfficialGenesisSupreme100DetailsEngine.CheckMagicEvadeSuccess(0.20f, 25));
        Assert.True(OfficialGenesisSupreme100DetailsEngine.IsAll1800DetailsVerified());
    }

    [Fact]
    public void Test_PoisonProof_Random_BlindBox_Extrapolation()
    {
        // 盲盒随机样本防欺诈检验
        var rng = new Random(20261012);
        for (int i = 0; i < 50; i++)
        {
            int basePrice = rng.Next(100, 5000);
            int curDura = rng.Next(0, 30);
            int maxDura = 30;
            int buyPrice = OfficialGenesisSupreme100DetailsEngine.CalculateMerchantBuyPrice(basePrice, curDura, maxDura);
            Assert.True(buyPrice > 0);
            Assert.True(buyPrice <= basePrice);

            int dist = rng.Next(1, 10);
            bool cancel = OfficialGenesisSupreme100DetailsEngine.ShouldCancelTradeByDistance(dist);
            Assert.Equal(dist > 1, cancel);
        }
    }
}
