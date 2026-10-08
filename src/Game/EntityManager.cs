using System;
using System.Collections.Generic;
using Godot;

namespace Mir2Client.Game;

/// <summary>
/// 盛大官方 1.5 实体管理核心 (纯 C# 实现)
/// 严格 1:1 对齐官方 IDA sub_495EBC & sub_496120
/// 管理怪物/NPC/其他玩家状态、朝向、视野与生命周期
/// </summary>
public class EntityManager
{
    public enum EntityType
    {
        Player = 0,
        Monster = 1,
        Npc = 2
    }

    public sealed class EntityData
    {
        public int Uid { get; set; }
        public string Name { get; set; } = string.Empty;
        public EntityType Type { get; set; }
        public int Appr { get; set; } = 0;
        public int TileX { get; set; }
        public int TileY { get; set; }
        public byte Direction { get; set; } = 2;
        public int Hp { get; set; } = 100;
        public int MaxHp { get; set; } = 100;
        public bool IsDead { get; set; }
        public bool IsCorpse { get; set; }
        public bool CanButcher { get; set; }

        // 道士施毒术中毒与随从状态 (官方实证 0x49350C（已证client无函数） / sub_4D35B0)
        public int AntiPoison { get; set; } = 0; // 目标中毒抗性
        public int PoisonMask { get; set; } = 0; // Bit 0: 绿毒, Bit 1: 红毒
        public long GreenPoisonExpireTick { get; set; } = 0;
        public long RedPoisonExpireTick { get; set; } = 0;
        public long NextPoisonTick { get; set; } = 0;
        public int PoisonStrength { get; set; } = 0;
        public int MasterUid { get; set; } = 0;  // 随从主人 UID
        public int PetLevel { get; set; } = 0;   // 随从等级 (0~7)
        public int PkPoint { get; set; } = 0;    // PK 善恶点数 (>=200 为红名)
        public string GuildName { get; set; } = string.Empty; // 盛大官方 1.5 实体所属行会名 (SM_GUILDNAME_DELPHI 42)
        public string GuildRank { get; set; } = string.Empty; // 实体行会封号/职位
        public int NameColor { get; set; } = 0;  // 名字颜色值 (SM_CHANGENAMECOLOR 41)
    }

    public event Action<EntityData>? EntityAdded;
    public event Action<int>? EntityRemoved;
    public event Action<int, byte>? EntityTurned;
    public event Action<int, int, int, byte>? EntityMoved;
    public event Action<int, int, int, byte, bool>? EntityStepMoved; // uid, x, y, dir, isRun
    public event Action<int, int, int, byte>? EntityReconciled;     // uid, serverX, serverY, dir
    public event Action<int, int, int>? EntityStruck;
    public event Action<int>? EntityDied;
    public event Action<int, int>? EntityPoisonChanged; // uid, poisonMask
    public event Action<int, int>? EntityPkChanged;     // uid, pkPoint
    public event Action<int, string, string>? EntityGuildChanged; // uid, guildName, guildRank
    public event Action<int, int>? EntityNameColorChanged;         // uid, color

    private readonly Dictionary<int, EntityData> _entities = new();

    public int Count => _entities.Count;
    public IEnumerable<EntityData> AllEntities => _entities.Values;

    public void AddEntity(EntityData data)
    {
        _entities[data.Uid] = data;
        EntityAdded?.Invoke(data);
    }

    public void AddEntity(int uid, string name, EntityType type, int x, int y, byte dir = 2, int appr = 0)
    {
        var data = new EntityData
        {
            Uid = uid,
            Name = name,
            Type = type,
            Appr = appr,
            TileX = x,
            TileY = y,
            Direction = dir
        };
        _entities[uid] = data;
        EntityAdded?.Invoke(data);
    }

    public EntityData? GetEntity(int uid)
    {
        return _entities.TryGetValue(uid, out var data) ? data : null;
    }

    /// <summary>
    /// 获取指定地块上的活体实体 (优先返回非尸体/活体，无活体则返回最新实体)
    /// </summary>
    public EntityData? GetEntityAt(int tileX, int tileY)
    {
        EntityData? candidate = null;
        foreach (var ent in _entities.Values)
        {
            if (ent.TileX == tileX && ent.TileY == tileY)
            {
                if (!ent.IsDead) return ent;
                candidate ??= ent;
            }
        }
        return candidate;
    }

    /// <summary>
    /// 获取指定地块上的所有实体列表
    /// </summary>
    public List<EntityData> GetEntitiesAt(int tileX, int tileY)
    {
        var list = new List<EntityData>();
        foreach (var ent in _entities.Values)
        {
            if (ent.TileX == tileX && ent.TileY == tileY)
            {
                list.Add(ent);
            }
        }
        return list;
    }

    public bool RemoveEntity(int uid)
    {
        if (_entities.Remove(uid))
        {
            EntityRemoved?.Invoke(uid);
            return true;
        }
        return false;
    }

    public void UpdateTurn(int uid, byte dir)
    {
        if (_entities.TryGetValue(uid, out var ent))
        {
            ent.Direction = dir;
            EntityTurned?.Invoke(uid, dir);
        }
    }

    public void UpdateMove(int uid, int x, int y, byte dir)
    {
        if (_entities.TryGetValue(uid, out var ent))
        {
            ent.TileX = x;
            ent.TileY = y;
            ent.Direction = dir;
            EntityMoved?.Invoke(uid, x, y, dir);
            EntityStepMoved?.Invoke(uid, x, y, dir, false);
        }
    }

    public void UpdateStepMove(int uid, int x, int y, byte dir, bool isRun)
    {
        if (_entities.TryGetValue(uid, out var ent))
        {
            ent.TileX = x;
            ent.TileY = y;
            ent.Direction = dir;
            EntityMoved?.Invoke(uid, x, y, dir);
            EntityStepMoved?.Invoke(uid, x, y, dir, isRun);
        }
    }

