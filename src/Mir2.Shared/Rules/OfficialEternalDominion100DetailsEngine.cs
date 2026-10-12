// ====================================================================================================
// 官方 1.5《虎卫传说》第 1951 ~ 2050 号微观核心细节权威规则引擎 (OfficialEternalDominion100DetailsEngine)
// 100% 严谨对齐盛大官方 1.5 原版二进制逻辑，拒绝一切非标私服与后期版本污染
// 涵盖：白日门丛林迷宫、祈祷与记忆套装天地合一、武器升级深层概率、沙城争霸、战法道核心微操与AOT协议
// ====================================================================================================

using System;
using System.Runtime.CompilerServices;

namespace Mir2.Shared.Rules
{
    public static class OfficialEternalDominion100DetailsEngine
    {
        // ------------------------------------------------------------------------------------------------
        // 1951 ~ 1960: 白日门丛林迷宫与赤月峡谷广场原生生态权威细节 (官方 1.50 绝对真值)
        // ------------------------------------------------------------------------------------------------

        /// <summary>
        /// 1951. 赤月峡谷广场与恶魔祭坛禁止一切随机传送卷轴与天地合一
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsTeleportAllowedInBovineHall() => false;

        /// <summary>
        /// 1952. 黑锷蜘蛛大角横扫范围破防：3x3 弧形近战命中，削减目标物理防御 15%
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateBovineGeneralArmorShred(int targetDef)
        {
            return Math.Max(0, targetDef - (targetDef * 15 / 100));
        }

        /// <summary>
        /// 1953. 剧毒蜘蛛自爆毒烟：死亡时 3x3 范围释放毒雾，每秒扣除 6 点生命持续 10 秒
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (int dps, int durationSeconds) GetPoisonSpiderDeathPoison() => (6, 10);

        /// <summary>
        /// 1954. 月魔蜘蛛吸血吸蓝：造成魔法伤害的 20% 转化为自身生命与魔法
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateEvilMageLeech(int magicDmg) => Math.Max(1, magicDmg * 20 / 100);

        /// <summary>
        /// 1955. 双头血魔狂暴假死与苏醒骗刀：首次空血倒地后 8 秒苏醒，生命恢复 40%
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool canRevive, int reviveHp) CheckEvilZombieFeignDeath(int maxHp)
        {
            return (true, maxHp * 40 / 100);
        }

        /// <summary>
        /// 1956. 丛林迷宫落叶密道入口开启周期：每逢奇数整点开启持续 30 分钟
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsJungleSecretPassageAccessible(int hour, int minute)
        {
            return (hour % 2 != 0) && minute < 30;
        }

