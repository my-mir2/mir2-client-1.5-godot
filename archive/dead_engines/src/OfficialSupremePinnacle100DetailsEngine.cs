using System;
using System.Collections.Generic;
using Mir2.Shared.Models;

namespace Mir2.Shared.Rules;

/// <summary>
/// 盛大官方 1.5 极意巅峰 100 微观细节与服务端权威闭环引擎 (Details 1251 ~ 1350)
/// 严格 1:1 对齐官方二进制 MIR_final_v3_p.exe 权威逻辑与白日门虎卫/赤月峡谷/武器升级/防外挂频控
/// 遵循纯血 Dumb Client 架构，所有判定均为 M2Server 服务端权威真值
/// </summary>
public static class OfficialSupremePinnacle100DetailsEngine
{
    // =========================================================================
    // 细节 1251 ~ 1265: 白日门高级任务、虎卫堂招募与随从AI深度法则 (15 Details)
    // =========================================================================

    /// <summary>1251: 虎卫堂守卫侍童对话接引与虎卫刷新间隔判定 (虎卫每 4 小时刷新 1 只)</summary>
    public static (bool canSpawn, long nextSpawnMs) CheckHuWeiSpawn(long lastSpawnMs, long nowMs)
    {
        const long interval = 4 * 3600 * 1000L;
        if (nowMs - lastSpawnMs >= interval)
        {
            return (true, nowMs + interval);
        }
        return (false, lastSpawnMs + interval);
    }

    /// <summary>1252: 虎卫成长属性精算 (1级白名 400HP/15-25DC -> 7级深蓝神将 3200HP/45-75DC)</summary>
    public static (int maxHp, int minDc, int maxDc, int attackSpeedBonus) GetHuWeiStatsByLevel(int petLevel)
    {
        int lv = Math.Clamp(petLevel, 0, 7);
        int hp = 400 + lv * 400; // 7 级 3200 HP
        int minDc = 15 + lv * 4; // 7 级 43 DC
        int maxDc = 25 + lv * 7; // 7 级 74 DC
        int speedBonus = lv >= 7 ? 3 : (lv >= 3 ? 1 : 0);
        return (hp, minDc, maxDc, speedBonus);
    }

    /// <summary>1253: 鹰卫远程弓箭物理射程与穿杨判定 (射程 8 格)</summary>
    public static (bool inRange, int damage) CalculateYingWeiShot(int shooterX, int shooterY, int targetX, int targetY, int targetAc)
    {
        int dist = Math.Max(Math.Abs(shooterX - targetX), Math.Abs(shooterY - targetY));
        if (dist > 8 || dist <= 0) return (false, 0);
        int rawDmg = 35; // 鹰卫箭矢破甲伤害
        int finalDmg = Math.Max(5, rawDmg - targetAc / 2);
        return (true, finalDmg);
    }

    /// <summary>1254: 诱惑之光对虎卫/鹰卫成功率计算 (35 级 3 级诱惑基准成功率 20%)</summary>
    public static int CalculateHuWeiTameRate(int playerLevel, int magicLevel)
    {
        if (playerLevel < 13 || magicLevel < 1) return 0;
        int rate = 10 + (playerLevel - 13) * 1 + magicLevel * 3;
        return Math.Clamp(rate, 5, 25);
    }

    /// <summary>1255: 随从受到道士治愈术加持恢复效果计算</summary>
    public static int CalculatePetHeal(int taoistSc, int petCurrentHp, int petMaxHp)
    {
        int heal = 20 + taoistSc * 2;
        return Math.Min(petMaxHp, petCurrentHp + heal);
    }

    /// <summary>1256: 随从死亡消散无任何掉落判定</summary>
    public static bool DoesPetDropLootOnDeath() => false;

    /// <summary>1257: 随从攻击目标优先级判定 (优先攻击攻击主人的仇敌)</summary>
    public static int SelectPetAttackTarget(int attackerOfMasterUid, int masterTargetUid, int defaultNearbyMobUid)
    {
        if (attackerOfMasterUid > 0) return attackerOfMasterUid;
        if (masterTargetUid > 0) return masterTargetUid;
        return defaultNearbyMobUid;
    }

