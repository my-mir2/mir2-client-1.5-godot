shader_type canvas_item;

// 光源中心 (屏幕像素坐标)
uniform vec2 light_pos = vec2(400.0, 300.0);
// 核心照亮半径 (内部完全明亮)
uniform float inner_radius : hint_range(0.0, 600.0) = 150.0;
// 边缘渐变羽化半径 (外部完全黑暗)
uniform float outer_radius : hint_range(0.0, 800.0) = 280.0;
// 环境黑暗颜色 (官方地牢深暗色)
uniform vec4 darkness_color : source_color = vec4(0.03, 0.03, 0.05, 0.96);
// 火光抖动微调 (模拟火把蜡烛微微跳动的真实感)
uniform float flicker : hint_range(0.8, 1.2) = 1.0;
// 是否启用黑夜/地牢模式
uniform bool enabled = true;

void fragment() {
	if (!enabled) {
		COLOR = vec4(0.0);
	} else {
		float dist = distance(FRAGCOORD.xy, light_pos);
		float current_inner = inner_radius * flicker;
		float current_outer = outer_radius * flicker;
		
		float t = clamp((dist - current_inner) / max(1.0, current_outer - current_inner), 0.0, 1.0);
		// 平滑三次 Hermite 插值
		t = t * t * (3.0 - 2.0 * t);
		
		COLOR = vec4(darkness_color.rgb, darkness_color.a * t);
	}
}
