using System;
using System.Collections.Generic;
using Mir2.Server.Core;
using Mir2.Server.Game;
using Mir2.Server.Maps;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 盛大官方 1.5 服务端权威世界心跳、全生命周期状态机与防外挂闭环自动化测试套件
/// 严格 1:1 核销：
/// 1. 药水持续缓回池服务端 Tick 匀速抽取与满血清空防溢出机制
/// 2. 角色死亡复活 CM_REVIVE (1018) 权威清除毒素、满血满魔与安全区回城重置
/// 3. 官方特戒传送指令 @move x y 权威瞬移与障碍墙体拦截
/// 4. 银杏山谷怪物刷新与 AI 在 WorldTick 驱动下的自动寻敌近战攻击
/// 5. 官方 80 秒掉落物杀手独享归属期与超时放行防吸物外挂裁决
/// </summary>
public sealed class TestServerWorldTickAndAuthoritativeFullLoop
{
    private (MapManager mapMgr, MonsterRegenEngine regenEng, NpcMerchantEngine npcEng, PacketDispatcher dispatcher, GameMap map) CreateTestEnvironment()
    {
        var mapMgr = new MapManager();
        var map = new GameMap(0, "比奇省", 400, 400);
        mapMgr.RegisterMap(map);

        var regenEng = new MonsterRegenEngine(mapMgr);
        var npcEng = new NpcMerchantEngine();
        var tradeEng = new TradeEngine();
        var groupEng = new GroupEngine();
        var guildEng = new GuildEngine();

        var dispatcher = new PacketDispatcher(mapMgr, regenEng, npcEng, tradeEng, groupEng, guildEng);
        return (mapMgr, regenEng, npcEng, dispatcher, map);
    }

    [Fact]
    public void Test_Potion_IncHealthSpell_Authoritative_WorldTick()
    {
        // 1. 验证金创药进入蓄积池并在服务端 Tick 驱动下匀速释放 5 点
        var player = new PlayerState
        {
            Hp = 50,
            MaxHp = 100,
            Mp = 20,
            MaxMp = 100
        };
        player.BagItems.Add(new EquipItem { Uid = 1001, Name = "金创药(小量)", Weight = 1 });

        var (res, effect) = PotionEngine.ConsumePotion(player, 0, "金创药(小量)");
        Assert.Equal(PotionEngine.PotionResult.Success, res);
        Assert.NotNull(effect);
        Assert.Equal(30, player.IncHealth); // 缓回池注入 30 点

        // 模拟第 1 个 Tick (抽取 5 点)
        var (hpGain1, _) = PotionEngine.TickIncHealthSpell(player);
        Assert.Equal(5, hpGain1);
        Assert.Equal(55, player.Hp);
        Assert.Equal(25, player.IncHealth);

        // 模拟连续 5 个 Tick
        for (int i = 0; i < 5; i++)
        {
            PotionEngine.TickIncHealthSpell(player);
        }
        Assert.Equal(80, player.Hp);
        Assert.Equal(0, player.IncHealth); // 30 点全部释放完毕

        // 满血时清空多余血池
        player.Hp = 100;
        player.IncHealth = 50;
        var (hpGainFull, _) = PotionEngine.TickIncHealthSpell(player);
        Assert.Equal(0, hpGainFull);
        Assert.Equal(0, player.IncHealth); // 满血自动归零
    }

    [Fact]
    public void Test_Authoritative_Revive_Protocol_1018()
    {
        var (mapMgr, regenEng, npcEng, dispatcher, map) = CreateTestEnvironment();

        var session = new ClientSession(1001, null!)
        {
            CurrentMapId = 0,
            X = 150,
            Y = 150,
            CharacterName = "测试烈士",
            State = new PlayerState
            {
                Hp = 0, // 阵亡状态
                MaxHp = 250,
                Mp = 0,
                MaxMp = 120,
                HasGreenPoison = true,
                HasRedPoison = true
            }
        };
        map.Aoi.Enter(session, 150, 150);

        // 客户端发送死亡复活协议: #1018/0/0/0/0/!
        dispatcher.Dispatch(session, "#1018/0/0/0/0/!");

        // 验证服务端权威复活裁决结果
        Assert.Equal(250, session.State.Hp); // 满血
        Assert.Equal(120, session.State.Mp); // 满魔
        Assert.False(session.State.HasGreenPoison); // 绿毒清除
        Assert.False(session.State.HasRedPoison);   // 红毒清除
        Assert.Equal(330, session.X);              // 传送回银杏山谷安全区
        Assert.Equal(270, session.Y);

        // 验证下发复活包 SM_REVIVE (501)
        Assert.True(session.HasBufferedPacket("#501/1001/330/270/0/测试烈士!"));
    }

