using System;
using System.Collections.Generic;

namespace Mir2.Shared.Rules;

/// <summary>
/// 盛大官方 1.5 战法道进阶技能、辅助光环与特殊状态抗性权威规则引擎 (细节 171 ~ 190)
/// 严格由服务端作为唯一权威判定执行，计算结果通过 SM_xxx 广播至各客户端渲染表现
/// </summary>
public static class AdvancedCombatRuleEngine
{
    // 细节 171: 战士野蛮冲撞撞碎地表木箱并产生碎屑
    public static class SavageCrateSmashEngine
    {
        public static (bool smashed, int woodParticles) CheckSmash(bool isCrate, int warriorLevel)
        {
            if (isCrate && warriorLevel >= 30)
            {
                return (true, 18); // 产生 18 个木屑粒子
            }
            return (false, 0);
        }
    }

    // 细节 172: 战士刺杀第二格剑气严格无视神圣战甲术 (AC+3) 与幽灵盾 (MAC+3)
    public static class SwordQiAuraPierceEngine
    {
        public static int CalculateDamage(int rawDc, int targetAc, int targetBuffAc, bool isSecondGrid)
        {
            if (isSecondGrid)
            {
                // 第二格完全无视目标基础防御与 Buff 防御
                return rawDc;
            }
            int totalAc = targetAc + targetBuffAc;
            return Math.Max(0, rawDc - totalAc);
        }
    }

    // 细节 173: 战士半月弯刀副目标严格不触发吸血 (虹魔套) 与中毒
    public static class HalfMoonSubTargetEngine
    {
        public static (int vampHp, bool applyPoison) EvaluateSubTargetProc(bool isSubTarget, int damage, bool hasRainbowSuit)
        {
            if (isSubTarget)
            {
                return (0, false); // 副目标绝不触发吸血与施毒
            }
            int vamp = hasRainbowSuit ? (int)(damage * 0.1f) : 0;
            return (vamp, false);
        }
    }

    // 细节 174: 烈火剑法蓄力状态单次受到 >30% 最大生命伤害时被打断
    public static class FireSwordDisruptEngine
    {
        public static (bool fireChargeMaintained, string reason) CheckDisruption(bool isCharged, int incomingDamage, int maxHp)
        {
            if (!isCharged) return (false, "");
            if (incomingDamage >= (int)(maxHp * 0.30f))
            {
                return (false, "遭受巨额重创，烈火剑气溃散！");
            }
            return (true, "");
        }
    }

    // 细节 175: 攻杀剑术对不死系生物 (骷髅/僵尸) 额外造成 150ms 击退硬直
    public static class AssassinationUndeadEngine
    {
        public static (int stunDurationMs, bool knockback) EvaluateUndeadHit(bool isUndead, bool isAssassinationHit)
        {
            if (isUndead && isAssassinationHit)
            {
                return (150, true);
            }
            return (0, false);
        }
    }

    // 细节 176: 诱惑之光对高阶生物 (白野猪/沃玛卫士) 无法招募但概率触发 5 秒黄字瘫痪定身
    public static class TameParalysisEngine
    {
        public static (bool tameSuccess, bool paralysisTriggered, float duration) AttemptTameBoss(int monsterLevel, int mageLevel, int roll100)
        {
            if (monsterLevel >= 35) // 白野猪/沃玛卫士级别不可招募
            {
                bool paralyze = roll100 < 35; // 35% 几率定身瘫痪
                return (false, paralyze, paralyze ? 5.0f : 0f);
            }
            return (roll100 < 50, false, 0f);
        }
    }

    // 细节 177: 诱惑宝宝存活超 6 小时叛变名字变黄攻击主人
    public static class TamedPetRebellionEngine
    {
        public const float MaxLoyaltyHours = 6.0f;
        public static (bool isRebelled, string nameColor) CheckRebellion(float survivalHours)
        {
            if (survivalHours >= MaxLoyaltyHours)
            {
                return (true, "#FFFF00"); // 变黄名叛变
            }
            return (false, "#0000FF"); // 蓝名正常
        }
    }

    // 细节 178: 小火球与大火球击退轻型怪物 (鹿/鸡/多钩猫) 1 格物理判定
    public static class FireballKnockbackEngine
    {
        public static (bool knockback, int stepX, int stepY) CalculateKnockback(bool isLightweight, int casterX, int casterY, int targetX, int targetY)
        {
            if (!isLightweight) return (false, 0, 0);
            int dx = Math.Sign(targetX - casterX);
            int dy = Math.Sign(targetY - casterY);
            return (true, targetX + dx, targetY + dy);
        }
    }