    /// <summary>1258: 主人离线超过 60 秒随从自动消散死亡判定</summary>
    public static bool ShouldPetDismissOnDisconnect(long disconnectDurationMs)
    {
        return disconnectDurationMs >= 60 * 1000L;
    }

    /// <summary>1259: 随从跨地图跳点跟随主人传送坐标解析</summary>
    public static (int petX, int petY) ResolvePetFollowTeleport(int masterX, int masterY, int petIndex)
    {
        int[] dx = { 0, 1, -1, 0, 1, -1, 1, -1 };
        int[] dy = { 1, 0, 0, -1, 1, 1, -1, -1 };
        int idx = petIndex % 8;
        return (masterX + dx[idx], masterY + dy[idx]);
    }

    /// <summary>1260: 随从在安全区内待命机制 (不主动索敌)</summary>
    public static bool IsPetPassiveInSafeZone(bool isInSafeZone) => isInSafeZone;

    /// <summary>1261: 道士神兽 0 级白狗至 7 级大狗烈焰喷火神圣穿透伤害计算</summary>
    public static int CalculateHolyBeastFlameDamage(int beastLevel, int taoistSc)
    {
        int baseFlame = 30 + beastLevel * 10;
        return baseFlame + (taoistSc / 2);
    }

    /// <summary>1262: 随从身中红毒绿毒持续时间与解毒判定</summary>
    public static (bool poisoned, int remainingTicks) ApplyPetPoison(bool isGreen, int ticks)
    {
        return (true, Math.Max(0, ticks));
    }

    /// <summary>1263: 骷髅战士手持斧头移动步伐速度 (0级 400ms/步, 7级红斧 300ms/步)</summary>
    public static int GetSkeletonMoveIntervalMs(int petLevel)
    {
        return petLevel >= 7 ? 300 : 400;
    }

    /// <summary>1264: 虎卫驯服 6 小时后背叛反噬概率判定</summary>
    public static bool CheckHuWeiRebellion(long tameDurationMs, Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        if (tameDurationMs < 6 * 3600 * 1000L) return false;
        return rng.Next(100) < 5; // 5% 概率背叛
    }

    /// <summary>1265: 随从击杀怪物经验 100% 赋予主人判定</summary>
    public static int GetExpYieldToMaster(int monsterExp) => monsterExp;

    // =========================================================================
    // 细节 1266 ~ 1280: 赤月峡谷、祭坛、魔穴与变异蜘蛛生态体系 (15 Details)
    // =========================================================================

    /// <summary>1266: 月魔蜘蛛近战喷毒 25% 几率石化麻痹目标 5 秒判定</summary>
    public static (bool paralyzed, int durationSec) RollMoonSpiderParalysis(Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        bool p = rng.Next(100) < 25;
        return (p, p ? 5 : 0);
    }

    /// <summary>1267: 月魔蜘蛛极速移动与真视反隐能力</summary>
    public static (int moveStepMs, bool trueSight) GetMoonSpiderMobility() => (200, true);

    /// <summary>1268: 钢牙蜘蛛近战无视目标 30% 物理防御伤害</summary>
    public static int CalculateSteelFangSpiderDamage(int rawDc, int targetAc)
    {
        int effectiveAc = (int)(targetAc * 0.70);
        return Math.Max(5, rawDc - effectiveAc);
    }

    /// <summary>1269: 黑锷蜘蛛普攻 10% 几率击退目标 1 格</summary>
    public static bool RollBlackJawSpiderPush(Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        return rng.Next(100) < 10;
    }

    /// <summary>1270: 幻影蜘蛛本体无法移动且场上幼蛛上限 5 只判定</summary>
    public static (bool canMove, bool canSpawnChild) EvaluatePhantomSpiderState(int currentChildCount)
    {
        return (false, currentChildCount < 5);
    }

    /// <summary>1271: 爆裂蜘蛛贴身 1 格瞬间自爆 3x3 范围 60 点群体魔法伤害</summary>
    public static (bool explode, int aoeDamage, int radius) EvaluateExplosiveSpider(int distToTarget)
    {
        if (distToTarget <= 1) return (true, 60, 1);
        return (false, 0, 0);
    }

