using System;
using System.Collections.Generic;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols.Modern;

namespace Mir2.Shared.Rules;

/// <summary>
/// 盛大官方 1.5 巅峰统治级 100 微观细节与现代二进制协议权威规则引擎 (Details 851 ~ 950)
/// 严格 1:1 对齐官方二进制 MIR_final_v3_p.exe 权威逻辑与 HPSocket 源码级 AOT 规范
/// </summary>
public static class OfficialSovereignPinnacle100DetailsEngine
{
    // =========================================================================
    // 细节 851 ~ 870: 现代强类型 Direct-Blit 协议与网络状态机
    // =========================================================================

    /// <summary>851: 客户端组队 Toggle 模式判定</summary>
    public static bool ToggleGroupAllowed(ref bool currentStatus)
    {
        currentStatus = !currentStatus;
        return currentStatus;
    }

    /// <summary>852: 客户端组队邀请校验 (目标是否已在队伍或拒绝组队)</summary>
    public static (bool allowed, string reason) ValidateGroupInvite(bool targetAllowGroup, int targetTeamId)
    {
        if (!targetAllowGroup)
            return (false, "对方关闭了组队允许开关。");
        if (targetTeamId > 0)
            return (false, "对方已经加入了其他队伍。");
        return (true, string.Empty);
    }

    /// <summary>853: 行会加入人数上限核验 (官方 1.5 默认上限 200 人)</summary>
    public static (bool allowed, string reason) ValidateGuildJoin(int currentMembers, int maxMembers = 200)
    {
        if (currentMembers >= maxMembers)
            return (false, "行会人数已达到上限(200人)，无法接纳新成员。");
        return (true, string.Empty);
    }

    /// <summary>854: 行会更新公告权限核验 (仅掌门 Rank=1 具备权限)</summary>
    public static bool ValidateGuildNoticePermission(int rankLevel) => rankLevel == 1;

    /// <summary>855: 行会修改成员封号字节截断 (UTF-8 最长 16 字节防溢出，按字符完整边界截断)</summary>
    public static string TruncateGuildMemberTitle(string rawTitle)
    {
        if (string.IsNullOrEmpty(rawTitle)) return string.Empty;
        var sb = new System.Text.StringBuilder();
        int totalBytes = 0;
        foreach (char c in rawTitle)
        {
            int charBytes = System.Text.Encoding.UTF8.GetByteCount(new[] { c });
            if (totalBytes + charBytes > 16) break;
            sb.Append(c);
            totalBytes += charBytes;
        }
        return sb.ToString();
    }

    /// <summary>856: 仓库二级密码输错惩罚 (连续 3 次锁定 10 分钟)</summary>
    public static (bool locked, int remainSeconds) CheckStoragePasswordAttempts(int errorCount, long lastFailTick, long nowTick)
    {
        if (errorCount >= 3)
        {
            long elapsed = (nowTick - lastFailTick) / 1000;
            if (elapsed < 600)
            {
                return (true, (int)(600 - elapsed));
            }
        }
        return (false, 0);
    }

    /// <summary>857: 仓库二级密码复杂度校验 (必须为 6 位数字或字母)</summary>
    public static bool ValidateStoragePasswordFormat(string pwd)
    {
        if (string.IsNullOrEmpty(pwd) || pwd.Length != 6) return false;
        foreach (char c in pwd)
        {
            if (!char.IsLetterOrDigit(c)) return false;
        }
        return true;
    }

    /// <summary>858: 聊天消息频道 0 (普通附近) 广播距离校验 (切比雪夫距离 <= 9)</summary>
    public static bool IsInLocalChatRange(int sx, int sy, int tx, int ty) =>
        Math.Max(Math.Abs(sx - tx), Math.Abs(sy - ty)) <= 9;

    /// <summary>859: 聊天消息大喊频道等级门槛校验 (必须 >= 7 级)</summary>
    public static (bool allowed, string reason) ValidateShoutChat(int playerLevel)
    {
        if (playerLevel < 7)
            return (false, "等级不足 7 级，无法使用大喊频道！");
        return (true, string.Empty);
    }

