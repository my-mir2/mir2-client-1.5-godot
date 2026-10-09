# 盛大官方 1.5 客户端 100% 绝对物理细节还原与封闭全集总攻计划书

## 一、核心战役目标 (Goal)
依据盛大官方原版二进制（`MIR_final_v3_p.exe`）IDA 逆向元数据与官方 WIL/WIX 资产规范，**彻底抛开服务端逻辑干扰与纯测试代理指标，以客户端为核心，一次性 1:1 完整实现官方客户端的全部 5 大封闭物理分母，实现 100% 细节穷尽无遗漏**。

---

## 二、五大封闭分母与战役清单 (Closed Master Plan)

### 战役 1：原生 24 窗体与全部子控件微交互 1:1 闭环 (UI 全集)
- [x] 1.1 【已闭环】登录、注册、改密、选服双龙石碑 1:1 交互：LineEdit 原生 500ms 光标闪烁、12字符上限限制、回车键聚焦密码与触发登录、密码遮罩（在 LoginView.cs 落地，提交 2feaf87）；
- [x] 1.2 【已闭环】神龛选角与创角：石台烛火 4 帧循环明暗正弦波呼吸、创角面板职业性别实时联动全身立绘预览、删角模态提示与按键状态机（在 SelectCharView.cs 落地，提交 14e143c）；
- [x] 1.3 【已闭环】HUD 底栏与仪表盘：血球魔球半透明玻璃罩高光层贴图覆盖 (ZIndex=2)、经验条亮青蓝渲染、负重条超重鲜红警示高亮、快捷键空槽暗影（在 HudController.cs 落地，提交 6817310）；
- [x] 1.4 【已闭环】背包 40 格与装备 9 槽位微交互：右键点击背包物品直接吃药或快捷穿戴、右键穿戴装备直接脱下放回背包、持久度归零鲜红警示字、神兵重装属性门槛拦截、负重腕力校验（在 HudController.cs 落地，提交 2feaf87, c4f23b3, d182847）；
- [x] 1.5 【已闭环】状态、技能、加点、快捷键绑定面板 1:1 坐标与点击反馈：9 大属性增减微调、剩余点数返还、重置全部点数、技能魔法书双向绑定弹窗拦截（在 AdjustAbilPanelView.cs 与 KeySelectDialogView.cs 落地，提交 c4f23b3）；
- [x] 1.6 【已闭环】社交与交易微交互：组队成员血量百分比实时渲染与低血量变红警示、Ctrl+E 快捷切换组队模式、面对面交易双方 10 格与防偷换锁定状态机（在 GroupPanelView.cs 与 TradePanelView.cs 落地，提交 c4f23b3）；
- [x] 1.7 【已闭环】商业与功能：商店货架批量购买数量输入框 (1~99)、装备特修耐久折损与二次确认弹窗、NPC 商店买卖与修理交互挂接（在 ShopPanelView.cs 与 RepairPanelView.cs 落地，提交 6817310）；
- [x] 1.8 【已闭环】对话、地图与风控：NPC 富文本下划线超链接 (NpcDialogView sub_48EAC0)、小地图雷达三色点与 540x360 全景大地图 (MinimapView sub_46FF0C)、二级安全密码弹窗 (HudController.ShowSecurityPasswordDialog)、死亡灰屏倒计时与复活确认框 (DeathScreenView sub_481DD4/360)；
- [x] 1.9 【已闭环】WindowStackManager 角色移动强互斥（StepStarted 时自动关闭交易、仓库、商店、NPC 对话框，提交 e4eb4d2）；
- [x] 1.10 【已归集】Official24DialogCompendium 原生 24 窗体 SetLeft/SetTop/SetWidth/SetHeight 物理坐标与 BaseFrame 贴图全量字典封装（提交 b087e14）。

### 战役 2：战法道 32 门技能视听动画、弹道与时序全集 (技能全集)
- [x] 2.1 【已闭环】战士 6 技能：攻杀蓝色刀光残影、刺杀隔位破防亮蓝剑气、半月弯刀扇形 4 格切片、野蛮冲撞撞退与撞硬直时钟、烈火剑法 8 秒蓄力刀刃火精灵（在 OfficialMagicCompendium 与 MagicManager 落地，提交 314bd31）；
- [x] 2.2 【已闭环】法师 14 技能：雷电术天雷直击、火墙地面 3 帧烈火翻滚与持续铺设、地狱雷光全屏 8 角雷光齐发、疾光电影 12 格直线电光贯穿、魔法盾金黄椭圆罩常驻护盾、起手与爆炸音效调度（在 MagicManager 落地，提交 314bd31）；
- [x] 2.3 【已闭环】道士 12 技能：治愈术神圣绿光上升、灵魂火符旋转飞行弹道与命中爆炸、幽灵盾/神圣战甲术八卦护体、困魔咒金黄八卦光柱锁死、起手受击音效调度（在 MagicManager 落地，提交 314bd31）；
- [x] 2.4 【已归集】OfficialMagicCompendium 32 门技能 Magic.wil / Magic2.wil 帧切片与 Sound.wil 权威音效全量元数据字典（提交 b087e14）；
- [x] 2.5 【已闭环】MagicManager 针对 32 门技能特化渲染管线：火墙翻滚、地狱雷光 8 向齐发、疾光电影贯穿、魔法盾附着、弹道碰撞与音效分派（提交 314bd31）。

