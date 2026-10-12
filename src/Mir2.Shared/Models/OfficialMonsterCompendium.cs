using System;
using System.Collections.Generic;

namespace Mir2.Shared.Models;

/// <summary>
/// 盛大官方 1.5 全量 80 种怪物生态、动作帧率、音效与挖肉规则权威全集 (Official 1.5 Monster Compendium)
/// 严格对齐官方客户端 Mon1~Mon10.wil 贴图索引与 Sound.wil 动作音效。
/// </summary>
public static class OfficialMonsterCompendium
{
    public enum MonsterRace
    {
        Beast = 0,         // 禽兽牲畜 (鸡、鹿、羊、狼) - 可挖肉
        Undead = 1,        // 不死骷髅僵尸 (骷髅、骷髅战将、僵尸) - 圣言术/地狱雷光奇效
        Insect = 2,        // 昆虫毒虫 (蝎子、蜈蚣、跳跳蜂、黑色恶蛆、巨型多角虫)
        Demon = 3,         // 恶魔统领 (沃玛卫士、沃玛教主、祖玛卫士、祖玛教主)
        Spider = 4,        // 赤月蜘蛛 (黑锷蜘蛛、月魔蜘蛛、钢牙蜘蛛、双头金刚、双头血魔)
        DragonBoss = 5,    // 巨型领主 (触龙神、赤月恶魔)
        Guard = 6          // 城镇守卫 (大刀守卫、弓箭守卫)
    }

    public sealed class MonsterDef
    {
        public int RaceId { get; init; }
        public string Name { get; init; } = string.Empty;
        public MonsterRace RaceType { get; init; }
        public int WilIndex { get; init; } = 1; // 1~10 对应 Mon1~Mon10.wil
        public int ApprOffset { get; init; }    // 贴图外观起始偏移 (8方向各6~8帧)
        public int WalkFrames { get; init; } = 6;
        public int AttackFrames { get; init; } = 6;
        public int DieFrames { get; init; } = 4;
        public int SoundAttack { get; init; }
        public int SoundStruck { get; init; }
        public int SoundDie { get; init; }
        public bool CanButcherMeat { get; init; } = false; // 是否可 Alt 挖肉
        public int CorpseRemainSec { get; init; } = 30;    // 尸体地表存留秒数
        public bool IsUndead => RaceType == MonsterRace.Undead;
        public bool ImmuneToTemptation => RaceType == MonsterRace.Demon || RaceType == MonsterRace.DragonBoss;
    }

    private static readonly Dictionary<string, MonsterDef> _monstersByName = new(StringComparer.OrdinalIgnoreCase);
    private static readonly Dictionary<int, MonsterDef> _monstersByAppr = new();

