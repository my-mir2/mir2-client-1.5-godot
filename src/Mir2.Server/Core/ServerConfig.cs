using System;
using System.IO;

namespace Mir2.Server.Core;

/// <summary>
/// 生产环境统一配置加载器 (优先级: 配置文件 config/server.ini > 环境变量 > 默认硬编码)
/// </summary>
public sealed class ServerConfig
{
    public int GatewayPort { get; set; } = 7000;
    public int MaxConnections { get; set; } = 3000;
    public int MaxPacketsPerSecond { get; set; } = 80;
    public int IdleTimeoutMs { get; set; } = 180000;
    public int MaxBufferSize { get; set; } = 65536;

    public string DatabaseConnectionString { get; set; } = "Host=127.0.0.1;Port=5432;Database=mir2_world;Username=postgres;Password=postgres";
    public bool StrictPgMode { get; set; } = true;

    public int AdminPort { get; set; } = 8080;
    public string AdminSecret { get; set; } = "mir2_admin_secret_2026";

    public string LogDirectory { get; set; } = "logs";
    public string LogDir => LogDirectory;
    public int MaxLogFileSizeMb { get; set; } = 50;
    public int LogMaxSizeMb => MaxLogFileSizeMb;
    public int LogRetentionDays { get; set; } = 30;
    public int LogArchiveRetentionDays { get; set; } = 30;
    public int LogDeepPurgeDays { get; set; } = 180;
    public bool EnableDatabaseLogging { get; set; } = true;

    public static ServerConfig Load(string configPath = "config/server.ini")
    {
        var cfg = new ServerConfig();

        // 1. 读取配置文件
        if (File.Exists(configPath))
        {
            string currentSection = "";
            foreach (var line in File.ReadAllLines(configPath))
            {
                string trimmed = line.Trim();
                if (string.IsNullOrEmpty(trimmed) || trimmed.StartsWith(';') || trimmed.StartsWith('#'))
                    continue;

                if (trimmed.StartsWith('[') && trimmed.EndsWith(']'))
                {
                    currentSection = trimmed[1..^1].Trim();
                    continue;
                }

                int eqIdx = trimmed.IndexOf('=');
                if (eqIdx <= 0) continue;

                string key = trimmed[..eqIdx].Trim();
                string val = trimmed[(eqIdx + 1)..].Trim();

                switch (key)
                {
                    case "GatewayPort" when int.TryParse(val, out int p): cfg.GatewayPort = p; break;
                    case "MaxConnections" when int.TryParse(val, out int mc): cfg.MaxConnections = mc; break;
                    case "MaxPacketsPerSecond" when int.TryParse(val, out int mpps): cfg.MaxPacketsPerSecond = mpps; break;
                    case "IdleTimeoutMs" when int.TryParse(val, out int itm): cfg.IdleTimeoutMs = itm; break;
                    case "MaxBufferSize" when int.TryParse(val, out int mbs): cfg.MaxBufferSize = mbs; break;
                    case "ConnectionString": cfg.DatabaseConnectionString = val; break;
                    case "StrictPgMode": cfg.StrictPgMode = val == "1" || val.Equals("true", StringComparison.OrdinalIgnoreCase); break;
                    case "AdminPort" when int.TryParse(val, out int ap): cfg.AdminPort = ap; break;
                    case "AdminSecret": cfg.AdminSecret = val; break;
                    case "LogDirectory": cfg.LogDirectory = val; break;
                    case "MaxLogFileSizeMb" when int.TryParse(val, out int mfs): cfg.MaxLogFileSizeMb = mfs; break;
                    case "LogRetentionDays" when int.TryParse(val, out int lrd): cfg.LogRetentionDays = lrd; break;
                    case "LogArchiveRetentionDays" when int.TryParse(val, out int lard): cfg.LogArchiveRetentionDays = lard; break;
                    case "LogDeepPurgeDays" when int.TryParse(val, out int ldpd): cfg.LogDeepPurgeDays = ldpd; break;
                    case "EnableDatabaseLogging": cfg.EnableDatabaseLogging = val == "1" || val.Equals("true", StringComparison.OrdinalIgnoreCase); break;
                }
            }
        }

        // 2. 环境变量覆盖
        string? envPg = Environment.GetEnvironmentVariable("MIR2_PG_CONN");
        if (!string.IsNullOrEmpty(envPg)) cfg.DatabaseConnectionString = envPg;

        string? envPort = Environment.GetEnvironmentVariable("MIR2_PORT");
        if (int.TryParse(envPort, out int ep)) cfg.GatewayPort = ep;

        return cfg;
    }
}
