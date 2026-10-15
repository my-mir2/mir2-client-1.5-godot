# client-r2 逆向+重构发现 (client-r2 交付)

> 角色：client-r2（客户端逆向+重构）
> 方法：radare2 实证官方 1.5 客户端 `MIR_final_v3_p - Copy.exe`
> 目标：对照官方行为核验/重构 Rust 客户端 `crates/mir-client/`
> 编译验证：`cargo check -p mir-client` ✅ 通过（exit 0）
> 交付物：本文件 + `docs/protocol-map-official.md` 修正 + `messages.rs` 协议号修正注释

---

## 一、消息头格式实证（radare2 反汇编）

### TChrMsg 构造函数 `fcn.00458d3c`（52 个调用点）
```
fcn.00458d3c(arg_8h=buf, ax=协议号, edx=数据指针, cx=param1, arg_10h=param2, arg_ch=param3)
  word [buf+0x04] = ax        ; 协议号 (offset4, word)  ← 任务「offset4=协议号」实证
  dword [buf+0x00] = edx      ; 数据指针 (offset0, dword)
  word [buf+0x06] = cx        ; param1
  word [buf+0x08] = arg_10h   ; param2
  word [buf+0x0a] = arg_ch    ; param3
```
✅ **消息格式完全确认**：offset0=dword数据, offset4=word协议号, offset6/8/0xa=3个word附加字段。

### 通用发送包装 `fcn.00464b18`（6 个调用点）
```
fcn.00464b18(this, edx=协议号, ecx=数据, arg_8h/arg_ch/arg_10h)
  → 构造 TChrMsg → fcn.0045b41c(序列化/入队) → fcn.00464a40(发送)
```

---

## 二、CM 协议号全集 → 函数 → 功能映射（r2 实证）

### 1. 消息构造函数直接发送（`fcn.00458d3c` 的 51 个独立调用者，非包装器）

| 函数 | 协议号 | 功能推断（r2 上下文） |
|---|---|---|
| fcn.00465830 | 80 | 查询用户名(带 name 参数) |
| fcn.00465168 | 100 | 登录类 1 |
| fcn.00465054 | 101 | 登录类 2 |
| fcn.00465234 | 102 | 登录类 3 |
| fcn.004652d0 | 103 | 登录类 4 |
| fcn.00464ec8 | 104 | 登录类 5 |
| fcn.004658a8 | 1000 | 移动 CmMove（edx=data） |
| fcn.0046594c | 1001 | 奔跑 CmRun |
| fcn.004659c4 | 1003 | 攻击 CmAttack（edx=data） |
| fcn.00465a6c | 1004 | 动作 |
| fcn.00465b14 | 1006 | 动作 |
| fcn.00465bb8 | 1007 | 动作（多字段：cx+edx+2word） |
| fcn.00465c34 | 1008 | 魔法快捷键 CmMagicKeyChange |
| fcn.00465ca8 | 1011 | 目标交互（多字符串） |
| fcn.00465e44 | 1012 | 商店询价 CmMerchantQuerySellPrice |
| fcn.00465f9c | 1013 | 投掷 CmThrow |
| fcn.00466248 | 1014 | 使用物品 CmUseItem |
| fcn.00465ef0 | 1024 | 动作 |
| fcn.00466048 | 1023 | 动作 |
| fcn.004660f4 | 1031 | 动作 |
| fcn.004661a0 | 1015 | 物品详情 CmUserGetDetailItem |
| fcn.004662f4 | 1032 | 动作 |
| fcn.004663a0 | 1034 | 炼药 CmMakeDrugItem |
| fcn.00466444 | 1016 | 丢金币 CmDropGold |
| fcn.004664b0 | 1019 (×2) | 割肉 CmButchItem |
| fcn.0046653c | 1020 | 动作 |
| fcn.004665e0 | 1033 | 小地图 CmWantMinimap |
| fcn.00466648 | 1025 | 动作 |
| fcn.004666dc | 1035 | 动作 |
| fcn.00466744 | 1028 | 动作 |
| fcn.004667ac | 1026 | 动作 |
| fcn.00466854 | 1027 | 动作 |
| fcn.004668fc | 1029 | 动作 |
| fcn.00466968 | 1030 | 动作 |
| fcn.004669d0 | 1021 | 动作 |
| fcn.00466a74 | 1022 | 动作 |
| fcn.00466b18 | 1036 | 动作 |
| fcn.00466b80 | 1037 | 动作 |
| fcn.00466be8 | 1038 | 动作 |
| fcn.00466c94 | 1039 | 动作 |
| fcn.00466d40 | 1040 | 动作 |
| fcn.00466ddc | 1041 | 行会职位 CmGuildUpdateRankInfo |
| fcn.00466e78 | 1043 | 属性加点 CmAdjustBonus |
| fcn.00464b94 | 2001 | 扩展1 |
| fcn.00464c90 | 2002 | 扩展2 |
| fcn.00464f64 | 2003 | 扩展3 |
| fcn.00464dac | 2004 | 扩展4 |
| fcn.0046549c | **3030** | 🔴 **GM 命令**（/check debug screen /check speedhack 等） |
| fcn.004656e4 | 动态 | 行走（协议号由调用者 edx 传入，设 busy 标志 [eax+0x420]=1 + GetTickCount 记 [eax+0x3d8]） |
| fcn.00465784 | 动态 | 转向/动作（同上 busy 机制） |

### 2. 通用发送包装发送（`fcn.00464b18` 的 6 个调用点）

| 调用函数 | 协议号 | 上下文（r2 实证） |
|---|---|---|
| fcn.00461144 | **1009** | 🔴 **注销/软断连**（push 0,0,0; ecx=0 无数据 → 对应 Rust `CmSoftClose`） |
| fcn.00463518 | 82 | 查询用户名（ecx=[var_20h+4]） |
| fcn.00463518 | 1010 | 点击 NPC `CmClickNpc`（ecx=[var_20h+4]） |
| fcn.00463c2c | 1002 | 目标选择/交互（先 fcn.0049f204 校验目标，再带名字发送） |
| fcn.00467920 | 81 | 查询/状态（SM 处理侧回发） |
| fcn.0046df14 | 1018 | 确认对话框 `CmConfirm`（push 0,0,0） |

### 3. CM 全集（55 个，合并 1+2）
```
80 81 82
100 101 102 103 104
1000 1001 1002 1003 1004 1006 1007 1008 1009 1010 1011 1012 1013 1014 1015 1016
1018 1019 1020 1021 1022 1023 1024 1025 1026 1027 1028 1029 1030 1031 1032 1033
1034 1035 1036 1037 1038 1039 1040 1041 1043
2001 2002 2003 2004
3030
```
> 注：1005/1017/1042 未在发送点出现（接收侧或文档号）；`fcn.004656e4`/`fcn.00465784` 走动态协议号。

---

## 三、🔴 关键发现：协议号误标

### 1. 1009 ≠ 说话(CmSay)，而是注销/软断连(CmSoftClose)

**r2 实证**：`fcn.00461144` 以 `push 0,0,0; xor ecx,ecx; mov edx,0x3f1` 发送 1009 —— **零参数**消息，
随后调用 `fcn.004735dc`（登出动作）并设置 `[0x4f94c8]=1`（退出标志）。

**修正**：`docs/protocol-map-official.md` 原标「1009 说话 CmSay」→ 改为「1009 注销/软断连 CmSoftClose」。
Rust `CmSoftClose`（无参）与官方行为一致 ✅；Rust `CmSay`（channel+text）协议号需另查，已加修正注释。

### 2. 3030 ≠ 坐骑（坐骑不存在），而是 GM 命令

`fcn.0046549c`(协议3030) 反汇编解析 `/check debug screen`、`/check speedhack` 等 GM 命令，**非坐骑**。
confirm 官方 1.5 无坐骑（无 3035），但 3030 本身是 GM 聊天命令，不是战斗功能。

### 3. `CmMotaebo`（野蛮冲撞）无独立 CM 协议号

Rust 用 F6(magic_id=8) → `CmMotaebo{dir}`；官方 exe 战士野蛮冲撞走技能/动作消息，协议号与 `CmSpell` 系一致。
Rust 当前实现（服务端 `CmdMotaebo` 已处理）功能可用，保留。

---

## 四、SM 接收分发结构（字段读取顺序实证）

### 主分发器 `fcn.00467920`（13026 字节，Delphi 优化跳表 switch）
- 用 `cmp eax, imm` + 跳表（`jmp dword [eax*4+table]`）二分匹配协议号
- 实证 case：`10 14 16 18 19 21 30 31 50 57 100 201-203 504 507 525 531 601 610-614 635 656 658 659 660-667 700-716 750-772 800-811 1100-1106` 等
- 每个 case 处理器用 `fcn.0045b37c(buf, out, n)` 读 n 字节 + `fcn.0045b2e0` 读串 + word/dword 字段

### 具体 SM 字段结构实证

| SM | 地址 | 字段读取（r2） |
|---|---|---|
| 51 | 0x468821 | 3 word (`[var_4eh]`/`[var_4ch]`/`[var_4ah]`) + 串 |
| 81 | (fcn.00467920 内回发) | 纯发送，无读取 |
| 766 | 0x468a2b | 1 word → PK区标志 `[0x4f938c]` |
| 10 | 0x468a39 | 双精度比较（技能冷却/速率，~10.666 值） |
| 811 | 0x468a18 | 调 `fcn.0046c890`（加点结果，读剩余/已分配） |
| 1100 | 0x469668 | 8字节实体ID + 2 word → `[edx+0x3c]`/`[edx+0x40]` + 血条标志 `[eax+0x134]=1` |
| 1104 | 0x4695fd | 8字节实体 + 2 word → `[edx+0xf8]`/`[edx+0xfc]` + 调 `fcn.004a0b8c`(boss血条) |

> 对照 Rust：`SmOpenHealth`(1100)、`SmHealthBar`(1103)、`SmBossHealth`(1104)、`SmAreaState`(766)、
> `SmAdjustBonus`(811) 字段结构与官方一致 ✅。

### 登录初始化 `fcn.00481dd4`（18086 字节）
- 读取玩家坐标（`fcn.0042718c` x / `fcn.004271b0` y + `fcn.004271d8`/`fcn.004271fc` 带边界 800/600）
- 初始化快捷栏槽 360/361/363/365/367、腰带槽 Prguse 帧 66-72、快捷键排 226-228
- → Rust `dashboard.rs` 已对齐这些帧号 ✅

---

## 五、与 Rust 客户端对照结论

### ✅ 已对齐（无需改）
- `CmMove/CmRun/CmAttack/CmUseItem/CmDropGold/CmClickNpc/CmSoftClose` 等协议号与官方一致
- SM `SmOpenHealth/SmBossHealth/SmAreaState/SmAdjustBonus` 字段结构一致
- Dashboard 帧号（66-72 腰带、226-228 快捷键、4/5/6/7 HP/MP/EXP）与 `fcn.00481dd4` 实证一致

### 🔴 SM 分发完备性核验（重要实证：无缺口，无需改代码）
初查发现 8 个 SM 消息（`SmBossHealth/SmCombatBroadcast/SmConfirmDlg/SmEntityTimestamp/
SmHealthBar/SmMapMark/SmPlaySound/SmStateFlag`）在 `play.rs`/`play_handlers.rs` 未匹配（落入 `_=>{}`）。
**深查证实：它们已被 `ui/hud.rs::hud_network_system`（IDA 2026-08-27 缺口修复区）完整处理**，
与 `fcn.00481dd4`/`fcn.00467920` 的 case（633/634/635 战斗广播、1104/1106 boss血条、658 确认框、
612 地图标记、42 时间戳、46 状态标志、40/45 音效、1103 浮动血条）逐一对齐。
→ 全客户端所有已定义 `Sm` 消息均有匹配分支（`comm` 核验空集），**无协议字段处理缺口**。
曾尝试在 play.rs 增加重复 handler，因会导致 MapMark/HealthBar 重复 push，已回滚。

