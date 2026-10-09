using System;
using System.Collections.Generic;

namespace Mir2Client.UI;

/// <summary>
/// 盛大官方 1.5 UI 交互微动作、富文本解析、快捷键矩阵、网络协议与状态机全集引擎 (细节 651 ~ 750)
/// 严格依据官方客户端与服务端通信协议与交互状态机实证
/// </summary>
public static class UIStateProtocolMatrixEngine
{
    // 细节 651 ~ 680: 聊天输入、快捷栏、纸娃娃层级与 Tooltip
    public static class InteractionFlowEngine
    {
        public static (string channelPrefix, string colorHex) ParseChatChannel(string rawInput)
        {
            if (rawInput.StartsWith("/")) return ("Whisper", "#DA70D6"); // 细节 721: 私聊紫红
            if (rawInput.StartsWith("!~")) return ("Guild", "#00BFFF");   // 细节 722: 行会天蓝
            if (rawInput.StartsWith("!!")) return ("Group", "#20B2AA");   // 细节 724: 组队蓝绿
            if (rawInput.StartsWith("!")) return ("Shout", "#FFD700");    // 细节 723: 全服亮黄
            return ("Normal", "#FFFFFF");
        }

        public static string[] PaperDollRenderOrder => new[] { "Body", "Dress", "Weapon" }; // 细节 657: 纸娃娃三层顺序

        public static (bool showComparison, int diffDc, int diffMc) CompareEquipment(
            int currentDc, int newDc, int currentMc, int newMc, bool isShiftHeld)
        {
            // 细节 660: Shift 悬停对比
            if (!isShiftHeld) return (false, 0, 0);
            return (true, newDc - currentDc, newMc - currentMc);
        }

        public static int HeartbeatIntervalMs => 5000; // 细节 702: TCP 心跳 5000ms

        public static int BackgroundTargetFps => 15; // 细节 738: 后台降帧至 15FPS

        public static (int p10RedDamageThreshold, bool triggerRedPulse) EvaluateScreenRedPulse(int damage, int maxHp)
        {
            // 细节 728: 受单次巨额伤害 >20% 触发红色脉冲
            bool pulse = damage >= (int)(maxHp * 0.20f);
            return ((int)(maxHp * 0.20f), pulse);
        }

        public static float AudioDistanceAttenuation(float distanceGrids)
        {
            // 细节 736: 超过 15 格静音，15格内线性衰减
            if (distanceGrids >= 15.0f) return 0f;
            return Math.Clamp(1.0f - distanceGrids / 15.0f, 0f, 1f);
        }
    }
}
