using System;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests
{
    public class TestOfficialCosmic100DetailsMatrix
    {
        [Fact]
        public void Test_AuthorityDetails_801_to_820()
        {
            // 细节 801: 脱装背包满拦截
            var (ok1, _) = OfficialCosmic100DetailsEngine.ValidateTakeOffAuthority(39, 40);
            Assert.True(ok1);
            var (fail1, msg1) = OfficialCosmic100DetailsEngine.ValidateTakeOffAuthority(40, 40);
            Assert.False(fail1);
            Assert.Contains("背包已满", msg1);

            // 细节 803: 祝福油升级阶梯
            // 1) 优先洗诅咒
            var (washOk, _, luckAfterWash, curseAfterWash) = OfficialCosmic100DetailsEngine.ApplyBlessingOilAuthority(0, 1, 0, 0);
            Assert.True(washOk);
            Assert.Equal(0, curseAfterWash);

            // 2) 运0升运1 (70% 成功率)
            var (up1, _, l1, _) = OfficialCosmic100DetailsEngine.ApplyBlessingOilAuthority(0, 0, 50, 0);
            Assert.True(up1);
            Assert.Equal(1, l1);

            // 3) 失败加诅咒
            var (failBless, isCursed, _, c1) = OfficialCosmic100DetailsEngine.ApplyBlessingOilAuthority(3, 0, 80, 10);
            Assert.False(failBless);
            Assert.True(isCursed);
            Assert.Equal(1, c1);

            // 细节 806: 神水洗涤神秘装备
            var (canOff, isId) = OfficialCosmic100DetailsEngine.ApplyHolyWaterToMysticEquip(true, true);
            Assert.True(canOff);
            Assert.True(isId);
        }

        [Fact]
        public void Test_SkillDetails_821_to_850()
        {
            // 细节 821: 双烈火时间窗
            Assert.True(OfficialCosmic100DetailsEngine.IsDoubleFireHitWindowActive(1000, 9500));
            Assert.False(OfficialCosmic100DetailsEngine.IsDoubleFireHitWindowActive(1000, 5000));

            // 细节 824: 魔法盾减伤率
            Assert.Equal(0.40, OfficialCosmic100DetailsEngine.GetMagicShieldDamageReductionRate(1));
            Assert.Equal(0.50, OfficialCosmic100DetailsEngine.GetMagicShieldDamageReductionRate(2));
            Assert.Equal(0.60, OfficialCosmic100DetailsEngine.GetMagicShieldDamageReductionRate(3));

            // 细节 826: 圣言术几率
            int chance = OfficialCosmic100DetailsEngine.CalculateHolyWordChance(40, 30, 3);
            Assert.Equal(50, chance); // (40 - 30) * 2 + 30 = 50
        }

        [Fact]
        public void Test_BossAndEcologicalDetails_851_to_875()
        {
            // 细节 852: 双头金刚割肉
            var (hasDrop, itemName) = OfficialCosmic100DetailsEngine.ButchDualHeadKingCorpse(1, 10);
            Assert.True(hasDrop);
            Assert.Equal("法神项链", itemName);

            // 细节 856: 触龙神魔免
            Assert.True(OfficialCosmic100DetailsEngine.IsTouchDragonGodMagicImmune());
        }

        [Fact]
        public void Test_EconomyAndChatDetails_876_to_900()
        {
            // 细节 876: 交易金币安全
            Assert.True(OfficialCosmic100DetailsEngine.ValidateTradeGoldSafety(4_000_000, 10_000_000));
            Assert.False(OfficialCosmic100DetailsEngine.ValidateTradeGoldSafety(6_000_000, 10_000_000)); // 单笔超 500 万

            // 细节 887: 组队经验均摊
            Assert.Equal(300, OfficialCosmic100DetailsEngine.CalculateGroupSharedExp(1000, 4));

            // 细节 894: 聊天冷却
            Assert.True(OfficialCosmic100DetailsEngine.CheckChatCooldown(isShout: true, lastChatMs: 1000, nowMs: 6500));
            Assert.False(OfficialCosmic100DetailsEngine.CheckChatCooldown(isShout: true, lastChatMs: 1000, nowMs: 3000));
        }
    }
}
