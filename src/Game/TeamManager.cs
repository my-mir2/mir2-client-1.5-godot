// ============================================================================
// Mir2 客户端 - 组队协同与雷达管理器 (1:1 盛大 1.5 官方实证)
// ============================================================================

using System;
using System.Collections.Generic;

namespace Mir2Client.Game
{
    public class TeamMember
    {
        public string Name { get; set; } = string.Empty;
        public int HpPct { get; set; } = 100;
        public int TileX { get; set; } = -1;
        public int TileY { get; set; } = -1;
        public int Uid { get; set; } = 0;
    }

    public class TeamManager
    {
        public List<TeamMember> Members { get; } = new();

        public event Action<List<TeamMember>>? TeamUpdated;
        public event Action<string>? MemberAdded;
        public event Action<string>? MemberRemoved;

        public void UpdateTeamStatus(string rawStr)
        {
            Members.Clear();
            if (!string.IsNullOrEmpty(rawStr))
            {
                var names = rawStr.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                foreach (var n in names)
                {
                    Members.Add(new TeamMember { Name = n, HpPct = 100 });
                }
            }
            TeamUpdated?.Invoke(Members);
        }

        public void AddMember(string name)
        {
            string trimmed = name.Trim();
            if (string.IsNullOrEmpty(trimmed)) return;
            if (Members.Exists(m => m.Name == trimmed)) return;

            Members.Add(new TeamMember { Name = trimmed, HpPct = 100 });
            MemberAdded?.Invoke(trimmed);
            TeamUpdated?.Invoke(Members);
        }

        public void RemoveMember(string name = "")
        {
            string trimmed = name.Trim();
            if (!string.IsNullOrEmpty(trimmed))
            {
                Members.RemoveAll(m => m.Name == trimmed);
                MemberRemoved?.Invoke(trimmed);
            }
            else
            {
                Members.Clear();
            }
            TeamUpdated?.Invoke(Members);
        }
    }
}
