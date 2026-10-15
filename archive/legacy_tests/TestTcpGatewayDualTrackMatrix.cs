using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Mir2.Server.Network;
using Mir2.Shared.Network.HPSocket;
using Mir2.Shared.Protocols.Modern;
using Xunit;

namespace Mir2.Tests;

/// <summary>
/// 盛大官方 1.5 现代高并发 TCP 网关 HPSocket TCP-Pack 二进制分帧解包验证矩阵
/// 彻底废除旧世代 '#' 与 '!' 文本字符串定界符
/// 涵盖: HPSocket TCP-Pack 规范封包、多包粘包拆分、跨分片半包重组、魔数滑动对齐容错
/// </summary>
public class TestTcpGatewayDualTrackMatrix
{
    private static int GetRandomPort() => Mir2Client.Tests.TestNetworkPortHelper.GetNextPort();

    private static byte[] BuildPack(ushort msgId, ReadOnlySpan<byte> payload = default, ushort seq = 0)
    {
        byte[] packet = new byte[BinaryProtocol.HeaderSize + payload.Length];
        var span = packet.AsSpan();
        System.Buffers.Binary.BinaryPrimitives.WriteUInt16LittleEndian(span[0..2], BinaryProtocol.DefaultMagic);
        System.Buffers.Binary.BinaryPrimitives.WriteUInt16LittleEndian(span[2..4], msgId);
        System.Buffers.Binary.BinaryPrimitives.WriteUInt16LittleEndian(span[4..6], (ushort)payload.Length);
        System.Buffers.Binary.BinaryPrimitives.WriteUInt16LittleEndian(span[6..8], seq);
        if (!payload.IsEmpty)
        {
            payload.CopyTo(span[BinaryProtocol.HeaderSize..]);
        }
        return packet;
    }

    [Fact]
    public async Task Test_TcpGateway_PackStream_NoiseAndCorruptedStream_Recovery()
    {
        int port = GetRandomPort();
        var gateway = new TcpGateway(port);
        var received = new List<(ushort msgId, string text)>();

        gateway.BinaryPacketReceivedHandler = (_, header, payload) =>
        {
            lock (received)
            {
                received.Add((header.MsgId, Encoding.UTF8.GetString(payload)));
            }
        };

        gateway.Start();
        try
        {
            using var client = new TcpClient();
            await client.ConnectAsync(IPAddress.Loopback, port);
            var stream = client.GetStream();

            // 发送模拟数据: 前导 4 字节非法噪点 + 两个合法 HPSocket Pack
            byte[] noise = new byte[] { 0xAA, 0xBB, 0xCC, 0xDD };
            byte[] p1 = BuildPack(100, Encoding.UTF8.GetBytes("User1"), 1);
            byte[] p2 = BuildPack(200, Encoding.UTF8.GetBytes("User2"), 2);

            using var ms = new MemoryStream();
            ms.Write(noise);
            ms.Write(p1);
            ms.Write(p2);

            byte[] fullStream = ms.ToArray();
            await stream.WriteAsync(fullStream);
            await stream.FlushAsync();

            await Task.Delay(150);

            lock (received)
            {
                // PackDecoder 能够滑动跳过非法前导噪点，正确捕获魔数并解出 2 个合法 Pack
                Assert.Equal(2, received.Count);
                Assert.Equal(100, received[0].msgId);
                Assert.Equal("User1", received[0].text);
                Assert.Equal(200, received[1].msgId);
                Assert.Equal("User2", received[1].text);
            }
        }
        finally
        {
            gateway.Stop();
        }
    }

    [Fact]
    public async Task Test_TcpGateway_BinaryStream_FramingAndUnsticking()
    {
        int port = GetRandomPort();
        var gateway = new TcpGateway(port);
        var receivedBinary = new List<(ushort msgId, byte[] payload)>();

        gateway.BinaryPacketReceivedHandler = (_, header, payload) =>
        {
            lock (receivedBinary)
            {
                receivedBinary.Add((header.MsgId, payload));
            }
        };

        gateway.Start();
        try
        {
            using var client = new TcpClient();
            await client.ConnectAsync(IPAddress.Loopback, port);
            var stream = client.GetStream();

            // 构造两个二进制包粘包
            byte[] p1 = new byte[] { 1, 2, 3, 4 };
            byte[] p2 = new byte[] { 5, 6, 7, 8, 9, 10 };

            byte[] pack1 = BuildPack((ushort)BinaryProtocol.MsgType.IntentMove, p1, 1);
            byte[] pack2 = BuildPack((ushort)BinaryProtocol.MsgType.IntentAttack, p2, 2);

            using var ms = new MemoryStream();
            ms.Write(pack1);
            ms.Write(pack2);

            byte[] allBytes = ms.ToArray();

            // 切分为两半发送模拟半包与网络分片
            int half = allBytes.Length / 2;
            await stream.WriteAsync(allBytes.AsMemory(0, half));
            await stream.FlushAsync();

            await Task.Delay(50);

            await stream.WriteAsync(allBytes.AsMemory(half));
            await stream.FlushAsync();

            await Task.Delay(150);

            lock (receivedBinary)
            {
                Assert.Equal(2, receivedBinary.Count);
                Assert.Equal((ushort)BinaryProtocol.MsgType.IntentMove, receivedBinary[0].msgId);
                Assert.Equal(p1, receivedBinary[0].payload);
                Assert.Equal((ushort)BinaryProtocol.MsgType.IntentAttack, receivedBinary[1].msgId);
                Assert.Equal(p2, receivedBinary[1].payload);
            }
        }
        finally
        {
            gateway.Stop();
        }
    }

    [Fact]
    public async Task Test_TcpGateway_MultiPackBatch_HighConcurrencyDelivery()
    {
        int port = GetRandomPort();
        var gateway = new TcpGateway(port);
        var receivedIds = new List<ushort>();

        gateway.BinaryPacketReceivedHandler = (_, header, _) =>
        {
            lock (receivedIds)
            {
                receivedIds.Add(header.MsgId);
            }
        };

        gateway.Start();
        try
        {
            using var client = new TcpClient();
            await client.ConnectAsync(IPAddress.Loopback, port);
            var stream = client.GetStream();

            // 连续发送 10 个轻量级 Pack
            using var ms = new MemoryStream();
            for (ushort i = 1; i <= 10; i++)
            {
                byte[] pack = BuildPack(i, ReadOnlySpan<byte>.Empty, i);
                ms.Write(pack);
            }

            byte[] all = ms.ToArray();
            await stream.WriteAsync(all);
            await stream.FlushAsync();

            await Task.Delay(150);

            lock (receivedIds)
            {
                Assert.Equal(10, receivedIds.Count);
                for (int i = 0; i < 10; i++)
                {
                    Assert.Equal((ushort)(i + 1), receivedIds[i]);
                }
            }
        }
        finally
        {
            gateway.Stop();
        }
    }
}
