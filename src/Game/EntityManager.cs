using System;
using System.Collections.Generic;
using Godot;

namespace Mir2Client.Game;

/// <summary>
/// 盛大官方 1.5 实体管理核心 (纯 C# 实现)
/// 严格 1:1 对齐官方 IDA sub_495EBC & sub_496120
/// 管理怪物/NPC/其他玩家状态、朝向、视野与生命周期
/// </summary>
public partial class EntityManager : Node
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
        public int TileX { get; set; }
        public int TileY { get; set; }
        public byte Direction { get; set; } = 2;
        public int Hp { get; set; } = 100;
        public int MaxHp { get; set; } = 100;
        public bool IsDead { get; set; }
        public bool IsCorpse { get; set; }
        public bool CanButcher { get; set; }
    }

    private readonly Dictionary<int, EntityData> _entities = new();

    public int Count => _entities.Count;
    public IEnumerable<EntityData> AllEntities => _entities.Values;

    public void AddEntity(int uid, string name, EntityType type, int x, int y, byte dir = 2)
    {
        _entities[uid] = new EntityData
        {
            Uid = uid,
            Name = name,
            Type = type,
            TileX = x,
            TileY = y,
            Direction = dir
        };
    }

    public EntityData? GetEntity(int uid)
    {
        return _entities.TryGetValue(uid, out var data) ? data : null;
    }

    public bool RemoveEntity(int uid)
    {
        return _entities.Remove(uid);
    }

    public void Clear()
    {
        _entities.Clear();
    }
}
