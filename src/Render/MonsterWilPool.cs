using System;
using System.Collections.Concurrent;
using System.IO;
using Mir2Client.Utils;

namespace Mir2Client.Render;

/// <summary>
/// 盛大官方 1.5 怪物贴图动态资产池 (Mon1.wil ~ Mon18.wil)
/// 严格 1:1 对齐官方 Delphi sub_495EBC @ 0x495EBC..0x49611B:
/// 依据 Appr / 10 路由加载 Mon1 ~ Mon18 资源，支持高效懒加载与全局缓存
/// </summary>
public sealed class MonsterWilPool : IDisposable
{
    private static MonsterWilPool? _instance;
    public static MonsterWilPool Instance => _instance ??= new MonsterWilPool();

    private readonly ConcurrentDictionary<int, WilLoader> _monLoaders = new();
    private bool _disposed = false;

    /// <summary>
    /// 根据官方 IDA sub_495EBC 算法根据 Appr 获取对应的 WilLoader
    /// </summary>
    public WilLoader? GetLoaderForAppr(int appr)
    {
        if (_disposed) return null;

        // 官方 IDA sub_495EBC switch ( (unsigned __int8)(appr / 10) )
        int monIndex = GetMonFileIndex(appr);
        return GetLoader(monIndex);
    }

    /// <summary>
    /// 官方 sub_495EBC 映射表:
    /// case 0 -> Mon1 (index 1)
    /// case 1 -> Mon2 (index 2)
    /// ...
    /// case 17 -> Mon18 (index 18)
    /// case 90 (0x5A) -> 特殊扩展
    /// </summary>
    public static int GetMonFileIndex(int appr)
    {
        int v1 = appr / 10;
        if (v1 is >= 0 and <= 17)
        {
            return v1 + 1; // 1..18
        }
        if (v1 == 90) // 0x5A
        {
            return 6; // 官方特例映射
        }
        return 1; // 默认保底 Mon1
    }

    /// <summary>
    /// 盛大官方 1.5 怪物基础贴图帧偏移算法 (严格 1:1 对齐 IDA Delphi sub_496120)
    /// </summary>
    public static int CalcMonsterBaseOffset(int appr)
    {
        int v8 = appr % 10;
        int v1 = appr / 10;

        if (v1 <= 6)
        {
            if (v1 == 6) return 440 * v8;
            return v1 switch
            {
                0 => 280 * v8,
                1 => 230 * v8,
                2 or 3 => 360 * v8,
                4 => (v8 == 1 ? 600 : 360 * v8),
                5 => 430 * v8,
                _ => 0
            };
        }

        if (v1 < 14)
        {
            int v2 = v1 - 7;
            if (v2 < 6) // v1 in 7..12
            {
                return 360 * v8;
            }
            // v1 == 13
            return v8 switch
            {
                1 => 360,
                2 => 440,
                3 => 550,
                _ => 360 * v8
            };
        }

        int v5 = v1 - 14;
        if (v5 < 3) // v1 in 14..16
        {
            return 360 * v8;
        }

        int v6 = v5 - 3; // v1 - 17
        if (v6 == 0) // v1 == 17 (神兽等 Mon18 怪物)
        {
            return 350 * v8;
        }

        if (v6 == 73) // v1 == 90 (0x5A)
        {
            return v8 switch
            {
                0 => 80,
                1 => 168,
                2 => 184,
                3 => 200,
                _ => 0
            };
        }

        return 360 * v8;
    }

    /// <summary>
    /// 懒加载指定编号的 Mon{monIndex}.wil
    /// </summary>
    public WilLoader? GetLoader(int monIndex)
    {
        if (_disposed) return null;
        if (monIndex < 1 || monIndex > 18) monIndex = 1;

        return _monLoaders.GetOrAdd(monIndex, idx =>
        {
            var loader = new WilLoader();
            string wilPath = ResPath.Resolve($"resources/wil/Data/Mon{idx}.wil");
            string wixPath = ResPath.Resolve($"resources/wil/Data/Mon{idx}.WIX");

            if (!File.Exists(wixPath))
            {
                wixPath = ResPath.Resolve($"resources/wil/Data/Mon{idx}.wix");
            }

            if (File.Exists(wilPath))
            {
                loader.Load(wilPath, File.Exists(wixPath) ? wixPath : null);
            }
            return loader;
        });
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        foreach (var loader in _monLoaders.Values)
        {
            loader.Dispose();
        }
        _monLoaders.Clear();
    }
}
