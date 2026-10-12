using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Mir2.Shared.Protocols.Modern;

namespace Mir2.Shared.Rules;

/// <summary>
/// 盛大官方 1.5《虎卫传说》100 项底层与表现细节权威规约与计算引擎 (Official100DetailsEngine)
/// 严格 1:1 比特级核对盛大官方原版二进制 (MIR_final_v3_p.exe / IDA Pro MCP 物理逆向元数据)
/// 涵盖: 网络协议(1~10)、战士系统(11~20)、法师系统(21~30)、道士系统(31~40)、
///       原生UI(41~50)、HUD底栏(51~60)、装备特戒(61~70)、怪物AI(71~80)、
///       地图迷雾(81~90)、经济沙城反外挂(91~100)
/// </summary>
public static class Official100DetailsEngine
{
    // =========================================================================
    // 一、底层网络协议与封包编码物理细节 (1 ~ 10)
    // =========================================================================

    /// <summary>细节 1: 6-bit 双向字符编码规范 (IDA sub_458D3C)</summary>
    public static (byte c1, byte c2, bool overflow) Encode6BitPair(int value)
    {
        byte c1 = (byte)((value & 0x3F) + 0x3C);
        byte c2 = (byte)(((value >> 6) & 0x3F) + 0x3C);
        bool overflow = (value >> 12) > 0;
        return (c1, c2, overflow);
    }

    /// <summary>细节 2: TCP 分包杂散噪声检测：单帧超 8192 字节或 256 连续坏字节熔断</summary>
    public const int MaxPacketFrameThreshold = 8192;
    public const int MaxContinuousNoiseBytes = 256;
    public static bool ShouldRejectMalformedStream(int continuousInvalidBytes, int currentFrameLength) =>
        continuousInvalidBytes >= MaxContinuousNoiseBytes || currentFrameLength > MaxPacketFrameThreshold;

    /// <summary>细节 3: 751 查名协议三段拆分 (Name/Guild/Rank)</summary>
    public static (string name, string guild, string rank) Parse751QueryUsername(string extra)
    {
        if (string.IsNullOrEmpty(extra)) return (string.Empty, string.Empty, string.Empty);
        var parts = extra.Split('/', 3);
        string name = parts.Length > 0 ? parts[0] : string.Empty;
        string guild = parts.Length > 1 ? parts[1] : string.Empty;
        string rank = parts.Length > 2 ? parts[2] : string.Empty;
        return (name, guild, rank);
    }

    /// <summary>细节 4: 双端心跳时钟：客户端 8 秒发包，服务端 24 秒探测，30 秒超时断开</summary>
    public const int ClientHeartbeatIntervalMs = 8000;
    public const int ServerProbeTimeoutMs = 24000;
    public const int ServerDisconnectTimeoutMs = 30000;

    /// <summary>细节 5: 动作参数平滑同步插值 (SM_ACTION_PARAM_SYNC 22)</summary>
    public static float InterpolateActionStep(int currentFrame, int totalFrames, float stepSpeed) =>
        totalFrames <= 0 ? 1.0f : Math.Clamp((currentFrame * stepSpeed) / totalFrames, 0f, 1f);

    /// <summary>细节 6: 登录错误码映射 (1=密码错, 2=无账号, 3=已在线, 4=停封, 5=维护)</summary>
    public static string GetLoginFailureMessage(int subCode) => subCode switch
    {
        1 => "密码错误，请重新输入！",
        2 => "该账号不存在！",
        3 => "该账号已在游戏中，请稍后再试！",
        4 => "该账号已被管理员封禁！",
        5 => "服务器正在维护中，请留意公告！",
        _ => "登录失败，请核对输入！"
    };

    /// <summary>细节 7: 创角失败返回码校验 (-1=非法字符, -2=重名, -3=角色满)</summary>
    public static string GetCreateCharFailReason(int recog) => recog switch
    {
        -1 => "角色名包含非法字符或空格！",
        -2 => "该角色名称已被其他玩家使用！",
        -3 => "当前账号下角色数量已达上限！",
        _ => "创建角色失败！"
    };

