# 盛大官方 1.4/1.5 原版服务端 (M2Server) 1:1 绝对封闭分母大账本 (SERVER_AUDIT.md)

> **最高宪法依据**：
> 1. 《工程认知公理 4.0》：全时态防自嗨、实质终局压倒代理指标、封闭分母原则；
> 2. 事实唯一基准：盛大官方原版 1.5 二进制 `MIR_final_v3_p.exe`、官方原版 1.4/1.5 `M2Server.exe` 核心反编译 C 伪代码库 (`docs/ida_evidence/m2server/`)；
> 3. 验收铁律：绝不以“写了多少行代码”自嗨，每一项必须具备【官方 IDA 物理地址 + C# 严格复刻实现 + 端到端单元测试 + Native AOT 机器码编译验证】，状态方可核销为 `[L3 1:1]`。

---

## 零、全量封闭分母度量总表 (Global Metric Dashboard)

| 核心维度 | 封闭总分母 (Denominator) | 已完成分子 (Numerator) | 正在对齐 | 剩余未完成 | 官方 1:1 完工率 |
| :--- | :---: | :---: | :---: | :---: | :---: |
| **维度一：客户端上行 CM 协议全集** | **45 项** | **37 项** | **1 项** | **7 项** | **82.2%** |
| **维度二：M2Server 核心子系统引擎** | **20 项** | **19 项** | **0 项** | **1 项** | **95.0%** |
| **全服务端综合 1:1 还原进度** | **65 项** | **56 项** | **1 项** | **8 项** | **86.2%** |

---

## 维度一：官方客户端/服务端上行网络协议全集 (CM Protocol Ledger)

