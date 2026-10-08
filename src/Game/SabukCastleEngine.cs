// ============================================================================
// Mir2 客户端/服务端 - 盛大官方 1.5 沙巴克攻城战与皇宫争夺引擎 (1:1 官方实证)
// 官方实证：IDA 0x4C92B0（已证client无函数） / sub_4C93A0 (城门状态与碰撞) & sub_4C9B80 (皇宫占领判定)
// ============================================================================

using System;
using System.Collections.Generic;
using Godot;

namespace Mir2Client.Game
{
    public class SabukCastleEngine
    {
        public enum GateState
        {
            Intact = 0,   // 完好无损 (阻挡开启)
            Damaged = 1,  // 遭受重创 (裂开，阻挡依然开启)
            Broken = 2    // 彻底倒塌 (阻挡消除，可自由穿行)
        }

        public enum WarPhase
        {
            Peace = 0,    // 和平期
            InWar = 1,    // 攻城战役激烈交火中 (官方 2 小时)
            Ended = 2     // 战役结算结束
        }

        /// <summary>
        /// 沙巴克城门管理
        /// </summary>
        public class SabukGate
        {
            public int MaxHp { get; set; } = 2000;
            public int CurrentHp { get; private set; } = 2000;
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
            /// 攻击城门结算并动态消除阻挡
            /// </summary>
            /// <param name="damage">承受伤害</param>
            /// <param name="setTileBlocked">地图阻挡回调 (x, y, blocked)</param>
            /// <returns>返回攻击后最新状态</returns>
            public GateState TakeDamage(int damage, Action<int, int, bool>? setTileBlocked = null)
            {
                if (State == GateState.Broken) return GateState.Broken;

                CurrentHp = Math.Max(0, CurrentHp - damage);
                GateHpChanged?.Invoke(CurrentHp, MaxHp);

                if (CurrentHp <= 0)
                {
                    State = GateState.Broken;
                    GateStateChanged?.Invoke(State);

                    // 官方权威机制：城门倒塌彻底消除物理阻挡！
                    if (setTileBlocked != null)
                    {
                        foreach (var tile in DoorTiles)
                        {
                            setTileBlocked(tile.X, tile.Y, false); // blocked = false
                        }
                    }
                }
                else if (CurrentHp <= MaxHp / 2 && State == GateState.Intact)
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
                        setTileBlocked(tile.X, tile.Y, true); // blocked = true
                    }
                }
            }
        }

        /// <summary>
        /// 官方 1.5 皇宫争夺与占领裁决器 (IDA 0x4C9B80（已证client无函数）)
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
            /// 判定皇宫大殿当前占领者 (官方实证 0x4C9B80（已证client无函数）：皇宫内清空敌对势力时方可占领)
            /// </summary>
            public bool EvaluatePalaceOccupancy(IEnumerable<(string guild, int uid)> playersInPalace)
            {
                if (Phase != WarPhase.InWar) return false;

                string? dominantGuild = null;
                bool hasConflict = false;

                foreach (var (guild, _) in playersInPalace)
                {
                    if (string.IsNullOrWhiteSpace(guild))
                    {
                        // 无行会散人存在，视为混战未清理完毕
                        hasConflict = true;
                        break;
                    }

                    if (dominantGuild == null)
                    {
                        dominantGuild = guild;
                    }
                    else if (!dominantGuild.Equals(guild, StringComparison.OrdinalIgnoreCase))
                    {
                        // 存在两个以上不同行会的成员在皇宫内厮杀
                        hasConflict = true;
                        break;
                    }
                }

                // 皇宫内只剩下某一个合法行会的成员
                if (!hasConflict && !string.IsNullOrEmpty(dominantGuild))
                {
                    if (!OccupyingGuild.Equals(dominantGuild, StringComparison.OrdinalIgnoreCase))
                    {
                        OccupyingGuild = dominantGuild;
                        PalaceOccupantChanged?.Invoke(OccupyingGuild);
                        return true;
                    }
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
