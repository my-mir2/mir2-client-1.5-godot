// ============================================================================
// Mir2 客户端 - 盛大官方 1.5 原版 DirectDraw 16-bit RGB 565 抖动算法单元测试
// 官方实证：DirectDraw 7 16-bit Surface (R5:G6:B5) 与 4x4 Bayer Ordered Dithering
// ============================================================================

using System;
using Godot;
using Mir2Client.Render;
using Xunit;

namespace Mir2Client.Tests
{
    public class RetroDitherMathTests
    {
        [Fact]
        public void Test_Bayer4x4_PeriodicityAndRange()
        {
            // 验证 4x4 周期性重复
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    float offset1 = RetroDitherMath.GetBayerOffset(x, y);
                    float offset2 = RetroDitherMath.GetBayerOffset(x + 4, y + 4);
                    Assert.Equal(offset1, offset2, 0.0001f);

                    // 偏置范围必须在 [-0.5, +0.5]
                    Assert.True(offset1 >= -0.5f && offset1 <= 0.5f);
                }
            }

            // 验证 (0, 0) 的值对应矩阵 0: 0/16 - 0.5 = -0.5
            Assert.Equal(-0.5f, RetroDitherMath.GetBayerOffset(0, 0), 0.0001f);
            // 验证 (0, 3) 的值对应矩阵 15: 15/16 - 0.5 = 0.4375
            Assert.Equal(15f / 16f - 0.5f, RetroDitherMath.GetBayerOffset(0, 3), 0.0001f);
        }

        [Fact]
        public void Test_Rgb565_QuantizationSteps()
        {
            // R 通道 31 阶: 步长 1.0 / 31.0
            float stepR = 1.0f / 31.0f;
            float q1 = RetroDitherMath.QuantizeChannel(stepR * 0.4f, 31);
            Assert.Equal(0.0f, q1, 0.0001f); // 四舍五入到 0

            float q2 = RetroDitherMath.QuantizeChannel(stepR * 0.6f, 31);
            Assert.Equal(stepR, q2, 0.0001f); // 四舍五入到 1 阶

            // G 通道 63 阶: 步长 1.0 / 63.0
            float stepG = 1.0f / 63.0f;
            float qg = RetroDitherMath.QuantizeChannel(stepG * 10.4f, 63);
            Assert.Equal(stepG * 10f, qg, 0.0001f);
        }

        [Fact]
        public void Test_ToRgb565Word_RoundTrip()
        {
            // 纯红
            var red = new Color(1f, 0f, 0f);
            ushort redWord = RetroDitherMath.ToRgb565Word(red);
            // 5位红在最高位: 31 << 11 = 0xF800
            Assert.Equal(0xF800, redWord);

            var recoveredRed = RetroDitherMath.FromRgb565Word(redWord);
            Assert.Equal(1.0f, recoveredRed.R, 0.01f);
            Assert.Equal(0.0f, recoveredRed.G, 0.01f);
            Assert.Equal(0.0f, recoveredRed.B, 0.01f);

            // 纯绿 (63 << 5 = 0x07E0)
            var green = new Color(0f, 1f, 0f);
            ushort greenWord = RetroDitherMath.ToRgb565Word(green);
            Assert.Equal(0x07E0, greenWord);

            // 纯蓝 (31 = 0x001F)
            var blue = new Color(0f, 0f, 1f);
            ushort blueWord = RetroDitherMath.ToRgb565Word(blue);
            Assert.Equal(0x001F, blueWord);

            // 纯白 (0xFFFF)
            var white = new Color(1f, 1f, 1f);
            ushort whiteWord = RetroDitherMath.ToRgb565Word(white);
            Assert.Equal(0xFFFF, whiteWord);
        }

        [Fact]
        public void Test_DitherEffect_AddsTextureNoise()
        {
            // 在中间灰阶 (0.5, 0.5, 0.5) 处，不开启抖动时全屏均为同一量化值
            var baseColor = new Color(0.5f, 0.5f, 0.5f);
            var noDither1 = RetroDitherMath.QuantizeRgb565(baseColor, 0, 0, false);
            var noDither2 = RetroDitherMath.QuantizeRgb565(baseColor, 1, 1, false);
            Assert.Equal(noDither1, noDither2);

            // 开启抖动后，相邻不同 Bayer 坐标会产生微小色阶扰动（模拟复古颗粒）
            var dither1 = RetroDitherMath.QuantizeRgb565(baseColor, 0, 0, true, 1.0f);
            var dither2 = RetroDitherMath.QuantizeRgb565(baseColor, 0, 3, true, 1.0f); // offset 为 -0.5 vs +0.4375
            Assert.NotEqual(dither1, dither2);
        }
    }
}
