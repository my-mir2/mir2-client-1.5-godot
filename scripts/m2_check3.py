import json, os
d = json.load(open("scratch/ida_m2_25.json", encoding="utf-8"))
nf = sum(1 for x in d if x["is_func"])
print(f"funcs={nf}/{len(d)}")
for x in d:
    if x["is_func"]:
        print(" ", x["ea"], x.get("name"), x.get("range"))
st = os.stat("scratch/ida_m2_25.json")
print("mtime_age_sec=", __import__("time").time() - st.st_mtime)
print("i64_exists=", os.path.exists("scratch/M2_copy.exe.i64"),
      os.path.getsize("scratch/M2_copy.exe.i64") if os.path.exists("scratch/M2_copy.exe.i64") else 0)