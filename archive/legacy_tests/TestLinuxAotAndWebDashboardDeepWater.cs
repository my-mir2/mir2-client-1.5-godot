using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Mir2.Server.Core;
using Mir2.Server.Game;
using Mir2.Server.Maps;
using Mir2.Server.Network;
using Mir2.Server.Persistence;
using Mir2.Server.Web;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// Linux AOT 生产级嵌入式 Web 监控后台、无锁异步存盘队列与多核多地图并行心跳测试矩阵
/// </summary>
public sealed class TestLinuxAotAndWebDashboardDeepWater : IDisposable
{
    private readonly string _testSaveDir;

    public TestLinuxAotAndWebDashboardDeepWater()
    {
        _testSaveDir = Path.Combine(Path.GetTempPath(), "mir2_test_webaot_" + Guid.NewGuid().ToString("N"));
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
            // 忽略测试清理
        }
    }

    [Fact]
    public async Task TestEmbeddedAdminWebServer_EndpointsAndAuthSecurity()
    {
        int testPort = TestNetworkPortHelper.GetNextPort();
        string token = "test_secret_token_123";

        var gateway = new TcpGateway(TestNetworkPortHelper.GetNextPort());
        var mapMgr = new MapManager();
        var dispatcher = new PacketDispatcher(mapMgr);
        var worldTick = new GameWorldTick(gateway);
        var pluginEngine = new ModernNpcPluginEngine();

        using var web = new EmbeddedAdminWebServer(gateway, dispatcher, worldTick, mapMgr, null, pluginEngine, testPort, token);
        web.Start();
        await Task.Delay(150); // 确保监听就绪

        using var http = new HttpClient();
        string baseUrl = $"http://127.0.0.1:{testPort}";

        // 1. 测试 GET / 返回 HTML 仪表盘
        var htmlResp = await http.GetAsync($"{baseUrl}/");
        Assert.True(htmlResp.IsSuccessStatusCode);
        string html = await htmlResp.Content.ReadAsStringAsync();
        Assert.Contains("盛大传奇 1.5 原生 M2 服务端控制总台", html);

        // 2. 测试 GET /api/stats 返回 JSON 指标
        var statsResp = await http.GetAsync($"{baseUrl}/api/stats");
        Assert.True(statsResp.IsSuccessStatusCode);
        string statsJson = await statsResp.Content.ReadAsStringAsync();
        Assert.Contains("onlinePlayers", statsJson);
        Assert.Contains("memoryMb", statsJson);

        // 3. 测试未授权访问敏感接口返回 HTTP 401
        var unauthResp = await http.PostAsync($"{baseUrl}/api/action/save", new StringContent(""));
        Assert.Equal(System.Net.HttpStatusCode.Unauthorized, unauthResp.StatusCode);

        // 4. 携带正确 Token 访问 POST /api/action/save 返回 HTTP 200
        var authResp = await http.PostAsync($"{baseUrl}/api/action/save?token={token}", new StringContent(""));
        Assert.Equal(System.Net.HttpStatusCode.OK, authResp.StatusCode);
        string saveJson = await authResp.Content.ReadAsStringAsync();
        var saveDto = System.Text.Json.JsonSerializer.Deserialize(saveJson, Mir2SaveJsonContext.Default.AdminActionResponseDto);
        Assert.NotNull(saveDto);
        Assert.True(saveDto.Success);
        Assert.Contains("成功", saveDto.Message);

        // 5. 测试广播接口 POST /api/action/broadcast
        var observer = new ClientSession(99, null!)
        {
            CharacterName = "测试在线者",
            CurrentMapId = 0,
            X = 330,
            Y = 270,
            State = new PlayerState()
        };
        gateway.RegisterSession(observer);

        var broadcastContent = new StringContent("{\"message\":\"沙巴克攻城战即将开始！\"}", Encoding.UTF8, "application/json");
        var bResp = await http.PostAsync($"{baseUrl}/api/action/broadcast?token={token}", broadcastContent);
        Assert.Equal(System.Net.HttpStatusCode.OK, bResp.StatusCode);

        string buffered = observer.GetBufferedDataUtf8();
        for (int i = 0; i < 20 && !buffered.Contains("【全服公告】"); i++)
        {
            await Task.Delay(25);
            buffered = observer.GetBufferedDataUtf8();
        }
        Assert.Contains("【全服公告】", buffered);
        Assert.Contains("沙巴克攻城战", buffered);

        web.Stop();
    }

    [Fact]
    public async Task TestAsyncPersistenceWorker_NonBlockingDiskQueue()
    {
        var persEngine = new DataPersistenceEngine(_testSaveDir);
        using var worker = new AsyncPersistenceWorker(persEngine, 1000);

        var pState = new PlayerState
        {
            Name = "异步落盘测试者",
            Level = 35,
            Gold = 999_999
        };

        var tcs = new TaskCompletionSource<bool>();
        bool queued = worker.EnqueueSavePlayer(pState.Name, pState, 0, 330, 270, tcs);
        Assert.True(queued);

        // 等待后台独立 IO 线程处理完毕
        bool success = await tcs.Task;
        Assert.True(success);

        // 从持久化引擎验证数据已正确落盘
        var loaded = persEngine.LoadPlayer(pState.Name, out int m, out int x, out int y);
        Assert.NotNull(loaded);
        Assert.Equal(35, loaded.Level);
        Assert.Equal(999_999, loaded.Gold);
    }

    [Fact]
    public void TestParallelMultiMapTick_CleansItemsConcurrentlyAcrossMaps()
    {
        var mapMgr = new MapManager();
        var dispatcher = new PacketDispatcher(mapMgr);

        var map0 = mapMgr.GetMap(0)!; // 比奇省
        var map2 = mapMgr.GetMap(2)!; // 毒蛇山谷
        var map3 = mapMgr.GetMap(3)!; // 盟重省

        long now = Environment.TickCount64;

        // 分别在 3 张地图放置过期物品
        map0.AddGroundItem(new GroundItemEntity { Uid = 7001, Name = "乌木剑", DropTickMs = now - 200_000, X = 10, Y = 10 });
        map2.AddGroundItem(new GroundItemEntity { Uid = 7002, Name = "蛇胆", DropTickMs = now - 200_000, X = 20, Y = 20 });
        map3.AddGroundItem(new GroundItemEntity { Uid = 7003, Name = "回城卷", DropTickMs = now - 200_000, X = 30, Y = 30 });

        Assert.Single(map0.GetAllGroundItems());
        Assert.Single(map2.GetAllGroundItems());
        Assert.Single(map3.GetAllGroundItems());

        // 执行多地图多核心并发心跳
        dispatcher.ProcessMultiMapParallelTick(1, now);

        // 验证 3 张地图的掉落物被多核并发全部清理
        Assert.Empty(map0.GetAllGroundItems());
        Assert.Empty(map2.GetAllGroundItems());
        Assert.Empty(map3.GetAllGroundItems());
    }
}
