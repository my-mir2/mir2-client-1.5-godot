using System;
using System.Collections.Generic;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests
{
    public class TestOfficialDivine100DetailsMatrix
    {
        [Fact]
        public void Test_ClientAuthorityDetails_701_to_720()
        {
            // 细节 701: 转向权威校验
            var (canTurnDead, dirDead) = OfficialDivine100DetailsEngine.ValidateTurnDirectionAuthority(isDead: true, isParalyzed: false, newDir: 2, currentDir: 0);
            Assert.False(canTurnDead);
            Assert.Equal(0, dirDead); // 阵亡禁止转向

            var (canTurnPara, dirPara) = OfficialDivine100DetailsEngine.ValidateTurnDirectionAuthority(isDead: false, isParalyzed: true, newDir: 2, currentDir: 0);
            Assert.False(canTurnPara);
            Assert.Equal(0, dirPara); // 麻痹禁止转向

            var (canTurnOk, dirOk) = OfficialDivine100DetailsEngine.ValidateTurnDirectionAuthority(isDead: false, isParalyzed: false, newDir: 2, currentDir: 0);
            Assert.True(canTurnOk);
            Assert.Equal(2, dirOk);

            // 细节 703: 攻击朝向
            Assert.True(OfficialDivine100DetailsEngine.ValidateMeleeAttackFacing(100, 100, 2, 101, 100)); // 面朝东，目标在东边
            Assert.False(OfficialDivine100DetailsEngine.ValidateMeleeAttackFacing(100, 100, 6, 101, 100)); // 面朝西，目标在东边 (背对)

            // 细节 704: 刺杀隔位直线
            Assert.True(OfficialDivine100DetailsEngine.ValidateLongHitAlignment(100, 100, 102, 100)); // 水平直线
            Assert.True(OfficialDivine100DetailsEngine.ValidateLongHitAlignment(100, 100, 102, 102)); // 45度斜线
            Assert.False(OfficialDivine100DetailsEngine.ValidateLongHitAlignment(100, 100, 102, 101)); // 歪角非直线

            // 细节 707: 雷电术 12 格射程
            Assert.True(OfficialDivine100DetailsEngine.ValidateLightningRange(100, 100, 110, 110));
            Assert.False(OfficialDivine100DetailsEngine.ValidateLightningRange(100, 100, 115, 115)); // 超出 12 格

            // 细节 714: 死亡视野收缩为 3
            Assert.Equal(3, OfficialDivine100DetailsEngine.GetDeathVisionRadius(isDead: true, normalRadius: 12));
            Assert.Equal(12, OfficialDivine100DetailsEngine.GetDeathVisionRadius(isDead: false, normalRadius: 12));

            // 细节 720: 打坐 1.2 倍易伤与强制打断
            var (dmgSit, interrupted) = OfficialDivine100DetailsEngine.ApplySitDownDamageVulnerability(100, isSitting: true);
            Assert.Equal(120, dmgSit);
            Assert.True(interrupted);
        }

        [Fact]
        public void Test_ItemAndAttributeDetails_721_to_750()
        {
            // 细节 721: 极品词条生成
            Assert.Equal(3, OfficialDivine100DetailsEngine.GenerateItemBonusPoints(0));
            Assert.Equal(2, OfficialDivine100DetailsEngine.GenerateItemBonusPoints(2));
            Assert.Equal(1, OfficialDivine100DetailsEngine.GenerateItemBonusPoints(10));
            Assert.Equal(0, OfficialDivine100DetailsEngine.GenerateItemBonusPoints(100));

            // 细节 735: 诅 10 与运 9 攻击发挥
            Assert.Equal(15, OfficialDivine100DetailsEngine.CalculateWeaponAttackDamage(15, 0, luck: 0, curse: 10, randVal: 5)); // 罗刹诅10打下限
            Assert.Equal(30, OfficialDivine100DetailsEngine.CalculateWeaponAttackDamage(0, 30, luck: 9, curse: 0, randVal: 5)); // 裁决运9打上限

            // 细节 736: 终极战袍属性
            var (ac1, mac1, dc1, _, _, req1, w1) = OfficialDivine100DetailsEngine.GetUltimateArmorStats(1); // 战神盔甲
            Assert.Equal(9, ac1);
            Assert.Equal(5, mac1);
            Assert.Equal(2, dc1);
            Assert.Equal(46, req1);
            Assert.Equal(40, w1);
        }

        [Fact]
        public void Test_BossAndEcologicalDetails_751_to_775()
        {
            // 细节 752: 未知暗殿真假白猪
            var (hpFake, spdFake, isFake) = OfficialDivine100DetailsEngine.GetUnknownDarkHallWhiteBoar(isFakeVariant: true);
            Assert.Equal(500, hpFake);
            Assert.Equal(400, spdFake);
            Assert.True(isFake);

            // 细节 755: 沃玛教主必爆号角
            Assert.True(OfficialDivine100DetailsEngine.CheckWomaHornDrop(30));

            // 细节 774: 圣言术秒杀无掉落保留 10% 经验
            var (hasDrop, finalExp) = OfficialDivine100DetailsEngine.CalculateHolyWordReward(1000, isInstaKill: true);
            Assert.False(hasDrop);
            Assert.Equal(100, finalExp);
        }

        [Fact]
        public void Test_EconomyAndSecurityDetails_776_to_800()
        {
            // 细节 779: 沙城税金提取
            var (wOk, wAmt, remBank) = OfficialDivine100DetailsEngine.WithdrawSabukCityGold(10_000_000, 3_000_000);
            Assert.True(wOk);
            Assert.Equal(3_000_000, wAmt);
            Assert.Equal(7_000_000, remBank);

            // 细节 780: 修理城门
            var (repOk, remG) = OfficialDivine100DetailsEngine.RepairSabukGate(1_000_000);
            Assert.True(repOk);
            Assert.Equal(500_000, remG);

            // 细节 794: 协议序号防重放自增校验
            var (seqValid, nextSeq) = OfficialDivine100DetailsEngine.ValidateProtocolSequence(100, 100);
            Assert.True(seqValid);
            Assert.Equal(101u, nextSeq);

            var (seqInvalid, _) = OfficialDivine100DetailsEngine.ValidateProtocolSequence(99, 100);
            Assert.False(seqInvalid);
        }
    }
}
