import glob, os
print("== existing M2 idb ==")
for g in glob.glob(r"D:\vbcoding\**\M2Server*.i64", recursive=True)[:10]:
    print(g, os.path.getsize(g))
for g in glob.glob(r"D:\vbcoding\**\M2Server*.idb", recursive=True)[:10]:
    print(g)
print("== korea content ==")
print(sorted(os.listdir(r"D:\vbcoding\mir2\欧洲官方-14Server\korea"))[:30])
print("== mir2-my anywhere? ==")
for g in glob.glob(r"D:\*\mir2-my", recursive=False)[:5]:
    print(g)
print("== sub_4C1EF4_code.c ==")
print(open("sub_4C1EF4_code.c", encoding="utf-8", errors="ignore").read()[:1500])
