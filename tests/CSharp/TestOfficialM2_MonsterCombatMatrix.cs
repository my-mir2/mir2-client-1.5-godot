using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mir2.Server.Core;
using Mir2.Server.Game;
using Mir2.Server.Maps;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests;

public class TestOfficialM2_MonsterCombatMatrix
{
    [Fact]
    public void TestOfficialM2_Monster_SpotPlayer_And_Approach()
    {
        var mapManager = new MapManager();
        var regen = new MonsterRegenEngine(mapManager);

        // 刷新一只红野猪在 (150, 150)
        regen.AddRule(new MonsterSpawnRule
        {
            RuleId = 1,
            MonsterName = "红野猪",
            MapId = 0,
            CenterX = 150,
            CenterY = 150,
            Radius = 0,
            MaxCount = 1,
            BaseHp = 320
        });

        var rng = new OfficialClientRulesEngine.DelphiRandom(123);
        regen.ProcessRegenTick(rng);

        var pig = regen.GetMonster(100_001);
        Assert.NotNull(pig);
        Assert.Equal(150, pig.X);
        Assert.Equal(150, pig.Y);

        // 模拟玩家处于 (150, 153)，距离 3 格 (在红野猪 5 格视野内)
        var playerSession = new ClientSession(1, null!)
        {
            CurrentMapId = 0,
            X = 150,
            Y = 153,
            State = new PlayerState { Hp = 200, MaxHp = 200 }
        };

        // 驱动怪物 AI 心跳
        var (actions, _) = regen.ProcessMonsterAiTick((mapId, x, y) => new System.Collections.Generic.List<ClientSession> { playerSession }, rng);

        Assert.Single(actions);
        var act = actions[0];
        Assert.Equal(0, act.ActionType); // 追击步进
        Assert.Equal(150, act.X);
        Assert.Equal(151, act.Y); // 向 Y+ 方向移动了 1 格，逼近玩家！
        Assert.Equal(4, act.Direction); // 面向下
    }

    [Fact]
    public void TestOfficialM2_Monster_Adjacent_Melee_Attack_Inflicts_Damage()
    {
        var mapManager = new MapManager();
        var regen = new MonsterRegenEngine(mapManager);

        regen.AddRule(new MonsterSpawnRule
        {
            RuleId = 2,
            MonsterName = "黑野猪",
            MapId = 0,
            CenterX = 160,
            CenterY = 160,
            Radius = 0,
            MaxCount = 1,
            BaseHp = 380
        });

        var rng = new OfficialClientRulesEngine.DelphiRandom(888);
        regen.ProcessRegenTick(rng);

        var pig = regen.GetMonster(100_001);
        Assert.NotNull(pig);

        // 玩家紧邻黑野猪: 处于 (161, 160)，切比雪夫距离为 1 格
        var playerSession = new ClientSession(2, null!)
        {
            CurrentMapId = 0,
            X = 161,
            Y = 160,
            State = new PlayerState { Hp = 100, MaxHp = 100 }
        };

        // 驱动怪物 AI 心跳
        var (actions, _) = regen.ProcessMonsterAiTick((mapId, x, y) => new System.Collections.Generic.List<ClientSession> { playerSession }, rng);

        Assert.Single(actions);
        var act = actions[0];
        Assert.Equal(1, act.ActionType); // 近战攻击
        Assert.Equal(2, act.Direction); // 面向右 (2)
        Assert.True(act.DamageDealt >= pig.AttackDamageMin && act.DamageDealt <= pig.AttackDamageMax);
        Assert.Equal(100 - act.DamageDealt, playerSession.State.Hp); // 玩家血量被实打实扣减！
    }

