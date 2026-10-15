using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Mir2Client.Audio;
using Mir2Client.Net;
using Mir2Client.Render;
using Mir2Client.Utils;
using Mir2.Shared.Protocols.Modern;

namespace Mir2Client;

/// <summary>
/// 客户端主入口与生命周期状态机 (纯 C# 实现)
/// 管理 Login -> SelectServer -> SelectChar -> Game 阶段迁移
/// </summary>
public partial class Main : Node
{
    public enum Phase
    {
        Login,
        SelectServer,
        SelectChar,
        Game
    }

    public const string DefaultHost = "127.0.0.1";
    public const int DefaultPort = 7000;

    private Phase _currentPhase = Phase.Login;
    private NetManager? _net;
    private AudioManager? _audio;

    private string _account = string.Empty;
    private string _password = string.Empty;
    private string _selectedCharName = "热血战士";
    private int _selectedCharLevel = 35;
    private int _selectedCharGender = 0;
    private int _selectedCharJob = 0;
    private int _sessionId = 1;

    private readonly List<string> _serverList = new();
    private readonly List<UI.SelectCharView.CharEntry> _serverCharList = new();

    public Phase CurrentPhase => _currentPhase;

    private Node? _currentSceneNode;

    public override void _Ready()
    {
        _net = GetNodeOrNull<NetManager>("/root/NetManager");
        _audio = GetNodeOrNull<AudioManager>("/root/AudioManager");

        if (_net != null)
        {
            _net.PacketReceived += OnGlobalPacketReceived;
            _net.BinaryPacketReceived += (msgId, payload) => OnGlobalPacketReceived((short)msgId, payload);
            _net.Connected += OnNetworkConnected;
            _net.ConnectFailed += OnNetworkConnectFailed;
            _net.Disconnected += OnNetworkDisconnected;
        }

        SetupCustomCursor();
        DisplayServer.WindowSetTitle("热血传奇 1.50 - 虎卫传说 (官方 1:1 C# 工业级单轨版)");
        FontManager.AttachTreeAutoStyler(GetTree());
        GD.Print("[Main] 盛大传奇 1.5 C# 主入口已就绪 (官方中易宋体全局样式已挂载)");

        SwitchPhase(Phase.Login);

        // 如果携带 --autotest 或 --debug-grid 命令行参数，启动 1 像素控件边框审计层与自动化驱动器
        var args = OS.GetCmdlineArgs();
        var userArgs = OS.GetCmdlineUserArgs();
        var allArgs = new List<string>();
        if (args != null) allArgs.AddRange(args);
        if (userArgs != null) allArgs.AddRange(userArgs);
        GD.Print($"[Main] 收到启动参数 [{allArgs.Count}]: {string.Join(" | ", allArgs)}");

        // 满足人工与自动化机器审计：启动 1 像素高对比度细线框审计层（当携带 --debug-grid 时启用，避免 --autotest 残留调试辅助线）
        if (allArgs.Any(a => a.Contains("debug-grid")))
        {
            AddChild(new DebugGridOverlay());
        }

#if MIR2_SANDBOX_DEV
        if (allArgs.Any(a => a.Contains("autotest") || a.Contains("auto-e2e")))
        {
            AddChild(new AutoEndToEndDriver());
        }
#endif
        if (allArgs.Any(a => a.Contains("auto-screenshot") || a.Contains("auto-live-screenshot")))
        {
            GD.Print("[Main] 正在挂载 AutoLiveScreenshotRunner 巡检驱动器...");
            AddChild(new AutoLiveScreenshotRunner());
        }
    }

    public Node? CurrentSceneNode => _currentSceneNode;

    public override void _Notification(int what)
    {
        if (what == NotificationWMCloseRequest)
        {
            GD.Print("[Main] 收到窗口关闭请求 (NotificationWMCloseRequest)，执行安全退出...");
            AppLifecycle.SafeQuit(0);
        }
    }

    public override void _ExitTree()
    {
        // 退出前释放自定义鼠标指针纹理,避免引擎退出期纹理泄漏 ERROR
        Input.SetCustomMouseCursor(null, Input.CursorShape.Arrow, Vector2.Zero);
        _net?.DisconnectFromServer();
    }

