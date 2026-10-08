# 对话框真名表（IDA 函数名枚举 165 个，证据 scratch/ida_dialogs.json）

| IDA地址 | 官方函数名 | 我方View | 对齐 |
|---|---|---|---|
| 0x401250 | ExitProcess | `GameView.cs`: GetTree().Quit() | 已对齐 |
| 0x4060d4 | GetCurrentProcessId | `Main.cs`: OS.GetProcessId() | 已对齐 |
| 0x406724 | GetWindowThreadProcessId | `Main.cs`: OS.GetProcessId() | 已对齐 |
| 0x45f198 | TFrmMain_FormCreate | `Main.cs`: _Ready() | 已对齐 |
| 0x45fc68 | TFrmMain_FormDestroy | `Main.cs`: _Notification(NOTIFICATION_PREDELETE) | 已对齐 |
| 0x460010 | TFrmMain_DXDraw1Initialize | `GameView.cs`: _Ready() 初始化瓦片与大世界视口 | 已对齐 |
| 0x4609dc | TFrmMain_DXDraw1Finalize | `GameView.cs`: _ExitTree() 清理大世界资源 | 已对齐 |
| 0x4609ec | TFrmMain_FormClose | `Main.cs`: OnWindowClose() | 已对齐 |
| 0x461dec | TFrmMain_FormKeyDown | `InputPlugin.cs`: HandleActionKeyDown() 按键驱动 | 已对齐 |
| 0x4622f0 | TFrmMain_FormKeyPress | `InputPlugin.cs`: HandleActionKeyPress() 文本输入 | 已对齐 |
| 0x4630f4 | TFrmMain_DXDraw1MouseMove | `GameView.cs`: _UnhandledInput() 鼠标移动大世界瓦片追踪 | 已对齐 |
| 0x463310 | TFrmMain_DXDraw1MouseDown | `GameView.cs`: _UnhandledInput() 鼠标按下出刀/走动/选怪 | 已对齐 |
| 0x463bfc | TFrmMain_DXDraw1DblClick | `GameView.cs`: _UnhandledInput() 鼠标双击Shift深度查名 | 已对齐 |
| 0x463c94 | TFrmMain_DXDraw1MouseUp | `GameView.cs`: _UnhandledInput() 鼠标抬起停止位移 | 已对齐 |
| 0x463cd4 | TFrmMain_DXDraw1Click | `GameView.cs`: _UnhandledInput() 瓦片单步交互 | 已对齐 |
| 0x463d04 | TFrmMain_MouseTimerTimer | `GameView.cs`: _Process() 鼠标悬停雷达与交互时钟 | 已对齐 |
| 0x463eac | TFrmMain_WaitMsgTimerTimer | `GameClient.cs`: WaitMsgTimer 网络超时与消息重发 | 已对齐 |
| 0x463fc0 | TFrmMain_SelChrWaitTimerTimer | `SelectCharView.cs`: SelChrWaitTimer 选角超时守护时钟 | 已对齐 |
| 0x464014 | TFrmMain_CmdTimerTimer | `GameClient.cs`: CmdTimer 心跳保活与队列发包 | 已对齐 |
| 0x464600 | TFrmMain_CSocketConnect | `GameClient.cs`: OnConnected() 网络链路握手建立 | 已对齐 |
| 0x4648a4 | TFrmMain_CSocketDisconnect | `GameClient.cs`: OnDisconnected() 链路优雅断开 | 已对齐 |
| 0x464930 | TFrmMain_CSocketError | `GameClient.cs`: OnError() 网络异常熔断 | 已对齐 |
| 0x464954 | TFrmMain_CSocketRead | `GameClient.cs`: OnDataReceived() 协议分包与解码 | 已对齐 |
| 0x4671b0 | TFrmMain_Timer1Timer | `Main.cs`: SystemTimer() 全局状态定时器 | 已对齐 |
| 0x467920 | ProcessServerMessage | `ServerMessageRouter.cs`: OnPacket() 172个顶层协议总路由 | 已对齐 |
| 0x46e88c | TFrmMain_MinTimerTimer | `GameClient.cs`: MinTimer 1分钟定时清理脏实体 | 已对齐 |
| 0x46e978 | TFrmMain_CheckHackTimerTimer | `GameClient.cs`: CheckHackTimer 动作时钟频次防加速自检 | 已对齐 |
| 0x46eb94 | TFrmMain_SendTimeTimerTimer | `GameClient.cs`: SendTimeTimer 时间戳同步时钟 | 已对齐 |
| 0x47ddbc | MsgProcessor_CmLogin | `PacketEncoder.cs`: Login() 账号密码发包 | 已对齐 |
| 0x48198c | TFrmDlg_FormCreate | `HudController.cs`: _Ready() 24个原生UI栈管理器初始化 | 已对齐 |
| 0x481c5c | TFrmDlg_FormDestroy | `HudController.cs`: _ExitTree() 释放全部子面板 | 已对齐 |
| 0x486730 | TFrmDlg_DBackgroundBackgroundClick | `HudController.cs`: OnBackgroundClick() 模态遮罩穿透防御 | 已对齐 |
| 0x486858 | TFrmDlg_DBackgroundMouseDown | `HudController.cs`: OnBackgroundMouseDown() 窗口置顶提权 | 已对齐 |
| 0x486964 | TFrmDlg_DBottomMouseDown | `HudController.cs`: OnBottomBarMouseDown() HUD底栏置顶 | 已对齐 |
| 0x487540 | TFrmDlg_DMsgDlgOkClick | `MessageBoxDialog.cs`: OnOkBtnPressed() 模态弹窗确认 | 已对齐 |
| 0x4875ec | TFrmDlg_DMsgDlgKeyDown | `MessageBoxDialog.cs`: _UnhandledInput() 回车确认/ESC关闭 | 已对齐 |
| 0x487724 | TFrmDlg_DMsgDlgOkDirectPaint | `MessageBoxDialog.cs`: _Draw() 确认按钮 Prguse 贴图 | 已对齐 |
| 0x4877d0 | TFrmDlg_DMsgDlgDirectPaint | `MessageBoxDialog.cs`: _Draw() 模态对话框底板 Prguse_0360 | 已对齐 |
| 0x487a6c | TFrmDlg_OnLoginPanel_Paint | `LoginView.cs`: _Draw() 登录主框 Prguse 贴图绘制 | 已对齐 |
| 0x487afc | TFrmDlg_OnNewAccountBtn_Click | `LoginView.cs`: OnNewAccountBtnPressed() 打开新用户注册 | 已对齐 |
| 0x487b20 | TFrmDlg_OnLoginBtn_Click | `LoginView.cs`: OnLoginBtnPressed() 提交账号密码登录 | 已对齐 |
| 0x487b44 | TFrmDlg_OnCloseBtn_Click | `LoginView.cs`: OnCloseBtnPressed() 关闭登录窗口 | 已对齐 |
| 0x487b68 | TFrmDlg_OnChgPasswordBtn_Click | `LoginView.cs`: OnChgPasswordBtnPressed() 打开修改密码 | 已对齐 |
| 0x487bf4 | TFrmDlg_DSelServerDlgDirectPaint | `SelectServerView.cs`: _Draw() 双龙石碑选服底板 | 已对齐 |
| 0x488028 | TFrmDlg_DSServer1Click | `SelectServerView.cs`: OnServerItemClicked() 选中服务器分区 | 已对齐 |
| 0x488380 | TFrmDlg_DEngServer1Click | `SelectServerView.cs`: OnServerItemClicked() 线路选择 | 已对齐 |
| 0x48841c | TFrmDlg_DSSrvCloseClick | `SelectServerView.cs`: OnCloseClicked() 退回登录界面 | 已对齐 |
| 0x488450 | TFrmDlg_DNewAccountOkClick | `LoginView.cs`: OnNewAccountOkClicked() 注册新账号 | 已对齐 |
| 0x488474 | TFrmDlg_DNewAccountCloseClick | `LoginView.cs`: OnNewAccountCloseClicked() 关闭注册界面 | 已对齐 |
| 0x488498 | TFrmDlg_DNewAccountDirectPaint | `LoginView.cs`: DrawNewAccount() 注册底板绘制 | 已对齐 |
| 0x4885f8 | TFrmDlg_DChgpwOkClick | `LoginView.cs`: OnChangePasswordOkClicked() 提交改密 | 已对齐 |
| 0x488644 | TFrmDlg_DscSelect1DirectPaint | `SelectCharView.cs`: _Draw() 神龛角色选角基座 | 已对齐 |
| 0x4886c0 | TFrmDlg_DscSelect1Click | `SelectCharView.cs`: OnSelectCharClicked() 选中神龛槽位 | 已对齐 |
| 0x48878c | TFrmDlg_DccCloseDirectPaint | `CreateCharView.cs`: _Draw() 创角金雕界面底板 | 已对齐 |
| 0x4889c0 | TFrmDlg_DccCloseClick | `CreateCharView.cs`: OnCloseClicked() 取消创角退回神龛 | 已对齐 |
| 0x488af4 | TFrmDlg_DStateWinDirectPaint | `EquipPanelView.cs`: _Draw() 9槽位装备立绘与底板 | 已对齐 |
| 0x48a0f0 | TFrmDlg_DSWLightDirectPaint | `EquipPanelView.cs`: _Draw() 蜡烛/火把持久光晕绘制 | 已对齐 |
| 0x48a6a4 | TFrmDlg_DStateWinClick | `EquipPanelView.cs`: OnPanelClicked() 装备面板置顶 | 已对齐 |
| 0x48a77c | TFrmDlg_DCloseStateClick | `EquipPanelView.cs`: OnCloseClicked() 关闭装备面板 (F10) | 已对齐 |
| 0x48a7a4 | TFrmDlg_DPrevStateDirectPaint | `EquipPanelView.cs`: _Draw() 翻页按钮绘制 | 已对齐 |
| 0x48a93c | TFrmDlg_DPrevStateClick | `HudController.cs`: TFrmDlg_DPrevStateClick() 切回装备面板 | 已对齐 |
| 0x48a97c | TFrmDlg_DNextStateClick | `HudController.cs`: TFrmDlg_DNextStateClick() 切换至独立状态面板 | 已对齐 |
| 0x48a9bc | TFrmDlg_DSWWeaponClick | `EquipPanelView.cs`: OnWeaponSlotClicked() 装备脱下/互换 | 已对齐 |
| 0x48ae2c | TFrmDlg_DSWWeaponMouseMove | `EquipPanelView.cs`: OnSlotHover() 浮动对比装备Tooltip | 已对齐 |
| 0x48af7c | TFrmDlg_DStateWinMouseMove | `EquipPanelView.cs`: OnMouseMove() 槽位高亮反馈 | 已对齐 |
| 0x48af9c | TFrmDlg_DStMag1DirectPaint | `SkillPanelView.cs`: _Draw() 魔法书羊皮卷底板 | 已对齐 |
| 0x48b0e0 | TFrmDlg_DStMag1Click | `SkillPanelView.cs`: OnSkillItemClicked() 选中技能/设快捷键 | 已对齐 |
| 0x48b23c | TFrmDlg_DStPageUpClick | `SkillPanelView.cs`: OnPageUpClicked() 魔法书向上翻页 | 已对齐 |
| 0x48b2a0 | TFrmDlg_DBottomDirectPaint | `HudController.cs`: _Draw() HUD底盘、血球魔球与经验刻度 | 已对齐 |
| 0x48ba74 | TFrmDlg_DBottomInRealArea | `HudController.cs`: IsPointInHudArea() 底栏异形点击碰撞判定 | 已对齐 |
| 0x48bacc | TFrmDlg_DMyStateDirectPaint | `HudController.cs`: _Draw() 状态按钮高亮态 | 已对齐 |
| 0x48bb60 | TFrmDlg_DBotGroupDirectPaint | `HudController.cs`: _Draw() 组队按钮高亮态 | 已对齐 |
| 0x48bc5c | TFrmDlg_DBotPlusAbilDirectPaint | `HudController.cs`: _Draw() 加点按钮高亮态 | 已对齐 |
| 0x48bdec | TFrmDlg_DMyStateClick | `HudController.cs`: OnStateBtnClicked() 呼出/隐藏人物面板 | 已对齐 |
| 0x48be70 | TFrmDlg_DOptionClick | `HudController.cs`: OnOptionBtnClicked() 呼出系统设置菜单 | 已对齐 |
| 0x48be80 | TFrmDlg_DBelt1DirectPaint | `HudController.cs`: _Draw() 1-6快捷药水栏与角标 | 已对齐 |
| 0x48bfd4 | TFrmDlg_DBelt1MouseMove | `HudController.cs`: OnBeltMouseMove() 药水悬停提示 | 已对齐 |
| 0x48c02c | TFrmDlg_DBelt1Click | `HudController.cs`: OnBeltClick() 快捷栏槽位选中 | 已对齐 |
| 0x48c188 | TFrmDlg_DBelt1DblClick | `HudController.cs`: UseBeltItem() 双击或按键快捷使用药水 | 已对齐 |
| 0x48dc24 | TFrmDlg_DItemBagDirectPaint | `ItemBagView.cs`: _Draw() 40格背包面板与负重条 | 已对齐 |
| 0x48debc | TFrmDlg_DRepairItemInRealArea | `RepairPanelView.cs`: IsPointInRepairArea() 铁匠铺异形判定 | 已对齐 |
| 0x48df10 | TFrmDlg_DRepairItemDirectPaint | `RepairPanelView.cs`: _Draw() 装备修理面板底板 | 已对齐 |
| 0x48df9c | TFrmDlg_DCloseBagDirectPaint | `ItemBagView.cs`: _Draw() 背包关闭按钮 | 已对齐 |
| 0x48e02c | TFrmDlg_DCloseBagClick | `ItemBagView.cs`: OnCloseClicked() 关闭背包面板 (F9) | 已对齐 |
| 0x48e054 | TFrmDlg_DItemGridGridMouseMove | `ItemBagView.cs`: OnGridMouseMove() 物品Tooltip追踪 | 已对齐 |
| 0x48e118 | TFrmDlg_DItemGridGridSelect | `ItemBagView.cs`: OnGridSelected() 格子选中高亮 | 已对齐 |
| 0x48e33c | TFrmDlg_DItemGridDblClick | `ItemBagView.cs`: OnGridDblClicked() 双击快速吃药/穿戴 | 已对齐 |
| 0x48e458 | TFrmDlg_DItemGridGridPaint | `ItemBagView.cs`: _Draw() 40格子物品图标与极品染色 | 已对齐 |
| 0x48e564 | TFrmDlg_DGoldClick | `ItemBagView.cs`: OnGoldClicked() 背包底栏点击丢金币 | 已对齐 |
| 0x48eac0 | TFrmDlg_DMerchantDlgDirectPaint | `MerchantPanelView.cs`: _Draw() NPC对话框 Prguse 贴图 | 已对齐 |
| 0x48ef90 | TFrmDlg_DMerchantDlgCloseClick | `MerchantPanelView.cs`: OnCloseClicked() 关闭NPC对话框 | 已对齐 |
| 0x48f020 | TFrmDlg_DMenuDlgDirectPaint | `MerchantPanelView.cs`: _Draw() 商店菜单与货架页面 | 已对齐 |
| 0x48f658 | TFrmDlg_DMenuDlgClick | `MerchantPanelView.cs`: OnMenuClicked() 脚本超链接选项点击 | 已对齐 |
| 0x48f9ec | TFrmDlg_DMenuDlgMouseMove | `MerchantPanelView.cs`: OnMenuMouseMove() 超链接悬停变色 | 已对齐 |
| 0x48fa80 | TFrmDlg_DMenuBuyClick | `MerchantPanelView.cs`: OnBuyClicked() 购买选中货品 | 已对齐 |
| 0x48fc14 | TFrmDlg_DMenuPrevClick | `MerchantPanelView.cs`: OnPrevPageClicked() 货架上一页 | 已对齐 |
| 0x48fcb4 | TFrmDlg_DMenuNextClick | `MerchantPanelView.cs`: OnNextPageClicked() 货架下一页 | 已对齐 |
| 0x48fec4 | TFrmDlg_DMenuCloseClick | `MerchantPanelView.cs`: OnCloseClicked() 关闭买卖货架 | 已对齐 |
| 0x48feec | TFrmDlg_DMerchantDlgClick | `MerchantPanelView.cs`: OnMerchantClicked() NPC交互点击 | 已对齐 |
| 0x490010 | TFrmDlg_DMerchantDlgMouseDown | `MerchantPanelView.cs`: OnMouseDown() 拖拽NPC窗体 | 已对齐 |
| 0x49011c | TFrmDlg_DMerchantDlgMouseUp | `MerchantPanelView.cs`: OnMouseUp() 释放NPC窗体 | 已对齐 |
| 0x490140 | TFrmDlg_DSellDlgDirectPaint | `MerchantPanelView.cs`: _Draw() 物品出售/典当底板 | 已对齐 |
| 0x4902f4 | TFrmDlg_DSellDlgCloseClick | `MerchantPanelView.cs`: OnSellCloseClicked() 关闭出售面板 | 已对齐 |
| 0x490314 | TFrmDlg_DSellDlgSpotClick | `MerchantPanelView.cs`: OnSellSpotClicked() 放入待售物品 | 已对齐 |
| 0x490460 | TFrmDlg_DSellDlgSpotDirectPaint | `MerchantPanelView.cs`: _Draw() 待售格子高亮 | 已对齐 |
| 0x490520 | TFrmDlg_DSellDlgSpotMouseMove | `MerchantPanelView.cs`: OnSellSpotMouseMove() 估价提示 | 已对齐 |
| 0x490550 | TFrmDlg_DSellDlgOkClick | `MerchantPanelView.cs`: OnSellOkClicked() 确认出售交易 | 已对齐 |
| 0x4907d8 | TFrmDlg_DKeySelDlgDirectPaint | `KeySelectPanelView.cs`: _Draw() F1-F8快捷键设置底板 | 已对齐 |
| 0x490920 | TFrmDlg_DKsIconDirectPaint | `KeySelectPanelView.cs`: _Draw() 技能图标高亮槽位 | 已对齐 |
| 0x4909a0 | TFrmDlg_DKsF1DirectPaint | `KeySelectPanelView.cs`: _Draw() F1按键槽位回显 | 已对齐 |
| 0x490b50 | TFrmDlg_DKsOkClick | `KeySelectPanelView.cs`: OnOkClicked() 绑定技能快捷键 | 已对齐 |
| 0x490b78 | TFrmDlg_DKsF1Click | `KeySelectPanelView.cs`: OnKeySlotClicked() 选中F1键位 | 已对齐 |
| 0x490c80 | TFrmDlg_DBotMiniMapClick | `HudController.cs`: OnMinimapBtnClicked() 切换小地图雷达 | 已对齐 |
| 0x490d04 | TFrmDlg_DBotTradeClick | `HudController.cs`: OnTradeBtnClicked() 发起面对面交易 | 已对齐 |
| 0x490d48 | TFrmDlg_DBotGuildClick | `HudController.cs`: OnGuildBtnClicked() 呼出行会主面板 | 已对齐 |
| 0x490db0 | TFrmDlg_DBotGroupClick | `HudController.cs`: OnGroupBtnClicked() 呼出组队编组面板 | 已对齐 |
| 0x490dfc | TFrmDlg_DGroupDlgDirectPaint | `GroupPanelView.cs`: _Draw() 组队面板与成员列表 | 已对齐 |
| 0x490fe0 | TFrmDlg_DGrpDlgCloseClick | `GroupPanelView.cs`: OnCloseClicked() 关闭组队面板 | 已对齐 |
| 0x491008 | TFrmDlg_DGrpAllowGroupDirectPaint | `GroupPanelView.cs`: _Draw() 允许组队复选框 | 已对齐 |
| 0x491104 | TFrmDlg_DGrpAllowGroupClick | `GroupPanelView.cs`: OnAllowGroupClicked() 切换允许组队模式 | 已对齐 |
| 0x491160 | TFrmDlg_DGrpCreateClick | `GroupPanelView.cs`: OnCreateGroupClicked() 创建新队伍 | 已对齐 |
| 0x491264 | TFrmDlg_DGrpAddMemClick | `GroupPanelView.cs`: OnAddMemberClicked() 邀请玩家入队 | 已对齐 |
| 0x491368 | TFrmDlg_DGrpDelMemClick | `GroupPanelView.cs`: OnDelMemberClicked() 开除队伍成员 | 已对齐 |
| 0x491474 | TFrmDlg_DBotLogoutClick | `HudController.cs`: OnLogoutClicked() 软注销退回角色神龛 | 已对齐 |
| 0x491520 | TFrmDlg_DBotExitClick | `HudController.cs`: OnExitClicked() 完全退出游戏 | 已对齐 |
| 0x4915cc | TFrmDlg_DBotPlusAbilClick | `HudController.cs`: OnPlusAbilClicked() 呼出属性点分配 | 已对齐 |
| 0x491710 | TFrmDlg_DDealOkClick | `TradePanelView.cs`: OnConfirmClicked() 两阶段交易锁定确认 | 已对齐 |
| 0x491798 | TFrmDlg_DDealCloseClick | `TradePanelView.cs`: OnCancelClicked() 取消交易退还物品 | 已对齐 |
| 0x4917d0 | TFrmDlg_DDealRemoteDlgDirectPaint | `TradePanelView.cs`: _Draw() 对方交易栏货品展示 | 已对齐 |
| 0x491954 | TFrmDlg_DDealDlgDirectPaint | `TradePanelView.cs`: _Draw() 己方交易栏与锁定框 | 已对齐 |
| 0x491b3c | TFrmDlg_DDGridGridSelect | `TradePanelView.cs`: OnGridSelect() 放入交易物品 | 已对齐 |
| 0x491cb8 | TFrmDlg_DDGridGridPaint | `TradePanelView.cs`: _Draw() 己方交易物品图标 | 已对齐 |
| 0x491dbc | TFrmDlg_DDGridGridMouseMove | `TradePanelView.cs`: OnGridMouseMove() 物品Tooltip | 已对齐 |
| 0x491e14 | TFrmDlg_DDRGridGridPaint | `TradePanelView.cs`: _Draw() 对方交易物品图标 | 已对齐 |
| 0x491f18 | TFrmDlg_DDRGridGridMouseMove | `TradePanelView.cs`: OnRemoteGridMouseMove() 对方物品Tooltip | 已对齐 |
| 0x491fb0 | TFrmDlg_DDGoldClick | `TradePanelView.cs`: OnChangeGoldClicked() 输入交易金币 | 已对齐 |
| 0x49219c | TFrmDlg_DUserState1DirectPaint | `StatusPanelView.cs`: _Draw() 角色防御魔御攻魔道四维数值 | 已对齐 |
| 0x492840 | TFrmDlg_DUserState1MouseDown | `StatusPanelView.cs`: OnMouseDown() 属性面板置顶 | 已对齐 |
| 0x4929c8 | TFrmDlg_DUserState1MouseMove | `StatusPanelView.cs`: OnMouseMove() 属性词条悬停 | 已对齐 |
| 0x4929e8 | TFrmDlg_DWeaponUS1MouseMove | `StatusPanelView.cs`: OnSlotHover() 装备耐久回显 | 已对齐 |
| 0x492ae8 | TFrmDlg_DCloseUS1Click | `StatusPanelView.cs`: OnCloseClicked() 关闭独立状态面板 | 已对齐 |
| 0x492b10 | TFrmDlg_DNecklaceUS1DirectPaint | `StatusPanelView.cs`: _Draw() 项链/手镯槽位极品属性高亮 | 已对齐 |
| 0x493844 | TFrmDlg_DGuildDlgDirectPaint | `GuildPanelView.cs`: _Draw() 行会主页与公告底板 | 已对齐 |
| 0x493a48 | TFrmDlg_DGDUpClick | `GuildPanelView.cs`: OnScrollUpClicked() 行会公告向上翻动 | 已对齐 |
| 0x493a8c | TFrmDlg_DGDDownClick | `GuildPanelView.cs`: OnScrollDownClicked() 行会公告向下翻动 | 已对齐 |
| 0x493acc | TFrmDlg_DGDCloseClick | `GuildPanelView.cs`: OnCloseClicked() 关闭行会主面板 | 已对齐 |
| 0x493afc | TFrmDlg_DGDHomeClick | `GuildPanelView.cs`: OnHomeClicked() 查看行会主页公告 | 已对齐 |
| 0x493b48 | TFrmDlg_DGDListClick | `GuildPanelView.cs`: OnMemberListClicked() 请求成员列表 | 已对齐 |
| 0x493b94 | TFrmDlg_DGDAddMemClick | `GuildPanelView.cs`: OnAddMemberClicked() 掌门人面对面招人 | 已对齐 |
| 0x493c50 | TFrmDlg_DGDDelMemClick | `GuildPanelView.cs`: OnDelMemberClicked() 开除成员或退出行会 | 已对齐 |
| 0x493d0c | TFrmDlg_DGDEditNoticeClick | `GuildPanelView.cs`: OnEditNoticeClicked() 掌门人编辑公告 | 已对齐 |
| 0x493d58 | TFrmDlg_DGDEditGradeClick | `GuildPanelView.cs`: OnEditRankClicked() 掌门人任免封号职位 | 已对齐 |
| 0x493dd8 | TFrmDlg_DGDAllyClick | `GuildPanelView.cs`: OnAllyClicked() 发起外交结盟申请 | 已对齐 |
| 0x493f58 | TFrmDlg_DGuildEditNoticeDirectPaint | `GuildPanelView.cs`: _Draw() 608x327 编辑公告窗口 | 已对齐 |
| 0x494030 | TFrmDlg_DGECloseClick | `GuildPanelView.cs`: OnNoticeCloseClicked() 关闭公告编辑 | 已对齐 |
| 0x494078 | TFrmDlg_DGEOkClick | `GuildPanelView.cs`: OnNoticeSubmitClicked() 保存并发布新公告 | 已对齐 |
| 0x49415c | TFrmDlg_DGDChatClick | `GuildPanelView.cs`: OnGuildChatClicked() 切换行会聊天频道 | 已对齐 |
| 0x4941d8 | TFrmDlg_DGoldDirectPaint | `GuildPanelView.cs`: _Draw() 行会基金金币显示 | 已对齐 |
| 0x494260 | TFrmDlg_DAdjustAbilCloseClick | `AdjustBonusPanelView.cs`: OnCloseClicked() 关闭加点面板 | 已对齐 |
| 0x494304 | TFrmDlg_DAdjustAbilityDirectPaint | `AdjustBonusPanelView.cs`: _Draw() 自由属性点分配界面底板 | 已对齐 |
| 0x494f5c | TFrmDlg_DPlusDCClick | `AdjustBonusPanelView.cs`: OnPlusDcClicked() 增加攻击/魔力点数 | 已对齐 |
| 0x4950a0 | TFrmDlg_DMinusDCClick | `AdjustBonusPanelView.cs`: OnMinusDcClicked() 减少属性点数 | 已对齐 |
| 0x4952a8 | TFrmDlg_DAdjustAbilOkClick | `AdjustBonusPanelView.cs`: OnConfirmClicked() 确认自由属性点分配 | 已对齐 |
| 0x4952e8 | TFrmDlg_DAdjustAbilityMouseMove | `AdjustBonusPanelView.cs`: OnMouseMove() 加点悬停提示 | 已对齐 |
| 0x4953e0 | TFrmDlg_DSServer1DirectPaint | `SelectServerView.cs`: _Draw() 服务器分区列表子项绘制 | 已对齐 |

