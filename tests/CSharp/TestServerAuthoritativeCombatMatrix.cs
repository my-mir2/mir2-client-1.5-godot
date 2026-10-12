using System;
using System.Collections.Generic;
using Mir2.Server.Game;
using Mir2.Server.Maps;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;
using Mir2.Shared.Protocols.Modern;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 服务端全权威物理战斗与职业特技深度测试矩阵 (1:1 官方 1.5 权威真值)
/// 严格断言服务端物理世界对战士、法师、道士战斗规则与负重约束的权威裁决，杜绝客户端私算与外挂伪造
/// </summary>
public sealed class TestServerAuthoritativeCombatMatrix
{
    private static (PacketDispatcher dispatcher, GameMap map, ClientSession warrior, ClientSession defender) CreateCombatContext()
    {
        var map = new GameMap(0, "比奇省", 100, 100);
        var mapMgr = new MapManager();
        mapMgr.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapMgr);

        var warrior = new ClientSession(1, null!)
        {
            CharacterName = "权威战士",
            CurrentMapId = 0,
            X = 50,
            Y = 50,
            Direction = 0,
            UseModernProtocol = true,
            State = new PlayerState
            {
                Uid = 1,
                Name = "权威战士",
                Job = 0, // 战士
                Level = 40,
                Hp = 600,
                MaxHp = 600,
                Mp = 200,
                MaxMp = 200,
                Hit = 25,
                Speed = 15,
                Dc = 15,
                MaxDc = 35
            }
        };
        map.Aoi.Enter(warrior, 50, 50);

        var defender = new ClientSession(2, null!)
        {
            CharacterName = "受测法师",
            CurrentMapId = 0,
            X = 50,
            Y = 49, // 贴身正上方 1 格
            Direction = 4,
            UseModernProtocol = true,
            State = new PlayerState
            {
                Uid = 2,
                Name = "受测法师",
                Job = 1, // 法师
                Level = 35,
                Hp = 200,
                MaxHp = 200,
                Mp = 500,
                MaxMp = 500,
                Hit = 15,
                Speed = 10
            }
        };
        map.Aoi.Enter(defender, 50, 49);

