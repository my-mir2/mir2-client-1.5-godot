import re, socket
e = open("src/Net/PacketEncoder.cs", encoding="utf-8").read()
for m in re.finditer(r"public static byte\[\] (Motaebo|Say|Whisper|MakeMine|BuyFromShop)\([^)]*\) =>\s*\n?\s*MakePacket\(([^;]+)\);", e):
    print(m.group(1), "->", m.group(2).strip())
print("== rust 7100 listening? ==")
s = socket.socket()
s.settimeout(2)
try:
    s.connect(("127.0.0.1", 7100))
    print("RUST_7100_OPEN")
except Exception as ex:
    print("RUST_7100_CLOSED", ex)
s.close()