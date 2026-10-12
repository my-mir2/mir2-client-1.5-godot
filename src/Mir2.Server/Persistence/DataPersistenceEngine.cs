using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Mir2.Server.Game;
using Mir2.Shared.Models;

namespace Mir2.Server.Persistence;

/// <summary>
/// 盛大官方 1.5 服务端数据持久化引擎 (零依赖、Native AOT、高可靠原子写)
/// 彻底解决“重启即删档”坏账，提供角色属性、装备、背包、仓库与世界行会沙巴克原子化存储
/// </summary>
public sealed class DataPersistenceEngine
{
    private readonly string _baseDir;
    private readonly string _playerDir;
    private readonly string _worldDir;
    private readonly object _ioLock = new();

    public DataPersistenceEngine(string baseDir = "save")
    {
        _baseDir = Path.GetFullPath(baseDir);
        _playerDir = Path.Combine(_baseDir, "players");
        _worldDir = Path.Combine(_baseDir, "world");

        Directory.CreateDirectory(_playerDir);
        Directory.CreateDirectory(_worldDir);
    }

    /// <summary>
    /// 原子化写入文本文件 (先写临时文件再原子覆盖，防断电或中断损坏)
    /// </summary>
    private static void AtomicWriteAllText(string filePath, string content)
    {
        string tmpFile = filePath + ".tmp";
        string? dir = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(dir))
        {
            Directory.CreateDirectory(dir);
        }