    private static void SetupCustomCursor()
    {
        string cursorPath = ResPath.Resolve("resources/wil/png/Prguse/Prguse_0000.png");
        var tex = ResPath.LoadImageTexture(cursorPath);
        if (tex != null)
        {
            Input.SetCustomMouseCursor(tex, Input.CursorShape.Arrow, Vector2.Zero);
        }
    }

    public void SwitchPhase(Phase nextPhase)
    {
        _currentPhase = nextPhase;
        GD.Print($"[Main] 切换游戏阶段: {nextPhase}");

        if (_currentSceneNode != null)
        {
            _currentSceneNode.QueueFree();
            _currentSceneNode = null;
        }

        switch (nextPhase)
        {
            case Phase.Login:
            {
                AudioManager.Instance?.PlayBgm("Log-in-long2.wav", loop: true);
                var loginScene = GD.Load<PackedScene>("res://scenes/Login.tscn");
                if (loginScene != null)
                {
                    var node = loginScene.Instantiate<UI.LoginView>();
                    node.ServerHost = DefaultHost;
                    node.ServerPort = DefaultPort;
                    node.LoginRequested += OnLoginRequested;
                    node.RegisterRequested += OnRegisterRequested;
                    node.ChangePasswordRequested += OnChangePasswordRequested;
                    AddChild(node);
                    _currentSceneNode = node;
                }
                break;
            }
            case Phase.SelectServer:
            {
                AudioManager.Instance?.PlayBgm("Log-in-long2.wav", loop: true);
                var srvScene = GD.Load<PackedScene>("res://scenes/SelectServer.tscn");
                if (srvScene != null)
                {
                    var node = srvScene.Instantiate<UI.SelectServerView>();
                    if (_serverList.Count > 0)
                    {
                        node.SetServers(_serverList);
                    }
                    node.ServerSelected += (idx) =>
                    {
                        if (_net != null && _net.IsConnectedToServer)
                        {
                            var selServ = new BinaryProtocol.SelectServerIntentPayload { ServerIndex = idx };
                            _net.SendBinaryIntent((ushort)BinaryProtocol.MsgType.IntentSelectServer, in selServ);
                            var queryChar = BinaryProtocol.QueryCharIntentPayload.Create(_account);
                            _net.SendBinaryIntent((ushort)BinaryProtocol.MsgType.IntentQueryChar, in queryChar);
                        }
#if MIR2_SANDBOX_DEV
                        else
                        {
                            SwitchPhase(Phase.SelectChar);
                        }
#else
                        else
                        {
                            if (_currentSceneNode is UI.SelectServerView srvView)
                            {
                                srvView.SetStatusText("未连接到游戏网关，请检查网络！");
                            }
                        }
#endif
                    };
                    AddChild(node);
                    _currentSceneNode = node;
                }
                break;
            }
            case Phase.SelectChar:
            {
                AudioManager.Instance?.PlayBgm("sellect-loop2.wav", loop: true);
                var charScene = GD.Load<PackedScene>("res://scenes/SelectChar.tscn");
                if (charScene != null)
                {
                    var node = charScene.Instantiate<UI.SelectCharView>();
                    if (_serverCharList.Count > 0)
                    {
                        node.SetCharacters(_serverCharList);
                    }
#if MIR2_SANDBOX_DEV
                    else
                    {
                        node.SetCharacters(new[]
                        {
                            new UI.SelectCharView.CharEntry("热血战士", 0, 0, 35),
                            new UI.SelectCharView.CharEntry("烈焰法师", 1, 1, 38)
                        });
                    }
#endif
                    node.CharSelected += (idx, name, lvl, sex, job) =>
                    {
                        _selectedCharName = name;
                        _selectedCharLevel = lvl;
                        _selectedCharGender = sex;
                        _selectedCharJob = job;
                        if (_net != null && _net.IsConnectedToServer)
                        {
                            var selChar = BinaryProtocol.SelectCharIntentPayload.Create(idx, name);
                            _net.SendBinaryIntent((ushort)BinaryProtocol.MsgType.SelectCharRequest, in selChar);
                        }
#if MIR2_SANDBOX_DEV
                        else
                        {
                            SwitchPhase(Phase.Game);
                        }
#else
                        else
                        {
                            if (_currentSceneNode is UI.SelectCharView selView)
                            {
                                selView.ShowMessageModal("未连接到游戏服务器，无法开始游戏！");
                            }
                        }
#endif
                    };
                    node.NewCharRequested += (name, job, sex, hair) =>
                    {
                        if (_net != null && _net.IsConnectedToServer)
                        {
                            var newChar = BinaryProtocol.CreateCharIntentPayload.Create(name, (byte)job, (byte)sex, (byte)(hair + 1));
                            _net.SendBinaryIntent((ushort)BinaryProtocol.MsgType.CreateCharRequest, in newChar);
                        }
                    };
                    node.DelCharRequested += (idx) =>
                    {
                        if (_net != null && _net.IsConnectedToServer)
                        {
                            var delChar = BinaryProtocol.DeleteCharIntentPayload.Create(idx);
                            _net.SendBinaryIntent((ushort)BinaryProtocol.MsgType.IntentDeleteChar, in delChar);
                        }
                    };
                    AddChild(node);
                    _currentSceneNode = node;
                }
                break;
            }
            case Phase.Game:
            {
                AudioManager.Instance?.PlayBgm("main_theme.wav", loop: false);
                var gameScene = GD.Load<PackedScene>("res://scenes/Game.tscn");
                if (gameScene != null)
                {
                    var node = gameScene.Instantiate<UI.GameView>();
                    AddChild(node);
                    _currentSceneNode = node;
                    if (_net != null)
                    {
                        node.SetNet(_net, _sessionId, _selectedCharName, _selectedCharLevel, _selectedCharGender, _selectedCharJob);
                        if (_net.IsConnectedToServer)
                        {
                            var loginOk = new BinaryProtocol.LoginNoticeOkPayload { Acknowledged = 1 };
                            _net.SendBinaryIntent((ushort)BinaryProtocol.MsgType.IntentLoginNoticeOk, in loginOk);
                        }
                    }
                }
                break;
            }
        }

        if (_currentSceneNode != null)
        {
            FontManager.ApplyRecursively(_currentSceneNode);
        }
    }

