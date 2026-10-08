import json
d = json.load(open("scratch/ida_m2_25.json", encoding="utf-8"))
nfunc = sum(1 for x in d if x["is_func"])
print(f"all_nonfunc={nfunc==0}  funcs={nfunc}/{len(d)}")
non = [x["ea"] for x in d]
print("NONFUNC_EVALS")
for x in d:
    print(x["ea"], x["is_func"])