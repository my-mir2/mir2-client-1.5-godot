namespace Mir2.Shared.Models;

/// <summary>
/// 盛大官方 1.5 商店与仓库条目模型 (IDA sub_48F020 & sub_46DB28 权威实证)
/// 跨端领域模型，供服务端下行协议打包与客户端 UI 视图共享
/// </summary>
public struct ShopItemEntry
{
    public string Name;
    public int Weight;
    public int Price;
    /// <summary>托管列表专用: 当前持久(IDA sub_46DB28: 源字段 +0x30 ushort / 除数 → 条目 +14h)</summary>
    public int Dura;
    /// <summary>托管列表专用: 最大持久(IDA sub_46DB28: 源字段 +0x32 ushort / 除数 → 条目 +18h)</summary>
    public int MaxDura;

    public ShopItemEntry(string name, int weight, int price)
    {
        Name = name;
        Weight = weight;
        Price = price;
        Dura = 0;
        MaxDura = 0;
    }

    public static ShopItemEntry Stored(string name, int dura, int maxDura)
    {
        var e = new ShopItemEntry(name, 0, 0);
        e.Dura = dura;
        e.MaxDura = maxDura;
        return e;
    }
}
