// ============================================================================
// Mir2 客户端 - 盛大官方 1.5 原版 DirectDraw 16-bit (RGB 565) 复古有序抖动滤镜
// 官方实证：DirectDraw 7 硬件 16 位色深 (32阶红/64阶绿/32阶蓝) 与 Bayer 4x4 Dithering
// 真实重现 2001 年原版传奇夜间火把光晕阶梯、半透明魔法粒子与像素颗粒质感
// ============================================================================

shader_type canvas_item;

uniform bool enable_dither = true;
uniform float dither_strength : hint_range(0.0, 1.0) = 0.85;
uniform sampler2D screen_texture : hint_screen_texture, filter_nearest;

// 经典 4x4 Bayer 空间有序抖动矩阵
const mat4 bayer4x4 = mat4(
    vec4( 0.0,  8.0,  2.0, 10.0),
    vec4(12.0,  4.0, 14.0,  6.0),
    vec4( 3.0, 11.0,  1.0,  9.0),
    vec4(15.0,  7.0, 13.0,  5.0)
);

void fragment() {
    vec4 src = texture(screen_texture, SCREEN_UV);
    vec3 col = src.rgb;

    if (enable_dither) {
        int x = int(mod(FRAGCOORD.x, 4.0));
        int y = int(mod(FRAGCOORD.y, 4.0));
        // 将 [0, 15] 映射到 [-0.5, 0.5]
        float bayer = (bayer4x4[x][y] / 16.0) - 0.5;

        // 根据 RGB 565 各通道的量化间隔施加抖动位移
        // R (5-bit): 31 阶; G (6-bit): 63 阶; B (5-bit): 31 阶
        vec3 dither = vec3(
            bayer / 31.0,
            bayer / 63.0,
            bayer / 31.0
        ) * dither_strength;

        col += dither;
    }

    // 官方 16-bit RGB 565 严密离散量化
    col.r = clamp(floor(col.r * 31.0 + 0.5) / 31.0, 0.0, 1.0);
    col.g = clamp(floor(col.g * 63.0 + 0.5) / 63.0, 0.0, 1.0);
    col.b = clamp(floor(col.b * 31.0 + 0.5) / 31.0, 0.0, 1.0);

    COLOR = vec4(col, src.a);
}
