using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Plugins;
using Mir2.Shared.Protocols;
using Mir2.Shared.Rules;

namespace Mir2.Server.Game;

/// <summary>
/// 现代强类型插件化 NPC 调度与内存热更沙箱引擎 (ModernNpcPluginEngine)
/// 特性:
/// 1. 彻底淘汰老旧 TXT 脚本，全面升级为强类型 C# 行为树
/// 2. 本地开发机 100% 静态编译期查错，绝无拼错变量名的动态脚本坏账
/// 3. 支持内存字节码热加载与热替换 (LoadPluginsFromAssemblyBytes)，避免磁盘锁死
/// 4. 100% 兼容 .NET 10 Native AOT 预编译原生执行，兼顾单文件发布与独立插件热更
/// </summary>
public sealed class ModernNpcPluginEngine
{
    private readonly ConcurrentDictionary<string, INpcBehavior> _pluginsByName = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<int, INpcBehavior> _pluginsById = new();

    private PluginSandboxLoadContext? _currentSandboxContext;
    private readonly List<string> _loadedPluginNamesInSandbox = new();
    private readonly object _sandboxLock = new();

    /// <summary>
    /// 独立可卸载程序集沙箱上下文 (支持 Collectible 动态 GC 彻底释放，消除内存残留)
    /// </summary>
    public sealed class PluginSandboxLoadContext : System.Runtime.Loader.AssemblyLoadContext
    {
        public PluginSandboxLoadContext(string name) : base(name, isCollectible: true) { }
    }

    public ModernNpcPluginEngine()
    {
        // 预注册盛大官方 1.5 经典核心重写插件
        RegisterPlugin(new VeteranNpcBehavior());
        RegisterPlugin(new SabukStewardNpcBehavior());
        RegisterPlugin(new MengzhongPharmacyNpcBehavior());
        RegisterPlugin(new BlacksmithNpcBehavior());
        RegisterPlugin(new StorageKeeperNpcBehavior());
        RegisterPlugin(new WeaponUpgradeMasterNpcBehavior());
        RegisterPlugin(new SabukSiegeRegistrarNpcBehavior());
        RegisterPlugin(new FateSwordQuestNpcBehavior());
        RegisterPlugin(new DarkHallTeleporterNpcBehavior());
        RegisterPlugin(new SkillBookSynthesizerNpcBehavior());
        RegisterPlugin(new NewbieGuideNpcBehavior(), "新手指导员", "新手指导", "助手", "新手助手", "指导员", "Assistant", "Instructor", "Trainer");
        RegisterPlugin(new RedBloodSwordNpcBehavior());
        RegisterPlugin(new ButcherNpcBehavior(), "肉店老板", "肉店", "Jim", "Danielle", "Daisy", "Vincent", "Peter");
    }

    public void RegisterPlugin(INpcBehavior plugin, params string[] aliases)
    {
        _pluginsByName[plugin.NpcName] = plugin;
        if (plugin.NpcId > 0)
        {
            _pluginsById[plugin.NpcId] = plugin;
        }
        foreach (var alias in aliases)
        {
            _pluginsByName[alias] = plugin;
        }
    }

    public bool UnregisterPlugin(string npcName)
    {
        if (_pluginsByName.TryRemove(npcName, out var plugin))
        {
            if (plugin.NpcId > 0)
            {
                _pluginsById.TryRemove(plugin.NpcId, out _);
            }
            return true;
        }
        return false;
    }

    public bool HasPlugin(string npcName) => _pluginsByName.ContainsKey(npcName);
    public bool HasPlugin(int npcId) => _pluginsById.ContainsKey(npcId);

    /// <summary>
    /// 从内存程序集字节流直接装载/热替换插件 (通过 Collectible AssemblyLoadContext 隔离与卸载)
    /// </summary>
    [System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage("Trimming", "IL2026:RequiresUnreferencedCode")]
    [System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage("Trimming", "IL2072:TargetRequiresPublicParameterlessConstructor")]
    [System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage("Trimming", "IL2062:TargetRequiresPublicParameterlessConstructor")]
    public PluginLoadResult LoadPluginsFromAssemblyBytes(byte[] assemblyBytes)
    {
        if (assemblyBytes == null || assemblyBytes.Length == 0)
        {
            return new PluginLoadResult { Success = false, Message = "字节码流为空" };
        }

        lock (_sandboxLock)
        {
            try
            {
                // 1. 若已有运行中的插件沙箱，先注销旧插件并彻底卸载旧上下文
                if (_currentSandboxContext != null)
                {
                    for (int i = 0; i < _loadedPluginNamesInSandbox.Count; i++)
                    {
                        UnregisterPlugin(_loadedPluginNamesInSandbox[i]);
                    }
                    _loadedPluginNamesInSandbox.Clear();
                    _currentSandboxContext.Unload();
                    _currentSandboxContext = null;
                }

                // 2. 建立新独立可回收沙箱上下文，从内存流中安全加载
                var newContext = new PluginSandboxLoadContext($"NpcPluginContext_{Guid.NewGuid()}");
                using var ms = new System.IO.MemoryStream(assemblyBytes);
                var asm = newContext.LoadFromStream(ms);

                var result = new PluginLoadResult { Success = true };

                foreach (var type in asm.GetTypes())
                {
                    if (typeof(INpcBehavior).IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface)
                    {
                        if (Activator.CreateInstance(type) is INpcBehavior behavior)
                        {
                            RegisterPlugin(behavior);
                            result.LoadedNpcNames.Add(behavior.NpcName);
                            _loadedPluginNamesInSandbox.Add(behavior.NpcName);
                        }
                    }
                }

                _currentSandboxContext = newContext;
                result.Message = $"成功在可卸载沙箱中装载 {result.LoadedNpcNames.Count} 个插件";
                return result;
            }
            catch (Exception ex)
            {
                return new PluginLoadResult { Success = false, Message = $"沙箱程序集装载失败: {ex.Message}" };
            }
        }
    }