        File.WriteAllText(tmpFile, content);
        File.Move(tmpFile, filePath, overwrite: true);
    }

    /// <summary>
    /// 保存单个玩家角色全景状态至本地磁盘
    /// </summary>
    public bool SavePlayer(string playerName, PlayerState state, int mapId, int mapX, int mapY)
    {
        if (string.IsNullOrWhiteSpace(playerName) || state == null) return false;

        try
        {
            var dto = new PlayerSaveData
            {
                Name = playerName,
                Job = state.Job,
                Sex = state.Sex,
                Level = state.Level,
                Hp = state.Hp,
                MaxHp = state.MaxHp,
                Mp = state.Mp,
                MaxMp = state.MaxMp,
                MapId = mapId,
                MapX = mapX,
                MapY = mapY,
                Direction = state.Direction,
                Gold = state.Gold,
                PkPoints = state.PkPoints,
                AttackMode = state.AttackMode,
                BonusDc = state.BonusDc,
                BonusMc = state.BonusMc,
                BonusSc = state.BonusSc,
                BonusAc = state.BonusAc,
                BonusMac = state.BonusMac,
                BonusHp = state.BonusHp,
                BonusMp = state.BonusMp,
                StorageGold = state.StorageGold,
                StoragePassword = state.StoragePassword,
                GuildRank = state.GuildRank,
                LastSavedTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            };

            // 转换 9 大装备槽位
            for (int i = 0; i < state.Equipment.Length; i++)
            {
                var equip = state.Equipment[i];
                dto.Equipment.Add(equip != null ? ToEquipDto(equip) : null);
            }

            // 转换背包
            lock (state.BagItems)
            {
                foreach (var item in state.BagItems)
                {
                    dto.BagItems.Add(ToEquipDto(item));
                }
            }

            // 转换仓库
            lock (state.StorageItems)
            {
                foreach (var item in state.StorageItems)
                {
                    dto.StorageItems.Add(ToEquipDto(item));
                }
            }

            string json = JsonSerializer.Serialize(dto, Mir2SaveJsonContext.Default.PlayerSaveData);
            string filePath = Path.Combine(_playerDir, $"{SanitizeFileName(playerName)}.json");

            lock (_ioLock)
            {
                AtomicWriteAllText(filePath, json);
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[DataPersistence] 保存玩家 {playerName} 失败: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// 从本地磁盘加载玩家角色数据 (若不存在返回 null)
    /// </summary>
    public PlayerState? LoadPlayer(string playerName, out int mapId, out int mapX, out int mapY)
    {
        mapId = 0;
        mapX = 330;
        mapY = 270;

        if (string.IsNullOrWhiteSpace(playerName)) return null;

        string filePath = Path.Combine(_playerDir, $"{SanitizeFileName(playerName)}.json");
        if (!File.Exists(filePath)) return null;

        try
        {
            string json;
            lock (_ioLock)
            {
                json = File.ReadAllText(filePath);
            }

            var dto = JsonSerializer.Deserialize(json, Mir2SaveJsonContext.Default.PlayerSaveData);
            if (dto == null) return null;

            var state = new PlayerState
            {
                Name = dto.Name,
                Job = dto.Job,
                Sex = dto.Sex,
                Level = Math.Max(1, dto.Level),
                Hp = dto.Hp,
                MaxHp = dto.MaxHp,
                Mp = dto.Mp,
                MaxMp = dto.MaxMp,
                Direction = dto.Direction,
                Gold = dto.Gold,
                PkPoints = dto.PkPoints,
                AttackMode = dto.AttackMode,
                BonusDc = dto.BonusDc,
                BonusMc = dto.BonusMc,
                BonusSc = dto.BonusSc,
                BonusAc = dto.BonusAc,
                BonusMac = dto.BonusMac,
                BonusHp = dto.BonusHp,
                BonusMp = dto.BonusMp,
                StorageGold = dto.StorageGold,
                StoragePassword = dto.StoragePassword ?? string.Empty,
                GuildRank = dto.GuildRank
            };

            mapId = dto.MapId;
            mapX = dto.MapX;
            mapY = dto.MapY;

            // 恢复装备槽位
            if (dto.Equipment != null)
            {
                int len = Math.Min(state.Equipment.Length, dto.Equipment.Count);
                for (int i = 0; i < len; i++)
                {
                    var itemDto = dto.Equipment[i];
                    state.Equipment[i] = itemDto != null ? FromEquipDto(itemDto) : null;
                }
            }

            // 恢复背包
            state.BagItems.Clear();
            if (dto.BagItems != null)
            {
                foreach (var itemDto in dto.BagItems)
                {
                    state.BagItems.Add(FromEquipDto(itemDto));
                }
            }

            // 恢复仓库
            state.StorageItems.Clear();
            if (dto.StorageItems != null)
            {
                foreach (var itemDto in dto.StorageItems)
                {
                    state.StorageItems.Add(FromEquipDto(itemDto));
                }
            }

            return state;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[DataPersistence] 读取玩家 {playerName} 失败: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// 保存世界状态 (沙巴克金库与行会大盘)
    /// </summary>
    public bool SaveWorld(SabukTreasuryEngine treasury, GuildEngine guildEngine)
    {
        try
        {
            var worldDto = new WorldSaveData
            {
                SabukGold = (int)Math.Min(int.MaxValue, treasury.GoldTreasury),
                SabukOwnerGuild = treasury.OwnerGuildName,
                LastSavedTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            };

            // 获取行会
            foreach (var g in guildEngine.GetAllGuilds())
            {
                var gDto = new GuildSaveData
                {
                    GuildName = g.Name,
                    LeaderName = g.MasterName,
                    Notice = g.Notice
                };
                foreach (var m in g.Members)
                {
                    gDto.Members.Add(new GuildMemberSaveData
                    {
                        Name = m,
                        Rank = (byte)(m == g.MasterName ? 1 : 0),
                        Level = 1
                    });
                }
                worldDto.Guilds.Add(gDto);
            }

            string json = JsonSerializer.Serialize(worldDto, Mir2SaveJsonContext.Default.WorldSaveData);
            string filePath = Path.Combine(_worldDir, "world.json");

            lock (_ioLock)
            {
                AtomicWriteAllText(filePath, json);
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[DataPersistence] 保存世界状态失败: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// 加载世界状态
    /// </summary>
    public bool LoadWorld(SabukTreasuryEngine treasury, GuildEngine guildEngine)
    {
        string filePath = Path.Combine(_worldDir, "world.json");
        if (!File.Exists(filePath)) return false;

        try
        {
            string json;
            lock (_ioLock)
            {
                json = File.ReadAllText(filePath);
            }

            var dto = JsonSerializer.Deserialize(json, Mir2SaveJsonContext.Default.WorldSaveData);
            if (dto == null) return false;

            treasury.SetTreasury(dto.SabukGold, dto.SabukOwnerGuild, "沙巴克城主");

            foreach (var gDto in dto.Guilds)
            {
                var memberNames = new List<string>();
                foreach (var m in gDto.Members)
                {
                    memberNames.Add(m.Name);
                }
                guildEngine.LoadGuild(gDto.GuildName, gDto.LeaderName, gDto.Notice, memberNames);
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[DataPersistence] 读取世界状态失败: {ex.Message}");
            return false;
        }
    }

    private static EquipItemSaveData ToEquipDto(EquipItem item)
    {
        return new EquipItemSaveData
        {
            Uid = item.Uid,
            Name = item.Name,
            Shape = item.Shape,
            Weight = item.Weight,
            Dura = item.Dura,
            MaxDura = item.MaxDura,
            Ac = item.Ac,
            MaxAc = item.MaxAc,
            Mac = item.Mac,
            MaxMac = item.MaxMac,
            Dc = item.Dc,
            MaxDc = item.MaxDc,
            Mc = item.Mc,
            MaxMc = item.MaxMc,
            Sc = item.Sc,
            MaxSc = item.MaxSc,
            BonusDc = item.BonusDc,
            BonusMc = item.BonusMc,
            BonusSc = item.BonusSc,
            BonusAc = item.BonusAc,
            BonusMac = item.BonusMac,
            Luck = item.Luck,
            Curse = item.Curse,
            UpgradeState = item.UpgradeState,
            UpgradeCount = item.UpgradeCount,
            Accuracy = item.Accuracy,
            Agility = item.Agility,
            AttackSpeed = item.AttackSpeed,
            MagicEvade = item.MagicEvade,
            AntiPoison = item.AntiPoison
        };
    }

    private static EquipItem FromEquipDto(EquipItemSaveData dto)
    {
        return new EquipItem
        {
            Uid = dto.Uid,
            Name = dto.Name ?? string.Empty,
            Shape = dto.Shape,
            Weight = dto.Weight,
            Dura = dto.Dura,
            MaxDura = dto.MaxDura,
            Ac = dto.Ac,
            MaxAc = dto.MaxAc,
            Mac = dto.Mac,
            MaxMac = dto.MaxMac,
            Dc = dto.Dc,
            MaxDc = dto.MaxDc,
            Mc = dto.Mc,
            MaxMc = dto.MaxMc,
            Sc = dto.Sc,
            MaxSc = dto.MaxSc,
            BonusDc = dto.BonusDc,
            BonusMc = dto.BonusMc,
            BonusSc = dto.BonusSc,
            BonusAc = dto.BonusAc,
            BonusMac = dto.BonusMac,
            Luck = dto.Luck,
            Curse = dto.Curse,
            UpgradeState = dto.UpgradeState,
            UpgradeCount = dto.UpgradeCount,
            Accuracy = dto.Accuracy,
            Agility = dto.Agility,
            AttackSpeed = dto.AttackSpeed,
            MagicEvade = dto.MagicEvade,
            AntiPoison = dto.AntiPoison
        };
    }

    private static string SanitizeFileName(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(c, '_');
        }
        return name;
    }
}
