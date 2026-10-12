using System;
using Mir2.Shared.Rules;

namespace Mir2Client.Game;

/// <summary>
/// 盛大官方 1.5 怪物生态与掉落表现层适配器 (细节 451 ~ 550)
/// 核心掉落权属与爆率算法已 100% 下沉至 Mir2.Shared.Rules.AdvancedCombatRuleEngine.MonsterEcologyRuleEngine 由服务端唯一权威裁决。
/// 客户端在此仅作为视觉表现与几何动画映射。
/// </summary>
public static class MonsterEcologyDropEngine
{
    public static class CreatureEcologyEngine
    {
        public static (int meatQuality, float moveSpeedMultiplier) GetChickenDeathMeat(int struckCount) =>
            AdvancedCombatRuleEngine.MonsterEcologyRuleEngine.CreatureEcologyEngine.GetChickenDeathMeat(struckCount);

        public static (float precastSec, int stunMs) GetOrcPrecast(string orcType) => (0.4f, 250);

        public static (float normalCorpseSeconds, float bossCorpseSeconds) CorpseDecayTimers =>
            AdvancedCombatRuleEngine.MonsterEcologyRuleEngine.CreatureEcologyEngine.CorpseDecayTimers;

        public static (int minHearingRange, int maxVisionRange) GetMonsterSenses(bool isBoss) =>
            isBoss ? (6, 12) : (4, 8);

        public static int WomaGuardDualSlashIntervalMs => 600;
    }

    public static class BossDropTreeEngine
    {
        public static (float innerRadius, float outerRadius) GetBossDropRadii() => (2.0f, 4.0f);

        public static (bool hasOwnership, float protectionSeconds) CheckDropOwnership(long killerId, long pickerId, float dropElapsedSeconds) =>
            AdvancedCombatRuleEngine.MonsterEcologyRuleEngine.BossDropTreeEngine.CheckDropOwnership(killerId, pickerId, dropElapsedSeconds);

        public static (bool success, string dropItem) EvaluateBloodZombieBloodDrink(int roll1000) =>
            AdvancedCombatRuleEngine.MonsterEcologyRuleEngine.BossDropTreeEngine.EvaluateBloodZombieBloodDrink(roll1000);

        public static (bool harvestSuccess, float progressSec) EvaluateButcherCarving(bool hasKnife, float standingSeconds) =>
            (hasKnife && standingSeconds >= 3.0f, Math.Min(3.0f, standingSeconds));
    }
}