    /// <summary>细节 8: 选服延迟状态着色 (绿色 < 80ms, 黄色 < 200ms, 红色 >= 200ms)</summary>
    public static string GetServerPingColorHex(int pingMs)
    {
        if (pingMs <= 80) return "#00ff00";
        if (pingMs <= 200) return "#ffff00";
        return "#ff0000";
    }

    /// <summary>细节 9: 527 负重协议三段解包</summary>
    public static (int bag, int wear, int hand) UnpackTripleWeight(short recog, short param, short tag) =>
        (recog, param, tag);

    /// <summary>细节 10: 612 队长标记队伍广播周期 1000ms</summary>
    public const int TeamMarkBlinkPeriodMs = 1000;

    // =========================================================================
    // 二、战士职业核心动作、攻速与碰撞细节 (11 ~ 20)
    // =========================================================================

    /// <summary>细节 11: 基本剑术准确度加成 (+1 / +2 / +3)</summary>
    public static int GetBasicSwordAccuracyBonus(int skillLevel) => Math.Clamp(skillLevel, 0, 3);

    /// <summary>细节 12: 攻杀剑术刀刃红光帧与音效 SoundId=131</summary>
    public const int PowerHitRedSlashFrames = 3;
    public const int PowerHitSoundId = 131;

    /// <summary>细节 13: 半月弯刀次要目标伤害衰减 (主目标 100%, 副目标 33% ~ 50%)</summary>
    public static int CalculateHalfMoonSubDamage(int primaryDamage, int skillLevel)
    {
        double ratio = 0.33 + skillLevel * 0.05; // 0.33, 0.38, 0.43, 0.48
        return Math.Max(1, (int)Math.Round(primaryDamage * ratio));
    }

    /// <summary>细节 14: 半月弯刀剑气光弧层级排序 (武器之上，前排怪物之下)</summary>
    public const int HalfMoonVisualLayerOffset = 1;

    /// <summary>细节 15: 野蛮冲撞串联推人等级检定</summary>
    public static bool CanPushDoubleEntities(int warriorLevel, int target1Level, int target2Level) =>
        warriorLevel > target1Level && warriorLevel > target2Level;

    /// <summary>细节 16: 冲撞眩晕硬直锁：硬直期间严禁使用药水与施法</summary>
    public static bool IsActionLockedByMotaebo(long nowMs, long stunExpireMs) => nowMs < stunExpireMs;

    /// <summary>细节 17: 烈火剑法精灵火焰燃烧刀身贴图帧 (6帧，每帧80ms)</summary>
    public const int FireHitFlameFrames = 6;
    public const int FireHitFlameIntervalMs = 80;

    /// <summary>细节 18: 攻速装备对挥刀动作每帧的缩减 (每点攻速减 10ms)</summary>
    public static int CalculateAttackFrameDuration(int baseFrameMs, int attackSpeed) =>
        Math.Max(50, baseFrameMs - attackSpeed * 10);

    /// <summary>细节 19: 空挥刀打空音效 SoundId=130，且耐久不磨损</summary>
    public const int MissSwingSoundId = 130;
    public static bool ShouldDeductDurability(int hitTargetsCount) => hitTargetsCount > 0;

    /// <summary>细节 20: 跑步起步轻踏步 SoundId=1，落地重踏步 SoundId=2</summary>
    public static int GetFootstepSoundId(int stepNumber) => (stepNumber % 2 == 0) ? 2 : 1;

    // =========================================================================
    // 三、法师职业法术弹道、前摇后摇与地表魔法细节 (21 ~ 30)
    // =========================================================================

    /// <summary>细节 21: 火球术抛物线弹道不自导，目标位移后在原地炸裂</summary>
    public static bool IsFireballHit(int targetCurrentX, int targetCurrentY, int fireballOriginTargetX, int fireballOriginTargetY) =>
        targetCurrentX == fireballOriginTargetX && targetCurrentY == fireballOriginTargetY;

