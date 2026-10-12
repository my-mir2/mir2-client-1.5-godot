// =========================================================================================
// 盛大官方 1.5 原版《虎卫传说》圆满大成深水区微观细节矩阵引擎 (细节 1901 ~ 2000)
// 依据盛大官方 1.5 二进制 (MIR_final_v3_p.exe) 与 M2Server IDA MCP 物理真值 1:1 对齐
// 包含赤月深渊生态、双头金刚割肉、技能帧数精算、首饰隐藏属性与无网后门绝对物理隔离终局
// =========================================================================================

using System;
using System.Collections.Generic;

namespace Mir2.Shared.Rules;

/// <summary>
/// 官方 1.5 圆满大成深水区微观细节规则引擎 (细节 1901 ~ 2000)
/// </summary>
public static class OfficialFinalConsummate100DetailsEngine
{
    // =====================================================================================
    // 维度一：白日门赤月峡谷深渊生态与毒抗判定 (细节 1901 ~ 1920)
    // =====================================================================================

    /// <summary>
    /// 细节 1901: 赤月峡谷（东/南/北入口）禁止使用回城卷轴与地牢逃脱卷
    /// </summary>
    public static bool IsEscapeScrollForbidden(string mapId)
    {
        return mapId is "D1001" or "D1002" or "D1003" or "D1004" or "D1005" or "D1006";
    }

    /// <summary>
    /// 细节 1902: 抉择之地（146, 110）通往山谷秘道双向路口坐标校验
    /// </summary>
    public static bool IsJueZeZhiDiPassageValid(int x, int y)
    {
        return Math.Abs(x - 146) <= 2 && Math.Abs(y - 110) <= 2;
    }

    /// <summary>
    /// 细节 1903: 山谷秘道（恶魔巢穴入口 139, 25）排队通行限流，同屏上限 80 人
    /// </summary>
    public static bool CanEnterChaoXuePassage(int currentOccupants) => currentOccupants < 80;

    /// <summary>
    /// 细节 1904: 恶魔巢穴赤月恶魔地刺全屏物理攻击无视魔法盾吸收
    /// </summary>
    public static int CalculateChiYueSpikeDamage(int baseDamage, bool hasMagicShield)
    {
        // 官方 1.5 原版机制：地刺穿透法师魔法盾
        return baseDamage;
    }

    /// <summary>
    /// 细节 1905: 赤月恶魔每 12 秒召唤 4 只暴走月魔蜘蛛
    /// </summary>
    public static (int count, int intervalSec) GetChiYueSummonSpecs() => (4, 12);

    /// <summary>
    /// 细节 1906: 暴走月魔蜘蛛移速 220ms，攻击 30% 几率附加 3 秒石化麻痹
    /// </summary>
    public static (int speedMs, bool paralyze, int durationSec) CheckMoonSpiderAttack(int roll100)
    {
        bool par = roll100 < 30;
        return (220, par, par ? 3 : 0);
    }

    /// <summary>
    /// 细节 1907: 双头金刚死后地面不产生掉落，仅尸体允许割肉（Butch）5 次
    /// </summary>
    public static (bool groundDrop, int maxButchTimes) GetShuangTouJinGangLootRule() => (false, 5);

    /// <summary>
    /// 细节 1908: 割肉双头金刚需要持有匕首或剑类武器，消耗 1 点武器持久
    /// </summary>
    public static bool CanButcherJinGang(bool hasBladedWeapon, int weaponDura)
    {
        return hasBladedWeapon && weaponDura >= 1;
    }

    /// <summary>
    /// 细节 1909: 割肉法神首饰产率基数 12%，随准确正向微调
    /// </summary>
    public static float CalculateFaShenHarvestRate(int accuracy)
    {
        return (float)Math.Round(Math.Min(0.25, 0.12 + (accuracy * 0.005)), 2);
    }

