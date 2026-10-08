import json, os
from PIL import Image
d = json.load(open("scratch/ida_status_dump.json", encoding="utf-8"))
for x in d:
    print(x["ea"], x["is_func"], x.get("name"), x.get("range"))
for n in ["0370", "0371", "0373", "0382", "0398"]:
    p = os.path.join("resources", "wil", "png", "Prguse", f"Prguse_{n}.png")
    try:
        print(f"Prguse_{n}", Image.open(p).size)
    except Exception:
        print(f"Prguse_{n} MISS")
for n in ["0060", "0080"]:
    p = os.path.join("resources", "wil", "png", "StateItem", f"StateItem_{n}.png")
    try:
        print(f"StateItem_{n}", Image.open(p).size)
    except Exception:
        print(f"StateItem_{n} MISS")