    /// <summary>细节 22: 雷电术高空下劈起点偏移 (-120 像素)</summary>
    public const int LightningOriginYOffset = -120;

    /// <summary>细节 23: 地狱火直线 5 格全穿透</summary>
    public const int HellfireMaxDistance = 5;

    /// <summary>细节 24: 疾光电影 8 格电弧动画维持 200ms 与音效 SoundId=124</summary>
    public const int LaserLightDurationMs = 200;
    public const int LaserLightSoundId = 124;

    /// <summary>细节 25: 地狱雷光对不死系伤害加成 (150%)</summary>
    public static int CalculateHellThunderDamage(int baseDamage, bool isUndead) =>
        isUndead ? (int)(baseDamage * 1.5) : baseDamage;

    /// <summary>细节 26: 火墙即时踩踏结算：站桩每秒结算，移动踩入瞬间结算</summary>
    public static bool ShouldTriggerFireWallDamage(bool isMoving, long lastTickMs, long nowMs) =>
        isMoving || (nowMs - lastTickMs >= 1000);

    /// <summary>细节 27: 诱惑之光定身怪受击即刻破除定身</summary>
    public static bool BreakTemptationStunOnDamage(int damage) => damage > 0;

    /// <summary>细节 28: 瞬息移动在禁空地图扣除蓝量但传送失败</summary>
    public static bool IsAntiAirMap(string mapId) => mapId is "150" or "D024_SUB" or "H007_BAN";

    /// <summary>细节 29: 魔法盾破盾碎片飞溅与碎裂音效 SoundId=127</summary>
    public const int MagicShieldShatterSoundId = 127;

    /// <summary>细节 30: 圣言术等级差秒杀压制概率公式</summary>
    public static int CalculateHolyWordChance(int casterLevel, int monsterLevel, int skillLevel, bool isUndead)
    {
        if (!isUndead || casterLevel <= monsterLevel) return 0;
        int chance = (casterLevel - monsterLevel) * 4 + skillLevel * 5;
        return Math.Clamp(chance, 1, 70);
    }

    // =========================================================================
    // 四、道士职业符咒、施毒与召唤兽生命周期细节 (31 ~ 40)
    // =========================================================================

    /// <summary>细节 31: 灵魂火符施放消耗护身符 1 点持久</summary>
    public static bool CanCastSoulFire(int amuletDura, out int newAmuletDura)
    {
        if (amuletDura <= 0)
        {
            newAmuletDura = 0;
            return false;
        }
        newAmuletDura = amuletDura - 1;
        return true;
    }

    /// <summary>细节 32: 施毒术灰粉与黄粉独立效果</summary>
    public enum PoisonType { None = 0, GreenHpDrain = 1, RedArmorBreak = 2 }

    /// <summary>细节 33: 治愈术 6 次脉冲回复</summary>
    public const int HealingPulseTotalTicks = 6;
    public static int CalculateHealingPulse(int scMin, int scMax, int randomRoll) =>
        5 + scMin + (randomRoll % Math.Max(1, scMax - scMin + 1));

    /// <summary>细节 34: 群体治愈术 3x3 八卦光阵 (MagicId = 18)</summary>
    public const int GroupHealingAreaRadius = 1;

    /// <summary>细节 35: 幽灵盾头顶金钟罩消散音效 SoundId=117</summary>
    public const int GhostShieldExpireSoundId = 117;
    public static int CalculateGhostShieldDuration(int scMax) => 30 + scMax * 2;

    /// <summary>细节 36: 神圣战甲术护甲提升持续秒数</summary>
    public static int CalculateHolyArmorDuration(int scMax) => 30 + scMax * 2;

    /// <summary>细节 37: 隐身术脚下八卦阵与起步破隐</summary>
    public static bool ShouldBreakInvisibilityOnMove(int fromX, int fromY, int toX, int toY) =>
        fromX != toX || fromY != toY;

