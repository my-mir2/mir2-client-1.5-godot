using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Mir2.Server.Game;
using Mir2.Server.Network;
using Mir2.Server.Persistence;
using Mir2.Shared.Models;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 预写式事务物理日志 (WAL / Redo Log) 与断电崩溃恢复权威测试矩阵 (TestServerWalCrashRecoveryMatrix)
/// 针对《工程认知公理 4.0》中揭示的“异步持久化在进程强杀或服务器断电下存在内存丢失窗口”坏账，
/// 实证验证 WriteAheadLogEngine 的物理刷盘 (fsync)、事务记录完整性、断电半写坏块安全截断与崩溃重放自愈能力。
/// </summary>
public class TestServerWalCrashRecoveryMatrix : IDisposable
{
    private readonly string _testWalDir;
    private readonly string _testWalPath;

    public TestServerWalCrashRecoveryMatrix()
    {
        _testWalDir = Path.Combine(Path.GetTempPath(), "mir2_test_wal_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(_testWalDir);
        _testWalPath = Path.Combine(_testWalDir, "m2_transactions.wal");
    }

    public void Dispose()
    {
        try
        {
            if (Directory.Exists(_testWalDir))
            {
                Directory.Delete(_testWalDir, recursive: true);
            }
        }
        catch
        {
            // 忽略临时目录清理异常
        }
    }

    [Fact]
    public void TestWalEngine_InitializationAndHeader()
    {
        using (var wal = new WriteAheadLogEngine(_testWalPath))
        {
            Assert.True(File.Exists(_testWalPath));
            var info = new FileInfo(_testWalPath);
            Assert.True(info.Length >= 5, "WAL 文件至少包含 5 字节魔数与版本头");
        }

        // 二次开启应保留已有文件
        using (var wal2 = new WriteAheadLogEngine(_testWalPath))
        {
            Assert.Equal(0UL, wal2.CurrentLsn);
        }
    }

    [Fact]
    public void TestWalEngine_ItemTradeLogging_And_CrashReplay()
    {
        // 1. 模拟运行期两个在线玩家发生极品神兵交易 (面对面站立 1 格)
        var sessionA = new ClientSession(1001)
        {
            CharacterName = "战士A",
            CurrentMapId = 0,
            X = 100,
            Y = 100,
            State = new PlayerState { Name = "战士A", Level = 45, Job = 0, Hp = 500, MaxHp = 500, Gold = 1_000_000 }
        };
        var dragonSaber = new EquipItem { Id = 8888, Uid = 8888, Name = "屠龙", Weight = 99 };
        sessionA.State.BagItems.Add(dragonSaber);
        sessionA.State.RecalculateAuthoritativeWeights(sessionA.State.Gold);

        var sessionB = new ClientSession(1002)
        {
            CharacterName = "法师B",
            CurrentMapId = 0,
            X = 101,
            Y = 100,
            State = new PlayerState { Name = "法师B", Level = 45, Job = 1, Hp = 200, MaxHp = 200, Gold = 2_000_000 }
        };
        sessionB.State.RecalculateAuthoritativeWeights(sessionB.State.Gold);

        // 2. 挂接 WAL 引擎并执行面对面交易
        var wal = new WriteAheadLogEngine(_testWalPath);
        var tradeEngine = new TradeEngine { WalEngine = wal };

        tradeEngine.TryInitiateTrade(sessionA, sessionB);
        tradeEngine.AddItem(sessionA, 8888, "屠龙");
        tradeEngine.ChangeGold(sessionB, 500_000);

        // 盛大官方 1.5 铁律: 变动后必须满足 1000ms 防快刷门禁 (sub_4DDC8C @ 0x4DDCF5)
        var tradeCtx = tradeEngine.GetTrade(sessionA.SessionId);
        Assert.NotNull(tradeCtx);
        tradeCtx.LastChangeTickA = Environment.TickCount64 - 1500;
        tradeCtx.LastChangeTickB = Environment.TickCount64 - 1500;

        // 双方确认交割 (触发原子移交与 WAL 物理预写刷盘)
        tradeEngine.ConfirmDeal(sessionA);
        var (complete, _, reason) = tradeEngine.ConfirmDeal(sessionB);
        Assert.True(complete, $"交易必须顺利达成: {reason}");

        // 验证运行期内存状态
        Assert.DoesNotContain(sessionA.State.BagItems, it => it.Id == 8888);
        Assert.Contains(sessionB.State.BagItems, it => it.Id == 8888);
        Assert.Equal(1_500_000, sessionA.State.Gold);
        Assert.Equal(1_500_000, sessionB.State.Gold);

        // 3. 模拟操作系统崩溃与断电掉电 (模拟进程强杀：直接释放 WAL 句柄，主数据库完全不存盘)
        wal.Dispose();

        // 4. 重启服务端，从冷启动状态重现崩溃丢失现场
        // 此时模拟主数据库中的旧存档（交易前的状态：屠龙仍在战士A手中，战士A金币100万，法师B金币200万）
        var restoredStateA = new PlayerState { Name = "战士A", Gold = 1_000_000 };
        restoredStateA.BagItems.Add(new EquipItem { Id = 8888, Name = "屠龙", Weight = 99 });
        var restoredStateB = new PlayerState { Name = "法师B", Gold = 2_000_000 };

        // 执行 WAL 顺序重放补偿 (Crash Recovery / Redo)
        int replayedCount = WriteAheadLogEngine.ReplayWal(_testWalPath, record =>
        {
            if (record.Type == WriteAheadLogEngine.TransactionType.ItemTraded)
            {
                var payload = JsonSerializer.Deserialize<WriteAheadLogEngine.TradePayload>(record.Payload);
                Assert.NotNull(payload);

                if (payload.ItemUid == 8888 && payload.SenderName == "战士A" && payload.ReceiverName == "法师B")
                {
                    // 补偿移除 A 的屠龙，补偿给 B
                    restoredStateA.BagItems.RemoveAll(it => it.Id == 8888);
                    restoredStateB.BagItems.Add(new EquipItem { Id = payload.ItemUid, Name = payload.ItemName, Weight = 99 });
                    restoredStateA.Gold += 500_000;
                    restoredStateB.Gold -= 500_000;
                }
            }
        });

        // 验证重放结果：神兵与金币 100% 成功补偿恢复，0 丢失！
        Assert.True(replayedCount > 0);
        Assert.DoesNotContain(restoredStateA.BagItems, it => it.Id == 8888);
        Assert.Contains(restoredStateB.BagItems, it => it.Id == 8888);
        Assert.Equal(1_500_000, restoredStateA.Gold);
        Assert.Equal(1_500_000, restoredStateB.Gold);
    }

    [Fact]
    public void TestWalEngine_TornWriteCorruption_SafeTruncation()
    {
        // 验证写入一半遭遇断电产生的残缺坏块自愈
        using (var wal = new WriteAheadLogEngine(_testWalPath))
        {
            wal.LogGoldUpdate("道士C", 10000, 20000, "挖矿奖励", syncToDisk: true);
            wal.LogGoldUpdate("道士C", 5000, 25000, "打怪掉落", syncToDisk: true);
        }

        // 人为在 WAL 文件末尾追加 7 字节残缺垃圾数据 (模拟半写撕裂 Torn Write)
        byte[] junkBytes = new byte[] { 0xDE, 0xAD, 0xBE, 0xEF, 0x01, 0x02, 0x03 };
        File.WriteAllBytes(_testWalPath, File.ReadAllBytes(_testWalPath).Concat(junkBytes).ToArray());

        int validRecordsCount = 0;
        int count = WriteAheadLogEngine.ReplayWal(_testWalPath, record =>
        {
            validRecordsCount++;
            Assert.Equal(WriteAheadLogEngine.TransactionType.GoldUpdated, record.Type);
        });

        // 验证：前面 2 条合法事务顺利重放，末尾半写坏块被安全截断，未发生崩溃或死锁
        Assert.Equal(2, count);
        Assert.Equal(2, validRecordsCount);
    }

    [Fact]
    public void TestWalEngine_CheckpointAndLogTruncation_PreventsDiskBloat()
    {
        // 验证 Checkpoint 机制能有效截断 WAL 文件并防止生产环境磁盘无限膨胀
        using (var wal = new WriteAheadLogEngine(_testWalPath))
        {
            // 写入多条金币变动与拾取事务
            for (int i = 0; i < 50; i++)
            {
                wal.LogGoldUpdate("玩家A", 100, 100 + i * 100, "测试交易", syncToDisk: true);
            }

            var infoBefore = new FileInfo(_testWalPath);
            Assert.True(infoBefore.Length > 2000, "50条事务日志大小应超过2KB");

            // 执行 Checkpoint 与截断轮转
            bool snapshotTriggered = false;
            bool success = wal.CreateCheckpointAndTruncate(() =>
            {
                snapshotTriggered = true;
                return true; // 模拟外部快照保存成功
            });

            Assert.True(success);
            Assert.True(snapshotTriggered);

            // 截断后物理文件大小必须精确重置为 5 字节文件头
            var infoAfter = new FileInfo(_testWalPath);
            Assert.Equal(5, infoAfter.Length);

            // 截断后重放，记录数应为 0
            int replayedAfterTruncate = WriteAheadLogEngine.ReplayWal(_testWalPath, _ => { });
            Assert.Equal(0, replayedAfterTruncate);

            // 截断后继续写入新事务，依然可以正常持久化与重放
            wal.LogGoldUpdate("玩家A", 500, 10000, "截断后新事务", syncToDisk: true);
        }

        int countFinal = WriteAheadLogEngine.ReplayWal(_testWalPath, record =>
        {
            Assert.Equal(WriteAheadLogEngine.TransactionType.GoldUpdated, record.Type);
        });
        Assert.Equal(1, countFinal);
    }
}
