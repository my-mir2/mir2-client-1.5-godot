# 无头机器人套件报告

生成时间: 2026-10-15 05:40:30  重复次数: 1

## 坏账（先看这里）

- FAIL x0
- INCONCLUSIVE x0 (没触发想测的事件，不算通过)
- 服务端日志异常行 x0
- 尚未覆盖 (NOT_IMPLEMENTED):
  - 断线重连
  - 行会/交易
  - 长时间(>1h)稳定性

## 汇总

场景运行 21 次: PASS 21 / FAIL 0 / INCONCLUSIVE 0

| 场景 | 状态 | 用时(s) |
|---|---|---|
| chicken_bichon#0.0 | PASS | 69 |
| gm_level_gold_item#0.0 | PASS | 13 |
| buy_and_drink_potion#0.0 | PASS | 30 |
| equip_wear_takeoff_drop#0.0 | PASS | 13 |
| travel_and_hunt#0.0 | PASS | 219 |
| strong_monsters#0.0 | PASS | 118 |
| death_and_revive#0.0 | PASS | 17 |
| multi_player_same_map#0.0 | PASS | 22 |
| multi_player_same_map#0.1 | PASS | 22 |
| multi_player_same_map#0.2 | PASS | 22 |
| mage_magic_combat#0.0 | PASS | 24 |
| magic_keybind_setup#0.0 | PASS | 13 |
| taoist_combat_and_pet#0.0 | PASS | 98 |
| party_group_exp_share#0.0 | PASS | 27 |
| party_group_exp_share#0.1 | PASS | 27 |
| adjust_bonus_alloc#0.0 | PASS | 12 |
| drop_gold_to_ground#0.0 | PASS | 12 |
| butch_corpse_meat#0.0 | PASS | 26 |
| sell_item_to_shop#0.0 | PASS | 10 |
| repair_item_at_shop#0.0 | PASS | 11 |
| sword_skill_switch#0.0 | PASS | 13 |

## 规则明细

### chicken_bichon#0.0  PASS
- [PASS] R-无机器人侧异常 
- [PASS] R-位置拉回<=3 次 
- [PASS] R-守卫不攻击非红名玩家 
- [PASS] R-掉落物 ⊆ 官方掉落表(DB) 
- [PASS] R-击杀>=8 
- [PASS] R-1 级打鸡不死 

### gm_level_gold_item#0.0  PASS
- [PASS] R-无机器人侧异常 
- [PASS] R-位置拉回<=3 次 
- [PASS] R-守卫不攻击非红名玩家 
- [PASS] R-GM 设等级生效 
- [PASS] R-30 级最大血量合理(>=100，与真实升级同一路径) 
- [PASS] R-GM 设金币回显 

### buy_and_drink_potion#0.0  PASS
- [PASS] R-无机器人侧异常 
- [PASS] R-位置拉回<=3 次 
- [PASS] R-守卫不攻击非红名玩家 
- [PASS] R-NPC交互对话回显 
- [PASS] R-商店买药成功(648/200) 
- [PASS] R-使用药水成功(606 SM_EAT_OK) 
- [PASS] R-药水回血生效(HP>5) 

### equip_wear_takeoff_drop#0.0  PASS
- [PASS] R-无机器人侧异常 
- [PASS] R-位置拉回<=3 次 
- [PASS] R-守卫不攻击非红名玩家 
- [PASS] R-装备穿戴成功(615 SM_TAKEON_OK 槽位0) 
- [PASS] R-装备脱下成功(619 SM_TAKEOFF_OK 槽位0) 
- [PASS] R-丢弃物品成功(202 SM_DELITEM / 625 SM_ITEMSHOW) 

### travel_and_hunt#0.0  PASS
- [PASS] R-无机器人侧异常 
- [PASS] R-位置拉回<=3 次 
- [PASS] R-守卫不攻击非红名玩家 
- [PASS] R-落点 == 数据库传送门目标 (1) 
- [PASS] R-换图后视野内有实体 (1) 
- [PASS] R-掉落物 ⊆ 官方掉落表(DB) 
- [PASS] R-新地图击杀>=6 
- [PASS] R-最终在目标地图 

### strong_monsters#0.0  PASS
- [PASS] R-无机器人侧异常 
- [PASS] R-位置拉回<=3 次 
- [PASS] R-守卫不攻击非红名玩家 
- [PASS] R-掉落物 ⊆ 官方掉落表(DB) 
- [PASS] R-GM 刷怪成功 
- [PASS] R-确实与刷出的怪交手 

### death_and_revive#0.0  PASS
- [PASS] R-无机器人侧异常 
- [PASS] R-位置拉回<=3 次 
- [PASS] R-守卫不攻击非红名玩家 
- [PASS] R-每次死亡都收到复活 
- [PASS] R-最终存活 

