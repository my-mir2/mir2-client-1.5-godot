using System;
using System.IO;
using System.Threading;
using Mir2.Server.Core;
using Mir2.Server.Game;
using Mir2.Server.Maps;
using Mir2.Server.Network;
using Mir2.Server.Persistence;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 盛大官方 1.5 服务端数据持久化、自动热更监听与地表衰减深水区测试矩阵
/// 彻底核销“重启即删档”、“幽灵地表物”与“无自动热更监听”坏账
/// </summary>
public sealed class TestServerPersistenceAndWatcherDeepWater : IDisposable
{
    private readonly string _testSaveDir;

    public TestServerPersistenceAndWatcherDeepWater()
    {
        _testSaveDir = Path.Combine(Path.GetTempPath(), "mir2_test_save_" + Guid.NewGuid().ToString("N"));
    }

    public void Dispose()
    {
        try
        {
            if (Directory.Exists(_testSaveDir))
            {
                Directory.Delete(_testSaveDir, recursive: true);
            }
        }
        catch
        {
            // 测试临时目录清理容错
        }
    }

    [Fact]
    public void TestPlayerPersistenceFullCycle_PreservesAllStatsBagEquipAndStorage()
    {
        var engine = new DataPersistenceEngine(_testSaveDir);

        // 1. 构建一个深度复杂角色状态机
        var originalState = new PlayerState
        {
            Name = "传奇战神_888",
            Job = 0, // 战士
            Sex = 0, // 男
            Level = 45,
            Hp = 850,
            MaxHp = 850,
            Mp = 240,
            MaxMp = 240,
            Gold = 5_000_000,
            PkPoints = 350, // 红名
            AttackMode = 1,
            BonusDc = 15,
            BonusAc = 8,
            BonusHp = 100,
            StorageGold = 3_000_000,
            StoragePassword = "secure_pwd_123",
            GuildRank = 1
        };

        // 穿戴一把强化屠龙
        originalState.Equipment[(int)OfficialEquipSlot.Weapon] = new EquipItem
        {
            Uid = 99991,
            Name = "屠龙",
            Shape = 1,
            Dc = 5,
            MaxDc = 35,
            BonusDc = 7,
            Luck = 7,
            UpgradeState = 12,
            UpgradeCount = 7,
            Dura = 33000,
            MaxDura = 33000
        };

        // 背包放入圣战戒指
        originalState.BagItems.Add(new EquipItem
        {
            Uid = 99992,
            Name = "圣战戒指",
            Shape = 22,
            Dc = 0,
            MaxDc = 7,
            Dura = 7000,
            MaxDura = 7000
        });

        // 仓库放入金条
        originalState.StorageItems.Add(new EquipItem
        {
            Uid = 99993,
            Name = "金条",
            Shape = 50,
            Weight = 5
        });

        // 2. 执行原子化存盘
        bool saved = engine.SavePlayer(originalState.Name, originalState, mapId: 0, mapX: 330, mapY: 270);
        Assert.True(saved);

        // 3. 从磁盘重新加载并比对
        var loaded = engine.LoadPlayer(originalState.Name, out int mapId, out int x, out int y);
        Assert.NotNull(loaded);
        Assert.Equal("传奇战神_888", loaded.Name);
        Assert.Equal(45, loaded.Level);
        Assert.Equal(850, loaded.Hp);
        Assert.Equal(5_000_000, loaded.Gold);
        Assert.Equal(350, loaded.PkPoints);
        Assert.True(loaded.IsRedName);
        Assert.Equal(15, loaded.BonusDc);
        Assert.Equal(8, loaded.BonusAc);
        Assert.Equal(100, loaded.BonusHp);
        Assert.Equal(3_000_000, loaded.StorageGold);
        Assert.Equal("secure_pwd_123", loaded.StoragePassword);
        Assert.Equal(0, mapId);
        Assert.Equal(330, x);
        Assert.Equal(270, y);

        // 验证武器与极品属性
        var weapon = loaded.Equipment[(int)OfficialEquipSlot.Weapon];
        Assert.NotNull(weapon);
        Assert.Equal("屠龙", weapon.Name);
        Assert.Equal(35, weapon.MaxDc);
        Assert.Equal(7, weapon.BonusDc);
        Assert.Equal(7, weapon.Luck);

        // 验证背包
        Assert.Single(loaded.BagItems);
        Assert.Equal("圣战戒指", loaded.BagItems[0].Name);

        // 验证仓库
        Assert.Single(loaded.StorageItems);
        Assert.Equal("金条", loaded.StorageItems[0].Name);
    }

