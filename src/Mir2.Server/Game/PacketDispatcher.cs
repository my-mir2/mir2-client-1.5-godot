using System;
using System.Collections.Generic;
using System.Text;
using Mir2.Server.Maps;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;
using Mir2.Shared.Rules;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.5 原版网络封包路由与分发中心 (M2 PacketDispatcher)
/// 严格 1:1 处理官方 CM_ 上行协议，并驱动服务端物理世界与九宫格 AOI 广播
/// </summary>
public sealed class PacketDispatcher
{
    private readonly MapManager _mapManager;
    private readonly List<ClientSession> _ninePatchBuffer = new(64);
    public MonsterRegenEngine? RegenEngine { get; set; }

    public PacketDispatcher(MapManager mapManager, MonsterRegenEngine? regenEngine = null)
    {
        _mapManager = mapManager;
        RegenEngine = regenEngine;
    }

    public PacketDispatcher(NinePatchAoi<ClientSession> mapAoi)
    {
        _mapManager = new MapManager();
        _mapManager.RegisterMap(new GameMap(0, "比奇省", mapAoi.Width, mapAoi.Height));
    }

    /// <summary>
    /// 处理客户端发来的单条原始封包 (格式: #IDENT/RECOG/PARAM/TAG/SERIES/EXTRA!)
    /// 兼容传统 ASCII 协议与二进制 6-Bit 混合帧
    /// </summary>
    public void Dispatch(ClientSession session, string rawPacket)
    {
        if (string.IsNullOrEmpty(rawPacket) || rawPacket.Length < 3) return;

        // 去除首尾 '#' 与 '!'
        ReadOnlySpan<char> span = rawPacket.AsSpan();
        if (span[0] == '#') span = span[1..];
        if (span.Length > 0 && span[^1] == '!') span = span[..^1];

        // 尝试按斜杠分词
        int slashIdx = span.IndexOf('/');
        if (slashIdx < 0) return;

        if (!short.TryParse(span[..slashIdx], out short ident)) return;

        ReadOnlySpan<char> remaining = span[(slashIdx + 1)..];

        switch (ident)
        {
            case ProtocolConsts.CM_QUERYVERSION: // 2001 客户端查询版本 / 握手
                HandleQueryVersion(session);
                break;

            case ProtocolConsts.CM_LOGIN: // 2 账号密码登录
                HandleLogin(session, remaining);
                break;

            case ProtocolConsts.CM_QUERYCHRINFO: // 119 查询角色列表
                HandleQueryCharInfo(session);
                break;

            case ProtocolConsts.CM_SELECTCHAR: // 103 选择角色进入游戏
                HandleSelectChar(session, remaining);
                break;

            case ProtocolConsts.CM_TURNDIRECTION: // 3010 转向
                HandleTurn(session, remaining);
                break;

            case ProtocolConsts.CM_WALK: // 3011 行走 (1格)
                HandleMove(session, remaining, isRun: false);
                break;

            case ProtocolConsts.CM_RUN: // 3013 跑步 (2格)
                HandleMove(session, remaining, isRun: true);
                break;

            case ProtocolConsts.CM_HIT: // 3014 物理攻击
                HandleHit(session, remaining);
                break;

            case ProtocolConsts.CM_SPELL: // 3017 技能施法
                HandleSpell(session, remaining);
                break;

            case ProtocolConsts.CM_SAY: // 3030 聊天通道
                HandleSay(session, remaining);
                break;

            case ProtocolConsts.CM_PICKUP: // 1001 拾取地表物品 (IDA 0x4C5CB0)
                HandlePickUp(session);
                break;

            default:
                // 未知或扩展协议默认回显记录
                break;
        }
    }

    private void HandleQueryVersion(ClientSession session)
    {
        // 官方 1.5 握手认证通过: SM_PASSOK (500)
        session.EnqueuePacket(ProtocolConsts.SM_PASSOK, session.SessionId, 0, 0, 0, "盛大官方1.5原版引擎就绪");
    }

