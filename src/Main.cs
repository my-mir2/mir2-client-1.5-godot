using System;
using System.Collections.Generic;
using Godot;
using Mir2Client.Audio;
using Mir2Client.Net;
using Mir2Client.Render;
using Mir2Client.Utils;

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
    public const int DefaultPort = 7001;

    private Phase _currentPhase = Phase.Login;
    private NetManager? _net;
    private AudioManager? _audio;

    private string _account = string.Empty;
    private string _password = string.Empty;
    private string _selectedCharName = "热血战士";
    private int _selectedCharLevel = 35;
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
            _net.Connected += OnNetworkConnected;
        }

        SetupCustomCursor();
        DisplayServer.WindowSetTitle("热血传奇 1.50 - 虎卫传说 (官方 1:1 C# 工业级单轨版)");
        GD.Print("[Main] 盛大传奇 1.5 C# 主入口已就绪");

        SwitchPhase(Phase.Login);

        // 如果携带 --autotest 或 --debug-grid 命令行参数，启动 1 像素控件边框审计层与自动化驱动器
        var args = OS.GetCmdlineArgs();
        var userArgs = OS.GetCmdlineUserArgs();
        var allArgs = new List<string>();
        if (args != null) allArgs.AddRange(args);
        if (userArgs != null) allArgs.AddRange(userArgs);

        // 满足人工与自动化机器审计：启动 1 像素高对比度细线框审计层（当携带 --debug-grid 时启用，避免 --autotest 残留调试辅助线）
        if (allArgs.Contains("--debug-grid"))
        {
            AddChild(new DebugGridOverlay());
        }

        if (allArgs.Contains("--autotest") || allArgs.Contains("--auto-e2e"))
        {
            AddChild(new AutoEndToEndDriver());
        }
    }

    public override void _ExitTree()
    {
        // 退出前释放自定义鼠标指针纹理,避免引擎退出期纹理泄漏 ERROR
        Input.SetCustomMouseCursor(null, Input.CursorShape.Arrow, Vector2.Zero);
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
                var loginScene = GD.Load<PackedScene>("res://scenes/Login.tscn");
                if (loginScene != null)
                {
                    var node = loginScene.Instantiate<UI.LoginView>();
                    node.ServerHost = DefaultHost;
                    node.ServerPort = DefaultPort;
                    node.LoginRequested += OnLoginRequested;
                    node.RegisterRequested += (acc, pwd) =>
                    {
                        GD.Print($"[Main] 收到新账号注册请求: {acc}");
                    };
                    node.ChangePasswordRequested += (acc, oldP, newP) =>
                    {
                        GD.Print($"[Main] 收到修改密码请求: {acc}");
                    };
                    AddChild(node);
                    _currentSceneNode = node;
                }
                break;
            }
            case Phase.SelectServer:
            {
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
                            _net.SendPacket(PacketEncoder.SelectServer(idx));
                            _net.SendPacket(PacketEncoder.MakePacket(119, 0, 0, 0, System.Text.Encoding.UTF8.GetBytes(_account)));
                        }
                        SwitchPhase(Phase.SelectChar);
                    };
                    AddChild(node);
                    _currentSceneNode = node;
                }
                break;
            }
            case Phase.SelectChar:
            {
                var charScene = GD.Load<PackedScene>("res://scenes/SelectChar.tscn");
                if (charScene != null)
                {
                    var node = charScene.Instantiate<UI.SelectCharView>();
                    if (_serverCharList.Count > 0)
                    {
                        node.SetCharacters(_serverCharList);
                    }
                    else
                    {
                        node.SetCharacters(new[]
                        {
                            new UI.SelectCharView.CharEntry("热血战士", 0, 0, 35),
                            new UI.SelectCharView.CharEntry("烈焰法师", 1, 1, 38)
                        });
                    }
                    node.CharSelected += (idx, name, lvl) =>
                    {
                        _selectedCharName = name;
                        _selectedCharLevel = lvl;
                        if (_net != null && _net.IsConnectedToServer)
                        {
                            _net.SendPacket(PacketEncoder.MakePacket(ProtocolConsts.CM_SELECTCHAR, idx, 0, 0, System.Text.Encoding.UTF8.GetBytes(name)));
                        }
                        SwitchPhase(Phase.Game);
                    };
                    node.NewCharRequested += (name, job, sex) =>
                    {
                        if (_net != null && _net.IsConnectedToServer)
                        {
                            _net.SendPacket(PacketEncoder.NewChar(name, job, sex));
                        }
                    };
                    node.DelCharRequested += (idx) =>
                    {
                        if (_net != null && _net.IsConnectedToServer)
                        {
                            _net.SendPacket(PacketEncoder.DelChar(idx));
                        }
                    };
                    AddChild(node);
                    _currentSceneNode = node;
                }
                break;
            }
            case Phase.Game:
            {
                var gameScene = GD.Load<PackedScene>("res://scenes/Game.tscn");
                if (gameScene != null)
                {
                    var node = gameScene.Instantiate<UI.GameView>();
                    AddChild(node);
                    _currentSceneNode = node;
                    if (_net != null)
                    {
                        node.SetNet(_net, _sessionId, _selectedCharName, _selectedCharLevel);
                        if (_net.IsConnectedToServer)
                        {
                            _net.SendPacket(PacketEncoder.LoginNoticeOk());
                        }
                    }
                }
                break;
            }
        }
    }

    private void OnLoginRequested(string host, int port, string user, string pass)
    {
        GD.Print($"[Main] 收到登录请求: {host}:{port}, 用户: {user}");
        _account = user;
        _password = pass;
        if (_net != null)
        {
            if (_net.IsConnectedToServer)
            {
                _net.SendPacket(PacketEncoder.Login(user, pass));
            }
            else
            {
                _net.ConnectTo(host, port, autoReconnect: false);
            }
        }

        // 官方 1.5 存疑：0x47D6CC 处无函数（已实证），原 0x47D859 调用点归属待查，流程逻辑不变：
        // 登录成功或进入选区时，先启动 ChrSel.wil[23..32] 10 帧石门开启序列动画，开门完毕后再切换至选服/选角
        if (_currentSceneNode is UI.LoginView loginView)
        {
            GD.Print("[Main] 播放官方石门开启序列动画 (23~32帧, 230ms 帧时钟)...");
            loginView.PlayDoorOpeningAnimation(() =>
            {
                SwitchPhase(Phase.SelectServer);
            });
        }
        else
        {
            SwitchPhase(Phase.SelectServer);
        }
    }

    private void OnNetworkConnected()
    {
        GD.Print($"[Main] 网络成功连接到服务器，当前生命周期阶段: {_currentPhase}");
        if (_currentPhase == Phase.Login && !string.IsNullOrEmpty(_account))
        {
            GD.Print($"[Main] 连接建立完毕，发送登录封包: account={_account}");
            _net?.SendPacket(PacketEncoder.Login(_account, _password));
        }
    }

    private void OnGlobalPacketReceived(short smCode, byte[] data)
    {
        switch (smCode)
        {
            case ProtocolConsts.SM_PASSOK: // 500
            case 530: // SM_LOGINSUCCESS
            {
                GD.Print($"[Main] 收到登录成功回执 SM_{smCode}");
                break;
            }
            case 114: // SM_SERVERINDEX (服务端下发分区列表)
            {
                var parsed = PacketDecoder.Parse(data);
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
            case 120: // SM_CHARLIST (官方 1.5 桥接网关下发角色列表)
            {
                var parsed = PacketDecoder.Parse(data);
                string text = parsed?.ExtraStr ?? string.Empty;
                if (!string.IsNullOrEmpty(text))
                {
                    var list = new List<UI.SelectCharView.CharEntry>();
                    var lines = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var l in lines)
                    {
                        var parts = l.Split(new[] { '\t', '/' }, StringSplitOptions.None);
                        if (parts.Length >= 4)
                        {
                            string cname = parts[0];
                            int job = int.TryParse(parts[1], out var j) ? j : 0;
                            int f2 = int.TryParse(parts[2], out var v2) ? v2 : 0;
                            int f3 = int.TryParse(parts[3], out var v3) ? v3 : 0;
                            int level = (f2 > 1) ? f2 : f3;
                            int gender = (f2 > 1) ? f3 : f2;
                            list.Add(new UI.SelectCharView.CharEntry(cname, job, gender, level));
                        }
                    }
                    if (list.Count > 0)
                    {
                        _serverCharList.Clear();
                        _serverCharList.AddRange(list);
                        if (_currentSceneNode is UI.SelectCharView selView)
                        {
                            selView.SetCharacters(list);
                        }
                    }
                }
                break;
            }
            case ProtocolConsts.SM_NEWCHR_SUCCESS: // 121
            case ProtocolConsts.SM_DELCHR_SUCCESS: // 123
            case ProtocolConsts.SM_DELCHAR_SUCCESS_DELPHI: // 523: 盛大官方 1.5 删角成功重新查询
            {
                GD.Print($"[Main] 角色创建/删除成功，重新向网关查询角色列表 (SM_{smCode})");
                if (_net != null && _net.IsConnectedToServer)
                {
                    _net.SendPacket(PacketEncoder.QueryChr(_account));
                }
                break;
            }
            case ProtocolConsts.SM_NEWCHR_FAIL: // 122
            case ProtocolConsts.SM_NEWCHAR_FAIL_DELPHI: // 522: 盛大官方 1.5 原版创建角色失败 (IDA case 522 @ 0x46B4C8~0x46B55C)
            {
                var parsed = PacketDecoder.Parse(data);
                int code = parsed?.Recog ?? 0;
                string failMsg = code switch
                {
                    2 => "[failure] 这个名字已经存在。", // 0x46B4C8
                    3 => "[Failure] 你只能为一个帐户设两个角色.\\请和游戏管理员联系。", // 0x46B4F0
                    4 => "[failure] 角色建立失败. Error=4", // 0x46B534
                    _ => "[Failure] 未知的错误. 请写信给: support@legendofmir.net" // 0x46B55C
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
                var parsed = PacketDecoder.Parse(data);
                int code = parsed?.Recog ?? 0;
                string failMsg = code == -2
                    ? "帐户被锁定 .请稍后再试。" // 0x46B5DC
                    : "您选择的服务器用户满员。"; // 0x46B668
                GD.Print($"[Main] 官方 1.5 选服失败模态提示: code={code}, msg={failMsg}");
                break;
            }
            case ProtocolConsts.SM_QUERYCHAR_FAIL: // 528: 盛大官方 1.5 查询角色失败/强制断线 (IDA case 528 @ 0x46B758)
            {
                string failMsg = "服务器连接被强行中断。\\连接时间可能超过限制，\\或者用户请求重新连接。"; // 0x46B758
                GD.Print($"[Main] 官方 1.5 连接强行中断: {failMsg}");
                break;
            }
        }
    }
}