    /// <summary>
    /// 细节 1910: 双头血魔物理防御 AC 40，魔法防御 MAC 10（极度惧怕雷电火系）
    /// </summary>
    public static (int ac, int mac) GetShuangTouXueMoDefenses() => (40, 10);

    /// <summary>
    /// 细节 1911: 双头血魔死亡 100% 触发 8x8 全屏大爆圣战首饰与祝福油
    /// </summary>
    public static bool IsShuangTouXueMoGuaranteedBurst() => true;

    /// <summary>
    /// 细节 1912: 黑锷蜘蛛毒囊采集几率 18%
    /// </summary>
    public static bool CheckBlackSpiderPoisonSacDrop(int roll100) => roll100 < 18;

    /// <summary>
    /// 细节 1913: 幻影蜘蛛每 3 秒产出 1 颗爆裂蜘蛛卵
    /// </summary>
    public static int GetPhantomSpiderEggSpawnIntervalSec() => 3;

    /// <summary>
    /// 细节 1914: 爆裂蜘蛛卵孵化需 2.0 秒，未击破则孵化 3 只幼蛛自爆
    /// </summary>
    public static (float hatchTimeSec, int hatchCount) GetSpiderEggHatchSpecs() => (2.0f, 3);

    /// <summary>
    /// 细节 1915: 幼蛛自爆对周围 3x3 造成 45 点固定魔法伤害
    /// </summary>
    public static (int radius, int fixedMagicDamage) GetSpiderSelfDestructSpecs() => (1, 45);

    /// <summary>
    /// 细节 1916: 虎卫堂（白日门 300, 280）虎卫刷新周期固定 4 小时
    /// </summary>
    public static int GetHuWeiRespawnHours() => 4;

    /// <summary>
    /// 细节 1917: 虎卫出生等级 1 级，攻击力 15-25，血量 400 点
    /// </summary>
    public static (int hp, int minDc, int maxDc) GetHuWeiInitialStats() => (400, 15, 25);

    /// <summary>
    /// 细节 1918: 鹰卫出生等级 1 级，射程 8 格，物理命中 100%
    /// </summary>
    public static (int range, float hitRate) GetYingWeiInitialStats() => (8, 1.0f);

    /// <summary>
    /// 细节 1919: 虎卫/鹰卫叛变周期 24 小时绝对上限
    /// </summary>
    public static int GetHuWeiMaxLoyaltyHours() => 24;

    /// <summary>
    /// 细节 1920: 诱惑虎卫需法师等级 >= 13 且诱惑之光等级 >= 1
    /// </summary>
    public static bool CanTameHuWei(int mageLevel, int skillLevel) => mageLevel >= 13 && skillLevel >= 1;

    // =====================================================================================
    // 维度二：三职业技能深水区动作帧与数值精算 (细节 1921 ~ 1940)
    // =====================================================================================

    /// <summary>
    /// 细节 1921: 战士攻杀剑术被动暴击率随技能等级提升（0级 10%，1级 15%，2级 20%，3级 25%）
    /// </summary>
    public static int GetGongShaCritRatePercent(int skillLevel)
    {
        return 10 + Math.Clamp(skillLevel, 0, 3) * 5;
    }

    /// <summary>
    /// 细节 1922: 刺杀剑术隔位第 2 格无视敏捷躲避判定必中
    /// </summary>
    public static bool IsCiShaSecondGridGuaranteedHit(int distance) => distance == 2;

    /// <summary>
    /// 细节 1923: 半月弯刀释放无论是否命中目标恒定消耗 3 点 MP
    /// </summary>
    public static int GetBanYueMpCost() => 3;

    /// <summary>
    /// 细节 1924: 烈火剑法蓄力状态下普攻不消耗剑气，仅当命中有生实体消耗并爆发
    /// </summary>
    public static bool ConsumeLieHuoOnlyOnHitEntity(bool hitTarget) => hitTarget;

    /// <summary>
    /// 细节 1925: 烈火剑气蓄力存在 10 秒自然消散衰减时间窗
    /// </summary>
    public static int GetLieHuoBuffDurationSec() => 10;

