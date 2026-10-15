using System;
using System.Threading;
using Godot;
using Mir2Client.Net;

namespace Mir2Client.Utils;

/// <summary>
/// 盛大传奇 1.5 客户端进程生命周期与安全退出保障中心
/// 解决 Godot 4 + Mono/C# 在 Windows 平台下因异步 Socket、后台网络线程或运行时析构挂起导致的进程残留僵尸问题
/// </summary>
public static class AppLifecycle
{
    private static int _isQuitting = 0;

    /// <summary>
    /// 安全终止整个客户端进程：断开网络、清理资源、通知 SceneTree 并以独立看门狗 Watchdog 强制退出，确保 100% 不留后台僵尸进程
    /// </summary>
    public static void SafeQuit(int exitCode = 0)
    {
        if (Interlocked.Exchange(ref _isQuitting, 1) != 0)
        {
            return; // 已经在退出流程中，避免重入
        }

        GD.Print($"[AppLifecycle] 收到退出请求 (exitCode={exitCode})，正在优雅关闭客户端网络与进程...");

        try
        {
            if (Engine.GetMainLoop() is SceneTree tree)
            {
                var net = tree.Root.GetNodeOrNull<NetManager>("/root/NetManager");
                if (net != null)
                {
                    try
                    {
                        net.DisconnectFromServer();
                    }
                    catch (Exception ex)
                    {
                        GD.PrintErr($"[AppLifecycle] 断开网络连接异常: {ex.Message}");
                    }
                }

                tree.Quit(exitCode);
            }
        }
        catch (Exception ex)
        {
            GD.PrintErr($"[AppLifecycle] SceneTree.Quit 异常: {ex.Message}");
        }

        // 终极进程保险丝：启动独立后台线程，在 200ms 后若主进程仍未结束则直接强杀，杜绝后台残留！
        var watchdog = new Thread(() =>
        {
            Thread.Sleep(200);
            GD.Print($"[AppLifecycle] 进程退出看门狗超时触发，强制终止 System.Environment.Exit({exitCode})");
            System.Environment.Exit(exitCode);
        })
        {
            IsBackground = true,
            Name = "Mir2Client_ExitWatchdog"
        };
        watchdog.Start();
    }
}
