extends SceneTree

func _init():
    print("Converting SimSun.ttc to Songti.res...")
    var font = FontFile.new()
    var err = font.load_dynamic_font("res://resources/fonts/SimSun.ttc")
    print("load_dynamic_font result:", err)
    if err == OK:
        font.antialiasing = TextServer.FONT_ANTIALIASING_GRAY
        font.subpixel_positioning = TextServer.SUBPIXEL_POSITIONING_DISABLED
        font.generate_mipmaps = false
        font.multichannel_signed_distance_field = false
        font.hinting = TextServer.HINTING_NORMAL
        
        var save_err = ResourceSaver.save(font, "res://resources/fonts/Songti.res")
        print("ResourceSaver save result:", save_err)
    quit()