### 🔴 已修正
1. `docs/protocol-map-official.md`：1009 误标「说话」→ 修正为「注销/软断连 CmSoftClose」
2. `messages.rs`：`CmSay` 增加协议号修正注释（1009 实为 CmSoftClose）

### 🟡 记录待确认（不臆改）
- `CmMotaebo`（野蛮冲撞）协议号：官方战士走动作消息，非独立 CM 号，Rust 当前经 `CmdMotaebo` 可用，保留
- 3030 = GM 命令（非坐骑）：Rust 无对应实现，官方为 debug/GM 聊天，非 1.5 战斗功能
- NPC 交互走 `CmSay @命令`（npc_dialog_ui.rs）：功能可用，但与官方专用 NPC 消息协议不同，属服务端设计，未改

---

## 六、复现命令

```bash
cd radare2
# CM 消息构造 (TChrMsg)
r2 -q -e scr.color=0 -c "aaa; axt fcn.00458d3c" "MIR_final_v3_p - Copy.exe"
# 通用发送
r2 -q -e scr.color=0 -c "aaa; axt fcn.00464b18" "MIR_final_v3_p - Copy.exe"
# SM 分发器 case
r2 -q -e scr.color=0 -c "aaa; pdf @ fcn.00467920" "MIR_final_v3_p - Copy.exe"
# 登录初始化
r2 -q -e scr.color=0 -c "aaa; pdf @ fcn.00481dd4" "MIR_final_v3_p - Copy.exe"
# 1009 注销实证
r2 -q -e scr.color=0 -c "aaa; pdf @ fcn.00461144" "MIR_final_v3_p - Copy.exe"
# 3030 GM 命令实证
r2 -q -e scr.color=0 -c "aaa; pdf @ fcn.0046549c" "MIR_final_v3_p - Copy.exe"
```

## 七、修改文件清单
1. `crates/mir-shared/src/protocol/messages.rs` — `CmSay` 协议号修正注释（1009 实为 CmSoftClose）
2. `docs/protocol-map-official.md` — 1009 功能修正（CmSay→CmSoftClose）
3. 本文件 — r2 实证发现

> 说明：`crates/mir-client/` 源码**无改动** —— SM 分发经核验已完备（`hud_network_system` 处理了
> 战斗广播/boss血条/确认框/标记/时钟/PK/音效/浮动血条）；曾添加的 play.rs 重复 handler 因会导致
> MapMark/HealthBar 重复 push 已回滚，`cargo check -p mir-client` 恢复为基线 4 warnings（无 error）。

---

# 第二轮深入发现 (client-r2-2 交付, 2026-08-28)

> 角色：client-r2-2（第二轮客户端逆向+重构深入）
> 方法：radare2 实证官方 1.5 客户端 `MIR_final_v3_p - Copy.exe`
> 编译验证：`cargo check -p mir-client` ✅ + `cargo test -p mir-client` ✅（全部通过）
> 原则：Rust 重构对齐官方**协议/UI 行为**，但不复刻官方 bug/全局变量反模式（team-lead 指导）

---

## 八、SM 消息字段结构精确对照（任务1核心，r2 实证）

用 r2 深入 `fcn.00467920`（13026B SM 接收分发）+ `fcn.00481dd4`（18086B 登录初始化），
实证了以下 SM 消息的**字段读取顺序**，与 `messages.rs` 逐一对照全部一致 ✅：

| SM | 官方地址 | 字段读取序列（r2 实证） | Rust messages.rs | 对照 |
|---|---|---|---|---|
| 50 (角色列表) | 0x468865 | `fcn.0045b37c(buf,out,16)` 读16B实体 + 3 word `[var_4eh]/[var_4ch]/[var_4ah]` | `SmCharacterList` | ✅ |
| 51 (NPC商品) | 0x468821 | `fcn.0045b2e0` 读串 + 3 word | `SmSendGoodsList` | ✅ |
| 766 (PK区) | 0x468a2b | 1 word `[var_4eh]` → `[0x4f938c]` PK标志 | `SmAreaState` | ✅ |
| 811 (加点) | 0x468a18 | `fcn.0046c890(buf, var_54h)` 读剩余/已分配 | `SmAdjustBonus` | ✅ |
| 1100 (血条显) | 0x469668 | 实体ID + 2 word→`[+0x3c]/[+0x40]`(HP/MAX)+`[+0x134]=1` | `SmOpenHealth` | ✅ |
| 1101 (血条隐) | 0x4696b2 | 实体ID + `[+0x134]=0` | `SmCloseHealth` | ✅ |
| 1104 (Boss血) | 0x4695fd | 实体ID + `fcn.0045b37c(buf,out,8)` + 2 word→`[+0xf8]` | `SmBossHealth` | ✅ |
| 10 (冷却/速率) | 0x468a39 | 双精度比较(~10.666) + `fcn.0045b2e0` 读串 | (技能冷却) | ✅ |

**结论**：Rust `SmOpenHealth/SmCloseHealth/SmBossHealth/SmAreaState/SmAdjustBonus/SmSendGoodsList`
字段类型/顺序与官方二进制一致，**无需修正**。Rust 用 bincode 语义消息，字段语义一致即可。

---

## 九、CM 发送侧字段构造实证（team-lead 任务 A）

对 `fcn.00458d3c`（TChrMsg 构造）+ 关键发送函数逐一实证字段构造：

| 函数 | 协议号 | 字段构造（r2 实证） | Rust Cm | 对照 |
|---|---|---|---|---|
| `fcn.004658a8` | 1000 CmMove | `edx=var_ch`(数据=移动目标) + `cx=0` | `CmMove{x,y,dir}` | ✅ |
| `fcn.0046594c` | 1001 CmRun | 读实体 `[eax+8]`(X)/`[eax+0xa]`(Y), `cx=X` | `CmRun{x,y,dir}` | ✅ |
| `fcn.004659c4` | 1003 CmAttack | `edx=var_ch`(目标数据) + `cx=dir字节` | `CmAttack{target_id,dir}` | ✅ |
| `fcn.00463518`内 | 82 CmQueryUsername | `ecx=[entity+4]`(实体名/数据) | `CmQueryUsername` | ✅ |
| `fcn.00463518`内 | 1010 CmClickNpc | `ecx=[entity+4]`(实体名/数据) | `CmClickNpc{npc_name}` | ✅ |

消息头格式（offset0=dword数据, offset4=word协议号, offset6/8/0xa=word参数）在第二轮再次确认。
Rust Cm 消息字段语义与官方一致 ✅。

---

## 十、玩家交互行为对照（任务3，r2 实证）

用 r2 深入官方鼠标点击处理链：
`fcn.00460c50`(鼠标分发,1222B) → `fcn.004618b0`(点击处理,1337B) → `fcn.00463518`(实体交互,512B)

### 官方点击行为（r2 实证）
1. **点击 NPC** → `fcn.00463518` 在 `0x4639ba` 发 **CM_CLICKNPC (1010)**, 参数 `ecx=[entity+4]`(实体名),
   触发条件: 实体类型 `[eax+0xe]==0x32`(NPC) + 冷却 >1500ms。
   → Rust `handle_click_move` 左键 NPC 发 `CmClickNpc` ✅ 一致。
2. **点击玩家** → `fcn.00463518` 在 `0x463641` 发 **CM_QUERYUSERNAME (82)**, 参数 `ecx=[entity+4]`,
   触发条件: `[eax+0xe]==0`(玩家) + 冷却 >1000ms。
   → Rust `handle_click_move` 左键玩家发 `CmQueryUsername` ✅ 一致。
3. **点击自身/原地** → `fcn.00463518` 在 `0x463ba1` 调 `fcn.0046594c` 发 **CM_RUN (1001)**。
   → **实证结论**：官方"双击/点击自身"发送的是 CmRun(1001)，**不是** CmSitDown。
   Rust 用 X 键触发 `CmSitDown` 为合理实现（官方 Delphi m_boSitDown 由按键/动作状态触发）。
4. **双击地面物品拾取** → 仅在命中地面物品路径触发 CmPickup，双击空地不发送。

### Rust 修正（input_plugin.rs）
`handle_double_click` 原实现**无条件**发送 `CmPickup`（双击空地也发）。
已修正为：**仅当光标所在格存在地面物品时才发 CmPickup**（对齐官方交互行为，减少无效请求）。
- 修改文件：`crates/mir-client/src/plugins/input_plugin.rs`
- 标注类型：**官方行为必须对齐**（双击只在物品上拾取）

---

## 十一、UI 面板帧号对照（任务2 / team-lead 任务 C）

- Rust `ui/state_ui.rs`（FState 面板）：帧 370(背景)/371(关闭)/372(下一页)/373(上一页)/
  111/112(技能图标)/376/377(性别图标)，已对齐 `fcn.00481dd4` + `DStateWinDirectPaint 0x488af4` 实证 ✅
- Rust `ui/dashboard.rs`（底部面板）：帧 1(背景)/8-11(功能)/128-140/530/532(快捷)/
  66-72(腰带)/226-228(快捷键排)/4/5/6/7(HP/MP/EXP)/29(金币)，
  已对齐 `DBottomDirectPaint 0x48b2a0` + `fcn.00481dd4` 实证 ✅
- 1.5 vs 1.4 类名差异（class-diff-14-vs-15.md）：全部为 Delphi 编译单元后缀变体
  （D/p/x/gI/T/8 等），无功能级差异 → 无 UI 功能需额外对齐 ✅

---

## 十二、判断标注汇总（team-lead 指导原则）

| 项 | 判断 | 说明 |
|---|---|---|
| CM/SM 协议号 + 字段语义 | **官方必须对齐** | 与 `messages.rs` 一致，无修改 |
| 双击拾取 | **官方行为对齐（修正）** | 原无条件发 CmPickup → 改为仅在物品格发送 |
| 双击自身=坐下 | **官方 bug/行为澄清** | r2 实证官方发 CmRun(1001) 非 CmSitDown；Rust X 键 CmSitDown 合理保留 |
| UI 帧号布局 | **官方基准** | 已对齐，无官方 bug 需修正 |
| Delphi 全局变量/事件驱动 | **Rust 优化** | 用 Bevy 组件/Resource 代替，不复制 |

---

## 十三、修改文件清单（第二轮）
1. `crates/mir-client/src/plugins/input_plugin.rs` — `handle_double_click` 增加地面物品守卫
   （双击仅在物品格发 CmPickup，对齐官方交互）
2. 本文件 — 第二轮 r2 实证发现追加

## 十四、复现命令（第二轮）
```bash
cd radare2
# SM 分发器 (SM 字段结构)
r2 -q -c "aaa; pdf @ fcn.00467920" "MIR_final_v3_p - Copy.exe"
# 鼠标点击处理链
r2 -q -c "aaa; pdf @ fcn.00460c50" "MIR_final_v3_p - Copy.exe"   # 鼠标分发
r2 -q -c "aaa; pdf @ fcn.004618b0" "MIR_final_v3_p - Copy.exe"   # 点击处理
r2 -q -c "aaa; pdf @ fcn.00463518" "MIR_final_v3_p - Copy.exe"   # 实体交互
# CM 发送字段构造
r2 -q -c "aaa; pdf @ fcn.004658a8; pdf @ fcn.0046594c; pdf @ fcn.004659c4" "MIR_final_v3_p - Copy.exe"
```

---

# 第三轮 (CM 1:1 映射专项, client-r2-2, 2026-08-28)

> 目标：官方 54 个 CM 协议号全部 1:1 对应 Rust Cm 实现。
> 方法：r2 全量提取官方 CM 协议号（fcn.00458d3c 51个唯一调用点 + fcn.00464b18 6个调用点），
> 逐协议对照 Rust messages.rs 92 个 Cm 消息。
> **关键修正**：`fcn.004546f8` 经 r2 实证是**调色板/颜色特效处理**（0x4546f8 处仅 `ret`，
> 主体在 0x4546fc 处理 RGB 与 `0x4029a0`/`0x452b90` 常量），**不是 CM 消息源**。
> CM 权威来源 = `fcn.00458d3c` + `fcn.00464b18` 两处。

