import re
root = r"D:\vbcoding\mir2\mir2-client1.5-delphi\mir2-client1.5-delphi"
r = open(root + r"\src\Net\ServerMessageRouter.cs", encoding="utf-8").read().splitlines()
for i in range(0, 110):
    print(f"{i+1}: {r[i]}")
print("=== HudController guild/addchat ===")
h = open(root + r"\src\UI\HudController.cs", encoding="utf-8").read().splitlines()
for i, l in enumerate(h):
    if re.search(r"public void (AddChat|ToggleGuild|OnServerTrade|OpenTrade|UpdateGold)", l):
        print(f"{i+1}: {l.strip()}")
print("=== GuildManager full ===")
print(open(root + r"\src\Game\GuildManager.cs", encoding="utf-8").read()[:4000])