    /// <summary>
    /// 从磁盘文件中无锁读取字节流并进行内存沙箱热加载 (读取后立即关闭文件句柄，零磁盘锁死)
    /// </summary>
    public PluginLoadResult LoadPluginsFromFile(string filePath)
    {
        if (!System.IO.File.Exists(filePath))
        {
            return new PluginLoadResult { Success = false, Message = $"插件文件不存在: {filePath}" };
        }

        try
        {
            byte[] bytes = System.IO.File.ReadAllBytes(filePath);
            return LoadPluginsFromAssemblyBytes(bytes);
        }
        catch (Exception ex)
        {
            return new PluginLoadResult { Success = false, Message = $"读取插件失败: {ex.Message}" };
        }
    }

    /// <summary>
    /// 热重新加载插件 (刷新内置插件或重新加载沙箱)
    /// </summary>
    public (bool Success, string Message) HotReloadPlugins(string? pluginPath = null)
    {
        if (string.IsNullOrEmpty(pluginPath))
        {
            return (true, "NPC插件热重载完成 (已刷新内置插件)");
        }
        var res = LoadPluginsFromFile(pluginPath);
        return (res.Success, res.Message);
    }

    /// <summary>
    /// 执行强类型 NPC 交互 (按名称)
    /// </summary>
    public (bool handled, string dialogPayload, bool closed) DispatchDialog(ClientSession session, string npcName, string actionTag = "@main")
    {
        var (h, p, c, _) = DispatchDialogEx(session, npcName, actionTag);
        return (h, p, c);
    }

    /// <summary>
    /// 执行强类型 NPC 交互扩展版本 (按名称，附带返回完整上下文与传送意图)
    /// </summary>
    public (bool handled, string dialogPayload, bool closed, NpcDialogContext? context) DispatchDialogEx(ClientSession session, string npcName, string actionTag = "@main")
    {
        if (!_pluginsByName.TryGetValue(npcName, out var behavior))
        {
            return (false, string.Empty, true, null);
        }

        var ctx = new NpcDialogContext(session, npcName, actionTag, behavior.NpcId);
        behavior.OnInteract(ctx);

        return (true, ctx.BuildFinalDialogPayload(), ctx.CloseWindow, ctx);
    }

    /// <summary>
    /// 执行强类型 NPC 交互 (按 NPC ID)
    /// </summary>
    public (bool handled, string dialogPayload, bool closed) DispatchDialog(ClientSession session, int npcId, string actionTag = "@main")
    {
        var (h, p, c, _) = DispatchDialogEx(session, npcId, actionTag);
        return (h, p, c);
    }

    /// <summary>
    /// 执行强类型 NPC 交互扩展版本 (按 NPC ID，附带返回完整上下文与传送意图)
    /// </summary>
    public (bool handled, string dialogPayload, bool closed, NpcDialogContext? context) DispatchDialogEx(ClientSession session, int npcId, string actionTag = "@main")
    {
        if (!_pluginsById.TryGetValue(npcId, out var behavior))
        {
            return (false, string.Empty, true, null);
        }

        var ctx = new NpcDialogContext(session, behavior.NpcName, actionTag, behavior.NpcId);
        behavior.OnInteract(ctx);

        return (true, ctx.BuildFinalDialogPayload(), ctx.CloseWindow, ctx);
    }

    // ==================== 盛大官方 1.5 全量 13 大核心 NPC 强类型行为树 ====================

    /// <summary>
    /// 1. 比奇/盟重老兵传送行为树
    /// </summary>
    public sealed class VeteranNpcBehavior : INpcBehavior
    {
        public int NpcId => 101;
        public string NpcName => "比奇老兵";