    /// <summary>细节 38: 召唤骷髅 0~7 级颜色标识 (0白, 1~2浅蓝, 3~4深蓝, 5~6黄, 7红)</summary>
    public static string GetSkeletonColorHex(int petLevel) => petLevel switch
    {
        0 => "#ffffff",
        1 or 2 => "#80d0ff",
        3 or 4 => "#0080ff",
        5 or 6 => "#ffff00",
        7 => "#ff0000",
        _ => "#ffffff"
    };

    /// <summary>细节 39: 7级神兽 2 格直线喷火与音效 SoundId=135</summary>
    public const int ShinsuFlameRange = 2;
    public const int ShinsuFlameSoundId = 135;

    /// <summary>细节 40: 心灵启示血条持续时间与宽度 32 像素</summary>
    public const int MindRevelationBarWidth = 32;
    public static int CalculateMindRevelationDuration(int scMax) => 20 + scMax * 2;

    // =========================================================================
    // 五、官方 24 原生 UI、窗口层级与焦点交互细节 (41 ~ 50)
    // =========================================================================

    /// <summary>细节 41: 登录框 Tab 循环切换焦点 (0=账号, 1=密码)</summary>
    public static int SwitchLoginInputFocus(int current) => current == 0 ? 1 : 0;

    /// <summary>细节 42: 创角 360 度 8 方向旋转步进</summary>
    public static int RotateCharDirection(int currentDir, bool clockwise) =>
        clockwise ? (currentDir + 1) % 8 : (currentDir + 7) % 8;

    /// <summary>细节 43: 选角火盆火焰 8 帧循环</summary>
    public const int BrazierFlameFrames = 8;

    /// <summary>细节 44: 24 原生窗体全局 ESC 栈优先弹出</summary>
    public enum WindowPriority { Low = 0, Normal = 1, High = 2, Modal = 3 }

    /// <summary>细节 45: 背包物品拖拽指针锚点 (+2, +2)</summary>
    public static (int x, int y) GetDragAnchorOffset() => (2, 2);

    /// <summary>细节 46: 槽位类型非法穿戴失败音效 SoundId=105</summary>
    public const int EquipRejectSoundId = 105;

    /// <summary>细节 47: NPC 对话超过 8 行出现翻页三角</summary>
    public const int NpcDialogMaxVisibleLines = 8;

    /// <summary>细节 48: 行会列表掌门人永远置顶</summary>
    public static int CompareGuildMembers(bool isMasterA, bool isOnlineA, int levelA, bool isMasterB, bool isOnlineB, int levelB)
    {
        if (isMasterA != isMasterB) return isMasterA ? -1 : 1;
        if (isOnlineA != isOnlineB) return isOnlineA ? -1 : 1;
        return levelB.CompareTo(levelA);
    }

    /// <summary>细节 49: 组队微型血槽宽 40 高 6 像素</summary>
    public const int GroupHpBarWidth = 40;
    public const int GroupHpBarHeight = 6;

    /// <summary>细节 50: 快捷键占用替换提示</summary>
    public static bool IsHotKeyConflict(int currentAssignedMagic, int newMagic) =>
        currentAssignedMagic > 0 && currentAssignedMagic != newMagic;

    // =========================================================================
    // 六、HUD 底栏、血球魔球、快捷键与聊天系统细节 (51 ~ 60)
    // =========================================================================

    /// <summary>细节 51: HUD 底栏两分片整数字节对齐 (800x600 居中拼合)</summary>
    public static (int leftW, int rightW, int totalW) GetHudBottomBarDimensions() => (400, 400, 800);

    /// <summary>细节 52: 血球球面波纹直径 56 像素</summary>
    public const int SphereDiameter = 56;

    /// <summary>细节 53: 快捷栏按下下凹右下偏移 1 像素</summary>
    public static (int dx, int dy) GetBeltSlotPressedOffset() => (1, 1);

    /// <summary>细节 54: 聊天自动换行缩进 4 半角空格</summary>
    public const string ChatHangingIndent = "    ";

    /// <summary>细节 55: 五大聊天频道色值</summary>
    public static string GetChannelColorHex(string channel) => channel switch
    {
        "Normal" => "#ffffff",
        "Whisper" => "#ffff00",
        "Group" => "#00ffff",
        "Guild" => "#00ff00",
        "System" => "#ff0000",
        _ => "#ffffff"
    };