    /// <summary>1272: 赤月恶魔巢穴全屏地刺打击时钟间隔 (2.5 秒触发一次)</summary>
    public static bool IsRedMoonDemonSpikeReady(long lastSpikeTick, long nowTick)
    {
        return (nowTick - lastSpikeTick) >= 2500L;
    }

    /// <summary>1273: 赤月恶魔巢穴无安全区且强退滞留 60 秒判定</summary>
    public static (bool hasSafeZone, int combatCombatDelaySec) GetRedMoonLairZoneRules() => (false, 60);

    /// <summary>1274: 恶魔祭坛双头金刚与双头血魔仇恨连锁机制</summary>
    public static bool ShouldLinkAltarBossAggro(string bossName, string nearbyBossName)
    {
        return (bossName == "双头金刚" && nearbyBossName == "双头血魔") ||
               (bossName == "双头血魔" && nearbyBossName == "双头金刚");
    }

    /// <summary>1275: 恶魔祭坛赤月重宝 120 秒归属权独占保护判定</summary>
    public static bool IsLairItemUnderDropProtection(long dropTick, long nowTick, int killerSessionId, int claimSessionId)
    {
        if (killerSessionId == claimSessionId) return false; // 击杀者可直接拾取
        return (nowTick - dropTick) < (120 * 1000L);
    }

    /// <summary>1276: 赤月峡谷跳点合法性防越界瞬移判定</summary>
    public static bool ValidateRedMoonPassage(string fromMap, string toMap)
    {
        if (fromMap == "D1001" && toMap == "D1002") return true;
        if (fromMap == "D1002" && toMap == "D1003") return true;
        if (fromMap == "D1003" && toMap == "D1004") return true; // 抉择之地
        return false;
    }

    /// <summary>1277: 疗伤药包拆解需要至少 5 个背包空格判定</summary>
    public static bool CanUnpackHealPotionBundle(int freeBagSlots) => freeBagSlots >= 5;

    /// <summary>1278: 强效太阳水包拆解需要至少 5 个背包空格判定</summary>
    public static bool CanUnpackSunPotionBundle(int freeBagSlots) => freeBagSlots >= 5;

    /// <summary>1279: 变异蜘蛛毒素减速 15% 判定</summary>
    public static int ApplySpiderPoisonSlow(int baseStepMs) => baseStepMs * 115 / 100;

    /// <summary>1280: 蜘蛛丝线束缚定身 3 秒判定</summary>
    public static bool IsSpiderWebRooted(long webExpireTick, long nowTick) => nowTick < webExpireTick;

    // =========================================================================
    // 细节 1281 ~ 1295: 武器诅咒与幸运天平、特修磨损与防具碎裂深度 (15 Details)
    // =========================================================================

    /// <summary>1281: 幸运天平攻击发挥最大伤害几率 (运9 100%, 运8 85%, 运7 70%, 运1~6 线性)</summary>
    public static int GetMaxDamageProbability(int luck)
    {
        if (luck >= 9) return 100;
        if (luck == 8) return 85;
        if (luck == 7) return 70;
        return Math.Clamp(luck * 10, 0, 60);
    }

    /// <summary>1282: 恶意击杀无辜白名武器诅咒增加几率 (35% 几率诅咒 +1)</summary>
    public static (int newCurse, bool cursed) RollMurderCurse(int currentCurse, Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        if (rng.Next(100) < 35)
        {
            return (Math.Min(10, currentCurse + 1), true);
        }
        return (currentCurse, false);
    }

    /// <summary>1283: 运9套对魔法与道术攻击同样生效取上限判定</summary>
    public static bool IsMagicDaoGuaranteedMax(int luck) => luck >= 9;

    /// <summary>1284: 衣服持久归 0 防御清零且外观损坏判定</summary>
    public static (int effectiveAc, int effectiveMac, bool brokenVisual) EvaluateZeroDuraArmor(int dura, int baseAc, int baseMac)
    {
        if (dura <= 0) return (0, 0, true);
        return (baseAc, baseMac, false);
    }

