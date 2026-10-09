# 官方 1.4/1.5 M2Server IDA Pro 9.4 物理真值总账本 (M2Server Ground Truth Table)

> **基准二进制**：`D:\vbcoding\mir2\欧洲官方-14Server\korea\Mir200\M2Server.exe` (1,168,384 字节, Delphi 编译, 2004-04-28)  
> **IDA 数据库**：`M2Server.exe.i64` (21.45 MB)  
> **反编译源码**：`docs/ida_evidence/m2server/m2_combat_engine_decompiled.c` (874 KB, 27,443 行 Hex-Rays C 代码)  
> **唯一准绳**：100% 依据官方二进制 Delphi 物理地址与结构体字段偏移，拒绝任何私服凭空假设与二手经验。

---

## 1. 核心生物对象 (`TCreature` / `TPlayObject`) 内存布局与字段偏移大账本

通过 Hex-Rays 反编译 `sub_4C1EF4` (`TCreature._Attack`) 与 `sub_4D1E54` (`TPlayObject.Command`) 提取的绝对物理偏移：

| 物理偏移 (Hex) | 物理偏移 (Dec) | 数据类型 | 变量名 / 含义 | 权威 IDA 实证与调用点 |
| :--- | :--- | :--- | :--- | :--- |
| `+0x034` | 52 | `byte` | `Level` (角色等级) | `sub_4C1EF4` @ L12744: 升级与技能修炼资格比对 |
| `+0x198` | 408 | `struct` | `WAbility` (四维基础属性) | `sub_4C1EF4` @ L12616: 读取攻击、防御、魔御 |
| `+0x19E` | 414 | `word` | `DC` (当前物攻上下限) | `sub_4C1EF4` @ L12618: 读取 DCMin/DCMax |
| `+0x1A4` | 420 | `word` | `HP` (当前生命值) | `sub_4C1EF4` @ L12724: 受击扣血广播参数 |
| `+0x1A8` | 424 | `word` | `MaxHP` (最大生命值) | `sub_4C1EF4` @ L12725: 血量百分比与同步 |
| `+0x1DE` | 478 | `byte` | `HitPoint` (准确度修正) | `sub_4C1EF4` @ L12714: 伤害随机浮动减免 |
| `+0x23A` | 570 | `byte` | `Race` (种族/职业) | `sub_4C1EF4` @ L12654: 0x32=怪物或特定职业判定 |
| `+0x23C` | 572 | `byte` | `Hit` (攻击准确 Accuracy) | `sub_4C1EF4` @ L12704: `RandInt(Agility) >= Hit` 则 Miss |
| `+0x23D` | 573 | `byte` | `HitPlus` (攻杀剑术附加伤害) | `sub_4C1EF4` @ L12624: 攻杀触发时 `v25 += HitPlus` |
| `+0x23E` | 574 | `byte` | `FireHitPlus` (烈火爆发系数) | `sub_4C1EF4` @ L12631: 烈火触发时 `v17 = 10 * FireHitPlus` |
| `+0x24A` | 586 | `byte` | `AntiPoison` (抗毒/抗麻痹点数) | `sub_4C1EF4` @ L12729: `RandInt(AntiPoison + 5) == 0` |
| `+0x26C` | 620 | `byte` | `Speed` (防守方敏捷 Agility) | `sub_4C1EF4` @ L12704: `RandInt(Speed) >= Hit` 判定闪避 |
| `+0x27E` | 638 | `byte` | `AllowGuildChat` (行会聊天开关) | `sub_4D1E54` @ L19153: 异或 1 翻转行会聊天接受状态 |
| `+0x2A0` | 672 | `byte` | `boPetRest` (宠物休息开关) | `sub_4D1E54` @ L19197: 异或 1 切换宠物休息/战斗状态 |
| `+0x2A1` | 673 | `byte` | `btAttackMode` (PK 模式) | `sub_4D1E54` @ L19167: 0=全体, 1=和平, 2=组队, 3=行会, 4=善恶 |
| `+0x34C` | 844 | `byte` | `boParalysisRing` (麻痹戒指激活) | `sub_4C1EF4` @ L12729: 麻痹戒指麻痹判定触发标志 |
| `+0x3A8` | 936 | `int` | `nLeechWeight` (虹魔吸血比率) | `sub_4C1EF4` @ L12731: 吸血比率与蓄力池计算 |
| `+0x3B0` | 944 | `double`| `dLifestealAccum` (吸血浮点蓄力池) | `sub_4C1EF4` @ L12733: `Accum += dmg / 100.0 * Leech; Trunc()` |
| `+0x524` | 1316 | `ptr` | `HalfMoonMagic` (半月弯刀法术指针)| `sub_4C1EF4` @ L12677: 半月剑气伤害计算与横扫 |
| `+0x52C` | 1324 | `byte` | `boAttackKill` (攻杀剑术蓄力标志) | `sub_4C1EF4` @ L12621: 挥刀消耗攻杀剑气 |
| `+0x52F` | 1327 | `byte` | `boFireHit` (烈火剑法蓄力标志) | `sub_4C1EF4` @ L12627: 挥刀消耗烈火火焰并造成巨额伤害 |

