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

    public const int SabukCityMapId = 3; // 盟重省沙巴克城内
    public const int SabukCityReviveX = 644;
    public const int SabukCityReviveY = 290;

    public const int MengZhengCitySafeX = 335; // 盟重土城安全区
    public const int MengZhengCitySafeY = 330;

    public const int NormalCityMapId = 0; // 比奇省
    public const int NormalCitySafeX = 330;
    public const int NormalCitySafeY = 270;

    public sealed class DeathDropResult
    {
        public List<EquipItem> DroppedBagItems { get; } = new();
        public List<int> DroppedBagSlots { get; } = new(); // 记录被掉落的背包原始槽位
        public List<EquipItem> DroppedEquipItems { get; } = new();
        public List<int> DroppedEquipSlots { get; } = new();
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

        // 1. 背包掉落概率 (白名 10%, 黄名 25%, 红名 100% 官方大爆必爆 0x4BB44C)
        int bagDropChance = isRed ? 100 : (isYellow ? 25 : 10);
        for (int i = state.BagItems.Count - 1; i >= 0; i--)
        {
            if (rng.Next(100) < bagDropChance)
            {
                var item = state.BagItems[i];
                state.BagItems.RemoveAt(i);
                result.DroppedBagItems.Add(item);
                result.DroppedBagSlots.Add(i);
            }
        }
        if (result.DroppedBagItems.Count > 0)
        {
            state.RecalculateAuthoritativeWeights(state.Gold);
        }

        // 2. 身上装备掉落概率 (白名 2%, 黄名 10%, 红名 35%)
        int equipDropChance = isRed ? 35 : (isYellow ? 10 : 2);
        for (int i = 0; i < 9; i++)
        {
            var equip = state.Equipment[i];
            if (equip == null) continue;

            // 盛大官方 1.5 权威规则：祈祷头盔死亡必定碎裂销毁 (不掉落到地表)
            if (i == (int)OfficialEquipSlot.Helmet && equip.Name.Contains("祈祷头盔"))
            {
                state.Equipment[i] = null;
                continue;
            }

            if (rng.Next(100) < equipDropChance)
            {
                state.Equipment[i] = null;
                result.DroppedEquipItems.Add(equip);
                result.DroppedEquipSlots.Add(i);
            }
        }

        if (result.DroppedEquipItems.Count > 0)
        {
            state.RecalculateAttributes();
        }

        return result;
    }

    /// <summary>
    /// 盛大官方 1.4/1.5 角色死亡复活点权威路由 (沙城守军/红名流放/就近主城)
    /// </summary>
    public static (int mapId, int x, int y, string message) GetReviveLocation(
        PlayerState state,
        int currentMapId = 0,
        SabukSiegeEngine? sabukEngine = null,
        string? playerGuild = null)
    {
        // 1. 红名玩家无论身处何处阵亡，灵魂强制流放至盟重红名村
        if (state != null && state.IsRedName)
        {
            return (RedTownMapId, RedTownSafeX, RedTownSafeY, "你在盟重红名村复活。");
        }

        // 2. 沙巴克攻城战期间阵亡处理
        if (sabukEngine != null && sabukEngine.Phase == SabukSiegeEngine.WarPhase.InWar)
        {
            // 在沙巴克城或沙巴克皇宫阵亡 (MapId 3 或 150)
            if (currentMapId == SabukSiegeEngine.SabukCastleMapId || currentMapId == SabukSiegeEngine.SabukPalaceMapId)
            {
                // 属于沙巴克守城行会成员 -> 复活在沙巴克城池内部复活点 (3, 644, 290)
                if (!string.IsNullOrWhiteSpace(playerGuild) &&
                    playerGuild.Equals(sabukEngine.OwnerGuild, StringComparison.OrdinalIgnoreCase))
                {
                    return (SabukCityMapId, SabukCityReviveX, SabukCityReviveY, "你在沙巴克城复活。");
                }

                // 攻击方行会或未参战散人 -> 死亡回盟重土城安全区复活
                return (SabukCityMapId, MengZhengCitySafeX, MengZhengCitySafeY, "你在盟重土城复活。");
            }
        }

        // 3. 常规区域死亡复活: 盟重及下属地图回盟重土城，比奇及附属地图回比奇大城
        if (currentMapId != 0)
        {
            return (SabukCityMapId, MengZhengCitySafeX, MengZhengCitySafeY, "你在盟重土城安全区复活回城。");
        }

        return (NormalCityMapId, NormalCitySafeX, NormalCitySafeY, "你在主城安全区复活回城。");
    }

    /// <summary>
    /// 兼容重载
    /// </summary>
    public static (int mapId, int x, int y, string message) GetReviveLocation(PlayerState state)
        => GetReviveLocation(state, 0, null, null);

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

    /// <summary>
    /// 盛大官方 1.5 红名/首领大爆金币与战利品同心圆螺旋辐射散落算法 (IDA sub_46AEF0)
    /// </summary>
    public static List<(int x, int y)> GenerateSpiralDropCoords(int centerX, int centerY, int count, Func<int, int, bool> isWalkable)
    {
        var coords = new List<(int x, int y)>(count);
        if (count <= 0) return coords;

        // 第 1 堆优先落在中心点
        if (isWalkable(centerX, centerY))
        {
            coords.Add((centerX, centerY));
        }

        int layer = 1;
        int maxLayer = 5;

        while (coords.Count < count && layer <= maxLayer)
        {
            // 环绕第 layer 层正方形边缘顺时针排布
            for (int x = centerX - layer; x <= centerX + layer && coords.Count < count; x++)
            {
                int topY = centerY - layer;
                if (isWalkable(x, topY)) coords.Add((x, topY));
                int botY = centerY + layer;
                if (coords.Count < count && isWalkable(x, botY)) coords.Add((x, botY));
            }
            for (int y = centerY - layer + 1; y <= centerY + layer - 1 && coords.Count < count; y++)
            {
                int leftX = centerX - layer;
                if (isWalkable(leftX, y)) coords.Add((leftX, y));
                int rightX = centerX + layer;
                if (coords.Count < count && isWalkable(rightX, y)) coords.Add((rightX, y));
            }
            layer++;
        }

        // 若地表阻挡严重未填满，用中心点保底
        while (coords.Count < count)
        {
            coords.Add((centerX, centerY));
        }

        return coords;
    }
}