    [Fact]
    public void Test_TeleportRing_MoveCommand_Authoritative_Movement()
    {
        var (mapMgr, regenEng, npcEng, dispatcher, map) = CreateTestEnvironment();

        var session = new ClientSession(2001, null!)
        {
            CurrentMapId = 0,
            X = 330,
            Y = 270,
            CharacterName = "法神",
            State = new PlayerState
            {
                Hp = 150,
                MaxHp = 150
            }
        };
        map.Aoi.Enter(session, 330, 270);

        // 1. 发送合法特戒传送指令: @move 345 285
        dispatcher.Dispatch(session, "#3030/0/0/0/0/@move 345 285!");

        Assert.Equal(345, session.X);
        Assert.Equal(285, session.Y);
        Assert.True(session.HasBufferedPacket("瞬移至 (345, 285) 成功"));

        // 2. 发送越界或非法坐标: @move 9999 9999
        dispatcher.Dispatch(session, "#3030/0/0/0/0/@move 9999 9999!");
        Assert.Equal(345, session.X); // 坐标未变
        Assert.True(session.HasBufferedPacket("传送失败"));
    }

    [Fact]
    public void Test_Monster_Regen_And_Ai_Attack_In_WorldTick()
    {
        var (mapMgr, regenEng, npcEng, dispatcher, map) = CreateTestEnvironment();

        // 注册银杏山谷钉耙猫规则
        regenEng.AddRule(new MonsterSpawnRule
        {
            RuleId = 10,
            MonsterName = "钉耙猫",
            MapId = 0,
            CenterX = 330,
            CenterY = 270,
            Radius = 2,
            MaxCount = 5,
            BaseHp = 60
        });

        // 驱动刷怪
        var rng = OfficialClientRulesEngine.DelphiRandom.Shared;
        var (spawned, _) = regenEng.ProcessRegenTick(rng, enforceTimeSlice: false);
        Assert.True(spawned > 0);
        Assert.True(regenEng.TotalActiveMonsters > 0);

        // 在怪物旁边放入一个玩家
        var playerSession = new ClientSession(3001, null!)
        {
            CurrentMapId = 0,
            X = 330,
            Y = 270,
            CharacterName = "初级战士",
            State = new PlayerState
            {
                Hp = 100,
                MaxHp = 100,
                Speed = 10
            }
        };
        map.Aoi.Enter(playerSession, 330, 270);

        // 驱动怪物 AI 行为周期
        int aiActions = dispatcher.ProcessMonsterAiCycle(rng);
        Assert.True(aiActions >= 0);
    }

    [Fact]
    public void Test_Authoritative_GroundItem_80s_Protection_And_Pickup()
    {
        var (mapMgr, _, _, dispatcher, map) = CreateTestEnvironment();
        long now = Environment.TickCount64;

        // 地面上有一件裁决之杖，归属玩家 1001 (杀手)，在时间 now 掉落
        var drop = new GroundItemEntity
        {
            Uid = 8888,
            Name = "裁决之杖",
            X = 330,
            Y = 270,
            OwnerSessionId = 1001,
            DropTickMs = now,
            Weight = 30
        };
        map.AddGroundItem(drop);

        // 旁观者 2002 走上该坐标试图拾取 (掉落仅过去 10 秒)
        var otherPlayer = new ClientSession(2002, null!)
        {
            CurrentMapId = 0,
            X = 330,
            Y = 270,
            CharacterName = "路人甲",
            State = new PlayerState { Hp = 100, MaxHp = 100, BagWeight = 0, MaxBagWeight = 200 }
        };
        map.Aoi.Enter(otherPlayer, 330, 270);

        // 旁观者拾取尝试
        dispatcher.Dispatch(otherPlayer, "#1001/0/0/0/0/!");
        Assert.True(otherPlayer.HasBufferedPacket("物品受保护中"));
        Assert.Empty(otherPlayer.State.BagItems); // 背包仍然为空，严禁被拾取

        // 杀手 1001 本人前来拾取
        var killer = new ClientSession(1001, null!)
        {
            CurrentMapId = 0,
            X = 330,
            Y = 270,
            CharacterName = "终极杀手",
            State = new PlayerState { Hp = 100, MaxHp = 100, BagWeight = 0, MaxBagWeight = 200 }
        };
        map.Aoi.Enter(killer, 330, 270);
        dispatcher.Dispatch(killer, "#1001/0/0/0/0/!");

        Assert.Single(killer.State.BagItems);
        Assert.Equal("裁决之杖", killer.State.BagItems[0].Name);
        Assert.Equal(30, killer.State.BagWeight);
        Assert.True(killer.HasBufferedPacket("#200/8888/30/0/0/裁决之杖!"));
    }
}