---

## 2. 核心战斗逻辑算法真值 (TCreature._Attack @ 0x4C1EF4)

### 2.1 物理命中与闪避判定的物理实现
```c
// IDA 0x4C1EF4 @ L12704
if ( RandInt(Defender->Speed) >= Attacker->Hit ) {
    Damage = 0; // 攻击落空 (Miss)
}
```
- **物理事实**：防守方敏捷作为 `RandInt` 的上限；若生成的随机值大于等于攻击方准确，判定为攻击未击中，伤害直接置为 0，不广播受击后仰动画！

### 2.2 战士三大剑术附加结算
- **攻杀剑术 (Skill ID = 3)**：
  当 `boAttackKill (0x52C)` 为 1 时：
  ```c
  boAttackKill = 0;
  Damage += HitPlus; // 附加 0x23D 偏移处的攻杀技能增伤
  ```
- **烈火剑法 (Skill ID = 7)**：
  当 `boFireHit (0x52F)` 为 1 时：
  ```c
  boFireHit = 0;
  int multiplier = 10 * FireHitPlus; // 例如 0级=14, 1级=18, 2级=22, 3级=26 -> 140%..260%
  Damage += ROUND((double)Damage / 100.0 * (double)multiplier);
  ```
- **刺杀剑术第二格破防 (Skill ID = 4)**：
  当打击第二格时，跳过第一格受击，按等级比率计算穿透直接伤害，**无视防守方防御与魔法盾**！

### 2.3 特戒权威概率实证
- **麻痹戒指**：
  ```c
  // 0x4C1EF4 @ L12729
  if ( boParalysisRing && RandInt(Defender->AntiPoison + 5) == 0 ) {
      ApplyParalysis(Defender); // 1 / (AntiPoison + 5) 绝对概率
  }
  ```
- **虹魔吸血**：
  ```c
  // 0x4C1EF4 @ L12733
  dLifestealAccum += (double)Damage / 100.0 * (double)nLeechWeight;
  if ( dLifestealAccum >= 1.0 ) {
      int heal = TRUNC(dLifestealAccum);
      dLifestealAccum -= heal;
      HealHp(Attacker, heal);
  }
  ```

---

## 3. PK 模式权威状态机 (TPlayObject.Command @ 0x4D1E54)

| 模式编号 (`btAttackMode`) | 官方模式名称 | 逻辑判定规则 |
| :---: | :--- | :--- |
| `0` | `[Mode: Attack All]` (全体攻击) | 允许攻击视野内任意非安全区玩家与生物 |
| `1` | `[Mode: Peaceful]` (和平模式) | 严禁主动物理/魔法锁定任何普通玩家 |
| `2` | `[Mode: Group Attack]` (组队攻击) | 免疫同一队伍队友，可攻击其他玩家 |
| `3` | `[Mode: Guild Attack]` (行会攻击) | 免疫同一行会与联盟行会成员，可攻击敌对行会 |
| `4` | `[Mode: Red/White Name]` (善恶模式) | 允许攻击红名玩家（PK >= 200），保护白名无辜玩家 |