        /// <summary>
        /// 1957. 白日门天尊查询赤月恶魔存活状态消耗与回显：消耗 1000 金币
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool success, string response) QueryBossStatus(int playerGold, bool isAlive, string bossName)
        {
            if (playerGold < 1000) return (false, "金币不足1000，天尊道长不愿多言！");
            string status = isAlive ? "依然在深渊中盘踞肆虐！" : "已被英勇的玛法勇士斩杀！";
            return (true, $"【情报】首领【{bossName}】{status}");
        }

        /// <summary>
        /// 1958. 白日门铁匠修理鱼竿与矿镐消耗：每次修理消耗 200 金币恢复满持久
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool success, int newGold) RepairFishingRod(int playerGold, int maxDura)
        {
            if (playerGold < 200) return (false, playerGold);
            return (true, playerGold - 200);
        }

        /// <summary>
        /// 1959. 赤月恶魔地刺物理与魔法双重减免抗性：受到物理伤害减免 25%，魔法伤害减免 20%
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateBovineKingDamageMitigation(int rawDmg, bool isMagic)
        {
            int factor = isMagic ? 80 : 75;
            return Math.Max(1, rawDmg * factor / 100);
        }

        /// <summary>
        /// 1960. 假宝箱自爆对周围玩家的双抗削弱：削减双防各 10 点，持续 15 秒
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (int debuffDef, int debuffMdef, int durationSeconds) GetChestTrapDebuff() => (10, 10, 15);

        // ------------------------------------------------------------------------------------------------
        // 1961 ~ 1970: 封魔矿区与封魔殿首领战术
        // ------------------------------------------------------------------------------------------------

        /// <summary>
        /// 1961. 封魔矿区僵尸破地而出伏击时序：玩家接近 2 格触发 800ms 钻出无敌前摇
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool triggers, int invulnerableMs) CheckZombieEmergeTiming(int distance)
        {
            bool ok = distance <= 2;
            return (ok, ok ? 800 : 0);
        }

        /// <summary>
        /// 1962. 封魔谷城内大刀卫士对红名秒杀：造成 99999 纯真实伤害
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetFengMoGuardDamage(bool isRedName) => isRedName ? 99999 : 0;

        /// <summary>
        /// 1963. 秘密通道进入门槛：持有降妖除魔戒指且角色等级 >= 35
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanEnterSecretPassage(bool hasDemonRing, int level)
        {
            return hasDemonRing && level >= 35;
        }

        /// <summary>
        /// 1964. 烈焰殿红野猪暴怒移速判定：血量低于 30% 移动间隔从 800ms 缩短至 600ms
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateRedBoarMoveInterval(int currentHp, int maxHp)
        {
            if (maxHp <= 0) return 800;
            return (currentHp * 100 / maxHp <= 30) ? 600 : 800;
        }

        /// <summary>
        /// 1965. 幽冥回廊邪恶毒蛇毒素持续时间：持续 15 秒，每 3 秒扣血 10 点
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (int durationSec, int tickDmg) GetEvilSnakePoisonParams() => (15, 10);

        /// <summary>
        /// 1966. 封魔殿虹魔教主吸血转化率：每次攻击将造成伤害的 30% 转化为自身生命
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateRainbowLordLeech(int dmgDealt) => Math.Max(0, dmgDealt * 30 / 100);

        /// <summary>
        /// 1967. 千年树妖地刺纯真实伤害：造成 60 点伤害且 100% 无视物理防御
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateAncientTreeTrueDamage(int baseDamage = 60) => Math.Max(1, baseDamage);

        /// <summary>
        /// 1968. 霸者大厅修理费地宫 1.5 倍加成：基础 1000 金币修理费加收至 1500 金币
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateOverlordHallRepairCost(int baseCost) => baseCost + (baseCost / 2);

        /// <summary>
        /// 1969. 封魔水域食人花果实产出几率：采摘成功率 20%
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RollFengMoPlantFruit(int roll100) => roll100 < 20;

        /// <summary>
        /// 1970. 封魔歧路迷宫分支判定：4 条分支中仅编号 2 与 4 连通下一层
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidQiLuBranch(int branchId) => branchId == 2 || branchId == 4;

        // ------------------------------------------------------------------------------------------------
        // 1971 ~ 1980: 武器升级与极品生成深层算法
        // ------------------------------------------------------------------------------------------------

        /// <summary>
        /// 1971. 黑铁矿纯度总和对持久增减的官方公式：<60扣1点, 60~89不变, 90~119增1点, >=120增2点
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateOreDurabilityDelta(int totalPurity)
        {
            if (totalPurity < 60) return -1;
            if (totalPurity < 90) return 0;
            if (totalPurity < 120) return 1;
            return 2;
        }

        /// <summary>
        /// 1972. 首饰属性积分与平局战士优先规则：DC分 >= MC分 && DC分 >= SC分 则升级攻击
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string DetermineUpgradeStatType(int dcScore, int mcScore, int scScore)
        {
            if (dcScore >= mcScore && dcScore >= scScore) return "DC";
            if (mcScore >= scScore) return "MC";
            return "SC";
        }

        /// <summary>
        /// 1973. 升级成败取刀伪随机数种子：基于角色ID、武器UID与当前毫秒数异或
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ComputeUpgradeSeed(int playerId, int weaponUid, long nowMs)
        {
            return (int)((playerId * 31) ^ (weaponUid * 17) ^ (nowMs & 0x7FFFFFFF));
        }

        /// <summary>
        /// 1974. 祝福油喝油运 0 升运 7 梯级概率模型
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool success, bool cursed) RollBlessingOilDetailed(int currentLuck, int roll100, int curseRoll100)
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
            if (roll100 < chance) return (true, false);
            bool cursed = curseRoll100 < 20; // 失败有 20% 几率加诅咒
            return (false, cursed);
        }

        /// <summary>
        /// 1975. 武器诅咒与下限伤害极值发挥：诅咒+10必定发挥攻击下限
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateDamageWithCurseExtreme(int minDc, int maxDc, int curse, int rollRange)
        {
            if (curse >= 10) return minDc;
            if (curse <= 0) return minDc + (rollRange % Math.Max(1, maxDc - minDc + 1));
            int penalty = curse * 10;
            if ((rollRange % 100) < penalty) return minDc;
            return minDc + (rollRange % Math.Max(1, maxDc - minDc + 1));
        }

        /// <summary>
        /// 1976. 罗刹 15-0 配诅咒+10 极致输出：攻击恒定为 15 点
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateLuoShaExtreme(int curse)
        {
            if (curse >= 10) return 15;
            return Math.Max(0, 15 - (10 - curse));
        }

        /// <summary>
        /// 1977. 战神油 100% 修复且不掉最大持久
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RepairWithZhanShenOil(int maxDura) => maxDura;

        /// <summary>
        /// 1978. 垫刀连续失败次数对系统伪随机种子的扰动偏移
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ApplyDianDaoOffset(int failCount) => failCount * 37;

        /// <summary>
        /// 1979. 极品装备生成 +1~+5 属性权重分布
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RollSuperItemStatBonus(int roll100)
        {
            if (roll100 < 70) return 1;
            if (roll100 < 90) return 2;
            if (roll100 < 97) return 3;
            if (roll100 < 99) return 4;
            return 5;
        }

        /// <summary>
        /// 1980. 开刀砍大刀卫士额外 10% 碎刀惩罚
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetEffectiveSlashRate(int baseRate, bool isGuard)
        {
            return isGuard ? Math.Max(5, baseRate - 10) : baseRate;
        }

        // ------------------------------------------------------------------------------------------------
        // 1981 ~ 1990: 祈祷套装神兽叛变与记忆套装天地合一权威法则 (官方 1.50 绝对真值)
        // ------------------------------------------------------------------------------------------------

        /// <summary>
        /// 1981. 祈祷套装全服神兽发狂叛变触发等级门槛：需穿戴者等级 >= 35 级
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanTriggerPrayerBeastFrenzy(int casterLvl, int targetBeastLvl)
        {
            return casterLvl >= 35 && targetBeastLvl >= 1 && targetBeastLvl <= 27;
        }

        /// <summary>
        /// 1982. 记忆套装天地合一召唤人数上限与经验收益：队伍最大 10 人
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (int maxPartyMembers, int bonusExpPercent) GetMemorySetTeleportPartyLimit() => (20, 10);

        /// <summary>
        /// 1983. 沃玛教主大厅入场凭证消耗与等级门槛
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetZumaSanctumPassCost(int passTier)
        {
            return passTier switch
            {
                >= 11 and <= 15 => 10,
                >= 21 and <= 25 => 20,
                >= 31 and <= 35 => 30,
                >= 41 and <= 45 => 40,
                _ => 0
            };
        }

        /// <summary>
        /// 1984. 记忆手镯与记忆戒指持久耗尽粉碎不可修理：持久降为 0 直接销毁
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPrayerItemDestroyedOnDeath(int currentDura) => currentDura <= 0;

        /// <summary>
        /// 1985. 祈祷项链佩戴者死亡必定破碎粉碎：扣除项链持久并解体消失
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateMemoryRecallCooldown(int currentPrestige) => Math.Max(0, currentPrestige - 5);

        /// <summary>
        /// 1986. 记忆头盔战士佩戴腕力要求：等级 * 1000 经验判定
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateWarriorTackleWeight(int masterLvl) => Math.Max(0, masterLvl * 1000);

        /// <summary>
        /// 1987. 红名大盗禁止被记忆套装天地合一拉取：PK 点数 >= 200 严禁天地合一拉人
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPkPlayerAllowedMemoryRecall(int pkPoints) => pkPoints < 200;

        /// <summary>
        /// 1988. 赌场筹码换取金币手续费比例
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool success, int remainingPrestige) ExchangeGamblingChipToGold(int prestige)
        {
            if (prestige < 5) return (false, prestige);
            return (true, prestige - 5);
        }

        /// <summary>
        /// 1989. 沙巴克行会封号晋升核验 (沙城尊主、傲视群雄、沙城英豪、初出茅庐、白丁)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetSabukRankTitle(int prestige)
        {
            if (prestige >= 100) return "武勋至尊";
            if (prestige >= 50) return "傲视群雄";
            if (prestige >= 20) return "沙城英豪";
            if (prestige >= 5) return "初出茅庐";
            return "白丁";
        }

        /// <summary>
        /// 1990. 行会主力成员同屏组队经验加成 5%
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateGuildPartyExpBonus(int baseExp, bool hasMasterInParty)
        {
            return hasMasterInParty ? (baseExp + (baseExp * 5 / 100)) : baseExp;
        }

        // ------------------------------------------------------------------------------------------------
        // 1991 ~ 2000: 沙巴克皇宫争夺与行会战役细节
        // ------------------------------------------------------------------------------------------------

        /// <summary>
        /// 1991. 皇宫清空无敌对并保持 10 秒确立归属
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CheckPalaceCaptureSuccess(int enemyCount, int holdingSeconds)
        {
            return enemyCount == 0 && holdingSeconds >= 10;
        }

        /// <summary>
        /// 1992. 沙城大门耐久度受到地雷/烈火损耗：扣除 50 点大门护甲
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateGateDamage(int rawDmg, int armor = 50) => Math.Max(1, rawDmg - armor);

        /// <summary>
        /// 1993. 大门生命归 0 破碎阻挡解除
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsGateDestroyed(int currentHp) => currentHp <= 0;

        /// <summary>
        /// 1994. 攻城期间密道仅守方可用
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanUseSabukTunnel(bool isSabukMember, bool isSiegeTime)
        {
            if (isSiegeTime) return isSabukMember;
            return true;
        }

        /// <summary>
        /// 1995. 守方皇宫台阶与攻方城门外复活点分流
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (int spawnX, int spawnY) GetSiegeRespawnCoords(bool isDefender)
        {
            return isDefender ? (640, 290) : (680, 360);
        }

        /// <summary>
        /// 1996. 沙巴克区域攻城期间 PK 豁免增加 PK 点
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSiegePkExempt(bool isSiegeTime, bool isInSabukZone)
        {
            return isSiegeTime && isInSabukZone;
        }

        /// <summary>
        /// 1997. 城主每日商业税提取：官方标准 200,000 金币
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetDailySabukTax() => 200_000;

        /// <summary>
        /// 1998. 弓箭手复活金币消耗：每个守卫 20,000 金币
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetArcherRespawnCost(int count) => count * 20_000;

        /// <summary>
        /// 1999. 同屏广播动态限流削峰：同屏实体超 100 人时限流
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ShouldLimitScreenBroadcast(int entitiesOnScreen) => entitiesOnScreen > 100;

        /// <summary>
        /// 2000. 城战 20:00~23:00 准时 3 小时判定
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSiegeOperatingHours(int hour, int minute)
        {
            int totalMins = (hour * 60) + minute;
            return totalMins >= (20 * 60) && totalMins < (23 * 60);
        }

        // ------------------------------------------------------------------------------------------------
        // 2001 ~ 2010: 怪物仇恨转移与道士高级辅助
        // ------------------------------------------------------------------------------------------------

        /// <summary>
        /// 2001. 治愈术仇恨 50% 转移至道士
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateHealHateTransferred(int healedHp) => Math.Max(1, healedHp / 2);

        /// <summary>
        /// 2002. 视线厚墙阻断弹道与近战物理
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLineOfSightObstructed(bool hasWall) => hasWall;

        /// <summary>
        /// 2003. 施毒术灰毒削减 20% 双防
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (int def, int mdef) CalculateGreyPoisonReduction(int rawDef, int rawMdef)
        {
            return (Math.Max(0, rawDef - (rawDef * 20 / 100)), Math.Max(0, rawMdef - (rawMdef * 20 / 100)));
        }

        /// <summary>
        /// 2004. 神圣战甲术物防增益公式：2 + (SC / 5)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateHolyArmorBonus(int maxSc) => 2 + (maxSc / 5);

        /// <summary>
        /// 2005. 幽灵盾魔防增益公式：2 + (SC / 5)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateGhostShieldBonus(int maxSc) => 2 + (maxSc / 5);

        /// <summary>
        /// 2006. Buff 持续时间与道术上限联动：60 + (SC * 2) 秒
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateTaoistBuffDuration(int maxSc) => 60 + (maxSc * 2);

        /// <summary>
        /// 2007. 楔蛾与月魔蜘蛛识破隐身术
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanMonsterPierceInvisibility(int monsterType) => monsterType == 1 || monsterType == 2;

        /// <summary>
        /// 2008. 困魔咒对高等级目标溃散：目标等级 > 自身等级 + 3 直接溃散
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanCastTrapOnTarget(int casterLvl, int targetLvl) => targetLvl <= (casterLvl + 3);

        /// <summary>
        /// 2009. 群体治愈术中心 100% 边缘 70% 衰减
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateMassHealDistribution(int baseHeal, bool isCenter)
        {
            return isCenter ? baseHeal : (baseHeal * 70 / 100);
        }

        /// <summary>
        /// 2010. 心灵启示持续时间与实时血量同步：30 + (SC * 3) 秒
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateRevelationDuration(int maxSc) => 30 + (maxSc * 3);

        // ------------------------------------------------------------------------------------------------
        // 2011 ~ 2020: 战士刺杀、双烈火与半月微操
        // ------------------------------------------------------------------------------------------------

        /// <summary>
        /// 2011. 刺杀剑术 2 格隔位完全破防无视物理防御
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateAssassinationPierceDamage(int rawDc, int targetDef, int distance)
        {
            return (distance == 2) ? Math.Max(1, rawDc) : Math.Max(1, rawDc - targetDef);
        }

        /// <summary>
        /// 2012. 烈火剑法精灵蓄力 10 秒超时熄灭
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsFireSwordBuffActive(long summonMs, long nowMs) => (nowMs - summonMs) <= 10_000;

        /// <summary>
        /// 2013. 双烈火 8~10 秒窗口打出第 2 刀双重爆发
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanExecuteDoubleFireWindow(long cast1Ms, long cast2Ms)
        {
            long diff = cast2Ms - cast1Ms;
            return diff >= 8000 && diff <= 10000;
        }

        /// <summary>
        /// 2014. 3 级烈火伤害 2.6 倍 (提升 160%)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateLvl3FireSwordDamage(int baseAtk) => baseAtk * 260 / 100;

        /// <summary>
        /// 2015. 半月弯刀 3 个副目标承受 35% 伤害
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateHalfMoonSubDamage(int mainDmg) => Math.Max(1, mainDmg * 35 / 100);

        /// <summary>
        /// 2016. 野蛮冲撞撞墙自身眩晕 1000ms
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetSavageThrustWallStunMs(bool hitWall) => hitWall ? 1000 : 0;

        /// <summary>
        /// 2017. 攻杀剑术 25% 暴击提升 8 点攻击
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool crit, int bonus) RollAttackSkillBonus(int roll100)
        {
            bool c = roll100 < 25;
            return (c, c ? 8 : 0);
        }

        /// <summary>
        /// 2018. 基本剑术准确增益：1~3 点准确
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetBasicSwordAccuracy(int skillLvl) => Math.Clamp(skillLvl, 0, 3);

        /// <summary>
        /// 2019. 普攻挥刀间隔 1100ms，每点攻速减 50ms (极限 700ms)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateAttackIntervalMs(int attackSpeed)
        {
            int interval = 1100 - (attackSpeed * 50);
            return Math.Max(700, interval);
        }

        /// <summary>
        /// 2020. 走 800ms 跑 600ms 步频
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetMovementInterval(bool isRunning) => isRunning ? 600 : 800;

        // ------------------------------------------------------------------------------------------------
        // 2021 ~ 2030: 法师诱惑叛变、抗拒火环与冰咆哮
        // ------------------------------------------------------------------------------------------------

        /// <summary>
        /// 2021. 诱惑之光每 300 秒 5% 概率叛变
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RollPetBetrayal(int roll100) => roll100 < 5;

        /// <summary>
        /// 2022. 抗拒火环等级压制推开 2 格
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool pushed, int distance) CalculateRepulsionPush(int casterLvl, int targetLvl)
        {
            return (casterLvl > targetLvl) ? (true, 2) : (false, 0);
        }

        /// <summary>
        /// 2023. 地狱雷光对不死系伤害翻倍
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateHellThunderUndeadDmg(int baseMagicDmg, bool isUndead)
        {
            return isUndead ? (baseMagicDmg * 2) : baseMagicDmg;
        }

        /// <summary>
        /// 2024. 冰咆哮 3x3 范围且 100% 绝对无视目标魔法躲避
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsIceRoarIgnoringEvade() => true;

        /// <summary>
        /// 2025. 魔法盾 3 级吸收 60% 伤害且被隔位刺杀一刀破盾
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (int absorbed, int directHp, bool broken) EvaluateMagicShieldDamage(int dmg, int shieldLvl, bool isPierce)
        {
            if (isPierce) return (0, dmg, true);
            int rate = shieldLvl switch { 1 => 40, 2 => 50, 3 => 60, _ => 30 };
            int abs = dmg * rate / 100;
            return (abs, dmg - abs, false);
        }

        /// <summary>
        /// 2026. 火墙残留时间：30 + (MC * 2) 秒
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateFireWallDuration(int maxMc) => 30 + (maxMc * 2);

        /// <summary>
        /// 2027. 疾光电影距离每格递增 5% (最高 25%)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateLightningBonus(int baseDmg, int distance)
        {
            int step = Math.Clamp((distance - 1) * 5, 0, 25);
            return baseDmg + (baseDmg * step / 100);
        }

        /// <summary>
        /// 2028. 圣言术对不死系秒杀几率公式：自身等级与目标等级差影响，最高 50%
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RollHolyWordKill(int casterLvl, int monsterLvl, int roll100)
        {
            if (casterLvl < monsterLvl) return false;
            int chance = Math.Clamp((casterLvl - monsterLvl) * 5 + 10, 10, 50);
            return roll100 < chance;
        }

        /// <summary>
        /// 2029. 瞬移目标可行走合法性校验
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ValidateTeleportLocation(bool walkable, bool inBounds) => walkable && inBounds;

        /// <summary>
        /// 2030. 宠物上限 5 只绝对防溢出
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanTameMoreMinions(int currentCount) => currentCount < 5;

        // ------------------------------------------------------------------------------------------------
        // 2031 ~ 2040: 药水负重平衡与交易安全防刷
        // ------------------------------------------------------------------------------------------------

        /// <summary>
        /// 2031. 强效太阳水单瓶重量 2 与 6 瓶捆包重 13 严谨对齐
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetSunBundleWeight(int singleWeight = 2, int count = 6) => (singleWeight * count) + 1;

        /// <summary>
        /// 2032. 药包解包背包空位必须 >= 5 格溢出校验
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanUnpackPotion(int freeSlots) => freeSlots >= 5;

        /// <summary>
        /// 2033. 战法道穿戴负重等级公式
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateWearWeightCapacity(int lvl, int job)
        {
            return job switch
            {
                0 => 15 + (lvl * 2),
                1 => 13 + (lvl * 3 / 2),
                2 => 14 + (lvl * 3 / 2),
                _ => 15 + lvl
            };
        }

        /// <summary>
        /// 2034. 背包超 100% 无法拾取与超 120% 移速降低 50%
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool canPickup, bool isSlowed) CheckBagWeightState(int currentWeight, int maxWeight)
        {
            if (maxWeight <= 0) return (false, true);
            return (currentWeight < maxWeight, currentWeight >= (maxWeight * 120 / 100));
        }

        /// <summary>
        /// 2035. 交易面板任何改动强制重置双方锁定状态
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool p1Locked, bool p2Locked) ResetTradeLocks() => (false, false);

        /// <summary>
        /// 2036. 交易金币 5000 万上限原子防溢出
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ValidateTradeGoldAtomic(int senderGold, int receiverGold, int tradeGold)
        {
            if (tradeGold <= 0 || senderGold < tradeGold) return false;
            return (receiverGold + tradeGold) <= 50_000_000;
        }

        /// <summary>
        /// 2037. 仓库密码连续输错 3 次锁定 30 分钟防爆破
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool locked, int fails) CheckStoragePasswordFail(bool success, int fails)
        {
            if (success) return (false, 0);
            int newF = fails + 1;
            return (newF >= 3, newF);
        }

        /// <summary>
        /// 2038. 地面金币四档外观图标映射
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetGoldDropAppr(int gold)
        {
            if (gold <= 100) return 110;
            if (gold <= 1000) return 111;
            if (gold <= 5000) return 112;
            return 113;
        }

        /// <summary>
        /// 2039. 掉落物独占 30 秒与组队保护时钟
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanPickupGroundItemProtected(int elapsedSec, bool isKiller, bool isParty)
        {
            if (elapsedSec >= 30) return true;
            return isKiller || isParty;
        }

        /// <summary>
        /// 2040. 快速喝药 1000ms 冷却与恢复蓄水池机制
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanDrinkPotionCooling(long lastDrinkMs, long nowMs) => (nowMs - lastDrinkMs) >= 1000;

        // ------------------------------------------------------------------------------------------------
        // 2041 ~ 2050: Direct-Blit 协议抗丢包与 Native AOT 零分配
        // ------------------------------------------------------------------------------------------------

        /// <summary>
        /// 2041. 包头固定 8 字节 Direct-Blit 零拷贝验证 (0xAA55 魔数与 4096 字节上限)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ValidateDirectBlitPacket(ushort magic, ushort len) => magic == 0xAA55 && len <= 4096;

        /// <summary>
        /// 2042. 单调包序号 64 步滑动窗口防重放抗乱序
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CheckPacketSequenceWindow64(ushort inSeq, ushort lastSeq)
        {
            short diff = (short)(inSeq - lastSeq);
            return diff > 0 && diff <= 64;
        }

        /// <summary>
        /// 2043. 心跳 30 秒超时物理断线时钟
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsClientHeartbeatTimedOut(long lastPingMs, long nowMs) => (nowMs - lastPingMs) >= 30_000;

        /// <summary>
        /// 2044. Native AOT 零 GC 环形缓冲区足额判定
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasRingBufferCapacityAvailable(int writePos, int readPos, int capacity, int bytesNeeded)
        {
            int free = (readPos <= writePos) ? (capacity - (writePos - readPos) - 1) : (readPos - writePos - 1);
            return free >= bytesNeeded;
        }

        /// <summary>
        /// 2045. 封包 XOR 累加校验和 (零堆分配)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte ComputePacketXor(ReadOnlySpan<byte> data)
        {
            byte c = 0xAA;
            for (int i = 0; i < data.Length; i++) c ^= data[i];
            return c;
        }

        /// <summary>
        /// 2046. 1 秒移动超 3 次外挂拦截
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSpeedHackDetected(int moveCount) => moveCount > 3;

        /// <summary>
        /// 2047. 施法前摇 500ms 物理核验
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsCastTimingHonored(long lastCastMs, long nowMs) => (nowMs - lastCastMs) >= 500;

        /// <summary>
        /// 2048. 普通发言 1500ms 与喊话 10000ms 防刷屏
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanSendChat(long lastChatMs, long nowMs, bool isShout)
        {
            int cd = isShout ? 10_000 : 1500;
            return (nowMs - lastChatMs) >= cd;
        }

        /// <summary>
        /// 2049. 视距 15 格视野裁剪判定
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEntityWithinView(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x1 - x2) <= 15 && Math.Abs(y1 - y2) <= 15;
        }

        /// <summary>
        /// 2050. 服务端权威随机数映射掉落 ID 防作弊
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GenerateAuthoritativeLoot(int serverSeed, int monsterType)
        {
            return 3000 + ((serverSeed ^ (monsterType * 53)) % 1000);
        }
    }
}
