# 项目规范与核心记忆

## 1. 服务端接入与联合调测规范（用户授权：方案 A）
- **允许访问 `mir2-my` 服务端**：经用户明确授权，全面开放读取、分析与运行 `/Users/mac/Documents/github/mir2-my` 目录下的 Rust 服务端（Server）源码、协议定义、配置文件与启动脚本，用于双端协议对齐与联合调试。
- **客户端实证原则**：客户端的表现层、UI 像素与核心渲染逻辑依然以官方原版二进制 (`MIR_final_v3_p.exe`) IDA Pro 逆向和官方 WIL/WIX 资产为基准。

## 1.1 官方 1.5 唯一真值基准与非标 1.5+ (delphi-custom) 绝绝对对隔离铁律
- **唯一真值来源**：本项目当前以盛大官方原版 1.5 (`MIR_final_v3_p.exe`) 的 IDA MCP 逆向元数据为唯一准绳。
- **本地 IDA 元数据检索优先**：所有 IDA MCP 逆向成果已完全导出并归档在本项目工程内（如 `docs/ida_evidence/SM_truth_table.md`, `docs/ida_evidence/dialog_truth_table.md`, `docs/ida_evidence/sub_ledger.md`, `scratch/ida_processmsg.c` 等）。**无需额外调用外部 IDA MCP，直接在项目工程内进行文本搜索即可获得官方 1.5 的绝对物理真值**！
- **严禁参考与混淆 `delphi-custom`**：`delphi-custom` 是一个非标的、更高的 1.5+ 私服分支，存在大量协议篡改、非官方魔改字段与逻辑漂移，**绝对严禁将其作为参考分母或开发依据**！
- **协议收敛铁律**：协议通信只遵守官方 1.5 原版二进制有什么，客户端就实现什么。
- **双端分工重心**：服务端为 1.4 底座（`mir2-my`），与 1.5 差异不大；当前项目的最高战略重心始终且唯一是**客户端的表现层与逻辑细节 1:1 闭环**。

## 2. 真实性与实证原则
- 所有 1:1 像素级复刻、逆向实证与客户端逻辑必须完全自主完成。
- 直接基于官方原版二进制 (`MIR_final_v3_p.exe`)、IDA Pro MCP 实时逆向分析、本工程内原始 WIL/WIX 资产解析，以及 Godot 4 引擎实机渲染与视觉审计。

## 3. 核心行为底线：全自动闭环与自查自愈规范（Zero-Manual-Reminder）
1. **自检前置原则**：
   - 任何涉及表现层、UI、通信或运行状态的任务，Agent 必须自主走完“静态代码审查 -> 依赖服务自检 -> 端到端自动化收发包 -> 真实截屏机器审计 -> 运行时日志错误零容忍扫描”全套闭环。
   - 严禁向用户交付未经实机渲染验证的半成品；严禁依赖用户人肉截图或在会话中提醒才去修复明显的断链与硬编码缺陷。

2. **视觉交付金标准**：
   - 启动任何前端/客户端后，必须主动抓取当前 Viewport/窗口帧并进行自验；
   - 凡出现“纯黑背景”、“贴图丢失导致的回退默认控件”、“未清除的开发硬编码（如 auto_tester）”或“调试辅助线残留”，一律视为交付阻断性缺陷（P0），必须自主修复至符合官方 1:1 美术表现后方可交付。

3. **运行时日志零容忍**：
   - 交付前必须主动审查引擎与服务端运行时日志文件（如 `godot.log`、`server.log`、`gateway.log`）；
   - 任何 `ERROR:`、`SCRIPT ERROR:` 或资源加载失败，均必须主动归零，不得静默跳过。

