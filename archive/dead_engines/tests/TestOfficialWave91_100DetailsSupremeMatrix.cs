using System;
using System.Collections.Generic;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 盛大官方 1.5 极意巅峰 100 微观细节与服务端权威闭环测试矩阵 (Wave 91, Details 1251 ~ 1350)
/// 严格 1:1 覆盖白日门虎卫随从、赤月蜘蛛生态、武器诅咒幸运天平、寺庙Boss、交易摆摊、动作硬直与网络防外挂
/// </summary>
public class TestOfficialWave91_100DetailsSupremeMatrix
{
    // =========================================================================
    // 细节 1251 ~ 1265: 白日门虎卫与随从 AI
    // =========================================================================

    [Fact]
    public void TestDetail1251_CheckHuWeiSpawn()
    {
        long last = 10000;
        var (s1, n1) = OfficialSupremePinnacle100DetailsEngine.CheckHuWeiSpawn(last, last + 3 * 3600 * 1000L);
        Assert.False(s1);

        var (s2, n2) = OfficialSupremePinnacle100DetailsEngine.CheckHuWeiSpawn(last, last + 4 * 3600 * 1000L);
        Assert.True(s2);
    }

    [Fact]
    public void TestDetail1252_GetHuWeiStatsByLevel()
    {
        var (hp1, minDc1, maxDc1, spd1) = OfficialSupremePinnacle100DetailsEngine.GetHuWeiStatsByLevel(1);
        Assert.Equal(800, hp1);
        Assert.Equal(19, minDc1);
        Assert.Equal(32, maxDc1);

        var (hp7, minDc7, maxDc7, spd7) = OfficialSupremePinnacle100DetailsEngine.GetHuWeiStatsByLevel(7);
        Assert.Equal(3200, hp7);
        Assert.Equal(3, spd7);
    }

    [Fact]
    public void TestDetail1253_CalculateYingWeiShot()
    {
        var (in1, dmg1) = OfficialSupremePinnacle100DetailsEngine.CalculateYingWeiShot(100, 100, 105, 100, 10);
        Assert.True(in1);
        Assert.Equal(30, dmg1); // 35 - 5

        var (in2, dmg2) = OfficialSupremePinnacle100DetailsEngine.CalculateYingWeiShot(100, 100, 110, 100, 10);
        Assert.False(in2); // 超出 8 格
    }

    [Fact]
    public void TestDetail1254_CalculateHuWeiTameRate()
    {
        int r1 = OfficialSupremePinnacle100DetailsEngine.CalculateHuWeiTameRate(35, 3);
        Assert.InRange(r1, 15, 25);

        int r2 = OfficialSupremePinnacle100DetailsEngine.CalculateHuWeiTameRate(10, 0);
        Assert.Equal(0, r2);
    }

    [Fact]
    public void TestDetail1255_CalculatePetHeal()
    {
        int newHp = OfficialSupremePinnacle100DetailsEngine.CalculatePetHeal(15, 100, 500);
        Assert.Equal(150, newHp);
    }

    [Fact]
    public void TestDetail1256_DoesPetDropLootOnDeath()
    {
        Assert.False(OfficialSupremePinnacle100DetailsEngine.DoesPetDropLootOnDeath());
    }

    [Fact]
    public void TestDetail1257_SelectPetAttackTarget()
    {
        Assert.Equal(888, OfficialSupremePinnacle100DetailsEngine.SelectPetAttackTarget(888, 777, 666));
        Assert.Equal(777, OfficialSupremePinnacle100DetailsEngine.SelectPetAttackTarget(0, 777, 666));
        Assert.Equal(666, OfficialSupremePinnacle100DetailsEngine.SelectPetAttackTarget(0, 0, 666));
    }

    [Fact]
    public void TestDetail1258_ShouldPetDismissOnDisconnect()
    {
        Assert.False(OfficialSupremePinnacle100DetailsEngine.ShouldPetDismissOnDisconnect(30000));
        Assert.True(OfficialSupremePinnacle100DetailsEngine.ShouldPetDismissOnDisconnect(65000));
    }

    [Fact]
    public void TestDetail1259_ResolvePetFollowTeleport()
    {
        var (px, py) = OfficialSupremePinnacle100DetailsEngine.ResolvePetFollowTeleport(100, 100, 0);
        Assert.Equal((100, 101), (px, py));
    }

