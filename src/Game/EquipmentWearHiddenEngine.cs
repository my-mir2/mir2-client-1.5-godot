using System;
using Mir2.Shared.Rules;

namespace Mir2Client.Game;

/// <summary>
/// 盛大官方 1.5 装备磨损与暗属性客户端表现层适配器 (细节 211 ~ 230)
/// 核心磨损公式与属性检定已 100% 下沉至 Mir2.Shared.Rules.AdvancedCombatRuleEngine.EquipmentWearRuleEngine 由服务端唯一权威裁决。
/// 客户端在此仅作为只读代理与 UI 表现映射 (如红字耐久告警)。
/// </summary>
public static class EquipmentWearHiddenEngine
{
    public static class ArmorDuraWearEngine
    {
        public static (int newDura, bool duraReduced) CheckWear(int currentDura, bool isCriticalHit, int roll100) =>
            AdvancedCombatRuleEngine.EquipmentWearRuleEngine.ArmorDuraWearEngine.CheckWear(currentDura, isCriticalHit, roll100);
    }

    public static class WeaponHardObstacleWearEngine
    {
        public static (int newDura, bool chipped) StrikeHardObstacle(int currentDura, bool isHardObstacle) =>
            AdvancedCombatRuleEngine.EquipmentWearRuleEngine.WeaponHardObstacleWearEngine.StrikeHardObstacle(currentDura, isHardObstacle);
    }

    public static class LowDuraRedAlertEngine
    {
        public const int WarningThreshold = 100;
        public static (bool isRedAlert, string textColor) EvaluateDuraWarning(int dura) =>
            dura < WarningThreshold ? (true, "#FF0000") : (false, "#FFFFFF");
    }

    public static class MysticHelmetLockEngine
    {
        public static (bool canTakeOff, string message) EvaluateTakeOff(int playerDc, int playerMc, int requiredDc, int requiredMc) =>
            AdvancedCombatRuleEngine.EquipmentWearRuleEngine.MysticHelmetLockEngine.EvaluateTakeOff(playerDc, playerMc, requiredDc, requiredMc);
    }

    public static class PrayerHelmetShatterEngine
    {
        public static (bool isShattered, bool droppedOnGround) OnPlayerDeath(bool isWearingPrayerHelmet) =>
            AdvancedCombatRuleEngine.EquipmentWearRuleEngine.PrayerHelmetShatterEngine.OnPlayerDeath(isWearingPrayerHelmet);
    }

    public static class MemoryNecklaceLuckWeightEngine
    {
        public static int RollLuck(int roll1000) =>
            AdvancedCombatRuleEngine.EquipmentWearRuleEngine.MemoryNecklaceLuckWeightEngine.RollLuck(roll1000);
    }

    public static class DevilBellTameEngine
    {
        public static float ApplyTameBonus(float baseRate, bool hasDevilBell) =>
            AdvancedCombatRuleEngine.EquipmentWearRuleEngine.DevilBellTameEngine.ApplyTameBonus(baseRate, hasDevilBell);
    }

    public static class DragonBraceletShieldBreakEngine
    {
        public static bool CheckShieldBreak(bool hasDragonBracelet, int roll100) =>
            AdvancedCombatRuleEngine.EquipmentWearRuleEngine.DragonBraceletShieldBreakEngine.CheckShieldBreak(hasDragonBracelet, roll100);
    }

    public static class SoulBraceletAccuracyEngine
    {
        public static int GetAccuracyBonus(bool hasSoulBracelet) => hasSoulBracelet ? 2 : 0;
    }

    public static class RubyShieldRecoveryEngine
    {
        public static float CalculateShieldStun(float baseStun, bool hasRubyRing) =>
            AdvancedCombatRuleEngine.EquipmentWearRuleEngine.RubyShieldRecoveryEngine.CalculateShieldStun(baseStun, hasRubyRing);
    }

    public static class PlatinumArmorBoostEngine
    {
        public static int GetDefenseBonus(bool hasPlatinumRing) => hasPlatinumRing ? 2 : 0;
    }

    public static class IronRingFirewallDefEngine
    {
        public static int ReduceFirewallDamage(int damage, int ironRingCount) =>
            AdvancedCombatRuleEngine.EquipmentWearRuleEngine.IronRingFirewallDefEngine.ReduceFirewallDamage(damage, ironRingCount);
    }

    public static class DemonSubdueRingEngine
    {
        public static int ApplyDemonReduction(int damage, bool isDemonMonster, bool hasDemonSubdueRing) =>
            AdvancedCombatRuleEngine.EquipmentWearRuleEngine.DemonSubdueRingEngine.ApplyDemonReduction(damage, isDemonMonster, hasDemonSubdueRing);
    }

    public static class CoralRingWeightEngine
    {
        public static (int weight, int minDc, int maxDc) GetStats() =>
            AdvancedCombatRuleEngine.EquipmentWearRuleEngine.CoralRingWeightEngine.GetStats();
    }

    public static class GoldBraceletSuperStatEngine
    {
        public static (int extraMac, int extraDc) RollSuperStat(int roll100) =>
            AdvancedCombatRuleEngine.EquipmentWearRuleEngine.GoldBraceletSuperStatEngine.RollSuperStat(roll100);
    }

    public static class BigBraceletSuperStatEngine
    {
        public static (int extraAc, int extraDc) RollSuperStat(int roll100) =>
            AdvancedCombatRuleEngine.EquipmentWearRuleEngine.BigBraceletSuperStatEngine.RollSuperStat(roll100);
    }

    public static class IronBraceletAccuracyEngine
    {
        public static int ApplyIronBracelet(int baseAcc, int ironBraceletCount) =>
            AdvancedCombatRuleEngine.EquipmentWearRuleEngine.IronBraceletAccuracyEngine.ApplyIronBracelet(baseAcc, ironBraceletCount);
    }

    public static class SilverSnakeHealBoostEngine
    {
        public static int CalculateHeal(int baseHeal, bool hasSilverSnake) =>
            AdvancedCombatRuleEngine.EquipmentWearRuleEngine.SilverSnakeHealBoostEngine.CalculateHeal(baseHeal, hasSilverSnake);
    }

    public static class MagicStaffMinDmgEngine
    {
        public static int EnsureMinDamage(int rolledDamage, bool hasMagicStaff) =>
            AdvancedCombatRuleEngine.EquipmentWearRuleEngine.MagicStaffMinDmgEngine.EnsureMinDamage(rolledDamage, hasMagicStaff);
    }

    public static class PurgatoryAxeWeightEngine
    {
        public static (bool canWield, string reason) CheckWield(int handWeight) =>
            AdvancedCombatRuleEngine.EquipmentWearRuleEngine.PurgatoryAxeWeightEngine.CheckWield(handWeight);
    }
}