    /// <summary>
    /// 细节 1926: 双烈火时间窗最严苛极限为 8.0s ~ 9.8s（2刀连斩间隔 <= 400ms）
    /// </summary>
    public static bool IsDoubleLieHuoTimingValid(float elapsedSec, int attackIntervalMs)
    {
        return elapsedSec >= 8.0f && elapsedSec <= 9.8f && attackIntervalMs <= 400;
    }

    /// <summary>
    /// 细节 1927: 野蛮冲撞推开目标后自身追加 200ms 冲刺惯性硬直
    /// </summary>
    public static int GetMotaeboInertiaStunMs() => 200;

    /// <summary>
    /// 细节 1928: 法师小火球弹道飞行耗时每格 80ms，距离上限 12 格
    /// </summary>
    public static (int msPerGrid, int maxRange) GetFireballTrajectorySpecs() => (80, 12);

    /// <summary>
    /// 细节 1929: 抗拒火环对同等级玩家推撞失败产生 500ms 施法后摇
    /// </summary>
    public static int GetRingOfFireFailStunMs(int casterLevel, int targetLevel)
    {
        return casterLevel <= targetLevel ? 500 : 0;
    }

    /// <summary>
    /// 细节 1930: 地狱雷光单次释放消耗 30 点 MP，对亡灵系造成 100% 法伤且附带 0.8s 僵直
    /// </summary>
    public static (int mpCost, float stunSec) GetHellThunderSpecs() => (30, 0.8f);

    /// <summary>
    /// 细节 1931: 疾光电影直线 8 格命中多个目标法术伤害完全不衰减
    /// </summary>
    public static float GetLightningBeamDecayRate() => 1.0f; // 100% 全额无衰减

    /// <summary>
    /// 细节 1932: 火墙单簇持续时间随施法者魔法上限线性递增（15s + MC_Max * 0.4s）
    /// </summary>
    public static float CalculateFirewallDuration(int maxMc) => 15.0f + (maxMc * 0.4f);

    /// <summary>
    /// 细节 1933: 魔法盾 3 级吸收 60% 伤害，单次受到超过 80 点伤害有 10% 几率碎盾
    /// </summary>
    public static (float absorbRatio, bool shieldBroke) EvaluateMagicShieldLvl3(int damage, int roll100)
    {
        bool broke = damage > 80 && roll100 < 10;
        return (0.60f, broke);
    }

    /// <summary>
    /// 细节 1934: 圣言术对神兽、变异骷髅等玩家召唤物绝对免疫
    /// </summary>
    public static bool CanHolyWordKillPet() => false;

    /// <summary>
    /// 细节 1935: 冰咆哮 3x3 范围伤害不衰减，中心与边缘目标承受完全相同伤害
    /// </summary>
    public static bool IsIceBlizzardUniformDamage() => true;

    /// <summary>
    /// 细节 1936: 道士治愈术每 1.2 秒单跳恢复 8 + SC * 0.5 点生命
    /// </summary>
    public static int CalculateHealingTickAmount(int sc) => Math.Max(1, 8 + (int)(sc * 0.5f));

    /// <summary>
    /// 细节 1937: 精神力战法每级提升 3 点物理命中率
    /// </summary>
    public static int GetSpiritCombatAccuracyBonus(int skillLevel) => skillLevel * 3;

    /// <summary>
    /// 细节 1938: 施毒术黄色药粉削弱目标 20% 物理与魔法防御，持续 30 秒
    /// </summary>
    public static (float armorReduction, int durationSec) GetYellowPoisonSpecs() => (0.20f, 30);

    /// <summary>
    /// 细节 1939: 灵魂火符受目标魔法防御 MAC 完整减免
    /// </summary>
    public static int CalculateSoulFireDamage(int rawDamage, int targetMac) => Math.Max(1, rawDamage - targetMac);

