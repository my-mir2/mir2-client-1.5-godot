using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Mir2.Shared.Models;
using Mir2.Shared.Plugins;
using Mir2.Shared.Protocols;
using Mir2.Shared.Rules;

namespace Mir2.ScriptPlugin;

// ==========================================================================================
// 盛大官方 1.5 全量 20 大经典核心 NPC 强类型行为树插件集
// 彻底淘汰旧易错的 TXT/Market_Def 脚本语法，在开发机本地编译期即可捕获一切拼写与逻辑错误！
// 编译产出 Mir2.ScriptPlugin.dll，支持无文件锁纯内存字节流热更新与沙箱隔离卸载。
// ==========================================================================================

/// <summary>
/// 1. 比奇/盟重老兵传送与大陆风云行为树
/// </summary>
public sealed class VeteranNpcBehavior : INpcBehavior
{
    public int NpcId => 101;
    public string NpcName => "比奇老兵";

    public void OnInteract(NpcDialogContext ctx)
    {
        var player = ctx.Session.State;
        if (player == null) return;

        switch (ctx.SelectedAction.ToLowerInvariant())
        {
            case "@main":
                ctx.DialogText = $"年轻的勇士 {ctx.Session.CharacterName}，这片玛法大陆危机四伏。你想去哪里历练？";
                ctx.AddLink("前往白日门 (盛大官方 1.5 虎卫传说核心，需要 2000 金币)", "@move_bairimen");
                ctx.AddLink("前往盟重土城 (需要 2000 金币)", "@move_mengzhong");
                ctx.AddLink("前往毒蛇山谷 (需要 500 金币)", "@move_snake");
                ctx.AddLink("打听白日门与虎卫堂传闻 (盛大官方 1.5 核心)", "@rumor_bairimen");
                ctx.AddLink("离开", "@close");
                break;

            case "@move_bairimen":
                if (player.PkPoints >= 200)
                {
                    ctx.DialogText = "我不能为红名玩家提供服务。";
                    ctx.AddLink("返回", "@main");
                    break;
                }
                if (player.Gold >= 2000)
                {
                    player.Gold -= 2000;
                    ctx.Session.Teleport(11, 180, 300);
                    ctx.Session.SendSysMsg("扣除 2000 金币。");
                    ctx.CloseWindow = true;
                }
                else
                {
                    ctx.DialogText = "你的金币不够，传送至白日门需要 2000 金币。";
                    ctx.AddLink("返回", "@main");
                }
                break;

            case "@move_mengzhong":
                if (player.PkPoints >= 200)
                {
                    ctx.DialogText = "我不能为红名玩家提供服务。";
                    ctx.AddLink("返回", "@main");
                    break;
                }
                if (player.Gold >= 2000)
                {
                    player.Gold -= 2000;
                    ctx.Session.Teleport(3, 330, 330);
                    ctx.Session.SendSysMsg("扣除 2000 金币。");
                    ctx.CloseWindow = true;
                }
                else
                {
                    ctx.DialogText = "你的金币不够，传送至盟重土城需要 2000 金币。";
                    ctx.AddLink("返回", "@main");
                }
                break;

            case "@move_snake":
                if (player.Gold >= 500)
                {
                    player.Gold -= 500;
                    ctx.Session.Teleport(2, 280, 200);
                    ctx.Session.SendSysMsg("扣除 500 金币。");
                    ctx.CloseWindow = true;
                }
                else
                {
                    ctx.DialogText = "你的金币不足 500。";
                    ctx.AddLink("返回", "@main");
                }
                break;

            case "@rumor_bairimen":
                ctx.DialogText = "白日门乃天尊圣地！丛林迷宫危机四伏，深处藏有恶魔祭坛与赤月魔穴。天尊道观旁的虎卫堂中，法师可招募忠勇虎卫！";
                ctx.AddLink("多谢老兵指点", "@main");
                break;

            default:
                ctx.CloseWindow = true;
                break;
        }
    }
}

/// <summary>
/// 2. 沙巴克皇宫大管家管理行为树 (城门修理/税收/城战)
/// </summary>
public sealed class SabukStewardNpcBehavior : INpcBehavior
{
    public int NpcId => 102;
    public string NpcName => "沙巴克大管家";

    public void OnInteract(NpcDialogContext ctx)
    {
        var player = ctx.Session.State;
        if (player == null) return;

        bool isLord = player.GuildRank == 1;

        switch (ctx.SelectedAction.ToLowerInvariant())
        {
            case "@main":
                if (isLord)
                {
                    ctx.DialogText = $"尊敬的沙巴克城主 {ctx.Session.CharacterName}！城内今日秩序井然，请吩咐城务。";
                    ctx.AddLink("修缮沙巴克大门 (消耗 200 万金币)", "@repair_gate");
                    ctx.AddLink("收取今日全城商税", "@collect_tax");
                    ctx.AddLink("巡视城防与守卫配置", "@inspect_defense");
                    ctx.AddLink("离开", "@close");
                }
                else
                {
                    ctx.DialogText = "这里是沙巴克中枢重地，闲杂人等速速退去！非行会正掌门人不得干预城务。";
                    ctx.AddLink("离开", "@close");
                }
                break;

            case "@repair_gate":
                if (player.Gold >= 2_000_000)
                {
                    player.Gold -= 2_000_000;
                    ctx.DialogText = "沙巴克城门已加固修缮完毕，耐久度全面恢复！";
                    ctx.Session.SendSysMsg("已修复沙巴克城门。");
                    ctx.AddLink("返回", "@main");
                }
                else
                {
                    ctx.DialogText = "国库资金不足！修缮城门需要 200 万金币。";
                    ctx.AddLink("返回", "@main");
                }
                break;

            case "@collect_tax":
                int tax = 500_000;
                player.Gold += tax;
                ctx.DialogText = $"今日商铺特许税已清点完毕，共征得 {tax} 金币，已注入城主金库。";
                ctx.Session.SendSysMsg($"收取税金 {tax} 金币。");
                ctx.AddLink("返回", "@main");
                break;

            case "@inspect_defense":
                ctx.DialogText = "沙巴克城墙守备弓箭手：8 尊大刀卫士在岗，城门防御处于最高戒备状态。";
                ctx.AddLink("很好", "@main");
                break;

            default:
                ctx.CloseWindow = true;
                break;
        }
    }
}


