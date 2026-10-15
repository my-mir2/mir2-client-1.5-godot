with open(r'D:\vbcoding\mir2\mir2-my\radare2\MIR_final_v3_p - Copy.exe', 'rb') as f:
    data = f.read()

idx = data.find('宋体'.encode('gbk'))
if idx != -1:
    chunk = data[idx-100:idx+200]
    print('Context around 宋体:')
    # 打印可读字符串
    import re
    words = re.findall(b'[\x20-\x7e\x80-\xff]+', chunk)
    for w in words:
        try:
            print(' ', w.decode('gbk'))
        except:
            print('  raw:', w)
