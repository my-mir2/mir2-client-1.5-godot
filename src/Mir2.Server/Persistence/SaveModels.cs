using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Mir2.Shared.Models;

namespace Mir2.Server.Persistence;

/// <summary>
/// 装备物品持久化数据传输对象 (零反射 Native AOT 兼容)
/// </summary>
public sealed class EquipItemSaveData
{
    public int Uid { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Shape { get; set; }
    public int Weight { get; set; }
    public int Dura { get; set; }
    public int MaxDura { get; set; }
    public int Ac { get; set; }
    public int MaxAc { get; set; }
    public int Mac { get; set; }
    public int MaxMac { get; set; }
    public int Dc { get; set; }
    public int MaxDc { get; set; }
    public int Mc { get; set; }
    public int MaxMc { get; set; }
    public int Sc { get; set; }
    public int MaxSc { get; set; }
    public int BonusDc { get; set; }
    public int BonusMc { get; set; }
    public int BonusSc { get; set; }
    public int BonusAc { get; set; }
    public int BonusMac { get; set; }
    public int Luck { get; set; }
    public int Curse { get; set; }
    public int UpgradeState { get; set; }
    public int UpgradeCount { get; set; }
    public int Accuracy { get; set; }
    public int Agility { get; set; }
    public int AttackSpeed { get; set; }
    public int MagicEvade { get; set; }
    public int AntiPoison { get; set; }
}

/// <summary>
/// 玩家角色全状态持久化存档模型
/// 严格覆盖：核心属性、自由加点、9大装备槽、40格背包、40格仓库及安全密码
/// </summary>
public sealed class PlayerSaveData
{
    public string Name { get; set; } = string.Empty;
    public int Job { get; set; }
    public int Sex { get; set; }
    public int Level { get; set; } = 1;

    public int Hp { get; set; } = 100;
    public int MaxHp { get; set; } = 100;
    public int Mp { get; set; } = 100;
    public int MaxMp { get; set; } = 100;

    public int MapId { get; set; } = 0; // 比奇省
    public int MapX { get; set; } = 288;
    public int MapY { get; set; } = 615;
    public byte Direction { get; set; }

    public int Gold { get; set; }
    public int PkPoints { get; set; }
    public byte AttackMode { get; set; }

    // 自由加点
    public int BonusDc { get; set; }
    public int BonusMc { get; set; }
    public int BonusSc { get; set; }
    public int BonusAc { get; set; }
    public int BonusMac { get; set; }
    public int BonusHp { get; set; }
    public int BonusMp { get; set; }

    // 9 大装备槽位 (null 表示空槽)
    public List<EquipItemSaveData?> Equipment { get; set; } = new(9);

    // 背包 40 格
    public List<EquipItemSaveData> BagItems { get; set; } = new();

    // 独立仓库 40 格与金币
    public List<EquipItemSaveData> StorageItems { get; set; } = new();
    public int StorageGold { get; set; }
    public string StoragePassword { get; set; } = string.Empty;

    // 行会
    public byte GuildRank { get; set; }
    public string GuildName { get; set; } = string.Empty;

    public long LastSavedTimestamp { get; set; }
}

/// <summary>
/// 行会成员持久化模型
/// </summary>
public sealed class GuildMemberSaveData
{
    public string Name { get; set; } = string.Empty;
    public byte Rank { get; set; } // 1=正掌门, 2=副掌门, 0=会员
    public int Level { get; set; }
}

/// <summary>
/// 行会全景持久化模型
/// </summary>
public sealed class GuildSaveData
{
    public string GuildName { get; set; } = string.Empty;
    public string LeaderName { get; set; } = string.Empty;
    public string Notice { get; set; } = string.Empty;
    public List<GuildMemberSaveData> Members { get; set; } = new();
}

/// <summary>
/// 全服世界状态持久化模型 (沙巴克城堡与行会大盘)
/// </summary>
public sealed class WorldSaveData
{
    public string SabukOwnerGuild { get; set; } = string.Empty;
    public int SabukGold { get; set; }
    public int SabukGateHp { get; set; } = 10000;
    public int SabukWallHp { get; set; } = 20000;
    public List<GuildSaveData> Guilds { get; set; } = new();
    public long LastSavedTimestamp { get; set; }
}

/// <summary>
/// Web 监控后台全量系统指标数据包 (Native AOT 强类型)
/// </summary>
public sealed class ServerStatsDto
{
    public int OnlinePlayers { get; set; }
    public int TotalMonsters { get; set; }
    public int GroundItems { get; set; }
    public double LastTickMs { get; set; }
    public double MaxTickMs { get; set; }
    public long MemoryMb { get; set; }
    public long CurrentTick { get; set; }
    public long UpTimeSeconds { get; set; }
    public bool IsNativeAot { get; set; }
    public List<string> PlayerList { get; set; } = new();
}

/// <summary>
/// Web 管理员操作响应数据包
/// </summary>
public sealed class AdminActionResponseDto
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}

/// <summary>
/// Web 全服公告请求体
/// </summary>
public sealed class AdminBroadcastRequestDto
{
    public string Message { get; set; } = string.Empty;
}

/// <summary>
/// Native AOT 编译期源生成 JSON 序列化上下文 (零反射、零裁剪开销)
/// </summary>
[JsonSourceGenerationOptions(WriteIndented = true, PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(PlayerSaveData))]
[JsonSerializable(typeof(EquipItemSaveData))]
[JsonSerializable(typeof(WorldSaveData))]
[JsonSerializable(typeof(GuildSaveData))]
[JsonSerializable(typeof(GuildMemberSaveData))]
[JsonSerializable(typeof(ServerStatsDto))]
[JsonSerializable(typeof(AdminActionResponseDto))]
[JsonSerializable(typeof(AdminBroadcastRequestDto))]
public partial class Mir2SaveJsonContext : JsonSerializerContext
{
}
