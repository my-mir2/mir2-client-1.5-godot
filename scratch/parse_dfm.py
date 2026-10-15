import struct
import pefile

def parse_tpf0(data):
    assert data[:4] == b'TPF0', 'Not TPF0'
    pos = 4
    indent = 0
    lines = []

    def read_byte():
        nonlocal pos
        b = data[pos]
        pos += 1
        return b

    def read_str():
        nonlocal pos
        length = read_byte()
        s = data[pos:pos+length]
        pos += length
        return s.decode('gbk', errors='ignore')

    def parse_value():
        nonlocal pos
        vtype = read_byte()
        if vtype == 0: # valInteger 1 byte
            return str(struct.unpack('b', bytes([read_byte()]))[0])
        elif vtype == 1: # valInteger 2 bytes
            val = struct.unpack('<h', data[pos:pos+2])[0]
            pos += 2
            return str(val)
        elif vtype == 2: # valInteger 4 bytes
            val = struct.unpack('<i', data[pos:pos+4])[0]
            pos += 4
            return str(val)
        elif vtype == 3: # valExtended float
            pos += 10
            return '<float>'
        elif vtype == 4: # valString
            return repr(read_str())
        elif vtype == 5: # valIdent
            return read_str()
        elif vtype == 6: # valFalse
            return 'False'
        elif vtype == 7: # valTrue
            return 'True'
        elif vtype == 8: # valBinary
            length = struct.unpack('<I', data[pos:pos+4])[0]
            pos += 4 + length
            return f'<binary {length} bytes>'
        elif vtype == 9: # valSet
            items = []
            while True:
                item = read_str()
                if not item: break
                items.append(item)
            return '[' + ', '.join(items) + ']'
        elif vtype == 10: # valCollection
            return '<collection>'
        elif vtype == 11: # valNull
            return 'nil'
        elif vtype == 12: # valSingle
            pos += 4
            return '<single>'
        elif vtype == 13: # valCurrency
            pos += 8
            return '<currency>'
        elif vtype == 14: # valDate
            pos += 8
            return '<date>'
        elif vtype == 15: # valWString
            length = struct.unpack('<I', data[pos:pos+4])[0]
            pos += 4
            s = data[pos:pos+length*2].decode('utf-16le', errors='ignore')
            pos += length*2
            return repr(s)
        elif vtype == 16: # valLString
            length = struct.unpack('<I', data[pos:pos+4])[0]
            pos += 4
            s = data[pos:pos+length].decode('gbk', errors='ignore')
            pos += length
            return repr(s)
        else:
            return f'<unknown type {vtype}>'

    while pos < len(data):
        b = read_byte()
        if b == 0: # end of object
            indent -= 2
            lines.append(' ' * indent + 'end')
            if indent <= 0:
                break
        elif b in [1, 2]: # object or inherited
            type_name = read_str()
            var_name = read_str()
            lines.append(' ' * indent + f'object {var_name}: {type_name}')
            indent += 2
        else: # property
            pos -= 1
            prop_name = read_str()
            val = parse_value()
            lines.append(' ' * indent + f'{prop_name} = {val}')

    return '\n'.join(lines)

if __name__ == '__main__':
    with open('scratch/TFRMDLG.dfm.bin', 'rb') as f:
        txt = parse_tpf0(f.read())
    with open('scratch/TFRMDLG.dfm.txt', 'w', encoding='utf-8') as f:
        f.write(txt)
    print(f'Successfully parsed TFRMDLG.dfm.txt, {len(txt.splitlines())} lines!')

    with open('scratch/TFRMMAIN.dfm.bin', 'rb') as f:
        txt_m = parse_tpf0(f.read())
    with open('scratch/TFRMMAIN.dfm.txt', 'w', encoding='utf-8') as f:
        f.write(txt_m)
    print(f'Successfully parsed TFRMMAIN.dfm.txt, {len(txt_m.splitlines())} lines!')
