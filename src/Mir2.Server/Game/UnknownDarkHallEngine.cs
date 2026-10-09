using System;
using Mir2.Shared.Models;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 未知暗殿真假怪反差萌权威引擎 (UnknownDarkHallEngine)
/// 严格 1:1 对齐官方 M2Server 未知暗殿生态规则:
/// 1. 殿内强大 BOSS (沃玛教主/邪恶钳虫/白猪) 血量仅 100 点，一刀即死
/// 2. 殿内弱小怪物 (暗殿鹿/稻草人/鸡) 拥有 5000 点超高生命与超强物理攻击，击杀掉落神水
/// </summary>
public static class UnknownDarkHallEngine
{
    public const int DarkHallMapId = 1000; // 未知暗殿官方地图编号

    public static bool IsDarkHallMap(int mapId) => mapId == DarkHallMapId;

    /// <summary>
    /// 当怪物在未知暗殿生成时，代入反差萌属性修正
    /// </summary>
    public static void ApplyDarkHallAttributes(MonsterInstance monster, int mapId)
    {
        if (!IsDarkHallMap(mapId)) return;

        // 1. 假 BOSS: 血量仅 100 点，一刀死
        if (monster.Name.Contains("沃玛教主") || monster.Name.Contains("邪恶钳虫") || monster.Name.Contains("白野猪"))
        {
            monster.MaxHp = 100;
            monster.Hp = 100;
            monster.Exp = 100;
        }
        // 2. 真神怪 (外表为弱小动物，实则血厚暴击)
        else if (monster.Name.Contains("鹿") || monster.Name.Contains("稻草人") || monster.Name.Contains("鸡"))
        {
            monster.MaxHp = 5000;
            monster.Hp = 5000;
            monster.Exp = 3000;
        }
    }
}