    // 细节 179: 火墙地表遇冰咆哮燃烧剩余时间缩短 5 秒
    public static class IceExtinguishFirewallEngine
    {
        public static float ApplyIceStormImpact(float remainingFirewallSeconds)
        {
            return Math.Max(0f, remainingFirewallSeconds - 5.0f);
        }
    }

    // 细节 180: 魔法盾受到神圣伤害 (火符/地狱雷光) 15% 护盾穿透率
    public static class MagicShieldHolyPierceEngine
    {
        public static (int absorbedDmg, int piercedDirectDmg) CalculateHolyPierce(int rawDamage, bool isHoly)
        {
            if (isHoly)
            {
                int direct = (int)(rawDamage * 0.15f);
                int absorbed = rawDamage - direct;
                return (absorbed, direct);
            }
            return (rawDamage, 0);
        }
    }

    // 细节 181: 瞬息移动 5 秒内连续使用蓝耗翻倍递增防无限飞
    public static class TeleportManaSurgeEngine
    {
        public const int BaseMpCost = 10;
        public static int CalculateSurgeCost(int continuousCastCount, float secondsSinceLastCast)
        {
            if (secondsSinceLastCast > 5.0f) return BaseMpCost;
            return BaseMpCost * (1 << Math.Min(4, continuousCastCount)); // 最多翻 16 倍
        }
    }

    // 细节 182: 施毒术绿毒扣血不破除目标被施加的隐身状态
    public static class PoisonInvisibilityEngine
    {
        public static bool DoesPoisonBreakInvisibility() => false;
    }

    // 细节 183: 真实视距生物 (楔蛾/月魔/触龙神) 面前隐身术失效
    public static class TrueSightEngine
    {
        public static bool CanDetectInvisible(string monsterName)
        {
            return monsterName is "楔蛾" or "月魔蜘蛛" or "触龙神";
        }
    }

    // 细节 184: 神圣战甲术与幽灵盾单次施法最多作用 8 个友方
    public static class AuraTargetLimitEngine
    {
        public const int MaxAuraTargets = 8;
        public static List<T> FilterAuraTargets<T>(List<T> nearbyAllies)
        {
            if (nearbyAllies.Count <= MaxAuraTargets) return nearbyAllies;
            return nearbyAllies.GetRange(0, MaxAuraTargets);
        }
    }

    // 细节 185: 困魔咒金黄八卦光柱锁怪 (目标定身不反击，受外力攻击破咒)
    public static class TrapMonsterEngine
    {
        public static (bool isTrapped, bool brokenByDamage) EvaluateTrap(bool hasTrapAura, int incomingDamage)
        {
            if (!hasTrapAura) return (false, false);
            if (incomingDamage > 0) return (false, true); // 受击破咒
            return (true, false);
        }
    }

    // 细节 186: 群体隐身术 3x3 八卦光斑散落全队隐身
    public static class GroupInvisibilityEngine
    {
        public static bool IsIn3x3Area(int casterX, int casterY, int targetX, int targetY)
        {
            return Math.Abs(casterX - targetX) <= 1 && Math.Abs(casterY - targetY) <= 1;
        }
    }

    // 细节 187: 骷髅战士 0 级到 7 级外观进阶 (小斧/大板斧/蓝骨/红骨)
    public static class SkeletonEvolutionEngine
    {
        public enum SkeletonForm { SmallAxe, HeavyAxe, BlueBone, RedBoneReinforced }

        public static SkeletonForm GetSkeletonForm(int skillLevel)
        {
            return skillLevel switch
            {
                <= 2 => SkeletonForm.SmallAxe,
                <= 4 => SkeletonForm.HeavyAxe,
                <= 6 => SkeletonForm.BlueBone,
                _ => SkeletonForm.RedBoneReinforced
            };
        }
    }

    // 细节 188: 神兽 0 级到 7 级喷火颜色 (暗红/亮红/纯白圣火) 与缩放
    public static class ShinsuEvolutionEngine
    {
        public static (string flameColorHex, float scale) GetShinsuStats(int petLevel)
        {
            if (petLevel < 3) return ("#8B0000", 1.0f); // 暗红小神兽
            if (petLevel < 7) return ("#FF4500", 1.2f); // 亮红中神兽
            return ("#FFFFFF", 1.4f); // 纯白极热 7 级大麒麟
        }
    }

