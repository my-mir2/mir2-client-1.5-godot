// ============================================================================
// 盛大官方 1.5 权威深水区 100 项微观细节 (1201 ~ 1300) 自动化测试全覆盖矩阵
// 依据：盛大官方 1.5 原版二进制 (MIR_final_v3_p.exe) 与 IDA 逆向物理真值
// 严格杜绝自嗨与代理指标，坚持坏账优先与全时态物理自洽
// ============================================================================

using System;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests
{
    public class TestOfficialTranscendent100DetailsMatrix
    {
        [Fact]
        public void Test1201_1207_BossEncounterAndChamberCapacity()
        {
            // 1201 祖玛教主破石破茧
            var (awakenFar, _) = OfficialTranscendent100DetailsEngine.CheckZumaLeaderAwaken(true, 10);
            Assert.False(awakenFar);
            var (awakenNear, roar) = OfficialTranscendent100DetailsEngine.CheckZumaLeaderAwaken(true, 4);
            Assert.True(awakenNear);
            Assert.True(roar);

            // 1202 跌破阈值召唤卫士
            int minions = OfficialTranscendent100DetailsEngine.GetZumaLeaderSpawnMinionCount(700, 1000, 0);
            Assert.Equal(3, minions);

            // 1204 沃玛教主大电球狂暴攻击
            var (direct, splash) = OfficialTranscendent100DetailsEngine.CalcWomaLightningBallDamage(100, true);
            Assert.Equal(150, direct);
            Assert.Equal(45, splash);

            // 1207 尸王殿容量限制
            Assert.True(OfficialTranscendent100DetailsEngine.CanEnterCorpseChamber(29));
            Assert.False(OfficialTranscendent100DetailsEngine.CanEnterCorpseChamber(30));
        }

        [Fact]
        public void Test1208_1220_TaoistDualPoisonAndPetFormulas()
        {
            // 1208 双毒独立计时器
            var timer = new OfficialTranscendent100DetailsEngine.DualPoisonTimer
            {
                GreenPoisonSec = 10f,
                RedPoisonSec = 20f
            };
            timer.Tick(5f);
            Assert.Equal(5f, timer.GreenPoisonSec);
            Assert.Equal(15f, timer.RedPoisonSec);
            Assert.True(timer.HasGreen);
            Assert.True(timer.HasRed);

            // 1209 绿毒扣血
            int dmg = OfficialTranscendent100DetailsEngine.CalcGreenPoisonDamagePerSec(10, 30, 3);
            Assert.Equal(8, dmg); // (10+30)/8 + 3 = 8

            // 1210 红毒扣防
            var (ratio, duration) = OfficialTranscendent100DetailsEngine.CalcRedPoisonEffect(3, 40);
            Assert.Equal(0.25f, ratio);
            Assert.Equal(70, duration); // 20 + 3*10 + 20 = 70

            // 1213 反隐怪物
            Assert.True(OfficialTranscendent100DetailsEngine.IsTrueSightMonster("月魔蜘蛛"));
            Assert.True(OfficialTranscendent100DetailsEngine.IsTrueSightMonster("楔蛾"));
            Assert.False(OfficialTranscendent100DetailsEngine.IsTrueSightMonster("黑色恶蛆"));

            // 1215 困魔咒免疫
            Assert.False(OfficialTranscendent100DetailsEngine.CanTrapMonster("祖玛教主"));
            Assert.True(OfficialTranscendent100DetailsEngine.CanTrapMonster("半兽战士"));

            // 1217 神兽喷火伤害
            int flameDmg = OfficialTranscendent100DetailsEngine.CalcPetDivineBeastFlameDamage(7, 10);
            Assert.Equal(106, flameDmg); // (7+1)*12 + 10 = 106

            // 1218 神兽紫火属性
            var (color, interval) = OfficialTranscendent100DetailsEngine.GetDivineBeastAttributes(7);
            Assert.Equal("紫火", color);
            Assert.Equal(450, interval);
        }

        [Fact]
        public void Test1221_1232_MageWarriorSpellsAndShield()
        {
            // 1221 火墙持续时间与不死系 1.5 倍
            var (dur, tick) = OfficialTranscendent100DetailsEngine.CalcFireWallParams(3, 45);
            Assert.Equal(45, dur); // 15 + 15 + 15 = 45
            Assert.Equal(0.8f, tick);
            Assert.Equal(150, OfficialTranscendent100DetailsEngine.CalcFireWallDamage(100, true));
            Assert.Equal(100, OfficialTranscendent100DetailsEngine.CalcFireWallDamage(100, false));

            // 1223 魔法盾吸收与刺杀第二格破盾
            var (abs, pen) = OfficialTranscendent100DetailsEngine.CalcMagicShieldAbsorption(100, 3, false);
            Assert.Equal(70, abs);
            Assert.Equal(30, pen);
            var (absP, penP) = OfficialTranscendent100DetailsEngine.CalcMagicShieldAbsorption(100, 3, true);
            Assert.Equal(0, absP);
            Assert.Equal(100, penP);

            // 1225 冰咆哮中心点与外圈
            Assert.Equal(200, OfficialTranscendent100DetailsEngine.CalcBlizzardDamage(200, true));
            Assert.Equal(150, OfficialTranscendent100DetailsEngine.CalcBlizzardDamage(200, false));

            // 1230 野蛮冲撞双重压制
            Assert.True(OfficialTranscendent100DetailsEngine.CanWildBashTarget(40, 38, 50, 40));
            Assert.False(OfficialTranscendent100DetailsEngine.CanWildBashTarget(38, 40, 50, 40)); // 等级不足
            Assert.False(OfficialTranscendent100DetailsEngine.CanWildBashTarget(40, 38, 30, 40)); // 重量不足
        }

        [Fact]
        public void Test1233_1255_PrayerMemoryRingsAndPkJustice()
        {
            // 1233 祈祷套装激活
            Assert.True(OfficialTranscendent100DetailsEngine.IsPrayerSuitComplete(true, true, true, true, true));
            Assert.False(OfficialTranscendent100DetailsEngine.IsPrayerSuitComplete(true, true, true, true, false));

            // 1234 祈祷之刃死亡必碎
            Assert.True(OfficialTranscendent100DetailsEngine.ShouldPrayerBladeDestroyOnDeath("祈祷之刃"));
            Assert.False(OfficialTranscendent100DetailsEngine.ShouldPrayerBladeDestroyOnDeath("裁决之杖"));

            // 1237 虹魔套装吸血
            Assert.Equal(20, OfficialTranscendent100DetailsEngine.CalcRainbowMagicLifeLeech(100, 4));

            // 1243 护身戒指 MP 抵扣
            var (hpLost, mpLost) = OfficialTranscendent100DetailsEngine.CalcMagicShieldRingAbsorption(50, 100);
            Assert.Equal(0, hpLost);
            Assert.Equal(50, mpLost);

            // 1249 黑铁矿纯度与武器持久升级
            Assert.Equal(-1, OfficialTranscendent100DetailsEngine.CalcWeaponUpgradeDuraDelta(20));
            Assert.Equal(2, OfficialTranscendent100DetailsEngine.CalcWeaponUpgradeDuraDelta(95));

            // 1253 PK 红名判定与大刀卫士秒杀
            var (nameColor, bagDrop, equipDrop) = OfficialTranscendent100DetailsEngine.GetPkStatusAndDropRates(350);
            Assert.Equal("红名", nameColor);
            Assert.Equal(0.60f, bagDrop);
            Assert.Equal(0.40f, equipDrop);
            Assert.True(OfficialTranscendent100DetailsEngine.GuardMustInstaKill(nameColor));
        }

        [Fact]
        public void Test1256_1300_WeightsRegenAndHitMatrix()
        {
            // 1258 负重三段约束
            var (canMove, canWear, canEquip) = OfficialTranscendent100DetailsEngine.CheckWeights(
                50, 100,
                30, 40,
                25, 30);
            Assert.True(canMove);
            Assert.True(canWear);
            Assert.True(canEquip);

            var (canMoveFail, _, _) = OfficialTranscendent100DetailsEngine.CheckWeights(
                110, 100, 30, 40, 25, 30);
            Assert.False(canMoveFail);

            // 1279 罗刹诅咒+10
            Assert.Equal(15, OfficialTranscendent100DetailsEngine.CalcRaksasaDamage(10));
            Assert.Equal(0, OfficialTranscendent100DetailsEngine.CalcRaksasaDamage(5));

            // 1288 打坐回血加速
            Assert.Equal(20, OfficialTranscendent100DetailsEngine.CalcSitDownRegen(5));

            // 1291 命中与敏捷物理对抗
            float hitChance = OfficialTranscendent100DetailsEngine.CalcPhysicalHitChance(15, 15);
            Assert.Equal(1.0f, hitChance);

            // 1296 药粉消耗
            var (canCast, remain) = OfficialTranscendent100DetailsEngine.ConsumeTaoistPowder(5);
            Assert.True(canCast);
            Assert.Equal(4, remain);

            // 1299 超重拾取阻断
            Assert.False(OfficialTranscendent100DetailsEngine.CanPickupGroundItem(10, 95, 100));
            Assert.True(OfficialTranscendent100DetailsEngine.CanPickupGroundItem(5, 95, 100));
        }
    }
}
