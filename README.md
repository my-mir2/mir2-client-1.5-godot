# 热血传奇 1.5 客户端与服务端重构项目 (Mir2 1.5 Godot 4 & C#)

本项目为盛大官方原版热血传奇 1.5（富贵兽/虎卫传说版本）的现代化复刻与重构工程。采用 **Godot 4 (.NET / C#)** 重写客户端，服务端采用成熟高并发 C# 原生核心，1:1 像素级复刻官方 1.5 Delphi 原版 (`MIR_final_v3_p.exe`) 的表现层与核心逻辑。

本项目**完全自包含官方真值资产与元数据**，在云端环境（如 **Google Jules**、GitHub Codespaces、CI/CD 自动化构建）中**零外部 IDA MCP 依赖**，可直接拉取代码、独立编译构建、运行机器人自动化套件并进行云端端到端联调测试。

---

## 核心特性与架构 (Architecture Highlights)

1. **官方 1.5 唯一物理真值保证 (Ground Truth Axiom)**
   - 全量 24 个原生 UI 窗体控件坐标与尺寸 100% 来源于官方 PE 资源段提取的明文 DFM 对象树（`scratch/official_dfm_tree.json`）与 IDA Delphi 原生指令反编译。
   - 彻底封死“猜测坐标”与“非标私服魔改”，所有协议与表现层细节均有代码与实证台账支撑。

2. **自包含官方数据与元数据 (Self-Contained & Zero-External-IDA-MCP)**
   - 官方 128+ 个 CM/SM 协议真值表：[`docs/ida_evidence/SM_truth_table.md`](docs/ida_evidence/SM_truth_table.md)
   - 官方对话框与交互分发表：[`docs/ida_evidence/dialog_truth_table.md`](docs/ida_evidence/dialog_truth_table.md)
   - 官方怪物属性与掉落全集：[`src/Mir2.Shared/Data/OfficialMonsterStats.g.cs`](src/Mir2.Shared/Data/OfficialMonsterStats.g.cs) 与 [`OfficialMonsterDBRegistry.cs`](src/Mir2.Shared/Data/OfficialMonsterDBRegistry.cs)
   - 官方物品与切片真值：[`src/Mir2.Shared/Data/OfficialStdItemsRegistry.cs`](src/Mir2.Shared/Data/OfficialStdItemsRegistry.cs)
   - 全量还原审计大账本：[`docs/AUDIT.md`](docs/AUDIT.md)

3. **双轨高并发服务端架构 (Mir2.Server)**
   - 盛大官方 1.4/1.5 Delphi 文本协议网关与现代化高性能消特征二进制协议网关并存；
   - 严格的服务端权威战斗、防加速外挂、移动校验、穿脱装备与背包负重体系；
   - 完整的传送门（Passage/Gate）、生物尸体割肉（Butch）、药包解包与全量药品消耗系统。

4. **端到端自动化验证套件 (Mir2.Bot)**
   - 包含真实的无头玩家机器人测试套件（`tools/Mir2.Bot`），直接通过网络协议连接服务端，支持 18 大核心场景并发验证（移动、打怪、掉落争抢、穿脱装备、买卖修理、死亡复活等）。

---

## 快速构建与测试 (Build & Test)

### 1. 编译构建
```bash
dotnet build Mir2Client.sln
```

### 2. 运行单元与集成测试
```bash
dotnet test tests/CSharp/Mir2Client.Tests.csproj --no-build
```

### 3. 运行无头机器人全量测试套件 (需先启动服务端)
```bash
python tools/run_bot_suite.py
```

---

## 目录结构说明

- `src/`：客户端表现层与渲染层源码（Godot 4 C#）
- `src/Mir2.Server/`：高性能 C# 原生服务端与逻辑引擎
- `src/Mir2.Shared/`：双端共享协议定义、状态机、战斗计算公式与官方数据库注册表
- `docs/`：官方 1.5 还原度大账本（`AUDIT.md`）及 IDA 物理真值证据库（`docs/ida_evidence/`）
- `scratch/`：包含官方 DFM 对象树（`official_dfm_tree.json`）与反编译证据归档
- `tools/`：无头自动化测试机器人（`Mir2.Bot`）与数据转换导入工具
- `AGENTS.md`：面向 AI 协同编码智能体（如 Google Jules、Antigravity 等）的核心规范与工程公理
