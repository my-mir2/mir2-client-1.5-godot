with open(r'D:\vbcoding\mir2\mir2-client1.5-delphi-custom\MShare.pas', 'rb') as f:
    lines = f.readlines()
for i in range(295, min(len(lines), 325)):
    line = lines[i].decode('gbk', errors='ignore').strip()
    print(f'{i+1}: {line}')
