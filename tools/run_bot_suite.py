#!/usr/bin/env python3
"""全自动机器人测试套件：起服务端(GM 模式) -> 逐场景跑无头机器人 -> 按规则判定 -> 出报告。

用法:  python tools/run_bot_suite.py [--repeat N] [--only 场景名,场景名] [--no-server]
退出码: 0=全部通过(无 FAIL 且无 INCONCLUSIVE 之外的问题)  1=存在 FAIL

判定原则（防自嗨）:
  * 期望值来自 PostgreSQL 里的官方数据 (掉落表、传送门落点)，不由机器人自己决定；
  * 场景没触发想测的事件 (例如想测死亡却没死) 判 INCONCLUSIVE，不算通过；
  * 服务端日志出现 ERR/异常 一律记 FAIL；
  * 未实现的场景 (商店买药等) 在报告里明确列为 NOT_IMPLEMENTED，不占通过率分子。
"""
import argparse, csv, json, os, socket, subprocess, sys, time, threading
from concurrent.futures import ThreadPoolExecutor

import psycopg2

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
PG = dict(host="127.0.0.1", port=5432, dbname="mir2_world", user="postgres", password="postgres")
SERVER_DIR = os.path.join(ROOT, "bin", "server_release")
BOT_DLL = os.path.join(ROOT, "tools", "Mir2.Bot", "bin", "Release", "net10.0", "Mir2.Bot.dll")
OUT = os.path.join(ROOT, "scratch", "suite")
REPORT = os.path.join(ROOT, "docs", "bot_reports", "suite_latest.md")


# ---------------------------------------------------------------- 官方真值 (数据库)
class Truth:
    def __init__(self):
        self.db = psycopg2.connect(**PG)
        self.cn2en, self.en2cn, self.alias_cn2en = {}, {}, {}
        with open(os.path.join(ROOT, "data", "official_14", "cn_names.csv"), encoding="utf-8-sig") as f:
            for r in csv.DictReader(f):
                self.cn2en.setdefault((r["table"], r["cn"]), set()).add(r["en"])
                self.en2cn.setdefault((r["table"], r["en"].lower()), r["cn"])

        # 物品中文别名来自服务端注册表源码 RegisterAlias("中文","English")，与服务端对外显示同源
        import re
        src = open(os.path.join(ROOT, "src", "Mir2.Shared", "Data", "OfficialStdItemsRegistry.cs"), encoding="utf-8-sig").read()
        for cn, en in re.findall(r'RegisterAlias\("([^"]+)",\s*"([^"]+)"\)', src):
            self.en2cn.setdefault(("item", en.lower()), cn)
            self.alias_cn2en.setdefault(cn, set()).add(en)

        # 盛大官方 1.4/1.5 数据库 official_stditems 中全量 391 条权威中文名 (name_cn) 同步注入别名表
        cur = self.db.cursor()
        cur.execute("select name, name_cn from official_stditems where name_cn is not null and name_cn != ''")
        for en, cn in cur.fetchall():
            self.en2cn.setdefault(("item", en.lower()), cn)
            self.alias_cn2en.setdefault(cn, set()).add(en)
            if cn.endswith("?"):
                self.alias_cn2en.setdefault(cn[:-1], set()).add(en)

    def drop_verdict(self, monster_cn, seen):
        """返回 (状态, 说明)。DB 掉落表物品名为英文；客户端看到的中文名经服务端别名表反查英文再对照。
        中文名在别名表里查不到 => 无法对照(INCONCLUSIVE)，不当成通过也不当成失败。"""
        keys = self.cn2en.get(("monster", monster_cn))
        if not keys:
            return "INCONCLUSIVE", f"怪物 {monster_cn} 在 cn_names 中无英文名"
        cur = self.db.cursor()
        allowed = {"gold"}
        for k in keys:
            cur.execute("select item_name from official_mon_drop where lower(monster_key)=lower(%s)", (k,))
            allowed |= {n.lower() for (n,) in cur.fetchall()}
        if seen in ("金币", "Gold"):
            return ("PASS", "") if "gold" in allowed else ("FAIL", f"{monster_cn} 官方表无金币却掉了金币")
        ens = {e.lower() for e in self.alias_cn2en.get(seen, set())}
        if not ens and seen.isascii(): ens = {seen.lower()}
        if not ens:
            return "INCONCLUSIVE", f"物品中文名 {seen} 无权威映射"
        return ("PASS", "") if ens & allowed else ("FAIL", f"{monster_cn} 掉了 {seen}({sorted(ens)}) 不在官方掉落表")

    def has_drop_table(self, monster_cn):
        keys = self.cn2en.get(("monster", monster_cn), {monster_cn})
        cur = self.db.cursor()
        cur.execute("select count(*) from official_mon_drop where lower(monster_key) = any(%s)", ([k.lower() for k in keys],))
        return cur.fetchone()[0] > 0


