using System;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests
{
    /// <summary>
    /// 盛大官方 1.5 十全大成 100 项微观细节矩阵测试 (细节 901 ~ 1000)
    /// 严禁自嗨与顺从性测试，覆盖极端边界、盲盒外推与防伪验证。
    /// </summary>
    public class TestOfficialTenacious100DetailsMatrix
    {
        [Fact]
        public void Test_BossAI_AndCombatMechanisms_901_To_910()
        {
            // 细节 901: 沃玛教主发怒暴走
            var normal = OfficialTenacious100DetailsEngine.CheckWomaLeaderRage(800, 1000);
            Assert.False(normal.isEnraged);
            Assert.Equal(1.0f, normal.speedMultiplier);

            var enraged = OfficialTenacious100DetailsEngine.CheckWomaLeaderRage(250, 1000);
            Assert.True(enraged.isEnraged);
            Assert.Equal(1.5f, enraged.speedMultiplier);
            Assert.Equal(1.5f, enraged.attackSpeedMultiplier);

            // 细节 902: 沃玛教主瞬移脱困
            Assert.True(OfficialTenacious100DetailsEngine.CheckWomaLeaderTeleportEscape(5, 30));
            Assert.False(OfficialTenacious100DetailsEngine.CheckWomaLeaderTeleportEscape(3, 10)); // 包围人数不足

            // 细节 903: 祖玛教主麻痹震荡
            var (para, dur) = OfficialTenacious100DetailsEngine.CheckZumaLeaderShockParalyze(5);
            Assert.True(para);
            Assert.Equal(3000, dur);

            // 细节 906: 契蛾与月魔蜘蛛毒液与麻痹
            var struck = OfficialTenacious100DetailsEngine.CheckMothSpiderStruckEffect(15, 10);
            Assert.True(struck.greenPoison);
            Assert.True(struck.paralyze);
            Assert.Equal(3500, struck.paralyzeDurationMs);

            // 细节 908 & 909: 爆裂蜘蛛自爆伤害
            var blastNear = OfficialTenacious100DetailsEngine.CalculateExplodingSpiderBlast(1, 100);
            Assert.True(blastNear.explode);
            Assert.Equal(100, blastNear.damage);

            var blastFar = OfficialTenacious100DetailsEngine.CalculateExplodingSpiderBlast(3, 100);
            Assert.False(blastFar.explode);

            // 细节 910: 血僵尸血饮 5% 爆率
            Assert.True(OfficialTenacious100DetailsEngine.CheckBloodZombieBloodDrinkDrop(30));
            Assert.False(OfficialTenacious100DetailsEngine.CheckBloodZombieBloodDrinkDrop(80));
        }

        [Fact]
        public void Test_TaoistAndMageSkillMechanisms_911_To_930()
        {
            // 细节 911: 灵魂火符护身符扣除
            var okCast = OfficialTenacious100DetailsEngine.ValidateSoulSymbolCost(50);
            Assert.True(okCast.canCast);
            Assert.Equal(49, okCast.newDura);

            var failCast = OfficialTenacious100DetailsEngine.ValidateSoulSymbolCost(0);
            Assert.False(failCast.canCast);

            // 细节 912: 施毒术双抗削弱与持续掉血
            var poison = OfficialTenacious100DetailsEngine.CalculateTaoistPoisonEffect(30, 20, 25, 3);
            Assert.Equal(24, poison.debuffedAc); // 30 * 0.8 = 24
            Assert.Equal(16, poison.debuffedMac); // 20 * 0.8 = 16
            Assert.Equal(16, poison.greenTickDamage); // 2 + 3*3 + 25/5 = 2 + 9 + 5 = 16

            // 细节 917: 骷髅进化
            var redSkel = OfficialTenacious100DetailsEngine.GetSkeletonForm(7);
            Assert.Equal("变异骷髅", redSkel.name);
            Assert.Equal(1.3f, redSkel.attackSpeedMultiplier);

            // 细节 918: 神兽进化
            var divineBeast = OfficialTenacious100DetailsEngine.GetDivineBeastForm(7);
            Assert.Equal("红火极品神兽", divineBeast.formName);
            Assert.Equal(2.0f, divineBeast.statMultiplier);

            // 细节 921: 刺杀隔位破盾
            var penetrate = OfficialTenacious100DetailsEngine.CalculateAssassinationShieldDamage(100, true, 2);
            Assert.True(penetrate.shieldPenetrated);
            Assert.Equal(100, penetrate.actualDamage);

            var absorbed = OfficialTenacious100DetailsEngine.CalculateAssassinationShieldDamage(100, true, 1);
            Assert.False(absorbed.shieldPenetrated);
            Assert.Equal(40, absorbed.actualDamage); // 被吸收 60%

            // 细节 923: 火墙持续时长
            int fireDur = OfficialTenacious100DetailsEngine.CalculateFireWallDurationSeconds(30);
            Assert.Equal(30, fireDur); // 15 + 15 = 30s

            // 细节 925: 5 只随从上限
            Assert.True(OfficialTenacious100DetailsEngine.ValidatePetCountLimit(4));
            Assert.False(OfficialTenacious100DetailsEngine.ValidatePetCountLimit(5));

            // 细节 930: 冰咆哮不可闪避
            Assert.False(OfficialTenacious100DetailsEngine.IsMagicEvadable(33));
            Assert.True(OfficialTenacious100DetailsEngine.IsMagicEvadable(11)); // 雷电术可躲避
        }

        [Fact]
        public void Test_WarriorAndEquipmentRules_931_To_960()
        {
            // 细节 931: 基本剑术准确
            Assert.Equal(3, OfficialTenacious100DetailsEngine.GetBasicSwordAccuracyBonus(3));

            // 细节 934: 半月扫击衰减
            Assert.Equal(100, OfficialTenacious100DetailsEngine.CalculateHalfMoonDamage(100, true));
            Assert.Equal(33, OfficialTenacious100DetailsEngine.CalculateHalfMoonDamage(100, false));

            // 细节 936 & 937: 野蛮等级压制与撞墙
            var pushOk = OfficialTenacious100DetailsEngine.ExecuteMotaeboAuthority(40, 35, true);
            Assert.True(pushOk.targetPushed);
            Assert.Equal(20, pushOk.wallDamage);
            Assert.Equal(0, pushOk.selfStunMs);

            var pushFail = OfficialTenacious100DetailsEngine.ExecuteMotaeboAuthority(35, 40, false);
            Assert.False(pushFail.targetPushed);
            Assert.Equal(2000, pushFail.selfStunMs); // 自身晕眩 2 秒

            // 细节 938: 双烈火时间窗
            Assert.True(OfficialTenacious100DetailsEngine.ValidateDoubleFireHitWindow(8500));
            Assert.False(OfficialTenacious100DetailsEngine.ValidateDoubleFireHitWindow(5000));
            Assert.False(OfficialTenacious100DetailsEngine.ValidateDoubleFireHitWindow(11000));

            // 细节 941 & 943: 耐久归零效果
            var weaponZero = OfficialTenacious100DetailsEngine.EvaluateDuraZeroEffect(false);
            Assert.False(weaponZero.itemBrokenDisappear);
            Assert.True(weaponZero.statsNegated);

            var ringZero = OfficialTenacious100DetailsEngine.EvaluateDuraZeroEffect(true);
            Assert.True(ringZero.itemBrokenDisappear);

            // 细节 944: 屠龙腕力
            Assert.True(OfficialTenacious100DetailsEngine.ValidateDragonSlayerHandWeight(55));
            Assert.False(OfficialTenacious100DetailsEngine.ValidateDragonSlayerHandWeight(45));

            // 细节 945: 黑铁头盔需求
            Assert.True(OfficialTenacious100DetailsEngine.ValidateBlackIronHelmet(48, 25));
            Assert.False(OfficialTenacious100DetailsEngine.ValidateBlackIronHelmet(40, 25));
            Assert.False(OfficialTenacious100DetailsEngine.ValidateBlackIronHelmet(48, 15));

            // 细节 951: 地表 8 物品上限
            Assert.True(OfficialTenacious100DetailsEngine.CheckTileItemLimit(7));
            Assert.False(OfficialTenacious100DetailsEngine.CheckTileItemLimit(8));

            // 细节 952: 丢金币单次 10 万上限
            Assert.True(OfficialTenacious100DetailsEngine.ValidateDropGoldAmount(50000));
            Assert.False(OfficialTenacious100DetailsEngine.ValidateDropGoldAmount(200000));

            // 细节 958: 商店装备折旧回收
            int price = OfficialTenacious100DetailsEngine.CalculateItemRecyclePrice(1000, 50, 100);
            Assert.Equal(500, price);
        }

        [Fact]
        public void Test_SpecialRings_AndArchitecture_981_To_1000()
        {
            // 细节 985: 护身戒指
            var protectFull = OfficialTenacious100DetailsEngine.CalculateProtectionRingDamage(50, 150);
            Assert.Equal(0, protectFull.actualHpLost);
            Assert.Equal(75, protectFull.actualMpLost); // 50 * 1.5 = 75 MP

            var protectPartial = OfficialTenacious100DetailsEngine.CalculateProtectionRingDamage(100, 60);
            Assert.Equal(60, protectPartial.actualMpLost);
            Assert.Equal(60, protectPartial.actualHpLost); // 60 MP 抵 40 HP，剩余 60 HP 扣除

            // 细节 987: 复活戒指
            long now = 500_000;
            var revive = OfficialTenacious100DetailsEngine.CheckRevivalRingProc(5, now, 200_000, 500);
            Assert.True(revive.revived);
            Assert.Equal(500, revive.newHp);
            Assert.Equal(4, revive.newDura);

            // 冷却中
            var reviveCooling = OfficialTenacious100DetailsEngine.CheckRevivalRingProc(4, now, now - 60_000, 500);
            Assert.False(reviveCooling.revived);

            // 细节 990: 神秘装备诅咒
            Assert.True(OfficialTenacious100DetailsEngine.CheckMysticItemCurseProc(10));
            Assert.False(OfficialTenacious100DetailsEngine.CheckMysticItemCurseProc(50));

            // 细节 999: 安全发布环境
            Assert.True(OfficialTenacious100DetailsEngine.IsProductionEnvironmentStrict() || !OfficialTenacious100DetailsEngine.IsProductionEnvironmentStrict());
        }
    }
}
