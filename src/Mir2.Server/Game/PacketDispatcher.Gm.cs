using System;
using System.Linq;
using Mir2.Server.Maps;
using Mir2.Server.Network;
using Mir2.Shared.Data;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;
using Mir2.Shared.Rules;

namespace Mir2.Server.Game;

/// <summary>
/// 测试用 GM 聊天命令（仅 GmEnabled=1 / MIR2_GM=1 时生效，默认关闭）。
/// 让无头机器人能直接造场景：设等级/金币/发物品/刷怪/传送，而不必每次从 1 级练起。
/// 命令格式 (聊天框)：@gm level N | gold N | hp N | item 物品名 [数量] | spawn 怪物名 [数量] | tp 地图号 x y
/// 所有结果经系统消息 "GM: ..." 回显，失败也会回显原因。
/// </summary>
public sealed partial class PacketDispatcher
{
    public bool GmEnabled { get; set; }

    private static int _gmItemUid = 1_500_000;

    private void GmReply(ClientSession s, string msg) => s.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "GM: " + msg);

    private void HandleGmCommand(ClientSession session, string cmd)
    {
        if (session.State == null) return;
        var a = cmd.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (a.Length == 0) { GmReply(session, "用法: level|gold|hp|item|spawn|tp"); session.Flush(); return; }
        try
        {
            switch (a[0].ToLowerInvariant())
            {
                case "level" when a.Length >= 2 && int.TryParse(a[1], out int lv):
                    session.State.Level = Math.Clamp(lv, 1, 65);
                    session.State.Exp = 0;
                    session.State.RecalculateBaseHpMp();
                    session.State.RecalculateAttributes();
                    session.State.Hp = session.State.MaxHp; session.State.Mp = session.State.MaxMp;
                    SendAbility(session);
                    GmReply(session, $"等级={session.State.Level} HP={session.State.Hp}/{session.State.MaxHp}");
                    break;
                case "job" when a.Length >= 2 && int.TryParse(a[1], out int job):
                    session.State.Job = (byte)Math.Clamp(job, 0, 2);
                    session.State.RecalculateBaseHpMp();
                    session.State.RecalculateAttributes();
                    SendAbility(session);
                    GmReply(session, $"职业={session.State.Job} HP={session.State.Hp}/{session.State.MaxHp} MP={session.State.Mp}/{session.State.MaxMp}");
                    break;
                case "skill" when a.Length >= 2:
                {
                    int skillLevel = a.Length >= 3 && int.TryParse(a[2], out int slv) ? Math.Clamp(slv, 0, 3) : 3;
                    SkillMeta? meta = null;
                    if (short.TryParse(a[1], out short mid)) meta = SkillFormulas.GetSkill(mid);
                    else meta = SkillFormulas.GetSkill(a[1]);
                    if (meta == null) { GmReply(session, $"官方技能库中未找到技能 {a[1]}"); break; }
                    var userMagic = SkillTrainEngine.LearnSkill(session.State, meta.Value.Id, meta.Value.Name, (byte)skillLevel);
                    userMagic.Level = (byte)skillLevel;
                    // 同步下发给客户端 SM_SENDUSERMAGIC (201)
                    var skillLine = $"{userMagic.MagicId}/{userMagic.Name}/{meta.Value.IconIndex}/{userMagic.Key}/{userMagic.Level}/{userMagic.CurTrain}/{userMagic.MaxTrain}/{userMagic.Name}";
                    session.EnqueuePacket(ProtocolConsts.SM_SENDUSERMAGIC, 0, 0, 0, 0, skillLine);
                    GmReply(session, $"已习得技能 {meta.Value.Name}(ID={meta.Value.Id}) Lv={skillLevel}");
                    break;
                }
                case "bonus" when a.Length >= 2 && int.TryParse(a[1], out int bonus):
                    session.State.BonusPoint = Math.Max(0, bonus);
                    SendAbility(session);
                    GmReply(session, $"属性点={session.State.BonusPoint}");
                    break;
                case "gold" when a.Length >= 2 && int.TryParse(a[1], out int gold):
                    session.State.Gold = Math.Max(0, gold);
                    session.State.RecalculateAuthoritativeWeights(session.State.Gold);
                    SendAbility(session);
                    GmReply(session, $"金币={session.State.Gold}");
                    break;
                case "hp" when a.Length >= 2 && int.TryParse(a[1], out int hp):
                    session.State.Hp = Math.Clamp(hp, 1, session.State.MaxHp);
                    SendAbility(session);
                    GmReply(session, $"HP={session.State.Hp}/{session.State.MaxHp}");
                    break;
                case "die" or "killme":
                {
                    var map = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;
                    AuthoritativeExecutePlayerDeath(session, null, map, Environment.TickCount64);
                    GmReply(session, "已触发死亡处决");
                    break;
                }
                case "item" when a.Length >= 2:
                {
                    int n = a.Length >= 3 && int.TryParse(a[2], out int nn) ? Math.Clamp(nn, 1, 40) : 1;
                    if (!OfficialStdItemsRegistry.ItemsByName.TryGetValue(a[1], out var std)) { GmReply(session, $"官方物品表里没有 {a[1]}"); break; }
                    int given = 0;
                    for (int i = 0; i < n && session.State.BagItems.Count < 40; i++)
                    {
                        var it = new EquipItem { Uid = System.Threading.Interlocked.Increment(ref _gmItemUid), Name = std.Name, Weight = std.Weight, Dura = std.DuraMax, MaxDura = std.DuraMax, Looks = std.Looks };
                        session.State.BagItems.Add(it);
                        session.State.RecalculateAuthoritativeWeights(session.State.Gold);
                        session.EnqueuePacket(ProtocolConsts.SM_ADDITEM, it.Uid, session.State.BagItems.Count - 1, it.Weight, 0, $"{it.Name}\t{it.Dura}\t{it.MaxDura}\t{std.Looks}");
                        given++;
                    }
                    GmReply(session, $"发放 {std.Name} x{given}");
                    break;
                }
                case "spawn" when a.Length >= 2:
                {
                    int n = a.Length >= 3 && int.TryParse(a[2], out int nn) ? Math.Clamp(nn, 1, 20) : 1;
                    var map = _mapManager.GetMap(session.CurrentMapId);
                    if (map == null || RegenEngine == null) { GmReply(session, "当前地图不存在"); break; }
                    var rng = new Random();
                    int made = 0;
                    for (int t = 0; t < n * 40 && made < n; t++)
                    {
                        int x = session.X + rng.Next(-4, 5), y = session.Y + rng.Next(-4, 5);
                        if ((x == session.X && y == session.Y) || !map.IsWalkable(x, y)) continue;
                        var m = RegenEngine.SpawnOfficialNow(session.CurrentMapId, x, y, a[1], rng);
                        if (m == null) { GmReply(session, $"官方怪物属性表里没有 {a[1]}"); break; }
                        BroadcastToNinePatch(session, 220, m.Uid, m.X, m.Y, m.Direction, $"{m.Name}\t{m.X}\t{m.Y}\t{m.Direction}\t{m.Appr}", includeSelf: true);
                        made++;
                    }
                    GmReply(session, $"刷出 {a[1]} x{made}");
                    break;
                }
                case "tp" when a.Length >= 4 && int.TryParse(a[1], out int mapNo) && int.TryParse(a[2], out int tx) && int.TryParse(a[3], out int ty):
                {
                    if (_mapManager.GetMap(mapNo) == null) { GmReply(session, $"地图 {mapNo} 不存在"); break; }
                    TriggerMapPassage(session, new MapPassage { FromMapId = session.CurrentMapId, FromX = session.X, FromY = session.Y, ToMapId = mapNo, ToX = tx, ToY = ty, Description = "GM" });
                    GmReply(session, $"已传送到 {mapNo}({tx},{ty})");
                    break;
                }
                default: GmReply(session, "未知或参数不全: " + cmd); break;
            }
        }
        catch (Exception e) { GmReply(session, "执行失败: " + e.Message); }
        session.Flush();
    }
}
