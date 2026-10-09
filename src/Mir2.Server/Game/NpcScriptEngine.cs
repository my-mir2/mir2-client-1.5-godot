using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 NPC 脚本虚拟机 (NpcScriptEngine)
/// 严格 1:1 对齐官方 M2Server 脚本物理运行标准 (Market_Def 脚本体系):
/// 1. 标签流转: [@main], [@buy], [@sell], [@repair], 自定义子标签 [@target]
/// 2. 条件判据集 (#IF): CHECKGOLD, CHECKLEVELEX, CHECKITEM, CHECKJOB, CHECKPKPOINT, CHECKBAGSIZE, CHECKGENDER
/// 3. 执行指令集 (#ACT / #ELSEACT): TAKE, GIVE, TAKEGOLD, GIVEGOLD, MAPMOVE, GOTO, BREAK, CLOSE
/// 4. 对话富文本集 (#SAY / #ELSESAY): <$USERNAME>, <$GOLD>, <$LEVEL>, <$JOB> 变量插值与 <文本/@标签> 超链接
/// </summary>
public sealed class NpcScriptEngine
{
    public sealed class ScriptSection
    {
        public string TagName { get; init; } = string.Empty;
        public List<string> Conditions { get; } = new(); // #IF
        public List<string> Actions { get; } = new();    // #ACT
        public List<string> SayTexts { get; } = new();   // #SAY
        public List<string> ElseActions { get; } = new();// #ELSEACT
        public List<string> ElseSayTexts { get; } = new();// #ELSESAY
    }

