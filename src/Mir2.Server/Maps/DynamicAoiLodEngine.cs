using System;
using System.Collections.Generic;
using Mir2.Server.Network;
using Mir2.Shared.Protocols;

namespace Mir2.Server.Maps;

/// <summary>
/// 盛大官方 1.5 沙巴克皇宫 200+ 极限密度 AOI 降级与广播限流引擎 (Dynamic AOI LOD & Broadcast Decimation Engine)
/// 针对狭小室内地图（如 15x15 沙巴克皇宫）200~300 人同屏铺满火墙、连续施放冰咆哮时，
/// 解决九宫格 AOI 退化为全量 O(N^2) 笛卡尔积广播导致的延迟飙升与网关缓冲区溢出。
/// </summary>
public sealed class DynamicAoiLodEngine
{
    public enum DensityLevel
    {
        Normal = 0,   // 常规视野 (< 30 人)：100% 全量物理保真广播
        Crowded = 1,  // 拥挤场景 (30 ~ 79 人)：轻度降级，合并非必要转向
        Extreme = 2   // 极限密度 (>= 80 人，如沙城皇宫决战)：核心保障模式，严密分级限流
    }

    public enum PacketPriority
    {
        P0_Critical = 0,   // 绝对必达 (0 丢包)：HP/MP 结算、阵亡、复活、攻击受击、施法、装备与控制
        P1_Secondary = 1,  // 次要事件 (可降采样)：实体移动步伐、属性非关键刷新
        P2_Cosmetic = 2    // 纯表现光效 (极限下限流)：转向 (Turn)、轻微地表光效 (SpellEffect)
    }

    public int CrowdedThreshold { get; set; } = 30;
    public int ExtremeThreshold { get; set; } = 80;

    // 单 Tick 内每个会话允许接收的最大 P2 纯表现封包数 (防止击穿 Socket SendBuffer)
    public int MaxP2PacketsPerTickExtreme { get; set; } = 5;

    // 统计指标
    public long TotalBroadcastRequests { get; private set; }
    public long TotalFilteredP2Packets { get; private set; }
    public long TotalBatchedPackets { get; private set; }

    /// <summary>
    /// 判定指定协议号的优先级分级
    /// </summary>
    public static PacketPriority ClassifyPacket(short ident)
    {
        return ident switch
        {
            // P0: 绝对必达核心物理结算与生命周期
            ProtocolConsts.SM_OBJECTSTRUCK_DELPHI or
            ProtocolConsts.SM_DEATH or
            ProtocolConsts.SM_DEATH_ACTION_DELPHI or
            ProtocolConsts.SM_REVIVE or
            ProtocolConsts.SM_HIT_DELPHI or
            ProtocolConsts.SM_OBJECTSPELL or
            ProtocolConsts.SM_OBJECTSTRUCK or
            ProtocolConsts.SM_CHANGENAMECOLOR or
            ProtocolConsts.SM_MYSTATUS => PacketPriority.P0_Critical,

            // P1: 实体位移与基础进出视野
            ProtocolConsts.SM_WALK_DELPHI or
            ProtocolConsts.SM_RUN_DELPHI or
            ProtocolConsts.SM_LOGON_DELPHI or
            ProtocolConsts.SM_USERNAME => PacketPriority.P1_Secondary,

            // P2: 表现层微调 (转向、微粒)
            ProtocolConsts.SM_TURNDIRECTION or
            ProtocolConsts.SM_SPELLEFFECT_DELPHI => PacketPriority.P2_Cosmetic,

            _ => PacketPriority.P0_Critical // 默认未分类协议视为关键数据保障通过
        };
    }

    /// <summary>
    /// 根据当前区域实体数量评估负载等级
    /// </summary>
    public DensityLevel EvaluateDensity(int nearbyEntityCount)
    {
        if (nearbyEntityCount >= ExtremeThreshold) return DensityLevel.Extreme;
        if (nearbyEntityCount >= CrowdedThreshold) return DensityLevel.Crowded;
        return DensityLevel.Normal;
    }

    /// <summary>
    /// 在极限密度下判定某条封包是否应对该目标会话放行
    /// </summary>
    /// <param name="density">当前九宫格密度等级</param>
    /// <param name="priority">封包优先级</param>
    /// <param name="sourceSessionId">发出者 SessionId</param>
    /// <param name="targetSessionId">接收者 SessionId</param>
    /// <param name="p2SentCountThisTick">本 Tick 该目标已接收的 P2 数量</param>
    /// <param name="tickSequence">全局 Tick 序号</param>
    /// <returns>true 放行，false 限流削峰</returns>
    public bool ShouldDeliverPacket(
        DensityLevel density,
        PacketPriority priority,
        int sourceSessionId,
        int targetSessionId,
        int p2SentCountThisTick,
        long tickSequence)
    {
        TotalBroadcastRequests++;

        // 1. P0 关键物理逻辑封包：任何负载下绝对放行 (Zero-Drop)
        if (priority == PacketPriority.P0_Critical)
        {
            return true;
        }

        // 2. 发给自己的封包：绝对放行
        if (sourceSessionId == targetSessionId)
        {
            return true;
        }

        // 3. 常规负载下：全部放行
        if (density == DensityLevel.Normal)
        {
            return true;
        }

        // 4. 拥挤场景 (Crowded)：对 P2 表现进行二分降采样
        if (density == DensityLevel.Crowded)
        {
            if (priority == PacketPriority.P2_Cosmetic)
            {
                // 奇数 Tick 放行，偶数 Tick 滤除，降低 50% 转向广播
                if ((tickSequence + sourceSessionId) % 2 != 0)
                {
                    TotalFilteredP2Packets++;
                    return false;
                }
            }
            return true;
        }

        // 5. 极限密度 (Extreme，如沙巴克皇宫 200+ 人)：
        // P1 次要封包放行；P2 严格受限流 Token 约束 (超额直接丢弃)
        if (priority == PacketPriority.P2_Cosmetic)
        {
            if (p2SentCountThisTick >= MaxP2PacketsPerTickExtreme)
            {
                TotalFilteredP2Packets++;
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// 高密度聚批广播辅助器：将多条待广播封包打包或流控后投递
    /// </summary>
    public int BroadcastWithLod(
        ClientSession source,
        short ident,
        int recog,
        int param,
        int tag,
        int series,
        string extra,
        IReadOnlyList<ClientSession> targets,
        long tickSequence)
    {
        if (targets == null || targets.Count == 0) return 0;

        var priority = ClassifyPacket(ident);
        var density = EvaluateDensity(targets.Count);
        int deliveredCount = 0;

        for (int i = 0; i < targets.Count; i++)
        {
            var target = targets[i];
            if (target == null) continue;

            // 检查目标在本 Tick 内的限流状态
            int p2Count = target.P2PacketsSentThisTick;

            if (ShouldDeliverPacket(density, priority, source.SessionId, target.SessionId, p2Count, tickSequence))
            {
                target.EnqueuePacket(ident, recog, param, tag, series, extra);
                deliveredCount++;
                if (priority == PacketPriority.P2_Cosmetic)
                {
                    target.P2PacketsSentThisTick++;
                }
            }
        }

        TotalBatchedPackets += deliveredCount;
        return deliveredCount;
    }
}
