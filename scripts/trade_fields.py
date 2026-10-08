t = open("src/UI/TradePanelView.cs", encoding="utf-8", errors="ignore").read().splitlines()
for i, l in enumerate(t[:70]):
    print(f"{i+1}: {l}")