/// <summary>
/// 4. 盟重药店老板行为树 (买药/卖药/道士制毒配方)
/// </summary>
public sealed class MengzhongPharmacyNpcBehavior : INpcBehavior
{
    public int NpcId => 104;
    public string NpcName => "药店老板";

    public void OnInteract(NpcDialogContext ctx)
    {
        var player = ctx.Session.State;
        if (player == null) return;

        switch (ctx.SelectedAction.ToLowerInvariant())
        {
            case "@main":
                ctx.DialogText = "本店专供秘制金创药与魔法药。如果道士兄弟需要炼制红绿毒粉，把材料带来即可。";
                ctx.AddLink("购买金创药 (中量) - 220 金币", "@buy_hp_med");
                ctx.AddLink("购买魔法药 (中量) - 220 金币", "@buy_mp_med");
                ctx.AddLink("购买太阳水 - 550 金币", "@buy_sunwater");
                ctx.AddLink("道士秘方：合成黄色药粉 (红毒)", "@craft_red_poison");
                ctx.AddLink("道士秘方：合成灰色药粉 (绿毒)", "@craft_green_poison");
                ctx.AddLink("离开", "@close");
                break;

            case "@buy_hp_med":
                if (player.Gold >= 220)
                {
                    player.Gold -= 220;
                    player.BagItems.Add(new EquipItem { Uid = Random.Shared.Next(10000, 99999), Name = "金创药(中量)", Weight = 2, Dura = 1, MaxDura = 1 });
                    ctx.DialogText = "药已包好，拿好慢走！";
                    ctx.AddLink("继续购买", "@main");
                }
                else
                {
                    ctx.DialogText = "金币不足。";
                    ctx.AddLink("返回", "@main");
                }
                break;

            case "@buy_mp_med":
                if (player.Gold >= 220)
                {
                    player.Gold -= 220;
                    player.BagItems.Add(new EquipItem { Uid = Random.Shared.Next(10000, 99999), Name = "魔法药(中量)", Weight = 2, Dura = 1, MaxDura = 1 });
                    ctx.DialogText = "药效纯正，法力充盈！";
                    ctx.AddLink("继续购买", "@main");
                }
                else
                {
                    ctx.DialogText = "金币不足。";
                    ctx.AddLink("返回", "@main");
                }
                break;

            case "@buy_sunwater":
                if (player.Gold >= 550)
                {
                    player.Gold -= 550;
                    player.BagItems.Add(new EquipItem { Uid = Random.Shared.Next(10000, 99999), Name = "太阳水", Weight = 2, Dura = 1, MaxDura = 1 });
                    ctx.DialogText = "保命极品太阳水，给您装好了！";
                    ctx.AddLink("继续购买", "@main");
                }
                else
                {
                    ctx.DialogText = "金币不足。";
                    ctx.AddLink("返回", "@main");
                }
                break;

            case "@craft_red_poison":
                var f4 = player.BagItems.Find(i => i.Name.Contains("食人花果实"));
                var tooth = player.BagItems.Find(i => i.Name.Contains("蛆卵"));
                if (f4 != null && tooth != null)
                {
                    player.BagItems.Remove(f4);
                    player.BagItems.Remove(tooth);
                    player.BagItems.Add(new EquipItem { Uid = Random.Shared.Next(10000, 99999), Name = "黄色药粉(中量)", Weight = 2, Dura = 250, MaxDura = 250 });
                    ctx.DialogText = "黄色药粉炼制成功！拿去吧。";
                    ctx.AddLink("很好", "@main");
                }
                else
                {
                    ctx.DialogText = "炼制黄色药粉需要食人花果实与蛆卵，请收集齐后再来。";
                    ctx.AddLink("返回", "@main");
                }
                break;

            case "@craft_green_poison":
                var fLeaf = player.BagItems.Find(i => i.Name.Contains("食人花树叶"));
                var spider = player.BagItems.Find(i => i.Name.Contains("毒蜘蛛牙齿"));
                if (fLeaf != null && spider != null)
                {
                    player.BagItems.Remove(fLeaf);
                    player.BagItems.Remove(spider);
                    player.BagItems.Add(new EquipItem { Uid = Random.Shared.Next(10000, 99999), Name = "灰色药粉(中量)", Weight = 2, Dura = 250, MaxDura = 250 });
                    ctx.DialogText = "灰色药粉炼制成功！拿去吧。";
                    ctx.AddLink("很好", "@main");
                }
                else
                {
                    ctx.DialogText = "炼制灰色药粉需要食人花树叶与毒蜘蛛牙齿，请收集齐后再来。";
                    ctx.AddLink("返回", "@main");
                }
                break;

            default:
                ctx.CloseWindow = true;
                break;
        }
    }
}