    private void OnLoginRequested(string host, int port, string user, string pass)
    {
        GD.Print($"[Main] 收到登录请求: {host}:{port}, 用户: {user}");
        _account = user;
        _password = pass;

        if (_net == null)
        {
            if (_currentSceneNode is UI.LoginView loginView)
            {
                loginView.ShowMessageModal("网络通信组件未初始化！");
            }
            return;
        }

        if (_net.IsConnectedToServer)
        {
            var loginIntent = BinaryProtocol.LoginIntentPayload.Create(user, pass);
            _net.SendBinaryIntent((ushort)BinaryProtocol.MsgType.LoginRequest, in loginIntent);
        }
        else
        {
            GD.Print($"[Main] 正在异步发起连接 {host}:{port}...");
            _net.ConnectTo(host, port, autoReconnect: false);
        }

#if MIR2_SANDBOX_DEV
        // 开发者沙盒演练模式：若处于离线单机开发环境，允许本地直接触发石门动画
        if (!_net.IsConnectedToServer)
        {
            if (_currentSceneNode is UI.LoginView sandboxLoginView)
            {
                GD.Print("[Main] [SANDBOX] 开发离线模式播放石门开启序列动画 (23~32帧)...");
                sandboxLoginView.PlayDoorOpeningAnimation(() =>
                {
                    SwitchPhase(Phase.SelectServer);
                });
            }
            else
            {
                SwitchPhase(Phase.SelectServer);
            }
        }
#endif
    }

    private Action? _onConnectedAction;

    private void OnRegisterRequested(string acc, string pwd)
    {
        GD.Print($"[Main] 处理新账号注册请求: {acc}");
        if (_net == null) return;
        Action sendRegister = () =>
        {
            var regIntent = BinaryProtocol.CreateAccountPayload.Create(acc, pwd);
            _net.SendBinaryIntent((ushort)BinaryProtocol.MsgType.CreateAccountRequest, in regIntent);
        };

        if (_net.IsConnectedToServer)
        {
            sendRegister();
        }
        else
        {
            _onConnectedAction = sendRegister;
            _net.ConnectTo(DefaultHost, DefaultPort, autoReconnect: false);
        }
    }