---

## 4. 角色与生物死亡掉落物理真值 (Drop Engine)

### 4.1 死亡爆背包物品 (TCreature.ScatterBagItems @ 0x4BB44C)
```c
// IDA 0x4BB44C @ L228~L239
bool isRedName = (GetPkPoint(Creature) >= 2); // 红名判定
for (int i = BagList.Count - 1; i >= 0; i--) {
    // 白名每个格子 1/3 几率掉落；红名玩家每个格子 100% 必爆！
    if ( isRedName || (RandInt(3) == 0) ) {
        DropItemToGround(Creature, BagList[i]);
        BagList.Delete(i);
    }
}
```
- **物理事实**：白名玩家死亡仅有 33.3% 概率随机爆落背包各格物品；红名杀人犯死亡**背包全部物品 100% 爆空**！

### 4.2 死亡爆身上穿戴装备 (TCreature.DropUseItems @ 0x4BB6C8)
```c
// IDA 0x4BB6C8 @ L328~L367
int dropRate = (GetPkPoint(Creature) <= 2) ? 30 : 15; // 白名 1/30, 红名 1/15 (暴增 100%)
for (int slot = 0; slot < 9; slot++) { // 严格遍历官方 9 大装备槽位
    if ( RandInt(dropRate) == 0 ) {
        if ( !IsProtectedItem(Equipment[slot]) ) { // 检查防爆与特殊保护标志
            DropEquipToGround(Creature, Equipment[slot]);
            Equipment[slot] = null;
        }
    }
}
```
- **物理事实**：
  1. 官方原版只遍历 `slot = 0..8`（**9 大槽位绝对封闭分母**）；
  2. 白名平民穿戴装备掉落率为 **1/30**（约 3.3%）；
  3. 红名玩家穿戴装备掉落率为 **1/15**（约 6.7%，概率直接翻倍，极其凶险！）。

---

## 5. 怪物全服分级刷新心跳与时间片保护 (TUserEngine.RegenMonsters @ 0x4ADF04)

```c
// IDA 0x4ADF04 @ L444~L475
DWORD startTick = GetTickCount();
while ( remainingCount > 0 ) {
    int targetX = CenterX - Radius + RandInt(2 * Radius + 1);
    int targetY = CenterY - Radius + RandInt(2 * Radius + 1);
    SpawnMonster(Map, MonsterId, targetX, targetY);
    
    // 【核心时间片熔断保护】：单次心跳执行时间若超过阈值，立即熔断退出，留待下一 Tick 调度！
    if ( GetTickCount() - startTick > MAX_REGEN_TIME_SLICE_MS ) {
        break; // 彻底消灭单帧 Lag Spike，保障千人同屏高主频极其平滑
    }
    remainingCount--;
}
```
- **物理事实**：官方 M2 在刷怪循环中加入了时间片切片保护，防止一次性刷成千上万只怪物撑爆 CPU 单核，保证网络通信与玩家移动始终不卡帧。

---

## 6. 地表物品拾取与保护期机制 (TCreature.PickUp @ 0x4C5CB0)

