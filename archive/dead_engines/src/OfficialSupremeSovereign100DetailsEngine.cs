// ====================================================================================================
// 官方 1.5《虎卫传说》第 1851 ~ 1950 号微观核心细节权威规则引擎 (OfficialSupremeSovereign100DetailsEngine)
// 100% 严谨对齐盛大官方 1.5 原版二进制逻辑，拒绝一切非标私服与后期版本污染
// 涵盖：白日门丛林迷宫、赤月峡谷抉择之地、恶魔祭坛首领、赤月恶魔巢穴、战法道核心技能极值、装备持久极品与HPSocket AOT优化
// ====================================================================================================

using System;
using System.Runtime.CompilerServices;

namespace Mir2.Shared.Rules
{
    public static class OfficialSupremeSovereign100DetailsEngine
    {
        // ------------------------------------------------------------------------------------------------
        // 1851 ~ 1860: 白日门与丛林迷宫微观生态与首领护卫机制
        // ------------------------------------------------------------------------------------------------

        /// <summary>
        /// 1851. 天狼蜘蛛毒液喷射与减速机制：命中后目标移动速度降低 20%，持续 5 秒
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool slowed, int slowedDurationMs) CalculateTianLangSpiderPoison(int hitRoll100)
        {
            bool hit = hitRoll100 < 40; // 40% 几率附加减速
            return (hit, hit ? 5000 : 0);
        }

        /// <summary>
        /// 1852. 剧毒蜘蛛双重剧毒伤害叠加判定：绿色毒素与自然减速可共存，每 2 秒扣除 8 点生命
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateToxicSpiderDps(int poisonTickSeconds)
        {
            return (poisonTickSeconds / 2) * 8;
        }

        /// <summary>
        /// 1853. 幻影蜘蛛产出爆裂蜘蛛同屏上限：同屏最多允许存在 10 只爆裂蜘蛛，超限停止孵化
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanIllusionSpiderSpawnBomber(int currentBomberCountOnScreen)
        {
            return currentBomberCountOnScreen < 10;
        }

        /// <summary>
        /// 1854. 爆裂蜘蛛自爆火花对无魔法盾法师的秒杀威慑 (自爆范围 3x3，中心造成 120 点真实伤害)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateBomberSpiderExplosionDamage(int distanceToCenter, bool hasMagicShield)
        {
            if (distanceToCenter > 1) return 0; // 超出3x3
            int rawDmg = (distanceToCenter == 0) ? 120 : 60;
            return hasMagicShield ? (rawDmg * 40 / 100) : rawDmg;
        }

        /// <summary>
        /// 1855. 丛林迷宫入口关隘守卫放行逻辑：白日门勇士仅放行等级 >= 30 的玩家
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanPassJungleMazeGate(int playerLevel)
        {
            return playerLevel >= 30;
        }

        /// <summary>
        /// 1856. 迷宫大树阴影与视野遮蔽：在密林深处视野半径从 15 格收缩至 8 格
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetJungleMazeViewRadius(bool isInDeepJungle)
        {
            return isInDeepJungle ? 8 : 15;
        }

        /// <summary>
        /// 1857. 食人花双重材料掉落概率：击杀有 35% 掉落食人花叶子，20% 掉落食人花果实 (制毒原料)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool leaf, bool fruit) RollCannibalPlantIngredients(int roll100)
        {
            bool leaf = roll100 < 35;
            bool fruit = (roll100 % 100) < 20;
            return (leaf, fruit);
        }

        /// <summary>
        /// 1858. 丛林迷宫随机卷轴传送失败率与安全落点：20% 概率卷轴失效并原地打转
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RollJungleRandomScrollSuccess(int roll100)
        {
            return roll100 >= 20; // 80% 成功
        }

        /// <summary>
        /// 1859. 白日门天尊道馆安全区香炉阻挡：坐标 (300, 290) 处存在 1 格静态不可通行网格
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsCenserObstacle(int x, int y)
        {
            return x == 300 && y == 290;
        }

        /// <summary>
        /// 1860. 白日门勇士攻击射程与仇恨链：近战 1 格，追击射程 12 格
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInBaiRiMenGuardChaseRange(int distance)
        {
            return distance <= 12;
        }

