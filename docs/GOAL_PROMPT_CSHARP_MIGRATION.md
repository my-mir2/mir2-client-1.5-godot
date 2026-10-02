# 热血传奇 1.5 客户端全量 .NET / C# 单轨化平移长周期执行指令 (Goal Prompt)

复制以下提示词并在聊天框中使用 `/goal` 触发执行：

```markdown
/goal 你现在全权负责将热血传奇 1.5 客户端（`mir2-client1.5-delphi`）从当前的 GDScript 全量平移至纯 .NET / C#（Godot.NET SDK 4.4 / .NET 10 / net8.0 目标）单轨化工业级架构。最终交付目标为 Windows 平台（支持 Native AOT 极限性能展开与单文件独立发布），macOS 作为当前开发与编译验证环境。

### 一、最高原则与行为底线 (Zero-Manual-Reminder)
1. **单轨化彻底推进**：废除 GDScript 与 C# 双轨制。严禁保留两套测试或两套代码，必须将网络协议、数据模型、实体状态机、游戏渲染底层以及 24 个原生 UI 窗体全部迁移为强类型 C#。
2. **测试策略绝不挂起**：严禁通过 `godot-mono --headless <scene.tscn>` 的游戏循环运行单元测试（该方式在 Headless 下会因事件泵等待导致进程挂起）。所有逻辑、公式、算法与协议测试，一律编写为标准 xUnit 单元测试，统一由 `dotnet test tests/CSharp/Mir2Client.Tests.csproj` 驱动，必须在 2 秒内毫秒级极速退出并返回测试报告。
3. **坏账优先与零自嗨铁律**：每完成一个模块的迁移，必须先汇报遇到的边界缺陷、类型不兼容或性能损耗，再陈述迁移进度。严禁虚假 Mock，严禁断言放水。
4. **官方 1.5 封闭分母对齐**：
   - 协议全集：164 个 CM/SM 协议封包与解包 100% 互逆且强类型化；
   - 技能全集：官方 32 门技能公式、前后摇、光效编号与伤害公式 100% C# 化；
   - UI 界面：24 个官方原生生命周期窗体（登录、选服、选角、HUD、背包、装备、技能、加点、快捷键、组队、行会、交易、仓库、商店、修理、NPC、模态框、观人、小地图、大地图、死亡灰屏、Tooltip、二级密码）1:1 对齐 `Prguse.wil` 贴图与 Delphi 交互手感；
   - 交互自治：多窗口叠加 LIFO ESC 关闭栈、空白处右键关闭、全看板拖拽防抖、大世界点击穿透阻断。

---

### 二、四阶段平移施工路线图

#### 阶段 1：网络与数据核心全量 C# 化 (已启动，需彻底核销)
- 完善 `src/Net/` 下的强类型数据包定义：实现全量 64 个 CM 结构体与 64 个 SM 结构体；
- 实现 `src/Net/NetManager.cs`：基于 `System.Net.Sockets.TcpClient` 或 Godot `StreamPeerTcp`，实现强类型异步分发；
- 扩展 `tests/CSharp/TestProtocolMatrix.cs`，对齐原 `tests/test_protocol_matrix.gd` 中的全部 52 项 CM + 82 项 SM 编解码测试，确保 `dotnet test` 100% PASS。

#### 阶段 2：技能、战斗与实体核心引擎 C# 化
- 建立 `src/Game/` 目录：
  - `SkillFormulas.cs`：战法道全量 32 门技能消耗、伤害与等级加成公式；
  - `SpecialRings.cs`：7 大特戒（麻痹、护身、复活、传送、隐身、超负载、火焰）与 4 大套装（记忆、魔血、虹魔、祈祷）底层判定逻辑；
  - `PlayerState.cs` 与 `EntityManager.cs`：实体插值位移、转向、攻击、施法状态机与 9 大装备槽位数据模型；
  - `DropGeometryEngine.cs` & `PathFinder.cs`：打怪爆物螺旋散落几何算法与 8 方向防穿角 A* 寻路；
- 建立 `tests/CSharp/TestCombatSkillsMatrix.cs`，将 `test_skills_comprehensive_matrix.gd` 与 `test_combat_magic_chat_audio.gd` 全量移植为 xUnit 事实断言，执行 `dotnet test` 确保无任何 FAIL。

#### 阶段 3：渲染管线与 WIL 资源引擎 C# 优化
- 建立 `src/Render/` 目录：
  - `WilLoader.cs`：完成 16-bit 565 调色板解码与 WIX 帧索引定位，支持 `unsafe` 指针与直接内存操作；
  - `MapRenderer.cs`：地图瓦片与建筑分层批处理，对齐 IDA `sub_47009C`；
  - `SpriteEngine.cs`：纸娃娃外观图层混合（衣服、武器、发型、光效）与动态深度重排；
- 编写 `tests/CSharp/TestRenderMatrix.cs` 校验地图 .map 瓦片读取与 WIL 解压正确性。

#### 阶段 4：24 个原生 UI 窗体与场景树 C# 绑定
- 建立 `src/UI/` 目录，将现有的 24 个 UI 界面平移为继承自 `Godot.Control` 的强类型 C# 类：
  - `HUD.cs`：底栏血球/蓝球裁剪、快捷键、LIFO 窗口栈（`PushWindow`, `PopWindow`, `CloseTopDialog`）、`IsMouseOverUi` 物理防穿透；
  - `TradePanel.cs`：对方/己方双面板，独立边界阻断，防止全屏 800x600 容器导致大世界假死；
  - `ShopPanel.cs`：10 行商品陈列、翻页、特修、纳入 HUD 窗口栈；
  - `StoragePanel.cs`、`AdjustAbilPanel.cs`、`GroupPanel.cs`、`GuildPanel.cs`、`ItemTooltip.cs` 等全部完成挂载转换；
- 更新 `.tscn` 场景文件中的脚本引用，使场景直接挂载对应的 C# 脚本；
- 逐步移除过时的 `scripts/**/*.gd`，使整个工程成为纯粹的 C# / .NET 现代化架构。

---

### 三、自主闭环验收标准
在宣告每个阶段完成前，必须自主执行以下验证：
1. `dotnet build Mir2Client.sln` 零警告、零错误；
2. `dotnet test tests/CSharp/Mir2Client.Tests.csproj` 毫秒级极速跑通所有 xUnit 断言，FAIL 必须严格为 0；
3. 检查代码目录，杜绝未使用的死代码，确保所有的迁移都有对应实证记录在 `docs/AUDIT.md` 中。

请立即开始执行阶段 1 的剩余网络模型补全，并在每一个阶段完成后主动向我汇报进展与坏账情况！
```
