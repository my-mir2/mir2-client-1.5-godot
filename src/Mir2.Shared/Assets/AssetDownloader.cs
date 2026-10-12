using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Mir2.Shared.Assets;

/// <summary>
/// 盛大官方 1.5 客户端资产增量分块补丁下载器 (CDN / HTTP Patch Downloader)
/// 专为生产发布与启动器设计，负责拉取 AssetManifestEngine.DiffManifests 产出的差异文件。
/// 具备：断点续传准备、多文件流水线下载、SHA256 落盘二次校验、下载进度实时回调。
/// </summary>
public sealed class AssetDownloader : IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly bool _disposeClient;

    public AssetDownloader(HttpClient? httpClient = null)
    {
        if (httpClient != null)
        {
            _httpClient = httpClient;
            _disposeClient = false;
        }
        else
        {
            _httpClient = new HttpClient();
            _disposeClient = true;
        }
    }

    /// <summary>
    /// 执行增量补丁文件批量下载与安全落盘
    /// </summary>
    /// <param name="baseUrl">CDN 或更新服务器的基础 URL (例如: http://cdn.mir2.com/patch/1.5/)</param>
    /// <param name="localDestinationDir">客户端本地根目录</param>
    /// <param name="filesToDownload">待下载文件清单 (从 DiffManifests 获取)</param>
    /// <param name="progressCallback">进度上报回调 (completedFiles, totalFiles, downloadedBytes, totalBytes)</param>
    /// <param name="cancellationToken">取消令牌</param>
    public async Task<bool> DownloadPatchesAsync(
        string baseUrl,
        string localDestinationDir,
        IReadOnlyList<AssetManifestEngine.AssetFileRecord> filesToDownload,
        Action<int, int, long, long>? progressCallback = null,
        CancellationToken cancellationToken = default)
    {
        if (filesToDownload == null || filesToDownload.Count == 0)
        {
            progressCallback?.Invoke(0, 0, 0, 0);
            return true;
        }

        baseUrl = baseUrl.TrimEnd('/') + "/";
        long totalBytes = 0;
        foreach (var file in filesToDownload)
        {
            totalBytes += file.FileSizeBytes;
        }

        long downloadedBytes = 0;
        int completedFiles = 0;

        foreach (var file in filesToDownload)
        {
            cancellationToken.ThrowIfCancellationRequested();

            string relativeUrl = file.RelativePath.Replace('\\', '/');
            string fullUrl = baseUrl + relativeUrl;
            string targetPath = Path.Combine(localDestinationDir, file.RelativePath.Replace('/', Path.DirectorySeparatorChar));

            string? dir = Path.GetDirectoryName(targetPath);
            if (!string.IsNullOrEmpty(dir))
            {
                Directory.CreateDirectory(dir);
            }

            // 执行物理下载
            using var response = await _httpClient.GetAsync(fullUrl, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            string tempFile = targetPath + ".downloading";
            try
            {
                await using (var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken))
                await using (var fileStream = new FileStream(tempFile, FileMode.Create, FileAccess.Write, FileShare.None, 81920, true))
                {
                    byte[] buffer = new byte[81920];
                    int read;
                    while ((read = await contentStream.ReadAsync(buffer.AsMemory(0, buffer.Length), cancellationToken)) > 0)
                    {
                        await fileStream.WriteAsync(buffer.AsMemory(0, read), cancellationToken);
                        downloadedBytes += read;
                        progressCallback?.Invoke(completedFiles, filesToDownload.Count, downloadedBytes, totalBytes);
                    }
                }

                // 校验 SHA256，防止网络劫持或下载损坏
                if (!string.IsNullOrEmpty(file.Sha256Hex))
                {
                    string hash = AssetManifestEngine.ComputeFileSha256(tempFile);
                    if (!string.Equals(hash, file.Sha256Hex, StringComparison.OrdinalIgnoreCase))
                    {
                        File.Delete(tempFile);
                        return false;
                    }
                }

                if (File.Exists(targetPath))
                {
                    File.Delete(targetPath);
                }

                File.Move(tempFile, targetPath);
                completedFiles++;
                progressCallback?.Invoke(completedFiles, filesToDownload.Count, downloadedBytes, totalBytes);
            }
            finally
            {
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                }
            }
        }

        return true;
    }

    public void Dispose()
    {
        if (_disposeClient)
        {
            _httpClient.Dispose();
        }
    }
}
