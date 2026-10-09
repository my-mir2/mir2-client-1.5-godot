using System;
using System.Collections.Generic;

namespace Mir2Client.Game;

/// <summary>
/// 盛大官方 1.5 战法道进阶技能、辅助光环与特殊状态抗性引擎 (细节 171 ~ 190)
/// 严格依据官方服务端机制与客户端表现规范
/// </summary>
public static class AdvancedSkillEngine
{
    // 细节 171: 战士野蛮冲撞撞碎地表木箱并飞溅碎屑粒子
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

    // 细节 190: 气功波推开周围 8 方向 1 格低于自身等级目标
    public static class QiGongRepelEngine
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
}
