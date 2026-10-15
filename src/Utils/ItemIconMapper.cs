using System;
using Mir2.Shared.Data;

namespace Mir2Client.Utils;

/// <summary>
/// 盛大官方 1.5 物品图元映射器 (严格 1:1 对齐官方 Items.wil 切片物理真值)
/// 证据来源: 
/// - 盛大官方客户端 Items.wil 原版 518 帧物理切片
/// - 盛大官方 M2Server 数据库 StdItems.DB 原版 377 条物品 Looks 映射表 (OfficialStdItemsRegistry)
/// </summary>
public static class ItemIconMapper
{
    public static string GetIconFile(string itemName)
    {
        if (string.IsNullOrEmpty(itemName)) return string.Empty;

        // 1. 唯一最高真值：100% 优先检索官方 377 条 StdItems.DB 物理 Looks 数据库
        int looks = OfficialStdItemsRegistry.ResolveLooks(itemName, -1);
        if (looks >= 0)
        {
            return $"Items_{looks:D4}.png";
        }

        // 2. 肉与食物 (StdItems: Meat=1, Chicken=13)
        if (itemName.Contains("鸡肉") || itemName.Contains("生鸡")) return "Items_0013.png";
        if (itemName.Contains("肉")) return "Items_0001.png";

        // 3. 金币与硬通货 (StdItems: Gold=112~116, GoldBar=117, GoldBrick=121, GoldChest=122)
        if (itemName.Contains("金箱")) return "Items_0122.png";
        if (itemName.Contains("金砖")) return "Items_0121.png";
        if (itemName.Contains("金条")) return "Items_0117.png";
        if (itemName.Contains("金币") || itemName.Contains("金钱")) return "Items_0114.png";

        // 4. 药水家族兜底 (严格 1:1 盛大官方 1.5 原版 Items.wil 物理切片真值)
        // 强效特药 (经典大网格圆胖坛，Looks 28/29)
        if (itemName.Contains("强效金创药") || itemName.Contains("金创药(大") || itemName.Contains("超级金创药")) return "Items_0028.png";
        if (itemName.Contains("强效魔法药") || itemName.Contains("魔法药(大") || itemName.Contains("超级魔法药")) return "Items_0029.png";
        
        // 中量药水 (经典带刻度直筒方药瓶，Looks 400/396)
        if (itemName.Contains("金创药(中") || itemName.Contains("中量金创药")) return "Items_0400.png";
        if (itemName.Contains("魔法药(中") || itemName.Contains("中量魔法药")) return "Items_0396.png";
        
        // 小量药水 (经典带木塞下圆玻璃药瓶，Looks 398/394)
        if (itemName.Contains("金创药")) return "Items_0398.png";
        if (itemName.Contains("魔法药")) return "Items_0394.png";
        
        // 瞬间回复特药 (太阳水=16 金黄瓶, 强效太阳水=312 圆坛, 万年雪霜=260 长须人参, 疗伤药=412 药臼小碗)
        if (itemName.Contains("强效太阳水")) return "Items_0312.png";
        if (itemName.Contains("万年雪霜")) return "Items_0260.png";
        if (itemName.Contains("疗伤药")) return "Items_0412.png";
        if (itemName.Contains("太阳水")) return "Items_0016.png";

        // 道士毒药与药粉 (Looks 17/18)
        if (itemName.Contains("黄色药粉") || itemName.Contains("黄药粉") || itemName.Contains("灰毒")) return "Items_0018.png";
        if (itemName.Contains("灰色药粉") || itemName.Contains("绿毒") || itemName.Contains("毒药")) return "Items_0017.png";

        // 5. 卷轴类道具 (Items 5, 7, 8)
        if (itemName.Contains("随机")) return "Items_0005.png";      // 随机传送卷(经典盘香)
        if (itemName.Contains("地牢")) return "Items_0007.png";      // 地牢逃脱卷
        if (itemName.Contains("回城")) return "Items_0008.png";      // 回城卷
        if (itemName.Contains("行会回城")) return "Items_0006.png";  // 行会回城树枝

        // 5. 照明物 (Items 130/131)
        if (itemName.Contains("蜡烛")) return "Items_0130.png";
        if (itemName.Contains("火把")) return "Items_0131.png";

        // 6. 武器家族 (严格对齐 Items.wil 30~55)
        if (itemName.Contains("木剑")) return "Items_0030.png";
        if (itemName.Contains("匕首")) return "Items_0031.png";
        if (itemName.Contains("乌木剑")) return "Items_0032.png";
        if (itemName.Contains("青铜剑")) return "Items_0033.png";
        if (itemName.Contains("铁剑")) return "Items_0034.png";
        if (itemName.Contains("短剑")) return "Items_0035.png";
        if (itemName.Contains("凌风")) return "Items_0036.png";
        if (itemName.Contains("破魂")) return "Items_0037.png";
        if (itemName.Contains("斩马刀")) return "Items_0038.png";
        if (itemName.Contains("修罗")) return "Items_0039.png";
        if (itemName.Contains("炼狱")) return "Items_0040.png";
        if (itemName.Contains("井中月")) return "Items_0041.png";
        if (itemName.Contains("降魔")) return "Items_0042.png";
        if (itemName.Contains("八荒")) return "Items_0043.png";
        if (itemName.Contains("凝霜")) return "Items_0044.png";
        if (itemName.Contains("银蛇")) return "Items_0045.png";
        if (itemName.Contains("魔杖")) return "Items_0046.png";
        if (itemName.Contains("裁决之杖") || itemName.Contains("裁决")) return "Items_0048.png";
        if (itemName.Contains("骨玉权杖") || itemName.Contains("骨玉")) return "Items_0049.png";
        if (itemName.Contains("鹤嘴锄")) return "Items_0050.png";
        if (itemName.Contains("偃月")) return "Items_0051.png";
        if (itemName.Contains("龙纹剑") || itemName.Contains("龙纹")) return "Items_0052.png";
        if (itemName.Contains("命运之刃")) return "Items_0053.png";
        if (itemName.Contains("屠龙")) return "Items_0054.png";

        // 7. 衣服家族 (男 60~64, 女 80~84)
        if (itemName.Contains("布衣(女)")) return "Items_0080.png";
        if (itemName.Contains("布衣")) return "Items_0060.png";
        if (itemName.Contains("轻型盔甲(女)")) return "Items_0081.png";
        if (itemName.Contains("轻型盔甲")) return "Items_0061.png";
        if (itemName.Contains("中型盔甲(女)")) return "Items_0081.png";
        if (itemName.Contains("中型盔甲")) return "Items_0061.png";
        if (itemName.Contains("重盔甲(女)")) return "Items_0082.png";
        if (itemName.Contains("重盔甲")) return "Items_0062.png";
        if (itemName.Contains("魔法长袍(女)")) return "Items_0083.png";
        if (itemName.Contains("魔法长袍")) return "Items_0063.png";
        if (itemName.Contains("灵魂战衣(女)")) return "Items_0084.png";
        if (itemName.Contains("灵魂战衣")) return "Items_0064.png";

        // 8. 头盔家族 (Items 100~111)
        if (itemName.Contains("道士头盔")) return "Items_0108.png";
        if (itemName.Contains("骷髅头盔")) return "Items_0103.png";
        if (itemName.Contains("魔法头盔")) return "Items_0102.png";
        if (itemName.Contains("青铜头盔")) return "Items_0100.png";
        if (itemName.Contains("黑铁头盔")) return "Items_0107.png";
        if (itemName.Contains("头盔")) return "Items_0100.png";

        // 9. 戒指首饰 (Items 140~159)
        if (itemName.Contains("古铜戒指")) return "Items_0140.png";
        if (itemName.Contains("玻璃戒指")) return "Items_0141.png";
        if (itemName.Contains("金戒指")) return "Items_0146.png";
        if (itemName.Contains("红宝石戒指")) return "Items_0147.png";
        if (itemName.Contains("珊瑚戒指")) return "Items_0148.png";
        if (itemName.Contains("铂金戒指")) return "Items_0149.png";
        if (itemName.Contains("骷髅戒指")) return "Items_0150.png";
        if (itemName.Contains("龙之戒指")) return "Items_0151.png";
        if (itemName.Contains("泰坦戒指")) return "Items_0152.png";
        if (itemName.Contains("复活戒指") || itemName.Contains("麻痹戒指") || itemName.Contains("护身戒指") || itemName.Contains("戒指")) return "Items_0146.png";

        // 兜底返回经典物品
        return "Items_0001.png";
    }
}