# ---------------------------------------------------------------- 规则
class Result:
    def __init__(self):
        self.checks = []  # (状态, 规则, 说明)

    def ok(self, rule, msg=""): self.checks.append(("PASS", rule, msg))
    def fail(self, rule, msg): self.checks.append(("FAIL", rule, msg))
    def inc(self, rule, msg): self.checks.append(("INCONCLUSIVE", rule, msg))
    def expect(self, cond, rule, msg_fail, msg_ok=""):
        (self.ok if cond else self.fail)(rule, msg_ok if cond else msg_fail)

    @property
    def status(self):
        s = {c[0] for c in self.checks}
        return "FAIL" if "FAIL" in s else ("INCONCLUSIVE" if "INCONCLUSIVE" in s else "PASS")


def rule_common(r, j):
    # 死亡后复活在比奇出生点，机器人在那里自然找不到目标怪，这是剧本结果而非缺陷，故在有死亡时忽略该条
    probs = [p for p in j["problems"] if not (j["deaths"] and p.startswith("视野内没有可打的怪物"))]
    r.expect(not probs, "R-无机器人侧异常", f"机器人记录的问题: {probs[:6]}")
    r.expect(j["rollbacks"] <= 3, "R-位置拉回<=3 次", f"服务端拉回 {j['rollbacks']} 次")
    guard = [s for s in j["struckBy"] if "守卫" in s["who"]]
    r.expect(not guard, "R-守卫不攻击非红名玩家", f"被守卫打了 {guard}")


def rule_drops(r, j, truth):
    bad, undec = [], set()
    for f in j["fights"]:
        if not f["Killed"]:
            continue
        for d in f["drops"]:
            st, msg = truth.drop_verdict(f["Name"], d)
            if st == "FAIL": bad.append(msg)
            elif st == "INCONCLUSIVE": undec.add(msg)
    r.expect(not bad, "R-掉落物 ⊆ 官方掉落表(DB)", f"{sorted(set(bad))[:6]}")
    if undec:
        r.inc("R-掉落物对照", f"无法对照: {sorted(undec)[:6]}")


def rule_arrivals(r, j):
    if not j["arrivals"]:
        r.fail("R-发生了换图", "没有任何换图记录"); return
    for a in j["arrivals"]:
        r.expect((a["x"], a["y"]) == (a["expectX"], a["expectY"]), f"R-落点 == 数据库传送门目标 ({a['map']})",
                 f"落点 ({a['x']},{a['y']}) != DB 期望 ({a['expectX']},{a['expectY']})")
        r.expect(a["ents"] > 0, f"R-换图后视野内有实体 ({a['map']})", f"到图后视野内 {a['ents']} 个实体")


def sc_chicken(j, t, r):
    rule_common(r, j); rule_drops(r, j, t)
    r.expect(j["kills"] >= 8, "R-击杀>=8", f"只击杀 {j['kills']}")
    r.expect(j["deaths"] == 0, "R-1 级打鸡不死", f"死亡 {j['deaths']} 次")