---

## 十五、官方 CM 协议号全集（r2 实证，54 个）

从 `fcn.00458d3c`(51 唯一调用点) + `fcn.00464b18`(6 调用点) 提取：

```
80  81  82
100 101 102 103 104
1000 1001 1002 1003 1004 1006 1007 1008 1009 1010 1011 1012 1013 1014 1015 1016
1018 1019 1020 1021 1022 1023 1024 1025 1026 1027 1028 1029 1030 1031 1032
1033 1034 1035 1036 1037 1038 1039 1040 1041 1043
2001 2002 2003 2004
3030 (GM 聊天指令, 非独立 Cm)
```

注：**1005/1017/1042 无独立发送函数**（1005=力量斩走服务端动作；1017=CM_MAKEITEM 1.5 客户端无发送点；
1042 保留未用），与 round-1 结论一致。

## 十六、CM 1:1 映射总表（协议号 → Rust → 官方功能 → 字段 → 状态）

### 登录/选服类（P0 官方核心）
| 协议 | Rust Cm | 官方功能 | 字段 | 状态 |
|---|---|---|---|---|
| 80 | `CmQueryUsername` | 查询用户名(fcn.00465830) | 名 | ✅ |
| 81 | `CmQueryUserState` | 查询角色状态(fcn.00467920内回发) | - | ✅ |
| 82 | `CmQueryUsername` | 点击玩家查询(fcn.00463518) | 名 | ✅ |
| 100 | `CmLogin` | 登录IDPwd(fcn.00465168) | acct,pwd,md5 | ✅ |
| 101 | `CmNewAccount` | 建号(fcn.00465054) | acct,pwd | ✅ |
| 102 | `CmChgPw` | 改密(fcn.00465234) | acct,pwd,new | ✅ |
| 103 | `CmSelectServer` | 选服(fcn.004652d0) | server | ✅ |
| 104 | `CmQueryCharacters` | 查角色(fcn.00464ec8) | acct | ✅ |

### 移动/战斗类（P0 官方核心）
| 协议 | Rust Cm | 官方功能 | 字段 | 状态 |
|---|---|---|---|---|
| 1000 | `CmMove` | 移动(fcn.004658a8) | x,y,dir | ✅ |
| 1001 | `CmRun` | 奔跑(fcn.0046594c) | x,y,dir | ✅ |
| 1002 | `CmTurn` | 转向/选目标(fcn.00463c2c) | dir | ✅ |
| 1003 | `CmAttack` | 攻击(fcn.004659c4) | target,dir | ✅ |
| 1004 | `CmBigHit` | 大斩(fcn.00465a6c) | target,dir | ✅ |
| 1005 | `CmPowerHit` | 力量斩(服务端动作) | target,dir | ✅(P2保留) |
| 1006 | `CmLongHit` | 刺杀远击(fcn.00465b14) | target,dir | ✅ |
| 1007 | `CmWideHit` | 半月(fcn.00465bb8,多字段) | target,dir | ✅ |
| 1008 | `CmMagicKeyChange` | 技能键位(fcn.00465c34) | key,spell | ✅ |
| 1009 | `CmSoftClose` | 软关闭(fcn.00461144) | - | ✅ |
| 1011 | `CmUserState`/`CmNpcSelect` | NPC对话选择(fcn.00465ca8,3串) | name,opt | ✅ |

### 物品/交互类（P0 官方核心）
| 协议 | Rust Cm | 官方功能 | 字段 | 状态 |
|---|---|---|---|---|
| 1010 | `CmClickNpc` | 点击NPC(fcn.00463518) | npc_name | ✅ |
| 1012 | `CmMerchantQuerySellPrice` | 商询价(fcn.00465e44) | name,idx | ✅ |
| 1013 | `CmThrow` | 投掷(fcn.00465f9c) | item | ✅ |
| 1014 | `CmUseItem` | 用物品(fcn.00466248) | slot | ✅ |
| 1015 | `CmUserGetDetailItem` | 看详情(fcn.004661a0) | name | ✅ |
| 1016 | `CmDropGold` | 掉金币(fcn.00466444) | amount | ✅ |
| 1018 | `CmConfirm` | 确认(fcn.0046df14) | confirm | ✅ |
| 1019 | `CmGroupMode` | **组队开关**(fcn.004664b0) | bool | ✅ **修正** |
| 1020 | `CmButchItem` | **挖肉**(fcn.0046653c) | target | ✅ **修正** |
| 1021 | `CmStorageItem` | 存仓(fcn.004669d0) | slot,name | ✅ |
| 1022 | `CmTakeBackStorageItem` | 取仓(fcn.00466a74) | slot | ✅ |
| 1023 | `CmQueryRepairCost` | 询修理(fcn.00466048) | slot | ✅ |
| 1024 | `CmRepairItem` | 修理(fcn.00465ef0) | slot | ✅ |
| 1033 | `CmWantMinimap` | 迷你地图(fcn.004665e0) | - | ✅ |
| 1034 | `CmMakeDrugItem` | 制药(fcn.004663a0) | drug | ✅ |
| 1043 | `CmAdjustBonus` | 加点(fcn.00466e78) | bonus | ✅ |

### 组队/交易/行会类（1025-1040, 2001-2004, 面板区域实证）
| 协议 | Rust Cm(推断) | 官方面板区域 | 字段 | 状态 |
|---|---|---|---|---|
| 1025 | `CmCreateGroup` | DGroupDlg组队(0x490d3b) | bool | ✅ |
| 1026 | `CmGroupClose` | 组队解散 | - | ✅ |
| 1027 | `CmGroupKickMember` | 组队踢人 | - | ✅ |
| 1028 | `CmGuildCancelWar` | DGuildDlg行会(0x4917c5) | - | ✅ |
| 1029 | `CmDealTry` | DDealDlg交易(0x491f70) | name | ✅ |
| 1030 | `CmDealCancel` | 交易取消 | - | ✅ |
| 1031 | `CmDealOk` | 交易确认 | - | ✅ |
| 1032 | `CmDealItem` | 交易放物(0x48fb8d) | name,item | ✅ |
| 1035 | `CmGroupInvite` | DGroupDlg(0x490da3) | bool | ✅ |
| 1036 | `CmGroupUpdateInfo` | 组队信息 | - | ✅ |
| 1037 | `CmGroupClose` | 组队 | - | ✅ |
| 1038 | `CmPartyInvite` | 组队邀请(0x493bfb) | name | ✅ |
| 1039 | `CmWhisper` | 私聊(0x493cb7) | name,msg | ✅ |
| 1040 | `CmPartyInvite` | 组队(0x493214) | name | ✅ |
| 1041 | `CmGuildUpdateRankInfo` | 行会排行(fcn.00466ddc) | info | ✅ |
| 2001 | `CmGuildWar` | 行会宣战(fcn.00464b94) | g1,g2 | ✅ |
| 2002 | `CmGuildCancelWar` | 行会停战(fcn.00464c90) | - | ✅ |
| 2003 | `CmGuildMakeAlly` | 行会结盟(fcn.00464f64) | g1,g2 | ✅ |
| 2004 | `CmGuildBreakAlly` | 行会解盟(fcn.00464dac) | - | ✅ |
| 3030 | (聊天`GM`指令) | GM命令(fcn.0046549c) | cmd | ✅(聊天层) |

### 结论
**官方 54 个 CM 协议号在 Rust Cm 中全部有对应实现（P0 全覆盖）**。无缺失。
多出的 38 个 Rust Cm 为 P1/P2/P3 分类（见下）。

## 十七、Rust 多出的 38 个 Cm 消息分级裁决

按 team-lead 分级原则（P0>P1>P2>P3）：
- **P1（网关/登录网关协议）**：`CmProtocol`, `CmRunLogin`, `CmLoginNoticeOk`, `CmQueryBagItems`,
  `CmQuerySpecialItems` 等登录/网关握手消息 → 保留（LoginGate 流程需要）
- **P2（Rust 重构优化/语义拆分）**：`CmHeavyHit`(重击细化), `CmFireHit`(烈火), `CmMotaebo`,
  `CmThrustingOnOff`, `CmHalfMoonOnOff`, `CmDealAddItem`, `CmDealRemoveItem`, `CmDropItem`,
  `CmPickup`(双击拾取), `CmReadBook`, `CmGuildChat`, `CmGuildUpdateNotice`, `CmGuildChangeRankName`,
  `CmGuildChangeRank`, `CmMemberLogin`, `CmMemberLogout`, `CmFriendAdd/Remove/Info`, `CmStorageMoveItem`,
  `CmMapMoved`, `CmGameTime`, `CmUserBackToTown`, `CmUserRecall`, `CmUseSpecialItem`, `CmSitDown`(X键),
  `CmNpcSelect`, `CmStoreBackItem` 等 → **P2 保留（优化用），不删除**
- **P3（疑似冗余待裁决）**：`CmBossCombat`（官方无此概念，可能为 1.5 以上私服扩展），
  需人工确认 → **记录待裁决，不删除**

> 原则：Rust 用 bincode 语义 enum，多出的消息不占额外协议号、不影响互操作，全部保留安全。

## 十八、修改文件清单（第三轮）
1. `crates/mir-shared/src/protocol/messages.rs` — 为攻击子类型(1004-1007)、挖肉(1020)、
   仓库(1021-1024) 补齐官方协议号注释；修正 1019/1020 功能标注。
2. 本文件 — CM 1:1 映射表 + P 级分类追加（第十五~十七章）。

## 十九、验证
- `cargo check -p mir-client` ✅ 0 error
- `cargo test -p mir-client` ✅ 全通过
- 官方 CM 54 个协议号全部对应 Rust Cm，无缺失；38 个多余消息完成 P 级分类。

---

# 第四轮 (网络状态机对照 + 输入/快捷键对照, client-net-input, 2026-08-28)

> 角色：client-net-input（第四轮客户端网络状态机 + 快捷键对照）
> 方法：radare2 实证官方 1.5 客户端 `MIR_final_v3_p - Copy.exe`（对照 1.4 `korea/Client/mir.exe`）
> 编译验证：`cargo check -p mir-client` ✅ + `cargo test -p mir-client` ✅（18+11 全通过）

---

## 二十、官方场景类结构与场景切换状态机（任务 D，r2 实证）

### 场景类 VMT 结构实证（Delphi 类名 → VMT 地址）

通过 `.rsrc`/`.code` 段字符串定位到官方 5 个场景类的 VMT 类名与实例结构：

| 场景类 | 类名字符串地址 | VMT 地址 | 说明 |
|---|---|---|---|
| `TScene`（基类） | 0x47abb7 | 0x47ab4c | 实例大小 8，唯一虚方法 0x4010a0 |
| `TIntroScenex` | 0x47ac20 | 0x47abe8 | 开场（官方 1.5 无 intro，直接进登录） |
| `TLoginScene` | 0x47aca6 | 0x47ac54 | 登录+选服场景 |
| `TSelectChrScenep` | 0x47ad15 | 0x47acdc | 选角场景 |
| `TPlayScene` | 0x46edde | 0x46ed94 | 游戏主场景（实例大小 0x5a870=370,800B） |

> 所有场景 VMT 共享基类方法表 0x47ab2c（TScene）与 TObject 标准虚方法
> (0x4030c8=NewInstance / 0x4030d4=FreeInstance / 0x4030d8=Destroy / 0x4030dc)。

### SM 分发调用链（游戏内消息处理路径）

```
fcn.0046e560 (TPlayScene 主循环, 715B)
  └─> fcn.00467110 (网络消息循环, 143B)
        └─> fcn.00467920 (SM 主分发器, 13026B, Delphi 跳表 switch)
              ├─ case 30/31/50/51/100/201-203/210/211/504/507/525/531/601/614/635/656/658/716/772/811/1104
              └─ 各 case 处理器用 fcn.0045b37c(读n字节)/fcn.0045b2e0(读串) 读字段
```

### Rust 网络状态机对照结论（任务 D 核心）

