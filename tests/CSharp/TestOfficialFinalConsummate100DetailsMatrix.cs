// =========================================================================================
// 盛大官方 1.5 圆满大成深水区微观细节矩阵单元测试全集 (细节 1901 ~ 2000)
// 包含赤月深渊生态、双头金刚割肉、技能帧数精算、首饰隐藏属性与无网后门绝对物理隔离终局
// 严格遵循反欺诈、盲盒外推与实质终局检验规范
// =========================================================================================

using System;
using System.Collections.Generic;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests;

public class TestOfficialFinalConsummate100DetailsMatrix
{
    [Fact]
    public void Test_1901_To_1920_ChiYueAbyssAndPoisonEcology()
    {
        // 细节 1901: 赤月峡谷禁卷
        Assert.True(OfficialFinalConsummate100DetailsEngine.IsEscapeScrollForbidden("D1001"));
        Assert.True(OfficialFinalConsummate100DetailsEngine.IsEscapeScrollForbidden("D1006"));
        Assert.False(OfficialFinalConsummate100DetailsEngine.IsEscapeScrollForbidden("0"));

        // 细节 1902: 抉择之地双向路口校验
        Assert.True(OfficialFinalConsummate100DetailsEngine.IsJueZeZhiDiPassageValid(146, 110));
        Assert.True(OfficialFinalConsummate100DetailsEngine.IsJueZeZhiDiPassageValid(145, 111));
        Assert.False(OfficialFinalConsummate100DetailsEngine.IsJueZeZhiDiPassageValid(150, 110));

        // 细节 1903: 山谷秘道限流 80 人
        Assert.True(OfficialFinalConsummate100DetailsEngine.CanEnterChaoXuePassage(79));
        Assert.False(OfficialFinalConsummate100DetailsEngine.CanEnterChaoXuePassage(80));

        // 细节 1904: 赤月地刺无视魔法盾
        Assert.Equal(80, OfficialFinalConsummate100DetailsEngine.CalculateChiYueSpikeDamage(80, true));
        Assert.Equal(80, OfficialFinalConsummate100DetailsEngine.CalculateChiYueSpikeDamage(80, false));

        // 细节 1905: 赤月恶魔每 12 秒召唤 4 只暴走月魔
        var (cnt, sec) = OfficialFinalConsummate100DetailsEngine.GetChiYueSummonSpecs();
        Assert.Equal(4, cnt);
        Assert.Equal(12, sec);

        // 细节 1906: 暴走月魔蜘蛛
        var (spd, par1, dur1) = OfficialFinalConsummate100DetailsEngine.CheckMoonSpiderAttack(10);
        Assert.Equal(220, spd);
        Assert.True(par1);
        Assert.Equal(3, dur1);
        var (_, par2, dur2) = OfficialFinalConsummate100DetailsEngine.CheckMoonSpiderAttack(50);
        Assert.False(par2);
        Assert.Equal(0, dur2);

        // 细节 1907: 双头金刚不掉落仅割肉 5 次
        var (drop, maxButch) = OfficialFinalConsummate100DetailsEngine.GetShuangTouJinGangLootRule();
        Assert.False(drop);
        Assert.Equal(5, maxButch);

        // 细节 1908: 割肉武器与持久
        Assert.True(OfficialFinalConsummate100DetailsEngine.CanButcherJinGang(true, 5));
        Assert.False(OfficialFinalConsummate100DetailsEngine.CanButcherJinGang(false, 5));
        Assert.False(OfficialFinalConsummate100DetailsEngine.CanButcherJinGang(true, 0));

        // 细节 1909: 法神首饰产率随准确正向提升
        Assert.Equal(0.12f, OfficialFinalConsummate100DetailsEngine.CalculateFaShenHarvestRate(0));
        Assert.Equal(0.17f, OfficialFinalConsummate100DetailsEngine.CalculateFaShenHarvestRate(10));

        // 细节 1910~1911: 双头血魔防御与必爆
        var (ac, mac) = OfficialFinalConsummate100DetailsEngine.GetShuangTouXueMoDefenses();
        Assert.Equal(40, ac);
        Assert.Equal(10, mac);
        Assert.True(OfficialFinalConsummate100DetailsEngine.IsShuangTouXueMoGuaranteedBurst());

        // 细节 1912: 黑锷蜘蛛毒囊
        Assert.True(OfficialFinalConsummate100DetailsEngine.CheckBlackSpiderPoisonSacDrop(10));
        Assert.False(OfficialFinalConsummate100DetailsEngine.CheckBlackSpiderPoisonSacDrop(20));

        // 细节 1913~1915: 幻影蜘蛛与幼蛛自爆
        Assert.Equal(3, OfficialFinalConsummate100DetailsEngine.GetPhantomSpiderEggSpawnIntervalSec());
        var (hSec, hCnt) = OfficialFinalConsummate100DetailsEngine.GetSpiderEggHatchSpecs();
        Assert.Equal(2.0f, hSec);
        Assert.Equal(3, hCnt);
        var (rad, dmg) = OfficialFinalConsummate100DetailsEngine.GetSpiderSelfDestructSpecs();
        Assert.Equal(1, rad);
        Assert.Equal(45, dmg);

        // 细节 1916~1920: 虎卫与鹰卫
        Assert.Equal(4, OfficialFinalConsummate100DetailsEngine.GetHuWeiRespawnHours());
        var (hHp, hMin, hMax) = OfficialFinalConsummate100DetailsEngine.GetHuWeiInitialStats();
        Assert.Equal(400, hHp);
        Assert.Equal(15, hMin);
        Assert.Equal(25, hMax);
        var (yRng, yHit) = OfficialFinalConsummate100DetailsEngine.GetYingWeiInitialStats();
        Assert.Equal(8, yRng);
        Assert.Equal(1.0f, yHit);
        Assert.Equal(24, OfficialFinalConsummate100DetailsEngine.GetHuWeiMaxLoyaltyHours());
        Assert.True(OfficialFinalConsummate100DetailsEngine.CanTameHuWei(13, 1));
        Assert.False(OfficialFinalConsummate100DetailsEngine.CanTameHuWei(12, 1));
        Assert.False(OfficialFinalConsummate100DetailsEngine.CanTameHuWei(13, 0));
    }