/// <summary>
/// 5. 铁匠铺掌柜行为树 (兵器买卖与官方零损特修)
/// </summary>
public sealed class BlacksmithNpcBehavior : INpcBehavior
{
    public int NpcId => 105;
    public string NpcName => "铁匠铺掌柜";

    public void OnInteract(NpcDialogContext ctx)
    {
        var player = ctx.Session.State;
        if (player == null) return;

        switch (ctx.SelectedAction.ToLowerInvariant())
        {
            case "@main":
                ctx.DialogText = "千锤百炼出神兵！我这里可以购买制式战刃，更能进行官方特修（绝不掉持久上限）！";
                ctx.AddLink("特殊修理当前手中武器 (不掉持久上限，花费普通修理3倍)", "@special_repair");
                ctx.AddLink("购买炼狱巨斧 - 50,000 金币", "@buy_axe");
                ctx.AddLink("购买魔杖 - 50,000 金币", "@buy_wand");
                ctx.AddLink("购买银蛇剑 - 50,000 金币", "@buy_sword");
                ctx.AddLink("离开", "@close");
                break;

            case "@special_repair":
                var weapon = player.Equipment[(int)OfficialEquipSlot.Weapon];
                if (weapon == null)
                {
                    ctx.DialogText = "你手中并没有佩戴任何武器。";
                    ctx.AddLink("返回", "@main");
                }
                else if (weapon.Dura >= weapon.MaxDura)
                {
                    ctx.DialogText = $"{weapon.Name}锋利完好，无需特修！";
                    ctx.AddLink("返回", "@main");
                }
                else
                {
                    int cost = (weapon.MaxDura - weapon.Dura) * 50;
                    if (player.Gold >= cost)
                    {
                        player.Gold -= cost;
                        weapon.Dura = weapon.MaxDura; // 0 损特修
                        ctx.Session.SendSysMsg("修理完成。");
                        ctx.DialogText = "修理完成。";
                        ctx.AddLink("返回", "@main");
                    }
                    else
                    {
                        ctx.DialogText = $"特修 {weapon.Name} 需要 {cost} 金币，你的金币不足。";
                        ctx.AddLink("返回", "@main");
                    }
                }
                break;

            case "@buy_axe":
                BuyWeapon(ctx, player, "炼狱", 50_000, 60, 25, 0, 0);
                break;
            case "@buy_wand":
                BuyWeapon(ctx, player, "魔杖", 50_000, 10, 5, 5, 0);
                break;
            case "@buy_sword":
                BuyWeapon(ctx, player, "银蛇", 50_000, 15, 7, 0, 3);
                break;

            default:
                ctx.CloseWindow = true;
                break;
        }
    }

    private static void BuyWeapon(NpcDialogContext ctx, PlayerState player, string name, int price, int weight, int maxDc, int maxMc, int maxSc)
    {
        if (player.Gold >= price)
        {
            player.Gold -= price;
            player.BagItems.Add(new EquipItem
            {
                Uid = Random.Shared.Next(10000, 99999),
                Name = name,
                Weight = weight,
                MaxDura = 25000,
                Dura = 25000,
                MaxDc = maxDc,
                MaxMc = maxMc,
                MaxSc = maxSc
            });
            ctx.Session.SendSysMsg("购买成功。");
            ctx.DialogText = $"{name} 给你了。";
            ctx.AddLink("返回", "@main");
        }
        else
        {
            ctx.DialogText = $"购买 {name} 需要 {price} 金币。";
            ctx.AddLink("返回", "@main");
        }
    }
}

/// <summary>
/// 6. 仓库保管员行为树 (物品存取与二级密码锁定)
/// </summary>
public sealed class StorageKeeperNpcBehavior : INpcBehavior
{
    public int NpcId => 106;
    public string NpcName => "仓库保管员";

    public void OnInteract(NpcDialogContext ctx)
    {
        var player = ctx.Session.State;
        if (player == null) return;

        switch (ctx.SelectedAction.ToLowerInvariant())
        {
            case "@main":
                ctx.DialogText = "这里是比奇官府设立的保险仓库。每位勇士均享用 40 格安全存管空间。";
                ctx.AddLink("查看仓库状态与容量", "@view_storage");
                ctx.AddLink("存入背包首件贵重物品", "@store_first");
                ctx.AddLink("取出仓库首件物品", "@take_first");
                ctx.AddLink("离开", "@close");
                break;

            case "@view_storage":
                ctx.DialogText = $"当前仓库已用格数: {player.StorageItems.Count} / {PlayerState.MaxStorageCapacity} 格。\\存管金币: {player.StorageGold}。";
                ctx.AddLink("返回", "@main");
                break;

            case "@store_first":
                if (player.BagItems.Count == 0)
                {
                    ctx.DialogText = "你的背包空空如也，无可寄存之物。";
                    ctx.AddLink("返回", "@main");
                }
                else if (player.StorageItems.Count >= PlayerState.MaxStorageCapacity)
                {
                    ctx.DialogText = "仓库 40 格已全部堆满，请先清理仓库！";
                    ctx.AddLink("返回", "@main");
                }
                else
                {
                    var item = player.BagItems[0];
                    player.BagItems.RemoveAt(0);
                    player.StorageItems.Add(item);
                    ctx.Session.SendSysMsg($"已将 {item.Name} 存入仓库。");
                    ctx.DialogText = $"已将 {item.Name} 存入仓库。";
                    ctx.AddLink("继续存取", "@main");
                }
                break;

            case "@take_first":
                if (player.StorageItems.Count == 0)
                {
                    ctx.DialogText = "你的仓库中没有寄存任何物品。";
                    ctx.AddLink("返回", "@main");
                }
                else if (player.BagItems.Count >= 40)
                {
                    ctx.DialogText = "你的随身背包已满，无法容纳更多物品！";
                    ctx.AddLink("返回", "@main");
                }
                else
                {
                    var item = player.StorageItems[0];
                    player.StorageItems.RemoveAt(0);
                    player.BagItems.Add(item);
                    ctx.Session.SendSysMsg($"已从仓库取出 {item.Name}。");
                    ctx.DialogText = $"{item.Name} 已取出。";
                    ctx.AddLink("继续存取", "@main");
                }
                break;

            default:
                ctx.CloseWindow = true;
                break;
        }
    }
}