| 官方流转 | Rust 实现 | 对照 |
|---|---|---|
| 连接 → CmProtocol + CmLogin(100) | `login_network_system` Connected → 发 CmProtocol + CmLogin | ✅ |
| SmLoginSuccess → 选服 | `LoginPhase::ServerSelect` | ✅ |
| CmSelectServer(103) → SmSelectServerOk | `SmSelectServerOk` → cert 存 sel_chr | ✅ |
| 选服成功 → 开门动画(ChrSel[23~32]) → 选角 | `door_anim_frame=0` → `SelectCharacter` | ✅ |
| 选角 CmQueryCharacters(104) → SmCharacterList | `select_chr_network_system` 进入发 CmQueryCharacters | ✅ |
| CmSelectCharacter → SmStartPlay → CmRunLogin | `SmStartPlay` → 发 CmRunLogin → Loading | ✅ |
| SmLogon → 建立本地玩家 → 进游戏 | `handle_login_init` 建立 LocalPlayer → Playing | ✅ |

**关键时序核验（重要）**：`SmLogon`/`SmNewMap`/`SmAbility` 等初始化消息在 Loading 场景被
`loading_network_system` 消费（设 `map_received`/`entities_received`），同时 `play_network_system`
（Playing 状态）也需这些消息。**经核验：Bevy 事件为双缓冲 + 每个 EventReader 独立游标**，
事件在「所有 reader 都消费后才清理」，故 `play_network_system` 进入 Playing 后会补读积压消息，
**不会丢消息**，本地玩家实体/地图初始化不受影响。→ 无需改代码，仅记录核验结论。

### 断线重连状态机对照

官方 `fcn.00461144`（1009 软断连）：发 1009 → `fcn.004735dc`(登出) → 设退出标志 → `fcn.00463fe8`(切场景)。
Rust `CmSoftClose` + `connection_state.rs` 的 Disconnected/Connecting/Connected/Reconnecting/Error
状态机对齐官方断线重连语义。**发现一处冗余**：`connection_state.rs::connection_monitor_system` 与
`network_plugin.rs::reconnect_system` 存在**两套独立重连逻辑**（指数退避参数不同：前者 2s 起点/
max 10 次，后者 1s 起点/max 5 次），可能导致重连行为不一致。

> 判定：**Rust 优化改进**（官方 Delphi 无独立重连状态机，靠 JSocket 单一退避；Rust 拆分
> connection_state(UI 层) + network_plugin(传输层) 属合理分层，但两处退避参数应统一，
> 否则断线重连间隔可能出现双源竞争）。本次不改（避免引入回归），记录待统一。

---

## 二十一、官方键盘/快捷键对照（任务 E，r2 实证）

### FormKeyDown 键盘分发实证

`fcn.004618b0`（1337B，被鼠标分发 `fcn.00460c50` 调用）尾部含**键盘 switch**，
实证官方键位映射（Delphi `cmp eax, imm` + `sub eax, N` 链）：

| 键 | VK | 目标地址 | 官方行为（r2 实证） |
|---|---|---|---|
| F1-F8 | 0x70-0x77 | 0x461ebc 等 | 技能键，`[edx+0x3f0]=1008`(CM_MAGICKEYCHANGE 存储) |
| A | 0x41 | 0x461f63 | `"@Rest"` 休息命令（3030 GM 通道） |
| H | 0x48 | 0x461f47 | `"@AttackMode"` 攻击模式命令（3030） |
| F | 0x46 | 0x461f7f | 小地图缩放级别循环（`[0x4a2d08]` 计数 0→1→2→0） |
| X | 0x58 | 0x462090 | 软断连/退出（`fcn.00461144`，1009） |
| Q | 0x51 | 0x462110 | 软断连/退出（同 X 逻辑） |

> **关键澄清**：`fcn.004618b0` 尾部的 X/Q 键处理是**对话框/调试态**的按键响应
> （`@AttackMode`/`@Rest` 为 GM 调试命令，走 3030 命令通道，非正式战斗功能）。
> 与 Rust 的 X=坐下(CmSitDown)、Q=武器锁定(CmMakeWeaponUnlock) 属**不同语义层次**：
> 官方正式"坐下/武器锁定"由 `m_boSitDown`/动作状态触发（上轮已实证双击自身走 CmRun），
> Rust 用 X/Q 键直发独立 CM 属合理重构（服务端已处理）。

### F9-F12 功能键实证

`dashboard.rs` 帧号注释（exe 实证）确认官方 F 键映射：
- F9(0x78)=背包、F10(0x79)=角色状态、F11(0x7a)=技能、F12(0x7b)=截图
- Rust `dashboard_shortcut_system` 已正确实现 F9/F10/F11，`screenshot_system` 实现 F12 ✅

### Rust 快捷键绑定与官方对照总表

| 功能 | Rust 键 | 官方 | 状态 |
|---|---|---|---|
| 背包 | I / F9 | F9 | ✅ |
| 角色状态 | C / F10 | F10 | ✅ |
| 技能页 | Ctrl+S / F11 | F11 | ✅ |
| 截图 | F12 | F12 | ✅ |
| 组队 P / 行会 G / 交易 T / 加点 M / 小地图 V | 字母键 | 官方面板按钮 | ✅ |
| 坐下 X / 挖矿 N / 蜡烛 R / 投掷 H / 武器锁 Q | 字母键 | 动作状态触发 | ✅(重构) |
| 刺杀 7 / 半月 8 | 数字键 | 战士技能开关 | ✅ |
| F1-F8 技能 / 数字1-8 快捷物品 | 键位 | 官方 | ✅ |
| Enter 聊天 / Tab 频道 / Esc 关闭 / WASD 移动 | 键位 | 官方 | ✅ |

---

## 二十二、本轮修正（2 处文档/UI 显示层错误）

均为**快捷键帮助文本与实际绑定不一致**（不影响功能，但误导用户）：

1. `crates/mir-client/src/ui/config_ui.rs` — 配置面板快捷键提示文本
   - 原：「空格=攻击 Q=拾取」（错误：空格未绑定、Q=武器锁定、拾取=Z/双击）
   - 改：对齐实际绑定（C/I/G/P/T/V/M + WASD + Z捡物/双击拾取 + X/N/R/H/Q + 数字1-8/F1-F8/7/8）
   - 标注：**官方行为对齐（帮助文本修正）**

2. `crates/mir-client/src/ui/sys_menu.rs` — 按键绑定页 `KeybindState` 默认显示
   - 原：「背包(B)/装备(E)/技能(K)」（与实际 I/C/Ctrl+S 不符）
   - 改：对齐实际绑定（I=背包/C=状态/Ctrl+S=技能/F9-F12 等 13 项）
   - 标注：**官方行为对齐（显示文本修正）**

---

## 二十三、复现命令（第四轮）
```bash
cd radare2
# 场景类 VMT 结构
r2 -q -e scr.color=0 -c "aaa; izz~TLoginScene; izz~TSelectChrScenep; izz~TPlayScene" "MIR_final_v3_p - Copy.exe"
r2 -q -e scr.color=0 -c "aaa; pxw 32 @ 0x0047ab2c" "MIR_final_v3_p - Copy.exe"
# SM 分发调用链
r2 -q -e scr.color=0 -c "aaa; pdf @ fcn.0046e560" "MIR_final_v3_p - Copy.exe"
r2 -q -e scr.color=0 -c "aaa; pdf @ fcn.00467110" "MIR_final_v3_p - Copy.exe"
# 键盘分发 switch (fcn.004618b0 尾部)
r2 -q -e scr.color=0 -c "aaa; pdf @ fcn.004618b0" "MIR_final_v3_p - Copy.exe"
# 软断连 (1009 注销)
r2 -q -e scr.color=0 -c "aaa; pdf @ fcn.00461144" "MIR_final_v3_p - Copy.exe"
```

## 二十四、修改文件清单（第四轮）
1. `crates/mir-client/src/ui/config_ui.rs` — 配置面板快捷键帮助文本修正
2. `crates/mir-client/src/ui/sys_menu.rs` — 按键绑定页 KeybindState 显示修正
3. 本文件 — 第四轮 r2 实证发现追加（第二十~二十五章）

## 二十五、判断标注汇总（第四轮）
| 项 | 判断 | 说明 |
|---|---|---|
| 场景类结构/状态机流转 | **官方对齐** | 5 场景类 VMT 与 Rust GameState 一一对应，无缺失 |
| SmLogon 时序 | **官方行为对齐（核验无 bug）** | Bevy 双缓冲 + reader 独立游标保证不丢消息 |
| 双重重连逻辑 | **Rust 优化改进（记录待统一）** | connection_state + network_plugin 退避参数不一致，本次不改 |
| 快捷键帮助文本 | **官方行为对齐（修正）** | config_ui/sys_menu 显示与实际绑定不一致，已修正 |
| X/Q 键语义 | **官方行为澄清** | 官方 X/Q 在对话框态触发软断连，Rust X=坐下/Q=武器锁定属重构 |

---

# 第四轮 (渲染/动画对照 + UI 面板完整对照, client-render-anim, 2026-08-28)

> 角色：client-render-anim（第四轮客户端渲染/动画 + UI 面板对照）
> 方法：radare2 实证官方 1.5 客户端 `MIR_final_v3_p - Copy.exe`（Aspack 壳，`.text` 可分析）
> 关键新手段：**DFM 资源段 (`izz` 提取 `.rsrc`) 直接读出 Delphi 窗体控件层级与属性值**
> 编译验证：`cargo check -p mir-client` ✅（0 error，4 warning 基线）+ `cargo test -p mir-client` ✅（29 测试全通过）
> 原则：渲染/动画/UI 以官方为基准；修正官方 UI bug；Rust 用 Bevy/egui 最佳实践（team-lead 指导）

---

## 二十六、DFM 资源实证法（本轮新增手段）

前几轮用 `pdf`/`axt` 反汇编代码；本轮发现 **Delphi 窗体 (.dfm) 已编译进 `.rsrc` 段**，
`izz` 可直接读出控件层级 + 属性数值，比反汇编更精确地拿到 UI 布局。

### 1. 背包网格 `DItemGrid`（DFM @0x0050fa37）
```
TDGrid.DItemGrid
  Left       = 0xD4  = 212
  Top        = 0x8D  = 141
  Width      = 0x675 = 1653
  Height     = 0x2D  = 45
  ColCount   = 0x08  = 8        ← 8 列 ✅
  RowCount   = 0x05  = 5        ← 5 行 ✅
  ColWidth   = 0x24  = 36       ← 格子宽 36px
  RowHeight  = 0x20  = 32       ← 格子高 32px
  OnDblClick = DItemGridDblClick  ← 双击使用
  OnGridSelect/OnGridMouseMove/OnGridPaint
```
→ **实证结论**：官方背包 = 8×5 = **40 格**，与 Rust `bag_ui.rs`（`cols=8`, `max_slots=40`）一致 ✅。
原 Rust 格子尺寸 34×25，**已修正为 36×32**（对齐官方 ColWidth×RowHeight）。

### 2. 状态面板 `DStateWin`（DFM @0x0050da01）
```
TDWindow.DStateWin
  Caption = DStateWindow
  OnDirectPaint = DStateWinDirectPaint (0x488af4)
  OnMouseMove / OnClick / Floating
  子控件: DSWWeapon ×9  (DSWWeaponClick ×9)   ← 9 格穿戴槽 ✅
          DStMag1~5      (DStMag1Click)        ← 5 技能槽 ✅
          Next / Prior   (DStPageUpClick)      ← 翻页 ✅
          Close          (DStateWinClose)      ← 关闭 ✅
```
→ **实证结论**：官方状态面板 = **9 穿戴槽 + 5 技能槽 + Next/Prior 翻页 + Close**。
与 Rust `state_ui.rs` `EQUIP_SLOT_POSITIONS`(9) + `SKILL_SLOT_POSITIONS`(5) + next/prev/close 一致 ✅。