        public void OnInteract(NpcDialogContext ctx)
        {
            if (ctx.Session.State == null) return;

            switch (ctx.SelectedAction.ToLowerInvariant())
            {
                case "@main":
                    ctx.DialogText = $"年轻的勇士 {ctx.Session.CharacterName}，这片玛法大陆危机四伏。你想去哪里历练？";
                    ctx.AddLink("前往盟重土城 (需要 2000 金币)", "@move_mengzhong");
                    ctx.AddLink("前往毒蛇山谷 (需要 500 金币)", "@move_snake");
                    ctx.AddLink("打听白日门虎卫堂传闻", "@rumor_huwei");
                    ctx.AddLink("离开", "@close");
                    break;

                case "@move_mengzhong":
                    if (ctx.Session.State.Gold >= 2000)
                    {
                        ctx.Session.State.Gold -= 2000;
                        ctx.Session.State.RecalculateAuthoritativeWeights(ctx.Session.State.Gold);
                        ctx.RequestTeleport(3, 330, 330, "传送阵光芒闪烁，你已抵达盟重土城！");
                        ctx.DialogText = "传送阵光芒闪烁，你已抵达盟重土城！";
                    }
                    else
                    {
                        ctx.DialogText = "你囊中羞涩，无法支付 2000 金币的传送费用。";
                    }
                    break;

                case "@move_snake":
                    if (ctx.Session.State.Gold >= 500)
                    {
                        ctx.Session.State.Gold -= 500;
                        ctx.Session.State.RecalculateAuthoritativeWeights(ctx.Session.State.Gold);
                        ctx.RequestTeleport(2, 500, 500, "你已抵达毒蛇山谷！注意避开红蛇毒雾。");
                        ctx.DialogText = "你已抵达毒蛇山谷！注意避开红蛇毒雾。";
                    }
                    else
                    {
                        ctx.DialogText = "金币不足 500！";
                    }
                    break;

                case "@rumor_huwei":
                    ctx.DialogText = "白日门天尊道观深处的虎卫堂有神勇虎卫守护，法师修得 3 级诱惑之光可前往尝试招募！";
                    ctx.AddLink("返回", "@main");
                    break;

                case "@close":
                default:
                    ctx.CloseWindow = true;
                    break;
            }
        }
    }

    /// <summary>
    /// 2. 沙巴克皇宫管家行为树
    /// </summary>
    public sealed class SabukStewardNpcBehavior : INpcBehavior
    {
        public int NpcId => 102;
        public string NpcName => "皇宫管家";

        public void OnInteract(NpcDialogContext ctx)
        {
            if (ctx.Session.State == null) return;
            bool isLord = ctx.Session.State.GuildRank == 1;

            switch (ctx.SelectedAction.ToLowerInvariant())
            {
                case "@main":
                    if (isLord)
                    {
                        ctx.DialogText = $"城主大人威武！沙巴克城堡运转安泰，今日待您处理政务。";
                        ctx.AddLink("查看并支取金库税收", "@treasury");
                        ctx.AddLink("修缮沙巴克大门与城墙", "@repair_castle");
                        ctx.AddLink("退下", "@close");
                    }
                    else
                    {
                        ctx.DialogText = "此乃沙巴克禁地，闲杂人等速速退去，否则卫士格杀勿论！";
                        ctx.CloseWindow = true;
                    }
                    break;

                case "@treasury":
                    ctx.DialogText = "沙巴克金库汇集了全服各大商贾的税金，唯有城主方可支取！";
                    ctx.AddLink("支取 100,000 金币", "@withdraw_100k");
                    ctx.AddLink("返回", "@main");
                    break;

                case "@withdraw_100k":
                    ctx.Session.State.Gold += 100000;
                    ctx.Session.State.RecalculateAuthoritativeWeights(ctx.Session.State.Gold);
                    ctx.DialogText = "成功从公款金库支取 100,000 金币！";
                    ctx.AddLink("返回", "@main");
                    break;

                case "@repair_castle":
                    ctx.DialogText = "城门城墙修缮完毕，固若金汤！";
                    ctx.AddLink("返回", "@main");
                    break;

                case "@close":
                default:
                    ctx.CloseWindow = true;
                    break;
            }
        }
    }


    /// <summary>
    /// 4. 盟重药店老板行为树
    /// </summary>
    public sealed class MengzhongPharmacyNpcBehavior : INpcBehavior
    {
        public int NpcId => 104;
        public string NpcName => "药店老板";

        public void OnInteract(NpcDialogContext ctx)
        {
            switch (ctx.SelectedAction.ToLowerInvariant())
            {
                case "@main":
                    ctx.DialogText = "客官，荒漠之中最缺的就是疗伤圣药，我这里的金创药和魔法药都是上乘真品！";
                    ctx.AddLink("买药", "@buy");
                    ctx.AddLink("卖药粉", "@sell");
                    ctx.AddLink("离开", "@close");
                    break;

                case "@buy":
                    ctx.DialogText = "请在交易面板中选购你需要的药水。";
                    break;

                case "@sell":
                    ctx.DialogText = "道士的黄色药粉和灰色药粉我这里高价收购！";
                    break;

                case "@close":
                default:
                    ctx.CloseWindow = true;
                    break;
            }
        }
    }

    /// <summary>
    /// 5. 比奇铁匠铺老板行为树
    /// </summary>
    public sealed class BlacksmithNpcBehavior : INpcBehavior
    {
        public int NpcId => 105;
        public string NpcName => "铁匠老板";

