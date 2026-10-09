using System;

namespace Mir2Client.Game;

/// <summary>
/// 盛大官方 1.5 药水与生命魔法回复系统 (Potion Recovery System)
/// 严格还原官方双轨回复机制：
/// 1. 瞬时回复 (Instant): 太阳水、强效太阳水、疗伤药、万年雪霜，立即加血加魔，无缓回延迟；
/// 2. 持续缓回 (Gradual Tick): 金创药族、魔法药族，总增量注入缓冲池，每 tick 定频逐点灌注，还原传奇经典顶药手感。
/// </summary>
public class PotionSystem
{
    public enum PotionKind
    {
        None = 0,
        GradualHp,      // 金创药族 (缓回)
        GradualMp,      // 魔法药族 (缓回)
        InstantHpMp     // 太阳水族 (瞬回)
    }

    public struct PotionMeta
    {
        public string Name;
        public PotionKind Kind;
        public int TotalHp;
        public int TotalMp;
        public bool IsInstant => Kind == PotionKind.InstantHpMp;
    }

    /// <summary>缓回 Tick 时间步长 (毫秒)，官方约 250ms 一次生命灌注</summary>
    public const int TickIntervalMs = 250;

    /// <summary>每次缓回 Tick 默认灌注 HP 步长</summary>
    public const int DefaultHpTickStep = 4;

    /// <summary>每次缓回 Tick 默认灌注 MP 步长</summary>
    public const int DefaultMpTickStep = 5;

    // 缓回池
    public int PendingHp { get; private set; }
    public int PendingMp { get; private set; }

    // 上次结算时间戳
    public long LastTickMs { get; private set; }

    // 上次使用药水时间戳 (用于普通药水防抖)
    public long LastDrinkMs { get; private set; }

    /// <summary>
    /// 解析药水属性元数据 (官方标准 1.5 经典药水数值)
    /// </summary>
    public static PotionMeta ParsePotion(string itemName)
    {
        if (string.IsNullOrWhiteSpace(itemName))
        {
            return new PotionMeta { Name = string.Empty, Kind = PotionKind.None };
        }

        string trimmed = itemName.Trim();

        // 优先使用盛大官方 1.5 权威药水代谢引擎解析
        var profile = OfficialClientRulesEngine.PotionMetabolism.GetPotionProfile(trimmed);
        if (profile.InstantHp > 0 || profile.InstantMp > 0)
        {
            return new PotionMeta
            {
                Name = trimmed,
                Kind = PotionKind.InstantHpMp,
                TotalHp = profile.InstantHp,
                TotalMp = profile.InstantMp
            };
        }
        if (profile.HotHpTotal > 0)
        {
            return new PotionMeta
            {
                Name = trimmed,
                Kind = PotionKind.GradualHp,
                TotalHp = profile.HotHpTotal,
                TotalMp = 0
            };
        }
        if (profile.HotMpTotal > 0)
        {
            return new PotionMeta
            {
                Name = trimmed,
                Kind = PotionKind.GradualMp,
                TotalHp = 0,
                TotalMp = profile.HotMpTotal
            };
        }

        // 兜底模糊匹配
        if (trimmed.Contains("太阳水") || trimmed.Contains("疗伤药") || trimmed.Contains("万年雪霜"))
            return new PotionMeta { Name = trimmed, Kind = PotionKind.InstantHpMp, TotalHp = 50, TotalMp = 50 };
        if (trimmed.Contains("金创药") || trimmed.Contains("红"))
            return new PotionMeta { Name = trimmed, Kind = PotionKind.GradualHp, TotalHp = 50, TotalMp = 0 };
        if (trimmed.Contains("魔法药") || trimmed.Contains("蓝"))
            return new PotionMeta { Name = trimmed, Kind = PotionKind.GradualMp, TotalHp = 0, TotalMp = 80 };

        return new PotionMeta { Name = trimmed, Kind = PotionKind.None };
    }

    /// <summary>
    /// 玩家饮用药水处理逻辑：
    /// - 瞬回药水：立即返回 (instantHp, instantMp)，不加入缓回池；
    /// - 缓回药水：增量追加至 PendingHp / PendingMp 池中，返回 (0, 0)。
    /// </summary>
    public (int instantHp, int instantMp) Drink(string itemName, long nowMs)
    {
        var meta = ParsePotion(itemName);
        if (meta.Kind == PotionKind.None) return (0, 0);

        LastDrinkMs = nowMs;

        if (meta.IsInstant)
        {
            // 瞬回生效
            return (meta.TotalHp, meta.TotalMp);
        }

        // 缓回入池
        if (meta.TotalHp > 0)
        {
            PendingHp += meta.TotalHp;
        }
        if (meta.TotalMp > 0)
        {
            PendingMp += meta.TotalMp;
        }

        if (LastTickMs == 0)
        {
            LastTickMs = nowMs;
        }

        return (0, 0);
    }

    /// <summary>
    /// 定频心跳 Tick 结算：
    /// 检查是否到达 Tick 间隔（250ms），若到达则从缓冲池中提取增量。
    /// </summary>
    public bool ProcessTick(long nowMs, out int hpTick, out int mpTick, int hpStep = DefaultHpTickStep, int mpStep = DefaultMpTickStep)
    {
        hpTick = 0;
        mpTick = 0;

        if (PendingHp <= 0 && PendingMp <= 0)
        {
            return false;
        }

        if (LastTickMs == 0)
        {
            LastTickMs = nowMs;
            return false;
        }

        if (nowMs - LastTickMs < TickIntervalMs)
        {
            return false;
        }

        LastTickMs = nowMs;

        if (PendingHp > 0)
        {
            hpTick = Math.Min(PendingHp, Math.Max(1, hpStep));
            PendingHp -= hpTick;
        }

        if (PendingMp > 0)
        {
            mpTick = Math.Min(PendingMp, Math.Max(1, mpStep));
            PendingMp -= mpTick;
        }

        return hpTick > 0 || mpTick > 0;
    }

    /// <summary>
    /// 强制清空药水缓冲池 (例如角色阵亡或重置)
    /// </summary>
    public void Reset()
    {
        PendingHp = 0;
        PendingMp = 0;
        LastTickMs = 0;
        LastDrinkMs = 0;
    }
}
