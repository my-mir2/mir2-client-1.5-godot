extends SceneTree

func _init():
    var img = Image.create(800, 300, false, Image.FORMAT_RGBA8)
    img.fill(Color(0.1, 0.1, 0.1, 1.0))

    var vp = SubViewport.new()
    vp.size = Vector2i(800, 300)
    vp.render_target_update_mode = SubViewport.UPDATE_ALWAYS

    var font = load("res://resources/fonts/Songti.res") as Font
    
    var vb = VBoxContainer.new()
    vb.position = Vector2(20, 20)
    
    # 模式 1: 当前有 outline = 1
    var l1 = Label.new()
    l1.text = "模式1 (当前带 outline=1): [系统] 欢迎来到热血传奇！ 女战9120 金创药(小量) 重量 1 多钩猫"
    l1.add_theme_font_override("font", font)
    l1.add_theme_font_size_override("font_size", 12)
    l1.add_theme_color_override("font_color", Color.WHITE)
    l1.add_theme_constant_override("outline_size", 1)
    l1.add_theme_color_override("font_outline_color", Color.BLACK)
    vb.add_child(l1)

    # 模式 2: 无 outline, 纯净宋体
    var l2 = Label.new()
    l2.text = "模式2 (纯净宋体 无 outline): [系统] 欢迎来到热血传奇！ 女战9120 金创药(小量) 重量 1 多钩猫"
    l2.add_theme_font_override("font", font)
    l2.add_theme_font_size_override("font_size", 12)
    l2.add_theme_color_override("font_color", Color.WHITE)
    l2.add_theme_constant_override("outline_size", 0)
    vb.add_child(l2)

    # 模式 3: 无 outline, 黄色字 (怪物/系统)
    var l3 = Label.new()
    l3.text = "模式3 (黄色名牌 无 outline): 多钩猫 森林雪人 半兽人 钉耙猫 金创药(小量)"
    l3.add_theme_font_override("font", font)
    l3.add_theme_font_size_override("font_size", 12)
    l3.add_theme_color_override("font_color", Color(1.0, 0.95, 0.5))
    l3.add_theme_constant_override("outline_size", 0)
    vb.add_child(l3)

    # 模式 4: RichTextLabel [b] 粗体对比
    var r1 = RichTextLabel.new()
    r1.bbcode_enabled = true
    r1.text = "模式4 (RichTextLabel 带[b]): [b][color=#ffff40]金创药(小量)[/color][/b] [color=#c0c0c0]重量 1[/color]"
    r1.add_theme_font_override("normal_font", font)
    r1.add_theme_font_override("bold_font", font)
    r1.add_theme_font_size_override("normal_font_size", 12)
    r1.add_theme_font_size_override("bold_font_size", 12)
    r1.custom_minimum_size = Vector2(600, 24)
    vb.add_child(r1)

    # 模式 5: RichTextLabel 不带 [b]
    var r2 = RichTextLabel.new()
    r2.bbcode_enabled = true
    r2.text = "模式5 (RichTextLabel 无[b]): [color=#ffff40]金创药(小量)[/color] [color=#c0c0c0]重量 1[/color]"
    r2.add_theme_font_override("normal_font", font)
    r2.add_theme_font_override("bold_font", font)
    r2.add_theme_font_size_override("normal_font_size", 12)
    r2.add_theme_font_size_override("bold_font_size", 12)
    r2.custom_minimum_size = Vector2(600, 24)
    vb.add_child(r2)

    root.add_child(vp)
    vp.add_child(vb)

    # 渲染一帧
    await process_frame
    await process_frame
    
    var tex = vp.get_texture()
    var captureImg = tex.get_image()
    captureImg.save_png("scratch/font_modes_comparison.png")
    print("Saved scratch/font_modes_comparison.png")
    quit(0)
