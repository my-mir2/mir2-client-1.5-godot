using System;
using System.Buffers.Binary;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

// 1. 连接 127.0.0.1:7000
using var client = new TcpClient();
await client.ConnectAsync("127.0.0.1", 7000);
var stream = client.GetStream();
Console.WriteLine("[PROBE] 成功连接至 Mir2.Server TCP-Pack 网关 127.0.0.1:7000");

// 2. 构造 HPSocket TCP-Pack 二进制包: Magic 0x594D, MsgId 2001 (CM_QUERYVERSION), Payload: "0/0/0/0/0/"
ushort magic = 0x594D;
ushort msgId = 2001; // CM_QUERYVERSION
byte[] payloadBytes = Encoding.UTF8.GetBytes("0/0/0/0/0/");
byte[] sendBuf = new byte[8 + payloadBytes.Length];
BinaryPrimitives.WriteUInt16LittleEndian(sendBuf.AsSpan(0, 2), magic);
BinaryPrimitives.WriteUInt16LittleEndian(sendBuf.AsSpan(2, 2), msgId);
BinaryPrimitives.WriteUInt16LittleEndian(sendBuf.AsSpan(4, 2), (ushort)payloadBytes.Length);
BinaryPrimitives.WriteUInt16LittleEndian(sendBuf.AsSpan(6, 2), 1);
payloadBytes.CopyTo(sendBuf, 8);

await stream.WriteAsync(sendBuf);
await stream.FlushAsync();
Console.WriteLine($"[PROBE] 发送 HPSocket TCP-Pack (CM_QUERYVERSION, 8B 帧头 + {payloadBytes.Length}B 载荷)");

// 3. 接收下行响应 Pack
byte[] recvBuf = new byte[4096];
int n = await stream.ReadAsync(recvBuf);
if (n >= 8)
{
    ushort respMagic = BinaryPrimitives.ReadUInt16LittleEndian(recvBuf.AsSpan(0, 2));
    ushort respMsgId = BinaryPrimitives.ReadUInt16LittleEndian(recvBuf.AsSpan(2, 2));
    ushort respLen = BinaryPrimitives.ReadUInt16LittleEndian(recvBuf.AsSpan(4, 2));
    ushort respSeq = BinaryPrimitives.ReadUInt16LittleEndian(recvBuf.AsSpan(6, 2));
    Console.WriteLine($"[PROBE] 收到服务端回包 -> Magic: 0x{respMagic:X4}, MsgId: {respMsgId} (SM_PASSOK=500), Length: {respLen}, Seq: {respSeq}");
    if (respMagic == 0x594D && respMsgId == 500)
    {
        Console.WriteLine("[PROBE] SUCCESS: HPSocket TCP-Pack 物理线路收发全闭环实证通过！");
    }
}
