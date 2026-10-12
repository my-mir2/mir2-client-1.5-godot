using System;
using System.Collections.Generic;
using System.Text;
using Mir2.Shared.Models;

namespace Mir2.Shared.Plugins;

/// <summary>
/// 插件化 NPC 交互玩家会话抽象 (与具体服务端网络协议解耦)
/// </summary>
public interface INpcPlayerSession
{
    int SessionId { get; }
    string CharacterName { get; }
    PlayerState? State { get; }
    int CurrentMapId { get; set; }
    int X { get; set; }
    int Y { get; set; }
    bool IsStorageOpen { get; set; }
    int StorageNpcUid { get; set; }
    void Teleport(int mapId, int x, int y);
    void SendSysMsg(string message);
    void EnqueuePacket(short ident, int recog, int param, int tag, int series, ReadOnlySpan<char> extra = default);
}

/// <summary>
/// NPC 强类型对话上下文模型 (具备 100% 编译期静态安全与类型感知)
/// </summary>
public sealed class NpcDialogContext
{
    public INpcPlayerSession Session { get; }
    public string NpcName { get; }
    public int NpcId { get; }
    public string SelectedAction { get; set; } = "@main";
    public string DialogText { get; set; } = string.Empty;
    public List<(string label, string actionTag)> Hyperlinks { get; } = new();
    public bool CloseWindow { get; set; }
    public bool HasTeleportIntent { get; set; }
    public int TargetMapId { get; set; }
    public int TargetX { get; set; }
    public int TargetY { get; set; }
    public string TeleportMessage { get; set; } = string.Empty;

    public NpcDialogContext(INpcPlayerSession session, string npcName, string initialAction = "@main", int npcId = 0)
    {
        Session = session;
        NpcName = npcName;
        SelectedAction = initialAction;
        NpcId = npcId;
    }

    public void RequestTeleport(int mapId, int x, int y, string message = "")
    {
        HasTeleportIntent = true;
        TargetMapId = mapId;
        TargetX = x;
        TargetY = y;
        TeleportMessage = message;
        CloseWindow = true;
        Session.Teleport(mapId, x, y);
    }

    public void AddLink(string label, string actionTag) =>
        Hyperlinks.Add((label, actionTag));

    public string BuildFinalDialogPayload()
    {
        var sb = new StringBuilder();
        sb.AppendLine(DialogText);
        for (int i = 0; i < Hyperlinks.Count; i++)
        {
            var (lbl, act) = Hyperlinks[i];
            sb.AppendLine($"<{lbl}/{act}>");
        }
        return sb.ToString().TrimEnd();
    }
}

/// <summary>
/// 现代化强类型 NPC 行为树插件接口 (INpcBehavior)
/// 彻底淘汰旧易错的 TXT 语法，在开发机本地编译期即可捕获一切语法与逻辑错误！
/// </summary>
public interface INpcBehavior
{
    int NpcId { get; }
    string NpcName { get; }
    void OnInteract(NpcDialogContext ctx);
}

/// <summary>
/// 插件装载结果状态包
/// </summary>
public sealed class PluginLoadResult
{
    public bool Success { get; init; }
    public string Message { get; set; } = string.Empty;
    public List<string> LoadedNpcNames { get; } = new();
    public int LoadedCount => LoadedNpcNames.Count;
}
