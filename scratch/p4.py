f=r'tools/run_bot_suite.py'
t=open(f,encoding='utf-8').read()
def R(a,b):
    global t
    assert a in t, a[:60]
    t=t.replace(a,b,1)
R('    server = None if a.no_server else start_server()\n','    server = None\n')
R('                t0 = time.time()\n','''                t0 = time.time()
                # 每个场景都重启服务端：GM 刷出的怪不会消失，会污染后续场景 (此前 travel 场景被前面刷的半兽勇士打死)
                if not a.no_server:
                    if server: server.kill(); server.wait()
                    server = start_server()
''')
R('futs = [ex.submit(run_bot, name, args, timeout, i) for i in range(copies)]','futs = [ex.submit(run_bot, f"{name}.r{rep}", args, timeout, i) for i in range(copies)]')
# 服务端日志按追加方式保留，便于扫描全部轮次
R('log = open(os.path.join(OUT, "server.log"), "wb")','log = open(os.path.join(OUT, "server.log"), "ab")')
R('    os.makedirs(OUT, exist_ok=True); os.makedirs(os.path.dirname(REPORT), exist_ok=True)','    os.makedirs(OUT, exist_ok=True); os.makedirs(os.path.dirname(REPORT), exist_ok=True)\n    open(os.path.join(OUT, "server.log"), "wb").close()')
open(f,'w',encoding='utf-8').write(t)
