with open(r'D:\vbcoding\mir2\mir2-client1.5-delphi-custom\PlayScn.pas', 'rb') as f:
    lines = f.readlines()
for i in range(1385, 1410):
    print(f'{i+1}: {lines[i].decode("gbk", errors="ignore").strip()}')
