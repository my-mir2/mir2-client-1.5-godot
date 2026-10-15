// ============================================================================
// 盛大官方 1.4/1.5 原版 Monster.DB 185 种全量怪物生态物理真值静态大字典
// 依据欧洲官方/盛大官方 mud2/DB/Monster.DB 二进制真值导出
// ============================================================================
using System;
using System.Collections.Generic;

namespace Mir2.Shared.Data;

public record MonsterDBEntry(string Name, int Race, int RaceImg, int Appr, int Level, int HP);

public static class OfficialMonsterDBRegistry
{
    public static readonly Dictionary<string, MonsterDBEntry> MonstersByName = new(StringComparer.OrdinalIgnoreCase);

    static OfficialMonsterDBRegistry()
    {
        Register(new MonsterDBEntry("Hen", 51, 11, 160, 5, 5));
        Register(new MonsterDBEntry("Deer", 52, 11, 161, 12, 15));
        Register(new MonsterDBEntry("Deer1", 97, 11, 161, 50, 100));
        Register(new MonsterDBEntry("Oma", 83, 19, 100, 15, 25));
        Register(new MonsterDBEntry("Oma0", 83, 19, 100, 15, 25));
        Register(new MonsterDBEntry("OmaFighter", 81, 19, 101, 22, 90));
        Register(new MonsterDBEntry("OmaFighter0", 81, 19, 101, 22, 90));
        Register(new MonsterDBEntry("OmaWarrior", 81, 19, 102, 28, 300));
        Register(new MonsterDBEntry("OmaWarrior1", 81, 19, 102, 50, 100));
        Register(new MonsterDBEntry("OmaWarrior2", 81, 19, 102, 50, 1));
        Register(new MonsterDBEntry("OmaWarrior10", 81, 19, 102, 50, 300));
        Register(new MonsterDBEntry("SpittingSpider", 82, 19, 163, 16, 42));
        Register(new MonsterDBEntry("Guard", 11, 12, 0, 99, 1));
        Register(new MonsterDBEntry("Yob", 83, 19, 162, 12, 15));
        Register(new MonsterDBEntry("Yob0", 83, 19, 162, 12, 15));
        Register(new MonsterDBEntry("Scorpion", 84, 32, 83, 18, 45));
        Register(new MonsterDBEntry("CaveBat", 81, 19, 80, 20, 25));
        Register(new MonsterDBEntry("CaveBat0", 81, 19, 80, 20, 25));
        Register(new MonsterDBEntry("ForestYeti", 83, 10, 1, 16, 30));
        Register(new MonsterDBEntry("ForestYeti0", 83, 10, 1, 16, 30));
        Register(new MonsterDBEntry("CannibalPlant", 85, 13, 10, 20, 28));
        Register(new MonsterDBEntry("Skeleton", 86, 14, 20, 18, 85));
        Register(new MonsterDBEntry("Skeleton0", 86, 14, 20, 18, 85));
        Register(new MonsterDBEntry("AxeSkeleton", 87, 15, 21, 18, 90));
        Register(new MonsterDBEntry("AxeSkeleton0", 87, 15, 21, 18, 90));
        Register(new MonsterDBEntry("AxeSkeleton9", 87, 15, 21, 18, 90));
        Register(new MonsterDBEntry("BoneFighter", 88, 14, 22, 19, 95));
        Register(new MonsterDBEntry("BoneFighter0", 88, 14, 22, 19, 95));
        Register(new MonsterDBEntry("BoneWarrior", 89, 14, 23, 20, 100));
        Register(new MonsterDBEntry("BoneWarrior0", 89, 14, 23, 20, 100));
        Register(new MonsterDBEntry("BoneElite", 89, 14, 150, 40, 600));
        Register(new MonsterDBEntry("BoneElite1", 89, 14, 150, 50, 100));
        Register(new MonsterDBEntry("BoneElite2", 89, 14, 150, 50, 1));
        Register(new MonsterDBEntry("BoneElite10", 89, 14, 150, 40, 600));
        Register(new MonsterDBEntry("CaveMaggot", 90, 16, 24, 21, 60));
        Register(new MonsterDBEntry("HookingCat", 81, 17, 25, 13, 17));
        Register(new MonsterDBEntry("RakingCat", 81, 17, 26, 13, 18));
        Register(new MonsterDBEntry("RakingCat0", 81, 17, 26, 13, 18));
        Register(new MonsterDBEntry("Scarecrow", 83, 18, 27, 10, 12));
        Register(new MonsterDBEntry("Scarecrow0", 83, 18, 27, 10, 12));
        Register(new MonsterDBEntry("Scarecrow1", 83, 18, 27, 50, 100));
        Register(new MonsterDBEntry("WoomaSoldier", 97, 19, 30, 30, 260));
        Register(new MonsterDBEntry("WoomaSoldier0", 97, 19, 30, 30, 260));
        Register(new MonsterDBEntry("WoomaFighter", 97, 19, 32, 30, 280));
        Register(new MonsterDBEntry("WoomaFighter0", 97, 19, 32, 30, 280));
        Register(new MonsterDBEntry("WoomaWarrior", 97, 19, 33, 30, 280));
        Register(new MonsterDBEntry("WoomaWarrior0", 97, 19, 33, 30, 280));
        Register(new MonsterDBEntry("FlamingWooma", 91, 20, 31, 31, 290));
        Register(new MonsterDBEntry("FlamingWooma0", 91, 20, 31, 31, 290));
        Register(new MonsterDBEntry("FlamingWooma2", 91, 20, 31, 50, 290));
        Register(new MonsterDBEntry("WoomaGuardian", 97, 19, 151, 50, 1200));
        Register(new MonsterDBEntry("WoomaGuardian1", 97, 19, 151, 50, 100));
        Register(new MonsterDBEntry("WoomaGuardian2", 97, 19, 151, 50, 1));
        Register(new MonsterDBEntry("WoomaGuardian0", 97, 19, 151, 50, 1200));
        Register(new MonsterDBEntry("Woomataurus", 92, 21, 34, 60, 2500));
        Register(new MonsterDBEntry("Woomataurus1", 92, 21, 34, 60, 100));
        Register(new MonsterDBEntry("Woomataurus9", 92, 21, 34, 60, 2500));
        Register(new MonsterDBEntry("Dark", 93, 22, 28, 26, 200));
        Register(new MonsterDBEntry("Dark0", 93, 22, 28, 26, 200));
        Register(new MonsterDBEntry("Dung", 106, 53, 29, 26, 180));
        Register(new MonsterDBEntry("Dung0", 106, 53, 29, 26, 180));
        Register(new MonsterDBEntry("Zombie1", 94, 40, 40, 25, 160));
        Register(new MonsterDBEntry("Zombie10", 94, 40, 40, 25, 160));
        Register(new MonsterDBEntry("Zombie2", 95, 41, 50, 25, 160));
        Register(new MonsterDBEntry("Zombie20", 95, 41, 50, 25, 160));
        Register(new MonsterDBEntry("Zombie3", 96, 42, 51, 25, 160));
        Register(new MonsterDBEntry("Zombie30", 96, 42, 51, 25, 160));
        Register(new MonsterDBEntry("Zombie4", 96, 42, 52, 25, 160));
        Register(new MonsterDBEntry("Zombie40", 96, 42, 52, 25, 160));
        Register(new MonsterDBEntry("Zombie5", 96, 42, 53, 25, 160));
        Register(new MonsterDBEntry("Zombie50", 96, 42, 53, 25, 160));
        Register(new MonsterDBEntry("Ghoul2", 81, 19, 152, 50, 800));
        Register(new MonsterDBEntry("Ghoul3", 81, 19, 152, 40, 800));
        Register(new MonsterDBEntry("RedSnake", 81, 19, 36, 17, 50));
        Register(new MonsterDBEntry("RedSnake0", 81, 19, 36, 17, 50));
        Register(new MonsterDBEntry("Trainer", 55, 19, 72, 99, 1));
        Register(new MonsterDBEntry("BoneFamm", 100, 23, 37, 15, 1));
        Register(new MonsterDBEntry("TigerSnake", 81, 19, 38, 18, 53));
        Register(new MonsterDBEntry("TigerSnake0", 81, 19, 38, 18, 53));
        Register(new MonsterDBEntry("Sheep", 52, 19, 43, 13, 20));
        Register(new MonsterDBEntry("SkyStinger", 81, 19, 44, 16, 38));
        Register(new MonsterDBEntry("SkyStinger0", 81, 19, 44, 16, 38));
        Register(new MonsterDBEntry("ShellNipper", 81, 19, 45, 16, 37));
        Register(new MonsterDBEntry("ShellNipper0", 81, 19, 45, 16, 37));
        Register(new MonsterDBEntry("SandWorm", 82, 19, 48, 16, 42));
        Register(new MonsterDBEntry("VisceralWorm", 82, 19, 49, 17, 54));
        Register(new MonsterDBEntry("Keratoid", 81, 19, 90, 16, 32));
        Register(new MonsterDBEntry("Keratoid0", 81, 19, 90, 16, 32));
        Register(new MonsterDBEntry("GiantKeratoid", 81, 19, 91, 27, 200));
        Register(new MonsterDBEntry("GiantKeratoid1", 81, 19, 91, 50, 100));
        Register(new MonsterDBEntry("GiantKeratoid2", 81, 19, 91, 50, 1));
        Register(new MonsterDBEntry("GiantKeratoid3", 81, 19, 91, 27, 200));
        Register(new MonsterDBEntry("Wolf", 53, 19, 70, 16, 35));
        Register(new MonsterDBEntry("Centipede", 81, 19, 73, 26, 230));
        Register(new MonsterDBEntry("Centipede0", 81, 19, 73, 26, 230));
        Register(new MonsterDBEntry("EvilCentipede", 107, 33, 140, 60, 2500));
        Register(new MonsterDBEntry("BlackMaggot", 81, 19, 74, 28, 180));
        Register(new MonsterDBEntry("BlackMaggot0", 81, 19, 74, 28, 180));
        Register(new MonsterDBEntry("Tongs", 81, 19, 120, 31, 250));
        Register(new MonsterDBEntry("Tongs0", 81, 19, 120, 31, 250));
        Register(new MonsterDBEntry("EvilTongs", 81, 19, 121, 60, 1400));
        Register(new MonsterDBEntry("EvilTongs1", 81, 19, 121, 60, 100));
        Register(new MonsterDBEntry("EvilTongs2", 81, 19, 121, 60, 1400));
        Register(new MonsterDBEntry("EvilTongs9", 81, 19, 121, 60, 1400));
        Register(new MonsterDBEntry("EvilTongs10", 81, 19, 121, 60, 1400));
        Register(new MonsterDBEntry("WhimperingBee", 81, 19, 81, 26, 210));
        Register(new MonsterDBEntry("GiantWorm", 81, 19, 82, 26, 230));
        Register(new MonsterDBEntry("GiantWorm0", 81, 19, 82, 26, 230));
        Register(new MonsterDBEntry("BugBat", 81, 31, 42, 5, 5));
        Register(new MonsterDBEntry("WedgeMoth", 105, 52, 39, 32, 350));
        Register(new MonsterDBEntry("RedEvilBoar", 81, 19, 110, 32, 320));
        Register(new MonsterDBEntry("RedEvilBoar0", 81, 19, 110, 32, 320));
        Register(new MonsterDBEntry("RedEvilBoar3", 81, 19, 110, 40, 480));
        Register(new MonsterDBEntry("BlackEvilBoar", 81, 19, 111, 35, 380));
        Register(new MonsterDBEntry("BlackEvilBoar0", 81, 19, 111, 35, 380));
        Register(new MonsterDBEntry("BlackEvilBoar3", 81, 19, 111, 40, 570));
        Register(new MonsterDBEntry("WhiteEvilBoar", 81, 19, 112, 50, 1600));
        Register(new MonsterDBEntry("WhiteEvilBoar0", 81, 19, 112, 50, 1600));
        Register(new MonsterDBEntry("WhiteEvilBoar1", 81, 19, 112, 50, 100));
        Register(new MonsterDBEntry("WhiteEvilBoar2", 81, 19, 112, 50, 1600));
        Register(new MonsterDBEntry("SnakeScorpion", 81, 19, 130, 35, 360));
        Register(new MonsterDBEntry("SnakeScorpion0", 81, 19, 130, 35, 360));
        Register(new MonsterDBEntry("SnakeScorpion3", 81, 19, 130, 40, 540));
        Register(new MonsterDBEntry("EvilSnake", 81, 19, 164, 50, 1800));
        Register(new MonsterDBEntry("EvilSnake1", 81, 19, 164, 50, 100));
        Register(new MonsterDBEntry("BigRat", 81, 19, 46, 24, 330));
        Register(new MonsterDBEntry("BigRat0", 81, 19, 46, 24, 330));
        Register(new MonsterDBEntry("ZumaArcher", 104, 45, 47, 40, 400));
        Register(new MonsterDBEntry("ZumaArcher0", 104, 45, 47, 40, 400));
        Register(new MonsterDBEntry("ZumaArcher3", 104, 45, 47, 40, 600));
        Register(new MonsterDBEntry("ZumaStatue", 101, 47, 61, 42, 550));
        Register(new MonsterDBEntry("ZumaStatue0", 101, 47, 61, 42, 550));
        Register(new MonsterDBEntry("ZumaStatue3", 101, 47, 61, 42, 810));
        Register(new MonsterDBEntry("ZumaGuardian", 101, 47, 62, 43, 580));
        Register(new MonsterDBEntry("ZumaGuardian0", 101, 47, 62, 43, 580));
        Register(new MonsterDBEntry("ZumaGuardian3", 101, 47, 62, 46, 870));
        Register(new MonsterDBEntry("ZumaGuardian00", 101, 47, 62, 50, 2000));
        Register(new MonsterDBEntry("ZumaGuardian10", 101, 47, 62, 43, 2000));
        Register(new MonsterDBEntry("Zumataurus", 102, 49, 63, 60, 3000));
        Register(new MonsterDBEntry("Zumataurus9", 102, 49, 63, 60, 3000));
        Register(new MonsterDBEntry("Archer", 112, 45, 71, 99, 1));
        Register(new MonsterDBEntry("Arch", 104, 45, 71, 99, 1));
        Register(new MonsterDBEntry("Trainer10", 81, 19, 72, 99, 1));
        Register(new MonsterDBEntry("LeftWall", 111, 98, 901, 99, 1));
        Register(new MonsterDBEntry("CenterWall", 111, 98, 902, 99, 1));
        Register(new MonsterDBEntry("RightWall", 111, 98, 903, 99, 1));
        Register(new MonsterDBEntry("ArcherGuard", 112, 45, 71, 99, 1));
        Register(new MonsterDBEntry("Dogz", 113, 54, 170, 32, 1));
        Register(new MonsterDBEntry("Dogz1", 114, 55, 171, 32, 1));
        Register(new MonsterDBEntry("SpidBat", 105, 37, 113, 52, 390));
        Register(new MonsterDBEntry("GangSpider", 81, 19, 114, 53, 410));
        Register(new MonsterDBEntry("GangSpider0", 81, 19, 114, 63, 410));
        Register(new MonsterDBEntry("GangSpider3", 81, 19, 114, 73, 615));
        Register(new MonsterDBEntry("VenomSpider", 118, 19, 119, 40, 380));
        Register(new MonsterDBEntry("VenomSpider0", 118, 19, 119, 50, 380));
        Register(new MonsterDBEntry("VenomSpider3", 118, 19, 119, 60, 570));
        Register(new MonsterDBEntry("BigSpider", 81, 19, 118, 53, 430));
        Register(new MonsterDBEntry("BigSpider0", 81, 19, 118, 63, 430));
        Register(new MonsterDBEntry("BigSpider3", 81, 19, 118, 73, 645));
        Register(new MonsterDBEntry("RootSpider", 116, 35, 132, 53, 350));
        Register(new MonsterDBEntry("LureSpider", 119, 19, 117, 54, 440));
        Register(new MonsterDBEntry("LureSpider0", 119, 19, 117, 64, 440));
        Register(new MonsterDBEntry("LureSpider3", 119, 19, 117, 74, 660));
        Register(new MonsterDBEntry("Larva", 117, 36, 133, 52, 7));
        Register(new MonsterDBEntry("BigApe", 81, 19, 115, 55, 800));
        Register(new MonsterDBEntry("BigApe0", 81, 19, 115, 65, 800));
        Register(new MonsterDBEntry("BigApe3", 81, 19, 115, 75, 1200));
        Register(new MonsterDBEntry("EvilApe", 81, 19, 116, 56, 1500));
        Register(new MonsterDBEntry("EvilApe0", 81, 19, 116, 56, 1500));
        Register(new MonsterDBEntry("RedEvilApe", 81, 19, 92, 55, 750));
        Register(new MonsterDBEntry("GreyEvilApe", 81, 19, 93, 55, 750));
        Register(new MonsterDBEntry("RedMoonEvil", 115, 34, 131, 60, 2800));
        Register(new MonsterDBEntry("RedMoonEvil1", 115, 34, 131, 60, 1));
        Register(new MonsterDBEntry("Guard1", 11, 24, 2, 99, 1));
        Register(new MonsterDBEntry("FootBall", 120, 9, 3, 1, 1));
        Register(new MonsterDBEntry("Deer10", 52, 11, 161, 12, 15));
        Register(new MonsterDBEntry("Deer20", 52, 11, 161, 12, 15));
        Register(new MonsterDBEntry("Deer30", 52, 11, 161, 12, 15));
        Register(new MonsterDBEntry("MirKing", 81, 49, 63, 60, 6000));
        Register(new MonsterDBEntry("BichonLord", 92, 21, 150, 99, 6000));
        Register(new MonsterDBEntry("SerpentLord", 92, 21, 164, 99, 6000));
        Register(new MonsterDBEntry("WoomyonLord", 92, 21, 151, 99, 6000));
        Register(new MonsterDBEntry("MongchonLord", 92, 21, 121, 99, 6000));
        Register(new MonsterDBEntry("Raider1", 81, 12, 0, 99, 1));
        Register(new MonsterDBEntry("Raider2", 104, 45, 71, 99, 1));

        RegisterAlias("半兽人", "Oma");
        RegisterAlias("半兽战士", "OmaFighter");
        RegisterAlias("半兽勇士", "OmaWarrior");
        RegisterAlias("半兽战将", "OmaWarrior");
        RegisterAlias("大刀卫士", "Guard");
        RegisterAlias("带刀护卫", "Guard");
        RegisterAlias("森林雪人", "ForestYeti");
        RegisterAlias("雪人", "ForestYeti");
        RegisterAlias("食人花", "CannibalPlant");
        RegisterAlias("骷髅", "Skeleton");
        RegisterAlias("骷髅战士", "AxeSkeleton");
        RegisterAlias("骷髅战将", "BoneFighter");
        RegisterAlias("骷髅精灵", "BoneElite");
        RegisterAlias("洞蛆", "CaveMaggot");
        RegisterAlias("多钩猫", "HookingCat");
        RegisterAlias("钉耙猫", "RakingCat");
        RegisterAlias("稻草人", "Scarecrow");
        RegisterAlias("毒蜘蛛", "SpittingSpider");
        RegisterAlias("红蛇", "RedSnake");
        RegisterAlias("虎蛇", "TigerSnake");
        RegisterAlias("楔蛾", "WedgeMoth");
        RegisterAlias("黑色恶蛆", "BlackMaggot");
        RegisterAlias("蜈蚣", "Centipede");
        RegisterAlias("红野猪", "RedBoar");
        RegisterAlias("黑野猪", "BlackBoar");
        RegisterAlias("白野猪", "WhiteBoar");
        RegisterAlias("尸王", "ZombieKing");
        RegisterAlias("祖玛卫士", "ZumaGuardian");
        RegisterAlias("祖玛雕像", "ZumaStatue");
        RegisterAlias("祖玛弓箭手", "ZumaArcher");
        RegisterAlias("祖玛教主", "ZumaTaurus");
        RegisterAlias("沃玛教主", "WoomaTaurus");
        RegisterAlias("沃玛卫士", "WoomaWarrior");
        RegisterAlias("触龙神", "EvilCentipede");
        RegisterAlias("神兽", "Dogz");
        RegisterAlias("变异骷髅", "BoneElite");
        RegisterAlias("鸡", "Hen");
        RegisterAlias("鹿", "Deer");
        RegisterAlias("大刀侍卫", "Guard");
        RegisterAlias("带刀侍卫", "Guard");
        RegisterAlias("大刀卫士", "Guard");
        RegisterAlias("弓箭守卫", "ArcherGuard");
        RegisterAlias("弓箭卫士", "ArcherGuard");
    }
    private static void RegisterAlias(string aliasName, string canonicalName)
    {
        if (MonstersByName.TryGetValue(canonicalName, out var entry))
        {
            MonstersByName[aliasName] = entry;
        }
    }


    private static void Register(MonsterDBEntry m)
    {
        if (!MonstersByName.ContainsKey(m.Name))
            MonstersByName[m.Name] = m;
    }

    public static int ResolveAppr(string monsterName, int defaultAppr = 26)
    {
        if (string.IsNullOrEmpty(monsterName)) return defaultAppr;
        if (MonstersByName.TryGetValue(monsterName, out var entry)) return entry.Appr;
        return defaultAppr;
    }
}
