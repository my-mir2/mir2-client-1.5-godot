// ====================================================================================================
// 官方 1.5 细节矩阵 Wave 96: 第 1751 ~ 1850 项细节权威单元测试验证矩阵
// 严格对齐 OfficialSovereignApex100DetailsEngine 逻辑，全闭环无 Mock 恶意攻击与边界测试
// ====================================================================================================

using System;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests
{
    public class TestOfficialWave96_100DetailsApexMatrix
    {
        // --------------------------------------------------------------------------------------------
        // 1751 ~ 1760: 死亡棺材与蜈蚣洞生态
        // --------------------------------------------------------------------------------------------

        [Fact]
        public void Test_1751_CalculateBovineKingFlameDamage()
        {
            // 触龙神魔法全免疫，伤害强制为 0
            int dmg = OfficialSovereignApex100DetailsEngine.CalculateBovineKingFlameDamage(100, 50);
            Assert.Equal(0, dmg);

            int minDmg = OfficialSovereignApex100DetailsEngine.CalculateBovineKingFlameDamage(10, 100);
            Assert.Equal(0, minDmg);
        }

        [Fact]
        public void Test_1752_CanBovineGeneralKnockback()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.CanBovineGeneralKnockback(54, 55));
            Assert.False(OfficialSovereignApex100DetailsEngine.CanBovineGeneralKnockback(55, 55));
            Assert.False(OfficialSovereignApex100DetailsEngine.CanBovineGeneralKnockback(56, 55));
        }

        [Fact]
        public void Test_1753_ShouldBovinePriestTeleport()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.ShouldBovinePriestTeleport(25, 100));
            Assert.True(OfficialSovereignApex100DetailsEngine.ShouldBovinePriestTeleport(10, 100));
            Assert.False(OfficialSovereignApex100DetailsEngine.ShouldBovinePriestTeleport(26, 100));
            Assert.False(OfficialSovereignApex100DetailsEngine.ShouldBovinePriestTeleport(0, 100));
        }

        [Fact]
        public void Test_1754_IsDeathCoffinChestReal()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.IsDeathCoffinChestReal(79));
            Assert.False(OfficialSovereignApex100DetailsEngine.IsDeathCoffinChestReal(80));
            Assert.False(OfficialSovereignApex100DetailsEngine.IsDeathCoffinChestReal(99));
        }

        [Fact]
        public void Test_1755_CheckCentipedeRevive()
        {
            var (revive1, hp1) = OfficialSovereignApex100DetailsEngine.CheckCentipedeRevive(34, 1000);
            Assert.True(revive1);
            Assert.Equal(500, hp1);

            var (revive2, hp2) = OfficialSovereignApex100DetailsEngine.CheckCentipedeRevive(35, 1000);
            Assert.False(revive2);
            Assert.Equal(0, hp2);
        }

        [Fact]
        public void Test_1756_CalculateEvilCentipedeRespawnMinutes()
        {
            int t1 = OfficialSovereignApex100DetailsEngine.CalculateEvilCentipedeRespawnMinutes(0); // 180 + 0 - 15 = 165
            Assert.Equal(165, t1);
            int t2 = OfficialSovereignApex100DetailsEngine.CalculateEvilCentipedeRespawnMinutes(15); // 180 + 15 - 15 = 180
            Assert.Equal(180, t2);
            int t3 = OfficialSovereignApex100DetailsEngine.CalculateEvilCentipedeRespawnMinutes(30); // 180 + 30 - 15 = 195
            Assert.Equal(195, t3);
        }

        [Fact]
        public void Test_1757_CanBlackMaggotRollAttack()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.CanBlackMaggotRollAttack(1));
            Assert.True(OfficialSovereignApex100DetailsEngine.CanBlackMaggotRollAttack(3));
            Assert.False(OfficialSovereignApex100DetailsEngine.CanBlackMaggotRollAttack(0));
            Assert.False(OfficialSovereignApex100DetailsEngine.CanBlackMaggotRollAttack(4));
        }

        [Fact]
        public void Test_1758_CheckEvilCentipedeDrop()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.CheckEvilCentipedeDrop(50));
            Assert.True(OfficialSovereignApex100DetailsEngine.CheckEvilCentipedeDrop(100));
            Assert.False(OfficialSovereignApex100DetailsEngine.CheckEvilCentipedeDrop(49));
        }

        [Fact]
        public void Test_1759_CanTeleportCentipedeCave()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.CanTeleportCentipedeCave(40, 5000));
            Assert.False(OfficialSovereignApex100DetailsEngine.CanTeleportCentipedeCave(39, 5000));
            Assert.False(OfficialSovereignApex100DetailsEngine.CanTeleportCentipedeCave(40, 4999));
        }

        [Fact]
        public void Test_1760_IsDeathCoffinSecretPassageOpen()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.IsDeathCoffinSecretPassageOpen(1));
            Assert.True(OfficialSovereignApex100DetailsEngine.IsDeathCoffinSecretPassageOpen(13));
            Assert.False(OfficialSovereignApex100DetailsEngine.IsDeathCoffinSecretPassageOpen(0));
            Assert.False(OfficialSovereignApex100DetailsEngine.IsDeathCoffinSecretPassageOpen(14));
        }

        // --------------------------------------------------------------------------------------------
        // 1761 ~ 1770: 封魔谷水域迷宫
        // --------------------------------------------------------------------------------------------

        [Fact]
        public void Test_1761_GetCorrectThunderPathIndex()
        {
            Assert.Equal(3, OfficialSovereignApex100DetailsEngine.GetCorrectThunderPathIndex(0)); // 偶数
            Assert.Equal(1, OfficialSovereignApex100DetailsEngine.GetCorrectThunderPathIndex(1)); // 奇数
            Assert.Equal(3, OfficialSovereignApex100DetailsEngine.GetCorrectThunderPathIndex(2));
        }

        [Fact]
        public void Test_1762_CalculateOverlordHallRepairCost()
        {
            int cost = OfficialSovereignApex100DetailsEngine.CalculateOverlordHallRepairCost(1000);
            Assert.Equal(1500, cost);
        }

        [Fact]
        public void Test_1763_ShouldZombieEmerge()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.ShouldZombieEmerge(2, true));
            Assert.False(OfficialSovereignApex100DetailsEngine.ShouldZombieEmerge(3, true));
            Assert.False(OfficialSovereignApex100DetailsEngine.ShouldZombieEmerge(1, false));
        }

        [Fact]
        public void Test_1764_CalculateRedBoarEnragedMoveSpeed()
        {
            int normal = OfficialSovereignApex100DetailsEngine.CalculateRedBoarEnragedMoveSpeed(1000, false);
            Assert.Equal(1000, normal);
            int enraged = OfficialSovereignApex100DetailsEngine.CalculateRedBoarEnragedMoveSpeed(1000, true);
            Assert.Equal(800, enraged);
        }

        [Fact]
        public void Test_1765_IsValidCrossroadBranch()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.IsValidCrossroadBranch(1));
            Assert.True(OfficialSovereignApex100DetailsEngine.IsValidCrossroadBranch(4));
            Assert.False(OfficialSovereignApex100DetailsEngine.IsValidCrossroadBranch(0));
            Assert.False(OfficialSovereignApex100DetailsEngine.IsValidCrossroadBranch(5));
        }

        [Fact]
        public void Test_1766_GetEvilSnakePoisonParams()
        {
            var (dur, dps) = OfficialSovereignApex100DetailsEngine.GetEvilSnakePoisonParams();
            Assert.Equal(15, dur);
            Assert.Equal(5, dps);
        }

        [Fact]
        public void Test_1767_CalculateRainbowMagicLordLeech()
        {
            int leech = OfficialSovereignApex100DetailsEngine.CalculateRainbowMagicLordLeech(100);
            Assert.Equal(25, leech);
            Assert.Equal(0, OfficialSovereignApex100DetailsEngine.CalculateRainbowMagicLordLeech(0));
        }

        [Fact]
        public void Test_1768_CalculateAncientTreeSpikeDamage()
        {
            Assert.Equal(80, OfficialSovereignApex100DetailsEngine.CalculateAncientTreeSpikeDamage(80));
            Assert.Equal(1, OfficialSovereignApex100DetailsEngine.CalculateAncientTreeSpikeDamage(0));
        }

        [Fact]
        public void Test_1769_CanEnterSecretFengMoTunnel()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.CanEnterSecretFengMoTunnel(35, true));
            Assert.False(OfficialSovereignApex100DetailsEngine.CanEnterSecretFengMoTunnel(34, true));
            Assert.False(OfficialSovereignApex100DetailsEngine.CanEnterSecretFengMoTunnel(35, false));
        }

        [Fact]
        public void Test_1770_CheckFengMoFishingCatch()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.CheckFengMoFishingCatch(14));
            Assert.False(OfficialSovereignApex100DetailsEngine.CheckFengMoFishingCatch(15));
        }

        // --------------------------------------------------------------------------------------------
        // 1771 ~ 1780: 药水负重平衡与交易原子安全锁
        // --------------------------------------------------------------------------------------------

        [Fact]
        public void Test_1771_GetSunPotionBundleWeight()
        {
            Assert.Equal(13, OfficialSovereignApex100DetailsEngine.GetSunPotionBundleWeight(2, 6));
        }

        [Fact]
        public void Test_1772_CanUnpackPotionBundle()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.CanUnpackPotionBundle(5));
            Assert.False(OfficialSovereignApex100DetailsEngine.CanUnpackPotionBundle(4));
        }

        [Fact]
        public void Test_1773_CalculateMaxWearWeight()
        {
            // 战士 35级: 15 + 70 = 85
            Assert.Equal(85, OfficialSovereignApex100DetailsEngine.CalculateMaxWearWeight(35, 0));
            // 法师 35级: 13 + 52 = 65
            Assert.Equal(65, OfficialSovereignApex100DetailsEngine.CalculateMaxWearWeight(35, 1));
            // 道士 35级: 14 + 52 = 66
            Assert.Equal(66, OfficialSovereignApex100DetailsEngine.CalculateMaxWearWeight(35, 2));
        }

        [Fact]
        public void Test_1774_CheckBagWeightPenalties()
        {
            var (c1, s1) = OfficialSovereignApex100DetailsEngine.CheckBagWeightPenalties(99, 100);
            Assert.True(c1);
            Assert.False(s1);

            var (c2, s2) = OfficialSovereignApex100DetailsEngine.CheckBagWeightPenalties(100, 100);
            Assert.False(c2);
            Assert.False(s2);

            var (c3, s3) = OfficialSovereignApex100DetailsEngine.CheckBagWeightPenalties(120, 100);
            Assert.False(c3);
            Assert.True(s3);
        }

        [Fact]
        public void Test_1775_ResetTradeLocksOnItemChanged()
        {
            var (l1, l2) = OfficialSovereignApex100DetailsEngine.ResetTradeLocksOnItemChanged();
            Assert.False(l1);
            Assert.False(l2);
        }

        [Fact]
        public void Test_1776_ValidateTradeGoldTransfer()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.ValidateTradeGoldTransfer(1000, 1000, 500));
            Assert.False(OfficialSovereignApex100DetailsEngine.ValidateTradeGoldTransfer(100, 1000, 500));
            Assert.False(OfficialSovereignApex100DetailsEngine.ValidateTradeGoldTransfer(1000, 49_999_999, 2));
        }

        [Fact]
        public void Test_1777_RecordStoragePasswordAttempt()
        {
            var (lock1, fails1) = OfficialSovereignApex100DetailsEngine.RecordStoragePasswordAttempt(false, 0);
            Assert.False(lock1);
            Assert.Equal(1, fails1);

            var (lock2, fails2) = OfficialSovereignApex100DetailsEngine.RecordStoragePasswordAttempt(false, 2);
            Assert.True(lock2);
            Assert.Equal(3, fails2);

            var (lock3, fails3) = OfficialSovereignApex100DetailsEngine.RecordStoragePasswordAttempt(true, 2);
            Assert.False(lock3);
            Assert.Equal(0, fails3);
        }

        [Fact]
        public void Test_1778_GetGoldDropItemIndex()
        {
            Assert.Equal(110, OfficialSovereignApex100DetailsEngine.GetGoldDropItemIndex(50));
            Assert.Equal(111, OfficialSovereignApex100DetailsEngine.GetGoldDropItemIndex(500));
            Assert.Equal(112, OfficialSovereignApex100DetailsEngine.GetGoldDropItemIndex(2500));
            Assert.Equal(113, OfficialSovereignApex100DetailsEngine.GetGoldDropItemIndex(10000));
        }

        [Fact]
        public void Test_1779_CanPlayerPickupGroundItem()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.CanPlayerPickupGroundItem(10, true, false));
            Assert.True(OfficialSovereignApex100DetailsEngine.CanPlayerPickupGroundItem(10, false, true));
            Assert.False(OfficialSovereignApex100DetailsEngine.CanPlayerPickupGroundItem(10, false, false));
            Assert.True(OfficialSovereignApex100DetailsEngine.CanPlayerPickupGroundItem(30, false, false));
        }

        [Fact]
        public void Test_1780_CanDrinkPotion()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.CanDrinkPotion(2000, 1000, 1000));
            Assert.False(OfficialSovereignApex100DetailsEngine.CanDrinkPotion(1500, 1000, 1000));
        }

        // --------------------------------------------------------------------------------------------
        // 1781 ~ 1790: 武器升级、开刀与幸运极值
        // --------------------------------------------------------------------------------------------

        [Fact]
        public void Test_1781_CalculateWeaponDurabilityDelta()
        {
            Assert.Equal(-1, OfficialSovereignApex100DetailsEngine.CalculateWeaponDurabilityDelta(59));
            Assert.Equal(0, OfficialSovereignApex100DetailsEngine.CalculateWeaponDurabilityDelta(60));
            Assert.Equal(0, OfficialSovereignApex100DetailsEngine.CalculateWeaponDurabilityDelta(90));
            Assert.Equal(1, OfficialSovereignApex100DetailsEngine.CalculateWeaponDurabilityDelta(91));
        }

        [Fact]
        public void Test_1782_CalculateWeaponUpgradeSuccessRate()
        {
            Assert.Equal(40, OfficialSovereignApex100DetailsEngine.CalculateWeaponUpgradeSuccessRate(0));
            Assert.Equal(60, OfficialSovereignApex100DetailsEngine.CalculateWeaponUpgradeSuccessRate(10));
            Assert.Equal(85, OfficialSovereignApex100DetailsEngine.CalculateWeaponUpgradeSuccessRate(50));
        }

        [Fact]
        public void Test_1783_EvaluateWeaponFirstSlash()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.EvaluateWeaponFirstSlash(49, 50, false));
            Assert.False(OfficialSovereignApex100DetailsEngine.EvaluateWeaponFirstSlash(50, 50, false));
            // 目标为卫士，削减10%成功率 -> 40%
            Assert.False(OfficialSovereignApex100DetailsEngine.EvaluateWeaponFirstSlash(45, 50, true));
        }

        [Fact]
        public void Test_1784_GetBlessingOilSuccessChance()
        {
            Assert.Equal(70, OfficialSovereignApex100DetailsEngine.GetBlessingOilSuccessChance(0));
            Assert.Equal(20, OfficialSovereignApex100DetailsEngine.GetBlessingOilSuccessChance(3));
            Assert.Equal(2, OfficialSovereignApex100DetailsEngine.GetBlessingOilSuccessChance(6));
            Assert.Equal(0, OfficialSovereignApex100DetailsEngine.GetBlessingOilSuccessChance(7));
        }

        [Fact]
        public void Test_1785_RollBlessingOil()
        {
            var (s1, c1) = OfficialSovereignApex100DetailsEngine.RollBlessingOil(0, 50, 50);
            Assert.True(s1);
            Assert.False(c1);

            var (s2, c2) = OfficialSovereignApex100DetailsEngine.RollBlessingOil(0, 80, 10);
            Assert.False(s2);
            Assert.True(c2);
        }

        [Fact]
        public void Test_1786_CalculateWeaponDamageWithCurse()
        {
            Assert.Equal(10, OfficialSovereignApex100DetailsEngine.CalculateWeaponDamageWithCurse(10, 30, 10, 25));
            int dmg = OfficialSovereignApex100DetailsEngine.CalculateWeaponDamageWithCurse(10, 30, 0, 5);
            Assert.Equal(15, dmg);
        }

        [Fact]
        public void Test_1787_IsGuaranteedMaxDamage()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.IsGuaranteedMaxDamage(9));
            Assert.True(OfficialSovereignApex100DetailsEngine.IsGuaranteedMaxDamage(10));
            Assert.False(OfficialSovereignApex100DetailsEngine.IsGuaranteedMaxDamage(8));
        }

        [Fact]
        public void Test_1788_CheckWeaponCurseOnPkKill()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.CheckWeaponCurseOnPkKill(true, 29));
            Assert.False(OfficialSovereignApex100DetailsEngine.CheckWeaponCurseOnPkKill(true, 30));
            Assert.False(OfficialSovereignApex100DetailsEngine.CheckWeaponCurseOnPkKill(false, 10));
        }

        [Fact]
        public void Test_1789_CalculateRepairedMaxDurability()
        {
            Assert.Equal(16, OfficialSovereignApex100DetailsEngine.CalculateRepairedMaxDurability(16, true));
            Assert.Equal(15, OfficialSovereignApex100DetailsEngine.CalculateRepairedMaxDurability(16, false));
        }

        [Fact]
        public void Test_1790_CalculateLuoShaDamage()
        {
            Assert.Equal(15, OfficialSovereignApex100DetailsEngine.CalculateLuoShaDamage(10));
            Assert.Equal(14, OfficialSovereignApex100DetailsEngine.CalculateLuoShaDamage(9));
            Assert.Equal(5, OfficialSovereignApex100DetailsEngine.CalculateLuoShaDamage(0));
        }

        // --------------------------------------------------------------------------------------------
        // 1791 ~ 1800: 未知暗殿、命运之刃打造与武器升级
        // --------------------------------------------------------------------------------------------

        [Fact]
        public void Test_1791_IsUnknownDarkHallBossFake()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.IsUnknownDarkHallBossFake(100));
            Assert.True(OfficialSovereignApex100DetailsEngine.IsUnknownDarkHallBossFake(50));
            Assert.False(OfficialSovereignApex100DetailsEngine.IsUnknownDarkHallBossFake(101));
            Assert.False(OfficialSovereignApex100DetailsEngine.IsUnknownDarkHallBossFake(5000));
        }

        [Fact]
        public void Test_1792_CanHolyWaterPurifyCurse()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.CanHolyWaterPurifyCurse(28));
            Assert.False(OfficialSovereignApex100DetailsEngine.CanHolyWaterPurifyCurse(27));
        }

        [Fact]
        public void Test_1793_ValidateFateBladeGoldOre()
        {
            var (pure, count) = OfficialSovereignApex100DetailsEngine.ValidateFateBladeGoldOre();
            Assert.Equal(15, pure);
            Assert.Equal(3, count);
        }

        [Fact]
        public void Test_1794_ValidateFateBladeDeerMeat()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.ValidateFateBladeDeerMeat(15));
            Assert.True(OfficialSovereignApex100DetailsEngine.ValidateFateBladeDeerMeat(20));
            Assert.False(OfficialSovereignApex100DetailsEngine.ValidateFateBladeDeerMeat(14));
            Assert.False(OfficialSovereignApex100DetailsEngine.ValidateFateBladeDeerMeat(5));
        }

        [Fact]
        public void Test_1795_IsFateBladeArmorBroken()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.IsFateBladeArmorBroken(0));
            Assert.False(OfficialSovereignApex100DetailsEngine.IsFateBladeArmorBroken(1));
        }

        [Fact]
        public void Test_1796_CalculateCasinoBankerSweep()
        {
            Assert.Equal(15, OfficialSovereignApex100DetailsEngine.CalculateCasinoBankerSweep(20));
            Assert.Equal(0, OfficialSovereignApex100DetailsEngine.CalculateCasinoBankerSweep(3));
        }

        [Fact]
        public void Test_1797_CalculateBlackIronDuraBonus()
        {
            Assert.Equal(40_000, OfficialSovereignApex100DetailsEngine.CalculateBlackIronDuraBonus(40));
        }

        [Fact]
        public void Test_1798_CanBlacksmithServicePkPlayer()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.CanBlacksmithServicePkPlayer(199));
            Assert.False(OfficialSovereignApex100DetailsEngine.CanBlacksmithServicePkPlayer(200));
        }

        [Fact]
        public void Test_1799_ApplyBlessingOilToWeapon()
        {
            var (s1, r1) = OfficialSovereignApex100DetailsEngine.ApplyBlessingOilToWeapon(10);
            Assert.True(s1);
            Assert.Equal(5, r1);

            var (s2, r2) = OfficialSovereignApex100DetailsEngine.ApplyBlessingOilToWeapon(4);
            Assert.False(s2);
            Assert.Equal(4, r2);
        }

        [Fact]
        public void Test_1800_GetFateBladeAppraisalTitle()
        {
            Assert.Equal("名剑出鞘", OfficialSovereignApex100DetailsEngine.GetFateBladeAppraisalTitle(100));
            Assert.Equal("神兵初成", OfficialSovereignApex100DetailsEngine.GetFateBladeAppraisalTitle(55));
            Assert.Equal("千锤百炼", OfficialSovereignApex100DetailsEngine.GetFateBladeAppraisalTitle(25));
            Assert.Equal("锋芒毕露", OfficialSovereignApex100DetailsEngine.GetFateBladeAppraisalTitle(8));
            Assert.Equal("凡铁粗坯", OfficialSovereignApex100DetailsEngine.GetFateBladeAppraisalTitle(0));
        }

        // --------------------------------------------------------------------------------------------
        // 1801 ~ 1810: 沙巴克攻城战
        // --------------------------------------------------------------------------------------------

        [Fact]
        public void Test_1801_CheckPalaceDomination()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.CheckPalaceDomination(0, 10));
            Assert.False(OfficialSovereignApex100DetailsEngine.CheckPalaceDomination(1, 10));
            Assert.False(OfficialSovereignApex100DetailsEngine.CheckPalaceDomination(0, 9));
        }

        [Fact]
        public void Test_1802_CalculateGateDamageTaken()
        {
            Assert.Equal(50, OfficialSovereignApex100DetailsEngine.CalculateGateDamageTaken(100, 50));
            Assert.Equal(1, OfficialSovereignApex100DetailsEngine.CalculateGateDamageTaken(40, 50));
        }

        [Fact]
        public void Test_1803_IsGateBroken()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.IsGateBroken(0));
            Assert.False(OfficialSovereignApex100DetailsEngine.IsGateBroken(1));
        }

        [Fact]
        public void Test_1804_CanUseSabukSecretTunnel()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.CanUseSabukSecretTunnel(true, true));
            Assert.False(OfficialSovereignApex100DetailsEngine.CanUseSabukSecretTunnel(false, true));
            Assert.True(OfficialSovereignApex100DetailsEngine.CanUseSabukSecretTunnel(false, false));
        }

        [Fact]
        public void Test_1805_GetSabukRespawnCoords()
        {
            Assert.Equal((640, 290), OfficialSovereignApex100DetailsEngine.GetSabukRespawnCoords(true));
            Assert.Equal((680, 360), OfficialSovereignApex100DetailsEngine.GetSabukRespawnCoords(false));
        }

        [Fact]
        public void Test_1806_IsPkPointExemptInSiege()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.IsPkPointExemptInSiege(true, true));
            Assert.False(OfficialSovereignApex100DetailsEngine.IsPkPointExemptInSiege(false, true));
            Assert.False(OfficialSovereignApex100DetailsEngine.IsPkPointExemptInSiege(true, false));
        }

        [Fact]
        public void Test_1807_GetDailySabukTaxGold()
        {
            Assert.Equal(200_000, OfficialSovereignApex100DetailsEngine.GetDailySabukTaxGold());
        }

        [Fact]
        public void Test_1808_CalculateArcherRespawnCost()
        {
            Assert.Equal(60_000, OfficialSovereignApex100DetailsEngine.CalculateArcherRespawnCost(3));
        }

        [Fact]
        public void Test_1809_ShouldThrottleScreenBroadcast()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.ShouldThrottleScreenBroadcast(101));
            Assert.False(OfficialSovereignApex100DetailsEngine.ShouldThrottleScreenBroadcast(100));
        }

        [Fact]
        public void Test_1810_IsSiegeActive()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.IsSiegeActive(20, 0));
            Assert.True(OfficialSovereignApex100DetailsEngine.IsSiegeActive(22, 59));
            Assert.False(OfficialSovereignApex100DetailsEngine.IsSiegeActive(19, 59));
            Assert.False(OfficialSovereignApex100DetailsEngine.IsSiegeActive(23, 0));
        }

        // --------------------------------------------------------------------------------------------
        // 1811 ~ 1820: 怪物仇恨与道士技能
        // --------------------------------------------------------------------------------------------

        [Fact]
        public void Test_1811_CalculateHealThreatTransferred()
        {
            Assert.Equal(50, OfficialSovereignApex100DetailsEngine.CalculateHealThreatTransferred(100));
            Assert.Equal(1, OfficialSovereignApex100DetailsEngine.CalculateHealThreatTransferred(1));
        }

        [Fact]
        public void Test_1812_IsLineOfSightBlocked()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.IsLineOfSightBlocked(true));
            Assert.False(OfficialSovereignApex100DetailsEngine.IsLineOfSightBlocked(false));
        }

        [Fact]
        public void Test_1813_CalculateGreyPoisonReduction()
        {
            var (def, mdef) = OfficialSovereignApex100DetailsEngine.CalculateGreyPoisonReduction(100, 50);
            Assert.Equal(80, def);
            Assert.Equal(40, mdef);
        }

        [Fact]
        public void Test_1814_CalculateHolyArmorDefBonus()
        {
            Assert.Equal(6, OfficialSovereignApex100DetailsEngine.CalculateHolyArmorDefBonus(20)); // 2 + 4
        }

        [Fact]
        public void Test_1815_CalculateGhostShieldMDefBonus()
        {
            Assert.Equal(7, OfficialSovereignApex100DetailsEngine.CalculateGhostShieldMDefBonus(25)); // 2 + 5
        }

        [Fact]
        public void Test_1816_CalculateTaoistBuffDurationSeconds()
        {
            Assert.Equal(100, OfficialSovereignApex100DetailsEngine.CalculateTaoistBuffDurationSeconds(20)); // 60 + 40
        }

        [Fact]
        public void Test_1817_CanMonsterDetectInvisibility()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.CanMonsterDetectInvisibility(1));
            Assert.True(OfficialSovereignApex100DetailsEngine.CanMonsterDetectInvisibility(2));
            Assert.False(OfficialSovereignApex100DetailsEngine.CanMonsterDetectInvisibility(0));
        }

        [Fact]
        public void Test_1818_CanTrapMonster()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.CanTrapMonster(30, 33));
            Assert.False(OfficialSovereignApex100DetailsEngine.CanTrapMonster(30, 34));
        }

        [Fact]
        public void Test_1819_CalculateGroupHealAmount()
        {
            Assert.Equal(100, OfficialSovereignApex100DetailsEngine.CalculateGroupHealAmount(100, true));
            Assert.Equal(70, OfficialSovereignApex100DetailsEngine.CalculateGroupHealAmount(100, false));
        }

        [Fact]
        public void Test_1820_CalculateRevelationDurationSeconds()
        {
            Assert.Equal(90, OfficialSovereignApex100DetailsEngine.CalculateRevelationDurationSeconds(20)); // 30 + 60
        }

        // --------------------------------------------------------------------------------------------
        // 1821 ~ 1830: 战士刺杀、双烈火与半月
        // --------------------------------------------------------------------------------------------

        [Fact]
        public void Test_1821_CalculateAssassinationDamage()
        {
            // 隔位破防
            Assert.Equal(100, OfficialSovereignApex100DetailsEngine.CalculateAssassinationDamage(100, 50, 2));
            // 贴身计算防御
            Assert.Equal(50, OfficialSovereignApex100DetailsEngine.CalculateAssassinationDamage(100, 50, 1));
        }

        [Fact]
        public void Test_1822_IsFireSwordBuffValid()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.IsFireSwordBuffValid(1000, 10_000));
            Assert.False(OfficialSovereignApex100DetailsEngine.IsFireSwordBuffValid(1000, 11_500));
        }

        [Fact]
        public void Test_1823_CanExecuteDoubleFireSword()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.CanExecuteDoubleFireSword(1000, 9500, 8000));
            Assert.False(OfficialSovereignApex100DetailsEngine.CanExecuteDoubleFireSword(1000, 8500, 8000));
            Assert.False(OfficialSovereignApex100DetailsEngine.CanExecuteDoubleFireSword(1000, 12000, 8000));
        }

        [Fact]
        public void Test_1824_CalculateFireSwordDamage()
        {
            Assert.Equal(260, OfficialSovereignApex100DetailsEngine.CalculateFireSwordDamage(100, 3));
            Assert.Equal(220, OfficialSovereignApex100DetailsEngine.CalculateFireSwordDamage(100, 2));
            Assert.Equal(180, OfficialSovereignApex100DetailsEngine.CalculateFireSwordDamage(100, 1));
        }

        [Fact]
        public void Test_1825_CalculateHalfMoonTargetDamage()
        {
            Assert.Equal(100, OfficialSovereignApex100DetailsEngine.CalculateHalfMoonTargetDamage(100, true));
            Assert.Equal(35, OfficialSovereignApex100DetailsEngine.CalculateHalfMoonTargetDamage(100, false));
        }

        [Fact]
        public void Test_1826_GetSavageThrustSelfStunMs()
        {
            Assert.Equal(1000, OfficialSovereignApex100DetailsEngine.GetSavageThrustSelfStunMs(true));
            Assert.Equal(0, OfficialSovereignApex100DetailsEngine.GetSavageThrustSelfStunMs(false));
        }

        [Fact]
        public void Test_1827_RollAttackSkillBonus()
        {
            var (t1, b1) = OfficialSovereignApex100DetailsEngine.RollAttackSkillBonus(3, 20);
            Assert.True(t1);
            Assert.Equal(8, b1);

            var (t2, b2) = OfficialSovereignApex100DetailsEngine.RollAttackSkillBonus(3, 26);
            Assert.False(t2);
            Assert.Equal(0, b2);
        }

        [Fact]
        public void Test_1828_GetBasicSwordAccuracyBonus()
        {
            Assert.Equal(3, OfficialSovereignApex100DetailsEngine.GetBasicSwordAccuracyBonus(3));
            Assert.Equal(1, OfficialSovereignApex100DetailsEngine.GetBasicSwordAccuracyBonus(1));
        }

        [Fact]
        public void Test_1829_GetNormalAttackIntervalMs()
        {
            Assert.Equal(1100, OfficialSovereignApex100DetailsEngine.GetNormalAttackIntervalMs(0));
            Assert.Equal(1000, OfficialSovereignApex100DetailsEngine.GetNormalAttackIntervalMs(2));
            Assert.Equal(700, OfficialSovereignApex100DetailsEngine.GetNormalAttackIntervalMs(15));
        }

        [Fact]
        public void Test_1830_GetMoveIntervalMs()
        {
            Assert.Equal(600, OfficialSovereignApex100DetailsEngine.GetMoveIntervalMs(true));
            Assert.Equal(800, OfficialSovereignApex100DetailsEngine.GetMoveIntervalMs(false));
        }

        // --------------------------------------------------------------------------------------------
        // 1831 ~ 1840: 法师技能深度机制
        // --------------------------------------------------------------------------------------------

        [Fact]
        public void Test_1831_CheckPetBetrayal()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.CheckPetBetrayal(4));
            Assert.False(OfficialSovereignApex100DetailsEngine.CheckPetBetrayal(5));
        }

        [Fact]
        public void Test_1832_CalculateRepulsion()
        {
            var (p1, d1) = OfficialSovereignApex100DetailsEngine.CalculateRepulsion(35, 30);
            Assert.True(p1);
            Assert.Equal(2, d1);

            var (p2, d2) = OfficialSovereignApex100DetailsEngine.CalculateRepulsion(30, 35);
            Assert.False(p2);
            Assert.Equal(0, d2);
        }

        [Fact]
        public void Test_1833_CalculateHellThunderDamage()
        {
            Assert.Equal(200, OfficialSovereignApex100DetailsEngine.CalculateHellThunderDamage(100, true));
            Assert.Equal(100, OfficialSovereignApex100DetailsEngine.CalculateHellThunderDamage(100, false));
        }

        [Fact]
        public void Test_1834_IsIceStormIgnoringEvade()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.IsIceStormIgnoringEvade());
        }

        [Fact]
        public void Test_1835_ProcessMagicShieldHit()
        {
            // 3级盾吸收60%
            var (a1, p1, b1) = OfficialSovereignApex100DetailsEngine.ProcessMagicShieldHit(100, 3, false);
            Assert.Equal(60, a1);
            Assert.Equal(40, p1);
            Assert.False(b1);

            // 刺杀破盾
            var (a2, p2, b2) = OfficialSovereignApex100DetailsEngine.ProcessMagicShieldHit(100, 3, true);
            Assert.Equal(0, a2);
            Assert.Equal(100, p2);
            Assert.True(b2);
        }

        [Fact]
        public void Test_1836_CalculateFireWallDurationSeconds()
        {
            Assert.Equal(70, OfficialSovereignApex100DetailsEngine.CalculateFireWallDurationSeconds(20)); // 30 + 40
        }

        [Fact]
        public void Test_1837_CalculateLightningDistanceBonus()
        {
            // 距离 5格: bonus = (5-1)*5 = 20%
            Assert.Equal(120, OfficialSovereignApex100DetailsEngine.CalculateLightningDistanceBonus(100, 5));
        }

        [Fact]
        public void Test_1838_RollHolyWordInstantKill()
        {
            Assert.False(OfficialSovereignApex100DetailsEngine.RollHolyWordInstantKill(30, 35, 10));
            Assert.True(OfficialSovereignApex100DetailsEngine.RollHolyWordInstantKill(35, 30, 20)); // chance = (5)*5+10 = 35%
        }

        [Fact]
        public void Test_1839_ValidateTeleportDest()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.ValidateTeleportDest(true, true));
            Assert.False(OfficialSovereignApex100DetailsEngine.ValidateTeleportDest(false, true));
            Assert.False(OfficialSovereignApex100DetailsEngine.ValidateTeleportDest(true, false));
        }

        [Fact]
        public void Test_1840_CanTameMorePets()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.CanTameMorePets(4));
            Assert.False(OfficialSovereignApex100DetailsEngine.CanTameMorePets(5));
        }

        // --------------------------------------------------------------------------------------------
        // 1841 ~ 1850: Direct-Blit 强类型协议零拷贝与 Native AOT 零GC
        // --------------------------------------------------------------------------------------------

        [Fact]
        public void Test_1841_ValidatePacketHeaderBlit()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.ValidatePacketHeaderBlit(0xAA55, 128));
            Assert.False(OfficialSovereignApex100DetailsEngine.ValidatePacketHeaderBlit(0x1234, 128));
            Assert.False(OfficialSovereignApex100DetailsEngine.ValidatePacketHeaderBlit(0xAA55, 5000));
        }

        [Fact]
        public void Test_1842_IsPacketSequenceValid()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.IsPacketSequenceValid(10, 5));
            Assert.False(OfficialSovereignApex100DetailsEngine.IsPacketSequenceValid(5, 10)); // 重复或滞后
            Assert.False(OfficialSovereignApex100DetailsEngine.IsPacketSequenceValid(50, 5)); // 超出32滑动窗口
        }

        [Fact]
        public void Test_1843_IsClientConnectionTimedOut()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.IsClientConnectionTimedOut(1000, 31_000));
            Assert.False(OfficialSovereignApex100DetailsEngine.IsClientConnectionTimedOut(1000, 20_000));
        }

        [Fact]
        public void Test_1844_CanWriteRingBuffer()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.CanWriteRingBuffer(10, 0, 100, 50));
            Assert.False(OfficialSovereignApex100DetailsEngine.CanWriteRingBuffer(90, 0, 100, 50));
        }

        [Fact]
        public void Test_1845_ComputePacketXorChecksum()
        {
            byte[] data = new byte[] { 0x01, 0x02, 0x03 };
            byte checksum = OfficialSovereignApex100DetailsEngine.ComputePacketXorChecksum(data);
            Assert.Equal((byte)(0x5A ^ 0x01 ^ 0x02 ^ 0x03), checksum);
        }

        [Fact]
        public void Test_1846_IsSpeedHackDetected()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.IsSpeedHackDetected(4));
            Assert.False(OfficialSovereignApex100DetailsEngine.IsSpeedHackDetected(3));
        }

        [Fact]
        public void Test_1847_IsMagicCastTimingValid()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.IsMagicCastTimingValid(1000, 1500));
            Assert.False(OfficialSovereignApex100DetailsEngine.IsMagicCastTimingValid(1000, 1400));
        }

        [Fact]
        public void Test_1848_CanSendChatMessage()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.CanSendChatMessage(1000, 2500, false));
            Assert.False(OfficialSovereignApex100DetailsEngine.CanSendChatMessage(1000, 2000, false));
            Assert.True(OfficialSovereignApex100DetailsEngine.CanSendChatMessage(1000, 11000, true));
            Assert.False(OfficialSovereignApex100DetailsEngine.CanSendChatMessage(1000, 5000, true));
        }

        [Fact]
        public void Test_1849_IsEntityInViewRange()
        {
            Assert.True(OfficialSovereignApex100DetailsEngine.IsEntityInViewRange(100, 100, 115, 115));
            Assert.False(OfficialSovereignApex100DetailsEngine.IsEntityInViewRange(100, 100, 116, 100));
        }

        [Fact]
        public void Test_1850_GenerateAuthoritativeDropItemId()
        {
            int itemId1 = OfficialSovereignApex100DetailsEngine.GenerateAuthoritativeDropItemId(12345, 1);
            int itemId2 = OfficialSovereignApex100DetailsEngine.GenerateAuthoritativeDropItemId(12345, 1);
            Assert.Equal(itemId1, itemId2);
            Assert.InRange(itemId1, 1000, 1500);
        }
    }
}