## 批量点名追加真名（ida_all_remaining.json）
- 0x458D3C = Packet_Build
- 0x467920 = ProcessServerMessage
- 0x46DAE8 = handle_sm_open_shop
- 0x46DB28 = handle_sm_shop_items
- 0x47D864 = SceneManager_LoginNewChgPw
- 0x48A9BC = TFrmDlg_DSWWeaponClick
- 0x48B2A0 = TFrmDlg_DBottomDirectPaint
- 0x48C02C = TFrmDlg_DBelt1Click
- 0x48C188 = TFrmDlg_DBelt1DblClick
- 0x48DC24 = TFrmDlg_DItemBagDirectPaint
- 0x48EAC0 = TFrmDlg_DMerchantDlgDirectPaint
- 0x48F020 = TFrmDlg_DMenuDlgDirectPaint
- 0x48FC14 = TFrmDlg_DMenuPrevClick
- 0x48FCB4 = TFrmDlg_DMenuNextClick
- 0x4907D8 = TFrmDlg_DKeySelDlgDirectPaint
- 0x490DFC = TFrmDlg_DGroupDlgDirectPaint
- 0x491008 = TFrmDlg_DGrpAllowGroupDirectPaint
- 0x49219C = TFrmDlg_DUserState1DirectPaint
- 0x494304 = TFrmDlg_DAdjustAbilityDirectPaint
