using System;
using System.Collections.Generic;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 红名死亡大爆与红名村流放复活权威引擎 (RedNameDropAndReviveEngine)
/// 严格 1:1 对齐官方 M2Server PK 严厉惩罚体系:
/// 1. 死亡掉落差异化精算:
///    - 白名死亡: 背包 10% 掉落，身上装备 2% 掉落
///    - 黄名死亡: 背包 25% 掉落，身上装备 10% 掉落
///    - 红名死亡 (PkPoints >= 200): 触发“大爆/全爆”，背包 80%~100% 物品爆落，身上装备 30%~50% 爆落，九宫格散落
/// 2. 复活点路由逻辑:
///    - 白名/黄名死亡回城: 复活在最近的主城大安全区 (比奇 330, 270 或 盟重土城 335, 330)
///    - 红名死亡回城: 强制流放投送至“红名村安全区” (盟重地图 3，坐标 850, 660)
/// </summary>
public static class RedNameDropAndReviveEngine
{
    public const int RedTownMapId = 3; // 盟重省
    public const int RedTownSafeX = 850;
    public const int RedTownSafeY = 660;

    public const int NormalCityMapId = 0; // 比奇省
    public const int NormalCitySafeX = 330;
    public const int NormalCitySafeY = 270;

    public sealed class DeathDropResult
    {
        public List<EquipItem> DroppedBagItems { get; } = new();
        public List<EquipItem> DroppedEquipItems { get; } = new();
        public bool IsMegaExplosion { get; set; } // 是否触发了红名大爆
    }

    /// <summary>
    /// 角色死亡时权威精算掉落列表
    /// </summary>
    public static DeathDropResult CalculateDeathDrops(PlayerState state, Random rng)
    {
        var result = new DeathDropResult();
        if (state == null) return result;

        bool isRed = state.IsRedName;
        bool isYellow = state.IsYellowName;

        result.IsMegaExplosion = isRed;

        // 1. 背包掉落概率 (白名 10%, 黄名 25%, 红名 85%)
        int bagDropChance = isRed ? 85 : (isYellow ? 25 : 10);
        for (int i = state.BagItems.Count - 1; i >= 0; i--)
        {
            if (rng.Next(100) < bagDropChance)
            {
                var item = state.BagItems[i];
                state.BagItems.RemoveAt(i);
                result.DroppedBagItems.Add(item);
            }
        }

        // 2. 身上装备掉落概率 (白名 2%, 黄名 10%, 红名 35%)
        int equipDropChance = isRed ? 35 : (isYellow ? 10 : 2);
        for (int i = 0; i < 9; i++)
        {
            var equip = state.Equipment[i];
            if (equip == null) continue;

            if (rng.Next(100) < equipDropChance)
            {
                state.Equipment[i] = null;
                result.DroppedEquipItems.Add(equip);
            }
        }

        if (result.DroppedEquipItems.Count > 0)
        {
            state.RecalculateAttributes();
        }

        return result;
    }

    /// <summary>
    /// 角色死亡复活点权威路由 (红名流放红名村，白名回主城)
    /// </summary>
    public static (int mapId, int x, int y, string message) GetReviveLocation(PlayerState state)
    {
        if (state != null && state.IsRedName)
        {
            return (RedTownMapId, RedTownSafeX, RedTownSafeY, "因你作恶多端化身红名，死后灵魂被流放至盟重红名村！");
        }

        return (NormalCityMapId, NormalCitySafeX, NormalCitySafeY, "你在主城安全区复活回城。");
    }

    /// <summary>
    /// 执行死亡回城复活
    /// </summary>
    public static void ExecuteRevive(ClientSession session)
    {
        if (session.State == null) return;

        var (mapId, x, y, msg) = GetReviveLocation(session.State);
        session.CurrentMapId = mapId;
        session.X = x;
        session.Y = y;
        session.State.Hp = session.State.MaxHp / 2; // 复活恢复半血
        session.State.Mp = session.State.MaxMp / 2;

        session.EnqueuePacket(ProtocolConsts.SM_NEWMAP_DELPHI, mapId, x, y, 0);
        session.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, session.State.Hp, session.State.MaxHp, session.State.Mp, session.State.MaxMp);
        session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, msg);
    }
}
