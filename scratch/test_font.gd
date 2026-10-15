extends SceneTree

func _init():
    print("Testing font load...")
    var f1 = load("res://resources/fonts/SimSun.ttc")
    print("Load SimSun.ttc:", f1)
    var f2 = FontFile.new()
    var err = f2.load_dynamic_font("res://resources/fonts/SimSun.ttc")
    print("load_dynamic_font err:", err, " font:", f2)
    quit()
