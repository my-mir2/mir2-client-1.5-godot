// ============================================================================
// Mir2 客户端 - 精灵动作与物理帧引擎 (1:1 盛大 1.5 官方实证)
// 官方实证：IDA sub_495EBC & sub_496120 & Hum/Mon 物理帧结构
// ============================================================================

using System;
using System.Collections.Generic;

namespace Mir2Client.Render
{
    public enum ActionType
    {
        Stand = 0,
        Walk = 1,
        Run = 2,
        Hit = 3,
        HeavyHit = 4,
        BigHit = 5,
        PowerHit = 6,
        LongHit = 7,
        FireHit = 8,
        CrsHit = 9,
        Cast = 10,
        Struck = 11,
        Die = 12,
        SitDown = 13
    }

    public static class SpriteEngine
    {
        public const int HumDressStep = 600;
        public const int WeaponStep = 600;
        public const int HairStep = 600;
        public const int NpcStep = 10;
        public const int FramesSlotPerDir = 8;
        public const int DirCount = 8;

        public static readonly Dictionary<ActionType, int> ActionFramesPerDir = new()
        {
            [ActionType.Stand] = 4,
            [ActionType.Walk] = 6,
            [ActionType.Run] = 6,
            [ActionType.Hit] = 6,
            [ActionType.HeavyHit] = 6,
            [ActionType.BigHit] = 6,
            [ActionType.PowerHit] = 6,
            [ActionType.LongHit] = 6,
            [ActionType.FireHit] = 6,
            [ActionType.CrsHit] = 6,
            [ActionType.Cast] = 6,
            [ActionType.Struck] = 2,
            [ActionType.Die] = 4,
            [ActionType.SitDown] = 2
        };

        public static readonly Dictionary<ActionType, int> ActionIntervalMs = new()
        {
            [ActionType.Stand] = 200,
            [ActionType.Walk] = 100,     // IDA sub_4CAF08 @ 0x4CAF08：服务端行走门禁 600ms，单步 6 帧 = 600ms
            [ActionType.Run] = 100,      // IDA sub_4CB11C @ 0x4CB11C：服务端奔跑门禁 600ms，单次 2 格 6 帧 = 600ms
            [ActionType.Hit] = 80,       // 盛大官方 1.5 物理出刀斩击 6 帧 ~480ms
            [ActionType.HeavyHit] = 80,
            [ActionType.BigHit] = 80,
            [ActionType.PowerHit] = 80,
            [ActionType.LongHit] = 80,
            [ActionType.FireHit] = 80,
            [ActionType.CrsHit] = 80,
            [ActionType.Cast] = 80,      // 盛大官方 1.5 施法施咒 6 帧 ~480ms
            [ActionType.Struck] = 125,   // 盛大官方 1.5 怪物/人物受击硬直 2 帧严格 250ms (Actor.pas:472)
            [ActionType.Die] = 130,      // 盛大官方 1.5 倒地死亡 4 帧 ~520ms
            [ActionType.SitDown] = 200
        };

        public static readonly Dictionary<ActionType, int> ActionOffsets = new()
        {
            [ActionType.Stand] = 0,
            [ActionType.Walk] = 64,
            [ActionType.Run] = 128,
            [ActionType.Hit] = 200,
            [ActionType.HeavyHit] = 264,
            [ActionType.BigHit] = 328,
            [ActionType.PowerHit] = 328,
            [ActionType.LongHit] = 328,
            [ActionType.FireHit] = 328,
            [ActionType.CrsHit] = 328,
            [ActionType.Cast] = 392,
            [ActionType.Struck] = 472,
            [ActionType.Die] = 536,
            [ActionType.SitDown] = 456
        };

        public static int CalcMonsterBaseOffset(int race)
        {
            int v8 = race % 10;
            int v1 = race / 10;
            if (v1 <= 6)
            {
                if (v1 == 6) return 440 * v8;
                switch (v1)
                {
                    case 0: return 280 * v8;
                    case 1: return 230 * v8;
                    case 2:
                    case 3: return 360 * v8;
                    case 4:
                        if (v8 == 1) return 600; // 触龙神
                        return 360 * v8;
                    case 5: return 430 * v8;
                    default: return 0;
                }
            }
            if (v1 < 14)
            {
                int v2 = v1 - 7;
                if (v2 < 6) return 360 * v8;
                int v4 = v2 - 6;
                if (v4 == 0)
                {
                    switch (v8)
                    {
                        case 1: return 360;
                        case 2: return 440;
                        case 3: return 550;
                        default: return 360 * v8;
                    }
                }
                return 0;
            }
            int v5 = v1 - 14;
            if (v5 < 3) return 360 * v8;
            int v6 = v5 - 3;
            if (v6 == 0) return 350 * v8;
            if (v6 == 73)
            {
                switch (v8)
                {
                    case 0: return 80;
                    case 1: return 168;
                    case 2: return 184;
                    case 3: return 200;
                }
            }
            return 0;
        }

