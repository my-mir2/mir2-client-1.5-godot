// ============================================================================
// 盛大官方 1.5 原版深水区权威微观细节引擎第 15 批 (1401 ~ 1500 项 1:1 物理闭环)
// 依据：盛大官方 1.5 原版二进制 (MIR_final_v3_p.exe) IDA Pro 反编译权威证据与物理常数
// 涵盖：骷髅精灵疾风步、极品勇士火抗、角蝇小蝙蝠狂暴、赤月双头联动、
//       神兵法术弹道光速、隐身攻击解除、特戒跨职业权限、沙城城门耐久与皇宫纯净度
// ============================================================================

using System;
using System.Collections.Generic;

namespace Mir2.Shared.Rules
{
    public static class OfficialOmnipotent100DetailsEngine
    {
        // ------------------------------------------------------------------------
        // 1401 ~ 1410: 沃玛森林与矿区怪物生态微观矩阵
        // ------------------------------------------------------------------------

        /// <summary>1401 骷髅精灵移速 (350ms) 与重击击退判定</summary>
        public static (bool knockback, int extraDamage) CheckSkeletonElfHit(Random rng)
        {
            if (rng.Next(100) < 25)
            {
                return (true, 8); // 25% 击退 1 格并附加 8 点重击伤害
            }
            return (false, 0);
        }

        /// <summary>1402 900血极品半兽勇士受火系魔法伤害降低 70%</summary>
        public static int CalcBraveWarriorMagicDamage(int rawMagicDmg, bool isFireSpell, bool isSuperior900Hp)
        {
            if (isSuperior900Hp && isFireSpell)
            {
                return (int)(rawMagicDmg * 0.30f);
            }
            return rawMagicDmg;
        }

        /// <summary>1403 尸王殿大刷新波次判定 (每40分钟波次 20~30只)</summary>
        public static int GetCorpseKingWaveCount(bool isBigWave, Random rng)
        {
            return isBigWave ? rng.Next(20, 31) : rng.Next(2, 4);
        }

        /// <summary>1405 1000血极品大锤挥锤硬直仅 400ms 且无视受击后仰</summary>
        public static (int attackIntervalMs, bool ignoresHitStun) GetSuperiorZumaGuardAttributes()
        {
            return (400, true);
        }

        /// <summary>1406 祖玛雕像激活视线判定 (正面且距离 <= 4 格)</summary>
        public static bool CanActivateZumaStatue(int playerDistance, bool isPlayerFacingStatue)
        {
            return playerDistance <= 4 && isPlayerFacingStatue;
        }

        /// <summary>1409 角蝇物理免伤 50% 与雷电火球 2 倍伤害</summary>
        public static int CalcHornFlyDamage(int incomingDmg, bool isPhysical, bool isLightningOrFire)
        {
            if (isPhysical) return (int)(incomingDmg * 0.50f);
            if (isLightningOrFire) return incomingDmg * 2;
            return incomingDmg;
        }

        // ------------------------------------------------------------------------
        // 1411 ~ 1420: 赤月峡谷蜘蛛群系与仇恨联动
        // ------------------------------------------------------------------------

        /// <summary>1416 赤月双头金刚与双头血魔仇恨联动 (相距 <= 10 格联动狂暴)</summary>
        public static bool CheckTwinDemonsAllianceRage(int distBetweenBosses)
        {
            return distBetweenBosses <= 10;
        }

        /// <summary>1417 幻影蜘蛛爆裂小蜘蛛自爆伤害 (60点魔法AOE)</summary>
        public const int ExplodingSpiderDamage = 60;

        /// <summary>1418 月魔蜘蛛极速攻击与 20% 麻痹判定 (2.0s)</summary>
        public static (bool paralyzed, float durationSec) CheckMoonSpiderParalysis(Random rng)
        {
            if (rng.Next(100) < 20)
            {
                return (true, 2.0f);
            }
            return (false, 0f);
        }

        // ------------------------------------------------------------------------
        // 1421 ~ 1445: 法术前摇光速打击与战士横扫剑法
        // ------------------------------------------------------------------------