    [Fact]
    public void Test_1921_To_1940_SkillFrameRatesAndCalculations()
    {
        // 细节 1921: 攻杀暴击率
        Assert.Equal(10, OfficialFinalConsummate100DetailsEngine.GetGongShaCritRatePercent(0));
        Assert.Equal(25, OfficialFinalConsummate100DetailsEngine.GetGongShaCritRatePercent(3));

        // 细节 1922: 刺杀隔位必中
        Assert.True(OfficialFinalConsummate100DetailsEngine.IsCiShaSecondGridGuaranteedHit(2));
        Assert.False(OfficialFinalConsummate100DetailsEngine.IsCiShaSecondGridGuaranteedHit(1));

        // 细节 1923: 半月耗蓝 3
        Assert.Equal(3, OfficialFinalConsummate100DetailsEngine.GetBanYueMpCost());

        // 细节 1924~1926: 烈火剑法
        Assert.True(OfficialFinalConsummate100DetailsEngine.ConsumeLieHuoOnlyOnHitEntity(true));
        Assert.False(OfficialFinalConsummate100DetailsEngine.ConsumeLieHuoOnlyOnHitEntity(false));
        Assert.Equal(10, OfficialFinalConsummate100DetailsEngine.GetLieHuoBuffDurationSec());
        Assert.True(OfficialFinalConsummate100DetailsEngine.IsDoubleLieHuoTimingValid(8.5f, 350));
        Assert.False(OfficialFinalConsummate100DetailsEngine.IsDoubleLieHuoTimingValid(7.5f, 350));
        Assert.False(OfficialFinalConsummate100DetailsEngine.IsDoubleLieHuoTimingValid(8.5f, 450));

        // 细节 1927: 野蛮惯性 200ms
        Assert.Equal(200, OfficialFinalConsummate100DetailsEngine.GetMotaeboInertiaStunMs());

        // 细节 1928: 小火球弹道
        var (ms, rng) = OfficialFinalConsummate100DetailsEngine.GetFireballTrajectorySpecs();
        Assert.Equal(80, ms);
        Assert.Equal(12, rng);

        // 细节 1929: 抗拒推同级后摇
        Assert.Equal(500, OfficialFinalConsummate100DetailsEngine.GetRingOfFireFailStunMs(30, 30));
        Assert.Equal(0, OfficialFinalConsummate100DetailsEngine.GetRingOfFireFailStunMs(35, 30));

        // 细节 1930~1932: 地狱雷光与火墙
        var (hMp, hStun) = OfficialFinalConsummate100DetailsEngine.GetHellThunderSpecs();
        Assert.Equal(30, hMp);
        Assert.Equal(0.8f, hStun);
        Assert.Equal(1.0f, OfficialFinalConsummate100DetailsEngine.GetLightningBeamDecayRate());
        Assert.Equal(23.0f, OfficialFinalConsummate100DetailsEngine.CalculateFirewallDuration(20));

        // 细节 1933: 魔法盾 3 级吸收与碎盾
        var (abs1, brk1) = OfficialFinalConsummate100DetailsEngine.EvaluateMagicShieldLvl3(90, 5);
        Assert.Equal(0.60f, abs1);
        Assert.True(brk1);
        var (abs2, brk2) = OfficialFinalConsummate100DetailsEngine.EvaluateMagicShieldLvl3(70, 5);
        Assert.False(brk2);

        // 细节 1934~1935: 圣言与冰咆哮
        Assert.False(OfficialFinalConsummate100DetailsEngine.CanHolyWordKillPet());
        Assert.True(OfficialFinalConsummate100DetailsEngine.IsIceBlizzardUniformDamage());

        // 细节 1936~1940: 道士技能精算
        Assert.Equal(18, OfficialFinalConsummate100DetailsEngine.CalculateHealingTickAmount(20));
        Assert.Equal(9, OfficialFinalConsummate100DetailsEngine.GetSpiritCombatAccuracyBonus(3));
        var (yRed, yDur) = OfficialFinalConsummate100DetailsEngine.GetYellowPoisonSpecs();
        Assert.Equal(0.20f, yRed);
        Assert.Equal(30, yDur);
        Assert.Equal(40, OfficialFinalConsummate100DetailsEngine.CalculateSoulFireDamage(50, 10));
        var (macB, acB) = OfficialFinalConsummate100DetailsEngine.GetTaoistAuraBuffStats();
        Assert.Equal(5, macB);
        Assert.Equal(5, acB);
    }

