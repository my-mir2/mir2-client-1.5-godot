import struct
import math
import binascii

def extended_to_float(bytes_10):
    mantissa_bytes = bytes_10[0:8]
    exp_bytes = bytes_10[8:10]
    
    mantissa = struct.unpack('<Q', mantissa_bytes)[0]
    exp_sign = struct.unpack('<H', exp_bytes)[0]
    
    sign = -1 if (exp_sign & 0x8000) else 1
    exponent = exp_sign & 0x7FFF
    
    if exponent == 0:
        return 0.0 * sign
    
    exponent -= 16383 # Bias for 80-bit extended
    
    val = sign * (mantissa / (2**63)) * (2**exponent)
    return val

print("4BED28:", extended_to_float(binascii.unhexlify("3bdf4f8d976e1283f63f")))
