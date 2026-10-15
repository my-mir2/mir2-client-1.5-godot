with open(r'D:\vbcoding\mir2\mir2-client1.5-delphi-custom\DrawScrn.pas', 'rb') as f:
    lines = f.readlines()
for i in range(180, min(390, len(lines))):
    print(f'{i+1}: {lines[i].decode("gbk", errors="ignore").strip()}')




