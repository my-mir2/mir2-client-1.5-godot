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

    public Phase CurrentPhase => _currentPhase;

    private Node? _currentSceneNode;

    public override void _Ready()
    {
        _net = GetNodeOrNull<NetManager>("/root/NetManager");
        _audio = GetNodeOrNull<AudioManager>("/root/AudioManager");

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
                    node.CharSelected += (idx, name, lvl) => SwitchPhase(Phase.Game);
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
                }
                break;
            }
        }
    }

    private void OnLoginRequested(string host, int port, string user, string pass)
    {
        GD.Print($"[Main] 收到登录请求: {host}:{port}, 用户: {user}");
        if (_net != null)
        {
            _net.ConnectTo(host, port, autoReconnect: false);
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
}
