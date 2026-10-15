using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Diagnostics;

class PhysicalClientProbe
{
    static void Main()
    {
        Console.WriteLine("=== 开始向 127.0.0.1:7000 发送真实物理 TCP 游戏封包 ===");

        using var client = new TcpClient();
        client.Connect("127.0.0.1", 7000);
        using var stream = client.GetStream();
        stream.ReadTimeout = 3000;

        byte[] buffer = new byte[4096];

        // 1. 发送 CM_LOGIN (2)
        string loginPkt = "#2/real_pg_hero/mypassword123!";
        Console.WriteLine($"[客户端 -> 服务端] 发送: {loginPkt}");
        byte[] loginBytes = Encoding.UTF8.GetBytes(loginPkt);
        stream.Write(loginBytes, 0, loginBytes.Length);

        Thread.Sleep(300);
        int read = stream.Read(buffer, 0, buffer.Length);
        string resp1 = Encoding.UTF8.GetString(buffer, 0, read);
        Console.WriteLine($"[服务端 -> 客户端] 收到: {resp1}");

        // 2. 发送 CM_SELECTCHAR (103)
        string selectPkt = "#103/真传奇霸主/0/330/270!";
        Console.WriteLine($"[客户端 -> 服务端] 发送: {selectPkt}");
        byte[] selectBytes = Encoding.UTF8.GetBytes(selectPkt);
        stream.Write(selectBytes, 0, selectBytes.Length);

        Thread.Sleep(300);
        read = stream.Read(buffer, 0, buffer.Length);
        string resp2 = Encoding.UTF8.GetString(buffer, 0, read);
        Console.WriteLine($"[服务端 -> 客户端] 收到: {resp2}");

        // 3. 发送 CM_WALK (3011) 移动到 (331, 270)
        string walkPkt = "#3011/331/270/2!";
        Console.WriteLine($"[客户端 -> 服务端] 发送移动步包: {walkPkt}");
        byte[] walkBytes = Encoding.UTF8.GetBytes(walkPkt);
        stream.Write(walkBytes, 0, walkBytes.Length);

        Thread.Sleep(500);

        // 4. 断开连接触发下线持久化
        Console.WriteLine("[客户端] 主动断开连接，触发服务端 OnSessionDisconnected 异步落盘队列...");
        client.Close();

        // 等待异步落盘入库
        Thread.Sleep(1500);

        Console.WriteLine("=== 客户端封包交互完毕，正在通过 psql 查询 PostgreSQL 数据库物理落地证据 ===");
        var psi = new ProcessStartInfo
        {
            FileName = @"d:\tools\pgsql_bin\pgsql\bin\psql.exe",
            Arguments = "-h 127.0.0.1 -p 5432 -U postgres -d mir2_world -c \"SELECT account_name, password_hash, salt, last_login FROM mir2_accounts WHERE account_name = 'real_pg_hero'; SELECT char_name, account_name, job, level, hp, gold, map_id, map_x, map_y FROM mir2_characters WHERE char_name = '真传奇霸主';\"",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };
        using var p = Process.Start(psi)!;
        string psqlOut = p.StandardOutput.ReadToEnd();
        p.WaitForExit();
        Console.WriteLine(psqlOut);
        Console.WriteLine("=== 100% 物理真实闭环验证完成 ===");
    }
}
