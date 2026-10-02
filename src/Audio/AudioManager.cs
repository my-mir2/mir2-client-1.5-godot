using System;
using System.Collections.Generic;
using System.IO;
using Godot;
using Mir2Client.Utils;

namespace Mir2Client.Audio;

/// <summary>
/// 盛大热血传奇 1.5 官方全量音频管理器 (纯 C# 实现)
/// 严格对齐官方 MIR_final_v3_p.exe sub_495794 (PlaySound) 与 sound_utf8.lst
/// </summary>
public partial class AudioManager : Node
{
    public static AudioManager? Instance { get; private set; }

    public const string WavDir = "resources/wav/";
    public const string SoundLstPath = "resources/wav/sound_utf8.lst";

    private readonly Dictionary<int, string> _soundMap = new();
    private readonly Dictionary<string, AudioStreamWav> _cache = new(StringComparer.OrdinalIgnoreCase);

    private AudioStreamPlayer? _bgmPlayer;
    private readonly List<AudioStreamPlayer> _sfxPlayers = new();
    private int _sfxIdx = 0;
    private const int SfxChannels = 8;
    private bool _muted = false;

    public bool Muted
    {
        get => _muted;
        set => _muted = value;
    }

    public override void _Ready()
    {
        Instance = this;

        _bgmPlayer = new AudioStreamPlayer
        {
            Bus = "Master",
            VolumeDb = -6.0f
        };
        AddChild(_bgmPlayer);

        for (int i = 0; i < SfxChannels; i++)
        {
            var p = new AudioStreamPlayer
            {
                Bus = "Master",
                VolumeDb = -1.0f
            };
            AddChild(p);
            _sfxPlayers.Add(p);
        }

        LoadSoundLst();
        GD.Print($"[AudioManager] 官方 1.5 C# 音频引擎已就绪，已加载 {_soundMap.Count} 条官方音效映射");
    }

    public override void _ExitTree()
    {
        if (Instance == this) Instance = null;
        if (_bgmPlayer != null && IsInstanceValid(_bgmPlayer))
        {
            _bgmPlayer.Stop();
        }
        foreach (var p in _sfxPlayers)
        {
            if (IsInstanceValid(p)) p.Stop();
        }
    }

    public void LoadSoundLst()
    {
        string fullPath = ResPath.Resolve(SoundLstPath);
        if (!File.Exists(fullPath)) return;

        _soundMap.Clear();
        var lines = File.ReadAllLines(fullPath);
        foreach (var line in lines)
        {
            var trimmed = line.Trim();
            if (string.IsNullOrEmpty(trimmed) || trimmed.StartsWith("#") || trimmed.StartsWith(";"))
                continue;

            // 官方格式: "1:\twav\\1.wav" 或 "1 \twav/1.wav"
            var parts = trimmed.Replace(':', ' ').Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2 && int.TryParse(parts[0], out int soundId))
            {
                string rawFile = parts[1].Replace('\\', '/');
                string fileName = Path.GetFileName(rawFile);
                _soundMap[soundId] = fileName;
            }
        }
    }

    public void PlaySoundId(int soundId)
    {
        if (_muted) return;
        if (_soundMap.TryGetValue(soundId, out var wavFile))
        {
            PlaySfx(wavFile);
        }
    }

    public void PlaySfx(string wavFileName)
    {
        if (_muted || _sfxPlayers.Count == 0) return;

        string fileName = Path.GetFileName(wavFileName.Replace('\\', '/'));
        string fullPath = ResPath.Resolve(Path.Combine(WavDir, fileName));
        if (!File.Exists(fullPath))
        {
            if (!fileName.EndsWith(".wav", StringComparison.OrdinalIgnoreCase))
            {
                fullPath = ResPath.Resolve(Path.Combine(WavDir, fileName + ".wav"));
                if (!File.Exists(fullPath)) return;
            }
            else return;
        }

        if (!_cache.TryGetValue(fullPath, out var stream))
        {
            stream = LoadWavStream(fullPath);
            if (stream != null) _cache[fullPath] = stream;
        }

        if (stream != null)
        {
            var player = _sfxPlayers[_sfxIdx];
            _sfxIdx = (_sfxIdx + 1) % _sfxPlayers.Count;
            player.Stream = stream;
            player.Play();
        }
    }

    public void StopBgm()
    {
        _bgmPlayer?.Stop();
    }

    private static AudioStreamWav? LoadWavStream(string path)
    {
        try
        {
            byte[] bytes = File.ReadAllBytes(path);
            var stream = new AudioStreamWav
            {
                Data = bytes,
                Format = AudioStreamWav.FormatEnum.Format16Bits,
                MixRate = 22050
            };
            return stream;
        }
        catch
        {
            return null;
        }
    }
}