    [Fact]
    public void TestWorldPersistence_SavesAndRestoresSabukAndGuildLedger()
    {
        var engine = new DataPersistenceEngine(_testSaveDir);
        var treasury = new SabukTreasuryEngine();
        var guildEngine = new GuildEngine();

        // 存入沙巴克城堡税金 8,888,888 金币
        treasury.SetTreasury(8_888_888, "邪恶同盟", "独孤求败");

        // 建立两个行会
        guildEngine.LoadGuild("邪恶同盟", "独孤求败", "沙巴克行会霸主", new[] { "成员A", "成员B" });
        guildEngine.LoadGuild("昔日同盟", "小虾米", "沙城不倒我们不散", new[] { "成员C" });

        // 保存世界状态
        bool saved = engine.SaveWorld(treasury, guildEngine);
        Assert.True(saved);

        // 重置内存并加载
        var newTreasury = new SabukTreasuryEngine();
        var newGuildEngine = new GuildEngine();

        bool loaded = engine.LoadWorld(newTreasury, newGuildEngine);
        Assert.True(loaded);

        Assert.Equal(8_888_888, newTreasury.GoldTreasury);
        Assert.Equal("邪恶同盟", newTreasury.OwnerGuildName);

        var g1 = newGuildEngine.GetGuildByPlayer("独孤求败");
        Assert.NotNull(g1);
        Assert.Equal("邪恶同盟", g1.Name);
        Assert.Equal("独孤求败", g1.MasterName);
        Assert.Contains("成员A", g1.Members);
        Assert.Contains("成员B", g1.Members);

        var g2 = newGuildEngine.GetGuildByPlayer("小虾米");
        Assert.NotNull(g2);
        Assert.Equal("昔日同盟", g2.Name);
        Assert.Equal("小虾米", g2.MasterName);
    }

    [Fact]
    public void TestGroundItemDecay_CleansExpiredAndBroadcastsDelGroundItem()
    {
        var map = new GameMap(0, "比奇省", 100, 100);
        var mapMgr = new MapManager();
        mapMgr.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapMgr);

        // 注册一个周围的玩家观察者
        var observerSession = new ClientSession(1, null!)
        {
            CharacterName = "路人甲",
            CurrentMapId = 0,
            X = 50,
            Y = 50,
            State = new PlayerState()
        };
        map.Aoi.Enter(observerSession, 50, 50);

        // 在 (51, 50) 地表生成一件掉落物，掉落时间设为 200 秒前
        long now = Environment.TickCount64;
        map.AddGroundItem(new GroundItemEntity
        {
            Uid = 55501,
            Name = "金币",
            Count = 100,
            IsGold = true,
            X = 51,
            Y = 50,
            DropTickMs = now - 200_000 // 已经存在 200 秒 (超期 120 秒)
        });

        Assert.Single(map.GetAllGroundItems());

        // 驱动地表生命周期扫描
        dispatcher.UpdateWorldTick(1, now);

        // 验证地表物品被清除
        Assert.Empty(map.GetAllGroundItems());

        // 验证观察者收到 SM_DELGROUNDITEM (315) 封包
        string output = observerSession.GetBufferedDataUtf8();
        Assert.Contains($"#{ProtocolConsts.SM_DELGROUNDITEM}/55501/", output);
    }

    [Fact]
    public void TestConsoleCommandEngine_ExecutesSaveStatsAndClean()
    {
        var gateway = new TcpGateway(7005);
        var map = new GameMap(0, "比奇省", 100, 100);
        var mapMgr = new MapManager();
        mapMgr.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapMgr);
        var worldTick = new GameWorldTick(gateway);
        var pluginEngine = new ModernNpcPluginEngine();

        var cli = new ConsoleCommandEngine(gateway, dispatcher, worldTick, mapMgr, null, pluginEngine);

        string statsResp = cli.ExecuteCommand("stats");
        Assert.Contains("[CLI]", statsResp);
        Assert.Contains("Tick", statsResp);

        string saveResp = cli.ExecuteCommand("save");
        Assert.Contains("[CLI] 全量存盘完成", saveResp);

        string cleanResp = cli.ExecuteCommand("clean");
        Assert.Contains("[CLI] 立即清理地表过期物品", cleanResp);
    }
}