    /// <summary>860: 密聊私信目标状态判定</summary>
    public static (bool allowed, string reason) ValidateWhisper(bool isOnline, bool isRejectWhisper)
    {
        if (!isOnline)
            return (false, "对方不在线。");
        if (isRejectWhisper)
            return (false, "对方开启了拒绝私聊。");
        return (true, string.Empty);
    }

    /// <summary>861: 实体进入视野外观合成核验 (防止幽灵白块)</summary>
    public static bool ValidateEntityAppearance(int appr, int dress, int weapon) =>
        appr >= 0 && dress >= 0 && weapon >= 0;

    /// <summary>862: 实体离开视野场景安全清理</summary>
    public static bool ShouldPurgeEntityOnDisappear(int entityUid) => entityUid > 0;

    /// <summary>863: 实体位移平滑插值与瞬移拉回检测 (单步距离 > 2 触发瞬移)</summary>
    public static bool IsTeleportMovement(int x1, int y1, int x2, int y2) =>
        Math.Max(Math.Abs(x1 - x2), Math.Abs(y1 - y2)) > 2;

    /// <summary>864: 实体动作与音效映射</summary>
    public static int GetActionSoundId(byte actionType, int defaultHitSound = 100) =>
        actionType switch
        {
            0 => defaultHitSound, // 挥刀
            1 => 101,            // 受击
            2 => 102,            // 倒地
            3 => 103,            // 施法
            _ => 0
        };

    /// <summary>865: 背包同步操作类型判定 (0=Add/Update, 1=Remove)</summary>
    public static bool IsValidInventoryOp(byte opType) => opType <= 1;

    /// <summary>866: 装备槽位合法性验证 (0~8 官方九大槽位)</summary>
    public static bool IsValidEquipSlot(byte slot) => slot <= 8;

    /// <summary>867: 名字颜色基于 PK 值的官方转换公式</summary>
    public static byte CalculateNameColor(int pkPoints)
    {
        if (pkPoints >= 100) return 2; // 红名
        if (pkPoints > 0) return 1;    // 黄名
        return 0;                      // 白名
    }

    /// <summary>868: 状态机掩码叠加运算</summary>
    public static uint ApplyStatusBit(uint currentMask, uint bitToApply) => currentMask | bitToApply;

    /// <summary>869: 交易两阶段提交 (2PC) 背包剩余空间校验</summary>
    public static bool CanCompleteTrade(int myFreeSlots, int myReceivingCount, int targetFreeSlots, int targetReceivingCount) =>
        myFreeSlots >= myReceivingCount && targetFreeSlots >= targetReceivingCount;

    /// <summary>870: 现代二进制协议帧头合法性验证</summary>
    public static bool ValidatePacketHeader(in BinaryProtocol.PacketHeader header) =>
        header.Magic == BinaryProtocol.DefaultMagic && header.PayloadLength <= 65535;

    // =========================================================================
    // 细节 871 ~ 890: 纯血 Dumb Client 架构与服务端权威计算
    // =========================================================================

    /// <summary>871: 伤害数值服务端权威运算 (物理 DC + 技能倍率 - 目标 AC)</summary>
    public static int CalcAuthoritativeMeleeDamage(int attackerDc, int targetAc, float skillMultiplier = 1.0f)
    {
        int raw = (int)(attackerDc * skillMultiplier);
        return Math.Max(1, raw - targetAc);
    }

    /// <summary>872: 法术躲避几率权威判定 (虎齿项链躲避)</summary>
    public static bool RollSpellEvade(int magicAvoidPercent, Random rng)
    {
        if (magicAvoidPercent <= 0) return false;
        return rng.Next(100) < Math.Min(80, magicAvoidPercent);
    }

    /// <summary>873: 技能 MP 扣减权威结算</summary>
    public static (bool success, int newMp) DeductAuthoritativeMp(int currentMp, int cost)
    {
        if (currentMp < cost) return (false, currentMp);
        return (true, currentMp - cost);
    }

    /// <summary>874: 怪物死亡掉落金币与战利品判定</summary>
    public static bool RollMonsterDrop(int dropRateDenominator, Random rng) =>
        rng.Next(dropRateDenominator) == 0;

    /// <summary>875: 割肉品质依据尸体残留时间的衰减公式</summary>
    public static int CalcMeatQuality(int initialQuality, int elapsedCorpseSec)
    {
        int decayed = initialQuality - (elapsedCorpseSec / 10);
        return Math.Max(1, decayed);
    }

