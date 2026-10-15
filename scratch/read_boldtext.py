with open(r'D:\vbcoding\mir2\mir2-client1.5-delphi-custom\ClFunc.pas', 'rb') as f:
    lines = f.readlines()

for i in range(595, min(len(lines), 640)):
    txt = lines[i].decode('gbk', errors='ignore').strip()
    print(f'{i+1}: {txt}')
