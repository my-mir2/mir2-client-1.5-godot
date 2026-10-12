// ============================================================================
// Mir2 客户端 - 32 门官方技能视听资源完整性容错校验树 (1:1 盛大 1.5 官方实证)
// (SkillResourceValidationTree)
// 针对《工程认知公理 4.0》中揭示的“边缘技能在客户端资源包缺失或格式解析异常时可能产生静默或闪退”坏账，
// 建立 1~33 官方全量技能的视听校验树与热插拔容错安全降级机制。
// ============================================================================

using System;
using System.Collections.Generic;
using Mir2.Shared.Models;

namespace Mir2Client.Render;

/// <summary>
/// 官方技能视听资源安全描述符
/// </summary>
public sealed class SkillResourceDescriptor
{
    public int MagicId { get; init; }
    public string Name { get; init; } = string.Empty;
    public int SpellWil { get; init; } = 1; // 1: Magic.wil, 2: Magic2.wil
    public int BaseEffectFrame { get; init; }
    public int FrameCount { get; init; }
    public int HitFrame { get; init; }
    public int HitFrameCount { get; init; }
    public int SoundCast { get; init; }
    public int SoundFly { get; init; }
    public int SoundHit { get; init; }
    public int FallbackMagicId { get; init; } = 1; // 默认安全回退技能: 1 (基础火球术)
}

/// <summary>
/// 技能视听校验结果
/// </summary>
public sealed class SkillValidationResult
{
    public bool IsValid { get; set; } = true;
    public bool HasTextureIssue { get; set; } = false;
    public bool HasAudioIssue { get; set; } = false;
    public string WarningMessage { get; set; } = string.Empty;
}

/// <summary>
/// 32 官方技能视听资源完整性容错校验树
/// </summary>
public static class SkillResourceValidationTree
{
    private static readonly Dictionary<int, SkillResourceDescriptor> _descriptors = new();

    static SkillResourceValidationTree()
    {
        // 注册官方 1.5 原版 1~33 全量技能视听映射台账
        Register(1, "火球术", 1, 0, 10, 170, 10, soundCast: 10, soundFly: 11, soundHit: 12, fallbackId: 1);
        Register(2, "治愈术", 1, 200, 10, 200, 10, soundCast: 13, soundFly: 0, soundHit: 14, fallbackId: 1);
        Register(3, "基本剑术", 1, 0, 0, 0, 0, soundCast: 0, soundFly: 0, soundHit: 0, fallbackId: 1);
        Register(4, "精神力战法", 1, 0, 0, 0, 0, soundCast: 0, soundFly: 0, soundHit: 0, fallbackId: 1);
        Register(5, "大火球", 1, 400, 12, 170, 10, soundCast: 10, soundFly: 11, soundHit: 12, fallbackId: 1);
        Register(6, "施毒术", 1, 600, 8, 600, 8, soundCast: 10060, soundFly: 0, soundHit: 10062, fallbackId: 1);
        Register(7, "攻杀剑术", 1, 0, 0, 0, 0, soundCast: 130, soundFly: 0, soundHit: 131, fallbackId: 1);
        Register(8, "抗拒火环", 1, 900, 10, 900, 10, soundCast: 10080, soundFly: 0, soundHit: 10082, fallbackId: 1);
        Register(9, "地狱火", 1, 400, 12, 170, 10, soundCast: 10090, soundFly: 0, soundHit: 10092, fallbackId: 1);
        Register(10, "雷电术", 1, 120, 10, 120, 10, soundCast: 10100, soundFly: 0, soundHit: 10102, fallbackId: 1);
        Register(11, "瞬息移动", 1, 1680, 10, 1680, 10, soundCast: 10110, soundFly: 0, soundHit: 10112, fallbackId: 1);
        Register(12, "刺杀剑术", 1, 70, 8, 70, 8, soundCast: 132, soundFly: 0, soundHit: 132, fallbackId: 1);
        Register(13, "灵魂火符", 1, 900, 8, 920, 6, soundCast: 10130, soundFly: 10131, soundHit: 10132, fallbackId: 1);
        Register(14, "幽灵盾", 1, 1560, 10, 1560, 10, soundCast: 10140, soundFly: 0, soundHit: 10142, fallbackId: 1);
        Register(15, "神圣战甲术", 1, 1590, 10, 1590, 10, soundCast: 10150, soundFly: 0, soundHit: 10152, fallbackId: 1);
        Register(16, "困魔咒", 1, 3990, 10, 3990, 10, soundCast: 10160, soundFly: 0, soundHit: 10162, fallbackId: 1);
        Register(17, "召唤骷髅", 1, 1500, 10, 1500, 10, soundCast: 10170, soundFly: 0, soundHit: 570, fallbackId: 1);
        Register(18, "隐身术", 1, 1520, 8, 1520, 8, soundCast: 10180, soundFly: 0, soundHit: 10182, fallbackId: 1);
        Register(19, "集体隐身术", 1, 1520, 8, 1520, 8, soundCast: 10190, soundFly: 0, soundHit: 10192, fallbackId: 1);
        Register(20, "诱惑之光", 1, 1620, 12, 1620, 12, soundCast: 10200, soundFly: 0, soundHit: 10202, fallbackId: 1);
        Register(21, "瞬息移动(法师)", 1, 940, 10, 940, 10, soundCast: 10210, soundFly: 0, soundHit: 10212, fallbackId: 1);
        Register(22, "火墙", 1, 400, 6, 400, 6, soundCast: 10220, soundFly: 0, soundHit: 10222, fallbackId: 1);
        Register(23, "疾光电影", 1, 400, 10, 410, 6, soundCast: 10230, soundFly: 0, soundHit: 10232, fallbackId: 1);
        Register(24, "地狱雷光", 1, 3920, 10, 3920, 10, soundCast: 10240, soundFly: 0, soundHit: 10242, fallbackId: 1);
        Register(25, "半月弯刀", 1, 3960, 10, 3960, 10, soundCast: 133, soundFly: 0, soundHit: 133, fallbackId: 1);
        Register(26, "烈火剑法", 1, 170, 10, 170, 10, soundCast: 136, soundFly: 0, soundHit: 137, fallbackId: 1);
        Register(27, "野蛮冲撞", 1, 0, 0, 0, 0, soundCast: 134, soundFly: 0, soundHit: 135, fallbackId: 1);
        Register(28, "心灵启示", 1, 1620, 12, 1620, 12, soundCast: 10280, soundFly: 0, soundHit: 10282, fallbackId: 1);
        Register(29, "群体治愈术", 1, 1790, 12, 1790, 12, soundCast: 10290, soundFly: 0, soundHit: 10292, fallbackId: 2);
        Register(30, "召唤神兽", 1, 1650, 12, 1650, 12, soundCast: 10300, soundFly: 0, soundHit: 1900, fallbackId: 1);
        Register(31, "魔法盾", 1, 3880, 8, 3880, 8, soundCast: 10310, soundFly: 0, soundHit: 10312, fallbackId: 1);
        Register(32, "圣言术", 1, 1540, 12, 1540, 12, soundCast: 10320, soundFly: 0, soundHit: 10322, fallbackId: 1);
        Register(33, "冰咆哮", 1, 1540, 12, 1540, 12, soundCast: 10330, soundFly: 0, soundHit: 10332, fallbackId: 1);
    }