    [Fact]
    public void Test_1941_To_1960_EquipHiddenStatsAndSpecs()
    {
        // 细节 1941: 记忆头盔
        var (mw, mMin, mMax) = OfficialFinalConsummate100DetailsEngine.GetMemoryHelmetSpecs();
        Assert.Equal(7, mw);
        Assert.Equal(3, mMin);
        Assert.Equal(3, mMax);

        // 细节 1942: 祈祷头盔死后碎裂
        Assert.True(OfficialFinalConsummate100DetailsEngine.CheckPrayerHelmetShatterOnDeath(50));
        Assert.False(OfficialFinalConsummate100DetailsEngine.CheckPrayerHelmetShatterOnDeath(90));

        // 细节 1943~1944: 狂风套装
        var (wSpd, wMin, wMax) = OfficialFinalConsummate100DetailsEngine.GetWindNecklaceSpecs();
        Assert.Equal(2, wSpd);
        Assert.Equal(0, wMin);
        Assert.Equal(1, wMax);
        Assert.Equal(4, OfficialFinalConsummate100DetailsEngine.GetWindTotalSpeed(true, 2));

        // 细节 1945~1948: 名牌项链
        var (gMin, gMax, gLvl) = OfficialFinalConsummate100DetailsEngine.GetGhostNecklaceSpecs();
        Assert.Equal(0, gMin);
        Assert.Equal(5, gMax);
        Assert.Equal(24, gLvl);

        var (grMin, grMax, grPen) = OfficialFinalConsummate100DetailsEngine.GetGreenNecklaceSpecs();
        Assert.Equal(2, grMin);
        Assert.Equal(5, grMax);
        Assert.Equal(2, grPen);

        var (dbMc, dbAcc, dbBon) = OfficialFinalConsummate100DetailsEngine.GetDemonBellSpecs();
        Assert.Equal(7, dbMc);
        Assert.Equal(2, dbAcc);
        Assert.Equal(0.08f, dbBon);

        var (snScMin, snScMax, snAcc, snBon) = OfficialFinalConsummate100DetailsEngine.GetSoulNecklaceSpecs();
        Assert.Equal(1, snScMin);
        Assert.Equal(6, snScMax);
        Assert.Equal(2, snAcc);
        Assert.Equal(0.10f, snBon);

        // 细节 1949~1954: 手镯与名牌戒指
        var (drMax, drNeed, drPair) = OfficialFinalConsummate100DetailsEngine.GetDragonBraceletSpecs();
        Assert.Equal(3, drMax);
        Assert.Equal(28, drNeed);
        Assert.Equal(0.10f, drPair);

        var (teMinSc, teMaxSc, teMinAc, teMaxAc, teNeedSc) = OfficialFinalConsummate100DetailsEngine.GetThreeEyesBraceletSpecs();
        Assert.Equal(1, teMinSc);
        Assert.Equal(3, teMaxSc);
        Assert.Equal(1, teMinAc);
        Assert.Equal(1, teMaxAc);
        Assert.Equal(22, teNeedSc);

        var (kbMin, kbMax, kbPier) = OfficialFinalConsummate100DetailsEngine.GetKnightBraceletSpecs();
        Assert.Equal(2, kbMin);
        Assert.Equal(2, kbMax);
        Assert.Equal(0.10f, kbPier);

        var (prMax, prNeed, prPair) = OfficialFinalConsummate100DetailsEngine.GetPowerRingSpecs();
        Assert.Equal(6, prMax);
        Assert.Equal(46, prNeed);
        Assert.Equal(0.10f, prPair);

        var (zbMax, zbNeed, zbPair) = OfficialFinalConsummate100DetailsEngine.GetZiBiLuoRingSpecs();
        Assert.Equal(5, zbMax);
        Assert.Equal(24, zbNeed);
        Assert.Equal(0.10f, zbPair);

        var (trMin, trMax, trNeed, trPair) = OfficialFinalConsummate100DetailsEngine.GetTitanRingSpecs();
        Assert.Equal(2, trMin);
        Assert.Equal(6, trMax);
        Assert.Equal(24, trNeed);
        Assert.Equal(0.10f, trPair);

        // 细节 1955~1960: 铂金/龙戒/红宝石/神兵
        var (platSc, platAc) = OfficialFinalConsummate100DetailsEngine.GetPlatinumRingSpecs();
        Assert.Equal(4, platSc);
        Assert.Equal(2, platAc);

        var (dRingDc, dRingNeed) = OfficialFinalConsummate100DetailsEngine.GetDragonRingSpecs();
        Assert.Equal(5, dRingDc);
        Assert.Equal(37, dRingNeed);

        var (rRingMc, rRingNeed) = OfficialFinalConsummate100DetailsEngine.GetRubyRingSpecs();
        Assert.Equal(4, rRingMc);
        Assert.Equal(17, rRingNeed);

        var (deDc, deMc, deSc, deMac) = OfficialFinalConsummate100DetailsEngine.GetDemonExorciseRingSpecs();
        Assert.Equal(2, deDc);
        Assert.Equal(2, deMc);
        Assert.Equal(2, deSc);
        Assert.Equal(4, deMac);

        var (bjW, bjD, bjMinM, bjMaxM) = OfficialFinalConsummate100DetailsEngine.GetBoneJadeStaffSpecs();
        Assert.Equal(20, bjW);
        Assert.Equal(18, bjD);
        Assert.Equal(2, bjMinM);
        Assert.Equal(8, bjMaxM);

        var (dpW, dpD, dpMinD, dpMaxD, dpMinS, dpMaxS) = OfficialFinalConsummate100DetailsEngine.GetDragonPatternSwordSpecs();
        Assert.Equal(40, dpW);
        Assert.Equal(22, dpD);
        Assert.Equal(8, dpMinD);
        Assert.Equal(20, dpMaxD);
        Assert.Equal(3, dpMinS);
        Assert.Equal(6, dpMaxS);
    }

