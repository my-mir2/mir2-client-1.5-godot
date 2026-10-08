# 热血传奇 1.5 客户端两套工程全景细节对比与决策裁定书

> **文档目的**：对同级两套客户端工程 —— mir2-client1.5-delphi-custom（原生 Delphi 10.4 编译工程）与 mir2-client1.5-delphi（现代 Godot 4.4 + .NET 8/10 C# 工业级复刻工程）进行全维度、全细节对比审计，提炼核心差异与技术得失，由用户亲自敲定最终设计细节与技术路线基准。
> **文档位置**：mir2-client1.5-delphi/docs/PROJECT_COMPARISON_AND_DECISION.md
> **质检立场**：严守坏账优先律，真实终局压倒代理指标，禁绝自嗨粉饰。

---

## 零、工程定位与本质差异总览

| 比较维度 | mir2-client1.5-delphi-custom (当前工程) | mir2-client1.5-delphi (同级工程) |
| :--- | :--- | :--- |
| **工程本质** | **原生 Delphi Pascal 传统工程** (源自经典盛大 1.76 客户端源码) | **现代游戏引擎复刻工程** (Godot 4.4 Mono + C# .NET 8/10 单轨) |
| **源码形态** | 93 个 .pas / .dfm / .dpr 文件，总代码量约 12 万行 Pascal | 183 个 .cs 文件 + .tscn 场景树，C# 单轨化工业级架构 |
| **编译工具链** | Embarcadero Delphi 10.4 (DCC32.EXE)，32位 Windows 原生程序 | dotnet build (.NET SDK 8/10) + Godot 4.4 导出，支持跨平台/单文件 Native AOT |
| **图形渲染底层** | 原生 DirectDraw 7 软件/硬件表面 + 自研 GLViewport (OpenGL 纹理桥接) | Godot 4 内置渲染管线 (Vulkan / OpenGL3)，自研 16-bit Dither 复古 Shader |
| **资源读取机制** | 原生直接解包官方 Data/*.wil / *.wix 二进制封包 (Pascal WIL 引擎) | 导出提取为 PNG 离线资产，由 WilLoader.cs 与 Godot ImageTexture 高速缓存 |
| **自动化测试能力** | 依赖物理桌面手动启动 mir2.exe，依靠 startup_debug.log 文本追查 | 110 项全自动 xUnit 单元测试 (dotnet test 毫秒级跑通) + 48 步无头 E2E 机器渲染视觉审计 |

---

## 一、生命周期与架构设计细节对比

### 1. 窗口视口与分辨率缩放策略
- **mir2-client1.5-delphi-custom (Delphi 原生)**：
  - **设计方案**：严格锁定 800×600 逻辑窗口。移除最大化按钮（BorderIcons = [biSystemMenu, biMinimize]），拦截 SC_MAXIMIZE。
  - **优点**：100% 像素级对齐盛大 2001 年物理交互手感，输入框（TEdit）等原生 Windows 句柄控件不会因拉伸导致错位。
  - **缺点**：现代 2K/4K 高分屏下窗口较小，若强行全屏拉伸必须同步处理 VCL 原生输入框的矩阵变换。
- **mir2-client1.5-delphi (Godot C#)**：
  - **设计方案**：采用 Godot 4 内置 Viewport 机制，基准为 800×600，支持整数倍像素点对齐缩放（Pixel Perfect Integer Scaling）。
  - **优点**：高分屏拉伸时，场景树节点（包含文本输入框 LineEdit）自动等比放大、坐标完全居中，天然免疫错位问题。
  - **缺点**：双线性插值若未配置为 Nearest 模式，像素边缘可能出现轻微平滑模糊。

---

### 2. 网络协议与双端通信闭环
- **mir2-client1.5-delphi-custom (Delphi 原生)**：
  - **设计方案**：双轨兼容。g_UseNewProtocol 开关控制：
    - 旧协议：经典盛大 #1...! 帧格式，基于字符 Base64 编解码与 TDefaultMessage；
    - 新协议：NewProtoSend 适配层，发送基于 JSON 格式的字符串帧；
    - 网络组件采用 Windows TClientSocket (WinSock 异步事件泵)。
  - **现状痛点**：目前连接握手与场景切换逻辑较重，启动时若未配置服务端容易静默等待。
- **mir2-client1.5-delphi (Godot C#)**：
  - **设计方案**：强类型结构体化。全量实现官方 128+ 个 CM/SM 协议（PacketEncoder.cs / PacketDecoder.cs），基于 Base64+60 变长整型。
  - **优点**：100% 对齐官方原版二进制 IDA 逆向实证；拥有完整的 110 项自动化单测校验编解码可逆性。
  - **缺点**：若对接私服非标魔改 JSON 网关，需要另外编写转义胶水层。

---

### 3. UI 界面与交互自治规范
- **mir2-client1.5-delphi-custom (Delphi 原生)**：
  - **设计方案**：采用 Delphi 传统的 TDWinManager + TDWindow + TDButton 伪控件组件树，绘制直接 Blt 到主 DirectDraw Surface 上。
  - **优点**：绝对纯正的盛大官方 Pascal 源码资产，所有私服特有的微调逻辑完全保留。
  - **缺点**：界面控件与 Windows VCL 控件（如 TEdit）混用，生命周期与消息分发复杂，容易出现渲染层序与鼠标穿透问题。
- **mir2-client1.5-delphi (Godot C#)**：
  - **设计方案**：官方 24 个生命周期原生窗体全量提取自 IDA Pro 原生指令坐标（SetLeft, SetTop, SetWidth, SetHeight），基于纯 C# Control 节点驱动。
  - **关键机制**：实装了 **LIFO 窗口关闭栈 (WindowStack)**：
    - 按 ESC 优先关闭顶层打开的面板（如背包 -> 装备 -> 聊天）；
    - 空白处右键快速关窗；
    - 面板拖拽防抖；
    - 大世界点击穿透阻断（IsMouseOverUi 物理防穿透）。

---

### 4. 游戏战斗、技能与底层规则核心
- **mir2-client1.5-delphi-custom (Delphi 原生)**：
  - 技能逻辑分散在 ClMain.pas、magiceff.pas、Actor.pas 等数万行代码中，耦合度高，单步调试难度大。
- **mir2-client1.5-delphi (Godot C#)**：
  - 模块化解耦：
    - SkillFormulas.cs：官方 32 门技能数值公式与消耗前摇；
    - SpecialRings.cs：7 大特戒（麻痹、护身、复活、传送、隐身、超负载、火焰）与 4 大套装判定；
    - SabukCastleEngine.cs：沙巴克城门破碎阻挡消除与皇宫占领判定；
    - DeadReckoningEngine.cs：网络弱网航位推测与平滑阻尼插值。

---

## 二、核心决策裁定清单 (由用户拍板敲定)

以下为您需要权衡裁定的 5 个关键技术细节决策项：

### 决策项 1：客户端最终交付载体与技术路线
- **【用户最终裁定：选项 A 已拍板】选项 A (现代 Godot 4 C# 单轨 - 唯一主力交付基准)**：以 mir2-client1.5-delphi 为唯一主力交付，全面拥抱 Godot 4.4 Mono + .NET 10 跨平台、Native AOT 单文件发布与全套自动化测试，彻底摆脱 32 位 Delphi 老旧编译器束缚。
- ~~选项 B (原生 Delphi 守旧)~~：已否决。
- ~~选项 C (双轨互证)~~：已否决。Delphi 工程仅保留作为脱机静态历史代码参考，不再作为主力维护目标。

### 决策项 2：窗口显示模式与分辨率策略
- **选项 A (复古锁定 800×600)**：窗口保持固定大小，禁止随意缩放拉伸，杜绝任何坐标偏移与美术模糊。
- **选项 B (现代等比拉伸)**：统一采用 Viewport 整数倍拉伸（如 1600×1200 / 2400×1800），UI 输入框跟随视口矩阵缩放。

### 决策项 3：服务端协议对齐基准
- **选项 A (官方 1:1 二进制封包协议)**：双端统一采用官方 Base64+60 变长整型 CM/SM 协议体系（完全契合 IDA 实证）。
- **选项 B (现代 JSON 兼容协议)**：网络层采用 NewProtoSend 传递的 JSON 字符串协议，方便抓包与外部微服务解析。

### 决策项 4：窗口层叠与 ESC 关闭行为 (UI 交互手感)
- **选项 A (现代 LIFO 栈式关闭)**：按 ESC 键只关闭当前最上面一层面板（如背包、仓库、技能面板逐层退出）。
- **选项 B (Delphi 原生全清模式)**：按 ESC 或小退直接清空/关闭所有浮动窗口。

### 决策项 5：资源资产加载方式
- **选项 A (离线提取 PNG 资源缓存)**：提前将 WIL/WIX 导出为标准图像，加载极速，无解包崩溃风险。
- **选项 B (在线解析 WIL 原生文件)**：直接读取盛大官方 Data/*.wil 二进制包，减少客户端打包体积，原汁原味。
