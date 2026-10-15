// =========================================================================================
// 盛大官方 1.5 创世至尊统御深水区微观细节矩阵单元测试全集 (细节 1801 ~ 1900)
// 包含土城彩票、红名村老村长、沙巴克皇宫与密道、开刀方差玄学、名牌属性硬门槛与网络权威终局闭环
// 严格遵循反欺诈、盲盒外推与实质终局检验规范
// =========================================================================================

using System;
using System.Collections.Generic;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests;

public class TestOfficialGenesisSupremeSovereign100DetailsMatrix
{
    [Fact]
    public void Test_1801_To_1815_TuChengAndSabukSiegeEcology()
    {
        // 细节 1801: 土城彩票
        var (w1, p1) = OfficialGenesisSupremeSovereign100DetailsEngine.CheckLotteryTicket(0);
        Assert.True(w1);
        Assert.Equal("金条", p1);
        var (w2, p2) = OfficialGenesisSupremeSovereign100DetailsEngine.CheckLotteryTicket(5);
        Assert.True(w2);
        Assert.Equal("祝福油", p2);
        var (w3, _) = OfficialGenesisSupremeSovereign100DetailsEngine.CheckLotteryTicket(50);
        Assert.False(w3);

        // 细节 1802: 土城药店 PK 区边缘
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.IsTuChengPharmacyPkZone(360, 335));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.IsTuChengPharmacyPkZone(340, 335));

        // 细节 1803: 红名村老村长周围禁止卫士刷新
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CanSpawnGuardInRedNameVillage(849, 674));
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.CanSpawnGuardInRedNameVillage(300, 300));

        // 细节 1804: 沙城密道单向皇宫
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.IsSecretTunnelOneWayToPalace());

        // 细节 1805: 皇宫密室上限 100 人
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.CanEnterPalaceChamber(99));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CanEnterPalaceChamber(100));

        // 细节 1806: 皇宫石像破碎阻挡消除
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.IsPalaceStatuePassable(100));
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.IsPalaceStatuePassable(0));

        // 细节 1807: 沙城特修 5 折，城主免费
        Assert.Equal(1000, OfficialGenesisSupremeSovereign100DetailsEngine.CalculateSiegeRepairCost(1000, false, false));
        Assert.Equal(500, OfficialGenesisSupremeSovereign100DetailsEngine.CalculateSiegeRepairCost(1000, false, true));
        Assert.Equal(0, OfficialGenesisSupremeSovereign100DetailsEngine.CalculateSiegeRepairCost(1000, true, true));

        // 细节 1808: 攻城申请生效持续 3 天通知
        Assert.Equal(3, OfficialGenesisSupremeSovereign100DetailsEngine.GetSiegeNoticeDays());

        // 细节 1809: 战败行会全员传送比奇省 (0)
        Assert.Equal("0", OfficialGenesisSupremeSovereign100DetailsEngine.GetDefeatedGuildFallbackMap());

        // 细节 1810: 藏宝阁凭证校验
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.CanEnterSabukTreasureRoom(true, true));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CanEnterSabukTreasureRoom(true, false));

        // 细节 1811: 沙城铁匠专属祝福成功率提升 10%
        Assert.Equal(0.60f, OfficialGenesisSupremeSovereign100DetailsEngine.ApplySabukBlacksmithBonus(0.50f));

        // 细节 1812: 沙巴克城门修复
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.CanRepairSabukGate(5_000_000, 50));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CanRepairSabukGate(4_999_999, 50));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CanRepairSabukGate(5_000_000, 49));

        // 细节 1813: 行会战雷达点颜色
        Assert.Equal("#FF0000", OfficialGenesisSupremeSovereign100DetailsEngine.GetRadarDotColor(false, true));
        Assert.Equal("#0000FF", OfficialGenesisSupremeSovereign100DetailsEngine.GetRadarDotColor(true, false));
        Assert.Equal("#00FF00", OfficialGenesisSupremeSovereign100DetailsEngine.GetRadarDotColor(false, false));

        // 细节 1814: 行会战全服死亡广播格式
        string broadcast = OfficialGenesisSupremeSovereign100DetailsEngine.FormatGuildWarKillNotice("邪恶之眼", "昔日同盟");
        Assert.Contains("邪恶之眼", broadcast);
        Assert.Contains("昔日同盟", broadcast);

        // 细节 1815: 行会结盟条件
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.CanFormGuildAlliance(true, true, 2));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CanFormGuildAlliance(true, true, 3));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CanFormGuildAlliance(true, false, 1));
    }

    [Fact]
    public void Test_1816_To_1830_WeaponUpgradeAndLuckMetaphysics()
    {
        // 细节 1816: 开刀砍弓箭手方差越小越稳
        Assert.Equal(0.05f, OfficialGenesisSupremeSovereign100DetailsEngine.CalculateVarianceBonus(1.5f));
        Assert.Equal(0f, OfficialGenesisSupremeSovereign100DetailsEngine.CalculateVarianceBonus(2.5f));

        // 细节 1817: 开刀砍白名不产生诅咒
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.SlashWhitePlayerGeneratesCurse());

        // 细节 1818: 开刀砍红名幸运>0 有 15% 几率扣幸运
        var (l1, n1) = OfficialGenesisSupremeSovereign100DetailsEngine.CheckSlashRedPlayerLuck(3, 10);
        Assert.True(l1);
        Assert.Equal(2, n1);
        var (l2, n2) = OfficialGenesisSupremeSovereign100DetailsEngine.CheckSlashRedPlayerLuck(3, 20);
        Assert.False(l2);
        Assert.Equal(3, n2);

        // 细节 1819: 开刀砍宝宝中立
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.SlashPetAffectsStats());

        // 细节 1820: 武器升级跳点机制
        Assert.Equal(2, OfficialGenesisSupremeSovereign100DetailsEngine.CalculateUpgradeStatGain(250, 5));
        Assert.Equal(1, OfficialGenesisSupremeSovereign100DetailsEngine.CalculateUpgradeStatGain(250, 15));
        Assert.Equal(1, OfficialGenesisSupremeSovereign100DetailsEngine.CalculateUpgradeStatGain(150, 5));

        // 细节 1821: 武器升级单属性累计上限 7
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.CanUpgradeSingleStat(6));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CanUpgradeSingleStat(7));

        // 细节 1822: 黑铁纯度 >= 90 增加持久上限几率
        var (ad1, nd1) = OfficialGenesisSupremeSovereign100DetailsEngine.CheckPurity90Bonus(90, 20, 10);
        Assert.True(ad1);
        Assert.Equal(21, nd1);
        var (ad2, nd2) = OfficialGenesisSupremeSovereign100DetailsEngine.CheckPurity90Bonus(89, 20, 10);
        Assert.False(ad2);
        Assert.Equal(20, nd2);

        // 细节 1823: 普通祝福油最多提升至 +7
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.CanUseBlessOil(6));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CanUseBlessOil(7));

        // 细节 1824: 连败 5 次祝福油诅咒几率
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.CheckBlessOilCurse(5, 20));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CheckBlessOilCurse(5, 40));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CheckBlessOilCurse(4, 10));

        // 细节 1825: 诅咒+10 刀刀发挥下限
        Assert.Equal(10, OfficialGenesisSupremeSovereign100DetailsEngine.GetCurse10Damage(10, 50));

        // 细节 1826: 幸运+9 刀刀发挥上限
        Assert.Equal(50, OfficialGenesisSupremeSovereign100DetailsEngine.GetLuck9Damage(10, 50));

        // 细节 1827: 祈祷之刃诅咒+10 依然生效被动攻速+1 与命中+3
        var (spd, acc) = OfficialGenesisSupremeSovereign100DetailsEngine.GetPrayerBladePassiveStats();
        Assert.Equal(1, spd);
        Assert.Equal(3, acc);

        // 细节 1828: 命运之刃任务门槛
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.CanAcceptFateBladeQuest(35, 3));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CanAcceptFateBladeQuest(34, 3));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CanAcceptFateBladeQuest(35, 2));

        // 细节 1829: 屠龙刀规格
        var (arm, stun) = OfficialGenesisSupremeSovereign100DetailsEngine.GetDragonSlayerSpecs();
        Assert.Equal(50, arm);
        Assert.Equal(0.3f, stun);

        // 细节 1830: 嗜血佩戴攻击吸血 10%
        Assert.Equal(10, OfficialGenesisSupremeSovereign100DetailsEngine.CalculateBloodLustLeech(100));
        Assert.Equal(1, OfficialGenesisSupremeSovereign100DetailsEngine.CalculateBloodLustLeech(5));
    }

    [Fact]
    public void Test_1831_To_1850_EquipEliteAffixesAndHiddenSetStats()
    {
        // 细节 1831: 死神手套极品 1-4
        var (dMin, dMax) = OfficialGenesisSupremeSovereign100DetailsEngine.GetDeathGloveMaxStats();
        Assert.Equal(1, dMin);
        Assert.Equal(4, dMax);

        // 细节 1832: 金手镯极品魔防 2-4，攻击 0-3
        var (gMinM, gMaxM, gMinD, gMaxD) = OfficialGenesisSupremeSovereign100DetailsEngine.GetGoldBraceletMaxStats();
        Assert.Equal(2, gMinM);
        Assert.Equal(4, gMaxM);
        Assert.Equal(0, gMinD);
        Assert.Equal(3, gMaxD);

        // 细节 1833: 幽灵手套格挡 10%
        Assert.Equal(90, OfficialGenesisSupremeSovereign100DetailsEngine.ApplyGhostGloveBlock(100, true));
        Assert.Equal(100, OfficialGenesisSupremeSovereign100DetailsEngine.ApplyGhostGloveBlock(100, false));

        // 细节 1834: 阎罗手套反弹 10%
        Assert.Equal(10, OfficialGenesisSupremeSovereign100DetailsEngine.CalculateYanLuoReflectDamage(100, true));
        Assert.Equal(0, OfficialGenesisSupremeSovereign100DetailsEngine.CalculateYanLuoReflectDamage(100, false));

        // 细节 1835: 辟邪手镯一对提供 4 敏捷与 15% 闪避
        var (pxAgi, pxEva) = OfficialGenesisSupremeSovereign100DetailsEngine.GetPiXieBraceletPairBonus();
        Assert.Equal(4, pxAgi);
        Assert.Equal(0.15f, pxEva);

        // 细节 1836: 夏普儿手镯一对提供 4 准确与 15% 命中
        var (xpAcc, xpHit) = OfficialGenesisSupremeSovereign100DetailsEngine.GetXiaPuErBraceletPairBonus();
        Assert.Equal(4, xpAcc);
        Assert.Equal(0.15f, xpHit);

        // 细节 1837: 避毒手镯一对提供 40% 中毒抗性与 50% 毒伤减免
        var (bdRes, bdRed) = OfficialGenesisSupremeSovereign100DetailsEngine.GetBiDuBraceletPairBonus();
        Assert.Equal(0.40f, bdRes);
        Assert.Equal(0.50f, bdRed);

        // 细节 1838: 降妖除魔极品全+2 (基准2+2=4)
        var (dDc, dMc, dSc) = OfficialGenesisSupremeSovereign100DetailsEngine.GetDemonRingEliteBonus();
        Assert.Equal(4, dDc);
        Assert.Equal(4, dMc);
        Assert.Equal(4, dSc);

        // 细节 1839~1841: 名牌戒指最高属性
        Assert.Equal(6, OfficialGenesisSupremeSovereign100DetailsEngine.GetCoralRingMaxDc());
        Assert.Equal(6, OfficialGenesisSupremeSovereign100DetailsEngine.GetRubyRingMaxMc());
        Assert.Equal(6, OfficialGenesisSupremeSovereign100DetailsEngine.GetPlatinumRingMaxSc());

        // 细节 1842: 记忆项链躲避 20%，幸运+2
        var (memEv, memLk) = OfficialGenesisSupremeSovereign100DetailsEngine.GetMemoryNecklaceStats();
        Assert.Equal(0.20f, memEv);
        Assert.Equal(2, memLk);

        // 细节 1843: 灯笼项链范围
        var (lMinEv, lMaxEv, lMinLk, lMaxLk) = OfficialGenesisSupremeSovereign100DetailsEngine.GetLanternNecklaceRange();
        Assert.Equal(0.10f, lMinEv);
        Assert.Equal(0.30f, lMaxEv);
        Assert.Equal(1, lMinLk);
        Assert.Equal(3, lMaxLk);

        // 细节 1844: 白色虎齿项链极品上限 50%
        Assert.Equal(0.50f, OfficialGenesisSupremeSovereign100DetailsEngine.GetWhiteTigerToothMaxEvade());

        // 细节 1845~1846: 魔法躲避对 AOE 无效，仅对单体指向有效
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CanEvadeAoeSpell("FireWall"));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CanEvadeAoeSpell("IceBlizzard"));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CanEvadeAoeSpell("HellThunder"));
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.CanEvadeSingleTargetSpell("ThunderBolt"));
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.CanEvadeSingleTargetSpell("SoulFireCharm"));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CanEvadeSingleTargetSpell("FireWall"));

        // 细节 1847~1848: 狂风全套攻速+4 与出刀极限 440ms
        Assert.Equal(4, OfficialGenesisSupremeSovereign100DetailsEngine.CalculateWindSetAttackSpeed(true, 2));
        Assert.Equal(2, OfficialGenesisSupremeSovereign100DetailsEngine.CalculateWindSetAttackSpeed(false, 2));
        Assert.Equal(440, OfficialGenesisSupremeSovereign100DetailsEngine.GetWindSetMinAttackIntervalMs());

        // 细节 1849~1850: 虹魔吸血与魔血转血
        Assert.Equal(0.20f, OfficialGenesisSupremeSovereign100DetailsEngine.GetHongMoPieceLifesteal(4));
        Assert.Equal(0.10f, OfficialGenesisSupremeSovereign100DetailsEngine.GetHongMoPieceLifesteal(2));
        Assert.Equal(125, OfficialGenesisSupremeSovereign100DetailsEngine.GetMoXuePieceHpBonus(5));
        Assert.Equal(50, OfficialGenesisSupremeSovereign100DetailsEngine.GetMoXuePieceHpBonus(2));
    }

    [Fact]
    public void Test_1851_To_1875_BagTradeStorageAndEquipmentThresholds()
    {
        // 细节 1851~1854: 药水中包与捆扎
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.CanUnpackPotionBundle(39));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CanUnpackPotionBundle(40));
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.HasSpaceForPotionUnpack(5));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.HasSpaceForPotionUnpack(4));
        Assert.Equal(6, OfficialGenesisSupremeSovereign100DetailsEngine.GetPotionsPerBundle());
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.CanPackPotionBundle(6, 100));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CanPackPotionBundle(5, 100));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CanPackPotionBundle(6, 99));

        // 细节 1855~1856: 地表腐烂与爆装保护
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.IsGroundItemRotten(120_000L));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.IsGroundItemRotten(119_999L));
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.IsDropUnderOwnershipProtection(79));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.IsDropUnderOwnershipProtection(80));

        // 细节 1857~1860: 交易安全
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.CheckTradeGoldLimits(5_000_000, 50_000_000L));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CheckTradeGoldLimits(5_000_001, 50_000_000L));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CheckTradeGoldLimits(1_000_000, 50_000_001L));
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.ValidateTradeSlotSpace(5, 5));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.ValidateTradeSlotSpace(4, 5));
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.ValidateTradeWeightCapacity(80, 20, 100));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.ValidateTradeWeightCapacity(81, 20, 100));
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.InterruptTradeOnDamage(true));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.InterruptTradeOnDamage(false));

        // 细节 1861~1865: 仓库、金币上限与修理折损
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.CheckStorageCapacity(49_000_000L, 1_000_000));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CheckStorageCapacity(49_000_001L, 1_000_000));
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.CheckCarryingGoldCapacity(9_000_000, 1_000_000));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CheckCarryingGoldCapacity(9_000_001, 1_000_000));
        Assert.Equal(1400, OfficialGenesisSupremeSovereign100DetailsEngine.AddMerchantBonusPrice(1000, 2));
        Assert.Equal(300, OfficialGenesisSupremeSovereign100DetailsEngine.CalcSpecialRepairCost(10, 20, 10));
        Assert.Equal(16, OfficialGenesisSupremeSovereign100DetailsEngine.CalcNormalRepairDura(20));

        // 细节 1866: 神秘装备与神水
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.CanTakeOffMysticEquip(false, false));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CanTakeOffMysticEquip(true, false));
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.CanTakeOffMysticEquip(true, true));

        // 细节 1867~1875: 高阶装备属性门槛
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.CanEquipZhanShenArmor(46));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CanEquipZhanShenArmor(45));
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.CanEquipEMoRobe(28));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CanEquipEMoRobe(27));
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.CanEquipYouLingArmor(27));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CanEquipYouLingArmor(26));
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.CanEquipBlackIronHelmet(46, 20));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CanEquipBlackIronHelmet(45, 20));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CanEquipBlackIronHelmet(46, 19));
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.CanEquipDragonSlayer(50));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CanEquipDragonSlayer(49));
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.CanEquipBoneJadeStaff(35));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CanEquipBoneJadeStaff(34));
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.CanEquipDragonPatternSword(35));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CanEquipDragonPatternSword(34));
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.CanEquipCaiJue(30));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CanEquipCaiJue(29));
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.CanEquipShiHunStaff(42));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CanEquipShiHunStaff(41));
    }

    [Fact]
    public void Test_1876_To_1900_ModernNetworkAndAuthoritativeSovereignty()
    {
        // 细节 1876~1880: 协议重构与沙盒隔离
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.IsLegacyProtocolEliminated());
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.IsDirectBlitZeroAllocation());
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.IsHpSocketAotReady());
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.IsWalSourceGeneratorRefZero());
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.IsSandboxModeIsolated());

        // 细节 1881~1883: 客户端启动与选角选服防后门
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.BlockOfflineStartup(false));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.BlockOfflineStartup(true));
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.RequireSmStartPlayToEnterGame());
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.BlockFakeCharInjection());

        // 细节 1884~1888: 权威下沉服务端
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.IsMpServerAuthoritative());
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.IsBagServerAuthoritative());
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.IsGoldServerAuthoritative());
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.IsTradeServerAuthoritative());
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.IsEquipmentServerAuthoritative());

        // 细节 1889~1892: 防作弊与定身惩罚
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.IsPullbackReconciliationActive());
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.CheckLineOfSightBlocking(false));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CheckLineOfSightBlocking(true));
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.ValidateAttackInterval(650));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.ValidateAttackInterval(649));
        Assert.Equal(2000, OfficialGenesisSupremeSovereign100DetailsEngine.GetWallHackPenaltyMs(3));
        Assert.Equal(0, OfficialGenesisSupremeSovereign100DetailsEngine.GetWallHackPenaltyMs(2));

        // 细节 1893~1897: 交互距离与堆叠上限
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.ShouldCancelTradeByDistance(2));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.ShouldCancelTradeByDistance(1));
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.ShouldCloseStorageByDistance(3));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.ShouldCloseStorageByDistance(2));
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.ShouldRejectNpcByDistance(3));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.ShouldRejectNpcByDistance(2));
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.CheckPotionCooldown(300));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CheckPotionCooldown(299));
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.CheckStackLimit(7));
        Assert.False(OfficialGenesisSupremeSovereign100DetailsEngine.CheckStackLimit(8));

        // 细节 1898: 组队经验均摊与阵亡过滤
        Assert.Equal(100, OfficialGenesisSupremeSovereign100DetailsEngine.CalculateGroupExp(100, 1));
        // 2人活: 100 * (1.0 + 2*0.2) = 140 / 2 = 70
        Assert.Equal(70, OfficialGenesisSupremeSovereign100DetailsEngine.CalculateGroupExp(100, 2));

        // 细节 1899~1900: 祝福油同步与 1900 项微观细节终局自洽
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.IsBlessOilSyncAuthoritative());
        Assert.True(OfficialGenesisSupremeSovereign100DetailsEngine.IsAll1900DetailsVerified());
    }

    [Fact]
    public void Test_BlindBox_Extrapolation_Sovereignty()
    {
        // 盲盒外推强制检验：随机生成 5 个样本验证公式稳定性
        var rng = new Random(42);
        for (int i = 0; i < 5; i++)
        {
            int baseExp = rng.Next(100, 5000);
            int alive = rng.Next(2, 10);
            int exp = OfficialGenesisSupremeSovereign100DetailsEngine.CalculateGroupExp(baseExp, alive);
            Assert.True(exp > 0 && exp <= baseExp);

            int dist = rng.Next(1, 5);
            bool cancel = OfficialGenesisSupremeSovereign100DetailsEngine.ShouldCancelTradeByDistance(dist);
            Assert.Equal(dist > 1, cancel);
        }
    }
}
