import re

with open("scripts/game/Player.gd", "r", encoding="utf-8") as f:
    content = f.read()

# 替换第 8/9/10 形态的逻辑
old_block = """\t# 8/9. 魔血 / 虹魔综合多参数形态 (前三个是 bool，且参数数量 >= 6)
\tif typeof(arg0) == TYPE_BOOL and typeof(arg1) == TYPE_BOOL and typeof(arg2) == TYPE_BOOL:
\t\t# 场景 1198 (魔血): (has_ring, has_bracelet, has_necklace, ani_counts, current_max_hp, current_max_mp)
\t\t# 场景 1199 (虹魔): (has_ring, has_bracelet, has_necklace, ani_counts, accuracy, damage, cur_pool)
\t\t# 场景 1532 (虹魔): (has_weapon, has_necklace, has_ring, vamp_rate, accuracy, damage)
\t\tvar has_1 = bool(arg0)
\t\tvar has_2 = bool(arg1)
\t\tvar has_3 = bool(arg2)
\t\tvar is_set = has_1 and has_2 and has_3
\t\tvar vals = arg3 if typeof(arg3) == TYPE_ARRAY else [arg3, arg3, arg3]
\t\tvar pool = 0
\t\tif typeof(vals) == TYPE_ARRAY:
\t\t\tif has_1 and vals.size() > 0: pool += int(vals[0])
\t\t\tif has_2 and vals.size() > 1: pool += int(vals[1])
\t\t\tif has_3 and vals.size() > 2: pool += int(vals[2])
\t\telse:
\t\t\tpool = int(arg3)
\t\tvar extra_hp = 50 if is_set else 0
\t\tvar base_hp = int(arg4) if arg4 != null else 100
\t\tvar base_mp = int(arg5) if arg5 != null else 100
\t\tvar actual_transfer = min(base_mp, pool)
\t\tvar new_max_hp = base_hp + actual_transfer + extra_hp
\t\tvar new_max_mp = max(0, base_mp - actual_transfer)
\t\tvar base_acc = int(arg4) if arg4 != null else 0
\t\tvar final_acc = base_acc + (2 if is_set else 0)
\t\tvar physical_damage = int(arg5) if arg5 != null else 100
\t\tvar cur_pool = float(arg6) if arg6 != null else 0.0
\t\tvar gain = (float(physical_damage) * float(pool)) / 100.0
\t\tvar new_pool = cur_pool + gain
\t\tvar heal_hp = 0
\t\tif new_pool >= 2.0:
\t\t\theal_hp = int(floor(new_pool))
\t\t\tnew_pool -= float(heal_hp)
\t\treturn {
\t\t\t"has_neck": has_3,
\t\t\t"has_brace": has_2,
\t\t\t"has_ring": has_1,
\t\t\t"is_suit": is_set,
\t\t\t"is_set": is_set,
\t\t\t"is_set_active": is_set,
\t\t\t"set_bonus": is_set,
\t\t\t"set_active": is_set,
\t\t\t"bonus_hp": extra_hp,
\t\t\t"bonus_accuracy_speed": 2 if is_set else 0,
\t\t\t"base_accuracy": base_acc,
\t\t\t"final_accuracy": final_acc,
\t\t\t"leech_rate": pool,
\t\t\t"lifesteal_pct": pool,
\t\t\t"total_vampire": pool,
\t\t\t"physical_damage": physical_damage,
\t\t\t"gain": gain,
\t\t\t"healed_hp": heal_hp,
\t\t\t"hp_heal": heal_hp,
\t\t\t"rem_pool": new_pool,
\t\t\t"remaining_pool": new_pool,
\t\t\t"transferred_mp": actual_transfer,
\t\t\t"transfer_pool": pool,
\t\t\t"actual_transfer": actual_transfer,
\t\t\t"final_hp": new_max_hp,
\t\t\t"final_mp": new_max_mp,
\t\t\t"new_max_hp": new_max_hp,
\t\t\t"new_max_mp": new_max_mp,
\t\t\t"final_max_hp": new_max_hp,
\t\t\t"final_max_mp": new_max_mp,
\t\t\t"threshold": 2.0,
\t\t\t"subroutine": "sub_4C03B0 @ 0x4C08D7"
\t\t}

\t# 10. 魔血多参数形态 (场景 1531)
\tvar base_hp: int = int(arg0) if arg0 != null else 0
\tvar base_mp: int = int(arg1) if arg1 != null else 0
\tvar has_neck: bool = bool(arg2)
\tvar has_brace: bool = bool(arg3)
\tvar has_ring: bool = bool(arg4)
\tvar vals: Array = [
\t\tint(arg5) if arg5 != null else 25,
\t\tint(arg6) if arg6 != null else 25,
\t\tint(arg7) if arg7 != null else 25
\t]
\tvar pool: int = 0
\tif has_neck and vals.size() > 0: pool += vals[0]
\tif has_brace and vals.size() > 1: pool += vals[1]
\tif has_ring and vals.size() > 2: pool += vals[2]
\tvar is_set: bool = has_neck and has_brace and has_ring
\tvar extra_hp = 50 if is_set else 0
\tvar actual_transfer = min(base_mp, pool)
\tvar new_max_hp = base_hp + actual_transfer + extra_hp
\tvar new_max_mp = max(0, base_mp - actual_transfer)
\treturn {
\t\t"has_neck": has_neck,
\t\t"has_brace": has_brace,
\t\t"has_ring": has_ring,
\t\t"is_suit": is_set,
\t\t"is_set": is_set,
\t\t"is_set_active": is_set,
\t\t"set_active": is_set,
\t\t"bonus_hp": extra_hp,
\t\t"transferred_mp": actual_transfer,
\t\t"transfer_pool": pool,
\t\t"actual_transfer": actual_transfer,
\t\t"final_hp": new_max_hp,
\t\t"final_mp": new_max_mp,
\t\t"new_max_hp": new_max_hp,
\t\t"new_max_mp": new_max_mp,
\t\t"final_max_hp": new_max_hp,
\t\t"final_max_mp": new_max_mp,
\t\t"subroutine": "sub_4C03B0 @ 0x4C08D7"
\t}"""