    // 细节 189: 心灵启示显现目标真实数字 HP 与百分比血条 30 秒
    public static class MindInsightEngine
    {
        public const float DurationSeconds = 30.0f;
        public static (bool showDetails, string hpRatioText) GetInsightDisplay(float elapsed, int curHp, int maxHp)
        {
            if (elapsed >= DurationSeconds) return (false, "");
            float ratio = maxHp > 0 ? (float)curHp / maxHp * 100f : 0f;
            return (true, $"{curHp}/{maxHp} ({ratio:F0}%)");
        }
    }

    // 盛大官方 1.5 核心真值 (细节 190): 法师 12 级原生技能【抗拒火环】(Repulsion Ring / Ring of Fire)
    // 施法以自身为中心，爆发炽热烈焰冲击波，推开周围 8 方向紧邻 1 格且等级低于施法者的目标；等级高于或等于施法者则推不动
    public static class RepulsionRingEngine
    {
        public static (bool repelled, int targetNewX, int targetNewY) TryRepel(
            int selfLevel, int targetLevel, int selfX, int selfY, int targetX, int targetY)
        {
            if (targetLevel >= selfLevel) return (false, targetX, targetY); // 等级高推不动
            int dx = Math.Sign(targetX - selfX);
            int dy = Math.Sign(targetY - selfY);
            return (true, targetX + dx, targetY + dy);
        }
    }


    // ========================================================================
    // 盛大官方 1.5 怪物首领高阶 AI、狂暴、魔免与自爆机制 (细节 191 ~ 210)
    // ========================================================================
    public static class BossEncounterRuleEngine
    {
        // 细节 191: 触龙神绿毒与大范围群体石化
        public static class EvilSnakePoisonEngine
        {
            public static (bool petrified, bool poisoned, float duration) EvaluateBreath(int roll100)
            {
                bool petrify = roll100 < 25; // 25% 概率石化 3 秒
                return (petrify, true, petrify ? 3.0f : 0f);
            }
        }

        // 细节 192: 触龙神绝对免疫一切魔法攻击 (纯物理检定)
        public static class EvilSnakeMagicImmunityEngine
        {
            public static int CalculateDamage(int rawDamage, bool isMagicAttack) =>
                isMagicAttack ? 0 : rawDamage;
        }

        // 细节 194: 极品祖玛卫士移速与前摇
        public static class ZumaSuperGuardEngine
        {
            public static (float speedMulti, float attackPrecastSeconds) GetGuardAttributes(bool isSuperGuard) =>
                isSuperGuard ? (1.5f, 0.25f) : (1.0f, 0.45f);
        }

        // 细节 195: 极品祖玛雕像濒死 (HP < 10%) 自爆
        public static class ZumaStatueSelfDestructEngine
        {
            public static (bool willExplode, int explodeDamage) CheckSelfDestruct(int currentHp, int maxHp)
            {
                if (currentHp > 0 && ((float)currentHp / maxHp) <= 0.10f)
                {
                    return (true, 80); // 周围 1 格造成 80 点自爆伤害
                }
                return (false, 0);
            }
        }

        // 细节 198: 角蝇蝙蝠同屏产卵上限
        public static class BatSpitterEngine
        {
            public const int MaxBats = 10;
            public static bool CanSpawnBat(int currentBatsAlive) => currentBatsAlive < MaxBats;
        }

        // 细节 199: 沃玛卫士双刀连斩
        public static class WomaDualBladeEngine
        {
            public static (bool isDualSlash, int finalDamage) CalculateSlashDamage(int baseDc, bool dualSlashProc) =>
                dualSlashProc ? (true, baseDc * 2) : (false, baseDc);
        }

        // 细节 200: 尸王铁链前方横扫 (按当前朝向前方 1~3 格真实物理矢量)
        public static class CorpseKingChainEngine
        {
            public static List<(int x, int y)> GetChainSweepGrids(int x, int y, int dir)
            {
                var (dx, dy) = GetDirectionDelta(dir);
                return new() { (x + dx, y + dy), (x + dx * 2, y + dy * 2), (x + dx * 3, y + dy * 3) };
            }
        }

        // 细节 201: 骷髅精灵双叉狂暴
        public static class SkeletonElfEnrageEngine
        {
            public static (bool glowRed, float enrageMulti) CheckEnraged(int curHp, int maxHp) =>
                curHp <= maxHp * 0.5f ? (true, 1.3f) : (false, 1.0f);
        }

