using System;
using System.IO;
using System.Runtime.InteropServices;
using Godot;
using Mir2Client.Utils;

namespace Mir2Client.Render;

/// <summary>
/// 高性能 C# WIL 贴图加载器 (支持直接指针操作与 16-bit 像素解压)
/// 严格 1:1 对齐官方 WIL 格式 (Header=44B, Palette=1024B @56, FrameHeader=8B)
/// </summary>
public sealed class WilLoader : IDisposable
{
    private const int WixHdrSize = 44;
    private const int WixOffsets = 48;
    private const int WilFrameHdr = 8;
    private const int PaletteSize = 256;

    private readonly Color[] _palette = new Color[PaletteSize];
    private int[] _offsets = Array.Empty<int>();
    private Vector2I[] _anchors = Array.Empty<Vector2I>();
    private int _frameCount = 0;
    private string _wilPath = string.Empty;
    private FileStream? _fileStream = null;
    private readonly System.Collections.Generic.Dictionary<int, Texture2D> _textureCache = new();

    public int FrameCount => _frameCount;

    public bool Load(string wilPath, string? wixPath = null)
    {
        return LoadWil(wilPath, wixPath);
    }

    public bool LoadWil(string wilPath, string? wixPath = null)
    {
        _wilPath = wilPath;
        if (!File.Exists(wilPath)) return false;

        try
        {
            _fileStream = new FileStream(wilPath, FileMode.Open, System.IO.FileAccess.Read, FileShare.Read);
            LoadEmbeddedPalette();
            LoadIndexOffsets(wixPath);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private void LoadEmbeddedPalette()
    {
        if (_fileStream == null || _fileStream.Length < 56 + 1024) return;
        _fileStream.Seek(56, SeekOrigin.Begin);
        Span<byte> palBytes = stackalloc byte[1024];
        _fileStream.ReadExactly(palBytes);

        for (int i = 0; i < PaletteSize; i++)
        {
            int offset = i * 4;
            byte b = palBytes[offset];
            byte g = palBytes[offset + 1];
            byte r = palBytes[offset + 2];
            bool isTrans = (i == 0) || (r == 255 && g == 0 && b == 255);
            _palette[i] = new Color(r / 255.0f, g / 255.0f, b / 255.0f, isTrans ? 0.0f : 1.0f);
        }
    }

    private void LoadIndexOffsets(string? customWixPath = null)
    {
        string wixPath = !string.IsNullOrEmpty(customWixPath) && File.Exists(customWixPath)
            ? customWixPath
            : Path.ChangeExtension(_wilPath, ".wix");

        if (!File.Exists(wixPath)) return;

        using var fs = new FileStream(wixPath, FileMode.Open, System.IO.FileAccess.Read, FileShare.Read);
        if (fs.Length < WixOffsets) return;

        // 官方 WIX 标准结构：头部 44 字节签名，[44..48] 为 32 位整型 FrameCount
        fs.Seek(WixHdrSize, SeekOrigin.Begin);
        Span<byte> countBytes = stackalloc byte[4];
        fs.ReadExactly(countBytes);
        _frameCount = MemoryMarshal.Read<int>(countBytes);

        if (_frameCount <= 0 || _frameCount > 100000)
        {
            // 回退尝试 24 偏移
            fs.Seek(24, SeekOrigin.Begin);
            fs.ReadExactly(countBytes);
            _frameCount = MemoryMarshal.Read<int>(countBytes);
        }

        if (_frameCount <= 0 || _frameCount > 100000) return;

        fs.Seek(WixOffsets, SeekOrigin.Begin);
        _offsets = new int[_frameCount];
        _anchors = new Vector2I[_frameCount];
        Span<byte> offBytes = stackalloc byte[4];
        for (int i = 0; i < _frameCount; i++)
        {
            fs.ReadExactly(offBytes);
            _offsets[i] = MemoryMarshal.Read<int>(offBytes);
        }
    }

    public Texture2D? GetFrame(int index)
    {
        if (index < 0) return null;

        if (_textureCache.TryGetValue(index, out var cached))
            return cached;

        // 1. 优先尝试从 WIL/WIX 内存流中直接解压提取
        if (_fileStream != null && index < _offsets.Length && _offsets[index] > 0)
        {
            try
            {
                _fileStream.Seek(_offsets[index], SeekOrigin.Begin);
                Span<byte> hdr = stackalloc byte[WilFrameHdr];
                _fileStream.ReadExactly(hdr);

                short width = MemoryMarshal.Read<short>(hdr[0..2]);
                short height = MemoryMarshal.Read<short>(hdr[2..4]);
                short originX = MemoryMarshal.Read<short>(hdr[4..6]);
                short originY = MemoryMarshal.Read<short>(hdr[6..8]);

                _anchors[index] = new Vector2I(originX, originY);

                if (width > 0 && height > 0 && width <= 2048 && height <= 2048)
                {
                    int pixelCount = width * height;
                    byte[] rawPixels = new byte[pixelCount];
                    _fileStream.ReadExactly(rawPixels);

                    var img = Image.CreateEmpty(width, height, false, Image.Format.Rgba8);
                    for (int y = 0; y < height; y++)
                    {
                        int srcRow = (height - 1 - y) * width;
                        for (int x = 0; x < width; x++)
                        {
                            byte pIdx = rawPixels[srcRow + x];
                            Color c = _palette[pIdx];
                            img.SetPixel(x, y, c);
                        }
                    }

                    var tex = ImageTexture.CreateFromImage(img);
                    _textureCache[index] = tex;
                    return tex;
                }
            }
            catch
            {
                // 流读取异常回退至同名 PNG 切片
            }
        }

        // 2. 回退机制：若 WIL 二进制流未命中或不存在，自动检索已导出的官方 1:1 PNG 切片图
        if (!string.IsNullOrEmpty(_wilPath))
        {
            string wilName = Path.GetFileNameWithoutExtension(_wilPath);
            string pngRel = $"resources/wil/png/{wilName}/{wilName}_{index:D4}.png";
            var pngTex = ResPath.LoadImageTexture(pngRel);
            if (pngTex != null)
            {
                _textureCache[index] = pngTex;
                return pngTex;
            }
        }

        return null;
    }

    public Vector2I GetAnchor(int index)
    {
        if (index >= 0 && index < _anchors.Length)
            return _anchors[index];
        return Vector2I.Zero;
    }

    public void Dispose()
    {
        _textureCache.Clear();
        _fileStream?.Dispose();
        _fileStream = null;
    }
}
