import re
h = open("src/UI/HudController.cs", encoding="utf-8", errors="ignore").read().splitlines()
for i, l in enumerate(h):
    if re.search(r"DealConfirm|DealEnd|SetLocked|OnTradeLock|TradeLockedChanged|LockTrade", l):
        print(f"{i+1}: {l.strip()}"[:150])
print("== TradeManager LockTrade ==")
t = open("src/Game/TradeManager.cs", encoding="utf-8", errors="ignore").read().splitlines()
for i, l in enumerate(t):
    if re.search(r"LockTrade|lastAction|tick|DateTime|Stopwatch|frequen", l, re.I):
        print(f"{i+1}: {l.strip()}"[:150])
print("== SpecialRings shape switch ==")
s = open("src/Game/SpecialRings.cs", encoding="utf-8", errors="ignore").read()
m = re.search(r"(static|public)[^{]*Shape[^{]*\{.{0,200}", s)
for m in re.finditer(r"case 1(11|12|19|39)\b.{0,80}", s):
    print(" ", m.group(0).strip()[:120])
for m in re.finditer(r"153|154|155|156|157|158|159|flame_ring|FlameRing|probe|ProbeNeck|double_speed|DoubleSpeed", s):
    print("RING:", s[max(0, m.start()-60):m.end()+40].replace("\n", " ")[:160])
    break