// ============================================================================
// Mir2 客户端 - 沙巴克攻城战与皇宫争夺引擎单元测试 (1:1 官方实证)
// 官方实证：IDA sub_4C92B0 / sub_4C93A0 (城门状态与碰撞) & sub_4C9B80 (皇宫占领判定)
// ============================================================================

using System.Collections.Generic;
using Godot;
using Mir2Client.Game;
using Xunit;

namespace Mir2Client.Tests
{
    public class SabukCastleEngineTests
    {
        [Fact]
        public void Test_SabukGate_Damage_And_BlockRemoval()
        {
            var gate = new SabukCastleEngine.SabukGate();
            var customTiles = new List<Vector2I> { new(100, 100), new(100, 101) };
            gate.Initialize(1000, customTiles);

            var blockedMap = new Dictionary<Vector2I, bool>
            {
                { new(100, 100), true },
                { new(100, 101), true }
            };

            void SetTileBlocked(int x, int y, bool blocked)
            {
                blockedMap[new Vector2I(x, y)] = blocked;
            }

            Assert.Equal(SabukCastleEngine.GateState.Intact, gate.State);
            Assert.Equal(1000, gate.CurrentHp);

            // 承受 400 伤害 -> 剩余 600 HP (大于半血，依然 Intact)
            gate.TakeDamage(400, SetTileBlocked);
            Assert.Equal(SabukCastleEngine.GateState.Intact, gate.State);
            Assert.True(blockedMap[new Vector2I(100, 100)]);

            // 承受 200 伤害 -> 剩余 400 HP (小于等于半血，进入 Damaged)
            gate.TakeDamage(200, SetTileBlocked);
            Assert.Equal(SabukCastleEngine.GateState.Damaged, gate.State);
            Assert.True(blockedMap[new Vector2I(100, 100)]);

            // 致命 500 伤害 -> 彻底倒塌 Broken！
            gate.TakeDamage(500, SetTileBlocked);
            Assert.Equal(SabukCastleEngine.GateState.Broken, gate.State);
            Assert.Equal(0, gate.CurrentHp);

            // 验证核心终局：城门倒塌后阻挡必须被完全消除 (blocked = false)
            Assert.False(blockedMap[new Vector2I(100, 100)]);
            Assert.False(blockedMap[new Vector2I(100, 101)]);
        }

        [Fact]
        public void Test_SabukGate_Repair_RestoresBlock()
        {
            var gate = new SabukCastleEngine.SabukGate();
            var customTiles = new List<Vector2I> { new(50, 50) };
            gate.Initialize(500, customTiles);

            var blockedMap = new Dictionary<Vector2I, bool> { { new(50, 50), true } };
            void SetTileBlocked(int x, int y, bool blocked) => blockedMap[new Vector2I(x, y)] = blocked;

            // 打破城门
            gate.TakeDamage(600, SetTileBlocked);
            Assert.Equal(SabukCastleEngine.GateState.Broken, gate.State);
            Assert.False(blockedMap[new Vector2I(50, 50)]);

            // 城主修缮城门
            gate.RepairGate(SetTileBlocked);
            Assert.Equal(SabukCastleEngine.GateState.Intact, gate.State);
            Assert.Equal(500, gate.CurrentHp);
            Assert.True(blockedMap[new Vector2I(50, 50)]);
        }

        [Fact]
        public void Test_PalaceWarDecider_ContestedAndVictory()
        {
            var decider = new SabukCastleEngine.PalaceWarDecider();
            decider.StartWar(7200f);

            Assert.Equal(SabukCastleEngine.WarPhase.InWar, decider.Phase);
            Assert.Equal("沙巴克行会", decider.OccupyingGuild);

            // 1. 混战阶段：沙巴克行会与邪恶同盟两军在皇宫厮杀
            var contestedPlayers = new List<(string guild, int uid)>
            {
                ("沙巴克行会", 1001),
                ("邪恶同盟", 2001)
            };
            bool changed = decider.EvaluatePalaceOccupancy(contestedPlayers);
            Assert.False(changed); // 混战中不发生占领更替

            // 2. 散人搅局：只有邪恶同盟 + 1个无行会散人
            var scatterPlayers = new List<(string guild, int uid)>
            {
                ("邪恶同盟", 2001),
                ("", 3001)
            };
            changed = decider.EvaluatePalaceOccupancy(scatterPlayers);
            Assert.False(changed); // 依然混战

            // 3. 邪恶同盟彻底清场：皇宫内只有邪恶同盟成员
            var cleanPlayers = new List<(string guild, int uid)>
            {
                ("邪恶同盟", 2001),
                ("邪恶同盟", 2002)
            };
            changed = decider.EvaluatePalaceOccupancy(cleanPlayers);
            Assert.True(changed);
            Assert.Equal("邪恶同盟", decider.OccupyingGuild);

            // 4. 攻城战时间结束结算
            decider.UpdateWarTimer(7300f);
            Assert.Equal(SabukCastleEngine.WarPhase.Ended, decider.Phase);
            Assert.Equal("邪恶同盟", decider.LordGuild); // 正式加冕为新霸主！
        }
    }
}