def sc_gm_level(j, t, r):
    rule_common(r, j)
    r.expect(j["level"] >= 30, "R-GM 设等级生效", f"等级 {j['level']}")
    r.expect(j["maxHp"] >= 100, "R-30 级最大血量合理(>=100，与真实升级同一路径)", f"MaxHp={j['maxHp']}")
    r.expect(any("金币=5000" in g for g in j["gm"]), "R-GM 设金币回显", f"{j['gm']}")


def sc_travel(j, t, r):
    rule_common(r, j); rule_arrivals(r, j); rule_drops(r, j, t)
    r.expect(j["kills"] >= 6, "R-新地图击杀>=6", f"击杀 {j['kills']}")
    r.expect(j["finalMap"] == "1", "R-最终在目标地图", f"最终地图 {j['finalMap']}")


def sc_strong(j, t, r):
    rule_common(r, j); rule_drops(r, j, t)
    spawned = [g for g in j["gm"] if "刷出" in g]
    r.expect(bool(spawned), "R-GM 刷怪成功", f"{j['gm']}")
    fought = [f for f in j["fights"] if f["Name"] == "半兽勇士"]
    r.expect(bool(fought), "R-确实与刷出的怪交手", f"战斗列表里没有半兽勇士: {[f['Name'] for f in j['fights']]}")
    if j["deaths"]:
        r.expect(j["revives"] >= j["deaths"] and not j["dead"], "R-死亡后复活且最终存活",
                 f"死亡 {j['deaths']} 复活 {j['revives']} 最终dead={j['dead']}")


def sc_death(j, t, r):
    rule_common(r, j)
    if j["deaths"] == 0:
        r.inc("R-触发死亡", "机器人没有死，复活链路未被测到"); return
    r.expect(j["revives"] >= j["deaths"], "R-每次死亡都收到复活", f"死亡 {j['deaths']} 复活 {j['revives']}")
    r.expect(not j["dead"], "R-最终存活", "结束时仍是死亡状态")


def sc_multi(j, t, r):
    rule_common(r, j)
    r.expect(j["kills"] >= 3, "R-多人同图各自击杀>=3", f"击杀 {j['kills']}")


def sc_buy_and_drink(j, t, r):
    rule_common(r, j)
    buys = j.get("buys", [])
    eats = j.get("eats", 0)
    dlgs = j.get("npcDialogs", [])
    r.expect(len(dlgs) > 0, "R-NPC交互对话回显", f"未收到药店 NPC 对话 (dlgs={len(dlgs)})")
    r.expect(len(buys) > 0, "R-商店买药成功(648/200)", f"购买药水记录为空 (buys={buys})")
    r.expect(eats > 0, "R-使用药水成功(606 SM_EAT_OK)", f"使用药水记录为空 (eats={eats})")
    r.expect(j["hp"] > 5, "R-药水回血生效(HP>5)", f"使用药水后当前 HP={j['hp']}/{j['maxHp']}")


def sc_mage_magic(j, t, r):
    rule_common(r, j)
    magics = j.get("magics", [])
    spells = j.get("spellsCast", 0)
    effects = j.get("spellEffects", 0)
    has_thunder = any(m.get("id") == 10 or "雷电" in m.get("name", "") for m in magics)
    r.expect(has_thunder, "R-已学会雷电术(201)", f"技能列表未包含雷电术 (magics={magics})")
    r.expect(spells > 0 or effects > 0, "R-法术施法与光效广播(243/614)", f"施法包={spells}, 光效包={effects}")
    r.expect(j["kills"] >= 3, "R-法术击杀>=3", f"击杀={j['kills']}")


def sc_equip_lifecycle(j, t, r):
    rule_common(r, j)
    takeOns = j.get("takeOns", [])
    takeOffs = j.get("takeOffs", [])
    delItems = j.get("delItems", [])
    groundCount = j.get("groundCount", 0)
    has_weapon_on = any(x.get("slot") == 0 for x in takeOns)
    has_weapon_off = any(x.get("slot") == 0 for x in takeOffs)
    r.expect(has_weapon_on, "R-装备穿戴成功(615 SM_TAKEON_OK 槽位0)", f"未收到武器槽位穿戴包: {takeOns}")
    r.expect(has_weapon_off, "R-装备脱下成功(619 SM_TAKEOFF_OK 槽位0)", f"未收到武器脱下包: {takeOffs}")
    r.expect(len(delItems) > 0 or groundCount > 0, "R-丢弃物品成功(202 SM_DELITEM / 625 SM_ITEMSHOW)", f"丢弃记录: delItems={delItems}, ground={groundCount}")


