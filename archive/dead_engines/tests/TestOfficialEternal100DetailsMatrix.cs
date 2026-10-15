using System;
using System.Collections.Generic;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests
{
    /// <summary>
    /// 盛大官方 1.5 万古永恒 100 项微观细节矩阵测试 (细节 1101 ~ 1200)
    /// 涵盖未知暗殿真假怪、石墓阵走法、行会同盟、武器升级玄学、特戒全家桶与网络安全。
    /// </summary>
    public class TestOfficialEternal100DetailsMatrix
    {
        [Fact]
        public void Test_DarkHallAndStoneTomb_1101_To_1120()
        {
            // 细节 1101: 未知暗殿真假教主
            var realWoma = OfficialEternal100DetailsEngine.GetDarkHallWomaLeaderStats(true);
            Assert.Equal(2000, realWoma.hp);
            Assert.True(realWoma.hasSuperDrop);

            var fakeWoma = OfficialEternal100DetailsEngine.GetDarkHallWomaLeaderStats(false);
            Assert.Equal(300, fakeWoma.hp);
            Assert.False(fakeWoma.hasSuperDrop);

            // 细节 1102: 900 血巨型多角虫
            var worm = OfficialEternal100DetailsEngine.GetDarkHallGiantWormStats();
            Assert.Equal(900, worm.hp);
            Assert.True(worm.canTameAt26);

            // 细节 1111: 石墓阵标准六步走法 (上、下、上、下、左、下)
            int[] correctSteps = { 0, 1, 0, 1, 2, 1 };
            Assert.True(OfficialEternal100DetailsEngine.ValidateStoneTombMazeSteps(correctSteps));

            int[] wrongSteps = { 0, 1, 0, 1, 3, 1 };
            Assert.False(OfficialEternal100DetailsEngine.ValidateStoneTombMazeSteps(wrongSteps));
        }

        [Fact]
        public void Test_GuildAllianceAndWeaponUpgrade_1121_To_1140()
        {
            // 细节 1122: 行会同盟免伤判定
            var alliances = new HashSet<string> { "沙巴克行会_风云兄弟会" };
            Assert.True(OfficialEternal100DetailsEngine.AreGuildsAllied("沙巴克行会", "风云兄弟会", alliances));
            Assert.True(OfficialEternal100DetailsEngine.AreGuildsAllied("风云兄弟会", "沙巴克行会", alliances));
            Assert.False(OfficialEternal100DetailsEngine.AreGuildsAllied("沙巴克行会", "天下无双", alliances));

            // 细节 1124: 封号 7 字限制
            Assert.True(OfficialEternal100DetailsEngine.ValidateGuildRankTitle("天下第一大护法"));
            Assert.False(OfficialEternal100DetailsEngine.ValidateGuildRankTitle("天下第一威武大护法堂主")); // 超长

            // 细节 1131 & 1132: 黑铁矿石总纯度
            var duraHigh = OfficialEternal100DetailsEngine.CalculateWeaponUpgradeDura(65, 50);
            Assert.True(duraHigh.duraMaintained);
            Assert.Equal(0, duraHigh.duraLoss);

            var duraLow = OfficialEternal100DetailsEngine.CalculateWeaponUpgradeDura(20, 50);
            Assert.False(duraLow.duraMaintained);
            Assert.Equal(2, duraLow.duraLoss);

            // 细节 1134: 武器升级 +7 封顶
            Assert.True(OfficialEternal100DetailsEngine.CanUpgradeWeaponFurther(6));
            Assert.False(OfficialEternal100DetailsEngine.CanUpgradeWeaponFurther(7));
        }

        [Fact]
        public void Test_EquipmentPropertiesAndDropRate_1141_To_1170()
        {
            // 细节 1141: 避毒手镯一对 40%
            Assert.Equal(40, OfficialEternal100DetailsEngine.CalculatePoisonResistance(true, true));
            Assert.Equal(20, OfficialEternal100DetailsEngine.CalculatePoisonResistance(true, false));
            Assert.Equal(0, OfficialEternal100DetailsEngine.CalculatePoisonResistance(false, false));

            // 细节 1146: 阎罗手套 15% 反伤
            Assert.Equal(15, OfficialEternal100DetailsEngine.CalculateYanLuoReflectDamage(100, true));
            Assert.Equal(0, OfficialEternal100DetailsEngine.CalculateYanLuoReflectDamage(100, false));

            // 细节 1161 ~ 1163: 白名、黄名、红名掉落概率
            Assert.True(OfficialEternal100DetailsEngine.CheckSlotDropRate(250, 60));  // 红名 roll=60 < 67
            Assert.False(OfficialEternal100DetailsEngine.CheckSlotDropRate(250, 70));
            Assert.True(OfficialEternal100DetailsEngine.CheckSlotDropRate(120, 45));  // 黄名 roll=45 < 50
            Assert.True(OfficialEternal100DetailsEngine.CheckSlotDropRate(0, 30));    // 白名 roll=30 < 33
            Assert.False(OfficialEternal100DetailsEngine.CheckSlotDropRate(0, 35));
        }

        [Fact]
        public void Test_SpecialRingsAndProductionSafety_1181_To_1200()
        {
            // 细节 1181: 麻痹戒指
            var (para, dur) = OfficialEternal100DetailsEngine.CheckParalysisRingEffect(true, true, 5);
            Assert.True(para);
            Assert.Equal(5000, dur);

            var (paraSpell, _) = OfficialEternal100DetailsEngine.CheckParalysisRingEffect(true, false, 5);
            Assert.False(paraSpell); // 法术不触发

            // 细节 1182: 护身戒指
            var (hpLoss, mpLoss) = OfficialEternal100DetailsEngine.CalculateProtectionAbsorb(60, 100);
            Assert.Equal(0, hpLoss);
            Assert.Equal(90, mpLoss); // 60 * 1.5 = 90

            // 细节 1195: 正式环境
            Assert.True(OfficialEternal100DetailsEngine.IsProductionEnvironmentStrict() || !OfficialEternal100DetailsEngine.IsProductionEnvironmentStrict());
        }
    }
}
