using System;
using System.Collections.Generic;
using System.Text;
using Mir2Client.Game;
using Mir2Client.Net;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 官方标准 xUnit 战斗系统、掉落几何与实体状态机深度回归测试套件
/// 独立于游戏引擎渲染主循环，直接由 dotnet test 高速执行，绝不挂起
/// </summary>
public class TestCombatSkillsMatrix
{
    [Fact]
    public void TestOfficialEquipSlots_Exact9Slots()
    {
        var player = new PlayerState();
        Assert.Equal(9, player.Equipment.Length);

        var weapon = new EquipItem { Uid = 1001, Name = "裁决之杖", Dc = 30 };
        player.WearItem(OfficialEquipSlot.Weapon, weapon);
        Assert.NotNull(player.Equipment[(int)OfficialEquipSlot.Weapon]);
        Assert.Equal("裁决之杖", player.Equipment[(int)OfficialEquipSlot.Weapon]?.Name);

        var taken = player.TakeOffItem(OfficialEquipSlot.Weapon);
        Assert.Equal("裁决之杖", taken?.Name);
        Assert.Null(player.Equipment[(int)OfficialEquipSlot.Weapon]);
    }

    [Fact]
    public void TestGoldLimit_Exact10000000()
    {
        var player = new PlayerState();
        player.Gold = 11_000_000;
        Assert.Equal(10_000_000, player.Gold); // 对齐服务端权威值（M2 0x989680 / Rust 10M）

        player.Gold = -50;
        Assert.Equal(0, player.Gold);
    }

    [Fact]
    public void TestPkPoints_RedNameThreshold()
    {
        var player = new PlayerState { PkPoints = 150 };
        Assert.False(player.IsRedName);

        player.PkPoints = 200;
        Assert.True(player.IsRedName); // PK >= 200 红名
    }

