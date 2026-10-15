using System;
using System.Collections.Generic;
using System.Linq;
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
        dispatcher.ExpRate = config.ExpRate;
        dispatcher.GmEnabled = config.GmEnabled;
        if (config.GmEnabled) Log.Warning("[Mir2.Server] GM 聊天命令已启用 (@gm ...)，仅供测试，生产环境请勿开启");
        if (config.ExpRate != 1.0)
            Log.Warning("[Mir2.Server] 击杀经验倍率 ExpRate = {Rate}x (官方为 1.0，仅用于压测/机器人，生产环境请勿修改)", config.ExpRate);

        // 1~3. 地图碰撞 / 传送门 / 刷怪 / 掉落 / NPC 全部由数据库提供 (见下方 PostgreSQL 接入后的 LoadOfficialWorld)，
        //      不再有任何硬编码地图、刷怪规则或虚构 NPC。

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

                // 官方 Monster.DB 数据以数据库为唯一真值：启动时读入内存，供刷怪与战斗结算使用
                try
                {
                    var rows = pgEngine.LoadOfficialMonsterStats();
                    if (rows != null)
                    {
                        Mir2.Shared.Data.OfficialMonsterStatsTable.ReplaceRuntime(rows, $"PostgreSQL official_monster ({rows.Count} 条)");
                        Log.Information("[Mir2.Server] 官方怪物属性已从数据库载入内存: {Count} 条", rows.Count);
                        LoadOfficialWorld(pgEngine);
                    }
                    else
                    {
                        Log.Warning("[Mir2.Server] 数据库中无 official_monster 数据，回退内置生成表。请执行 tools/convert_official_db_to_sql.py 并导入 data/official_14/official_14_db.sql");
                    }
                }
                catch (Exception ex)
                {
                    Log.Warning("[Mir2.Server] 读取 official_monster 失败 ({Error})，回退内置生成表。", ex.Message);
                }
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
    }

    /// <summary>
    /// 从 PostgreSQL 一次性装载官方世界：地图碰撞网格、MapInfo 传送门、Mongen 刷怪规则、MonItems 掉落表、NPC 摆放与脚本。
    /// 刷怪按地图懒激活 (官方全图约 3 万只怪)：仅为有玩家的地图及预热地图刷怪。
    /// </summary>
    private void LoadOfficialWorld(PostgreSqlPersistenceEngine pgEngine)
    {
        var repo = new OfficialWorldRepository(pgEngine.DataSource);
        if (!repo.HasWorldData())
        {
            Log.Error("[Mir2.Server] 数据库中没有官方世界数据 (official_map)，世界将为空！请先执行 python tools/import_official_envir_to_pg.py");
            return;
        }

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var world = repo.Load();
        var rep = world.Report;

        OfficialWorldApplier.Apply(world, mapManager, regenEngine, npcEngine, dispatcher.SafeZoneEngine,
            () => dispatcher.ActiveSessions.Values.Select(s => s.CurrentMapId));

        var initRng = OfficialClientRulesEngine.DelphiRandom.Shared;
        for (int i = 0; i < 5; i++) regenEngine.ProcessRegenTick(initRng, enforceTimeSlice: false);

        Log.Information("[Mir2.Server] 官方世界已从数据库装载 ({Ms}ms): 地图 {Maps} 张 (因尺寸异常跳过 {Bad} 张), 传送门 {Gates} 条, " +
                        "刷怪规则 {Rules} 条 (官方总计 {Total} 只, 拼写错误跳过 {Skip} 条), 掉落表 {DropMon} 种怪/{DropRules} 条规则, NPC {Npcs} 个 (含货品 {Goods} 个)",
            sw.ElapsedMilliseconds, rep.MapsLoaded, rep.MapsSkippedBadSize, rep.GatesLoaded, world.Spawns.Count,
            rep.SpawnMonsterTotal, rep.SpawnRowsSkippedUnknownMonster, rep.DropMonsters, rep.DropRules, rep.NpcsLoaded, rep.NpcsWithGoods);
        if (rep.SkippedMaps.Count > 0)
            Log.Warning("[Mir2.Server] 跳过的地图: {Maps}", string.Join(", ", rep.SkippedMaps));
        if (rep.UnknownMonsters.Count > 0)
            Log.Warning("[Mir2.Server] 官方 Mongen 中不存在于 Monster.DB 的怪物名 (原版拼写错误): {Names}", string.Join(", ", rep.UnknownMonsters));
        if (rep.UnmatchedDropItems.Count > 0)
            Log.Warning("[Mir2.Server] 掉落表中无法对应 StdItems 的物品名 {N} 种 (保留原名): {Names}", rep.UnmatchedDropItems.Count,
                string.Join(", ", rep.UnmatchedDropItems.Take(20)));
        Log.Information("[Mir2.Server] 当前活跃怪物总数: {Count}", regenEngine.TotalActiveMonsters);
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
            // 死亡玩家不得自然回血，否则 HP 悄悄变回 >0，客户端仍显示死亡且 CM_REVIVE 被服务端忽略 (机器人套件抓到的缺陷)
            if (session.State != null && session.IsConnected && session.State.Hp > 0)
            {
                var (hpGain, mpGain) = PotionEngine.TickIncHealthSpell(session.State);
                if (hpGain > 0 || mpGain > 0)
                {
                    session.EnqueuePacket(ProtocolConsts.SM_HEALTHSPELLCHANGED, session.State.Hp, session.State.Mp, session.State.MaxHp, session.State.MaxMp);
                }

                // 盛大官方 1.5 权威心跳自然生命与魔法回复 (IDA sub_4C7720 @ 0x4C7851 ~ 0x4C78EA)
                if (session.State.LastNaturalRegenMs == 0)
                {
                    session.State.LastNaturalRegenMs = nowMs;
                }
                else if (nowMs - session.State.LastNaturalRegenMs >= 3000)
                {
                    session.State.LastNaturalRegenMs = nowMs;
                    bool naturalChanged = false;
                    if (!session.State.HasGreenPoison && session.State.Hp < session.State.MaxHp)
                    {
                        int stepHp = Math.Max(1, (session.State.MaxHp / 75) + 1);
                        session.State.Hp = Math.Min(session.State.MaxHp, session.State.Hp + stepHp);
                        naturalChanged = true;
                    }
                    if (session.State.Mp < session.State.MaxMp)
                    {
                        int stepMp = Math.Max(1, (session.State.MaxMp / 18) + 1);
                        session.State.Mp = Math.Min(session.State.MaxMp, session.State.Mp + stepMp);
                        naturalChanged = true;
                    }
                    if (naturalChanged)
                    {
                        session.EnqueuePacket(ProtocolConsts.SM_HEALTHSPELLCHANGED, session.State.Hp, session.State.Mp, session.State.MaxHp, session.State.MaxMp);
                    }
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
                            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "The Ring made you live");
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
