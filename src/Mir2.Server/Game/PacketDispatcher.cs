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
    private readonly NinePatchAoi<ClientSession> _mapAoi;
    private readonly List<ClientSession> _ninePatchBuffer = new(64);

    public PacketDispatcher(NinePatchAoi<ClientSession> mapAoi)
    {
        _mapAoi = mapAoi;
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

            case ProtocolConsts.CM_SAY: // 3030 聊天通道
                HandleSay(session, remaining);
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
        session.CurrentMapId = 0; // 比奇省
        session.X = spawnX;
        session.Y = spawnY;
        session.State ??= new PlayerState();

        // 1. 注册到比奇省 AOI 空间九宫格
        _mapAoi.Enter(session, session.X, session.Y);

        // 2. 下发官方进入游戏核心响应: SM_LOGON_DELPHI (50)
        session.EnqueuePacket(ProtocolConsts.SM_LOGON_DELPHI, session.SessionId, session.X, session.Y, 0, session.CharacterName);

        // 3. 下发地图切换: SM_NEWMAP_DELPHI (521) / 比奇省 0, 坐标 (330, 270)
        session.EnqueuePacket(ProtocolConsts.SM_NEWMAP_DELPHI, 0, session.X, session.Y, 0, "比奇省");

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
            // 1. 地图边界物理约束 (比奇省 400x400)
            if (newX < 0 || newX >= 400 || newY < 0 || newY >= 400) return;

            int oldX = session.X;
            int oldY = session.Y;

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
            _mapAoi.UpdatePosition(session, oldX, oldY, newX, newY, out _, out _);

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

            // 向九宫格广播普通物理挥刀动作: SM_OBJECTHIT (242)
            BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTHIT, session.SessionId, session.X, session.Y, dir, session.CharacterName, includeSelf: false);
        }
    }

    private void HandleSay(ClientSession session, ReadOnlySpan<char> payload)
    {
        // 聊天广播到周围九宫格
        string msg = $"{session.CharacterName}: {payload}";
        BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTSAY, session.SessionId, session.X, session.Y, 0, msg, includeSelf: true);
    }

    /// <summary>
    /// 执行局部九宫格高效广播 (将包 Enqueue 到周围玩家的待合包缓冲池中，100ms 末尾统一冲刷)
    /// </summary>
    private void BroadcastToNinePatch(ClientSession source, short ident, int recog, int param, int tag, int series, string extra, bool includeSelf)
    {
        lock (_ninePatchBuffer)
        {
            _mapAoi.GetEntitiesInNinePatch(source.X, source.Y, _ninePatchBuffer);
            for (int i = 0; i < _ninePatchBuffer.Count; i++)
            {
                var target = _ninePatchBuffer[i];
                if (!includeSelf && target.SessionId == source.SessionId) continue;
                target.EnqueuePacket(ident, recog, param, tag, series, extra);
            }
        }
    }
}