        // 细节 202: 沃玛教主狂暴瞬移伏击 (真实瞬移至目标背后格偷袭)
        public static class WomaLeaderTeleportEngine
        {
            public static (int tpX, int tpY, bool tpOk) CalculateAmbush(int targetX, int targetY, int targetDir, bool isBerserk)
            {
                if (!isBerserk) return (targetX, targetY, false);
                // 目标背后方向：对向矢量 (targetDir + 4) % 8
                int backDir = (targetDir + 4) & 7;
                var (dx, dy) = GetDirectionDelta(backDir);
                return (targetX + dx, targetY + dy, true);
            }
        }

        /// <summary>
        /// 盛大传奇标准 8 方向增量矢量表 (0:上, 1:右上, 2:右, 3:右下, 4:下, 5:左下, 6:左, 7:左上)
        /// </summary>
        private static (int dx, int dy) GetDirectionDelta(int dir)
        {
            return (dir & 7) switch
            {
                0 => (0, -1),
                1 => (1, -1),
                2 => (1, 0),
                3 => (1, 1),
                4 => (0, 1),
                5 => (-1, 1),
                6 => (-1, 0),
                7 => (-1, -1),
                _ => (0, 0)
            };
        }

        // 细节 203: 祖玛教主战鼓召唤
        public static class ZumaLeaderSummonEngine
        {
            public static (bool triggerSummon, int guardCount) CheckSummon(int curHp, int maxHp, bool summoned) =>
                !summoned && curHp <= maxHp * 0.20f ? (true, 4) : (false, 0);
        }

        // 细节 204: 赤月恶魔全屏地刺穿刺受 AC/MAC 双重减免
        public static class RedMoonSpikeDefenseEngine
        {
            public static int CalculateSpikeDamage(int baseDamage, int ac, int mac) =>
                Math.Max(5, baseDamage - (ac + mac) / 2);

            public static int CalculateDamage(int baseDamage, int ac, int mac) =>
                CalculateSpikeDamage(baseDamage, ac, mac);
        }

        // 细节 205: 双头金刚持屠夫小刀挖肉
        public static class DoubleHeadKongHarvestEngine
        {
            public static (bool harvOk, string item) TryHarvest(bool hasButcherKnife, int roll100) =>
                hasButcherKnife ? (true, "法神头盔") : (false, "");
        }

        // 细节 206: 双头血魔暴雨与圣战大爆
        public static class BloodDemonStormDropEngine
        {
            public static (bool rain, List<string> drops) EvaluateDeath() =>
                (true, new() { "圣战戒指", "强效金创药" });
        }

        // 盛大官方 1.5 核心真值 (细节 207): 原生首领【祖玛教主】挥击与烈焰法术双重伤害机制
        public static class ZumaLordDualStrikeEngine
        {
            public static (int dc, int mc) CalculateDualDamage(int rawDc, int rawMc) =>
                (rawDc, (int)(rawMc * 1.25f));
        }

        // 细节 208: 盛大官方 1.5 核心真值: 赤月恶魔大爆散落道士终极神装【天尊全套】
        public static class RedMoonDemonDropEngine
        {
            public static (bool success, string itemName) GenerateDrop(bool isRedMoonDemon, int roll100) =>
                (isRedMoonDemon && roll100 < 30, "天尊头盔");
        }

        // 细节 209: 盛大官方 1.5 核心真值: 沃玛教主大爆行会建立唯一信物【沃玛号角】
        public static class WomaLeaderHornDropEngine
        {
            public static List<string> GenerateDrop(int roll100) =>
                new() { "沃玛号角", "铂金戒指", "金币" };
        }

        // 细节 210: 白野猪死后满屏 8x8 道具同心圆螺旋散开大爆
        public static class WhiteBoarBigDropEngine
        {
            public static List<(int x, int y)> Generate8x8Drop(int centerX, int centerY)
            {
                var list = new List<(int, int)>(64);
                for (int dx = -4; dx < 4; dx++)
                {
                    for (int dy = -4; dy < 4; dy++)
                    {
                        list.Add((centerX + dx, centerY + dy));
                    }
                }
                return list;
            }
        }
    }

    // ========================================================================
    // 盛大官方 1.5 装备磨损、暗属性与极品掉落规则 (细节 211 ~ 230)
    // ========================================================================
    public static class EquipmentWearRuleEngine
    {
        // 细节 211: 暴击 5% 概率磨损 1 点持久
        public static class ArmorDuraWearEngine
        {
            public static (int newDura, bool duraReduced) CheckWear(int currentDura, bool isCriticalHit, int roll100)
            {
                if (isCriticalHit && (roll100 < 5))
                {
                    return (Math.Max(0, currentDura - 1), true);
                }
                return (currentDura, false);
            }
        }