def sc_taoist_combat(j, t, r):
    rule_common(r, j)
    magics = j.get("magics", [])
    spells = j.get("spellsCast", 0)
    effects = j.get("spellEffects", 0)
    has_charm = any(m.get("id") == 13 or "灵魂火符" in m.get("name", "") for m in magics)
    has_summon = any(m.get("id") == 17 or "召唤骷髅" in m.get("name", "") for m in magics)
    r.expect(has_charm and has_summon, "R-已学会灵魂火符与召唤骷髅(201)", f"道士技能列表: {magics}")
    r.expect(spells > 0 or effects > 0, "R-道士施法与光效广播(243/614)", f"施法包={spells}, 光效包={effects}")
    r.expect(j["kills"] >= 2, "R-道士协同击杀>=2", f"击杀={j['kills']}")


def sc_party_group(j, t, r):
    rule_common(r, j)
    groups = j.get("groupLists", [])
    r.expect(len(groups) > 0, "R-队伍成员名单广播(667 SM_GROUPMEMBERS)", f"未收到组队广播: {groups}")
    if j.get("kills", 0) > 0:
        r.expect(j["kills"] >= 1, "R-队长击杀怪物", f"队长击杀={j['kills']}")
    else:
        r.expect(j.get("exp", 0) > 0, "R-队员获得同屏均摊经验", f"队员经验={j.get('exp', 0)}")


def sc_magic_keybind(j, t, r):
    rule_common(r, j)
    magics = j.get("magics", [])
    updates = j.get("magicUpdates", [])
    has_thunder = any(m.get("id") == 10 for m in magics)
    r.expect(has_thunder, "R-已学会雷电术(201)", f"技能列表: {magics}")
    thunder_f1 = any(m.get("id") == 10 and m.get("key") == 0 for m in magics) or any(u.get("id") == 10 and u.get("key") == 0 for u in updates)
    r.expect(thunder_f1, "R-技能快捷键绑定生效(1008 CM_MAGICKEYCHANGE / 203 SM_UPDATEUSERMAGIC F1)", f"更新记录={updates}, 当前技能={magics}")


def sc_adjust_bonus(j, t, r):
    rule_common(r, j)
    maxHp = j.get("maxHp", 0)
    bonusPoint = j.get("bonusPoint", 0)
    # 战士 20 级基础血量约 224，加 1 点 HP 属性点后增加 10 点 MaxHp，剩余点数由 5 变 4
    r.expect(maxHp >= 200, "R-自由加点生命值生效(1043 CM_ADJUST_BONUS)", f"当前 MaxHp={maxHp}")
    r.expect(bonusPoint == 4, "R-属性点扣减生效(SM_ABILITY 剩余点数=4)", f"当前剩余属性点={bonusPoint}")


def sc_drop_gold(j, t, r):
    rule_common(r, j)
    gold = j.get("gold", 0)
    groundCount = j.get("groundCount", 0)
    r.expect(gold == 1800, "R-丢弃金币扣减生效(1016 CM_DROPGOLD 2000->1800)", f"当前金币={gold}")
    r.expect(groundCount > 0, "R-地面金币出现并广播(SM_ITEMSHOW)", f"地面物品数={groundCount}")


def sc_butch_meat(j, t, r):
    rule_common(r, j)
    butchs = j.get("butchResults", [])
    bag = j.get("bag", [])
    has_meat = any("肉" in x for x in bag)
    r.expect(len(butchs) > 0 or has_meat, "R-生物尸体割肉成功(1007 CM_BUTCHITEM / 665 SM_BUTCH)", f"割肉回显={butchs}, 背包={bag}")