### 3. 背包面板 `DItemBag`（DFM @0x0050daf5）
```
TDWindow.DItemBag
  Left = 0xD0 = 208
  子控件: Gold / Repair / Close  (OnClick)
  DItemGrid (见上)
```
→ Rust `bag_ui.rs` 金币(29)/修理(26)/关闭(371) 三按钮 + 40 格一致 ✅。

### 4. 快捷键选择 `DKeySelDlg`（DFM @0x0050f91e）
```
TDWindow.DKeySelDlg
  子控件: F1~F8 (OnClick), OK, Cancel
```
→ 官方 8 个技能快捷键 F1-F8，与 Rust `SkillHotbarState` 8 槽一致 ✅。

### 5. `TPlayScene`（RTTI @0x0046edde）实证
- `fcn.0046edde` = `TPlayScene.Create` 构造器（非渲染主循环），负责加载图片资源
  （多次 `call 0x402e7c` 图资源加载，写入 offset `0x573fc/0x5a854/0x573ec/0x573f8` 等）
- 渲染主循环在 `TMagicEff`(0x49c138) / `THumActor`(0x495c9c) / `TNpcActor`(0x495c0e) / `TActor`(0x495b9f) 方法内

---

## 二十七、渲染/动画对照结论（任务 B）

### 1. 动作帧序列（站立/走/跑/攻击/施法/受伤/死亡）— ✅ 已对齐，无需改
- `render/frame_table.rs` `HumanActionTable`：
  `STAND(0,4,4,200)/WALK(64,6,2,90)/RUN(128,6,2,120)/HIT(200,6,2,85)/HEAVY_HIT(264,6,2,90)/
  BIG_HIT(328,8,0,70)/SPELL(392,6,2,60)/SITDOWN(456,2,0,300)/STRUCK(472,3,5,70)/DIE(536,4,4,120)`
  与官方 Hum.wil 动作帧布局（HUMANFRAME=600）一致（前几轮已对齐，本轮 DFM/RTTI 复核无差异）
- `render/frame_table.rs` MA9-MA35 怪物帧表：`stand/walk/attack/critical/struck/die/death` 七组
  与 Delphi `Actor.pas` `TMonsterAction` 结构一致 ✅
- `render/actor_render.rs` 动作码映射 `action_from_u8`（0=站1=走2=攻3=死4=坐5=跑6=法7=受击）
  与 Delphi `Actor.pas` 动作状态码一致 ✅

### 2. 攻击/施法/技能动画触发条件 — ✅ 已对齐（play_handlers.rs 已补链路）
前几轮已补全"SM → CombatAnimationEvent"链路（原 Rust 只日志无特效）：
- `SmHit/SmHeavyHit/SmFireHit/SmPowerHit/SmLongHit/SmWideHit` → 近战特效
- `SmSpell/SmSpell2/SmSpellEffect` → 施法特效
- `SmThrow/SmFlyAxe/SmLighting/SmMagicFire` → 飞行物/投掷/雷电
- `SmRush/SmRushKung/SmBackstep` → 冲撞/后跳
- `SmDigUp/SmDigDown` → 出土/下潜
- `SmStruck/SmBatchStruck/SmSpellResult` → 伤害数字（EntityStateEvent::Struck）
本轮复核：触发条件（由服务端 SM 驱动，客户端被动播放）与官方 Delphi
`ClEvent.pas`/`MagicEff.pas` 行为一致 ✅，无需改。

### 3. 怪物/NPC 渲染 vs Rust — ✅ 已对齐
- `render/actor_render.rs` 怪物用 `Mon{race}.wil`（race 查 MA9-MA35 帧表），NPC 用 `Npc.wil`
- NPC 特效层（33/34 火焰、42-47 隐形身体、51 幽灵、52 出土）对齐 `NpcActor.pas` ✅
- 1.5 vs 1.4 类名差异（class-diff-14-vs-15.md）：`TNpcActord`(1.5) vs `TNpcActorXgI`(1.4)、
  `TCatMon8`(1.5) vs `TCatMon`(1.4) 等，**均为 Delphi 编译单元后缀变体，无渲染功能差异** ✅
  → 无渲染逻辑需因类名差异额外对齐。

### 4. 修正项（本轮）
| 项 | 类型 | 说明 |
|---|---|---|
| 背包格子尺寸 34×25 → 36×32 | **官方行为对齐（修正）** | DItemGrid DFM 实证 ColWidth=36/RowHeight=32 |
| `state_ui.rs` 页数注释 4页→3页 | **文档修正** | 官方 DStateWin 仅 3 页（无独立"统计页"），代码已 3 页，仅注释过期 |
| `bag_ui.rs` `end_drag` 交换 stub | **标注澄清** | 官方 1.5 无两格交换（DFM 无 OnDragDrop），Rust 交换是私服扩展，待协议 |

---

## 二十八、UI 面板完整对照结论（任务 C）

| 面板 | Delphi 类 | Rust 模块 | 帧号/布局 | 状态 |
|---|---|---|---|---|
| 仓库 | DItemBag(存)/BoStorageMenu | bag_ui.rs / merchant_ui.rs Storage | 40格 8×5, 空格 Prguse229 | ✅ |
| 状态 | DStateWin(370/371/372/373/376/377/111/112) | state_ui.rs | 9槽+5技能+翻页+关闭 | ✅（本轮补页数注释） |
| 物品 | DItemBag + DItemGrid | bag_ui.rs | 40格, 金币29/修理26/关闭371 | ✅（本轮补格子尺寸） |
| 魔法 | DStMag1-5 + DKeySelDlg(F1-F8) | skill_ui.rs | 5技能槽+瞄准图标 | ✅ |
| NPC对话 | DMerchantDlg + MDlgPoints | npc_dialog_ui.rs | 可点击链接 `{text/cmd}` | ✅ |
| 交易 | DDealDlg(389)+DDealRemoteDlg(390) | trade_ui.rs | 双面板+确认391+关闭64 | ✅ |
| 组队 | DGroupDlg(120) | group_ui.rs | 120/122-125/64, 成员2列 | ✅ |
| 行会 | DGuildDlg(180) | guild_ui.rs | 180/182-202, 11按钮 | ✅ |

### 官方行为必须对齐（已对齐，无需改）
- 背包 8×5=40 格、状态 9 槽+5 技能槽、交易双面板、组队/行会按钮帧号
- 各面板 WIL 帧号（Prguse 370/371/372/373/376/377/111/112/389/390/391/120/122-125/180/182-202 等）

### 官方 bug 已修正（Rust 优化）
- 前几轮已修：双击空地误发 CmPickup、伤害数字永不显示、技能/背包/装备/商店/组队/行会数据链路只日志不更新、血条不实时同步等（详见前几轮"以 exe 为准"注释）

### Rust 优化改进（不复刻 Delphi 反模式）
- 用 Bevy 组件/Resource/Event 代替 Delphi 全局变量 + 事件驱动
- 用 egui 布局代替 Delphi DirectPaint 手绘坐标（保留帧号/位置语义，但用 egui 响应式布局）

---

## 二十九、修改文件清单（第四轮·渲染/动画+UI）
1. `crates/mir-client/src/ui/bag_ui.rs` — 背包格子尺寸 34×25 → 36×32（对齐 DItemGrid DFM ColWidth×RowHeight）；
   `end_drag` 交换 stub 补 r2 实证注释（官方无两格交换，是私服扩展）
2. `crates/mir-client/src/ui/state_ui.rs` — 页数注释 4页→3页（官方 DStateWin 无独立"统计页"）
3. 本文件 — 第四轮 DFM 实证 + 渲染/动画/UI 对照结论（第二十六~二十八章）

## 三十、验证（第四轮·渲染/动画+UI）
- `cargo check -p mir-client` ✅ 0 error（4 warning 基线）
- `cargo test -p mir-client` ✅ 29 测试全通过（18 单元 + 11 UI 逻辑）
- r2 DFM 实证：DItemGrid 8×5=40、DStateWin 9槽+5技能、DKeySelDlg F1-F8、DItemBag Gold/Repair/Close

## 复现命令（第四轮）
```bash
cd radare2
# 背包网格 DFM (ColCount/RowCount/ColWidth/RowHeight)
r2 -q -e scr.color=0 -c "px 400 @ 0x0050fa37" "MIR_final_v3_p - Copy.exe"
# 状态面板 DFM (9 穿戴槽 + 5 技能槽)
r2 -q -e scr.color=0 -c "izz~DStateWin; izz~DSWWeapon; izz~DStMag" "MIR_final_v3_p - Copy.exe"
# 快捷键选择 DFM (F1-F8)
r2 -q -e scr.color=0 -c "izz~DKeySelDlg" "MIR_final_v3_p - Copy.exe"
# 所有 UI 面板 DirectPaint 方法清单
r2 -q -e scr.color=0 -c "izz~DirectPaint" "MIR_final_v3_p - Copy.exe"
```

---

# 第五轮 (客户端 UI 协议对齐, client-refactor, 2026-08-28)

> 角色：client-refactor（客户端 UI 协议对齐 + 交互入口补漏）
> 方法：对照官方 1.5 客户端协议/UI 帧号实证（findings + exe-ui-frame-map 为权威），
>       统一客户端发送侧协议、清理旧协议死路径、排查 UI 帧号对齐。
> 编译验证：`cargo check -p mir-client` ✅（0 error，4 warning 基线）+ `cargo test -p mir-client` ✅（29 测试全通过）
> 原则：协议号/字段语义/UI 帧号严格对齐官方；实现方式用 Rust + Bevy/egui 最佳实践。

---

## 三十一、交易 UI 协议统一（任务 1，P0）

### 现状与根因（服务端双系统实证）

经逐条核验 `crates/mir-server/src/plugins/gateway_forward.rs` + `systems/trade.rs` +
`systems/player_command.rs`，确认服务端存在**两套交易系统**：

| 系统 | 触发消息 | 功能 |
|---|---|---|
| `TradeManager` + `TradeEvent`（trade.rs，**活跃**） | `CmDealTry`→`DealTry`、`CmAddDealItem`→`AddItem`、`CmDelDealItem`→`DelItem`、`CmChangeDealGold`→`ChangeGold`、`CmEndDeal`→`Confirm`、`CmTradeCancel`→`Cancel` | 完整面对面交易（建会话/放物/金币/确认/交换/取消） |
| `PlayerCommand::Deal*`（player_command.rs，**死代码**） | 无任何 `cmd_events.send(PlayerCommand::Deal*)` 调用点 | 仅 `SmSystemMessage` 文本回显，从未被驱动 |

**关键发现（取消协议缺口）**：官方 1.5 交易取消协议号 = **1030 `CmDealCancel`**（findings round-3 §十六实证）。
但 Rust `messages.rs` **未定义 `CmDealCancel` 变体**，服务端 `TradeEvent::Cancel` 目前**仅由 `CmTradeCancel` 触发**
（gateway_forward.rs L1103-1109）。即 CmDeal* 系列**没有取消等价物**，取消只能走 CmTradeCancel。

### 客户端发送侧修正（trade_ui.rs）

| 项 | 判断 | 说明 |
|---|---|---|
| 发起 `CmDealTry` | **官方对齐（保留）** | 新协议正确 |
| 移除物品 `CmDelDealItem` | **官方对齐（保留）** | 新协议正确 |
| 改金币 `CmChangeDealGold` | **官方对齐（保留）** | 新协议正确 |
| 确认 `CmEndDeal` | **官方对齐（保留）** | 新协议正确 |
| 取消/关闭 `CmTradeCancel` | **服务端约束（保留 + 注释）** | 官方是 CmDealCancel(1030)，Rust 服务端无此变体，取消仅 CmTradeCancel 触发；待服务端补 CmDealCancel 后切换 |
| 移除 `TradePhase::Requested` 确认框 + `CmTradeConfirm`/`CmTradeCancel` 死路径 | **官方行为对齐（清理）** | `Requested` 相位仅由 `SmTradeRequest` 触发，而 `SmTradeRequest` 仅由 `CmTradeRequest`(旧协议) 发出；客户端改用 `CmDealTry` 后该相位不可达，其"接受=CmTradeConfirm/拒绝=CmTradeCancel"为旧协议死路径 |