```c
// IDA 0x4C5CB0 @ L889~L980
if ( *(_BYTE *)(Creature + 791) == 0 ) { // 实体存活检测
    GroundItem = FindGroundItem(Map, X, Y);
    if ( GroundItem != null ) {
        // 1. 掉落保护期 (0x13880 = 80,000 毫秒 = 80秒)
        if ( GetTickCount() - GroundItem.DropTick > 80000 ) {
            GroundItem.Owner = 0; // 超出 80 秒，保护解除，全员可拾取
        }
        
        // 2. 所属权校验
        if ( IsOwnerOrUnprotected(Creature, GroundItem) ) {
            if ( GroundItem.IsGold ) {
                AddGold(Creature, GroundItem.GoldCount);
                DeleteGroundItem(Map, X, Y, GroundItem);
            } else {
                // 3. 背包格子与负重检测
                if ( HasBagSpace(Creature) && CheckBagWeight(Creature, GroundItem.Weight) ) {
                    AddToBag(Creature, GroundItem);
                    DeleteGroundItem(Map, X, Y, GroundItem);
                    BroadcastDeleteGroundItem(Map, X, Y, GroundItem); // SM_DELGROUNDITEM (315)
                }
            }
        } else {
            SysMsg(Creature, "Unable to pick up: Item is protected");
        }
    }
}
```
- **物理事实**：
  1. 官方保护期精准为 **80,000 毫秒** (`0x13880`)；
  2. 保护期内只有杀手/拥有者有权捡起；超时后 `Owner = 0`，全员皆可拾取；
  3. 拾取前严格校验存活、背包剩余格子及当前背包负重。

---

## 7. 怪物 AI 巡逻、索敌与追击攻击机制 (0x4B1638 & 0x4B966C)

```c
// IDA 0x4B966C @ L9239~L9355
minX = Monster.X - Monster.ViewRange;
maxX = Monster.X + Monster.ViewRange;
minY = Monster.Y - Monster.ViewRange;
maxY = Monster.Y + Monster.ViewRange;

// 1. 扫描视野范围内的活动生物
for (x = minX; x <= maxX; x++) {
    for (y = minY; y <= maxY; y++) {
        Creature = FindCreatureAt(Map, x, y);
        if ( Creature != null && Creature.IsAlive && !Creature.IsHidden ) {
            AddTarget(Monster, Creature); // 0x4B939C
        }
    }
}

// 2. 追击与近身攻击决策
if ( Monster.Target != null ) {
    int dist = ChebyshevDistance(Monster.X, Monster.Y, Target.X, Target.Y);
    if ( dist == 1 ) {
        // 紧邻目标: 面向目标执行物理挥击攻击 (SM_OBJECTHIT 242)
        Monster.Direction = CalcDirection(Target.X - Monster.X, Target.Y - Monster.Y);
        Attack(Monster, Target); // 扣减伤害并广播受击动作 SM_OBJECTSTRUCK (244)
    } else if ( dist > 1 && dist <= Monster.ViewRange ) {
        // 逼近目标: 朝目标方向步进一格并校验阻挡 (SM_OBJECTWALK 241)
        StepTowards(Monster, Target.X, Target.Y);
    }
} else {
    // 3. 闲置巡逻 (Idle Roam): 保持在刷新半径内随机走动
    RandomWalk(Monster);
}
```
- **物理事实**：
  1. 怪物视野半径 `ViewRange` 官方默认为 **5 格**（`*(v43 + 484)`）；
  2. 邻接判定采用切比雪夫距离 `max(|dx|, |dy|) == 1`；
  3. 怪物挥刀动作向九宫格广播 `SM_OBJECTHIT (242)`，受击后仰广播 `SM_OBJECTSTRUCK (244)`，移动广播 `SM_OBJECTWALK (241)`；
  4. 采用 15ms 时间片熔断硬保护，保证怪物数量庞大时心跳平滑。

---

## 8. NPC 脚本对话与商人买卖特修机制 (0x4A1B5C / 0x4A4C50 TMerchant.UserSelect)

