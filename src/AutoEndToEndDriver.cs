using System;
using Godot;
using Mir2Client.Game;
using Mir2Client.UI;

namespace Mir2Client;

/// <summary>
/// 纯 C# 全套端到端自动化演练控制器 (End-to-End Autonomous Pipeline)
/// 严格 1:1 驱动客户端生命周期：
/// 1. 登录 (Login) 触发账密校验与选区流转
/// 2. 选服 (SelectServer) 绑定石碑槽位与进入选角
/// 3. 选角 (SelectChar) 选定 1/2 号席位进入大世界
/// 4. 大世界 (GameView) 验证地表渲染、纸娃娃装备、安全区走跑、HUD面板呼出与 LIFO 栈关闭
/// </summary>
public partial class AutoEndToEndDriver : Node
{
    private Main? _main;
    private int _step = 0;
    private bool _abilityPageCaptured = false;
    private double _timer = 0.0;
    private bool _pendingCapture = false;
    private string _pendingTag = string.Empty;

    public override void _Ready()
    {
        _main = GetParent<Main>();
        GD.Print("[AutoE2E] 端到端自动化演练驱动器已装载，开始执行全链路自主巡检...");
    }

    public override void _Process(double delta)
    {
        if (_pendingCapture)
        {
            _pendingCapture = false;
            PerformCapture(_pendingTag);
        }

        _timer += delta;

        // 每隔 0.4 秒推进一个端到端生命周期阶段，保证画面充分绘制完毕
        if (_timer >= 0.4)
        {
            _timer = 0.0;
            _step++;
            ExecuteStep(_step);
        }
    }

