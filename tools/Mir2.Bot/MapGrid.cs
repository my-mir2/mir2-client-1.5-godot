namespace Mir2.Bot;

/// <summary>地图碰撞 (与真实客户端同一份 .map 资源、同一阻挡规则)，用于机器人寻路。</summary>
public sealed class MapGrid
{
    public readonly int W, H; private readonly byte[] _cells;
    private MapGrid(int w, int h, byte[] cells) { W = w; H = h; _cells = cells; }

    public static MapGrid? Load(string path)
    {
        if (!File.Exists(path)) return null;
        var d = File.ReadAllBytes(path);
        if (d.Length < 52) return null;
        int w = d[0] | (d[1] << 8), h = d[2] | (d[3] << 8);
        if (d.Length < 52 + w * h * 12) return null;
        return new MapGrid(w, h, d[52..]);
    }

    public bool Walkable(int x, int y)
    {
        if (x < 0 || y < 0 || x >= W || y >= H) return false;
        int o = (x * H + y) * 12;
        int floor = _cells[o] | (_cells[o + 1] << 8), obj = _cells[o + 4] | (_cells[o + 5] << 8);
        return ((floor | obj) & 0x8000) == 0;
    }

    public static readonly (int dx, int dy)[] Dirs = { (0, -1), (1, -1), (1, 0), (1, 1), (0, 1), (-1, 1), (-1, 0), (-1, -1) };
    public static int DirOf(int dx, int dy) { for (int i = 0; i < 8; i++) if (Dirs[i] == (Math.Sign(dx), Math.Sign(dy))) return i; return 4; }
    public static int Cheb(int ax, int ay, int bx, int by) => Math.Max(Math.Abs(ax - bx), Math.Abs(ay - by));

    /// <summary>A* 找到从 (sx,sy) 走到任意"与 (tx,ty) 切比雪夫距离 ≤ stopDist"的格子的路径；blocked 为其它实体占用格。</summary>
    public List<(int x, int y)>? Path(int sx, int sy, int tx, int ty, int stopDist, HashSet<(int, int)> blocked, int maxNodes = 30000)
    {
        if (Cheb(sx, sy, tx, ty) <= stopDist) return new();
        var open = new PriorityQueue<(int x, int y), int>();
        var g = new Dictionary<(int, int), int> { [(sx, sy)] = 0 };
        var from = new Dictionary<(int, int), (int, int)>();
        open.Enqueue((sx, sy), 0);
        int nodes = 0;
        while (open.TryDequeue(out var cur, out _) && nodes++ < maxNodes)
        {
            if (Cheb(cur.x, cur.y, tx, ty) <= stopDist)
            {
                var path = new List<(int, int)> { cur };
                while (from.TryGetValue(path[^1], out var pr) && pr != (sx, sy)) path.Add(pr);
                path.Reverse();
                return path;
            }
            foreach (var (dx, dy) in Dirs)
            {
                int nx = cur.x + dx, ny = cur.y + dy;
                if (!Walkable(nx, ny) || blocked.Contains((nx, ny))) continue;
                int ng = g[cur] + 1;
                if (g.TryGetValue((nx, ny), out var og) && og <= ng) continue;
                g[(nx, ny)] = ng; from[(nx, ny)] = cur;
                open.Enqueue((nx, ny), ng + Cheb(nx, ny, tx, ty));
            }
        }
        return null;
    }
}