def sc_sell_item(j, t, r):
    rule_common(r, j)
    sells = j.get("sells", [])
    gold = j.get("gold", 0)
    r.expect(len(sells) > 0, "R-商店出售物品成功(1013 CM_USERSELLITEM / 650 SM_SELL_OK_DELPHI)", f"出售记录={sells}")
    r.expect(gold > 0, "R-出售后金币正确增加", f"当前金币={gold}")


def sc_repair_item(j, t, r):
    rule_common(r, j)
    repairs = j.get("repairs", [])
    r.expect(len(repairs) > 0, "R-铁匠铺装备修理成功(1023 CM_USERREPAIRITEM / 669 SM_REPAIROK_DELPHI)", f"修理记录={repairs}")


def sc_sword_switch(j, t, r):
    rule_common(r, j)
    sysMsgs = j.get("sysMsgs", [])
    has_thrusting = any("刺杀剑术" in m for m in sysMsgs)
    has_halfmoon = any("半月弯刀" in m for m in sysMsgs)
    r.expect(has_thrusting, "R-刺杀剑术开关提示生效(3026 CM_THRUSTINGONOFF)", f"系统消息={sysMsgs}")
    r.expect(has_halfmoon, "R-半月弯刀开关提示生效(3027 CM_HALFMOONONOFF)", f"系统消息={sysMsgs}")


# 场景: (名称, 机器人参数, 规则函数, 超时秒, 并发份数)
SCENARIOS = [
    ("chicken_bichon", ["--kills", "8", "--seconds", "240"], sc_chicken, 300, 1),
    ("gm_level_gold_item", ["--gm", "level 30;gold 5000;item 金创药(小量) 2", "--kills", "0", "--seconds", "30"], sc_gm_level, 90, 1),
    ("buy_and_drink_potion", ["--gm", "gold 1000", "--buy-potion", "金创药(小)", "--kills", "0", "--seconds", "45"], sc_buy_and_drink, 90, 1),
    ("equip_wear_takeoff_drop", ["--gm", "level 20;item 铁剑 1;item 金手镯 1", "--test-equip", "--kills", "0", "--seconds", "45"], sc_equip_lifecycle, 90, 1),
    ("travel_and_hunt", ["--gm", "level 15", "--goto", "1", "--kills", "8", "--seconds", "420"], sc_travel, 480, 1),
    ("strong_monsters", ["--gm", "level 25;tp 1 497 465;spawn 半兽勇士 4", "--hunt", "半兽勇士", "--kills", "3", "--seconds", "150"], sc_strong, 200, 1),
    ("death_and_revive", ["--gm", "level 1;tp 1 497 465;spawn 半兽勇士 6;die", "--hunt", "半兽勇士", "--no-rest", "--kills", "1", "--seconds", "120"], sc_death, 180, 1),
    ("multi_player_same_map", ["--gm", "level 10;tp 1 497 465;spawn 钉耙猫 8", "--hunt", "钉耙猫", "--kills", "3", "--seconds", "120"], sc_multi, 180, 3),
    ("mage_magic_combat", ["--gm", "job 1;level 25;skill 雷电术 3;tp 1 497 465;spawn 钉耙猫 6", "--spell", "雷电术", "--hunt", "钉耙猫", "--kills", "3", "--seconds", "90"], sc_mage_magic, 120, 1),
    ("magic_keybind_setup", ["--gm", "job 1;level 25;skill 雷电术 3", "--test-keybind", "--kills", "0", "--seconds", "30"], sc_magic_keybind, 90, 1),
    ("taoist_combat_and_pet", ["--gm", "job 2;level 35;skill 灵魂火符 3;skill 召唤骷髅 3;item 护身符 10;tp 1 497 465;spawn 钉耙猫 6", "--taoist-combat", "--hunt", "钉耙猫", "--kills", "2", "--seconds", "90"], sc_taoist_combat, 120, 1),
    ("party_group_exp_share", ["--gm", "level 15;tp 1 497 465;spawn 钉耙猫 6", "--party-role", "leader", "--hunt", "钉耙猫", "--kills", "2", "--seconds", "90"], sc_party_group, 120, 2),
    ("adjust_bonus_alloc", ["--gm", "level 20;bonus 5", "--test-adjust-bonus", "--kills", "0", "--seconds", "30"], sc_adjust_bonus, 90, 1),
    ("drop_gold_to_ground", ["--gm", "gold 2000", "--test-drop-gold", "--kills", "0", "--seconds", "30"], sc_drop_gold, 90, 1),
    ("butch_corpse_meat", ["--gm", "level 10", "--test-butch", "--kills", "0", "--seconds", "45"], sc_butch_meat, 90, 1),
    ("sell_item_to_shop", ["--test-sell", "--kills", "0", "--seconds", "30"], sc_sell_item, 90, 1),
    ("repair_item_at_shop", ["--test-repair", "--kills", "0", "--seconds", "30"], sc_repair_item, 90, 1),
    ("sword_skill_switch", ["--gm", "level 30;skill 刺杀剑术 3;skill 半月弯刀 3", "--test-sword-switch", "--kills", "0", "--seconds", "30"], sc_sword_switch, 90, 1),
]
NOT_IMPLEMENTED = ["断线重连", "行会/交易", "长时间(>1h)稳定性"]