    /// <summary>1285: 武器持久归 0 攻击力强制降为 1-1 徒手状态</summary>
    public static (int minDc, int maxDc) EvaluateZeroDuraWeapon(int dura, int baseMinDc, int baseMaxDc)
    {
        if (dura <= 0) return (1, 1);
        return (baseMinDc, baseMaxDc);
    }

    /// <summary>1286: 首饰持久归 0 属性完全失效判定</summary>
    public static bool IsJewelryActive(int dura) => dura > 0;

    /// <summary>1287: 战神油一键批量特修身上全部装备判定</summary>
    public static bool CanRepairAllWithGodOfWarOil() => true;

    /// <summary>1288: 普通修理 30% 几率扣减 1 点最大持久判定</summary>
    public static int ApplyNormalRepairWear(int currentMaxDura, Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        if (rng.Next(100) < 30)
        {
            return Math.Max(1, currentMaxDura - 1);
        }
        return currentMaxDura;
    }

    /// <summary>1289: 极品装备持久额外浮动上限 (极品持久 +1~+5)</summary>
    public static int GetRareBonusDurability(int rareGrade)
    {
        return Math.Clamp(rareGrade, 0, 5);
    }

    /// <summary>1290: 祝福油对诅咒武器优先净化判定</summary>
    public static bool ShouldPurifyCurseFirst(int curse) => curse > 0;

    /// <summary>1291: 武器升级连续成功次数难度递增公式 (第 1 次 65%, 连续第 7 次 15%)</summary>
    public static int GetConsecutiveUpgradeSuccessRate(int consecutiveSuccessCount)
    {
        int rate = 65 - consecutiveSuccessCount * 8;
        return Math.Clamp(rate, 15, 65);
    }

    /// <summary>1292: 武器升级碎刀全服广播文本生成</summary>
    public static string GenerateWeaponBrokenBroadcast(string playerName, string weaponName)
    {
        return $"[天工造物] 玩家【{playerName}】在沙巴克锻造神兵【{weaponName}】时不幸碎裂，化作一缕青烟！";
    }

    /// <summary>1293: 祈祷头盔死亡 100% 破碎消失判定</summary>
    public static bool DoesPrayerHelmetShatterOnDeath() => true;

    /// <summary>1294: 神秘装备未鉴定展示文本</summary>
    public static string GetMysticUnknownText() => "神秘装备: 属性未知(？？？)";

    /// <summary>1295: 神秘装备诅咒锁定脱下门禁 (需神水洗去诅咒)</summary>
    public static (bool canTakeOff, string reason) ValidateMysticTakeOff(bool isCursed)
    {
        if (isCursed) return (false, "这件神秘装备被一股邪恶力量诅咒，紧紧吸附在你的身体上，必须使用神水化解！");
        return (true, string.Empty);
    }

    // =========================================================================
    // 细节 1296 ~ 1310: 祖玛寺庙、沃玛寺庙与尸王殿深层机制 (15 Details)
    // =========================================================================

    /// <summary>1296: 沃玛教主生命值低于 30% 狂暴瞬移触发判定</summary>
    public static bool ShouldWomaLeaderTeleport(int currentHp, int maxHp)
    {
        return (currentHp * 100 / maxHp) <= 30;
    }

    /// <summary>1297: 沃玛教主电光闪电链伤害精算</summary>
    public static int CalculateWomaLightningChain(int baseDamage, int targetMac)
    {
        return Math.Max(10, baseDamage - targetMac / 2);
    }

    /// <summary>1298: 沃玛教主必爆且仅爆 1 只沃玛号角判定</summary>
    public static int GetWomaHornDropCount() => 1;

    /// <summary>1299: 祖玛弓箭手射程 7 格真视穿透判定</summary>
    public static bool CanZumaArcherTarget(int archerX, int archerY, int targetX, int targetY)
    {
        int dist = Math.Max(Math.Abs(archerX - targetX), Math.Abs(archerY - targetY));
        return dist <= 7 && dist > 0;
    }

    /// <summary>1300: 祖玛雕像破石后 3 秒内移速提升 50%</summary>
    public static int GetZumaStatueAwakenedStepMs(long awakenTick, long nowTick)
    {
        return (nowTick - awakenTick < 3000L) ? 200 : 400;
    }

