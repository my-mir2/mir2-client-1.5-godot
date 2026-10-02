// ============================================================================
// Mir2 客户端 - 行会外交与成员自治管理器 (1:1 盛大 1.5 官方实证)
// ============================================================================

using System;
using System.Collections.Generic;

namespace Mir2Client.Game
{
    public class GuildMember
    {
        public string Name { get; set; } = string.Empty;
        public string Rank { get; set; } = string.Empty;
        public int Level { get; set; }
        public bool Online { get; set; }
    }

    public class GuildManager
    {
        public string GuildName { get; private set; } = string.Empty;
        public string GuildRank { get; private set; } = string.Empty;
        public string GuildNotice { get; private set; } = string.Empty;
        public List<GuildMember> Members { get; } = new();
        public List<string> Allies { get; } = new();
        public List<string> Enemies { get; } = new();
        public bool IsInWar { get; private set; } = false;
        public string CastleOwnerGuild { get; private set; } = string.Empty;

        public event Action<string, string>? GuildInfoUpdated;
        public event Action<string>? GuildNoticeUpdated;
        public event Action<List<string>, List<string>>? GuildDiplomacyChanged;
        public event Action<string, string>? CastleStatusUpdated;

        public bool IsAlly(string targetGuild) => Allies.Contains(targetGuild);
        public bool IsEnemy(string targetGuild) => Enemies.Contains(targetGuild);

        public void SetGuildInfo(string gname, string rank)
        {
            GuildName = gname;
            GuildRank = rank;
            GuildInfoUpdated?.Invoke(gname, rank);
        }

        public void SetNotice(string notice)
        {
            GuildNotice = notice;
            GuildNoticeUpdated?.Invoke(notice);
        }

        public void AddAlly(string targetGuild)
        {
            if (!Allies.Contains(targetGuild))
            {
                Allies.Add(targetGuild);
                GuildDiplomacyChanged?.Invoke(Allies, Enemies);
            }
        }

        public void RemoveAlly(string targetGuild)
        {
            if (Allies.Remove(targetGuild))
            {
                GuildDiplomacyChanged?.Invoke(Allies, Enemies);
            }
        }

        public void DeclareWar(string targetGuild)
        {
            RemoveAlly(targetGuild);
            if (!Enemies.Contains(targetGuild))
            {
                Enemies.Add(targetGuild);
            }
            IsInWar = true;
            GuildDiplomacyChanged?.Invoke(Allies, Enemies);
        }

        public void CeaseWar(string targetGuild)
        {
            Enemies.Remove(targetGuild);
            if (Enemies.Count == 0) IsInWar = false;
            GuildDiplomacyChanged?.Invoke(Allies, Enemies);
        }

        public void SetCastleOwner(string castleName, string owner)
        {
            CastleOwnerGuild = owner;
            CastleStatusUpdated?.Invoke(castleName, owner);
        }
    }
}