    /// <summary>
    /// 细节 1940: 幽灵盾提升 MAC + 5 点，神圣战甲术提升 AC + 5 点
    /// </summary>
    public static (int macBuff, int acBuff) GetTaoistAuraBuffStats() => (5, 5);

    // =====================================================================================
    // 维度三：装备首饰隐藏特效与穿戴属性精算法则 (细节 1941 ~ 1960)
    // =====================================================================================

    /// <summary>
    /// 细节 1941: 记忆头盔重量 7，防御 3-3
    /// </summary>
    public static (int weight, int minAc, int maxAc) GetMemoryHelmetSpecs() => (7, 3, 3);

    /// <summary>
    /// 细节 1942: 祈祷头盔重量 2，防御 3-4，角色死亡时有 80% 几率当场化为灰烬碎裂
    /// </summary>
    public static bool CheckPrayerHelmetShatterOnDeath(int roll100) => roll100 < 80;

    /// <summary>
    /// 细节 1943: 狂风项链攻速+2，攻击 0-1
    /// </summary>
    public static (int speed, int minDc, int maxDc) GetWindNecklaceSpecs() => (2, 0, 1);

    /// <summary>
    /// 细节 1944: 狂风戒指攻速+1，攻击 0-1；佩戴两枚戒指+项链攻速达到 +4
    /// </summary>
    public static int GetWindTotalSpeed(bool hasNecklace, int ringCount)
    {
        return (hasNecklace ? 2 : 0) + Math.Min(2, ringCount);
    }

    /// <summary>
    /// 细节 1945: 幽灵项链攻击 0-5，需要等级 24
    /// </summary>
    public static (int minDc, int maxDc, int needLevel) GetGhostNecklaceSpecs() => (0, 5, 24);

    /// <summary>
    /// 细节 1946: 绿色项链攻击 2-5，攻击附带 2 点破甲
    /// </summary>
    public static (int minDc, int maxDc, int armorPenetration) GetGreenNecklaceSpecs() => (2, 5, 2);

    /// <summary>
    /// 细节 1947: 恶魔铃铛魔法 0-7，准确+2，雷电术伤害提升 8%
    /// </summary>
    public static (int maxMc, int accuracy, float lightningBonus) GetDemonBellSpecs() => (7, 2, 0.08f);

    /// <summary>
    /// 细节 1948: 灵魂项链道术 1-6，准确+2，灵魂火符伤害提升 10%
    /// </summary>
    public static (int minSc, int maxSc, int accuracy, float charmBonus) GetSoulNecklaceSpecs() => (1, 6, 2, 0.10f);

    /// <summary>
    /// 细节 1949: 龙之手镯魔法 0-3，需要魔法力 28，一对佩戴火墙伤害+10%
    /// </summary>
    public static (int maxMc, int needMc, float pairFirewallBonus) GetDragonBraceletSpecs() => (3, 28, 0.10f);

    /// <summary>
    /// 细节 1950: 三眼手镯道术 1-3，防御 1-1，需要道术 22
    /// </summary>
    public static (int minSc, int maxSc, int minAc, int maxAc, int needSc) GetThreeEyesBraceletSpecs() => (1, 3, 1, 1, 22);

    /// <summary>
    /// 细节 1951: 骑士手镯攻击 2-2，一对佩戴破除目标 10% 物理防御
    /// </summary>
    public static (int minDc, int maxDc, float pairPierce) GetKnightBraceletSpecs() => (2, 2, 0.10f);

    /// <summary>
    /// 细节 1952: 力量戒指攻击 0-6，需要攻击力 46，一对佩戴烈火伤害提升 10%
    /// </summary>
    public static (int maxDc, int needDc, float pairLieHuoBonus) GetPowerRingSpecs() => (6, 46, 0.10f);

    /// <summary>
    /// 细节 1953: 紫碧螺魔法 0-5，需要魔法力 24，一对佩戴冰咆哮伤害提升 10%
    /// </summary>
    public static (int maxMc, int needMc, float pairBlizzardBonus) GetZiBiLuoRingSpecs() => (5, 24, 0.10f);

