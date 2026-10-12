// ====================================================================================================
// 官方 1.5 细节矩阵 Wave 98: 第 1951 ~ 2050 项微观核心细节权威单元测试验证矩阵
// 严格对齐 OfficialEternalDominion100DetailsEngine，全闭环无 Mock 恶意攻击与边界测试
// ====================================================================================================

using System;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests
{
    public class TestOfficialWave98_100DetailsDominionMatrix
    {
        // --------------------------------------------------------------------------------------------
        // 1951 ~ 1960: 白日门丛林迷宫与赤月峡谷地宫细节
        // --------------------------------------------------------------------------------------------

        [Fact]
        public void Test_1951_IsTeleportAllowedInBovineHall()
        {
            Assert.False(OfficialEternalDominion100DetailsEngine.IsTeleportAllowedInBovineHall());
        }

        [Fact]
        public void Test_1952_CalculateBovineGeneralArmorShred()
        {
            Assert.Equal(85, OfficialEternalDominion100DetailsEngine.CalculateBovineGeneralArmorShred(100)); // 100 - 15 = 85
        }

        [Fact]
        public void Test_1953_GetPoisonSpiderDeathPoison()
        {
            var (dps, dur) = OfficialEternalDominion100DetailsEngine.GetPoisonSpiderDeathPoison();
            Assert.Equal(6, dps);
            Assert.Equal(10, dur);
        }

        [Fact]
        public void Test_1954_CalculateEvilMageLeech()
        {
            Assert.Equal(20, OfficialEternalDominion100DetailsEngine.CalculateEvilMageLeech(100));
        }

        [Fact]
        public void Test_1955_CheckEvilZombieFeignDeath()
        {
            var (canRevive, hp) = OfficialEternalDominion100DetailsEngine.CheckEvilZombieFeignDeath(1000);
            Assert.True(canRevive);
            Assert.Equal(400, hp);
        }

        [Fact]
        public void Test_1956_IsJungleSecretPassageAccessible()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.IsJungleSecretPassageAccessible(1, 15));
            Assert.False(OfficialEternalDominion100DetailsEngine.IsJungleSecretPassageAccessible(1, 35));
            Assert.False(OfficialEternalDominion100DetailsEngine.IsJungleSecretPassageAccessible(2, 15));
        }

        [Fact]
        public void Test_1957_QueryBossStatus()
        {
            var (s1, r1) = OfficialEternalDominion100DetailsEngine.QueryBossStatus(1000, true, "赤月恶魔");
            Assert.True(s1);
            Assert.Contains("赤月恶魔", r1);
            Assert.Contains("依然在深渊中盘踞肆虐", r1);

            var (s2, r2) = OfficialEternalDominion100DetailsEngine.QueryBossStatus(500, true, "赤月恶魔");
            Assert.False(s2);
        }

        [Fact]
        public void Test_1958_RepairFishingRod()
        {
            var (s1, g1) = OfficialEternalDominion100DetailsEngine.RepairFishingRod(500, 20);
            Assert.True(s1);
            Assert.Equal(300, g1);

            var (s2, g2) = OfficialEternalDominion100DetailsEngine.RepairFishingRod(100, 20);
            Assert.False(s2);
            Assert.Equal(100, g2);
        }

        [Fact]
        public void Test_1959_CalculateBovineKingDamageMitigation()
        {
            // 物理减免 25% (保留 75%)
            Assert.Equal(75, OfficialEternalDominion100DetailsEngine.CalculateBovineKingDamageMitigation(100, false));
            // 魔法减免 20% (保留 80%)
            Assert.Equal(80, OfficialEternalDominion100DetailsEngine.CalculateBovineKingDamageMitigation(100, true));
        }

        [Fact]
        public void Test_1960_GetChestTrapDebuff()
        {
            var (def, mdef, dur) = OfficialEternalDominion100DetailsEngine.GetChestTrapDebuff();
            Assert.Equal(10, def);
            Assert.Equal(10, mdef);
            Assert.Equal(15, dur);
        }

        // --------------------------------------------------------------------------------------------
        // 1961 ~ 1970: 封魔矿区与封魔殿首领战术
        // --------------------------------------------------------------------------------------------

        [Fact]
        public void Test_1961_CheckZombieEmergeTiming()
        {
            var (t1, ms1) = OfficialEternalDominion100DetailsEngine.CheckZombieEmergeTiming(2);
            Assert.True(t1);
            Assert.Equal(800, ms1);

            var (t2, ms2) = OfficialEternalDominion100DetailsEngine.CheckZombieEmergeTiming(3);
            Assert.False(t2);
            Assert.Equal(0, ms2);
        }

        [Fact]
        public void Test_1962_GetFengMoGuardDamage()
        {
            Assert.Equal(99999, OfficialEternalDominion100DetailsEngine.GetFengMoGuardDamage(true));
            Assert.Equal(0, OfficialEternalDominion100DetailsEngine.GetFengMoGuardDamage(false));
        }

        [Fact]
        public void Test_1963_CanEnterSecretPassage()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.CanEnterSecretPassage(true, 35));
            Assert.False(OfficialEternalDominion100DetailsEngine.CanEnterSecretPassage(false, 35));
            Assert.False(OfficialEternalDominion100DetailsEngine.CanEnterSecretPassage(true, 34));
        }

        [Fact]
        public void Test_1964_CalculateRedBoarMoveInterval()
        {
            Assert.Equal(600, OfficialEternalDominion100DetailsEngine.CalculateRedBoarMoveInterval(30, 100));
            Assert.Equal(800, OfficialEternalDominion100DetailsEngine.CalculateRedBoarMoveInterval(31, 100));
        }

        [Fact]
        public void Test_1965_GetEvilSnakePoisonParams()
        {
            var (dur, dmg) = OfficialEternalDominion100DetailsEngine.GetEvilSnakePoisonParams();
            Assert.Equal(15, dur);
            Assert.Equal(10, dmg);
        }

        [Fact]
        public void Test_1966_CalculateRainbowLordLeech()
        {
            Assert.Equal(30, OfficialEternalDominion100DetailsEngine.CalculateRainbowLordLeech(100));
        }

        [Fact]
        public void Test_1967_CalculateAncientTreeTrueDamage()
        {
            Assert.Equal(60, OfficialEternalDominion100DetailsEngine.CalculateAncientTreeTrueDamage(60));
        }

        [Fact]
        public void Test_1968_CalculateOverlordHallRepairCost()
        {
            Assert.Equal(1500, OfficialEternalDominion100DetailsEngine.CalculateOverlordHallRepairCost(1000));
        }

        [Fact]
        public void Test_1969_RollFengMoPlantFruit()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.RollFengMoPlantFruit(19));
            Assert.False(OfficialEternalDominion100DetailsEngine.RollFengMoPlantFruit(20));
        }

        [Fact]
        public void Test_1970_IsValidQiLuBranch()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.IsValidQiLuBranch(2));
            Assert.True(OfficialEternalDominion100DetailsEngine.IsValidQiLuBranch(4));
            Assert.False(OfficialEternalDominion100DetailsEngine.IsValidQiLuBranch(1));
            Assert.False(OfficialEternalDominion100DetailsEngine.IsValidQiLuBranch(3));
        }

        // --------------------------------------------------------------------------------------------
        // 1971 ~ 1980: 武器升级与极品生成深层算法
        // --------------------------------------------------------------------------------------------

        [Fact]
        public void Test_1971_CalculateOreDurabilityDelta()
        {
            Assert.Equal(-1, OfficialEternalDominion100DetailsEngine.CalculateOreDurabilityDelta(59));
            Assert.Equal(0, OfficialEternalDominion100DetailsEngine.CalculateOreDurabilityDelta(60));
            Assert.Equal(0, OfficialEternalDominion100DetailsEngine.CalculateOreDurabilityDelta(89));
            Assert.Equal(1, OfficialEternalDominion100DetailsEngine.CalculateOreDurabilityDelta(90));
            Assert.Equal(2, OfficialEternalDominion100DetailsEngine.CalculateOreDurabilityDelta(120));
        }

        [Fact]
        public void Test_1972_DetermineUpgradeStatType()
        {
            Assert.Equal("DC", OfficialEternalDominion100DetailsEngine.DetermineUpgradeStatType(10, 10, 5));
            Assert.Equal("MC", OfficialEternalDominion100DetailsEngine.DetermineUpgradeStatType(5, 10, 10));
            Assert.Equal("SC", OfficialEternalDominion100DetailsEngine.DetermineUpgradeStatType(5, 5, 10));
        }

        [Fact]
        public void Test_1973_ComputeUpgradeSeed()
        {
            int s1 = OfficialEternalDominion100DetailsEngine.ComputeUpgradeSeed(1, 2, 1000);
            int s2 = OfficialEternalDominion100DetailsEngine.ComputeUpgradeSeed(1, 2, 1000);
            Assert.Equal(s1, s2);
        }

        [Fact]
        public void Test_1974_RollBlessingOilDetailed()
        {
            var (s1, c1) = OfficialEternalDominion100DetailsEngine.RollBlessingOilDetailed(0, 69, 50);
            Assert.True(s1);
            Assert.False(c1);

            var (s2, c2) = OfficialEternalDominion100DetailsEngine.RollBlessingOilDetailed(0, 75, 10);
            Assert.False(s2);
            Assert.True(c2);
        }

        [Fact]
        public void Test_1975_CalculateDamageWithCurseExtreme()
        {
            Assert.Equal(5, OfficialEternalDominion100DetailsEngine.CalculateDamageWithCurseExtreme(5, 30, 10, 20));
            Assert.Equal(10, OfficialEternalDominion100DetailsEngine.CalculateDamageWithCurseExtreme(5, 30, 0, 5));
        }

        [Fact]
        public void Test_1976_CalculateLuoShaExtreme()
        {
            Assert.Equal(15, OfficialEternalDominion100DetailsEngine.CalculateLuoShaExtreme(10));
            Assert.Equal(14, OfficialEternalDominion100DetailsEngine.CalculateLuoShaExtreme(9));
            Assert.Equal(5, OfficialEternalDominion100DetailsEngine.CalculateLuoShaExtreme(0));
        }

        [Fact]
        public void Test_1977_RepairWithZhanShenOil()
        {
            Assert.Equal(30, OfficialEternalDominion100DetailsEngine.RepairWithZhanShenOil(30));
        }

        [Fact]
        public void Test_1978_ApplyDianDaoOffset()
        {
            Assert.Equal(111, OfficialEternalDominion100DetailsEngine.ApplyDianDaoOffset(3)); // 3 * 37 = 111
        }

        [Fact]
        public void Test_1979_RollSuperItemStatBonus()
        {
            Assert.Equal(1, OfficialEternalDominion100DetailsEngine.RollSuperItemStatBonus(69));
            Assert.Equal(2, OfficialEternalDominion100DetailsEngine.RollSuperItemStatBonus(70));
            Assert.Equal(5, OfficialEternalDominion100DetailsEngine.RollSuperItemStatBonus(99));
        }

        [Fact]
        public void Test_1980_GetEffectiveSlashRate()
        {
            Assert.Equal(40, OfficialEternalDominion100DetailsEngine.GetEffectiveSlashRate(50, true));
            Assert.Equal(50, OfficialEternalDominion100DetailsEngine.GetEffectiveSlashRate(50, false));
        }

        // --------------------------------------------------------------------------------------------
        // 1981 ~ 1990: 祈祷套装神兽叛变与记忆套装天地合一权威规则
        // --------------------------------------------------------------------------------------------

        [Fact]
        public void Test_1981_CanTriggerPrayerBeastFrenzy()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.CanTriggerPrayerBeastFrenzy(35, 27));
            Assert.False(OfficialEternalDominion100DetailsEngine.CanTriggerPrayerBeastFrenzy(34, 27));
            Assert.False(OfficialEternalDominion100DetailsEngine.CanTriggerPrayerBeastFrenzy(35, 28));
        }

        [Fact]
        public void Test_1982_GetMemorySetTeleportPartyLimit()
        {
            var (m, a) = OfficialEternalDominion100DetailsEngine.GetMemorySetTeleportPartyLimit();
            Assert.Equal(20, m);
            Assert.Equal(10, a);
        }

        [Fact]
        public void Test_1983_GetZumaSanctumPassCost()
        {
            Assert.Equal(10, OfficialEternalDominion100DetailsEngine.GetZumaSanctumPassCost(11));
            Assert.Equal(20, OfficialEternalDominion100DetailsEngine.GetZumaSanctumPassCost(25));
            Assert.Equal(30, OfficialEternalDominion100DetailsEngine.GetZumaSanctumPassCost(33));
            Assert.Equal(40, OfficialEternalDominion100DetailsEngine.GetZumaSanctumPassCost(43));
            Assert.Equal(0, OfficialEternalDominion100DetailsEngine.GetZumaSanctumPassCost(5));
        }

        [Fact]
        public void Test_1984_IsPrayerItemDestroyedOnDeath()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.IsPrayerItemDestroyedOnDeath(0));
            Assert.False(OfficialEternalDominion100DetailsEngine.IsPrayerItemDestroyedOnDeath(1));
        }

        [Fact]
        public void Test_1985_CalculateMemoryRecallCooldown()
        {
            Assert.Equal(15, OfficialEternalDominion100DetailsEngine.CalculateMemoryRecallCooldown(20));
            Assert.Equal(0, OfficialEternalDominion100DetailsEngine.CalculateMemoryRecallCooldown(3));
        }

        [Fact]
        public void Test_1986_CalculateWarriorTackleWeight()
        {
            Assert.Equal(40_000, OfficialEternalDominion100DetailsEngine.CalculateWarriorTackleWeight(40));
        }

        [Fact]
        public void Test_1987_IsPkPlayerAllowedMemoryRecall()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.IsPkPlayerAllowedMemoryRecall(199));
            Assert.False(OfficialEternalDominion100DetailsEngine.IsPkPlayerAllowedMemoryRecall(200));
        }

        [Fact]
        public void Test_1988_ExchangeGamblingChipToGold()
        {
            var (s1, p1) = OfficialEternalDominion100DetailsEngine.ExchangeGamblingChipToGold(10);
            Assert.True(s1);
            Assert.Equal(5, p1);

            var (s2, p2) = OfficialEternalDominion100DetailsEngine.ExchangeGamblingChipToGold(4);
            Assert.False(s2);
            Assert.Equal(4, p2);
        }

        [Fact]
        public void Test_1989_GetSabukRankTitle()
        {
            Assert.Equal("武勋至尊", OfficialEternalDominion100DetailsEngine.GetSabukRankTitle(100));
            Assert.Equal("傲视群雄", OfficialEternalDominion100DetailsEngine.GetSabukRankTitle(55));
            Assert.Equal("沙城英豪", OfficialEternalDominion100DetailsEngine.GetSabukRankTitle(25));
            Assert.Equal("初出茅庐", OfficialEternalDominion100DetailsEngine.GetSabukRankTitle(8));
            Assert.Equal("白丁", OfficialEternalDominion100DetailsEngine.GetSabukRankTitle(0));
        }

        [Fact]
        public void Test_1990_CalculateGuildPartyExpBonus()
        {
            Assert.Equal(105, OfficialEternalDominion100DetailsEngine.CalculateGuildPartyExpBonus(100, true));
            Assert.Equal(100, OfficialEternalDominion100DetailsEngine.CalculateGuildPartyExpBonus(100, false));
        }

        // --------------------------------------------------------------------------------------------
        // 1991 ~ 2000: 沙巴克皇宫争夺与行会战役细节
        // --------------------------------------------------------------------------------------------

        [Fact]
        public void Test_1991_CheckPalaceCaptureSuccess()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.CheckPalaceCaptureSuccess(0, 10));
            Assert.False(OfficialEternalDominion100DetailsEngine.CheckPalaceCaptureSuccess(1, 10));
            Assert.False(OfficialEternalDominion100DetailsEngine.CheckPalaceCaptureSuccess(0, 9));
        }

        [Fact]
        public void Test_1992_CalculateGateDamage()
        {
            Assert.Equal(50, OfficialEternalDominion100DetailsEngine.CalculateGateDamage(100, 50));
            Assert.Equal(1, OfficialEternalDominion100DetailsEngine.CalculateGateDamage(30, 50));
        }

        [Fact]
        public void Test_1993_IsGateDestroyed()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.IsGateDestroyed(0));
            Assert.False(OfficialEternalDominion100DetailsEngine.IsGateDestroyed(1));
        }

        [Fact]
        public void Test_1994_CanUseSabukTunnel()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.CanUseSabukTunnel(true, true));
            Assert.False(OfficialEternalDominion100DetailsEngine.CanUseSabukTunnel(false, true));
            Assert.True(OfficialEternalDominion100DetailsEngine.CanUseSabukTunnel(false, false));
        }

        [Fact]
        public void Test_1995_GetSiegeRespawnCoords()
        {
            Assert.Equal((640, 290), OfficialEternalDominion100DetailsEngine.GetSiegeRespawnCoords(true));
            Assert.Equal((680, 360), OfficialEternalDominion100DetailsEngine.GetSiegeRespawnCoords(false));
        }

        [Fact]
        public void Test_1996_IsSiegePkExempt()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.IsSiegePkExempt(true, true));
            Assert.False(OfficialEternalDominion100DetailsEngine.IsSiegePkExempt(false, true));
            Assert.False(OfficialEternalDominion100DetailsEngine.IsSiegePkExempt(true, false));
        }

        [Fact]
        public void Test_1997_GetDailySabukTax()
        {
            Assert.Equal(200_000, OfficialEternalDominion100DetailsEngine.GetDailySabukTax());
        }

        [Fact]
        public void Test_1998_GetArcherRespawnCost()
        {
            Assert.Equal(60_000, OfficialEternalDominion100DetailsEngine.GetArcherRespawnCost(3));
        }

        [Fact]
        public void Test_1999_ShouldLimitScreenBroadcast()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.ShouldLimitScreenBroadcast(101));
            Assert.False(OfficialEternalDominion100DetailsEngine.ShouldLimitScreenBroadcast(100));
        }

        [Fact]
        public void Test_2000_IsSiegeOperatingHours()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.IsSiegeOperatingHours(20, 0));
            Assert.True(OfficialEternalDominion100DetailsEngine.IsSiegeOperatingHours(22, 59));
            Assert.False(OfficialEternalDominion100DetailsEngine.IsSiegeOperatingHours(19, 59));
            Assert.False(OfficialEternalDominion100DetailsEngine.IsSiegeOperatingHours(23, 0));
        }

        // --------------------------------------------------------------------------------------------
        // 2001 ~ 2010: 怪物仇恨转移与道士高级辅助
        // --------------------------------------------------------------------------------------------

        [Fact]
        public void Test_2001_CalculateHealHateTransferred()
        {
            Assert.Equal(50, OfficialEternalDominion100DetailsEngine.CalculateHealHateTransferred(100));
            Assert.Equal(1, OfficialEternalDominion100DetailsEngine.CalculateHealHateTransferred(1));
        }

        [Fact]
        public void Test_2002_IsLineOfSightObstructed()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.IsLineOfSightObstructed(true));
            Assert.False(OfficialEternalDominion100DetailsEngine.IsLineOfSightObstructed(false));
        }

        [Fact]
        public void Test_2003_CalculateGreyPoisonReduction()
        {
            var (def, mdef) = OfficialEternalDominion100DetailsEngine.CalculateGreyPoisonReduction(100, 50);
            Assert.Equal(80, def);
            Assert.Equal(40, mdef);
        }

        [Fact]
        public void Test_2004_CalculateHolyArmorBonus()
        {
            Assert.Equal(6, OfficialEternalDominion100DetailsEngine.CalculateHolyArmorBonus(20)); // 2 + 4
        }

        [Fact]
        public void Test_2005_CalculateGhostShieldBonus()
        {
            Assert.Equal(7, OfficialEternalDominion100DetailsEngine.CalculateGhostShieldBonus(25)); // 2 + 5
        }

        [Fact]
        public void Test_2006_CalculateTaoistBuffDuration()
        {
            Assert.Equal(100, OfficialEternalDominion100DetailsEngine.CalculateTaoistBuffDuration(20)); // 60 + 40
        }

        [Fact]
        public void Test_2007_CanMonsterPierceInvisibility()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.CanMonsterPierceInvisibility(1));
            Assert.True(OfficialEternalDominion100DetailsEngine.CanMonsterPierceInvisibility(2));
            Assert.False(OfficialEternalDominion100DetailsEngine.CanMonsterPierceInvisibility(0));
        }

        [Fact]
        public void Test_2008_CanCastTrapOnTarget()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.CanCastTrapOnTarget(30, 33));
            Assert.False(OfficialEternalDominion100DetailsEngine.CanCastTrapOnTarget(30, 34));
        }

        [Fact]
        public void Test_2009_CalculateMassHealDistribution()
        {
            Assert.Equal(100, OfficialEternalDominion100DetailsEngine.CalculateMassHealDistribution(100, true));
            Assert.Equal(70, OfficialEternalDominion100DetailsEngine.CalculateMassHealDistribution(100, false));
        }

        [Fact]
        public void Test_2010_CalculateRevelationDuration()
        {
            Assert.Equal(90, OfficialEternalDominion100DetailsEngine.CalculateRevelationDuration(20)); // 30 + 60
        }

        // --------------------------------------------------------------------------------------------
        // 2011 ~ 2020: 战士刺杀、双烈火与半月微操
        // --------------------------------------------------------------------------------------------

        [Fact]
        public void Test_2011_CalculateAssassinationPierceDamage()
        {
            Assert.Equal(100, OfficialEternalDominion100DetailsEngine.CalculateAssassinationPierceDamage(100, 50, 2));
            Assert.Equal(50, OfficialEternalDominion100DetailsEngine.CalculateAssassinationPierceDamage(100, 50, 1));
        }

        [Fact]
        public void Test_2012_IsFireSwordBuffActive()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.IsFireSwordBuffActive(1000, 10_000));
            Assert.False(OfficialEternalDominion100DetailsEngine.IsFireSwordBuffActive(1000, 12_000));
        }

        [Fact]
        public void Test_2013_CanExecuteDoubleFireWindow()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.CanExecuteDoubleFireWindow(1000, 9500));
            Assert.False(OfficialEternalDominion100DetailsEngine.CanExecuteDoubleFireWindow(1000, 8500));
            Assert.False(OfficialEternalDominion100DetailsEngine.CanExecuteDoubleFireWindow(1000, 12000));
        }

        [Fact]
        public void Test_2014_CalculateLvl3FireSwordDamage()
        {
            Assert.Equal(260, OfficialEternalDominion100DetailsEngine.CalculateLvl3FireSwordDamage(100));
        }

        [Fact]
        public void Test_2015_CalculateHalfMoonSubDamage()
        {
            Assert.Equal(35, OfficialEternalDominion100DetailsEngine.CalculateHalfMoonSubDamage(100));
        }

        [Fact]
        public void Test_2016_GetSavageThrustWallStunMs()
        {
            Assert.Equal(1000, OfficialEternalDominion100DetailsEngine.GetSavageThrustWallStunMs(true));
            Assert.Equal(0, OfficialEternalDominion100DetailsEngine.GetSavageThrustWallStunMs(false));
        }

        [Fact]
        public void Test_2017_RollAttackSkillBonus()
        {
            var (c1, b1) = OfficialEternalDominion100DetailsEngine.RollAttackSkillBonus(20);
            Assert.True(c1);
            Assert.Equal(8, b1);

            var (c2, b2) = OfficialEternalDominion100DetailsEngine.RollAttackSkillBonus(30);
            Assert.False(c2);
            Assert.Equal(0, b2);
        }

        [Fact]
        public void Test_2018_GetBasicSwordAccuracy()
        {
            Assert.Equal(3, OfficialEternalDominion100DetailsEngine.GetBasicSwordAccuracy(3));
            Assert.Equal(1, OfficialEternalDominion100DetailsEngine.GetBasicSwordAccuracy(1));
        }

        [Fact]
        public void Test_2019_CalculateAttackIntervalMs()
        {
            Assert.Equal(1100, OfficialEternalDominion100DetailsEngine.CalculateAttackIntervalMs(0));
            Assert.Equal(1000, OfficialEternalDominion100DetailsEngine.CalculateAttackIntervalMs(2));
            Assert.Equal(700, OfficialEternalDominion100DetailsEngine.CalculateAttackIntervalMs(15));
        }

        [Fact]
        public void Test_2020_GetMovementInterval()
        {
            Assert.Equal(600, OfficialEternalDominion100DetailsEngine.GetMovementInterval(true));
            Assert.Equal(800, OfficialEternalDominion100DetailsEngine.GetMovementInterval(false));
        }

        // --------------------------------------------------------------------------------------------
        // 2021 ~ 2030: 法师诱惑叛变、抗拒火环与冰咆哮
        // --------------------------------------------------------------------------------------------

        [Fact]
        public void Test_2021_RollPetBetrayal()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.RollPetBetrayal(4));
            Assert.False(OfficialEternalDominion100DetailsEngine.RollPetBetrayal(5));
        }

        [Fact]
        public void Test_2022_CalculateRepulsionPush()
        {
            var (p1, d1) = OfficialEternalDominion100DetailsEngine.CalculateRepulsionPush(35, 30);
            Assert.True(p1);
            Assert.Equal(2, d1);

            var (p2, d2) = OfficialEternalDominion100DetailsEngine.CalculateRepulsionPush(30, 35);
            Assert.False(p2);
            Assert.Equal(0, d2);
        }

        [Fact]
        public void Test_2023_CalculateHellThunderUndeadDmg()
        {
            Assert.Equal(200, OfficialEternalDominion100DetailsEngine.CalculateHellThunderUndeadDmg(100, true));
            Assert.Equal(100, OfficialEternalDominion100DetailsEngine.CalculateHellThunderUndeadDmg(100, false));
        }

        [Fact]
        public void Test_2024_IsIceRoarIgnoringEvade()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.IsIceRoarIgnoringEvade());
        }

        [Fact]
        public void Test_2025_EvaluateMagicShieldDamage()
        {
            var (a1, d1, b1) = OfficialEternalDominion100DetailsEngine.EvaluateMagicShieldDamage(100, 3, false);
            Assert.Equal(60, a1);
            Assert.Equal(40, d1);
            Assert.False(b1);

            var (a2, d2, b2) = OfficialEternalDominion100DetailsEngine.EvaluateMagicShieldDamage(100, 3, true);
            Assert.Equal(0, a2);
            Assert.Equal(100, d2);
            Assert.True(b2);
        }

        [Fact]
        public void Test_2026_CalculateFireWallDuration()
        {
            Assert.Equal(70, OfficialEternalDominion100DetailsEngine.CalculateFireWallDuration(20)); // 30 + 40
        }

        [Fact]
        public void Test_2027_CalculateLightningBonus()
        {
            Assert.Equal(120, OfficialEternalDominion100DetailsEngine.CalculateLightningBonus(100, 5));
        }

        [Fact]
        public void Test_2028_RollHolyWordKill()
        {
            Assert.False(OfficialEternalDominion100DetailsEngine.RollHolyWordKill(30, 35, 10));
            Assert.True(OfficialEternalDominion100DetailsEngine.RollHolyWordKill(35, 30, 20));
        }

        [Fact]
        public void Test_2029_ValidateTeleportLocation()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.ValidateTeleportLocation(true, true));
            Assert.False(OfficialEternalDominion100DetailsEngine.ValidateTeleportLocation(false, true));
            Assert.False(OfficialEternalDominion100DetailsEngine.ValidateTeleportLocation(true, false));
        }

        [Fact]
        public void Test_2030_CanTameMoreMinions()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.CanTameMoreMinions(4));
            Assert.False(OfficialEternalDominion100DetailsEngine.CanTameMoreMinions(5));
        }

        // --------------------------------------------------------------------------------------------
        // 2031 ~ 2040: 药水负重平衡与交易安全防刷
        // --------------------------------------------------------------------------------------------

        [Fact]
        public void Test_2031_GetSunBundleWeight()
        {
            Assert.Equal(13, OfficialEternalDominion100DetailsEngine.GetSunBundleWeight(2, 6));
        }

        [Fact]
        public void Test_2032_CanUnpackPotion()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.CanUnpackPotion(5));
            Assert.False(OfficialEternalDominion100DetailsEngine.CanUnpackPotion(4));
        }

        [Fact]
        public void Test_2033_CalculateWearWeightCapacity()
        {
            Assert.Equal(85, OfficialEternalDominion100DetailsEngine.CalculateWearWeightCapacity(35, 0));
            Assert.Equal(65, OfficialEternalDominion100DetailsEngine.CalculateWearWeightCapacity(35, 1));
            Assert.Equal(66, OfficialEternalDominion100DetailsEngine.CalculateWearWeightCapacity(35, 2));
        }

        [Fact]
        public void Test_2034_CheckBagWeightState()
        {
            var (c1, s1) = OfficialEternalDominion100DetailsEngine.CheckBagWeightState(99, 100);
            Assert.True(c1);
            Assert.False(s1);

            var (c2, s2) = OfficialEternalDominion100DetailsEngine.CheckBagWeightState(100, 100);
            Assert.False(c2);
            Assert.False(s2);

            var (c3, s3) = OfficialEternalDominion100DetailsEngine.CheckBagWeightState(120, 100);
            Assert.False(c3);
            Assert.True(s3);
        }

        [Fact]
        public void Test_2035_ResetTradeLocks()
        {
            var (l1, l2) = OfficialEternalDominion100DetailsEngine.ResetTradeLocks();
            Assert.False(l1);
            Assert.False(l2);
        }

        [Fact]
        public void Test_2036_ValidateTradeGoldAtomic()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.ValidateTradeGoldAtomic(1000, 1000, 500));
            Assert.False(OfficialEternalDominion100DetailsEngine.ValidateTradeGoldAtomic(100, 1000, 500));
            Assert.False(OfficialEternalDominion100DetailsEngine.ValidateTradeGoldAtomic(1000, 49_999_999, 2));
        }

        [Fact]
        public void Test_2037_CheckStoragePasswordFail()
        {
            var (lock1, fails1) = OfficialEternalDominion100DetailsEngine.CheckStoragePasswordFail(false, 0);
            Assert.False(lock1);
            Assert.Equal(1, fails1);

            var (lock2, fails2) = OfficialEternalDominion100DetailsEngine.CheckStoragePasswordFail(false, 2);
            Assert.True(lock2);
            Assert.Equal(3, fails2);

            var (lock3, fails3) = OfficialEternalDominion100DetailsEngine.CheckStoragePasswordFail(true, 2);
            Assert.False(lock3);
            Assert.Equal(0, fails3);
        }

        [Fact]
        public void Test_2038_GetGoldDropAppr()
        {
            Assert.Equal(110, OfficialEternalDominion100DetailsEngine.GetGoldDropAppr(50));
            Assert.Equal(111, OfficialEternalDominion100DetailsEngine.GetGoldDropAppr(500));
            Assert.Equal(112, OfficialEternalDominion100DetailsEngine.GetGoldDropAppr(2500));
            Assert.Equal(113, OfficialEternalDominion100DetailsEngine.GetGoldDropAppr(10000));
        }

        [Fact]
        public void Test_2039_CanPickupGroundItemProtected()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.CanPickupGroundItemProtected(10, true, false));
            Assert.True(OfficialEternalDominion100DetailsEngine.CanPickupGroundItemProtected(10, false, true));
            Assert.False(OfficialEternalDominion100DetailsEngine.CanPickupGroundItemProtected(10, false, false));
            Assert.True(OfficialEternalDominion100DetailsEngine.CanPickupGroundItemProtected(30, false, false));
        }

        [Fact]
        public void Test_2040_CanDrinkPotionCooling()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.CanDrinkPotionCooling(1000, 2000));
            Assert.False(OfficialEternalDominion100DetailsEngine.CanDrinkPotionCooling(1000, 1500));
        }

        // --------------------------------------------------------------------------------------------
        // 2041 ~ 2050: Direct-Blit 协议抗丢包与 Native AOT 零分配
        // --------------------------------------------------------------------------------------------

        [Fact]
        public void Test_2041_ValidateDirectBlitPacket()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.ValidateDirectBlitPacket(0xAA55, 1024));
            Assert.False(OfficialEternalDominion100DetailsEngine.ValidateDirectBlitPacket(0x1234, 1024));
            Assert.False(OfficialEternalDominion100DetailsEngine.ValidateDirectBlitPacket(0xAA55, 5000));
        }

        [Fact]
        public void Test_2042_CheckPacketSequenceWindow64()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.CheckPacketSequenceWindow64(10, 5));
            Assert.False(OfficialEternalDominion100DetailsEngine.CheckPacketSequenceWindow64(5, 10)); // 重复
            Assert.False(OfficialEternalDominion100DetailsEngine.CheckPacketSequenceWindow64(100, 5)); // 超出64
        }

        [Fact]
        public void Test_2043_IsClientHeartbeatTimedOut()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.IsClientHeartbeatTimedOut(1000, 31_000));
            Assert.False(OfficialEternalDominion100DetailsEngine.IsClientHeartbeatTimedOut(1000, 20_000));
        }

        [Fact]
        public void Test_2044_HasRingBufferCapacityAvailable()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.HasRingBufferCapacityAvailable(10, 0, 100, 50));
            Assert.False(OfficialEternalDominion100DetailsEngine.HasRingBufferCapacityAvailable(95, 0, 100, 50));
        }

        [Fact]
        public void Test_2045_ComputePacketXor()
        {
            byte[] data = new byte[] { 0x01, 0x02, 0x03 };
            byte expected = (byte)(0xAA ^ 0x01 ^ 0x02 ^ 0x03);
            Assert.Equal(expected, OfficialEternalDominion100DetailsEngine.ComputePacketXor(data));
        }

        [Fact]
        public void Test_2046_IsSpeedHackDetected()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.IsSpeedHackDetected(4));
            Assert.False(OfficialEternalDominion100DetailsEngine.IsSpeedHackDetected(3));
        }

        [Fact]
        public void Test_2047_IsCastTimingHonored()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.IsCastTimingHonored(1000, 1500));
            Assert.False(OfficialEternalDominion100DetailsEngine.IsCastTimingHonored(1000, 1400));
        }

        [Fact]
        public void Test_2048_CanSendChat()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.CanSendChat(1000, 2500, false));
            Assert.False(OfficialEternalDominion100DetailsEngine.CanSendChat(1000, 2000, false));
            Assert.True(OfficialEternalDominion100DetailsEngine.CanSendChat(1000, 11000, true));
            Assert.False(OfficialEternalDominion100DetailsEngine.CanSendChat(1000, 5000, true));
        }

        [Fact]
        public void Test_2049_IsEntityWithinView()
        {
            Assert.True(OfficialEternalDominion100DetailsEngine.IsEntityWithinView(100, 100, 115, 115));
            Assert.False(OfficialEternalDominion100DetailsEngine.IsEntityWithinView(100, 100, 116, 100));
        }

        [Fact]
        public void Test_2050_GenerateAuthoritativeLoot()
        {
            int l1 = OfficialEternalDominion100DetailsEngine.GenerateAuthoritativeLoot(12345, 1);
            int l2 = OfficialEternalDominion100DetailsEngine.GenerateAuthoritativeLoot(12345, 1);
            Assert.Equal(l1, l2);
            Assert.InRange(l1, 3000, 4000);
        }
    }
}
