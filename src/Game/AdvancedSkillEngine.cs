using System;
using System.Collections.Generic;
using Mir2.Shared.Rules;

namespace Mir2Client.Game;

/// <summary>
/// 盛大官方 1.5 战法道进阶技能表现层映射 (细节 171 ~ 190)
/// 核心规则已 100% 下沉至 Mir2.Shared.Rules.AdvancedCombatRuleEngine 由服务端唯一权威裁决。
/// 客户端仅作为只读代理与表现层适配器，绝不拥有私自裁决权。
/// </summary>
public static class AdvancedSkillEngine
{
    public static class SavageCrateSmashEngine
    {
        public static (bool smashed, int woodParticles) CheckSmash(bool isCrate, int warriorLevel) =>
            AdvancedCombatRuleEngine.SavageCrateSmashEngine.CheckSmash(isCrate, warriorLevel);
    }

    public static class SwordQiAuraPierceEngine
    {
        public static int CalculateDamage(int rawDc, int targetAc, int targetBuffAc, bool isSecondGrid) =>
            AdvancedCombatRuleEngine.SwordQiAuraPierceEngine.CalculateDamage(rawDc, targetAc, targetBuffAc, isSecondGrid);
    }

    public static class HalfMoonSubTargetEngine
    {
        public static (int vampHp, bool applyPoison) EvaluateSubTargetProc(bool isSubTarget, int damage, bool hasRainbowSuit) =>
            AdvancedCombatRuleEngine.HalfMoonSubTargetEngine.EvaluateSubTargetProc(isSubTarget, damage, hasRainbowSuit);
    }

    public static class FireSwordDisruptEngine
    {
        public static (bool fireChargeMaintained, string reason) CheckDisruption(bool isCharged, int incomingDamage, int maxHp) =>
            AdvancedCombatRuleEngine.FireSwordDisruptEngine.CheckDisruption(isCharged, incomingDamage, maxHp);
    }

    public static class AssassinationUndeadEngine
    {
        public static (int stunDurationMs, bool knockback) EvaluateUndeadHit(bool isUndead, bool isAssassinationHit) =>
            AdvancedCombatRuleEngine.AssassinationUndeadEngine.EvaluateUndeadHit(isUndead, isAssassinationHit);
    }

    public static class TameParalysisEngine
    {
        public static (bool tameSuccess, bool paralysisTriggered, float duration) AttemptTameBoss(int monsterLevel, int mageLevel, int roll100) =>
            AdvancedCombatRuleEngine.TameParalysisEngine.AttemptTameBoss(monsterLevel, mageLevel, roll100);
    }

    public static class TamedPetRebellionEngine
    {
        public const float MaxLoyaltyHours = AdvancedCombatRuleEngine.TamedPetRebellionEngine.MaxLoyaltyHours;
        public static (bool isRebelled, string nameColor) CheckRebellion(float survivalHours) =>
            AdvancedCombatRuleEngine.TamedPetRebellionEngine.CheckRebellion(survivalHours);
    }

    public static class FireballKnockbackEngine
    {
        public static (bool knockback, int stepX, int stepY) CalculateKnockback(bool isLightweight, int casterX, int casterY, int targetX, int targetY) =>
            AdvancedCombatRuleEngine.FireballKnockbackEngine.CalculateKnockback(isLightweight, casterX, casterY, targetX, targetY);
    }

    public static class IceExtinguishFirewallEngine
    {
        public static float ApplyIceStormImpact(float remainingFirewallSeconds) =>
            AdvancedCombatRuleEngine.IceExtinguishFirewallEngine.ApplyIceStormImpact(remainingFirewallSeconds);
    }

    public static class MagicShieldHolyPierceEngine
    {
        public static (int absorbedDmg, int piercedDirectDmg) CalculateHolyPierce(int rawDamage, bool isHoly) =>
            AdvancedCombatRuleEngine.MagicShieldHolyPierceEngine.CalculateHolyPierce(rawDamage, isHoly);
    }

    public static class TeleportManaSurgeEngine
    {
        public const int BaseMpCost = AdvancedCombatRuleEngine.TeleportManaSurgeEngine.BaseMpCost;
        public static int CalculateSurgeCost(int continuousCastCount, float secondsSinceLastCast) =>
            AdvancedCombatRuleEngine.TeleportManaSurgeEngine.CalculateSurgeCost(continuousCastCount, secondsSinceLastCast);
    }

    public static class PoisonInvisibilityEngine
    {
        public static bool DoesPoisonBreakInvisibility() =>
            AdvancedCombatRuleEngine.PoisonInvisibilityEngine.DoesPoisonBreakInvisibility();
    }

    public static class TrueSightEngine
    {
        public static bool CanDetectInvisible(string monsterName) =>
            AdvancedCombatRuleEngine.TrueSightEngine.CanDetectInvisible(monsterName);
    }

    public static class AuraTargetLimitEngine
    {
        public const int MaxAuraTargets = AdvancedCombatRuleEngine.AuraTargetLimitEngine.MaxAuraTargets;
        public static List<T> FilterAuraTargets<T>(List<T> nearbyAllies) =>
            AdvancedCombatRuleEngine.AuraTargetLimitEngine.FilterAuraTargets(nearbyAllies);
    }

    public static class TrapMonsterEngine
    {
        public static (bool isTrapped, bool brokenByDamage) EvaluateTrap(bool hasTrapAura, int incomingDamage) =>
            AdvancedCombatRuleEngine.TrapMonsterEngine.EvaluateTrap(hasTrapAura, incomingDamage);
    }

    public static class GroupInvisibilityEngine
    {
        public static bool IsIn3x3Area(int casterX, int casterY, int targetX, int targetY) =>
            AdvancedCombatRuleEngine.GroupInvisibilityEngine.IsIn3x3Area(casterX, casterY, targetX, targetY);
    }

    public static class SkeletonEvolutionEngine
    {
        public enum SkeletonForm { SmallAxe, HeavyAxe, BlueBone, RedBoneReinforced }

        public static SkeletonForm GetSkeletonForm(int skillLevel)
        {
            var sharedForm = AdvancedCombatRuleEngine.SkeletonEvolutionEngine.GetSkeletonForm(skillLevel);
            return (SkeletonForm)(int)sharedForm;
        }
    }

    public static class ShinsuEvolutionEngine
    {
        public static (string flameColorHex, float scale) GetShinsuStats(int petLevel) =>
            AdvancedCombatRuleEngine.ShinsuEvolutionEngine.GetShinsuStats(petLevel);
    }

    public static class MindInsightEngine
    {
        public const float DurationSeconds = AdvancedCombatRuleEngine.MindInsightEngine.DurationSeconds;
        public static (bool showDetails, string hpRatioText) GetInsightDisplay(float elapsed, int curHp, int maxHp) =>
            AdvancedCombatRuleEngine.MindInsightEngine.GetInsightDisplay(elapsed, curHp, maxHp);
    }

    // 盛大官方 1.5 核心真值: 法师 12 级原生技能【抗拒火环】(Repulsion Ring)
    public static class RepulsionRingEngine
    {
        public static (bool repelled, int targetNewX, int targetNewY) TryRepel(
            int selfLevel, int targetLevel, int selfX, int selfY, int targetX, int targetY) =>
            AdvancedCombatRuleEngine.RepulsionRingEngine.TryRepel(selfLevel, targetLevel, selfX, selfY, targetX, targetY);
    }
}
