shader_type canvas_item;

// 灰度混合比例 (0.0: 原彩, 1.0: 纯黑白单色)
uniform float gray_amount : hint_range(0.0, 1.0) = 0.0;
uniform sampler2D screen_texture : hint_screen_texture, filter_linear_mipmap;

void fragment() {
	vec4 col = texture(screen_texture, SCREEN_UV);
	// 经典 ITU-R BT.601 亮度加权系数
	float gray = dot(col.rgb, vec3(0.299, 0.587, 0.114));
	// 略微压暗对比度，呈现传奇经典黑白惨烈感
	vec3 mono = vec3(gray * 0.85);
	COLOR = vec4(mix(col.rgb, mono, gray_amount), col.a);
}
