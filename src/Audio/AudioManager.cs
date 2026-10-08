using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Godot;
using Mir2Client.Utils;

namespace Mir2Client.Audio;

/// <summary>
/// 盛大热血传奇 1.5 官方全量音频管理器 (纯 C# 工业级实现)
/// 严格对齐官方 MIR_final_v3_p.exe sub_495794 (PlaySound) 与 sound_utf8.lst
/// 支持 RIFF/WAVE 标准原生解析 (精准提取 44100/22050/11025 Hz 采样率与 8/16位 PCM，彻底消除变调与爆音坏账)
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
    private const int SfxChannels = 16; // 扩充至 16 通道防丢音
    private bool _muted = false;

    public bool Muted
    {
        get => _muted;
        set => _muted = value;
    }

    private bool _footStepToggle = false;

    /// <summary>
    /// 盛大官方 1.5 原版音效 ID 常量 (严格对齐 Delphi SoundUtil.pas)
    /// </summary>
    public static class SoundId
    {
        // 脚步声 (泥土地)
        public const int WalkGroundL = 1;
        public const int WalkGroundR = 2;
        public const int RunGroundL = 3;
        public const int RunGroundR = 4;

        // UI 与 物品交互
        public const int RockDoorOpen = 100;
        public const int MeltStone = 101;
        public const int IntroTheme = 102;
        public const int NormButtonClick = 103;
        public const int RockButtonClick = 104;
        public const int GlassButtonClick = 105;
        public const int Money = 106;
        public const int EatDrug = 107;
        public const int ClickDrug = 108;
        public const int SpaceMoveOut = 109;
        public const int SpaceMoveIn = 110;
        public const int LevelUp = 255;     // 官方 1.5 角色升级音效 (IDA ProcessServerMessage case 44 @ 0x46A18D)

        public const int ClickWeapon = 111;
        public const int ClickArmor = 112;
        public const int ClickRing = 113;
        public const int ClickArmring = 114;
        public const int ClickNecklace = 115;
        public const int ClickHelmet = 116;
        public const int ClickGrobes = 117;
        public const int ItemClick = 118;

        // 战士技能音效
        public const int YedoMan = 130;
        public const int YedoWoman = 131;
        public const int Longhit = 132;
        public const int Widehit = 133;
        public const int RushL = 134;
        public const int RushR = 135;
        public const int FirehitReady = 136;
        public const int Firehit = 137;

        // 角色受击与死亡音效
        public const int ManStruck = 138;
        public const int WomStruck = 139;
        public const int ManDie = 144;
        public const int WomDie = 145;
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
        var lines = File.ReadAllLines(fullPath, Encoding.UTF8);
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
        else
        {
            // 回退直接按 ID 查找
            PlaySfx($"{soundId}.wav");
        }
    }

    /// <summary>
    /// 官方走步行走脚步声 (泥土地左右脚交替 1, 2)
    /// </summary>
    public void PlayWalkStepSound()
    {
        _footStepToggle = !_footStepToggle;
        PlaySoundId(_footStepToggle ? SoundId.WalkGroundL : SoundId.WalkGroundR);
    }

    /// <summary>
    /// 官方跑步奔跑脚步声 (泥土地左右脚交替 3, 4)
    /// </summary>
    public void PlayRunStepSound()
    {
        _footStepToggle = !_footStepToggle;
        PlaySoundId(_footStepToggle ? SoundId.RunGroundL : SoundId.RunGroundR);
    }

    /// <summary>
    /// 严格对齐 Delphi SoundUtil.pas:ItemClickSound 物品点击/穿戴音效
    /// </summary>
    public void PlayItemClickSound(int stdMode)
    {
        int sid = stdMode switch
        {
            0 or 31 => SoundId.ClickDrug,           // 108
            5 or 6 => SoundId.ClickWeapon,          // 111
            10 or 11 => SoundId.ClickArmor,         // 112
            22 or 23 => SoundId.ClickRing,          // 113
            24 or 26 => SoundId.ClickArmring,       // 114
            19 or 20 or 21 => SoundId.ClickNecklace,// 115
            15 => SoundId.ClickHelmet,              // 116
            _ => SoundId.ItemClick                  // 118
        };
        PlaySoundId(sid);
    }

    /// <summary>
    /// 严格对齐 Delphi SoundUtil.pas:ItemUseSound 物品使用音效
    /// </summary>
    public void PlayItemUseSound(int stdMode)
    {
        if (stdMode is 1 or 2)
        {
            PlaySoundId(SoundId.EatDrug); // 107
        }
        else if (stdMode == 0)
        {
            PlaySoundId(SoundId.ClickDrug); // 108
        }
    }

    /// <summary>
    /// 官方通用按钮点击音效 (103)
    /// </summary>
    public void PlayButtonClick()
    {
        PlaySoundId(SoundId.NormButtonClick);
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

    public struct WavRiffInfo
    {
        public int SampleRate;
        public int BitsPerSample;
        public int NumChannels;
        public byte[] PcmData;
    }

    /// <summary>
    /// 纯托管 RIFF/WAVE 字节流解析器，0 依赖 Godot 引擎原生运行时，支持毫秒级脱机单测
    /// </summary>
    public static WavRiffInfo? ParseWavRiff(byte[] fileBytes)
    {
        if (fileBytes == null || fileBytes.Length < 44) return null;

        if (Encoding.ASCII.GetString(fileBytes, 0, 4) != "RIFF" ||
            Encoding.ASCII.GetString(fileBytes, 8, 4) != "WAVE")
        {
            return null;
        }

        int offset = 12;
        int numChannels = 1;
        int sampleRate = 22050;
        int bitsPerSample = 16;
        byte[]? pcmData = null;

        while (offset + 8 <= fileBytes.Length)
        {
            string chunkId = Encoding.ASCII.GetString(fileBytes, offset, 4);
            int chunkSize = BitConverter.ToInt32(fileBytes, offset + 4);
            offset += 8;

            if (chunkSize < 0 || offset + chunkSize > fileBytes.Length)
            {
                chunkSize = fileBytes.Length - offset;
            }

            if (chunkId == "fmt " && chunkSize >= 16)
            {
                numChannels = BitConverter.ToInt16(fileBytes, offset + 2);
                sampleRate = BitConverter.ToInt32(fileBytes, offset + 4);
                bitsPerSample = BitConverter.ToInt16(fileBytes, offset + 14);
            }
            else if (chunkId == "data")
            {
                pcmData = new byte[chunkSize];
                Buffer.BlockCopy(fileBytes, offset, pcmData, 0, chunkSize);
                break;
            }

            offset += chunkSize;
            if ((chunkSize & 1) != 0) offset++;
        }

        if (pcmData == null) return null;

        return new WavRiffInfo
        {
            SampleRate = sampleRate > 0 ? sampleRate : 22050,
            BitsPerSample = bitsPerSample,
            NumChannels = numChannels,
            PcmData = pcmData
        };
    }

    /// <summary>
    /// 标准 RIFF/WAVE 格式解析器，精准提取采样率、声道数、位数与纯 PCM 字节流
    /// 彻底消除直接传文件头导致的爆音以及采样率错配导致的音调扭曲
    /// </summary>
    public static AudioStreamWav? LoadWavStream(string path)
    {
        try
        {
            byte[] fileBytes = File.ReadAllBytes(path);
            var info = ParseWavRiff(fileBytes);
            if (info == null) return null;

            var stream = new AudioStreamWav
            {
                Data = info.Value.PcmData,
                Format = info.Value.BitsPerSample == 8 ? AudioStreamWav.FormatEnum.Format8Bits : AudioStreamWav.FormatEnum.Format16Bits,
                MixRate = info.Value.SampleRate,
                Stereo = info.Value.NumChannels >= 2
            };

            return stream;
        }
        catch (Exception ex)
        {
            GD.PrintErr($"[AudioManager] 解析 WAV 文件失败 ({path}): {ex.Message}");
            return null;
        }
    }
}

