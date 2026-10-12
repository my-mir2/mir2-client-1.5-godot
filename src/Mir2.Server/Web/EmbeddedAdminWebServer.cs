using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Mir2.Server.Core;
using Mir2.Server.Game;
using Mir2.Server.Maps;
using Mir2.Server.Network;
using Mir2.Server.Persistence;
using Mir2.Shared.Protocols;

namespace Mir2.Server.Web;

/// <summary>
/// 盛大官方 1.5 服务端嵌入式 Web 管理员监控控制台 (零外部框架、Native AOT 完美兼容)
/// 监听 HTTP 端口，提供全栈动态暗黑监控面板与运维 RESTful API
/// </summary>
public sealed class EmbeddedAdminWebServer : IDisposable
{
    private readonly HttpListener _listener;
    private readonly TcpGateway _gateway;
    private readonly PacketDispatcher _dispatcher;
    private readonly GameWorldTick _worldTick;
    private readonly MapManager _mapManager;
    private readonly MonsterRegenEngine? _regenEngine;
    private readonly ModernNpcPluginEngine _pluginEngine;
    private readonly string _adminToken;
    private readonly long _startTimestampMs;
    private CancellationTokenSource? _cts;
    private Task? _listenerTask;
    private bool _isDisposed;

    public int Port { get; }
    public bool IsRunning => _listener.IsListening;

    public EmbeddedAdminWebServer(
        TcpGateway gateway,
        PacketDispatcher dispatcher,
        GameWorldTick worldTick,
        MapManager mapManager,
        MonsterRegenEngine? regenEngine,
        ModernNpcPluginEngine pluginEngine,
        int port = 8080,
        string adminToken = "mir2_admin_secret_2026")
    {
        _gateway = gateway;
        _dispatcher = dispatcher;
        _worldTick = worldTick;
        _mapManager = mapManager;
        _regenEngine = regenEngine;
        _pluginEngine = pluginEngine;
        Port = port;
        _adminToken = adminToken;
        _startTimestampMs = Environment.TickCount64;

        _listener = new HttpListener();
        if (OperatingSystem.IsWindows())
        {
            _listener.Prefixes.Add($"http://127.0.0.1:{port}/");
        }
        else
        {
            _listener.Prefixes.Add($"http://*:{port}/");
        }
    }

    /// <summary>
    /// 启动 HTTP 监控服务
    /// </summary>
    public void Start()
    {
        if (_isDisposed || _listener.IsListening) return;

        try
        {
            _listener.Start();
            _cts = new CancellationTokenSource();
            _listenerTask = Task.Run(() => ListenLoopAsync(_cts.Token));
            Console.WriteLine($"[WebAdmin] 嵌入式 Web 监控后台已启动: http://127.0.0.1:{Port}/ (访问密钥: {_adminToken})");
        }
        catch (HttpListenerException)
        {
            // 非管理员权限绑定失败时回退到 127.0.0.1
            try
            {
                _listener.Prefixes.Clear();
                _listener.Prefixes.Add($"http://127.0.0.1:{Port}/");
                _listener.Start();
                _cts = new CancellationTokenSource();
                _listenerTask = Task.Run(() => ListenLoopAsync(_cts.Token));
                Console.WriteLine($"[WebAdmin] 权限受限，已自动回退到本地监听: http://127.0.0.1:{Port}/");
            }
            catch (Exception fallbackEx)
            {
                Console.WriteLine($"[WebAdmin] 启动 Web 监控后台失败: {fallbackEx.Message}");
            }
        }
    }