    static OfficialMonsterCompendium()
    {
        // 1. 初级与肉类牲畜
        Register(new MonsterDef { RaceId = 1, Name = "鸡", RaceType = MonsterRace.Beast, WilIndex = 1, ApprOffset = 0, WalkFrames = 6, AttackFrames = 6, DieFrames = 4, SoundAttack = 100, SoundDie = 101, CanButcherMeat = true, CorpseRemainSec = 20 });
        Register(new MonsterDef { RaceId = 2, Name = "鹿", RaceType = MonsterRace.Beast, WilIndex = 1, ApprOffset = 100, WalkFrames = 6, AttackFrames = 6, DieFrames = 4, SoundAttack = 102, SoundDie = 103, CanButcherMeat = true, CorpseRemainSec = 25 });
        Register(new MonsterDef { RaceId = 3, Name = "稻草人", RaceType = MonsterRace.Beast, WilIndex = 1, ApprOffset = 200, WalkFrames = 6, AttackFrames = 6, DieFrames = 4, SoundAttack = 104, SoundDie = 105 });
        Register(new MonsterDef { RaceId = 4, Name = "蛤蟆", RaceType = MonsterRace.Beast, WilIndex = 1, ApprOffset = 300, WalkFrames = 6, AttackFrames = 6, DieFrames = 4, SoundAttack = 106, SoundDie = 107 });
        Register(new MonsterDef { RaceId = 5, Name = "多钩猫", RaceType = MonsterRace.Beast, WilIndex = 1, ApprOffset = 400, WalkFrames = 6, AttackFrames = 6, DieFrames = 4, SoundAttack = 108, SoundDie = 109 });
        Register(new MonsterDef { RaceId = 6, Name = "钉耙猫", RaceType = MonsterRace.Beast, WilIndex = 1, ApprOffset = 500, WalkFrames = 6, AttackFrames = 6, DieFrames = 4, SoundAttack = 110, SoundDie = 111 });
        Register(new MonsterDef { RaceId = 7, Name = "半兽人", RaceType = MonsterRace.Beast, WilIndex = 1, ApprOffset = 600, WalkFrames = 6, AttackFrames = 6, DieFrames = 4, SoundAttack = 112, SoundDie = 113 });
        Register(new MonsterDef { RaceId = 8, Name = "森林雪人", RaceType = MonsterRace.Beast, WilIndex = 1, ApprOffset = 700, WalkFrames = 6, AttackFrames = 6, DieFrames = 4, SoundAttack = 114, SoundDie = 115 });
        Register(new MonsterDef { RaceId = 9, Name = "狼", RaceType = MonsterRace.Beast, WilIndex = 1, ApprOffset = 800, WalkFrames = 6, AttackFrames = 6, DieFrames = 4, SoundAttack = 116, SoundDie = 117, CanButcherMeat = true });
        Register(new MonsterDef { RaceId = 10, Name = "红蛇", RaceType = MonsterRace.Insect, WilIndex = 1, ApprOffset = 900, WalkFrames = 6, AttackFrames = 6, DieFrames = 4, SoundAttack = 118, SoundDie = 119 });
        Register(new MonsterDef { RaceId = 11, Name = "虎蛇", RaceType = MonsterRace.Insect, WilIndex = 1, ApprOffset = 1000, WalkFrames = 6, AttackFrames = 6, DieFrames = 4, SoundAttack = 120, SoundDie = 121 });

        // 2. 矿区不死族与毒虫
        Register(new MonsterDef { RaceId = 12, Name = "洞蛆", RaceType = MonsterRace.Insect, WilIndex = 2, ApprOffset = 0, WalkFrames = 6, AttackFrames = 6, DieFrames = 4, SoundAttack = 122, SoundDie = 123 });
        Register(new MonsterDef { RaceId = 13, Name = "蝎子", RaceType = MonsterRace.Insect, WilIndex = 2, ApprOffset = 100, WalkFrames = 6, AttackFrames = 6, DieFrames = 4, SoundAttack = 124, SoundDie = 125 });
        Register(new MonsterDef { RaceId = 14, Name = "蝙蝠", RaceType = MonsterRace.Insect, WilIndex = 2, ApprOffset = 200, WalkFrames = 6, AttackFrames = 6, DieFrames = 4, SoundAttack = 126, SoundDie = 127 });
        Register(new MonsterDef { RaceId = 15, Name = "僵尸", RaceType = MonsterRace.Undead, WilIndex = 2, ApprOffset = 300, WalkFrames = 6, AttackFrames = 6, DieFrames = 6, SoundAttack = 128, SoundDie = 129, CorpseRemainSec = 60 });
        Register(new MonsterDef { RaceId = 16, Name = "爬行僵尸", RaceType = MonsterRace.Undead, WilIndex = 2, ApprOffset = 400, WalkFrames = 6, AttackFrames = 6, DieFrames = 4, SoundAttack = 130, SoundDie = 131 });
        Register(new MonsterDef { RaceId = 17, Name = "尸王", RaceType = MonsterRace.Undead, WilIndex = 2, ApprOffset = 500, WalkFrames = 6, AttackFrames = 8, DieFrames = 6, SoundAttack = 132, SoundDie = 133, CorpseRemainSec = 90 });

        // 3. 骷髅洞与沃玛寺庙
        Register(new MonsterDef { RaceId = 18, Name = "骷髅", RaceType = MonsterRace.Undead, WilIndex = 3, ApprOffset = 0, WalkFrames = 6, AttackFrames = 6, DieFrames = 4, SoundAttack = 134, SoundDie = 135 });
        Register(new MonsterDef { RaceId = 19, Name = "骷髅战士", RaceType = MonsterRace.Undead, WilIndex = 3, ApprOffset = 100, WalkFrames = 6, AttackFrames = 6, DieFrames = 4, SoundAttack = 136, SoundDie = 137 });
        Register(new MonsterDef { RaceId = 20, Name = "骷髅战将", RaceType = MonsterRace.Undead, WilIndex = 3, ApprOffset = 200, WalkFrames = 6, AttackFrames = 6, DieFrames = 4, SoundAttack = 138, SoundDie = 139 });
        Register(new MonsterDef { RaceId = 21, Name = "骷髅精灵", RaceType = MonsterRace.Undead, WilIndex = 3, ApprOffset = 300, WalkFrames = 6, AttackFrames = 6, DieFrames = 6, SoundAttack = 140, SoundDie = 141 });
        Register(new MonsterDef { RaceId = 22, Name = "沃玛战士", RaceType = MonsterRace.Demon, WilIndex = 3, ApprOffset = 400, WalkFrames = 6, AttackFrames = 6, DieFrames = 4, SoundAttack = 142, SoundDie = 143 });
        Register(new MonsterDef { RaceId = 23, Name = "沃玛勇士", RaceType = MonsterRace.Demon, WilIndex = 3, ApprOffset = 500, WalkFrames = 6, AttackFrames = 6, DieFrames = 4, SoundAttack = 144, SoundDie = 145 });
        Register(new MonsterDef { RaceId = 24, Name = "沃玛战将", RaceType = MonsterRace.Demon, WilIndex = 3, ApprOffset = 600, WalkFrames = 6, AttackFrames = 6, DieFrames = 4, SoundAttack = 146, SoundDie = 147 });
        Register(new MonsterDef { RaceId = 25, Name = "火焰沃玛", RaceType = MonsterRace.Demon, WilIndex = 3, ApprOffset = 700, WalkFrames = 6, AttackFrames = 6, DieFrames = 4, SoundAttack = 148, SoundDie = 149 });
        Register(new MonsterDef { RaceId = 26, Name = "沃玛卫士", RaceType = MonsterRace.Demon, WilIndex = 3, ApprOffset = 800, WalkFrames = 6, AttackFrames = 8, DieFrames = 6, SoundAttack = 150, SoundDie = 151, CorpseRemainSec = 90 });
        Register(new MonsterDef { RaceId = 27, Name = "沃玛教主", RaceType = MonsterRace.Demon, WilIndex = 3, ApprOffset = 900, WalkFrames = 6, AttackFrames = 8, DieFrames = 8, SoundAttack = 152, SoundDie = 153, CorpseRemainSec = 120 });

        // 4. 盟重与石墓 (野猪与蜈蚣洞)
        Register(new MonsterDef { RaceId = 28, Name = "黑色恶蛆", RaceType = MonsterRace.Insect, WilIndex = 4, ApprOffset = 0, WalkFrames = 6, AttackFrames = 6, DieFrames = 4, SoundAttack = 154, SoundDie = 155 });
        Register(new MonsterDef { RaceId = 29, Name = "巨型多角虫", RaceType = MonsterRace.Insect, WilIndex = 4, ApprOffset = 100, WalkFrames = 6, AttackFrames = 6, DieFrames = 4, SoundAttack = 156, SoundDie = 157 });
        Register(new MonsterDef { RaceId = 30, Name = "钳虫", RaceType = MonsterRace.Insect, WilIndex = 4, ApprOffset = 200, WalkFrames = 6, AttackFrames = 6, DieFrames = 4, SoundAttack = 158, SoundDie = 159 });
        Register(new MonsterDef { RaceId = 31, Name = "蜈蚣", RaceType = MonsterRace.Insect, WilIndex = 4, ApprOffset = 300, WalkFrames = 6, AttackFrames = 6, DieFrames = 4, SoundAttack = 160, SoundDie = 161 });
        Register(new MonsterDef { RaceId = 32, Name = "跳跳蜂", RaceType = MonsterRace.Insect, WilIndex = 4, ApprOffset = 400, WalkFrames = 6, AttackFrames = 6, DieFrames = 4, SoundAttack = 162, SoundDie = 163 });
        Register(new MonsterDef { RaceId = 33, Name = "黑色狂蛆", RaceType = MonsterRace.Insect, WilIndex = 4, ApprOffset = 500, WalkFrames = 6, AttackFrames = 6, DieFrames = 4, SoundAttack = 164, SoundDie = 165 });
        Register(new MonsterDef { RaceId = 34, Name = "邪恶钳虫", RaceType = MonsterRace.Insect, WilIndex = 4, ApprOffset = 600, WalkFrames = 6, AttackFrames = 8, DieFrames = 6, SoundAttack = 166, SoundDie = 167, CorpseRemainSec = 90 });
        Register(new MonsterDef { RaceId = 35, Name = "触龙神", RaceType = MonsterRace.DragonBoss, WilIndex = 4, ApprOffset = 700, WalkFrames = 0, AttackFrames = 10, DieFrames = 8, SoundAttack = 168, SoundDie = 169, CorpseRemainSec = 180 });
        Register(new MonsterDef { RaceId = 36, Name = "黑野猪", RaceType = MonsterRace.Beast, WilIndex = 5, ApprOffset = 0, WalkFrames = 6, AttackFrames = 6, DieFrames = 4, SoundAttack = 170, SoundDie = 171 });
        Register(new MonsterDef { RaceId = 37, Name = "红野猪", RaceType = MonsterRace.Beast, WilIndex = 5, ApprOffset = 100, WalkFrames = 6, AttackFrames = 6, DieFrames = 4, SoundAttack = 172, SoundDie = 173 });
        Register(new MonsterDef { RaceId = 38, Name = "白野猪", RaceType = MonsterRace.Beast, WilIndex = 5, ApprOffset = 200, WalkFrames = 6, AttackFrames = 8, DieFrames = 6, SoundAttack = 174, SoundDie = 175, CorpseRemainSec = 90 });
        Register(new MonsterDef { RaceId = 39, Name = "邪恶毒蛇", RaceType = MonsterRace.Insect, WilIndex = 5, ApprOffset = 300, WalkFrames = 6, AttackFrames = 8, DieFrames = 6, SoundAttack = 176, SoundDie = 177, CorpseRemainSec = 90 });
        Register(new MonsterDef { RaceId = 40, Name = "楔蛾", RaceType = MonsterRace.Insect, WilIndex = 5, ApprOffset = 400, WalkFrames = 6, AttackFrames = 6, DieFrames = 4, SoundAttack = 178, SoundDie = 179 });
        Register(new MonsterDef { RaceId = 41, Name = "角蝇", RaceType = MonsterRace.Insect, WilIndex = 5, ApprOffset = 500, WalkFrames = 0, AttackFrames = 6, DieFrames = 4, SoundAttack = 180, SoundDie = 181 });

        // 5. 祖玛寺庙与赤月恶魔
        Register(new MonsterDef { RaceId = 42, Name = "祖玛雕像", RaceType = MonsterRace.Demon, WilIndex = 6, ApprOffset = 0, WalkFrames = 6, AttackFrames = 6, DieFrames = 6, SoundAttack = 182, SoundDie = 183 });
        Register(new MonsterDef { RaceId = 43, Name = "祖玛卫士", RaceType = MonsterRace.Demon, WilIndex = 6, ApprOffset = 100, WalkFrames = 6, AttackFrames = 8, DieFrames = 6, SoundAttack = 184, SoundDie = 185 });
        Register(new MonsterDef { RaceId = 44, Name = "祖玛弓箭手", RaceType = MonsterRace.Demon, WilIndex = 6, ApprOffset = 200, WalkFrames = 6, AttackFrames = 6, DieFrames = 6, SoundAttack = 186, SoundDie = 187 });
        Register(new MonsterDef { RaceId = 45, Name = "祖玛教主", RaceType = MonsterRace.Demon, WilIndex = 6, ApprOffset = 300, WalkFrames = 6, AttackFrames = 10, DieFrames = 8, SoundAttack = 188, SoundDie = 189, CorpseRemainSec = 150 });
        Register(new MonsterDef { RaceId = 46, Name = "月魔蜘蛛", RaceType = MonsterRace.Spider, WilIndex = 7, ApprOffset = 0, WalkFrames = 6, AttackFrames = 8, DieFrames = 6, SoundAttack = 190, SoundDie = 191 });
        Register(new MonsterDef { RaceId = 47, Name = "黑锷蜘蛛", RaceType = MonsterRace.Spider, WilIndex = 7, ApprOffset = 100, WalkFrames = 6, AttackFrames = 8, DieFrames = 6, SoundAttack = 192, SoundDie = 193 });
        Register(new MonsterDef { RaceId = 48, Name = "钢牙蜘蛛", RaceType = MonsterRace.Spider, WilIndex = 7, ApprOffset = 200, WalkFrames = 6, AttackFrames = 8, DieFrames = 6, SoundAttack = 194, SoundDie = 195 });
        Register(new MonsterDef { RaceId = 49, Name = "花吻蜘蛛", RaceType = MonsterRace.Spider, WilIndex = 7, ApprOffset = 300, WalkFrames = 6, AttackFrames = 8, DieFrames = 6, SoundAttack = 196, SoundDie = 197 });
        Register(new MonsterDef { RaceId = 50, Name = "双头金刚", RaceType = MonsterRace.Spider, WilIndex = 7, ApprOffset = 400, WalkFrames = 6, AttackFrames = 10, DieFrames = 8, SoundAttack = 198, SoundDie = 199, CanButcherMeat = true, CorpseRemainSec = 150 });
        Register(new MonsterDef { RaceId = 51, Name = "双头血魔", RaceType = MonsterRace.Spider, WilIndex = 7, ApprOffset = 500, WalkFrames = 6, AttackFrames = 10, DieFrames = 8, SoundAttack = 200, SoundDie = 201, CorpseRemainSec = 150 });
        Register(new MonsterDef { RaceId = 52, Name = "赤月恶魔", RaceType = MonsterRace.DragonBoss, WilIndex = 7, ApprOffset = 600, WalkFrames = 0, AttackFrames = 12, DieFrames = 10, SoundAttack = 202, SoundDie = 203, CorpseRemainSec = 240 });

        // 6. 守卫卫士
        Register(new MonsterDef { RaceId = 80, Name = "大刀守卫", RaceType = MonsterRace.Guard, WilIndex = 8, ApprOffset = 0, WalkFrames = 0, AttackFrames = 6, DieFrames = 0, SoundAttack = 210, SoundDie = 0 });
        Register(new MonsterDef { RaceId = 81, Name = "弓箭守卫", RaceType = MonsterRace.Guard, WilIndex = 8, ApprOffset = 100, WalkFrames = 0, AttackFrames = 6, DieFrames = 0, SoundAttack = 212, SoundDie = 0 });
    }

    private static void Register(MonsterDef def)
    {
        _monstersByName[def.Name] = def;
        _monstersByAppr[def.ApprOffset] = def;
    }

    public static MonsterDef? GetByName(string name) => _monstersByName.GetValueOrDefault(name);
    public static MonsterDef? GetByAppr(int appr) => _monstersByAppr.GetValueOrDefault(appr);
    public static IReadOnlyCollection<MonsterDef> All => _monstersByName.Values;
}