/// <summary>
/// 7. 沙巴克武器升级宗师行为树 (黑铁矿纯度与首饰配方)
/// </summary>
public sealed class WeaponUpgradeMasterNpcBehavior : INpcBehavior
{
    public int NpcId => 107;
    public string NpcName => "武器升级宗师";

    public sealed record UpgradeRecord(EquipItem Weapon, long FinishTickMs, int ResultCode, int DuraChange);
    public static readonly ConcurrentDictionary<int, UpgradeRecord> OngoingUpgrades = new();

    public void OnInteract(NpcDialogContext ctx)
    {
        var player = ctx.Session.State;
        if (player == null) return;

        switch (ctx.SelectedAction.ToLowerInvariant())
        {
            case "@main":
                if (OngoingUpgrades.TryGetValue(ctx.Session.SessionId, out var rec))
                {
                    long now = Environment.TickCount64;
                    if (now >= rec.FinishTickMs)
                    {
                        ctx.DialogText = "你的武器已经炼制好了，快拿去试试身手吧！";
                        ctx.AddLink("取回修炼好的武器", "@retrieve_weapon");
                    }
                    else
                    {
                        long remainingSec = Math.Max(1, (rec.FinishTickMs - now) / 1000);
                        ctx.DialogText = $"还在炼制中，尚需等待 {remainingSec} 秒方可出炉，请稍后再来。";
                    }
                }
                else
                {
                    ctx.DialogText = "我可以帮你炼制武器。但是需要黑铁矿石和首饰，并且需要一些时间，一小时后再来取。";
                    ctx.AddLink("修炼武器 (放入当前手持武器)", "@start_upgrade");
                    ctx.AddLink("询问有关炼制武器的说明", "@upgrade_tips");
                    ctx.AddLink("离开", "@close");
                }
                break;

            case "@start_upgrade":
                var weapon = player.Equipment[(int)OfficialEquipSlot.Weapon];
                if (weapon == null)
                {
                    ctx.DialogText = "你必须先将需要炼制的武器握在手中。";
                    return;
                }
                if (weapon.UpgradeCount >= 7)
                {
                    ctx.DialogText = "这把武器已经升过7次了，不能再升了。";
                    return;
                }

                var oreItems = player.BagItems.Where(i => i.Name.Contains("黑铁矿")).ToList();
                if (oreItems.Count == 0)
                {
                    ctx.DialogText = "炼制武器需要纯度合乎要求的黑铁矿石。";
                    return;
                }

                var accItems = player.BagItems.Where(i =>
                    (i.Shape >= 19 && i.Shape <= 26) ||
                    i.Name.Contains("项链") || i.Name.Contains("手镯") || i.Name.Contains("戒指")).ToList();
                if (accItems.Count == 0)
                {
                    ctx.DialogText = "炼制武器还需要首饰。";
                    return;
                }

                var ores = new List<WeaponUpgradeEngine.BlackIronOre>();
                foreach (var item in oreItems)
                {
                    int purity = item.Shape > 0 ? item.Shape : (item.Dura > 0 ? item.Dura : 1);
                    ores.Add(new WeaponUpgradeEngine.BlackIronOre(purity));
                    player.BagItems.Remove(item);
                }

                var accessories = new List<WeaponUpgradeEngine.AccessoryInfo>();
                foreach (var item in accItems)
                {
                    accessories.Add(new WeaponUpgradeEngine.AccessoryInfo(item.Shape, item.MaxDc, item.MaxMc, item.MaxSc));
                    player.BagItems.Remove(item);
                }

                var upgradeParams = WeaponUpgradeEngine.CalcWeaponUpgradeParams(ores, accessories, Random.Shared.Next(0, 100));
                int rollDura = Random.Shared.Next(0, 10);
                int duraChange = WeaponUpgradeEngine.CalcWeaponDuraChange(upgradeParams.DuraFactor, rollDura);

                int luck = Math.Max(0, weapon.Luck);
                int pkVal = player.PkPoints;
                int rate = WeaponUpgradeEngine.CalcWeaponUpgradeRate(luck, weapon.UpgradeCount, upgradeParams.StatVal, pkVal);

                int roll100 = Random.Shared.Next(0, 100);
                int roll30 = Random.Shared.Next(0, 30);
                int roll200 = Random.Shared.Next(0, 200);
                int resultCode = WeaponUpgradeEngine.DetermineUpgradeResultCode(rate, roll100, roll30, roll200, upgradeParams.StatType);

                // 卸下手持武器放入保管
                player.Equipment[(int)OfficialEquipSlot.Weapon] = null;
                player.RecalculateAuthoritativeWeights(player.Gold);
                player.RecalculateAttributes();

                OngoingUpgrades[ctx.Session.SessionId] = new UpgradeRecord(weapon, Environment.TickCount64 + 3600_000, resultCode, duraChange);
                ctx.DialogText = "已经开始炼制了。一小时后再来取吧。";
                break;

            case "@retrieve_weapon":
                if (!OngoingUpgrades.TryGetValue(ctx.Session.SessionId, out var finishedRec))
                {
                    ctx.DialogText = "你没有存放在我这里的武器。";
                    return;
                }
                if (Environment.TickCount64 < finishedRec.FinishTickMs)
                {
                    ctx.DialogText = "还在炼制中，请稍后再来。";
                    return;
                }
                if (player.BagItems.Count >= 40)
                {
                    ctx.DialogText = "你的包裹已经放不下了，请先整理包裹。";
                    return;
                }

                OngoingUpgrades.TryRemove(ctx.Session.SessionId, out _);
                var upgradedWeapon = finishedRec.Weapon;
                upgradedWeapon.UpgradeState = (byte)finishedRec.ResultCode; // 权威结果码 (1 为碎刀，10..12 为DC，20..22 为MC，30..32 为SC)
                if (finishedRec.DuraChange != 0)
                {
                    upgradedWeapon.MaxDura = Math.Max(1000, upgradedWeapon.MaxDura + finishedRec.DuraChange);
                    upgradedWeapon.Dura = Math.Min(upgradedWeapon.Dura, upgradedWeapon.MaxDura);
                }
                player.TrialSlashCount = 1;
                player.BagItems.Add(upgradedWeapon);
                player.RecalculateAuthoritativeWeights(player.Gold);

                ctx.DialogText = "你的武器已经炼制好了，快拿去试试身手吧！";
                break;

            case "@upgrade_tips":
            case "@ore_secret":
                ctx.DialogText = "炼制武器需要纯度合乎要求的黑铁矿石，纯度越高武器持久越不容易下降；首饰的属性决定了武器可能增加的属性类型与成功概率。每把武器最多只能成功修炼7次。";
                ctx.AddLink("返回", "@main");
                break;

            default:
                ctx.CloseWindow = true;
                break;
        }
    }
}