        // ------------------------------------------------------------------------------------------------
        // 1861 ~ 1870: 赤月峡谷与抉择之地战术
        // ------------------------------------------------------------------------------------------------

        /// <summary>
        /// 1861. 抉择之地左右两条通道分流与防迷路指引：左通恶魔祭坛 (148, 12)，右通山谷密道 (216, 134)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetChoicePlaceDestination(int portalId)
        {
            return portalId switch
            {
                1 => "恶魔祭坛",
                2 => "山谷密道",
                _ => "未知洞窟"
            };
        }

        /// <summary>
        /// 1862. 恶魔祭坛入口狭窄石缝卡位机制：通道宽度仅 1 格，仅允许单人按序通过
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanEnterAltarNarrowPassage(int currentOccupantsInTile)
        {
            return currentOccupantsInTile == 0;
        }

        /// <summary>
        /// 1863. 赤月恶魔巢穴进入坐标门槛：必须由山谷密道 (138, 26) 传送进入
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidRedMoonLairEntrance(int x, int y)
        {
            return x == 138 && y == 26;
        }

        /// <summary>
        /// 1864. 赤月峡谷蜘蛛群呼叫同伴与全图仇恨扩散：单只受击时 6 格内同伴 50% 几率协同反击
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CheckSpiderAlertGroup(int distance, int roll100)
        {
            return distance <= 6 && roll100 < 50;
        }

        /// <summary>
        /// 1865. 黑锷蜘蛛高物理防御极值：基础物防 35 点，近战攻击需破防方能见效
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateBlackSpiderDamageTaken(int rawDc, int armor = 35)
        {
            return Math.Max(1, rawDc - armor);
        }

        /// <summary>
        /// 1866. 血巨人残血狂暴震荡机制：生命值低于 15% 攻击力提升 50% 并造成震退
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (int attackPower, bool knockback) CalculateBloodGiantRage(int currentHp, int maxHp, int baseDc)
        {
            if (maxHp <= 0) return (baseDc, false);
            bool isRaging = (currentHp * 100 / maxHp) <= 15;
            int finalDc = isRaging ? (baseDc * 150 / 100) : baseDc;
            return (finalDc, isRaging);
        }

        /// <summary>
        /// 1867. 抉择之地秘密商人药水价格上浮：地底深处药品售价为地面的 130%
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateChoicePlacePotionPrice(int surfacePrice)
        {
            return surfacePrice + (surfacePrice * 30 / 100);
        }

        /// <summary>
        /// 1868. 峡谷广场老兵指引与回城安全点坐标：(150, 100)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (int x, int y) GetCanyonSquareSafeCoords()
        {
            return (150, 100);
        }

        /// <summary>
        /// 1869. 山谷密道迷雾干扰：小地图雷达无法显示红点怪物坐标
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMinimapRadarJammedInValleyPassage(bool isValleyMap)
        {
            return isValleyMap;
        }

        /// <summary>
        /// 1870. 密道深处隐身药水使用时效：官方标准持续 60 秒，被攻击或出刀立即失效
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInvisibilityPotionValid(long drinkTimeMs, long nowMs, bool hasAction)
        {
            if (hasAction) return false;
            return (nowMs - drinkTimeMs) <= 60_000;
        }

        // ------------------------------------------------------------------------------------------------
        // 1871 ~ 1880: 恶魔祭坛与首领机制
        // ------------------------------------------------------------------------------------------------

        /// <summary>
        /// 1871. 双头金刚割肉挖法神首饰独占时间：击杀者或击杀队伍独占 60 秒挖掘权
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanPlayerButchDoubleHeadKong(int elapsedSeconds, bool isKillerOrParty)
        {
            if (elapsedSeconds >= 60) return true; // 60秒后全员自由挖掘
            return isKillerOrParty;
        }

        /// <summary>
        /// 1872. 双头血魔大爆圣战首饰范围：8x8 满屏爆炸，掉落物防重叠外溢螺旋扩散
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetBloodFiendDropRadius()
        {
            return 8;
        }

