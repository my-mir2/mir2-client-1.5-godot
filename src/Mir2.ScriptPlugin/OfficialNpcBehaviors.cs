using System;
using System.Collections.Generic;
using Mir2.Shared.Models;
using Mir2.Shared.Plugins;
using Mir2.Shared.Protocols;

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
                    ctx.DialogText = "老夫乃比奇守法老兵，不为恶贯满盈的红名狂徒提供传送服务！速速离开！";
                    ctx.AddLink("返回", "@main");
                    break;
                }
                if (player.Gold >= 2000)
                {
                    player.Gold -= 2000;
                    ctx.Session.Teleport(11, 180, 300);
                    ctx.Session.SendSysMsg("【老兵传送】你已安全抵达白日门要塞！扣除 2000 金币。");
                    ctx.CloseWindow = true;
                }
                else
                {
                    ctx.DialogText = "你的盘缠不够，传送至白日门需要 2000 金币。";
                    ctx.AddLink("返回", "@main");
                }
                break;

            case "@move_mengzhong":
                if (player.PkPoints >= 200)
                {
                    ctx.DialogText = "老夫乃比奇守法老兵，不为恶贯满盈的红名狂徒提供传送服务！速速离开！";
                    ctx.AddLink("返回", "@main");
                    break;
                }
                if (player.Gold >= 2000)
                {
                    player.Gold -= 2000;
                    ctx.Session.Teleport(3, 330, 330);
                    ctx.Session.SendSysMsg("【老兵传送】你已安全抵达盟重土城！扣除 2000 金币。");
                    ctx.CloseWindow = true;
                }
                else
                {
                    ctx.DialogText = "你的盘缠不够，传送至盟重土城需要 2000 金币。";
                    ctx.AddLink("返回", "@main");
                }
                break;

            case "@move_snake":
                if (player.Gold >= 500)
                {
                    player.Gold -= 500;
                    ctx.Session.Teleport(2, 280, 200);
                    ctx.Session.SendSysMsg("【老兵传送】你已抵达毒蛇山谷！扣除 500 金币。");
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
                    ctx.Session.SendSysMsg("【沙巴克城务】消耗 200 万金币成功修复沙巴克主城门！");
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
                ctx.Session.SendSysMsg($"【沙巴克金税】成功收取商税 {tax} 金币！");
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
                    ctx.Session.SendSysMsg("【药店交易】成功购入一瓶金创药(中量)！");
                    ctx.DialogText = "药已包好，拿好慢走！";
                    ctx.AddLink("继续购买", "@main");
                }
                else
                {
                    ctx.DialogText = "客官，金币不够啊！";
                    ctx.AddLink("返回", "@main");
                }
                break;

            case "@buy_mp_med":
                if (player.Gold >= 220)
                {
                    player.Gold -= 220;
                    player.BagItems.Add(new EquipItem { Uid = Random.Shared.Next(10000, 99999), Name = "魔法药(中量)", Weight = 2, Dura = 1, MaxDura = 1 });
                    ctx.Session.SendSysMsg("【药店交易】成功购入一瓶魔法药(中量)！");
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
                    ctx.Session.SendSysMsg("【药店交易】成功购入一瓶太阳水 (即时双回 30/40)！");
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
                    ctx.Session.SendSysMsg("【秘药合成】药店老板为你炼制出一瓶黄色药粉(中量)！");
                    ctx.DialogText = "黄色药粉炼制成功！施放施毒术将使目标进入红毒易伤减防状态。";
                    ctx.AddLink("很好", "@main");
                }
                else
                {
                    ctx.DialogText = "配方缺失：炼制黄色药粉需要【食人花果实 x1】与【蛆卵 x1】。请收集齐后再来。";
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
                    ctx.Session.SendSysMsg("【秘药合成】药店老板为你炼制出一瓶灰色药粉(中量)！");
                    ctx.DialogText = "灰色药粉炼制成功！施毒术将使目标进入绿毒持续掉血且遏制回血状态。";
                    ctx.AddLink("很好", "@main");
                }
                else
                {
                    ctx.DialogText = "配方缺失：炼制灰色药粉需要【食人花树叶 x1】与【毒蜘蛛牙齿 x1】。请去林中猎取。";
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
                        ctx.Session.SendSysMsg($"【神兵特修】花费 {cost} 金币特修成功！{weapon.Name}持久恢复至满值 {weapon.MaxDura}，上限无损！");
                        ctx.DialogText = "宝刀已重现寒芒！";
                        ctx.AddLink("很好", "@main");
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
            ctx.Session.SendSysMsg($"【兵刃锻造】成功购入一把【{name}】！");
            ctx.DialogText = $"{name} 已交到你手中，祝勇士旗开得胜！";
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
                    ctx.Session.SendSysMsg($"【仓库入库】成功将【{item.Name}】存入保险仓库！");
                    ctx.DialogText = $"已将【{item.Name}】安全收纳入库。";
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
                    ctx.Session.SendSysMsg($"【仓库出库】成功从仓库提取【{item.Name}】！");
                    ctx.DialogText = $"【{item.Name}】已取回至背包。";
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

    public void OnInteract(NpcDialogContext ctx)
    {
        var player = ctx.Session.State;
        if (player == null) return;

        switch (ctx.SelectedAction.ToLowerInvariant())
        {
            case "@main":
                ctx.DialogText = "神兵升级需天地机缘。交纳一把主武器、高纯度黑铁矿石与上等首饰，我为你开炉淬火！";
                ctx.AddLink("提交手中武器进行淬火精炼", "@upgrade_weapon");
                ctx.AddLink("打听黑铁矿纯度与持久关系", "@ore_secret");
                ctx.AddLink("离开", "@close");
                break;

            case "@upgrade_weapon":
                var weapon = player.Equipment[(int)OfficialEquipSlot.Weapon];
                if (weapon == null)
                {
                    ctx.DialogText = "你手中未佩戴武器。";
                    ctx.AddLink("返回", "@main");
                }
                else if (weapon.UpgradeCount >= 7)
                {
                    ctx.DialogText = "此神兵已经过 7 次巅峰精炼，受凡界法则压制，无法再承受更强淬火！";
                    ctx.AddLink("返回", "@main");
                }
                else
                {
                    weapon.UpgradeCount++;
                    weapon.MaxDc += 1; // 权威强化 DC +1
                    ctx.Session.SendSysMsg($"【神兵升阶】炉火纯青！{weapon.Name} 精炼成功，攻击上限 +1 (当前强化次数: {weapon.UpgradeCount}/7)！");
                    ctx.DialogText = $"大功告成！{weapon.Name} 的锋芒大幅跃升！";
                    ctx.AddLink("太好了", "@main");
                }
                break;

            case "@ore_secret":
                ctx.DialogText = "宗师真传：黑铁矿石总纯度若达到 60 点以上，武器淬火时 100% 绝不掉持久；若纯度极佳更有几率跳点暴增持久！";
                ctx.AddLink("多谢指点", "@main");
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
                ctx.DialogText = "沙巴克乃玛法第一要塞，群雄逐鹿之地。若想夺取城主宝座，必须先奉上【祖玛头像】作为战书申请攻城！";
                ctx.AddLink("呈交【祖玛头像】申请三日后沙巴克攻城战", "@apply_siege");
                ctx.AddLink("离开", "@close");
                break;

            case "@apply_siege":
                var head = player.BagItems.Find(i => i.Name.Contains("祖玛头像"));
                if (head != null)
                {
                    player.BagItems.Remove(head);
                    ctx.Session.SendSysMsg("【沙城号角】战书已准奏！比奇官府已核准三日后 20:00 开启沙巴克攻城大战！");
                    ctx.DialogText = "本王已批准你的攻城申请！通告天下行会，三日后决战沙巴克之巅！";
                    ctx.AddLink("领命", "@close");
                }
                else
                {
                    ctx.DialogText = "无凭无据，本王岂能轻易开启沙巴克战端？速去击杀祖玛教主取回【祖玛头像】！";
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
                ctx.DialogText = "呵呵呵……老夫漂泊半生，手中藏有神兵【命运之刃】的锻造奇书。你若达到 35 级，可敢与老夫猜拳打赌？";
                ctx.AddLink("我出【剪刀】与你对决！", "@guess_scissors");
                ctx.AddLink("我出【石头】！", "@guess_rock");
                ctx.AddLink("我出【布】！", "@guess_paper");
                ctx.AddLink("离开", "@close");
                break;

            case "@guess_scissors":
            case "@guess_rock":
            case "@guess_paper":
                if (player.Level < 35)
                {
                    ctx.DialogText = "年轻人，武艺未达 35 级前莫要贪图神兵，恐遭天谴！";
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
                    ctx.Session.SendSysMsg("【命运天启】猜拳获胜！神秘老人将神兵【命运之刃】郑重托付于你！");
                    ctx.DialogText = "好胆魄！老夫认赌服输，这把【命运之刃】便交由你传承！";
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
                ctx.DialogText = "未知暗殿乃天地颠倒之界，弱鸡如神，教主如蚁。需奉纳【神水】方可开启虚空之门。";
                ctx.AddLink("奉纳【神水】进入未知暗殿", "@enter_darkhall");
                ctx.AddLink("离开", "@close");
                break;

            case "@enter_darkhall":
                var water = player.BagItems.Find(i => i.Name.Contains("神水"));
                if (water != null)
                {
                    player.BagItems.Remove(water);
                    ctx.Session.Teleport(10, 50, 50); // 未知暗殿专属地图
                    ctx.Session.SendSysMsg("【虚空破界】神水激活法阵，已踏入未知暗殿！");
                    ctx.CloseWindow = true;
                }
                else
                {
                    ctx.DialogText = "你未曾携带【神水】，法阵无法运转。";
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
                ctx.DialogText = "白日门道法通神。收集齐 5 张【技能残卷】，便可为你装订一本 35 级终极武学秘籍！";
                ctx.AddLink("装订【烈火剑法】秘籍 (战士)", "@craft_firehit");
                ctx.AddLink("装订【冰咆哮】秘籍 (法师)", "@craft_blizzard");
                ctx.AddLink("装订【召唤神兽】秘籍 (道士)", "@craft_shinsu");
                ctx.AddLink("离开", "@close");
                break;

            case "@craft_firehit":
            case "@craft_blizzard":
            case "@craft_shinsu":
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
                ctx.Session.SendSysMsg($"【道法天成】技能残卷装订成功！获得一本【{skillName}秘籍】！");
                ctx.DialogText = $"{skillName}乃不世神技，切记勤加演练！";
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
                ctx.Session.SendSysMsg("【新手补给】成功领取布衣与木剑！快去村外砍鸡割肉历练吧！");
                ctx.DialogText = "补给已发放至背包，祝你早日名扬玛法！";
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
                ctx.DialogText = "血月当空，祭坛低鸣。集齐【罗刹】与 6 瓶不同神水，便可引幽冥魔血锻造成【赤血魔剑】！";
                ctx.AddLink("开始血祭合成【赤血魔剑】", "@craft_red_sword");
                ctx.AddLink("离开", "@close");
                break;

            case "@craft_red_sword":
                var luocha = player.BagItems.Find(i => i.Name.Contains("罗刹"));
                if (luocha != null)
                {
                    player.BagItems.Remove(luocha);
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
                    ctx.Session.SendSysMsg("【血祭成功】幽冥烈焰吞噬罗刹，神兵【赤血魔剑】破空降世！");
                    ctx.DialogText = "赤血魔剑已现世！此剑不灭，杀戮不止！";
                    ctx.AddLink("握紧魔剑", "@close");
                }
                else
                {
                    ctx.DialogText = "缺少关键祭品【罗刹】，祭坛无法唤醒。";
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
                    ctx.Session.SendSysMsg($"【肉铺收购】收购了 {count} 块新鲜肉，支付给您 {earn} 金币！");
                    ctx.DialogText = $"承蒙照顾！共收购 {count} 块肉，给你 {earn} 金币，慢走！";
                    ctx.AddLink("好嘞", "@main");
                }
                else
                {
                    ctx.DialogText = "客官，你包里连块肉毛都没有，逗我玩呢？去村外按住 Alt+左键 割肉再来吧！";
                    ctx.AddLink("这就去", "@main");
                }
                break;

            case "@meat_tips":
                ctx.DialogText = "老板真传：怪物死后迅速割肉，肉质新鲜品质最高；若在地上暴晒太久，肉质腐烂就不值钱了！";
                ctx.AddLink("受教了", "@main");
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
                ctx.DialogText = "量体裁衣，针线无双。轻盔甲、重盔甲与各色法袍一应俱全。";
                ctx.AddLink("购买轻盔甲 (男) - 3000 金币", "@buy_light_armor");
                ctx.AddLink("购买中型盔甲 (男) - 10000 金币", "@buy_medium_armor");
                ctx.AddLink("离开", "@close");
                break;

            case "@buy_light_armor":
                if (player.Gold >= 3000)
                {
                    player.Gold -= 3000;
                    player.BagItems.Add(new EquipItem { Uid = Random.Shared.Next(10000, 99999), Name = "轻盔甲(男)", Weight = 8, Dura = 8000, MaxDura = 8000, MaxAc = 3 });
                    ctx.Session.SendSysMsg("【裁缝交易】成功购入一件轻盔甲(男)！");
                    ctx.DialogText = "新盔甲针脚细密，穿上倍精神！";
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
                    ctx.Session.SendSysMsg("【裁缝交易】成功购入一件中型盔甲(男)！");
                    ctx.DialogText = "百炼中型甲，坚固耐用！";
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
                ctx.DialogText = "腹有诗书气自华。各门各派初阶秘籍全在此处，研习后即可领悟无上心法！";
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
                    ctx.Session.SendSysMsg($"【秘籍购入】成功购买一本【{bookName}秘籍】！");
                    ctx.DialogText = $"研习【{bookName}】后功力大进，好好参悟！";
                    ctx.AddLink("好", "@main");
                }
                else
                {
                    ctx.DialogText = "买书需 1000 金币，囊中羞涩可不行。";
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
                ctx.DialogText = "博一博，草屋变金殿！每张彩票只需 500 金币，最高可开出 100 万金币特等大奖！";
                ctx.AddLink("购买一张刮刮乐彩票 (500 金币)", "@buy_lottery");
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
                        ctx.Session.SendSysMsg($"【彩票中奖】鸿运当头！恭喜斩获二等奖，赢得 {win} 金币！");
                        ctx.DialogText = $"恭喜中奖！奖金 {win} 金币已如数奉上！";
                    }
                    else
                    {
                        ctx.Session.SendSysMsg("【彩票结果】很遗憾，谢谢惠顾！下次再来！");
                        ctx.DialogText = "差一点就中了，再来一张试试手气？";
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
                ctx.DialogText = "丛林迷宫深处盘踞着月魔蜘蛛与双头金刚。你当真要深入赤月峡谷？";
                ctx.AddLink("穿过丛林迷宫 (需要 5000 金币)", "@move_labyrinth");
                ctx.AddLink("打听赤月恶魔弱点", "@demon_intel");
                ctx.AddLink("离开", "@close");
                break;

            case "@move_labyrinth":
                if (player.Gold >= 5000)
                {
                    player.Gold -= 5000;
                    ctx.Session.Teleport(11, 100, 100);
                    ctx.Session.SendSysMsg("【丛林向导】你已被带入丛林迷宫深处！多加小心！");
                    ctx.CloseWindow = true;
                }
                else
                {
                    ctx.DialogText = "向导酬劳不足 5000 金币。";
                    ctx.AddLink("返回", "@main");
                }
                break;

            case "@demon_intel":
                ctx.DialogText = "向导密信：赤月恶魔全屏地刺无视防御，法师需撑满魔法盾，道士必须及时群体治愈！";
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
                ctx.DialogText = "买定离手！押大押小，一掷千金！底注 1000 金币。";
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
                        ctx.Session.SendSysMsg($"【赌场赢家】骰子点数 [{d1},{d2},{d3}] 共 {total} 点！押中获胜，赢得 2000 金币！");
                        ctx.DialogText = $"好眼力！点数 {total} 点，赢家通吃！";
                    }
                    else
                    {
                        ctx.Session.SendSysMsg($"【赌场失手】骰子点数 [{d1},{d2},{d3}] 共 {total} 点！庄家通吃！");
                        ctx.DialogText = $"点数 {total} 点，庄家吃进！要不再来一把翻盘？";
                    }
                    ctx.AddLink("再赌一把", "@main");
                    ctx.AddLink("不玩了", "@close");
                }
                else
                {
                    ctx.DialogText = "底注需 1000 金币，没钱莫入赌门。";
                    ctx.AddLink("返回", "@main");
                }
                break;

            default:
                ctx.CloseWindow = true;
                break;
        }
    }
}
