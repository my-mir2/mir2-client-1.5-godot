using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Godot;
using Mir2.Server.Security;
using Mir2.Shared.Assets;
using Mir2.Shared.Packets;
using Mir2Client.Game;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 商业化上线就绪度三大技术支柱综合实证测试矩阵
/// 覆盖：
/// 1. 公网抗抖动、实体平滑插值与 64KB 大包切片重组
/// 2. 客户端资产完整性校验与增量补丁比对
/// 3. 服务端变速齿轮时钟防御与网关防泛洪熔断
/// </summary>
public class TestOnlineReadinessThreePillarsMatrix
{
    // =========================================================================
    // 支柱一：公网抗抖动与 64KB 大包切片重组矩阵
    // =========================================================================

    [Fact]
    public void Pillar1_BigPacketChunking_SplitAndReassemble_ZeroCorruption()
    {
        // 构造一个 128KB 的超大载荷 (模拟沙巴克千人战报或超大 NPC 任务树)
        byte[] originalPayload = new byte[131072];
        new Random(42).NextBytes(originalPayload);

        uint transferId = 88888;
        ushort originalMsgId = 1001;

        // 1. 切片为不超过 32KB 的分片包
        var chunks = BigPacketChunkEngine.SplitPayload(transferId, originalMsgId, originalPayload, chunkSize: 32768);
        Assert.Equal(4, chunks.Count);

        // 2. 验证每个切片的帧头元数据
        var assembler = new BigPacketChunkEngine.Assembler(timeoutMs: 10000);
        long now = 100000;

        (ushort OriginalMsgId, byte[] Payload)? result = null;

        // 模拟网络传输到达前 3 片
        for (int i = 0; i < 3; i++)
        {
            result = assembler.ProcessIncomingChunk(chunks[i], now);
            Assert.Null(result); // 尚未到齐，不应触发交付
        }

        // 第 4 片到达，触发原子交付
        result = assembler.ProcessIncomingChunk(chunks[3], now);
        Assert.NotNull(result);
        Assert.Equal(originalMsgId, result.Value.OriginalMsgId);
        Assert.Equal(originalPayload.Length, result.Value.Payload.Length);
        Assert.Equal(originalPayload, result.Value.Payload);
    }

    [Fact]
    public void Pillar1_BigPacketChunking_OutOrderChunks_AssembleSuccessfully()
    {
        // 模拟公网乱序到达 (切片顺序: 2, 0, 1)
        byte[] original = Encoding.UTF8.GetBytes("【盛大传奇1.5官方原版沙巴克攻城战报全景数据】" + new string('A', 70000));
        var chunks = BigPacketChunkEngine.SplitPayload(999, 2002, original, chunkSize: 30000);
        Assert.Equal(3, chunks.Count);

        var assembler = new BigPacketChunkEngine.Assembler();
        long now = 1000;

        // 乱序灌入切片
        Assert.Null(assembler.ProcessIncomingChunk(chunks[2], now));
        Assert.Null(assembler.ProcessIncomingChunk(chunks[0], now));
        var result = assembler.ProcessIncomingChunk(chunks[1], now);

        Assert.NotNull(result);
        Assert.Equal(original.Length, result.Value.Payload.Length);
        Assert.Equal(original, result.Value.Payload);
    }

    [Fact]
    public void Pillar1_EntityInterpolationBuffer_WeakNetworkJitter_SmoothTransition()
    {
        var buffer = new EntityInterpolationBuffer();
        buffer.InterpolationDelaySeconds = 0.080; // 80ms 延迟窗口

        // 模拟服务端下发的实体位置快照 (以 100ms 间隔下发)
        // 0.000s -> (100, 100)
        // 0.100s -> (120, 100)
        // 0.200s -> (140, 100)
        buffer.PushSnapshot(0.000, new Vector2(100, 100), 2, true, false);
        buffer.PushSnapshot(0.100, new Vector2(120, 100), 2, true, false);
        buffer.PushSnapshot(0.200, new Vector2(140, 100), 2, true, false);

        // 客户端在 0.230s 渲染：
        // TargetRenderTime = 0.230 - 0.080 = 0.150s (刚好落在 [0.100, 0.200] 的中点)
        var sampled = buffer.Sample(0.230);

        // 严格断言：X 坐标必须平滑插值到 130 (中点)，无突兀跳跃！
        Assert.Equal(130.0f, sampled.RenderPos.X, 0.1f);
        Assert.Equal(100.0f, sampled.RenderPos.Y, 0.1f);
        Assert.Equal(2, sampled.Direction);
        Assert.True(sampled.IsMoving);
    }

    // =========================================================================
    // 支柱二：客户端资产完整性校验与增量补丁比对矩阵
    // =========================================================================