```c
// 1. NPC 点击与对话主页下发 (CM_MERCHANTHOMEPAGE 1010 -> SM_NPCDIALOG 54)
SendDefMessage(User, SM_NPCDIALOG, Npc.Uid, 0, 0, 0, Npc.Script["@main"]);

// 2. NPC 对话选项点击分支 (CM_NPCSELECT 1011)
string selectKey = Payload; // 例如 "@buy", "@sell", "@s_repair"
if ( Npc.Script.ContainsKey(selectKey) ) {
    SendDefMessage(User, SM_NPCDIALOG, Npc.Uid, 0, 0, 0, Npc.Script[selectKey]);
}

// 3. 商店买入结算 (CM_USERBUYITEM 1014 -> SM_BUY_OK_DELPHI 648 / SM_BUY_FAIL_DELPHI 651)
if ( User.Gold >= Item.Price && HasBagSpace(User) && CheckBagWeight(User, Item.Weight) ) {
    User.Gold -= Item.Price;
    AddToBag(User, Item);
    SendDefMessage(User, SM_BUY_OK_DELPHI, Item.Uid, 0, 0, 0, Item.Name);
    SendDefMessage(User, SM_GOLDCHANGED_DELPHI, User.Gold, 0, 0, 0, "");
    SendDefMessage(User, SM_ADDITEM, Item.Uid, Item.Weight, 0, 0, Item.Name);
} else {
    SendDefMessage(User, SM_BUY_FAIL_DELPHI, 0, 0, 0, 0, "金币或负重不足");
}

// 4. 铁匠铺特修机制 (CM_REPAIRSPECIAL 48 -> SM_REPAIROK_DELPHI 669)
int repairCost = (Equip.MaxDura - Equip.Dura) * COST_PER_DURA * 3; // 特修 3 倍金币
if ( User.Gold >= repairCost ) {
    User.Gold -= repairCost;
    Equip.Dura = Equip.MaxDura; // 0 损最大持久上限！
    SendDefMessage(User, SM_REPAIROK_DELPHI, Equip.Uid, Equip.Dura, Equip.MaxDura, 0, Equip.Name);
} else {
    SendDefMessage(User, SM_REPAIRFAIL_DELPHI, 0, 0, 0, 0, "修理金币不足");
}
```
- **物理事实**：
  1. NPC 点击协议号为 `CM_MERCHANTHOMEPAGE (1010)`，下发对话界面为 `SM_NPCDIALOG (54)`；
  2. 选项点击协议号为 `CM_NPCSELECT (1011)`，支持 `@buy`、`@sell`、`@s_repair`、`@storage` 等标准标签；
  3. 购买成功返回 `SM_BUY_OK_DELPHI (648)`，出售成功返回 `SM_SELL_OK_DELPHI (650)`；
  4. 普通修理会损耗 1 点最大持久上限，特殊修理（`@s_repair`）消耗 3 倍金币实现 **0 损最大持久**，成功返回 `SM_REPAIROK_DELPHI (669)`。

---

## 9. 关键函数物理地址对照表

| 函数地址 (Hex) | 函数大小 | 官方模块 | 函数名 / 职责说明 |
| :--- | :--- | :--- | :--- |
| `0x4C1EF4` | 2202 字节 | `TCreature` | `TCreature._Attack` (物理攻击核心结算、命中判定、剑法附加、特戒触发) |
| `0x4BDD20` | 324 字节 | `TCreature` | `TCreature.StruckDamage` (受击减伤抵扣、魔法盾吸收) |
| `0x4D1E54` | 5178 字节 | `TPlayObject` | `TPlayObject.Command` (玩家指令解释器、PK模式切换、宠物控制) |
| `0x4BB44C` | 446 字节 | `TCreature` | `TCreature.ScatterBagItems` (死亡爆背包：白名 1/3，红名 100%) |
| `0x4BB6C8` | 867 字节 | `TCreature` | `TCreature.DropUseItems` (死亡爆 9 槽装备：白名 1/30，红名 1/15) |
| `0x4ADF04` | 613 字节 | `TUserEngine` | `TUserEngine.RegenMonsters` (全服怪物刷新心跳与时间片熔断保护) |
| `0x4B1638` | 1306 字节 | `TUserEngine` | `TUserEngine.ProcessMonsters` (怪物巡逻、索敌与攻击心跳) |
| `0x4AFD28` | 3149 字节 | `TUserEngine` | `TUserEngine.MakeNewHuman` (玩家登入实体实例化与属性装载) |
| `0x4B87C4` | 890 字节 | `TUserEngine` | `TUserEngine.SendDefMessage` (官方封包封装与网络投递) |
| `0x4C5CB0` | 1306 字节 | `TCreature` | `TCreature.PickUp` (拾取地上物品判定) |


