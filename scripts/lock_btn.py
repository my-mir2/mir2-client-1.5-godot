t = open("src/UI/TradePanelView.cs", encoding="utf-8", errors="ignore").read().splitlines()
for i in range(420, 465):
    print(f"{i+1}: {t[i]}")
print("== fields top ==")
for i in range(24, 80):
    if "private" in t[i] or "public" in t[i]:
        print(f"{i+1}: {t[i]}")