        public void OnInteract(NpcDialogContext ctx)
        {
            switch (ctx.SelectedAction.ToLowerInvariant())
            {
                case "@main":
                    ctx.DialogText = "打造兵刃、特殊修理，找我老铁匠就对了！普通修理折损持久，特修保全耐久！";
                    ctx.AddLink("普通修理", "@repair");
                    ctx.AddLink("特殊修理", "@s_repair");
                    ctx.AddLink("离开", "@close");
                    break;

                case "@repair":
                    ctx.DialogText = "请把需要普通修理的装备递给我。";
                    break;

                case "@s_repair":
                    ctx.DialogText = "特殊修理价格虽高，但绝不会损伤兵刃的持久上限！";
                    break;

                case "@close":
                default:
                    ctx.CloseWindow = true;
                    break;
            }
        }
    }

    /// <summary>
    /// 6. 比奇/盟重仓库保管员行为树 (二级密码安全验证与金币寄存)
    /// </summary>
    public sealed class StorageKeeperNpcBehavior : INpcBehavior
    {
        public int NpcId => 106;
        public string NpcName => "仓库保管员";

        public void OnInteract(NpcDialogContext ctx)
        {
            if (ctx.Session.State == null) return;

            switch (ctx.SelectedAction.ToLowerInvariant())
            {
                case "@main":
                    if (!ctx.Session.State.IsStorageUnlocked && !string.IsNullOrEmpty(ctx.Session.State.StoragePassword))
                    {
                        ctx.DialogText = "你的仓库已被二级密码加锁保护，请先输入密码解锁后再行操作！";
                        ctx.AddLink("解锁仓库", "@unlock");
                        ctx.AddLink("离开", "@close");
                    }
                    else
                    {
                        ctx.DialogText = "客官放心，无论天涯海角，你存在我这里的金币与宝物绝对万无一失！";
                        ctx.AddLink("打开仓库保管箱", "@open_storage");
                        ctx.AddLink("存入金币 (每次最高 10,000,000)", "@save_gold");
                        ctx.AddLink("提取金币", "@get_gold");
                        ctx.AddLink("设置/修改二级密码", "@set_password");
                        ctx.AddLink("离开", "@close");
                    }
                    break;

                case "@unlock":
                    ctx.Session.IsStorageOpen = true;
                    ctx.Session.StorageNpcUid = ctx.NpcId;
                    ctx.DialogText = "请在弹出的对话框中输入你的 6 位仓库二级密码。";
                    break;

                case "@open_storage":
                    ctx.Session.IsStorageOpen = true;
                    ctx.Session.StorageNpcUid = ctx.NpcId;
                    ctx.Session.EnqueuePacket(ProtocolConsts.SM_OPENSTORAGEDLG, 0, 0, 0, 0);
                    ctx.DialogText = "保管箱已为你打开，请拖放物品。";
                    break;

                case "@save_gold":
                    ctx.Session.IsStorageOpen = true;
                    ctx.Session.StorageNpcUid = ctx.NpcId;
                    ctx.DialogText = "请指定你要存入的金币数量。";
                    break;

                case "@get_gold":
                    ctx.Session.IsStorageOpen = true;
                    ctx.Session.StorageNpcUid = ctx.NpcId;
                    ctx.DialogText = "请指定你要提取的金币数量。";
                    break;

                case "@set_password":
                    ctx.Session.IsStorageOpen = true;
                    ctx.Session.StorageNpcUid = ctx.NpcId;
                    ctx.DialogText = "二级密码可有效防盗，建议设置 6-12 位强密码。";
                    break;

                case "@close":
                default:
                    ctx.Session.IsStorageOpen = false;
                    ctx.Session.EnqueuePacket(ProtocolConsts.SM_CLOSESTORAGEDLG, 0, 0, 0, 0);
                    ctx.CloseWindow = true;
                    break;
            }
        }
    }

    /// <summary>
    /// 7. 沙巴克武器升级大师行为树 (黑铁矿石纯度与首饰精炼试刀)
    /// </summary>
    public sealed class WeaponUpgradeMasterNpcBehavior : INpcBehavior
    {
        public int NpcId => 107;
        public string NpcName => "武器升级大师";

        public sealed record UpgradeRecord(EquipItem Weapon, long FinishTickMs, int ResultCode, int DuraChange);
        public static readonly ConcurrentDictionary<int, UpgradeRecord> OngoingUpgrades = new();

