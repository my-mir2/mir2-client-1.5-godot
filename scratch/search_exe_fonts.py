with open(r'D:\vbcoding\mir2\mir2-my\radare2\MIR_final_v3_p - Copy.exe', 'rb') as f:
    data = f.read()

import re

# 搜索可能的中文字体名、GBK、或 Font 相关字串
names = ['宋体', '新宋体', '黑体', '楷体', '仿宋', 'MS Sans Serif', 'Arial', 'System', 'Terminal', 'Fixedsys']
for name in names:
    for enc in ['gbk', 'utf-8', 'latin1']:
        try:
            b = name.encode(enc)
            pos = 0
            count = 0
            while True:
                idx = data.find(b, pos)
                if idx == -1: break
                count += 1
                pos = idx + len(b)
            if count > 0:
                print(f'{name} ({enc}): found {count} times')
        except:
            pass