    private void OnChangePasswordRequested(string acc, string oldP, string newP)
    {
        GD.Print($"[Main] 处理修改密码请求: {acc}");
        if (_net == null) return;
        Action sendChange = () =>
        {
            var chgIntent = BinaryProtocol.ChangePasswordPayload.Create(acc, oldP, newP);
            _net.SendBinaryIntent((ushort)BinaryProtocol.MsgType.ChangePasswordRequest, in chgIntent);
        };

        if (_net.IsConnectedToServer)
        {
            sendChange();
        }
        else
        {
            _onConnectedAction = sendChange;
            _net.ConnectTo(DefaultHost, DefaultPort, autoReconnect: false);
        }
    }

    private void OnNetworkConnected()
    {
        GD.Print($"[Main] 网络成功连接到服务器，当前生命周期阶段: {_currentPhase}");
        if (_onConnectedAction != null)
        {
            var action = _onConnectedAction;
            _onConnectedAction = null;
            action();
            return;
        }

        if (_currentPhase == Phase.Login && !string.IsNullOrEmpty(_account))
        {
            GD.Print($"[Main] 连接建立完毕，发送登录封包: account={_account}");
            var loginIntent = BinaryProtocol.LoginIntentPayload.Create(_account, _password);
            _net?.SendBinaryIntent((ushort)BinaryProtocol.MsgType.LoginRequest, in loginIntent);
        }
    }

    private void OnNetworkConnectFailed()
    {
        GD.PrintErr($"[Main] 连接游戏服务器失败，当前生命周期阶段: {_currentPhase}");
        if (_currentSceneNode is UI.LoginView loginView)
        {
            loginView.ShowMessageModal("无法连接到游戏服务器，请检查网络连接！");
        }
    }

    private string? _lastDisconnectReason;

    private void OnNetworkDisconnected(string reason)
    {
        GD.PrintErr($"[Main] 与游戏服务器断开连接，原因: {reason}，当前阶段: {_currentPhase}");
        if (_currentSceneNode is UI.LoginView loginView)
        {
            string showMsg = !string.IsNullOrEmpty(_lastDisconnectReason)
                ? _lastDisconnectReason
                : "与服务器断开连接！";
            _lastDisconnectReason = null;
            loginView.ShowMessageModal(showMsg);
        }
    }

    private static ServerPacket? DecodeServerPacket(short smCode, byte[] data)
    {
        if (data == null || data.Length == 0) return null;
        if (data.Length >= 16)
        {
            return PacketDecoder.ParseModernPayload(smCode, data);
        }
        return PacketDecoder.Parse(data);
    }