    private void HandleLogin(ClientSession session, ReadOnlySpan<char> payload)
    {
        // 格式: 账号/密码
        int slash = payload.IndexOf('/');
        string account = slash > 0 ? payload[..slash].ToString() : payload.ToString();
        session.AccountName = account;

        // 回复登录成功: SM_LOGINSUCCESS (530)
        session.EnqueuePacket(ProtocolConsts.SM_LOGINSUCCESS, session.SessionId, 0, 0, 0, account);
    }

    private void HandleQueryCharInfo(ClientSession session)
    {
        // 默认模拟返回 1 个 1.5 原版角色: 战士, 等级 35, 男
        string charInfo = $"{session.AccountName}_战神/0/35/0/0/0";
        session.EnqueuePacket(ProtocolConsts.SM_QUERYCHAR_OK, 1, 0, 0, 0, charInfo);
    }

    private void HandleSelectChar(ClientSession session, ReadOnlySpan<char> payload)
    {
        string name = payload.ToString();
        int spawnX = 330;
        int spawnY = 270;

        int slash1 = payload.IndexOf('/');
        if (slash1 > 0)
        {
            name = payload[..slash1].ToString();
            ReadOnlySpan<char> rest = payload[(slash1 + 1)..];
            int slash2 = rest.IndexOf('/');
            if (slash2 > 0 && int.TryParse(rest[..slash2], out int px) && int.TryParse(rest[(slash2 + 1)..], out int py))
            {
                spawnX = px;
                spawnY = py;
            }
        }

        session.CharacterName = name;
        session.CharacterName = name;
        session.CurrentMapId = 0; // 比奇省
        session.X = spawnX;
        session.Y = spawnY;
        session.State ??= new PlayerState();

        var map = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;

        // 1. 注册到当前地图 AOI 空间九宫格
        map.Aoi.Enter(session, session.X, session.Y);

        // 2. 下发官方进入游戏核心响应: SM_LOGON_DELPHI (50)
        session.EnqueuePacket(ProtocolConsts.SM_LOGON_DELPHI, session.SessionId, session.X, session.Y, 0, session.CharacterName);

        // 3. 下发地图切换: SM_NEWMAP_DELPHI (521) / 比奇省 0, 坐标 (330, 270)
        session.EnqueuePacket(ProtocolConsts.SM_NEWMAP_DELPHI, map.MapId, session.X, session.Y, 0, map.Name);

        // 4. 下发自身属性面板: SM_MYSTATUS (52)
        session.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, session.State.Hp, session.State.MaxHp, session.State.Mp, session.State.MaxMp);

