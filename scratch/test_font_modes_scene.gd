extends Control

func _ready():
    var bg = ColorRect.new()
    bg.size = Vector2(800, 600)
    bg.color = Color(0.1, 0.1, 0.1)
    add_child(bg)
    
    var modes = [
        {"name": "1. HINTING_NONE + AA_NONE (当前代码使用)", "hinting": TextServer.HINTING_NONE, "aa": TextServer.FONT_ANTIALIASING_NONE},
        {"name": "2. HINTING_LIGHT + AA_NONE", "hinting": TextServer.HINTING_LIGHT, "aa": TextServer.FONT_ANTIALIASING_NONE},
        {"name": "3. HINTING_NORMAL + AA_NONE", "hinting": TextServer.HINTING_NORMAL, "aa": TextServer.FONT_ANTIALIASING_NONE},
        {"name": "4. HINTING_NORMAL + AA_GRAY", "hinting": TextServer.HINTING_NORMAL, "aa": TextServer.FONT_ANTIALIASING_GRAY},
        {"name": "5. HINTING_LIGHT + AA_GRAY", "hinting": TextServer.HINTING_LIGHT, "aa": TextServer.FONT_ANTIALIASING_GRAY},
        {"name": "6. HINTING_NORMAL + AA_LCD", "hinting": TextServer.HINTING_NORMAL, "aa": TextServer.FONT_ANTIALIASING_LCD},
    ]
    
    var y = 20
    for m in modes:
        var font = FontFile.new()
        font.load_dynamic_font("res://resources/fonts/SimSun.ttc")
        font.hinting = m["hinting"]
        font.antialiasing = m["aa"]
        font.subpixel_positioning = TextServer.SUBPIXEL_POSITIONING_DISABLED
        
        var title = Label.new()
        title.text = m["name"]
        title.position = Vector2(20, y)
        title.add_theme_font_override("font", font)
        title.add_theme_font_size_override("font_size", 12)
        title.add_theme_color_override("font_color", Color(1, 0.8, 0.2))
        add_child(title)
        y += 20
        
        var lbl = Label.new()
        lbl.text = "金创药(小量) 重量 1 持久与即时回复生命值 | 女战0143 半兽人 钉耙猫"
        lbl.position = Vector2(20, y)
        lbl.add_theme_font_override("font", font)
        lbl.add_theme_font_size_override("font_size", 12)
        lbl.add_theme_color_override("font_color", Color.WHITE)
        add_child(lbl)
        y += 35
        
    await get_tree().process_frame
    await get_tree().process_frame
    
    var img = get_viewport().get_texture().get_image()
    img.save_png("scratch/godot_font_render_modes.png")
    print("Screenshot saved to scratch/godot_font_render_modes.png")
    get_tree().quit()