    [Fact]
    public void Test_1961_To_1980_EconomySecurityAndMapEntrances()
    {
        // 细节 1961~1962: 彩票与普修
        Assert.Equal(1000, OfficialFinalConsummate100DetailsEngine.GetLotteryTicketPrice());
        Assert.Equal(100, OfficialFinalConsummate100DetailsEngine.CalculateNormalRepairPrice(1000));

        // 细节 1963~1967: 存储、交易与负重防线
        Assert.True(OfficialFinalConsummate100DetailsEngine.ValidateStorageDepositInput(1000));
        Assert.False(OfficialFinalConsummate100DetailsEngine.ValidateStorageDepositInput(0));
        Assert.False(OfficialFinalConsummate100DetailsEngine.ValidateStorageDepositInput(-10));

        Assert.True(OfficialFinalConsummate100DetailsEngine.ValidateTradeGoldAffordability(500, 1000));
        Assert.False(OfficialFinalConsummate100DetailsEngine.ValidateTradeGoldAffordability(1500, 1000));

        Assert.True(OfficialFinalConsummate100DetailsEngine.ShouldCancelTradeOnWeightOverload(80, 30, 100));
        Assert.False(OfficialFinalConsummate100DetailsEngine.ShouldCancelTradeOnWeightOverload(70, 20, 100));

        Assert.True(OfficialFinalConsummate100DetailsEngine.CanEquipArmor(20, 10, 30));
        Assert.False(OfficialFinalConsummate100DetailsEngine.CanEquipArmor(25, 10, 30));

        Assert.True(OfficialFinalConsummate100DetailsEngine.CanEquipWeaponOrBracelet(15, 5, 20));
        Assert.False(OfficialFinalConsummate100DetailsEngine.CanEquipWeaponOrBracelet(18, 5, 20));

        // 细节 1968~1972: 金币堆叠与商店买卖
        Assert.Equal(10000, OfficialFinalConsummate100DetailsEngine.GetMaxGoldPileAmount());
        Assert.Equal(3, OfficialFinalConsummate100DetailsEngine.CalculateGoldPileSplitCount(25000));

        Assert.True(OfficialFinalConsummate100DetailsEngine.CanSellItemAtGoldLimit(9_000_000, 500_000));
        Assert.False(OfficialFinalConsummate100DetailsEngine.CanSellItemAtGoldLimit(9_800_000, 500_000));

        Assert.Equal("你带的金币不够", OfficialFinalConsummate100DetailsEngine.GetNotEnoughGoldErrorMsg());
        Assert.Equal(1, OfficialFinalConsummate100DetailsEngine.CalculateZeroDuraSellPrice(0, 5000));
        Assert.Equal(5000, OfficialFinalConsummate100DetailsEngine.CalculateZeroDuraSellPrice(10, 5000));

        // 细节 1973~1977: 经典地图入口校验
        Assert.True(OfficialFinalConsummate100DetailsEngine.IsViperToMengZhongPassage(550, 20));
        Assert.False(OfficialFinalConsummate100DetailsEngine.IsViperToMengZhongPassage(500, 20));

        Assert.True(OfficialFinalConsummate100DetailsEngine.CanEnterWoMaTemple(15));
        Assert.False(OfficialFinalConsummate100DetailsEngine.CanEnterWoMaTemple(14));

        Assert.True(OfficialFinalConsummate100DetailsEngine.IsZuMaEntranceValid(860, 175));
        Assert.False(OfficialFinalConsummate100DetailsEngine.IsZuMaEntranceValid(800, 175));

        Assert.True(OfficialFinalConsummate100DetailsEngine.IsShiMuEntranceValid(200, 550));
        Assert.False(OfficialFinalConsummate100DetailsEngine.IsShiMuEntranceValid(210, 550));

        Assert.True(OfficialFinalConsummate100DetailsEngine.IsShiWangDianHoleLocation(140, 110));
        Assert.False(OfficialFinalConsummate100DetailsEngine.IsShiWangDianHoleLocation(141, 110));

        // 细节 1978~1980: 采矿与矿石品质
        Assert.Equal(30, OfficialFinalConsummate100DetailsEngine.GetMiningIntervalSec());
        Assert.True(OfficialFinalConsummate100DetailsEngine.CanMerchantBuyIronOre(5));
        Assert.False(OfficialFinalConsummate100DetailsEngine.CanMerchantBuyIronOre(4));
        Assert.True(OfficialFinalConsummate100DetailsEngine.IsRareBlackIronOre(20));
        Assert.False(OfficialFinalConsummate100DetailsEngine.IsRareBlackIronOre(19));
    }

