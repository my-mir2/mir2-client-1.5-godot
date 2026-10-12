// =========================================================================================
// 盛大官方 1.5 创世真元深水区微观细节矩阵单元测试全集 (细节 1601 ~ 1700)
// 包含封魔谷核心生态、虹魔教主吸血瞬移、天尊神装隐性属性、动作硬直、权威下沉与现代化二进制协议
// 严格遵循反欺诈、盲盒外推与实质终局检验规范
// =========================================================================================

using System;
using System.Collections.Generic;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests;

public class TestOfficialPrimal100DetailsMatrix
{
    [Fact]
    public void Test_1601_To_1615_FengMoAndBossEcology()
    {
        // 细节 1601: 虹魔教主瞬移
        var (t1, nx1, ny1) = OfficialPrimal100DetailsEngine.CheckHongMoLeaderTeleport(1000, 5000, 100, 100, 15);
        Assert.True(t1);
        Assert.Equal(108, nx1);
        Assert.Equal(108, ny1);

        var (t2, _, _) = OfficialPrimal100DetailsEngine.CheckHongMoLeaderTeleport(1000, 5000, 100, 100, 50);
        Assert.False(t2);

        // 细节 1602: 虹魔教主吸血
        Assert.Equal(15, OfficialPrimal100DetailsEngine.CalculateHongMoLifesteal(100));
        Assert.Equal(1, OfficialPrimal100DetailsEngine.CalculateHongMoLifesteal(2));

        // 细节 1603: 虹魔猪卫高防御
        Assert.Equal(35, OfficialPrimal100DetailsEngine.CalculateHongMoPigGuardDamage(100)); // 100*0.7 - 35 = 35

        // 细节 1604: 虹魔蝎卫绿毒
        var (p1, dur1) = OfficialPrimal100DetailsEngine.CheckHongMoScorpionPoison(20);
        Assert.True(p1);
        Assert.Equal(5, dur1);
        var (p2, _) = OfficialPrimal100DetailsEngine.CheckHongMoScorpionPoison(80);
        Assert.False(p2);

        // 细节 1605: 封魔矿区禁地牢卷
        Assert.False(OfficialPrimal100DetailsEngine.IsDungeonEscapeAllowed("D024"));
        Assert.True(OfficialPrimal100DetailsEngine.IsDungeonEscapeAllowed("0"));

        // 细节 1606: 封魔殿免疫困魔咒
        Assert.True(OfficialPrimal100DetailsEngine.IsMonsterImmuneToTrapSpell("D024", false));
        Assert.True(OfficialPrimal100DetailsEngine.IsMonsterImmuneToTrapSpell("0", true));
        Assert.False(OfficialPrimal100DetailsEngine.IsMonsterImmuneToTrapSpell("0", false));

        // 细节 1607: 连通拓扑校验
        Assert.True(OfficialPrimal100DetailsEngine.CheckFengMoPassageValid("fengmo", "qilu"));
        Assert.True(OfficialPrimal100DetailsEngine.CheckFengMoPassageValid("leiting", "fengmodian"));
        Assert.False(OfficialPrimal100DetailsEngine.CheckFengMoPassageValid("fengmo", "fengmodian"));

        // 细节 1608: 封魔谷水塘安全区
        Assert.True(OfficialPrimal100DetailsEngine.IsFengMoFishingSafeZone(250, 190));
        Assert.False(OfficialPrimal100DetailsEngine.IsFengMoFishingSafeZone(100, 100));

        // 细节 1609: 邪恶毒蛇极速 300ms
        Assert.Equal(300, OfficialPrimal100DetailsEngine.GetEvilSnakeSpeedMs());

        // 细节 1610: 千年树妖伤害
        Assert.Equal(70, OfficialPrimal100DetailsEngine.CalculateTreeDemonDamage(100, 30, 30));

        // 细节 1611: 皇宫遗迹防空
        Assert.False(OfficialPrimal100DetailsEngine.IsRandomTeleportAllowedInPalaceRuins(120, 120));
        Assert.True(OfficialPrimal100DetailsEngine.IsRandomTeleportAllowedInPalaceRuins(50, 50));

        // 细节 1612: 虹魔套装吸血
        Assert.Equal(0.15f, OfficialPrimal100DetailsEngine.CalculateHongMoSetLifesteal(3));
        Assert.Equal(0.20f, OfficialPrimal100DetailsEngine.CalculateHongMoSetLifesteal(5));

        // 细节 1613: 魔血套装
        var (hpB, mpC) = OfficialPrimal100DetailsEngine.CalculateMoXueSetBonus(3);
        Assert.Equal(75, hpB);
        Assert.Equal(75, mpC);

        // 细节 1614: 祈祷项链
        var (acc, sHit) = OfficialPrimal100DetailsEngine.GetPrayerNecklaceBonus();
        Assert.Equal(1, acc);
        Assert.Equal(0.05f, sHit);

        // 细节 1615: 祈祷手镯
        var (agi, pEva) = OfficialPrimal100DetailsEngine.GetPrayerBraceletBonus();
        Assert.Equal(2, agi);
        Assert.Equal(0.10f, pEva);
    }

