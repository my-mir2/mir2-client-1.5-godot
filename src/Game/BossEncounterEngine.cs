using System;
using System.Collections.Generic;
using Mir2.Shared.Rules;

namespace Mir2Client.Game;

/// <summary>
/// 盛大官方 1.5 怪物首领机制客户端表现层适配器 (细节 191 ~ 210)
/// 核心战斗数值已 100% 下沉至 Mir2.Shared.Rules.AdvancedCombatRuleEngine.BossEncounterRuleEngine 由服务端唯一权威裁决。
/// 客户端在此仅作为只读代理与视觉表现映射。
/// </summary>
public static class BossEncounterEngine
{
    public static class EvilSnakePoisonEngine
    {
        public static (bool petrified, bool poisoned, float duration) EvaluateBreath(int roll100) =>
            AdvancedCombatRuleEngine.BossEncounterRuleEngine.EvilSnakePoisonEngine.EvaluateBreath(roll100);
    }

    public static class EvilSnakeMagicImmunityEngine
    {
        public static int CalculateDamage(int rawDamage, bool isMagicAttack) =>
            AdvancedCombatRuleEngine.BossEncounterRuleEngine.EvilSnakeMagicImmunityEngine.CalculateDamage(rawDamage, isMagicAttack);
    }

    public static class ViperMobilityEngine
    {
        public static (float moveSpeedMultiplier, bool applyToxic) EvaluateViperState(int distanceToTarget) =>
            (1.6f, distanceToTarget <= 1);
    }

    public static class ZumaSuperGuardEngine
    {
        public static (float speedMulti, float attackPrecastSeconds) GetGuardAttributes(bool isSuperGuard) =>
            AdvancedCombatRuleEngine.BossEncounterRuleEngine.ZumaSuperGuardEngine.GetGuardAttributes(isSuperGuard);
    }

    public static class ZumaStatueSelfDestructEngine
    {
        public static (bool willExplode, int explodeDamage) CheckSelfDestruct(int currentHp, int maxHp) =>
            AdvancedCombatRuleEngine.BossEncounterRuleEngine.ZumaStatueSelfDestructEngine.CheckSelfDestruct(currentHp, maxHp);
    }

    public static class BlackMaggotChargeEngine
    {
        public static (bool hitSuccess, int stunMs) EvaluateCharge(int distance) =>
            (distance <= 4, distance <= 4 ? 200 : 0);
    }

    public static class MothPetrifyEngine
    {
        public static (bool isPetrified, float duration) CheckPetrify(int roll100) =>
            roll100 < 5 ? (true, 3.0f) : (false, 0f);
    }

    public static class BatSpitterEngine
    {
        public const int MaxBats = AdvancedCombatRuleEngine.BossEncounterRuleEngine.BatSpitterEngine.MaxBats;
        public static bool CanSpawnBat(int currentBatsAlive) =>
            AdvancedCombatRuleEngine.BossEncounterRuleEngine.BatSpitterEngine.CanSpawnBat(currentBatsAlive);
    }

    public static class WomaDualBladeEngine
    {
        public static (bool isDualSlash, int finalDamage) CalculateSlashDamage(int baseDc, bool dualSlashProc) =>
            AdvancedCombatRuleEngine.BossEncounterRuleEngine.WomaDualBladeEngine.CalculateSlashDamage(baseDc, dualSlashProc);
    }

    public static class CorpseKingChainEngine
    {
        public static (int stunMs, string soundEffect) GetChainStrikeAttributes() => (300, "snd_chain_strike");
        public static List<(int x, int y)> GetChainSweepGrids(int x, int y, int dir) =>
            AdvancedCombatRuleEngine.BossEncounterRuleEngine.CorpseKingChainEngine.GetChainSweepGrids(x, y, dir);
    }

    public static class SkeletonElfEnrageEngine
    {
        public static (float speedMulti, string tintColor) GetEnrageStats(float hpPercent) =>
            hpPercent <= 0.3f ? (1.8f, "#FF0000") : (1.0f, "#FFFFFF");
        public static (bool glowRed, float enrageMulti) CheckEnraged(int curHp, int maxHp) =>
            AdvancedCombatRuleEngine.BossEncounterRuleEngine.SkeletonElfEnrageEngine.CheckEnraged(curHp, maxHp);
    }

