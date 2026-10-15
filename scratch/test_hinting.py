import freetype
from PIL import Image

face = freetype.Face('resources/fonts/SimSun.ttc')
face.set_pixel_sizes(12, 12)

text = '金创药(小量) 持久与即时回复生命值'

def render_line(load_flags):
    img = Image.new('L', (400, 24), 0)
    x = 0
    for ch in text:
        face.load_char(ch, load_flags | freetype.FT_LOAD_RENDER)
        bmp = face.glyph.bitmap
        w, h, pitch = bmp.width, bmp.rows, bmp.pitch
        top = face.glyph.bitmap_top
        left = face.glyph.bitmap_left
        buf = bytes(bmp.buffer)
        
        if bmp.pixel_mode == freetype.FT_PIXEL_MODE_MONO:
            glyph_img = Image.new('L', (w, h), 0)
            for r in range(h):
                for c in range(w):
                    b_idx = r * pitch + (c // 8)
                    if b_idx < len(buf):
                        val = ((buf[b_idx] >> (7 - (c % 8))) & 1) * 255
                        glyph_img.putpixel((c, r), val)
            g_out = glyph_img
        elif bmp.pixel_mode == freetype.FT_PIXEL_MODE_GRAY:
            g_out = Image.frombytes('L', (w, h), buf)
        else:
            continue
            
        y = 16 - top
        img.paste(g_out, (x + left, y))
        x += face.glyph.advance.x >> 6
    return img

# 1. 开启 bytecode hinting + 单色 (原生点阵)
img_hint_mono = render_line(freetype.FT_LOAD_TARGET_MONO)

# 2. 开启 bytecode hinting + 灰度抗锯齿
img_hint_gray = render_line(freetype.FT_LOAD_DEFAULT)

# 3. 关闭 hinting + 单色 (当前代码使用的错误设置)
img_nohint_mono = render_line(freetype.FT_LOAD_NO_HINTING | freetype.FT_LOAD_MONOCHROME)

comp = Image.new('L', (400, 80), 0)
comp.paste(img_hint_mono, (0, 0))
comp.paste(img_hint_gray, (0, 26))
comp.paste(img_nohint_mono, (0, 52))
comp.save('scratch/compare_hinting.png')
print('Saved scratch/compare_hinting.png')
