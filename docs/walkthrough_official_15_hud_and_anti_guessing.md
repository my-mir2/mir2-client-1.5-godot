# 盛大官方 1.50 (虎卫传说) HUD 底栏与物理防猜坐标技术走查与复盘 (Walkthrough)

## 一、 为什么之前会反复出现“改这里崩那里”与“猜坐标”现象？

大模型或开发者在面对历史遗留游戏复刻时，天然容易产生**“经验拟合”与“启发式试错”的顺从性倾向**：
1. **经验偏差与现代私服污染**：以为“任何职业都有红球和蓝球”，于是不管职业和等级直接加载红蓝双球，看到战士没有蓝，就自作聪明地用 Shader 把右侧蓝球裁切掉一半，制造出怪异的“黑色空槽残缺双球”；
2. **凭感觉肉眼调坐标**：在没有拿到官方 Delphi 原版绝对真值的情况下，凭截图一个像素一个像素地“人肉试错微调”（如把等级设为 642, 492 居中，把经验负重强行加了白色文字 Label），既脱离了官方原版设计，又导致字体发虚发白、界面混乱；
3. **缺少物理硬锁门禁**：没有把官方二进制里的物理常数直接转化为自动化断言测试，导致每次修改其他模块时，容易误触改动既有 UI。

---

## 二、 官方 1.5 绝对物理真值的终局挖掘与实证

本次复盘彻底穿透了盛大官方 1.5 原版二进制 `MIR_final_v3_p.exe`，从两套底层数据中锁死了不可篡改的真值：

### 1. 静态控件物理真值：PE 资源段 Delphi TPF0 (DFM) 100% 字节无损提取
- 通过纯 Python 解析器 [`scratch/parse_dfm.py`](file:///d:/vbcoding/mir2/mir2-client1.5-delphi/mir2-client1.5-delphi/scratch/parse_dfm.py)，将官方 `TFRMDLG` 与 `TFRMMAIN` 的全部窗体属性 100% 提取并导出为 [`scratch/official_dfm_tree.json`](file:///d:/vbcoding/mir2/mir2-client1.5-delphi/mir2-client1.5-delphi/scratch/official_dfm_tree.json)；
- 官方 24 个原生界面的全部静态控件尺寸（Left, Top, Width, Height, Font）全部有案可查，严禁任何人肉臆测。

### 2. HUD 动态绘制物理真值：IDA `sub_48B2A0` 与 `0x45ec70` 汇编硬常数
- **战士单血球铁律 (IDA 0x48b4b4)**：
  ```assembly
  0048b4b4: cmp byte ptr [eax + 0x12], 0  ; Job == 0 (Warrior 战士)
  0048b4c5: cmp byte ptr [eax + 0x30], 0x1c ; Level < 28 (未学半月弯刀)
  jae 0x48b5d8                            ; 达到 28 级或法师道士进入双球
  ; 战士 < 28:
  0048b4dc: mov edx, 5  ; Prguse_0005 (96x92 单球空槽暗底 @ 38, 439)
  0048b538: mov edx, 6  ; Prguse_0006 (96x92 纯红满血单球 @ 38, 439)
  ```
  **结论**：战士在 28 级之前，底栏是一颗**巨大的、完整的纯红单血球**，完全没有中间分割线，也没有右侧的蓝色魔球。
- **等级贴图数字铁律 (IDA 0x45ec70 & 0x48b749)**：
  - 官方绘制等级数字时，在 `0048b749` 传入基准坐标 `(X: 660, Y: 496)`；
  - 函数 `0x45ec70` 读取字符，每个数字字符映射到 `Prguse.wil` 里的切片 `30 + (char - '0')`（`Prguse_0030..0039` 艺术贴图字），每个数字宽 8 像素；
  - 官方底栏**绝无任何矢量字体 Label**，全是用切片贴图拼装出来的艺术字。
- **纯净刻度条铁律 (IDA 0x48b838 & 0x48b8c8)**：
  - 经验进度条 `Prguse_0007` 位于 `(666, 527)`；
  - 负重进度条 `Prguse_0007` 位于 `(666, 560)`；
  - 官方底盘刻度槽内**绝无常显的“0.00%”或“0/15”粗体文字**，原版保持纯净金属质感，数据仅在鼠标悬停时 Tooltip 浮动呈现。

---

## 三、 防退化与防遗忘长效工程机制

为确保任何后续会话或开发者绝不会再次遗忘上述真值，本项目建立了以下三重防线：

1. **项目规则最高法典锁死**：
   - 已将《根治猜坐标与 UI/HUD 物理真值锁死宪法》固化进 [`AGENTS.md`](file:///d:/vbcoding/mir2/mir2-client1.5-delphi/mir2-client1.5-delphi/AGENTS.md) 与 [`GEMINI.md`](file:///d:/vbcoding/mir2/mir2-client1.5-delphi/mir2-client1.5-delphi/GEMINI.md) 第 6 节，全局常驻，每次启动强制生效。
2. **自动化单元测试硬熔断门禁**：
   - 在 [`tests/CSharp/TestOfficialUiGroundTruth.cs`](file:///d:/vbcoding/mir2/mir2-client1.5-delphi/mir2-client1.5-delphi/tests/CSharp/TestOfficialUiGroundTruth.cs) 中新增了 `TestHudBottomBar_OfficialGroundTruth` 物理断言测试，将 DBottom、等级贴图坐标、经验负重坐标、单双球模式全部断言锁死，一旦发生偏差 CI 立即红牌阻断。
3. **真实端到端实机机器视觉核销**：
   - 任何改动必须通过 `AutoLiveScreenshotRunner` 进行实机 OpenGL3 帧捕获与审查，杜绝脱机虚假单测。