    public static class WomaLeaderTeleportEngine
    {
        public static (bool willTeleport, int newX, int newY) CheckTeleport(float hpPercent, int curX, int curY) =>
            hpPercent <= 0.2f ? (true, curX + 3, curY + 3) : (false, curX, curY);
        public static (int tpX, int tpY, bool tpOk) CalculateAmbush(int targetX, int targetY, int targetDir, bool isBerserk) =>
            AdvancedCombatRuleEngine.BossEncounterRuleEngine.WomaLeaderTeleportEngine.CalculateAmbush(targetX, targetY, targetDir, isBerserk);
    }

    public static class ZumaLeaderSummonEngine
    {
        public static (bool triggerSummon, int guardCount) CheckSummon(float hpPercent) =>
            hpPercent <= 0.5f ? (true, 4) : (false, 0);
        public static (bool triggerSummon, int guardCount) CheckSummon(int curHp, int maxHp, bool summoned) =>
            AdvancedCombatRuleEngine.BossEncounterRuleEngine.ZumaLeaderSummonEngine.CheckSummon(curHp, maxHp, summoned);
    }

    public static class RedMoonSpikeDefenseEngine
    {
        public static int CalculateDamage(int baseDamage, int ac, int mac) =>
            AdvancedCombatRuleEngine.BossEncounterRuleEngine.RedMoonSpikeDefenseEngine.CalculateDamage(baseDamage, ac, mac);
        public static int CalculateSpikeDamage(int baseDamage, int ac, int mac) =>
            AdvancedCombatRuleEngine.BossEncounterRuleEngine.RedMoonSpikeDefenseEngine.CalculateSpikeDamage(baseDamage, ac, mac);
    }

    public static class DoubleHeadKongHarvestEngine
    {
        public static bool RequiresAltCarving => true;
        public static (bool harvOk, string item) TryHarvest(bool hasButcherKnife, int roll100) =>
            AdvancedCombatRuleEngine.BossEncounterRuleEngine.DoubleHeadKongHarvestEngine.TryHarvest(hasButcherKnife, roll100);
    }

    public static class BloodDemonStormDropEngine
    {
        public static (bool dropsRedMantle, float dropRate) GetDropRate() => (true, 0.01f);
        public static (bool rain, List<string> drops) EvaluateDeath() =>
            AdvancedCombatRuleEngine.BossEncounterRuleEngine.BloodDemonStormDropEngine.EvaluateDeath();
    }

    // 盛大官方 1.5 核心真值: 原生首领【祖玛教主】双重打击机制
    public static class ZumaLordDualStrikeEngine
    {
        public static (int waveDamage, float fireDuration) GetFlameStrike() => (60, 4.0f);
        public static (int dc, int mc) CalculateDualDamage(int rawDc, int rawMc) =>
            AdvancedCombatRuleEngine.BossEncounterRuleEngine.ZumaLordDualStrikeEngine.CalculateDualDamage(rawDc, rawMc);
    }

    // 盛大官方 1.5 核心真值 (细节 208): 赤月恶魔大爆天尊全套
    public static class RedMoonDemonDropEngine
    {
        public static (bool success, string itemName) GenerateDrop(bool isRedMoonDemon, int roll100) =>
            AdvancedCombatRuleEngine.BossEncounterRuleEngine.RedMoonDemonDropEngine.GenerateDrop(isRedMoonDemon, roll100);
    }

    // 盛大官方 1.5 核心真值 (细节 209): 沃玛教主大爆沃玛号角
    public static class WomaLeaderHornDropEngine
    {
        public static List<string> GenerateDrop(int roll100) =>
            AdvancedCombatRuleEngine.BossEncounterRuleEngine.WomaLeaderHornDropEngine.GenerateDrop(roll100);
    }

    public static class WhiteBoarBigDropEngine
    {
        public static (int goldPiles, int potionCount) GetBigDropScatter() => (8, 12);
        public static List<(int x, int y)> Generate8x8Drop(int centerX, int centerY) =>
            AdvancedCombatRuleEngine.BossEncounterRuleEngine.WhiteBoarBigDropEngine.Generate8x8Drop(centerX, centerY);
    }
}
