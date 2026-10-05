// ============================================================================
// Mir2 客户端 - 盛大官方 1.5 原版 DirectDraw 16-bit RGB 565 抖动与色深算法
// 官方实证：DirectDraw 7 16-bit Surface (R5:G6:B5) 与 4x4 Bayer Ordered Dithering
// ============================================================================

using System;
using Godot;

namespace Mir2Client.Render
{
    public static class RetroDitherMath
    {
        // 经典 4x4 Bayer 有序抖动矩阵 (归一化范围 0~15)
        public static readonly float[,] Bayer4x4 = new float[4, 4]
        {
            {  0f,  8f,  2f, 10f },
            { 12f,  4f, 14f,  6f },
            {  3f, 11f,  1f,  9f },
            { 15f,  7f, 13f,  5f }
        };

        /// <summary>
        /// 获取 (x, y) 像素坐标处的 Bayer 抖动扰动偏置 (-0.5 ~ +0.5)
        /// </summary>
        public static float GetBayerOffset(int screenX, int screenY)
        {
            int x = (screenX % 4 + 4) % 4;
            int y = (screenY % 4 + 4) % 4;
            return (Bayer4x4[y, x] / 16.0f) - 0.5f;
        }

        /// <summary>
        /// RGB 565 量化阶梯：R 31阶, G 63阶, B 31阶
        /// </summary>
        public static float QuantizeChannel(float value, int steps)
        {
            float clamped = Math.Clamp(value, 0f, 1f);
            return MathF.Round(clamped * steps) / steps;
        }

        /// <summary>
        /// 执行 1:1 官方 16-bit RGB 565 有序抖动与色彩离散化
        /// </summary>
        public static Color QuantizeRgb565(Color color, int screenX, int screenY, bool enableDither, float ditherStrength = 0.85f)
        {
            float r = color.R;
            float g = color.G;
            float b = color.B;

            if (enableDither)
            {
                float bayer = GetBayerOffset(screenX, screenY) * ditherStrength;
                r += bayer / 31.0f;
                g += bayer / 63.0f;
                b += bayer / 31.0f;
            }

            float qr = QuantizeChannel(r, 31);
            float qg = QuantizeChannel(g, 63);
            float qb = QuantizeChannel(b, 31);

            return new Color(qr, qg, qb, color.A);
        }

        /// <summary>
        /// 转换为官方 DirectDraw 原生 16 位 ushort RGB565 整数值
        /// </summary>
        public static ushort ToRgb565Word(Color color)
        {
            int r = Math.Clamp((int)MathF.Round(color.R * 31.0f), 0, 31);
            int g = Math.Clamp((int)MathF.Round(color.G * 63.0f), 0, 63);
            int b = Math.Clamp((int)MathF.Round(color.B * 31.0f), 0, 31);

            return (ushort)((r << 11) | (g << 5) | b);
        }

        /// <summary>
        /// 从官方 DirectDraw 原生 16 位 ushort RGB565 恢复 Color
        /// </summary>
        public static Color FromRgb565Word(ushort val)
        {
            int r = (val >> 11) & 0x1F;
            int g = (val >> 5) & 0x3F;
            int b = val & 0x1F;

            return new Color(r / 31.0f, g / 63.0f, b / 31.0f, 1.0f);
        }
    }
}
