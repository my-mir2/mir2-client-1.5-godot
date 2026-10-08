import re
h = open("src/UI/HudController.cs", encoding="utf-8", errors="ignore").read()
for m in re.finditer(r".{0,60}(TradeManagerInstance|tm\.LockTrade|SetLocked|OnServerTradeSuccess|TradeConfirmRequested).{0,60}", h):
    s = m.group(0).strip().replace("\n", " ")
    if len(s) < 200:
        print(s)
print("== TradePanel SetLocked/EndDeal ==")
t = open("src/UI/TradePanelView.cs", encoding="utf-8", errors="ignore").read().splitlines()
for i, l in enumerate(t):
    if re.search(r"EndDealConfirmed|SetLocked|event Action", l):
        print(f"{i+1}: {l.strip()}"[:140])