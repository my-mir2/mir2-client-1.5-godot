using System;
using System.Collections.Generic;
using System.Linq;

namespace Mir2Client.Game;

/// <summary>
/// 盛大官方 1.5 实体纸娃娃、怪物 AI 行为、尸体与场景渲染引擎 (细节 91 ~ 110)
/// 严格依据官方客户端与服务端实证
/// </summary>
public static class MonsterBehaviorEngine
{
    // --------------------------------------------------------------------------------
    // 细节 91: 男女角色 8 方向行走与奔跑时衣服下摆与斗篷惯性摆动
    // --------------------------------------------------------------------------------
    public static class CapeSwingEngine
    {
        public static float CalculateSwingAngle(bool isRunning, float cycleProgress)
        {
            float maxAngle = isRunning ? 25.0f : 12.0f; // 奔跑时斗篷摆动幅度增大
            return maxAngle * MathF.Sin(cycleProgress * 6.28f);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 92: 角色佩戴头盔后发型自动隐藏 (避免穿模)
    // --------------------------------------------------------------------------------
    public static class HelmetHairEngine
    {
        public static bool ShouldShowHair(int helmetItemId)
        {
            return helmetItemId <= 0; // 装备头盔 (helmetItemId > 0) 时隐藏头发
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 93: 武器类型持握与背负姿态 (单手剑/双手重兵器/法杖/道剑)
    // --------------------------------------------------------------------------------
    public static class WeaponGripEngine
    {
        public enum WeaponType { OneHandSword, TwoHandHeavy, MagicStaff, TaoistSword }
        public enum GripPose { SingleHand, TwoHandsHeavy, StaffHold, ReverseHold }

        public static GripPose GetGripPose(WeaponType weaponType)
        {
            return weaponType switch
            {
                WeaponType.OneHandSword => GripPose.SingleHand,
                WeaponType.TwoHandHeavy => GripPose.TwoHandsHeavy,
                WeaponType.MagicStaff => GripPose.StaffHold,
                WeaponType.TaoistSword => GripPose.ReverseHold,
                _ => GripPose.SingleHand
            };
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 94: 怪物受击后仰硬直 250ms 与受击声相方位判定
    // --------------------------------------------------------------------------------
    public static class StruckStunEngine
    {
        public const float StruckDurationSeconds = 0.25f; // 严格 250ms

        public static (bool isInStun, float audioPan) EvaluateStruck(float elapsedSinceHit, int monsterX, int playerX)
        {
            bool inStun = elapsedSinceHit < StruckDurationSeconds;
            // monsterX 相对 playerX 的偏移计算声相 pan [-1.0, 1.0]
            float pan = Math.Clamp((monsterX - playerX) / 10.0f, -1.0f, 1.0f);
            return (inStun, pan);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 95: 鹿与鸡被动生物受惊吓反向逃跑寻路
    // --------------------------------------------------------------------------------
    public static class HerbivoreFleeEngine
    {
        public static (int fleeX, int fleeY) CalculateFleeStep(int selfX, int selfY, int threatX, int threatY)
        {
            int dx = selfX - threatX;
            int dy = selfY - threatY;
            int stepX = dx == 0 ? 0 : (dx > 0 ? 1 : -1);
            int stepY = dy == 0 ? 0 : (dy > 0 ? 1 : -1);
            return (selfX + stepX, selfY + stepY);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 96: 稻草人死后化为一堆燃烧的草木灰烬动画
    // --------------------------------------------------------------------------------
    public static class ScarecrowAshEngine
    {
        public static (bool isAshPile, float smokeAlpha) EvaluateDeath(float elapsedSinceDead)
        {
            bool isAsh = elapsedSinceDead >= 1.2f;
            float smoke = Math.Clamp(1.0f - elapsedSinceDead / 3.0f, 0f, 1f);
            return (isAsh, smoke);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 97: 半兽人倒地后重型石斧砸落地面金属撞击声
    // --------------------------------------------------------------------------------
    public static class OrcAxeDropEngine
    {
        public const int AxeDropSoundId = 12;

        public static (bool playDropSound, float dropDelay) GetAxeDropTiming()
        {
            return (true, 0.4f); // 死亡动画进行到 0.4 秒石斧脱手砸地
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 98: 骷髅战士倒地散落为一地白色骨架碎块并永久保留在地面
    // --------------------------------------------------------------------------------
    public static class SkeletonBonesEngine
    {
        public const int BonesCorpseWilFrame = 55;

        public static (int corpseFrame, bool persistentUntilDespawn) GetCorpseState()
        {
            return (BonesCorpseWilFrame, true);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 99: 僵尸死亡后概率触发“二次爬起复活”假死状态机
    // --------------------------------------------------------------------------------
    public static class ZombieReviveEngine
    {
        public static bool CheckZombieRevive(int rollPercent, int threshold = 30)
        {
            return rollPercent < threshold; // 30% 概率二次爬起
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 100: 尸王钻出地面时地面石板破裂四溅碎石微特效
    // --------------------------------------------------------------------------------
    public static class CorpseKingEmergeEngine
    {
        public static (int rockParticles, float groundCrackScale) EvaluateEmerge(float progress)
        {
            progress = Math.Clamp(progress, 0f, 1f);
            int rocks = (int)(15 * progress);
            return (rocks, progress);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 101: 沃玛教主狂暴状态 (血量低于 30% 移动速度翻倍，全身红色闪电)
    // --------------------------------------------------------------------------------
    public static class WomaBerserkEngine
    {
        public static (bool isBerserk, float speedMultiplier, string auraColor) CheckBerserk(int currentHp, int maxHp)
        {
            bool berserk = currentHp > 0 && ((float)currentHp / maxHp) <= 0.3f;
            return (berserk, berserk ? 2.0f : 1.0f, berserk ? "#FF0000" : "#000000");
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 102: 祖玛卫士由石化雕像破壳苏醒转为活体实体的石皮脱落动画
    // --------------------------------------------------------------------------------
    public static class ZumaAwakenEngine
    {
        public static (bool isAwakened, float stoneCrustShatterProgress) CheckAwaken(int playerDist)
        {
            bool awake = playerDist <= 3; // 进入 3 格视距破壳
            return (awake, awake ? 1.0f : 0.0f);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 103: 白野猪移动震屏微震动 (Camera Shake)
    // --------------------------------------------------------------------------------
    public static class WhiteBoarScreenShakeEngine
    {
        public static (float shakeX, float shakeY) GetStepShakeOffset(float stepElapsed)
        {
            if (stepElapsed < 0 || stepElapsed > 0.1f) return (0f, 0f);
            return (0f, 2.0f * MathF.Sin(stepElapsed * 62.8f)); // 上下 2 像素轻震
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 104: 地表水洼瓦片角色踩踏走过溅起水花涟漪
    // --------------------------------------------------------------------------------
    public static class WaterPuddleSplashEngine
    {
        public static (float rippleRadius, float alpha) EvaluateSplash(float elapsed)
        {
            if (elapsed < 0 || elapsed > 0.5f) return (0f, 0f);
            float radius = 5.0f + elapsed * 20.0f;
            float alpha = Math.Clamp(1.0f - elapsed / 0.5f, 0f, 1f);
            return (radius, alpha);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 105: 地表毒池瓦片走入冒绿色泡泡并造成 1 点环境伤害
    // --------------------------------------------------------------------------------
    public static class PoisonSwampEngine
    {
        public static (bool takeEnvDamage, int damage, int bubbleCount) EvaluateStep(bool isInPoisonTile, float secondsInTile)
        {
            if (!isInPoisonTile) return (false, 0, 0);
            bool tick = ((int)secondsInTile) > 0 && Math.Abs(secondsInTile - (int)secondsInTile) < 0.1f;
            return (tick, tick ? 1 : 0, 3);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 106: 沙漠土城黄沙漫天风滚草与随风飘散沙尘
    // --------------------------------------------------------------------------------
    public static class DesertSandstormEngine
    {
        public static (float windX, float tumbleweedOffset) GetWindEffect(float time)
        {
            float wind = 15.0f + 5.0f * MathF.Sin(time);
            float tumble = (time * 40.0f) % 800.0f;
            return (wind, tumble);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 107: 森林瓦片树荫下半透明黑色椭圆投影
    // --------------------------------------------------------------------------------
    public static class EntityShadowEngine
    {
        public static (int radiusX, int radiusY, float alpha) CalculateShadow(int bodySize, bool underTree)
        {
            int rx = 12 * bodySize;
            int ry = 6 * bodySize;
            float alpha = underTree ? 0.6f : 0.35f;
            return (rx, ry, alpha);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 108: 洞穴顶端水滴滴落地面水渍渐隐
    // --------------------------------------------------------------------------------
    public static class CaveDripEngine
    {
        public static (bool isFading, float stainAlpha) EvaluateDripStain(float elapsedSinceDrop)
        {
            if (elapsedSinceDrop > 2.0f) return (false, 0f);
            return (true, 1.0f - elapsedSinceDrop / 2.0f);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 109: 同屏多尸体堆叠 Y-Sort 深度层级排序
    // --------------------------------------------------------------------------------
    public static class CorpseLayerSortEngine
    {
        public record Corpse(long Id, int X, int Y);

        public static List<Corpse> SortCorpses(IEnumerable<Corpse> list)
        {
            // Y 越小越先渲染 (在底层)，Y 越大越后渲染 (遮挡在上面)
            return list.OrderBy(c => c.Y).ThenBy(c => c.X).ToList();
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 110: 实体离开视野 (SM 11) 200ms 半透明平滑淡出
    // --------------------------------------------------------------------------------
    public static class EntityFadeOutEngine
    {
        public const float FadeDuration = 0.2f;

        public static (float alpha, bool isFinished) CalculateFade(float elapsed)
        {
            if (elapsed >= FadeDuration) return (0f, true);
            return (1.0f - elapsed / FadeDuration, false);
        }
    }
}
