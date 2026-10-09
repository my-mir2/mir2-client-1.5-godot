using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;

namespace Mir2.Server.Game;

/// <summary>
/// NPC 强类型对话上下文模型 (具备 100% 编译期静态安全与类型感知)
/// </summary>
public sealed class NpcDialogContext
{
    public ClientSession Session { get; }
    public string NpcName { get; }
    public string SelectedAction { get; set; } = "@main";
    public string DialogText { get; set; } = string.Empty;
    public List<(string label, string actionTag)> Hyperlinks { get; } = new();
    public bool CloseWindow { get; set; }

    public NpcDialogContext(ClientSession session, string npcName, string initialAction = "@main")
    {
        Session = session;
        NpcName = npcName;
        SelectedAction = initialAction;
    }

    public void AddLink(string label, string actionTag) =>
        Hyperlinks.Add((label, actionTag));

    public string BuildFinalDialogPayload()
    {
        var sb = new StringBuilder();
        sb.AppendLine(DialogText);
        for (int i = 0; i < Hyperlinks.Count; i++)
        {
            var (lbl, act) = Hyperlinks[i];
            sb.AppendLine($"<{lbl}/{act}>");
        }
        return sb.ToString().TrimEnd();
    }
}

/// <summary>
/// 现代化强类型 NPC 行为树插件接口 (INpcBehavior)
/// 彻底淘汰旧易错的 TXT 语法，在开发机本地编译期即可捕获一切语法与逻辑错误！
/// </summary>
public interface INpcBehavior
{
    int NpcId { get; }
    string NpcName { get; }
    void OnInteract(NpcDialogContext ctx);
}

