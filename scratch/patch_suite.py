f=r'tools/run_bot_suite.py'
t=open(f,encoding='utf-8').read()
s=t.index('    def allowed_drops(self, monster_cn):')
e=t.index('    def has_drop_table(self, monster_cn):')
new='''    def drop_verdict(self, monster_cn, seen):
        """返回 (状态, 说明)。DB 掉落表物品名为英文；客户端看到的中文名经服务端别名表反查英文再对照。
        中文名在别名表里查不到 => 无法对照(INCONCLUSIVE)，不当成通过也不当成失败。"""
        keys = self.cn2en.get(("monster", monster_cn))
        if not keys:
            return "INCONCLUSIVE", f"怪物 {monster_cn} 在 cn_names 中无英文名"
        cur = self.db.cursor()
        allowed = {"gold"}
        for k in keys:
            cur.execute("select item_name from official_mon_drop where lower(monster_key)=lower(%s)", (k,))
            allowed |= {n.lower() for (n,) in cur.fetchall()}
        if seen in ("金币", "Gold"):
            return ("PASS", "") if "gold" in allowed else ("FAIL", f"{monster_cn} 官方表无金币却掉了金币")
        ens = {e.lower() for e in self.alias_cn2en.get(seen, set())}
        if not ens and seen.isascii(): ens = {seen.lower()}
        if not ens:
            return "INCONCLUSIVE", f"物品中文名 {seen} 无权威映射"
        return ("PASS", "") if ens & allowed else ("FAIL", f"{monster_cn} 掉了 {seen}({sorted(ens)}) 不在官方掉落表")

'''
t=t[:s]+new+t[e:]
t=t.replace('            self.en2cn.setdefault(("item", en.lower()), cn)','            self.en2cn.setdefault(("item", en.lower()), cn)\n            self.alias_cn2en.setdefault(cn, set()).add(en)')
t=t.replace('        self.cn2en, self.en2cn = {}, {}','        self.cn2en, self.en2cn, self.alias_cn2en = {}, {}, {}')
a=t.index('def rule_drops(r, j, truth):'); b=t.index('def rule_arrivals')
t=t[:a]+'''def rule_drops(r, j, truth):
    bad, undec = [], set()
    for f in j["fights"]:
        if not f["Killed"]:
            continue
        for d in f["drops"]:
            st, msg = truth.drop_verdict(f["Name"], d)
            if st == "FAIL": bad.append(msg)
            elif st == "INCONCLUSIVE": undec.add(msg)
    r.expect(not bad, "R-掉落物 ⊆ 官方掉落表(DB)", f"{sorted(set(bad))[:6]}")
    if undec:
        r.inc("R-掉落物对照", f"无法对照: {sorted(undec)[:6]}")


'''+t[b:]
t=t.replace('"--gm", "level 10;spawn 鸡 8", "--hunt", "鸡"','"--gm", "level 10;tp 1 497 465;spawn 钉耙猫 8", "--hunt", "钉耙猫"')
open(f,'w',encoding='utf-8').write(t)