    [Fact]
    public async Task TestE2E_FullCycle_PlayerKillsMonster_SpawnsLoot_And_PicksUp()
    {
        int testPort = 29010;
        var mapManager = new MapManager();
        var regen = new MonsterRegenEngine(mapManager);

        // 在比奇省 (250, 250) 刷新一只残血半兽战士
        regen.AddRule(new MonsterSpawnRule
        {
            RuleId = 99,
            MonsterName = "半兽战士",
            MapId = 0,
            CenterX = 250,
            CenterY = 250,
            Radius = 0,
            MaxCount = 1,
            BaseHp = 10 // 仅 10 点血，一刀必死
        });

        var rng = new OfficialClientRulesEngine.DelphiRandom(999);
        regen.ProcessRegenTick(rng);

        var dispatcher = new PacketDispatcher(mapManager, regen);
        var gateway = new TcpGateway(testPort);
        gateway.PacketReceivedHandler = (session, pkt) => dispatcher.Dispatch(session, pkt);
        gateway.Start();

        var tickEngine = new GameWorldTick(gateway);
        tickEngine.Start();

        try
        {
            // 勇士登入: 位于 (249, 250) 面向右侧的怪物
            using var client = new System.Net.Sockets.TcpClient();
            await client.ConnectAsync("127.0.0.1", testPort);
            var stream = client.GetStream();
            await stream.WriteAsync(Encoding.UTF8.GetBytes("#103/传奇勇士/249/250!"));
            await stream.FlushAsync();

            await Task.Delay(200);

            // 获取服务器上的会话，确保准确度为 100 必中
            var map = mapManager.GetMap(0);
            Assert.NotNull(map);
            var buffer = new System.Collections.Generic.List<ClientSession>();
            map.Aoi.GetEntitiesInNinePatch(250, 250, buffer);
            var session = buffer.Find(s => s.CharacterName == "传奇勇士");
            Assert.NotNull(session);
            session.State!.Hit = 100;

            byte[] flushBuf = new byte[4096];
            while (stream.DataAvailable) _ = await stream.ReadAsync(flushBuf, 0, flushBuf.Length);

            // 1. 勇士向怪物 (250, 250) 挥刀攻击 CM_HIT (3014)
            byte[] hitPkt = Encoding.UTF8.GetBytes("#3014/250/250/2!");
            await stream.WriteAsync(hitPkt);
            await stream.FlushAsync();

            await Task.Delay(200);

            // 勇士必须收到怪物受击、怪物死亡倒地与掉落地表战利品的广播
            Assert.True(stream.DataAvailable, "击杀怪物必须收到全套战斗与掉落广播");
            byte[] buf1 = new byte[4096];
            int len1 = await stream.ReadAsync(buf1, 0, buf1.Length);
            string resp1 = Encoding.UTF8.GetString(buf1, 0, len1);

            Assert.Contains($"#{ProtocolConsts.SM_OBJECTSTRUCK}/", resp1); // 怪物受击
            Assert.Contains($"#{ProtocolConsts.SM_DEATH_ACTION_DELPHI}/", resp1); // 怪物死亡倒地
            Assert.Contains($"#{ProtocolConsts.SM_GROUNDITEM}/", resp1); // 掉落战利品
            Assert.Contains("半兽战士_战利品", resp1);

            // 验证怪物实例已被销毁并移出活动列表
            Assert.Equal(0, regen.TotalActiveMonsters);

            // 2. 勇士走向掉落物位置 (250, 250)
            session.X = 250;
            session.Y = 250;
            while (stream.DataAvailable) _ = await stream.ReadAsync(flushBuf, 0, flushBuf.Length);

            // 3. 勇士发送拾取指令 CM_PICKUP (1001)
            byte[] pickupPkt = Encoding.UTF8.GetBytes("#1001/!");
            await stream.WriteAsync(pickupPkt);
            await stream.FlushAsync();

            await Task.Delay(200);

            // 验证勇士收到物品入包 SM_ADDITEM (200) 与地表物品消除 SM_DELGROUNDITEM (315)
            Assert.True(stream.DataAvailable, "拾取战利品必须收到加物品与清除广播");
            byte[] buf2 = new byte[4096];
            int len2 = await stream.ReadAsync(buf2, 0, buf2.Length);
            string resp2 = Encoding.UTF8.GetString(buf2, 0, len2);

            Assert.Contains($"#{ProtocolConsts.SM_ADDITEM}/", resp2);
            Assert.Contains("半兽战士_战利品", resp2);
            Assert.Contains($"#{ProtocolConsts.SM_DELGROUNDITEM}/", resp2);

            // 验证勇士背包中真实存在该物品
            Assert.Single(session.State.BagItems);
            Assert.Equal("半兽战士_战利品", session.State.BagItems[0].Name);
        }
        finally
        {
            tickEngine.Stop();
            gateway.Stop();
        }
    }

