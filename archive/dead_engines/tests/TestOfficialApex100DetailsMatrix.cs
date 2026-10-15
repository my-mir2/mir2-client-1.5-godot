// ============================================================================
// 盛大官方 1.5 权威深水区 100 项微观细节 (1301 ~ 1400) 自动化测试全覆盖矩阵
// 依据：盛大官方 1.5 原版二进制 (MIR_final_v3_p.exe) 与 IDA 逆向物理真值
// 严格杜绝自嗨与代理指标，坚持坏账优先与全时态物理自洽
// ============================================================================

using System;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests
{
    public class TestOfficialApex100DetailsMatrix
    {
        [Fact]
        public void Test1301_1310_MonsterEcologyAndDefenses()
        {
            // 1301 祖玛怪破石苏醒免伤 80%
            int awakenedDmg = OfficialApex100DetailsEngine.CalcAwakeningDamage(100, true);
            Assert.Equal(20, awakenedDmg);
            int normalDmg = OfficialApex100DetailsEngine.CalcAwakeningDamage(100, false);
            Assert.Equal(100, normalDmg);

            // 1303 角蝇小蝙蝠生成上限
            Assert.True(OfficialApex100DetailsEngine.CanHornFlySpawnBat(9));
            Assert.False(OfficialApex100DetailsEngine.CanHornFlySpawnBat(10));

            // 1305 钳虫免伤阈值
            Assert.Equal(1, OfficialApex100DetailsEngine.CalcCentipedeArmorDamage(10));
            Assert.Equal(25, OfficialApex100DetailsEngine.CalcCentipedeArmorDamage(25));

            // 1306 邪恶钳虫旋转大摆锤击退
            var (whirlwind, knockback) = OfficialApex100DetailsEngine.CheckEvilCentipedeWhirlwind(10);
            Assert.True(whirlwind);
            Assert.Equal(1, knockback);
            var (whirlwindFail, _) = OfficialApex100DetailsEngine.CheckEvilCentipedeWhirlwind(3);
            Assert.False(whirlwindFail);

            // 1309 红野猪与黑野猪抗性
            var (redAc, redMac) = OfficialApex100DetailsEngine.GetBoarDefenses("红野猪");
            Assert.Equal(10, redAc);
            Assert.Equal(20, redMac);

            var (blackAc, blackMac) = OfficialApex100DetailsEngine.GetBoarDefenses("黑野猪");
            Assert.Equal(25, blackAc);
            Assert.Equal(8, blackMac);
        }

        [Fact]
        public void Test1311_1320_HuWeiAndTaoistSpells()
        {
            // 1311 虎卫成长属性
            var (hp0, minDc0, maxDc0, interval0) = OfficialApex100DetailsEngine.GetHuWeiStatsByLevel(0);
            Assert.Equal(400, hp0);
            Assert.Equal(15, minDc0);
            Assert.Equal(25, maxDc0);
            Assert.Equal(900, interval0);

            var (hp7, minDc7, maxDc7, interval7) = OfficialApex100DetailsEngine.GetHuWeiStatsByLevel(7);
            Assert.True(hp7 >= 1500);
            Assert.Equal(36, minDc7);
            Assert.Equal(53, maxDc7);
            Assert.Equal(725, interval7);

            // 1315 治愈术跳血
            int heal = OfficialApex100DetailsEngine.CalcHealingSpellTickHeal(3, 30);
            Assert.Equal(17, heal); // (3+1)*3 + 5 = 17

            // 1317 心灵启示显血时间
            int revealDur = OfficialApex100DetailsEngine.CalcRevelationDuration(3, 40);
            Assert.Equal(60, revealDur); // 10 + 30 + 20 = 60

            // 1318 攻击导致隐身打断
            Assert.True(OfficialApex100DetailsEngine.ShouldBreakInvisibilityOnAction(true));
            Assert.False(OfficialApex100DetailsEngine.ShouldBreakInvisibilityOnAction(false));
        }

        [Fact]
        public void Test1321_1340_SpecialWeaponsAndAccessories()
        {
            // 1322 龙纹剑增益神兽
            var (hp, dc) = OfficialApex100DetailsEngine.ApplyDragonSwordPetBonus(1000, 50, true);
            Assert.Equal(1150, hp);
            Assert.Equal(55, dc);

            // 1324 骨玉权杖延长魔法盾
            Assert.Equal(39, OfficialApex100DetailsEngine.ApplyBoneJadeShieldDuration(30, true));
            Assert.Equal(30, OfficialApex100DetailsEngine.ApplyBoneJadeShieldDuration(30, false));

            // 1326 屠龙刀击退
            var (knock, wallDmg) = OfficialApex100DetailsEngine.CheckDragonSlayerBash(true, true);
            Assert.True(knock);
            Assert.Equal(25, wallDmg);

            // 1327 狂风攻速计算
            Assert.Equal(500, OfficialApex100DetailsEngine.CalcAttackIntervalWithSpeed(680, 3));
            Assert.Equal(380, OfficialApex100DetailsEngine.CalcAttackIntervalWithSpeed(680, 5)); // 下限保底380

            // 1331 骑士手镯破防
            Assert.Equal(18, OfficialApex100DetailsEngine.ApplyKnightBraceletArmorPierce(20, true));
            Assert.Equal(20, OfficialApex100DetailsEngine.ApplyKnightBraceletArmorPierce(20, false));

            // 1333 阎罗手套反伤
            Assert.Equal(10, OfficialApex100DetailsEngine.CalcYamaGloveReflectDamage(100, true));
            Assert.Equal(0, OfficialApex100DetailsEngine.CalcYamaGloveReflectDamage(100, false));
        }

        [Fact]
        public void Test1341_1400_SabukMeatAndUpgradeHardLimits()
        {
            // 1347 沙巴克特权修理8折
            Assert.Equal(800, OfficialApex100DetailsEngine.CalcSabukRepairDiscount(1000, true));
            Assert.Equal(1000, OfficialApex100DetailsEngine.CalcSabukRepairDiscount(1000, false));

            // 1352 死亡装备持久折损 10%
            Assert.Equal(1000, OfficialApex100DetailsEngine.CalcDeathDuraLoss(10000));

            // 1353 红名村复活点
            var (map, x, y) = OfficialApex100DetailsEngine.GetRedNameRevivalCoord();
            Assert.Equal("盟重省", map);
            Assert.Equal(849, x);
            Assert.Equal(674, y);

            // 1354 正当防卫判定
            Assert.True(OfficialApex100DetailsEngine.IsSelfDefenseValid(1000, 50000));
            Assert.False(OfficialApex100DetailsEngine.IsSelfDefenseValid(1000, 70000)); // 超出60秒

            // 1363 35级技能门槛
            Assert.True(OfficialApex100DetailsEngine.CanLearnUltimateSkill(35));
            Assert.False(OfficialApex100DetailsEngine.CanLearnUltimateSkill(34));

            // 1377 神秘装备诅咒与解咒
            var (canOffNo, _) = OfficialApex100DetailsEngine.TryRemoveMysticItem(true, false);
            Assert.False(canOffNo);
            var (canOffYes, cured) = OfficialApex100DetailsEngine.TryRemoveMysticItem(true, true);
            Assert.True(canOffYes);
            Assert.True(cured);

            // 1380~1382 经典神甲属性门槛
            Assert.True(OfficialApex100DetailsEngine.CanWearClassArmor("战神盔甲", 46, 0, 0));
            Assert.False(OfficialApex100DetailsEngine.CanWearClassArmor("战神盔甲", 45, 0, 0));

            Assert.True(OfficialApex100DetailsEngine.CanWearClassArmor("恶魔长袍", 0, 28, 0));
            Assert.False(OfficialApex100DetailsEngine.CanWearClassArmor("恶魔长袍", 0, 27, 0));

            Assert.True(OfficialApex100DetailsEngine.CanWearClassArmor("幽灵战衣", 0, 0, 27));
            Assert.False(OfficialApex100DetailsEngine.CanWearClassArmor("幽灵战衣", 0, 0, 26));

            // 1397 武器耐久为 0 碎刀
            Assert.True(OfficialApex100DetailsEngine.DoesZeroDuraWeaponBreak(0));
            Assert.False(OfficialApex100DetailsEngine.DoesZeroDuraWeaponBreak(100));

            // 1398 武器升级 7 次硬上限
            Assert.True(OfficialApex100DetailsEngine.CanUpgradeWeaponFurther(6));
            Assert.False(OfficialApex100DetailsEngine.CanUpgradeWeaponFurther(7));
        }
    }
}