        public static int CalcHumFrame(ActionType action, int direction, int animFrame, int bodyDressType = 0)
        {
            int dressBase = bodyDressType * HumDressStep;
            int actionOffset = ActionOffsets.GetValueOrDefault(action, 0);
            int dirClamped = direction & 0x07;
            int frames = ActionFramesPerDir.GetValueOrDefault(action, 1);
            int frameClamped = animFrame % Math.Max(1, frames);
            return dressBase + actionOffset + dirClamped * FramesSlotPerDir + frameClamped;
        }

        public static int CalcWeaponFrame(ActionType action, int direction, int animFrame, int weaponLook = 0)
        {
            int weaponBase = weaponLook * WeaponStep;
            int actionOffset = ActionOffsets.GetValueOrDefault(action, 0);
            int dirClamped = direction & 0x07;
            int frames = ActionFramesPerDir.GetValueOrDefault(action, 1);
            int frameClamped = animFrame % Math.Max(1, frames);
            return weaponBase + actionOffset + dirClamped * FramesSlotPerDir + frameClamped;
        }

        public static bool IsWeaponInFront(ActionType action, int direction, int animFrame = 0)
        {
            int dir = direction & 0x07;
            if (action == ActionType.Stand)
                return dir == 1 || dir == 2 || dir == 3;
            return dir == 1 || dir == 2 || dir == 3;
        }

        public static int CalcHairFrame(ActionType action, int direction, int animFrame, int hairStyle = 0, int gender = 0)
        {
            // 盛大官方 1.5 原版《Hair.wil》资产逆向结构实证：
            // Base 0/1 (0~1199): 单像素透明占位帧
            // Base 2 (1200~1799): 女披肩红长发
            // Base 3 (1800~2399): 女披肩蓝长发
            // Base 4 (2400~2999): 男经典深棕短发
            // Base 5 (3000~3599): 女干练红棕盘发/短发
            int actualStyle;
            if (gender != 0) // 女性
            {
                actualStyle = hairStyle switch
                {
                    1 => 5, // 创角发型 1: 红棕盘发/短发 (与大世界及纸娃娃最契合)
                    2 => 2, // 创角发型 2: 女红长发
                    3 => 3, // 创角发型 3: 女蓝长发
                    5 => 5,
                    _ => 5  // 女性默认发型 5
                };
            }
            else // 男性
            {
                actualStyle = 4; // 官方 1.5 原版男战士经典深棕短发
            }

            int hairBase = actualStyle * HairStep;
            int actionOffset = ActionOffsets.GetValueOrDefault(action, 0);
            int dirClamped = direction & 0x07;
            int frames = ActionFramesPerDir.GetValueOrDefault(action, 1);
            int frameClamped = animFrame % Math.Max(1, frames);
            return hairBase + actionOffset + dirClamped * FramesSlotPerDir + frameClamped;
        }

        public static int CalcMonsterFrame(ActionType action, int direction, int animFrame, int appr = 0, int raceImg = 10)
        {
            int monBase = CalcMonsterBaseOffset(appr);
            int stride = (raceImg == 11 || raceImg == 14) ? 10 : 8;
            int dirClamped = direction & 0x07;
            int frames = ActionFramesPerDir.GetValueOrDefault(action, 1);

            if (raceImg == 11 || raceImg == 14)
            {
                return action switch
                {
                    ActionType.Stand => monBase + 0 + dirClamped * 10 + (animFrame % 4),
                    ActionType.Walk or ActionType.Run => monBase + 80 + dirClamped * 10 + (animFrame % 6),
                    ActionType.Hit or ActionType.HeavyHit or ActionType.BigHit or ActionType.PowerHit
                        => monBase + 160 + dirClamped * 10 + (animFrame % 6),
                    ActionType.Cast or ActionType.Struck => monBase + 240 + dirClamped * 10 + (animFrame % 2),
                    ActionType.Die => (animFrame >= frames - 1)
                        ? monBase + 340 + dirClamped
                        : monBase + 260 + dirClamped * 10 + (animFrame % 10),
                    _ => monBase + dirClamped * 10
                };
            }
            else if (raceImg == 10)
            {
                return action switch
                {
                    ActionType.Stand => monBase + 0 + dirClamped * 8 + (animFrame % 4),
                    ActionType.Walk or ActionType.Run => monBase + 64 + dirClamped * 8 + (animFrame % 6),
                    ActionType.Hit or ActionType.HeavyHit or ActionType.BigHit or ActionType.PowerHit
                        => monBase + 128 + dirClamped * 8 + (animFrame % 4),
                    ActionType.Cast or ActionType.Struck => monBase + 192 + dirClamped * 2 + (animFrame % 2),
                    ActionType.Die => (animFrame >= frames - 1)
                        ? monBase + 208 + dirClamped * 8 + 3
                        : monBase + 208 + dirClamped * 8 + (animFrame % 4),
                    _ => monBase + dirClamped * 8
                };
            }
            else
            {
                int actionOffset = 0;
                int subFrame = 0;
                switch (action)
                {
                    case ActionType.Stand:
                        actionOffset = 0;
                        subFrame = animFrame % 4;
                        break;
                    case ActionType.Walk:
                    case ActionType.Run:
                        actionOffset = 64;
                        subFrame = animFrame % 6;
                        break;
                    case ActionType.Hit:
                    case ActionType.HeavyHit:
                    case ActionType.BigHit:
                    case ActionType.PowerHit:
                        actionOffset = 128;
                        subFrame = animFrame % 6;
                        break;
                    case ActionType.Cast:
                    case ActionType.Struck:
                        actionOffset = 192;
                        subFrame = animFrame % 2;
                        break;
                    case ActionType.Die:
                        if (animFrame >= frames - 1)
                            return monBase + 208 + dirClamped * 8 + 3;
                        actionOffset = 208;
                        subFrame = animFrame % 4;
                        break;
                    default:
                        actionOffset = 0;
                        subFrame = animFrame % 4;
                        break;
                }
                return monBase + actionOffset + dirClamped * stride + subFrame;
            }
        }

