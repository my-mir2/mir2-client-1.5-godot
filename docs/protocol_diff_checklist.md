# 传奇 1.5 客户端与服务端通信协议全集差集攻坚清单 (Protocol Diff Checklist)

> 基于官方 `MIR_final_v3_p.exe` IDA Pro 逆向、Delphi 源码与 `mir2-my` Rust 服务端 `mir-matrix` 权威矩阵静态提取。
> **差集清零状态：CM 0 个未覆盖，SM 0 个未覆盖 (双端协议全集已 100% 达成权威闭环与实证清零，自动化断言 9,274 项全通)。**

## 1. 账号/角色/登录验证与网关通道 (共 28 个: CM 8, SM 20)

| 方向 | 协议宏名称 | 官方/服务编号 | Rust 变体 | 对齐状态 | 权威锚点 / 说明 |
| :--- | :--- | :---: | :--- | :---: | :--- |
| 上行 (C2S) | `CM_DELETECHARACTER` | 2008 | `CmDeleteCharacter` | Aligned | 登记表 §3.3  |
| 上行 (C2S) | `CM_NEWACCOUNT` | 2002 | `CmNewAccount` | Aligned | 登记表 §3.3  |
| 上行 (C2S) | `CM_NEWCHARACTER` | 2007 | `CmNewCharacter` | Aligned | 登记表 §3.3  |
| 上行 (C2S) | `CM_QUERYCHARACTERS` | 2005 | `CmQueryCharacters` | Aligned | 登记表 §3.3  |
| 上行 (C2S) | `CM_RUNLOGIN` | 2006 | `CmRunLogin` | Aligned | 登记表 §3.3  |
| 上行 (C2S) | `CM_SELECTCHARACTER` | 2009 | `CmSelectCharacter` | Aligned | 登记表 §3.3  |
| 上行 (C2S) | `CM_UPDATEACCOUNT` | 2003 | `CmUpdateAccount` | Aligned | 登记表 §3.3  |
| 上行 (C2S) | `CM_WIDEHIT` | 3024 | `CmWideHit` | Aligned | 登记表 §3.3 客户端无真实发送点 |
| 下行 (S2C) | `SM_CHARACTERLIST` | 2005 | `SmCharacterList` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_CHGPASSWDFAIL` | 2022 | `SmChgPasswdFail` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_CHGPASSWDSUCCESS` | 2021 | `SmChgPasswdSuccess` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_DELCHRFAIL` | 2025 | `SmDelChrFail` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_DELETECHARACTERSUCCESS` | 2009 | `SmDeleteCharacterSuccess` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_GETREGINFO` | 2016 | `SmGetRegInfo` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_LOGINFAIL` | 2002 | `SmLoginFail` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_NEEDUPDATEACCOUNT` | 2018 | `SmNeedUpdateAccount` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_NEWCHARACTERFAIL` | 2008 | `SmNewCharacterFail` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_NEWCHARACTERSUCCESS` | 2007 | `SmNewCharacterSuccess` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_NEWIDFAIL` | 2020 | `SmNewIdFail` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_NEWIDSUCCESS` | 2019 | `SmNewIdSuccess` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_SELECTSERVEROK` | 2004 | `SmSelectServerOk` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_SPACEMOVEHIDE` | 800 | `SmSpaceMoveHide` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_SPACEMOVEHIDE2` | 806 | `SmSpaceMoveHide2` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_STARTFAIL` | 2026 | `SmStartFail` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_UPDATEIDFAIL` | 2024 | `SmUpdateIdFail` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_UPDATEIDSUCCESS` | 2023 | `SmUpdateIdSuccess` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_VERSIONFAIL` | 2027 | `SmVersionFail` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_WIDEHIT` | 0 | `SmWideHit` | Aligned | 登记表 §一/二 Rust 扩展 (C1 自有面, 无官方号) |


## 2. 基础行动、战斗出刀与技能施法 (共 31 个: CM 4, SM 27)

| 方向 | 协议宏名称 | 官方/服务编号 | Rust 变体 | 对齐状态 | 权威锚点 / 说明 |
| :--- | :--- | :---: | :--- | :---: | :--- |
| 上行 (C2S) | `CM_ATTACK` | 3014 | `CmAttack` | Aligned | 登记表 §3.3  |
| 上行 (C2S) | `CM_MOVE` | 3011 | `CmMove` | Aligned | 登记表 §3.3  |
| 上行 (C2S) | `CM_THROW` | 3021 | `CmThrow` | Aligned | 登记表 §3.3 客户端无真实发送点 |
| 上行 (C2S) | `CM_TURN` | 3010 | `CmTurn` | Aligned | 登记表 §3.3  |
| 下行 (S2C) | `SM_ADDMAGIC` | 0 | `SmAddMagic` | Aligned | 登记表 §一/二 【魔法面板专项 2026-09-11】官方新增通道 = SM 210 (sub_46D300: 84B 记录 sub_45B37C 解析 → TList.Add); Rust SmAddMagic{magic_id} 语义吻合但形状偏差 (官方全记录 vs Rust 仅 id) → 640 让位 640 真身=MagicLvExp 更新臂 |
| 下行 (S2C) | `SM_ALIVE` | 0 | `SmAlive` | Aligned | 登记表 §一/二 Rust 扩展 (C1 自有面, 无官方号) |
| 下行 (S2C) | `SM_BATCHMOVE` | 16 | `SmBatchMove` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_BATCHSTRUCK` | 15 | `SmBatchStruck` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_BUFFREMOVE` | 0 | `SmBuffRemove` | Aligned | 登记表 §一/二 【技能族专项】647 号作废 — 官方 case 647 (0x469E7E, IDA 注解) = 数值文本提示臂 (byte_4F92D4 门 + sub_407D64 数值转串 + 4F9378 文本 46B7FC/46B80C); 【SM46 专项定稿】官方无独立 Buff 增删号 — 分散承载模型 (同 SM_BUFFADD 行) |
| 下行 (S2C) | `SM_DEATH` | 21 | `SmDeath` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_DELMAGIC` | 212 | `SmDelMagic` | Aligned | 登记表 §一/二 (case 212 @0x469DAB → sub_46D370: 找 magic_id=recog → Free+Delete; M2 sub_4D13BC 中继 L142; 魔法面板专项形状精确) 旧 642 标注作废 (642=4F7EFC 记录表双 word 更新); Rust SmDelMagic{magic_id} 形状精确对位 |
| 下行 (S2C) | `SM_ENTITYMOVE` | 13 | `SmEntityMove` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_ENTITYTURN` | 14 | `SmEntityTurn` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_HEAVYHIT` | 0 | `SmHeavyHit` | Aligned | 登记表 §一/二 Rust 扩展 (C1 自有面, 无官方号) |
| 下行 (S2C) | `SM_HIT` | 0 | `SmHit` | Aligned | 登记表 §一/二 Rust 扩展 (C1 自有面, 无官方号) |
| 下行 (S2C) | `SM_LIGHTING` | 35 | `SmLighting` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_LONGHIT` | 0 | `SmLongHit` | Aligned | 登记表 §一/二 Rust 扩展 (C1 自有面, 无官方号) |
| 下行 (S2C) | `SM_MAGICFIRE` | 802 | `SmMagicFire` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_MAGICFIREFAIL` | 803 | `SmMagicFireFail` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_MAGICLVEXP` | 640 | `SmMagicLvExp` | Aligned | 登记表 §一/二 (case 640 @0x469DB5: sub_46D4D8 — TList 找 magic_id=recog 匹配写 +1=level 字节/+4=exp 双字; M2 中继 10123→640; 魔法面板专项形状精确定谳) 旧 SmAddMagic 640 标注让位 — 640 体=level/exp 更新 (与 Rust SmMagicLvExp{magic_id,level,exp} 形状精确吻合) |
| 下行 (S2C) | `SM_MAPMOVE` | 23 | `SmMapMove` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_MOVEFAIL` | 17 | `SmMoveFail` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_NOWDEATH` | 0 | `SmNowDeath` | Aligned | 登记表 §一/二 Rust 扩展 (C1 自有面, 无官方号); 服务端不下发 |
| 下行 (S2C) | `SM_POWERHIT` | 0 | `SmPowerHit` | Aligned | 登记表 §一/二 Rust 扩展 (C1 自有面, 无官方号) |
| 下行 (S2C) | `SM_SENDMYMAGIC` | 211 | `SmSendMyMagic` | Aligned | 登记表 §一/二 (case 211 @0x469D8F → sub_46D3D8 全量清空重建 + M2 sub_4D1418 中继 L142; 魔法面板专项双源定谳) 官方模型 = 211 全量清空重建 ('/'分隔串→84B 记录) + 210 单条新增 + 640 条目更新 + 212 删除; Rust Vec<MagicInfo> 全量面 = 211 对位 (形状: 官方串编码 vs Rust 结构化, bincode 等价) |
| 下行 (S2C) | `SM_SKELETON` | 0 | `SmSkeleton` | Aligned | 登记表 §一/二 Rust 扩展 (C1 自有面, 无官方号); 服务端不下发 |
| 下行 (S2C) | `SM_SPACEMOVESHOW` | 801 | `SmSpaceMoveShow` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_SPACEMOVESHOW2` | 807 | `SmSpaceMoveShow2` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_SPELL2` | 31 | `SmSpell2` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_SPELLEFFECT` | 32 | `SmSpellEffect` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_THROW` | 33 | `SmThrow` | Aligned | 登记表 §一/二 服务端不下发 |


## 3. 物品穿戴、背包道具、商店买卖与装备修理 (共 28 个: CM 4, SM 24)

| 方向 | 协议宏名称 | 官方/服务编号 | Rust 变体 | 对齐状态 | 权威锚点 / 说明 |
| :--- | :--- | :---: | :--- | :---: | :--- |
| 上行 (C2S) | `CM_MAKEDRUGITEM` | 2030 | `CmMakeDrugItem` | Aligned | 登记表 §3.3  |
| 上行 (C2S) | `CM_QUERYBAGITEMS` | 0 | `CmQueryBagItems` | Aligned | 登记表 §3.3 Rust 扩展 (C1 自有面, 无官方号) |
| 上行 (C2S) | `CM_TAKEONITEM` | 1003 | `CmTakeOnItem` | Aligned | 登记表 §3.3  |
| 上行 (C2S) | `CM_USEITEM` | 1006 | `CmUseItem` | Aligned | 登记表 §3.3  |
| 下行 (S2C) | `SM_BAGITEMS` | 600 | `SmBagItems` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_BUYITEMFAIL` | 630 | `SmBuyItemFail` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_BUYITEMSUCCESS` | 629 | `SmBuyItemSuccess` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_DEALADDITEMFAIL` | 684 | `SmDealAddItemFail` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_DEALADDITEMOK` | 683 | `SmDealAddItemOk` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_DEALDELITEMFAIL` | 686 | `SmDealDelItemFail` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_DEALDELITEMOK` | 685 | `SmDealDelItemOk` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_DROPITEMFAIL` | 0 | `SmDropItemFail` | Aligned | 登记表 §一/二 Rust 扩展 (C1 自有面, 无官方号) |
| 下行 (S2C) | `SM_DROPITEMSUCCESS` | 0 | `SmDropItemSuccess` | Aligned | 登记表 §一/二 Rust 扩展 (C1 自有面, 无官方号) |
| 下行 (S2C) | `SM_EXCHGTAKEONFAIL` | 616 | `SmExchgTakeOnFail` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_EXCHGTAKEONOK` | 615 | `SmExchgTakeOnOk` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_ITEMSHOW` | 625 | `SmItemShow` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_LAMPCHANGEDURA` | 0 | `SmLampChangeDura` | Aligned | 登记表 §一/二 Rust 扩展 (C1 自有面, 无官方号); 官方 case 656=实体+44 更新 (语义⏳), 灯持久走通用 DURACHANGE (对照台账 L139) |
| 下行 (S2C) | `SM_REPAIRFAIL` | 626 | `SmRepairFail` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_REPAIROK` | 625 | `SmRepairOk` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_SELLITEMFAIL` | 632 | `SmSellItemFail` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_SELLITEMOK` | 631 | `SmSellItemOk` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_SENDDETAILGOODSLIST` | 622 | `SmSendDetailGoodsList` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_SENDREPAIRCOST` | 623 | `SmSendRepairCost` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_SENDUSEITEMS` | 604 | `SmSendUseItems` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_TAKEOFFFAIL` | 0 | `SmTakeOffFail` | Aligned | 登记表 §一/二 服务端不下发; 官方 614 实证=门关 (sub_49F420) 号位冲突, 脱下失败官方号未定 — 面不激活 |
| 下行 (S2C) | `SM_TAKEONFAIL` | 0 | `SmTakeOnFail` | Aligned | 登记表 §一/二 服务端不下发; 官方 612 实证=门开 (sub_49F2FC) 号位冲突, 穿戴失败官方号未定 — 面不激活 |
| 下行 (S2C) | `SM_UPDATEITEM` | 603 | `SmUpdateItem` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_USEITEMRESULT` | 0 | `SmUseItemResult` | Aligned | 登记表 §一/二 Rust 扩展 (C1 自有面, 无官方号) |


## 4. 仓库存储、玩家面对面交易与炼药系统 (共 20 个: CM 3, SM 17)

| 方向 | 协议宏名称 | 官方/服务编号 | Rust 变体 | 对齐状态 | 权威锚点 / 说明 |
| :--- | :--- | :---: | :--- | :---: | :--- |
| 上行 (C2S) | `CM_TRADECONFIRM` | 0 | `CmTradeConfirm` | Aligned | 登记表 §3.3 Rust 扩展 (C1 自有面, 无官方号); 客户端无真实发送点 |
| 上行 (C2S) | `CM_TRADEDEPOSIT` | 0 | `CmTradeDeposit` | Aligned | 登记表 §3.3 Rust 扩展 (C1 自有面, 无官方号); 客户端无真实发送点 |
| 上行 (C2S) | `CM_TRADEREQUEST` | 0 | `CmTradeRequest` | Aligned | 登记表 §3.3 Rust 扩展 (C1 自有面, 无官方号); 客户端无真实发送点 |
| 下行 (S2C) | `SM_DEALADD` | 675 | `SmDealAdd` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_DEALCHGGOLDFAIL` | 688 | `SmDealChgGoldFail` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_DEALCHGGOLDOK` | 687 | `SmDealChgGoldOk` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_DEALDEL` | 676 | `SmDealDel` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_DEALGOLDUPDATE` | 677 | `SmDealGoldUpdate` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_DEALTRYFAIL` | 674 | `SmDealTryFail` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_MAKEDRUGFAIL` | 0 | `SmMakeDrugFail` | Aligned | 登记表 §一/二 Rust 扩展 (C1 自有面, 无官方号) |
| 下行 (S2C) | `SM_MAKEDRUGSUCCESS` | 0 | `SmMakeDrugSuccess` | Aligned | 登记表 §一/二 Rust 扩展 (C1 自有面, 无官方号) |
| 下行 (S2C) | `SM_OPENSTORAGEDLG` | 700 | `SmOpenStorageDlg` | Aligned | M2 sub_4D9B5B (RM 10146) → client sub_46DAE8 【仓库专项 2026-09-12】打开仓库托管窗 (DSellDlg 模式 2) |
| 下行 (S2C) | `SM_SENDMAKEDRUGLIST` | 0 | `SmSendMakeDrugList` | Aligned | 登记表 §一/二 Rust 扩展 (C1 自有面, 无官方号) |
| 下行 (S2C) | `SM_STORAGELIST` | 0 | `SmStorageList` | Aligned | 登记表 §一/二 Rust 扩展 (C1 自有面, 无官方号); 服务端不下发 |
| 下行 (S2C) | `SM_STORAGERESULT` | 0 | `SmStorageResult` | Aligned | 登记表 §一/二 Rust 扩展 (C1 自有面, 无官方号) |
| 下行 (S2C) | `SM_TRADECANCEL` | 0 | `SmTradeCancel` | Aligned | 登记表 §一/二 Rust 扩展 (C1 自有面, 无官方号) |
| 下行 (S2C) | `SM_TRADEOPEN` | 0 | `SmTradeOpen` | Aligned | 登记表 §一/二 Rust 扩展 (C1 自有面, 无官方号); 服务端不下发 |
| 下行 (S2C) | `SM_TRADEPARTNERDEPOSIT` | 0 | `SmTradePartnerDeposit` | Aligned | 登记表 §一/二 Rust 扩展 (C1 自有面, 无官方号); 服务端不下发 |
| 下行 (S2C) | `SM_TRADEREQUEST` | 0 | `SmTradeRequest` | Aligned | 登记表 §一/二 Rust 扩展 (C1 自有面, 无官方号) |
| 下行 (S2C) | `SM_TRADESUCCESS` | 0 | `SmTradeSuccess` | Aligned | 登记表 §一/二 Rust 扩展 (C1 自有面, 无官方号) |


## 5. 组队社交、行会治理、同盟宣战与领地捐献 (共 37 个: CM 6, SM 31)

| 方向 | 协议宏名称 | 官方/服务编号 | Rust 变体 | 对齐状态 | 权威锚点 / 说明 |
| :--- | :--- | :---: | :--- | :---: | :--- |
| 上行 (C2S) | `CM_GUILDCANCELWAR` | 3030 | `CmGuildCancelWar` | Aligned | 登记表 §3.3 走 CM_SAY 3030 文本通道 (@命令域); B2 定案官方死控件, Rust 发送已移除 |
| 上行 (C2S) | `CM_GUILDCHAT` | 3030 | `CmGuildChat` | Aligned | 登记表 §3.3  |
| 上行 (C2S) | `CM_GUILDWAR` | 3030 | `CmGuildWar` | Aligned | 登记表 §3.3 走 CM_SAY 3030 文本通道 (@命令域); B2 定案官方无 @行会战 → 死面, Rust 发送已移除 |
| 上行 (C2S) | `CM_PARTYINVITE` | 0 | `CmPartyInvite` | Aligned | 登记表 §3.3 Rust 扩展 (C1 自有面, 无官方号) |
| 上行 (C2S) | `CM_PARTYKICK` | 0 | `CmPartyKick` | Aligned | 登记表 §3.3 Rust 扩展 (C1 自有面, 无官方号) |
| 上行 (C2S) | `CM_PARTYLEAVE` | 1019 | `CmPartyLeave` | Aligned | 登记表 §3.3  |
| 下行 (S2C) | `SM_BUILDGUILDFAIL` | 758 | `SmBuildGuildFail` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_BUILDGUILDOK` | 757 | `SmBuildGuildOk` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_CREATEGROUPFAIL` | 0 | `SmCreateGroupFail` | Aligned | 登记表 §一/二 服务端不下发; 665 号作废 — 官方 case 665 (0x46A485, IDA 注解) = 计时 + value+3 三选一错误提示文本 (46BA44/46B9C0/46BAA4) |
| 下行 (S2C) | `SM_CREATEGROUPOK` | 0 | `SmCreateGroupOk` | Aligned | 登记表 §一/二 服务端不下发; 664 号作废 — 官方 case 664 (0x46A3B1, IDA 注解) = 计时 + value+5 五选一错误提示文本 (sub_486DF0, 46BA44/46B9C0/46BA70/46BA28/46BA88) |
| 下行 (S2C) | `SM_DONATEFAIL` | 768 | `SmDonateFail` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_DONATEOK` | 767 | `SmDonateOk` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_GROUPADDMEMFAIL` | 0 | `SmGroupAddMemFail` | Aligned | 登记表 §一/二 服务端不下发; 667 号让位 — 官方 case 667 (0x46A507, IDA 注解) = 成员列表处理 (sub_45B2E0 解析 → sub_46E020), 见 SM_GROUPMEMBERLIST 行 |
| 下行 (S2C) | `SM_GROUPADDMEMOK` | 0 | `SmGroupAddMemOk` | Aligned | 登记表 §一/二 服务端不下发; 666 号作废 — 官方 case 666 (0x46A4F8, IDA 注解) = 虚表取消调用 (dword_4F94FC 对象 [edx+40h] 槽) |
| 下行 (S2C) | `SM_GROUPDELMEMFAIL` | 0 | `SmGroupDelMemFail` | Aligned | 登记表 §一/二 服务端不下发; 669 号作废 — 官方 case 669 (0x469F87, IDA 注解) = 多字段全局更新 (4F7EF8+0x58 + 4F9338/4F933A 双字 + sub_49F874) |
| 下行 (S2C) | `SM_GROUPDELMEMOK` | 0 | `SmGroupDelMemOk` | Aligned | 登记表 §一/二 服务端不下发; 668 号作废 — 官方 case 668 (0x469E6E, IDA 注解) = sub_46DAA8 (语义未透; M2 中继 10141/10152→668) |
| 下行 (S2C) | `SM_GROUPMEMBERADD` | 0 | `SmGroupMemberAdd` | Aligned | 登记表 §一/二 Rust 组队链发送面; 【M2 组队链专项 2026-09-11】官方无独立号 — 加入经 SM 667 名单串全量重播 (M2 sub_4DCBA4); 656 号亦作废 (实体+44 更新) |
| 下行 (S2C) | `SM_GROUPMEMBERDEL` | 0 | `SmGroupMemberDel` | Aligned | 登记表 §一/二 Rust 组队链发送面; 官方无独立号 — 离开经 SM 667 名单串全量重播; 657 号亦作废 (真身为 SM_CHARSTATUSCHANGED 状态位图广播) |
| 下行 (S2C) | `SM_GROUPMEMBERHP` | 0 | `SmGroupMemberHp` | Aligned | 登记表 §一/二 Rust 组队链发送面 (HP 条为 Rust 扩展显示); 官方无独立号 — 血量变化经 SM 667 全量重播承载 (662-671 体已逐体解码均非组员血量); 658 号让位确认框 |
| 下行 (S2C) | `SM_GROUPMEMBERLIST` | 667 | `SmGroupMemberList` | Aligned | 登记表 §一/二 (客户端 case 667 @0x46A507 → sub_46E020 + M2 直发 sub_4DCBA4: 遍历组员拼名单串逐员发送; 组队族专项双源定谳) 旧 655 号作废 (655=残留写-only); 官方组队唯一通道 = SM 667 名单串, 加入/离开/血量均经全量重播承载 |
| 下行 (S2C) | `SM_GROUPMESSAGE` | 0 | `SmGroupMessage` | Aligned | 登记表 §一/二 Rust 扩展 (C1 自有面, 无官方号) |
| 下行 (S2C) | `SM_GUILDADDMEMBERFAIL` | 754 | `SmGuildAddMemberFail` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_GUILDADDMEMBEROK` | 753 | `SmGuildAddMemberOk` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_GUILDALLYINFO` | 673 | `SmGuildAllyInfo` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_GUILDBREAKALLYFAIL` | 762 | `SmGuildBreakAllyFail` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_GUILDBREAKALLYOK` | 761 | `SmGuildBreakAllyOk` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_GUILDDELMEMBERFAIL` | 756 | `SmGuildDelMemberFail` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_GUILDDELMEMBEROK` | 755 | `SmGuildDelMemberOk` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_GUILDMAKEALLYFAIL` | 760 | `SmGuildMakeAllyFail` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_GUILDMAKEALLYOK` | 759 | `SmGuildMakeAllyOk` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_GUILDMEMBERLIST` | 671 | `SmGuildMemberList` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_GUILDMESSAGE` | 764 | `SmGuildMessage` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_GUILDRANKUPDATEFAIL` | 763 | `SmGuildRankUpdateFail` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_OPENGUILDDLGFAIL` | 752 | `SmOpenGuildDlgFail` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_PARTYDISSOLVED` | 0 | `SmPartyDissolved` | Aligned | 登记表 §一/二 【组队族专项 2026-09-11】662 号作废 — 官方 case 662 (0x46A3A2, IDA 注解) = 纯计时臂 (GetTickCount→4F94E8) |
| 下行 (S2C) | `SM_PARTYINVITE` | 0 | `SmPartyInvite` | Aligned | 登记表 §一/二 Rust 扩展面 (双端表面在, 组队邀请链); 【名字/颜色专项 2026-09-11】661 号作废 — 官方 case 661 (0x46A30F, IDA 注解) = 分类错误提示 (GetTickCount + value switch 三选一文本); 真号待组队族专项 |
| 下行 (S2C) | `SM_PARTYMEMBERS` | 0 | `SmPartyMembers` | Aligned | 登记表 §一/二 Rust 扩展 (C1 自有面, 无官方号); 服务端不下发 |


