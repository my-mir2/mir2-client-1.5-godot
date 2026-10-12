using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Mir2.Server.Game;
using Mir2.Server.Logging;
using Mir2.Server.Maps;
using Mir2.Server.Network;
using Mir2.Server.Persistence;
using Mir2.Server.Web;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;
using Mir2.Shared.Rules;
using Serilog;

namespace Mir2.Server.Core;

/// <summary>
/// 盛大官方 1.5 服务端现代后台宿主托管服务 (Hosted Service)
/// 基于 C# 12 主构造函数 (Primary Constructor) 与标准依赖注入 (DI)
/// 负责全生命周期状态机编排：资源装配、网络网关、物理世界心跳、Web 监控与安全优雅关机
/// </summary>
public sealed class Mir2ServerHostedService(
    ServerConfig config,
    MapManager mapManager,
    MonsterRegenEngine regenEngine,
    NpcMerchantEngine npcEngine,
    PacketDispatcher dispatcher,
    GameWorldTick worldTick,
    TcpGateway gateway,
    IHostApplicationLifetime appLifetime) : IHostedService
{
    private NpcPluginDirectoryWatcher? _pluginWatcher;
    private EmbeddedAdminWebServer? _webAdmin;
    private ConsoleCommandEngine? _cliEngine;
    private CancellationTokenSource? _workerCts;
    private Task? _monitorLoopTask;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Log.Information("[Mir2ServerHostedService] 正在通过现代依赖注入容器拉起传奇 1.5 核心引擎...");

        // 1. 初始化主地图与物理碰撞网格
        var mainMap = mapManager.GetMap(0) ?? new GameMap(0, "比奇省", 700, 700);
        string mapPath = Path.Combine(Directory.GetCurrentDirectory(), "resources", "maps", "Map", "0.map");
        if (File.Exists(mapPath))
        {
            mainMap.LoadMapFile(mapPath);
            Log.Information("[Mir2.Server] 比奇省官方真实物理碰撞网格装载成功。");
        }
        mapManager.RegisterMap(mainMap);
        int zoneCols = (mainMap.Width + 15) / 16;
        int zoneRows = (mainMap.Height + 15) / 16;
        Log.Information("[Mir2.Server] 比奇省地图 AOI 空间索引初始化完成 (尺寸: {Width}x{Height}, 划分 {Zones} 个 Zone).",
            mainMap.Width, mainMap.Height, zoneCols * zoneRows);

        // 2. 初始化经典怪物刷新规则
        regenEngine.AddRule(new MonsterSpawnRule { RuleId = 1, MonsterName = "半兽人", MapId = 0, CenterX = 330, CenterY = 270, Radius = 30, MaxCount = 30, BaseHp = 90 });
        regenEngine.AddRule(new MonsterSpawnRule { RuleId = 2, MonsterName = "多钩猫", MapId = 0, CenterX = 340, CenterY = 260, Radius = 25, MaxCount = 25, BaseHp = 65 });
        regenEngine.AddRule(new MonsterSpawnRule { RuleId = 3, MonsterName = "钉耙猫", MapId = 0, CenterX = 320, CenterY = 280, Radius = 25, MaxCount = 25, BaseHp = 65 });
        regenEngine.AddRule(new MonsterSpawnRule { RuleId = 4, MonsterName = "稻草人", MapId = 0, CenterX = 310, CenterY = 250, Radius = 20, MaxCount = 20, BaseHp = 45 });
        regenEngine.AddRule(new MonsterSpawnRule { RuleId = 5, MonsterName = "森林雪人", MapId = 0, CenterX = 350, CenterY = 290, Radius = 20, MaxCount = 20, BaseHp = 110 });

        // 3. 初始化官方商人与 NPC 实体 (100% 对齐官方 merchant.txt)
        var biqiPharmacy = new NpcEntity { Uid = 101, Name = "比奇药店老板", MapId = 0, X = 313, Y = 271 };
        biqiPharmacy.ScriptPages["@main"] = "欢迎光临比奇大药房，这里提供最好的草药！\\ \\<购买/@buy> 药品\\<出售/@sell> 物品\\<离开/@exit>";
        biqiPharmacy.Goods.Add(new MerchantGoods { GoodsId = 1, Name = "金创药(小量)", Price = 55, Weight = 1 });
        biqiPharmacy.Goods.Add(new MerchantGoods { GoodsId = 2, Name = "魔法药(小量)", Price = 55, Weight = 1 });
        biqiPharmacy.Goods.Add(new MerchantGoods { GoodsId = 3, Name = "太阳水", Price = 550, Weight = 2 });
        biqiPharmacy.Goods.Add(new MerchantGoods { GoodsId = 4, Name = "强效太阳水", Price = 1100, Weight = 2 });
        npcEngine.RegisterNpc(biqiPharmacy);

        var biqiBlacksmith = new NpcEntity { Uid = 102, Name = "比奇铁匠", MapId = 0, X = 302, Y = 219 };
        biqiBlacksmith.ScriptPages["@main"] = "想要锋利的宝剑吗？我这里能修也能卖！\\ \\<购买/@buy> 武器\\<修理/@repair> 装备\\<特殊修理/@s_repair> 装备\\<离开/@exit>";
        biqiBlacksmith.Goods.Add(new MerchantGoods { GoodsId = 10, Name = "木剑", Price = 100, Weight = 7, MaxDura = 4000 });
        biqiBlacksmith.Goods.Add(new MerchantGoods { GoodsId = 11, Name = "铁剑", Price = 1000, Weight = 10, MaxDura = 10000 });
        npcEngine.RegisterNpc(biqiBlacksmith);

        // 4. PostgreSQL 工业级持久化接入
        if (config.EnableDatabaseLogging || !string.IsNullOrWhiteSpace(config.DatabaseConnectionString))
        {
            try
            {
                var pgEngine = new PostgreSqlPersistenceEngine(config.DatabaseConnectionString);
                pgEngine.InitializeSchema();
                dispatcher.PgPersistenceEngine = pgEngine;
                Log.Information("[Mir2.Server] PostgreSQL 工业级数据库物理连接成功，DDL 表结构与索引已就绪！");
                pgEngine.LoadWorld(dispatcher.TreasuryEngine, dispatcher.GuildEngine);
            }
            catch (Exception ex)
            {
                Log.Warning("[Mir2.Server] PostgreSQL 暂未连通 ({Error})，自动切入文件安全原子持久化备份。", ex.Message);
                dispatcher.PersistenceEngine.LoadWorld(dispatcher.TreasuryEngine, dispatcher.GuildEngine);
            }
        }
        else
        {
            dispatcher.PersistenceEngine.LoadWorld(dispatcher.TreasuryEngine, dispatcher.GuildEngine);
        }
        Log.Information("[Mir2.Server] 沙巴克城堡与行会大盘持久化存档加载完毕。");

        // 5. 插件热更监听器
        _pluginWatcher = new NpcPluginDirectoryWatcher(dispatcher.ModernNpcEngine, "plugins");
        _pluginWatcher.Start();

        // 6. 配置网关事件回调
        gateway.SessionConnectedHandler = OnSessionConnected;
        gateway.SessionDisconnectedHandler = OnSessionDisconnected;
        gateway.PacketReceivedHandler = (s, p) => dispatcher.Dispatch(s, p);
        gateway.BinaryPacketReceivedHandler = (s, h, p) => dispatcher.DispatchBinary(s, in h, p);
        gateway.Start();

        // 7. 启动世界心跳
        worldTick.WorldUpdateHandler = OnWorldTick;
        worldTick.Start();

        // 8. 启动 CLI 与 Web 监控面板
        _cliEngine = new ConsoleCommandEngine(gateway, dispatcher, worldTick, mapManager, regenEngine, dispatcher.ModernNpcEngine, _pluginWatcher);
        _webAdmin = new EmbeddedAdminWebServer(gateway, dispatcher, worldTick, mapManager, regenEngine, dispatcher.ModernNpcEngine, config.AdminPort, config.AdminSecret);
        _webAdmin.Start();

        // 9. 启动后台监控与自动存档循环
        _workerCts = new CancellationTokenSource();
        _monitorLoopTask = RunMonitorLoopAsync(_workerCts.Token);

        // 10. 控制台读取 (针对有交互式 TTY 的前台会话，无控制台或后台托管模式下安全静默)
        try
        {
            if (!Console.IsInputRedirected)
            {
                _ = Task.Run(() =>
                {
                    try
                    {
                        while (!_workerCts.IsCancellationRequested)
                        {
                            string? line = Console.ReadLine();
                            if (line == null) break;
                            string resp = _cliEngine.ExecuteCommand(line);
                            if (!string.IsNullOrEmpty(resp))
                            {
                                Console.WriteLine(resp);
                            }
                            if (line.Trim().Equals("stop", StringComparison.OrdinalIgnoreCase))
                            {
                                appLifetime.StopApplication();
                                break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Debug("[Mir2ServerHostedService] 控制台输入流不可用或已关闭: {Message}", ex.Message);
                    }
                });
            }
        }
        catch (Exception ex)
        {
            Log.Debug("[Mir2ServerHostedService] 无法检测控制台重定向状态: {Message}", ex.Message);
        }

        Log.Information("[Mir2.Server] 服务端托管生命周期启动完毕，网关端口: {Port}, Web监控: http://localhost:{WebPort}/",
            config.GatewayPort, config.AdminPort);

        return Task.CompletedTask;
    }

    private void OnSessionConnected(ClientSession session)
    {
        Log.Information("[网关] 玩家连接建立: 会话 #{SessionId} (端点: {RemoteEndPoint})", session.SessionId, session.RemoteEndPoint);
        session.CurrentMapId = 0;
        session.X = 330;
        session.Y = 270;
        session.State = new PlayerState();

        var map = mapManager.GetMap(session.CurrentMapId) ?? mapManager.DefaultMap;
        map?.Aoi.Enter(session, session.X, session.Y);

        session.EnqueuePacket(ProtocolConsts.SM_LOGON_DELPHI, 1, 0, 0, 0, "盛大传奇1.5官方服务器");
    }

    private void OnSessionDisconnected(ClientSession session)
    {
        Log.Information("[网关] 玩家连接断开: 会话 #{SessionId} 角色: {CharacterName}", session.SessionId, session.CharacterName);
        if (!string.IsNullOrEmpty(session.AccountName))
        {
            ServerLogger.Audit("LOGOUT", session.AccountName, session.CharacterName, "玩家断开网络连接");
        }
        dispatcher.OnSessionDisconnected(session);
    }

    private void OnWorldTick(long tick, double deltaMs)
    {
        var rng = OfficialClientRulesEngine.DelphiRandom.Shared;
        long nowMs = Environment.TickCount64;

        if (tick % 10 == 0)
        {
            regenEngine.ProcessRegenTick(rng);
        }

        dispatcher.ProcessMonsterAiCycle(rng);
        dispatcher.UpdateWorldTick(tick, nowMs);

        // 3. 玩家持续状态循环 (药水、毒素、复活戒等)
        foreach (var session in gateway.AllSessions)
        {
            if (session.State != null && session.IsConnected)
            {
                var (hpGain, mpGain) = PotionEngine.TickIncHealthSpell(session.State);
                if (hpGain > 0 || mpGain > 0)
                {
                    session.EnqueuePacket(ProtocolConsts.SM_HEALTHSPELLCHANGED, session.State.Hp, session.State.Mp, session.State.MaxHp, session.State.MaxMp);
                }

                var (tookDmg, dmg, greenExpired, redExpired) = session.State.ProcessPoisonTick(nowMs);
                if (tookDmg && dmg > 0)
                {
                    session.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, session.State.Hp, session.State.MaxHp, session.State.Mp, session.State.MaxMp);
                    if (session.State.Hp <= 0)
                    {
                        var (revived, revHp, revMp) = session.State.CheckAndTriggerRevivalRing(nowMs);
                        if (revived)
                        {
                            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "【神戒护佑】复活戒指释放神圣光芒，生命值完全恢复！");
                            session.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, session.State.Hp, session.State.MaxHp, session.State.Mp, session.State.MaxMp);
                        }
                        else
                        {
                            session.EnqueuePacket(ProtocolConsts.SM_DEATH_ACTION_DELPHI, session.SessionId, session.X, session.Y, 0, session.CharacterName);
                        }
                    }
                }
                if (greenExpired) session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "绿毒效果已消散。");
                if (redExpired) session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "红毒效果已消散。");
            }
        }

        // 4. 地表火墙生命周期清理 (每 10 个 Tick)
        if (tick % 10 == 0)
        {
            var map = mapManager.GetMap(0);
            map?.CleanExpiredFireWalls(nowMs);
        }

        // 5. 地图门状态机自动关门流转 (每 10 个 Tick)
        if (tick % 10 == 0)
        {
            dispatcher.TickWorldDoors(nowMs);
        }
    }

    private async Task RunMonitorLoopAsync(CancellationToken ct)
    {
        long lastAutoSaveTick = Environment.TickCount64;
        long lastLogArchiveTick = Environment.TickCount64;

        while (!ct.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(5000, ct);
            }
            catch (OperationCanceledException)
            {
                break;
            }

            double tickMs = worldTick.LastTickDurationMs;
            double maxTickMs = worldTick.MaxTickDurationMs;
            int online = gateway.ActiveConnectionsCount;
            int monsters = regenEngine.TotalActiveMonsters;

            if (Environment.TickCount64 - lastAutoSaveTick >= 300_000)
            {
                lastAutoSaveTick = Environment.TickCount64;
                _cliEngine?.ExecuteCommand("save");
            }

            if (Environment.TickCount64 - lastLogArchiveTick >= 3600_000)
            {
                lastLogArchiveTick = Environment.TickCount64;
                if (ServerLogger.ArchiveEngine != null)
                {
                    _ = ServerLogger.ArchiveEngine.ArchiveOldLogsAsync(config.LogArchiveRetentionDays);
                    _ = ServerLogger.ArchiveEngine.PurgeArchiveLogsAsync(config.LogDeepPurgeDays);
                }
            }

            Log.Information("[监控] 在线会话: {Online} | 活跃怪物: {Monsters} | 当前Tick耗时: {TickMs:F3}ms | 峰值Tick耗时: {MaxTickMs:F3}ms | 运行周期: {Tick}",
                online, monsters, tickMs, maxTickMs, worldTick.CurrentTick);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Log.Information("[Mir2ServerHostedService] 正在执行全服优雅关机流程 (Graceful Shutdown)...");
        _workerCts?.Cancel();
        _webAdmin?.Stop();
        _cliEngine?.ShutdownGracefully();
        _pluginWatcher?.Dispose();
        gateway.Stop();
        worldTick.Stop();
        Log.Information("[Mir2ServerHostedService] 服务端所有底层服务已安全释放。");
        return Task.CompletedTask;
    }
}
