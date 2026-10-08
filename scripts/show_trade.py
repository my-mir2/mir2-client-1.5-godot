import re
root = r"D:\vbcoding\mir2\mir2-client1.5-delphi\mir2-client1.5-delphi"
def show(path, pat, ctx=3):
    ps = [path] if isinstance(path, str) else path
    for p in ps:
        fp = root + "\\" + p
        try:
            lines = open(fp, encoding="utf-8", errors="ignore").read().splitlines()
        except Exception as e:
            print("MISS", p, e); continue
        print("=" * 20, p)
        for i, l in enumerate(lines):
            if re.search(pat, l, re.I):
                for j in range(max(0, i - ctx), min(len(lines), i + ctx + 1)):
                    print(f"{j+1}: {lines[j]}")
                print("---")
show("src\\Net\\ProtocolConsts.cs", r"DEAL|TRADE|Deal|Trade", 2)
show(["src\\Net\\PacketEncoder.cs", "src\\Net\\ServerMessageRouter.cs"], r"Deal|Trade", 4)
show("src\\UI\\GuildPanelView.cs", r"sub_|Prguse|Position|Size", 1)