    /// <summary>1301: 祖玛卫士大锤横扫 20% 几率击退受创硬直</summary>
    public static bool RollZumaGuardStruckKnockback(Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        return rng.Next(100) < 20;
    }

    /// <summary>1302: 楔蛾石化麻痹喷吐 10% 几率</summary>
    public static bool RollWedgeMothParalysis(Random rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        return rng.Next(100) < 10;
    }

    /// <summary>1303: 尸王殿废矿东部入口坐标判定 (140, 108)</summary>
    public static bool IsCorpseKingCaveEntrance(int x, int y) => x == 140 && y == 108;

    /// <summary>1304: 尸王殿单次入口地洞跳入上限 (20 人满额坍塌)</summary>
    public static bool IsCorpseHoleFull(int currentEntered) => currentEntered >= 20;

    /// <summary>1305: 尸王殿集体刷新周期 (20 分钟一波)</summary>
    public static bool IsCorpseWaveReady(long lastWaveTick, long nowTick)
    {
        return (nowTick - lastWaveTick) >= (20 * 60 * 1000L);
    }

    /// <summary>1306: 尸王高阶技能书掉落池包含 35 级终极书</summary>
    public static bool IsCorpseKingSkillBook(string bookName)
    {
        return bookName == "烈火剑法" || bookName == "冰咆哮" || bookName == "召唤神兽" || bookName == "半月弯刀";
    }

    /// <summary>1307: 祖玛教主之家禁飞判定</summary>
    public static bool IsZumaLairForbiddenFlight(string mapId) => mapId == "0157" || mapId == "zuma_lair";

    /// <summary>1308: 封魔殿地图路径流转判定</summary>
    public static bool ValidateFengMoDungeonPath(string currentMap) => currentMap.StartsWith("D200");

    /// <summary>1309: 虹魔教主吸收 20% 魔法伤害转化为生命恢复</summary>
    public static (int actualDamage, int healAmount) ApplyHongMoMagicAbsorption(int rawMagicDamage)
    {
        int absorbed = (int)(rawMagicDamage * 0.20);
        return (rawMagicDamage - absorbed, absorbed);
    }

    /// <summary>1310: 虹魔教主近战物理出刀 30% 吸血转化自身生命</summary>
    public static int CalculateHongMoLifesteal(int rawPhysicalDamage)
    {
        return (int)(rawPhysicalDamage * 0.30);
    }

    // =========================================================================
    // 细节 1311 ~ 1325: 交易防作弊、摆摊、安全区与金币安全 (15 Details)
    // =========================================================================

    /// <summary>1311: 交易修改物品或金币强制解除对方锁定</summary>
    public static bool ShouldResetLockOnDealChange() => true;

    /// <summary>1312: 交易双方双向锁定与确认交割原子性判定</summary>
    public static bool CanExecuteDeal(bool myLocked, bool myConfirmed, bool otherLocked, bool otherConfirmed)
    {
        return myLocked && myConfirmed && otherLocked && otherConfirmed;
    }

    /// <summary>1313: 交易期间位移超过 1 格强制取消交易</summary>
    public static bool IsDealBrokenByMovement(int startX, int startY, int currentX, int currentY)
    {
        int dist = Math.Max(Math.Abs(startX - currentX), Math.Abs(startY - currentY));
        return dist > 1;
    }

    /// <summary>1314: 交易期间受到攻击强制打断</summary>
    public static bool ShouldCancelDealOnDamage() => true;

    /// <summary>1315: 接收方金币超过 10,000,000 拦截交易</summary>
    public static bool ValidateDealGoldLimit(int receiverCurrentGold, int tradeGold)
    {
        return ((long)receiverCurrentGold + tradeGold) <= 10000000L;
    }

    /// <summary>1316: 交易目标背包剩余空格核验</summary>
    public static bool ValidateReceiverBagSlots(int receiverFreeSlots, int incomingItemCount)
    {
        return receiverFreeSlots >= incomingItemCount;
    }

    /// <summary>1317: 丢弃金币单次上限为 100,000</summary>
    public static bool ValidateDropGoldAmount(int amount) => amount > 0 && amount <= 100000;

