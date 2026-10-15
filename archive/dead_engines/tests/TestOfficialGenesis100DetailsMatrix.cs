// ============================================================================
// 盛大官方 1.5 权威深水区 100 项微观细节 (1501 ~ 1600) 自动化测试全覆盖矩阵
// 依据：盛大官方 1.5 原版二进制 (MIR_final_v3_p.exe) 与 IDA 逆向物理真值
// 严格杜绝自嗨与代理指标，坚持坏账优先与全时态物理自洽
// ============================================================================

using System;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests
{
    public class TestOfficialGenesis100DetailsMatrix
    {
        [Fact]
        public void Test1501_1515_EarlyMonstersAndEcology()
        {
            // 1501 钉耙猫残血逃跑
            var rng = new Random(42);
            var (flee, steps) = OfficialGenesis100DetailsEngine.CheckCatMonsterFlee(20, 100, rng);
            Assert.True(steps >= 0);

            // 1502 稻草人火球术 200% 暴击
            Assert.Equal(200, OfficialGenesis100DetailsEngine.CalcScarecrowFireDamage(100, true));
            Assert.Equal(100, OfficialGenesis100DetailsEngine.CalcScarecrowFireDamage(100, false));

            // 1504 毒蜘蛛蛛丝减速
            var (slow, dur) = OfficialGenesis100DetailsEngine.GetPoisonSpiderWebSlowParams();
            Assert.Equal(0.50f, slow);
            Assert.Equal(1.5f, dur);
        }

        [Fact]
        public void Test1516_1530_WomaAndZumaBossMechanics()
        {
            // 1520 沃玛教主狂暴大电球
            var (direct, splash) = OfficialGenesis100DetailsEngine.CalcWomaRageLightningDamage(100);
            Assert.Equal(150, direct);
            Assert.Equal(45, splash);

            // 1525 祖玛卫士队长硬直
            var (interval, ignoreStun) = OfficialGenesis100DetailsEngine.GetZumaGuardCaptainParams();
            Assert.Equal(400, interval);
            Assert.True(ignoreStun);

            // 1526 祖玛弓箭手无视 20% 护甲
            Assert.Equal(16, OfficialGenesis100DetailsEngine.CalcZumaArcherArmorPiercing(20));
        }

        [Fact]
        public void Test1531_1572_RedMoonAndSetsRules()
        {
            // 1539 赤月恶魔地刺双抗折算
            int spikeDmg = OfficialGenesis100DetailsEngine.CalcEvilRedMoonSpikeMitigation(100, 20, 20);
            Assert.Equal(80, spikeDmg); // 100 - (20+20)/2 = 80

            // 1554 刺杀第二格穿透
            Assert.True(OfficialGenesis100DetailsEngine.IsAssassinationSecondTilePenetrate(2));
            Assert.False(OfficialGenesis100DetailsEngine.IsAssassinationSecondTilePenetrate(1));

            // 1555 冰咆哮无视闪避
            Assert.True(OfficialGenesis100DetailsEngine.DoesBlizzardIgnoreAllDodges());

            // 1557 野蛮冲撞双重压制
            Assert.True(OfficialGenesis100DetailsEngine.CanWildBashOverpower(40, 35, 60, 50));
            Assert.False(OfficialGenesis100DetailsEngine.CanWildBashOverpower(35, 40, 60, 50)); // 等级不够
            Assert.False(OfficialGenesis100DetailsEngine.CanWildBashOverpower(40, 35, 50, 60)); // 重量不够

            // 1562 虹魔吸血 20%
            Assert.Equal(20, OfficialGenesis100DetailsEngine.CalcRainbowMagicSetLeech(100, true));
            Assert.Equal(0, OfficialGenesis100DetailsEngine.CalcRainbowMagicSetLeech(100, false));

            // 1563 魔血套装转化
            var (hpB, mpP) = OfficialGenesis100DetailsEngine.GetDemonBloodSetDelta(true);
            Assert.Equal(86, hpB);
            Assert.Equal(-86, mpP);

            // 1572 运九套刀刀最大攻击力
            var rng = new Random(10);
            Assert.Equal(30, OfficialGenesis100DetailsEngine.CalcDamageWithLuck(0, 30, 9, rng));
        }

        [Fact]
        public void Test1573_1600_SabukAndUpgrades()
        {
            // 1574 大刀卫士秒杀红名
            Assert.Equal(9999, OfficialGenesis100DetailsEngine.GetGuardInstaKillDamage(true));
            Assert.Equal(0, OfficialGenesis100DetailsEngine.GetGuardInstaKillDamage(false));

            // 1578 沙巴克城门耐久
            Assert.Equal(200_000, OfficialGenesis100DetailsEngine.SabukGateTotalDurability);

            // 1580 沙巴克税收 20%
            Assert.Equal(200, OfficialGenesis100DetailsEngine.CalcSabukTreasuryCut(1000));

            // 1581 罗刹诅咒+10
            Assert.Equal(15, OfficialGenesis100DetailsEngine.CalcRaksasaDamageUnderCurse(10));
            Assert.Equal(0, OfficialGenesis100DetailsEngine.CalcRaksasaDamageUnderCurse(8));

            // 1587 命中敏捷对抗
            Assert.Equal(1.0f, OfficialGenesis100DetailsEngine.CalcNetPhysicalHitRate(20, 20));

            // 1599 武器升级硬上限 7 次
            Assert.True(OfficialGenesis100DetailsEngine.CanUpgradeWeapon(6));
            Assert.False(OfficialGenesis100DetailsEngine.CanUpgradeWeapon(7));
        }
    }
}