    private void OnGlobalPacketReceived(short smCode, byte[] data)
    {
        switch (smCode)
        {
            case ProtocolConsts.SM_OUTOFCONNECTION: // 50: 盛大官方 1.5 断线提示 (账号密码错误、锁定、被踢下线)
            {
                var parsed = DecodeServerPacket(smCode, data);
                string text = parsed?.ExtraStr ?? string.Empty;
                if (string.IsNullOrWhiteSpace(text) && data != null && data.Length > 0)
                {
                    try { text = System.Text.Encoding.UTF8.GetString(data); } catch { }
                }
                if (string.IsNullOrWhiteSpace(text)) text = "账号或密码错误，请检查后重试！";
                GD.Print($"[Main] 收到服务端错误提示 SM_50: {text}");
                _lastDisconnectReason = text;
                if (_currentSceneNode is UI.LoginView lv)
                {
                    lv.ShowMessageModal(text);
                }
                break;
            }
            case ProtocolConsts.SM_STARTPLAY: // 20: 盛大官方 1.5 原版角色进入游戏世界确认 (IDA 0x468867)
            {
                GD.Print($"[Main] 收到服务端进入游戏确认协议 SM_{smCode}，角色身份验证通过，进入游戏大世界！");
                if (_currentPhase == Phase.SelectChar)
                {
                    SwitchPhase(Phase.Game);
                }
                break;
            }
            case ProtocolConsts.SM_NEWID_SUCCESS: // 504 盛大官方 1.5 账号注册成功
            {
                var parsed = DecodeServerPacket(smCode, data);
                string text = parsed?.ExtraStr ?? string.Empty;
                GD.Print($"[Main] 收到官方 1.5 账号注册成功回执: {text}");
                if (_currentSceneNode is UI.LoginView lv)
                {
                    lv.SwitchSubState(0);
                    if (!string.IsNullOrEmpty(text))
                    {
                        lv.SetUsername(text);
                    }
                    lv.ShowMessageModal("账号注册成功！请直接输入密码登录。");
                }
                break;
            }
            case ProtocolConsts.SM_NEWID_FAIL: // 505 盛大官方 1.5 账号注册失败
            {
                var parsed = DecodeServerPacket(smCode, data);
                string text = parsed?.ExtraStr ?? string.Empty;
                if (string.IsNullOrWhiteSpace(text)) text = "该账号已被占用或注册信息不合法！";
                GD.Print($"[Main] 收到官方 1.5 账号注册失败回执: {text}");
                if (_currentSceneNode is UI.LoginView lv)
                {
                    lv.ShowMessageModal(text);
                }
                break;
            }
            case ProtocolConsts.SM_CHGPASSWD_SUCCESS: // 506
            case ProtocolConsts.SM_CHANGEPASS_OK: // 532 盛大官方 1.5 密码修改成功
            {
                GD.Print("[Main] 收到官方 1.5 密码修改成功回执");
                if (_currentSceneNode is UI.LoginView lv)
                {
                    lv.SwitchSubState(0);
                    lv.ShowMessageModal("密码修改成功！请使用新密码重新登录。");
                }
                break;
            }
            case ProtocolConsts.SM_CHANGEPASS_FAIL: // 503 盛大官方 1.5 密码修改失败
            {
                var parsed = DecodeServerPacket(smCode, data);
                string text = parsed?.ExtraStr ?? string.Empty;
                if (string.IsNullOrWhiteSpace(text)) text = "原密码不正确或账号不存在！";
                GD.Print($"[Main] 收到官方 1.5 密码修改失败回执: {text}");
                if (_currentSceneNode is UI.LoginView lv)
                {
                    lv.ShowMessageModal(text);
                }
                break;
            }
            case ProtocolConsts.SM_PASSOK: // 500
            case 530: // SM_LOGINSUCCESS
            {
                GD.Print($"[Main] 收到登录成功回执 SM_{smCode}，启动官方石门开启序列动画...");
                if (_currentPhase == Phase.Login)
                {
                    if (_currentSceneNode is UI.LoginView lv)
                    {
                        lv.PlayDoorOpeningAnimation(() =>
                        {
                            SwitchPhase(Phase.SelectServer);
                        });
                    }
                    else
                    {
                        SwitchPhase(Phase.SelectServer);
                    }
                }
                break;
            }
            case 114: // SM_SERVERINDEX (服务端下发分区列表)
            {
                var parsed = DecodeServerPacket(smCode, data);
                string text = parsed?.ExtraStr ?? string.Empty;
                if (!string.IsNullOrEmpty(text))
                {
                    var srvs = text.Split(new[] { '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                    if (srvs.Length > 0)
                    {
                        _serverList.Clear();
                        _serverList.AddRange(srvs);
                        if (_currentSceneNode is UI.SelectServerView srvView)
                        {
                            srvView.SetServers(_serverList);
                        }
                    }
                }
                break;
            }
            case ProtocolConsts.SM_QUERYCHR: // 518
            case ProtocolConsts.SM_CHARLIST_DELPHI: // 211: 盛大官方 1.5 原版角色列表下发 (IDA case 0xD3u @ 0x469AE0 handle_sm_char_list)
            case 120: // SM_CHARLIST (官方 1.5 桥接网关下发角色列表)
            case ProtocolConsts.SM_QUERYCHAR_OK: // 523: 盛大官方 1.5 查询角色成功进入角色槽位
            {
                var parsed = DecodeServerPacket(smCode, data);
                string text = parsed?.ExtraStr ?? string.Empty;
                var list = new List<UI.SelectCharView.CharEntry>();
                if (!string.IsNullOrEmpty(text))
                {
                    var lines = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var l in lines)
                    {
                        var parts = l.Split(new[] { '\t', '/' }, StringSplitOptions.None);
                        if (parts.Length >= 4)
                        {
                            string cname = parts[0];
                            int job = int.TryParse(parts[1], out var j) ? j : 0;
                            int level = int.TryParse(parts[2], out var lvl) ? lvl : 1;
                            int sex = int.TryParse(parts[3], out var s) ? s : 0;
                            list.Add(new UI.SelectCharView.CharEntry(cname, job, sex, level));
                        }
                    }
                }
                
                _serverCharList.Clear();
                _serverCharList.AddRange(list);
                if (_currentPhase == Phase.SelectServer || _currentPhase == Phase.Login)
                {
                    SwitchPhase(Phase.SelectChar);
                }
                else if (_currentSceneNode is UI.SelectCharView selView)
                {
                    selView.SetCharacters(list);
                }
                break;
            }
            case ProtocolConsts.SM_NEWCHR_SUCCESS: // 121
            case ProtocolConsts.SM_DELCHR_SUCCESS: // 123
            case ProtocolConsts.SM_DELCHAR_DELPHI: // 212: 盛大官方 1.5 原版删角成功 (IDA case 0xD4u @ 0x469B00 handle_sm_char_list_del)
            {
                GD.Print($"[Main] 角色创建/删除成功，重新向网关查询角色列表 (SM_{smCode})");
                if (_net != null && _net.IsConnectedToServer)
                {
                    var queryChar = BinaryProtocol.QueryCharIntentPayload.Create(_account);
                    _net.SendBinaryIntent((ushort)BinaryProtocol.MsgType.IntentQueryChar, in queryChar);
                }
                break;
            }
            case ProtocolConsts.SM_NEWCHR_FAIL: // 122
            case ProtocolConsts.SM_NEWCHAR_FAIL_DELPHI: // 522: 盛大官方 1.5 原版创建角色失败 (IDA case 522 @ 0x46B4C8~0x46B55C)
            {
                var parsed = DecodeServerPacket(smCode, data);
                int code = parsed?.Recog ?? 0;
                string failMsg = code switch
                {
                    2 => "角色建立失败：这个名字已经存在！", // 0x46B4C8
                    3 => "角色建立失败：每个帐号最多只能建立两个角色！", // 0x46B4F0
                    4 => "角色建立失败：角色名称不合规范，长度应在1至14个字符之间！", // 0x46B534
                    _ => "角色建立失败：发生未知错误，请联系游戏管理员。" // 0x46B55C
                };
                GD.Print($"[Main] 官方 1.5 创角失败模态弹窗: code={code}, msg={failMsg}");
                if (_currentSceneNode is UI.SelectCharView selView)
                {
                    selView.ShowMessageModal(failMsg);
                }
                break;
            }
            case ProtocolConsts.SM_DELCHR_FAIL: // 124
            case ProtocolConsts.SM_DELCHAR_FAIL_DELPHI: // 524: 盛大官方 1.5 原版删除角色失败 (IDA case 524 @ 0x46B624)
            {
                string failMsg = "[Failure] 删除角色失败, Email: support@legendofmir.com.cn"; // 0x46B624
                GD.Print($"[Main] 官方 1.5 删角失败模态弹窗: {failMsg}");
                if (_currentSceneNode is UI.SelectCharView selView)
                {
                    selView.ShowMessageModal(failMsg);
                }
                break;
            }
            case ProtocolConsts.SM_SELECTSERVER_FAIL: // 507: 盛大官方 1.5 选服连接失败 (IDA case 507 @ 0x46B5DC 0x46B668)
            {
                var parsed = DecodeServerPacket(smCode, data);
                int code = parsed?.Recog ?? 0;
                string failMsg = code == -2
                    ? "帐户被锁定 .请稍后再试。" // 0x46B5DC
                    : "您选择的服务器用户满员。"; // 0x46B668
                GD.Print($"[Main] 官方 1.5 选服失败模态提示: code={code}, msg={failMsg}");
                break;
            }
            case ProtocolConsts.SM_QUERYCHAR_FAIL: // 528: 盛大官方 1.5 查询角色失败/强制断线 (IDA case 528 @ 0x46B758)
            {
                string failMsg = "服务器连接被强行中断。\n连接时间可能超过限制，\n或者用户请求重新连接。"; // 0x46B758
                GD.Print($"[Main] 官方 1.5 连接强行中断: {failMsg}");
                break;
            }
        }
    }
}