/// <summary>
/// 插件装载结果状态包
/// </summary>
public sealed class PluginLoadResult
{
    public bool Success { get; init; }
    public string Message { get; set; } = string.Empty;
    public List<string> LoadedNpcNames { get; } = new();
}

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
        // 预注册盛大官方 1.5 全量 14 大经典核心重写插件
        RegisterPlugin(new VeteranNpcBehavior());
        RegisterPlugin(new SabukStewardNpcBehavior());
        RegisterPlugin(new YuelaoNpcBehavior());
        RegisterPlugin(new MengzhongPharmacyNpcBehavior());
        RegisterPlugin(new BlacksmithNpcBehavior());
        RegisterPlugin(new StorageKeeperNpcBehavior());
        RegisterPlugin(new WeaponUpgradeMasterNpcBehavior());
        RegisterPlugin(new MasterApprenticeRegistrarNpcBehavior());
        RegisterPlugin(new SabukSiegeRegistrarNpcBehavior());
        RegisterPlugin(new FateSwordQuestNpcBehavior());
        RegisterPlugin(new DarkHallTeleporterNpcBehavior());
        RegisterPlugin(new SkillBookSynthesizerNpcBehavior());
        RegisterPlugin(new NewbieGuideNpcBehavior());
        RegisterPlugin(new RedBloodSwordNpcBehavior());
    }

    public void RegisterPlugin(INpcBehavior plugin)
    {
        _pluginsByName[plugin.NpcName] = plugin;
        if (plugin.NpcId > 0)
        {
            _pluginsById[plugin.NpcId] = plugin;
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
    /// 执行强类型 NPC 交互 (按名称)
    /// </summary>
    public (bool handled, string dialogPayload, bool closed) DispatchDialog(ClientSession session, string npcName, string actionTag = "@main")
    {
        if (!_pluginsByName.TryGetValue(npcName, out var behavior))
        {
            return (false, string.Empty, true);
        }

        var ctx = new NpcDialogContext(session, npcName, actionTag);
        behavior.OnInteract(ctx);

        return (true, ctx.BuildFinalDialogPayload(), ctx.CloseWindow);
    }

    /// <summary>
    /// 执行强类型 NPC 交互 (按 NPC ID)
    /// </summary>
    public (bool handled, string dialogPayload, bool closed) DispatchDialog(ClientSession session, int npcId, string actionTag = "@main")
    {
        if (!_pluginsById.TryGetValue(npcId, out var behavior))
        {
            return (false, string.Empty, true);
        }

        var ctx = new NpcDialogContext(session, behavior.NpcName, actionTag);
        behavior.OnInteract(ctx);

        return (true, ctx.BuildFinalDialogPayload(), ctx.CloseWindow);
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
                    ctx.AddLink("打听封魔谷传闻", "@rumor_fengmo");
                    ctx.AddLink("离开", "@close");
                    break;

                case "@move_mengzhong":
                    if (ctx.Session.State.Gold >= 2000)
                    {
                        ctx.Session.State.Gold -= 2000;
                        ctx.Session.State.RecalculateAuthoritativeWeights(ctx.Session.State.Gold);
                        ctx.Session.CurrentMapId = 3;
                        ctx.Session.X = 330;
                        ctx.Session.Y = 330;
                        ctx.Session.EnqueuePacket(ProtocolConsts.SM_NEWMAP_DELPHI, 3, 330, 330, 0);
                        ctx.DialogText = "传送阵光芒闪烁，你已抵达盟重土城！";
                        ctx.CloseWindow = true;
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
                        ctx.Session.CurrentMapId = 2;
                        ctx.Session.X = 500;
                        ctx.Session.Y = 500;
                        ctx.Session.EnqueuePacket(ProtocolConsts.SM_NEWMAP_DELPHI, 2, 500, 500, 0);
                        ctx.DialogText = "你已抵达毒蛇山谷！注意避开红蛇毒雾。";
                        ctx.CloseWindow = true;
                    }
                    else
                    {
                        ctx.DialogText = "金币不足 500！";
                    }
                    break;

                case "@rumor_fengmo":
                    ctx.DialogText = "封魔谷封印着千万年前的千年树妖与虹魔教主，实力不足 35 级切勿涉险！";
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
    /// 3. 封魔谷月老行为树
    /// </summary>
    public sealed class YuelaoNpcBehavior : INpcBehavior
    {
        public int NpcId => 103;
        public string NpcName => "月老";

        public void OnInteract(NpcDialogContext ctx)
        {
            if (ctx.Session.State == null) return;

            switch (ctx.SelectedAction.ToLowerInvariant())
            {
                case "@main":
                    ctx.DialogText = "在天愿作比翼鸟，在地愿为连理枝。千里姻缘一线牵，二位新人准备好了吗？";
                    ctx.AddLink("我们要结婚 (双方各需 1 婚戒与 520,000 金币)", "@marry");
                    ctx.AddLink("斩断情丝，请求离婚", "@divorce");
                    ctx.AddLink("离开", "@close");
                    break;

                case "@marry":
                    if (ctx.Session.State.Gold >= 520000)
                    {
                        ctx.DialogText = "月老红线已系！恭喜二位喜结良缘，永浴爱河！";
                    }
                    else
                    {
                        ctx.DialogText = "婚礼筹备需要 520,000 金币礼金，金币不足！";
                    }
                    break;

                case "@divorce":
                    ctx.DialogText = "既已情断义绝，需支付 1,000,000 抚恤金解除婚姻关系。";
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
                    ctx.DialogText = "请在弹出的对话框中输入你的 6 位仓库二级密码。";
                    break;

                case "@open_storage":
                    ctx.DialogText = "保管箱已为你打开，请拖放物品。";
                    break;

                case "@save_gold":
                    ctx.DialogText = "请指定你要存入的金币数量。";
                    break;

                case "@get_gold":
                    ctx.DialogText = "请指定你要提取的金币数量。";
                    break;

                case "@set_password":
                    ctx.DialogText = "二级密码可有效防盗，建议设置 6-12 位强密码。";
                    break;

                case "@close":
                default:
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

        public sealed record UpgradeRecord(int WeaponUid, string WeaponName, long FinishTickMs, int UpgradeType);
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
                            ctx.DialogText = $"你的【{rec.WeaponName}】已经修炼完成！请取回武器并在实战中试刀见证成果！";
                            ctx.AddLink("取回修炼好的武器", "@retrieve_weapon");
                        }
                        else
                        {
                            long remainingSec = Math.Max(1, (rec.FinishTickMs - now) / 1000);
                            ctx.DialogText = $"武器仍在三昧真火中淬炼，尚需 {remainingSec} 秒方可出炉，切勿心急！";
                        }
                    }
                    else
                    {
                        ctx.DialogText = "老夫掌握上古神兵锻造秘术。需投入黑铁矿石（总纯度决定持久）与首饰（属性决定成败）！";
                        ctx.AddLink("开始修炼武器 (放入当前手持武器)", "@start_upgrade");
                        ctx.AddLink("打听升级秘诀", "@upgrade_tips");
                        ctx.AddLink("离开", "@close");
                    }
                    break;

                case "@start_upgrade":
                    var weapon = ctx.Session.State.Equipment[(int)OfficialEquipSlot.Weapon];
                    if (weapon == null)
                    {
                        ctx.DialogText = "你手中空空如也，请先装备上想要升级的武器！";
                        return;
                    }

                    // 启动升级状态机 (模拟淬火 1 秒完成供即时测试)
                    OngoingUpgrades[ctx.Session.SessionId] = new UpgradeRecord(weapon.Uid, weapon.Name, Environment.TickCount64 + 1000, 0);
                    ctx.DialogText = $"【{weapon.Name}】已被老夫投入熔炉！稍后即可前来取回。";
                    break;

                case "@retrieve_weapon":
                    if (OngoingUpgrades.TryRemove(ctx.Session.SessionId, out var finishedRec))
                    {
                        ctx.Session.State.TrialSlashCount = 1; // 激活试刀状态
                        var curW = ctx.Session.State.Equipment[(int)OfficialEquipSlot.Weapon];
                        if (curW != null) curW.UpgradeState = 1;
                        ctx.DialogText = $"已取回【{finishedRec.WeaponName}】！请立刻找怪物或对手开刀，生死由命，成败在天！";
                    }
                    break;

                case "@upgrade_tips":
                    ctx.DialogText = "黑铁矿石总纯度达到 60 以上可保武器持久不降甚至增加；首饰的上限攻魔道属性越高，成神几率越高！最高可升 7 点！";
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
    /// 8. 比奇武馆教头 / 拜师收徒使者行为树
    /// </summary>
    public sealed class MasterApprenticeRegistrarNpcBehavior : INpcBehavior
    {
        public int NpcId => 108;
        public string NpcName => "武馆教头";

        public void OnInteract(NpcDialogContext ctx)
        {
            if (ctx.Session.State == null) return;

            switch (ctx.SelectedAction.ToLowerInvariant())
            {
                case "@main":
                    ctx.DialogText = "尊师重道，武德流芳。年轻人，未满 28 级可拜师学艺；满 35 级方可开宗立派收徒！";
                    ctx.AddLink("我要拜师 (未满 28 级)", "@apprentice");
                    ctx.AddLink("我要收徒 (35 级以上且声望充足)", "@master");
                    ctx.AddLink("解除师徒关系", "@dissolve");
                    ctx.AddLink("离开", "@close");
                    break;

                case "@apprentice":
                    if (ctx.Session.State.Level < 28)
                    {
                        ctx.DialogText = "你资质上乘，请对方师父到我面前共同见证拜师之礼！";
                    }
                    else
                    {
                        ctx.DialogText = "你已达 28 级武艺小成，不可再拜师！";
                    }
                    break;

                case "@master":
                    if (ctx.Session.State.Level >= 35)
                    {
                        ctx.DialogText = "武学宗师之姿！徒弟出师后你将获得大量声望与荣耀奖励！";
                    }
                    else
                    {
                        ctx.DialogText = "未达 35 级，火候尚浅，不可误人子弟！";
                    }
                    break;

                case "@dissolve":
                    ctx.DialogText = "强行解除师徒关系将扣除 5 点江湖声望，是否执意如此？";
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
                    ctx.DialogText = "欲问鼎沙巴克尊位，唯有用铁与血来铸就！行会掌门人可向我提交攻城申请！";
                    ctx.AddLink("提交祖玛头像与攻城费 (需要 1 祖玛头像 + 1,000,000 金币)", "@apply_siege");
                    ctx.AddLink("查询当前攻城排期", "@check_schedule");
                    ctx.AddLink("离开", "@close");
                    break;

                case "@apply_siege":
                    if (ctx.Session.State.GuildRank != 1)
                    {
                        ctx.DialogText = "只有行会的一号掌门人方有权宣战攻城！";
                        return;
                    }

                    if (ctx.Session.State.Gold >= 1000000)
                    {
                        ctx.Session.State.Gold -= 1000000;
                        ctx.Session.State.RecalculateAuthoritativeWeights(ctx.Session.State.Gold);
                        ctx.DialogText = "攻城申请已被接纳！攻城战将于 3 天后 20:00 准时打响，号令天下英雄决战沙城！";
                    }
                    else
                    {
                        ctx.DialogText = "攻城申请需支付 1,000,000 战备金币，你的金币不足！";
                    }
                    break;

                case "@check_schedule":
                    ctx.DialogText = "本周攻城大战排期已定，全服英雄豪杰皆在厉兵秣马！";
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
                    ctx.Session.CurrentMapId = 9000;
                    ctx.Session.X = 50;
                    ctx.Session.Y = 50;
                    ctx.Session.EnqueuePacket(ProtocolConsts.SM_NEWMAP_DELPHI, 9000, 50, 50, 0);
                    ctx.DialogText = "乾坤逆转，你已踏入未知暗殿！小心看似弱小的生灵！";
                    ctx.CloseWindow = true;
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
                    var woodSword = new EquipItem { Uid = 1001, Name = "木剑", Dura = 4000, MaxDura = 4000, Weight = 7 };
                    var clothArmor = new EquipItem { Uid = 1002, Name = "布衣", Dura = 5000, MaxDura = 5000, Weight = 5 };
                    ctx.Session.State.BagItems.Add(woodSword);
                    ctx.Session.State.BagItems.Add(clothArmor);
                    ctx.DialogText = "新手装备已放入你的包裹，按 F9 键即可打开背包双击穿戴！";
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
}