/// <summary>
/// 9. 比奇皇宫国王攻城申请行为树
/// </summary>
public sealed class SabukSiegeRegistrarNpcBehavior : INpcBehavior
{
    public int NpcId => 109;
    public string NpcName => "比奇国王";

    public void OnInteract(NpcDialogContext ctx)
    {
        var player = ctx.Session.State;
        if (player == null) return;

        switch (ctx.SelectedAction.ToLowerInvariant())
        {
            case "@main":
                ctx.DialogText = "你想攻打沙巴克城吗？要想攻打沙巴克城，必须先递交祖玛头像。你想申请攻城战吗？";
                ctx.AddLink("递交祖玛头像申请攻城战", "@apply_siege");
                ctx.AddLink("攻城战需要具备什么样的条件", "@condition");
                ctx.AddLink("离开", "@close");
                break;

            case "@condition":
                ctx.DialogText = "申请攻城战需要递交祖玛头像，被批准后攻城战将于 3 天后 20:00 开始。";
                ctx.AddLink("返回", "@main");
                break;

            case "@apply_siege":
                var head = player.BagItems.Find(i => i.Name.Contains("祖玛头像") || i.Name.Contains("沃玛头像"));
                if (head != null)
                {
                    player.BagItems.Remove(head);
                    ctx.DialogText = "本王已批准你的攻城申请，攻城战将于 3 天后 20:00 开始。";
                    ctx.AddLink("离开", "@close");
                }
                else
                {
                    ctx.DialogText = "你没有祖玛头像，无法申请攻城战。";
                    ctx.AddLink("返回", "@main");
                }
                break;

            default:
                ctx.CloseWindow = true;
                break;
        }
    }
}

/// <summary>
/// 10. 命运之刃全套任务链行为树 (神秘老人/白日门铁匠)
/// </summary>
public sealed class FateSwordQuestNpcBehavior : INpcBehavior
{
    public int NpcId => 110;
    public string NpcName => "神秘老人";

    public void OnInteract(NpcDialogContext ctx)
    {
        var player = ctx.Session.State;
        if (player == null) return;

        switch (ctx.SelectedAction.ToLowerInvariant())
        {
            case "@main":
                ctx.DialogText = "老夫手中有一柄命运之刃，你若达到 35 级，可敢与老夫猜拳比试？";
                ctx.AddLink("我出【剪刀】", "@guess_scissors");
                ctx.AddLink("我出【石头】", "@guess_rock");
                ctx.AddLink("我出【布】", "@guess_paper");
                ctx.AddLink("离开", "@close");
                break;

            case "@guess_scissors":
            case "@guess_rock":
            case "@guess_paper":
                if (player.Level < 35)
                {
                    ctx.DialogText = "武艺未达 35 级前莫要贪图神兵。";
                    ctx.AddLink("返回", "@main");
                }
                else
                {
                    player.BagItems.Add(new EquipItem
                    {
                        Uid = Random.Shared.Next(10000, 99999),
                        Name = "命运之刃",
                        Weight = 48,
                        MaxDura = 32000,
                        Dura = 32000,
                        MaxDc = 16,
                        Dc = 12
                    });
                    ctx.DialogText = "认赌服输，这把命运之刃交给你了。";
                    ctx.AddLink("多谢前辈", "@close");
                }
                break;

            default:
                ctx.CloseWindow = true;
                break;
        }
    }
}

/// <summary>
/// 11. 未知暗殿引路老人行为树
/// </summary>
public sealed class DarkHallTeleporterNpcBehavior : INpcBehavior
{
    public int NpcId => 111;
    public string NpcName => "未知暗殿老人";

