# M2Server 全量点名册（25 个 M2 侧声明 vs M2Server.exe 6.4MB 实测）
# 方法：新开 idat 全分析（auto_wait）+ ida_m2_25.json / ida_m2_deep.json

## 结论先行
- 3/25 命中相邻函数（声明地址为函数内偏移，差 +0x14/+0x24/+0x1C）：
  - 0x4BD4A0 → sub_4BD48C（PlayerAuditEngine 审计）
  - 0x4BDD20 → sub_4BDCFC（SkillFormulas 魔法盾）
  - 0x4CBCEC → sub_4CBCD0（SkillFormulas 技能公式）
- 22/25 在此 M2Server.exe（6.4MB，.text 0x401000-0x940000）中无函数；
  0x4C1EF4/0x4C7720/0x4C03B0/0x4BE3FC 处字节为数据（RTTI/字符串如 Compare/Index），非代码。
- 判定：C# 注释里的 0x4B-0x4D 地址来自另一 M2 构建版本，与此 exe 的 RVA 不映射；
  sub_4C1EF4_code.c（麻痹戒指伪代码）同样来自他版，仅作语义参考，禁作字节真源。

| M2地址 | 我方引擎 | M2实证（此exe） | 等级 |
|---|---|---|---|
| 0x4B2C38 | DropGeometryEngine | 无函数 | L1 |
| 0x4BA43C | PlayerAuditEngine | 无函数 | L1 |
| 0x4BB63C | PlayerAuditEngine | 无函数 | L1 |
| 0x4BB6C8 | PlayerAuditEngine | 无函数 | L1 |
| 0x4BD4A0 | PlayerAuditEngine | 内偏移 sub_4BD48C+0x14 | L2 |
| 0x4BDD20 | SkillFormulas | 内偏移 sub_4BDCFC+0x24 | L2 |
| 0x4BDEB8 | TaoistCombatEngine | 无函数 | L1 |
| 0x4BE3FC | SpecialRings | 无函数（该处为数据） | L1 |
| 0x4BF0A0 | PkRuleEngine | 无函数 | L1 |
| 0x4BF180 | PkRuleEngine | 无函数 | L1 |
| 0x4BF7DC | PlayerAuditEngine | 无函数 | L1 |
| 0x4C01B8 | SpecialRings | 无函数 | L1 |
| 0x4C03B0 | SpecialRings | 无函数（该处为 Compare 字符串数据） | L1 |
| 0x4C1EF4 | SpecialRings | 无函数（该处为数据；伪代码他版，仅语义参考） | L1 |
| 0x4C2854 | WeaponUpgradeEngine | 无函数 | L1 |
| 0x4C3130 | PlayerAuditEngine | 无函数 | L1 |
| 0x4C50E9 | WeightConstraintEngine | 无函数 | L1 |
| 0x4C5114 | WeightConstraintEngine | 无函数 | L1 |
| 0x4C5238 | DropGeometryEngine | 无函数 | L1 |
| 0x4C6238 | PlayerAuditEngine | 无函数 | L1 |
| 0x4C6E4C | SkillFormulas | 无函数 | L1 |
| 0x4C7720 | SpecialRings/PlayerAudit/Taoist | 无函数（该处为 Index 数据） | L1 |
| 0x4C93A0 | SabukCastleEngine | 无函数 | L1 |
| 0x4C9B80 | SabukCastleEngine | 无函数 | L1 |
| 0x4CBCEC | SkillFormulas | 内偏移 sub_4CBCD0+0x1C | L2 |
## Rust（mir2-my）对照结论（2026-10-06 首轮）

| # | M2语义 | Rust实现 | 与C#对比 | 处置 |
|---|---|---|---|---|
| 1 | 交易金币上限 sub_4DDC8C 0x989680=10M | trade.rs MAX_GOLD_CAP=10M（引M2同值） | C#曾误标5M | C#已改10M，测试锁定 |
| 2 | 交易锁定/破锁 | trade.rs：改动即双解锁+系统消息；锁定20tick防连点 | TradePanelView.OnConfirm 1000ms窗口已镜像（服务端权威） | 已闭环 |
| 3 | 复活戒指 30s/1000持久 | special_items can_revive 600tick=30s | C# 30000ms一致 | 一致 |
| 4 | 麻痹 Random(AP+5)==0 | paralysis_hit 同式 | C# 1/(recover+5)一致 | 一致 |
| 5 | 魔法盾 (dmg/100)*(lv+2)*8 | apply_magic_bubble_reduction 同式 | C#同式 | 一致 |
| 6 | 特戒Shape 111/112/119/139 | Rust按Shape/AniCount双路径，另有153/156-159复合戒指 | C#缺153/156-159复合 | L2，复合戒指待补 |
| 7 | 交易opcode | bridge：CM 1025-1030 internal；SM 673-678/682-688；无672 | C# CM60-65经网关翻译；672已删；678已接 | 闭环 |
| 8 | 行会opcode | CM_GUILDHOME=1035等；SM 753/756/757-771 | C# 1035-1042 vs Rust 1035/1037-1045部分错位 | 待联调逐项 |

| 9 | 行会 CM 全族 | bridge 1035/1037-1041/1044-1045；宣战无CM入口走@guildwar；封号recog=职位id | C#已按bridge重映射，测试锁定 | 已闭环 |

| 10 | 行会 SM 全族 | 753开板/756公告/757-760人事/761封号败/762-763建会/767公告果/769/771外交败/671名单4段式 | 路由+名单解析器已对齐 | 已闭环 |

| 11 | 移动战斗 CM | 走3011/跑3013/转向3010/普攻3014/技能3017-3025与bridge一致；普攻recog=目标UID（旧坐标打包废）；扔金1002→1016 | C#已改，测试锁定 | 已闭环 |

| 15 | 红名/升武 | 衰减120s→50s/点；黑铁因子改ROUND(n+top5/5)；持久9-15/18+档改真分布 | C#已改，测试锁定 | 已闭环 |

| 12 | 商店组队仓库 CM | 买卖45/46、修理1023、查价1024/1012；组队1019-1022；仓库1031/1032；退组73废 | C#已改，测试锁定 | 已闭环 |
| 13 | 移动战斗技能背包 | 普攻recog=UID、扔金→1016、捡物extra坐标、穿脱名载荷；技能id表33个一致 | C#已改，测试锁定 | 已闭环 |
| 14 | 登录建角NPC | 查角→119、建角121/122、删角123/124（525撞STARTPLAY）；NPC选项信号接通61/1011 | C#已改，测试锁定 | 已闭环 |
| 16 | 边缘收包 | 飞斧34/破土36/潜地37/改名750/捐献764/765接入；764双义分流 | C#已改 | 已闭环 |
| 17 | 战斗公式 | 烈火1.4x/CD10s/蓄力20s；刺杀分级缩放；野蛮概率距离自伤；毒持续跳伤；法师7式；红名50s/点；升武因子持久档 | C#已改，测试锁定 | 已闭环 |
| 18 | 数值表 | ExpTable经典0-50级表新增；负重C#经典式与Rust简化式分歧挂起 | 联调以经典为准 | 待联调 |
| 19 | 特戒正名 | 麻痹分母AntiPoison正名；护身/复活/吸血一致 | C#已改，测试锁定 | 已闭环 |

| 20 | 交易流程 | 发包60-65全无arm→1025-1030；收包673-688路由已对；删ProtocolConsts 7个错位重复SM | C#已改，测试锁定 | 已闭环 |