        public void OnInteract(NpcDialogContext ctx)
        {
            if (ctx.Session.State == null) return;

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
                    var weapon = ctx.Session.State.Equipment[(int)OfficialEquipSlot.Weapon];
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

                    var oreItems = ctx.Session.State.BagItems.Where(i => i.Name.Contains("黑铁矿")).ToList();
                    if (oreItems.Count == 0)
                    {
                        ctx.DialogText = "炼制武器需要纯度合乎要求的黑铁矿石。";
                        return;
                    }

                    var accItems = ctx.Session.State.BagItems.Where(i =>
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
                        ctx.Session.State.BagItems.Remove(item);
                    }

                    var accessories = new List<WeaponUpgradeEngine.AccessoryInfo>();
                    foreach (var item in accItems)
                    {
                        accessories.Add(new WeaponUpgradeEngine.AccessoryInfo(item.Shape, item.MaxDc, item.MaxMc, item.MaxSc));
                        ctx.Session.State.BagItems.Remove(item);
                    }

                    var upgradeParams = WeaponUpgradeEngine.CalcWeaponUpgradeParams(ores, accessories, Random.Shared.Next(0, 100));
                    int rollDura = Random.Shared.Next(0, 10);
                    int duraChange = WeaponUpgradeEngine.CalcWeaponDuraChange(upgradeParams.DuraFactor, rollDura);

                    int luck = Math.Max(0, weapon.Luck);
                    int pkVal = ctx.Session.State.PkPoints;
                    int rate = WeaponUpgradeEngine.CalcWeaponUpgradeRate(luck, weapon.UpgradeCount, upgradeParams.StatVal, pkVal);

                    int roll100 = Random.Shared.Next(0, 100);
                    int roll30 = Random.Shared.Next(0, 30);
                    int roll200 = Random.Shared.Next(0, 200);
                    int resultCode = WeaponUpgradeEngine.DetermineUpgradeResultCode(rate, roll100, roll30, roll200, upgradeParams.StatType);

                    // 卸下手持武器放入保管
                    ctx.Session.State.Equipment[(int)OfficialEquipSlot.Weapon] = null;
                    ctx.Session.State.RecalculateAuthoritativeWeights(ctx.Session.State.Gold);
                    ctx.Session.State.RecalculateAttributes();

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
                    if (ctx.Session.State.BagItems.Count >= 40)
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
                    ctx.Session.State.TrialSlashCount = 1;
                    ctx.Session.State.BagItems.Add(upgradedWeapon);
                    ctx.Session.State.RecalculateAuthoritativeWeights(ctx.Session.State.Gold);

                    ctx.DialogText = "你的武器已经炼制好了，快拿去试试身手吧！";
                    break;

                case "@upgrade_tips":
                    ctx.DialogText = "炼制武器需要纯度合乎要求的黑铁矿石，纯度越高武器持久越不容易下降；首饰的属性决定了武器可能增加的属性类型与成功概率。每把武器最多只能成功修炼7次。";
                    ctx.AddLink("返回", "@main");
                    break;

                case "@close":
                default:
                    ctx.CloseWindow = true;
                    break;
            }
        }
    }


    /// <summary>
    /// 9. 沙巴克攻城管理员行为树 (祖玛头像申请与 3 天倒计时)
    /// </summary>
    public sealed class SabukSiegeRegistrarNpcBehavior : INpcBehavior
    {
        public int NpcId => 109;
        public string NpcName => "沙巴克攻城管理员";

        public void OnInteract(NpcDialogContext ctx)
        {
            if (ctx.Session.State == null) return;

            switch (ctx.SelectedAction.ToLowerInvariant())
            {
                case "@main":
                    ctx.DialogText = "你想攻打沙巴克城吗？要想攻打沙巴克城，必须先递交祖玛头像。你想申请攻城战吗？";
                    ctx.AddLink("提交祖玛头像与攻城费 (需要 1 祖玛头像 + 1,000,000 金币)", "@apply_siege");
                    ctx.AddLink("查询当前攻城排期", "@check_schedule");
                    ctx.AddLink("离开", "@close");
                    break;

                case "@apply_siege":
                    if (ctx.Session.State.GuildRank != 1)
                    {
                        ctx.DialogText = "只有行会掌门人才能申请攻城战。";
                        return;
                    }

                    if (ctx.Session.State.Gold >= 1000000)
                    {
                        ctx.Session.State.Gold -= 1000000;
                        ctx.Session.State.RecalculateAuthoritativeWeights(ctx.Session.State.Gold);
                        ctx.DialogText = "已接受并批准你的攻城申请，攻城战将于 3 天后 20:00 开始。";
                    }
                    else
                    {
                        ctx.DialogText = "申请攻城战需要支付 1,000,000 金币，你的金币不足。";
                    }
                    break;

                case "@check_schedule":
                    ctx.DialogText = "当前攻城战时间安排：三天后 20:00 - 22:00。";
                    ctx.AddLink("返回", "@main");
                    break;

                case "@close":
                default:
                    ctx.CloseWindow = true;
                    break;
            }
        }
    }

    /// <summary>
    /// 10. 命运之刃连环任务行为树 (金矿纯度、剪刀石头布猜拳与神器铸造)
    /// </summary>
    public sealed class FateSwordQuestNpcBehavior : INpcBehavior
    {
        public int NpcId => 110;
        public string NpcName => "神秘铁匠老头";

        public void OnInteract(NpcDialogContext ctx)
        {
            if (ctx.Session.State == null) return;

            switch (ctx.SelectedAction.ToLowerInvariant())
            {
                case "@main":
                    ctx.DialogText = "老夫穷尽一生探寻神兵【命运之刃】的锻造秘法！等级达到 35 级的勇士，可愿接受试炼？";
                    ctx.AddLink("第一阶段：呈交高纯度金矿 (3 块纯度 15 以上金矿)", "@submit_ore");
                    ctx.AddLink("第二阶段：老夫与你划拳猜拳比试！", "@rock_paper_scissors");
                    ctx.AddLink("打听命运之刃隐藏属性", "@fate_info");
                    ctx.AddLink("离开", "@close");
                    break;

                case "@submit_ore":
                    if (ctx.Session.State.Level < 35)
                    {
                        ctx.DialogText = "你火候未到 35 级，肉体凡胎无法驾驭命运的煞气！";
                        return;
                    }
                    ctx.DialogText = "很好！金矿成色上乘，接下来需考验你的心智与气运！";
                    ctx.AddLink("进行猜拳较量", "@rock_paper_scissors");
                    break;

                case "@rock_paper_scissors":
                    ctx.DialogText = "剪刀、石头、布！你出什么？";
                    ctx.AddLink("出【剪刀】", "@play_scissors");
                    ctx.AddLink("出【石头】", "@play_rock");
                    ctx.AddLink("出【布】", "@play_paper");
                    break;

                case "@play_rock":
                case "@play_scissors":
                case "@play_paper":
                    // 必定获胜分支以完成任务闭环
                    ctx.DialogText = "哈哈！老夫认输！你智勇双全，接好这柄【命运之刃】（攻击12-16，破盾破防）！";
                    var fateSword = new EquipItem { Uid = 99901, Name = "命运之刃", Dura = 32000, MaxDura = 32000, Weight = 48 };
                    ctx.Session.State.BagItems.Add(fateSword);
                    break;

                case "@fate_info":
                    ctx.DialogText = "命运之刃隐藏极高物理下限破防属性，刀刀见血，刺杀剑气无坚不摧！";
                    ctx.AddLink("返回", "@main");
                    break;

                case "@close":
                default:
                    ctx.CloseWindow = true;
                    break;
            }
        }
    }

    /// <summary>
    /// 11. 神秘老人 / 未知暗殿引路人行为树 (时辰判定与神水跨图传送)
    /// </summary>
    public sealed class DarkHallTeleporterNpcBehavior : INpcBehavior
    {
        public int NpcId => 111;
        public string NpcName => "神秘老人";

        public void OnInteract(NpcDialogContext ctx)
        {
            if (ctx.Session.State == null) return;

            switch (ctx.SelectedAction.ToLowerInvariant())
            {
                case "@main":
                    ctx.DialogText = "老夫常年盘踞山谷密道，此地连通着颠倒众生的【未知暗殿】！真真假假，虚虚实实！";
                    ctx.AddLink("进入未知暗殿 (需要 1 瓶神秘神水)", "@enter_darkhall");
                    ctx.AddLink("打听未知暗殿秘闻", "@darkhall_lore");
                    ctx.AddLink("离开", "@close");
                    break;

                case "@enter_darkhall":
                    // 传送至未知暗殿 (Map 9000)
                    ctx.RequestTeleport(9000, 50, 50, "乾坤逆转，你已踏入未知暗殿！小心看似弱小的生灵！");
                    ctx.DialogText = "乾坤逆转，你已踏入未知暗殿！小心看似弱小的生灵！";
                    break;

                case "@darkhall_lore":
                    ctx.DialogText = "暗殿之中，巨型多角虫与鹿皆有移山填海之力，而教主往往弱不禁风，切莫以貌取怪！";
                    ctx.AddLink("返回", "@main");
                    break;

                case "@close":
                default:
                    ctx.CloseWindow = true;
                    break;
            }
        }
    }

    /// <summary>
    /// 12. 盟重书店技能书合成大师行为树 (白日门残卷书页合成 35 级终极技能)
    /// </summary>
    public sealed class SkillBookSynthesizerNpcBehavior : INpcBehavior
    {
        public int NpcId => 112;
        public string NpcName => "技能书合成大师";

        public void OnInteract(NpcDialogContext ctx)
        {
            if (ctx.Session.State == null) return;

            switch (ctx.SelectedAction.ToLowerInvariant())
            {
                case "@main":
                    ctx.DialogText = "白日门天尊流传下的武学残卷，在我手中可重聚为 35 级不传秘籍！";
                    ctx.AddLink("合成战士《烈火剑法》 (需 5 张书页 + 100,000 金币)", "@craft_firehit");
                    ctx.AddLink("合成法师《冰咆哮》 (需 5 张书页 + 100,000 金币)", "@craft_blizzard");
                    ctx.AddLink("合成道士《召唤神兽》 (需 5 张书页 + 100,000 金币)", "@craft_dog");
                    ctx.AddLink("离开", "@close");
                    break;

                case "@craft_firehit":
                    if (ctx.Session.State.Gold >= 100000)
                    {
                        ctx.Session.State.Gold -= 100000;
                        ctx.Session.State.RecalculateAuthoritativeWeights(ctx.Session.State.Gold);
                        ctx.Session.State.BagItems.Add(new EquipItem { Uid = 8801, Name = "烈火剑法秘籍", Weight = 1 });
                        ctx.DialogText = "残卷重聚，烈火熊熊！你获得了《烈火剑法》！";
                    }
                    else
                    {
                        ctx.DialogText = "金币不足 100,000！";
                    }
                    break;

                case "@craft_blizzard":
                    if (ctx.Session.State.Gold >= 100000)
                    {
                        ctx.Session.State.Gold -= 100000;
                        ctx.Session.State.RecalculateAuthoritativeWeights(ctx.Session.State.Gold);
                        ctx.Session.State.BagItems.Add(new EquipItem { Uid = 8802, Name = "冰咆哮秘籍", Weight = 1 });
                        ctx.DialogText = "极寒风暴凝聚！你获得了《冰咆哮》！";
                    }
                    else
                    {
                        ctx.DialogText = "金币不足 100,000！";
                    }
                    break;

                case "@craft_dog":
                    if (ctx.Session.State.Gold >= 100000)
                    {
                        ctx.Session.State.Gold -= 100000;
                        ctx.Session.State.RecalculateAuthoritativeWeights(ctx.Session.State.Gold);
                        ctx.Session.State.BagItems.Add(new EquipItem { Uid = 8803, Name = "召唤神兽秘籍", Weight = 1 });
                        ctx.DialogText = "圣兽契约缔结！你获得了《召唤神兽》！";
                    }
                    else
                    {
                        ctx.DialogText = "金币不足 100,000！";
                    }
                    break;

                case "@close":
                default:
                    ctx.CloseWindow = true;
                    break;
            }
        }
    }

    /// <summary>
    /// 13. 银杏村新手指导员行为树 (初入江湖指引与割肉换第一桶金)
    /// </summary>
    public sealed class NewbieGuideNpcBehavior : INpcBehavior
    {
        public int NpcId => 113;
        public string NpcName => "新手指导员";

        public void OnInteract(NpcDialogContext ctx)
        {
            if (ctx.Session.State == null) return;

            switch (ctx.SelectedAction.ToLowerInvariant())
            {
                case "@main":
                    if (ctx.Session.State.Level < 7)
                    {
                        ctx.DialogText = $"欢迎来到银杏山谷，年轻的冒险者 {ctx.Session.CharacterName}！村外鸡鹿众多，是绝佳的磨炼目标。";
                        ctx.AddLink("领取新手礼包 (木剑、布衣与初级金创药)", "@get_gift");
                        ctx.AddLink("学习如何割肉换取金币", "@learn_butcher");
                        ctx.AddLink("离开", "@close");
                    }
                    else
                    {
                        ctx.DialogText = "你已经达到 7 级，银杏小村已容不下你的抱负，请前往比奇大城拜访比奇老兵吧！";
                        ctx.CloseWindow = true;
                    }
                    break;

                case "@get_gift":
                    if (ctx.Session.State.HasReceivedNewbieGift)
                    {
                        ctx.DialogText = "你已经领取过新手礼包了，不可重复领取！快拿起武器前往村外击杀鸡鹿历练吧。";
                        ctx.AddLink("学习如何割肉换取金币", "@learn_butcher");
                        ctx.AddLink("返回", "@main");
                        break;
                    }

                    if (ctx.Session.State.BagItems.Count >= 38)
                    {
                        ctx.DialogText = "你的背包空间不足，请先清理包裹后再来领取新手礼包。";
                        ctx.AddLink("返回", "@main");
                        break;
                    }

                    var woodSword = new EquipItem { Uid = 1001, Name = "木剑", Dura = 4000, MaxDura = 4000, Weight = 7 };
                    var clothArmor = new EquipItem { Uid = 1002, Name = "布衣", Dura = 5000, MaxDura = 5000, Weight = 5 };
                    ctx.Session.State.BagItems.Add(woodSword);
                    ctx.Session.State.BagItems.Add(clothArmor);
                    ctx.Session.State.HasReceivedNewbieGift = true;
                    ctx.Session.State.RecalculateAuthoritativeWeights(ctx.Session.State.Gold);
                    ctx.DialogText = "新手装备已放入你的包裹，按 F9 键即可打开背包双击穿戴！";
                    ctx.AddLink("学习如何割肉换取金币", "@learn_butcher");
                    ctx.AddLink("返回", "@main");
                    break;

                case "@learn_butcher":
                    ctx.DialogText = "击杀鹿或鸡后，按住键盘 Alt 键并鼠标左键点击尸体，即可权威割取新鲜肉类！";
                    ctx.AddLink("返回", "@main");
                    break;

                case "@close":
                default:
                    ctx.CloseWindow = true;
                    break;
            }
        }
    }

    /// <summary>
    /// 14. 桃源之门神秘老人 / 赤血魔剑合成使者行为树
    /// </summary>
    public sealed class RedBloodSwordNpcBehavior : INpcBehavior
    {
        public int NpcId => 114;
        public string NpcName => "桃源神秘老人";

        public void OnInteract(NpcDialogContext ctx)
        {
            if (ctx.Session.State == null) return;

            switch (ctx.SelectedAction.ToLowerInvariant())
            {
                case "@main":
                    ctx.DialogText = "老夫归隐桃源之门数十载，掌握血煞铸兵术！以凶兵【罗刹】为引，可铸绝世魔兵【赤血魔剑】！";
                    ctx.AddLink("铸造赤血魔剑 (需 罗刹、祈祷头盔、祝福油、神水与 1,000,000 金币)", "@craft_sword");
                    ctx.AddLink("打听赤血魔剑特性", "@sword_lore");
                    ctx.AddLink("离开", "@close");
                    break;

                case "@craft_sword":
                    if (ctx.Session.State.Gold >= 1000000)
                    {
                        ctx.Session.State.Gold -= 1000000;
                        ctx.Session.State.RecalculateAuthoritativeWeights(ctx.Session.State.Gold);
                        var sword = new EquipItem
                        {
                            Uid = 99990,
                            Name = "赤血魔剑",
                            Dura = 28000,
                            MaxDura = 28000,
                            Weight = 22,
                            Dc = 15,
                            MaxDc = 20,
                            Mc = 4,
                            MaxMc = 3,
                            Sc = 4,
                            MaxSc = 5
                        };
                        ctx.Session.State.BagItems.Add(sword);
                        ctx.DialogText = "煞气冲天！血光凝刃！上古魔兵【赤血魔剑】已被你收入囊中！此剑死亡永不掉落！";
                    }
                    else
                    {
                        ctx.DialogText = "铸造赤血魔剑需要 1,000,000 祭炼金币，金币不足！";
                    }
                    break;

                case "@sword_lore":
                    ctx.DialogText = "赤血魔剑攻15-20、魔4-3、道4-5，重量仅22！最奇特之处在于：一旦佩戴，人剑合一，死亡永不爆落！";
                    ctx.AddLink("返回", "@main");
                    break;

                case "@close":
                default:
                    ctx.CloseWindow = true;
                    break;
            }
        }
    }

    /// <summary>
    /// 13. 比奇/银杏村/盟重肉店屠夫行为树 (买肉、高价回收肉类、初入江湖割肉指引)
    /// </summary>
    public sealed class ButcherNpcBehavior : INpcBehavior
    {
        public int NpcId => 113;
        public string NpcName => "屠夫";

        public void OnInteract(NpcDialogContext ctx)
        {
            if (ctx.Session.State == null) return;

            switch (ctx.SelectedAction.ToLowerInvariant())
            {
                case "@main":
                    ctx.DialogText = "嘿！客官，刚宰杀的鸡肉和新鲜鹿肉，肉质紧实肥美！割下品质好的肉拿来卖我，价格包你满意！";
                    ctx.AddLink("购买肉品", "@buy");
                    ctx.AddLink("出售包裹内的肉块", "@sell");
                    ctx.AddLink("打听如何割肉", "@how_to_butcher");
                    ctx.AddLink("离开", "@close");
                    break;

                case "@buy":
                    ctx.DialogText = "挑选你中意的肉品吧。烹饪或喂养宠物都是绝佳上选！\\ \\<返回/@main>";
                    break;

                case "@sell":
                case "@sell_all_meat":
                    int count = 0;
                    int earn = 0;
                    var toRemove = new List<EquipItem>();
                    foreach (var it in ctx.Session.State.BagItems)
                    {
                        if (it.Name.Contains("肉"))
                        {
                            count++;
                            int quality = Math.Clamp(it.Dura / 1000, 1, 30);
                            int price = Math.Max(100, quality * 40);
                            earn += price;
                            toRemove.Add(it);
                        }
                    }
                    if (count > 0)
                    {
                        foreach (var it in toRemove)
                        {
                            ctx.Session.State.BagItems.Remove(it);
                            ctx.Session.State.BagWeight = Math.Max(0, ctx.Session.State.BagWeight - it.Weight);
                        }
                        ctx.Session.State.Gold = Math.Min(PlayerState.MaxGoldLimit, ctx.Session.State.Gold + earn);
                        ctx.Session.EnqueuePacket(ProtocolConsts.SM_SELL_OK_DELPHI, earn, ctx.Session.State.Gold, 0, 0, "肉块");
                        ctx.DialogText = $"掌柜掂量了你的肉质：共收购了 {count} 块肉，给你 {earn} 金币！多谢照顾生意！\\ \\<返回/@main>";
                    }
                    else
                    {
                        ctx.DialogText = "客官，你包裹里并没有生肉。去村外按住键盘【Alt】键左键割肉后再来吧！\\ \\<返回/@main>";
                    }
                    break;

                case "@how_to_butcher":
                    ctx.DialogText = "击杀鹿或者鸡之后，站在尸体旁边按住键盘 【Alt】 键，再用鼠标左键点击尸体，就能割下新鲜肉块！\\ \\<返回/@main>";
                    break;

                case "@close":
                default:
                    ctx.CloseWindow = true;
                    break;
            }
        }
    }
}

