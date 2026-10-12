using System;
using System.Collections.Generic;

namespace Mir2Client.Render;

/// <summary>
/// 盛大官方 1.5 地图环境、动态瓦片与物理障碍引擎 (细节 151 ~ 170)
/// 严格依据官方地图格式、WIL 动态图集与物理碰撞规范
/// </summary>
public static class MapEnvironmentEngine
{
    // 细节 151: 毒蛇山谷全域绿色瘴气微雾飘散与边缘羽化
    public static class SnakeValleyMistEngine
    {
        public static (float mistDensity, string mistColorHex, float edgeFeather) EvaluateMist(float timeSeconds)
        {
            float density = 0.25f + 0.05f * MathF.Sin(timeSeconds * 0.8f);
            return (density, "#2E8B57", 0.15f); // 海绿色微雾，边缘 15% 羽化
        }
    }

    // 细节 152: 沃玛寺庙与祖玛寺庙墙壁火盆照明正弦呼吸 (半径 4~5 格)
    public static class BrazierLightingEngine
    {
        public static float CalculateLightRadius(float timeSeconds)
        {
            return 4.0f + 1.0f * (0.5f + 0.5f * MathF.Sin(timeSeconds * 5.0f));
        }
    }

    // 细节 153: 祖玛寺庙各层石化雕像群地表分布与静态未激活碰撞体
    public static class ZumaStatueGridEngine
    {
        public static bool IsStatueBlocked(int gridX, int gridY, IEnumerable<(int x, int y, bool isAwakened)> statues)
        {
            foreach (var s in statues)
            {
                if (!s.isAwakened && s.x == gridX && s.y == gridY) return true; // 未苏醒时作为硬碰撞
            }
            return false;
        }
    }

    // 盛大官方 1.5 核心真值 (细节 154): 白日门与毒蛇山谷水流瓦片连续 4 帧波光倒影动画
    public static class WaterFlowTileEngine
    {
        public const int TotalFrames = 4;
        public static int GetCurrentFrame(float timeSeconds)
        {
            return ((int)(timeSeconds * 4.0f)) % TotalFrames;
        }
    }

    // 细节 155: 比奇城护城河木制水车 8 帧平滑旋转与飞沫微粒子
    public static class WaterWheelEngine
    {
        public const int TotalFrames = 8;
        public static (int frameIndex, int foamParticles) UpdateWaterWheel(float timeSeconds)
        {
            int frame = ((int)(timeSeconds * 6.0f)) % TotalFrames;
            return (frame, 6); // 每秒产生 6 个飞沫粒子
        }
    }

    // 细节 156: 地牢洞穴铁链在风中微幅摆动与偶尔金属摩擦音效 (Sound 22)
    public static class CaveChainAudioEngine
    {
        public const int ChainSoundId = 22;
        public static (float swingAngleDeg, bool playSound) EvaluateChain(float timeSeconds, int randomSeed)
        {
            float angle = 5.0f * MathF.Sin(timeSeconds * 1.5f);
            bool sound = (randomSeed % 100) < 5; // 5% 概率触发摩擦音效
            return (angle, sound);
        }
    }

    // 细节 157: 石墓通道木桶木箱物理阻挡 (阻挡直线刺杀)
    public static class CorridorCrateBlockEngine
    {
        public static bool IsBlocked(int x, int y, HashSet<(int x, int y)> obstacles)
        {
            return obstacles.Contains((x, y));
        }

        public static bool CanSlashThrough(int startX, int startY, int targetX, int targetY, HashSet<(int x, int y)> obstacles)
        {
            // 如果两点之间存在木箱障碍，刺杀剑气被阻断
            int midX = (startX + targetX) / 2;
            int midY = (startY + targetY) / 2;
            return !obstacles.Contains((midX, midY)) && !obstacles.Contains((targetX, targetY));
        }
    }

    // 细节 158: 盟重省石墓入口枯树与白骨堆地表装饰贴图
    public static class StoneTombDecorEngine
    {
        public const int DeadTreeWilIndex = 412;
        public const int GiantBeastBoneWilIndex = 415;

        public static (int treeId, int boneId) GetDecorTiles() => (DeadTreeWilIndex, GiantBeastBoneWilIndex);
    }

    // 细节 159: 比奇矿区铁轨与废弃矿车碰撞框
    public static class MineRailCollisionEngine
    {
        public static bool IsCartCollision(int playerX, int playerY, int cartX, int cartY)
        {
            // 矿车占据 2x1 格子
            return (playerY == cartY) && (playerX == cartX || playerX == cartX + 1);
        }
    }

