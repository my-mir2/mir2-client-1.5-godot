import re
root = r"D:\vbcoding\mir2\mir2-client1.5-delphi\mir2-client1.5-delphi"
r = open(root + r"\src\Net\ServerMessageRouter.cs", encoding="utf-8").read().splitlines()
print("== router guild cases ==")
for i, l in enumerate(r):
    if re.search(r"GUILD|Guild", l):
        print(f"{i+1}: {l}")
print("== router file len ==", len(r))
h = open(root + r"\src\UI\HudController.cs", encoding="utf-8").read().splitlines()
print("== hud guild methods ==")
for i, l in enumerate(h):
    if re.search(r"Guild|guild", l) and re.search(r"public|private|void|bool|string", l):
        print(f"{i+1}: {l.strip()}"[:160])
g = open(root + r"\src\Game\GuildManager.cs", encoding="utf-8").read().splitlines()
print("== manager methods ==")
for i, l in enumerate(g):
    if re.search(r"public", l):
        print(f"{i+1}: {l.strip()}"[:160])