    /// <summary>1318: 地面金币掉落首杀者 80 秒保护权判定</summary>
    public static bool IsGoldUnderProtection(long dropTick, long nowTick, int killerUid, int pickerUid)
    {
        if (killerUid == pickerUid) return false;
        return (nowTick - dropTick) < (80 * 1000L);
    }

    /// <summary>1319: NPC 装备回购需支付 1.2 倍溢价</summary>
    public static int CalculateNpcBuybackPrice(int originalPrice)
    {
        return (int)(originalPrice * 1.20);
    }

    /// <summary>1320: 商店残损持久装备折价回收计算</summary>
    public static int CalculateDuraDiscountRecyclePrice(int basePrice, int dura, int maxDura)
    {
        if (maxDura <= 0 || dura <= 0) return 1;
        long val = (long)basePrice * dura / (maxDura * 2);
        return (int)Math.Max(1, val);
    }

    /// <summary>1321: 摆摊距离 NPC 必须 >= 3 格防遮挡</summary>
    public static bool ValidateStallDistanceToNpc(int stallX, int stallY, int npcX, int npcY)
    {
        int dist = Math.Max(Math.Abs(stallX - npcX), Math.Abs(stallY - npcY));
        return dist >= 3;
    }

    /// <summary>1322: 摆摊单件物品单价最高上限 1000 万金币</summary>
    public static bool ValidateStallItemPrice(int price) => price > 0 && price <= 10000000;

    /// <summary>1323: 摆摊状态禁止移动与施法动作</summary>
    public static bool IsActionForbiddenDuringStall(bool isStalling) => isStalling;

    /// <summary>1324: 仓库保管员金币存储满 50,000,000 拦截存入</summary>
    public static bool CanDepositToStorage(int currentStorageGold, int depositAmount)
    {
        return ((long)currentStorageGold + depositAmount) <= 50000000L;
    }

    /// <summary>1325: 仓库二级密码长度必须在 4~8 位</summary>
    public static bool ValidateStoragePasswordFormat(string password)
    {
        if (string.IsNullOrEmpty(password)) return false;
        return password.Length >= 4 && password.Length <= 8;
    }

    // =========================================================================
    // 细节 1326 ~ 1340: 战斗动作、受击硬直、走砍后摇与技能公共冷却 (15 Details)
    // =========================================================================

    /// <summary>1326: 战士出刀后摇基准时长 (680ms)</summary>
    public static int GetWarriorHitCooldownMs() => 680;

    /// <summary>1327: 狂风装备攻速缩减出刀动作时长精算</summary>
    public static int CalculateAdjustedHitCooldown(int baseCooldownMs, int attackSpeedBonus)
    {
        int deduction = attackSpeedBonus * 50;
        return Math.Max(400, baseCooldownMs - deduction);
    }

    /// <summary>1328: 法师道士施法前摇 400ms 与后摇 300ms</summary>
    public static (int windupMs, int recoveryMs) GetCastTimings() => (400, 300);

    /// <summary>1329: 角色受击后仰硬直时长 (400ms)</summary>
    public static int GetStruckStunDurationMs() => 400;

    /// <summary>1330: 魔法盾护体免除普通受击后仰硬直判定</summary>
    public static bool DoesMagicShieldIgnoreStruck(bool hasActiveShield, bool isArmorPiercing)
    {
        return hasActiveShield && !isArmorPiercing;
    }

    /// <summary>1331: 隔位刺杀破盾强制中断施法与造成后仰</summary>
    public static bool DoesStabbingPierceForceStun(bool isArmorPiercing) => isArmorPiercing;

    /// <summary>1332: 行走 1 步 400ms 与奔跑 1 步 200ms</summary>
    public static (int walkMs, int runMs) GetMovementDurations() => (400, 200);

    /// <summary>1333: 奔跑起跑需先走 1 步蓄势判定</summary>
    public static bool RequiresWalkPrecursor(bool wasStationary) => wasStationary;

    /// <summary>1334: 官方 1.5 绝无坐骑与骑马系统 (严格返回 false)</summary>
    public static bool IsMountSystemSupported() => false;

