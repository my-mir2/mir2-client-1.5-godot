# Mir2.Bot —— 无头玩家机器人

像真人一样玩游戏: 真实 TCP 连接服务端，只讲线协议、只信服务端下发的包，不引用任何服务端/客户端代码。
流程: 登录 -> 建角色 -> 进图 -> A* 寻路 -> 攻击 -> 拾取，最后输出**玩家视角报告** (击杀耗时、单刀伤害、受到伤害、掉落、升级曲线、遇到的问题)。

## 用法
```powershell
# 1) 启动服务端 (需要本机 PostgreSQL 与已导入的官方世界数据)
.\bin\server_release\Mir2.Server.exe

# 2) 另开终端跑机器人
dotnet run --project tools/Mir2.Bot -- --kills 8                 # 打 8 只怪
dotnet run --project tools/Mir2.Bot -- --target-level 12         # 一直打到 12 级，输出升级曲线
dotnet run --project tools/Mir2.Bot -- --hunt 半兽人 --kills 20   # 只打名字含“半兽人”的怪
dotnet run --project tools/Mir2.Bot -- --trace                   # 额外打印收到的所有包类型及样例
```

## 经验倍率 (加速练级/跑各种怪)
服务端支持击杀经验倍率，官方为 1.0，**仅用于机器人/压测，生产环境必须保持 1.0**:
```powershell
$env:MIR2_EXP_RATE = '100'; .\bin\server_release\Mir2.Server.exe     # 环境变量
# 或在 config/server.ini 写入: ExpRate=100
```
100 倍下机器人约 5 分钟、击杀 44 只鸡即可练到 12 级。

## 机器人的“玩家常识”
- 血量低于一半就停手休息到 85%；
- 一场架被打掉 60% 以上最大血量的怪，之后绕开；
- 背包满了就不再白跑去捡 (没有卖物/丢弃逻辑)；
- 默认不打城镇守卫 (`--attack-guards` 才打)。

## 目前没覆盖的玩法 (不得据此宣称“已验证”)
换图/传送门、商店买卖与吃药、技能与法/道职业、死亡与复活、多人同屏与断线重连、长时间运行。
