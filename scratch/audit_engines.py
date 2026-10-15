import os

all_engines = []
for f in os.listdir('src/Mir2.Shared/Rules'):
    if '100' in f or '101' in f or '102' in f:
        name = f.replace('.cs', '')
        all_engines.append(name)

used_counts = {e: 0 for e in all_engines}
for root, dirs, files in os.walk('src'):
    if 'Rules' in root: continue
    for f in files:
        if f.endswith('.cs'):
            p = os.path.join(root, f)
            c = open(p, encoding='utf-8', errors='ignore').read()
            for e in all_engines:
                if e in c:
                    used_counts[e] += 1

test_counts = {e: 0 for e in all_engines}
for root, dirs, files in os.walk('tests'):
    for f in files:
        if f.endswith('.cs'):
            p = os.path.join(root, f)
            c = open(p, encoding='utf-8', errors='ignore').read()
            for e in all_engines:
                if e in c:
                    test_counts[e] += 1

print(f"{'Engine Class':55} {'Src Usages':12} {'Test Usages':12}")
print('-'*80)
for e in all_engines:
    print(f"{e:55} {used_counts[e]:<12} {test_counts[e]:<12}")
