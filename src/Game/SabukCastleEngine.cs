// ============================================================================
// Mir2 客户端/服务端 - 盛大官方 1.5 沙巴克攻城战与皇宫争夺引擎 (1:1 官方实证)
// 官方实证：IDA 0x4C92B0（已证client无函数） / sub_4C93A0 (城门状态与碰撞) & sub_4C9B80 (皇宫占领判定)
// ============================================================================

using System;
using System.Collections.Generic;
using Godot;
using Mir2.Shared.Rules;

namespace Mir2Client.Game
{
    /// <summary>
    /// 盛大官方 1.5 沙巴克攻城战与皇宫争夺客户端驱动与表现引擎 (1:1 官方实证)
    /// 核心战斗与占领权更迭逻辑权威下沉至 Mir2.Shared.Rules.SabukCastleRulesEngine 与服务端 SabukSiegeEngine
    /// 客户端只负责地图瓦片阻挡动态开关与战争状态表现渲染
    /// </summary>
    public class SabukCastleEngine
    {
        public enum GateState
        {
            Intact = SabukCastleRulesEngine.GateState.Intact,   // 完好无损 (阻挡开启)
            Damaged = SabukCastleRulesEngine.GateState.Damaged,  // 遭受重创 (裂开，阻挡依然开启)
            Broken = SabukCastleRulesEngine.GateState.Broken    // 彻底倒塌 (阻挡消除，可自由穿行)
        }

        public enum WarPhase
        {
            Peace = SabukCastleRulesEngine.WarPhase.Peace,    // 和平期
            InWar = SabukCastleRulesEngine.WarPhase.InWar,    // 攻城战役激烈交火中 (官方 2 小时)
            Ended = SabukCastleRulesEngine.WarPhase.Ended     // 战役结算结束
        }

        /// <summary>
        /// 沙巴克城门管理 (客户端瓦片驱动层)
        /// </summary>
        public class SabukGate
        {
            public int MaxHp { get; set; } = SabukCastleRulesEngine.DefaultGateMaxHp;
            public int CurrentHp { get; private set; } = SabukCastleRulesEngine.DefaultGateMaxHp;
            public GateState State { get; private set; } = GateState.Intact;

            // 官方沙巴克城门占位瓦片 (盟重沙巴克正门 672, 331 及其两侧)
            public List<Vector2I> DoorTiles { get; } = new()
            {
                new Vector2I(672, 331),
                new Vector2I(672, 332),
                new Vector2I(672, 333)
            };

            public event Action<GateState>? GateStateChanged;
            public event Action<int, int>? GateHpChanged; // curHp, maxHp

            public void Initialize(int maxHp, IEnumerable<Vector2I>? customTiles = null)
            {
                MaxHp = maxHp;
                CurrentHp = maxHp;
                State = GateState.Intact;
                if (customTiles != null)
                {
                    DoorTiles.Clear();
                    DoorTiles.AddRange(customTiles);
                }
            }

            /// <summary>
            /// 攻击城门结算并动态消除阻挡 (权威规则委托给 SabukCastleRulesEngine)
            /// </summary>
            public GateState TakeDamage(int damage, Action<int, int, bool>? setTileBlocked = null)
            {
                if (State == GateState.Broken) return GateState.Broken;

                var (newHp, newState, broke, damaged) = SabukCastleRulesEngine.EvaluateGateDamage(
                    CurrentHp, MaxHp, (SabukCastleRulesEngine.GateState)State, damage);

                CurrentHp = newHp;
                GateHpChanged?.Invoke(CurrentHp, MaxHp);

                if (broke)
                {
                    State = GateState.Broken;
                    GateStateChanged?.Invoke(State);

                    // 官方权威机制：城门倒塌彻底消除物理阻挡！
                    if (setTileBlocked != null)
                    {
                        foreach (var tile in DoorTiles)
                        {
                            setTileBlocked(tile.X, tile.Y, false);
                        }
                    }
                }
                else if (damaged && State == GateState.Intact)
                {
                    State = GateState.Damaged;
                    GateStateChanged?.Invoke(State);
                }

                return State;
            }

            /// <summary>
            /// 沙巴克城主修缮城门并恢复物理阻挡
            /// </summary>
            public void RepairGate(Action<int, int, bool>? setTileBlocked = null)
            {
                CurrentHp = MaxHp;
                State = GateState.Intact;
                GateHpChanged?.Invoke(CurrentHp, MaxHp);
                GateStateChanged?.Invoke(State);

                // 重新建立物理阻挡
                if (setTileBlocked != null)
                {
                    foreach (var tile in DoorTiles)
                    {
                        setTileBlocked(tile.X, tile.Y, true);
                    }
                }
            }
        }

        /// <summary>
        /// 官方 1.5 皇宫争夺与占领裁决器 (权威下沉至 SabukCastleRulesEngine)
        /// </summary>
        public class PalaceWarDecider
        {
            public string LordGuild { get; private set; } = "沙巴克行会";
            public string OccupyingGuild { get; private set; } = string.Empty;
            public WarPhase Phase { get; private set; } = WarPhase.Peace;
            public float WarRemainingSec { get; private set; } = 0f;

            public event Action<string>? PalaceOccupantChanged; // guild
            public event Action<string>? WarFinalized;           // winnerGuild

            public void StartWar(float durationSec = 7200f) // 官方 2 小时 (7200 秒)
            {
                Phase = WarPhase.InWar;
                WarRemainingSec = durationSec;
                OccupyingGuild = LordGuild;
            }

            public void UpdateWarTimer(float dt)
            {
                if (Phase != WarPhase.InWar) return;

                WarRemainingSec -= dt;
                if (WarRemainingSec <= 0f)
                {
                    WarRemainingSec = 0f;
                    FinalizeWar();
                }
            }

            /// <summary>
            /// 判定皇宫大殿当前占领者 (权威委托给 SabukCastleRulesEngine)
            /// </summary>
            public bool EvaluatePalaceOccupancy(IEnumerable<(string guild, int uid)> playersInPalace)
            {
                var (changed, newOccupier) = SabukCastleRulesEngine.EvaluatePalaceOccupancy(
                    (SabukCastleRulesEngine.WarPhase)Phase, OccupyingGuild, playersInPalace);

                if (changed)
                {
                    OccupyingGuild = newOccupier;
                    PalaceOccupantChanged?.Invoke(OccupyingGuild);
                    return true;
                }

                return false;
            }

            /// <summary>
            /// 战役结束鸣金收兵，最终占领皇宫者加冕为新城主行会
            /// </summary>
            public string FinalizeWar()
            {
                Phase = WarPhase.Ended;
                if (!string.IsNullOrEmpty(OccupyingGuild))
                {
                    LordGuild = OccupyingGuild;
                }
                WarFinalized?.Invoke(LordGuild);
                return LordGuild;
            }
        }
    }
}