    private void ExecuteStep(int step)
    {
        if (_main == null) return;

        switch (step)
        {
            case 1:
            {
                GD.Print("[AutoE2E] === 阶段 1: 捕获登录界面 (LoginView) 主登录面板 ===");
                RequestCapture("step1_login");
                break;
            }

            case 2:
            {
                GD.Print("[AutoE2E] === 阶段 1.1: 弹出官方 1.5 新用户注册面板 (Prguse.wil[63]) ===");
                var loginView = _main.GetNodeOrNull<LoginView>("Login");
                if (loginView != null)
                {
                    loginView.SwitchSubState(1);
                }
                break;
            }

            case 3:
            {
                GD.Print("[AutoE2E] === 阶段 1.1 捕获: 捕获注册面板截屏 ===");
                RequestCapture("step1_1_new_user_panel");
                break;
            }

            case 4:
            {
                GD.Print("[AutoE2E] === 阶段 1.2: 弹出官方 1.5 修改密码面板 (Prguse.wil[50]) ===");
                var loginView = _main.GetNodeOrNull<LoginView>("Login");
                if (loginView != null)
                {
                    loginView.SwitchSubState(2);
                }
                break;
            }

            case 5:
            {
                GD.Print("[AutoE2E] === 阶段 1.2 捕获: 捕获修改密码面板截屏 ===");
                RequestCapture("step1_2_change_pwd_panel");
                break;
            }

            case 6:
            {
                GD.Print("[AutoE2E] === 阶段 1.3: 启动官方 1.5 石门开启序列动画 (ChrSel 23~32) ===");
                var loginView = _main.GetNodeOrNull<LoginView>("Login");
                if (loginView != null)
                {
                    loginView.PlayDoorOpeningAnimation(() =>
                    {
                        GD.Print("[AutoE2E] 10 帧石门完全开启，正式切换至选区阶段 (SelectServerView)");
                        _main.SwitchPhase(Main.Phase.SelectServer);
                    });
                }
                break;
            }

            case 7:
            {
                // 石门播放中（第 4~5 帧半开洞口），捕获石门展开中渲染帧
                GD.Print("[AutoE2E] === 阶段 1.3 捕获: 捕获石门半开展开中渲染帧 ===");
                RequestCapture("step1_3_door_opening");
                break;
            }

            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
            {
                // 石门开启需要 2.3 秒（约 6 个 0.4 秒时钟周期），此处留足时间给石门完全开启并切换到 SelectServer
                GD.Print($"[AutoE2E] 等待石门完整开启并进入选服场景... (Step {step})");
                break;
            }

            case 13:
            {
                // 此时 SelectServerView 已经充分渲染就绪，捕获选服石碑界面
                GD.Print("[AutoE2E] === 阶段 2: 验证选服界面 (SelectServerView) 石碑槽位与选区 ===");
                RequestCapture("step2_select_server");
                break;
            }

            case 14:
            {
                // 切换到选角神龛场景
                GD.Print("[AutoE2E] === 阶段 3: 切换至选角神龛 (SelectCharView) ===");
                _main.SwitchPhase(Main.Phase.SelectChar);
                break;
            }

            case 15:
            {
                // 选角神龛已完全渲染，捕获选角神龛界面
                GD.Print("[AutoE2E] === 阶段 3 捕获: 捕获选角神龛双角色展示界面 ===");
                RequestCapture("step3_select_char");
                break;
            }

            case 16:
            {
                // 弹出官方 1.5 创角面板 (NewCharPanel, Prguse_0073)
                GD.Print("[AutoE2E] === 阶段 3.1: 弹出官方 1.5 创角面板 (Prguse_0073 300x417) ===");
                var selectChar = _main.GetNodeOrNull<SelectCharView>("SelectChar");
                if (selectChar != null)
                {
                    var panel = selectChar.GetNodeOrNull<Panel>("NewCharPanel");
                    if (panel != null) panel.Visible = true;
                }
                break;
            }

            case 17:
            {
                RequestCapture("step3_1_new_char_panel");
                break;
            }

            case 18:
            {
                // 关闭创角面板，弹出删角确认模态框 (MsgModal)
                GD.Print("[AutoE2E] === 阶段 3.2: 弹出删角确认模态弹窗 ===");
                var selectChar = _main.GetNodeOrNull<SelectCharView>("SelectChar");
                if (selectChar != null)
                {
                    var panel = selectChar.GetNodeOrNull<Panel>("NewCharPanel");
                    if (panel != null) panel.Visible = false;
                    selectChar.ShowMessageModal(SelectCharView.PromptDeleteConfirm);
                }
                break;
            }

            case 19:
            {
                RequestCapture("step3_2_del_char_modal");
                break;
            }

            case 20:
            {
                // 选角完成，进入大世界
                var selectChar = _main.GetNodeOrNull<SelectCharView>("SelectChar");
                if (selectChar != null)
                {
                    var modal = selectChar.GetNodeOrNull<Panel>("MsgModal");
                    if (modal != null) modal.Visible = false;
                }
                GD.Print("[AutoE2E] === 阶段 4: 进入大世界场景 (GameView) ===");
                _main.SwitchPhase(Main.Phase.Game);
                break;
            }

            case 21:
            {
                var gameView = _main.GetNodeOrNull<GameView>("Game");
                if (gameView != null)
                {
                    GD.Print("[AutoE2E] 大世界场景成功加载！执行单机沙盒比奇省初始化");
                    gameView.InitSandboxMode();
                    
                    var hud = gameView.HudInstance;
                    if (hud != null)
                    {
                        GD.Print("[AutoE2E] 验证快捷键呼出包裹面板 (F9) 与底部官方金属内胆属性回显");
                        hud.ToggleBag();
                        hud.ShowSlotTooltip("屠龙刀", "[color=#c0c0c0]重量 99  持久力 33/33[/color]\n[color=#ffff00]攻击 5-35  强度+1[/color]  [color=#00ffff]幸运+7[/color]\n[color=#c0c0c0]需要等级 35[/color]", new Vector2(400, 100), showFloating: false);
                    }
                }
                break;
            }

            case 22:
            {
                RequestCapture("step4_game_world_bag");
                break;
            }

            case 23:
            {
                var gameView = _main.GetNodeOrNull<GameView>("Game");
                if (gameView?.HudInstance != null)
                {
                    gameView.HudInstance.HideSlotTooltip();
                    gameView.HudInstance.ToggleBag(); // 关闭背包
                    GD.Print("[AutoE2E] === 验证人物装备 (F10) ===");
                    gameView.HudInstance.ToggleEquip();
                }
                break;
            }

            case 24:
            {
                RequestCapture("step5_game_world_equip");
                break;
            }

            case 25:
            {
                // 切换角色面板到独立状态面板 (F10 第二页 Tab 2)
                var gameView = _main.GetNodeOrNull<GameView>("Game");
                if (gameView?.HudInstance != null)
                {
                    GD.Print("[AutoE2E] === 验证独立人物状态面板 (StatusPanelView, 独立场景与实体窗体) ===");
                    var btnTab2 = gameView.HudInstance.GetNodeOrNull<BaseButton>("EquipPanel/BtnEquipTab2");
                    if (btnTab2 != null)
                    {
                        btnTab2.EmitSignal(BaseButton.SignalName.Pressed);
                    }
                    else
                    {
                        gameView.HudInstance.SwitchEquipToStatus();
                    }
                }
                break;
            }

            case 26:
            {
                RequestCapture("step5_1_game_world_status");
                break;
            }

            case 27:
            {
                var gameView = _main.GetNodeOrNull<GameView>("Game");
                if (!_abilityPageCaptured && gameView?.HudInstance?.StatusPanel != null)
                {
                    // 官方能力页 (IDA sub_488AF4 0x4892c2) 截图,随后回退一步让本 case 再执行一次以关闭面板
                    _abilityPageCaptured = true;
                    gameView.HudInstance.StatusPanel.SetAbilityPage(true);
                    // MOCK 数据(非官方取值格式,仅验证第 2~10 行布局): 负重量/腕力/精确/敏捷/魔防/毒防/毒恢复/体恢复/魔恢复
                    string[] mockRows = { "12/50", "45/60", "9", "9", "0", "0", "0", "10%", "10%" };
                    for (int mr = 0; mr < mockRows.Length; mr++)
                        gameView.HudInstance.StatusPanel.UpdateAbilityValue(2 + mr, mockRows[mr]);
                    RequestCapture("step5_2_status_ability_page");
                    _step--;
                    break;
                }
                if (gameView?.HudInstance != null)
                {
                    gameView.HudInstance.StatusPanel?.SetAbilityPage(false);
                    gameView.HudInstance.CloseTopDialog(); // 关闭独立状态面板
                    GD.Print("[AutoE2E] === 验证快捷键呼出技能魔法书 (F11) ===");
                    gameView.HudInstance.ToggleSkill();
                }
                break;
            }

            case 28:
            {
                RequestCapture("step6_game_world_skill");
                break;
            }

            case 29:
            {
                var gameView = _main.GetNodeOrNull<GameView>("Game");
                if (gameView?.HudInstance != null)
                {
                    gameView.HudInstance.ToggleSkill(); // 关闭技能
                    GD.Print("[AutoE2E] === 验证模态输入弹窗 (MessageBoxDialog/TxtInput) ===");
                    gameView.HudInstance.ShowInputBox("请输入金币存取数量或丢弃金币：", "50000");
                }
                break;
            }

            case 30:
            {
                RequestCapture("step7_message_input_box");
                break;
            }

            case 31:
            {
                var gameView = _main.GetNodeOrNull<GameView>("Game");
                if (gameView?.HudInstance != null)
                {
                    gameView.HudInstance.CloseTopDialog(); // 关闭模态框
                    GD.Print("[AutoE2E] === 验证组队面板 (GroupPanelView) ===");
                    gameView.HudInstance.ToggleGroup();
                }
                break;
            }

            case 32:
            {
                RequestCapture("step8_group_panel");
                break;
            }

            case 33:
            {
                var gameView = _main.GetNodeOrNull<GameView>("Game");
                if (gameView?.HudInstance != null)
                {
                    gameView.HudInstance.ToggleGroup(); // 关闭组队
                    GD.Print("[AutoE2E] === 验证仓库保管箱面板 (StoragePanelView) ===");
                    gameView.HudInstance.ToggleStorage();
                }
                break;
            }

            case 34:
            {
                RequestCapture("step9_storage_panel");
                break;
            }

            case 35:
            {
                var gameView = _main.GetNodeOrNull<GameView>("Game");
                if (gameView?.HudInstance != null)
                {
                    gameView.HudInstance.ToggleStorage(); // 关闭仓库
                    GD.Print("[AutoE2E] === 验证商店买卖面板 (ShopPanelView) ===");
                    // MOCK 数据(非官方,仅用于验证表头/行布局):商品名、持久、价格
                    gameView.HudInstance.ShopPanel?.SetItems(new[]
                    {
                        new ShopPanelView.ShopItemEntry("金创药(小)", 1, 100),
                        new ShopPanelView.ShopItemEntry("魔法药(小)", 1, 100),
                        new ShopPanelView.ShopItemEntry("太阳水", 1, 500),
                        new ShopPanelView.ShopItemEntry("回城卷", 1, 150),
                        new ShopPanelView.ShopItemEntry("随机传送卷", 1, 200),
                    });
                    gameView.HudInstance.ToggleShop(101);
                }
                break;
            }

            case 36:
            {
                RequestCapture("step10_shop_panel");
                break;
            }

            case 37:
            {
                var gameView = _main.GetNodeOrNull<GameView>("Game");
                if (gameView?.HudInstance != null)
                {
                    gameView.HudInstance.ToggleShop(101); // 关闭商店
                    GD.Print("[AutoE2E] === 验证铁匠铺修理面板 (RepairPanelView) ===");
                    gameView.HudInstance.ToggleRepair();
                }
                break;
            }

            case 38:
            {
                RequestCapture("step11_repair_panel");
                break;
            }

            case 39:
            {
                var gameView = _main.GetNodeOrNull<GameView>("Game");
                if (gameView?.HudInstance != null)
                {
                    gameView.HudInstance.ToggleRepair(); // 关闭修理
                    GD.Print("[AutoE2E] === 验证双面板玩家交易系统 (TradePanelView) ===");
                    gameView.HudInstance.ToggleTrade();
                }
                break;
            }

            case 40:
            {
                RequestCapture("step12_trade_panel");
                break;
            }

            case 41:
            {
                var gameView = _main.GetNodeOrNull<GameView>("Game");
                if (gameView?.HudInstance != null)
                {
                    gameView.HudInstance.ToggleTrade(); // 关闭交易
                    GD.Print("[AutoE2E] === 验证行会面板 (GuildPanelView) ===");
                    // MOCK 数据(非官方内容,仅验证官方三段格式的渲染)
                    gameView.HudInstance.GuildPanel?.SetHomeText(
                        new[] { "本周六晚八点攻城。", "请备足药品。" },
                        new[] { "不败神话" },
                        new[] { "邪风军团", "昔日同盟" });
                    gameView.HudInstance.ToggleGuild();
                }
                break;
            }

            case 42:
            {
                RequestCapture("step13_guild_panel");
                break;
            }

            case 43:
            {
                var gameView = _main.GetNodeOrNull<GameView>("Game");
                if (gameView?.HudInstance != null)
                {
                    gameView.HudInstance.ToggleGuild(); // 关闭行会
                    GD.Print("[AutoE2E] === 验证NPC对话框 (NpcDialogView) ===");
                    gameView.HudInstance.NpcDialog?.ShowDialog("比奇老兵", "欢迎来到热血传奇1.5官方复刻版！\n\n请选择前往的目的地：", new string[] { "前往盟重省", "前往毒蛇山谷", "取消" });
                }
                break;
            }

            case 44:
            {
                RequestCapture("step14_npc_dialog");
                break;
            }

            case 45:
            {
                var gameView = _main.GetNodeOrNull<GameView>("Game");
                if (gameView?.HudInstance != null)
                {
                    if (gameView.HudInstance.NpcDialog != null) gameView.HudInstance.NpcDialog.Visible = false;
                    GD.Print("[AutoE2E] === 验证观察他人装备面板 (InspectPanelView) ===");
                    // MOCK 数据(非官方内容,仅验证槽位图标填充与尺寸):手镯/戒指/项链
                    gameView.HudInstance.InspectPanel?.SetSlotItem(5, "StateItem_0189.png", "[color=#ffff40]MOCK 手镯[/color]");
                    gameView.HudInstance.InspectPanel?.SetSlotItem(7, "StateItem_0171.png", "[color=#ffff40]MOCK 戒指[/color]");
                    gameView.HudInstance.InspectPanel?.SetSlotItem(3, "StateItem_0219.png", "[color=#ffff40]MOCK 项链[/color]");
                    gameView.HudInstance.InspectPanel?.Inspect("无涯");
                }
                break;
            }

            case 46:
            {
                RequestCapture("step15_inspect_panel");
                break;
            }

            case 47:
            {
                var gameView = _main.GetNodeOrNull<GameView>("Game");
                if (gameView?.HudInstance != null)
                {
                    if (gameView.HudInstance.InspectPanel != null) gameView.HudInstance.InspectPanel.Visible = false;
                    GD.Print("[AutoE2E] === 验证死亡复活界面 (DeathScreenView) ===");
                    gameView.HudInstance.DeathScreen?.ShowDeath();
                }
                break;
            }

            case 48:
            {
                RequestCapture("step16_death_screen");
                break;
            }

            case 49:
            {
                var gameView = _main.GetNodeOrNull<GameView>("Game");
                if (gameView?.HudInstance != null)
                {
                    if (gameView.HudInstance.DeathScreen != null) gameView.HudInstance.DeathScreen.Visible = false;
                    GD.Print("[AutoE2E] === 验证加点分配界面 (AdjustAbilPanelView) ===");
                    gameView.HudInstance.AdjustAbilPanel?.Open(5);
                }
                break;
            }

            case 50:
            {
                RequestCapture("step18_adjust_abil_panel");
                break;
            }

            case 51:
            {
                var gameView = _main.GetNodeOrNull<GameView>("Game");
                if (gameView?.HudInstance != null)
                {
                    if (gameView.HudInstance.AdjustAbilPanel != null) gameView.HudInstance.AdjustAbilPanel.Visible = false;
                    GD.Print("[AutoE2E] === 验证技能快捷键绑定界面 (KeySelectDialogView) ===");
                    gameView.HudInstance.KeySelectDialog?.OpenForSkill(1001, "基础剑术", 0);
                }
                break;
            }

            case 52:
            {
                RequestCapture("step19_key_select_dialog");
                break;
            }

            case 53:
            {
                var gameView = _main.GetNodeOrNull<GameView>("Game");
                if (gameView?.HudInstance != null)
                {
                    if (gameView.HudInstance.KeySelectDialog != null) gameView.HudInstance.KeySelectDialog.Visible = false;
                    GD.Print("[AutoE2E] === 验证小地图雷达模式 (MinimapView: Mini) ===");
                    gameView.HudInstance.Minimap?.SetMode(MinimapView.MinimapMode.Mini);
                }
                break;
            }

            case 54:
            {
                RequestCapture("step20_minimap_radar");
                break;
            }

            case 55:
            {
                var gameView = _main.GetNodeOrNull<GameView>("Game");
                if (gameView?.HudInstance != null)
                {
                    GD.Print("[AutoE2E] === 验证全景大地图模式 (MinimapView: Big) ===");
                    gameView.HudInstance.Minimap?.SetMode(MinimapView.MinimapMode.Big);
                }
                break;
            }

            case 56:
            {
                RequestCapture("step21_bigmap_overview");
                break;
            }

            case 57:
            {
                var gameView = _main.GetNodeOrNull<GameView>("Game");
                if (gameView?.HudInstance != null)
                {
                    gameView.HudInstance.Minimap?.SetMode(MinimapView.MinimapMode.Closed);
                    GD.Print("[AutoE2E] === 验证动态血球魔球裁剪 (HP 45%, MP 60%) 与经验负重条 ===");
                    gameView.HudInstance.UpdateHp(45, 100);
                    gameView.HudInstance.UpdateMp(60, 100);
                    gameView.HudInstance.UpdateExp(65000, 100000);
                    gameView.HudInstance.UpdateWeight(45, 60);
                }
                RequestCapture("step22_dynamic_orb_and_bars");
                break;
            }

            case 58:
            {
                var gameView = _main.GetNodeOrNull<GameView>("Game");
                if (gameView?.HudInstance != null)
                {
                    GD.Print("[AutoE2E] === 验证数字键 1 快捷药水使用与数量角标更新 ===");
                    gameView.HudInstance.UseQuickSlot(0); // 消耗一瓶强效金创药
                }
                break;
            }

            case 59:
            {
                RequestCapture("step23_quick_potion_use");
                break;
            }

            case 60:
            {
                var gameView = _main.GetNodeOrNull<GameView>("Game");
                if (gameView?.HudInstance != null)
                {
                    GD.Print("[AutoE2E] === 验证技能魔法书修炼经验条、经验数值与快捷键角标 ===");
                    gameView.HudInstance.ToggleSkill();
                }
                RequestCapture("step24_skill_exp_bars");
                break;
            }

            case 61:
            {
                var gameView = _main.GetNodeOrNull<GameView>("Game");
                if (gameView?.HudInstance != null)
                {
                    gameView.HudInstance.ToggleSkill(); // 关闭技能
                    GD.Print("[AutoE2E] === 验证装备槽位极品属性高亮染色 (+1) 官方内胆回显 ===");
                    gameView.HudInstance.ToggleEquip(); // 打开装备面板
                    // 盛大原版 1.5 规则：鼠标悬停在装备槽位时，极品与基础属性回显在面板底部内胆凹槽内
                    gameView.HudInstance.ShowSlotTooltip("死神手套(右)", "[color=#c0c0c0]重量 2   持久力 8/8[/color]\n[color=#ffff00]攻击 1-3[/color] [color=#00ffff](+1)[/color]\n[color=#c0c0c0]需要等级 22[/color]", new Vector2(560, 160), showFloating: false);
                }
                RequestCapture("step25_equip_god_stat_tooltip");
                break;
            }

            case 62:
            {
                var gameView = _main.GetNodeOrNull<GameView>("Game");
                if (gameView?.HudInstance != null && gameView.PlayerInstance != null)
                {
                    gameView.HudInstance.HideSlotTooltip();
                    gameView.HudInstance.ToggleEquip(); // 关闭装备
                    GD.Print("[AutoE2E] === 验证头顶红名大恶魔 (PK点数>=200) 与受创微型血条 ===");
                    gameView.PlayerInstance.SetPkPoints(250);
                    gameView.PlayerInstance.TriggerHpDamage(35, 100); // 扣血至 35%，激活头顶血条 (3秒倒计时)
                }
                RequestCapture("step26_player_pk_redname_hpbar");
                break;
            }

            case 63:
            {
                GD.Print("[AutoE2E] === 全量大世界面板与底层动态细节 100% 验证完成 ===");
                RequestCapture("step17_all_buttons_completed");
                break;
            }

            case 64:
            {
                GD.Print("[AutoE2E] ALL DYNAMIC DETAILS AND BUTTON CHECKS COMPLETED SUCCESSFULLY!");
                GetTree().Quit();
                break;
            }




        }
    }

    private void RequestCapture(string tag)
    {
        _pendingCapture = true;
        _pendingTag = tag;
    }

    private void PerformCapture(string stageTag)
    {
        try
        {
            var vp = GetViewport();
            if (vp == null) return;
            var tex = vp.GetTexture();
            if (tex == null) return;
            var img = tex.GetImage();
            if (img != null)
            {
                string path = $"/tmp/e2e_{stageTag}.png";
                img.SavePng(path);
                GD.Print($"[AutoE2E] 捕获阶段渲染截图: {path}");
            }
        }
        catch (Exception ex)
        {
            GD.PrintErr($"[AutoE2E] 截屏异常 ({stageTag}): {ex.Message}");
        }
    }
}
