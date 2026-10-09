using System;
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
        int testPort = 17010;
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
}
