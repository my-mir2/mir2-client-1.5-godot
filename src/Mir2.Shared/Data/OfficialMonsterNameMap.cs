using System;
using System.Collections.Generic;

namespace Mir2.Shared.Data;

/// <summary>
/// 中文怪物名 -> 官方 Monster.DB 属性 的解析器。
/// 中文名完全来自数据库 official_monster.name_cn (汉化数据由 tools/convert_official_db_to_sql.py 与 data/official_14/cn_names.csv 维护)，
/// 代码中不再硬编码任何译名。同一中文名对应多个数字变体时 (Oma / Oma0 / Oma10) 取基础名条目。
/// </summary>
public static class OfficialMonsterNameMap
{
    private static readonly object Gate = new();
    private static IReadOnlyDictionary<string, OfficialMonsterStats>? _builtFrom;
    private static Dictionary<string, OfficialMonsterStats> _byChinese = new(StringComparer.Ordinal);

    private static Dictionary<string, OfficialMonsterStats> GetChineseIndex(IReadOnlyDictionary<string, OfficialMonsterStats> table)
    {
        lock (Gate)
        {
            if (ReferenceEquals(_builtFrom, table)) return _byChinese;
            var idx = new Dictionary<string, OfficialMonsterStats>(StringComparer.Ordinal);
            foreach (var s in table.Values)
            {
                if (string.IsNullOrEmpty(s.NameCn)) continue;
                // 优先基础名 (不带数字后缀)；已有条目且当前不是基础名则不覆盖
                bool isBase = s.Name.Length > 0 && !char.IsDigit(s.Name[^1]);
                if (!idx.ContainsKey(s.NameCn) || isBase) idx[s.NameCn] = s;
            }
            _byChinese = idx;
            _builtFrom = table;
            return idx;
        }
    }

    /// <summary>按名字取官方属性：先按中文名 (name_cn)，再按官方英文名直查；查不到返回 null，由调用方回退。</summary>
    public static OfficialMonsterStats? Resolve(string monsterName)
    {
        if (string.IsNullOrEmpty(monsterName)) return null;
        var table = OfficialMonsterStatsTable.Current;
        if (GetChineseIndex(table).TryGetValue(monsterName, out var byCn)) return byCn;
        return table.TryGetValue(monsterName, out var byEn) ? byEn : null;
    }
}