    [Fact]
    public void Pillar2_AssetManifest_IntegrityVerification_DetectCorruptionAndMissing()
    {
        string tempDir = Path.Combine(Path.GetTempPath(), "mir2_asset_test_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string dataDir = Path.Combine(tempDir, "Data");
        Directory.CreateDirectory(dataDir);

        try
        {
            // 写入两个模拟资产文件
            string prgusePath = Path.Combine(dataDir, "Prguse.wil");
            string soundPath = Path.Combine(tempDir, "Wav");
            Directory.CreateDirectory(soundPath);
            string wavFile = Path.Combine(soundPath, "hit.wav");

            File.WriteAllBytes(prgusePath, new byte[] { 1, 2, 3, 4, 5 });
            File.WriteAllBytes(wavFile, new byte[] { 10, 20, 30 });

            // 1. 生成权威清单
            var manifest = AssetManifestEngine.GenerateManifest(tempDir, "1.5.0");
            Assert.Equal(2, manifest.Files.Count);

            // 2. 正常校验 -> 100% 成功
            var validResult = AssetManifestEngine.VerifyIntegrity(tempDir, manifest, deepHashCheck: true);
            Assert.True(validResult.IsFullyValid);
            Assert.Equal(2, validResult.ValidFiles.Count);

            // 3. 模拟文件被篡改 (向 Prguse.wil 注入恶意字节)
            File.WriteAllBytes(prgusePath, new byte[] { 9, 9, 9 });
            var corruptedResult = AssetManifestEngine.VerifyIntegrity(tempDir, manifest, deepHashCheck: true);
            Assert.False(corruptedResult.IsFullyValid);
            Assert.Contains(corruptedResult.CorruptedFiles, f => f.Contains("Prguse.wil"));

            // 4. 模拟文件丢失
            File.Delete(wavFile);
            var missingResult = AssetManifestEngine.VerifyIntegrity(tempDir, manifest, deepHashCheck: false);
            Assert.False(missingResult.IsFullyValid);
            Assert.Contains(missingResult.MissingFiles, f => f.Contains("hit.wav"));
        }
        finally
        {
            if (Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir, true);
            }
        }
    }

    [Fact]
    public void Pillar2_AssetManifest_DiffManifests_CalculateIncrementalDownloads()
    {
        var local = new AssetManifestEngine.AssetManifest
        {
            Version = "1.5.0",
            Files =
            [
                new AssetManifestEngine.AssetFileRecord { RelativePath = "Data/Prguse.wil", FileSizeBytes = 1000, Sha256Hex = "aaa" },
                new AssetManifestEngine.AssetFileRecord { RelativePath = "Data/Deprecated.wil", FileSizeBytes = 500, Sha256Hex = "bbb" }
            ]
        };

        var remote = new AssetManifestEngine.AssetManifest
        {
            Version = "1.5.1",
            Files =
            [
                new AssetManifestEngine.AssetFileRecord { RelativePath = "Data/Prguse.wil", FileSizeBytes = 1000, Sha256Hex = "aaa" }, // 无变化
                new AssetManifestEngine.AssetFileRecord { RelativePath = "Data/Items.wil", FileSizeBytes = 2500, Sha256Hex = "ccc" }    // 新增补丁
            ]
        };

        var diff = AssetManifestEngine.DiffManifests(local, remote);

        Assert.Single(diff.FilesToDownload);
        Assert.Equal("Data/Items.wil", diff.FilesToDownload[0].RelativePath);
        Assert.Equal(2500, diff.TotalDownloadBytes);
        Assert.Single(diff.FilesToDelete);
        Assert.Equal("Data/Deprecated.wil", diff.FilesToDelete[0]);
    }

    // =========================================================================
    // 支柱三：服务端变速齿轮时钟防御与网关防泛洪攻击矩阵
    // =========================================================================

    [Fact]
    public void Pillar3_SpeedHackWatchdog_NormalPlayerAllowed_SpeedHackerInterception()
    {
        var watchdog = new SpeedHackWatchdogEngine();
        int normalSid = 1001;
        int hackerSid = 2002;

        long serverStart = 10000;
        long clientStart = 10000;

        // 1. 正常玩家：每 500ms 进行一次动作 (2 秒内 4 次动作)
        for (int i = 0; i < 4; i++)
        {
            long sTick = serverStart + i * 500;
            long cTick = clientStart + i * 500;
            bool isHack = watchdog.VerifyActionAndDetectSpeedHack(normalSid, cTick, sTick, out _);
            Assert.False(isHack, "正常玩家不应被误判为加速外挂");
        }

        // 2. 变速齿轮外挂玩家：先建立初始时钟基准
        watchdog.VerifyActionAndDetectSpeedHack(hackerSid, clientStart, serverStart, out _);

        // 经过 2.5 秒物理时间，客户端时钟却前进了 4 秒 (4000ms / 2500ms = 1.6x)
        long hServerTick = serverStart + 2500;
        long hClientTick = clientStart + 4000;
        bool detected = watchdog.VerifyActionAndDetectSpeedHack(hackerSid, hClientTick, hServerTick, out var reason);

        Assert.True(detected, "变速齿轮加速外挂必须被看门狗瞬间捕捉拦截！");
        Assert.Contains("客户端系统时钟加速", reason);

        // 3. 处于惩罚期内再次发包，依然被直接拦截制动
        bool punished = watchdog.VerifyActionAndDetectSpeedHack(hackerSid, hClientTick + 100, hServerTick + 100, out var penaltyReason);
        Assert.True(punished);
        Assert.Contains("冷冻期", penaltyReason);
    }

