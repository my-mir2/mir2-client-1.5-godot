with open("scripts/game/Player.gd", "r", encoding="utf-8") as f:
    content = f.read()

old_block = """\t# 8. 虹魔多参数形态 (场景 1199 & 1532)
\tif typeof(arg0) == TYPE_BOOL and typeof(arg1) == TYPE_BOOL and typeof(arg2) == TYPE_BOOL and (arg6 != null or typeof(arg5) == TYPE_FLOAT):
\t\tvar has_1 = bool(arg0)
\t\tvar has_2 = bool(arg1)
\t\tvar has_3 = bool(arg2)
\t\tvar is_set = has_1 and has_2 and has_3
\t\tvar total_vamp = 0
\t\tvar base_acc = 0
\t\tvar final_acc = 0
\t\tvar physical_damage = 0
\t\tvar cur_pool = 0.0
\t\tif typeof(arg3) == TYPE_ARRAY:
\t\t\tvar arr = arg3
\t\t\ttotal_vamp = (arr[0] if has_1 else 0) + (arr[1] if has_2 else 0) + (arr[2] if has_3 else 0)
\t\t\tbase_acc = int(arg4)
\t\t\tfinal_acc = base_acc + (2 if is_set else 0)
\t\t\tphysical_damage = int(arg5)
\t\t\tcur_pool = float(arg6) if arg6 != null else 0.0
\t\telse:
\t\t\ttotal_vamp = int(arg3)
\t\t\tbase_acc = int(arg4)
\t\t\tfinal_acc = base_acc + (2 if is_set else 0)
\t\t\tphysical_damage = 100
\t\t\tcur_pool = float(arg5) if arg5 != null else 0.0
\t\tvar gain = (float(physical_damage) * float(total_vamp)) / 100.0
\t\tvar new_pool = cur_pool + gain
\t\tvar heal_hp = 0
\t\tif new_pool >= 2.0:
\t\t\theal_hp = int(floor(new_pool))
\t\t\tnew_pool -= float(heal_hp)
\t\treturn {
\t\t\t"is_suit": is_set,
\t\t\t"set_bonus": is_set,
\t\t\t"is_set_active": is_set,
\t\t\t"bonus_accuracy_speed": 2 if is_set else 0,
\t\t\t"base_accuracy": base_acc,
\t\t\t"final_accuracy": final_acc,
\t\t\t"leech_rate": total_vamp,
\t\t\t"lifesteal_pct": total_vamp,
\t\t\t"total_vampire": total_vamp,
\t\t\t"physical_damage": physical_damage,
\t\t\t"gain": gain,
\t\t\t"healed_hp": heal_hp,
\t\t\t"hp_heal": heal_hp,
\t\t\t"rem_pool": new_pool,
\t\t\t"remaining_pool": new_pool,
\t\t\t"threshold": 2.0,
\t\t\t"subroutine": "sub_4C03B0 @ 0x4C08A9"
\t\t}"""

new_block = """\t# 8. 虹魔多参数形态 (场景 1199 & 1532)
\tif typeof(arg0) == TYPE_BOOL and typeof(arg1) == TYPE_BOOL and typeof(arg2) == TYPE_BOOL and (arg6 != null or typeof(arg5) == TYPE_FLOAT):
\t\tvar has_1: bool = bool(arg0)
\t\tvar has_2: bool = bool(arg1)
\t\tvar has_3: bool = bool(arg2)
\t\tvar is_set: bool = has_1 and has_2 and has_3
\t\tvar leech_pct: int = 0
\t\tvar base_acc: int = 0
\t\tvar damage: int = 0
\t\tvar pool: float = 0.0
\t\tvar threshold: float = 2.0

\t\tif typeof(arg3) == TYPE_ARRAY:
\t\t\tif has_3 and arg3.size() > 0: leech_pct += int(arg3[0])
\t\t\tif has_2 and arg3.size() > 1: leech_pct += int(arg3[1])
\t\t\tif has_1 and arg3.size() > 2: leech_pct += int(arg3[2])
\t\t\tbase_acc = int(arg4)
\t\t\tdamage = int(arg5)
\t\t\tpool = float(arg6)
\t\t\tthreshold = 1.0
\t\telif typeof(arg3) == TYPE_INT:
\t\t\tleech_pct = int(arg3)
\t\t\tdamage = int(arg4)
\t\t\tpool = float(arg5)
\t\t\tbase_acc = 0
\t\t\tthreshold = 2.0

\t\tvar bonus_acc: int = 2 if is_set else 0
\t\tvar final_acc: int = base_acc + bonus_acc
\t\tvar gain: float = (float(damage) / 100.0) * float(leech_pct)
\t\tvar new_pool: float = pool + gain
\t\tvar healed: int = 0
\t\tvar rem: float = new_pool
\t\tif new_pool >= threshold:
\t\t\thealed = int(floor(new_pool))
\t\t\trem = new_pool - float(healed)

\t\treturn {
\t\t\t"is_suit": is_set,
\t\t\t"set_bonus": is_set,
\t\t\t"is_set_active": is_set,
\t\t\t"bonus_accuracy_speed": bonus_acc,
\t\t\t"base_accuracy": base_acc,
\t\t\t"final_accuracy": final_acc,
\t\t\t"leech_rate": leech_pct,
\t\t\t"lifesteal_pct": leech_pct,
\t\t\t"total_vampire": leech_pct,
\t\t\t"physical_damage": damage,
\t\t\t"gain": gain,
\t\t\t"healed_hp": healed,
\t\t\t"hp_heal": healed,
\t\t\t"rem_pool": rem,
\t\t\t"remaining_pool": rem,
\t\t\t"threshold": threshold,
\t\t\t"subroutine": "sub_4C03B0 @ 0x4C08A9 & sub_4C1EF4 @ 0x4C2288 ~ 0x4C22F4"
\t\t}"""

if old_block in content:
    content = content.replace(old_block, new_block)
    with open("scripts/game/Player.gd", "w", encoding="utf-8") as f:
        f.write(content)
    print("Fixed hong mo 1532 calculation successfully!")
else:
    print("Error: old_block not found!")
