using System;
using System.Collections.Generic;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests
{
    public class TestOfficialPerfection100DetailsMatrix
    {
        [Fact]
        public void Test_ClientAuthoritySeparation_501_to_520()
        {
            // 细节 501: 穿脱装备权威校验
            Assert.True(OfficialPerfection100DetailsEngine.ValidateEquipWearAuthority(35, 0, 10, 30, 5, 30, 0));
            Assert.False(OfficialPerfection100DetailsEngine.ValidateEquipWearAuthority(25, 0, 10, 30, 5, 30, 0)); // 等级不够
            Assert.False(OfficialPerfection100DetailsEngine.ValidateEquipWearAuthority(35, 1, 10, 30, 5, 30, 0)); // 职业不符
            Assert.False(OfficialPerfection100DetailsEngine.ValidateEquipWearAuthority(35, 0, 28, 30, 5, 30, 0)); // 超重

            // 细节 502: 背包交换有效性
            Assert.True(OfficialPerfection100DetailsEngine.ValidateInventorySwapAuthority(40, 0, 5));
            Assert.False(OfficialPerfection100DetailsEngine.ValidateInventorySwapAuthority(40, 0, 0)); // 相同槽位
            Assert.False(OfficialPerfection100DetailsEngine.ValidateInventorySwapAuthority(40, 0, 40)); // 越界

            // 细节 503: 地表拾取权威校验
            Assert.True(OfficialPerfection100DetailsEngine.ValidateGroundItemPickUpAuthority(100, 100, 101, 101, 1, 1, 1000, 5000));
            Assert.False(OfficialPerfection100DetailsEngine.ValidateGroundItemPickUpAuthority(100, 100, 105, 105, 1, 1, 1000, 5000)); // 距离过远
            Assert.False(OfficialPerfection100DetailsEngine.ValidateGroundItemPickUpAuthority(100, 100, 101, 101, 2, 1, 1000, 50_000)); // 归属保护期内

            // 细节 504: NPC 商品购买价格折算 (沙城税率 10%)
            var (canBuy, price) = OfficialPerfection100DetailsEngine.CalculateNpcBuyPriceAuthority(1000, 0.10f, 1500);
            Assert.True(canBuy);
            Assert.Equal(1100, price);

            // 细节 505: NPC 回收折算
            int recycle = OfficialPerfection100DetailsEngine.CalculateNpcSellRecyclePriceAuthority(2000, 10, 20);
            Assert.Equal(500, recycle); // 2000 * 0.5 * 0.5 = 500

            // 细节 506: 修理费用精算
            var (costNorm, maxNorm) = OfficialPerfection100DetailsEngine.CalculateRepairAuthority(10, 20, 10, isSpecial: false);
            Assert.Equal(100, costNorm);
            Assert.Equal(19, maxNorm); // 20 - 1 = 19 (掉 1/16)

            var (costSpec, maxSpec) = OfficialPerfection100DetailsEngine.CalculateRepairAuthority(10, 20, 10, isSpecial: true);
            Assert.Equal(300, costSpec); // 特修 3 倍
            Assert.Equal(20, maxSpec);   // 不掉最大持久

            // 细节 507: 药水回血池恒定释放
            var (heal, remPool) = OfficialPerfection100DetailsEngine.TickHealthPoolAuthority(50, 100, 30, 5);
            Assert.Equal(5, heal);
            Assert.Equal(25, remPool);

            // 细节 508: 技能等级晋级
            var (lvUp, newLv, newTrain) = OfficialPerfection100DetailsEngine.CheckSkillLevelUpAuthority(0, 100, 100);
            Assert.True(lvUp);
            Assert.Equal(1, newLv);
            Assert.Equal(0, newTrain);

            // 细节 509: 仓库密码连续 3 次错误锁定 300 秒
            var (s1, l1, f1, t1) = OfficialPerfection100DetailsEngine.CheckStoragePasswordAuthority("wrong", "secret", 0, 0, 1000);
            Assert.False(s1);
            Assert.False(l1);
            Assert.Equal(1, f1);

            var (s2, l2, f2, t2) = OfficialPerfection100DetailsEngine.CheckStoragePasswordAuthority("wrong", "secret", 2, 0, 1000);
            Assert.False(s2);
            Assert.True(l2); // 触发锁定
            Assert.Equal(301_000, t2);

            // 细节 512: 物理出刀 CD 650ms 校验
            Assert.True(OfficialPerfection100DetailsEngine.ValidateAttackIntervalAuthority(1000, 1660, 0));
            Assert.False(OfficialPerfection100DetailsEngine.ValidateAttackIntervalAuthority(1000, 1500, 0)); // 加速刀拦截

            // 细节 514: 法术 GCD 1000ms 校验
            var (spAllowed, newMp) = OfficialPerfection100DetailsEngine.CheckSpellGcdAndMpAuthority(1000, 2100, 50, 20);
            Assert.True(spAllowed);
            Assert.Equal(30, newMp);

            // 细节 516: 杀人加诅几率
            var (curseAdded, newCurse) = OfficialPerfection100DetailsEngine.CheckWeaponCurseOnPkAuthority(0, 10);
            Assert.True(curseAdded);
            Assert.Equal(1, newCurse);
        }

        [Fact]
        public void Test_CombatDetails_521_to_550()
        {
            // 细节 521: 双烈火时间窗判定
            var (canDouble, desc) = OfficialPerfection100DetailsEngine.CheckDoubleFireHitWindow(0, 8500);
            Assert.True(canDouble);

            // 细节 522: 攻杀 1.3 倍物理爆发
            var (isPwr, pwrDmg) = OfficialPerfection100DetailsEngine.CalculatePowerHitDamage(100, 0);
            Assert.True(isPwr);
            Assert.Equal(130, pwrDmg);

            // 细节 523: 刺杀隔位破盾
            var (stabDmg, pierced) = OfficialPerfection100DetailsEngine.CalculateLongHitFarDamage(60, 20, 0.6);
            Assert.Equal(60, stabDmg);
            Assert.True(pierced);

            // 细节 524: 魔法盾减伤
            int dmgShield3 = OfficialPerfection100DetailsEngine.ApplyMagicShieldDamageReduction(100, 3);
            Assert.Equal(40, dmgShield3); // 100 * (1 - 0.6) = 40

            // 细节 526: 抗拒火环推人
            var (pushed, pDist) = OfficialPerfection100DetailsEngine.CheckRingOfFirePush(35, 30, isBoss: false);
            Assert.True(pushed);
            Assert.Equal(2, pDist);

            // 细节 528: 圣言术秒杀几率
            bool holySuccess = OfficialPerfection100DetailsEngine.CheckHolyWordInstaKill(40, 20, 3, 20);
            Assert.True(holySuccess);

            // 细节 529: 地狱雷光不死系 150% 增伤
            int hellUndead = OfficialPerfection100DetailsEngine.CalculateHellfireDamage(100, isUndead: true);
            Assert.Equal(150, hellUndead);

            // 细节 532: 灵魂火符扣减护身符
            var (talismanOk, remTalisman) = OfficialPerfection100DetailsEngine.ConsumeTalismanForFireCharm(50);
            Assert.True(talismanOk);
            Assert.Equal(49, remTalisman);

            // 细节 533: 施毒术红毒降防 20%
            var (ac, mac) = OfficialPerfection100DetailsEngine.ApplyRedPoisonReduction(20, 15, hasRedPoison: true);
            Assert.Equal(16, ac);
            Assert.Equal(12, mac);

            // 细节 534: 神兽 Lv.7 属性
            var (beastHp, bMinDc, bMaxDc) = OfficialPerfection100DetailsEngine.GetDivineBeastStats(7);
            Assert.Equal(2200, beastHp);
            Assert.Equal(60, bMinDc);
            Assert.Equal(90, bMaxDc);

            // 细节 540: 麻痹戒指 1/15 几率
            var (paralyzed, pSec) = OfficialPerfection100DetailsEngine.CheckParalyzeRingEffect(0);
            Assert.True(paralyzed);
            Assert.Equal(5, pSec);

            // 细节 541: 护身戒指优先扣蓝
            var (hpLost, leftMp) = OfficialPerfection100DetailsEngine.ApplyProtectionRingDamage(60, 100, 100);
            Assert.Equal(0, hpLost);
            Assert.Equal(40, leftMp);
        }

        [Fact]
        public void Test_BossAndEnvironmentDetails_551_to_575()
        {
            // 细节 552: 双头金刚必须挖肉
            Assert.True(OfficialPerfection100DetailsEngine.IsDualHeadKingButchExclusive(71));

            // 细节 553: 双头血魔物理高防魔御极低
            var (ac, mac) = OfficialPerfection100DetailsEngine.GetDualHeadBloodDemonDefense();
            Assert.Equal(40, ac);
            Assert.Equal(5, mac);

            // 细节 554: 沃玛教主 30% 血量瞬移
            Assert.True(OfficialPerfection100DetailsEngine.CheckWomaLeaderEscapeTeleport(290, 1000));
            Assert.False(OfficialPerfection100DetailsEngine.CheckWomaLeaderEscapeTeleport(350, 1000));

            // 细节 555: 祖玛教主破石苏醒
            Assert.True(OfficialPerfection100DetailsEngine.CheckZumaLeaderAwakening(100, 100, 102, 102, isAttacked: false));
            Assert.False(OfficialPerfection100DetailsEngine.CheckZumaLeaderAwakening(100, 100, 110, 110, isAttacked: false));

            // 细节 556: 未知暗殿真假神兽
            var (rHp, rSpd, rFake) = OfficialPerfection100DetailsEngine.GetUnknownDarkHallBeast(isRealVariant: true);
            Assert.Equal(3500, rHp);
            Assert.False(rFake);

            // 细节 559: 触龙神魔法免疫
            Assert.Equal(0, OfficialPerfection100DetailsEngine.ApplyTouchDragonGodDamage(100, isMagicDmg: true));
            Assert.Equal(100, OfficialPerfection100DetailsEngine.ApplyTouchDragonGodDamage(100, isMagicDmg: false));

            // 细节 560: 角蝇产卵上限 10 只
            Assert.True(OfficialPerfection100DetailsEngine.CanHornFlySpawnBat(9));
            Assert.False(OfficialPerfection100DetailsEngine.CanHornFlySpawnBat(10));

            // 细节 571: 回城卷轴红名送往红名村
            var (rmMap, rmX, rmY) = OfficialPerfection100DetailsEngine.GetRecallScrollDestination(250, "0", 330, 270);
            Assert.Equal("0", rmMap);
            Assert.Equal(33, rmX);
            Assert.Equal(33, rmY);
        }

        [Fact]
        public void Test_EconomyAndSocialDetails_576_to_600()
        {
            // 细节 576: 交易金币单笔 500 万与总额 5000 万熔断
            Assert.True(OfficialPerfection100DetailsEngine.ValidateTradeGoldLimit(4_000_000, 10_000_000));
            Assert.False(OfficialPerfection100DetailsEngine.ValidateTradeGoldLimit(6_000_000, 10_000_000)); // 单笔超 500 万
            Assert.False(OfficialPerfection100DetailsEngine.ValidateTradeGoldLimit(3_000_000, 49_000_000)); // 总额超 5000 万

            // 细节 582: 地表金币三阶外观
            Assert.Equal(110, OfficialPerfection100DetailsEngine.GetGroundGoldAppearance(50));
            Assert.Equal(111, OfficialPerfection100DetailsEngine.GetGroundGoldAppearance(500));
            Assert.Equal(112, OfficialPerfection100DetailsEngine.GetGroundGoldAppearance(5000));

            // 细节 584: 红名死亡金币大爆 (30%~50%)
            int droppedGold = OfficialPerfection100DetailsEngine.CalculateRedNameDeathGoldDrop(1_000_000, 40);
            Assert.Equal(400_000, droppedGold);

            // 细节 586: 沙巴克皇宫排他性纯净度
            Assert.True(OfficialPerfection100DetailsEngine.CheckSabukPalaceExclusiveOwnership(new List<int> { 1, 1, 1 }, 1));
            Assert.False(OfficialPerfection100DetailsEngine.CheckSabukPalaceExclusiveOwnership(new List<int> { 1, 2, 1 }, 1));

            // 细节 589: 行会封号 14 字符截断
            Assert.Equal("12345678901234", OfficialPerfection100DetailsEngine.SanitizeGuildRankTitle("12345678901234567890"));

            // 细节 590: 组队 11 人限制与经验均摊
            var (canJoin, expEach) = OfficialPerfection100DetailsEngine.CalculateGroupExpSharing(3, 1000);
            Assert.True(canJoin);
            Assert.Equal(300, expEach); // 1000 * 1.2 = 1200 / 4 = 300

            // 细节 594: 装备持久为 0 属性失效
            var (effAc, effDc) = OfficialPerfection100DetailsEngine.GetEffectiveEquipStats(10, 20, 0);
            Assert.Equal(0, effAc);
            Assert.Equal(0, effDc);

            // 细节 597: 聊天频道冷却防刷屏
            Assert.True(OfficialPerfection100DetailsEngine.ValidateChatChannelCooldown(1, 1000, 6500));
            Assert.False(OfficialPerfection100DetailsEngine.ValidateChatChannelCooldown(1, 1000, 3000)); // 喊话未满 5000ms

            // 细节 598: 非安全区喊话扣 1 点 MP
            var (shoutOk, leftMp) = OfficialPerfection100DetailsEngine.CheckShoutMpCost(inSafeZone: false, currentMp: 10);
            Assert.True(shoutOk);
            Assert.Equal(9, leftMp);
        }
    }
}