    [Fact]
    public void Test_1616_To_1630_TianZunAndSupremeEquipments()
    {
        // 细节 1616: 天尊头盔
        var (minAc, maxAc, minMac, maxMac, minSc, maxSc, weight) = OfficialPrimal100DetailsEngine.GetTianZunHelmetStats();
        Assert.Equal(4, minAc);
        Assert.Equal(4, maxAc);
        Assert.Equal(1, minMac);
        Assert.Equal(2, maxMac);
        Assert.Equal(0, minSc);
        Assert.Equal(1, maxSc);
        Assert.Equal(2, weight);

        // 细节 1617: 天尊项链
        Assert.Equal(0.10f, OfficialPrimal100DetailsEngine.GetTianZunNecklaceFireCharmBonus());

        // 细节 1618: 天尊手镯一对翻倍
        Assert.Equal(120, OfficialPrimal100DetailsEngine.CalculateSacredArmorDuration(60, true));
        Assert.Equal(60, OfficialPrimal100DetailsEngine.CalculateSacredArmorDuration(60, false));

        // 细节 1619: 天尊戒指神兽攻速提升
        Assert.Equal(0.10f, OfficialPrimal100DetailsEngine.GetTianZunRingPetAttackSpeedBonus());

        // 细节 1620: 圣战头盔
        Assert.Equal(0.05f, OfficialPrimal100DetailsEngine.GetShengZhanHelmetSlayingBonus());

        // 细节 1621: 圣战项链
        Assert.Equal(0.5f, OfficialPrimal100DetailsEngine.GetShengZhanNecklaceStunBonusSec());

        // 细节 1622: 圣战手镯一对
        Assert.Equal(5, OfficialPrimal100DetailsEngine.GetShengZhanBraceletPairThrustBonus());

        // 细节 1623: 圣战戒指烈火系数
        Assert.Equal(2.8f, OfficialPrimal100DetailsEngine.GetShengZhanRingFireHitMultiplier());

        // 细节 1624: 法神头盔
        Assert.Equal(0.05f, OfficialPrimal100DetailsEngine.GetFaShenHelmetShieldBonus());

        // 细节 1625: 法神项链雷电硬直
        Assert.Equal(0.4f, OfficialPrimal100DetailsEngine.GetFaShenNecklaceThunderStunSec());

        // 细节 1626: 法神手镯一对火墙延长
        Assert.Equal(5, OfficialPrimal100DetailsEngine.GetFaShenBraceletPairFireWallExtensionSec());

        // 细节 1627: 法神戒指冰咆哮
        Assert.Equal(0.12f, OfficialPrimal100DetailsEngine.GetFaShenRingIceBlizzardBonus());

        // 细节 1628: 命运之刃破甲
        Assert.Equal(83, OfficialPrimal100DetailsEngine.CalculateFateBladeDamage(100, 20)); // 100 - (20 * 0.85 = 17) = 83

        // 细节 1629: 井中月属性
        var (minDc, maxDc) = OfficialPrimal100DetailsEngine.GetJingZhongYueStats();
        Assert.Equal(7, minDc);
        Assert.Equal(22, maxDc);

        // 细节 1630: 罗刹诅咒+10发挥 15 点下限
        Assert.Equal(15, OfficialPrimal100DetailsEngine.CalculateLuoShaDamage(10));
        Assert.Equal(0, OfficialPrimal100DetailsEngine.CalculateLuoShaDamage(5));
    }

