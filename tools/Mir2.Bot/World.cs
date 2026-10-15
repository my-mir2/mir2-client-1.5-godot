using System.Text;

namespace Mir2.Bot;

public enum Kind { Monster, Npc, Player }

public sealed class Ent
{
    public int Uid; public string Name = ""; public int X, Y; public Kind Kind; public bool Dead; public long DeadAtMs;
    public int Appr;
}

public sealed record Ground(int Uid, string Name, int X, int Y, long AtMs, string Raw);

/// <summary>机器人眼中的世界: 完全由服务端下发的包推导，不读服务端任何内部状态。</summary>
public sealed class World
{
    public int SelfUid; public int X, Y, MapNum = -1; public string MapId = "", MapName = "";
    public int Hp, Mp, MaxHp, MaxMp, Level, Gold = -1, BonusPoint = 0;
    public long Exp, MaxExp;
    public List<(long At, int Level)> LevelUps = new();
    public bool SelfDead; public int Rollbacks;
    public Dictionary<int, Ent> Ents = new();
    public Dictionary<int, Ground> Ground = new();
    public List<(long At, int Uid, int Dmg, int X, int Y, string Who)> Struck = new();
    public List<(long At, int Uid, string Name)> Revives = new();
    public List<(long At, int Uid)> Deaths = new();
    public List<(long At, string Text)> SysMsgs = new();
    public List<(long At, string Name)> BagAdds = new();
    public List<(long At, int Uid)> GroundGone = new();
    public List<(long At, int NpcUid, string Dialog)> NpcDialogs = new();
    public List<(long At, int ItemUid, string Name)> BuySuccesses = new();
    public List<(long At, string Error)> BuyFails = new();
    public List<long> EatSuccesses = new();
    public Dictionary<int, (string Name, int Level, int Key)> Magics = new();
    public List<(long At, int MagicId, int Level, int Key)> MagicUpdates = new();
    public List<(long At, int CasterUid, int MagicId, int X, int Y)> SpellsCast = new();
    public List<(long At, int CasterUid, int TargetX, int TargetY, int MagicId, string Name)> SpellEffects = new();
    public List<(long At, int EquipSlot, string Name)> TakeOns = new();
    public List<(long At, int EquipSlot)> TakeOffs = new();
    public List<(long At, int ItemUid, string Name)> DelItems = new();
    public List<(long At, int BagWeight, int WearWeight, int HandWeight)> WeightChanges = new();
    public List<(long At, int Count, string Members)> GroupLists = new();
    public List<(long At, int CorpseUid, string MeatDesc)> ButchResults = new();
    public List<(long At, int ItemUid, int GoldEarned, string Name)> SellSuccesses = new();
    public List<(long At, int GoldRemaining, int Dura, int MaxDura, string Name)> RepairSuccesses = new();
    public Dictionary<int, int> PacketCounts = new();
    public Dictionary<int, List<Pkt>> Samples = new();

    public int Apply(Conn c)
    {
        int n = 0;
        while (c.Inbox.TryDequeue(out var p)) { Apply(p); n++; }
        return n;
    }

    private static string[] Split(string s) => s.Split(new[] { '\t', '/' }, StringSplitOptions.None);
    private static bool I(string[] a, int i, out int v) { v = 0; return i < a.Length && int.TryParse(a[i], out v); }