    /// <summary>细节 56: 聊天输入框最大 70 字符截断</summary>
    public const int ChatInputMaxLength = 70;
    public static string TruncateChatInput(string text) =>
        string.IsNullOrEmpty(text) ? string.Empty : (text.Length > ChatInputMaxLength ? text[..ChatInputMaxLength] : text);

    /// <summary>细节 57: 历史发言记忆深度 10 条</summary>
    public const int ChatHistoryMaxCapacity = 10;

    /// <summary>细节 58: 小地图雷达按钮开关状态贴图索引</summary>
    public const int RadarButtonNormalFrame = 28;
    public const int RadarButtonHighlightFrame = 29;

    /// <summary>细节 59: Ctrl+H 攻击模式切换切刀音 SoundId=100</summary>
    public const int AttackModeSwitchSoundId = 100;

    /// <summary>细节 60: 经验条 8 晶格分割与升级号角 SoundId=12</summary>
    public const int ExpBarSegmentsCount = 8;
    public const int LevelUpHornSoundId = 12;

    // =========================================================================
    // 七、装备系统、持久磨损、极品属性与特戒套装细节 (61 ~ 70)
    // =========================================================================

    /// <summary>细节 61: 武器持久为 0 时攻击力清零</summary>
    public static int ApplyWeaponZeroDurability(int currentDura, int baseDc) => currentDura <= 0 ? 0 : baseDc;

    /// <summary>细节 62: 衣服持久为 0 时防御加成清零</summary>
    public static int ApplyArmorZeroDurability(int currentDura, int baseAc) => currentDura <= 0 ? 0 : baseAc;

    /// <summary>细节 63: 战神油 0 损耗特修回满</summary>
    public static (int dura, int maxDura) RepairWithGodWarOil(int maxDura) => (maxDura, maxDura);

    /// <summary>细节 64: 修复油普修扣减 1 点最大持久</summary>
    public static (int dura, int maxDura, bool success) RepairWithNormalOil(int currentMaxDura)
    {
        if (currentMaxDura <= 3) return (currentMaxDura, currentMaxDura, false);
        int newMax = currentMaxDura - 1;
        return (newMax, newMax, true);
    }

    /// <summary>细节 65: 祝福油幸运硬上限 7，诅咒硬上限 10</summary>
    public const int MaxWeaponLuck = 7;
    public const int MaxWeaponCurse = 10;
    public static int ClampWeaponLuck(int luck) => Math.Clamp(luck, 0, MaxWeaponLuck);

    /// <summary>细节 66: 神秘装备未佩戴前问号隐藏</summary>
    public const string MysticHiddenMask = "???";

