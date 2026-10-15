extends SceneTree

func _init():
    var vp = SubViewport.new()
    vp.size = Vector2i(800, 360)
    vp.render_target_update_mode = SubViewport.UPDATE_ALWAYS

    # 用沙地颜色充当背景 (RGB: 168, 148, 116)
    var bg = ColorRect.new()
    bg.size = Vector2(800, 360)
    bg.color = Color(0.66, 0.58, 0.45)
    vp.add_child(bg)

    var font = load("res://resources/fonts/Songti.res") as Font
    
    var vb = VBoxContainer.new()
    vb.position = Vector2(20, 20)
    vb.add_theme_constant_override("separation", 14)
    vp.add_child(vb)
    
    # 方案 A: 当前现状 (outline = 1)
    var l1 = Label.new()
    l1.text = "方案A (当前现状 outline=1): 多钩猫  半兽人  女战9120  金币(20)  金创药(小量)"
    l1.add_theme_font_override("font", font)
    l1.add_theme_font_size_override("font_size", 12)
    l1.add_theme_color_override("font_color", Color(1.0, 0.95, 0.5))
    l1.add_theme_constant_override("outline_size", 1)
    l1.add_theme_color_override("font_outline_color", Color.BLACK)
    vb.add_child(l1)

    # 方案 B: 纯净宋体 (无 outline，无 shadow)
    var l2 = Label.new()
    l2.text = "方案B (纯净宋体 无outline): 多钩猫  半兽人  女战9120  金币(20)  金创药(小量)"
    l2.add_theme_font_override("font", font)
    l2.add_theme_font_size_override("font_size", 12)
    l2.add_theme_color_override("font_color", Color(1.0, 0.95, 0.5))
    l2.add_theme_constant_override("outline_size", 0)
    vb.add_child(l2)

    # 方案 C: 1px 黑色微投影 (shadow x=1, y=1) 经典清晰度
    var l3 = Label.new()
    l3.text = "方案C (1px 黑色清晰投影 shadow x=1 y=1): 多钩猫  半兽人  女战9120  金币(20)  金创药(小量)"
    l3.add_theme_font_override("font", font)
    l3.add_theme_font_size_override("font_size", 12)
    l3.add_theme_color_override("font_color", Color(1.0, 0.95, 0.5))
    l3.add_theme_constant_override("outline_size", 0)
    l3.add_theme_constant_override("shadow_offset_x", 1)
    l3.add_theme_constant_override("shadow_offset_y", 1)
    l3.add_theme_color_override("font_shadow_color", Color(0, 0, 0, 0.9))
    vb.add_child(l3)

    # 方案 D: 黑底面板/内胆中的文本 (金创药小量、背包、装备)
    var panel = Panel.new()
    panel.custom_minimum_size = Vector2(760, 100)
    var sb = StyleBoxFlat.new()
    sb.bg_color = Color(0.05, 0.05, 0.05, 1.0)
    panel.add_theme_stylebox_override("panel", sb)
    
    var vb2 = VBoxContainer.new()
    vb2.position = Vector2(10, 10)
    panel.add_child(vb2)

    var lp1 = Label.new()
    lp1.text = "黑底面板 方案A (当前带outline=1): 金创药(小量) 重量 1 持久与即时回复生命值"
    lp1.add_theme_font_override("font", font)
    lp1.add_theme_font_size_override("font_size", 12)
    lp1.add_theme_color_override("font_color", Color.WHITE)
    lp1.add_theme_constant_override("outline_size", 1)
    lp1.add_theme_color_override("font_outline_color", Color.BLACK)
    vb2.add_child(lp1)

    var lp2 = Label.new()
    lp2.text = "黑底面板 方案B (纯净宋体 无outline): 金创药(小量) 重量 1 持久与即时回复生命值"
    lp2.add_theme_font_override("font", font)
    lp2.add_theme_font_size_override("font_size", 12)
    lp2.add_theme_color_override("font_color", Color.WHITE)
    lp2.add_theme_constant_override("outline_size", 0)
    vb2.add_child(lp2)

    var lp3 = Label.new()
    lp3.text = "黑底面板 聊天消息/系统提示: [系统] 欢迎来到热血传奇！"
    lp3.add_theme_font_override("font", font)
    lp3.add_theme_font_size_override("font_size", 12)
    lp3.add_theme_color_override("font_color", Color(0.2, 1.0, 0.2))
    lp3.add_theme_constant_override("outline_size", 0)
    vb2.add_child(lp3)

    vb.add_child(panel)

    root.add_child(vp)

    await process_frame
    await process_frame
    
    var tex = vp.get_texture()
    var captureImg = tex.get_image()
    captureImg.save_png("scratch/sand_font_comparison.png")
    print("Saved scratch/sand_font_comparison.png")
    quit(0)
