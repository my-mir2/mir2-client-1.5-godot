import json, re, os

dfm = json.load(open('scratch/official_dfm_tree.json', encoding='utf-8'))
dfm_map = {}
for c in dfm['children']:
    dfm_map[c['name']] = c

print("=== 1. DCreateChr (创角界面) 官方 DFM 坐标 ===")
create_chr_keys = ['DCreateChr', 'DccWarrior', 'DccWizzard', 'DccMonk', 'DccReserved', 'DccMale', 'DccFemale', 'DccLeftHair', 'DccRightHair', 'DccOk', 'DccClose']
for k in create_chr_keys:
    if k in dfm_map:
        p = dfm_map[k]['props']
        print(f"  {k:20} Left={p.get('Left', 0):<4} Top={p.get('Top', 0):<4} Width={p.get('Width', 0):<4} Height={p.get('Height', 0):<4}")

print("\n=== 2. DSelectChr (选角神龛界面) 官方 DFM 坐标 ===")
sel_chr_keys = ['DSelectChr', 'DscSelect1', 'DscSelect2', 'DscStart', 'DscNewChr', 'DscEraseChr', 'DscCredits', 'DscExit']
for k in sel_chr_keys:
    if k in dfm_map:
        p = dfm_map[k]['props']
        print(f"  {k:20} Left={p.get('Left', 0):<4} Top={p.get('Top', 0):<4} Width={p.get('Width', 0):<4} Height={p.get('Height', 0):<4}")

print("\n=== 3. DBottom (HUD 底栏与快捷键) 官方 DFM 坐标 ===")
bottom_keys = ['DBottom', 'DMyState', 'DMyBag', 'DMyMagic', 'DOption', 'DGold', 'DBelt1', 'DBelt2', 'DBelt3', 'DBelt4', 'DBelt5', 'DBelt6', 'DBotMiniMap', 'DBotTrade', 'DBotGuild', 'DBotGroup', 'DBotPlusAbil', 'DBotLogout', 'DBotExit']
for k in bottom_keys:
    if k in dfm_map:
        p = dfm_map[k]['props']
        print(f"  {k:20} Left={p.get('Left', 0):<4} Top={p.get('Top', 0):<4} Width={p.get('Width', 0):<4} Height={p.get('Height', 0):<4}")

print("\n=== 4. DUserState1 (装备面板/观人) 官方 DFM 坐标 ===")
user_state_keys = ['DUserState1', 'DCloseUS1', 'DWeaponUS1', 'DHelmetUS1', 'DNecklaceUS1', 'DDressUS1', 'DLightUS1', 'DArmringRUS1', 'DRingRUS1', 'DArmringLUS1', 'DRingLUS1']
for k in user_state_keys:
    if k in dfm_map:
        p = dfm_map[k]['props']
        print(f"  {k:20} Left={p.get('Left', 0):<4} Top={p.get('Top', 0):<4} Width={p.get('Width', 0):<4} Height={p.get('Height', 0):<4}")

print("\n=== 5. DItemBag (背包面板) 官方 DFM 坐标 ===")
bag_keys = ['DItemBag', 'DItemGrid', 'DCloseBag', 'DGold', 'DRepairItem']
for k in bag_keys:
    if k in dfm_map:
        p = dfm_map[k]['props']
        print(f"  {k:20} Left={p.get('Left', 0):<4} Top={p.get('Top', 0):<4} Width={p.get('Width', 0):<4} Height={p.get('Height', 0):<4}")

print("\n=== 6. DStateWin (状态面板 F8) 官方 DFM 坐标 ===")
state_keys = ['DStateWin', 'DCloseState', 'DPrevState', 'DNextState', 'DStMag1', 'DStMag2', 'DStMag3', 'DStMag4', 'DStMag5', 'DStPageUp', 'DStPageDown']
for k in state_keys:
    if k in dfm_map:
        p = dfm_map[k]['props']
        print(f"  {k:20} Left={p.get('Left', 0):<4} Top={p.get('Top', 0):<4} Width={p.get('Width', 0):<4} Height={p.get('Height', 0):<4}")
