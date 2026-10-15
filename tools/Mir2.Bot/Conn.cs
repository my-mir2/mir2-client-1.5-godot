using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Text;

namespace Mir2.Bot;

/// <summary>服务端下行包: 8 字节头 + 16 字节 (recog,param,tag,series) + UTF-8 extra。</summary>
public sealed record Pkt(long AtMs, int Id, int Recog, int Param, int Tag, int Series, string Extra)
{
    public override string ToString() => $"#{Id}/{Recog}/{Param}/{Tag}/{Series}/{Extra.Replace('\t', '|')}";
}

/// <summary>真实 TCP 连接。上行为文本载荷 (与服务端 Dispatch 文本入口一致)，下行按线协议解包。</summary>
public sealed class Conn : IDisposable
{
    private const ushort Magic = 0x594D;
    private readonly TcpClient _tcp = new() { NoDelay = true };
    private NetworkStream _stream = null!;
    private ushort _seq;
    private readonly System.Diagnostics.Stopwatch _clock = System.Diagnostics.Stopwatch.StartNew();
    public ConcurrentQueue<Pkt> Inbox { get; } = new();
    public volatile bool Closed;
    public long NowMs => _clock.ElapsedMilliseconds;

    public async Task ConnectAsync(string host, int port)
    {
        await _tcp.ConnectAsync(host, port);
        _stream = _tcp.GetStream();
        _ = Task.Run(ReadLoop);
    }

    public void Send(int msgId, string payload = "")
    {
        byte[] body = payload.Length == 0 ? Array.Empty<byte>() : Encoding.UTF8.GetBytes(payload);
        byte[] buf = new byte[8 + body.Length];
        BitConverter.TryWriteBytes(buf.AsSpan(0, 2), Magic);
        BitConverter.TryWriteBytes(buf.AsSpan(2, 2), (ushort)msgId);
        BitConverter.TryWriteBytes(buf.AsSpan(4, 2), (ushort)body.Length);
        BitConverter.TryWriteBytes(buf.AsSpan(6, 2), ++_seq);
        body.CopyTo(buf, 8);
        _stream.Write(buf);
        _stream.Flush();
    }

    private async Task ReadLoop()
    {
        var acc = new List<byte>(8192);
        var tmp = new byte[8192];
        try
        {
            while (true)
            {
                int n = await _stream.ReadAsync(tmp);
                if (n <= 0) break;
                acc.AddRange(tmp.AsSpan(0, n).ToArray());
                while (acc.Count >= 8)
                {
                    int len = acc[4] | (acc[5] << 8);
                    if (acc.Count < 8 + len) break;
                    int id = acc[2] | (acc[3] << 8);
                    var p = acc.GetRange(8, len).ToArray();
                    acc.RemoveRange(0, 8 + len);
                    int Rd(int o) => p.Length >= o + 4 ? BitConverter.ToInt32(p, o) : 0;
                    string extra = p.Length > 16 ? Encoding.UTF8.GetString(p, 16, p.Length - 16) : (p.Length > 0 && p.Length < 16 ? Encoding.UTF8.GetString(p) : "");
                    Inbox.Enqueue(new Pkt(NowMs, id, Rd(0), Rd(4), Rd(8), Rd(12), extra));
                }
            }
        }
        catch { /* 连接断开 */ }
        finally { Closed = true; }
    }

    public void Dispose() { try { _tcp.Close(); } catch { } }
}