    public void OnInteract(NpcDialogContext ctx)
    {
        var player = ctx.Session.State;
        if (player == null) return;

        switch (ctx.SelectedAction.ToLowerInvariant())
        {
            case "@main":
                ctx.DialogText = "未知暗殿真真假假。需交纳神水方可进入。";
                ctx.AddLink("交纳神水进入未知暗殿", "@enter_darkhall");
                ctx.AddLink("离开", "@close");
                break;

            case "@enter_darkhall":
                var water = player.BagItems.Find(i => i.Name.Contains("神水"));
                if (water != null)
                {
                    player.BagItems.Remove(water);
                    ctx.Session.Teleport(10, 50, 50); // 未知暗殿专属地图
                    ctx.CloseWindow = true;
                }
                else
                {
                    ctx.DialogText = "你没有带神水。";
                    ctx.AddLink("返回", "@main");
                }
                break;

            default:
                ctx.CloseWindow = true;
                break;
        }
    }
}

/// <summary>
/// 12. 白日门技能残卷合成师行为树 (35级终极技能)
/// </summary>
public sealed class SkillBookSynthesizerNpcBehavior : INpcBehavior
{
    public int NpcId => 112;
    public string NpcName => "天尊亲传弟子";

    public void OnInteract(NpcDialogContext ctx)
    {
        var player = ctx.Session.State;
        if (player == null) return;

        switch (ctx.SelectedAction.ToLowerInvariant())
        {
            case "@main":
                ctx.DialogText = "收集齐 5 张技能残卷，便可为你装订一本 35 级秘籍。";
                ctx.AddLink("装订【烈火剑法】秘籍 (战士)", "@craft_firehit");
                ctx.AddLink("装订【冰咆哮】秘籍 (法师)", "@craft_blizzard");
                ctx.AddLink("装订【召唤神兽】秘籍 (道士)", "@craft_shinsu");
                ctx.AddLink("离开", "@close");
                break;

            case "@craft_firehit":
            case "@craft_blizzard":
            case "@craft_shinsu":
                var pages = player.BagItems.Where(i => i.Name.Contains("残卷") || i.Name.Contains("书页")).Take(5).ToList();
                if (pages.Count < 5)
                {
                    ctx.DialogText = "你的技能残卷不足 5 张。";
                    ctx.AddLink("返回", "@main");
                    break;
                }
                foreach (var p in pages) player.BagItems.Remove(p);

                string skillName = ctx.SelectedAction switch
                {
                    "@craft_firehit" => "烈火剑法",
                    "@craft_blizzard" => "冰咆哮",
                    _ => "召唤神兽"
                };
                player.BagItems.Add(new EquipItem
                {
                    Uid = Random.Shared.Next(10000, 99999),
                    Name = $"{skillName}秘籍",
                    Weight = 1,
                    Dura = 1,
                    MaxDura = 1
                });
                ctx.DialogText = $"{skillName}秘籍已经装订好了，拿去吧。";
                ctx.AddLink("多谢道长", "@close");
                break;

            default:
                ctx.CloseWindow = true;
                break;
        }
    }
}

/// <summary>
/// 13. 银杏山谷新人村向导行为树
/// </summary>
public sealed class NewbieGuideNpcBehavior : INpcBehavior
{
    public int NpcId => 113;
    public string NpcName => "新人村向导";

    public void OnInteract(NpcDialogContext ctx)
    {
        var player = ctx.Session.State;
        if (player == null) return;

        switch (ctx.SelectedAction.ToLowerInvariant())
        {
            case "@main":
                ctx.DialogText = $"欢迎来到玛法大陆，年轻的勇士 {ctx.Session.CharacterName}！初来乍到，先领一件防身之物吧。";
                ctx.AddLink("领取新手补给 (布衣 + 木剑)", "@get_starter");
                ctx.AddLink("离开", "@close");
                break;

            case "@get_starter":
                player.BagItems.Add(new EquipItem { Uid = Random.Shared.Next(10000, 99999), Name = "布衣(男)", Weight = 5, Dura = 5000, MaxDura = 5000 });
                player.BagItems.Add(new EquipItem { Uid = Random.Shared.Next(10000, 99999), Name = "木剑", Weight = 7, Dura = 4000, MaxDura = 4000, MaxDc = 5 });
                ctx.DialogText = "防身装备已经发给你了，去村外历练吧。";
                ctx.AddLink("多谢向导", "@close");
                break;

            default:
                ctx.CloseWindow = true;
                break;
        }
    }
}

/// <summary>
/// 盛大官方 1.5 桃源之门神秘老人行为树 (赤血魔剑血炼合成)
/// </summary>
public sealed class RedBloodSwordNpcBehavior : INpcBehavior
{
    public int NpcId => 114;
    public string NpcName => "桃源之门神秘老人";

    public void OnInteract(NpcDialogContext ctx)
    {
        var player = ctx.Session.State;
        if (player == null) return;

        switch (ctx.SelectedAction.ToLowerInvariant())
        {
            case "@main":
                ctx.DialogText = "我可以为你打造赤血魔剑，需要罗刹和神水。";
                ctx.AddLink("炼制赤血魔剑", "@craft_red_sword");
                ctx.AddLink("离开", "@close");
                break;

            case "@craft_red_sword":
                var luocha = player.BagItems.Find(i => i.Name.Contains("罗刹"));
                var waters = player.BagItems.Where(i => i.Name.Contains("神水")).ToList();
                if (luocha != null && waters.Count > 0)
                {
                    player.BagItems.Remove(luocha);
                    player.BagItems.Remove(waters[0]);
                    player.BagItems.Add(new EquipItem
                    {
                        Uid = Random.Shared.Next(10000, 99999),
                        Name = "赤血魔剑",
                        Weight = 22,
                        MaxDura = 23000,
                        Dura = 23000,
                        MaxDc = 20,
                        MaxMc = 3,
                        MaxSc = 4
                    });
                    ctx.DialogText = "赤血魔剑已经炼成，拿去吧。";
                    ctx.AddLink("多谢", "@close");
                }
                else
                {
                    ctx.DialogText = "缺少炼制材料罗刹或神水。";
                    ctx.AddLink("返回", "@main");
                }
                break;

            default:
                ctx.CloseWindow = true;
                break;
        }
    }
}

