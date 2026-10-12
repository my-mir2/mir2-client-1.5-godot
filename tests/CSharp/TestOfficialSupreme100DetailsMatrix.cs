using System;
using System.Collections.Generic;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests
{
    public class TestOfficialSupreme100DetailsMatrix
    {
        [Fact]
        public void Test_ClientAuthorityDetails_601_to_620()
        {
            // 细节 601: 快捷栏吃药权威仲裁
            var (canEat, _) = OfficialSupreme100DetailsEngine.ValidatePotionConsumeAuthority(isDead: false, lastEatMs: 1000, nowMs: 1500);
            Assert.True(canEat);
            var (deadEat, _) = OfficialSupreme100DetailsEngine.ValidatePotionConsumeAuthority(isDead: true, lastEatMs: 1000, nowMs: 1500);
            Assert.False(deadEat); // 阵亡不可吃药
            var (fastEat, _) = OfficialSupreme100DetailsEngine.ValidatePotionConsumeAuthority(isDead: false, lastEatMs: 1000, nowMs: 1100);
            Assert.False(fastEat); // 未满 300ms 冷却

            // 细节 602: 地表金币重量精算 (10,000 金币 = 1 负重)
            Assert.Equal(0, OfficialSupreme100DetailsEngine.CalculateGoldWeight(9999));
            Assert.Equal(1, OfficialSupreme100DetailsEngine.CalculateGoldWeight(10000));
            Assert.Equal(500, OfficialSupreme100DetailsEngine.CalculateGoldWeight(5_000_000));

            // 细节 603: 装备受击磨损
            var (worn, remDura) = OfficialSupreme100DetailsEngine.CalculateArmorDuraWear(100, 0, 5);
            Assert.True(worn);
            Assert.Equal(90, remDura); // 100 - (5 + 5) = 90

            // 细节 605: 火墙持续时间 (基础 20s + MaxMC * 1.5s)
            int fwMs = OfficialSupreme100DetailsEngine.CalculateFireWallDurationMs(20);
            Assert.Equal(50_000, fwMs); // (20 + 20 * 1.5) = 50s = 50000ms

            // 细节 607: 野蛮冲撞
            var (pushWall, _, selfWallStun, _) = OfficialSupreme100DetailsEngine.ExecuteAuthoritativeMotaebo(40, 30, hasWallCollision: true);
            Assert.False(pushWall);
            Assert.Equal(1000, selfWallStun);

            var (pushOk, pGrids, _, tgtStun) = OfficialSupreme100DetailsEngine.ExecuteAuthoritativeMotaebo(40, 30, hasWallCollision: false);
            Assert.True(pushOk);
            Assert.Equal(2, pGrids);
            Assert.Equal(1500, tgtStun);

            // 细节 613: 怪物真眼
            Assert.True(OfficialSupreme100DetailsEngine.CheckTrueSightMonster(72)); // 月魔蜘蛛
            Assert.True(OfficialSupreme100DetailsEngine.CheckTrueSightMonster(73)); // 楔蛾
            Assert.False(OfficialSupreme100DetailsEngine.CheckTrueSightMonster(10)); // 稻草人

            // 细节 617: 传送戒指智能避障
            var (fx, fy, found) = OfficialSupreme100DetailsEngine.FindSafeTeleportTile(100, 100, (x, y) => x == 101 && y == 100);
            Assert.True(found);
            Assert.Equal(101, fx);
            Assert.Equal(100, fy);
        }

        [Fact]
        public void Test_CombatAndSkillDetails_621_to_650()
        {
            // 细节 624: 半月弯刀伤害衰减 (主 100%, 副 40%, 外缘 20%)
            Assert.Equal(100, OfficialSupreme100DetailsEngine.CalculateHalfMoonTargetDamage(100, 0));
            Assert.Equal(40, OfficialSupreme100DetailsEngine.CalculateHalfMoonTargetDamage(100, 1));
            Assert.Equal(40, OfficialSupreme100DetailsEngine.CalculateHalfMoonTargetDamage(100, 2));
            Assert.Equal(20, OfficialSupreme100DetailsEngine.CalculateHalfMoonTargetDamage(100, 3));

            // 细节 625: 基本剑术命中加成
            Assert.Equal(3, OfficialSupreme100DetailsEngine.GetBasicSwordAccuracyBonus(3));

            // 细节 633: 治愈术溢出截断
            var (healed, newHp) = OfficialSupreme100DetailsEngine.ApplyHealingSpell(90, 100, 20);
            Assert.Equal(10, healed);
            Assert.Equal(100, newHp);

            // 细节 638: 召唤骷髅斧头色阶
            Assert.Equal("White", OfficialSupreme100DetailsEngine.GetSkeletonAxeColorTier(0));
            Assert.Equal("Green", OfficialSupreme100DetailsEngine.GetSkeletonAxeColorTier(2));
            Assert.Equal("Blue", OfficialSupreme100DetailsEngine.GetSkeletonAxeColorTier(4));
            Assert.Equal("Purple", OfficialSupreme100DetailsEngine.GetSkeletonAxeColorTier(6));
            Assert.Equal("Gold", OfficialSupreme100DetailsEngine.GetSkeletonAxeColorTier(7));

            // 细节 640: 幽灵盾持续时间
            int duraMs = OfficialSupreme100DetailsEngine.CalculateSoulShieldDurationMs(10, 20);
            Assert.Equal(50_000, duraMs); // (10 + 20 * 2) = 50s = 50000ms

            // 细节 644: 狂风套装挥刀硬直
            int delay = OfficialSupreme100DetailsEngine.CalculateWindSetAttackDelay(3);
            Assert.Equal(530, delay); // 650 - 3 * 40 = 530
        }

        [Fact]
        public void Test_BossAndDropDetails_651_to_675()
        {
            // 细节 651: 赤月恶魔地刺双抗折半
            int spikeDmg = OfficialSupreme100DetailsEngine.CalculateRedMoonDemonGroundSpike(100, 20, 10);
            Assert.Equal(85, spikeDmg); // 100 - (20 + 10) / 2 = 85

            // 细节 653: 双头血魔 8x8 满屏大爆
            var scatter = OfficialSupreme100DetailsEngine.GenerateBloodDemonDropScatter(100, 100, 16);
            Assert.Equal(16, scatter.Count);

            // 细节 661: 虹魔套装吸血 (10%)
            var (gained, newHp) = OfficialSupreme100DetailsEngine.ApplyRainbowMagicLeech(200, 50, 100, fullSet: true);
            Assert.Equal(20, gained);
            Assert.Equal(70, newHp);

            // 细节 662: 魔血套装转换
            var (addHp, subMp) = OfficialSupreme100DetailsEngine.GetMagicBloodSetConversion(fullSet: true);
            Assert.Equal(175, addHp);
            Assert.Equal(125, subMp);
        }

        [Fact]
        public void Test_EconomyAndUiDetails_676_to_700()
        {
            // 细节 679: 仓库金币存入 5000 万上限
            var (sOk, bankG, bagG) = OfficialSupreme100DetailsEngine.ProcessStorageGoldDeposit(40_000_000, 5_000_000, 5_000_000);
            Assert.True(sOk);
            Assert.Equal(45_000_000, bankG);
            Assert.Equal(0, bagG);

            var (sFail, _, _) = OfficialSupreme100DetailsEngine.ProcessStorageGoldDeposit(48_000_000, 5_000_000, 5_000_000);
            Assert.False(sFail); // 超过 50,000,000

            // 细节 683: 沙巴克城门损耗
            var (broken, remGate) = OfficialSupreme100DetailsEngine.ApplySabukGateDamage(1000, 1000);
            Assert.True(broken);
            Assert.Equal(0, remGate);

            // 细节 689: PK 点衰减 (每 200 秒消散 1 点)
            var (newPk, nextDecay) = OfficialSupreme100DetailsEngine.ProcessPkPointDecay(10, 0, 450_000);
            Assert.Equal(8, newPk); // 消散 2 点
            Assert.Equal(400_000, nextDecay);

            // 细节 696: 拖拽阴影偏移 (+2, +2)
            var (sx, sy) = OfficialSupreme100DetailsEngine.GetItemDragShadowOffset(10, 20);
            Assert.Equal(12, sx);
            Assert.Equal(22, sy);
        }
    }
}