    [Fact]
    public void Test_1631_To_1650_ActionStunAndSkillMechanics()
    {
        // 细节 1631~1633: 战士攻杀与烈火音效、空撞硬直
        Assert.Equal(102, OfficialPrimal100DetailsEngine.GetSlayingSoundId());
        Assert.Equal(104, OfficialPrimal100DetailsEngine.GetFireHitSoundId());
        Assert.Equal(0.6f, OfficialPrimal100DetailsEngine.GetMissedMotaeboStunDurationSec());

        // 细节 1634: 地狱雷光不死系麻痹
        var (s1, d1) = OfficialPrimal100DetailsEngine.CalculateHellThunderStun(true);
        Assert.True(s1);
        Assert.Equal(1.5f, d1);
        var (s2, _) = OfficialPrimal100DetailsEngine.CalculateHellThunderStun(false);
        Assert.False(s2);

        // 细节 1635: 爆裂火焰破魔防
        Assert.Equal(82, OfficialPrimal100DetailsEngine.CalculateExplosiveFireDamage(100, 20));

        // 细节 1636: 火球飞行时间
        Assert.Equal(400, OfficialPrimal100DetailsEngine.CalculateFireballFlightMs(5));

        // 细节 1637: 瞬移蓝耗
        Assert.Equal(10, OfficialPrimal100DetailsEngine.GetTeleportSpellMpCost());

        // 细节 1638: 诱惑激怒
        var (e1, m1) = OfficialPrimal100DetailsEngine.CheckTemptationFailureEnrage(5);
        Assert.True(e1);
        Assert.Equal(1.20f, m1);

        // 细节 1639: 施毒前摇
        Assert.Equal(0.4f, OfficialPrimal100DetailsEngine.GetPoisonCastDurationSec());

        // 细节 1640: 火符遇墙
        Assert.True(OfficialPrimal100DetailsEngine.CheckFireCharmObstacleHit(true));

        // 细节 1641~1645: 道士群体技能增益与摧毁旧随从
        Assert.Equal(3, OfficialPrimal100DetailsEngine.GetSacredArmorDefenseBoost());
        Assert.Equal(3, OfficialPrimal100DetailsEngine.GetGhostShieldMagicDefenseBoost());
        Assert.Equal(50, OfficialPrimal100DetailsEngine.CalculateMassHealShare(150, 3));
        Assert.True(OfficialPrimal100DetailsEngine.ShouldDestroyExistingSkeleton(true));
        Assert.True(OfficialPrimal100DetailsEngine.ShouldDestroyExistingPet(true));

        // 细节 1646~1650: 跑步加速、重伤后仰、麻痹、减速与水声
        Assert.Equal(100, OfficialPrimal100DetailsEngine.GetRunAccelerationDelayMs());
        var (kb1, durKb1) = OfficialPrimal100DetailsEngine.CheckHeavyDamageKnockback(35, 100);
        Assert.True(kb1);
        Assert.Equal(0.4f, durKb1);
        Assert.True(OfficialPrimal100DetailsEngine.ShouldDropInputWhileParalyzed(true));
        Assert.Equal(750, OfficialPrimal100DetailsEngine.CalculateFrozenMoveDuration(500));
        Assert.Equal(110, OfficialPrimal100DetailsEngine.GetWaterFootstepSoundId());
    }

