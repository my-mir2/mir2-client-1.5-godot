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
    public const int DefaultPort = 7100;

    private Phase _currentPhase = Phase.Login;
    private NetManager? _net;
    private AudioManager? _audio;

    private string _account = string.Empty;
    private string _password = string.Empty;
    private string _selectedCharName = "热血战士";
    private int _selectedCharLevel = 35;
    private int _sessionId = 1;

    public Phase CurrentPhase => _currentPhase;

    private Node? _currentSceneNode;

    public override void _Ready()
    {
        _net = GetNodeOrNull<NetManager>("/root/NetManager");
        _audio = GetNodeOrNull<AudioManager>("/root/AudioManager");

        if (_net != null)
        {
            _net.PacketReceived += OnGlobalPacketReceived;
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

        // 满足人工与自动化机器审计：启动 1 像素高对比度细线框审计层（当携带 --debug-grid 或 --autotest 时自动启用）
        if (allArgs.Contains("--debug-grid") || allArgs.Contains("--autotest"))
        {
            AddChild(new DebugGridOverlay());
        }

        if (allArgs.Contains("--autotest"))
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
                    node.ServerSelected += (idx) => SwitchPhase(Phase.SelectChar);
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
                    node.SetCharacters(new[]
                    {
                        new UI.SelectCharView.CharEntry("热血战士", 0, 0, 35),
                        new UI.SelectCharView.CharEntry("烈焰法师", 1, 1, 38)
                    });
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
            _net.ConnectTo(host, port, autoReconnect: false);
            if (_net.IsConnectedToServer)
            {
                _net.SendPacket(PacketEncoder.Login(user, pass));
            }
        }

        // 官方 1.5 逆向实证 (IDA sub_47D6CC @ 0x47D859):
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

    private void OnGlobalPacketReceived(short smCode, byte[] data)
    {
        switch (smCode)
        {
            case ProtocolConsts.SM_QUERYCHR:
            {
                if (_currentSceneNode is UI.SelectCharView selView && data != null)
                {
                    // 动态解析下发的角色列表
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
                                int gender = int.TryParse(parts[2], out var g) ? g : 0;
                                int level = int.TryParse(parts[3], out var lvl) ? lvl : 1;
                                list.Add(new UI.SelectCharView.CharEntry(cname, job, gender, level));
                            }
                        }
                        if (list.Count > 0)
                        {
                            selView.SetCharacters(list);
                        }
                    }
                }
                break;
            }
            case ProtocolConsts.SM_NEWCHR_SUCCESS:
            case ProtocolConsts.SM_DELCHR_SUCCESS:
            {
                if (_net != null && _net.IsConnectedToServer)
                {
                    _net.SendPacket(PacketEncoder.QueryChr(_account));
                }
                break;
            }
        }
    }
}
