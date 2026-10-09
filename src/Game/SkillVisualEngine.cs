using System;
using System.Collections.Generic;

namespace Mir2Client.Game;

/// <summary>
/// 盛大官方 1.5 战法道技能表现、动作前摇、光效与音效调度引擎 (细节 71 ~ 90)
/// 严格依据官方客户端与服务端技能权威规范
/// </summary>
public static class SkillVisualEngine
{
    // --------------------------------------------------------------------------------
    // 细节 71: 战士普通攻击挥砍刀尖银白残影拖尾动画 (3 帧半透明渐隐: 0.8, 0.5, 0.2)
    // --------------------------------------------------------------------------------
    public static class SlashTrailEngine
    {
        public static float[] GetTrailAlphas(int frameIndex)
        {
            return new float[] { 0.8f, 0.5f, 0.2f };
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 72: 战士攻杀剑术刀刃金黄强光与金属爆鸣音效 (Sound 1)
    // --------------------------------------------------------------------------------
    public static class AssassinationVisualEngine
    {
        public const int SoundId = 1; // 金属清脆爆鸣
        public const string GlowColorHex = "#FFD700"; // 金黄色

        public static (bool triggered, int soundId, string glowColor) EvaluateAssassination(bool isSkillActive, int attackCounter)
        {
            if (!isSkillActive) return (false, 0, "");
            bool trigger = (attackCounter % 4) == 0; // 每 4 刀触发一次攻杀
            return (trigger, trigger ? SoundId : 0, trigger ? GlowColorHex : "");
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 73: 战士刺杀剑术第二格剑气 (Blue Sword Qi) 亮蓝细长光柱
    // --------------------------------------------------------------------------------
    public static class SwordQiEngine
    {
        public const string BeamColorHex = "#00BFFF"; // 亮蓝

        public static (int targetX, int targetY, bool isSecondGrid, bool ignoreDefense) CalculateSecondGridQi(
            int casterX, int casterY, int dir)
        {
            // dir: 0=Up, 1=UpRight, 2=Right, 3=DownRight, 4=Down, 5=DownLeft, 6=Left, 7=UpLeft
            int[] dx = { 0, 1, 1, 1, 0, -1, -1, -1 };
            int[] dy = { -1, -1, 0, 1, 1, 1, 0, -1 };

            int targetX = casterX + dx[dir] * 2;
            int targetY = casterY + dy[dir] * 2;
            return (targetX, targetY, true, true); // 第 2 格无视防御与魔法盾
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 74: 战士半月弯刀 4 格弧形刀光动画 (半月形扇面覆盖前方及左右)
    // --------------------------------------------------------------------------------
    public static class HalfMoonArcEngine
    {
        public static List<(int x, int y)> GetHalfMoonArcPositions(int casterX, int casterY, int dir)
        {
            int[] dx = { 0, 1, 1, 1, 0, -1, -1, -1 };
            int[] dy = { -1, -1, 0, 1, 1, 1, 0, -1 };

            var list = new List<(int x, int y)>();
            int frontDir = dir;
            int leftDir = (dir + 7) % 8;
            int rightDir = (dir + 1) % 8;
            int leftFarDir = (dir + 6) % 8;

            list.Add((casterX + dx[frontDir], casterY + dy[frontDir]));
            list.Add((casterX + dx[leftDir], casterY + dy[leftDir]));
            list.Add((casterX + dx[rightDir], casterY + dy[rightDir]));
            list.Add((casterX + dx[leftFarDir], casterY + dy[leftFarDir]));
            return list;
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 75: 战士野蛮冲撞脚底扬尘泥土飞溅微粒子
    // --------------------------------------------------------------------------------
    public static class SavageDustEngine
    {
        public static (int particleCount, float spreadSpeed) CalculateDust(int pushDistance)
        {
            return (pushDistance * 8, 1.5f * pushDistance);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 76: 战士烈火剑法刀身持续燃烧腾腾烈焰动画 (每 100ms 循环一帧)
    // --------------------------------------------------------------------------------
    public static class FireSwordVisualEngine
    {
        public const int TotalFlameFrames = 6;

        public static int GetFlameFrame(long chargeElapsedMs)
        {
            if (chargeElapsedMs < 0 || chargeElapsedMs > 10000) return -1; // 超出 10 秒失效
            return (int)((chargeElapsedMs / 100) % TotalFlameFrames);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 77: 法师小火球/大火球弹道沿抛物线飞行并在击中目标时产生四散火星
    // --------------------------------------------------------------------------------
    public static class FireballTrajectoryEngine
    {
        public static (float currentX, float currentY, float currentZ, bool hit) CalculateTrajectory(
            float startX, float startY, float targetX, float targetY, float t) // t in [0, 1]
        {
            t = Math.Clamp(t, 0f, 1f);
            float curX = startX + (targetX - startX) * t;
            float curY = startY + (targetY - startY) * t;
            float curZ = 4.0f * 30.0f * t * (1.0f - t); // 抛物线最高 30 像素
            return (curX, curY, curZ, t >= 1.0f);
        }

        public static int GetHitSparkleCount(bool isBigFireball) => isBigFireball ? 16 : 8;
    }

    // --------------------------------------------------------------------------------
    // 细节 78: 法师雷电术从天顶笔直劈下一道贯穿天地的青白霹雳与受击瞬间白炽
    // --------------------------------------------------------------------------------
    public static class LightningBoltEngine
    {
        public const int TopSkyHeight = 600;

        public static (int startY, int endY, bool targetOverexposed) CalculateThunder(int targetY, float elapsedSeconds)
        {
            bool overexposed = elapsedSeconds < 0.15f; // 前 150ms 受击目标白炽过曝
            return (targetY - TopSkyHeight, targetY, overexposed);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 79: 法师地狱火向前喷射 5 格宽 2 格的翻滚火焰浪潮
    // --------------------------------------------------------------------------------
    public static class HellfireWaveEngine
    {
        public static List<(int x, int y)> GetHellfireArea(int casterX, int casterY, int dir)
        {
            int[] dx = { 0, 1, 1, 1, 0, -1, -1, -1 };
            int[] dy = { -1, -1, 0, 1, 1, 1, 0, -1 };

            var list = new List<(int x, int y)>();
            for (int dist = 1; dist <= 5; dist++)
            {
                int cx = casterX + dx[dir] * dist;
                int cy = casterY + dy[dir] * dist;
                list.Add((cx, cy));
                // 宽 2 格（侧向偏移一格）
                int sideDir = (dir + 2) % 8;
                list.Add((cx + dx[sideDir], cy + dy[sideDir]));
            }
            return list;
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 80: 法师疾光电影沿直线瞬发贯穿整屏炽烈强电光束 (12 格)
    // --------------------------------------------------------------------------------
    public static class LaserBeamEngine
    {
        public const int MaxRange = 12;

        public static List<(int x, int y)> GetBeamTrajectory(int casterX, int casterY, int dir)
        {
            int[] dx = { 0, 1, 1, 1, 0, -1, -1, -1 };
            int[] dy = { -1, -1, 0, 1, 1, 1, 0, -1 };

            var list = new List<(int x, int y)>();
            for (int r = 1; r <= MaxRange; r++)
            {
                list.Add((casterX + dx[dir] * r, casterY + dy[dir] * r));
            }
            return list;
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 81: 法师火墙燃烧时地表石砖裂缝窜出火舌与动态烟雾
    // --------------------------------------------------------------------------------
    public static class FireWallVisualEngine
    {
        public static (float flameHeightPx, float smokeAlpha) EvaluateVisual(float activeSeconds)
        {
            float flame = 25f + 5f * MathF.Sin(activeSeconds * 8f);
            float smokeAlpha = 0.4f + 0.1f * MathF.Cos(activeSeconds * 4f);
            return (flame, smokeAlpha);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 82: 法师地狱雷光以施法者为中心爆发 8 股向外辐射的连锁圣雷电网
    // --------------------------------------------------------------------------------
    public static class HellThunderRadiiEngine
    {
        public static List<(int dir, int radius)> GetRadii()
        {
            var list = new List<(int, int)>();
            for (int d = 0; d < 8; d++) list.Add((d, 3)); // 8 方向，辐射半径 3 格
            return list;
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 83: 法师瞬息移动原地淡出 (Alpha 渐变) 与落地点光环汇聚淡入
    // --------------------------------------------------------------------------------
    public static class TeleportFadeEngine
    {
        public static (float originAlpha, float destinationRingScale, float destAlpha) EvaluateFade(float progress) // [0, 1]
        {
            float originAlpha = Math.Clamp(1.0f - progress * 2.0f, 0f, 1f); // 前半段淡出
            float destAlpha = Math.Clamp((progress - 0.5f) * 2.0f, 0f, 1f); // 后半段淡入
            float ringScale = Math.Clamp(1.5f - progress * 0.5f, 1.0f, 1.5f);
            return (originAlpha, ringScale, destAlpha);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 84: 法师魔法盾椭圆护罩在受击时的涟漪波纹与受力凹陷微形变
    // --------------------------------------------------------------------------------
    public static class MagicShieldDistortionEngine
    {
        public static (float deformOffsetPx, float rippleAlpha) EvaluateHitDeform(float elapsedSinceHit)
        {
            if (elapsedSinceHit < 0 || elapsedSinceHit > 0.3f) return (0f, 0f);
            float deform = 4.0f * (1.0f - elapsedSinceHit / 0.3f);
            float ripple = 0.6f * (1.0f - elapsedSinceHit / 0.3f);
            return (deform, ripple);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 85: 法师冰咆哮 3x3 范围暴风雪漩涡与短暂白霜冰层
    // --------------------------------------------------------------------------------
    public static class IceStormVisualEngine
    {
        public static (bool hasFrostFloor, float rotationSpeed) EvaluateIceStorm(float elapsed)
        {
            bool frost = elapsed < 3.0f; // 3 秒地表白霜冰层
            float rotSpeed = elapsed < 1.0f ? 720f : 0f;
            return (frost, rotSpeed);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 86: 道士治愈术目标头顶旋转下落的绿色八卦治愈符咒
    // --------------------------------------------------------------------------------
    public static class HealBaguaVisualEngine
    {
        public static (float rotationDeg, float heightOffsetPx) EvaluateBagua(float elapsed)
        {
            float rot = (elapsed * 360f) % 360f;
            float h = Math.Clamp(60f - elapsed * 40f, 10f, 60f); // 从头顶 60 像素旋转下落到 10 像素
            return (rot, h);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 87: 道士精神力战法命中风鸣声 (Sound 5)
    // --------------------------------------------------------------------------------
    public static class SoulStrikeSoundEngine
    {
        public const int WindSoundId = 5;

        public static (bool playSound, int soundId) CheckSound(bool hitSuccess)
        {
            return (hitSuccess, hitSuccess ? WindSoundId : 0);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 88: 道士灵魂火符飞行弹道 (黄纸红字符咒旋转破空并在撞击时爆炸)
    // --------------------------------------------------------------------------------
    public static class SoulSymbolTrajectoryEngine
    {
        public static (float curX, float curY, float rotDeg, bool explode) Calculate(
            float startX, float startY, float targetX, float targetY, float t)
        {
            t = Math.Clamp(t, 0f, 1f);
            float x = startX + (targetX - startX) * t;
            float y = startY + (targetY - startY) * t;
            float rot = t * 1080f; // 飞行中旋转 3 圈
            return (x, y, rot, t >= 1.0f);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 89: 道士施毒术绿色 (持续毒) 与黄色 (减防毒) 半透明荧光着色覆盖
    // --------------------------------------------------------------------------------
    public static class PoisonGlowEngine
    {
        public enum PoisonType { None, GreenPoison, YellowPoison }

        public static (string tintColorHex, float glowAlpha) GetPoisonTint(PoisonType poison)
        {
            return poison switch
            {
                PoisonType.GreenPoison => ("#00FF00", 0.45f), // 绿毒荧光
                PoisonType.YellowPoison => ("#FFFF00", 0.45f), // 黄毒荧光
                _ => ("#FFFFFF", 0.0f)
            };
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 90: 道士召唤神兽地火裂开喷射与麒麟破土仰天咆哮
    // --------------------------------------------------------------------------------
    public static class SummonShinsuEngine
    {
        public enum ShinsuPhase { GroundCrack, EmergeFlame, Roaring, Active }

        public static (ShinsuPhase phase, string actionName) EvaluateSummonStage(float elapsed)
        {
            if (elapsed < 0.6f) return (ShinsuPhase.GroundCrack, "Crack");
            if (elapsed < 1.2f) return (ShinsuPhase.EmergeFlame, "Emerge");
            if (elapsed < 2.0f) return (ShinsuPhase.Roaring, "Roar");
            return (ShinsuPhase.Active, "Stand");
        }
    }
}