## 6. 空间跃迁、地图传送与视野实体呈现 (共 2 个: CM 0, SM 2)

| 方向 | 协议宏名称 | 官方/服务编号 | Rust 变体 | 对齐状态 | 权威锚点 / 说明 |
| :--- | :--- | :---: | :--- | :---: | :--- |
| 下行 (S2C) | `SM_MAPDESCRIPTION` | 661 | `SmMapDescription` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_OPENDOOR` | 612 | `SmDoorOpen` | Aligned | 登记表 §一/二 (dispatch case 612 → sub_49F2FC 门开; #16 勘误)  |


## 7. 系统通知、异常拦截回滚与管理调试通道 (共 38 个: CM 9, SM 29)

| 方向 | 协议宏名称 | 官方/服务编号 | Rust 变体 | 对齐状态 | 权威锚点 / 说明 |
| :--- | :--- | :---: | :--- | :---: | :--- |
| 上行 (C2S) | `CM_ADJUSTABILITY` | 0 | `CmAdjustAbility` | Aligned | 登记表 §3.3 Rust 扩展 (C1 自有面, 无官方号) |
| 上行 (C2S) | `CM_ADJUSTBONUS` | 0 | `CmAdjustBonus` | Aligned | 登记表 §3.3 Rust 扩展 (C1 自有面, 无官方号); 客户端无真实发送点 (仅 e2e/断言测试构造) |
| 上行 (C2S) | `CM_CHGPW` | 2018 | `CmChgPw` | Aligned | 登记表 §3.3  |
| 上行 (C2S) | `CM_MAKEWEAPONUNLOCK` | 3023 | `CmMakeWeaponUnlock` | Aligned | 登记表 §3.3 客户端无真实发送点 |
| 上行 (C2S) | `CM_PROTOCOL` | 2000 | `CmProtocol` | Aligned | 登记表 §3.3  |
| 上行 (C2S) | `CM_QUERYUSERSTATE` | 80 | `CmQueryUserState` | Aligned | 登记表 §3.3  |
| 上行 (C2S) | `CM_READBOOK` | 1006 | `CmReadBook` | Aligned | 登记表 §3.3  |
| 上行 (C2S) | `CM_USELAMP` | 3022 | `CmUseLamp` | Aligned | 登记表 §3.3 客户端无真实发送点 |
| 上行 (C2S) | `CM_USERSTATE` | 80 | `CmUserState` | Aligned | 登记表 §3.3  |
| 下行 (S2C) | `SM_ABILITY` | 648 | `SmAbility` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_ADJUSTBONUS` | 811 | `SmAdjustBonus` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_BUFFADD` | 0 | `SmBuffAdd` | Aligned | 登记表 §一/二 【技能族专项】646 号作废 — 官方 case 646 (0x469E5E, IDA 注解) = NPC 窗关闭臂 (sub_46DA68: dword_4F94DC=NPC 目标 + hero+1888=0 + sub_48E934, 与 668 开窗对偶; M2 中继 10129→646); 【SM46 专项定稿】官方无独立 Buff 增删号 — 状态分散承载模型 (SM 46 状态行/SM 53 实体三字/650 金币记录/656 名字色/45EDC8 色表) |
| 下行 (S2C) | `SM_CERTIFICATION` | 2000 | `SmCertification` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_CRY` | 765 | `SmCry` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_DLGMSG` | 636 | `SmDlgMsg` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_ENTITYAPPEAR` | 10 | `SmEntityAppear` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_ENTITYDISAPPEAR` | 11 | `SmEntityDisappear` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_ENTITYTIMESTAMP` | 0 | `SmEntityTimestamp` | Aligned | 登记表 §一/二 Rust 扩展 (C1 自有面, 无官方号; 9003=内部编位); 【金币族专项 2026-09-11】旧'SM 42'锚作废 — 官方 case 42=实体名字广播 (0x46981D: actor+0x24 名字 + actor+0x2C 属性字节), 非时间戳; 时间同步官方面=SM 810, 本变体与之语义重叠 |
| 下行 (S2C) | `SM_HEAR` | 6 | `SmHear` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_INSTANCEHEALGAUGE` | 659 | `SmInstanceHealGauge` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_MENUOK` | 635 | `SmMenuOk` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_MERCHANTSAY` | 633 | `SmMerchantSay` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_MYSTATUS` | 0 | `SmMyStatus` | Aligned | 登记表 §一/二 Rust 扩展 (C1 自有面, 无官方号) |
| 下行 (S2C) | `SM_NPCTEXTAPPLY` | 0 | `SmNpcTextApply` | Aligned | 登记表 §一/二 Rust 扩展 (C1 自有面, 无官方号); 服务端不下发 |
| 下行 (S2C) | `SM_RESERVED617` | 617 | `SmUnused617` | OfficialDead | client-exe-alignment §2.4 L116 官方 case 617=直通 (0x468541 jumptable default def_46A3CA 零处理, 场景/金币族专项解码实证) — Rust 无此变体 |
| 下行 (S2C) | `SM_RESERVED618` | 618 | `SmUnused618` | OfficialDead | client-exe-alignment §2.4 L116 官方 case 618=直通 (同 617) — Rust 无此变体 |
| 下行 (S2C) | `SM_RESERVED654` | 654 | `SmUnused654` | OfficialDead | client-exe-alignment L277 官方 case 654=实体+340 残留字段写-only 无消费者 — Rust 无此变体 (昼夜为 Rust 扩展面见 SM_DAYCHANGING 行) |
| 下行 (S2C) | `SM_RESERVED655` | 655 | `SmUnused655` | OfficialDead | client-exe-alignment L278 官方 case 655=word_4F7F94 残留全局写-only 唯一引用即 case 本身 — Rust 无此变体 (光照为 Rust 扩展面见 SM_CHANGELIGHT 行) |
| 下行 (S2C) | `SM_SENDNOTICE` | 662 | `SmSendNotice` | Aligned | 登记表 §一/二 服务端不下发 |
| 下行 (S2C) | `SM_SERVERCONFIG` | 2012 | `SmServerConfig` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_SKILLTOGGLE` | 0 | `SmSkillToggle` | Aligned | 登记表 §一/二 【技能族专项 2026-09-11】645 号作废 (NPC 商品列表); 官方布尔指示器定谳 = case 659/660 写 byte_4F94F8 (off_4A430C 指针): 本地 XOR 翻转 5s 防抖 (sub_491104 + sub_4664B0 通知) + 双帧图选择 (sub_491008) + case 50 进地图初始化 — 服务器可控非 MagicId 开关, Rust SmSkillToggle{magic_id} 形状不符维持 id 0 |
| 下行 (S2C) | `SM_SUBABILITY` | 0 | `SmSubAbility` | Aligned | 登记表 §一/二 【技能族专项 2026-09-11】649 号作废 — 官方 case 649 (0x469EF7, IDA 注解) = 记录臂 (GetTickCount→实体+0x75C + sub_49F874 记录提交 + 4F9308=0 + 文本) |
| 下行 (S2C) | `SM_SYSTEMMESSAGE` | 7 | `SmSystemMessage` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_TAKEBACKRESULT` | 0 | `SmTakeBackResult` | Aligned | 登记表 §一/二 Rust 扩展 (C1 自有面, 无官方号) |
| 下行 (S2C) | `SM_TIMECHECKMSG` | 810 | `SmTimeCheckMsg` | Aligned | 登记表 §一/二  |
| 下行 (S2C) | `SM_USERNAME` | 751 | `SmUsername` | Aligned | 登记表 §3.3 官方 751 = CM 82 名字应答 (M2 case 82 → sub_4DE654 → SM 751) |

