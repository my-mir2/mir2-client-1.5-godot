s = open("src/Game/SpecialRings.cs", encoding="utf-8", errors="ignore").read()
i = s.find("/// 8. 记忆套装检测")
print(s[max(0, i-300):i+200])