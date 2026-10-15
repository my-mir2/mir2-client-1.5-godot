from PIL import Image, ImageDraw, ImageFont

font_mac = ImageFont.truetype(r'resources\fonts\Songti.ttc', 12)
font_win = ImageFont.truetype(r'C:\Windows\Fonts\simsun.ttc', 12)

img = Image.new('RGB', (600, 200), (0, 0, 0))
draw = ImageDraw.Draw(img)

text1 = "[系统] 欢迎来到热血传奇！"
text2 = "女战9308  半兽人  钉耙猫  Lv.1  HP: 19/19"
text3 = "金创药(小量)  强效魔法药  屠龙  裁决之杖"

draw.text((20, 20), "macOS Songti SC (当前错误使用的字体):", fill=(255, 100, 100))
draw.text((20, 40), text1, font=font_mac, fill=(255, 255, 255))
draw.text((20, 60), text2, font=font_mac, fill=(255, 255, 255))
draw.text((20, 80), text3, font=font_mac, fill=(255, 255, 255))

draw.text((20, 110), "Windows 原版中易宋体 SimSun.ttc (官方 1.5 物理真值):", fill=(100, 255, 100))
draw.text((20, 130), text1, font=font_win, fill=(255, 255, 255))
draw.text((20, 150), text2, font=font_win, fill=(255, 255, 255))
draw.text((20, 170), text3, font=font_win, fill=(255, 255, 255))

img.save(r'scratch\font_comparison.png')
print('Saved scratch/font_comparison.png')