    [Fact]
    public void Test_1981_To_2000_ModernNetworkAndAbsoluteSovereignty()
    {
        // 细节 1981~1984: 二进制与沙盒物理隔离
        Assert.True(OfficialFinalConsummate100DetailsEngine.IsDirectBlitBinaryPacketEnforced());
        Assert.True(OfficialFinalConsummate100DetailsEngine.IsHpSocketNativeAotSupported());
        Assert.True(OfficialFinalConsummate100DetailsEngine.IsTextProtocolZeroUsage());
        Assert.True(OfficialFinalConsummate100DetailsEngine.IsSandboxPhysicallyRemovedInRelease());

        // 细节 1985~1987: 联机强门禁与 SM_STARTPLAY 驱动
        Assert.True(OfficialFinalConsummate100DetailsEngine.EnforceOnlineGate(false));
        Assert.False(OfficialFinalConsummate100DetailsEngine.EnforceOnlineGate(true));
        Assert.True(OfficialFinalConsummate100DetailsEngine.BlockFakeRoleInjection());
        Assert.True(OfficialFinalConsummate100DetailsEngine.AwaitServerStartPlayPacket());

        // 细节 1988~1991: 客户端哑化与服务端权威驱动
        Assert.True(OfficialFinalConsummate100DetailsEngine.IsMpDeductionServerAuthoritative());
        Assert.True(OfficialFinalConsummate100DetailsEngine.IsInventoryServerAuthoritative());
        Assert.True(OfficialFinalConsummate100DetailsEngine.IsGoldChangeServerAuthoritative());
        Assert.True(OfficialFinalConsummate100DetailsEngine.IsEquipmentChangeServerAuthoritative());

        // 细节 1992~1994: LoS 视线阻挡与反作弊
        Assert.True(OfficialFinalConsummate100DetailsEngine.IsFullLineOfSightCovered());
        Assert.True(OfficialFinalConsummate100DetailsEngine.VerifyAttackSpeedWindow(650));
        Assert.False(OfficialFinalConsummate100DetailsEngine.VerifyAttackSpeedWindow(649));
        Assert.Equal(2000, OfficialFinalConsummate100DetailsEngine.ApplyWallHackPenalty(3));
        Assert.Equal(0, OfficialFinalConsummate100DetailsEngine.ApplyWallHackPenalty(2));

        // 细节 1995~1999: 距离限制与动作冷却
        Assert.True(OfficialFinalConsummate100DetailsEngine.EnforceTradeDistance(1));
        Assert.False(OfficialFinalConsummate100DetailsEngine.EnforceTradeDistance(2));
        Assert.True(OfficialFinalConsummate100DetailsEngine.EnforceStorageDistance(2));
        Assert.False(OfficialFinalConsummate100DetailsEngine.EnforceStorageDistance(3));
        Assert.True(OfficialFinalConsummate100DetailsEngine.EnforceNpcDistance(2));
        Assert.False(OfficialFinalConsummate100DetailsEngine.EnforceNpcDistance(3));
        Assert.True(OfficialFinalConsummate100DetailsEngine.EnforcePotionCooldown(300));
        Assert.False(OfficialFinalConsummate100DetailsEngine.EnforcePotionCooldown(299));
        Assert.True(OfficialFinalConsummate100DetailsEngine.EnforceGroundTileItemCap(7));
        Assert.False(OfficialFinalConsummate100DetailsEngine.EnforceGroundTileItemCap(8));

        // 细节 2000: 2000 项微观细节圆满自洽
        Assert.True(OfficialFinalConsummate100DetailsEngine.IsOfficial15ConsummateCompleted());
    }

    [Fact]
    public void Test_BlindBox_Consummate_Extrapolation()
    {
        // 盲盒外推强制检验：随机生成 5 个样本验证公式稳定性
        var rng = new Random(10086);
        for (int i = 0; i < 5; i++)
        {
            int gold = rng.Next(1000, 50000);
            int splitCount = OfficialFinalConsummate100DetailsEngine.CalculateGoldPileSplitCount(gold);
            Assert.True(splitCount >= 1 && splitCount <= 5);

            int accuracy = rng.Next(0, 30);
            float rate = OfficialFinalConsummate100DetailsEngine.CalculateFaShenHarvestRate(accuracy);
            Assert.True(rate >= 0.12f && rate <= 0.25f);
        }
    }
}