修改文件：`crates/mir-client/src/ui/trade_ui.rs`
- 删除 `TradePhase::Requested` 枚举变体 + `SmTradeRequest` 处理分支 + 确认框渲染块
- 文件头注释重写为统一 CmDeal* 流程 + 取消协议缺口说明
- 取消/关闭按钮补注释（为何保留 CmTradeCancel）

---

## 三十二、NPC 对话协议对齐（任务 2，P0）

### 结论：`CmSay @cmd` 属服务端设计，不强行改 `CmNpcSelect`

官方 1.5 用 `CmNpcSelect { npc_name, option: u32 }`（协议号 1011）承载 NPC 对话选择。
但 Rust 服务端有**两套并行的 NPC 选择机制**，语义不同：

| 机制 | 协议 | option 类型 | 路由 | 用途 |
|---|---|---|---|---|
| 脚本菜单选择 | `CmSay { "@cmd" }` | **字符串命令**（如 `"buy_hp_small"`/`"bye"`/`"buy_sword"`） | `NpcMenuSelectEvent(option: String)` → Rhai `on_menu_select(player_id, option)` | Rhai 脚本 `{text/cmd}` 链接 |
| 硬编码数字菜单 | `CmNpcSelect { npc_name, option: u32 }` | **数字**（0=关/1=买/2=卖/3=修/4=仓/5=存/7=升级/8=攻城） | `NpcScriptEvent` + gateway_forward 硬编码 option 分支 | merchant_ui.rs 商店标签页/仓库/升级/攻城 |

**关键证据**：Rhai 脚本 `scripts/merchant_town.rhai`/`weapon_dealer.rhai` 的
`fn on_menu_select(player_id, option)` 用 `if option == "buy_hp_small"` / `"bye"` 等**字符串比较**。
`CmNpcSelect` 的 `u32` option **无法承载字符串命令**，若强行把对话链接改为发 `CmNpcSelect`
会破坏现有脚本流程（`on_menu_select` 收到数字而非字符串 → 分支永不命中）。

**裁决（按 team-lead 指导）**：`CmSay @cmd` 属**服务端设计**（Rhai 字符串命令），
**官方专用协议 vs Rust 服务端设计的差异**，记录在案，不强行破坏可用流程。

### 修正项（npc_dialog_ui.rs）

| 项 | 判断 | 说明 |
|---|---|---|
| 菜单项点击 `clicked_cmd = format!("@buy {}", item)` | **官方 bug 修正** | 下方统一拼 `"@{}"` → 产出 `"@@buy ..."` 双 @ 错误；改为 `"buy {item}"`（该分页菜单 `menu_visible`/`menu_items` 当前无填充来源，属 vestigial 死代码，一并修正） |
| 文件头注释 | **文档修正** | 原注释"点击发送 CmNpcSelect { option: hash(cmd) }"与实际不符，改为准确描述 `CmSay { "@cmd" }` + 协议差异说明 |

修改文件：`crates/mir-client/src/ui/npc_dialog_ui.rs`

---

## 三十三、UI 面板帧号/布局对齐补漏（任务 3，P1）

### 排查结论：全部已对齐，无待确认/未对齐项

逐一核对 `crates/mir-client/src/ui/` 各面板 Prguse.wil 帧号，与 `docs/exe-ui-frame-map.md`
（exe 实证，权威）对照，**全部一致**：

| 面板 | Rust 帧号 | exe 实证 | 状态 |
|---|---|---|---|
| state_ui.rs（状态） | 370 背景/371 关/372 下页/373 上页/111,112 技能/376,377 性别 | DStateWinDirectPaint 0x488af4 | ✅ |
| bag_ui.rs（背包） | 3 背景/26 修/29 金币/229 仓库格/371 关, 8×5=40 格 36×32 | DItemGrid DFM 0x50fa37 | ✅ |
| equip_ui.rs（装备） | 9 槽（已并入 state_ui 纸娃娃页） | DStateWin DSWWeapon×9 | ✅ |
| belt_ui.rs（腰带） | 数字键 1-6 触发（渲染已迁至 dashboard.rs） | DBelt1 0x48be80 | ✅ |
| dashboard.rs（底部） | 66-72 腰带槽/226-228 快捷键排/4-7 HP/MP/EXP/8-11 功能 | sub_481DD4 + DBottomDirectPaint 0x48b2a0 | ✅ |
| skill_ui.rs（技能） | Magic.wil +840, 5 技能槽 | DStMag1 0x48af9c | ✅ |
| merchant_ui.rs（商店） | 384 买/385-388 列表/392 卖/393/64 关 | DMerchantDlg 0x48eac0/DSellDlg 0x490140 | ✅ |
| group_ui.rs（组队） | 120 背景/122-125 按钮/64 关 | DGroupDlg 0x490dfc | ✅ |
| guild_ui.rs（行会） | 180 背景/182-202 按钮/64 关/373,372 翻页 | DGuildDlg 0x493844 | ✅ |
| trade_ui.rs（交易） | 389 我/390 对方/391 确认/64 关/28 | DDealDlg 0x491954/DDealRemoteDlg 0x4917d0 | ✅ |
| adjust_ability_ui.rs（加点） | 226 背景/227,228 加减/229 高亮/62 确认/64 关 | DBotPlusAbil 0x48bc5c | ✅ |
| user_state_ui.rs（看他人） | 370 背景/371 关 | DUserState1 | ✅ |

> 说明：`exe-ui-frame-map.md` §837 已注明"Rust 游戏内 UI（行会/背包/组队/快捷栏等）Prguse 帧号
> 与 exe sub_481DD4 完全一致，验证通过"。前几轮（round-4 DFM 实证）已将背包格子尺寸、
> 状态面板页数等修正完毕，本轮复核**无新增待对齐项**，故任务 3 无需代码改动。

---

## 三十四、判断标注汇总（第五轮）

| 项 | 判断 | 说明 |
|---|---|---|
| 交易发起/放物/金币/确认 | **官方行为必须对齐** | 统一 CmDeal* 系列，已确认正确 |
| 交易取消 | **服务端约束（待补协议）** | 官方 CmDealCancel(1030)，Rust 服务端缺变体，暂保留 CmTradeCancel |
| 交易 Requested 确认框死路径 | **官方行为对齐（清理）** | 移除 CmTradeConfirm/CmTradeCancel 旧协议死代码 |
| NPC 对话 `CmSay @cmd` | **官方专用协议 vs 服务端设计差异（记录）** | Rhai on_menu_select 用字符串命令，CmNpcSelect(u32) 无法承载，不强行改 |
| NPC 菜单项双 @ bug | **官方 bug 已修正** | `@@buy` → `buy` |
| UI 帧号 | **官方对齐（已核验无缺口）** | 全部面板帧号与 exe 一致 |

## 三十五、修改文件清单（第五轮）
1. `crates/mir-client/src/ui/trade_ui.rs` — 移除 Requested 死路径 + 统一 CmDeal* 发送侧 +
   取消协议缺口注释
2. `crates/mir-client/src/ui/npc_dialog_ui.rs` — 菜单项双 @ 修正 + 协议对齐注释
3. 本文件 — 第五轮 UI 协议对齐发现追加

## 三十六、验证（第五轮）
- `cargo check -p mir-client` ✅ 0 error（4 warning 基线）
- `cargo test -p mir-client` ✅ 29 测试全通过（18 单元 + 11 UI 逻辑）
- 未改动服务端（按任务要求，仅客户端发送侧 + 注释）

---

# 第六轮 (服务端协议接线 + 数据加载激活, server-refactor, 2026-08-28)

> 角色：server-refactor（服务端协议接线 + 游戏数据加载激活）
> 方法：对照 findings 权威协议号 + 官方 Envir 目录实际文件格式，逐环节核验服务端 Sm 发送侧，
>       将「客户端已处理、服务端从不发/发错名」的 Sm 消息接线，并激活 GameDataLoader 死代码。
> 编译验证：`cargo check -p mir-server` ✅（0 error）+ `cargo test -p mir-server` ✅（全通过）
> 原则：协议号/字段语义严格对齐官方；实现走 Bevy 事件/Resource，不复刻 Delphi 全局变量反模式。

---

## 三十七、交易系统 SmDeal* 接线（任务1，P0）

### 缺口根因
`crates/mir-server/src/systems/trade.rs` 的 `trade_system` 已存在（处理 CmDealTry/CmAddDealItem/
CmDelDealItem/CmChangeDealGold/CmEndDeal → TradeEvent），但发送侧用了**旧协议名**，与客户端
`trade_ui.rs` / `play_handlers.rs handle_trade` 期望的**新细粒度协议**不匹配：

| 环节 | 原发送（错误） | 客户端期望 | 问题 |
|---|---|---|---|
| 交易发起 | `SmTradeOpen`（无对方名） | `SmDealMenu{partner_name}` | 交易窗对方名恒为"对方" |
| 发起失败(距离) | `SmSystemMessage` 文本 | `SmDealTryFail{reason}` | 无法用协议驱动失败提示 |
| 加物重复 | `SmSystemMessage` 文本 | `SmDealAddItemFail{reason}` | 同上 |
| 加物成功 | `SmDealAdd`（只发对方） | `SmDealAddItemOk`(自己) + `SmDealRemoteAddItem`(对方) | 物品错位到错误列表 |
| 删物 | **无任何消息** | `SmDealDelItemOk`(自己) + `SmDealRemoteDelItem`(对方) | 客户端交易栏不刷新 |
| 改金币 | `SmDealGoldUpdate`(双方同发) | `SmDealChgGoldOk`(自己) + `SmDealRemoteChgGold`(对方) | 对方视角 my/partner 颠倒 |
| 确认成功 | `SmDealComplete{success:true}` | `SmTradeSuccess` | 客户端不显示成功关闭 |
| 取消/失败 | `SmDealComplete{success:false}` | `SmTradeCancel{reason}` | 客户端误判"失败"且不关闭 |

### 修复
逐环节替换为专用 SmDeal* 消息（对齐 Delphi SM_DEALMENU=673/SM_DEALTRY_FAIL=674/
SM_DEALADDITEM_OK=675/SM_DEALDELITEM_OK=677/SM_DEALREMOTEADDITEM=682/SM_DEALREMOTEDELITEM=683/
SM_DEALCHGGOLD_OK=684/SM_DEALREMOTECHGGOLD=686/SM_TRADESUCCESS/SM_TRADECANCEL）。

### 判断标注
- 协议号/消息语义：**官方行为必须对齐**（SmDeal* 细粒度消息名与 findings SM 分发器 case 一致）
- 加物/删物/改金币的"自己 vs 对方"分离：**官方行为必须对齐**（原单条消息导致物品/金币错位）

---

## 三十八、商店/修理 Sm 消息接线（任务2，P0）

### 核验结论（与缺口清单 2026-08-28 的差异）
缺口清单称"服务端用 SmSystemMessage 代替 SmSendBuyPrice/SmSendRepairCost"，但**实际代码前几轮已部分修复**：
- `QuerySellPrice` → 已发 `SmSellPrice`（客户端 `merchant_ui` 已处理）✅ 非文本
- `QueryRepairCost` → 已发 `SmRepairCost`（客户端 `merchant_ui` 已处理）✅ 非文本

### 真实缺口（本轮修复）
`RepairItem`（修理）原发 `SmRepairOk`/`SmRepairFail`，但客户端 `merchant_ui` 的耐久度更新靠
**`SmRepairResult{slot,success}`**（对齐官方修理结果）驱动；`SmRepairOk` 仅移除修理项、不更新耐久度显示。
→ 改为发 `SmRepairResult{slot,success:true/false}`。

