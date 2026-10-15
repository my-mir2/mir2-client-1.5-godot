shader_type canvas_item;

// 盛大官方 1.5 血球魔球动态液面裁剪 Shader
// 严格对齐 IDA sub_48B2A0 @ 0x48b64f..0x48b71c
// 左半球 HP，右半球 MP，自底向上填充

uniform float hp_ratio : hint_range(0.0, 1.0) = 1.0;
uniform float mp_ratio : hint_range(0.0, 1.0) = 1.0;
uniform bool is_single_hp_ball = false;

void fragment() {
    vec4 col = texture(TEXTURE, UV);
    float fill_y = 1.0 - UV.y; // 0.0 在底端, 1.0 在顶端

    if (is_single_hp_ball) {
        if (fill_y > hp_ratio) {
            col.a = 0.0;
        }
    } else {
        if (UV.x < 0.5) {
            if (fill_y > hp_ratio) {
                col.a = 0.0;
            }
        } else {
            if (mp_ratio <= 0.001 || fill_y > mp_ratio) {
                col.a = 0.0;
            }
        }
    }

    COLOR = col;
}