    [Fact]
    public void Test_MonsterThreatTable_LockAndSwitchAndDeAggro()
    {
        var threat = new MonsterThreatTable();
        Assert.False(threat.HasTarget);

        // 1. 玩家 1 造成 100 伤害，锁定为第一仇恨
        threat.AddHate(1, 100);
        Assert.Equal(1, threat.PrimaryTargetId);
        Assert.Equal(100, threat.MaxHate);

        // 2. 玩家 2 造成 110 伤害 (仅为 1.1 倍，未达到 1.2 倍门槛)，不发生换仇！
        threat.AddHate(2, 110);
        Assert.Equal(1, threat.PrimaryTargetId); // 依然锁定玩家 1

        // 3. 玩家 2 追加 20 伤害 (累计 130 伤害，达到 100 的 1.3 倍 > 1.2 倍)，成功换仇！
        threat.AddHate(2, 20);
        Assert.Equal(2, threat.PrimaryTargetId); // 切换锁定玩家 2

        // 4. 道士神兽 99 号造成 40 伤害并附带嘲讽 (40 * 2 + 50 = 130 嘲讽仇恨，总仇恨 260)，强力转火神兽！
        threat.AddHate(99, 40, isTaunt: true);
        Assert.Equal(99, threat.PrimaryTargetId);

        // 5. 目标脱仇 (例如神兽死亡或消失)，回退锁定剩余最高仇恨目标 (玩家 2)
        threat.RemoveTarget(99);
        Assert.Equal(2, threat.PrimaryTargetId);
    }

    [Fact]
    public void Test_ZumaStoneForm_AwakenOnApproachOrHit()
    {
        var mapManager = new MapManager();
        var regen = new MonsterRegenEngine(mapManager);

        // 刷新一只处于初始石化状态的祖玛卫士在 (120, 120)
        regen.AddRule(new MonsterSpawnRule
        {
            RuleId = 10,
            MonsterName = "祖玛卫士",
            MapId = 0,
            CenterX = 120,
            CenterY = 120,
            Radius = 0,
            MaxCount = 1,
            BaseHp = 1000
        });

        var rng = new OfficialClientRulesEngine.DelphiRandom(111);
        regen.ProcessRegenTick(rng);

        var zuma = regen.FindMonsterAt(0, 120, 120);
        Assert.NotNull(zuma);
        Assert.True(zuma.IsStoneForm, "祖玛系怪物初始必须处于石化雕像状态");

        // 玩家处于 5 格之外 (120, 125)
        var farPlayer = new ClientSession(1, null!)
        {
            CurrentMapId = 0,
            X = 120,
            Y = 125,
            State = new PlayerState { Hp = 300 }
        };

        var (actions1, _) = regen.ProcessMonsterAiTick((m, x, y) => new List<ClientSession> { farPlayer }, rng);
        Assert.Empty(actions1); // 石化状态且未达 3 格唤醒距离，绝不行动
        Assert.True(zuma.IsStoneForm);

        // 玩家逼近至 3 格内 (120, 123)
        var nearPlayer = new ClientSession(1, null!)
        {
            CurrentMapId = 0,
            X = 120,
            Y = 123,
            State = new PlayerState { Hp = 300 }
        };

        var (actions2, _) = regen.ProcessMonsterAiTick((m, x, y) => new List<ClientSession> { nearPlayer }, rng);
        Assert.Single(actions2);
        Assert.Equal(6, actions2[0].ActionType); // 动作 6: 破石苏醒！
        Assert.False(zuma.IsStoneForm, "接近 3 格后祖玛雕像破石碎裂苏醒");
    }

