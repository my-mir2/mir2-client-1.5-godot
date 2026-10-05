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
    }

    public event Action<EntityData>? EntityAdded;
    public event Action<int>? EntityRemoved;
    public event Action<int, byte>? EntityTurned;
    public event Action<int, int, int, byte>? EntityMoved;
    public event Action<int, int, int>? EntityStruck;
    public event Action<int>? EntityDied;

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
}