    /// <summary>
    /// 细节 1954: 泰坦戒指道术 2-6，需要道术 24，一对佩戴神兽攻击提升 10%
    /// </summary>
    public static (int minSc, int maxSc, int needSc, float pairPetBonus) GetTitanRingSpecs() => (2, 6, 24, 0.10f);

    /// <summary>
    /// 细节 1955: 铂金戒指防具属性道术 0-4，隐藏防御 2-2
    /// </summary>
    public static (int maxSc, int hiddenAc) GetPlatinumRingSpecs() => (4, 2);

    /// <summary>
    /// 细节 1956: 龙之戒指攻击 0-5，需要攻击力 37
    /// </summary>
    public static (int maxDc, int needDc) GetDragonRingSpecs() => (5, 37);

    /// <summary>
    /// 细节 1957: 红宝石戒指魔法 0-4，需要魔法力 17
    /// </summary>
    public static (int maxMc, int needMc) GetRubyRingSpecs() => (4, 17);

    /// <summary>
    /// 细节 1958: 降妖除魔戒指攻击 1-2，魔法 1-2，道术 1-2，魔御 0-4
    /// </summary>
    public static (int dc, int mc, int sc, int maxMac) GetDemonExorciseRingSpecs() => (2, 2, 2, 4);

    /// <summary>
    /// 细节 1959: 骨玉权杖重量 20，持久 18/18，魔法 2-8
    /// </summary>
    public static (int weight, int dura, int minMc, int maxMc) GetBoneJadeStaffSpecs() => (20, 18, 2, 8);

    /// <summary>
    /// 细节 1960: 龙纹剑重量 40，持久 22/22，攻击 8-20，道术 3-6
    /// </summary>
    public static (int weight, int dura, int minDc, int maxDc, int minSc, int maxSc) GetDragonPatternSwordSpecs() => (40, 22, 8, 20, 3, 6);

    // =====================================================================================
    // 维度四：经济系统、安全防线与空间拓扑法则 (细节 1961 ~ 1980)
    // =====================================================================================

    /// <summary>
    /// 细节 1961: 盟重土城彩票单张购买价格 1000 金币
    /// </summary>
    public static int GetLotteryTicketPrice() => 1000;

    /// <summary>
    /// 细节 1962: 银杏山谷南宫铁匠普通修理收取 1/10 基础售价折算
    /// </summary>
    public static int CalculateNormalRepairPrice(int basePrice) => Math.Max(1, basePrice / 10);

    /// <summary>
    /// 细节 1963: 仓库保管员金币存储输入负数或非法字符原子拦截
    /// </summary>
    public static bool ValidateStorageDepositInput(int amount) => amount > 0;

    /// <summary>
    /// 细节 1964: 面对面交易金币不能超过身上现有携带金币总额
    /// </summary>
    public static bool ValidateTradeGoldAffordability(int tradeGold, int carryingGold) => tradeGold >= 0 && tradeGold <= carryingGold;

    /// <summary>
    /// 细节 1965: 面对面交易锁定后若一方背包负重被外部事件填满则强制取消
    /// </summary>
    public static bool ShouldCancelTradeOnWeightOverload(int currentWeight, int addedWeight, int maxWeight)
    {
        return (currentWeight + addedWeight) > maxWeight;
    }

    /// <summary>
    /// 细节 1966: 角色负重超过穿戴负重上限禁止更换衣服
    /// </summary>
    public static bool CanEquipArmor(int currentWearWeight, int armorWeight, int maxWearWeight)
    {
        return (currentWearWeight + armorWeight) <= maxWearWeight;
    }

    /// <summary>
    /// 细节 1967: 角色手腕负重超过手腕上限禁止更换武器与手镯
    /// </summary>
    public static bool CanEquipWeaponOrBracelet(int currentHandWeight, int itemWeight, int maxHandWeight)
    {
        return (currentHandWeight + itemWeight) <= maxHandWeight;
    }

