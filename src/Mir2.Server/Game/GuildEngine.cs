using System;
using System.Collections.Generic;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 M2Server 行会系统与沙巴克攻城底座引擎
/// 对应官方物理地址:
/// - 0x4D3000 / 0x4D3200 TGuild / TPlayObject.Guild
/// - 官方封包: CM_CREATEGUILD (1041) / CM_GUILDOPEN (1040) / CM_GUILD_MEMBER_LIST (1037)
///          / CM_GUILDADDMEMBER (1038) / CM_GUILD_KICK_MEMBER (1039) / CM_GUILD_NOTICE_UPDATE (1040)
///          -> SM_BUILDGUILDOK (762) / SM_BUILDGUILDFAIL (763) / SM_GUILDMEMBERLIST (671)
/// 
/// 核心物理真值规则:
/// 1. 创建行会: 必须持有道具 [沃玛号角] 并且扣除 1,000,000 金币;
/// 2. 掌门人权限: 掌门人独享招人、踢人、发布行会公告与宣战结盟权力;
/// 3. 行会名单下发: 严格对齐客户端 GuildPanelView 渲染格式.
/// </summary>
public sealed class GuildEngine
{
    public const int CreateGuildGoldCost = 1_000_000;
    public const string HornItemName = "沃玛号角";

    public sealed class Guild
    {
        public string Name { get; set; } = string.Empty;
        public string MasterName { get; set; } = string.Empty;
        public string Notice { get; set; } = "热血传奇官方行会";
        public List<string> Members { get; } = new();
    }

    private readonly Dictionary<string, Guild> _guilds = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, string> _playerToGuild = new(StringComparer.OrdinalIgnoreCase);
    private readonly object _lock = new();

    public enum GuildResult
    {
        Success = 0,
        AlreadyInGuild = 1,
        GuildNameExists = 2,
        MissingHorn = 3,
        InsufficientGold = 4,
        NotMaster = 5,
        TargetNotFound = 6,
        GuildNotFound = 7,
    }

    /// <summary>
    /// 沃玛号角创建行会 (CM_CREATEGUILD 1041)
    /// </summary>
    public (GuildResult result, Guild? guild) CreateGuild(ClientSession session, string guildName)
    {
        if (session.State == null || string.IsNullOrWhiteSpace(guildName))
        {
            return (GuildResult.TargetNotFound, null);
        }

        lock (_lock)
        {
            if (_playerToGuild.ContainsKey(session.CharacterName))
            {
                return (GuildResult.AlreadyInGuild, null);
            }

            if (_guilds.ContainsKey(guildName))
            {
                return (GuildResult.GuildNameExists, null);
            }

            // 检查金币 1,000,000
            if (session.State.Gold < CreateGuildGoldCost)
            {
                return (GuildResult.InsufficientGold, null);
            }

            // 检查沃玛号角
            var hornItem = session.State.BagItems.Find(i => i.Name.Contains(HornItemName));
            if (hornItem == null)
            {
                return (GuildResult.MissingHorn, null);
            }

            // 扣除号角与金币
            session.State.BagItems.Remove(hornItem);
            session.State.BagWeight = Math.Max(0, session.State.BagWeight - hornItem.Weight);
            session.State.Gold -= CreateGuildGoldCost;

            var guild = new Guild
            {
                Name = guildName.Trim(),
                MasterName = session.CharacterName
            };
            guild.Members.Add(session.CharacterName);

            _guilds[guild.Name] = guild;
            _playerToGuild[session.CharacterName] = guild.Name;

            return (GuildResult.Success, guild);
        }
    }

    /// <summary>
    /// 获取玩家所属行会
    /// </summary>
    public Guild? GetGuildByPlayer(string playerName)
    {
        lock (_lock)
        {
            if (_playerToGuild.TryGetValue(playerName, out var guildName) &&
                _guilds.TryGetValue(guildName, out var guild))
            {
                return guild;
            }
            return null;
        }
    }

    /// <summary>
    /// 获取玩家所属行会名称 (未入会返回 null)
    /// </summary>
    public string? GetPlayerGuild(string playerName) =>
        GetGuildByPlayer(playerName)?.Name;

    /// <summary>
    /// 招收新成员入会 (CM_GUILDADDMEMBER 1038)
    /// </summary>
    public GuildResult AddMember(ClientSession masterSession, string targetName)
    {
        lock (_lock)
        {
            var guild = GetGuildByPlayer(masterSession.CharacterName);
            if (guild == null || guild.MasterName != masterSession.CharacterName)
            {
                return GuildResult.NotMaster;
            }

            if (_playerToGuild.ContainsKey(targetName))
            {
                return GuildResult.AlreadyInGuild;
            }

            guild.Members.Add(targetName);
            _playerToGuild[targetName] = guild.Name;

            return GuildResult.Success;
        }
    }

    /// <summary>
    /// 开除行会成员 (CM_GUILD_KICK_MEMBER 1039)
    /// </summary>
    public GuildResult KickMember(ClientSession masterSession, string targetName)
    {
        lock (_lock)
        {
            var guild = GetGuildByPlayer(masterSession.CharacterName);
            if (guild == null || guild.MasterName != masterSession.CharacterName)
            {
                return GuildResult.NotMaster;
            }

            if (!guild.Members.Remove(targetName))
            {
                return GuildResult.TargetNotFound;
            }

            _playerToGuild.Remove(targetName);
            return GuildResult.Success;
        }
    }

    /// <summary>
    /// 更新行会公告 (CM_GUILD_NOTICE_UPDATE 1040)
    /// </summary>
    public GuildResult UpdateNotice(ClientSession masterSession, string newNotice)
    {
        lock (_lock)
        {
            var guild = GetGuildByPlayer(masterSession.CharacterName);
            if (guild == null || guild.MasterName != masterSession.CharacterName)
            {
                return GuildResult.NotMaster;
            }

            guild.Notice = newNotice;
            return GuildResult.Success;
        }
    }

    /// <summary>
    /// 下发官方 1.5 格式行会成员列表 (SM_GUILDMEMBERLIST 671)
    /// </summary>
    public void SendGuildMemberList(ClientSession session)
    {
        var guild = GetGuildByPlayer(session.CharacterName);
        if (guild == null)
        {
            session.EnqueuePacket(ProtocolConsts.SM_OPENGUILDDLGFAIL, 0, 0, 0, 0, "您尚未加入任何行会");
            return;
        }

        // 格式: 行会名/掌门人/公告/成员1/成员2/...
        var data = $"{guild.Name}/{guild.MasterName}/{guild.Notice}/{string.Join('/', guild.Members)}";
        session.EnqueuePacket(ProtocolConsts.SM_GUILDMEMBERLIST, guild.Members.Count, 0, 0, 0, data);
    }
}
