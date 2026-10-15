// ============================================================================
// Mir2 服务端 - 大规模断线重连风暴削峰与平滑交错出栈测试矩阵
// (TestReconnectionStormPacerMatrix)
// 实证验证《工程认知公理 4.0》中揭示的“外网突发抖动导致数百人同时重连引发网络发送队列瞬时高水位雪崩”坏账的修复效果：
// 1. 突发 100+ 会话同时重连，平滑限制每 Tick 最大并发，彻底削平出栈波峰
// 2. 跨多个 Tick 均匀交错出栈，0 任务丢失
// 3. 单个会话异常隔离与鲁棒性容错
// ============================================================================

using System;
using System.Collections.Generic;
using Mir2.Server.Network;
using Xunit;

namespace Mir2Client.Tests;

public class TestReconnectionStormPacerMatrix
{
    [Fact]
    public void Test_ReconnectionStorm_PacedAcrossTicks()
    {
        // 设置单 Tick 最多放行 30 个重连初始化
        var pacer = new ReconnectionPacerEngine(maxConcurrentLogonsPerTick: 30);

        int totalExecuted = 0;
        var executedOrder = new List<int>();

        // 模拟 100 个在线玩家在同一毫秒遭遇网络波动并同时发起重连
        for (int i = 0; i < 100; i++)
        {
            int sessionId = 1000 + i;
            pacer.EnqueueOrExecute(() =>
            {
                totalExecuted++;
                executedOrder.Add(sessionId);
            });
        }

        Assert.Equal(100, pacer.PendingQueueCount);
        Assert.Equal(0, totalExecuted);

        // Tick 1: 严格放行 30 个，剩余 70 个排队
        int tick1Processed = pacer.ProcessTick();
        Assert.Equal(30, tick1Processed);
        Assert.Equal(30, totalExecuted);
        Assert.Equal(70, pacer.PendingQueueCount);

        // Tick 2: 再次平滑放行 30 个，剩余 40 个排队
        int tick2Processed = pacer.ProcessTick();
        Assert.Equal(30, tick2Processed);
        Assert.Equal(60, totalExecuted);
        Assert.Equal(40, pacer.PendingQueueCount);

        // Tick 3: 再次放行 30 个，剩余 10 个排队
        int tick3Processed = pacer.ProcessTick();
        Assert.Equal(30, tick3Processed);
        Assert.Equal(90, totalExecuted);
        Assert.Equal(10, pacer.PendingQueueCount);

        // Tick 4: 放行最后 10 个，队列清空
        int tick4Processed = pacer.ProcessTick();
        Assert.Equal(10, tick4Processed);
        Assert.Equal(100, totalExecuted);
        Assert.Equal(0, pacer.PendingQueueCount);

        // 验证执行顺序严格遵守 FIFO，0 会话丢失
        Assert.Equal(100, executedOrder.Count);
        Assert.Equal(1000, executedOrder[0]);
        Assert.Equal(1099, executedOrder[99]);
    }

    [Fact]
    public void Test_ProcessTick_ExceptionResilience()
    {
        var pacer = new ReconnectionPacerEngine(maxConcurrentLogonsPerTick: 10);
        int successCount = 0;

        pacer.EnqueueOrExecute(() => successCount++);
        pacer.EnqueueOrExecute(() => throw new InvalidOperationException("模拟某个破损数据包的解析异常"));
        pacer.EnqueueOrExecute(() => successCount++);

        int processed = pacer.ProcessTick();

        // 异常被安全隔离，不阻断后续正常会话的处理
        Assert.Equal(3, processed);
        Assert.Equal(2, successCount);
        Assert.Equal(0, pacer.PendingQueueCount);
    }
}