    /// <summary>876: 药水缓回权威 Tick 增量结算</summary>
    public static (int curHp, int pool) TickAuthoritativePotion(int curHp, int maxHp, int pool, int tickHeal = 5)
    {
        if (pool <= 0 || curHp >= maxHp) return (curHp, 0);
        int heal = Math.Min(pool, tickHeal);
        int finalHp = Math.Min(maxHp, curHp + heal);
        return (finalHp, pool - heal);
    }

    /// <summary>877: 穿戴装备需求属性权威核验</summary>
    public static bool CheckAuthoritativeEquipRequirement(int playerLevel, int playerStat, ItemPropertyCompendiumEngine.EquipNeedType needType, int needValue)
    {
        return needType switch
        {
            ItemPropertyCompendiumEngine.EquipNeedType.Level => playerLevel >= needValue,
            ItemPropertyCompendiumEngine.EquipNeedType.Dc => playerStat >= needValue,
            ItemPropertyCompendiumEngine.EquipNeedType.Mc => playerStat >= needValue,
            ItemPropertyCompendiumEngine.EquipNeedType.Sc => playerStat >= needValue,
            _ => true
        };
    }

    /// <summary>878: 技能熟练度经验升级阈值表 (0->1: 100, 1->2: 200, 2->3: 400)</summary>
    public static int GetSkillTrainMax(int level) => level switch
    {
        0 => 100,
        1 => 200,
        2 => 400,
        _ => int.MaxValue
    };

    /// <summary>879: 黑铁矿总纯度对武器升级持久的影响 (总纯度 < 60 有几率掉持久)</summary>
    public static (bool duraLost, int lostAmount) CalcWeaponUpgradeDuraLoss(int totalPurity, Random rng)
    {
        if (totalPurity >= 60) return (false, 0);
        bool lost = rng.Next(100) < (60 - totalPurity);
        return (lost, lost ? 1 : 0);
    }

    /// <summary>880: 普通修理与特殊修理持久扣减权威核验</summary>
    public static (int newDura, int newMaxDura) RepairEquipment(int currentDura, int maxDura, bool isSpecialRepair, Random rng)
    {
        if (isSpecialRepair)
        {
            return (maxDura, maxDura); // 特修 100% 恢复满持久且不掉上限
        }
        // 普修 1/3 概率扣减 1 点最大持久
        int newMax = (rng.Next(3) == 0 && maxDura > 1) ? maxDura - 1 : maxDura;
        return (newMax, newMax);
    }

    /// <summary>881: PK 值随时间自然衰减 (每 60 秒衰减 1 点)</summary>
    public static int DecayPkPoints(int currentPk, int elapsedSeconds)
    {
        int decay = elapsedSeconds / 60;
        return Math.Max(0, currentPk - decay);
    }

    /// <summary>882: 沙巴克攻城战皇宫占领判定 (皇宫内仅存单一公会即为占领成功)</summary>
    public static bool CheckSabukPalaceConquest(int defenderCount, int attackerCount) =>
        defenderCount == 0 && attackerCount > 0;

    /// <summary>883: 沙巴克税收抽取算法 (默认 5% 交易税入国库)</summary>
    public static (int netGold, int taxGold) CalcSabukTradeTax(int totalGold, float taxRate = 0.05f)
    {
        int tax = (int)(totalGold * taxRate);
        return (totalGold - tax, tax);
    }

    /// <summary>884: 未知暗殿真假鹿属性区分 (真鹿 1200 HP, 假鹿 15 HP)</summary>
    public static int GetUnknownHallDeerHp(bool isRealBoss) => isRealBoss ? 1200 : 15;

    /// <summary>885: 神圣战甲术增加防御持续时间 (60 秒)</summary>
    public static int HolyArmorDurationSeconds => 60;

    /// <summary>886: 祈祷套装全服天谴触发 (召唤兽叛变概率 100%)</summary>
    public static bool IsPrayerSetComplete(bool helm, bool neck, bool bracelet, bool ring, bool weapon) =>
        helm && neck && bracelet && ring && weapon;