    [Fact]
    public void Pillar3_GatewayDefense_RateLimitAndMagicStormBlacklist()
    {
        var defense = new GatewayDefenseEngine();
        defense.MaxConnectionsPerSecondPerIp = 3;
        defense.MaxMagicMismatchesBeforeDrop = 3;
        defense.TemporaryBlacklistSeconds = 10;

        string badIp = "192.168.1.110";
        long nowSec = 1700000000;

        // 1. 快速重连泛洪 (前 3 次放行，第 4 次触发拉黑)
        Assert.True(defense.ShouldAllowNewConnection(badIp, nowSec, out _));
        Assert.True(defense.ShouldAllowNewConnection(badIp, nowSec, out _));
        Assert.True(defense.ShouldAllowNewConnection(badIp, nowSec, out _));
        Assert.False(defense.ShouldAllowNewConnection(badIp, nowSec, out var denyReason));
        Assert.Contains("自动拉黑", denyReason);

        // 2. 黑名单惩罚期内拒绝连接
        Assert.False(defense.ShouldAllowNewConnection(badIp, nowSec + 5, out _));

        // 3. 魔数风暴攻击检测
        string stormIp = "10.0.0.50";
        Assert.False(defense.RecordMagicMismatchAndCheckDrop(stormIp, nowSec)); // 1
        Assert.False(defense.RecordMagicMismatchAndCheckDrop(stormIp, nowSec)); // 2
        bool drop = defense.RecordMagicMismatchAndCheckDrop(stormIp, nowSec);    // 3 (达到阈值)
        Assert.True(drop, "魔数风暴攻击必须立即断开连接");

        // 验证已自动被拉黑
        Assert.False(defense.ShouldAllowNewConnection(stormIp, nowSec, out _));
    }

    [Fact]
    public void Pillar1_EntityManager_InterpolationBufferIntegration()
    {
        var em = new EntityManager();
        int monUid = 5001;
        em.AddEntity(new EntityManager.EntityData
        {
            Uid = monUid,
            Name = "白野猪",
            Type = EntityManager.EntityType.Monster,
            TileX = 10,
            TileY = 10
        });

        // 模拟实体发生移动
        em.UpdateMove(monUid, 11, 10, 2);

        // 采样当前客户端时间的平滑坐标
        double clientTime = System.Environment.TickCount64 / 1000.0;
        var renderPos = em.GetEntityRenderPosition(monUid, clientTime);

        // 验证渲染坐标不为空且落在合理范围内
        Assert.True(renderPos.X > 0 && renderPos.Y > 0);
    }

    [Fact]
    public void Pillar3_PacketDispatcher_SpeedHack_MoveInterceptionAndRollback()
    {
        var mapMgr = new Mir2.Server.Maps.MapManager();
        var map = new Mir2.Server.Maps.GameMap(0, "比奇省", 100, 100);
        mapMgr.RegisterMap(map);
        var dispatcher = new Mir2.Server.Game.PacketDispatcher(mapMgr);

        var session = new Mir2.Server.Network.ClientSession(8001);
        session.CurrentMapId = 0;
        session.X = 10;
        session.Y = 10;
        session.Direction = 2;
        dispatcher.ActiveSessions[session.SessionId] = session;

        // 模拟正常走一步 (CM_WALK = 3011)
        dispatcher.Dispatch(session, "3011/11/10/2/!");
        Assert.Equal(11, session.X);

        // 连续疯狂瞬发移动 (步长合规但极短时间内高频发包，触发 SpeedHackWatchdog 拦截)
        for (int i = 0; i < 20; i++)
        {
            int targetX = (i % 2 == 0) ? 10 : 11;
            dispatcher.Dispatch(session, $"#3011/{targetX}/10/2/!");
        }

        // 验证被看门狗拦截制动，X 坐标无法继续向前非法窜动
        Assert.True(session.IsActionFrozen(System.Environment.TickCount64));
    }
}

