using System;
using System.Collections.Generic;
using System.Linq;

namespace Mir2Client.UI;

/// <summary>
/// 盛大官方 1.5 权威原生窗体像素、输入控件与交互状态机引擎 (细节 51 ~ 70)
/// 严格依据官方二进制 MIR_final_v3_p.exe Delphi 原生逻辑与 Prguse.wil 资产规范
/// </summary>
public static class UIInteractionEngine
{
    // --------------------------------------------------------------------------------
    // 细节 51: 登录界面账号/密码输入框光标 500ms 周期闪烁与退格长按连删
    // --------------------------------------------------------------------------------
    public static class InputCursorBackspaceEngine
    {
        public const int BlinkIntervalMs = 500;
        public const int BackspaceInitialDelayMs = 400;
        public const int BackspaceRepeatIntervalMs = 50;

        public static bool IsCursorVisible(long elapsedMs)
        {
            return (elapsedMs / BlinkIntervalMs) % 2 == 0;
        }

        public static string ProcessBackspace(string text, long pressDurationMs)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            if (pressDurationMs < 0) return text;

            int deletes = 1; // 首次按下删 1 个
            if (pressDurationMs > BackspaceInitialDelayMs)
            {
                long repeatTime = pressDurationMs - BackspaceInitialDelayMs;
                deletes += (int)(repeatTime / BackspaceRepeatIntervalMs);
            }

            if (deletes >= text.Length) return string.Empty;
            return text.Substring(0, text.Length - deletes);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 52: 登录选服石碑双列排版 (双列、每列 8 个，行高 18px，金色选中高亮)
    // --------------------------------------------------------------------------------
    public static class ServerSelectLayout
    {
        public const int ItemsPerColumn = 8;
        public const int RowHeight = 18;
        public const int LeftColX = 350;
        public const int RightColX = 520;
        public const int BaseY = 220;

        public static (int x, int y, int col, int row) GetServerItemPosition(int index)
        {
            int col = index / ItemsPerColumn;
            int row = index % ItemsPerColumn;
            int x = (col == 0) ? LeftColX : RightColX;
            int y = BaseY + row * RowHeight;
            return (x, y, col, row);
        }