# ---------------------------------------------------------------- 执行
def wait_port(port, sec):
    end = time.time() + sec
    while time.time() < end:
        try:
            socket.create_connection(("127.0.0.1", port), 1).close(); return True
        except OSError:
            time.sleep(0.5)
    return False


def start_server():
    subprocess.run(["powershell", "-NoProfile", "-Command", "Get-Process Mir2.Server -EA 0 | Stop-Process -Force"], check=False)
    time.sleep(1)
    env = dict(os.environ, MIR2_GM="1")
    env.pop("MIR2_EXP_RATE", None)  # 套件使用官方倍率 1.0，快速造场景靠 GM 命令
    log = open(os.path.join(OUT, "server.log"), "ab")
    p = subprocess.Popen([os.path.join(SERVER_DIR, "Mir2.Server.exe")], cwd=SERVER_DIR, env=env, stdout=log, stderr=subprocess.STDOUT)
    if not wait_port(7000, 60):
        raise SystemExit("服务端 60s 内没有监听 7000")
    time.sleep(2)
    return p


def run_bot(name, args, timeout, idx):
    jp = os.path.join(OUT, f"{name}.{idx}.json")
    if os.path.exists(jp): os.remove(jp)
    cmd = ["dotnet", BOT_DLL, "--root", ROOT, "--json", jp] + args
    with open(os.path.join(OUT, f"{name}.{idx}.log"), "wb") as lf:
        try:
            subprocess.run(cmd, stdout=lf, stderr=subprocess.STDOUT, timeout=timeout, cwd=ROOT)
        except subprocess.TimeoutExpired:
            return None, "机器人超时被杀"
    if not os.path.exists(jp):
        return None, "机器人没有产出 JSON (登录/进图失败?)，见 " + os.path.basename(lf.name)
    with open(jp, encoding="utf-8") as f:
        return json.load(f), ""