    private readonly Dictionary<string, ScriptSection> _sections = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// 解析官方 NPC 脚本纯文本
    /// </summary>
    public void LoadScript(string scriptText)
    {
        _sections.Clear();
        if (string.IsNullOrWhiteSpace(scriptText)) return;

        var lines = scriptText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        ScriptSection? currentSection = null;
        string currentBlock = string.Empty; // "IF", "ACT", "SAY", "ELSEACT", "ELSESAY"

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (line.StartsWith(";") || string.IsNullOrEmpty(line)) continue; // 注释与空行

            if (line.StartsWith("[") && line.EndsWith("]"))
            {
                string tag = line[1..^1].Trim();
                currentSection = new ScriptSection { TagName = tag };
                _sections[tag] = currentSection;
                currentBlock = "SAY"; // 官方规则: 默认段落下文本即为对话内容
                continue;
            }

            if (currentSection == null) continue;

            if (line.Equals("#IF", StringComparison.OrdinalIgnoreCase))
            {
                currentBlock = "IF";
            }
            else if (line.Equals("#ACT", StringComparison.OrdinalIgnoreCase))
            {
                currentBlock = "ACT";
            }
            else if (line.Equals("#SAY", StringComparison.OrdinalIgnoreCase))
            {
                currentBlock = "SAY";
            }
            else if (line.Equals("#ELSEACT", StringComparison.OrdinalIgnoreCase))
            {
                currentBlock = "ELSEACT";
            }
            else if (line.Equals("#ELSESAY", StringComparison.OrdinalIgnoreCase))
            {
                currentBlock = "ELSESAY";
            }
            else
            {
                switch (currentBlock)
                {
                    case "IF": currentSection.Conditions.Add(line); break;
                    case "ACT": currentSection.Actions.Add(line); break;
                    case "SAY": currentSection.SayTexts.Add(line); break;
                    case "ELSEACT": currentSection.ElseActions.Add(line); break;
                    case "ELSESAY": currentSection.ElseSayTexts.Add(line); break;
                }
            }
        }
    }

    /// <summary>
    /// 执行指定标签的脚本逻辑 (默认为 @main)
    /// </summary>
    public (bool success, string outputSay, bool closed) Execute(ClientSession session, string tag = "@main")
    {
        if (session.State == null) return (false, "玩家状态异常。", true);
        if (!_sections.TryGetValue(tag, out var section))
        {
            return (false, $"未找到脚本标签: {tag}", true);
        }

        // 1. 评估 #IF 条件
        bool ifPassed = true;
        for (int i = 0; i < section.Conditions.Count; i++)
        {
            if (!EvaluateCondition(session, section.Conditions[i]))
            {
                ifPassed = false;
                break;
            }
        }

        var actions = ifPassed ? section.Actions : section.ElseActions;
        var sayLines = ifPassed ? section.SayTexts : section.ElseSayTexts;

        // 2. 执行指令动作
        bool closed = false;
        string? gotoTag = null;
        for (int i = 0; i < actions.Count; i++)
        {
            var act = actions[i];
            if (act.StartsWith("MAPMOVE", StringComparison.OrdinalIgnoreCase))
            {
                // MAPMOVE <mapId> <x> <y>
                var parts = act.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 4 &&
                    int.TryParse(parts[1], out int mapId) &&
                    int.TryParse(parts[2], out int x) &&
                    int.TryParse(parts[3], out int y))
                {
                    session.CurrentMapId = mapId;
                    session.X = x;
                    session.Y = y;
                    session.EnqueuePacket(ProtocolConsts.SM_NEWMAP_DELPHI, mapId, x, y, 0);
                }
            }
            else if (act.StartsWith("TAKEGOLD", StringComparison.OrdinalIgnoreCase))
            {
                var parts = act.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 2 && int.TryParse(parts[1], out int gold))
                {
                    session.State.Gold = Math.Max(0, session.State.Gold - gold);
                    session.State.RecalculateAuthoritativeWeights(session.State.Gold);
                }
            }
            else if (act.StartsWith("GIVEGOLD", StringComparison.OrdinalIgnoreCase))
            {
                var parts = act.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 2 && int.TryParse(parts[1], out int gold))
                {
                    session.State.Gold = Math.Min(PlayerState.MaxGoldLimit, session.State.Gold + gold);
                    session.State.RecalculateAuthoritativeWeights(session.State.Gold);
                }
            }
            else if (act.StartsWith("GIVE", StringComparison.OrdinalIgnoreCase))
            {
                // GIVE <itemName> <count>
                var parts = act.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 2)
                {
                    string itemName = parts[1];
                    int count = (parts.Length >= 3 && int.TryParse(parts[2], out int c)) ? c : 1;
                    for (int n = 0; n < count; n++)
                    {
                        if (session.State.BagItems.Count < 40)
                        {
                            session.State.BagItems.Add(new EquipItem { Uid = Random.Shared.Next(10000, 99999), Name = itemName, Weight = 2 });
                        }
                    }
                    session.State.RecalculateAuthoritativeWeights(session.State.Gold);
                }
            }
            else if (act.StartsWith("TAKE", StringComparison.OrdinalIgnoreCase))
            {
                // TAKE <itemName> <count>
                var parts = act.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 2)
                {
                    string itemName = parts[1];
                    int count = (parts.Length >= 3 && int.TryParse(parts[2], out int c)) ? c : 1;
                    for (int n = 0; n < count; n++)
                    {
                        int idx = session.State.BagItems.FindIndex(item => item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
                        if (idx >= 0) session.State.BagItems.RemoveAt(idx);
                    }
                    session.State.RecalculateAuthoritativeWeights(session.State.Gold);
                }
            }
            else if (act.StartsWith("GOTO", StringComparison.OrdinalIgnoreCase))
            {
                var parts = act.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 2) gotoTag = parts[1];
            }
            else if (act.Equals("CLOSE", StringComparison.OrdinalIgnoreCase))
            {
                closed = true;
            }
            else if (act.Equals("BREAK", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }
        }

        if (gotoTag != null)
        {
            return Execute(session, gotoTag);
        }

        // 3. 构建 #SAY 对话富文本 (变量替换)
        var sb = new StringBuilder();
        for (int i = 0; i < sayLines.Count; i++)
        {
            string line = sayLines[i];
            line = line.Replace("<$USERNAME>", session.CharacterName)
                       .Replace("<$GOLD>", session.State.Gold.ToString())
                       .Replace("<$LEVEL>", session.State.Level.ToString())
                       .Replace("<$JOB>", session.State.Job == 0 ? "战士" : (session.State.Job == 1 ? "法师" : "道士"));
            sb.AppendLine(line);
        }

        return (true, sb.ToString().TrimEnd(), closed);
    }

    private static bool EvaluateCondition(ClientSession session, string conditionLine)
    {
        if (session.State == null) return false;
        var parts = conditionLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0) return true;

        string cmd = parts[0].ToUpperInvariant();
        switch (cmd)
        {
            case "CHECKGOLD":
                if (parts.Length >= 2 && int.TryParse(parts[1], out int gold))
                    return session.State.Gold >= gold;
                return false;

            case "CHECKLEVELEX":
                // 格式: CHECKLEVELEX > 20 或 CHECKLEVELEX >= 20
                if (parts.Length >= 3 && int.TryParse(parts[2], out int lvl))
                {
                    string op = parts[1];
                    return op switch
                    {
                        ">" => session.State.Level > lvl,
                        ">=" => session.State.Level >= lvl,
                        "<" => session.State.Level < lvl,
                        "<=" => session.State.Level <= lvl,
                        "=" or "==" => session.State.Level == lvl,
                        _ => false
                    };
                }
                return false;

            case "CHECKITEM":
                if (parts.Length >= 2)
                {
                    string itemName = parts[1];
                    int requiredCount = (parts.Length >= 3 && int.TryParse(parts[2], out int c)) ? c : 1;
                    int count = 0;
                    for (int i = 0; i < session.State.BagItems.Count; i++)
                    {
                        if (session.State.BagItems[i].Name.Equals(itemName, StringComparison.OrdinalIgnoreCase))
                            count++;
                    }
                    return count >= requiredCount;
                }
                return false;

            case "CHECKJOB":
                if (parts.Length >= 2)
                {
                    string jobName = parts[1].ToLowerInvariant();
                    int targetJob = jobName switch
                    {
                        "warrior" or "战士" => 0,
                        "wizard" or "mage" or "法师" => 1,
                        "taoist" or "道士" => 2,
                        _ => -1
                    };
                    return session.State.Job == targetJob;
                }
                return false;

            case "CHECKPKPOINT":
                if (parts.Length >= 3 && int.TryParse(parts[2], out int pk))
                {
                    string op = parts[1];
                    return op switch
                    {
                        ">" => session.State.PkPoints > pk,
                        ">=" => session.State.PkPoints >= pk,
                        "<" => session.State.PkPoints < pk,
                        "<=" => session.State.PkPoints <= pk,
                        "=" => session.State.PkPoints == pk,
                        _ => false
                    };
                }
                return false;

            case "CHECKBAGSIZE":
                if (parts.Length >= 2 && int.TryParse(parts[1], out int freeSlots))
                {
                    return (40 - session.State.BagItems.Count) >= freeSlots;
                }
                return false;

            case "CHECKGENDER":
                if (parts.Length >= 2)
                {
                    string g = parts[1].ToLowerInvariant();
                    int sex = (g == "man" || g == "男") ? 0 : 1;
                    return session.State.Sex == sex;
                }
                return false;

            default:
                return true;
        }
    }
}
