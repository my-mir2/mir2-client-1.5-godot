using System;
using System.Collections.Generic;

namespace Mir2Client.Game;

/// <summary>
/// 盛大官方 1.5 大地图核心地标与传送门门点定位引擎 (纯 C# 实现)
/// 严格 1:1 对齐官方原版客户端 MiniMap/BigMap 地标物理真值
/// </summary>
public static class MapLandmarkEngine
{
    public enum LandmarkType
    {
        Portal,     // 跨地图传送门点 (关卡/洞穴入口)
        Palace,     // 皇宫/城堡核心地标
        TownCenter, // 主城安全区/城镇中心
        SecretArea  // 隐藏洞穴/危险领地
    }

    public record Landmark(string Name, int X, int Y, LandmarkType Type, string Desc);

    private static readonly Dictionary<string, List<Landmark>> LandmarksByMap = new()
    {
        // 比奇省 (0)
        ["0"] = new()
        {
            new("比奇城", 330, 270, LandmarkType.TownCenter, "比奇省主城安全区"),
            new("比奇皇宫", 632, 633, LandmarkType.Palace, "比奇国王行宫"),
            new("废矿区入口", 662, 216, LandmarkType.Portal, "天然洞穴与僵尸尸王殿前置"),
            new("骷髅洞", 108, 160, LandmarkType.Portal, "古墓骷髅洞穴入口"),
            new("毒蛇山谷关卡", 400, 10, LandmarkType.Portal, "通往毒蛇山谷通道"),
            new("沃玛森林入口", 140, 20, LandmarkType.Portal, "通往沃玛森林林间要道")
        },
        // 盟重省 (3)
        ["3"] = new()
        {
            new("盟重土城", 330, 330, LandmarkType.TownCenter, "黄沙漫天的玛法商贸中心"),
            new("沙巴克皇宫", 640, 120, LandmarkType.Palace, "行会争霸最高荣耀城堡"),
            new("石墓(猪洞)", 300, 300, LandmarkType.Portal, "石质古墓一层入口"),
            new("死亡山谷(蜈蚣洞)", 140, 90, LandmarkType.Portal, "幽暗深邃的蜈蚣洞穴"),
            new("祖玛寺庙入口", 860, 180, LandmarkType.Portal, "古老邪恶的祖玛神庙"),
            new("毒蛇山谷入口", 30, 600, LandmarkType.Portal, "通往毒蛇山谷南部关卡")
        },
        // 毒蛇山谷 (2)
        ["2"] = new()
        {
            new("毒蛇小村", 500, 480, LandmarkType.TownCenter, "毒蛇山谷安全休整站"),
            new("盟重关卡", 500, 20, LandmarkType.Portal, "通往盟重土城边界"),
            new("比奇关卡", 280, 580, LandmarkType.Portal, "通往比奇省要冲")
        },
        // 沃玛森林 (1)
        ["1"] = new()
        {
            new("沃玛寺庙入口", 80, 80, LandmarkType.Portal, "沃玛教主盘踞古刹"),
            new("封魔谷入口", 10, 400, LandmarkType.Portal, "通往封魔矿区入口"),
            new("白日门入口", 350, 20, LandmarkType.Portal, "道家圣地白日门通路")
        },
        // 白日门 (11)
        ["11"] = new()
        {
            new("白日门城", 180, 300, LandmarkType.TownCenter, "天尊驻留之道家主城"),
            new("丛林迷宫入口", 350, 240, LandmarkType.Portal, "通往赤月峡谷之险径")
        },
        // 封魔谷 (4)
        ["4"] = new()
        {
            new("封魔城", 240, 200, LandmarkType.TownCenter, "封魔要塞安全区"),
            new("封魔矿区", 140, 80, LandmarkType.Portal, "封魔殿深层入口")
        }
    };

    public static IReadOnlyList<Landmark> GetLandmarks(string mapIdOrName)
    {
        // 兼容地图编号或名字查询
        if (LandmarksByMap.TryGetValue(mapIdOrName, out var list))
            return list;

        if (mapIdOrName.Contains("比奇")) return LandmarksByMap["0"];
        if (mapIdOrName.Contains("盟重") || mapIdOrName.Contains("土城")) return LandmarksByMap["3"];
        if (mapIdOrName.Contains("毒蛇")) return LandmarksByMap["2"];
        if (mapIdOrName.Contains("沃玛")) return LandmarksByMap["1"];
        if (mapIdOrName.Contains("白日门")) return LandmarksByMap["11"];
        if (mapIdOrName.Contains("封魔")) return LandmarksByMap["4"];

        return Array.Empty<Landmark>();
    }
}