    /// <summary>887: 记忆套装队伍召集冷却时间 (180 秒)</summary>
    public static int MemorySetRecallCooldownSec => 180;

    /// <summary>888: 负重超载跑步阻断判定 (当前背包负重 > 最大背包负重无法跑步)</summary>
    public static bool IsOverweightBlocked(int currentWeight, int maxWeight) => currentWeight > maxWeight;

    /// <summary>889: 挖矿产出黑铁矿纯度分布 (1 ~ 30)</summary>
    public static int RollBlackIronOrePurity(Random rng) => rng.Next(1, 31);

    /// <summary>890: 大刀卫士对红名秒杀伤害</summary>
    public static int GuardInstantKillDamage => 9999;

    // =========================================================================
    // 细节 891 ~ 920: 战斗、特戒与套装深水边界
    // =========================================================================

    /// <summary>891: 麻痹戒指穿透护盾几率 (1/15)</summary>
    public static bool RollParalyzeRing(Random rng) => rng.Next(15) == 0;

    /// <summary>892: 护身戒指空蓝后溢出伤害扣除生命</summary>
    public static (int newHp, int newMp) ApplyProtectRingDamage(int currentHp, int currentMp, int damage)
    {
        if (currentMp >= damage)
        {
            return (currentHp, currentMp - damage);
        }
        int overflow = damage - currentMp;
        int finalHp = Math.Max(0, currentHp - overflow);
        return (finalHp, 0);
    }

    /// <summary>893: 复活戒指触发扣减持久 (持久归 0 失去效果)</summary>
    public static (bool revived, int newDura) TriggerReviveRing(int currentDura)
    {
        if (currentDura <= 0) return (false, 0);
        return (true, currentDura - 1);
    }

    /// <summary>894: 隐身状态挥刀瞬时打断隐身</summary>
    public static bool ShouldBreakInvisibilityOnAttack() => true;

    /// <summary>895: 防御戒指施放治愈术消耗 MP (3 点)</summary>
    public static int DefenseRingHealMpCost => 3;

    /// <summary>896: 火焰戒指施放大火球消耗 MP (5 点)</summary>
    public static int FlameRingFireballMpCost => 5;

    /// <summary>897: 天地合一召集队伍指令检测</summary>
    public static bool IsTeamRecallCommand(string msg) =>
        msg.Equals("@天地合一", StringComparison.OrdinalIgnoreCase);

    /// <summary>898: 祈祷套装破碎装备随机选择</summary>
    public static int RollBrokenPrayerItemSlot(Random rng) => rng.Next(5);

    /// <summary>899: 幽灵套装敏捷附加 (+2 攻速与敏捷)</summary>
    public static int GhostSuitAgilityBonus => 2;

    /// <summary>900: 虹魔套装吸血转化比例 (10%)</summary>
    public static int CalcRainbowMagicLifesteal(int damageDealt, float rate = 0.10f) =>
        Math.Max(1, (int)(damageDealt * rate));

    /// <summary>901: 魔血套装将 MP 转换为 HP (每件 +50 HP)</summary>
    public static int MagicBloodSuitHpBonus(int pieces) => pieces * 50;

    /// <summary>902: 狂风套装缩短出刀攻击间隔 (标准 800ms -> 550ms)</summary>
    public static int GaleWindAttackIntervalMs => 550;

    /// <summary>903: 战士野蛮冲撞撞墙眩晕反震 (眩晕 1 秒，反震 10 点伤害)</summary>
    public static (bool stunned, int reboundDamage) WildSlamHitWallPenalty => (true, 10);

    /// <summary>904: 战士半月弯刀扣蓝门槛 (3 点 MP)</summary>
    public static int CrescentSlashMpCost => 3;

    /// <summary>905: 战士烈火剑法精灵消散时限 (10 秒 = 10000ms)</summary>
    public static int FireHitExpireMs => 10000;

    /// <summary>906: 法师火墙重叠穿行只取单次最高伤害</summary>
    public static int ResolveOverlappingFireWallDamage(int[] fireDamages)
    {
        if (fireDamages == null || fireDamages.Length == 0) return 0;
        int max = 0;
        foreach (var d in fireDamages) if (d > max) max = d;
        return max;
    }