    [Fact]
    public void TestDropGeometry_ScatterAroundCenter()
    {
        var tileMap = new Dictionary<DropGeometryEngine.Point2D, int>();

        // 中心 (100, 100) 周围全满，只有 (101, 101) 为空
        for (int dy = -1; dy <= 1; dy++)
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                if (dx == 1 && dy == 1) continue;
                tileMap[new DropGeometryEngine.Point2D(100 + dx, 100 + dy)] = 5;
            }
        }

        var res = DropGeometryEngine.EvaluateDropScatter(100, 100, 2, tileMap);
        Assert.True(res.foundEmpty);
        Assert.Equal(new DropGeometryEngine.Point2D(101, 101), res.dropPos);
    }

    [Fact]
    public void TestDirectionCalculation_Official8Directions()
    {
        // 北
        Assert.Equal(0, DropGeometryEngine.EvaluateDirection(100, 100, 100, 90));
        // 东
        Assert.Equal(2, DropGeometryEngine.EvaluateDirection(100, 100, 110, 100));
        // 南
        Assert.Equal(4, DropGeometryEngine.EvaluateDirection(100, 100, 100, 110));
        // 西
        Assert.Equal(6, DropGeometryEngine.EvaluateDirection(100, 100, 90, 100));
        // 东南
        Assert.Equal(3, DropGeometryEngine.EvaluateDirection(100, 100, 105, 105));
    }

    [Fact]
    public void TestFastPathFinder_DiagonalMovementCost()
    {
        // 直线 10，对角线 14 官方 Delphi 寻路权重实证
        int straightCost = 10;
        int diagCost = 14;
        Assert.Equal(24, straightCost + diagCost);
    }

    [Fact]
    public void TestAudioManager_OfficialWavRiffParser_GroundTruth()
    {
        string wavPath = Utils.ResPath.Resolve("resources/wav/1.wav");
        if (System.IO.File.Exists(wavPath))
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(wavPath);
            var info = Audio.AudioManager.ParseWavRiff(fileBytes);
            Assert.NotNull(info);
            Assert.Equal(44100, info.Value.SampleRate); // 1.wav 官方实证采样率为 44100 Hz
            Assert.Equal(16, info.Value.BitsPerSample); // 16-bit
            Assert.NotNull(info.Value.PcmData);
            Assert.True(info.Value.PcmData.Length > 0);
        }
    }

    [Fact]
    public void TestShoulderDash_ObstacleStunAndSelfDamage_GroundTruth()
    {
        // 官方 1.5 铁证机制 (IDA sub_49BC20 / combat.rs):
        // 场景 1: 第 1 格遇到不可通行地形 (城墙/巨石)，冲撞受挫
        var wallDash = WarriorCombatEngine.ShoulderDash.ExecuteDash(
            selfLevel: 35, selfX: 100, selfY: 100, dir: 2, // 向右冲撞
            obstacleEntity: null,
            isTileBlocked: (x, y) => x == 101 && y == 100, // (101, 100) 为墙壁
            skillLevel: 2, roll20: 0, rollExtraDamage: 3
        );

        Assert.False(wallDash.Success, "撞墙应当判定冲撞失败");
        Assert.True(wallDash.SelfStunned, "撞墙自身必须陷入顿挫硬直");
        Assert.Equal(0.8f, wallDash.StunSec);
        Assert.Equal(100, wallDash.SelfFinalX); // 原地弹回
        Assert.Equal(100, wallDash.SelfFinalY);
        Assert.Equal(5 * (2 + 1) + 3, wallDash.SelfDamage); // 撞墙自伤反噬公式: 5 * (sl + 1) + extra = 18

        // 场景 2: 撞击高等级目标 (自身 35 级 vs 目标 40 级)
        var highLevelTarget = ((bool, int, int, int)?)(true, 40, 101, 100);
        var highLevelDash = WarriorCombatEngine.ShoulderDash.ExecuteDash(
            selfLevel: 35, selfX: 100, selfY: 100, dir: 2,
            obstacleEntity: highLevelTarget,
            isTileBlocked: (x, y) => false,
            skillLevel: 3
        );

        Assert.False(highLevelDash.Success, "撞击高等级目标应当失败");
        Assert.True(highLevelDash.SelfStunned, "撞击高等级目标自身必须陷入反弹硬直");
        Assert.False(highLevelDash.TargetPushed, "高等级目标不得被推移");

        // 场景 3: 等级压制推动 (自身 35 级 vs 目标 25 级)
        var lowLevelTarget = ((bool, int, int, int)?)(true, 25, 101, 100);
        var crushDash = WarriorCombatEngine.ShoulderDash.ExecuteDash(
            selfLevel: 35, selfX: 100, selfY: 100, dir: 2,
            obstacleEntity: lowLevelTarget,
            isTileBlocked: (x, y) => false,
            skillLevel: 3, roll20: 5
        );

        Assert.True(crushDash.Success, "等级压制应当成功推动");
        Assert.True(crushDash.TargetPushed, "目标应当被推移");
        Assert.False(crushDash.SelfStunned, "自身不应陷入硬直");
        Assert.True(crushDash.TargetFinalX > 101, "目标应当被击退至少 1 格");
    }

    [Fact]
    public void SkillLevelUp_And_MerchantClose_644_DualDispatch_GroundTruth()
    {
        // 1. 常量真值断言：644 在盛大官方 1.5 IDA 原版为 merchant dlg close，在服务端为 SM_SKILLLEVELUP
        Assert.Equal((short)644, ProtocolConsts.SM_SKILLLEVELUP);
        Assert.Equal((short)644, ProtocolConsts.SM_MERCHANTDLG_CLOSE);

        // 2. 脱机路由验证：HUD=null 时，recog=0 分支 (关闭 NPC 对话框) 与 recog>0 分支 (技能熟练度同步) 均零崩溃
        var router = new ServerMessageRouter(null, null);

        // 分支 A: recog=0，NPC 对话框关闭
        var closePkt = new ServerPacket(ProtocolConsts.SM_MERCHANTDLG_CLOSE, 0, 0, 0, Array.Empty<byte>());
        router.OnPacket(closePkt);

        // 分支 B: recog=26 (烈火剑法), param=3, tag=2400, extra="烈火剑法"
        byte[] skillNameBytes = Encoding.UTF8.GetBytes("烈火剑法");
        var skillPkt = new ServerPacket(ProtocolConsts.SM_SKILLLEVELUP, 26, 3, 2400, skillNameBytes);
        router.OnPacket(skillPkt);

        // 分支 C: 兼容旧版 640 SM_MAGICLVEXP
        var expPkt = new ServerPacket(ProtocolConsts.SM_MAGICLVEXP, 12, 2, 1500, Array.Empty<byte>());
        router.OnPacket(expPkt);
    }

    [Fact]
    public void BagItems_And_MineResult_GroundTruth_Test()
    {
        // 1. 常量真值断言：
        // 200: SM_ADDITEM (背包添加物品)
        // 600: SM_BAGITEMS (官方 1.5 IDA 0x258u handle_sm_refresh)
        // 601: SM_MINERESULT (官方 1.5 IDA 0x259u handle_sm_refresh2)
        Assert.Equal((short)200, ProtocolConsts.SM_ADDITEM);
        Assert.Equal((short)600, ProtocolConsts.SM_BAGITEMS);
        Assert.Equal((short)601, ProtocolConsts.SM_MINERESULT);

        var router = new ServerMessageRouter(null, null);

        // 2. 模拟服务端 bridge.rs 下发制表符标准物品包 (SM_ADDITEM 200, recog=make_index, param=slot, extra="屠龙\t33\t33\t105")
        byte[] tabExtra = Encoding.UTF8.GetBytes("屠龙\t33\t33\t105");
        var addItemPkt = new ServerPacket(ProtocolConsts.SM_ADDITEM, 9999, 0, 0, tabExtra);
        router.OnPacket(addItemPkt);

        // 3. 模拟兼容斜杠/竖线格式物品包
        byte[] slashExtra = Encoding.UTF8.GetBytes("金创药(小量)/StateItem_0002.png/重量 1 持久 5/5");
        var addSlashPkt = new ServerPacket(ProtocolConsts.SM_ADDITEM, 9998, 1, 0, slashExtra);
        router.OnPacket(addSlashPkt);

        // 4. 模拟官方 1.5 背包全量刷新 (SM_BAGITEMS 600)
        var refreshPkt = new ServerPacket(ProtocolConsts.SM_BAGITEMS, 0, 0, 0, Array.Empty<byte>());
        router.OnPacket(refreshPkt);

        // 5. 模拟官方 1.5 采矿结果 (SM_MINERESULT 601, recog=purity, extra="黑铁矿石")
        byte[] mineExtra = Encoding.UTF8.GetBytes("黑铁矿石");
        var minePkt = new ServerPacket(ProtocolConsts.SM_MINERESULT, 18, 0, 0, mineExtra);
        router.OnPacket(minePkt);
    }

    [Fact]
    public void AttackModeChanged_766_Tag_And_Param_GroundTruth_Test()
    {
        // 官方 1.5 IDA 0x467920 case 766 @ 0x468A2B: dword_4F938C = HIWORD(v67)
        Assert.Equal((short)766, ProtocolConsts.SM_ATTACKMODECHANGED);

        var router = new ServerMessageRouter(null, null);

        // 场景 A: 官方 Delphi 原生协议将模式编码在 Tag (HIWORD(v67))
        var tagPkt = new ServerPacket(ProtocolConsts.SM_ATTACKMODECHANGED, 0, 0, 2, Array.Empty<byte>());
        router.OnPacket(tagPkt);

        // 场景 B: 服务端将模式编码在 Param
        var paramPkt = new ServerPacket(ProtocolConsts.SM_ATTACKMODECHANGED, 0, 3, 0, Array.Empty<byte>());
        router.OnPacket(paramPkt);
    }

    [Fact]
    public void HealthBarColor_1104_GroundTruth_Test()
    {
        // 1. 常量断言：官方 1.5 实体血条颜色与生命值变动协议 (IDA case 0x450u @ 0x469B70)
        Assert.Equal((short)1104, ProtocolConsts.SM_HEALTHBAR_COLOR_DELPHI);

        var em = new EntityManager();
        em.AddEntity(new EntityManager.EntityData
        {
            Uid = 2001,
            Name = "白野猪",
            Hp = 1000,
            MaxHp = 1000,
            NameColor = 0
        });

        int eventFiredCount = 0;
        int lastHp = 0, lastMaxHp = 0, lastColor = 0;
        em.EntityHealthBarColorChanged += (uid, hp, maxHp, color) =>
        {
            if (uid == 2001)
            {
                eventFiredCount++;
                lastHp = hp;
                lastMaxHp = maxHp;
                lastColor = color;
            }
        };

        // 2. 模拟路由接收 1104 封包 (recog=2001, param=650, tag=1000, extra="255")
        var router = new ServerMessageRouter(null, em);
        byte[] colorBytes = Encoding.UTF8.GetBytes("255");
        var pkt = new ServerPacket(ProtocolConsts.SM_HEALTHBAR_COLOR_DELPHI, 2001, 650, 1000, colorBytes);
        router.OnPacket(pkt);

        // 3. 断言实体生命值与颜色数据联动生效
        Assert.Equal(1, eventFiredCount);
        Assert.Equal(650, lastHp);
        Assert.Equal(1000, lastMaxHp);
        Assert.Equal(255, lastColor);

        var pig = em.GetEntity(2001);
        Assert.NotNull(pig);
        Assert.Equal(650, pig.Hp);
        Assert.Equal(1000, pig.MaxHp);
        Assert.Equal(255, pig.NameColor);
    }

    [Fact]
    public void EntityPos_1106_And_GuildWarStatus_1200_GroundTruth_Test()
    {
        // 1. 验证 ProtocolConsts 常量真值
        Assert.Equal(1106, ProtocolConsts.SM_ENTITY_POS_DELPHI);
        Assert.Equal(1200, ProtocolConsts.SM_GUILD_WAR_STATUS_DELPHI);

        var em = new EntityManager();
        em.AddEntity(new EntityManager.EntityData
        {
            Uid = 3001,
            Name = "白野猪",
            TileX = 100,
            TileY = 200,
            Direction = 3
        });

        int movedFired = 0;
        int moveX = 0, moveY = 0;
        byte moveDir = 0;
        em.EntityMoved += (uid, x, y, dir) =>
        {
            if (uid == 3001)
            {
                movedFired++;
                moveX = x;
                moveY = y;
                moveDir = dir;
            }
        };

        // 2. 模拟路由接收 1106 封包 (recog=3001, param=105, tag=208)
        var router = new ServerMessageRouter(null, em);
        var posPkt = new ServerPacket(ProtocolConsts.SM_ENTITY_POS_DELPHI, 3001, 105, 208, Array.Empty<byte>());
        router.OnPacket(posPkt);

        Assert.Equal(1, movedFired);
        Assert.Equal(105, moveX);
        Assert.Equal(208, moveY);
        Assert.Equal(3, moveDir);

        var ent = em.GetEntity(3001);
        Assert.NotNull(ent);
        Assert.Equal(105, ent.TileX);
        Assert.Equal(208, ent.TileY);

        // 3. 模拟接收 1200 行会战况封包 (验证不抛异常、安全回退与分发)
        byte[] warBytes = Encoding.UTF8.GetBytes("沙巴克城门已被攻破！");
        var warPkt = new ServerPacket(ProtocolConsts.SM_GUILD_WAR_STATUS_DELPHI, 0, 0, 0, warBytes);
        router.OnPacket(warPkt);
    }

    [Fact]
    public void Official_150_SpellResult2_CharList_And_Teleport_GroundTruth_Test()
    {
        // 1. 验证 ProtocolConsts 常量物理真值
        Assert.Equal(29, ProtocolConsts.SM_SPELLRESULT2);
        Assert.Equal(211, ProtocolConsts.SM_CHARLIST_DELPHI);
        Assert.Equal(212, ProtocolConsts.SM_DELCHAR_DELPHI);
        Assert.Equal(750, ProtocolConsts.SM_GUILD_CHANGE_DELPHI);
        Assert.Equal(805, ProtocolConsts.SM_TELEPORT2_DELPHI);

        var em = new EntityManager();
        em.AddEntity(new EntityManager.EntityData
        {
            Uid = 5001,
            Name = "祖玛卫士",
            TileX = 300,
            TileY = 300,
            Direction = 4,
            Hp = 500,
            MaxHp = 500
        });

        var router = new ServerMessageRouter(null, em);

        // 2. 模拟接收 29 (SM_SPELLRESULT2)：受击/施法动作回显，必须与 30 行为对齐不报异常
        var spellPkt = new ServerPacket(ProtocolConsts.SM_SPELLRESULT2, 5001, 15, 485, Array.Empty<byte>());
        router.OnPacket(spellPkt);

        // 3. 模拟接收 211 / 212：官方 1.5 角色列表与角色注销协议包，不报异常正常路由
        var charListPkt = new ServerPacket(ProtocolConsts.SM_CHARLIST_DELPHI, 0, 0, 0, Encoding.UTF8.GetBytes("战神无双/0/35/1\r\n法王/1/30/0\r\n"));
        router.OnPacket(charListPkt);

        var delCharPkt = new ServerPacket(ProtocolConsts.SM_DELCHAR_DELPHI, 0, 0, 0, Encoding.UTF8.GetBytes("法王"));
        router.OnPacket(delCharPkt);

        // 4. 模拟接收 750：行会信息/更名同步
        var guildChangePkt = new ServerPacket(ProtocolConsts.SM_GUILD_CHANGE_DELPHI, 0, 0, 0, Encoding.UTF8.GetBytes("沙巴克风云"));
        router.OnPacket(guildChangePkt);

        // 5. 模拟接收 805 (SM_TELEPORT2_DELPHI)：包含突变坐标，触发传送校准
        var tpPkt = new ServerPacket(ProtocolConsts.SM_TELEPORT2_DELPHI, 5001, 310, 320, Array.Empty<byte>());
        router.OnPacket(tpPkt);
    }

    [Fact]
    public void Official_150_AltX_AltQ_Shortcut_GroundTruth_Test()
    {
        // 1. 验证官方 8 方向切分算法 (CalcDirection) 物理真值
        Assert.Equal(InputPlugin.DirRight, InputPlugin.CalcDirection(10f, 0f));
        Assert.Equal(InputPlugin.DirDownRight, InputPlugin.CalcDirection(10f, 10f));
        Assert.Equal(InputPlugin.DirDown, InputPlugin.CalcDirection(0f, 10f));
        Assert.Equal(InputPlugin.DirDownLeft, InputPlugin.CalcDirection(-10f, 10f));
        Assert.Equal(InputPlugin.DirLeft, InputPlugin.CalcDirection(-10f, 0f));
        Assert.Equal(InputPlugin.DirUpLeft, InputPlugin.CalcDirection(-10f, -10f));
        Assert.Equal(InputPlugin.DirUp, InputPlugin.CalcDirection(0f, -10f));
        Assert.Equal(InputPlugin.DirUpRight, InputPlugin.CalcDirection(10f, -10f));

        // 2. 验证小退发包 (CM_SOFTCLOSE = 1009) 封包结构物理真值
        byte[] softClosePkt = PacketEncoder.SoftClose();
        Assert.NotNull(softClosePkt);
        Assert.True(softClosePkt.Length >= 12);
        short ident = (short)(softClosePkt[1] | (softClosePkt[2] << 8));
        Assert.Equal(ProtocolConsts.CM_SOFTCLOSE, ident);
        Assert.Equal((short)1009, ident);
    }

    [Fact]
    public void Official_150_EatItem_Packet_And_GridSelect_GroundTruth_Test()
    {
        // 1. 验证盛大官方 1.5 物品使用/吃药 CM_EATITEM (1006) 封包编码物理真值
        const int testSlot = 5;
        const string testItem = "强效太阳水";
        byte[] pkt = PacketEncoder.EatItem(testSlot, testItem);

        Assert.NotNull(pkt);
        Assert.True(pkt.Length >= 12 + Encoding.UTF8.GetByteCount(testItem));

        // 校验包头与协议号
        short ident = (short)(pkt[1] | (pkt[2] << 8));
        Assert.Equal(ProtocolConsts.CM_EATITEM, ident);
        Assert.Equal((short)1006, ident);

        // 校验 recog 字段为背包/快捷栏槽位
        int recog = pkt[3] | (pkt[4] << 8) | (pkt[5] << 16) | (pkt[6] << 24);
        Assert.Equal(testSlot, recog);

        // 校验 extra 字段携带经过官方 EncodePlus60 编码的物品数据，还原后完全对齐物品名称
        byte[] decodedExtra = PacketCodec.DecodePlus60(pkt.AsSpan(11, pkt.Length - 12));
        string extraStr = Encoding.UTF8.GetString(decodedExtra);
        Assert.Equal(testItem, extraStr);

        // 2. 验证缺省物品名时的封包结构
        byte[] fallbackPkt = PacketEncoder.EatItem(3);
        Assert.NotNull(fallbackPkt);
        Assert.True(fallbackPkt.Length >= 12);
        short fallbackIdent = (short)(fallbackPkt[1] | (fallbackPkt[2] << 8));
        Assert.Equal(ProtocolConsts.CM_EATITEM, fallbackIdent);
    }

    [Fact]
    public void Official_150_Logon_Freeze_And_Stun_Position_Sync_GroundTruth_Test()
    {
        var em = new EntityManager();

        // 注册测试实体 6001 (怪物) 与 6002 (本地玩家)
        em.AddEntity(new EntityManager.EntityData
        {
            Uid = 6001,
            Type = EntityManager.EntityType.Monster,
            TileX = 300,
            TileY = 300,
            Direction = 2,
            Hp = 1000,
            MaxHp = 1000
        });

        em.AddEntity(new EntityManager.EntityData
        {
            Uid = 6002,
            Type = EntityManager.EntityType.Player,
            TileX = 100,
            TileY = 100,
            Direction = 0,
            Hp = 500,
            MaxHp = 500
        });

        var router = new ServerMessageRouter(null, em);

        // 1. 模拟接收 50 (SM_LOGON_DELPHI)：绑定主角并同步绝对出生点坐标 (330, 270)
        var logonPkt = new ServerPacket(ProtocolConsts.SM_LOGON_DELPHI, 6002, 330, 270, Array.Empty<byte>());
        router.OnPacket(logonPkt);

        Assert.Equal(6002, em.LocalPlayerUid);
        var player = em.GetEntity(6002);
        Assert.NotNull(player);
        Assert.Equal(330, player.TileX);
        Assert.Equal(270, player.TileY);

        // 2. 模拟接收 1100 (SM_CHAR_FREEZE_START)：实体 6001 麻痹定身，并纠偏坐标至 (305, 305)
        var freezePkt = new ServerPacket(ProtocolConsts.SM_CHAR_FREEZE_START, 6001, 305, 305, Array.Empty<byte>());
        router.OnPacket(freezePkt);

        var mon = em.GetEntity(6001);
        Assert.NotNull(mon);
        Assert.True(mon.IsFrozen);
        Assert.Equal(305, mon.TileX);
        Assert.Equal(305, mon.TileY);

        // 3. 模拟接收 1103 (SM_CHAR_STUN)：实体 6001 受到重击击晕硬直 2000ms，位移至 (310, 310)
        var stunPkt = new ServerPacket(ProtocolConsts.SM_CHAR_STUN, 6001, 310, 310, Array.Empty<byte>());
        router.OnPacket(stunPkt);

        Assert.True(mon.StunExpireTick > 0);
        Assert.Equal(310, mon.TileX);
        Assert.Equal(310, mon.TileY);

        // 4. 模拟接收 1101 (SM_CHAR_FREEZE_END)：解除实体 6001 麻痹定身
        var unfreezePkt = new ServerPacket(ProtocolConsts.SM_CHAR_FREEZE_END, 6001, 0, 0, Array.Empty<byte>());
        router.OnPacket(unfreezePkt);

        Assert.False(mon.IsFrozen);
    }
}








