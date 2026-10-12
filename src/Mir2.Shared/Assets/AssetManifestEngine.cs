using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Mir2.Shared.Assets;

/// <summary>
/// 盛大官方 1.5 客户端资产完整性校验与增量补丁比对引擎
/// 负责扫描与验证 Data/*.wil, Data/*.wix, Wav/*.wav, Map/*.map 资产，
/// 防止商业化上线中由于客户端文件损坏、丢失导致的粉红方块（Missing Texture）、闪退与缺声音问题。
/// </summary>
public static class AssetManifestEngine
{
    public enum AssetCategory : byte
    {
        Unknown = 0,
        WilGraphic = 1,
        WixIndex = 2,
        WavAudio = 3,
        GameMap = 4,
        Configuration = 5
    }

    public sealed class AssetFileRecord
    {
        public string RelativePath { get; set; } = string.Empty;
        public long FileSizeBytes { get; set; }
        public string Sha256Hex { get; set; } = string.Empty;
        public AssetCategory Category { get; set; }
        public long LastModifiedUnixSec { get; set; }
    }

    public sealed class AssetManifest
    {
        public string Version { get; set; } = "1.5.0";
        public long GeneratedUnixSec { get; set; }
        public List<AssetFileRecord> Files { get; set; } = [];
    }

    public sealed class VerificationResult
    {
        public bool IsFullyValid => MissingFiles.Count == 0 && CorruptedFiles.Count == 0;
        public List<string> MissingFiles { get; } = [];
        public List<string> CorruptedFiles { get; } = [];
        public List<string> ValidFiles { get; } = [];
        public long TotalVerifiedBytes { get; set; }
    }

    public sealed class PatchDiffResult
    {
        public List<AssetFileRecord> FilesToDownload { get; } = [];
        public List<string> FilesToDelete { get; } = [];
        public long TotalDownloadBytes { get; set; }
    }

    public static AssetCategory DetectCategory(string relativePath)
    {
        string ext = Path.GetExtension(relativePath).ToLowerInvariant();
        return ext switch
        {
            ".wil" => AssetCategory.WilGraphic,
            ".wix" => AssetCategory.WixIndex,
            ".wav" => AssetCategory.WavAudio,
            ".map" => AssetCategory.GameMap,
            ".ini" or ".json" => AssetCategory.Configuration,
            _ => AssetCategory.Unknown
        };
    }

    /// <summary>
    /// 计算指定文件的 SHA256 16进制哈希
    /// </summary>
    public static string ComputeFileSha256(string absoluteFilePath)
    {
        if (!File.Exists(absoluteFilePath)) return string.Empty;
        using var stream = File.OpenRead(absoluteFilePath);
        using var sha256 = SHA256.Create();
        byte[] hash = sha256.ComputeHash(stream);
        return Convert.ToHexString(hash).ToLowerInvariant();
    }

    /// <summary>
    /// 扫描客户端目录，生成完整的资产清单
    /// </summary>
    public static AssetManifest GenerateManifest(string clientRootDir, string version = "1.5.0")
    {
        var manifest = new AssetManifest
        {
            Version = version,
            GeneratedUnixSec = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        };

        if (!Directory.Exists(clientRootDir))
            return manifest;

        string[] searchPatterns = ["*.wil", "*.wix", "*.wav", "*.map"];
        foreach (var pattern in searchPatterns)
        {
            foreach (var filePath in Directory.EnumerateFiles(clientRootDir, pattern, SearchOption.AllDirectories))
            {
                string relPath = Path.GetRelativePath(clientRootDir, filePath).Replace('\\', '/');
                var fi = new FileInfo(filePath);

                manifest.Files.Add(new AssetFileRecord
                {
                    RelativePath = relPath,
                    FileSizeBytes = fi.Length,
                    Sha256Hex = ComputeFileSha256(filePath),
                    Category = DetectCategory(relPath),
                    LastModifiedUnixSec = new DateTimeOffset(fi.LastWriteTimeUtc).ToUnixTimeSeconds()
                });
            }
        }

        return manifest;
    }

    /// <summary>
    /// 校验本地目录与权威清单的一致性
    /// </summary>
    /// <param name="clientRootDir">客户端根目录</param>
    /// <param name="manifest">权威基准清单</param>
    /// <param name="deepHashCheck">是否执行耗时的全量 SHA256 穿透检查 (false 则仅检查文件存在与大小)</param>
    public static VerificationResult VerifyIntegrity(string clientRootDir, AssetManifest manifest, bool deepHashCheck = true)
    {
        var result = new VerificationResult();

        foreach (var record in manifest.Files)
        {
            string localPath = Path.Combine(clientRootDir, record.RelativePath.Replace('/', Path.DirectorySeparatorChar));

            if (!File.Exists(localPath))
            {
                result.MissingFiles.Add(record.RelativePath);
                continue;
            }

            var fi = new FileInfo(localPath);
            if (fi.Length != record.FileSizeBytes)
            {
                result.CorruptedFiles.Add(record.RelativePath);
                continue;
            }

            if (deepHashCheck && !string.IsNullOrEmpty(record.Sha256Hex))
            {
                string actualHash = ComputeFileSha256(localPath);
                if (!string.Equals(actualHash, record.Sha256Hex, StringComparison.OrdinalIgnoreCase))
                {
                    result.CorruptedFiles.Add(record.RelativePath);
                    continue;
                }
            }

            result.ValidFiles.Add(record.RelativePath);
            result.TotalVerifiedBytes += fi.Length;
        }

        return result;
    }

    /// <summary>
    /// 对比本地旧版本与服务端新版本清单，生成增量下载差异包列表
    /// </summary>
    public static PatchDiffResult DiffManifests(AssetManifest localManifest, AssetManifest remoteManifest)
    {
        var diff = new PatchDiffResult();
        var localMap = new Dictionary<string, AssetFileRecord>(StringComparer.OrdinalIgnoreCase);

        foreach (var loc in localManifest.Files)
        {
            localMap[loc.RelativePath] = loc;
        }

        var remotePaths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var rem in remoteManifest.Files)
        {
            remotePaths.Add(rem.RelativePath);

            if (!localMap.TryGetValue(rem.RelativePath, out var locFile))
            {
                // 本地不存在 -> 需新增下载
                diff.FilesToDownload.Add(rem);
                diff.TotalDownloadBytes += rem.FileSizeBytes;
            }
            else if (locFile.FileSizeBytes != rem.FileSizeBytes ||
                     (!string.IsNullOrEmpty(rem.Sha256Hex) && !string.Equals(locFile.Sha256Hex, rem.Sha256Hex, StringComparison.OrdinalIgnoreCase)))
            {
                // 本地存在但大小或哈希不匹配 -> 需覆盖更新
                diff.FilesToDownload.Add(rem);
                diff.TotalDownloadBytes += rem.FileSizeBytes;
            }
        }

        // 检查本地多余的已废弃文件
        foreach (var loc in localManifest.Files)
        {
            if (!remotePaths.Contains(loc.RelativePath))
            {
                diff.FilesToDelete.Add(loc.RelativePath);
            }
        }

        return diff;
    }
}
