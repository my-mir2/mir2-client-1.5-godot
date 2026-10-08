import os, re

missing_ids = [1, 2, 3, 4, 5, 8, 9, 17, 18, 19, 20, 21, 22, 23, 24, 28, 29, 30, 520, 521, 522, 523, 524, 528, 530, 615, 617, 618, 619, 620, 621, 622, 637, 638, 639, 642, 643, 644, 645, 646, 647, 652, 653, 654, 655, 657, 663, 671, 673, 674, 675, 676, 677, 678, 681, 682, 683, 684, 685, 686, 687, 700, 704, 708, 709, 710, 712, 751, 753, 756, 758, 766, 767, 768, 770, 805, 810]

src_files = []
for root, dirs, files in os.walk('src'):
    for f in files:
        if f.endswith('.cs'):
            src_files.append(os.path.join(root, f))

found_dict = {}
for mid in missing_ids:
    hits = []
    pattern = re.compile(r'(\bcase\s+' + str(mid) + r'\b|\bIdent\s*==\s*' + str(mid) + r'\b|\bSM_[A-Za-z0-9_]+\s*=\s*' + str(mid) + r'\b)')
    for sf in src_files:
        with open(sf, 'r', encoding='utf-8', errors='ignore') as f:
            content = f.read()
            m = pattern.search(content)
            if m:
                hits.append((sf, m.group(0)))
    found_dict[mid] = hits

completely_missing = []
for mid in missing_ids:
    if not found_dict[mid]:
        completely_missing.append(mid)
    else:
        print(f"ID {mid} found in: {[h[0] + ': ' + h[1] for h in found_dict[mid]]}")

print('=== COMPLETELY MISSING IDS (' + str(len(completely_missing)) + ') ===')
print(completely_missing)