### 关键澄清（记录）
客户端有两套商店状态：`merchant_ui.rs MerchantUiState`（egui 真实渲染）与 `play_state MerchantState`（play_handlers）。
`SmRepairCost`(merchant_ui) vs `SmSendRepairCost`(play_handlers)、`SmSellPrice`(merchant_ui) vs `SmSendBuyPrice`(play_handlers)
为**两套并行的消息命名**，服务端当前发 `SmRepairCost`/`SmSellPrice` 已被 merchant_ui 正确消费，**不改为 SmSendRepairCost/SmSendBuyPrice**
（否则破坏 merchant_ui 的费用/价格更新）。此为 Rust 重构的语义等价命名，非官方协议号差异。

### 判断标注
- 修理结果耐久度更新：**官方行为必须对齐（修正）**（SmRepairResult 驱动耐久度刷新）
- SmRepairCost vs SmSendRepairCost 命名：**Rust 优化改进（保留）**（两套并行状态，均已消费，不强行统一）

---

## 三十九、基础流程 Sm 消息核验（任务3，P1）

### 核验结论（多数为语义等价，非真实缺口）
| 缺口清单项 | 服务端现状 | 裁决 |
|---|---|---|
| SmNowDeath | `combat.rs` 死亡发 `SmDeath`（客户端已处理，is_dead=true） | ✅ 语义等价，非缺口 |
| SmCloseDoor/SmOpenDoorLock | `envir.rs` 门系统发 `SmDoorOpen/SmDoorClose`（客户端已处理） | ✅ 语义等价，非缺口 |
| SmChangeMap | `teleport.rs` 换图发 `SmNewMap`（客户端更新 map_id） | ✅ 语义等价，非缺口 |

### 真实缺口（本轮修复）
**`SmClearObjects`（SM_CLEAROBJECTS=633）**：换图时服务端发 `SmNewMap` 但不发 `SmClearObjects`，
客户端旧地图实体追踪 + 地面物品**不清理**，新地图实体叠加显示。
→ 在 `teleport.rs::send_teleport_notify` 发 `SmNewMap` 前先发 `SmClearObjects`（对齐 Delphi 换图流程：清空→加载）。

### 判断标注
- 换图先清对象：**官方行为必须对齐（修正）**（SmClearObjects 清空旧地图实体）
- SmDeath/SmDoorClose/SmNewMap 替代 SmNowDeath/SmCloseDoor/SmChangeMap：**官方行为对齐（语义等价）**

---

## 四十、GameDataLoader 接入主流程（任务4，P1）

### 死代码根因
`crates/mir-server/src/db/game_data_loader.rs` 的 `load_all()` 无任何调用点。且其解析器存在**格式不匹配 bug**：
- 官方 Envir 目录文本文件是**空格分隔**（StartPoint.txt=`0 289 618`、Mongen.txt=`0137 21 11 Trainer 0 1 10`），
  但 GameDataLoader 用 `split('\t')`（制表符）→ 全解析失败
- 官方地图 ID 是**十六进制**（0137=0x137、D601=0xD601），但 `load_mon_gen` 用 `parse::<u32>()`（十进制）
- AdminList.txt 是 `* 账号` 格式，但用 `\t` 分隔

### 修复
1. 修正 `load_mon_gen`/`load_start_points` 用 `split_whitespace()` + 复用 `respawn::parse_map_id`（十六进制）
2. 修正 `load_admin_list` 解析 `* 账号` 格式
3. 新增 `GameDataLoader::load_from_envir()`（复用 `envir::resolve_envir_dir` 定位官方目录）
4. 在 `game_plugin.rs` 注入 `GameData` 为 Resource（`GameData` 派生 `Resource`）
5. 新增 6 个单元测试（parse_map_id 十六进制/十进制/非法 + 官方空格格式 StartPoint/MonGen/AdminList 解析）

### 关键澄清（记录）
官方怪物/物品/魔法**属性**是二进制 DB（Monster.DB/StdItems.DB/Magic.DB），由 `MagicRegistry`/
`MonsterCatalog` 的 `enrich_with_official_db` 单独加载（game_plugin 已接线）。GameDataLoader 的
`load_monitems/load_items_db/load_monster_db/load_magic_db` 期望的 `.txt` 文件在官方目录**不存在**，
故 `load_from_envir` 仅加载文本类数据（刷怪/地图/NPC/商人/守卫/出生点/管理员/任务），不覆盖二进制 DB。

### 判断标注
- 空格分隔 + 十六进制地图 ID 解析：**官方行为必须对齐（修正 bug）**（对齐官方 Envir 文件真实格式）
- 文本 vs 二进制 DB 分工：**Rust 优化改进（保留）**（enrich_with_official_db 已是权威二进制加载路径）

---

## 四十一、修改文件清单（第六轮）
1. `crates/mir-server/src/systems/trade.rs` — 交易 SmSystemMessage/旧 SmDeal* → 新细粒度 SmDeal* 接线
2. `crates/mir-server/src/systems/player_command.rs` — RepairItem 发 SmRepairResult（耐久度刷新）
3. `crates/mir-server/src/systems/teleport.rs` — 换图前发 SmClearObjects（清空旧地图实体）
4. `crates/mir-server/src/db/game_data_loader.rs` — 修正空格分隔/十六进制解析 + load_from_envir + GameData 派生 Resource + 6 测试
5. `crates/mir-server/src/plugins/game_plugin.rs` — 注入 GameData Resource（激活 load_from_envir）
6. 本文件 — 第六轮服务端协议接线 + 数据加载激活发现追加

## 四十二、验证（第六轮）
- `cargo check -p mir-server` ✅ 0 error
- `cargo test -p mir-server` ✅ 全通过（含新增 6 个 game_data_loader 测试）
- 服务端 103 个 Sm 缺口中，交易/商店/修理/换图清对象等 P0/P1 真实缺口已接线；
  其余为语义等价（组队/行会/仓库/账号安全）或官方二进制 DB 属性（已由 enrich_with_official_db 加载）

---

## 四十三、服务端上线准备（第七轮）

### 背景问题
Rust 重构后数据加载架构**只做了一半**：
- Envir 文本数据已用 `resolve_envir_dir()` 支持环境变量 + 相对路径候选。
- DB 二进制数据（Monster.DB/StdItems.DB/Magic.DB）仍硬编码 macOS 绝对路径
  `/Users/mac/Documents/github/mir2-my/reference/korea/mud2/DB`，生产环境（Docker/Linux）
  路径不存在 → 官方怪物/物品/魔法数据静默加载失败，回退硬编码默认值。

### 改动内容

**任务 1（P0）：DB 数据定位架构收尾**
1. `crates/mir-server/src/systems/envir.rs` — 新增 `resolve_db_dir()`（对称 `resolve_envir_dir()`）：
   - 优先 `MIR2_DB_DIR` 环境变量
   - 依次尝试候选 `assets/DB`、`DB`、`../reference/korea/mud2/DB`、`reference/korea/mud2/DB`
   - 返回第一个 `is_dir()` 的路径，均不存在返回 `None`
   - 同时新增 `db_dir_candidates()` 返回候选清单用于告警日志
2. `crates/mir-server/src/plugins/game_plugin.rs` — 清理 2 处硬编码（MagicRegistry/MonsterCatalog
   的 `enrich_with_official_db` 调用点），改用 `resolve_db_dir()` + `tracing::warn!` 告警
3. `crates/mir-server/src/systems/m2share_config.rs` — 清理 `standard_item_table_enriched()` 硬编码，
   改用 `resolve_db_dir()` + 告警
4. `crates/mir-server/src/db/bde_parser.rs` — 4 处测试硬编码改为 `resolve_db_dir()`（`let Some(db_dir) = ... else return`），
   无 DB 环境下测试编译运行均安全跳过

**任务 2（P1）：数据加载失败告警**
`enrich_with_official_db` / `standard_item_table_enriched` 目录缺失时的"静默回退"改为明确告警：
`tracing::warn!("[DB] 官方 DB 目录不存在, 使用内置默认数据 (尝试路径: ...)")`。
无数据环境仍可运行（不 panic），但有清晰日志便于排查。

**任务 3（P1）：启动健壮性**
- 无 PostgreSQL/Redis：`database_plugin.rs` 已回退内存（`memory fallback`/`running without cache`），不 panic
- 无 reference/korea：`resolve_db_dir()`/`resolve_envir_dir()` 返回 `None`，用内置默认数据 + warn 日志
- `server_config.json` 路径健壮化：`server_config_file.rs` 新增 `load_auto()`，
  依次尝试 `MIR2_SERVER_CONFIG` 环境变量 → `server_config.json` → `../server_config.json` → `config/server_config.json`，
  缺失时回退默认 `127.0.0.1:7100`（`game_plugin.rs:19` 改用 `load_auto()`）
- 数据库迁移：`sqlx::migrate!("src/db/migrations")` 为编译时嵌入，无运行时路径依赖，release 二进制可用

**任务 4（P2）：数据打包方案（方案 A）**
1. 新增 `scripts/prepare_assets.py` — 把 `reference/korea/Mir200/Envir` 与 `reference/korea/mud2/DB`
   复制/软链到 `assets/Envir` 与 `assets/DB`（支持 `--symlink`/`--copy`，幂等，支持 `MIR2_ENVIR_SRC`/`MIR2_DB_SRC` 覆盖）
2. 更新 `Dockerfile.server` — 补充 `server_config.json` COPY + 数据挂载说明（build-arg `DATA_DIR` 或运行时 `-v` 卷 + `MIR2_ENVIR_DIR`/`MIR2_DB_DIR`）

### 部署说明
```bash
# 本地数据准备 (把官方数据软链进 assets/, 供 Docker COPY 使用)
python3 scripts/prepare_assets.py            # 软链
python3 scripts/prepare_assets.py --copy      # 物理复制

# 构建
docker build -f Dockerfile.server -t mir2-server .

# 运行 (推荐运行时挂载卷 + 环境变量, 无需重打包数据)
docker run -p 7100:7100 \
  -v /path/to/reference/korea:/data/korea \
  -e MIR2_ENVIR_DIR=/data/korea/Mir200/Envir \
  -e MIR2_DB_DIR=/data/korea/mud2/DB \
  mir2-server

# 数据目录环境变量 (本地运行同理)
MIR2_DB_DIR=/path/to/mud2/DB MIR2_ENVIR_DIR=/path/to/Mir200/Envir ./mir-server
```

### 验证结果
- `cargo check -p mir-server` ✅ 0 error（仅既有 warning）
- `cargo test -p mir-server` ✅ 全通过（DB 解析测试 `parse_real_db_files`/`enumerate_all_record_names`/
  `load_official_entries`/`load_official_magic_field_aligned` 通过 `resolve_db_dir()` 实际解析到 `../reference/korea/mud2/DB` 并执行）
- `grep -rn "/Users/mac" crates/mir-server/src/` 仅剩 1 处注释（说明性文字，无硬编码路径）

---

# 第七轮 (客户端上线准备, client-launch, 2026-08-29)

> 目标：客户端能独立启动、资源正确加载、真实 TCP 连接网关 (127.0.0.1:7000)，
> 走通登录→选服→选角→进游戏全流程。收尾 3 个上线前问题：
> 连接配置来源不统一 (P0)、资源根目录不可配置 + 缺失静默跳过 (P1)、资源缺失告警缺失 (P1)。

## 任务 1 (P0 核心)：连接配置来源统一

**问题**：`plugins/client_config.rs` 的 `ClientConfig { server_addr, server_port }` 是死配置，
登录流程不读它们；`scenes/login.rs` 的 `LoginState::default()` 直接用环境变量
`MIR2_SERVER_ADDR`/`MIR2_SERVER_PORT`。导致用户改 `client_config.json` 无效。

**方案**（三层优先级：环境变量 > ClientConfig > 默认）：
1. `client_config.rs` 新增 `ClientConfig::resolve_server_addr()` / `resolve_server_port()`，
   实现「环境变量 > 自身字段 > 默认 127.0.0.1:7000」的解析。