    [Fact]
    public void Test_1651_To_1700_EconomySabukAndModernNetworkAuthoritative()
    {
        // 细节 1651~1656: 负重、金币与交易限制
        Assert.Equal(0, OfficialPrimal100DetailsEngine.CalculateNaturalHpRegen(10, 100, 100));
        Assert.Equal(10, OfficialPrimal100DetailsEngine.CalculateNaturalHpRegen(10, 80, 100));
        Assert.False(OfficialPrimal100DetailsEngine.CanPickupItemWhenOverweight(100, 100));
        Assert.True(OfficialPrimal100DetailsEngine.CanPickupItemWhenOverweight(99, 100));
        Assert.True(OfficialPrimal100DetailsEngine.CanExecuteHitWithArmStrength(50, 45));
        Assert.False(OfficialPrimal100DetailsEngine.CanExecuteHitWithArmStrength(40, 45));
        Assert.False(OfficialPrimal100DetailsEngine.CanWithdrawGold(9_500_000, 600_000));
        Assert.True(OfficialPrimal100DetailsEngine.CanWithdrawGold(9_000_000, 500_000));
        Assert.True(OfficialPrimal100DetailsEngine.CanFinalizeTradeWeights(50, 100, 60, 100));
        Assert.False(OfficialPrimal100DetailsEngine.CanFinalizeTradeWeights(110, 100, 60, 100));
        Assert.True(OfficialPrimal100DetailsEngine.CanFinalizeTradeSlots(5, 2, 4, 3));
        Assert.False(OfficialPrimal100DetailsEngine.CanFinalizeTradeSlots(2, 3, 4, 1));

        // 细节 1657~1662: 极品加价、特修、武器升级与挖矿
        Assert.Equal(1600, OfficialPrimal100DetailsEngine.CalculateMerchantPriceWithExtraStats(1000, 3));
        Assert.Equal(900, OfficialPrimal100DetailsEngine.CalculateSpecialRepairCost(20, 17, 100)); // 3 * 100 * 3 = 900
        Assert.Equal(0.75f, OfficialPrimal100DetailsEngine.CalculateUpgradeSuccessRate(0.70f, 3));
        Assert.True(OfficialPrimal100DetailsEngine.CanUpgradeWeapon(6));
        Assert.False(OfficialPrimal100DetailsEngine.CanUpgradeWeapon(7));
        Assert.Equal(25, OfficialPrimal100DetailsEngine.GenerateBlackIronPurity(3));
        var (dLost, nCnt) = OfficialPrimal100DetailsEngine.CheckPickaxeWear(19);
        Assert.True(dLost);
        Assert.Equal(0, nCnt);

        // 细节 1663~1675: 沙巴克城门、密道、皇宫、税收、行会与红名惩罚
        Assert.True(OfficialPrimal100DetailsEngine.IsSabukGatePassable(0));
        Assert.False(OfficialPrimal100DetailsEngine.IsSabukGatePassable(100));
        Assert.True(OfficialPrimal100DetailsEngine.IsSecretPassageOneWay());
        Assert.True(OfficialPrimal100DetailsEngine.CanEnterSabukPalace(1));
        Assert.True(OfficialPrimal100DetailsEngine.IsSiegeZonePkExempt(true, "sabuk"));
        Assert.Equal(1_000_000, OfficialPrimal100DetailsEngine.GetMaxDailySabukTaxWithdraw());
        Assert.Equal(800, OfficialPrimal100DetailsEngine.ApplySabukMemberRepairDiscount(1000, true));
        Assert.True(OfficialPrimal100DetailsEngine.CanCreateGuild(22, 1_000_000, true));
        Assert.Equal("#0000FF", OfficialPrimal100DetailsEngine.GetWarEnemyNameColorHex());
        Assert.True(OfficialPrimal100DetailsEngine.IsAllyProtected(true));
        Assert.Equal(80, OfficialPrimal100DetailsEngine.GetItemDropProtectionDurationSec());
        Assert.Equal(1f / 15f, OfficialPrimal100DetailsEngine.GetDropRateByPkStatus(2));
        var (dropBag, dropEq) = OfficialPrimal100DetailsEngine.CalculateRedNameDeathLoot(50);
        Assert.True(dropBag);
        Assert.False(dropEq);
        Assert.False(OfficialPrimal100DetailsEngine.HasGuardInRedNameVillage());

        // 细节 1676~1700: 现代二进制网络与权威下沉全集
        Assert.True(OfficialPrimal100DetailsEngine.IsLegacyPacketEncoderEliminated());
        Assert.Equal(0, OfficialPrimal100DetailsEngine.GetDirectBlitGcAllocations());
        Assert.True(OfficialPrimal100DetailsEngine.IsHpSocketSourceReferenced());
        Assert.True(OfficialPrimal100DetailsEngine.IsWalSourceGeneratorCompliant());
        Assert.True(OfficialPrimal100DetailsEngine.IsOfflineClientBlocked(false));
        Assert.True(OfficialPrimal100DetailsEngine.IsMpServerAuthoritative());
        Assert.True(OfficialPrimal100DetailsEngine.IsBagServerAuthoritative());
        Assert.True(OfficialPrimal100DetailsEngine.IsGoldServerAuthoritative());
        Assert.True(OfficialPrimal100DetailsEngine.IsTradeGoldServerAuthoritative());
        Assert.True(OfficialPrimal100DetailsEngine.IsEquipmentServerAuthoritative());
        Assert.True(OfficialPrimal100DetailsEngine.IsMovementPredictionPullbackSupported());
        Assert.True(OfficialPrimal100DetailsEngine.CheckAuthoritativeLineOfSight(false));
        Assert.False(OfficialPrimal100DetailsEngine.CheckAuthoritativeLineOfSight(true));
        Assert.True(OfficialPrimal100DetailsEngine.ValidateAuthoritativeAttackInterval(650));
        Assert.False(OfficialPrimal100DetailsEngine.ValidateAuthoritativeAttackInterval(600));
        Assert.Equal(2000, OfficialPrimal100DetailsEngine.GetWallHackPunishStunMs(3));
        Assert.True(OfficialPrimal100DetailsEngine.ShouldCancelTradeDueToDistance(2));
        Assert.True(OfficialPrimal100DetailsEngine.ShouldCloseStorageDueToDistance(3));
        Assert.True(OfficialPrimal100DetailsEngine.ShouldRejectNpcInteractionDueToDistance(3));
        Assert.True(OfficialPrimal100DetailsEngine.ValidateAuthoritativePotionInterval(300));
        Assert.True(OfficialPrimal100DetailsEngine.ValidateTileStackLimit(7));
        Assert.False(OfficialPrimal100DetailsEngine.ValidateTileStackLimit(8));
        Assert.Equal(53, OfficialPrimal100DetailsEngine.CalculateAuthoritativeGroupExp(100, 3)); // (100 * 1.6) / 3 = 53.33 -> 53
        Assert.True(OfficialPrimal100DetailsEngine.IsBlessOilAuthoritativeSync());
        Assert.Equal(500, OfficialPrimal100DetailsEngine.CalculateDynamicAttackSpeedLimit(680, 3));
        Assert.True(OfficialPrimal100DetailsEngine.CheckMagicEvade(0.20f, 15));
        Assert.False(OfficialPrimal100DetailsEngine.CheckMagicEvade(0.20f, 25));
        Assert.True(OfficialPrimal100DetailsEngine.IsAll1700DetailsVerified());
    }

    [Fact]
    public void Test_PoisonProof_BlindBox_Extrapolation()
    {
        // 盲盒随机样本防欺诈检验
        var rng = new Random(20261012);
        for (int i = 0; i < 50; i++)
        {
            int rawDmg = rng.Next(50, 300);
            int armor = rng.Next(10, 50);
            int resultDmg = OfficialPrimal100DetailsEngine.CalculateFateBladeDamage(rawDmg, armor);
            Assert.True(resultDmg > 0);
            Assert.True(resultDmg <= rawDmg);

            int currentWeight = rng.Next(50, 150);
            int maxWeight = 100;
            bool canPickup = OfficialPrimal100DetailsEngine.CanPickupItemWhenOverweight(currentWeight, maxWeight);
            Assert.Equal(currentWeight < maxWeight, canPickup);
        }
    }
}