    private static void Register(
        int magicId, string name, int spellWil, int baseFrame, int frameCount,
        int hitFrame, int hitCount, int soundCast, int soundFly, int soundHit, int fallbackId)
    {
        _descriptors[magicId] = new SkillResourceDescriptor
        {
            MagicId = magicId,
            Name = name,
            SpellWil = spellWil,
            BaseEffectFrame = baseFrame,
            FrameCount = frameCount,
            HitFrame = hitFrame,
            HitFrameCount = hitCount,
            SoundCast = soundCast,
            SoundFly = soundFly,
            SoundHit = soundHit,
            FallbackMagicId = fallbackId
        };
    }

    public static SkillResourceDescriptor? GetDescriptor(int magicId)
    {
        _descriptors.TryGetValue(magicId, out var desc);
        return desc;
    }

    public static int Count => _descriptors.Count;

    /// <summary>
    /// 全量静态扫描与完整性检测
    /// </summary>
    public static Dictionary<int, SkillValidationResult> ValidateAll(
        Func<int, int, bool> isWilFrameValid,
        Func<int, bool> isSfxValid)
    {
        var results = new Dictionary<int, SkillValidationResult>();

        foreach (var (id, desc) in _descriptors)
        {
            var res = new SkillValidationResult();

            // 检查贴图有效性 (若技能具备视效帧)
            if (desc.FrameCount > 0 && !isWilFrameValid(desc.SpellWil, desc.BaseEffectFrame))
            {
                res.IsValid = false;
                res.HasTextureIssue = true;
                res.WarningMessage += $"[贴图缺失] Wil={desc.SpellWil}, Frame={desc.BaseEffectFrame}; ";
            }

            // 检查音效有效性 (若技能具备音效)
            if (desc.SoundCast > 0 && !isSfxValid(desc.SoundCast))
            {
                res.IsValid = false;
                res.HasAudioIssue = true;
                res.WarningMessage += $"[音效缺失] SoundCast={desc.SoundCast}; ";
            }

            results[id] = res;
        }

        return results;
    }

    /// <summary>
    /// 安全解析技能视听资源，在缺失或异常时自动降级回退至通用火球术，杜绝渲染端抛错
    /// </summary>
    public static SkillResourceDescriptor ResolveSafeDescriptor(
        int magicId,
        Func<int, int, bool> isWilFrameValid,
        Func<int, bool> isSfxValid,
        out bool downgraded)
    {
        downgraded = false;
        if (!_descriptors.TryGetValue(magicId, out var desc))
        {
            // 未知技能 ID，强制回退至 1 号火球术
            downgraded = true;
            return _descriptors[1];
        }

        // 若被动技能 (无视效帧)，直接放行
        if (desc.FrameCount == 0 && desc.SoundCast == 0)
        {
            return desc;
        }

        // 校验关键贴图帧是否存在
        bool textureOk = desc.FrameCount <= 0 || isWilFrameValid(desc.SpellWil, desc.BaseEffectFrame);
        if (!textureOk)
        {
            // 贴图损坏或丢失，安全降级至 FallbackMagicId
            downgraded = true;
            int fallbackId = desc.FallbackMagicId > 0 && _descriptors.ContainsKey(desc.FallbackMagicId)
                ? desc.FallbackMagicId
                : 1;
            return _descriptors[fallbackId];
        }

        return desc;
    }
}
