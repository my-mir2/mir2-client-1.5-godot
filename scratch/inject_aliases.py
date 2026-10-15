aliases_mon = {
    '半兽人': 'Oma',
    '半兽战士': 'OmaFighter',
    '半兽勇士': 'OmaWarrior',
    '半兽战将': 'OmaWarrior',
    '大刀卫士': 'Guard',
    '带刀护卫': 'Guard',
    '森林雪人': 'ForestYeti',
    '雪人': 'ForestYeti',
    '食人花': 'CannibalPlant',
    '骷髅': 'Skeleton',
    '骷髅战士': 'AxeSkeleton',
    '骷髅战将': 'BoneFighter',
    '骷髅精灵': 'BoneElite',
    '洞蛆': 'CaveMaggot',
    '多钩猫': 'HookingCat',
    '钉耙猫': 'RakingCat',
    '稻草人': 'Scarecrow',
    '毒蜘蛛': 'SpittingSpider',
    '红蛇': 'RedSnake',
    '虎蛇': 'TigerSnake',
    '楔蛾': 'WedgeMoth',
    '黑色恶蛆': 'BlackMaggot',
    '蜈蚣': 'Centipede',
    '红野猪': 'RedBoar',
    '黑野猪': 'BlackBoar',
    '白野猪': 'WhiteBoar',
    '尸王': 'ZombieKing',
    '祖玛卫士': 'ZumaGuardian',
    '祖玛雕像': 'ZumaStatue',
    '祖玛弓箭手': 'ZumaArcher',
    '祖玛教主': 'ZumaTaurus',
    '沃玛教主': 'WoomaTaurus',
    '沃玛卫士': 'WoomaWarrior',
    '触龙神': 'EvilCentipede',
    '神兽': 'Shinsu',
    '变异骷髅': 'BoneElite',
    '鸡': 'Hen',
    '鹿': 'Deer'
}

aliases_items = {
    '金创药(小量)': '(HP)DrugSmall',
    '金创药': '(HP)DrugSmall',
    '魔法药(小量)': '(MP)DrugSmall',
    '魔法药': '(MP)DrugSmall',
    '金创药(中量)': '(HP)DrugMedium',
    '中量金创药': '(HP)DrugMedium',
    '魔法药(中量)': '(MP)DrugMedium',
    '中量魔法药': '(MP)DrugMedium',
    '强效金创药': '(HP)DrugLarge',
    '强效魔法药': '(MP)DrugLarge',
    '太阳水': 'SunWater',
    '强效太阳水': 'GreatSunWater',
    '万年雪霜': 'Ginseng',
    '疗伤药': 'Medicine',
    '肉': 'Meat',
    '鸡肉': 'Chicken',
    '木剑': 'WoodenSword',
    '乌木剑': 'EbonySword',
    '铁剑': 'IronSword',
    '青铜剑': 'BronzeSword',
    '八荒': 'MartialSword',
    '斩马刀': 'BroadSword',
    '修罗': 'PrinceSword',
    '炼狱': 'HeavyAxe',
    '井中月': 'CrescentAxe',
    '裁决之杖': 'StaffOfJudgment',
    '屠龙': 'DragonSlayer',
    '降魔': 'PurifierSword',
    '银蛇': 'SerpentSword',
    '无极棍': 'BoneStaff',
    '龙纹剑': 'DragonSword',
    '偃月': 'Scimitar',
    '魔杖': 'MagicStaff',
    '骨玉权杖': 'BoneStaff',
    '嗜魂法杖': 'SoulStaff',
    '布衣(男)': 'BaseDress(M)',
    '布衣(女)': 'BaseDress(F)',
    '布衣': 'BaseDress(M)',
    '轻型盔甲(男)': 'LightArmour(M)',
    '轻型盔甲(女)': 'LightArmour(F)',
    '中型盔甲(男)': 'MediumArmour(M)',
    '中型盔甲(女)': 'MediumArmour(F)',
    '重盔甲(男)': 'HeavyArmour(M)',
    '重盔甲(女)': 'HeavyArmour(F)',
    '魔法长袍(男)': 'MagicRobe(M)',
    '魔法长袍(女)': 'MagicRobe(F)',
    '灵魂战衣(男)': 'SoulArmour(M)',
    '灵魂战衣(女)': 'SoulArmour(F)',
    '幽灵战衣(男)': 'GhostArmour(M)',
    '幽灵战衣(女)': 'GhostArmour(F)',
    '战神盔甲(男)': 'GodArmour(M)',
    '战神盔甲(女)': 'GodArmour(F)',
    '恶魔长袍(男)': 'DevilRobe(M)',
    '恶魔长袍(女)': 'DevilRobe(F)',
    '随机传送卷': 'TeleportScroll',
    '回城卷': 'TownTeleport',
    '地牢逃脱卷': 'DungeonEscape',
    '行会回城卷': 'GuildTeleport',
    '蜡烛': 'Candle',
    '火把': 'Torch'
}

# Update OfficialMonsterDBRegistry.cs
with open('src/Mir2.Shared/Data/OfficialMonsterDBRegistry.cs', 'r', encoding='utf-8') as f:
    c = f.read()

reg_code = ''
for cn, en in aliases_mon.items():
    reg_code += f'        RegisterAlias("{cn}", "{en}");\n'

alias_methods = '''
    private static void RegisterAlias(string aliasName, string canonicalName)
    {
        if (MonstersByName.TryGetValue(canonicalName, out var entry))
        {
            MonstersByName[aliasName] = entry;
        }
    }
'''

target_str = '        Register(new MonsterDBEntry("Raider2", 104, 45, 71, 99, 1));\n    }'
repl_str = '        Register(new MonsterDBEntry("Raider2", 104, 45, 71, 99, 1));\n\n' + reg_code + '    }' + alias_methods
c = c.replace(target_str, repl_str)

with open('src/Mir2.Shared/Data/OfficialMonsterDBRegistry.cs', 'w', encoding='utf-8') as f:
    f.write(c)

# Update OfficialStdItemsRegistry.cs
with open('src/Mir2.Shared/Data/OfficialStdItemsRegistry.cs', 'r', encoding='utf-8') as f:
    ci = f.read()

item_alias_code = ''
for cn, en in aliases_items.items():
    item_alias_code += f'        RegisterAlias("{cn}", "{en}");\n'

item_alias_methods = '''
    private static void RegisterAlias(string aliasName, string canonicalName)
    {
        if (ItemsByName.TryGetValue(canonicalName, out var entry))
        {
            ItemsByName[aliasName] = entry;
        }
    }
'''

idx_last = ci.rfind('        Register(new StdItemEntry(')
end_of_last = ci.find('\n', idx_last) + 1
ci = ci[:end_of_last] + '\n' + item_alias_code + ci[end_of_last:]
ci = ci.replace('    private static void Register(StdItemEntry e)', item_alias_methods + '\n    private static void Register(StdItemEntry e)')

with open('src/Mir2.Shared/Data/OfficialStdItemsRegistry.cs', 'w', encoding='utf-8') as f:
    f.write(ci)

print('Updated Chinese aliases for both registries successfully!')
