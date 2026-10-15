using System;
using System.Collections.Generic;

namespace Mir2.Shared.Data;

public static partial class OfficialMonsterStatsTable
{
    private static IReadOnlyDictionary<string, OfficialMonsterStats>? _runtime;

    /// <summary>当前生效的数据来源：优先使用服务端启动时从数据库载入的内存表，未载入时回退到内置生成表。</summary>
    public static IReadOnlyDictionary<string, OfficialMonsterStats> Current => _runtime ?? ByName;

    /// <summary>数据来源描述，用于启动日志与诊断。</summary>
    public static string SourceDescription { get; private set; } = "内置生成表 (Monster.DB 导出，未连接数据库)";

    /// <summary>用数据库读出的记录替换运行期内存表 (整体替换，线程安全可见)。</summary>
    public static void ReplaceRuntime(IEnumerable<OfficialMonsterStats> rows, string source)
    {
        var dict = new Dictionary<string, OfficialMonsterStats>(StringComparer.OrdinalIgnoreCase);
        foreach (var r in rows) dict[r.Name] = r;
        _runtime = dict;
        SourceDescription = source;
    }

    /// <summary>仅用于测试：恢复为内置生成表。</summary>
    public static void ResetToEmbedded()
    {
        _runtime = null;
        SourceDescription = "内置生成表 (Monster.DB 导出，未连接数据库)";
    }
}
