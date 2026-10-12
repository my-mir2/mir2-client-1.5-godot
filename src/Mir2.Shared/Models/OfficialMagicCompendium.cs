using System;
using System.Collections.Generic;

namespace Mir2.Shared.Models;

/// <summary>
/// 盛大官方 1.5 战法道全量 33 门技能官方视听、时序与动作权威全集 (Official 1.5 Magic Compendium)
/// 严格 1:1 对齐官方二进制 Magic ID 1~33，覆盖动作帧、WIL 贴图索引、音效与伤害判定时序。
/// </summary>
public static class OfficialMagicCompendium
{
    public enum MagicJob
    {
        Warrior = 0,
        Wizard = 1,
        Taoist = 2
    }

    public enum MagicCastType
    {
        Passive = 0,         // 被动常驻或几率触发 (基础剑法、精神力战法、攻杀)
        AttackModifier = 1,  // 挥刀攻击强化 (刺杀、半月、烈火)
        SelfCast = 2,        // 自身施法 (魔法盾、抗拒火环、瞬息移动)
        TargetDirect = 3,    // 锁定单体直击 (雷电术、圣言术、心灵启示)
        TargetProjectile = 4,// 弹道飞行轨迹 (火球术、大火球、灵魂火符)
        GroundAoE = 5,       // 地面范围铺设 (火墙)
        SelfAoE = 6,         // 自身周围范围 (地狱雷光)
        DirectionalBeam = 7, // 方向直线穿透 (地狱火、疾光电影)
        TargetArea = 8,      // 目标点范围轰炸 (爆裂火焰、冰咆哮、群疗、群隐、困魔咒)
        Summon = 9,          // 召唤法阵 (召唤骷髅、召唤神兽)
        ChargeDash = 10      // 位移冲撞 (野蛮冲撞)
    }

    public sealed class MagicDef
    {
        public int MagicId { get; init; }
        public string Name { get; init; } = string.Empty;
        public MagicJob Job { get; init; }
        public MagicCastType CastType { get; init; }
        public int SpellWil { get; init; } = 1; // 1: Magic.wil, 2: Magic2.wil
        public int EffectStartFrame { get; init; }
        public int EffectFrameCount { get; init; }
        public int EffectFrameDelayMs { get; init; } = 80;
        public int SoundCast { get; init; }
        public int SoundFly { get; init; }
        public int SoundHit { get; init; }
        public float ProjectileSpeed { get; init; } // 像素/秒，直击类为 0
        public int GroundDurationMs { get; init; }  // 地面残留毫秒 (如火墙)
        public int PreCastDelayMs { get; init; } = 250;
        public int TotalCooldownMs { get; init; } = 1000;
        public int MpCostBase { get; init; }
        public string Desc { get; init; } = string.Empty;
    }

    private static readonly Dictionary<int, MagicDef> _magics = new();
    private static readonly Dictionary<string, MagicDef> _magicsByName = new(StringComparer.OrdinalIgnoreCase);
    private static readonly Dictionary<int, int> _aliasToOfficial = new();