## 4. 官方 1.5 还原度绝对审计与验收宪法 (Official 1.5 Ground Truth Audit Axiom)
1. **唯一审计基准 (Single Source of Truth)**：
   - 全项目的 1:1 还原度审计标准与进度度量，以 [`docs/AUDIT.md`](file:///Users/mac/Documents/github/mir2-client1.5-delphi/docs/AUDIT.md) 为唯一最高大账本。
   - 任何自封的“功能完成”、“已完美复刻”若未在 `docs/AUDIT.md` 中完成官方实证核销，一律视为无效自嗨，严禁向用户汇报为成果。

2. **四大维度的封闭分母原则 (Closed Denominator Protocol)**：
   - 官方传奇 1.5 属于历史完全冻结的有限封闭系统，必须且仅能依据四大封闭分母进行还原度审计：
     - **协议全集维度**：官方 128+ 个 CM/SM 协议的包头、参数、Extra 编码及双端状态流转（拒绝任何私服非标协议号）；
     - **技能全集维度**：战法道全量官方技能（官方权威 ID 1~32，扩展至 1.5 终极技能池），覆盖施法前摇/后摇、动作号、光效动画、音效、消耗与伤害公式；
     - **UI 界面维度**：官方 24 个生命周期全量原生窗体（登录、选服、选角、创角、HUD、背包、装备、技能、加点、快捷键、组队、行会、交易、仓库、商店、修理、NPC、模态框、观人、小地图、大地图、死亡灰屏、Tooltip、二级密码），必须与 `Prguse.wil` 贴图帧号和 IDA 交互事件 1:1 像素级对齐；
     - **底层规则维度**：官方 9 装备槽位、7 大特戒判定、4 大套装效果、武器升级与开刀机制、PK 红黄名与卫士惩罚、药水回复曲线。

3. **拒绝代理指标，落实实质终局**：
   - 严禁用“单元测试跑通”、“写了多少千行代码”、“部分界面能点开”作为还原度结论。
   - 所有开发任务必须直接对应 `docs/AUDIT.md` 中的“缺口清单 (Gap Backlog)”，每修复或实现一项，必须更新审计账本的状态迁移（`[未实现] -> [骨架] -> [对齐中] -> [1:1 实证完工]`）。

## 5. IDA MCP 全量逆向穿透与 1:1 查漏补缺协议 (IDA MCP Ground Truth Verification Protocol)
1. **反凭空假设与反 OpenCV 伪对齐铁律 (Anti-OpenCV/Heuristic-Alignment Axiom)**：
   - **严禁任何凭直觉、“私服经验”或现代游戏逻辑编写代码**；
   - **严禁使用 OpenCV、PIL 像素扫描、图像边缘检测、聚类等任何启发式手段来“猜测/倒推” UI 控件坐标与尺寸**！此类方法属于虚假代理指标，严禁用于工程落地；
   - 所有 24 个原生 UI 窗体、面板底板、按钮、输入框、装备槽位、图标的坐标与尺寸，**必须 100% 通过 IDA Pro MCP 从官方二进制 (`MIR_final_v3_p.exe`) 中的 Delphi 原生指令（如 `sub_42718C/SetLeft`, `sub_4271B0/SetTop`, `sub_4271D8/SetWidth`, `sub_4271FC/SetHeight` 以及绘制调用 `sub_45351C/Draw` 的操作数）直接提取反编译证据**；
   - 任何没有 IDA 反编译物理地址（如 `sub_481dd4`, `sub_488AF4`）作为依据的坐标，一律视为非法硬编码，零容忍清除。
2. **全量 UI 封闭全覆盖协议 (Full 24-UI Ground Truth Coverage Protocol)**：
   - 严禁“只搞一个界面就停滞”的局部幸存者偏差自嗨！
   - 必须对盛大官方 1.5 全生命周期 24 个原生窗体（登录主框、新用户注册、修改密码、选服石碑、选角神龛、创角界面、HUD 底栏、血球魔球、快捷按键、背包面板、装备面板、状态面板、技能面板、加点面板、快捷键设置、组队面板、行会面板、交易面板、仓库面板、商店买卖、装备修理、NPC 对话框、系统模态框、观人面板、小地图雷达、大地图、死亡复活）**全量进行 IDA 物理地址与原生绝对坐标提取，逐个建立实证台账并完成 1:1 代码重构与回归验证**。
3. **闭环真机验证**：
   - 每一处查漏补缺的改动，必须经过“IDA 证据定位 -> 代码精准重构 -> 自动化单元测试 -> OpenGL3 真实渲染捕获 -> 机器视觉审计”的五步闭环验证。

## 6. 根治猜坐标与 UI/HUD 物理真值锁死宪法 (Anti-Guessing Physical Ground Truth Axiom)
1. **坐标来源双轨锁死（杜绝一切人肉猜坐标与试错微调）**：
   - **静态控件（Label/Button/Panel/Edit）**：100% 必须来源于官方 PE 资源段提取的明文 DFM 对象树（`scratch/official_dfm_tree.json`），禁止任何人肉猜测或启发式微调！
   - **动态绘制（Draw/DirectPaint）**：100% 必须来源于 IDA 反编译汇编操作数（如 `sub_48B2A0`、`sub_45ec70`）与官方 WIL 切片 8 字节 FrameHeader 的 `(OriginX, OriginY)`，任何脱离物理真值的硬编码坐标零容忍清除。
2. **HUD 底栏与血球魔球四大不可动摇物理铁律**：
   - **战士单血球铁律 (IDA 0x48b4b4)**：战士等级 < 28 级时（未学半月弯刀），底槽必须为 `Prguse_0005`（96x92 @ 38, 439），血液填充必须为 `Prguse_0006`（96x92 @ 38, 439 纯红满血单球），严禁使用双球贴图遮罩！只有达到 28 级或法师道士才切为 `Prguse_0004` 红蓝双球！
   - **艺术字贴图数字铁律 (IDA 0x45ec70 & 0x48b749)**：底栏等级数字（Lv）严禁使用任何矢量字体或系统 GDI 字体，必须调用 `Prguse.wil` 切片 30~39（`Prguse_0030..0039`）艺术贴图字，绝对起始坐标固定为 `(660, 496)`，步长 8px！
   - **纯净刻度槽铁律 (IDA 0x48b838 & 0x48b8c8)**：经验条 `(666, 527)` 与负重条 `(666, 560)` 刻度槽内严禁常显“0.00%”或“0/15”等粗体文字标签！原版只渲染贴图 `Prguse_0007`，数值仅在鼠标 Hover 时浮动提示！
   - **严禁私服调试文本残留**：严禁在游戏聊天区注入任何“单轨化沙盒引擎启动”、“已自动为您装配...”等非官方现代私服调试文本！
3. **防退化测试熔断机制**：
   - 任何涉及 UI 坐标或渲染修改，必须在 `tests/CSharp/TestOfficialUiGroundTruth.cs` 中补充断言，并确保 100% 通过！一旦发生坐标偏差，CI 立即红牌熔断！




<!-- aoci:begin -->
## AOCI 仓库认知

AOCI 为本仓库维护一个稳定、可版本化、可增量更新的仓库级认知层，供模型跨任务复用对系统的理解。

`aoci.txt` 是面向模型的结构化认知索引。它以每个受管理文件、数据库表或其他受管理对象一条独立 Entry 的方式，用符号标签与 F/R/A/S 语义表达对象的核心职责、重要关系、对外契约，以及理解或修改系统时必须知道的非显然约束和设计决策。

Header、目录段和全部 Entry 共同组成完整仓库索引，可以覆盖前端、后端、配置、数据库结构及其他受管理内容。受管理内容发生变化时，通常只需维护受影响的认知条目，不需要重新生成整个索引。

AOCI 提供系统架构、对象职责、重要关系、对外契约和关键约束的高密度视图。

### 工作原理

AOCI 采用“模型生成、模型读取”的认知闭环。

Header、Entry 和 Curation 语义的创作只按当前机器签发的 Plan 与实时 Guide 执行；由 Host 模型基于当前绑定证据独立完成。

Entry 的语义必须来自模型对真实证据的理解。不得仅依据路径、文件名、扩展名、AST、符号列表、依赖扫描、正则、固定模板或规则引擎推导、预填、拼接或改写索引语义。

对 Fresh Bootstrap，只按当前机器签发的 Plan 和实时 Guide 执行。当它们要求创作时，Host 模型创作 Root、Meta、Tag 和 F/R/A/S，提供 authoring-run 声明，并把它绑定到 Plan、Evidence 与完整 Candidate。不得要求 AOCI 填写 `origin=host_model`、制造 Receipt 或把程序生成的 Framework 当作语义。本文件不自行重建 Onboarding 流程。内部批次不是用户决策；只有遇到既有批准边界或真实的安全、漂移、CAS、Recovery 条件才停止。

### 最小使用入口

- `aoci_rules`：取得当前AOCI版本的会话运行合同。
- `aoci_overview`：建立或恢复本仓库的完整认知。
- `aoci_maintain`：受管理对象达到最终稳定状态后检查认知是否需要维护。
- `aoci_update_entry`：提交与当前证据和源码摘要绑定的完整语义更新批次。
- `aoci_report`：仅当当前布局和工具状态支持时，在证据不足、无法可靠生成语义时登记待办，不猜写。

其他MCP工具、CLI命令、参数和专项流程，以当前工具说明、Guide和 `--help` 返回内容为准，不在本文件中重复完整手册。

本区块只规定仓库接入、认知使用和收尾原则。`aoci_rules` 承载当前会话合同，Guide实时输出承载当前Plan的执行顺序与停点，工具Schema、Spec和Validator承载机器结构与判据；Prompt、Description、README和静态文档不能覆盖这些机器事实。

### 建立、生成和恢复认知

1. 每个新的 Agent Run 开始时，应先判断：

   - 本仓库是否已经存在可用的完整AOCI索引；
   - 当前上下文中是否已有与本仓库根、当前索引版本和当前AOCI服务相匹配，并且模型仍可可靠使用的完整仓库认知。

2. 仓库已经存在可用的完整索引，但当前Run没有可靠完整认知时，先调用 `aoci_rules`，再调用 `aoci_overview`。

   完整认知仍可靠时直接复用。局部不确定本身不要求机械重读系统全貌。

   本Run从已知Host上下文压缩恢复时（包括宿主注入的压缩摘要），必须把此前模型认知视为不可靠。压缩handoff不得保留或摘要正式Whole-Index，也不得保留或摘要任何Overview Header、Entry、Chunk、Challenge或Attestation正文；只能保留安全续接所需的receipt身份、未完成write或Recovery状态，以及立即重载指令。复制进handoff的Whole-Index语义或receipt不能证明恢复后模型的当前认知可靠。若当前上下文已无法可靠保留运行合同，先调用 `aoci_rules`。继续业务任务前，使用 `refresh_reasons=["context_compaction"]` 和新的 `refresh_event_id` 调用普通完整Whole-Index `aoci_overview`（不设置 `check_only` 或设为false）；不得使用 `check_only` 或认知probe。原样跟随每个 `next_cursor` 直到 `completed=true`，确认交付，并且只基于新交付正文提交一次Attestation。完成这次新的完整传输后，即使Attestation为partial或fail也消费该generation，并按既有合同继续source-bound任务，不再自动调用第二次Overview。

   AOCI可以针对 `context_compaction`、项目 `cognition_refresh_threshold` 下的机器 `semantic_threshold` 或主要 `phase_transition` 提供checkpoint与认知状态事实。只需要这些紧凑事实时使用 `check_only=true`；这些事实只向Agent提供建议，不替模型决定是否需要系统全貌。

   Agent显式调用普通 `aoci_overview`（未设置 `check_only` 或为false）时，只要能形成一致的CognitionSet，AOCI必须完整交付请求scope。不得因为已有receipt、阈值未达到或没有待处理刷新原因而抑制正文。正式认知Dirty或Stale时仍交付正文，但必须标记不可靠。存在未决恢复或无法形成一致snapshot时失败关闭，不返回混合正文。

   普通Overview返回 `continuation_required=true` 时，必须原样提交 `next_cursor` 并自动继续到 `completed=true`。不得询问用户、开始业务任务或给出阶段性系统结论。Host截断、缺块、重复、乱序、cursor失败、Index变化或`chunk_tokens`变化时停止本次认知链。Attestation完成前不得用Memory、源码、Spec、`aoci.txt`、历史会话、scope、search或Entry读取修补或补充Whole-Index认知。Challenge ordinal是正式Entry序列中的1-based位置；Header内容、注释、空行、Section/Overview/Chunk Marker、Receipt与Metadata均不计数，Chunk Receipt ordinal使用同一序列。Attestation必须原样回绑本次Challenge发布的当前`index_sha256`、`entry_sequence_sha256`与`entry_count`；旧Index、旧Entry序列、旧数量或旧Attestation均无效。完整链结束后只正式提交一次既有模型认知Attestation；同一响应只允许一次不改变语义答案的JSON Schema或字段格式修正。对象、Tag或F不匹配即失败且认知吸收不确定，不得语义重试或旁路补答。首次认知失败时还不得执行Root/Meta、Migration、全局布局或其他未重新绑定的系统级决策。上下文压缩刷新若传输完整、认知身份不变、治理对齐且没有Recovery或第三方冲突，即使Attestation为partial或fail也消耗该refresh generation，并继续原任务，不再自动重读Overview。`system_mastery_percent`只自评系统框架——架构、职责、强关系、稳定外部契约以及高熵安全和维护约束——不表示完整实现或运行实况知识；机器索引覆盖率必须分开。默认只向用户输出由本次真实覆盖率、Challenge、块数、Token和掌握度生成的规定成功或失败一句话。Host截断时提示用户把 `overview_delivery.chunk_tokens` 设置为更小的合法值后重新开始，不得自动修改。

   加法认知等级必须与严格证明字段分开解释。`delivery_verified`表示已加载Index且Host交付已确认，但完整认知验证仍未完成；应表达为“已加载且交付已验证”，不得描述为“没有认知”或“没有理解系统”。`cognition_verified`要求Attestation通过（Challenge至少80%的ordinal完全正确且对象身份至多失手一处），`cognition_governed`还要求治理对齐。通用完整读取失败句只用于真实交付故障。

   当Overview响应包含可选`cognition-state/v2`投影时，必须分别解释各维度。其Level止于`model_cognition_usable`；`strict_attestation_verified`、`governance_aligned`与`current_system_cognition_reliable`都是独立状态，绝不参与该Level。ordinal、对象身份、Tag或核心F不匹配可以导致严格Attestation失败，而模型认知仍然可用；不得仅凭这种不匹配就宣称模型没有理解系统。只有`current_system_cognition_reliable=true`允许无保留地声称当前完整系统认知可靠。投影缺失时继续使用上述Legacy解释。

   普通的只读审计、分析、检查、不修改代码或不提交、不push，不自动等于严格零写入，也不改变上述认知有效性判断。Codex Memory和历史Skill只能辅助恢复经验、用户偏好与调查方向，不能替代与当前仓库根、索引摘要、AOCI服务身份和认知范围匹配的当前认知收据；项目AGENTS和当前AOCI身份在AOCI状态上优先于历史Memory。

   只有用户明确禁止Ledger、元数据、`.aoci`运行资产及任何文件写入时，才按严格零写入处理。若必要的认知建立与该边界冲突，必须报告冲突并请求用户裁决或建议使用隔离副本，不得静默以Memory替代当前仓库认知。

3. 仓库没有可用的完整索引，或当前只有最小骨架、Header不完整、Entries未完成、必要Curation尚未裁决时，如果需要建立正式完整AOCI索引，先取得 `aoci_rules`，然后进入当前AOCI Guide。由Guide依据仓库真实状态决定下一阶段并完成必要安全步骤。

   `aoci_maintain` 不替代索引建立流程。

   不在本文件中自行重建或硬编码完整索引生成状态机。

4. 在长程任务中，模型负责保留当前认知收据并正确使用刷新门禁：

   - Host报告上下文压缩或模型已知系统全貌丢失时，执行上述强制 `context_compaction` 重载规则；AOCI不能自行推断Host事件；
   - 进入真正的主要阶段时声明 `phase_transition`，不得把函数、测试运行或小步骤当作阶段；
   - 在有用的稳定检查点通过 `check_only=true` 取得机器语义计数；
   - 除已知压缩的强制重载外，由Agent判断当前任务是否需要再次显式获取指定scope或完整Overview；
   - 在维护和对齐完成前，保留AOCI报告的Dirty或Stale可靠性状态。

### 任务收尾与认知维护

5. 纯只读问答、分析、版本核验，或没有产生受AOCI管理对象变化的任务，不需要调用维护工具。当前AOCI版本是任意`aoci_overview` check_only或`aoci_maintain`响应里的`cognition_receipt.mcp_service_version`；二进制路径是项目`.mcp.json`里的`command`，CLI不必在PATH上。

6. 发生受AOCI管理对象变化时，待其达到本次任务的最终稳定状态后，只调用一次 `aoci_maintain`。不要在每次中间修改后逐文件维护。

7. 若维护结果返回真实语义候选，Host 模型必须基于每个候选绑定的对象和必要证据，独立创作完整标签与F/R/A/S更新。通过 `aoci_update_entry` 一次提交当前机器签发批次的完整候选集合，同时原样保留每项 `source_sha256`、`candidate_id` 与对应domain批次身份。`max_entries`只限制单次请求和原子事务，不限制logical plan、Whole-Index或Managed Scope。`remaining`非零时，在当前批次成功Apply后重新调用Maintain并从新preimage继续；绝不能为满足transport上限缩减Index覆盖或自行截取返回批次。

   没有足够证据且当前布局支持 `aoci_report` 时，使用它而不猜测、套用模板或为消除待办而生成缺乏证据的认知。

8. 必须遵守工具返回的结构化状态和安全边界：

   - `repair_required`：只修复明确命中的候选，再重新提交当前机器签发的完整批次；
   - `stopped`：结束当前写入尝试并检查 `failed_step`、错误、正式写入证据与Recovery。auto模式下，已证明零写入则记录closure并重新Plan；完整Intent和可证明postimage则Resume；策略要求Rollback且preimage可证明则精确恢复后重新Plan。只有证据不足、第三方正式字节冲突、需要审批或外部动作，或命中其他真实安全边界时，才停止整个用户任务；
   - 冲突、审批、人工裁决、权限和安全信号不得忽略；
   - 已经对齐后不得重复维护或重复写入；`refresh_ready_for_overview` 是checkpoint事实，由Agent决定是否为下一阶段请求普通完整Overview。

   维护完成后如果又修改了任何受管理对象，之前的维护结果失效，应在新的最终稳定状态重新完成收尾。

9. 用户只限制业务文件范围，但没有明确禁止仓库托管资产时，AOCI托管资产可以在收尾阶段为保持认知一致而更新，并应在审计和提交中与业务文件区分。

   用户明确禁止修改 `aoci.txt`、`.aoci`、元数据或任何额外文件时，以用户限制为准，不得写入，并如实报告剩余不一致。

### 专项流程

初始化、完整索引生成、Header生成、Entries生成、数据库结构索引、Curation、人工评审和故障恢复，只按当前AOCI Guide或工具在对应阶段返回的指令、命令和安全停点执行。

不预加载、不猜测，也不自行重建这些专项流程。平台调用方式、请求格式、批次上限、审批规则、索引格式细节和恢复步骤由对应Guide、工具说明、模型Prompt和CLI帮助按需提供。
<!-- aoci:end -->
