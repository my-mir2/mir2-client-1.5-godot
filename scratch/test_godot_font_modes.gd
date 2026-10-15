extends SceneTree

func _init():
    var img = Image.create(600, 400, false, Image.FORMAT_RGBA8)
    img.fill(Color.BLACK)
    
    var modes = [
        {"name": "SimSun_HintingNone_AANone", "hinting": TextServer.HINTING_NONE, "aa": TextServer.FONT_ANTIALIASING_NONE},
        {"name": "SimSun_HintingLight_AANone", "hinting": TextServer.HINTING_LIGHT, "aa": TextServer.FONT_ANTIALIASING_NONE},
        {"name": "SimSun_HintingNormal_AANone", "hinting": TextServer.HINTING_NORMAL, "aa": TextServer.FONT_ANTIALIASING_NONE},
        {"name": "SimSun_HintingNormal_AALCD", "hinting": TextServer.HINTING_NORMAL, "aa": TextServer.FONT_ANTIALIASING_LCD},
        {"name": "SimSun_HintingNormal_AAGrayscale", "hinting": TextServer.HINTING_NORMAL, "aa": TextServer.FONT_ANTIALIASING_GRAYSCALE},
    ]
    
    var ts = TextServerManager.get_primary_interface()
    var text = "金创药(小量) 重量 1 持久与即时回复生命值"
    var text2 = "女战0143 半兽人 钉耙猫 多钩猫 稻草人"
    
    for i in range(modes.size()):
        var m = modes[i]
        var font = FontFile.new()
        var err = font.load_dynamic_font("res://resources/fonts/SimSun.ttc")
        if err != OK:
            print("Failed to load SimSun.ttc")
            quit(1)
            return
            
        font.hinting = m["hinting"]
        font.antialiasing = m["aa"]
        font.subpixel_positioning = TextServer.SUBPIXEL_POSITIONING_DISABLED
        
        # 绘制到单独的一块视口或渲染测试
        print("Tested mode: ", m["name"])
        
    quit(0)