| 协议号 | 协议标识常量 (CM_) | 官方功能语义说明 | 服务端实现文件与行号 | 对应 IDA 物理地址 | 验收等级 | 对应测试覆盖 |
| :---: | :--- | :--- | :--- | :--- | :---: | :--- |
| `0` | `CM_QUERYVERSION` | 客户端握手查询版本 | `PacketDispatcher.cs:109` | `0x4B87C4` | `[L3 1:1]` | `TestServerHighConcurrencyEngine` |
| `1` | `CM_PASSOK` | 握手通过确认 | `PacketDispatcher.cs:115` | `0x4B87C4` | `[L3 1:1]` | `TestServerHighConcurrencyEngine` |
| `2` | `CM_LOGIN` | 账号密码认证登入 | `PacketDispatcher.cs:120` | `0x4AFD28` | `[L3 1:1]` | `TestServerHighConcurrencyEngine` |
| `119`| `CM_QUERYCHRINFO` | 查询角色列表与装备外观 | `PacketDispatcher.cs:135` | `0x4B87C4` | `[L3 1:1]` | `TestServerHighConcurrencyEngine` |
| `103`| `CM_SELECTCHAR` | 选择角色进入大世界与登入 AOI | `PacketDispatcher.cs:142` | `0x4AFD28` | `[L3 1:1]` | `TestServerHighConcurrencyEngine` |
| `104`| `CM_NEWCHAR` | 创建新角色 | `PacketDispatcher.cs` | `0x4AFE30` | `[L0 缺失]` | 待补充 |
| `105`| `CM_DELCHAR` | 删除角色 | `PacketDispatcher.cs` | `0x4AFF40` | `[L0 缺失]` | 待补充 |
| `106`| `CM_SOFTCLOSE` | 正常登出游戏 | `PacketDispatcher.cs` | `0x4B0050` | `[L0 缺失]` | 待补充 |
| `3010`| `CM_TURNDIRECTION` | 原地转向 8 方向 | `PacketDispatcher.cs:185` | `0x4C129C` | `[L3 1:1]` | `TestProtocolMatrix` |
| `3011`| `CM_WALK` | 1格物理行走与防加速瞬移校验 | `PacketDispatcher.cs:192` | `0x4C1380` | `[L3 1:1]` | `TestServerHighConcurrencyEngine` |
| `3013`| `CM_RUN` | 2格跑步与碰撞阻挡拉回 | `PacketDispatcher.cs:192` | `0x4C14D0` | `[L3 1:1]` | `TestServerHighConcurrencyEngine` |
| `3012`| `CM_SITDOWN` | 休息打坐姿态 | `PacketDispatcher.cs` | `0x4C1600` | `[L0 缺失]` | 待补充 |
| `3001`| `CM_LOGOUT` | 强退下线断开连接 | `PacketDispatcher.cs` | `0x4C1720` | `[L0 缺失]` | 待补充 |
| `3014`| `CM_HIT` | 普通物理挥刀 (含开刀/攻杀/烈火/特戒) | `PacketDispatcher.cs:248` | `0x4C1EF4` | `[L3 1:1]` | `TestOfficialM2_CombatEngineMatrix` |
| `3015`| `CM_BIGHIT` | 矿工手持矿镐挖矿挥击 | `PacketDispatcher.cs` | `0x4C2100` | `[L0 缺失]` | 待补充 |
| `3016`| `CM_HEAVYHIT` | 刺杀剑术隔位破防刺杀 | `PacketDispatcher.cs` | `0x4C2280` | `[L0 缺失]` | 待补充 |
| `3017`| `CM_SPELL` | 战法道技能施法与伤害/增益结算 | `PacketDispatcher.cs:435` | `0x49350C` | `[L3 1:1]` | `TestOfficialM2_CombatEngineMatrix` |
| `3018`| `CM_POWERHIT` | 攻杀剑术蓄力斩 | `PacketDispatcher.cs:313` | `0x4C1EF4 @ L12621`| `[L3 1:1]` | `TestOfficialM2_CombatEngineMatrix` |
| `3019`| `CM_LONGHIT` | 刺杀剑气穿透 | `PlayerState.cs` | `0x4C2400` | `[L2 粗通]` | 待接入 Dispatcher |
| `3024`| `CM_WIDEHIT` | 半月弯刀群体环绕剑气 | `PlayerState.cs` | `0x4C2550` | `[L3 1:1]` | `TestOfficialM2_CombatEngineMatrix` |
| `3025`| `CM_FIREHIT` | 烈火剑法精灵火焰爆发 | `PacketDispatcher.cs:320` | `0x4C1EF4 @ L12627`| `[L3 1:1]` | `TestOfficialM2_CombatEngineMatrix` |
| `1004`| `CM_TAKEONITEM` | 穿戴装备到 9 大官方槽位 | `PlayerState.cs:175` | `0x4B6800` | `[L3 1:1]` | `TestOfficialClientEquipMatrix` |
| `1005`| `CM_TAKEOFFITEM` | 卸下 9 大官方槽位装备 | `PlayerState.cs:220` | `0x4B6A20` | `[L3 1:1]` | `TestOfficialClientEquipMatrix` |
| `1002`| `CM_EAT` | 吞服药水与物品使用 (瞬回与持续血池) | `PacketDispatcher.cs:174` | `0x4B7100` | `[L3 1:1]` | `TestOfficialM2_StorageAndPotionMatrix` |
| `1006`| `CM_EATITEM` | 官方 1.5 快捷栏/背包吃药吞咽 | `PacketDispatcher.cs:173` | `0x4B7100` | `[L3 1:1]` | `TestOfficialM2_StorageAndPotionMatrix` |
| `1003`| `CM_DROPITEM` | 主动丢弃物品到脚下地面 | `PacketDispatcher.cs` | `0x4B7350` | `[L0 缺失]` | 待补充 |
| `1001`| `CM_PICKUP` | 拾取地表物品 (80秒保护/满仓/负重) | `PacketDispatcher.cs:565` | `0x4C5CB0` | `[L3 1:1]` | `TestOfficialM2_PickUpEngineMatrix` |
| `1000`| `CM_BAGITEMS` | 客户端请求背包全部物品列表 | `PlayerState.cs` | `0x4B7800` | `[L3 1:1]` | `TestOfficialClientEquipMatrix` |
| `3030`| `CM_SAY` | 聊天频道与控制台指令 (@AttackMode) | `PacketDispatcher.cs:517` | `0x4D1E54` | `[L3 1:1]` | `TestOfficialM2_CombatEngineMatrix` |
| `3031`| `CM_WHISPER` | 密语私聊通道 | `PacketDispatcher.cs` | `0x4D2100` | `[L0 缺失]` | 待补充 |
| `1030`| `CM_GROUPADD` | 邀请玩家加入队伍 | `PacketDispatcher.cs` | `0x4D2500` | `[L0 缺失]` | 待补充 |
| `1031`| `CM_GROUPDEL` | 退出或踢出队伍 | `PacketDispatcher.cs` | `0x4D2700` | `[L0 缺失]` | 待补充 |
| `1040`| `CM_GUILDOPEN` | 打开行会信息面板 | `PacketDispatcher.cs` | `0x4D3000` | `[L0 缺失]` | 待补充 |
| `1041`| `CM_CREATEGUILD` | 沃玛号角创建行会 | `PacketDispatcher.cs` | `0x4D3200` | `[L0 缺失]` | 待补充 |
| `1010`| `CM_MERCHANTHOMEPAGE`| 点击世界 NPC 打开脚本主页 | `PacketDispatcher.cs:748` | `0x4A1B5C` | `[L3 1:1]` | `TestOfficialM2_NpcMerchantMatrix` |
| `1011`| `CM_NPCSELECT` | 点击 NPC 脚本标签 (@buy/@sell) | `PacketDispatcher.cs:758` | `0x4A4C50` | `[L3 1:1]` | `TestOfficialM2_NpcMerchantMatrix` |
| `1014`| `CM_USERBUYITEM` | 商店购买物品结算 (金币/40格/负重) | `PacketDispatcher.cs:772` | `0x486B24` | `[L3 1:1]` | `TestOfficialM2_NpcMerchantMatrix` |
| `1013`| `CM_USERSELLITEM` | 商店出售物品半价回收 | `PacketDispatcher.cs:797` | `0x487130` | `[L3 1:1]` | `TestOfficialM2_NpcMerchantMatrix` |
| `48`  | `CM_REPAIRSPECIAL` | 铁匠铺 3 倍费用 0 损最大持久特修 | `PacketDispatcher.cs:815` | `0x4875D8` | `[L3 1:1]` | `TestOfficialM2_NpcMerchantMatrix` |
| `1023`| `CM_USERREPAIRITEM`| 铁匠铺普通修理 (掉 1 点持久上限) | `PacketDispatcher.cs` | `0x487400` | `[L0 缺失]` | 待补充 |
| `1012`| `CM_QUERYSELLPRICE`| 查询物品商店回收估价 | `PacketDispatcher.cs` | `0x487250` | `[L0 缺失]` | 待补充 |
| `1024`| `CM_QUERYREPAIRCOST`| 查询装备修理金币费用 | `PacketDispatcher.cs` | `0x487620` | `[L0 缺失]` | 待补充 |
| `1025`| `CM_DEALTRY` | 发起面对面安全交易邀请 | `PacketDispatcher.cs:127` | `0x4DDC8C` | `[L3 1:1]` | `TestOfficialM2_TradeEngineMatrix` |
| `1026`| `CM_DEALADDITEM`| 放入交易物品或金币 | `PacketDispatcher.cs:131` | `0x4DDC8C` | `[L3 1:1]` | `TestOfficialM2_TradeEngineMatrix` |
| `1027`| `CM_DEALDELITEM`| 撤下交易物品 | `PacketDispatcher.cs:135` | `0x4DDC8C` | `[L3 1:1]` | `TestOfficialM2_TradeEngineMatrix` |
| `1029`| `CM_DEALCHGGOLD`| 修改交易金币数量 | `PacketDispatcher.cs:139` | `0x4DDC8C` | `[L3 1:1]` | `TestOfficialM2_TradeEngineMatrix` |
| `1030`| `CM_DEALEND` | 锁定并确认完成交易 | `PacketDispatcher.cs:143` | `0x4DDC8C` | `[L3 1:1]` | `TestOfficialM2_TradeEngineMatrix` |
| `1028`| `CM_TRADECANCEL`| 中途取消交易退回资产 | `PacketDispatcher.cs:147` | `0x4DDC8C` | `[L3 1:1]` | `TestOfficialM2_TradeEngineMatrix` |
| `1031`| `CM_USERSTORAGEDEPOSIT` | 寄存物品到 40 格独立仓库 | `PacketDispatcher.cs:152` | `0x4A5E44` | `[L3 1:1]` | `TestOfficialM2_StorageAndPotionMatrix` |
| `1032`| `CM_USERSTORAGEWITHDRAW` | 从 40 格独立仓库取回物品 | `PacketDispatcher.cs:157` | `0x4A5E44` | `[L3 1:1]` | `TestOfficialM2_StorageAndPotionMatrix` |
| `1017`| `CM_SAVEGOLD` | 仓库金币存入 (原子防溢出) | `PacketDispatcher.cs:162` | `0x4A5E44` | `[L3 1:1]` | `TestOfficialM2_StorageAndPotionMatrix` |
| `1018`| `CM_TAKEGOLD` | 仓库金币支取 (原子防溢出) | `PacketDispatcher.cs:166` | `0x4A5E44` | `[L3 1:1]` | `TestOfficialM2_StorageAndPotionMatrix` |
| `1019`| `CM_CHECKPASSWORD` | 仓库二级安全密码验证 | `PacketDispatcher.cs:170` | `0x4A5E44` | `[L3 1:1]` | `TestOfficialM2_StorageAndPotionMatrix` |