    [Fact]
    public void Test_PoisonSpider_RangedPoisonAttack()
    {
        var mapManager = new MapManager();
        var regen = new MonsterRegenEngine(mapManager);

        regen.AddRule(new MonsterSpawnRule
        {
            RuleId = 20,
            MonsterName = "毒蜘蛛",
            MapId = 0,
            CenterX = 200,
            CenterY = 200,
            Radius = 0,
            MaxCount = 1,
            BaseHp = 120
        });

        var rng = new OfficialClientRulesEngine.DelphiRandom(222);
        regen.ProcessRegenTick(rng);

        var spider = regen.FindMonsterAt(0, 200, 200);
        Assert.NotNull(spider);

        // 玩家处于 3 格远程位置 (203, 200)，正好在 2~4 格远程喷毒射程内
        var player = new ClientSession(1, null!)
        {
            CurrentMapId = 0,
            X = 203,
            Y = 200,
            State = new PlayerState { Hp = 100, IncHealth = 50 } // 原有药水回血池 50
        };

        var (actions, _) = regen.ProcessMonsterAiTick((m, x, y) => new List<ClientSession> { player }, rng);
        Assert.Single(actions);
        Assert.Equal(3, actions[0].ActionType); // 动作 3: 远程喷毒
        Assert.True(actions[0].DamageDealt > 0);
        Assert.True(player.State.HasGreenPoison, "受到毒蜘蛛喷毒必须附加绿毒状态");
        Assert.Equal(0, player.State.IncHealth); // 官方规则：中绿毒立即清空药水持续回血池！
    }

    [Fact]
    public void Test_MothBat_MeleeParalysis_FreezesPlayer()
    {
        var mapManager = new MapManager();
        var regen = new MonsterRegenEngine(mapManager);

        regen.AddRule(new MonsterSpawnRule
        {
            RuleId = 30,
            MonsterName = "契蛾",
            MapId = 0,
            CenterX = 50,
            CenterY = 50,
            Radius = 0,
            MaxCount = 1,
            BaseHp = 200
        });

        var rng = new OfficialClientRulesEngine.DelphiRandom(333);
        regen.ProcessRegenTick(rng);

        var moth = regen.FindMonsterAt(0, 50, 50);
        Assert.NotNull(moth);

        // 玩家处于贴身 1 格 (51, 50)
        var player = new ClientSession(1, null!)
        {
            CurrentMapId = 0,
            X = 51,
            Y = 50,
            State = new PlayerState { Hp = 200 }
        };

        // 持续心跳直到触发契蛾 15% 几率近战麻痹
        bool paralyzed = false;
        for (int i = 0; i < 50; i++)
        {
            moth.NextActionTimeMs = 0;
            regen.ProcessMonsterAiTick((m, x, y) => new List<ClientSession> { player }, rng);
            if (player.State.IsParalyzed)
            {
                paralyzed = true;
                break;
            }
        }

        Assert.True(paralyzed, "契蛾近战攻击必须有几率附加麻痹石化");
        Assert.True(player.State.StatusBits[5], "麻痹状态位 5 必须置为 true");

        // 处于麻痹状态的玩家尝试移动与出刀，必须被服务端硬性拦截！
        var dispatcher = new PacketDispatcher(mapManager, regen);
        player.EnqueuePacket(ProtocolConsts.SM_LOGON_DELPHI, player.SessionId, 51, 50, 0, "测试勇士");

        // 尝试走路
        dispatcher.Dispatch(player, "#3011/52/50/2/!");
        Assert.True(player.HasBufferedPacket($"#{ProtocolConsts.SM_SENDUSERSTATE}/")); // 原地拉回包

        // 尝试攻击
        dispatcher.Dispatch(player, "#3014/50/50/6/!");
        Assert.True(player.HasBufferedPacket("【石化】你处于麻痹状态，无法攻击！"));
    }