        // 5. 在九宫格内向其他玩家广播该玩家登场生成: SM_SENDUSERSTATE (250)
        BroadcastToNinePatch(session, ProtocolConsts.SM_SENDUSERSTATE, session.SessionId, session.X, session.Y, 0, session.CharacterName, includeSelf: false);
    }

    private void HandleTurn(ClientSession session, ReadOnlySpan<char> payload)
    {
        // payload 包含朝向 (0..7)
        if (int.TryParse(payload, out int dir))
        {
            // 向九宫格广播转向: SM_SENDUSERSTATE (250)
            BroadcastToNinePatch(session, ProtocolConsts.SM_SENDUSERSTATE, session.SessionId, session.X, session.Y, dir, session.CharacterName, includeSelf: false);
        }
    }

    private void HandleMove(ClientSession session, ReadOnlySpan<char> payload, bool isRun)
    {
        // 格式: 目标X/目标Y/朝向
        int slash1 = payload.IndexOf('/');
        if (slash1 < 0) return;
        ReadOnlySpan<char> xSpan = payload[..slash1];
        ReadOnlySpan<char> rest = payload[(slash1 + 1)..];
        int slash2 = rest.IndexOf('/');
        if (slash2 < 0) return;
        ReadOnlySpan<char> ySpan = rest[..slash2];
        ReadOnlySpan<char> dirSpan = rest[(slash2 + 1)..];

        if (int.TryParse(xSpan, out int newX) &&
            int.TryParse(ySpan, out int newY) &&
            int.TryParse(dirSpan, out int dir))
        {
            int oldX = session.X;
            int oldY = session.Y;

            var map = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;

            // 1. 地图物理边界与碰撞阻挡判定 (IsWalkable 包含超界越境与水面/城墙障碍检测)
            if (!map.IsWalkable(newX, newY))
            {
                // 撞墙或穿墙外挂，立即下发坐标拉回包
                session.EnqueuePacket(ProtocolConsts.SM_SENDUSERSTATE, session.SessionId, oldX, oldY, dir, session.CharacterName);
                return;
            }

            // 2. 步长与防瞬移物理校验 (行走限 1 格，跑步限 2 格)
            int maxDist = isRun ? 2 : 1;
            int deltaX = Math.Abs(newX - oldX);
            int deltaY = Math.Abs(newY - oldY);
            if (deltaX > maxDist || deltaY > maxDist)
            {
                // 非法瞬移拉回当前坐标: SM_SENDUSERSTATE (250)
                session.EnqueuePacket(ProtocolConsts.SM_SENDUSERSTATE, session.SessionId, oldX, oldY, dir, session.CharacterName);
                return;
            }

            // 3. 移动速度反外挂检测 (最小间隔校验)
            long nowMs = Environment.TickCount64;
            if (session.LastMoveTimeMs > 0)
            {
                var (isSpeedHack, shouldRollback) = EconomySecurityEngine.SpeedHackDetectorEngine.CheckMovePacket(nowMs, session.LastMoveTimeMs);
                if (isSpeedHack && shouldRollback)
                {
                    // 移动过快判定外挂拉回
                    session.EnqueuePacket(ProtocolConsts.SM_SENDUSERSTATE, session.SessionId, oldX, oldY, dir, session.CharacterName);
                    return;
                }
            }
            session.LastMoveTimeMs = nowMs;

            // 更新实体坐标
            session.X = newX;
            session.Y = newY;

            // 更新九宫格 AOI 空间分区
            map.Aoi.UpdatePosition(session, oldX, oldY, newX, newY, out _, out _);

            // 广播移动数据帧: 行走 SM_OBJECTWALK (241) / 跑步 SM_OBJECTRUN (240)
            short smCode = isRun ? ProtocolConsts.SM_OBJECTRUN : ProtocolConsts.SM_OBJECTWALK;
            BroadcastToNinePatch(session, smCode, session.SessionId, newX, newY, dir, session.CharacterName, includeSelf: false);
        }
    }

    private void HandleHit(ClientSession session, ReadOnlySpan<char> payload)
    {
        // 格式: 目标X/目标Y/朝向 (CM_HIT)
        int slash1 = payload.IndexOf('/');
        if (slash1 < 0) return;
        ReadOnlySpan<char> xSpan = payload[..slash1];
        ReadOnlySpan<char> rest = payload[(slash1 + 1)..];
        int slash2 = rest.IndexOf('/');
        if (slash2 < 0) return;
        ReadOnlySpan<char> ySpan = rest[..slash2];
        ReadOnlySpan<char> dirSpan = rest[(slash2 + 1)..];

        if (int.TryParse(xSpan, out int targetX) &&
            int.TryParse(ySpan, out int targetY) &&
            int.TryParse(dirSpan, out int dir))
        {
            long nowMs = Environment.TickCount64;
            // 攻击速度校验 (间隔必须 >= 680ms)
            if (session.LastAttackTimeMs > 0 && !EconomySecurityEngine.AttackSpeedValidatorEngine.IsAttackValid(nowMs, session.LastAttackTimeMs))
            {
                return; // 挥刀过快拦截
            }
            session.LastAttackTimeMs = nowMs;

            // 1. 向九宫格广播普通物理挥刀动作: SM_OBJECTHIT (242)
            BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTHIT, session.SessionId, session.X, session.Y, dir, session.CharacterName, includeSelf: false);

            // 2. 武器开刀试刀判定
            session.State?.TrialSlashEquippedWeapon();

            // 3. 在九宫格同屏检索攻击目标 (坐标处于 targetX, targetY)
            ClientSession? defender = null;
            var hitMap = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;
            lock (_ninePatchBuffer)
            {
                hitMap.Aoi.GetEntitiesInNinePatch(session.X, session.Y, _ninePatchBuffer);
                for (int i = 0; i < _ninePatchBuffer.Count; i++)
                {
                    var ent = _ninePatchBuffer[i];
                    if (ent.SessionId != session.SessionId && ent.X == targetX && ent.Y == targetY)
                    {
                        defender = ent;
                        break;
                    }
                }
            }

            if (defender != null && defender.State != null && session.State != null)
            {
                // 1. 官方 PK 模式规则判定 (0x4D1E54 btAttackMode)
                // 模式 1: [Mode: Peaceful] 和平模式，禁止攻击白名玩家
                if (session.State.AttackMode == 1 && !defender.State.IsRedName && !defender.State.IsYellowName)
                {
                    return; // 和平模式受限
                }

                // 2. 物理命中判定 (1:1 官方 Delphi M2 sub_4C1EF4 @ L12704: RandInt(Defender.Speed) >= Attacker.Hit)
                int rollSpeed = OfficialClientRulesEngine.DelphiRandom.Shared.Next(defender.State.Speed + 1);
                bool hit = rollSpeed < session.State.Hit;

                if (hit)
                {
                    // 物理基础伤害精算
                    int rawDmg = OfficialClientRulesEngine.CombatMath.CalculatePhysicalDamage(15, 30, 0, 5, OfficialClientRulesEngine.DelphiRandom.Shared);

                    // 攻杀剑术附加伤害 (0x4C1EF4 @ L12621)
                    if (session.State.BoAttackKill)
                    {
                        session.State.BoAttackKill = false;
                        rawDmg += session.State.HitPlus;
                    }

                    // 烈火剑法火焰爆发 (0x4C1EF4 @ L12627: ROUND(Damage / 100.0 * 10 * FireHitPlus))
                    if (session.State.BoFireHit)
                    {
                        session.State.BoFireHit = false;
                        int multiplier = 10 * session.State.FireHitPlus;
                        rawDmg += (int)Math.Round((double)rawDmg / 100.0 * multiplier);
                    }

                    // 虹魔吸血判定 (0x4C1EF4 @ L12733: LeechWeight / 100.0)
                    session.State.ProcessAttackLifesteal(rawDmg);

                    // 麻痹戒指判定 (0x4C1EF4 @ L12729: HasParalysisRing && RandInt(AntiPoison + 5) == 0)
                    if (session.State.HasParalysisRing)
                    {
                        int rollPara = OfficialClientRulesEngine.DelphiRandom.Shared.Next(defender.State.AntiPoison + 5);
                        if (rollPara == 0)
                        {
                            defender.State.StatusBits[5] = true;
                        }
                    }

                    // 伤害扣减 (含魔法盾、护身、复活戒指)
                    var (finalHp, finalMp, died, revived, absorbed, shieldBroken) = defender.State.TakeCombatDamageFull(rawDmg, nowMs);

                    // 受击后仰广播: SM_OBJECTSTRUCK (244)
                    BroadcastToNinePatch(defender, ProtocolConsts.SM_OBJECTSTRUCK, defender.SessionId, defender.X, defender.Y, rawDmg, defender.CharacterName, includeSelf: true);

                    // 目标血量更新
                    defender.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, defender.State.Hp, defender.State.MaxHp, defender.State.Mp, defender.State.MaxMp);

                    // 死亡倒地动画广播: SM_DEATH_ACTION_DELPHI (28)
                    if (died)
                    {
                        session.State?.AddPkMurder();
                        BroadcastToNinePatch(defender, ProtocolConsts.SM_DEATH_ACTION_DELPHI, defender.SessionId, defender.X, defender.Y, 0, defender.CharacterName, includeSelf: true);

                        // 官方死亡爆装与爆背包结算 (IDA 0x4BB44C / 0x4BB6C8)
                        var dropped = MonsterDropEngine.ExecuteDeathDrop(defender.State, OfficialClientRulesEngine.DelphiRandom.Shared);
                        for (int d = 0; d < dropped.Count; d++)
                        {
                            var drop = dropped[d];
                            hitMap.AddGroundItem(new GroundItemEntity
                            {
                                Uid = drop.ItemUid,
                                Name = drop.ItemName,
                                X = defender.X,
                                Y = defender.Y,
                                OwnerSessionId = session.SessionId, // 杀手独享 80 秒拾取保护权 (IDA 0x4C5CB0)
                                DropTickMs = nowMs,
                                Weight = 1
                            });
                            BroadcastToNinePatch(defender, ProtocolConsts.SM_GROUNDITEM, drop.ItemUid, defender.X, defender.Y, 0, drop.ItemName, includeSelf: true);
                        }
                    }
                }
            }
            else if (RegenEngine != null && session.State != null)
            {
                // 4. 检索目标位置的怪物并进行物理攻击与掉落结算
                var monster = RegenEngine.FindMonsterAt(session.CurrentMapId, targetX, targetY);
                if (monster != null && !monster.IsDead)
                {
                    // 物理命中判定 (1:1 官方 Delphi M2 sub_4C1EF4 @ L12704)
                    int rollSpeed = OfficialClientRulesEngine.DelphiRandom.Shared.Next(16);
                    if (rollSpeed < session.State.Hit)
                    {
                        int rawDmg = OfficialClientRulesEngine.CombatMath.CalculatePhysicalDamage(15, 30, 0, 5, OfficialClientRulesEngine.DelphiRandom.Shared);
                        if (session.State.BoAttackKill)
                        {
                            session.State.BoAttackKill = false;
                            rawDmg += session.State.HitPlus;
                        }
                        if (session.State.BoFireHit)
                        {
                            session.State.BoFireHit = false;
                            int multiplier = 10 * session.State.FireHitPlus;
                            rawDmg += (int)Math.Round((double)rawDmg / 100.0 * multiplier);
                        }

                        monster.Hp = Math.Max(0, monster.Hp - rawDmg);

                        // 广播怪物受击后仰: SM_OBJECTSTRUCK (244)
                        BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTSTRUCK, monster.Uid, monster.X, monster.Y, rawDmg, monster.Name, includeSelf: true);

                        if (monster.IsDead)
                        {
                            RegenEngine.OnMonsterDied(monster.Uid);

                            // 广播怪物死亡倒地: SM_DEATH_ACTION_DELPHI (28)
                            BroadcastToNinePatch(session, ProtocolConsts.SM_DEATH_ACTION_DELPHI, monster.Uid, monster.X, monster.Y, 0, monster.Name, includeSelf: true);

                            // 怪物死亡爆落战利品
                            var loot = new GroundItemEntity
                            {
                                Uid = 50000 + monster.Uid % 10000,
                                Name = $"{monster.Name}_战利品",
                                X = monster.X,
                                Y = monster.Y,
                                OwnerSessionId = session.SessionId, // 击杀者 80 秒归属权
                                DropTickMs = nowMs,
                                Weight = 1
                            };
                            hitMap.AddGroundItem(loot);
                            BroadcastToNinePatch(session, ProtocolConsts.SM_GROUNDITEM, loot.Uid, monster.X, monster.Y, 0, loot.Name, includeSelf: true);
                        }
                    }
                }
            }
        }
    }

    private void HandleSpell(ClientSession session, ReadOnlySpan<char> payload)
    {
        if (session.State == null) return;

        // 格式: targetUid/magicId/targetX/targetY (CM_SPELL 3017)
        int slash1 = payload.IndexOf('/');
        if (slash1 < 0) return;
        ReadOnlySpan<char> uidSpan = payload[..slash1];
        ReadOnlySpan<char> rest1 = payload[(slash1 + 1)..];

        int slash2 = rest1.IndexOf('/');
        if (slash2 < 0) return;
        ReadOnlySpan<char> magicSpan = rest1[..slash2];
        ReadOnlySpan<char> rest2 = rest1[(slash2 + 1)..];

        int slash3 = rest2.IndexOf('/');
        if (slash3 < 0) return;
        ReadOnlySpan<char> xSpan = rest2[..slash3];
        ReadOnlySpan<char> ySpan = rest2[(slash3 + 1)..];

        if (int.TryParse(uidSpan, out int targetUid) &&
            short.TryParse(magicSpan, out short magicId) &&
            int.TryParse(xSpan, out int targetX) &&
            int.TryParse(ySpan, out int targetY))
        {
            long nowMs = Environment.TickCount64;
            // 施法公共CD校验 (500ms)
            if (session.LastSpellTimeMs > 0 && (nowMs - session.LastSpellTimeMs < 500))
            {
                return;
            }
            session.LastSpellTimeMs = nowMs;

            var skill = SkillFormulas.GetSkill(magicId);
            int mpCost = skill?.MpCost ?? 10;
            if (session.State.Mp < mpCost)
            {
                return; // 蓝量不足
            }

            session.State.Mp -= mpCost;
            session.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, session.State.Hp, session.State.MaxHp, session.State.Mp, session.State.MaxMp);

            // 向九宫格广播施法动作: SM_OBJECTSPELL (243)
            BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTSPELL, session.SessionId, session.X, session.Y, magicId, session.CharacterName, includeSelf: true);

            // 特殊技能分支
            if (magicId == 31) // 魔法盾
            {
                session.State.ActivateMagicShield(1, 30);
                return;
            }
            if (magicId == 2 || magicId == 29) // 治愈术 / 群体治愈术
            {
                session.State.Hp = Math.Min(session.State.MaxHp, session.State.Hp + 25);
                session.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, session.State.Hp, session.State.MaxHp, session.State.Mp, session.State.MaxMp);
                return;
            }

            // 伤害性法术 (雷电术=10, 火球术=1, 灵魂火符=13 等)
            ClientSession? defender = null;
            var spellMap = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;
            lock (_ninePatchBuffer)
            {
                spellMap.Aoi.GetEntitiesInNinePatch(session.X, session.Y, _ninePatchBuffer);
                for (int i = 0; i < _ninePatchBuffer.Count; i++)
                {
                    var ent = _ninePatchBuffer[i];
                    if (ent.SessionId != session.SessionId && (ent.SessionId == targetUid || (ent.X == targetX && ent.Y == targetY)))
                    {
                        defender = ent;
                        break;
                    }
                }
            }

            if (defender != null && defender.State != null)
            {
                int magicDmg = OfficialClientRulesEngine.CombatMath.CalculateMagicDamage(20, 35, 0, 5, OfficialClientRulesEngine.DelphiRandom.Shared);
                var (finalHp, finalMp, died, revived, absorbed, shieldBroken) = defender.State.TakeCombatDamageFull(magicDmg, nowMs);

                // 受击后仰广播: SM_OBJECTSTRUCK (244)
                BroadcastToNinePatch(defender, ProtocolConsts.SM_OBJECTSTRUCK, defender.SessionId, defender.X, defender.Y, magicDmg, defender.CharacterName, includeSelf: true);

                // 同步目标血量
                defender.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, defender.State.Hp, defender.State.MaxHp, defender.State.Mp, defender.State.MaxMp);

                if (died)
                {
                    session.State.AddPkMurder();
                    BroadcastToNinePatch(defender, ProtocolConsts.SM_DEATH_ACTION_DELPHI, defender.SessionId, defender.X, defender.Y, 0, defender.CharacterName, includeSelf: true);

                    // 官方死亡爆装与爆背包结算 (IDA 0x4BB44C / 0x4BB6C8)
                    var dropped = MonsterDropEngine.ExecuteDeathDrop(defender.State, OfficialClientRulesEngine.DelphiRandom.Shared);
                    for (int d = 0; d < dropped.Count; d++)
                    {
                        var drop = dropped[d];
                        spellMap.AddGroundItem(new GroundItemEntity
                        {
                            Uid = drop.ItemUid,
                            Name = drop.ItemName,
                            X = defender.X,
                            Y = defender.Y,
                            OwnerSessionId = session.SessionId, // 杀手独享 80 秒拾取保护权 (IDA 0x4C5CB0)
                            DropTickMs = nowMs,
                            Weight = 1
                        });
                        BroadcastToNinePatch(defender, ProtocolConsts.SM_GROUNDITEM, drop.ItemUid, defender.X, defender.Y, 0, drop.ItemName, includeSelf: true);
                    }
                }
            }
        }
    }

    private void HandleSay(ClientSession session, ReadOnlySpan<char> payload)
    {
        // 1. 官方控制指令判断 (IDA 0x4D1E54 TPlayObject.Command)
        if (payload.Equals("@AttackMode", StringComparison.OrdinalIgnoreCase))
        {
            if (session.State != null)
            {
                session.State.AttackMode = (byte)(session.State.AttackMode >= 4 ? 0 : session.State.AttackMode + 1);
                string modeStr = session.State.AttackMode switch
                {
                    0 => "[Mode: Attack All]",
                    1 => "[Mode: Peaceful]",
                    2 => "[Mode: Group Attack]",
                    3 => "[Mode: Guild Attack]",
                    4 => "[Mode: Red/White Name]",
                    _ => "[Mode: Attack All]"
                };
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, modeStr);
            }
            return;
        }

        // 2. 普通聊天广播到周围九宫格
        string msg = $"{session.CharacterName}: {payload}";
        BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTSAY, session.SessionId, session.X, session.Y, 0, msg, includeSelf: true);
    }

    /// <summary>
    /// 盛大官方 1.4/1.5 拾取地表物品权威逻辑 (IDA 0x4C5CB0 TCreature.PickUp)
    /// 包含死亡状态校验、80秒归属保护期、背包40格满仓检测、负重上限校验
    /// </summary>
    private void HandlePickUp(ClientSession session)
    {
        if (session.State == null || session.State.Hp <= 0) return; // 死亡不可拾取 (IDA 0x4C5CB0 @ L889)

        var map = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;
        long nowMs = Environment.TickCount64;

        // 尝试拾取玩家当前脚下 (session.X, session.Y) 的物品
        var (success, groundItem, errorMsg) = map.TryPickGroundItem(session.X, session.Y, session.SessionId, nowMs);
        if (!success || groundItem == null)
        {
            if (!string.IsNullOrEmpty(errorMsg))
            {
                // 发送官方系统提示消息
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, errorMsg);
            }
            return;
        }

        // 1. 金币拾取处理
        if (groundItem.IsGold)
        {
            session.State.Gold += groundItem.Count;
            // 广播地表物品消失: SM_DELGROUNDITEM (315)
            BroadcastToNinePatch(session, ProtocolConsts.SM_DELGROUNDITEM, groundItem.Uid, groundItem.X, groundItem.Y, 0, groundItem.Name, includeSelf: true);
            // 刷新金币
            session.EnqueuePacket(ProtocolConsts.SM_GOLDCHANGED_DELPHI, session.State.Gold, 0, 0, 0);
            return;
        }

        // 2. 普通装备/物品拾取处理
        // 2.1 检查背包格子上限 (官方 40 格)
        if (session.State.BagItems.Count >= 40)
        {
            // 背包已满，放回地面
            map.AddGroundItem(groundItem);
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "背包已满，无法拾取！");
            return;
        }

        // 2.2 检查背包负重 (IDA 0x4C5CB0 @ L913)
        if (session.State.BagWeight + groundItem.Weight > session.State.MaxBagWeight)
        {
            // 超过负重，放回地面
            map.AddGroundItem(groundItem);
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "负重不足，无法拾取！");
            return;
        }

        // 2.3 入包结算
        session.State.BagItems.Add(new EquipItem
        {
            Uid = groundItem.Uid,
            Name = groundItem.Name,
            Weight = groundItem.Weight
        });
        session.State.BagWeight += groundItem.Weight;

        // 2.4 九宫格广播地表物品消失: SM_DELGROUNDITEM (315)
        BroadcastToNinePatch(session, ProtocolConsts.SM_DELGROUNDITEM, groundItem.Uid, groundItem.X, groundItem.Y, 0, groundItem.Name, includeSelf: true);

        // 2.5 下发物品入包通知: SM_ADDITEM (200)
        session.EnqueuePacket(ProtocolConsts.SM_ADDITEM, groundItem.Uid, groundItem.Weight, 0, 0, groundItem.Name);
    }

    /// <summary>
    /// 执行局部九宫格高效广播 (将包 Enqueue 到周围玩家的待合包缓冲池中，100ms 末尾统一冲刷)
    /// </summary>
    private void BroadcastToNinePatch(ClientSession source, short ident, int recog, int param, int tag, int series, string extra, bool includeSelf)
    {
        var map = _mapManager.GetMap(source.CurrentMapId) ?? _mapManager.DefaultMap;
        lock (_ninePatchBuffer)
        {
            map.Aoi.GetEntitiesInNinePatch(source.X, source.Y, _ninePatchBuffer);
            for (int i = 0; i < _ninePatchBuffer.Count; i++)
            {
                var target = _ninePatchBuffer[i];
                if (!includeSelf && target.SessionId == source.SessionId) continue;
                target.EnqueuePacket(ident, recog, param, tag, series, extra);
            }
        }
    }

    /// <summary>
    /// 驱动一次怪物 AI 行为并在九宫格内向玩家广播其动作 (移动/近战/巡逻)
    /// </summary>
    public int ProcessMonsterAiCycle(OfficialClientRulesEngine.DelphiRandom rng)
    {
        if (RegenEngine == null) return 0;

        var (actions, _) = RegenEngine.ProcessMonsterAiTick((mapId, x, y) =>
        {
            var map = _mapManager.GetMap(mapId) ?? _mapManager.DefaultMap;
            var list = new List<ClientSession>();
            map.Aoi.GetEntitiesInNinePatch(x, y, list);
            return list;
        }, rng);

        for (int i = 0; i < actions.Count; i++)
        {
            var act = actions[i];
            var map = _mapManager.GetMap(0);
            if (map == null) continue;

            if (act.ActionType == 0 || act.ActionType == 2)
            {
                // 怪物移动广播: SM_OBJECTWALK (241)
                BroadcastToCoords(map, act.X, act.Y, ProtocolConsts.SM_OBJECTWALK, act.MonsterUid, act.X, act.Y, act.Direction, act.MonsterName);
            }
            else if (act.ActionType == 1)
            {
                // 怪物近战攻击动作: SM_OBJECTHIT (242)
                BroadcastToCoords(map, act.X, act.Y, ProtocolConsts.SM_OBJECTHIT, act.MonsterUid, act.X, act.Y, act.Direction, act.MonsterName);

                // 目标玩家受击后仰: SM_OBJECTSTRUCK (244)
                BroadcastToCoords(map, act.X, act.Y, ProtocolConsts.SM_OBJECTSTRUCK, act.TargetSessionId, act.X, act.Y, act.DamageDealt, act.MonsterName);
            }
        }

        return actions.Count;
    }

    private void BroadcastToCoords(GameMap map, int x, int y, short ident, int recog, int param, int tag, int series, string extra)
    {
        lock (_ninePatchBuffer)
        {
            map.Aoi.GetEntitiesInNinePatch(x, y, _ninePatchBuffer);
            for (int i = 0; i < _ninePatchBuffer.Count; i++)
            {
                _ninePatchBuffer[i].EnqueuePacket(ident, recog, param, tag, series, extra);
            }
        }
    }
}
