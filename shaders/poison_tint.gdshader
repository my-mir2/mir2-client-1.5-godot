shader_type canvas_item;

// 毒素/状态类型：0=正常, 1=绿毒(持续扣血), 2=红毒(削减防御), 3=双毒(黄绿), 4=石化(麻痹灰色)
uniform int effect_type : hint_range(0, 4) = 0;
// 混色强度 (0.0=完全原色, 1.0=完全染色)
uniform float blend_factor : hint_range(0.0, 1.0) = 1.0;

void fragment() {
	vec4 tex_color = texture(TEXTURE, UV);
	
	// 基础明度计算
	float lum = dot(tex_color.rgb, vec3(0.299, 0.587, 0.114));

	// 剔除全透明像素、关闭状态以及脚下半透明黑阴影 (lum < 0.05)
	if (tex_color.a < 0.01 || effect_type == 0 || lum < 0.05) {
		COLOR = tex_color;
	} else {
		vec3 tinted_rgb = tex_color.rgb;

		if (effect_type == 1) {
			// 官方绿毒：保留金属/布匹纹理高光，主色呈现经典深沉幽绿泛光，避免塑料过曝
			vec3 green_tint = vec3(lum * 0.18, lum * 1.12, lum * 0.16);
			tinted_rgb = mix(tex_color.rgb * vec3(0.35, 1.25, 0.35), green_tint, 0.72);
		} else if (effect_type == 2) {
			// 官方红毒：保留肌肉与盔甲明暗反光，呈现经典朱红破甲血色
			vec3 red_tint = vec3(lum * 1.20, lum * 0.18, lum * 0.18);
			tinted_rgb = mix(tex_color.rgb * vec3(1.30, 0.35, 0.35), red_tint, 0.72);
		} else if (effect_type == 3) {
			// 双毒共存：红绿交汇的黄绿腐蚀色
			vec3 dual_tint = vec3(lum * 1.05, lum * 1.05, lum * 0.15);
			tinted_rgb = mix(tex_color.rgb * vec3(1.10, 1.10, 0.25), dual_tint, 0.72);
		} else if (effect_type == 4) {
			// 官方石化/麻痹：花岗岩厚重去色雕像质感，暗部更沉实
			tinted_rgb = vec3(lum * 0.65);
		}

		COLOR.rgb = mix(tex_color.rgb, tinted_rgb, blend_factor);
		COLOR.a = tex_color.a;
	}
}