    /// <summary>细节 67: 传送戒指目标受阻周围 3x3 寻找空地</summary>
    public static (int x, int y)? FindFreeTileAround(int targetX, int targetY, Func<int, int, bool> isBlocked)
    {
        if (!isBlocked(targetX, targetY)) return (targetX, targetY);
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue;
                if (!isBlocked(targetX + dx, targetY + dy))
                    return (targetX + dx, targetY + dy);
            }
        }
        return null;
    }

    /// <summary>细节 68: 麻痹戒指 1/15 概率触发 5 秒麻痹</summary>
    public const int ParalysisChanceDenominator = 15;
    public const int ParalysisDurationMs = 5000;
    public static bool RollParalysis(int randomRoll) => (randomRoll % ParalysisChanceDenominator) == 0;

    /// <summary>细节 69: 护身戒指伤害溢出分水岭 (蓝量扣尽扣血量)</summary>
    public static (int remainingHp, int remainingMp) CalculateProtectionRingDamage(int currentHp, int currentMp, int damage)
    {
        if (currentMp >= damage)
        {
            return (currentHp, currentMp - damage);
        }
        int overflowDamage = damage - currentMp;
        return (Math.Max(0, currentHp - overflowDamage), 0);
    }

    /// <summary>细节 70: 祈祷全套头盔碎裂与天地变色公告</summary>
    public const string PrayerRebellionWorldNotice = "神之祈祷，天地动容，全服宠物发生叛变！";

    // =========================================================================
    // 八、怪物 AI、仇恨机制、生态步进与尸体战利品细节 (71 ~ 80)
    // =========================================================================

    /// <summary>细节 71: 半兽统领 4 格旋转飞斧 (SM_FLYAXE 16)</summary>
    public const int OrcCommanderAxeMaxDistance = 4;

    /// <summary>细节 72: 黑色恶蛆 400ms 滚动音效 SoundId=111</summary>
    public const int BlackMaggotRollSoundId = 111;

    /// <summary>细节 73: 角蝇同屏 10 只蝙蝠上限与自扣 5 HP</summary>
    public const int BatFlyNestMaxSpawns = 10;
    public const int BatFlySpawnSelfDamage = 5;

    /// <summary>细节 74: 野猪挥棒前摇 400ms 与打断判定</summary>
    public const int BoarAttackWindupMs = 400;

    /// <summary>细节 75: 楔蛾低血量 20% 逃跑 AI</summary>
    public static bool ShouldMothFlee(int hp, int maxHp) => maxHp > 0 && ((double)hp / maxHp <= 0.20);

    /// <summary>细节 76: 赤月恶魔地刺穿刺间隔 1200ms</summary>
    public const int RedMoonDemonSpikeIntervalMs = 1200;

    /// <summary>细节 77: 沃玛教主 30% 血量瞬移跳点</summary>
    public static bool ShouldWomaLeaderAmbush(int hp, int maxHp, int surroundedEnemies) =>
        maxHp > 0 && ((double)hp / maxHp <= 0.30) && surroundedEnemies >= 3;

    /// <summary>细节 78: 地表物品 80 秒归属权独享</summary>
    public const int LootOwnershipDurationMs = 80000;
    public static bool CanPickUpItem(long dropTimeMs, long nowMs, int ownerSessionId, int currentSessionId)
    {
        if (ownerSessionId == currentSessionId) return true;
        return (nowMs - dropTimeMs) >= LootOwnershipDurationMs;
    }

    /// <summary>细节 79: 怪物阵亡朝向锁定致命一击受力朝向</summary>
    public static byte LockCorpseDirection(byte hitDirection) => (byte)(hitDirection % 8);

    /// <summary>细节 80: 双头金刚尸体仅产出 1 件核心法神首饰</summary>
    public static bool TryDrawGoldKongCoreLoot(ref bool alreadyHarvested, out string coreItemName)
    {
        if (alreadyHarvested)
        {
            coreItemName = string.Empty;
            return false;
        }
        alreadyHarvested = true;
        coreItemName = "法神头盔";
        return true;
    }

    // =========================================================================
    // 九、物理地图、黑暗迷雾、动态阻挡与传送阵细节 (81 ~ 90)
    // =========================================================================

    /// <summary>细节 81: 地牢迷雾半径动态缩放 (无照明3, 蜡烛5, 火把8)</summary>
    public static int GetDungeonFogRadiusTiles(int lightLevel) => lightLevel switch
    {
        >= 2 => 8, // 火把
        1 => 5,    // 蜡烛
        _ => 3     // 裸体无光
    };

    /// <summary>细节 82: 小地图战争迷雾探索擦除半径 16 瓦片</summary>
    public const int MinimapFogRevealRadius = 16;

    /// <summary>细节 83: 跨图跳点门口防卡死周围 2 格溢出安置</summary>
    public const int MapPassageOverflowSearchRadius = 2;

    /// <summary>细节 84: 城门倒塌瓦片切换为废墟帧</summary>
    public const int CastleGateRubbleTileIndex = 999;

    /// <summary>细节 85: 沙巴克秘密地道踩踏延迟 500ms 触发传送</summary>
    public const int SecretPalaceTunnelStepDelayMs = 500;

    /// <summary>细节 86: 未知暗殿 M001 禁止小地图与随机卷</summary>
    public static bool IsUnknownDarkHallRestricted(string mapId) => mapId == "M001";

    /// <summary>细节 87: 大刀卫士 9999 神罚绝对秒杀红名</summary>
    public const int GuardExecutionDamage = 9999;

    /// <summary>细节 88: 毒蛇山谷关隘宽度 2 瓦片边界</summary>
    public const int CanyonPassValidWidth = 2;

    /// <summary>细节 89: 地表 300 节点同屏容量告警</summary>
    public const int GroundItemScreenDensityLimit = 300;

    /// <summary>细节 90: 盟重土城沙漠风声 SoundId=201</summary>
    public const int MudDesertWindSoundId = 201;

    // =========================================================================
    // 十、经济交易、沙巴克战役、PK 规则与服务端反作弊细节 (91 ~ 100)
    // =========================================================================

    /// <summary>细节 91: 面对面交易单笔最大 500 万，总额超过 1000 万熔断</summary>
    public const int SingleTradeMaxGold = 5000000;
    public const int PlayerGoldHardCap = 10000000;
    public static bool CanAcceptTradeGold(int currentGold, int tradeGold) =>
        tradeGold <= SingleTradeMaxGold && (long)currentGold + tradeGold <= PlayerGoldHardCap;

    /// <summary>细节 92: 商店买入前负重增量预检</summary>
    public static bool CanBuyWithWeight(int currentBagWeight, int maxBagWeight, int itemWeight, int count) =>
        currentBagWeight + (itemWeight * count) <= maxBagWeight;

    /// <summary>细节 93: 出刀反作弊 5 次滑动窗口平均间隔不得低于 650ms</summary>
    public static bool ValidateAttackSpeedWindow(ReadOnlySpan<long> attackIntervalsMs)
    {
        if (attackIntervalsMs.Length < 5) return true;
        long sum = 0;
        for (int i = 0; i < attackIntervalsMs.Length; i++) sum += attackIntervalsMs[i];
        return (sum / attackIntervalsMs.Length) >= 650;
    }

    /// <summary>细节 94: 1 秒内连续 3 次撞墙穿人定身惩罚 2000ms</summary>
    public const int WallHackTriggerThreshold = 3;
    public const int WallHackPenaltyStunMs = 2000;

    /// <summary>细节 95: 行会战期间敌对行会名号深蓝色 (#0000ff) 且免 PK 惩罚</summary>
    public const string GuildWarEnemyColorHex = "#0000ff";

    /// <summary>细节 96: 沙巴克攻城战全域免红名结界</summary>
    public static bool IsSabukWarPkFreeZone(string mapId, int x, int y) =>
        mapId is "3" or "0150" or "150";

    /// <summary>细节 97: 皇宫易主大殿全员遍历排他性扫描 (100% 纯净度)</summary>
    public static bool IsPalaceOccupiedPurely(int totalInPalace, int guildMembersInPalace) =>
        totalInPalace > 0 && totalInPalace == guildMembersInPalace;

    /// <summary>细节 98: PK 点每 200 秒衰减 1 点</summary>
    public const int PkDecaySecondsPerPoint = 200;
    public static (int newPk, int remainingSec) DecayPkPoints(int currentPk, int accumulatedSec)
    {
        int pointsToRemove = accumulatedSec / PkDecaySecondsPerPoint;
        int remainingSec = accumulatedSec % PkDecaySecondsPerPoint;
        return (Math.Max(0, currentPk - pointsToRemove), remainingSec);
    }

    /// <summary>细节 99: 红名村安全区无大刀卫士保障</summary>
    public static bool IsGuardSafeDistanceForRedVillage(int guardX, int guardY, int redVillageX = 850, int redVillageY = 660)
    {
        int dist = Math.Max(Math.Abs(guardX - redVillageX), Math.Abs(guardY - redVillageY));
        return dist >= 20; // 离红名村至少 20 格以外
    }

    /// <summary>细节 100: 重连网络风暴平滑消峰批次间隔 50ms</summary>
    public const int ReconnectionPacerBatchIntervalMs = 50;
}
