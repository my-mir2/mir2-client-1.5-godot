using System;

namespace Mir2Client.Game;

/// <summary>
/// 经典升级经验表（对齐 Rust LevelConfig g_dwOldNeedExps，M2Share.pas L2164-2215）。
/// NeedExp[level] = 该级升下一级所需经验；0 级占位。
/// </summary>
public static class ExpTable
{
    public static readonly long[] NeedExp = new long[]
    {
        0,
        100, 200, 300, 400, 600, 900, 1200, 1700, 2500,
        6000, 8000, 10000, 15000, 30000, 40000, 50000, 70000, 100000, 120000,
        140000, 250000, 300000, 350000, 400000, 500000, 700000, 1000000, 1400000, 1800000,
        2000000, 2400000, 2800000, 3200000, 3600000, 4000000, 4800000, 5600000, 8200000, 9000000,
        12000000, 16000000, 30000000, 50000000, 80000000, 120000000, 480000000, 1000000000, 3000000000, 3500000000,
        4000000000,
    };

    public static long ExpForLevel(int level) =>
        (level >= 0 && level < NeedExp.Length) ? NeedExp[level] : NeedExp[NeedExp.Length - 1];

    public static double ExpProgress(int level, long curExp)
    {
        long need = ExpForLevel(level);
        if (need <= 0) return 0.0;
        return Math.Clamp((double)curExp / need, 0.0, 1.0);
    }
}