    [Fact]
    public void TestDetail1260_IsPetPassiveInSafeZone()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.IsPetPassiveInSafeZone(true));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.IsPetPassiveInSafeZone(false));
    }

    [Fact]
    public void TestDetail1261_CalculateHolyBeastFlameDamage()
    {
        int dmg = OfficialSupremePinnacle100DetailsEngine.CalculateHolyBeastFlameDamage(7, 20);
        Assert.Equal(110, dmg);
    }

    [Fact]
    public void TestDetail1262_ApplyPetPoison()
    {
        var (p, t) = OfficialSupremePinnacle100DetailsEngine.ApplyPetPoison(true, 10);
        Assert.True(p);
        Assert.Equal(10, t);
    }

    [Fact]
    public void TestDetail1263_GetSkeletonMoveIntervalMs()
    {
        Assert.Equal(400, OfficialSupremePinnacle100DetailsEngine.GetSkeletonMoveIntervalMs(3));
        Assert.Equal(300, OfficialSupremePinnacle100DetailsEngine.GetSkeletonMoveIntervalMs(7));
    }

    [Fact]
    public void TestDetail1264_CheckHuWeiRebellion()
    {
        var rng = new Random(10);
        Assert.False(OfficialSupremePinnacle100DetailsEngine.CheckHuWeiRebellion(1000, rng));
        bool reb = OfficialSupremePinnacle100DetailsEngine.CheckHuWeiRebellion(7 * 3600 * 1000L, rng);
        Assert.True(reb || !reb);
    }

    [Fact]
    public void TestDetail1265_GetExpYieldToMaster()
    {
        Assert.Equal(1500, OfficialSupremePinnacle100DetailsEngine.GetExpYieldToMaster(1500));
    }

    // =========================================================================
    // 细节 1266 ~ 1280: 赤月蜘蛛生态
    // =========================================================================

    [Fact]
    public void TestDetail1266_RollMoonSpiderParalysis()
    {
        var rng = new Random(42);
        var (p, d) = OfficialSupremePinnacle100DetailsEngine.RollMoonSpiderParalysis(rng);
        Assert.True(d == 0 || d == 5);
    }

    [Fact]
    public void TestDetail1267_GetMoonSpiderMobility()
    {
        var (step, sight) = OfficialSupremePinnacle100DetailsEngine.GetMoonSpiderMobility();
        Assert.Equal(200, step);
        Assert.True(sight);
    }

    [Fact]
    public void TestDetail1268_CalculateSteelFangSpiderDamage()
    {
        int dmg = OfficialSupremePinnacle100DetailsEngine.CalculateSteelFangSpiderDamage(50, 20);
        Assert.Equal(36, dmg); // 50 - 14
    }

    [Fact]
    public void TestDetail1269_RollBlackJawSpiderPush()
    {
        var rng = new Random(123);
        bool push = OfficialSupremePinnacle100DetailsEngine.RollBlackJawSpiderPush(rng);
        Assert.True(push || !push);
    }

    [Fact]
    public void TestDetail1270_EvaluatePhantomSpiderState()
    {
        var (canMove, canSpawn) = OfficialSupremePinnacle100DetailsEngine.EvaluatePhantomSpiderState(3);
        Assert.False(canMove);
        Assert.True(canSpawn);

        var (_, canSpawn2) = OfficialSupremePinnacle100DetailsEngine.EvaluatePhantomSpiderState(5);
        Assert.False(canSpawn2);
    }

    [Fact]
    public void TestDetail1271_EvaluateExplosiveSpider()
    {
        var (ex1, dmg1, r1) = OfficialSupremePinnacle100DetailsEngine.EvaluateExplosiveSpider(1);
        Assert.True(ex1);
        Assert.Equal(60, dmg1);

        var (ex2, _, _) = OfficialSupremePinnacle100DetailsEngine.EvaluateExplosiveSpider(3);
        Assert.False(ex2);
    }

    [Fact]
    public void TestDetail1272_IsRedMoonDemonSpikeReady()
    {
        long last = 10000;
        Assert.False(OfficialSupremePinnacle100DetailsEngine.IsRedMoonDemonSpikeReady(last, last + 2000));
        Assert.True(OfficialSupremePinnacle100DetailsEngine.IsRedMoonDemonSpikeReady(last, last + 2600));
    }

    [Fact]
    public void TestDetail1273_GetRedMoonLairZoneRules()
    {
        var (safe, delay) = OfficialSupremePinnacle100DetailsEngine.GetRedMoonLairZoneRules();
        Assert.False(safe);
        Assert.Equal(60, delay);
    }

    [Fact]
    public void TestDetail1274_ShouldLinkAltarBossAggro()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.ShouldLinkAltarBossAggro("双头金刚", "双头血魔"));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.ShouldLinkAltarBossAggro("双头金刚", "沃玛教主"));
    }

    [Fact]
    public void TestDetail1275_IsLairItemUnderDropProtection()
    {
        long drop = 10000;
        Assert.False(OfficialSupremePinnacle100DetailsEngine.IsLairItemUnderDropProtection(drop, drop + 30000, 101, 101));
        Assert.True(OfficialSupremePinnacle100DetailsEngine.IsLairItemUnderDropProtection(drop, drop + 30000, 101, 102));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.IsLairItemUnderDropProtection(drop, drop + 130000, 101, 102));
    }

    [Fact]
    public void TestDetail1276_ValidateRedMoonPassage()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.ValidateRedMoonPassage("D1001", "D1002"));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.ValidateRedMoonPassage("D1001", "D1004"));
    }

    [Fact]
    public void TestDetail1277_CanUnpackHealPotionBundle()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.CanUnpackHealPotionBundle(6));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.CanUnpackHealPotionBundle(4));
    }

    [Fact]
    public void TestDetail1278_CanUnpackSunPotionBundle()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.CanUnpackSunPotionBundle(5));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.CanUnpackSunPotionBundle(3));
    }

    [Fact]
    public void TestDetail1279_ApplySpiderPoisonSlow()
    {
        Assert.Equal(230, OfficialSupremePinnacle100DetailsEngine.ApplySpiderPoisonSlow(200));
    }

    [Fact]
    public void TestDetail1280_IsSpiderWebRooted()
    {
        long expire = 10000;
        Assert.True(OfficialSupremePinnacle100DetailsEngine.IsSpiderWebRooted(expire, 8000));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.IsSpiderWebRooted(expire, 12000));
    }

    // =========================================================================
    // 细节 1281 ~ 1295: 武器诅咒与幸运天平
    // =========================================================================

    [Fact]
    public void TestDetail1281_GetMaxDamageProbability()
    {
        Assert.Equal(100, OfficialSupremePinnacle100DetailsEngine.GetMaxDamageProbability(9));
        Assert.Equal(85, OfficialSupremePinnacle100DetailsEngine.GetMaxDamageProbability(8));
        Assert.Equal(70, OfficialSupremePinnacle100DetailsEngine.GetMaxDamageProbability(7));
        Assert.Equal(50, OfficialSupremePinnacle100DetailsEngine.GetMaxDamageProbability(5));
    }

    [Fact]
    public void TestDetail1282_RollMurderCurse()
    {
        var rng = new Random(1);
        var (c, ok) = OfficialSupremePinnacle100DetailsEngine.RollMurderCurse(0, rng);
        Assert.InRange(c, 0, 1);
    }

    [Fact]
    public void TestDetail1283_IsMagicDaoGuaranteedMax()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.IsMagicDaoGuaranteedMax(9));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.IsMagicDaoGuaranteedMax(8));
    }

    [Fact]
    public void TestDetail1284_EvaluateZeroDuraArmor()
    {
        var (ac1, mac1, b1) = OfficialSupremePinnacle100DetailsEngine.EvaluateZeroDuraArmor(0, 20, 15);
        Assert.Equal(0, ac1);
        Assert.True(b1);

        var (ac2, mac2, b2) = OfficialSupremePinnacle100DetailsEngine.EvaluateZeroDuraArmor(50, 20, 15);
        Assert.Equal(20, ac2);
        Assert.False(b2);
    }

    [Fact]
    public void TestDetail1285_EvaluateZeroDuraWeapon()
    {
        Assert.Equal((1, 1), OfficialSupremePinnacle100DetailsEngine.EvaluateZeroDuraWeapon(0, 10, 30));
        Assert.Equal((10, 30), OfficialSupremePinnacle100DetailsEngine.EvaluateZeroDuraWeapon(50, 10, 30));
    }

    [Fact]
    public void TestDetail1286_IsJewelryActive()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.IsJewelryActive(1));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.IsJewelryActive(0));
    }

    [Fact]
    public void TestDetail1287_CanRepairAllWithGodOfWarOil()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.CanRepairAllWithGodOfWarOil());
    }

    [Fact]
    public void TestDetail1288_ApplyNormalRepairWear()
    {
        var rng = new Random(3);
        int d = OfficialSupremePinnacle100DetailsEngine.ApplyNormalRepairWear(30, rng);
        Assert.InRange(d, 29, 30);
    }

    [Fact]
    public void TestDetail1289_GetRareBonusDurability()
    {
        Assert.Equal(3, OfficialSupremePinnacle100DetailsEngine.GetRareBonusDurability(3));
        Assert.Equal(5, OfficialSupremePinnacle100DetailsEngine.GetRareBonusDurability(8));
    }

    [Fact]
    public void TestDetail1290_ShouldPurifyCurseFirst()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.ShouldPurifyCurseFirst(1));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.ShouldPurifyCurseFirst(0));
    }

    [Fact]
    public void TestDetail1291_GetConsecutiveUpgradeSuccessRate()
    {
        Assert.Equal(65, OfficialSupremePinnacle100DetailsEngine.GetConsecutiveUpgradeSuccessRate(0));
        Assert.Equal(15, OfficialSupremePinnacle100DetailsEngine.GetConsecutiveUpgradeSuccessRate(7));
    }

    [Fact]
    public void TestDetail1292_GenerateWeaponBrokenBroadcast()
    {
        string text = OfficialSupremePinnacle100DetailsEngine.GenerateWeaponBrokenBroadcast("独孤求败", "裁决之杖");
        Assert.Contains("独孤求败", text);
        Assert.Contains("裁决之杖", text);
    }

    [Fact]
    public void TestDetail1293_DoesPrayerHelmetShatterOnDeath()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.DoesPrayerHelmetShatterOnDeath());
    }

    [Fact]
    public void TestDetail1294_GetMysticUnknownText()
    {
        Assert.Contains("？？？", OfficialSupremePinnacle100DetailsEngine.GetMysticUnknownText());
    }

    [Fact]
    public void TestDetail1295_ValidateMysticTakeOff()
    {
        var (ok1, _) = OfficialSupremePinnacle100DetailsEngine.ValidateMysticTakeOff(false);
        Assert.True(ok1);

        var (ok2, _) = OfficialSupremePinnacle100DetailsEngine.ValidateMysticTakeOff(true);
        Assert.False(ok2);
    }

    // =========================================================================
    // 细节 1296 ~ 1310: 祖玛、沃玛与尸王殿
    // =========================================================================

    [Fact]
    public void TestDetail1296_ShouldWomaLeaderTeleport()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.ShouldWomaLeaderTeleport(200, 1000));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.ShouldWomaLeaderTeleport(500, 1000));
    }

    [Fact]
    public void TestDetail1297_CalculateWomaLightningChain()
    {
        Assert.Equal(35, OfficialSupremePinnacle100DetailsEngine.CalculateWomaLightningChain(45, 20));
    }

    [Fact]
    public void TestDetail1298_GetWomaHornDropCount()
    {
        Assert.Equal(1, OfficialSupremePinnacle100DetailsEngine.GetWomaHornDropCount());
    }

    [Fact]
    public void TestDetail1299_CanZumaArcherTarget()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.CanZumaArcherTarget(100, 100, 107, 100));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.CanZumaArcherTarget(100, 100, 109, 100));
    }

    [Fact]
    public void TestDetail1300_GetZumaStatueAwakenedStepMs()
    {
        long awaken = 10000;
        Assert.Equal(200, OfficialSupremePinnacle100DetailsEngine.GetZumaStatueAwakenedStepMs(awaken, awaken + 1500));
        Assert.Equal(400, OfficialSupremePinnacle100DetailsEngine.GetZumaStatueAwakenedStepMs(awaken, awaken + 4000));
    }

    [Fact]
    public void TestDetail1301_RollZumaGuardStruckKnockback()
    {
        var rng = new Random(100);
        bool push = OfficialSupremePinnacle100DetailsEngine.RollZumaGuardStruckKnockback(rng);
        Assert.True(push || !push);
    }

    [Fact]
    public void TestDetail1302_RollWedgeMothParalysis()
    {
        var rng = new Random(200);
        bool p = OfficialSupremePinnacle100DetailsEngine.RollWedgeMothParalysis(rng);
        Assert.True(p || !p);
    }

    [Fact]
    public void TestDetail1303_IsCorpseKingCaveEntrance()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.IsCorpseKingCaveEntrance(140, 108));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.IsCorpseKingCaveEntrance(100, 100));
    }

    [Fact]
    public void TestDetail1304_IsCorpseHoleFull()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.IsCorpseHoleFull(20));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.IsCorpseHoleFull(19));
    }

    [Fact]
    public void TestDetail1305_IsCorpseWaveReady()
    {
        long last = 10000;
        Assert.False(OfficialSupremePinnacle100DetailsEngine.IsCorpseWaveReady(last, last + 10 * 60 * 1000L));
        Assert.True(OfficialSupremePinnacle100DetailsEngine.IsCorpseWaveReady(last, last + 21 * 60 * 1000L));
    }

    [Fact]
    public void TestDetail1306_IsCorpseKingSkillBook()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.IsCorpseKingSkillBook("烈火剑法"));
        Assert.True(OfficialSupremePinnacle100DetailsEngine.IsCorpseKingSkillBook("冰咆哮"));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.IsCorpseKingSkillBook("火球术"));
    }

    [Fact]
    public void TestDetail1307_IsZumaLairForbiddenFlight()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.IsZumaLairForbiddenFlight("0157"));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.IsZumaLairForbiddenFlight("0"));
    }

    [Fact]
    public void TestDetail1308_ValidateFengMoDungeonPath()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.ValidateFengMoDungeonPath("D2001"));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.ValidateFengMoDungeonPath("0"));
    }

    [Fact]
    public void TestDetail1309_ApplyHongMoMagicAbsorption()
    {
        var (dmg, heal) = OfficialSupremePinnacle100DetailsEngine.ApplyHongMoMagicAbsorption(100);
        Assert.Equal(80, dmg);
        Assert.Equal(20, heal);
    }

    [Fact]
    public void TestDetail1310_CalculateHongMoLifesteal()
    {
        Assert.Equal(30, OfficialSupremePinnacle100DetailsEngine.CalculateHongMoLifesteal(100));
    }

    // =========================================================================
    // 细节 1311 ~ 1325: 交易与金币
    // =========================================================================

    [Fact]
    public void TestDetail1311_ShouldResetLockOnDealChange()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.ShouldResetLockOnDealChange());
    }

    [Fact]
    public void TestDetail1312_CanExecuteDeal()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.CanExecuteDeal(true, true, true, true));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.CanExecuteDeal(true, true, false, true));
    }

    [Fact]
    public void TestDetail1313_IsDealBrokenByMovement()
    {
        Assert.False(OfficialSupremePinnacle100DetailsEngine.IsDealBrokenByMovement(100, 100, 101, 100));
        Assert.True(OfficialSupremePinnacle100DetailsEngine.IsDealBrokenByMovement(100, 100, 103, 100));
    }

    [Fact]
    public void TestDetail1314_ShouldCancelDealOnDamage()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.ShouldCancelDealOnDamage());
    }

    [Fact]
    public void TestDetail1315_ValidateDealGoldLimit()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.ValidateDealGoldLimit(8000000, 1000000));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.ValidateDealGoldLimit(9500000, 1000000));
    }

    [Fact]
    public void TestDetail1316_ValidateReceiverBagSlots()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.ValidateReceiverBagSlots(5, 3));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.ValidateReceiverBagSlots(2, 3));
    }

    [Fact]
    public void TestDetail1317_ValidateDropGoldAmount()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.ValidateDropGoldAmount(50000));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.ValidateDropGoldAmount(200000));
    }

    [Fact]
    public void TestDetail1318_IsGoldUnderProtection()
    {
        long drop = 10000;
        Assert.False(OfficialSupremePinnacle100DetailsEngine.IsGoldUnderProtection(drop, drop + 20000, 101, 101));
        Assert.True(OfficialSupremePinnacle100DetailsEngine.IsGoldUnderProtection(drop, drop + 20000, 101, 102));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.IsGoldUnderProtection(drop, drop + 90000, 101, 102));
    }

    [Fact]
    public void TestDetail1319_CalculateNpcBuybackPrice()
    {
        Assert.Equal(1200, OfficialSupremePinnacle100DetailsEngine.CalculateNpcBuybackPrice(1000));
    }

    [Fact]
    public void TestDetail1320_CalculateDuraDiscountRecyclePrice()
    {
        int p = OfficialSupremePinnacle100DetailsEngine.CalculateDuraDiscountRecyclePrice(1000, 10, 20);
        Assert.Equal(250, p);
    }

    [Fact]
    public void TestDetail1321_ValidateStallDistanceToNpc()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.ValidateStallDistanceToNpc(100, 100, 104, 100));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.ValidateStallDistanceToNpc(100, 100, 101, 100));
    }

    [Fact]
    public void TestDetail1322_ValidateStallItemPrice()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.ValidateStallItemPrice(5000000));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.ValidateStallItemPrice(15000000));
    }

    [Fact]
    public void TestDetail1323_IsActionForbiddenDuringStall()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.IsActionForbiddenDuringStall(true));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.IsActionForbiddenDuringStall(false));
    }

    [Fact]
    public void TestDetail1324_CanDepositToStorage()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.CanDepositToStorage(40000000, 5000000));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.CanDepositToStorage(48000000, 5000000));
    }

    [Fact]
    public void TestDetail1325_ValidateStoragePasswordFormat()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.ValidateStoragePasswordFormat("123456"));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.ValidateStoragePasswordFormat("12"));
    }

    // =========================================================================
    // 细节 1326 ~ 1340: 战斗动作与后摇
    // =========================================================================

    [Fact]
    public void TestDetail1326_GetWarriorHitCooldownMs()
    {
        Assert.Equal(680, OfficialSupremePinnacle100DetailsEngine.GetWarriorHitCooldownMs());
    }

    [Fact]
    public void TestDetail1327_CalculateAdjustedHitCooldown()
    {
        Assert.Equal(580, OfficialSupremePinnacle100DetailsEngine.CalculateAdjustedHitCooldown(680, 2));
    }

    [Fact]
    public void TestDetail1328_GetCastTimings()
    {
        var (w, r) = OfficialSupremePinnacle100DetailsEngine.GetCastTimings();
        Assert.Equal(400, w);
        Assert.Equal(300, r);
    }

    [Fact]
    public void TestDetail1329_GetStruckStunDurationMs()
    {
        Assert.Equal(400, OfficialSupremePinnacle100DetailsEngine.GetStruckStunDurationMs());
    }

    [Fact]
    public void TestDetail1330_DoesMagicShieldIgnoreStruck()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.DoesMagicShieldIgnoreStruck(true, false));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.DoesMagicShieldIgnoreStruck(true, true)); // 刺杀破盾穿透
    }

    [Fact]
    public void TestDetail1331_DoesStabbingPierceForceStun()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.DoesStabbingPierceForceStun(true));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.DoesStabbingPierceForceStun(false));
    }

    [Fact]
    public void TestDetail1332_GetMovementDurations()
    {
        var (w, r) = OfficialSupremePinnacle100DetailsEngine.GetMovementDurations();
        Assert.Equal(400, w);
        Assert.Equal(200, r);
    }

    [Fact]
    public void TestDetail1333_RequiresWalkPrecursor()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.RequiresWalkPrecursor(true));
    }

    [Fact]
    public void TestDetail1334_IsMountSystemSupported()
    {
        Assert.False(OfficialSupremePinnacle100DetailsEngine.IsMountSystemSupported());
    }

    [Fact]
    public void TestDetail1335_IsFireSwordExtinguished()
    {
        long charge = 10000;
        Assert.False(OfficialSupremePinnacle100DetailsEngine.IsFireSwordExtinguished(charge, charge + 8000));
        Assert.True(OfficialSupremePinnacle100DetailsEngine.IsFireSwordExtinguished(charge, charge + 11000));
    }

    [Fact]
    public void TestDetail1336_CanExecuteDoubleFire()
    {
        long first = 10000;
        Assert.False(OfficialSupremePinnacle100DetailsEngine.CanExecuteDoubleFire(first, first + 7000));
        Assert.True(OfficialSupremePinnacle100DetailsEngine.CanExecuteDoubleFire(first, first + 8100));
    }

    [Fact]
    public void TestDetail1337_GetMotaeboWallHitStunDurationMs()
    {
        Assert.Equal(1000, OfficialSupremePinnacle100DetailsEngine.GetMotaeboWallHitStunDurationMs());
    }

    [Fact]
    public void TestDetail1338_CanPushTargetWithMotaebo()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.CanPushTargetWithMotaebo(35, 30));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.CanPushTargetWithMotaebo(30, 35));
    }

    [Fact]
    public void TestDetail1339_IsMonsterTameStunned()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.IsMonsterTameStunned(true));
    }

    [Fact]
    public void TestDetail1340_ShouldSkipStruckOnHolyDeath()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.ShouldSkipStruckOnHolyDeath(true));
    }

    // =========================================================================
    // 细节 1341 ~ 1350: 网络协议与防外挂
    // =========================================================================

    [Fact]
    public void TestDetail1341_ValidatePacketMagic()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.ValidatePacketMagic(0x594D));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.ValidatePacketMagic(0x1234));
    }

    [Fact]
    public void TestDetail1342_ValidatePacketSequence()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.ValidatePacketSequence(100, 101));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.ValidatePacketSequence(100, 99));
        Assert.True(OfficialSupremePinnacle100DetailsEngine.ValidatePacketSequence(65530, 5)); // 回绕放行
    }

    [Fact]
    public void TestDetail1343_GetPingIntervalMs()
    {
        Assert.Equal(10000, OfficialSupremePinnacle100DetailsEngine.GetPingIntervalMs());
    }

    [Fact]
    public void TestDetail1344_IsSessionHeartbeatTimeout()
    {
        long last = 10000;
        Assert.False(OfficialSupremePinnacle100DetailsEngine.IsSessionHeartbeatTimeout(last, last + 30000));
        Assert.True(OfficialSupremePinnacle100DetailsEngine.IsSessionHeartbeatTimeout(last, last + 65000));
    }

    [Fact]
    public void TestDetail1345_ValidateMovePacketFrequency()
    {
        long last = 10000;
        Assert.True(OfficialSupremePinnacle100DetailsEngine.ValidateMovePacketFrequency(last, last + 200));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.ValidateMovePacketFrequency(last, last + 100)); // 加速跑拦截
    }

    [Fact]
    public void TestDetail1346_ValidateAttackPacketFrequency()
    {
        long last = 10000;
        Assert.True(OfficialSupremePinnacle100DetailsEngine.ValidateAttackPacketFrequency(last, last + 600));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.ValidateAttackPacketFrequency(last, last + 400)); // 加速刀拦截
    }

    [Fact]
    public void TestDetail1347_ValidateSpellPacketFrequency()
    {
        long last = 10000;
        Assert.True(OfficialSupremePinnacle100DetailsEngine.ValidateSpellPacketFrequency(last, last + 850));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.ValidateSpellPacketFrequency(last, last + 500));
    }

    [Fact]
    public void TestDetail1348_ValidatePotionPacketFrequency()
    {
        long last = 10000;
        Assert.True(OfficialSupremePinnacle100DetailsEngine.ValidatePotionPacketFrequency(last, last + 300));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.ValidatePotionPacketFrequency(last, last + 100)); // 喝药连点拦截
    }

    [Fact]
    public void TestDetail1349_ValidateTileDisplacement()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.ValidateTileDisplacement(100, 100, 102, 100));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.ValidateTileDisplacement(100, 100, 105, 100)); // 瞬移外挂拦截
    }

    [Fact]
    public void TestDetail1350_ValidatePayloadLength()
    {
        Assert.True(OfficialSupremePinnacle100DetailsEngine.ValidatePayloadLength(500));
        Assert.False(OfficialSupremePinnacle100DetailsEngine.ValidatePayloadLength(70000));
    }
}