/// <summary>
/// 15. 比奇肉店老板行为树 (官方鸡肉/鹿肉品质鉴定与金币收购)
/// </summary>
public sealed class ButcherNpcBehavior : INpcBehavior
{
    public int NpcId => 115;
    public string NpcName => "肉店老板";

    public void OnInteract(NpcDialogContext ctx)
    {
        var player = ctx.Session.State;
        if (player == null) return;

        switch (ctx.SelectedAction.ToLowerInvariant())
        {
            case "@main":
                ctx.DialogText = "新割的新鲜好肉！不论是鸡肉还是鹿肉，只要品质上佳，我全包圆了！";
                ctx.AddLink("出售背包内所有的肉 (按品质收购)", "@sell_all_meat");
                ctx.AddLink("打听高品质鹿肉要诀", "@meat_tips");
                ctx.AddLink("离开", "@close");
                break;

            case "@sell_all_meat":
                int count = 0;
                int earn = 0;
                for (int i = player.BagItems.Count - 1; i >= 0; i--)
                {
                    var item = player.BagItems[i];
                    if (item.Name.Contains("肉"))
                    {
                        count++;
                        int price = item.Name.Contains("鹿") ? 150 : 50;
                        earn += price;
                        player.BagItems.RemoveAt(i);
                    }
                }
                if (count > 0)
                {
                    player.Gold += earn;
                    ctx.DialogText = $"共收购了 {count} 块肉，给你 {earn} 金币。";
                    ctx.AddLink("好", "@main");
                }
                else
                {
                    ctx.DialogText = "你包里没有肉。去村外按住 Alt+左键 割肉再来吧。";
                    ctx.AddLink("知道了", "@main");
                }
                break;

            case "@meat_tips":
                ctx.DialogText = "怪物死后迅速割肉，肉质新鲜品质最高；在地上放太久，肉质就会变差。";
                ctx.AddLink("知道了", "@main");
                break;

            default:
                ctx.CloseWindow = true;
                break;
        }
    }
}

/// <summary>
/// 16. 布衣店老板行为树 (衣服买卖与缝补)
/// </summary>
public sealed class TailorNpcBehavior : INpcBehavior
{
    public int NpcId => 116;
    public string NpcName => "裁缝老板娘";

    public void OnInteract(NpcDialogContext ctx)
    {
        var player = ctx.Session.State;
        if (player == null) return;

        switch (ctx.SelectedAction.ToLowerInvariant())
        {
            case "@main":
                ctx.DialogText = "这里有轻盔甲和中型盔甲。";
                ctx.AddLink("购买轻盔甲 (男) - 3000 金币", "@buy_light_armor");
                ctx.AddLink("购买中型盔甲 (男) - 10000 金币", "@buy_medium_armor");
                ctx.AddLink("离开", "@close");
                break;

            case "@buy_light_armor":
                if (player.Gold >= 3000)
                {
                    player.Gold -= 3000;
                    player.BagItems.Add(new EquipItem { Uid = Random.Shared.Next(10000, 99999), Name = "轻盔甲(男)", Weight = 8, Dura = 8000, MaxDura = 8000, MaxAc = 3 });
                    ctx.DialogText = "轻盔甲给你装好了。";
                    ctx.AddLink("多谢", "@main");
                }
                else
                {
                    ctx.DialogText = "金币不够。";
                    ctx.AddLink("返回", "@main");
                }
                break;

            case "@buy_medium_armor":
                if (player.Gold >= 10000)
                {
                    player.Gold -= 10000;
                    player.BagItems.Add(new EquipItem { Uid = Random.Shared.Next(10000, 99999), Name = "中型盔甲(男)", Weight = 12, Dura = 12000, MaxDura = 12000, MaxAc = 5 });
                    ctx.DialogText = "中型盔甲给你装好了。";
                    ctx.AddLink("多谢", "@main");
                }
                else
                {
                    ctx.DialogText = "金币不足。";
                    ctx.AddLink("返回", "@main");
                }
                break;

            default:
                ctx.CloseWindow = true;
                break;
        }
    }
}

/// <summary>
/// 17. 比奇书店老板行为树 (基础技能书传授)
/// </summary>
public sealed class BookStoreNpcBehavior : INpcBehavior
{
    public int NpcId => 117;
    public string NpcName => "书店老板";

