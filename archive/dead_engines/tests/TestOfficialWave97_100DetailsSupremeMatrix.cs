// ====================================================================================================
// 官方 1.5 细节矩阵 Wave 97: 第 1851 ~ 1950 项微观核心细节权威单元测试验证矩阵
// 严格对齐 OfficialSupremeSovereign100DetailsEngine，全闭环无 Mock 恶意攻击与边界测试
// ====================================================================================================

using System;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests
{
    public class TestOfficialWave97_100DetailsSupremeMatrix
    {
        // --------------------------------------------------------------------------------------------
        // 1851 ~ 1860: 白日门与丛林迷宫微观生态
        // --------------------------------------------------------------------------------------------

        [Fact]
        public void Test_1851_CalculateTianLangSpiderPoison()
        {
            var (s1, d1) = OfficialSupremeSovereign100DetailsEngine.CalculateTianLangSpiderPoison(39);
            Assert.True(s1);
            Assert.Equal(5000, d1);

            var (s2, d2) = OfficialSupremeSovereign100DetailsEngine.CalculateTianLangSpiderPoison(40);
            Assert.False(s2);
            Assert.Equal(0, d2);
        }

        [Fact]
        public void Test_1852_CalculateToxicSpiderDps()
        {
            Assert.Equal(24, OfficialSupremeSovereign100DetailsEngine.CalculateToxicSpiderDps(6)); // (6/2)*8 = 24
            Assert.Equal(0, OfficialSupremeSovereign100DetailsEngine.CalculateToxicSpiderDps(1));
        }

        [Fact]
        public void Test_1853_CanIllusionSpiderSpawnBomber()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.CanIllusionSpiderSpawnBomber(9));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.CanIllusionSpiderSpawnBomber(10));
        }

        [Fact]
        public void Test_1854_CalculateBomberSpiderExplosionDamage()
        {
            // 中心无盾
            Assert.Equal(120, OfficialSupremeSovereign100DetailsEngine.CalculateBomberSpiderExplosionDamage(0, false));
            // 中心有盾 120 * 40% = 48
            Assert.Equal(48, OfficialSupremeSovereign100DetailsEngine.CalculateBomberSpiderExplosionDamage(0, true));
            // 边缘有盾 60 * 40% = 24
            Assert.Equal(24, OfficialSupremeSovereign100DetailsEngine.CalculateBomberSpiderExplosionDamage(1, true));
            // 超出范围
            Assert.Equal(0, OfficialSupremeSovereign100DetailsEngine.CalculateBomberSpiderExplosionDamage(2, false));
        }

        [Fact]
        public void Test_1855_CanPassJungleMazeGate()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.CanPassJungleMazeGate(30));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.CanPassJungleMazeGate(29));
        }

        [Fact]
        public void Test_1856_GetJungleMazeViewRadius()
        {
            Assert.Equal(8, OfficialSupremeSovereign100DetailsEngine.GetJungleMazeViewRadius(true));
            Assert.Equal(15, OfficialSupremeSovereign100DetailsEngine.GetJungleMazeViewRadius(false));
        }

        [Fact]
        public void Test_1857_RollCannibalPlantIngredients()
        {
            var (l1, f1) = OfficialSupremeSovereign100DetailsEngine.RollCannibalPlantIngredients(15);
            Assert.True(l1);
            Assert.True(f1);

            var (l2, f2) = OfficialSupremeSovereign100DetailsEngine.RollCannibalPlantIngredients(34);
            Assert.True(l2);
            Assert.False(f2);
        }

        [Fact]
        public void Test_1858_RollJungleRandomScrollSuccess()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.RollJungleRandomScrollSuccess(20));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.RollJungleRandomScrollSuccess(19));
        }

        [Fact]
        public void Test_1859_IsCenserObstacle()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.IsCenserObstacle(300, 290));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.IsCenserObstacle(300, 291));
        }

        [Fact]
        public void Test_1860_IsInBaiRiMenGuardChaseRange()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.IsInBaiRiMenGuardChaseRange(12));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.IsInBaiRiMenGuardChaseRange(13));
        }

        // --------------------------------------------------------------------------------------------
        // 1861 ~ 1870: 赤月峡谷与抉择之地战术
        // --------------------------------------------------------------------------------------------

        [Fact]
        public void Test_1861_GetChoicePlaceDestination()
        {
            Assert.Equal("恶魔祭坛", OfficialSupremeSovereign100DetailsEngine.GetChoicePlaceDestination(1));
            Assert.Equal("山谷密道", OfficialSupremeSovereign100DetailsEngine.GetChoicePlaceDestination(2));
            Assert.Equal("未知洞窟", OfficialSupremeSovereign100DetailsEngine.GetChoicePlaceDestination(3));
        }

        [Fact]
        public void Test_1862_CanEnterAltarNarrowPassage()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.CanEnterAltarNarrowPassage(0));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.CanEnterAltarNarrowPassage(1));
        }

        [Fact]
        public void Test_1863_IsValidRedMoonLairEntrance()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.IsValidRedMoonLairEntrance(138, 26));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.IsValidRedMoonLairEntrance(138, 27));
        }

        [Fact]
        public void Test_1864_CheckSpiderAlertGroup()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.CheckSpiderAlertGroup(6, 49));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.CheckSpiderAlertGroup(6, 50));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.CheckSpiderAlertGroup(7, 10));
        }

        [Fact]
        public void Test_1865_CalculateBlackSpiderDamageTaken()
        {
            Assert.Equal(15, OfficialSupremeSovereign100DetailsEngine.CalculateBlackSpiderDamageTaken(50, 35));
            Assert.Equal(1, OfficialSupremeSovereign100DetailsEngine.CalculateBlackSpiderDamageTaken(30, 35));
        }

        [Fact]
        public void Test_1866_CalculateBloodGiantRage()
        {
            var (atk1, rage1) = OfficialSupremeSovereign100DetailsEngine.CalculateBloodGiantRage(15, 100, 40);
            Assert.True(rage1);
            Assert.Equal(60, atk1); // 40 * 1.5 = 60

            var (atk2, rage2) = OfficialSupremeSovereign100DetailsEngine.CalculateBloodGiantRage(16, 100, 40);
            Assert.False(rage2);
            Assert.Equal(40, atk2);
        }

        [Fact]
        public void Test_1867_CalculateChoicePlacePotionPrice()
        {
            Assert.Equal(130, OfficialSupremeSovereign100DetailsEngine.CalculateChoicePlacePotionPrice(100));
        }

        [Fact]
        public void Test_1868_GetCanyonSquareSafeCoords()
        {
            Assert.Equal((150, 100), OfficialSupremeSovereign100DetailsEngine.GetCanyonSquareSafeCoords());
        }

        [Fact]
        public void Test_1869_IsMinimapRadarJammedInValleyPassage()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.IsMinimapRadarJammedInValleyPassage(true));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.IsMinimapRadarJammedInValleyPassage(false));
        }

        [Fact]
        public void Test_1870_IsInvisibilityPotionValid()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.IsInvisibilityPotionValid(1000, 60_000, false));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.IsInvisibilityPotionValid(1000, 62_000, false));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.IsInvisibilityPotionValid(1000, 20_000, true));
        }

        // --------------------------------------------------------------------------------------------
        // 1871 ~ 1880: 恶魔祭坛与首领机制
        // --------------------------------------------------------------------------------------------

        [Fact]
        public void Test_1871_CanPlayerButchDoubleHeadKong()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.CanPlayerButchDoubleHeadKong(10, true));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.CanPlayerButchDoubleHeadKong(10, false));
            Assert.True(OfficialSupremeSovereign100DetailsEngine.CanPlayerButchDoubleHeadKong(60, false));
        }

        [Fact]
        public void Test_1872_GetBloodFiendDropRadius()
        {
            Assert.Equal(8, OfficialSupremeSovereign100DetailsEngine.GetBloodFiendDropRadius());
        }

        [Fact]
        public void Test_1873_CalculateDoubleHeadKongThornsDamage()
        {
            Assert.Equal(15, OfficialSupremeSovereign100DetailsEngine.CalculateDoubleHeadKongThornsDamage(100, true));
            Assert.Equal(0, OfficialSupremeSovereign100DetailsEngine.CalculateDoubleHeadKongThornsDamage(100, false));
        }

        [Fact]
        public void Test_1874_RollBloodFiendImmobilize()
        {
            var (imm1, dur1) = OfficialSupremeSovereign100DetailsEngine.RollBloodFiendImmobilize(2, 20);
            Assert.True(imm1);
            Assert.Equal(1000, dur1);

            var (imm2, dur2) = OfficialSupremeSovereign100DetailsEngine.RollBloodFiendImmobilize(4, 10);
            Assert.False(imm2);
            Assert.Equal(0, dur2);
        }

        [Fact]
        public void Test_1875_CanPetBlockPassage()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.CanPetBlockPassage(true, true));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.CanPetBlockPassage(false, true));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.CanPetBlockPassage(true, false));
        }

        [Fact]
        public void Test_1876_IsAltarEntityScanDue()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.IsAltarEntityScanDue(1000, 2000));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.IsAltarEntityScanDue(1000, 1800));
        }

        [Fact]
        public void Test_1877_GetAltarDeathRespawnCoords()
        {
            Assert.Equal(("11", 178, 325), OfficialSupremeSovereign100DetailsEngine.GetAltarDeathRespawnCoords());
        }

        [Fact]
        public void Test_1878_GetDualBossRespawnMinutes()
        {
            Assert.Equal(360, OfficialSupremeSovereign100DetailsEngine.GetDualBossRespawnMinutes());
        }

        [Fact]
        public void Test_1879_IsBossAggroLost()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.IsBossAggroLost(16));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.IsBossAggroLost(15));
        }

        [Fact]
        public void Test_1880_CanStepOntoAltarTile()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.CanStepOntoAltarTile(false));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.CanStepOntoAltarTile(true));
        }

        // --------------------------------------------------------------------------------------------
        // 1881 ~ 1890: 赤月恶魔巢穴地刺与全屏攻击
        // --------------------------------------------------------------------------------------------

        [Fact]
        public void Test_1881_CanRedMoonDemonCastSpikes()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.CanRedMoonDemonCastSpikes(1000, 4000));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.CanRedMoonDemonCastSpikes(1000, 3500));
        }

        [Fact]
        public void Test_1882_CalculateRedMoonSpikeDamage()
        {
            // mitigation = (20/2) + (20/2) = 20. 70 - 20 = 50
            Assert.Equal(50, OfficialSupremeSovereign100DetailsEngine.CalculateRedMoonSpikeDamage(70, 20, 20));
            // 兜底为 5
            Assert.Equal(5, OfficialSupremeSovereign100DetailsEngine.CalculateRedMoonSpikeDamage(20, 100, 100));
        }

        [Fact]
        public void Test_1883_CheckRedMoonDemonSummonMinions()
        {
            var (s1, c1) = OfficialSupremeSovereign100DetailsEngine.CheckRedMoonDemonSummonMinions(30, 100, false);
            Assert.True(s1);
            Assert.Equal(8, c1);

            var (s2, c2) = OfficialSupremeSovereign100DetailsEngine.CheckRedMoonDemonSummonMinions(30, 100, true);
            Assert.False(s2);
            Assert.Equal(0, c2);
        }

        [Fact]
        public void Test_1884_IsRedMoonDemonImmobile()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.IsRedMoonDemonImmobile());
        }

        [Fact]
        public void Test_1885_RollTianZunJewelryDrop()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.RollTianZunJewelryDrop(50));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.RollTianZunJewelryDrop(49));
        }

        [Fact]
        public void Test_1886_RollShieldBreakUnderSpikes()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.RollShieldBreakUnderSpikes(4));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.RollShieldBreakUnderSpikes(5));
        }

        [Fact]
        public void Test_1887_IsTeleportAllowedInRedMoonNest()
        {
            Assert.False(OfficialSupremeSovereign100DetailsEngine.IsTeleportAllowedInRedMoonNest());
        }

        [Fact]
        public void Test_1888_GenerateRedMoonSlayBroadcast()
        {
            string msg = OfficialSupremeSovereign100DetailsEngine.GenerateRedMoonSlayBroadcast("传奇英雄");
            Assert.Contains("传奇英雄", msg);
            Assert.Contains("赤月恶魔", msg);
        }

        [Fact]
        public void Test_1889_IsNestBlindSpot()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.IsNestBlindSpot(14, 18));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.IsNestBlindSpot(14, 19));
        }

        [Fact]
        public void Test_1890_IsRedMoonImmuneToDebuff()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.IsRedMoonImmuneToDebuff("Paralyze"));
            Assert.True(OfficialSupremeSovereign100DetailsEngine.IsRedMoonImmuneToDebuff("Tempt"));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.IsRedMoonImmuneToDebuff("Burn"));
        }

        // --------------------------------------------------------------------------------------------
        // 1891 ~ 1900: 道士核心控制与辅助深度
        // --------------------------------------------------------------------------------------------

        [Fact]
        public void Test_1891_CalculateDivineBeastHp()
        {
            Assert.Equal(500, OfficialSupremeSovereign100DetailsEngine.CalculateDivineBeastHp(0));
            Assert.Equal(2600, OfficialSupremeSovereign100DetailsEngine.CalculateDivineBeastHp(7)); // 500 + 2100
        }

        [Fact]
        public void Test_1892_CalculateDivineBeastBreathDamage()
        {
            Assert.Equal(100, OfficialSupremeSovereign100DetailsEngine.CalculateDivineBeastBreathDamage(100, 1));
            Assert.Equal(60, OfficialSupremeSovereign100DetailsEngine.CalculateDivineBeastBreathDamage(100, 2));
            Assert.Equal(0, OfficialSupremeSovereign100DetailsEngine.CalculateDivineBeastBreathDamage(100, 3));
        }

        [Fact]
        public void Test_1893_CalculateSoulFireDamage()
        {
            int dmg = OfficialSupremeSovereign100DetailsEngine.CalculateSoulFireDamage(10, 20, 0);
            Assert.Equal(20 + (10 * 12 / 10), dmg);
        }

        [Fact]
        public void Test_1894_CalculatePoisonEffects()
        {
            var (def, dps) = OfficialSupremeSovereign100DetailsEngine.CalculatePoisonEffects(100, 30);
            Assert.Equal(80, def);
            Assert.Equal(10, dps);
        }

        [Fact]
        public void Test_1895_CheckTrapBurstOnDamage()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.CheckTrapBurstOnDamage(1));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.CheckTrapBurstOnDamage(0));
        }

        [Fact]
        public void Test_1896_CalculateRingHealBonus()
        {
            Assert.Equal(120, OfficialSupremeSovereign100DetailsEngine.CalculateRingHealBonus(100, true));
            Assert.Equal(100, OfficialSupremeSovereign100DetailsEngine.CalculateRingHealBonus(100, false));
        }

        [Fact]
        public void Test_1897_ShouldBreakInvisibility()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.ShouldBreakInvisibility(true, false));
            Assert.True(OfficialSupremeSovereign100DetailsEngine.ShouldBreakInvisibility(false, true));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.ShouldBreakInvisibility(false, false));
        }

        [Fact]
        public void Test_1898_IsInGroupInvisibilityRange()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.IsInGroupInvisibilityRange(100, 100, 101, 101));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.IsInGroupInvisibilityRange(100, 100, 102, 100));
        }

        [Fact]
        public void Test_1899_GetGhostGlovesSetBonus()
        {
            Assert.Equal(1, OfficialSupremeSovereign100DetailsEngine.GetGhostGlovesSetBonus(true));
            Assert.Equal(0, OfficialSupremeSovereign100DetailsEngine.GetGhostGlovesSetBonus(false));
        }

        [Fact]
        public void Test_1900_GetThreeEyeBraceletHiddenStats()
        {
            var (minMac, maxMac) = OfficialSupremeSovereign100DetailsEngine.GetThreeEyeBraceletHiddenStats();
            Assert.Equal(1, minMac);
            Assert.Equal(1, maxMac);
        }

        // --------------------------------------------------------------------------------------------
        // 1901 ~ 1910: 战士刺杀、半月与烈火进阶判定
        // --------------------------------------------------------------------------------------------

        [Fact]
        public void Test_1901_CalculateAssassinationShieldPierce()
        {
            // 隔位破盾
            var (a1, d1) = OfficialSupremeSovereign100DetailsEngine.CalculateAssassinationShieldPierce(100, 2);
            Assert.Equal(0, a1);
            Assert.Equal(100, d1);

            // 贴身吸收 60%
            var (a2, d2) = OfficialSupremeSovereign100DetailsEngine.CalculateAssassinationShieldPierce(100, 1);
            Assert.Equal(60, a2);
            Assert.Equal(40, d2);
        }

        [Fact]
        public void Test_1902_ProcessHalfMoonSword()
        {
            var (mp, subDmg) = OfficialSupremeSovereign100DetailsEngine.ProcessHalfMoonSword(100);
            Assert.Equal(3, mp);
            Assert.Equal(35, subDmg);
        }

        [Fact]
        public void Test_1903_ExecuteFireSwordAttack()
        {
            var (dmg1, c1) = OfficialSupremeSovereign100DetailsEngine.ExecuteFireSwordAttack(100, true, 3);
            Assert.Equal(260, dmg1);
            Assert.True(c1);

            var (dmg2, c2) = OfficialSupremeSovereign100DetailsEngine.ExecuteFireSwordAttack(100, false, 3);
            Assert.Equal(100, dmg2);
            Assert.False(c2);
        }

        [Fact]
        public void Test_1904_CanExecuteDoubleFireCombo()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.CanExecuteDoubleFireCombo(1000, 9500));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.CanExecuteDoubleFireCombo(1000, 8500));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.CanExecuteDoubleFireCombo(1000, 12000));
        }

        [Fact]
        public void Test_1905_CalculateSavageThrustResult()
        {
            var (p1, b1) = OfficialSupremeSovereign100DetailsEngine.CalculateSavageThrustResult(35, 30);
            Assert.True(p1);
            Assert.False(b1);

            var (p2, b2) = OfficialSupremeSovereign100DetailsEngine.CalculateSavageThrustResult(30, 35);
            Assert.False(p2);
            Assert.True(b2);
        }

        [Fact]
        public void Test_1906_CanCastSavageThrust()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.CanCastSavageThrust(1000, 11_000));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.CanCastSavageThrust(1000, 9000));
        }

        [Fact]
        public void Test_1907_RollAttackSkillCrit()
        {
            var (crit1, b1) = OfficialSupremeSovereign100DetailsEngine.RollAttackSkillCrit(20);
            Assert.True(crit1);
            Assert.Equal(8, b1);

            var (crit2, b2) = OfficialSupremeSovereign100DetailsEngine.RollAttackSkillCrit(30);
            Assert.False(crit2);
            Assert.Equal(0, b2);
        }

        [Fact]
        public void Test_1908_CalculateJingZhongYueDamage()
        {
            int dmg = OfficialSupremeSovereign100DetailsEngine.CalculateJingZhongYueDamage(7, 22, 5);
            Assert.Equal(12, dmg);
        }

        [Fact]
        public void Test_1909_CalculateCaiJueDamage()
        {
            Assert.Equal(30, OfficialSupremeSovereign100DetailsEngine.CalculateCaiJueDamage(9, 0, 30, 5));
            Assert.Equal(5, OfficialSupremeSovereign100DetailsEngine.CalculateCaiJueDamage(5, 0, 30, 5));
        }

        [Fact]
        public void Test_1910_GetGhostNecklaceComboAccuracy()
        {
            Assert.Equal(1, OfficialSupremeSovereign100DetailsEngine.GetGhostNecklaceComboAccuracy(true, true));
            Assert.Equal(0, OfficialSupremeSovereign100DetailsEngine.GetGhostNecklaceComboAccuracy(true, false));
        }

        // --------------------------------------------------------------------------------------------
        // 1911 ~ 1920: 法师冰咆哮、火墙与诱惑进阶
        // --------------------------------------------------------------------------------------------

        [Fact]
        public void Test_1911_DoesIceStormIgnoreMagicEvade()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.DoesIceStormIgnoreMagicEvade(50));
        }

        [Fact]
        public void Test_1912_CalculateStackedFireWallDamage()
        {
            Assert.Equal(60, OfficialSupremeSovereign100DetailsEngine.CalculateStackedFireWallDamage(20, 3));
        }

        [Fact]
        public void Test_1913_CalculateLightningStepBonus()
        {
            Assert.Equal(120, OfficialSupremeSovereign100DetailsEngine.CalculateLightningStepBonus(100, 5));
        }

        [Fact]
        public void Test_1914_GetThunderHitStunMs()
        {
            Assert.Equal(400, OfficialSupremeSovereign100DetailsEngine.GetThunderHitStunMs());
        }

        [Fact]
        public void Test_1915_ProcessHellThunderHit()
        {
            var (d1, s1) = OfficialSupremeSovereign100DetailsEngine.ProcessHellThunderHit(100, true);
            Assert.Equal(200, d1);
            Assert.Equal(500, s1);

            var (d2, s2) = OfficialSupremeSovereign100DetailsEngine.ProcessHellThunderHit(100, false);
            Assert.Equal(100, d2);
            Assert.Equal(0, s2);
        }

        [Fact]
        public void Test_1916_RollTameHuWei()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.RollTameHuWei(14));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.RollTameHuWei(15));
        }

        [Fact]
        public void Test_1917_CalculatePetLevelFromKills()
        {
            Assert.Equal(0, OfficialSupremeSovereign100DetailsEngine.CalculatePetLevelFromKills(19));
            Assert.Equal(1, OfficialSupremeSovereign100DetailsEngine.CalculatePetLevelFromKills(20));
            Assert.Equal(7, OfficialSupremeSovereign100DetailsEngine.CalculatePetLevelFromKills(200));
        }

        [Fact]
        public void Test_1918_GetShieldBreakStunMs()
        {
            Assert.Equal(200, OfficialSupremeSovereign100DetailsEngine.GetShieldBreakStunMs(true));
            Assert.Equal(0, OfficialSupremeSovereign100DetailsEngine.GetShieldBreakStunMs(false));
        }

        [Fact]
        public void Test_1919_RollTeleportSpellSuccess()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.RollTeleportSpellSuccess(10));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.RollTeleportSpellSuccess(9));
        }

        [Fact]
        public void Test_1920_CalculateBoneBladeFireWallDuration()
        {
            Assert.Equal(130, OfficialSupremeSovereign100DetailsEngine.CalculateBoneBladeFireWallDuration(100, true));
            Assert.Equal(100, OfficialSupremeSovereign100DetailsEngine.CalculateBoneBladeFireWallDuration(100, false));
        }

        // --------------------------------------------------------------------------------------------
        // 1921 ~ 1930: 装备磨损、维修与极品掉落
        // --------------------------------------------------------------------------------------------

        [Fact]
        public void Test_1921_CheckDurabilityLossRoll()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.CheckDurabilityLossRoll(0, 100));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.CheckDurabilityLossRoll(0, 99));
            Assert.True(OfficialSupremeSovereign100DetailsEngine.CheckDurabilityLossRoll(1, 50));
            Assert.True(OfficialSupremeSovereign100DetailsEngine.CheckDurabilityLossRoll(2, 200));
        }

        [Fact]
        public void Test_1922_EvaluateDepletedWeapon()
        {
            var (atk1, b1) = OfficialSupremeSovereign100DetailsEngine.EvaluateDepletedWeapon(0, 50);
            Assert.Equal(0, atk1);
            Assert.True(b1);

            var (atk2, b2) = OfficialSupremeSovereign100DetailsEngine.EvaluateDepletedWeapon(1, 50);
            Assert.Equal(50, atk2);
            Assert.False(b2);
        }

        [Fact]
        public void Test_1923_IsEquipmentStatsActive()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.IsEquipmentStatsActive(1));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.IsEquipmentStatsActive(0));
        }

        [Fact]
        public void Test_1924_ApplyNormalRepairPenalty()
        {
            Assert.Equal(15, OfficialSupremeSovereign100DetailsEngine.ApplyNormalRepairPenalty(16));
        }

        [Fact]
        public void Test_1925_CalculateSpecialRepair()
        {
            var (cost, dura) = OfficialSupremeSovereign100DetailsEngine.CalculateSpecialRepair(100, 16);
            Assert.Equal(300, cost);
            Assert.Equal(16, dura);
        }

        [Fact]
        public void Test_1926_ApplyZhanShenOil()
        {
            Assert.Equal(30, OfficialSupremeSovereign100DetailsEngine.ApplyZhanShenOil(30));
        }

        [Fact]
        public void Test_1927_GenerateSuperItemBonus()
        {
            Assert.Equal(1, OfficialSupremeSovereign100DetailsEngine.GenerateSuperItemBonus(69));
            Assert.Equal(2, OfficialSupremeSovereign100DetailsEngine.GenerateSuperItemBonus(70));
            Assert.Equal(5, OfficialSupremeSovereign100DetailsEngine.GenerateSuperItemBonus(99));
        }

        [Fact]
        public void Test_1928_RollBlessingOilStep()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.RollBlessingOilStep(0, 69));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.RollBlessingOilStep(0, 70));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.RollBlessingOilStep(7, 0));
        }

        [Fact]
        public void Test_1929_GetDragonBraceletHitBonus()
        {
            Assert.Equal(5, OfficialSupremeSovereign100DetailsEngine.GetDragonBraceletHitBonus(true));
            Assert.Equal(0, OfficialSupremeSovereign100DetailsEngine.GetDragonBraceletHitBonus(false));
        }

        [Fact]
        public void Test_1930_GetPlatinumRingPoisonAvoidance()
        {
            Assert.Equal(10, OfficialSupremeSovereign100DetailsEngine.GetPlatinumRingPoisonAvoidance(true));
            Assert.Equal(0, OfficialSupremeSovereign100DetailsEngine.GetPlatinumRingPoisonAvoidance(false));
        }

        // --------------------------------------------------------------------------------------------
        // 1931 ~ 1940: PK 规则、红名村与卫士惩罚
        // --------------------------------------------------------------------------------------------

        [Fact]
        public void Test_1931_IsYellowNameTriggered()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.IsYellowNameTriggered(true, false));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.IsYellowNameTriggered(false, false));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.IsYellowNameTriggered(true, true));
        }

        [Fact]
        public void Test_1932_RecordPkKill()
        {
            var (p1, r1) = OfficialSupremeSovereign100DetailsEngine.RecordPkKill(150, true);
            Assert.Equal(250, p1);
            Assert.True(r1);

            var (p2, r2) = OfficialSupremeSovereign100DetailsEngine.RecordPkKill(50, true);
            Assert.Equal(150, p2);
            Assert.False(r2);
        }

        [Fact]
        public void Test_1933_GetGuardDamageOnTarget()
        {
            Assert.Equal(99999, OfficialSupremeSovereign100DetailsEngine.GetGuardDamageOnTarget(true));
            Assert.Equal(0, OfficialSupremeSovereign100DetailsEngine.GetGuardDamageOnTarget(false));
        }

        [Fact]
        public void Test_1934_IsGuaranteedDropOnDeath()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.IsGuaranteedDropOnDeath(true));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.IsGuaranteedDropOnDeath(false));
        }

        [Fact]
        public void Test_1935_GetTownTeleportCoords()
        {
            Assert.Equal(("3", 850, 670), OfficialSupremeSovereign100DetailsEngine.GetTownTeleportCoords(true));
            Assert.Equal(("0", 330, 270), OfficialSupremeSovereign100DetailsEngine.GetTownTeleportCoords(false));
        }

        [Fact]
        public void Test_1936_CalculateNaturalPkDecay()
        {
            Assert.Equal(90, OfficialSupremeSovereign100DetailsEngine.CalculateNaturalPkDecay(100, 10));
            Assert.Equal(0, OfficialSupremeSovereign100DetailsEngine.CalculateNaturalPkDecay(5, 10));
        }

        [Fact]
        public void Test_1937_WashPkPoints()
        {
            var (s1, c1) = OfficialSupremeSovereign100DetailsEngine.WashPkPoints(10, 15000);
            Assert.True(s1);
            Assert.Equal(10000, c1);

            var (s2, c2) = OfficialSupremeSovereign100DetailsEngine.WashPkPoints(10, 5000);
            Assert.False(s2);
            Assert.Equal(0, c2);
        }

        [Fact]
        public void Test_1938_IsSelfDefenseKill()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.IsSelfDefenseKill(true));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.IsSelfDefenseKill(false));
        }

        [Fact]
        public void Test_1939_IsFriendlyFireImmune()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.IsFriendlyFireImmune(true, true));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.IsFriendlyFireImmune(false, true));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.IsFriendlyFireImmune(true, false));
        }

        [Fact]
        public void Test_1940_IsSiegePkExempt()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.IsSiegePkExempt(true, true));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.IsSiegePkExempt(false, true));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.IsSiegePkExempt(true, false));
        }

        // --------------------------------------------------------------------------------------------
        // 1941 ~ 1950: Direct-Blit 协议与 HPSocket AOT 深度优化
        // --------------------------------------------------------------------------------------------

        [Fact]
        public void Test_1941_ValidateDirectBlitHeader()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.ValidateDirectBlitHeader(0xAA55, 1024));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.ValidateDirectBlitHeader(0x1234, 1024));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.ValidateDirectBlitHeader(0xAA55, 5000));
        }

        [Fact]
        public void Test_1942_IsPacketInSlidingWindow()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.IsPacketInSlidingWindow(10, 5));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.IsPacketInSlidingWindow(5, 10)); // 重复
            Assert.False(OfficialSupremeSovereign100DetailsEngine.IsPacketInSlidingWindow(100, 5)); // 超出64
        }

        [Fact]
        public void Test_1943_IsHeartbeatExpired()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.IsHeartbeatExpired(1000, 31_000));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.IsHeartbeatExpired(1000, 20_000));
        }

        [Fact]
        public void Test_1944_HasRingBufferCapacity()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.HasRingBufferCapacity(10, 0, 100, 50));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.HasRingBufferCapacity(95, 0, 100, 50));
        }

        [Fact]
        public void Test_1945_ComputeFastCrc32()
        {
            byte[] data = new byte[] { 0x31, 0x32, 0x33, 0x34 };
            uint crc = OfficialSupremeSovereign100DetailsEngine.ComputeFastCrc32(data);
            Assert.True(crc > 0);
        }

        [Fact]
        public void Test_1946_DetectMoveSpeedHack()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.DetectMoveSpeedHack(4));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.DetectMoveSpeedHack(3));
        }

        [Fact]
        public void Test_1947_VerifyMagicCastCooldown()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.VerifyMagicCastCooldown(1000, 1500));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.VerifyMagicCastCooldown(1000, 1400));
        }

        [Fact]
        public void Test_1948_CanChat()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.CanChat(1000, 2500));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.CanChat(1000, 2000));
        }

        [Fact]
        public void Test_1949_IsWithinBroadcastView()
        {
            Assert.True(OfficialSupremeSovereign100DetailsEngine.IsWithinBroadcastView(100, 100, 115, 115));
            Assert.False(OfficialSupremeSovereign100DetailsEngine.IsWithinBroadcastView(100, 100, 116, 100));
        }

        [Fact]
        public void Test_1950_GenerateServerAuthoritativeLootId()
        {
            int loot1 = OfficialSupremeSovereign100DetailsEngine.GenerateServerAuthoritativeLootId(12345, 1);
            int loot2 = OfficialSupremeSovereign100DetailsEngine.GenerateServerAuthoritativeLootId(12345, 1);
            Assert.Equal(loot1, loot2);
            Assert.InRange(loot1, 2000, 3000);
        }
    }
}