    [Fact]
    public void Test_HouseFly_SpawnsBats_WithCap()
    {
        var mapManager = new MapManager();
        var regen = new MonsterRegenEngine(mapManager);

        regen.AddRule(new MonsterSpawnRule
        {
            RuleId = 40,
            MonsterName = "角蝇",
            MapId = 0,
            CenterX = 300,
            CenterY = 300,
            Radius = 0,
            MaxCount = 1,
            BaseHp = 300
        });

        var rng = new OfficialClientRulesEngine.DelphiRandom(444);
        regen.ProcessRegenTick(rng);

        var fly = regen.FindMonsterAt(0, 300, 300);
        Assert.NotNull(fly);
        Assert.False(fly.CanMove, "角蝇固定原地不可移动");

        var player = new ClientSession(1, null!)
        {
            CurrentMapId = 0,
            X = 302,
            Y = 300,
            State = new PlayerState { Hp = 500 }
        };

        // 驱动心跳吐出小蝙蝠
        fly.NextSpecialSkillTimeMs = 0;
        var (actions, _) = regen.ProcessMonsterAiTick((m, x, y) => new List<ClientSession> { player }, rng);
        Assert.Single(actions);
        Assert.Equal(5, actions[0].ActionType); // 动作 5: 吐怪
        Assert.Equal(1, fly.SpawnedSubMonsterCount);

        // 验证小蝙蝠已被注册到引擎实体池中
        var bat = regen.FindMonsterAt(0, actions[0].X, actions[0].Y);
        Assert.NotNull(bat);
        Assert.Equal("角蝇小蝙蝠", bat.Name);
        Assert.Equal(fly.Uid, bat.ParentMonsterUid);

        // 击杀小蝙蝠，母体角蝇的存活子怪计数必须递减回收
        regen.OnMonsterDied(bat.Uid);
        Assert.Equal(0, fly.SpawnedSubMonsterCount);
    }

    [Fact]
    public void Test_Demon_FullMapSpikeDamage_PiercesInvisibility()
    {
        var mapManager = new MapManager();
        var regen = new MonsterRegenEngine(mapManager);

        regen.AddRule(new MonsterSpawnRule
        {
            RuleId = 50,
            MonsterName = "赤月恶魔",
            MapId = 0,
            CenterX = 500,
            CenterY = 500,
            Radius = 0,
            MaxCount = 1,
            BaseHp = 5000
        });

        var rng = new OfficialClientRulesEngine.DelphiRandom(555);
        regen.ProcessRegenTick(rng);

        var demon = regen.FindMonsterAt(0, 500, 500);
        Assert.NotNull(demon);
        Assert.False(demon.CanMove, "赤月恶魔固定不可移动");
        Assert.True(demon.SeeInvisibility, "赤月恶魔破隐看穿隐身术");

        // 玩家处于 10 格远位置 (510, 500)，并开启隐身术
        var player = new ClientSession(1, null!)
        {
            CurrentMapId = 0,
            X = 510,
            Y = 500,
            State = new PlayerState
            {
                Hp = 500,
                InvisibilityExpireMs = Environment.TickCount64 + 60_000 // 处于隐身
            }
        };
        Assert.True(player.State.IsInvisible);

        demon.NextSpecialSkillTimeMs = 0;
        var (actions, _) = regen.ProcessMonsterAiTick((m, x, y) => new List<ClientSession> { player }, rng);
        Assert.Single(actions);
        Assert.Equal(4, actions[0].ActionType); // 动作 4: 全屏地刺
        Assert.True(player.State.Hp < 500, "赤月恶魔全屏地刺穿刺面伤破隐直击玩家");
    }

    [Fact]
    public void Test_MonItemRules_ParseAndRollDrops_WithScatter()
    {
        string mockDropFile = @"
; 盛大官方白野猪掉落配置示例
1/1 金币
1/2 强效太阳水
1/10 裁决之杖
1/20 龙纹剑
";
        var rules = MonsterDropEngine.ParseMonItemRules(mockDropFile);
        Assert.Equal(4, rules.Count);
        Assert.Equal("金币", rules[0].ItemName);
        Assert.Equal(1, rules[0].Denominator);
        Assert.True(rules[0].IsGold);
        Assert.Equal("裁决之杖", rules[2].ItemName);
        Assert.Equal(10, rules[2].Denominator);

        // 投掷掉落与周边空地散射 (80秒归属权保护)
        var rng = new OfficialClientRulesEngine.DelphiRandom(777);
        long nowMs = Environment.TickCount64;
        var drops = MonsterDropEngine.RollMonsterDrops(150, 150, killerSessionId: 88, nowMs, rules, rng, (x, y) => true);

        Assert.NotEmpty(drops);
        // 金币 1/1 必掉
        var gold = drops.Find(d => d.IsGold);
        Assert.NotNull(gold);
        Assert.Equal(88, gold.OwnerSessionId); // 杀手 80 秒归属权
        Assert.Equal(nowMs, gold.DropTickMs);
    }
}