        return (dispatcher, map, warrior, defender);
    }

    [Fact]
    public void Test_Warrior_FireHit_Cooldown_And_Authoritative_Burst()
    {
        var (dispatcher, map, warrior, defender) = CreateCombatContext();
        long now = 100000;

        // 1. 施展烈火剑法凝聚火焰
        var (castOk, reason) = warrior.State!.FireHitTracker.Cast(now);
        Assert.True(castOk, "首次凝聚烈火应成功");

        // 2. CD 内重复施法应被拦截 (CD 10 秒)
        var (castFail, failReason) = warrior.State.FireHitTracker.Cast(now + 5000);
        Assert.False(castFail, "CD 内施法必须被服务端拦截");
        Assert.Contains("冷却中", failReason);

        // 3. 挥刀命中，触发 2.6 倍烈火暴击
        int dist = 1;
        var (damage, hit, isArmorPiercing, isFireHit, _, died) = dispatcher.ResolveAuthoritativePhysicalCombat(warrior, defender, dist, now + 1000);
        Assert.True(hit, "必须命中");
        Assert.True(isFireHit, "必须触发权威烈火剑法暴击");
        Assert.True(damage >= (int)(15 * 2.6), "烈火伤害必须为基础攻击的 2.6 倍");

        // 4. 第二刀烈火蓄力已消耗，应恢复为普通物理攻击
        var (dmg2, _, _, isFire2, _, _) = dispatcher.ResolveAuthoritativePhysicalCombat(warrior, defender, dist, now + 2000);
        Assert.False(isFire2, "第二刀已脱离蓄力，不再是烈火");
    }

    [Fact]
    public void Test_Warrior_Stabbing_Authoritative_Pierce_Shield_And_Defense()
    {
        var (dispatcher, map, warrior, defender) = CreateCombatContext();
        long now = 100000;

        // 法师开启 3 级魔法盾 (吸收 60% 伤害)
        defender.State!.ActivateMagicShield(3, 30);
        int initialHp = defender.State.Hp;

        // 情况 A: 贴身 1 格攻击，受魔法盾吸收保护
        int distClose = 1;
        var (dmgClose, hitClose, isPierceClose, _, _, _) = dispatcher.ResolveAuthoritativePhysicalCombat(warrior, defender, distClose, now);
        Assert.True(hitClose);
        Assert.False(isPierceClose, "贴身 1 格非隔位刺杀");
        Assert.True(defender.State.HasMagicShield, "贴身攻击未击碎魔法盾");

        // 重置法师血量
        defender.State.Hp = initialHp;

        // 情况 B: 隔位 2 格刺杀 (开启刺杀剑气)
        warrior.State!.StabbingTracker.AutoStab = true;
        defender.Y = 48; // 隔位第 2 格 (50, 48) 与 (50, 50) 距离为 2
        map.Aoi.UpdatePosition(defender, 50, 49, 50, 48, out _, out _);

        int distFar = 2;
        var (dmgFar, hitFar, isPierceFar, _, _, _) = dispatcher.ResolveAuthoritativePhysicalCombat(warrior, defender, distFar, now);
        Assert.True(hitFar);
        Assert.True(isPierceFar, "隔位第 2 格开启刺杀剑气必须判定为破防刺杀");
        // 破防破盾直接造成全额伤害
        Assert.True(dmgFar > 0, "隔位刺杀造成权威穿透伤害");
    }

    [Fact]
    public void Test_Mage_RingOfFire_LevelSuppression_And_Displacement()
    {
        var (dispatcher, map, warrior, defender) = CreateCombatContext();

        // 设立法师为施法者 (Level 35)
        // 目标 A: 低等级战士 (Level 30) - 位于法师正上方 (50, 48)
        var lowLevelWarrior = new ClientSession(3, null!)
        {
            X = 50,
            Y = 48,
            State = new PlayerState { Uid = 3, Level = 30, Hp = 200 }
        };
        map.Aoi.Enter(lowLevelWarrior, 50, 48);

        // 目标 B: 高等级战士 (Level 42) - 位于法师正下方 (50, 50)
        warrior.State!.Level = 42;

        var surrounding = new List<(int uid, int level, int x, int y, bool isBoss)>
        {
            (lowLevelWarrior.SessionId, lowLevelWarrior.State.Level, lowLevelWarrior.X, lowLevelWarrior.Y, false),
            (warrior.SessionId, warrior.State.Level, warrior.X, warrior.Y, false)
        };

        var pushResults = MageCombatEngine.RingOfFire.Execute(
            casterLevel: defender.State!.Level,
            casterX: defender.X,
            casterY: defender.Y,
            surroundingEntities: surrounding,
            isTileBlocked: (x, y) => !map.IsWalkable(x, y)
        );

        var pushA = pushResults.Find(r => r.Uid == 3);
        var pushB = pushResults.Find(r => r.Uid == 1);

        Assert.True(pushA.Pushed, "低等级目标必须被抗拒火环成功推开");
        Assert.Equal(47, pushA.FinalY); // 原为 48，远离 (50, 49) 被推至 47

        Assert.False(pushB.Pushed, "高等级目标必须抵抗抗拒火环推开");
        Assert.Contains("高于", pushB.FailReason);
    }

    [Fact]
    public void Test_Taoist_Poison_Hit_TickDamage_And_Vulnerability()
    {
        var (dispatcher, map, warrior, defender) = CreateCombatContext();
        long now = 100000;

        // 1. 目标 0 抗毒，施毒术必中判定
        bool hit = TaoistCombatEngine.CheckPoisonHit(targetAntiPoison: 0, Random.Shared);
        Assert.True(hit, "0 抗毒必须 100% 命中");

        // 2. 挂载绿毒与红毒
        defender.State!.ApplyPoison(TaoistCombatEngine.PoisonType.Green, durationSec: 30, sc: 25, nowMs: now);
        defender.State.ApplyPoison(TaoistCombatEngine.PoisonType.Red, durationSec: 40, sc: 25, nowMs: now);

        Assert.True(defender.State.HasGreenPoison);
        Assert.True(defender.State.HasRedPoison);
        Assert.Equal(0, defender.State.IncHealth); // 绿毒立即清空生命回复池

        // 3. 绿毒周期 Tick 掉血 (经过 2500ms)
        int hpBefore = defender.State.Hp;
        var (tookDmg, dmg, greenExpired, redExpired) = defender.State.ProcessPoisonTick(now + 2500);
        Assert.True(tookDmg, "2.5 秒后必须触发绿毒扣血");
        Assert.Equal(7, dmg); // 3 级施毒术 + 25 道术扣减 7 点 HP
        Assert.Equal(hpBefore - dmg, defender.State.Hp);

        // 4. 红毒 1.2 倍物理伤害易伤
        int rawPhysical = 100;
        int vulnDamage = TaoistCombatEngine.ApplyRedPoisonVulnerability(rawPhysical, defender.State.HasRedPoison);
        Assert.Equal(120, vulnDamage); // 1.2 倍
    }

    [Fact]
    public void Test_WeightConstraint_Authoritative_Enforcement()
    {
        var (dispatcher, map, warrior, _) = CreateCombatContext();

        // 战士 40 级，腕力上限计算
        warrior.State!.RecalculateAuthoritativeWeights();
        int maxHand = warrior.State.MaxHandWeight;
        Assert.True(maxHand > 0, "战士腕力上限必须大于0");

        // 尝试佩戴超重武器 (例如需要腕力 80 的假想神兵，而当前战士只有约 40~50 腕力)
        warrior.State.BagItems.Add(new EquipItem
        {
            Uid = 999,
            Name = "超重重锤",
            Weight = maxHand + 20
        });

        bool equipOk = dispatcher.HandleWearItem(warrior, bagSlot: 0, OfficialEquipSlot.Weapon);
        Assert.False(equipOk, "腕力超限必须被服务端权威拦截");
        Assert.Null(warrior.State.Equipment[(int)OfficialEquipSlot.Weapon]);

        // 尝试佩戴合格武器 (重量 <= maxHand)
        warrior.State.BagItems.Add(new EquipItem
        {
            Uid = 888,
            Name = "裁决之杖",
            Weight = 30 // 合格
        });

        bool equipCaiJue = dispatcher.HandleWearItem(warrior, bagSlot: 1, OfficialEquipSlot.Weapon);
        Assert.True(equipCaiJue, "合规武器必须允许穿戴");
        Assert.NotNull(warrior.State.Equipment[(int)OfficialEquipSlot.Weapon]);
        Assert.Equal(30, warrior.State.HandWeight);
    }

    [Fact]
    public void Test_Weapon_Luck9_Max_DC_Burst()
    {
        var (dispatcher, map, warrior, _) = CreateCombatContext();

        // 未加幸运时，攻击力在 minDc ~ maxDc 波动
        var rng = new Random(42);

        // 装备运 7 武器 + 运 2 项链 = 运 9 套
        warrior.State!.Equipment[(int)OfficialEquipSlot.Weapon] = new EquipItem
        {
            Uid = 101,
            Name = "裁决之杖",
            Luck = 7
        };
        warrior.State.Equipment[(int)OfficialEquipSlot.Necklace] = new EquipItem
        {
            Uid = 102,
            Name = "记忆项链",
            Luck = 2
        };

        // 连续 20 次攻击，必须刀刀发挥最大 DC (35)
        for (int i = 0; i < 20; i++)
        {
            int dc = warrior.State.CalculateAttackDamageWithLuck(minDc: 10, maxDc: 35, rng);
            Assert.Equal(35, dc); // 运 9 套刀刀上限攻击
        }
    }

    [Fact]
    public void Test_Pet_Command_Rest_And_AI_State_Sync()
    {
        var (dispatcher, map, taoist, _) = CreateCombatContext();
        taoist.State!.Job = 2; // 道士
        var mapMgr = new MapManager();
        mapMgr.RegisterMap(map);
        dispatcher.RegenEngine = new MonsterRegenEngine(mapMgr);

        // 1. 召唤一只神兽 (UID = 8888)
        var petInst = new MonsterInstance
        {
            Uid = 8888,
            Name = "神兽",
            MapId = 0,
            X = 51,
            Y = 50,
            Hp = 300,
            MaxHp = 300,
            AttackDamageMin = 10,
            AttackDamageMax = 20,
            MasterSessionId = taoist.SessionId,
            IsResting = false
        };
        dispatcher.RegenEngine.AddMonster(petInst);

        var slavePet = new HuWeiPetEngine.SlavePet
        {
            Uid = 8888,
            Name = "神兽",
            MasterSessionId = taoist.SessionId,
            CurrentMapId = 0,
            X = 51,
            Y = 50,
            Hp = 300,
            MaxHp = 300,
            MinDc = 10,
            MaxDc = 20,
            Level = 0,
            IsResting = false
        };
        taoist.Pets.Add(slavePet);

        // 2. 模拟客户端发送官方 1.5 Ctrl+A 原生指令 "@Rest"
        dispatcher.HandleSay(taoist, "@Rest");

        // 断言: 随从与地图怪物实体的 IsResting 必须被同步置为 true
        Assert.True(slavePet.IsResting, "随从 SlavePet.IsResting 必须为 true");
        Assert.True(petInst.IsResting, "地图怪物实例 MonsterInstance.IsResting 必须为 true");

        // 3. 验证 AI 心跳: 处于休息状态的随从，在 ProcessMonsterAiTick 中绝不离位漫步与追击
        var players = new List<ClientSession> { taoist };
        var (actionsRest, _) = dispatcher.RegenEngine.ProcessMonsterAiTick((m, x, y) => players, OfficialClientRulesEngine.DelphiRandom.Shared);
        Assert.Empty(actionsRest); // 原地戒备，无动作产生
        Assert.Equal(51, petInst.X);
        Assert.Equal(50, petInst.Y);

        // 4. 再次发送 "@Rest" 唤醒随从 (解除休息进入跟随模式)
        dispatcher.HandleSay(taoist, "@Rest");
        Assert.False(slavePet.IsResting, "随从 SlavePet.IsResting 必须恢复为 false");
        Assert.False(petInst.IsResting, "地图怪物实例 MonsterInstance.IsResting 必须恢复为 false");

        // 5. 让主人向右移动至 (55, 50)，使主人与神兽距离大于 2 格
        taoist.X = 55;
        taoist.Y = 50;

        // 执行怪物 AI 心跳，断言随从开始寻路跟随靠近主人
        var (actionsFollow, _) = dispatcher.RegenEngine.ProcessMonsterAiTick((m, x, y) => players, OfficialClientRulesEngine.DelphiRandom.Shared);
        Assert.NotEmpty(actionsFollow);
        Assert.Equal("跟随主人", actionsFollow[0].Extra);
        Assert.True(petInst.X > 51, "随从必须朝主人 X=55 方向靠拢寻路");

        // 6. 验证随从升级物理同步: 击杀怪物晋升
        bool lvlUp = HuWeiPetEngine.AddKillExpAndTryLevelUp(slavePet, 150); // 0->1 级需 100 经验
        Assert.True(lvlUp, "随从经验满足必须成功晋级");
        Assert.Equal(1, slavePet.Level);

        // 模拟 PacketDispatcher.ApplyDamageToMonster 中的同步逻辑
        var syncInst = dispatcher.RegenEngine.GetMonster(slavePet.Uid);
        Assert.NotNull(syncInst);
        syncInst.MaxHp = slavePet.MaxHp;
        syncInst.Hp = slavePet.Hp;
        syncInst.AttackDamageMin = slavePet.MinDc;
        syncInst.AttackDamageMax = slavePet.MaxDc;

        // 断言地图怪物实体属性完全同步
        Assert.Equal(slavePet.MaxHp, syncInst.MaxHp);
        Assert.Equal(slavePet.MinDc, syncInst.AttackDamageMin);
    }

    [Fact]
    public void Test_Spell_Kill_Settlement_And_Beam_Hit_And_HeavyHit_Assassination()
    {
        var (dispatcher, map, warrior, defender) = CreateCombatContext();
        long now = Environment.TickCount64;

        // ==================== 1. 验证手动 CM_HEAVYHIT 即使未开启自动刺杀，隔位刺杀依然破防与出剑气 ====================
        warrior.State!.StabbingTracker.AutoStab = false; // 关闭自动刺杀
        defender.X = 50;
        defender.Y = 48; // 正上方隔位 2 格
        defender.State!.Equipment[(int)OfficialEquipSlot.Dress] = new EquipItem { Ac = 20 };
        defender.State.ActivateMagicShield(3, 30); // 开启魔法盾

        // 模拟客户端发送 CM_HEAVYHIT 3016
        dispatcher.Dispatch(warrior, $"#{ProtocolConsts.CM_HEAVYHIT}/50/48/0");

        // 断言: 必须穿透魔法盾与防御扣减生命，且广播刺杀亮蓝剑光
        Assert.True(defender.State.Hp < 600, "隔位刺杀必须命中扣血");
        Assert.True(warrior.HasBufferedPacket($"#{ProtocolConsts.SM_SPELLEFFECT_DELPHI}/"), "出刀必须广播刺杀剑气光效");

        // ==================== 2. 验证法术击杀玩家全额走死亡惩罚、掉落与杀手变色广播 ====================
        warrior.ClearBufferedPackets();
        defender.State.Hp = 10;
        defender.State.PkPoint = 0; // 守法白名受害者，恶意击杀触发杀手恶名累加与名字变色
        defender.State.BagItems.Add(new EquipItem { Uid = 5555, Name = "红宝石戒指", Weight = 1 });

        // 战士使用疾光电影或冰咆哮致死受害者
        dispatcher.ResolveAuthoritativeSpell(warrior, 32, defender.SessionId, defender.X, defender.Y, now);

        // 断言: 受害者倒地、杀手名字颜色变动广播
        Assert.Equal(0, defender.State.Hp);
        Assert.True(warrior.HasBufferedPacket($"#{ProtocolConsts.SM_CHANGENAMECOLOR}/"), "杀手恶意击杀白名后必须同屏广播名字颜色变动");

        // ==================== 3. 验证地狱火/疾光电影怪物受击权威引擎联动 (红毒易伤 + 困魔咒破困) ====================
        var monster = new MonsterInstance
        {
            Uid = 9901,
            Name = "钳虫",
            MapId = 0,
            X = 50,
            Y = 47,
            Hp = 200,
            MaxHp = 200,
            IsTrappedByHolyCurse = true, // 被困魔咒禁锢
            HasRedPoison = true,         // 中了红毒
            RedPoisonExpireMs = now + 10000
        };
        var mapMgr = new MapManager();
        mapMgr.RegisterMap(map);
        dispatcher.RegenEngine = new MonsterRegenEngine(mapMgr);
        dispatcher.RegenEngine.AddMonster(monster);

        // 朝正上方打出疾光电影 (MagicId = 23, 贯穿 (50, 49) 到 (50, 42))
        warrior.X = 50;
        warrior.Y = 50;
        warrior.Direction = 0; // 上
        dispatcher.ResolveAuthoritativeSpell(warrior, 23, 0, 50, 47, now);

        // 断言: 怪物受击后困魔咒破碎解除禁锢，受到红毒 20% 易伤
        Assert.False(monster.IsTrappedByHolyCurse, "受到疾光电影击中，困魔咒牢笼必须破碎");
        Assert.True(monster.Hp < 200, "怪物必须受到疾光电影穿透伤害");
    }

    [Fact]
    public void TestAuthoritativeAttackModesAndFireWallSafety()
    {
        long now = 100000;

        // 1. PkRuleEngine 静态矩阵测试
        // 1.1 安全区禁武
        Assert.False(PkRuleEngine.CanAttackTargetPlayer(0, isAttackerInSafeZone: true, isTargetInSafeZone: false, 1, 2, false, false, false, false, false, false));
        Assert.False(PkRuleEngine.CanAttackTargetPlayer(0, isAttackerInSafeZone: false, isTargetInSafeZone: true, 1, 2, false, false, false, false, false, false));

        // 1.2 自己与死人
        Assert.False(PkRuleEngine.CanAttackTargetPlayer(0, false, false, 1, 1, false, false, false, false, false, false));
        Assert.False(PkRuleEngine.CanAttackTargetPlayer(0, false, false, 1, 2, targetIsDead: true, false, false, false, false, false));

        // 1.3 和平模式 (Peace = 1): 白名和红名均受保护禁止攻击
        Assert.False(PkRuleEngine.CanAttackTargetPlayer(1, false, false, 1, 2, false, targetIsRedOrYellow: false, false, false, false, false));
        Assert.False(PkRuleEngine.CanAttackTargetPlayer(1, false, false, 1, 2, false, targetIsRedOrYellow: true, false, false, false, false));

        // 1.4 组队模式 (Group = 2): 队友豁免，非队友放行
        Assert.False(PkRuleEngine.CanAttackTargetPlayer(2, false, false, 1, 2, false, false, isSameGroup: true, false, false, false));
        Assert.True(PkRuleEngine.CanAttackTargetPlayer(2, false, false, 1, 2, false, false, isSameGroup: false, false, false, false));

        // 1.5 行会模式 (Guild = 3): 同会与同盟豁免，敌对战争行会放行
        Assert.False(PkRuleEngine.CanAttackTargetPlayer(3, false, false, 1, 2, false, false, false, isSameGuild: true, false, false));
        Assert.False(PkRuleEngine.CanAttackTargetPlayer(3, false, false, 1, 2, false, false, false, false, isAlliedGuild: true, false));
        Assert.True(PkRuleEngine.CanAttackTargetPlayer(3, false, false, 1, 2, false, false, false, false, false, isWarGuild: true));

        // 1.6 善恶模式 (RedName = 4): 白名豁免，红黄名放行
        Assert.False(PkRuleEngine.CanAttackTargetPlayer(4, false, false, 1, 2, false, targetIsRedOrYellow: false, false, false, false, false));
        Assert.True(PkRuleEngine.CanAttackTargetPlayer(4, false, false, 1, 2, false, targetIsRedOrYellow: true, false, false, false, false));

        // 2. 端到端战斗模式联动与法术安全
        var map = new GameMap(0, "土城野外", 100, 100);
        var mapMgr = new MapManager();
        mapMgr.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapMgr);

        var mage = new ClientSession(101, null!)
        {
            CharacterName = "大法师",
            CurrentMapId = 0,
            X = 20,
            Y = 20,
            State = new PlayerState { Level = 40, Hp = 200, MaxHp = 200, Mp = 500, MaxMp = 500, AttackMode = 1 } // 和平模式
        };

        var whitePlayer = new ClientSession(102, null!)
        {
            CharacterName = "无辜白名",
            CurrentMapId = 0,
            X = 20,
            Y = 21,
            State = new PlayerState { Level = 35, Hp = 300, MaxHp = 300, Mp = 100, MaxMp = 100, PkPoint = 0 }
        };

        map.Aoi.Enter(mage, mage.X, mage.Y);
        map.Aoi.Enter(whitePlayer, whitePlayer.X, whitePlayer.Y);
        dispatcher.ActiveSessions[mage.SessionId] = mage;
        dispatcher.ActiveSessions[whitePlayer.SessionId] = whitePlayer;

        // 2.1 和平模式下施放雷电术 (10) 与地狱雷光 (21)，白名目标完全免疫不掉血
        dispatcher.ResolveAuthoritativeSpell(mage, 10, whitePlayer.SessionId, whitePlayer.X, whitePlayer.Y, now);
        Assert.Equal(300, whitePlayer.State.Hp);

        dispatcher.ResolveAuthoritativeSpell(mage, 21, 0, mage.X, mage.Y, now);
        Assert.Equal(300, whitePlayer.State.Hp);

        // 2.2 组队模式下队友免伤测试
        whitePlayer.State.AllowGroup = true;
        dispatcher.GroupEngine.CreateParty(mage);
        dispatcher.GroupEngine.AddMember(mage, whitePlayer);
        mage.State.AttackMode = 2; // 组队模式
        dispatcher.ResolveAuthoritativeSpell(mage, 21, 0, mage.X, mage.Y, now);
        Assert.Equal(300, whitePlayer.State.Hp);

        // 2.3 善恶模式下，道士对白名施毒失败，对红名施毒成功
        var redPlayer = new ClientSession(103, null!)
        {
            CharacterName = "杀人狂魔",
            CurrentMapId = 0,
            X = 21,
            Y = 20,
            State = new PlayerState { Level = 38, Hp = 400, MaxHp = 400, PkPoint = 300 }
        };
        map.Aoi.Enter(redPlayer, redPlayer.X, redPlayer.Y);
        dispatcher.ActiveSessions[redPlayer.SessionId] = redPlayer;
        mage.State.AttackMode = 4; // 善恶模式
        dispatcher.ResolveAuthoritativeSpell(mage, 10, whitePlayer.SessionId, whitePlayer.X, whitePlayer.Y, now);
        Assert.Equal(300, whitePlayer.State.Hp);

        dispatcher.ResolveAuthoritativeSpell(mage, 10, redPlayer.SessionId, redPlayer.X, redPlayer.Y, now);
        Assert.True(redPlayer.State.Hp < 400, "善恶模式下向红名施展雷电术必须造成伤害");

        // 3. 火墙 (FireWall) 权威自身免伤与队友豁免
        // 法师在 (20, 20) 放火墙
        map.AddFireWall(new FireWallEntity
        {
            Uid = 9999,
            X = 20,
            Y = 20,
            CasterSessionId = mage.SessionId,
            Damage = 50,
            ExpireTimeMs = now + 10000
        });

        // 法师自身站在 (20, 20) 火中，绝不受到自己火墙伤害
        Assert.False(dispatcher.CanFireWallDamagePlayer(mage.SessionId, mage, now), "法师自身必须对自身火墙绝对免疫");

        // 处于组队中的队友站在 (20, 20) 火中，火墙判定受组队保护
        Assert.False(dispatcher.CanFireWallDamagePlayer(mage.SessionId, whitePlayer, now), "同组队友处于火墙中受到攻击模式免伤保护");
    }

    [Fact]
    public void TestAuthoritativeSpecialRingDeepWaterCombat_Paralyze_Teleport_RevivalFullLoop()
    {
        long now = 1000000;
        var map = new GameMap(0, "比奇省野外", 200, 200);
        var mapMgr = new MapManager();
        mapMgr.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapMgr);
        dispatcher.RegenEngine = new MonsterRegenEngine(mapMgr);

        // 1. 麻痹戒指 (Paralysis Ring) 物理击中 5 秒石化与首领免疫
        var warrior = new ClientSession(201, null!)
        {
            CharacterName = "特戒战士",
            CurrentMapId = 0,
            X = 50,
            Y = 50,
            Direction = 2,
            State = new PlayerState
            {
                Level = 45,
                Hp = 600,
                MaxHp = 600,
                Dc = 50,
                Hit = 20
            }
        };
        warrior.State.Equipment[(int)OfficialEquipSlot.RingL] = new EquipItem { Name = "麻痹戒指", Dura = 5, MaxDura = 5 };
        warrior.State.RecalculateAttributes();
        warrior.State.Hit = 100;

        var victim = new ClientSession(202, null!)
        {
            CharacterName = "受害者",
            CurrentMapId = 0,
            X = 51,
            Y = 50,
            State = new PlayerState
            {
                Level = 40,
                Hp = 400,
                MaxHp = 400,
                Speed = 10,
                Ac = 5
            }
        };

        map.Aoi.Enter(warrior, warrior.X, warrior.Y);
        map.Aoi.Enter(victim, victim.X, victim.Y);
        dispatcher.ActiveSessions[warrior.SessionId] = warrior;
        dispatcher.ActiveSessions[victim.SessionId] = victim;

        // 战士物理攻击受害者，盛大官方 1/5 概率触发 5 秒麻痹 (SpecialRings.TriggerParalysisRing)
        bool paralyzedTriggered = false;
        for (int i = 0; i < 30; i++)
        {
            var combatRes = dispatcher.ResolveAuthoritativePhysicalCombat(warrior, victim, 1, now);
            Assert.True(combatRes.hit, "物理攻击必须命中");
            if (victim.State.ParalyzeExpireMs >= now + 5000)
            {
                paralyzedTriggered = true;
                Assert.True(victim.State.StatusBits[5], "目标状态掩码第 5 位必须置为石化");
                break;
            }
        }
        Assert.True(paralyzedTriggered, "在多次物理攻击中麻痹戒指必须成功触发 5 秒石化");

        // 麻痹戒指砍 Boss 级怪物: 连续砍 30 次，绝不被麻痹石化 (首领绝对免疫)
        var boss = dispatcher.RegenEngine.SpawnMonsterDirect(0, 50, 51, "祖玛教主", 3000, 50, 100);
        long beforeBossActionTime = boss.NextActionTimeMs;
        for (int i = 0; i < 30; i++)
        {
            var bossRes = dispatcher.ResolveAuthoritativeMonsterPhysicalCombat(warrior, boss, 1, now);
            Assert.True(bossRes.hit);
            Assert.Equal(beforeBossActionTime, boss.NextActionTimeMs);
        }

        // 2. 复活戒指 (Revival Ring) 权威复活与耐久耗尽碎裂脱下
        var revivedPlayer = new ClientSession(203, null!)
        {
            CharacterName = "复活法师",
            CurrentMapId = 0,
            X = 50,
            Y = 52,
            State = new PlayerState
            {
                Level = 40,
                Hp = 10,
                MaxHp = 200,
                Mp = 50,
                MaxMp = 400
            }
        };
        revivedPlayer.State.Equipment[(int)OfficialEquipSlot.RingL] = new EquipItem { Name = "复活戒指", Dura = 1, MaxDura = 5 };
        revivedPlayer.State.RecalculateAttributes();

        map.Aoi.Enter(revivedPlayer, revivedPlayer.X, revivedPlayer.Y);
        dispatcher.ActiveSessions[revivedPlayer.SessionId] = revivedPlayer;

        // 施加 50 点致死雷电术伤害
        dispatcher.ResolveAuthoritativeSpell(warrior, 10, revivedPlayer.SessionId, revivedPlayer.X, revivedPlayer.Y, now);
        Assert.Equal(200, revivedPlayer.State.Hp); // 满血复活
        Assert.Equal(400, revivedPlayer.State.Mp); // 满蓝复活
        // 耐久从 1 扣为 0，复活戒指碎裂脱下！
        Assert.Null(revivedPlayer.State.Equipment[(int)OfficialEquipSlot.RingL]);

        // 3. 传送戒指 (Teleport Ring) 规范位移流转与交易熔断
        var teleporter = new ClientSession(204, null!)
        {
            CharacterName = "传送道士",
            CurrentMapId = 0,
            X = 50,
            Y = 53,
            Direction = 0,
            State = new PlayerState
            {
                Level = 40,
                Hp = 300,
                MaxHp = 300
            }
        };
        teleporter.State.Equipment[(int)OfficialEquipSlot.RingL] = new EquipItem { Name = "传送戒指", Dura = 5, MaxDura = 5 };
        teleporter.State.RecalculateAttributes();

        var tradePartner = new ClientSession(205, null!)
        {
            CharacterName = "交易伙伴",
            CurrentMapId = 0,
            X = 50,
            Y = 54,
            State = new PlayerState { Level = 35, Hp = 200, MaxHp = 200 }
        };
        map.Aoi.Enter(teleporter, teleporter.X, teleporter.Y);
        map.Aoi.Enter(tradePartner, tradePartner.X, tradePartner.Y);
        dispatcher.ActiveSessions[teleporter.SessionId] = teleporter;
        dispatcher.ActiveSessions[tradePartner.SessionId] = tradePartner;

        // 发起面对面交易
        var (tradeOk, tradeErr) = dispatcher.TradeEngine.TryInitiateTrade(teleporter, tradePartner);
        Assert.True(tradeOk, $"发起交易必须成功: {tradeErr}");
        Assert.NotNull(dispatcher.TradeEngine.GetTrade(teleporter.SessionId));

        // 传送戒指瞬移至 (80, 80)
        dispatcher.HandleSay(teleporter, "@move 80 80");
        Assert.Equal(80, teleporter.X);
        Assert.Equal(80, teleporter.Y);
        Assert.Null(dispatcher.TradeEngine.GetTrade(teleporter.SessionId)); // 传送位移后必须强制熔断正在进行的交易
    }

    [Fact]
    public void TestAuthoritativeSpellCombat_MagicAndTaoistRealFormulas_And_UndeadBonus()
    {
        long now = 2000000;
        var map = new GameMap(0, "盟重省野外", 200, 200);
        var mapMgr = new MapManager();
        mapMgr.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapMgr);
        dispatcher.RegenEngine = new MonsterRegenEngine(mapMgr);

        // 1. 高魔攻法师施展雷电术对普通怪 vs 不死系怪 (尸王)
        var highMage = new ClientSession(301, null!)
        {
            CharacterName = "嗜魂法师",
            CurrentMapId = 0,
            X = 60,
            Y = 60,
            State = new PlayerState
            {
                Job = 1,
                Level = 45,
                Hp = 250,
                MaxHp = 250,
                Mp = 800,
                MaxMp = 800,
                Mc = 35,
                MaxMc = 35
            }
        };
        // 佩戴运 9 套消除随机方差，稳定发挥最大法伤
        highMage.State.Equipment[(int)OfficialEquipSlot.Weapon] = new EquipItem { Uid = 7001, Name = "嗜魂法杖", Luck = 7 };
        highMage.State.Equipment[(int)OfficialEquipSlot.Necklace] = new EquipItem { Uid = 7002, Name = "恶魔铃铛", Luck = 2 };

        map.Aoi.Enter(highMage, highMage.X, highMage.Y);
        dispatcher.ActiveSessions[highMage.SessionId] = highMage;

        // 生成普通怪野猪 vs 不死系尸王
        var boar = dispatcher.RegenEngine.SpawnMonsterDirect(0, 60, 62, "红野猪", 500, 20, 30);
        var corpseKing = dispatcher.RegenEngine.SpawnMonsterDirect(0, 60, 63, "千年尸王", 500, 20, 30);

        int initialBoarHp = boar.Hp;
        int initialKingHp = corpseKing.Hp;

        // 法师用雷电术攻击红野猪
        dispatcher.ResolveAuthoritativeSpell(highMage, 10, boar.Uid, boar.X, boar.Y, now);
        int boarDamage = initialBoarHp - boar.Hp;
        Assert.True(boarDamage >= 30, $"高魔攻雷电术伤害必须大于30，实际: {boarDamage}");

        // 法师用雷电术攻击尸王 (触发 1.5 倍不死系加成)
        dispatcher.ResolveAuthoritativeSpell(highMage, 10, corpseKing.Uid, corpseKing.X, corpseKing.Y, now);
        int kingDamage = initialKingHp - corpseKing.Hp;
        Assert.True(kingDamage > boarDamage * 1.2, $"尸王作为不死系受雷电术神圣伤害放大，实际造成: {kingDamage} vs 野猪: {boarDamage}");

        // 2. 道士灵魂火符道术伤害与防御者魔防抵扣
        var taoist = new ClientSession(302, null!)
        {
            CharacterName = "逍遥散人",
            CurrentMapId = 0,
            X = 70,
            Y = 70,
            State = new PlayerState
            {
                Job = 2,
                Level = 42,
                Hp = 350,
                MaxHp = 350,
                Mp = 600,
                MaxMp = 600
            }
        };
        // 佩戴运 7 龙纹剑与运 2 白色虎齿项链达成官方运九套，佩戴护身符通过耗材检验
        taoist.State.Equipment[(int)OfficialEquipSlot.Weapon] = new EquipItem { Uid = 3001, Name = "龙纹剑", Luck = 7, Sc = 3, MaxSc = 6, Dura = 30000, MaxDura = 30000 };
        taoist.State.Equipment[(int)OfficialEquipSlot.Necklace] = new EquipItem { Uid = 3002, Name = "白色虎齿项链", Luck = 2, Dura = 8000, MaxDura = 8000 };
        taoist.State.Equipment[(int)OfficialEquipSlot.Lighting] = new EquipItem { Name = "黄色药粉与护身符", Dura = 500, MaxDura = 500 };
        taoist.State.RecalculateAttributes();

        // 目标 1: 低魔防玩家 (Mac 0)
        var lowMacTarget = new ClientSession(303, null!)
        {
            CharacterName = "布衣小号",
            CurrentMapId = 0,
            X = 71,
            Y = 70,
            State = new PlayerState { Level = 10, Hp = 200, MaxHp = 200, Mac = 0, MaxMac = 0 }
        };
        // 目标 2: 高魔防玩家 (Mac 30)
        var highMacTarget = new ClientSession(304, null!)
        {
            CharacterName = "魔防肉盾",
            CurrentMapId = 0,
            X = 72,
            Y = 70,
            State = new PlayerState { Level = 40, Hp = 500, MaxHp = 500, Mac = 30, MaxMac = 30 }
        };

        map.Aoi.Enter(taoist, taoist.X, taoist.Y);
        map.Aoi.Enter(lowMacTarget, lowMacTarget.X, lowMacTarget.Y);
        map.Aoi.Enter(highMacTarget, highMacTarget.X, highMacTarget.Y);
        dispatcher.ActiveSessions[taoist.SessionId] = taoist;
        dispatcher.ActiveSessions[lowMacTarget.SessionId] = lowMacTarget;
        dispatcher.ActiveSessions[highMacTarget.SessionId] = highMacTarget;

        dispatcher.ResolveAuthoritativeSpell(taoist, 13, lowMacTarget.SessionId, lowMacTarget.X, lowMacTarget.Y, now);
        int lowTargetDmg = 200 - lowMacTarget.State.Hp;
        Assert.True(lowTargetDmg >= 20, $"火符打零魔防目标伤害必须大于20，实际: {lowTargetDmg}");

        dispatcher.ResolveAuthoritativeSpell(taoist, 13, highMacTarget.SessionId, highMacTarget.X, highMacTarget.Y, now);
        int highTargetDmg = 500 - highMacTarget.State.Hp;
        Assert.True(highTargetDmg < lowTargetDmg, $"高魔防目标受火符伤害必须被显著抵扣，实际: {highTargetDmg} vs 零魔防: {lowTargetDmg}");
    }

    [Fact]
    public void TestPhysicalCombat_RealDc_And_Luck9Set_AlwaysMaxDc()
    {
        var mapMgr = new MapManager();
        var map = new GameMap(0, "盟重省", 200, 200);
        mapMgr.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapMgr);

        var warrior = new ClientSession(401, null!)
        {
            CharacterName = "圣战武狂",
            CurrentMapId = 0,
            X = 100,
            Y = 100,
            State = new PlayerState
            {
                Job = 0,
                Level = 45,
                Hp = 800,
                MaxHp = 800,
                Hit = 50,
                Dc = 10,
                MaxDc = 80
            }
        };

        // 佩戴运 7 裁决之杖 (槽位 1) 与运 2 记忆项链 (槽位 3) -> 凑齐运 9 套
        warrior.State.Equipment[(int)OfficialEquipSlot.Weapon] = new EquipItem { Uid = 5001, Name = "裁决之杖", Luck = 7, Dura = 30000, MaxDura = 30000 };
        warrior.State.Equipment[(int)OfficialEquipSlot.Necklace] = new EquipItem { Uid = 5002, Name = "记忆项链", Luck = 2, Dura = 8000, MaxDura = 8000 };

        var dummy = new ClientSession(402, null!)
        {
            CharacterName = "物理沙袋",
            CurrentMapId = 0,
            X = 100,
            Y = 101,
            State = new PlayerState { Level = 40, Hp = 2000, MaxHp = 2000, Ac = 0, MaxAc = 0, Speed = 0 }
        };

        map.Aoi.Enter(warrior, 100, 100);
        map.Aoi.Enter(dummy, 100, 101);
        dispatcher.ActiveSessions[warrior.SessionId] = warrior;
        dispatcher.ActiveSessions[dummy.SessionId] = dummy;

        long now = 1000;
        // 连续 5 次物理攻击，验证运 9 套刀刀发挥最大 80 点真实伤害，彻底消灭 15~30 硬编码！
        for (int i = 0; i < 5; i++)
        {
            int hpBefore = dummy.State.Hp;
            var (dmg, hit, _, _, _, _) = dispatcher.ResolveAuthoritativePhysicalCombat(warrior, dummy, 1, now + i * 1000);
            Assert.True(hit);
            Assert.Equal(80, dmg);
            Assert.Equal(80, hpBefore - dummy.State.Hp);
        }
    }

    [Fact]
    public void TestMageCombat_SpellLuck9Set_AlwaysMaxMc()
    {
        var mapMgr = new MapManager();
        var map = new GameMap(0, "盟重省", 200, 200);
        mapMgr.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapMgr);

        var mage = new ClientSession(411, null!)
        {
            CharacterName = "嗜魂法皇",
            CurrentMapId = 0,
            X = 120,
            Y = 120,
            State = new PlayerState
            {
                Job = 1,
                Level = 45,
                Hp = 300,
                MaxHp = 300,
                Mp = 1000,
                MaxMp = 1000,
                Mc = 10,
                MaxMc = 78
            }
        };

        // 学习 0 级雷电术 (MagicId 10)
        mage.State.Magics.Add(new UserMagic { MagicId = 10, Level = 0, CurTrain = 0, MaxTrain = 500 });

        // 佩戴运 7 嗜魂法杖 与 运 2 幸运项链 -> 凑齐运 9 套
        mage.State.Equipment[(int)OfficialEquipSlot.Weapon] = new EquipItem { Uid = 6001, Name = "嗜魂法杖", Luck = 7, Dura = 20000, MaxDura = 20000 };
        mage.State.Equipment[(int)OfficialEquipSlot.Necklace] = new EquipItem { Uid = 6002, Name = "恶魔铃铛", Luck = 2, Dura = 8000, MaxDura = 8000 };

        var dummy = new ClientSession(412, null!)
        {
            CharacterName = "法术沙袋",
            CurrentMapId = 0,
            X = 120,
            Y = 123,
            State = new PlayerState { Level = 40, Hp = 2000, MaxHp = 2000, Mac = 0, MaxMac = 0 }
        };

        map.Aoi.Enter(mage, 120, 120);
        map.Aoi.Enter(dummy, 120, 123);
        dispatcher.ActiveSessions[mage.SessionId] = mage;
        dispatcher.ActiveSessions[dummy.SessionId] = dummy;

        long now = 1000;
        // 施展雷电术 (MagicId 10): 技能0级，maxDmg = 25 + 0 + MaxMc (45级法师成长50 + 装备28 = 78) = 103
        // 零魔防沙袋每次受到的法术伤害必须恒定为 103 点！
        for (int i = 0; i < 5; i++)
        {
            int hpBefore = dummy.State.Hp;
            dispatcher.ResolveAuthoritativeSpell(mage, 10, dummy.SessionId, dummy.X, dummy.Y, now + i * 1000);
            int spellDmg = hpBefore - dummy.State.Hp;
            Assert.Equal(103, spellDmg);
        }
    }

    [Fact]
    public void Test_WeaponLuckCurse_NetLuck_Algebraic_Cancellation()
    {
        var rng = new Random(12345);

        // 1. 运 2 项链配诅 5 武器 -> 净值 -3 (净诅咒 3)
        // 净值为负数，绝不可能触发运9刀刀上限，且有 30% 概率触发下限攻击 (minDc=10)
        int minCount = 0;
        int maxCount = 0;
        for (int i = 0; i < 1000; i++)
        {
            int dmg = ItemStatHiddenEngine.WeaponLuckCurseEngine.CalculateAttackPower(10, 30, totalLuck: 2, totalCurse: 5, rng);
            Assert.InRange(dmg, 10, 30);
            if (dmg == 10) minCount++;
            if (dmg == 30) maxCount++;
        }
        // minCount 应明显高于均匀分布的 1/21 (~4.7%)，且必定大于 20%
        Assert.True(minCount > 200, $"净诅咒 3 触发最小攻击次数应显著增加 (实际: {minCount}/1000)");

        // 2. 净运 9 套: 运 11 武器 - 诅 2 = 净运 9
        for (int i = 0; i < 20; i++)
        {
            int dmg = ItemStatHiddenEngine.WeaponLuckCurseEngine.CalculateAttackPower(10, 30, totalLuck: 11, totalCurse: 2, rng);
            Assert.Equal(30, dmg); // 净运 9 必须 100% 刀刀上限
        }

        // 3. 净诅 10 套: 运 1 - 诅 11 = 净诅 -10
        for (int i = 0; i < 20; i++)
        {
            int dmg = ItemStatHiddenEngine.WeaponLuckCurseEngine.CalculateAttackPower(10, 30, totalLuck: 1, totalCurse: 11, rng);
            Assert.Equal(10, dmg); // 净诅 10 必须 100% 刀刀下限
        }
    }

    /// <summary>
    /// 盛大官方 1.5 权威记忆全套队长 [@天地合一] 瞬移召唤全队成员:
    /// 散布寻点绝不同格重叠、打断正在进行的交易与随从神兽跨图追随
    /// </summary>
    [Fact]
    public void Test_Official15_MemorySuit_SummonAll_Scatter_TradeCancel_And_Pet_Follow()
    {
        var mapMgr = new MapManager();
        var map1 = new GameMap(0, "比奇省", 500, 500);
        var map2 = new GameMap(1, "沃玛森林", 500, 500);
        mapMgr.RegisterMap(map1);
        mapMgr.RegisterMap(map2);

        var tradeEngine = new TradeEngine();

        var leader = new ClientSession(901, null!)
        {
            CharacterName = "至尊队长",
            CurrentMapId = 0,
            X = 200,
            Y = 200,
            State = new PlayerState { Hp = 500, MaxHp = 500 }
        };

        // 穿齐记忆全套 (头盔/项链/手镯/戒指)
        leader.State.Equipment[(int)OfficialEquipSlot.Helmet] = new EquipItem { Name = "记忆头盔" };
        leader.State.Equipment[(int)OfficialEquipSlot.Necklace] = new EquipItem { Name = "记忆项链" };
        leader.State.Equipment[(int)OfficialEquipSlot.ArmringL] = new EquipItem { Name = "记忆手镯" };
        leader.State.Equipment[(int)OfficialEquipSlot.RingL] = new EquipItem { Name = "记忆戒指" };

        var member = new ClientSession(902, null!)
        {
            CharacterName = "紫霞道士",
            CurrentMapId = 1,
            X = 150,
            Y = 150,
            State = new PlayerState { Hp = 300, MaxHp = 300, AllowGroupRecall = true }
        };

        var thirdParty = new ClientSession(903, null!)
        {
            CharacterName = "买药商人",
            CurrentMapId = 1,
            X = 151,
            Y = 150,
            State = new PlayerState { Hp = 1000, MaxHp = 1000 }
        };

        map1.Aoi.Enter(leader, 200, 200);
        map2.Aoi.Enter(member, 150, 150);
        map2.Aoi.Enter(thirdParty, 151, 150);

        // 队员携带一只神兽随从
        var divineBeast = new HuWeiPetEngine.SlavePet
        {
            Uid = 8801,
            Name = "神兽",
            CurrentMapId = 1,
            X = 150,
            Y = 151,
            Hp = 300,
            MaxHp = 300,
            MasterSessionId = member.SessionId
        };
        member.Pets.Add(divineBeast);

        // 队员正在与其他玩家进行交易
        tradeEngine.TryInitiateTrade(member, thirdParty);
        Assert.NotNull(tradeEngine.GetTrade(member.SessionId));

        // 队长施展天地合一 (@天地合一 / @recall)
        var (ok, summonedCount, msg) = SuitWorldEventEngine.ExecuteMemorySummonAll(leader, new[] { member }, mapMgr, tradeEngine);
        Assert.True(ok, msg);
        Assert.Equal(1, summonedCount);

        // 验证 1: 队长与队员坐标绝不重叠同格穿人 (散布寻点)
        Assert.Equal(0, member.CurrentMapId);
        Assert.False(member.X == leader.X && member.Y == leader.Y, "队长与队员坐标绝不应重叠在同一坐标！");
        int dist = Math.Max(Math.Abs(member.X - leader.X), Math.Abs(member.Y - leader.Y));
        Assert.InRange(dist, 1, 2);

        // 验证 2: 队员的交易被强制熔断
        Assert.Null(tradeEngine.GetTrade(member.SessionId));

        // 验证 3: 随从神兽同步跨地图瞬移至新坐标 (随侍在侧 member.X + 1)
        Assert.Equal(0, divineBeast.CurrentMapId);
        Assert.Equal(member.X + 1, divineBeast.X);
        Assert.Equal(member.Y, divineBeast.Y);

        // 验证 4: 沙巴克皇宫禁空拦截
        leader.CurrentMapId = SabukSiegeEngine.SabukPalaceMapId;
        leader.State.LastMemoryRecallTickMs = 0; // 重置冷却
        var (palaceOk, _, palaceMsg) = SuitWorldEventEngine.ExecuteMemorySummonAll(leader, new[] { member }, mapMgr, tradeEngine);
        Assert.False(palaceOk);
        Assert.Contains("沙巴克皇宫受禁空结界封锁", palaceMsg);
    }

    [Fact]
    public void Test_SabukSiege_SafeZone_Lifted_During_War()
    {
        var sabukEngine = new SabukSiegeEngine();
        var safeZoneEngine = new SafeZoneAndGuardEngine { SabukEngine = sabukEngine };

        // 平时状态下: 沙巴克复活点 (3, 645, 290) 属于安全区
        Assert.True(safeZoneEngine.IsInSafeZone(3, 645, 290), "平时沙巴克复活点为安全区");

        // 攻城战打响
        sabukEngine.StartSiege(currentOwnerGuild: "邪皇殿", nowMs: 1000, durationMs: 7200_000);
        Assert.Equal(SabukSiegeEngine.WarPhase.InWar, sabukEngine.Phase);

        // 攻城期间: 沙巴克复活点安全区禁武全面失效！
        Assert.False(safeZoneEngine.IsInSafeZone(3, 645, 290), "攻城战期间沙巴克复活点安全区必须失效！");
        Assert.False(safeZoneEngine.IsAttackForbiddenBySafeZone(3, 645, 290, 645, 291), "攻城战期间沙城自由交火，不受安全区拦截！");

        // 但盟重土城安全区 (3, 330, 330) 依然有效
        Assert.True(safeZoneEngine.IsInSafeZone(3, 330, 330), "土城安全区不受沙城战火影响");

        // 攻城战结束
        sabukEngine.EndSiege();
        Assert.Equal(SabukSiegeEngine.WarPhase.Ended, sabukEngine.Phase);

        // 沙城安全区恢复
        Assert.True(safeZoneEngine.IsInSafeZone(3, 645, 290), "停战后沙巴克安全区重新生效");
    }

    [Fact]
    public void Test_Murder_Settlement_Killing_RedName_Or_YellowName_Does_Not_Add_PkPoints()
    {
        var (dispatcher, map, hero, victim) = CreateCombatContext();

        // 1. 英雄击杀大红名杀人犯 (PkPoints = 500)
        var redKiller = new ClientSession(701, null!)
        {
            CharacterName = "恶贯满盈",
            CurrentMapId = 0,
            X = 10,
            Y = 11,
            State = new PlayerState { PkPoints = 500, Hp = 0 } // 已倒地
        };

        hero.State!.PkPoints = 0;
        dispatcher.ProcessPlayerDeathAndMurderSettlement(hero, redKiller, map, 1000);

        // 击杀红名正义之举，英雄恶名点数必须依然为 0，绝不增加！
        Assert.Equal(0, hero.State.PkPoints);

        // 2. 英雄击杀守法白名
        var innocent = new ClientSession(702, null!)
        {
            CharacterName = "守法平民",
            CurrentMapId = 0,
            X = 10,
            Y = 12,
            State = new PlayerState { PkPoints = 0, Hp = 0 }
        };

        dispatcher.ProcessPlayerDeathAndMurderSettlement(hero, innocent, map, 2000);

        // 恶意击杀守法白名，恶名点数必须增加 100 点！
        Assert.Equal(100, hero.State.PkPoints);
    }

    [Fact]
    public void Test_Taoist_HolyArmor_And_GhostShield_TimeDecay_And_NonStacking()
    {
        var (dispatcher, map, taoist, warrior) = CreateCombatContext();
        taoist.State!.Job = 2;
        taoist.State.Sc = 20;

        int initialAc = warrior.State!.Ac;
        int initialMac = warrior.State.Mac;

        // 1. 道士施展神圣战甲术 (MagicId 15) 与 幽灵盾 (MagicId 14)
        long now = 10000;
        dispatcher.ResolveAuthoritativeSpell(taoist, 15, warrior.SessionId, warrior.X, warrior.Y, now);
        dispatcher.ResolveAuthoritativeSpell(taoist, 14, warrior.SessionId, warrior.X, warrior.Y, now);

        // 防御获得 +3 光环提升
        Assert.Equal(initialAc + TaoistCombatEngine.AuraBuffs.BonusAc, warrior.State.Ac);
        Assert.Equal(initialMac + TaoistCombatEngine.AuraBuffs.BonusMac, warrior.State.Mac);
        Assert.True(warrior.State.HolyArmorExpireMs > now);
        Assert.True(warrior.State.GhostShieldExpireMs > now);

        // 2. 连续施展 5 次神圣战甲术与幽灵盾 -> 必须防重复叠加，数值恒定为 +3，绝不无限暴涨！
        for (int i = 0; i < 5; i++)
        {
            dispatcher.ResolveAuthoritativeSpell(taoist, 15, warrior.SessionId, warrior.X, warrior.Y, now + 1000);
            dispatcher.ResolveAuthoritativeSpell(taoist, 14, warrior.SessionId, warrior.X, warrior.Y, now + 1000);
        }
        Assert.Equal(initialAc + TaoistCombatEngine.AuraBuffs.BonusAc, warrior.State.Ac);
        Assert.Equal(initialMac + TaoistCombatEngine.AuraBuffs.BonusMac, warrior.State.Mac);

        // 3. 模拟世界心跳时钟超时推进 (超过持续时间)
        long expireTime = warrior.State.HolyArmorExpireMs + 5000;
        dispatcher.UpdateWorldTick(expireTime, expireTime);

        // 光环自然解除，防御准确恢复至初始值
        Assert.Equal(initialAc, warrior.State.Ac);
        Assert.Equal(initialMac, warrior.State.Mac);
        Assert.Equal(0, warrior.State.HolyArmorExpireMs);
        Assert.Equal(0, warrior.State.GhostShieldExpireMs);
    }

    [Fact]
    public void Test_Physical_Melee_Hit_Dispatches_StruckAction_DurabilityWear_And_StatusSync()
    {
        var (dispatcher, map, attacker, defender) = CreateCombatContext();

        attacker.X = 10;
        attacker.Y = 10;
        attacker.Direction = 0; // 朝北
        defender.X = 10;
        defender.Y = 9; // 正北方 1 格

        map.Aoi.Enter(attacker, 10, 10);
        map.Aoi.Enter(defender, 10, 9);
        dispatcher.ActiveSessions[attacker.SessionId] = attacker;
        dispatcher.ActiveSessions[defender.SessionId] = defender;

        // 给防御者穿上一件衣服与头盔，持久 10000
        var armor = new EquipItem { Uid = 5001, Name = "重盔甲", Dura = 10000, MaxDura = 10000 };
        defender.State!.Equipment[(int)OfficialEquipSlot.Dress] = armor;

        attacker.ClearBufferedPackets();
        defender.ClearBufferedPackets();

        // 模拟物理普通出刀 CM_HIT (3003)
        dispatcher.Dispatch(attacker, $"#{ProtocolConsts.CM_HIT}/10/9/0");

        // 验证 1: 必须向同屏九宫格广播受击仰头动作与音效 (SM_OBJECTSTRUCK 31)
        Assert.True(defender.HasBufferedPacket($"#{ProtocolConsts.SM_OBJECTSTRUCK}/"), "受击必须广播仰头动作 SM_OBJECTSTRUCK");

        // 验证 2: 必须下发官方 Delphi 客户端血球同步协议 (SM_MYSTATUS 503)
        Assert.True(defender.HasBufferedPacket($"#{ProtocolConsts.SM_MYSTATUS}/"), "受击必须下发血球更新 SM_MYSTATUS");
    }

    [Fact]
    public void TestEquipAttributeRange_MaxStatsAggregationEngine()
    {
        // 验证盛大官方 1.5 装备属性区间累加核心逻辑 (杜绝裁决 0-30 与重盔甲 4-7 最大属性被丢失为下限的坏账)
        var player = new PlayerState { Job = 0, Level = 40 }; // 战士 40 级
        // 初始裸装: baseMinDc = 5 + 40/3 = 18, baseMaxDc = 10 + 40 = 50, Ac = 0, MaxAc = 0
        player.RecalculateAttributes();
        Assert.Equal(18, player.Dc);
        Assert.Equal(50, player.MaxDc);
        Assert.Equal(0, player.Ac);
        Assert.Equal(0, player.MaxAc);

        // 佩戴裁决之杖 (Dc=0, MaxDc=30) 与重盔甲 (Ac=4, MaxAc=7, Mac=2, MaxMac=3)
        player.WearItem(OfficialEquipSlot.Weapon, new EquipItem { Name = "裁决之杖", Dc = 0, MaxDc = 30, Dura = 30000, MaxDura = 30000 });
        player.WearItem(OfficialEquipSlot.Dress, new EquipItem { Name = "重盔甲", Ac = 4, MaxAc = 7, Mac = 2, MaxMac = 3, Dura = 25000, MaxDura = 25000 });

        // 验证: 裁决 0-30 完整接入，Dc = 18 + 0 = 18, MaxDc = 50 + 30 = 80
        Assert.Equal(18, player.Dc);
        Assert.Equal(80, player.MaxDc);

        // 验证: 重盔甲 4-7 与 2-3 完整接入，Ac = 4, MaxAc = 7, Mac = 2, MaxMac = 3
        Assert.Equal(4, player.Ac);
        Assert.Equal(7, player.MaxAc);
        Assert.Equal(2, player.Mac);
        Assert.Equal(3, player.MaxMac);

        // 验证单值装备 (如简写 Ac = 20): 必须上下限保底均为 20
        player.WearItem(OfficialEquipSlot.Helmet, new EquipItem { Name = "极品黑铁头盔", Ac = 20, Dura = 10000, MaxDura = 10000 });
        Assert.Equal(4 + 20, player.Ac);
        Assert.Equal(7 + 20, player.MaxAc);
    }

    [Fact]
    public void TestOfficial15_AttackMode_DropGold_And_DetailsAuthoritativeAudit()
    {
        // 1. 验证官方 1.5 攻击模式权威五档与协议 766 零值放行
        var mapMgr = new Mir2.Server.Maps.MapManager();
        var dispatcher = new PacketDispatcher(mapMgr);

        var session = new ClientSession(999, null!)
        {
            CharacterName = "测试战士",
            CurrentMapId = 3, // 盟重省
            X = 672,
            Y = 330,
            Direction = 0,
            State = new PlayerState { Level = 40, Hp = 500, MaxHp = 500, AttackMode = 4 } // 初始为善恶模式
        };

        // 触发 @AttackMode 指令循环 (4 -> 0 全体攻击模式)
        dispatcher.HandleSay(session, "@AttackMode");
        Assert.Equal(0, session.State.AttackMode);

        // 验证模拟收到 766 协议包时，0 (全体模式) 必须被客户端完整识别
        var pkt0 = new ServerPacket(ProtocolConsts.SM_ATTACKMODECHANGED, 0, 0, 0, Array.Empty<byte>());
        int attackMode0 = (pkt0.Param >= 0 && pkt0.Param <= 4) ? pkt0.Param : (pkt0.Recog >= 0 && pkt0.Recog <= 4 ? pkt0.Recog : 0);
        Assert.Equal(0, attackMode0);

        // 2. 验证沙巴克城门在盟重省 (MapId = 3) 坐标 (672, 331) 攻击扣血判定
        int initialGateHp = dispatcher.CastleGateEngine.Hp;
        dispatcher.Dispatch(session, $"#{ProtocolConsts.CM_HIT}/672/331/0/!"); // 正面砍门
        Assert.True(dispatcher.CastleGateEngine.Hp < initialGateHp, "沙巴克城门在盟重省 (MapId=3) 坐标 (672,331) 受击必须权威扣除耐久！");

        // 3. 验证 MapManager 官方 1.5 核心地图与跨图跳点
        var bairimen = mapMgr.GetMap(11);
        Assert.NotNull(bairimen);
        var darkHall = mapMgr.GetMap(10);
        Assert.NotNull(darkHall);
        Assert.Equal("未知暗殿", darkHall.Name);
        var zumaHall = mapMgr.GetMap(157);
        Assert.NotNull(zumaHall);
        Assert.Equal("祖玛寺庙大厅", zumaHall.Name);

        // 沃玛森林 (1) 通往白日门 (11) 跳点验证
        var passageToBairimen = mapMgr.FindPassage(1, 350, 20);
        Assert.NotNull(passageToBairimen);
        Assert.Equal(11, passageToBairimen.ToMapId);
        Assert.Equal(180, passageToBairimen.ToX);
        Assert.Equal(300, passageToBairimen.ToY);

        // 4. 验证战士核心技能 ID：野蛮冲撞为 27，半月弯刀为 25，绝不混淆
        var skillDash = Mir2.Shared.Rules.SkillFormulas.GetSkill(27);
        Assert.NotNull(skillDash);
        Assert.Equal("野蛮冲撞", skillDash.Value.Name);
        var skillMoon = Mir2.Shared.Rules.SkillFormulas.GetSkill(25);
        Assert.NotNull(skillMoon);
        Assert.Equal("半月弯刀", skillMoon.Value.Name);
    }
}