        public static int CalcNpcFrame(int look, int direction, int animFrame)
        {
            int npcBase = look * NpcStep;
            return npcBase + (animFrame % 4);
        }
    }

    public class AnimState
    {
        public ActionType Action { get; private set; } = ActionType.Stand;
        public int Direction { get; set; } = 2;
        public int AnimFrame { get; private set; } = 0;
        public float ElapsedMs { get; private set; } = 0f;
        public bool Loop { get; private set; } = true;
        public bool Finished { get; private set; } = false;
        public bool Paused { get; set; } = false;

        public event Action? AnimationDone;

        public void Pause() => Paused = true;
        public void Resume() => Paused = false;

        public bool IsAttack() => Action switch
        {
            ActionType.Hit or ActionType.HeavyHit or ActionType.BigHit or ActionType.PowerHit or
            ActionType.LongHit or ActionType.FireHit or ActionType.CrsHit or ActionType.Cast => true,
            _ => false
        };

        public bool IsBusy() => IsAttack() || Action == ActionType.Struck || Action == ActionType.Die;

        public void SetAction(ActionType newAction, int newDir = -1, bool? loop = null)
        {
            if (newDir >= 0) Direction = newDir & 7;
            if (Action != newAction)
            {
                Action = newAction;
                AnimFrame = 0;
                ElapsedMs = 0f;
                Finished = false;
                Loop = loop ?? (newAction == ActionType.Stand || newAction == ActionType.Walk || newAction == ActionType.Run);
            }
        }

        public void Reset()
        {
            Action = ActionType.Stand;
            AnimFrame = 0;
            ElapsedMs = 0f;
            Loop = true;
            Finished = false;
            Paused = false;
        }

        public void Update(float deltaMs)
        {
            if (Finished || Paused) return;

            ElapsedMs += deltaMs;
            float interval = SpriteEngine.ActionIntervalMs.GetValueOrDefault(Action, 200);
            if (ElapsedMs >= interval)
            {
                ElapsedMs -= interval;
                int maxFrames = SpriteEngine.ActionFramesPerDir.GetValueOrDefault(Action, 1);
                AnimFrame++;
                if (AnimFrame >= maxFrames)
                {
                    if (Loop)
                    {
                        AnimFrame = 0;
                    }
                    else
                    {
                        AnimFrame = maxFrames - 1;
                        Finished = true;
                        AnimationDone?.Invoke();
                        if (Action != ActionType.Die)
                        {
                            Action = ActionType.Stand;
                            AnimFrame = 0;
                            ElapsedMs = 0f;
                            Finished = false;
                            Loop = true;
                        }
                    }
                }
            }
        }

        public int GetHumFrame(int bodyType = 0) => SpriteEngine.CalcHumFrame(Action, Direction, AnimFrame, bodyType);
        public int GetMonsterFrame(int appr = 0, int raceImg = 10) => SpriteEngine.CalcMonsterFrame(Action, Direction, AnimFrame, appr, raceImg);
        public int GetNpcFrame(int look = 0) => SpriteEngine.CalcNpcFrame(look, Direction, AnimFrame);
    }
}