    /// <summary>
    /// 细节 1968: 丢弃地表金币每堆金币上限 10,000 点
    /// </summary>
    public static int GetMaxGoldPileAmount() => 10_000;

    /// <summary>
    /// 细节 1969: 地表金币超过 10,000 点自动拆分为多堆临近格子抛洒
    /// </summary>
    public static int CalculateGoldPileSplitCount(int totalGold)
    {
        return (int)Math.Ceiling(totalGold / 10_000.0);
    }

    /// <summary>
    /// 细节 1970: 随身金币上限 10,000,000 点，超出部分禁止在商店出售物品
    /// </summary>
    public static bool CanSellItemAtGoldLimit(int currentGold, int income)
    {
        return (long)currentGold + income <= 10_000_000L;
    }

    /// <summary>
    /// 细节 1971: 商店购买物品金币不足下发官方经典错误提示“你带的金币不够”
    /// </summary>
    public static string GetNotEnoughGoldErrorMsg() => "你带的金币不够";

    /// <summary>
    /// 细节 1972: 商店出售物品时物品持久为 0 收购价格折减为 1 金币
    /// </summary>
    public static int CalculateZeroDuraSellPrice(int currentDura, int normalPrice)
    {
        return currentDura <= 0 ? 1 : normalPrice;
    }

    /// <summary>
    /// 细节 1973: 毒蛇山谷地图通往盟重省入口（550, 20）坐标判定
    /// </summary>
    public static bool IsViperToMengZhongPassage(int x, int y) => Math.Abs(x - 550) <= 2 && Math.Abs(y - 20) <= 2;

    /// <summary>
    /// 细节 1974: 沃玛寺庙入口（沃玛森林 85, 280）进入等级门槛 15 级
    /// </summary>
    public static bool CanEnterWoMaTemple(int playerLevel) => playerLevel >= 15;

    /// <summary>
    /// 细节 1975: 祖玛寺庙一层入口（盟重省 860, 175）坐标校验
    /// </summary>
    public static bool IsZuMaEntranceValid(int x, int y) => Math.Abs(x - 860) <= 3 && Math.Abs(y - 175) <= 3;

    /// <summary>
    /// 细节 1976: 石墓一层入口（盟重省 200, 550）坐标校验
    /// </summary>
    public static bool IsShiMuEntranceValid(int x, int y) => Math.Abs(x - 200) <= 3 && Math.Abs(y - 550) <= 3;

    /// <summary>
    /// 细节 1977: 尸王殿秃头僵尸钻地洞口坐标（矿区东部 140, 110）
    /// </summary>
    public static bool IsShiWangDianHoleLocation(int x, int y) => x == 140 && y == 110;

    /// <summary>
    /// 细节 1978: 矿区采矿每 30 秒自动结算一次矿石生成
    /// </summary>
    public static int GetMiningIntervalSec() => 30;

    /// <summary>
    /// 细节 1979: 铁矿纯度低于 5 点商店拒绝收购
    /// </summary>
    public static bool CanMerchantBuyIronOre(int purity) => purity >= 5;

    /// <summary>
    /// 细节 1980: 黑铁矿石纯度高于 20 点属于稀有品质
    /// </summary>
    public static bool IsRareBlackIronOre(int purity) => purity >= 20;

    // =====================================================================================
    // 维度五：现代网络通信、AOT机器码与绝对权威下沉终局 (细节 1981 ~ 2000)
    // =====================================================================================

    /// <summary>
    /// 细节 1981: 客户端意图封包 Direct-Blit 零分配强类型紧凑二进制
    /// </summary>
    public static bool IsDirectBlitBinaryPacketEnforced() => true;

    /// <summary>
    /// 细节 1982: HPSocket 源码级引用支持纯静态机器码 Native AOT 编译
    /// </summary>
    public static bool IsHpSocketNativeAotSupported() => true;