        // 细节 212: 武器砍中不可破坏硬物掉 1 点持久
        public static class WeaponHardObstacleWearEngine
        {
            public static (int newDura, bool chipped) StrikeHardObstacle(int currentDura, bool isHardObstacle) =>
                isHardObstacle ? (Math.Max(0, currentDura - 1), true) : (currentDura, false);
        }

        // 细节 214: 神秘头盔属性不足无法取下
        public static class MysticHelmetLockEngine
        {
            public static (bool canTakeOff, string message) EvaluateTakeOff(int playerDc, int playerMc, int requiredDc, int requiredMc)
            {
                if (playerDc < requiredDc || playerMc < requiredMc)
                {
                    return (false, "神秘力量封印了头盔，你的属性不足，无法取下！");
                }
                return (true, "");
            }
        }

        // 细节 215: 祈祷头盔死亡掉落必碎裂消失
        public static class PrayerHelmetShatterEngine
        {
            public static (bool isShattered, bool droppedOnGround) OnPlayerDeath(bool isWearingPrayerHelmet) =>
                isWearingPrayerHelmet ? (true, false) : (false, true);
        }

        // 细节 216: 记忆项链幸运权重
        public static class MemoryNecklaceLuckWeightEngine
        {
            public static int RollLuck(int roll1000)
            {
                if (roll1000 < 5) return 3;
                if (roll1000 < 30) return 2;
                if (roll1000 < 150) return 1;
                return 0;
            }
        }

        // 细节 217: 恶魔铃铛诱惑概率 +15%
        public static class DevilBellTameEngine
        {
            public static float ApplyTameBonus(float baseRate, bool hasDevilBell) =>
                hasDevilBell ? Math.Min(1.0f, baseRate + 0.15f) : baseRate;
        }

        // 细节 218: 龙之手镯 10% 破盾几率
        public static class DragonBraceletShieldBreakEngine
        {
            public static bool CheckShieldBreak(bool hasDragonBracelet, int roll100) =>
                hasDragonBracelet && roll100 < 10;
        }

        // 细节 220: 红宝石戒指缩短法师魔法盾受击硬直 30%
        public static class RubyShieldRecoveryEngine
        {
            public static float CalculateShieldStun(float baseStun, bool hasRubyRing) =>
                hasRubyRing ? (float)Math.Round(baseStun * 0.70f, 2) : baseStun;
        }

        // 细节 222: 生铁戒指魔法防御对火墙按比例减免
        public static class IronRingFirewallDefEngine
        {
            public static int ReduceFirewallDamage(int damage, int ironRingCount) =>
                (int)(damage * (1.0f - 0.08f * ironRingCount));
        }

        // 细节 223: 降妖除魔戒指恶魔类怪物伤害减免 10%
        public static class DemonSubdueRingEngine
        {
            public static int ApplyDemonReduction(int damage, bool isDemonMonster, bool hasDemonSubdueRing) =>
                isDemonMonster && hasDemonSubdueRing ? (int)(damage * 0.90f) : damage;
        }

        // 细节 224: 珊瑚戒指轻便负重 (重 1) 与高攻击偏向 (0~4)
        public static class CoralRingWeightEngine
        {
            public static (int weight, int minDc, int maxDc) GetStats() => (1, 0, 4);
        }

        // 细节 225: 金手镯极品生成优先加权魔法防御
        public static class GoldBraceletSuperStatEngine
        {
            public static (int extraMac, int extraDc) RollSuperStat(int roll100) =>
                roll100 < 50 ? (1, 0) : (0, 1);
        }

        // 细节 226: 大手镯极品生成优先加权物理防御
        public static class BigBraceletSuperStatEngine
        {
            public static (int extraAc, int extraDc) RollSuperStat(int roll100) =>
                roll100 < 50 ? (1, 0) : (0, 1);
        }

        // 细节 227: 铁手镯准确 +1
        public static class IronBraceletAccuracyEngine
        {
            public static int ApplyIronBracelet(int baseAcc, int ironBraceletCount) =>
                baseAcc + ironBraceletCount;
        }