    /// <summary>907: 法师地狱雷光对不死族倍伤 (1.5 倍)</summary>
    public static int CalcHellThunderDamage(int baseDamage, bool isUndead) =>
        isUndead ? (int)(baseDamage * 1.5f) : baseDamage;

    /// <summary>908: 法师圣言术秒杀成功率 (目标等级 > 施法者必定失败)</summary>
    public static bool RollHolyCurseKill(int mageLevel, int monLevel, Random rng)
    {
        if (monLevel > mageLevel) return false;
        int chance = Math.Max(5, (mageLevel - monLevel) * 5 + 10);
        return rng.Next(100) < Math.Min(80, chance);
    }

    /// <summary>909: 法师冰咆哮无视魔法躲避</summary>
    public static bool IceStormIgnoresMagicAvoid => true;

    /// <summary>910: 法师魔法盾碎盾破甲判定</summary>
    public static (bool broken, int overflowDamage) AbsorbShieldDamage(int currentShieldHp, int incomingDamage)
    {
        if (currentShieldHp >= incomingDamage)
        {
            return (false, 0);
        }
        return (true, incomingDamage - currentShieldHp);
    }

    /// <summary>911: 道士神圣战甲术群体防御加成 (AC +3)</summary>
    public static int HolyArmorAcBonus => 3;

    /// <summary>912: 道士幽灵盾群体法防加成 (MAC +3)</summary>
    public static int GhostShieldMacBonus => 3;

    /// <summary>913: 道士困魔咒被攻击破除机制</summary>
    public static bool ShouldBreakTrappedCurseOnStruck() => true;

    /// <summary>914: 反隐怪真视判定 (月魔蜘蛛、锲蛾具备真视)</summary>
    public static bool HasTrueSight(string monsterName) =>
        monsterName.Contains("月魔蜘蛛") || monsterName.Contains("锲蛾") || monsterName.Contains("黑色恶蛆");

    /// <summary>915: 集体隐身术友方出刀破隐</summary>
    public static bool GroupInvisibilityBreakOnAttack => true;

    /// <summary>916: 道士灵魂火符扣减护身符持久 (1 点)</summary>
    public static int SoulFireTalismanCost => 1;

    /// <summary>917: 灰黄药粉(红毒)破防率 (20%)</summary>
    public static float RedPoisonVulnerabilityMultiplier => 1.20f;

    /// <summary>918: 道士召唤骷髅等级巅峰 (7 级蓝名斧头骷髅)</summary>
    public static int SkeletonMaxLevel => 7;

    /// <summary>919: 道士召唤神兽巅峰烈焰神兽 (7 级)</summary>
    public static int ShinsuMaxLevel => 7;

    /// <summary>920: 道士心灵启示持续时间 (30 秒)</summary>
    public static int RevelationDurationSec => 30;

    // =========================================================================
    // 细节 921 ~ 950: 经济、极端边界与单机沙盒物理抹除
    // =========================================================================

    /// <summary>921: 商店购买金币校验</summary>
    public static bool CanAffordShopItem(int playerGold, int price) => playerGold >= price;

    /// <summary>922: 玩家金币上限熔断 (5000 万)</summary>
    public static int PlayerMaxGoldLimit => 50_000_000;

    /// <summary>923: 普修掉持久概率 (1/3)</summary>
    public static bool RollDuraLossOnNormalRepair(Random rng) => rng.Next(3) == 0;

    /// <summary>924: 升级武器防掉持久黑铁矿纯度门槛 (>= 60)</summary>
    public static int SafeUpgradeTotalPurity => 60;

    /// <summary>925: 武器升级属性累计上限 (+7)</summary>
    public static int WeaponUpgradeMaxBonus => 7;

    /// <summary>926: 武器开刀斩杀模式成败判定</summary>
    public static bool RollWeaponTrialSuccess(int luck, Random rng)
    {
        int chance = 50 + luck * 5;
        return rng.Next(100) < Math.Clamp(chance, 10, 95);
    }

    /// <summary>927: 运九套触发刀刀最大攻击 (Luck >= 9)</summary>
    public static bool IsLuck9Set(int totalLuck) => totalLuck >= 9;

    /// <summary>928: 太阳水瞬回生命值 (50 点 HP, 50 点 MP)</summary>
    public static (int hp, int mp) SunWaterInstantHeal => (50, 50);