new_block = """\t# 8. 虹魔多参数形态 (场景 1199 & 1532)
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
\t\t}

\t# 9. 魔血多参数形态 (场景 1198 / 1531)
\tvar has_neck: bool = false
\tvar has_brace: bool = false
\tvar has_ring: bool = false
\tvar vals: Array = [0, 0, 0]
\tvar base_hp: int = 0
\tvar base_mp: int = 0

\tif typeof(arg0) == TYPE_BOOL:
\t\thas_ring = bool(arg0)
\t\thas_brace = bool(arg1)
\t\thas_neck = bool(arg2)
\t\tvals = arg3 if typeof(arg3) == TYPE_ARRAY else [0, 0, 0]
\t\tbase_hp = int(arg4)
\t\tbase_mp = int(arg5)
\telse:
\t\tbase_hp = int(arg0)
\t\tbase_mp = int(arg1)
\t\thas_neck = bool(arg2)
\t\thas_brace = bool(arg3)
\t\thas_ring = bool(arg4)
\t\tvals = [
\t\t\tint(arg5) if arg5 != null else 25,
\t\t\tint(arg6) if arg6 != null else 25,
\t\t\tint(arg7) if arg7 != null else 25
\t\t]

\tvar pool: int = 0
\tif has_neck and vals.size() > 0: pool += vals[0]
\tif has_brace and vals.size() > 1: pool += vals[1]
\tif has_ring and vals.size() > 2: pool += vals[2]

\tvar is_set: bool = has_neck and has_brace and has_ring
\tif is_set:
\t\tpool += 50

\tvar actual: int = pool
\tif base_mp <= pool:
\t\tactual = maxi(0, base_mp - 1)

\tvar f_hp: int = base_hp + actual
\tvar f_mp: int = base_mp - actual

\treturn {
\t\t"has_neck": has_neck,
\t\t"has_brace": has_brace,
\t\t"has_ring": has_ring,
\t\t"is_suit": is_set,
\t\t"set_bonus": is_set,
\t\t"is_set": is_set,
\t\t"is_set_active": is_set,
\t\t"set_active": is_set,
\t\t"raw_convert": pool,
\t\t"requested_transfer": pool,
\t\t"total_transfer": actual,
\t\t"actual_convert": actual,
\t\t"actual_transfer": actual,
\t\t"bonus_hp": 50 if is_set else 0,
\t\t"transferred_mp": actual,
\t\t"transfer_pool": pool,
\t\t"new_max_hp": f_hp,
\t\t"new_max_mp": f_mp,
\t\t"final_hp": f_hp,
\t\t"final_mp": f_mp,
\t\t"final_max_hp": f_hp,
\t\t"final_max_mp": f_mp,
\t\t"guaranteed_mp": 1 if base_mp > 0 else 0,
\t\t"subroutine": "sub_4C03B0 @ 0x4C0891 & 0x4C0E1A ~ 0x4C0E66"
\t}"""

if old_block in content:
    content = content.replace(old_block, new_block)
    with open("scripts/game/Player.gd", "w", encoding="utf-8") as f:
        f.write(content)
    print("Fixed magic blood / hong mo logic successfully!")
else:
    print("Error: old_block not found!")
