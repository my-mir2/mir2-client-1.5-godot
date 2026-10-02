import idc
import idautils

res = []
for head in idautils.FuncItems(0x4BF0A0):
    pass
# Wait, let's find where PKPoint is written to or where name color is determined.