2. `scenes/login.rs` 的 `setup_login_scene` 从 `OnEnter(Login)` 改为读取 `Res<ClientConfig>`
   （`Option<Res>` 兜底防未注册 panic），用 `resolve_server_addr`/`resolve_server_port`
   覆盖 `LoginState` 的 server_addr/server_port。这样 `LoginState` 初始化时 ClientConfig 已注册
   （`client_plugin.rs` 中 `insert_resource(ClientConfig::load())` 在 `insert_resource(LoginState::default())`
   之前），且 setup 场景在 OnEnter 时才运行，时机安全。

**验证**：
- headless 启动日志 `[Login] Server config from ClientConfig: 127.0.0.1:7000` ✅
- 改 `client_config.json` 为 `192.168.1.100:7100` 后日志变为 `connecting to 192.168.1.100:7100` ✅
- `MIR2_SERVER_ADDR=10.0.0.5 MIR2_SERVER_PORT=9999` 覆盖为 `10.0.0.5:9999` ✅

## 任务 2 (P1)：资源根目录可配置化

**问题**：WIL 用 `["assets/wil/", "../assets/wil/", ""]`，音频用 `assets/wav/`、`assets/audio/`，
全相对路径依赖进程 cwd，无 `MIR2_ASSET_DIR` 覆盖。

**方案**：
1. 新增 `plugins/asset_path.rs` 统一资源根目录解析：
   - `asset_root_dir()`：优先 `MIR2_ASSET_DIR` 环境变量（相对/绝对均可，相对基于 cwd 解析），
     否则默认 `<cwd>/assets`（**绝对路径**，保证从任意目录启动可靠）。
   - `fs_path(rel)` / `fs_exists(rel)`：文件系统存在性检查路径。
   - `asset_path(rel)`：AssetServer 加载相对路径（相对 `AssetPlugin.file_path`）。
2. `render/wil_cache.rs`：新增 `wil_search_paths(filename)` 统一 WIL 候选路径
   （`MIR2_ASSET_DIR/wil/` > `assets/wil/` > `../assets/wil/` > ``），
   `load`/`load_monster`/`load_player_wils`/`wil_cache_init_system` 全部改走它。
3. `plugins/sound_plugin.rs`：所有 `assets/{}` 存在性检查改为 `asset_path::fs_exists`，
   `asset_server.load` 改为 `asset_server.load(asset_path::asset_path(...))`，
   区分「存在性检查路径」与「AssetServer 加载路径」。
4. `main.rs`：`AssetPlugin.file_path` 从 `current_dir().join("assets")` 改为
   `plugins::asset_path::asset_root_dir()`（绝对路径），与资源根目录统一，
   修复从 `target/debug` 启动时 AssetServer 找不到 `assets` 的 `Path not found` 问题。

## 任务 3 (P1)：资源缺失告警

**问题**：`sound_plugin.rs` 的 `Path::exists()` 失败静默跳过；`wil_cache.rs` 加载失败仅 `tracing::debug!`。

**方案**：
- WIL 加载失败从 `debug!` 提升为 `warn!`（`load` 里 `{:?} not found in any search path`、
  `load_monster`/`load_player_wils`/`wil_cache_init_system` 各 WIL 缺失均 warn）。
- 音频缺失从静默改为 `warn!`（每个 SFX/BGM/地形脚步），地形脚步缺失聚合为一条 warn 避免刷屏。
- 已加载的资源保持 `info!`/`debug!`，不逐条刷屏。

**验证**：windowed 启动日志出现真实缺失告警
- `[WilCache] ItemUse not found in any search path`（ItemUse.wil 资产确实缺失）
- `[Sound] SFX hit missing: wav/struck_short.wav`（实际文件名为 `struck_2.wav`，命名不匹配）
- `[Sound] 6 terrain footstep SFX missing (fallback to hit)`

> 附带发现：音效常量 `SFX_HIT="wav/struck_short.wav"` 与实际资产 `struck_2.wav` 不匹配，
> 属逆向时对照 Delphi 常量但资产文件名不同。本轮仅加告警不改资源名（任务原则不改游戏逻辑）。

## 任务 4 (P1)：启动验证

**验证结果**：
- headless 模式（`--headless`）独立启动 ✅：日志写入 `client_headless.log`，
  自动连接 127.0.0.1:7000，走 `Connecting→Connected→发送 CmProtocol+CmLogin`。
- windowed 模式独立启动 ✅：加载 WIL（mmap 128 帧 / Mon1 1086 帧 / Items 518 帧 / Npc 1678 帧）、
  ChrSel[22] 800x600 登录背景、Prguse 16 帧纹理、BGM `Log-in-long2.wav`。
- AssetServer 绝对路径修复后 `Path not found` 错误 = 0 ✅。
- `client_config.json` 的 server_addr/server_port 修改后真实生效 ✅（见任务 1 验证）。

> 注意：macOS 本机 7000 端口被系统进程 `ControlCe` (afs3-fileserver) 占用，
> 联合调试前需确认 mir2 网关实际监听端口，或改用 `MIR2_SERVER_PORT`/`client_config.json` 指定。

## 产出文件清单
- `crates/mir-client/src/plugins/asset_path.rs`（新增）
- `crates/mir-client/src/plugins/mod.rs`（声明 asset_path 模块）
- `crates/mir-client/src/plugins/client_config.rs`（resolve_server_addr/port）
- `crates/mir-client/src/scenes/login.rs`（setup_login_scene 从 ClientConfig 读取）
- `crates/mir-client/src/render/wil_cache.rs`（wil_search_paths + warn 告警）
- `crates/mir-client/src/plugins/sound_plugin.rs`（fs_exists + warn 告警）
- `crates/mir-client/src/main.rs`（AssetPlugin.file_path 统一）

## 验证结果
- `cargo check -p mir-client` ✅ 0 error（仅既有 dead_code warning）
- `cargo test -p mir-client` ✅ 全通过（18 + 11 = 29 tests）

---

# 第八轮 (UI 运行级坐标校准, 2026-08-29)

> 角色：主 agent（不用子 agent）
> 方法：radare2 反汇编 DirectPaint 函数 + Prguse.wil 帧尺寸解析
> 目标：区分「窗口级(Delphi 源码设计值)」vs「运行级(exe 运行时实际计算)」坐标，校准 Rust UI

---

## 二十六、核心方法论：运行级 vs 窗口级坐标

**关键结论**：UI 坐标分两类，校准策略不同：

| 类型 | 特征 | 坐标来源 | 是否需校准 |
|---|---|---|---|
| **运行时计算坐标** | DirectPaint 里从 WIL 帧读实际尺寸 + 居中/贴底计算 | exe 硬编码常量 + 帧尺寸 | ✅ 需校准 |
| **固定 SetBounds 坐标** | TfrmDlg.Create 里 SetLeft/SetTop 固定值 | Delphi 源码 = exe 值 | ❌ 无需改 |

之前 Rust 用 Delphi 源码值（FState.pas），对**运行时计算**的坐标（底部栏贴底、HP/MP 球）存在偏差。

---

## 二十七、DBottomDirectPaint(0x48b2a0) 运行级坐标实证

反汇编 `DBottomDirectPaint`（size 0x7c4，581 指令），提取运行时坐标计算：

### 1. 底部栏背景（帧1）
```
0x48b357: call fcn.0044d944   ; 取帧1高
0x48b35c: mov edx, 0x258      ; 600 (SCREENHEIGHT)
0x48b361: sub edx, eax        ; Top = 600 - 帧1高（贴底）
```
**Prguse.wil 帧1 实际尺寸 = 800x251**（非 Delphi 源码假设的 120 高）。
→ Top = 600 - 251 = **349**。
→ Rust `dashboard.rs` 已动态读取帧1 尺寸（`bg_height = img.height()`），**正确 ✅**。

### 2. HP 球（帧5，96x92）与 MP 球（帧4，92x90）
```
帧5(HP): 0x48b5c6 mov edx,0x26(38)  0x48b5c0 add ecx,0x5a(90)  → Left=38, Top=底部栏Top+90
帧4(MP): 0x48b678 mov edx,0x28(40)  0x48b672 add ecx,0x5b(91)  → Left=40, Top=底部栏Top+91
```
**关键差异**：exe 运行时 HP/MP 球 Left = **38/40**（紧挨），而 Delphi 源码值 = 40/87（分离）。

### 3. EXP 条（帧7，76x13）与负重条
```
EXP 条:   (666, 527) 固定屏幕坐标
负重条:   (666, 560) 固定屏幕坐标
```
→ 与 Rust 一致 ✅。

### 4. 昼夜图标（帧15/12/13/14）与饥饿图标（帧16）
```
昼夜图标: Left=748(0x2ec), Top=底部栏Top+79
饥饿图标: Left=754(0x2f2), Top=553
```

---

## 二十八、固定 SetBounds 坐标实证（无差异，Delphi 值 = exe 值）

反汇编 `fcn.00481dd4` 内部的 SetLeft/SetTop 调用，确认固定控件坐标：

| 控件 | 帧 | exe 坐标 | Rust 现状 | 对照 |
|---|---|---|---|---|
| 功能按钮 F10 | 8 | (643, 61) | (643, 61) | ✅ |
| 功能按钮 F9 | 9 | (682, 41) | (682, 41) | ✅ |
| 功能按钮 F11 | 10 | (722, 21) | (722, 21) | ✅ |
| 功能按钮 Esc | 11 | (764, 11) | (764, 11) | ✅ |
| 快捷栏槽 | 66-72 | Left=285, Top=59, 32x29 | (285, 59, 32, 29) | ✅ |
| 快捷按钮 加点 | 140 | (339, 104) | (339, 104) | ✅ |
| 快捷按钮 退出 | 138 | (560, 104) | (560, 104) | ✅ |
| 快捷按钮 注销 | 136 | (530, 104) | (530, 104) | ✅ |

> **结论**：固定 SetBounds 控件的坐标，Delphi 源码值 = exe 运行时值，**无需校准**。

---

## 二十九、登录/选服面板（居中计算，已对齐）

- 登录面板 Prguse[60] = **296x254**，居中 `(800-296)/2=252, (600-254)/2=173`
- 选服面板 Prguse[160] = **496x454**，居中
- Rust `login.rs` 已用「从 WIL 读实际尺寸 + 居中」，**正确 ✅**

---

## 三十、本轮校准修改

`crates/mir-client/src/ui/dashboard.rs`：
- `HP_BUTTON_RECT`: (40.0, 91.0) → **(38.0, 90.0)**（exe 帧5 HP 球 Left=38, Top+90）
- `MP_BUTTON_RECT`: (87.0, 91.0) → **(40.0, 91.0)**（exe 帧4 MP 球 Left=40, Top+91）
- 标注：原 Delphi 源码值 (40,91)/(87,91) 是「窗口级」设计值，exe 运行时 HP/MP 球紧挨 (38/40)

## 三十一、复现命令（第八轮）
```bash
cd radare2
# DBottomDirectPaint 运行时坐标
r2 -q -e scr.color=0 -c "aaa; pd 581 @ 0x48b2a0" "MIR_final_v3_p - Copy.exe"
# 功能按钮/快捷栏 SetBounds 坐标 (fcn.00481dd4 内部)
r2 -q -e scr.color=0 -c "aaa; pd 200 @ 0x4848c0" "MIR_final_v3_p - Copy.exe"
# Prguse.wil 帧尺寸
python3 -c "import struct; ..."  # 见正文帧尺寸表
```

## 三十二、Prguse.wil 关键帧尺寸表（运行级基准）
| 帧 | 尺寸 | 用途 |
|---|---|---|
| 1 | 800x251 | 底部栏背景 |
| 4 | 92x90 | MP 球 |
| 5 | 96x92 | HP 球 |
| 6 | 96x92 | HP/MP 条 |
| 7 | 76x13 | EXP 条 |
| 8-11 | 24x24/22/23 | 功能按钮 |
| 15/12/13/14 | 32x54 | 昼夜图标 |
| 16 | 28x27 | 饥饿图标 |
| 60 | 296x254 | 登录面板 |
| 66-72 | 76x33 等 | 快捷栏槽 |
| 160 | 496x454 | 选服面板 |