        public static bool IsSelectedGoldenHighlight(int itemIndex, int selectedIndex)
        {
            return itemIndex == selectedIndex;
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 53: 角色选择神龛界面左右烛台明暗波动微动画 (正弦周期 0.7~1.0 亮度)
    // --------------------------------------------------------------------------------
    public static class ChrSelCandleEngine
    {
        public static (float leftCandleAlpha, float rightCandleAlpha) GetCandleBrightness(float timeSeconds)
        {
            // 左右烛台具有 0.5 弧度相位差，模拟真实双烛台独立跳动
            float left = 0.85f + 0.15f * MathF.Sin(timeSeconds * 6.28f);
            float right = 0.85f + 0.15f * MathF.Sin(timeSeconds * 6.28f + 0.5f);
            return (Math.Clamp(left, 0.7f, 1.0f), Math.Clamp(right, 0.7f, 1.0f));
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 54: 角色删除石化破碎状态机 (确认密码 -> 石化 1.5s -> 碎裂 1.0s -> 销毁)
    // --------------------------------------------------------------------------------
    public static class CharDeleteStateEngine
    {
        public enum DeleteStage { None, PasswordPrompt, Petrifying, Shattering, Destroyed }

        public static (DeleteStage stage, float progress) UpdateDeleteAnimation(DeleteStage currentStage, float elapsedInStage)
        {
            switch (currentStage)
            {
                case DeleteStage.Petrifying:
                    if (elapsedInStage >= 1.5f) return (DeleteStage.Shattering, 0f);
                    return (DeleteStage.Petrifying, elapsedInStage / 1.5f);

                case DeleteStage.Shattering:
                    if (elapsedInStage >= 1.0f) return (DeleteStage.Destroyed, 1f);
                    return (DeleteStage.Shattering, elapsedInStage / 1.0f);

                default:
                    return (currentStage, 0f);
            }
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 55: 创建角色战法道介绍与立绘切换
    // --------------------------------------------------------------------------------
    public static class CreateChrEngine
    {
        public static (string jobDesc, int portraitWilIndex) GetJobPresentation(int job, int gender)
        {
            // job: 0=Warrior, 1=Mage, 2=Taoist; gender: 0=Male, 1=Female
            return job switch
            {
                0 => ("战士拥有强大的体魄和坚韧的防御，擅长近身肉搏。", gender == 0 ? 100 : 101),
                1 => ("法师具备强大的远程法术毁灭力，但肉体防御孱弱。", gender == 0 ? 110 : 111),
                2 => ("道士精通博大精深的精神力，擅长施毒与召唤神兽。", gender == 0 ? 120 : 121),
                _ => ("未知职业", 0)
            };
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 56: HUD 主底栏血球魔球玻璃反光罩高光层
    // --------------------------------------------------------------------------------
    public static class HudOrbGlassEngine
    {
        public const float GlassHighlightAlpha = 0.35f;
        public const int LeftOrbCenterX = 29;
        public const int LeftOrbCenterY = 575;
        public const int RightOrbCenterX = 771;
        public const int RightOrbCenterY = 575;
        public const int OrbRadius = 32;

        public static bool IsPointInOrbHighlight(int px, int py, bool isHpOrb)
        {
            int cx = isHpOrb ? LeftOrbCenterX : RightOrbCenterX;
            int cy = isHpOrb ? LeftOrbCenterY : RightOrbCenterY;
            int dx = px - cx;
            int dy = py - cy;
            return (dx * dx + dy * dy) <= (OrbRadius * OrbRadius) && py < cy; // 上半弧反光
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 57: HUD 经验条 16 截断每满 1/16 触发微型像素闪烁
    // --------------------------------------------------------------------------------
    public static class HudExpBarEngine
    {
        public const int Segments = 16;

        public static (int litSegments, bool triggerFlash) CalculateExpProgress(long currentExp, long maxExp, long previousExp)
        {
            if (maxExp <= 0) return (0, false);
            int currentSegment = (int)Math.Clamp((currentExp * Segments) / maxExp, 0, Segments);
            int previousSegment = (int)Math.Clamp((previousExp * Segments) / maxExp, 0, Segments);
            bool flash = currentSegment > previousSegment && currentSegment > 0;
            return (currentSegment, flash);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 58: HUD 负重条三段式实时比例分割 (腕力、穿戴负重、背包负重)
    // --------------------------------------------------------------------------------
    public static class HudWeightBarEngine
    {
        public static (float armRatio, float wearRatio, float bagRatio, bool isOverweight) CalculateWeightRatios(
            int currentArm, int maxArm,
            int currentWear, int maxWear,
            int currentBag, int maxBag)
        {
            float armRatio = maxArm > 0 ? Math.Clamp((float)currentArm / maxArm, 0f, 1f) : 0f;
            float wearRatio = maxWear > 0 ? Math.Clamp((float)currentWear / maxWear, 0f, 1f) : 0f;
            float bagRatio = maxBag > 0 ? Math.Clamp((float)currentBag / maxBag, 0f, 1f) : 0f;
            bool isOverweight = currentBag > maxBag || currentWear > maxWear || currentArm > maxArm;
            return (armRatio, wearRatio, bagRatio, isOverweight);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 59: 背包 40 格物品空格子半透明内凹底纹与鼠标划过泛光
    // --------------------------------------------------------------------------------
    public static class BagSlotInteractionEngine
    {
        public const int Cols = 8;
        public const int Rows = 5;
        public const int TotalSlots = 40;
        public const int StartX = 640;
        public const int StartY = 320;
        public const int SlotSize = 32;
        public const int Spacing = 2;

        public static (int x, int y) GetSlotPosition(int index)
        {
            int c = index % Cols;
            int r = index / Cols;
            return (StartX + c * (SlotSize + Spacing), StartY + r * (SlotSize + Spacing));
        }

        public static int GetHoveredSlotIndex(int mouseX, int mouseY)
        {
            for (int i = 0; i < TotalSlots; i++)
            {
                var (x, y) = GetSlotPosition(i);
                if (mouseX >= x && mouseX < x + SlotSize && mouseY >= y && mouseY < y + SlotSize)
                {
                    return i;
                }
            }
            return -1;
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 60: 装备面板 9 槽位官方原生镂空暗影图标
    // --------------------------------------------------------------------------------
    public static class EquipSlotShadowEngine
    {
        public enum EquipSlotId { Dress = 0, Weapon = 1, Medal = 2, Necklace = 3, Helmet = 4, LeftArm = 5, RightArm = 6, LeftRing = 7, RightRing = 8 }

        public static int GetSlotShadowWilIndex(EquipSlotId slot)
        {
            return slot switch
            {
                EquipSlotId.Dress => 701,
                EquipSlotId.Weapon => 702,
                EquipSlotId.Medal => 703,
                EquipSlotId.Necklace => 704,
                EquipSlotId.Helmet => 705,
                EquipSlotId.LeftArm => 706,
                EquipSlotId.RightArm => 707,
                EquipSlotId.LeftRing => 708,
                EquipSlotId.RightRing => 709,
                _ => 0
            };
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 61: 状态面板数值加点/施加 Buff 绿色上升小箭头持续 3 秒
    // --------------------------------------------------------------------------------
    public static class StatArrowEngine
    {
        public const float DurationSeconds = 3.0f;

        public static bool ShouldShowArrow(float elapsedSinceStatIncreased)
        {
            return elapsedSinceStatIncreased >= 0 && elapsedSinceStatIncreased < DurationSeconds;
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 62: 技能面板上下翻页金属旋钮按压时的 1 像素物理下沉
    // --------------------------------------------------------------------------------
    public static class SkillKnobEngine
    {
        public static (int renderX, int renderY) GetKnobRenderOffset(int baseX, int baseY, bool isPressed)
        {
            return isPressed ? (baseX, baseY + 1) : (baseX, baseY);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 63: 加点面板长按连续加点与重置撤销按钮状态
    // --------------------------------------------------------------------------------
    public static class AdjustAbilInteractionEngine
    {
        public static (int newStat, int remainingPoints) ApplyAddPoint(int currentStat, int remainingPoints, int delta)
        {
            if (remainingPoints < delta) return (currentStat, remainingPoints);
            return (currentStat + delta, remainingPoints - delta);
        }

        public static (int resetStat, int restoredPoints) ResetPoints(int currentStat, int baseStat, int remainingPoints)
        {
            int allocated = currentStat - baseStat;
            if (allocated <= 0) return (currentStat, remainingPoints);
            return (baseStat, remainingPoints + allocated);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 64: 快捷键设置浮窗高亮待输入状态 (按 Esc 取消)
    // --------------------------------------------------------------------------------
    public static class KeySelectEngine
    {
        public static (string newKey, bool isListening) ProcessKeyInput(string currentKey, string pressedKey, bool isListening)
        {
            if (!isListening) return (currentKey, false);
            if (pressedKey == "Escape") return (currentKey, false); // 取消绑定
            return (pressedKey, false);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 65: 商店买卖面板单页 6 项严格高度对齐与平滑滚动
    // --------------------------------------------------------------------------------
    public static class ShopListLayoutEngine
    {
        public const int PageSize = 6;
        public const int ItemRowHeight = 24;

        public static List<int> GetVisibleItems(int totalItems, int scrollOffset)
        {
            var list = new List<int>();
            int start = Math.Clamp(scrollOffset, 0, Math.Max(0, totalItems - PageSize));
            for (int i = 0; i < PageSize && (start + i) < totalItems; i++)
            {
                list.Add(start + i);
            }
            return list;
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 66: 装备修理面板金币不足时按钮置灰与红色文字告警
    // --------------------------------------------------------------------------------
    public static class RepairNoticeEngine
    {
        public static (bool canRepair, string noticeColor, string noticeText) EvaluateRepair(int currentGold, int repairCost)
        {
            if (currentGold < repairCost)
            {
                return (false, "RED", $"金币不足，需要 {repairCost} 金币进行修理！");
            }
            return (true, "WHITE", $"修理需要消耗 {repairCost} 金币。");
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 67: NPC 交互对话框关闭叉号鼠标悬停微亮高光态
    // --------------------------------------------------------------------------------
    public static class NpcCloseButtonEngine
    {
        public const int NormalWilIndex = 64;
        public const int HoverHighlightWilIndex = 65;

        public static int GetCloseButtonFrame(bool isHovered)
        {
            return isHovered ? HoverHighlightWilIndex : NormalWilIndex;
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 68: 行会面板成员在线状态与职位排序
    // --------------------------------------------------------------------------------
    public static class GuildMemberSortEngine
    {
        public record MemberItem(string Name, int Rank, bool IsOnline); // Rank: 0=掌门, 1=副掌门, 2=堂主, 3=会员

        public static List<MemberItem> SortMembers(IEnumerable<MemberItem> members)
        {
            return members
                .OrderByDescending(m => m.IsOnline) // 在线排前
                .ThenBy(m => m.Rank)                 // 职位高的排前
                .ThenBy(m => m.Name)
                .ToList();
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 69: 观人面板查看他人装备时的 3 秒沙漏加载动画
    // --------------------------------------------------------------------------------
    public static class InspectLoadEngine
    {
        public const float TimeoutSeconds = 3.0f;

        public static (bool isStillLoading, int hourglassFrame) UpdateLoading(float elapsed)
        {
            if (elapsed >= TimeoutSeconds) return (false, 0);
            int frame = ((int)(elapsed * 4)) % 8; // 每秒 4 帧，共 8 帧沙漏旋转
            return (true, frame);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 70: 系统设置面板音效 0% 静音小喇叭打叉
    // --------------------------------------------------------------------------------
    public static class AudioMuteIconEngine
    {
        public const int SpeakerNormalWilIndex = 380;
        public const int SpeakerMutedWilIndex = 381;

        public static int GetSpeakerIcon(int volumePercent)
        {
            return volumePercent <= 0 ? SpeakerMutedWilIndex : SpeakerNormalWilIndex;
        }
    }
}