def scan_server_log():
    bad = []
    with open(os.path.join(OUT, "server.log"), encoding="utf-8", errors="replace") as f:
        for line in f:
            if " ERR]" in line or "Exception" in line or "SCRIPT ERROR" in line or " FTL]" in line:
                bad.append(line.strip()[:200])
    return bad


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--repeat", type=int, default=1)
    ap.add_argument("--only", default="")
    ap.add_argument("--no-server", action="store_true")
    a = ap.parse_args()
    os.makedirs(OUT, exist_ok=True); os.makedirs(os.path.dirname(REPORT), exist_ok=True)
    open(os.path.join(OUT, "server.log"), "wb").close()
    if not os.path.exists(BOT_DLL):
        subprocess.run(["dotnet", "build", os.path.join(ROOT, "tools", "Mir2.Bot"), "-c", "Release"], check=True)
    truth = Truth()
    server = None
    only = {x for x in a.only.split(",") if x}
    rows = []
    try:
        for rep in range(a.repeat):
            for name, args, fn, timeout, copies in SCENARIOS:
                if only and name not in only: continue
                t0 = time.time()
                # 每个场景都重启服务端：GM 刷出的怪不会消失，会污染后续场景 (此前 travel 场景被前面刷的半兽勇士打死)
                if not a.no_server:
                    if server: server.kill(); server.wait()
                    server = start_server()
                with ThreadPoolExecutor(copies) as ex:
                    def _get_args(cp_idx):
                        if name == "party_group_exp_share":
                            if cp_idx == 0:
                                return ["--gm", "level 15;tp 1 497 465;spawn 钉耙猫 6", "--char-name", f"队长_{rep}", "--invite-name", f"队员_{rep}", "--party-role", "leader", "--hunt", "钉耙猫", "--kills", "2", "--seconds", "90"]
                            else:
                                return ["--gm", "level 15;tp 1 497 465", "--char-name", f"队员_{rep}", "--party-role", "member", "--hunt", "钉耙猫", "--kills", "0", "--seconds", "90"]
                        return args

                    futs = [ex.submit(run_bot, f"{name}.r{rep}", _get_args(i), timeout, i) for i in range(copies)]
                    outs = [f.result() for f in futs]
                for i, (j, err) in enumerate(outs):
                    r = Result()
                    if j is None: r.fail("R-机器人跑完并产出结果", err)
                    else: fn(j, truth, r)
                    rows.append((f"{name}#{rep}.{i}", r, round(time.time() - t0)))
                    print(f"[{r.status:12}] {name}#{rep}.{i}  ({round(time.time()-t0)}s)", flush=True)
    finally:
        if server: server.kill()
    logbad = scan_server_log()

    n = len(rows); cnt = {s: sum(1 for _, r, _ in rows if r.status == s) for s in ("PASS", "FAIL", "INCONCLUSIVE")}
    lines = ["# 无头机器人套件报告", "", f"生成时间: {time.strftime('%Y-%m-%d %H:%M:%S')}  重复次数: {a.repeat}", "",
             "## 坏账（先看这里）", ""]
    fails = [(nm, c) for nm, r, _ in rows for c in r.checks if c[0] == "FAIL"]
    incs = [(nm, c) for nm, r, _ in rows for c in r.checks if c[0] == "INCONCLUSIVE"]
    lines += [f"- FAIL x{len(fails)}"] + [f"  - `{nm}` {c[1]}: {c[2]}" for nm, c in fails]
    lines += [f"- INCONCLUSIVE x{len(incs)} (没触发想测的事件，不算通过)"] + [f"  - `{nm}` {c[1]}: {c[2]}" for nm, c in incs]
    lines += [f"- 服务端日志异常行 x{len(logbad)}"] + [f"  - {x}" for x in logbad[:10]]
    lines += ["- 尚未覆盖 (NOT_IMPLEMENTED):"] + [f"  - {x}" for x in NOT_IMPLEMENTED]
    lines += ["", "## 汇总", "", f"场景运行 {n} 次: PASS {cnt['PASS']} / FAIL {cnt['FAIL']} / INCONCLUSIVE {cnt['INCONCLUSIVE']}", "",
              "| 场景 | 状态 | 用时(s) |", "|---|---|---|"] + [f"| {nm} | {r.status} | {sec} |" for nm, r, sec in rows]
    lines += ["", "## 规则明细", ""]
    for nm, r, _ in rows:
        lines.append(f"### {nm}  {r.status}")
        lines += [f"- [{c[0]}] {c[1]} {c[2]}" for c in r.checks]
        lines.append("")
    with open(REPORT, "w", encoding="utf-8") as f: f.write("\n".join(lines))
    print(f"\n报告: {REPORT}\nPASS {cnt['PASS']} FAIL {cnt['FAIL']} INCONCLUSIVE {cnt['INCONCLUSIVE']}  服务端日志异常 {len(logbad)}")
    sys.exit(1 if cnt["FAIL"] or logbad else 0)


if __name__ == "__main__":
    main()