### multi_player_same_map#0.0  PASS
- [PASS] R-无机器人侧异常 
- [PASS] R-位置拉回<=3 次 
- [PASS] R-守卫不攻击非红名玩家 
- [PASS] R-多人同图各自击杀>=3 

### multi_player_same_map#0.1  PASS
- [PASS] R-无机器人侧异常 
- [PASS] R-位置拉回<=3 次 
- [PASS] R-守卫不攻击非红名玩家 
- [PASS] R-多人同图各自击杀>=3 

### multi_player_same_map#0.2  PASS
- [PASS] R-无机器人侧异常 
- [PASS] R-位置拉回<=3 次 
- [PASS] R-守卫不攻击非红名玩家 
- [PASS] R-多人同图各自击杀>=3 

### mage_magic_combat#0.0  PASS
- [PASS] R-无机器人侧异常 
- [PASS] R-位置拉回<=3 次 
- [PASS] R-守卫不攻击非红名玩家 
- [PASS] R-已学会雷电术(201) 
- [PASS] R-法术施法与光效广播(243/614) 
- [PASS] R-法术击杀>=3 

### magic_keybind_setup#0.0  PASS
- [PASS] R-无机器人侧异常 
- [PASS] R-位置拉回<=3 次 
- [PASS] R-守卫不攻击非红名玩家 
- [PASS] R-已学会雷电术(201) 
- [PASS] R-技能快捷键绑定生效(1008 CM_MAGICKEYCHANGE / 203 SM_UPDATEUSERMAGIC F1) 

### taoist_combat_and_pet#0.0  PASS
- [PASS] R-无机器人侧异常 
- [PASS] R-位置拉回<=3 次 
- [PASS] R-守卫不攻击非红名玩家 
- [PASS] R-已学会灵魂火符与召唤骷髅(201) 
- [PASS] R-道士施法与光效广播(243/614) 
- [PASS] R-道士协同击杀>=2 

### party_group_exp_share#0.0  PASS
- [PASS] R-无机器人侧异常 
- [PASS] R-位置拉回<=3 次 
- [PASS] R-守卫不攻击非红名玩家 
- [PASS] R-队伍成员名单广播(667 SM_GROUPMEMBERS) 
- [PASS] R-队长击杀怪物 

### party_group_exp_share#0.1  PASS
- [PASS] R-无机器人侧异常 
- [PASS] R-位置拉回<=3 次 
- [PASS] R-守卫不攻击非红名玩家 
- [PASS] R-队伍成员名单广播(667 SM_GROUPMEMBERS) 
- [PASS] R-队员获得同屏均摊经验 

### adjust_bonus_alloc#0.0  PASS
- [PASS] R-无机器人侧异常 
- [PASS] R-位置拉回<=3 次 
- [PASS] R-守卫不攻击非红名玩家 
- [PASS] R-自由加点生命值生效(1043 CM_ADJUST_BONUS) 
- [PASS] R-属性点扣减生效(SM_ABILITY 剩余点数=4) 

### drop_gold_to_ground#0.0  PASS
- [PASS] R-无机器人侧异常 
- [PASS] R-位置拉回<=3 次 
- [PASS] R-守卫不攻击非红名玩家 
- [PASS] R-丢弃金币扣减生效(1016 CM_DROPGOLD 2000->1800) 
- [PASS] R-地面金币出现并广播(SM_ITEMSHOW) 

### butch_corpse_meat#0.0  PASS
- [PASS] R-无机器人侧异常 
- [PASS] R-位置拉回<=3 次 
- [PASS] R-守卫不攻击非红名玩家 
- [PASS] R-生物尸体割肉成功(1007 CM_BUTCHITEM / 665 SM_BUTCH) 

### sell_item_to_shop#0.0  PASS
- [PASS] R-无机器人侧异常 
- [PASS] R-位置拉回<=3 次 
- [PASS] R-守卫不攻击非红名玩家 
- [PASS] R-商店出售物品成功(1013 CM_USERSELLITEM / 650 SM_SELL_OK_DELPHI) 
- [PASS] R-出售后金币正确增加 

### repair_item_at_shop#0.0  PASS
- [PASS] R-无机器人侧异常 
- [PASS] R-位置拉回<=3 次 
- [PASS] R-守卫不攻击非红名玩家 
- [PASS] R-铁匠铺装备修理成功(1023 CM_USERREPAIRITEM / 669 SM_REPAIROK_DELPHI) 

### sword_skill_switch#0.0  PASS
- [PASS] R-无机器人侧异常 
- [PASS] R-位置拉回<=3 次 
- [PASS] R-守卫不攻击非红名玩家 
- [PASS] R-刺杀剑术开关提示生效(3026 CM_THRUSTINGONOFF) 
- [PASS] R-半月弯刀开关提示生效(3027 CM_HALFMOONONOFF) 