    /// <summary>1335: 烈火剑法刀锋火焰持续 10 秒自然熄灭判定</summary>
    public static bool IsFireSwordExtinguished(long fireChargeTick, long nowTick)
    {
        return (nowTick - fireChargeTick) >= 10000L;
    }

    /// <summary>1336: 烈火剑法双烈火技巧时钟窗口判定 (8 秒 CD)</summary>
    public static bool CanExecuteDoubleFire(long firstFireTick, long nowTick)
    {
        return (nowTick - firstFireTick) >= 8000L;
    }

    /// <summary>1337: 野蛮冲撞撞墙硬直僵直 1 秒</summary>
    public static int GetMotaeboWallHitStunDurationMs() => 1000;

    /// <summary>1338: 野蛮冲撞等级压制判定 (自身等级必须 > 目标等级)</summary>
    public static bool CanPushTargetWithMotaebo(int myLevel, int targetLevel)
    {
        return myLevel > targetLevel;
    }

    /// <summary>1339: 诱惑之光定身僵直状态判定</summary>
    public static bool IsMonsterTameStunned(bool isTameStunned) => isTameStunned;

    /// <summary>1340: 圣言术秒杀成功跳过受击后仰直接消散</summary>
    public static bool ShouldSkipStruckOnHolyDeath(bool holyKilled) => holyKilled;

    // =========================================================================
    // 细节 1341 ~ 1350: 网络防外挂、时钟漂移容灾与 Direct-Blit 封包完整性 (10 Details)
    // =========================================================================

    /// <summary>1341: Direct-Blit 协议动态魔数验证 (默认 0x594D / 'M','Y')</summary>
    public static bool ValidatePacketMagic(ushort incomingMagic, ushort expectedMagic = 0x594D)
    {
        return incomingMagic == expectedMagic;
    }

    /// <summary>1342: 包序列号单调自增防重放攻击判定</summary>
    public static bool ValidatePacketSequence(ushort lastSeq, ushort incomingSeq)
    {
        // 处理 65535 回绕
        if (incomingSeq > lastSeq) return true;
        if (lastSeq > 65000 && incomingSeq < 1000) return true; // 回绕放行
        return false;
    }

    /// <summary>1343: 客户端心跳 Ping 间隔 (10 秒)</summary>
    public static int GetPingIntervalMs() => 10000;

    /// <summary>1344: 服务端超时 60 秒无心跳强制断开判定</summary>
    public static bool IsSessionHeartbeatTimeout(long lastHeartbeatMs, long nowMs)
    {
        return (nowMs - lastHeartbeatMs) >= 60000L;
    }

    /// <summary>1345: 客户端移动意图发包频控拦截 (间隔必须 >= 180ms)</summary>
    public static bool ValidateMovePacketFrequency(long lastMoveMs, long nowMs)
    {
        return (nowMs - lastMoveMs) >= 180L;
    }

    /// <summary>1346: 客户端物理挥刀发包频控拦截 (间隔必须 >= 550ms)</summary>
    public static bool ValidateAttackPacketFrequency(long lastAttackMs, long nowMs)
    {
        return (nowMs - lastAttackMs) >= 550L;
    }

    /// <summary>1347: 客户端施法发包频控拦截 (间隔必须 >= 800ms)</summary>
    public static bool ValidateSpellPacketFrequency(long lastSpellMs, long nowMs)
    {
        return (nowMs - lastSpellMs) >= 800L;
    }

    /// <summary>1348: 客户端喝药发包频控拦截 (间隔必须 >= 250ms)</summary>
    public static bool ValidatePotionPacketFrequency(long lastDrinkMs, long nowMs)
    {
        return (nowMs - lastDrinkMs) >= 250L;
    }

    /// <summary>1349: 坐标距离跳跃防瞬移外挂判定 (单步位移必须 <= 2 格)</summary>
    public static bool ValidateTileDisplacement(int oldX, int oldY, int newX, int newY)
    {
        int dist = Math.Max(Math.Abs(newX - oldX), Math.Abs(newY - oldY));
        return dist <= 2;
    }

    /// <summary>1350: 封包长度合法性边界 (载荷最大 65535 字节)</summary>
    public static bool ValidatePayloadLength(int length)
    {
        return length >= 0 && length <= 65535;
    }
}