    private async Task ListenLoopAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested && _listener.IsListening)
        {
            try
            {
                var context = await _listener.GetContextAsync();
                _ = Task.Run(() => HandleRequestSafe(context), token);
            }
            catch (HttpListenerException)
            {
                break;
            }
            catch (Exception ex)
            {
                if (token.IsCancellationRequested) break;
                Console.WriteLine($"[WebAdmin] 处理请求循环异常: {ex.Message}");
            }
        }
    }

    private void HandleRequestSafe(HttpListenerContext ctx)
    {
        try
        {
            string path = ctx.Request.Url?.AbsolutePath.ToLowerInvariant() ?? "/";

            // CORS 支持
            ctx.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            ctx.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
            ctx.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");

            if (ctx.Request.HttpMethod == "OPTIONS")
            {
                ctx.Response.StatusCode = 204;
                ctx.Response.Close();
                return;
            }

            // 静态仪表盘首页
            if (path == "/" || path == "/index.html")
            {
                ServeDashboardHtml(ctx);
                return;
            }

            // 鉴权检查 (除 GET / 与 GET /api/stats 之外的敏感写操作必须验证 Token)
            if (path.StartsWith("/api/action/") && !IsAuthorized(ctx.Request))
            {
                WriteJsonResponse(ctx, 401, new AdminActionResponseDto { Success = false, Message = "未授权访问：请提供正确的 Admin Token！" }, Mir2SaveJsonContext.Default.AdminActionResponseDto);
                return;
            }

            switch (path)
            {
                case "/api/stats":
                    HandleGetStats(ctx);
                    break;

                case "/api/action/save":
                    HandleActionSave(ctx);
                    break;

                case "/api/action/reload":
                    HandleActionReload(ctx);
                    break;

                case "/api/action/clean":
                    HandleActionClean(ctx);
                    break;

                case "/api/action/broadcast":
                    HandleActionBroadcast(ctx);
                    break;

                case "/api/action/kick":
                    HandleActionKick(ctx);
                    break;

                default:
                    ctx.Response.StatusCode = 404;
                    WriteStringResponse(ctx, 404, "404 Not Found");
                    break;
            }
        }
        catch (Exception ex)
        {
            try
            {
                WriteJsonResponse(ctx, 500, new AdminActionResponseDto { Success = false, Message = $"内部异常: {ex.Message}" }, Mir2SaveJsonContext.Default.AdminActionResponseDto);
            }
            catch
            {
                // 忽略底层断开
            }
        }
    }

    private bool IsAuthorized(HttpListenerRequest req)
    {
        if (string.IsNullOrEmpty(_adminToken)) return true;

        string? queryToken = req.QueryString["token"];
        if (string.Equals(queryToken, _adminToken, StringComparison.Ordinal)) return true;

        string? authHeader = req.Headers["Authorization"];
        if (!string.IsNullOrEmpty(authHeader))
        {
            if (authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                string bearer = authHeader.Substring(7).Trim();
                if (string.Equals(bearer, _adminToken, StringComparison.Ordinal)) return true;
            }
            if (string.Equals(authHeader, _adminToken, StringComparison.Ordinal)) return true;
        }

        return false;
    }

    private void HandleGetStats(HttpListenerContext ctx)
    {
        var map0 = _mapManager.GetMap(0);
        int groundCount = map0?.GetAllGroundItems().Count ?? 0;
        int activeMonsters = _regenEngine?.TotalActiveMonsters ?? 0;
        int onlineCount = _gateway.ActiveConnectionsCount;
        long memMb = GC.GetTotalMemory(false) / 1024 / 1024;
        long upTimeSec = (Environment.TickCount64 - _startTimestampMs) / 1000;

        var playerNames = new List<string>(onlineCount);
        foreach (var s in _gateway.AllSessions)
        {
            if (s.IsConnected && !string.IsNullOrEmpty(s.CharacterName))
            {
                playerNames.Add($"{s.CharacterName} (地图:{s.CurrentMapId}, 坐标:{s.X},{s.Y})");
            }
        }

        var dto = new ServerStatsDto
        {
            OnlinePlayers = onlineCount,
            TotalMonsters = activeMonsters,
            GroundItems = groundCount,
            LastTickMs = _worldTick.LastTickDurationMs,
            MaxTickMs = _worldTick.MaxTickDurationMs,
            MemoryMb = memMb,
            CurrentTick = _worldTick.CurrentTick,
            UpTimeSeconds = upTimeSec,
            IsNativeAot = !System.Runtime.CompilerServices.RuntimeFeature.IsDynamicCodeCompiled,
            PlayerList = playerNames
        };

        WriteJsonResponse(ctx, 200, dto, Mir2SaveJsonContext.Default.ServerStatsDto);
    }

    private void HandleActionSave(HttpListenerContext ctx)
    {
        int saved = _dispatcher.SaveAllActiveSessions(_gateway.AllSessions);
        bool worldSaved = _dispatcher.PersistenceEngine.SaveWorld(_dispatcher.TreasuryEngine, _dispatcher.GuildEngine);
        var resp = new AdminActionResponseDto
        {
            Success = true,
            Message = $"全服数据原子保存成功！落盘在线角色: {saved} 人，世界大盘: {(worldSaved ? "成功" : "失败")}"
        };
        WriteJsonResponse(ctx, 200, resp, Mir2SaveJsonContext.Default.AdminActionResponseDto);
    }

    private void HandleActionReload(HttpListenerContext ctx)
    {
        string pluginPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins", "Mir2.ScriptPlugin.dll");
        if (!File.Exists(pluginPath))
        {
            pluginPath = Path.Combine(Directory.GetCurrentDirectory(), "plugins", "Mir2.ScriptPlugin.dll");
        }

        if (File.Exists(pluginPath))
        {
            var res = _pluginEngine.LoadPluginsFromFile(pluginPath);
            var resp = new AdminActionResponseDto
            {
                Success = res.Success,
                Message = $"插件热重载成功: 装载 {res.LoadedCount} 个 NPC 行为树。"
            };
            WriteJsonResponse(ctx, 200, resp, Mir2SaveJsonContext.Default.AdminActionResponseDto);
        }
        else
        {
            var resp = new AdminActionResponseDto
            {
                Success = false,
                Message = $"未在 plugins/ 目录找到 Mir2.ScriptPlugin.dll 插件文件！"
            };
            WriteJsonResponse(ctx, 404, resp, Mir2SaveJsonContext.Default.AdminActionResponseDto);
        }
    }

    private void HandleActionClean(HttpListenerContext ctx)
    {
        long now = Environment.TickCount64;
        var map = _mapManager.GetMap(0);
        int cleaned = 0;
        if (map != null)
        {
            cleaned = map.CleanExpiredGroundItems(now, 0);
        }
        var resp = new AdminActionResponseDto
        {
            Success = true,
            Message = $"立即清理地表无主道具完成，共销毁回收: {cleaned} 件"
        };
        WriteJsonResponse(ctx, 200, resp, Mir2SaveJsonContext.Default.AdminActionResponseDto);
    }

    private void HandleActionBroadcast(HttpListenerContext ctx)
    {
        using var reader = new StreamReader(ctx.Request.InputStream, Encoding.UTF8);
        string body = reader.ReadToEnd();
        string msg = "";

        if (!string.IsNullOrWhiteSpace(body))
        {
            try
            {
                var req = JsonSerializer.Deserialize(body, Mir2SaveJsonContext.Default.AdminBroadcastRequestDto);
                msg = req?.Message ?? body;
            }
            catch
            {
                msg = body;
            }
        }
        if (string.IsNullOrWhiteSpace(msg))
        {
            msg = ctx.Request.QueryString["msg"] ?? "【系统公告】服务器运行平稳，祝诸位大侠武运昌隆！";
        }

        int count = 0;
        foreach (var s in _gateway.AllSessions)
        {
            if (s.IsConnected)
            {
                s.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"【全服公告】{msg}");
                count++;
            }
        }

        var resp = new AdminActionResponseDto
        {
            Success = true,
            Message = $"全服红字公告已成功广播至 {count} 名在线玩家。"
        };
        WriteJsonResponse(ctx, 200, resp, Mir2SaveJsonContext.Default.AdminActionResponseDto);
    }

    private void HandleActionKick(HttpListenerContext ctx)
    {
        string? target = ctx.Request.QueryString["name"];
        if (string.IsNullOrWhiteSpace(target))
        {
            using var reader = new StreamReader(ctx.Request.InputStream, Encoding.UTF8);
            target = reader.ReadToEnd().Trim();
        }

        if (string.IsNullOrWhiteSpace(target))
        {
            WriteJsonResponse(ctx, 400, new AdminActionResponseDto { Success = false, Message = "请指定要踢出的角色名 (参数 ?name=...)" }, Mir2SaveJsonContext.Default.AdminActionResponseDto);
            return;
        }

        bool kicked = false;
        foreach (var s in _gateway.AllSessions)
        {
            if (s.CharacterName.Equals(target, StringComparison.OrdinalIgnoreCase))
            {
                if (s.State != null)
                {
                    _dispatcher.PersistenceEngine.SavePlayer(s.CharacterName, s.State, s.CurrentMapId, s.X, s.Y);
                }
                s.Dispose();
                kicked = true;
                break;
            }
        }

        var resp = new AdminActionResponseDto
        {
            Success = kicked,
            Message = kicked ? $"角色 {target} 已安全存盘并强制下线。" : $"未找到名为 {target} 的在线玩家。"
        };
        WriteJsonResponse(ctx, kicked ? 200 : 404, resp, Mir2SaveJsonContext.Default.AdminActionResponseDto);
    }

    private static void WriteJsonResponse<T>(HttpListenerContext ctx, int statusCode, T data, System.Text.Json.Serialization.Metadata.JsonTypeInfo<T> typeInfo)
    {
        ctx.Response.StatusCode = statusCode;
        ctx.Response.ContentType = "application/json; charset=utf-8";
        string json = JsonSerializer.Serialize(data, typeInfo);
        byte[] bytes = Encoding.UTF8.GetBytes(json);
        ctx.Response.ContentLength64 = bytes.Length;
        ctx.Response.OutputStream.Write(bytes, 0, bytes.Length);
        ctx.Response.OutputStream.Close();
    }

    private static void WriteStringResponse(HttpListenerContext ctx, int statusCode, string text)
    {
        ctx.Response.StatusCode = statusCode;
        ctx.Response.ContentType = "text/plain; charset=utf-8";
        byte[] bytes = Encoding.UTF8.GetBytes(text);
        ctx.Response.ContentLength64 = bytes.Length;
        ctx.Response.OutputStream.Write(bytes, 0, bytes.Length);
        ctx.Response.OutputStream.Close();
    }

    private void ServeDashboardHtml(HttpListenerContext ctx)
    {
        ctx.Response.StatusCode = 200;
        ctx.Response.ContentType = "text/html; charset=utf-8";
        string html = DashboardHtmlContent.Replace("{{TOKEN}}", _adminToken);
        byte[] bytes = Encoding.UTF8.GetBytes(html);
        ctx.Response.ContentLength64 = bytes.Length;
        ctx.Response.OutputStream.Write(bytes, 0, bytes.Length);
        ctx.Response.OutputStream.Close();
    }

    public void Stop()
    {
        if (_isDisposed) return;
        try
        {
            _cts?.Cancel();
            _listener.Stop();
        }
        catch
        {
            // 忽略关闭
        }
    }

    public void Dispose()
    {
        if (_isDisposed) return;
        _isDisposed = true;
        Stop();
        _cts?.Dispose();
        _listener.Close();
    }

    // 内嵌单页暗黑高颜值响应式控制台 HTML/CSS/JS (自包含无需任何外部 CDN)
    private const string DashboardHtmlContent = @"<!DOCTYPE html>