    private void Apply(Pkt p)
    {
        PacketCounts[p.Id] = PacketCounts.GetValueOrDefault(p.Id) + 1;
        if (!Samples.TryGetValue(p.Id, out var sl)) Samples[p.Id] = sl = new();
        if (sl.Count < 3) sl.Add(p);

        switch (p.Id)
        {
            case 50: SelfUid = p.Recog; break;                                   // 主角进入世界
            case 521:                                                           // 换图 / 进图
                MapNum = p.Recog; X = p.Param; Y = p.Tag;
                var nm = p.Extra.Split('|');
                MapId = nm.Length > 1 ? nm[0] : p.Recog.ToString(); MapName = nm.Length > 1 ? nm[1] : p.Extra;
                Ents.Clear(); Ground.Clear();
                SelfDead = false; // 换图/回城复活都会下发 521
                break;
            case 220: case 230: case 10:
            {
                var a = Split(p.Extra);
                var kind = p.Id == 220 ? Kind.Monster : p.Id == 230 ? Kind.Npc : Kind.Player;
                int x = p.Param, y = p.Tag;
                if (I(a, 1, out var ax) && I(a, 2, out var ay)) { x = ax; y = ay; }
                I(a, 4, out var appr);
                if (p.Recog == SelfUid && p.Id == 10) { X = x; Y = y; break; }
                Ents[p.Recog] = new Ent { Uid = p.Recog, Name = a[0], X = x, Y = y, Kind = kind, Appr = appr };
                break;
            }
            case 11: Ents.Remove(p.Recog); break;
            case 240: case 241:
                if (Ents.TryGetValue(p.Recog, out var e)) { e.X = p.Param; e.Y = p.Tag; }
                break;
            case 501:                                                            // SM_REVIVE 复活应答
                Revives.Add((p.AtMs, p.Recog, p.Extra));
                if (p.Recog == SelfUid) { SelfDead = false; X = p.Param; Y = p.Tag; }
                else if (Ents.TryGetValue(p.Recog, out var rv)) { rv.Dead = false; rv.X = p.Param; rv.Y = p.Tag; }
                break;
            case 52:                                                             // 服务端位置纠正 (拉回)
                if (p.Recog == SelfUid) { X = p.Param; Y = p.Tag; Rollbacks++; }
                break;
            case 53: Hp = p.Recog; Mp = p.Param; MaxHp = p.Tag; MaxMp = p.Series; break;
            case 648:                                                            // SM_ABILITY 或 SM_BUY_OK_DELPHI
            {
                var a = Split(p.Extra);
                if (p.Param == 0 && p.Tag == 0 && p.Series == 0 && !string.IsNullOrEmpty(p.Extra) && (a.Length < 3 || !int.TryParse(a[0], out _)))
                {
                    BuySuccesses.Add((p.AtMs, p.Recog, p.Extra));
                }
                else
                {
                    Level = p.Recog; Hp = p.Param; MaxHp = p.Tag; Mp = p.Series;
                    if (a.Length >= 3) { if (I(a, 0, out var mm)) MaxMp = mm; if (long.TryParse(a[1], out var ex)) Exp = ex; if (long.TryParse(a[2], out var mx)) MaxExp = mx; }
                    if (a.Length >= 10 && I(a, 9, out var gold)) Gold = gold;
                    if (a.Length >= 11 && I(a, 10, out var bonus)) BonusPoint = bonus;
                }
                break;
            }
            case 45: Exp = p.Recog; if (p.Param > 0) MaxExp = p.Param; break;
            case 244: case 637:
                Struck.Add((p.AtMs, p.Recog, p.Series, p.Param, p.Tag, p.Extra));
                break;
            case 28: case 21:
                Deaths.Add((p.AtMs, p.Recog));
                if (p.Recog == SelfUid) SelfDead = true;
                if (Ents.TryGetValue(p.Recog, out var d)) { d.Dead = true; d.DeadAtMs = p.AtMs; }
                break;
            case 314: case 625:
            {
                var a = Split(p.Extra);
                string name = a.FirstOrDefault(s => s.Length > 0 && !int.TryParse(s, out _)) ?? "?";
                int gx = p.Param, gy = p.Tag;
                var nums = a.Where(s => int.TryParse(s, out _)).Select(int.Parse).ToArray();
                if (nums.Length >= 2) { gx = nums[0]; gy = nums[1]; }
                Ground[p.Recog] = new Ground(p.Recog, name, gx, gy, p.AtMs, p.ToString());
                break;
            }
            case 315:
            case 805: Ground.Remove(p.Recog); GroundGone.Add((p.AtMs, p.Recog)); break;
            case 200: BagAdds.Add((p.AtMs, p.Extra)); break;
            case 653: Gold = p.Recog; break;
            case 100: SysMsgs.Add((p.AtMs, p.Extra)); break;
            case 44: if (p.Param == 0 && p.Recog > 0) { Level = p.Recog; LevelUps.Add((p.AtMs, p.Recog)); } break; // 自己升级: (等级,0,0,0)；他人升级为 (会话号,等级,..)
            case 54: NpcDialogs.Add((p.AtMs, p.Recog, p.Extra)); break;               // SM_NPCDIALOG
            case 128:
            case 606: EatSuccesses.Add(p.AtMs); break;                                 // SM_EAT_OK
            case 651: BuyFails.Add((p.AtMs, p.Extra)); break;                          // SM_BUY_FAIL_DELPHI
            case 201:                                                                  // SM_SENDUSERMAGIC
            {
                var lines = p.Extra.Split(new[] { '\r', '\n', ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    var parts = line.Split(new[] { '/', '|' }, StringSplitOptions.None);
                    if (parts.Length >= 2 && int.TryParse(parts[0], out int mid))
                    {
                        int key = parts.Length > 3 && int.TryParse(parts[3], out int k) ? k : 0;
                        int lvl = parts.Length > 4 && int.TryParse(parts[4], out int l) ? l : 0;
                        Magics[mid] = (parts[1], lvl, key);
                    }
                }
                break;
            }
            case 203:                                                                  // SM_UPDATEUSERMAGIC
            {
                int mid = p.Recog, lvl = p.Param, key = p.Tag;
                MagicUpdates.Add((p.AtMs, mid, lvl, key));
                if (Magics.TryGetValue(mid, out var prev)) Magics[mid] = (prev.Name, lvl, key);
                else Magics[mid] = ($"Magic_{mid}", lvl, key);
                break;
            }
            case 243:                                                                  // SM_OBJECTSPELL
                SpellsCast.Add((p.AtMs, p.Recog, p.Series, p.Param, p.Tag));
                break;
            case 614:                                                                  // SM_SPELLEFFECT_DELPHI
                SpellEffects.Add((p.AtMs, p.Recog, p.Param, p.Tag, p.Series, p.Extra));
                break;
            case 615:                                                                  // SM_TAKEON_OK
                TakeOns.Add((p.AtMs, p.Param, p.Extra));
                break;
            case 619:                                                                  // SM_TAKEOFF_OK
                TakeOffs.Add((p.AtMs, p.Param));
                break;
            case 202:                                                                  // SM_DELITEM
                DelItems.Add((p.AtMs, p.Recog, p.Extra));
                break;
            case 622:                                                                  // SM_WEIGHTCHANGED
                WeightChanges.Add((p.AtMs, p.Recog, p.Param, p.Tag));
                break;
            case 667:                                                                  // SM_GROUPMEMBERS
                GroupLists.Add((p.AtMs, p.Recog, p.Extra));
                break;
            case 665:                                                                  // SM_BUTCH (官方割肉回显)
                ButchResults.Add((p.AtMs, p.Recog, p.Extra));
                break;
            case 650:                                                                  // SM_SELL_OK_DELPHI (官方出售物品成功)
                SellSuccesses.Add((p.AtMs, p.Recog, p.Param, p.Extra));
                break;
            case 669:                                                                  // SM_REPAIROK_DELPHI (官方修理装备成功)
                RepairSuccesses.Add((p.AtMs, p.Recog, p.Param, p.Tag, p.Extra));
                break;
        }
    }
}