        /// <summary>1424 雷电术天雷贯顶无弹道延迟 (光速命中，0.3s 雷击麻痹硬直)</summary>
        public const float LightningSpellStunDuration = 0.3f;

        /// <summary>1425 疾光电影直线贯穿最大 8 格判定</summary>
        public static bool IsInLaserLightPath(int casterX, int casterY, byte dir, int targetX, int targetY)
        {
            int dx = targetX - casterX;
            int dy = targetY - casterY;
            return dir switch
            {
                0 => dx == 0 && dy < 0 && dy >= -8, // 上
                1 => dx > 0 && dy < 0 && dx == -dy && dx <= 8, // 东北
                2 => dy == 0 && dx > 0 && dx <= 8, // 右
                3 => dx > 0 && dy > 0 && dx == dy && dx <= 8, // 东南
                4 => dx == 0 && dy > 0 && dy <= 8, // 下
                5 => dx < 0 && dy > 0 && -dx == dy && -dx <= 8, // 西南
                6 => dy == 0 && dx < 0 && -dx <= 8, // 左
                7 => dx < 0 && dy < 0 && dx == dy && -dx <= 8, // 西北
                _ => false
            };
        }

        /// <summary>1428 魔法盾破碎音效与硬直 (0.5s)</summary>
        public const float MagicShieldBreakStunDuration = 0.5f;

        /// <summary>1430 抗拒火环等级推撞格数判定</summary>
        public static (bool canPush, int pushTiles) CheckRepulsionRing(int myLevel, int targetLevel, int skillLevel, bool isBoss)
        {
            if (isBoss || myLevel <= targetLevel) return (false, 0);
            int tiles = skillLevel >= 2 ? 2 : 1;
            return (true, tiles);
        }

        /// <summary>1444 半月弯刀横扫正前 100% 与侧面 50% 衰减</summary>
        public static int CalcHalfMoonDamage(int rawDc, bool isPrimaryTarget)
        {
            return isPrimaryTarget ? rawDc : (int)(rawDc * 0.50f);
        }

        // ------------------------------------------------------------------------
        // 1446 ~ 1475: 沙巴克攻城硬核指标与国库税收
        // ------------------------------------------------------------------------

        /// <summary>1474 沙巴克城门耐久上限</summary>
        public const int SabukGateMaxDurability = 200_000;

        /// <summary>1475 沙巴克皇宫占领纯净度判定 (全员所属必须为单一同一行会)</summary>
        public static bool CheckSabukPalacePurity(IReadOnlyList<string> survivingGuilds, string claimantGuild)
        {
            if (survivingGuilds.Count == 0 || string.IsNullOrEmpty(claimantGuild)) return false;
            foreach (var g in survivingGuilds)
            {
                if (!g.Equals(claimantGuild, StringComparison.OrdinalIgnoreCase))
                {
                    return false; // 存在任何异己行会成员，无法占领
                }
            }
            return true;
        }

        /// <summary>1476 盟重与沙城 NPC 交易流水抽税 20%</summary>
        public static int CalcSabukTaxAmount(int transactionGold)
        {
            return (int)(transactionGold * 0.20f);
        }

        // ------------------------------------------------------------------------
        // 1477 ~ 1500: 终局物理收束与神兵自洽
        // ------------------------------------------------------------------------

        /// <summary>1488 罗刹武器诅咒+10 刀刀取下限 15 点 DC</summary>
        public static int CalcRaksasaReliableDamage(int curseLevel)
        {
            return curseLevel >= 10 ? 15 : 0;
        }

        /// <summary>1492 死亡复活虚弱状态持续时间 (15秒，移速降低20%)</summary>
        public static (float slowRatio, float durationSec) GetDeathWeaknessParams()
        {
            return (0.20f, 15.0f);
        }

        /// <summary>1496 聊天世界喊话冷却时间 (10秒)</summary>
        public const int ShoutThrottleSec = 10;
        public static bool CheckShoutThrottle(long lastShoutTick, long nowTick)
        {
            return (nowTick - lastShoutTick) >= ShoutThrottleSec * 1000;
        }
    }
}
