import json, re
cov = json.load(open("scratch/coverage_gap.json", encoding="utf-8"))
led = open("docs/ida_evidence/sub_ledger.md", encoding="utf-8").read()
done = set(re.findall(r"- \[x\] (0x[0-9a-f]+)", led))
targets = [s for s in cov["subs"] if ("0x" + s.lower()) not in done]
print(f"REMAINING={len(targets)}")
open("scratch/remaining_subs.json", "w").write(json.dumps(targets))
print(targets[:20])
