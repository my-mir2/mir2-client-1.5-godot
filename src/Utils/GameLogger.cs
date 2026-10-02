using System;
using System.IO;
using Godot;

namespace Mir2Client.Utils;

/// <summary>
/// 盛大官方 1.5 客户端全局高精度结构化日志落盘系统 (纯 C# 实现)
/// </summary>
public static class GameLogger
{
    private static StreamWriter? _writer = null;
    private static bool _isInitialized = false;
    private static readonly object _lock = new();

    private static void EnsureInit()
    {
        if (_isInitialized) return;
        lock (_lock)
        {
            if (_isInitialized) return;
            try
            {
                string userDir = ProjectSettings.GlobalizePath("user://");
                Directory.CreateDirectory(userDir);
                string logFile = Path.Combine(userDir, "game_events.log");
                var fs = new FileStream(logFile, FileMode.Append, System.IO.FileAccess.Write, FileShare.ReadWrite);
                _writer = new StreamWriter(fs) { AutoFlush = true };
                _writer.WriteLine("\n============================================================");
                _writer.WriteLine($"[SESSION_START] 传奇 1.5 全局 C# 结构化审计日志初始化: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                _writer.WriteLine("============================================================");
            }
            catch (Exception ex)
            {
                GD.PrintErr($"[GameLogger] Failed to init log: {ex.Message}");
            }
            finally
            {
                _isInitialized = true;
            }
        }
    }

    private static void Log(string category, string message)
    {
        EnsureInit();
        string timeStr = DateTime.Now.ToString("HH:mm:ss.fff");
        string line = $"[{timeStr}] [{category}] {message}";
        GD.Print(line);
        lock (_lock)
        {
            _writer?.WriteLine(line);
        }
    }

    public static void LogNetRecv(string smName, int smCode, int recog, int param, int tag, int extraLen, string extraText = "")
    {
        string msg = $"SM={smName}({smCode}) | recog={recog}, param={param}, tag={tag} | extra_len={extraLen}";
        if (!string.IsNullOrEmpty(extraText))
        {
            string clean = extraText.Replace("\n", "\\n").Replace("\t", "\\t");
            if (clean.Length > 120) clean = clean[..120] + "...(truncated)";
            msg += $" | extra='{clean}'";
        }
        Log("NET:RECV", msg);
    }

    public static void LogNetSend(string cmName, int cmCode, int recog, int param, int tag, int extraLen, string extraText = "")
    {
        string msg = $"CM={cmName}({cmCode}) | recog={recog}, param={param}, tag={tag} | extra_len={extraLen}";
        if (!string.IsNullOrEmpty(extraText))
        {
            string clean = extraText.Replace("\n", "\\n").Replace("\t", "\\t");
            msg += $" | extra='{clean}'";
        }
        Log("NET:SEND", msg);
    }

    public static void LogNetWarn(string msg) => Log("NET:WARN", msg);
    public static void LogNetError(string msg) => Log("NET:ERROR", msg);
    public static void LogSystemEvent(string msg) => Log("SYS:EVENT", msg);
}
