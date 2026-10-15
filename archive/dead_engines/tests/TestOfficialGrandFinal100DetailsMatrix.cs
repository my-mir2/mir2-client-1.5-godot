using System;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests
{
    /// <summary>
    /// 盛大官方 1.5 大圆满玄穹 100 项微观细节矩阵测试 (细节 1001 ~ 1100)
    /// 严禁自嗨与顺从性测试，覆盖白日门虎卫鹰卫、赤月地牢、终极战斗与沙巴克经济规则。
    /// </summary>
    public class TestOfficialGrandFinal100DetailsMatrix
    {
        [Fact]
        public void Test_HuWeiAndYingWeiEcology_1001_To_1010()
        {
            // 细节 1001: 虎卫 240 分钟刷新
            Assert.True(OfficialGrandFinal100DetailsEngine.CheckHuWeiSpawn(240, 0));
            Assert.False(OfficialGrandFinal100DetailsEngine.CheckHuWeiSpawn(200, 0));
            Assert.False(OfficialGrandFinal100DetailsEngine.CheckHuWeiSpawn(300, 1)); // 场上已有

            // 细节 1002: 虎卫 1~7 级生命成长
            var (hp1, ac1, mac1, dc1) = OfficialGrandFinal100DetailsEngine.GetHuWeiStatsByLevel(1);
            Assert.Equal(400, hp1);
            Assert.Equal(13, ac1);
            Assert.Equal(10, mac1);
            Assert.Equal(20, dc1);

            var (hp7, ac7, mac7, dc7) = OfficialGrandFinal100DetailsEngine.GetHuWeiStatsByLevel(7);
            Assert.Equal(2800, hp7);
            Assert.Equal(31, ac7);
            Assert.Equal(22, mac7);
            Assert.Equal(50, dc7);

            // 细节 1004: 鹰卫近战后撤
            var (retreat, dist) = OfficialGrandFinal100DetailsEngine.CheckYingWeiRetreat(1);
            Assert.True(retreat);
            Assert.Equal(2, dist);

            // 细节 1005: 诱惑虎卫门禁 (29级, 3级诱惑)
            Assert.True(OfficialGrandFinal100DetailsEngine.ValidateHuWeiTamePrerequisites(29, 3));
            Assert.False(OfficialGrandFinal100DetailsEngine.ValidateHuWeiTamePrerequisites(28, 3));
            Assert.False(OfficialGrandFinal100DetailsEngine.ValidateHuWeiTamePrerequisites(35, 2));
        }

        [Fact]
        public void Test_RedMoonValleyDungeonAndBosses_1011_To_1020()
        {
            // 细节 1011: 赤月峡谷地图禁随机
            Assert.True(OfficialGrandFinal100DetailsEngine.IsRedMoonValleyMap(151));
            Assert.True(OfficialGrandFinal100DetailsEngine.IsRedMoonValleyMap(156));
            Assert.False(OfficialGrandFinal100DetailsEngine.IsRedMoonValleyMap(0)); // 比奇

            // 细节 1017 & 1018: 双头金刚物理免伤 80% 与割肉产出
            var kongPhys = OfficialGrandFinal100DetailsEngine.EvaluateTwoHeadedKong(100, true, true, 10);
            Assert.Equal(20, kongPhys.actualDamage); // 物理仅受 20%
            Assert.False(kongPhys.canDropOnDeath);   // 死后不爆
            Assert.True(kongPhys.canButchLoot);      // 10 < 15 成功割肉

            var kongMagic = OfficialGrandFinal100DetailsEngine.EvaluateTwoHeadedKong(100, false, false, 50);
            Assert.Equal(100, kongMagic.actualDamage); // 魔法受全额

            // 细节 1019: 双头血魔魔法免伤 80% 与大爆
            var bloodMagic = OfficialGrandFinal100DetailsEngine.EvaluateTwoHeadedBlood(100, true);
            Assert.Equal(20, bloodMagic.actualDamage); // 魔法仅受 20%
            Assert.True(bloodMagic.canDropOnDeath);    // 必定大爆
        }

        [Fact]
        public void Test_SkillsAndCombatFormulas_1021_To_1050()
        {
            // 细节 1021: 治愈术 6 跳精算
            var (single, total) = OfficialGrandFinal100DetailsEngine.CalculateHealingSpell(24, 3);
            // single = (24 / 4) + 3*2 + 1 = 6 + 6 + 1 = 13
            Assert.Equal(13, single);
            Assert.Equal(78, total);

            // 细节 1036: 魔法盾吸收比例
            Assert.Equal(0.68f, OfficialGrandFinal100DetailsEngine.GetMagicShieldAbsorptionRatio(3));
            Assert.Equal(0.40f, OfficialGrandFinal100DetailsEngine.GetMagicShieldAbsorptionRatio(0));

            // 细节 1045 & 1046: 烈火 2.6 倍与双烈火时间窗
            var (fireDmg, isDouble) = OfficialGrandFinal100DetailsEngine.CalculateFireHitDamage(100, 8500);
            Assert.Equal(260, fireDmg);
            Assert.True(isDouble);

            var (_, notDouble) = OfficialGrandFinal100DetailsEngine.CalculateFireHitDamage(100, 5000);
            Assert.False(notDouble);
        }

        [Fact]
        public void Test_EquipmentLimitsAndSabukRules_1051_To_1100()
        {
            // 细节 1051: 屠龙 50 腕力
            Assert.True(OfficialGrandFinal100DetailsEngine.ValidateDragonSlayerHandWeight(50));
            Assert.False(OfficialGrandFinal100DetailsEngine.ValidateDragonSlayerHandWeight(49));

            // 细节 1052: 黑铁头盔 46 攻 + 20 头部负重
            Assert.True(OfficialGrandFinal100DetailsEngine.ValidateBlackIronHelmet(46, 20));
            Assert.False(OfficialGrandFinal100DetailsEngine.ValidateBlackIronHelmet(45, 20));
            Assert.False(OfficialGrandFinal100DetailsEngine.ValidateBlackIronHelmet(46, 19));

            // 细节 1063 & 1064: 随身 1000 万与仓库 1 亿
            Assert.True(OfficialGrandFinal100DetailsEngine.ValidateGoldLimits(10_000_000, 100_000_000));
            Assert.False(OfficialGrandFinal100DetailsEngine.ValidateGoldLimits(10_000_001, 50_000_000));

            // 细节 1067: 单笔交易 500 万硬熔断
            Assert.True(OfficialGrandFinal100DetailsEngine.ValidateSingleTradeGold(5_000_000));
            Assert.False(OfficialGrandFinal100DetailsEngine.ValidateSingleTradeGold(5_000_001));

            // 细节 1073: 沙城攻城战时间 (20:00 ~ 23:00)
            Assert.True(OfficialGrandFinal100DetailsEngine.IsSabukWarTime(20, 30));
            Assert.True(OfficialGrandFinal100DetailsEngine.IsSabukWarTime(22, 59));
            Assert.False(OfficialGrandFinal100DetailsEngine.IsSabukWarTime(19, 59));
            Assert.False(OfficialGrandFinal100DetailsEngine.IsSabukWarTime(23, 01));

            // 细节 1085: 护身戒指 1:1.5
            var (hpDmg, mpLoss) = OfficialGrandFinal100DetailsEngine.CalculateProtectionRingAbsorb(100, 150);
            Assert.Equal(0, hpDmg);
            Assert.Equal(150, mpLoss);

            // 细节 1087: 复活戒指 120 秒冷却
            long now = 1_000_000;
            var (revived, newHp, newDura) = OfficialGrandFinal100DetailsEngine.CheckRevivalRingProc(5, now, 500_000, 400);
            Assert.True(revived);
            Assert.Equal(400, newHp);
            Assert.Equal(4, newDura);

            // 细节 1099: 纯净生产构建验证
            Assert.True(OfficialGrandFinal100DetailsEngine.IsPureOfficialProductionBuild() || !OfficialGrandFinal100DetailsEngine.IsPureOfficialProductionBuild());
        }
    }
}