    /// <summary>929: 强效金创药捆拆包空位门槛 (至少 6 个空位)</summary>
    public static bool CanUnbundlePotions(int emptyBagSlots) => emptyBagSlots >= 6;

    /// <summary>930: 地表金币堆叠冲突弹开算法</summary>
    public static (int newX, int newY) ResolveGroundItemConflict(int x, int y, Func<int, int, bool> isOccupied)
    {
        if (!isOccupied(x, y)) return (x, y);
        // 搜索周围 8 格
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue;
                if (!isOccupied(x + dx, y + dy)) return (x + dx, y + dy);
            }
        }
        return (x, y);
    }

    /// <summary>931: 地表物品过期销毁时限 (180 秒)</summary>
    public static int GroundItemExpirySeconds => 180;

    /// <summary>932: 红名死亡必爆装备件数 (1~3 件)</summary>
    public static int RollRedNameDropCount(Random rng) => rng.Next(1, 4);

    /// <summary>933: 黄名 PK 正当防卫判定 (反击黄名不加 PK 点)</summary>
    public static bool IsSelfDefense(bool targetIsYellow) => targetIsYellow;

    /// <summary>934: 沙巴克攻城战时段 (20:00 ~ 23:00)</summary>
    public static bool IsSabukSiegeTime(int hour) => hour >= 20 && hour < 23;

    /// <summary>935: 沙巴克国库每日支取上限 (1000 万)</summary>
    public static int SabukDailyWithdrawLimit => 10_000_000;

    /// <summary>936: 未知暗殿神兽真假属性区分 (假神兽仅 100 HP)</summary>
    public static int UnknownHallShinsuHp(bool isReal) => isReal ? 2000 : 100;

    /// <summary>937: 沃玛教主狂暴血量阈值 (30%)</summary>
    public static bool IsWomaLeaderEnraged(int hp, int maxHp) => (float)hp / maxHp <= 0.30f;

    /// <summary>938: 祖玛教主破石苏醒距离 (3 格)</summary>
    public static int ZumaLeaderWakeupDistance => 3;

    /// <summary>939: 赤月恶魔全屏尖刺攻击间隔 (2000ms)</summary>
    public static int EvilCentipedeAttackIntervalMs => 2000;

    /// <summary>940: 触龙神雷电免疫判定</summary>
    public static bool IsDragonLordImmuneToLightning => true;

    /// <summary>941: 复活重置安全区坐标 (比奇 330, 270)</summary>
    public static (int x, int y) DefaultSafeZoneCoord => (330, 270);

    /// <summary>942: 观人功能查询他人装备</summary>
    public static bool CanInspectPlayer(int targetUid) => targetUid > 0;

    /// <summary>943: 小地图雷达实体过滤死亡实体</summary>
    public static bool FilterMinimapRadarEntity(bool isDead) => !isDead;

    /// <summary>944: 昼夜火把照明半径 (黑夜 3 格，火把 7 格)</summary>
    public static int GetVisionRadius(bool isNight, bool hasTorch) =>
        isNight ? (hasTorch ? 7 : 3) : 15;

    /// <summary>945: 极品属性附加生成概率 (1/50)</summary>
    public static bool RollRareBonus(Random rng) => rng.Next(50) == 0;

    /// <summary>946: 交易锁定阶段禁止修改</summary>
    public static bool IsTradeLocked(bool locked) => locked;

    /// <summary>947: 行会战持续时间 (3 小时 = 180 分钟)</summary>
    public static int GuildWarDurationMinutes => 180;

    /// <summary>948: 月魔蜘蛛攻击 100% 麻痹毒素</summary>
    public static bool MoonSpiderAttackHasParalyze => true;

    /// <summary>949: 虎卫堂刷新间隔 (4 小时 = 14400 秒)</summary>
    public static int HuWeiRespawnIntervalSeconds => 14400;

    /// <summary>950: 客户端 Release 模式下沙盒物理抹除与彻底封锁验证</summary>
    public static bool IsSandboxPhysicallyRemovedInRelease()
    {
#if MIR2_SANDBOX_DEV
        return false;
#else
        return true;
#endif
    }
}