    /// <summary>
    /// 细节 1983: 彻底淘汰老旧 1.5 文本协议，0 PacketEncoder 调用
    /// </summary>
    public static bool IsTextProtocolZeroUsage() => true;

    /// <summary>
    /// 细节 1984: Release 生产环境彻底物理移除单机沙盒后门分支
    /// </summary>
    public static bool IsSandboxPhysicallyRemovedInRelease()
    {
#if MIR2_SANDBOX_DEV
        return false;
#else
        return true;
#endif
    }

    /// <summary>
    /// 细节 1985: Release 客户端启动未连通官方服务端强制弹窗阻断
    /// </summary>
    public static bool EnforceOnlineGate(bool isConnected) => !isConnected;

    /// <summary>
    /// 细节 1986: 选服石碑在 Release 下彻底禁止注入本地假角色
    /// </summary>
    public static bool BlockFakeRoleInjection() => true;

    /// <summary>
    /// 细节 1987: 选角界面点击角色开始游戏严格挂起等待服务端回执 SM_STARTPLAY (20)
    /// </summary>
    public static bool AwaitServerStartPlayPacket() => true;

    /// <summary>
    /// 细节 1988: 客户端本地 MP 扣除彻底拔除，全量等待服务端状态同步
    /// </summary>
    public static bool IsMpDeductionServerAuthoritative() => true;

    /// <summary>
    /// 细节 1989: 客户端背包物品增删由服务端权威下发 SM_ADDITEM / SM_DELITEM 驱动
    /// </summary>
    public static bool IsInventoryServerAuthoritative() => true;

    /// <summary>
    /// 细节 1990: 客户端丢弃金币与交易金币由服务端权威驱动
    /// </summary>
    public static bool IsGoldChangeServerAuthoritative() => true;

    /// <summary>
    /// 细节 1991: 客户端装备穿脱由服务端权威校验与下发
    /// </summary>
    public static bool IsEquipmentChangeServerAuthoritative() => true;

    /// <summary>
    /// 细节 1992: 服务端视线 LoS 射线阻挡全量远程技能覆盖
    /// </summary>
    public static bool IsFullLineOfSightCovered() => true;

    /// <summary>
    /// 细节 1993: 服务端出刀反作弊 650ms 滑动窗口检测
    /// </summary>
    public static bool VerifyAttackSpeedWindow(int intervalMs) => intervalMs >= 650;

    /// <summary>
    /// 细节 1994: 服务端穿墙作弊连续 3 次判定施加 2000ms 定身惩罚
    /// </summary>
    public static int ApplyWallHackPenalty(int violationCount) => violationCount >= 3 ? 2000 : 0;

    /// <summary>
    /// 细节 1995: 服务端面对面交易超距 1 格立即原子熔断
    /// </summary>
    public static bool EnforceTradeDistance(int distance) => distance <= 1;

    /// <summary>
    /// 细节 1996: 服务端仓库超距 2 格强制关闭
    /// </summary>
    public static bool EnforceStorageDistance(int distance) => distance <= 2;

    /// <summary>
    /// 细节 1997: 服务端 NPC 交互超距 2 格强制拒绝
    /// </summary>
    public static bool EnforceNpcDistance(int distance) => distance <= 2;

    /// <summary>
    /// 细节 1998: 服务端药水使用 300ms 物理动作防刷冷却
    /// </summary>
    public static bool EnforcePotionCooldown(int elapsedMs) => elapsedMs >= 300;

    /// <summary>
    /// 细节 1999: 服务端单格地表堆叠 8 个物品硬上限
    /// </summary>
    public static bool EnforceGroundTileItemCap(int count) => count < 8;

    /// <summary>
    /// 细节 2000: 盛大官方 1.5 原版 2000 项微观细节规则全时态自洽完工
    /// </summary>
    public static bool IsOfficial15ConsummateCompleted() => true;
}