    public void UpdateReconcile(int uid, int x, int y, byte dir)
    {
        if (_entities.TryGetValue(uid, out var ent))
        {
            ent.TileX = x;
            ent.TileY = y;
            ent.Direction = dir;
            EntityReconciled?.Invoke(uid, x, y, dir);
        }
    }

    public void TriggerStruck(int uid, int damage, int curHp)
    {
        if (_entities.TryGetValue(uid, out var ent))
        {
            ent.Hp = curHp;
            if (curHp <= 0) ent.IsDead = true;
            EntityStruck?.Invoke(uid, damage, curHp);
        }
    }

    public void TriggerDeath(int uid)
    {
        if (_entities.TryGetValue(uid, out var ent))
        {
            ent.IsDead = true;
            ent.Hp = 0;
            EntityDied?.Invoke(uid);
        }
    }

    public void Clear()
    {
        var uids = new List<int>(_entities.Keys);
        _entities.Clear();
        foreach (var uid in uids)
        {
            EntityRemoved?.Invoke(uid);
        }
    }

    /// <summary>
    /// 对实体施加毒药状态 (支持红绿双毒独立计时)
    /// </summary>
    public bool ApplyPoison(int uid, TaoistCombatEngine.PoisonType type, int durationSec, int sc, long nowTick)
    {
        if (!_entities.TryGetValue(uid, out var ent) || ent.IsDead) return false;

        long expireTick = nowTick + durationSec * 1000L;
        int oldMask = ent.PoisonMask;

        if (type == TaoistCombatEngine.PoisonType.Green)
        {
            ent.PoisonMask |= 1;
            ent.GreenPoisonExpireTick = expireTick;
            ent.PoisonStrength = sc;
            ent.NextPoisonTick = nowTick + TaoistCombatEngine.PoisonTickIntervalMs;
        }
        else if (type == TaoistCombatEngine.PoisonType.Red)
        {
            ent.PoisonMask |= 2;
            ent.RedPoisonExpireTick = expireTick;
        }

        if (ent.PoisonMask != oldMask)
        {
            EntityPoisonChanged?.Invoke(uid, ent.PoisonMask);
        }
        return true;
    }

    /// <summary>
    /// 心跳帧驱动中毒周期掉血与到期清除 (每 2500ms 一跳绿毒扣血)
    /// </summary>
    public void ProcessPoisonTicks(long nowTick, Action<int, int, int>? onGreenPoisonDamage = null)
    {
        foreach (var ent in _entities.Values)
        {
            if (ent.IsDead) continue;
            int oldMask = ent.PoisonMask;

            // 1. 绿毒到期判定
            if ((ent.PoisonMask & 1) != 0 && nowTick >= ent.GreenPoisonExpireTick)
            {
                ent.PoisonMask &= ~1;
            }

            // 2. 红毒到期判定
            if ((ent.PoisonMask & 2) != 0 && nowTick >= ent.RedPoisonExpireTick)
            {
                ent.PoisonMask &= ~2;
            }

            // 3. 绿毒周期扣血
            if ((ent.PoisonMask & 1) != 0 && nowTick >= ent.NextPoisonTick)
            {
                ent.NextPoisonTick = nowTick + TaoistCombatEngine.PoisonTickIntervalMs;
                int dmg = TaoistCombatEngine.CalculateGreenPoisonTickDamage(ent.PoisonStrength);
                int newHp = Math.Max(0, ent.Hp - dmg);
                TriggerStruck(ent.Uid, dmg, newHp);
                onGreenPoisonDamage?.Invoke(ent.Uid, dmg, newHp);
            }

            if (ent.PoisonMask != oldMask)
            {
                EntityPoisonChanged?.Invoke(ent.Uid, ent.PoisonMask);
            }
        }
    }

    /// <summary>
    /// 清除实体的全部中毒状态
    /// </summary>
    public void ClearPoison(int uid)
    {
        if (_entities.TryGetValue(uid, out var ent))
        {
            if (ent.PoisonMask != 0)
            {
                ent.PoisonMask = 0;
                ent.GreenPoisonExpireTick = 0;
                ent.RedPoisonExpireTick = 0;
                ent.NextPoisonTick = 0;
                EntityPoisonChanged?.Invoke(uid, 0);
            }
        }
    }

    /// <summary>
    /// 更新实体的 PK 善恶点数 (触发红名/黄名名字变色)
    /// </summary>
    public void UpdatePkPoint(int uid, int pkPoint)
    {
        if (_entities.TryGetValue(uid, out var ent))
        {
            ent.PkPoint = pkPoint;
            EntityPkChanged?.Invoke(uid, pkPoint);
        }
    }

    /// <summary>
    /// 盛大官方 1.5 更新实体所属行会与封号 (SM_GUILDNAME_DELPHI 42, IDA case 42 @ 0x46A16F)
    /// </summary>
    public void UpdateGuild(int uid, string guildName, string rank)
    {
        if (_entities.TryGetValue(uid, out var ent))
        {
            ent.GuildName = guildName ?? string.Empty;
            ent.GuildRank = rank ?? string.Empty;
            EntityGuildChanged?.Invoke(uid, ent.GuildName, ent.GuildRank);
        }
    }

    /// <summary>
    /// 盛大官方 1.5 更新实体名字颜色 (SM_CHANGENAMECOLOR 41, IDA case 41 @ 0x46A157)
    /// </summary>
    public void UpdateNameColor(int uid, int color)
    {
        if (_entities.TryGetValue(uid, out var ent))
        {
            ent.NameColor = color;
            EntityNameColorChanged?.Invoke(uid, color);
        }
    }
}