<html lang=""zh-CN"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>热血传奇 1.5 - 服务端实时监控面板</title>
    <style>
        :root {
            --bg-main: #12141a;
            --bg-card: #1c1f26;
            --border-color: #2e3440;
            --accent-gold: #e5a93b;
            --accent-green: #4cd964;
            --accent-red: #ff3b30;
            --accent-blue: #0a84ff;
            --text-light: #eceff4;
            --text-muted: #8892b0;
        }
        * { box-sizing: border-box; margin: 0; padding: 0; font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif; }
        body { background-color: var(--bg-main); color: var(--text-light); padding: 20px; font-size: 14px; }
        .header { display: flex; justify-content: space-between; align-items: center; border-bottom: 2px solid var(--accent-gold); padding-bottom: 12px; margin-bottom: 20px; }
        .header h1 { font-size: 20px; color: var(--accent-gold); display: flex; align-items: center; gap: 10px; }
        .tag { background: #2b303c; padding: 4px 8px; border-radius: 4px; font-size: 12px; color: var(--accent-green); }
        .grid { display: grid; grid-template-columns: repeat(auto-fit, minmax(200px, 1fr)); gap: 16px; margin-bottom: 24px; }
        .card { background: var(--bg-card); border: 1px solid var(--border-color); border-radius: 8px; padding: 16px; }
        .card .title { font-size: 12px; color: var(--text-muted); margin-bottom: 8px; text-transform: uppercase; }
        .card .val { font-size: 26px; font-weight: bold; color: #fff; }
        .card .sub { font-size: 12px; color: var(--accent-gold); margin-top: 4px; }
        .panel { background: var(--bg-card); border: 1px solid var(--border-color); border-radius: 8px; padding: 20px; margin-bottom: 24px; }
        .panel h2 { font-size: 16px; margin-bottom: 16px; color: var(--accent-gold); }
        .btn-group { display: flex; flex-wrap: wrap; gap: 10px; margin-bottom: 16px; }
        button { background: var(--accent-gold); color: #000; border: none; padding: 10px 18px; border-radius: 6px; font-weight: bold; cursor: pointer; transition: all 0.2s; }
        button:hover { opacity: 0.85; transform: translateY(-1px); }
        button.danger { background: var(--accent-red); color: #fff; }
        button.blue { background: var(--accent-blue); color: #fff; }
        button.green { background: var(--accent-green); color: #000; }
        input[type=""text""] { background: #12141a; border: 1px solid var(--border-color); color: #fff; padding: 10px 14px; border-radius: 6px; flex: 1; min-width: 250px; }
        .input-row { display: flex; gap: 10px; margin-bottom: 16px; }
        table { width: 100%; border-collapse: collapse; margin-top: 10px; }
        th, td { text-align: left; padding: 10px 12px; border-bottom: 1px solid var(--border-color); }
        th { color: var(--text-muted); font-size: 12px; }
        .status-dot { width: 8px; height: 8px; border-radius: 50%; background: var(--accent-green); display: inline-block; margin-right: 6px; }
        .log-box { background: #000; color: #39ff14; font-family: monospace; font-size: 12px; padding: 12px; border-radius: 6px; height: 120px; overflow-y: auto; }
    </style>
</head>
<body>
    <div class=""header"">
        <h1>⚔️ 盛大传奇 1.5 原生 M2 服务端控制总台 <span class=""tag"">Native AOT 极速运行</span></h1>
        <div>
            <span>Token: </span><input type=""password"" id=""adminToken"" value=""{{TOKEN}}"" style=""background:#222;border:1px solid #444;color:#aaa;padding:4px;border-radius:4px;width:160px;"">
            <span class=""status-dot""></span><span id=""statusText"">监控运行中 (每 2s 自动刷新)</span>
        </div>
    </div>

    <div class=""grid"">
        <div class=""card"">
            <div class=""title"">当前在线玩家</div>
            <div class=""val"" id=""valOnline"">0</div>
            <div class=""sub"">千人同屏高并发网络</div>
        </div>
        <div class=""card"">
            <div class=""title"">全服活跃怪物</div>
            <div class=""val"" id=""valMonsters"">0</div>
            <div class=""sub"">AI 心跳与视野驱动</div>
        </div>
        <div class=""card"">
            <div class=""title"">地表实体存量</div>
            <div class=""val"" id=""valGround"">0</div>
            <div class=""sub"">120秒自动衰减机制</div>
        </div>
        <div class=""card"">
            <div class=""title"">Tick 耗时 (均值/峰值)</div>
            <div class=""val""><span id=""valTick"">0.00</span> <span style=""font-size:14px;color:#aaa"">/ <span id=""valMaxTick"">0.00</span> ms</span></div>
            <div class=""sub"">50ms 权威世界帧</div>
        </div>
        <div class=""card"">
            <div class=""title"">内存物理消耗</div>
            <div class=""val""><span id=""valMem"">0</span> <span style=""font-size:14px;color:#aaa"">MB</span></div>
            <div class=""sub"">零堆分配缓冲池</div>
        </div>
        <div class=""card"">
            <div class=""title"">系统运行时间</div>
            <div class=""val"" id=""valUptime"">0s</div>
            <div class=""sub"">高可用服务</div>
        </div>
    </div>

    <div class=""panel"">
        <h2>🛠️ 运维一键快速操作</h2>
        <div class=""btn-group"">
            <button class=""green"" onclick=""execAction('save')"">💾 全服数据安全存盘</button>
            <button class=""blue"" onclick=""execAction('reload')"">🔄 热重载 NPC 脚本插件</button>
            <button onclick=""execAction('clean')"">🧹 清理地表过期掉落物</button>
        </div>
        <div class=""input-row"">
            <input type=""text"" id=""broadcastInput"" placeholder=""输入全服红字系统广播公告..."">
            <button onclick=""sendBroadcast()"">📢 发送全服广播</button>
        </div>
    </div>

    <div class=""panel"">
        <h2>👥 在线玩家列表</h2>
        <table>
            <thead>
                <tr>
                    <th>角色信息</th>
                    <th>操作</th>
                </tr>
            </thead>
            <tbody id=""playerTableBody"">
                <tr><td colspan=""2"" style=""color:#666;"">暂无在线玩家</td></tr>
            </tbody>
        </table>
    </div>

    <div class=""panel"">
        <h2>📜 操作反馈控制台日志</h2>
        <div class=""log-box"" id=""logBox"">
            [系统就绪] Web 管理员监控仪表盘初始化完毕。<br>
        </div>
    </div>

    <script>
        function log(msg) {
            const box = document.getElementById('logBox');
            box.innerHTML += `[${new Date().toLocaleTimeString()}] ${msg}<br>`;
            box.scrollTop = box.scrollHeight;
        }

        function getToken() {
            return document.getElementById('adminToken').value.trim();
        }

        async function fetchStats() {
            try {
                const res = await fetch('/api/stats');
                if (!res.ok) return;
                const d = await res.json();
                document.getElementById('valOnline').innerText = d.onlinePlayers;
                document.getElementById('valMonsters').innerText = d.totalMonsters;
                document.getElementById('valGround').innerText = d.groundItems;
                document.getElementById('valTick').innerText = d.lastTickMs.toFixed(2);
                document.getElementById('valMaxTick').innerText = d.maxTickMs.toFixed(2);
                document.getElementById('valMem').innerText = d.memoryMb;
                document.getElementById('valUptime').innerText = d.upTimeSeconds + 's';

                const tbody = document.getElementById('playerTableBody');
                if (d.playerList && d.playerList.length > 0) {
                    tbody.innerHTML = d.playerList.map(p => {
                        const name = p.split(' ')[0];
                        return `<tr>
                            <td>${p}</td>
                            <td><button class=""danger"" style=""padding:4px 10px;font-size:12px;"" onclick=""kickPlayer('${name}')"">踢出</button></td>
                        </tr>`;
                    }).join('');
                } else {
                    tbody.innerHTML = '<tr><td colspan=""2"" style=""color:#666;"">当前无在线玩家</td></tr>';
                }
            } catch (e) {
                document.getElementById('statusText').innerText = '连接中断重试中...';
            }
        }

        async function execAction(action) {
            const token = getToken();
            log(`正在执行操作: ${action}...`);
            try {
                const res = await fetch(`/api/action/${action}?token=${encodeURIComponent(token)}`, {
                    method: 'POST',
                    headers: { 'Authorization': `Bearer ${token}` }
                });
                const data = await res.json();
                log(data.message || '操作完成');
                fetchStats();
            } catch (e) {
                log('操作执行失败: ' + e);
            }
        }

        async function sendBroadcast() {
            const text = document.getElementById('broadcastInput').value.trim();
            if (!text) return alert('请输入广播内容！');
            const token = getToken();
            try {
                const res = await fetch(`/api/action/broadcast?token=${encodeURIComponent(token)}`, {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json', 'Authorization': `Bearer ${token}` },
                    body: JSON.stringify({ message: text })
                });
                const data = await res.json();
                log(data.message);
                document.getElementById('broadcastInput').value = '';
            } catch (e) {
                log('广播失败: ' + e);
            }
        }

        async function kickPlayer(name) {
            if (!confirm(`确定强制将角色 [${name}] 踢下线吗？`)) return;
            const token = getToken();
            try {
                const res = await fetch(`/api/action/kick?name=${encodeURIComponent(name)}&token=${encodeURIComponent(token)}`, {
                    method: 'POST',
                    headers: { 'Authorization': `Bearer ${token}` }
                });
                const data = await res.json();
                log(data.message);
                fetchStats();
            } catch (e) {
                log('踢人失败: ' + e);
            }
        }

        setInterval(fetchStats, 2000);
        fetchStats();
    </script>
</body>
</html>";
}