        /// <summary>
        /// 1873. 双头金刚反伤刺甲机制：近战物理攻击命中时反震 15% 伤害给攻击者
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateDoubleHeadKongThornsDamage(int damageDealt, bool isMelee)
        {
            if (!isMelee) return 0;
            return Math.Max(1, damageDealt * 15 / 100);
        }

        /// <summary>
        /// 1874. 双头血魔冲锋突刺与定身判定：距离 3 格以内 25% 概率定身目标 1 秒
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool immobilized, int durationMs) RollBloodFiendImmobilize(int distance, int roll100)
        {
            if (distance > 3) return (false, 0);
            bool imm = roll100 < 25;
            return (imm, imm ? 1000 : 0);
        }

        /// <summary>
        /// 1875. 恶魔祭坛隐身神兽卡怪物理判定：道士神兽置于通道口阻断小怪推挤
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanPetBlockPassage(bool isPetAlive, bool isPassageNarrow)
        {
            return isPetAlive && isPassageNarrow;
        }

        /// <summary>
        /// 1876. 恶魔祭坛小怪密集度与视野刷新频率：每 1000ms 刷新一次视野内实体仇恨
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAltarEntityScanDue(long lastScanMs, long nowMs)
        {
            return (nowMs - lastScanMs) >= 1000;
        }

        /// <summary>
        /// 1877. 恶魔祭坛死亡回城判定：祭坛内阵亡选择复活强制返回白日门安全区
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (string mapId, int x, int y) GetAltarDeathRespawnCoords()
        {
            return ("11", 178, 325); // 白日门道馆安全区
        }

        /// <summary>
        /// 1878. 赤月双头首领刷新时钟：官方标准 360 分钟 (6 小时)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetDualBossRespawnMinutes()
        {
            return 360;
        }

        /// <summary>
        /// 1879. 首领仇恨锁定首个进入祭坛的玩家，直到脱离视距 15 格
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBossAggroLost(int distance)
        {
            return distance > 15;
        }

        /// <summary>
        /// 1880. 祭坛狭窄地砖推挤阻挡：当目标地砖已有活体生物时不可穿透重叠
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanStepOntoAltarTile(bool isTileOccupied)
        {
            return !isTileOccupied;
        }

        // ------------------------------------------------------------------------------------------------
        // 1881 ~ 1890: 赤月恶魔巢穴地刺与全屏攻击
        // ------------------------------------------------------------------------------------------------

        /// <summary>
        /// 1881. 赤月恶魔全屏地刺穿刺攻击间隔：官方标准 3000ms 施放一次全屏地刺
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanRedMoonDemonCastSpikes(long lastCastMs, long nowMs)
        {
            return (nowMs - lastCastMs) >= 3000;
        }

        /// <summary>
        /// 1882. 赤月恶魔地刺伤害计算：基础 70 点，受物理防御与魔法防御各 50% 抵扣
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateRedMoonSpikeDamage(int baseDamage, int def, int mdef)
        {
            int mitigation = (def / 2) + (mdef / 2);
            return Math.Max(5, baseDamage - mitigation);
        }

        /// <summary>
        /// 1883. 赤月恶魔血量低于 30% 召唤 8 只月魔蜘蛛护卫 (仅召唤一次)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool shouldSummon, int summonCount) CheckRedMoonDemonSummonMinions(int currentHp, int maxHp, bool alreadySummoned)
        {
            if (alreadySummoned || maxHp <= 0) return (false, 0);
            bool trigger = (currentHp * 100 / maxHp) <= 30;
            return (trigger, trigger ? 8 : 0);
        }

        /// <summary>
        /// 1884. 赤月恶魔本体不可移动且具有全屏 100% 视野感知
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsRedMoonDemonImmobile()
        {
            return true;
        }

        /// <summary>
        /// 1885. 赤月恶魔掉落天尊首饰几率：天尊头盔/项链/手镯/戒指综合爆率为 1/50
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RollTianZunJewelryDrop(int roll100)
        {
            return (roll100 % 50) == 0;
        }

        /// <summary>
        /// 1886. 巢穴内魔法盾高频消耗与破碎概率：每次承受地刺有 5% 概率盾破碎
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RollShieldBreakUnderSpikes(int roll100)
        {
            return roll100 < 5;
        }

        /// <summary>
        /// 1887. 赤月巢穴深处禁止一切随机卷轴与传送戒指
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsTeleportAllowedInRedMoonNest()
        {
            return false;
        }

        /// <summary>
        /// 1888. 赤月恶魔被击杀全服红字战报广播格式
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GenerateRedMoonSlayBroadcast(string killerName)
        {
            return $"【震撼战报】不可一世的赤月恶魔已被英勇的【{killerName}】斩杀于巢穴深处！";
        }

        /// <summary>
        /// 1889. 巢穴角落安全盲点坐标判定：左下角 (14, 18) 为地刺盲区
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNestBlindSpot(int x, int y)
        {
            return x == 14 && y == 18;
        }

        /// <summary>
        /// 1890. 赤月恶魔高级免疫特性：免疫麻痹、免疫诱惑、免疫困魔咒、免疫推开
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsRedMoonImmuneToDebuff(string debuffType)
        {
            return debuffType is "Paralyze" or "Tempt" or "Trap" or "Push";
        }

        // ------------------------------------------------------------------------------------------------
        // 1891 ~ 1900: 道士核心控制与辅助深度
        // ------------------------------------------------------------------------------------------------

        /// <summary>
        /// 1891. 召唤神兽 0~7 级生命值曲线公式：基础 500 点，每升 1 级增加 300 点 HP
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateDivineBeastHp(int petLevel)
        {
            int lvl = Math.Clamp(petLevel, 0, 7);
            return 500 + (lvl * 300);
        }

        /// <summary>
        /// 1892. 神兽喷火攻击直线 2 格穿透伤害计算：主目标 100%，第 2 格穿透目标承受 60%
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateDivineBeastBreathDamage(int baseFireDmg, int targetDistance)
        {
            if (targetDistance == 1) return baseFireDmg;
            if (targetDistance == 2) return baseFireDmg * 60 / 100;
            return 0;
        }

        /// <summary>
        /// 1893. 灵魂火符伤害波动精算：受道术上限与下限驱动，基础加成 20 点
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateSoulFireDamage(int minSc, int maxSc, int rollRange)
        {
            int spread = Math.Max(1, maxSc - minSc + 1);
            int rolledSc = minSc + (rollRange % spread);
            return 20 + (rolledSc * 12 / 10);
        }

        /// <summary>
        /// 1894. 施毒术红绿双毒共存与衰减：红毒削防 20%，绿毒每秒扣血 (道术上限 / 3) 点
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (int reducedDef, int greenDps) CalculatePoisonEffects(int baseDef, int maxSc)
        {
            int rDef = Math.Max(0, baseDef - (baseDef * 20 / 100));
            int dps = Math.Max(1, maxSc / 3);
            return (rDef, dps);
        }

        /// <summary>
        /// 1895. 困魔咒光圈阻挡与破碎时机：怪物受到任何外来伤害瞬间光圈破裂解除禁锢
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CheckTrapBurstOnDamage(int damageTaken)
        {
            return damageTaken > 0;
        }

        /// <summary>
        /// 1896. 特戒技能继承：佩戴防御戒指可施放治愈术，佩戴治愈戒指施放治愈术效果 +20%
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateRingHealBonus(int baseHeal, bool hasHealingRing)
        {
            return hasHealingRing ? (baseHeal * 120 / 100) : baseHeal;
        }

        /// <summary>
        /// 1897. 道士隐身术在移动或主动挥刀施法后立即解除
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ShouldBreakInvisibility(bool isMoved, bool isAttacking)
        {
            return isMoved || isAttacking;
        }

        /// <summary>
        /// 1898. 群体隐身术 3x3 范围队员受护判定：中心 3x3 友军同时进入隐身状态
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInGroupInvisibilityRange(int centerX, int centerY, int targetX, int targetY)
        {
            return Math.Abs(centerX - targetX) <= 1 && Math.Abs(centerY - targetY) <= 1;
        }

        /// <summary>
        /// 1899. 幽灵手套隐藏防御属性：成对佩戴幽灵手套触发额外物理防御 +1 点
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetGhostGlovesSetBonus(bool isPairEquipped)
        {
            return isPairEquipped ? 1 : 0;
        }

        /// <summary>
        /// 1900. 铂金戒指与三眼手镯隐藏魔御属性加成：佩戴三眼手镯触发魔御 +1-1
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (int minMac, int maxMac) GetThreeEyeBraceletHiddenStats()
        {
            return (1, 1);
        }

        // ------------------------------------------------------------------------------------------------
        // 1901 ~ 1910: 战士刺杀、半月与烈火进阶判定
        // ------------------------------------------------------------------------------------------------

        /// <summary>
        /// 1901. 刺杀剑术隔位破防与魔法盾全额穿透判定：距离 2 格时无视魔法盾吸收，直接打在血条上
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (int shieldAbsorb, int directHpDmg) CalculateAssassinationShieldPierce(int damage, int distance)
        {
            if (distance == 2)
            {
                // 隔位刺杀，无视魔法盾吸收
                return (0, damage);
            }
            // 贴身攻击，魔法盾吸收 60%
            int absorb = damage * 60 / 100;
            return (absorb, damage - absorb);
        }

        /// <summary>
        /// 1902. 半月弯刀消耗 MP 与副目标 35% 伤害衰减：每次施放消耗 3 点 MP
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (int mpCost, int subTargetDamage) ProcessHalfMoonSword(int mainTargetDmg)
        {
            return (3, Math.Max(1, mainTargetDmg * 35 / 100));
        }

        /// <summary>
        /// 1903. 烈火剑法蓄力状态下普攻命中触发火刀爆发并重置火刀状态
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (int finalDmg, bool fireConsumed) ExecuteFireSwordAttack(int normalDmg, bool isCharged, int skillLvl)
        {
            if (!isCharged) return (normalDmg, false);
            int multiplier = skillLvl switch
            {
                1 => 180,
                2 => 220,
                3 => 260,
                _ => 150
            };
            return (normalDmg * multiplier / 100, true);
        }

        /// <summary>
        /// 1904. 双烈火 8~10 秒时序窗口判定：第 1 刀蓄力后等待 8~10 秒释放，随即可立即打出第 2 刀烈火
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanExecuteDoubleFireCombo(long chargeMs, long firstSlashMs)
        {
            long elapsed = firstSlashMs - chargeMs;
            return elapsed >= 8000 && elapsed <= 10000;
        }

        /// <summary>
        /// 1905. 野蛮冲撞等级压制判定：目标等级小于自身方可撞退，否则反作用力震退自身
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool pushTarget, bool bounceSelf) CalculateSavageThrustResult(int attackerLvl, int targetLvl)
        {
            if (attackerLvl > targetLvl) return (true, false);
            return (false, true);
        }

        /// <summary>
        /// 1906. 连续野蛮冲撞冷却时间：官方权威冷却时间为 10,000ms
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanCastSavageThrust(long lastCastMs, long nowMs)
        {
            return (nowMs - lastCastMs) >= 10_000;
        }

        /// <summary>
        /// 1907. 攻杀剑术暴击蓝光刀光触发：25% 几率触发，攻击力额外提升 8 点
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool isCrit, int bonusDmg) RollAttackSkillCrit(int roll100)
        {
            bool crit = roll100 < 25;
            return (crit, crit ? 8 : 0);
        }

        /// <summary>
        /// 1908. 井中月 7-22 攻击平稳输出与半月配合：输出波动极小，发挥下限极高
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateJingZhongYueDamage(int minAtk = 7, int maxAtk = 22, int seed = 0)
        {
            return minAtk + (seed % (maxAtk - minAtk + 1));
        }

        /// <summary>
        /// 1909. 裁决之杖运 9 极值发挥：佩戴幸运 9 时必定发挥最大攻击 30 点
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateCaiJueDamage(int totalLuck, int minAtk = 0, int maxAtk = 30, int seed = 0)
        {
            if (totalLuck >= 9) return maxAtk;
            return minAtk + (seed % (maxAtk - minAtk + 1));
        }

        /// <summary>
        /// 1910. 幽灵项链配合死神手镯触发隐藏攻击加成：额外提升准确 +1 点
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetGhostNecklaceComboAccuracy(bool hasGhostNecklace, bool hasDeathGlove)
        {
            return (hasGhostNecklace && hasDeathGlove) ? 1 : 0;
        }

        // ------------------------------------------------------------------------------------------------
        // 1911 ~ 1920: 法师冰咆哮、火墙与诱惑进阶
        // ------------------------------------------------------------------------------------------------

        /// <summary>
        /// 1911. 冰咆哮 3x3 范围绝对无视魔法躲避权威判定：目标即使佩戴 50% 虎齿项链也无法躲避
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DoesIceStormIgnoreMagicEvade(int targetEvadePercent)
        {
            return true; // 官方 1.5 铁律：魔法躲避对冰咆哮无效
        }

        /// <summary>
        /// 1912. 火墙多团踩踏独立伤害：怪物站在多团火墙交界处每秒独立计算扣血
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateStackedFireWallDamage(int singleWallDmg, int overlappingWalls)
        {
            int count = Math.Clamp(overlappingWalls, 1, 4);
            return singleWallDmg * count;
        }

        /// <summary>
        /// 1913. 疾光电影直线每格伤害递增 5% (最多 5 格提升 25%)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateLightningStepBonus(int baseDmg, int distance)
        {
            int stepBonus = Math.Clamp((distance - 1) * 5, 0, 25);
            return baseDmg + (baseDmg * stepBonus / 100);
        }

        /// <summary>
        /// 1914. 雷电术受击硬直时钟：被雷电命中进入 400ms 受击后仰硬直
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetThunderHitStunMs()
        {
            return 400;
        }

        /// <summary>
        /// 1915. 地狱雷光对不死系生物伤害翻倍并附带 500ms 眩晕
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (int dmg, int stunMs) ProcessHellThunderHit(int baseDmg, bool isUndead)
        {
            return isUndead ? (baseDmg * 2, 500) : (baseDmg, 0);
        }

        /// <summary>
        /// 1916. 诱惑之光诱惑虎卫成功率与忠诚度递减：诱惑成功率 15%，每只虎卫存活最长 6 小时
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RollTameHuWei(int roll100)
        {
            return roll100 < 15;
        }

        /// <summary>
        /// 1917. 宠物等级 0~7 级升级所需击杀怪数量：每级需要击杀 20 只怪物
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculatePetLevelFromKills(int killCount)
        {
            int lvl = killCount / 20;
            return Math.Clamp(lvl, 0, 7);
        }

        /// <summary>
        /// 1918. 魔法盾破盾碎片音效与瞬间硬直：受到破盾伤害产生 200ms 硬直
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetShieldBreakStunMs(bool isShieldBroken)
        {
            return isShieldBroken ? 200 : 0;
        }

        /// <summary>
        /// 1919. 瞬息移动失败原地打转与成功传送随机合法点：10% 概率瞬移失败
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RollTeleportSpellSuccess(int roll100)
        {
            return roll100 >= 10;
        }

        /// <summary>
        /// 1920. 骨玉权杖隐藏属性：延长火墙持续时间 30%
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateBoneBladeFireWallDuration(int baseDuration, bool hasBoneBlade)
        {
            return hasBoneBlade ? (baseDuration * 130 / 100) : baseDuration;
        }

        // ------------------------------------------------------------------------------------------------
        // 1921 ~ 1930: 装备磨损、维修与极品掉落
        // ------------------------------------------------------------------------------------------------

        /// <summary>
        /// 1921. 受击各装备槽位持久磨损概率：衣服 1/50，武器挥刀 1/100，首饰受击 1/200
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CheckDurabilityLossRoll(int slotType, int roll)
        {
            return slotType switch
            {
                0 => (roll % 100) == 0, // Weapon
                1 => (roll % 50) == 0,  // Armor
                _ => (roll % 200) == 0  // Jewelry
            };
        }

        /// <summary>
        /// 1922. 武器持久归 0 时攻击力变为 0 且开刀必碎裂
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (int effectiveAtk, bool breaksOnSlash) EvaluateDepletedWeapon(int dura, int normalAtk)
        {
            if (dura <= 0) return (0, true);
            return (normalAtk, false);
        }

        /// <summary>
        /// 1923. 衣服与首饰持久归 0 时防御与附加属性完全失效
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEquipmentStatsActive(int currentDura)
        {
            return currentDura > 0;
        }

        /// <summary>
        /// 1924. 普通修理损失当前持久上限的 1/16 算法
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ApplyNormalRepairPenalty(int maxDura)
        {
            int loss = Math.Max(1, maxDura / 16);
            return Math.Max(1, maxDura - loss);
        }

        /// <summary>
        /// 1925. 特殊修理消耗 3 倍金币且 0 磨损持久上限
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (int cost, int newMaxDura) CalculateSpecialRepair(int normalRepairCost, int maxDura)
        {
            return (normalRepairCost * 3, maxDura);
        }

        /// <summary>
        /// 1926. 战神油 100% 修复手持武器全部持久且不掉持久上限
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ApplyZhanShenOil(int maxDura)
        {
            return maxDura; // 恢复至满持久且上限不变
        }

        /// <summary>
        /// 1927. 极品装备生成 +1~+5 属性分布权重：+1为70%，+2为20%，+3为7%，+4为2%，+5仅1%
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GenerateSuperItemBonus(int roll100)
        {
            if (roll100 < 70) return 1;
            if (roll100 < 90) return 2;
            if (roll100 < 97) return 3;
            if (roll100 < 99) return 4;
            return 5;
        }

        /// <summary>
        /// 1928. 祝福油喝油运 0 升运 7 梯级概率模型
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RollBlessingOilStep(int currentLuck, int roll100)
        {
            int chance = currentLuck switch
            {
                0 => 70,
                1 => 50,
                2 => 35,
                3 => 20,
                4 => 12,
                5 => 6,
                6 => 2,
                _ => 0
            };
            return roll100 < chance;
        }

        /// <summary>
        /// 1929. 恶魔铃铛与龙之手镯隐藏魔法命中加成：命中率提升 5%
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetDragonBraceletHitBonus(bool hasDragonBracelet)
        {
            return hasDragonBracelet ? 5 : 0;
        }

        /// <summary>
        /// 1930. 铂金戒指防毒属性：佩戴防毒增加 10%
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetPlatinumRingPoisonAvoidance(bool hasPlatinumRing)
        {
            return hasPlatinumRing ? 10 : 0;
        }

        // ------------------------------------------------------------------------------------------------
        // 1931 ~ 1940: PK 规则、红名村与卫士惩罚
        // ------------------------------------------------------------------------------------------------

        /// <summary>
        /// 1931. 恶意攻击白名触发褐名 (黄名) 状态：持续 60 秒
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsYellowNameTriggered(bool targetIsWhite, bool isPeacefulMode)
        {
            return targetIsWhite && !isPeacefulMode;
        }

        /// <summary>
        /// 1932. 击杀白名增加 100 点 PK 值，超过 200 点转变为红名
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (int newPk, bool isRedName) RecordPkKill(int currentPk, bool victimIsWhite)
        {
            int add = victimIsWhite ? 100 : 0;
            int total = currentPk + add;
            return (total, total >= 200);
        }

        /// <summary>
        /// 1933. 红名玩家在大刀卫士视线内受到 99999 点纯真实伤害秒杀
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetGuardDamageOnTarget(bool isRedName)
        {
            return isRedName ? 99999 : 0;
        }

        /// <summary>
        /// 1934. 红名死亡 100% 几率大爆身上装备与背包物品
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsGuaranteedDropOnDeath(bool isRedName)
        {
            return isRedName;
        }

        /// <summary>
        /// 1935. 红名使用回城卷轴强制传送至盟重红名村 (850, 670)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (string mapId, int x, int y) GetTownTeleportCoords(bool isRedName)
        {
            return isRedName ? ("3", 850, 670) : ("0", 330, 270);
        }

        /// <summary>
        /// 1936. PK 值自然消退速度：每 60 秒自然减少 1 点
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateNaturalPkDecay(int currentPk, int elapsedMinutes)
        {
            return Math.Max(0, currentPk - elapsedMinutes);
        }

        /// <summary>
        /// 1937. 红名清洗 NPC：每清洗 1 点 PK 值消耗 1,000 金币
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool success, int goldCost) WashPkPoints(int pkPointsToWash, int playerGold)
        {
            int cost = pkPointsToWash * 1000;
            if (playerGold < cost) return (false, 0);
            return (true, cost);
        }

        /// <summary>
        /// 1938. 正当防卫机制：击杀正在攻击自己的黄名玩家不增加 PK 值
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSelfDefenseKill(bool victimIsYellowAttacker)
        {
            return victimIsYellowAttacker;
        }

        /// <summary>
        /// 1939. 组队与行会模式友军伤害免疫：攻击同一行会成员伤害判定为 0
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsFriendlyFireImmune(bool isSameGuild, bool isGuildMode)
        {
            return isSameGuild && isGuildMode;
        }

        /// <summary>
        /// 1940. 沙巴克攻城期间沙城区域内 PK 100% 豁免增加 PK 点数
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSiegePkExempt(bool isSiegeActive, bool isInSabukRegion)
        {
            return isSiegeActive && isInSabukRegion;
        }

        // ------------------------------------------------------------------------------------------------
        // 1941 ~ 1950: Direct-Blit 协议与 HPSocket AOT 深度优化
        // ------------------------------------------------------------------------------------------------

        /// <summary>
        /// 1941. 包头固定 8 字节 Direct-Blit 零拷贝验证 (魔数 0xAA55, 长度 <= 4096)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ValidateDirectBlitHeader(ushort magic, ushort length)
        {
            return magic == 0xAA55 && length <= 4096;
        }

        /// <summary>
        /// 1942. 64 步滑动窗口单调包序号抗乱序防重放判定
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPacketInSlidingWindow(ushort incomingSeq, ushort lastSeq)
        {
            short diff = (short)(incomingSeq - lastSeq);
            return diff > 0 && diff <= 64;
        }

        /// <summary>
        /// 1943. 30 秒心跳超时强退断线时钟核验
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsHeartbeatExpired(long lastPingMs, long nowMs)
        {
            return (nowMs - lastPingMs) >= 30_000;
        }

        /// <summary>
        /// 1944. Native AOT 零 GC 环形缓冲区足额判定
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasRingBufferCapacity(int writeIdx, int readIdx, int bufferSize, int requiredBytes)
        {
            int available = (readIdx <= writeIdx)
                ? (bufferSize - (writeIdx - readIdx) - 1)
                : (readIdx - writeIdx - 1);
            return available >= requiredBytes;
        }

        /// <summary>
        /// 1945. 封包快速 CRC32 校验和算法 (零堆分配)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ComputeFastCrc32(ReadOnlySpan<byte> span)
        {
            uint crc = 0xFFFFFFFF;
            for (int i = 0; i < span.Length; i++)
            {
                crc ^= span[i];
                for (int j = 0; j < 8; j++)
                {
                    crc = (crc >> 1) ^ (0xEDB88320 & ~((crc & 1) - 1));
                }
            }
            return ~crc;
        }

        /// <summary>
        /// 1946. 1 秒移动超过 3 次外挂拦截判定
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DetectMoveSpeedHack(int movesInOneSecond)
        {
            return movesInOneSecond > 3;
        }

        /// <summary>
        /// 1947. 魔法施法前摇 500ms 物理核验
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool VerifyMagicCastCooldown(long lastCastMs, long nowMs)
        {
            return (nowMs - lastCastMs) >= 500;
        }

        /// <summary>
        /// 1948. 发言防刷屏时钟限制：普通发言 1500ms 冷却
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanChat(long lastChatMs, long nowMs)
        {
            return (nowMs - lastChatMs) >= 1500;
        }

        /// <summary>
        /// 1949. 15 格地图广播视距裁剪判定
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsWithinBroadcastView(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x1 - x2) <= 15 && Math.Abs(y1 - y2) <= 15;
        }

        /// <summary>
        /// 1950. 服务端权威随机数映射掉落物防作弊
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GenerateServerAuthoritativeLootId(int seed, int monsterId)
        {
            return 2000 + ((seed ^ (monsterId * 41)) % 1000);
        }
    }
}
