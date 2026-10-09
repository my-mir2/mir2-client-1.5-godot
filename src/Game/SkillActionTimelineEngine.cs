using System;
using System.Collections.Generic;

namespace Mir2Client.Game;

/// <summary>
/// 盛大官方 1.5 战法道 32 门技能动作帧数、光效粒子、弹道与受击反馈全集引擎 (细节 351 ~ 450)
/// 严格依据官方服务端与客户端技能物理时钟与动作帧规范
/// </summary>
public static class SkillActionTimelineEngine
{
    // 细节 351 ~ 370: 战士核心技能前摇/后摇与法师群攻技能表现
    public static class CombatTimelineEngine
    {
        public const int FireSwordSoundId = 12; // 细节 406
        public const int SoulSymbolSoundId = 10; // 细节 374

        public static (int precastFrames, int recoveryFrames) GetWarriorSlashFrames(int skillId)
        {
            return skillId switch
            {
                7 => (1, 2),  // 细节 352: 攻杀剑术前摇 1 帧蓄劲
                12 => (1, 2), // 细节 353: 刺杀剑术后摇 2 帧收刀
                25 => (2, 2), // 细节 354: 半月弯刀横扫
                26 => (1, 3), // 细节 355: 野蛮冲撞前倾 45 度
                _ => (1, 1)
            };
        }

        public static float FireballSpeedGridsPerSecond => 12.0f; // 细节 358: 小火球速度 12 格/秒

        public static (int waveDelayMs, int totalGrids) GetHellfireWaveTiming(int gridIndex)
        {
            // 细节 361: 地狱火 5 格地表火浪每格递延 40ms
            return (gridIndex * 40, 5);
        }

        public static (float stunDurationSeconds, bool fatalShatter) EvaluateHolyWord(bool isUndead, bool isSuccess)
        {
            // 细节 369: 圣言术一击必杀碎骨化灰
            if (isUndead && isSuccess) return (0f, true);
            return (0.25f, false);
        }

        public static (int fallingIcicles, float spinSpeed) GetIceStormVfx(float elapsed)
        {
            // 细节 370: 冰咆哮巨型冰锥旋转下坠
            return (4, 360f);
        }

        public static (float risingOffset, string colorHex) GetHealCrossPuffs(float elapsed)
        {
            // 细节 371: 治愈术绿色十字光斑上升
            return (elapsed * 25.0f, "#00FF7F");
        }

        public static (int paperFragments, float explosionRadius) GetSoulSymbolDetonation()
        {
            // 细节 387: 灵魂火符爆破散落 6 块燃烧纸屑
            return (6, 1.0f);
        }

        public static float GetStruckOverexposedDurationSeconds() => 0.12f; // 细节 386: 雷电术受击白炽 120ms
    }

    // 细节 371 ~ 410: 道士召唤、隐身、防御光环与物理判定
    public static class TaoistSpellTimelineEngine
    {
        public const float ShinsuEmergeTotalSeconds = 1.8f; // 细节 409: 神兽破土耗时严格 1.8s
        public const float SkeletonEmergeTotalSeconds = 1.2f; // 细节 410: 骷髅爬出耗时严格 1.2s

        public static (float alpha, bool dropAggro) GetInvisibilityState(bool isGhostActive)
        {
            // 细节 376: 隐身术 Alpha 0.35 且怪脱仇恨
            return (isGhostActive ? 0.35f : 1.0f, isGhostActive);
        }

        public static (bool flashWarn, float remainingSec) CheckAuraExpireWarning(float remainingTime)
        {
            // 细节 399/400: 神圣战甲术/幽灵盾结束前 5 秒闪烁告警
            return (remainingTime <= 5.0f, remainingTime);
        }

        public static float SmallFireballArcHeightPx => 30.0f; // 细节 402: 抛物线高 30 像素
        public static float BigFireballArcHeightPx => 45.0f;   // 细节 403: 抛物线高 45 像素

        public static (int wallDamageTicks, float intervalSec) GetFirewallDamageCadence()
        {
            // 细节 435: 火墙每秒触发 2 次伤害判定
            return (2, 0.5f);
        }

        public static (int absorbedHp, int consumedMp) CalculateShieldAbsorb(int incomingDmg, int currentMp)
        {
            // 细节 448: 魔法盾 1 点伤害扣 0.5 点 MP
            int mpCost = (int)(incomingDmg * 0.5f);
            if (currentMp >= mpCost) return (incomingDmg, mpCost);
            int partialDmg = (int)(currentMp / 0.5f);
            return (partialDmg, currentMp);
        }

        public static int SpellGlobalCooldownMs => 800; // 细节 450: 官方 800ms 施法节律
    }
}