        // 细节 228: 银蛇治愈术增幅 10%
        public static class SilverSnakeHealBoostEngine
        {
            public static int CalculateHeal(int baseHeal, bool hasSilverSnake) =>
                hasSilverSnake ? (int)(baseHeal * 1.10f) : baseHeal;
        }

        // 细节 229: 魔杖雷电术最小伤害保底 5 点
        public static class MagicStaffMinDmgEngine
        {
            public static int EnsureMinDamage(int rolledDamage, bool hasMagicStaff) =>
                hasMagicStaff ? Math.Max(5, rolledDamage) : rolledDamage;
        }

        // 细节 230: 炼狱巨斧重 60，检定战士手部腕力上限
        public static class PurgatoryAxeWeightEngine
        {
            public static (bool canWield, string reason) CheckWield(int handWeight) =>
                handWeight >= 60 ? (true, "") : (false, "腕力不足，无法持握炼狱巨斧！");
        }
    }

    // ========================================================================
    // 盛大官方 1.5 怪物生态与掉落保护规则 (细节 451 ~ 550)
    // ========================================================================
    public static class MonsterEcologyRuleEngine
    {
        // 细节 451/452: 鸡肉鹿肉品质计算
        public static class CreatureEcologyEngine
        {
            public static (int meatQuality, float moveSpeedMultiplier) GetChickenDeathMeat(int struckCount)
            {
                int quality = Math.Clamp(15 - struckCount, 5, 15);
                return (quality, 2.0f);
            }

            public static (float normalCorpseSeconds, float bossCorpseSeconds) CorpseDecayTimers => (60.0f, 300.0f);
        }

        // 细节 534: 掉落物保护 120 秒
        public static class BossDropTreeEngine
        {
            public static (bool hasOwnership, float protectionSeconds) CheckDropOwnership(long killerId, long pickerId, float dropElapsedSeconds)
            {
                if (dropElapsedSeconds >= 120.0f) return (true, 0f);
                bool owner = (killerId == pickerId);
                return (owner, owner ? 0f : (120.0f - dropElapsedSeconds));
            }

            public static (bool success, string dropItem) EvaluateBloodZombieBloodDrink(int roll1000) =>
                roll1000 < 5 ? (true, "血饮") : (false, "");
        }

        // 地表物品拾取归属权威判定
        public static class GroundItemPickupEngine
        {
            public static bool CanPickup(float protectionTimeRemaining, string? ownerName, string pickerName)
            {
                if (protectionTimeRemaining <= 0.0f) return true;
                return string.IsNullOrEmpty(ownerName) || string.Equals(ownerName, pickerName, StringComparison.OrdinalIgnoreCase);
            }
        }
    }

    // 地表物品拾取归属权威判定根级门面
    public static class GroundItemPickupEngine
    {
        public static bool CanPickup(float protectionTimeRemaining, string? ownerName, string pickerName) =>
            MonsterEcologyRuleEngine.GroundItemPickupEngine.CanPickup(protectionTimeRemaining, ownerName, pickerName);
    }

    // ========================================================================
    // 盛大官方 1.5 魔法盾、圣言术与火墙底层战斗规则
    // ========================================================================
    public static class SpellRuleEngine
    {
        // 细节 448: 魔法盾 1 点伤害扣 0.5 点 MP
        public static class MagicShieldAbsorbEngine
        {
            public static (int absorbedHp, int consumedMp) CalculateShieldAbsorb(int incomingDmg, int currentMp)
            {
                int mpCost = (int)(incomingDmg * 0.5f);
                if (currentMp >= mpCost) return (incomingDmg, mpCost);
                int partialDmg = (int)(currentMp / 0.5f);
                return (partialDmg, currentMp);
            }
        }

        // 细节 369: 圣言术一击必杀碎骨化灰判定
        public static class HolyWordExecutionEngine
        {
            public static (float stunDurationSeconds, bool fatalShatter) EvaluateHolyWord(bool isUndead, bool isSuccess)
            {
                if (isUndead && isSuccess) return (0f, true);
                return (0.25f, false);
            }
        }

        // 细节 435: 火墙心跳节律
        public static class FirewallCadenceEngine
        {
            public static (int wallDamageTicks, float intervalSec) GetFirewallDamageCadence() => (2, 0.5f);
        }

        // 官方 1.5 施毒术公式与周期
        public static class PoisonRuleEngine
        {
            public const int PoisonTickIntervalMs = 2500; // 官方 2500ms 一跳
            public static int CalculateGreenPoisonDamage(int sc) => Math.Max(1, 2 + (sc / 10));
        }
    }
}