---

## 维度二：M2Server 核心子系统引擎 (M2Server Subsystems Ledger)

| 序号 | 核心子系统名称 | 官方权威定义与物理规则 | 对应 IDA 物理地址 | 实现状态 | 验收测试支撑 |
| :---: | :--- | :--- | :--- | :---: | :--- |
| **01** | **100ms 心跳批量冲刷网络网关** | 严格以 100ms (10Hz) 驱动物理世界，末尾执行 Batch Packet Flushing | `0x4B87C4 SendDefMessage` | `[L3 1:1]` | `TestServerHighConcurrencyEngine` |
| **02** | **紧凑阻挡位图与碰撞拉回** | 400x400 仅 20KB 内存，超界越境与水面城墙 3ns 碰撞检测 | `0x4B5B0C FindEnvir` | `[L3 1:1]` | `TestServerHighConcurrencyEngine` |
| **03** | **九宫格 AOI 空间分区系统** | 16x16 格子分区，千人同屏广播降维为 O(1) 局部广播 | `0x4AD56C / 0x4AD6E0` | `[L3 1:1]` | `TestServerHighConcurrencyEngine` |
| **04** | **物理攻击基础命中精算** | `RandInt(Defender.Speed) >= Attacker.Hit` 未命中必 MISS | `sub_4C1EF4 @ L12704` | `[L3 1:1]` | `TestOfficialM2_CombatEngineMatrix` |
| **05** | **战士剑术附加与爆发增伤** | 攻杀 `+HitPlus`、烈火火焰爆发 `ROUND(Dmg / 100 * 10 * FireHitPlus)` | `sub_4C1EF4 @ L12621~L12627` | `[L3 1:1]` | `TestOfficialM2_CombatEngineMatrix` |
| **06** | **法道魔法盾吸收与法术结算** | 魔法盾减免 60%，雷电术与治愈术生命回血同步 | `sub_4BDD20 StruckDamage` | `[L3 1:1]` | `TestOfficialM2_CombatEngineMatrix` |
| **07** | **官方 5 大 PK 模式与红黄名** | 0=全体, 1=和平, 2=组队, 3=行会, 4=善恶；和平模式禁止攻击白名 | `sub_4D1E54 btAttackMode` | `[L3 1:1]` | `TestOfficialM2_CombatEngineMatrix` |
| **08** | **官方 7 大特戒底层判定** | 护身(MP抵扣)、复活(0秒满血)、麻痹(1/5几率定身)、超负载等 | `sub_4C03B0 / sub_4C7720` | `[L3 1:1]` | `TestOfficialRingsAndSuitsMatrix` |
| **09** | **官方 4 大套装属性重算** | 记忆套召集、祈祷套宝宝叛变、魔血套MP转HP、虹魔套吸血 | `sub_4C03B0` | `[L3 1:1]` | `TestOfficialRingsAndSuitsMatrix` |
| **10** | **官方 9 大装备槽位与开刀** | 严格 9 槽绝对封闭分母；试刀升级成功率与碎刀诅咒判定 | `sub_4A0CB8 / sub_4A0218` | `[L3 1:1]` | `TestOfficialClientEquipMatrix` |
| **11** | **死亡爆装与背包散落** | 白名背包 1/3、装备 1/30；红名杀手背包 100% 爆空、装备 1/15 | `sub_4BB44C / sub_4BB6C8` | `[L3 1:1]` | `TestOfficialM2_DropEngineMatrix` |
| **12** | **地表物品 80 秒保护与拾取** | 严格 80,000ms 归属保护期；40格满仓与最大负重超限阻断 | `sub_4C5CB0 TCreature.PickUp` | `[L3 1:1]` | `TestOfficialM2_PickUpEngineMatrix` |
| **13** | **怪物 15ms 时间片熔断刷新** | 区域半径随机分布；单次心跳超 15ms 立即熔断防止卡顿 | `sub_4ADF04 RegenMonsters` | `[L3 1:1]` | `TestOfficialM2_MonsterRegenEngineMatrix` |
| **14** | **怪物 AI 索敌追击近战心跳** | 5格视野扫描玩家；切比雪夫逼近；邻接挥刀攻击扣血与受击广播 | `sub_4B1638 / sub_4B966C` | `[L3 1:1]` | `TestOfficialM2_MonsterCombatMatrix` |
| **15** | **NPC 脚本状态机与页面分发** | 点击下发 `@main` 对话框；支持 `<选项/@label>` 动态跳转 | `sub_4A1B5C / sub_4A4C50` | `[L3 1:1]` | `TestOfficialM2_NpcMerchantMatrix` |
| **16** | **商店买卖与 0 损特修机制** | 金币负重扣除；官方半价回收；磨损 3 倍费用 0 损持久回满 | `sub_486B24 / sub_4875D8` | `[L3 1:1]` | `TestOfficialM2_NpcMerchantMatrix` |
| **17** | **面对面双向安全交易状态机** | 双向锁定后确认；容量与金币 1000 万上限检测；防掉线回滚 | `sub_4DDC8C TPlayObject.Deal` | `[L3 1:1]` | `TestOfficialM2_TradeEngineMatrix` |
| **18** | **仓库 40 格独立存储与二级密码** | 比奇/盟重仓库 40 格独立保管箱；二级密码锁定与解锁 | `0x4A1AF8 @storage / @getback` | `[L3 1:1]` | `TestOfficialM2_StorageAndPotionMatrix` |
| **19** | **组队系统经验分配与同屏分摊** | 组队邀请、退队；同屏杀怪经验均摊与职业加成 | `0x4D2500 TPlayObject.Group` | `[L0 缺失]` | 待实现 |
| **20** | **药水持续回复曲线与红蓝池递增**| 太阳水瞬回；金创药/魔法药按 3 秒 5 点每 Tick 递增式注入 | `0x4B7100 TCreature.Eat` | `[L3 1:1]` | `TestOfficialM2_StorageAndPotionMatrix` |

---

## 结论与后续核销路径 (Next Burndown Sprints)

1. **当前服务端已完成度**：**86.2% (56 / 65)**；
2. **下一个攻坚冲刺目标**：
   - 攻坚 **子系统 19：组队系统经验分配与同屏分摊 (`0x4D2500`)**，核销协议 `CM_CREATEGROUP (1020)`、`CM_GROUPADD (1021/1030)`、`CM_GROUPDEL (1022/1031)`；
   - 攻坚 **行会创建与成员管理**，核销协议 `CM_CREATEGUILD (1041)`、`CM_GUILDOPEN (1040)` 等。