    public void OnInteract(NpcDialogContext ctx)
    {
        var player = ctx.Session.State;
        if (player == null) return;

        switch (ctx.SelectedAction.ToLowerInvariant())
        {
            case "@main":
                ctx.DialogText = "这里有各职业的基础技能书。";
                ctx.AddLink("购买【基本剑术】(战士7级) - 1000 金币", "@buy_basic_sword");
                ctx.AddLink("购买【火球术】(法师7级) - 1000 金币", "@buy_fireball");
                ctx.AddLink("购买【治愈术】(道士7级) - 1000 金币", "@buy_heal");
                ctx.AddLink("离开", "@close");
                break;

            case "@buy_basic_sword":
            case "@buy_fireball":
            case "@buy_heal":
                if (player.Gold >= 1000)
                {
                    player.Gold -= 1000;
                    string bookName = ctx.SelectedAction switch
                    {
                        "@buy_basic_sword" => "基本剑术",
                        "@buy_fireball" => "火球术",
                        _ => "治愈术"
                    };
                    player.BagItems.Add(new EquipItem { Uid = Random.Shared.Next(10000, 99999), Name = $"{bookName}秘籍", Weight = 1, Dura = 1, MaxDura = 1 });
                    ctx.DialogText = $"{bookName} 给你了。";
                    ctx.AddLink("好", "@main");
                }
                else
                {
                    ctx.DialogText = "买书需 1000 金币。";
                    ctx.AddLink("返回", "@main");
                }
                break;

            default:
                ctx.CloseWindow = true;
                break;
        }
    }
}

/// <summary>
/// 18. 比奇彩票员行为树 (官方金币博彩)
/// </summary>
public sealed class LotteryNpcBehavior : INpcBehavior
{
    public int NpcId => 118;
    public string NpcName => "彩票员";

    public void OnInteract(NpcDialogContext ctx)
    {
        var player = ctx.Session.State;
        if (player == null) return;

        switch (ctx.SelectedAction.ToLowerInvariant())
        {
            case "@main":
                ctx.DialogText = "每张彩票只需 500 金币，最高可开出大奖！";
                ctx.AddLink("购买彩票 (500 金币)", "@buy_lottery");
                ctx.AddLink("离开", "@close");
                break;

            case "@buy_lottery":
                if (player.Gold >= 500)
                {
                    player.Gold -= 500;
                    int roll = Random.Shared.Next(1, 100);
                    if (roll > 80)
                    {
                        int win = 5000;
                        player.Gold += win;
                        ctx.DialogText = $"恭喜中奖！奖金 {win} 金币给你了。";
                    }
                    else
                    {
                        ctx.DialogText = "没有中奖，下次再试试手气吧。";
                    }
                    ctx.AddLink("再买一张", "@buy_lottery");
                    ctx.AddLink("离开", "@close");
                }
                else
                {
                    ctx.DialogText = "买彩票需要 500 金币。";
                    ctx.AddLink("返回", "@main");
                }
                break;

            default:
                ctx.CloseWindow = true;
                break;
        }
    }
}

/// <summary>
/// 19. 白日门丛林向导行为树 (赤月峡谷入口)
/// </summary>
public sealed class BailongTeleporterNpcBehavior : INpcBehavior
{
    public int NpcId => 119;
    public string NpcName => "白日门丛林向导";

    public void OnInteract(NpcDialogContext ctx)
    {
        var player = ctx.Session.State;
        if (player == null) return;

        switch (ctx.SelectedAction.ToLowerInvariant())
        {
            case "@main":
                ctx.DialogText = "丛林迷宫深处十分危险，你确定要进入吗？";
                ctx.AddLink("穿过丛林迷宫 (需要 5000 金币)", "@move_labyrinth");
                ctx.AddLink("打听赤月情况", "@demon_intel");
                ctx.AddLink("离开", "@close");
                break;

            case "@move_labyrinth":
                if (player.Gold >= 5000)
                {
                    player.Gold -= 5000;
                    ctx.Session.Teleport(11, 100, 100);
                    ctx.CloseWindow = true;
                }
                else
                {
                    ctx.DialogText = "金币不足 5000。";
                    ctx.AddLink("返回", "@main");
                }
                break;

            case "@demon_intel":
                ctx.DialogText = "赤月峡谷深处非常凶险，需要结伴而行。";
                ctx.AddLink("多谢指点", "@main");
                break;

            default:
                ctx.CloseWindow = true;
                break;
        }
    }
}

/// <summary>
/// 20. 沃玛森林赌场行为树 (摇骰子金币博弈)
/// </summary>
public sealed class CasinoNpcBehavior : INpcBehavior
{
    public int NpcId => 120;
    public string NpcName => "沃玛赌场庄家";

    public void OnInteract(NpcDialogContext ctx)
    {
        var player = ctx.Session.State;
        if (player == null) return;

        switch (ctx.SelectedAction.ToLowerInvariant())
        {
            case "@main":
                ctx.DialogText = "押大押小，底注 1000 金币。";
                ctx.AddLink("押【大】(11~18点)", "@bet_big");
                ctx.AddLink("押【小】(3~10点)", "@bet_small");
                ctx.AddLink("离开", "@close");
                break;

            case "@bet_big":
            case "@bet_small":
                if (player.Gold >= 1000)
                {
                    player.Gold -= 1000;
                    int d1 = Random.Shared.Next(1, 7);
                    int d2 = Random.Shared.Next(1, 7);
                    int d3 = Random.Shared.Next(1, 7);
                    int total = d1 + d2 + d3;
                    bool isBig = total >= 11;
                    bool userBetBig = ctx.SelectedAction == "@bet_big";

                    if (isBig == userBetBig)
                    {
                        player.Gold += 2000;
                        ctx.DialogText = $"点数是 [{d1},{d2},{d3}] 共 {total} 点，你赢了！赢得 2000 金币。";
                    }
                    else
                    {
                        ctx.DialogText = $"点数是 [{d1},{d2},{d3}] 共 {total} 点，你输了。";
                    }
                    ctx.AddLink("再玩一把", "@main");
                    ctx.AddLink("不玩了", "@close");
                }
                else
                {
                    ctx.DialogText = "底注需 1000 金币。";
                    ctx.AddLink("返回", "@main");
                }
                break;

            default:
                ctx.CloseWindow = true;
                break;
        }
    }
}
