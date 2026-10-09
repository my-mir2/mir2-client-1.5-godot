using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Mir2.Server.Core;
using Mir2.Server.Maps;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Server.Game;
using Mir2.Shared.Protocols;
using Mir2.Shared.Rules;

namespace Mir2.Server;

public static class Program
{
    private static TcpGateway? _gateway;
    private static GameWorldTick? _worldTick;
    private static MapManager? _mapManager;
    private static MonsterRegenEngine? _regenEngine;
    private static NpcMerchantEngine? _npcEngine;
    private static TradeEngine? _tradeEngine;
    private static GroupEngine? _groupEngine;
    private static GuildEngine? _guildEngine;
    private static PacketDispatcher? _dispatcher;

    public static async Task Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("====================================================================");
        Console.WriteLine("  热血传奇 1.5 官方原版 .NET 10 高并发 M2 服务端引擎 (Mir2.Server)");
        Console.WriteLine($"  运行框架: .NET 10 (CLR / Native AOT 模式: {IsNativeAot()})");
        Console.WriteLine("  架构设计: 100ms 心跳末尾合包 + 九宫格 AOI 空间分区 + 零分配缓冲池");
        Console.WriteLine("====================================================================");
        Console.ResetColor();

        // 1. 初始化地图管理器与主地图比奇省 (400x400 格子)
        _mapManager = new MapManager();
        var mainMap = new GameMap(0, "比奇省", 400, 400);
        string mapPath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "resources", "maps", "Map", "0.map");
        if (System.IO.File.Exists(mapPath))
        {
            mainMap.LoadMapFile(mapPath);
            Console.WriteLine("[Mir2.Server] 比奇省官方真实物理碰撞网格装载成功。");
        }
        _mapManager.RegisterMap(mainMap);
        Console.WriteLine("[Mir2.Server] 比奇省地图 AOI 空间索引初始化完成 (尺寸: 400x400, 划分 25x25=625 个 Zone).");

        // 2. 初始化怪物刷新引擎与官方经典怪物规则
        _regenEngine = new MonsterRegenEngine(_mapManager);
        _regenEngine.AddRule(new MonsterSpawnRule { RuleId = 1, MonsterName = "半兽人", MapId = 0, CenterX = 330, CenterY = 270, Radius = 30, MaxCount = 30, BaseHp = 90 });
        _regenEngine.AddRule(new MonsterSpawnRule { RuleId = 2, MonsterName = "多钩猫", MapId = 0, CenterX = 340, CenterY = 260, Radius = 25, MaxCount = 25, BaseHp = 65 });
        _regenEngine.AddRule(new MonsterSpawnRule { RuleId = 3, MonsterName = "钉耙猫", MapId = 0, CenterX = 320, CenterY = 280, Radius = 25, MaxCount = 25, BaseHp = 65 });
        _regenEngine.AddRule(new MonsterSpawnRule { RuleId = 4, MonsterName = "稻草人", MapId = 0, CenterX = 310, CenterY = 250, Radius = 20, MaxCount = 20, BaseHp = 45 });
        _regenEngine.AddRule(new MonsterSpawnRule { RuleId = 5, MonsterName = "森林雪人", MapId = 0, CenterX = 350, CenterY = 290, Radius = 20, MaxCount = 20, BaseHp = 110 });

        // 3. 初始化官方商人与 NPC 实体
        _npcEngine = new NpcMerchantEngine();
        var pharmacy = new NpcEntity { Uid = 101, Name = "银杏药店老板", MapId = 0, X = 335, Y = 275 };
        pharmacy.ScriptPages["@main"] = "欢迎光临银杏山谷药房，这里提供最好的草药！\\ \\<购买/@buy> 药品\\<出售/@sell> 物品\\<离开/@exit>";
        pharmacy.Goods.Add(new MerchantGoods { GoodsId = 1, Name = "金创药(小量)", Price = 55, Weight = 1 });
        pharmacy.Goods.Add(new MerchantGoods { GoodsId = 2, Name = "魔法药(小量)", Price = 55, Weight = 1 });
        pharmacy.Goods.Add(new MerchantGoods { GoodsId = 3, Name = "太阳水", Price = 550, Weight = 2 });
        pharmacy.Goods.Add(new MerchantGoods { GoodsId = 4, Name = "强效太阳水", Price = 1100, Weight = 2 });
        _npcEngine.RegisterNpc(pharmacy);

        var blacksmith = new NpcEntity { Uid = 102, Name = "银杏铁匠", MapId = 0, X = 330, Y = 265 };
        blacksmith.ScriptPages["@main"] = "想要锋利的宝剑吗？我这里能修也能卖！\\ \\<购买/@buy> 武器\\<修理/@repair> 装备\\<特殊修理/@s_repair> 装备\\<离开/@exit>";
        blacksmith.Goods.Add(new MerchantGoods { GoodsId = 10, Name = "木剑", Price = 100, Weight = 7, MaxDura = 4000 });
        blacksmith.Goods.Add(new MerchantGoods { GoodsId = 11, Name = "铁剑", Price = 1000, Weight = 10, MaxDura = 10000 });
        _npcEngine.RegisterNpc(blacksmith);

        _tradeEngine = new TradeEngine();
        _groupEngine = new GroupEngine();
        _guildEngine = new GuildEngine();

        // 4. 装配官方 1.5 完整封包分发中心
        _dispatcher = new PacketDispatcher(_mapManager, _regenEngine, _npcEngine, _tradeEngine, _groupEngine, _guildEngine);

        // 5. 初始化现代异步网络网关 (默认端口 7000)
        _gateway = new TcpGateway(7000);
        _gateway.SessionConnectedHandler = OnSessionConnected;
        _gateway.SessionDisconnectedHandler = OnSessionDisconnected;
        _gateway.PacketReceivedHandler = OnPacketReceived;
        _gateway.Start();

        // 6. 启动 100ms 物理世界心跳引擎
        _worldTick = new GameWorldTick(_gateway);
        _worldTick.WorldUpdateHandler = OnWorldTick;
        _worldTick.Start();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("[Mir2.Server] 服务端引擎启动完毕，千人同屏架构与全生命周期世界状态机已就绪。正在监听玩家连接...");
        Console.ResetColor();

        // 7. 统计监控循环 (每 5 秒输出一次性能指标)
        while (true)
        {
            await Task.Delay(5000);
            double tickMs = _worldTick.LastTickDurationMs;
            double maxTickMs = _worldTick.MaxTickDurationMs;
            int online = _gateway.ActiveConnectionsCount;
            int monsters = _regenEngine.TotalActiveMonsters;

            Console.WriteLine($"[监控] 在线会话: {online} | 活跃怪物: {monsters} | 当前Tick耗时: {tickMs:F3}ms | 峰值Tick耗时: {maxTickMs:F3}ms | 运行周期: {_worldTick.CurrentTick}");
        }
    }

    private static void OnSessionConnected(ClientSession session)
    {
        Console.WriteLine($"[网关] 玩家连接建立: 会话 #{session.SessionId}");
        // 初始出生点: 比奇省银杏山谷 (330, 270)
        session.CurrentMapId = 0;
        session.X = 330;
        session.Y = 270;
        session.State = new PlayerState();

        var map = _mapManager?.GetMap(session.CurrentMapId) ?? _mapManager?.DefaultMap;
        map?.Aoi.Enter(session, session.X, session.Y);

        // 下发官方 1.5 登录就绪与欢迎包
        session.EnqueuePacket(ProtocolConsts.SM_LOGON_DELPHI, 1, 0, 0, 0, "盛大传奇1.5官方服务器");
    }

    private static void OnSessionDisconnected(ClientSession session)
    {
        Console.WriteLine($"[网关] 玩家连接断开: 会话 #{session.SessionId}");
        var map = _mapManager?.GetMap(session.CurrentMapId) ?? _mapManager?.DefaultMap;
        map?.Aoi.Leave(session, session.X, session.Y);
    }

    private static void OnPacketReceived(ClientSession session, string packet)
    {
        _dispatcher?.Dispatch(session, packet);
    }

    private static void OnWorldTick(long tick, double deltaMs)
    {
        if (_dispatcher == null || _gateway == null) return;
        long nowMs = Environment.TickCount64;

        // 1. 怪物刷新心跳 (每 10 个 Tick 即 1 秒触发一次带 15ms 熔断保护的刷怪心跳)
        if (tick % 10 == 0 && _regenEngine != null)
        {
            _regenEngine.ProcessRegenTick(OfficialClientRulesEngine.DelphiRandom.Shared);
        }

        // 2. 怪物 AI 行为驱动 (每个 Tick 100ms 驱动怪物寻敌与攻击动作)
        _dispatcher.ProcessMonsterAiCycle(OfficialClientRulesEngine.DelphiRandom.Shared);

        // 3. 玩家状态周期性权威流转 (药水缓回、道士毒素 Tick)
        foreach (var session in _gateway.AllSessions)
        {
            if (session.State != null && session.IsConnected)
            {
                // 3.1 官方药水持续回复蓄积池 (每 Tick 均匀抽取释放 5 点血/蓝并下发 SM_HEALTHSPELLCHANGED)
                var (hpGain, mpGain) = PotionEngine.TickIncHealthSpell(session.State);
                if (hpGain > 0 || mpGain > 0)
                {
                    session.EnqueuePacket(ProtocolConsts.SM_HEALTHSPELLCHANGED, session.State.Hp, session.State.Mp, session.State.MaxHp, session.State.MaxMp);
                }

                // 3.2 道士毒素 Tick 扣血
                var (tookDmg, dmg, greenExpired, redExpired) = session.State.ProcessPoisonTick(nowMs);
                if (tookDmg && dmg > 0)
                {
                    session.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, session.State.Hp, session.State.MaxHp, session.State.Mp, session.State.MaxMp);
                    if (session.State.Hp <= 0)
                    {
                        session.EnqueuePacket(ProtocolConsts.SM_DEATH_ACTION_DELPHI, session.SessionId, session.X, session.Y, 0, session.CharacterName);
                    }
                }
                if (greenExpired) session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "绿毒效果已消散。");
                if (redExpired) session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "红毒效果已消散。");
            }
        }

        // 4. 地面掉落物生命周期清理 (每 50 个 Tick 清理一次超过存活时间的掉落物并解除 80 秒拾取权保护)
        if (tick % 50 == 0 && _mapManager != null)
        {
            var map = _mapManager.GetMap(0);
            map?.CleanExpiredGroundItems(nowMs);
        }

        // 5. 地表火墙生命周期清理 (每 10 个 Tick 清理一次过期火墙实体)
        if (tick % 10 == 0 && _mapManager != null)
        {
            var map = _mapManager.GetMap(0);
            map?.CleanExpiredFireWalls(nowMs);
        }
    }

    private static bool IsNativeAot()
    {
        return !RuntimeFeature.IsDynamicCodeCompiled;
    }
}