    static OfficialMagicCompendium()
    {
        // -----------------------------------------------------------------------------------------------------
        // 官方 1.5 物理真值技能库 (Magic ID 1~33，100% 对齐官方 IDA sub_4BDCFC 与 M2Server 规则引擎)
        // -----------------------------------------------------------------------------------------------------

        // 1. 火球术 (法师初级弹道，Magic.wil 0..5)
        Register(new MagicDef
        {
            MagicId = 1, Name = "火球术", Job = MagicJob.Wizard, CastType = MagicCastType.TargetProjectile,
            SpellWil = 1, EffectStartFrame = 0, EffectFrameCount = 6, EffectFrameDelayMs = 70,
            SoundCast = 10, SoundFly = 11, SoundHit = 12, ProjectileSpeed = 550f, MpCostBase = 3,
            Desc = "初级火系弹道法术"
        });

        // 2. 治愈术 (道士单体治愈，Magic.wil 20..29)
        Register(new MagicDef
        {
            MagicId = 2, Name = "治愈术", Job = MagicJob.Taoist, CastType = MagicCastType.TargetDirect,
            SpellWil = 1, EffectStartFrame = 20, EffectFrameCount = 10, EffectFrameDelayMs = 90,
            SoundCast = 13, SoundHit = 14, MpCostBase = 3,
            Desc = "神圣绿光上升持续恢复生命值"
        });

        // 3. 基本剑术 (战士被动)
        Register(new MagicDef
        {
            MagicId = 3, Name = "基本剑术", Job = MagicJob.Warrior, CastType = MagicCastType.Passive,
            Desc = "战士被动提升命中精度与物理命中率"
        });

        // 4. 精神力战法 (道士被动)
        Register(new MagicDef
        {
            MagicId = 4, Name = "精神力战法", Job = MagicJob.Taoist, CastType = MagicCastType.Passive,
            Desc = "道士被动增加物理准确与挥刀命中率"
        });

        // 5. 大火球 (法师进阶弹道，Magic.wil 40..45)
        Register(new MagicDef
        {
            MagicId = 5, Name = "大火球", Job = MagicJob.Wizard, CastType = MagicCastType.TargetProjectile,
            SpellWil = 1, EffectStartFrame = 40, EffectFrameCount = 6, EffectFrameDelayMs = 70,
            SoundCast = 15, SoundFly = 16, SoundHit = 17, ProjectileSpeed = 600f, MpCostBase = 7,
            Desc = "强力火球弹道，爆破威能提升"
        });

        // 6. 施毒术 (道士减防损血，Magic.wil 60..67)
        Register(new MagicDef
        {
            MagicId = 6, Name = "施毒术", Job = MagicJob.Taoist, CastType = MagicCastType.TargetDirect,
            SpellWil = 1, EffectStartFrame = 60, EffectFrameCount = 8, EffectFrameDelayMs = 80,
            SoundCast = 18, SoundHit = 19, MpCostBase = 4,
            Desc = "灰色/黄色药粉触发红绿毒持续降防与扣血"
        });

        // 7. 攻杀剑术 (战士破坏重击)
        Register(new MagicDef
        {
            MagicId = 7, Name = "攻杀剑术", Job = MagicJob.Warrior, CastType = MagicCastType.Passive,
            SoundHit = 1, Desc = "几率触发破坏性重击，刀光转金黄与金属爆鸣"
        });

        // 8. 抗拒火环 (法师自身推挤，Magic.wil 280..285)
        Register(new MagicDef
        {
            MagicId = 8, Name = "抗拒火环", Job = MagicJob.Wizard, CastType = MagicCastType.SelfCast,
            SpellWil = 1, EffectStartFrame = 280, EffectFrameCount = 6, EffectFrameDelayMs = 60,
            SoundCast = 41, SoundHit = 42, MpCostBase = 5,
            Desc = "环状烈火外推四周比自身等级低的敌人"
        });

        // 9. 地狱火 (法师正前直线 5 格穿透，Magic.wil 260..265)
        Register(new MagicDef
        {
            MagicId = 9, Name = "地狱火", Job = MagicJob.Wizard, CastType = MagicCastType.DirectionalBeam,
            SpellWil = 1, EffectStartFrame = 260, EffectFrameCount = 6, EffectFrameDelayMs = 70,
            SoundCast = 39, SoundHit = 40, MpCostBase = 8,
            Desc = "前方直线 5 格向前推进的烈火长蛇"
        });

        // 10. 雷电术 (法师天雷直击，Magic.wil 240..243)
        Register(new MagicDef
        {
            MagicId = 10, Name = "雷电术", Job = MagicJob.Wizard, CastType = MagicCastType.TargetDirect,
            SpellWil = 1, EffectStartFrame = 240, EffectFrameCount = 4, EffectFrameDelayMs = 60,
            SoundCast = 37, SoundHit = 38, MpCostBase = 9,
            Desc = "引九天狂雷自天灵盖直线劈落目标"
        });

        // 11. 瞬息移动 (法师随机传送，Magic.wil 160..167)
        Register(new MagicDef
        {
            MagicId = 11, Name = "瞬息移动", Job = MagicJob.Wizard, CastType = MagicCastType.SelfCast,
            SpellWil = 1, EffectStartFrame = 160, EffectFrameCount = 8, EffectFrameDelayMs = 80,
            SoundCast = 29, SoundHit = 30, MpCostBase = 10,
            Desc = "虚空折叠随机传送至本地图任意安全坐标"
        });

        // 12. 刺杀剑术 (战士隔位剑气)
        Register(new MagicDef
        {
            MagicId = 12, Name = "刺杀剑术", Job = MagicJob.Warrior, CastType = MagicCastType.AttackModifier,
            SoundCast = 2, SoundHit = 3, Desc = "隔位亮蓝剑气无视防御与魔法盾"
        });

        // 13. 灵魂火符 (道士火符弹道，Magic.wil 80..85)
        Register(new MagicDef
        {
            MagicId = 13, Name = "灵魂火符", Job = MagicJob.Taoist, CastType = MagicCastType.TargetProjectile,
            SpellWil = 1, EffectStartFrame = 80, EffectFrameCount = 6, EffectFrameDelayMs = 60,
            SoundCast = 20, SoundFly = 21, SoundHit = 22, ProjectileSpeed = 650f, MpCostBase = 4,
            Desc = "消耗护身符激射飞舞旋转火符弹道"
        });

        // 14. 幽灵盾 (道士魔防光环，Magic.wil 100..109)
        Register(new MagicDef
        {
            MagicId = 14, Name = "幽灵盾", Job = MagicJob.Taoist, CastType = MagicCastType.TargetArea,
            SpellWil = 1, EffectStartFrame = 100, EffectFrameCount = 10, EffectFrameDelayMs = 90,
            SoundCast = 23, SoundHit = 24, MpCostBase = 6,
            Desc = "太极八卦升空强化范围队友魔法防御力 (MAC)"
        });

        // 15. 神圣战甲术 (道士物防光环，Magic.wil 120..129)
        Register(new MagicDef
        {
            MagicId = 15, Name = "神圣战甲术", Job = MagicJob.Taoist, CastType = MagicCastType.TargetArea,
            SpellWil = 1, EffectStartFrame = 120, EffectFrameCount = 10, EffectFrameDelayMs = 90,
            SoundCast = 25, SoundHit = 26, MpCostBase = 6,
            Desc = "金黄战甲护体强化范围队友物理防御力 (AC)"
        });

        // 16. 困魔咒 (道士金光禁锢，Magic.wil 340..347)
        Register(new MagicDef
        {
            MagicId = 16, Name = "困魔咒", Job = MagicJob.Taoist, CastType = MagicCastType.TargetArea,
            SpellWil = 1, EffectStartFrame = 340, EffectFrameCount = 8, EffectFrameDelayMs = 90,
            SoundCast = 47, SoundHit = 48, MpCostBase = 12, GroundDurationMs = 15000,
            Desc = "金黄八卦光柱锁死目标区域怪物行动"
        });

        // 17. 召唤骷髅 (道士亡灵召唤，Magic.wil 360..367)
        Register(new MagicDef
        {
            MagicId = 17, Name = "召唤骷髅", Job = MagicJob.Taoist, CastType = MagicCastType.Summon,
            SpellWil = 1, EffectStartFrame = 360, EffectFrameCount = 8, EffectFrameDelayMs = 80,
            SoundCast = 49, SoundHit = 50, MpCostBase = 12,
            Desc = "从冥界召唤提斧骷髅为自己而战"
        });

        // 18. 隐身术 (道士自身匿踪，Magic.wil 300..305)
        Register(new MagicDef
        {
            MagicId = 18, Name = "隐身术", Job = MagicJob.Taoist, CastType = MagicCastType.SelfCast,
            SpellWil = 1, EffectStartFrame = 300, EffectFrameCount = 6, EffectFrameDelayMs = 80,
            SoundCast = 43, SoundHit = 44, MpCostBase = 5,
            Desc = "遁入阴阳使普通怪物的视线失去聚焦"
        });

        // 19. 集体隐身术 (道士范围匿踪，Magic.wil 320..327)
        Register(new MagicDef
        {
            MagicId = 19, Name = "集体隐身术", Job = MagicJob.Taoist, CastType = MagicCastType.TargetArea,
            SpellWil = 1, EffectStartFrame = 320, EffectFrameCount = 8, EffectFrameDelayMs = 80,
            SoundCast = 45, SoundHit = 46, MpCostBase = 10,
            Desc = "消耗护身符在指定范围隐蔽全队"
        });

        // 20. 诱惑之光 (法师定身驯兽，Magic.wil 140..145)
        Register(new MagicDef
        {
            MagicId = 20, Name = "诱惑之光", Job = MagicJob.Wizard, CastType = MagicCastType.TargetDirect,
            SpellWil = 1, EffectStartFrame = 140, EffectFrameCount = 6, EffectFrameDelayMs = 70,
            SoundCast = 27, SoundHit = 28, MpCostBase = 8,
            Desc = "强光照射怪物大脑使其麻痹或驯服为宠"
        });

        // 21. 地狱雷光 (法师八角范围雷暴，Magic.wil 200..207)
        Register(new MagicDef
        {
            MagicId = 21, Name = "地狱雷光", Job = MagicJob.Wizard, CastType = MagicCastType.SelfAoE,
            SpellWil = 1, EffectStartFrame = 200, EffectFrameCount = 8, EffectFrameDelayMs = 60,
            SoundCast = 33, SoundHit = 34, MpCostBase = 25,
            Desc = "自身周围八角八方爆发雷光，对不死生物奇效"
        });

        // 22. 火墙 (法师地面火毯，Magic.wil 180..182)
        Register(new MagicDef
        {
            MagicId = 22, Name = "火墙", Job = MagicJob.Wizard, CastType = MagicCastType.GroundAoE,
            SpellWil = 1, EffectStartFrame = 180, EffectFrameCount = 3, EffectFrameDelayMs = 120,
            SoundCast = 31, SoundHit = 32, GroundDurationMs = 20000, MpCostBase = 15,
            Desc = "地表铺设 3 帧烈火地毯对踏足实体持续灼烧"
        });

        // 23. 疾光电影 (法师直线 8 格雷束，Magic.wil 220..225)
        Register(new MagicDef
        {
            MagicId = 23, Name = "疾光电影", Job = MagicJob.Wizard, CastType = MagicCastType.DirectionalBeam,
            SpellWil = 1, EffectStartFrame = 220, EffectFrameCount = 6, EffectFrameDelayMs = 50,
            SoundCast = 35, SoundHit = 36, MpCostBase = 20,
            Desc = "正前方向 8 格贯穿雷电强光巨束"
        });

        // 24. 圣言术 (法师秒杀亡灵，Magic.wil 440..447)
        Register(new MagicDef
        {
            MagicId = 24, Name = "圣言术", Job = MagicJob.Wizard, CastType = MagicCastType.TargetDirect,
            SpellWil = 1, EffectStartFrame = 440, EffectFrameCount = 8, EffectFrameDelayMs = 70,
            SoundCast = 59, SoundHit = 60, MpCostBase = 30,
            Desc = "九天圣光根据等级差概率直接超度不死生物"
        });

        // 25. 半月弯刀 (战士扇面剑气)
        Register(new MagicDef
        {
            MagicId = 25, Name = "半月弯刀", Job = MagicJob.Warrior, CastType = MagicCastType.AttackModifier,
            SoundCast = 4, SoundHit = 5, Desc = "扇面 4 格金蓝弧形刀光剑气，攻击前方与两侧敌人"
        });

        // 26. 烈火剑法 (战士终极烈火)
        Register(new MagicDef
        {
            MagicId = 26, Name = "烈火剑法", Job = MagicJob.Warrior, CastType = MagicCastType.AttackModifier,
            SoundCast = 57, SoundHit = 58, TotalCooldownMs = 8000, MpCostBase = 7,
            Desc = "剑刃凝练烈火精灵劈出毁天灭地的毁灭双烈火"
        });

        // 27. 野蛮冲撞 (战士雄躯推开)
        Register(new MagicDef
        {
            MagicId = 27, Name = "野蛮冲撞", Job = MagicJob.Warrior, CastType = MagicCastType.ChargeDash,
            SoundCast = 6, SoundHit = 7, TotalCooldownMs = 3000,
            Desc = "以雄躯推开前方低等级目标并造成 1 秒硬直"
        });

        // 28. 心灵启示 (道士洞察生命，Magic.wil 380..385)
        Register(new MagicDef
        {
            MagicId = 28, Name = "心灵启示", Job = MagicJob.Taoist, CastType = MagicCastType.TargetDirect,
            SpellWil = 1, EffectStartFrame = 380, EffectFrameCount = 6, EffectFrameDelayMs = 80,
            SoundCast = 51, SoundHit = 52, MpCostBase = 6,
            Desc = "洞察天地使怪物头顶显现实时血量数值"
        });

        // 29. 群体治愈术 (道士范围群疗，Magic.wil 500..509)
        Register(new MagicDef
        {
            MagicId = 29, Name = "群体治愈术", Job = MagicJob.Taoist, CastType = MagicCastType.TargetArea,
            SpellWil = 1, EffectStartFrame = 500, EffectFrameCount = 10, EffectFrameDelayMs = 80,
            SoundCast = 65, SoundHit = 66, MpCostBase = 20,
            Desc = "大范围甘霖普降恢复范围内所有友军生命"
        });

        // 30. 召唤神兽 (道士地脉麒麟，Magic.wil 480..489)
        Register(new MagicDef
        {
            MagicId = 30, Name = "召唤神兽", Job = MagicJob.Taoist, CastType = MagicCastType.Summon,
            SpellWil = 1, EffectStartFrame = 480, EffectFrameCount = 10, EffectFrameDelayMs = 80,
            SoundCast = 63, SoundHit = 64, MpCostBase = 25,
            Desc = "地表八卦阵破土召唤喷火麒麟神兽"
        });

        // 31. 魔法盾 (法师护盾减伤，Magic.wil 460..469)
        Register(new MagicDef
        {
            MagicId = 31, Name = "魔法盾", Job = MagicJob.Wizard, CastType = MagicCastType.SelfCast,
            SpellWil = 1, EffectStartFrame = 460, EffectFrameCount = 10, EffectFrameDelayMs = 80,
            SoundCast = 61, SoundHit = 62, MpCostBase = 20, GroundDurationMs = 30000,
            Desc = "金黄色椭圆半透明护盾吸收巨额物理与魔法伤害"
        });

        // 32. 冰咆哮 (法师暴风雪AOE，Magic.wil 420..429)
        Register(new MagicDef
        {
            MagicId = 32, Name = "冰咆哮", Job = MagicJob.Wizard, CastType = MagicCastType.TargetArea,
            SpellWil = 1, EffectStartFrame = 420, EffectFrameCount = 10, EffectFrameDelayMs = 60,
            SoundCast = 55, SoundHit = 56, MpCostBase = 26,
            Desc = "召唤暴风雪冰锥旋风无视魔法躲避与防魔"
        });

        // 33. 爆裂火焰 (法师3x3爆炸AOE，Magic.wil 400..407)
        Register(new MagicDef
        {
            MagicId = 33, Name = "爆裂火焰", Job = MagicJob.Wizard, CastType = MagicCastType.TargetArea,
            SpellWil = 1, EffectStartFrame = 400, EffectFrameCount = 8, EffectFrameDelayMs = 70,
            SoundCast = 53, SoundHit = 54, MpCostBase = 16,
            Desc = "指定 3x3 区域轰然引爆烈焰火海"
        });
    }

    private static void Register(MagicDef def)
    {
        _magics[def.MagicId] = def;
        _magicsByName[def.Name] = def;
    }

    public static MagicDef? Get(int magicId)
    {
        if (_magics.TryGetValue(magicId, out var def))
        {
            return def;
        }
        if (_aliasToOfficial.TryGetValue(magicId, out int officialId) && _magics.TryGetValue(officialId, out var aliasDef))
        {
            return aliasDef;
        }
        return null;
    }

    public static MagicDef? GetByName(string name) =>
        _magicsByName.GetValueOrDefault(name);

    public static IReadOnlyCollection<MagicDef> All => _magics.Values;

    public static int Count => _magics.Count;
}