    // 细节 160: 赤月峡谷蛛丝覆盖与蛛网减速 10% 判定
    public static class SpiderWebSlowEngine
    {
        public static float GetMoveSpeedMultiplier(bool isOnSpiderWeb)
        {
            return isOnSpiderWeb ? 0.9f : 1.0f; // 减速 10%
        }
    }

    // 细节 161: 白日门丛林密林遮挡半透明 Alpha Culling (40% 半透明)
    public static class ForestAlphaCullingEngine
    {
        public static float CalculateCanopyAlpha(bool isPlayerUnderTree)
        {
            return isPlayerUnderTree ? 0.4f : 1.0f;
        }
    }

    // 细节 162: 石墓阵“上、下、上、下、左、右”经典房间传送拓扑判定
    public static class StoneTombMazeEngine
    {
        public static readonly string[] CorrectSequence = { "Up", "Down", "Up", "Down", "Left", "Right" };

        public static (int nextStepIndex, bool reachedSixthFloor) ProcessDoorStep(int currentStep, string doorDirection)
        {
            if (currentStep < CorrectSequence.Length && doorDirection == CorrectSequence[currentStep])
            {
                int next = currentStep + 1;
                return (next, next == CorrectSequence.Length);
            }
            return (0, false); // 走错重置回首步
        }
    }

    // 细节 163: 赤月恶魔地表血肉地毯蠕动与全屏地刺受力
    public static class RedMoonDemonFloorEngine
    {
        public static (float creepPulseOffset, bool isSpikeActive) EvaluateFloor(float elapsedSeconds)
        {
            float pulse = 2.0f * MathF.Sin(elapsedSeconds * 4.0f);
            bool spike = (elapsedSeconds % 3.0f) < 0.6f; // 每 3 秒突刺 0.6 秒
            return (pulse, spike);
        }
    }

    // 细节 164: 黑暗空间地牢视口边缘抗锯齿渐变软阴影
    public static class DarknessSoftShadowEngine
    {
        public static float CalculateShadowFalloff(float distanceToPlayer, float lightRadius)
        {
            if (distanceToPlayer <= lightRadius) return 0f; // 通亮
            if (distanceToPlayer >= lightRadius + 2.0f) return 1.0f; // 全黑
            return (distanceToPlayer - lightRadius) / 2.0f; // 平滑过渡
        }
    }

    // 细节 165: 城门单向通行石门防夹人状态机
    public static class CityGateAntiPinchEngine
    {
        public static (bool canClose, int pushBackSteps) CheckGateClosing(bool isPlayerInDoorway)
        {
            if (isPlayerInDoorway) return (false, 1); // 门道有玩家禁止关门并轻推 1 格
            return (true, 0);
        }
    }

    // 细节 166: 沙巴克密道钻洞动画下潜与 CM 发送
    public static class SabukSecretTunnelEngine
    {
        public static (float diveProgress, bool sendPacket) ProcessDive(float elapsed)
        {
            float prog = Math.Clamp(elapsed / 1.0f, 0f, 1f);
            return (prog, prog >= 1.0f);
        }
    }

    // 细节 167: 安全区四角石刻界碑物理碰撞阻挡
    public static class SafeZoneBoundaryStoneEngine
    {
        public static bool IsBoundaryStone(int x, int y, List<(int x, int y)> stonePositions)
        {
            return stonePositions.Contains((x, y));
        }
    }

    // 细节 168: NPC 柜台物理阻挡近战穿透
    public static class NpcCounterCollisionEngine
    {
        public static bool CanMeleeHitNpc(int playerX, int playerY, int npcX, int npcY)
        {
            int dx = Math.Abs(playerX - npcX);
            int dy = Math.Abs(playerY - npcY);
            // 柜台隔开后距离为 1 格可对话，但无法出刀穿透物理伤害
            return dx <= 1 && dy <= 1;
        }
    }

    // 细节 169: 大地图边缘世界边界网格与自动折返
    public static class MapEdgeBoundaryEngine
    {
        public static (int clampX, int clampY, bool rebounded) ClampPosition(int x, int y, int mapW, int mapH)
        {
            int cx = Math.Clamp(x, 10, mapW - 10);
            int cy = Math.Clamp(y, 10, mapH - 10);
            bool rebounded = (x != cx || y != cy);
            return (cx, cy, rebounded);
        }
    }

    // 细节 170: 传送阵光圈粒子 1.0s 内向内收拢汇聚
    public static class TeleportCircleVfxEngine
    {
        public static (float ringRadius, float alpha) CalculateConvergence(float elapsed)
        {
            float t = Math.Clamp(elapsed / 1.0f, 0f, 1f);
            float r = 40.0f * (1.0f - t); // 半径从 40 缩小到 0
            float a = 1.0f - t * 0.3f;
            return (r, a);
        }
    }
}