### 战役 3：155 个下行 SM 消息客户端本地状态机全集 (协议全集)
- [x] 3.1 实体生命周期分发（已通过 _entityManager.UpdateMove/TriggerDeath/AddEntity/RemoveEntity 实质接入）；
- [x] 3.2 动作与伤害广播（已通过 _entityManager.TriggerHit/TriggerStruck 实质接入）；
- [x] 3.3 背包、资产与属性同步（已通过 _hud.SetBagItem/ClearBagItem/UpdateGold/UpdateLevel/UpdateExp/UpdateHp/UpdateMp/UpdateWeight 实质接入）；
- [x] 3.4 交互与社交回执（已通过 _hud.ShowNpcDialog/ShowShop/OpenTrade/OpenStorageList/OpenInspect/ToggleGuild 实质接入）；
- [x] 3.5 环境与地图切换（已通过 _hud.AddChat/GameView.PlayerInstance.SetTilePos/_entityManager.Clear 实质接入）；
- [x] 3.6 【实质闭环】Official15ProtocolDispatcher 彻底重构消灭空壳注释，真实驱动 UI、实体与大世界生命周期（提交 c2a6817）。

### 战役 4：角色/怪物 8 方向动作骨骼与地图环境生态 (表现层全集)
- [x] 4.1 【已闭环】主角 8 方向动作切片：站立(4帧)、走(6帧)、跑(6帧)、普攻(6帧)、施法(6帧)、受击(2帧)、倒地(4帧) 官方帧率对齐，并在 PlayerNode 落地 PlayButcher 挖肉割刀动作与 40 号割肉音效调度 (提交 14e143c)；
- [x] 4.2 【已闭环】衣服（Hum.wil）、武器（Weapon.wil）、发型（Hair.wil）8 方向层叠顺位：在 PlayerNode.cs 中由 SpriteEngine.IsWeaponInFront 判定朝向前置后置并动态调整 Z-Index（前置 1 / 后置 -1）；
- [x] 4.3 【已闭环】80 种官方怪物生态与 Alt 挖肉：在 EntityNode.cs 落地 TryHarvestMeat 尸体挖肉机制，割取肉块物品并由 DelphiRandom 浮动品质，调用 40 号割肉音效（提交 a703302）；
- [x] 4.4 【已闭环】地图动态瓦片：在 MapRenderer.cs 落地官方 6 FPS (166ms) 定频时钟与 AniFrame/AniTick 动态物件帧偏移计算 (水流/火盆/旗帜等)，消除黑底倒置与安全区光晕混合 (提交 4faec2b)；
- [x] 4.5 【已归集】OfficialMonsterCompendium 80 种怪物独立音效、帧率与尸体保留时间字典（提交 b087e14）。

### 战役 5：装备隐藏属性、随机数、持久与经济规则全集 (本地规则全集)
- [x] 5.1 【已闭环】9 槽位佩戴属性门槛判定与持久衰减公式：角色换装时由 CheckEquipRequirement 真实拦截等级与攻击力/魔法力/道术力不足条件（在 HudController 中真实生效），以及铁匠铺特修/普修耐久折损公式（提交 d182847）；
- [x] 5.2 【已闭环】攻击力、魔法力、道术力上下限 Delphi Random 命中随机数波动：由 DelphiRandom.Shared 线性同余发生器提供原版分布（提交 a703302）；
- [x] 5.3 【已闭环】盛大官方 1.5 七大特戒判定：麻痹 1/15 几率麻痹、护身 MP 吸收伤害、复活瞬间满血、隐身、传送、超负载、火焰戒指（在 OfficialClientRulesEngine.cs 落地，提交 d182847）；
- [x] 5.4 【已闭环】药水双轨回复曲线：由 PotionMetabolism 与 PotionSystem 真实执行 250ms 定频注入，生命球平滑上涨（提交 ac71152）；
- [x] 5.5 【已归集】OfficialClientRulesEngine 本地数值与 Delphi 伪随机数发生器封装（提交 b087e14）。

### 战役 6：官方 1.5 深水区微按键交互与防误触盲区排查 (盲区清零全集)
- [x] 6.1 【已闭环】退出与小退防误触二次确认：Alt+X (TFrmDlg_DBotExitClick 0x491520) 弹出“你真的想要退出游戏吗？”模态确认框；Alt+Q (TFrmDlg_DBotLogoutClick 0x491474) 弹出“你真的想要重新选择角色吗？”模态确认框（在 HudController.cs 落地，提交 3e90aa4）；
- [x] 6.2 【已闭环】PrintScreen / Pause 全屏截屏：将当前视口渲染图保存为 Images/Mir_yyyyMMdd_HHmmss.png，并在聊天栏飘黄字“游戏截图已保存至 Images 目录”，调度 103 号按钮点击声（在 GameView.cs 落地，提交 3e90aa4）；
- [x] 6.3 【已闭环】道士 Ctrl+A 召唤兽宝宝控制闭环：连接 InputPlugin.PetCommandRequested 信号，切换休息/攻击状态并飘绿字“[系统] 召唤兽进入休息/攻击状态。”（在 GameView.cs 落地，提交 3e90aa4）；
- [x] 6.4 【已闭环】战士快捷键冲突纠正：修正 InputPlugin.cs 中 Ctrl+B 原错误绑为包裹打开，对齐官方 1.5 纠正为半月弯刀开关 (ToggleHalfMoonRequested)，并增设 Ctrl+F 刺杀剑术开关 (ThrustingToggleRequested) 路由打通（在 InputPlugin.cs 落地，提交 3e90aa4）。

---

## 三、终局交付标准
1. 所有 5 大维度代码全面合入工程主目录；
2. 消除任何未实现的存疑协议与窗体空洞；
3. 官方原版二进制所有核心表现与逻辑 1:1 就绪。
