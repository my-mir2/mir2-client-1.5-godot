
; === VMT_TSkeletonOma @ 0x473DFC ===
0x473DFC: push     es
0x473DFD: add      ah, bh
0x473E02: std      
0x473E03: push     ebx
0x473E06: cli      
0x473E07: push     edx
0x473E09: call     ptr [ebp + eax*2 + 0x45f40047]
0x473E10: inc      edi
0x473E11: add      byte ptr [esp + ecx*2], bh
0x473E14: inc      edi
0x473E15: add      byte ptr [ebp + ecx*2 + 0x47], dl
0x473E19: add      byte ptr [eax + 0x4e], bl
0x473E1C: inc      edi
0x473E1D: add      byte ptr [eax + edx*2 + 0x540c0047], dl
0x473E24: push     ebx
0x473E25: imul     esp, dword ptr [ebp + 0x6c], 0x65
0x473E29: je       0x473e9a
0x473E2B: outsb    dx, byte ptr [esi]
0x473E2C: dec      edi
0x473E2D: insd     dword ptr es:[edi], dx
0x473E2E: popal    
0x473E2F: nop      
0x473E30: jl       0x473e70
0x473E32: inc      edi
0x473E33: add      byte ptr [eax], al
0x473E35: add      byte ptr [eax], al
0x473E37: add      byte ptr [eax], al
0x473E39: add      byte ptr [eax], al
0x473E3B: add      byte ptr [eax], al
0x473E3D: add      byte ptr [eax], al
0x473E3F: add      byte ptr [eax], al
0x473E41: add      byte ptr [eax], al
0x473E43: add      byte ptr [eax], al
0x473E45: add      byte ptr [eax], al
0x473E47: add      byte ptr [eax], al
0x473E49: add      byte ptr [eax], al
0x473E4B: add      byte ptr [esi + edi + 0x47], bh
0x473E4F: add      byte ptr [esi + edi + 0x2500047], al
0x473E56: add      byte ptr [eax], al
0x473E58: mov      al, 0x3d
0x473E5A: inc      edi
0x473E5B: add      al, cl
0x473E5D: xor      byte ptr [eax], al
0x473E60: aam      0x30
0x473E62: inc      eax
0x473E63: add      al, bl
0x473E65: xor      byte ptr [eax], al
0x473E68: fdiv     qword ptr [eax]
0x473E6A: inc      eax
0x473E6B: add      al, dl
0x473E6D: xor      byte ptr [eax], al
0x473E70: inc      eax
0x473E71: inc      eax
0x473E73: add      byte ptr [esi + ebp + 0x40], dl
0x473E77: add      byte ptr [eax], cl
0x473E79: dec      ecx
0x473E7B: add      byte ptr [ecx], al
0x473E7D: add      dl, bh
0x473E7F: jmp      dword ptr [eax + 0xb004751]
0x473E85: push     esp
0x473E86: inc      esp
0x473E87: jne      0x473eea
0x473E89: insb     byte ptr es:[edi], dx
0x473E8A: inc      ecx
0x473E8B: js       0x473ef2
0x473E8D: dec      edi
0x473E8E: insd     dword ptr es:[edi], dx
0x473E8F: popal    
0x473E90: fdivr    qword ptr [esi]
0x473E92: inc      edi
0x473E93: add      byte ptr [eax], al
0x473E95: add      byte ptr [eax], al
0x473E97: add      byte ptr [eax], al
0x473E99: add      byte ptr [eax], al
0x473E9B: add      byte ptr [eax], al
0x473E9D: add      byte ptr [eax], al
0x473E9F: add      byte ptr [eax], al
0x473EA1: add      byte ptr [eax], al
0x473EA3: add      byte ptr [eax], al
0x473EA5: add      byte ptr [eax], al
0x473EA7: add      byte ptr [eax], al
0x473EA9: add      byte ptr [eax], al
0x473EAB: add      ah, bl
0x473EAD: inc      edi
0x473EAF: add      ah, ah
0x473EB1: inc      edi
0x473EB3: add      byte ptr [eax + 2], dl
0x473EB6: add      byte ptr [eax], al
0x473EB8: mov      al, 0x3d
0x473EBA: inc      edi
0x473EBB: add      al, cl
0x473EBD: xor      byte ptr [eax], al
0x473EC0: aam      0x30
0x473EC2: inc      eax
0x473EC3: add      al, bl
0x473EC5: xor      byte ptr [eax], al
0x473EC8: fdiv     qword ptr [eax]
0x473ECA: inc      eax
0x473ECB: add      al, dl
0x473ECD: xor      byte ptr [eax], al
0x473ED0: inc      eax
0x473ED1: inc      eax
0x473ED3: add      byte ptr [esi + ebp + 0x40], dl
0x473ED7: add      byte ptr [eax], cl
0x473ED9: dec      ecx
0x473EDB: add      byte ptr [ecx], al
0x473EDD: add      dl, dh
0x473EE0: cmp      byte ptr [ebp + 0x47], dl
0x473EE3: add      byte ptr [edi], al
0x473EE5: push     esp
0x473EE6: inc      ebx
0x473EE7: popal    
0x473EE8: je       0x473f37
0x473EEA: outsd    dx, dword ptr [esi]
0x473EEB: outsb    dx, byte ptr [esi]
0x473EEC: cmp      byte ptr [edi], bh
0x473EEE: inc      edi
0x473EEF: add      byte ptr [eax], al
0x473EF1: add      byte ptr [eax], al
0x473EF3: add      byte ptr [eax], al
0x473EF5: add      byte ptr [eax], al
0x473EF7: add      byte ptr [eax], al
0x473EF9: add      byte ptr [eax], al
0x473EFB: add      byte ptr [eax], al
0x473EFD: add      byte ptr [eax], al
0x473EFF: add      byte ptr [eax], al
0x473F01: add      byte ptr [eax], al
0x473F03: add      byte ptr [eax], al
0x473F05: add      byte ptr [eax], al
0x473F07: add      byte ptr [eax], bh
0x473F09: aas      
0x473F0A: inc      edi
0x473F0B: add      byte ptr [eax + 0x3f], al
0x473F0E: inc      edi
0x473F0F: add      byte ptr [eax + 2], dl
0x473F12: add      byte ptr [eax], al
0x473F14: nop      
0x473F15: inc      edi
0x473F17: add      al, cl
0x473F19: xor      byte ptr [eax], al
0x473F1C: aam      0x30
0x473F1E: inc      eax
0x473F1F: add      al, bl
0x473F21: xor      byte ptr [eax], al
0x473F24: fdiv     qword ptr [eax]
0x473F26: inc      eax
0x473F27: add      al, dl
0x473F29: xor      byte ptr [eax], al
0x473F2C: inc      eax
0x473F2D: inc      eax
0x473F2F: add      byte ptr [esi + ebp + 0x40], dl
0x473F33: add      byte ptr [eax], cl
0x473F35: dec      ecx
0x473F37: add      byte ptr [ecx], al
0x473F39: add      dl, bh
0x473F3C: in       al, dx
0x473F3D: push     ebp
0x473F3E: inc      edi
0x473F3F: add      byte ptr [edx], cl
0x473F41: push     esp
0x473F42: inc      ecx
0x473F43: jb       0x473fa8
0x473F45: push     0x6f4d7265
0x473F4A: outsb    dx, byte ptr [esi]
0x473F4B: nop      
0x473F4C: cwde     
0x473F4D: aas      
0x473F4E: inc      edi
0x473F4F: add      byte ptr [eax], al
0x473F51: add      byte ptr [eax], al
0x473F53: add      byte ptr [eax], al
0x473F55: add      byte ptr [eax], al
0x473F57: add      byte ptr [eax], al
0x473F59: add      byte ptr [eax], al
0x473F5B: add      byte ptr [eax], al
0x473F5D: add      byte ptr [eax], al
0x473F5F: add      byte ptr [eax], al
0x473F61: add      byte ptr [eax], al
0x473F63: add      byte ptr [eax], al
0x473F65: add      byte ptr [eax], al
0x473F67: add      byte ptr [eax], al
0x473F69: add      byte ptr [eax], al
0x473F6B: add      byte ptr [eax + 0x5000473f], bl
0x473F71: add      al, byte ptr [eax]
0x473F73: add      byte ptr [eax - 0x37ffb8c2], dl
0x473F79: xor      byte ptr [eax], al
0x473F7C: aam      0x30
0x473F7E: inc      eax
0x473F7F: add      al, bl
0x473F81: xor      byte ptr [eax], al
0x473F84: fdiv     qword ptr [eax]
0x473F86: inc      eax
0x473F87: add      al, dl
0x473F89: xor      byte ptr [eax], al
0x473F8C: inc      eax
0x473F8D: inc      eax
0x473F8F: add      byte ptr [esi + ebp + 0x40], dl
0x473F93: add      byte ptr [eax], cl
0x473F95: dec      ecx
0x473F97: add      byte ptr [esp + edx*2], cl
0x473F9A: push     ebx
0x473F9B: arpl     word ptr [edi + 0x72], bp
0x473F9E: jo       0x474009
0x473FA0: outsd    dx, dword ptr [esi]
0x473FA1: outsb    dx, byte ptr [esi]
0x473FA2: dec      ebp
0x473FA3: outsd    dx, dword ptr [esi]
0x473FA4: outsb    dx, byte ptr [esi]
0x473FA5: lea      eax, [eax]
0x473FA8: hlt      
0x473FA9: aas      
0x473FAA: inc      edi
0x473FAB: add      byte ptr [eax], al
0x473FAD: add      byte ptr [eax], al
0x473FAF: add      byte ptr [eax], al
0x473FB1: add      byte ptr [eax], al
0x473FB3: add      byte ptr [eax], al
0x473FB5: add      byte ptr [eax], al
0x473FB7: add      byte ptr [eax], al
0x473FB9: add      byte ptr [eax], al
0x473FBB: add      byte ptr [eax], al
0x473FBD: add      byte ptr [eax], al
0x473FBF: add      byte ptr [eax], al
0x473FC1: add      byte ptr [eax], al
0x473FC3: add      ah, dh
0x473FC5: aas      
0x473FC6: inc      edi
0x473FC7: add      ah, bh
0x473FC9: aas      
0x473FCA: inc      edi
0x473FCB: add      byte ptr [eax + 2], dl
0x473FCE: add      byte ptr [eax], al
0x473FD0: mov      al, 0x3d
0x473FD2: inc      edi
0x473FD3: add      al, cl
0x473FD5: xor      byte ptr [eax], al
0x473FD8: aam      0x30
0x473FDA: inc      eax
0x473FDB: add      al, bl
0x473FDD: xor      byte ptr [eax], al
0x473FE0: fdiv     qword ptr [eax]
0x473FE2: inc      eax
0x473FE3: add      al, dl
0x473FE5: xor      byte ptr [eax], al
0x473FE8: inc      eax
0x473FE9: inc      eax
0x473FEB: add      byte ptr [esi + ebp + 0x40], dl
0x473FEF: add      byte ptr [eax], cl
0x473FF1: dec      ecx
0x473FF3: add      byte ptr [ecx], al
0x473FF5: add      bl, dh
0x473FF7: jmp      ptr [ecx + ebx*2]
0x473FFA: inc      edi
0x473FFB: add      byte ptr [eax], cl
0x473FFD: push     esp
0x473FFE: dec      eax
0x473FFF: jne      0x474054 ; VMT_TZombiDigOut
0x474001: jne      0x474044
0x474003: inc      edx
0x474004: imul     ecx, dword ptr [ebp + 0x40540040], 0x47
0x47400E: add      byte ptr [eax], al
0x474010: add      byte ptr [eax], al
0x474012: add      byte ptr [eax], al
0x474014: add      byte ptr [eax], al
0x474016: add      byte ptr [eax], al
0x474018: add      byte ptr [eax], al
0x47401A: add      byte ptr [eax], al
0x47401C: add      byte ptr [eax], al
0x47401E: add      byte ptr [eax], al
0x474020: add      byte ptr [eax], al
0x474022: add      byte ptr [eax], al
0x474024: push     esp
0x474025: inc      eax
0x474026: inc      edi
0x474027: add      byte ptr [eax + eax*2 + 0x47], bl
0x47402B: add      byte ptr [eax + 2], dl
0x47402E: add      byte ptr [eax], al
0x474030: mov      al, 0x3d
0x474032: inc      edi
0x474033: add      al, cl
0x474035: xor      byte ptr [eax], al
0x474038: aam      0x30
0x47403A: inc      eax
0x47403B: add      al, bl
0x47403D: xor      byte ptr [eax], al
0x474040: fdiv     qword ptr [eax]
0x474042: inc      eax
0x474043: add      al, dl
0x474045: xor      byte ptr [eax], al
0x474048: inc      eax
0x474049: inc      eax
0x47404B: add      byte ptr [esi + ebp + 0x40], dl
0x47404F: add      byte ptr [eax], cl
0x474051: dec      ecx
0x474053: add      byte ptr [ecx], al
0x474055: add      bh, dh
0x474057: inc      esp
0x474059: pop      eax
0x47405A: inc      edi
0x47405B: add      byte ptr [esp + edx*2], cl
0x47405E: pop      edx
0x47405F: outsd    dx, dword ptr [esi]
0x474060: insd     dword ptr es:[edi], dx
0x474061: bound    ebp, qword ptr [ecx + 0x44]
0x474064: imul     esp, dword ptr [edi + 0x4f], 0x408d7475
0x47406B: add      byte ptr [eax + 0x4740], bh
0x474071: add      byte ptr [eax], al
0x474073: add      byte ptr [eax], al
0x474075: add      byte ptr [eax], al
0x474077: add      byte ptr [eax], al
0x474079: add      byte ptr [eax], al
0x47407B: add      byte ptr [eax], al
0x47407D: add      byte ptr [eax], al
0x47407F: add      byte ptr [eax], al
0x474081: add      byte ptr [eax], al
0x474083: add      byte ptr [eax], al
0x474085: add      byte ptr [eax], al
0x474087: add      byte ptr [eax], al
0x474089: add      byte ptr [eax], al
0x47408B: add      byte ptr [eax + 0x50004740], bh
0x474091: add      al, byte ptr [eax]
0x474093: add      byte ptr [eax - 0x37ffb8c3], dh
0x474099: xor      byte ptr [eax], al
0x47409C: aam      0x30
0x47409E: inc      eax
0x47409F: add      al, bl
0x4740A1: xor      byte ptr [eax], al
0x4740A4: fdiv     qword ptr [eax]
0x4740A6: inc      eax
0x4740A7: add      al, dl
0x4740A9: xor      byte ptr [eax], al
0x4740AC: inc      eax
0x4740AD: inc      eax
0x4740AF: add      byte ptr [esi + ebp + 0x40], dl
0x4740B3: add      byte ptr [eax], cl
0x4740B5: dec      ecx
0x4740B7: add      byte ptr [esp + edx*2], cl
0x4740BA: pop      edx
0x4740BB: outsd    dx, dword ptr [esi]
0x4740BC: insd     dword ptr es:[edi], dx
0x4740BD: bound    ebp, qword ptr [ecx + 0x5a]
0x4740C0: imul     ebp, dword ptr [ebx + ebp*2 + 0x69], 0x408d6e
0x4740C8: adc      al, 0x41
0x4740CA: inc      edi
0x4740CB: add      byte ptr [eax], al
0x4740CD: add      byte ptr [eax], al
0x4740CF: add      byte ptr [eax], al
0x4740D1: add      byte ptr [eax], al
0x4740D3: add      byte ptr [eax], al
0x4740D5: add      byte ptr [eax], al
0x4740D7: add      byte ptr [eax], al
0x4740D9: add      byte ptr [eax], al
0x4740DB: add      byte ptr [eax], al
0x4740DD: add      byte ptr [eax], al
0x4740DF: add      byte ptr [eax], al
0x4740E1: add      byte ptr [eax], al
0x4740E3: add      byte ptr [eax], al
0x4740E5: add      byte ptr [eax], al
0x4740E7: add      byte ptr [ecx + eax*2], dl
0x4740EA: inc      edi
0x4740EB: add      byte ptr [eax + 2], dl
0x4740EE: add      byte ptr [eax], al
0x4740F0: mov      al, 0x3d
0x4740F2: inc      edi
0x4740F3: add      al, cl
0x4740F5: xor      byte ptr [eax], al
0x4740F8: aam      0x30
0x4740FA: inc      eax
0x4740FB: add      al, bl
0x4740FD: xor      byte ptr [eax], al
0x474100: fdiv     qword ptr [eax]
0x474102: inc      eax
0x474103: add      al, dl
0x474105: xor      byte ptr [eax], al
0x474108: inc      eax
0x474109: inc      eax
0x47410B: add      byte ptr [esi + ebp + 0x40], dl
0x47410F: add      byte ptr [eax], cl
0x474111: dec      ecx
0x474113: add      byte ptr [esi], cl
0x474115: push     esp
0x474116: push     edi
0x474117: push     0x53657469
0x47411C: imul     esp, dword ptr [ebp + 0x6c], 0x65
0x474120: je       0x474191
0x474122: outsb    dx, byte ptr [esi]
0x474123: nop      
0x474124: jo       0x474167
0x474126: inc      edi
0x474127: add      byte ptr [eax], al
0x474129: add      byte ptr [eax], al
0x47412B: add      byte ptr [eax], al
0x47412D: add      byte ptr [eax], al
0x47412F: add      byte ptr [eax], al
0x474131: add      byte ptr [eax], al
0x474133: add      byte ptr [eax], al
0x474135: add      byte ptr [eax], al
0x474137: add      byte ptr [eax], al
0x474139: add      byte ptr [eax], al
0x47413B: add      byte ptr [eax], al
0x47413D: add      byte ptr [eax], al
0x47413F: add      byte ptr [eax + 0x41], dh
0x474142: inc      edi
0x474143: add      byte ptr [ecx + eax*2 + 0x2680047], bl
0x47414A: add      byte ptr [eax], al
0x47414C: int3     
0x47414D: pop      edx
0x47414E: dec      ecx
0x47414F: add      al, cl
0x474151: xor      byte ptr [eax], al
0x474154: aam      0x30
0x474156: inc      eax
0x474157: add      al, bl
0x474159: xor      byte ptr [eax], al
0x47415C: fdiv     qword ptr [eax]
0x47415E: inc      eax
0x47415F: add      al, dl
0x474161: xor      byte ptr [eax], al
0x474164: inc      eax
0x474165: inc      eax
0x474167: add      byte ptr [esi + ebp + 0x40], dl
0x47416B: add      byte ptr [eax], cl
0x47416D: dec      ecx
0x47416F: add      byte ptr [edi], al
0x474171: add      ah, bh
0x474176: std      
0x474177: push     ebx
0x47417A: cli      
0x47417B: push     edx
0x47417D: push     ecx
0x47417F: call     dword ptr [eax - 0x3ffb8a7]
0x474185: pop      ecx
0x474186: inc      edi
0x474187: add      byte ptr [eax - 0x7bffb8a1], dl
0x47418D: pushal   
0x47418E: inc      edi
0x47418F: add      ah, dh
0x474191: arpl     word ptr [edi], ax
0x474194: enter    0x4766, 0
0x474198: jl       0x474201
0x47419A: inc      edi
0x47419B: add      byte ptr [edx], cl
0x47419D: push     esp
0x47419E: inc      edi
0x47419F: popal    
0x4741A0: jae      0x4741ed
0x4741A2: jne      0x4741e8
0x4741A4: inc      edi
0x4741A6: imul     edx, dword ptr [eax + 0x4741f4], 0 ; VMT_TFireCowFaceMon
0x4741B0: add      byte ptr [eax], al
0x4741B2: add      byte ptr [eax], al
0x4741B4: add      byte ptr [eax], al
0x4741B6: add      byte ptr [eax], al
0x4741B8: add      byte ptr [eax], al
0x4741BA: add      byte ptr [eax], al
0x4741BC: add      byte ptr [eax], al
0x4741BE: add      byte ptr [eax], al
0x4741C0: add      byte ptr [eax], al
0x4741C2: add      byte ptr [eax], al
0x4741C4: hlt      
0x4741C5: inc      ecx
0x4741C6: inc      edi
0x4741C7: add      ah, bh
0x4741C9: inc      ecx
0x4741CA: inc      edi
0x4741CB: add      byte ptr [eax + 2], ch
0x4741CE: add      byte ptr [eax], al
0x4741D0: and      al, 0x41
0x4741D2: inc      edi
0x4741D3: add      al, cl
0x4741D5: xor      byte ptr [eax], al
0x4741D8: aam      0x30
0x4741DA: inc      eax
0x4741DB: add      al, bl
0x4741DD: xor      byte ptr [eax], al
0x4741E0: fdiv     qword ptr [eax]
0x4741E2: inc      eax
0x4741E3: add      al, dl
0x4741E5: xor      byte ptr [eax], al
0x4741E8: inc      eax
0x4741E9: inc      eax
0x4741EB: add      byte ptr [esi + ebp + 0x40], dl
0x4741EF: add      byte ptr [eax], cl
0x4741F1: dec      ecx
0x4741F3: add      byte ptr [ecx], al
0x4741F5: add      ah, dh
0x4741F7: dec      eax
0x4741F9: imul     eax, dword ptr [edi], 0x6946540f
0x474200: jb       0x474267
0x474202: inc      ebx
0x474203: outsd    dx, dword ptr [esi]
0x474204: ja       0x47424c
0x474206: popal    
0x474207: arpl     word ptr [ebp + 0x4d], sp
0x47420A: outsd    dx, dword ptr [esi]
0x47420B: outsb    dx, byte ptr [esi]
0x47420C: pop      eax
0x47420D: inc      edx
0x47420E: inc      edi
0x47420F: add      byte ptr [eax], al
0x474211: add      byte ptr [eax], al
0x474213: add      byte ptr [eax], al
0x474215: add      byte ptr [eax], al
0x474217: add      byte ptr [eax], al
0x474219: add      byte ptr [eax], al
0x47421B: add      byte ptr [eax], al
0x47421D: add      byte ptr [eax], al
0x47421F: add      byte ptr [eax], al
0x474221: add      byte ptr [eax], al
0x474223: add      byte ptr [eax], al
0x474225: add      byte ptr [eax], al
0x474227: add      byte ptr [eax + 0x42], bl
0x47422A: inc      edi
0x47422B: add      byte ptr [eax + 0x42], ah
0x47422E: inc      edi
0x47422F: add      byte ptr [eax + 2], ch
0x474232: add      byte ptr [eax], al
0x474234: and      al, 0x41
0x474236: inc      edi
0x474237: add      al, cl
0x474239: xor      byte ptr [eax], al
0x47423C: aam      0x30
0x47423E: inc      eax
0x47423F: add      al, bl
0x474241: xor      byte ptr [eax], al
0x474244: fdiv     qword ptr [eax]
0x474246: inc      eax
0x474247: add      al, dl
0x474249: xor      byte ptr [eax], al
0x47424C: inc      eax
0x47424D: inc      eax
0x47424F: add      byte ptr [esi + ebp + 0x40], dl
0x474253: add      byte ptr [eax], cl
0x474255: dec      ecx
0x474257: add      byte ptr [ecx], al
0x474259: add      ah, dh
0x47425B: inc      dword ptr [edx + ebp*2]
0x47425E: inc      edi
0x47425F: add      byte ptr [esp + edx*2], cl
0x474262: inc      ebx
0x474263: outsd    dx, dword ptr [esi]
0x474264: ja       0x4742ac
0x474266: popal    
0x474267: arpl     word ptr [ebp + 0x4b], sp
0x47426A: imul     ebp, dword ptr [esi + 0x67], 0xbc00408d
0x474271: inc      edx
0x474272: inc      edi
0x474273: add      byte ptr [eax], al
0x474275: add      byte ptr [eax], al
0x474277: add      byte ptr [eax], al
0x474279: add      byte ptr [eax], al
0x47427B: add      byte ptr [eax], al
0x47427D: add      byte ptr [eax], al
0x47427F: add      byte ptr [eax], al
0x474281: add      byte ptr [eax], al
0x474283: add      byte ptr [eax], al
0x474285: add      byte ptr [eax], al
0x474287: add      byte ptr [eax], al
0x474289: add      byte ptr [eax], al
0x47428B: add      byte ptr [eax], al
0x47428D: add      byte ptr [eax], al
0x47428F: add      byte ptr [edx + eax*2 + 0x2680047], bh
0x474296: add      byte ptr [eax], al
0x474298: and      al, 0x41
0x47429A: inc      edi
0x47429B: add      al, cl
0x47429D: xor      byte ptr [eax], al
0x4742A0: aam      0x30
0x4742A2: inc      eax
0x4742A3: add      al, bl
0x4742A5: xor      byte ptr [eax], al
0x4742A8: fdiv     qword ptr [eax]
0x4742AA: inc      eax
0x4742AB: add      al, dl
0x4742AD: xor      byte ptr [eax], al
0x4742B0: inc      eax
0x4742B1: inc      eax
0x4742B3: add      byte ptr [esi + ebp + 0x40], dl
0x4742B7: add      byte ptr [eax], cl
0x4742B9: dec      ecx
0x4742BB: add      byte ptr [esi], cl
0x4742BD: push     esp
0x4742BE: pop      edx
0x4742BF: outsd    dx, dword ptr [esi]
0x4742C0: insd     dword ptr es:[edi], dx
0x4742C1: bound    ebp, qword ptr [ecx + 0x4c]
0x4742C4: imul     esp, dword ptr [edi + 0x68], 0x676e6974
0x4742CB: nop      
0x4742CC: sbb      byte ptr [ebx + 0x47], al
0x4742CF: add      byte ptr [eax], al
0x4742D1: add      byte ptr [eax], al
0x4742D3: add      byte ptr [eax], al
0x4742D5: add      byte ptr [eax], al
0x4742D7: add      byte ptr [eax], al
0x4742D9: add      byte ptr [eax], al
0x4742DB: add      byte ptr [eax], al
0x4742DD: add      byte ptr [eax], al
0x4742DF: add      byte ptr [eax], al
0x4742E1: add      byte ptr [eax], al
0x4742E3: add      byte ptr [eax], al
0x4742E5: add      byte ptr [eax], al
0x4742E7: add      byte ptr [eax], al
0x4742E9: add      byte ptr [eax], al
0x4742EB: add      byte ptr [eax], bl
0x4742ED: inc      ebx
0x4742EE: inc      edi
0x4742EF: add      byte ptr [eax + 2], ch
0x4742F2: add      byte ptr [eax], al
0x4742F4: and      al, 0x41
0x4742F6: inc      edi
0x4742F7: add      al, cl
0x4742F9: xor      byte ptr [eax], al
0x4742FC: aam      0x30
0x4742FE: inc      eax
0x4742FF: add      al, bl
0x474301: xor      byte ptr [eax], al
0x474304: fdiv     qword ptr [eax]
0x474306: inc      eax
0x474307: add      al, dl
0x474309: xor      byte ptr [eax], al
0x47430C: inc      eax
0x47430D: inc      eax
0x47430F: add      byte ptr [esi + ebp + 0x40], dl
0x474313: add      byte ptr [eax], cl
0x474315: dec      ecx
0x474317: add      byte ptr [esi], cl
0x474319: push     esp
0x47431A: push     ebx
0x47431B: jne      0x47438d
0x47431D: jb       0x474389
0x474320: outsd    dx, dword ptr [esi]
0x474321: jb       0x47436a
0x474323: jne      0x474386
0x474325: jb       0x47438b
0x474327: nop      
0x474328: je       0x47436d
0x47432A: inc      edi
0x47432B: add      byte ptr [eax], al
0x47432D: add      byte ptr [eax], al
0x47432F: add      byte ptr [eax], al
0x474331: add      byte ptr [eax], al
0x474333: add      byte ptr [eax], al
0x474335: add      byte ptr [eax], al
0x474337: add      byte ptr [eax], al
0x474339: add      byte ptr [eax], al
0x47433B: add      byte ptr [eax], al
0x47433D: add      byte ptr [eax], al
0x47433F: add      byte ptr [eax], al
0x474341: add      byte ptr [eax], al
0x474343: add      byte ptr [ebx + eax*2 + 0x47], dh
0x474347: add      byte ptr [edx + 0x68004743], al
0x47434D: add      al, byte ptr [eax]
0x47434F: add      byte ptr [ecx + eax*2], ah
0x474352: inc      edi
0x474353: add      al, cl
0x474355: xor      byte ptr [eax], al
0x474358: aam      0x30
0x47435A: inc      eax
0x47435B: add      al, bl
0x47435D: xor      byte ptr [eax], al
0x474360: fdiv     qword ptr [eax]
0x474362: inc      eax
0x474363: add      al, dl
0x474365: xor      byte ptr [eax], al
0x474368: inc      eax
0x474369: inc      eax
0x47436B: add      byte ptr [esi + ebp + 0x40], dl
0x47436F: add      byte ptr [eax], cl
0x474371: dec      ecx
0x474373: add      byte ptr [edx], al
0x474375: add      bh, bh
0x474377: push     ebx
0x474379: inc      dword ptr [eax + 0x68]
0x47437C: inc      edi
0x47437D: add      al, bl
0x47437F: push     0x54100047
0x474384: inc      ebp
0x474385: js       0x4743f7
0x474387: insb     byte ptr es:[edi], dx
0x474388: outsd    dx, dword ptr [esi]
0x474389: jae      0x4743f4
0x47438B: outsd    dx, dword ptr [esi]
0x47438C: outsb    dx, byte ptr [esi]
0x47438D: push     ebx
0x47438E: jo       0x4743f9
0x474390: jb       0x474324
0x474394: loopne   0x4743d9
0x474396: inc      edi
0x474397: add      byte ptr [eax], al
0x474399: add      byte ptr [eax], al
0x47439B: add      byte ptr [eax], al
0x47439D: add      byte ptr [eax], al
0x47439F: add      byte ptr [eax], al
0x4743A1: add      byte ptr [eax], al
0x4743A3: add      byte ptr [eax], al
0x4743A5: add      byte ptr [eax], al
0x4743A7: add      byte ptr [eax], al
0x4743A9: add      byte ptr [eax], al
0x4743AB: add      byte ptr [eax], al
0x4743AD: add      byte ptr [eax], al
0x4743AF: add      al, ah
0x4743B1: inc      ebx
0x4743B2: inc      edi
0x4743B3: add      al, ch
0x4743B5: inc      ebx
0x4743B6: inc      edi
0x4743B7: add      byte ptr [eax + 2], dl
0x4743BA: add      byte ptr [eax], al
0x4743BC: mov      al, 0x3d
0x4743BE: inc      edi
0x4743BF: add      al, cl
0x4743C1: xor      byte ptr [eax], al
0x4743C4: aam      0x30
0x4743C6: inc      eax
0x4743C7: add      al, bl
0x4743C9: xor      byte ptr [eax], al
0x4743CC: fdiv     qword ptr [eax]
0x4743CE: inc      eax
0x4743CF: add      al, dl
0x4743D1: xor      byte ptr [eax], al
0x4743D4: inc      eax
0x4743D5: inc      eax
0x4743D7: add      byte ptr [esi + ebp + 0x40], dl
0x4743DB: add      byte ptr [eax], cl
0x4743DD: dec      ecx
0x4743DF: add      byte ptr [ecx], al
0x4743E1: add      bh, bh
0x4743E4: cmp      al, 0x69
0x4743E6: inc      edi
0x4743E7: add      byte ptr [0x796c4654], cl
0x4743ED: imul     ebp, dword ptr [esi + 0x67], 0x64697053
0x4743F4: jb       0x474382 ; "TExplosionSpider"
0x4743F7: rol      byte ptr [esp + eax*2 + 0x47], 0
0x4743FC: add      byte ptr [eax], al
0x4743FE: add      byte ptr [eax], al
0x474400: add      byte ptr [eax], al
0x474402: add      byte ptr [eax], al
0x474404: add      byte ptr [eax], al
0x474406: add      byte ptr [eax], al
0x474408: add      byte ptr [eax], al
0x47440A: add      byte ptr [eax], al
0x47440C: add      byte ptr [eax], al
0x47440E: add      byte ptr [eax], al
0x474410: add      byte ptr [eax], al
0x474412: add      byte ptr [eax], al
0x474414: inc      esp
0x474415: inc      esp
0x474416: inc      edi
0x474417: add      byte ptr [esp + eax*2 + 0x47], ah
0x47441B: add      byte ptr [eax + 2], ah
0x47441E: add      byte ptr [eax], al
0x474420: mov      al, 0x3d
0x474422: inc      edi
0x474423: add      al, cl
0x474425: xor      byte ptr [eax], al
0x474428: aam      0x30
0x47442A: inc      eax
0x47442B: add      al, bl
0x47442D: xor      byte ptr [eax], al
0x474430: fdiv     qword ptr [eax]
0x474432: inc      eax
0x474433: add      al, dl
0x474435: xor      byte ptr [eax], al
0x474438: inc      eax
0x474439: inc      eax
0x47443B: add      byte ptr [esi + ebp + 0x40], dl
0x47443F: add      byte ptr [eax], cl
0x474441: dec      ecx
0x474443: add      byte ptr [0xf3ffff00], al
0x47444A: std      
0x47444B: push     ecx
0x47444E: cli      
0x47444F: inc      dword ptr [eax + 0x6a]
0x474452: inc      edi
0x474453: add      ah, bh
0x474455: outsd    dx, dword ptr [esi]
0x474456: inc      edi
0x474457: add      byte ptr [eax + esi*2 + 0x47], bh
0x47445B: add      byte ptr [ecx + esi*2 + 0x72180047], ch
0x474462: inc      edi
0x474463: add      byte ptr [0x75635354], cl
0x474469: insb     byte ptr es:[edi], dx
0x47446A: jo       0x4744e0
0x47446C: jne      0x4744e0
0x47446E: dec      ebp
0x474470: outsd    dx, dword ptr [esi]
0x474471: outsb    dx, byte ptr [esi]
0x474472: mov      eax, eax
0x474474: rol      byte ptr [edi + eax*2], 0
0x474479: add      byte ptr [eax], al
0x47447B: add      byte ptr [eax], al
0x47447D: add      byte ptr [eax], al
0x47447F: add      byte ptr [eax], al
0x474481: add      byte ptr [eax], al
0x474483: add      byte ptr [eax], al
0x474485: add      byte ptr [eax], al
0x474487: add      byte ptr [eax], al
0x474489: add      byte ptr [eax], al
0x47448B: add      byte ptr [eax], al
0x47448D: add      byte ptr [eax], al
0x47448F: add      byte ptr [eax], al
0x474491: add      byte ptr [eax], al
0x474493: add      al, al
0x474495: inc      esp
0x474496: inc      edi
0x474497: add      byte ptr [eax + 2], ah
0x47449A: add      byte ptr [eax], al
0x47449C: clc      
0x47449D: inc      ebx
0x47449E: inc      edi
0x47449F: add      al, cl
0x4744A1: xor      byte ptr [eax], al
0x4744A4: aam      0x30
0x4744A6: inc      eax
0x4744A7: add      al, bl
0x4744A9: xor      byte ptr [eax], al
0x4744AC: fdiv     qword ptr [eax]
0x4744AE: inc      eax
0x4744AF: add      al, dl
0x4744B1: xor      byte ptr [eax], al
0x4744B4: inc      eax
0x4744B5: inc      eax
0x4744B7: add      byte ptr [esi + ebp + 0x40], dl
0x4744BB: add      byte ptr [eax], cl
0x4744BD: dec      ecx
0x4744BF: add      byte ptr [ecx], dl
0x4744C1: push     esp
0x4744C2: push     ebx
0x4744C3: arpl     word ptr [ebp + 0x6c], si
0x4744C6: jo       0x47453c
0x4744C8: jne      0x47453c
0x4744CA: dec      ebx
0x4744CC: imul     ebp, dword ptr [esi + 0x67], 0x8b6e6f4d
0x4744D3: shl      byte ptr [eax], 0x45
0x4744D6: inc      edi
0x4744D7: add      byte ptr [eax], al
0x4744D9: add      byte ptr [eax], al
0x4744DB: add      byte ptr [eax], al
0x4744DD: add      byte ptr [eax], al
0x4744DF: add      byte ptr [eax], al
0x4744E1: add      byte ptr [eax], al
0x4744E3: add      byte ptr [eax], al
0x4744E5: add      byte ptr [eax], al
0x4744E7: add      byte ptr [eax], al
0x4744E9: add      byte ptr [eax], al
0x4744EB: add      byte ptr [eax], al
0x4744ED: add      byte ptr [eax], al
0x4744EF: add      byte ptr [eax], al
0x4744F1: add      byte ptr [eax], al
0x4744F3: add      byte ptr [eax], ah
0x4744F5: inc      ebp
0x4744F6: inc      edi
0x4744F7: add      byte ptr [eax + 2], dl
0x4744FA: add      byte ptr [eax], al
0x4744FC: mov      al, 0x3d
0x4744FE: inc      edi
0x4744FF: add      al, cl
0x474501: xor      byte ptr [eax], al
0x474504: aam      0x30
0x474506: inc      eax
0x474507: add      al, bl
0x474509: xor      byte ptr [eax], al
0x47450C: fdiv     qword ptr [eax]
0x47450E: inc      eax
0x47450F: add      al, dl
0x474511: xor      byte ptr [eax], al
0x474514: inc      eax
0x474515: inc      eax
0x474517: add      byte ptr [esi + ebp + 0x40], dl
0x47451B: add      byte ptr [eax], cl
0x47451D: dec      ecx
0x47451F: add      byte ptr [eax], dl
0x474521: push     esp
0x474522: push     ebx
0x474523: insd     dword ptr es:[edi], dx
0x474524: popal    
0x474525: insb     byte ptr es:[edi], dx
0x474526: insb     byte ptr es:[edi], dx
0x474527: inc      ebp
0x474528: insb     byte ptr es:[edi], dx
0x474529: dec      bp
0x47452B: outsd    dx, dword ptr [esi]
0x47452C: outsb    dx, byte ptr [esi]
0x47452D: jae      0x4745a3
0x47452F: jb       0x4744bf
0x474532: inc      eax
0x474533: add      byte ptr [eax + 0x4745], al
0x474539: add      byte ptr [eax], al
0x47453B: add      byte ptr [eax], al
0x47453D: add      byte ptr [eax], al
0x47453F: add      byte ptr [eax], al
0x474541: add      byte ptr [eax], al
0x474543: add      byte ptr [eax], al
0x474545: add      byte ptr [eax], al
0x474547: add      byte ptr [eax], al
0x474549: add      byte ptr [eax], al
0x47454B: add      byte ptr [eax], al
0x47454D: add      byte ptr [eax], al
0x47454F: add      byte ptr [eax - 0x77ffb8bb], al
0x474555: inc      ebp
0x474556: inc      edi
0x474557: add      byte ptr [edx + eax], dl
0x47455B: add      byte ptr [eax - 0x37ffb8c3], dh
0x474561: xor      byte ptr [eax], al
0x474564: aam      0x30
0x474566: inc      eax
0x474567: add      al, bl
0x474569: xor      byte ptr [eax], al
0x47456C: fdiv     qword ptr [eax]
0x47456E: inc      eax
0x47456F: add      al, dl
0x474571: xor      byte ptr [eax], al
0x474574: inc      eax
0x474575: inc      eax
0x474577: add      byte ptr [esi + ebp + 0x40], dl
0x47457B: add      byte ptr [eax], cl
0x47457D: dec      ecx
0x47457F: add      byte ptr [ecx], al
0x474581: add      bh, dh
0x474583: call     dword ptr [eax + 0x12004754]
0x474589: push     esp
0x47458A: push     edi
0x47458B: popal    
0x47458C: jb       0x474600
0x47458E: imul     ebp, dword ptr [edi + 0x72], 0x4d666c45
0x474595: outsd    dx, dword ptr [esi]
0x474596: outsb    dx, byte ptr [esi]
0x474597: jae      0x47460d
0x474599: jb       0x47452c
0x47459C: push     ebp
0x47459D: mov      ebp, esp
0x47459F: add      esp, -8
0x4745A2: test     dl, dl
0x4745A4: je       0x4745ae
0x4745A6: add      esp, -0x10
0x4745A9: call     0x4031a4
0x4745AE: mov      byte ptr [ebp - 5], dl
0x4745B1: mov      dword ptr [ebp - 4], eax
0x4745B4: xor      edx, edx
0x4745B6: mov      eax, dword ptr [ebp - 4]
0x4745B9: call     0x4962d0 ; TActor.DMT_-4
0x4745BE: mov      eax, dword ptr [ebp - 4]
0x4745C1: xor      edx, edx
0x4745C3: mov      dword ptr [eax + 0x244], edx
0x4745C9: mov      eax, dword ptr [ebp - 4]
0x4745CC: mov      byte ptr [eax + 0xee], 0
0x4745D3: mov      eax, dword ptr [ebp - 4]
0x4745D6: cmp      byte ptr [ebp - 5], 0
0x4745DA: je       0x4745eb
0x4745DC: call     0x4031fc
0x4745E1: pop      dword ptr fs:[0]
0x4745E8: add      esp, 0xc
0x4745EB: mov      eax, dword ptr [ebp - 4]
0x4745EE: pop      ecx
0x4745EF: pop      ecx
0x4745F0: pop      ebp
0x4745F1: ret      
0x4745F2: mov      eax, eax
0x4745F4: push     ebp
0x4745F5: mov      ebp, esp
0x4745F7: add      esp, -0xc
0x4745FA: mov      dword ptr [ebp - 4], eax
0x4745FD: mov      eax, dword ptr [ebp - 4]
0x474600: mov      dword ptr [eax + 0x1b8], 0xffffffff
0x47460A: mov      eax, dword ptr [ebp - 4]
0x47460D: mov      byte ptr [eax + 0x14c], 0
0x474614: mov      eax, dword ptr [ebp - 4]
0x474617: mov      byte ptr [eax + 0xee], 0
0x47461E: mov      eax, dword ptr [ebp - 4]
0x474621: movzx    eax, word ptr [eax + 0x14]
0x474625: call     0x496120
0x47462A: mov      edx, dword ptr [ebp - 4]
0x47462D: mov      dword ptr [edx + 0xe0], eax
0x474633: mov      eax, dword ptr [ebp - 4]
0x474636: movzx    edx, word ptr [eax + 0x14]
0x47463A: mov      eax, dword ptr [ebp - 4]
0x47463D: movzx    eax, byte ptr [eax + 0xe]
0x474641: call     0x495ca8
0x474646: mov      dword ptr [ebp - 8], eax
0x474649: cmp      dword ptr [ebp - 8], 0
0x47464D: je       0x474c35
0x474653: mov      eax, dword ptr [ebp - 4]
0x474656: mov      eax, dword ptr [eax + 0x148]
0x47465C: add      eax, -9
0x47465F: cmp      eax, 0x19
0x474662: ja       0x474c35
0x474668: mov      al, byte ptr [eax + 0x474675]
0x47466E: jmp      dword ptr [eax*4 + 0x47468f]
0x474675: add      al, byte ptr [ecx]
0x474677: add      al, byte ptr [eax]
0x474679: add      byte ptr [0], al
0x47467F: add      byte ptr [ebx], al
0x474681: add      al, 5
0x474683: add      eax, 0xa000000
0x474688: add      byte ptr [eax], al
0x47468A: add      byte ptr [esi], al
0x47468C: pop      es
0x47468D: or       dword ptr [eax], ecx
0x47468F: xor      eax, 0xbb00474c
0x474694: inc      esi
0x474695: inc      edi
0x474696: add      byte ptr [eax + 0x47], cl
0x474699: inc      edi
0x47469A: add      byte ptr [edi + 0x48], cl
0x47469D: inc      edi
0x47469E: add      al, ch
0x4746A0: dec      eax
0x4746A1: inc      edi
0x4746A2: add      byte ptr [ebp + 0x49], bh
0x4746A5: inc      edi
0x4746A6: add      byte ptr [0x8f00474a], ah
0x4746AC: dec      edx
0x4746AD: inc      edi
0x4746AE: add      byte ptr [ecx], cl
0x4746B0: dec      ebx
0x4746B1: inc      edi
0x4746B2: add      byte ptr [eax - 0x2dffb8b5], cl
0x4746B8: dec      ebx
0x4746B9: inc      edi
0x4746BA: add      byte ptr [ebx - 0x48f007bb], cl
0x4746C0: inc      eax
0x4746C1: add      cl, byte ptr [ebx - 0x48f007ab]
0x4746C7: push     edx
0x4746C8: add      al, 3
0x4746CA: ret      0x558b
0x4746CD: cld      
0x4746CE: movzx    edx, byte ptr [edx + 0xc]
0x4746D2: imul     edx
0x4746D4: mov      edx, dword ptr [ebp - 8]
0x4746D7: movzx    edx, word ptr [edx]
0x4746DA: add      eax, edx
0x4746DC: mov      edx, dword ptr [ebp - 4]
0x4746DF: mov      dword ptr [edx + 0x1b0], eax
0x4746E5: mov      eax, dword ptr [ebp - 8]
0x4746E8: movzx    eax, word ptr [eax + 2]
0x4746EC: mov      edx, dword ptr [ebp - 4]
0x4746EF: add      eax, dword ptr [edx + 0x1b0]
0x4746F5: dec      eax
0x4746F6: mov      edx, dword ptr [ebp - 4]
0x4746F9: mov      dword ptr [edx + 0x1b4], eax
0x4746FF: mov      eax, dword ptr [ebp - 8]
0x474702: movzx    eax, word ptr [eax + 6]
0x474706: mov      edx, dword ptr [ebp - 4]
0x474709: mov      dword ptr [edx + 0x1d0], eax
0x47470F: call     0x406154
0x474714: mov      edx, dword ptr [ebp - 4]
0x474717: mov      dword ptr [edx + 0x1d4], eax
0x47471D: mov      eax, dword ptr [ebp - 8]
0x474720: movzx    eax, word ptr [eax + 2]
0x474724: mov      edx, dword ptr [ebp - 4]
0x474727: mov      dword ptr [edx + 0x1f4], eax
0x47472D: push     0
0x47472F: push     1
0x474731: mov      eax, dword ptr [ebp - 4]
0x474734: xor      edx, edx
0x474736: mov      dl, byte ptr [eax + 0xc]
0x474739: xor      ecx, ecx
0x47473B: mov      eax, dword ptr [ebp - 4]
0x47473E: call     0x497610
0x474743: jmp      0x474c35
0x474748: mov      eax, dword ptr [ebp - 8]
0x47474B: movzx    eax, word ptr [eax + 0xc]
0x47474F: mov      edx, dword ptr [ebp - 8]
0x474752: movzx    edx, word ptr [edx + 0xe]
0x474756: add      eax, edx
0x474758: mov      edx, dword ptr [ebp - 4]
0x47475B: movzx    edx, byte ptr [edx + 0xc]
0x47475F: imul     edx
0x474761: mov      edx, dword ptr [ebp - 8]
0x474764: movzx    edx, word ptr [edx + 0xa]
0x474768: add      eax, edx
0x47476A: mov      edx, dword ptr [ebp - 4]
0x47476D: mov      dword ptr [edx + 0x1b0], eax
0x474773: mov      eax, dword ptr [ebp - 8]
0x474776: movzx    eax, word ptr [eax + 0xc]
0x47477A: mov      edx, dword ptr [ebp - 4]
0x47477D: add      eax, dword ptr [edx + 0x1b0]
0x474783: dec      eax
0x474784: mov      edx, dword ptr [ebp - 4]
0x474787: mov      dword ptr [edx + 0x1b4], eax
0x47478D: mov      eax, dword ptr [ebp - 4]
0x474790: mov      eax, dword ptr [eax + 0x160]
0x474796: mov      edx, dword ptr [ebp - 4]
0x474799: mov      dword ptr [edx + 0x1d0], eax
0x47479F: call     0x406154
0x4747A4: mov      edx, dword ptr [ebp - 4]
0x4747A7: mov      dword ptr [edx + 0x1d4], eax
0x4747AD: mov      eax, dword ptr [ebp - 8]
0x4747B0: movzx    eax, byte ptr [eax + 0x12]
0x4747B4: mov      edx, dword ptr [ebp - 4]
0x4747B7: mov      dword ptr [edx + 0x1d8], eax
0x4747BD: mov      eax, dword ptr [ebp - 4]
0x4747C0: xor      edx, edx
0x4747C2: mov      dword ptr [eax + 0x1dc], edx
0x4747C8: mov      eax, dword ptr [ebp - 4]
0x4747CB: mov      dword ptr [eax + 0x1e0], 1
0x4747D5: mov      eax, dword ptr [ebp - 4]
0x4747D8: cmp      dword ptr [eax + 0x148], 0xb
0x4747DF: jne      0x474815
0x4747E1: push     0
0x4747E3: mov      eax, dword ptr [ebp - 4]
0x4747E6: mov      eax, dword ptr [eax + 0x1b4]
0x4747EC: mov      edx, dword ptr [ebp - 4]
0x4747EF: sub      eax, dword ptr [edx + 0x1b0]
0x4747F5: inc      eax
0x4747F6: push     eax
0x4747F7: mov      eax, dword ptr [ebp - 4]
0x4747FA: xor      edx, edx
0x4747FC: mov      dl, byte ptr [eax + 0xc]
0x4747FF: mov      eax, dword ptr [ebp - 4]
0x474802: mov      ecx, dword ptr [eax + 0x1e0]
0x474808: mov      eax, dword ptr [ebp - 4]
0x47480B: call     0x497610
0x474810: jmp      0x474c35
0x474815: push     0
0x474817: mov      eax, dword ptr [ebp - 4]
0x47481A: mov      eax, dword ptr [eax + 0x1b4]
0x474820: mov      edx, dword ptr [ebp - 4]
0x474823: sub      eax, dword ptr [edx + 0x1b0]
0x474829: inc      eax
0x47482A: push     eax
0x47482B: mov      eax, dword ptr [ebp - 4]
0x47482E: movzx    eax, byte ptr [eax + 0xc]
0x474832: call     0x4a023c
0x474837: mov      edx, eax
0x474839: mov      eax, dword ptr [ebp - 4]
0x47483C: mov      ecx, dword ptr [eax + 0x1e0]
0x474842: mov      eax, dword ptr [ebp - 4]
0x474845: call     0x497610
0x47484A: jmp      0x474c35
0x47484F: mov      eax, dword ptr [ebp - 4]
0x474852: cmp      byte ptr [eax + 0xe], 0x17
0x474856: jne      0x47486a
0x474858: mov      eax, dword ptr [ebp - 8]
0x47485B: movzx    eax, word ptr [eax + 0x3c]
0x47485F: mov      edx, dword ptr [ebp - 4]
0x474862: mov      dword ptr [edx + 0x1b0], eax
0x474868: jmp      0x474895
0x47486A: mov      eax, dword ptr [ebp - 8]
0x47486D: movzx    eax, word ptr [eax + 0x3e]
0x474871: mov      edx, dword ptr [ebp - 8]
0x474874: movzx    edx, word ptr [edx + 0x40]
0x474878: add      eax, edx
0x47487A: mov      edx, dword ptr [ebp - 4]
0x47487D: movzx    edx, byte ptr [edx + 0xc]
0x474881: imul     edx
0x474883: mov      edx, dword ptr [ebp - 8]
0x474886: movzx    edx, word ptr [edx + 0x3c]
0x47488A: add      eax, edx
0x47488C: mov      edx, dword ptr [ebp - 4]
0x47488F: mov      dword ptr [edx + 0x1b0], eax
0x474895: mov      eax, dword ptr [ebp - 8]
0x474898: movzx    eax, word ptr [eax + 0x3e]
0x47489C: mov      edx, dword ptr [ebp - 4]
0x47489F: add      eax, dword ptr [edx + 0x1b0]
0x4748A5: dec      eax
0x4748A6: mov      edx, dword ptr [ebp - 4]
0x4748A9: mov      dword ptr [edx + 0x1b4], eax
0x4748AF: mov      eax, dword ptr [ebp - 8]
0x4748B2: movzx    eax, word ptr [eax + 0x42]
0x4748B6: mov      edx, dword ptr [ebp - 4]
0x4748B9: mov      dword ptr [edx + 0x1d0], eax
0x4748BF: call     0x406154
0x4748C4: mov      edx, dword ptr [ebp - 4]
0x4748C7: mov      dword ptr [edx + 0x1d4], eax
0x4748CD: push     0
0x4748CF: push     1
0x4748D1: mov      eax, dword ptr [ebp - 4]
0x4748D4: xor      edx, edx
0x4748D6: mov      dl, byte ptr [eax + 0xc]
0x4748D9: xor      ecx, ecx
0x4748DB: mov      eax, dword ptr [ebp - 4]
0x4748DE: call     0x497610
0x4748E3: jmp      0x474c35
0x4748E8: mov      eax, dword ptr [ebp - 4]
0x4748EB: cmp      byte ptr [eax + 0xe], 0x37
0x4748EF: jne      0x474c35
0x4748F5: mov      eax, dword ptr [ebp - 8]
0x4748F8: movzx    eax, word ptr [eax + 0x20]
0x4748FC: mov      edx, dword ptr [ebp - 8]
0x4748FF: movzx    edx, word ptr [edx + 0x22]
0x474903: add      eax, edx
0x474905: mov      edx, dword ptr [ebp - 4]
0x474908: movzx    edx, byte ptr [edx + 0xc]
0x47490C: imul     edx
0x47490E: mov      edx, dword ptr [ebp - 8]
0x474911: movzx    edx, word ptr [edx + 0x1e]
0x474915: add      eax, edx
0x474917: mov      edx, dword ptr [ebp - 4]
0x47491A: mov      dword ptr [edx + 0x1b0], eax
0x474920: mov      eax, dword ptr [ebp - 8]
0x474923: movzx    eax, word ptr [eax + 0x20]
0x474927: mov      edx, dword ptr [ebp - 4]
0x47492A: add      eax, dword ptr [edx + 0x1b0]
0x474930: dec      eax
0x474931: mov      edx, dword ptr [ebp - 4]
0x474934: mov      dword ptr [edx + 0x1b4], eax
0x47493A: mov      eax, dword ptr [ebp - 8]
0x47493D: movzx    eax, word ptr [eax + 0x24]
0x474941: mov      edx, dword ptr [ebp - 4]
0x474944: mov      dword ptr [edx + 0x1d0], eax
0x47494A: call     0x406154
0x47494F: mov      edx, dword ptr [ebp - 4]
0x474952: mov      dword ptr [edx + 0x1d4], eax
0x474958: mov      eax, dword ptr [ebp - 4]
0x47495B: mov      byte ptr [eax + 0x14c], 1
0x474962: push     0
0x474964: push     1
0x474966: mov      eax, dword ptr [ebp - 4]
0x474969: xor      edx, edx
0x47496B: mov      dl, byte ptr [eax + 0xc]
0x47496E: xor      ecx, ecx
0x474970: mov      eax, dword ptr [ebp - 4]
0x474973: call     0x497610
0x474978: jmp      0x474c35
0x47497D: mov      eax, dword ptr [ebp - 8]
0x474980: movzx    eax, word ptr [eax + 0x16]
0x474984: mov      edx, dword ptr [ebp - 8]
0x474987: movzx    edx, word ptr [edx + 0x18]
0x47498B: add      eax, edx
0x47498D: mov      edx, dword ptr [ebp - 4]
0x474990: movzx    edx, byte ptr [edx + 0xc]
0x474994: imul     edx
0x474996: mov      edx, dword ptr [ebp - 8]
0x474999: movzx    edx, word ptr [edx + 0x14]
0x47499D: add      eax, edx
0x47499F: mov      edx, dword ptr [ebp - 4]
0x4749A2: mov      dword ptr [edx + 0x1b0], eax
0x4749A8: mov      eax, dword ptr [ebp - 8]
0x4749AB: movzx    eax, word ptr [eax + 0x16]
0x4749AF: mov      edx, dword ptr [ebp - 4]
0x4749B2: add      eax, dword ptr [edx + 0x1b0]
0x4749B8: dec      eax
0x4749B9: mov      edx, dword ptr [ebp - 4]
0x4749BC: mov      dword ptr [edx + 0x1b4], eax
0x4749C2: mov      eax, dword ptr [ebp - 8]
0x4749C5: movzx    eax, word ptr [eax + 0x1a]
0x4749C9: mov      edx, dword ptr [ebp - 4]
0x4749CC: mov      dword ptr [edx + 0x1d0], eax
0x4749D2: call     0x406154
0x4749D7: mov      edx, dword ptr [ebp - 4]
0x4749DA: mov      dword ptr [edx + 0x1d4], eax
0x4749E0: call     0x406154
0x4749E5: mov      edx, dword ptr [ebp - 4]
0x4749E8: mov      dword ptr [edx + 0x150], eax
0x4749EE: mov      eax, dword ptr [ebp - 4]
0x4749F1: cmp      byte ptr [eax + 0xe], 0x10
0x4749F5: je       0x474a00
0x4749F7: mov      eax, dword ptr [ebp - 4]
0x4749FA: cmp      byte ptr [eax + 0xe], 0x36
0x4749FE: jne      0x474a0a
0x474A00: mov      eax, dword ptr [ebp - 4]
0x474A03: mov      byte ptr [eax + 0xee], 1
0x474A0A: push     0
0x474A0C: push     1
0x474A0E: mov      eax, dword ptr [ebp - 4]
0x474A11: xor      edx, edx
0x474A13: mov      dl, byte ptr [eax + 0xc]
0x474A16: xor      ecx, ecx
0x474A18: mov      eax, dword ptr [ebp - 4]
0x474A1B: call     0x497610
0x474A20: jmp      0x474c35
0x474A25: mov      eax, dword ptr [ebp - 8]
0x474A28: movzx    eax, word ptr [eax + 0x2a]
0x474A2C: mov      edx, dword ptr [ebp - 8]
0x474A2F: movzx    edx, word ptr [edx + 0x2c]
0x474A33: add      eax, edx
0x474A35: mov      edx, dword ptr [ebp - 4]
0x474A38: movzx    edx, byte ptr [edx + 0xc]
0x474A3C: imul     edx
0x474A3E: mov      edx, dword ptr [ebp - 8]
0x474A41: movzx    edx, word ptr [edx + 0x28]
0x474A45: add      eax, edx
0x474A47: mov      edx, dword ptr [ebp - 4]
0x474A4A: mov      dword ptr [edx + 0x1b0], eax
0x474A50: mov      eax, dword ptr [ebp - 8]
0x474A53: movzx    eax, word ptr [eax + 0x2a]
0x474A57: mov      edx, dword ptr [ebp - 4]
0x474A5A: add      eax, dword ptr [edx + 0x1b0]
0x474A60: dec      eax
0x474A61: mov      edx, dword ptr [ebp - 4]
0x474A64: mov      dword ptr [edx + 0x1b4], eax
0x474A6A: mov      eax, dword ptr [ebp - 4]
0x474A6D: mov      eax, dword ptr [eax + 0x1e8]
0x474A73: mov      edx, dword ptr [ebp - 4]
0x474A76: mov      dword ptr [edx + 0x1d0], eax
0x474A7C: call     0x406154
0x474A81: mov      edx, dword ptr [ebp - 4]
0x474A84: mov      dword ptr [edx + 0x1d4], eax
0x474A8A: jmp      0x474c35
0x474A8F: mov      eax, dword ptr [ebp - 8]
0x474A92: movzx    eax, word ptr [eax + 0x34]
0x474A96: mov      edx, dword ptr [ebp - 8]
0x474A99: movzx    edx, word ptr [edx + 0x36]
0x474A9D: add      eax, edx
0x474A9F: mov      edx, dword ptr [ebp - 4]
0x474AA2: movzx    edx, byte ptr [edx + 0xc]
0x474AA6: imul     edx
0x474AA8: mov      edx, dword ptr [ebp - 8]
0x474AAB: movzx    edx, word ptr [edx + 0x32]
0x474AAF: add      eax, edx
0x474AB1: mov      edx, dword ptr [ebp - 4]
0x474AB4: mov      dword ptr [edx + 0x1b0], eax
0x474ABA: mov      eax, dword ptr [ebp - 8]
0x474ABD: movzx    eax, word ptr [eax + 0x34]
0x474AC1: mov      edx, dword ptr [ebp - 4]
0x474AC4: add      eax, dword ptr [edx + 0x1b0]
0x474ACA: dec      eax
0x474ACB: mov      edx, dword ptr [ebp - 4]
0x474ACE: mov      dword ptr [edx + 0x1b4], eax
0x474AD4: mov      eax, dword ptr [ebp - 4]
0x474AD7: mov      eax, dword ptr [eax + 0x1b4]
0x474ADD: mov      edx, dword ptr [ebp - 4]
0x474AE0: mov      dword ptr [edx + 0x1b0], eax
0x474AE6: mov      eax, dword ptr [ebp - 8]
0x474AE9: movzx    eax, word ptr [eax + 0x38]
0x474AED: mov      edx, dword ptr [ebp - 4]
0x474AF0: mov      dword ptr [edx + 0x1d0], eax
0x474AF6: call     0x406154
0x474AFB: mov      edx, dword ptr [ebp - 4]
0x474AFE: mov      dword ptr [edx + 0x1d4], eax
0x474B04: jmp      0x474c35
0x474B09: mov      eax, dword ptr [ebp - 8]
0x474B0C: movzx    eax, word ptr [eax + 0x34]
0x474B10: mov      edx, dword ptr [ebp - 8]
0x474B13: movzx    edx, word ptr [edx + 0x36]
0x474B17: add      eax, edx
0x474B19: mov      edx, dword ptr [ebp - 4]
0x474B1C: movzx    edx, byte ptr [edx + 0xc]
0x474B20: imul     edx
0x474B22: mov      edx, dword ptr [ebp - 8]
0x474B25: movzx    edx, word ptr [edx + 0x32]
0x474B29: add      eax, edx
0x474B2B: mov      edx, dword ptr [ebp - 4]
0x474B2E: mov      dword ptr [edx + 0x1b0], eax
0x474B34: mov      eax, dword ptr [ebp - 8]
0x474B37: movzx    eax, word ptr [eax + 0x34]
0x474B3B: mov      edx, dword ptr [ebp - 4]
0x474B3E: add      eax, dword ptr [edx + 0x1b0]
0x474B44: dec      eax
0x474B45: mov      edx, dword ptr [ebp - 4]
0x474B48: mov      dword ptr [edx + 0x1b4], eax
0x474B4E: mov      eax, dword ptr [ebp - 8]
0x474B51: movzx    eax, word ptr [eax + 0x38]
0x474B55: mov      edx, dword ptr [ebp - 4]
0x474B58: mov      dword ptr [edx + 0x1d0], eax
0x474B5E: call     0x406154
0x474B63: mov      edx, dword ptr [ebp - 4]
0x474B66: mov      dword ptr [edx + 0x1d4], eax
0x474B6C: mov      eax, dword ptr [ebp - 4]
0x474B6F: cmp      byte ptr [eax + 0xe], 0x16
0x474B73: je       0x474c35
0x474B79: mov      eax, dword ptr [ebp - 4]
0x474B7C: mov      byte ptr [eax + 0xee], 1
0x474B83: jmp      0x474c35
0x474B88: mov      eax, dword ptr [ebp - 8]
0x474B8B: movzx    eax, word ptr [eax + 0x3c]
0x474B8F: mov      edx, dword ptr [ebp - 4]
0x474B92: mov      dword ptr [edx + 0x1b0], eax
0x474B98: mov      eax, dword ptr [ebp - 8]
0x474B9B: movzx    eax, word ptr [eax + 0x3e]
0x474B9F: mov      edx, dword ptr [ebp - 4]
0x474BA2: add      eax, dword ptr [edx + 0x1b0]
0x474BA8: dec      eax
0x474BA9: mov      edx, dword ptr [ebp - 4]
0x474BAC: mov      dword ptr [edx + 0x1b4], eax
0x474BB2: mov      eax, dword ptr [ebp - 8]
0x474BB5: movzx    eax, word ptr [eax + 0x42]
0x474BB9: mov      edx, dword ptr [ebp - 4]
0x474BBC: mov      dword ptr [edx + 0x1d0], eax
0x474BC2: call     0x406154
0x474BC7: mov      edx, dword ptr [ebp - 4]
0x474BCA: mov      dword ptr [edx + 0x1d4], eax
0x474BD0: jmp      0x474c35
0x474BD2: mov      eax, dword ptr [ebp - 8]
0x474BD5: movzx    eax, word ptr [eax + 0x3e]
0x474BD9: mov      edx, dword ptr [ebp - 8]
0x474BDC: movzx    edx, word ptr [edx + 0x40]
0x474BE0: add      eax, edx
0x474BE2: mov      edx, dword ptr [ebp - 4]
0x474BE5: movzx    edx, byte ptr [edx + 0xc]
0x474BE9: imul     edx
0x474BEB: mov      edx, dword ptr [ebp - 8]
0x474BEE: movzx    edx, word ptr [edx + 0x3c]
0x474BF2: add      eax, edx
0x474BF4: mov      edx, dword ptr [ebp - 4]
0x474BF7: mov      dword ptr [edx + 0x1b0], eax
0x474BFD: mov      eax, dword ptr [ebp - 8]
0x474C00: movzx    eax, word ptr [eax + 0x3e]
0x474C04: mov      edx, dword ptr [ebp - 4]
0x474C07: add      eax, dword ptr [edx + 0x1b0]
0x474C0D: dec      eax
0x474C0E: mov      edx, dword ptr [ebp - 4]
0x474C11: mov      dword ptr [edx + 0x1b4], eax
0x474C17: mov      eax, dword ptr [ebp - 8]
0x474C1A: movzx    eax, word ptr [eax + 0x42]
0x474C1E: mov      edx, dword ptr [ebp - 4]
0x474C21: mov      dword ptr [edx + 0x1d0], eax
0x474C27: call     0x406154
0x474C2C: mov      edx, dword ptr [ebp - 4]
0x474C2F: mov      dword ptr [edx + 0x1d4], eax
0x474C35: mov      esp, ebp
0x474C37: pop      ebp
0x474C38: ret      
0x474C39: lea      eax, [eax]
0x474C3C: push     ebp
0x474C3D: mov      ebp, esp
0x474C3F: add      esp, -0x18
0x474C42: mov      byte ptr [ebp - 0x11], dl
0x474C45: mov      dword ptr [ebp - 4], eax
0x474C48: mov      eax, dword ptr [ebp - 4]
0x474C4B: movzx    edx, word ptr [eax + 0x14]
0x474C4F: mov      eax, dword ptr [ebp - 4]
0x474C52: movzx    eax, byte ptr [eax + 0xe]
0x474C56: call     0x495ca8
0x474C5B: mov      dword ptr [ebp - 0x10], eax
0x474C5E: cmp      dword ptr [ebp - 0x10], 0
0x474C62: je       0x474d4c
0x474C68: mov      eax, dword ptr [ebp - 4]
0x474C6B: cmp      byte ptr [eax + 0x20], 0
0x474C6F: je       0x474cdd
0x474C71: mov      eax, dword ptr [ebp - 4]
0x474C74: mov      ax, word ptr [eax + 0x14]
0x474C78: add      eax, -0x1e
0x474C7B: sub      ax, 5
0x474C7F: jb       0x474c87
0x474C81: sub      ax, 0x74
0x474C85: jne      0x474c94
0x474C87: mov      eax, dword ptr [ebp - 4]
0x474C8A: mov      dword ptr [eax + 0xc0], 1
0x474C94: mov      eax, dword ptr [ebp - 4]
0x474C97: cmp      byte ptr [eax + 0x21], 0
0x474C9B: je       0x474cac
0x474C9D: mov      eax, dword ptr [ebp - 0x10]
0x474CA0: movzx    eax, word ptr [eax + 0x3c]
0x474CA4: mov      dword ptr [ebp - 8], eax
0x474CA7: jmp      0x474d4c
0x474CAC: mov      eax, dword ptr [ebp - 0x10]
0x474CAF: movzx    eax, word ptr [eax + 0x34]
0x474CB3: mov      edx, dword ptr [ebp - 0x10]
0x474CB6: movzx    edx, word ptr [edx + 0x36]
0x474CBA: add      eax, edx
0x474CBC: mov      edx, dword ptr [ebp - 4]
0x474CBF: movzx    edx, byte ptr [edx + 0xc]
0x474CC3: imul     edx
0x474CC5: mov      edx, dword ptr [ebp - 0x10]
0x474CC8: movzx    edx, word ptr [edx + 0x32]
0x474CCC: add      eax, edx
0x474CCE: mov      edx, dword ptr [ebp - 0x10]
0x474CD1: movzx    edx, word ptr [edx + 0x34]
0x474CD5: dec      edx
0x474CD6: add      eax, edx
0x474CD8: mov      dword ptr [ebp - 8], eax
0x474CDB: jmp      0x474d4c
0x474CDD: mov      eax, dword ptr [ebp - 0x10]
0x474CE0: movzx    eax, word ptr [eax + 2]
0x474CE4: mov      edx, dword ptr [ebp - 4]
0x474CE7: mov      dword ptr [edx + 0x1f4], eax
0x474CED: mov      eax, dword ptr [ebp - 4]
0x474CF0: cmp      dword ptr [eax + 0x1ec], 0
0x474CF7: jge      0x474d00
0x474CF9: xor      eax, eax
0x474CFB: mov      dword ptr [ebp - 0xc], eax
0x474CFE: jmp      0x474d25
0x474D00: mov      eax, dword ptr [ebp - 0x10]
0x474D03: movzx    eax, word ptr [eax + 2]
0x474D07: mov      edx, dword ptr [ebp - 4]
0x474D0A: cmp      eax, dword ptr [edx + 0x1ec]
0x474D10: jg       0x474d19
0x474D12: xor      eax, eax
0x474D14: mov      dword ptr [ebp - 0xc], eax
0x474D17: jmp      0x474d25
0x474D19: mov      eax, dword ptr [ebp - 4]
0x474D1C: mov      eax, dword ptr [eax + 0x1ec]
0x474D22: mov      dword ptr [ebp - 0xc], eax
0x474D25: mov      eax, dword ptr [ebp - 0x10]
0x474D28: movzx    eax, word ptr [eax + 2]
0x474D2C: mov      edx, dword ptr [ebp - 0x10]
0x474D2F: movzx    edx, word ptr [edx + 4]
0x474D33: add      eax, edx
0x474D35: mov      edx, dword ptr [ebp - 4]
0x474D38: movzx    edx, byte ptr [edx + 0xc]
0x474D3C: imul     edx
0x474D3E: mov      edx, dword ptr [ebp - 0x10]
0x474D41: movzx    edx, word ptr [edx]
0x474D44: add      eax, edx
0x474D46: add      eax, dword ptr [ebp - 0xc]
0x474D49: mov      dword ptr [ebp - 8], eax
0x474D4C: mov      eax, dword ptr [ebp - 8]
0x474D4F: mov      esp, ebp
0x474D51: pop      ebp
0x474D52: ret      
0x474D53: nop      
0x474D54: push     ebp
0x474D55: mov      ebp, esp
0x474D57: push     ecx
0x474D58: mov      dword ptr [ebp - 4], eax
0x474D5B: mov      eax, dword ptr [ebp - 4]
0x474D5E: call     0x497df4 ; TActor.DMT_-13
0x474D63: mov      eax, dword ptr [ebp - 4]
0x474D66: movzx    eax, byte ptr [eax + 0xe]
0x474D6A: cmp      eax, 0x16
0x474D6D: jg       0x474d81
0x474D6F: je       0x474d8f
0x474D71: add      eax, -0xe
0x474D74: sub      eax, 2
0x474D77: jb       0x474d8f
0x474D79: dec      eax
0x474D7A: je       0x474d8f
0x474D7C: jmp      0x474e53
0x474D81: sub      eax, 0x17
0x474D84: je       0x474de7
0x474D86: sub      eax, 0x1e
0x474D89: jne      0x474e53
0x474D8F: mov      eax, dword ptr [ebp - 4]
0x474D92: cmp      byte ptr [eax + 0xee], 0
0x474D99: je       0x474e53
0x474D9F: mov      eax, dword ptr [ebp - 4]
0x474DA2: add      eax, 0x24c
0x474DA7: push     eax
0x474DA8: mov      eax, dword ptr [ebp - 4]
0x474DAB: lea      ecx, [eax + 0x248]
0x474DB1: mov      eax, dword ptr [ebp - 4]
0x474DB4: mov      edx, dword ptr [eax + 0x1b8]
0x474DBA: add      edx, 0x154
0x474DC0: mov      eax, dword ptr [ebp - 4]
0x474DC3: sub      edx, dword ptr [eax + 0x1b0]
0x474DC9: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x474DCE: mov      eax, dword ptr [eax]
0x474DD0: mov      eax, dword ptr [eax + 0x358]
0x474DD6: call     0x453a48
0x474DDB: mov      edx, dword ptr [ebp - 4]
0x474DDE: mov      dword ptr [edx + 0x244], eax
0x474DE4: pop      ecx
0x474DE5: pop      ebp
0x474DE6: ret      
0x474DE7: mov      eax, dword ptr [ebp - 4]
0x474DEA: cmp      dword ptr [eax + 0x148], 0x14
0x474DF1: jne      0x474e49
0x474DF3: mov      eax, dword ptr [ebp - 4]
0x474DF6: xor      edx, edx
0x474DF8: mov      dword ptr [eax + 0x140], edx
0x474DFE: mov      eax, dword ptr [ebp - 4]
0x474E01: add      eax, 0x24c
0x474E06: push     eax
0x474E07: mov      eax, dword ptr [ebp - 4]
0x474E0A: lea      ecx, [eax + 0x248]
0x474E10: mov      eax, dword ptr [ebp - 4]
0x474E13: mov      edx, dword ptr [eax + 0xe0]
0x474E19: mov      eax, dword ptr [ebp - 4]
0x474E1C: add      edx, dword ptr [eax + 0x1b8]
0x474E22: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x474E27: mov      eax, dword ptr [eax]
0x474E29: mov      eax, dword ptr [eax + 0x360]
0x474E2F: call     0x453a48
0x474E34: mov      edx, dword ptr [ebp - 4]
0x474E37: mov      dword ptr [edx + 0x244], eax
0x474E3D: mov      eax, dword ptr [ebp - 4]
0x474E40: mov      byte ptr [eax + 0xee], 1
0x474E47: jmp      0x474e53
0x474E49: mov      eax, dword ptr [ebp - 4]
0x474E4C: mov      byte ptr [eax + 0xee], 0
0x474E53: pop      ecx
0x474E54: pop      ebp
0x474E55: ret      
0x474E56: mov      eax, eax
0x474E58: push     ebp
0x474E59: mov      ebp, esp
0x474E5B: add      esp, -0x14
0x474E5E: push     ebx
0x474E5F: mov      dword ptr [ebp - 4], eax
0x474E62: mov      eax, dword ptr [ebp - 4]
0x474E65: cmp      dword ptr [eax + 0x148], 0xb
0x474E6C: je       0x475089
0x474E72: mov      eax, dword ptr [ebp - 4]
0x474E75: cmp      dword ptr [eax + 0x148], 9
0x474E7C: je       0x475089
0x474E82: mov      eax, dword ptr [ebp - 4]
0x474E85: cmp      dword ptr [eax + 0x148], 0xd
0x474E8C: je       0x475089
0x474E92: mov      eax, dword ptr [ebp - 4]
0x474E95: mov      byte ptr [eax + 0x1e4], 0
0x474E9C: mov      eax, dword ptr [ebp - 4]
0x474E9F: mov      eax, dword ptr [eax + 0x220]
0x474EA5: cmp      dword ptr [eax + 8], 2
0x474EA9: jl       0x474eb5
0x474EAB: mov      eax, dword ptr [ebp - 4]
0x474EAE: mov      byte ptr [eax + 0x1e4], 1
0x474EB5: mov      eax, dword ptr [ebp - 4]
0x474EB8: mov      edx, dword ptr [eax + 0x1b8]
0x474EBE: mov      eax, dword ptr [ebp - 4]
0x474EC1: sub      edx, dword ptr [eax + 0x1b0]
0x474EC7: mov      eax, dword ptr [ebp - 4]
0x474ECA: mov      bx, 0xfff8
0x474ECE: call     0x403080
0x474ED3: mov      eax, dword ptr [ebp - 4]
0x474ED6: mov      edx, dword ptr [eax + 0x1b8]
0x474EDC: mov      eax, dword ptr [ebp - 4]
0x474EDF: sub      edx, dword ptr [eax + 0x1b0]
0x474EE5: mov      eax, dword ptr [ebp - 4]
0x474EE8: mov      bx, 0xfff7
0x474EEC: call     0x403080
0x474EF1: mov      eax, dword ptr [ebp - 4]
0x474EF4: mov      eax, dword ptr [eax + 0x1b8]
0x474EFA: mov      dword ptr [ebp - 8], eax
0x474EFD: mov      eax, dword ptr [ebp - 4]
0x474F00: cmp      dword ptr [eax + 0x148], 0
0x474F07: je       0x474ff5
0x474F0D: mov      eax, dword ptr [ebp - 4]
0x474F10: mov      eax, dword ptr [eax + 0x1b8]
0x474F16: mov      edx, dword ptr [ebp - 4]
0x474F19: cmp      eax, dword ptr [edx + 0x1b0]
0x474F1F: jl       0x474f35
0x474F21: mov      eax, dword ptr [ebp - 4]
0x474F24: mov      eax, dword ptr [eax + 0x1b8]
0x474F2A: mov      edx, dword ptr [ebp - 4]
0x474F2D: cmp      eax, dword ptr [edx + 0x1b4]
0x474F33: jle      0x474f47
0x474F35: mov      eax, dword ptr [ebp - 4]
0x474F38: mov      eax, dword ptr [eax + 0x1b0]
0x474F3E: mov      edx, dword ptr [ebp - 4]
0x474F41: mov      dword ptr [edx + 0x1b8], eax
0x474F47: mov      eax, dword ptr [ebp - 4]
0x474F4A: cmp      byte ptr [eax + 0x1e4], 0
0x474F51: je       0x474f79
0x474F53: mov      eax, dword ptr [ebp - 4]
0x474F56: mov      eax, dword ptr [eax + 0x1d0]
0x474F5C: add      eax, eax
0x474F5E: mov      dword ptr [ebp - 0x14], eax
0x474F61: xor      eax, eax
0x474F63: mov      dword ptr [ebp - 0x10], eax
0x474F66: fild     qword ptr [ebp - 0x14]
0x474F69: fdiv     dword ptr [0x475090]
0x474F6F: call     0x4029a0
0x474F74: mov      dword ptr [ebp - 0xc], eax
0x474F77: jmp      0x474f85
0x474F79: mov      eax, dword ptr [ebp - 4]
0x474F7C: mov      eax, dword ptr [eax + 0x1d0]
0x474F82: mov      dword ptr [ebp - 0xc], eax
0x474F85: call     0x406154
0x474F8A: mov      edx, dword ptr [ebp - 4]
0x474F8D: sub      eax, dword ptr [edx + 0x1d4]
0x474F93: cmp      eax, dword ptr [ebp - 0xc]
0x474F96: jbe      0x474fda
0x474F98: mov      eax, dword ptr [ebp - 4]
0x474F9B: mov      eax, dword ptr [eax + 0x1b8]
0x474FA1: mov      edx, dword ptr [ebp - 4]
0x474FA4: cmp      eax, dword ptr [edx + 0x1b4]
0x474FAA: jge      0x474fc5
0x474FAC: mov      eax, dword ptr [ebp - 4]
0x474FAF: inc      dword ptr [eax + 0x1b8]
0x474FB5: call     0x406154
0x474FBA: mov      edx, dword ptr [ebp - 4]
0x474FBD: mov      dword ptr [edx + 0x1d4], eax
0x474FC3: jmp      0x474fda
0x474FC5: mov      eax, dword ptr [ebp - 4]
0x474FC8: xor      edx, edx
0x474FCA: mov      dword ptr [eax + 0x148], edx
0x474FD0: mov      eax, dword ptr [ebp - 4]
0x474FD3: mov      byte ptr [eax + 0xee], 0
0x474FDA: mov      eax, dword ptr [ebp - 4]
0x474FDD: xor      edx, edx
0x474FDF: mov      dword ptr [eax + 0x1ec], edx
0x474FE5: call     0x406154
0x474FEA: mov      edx, dword ptr [ebp - 4]
0x474FED: mov      dword ptr [edx + 0x1f0], eax
0x474FF3: jmp      0x475061
0x474FF5: call     0x406154
0x474FFA: mov      edx, dword ptr [ebp - 4]
0x474FFD: sub      eax, dword ptr [edx + 0x1fc]
0x475003: cmp      eax, 0xc8
0x475008: jbe      0x475061
0x47500A: call     0x406154
0x47500F: mov      edx, dword ptr [ebp - 4]
0x475012: sub      eax, dword ptr [edx + 0x1f0]
0x475018: cmp      eax, 0x1f4
0x47501D: jbe      0x475055
0x47501F: call     0x406154
0x475024: mov      edx, dword ptr [ebp - 4]
0x475027: mov      dword ptr [edx + 0x1f0], eax
0x47502D: mov      eax, dword ptr [ebp - 4]
0x475030: inc      dword ptr [eax + 0x1ec]
0x475036: mov      eax, dword ptr [ebp - 4]
0x475039: mov      eax, dword ptr [eax + 0x1ec]
0x47503F: mov      edx, dword ptr [ebp - 4]
0x475042: cmp      eax, dword ptr [edx + 0x1f4]
0x475048: jl       0x475055
0x47504A: mov      eax, dword ptr [ebp - 4]
0x47504D: xor      edx, edx
0x47504F: mov      dword ptr [eax + 0x1ec], edx
0x475055: mov      eax, dword ptr [ebp - 4]
0x475058: mov      bx, 0xfffe
0x47505C: call     0x403080
0x475061: mov      eax, dword ptr [ebp - 8]
0x475064: mov      edx, dword ptr [ebp - 4]
0x475067: cmp      eax, dword ptr [edx + 0x1b8]
0x47506D: je       0x475089
0x47506F: call     0x406154
0x475074: mov      edx, dword ptr [ebp - 4]
0x475077: mov      dword ptr [edx + 0x204], eax
0x47507D: mov      eax, dword ptr [ebp - 4]
0x475080: mov      bx, 0xfff3
0x475084: call     0x403080
0x475089: pop      ebx
0x47508A: mov      esp, ebp
0x47508C: pop      ebp
0x47508D: ret      
0x47508E: add      byte ptr [eax], al
0x475090: add      byte ptr [eax], al
0x475092: inc      eax
0x475093: inc      eax
0x475094: push     ebp
0x475095: mov      ebp, esp
0x475097: add      esp, -0x18
0x47509A: push     ebx
0x47509B: mov      dword ptr [ebp - 0xc], ecx
0x47509E: mov      dword ptr [ebp - 8], edx
0x4750A1: mov      dword ptr [ebp - 4], eax
0x4750A4: mov      eax, dword ptr [ebp - 4]
0x4750A7: mov      al, byte ptr [eax + 0xc]
0x4750AA: sub      al, 8
0x4750AC: jae      0x475196
0x4750B2: call     0x406154
0x4750B7: mov      edx, dword ptr [ebp - 4]
0x4750BA: sub      eax, dword ptr [edx + 0x204]
0x4750C0: cmp      eax, 0xea60
0x4750C5: jbe      0x4750e1
0x4750C7: call     0x406154
0x4750CC: mov      edx, dword ptr [ebp - 4]
0x4750CF: mov      dword ptr [edx + 0x204], eax
0x4750D5: mov      eax, dword ptr [ebp - 4]
0x4750D8: mov      bx, 0xfff3
0x4750DC: call     0x403080
0x4750E1: mov      eax, dword ptr [ebp - 4]
0x4750E4: call     0x498170
0x4750E9: mov      byte ptr [ebp - 0xd], al
0x4750EC: mov      eax, dword ptr [ebp - 4]
0x4750EF: cmp      dword ptr [eax + 0x140], 0
0x4750F6: je       0x475140
0x4750F8: mov      eax, dword ptr [ebp - 0xc]
0x4750FB: mov      edx, dword ptr [ebp - 4]
0x4750FE: add      eax, dword ptr [edx + 0xa0]
0x475104: mov      edx, dword ptr [ebp - 4]
0x475107: add      eax, dword ptr [edx + 0x98]
0x47510D: push     eax
0x47510E: mov      eax, dword ptr [ebp + 0xc]
0x475111: mov      edx, dword ptr [ebp - 4]
0x475114: add      eax, dword ptr [edx + 0xac]
0x47511A: mov      edx, dword ptr [ebp - 4]
0x47511D: add      eax, dword ptr [edx + 0x9c]
0x475123: push     eax
0x475124: mov      al, byte ptr [ebp + 8]
0x475127: push     eax
0x475128: mov      al, byte ptr [ebp - 0xd]
0x47512B: push     eax
0x47512C: mov      eax, dword ptr [ebp - 4]
0x47512F: mov      ecx, dword ptr [eax + 0x140]
0x475135: mov      edx, dword ptr [ebp - 8]
0x475138: mov      eax, dword ptr [ebp - 4]
0x47513B: call     0x497fd0
0x475140: mov      eax, dword ptr [ebp - 4]
0x475143: cmp      byte ptr [eax + 0xee], 0
0x47514A: je       0x475196
0x47514C: mov      eax, dword ptr [ebp - 4]
0x47514F: cmp      dword ptr [eax + 0x244], 0
0x475156: je       0x475196
0x475158: mov      eax, dword ptr [ebp - 4]
0x47515B: mov      eax, dword ptr [eax + 0x244]
0x475161: push     eax
0x475162: push     1
0x475164: mov      ecx, dword ptr [ebp + 0xc]
0x475167: mov      eax, dword ptr [ebp - 4]
0x47516A: add      ecx, dword ptr [eax + 0x24c]
0x475170: mov      eax, dword ptr [ebp - 4]
0x475173: add      ecx, dword ptr [eax + 0x9c]
0x475179: mov      edx, dword ptr [ebp - 0xc]
0x47517C: mov      eax, dword ptr [ebp - 4]
0x47517F: add      edx, dword ptr [eax + 0x248]
0x475185: mov      eax, dword ptr [ebp - 4]
0x475188: add      edx, dword ptr [eax + 0x98]
0x47518E: mov      eax, dword ptr [ebp - 8]
0x475191: call     0x455578
0x475196: pop      ebx
0x475197: mov      esp, ebp
0x475199: pop      ebp
0x47519A: ret      8
0x47519D: lea      eax, [eax]
0x4751A0: push     ebp
0x4751A1: mov      ebp, esp
0x4751A3: add      esp, -0x18
0x4751A6: push     ebx
0x4751A7: mov      dword ptr [ebp - 4], eax
0x4751AA: mov      eax, dword ptr [ebp - 4]
0x4751AD: cmp      dword ptr [eax + 0x148], 0xb
0x4751B4: je       0x475486
0x4751BA: mov      eax, dword ptr [ebp - 4]
0x4751BD: cmp      dword ptr [eax + 0x148], 9
0x4751C4: je       0x475486
0x4751CA: mov      eax, dword ptr [ebp - 4]
0x4751CD: cmp      dword ptr [eax + 0x148], 0xd
0x4751D4: je       0x475486
0x4751DA: mov      eax, dword ptr [ebp - 4]
0x4751DD: mov      byte ptr [eax + 0x1e4], 0
0x4751E4: mov      eax, dword ptr [ebp - 4]
0x4751E7: mov      eax, dword ptr [eax + 0x220]
0x4751ED: cmp      dword ptr [eax + 8], 2
0x4751F1: jl       0x4751fd
0x4751F3: mov      eax, dword ptr [ebp - 4]
0x4751F6: mov      byte ptr [eax + 0x1e4], 1
0x4751FD: mov      eax, dword ptr [ebp - 4]
0x475200: mov      edx, dword ptr [eax + 0x1b8]
0x475206: mov      eax, dword ptr [ebp - 4]
0x475209: sub      edx, dword ptr [eax + 0x1b0]
0x47520F: mov      eax, dword ptr [ebp - 4]
0x475212: mov      bx, 0xfff8
0x475216: call     0x403080
0x47521B: mov      eax, dword ptr [ebp - 4]
0x47521E: mov      edx, dword ptr [eax + 0x1b8]
0x475224: mov      eax, dword ptr [ebp - 4]
0x475227: sub      edx, dword ptr [eax + 0x1b0]
0x47522D: mov      eax, dword ptr [ebp - 4]
0x475230: mov      bx, 0xfff7
0x475234: call     0x403080
0x475239: mov      eax, dword ptr [ebp - 4]
0x47523C: mov      eax, dword ptr [eax + 0x1b8]
0x475242: mov      dword ptr [ebp - 8], eax
0x475245: mov      eax, dword ptr [ebp - 4]
0x475248: cmp      dword ptr [eax + 0x148], 0
0x47524F: je       0x4753f2
0x475255: mov      eax, dword ptr [ebp - 4]
0x475258: mov      eax, dword ptr [eax + 0x1b8]
0x47525E: mov      edx, dword ptr [ebp - 4]
0x475261: cmp      eax, dword ptr [edx + 0x1b0]
0x475267: jl       0x47527d
0x475269: mov      eax, dword ptr [ebp - 4]
0x47526C: mov      eax, dword ptr [eax + 0x1b8]
0x475272: mov      edx, dword ptr [ebp - 4]
0x475275: cmp      eax, dword ptr [edx + 0x1b4]
0x47527B: jle      0x47528f
0x47527D: mov      eax, dword ptr [ebp - 4]
0x475280: mov      eax, dword ptr [eax + 0x1b0]
0x475286: mov      edx, dword ptr [ebp - 4]
0x475289: mov      dword ptr [edx + 0x1b8], eax
0x47528F: mov      eax, dword ptr [ebp - 4]
0x475292: cmp      byte ptr [eax + 0x1e4], 0
0x475299: je       0x4752c1
0x47529B: mov      eax, dword ptr [ebp - 4]
0x47529E: mov      eax, dword ptr [eax + 0x1d0]
0x4752A4: add      eax, eax
0x4752A6: mov      dword ptr [ebp - 0x18], eax
0x4752A9: xor      eax, eax
0x4752AB: mov      dword ptr [ebp - 0x14], eax
0x4752AE: fild     qword ptr [ebp - 0x18]
0x4752B1: fdiv     dword ptr [0x47548c]
0x4752B7: call     0x4029a0
0x4752BC: mov      dword ptr [ebp - 0xc], eax
0x4752BF: jmp      0x4752cd
0x4752C1: mov      eax, dword ptr [ebp - 4]
0x4752C4: mov      eax, dword ptr [eax + 0x1d0]
0x4752CA: mov      dword ptr [ebp - 0xc], eax
0x4752CD: call     0x406154
0x4752D2: mov      edx, dword ptr [ebp - 4]
0x4752D5: sub      eax, dword ptr [edx + 0x1d4]
0x4752DB: cmp      eax, dword ptr [ebp - 0xc]
0x4752DE: jbe      0x4753d7
0x4752E4: mov      eax, dword ptr [ebp - 4]
0x4752E7: mov      eax, dword ptr [eax + 0x1b8]
0x4752ED: mov      edx, dword ptr [ebp - 4]
0x4752F0: cmp      eax, dword ptr [edx + 0x1b4]
0x4752F6: jge      0x475311
0x4752F8: mov      eax, dword ptr [ebp - 4]
0x4752FB: inc      dword ptr [eax + 0x1b8]
0x475301: call     0x406154
0x475306: mov      edx, dword ptr [ebp - 4]
0x475309: mov      dword ptr [edx + 0x1d4], eax
0x47530F: jmp      0x475326
0x475311: mov      eax, dword ptr [ebp - 4]
0x475314: xor      edx, edx
0x475316: mov      dword ptr [eax + 0x148], edx
0x47531C: mov      eax, dword ptr [ebp - 4]
0x47531F: mov      byte ptr [eax + 0xee], 0
0x475326: mov      eax, dword ptr [ebp - 4]
0x475329: cmp      dword ptr [eax + 0x148], 0x16
0x475330: jne      0x4753d7
0x475336: mov      eax, dword ptr [ebp - 4]
0x475339: mov      eax, dword ptr [eax + 0x1b8]
0x47533F: mov      edx, dword ptr [ebp - 4]
0x475342: sub      eax, dword ptr [edx + 0x1b0]
0x475348: cmp      eax, 2
0x47534B: jne      0x4753d7
0x475351: mov      eax, dword ptr [ebp - 4]
0x475354: movzx    eax, word ptr [eax + 0xa]
0x475358: push     eax
0x475359: mov      eax, dword ptr [ebp - 4]
0x47535C: mov      eax, dword ptr [eax + 0xc4]
0x475362: push     eax
0x475363: mov      eax, dword ptr [ebp - 4]
0x475366: mov      eax, dword ptr [eax + 0xc8]
0x47536C: push     eax
0x47536D: mov      eax, dword ptr [ebp - 4]
0x475370: mov      eax, dword ptr [eax + 0xcc]
0x475376: push     eax
0x475377: push     3
0x475379: mov      eax, dword ptr [ebp - 4]
0x47537C: movzx    ecx, word ptr [eax + 8]
0x475380: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x475385: mov      eax, dword ptr [eax]
0x475387: mov      edx, dword ptr [ebp - 4]
0x47538A: call     0x4723a8
0x47538F: mov      dword ptr [ebp - 0x10], eax
0x475392: cmp      dword ptr [ebp - 0x10], 0
0x475396: je       0x4753d7
0x475398: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47539D: mov      eax, dword ptr [eax]
0x47539F: mov      eax, dword ptr [eax + 0x358]
0x4753A5: mov      edx, dword ptr [ebp - 0x10]
0x4753A8: mov      dword ptr [edx + 0x14], eax
0x4753AB: mov      eax, dword ptr [ebp - 4]
0x4753AE: mov      al, byte ptr [eax + 0xe]
0x4753B1: sub      al, 0xf
0x4753B3: je       0x4753bb
0x4753B5: sub      al, 7
0x4753B7: je       0x4753ca
0x4753B9: jmp      0x4753d7
0x4753BB: mov      eax, dword ptr [ebp - 0x10]
0x4753BE: mov      dword ptr [eax + 0xc0], 0x1bf
0x4753C8: jmp      0x4753d7
0x4753CA: mov      eax, dword ptr [ebp - 0x10]
0x4753CD: mov      dword ptr [eax + 0xc0], 0xb97
0x4753D7: mov      eax, dword ptr [ebp - 4]
0x4753DA: xor      edx, edx
0x4753DC: mov      dword ptr [eax + 0x1ec], edx
0x4753E2: call     0x406154
0x4753E7: mov      edx, dword ptr [ebp - 4]
0x4753EA: mov      dword ptr [edx + 0x1f0], eax
0x4753F0: jmp      0x47545e
0x4753F2: call     0x406154
0x4753F7: mov      edx, dword ptr [ebp - 4]
0x4753FA: sub      eax, dword ptr [edx + 0x1fc]
0x475400: cmp      eax, 0xc8
0x475405: jbe      0x47545e
0x475407: call     0x406154
0x47540C: mov      edx, dword ptr [ebp - 4]
0x47540F: sub      eax, dword ptr [edx + 0x1f0]
0x475415: cmp      eax, 0x1f4
0x47541A: jbe      0x475452
0x47541C: call     0x406154
0x475421: mov      edx, dword ptr [ebp - 4]
0x475424: mov      dword ptr [edx + 0x1f0], eax
0x47542A: mov      eax, dword ptr [ebp - 4]
0x47542D: inc      dword ptr [eax + 0x1ec]
0x475433: mov      eax, dword ptr [ebp - 4]
0x475436: mov      eax, dword ptr [eax + 0x1ec]
0x47543C: mov      edx, dword ptr [ebp - 4]
0x47543F: cmp      eax, dword ptr [edx + 0x1f4]
0x475445: jl       0x475452
0x475447: mov      eax, dword ptr [ebp - 4]
0x47544A: xor      edx, edx
0x47544C: mov      dword ptr [eax + 0x1ec], edx
0x475452: mov      eax, dword ptr [ebp - 4]
0x475455: mov      bx, 0xfffe
0x475459: call     0x403080
0x47545E: mov      eax, dword ptr [ebp - 8]
0x475461: mov      edx, dword ptr [ebp - 4]
0x475464: cmp      eax, dword ptr [edx + 0x1b8]
0x47546A: je       0x475486
0x47546C: call     0x406154
0x475471: mov      edx, dword ptr [ebp - 4]
0x475474: mov      dword ptr [edx + 0x204], eax
0x47547A: mov      eax, dword ptr [ebp - 4]
0x47547D: mov      bx, 0xfff3
0x475481: call     0x403080
0x475486: pop      ebx
0x475487: mov      esp, ebp
0x475489: pop      ebp
0x47548A: ret      
0x47548B: add      byte ptr [eax], al
0x47548D: add      byte ptr [eax + 0x40], al
0x475490: push     ebp
0x475491: mov      ebp, esp
0x475493: add      esp, -0x10
0x475496: mov      dword ptr [ebp - 8], edx
0x475499: mov      dword ptr [ebp - 4], eax
0x47549C: mov      eax, dword ptr [ebp - 4]
0x47549F: cmp      dword ptr [eax + 0x148], 0xe
0x4754A6: jne      0x475534
0x4754AC: cmp      dword ptr [ebp - 8], 5
0x4754B0: jne      0x475528
0x4754B2: mov      eax, dword ptr [ebp - 4]
0x4754B5: mov      eax, dword ptr [eax + 0x250]
0x4754BB: cmp      eax, dword ptr [ebp - 8]
0x4754BE: je       0x475528
0x4754C0: push     5
0x4754C2: mov      eax, dword ptr [ebp - 4]
0x4754C5: movzx    eax, word ptr [eax + 8]
0x4754C9: push     eax
0x4754CA: mov      eax, dword ptr [ebp - 4]
0x4754CD: movzx    eax, word ptr [eax + 0xa]
0x4754D1: push     eax
0x4754D2: mov      eax, dword ptr [ebp - 4]
0x4754D5: movzx    eax, byte ptr [eax + 0xc]
0x4754D9: mov      ecx, eax
0x4754DB: add      ecx, ecx
0x4754DD: lea      ecx, [ecx + ecx*4]
0x4754E0: add      ecx, 0x334
0x4754E6: inc      ecx
0x4754E7: mov      dl, 1
0x4754E9: mov      eax, dword ptr [0x49c270]
0x4754EE: call     0x49d9f4
0x4754F3: mov      dword ptr [ebp - 0xc], eax
0x4754F6: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4754FB: mov      eax, dword ptr [eax]
0x4754FD: mov      eax, dword ptr [eax + 0x3b8]
0x475503: mov      edx, dword ptr [ebp - 0xc]
0x475506: mov      dword ptr [edx + 0x14], eax
0x475509: mov      eax, dword ptr [ebp - 0xc]
0x47550C: mov      dword ptr [eax + 0x74], 0x64
0x475513: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x475518: mov      eax, dword ptr [eax]
0x47551A: mov      eax, dword ptr [eax + 0x5a860]
0x475520: mov      edx, dword ptr [ebp - 0xc]
0x475523: call     0x40f588
0x475528: mov      eax, dword ptr [ebp - 8]
0x47552B: mov      edx, dword ptr [ebp - 4]
0x47552E: mov      dword ptr [edx + 0x250], eax
0x475534: mov      esp, ebp
0x475536: pop      ebp
0x475537: ret      

; === TCatMon.DMT_-14 @ 0x475538 ===
0x475538: push     ebp
0x475539: mov      ebp, esp
0x47553B: add      esp, -0x18
0x47553E: push     ebx
0x47553F: mov      dword ptr [ebp - 0xc], ecx
0x475542: mov      dword ptr [ebp - 8], edx
0x475545: mov      dword ptr [ebp - 4], eax
0x475548: mov      eax, dword ptr [ebp - 4]
0x47554B: mov      al, byte ptr [eax + 0xc]
0x47554E: sub      al, 8
0x475550: jae      0x4755e4
0x475556: call     0x406154
0x47555B: mov      edx, dword ptr [ebp - 4]
0x47555E: sub      eax, dword ptr [edx + 0x204]
0x475564: cmp      eax, 0xea60
0x475569: jbe      0x475585
0x47556B: call     0x406154
0x475570: mov      edx, dword ptr [ebp - 4]
0x475573: mov      dword ptr [edx + 0x204], eax
0x475579: mov      eax, dword ptr [ebp - 4]
0x47557C: mov      bx, 0xfff3
0x475580: call     0x403080
0x475585: mov      eax, dword ptr [ebp - 4]
0x475588: call     0x498170
0x47558D: mov      byte ptr [ebp - 0xd], al
0x475590: mov      eax, dword ptr [ebp - 4]
0x475593: cmp      dword ptr [eax + 0x140], 0
0x47559A: je       0x4755e4
0x47559C: mov      eax, dword ptr [ebp - 0xc]
0x47559F: mov      edx, dword ptr [ebp - 4]
0x4755A2: add      eax, dword ptr [edx + 0xa0]
0x4755A8: mov      edx, dword ptr [ebp - 4]
0x4755AB: add      eax, dword ptr [edx + 0x98]
0x4755B1: push     eax
0x4755B2: mov      eax, dword ptr [ebp + 0xc]
0x4755B5: mov      edx, dword ptr [ebp - 4]
0x4755B8: add      eax, dword ptr [edx + 0xac]
0x4755BE: mov      edx, dword ptr [ebp - 4]
0x4755C1: add      eax, dword ptr [edx + 0x9c]
0x4755C7: push     eax
0x4755C8: mov      al, byte ptr [ebp + 8]
0x4755CB: push     eax
0x4755CC: mov      al, byte ptr [ebp - 0xd]
0x4755CF: push     eax
0x4755D0: mov      eax, dword ptr [ebp - 4]
0x4755D3: mov      ecx, dword ptr [eax + 0x140]
0x4755D9: mov      edx, dword ptr [ebp - 8]
0x4755DC: mov      eax, dword ptr [ebp - 4]
0x4755DF: call     0x497fd0
0x4755E4: pop      ebx
0x4755E5: mov      esp, ebp
0x4755E7: pop      ebp
0x4755E8: ret      8
0x4755EB: nop      
0x4755EC: push     ebp
0x4755ED: mov      ebp, esp
0x4755EF: add      esp, -0x18
0x4755F2: push     ebx
0x4755F3: mov      dword ptr [ebp - 4], eax
0x4755F6: mov      eax, dword ptr [ebp - 4]
0x4755F9: cmp      dword ptr [eax + 0x148], 0xb
0x475600: je       0x4758b9
0x475606: mov      eax, dword ptr [ebp - 4]
0x475609: cmp      dword ptr [eax + 0x148], 9
0x475610: je       0x4758b9
0x475616: mov      eax, dword ptr [ebp - 4]
0x475619: cmp      dword ptr [eax + 0x148], 0xd
0x475620: je       0x4758b9
0x475626: mov      eax, dword ptr [ebp - 4]
0x475629: mov      byte ptr [eax + 0x1e4], 0
0x475630: mov      eax, dword ptr [ebp - 4]
0x475633: mov      eax, dword ptr [eax + 0x220]
0x475639: cmp      dword ptr [eax + 8], 2
0x47563D: jl       0x475649
0x47563F: mov      eax, dword ptr [ebp - 4]
0x475642: mov      byte ptr [eax + 0x1e4], 1
0x475649: mov      eax, dword ptr [ebp - 4]
0x47564C: mov      edx, dword ptr [eax + 0x1b8]
0x475652: mov      eax, dword ptr [ebp - 4]
0x475655: sub      edx, dword ptr [eax + 0x1b0]
0x47565B: mov      eax, dword ptr [ebp - 4]
0x47565E: mov      bx, 0xfff8
0x475662: call     0x403080
0x475667: mov      eax, dword ptr [ebp - 4]
0x47566A: mov      edx, dword ptr [eax + 0x1b8]
0x475670: mov      eax, dword ptr [ebp - 4]
0x475673: sub      edx, dword ptr [eax + 0x1b0]
0x475679: mov      eax, dword ptr [ebp - 4]
0x47567C: mov      bx, 0xfff7
0x475680: call     0x403080
0x475685: mov      eax, dword ptr [ebp - 4]
0x475688: mov      eax, dword ptr [eax + 0x1b8]
0x47568E: mov      dword ptr [ebp - 8], eax
0x475691: mov      eax, dword ptr [ebp - 4]
0x475694: cmp      dword ptr [eax + 0x148], 0
0x47569B: je       0x475825
0x4756A1: mov      eax, dword ptr [ebp - 4]
0x4756A4: mov      eax, dword ptr [eax + 0x1b8]
0x4756AA: mov      edx, dword ptr [ebp - 4]
0x4756AD: cmp      eax, dword ptr [edx + 0x1b0]
0x4756B3: jl       0x4756c9
0x4756B5: mov      eax, dword ptr [ebp - 4]
0x4756B8: mov      eax, dword ptr [eax + 0x1b8]
0x4756BE: mov      edx, dword ptr [ebp - 4]
0x4756C1: cmp      eax, dword ptr [edx + 0x1b4]
0x4756C7: jle      0x4756db
0x4756C9: mov      eax, dword ptr [ebp - 4]
0x4756CC: mov      eax, dword ptr [eax + 0x1b0]
0x4756D2: mov      edx, dword ptr [ebp - 4]
0x4756D5: mov      dword ptr [edx + 0x1b8], eax
0x4756DB: mov      eax, dword ptr [ebp - 4]
0x4756DE: cmp      byte ptr [eax + 0x1e4], 0
0x4756E5: je       0x47570d
0x4756E7: mov      eax, dword ptr [ebp - 4]
0x4756EA: mov      eax, dword ptr [eax + 0x1d0]
0x4756F0: add      eax, eax
0x4756F2: mov      dword ptr [ebp - 0x18], eax
0x4756F5: xor      eax, eax
0x4756F7: mov      dword ptr [ebp - 0x14], eax
0x4756FA: fild     qword ptr [ebp - 0x18]
0x4756FD: fdiv     dword ptr [0x4758c0]
0x475703: call     0x4029a0
0x475708: mov      dword ptr [ebp - 0xc], eax
0x47570B: jmp      0x475719
0x47570D: mov      eax, dword ptr [ebp - 4]
0x475710: mov      eax, dword ptr [eax + 0x1d0]
0x475716: mov      dword ptr [ebp - 0xc], eax
0x475719: call     0x406154
0x47571E: mov      edx, dword ptr [ebp - 4]
0x475721: sub      eax, dword ptr [edx + 0x1d4]
0x475727: cmp      eax, dword ptr [ebp - 0xc]
0x47572A: jbe      0x47580a
0x475730: mov      eax, dword ptr [ebp - 4]
0x475733: mov      eax, dword ptr [eax + 0x1b8]
0x475739: mov      edx, dword ptr [ebp - 4]
0x47573C: cmp      eax, dword ptr [edx + 0x1b4]
0x475742: jge      0x47575d
0x475744: mov      eax, dword ptr [ebp - 4]
0x475747: inc      dword ptr [eax + 0x1b8]
0x47574D: call     0x406154
0x475752: mov      edx, dword ptr [ebp - 4]
0x475755: mov      dword ptr [edx + 0x1d4], eax
0x47575B: jmp      0x475772
0x47575D: mov      eax, dword ptr [ebp - 4]
0x475760: xor      edx, edx
0x475762: mov      dword ptr [eax + 0x148], edx
0x475768: mov      eax, dword ptr [ebp - 4]
0x47576B: mov      byte ptr [eax + 0xee], 0
0x475772: mov      eax, dword ptr [ebp - 4]
0x475775: cmp      dword ptr [eax + 0x148], 0x16
0x47577C: jne      0x47580a
0x475782: mov      eax, dword ptr [ebp - 4]
0x475785: mov      eax, dword ptr [eax + 0x1b8]
0x47578B: mov      edx, dword ptr [ebp - 4]
0x47578E: sub      eax, dword ptr [edx + 0x1b0]
0x475794: cmp      eax, 4
0x475797: jne      0x47580a
0x475799: mov      eax, dword ptr [ebp - 4]
0x47579C: movzx    eax, word ptr [eax + 0xa]
0x4757A0: push     eax
0x4757A1: mov      eax, dword ptr [ebp - 4]
0x4757A4: mov      eax, dword ptr [eax + 0xc4]
0x4757AA: push     eax
0x4757AB: mov      eax, dword ptr [ebp - 4]
0x4757AE: mov      eax, dword ptr [eax + 0xc8]
0x4757B4: push     eax
0x4757B5: mov      eax, dword ptr [ebp - 4]
0x4757B8: mov      eax, dword ptr [eax + 0xcc]
0x4757BE: push     eax
0x4757BF: push     0xb
0x4757C1: mov      eax, dword ptr [ebp - 4]
0x4757C4: movzx    ecx, word ptr [eax + 8]
0x4757C8: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x4757CD: mov      eax, dword ptr [eax]
0x4757CF: mov      edx, dword ptr [ebp - 4]
0x4757D2: call     0x4723a8
0x4757D7: mov      dword ptr [ebp - 0x10], eax
0x4757DA: cmp      dword ptr [ebp - 0x10], 0
0x4757DE: je       0x47580a
0x4757E0: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4757E5: mov      eax, dword ptr [eax]
0x4757E7: mov      eax, dword ptr [eax + 0x370]
0x4757ED: mov      edx, dword ptr [ebp - 0x10]
0x4757F0: mov      dword ptr [edx + 0x14], eax
0x4757F3: mov      eax, dword ptr [ebp - 0x10]
0x4757F6: mov      dword ptr [eax + 0x74], 0x1e
0x4757FD: mov      eax, dword ptr [ebp - 0x10]
0x475800: mov      dword ptr [eax + 0xc0], 0x110
0x47580A: mov      eax, dword ptr [ebp - 4]
0x47580D: xor      edx, edx
0x47580F: mov      dword ptr [eax + 0x1ec], edx
0x475815: call     0x406154
0x47581A: mov      edx, dword ptr [ebp - 4]
0x47581D: mov      dword ptr [edx + 0x1f0], eax
0x475823: jmp      0x475891
0x475825: call     0x406154
0x47582A: mov      edx, dword ptr [ebp - 4]
0x47582D: sub      eax, dword ptr [edx + 0x1fc]
0x475833: cmp      eax, 0xc8
0x475838: jbe      0x475891
0x47583A: call     0x406154
0x47583F: mov      edx, dword ptr [ebp - 4]
0x475842: sub      eax, dword ptr [edx + 0x1f0]
0x475848: cmp      eax, 0x1f4
0x47584D: jbe      0x475885
0x47584F: call     0x406154
0x475854: mov      edx, dword ptr [ebp - 4]
0x475857: mov      dword ptr [edx + 0x1f0], eax
0x47585D: mov      eax, dword ptr [ebp - 4]
0x475860: inc      dword ptr [eax + 0x1ec]
0x475866: mov      eax, dword ptr [ebp - 4]
0x475869: mov      eax, dword ptr [eax + 0x1ec]
0x47586F: mov      edx, dword ptr [ebp - 4]
0x475872: cmp      eax, dword ptr [edx + 0x1f4]
0x475878: jl       0x475885
0x47587A: mov      eax, dword ptr [ebp - 4]
0x47587D: xor      edx, edx
0x47587F: mov      dword ptr [eax + 0x1ec], edx
0x475885: mov      eax, dword ptr [ebp - 4]
0x475888: mov      bx, 0xfffe
0x47588C: call     0x403080
0x475891: mov      eax, dword ptr [ebp - 8]
0x475894: mov      edx, dword ptr [ebp - 4]
0x475897: cmp      eax, dword ptr [edx + 0x1b8]
0x47589D: je       0x4758b9
0x47589F: call     0x406154
0x4758A4: mov      edx, dword ptr [ebp - 4]
0x4758A7: mov      dword ptr [edx + 0x204], eax
0x4758AD: mov      eax, dword ptr [ebp - 4]
0x4758B0: mov      bx, 0xfff3
0x4758B4: call     0x403080
0x4758B9: pop      ebx
0x4758BA: mov      esp, ebp
0x4758BC: pop      ebp
0x4758BD: ret      
0x4758BE: add      byte ptr [eax], al
0x4758C0: add      byte ptr [eax], al
0x4758C2: inc      eax
0x4758C3: inc      eax
0x4758C4: push     ebp
0x4758C5: mov      ebp, esp
0x4758C7: add      esp, -0xc
0x4758CA: mov      dword ptr [ebp - 8], edx
0x4758CD: mov      dword ptr [ebp - 4], eax
0x4758D0: mov      eax, dword ptr [ebp - 4]
0x4758D3: cmp      dword ptr [eax + 0x148], 0x14
0x4758DA: jne      0x475928
0x4758DC: cmp      dword ptr [ebp - 8], 6
0x4758E0: jne      0x475928
0x4758E2: mov      eax, dword ptr [ebp - 4]
0x4758E5: movzx    eax, word ptr [eax + 8]
0x4758E9: push     eax
0x4758EA: mov      eax, dword ptr [ebp - 4]
0x4758ED: movzx    eax, word ptr [eax + 0xa]
0x4758F1: push     eax
0x4758F2: push     1
0x4758F4: mov      eax, dword ptr [ebp - 4]
0x4758F7: mov      ecx, dword ptr [eax + 0xd8]
0x4758FD: mov      dl, 1
0x4758FF: mov      eax, dword ptr [0x4772f0]
0x475904: call     0x4773b0
0x475909: mov      dword ptr [ebp - 0xc], eax
0x47590C: mov      eax, dword ptr [ebp - 4]
0x47590F: movzx    eax, byte ptr [eax + 0xc]
0x475913: mov      edx, dword ptr [ebp - 0xc]
0x475916: mov      dword ptr [edx + 0xc], eax
0x475919: mov      eax, dword ptr [0x4a40a8] ; *0x4A40A8->0x4F7DCC
0x47591E: mov      eax, dword ptr [eax]
0x475920: mov      edx, dword ptr [ebp - 0xc]
0x475923: call     0x4777f0
0x475928: mov      esp, ebp
0x47592A: pop      ebp
0x47592B: ret      

; === THuSuABi.DMT_-13 @ 0x47592C ===
0x47592C: push     ebp
0x47592D: mov      ebp, esp
0x47592F: push     ecx
0x475930: mov      dword ptr [ebp - 4], eax
0x475933: mov      eax, dword ptr [ebp - 4]
0x475936: call     0x474d54 ; TSkeletonOma.DMT_-13
0x47593B: mov      eax, dword ptr [ebp - 4]
0x47593E: cmp      byte ptr [eax + 0xee], 0
0x475945: je       0x47598c
0x475947: mov      eax, dword ptr [ebp - 4]
0x47594A: add      eax, 0x24c
0x47594F: push     eax
0x475950: mov      eax, dword ptr [ebp - 4]
0x475953: lea      ecx, [eax + 0x248]
0x475959: mov      eax, dword ptr [ebp - 4]
0x47595C: mov      edx, dword ptr [eax + 0x1b8]
0x475962: add      edx, 0xb2c
0x475968: mov      eax, dword ptr [ebp - 4]
0x47596B: sub      edx, dword ptr [eax + 0x1b0]
0x475971: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x475976: mov      eax, dword ptr [eax]
0x475978: mov      eax, dword ptr [eax + 0x358]
0x47597E: call     0x453a48
0x475983: mov      edx, dword ptr [ebp - 4]
0x475986: mov      dword ptr [edx + 0x244], eax
0x47598C: pop      ecx
0x47598D: pop      ebp
0x47598E: ret      
0x47598F: nop      
0x475990: push     ebp
0x475991: mov      ebp, esp
0x475993: add      esp, -8
0x475996: test     dl, dl
0x475998: je       0x4759a2
0x47599A: add      esp, -0x10
0x47599D: call     0x4031a4
0x4759A2: mov      byte ptr [ebp - 5], dl
0x4759A5: mov      dword ptr [ebp - 4], eax
0x4759A8: xor      edx, edx
0x4759AA: mov      eax, dword ptr [ebp - 4]
0x4759AD: call     0x4962d0 ; TActor.DMT_-4
0x4759B2: mov      eax, dword ptr [ebp - 4]
0x4759B5: xor      edx, edx
0x4759B7: mov      dword ptr [eax + 0x244], edx
0x4759BD: mov      eax, dword ptr [ebp - 4]
0x4759C0: xor      edx, edx
0x4759C2: mov      dword ptr [eax + 0x248], edx
0x4759C8: mov      eax, dword ptr [ebp - 4]
0x4759CB: mov      byte ptr [eax + 0xee], 0
0x4759D2: mov      eax, dword ptr [ebp - 4]
0x4759D5: mov      byte ptr [eax + 0x24c], 0
0x4759DC: mov      eax, dword ptr [ebp - 4]
0x4759DF: cmp      byte ptr [ebp - 5], 0
0x4759E3: je       0x4759f4
0x4759E5: call     0x4031fc
0x4759EA: pop      dword ptr fs:[0]
0x4759F1: add      esp, 0xc
0x4759F4: mov      eax, dword ptr [ebp - 4]
0x4759F7: pop      ecx
0x4759F8: pop      ecx
0x4759F9: pop      ebp
0x4759FA: ret      
0x4759FB: nop      
0x4759FC: push     ebp
0x4759FD: mov      ebp, esp
0x4759FF: add      esp, -0x24
0x475A02: mov      dword ptr [ebp - 4], eax
0x475A05: mov      eax, dword ptr [ebp - 4]
0x475A08: mov      dword ptr [eax + 0x1b8], 0xffffffff
0x475A12: mov      eax, dword ptr [ebp - 4]
0x475A15: movzx    eax, word ptr [eax + 0x14]
0x475A19: call     0x496120
0x475A1E: mov      edx, dword ptr [ebp - 4]
0x475A21: mov      dword ptr [edx + 0xe0], eax
0x475A27: mov      eax, dword ptr [ebp - 4]
0x475A2A: movzx    edx, word ptr [eax + 0x14]
0x475A2E: mov      eax, dword ptr [ebp - 4]
0x475A31: movzx    eax, byte ptr [eax + 0xe]
0x475A35: call     0x495ca8
0x475A3A: mov      dword ptr [ebp - 8], eax
0x475A3D: cmp      dword ptr [ebp - 8], 0
0x475A41: je       0x475f8a
0x475A47: mov      eax, dword ptr [ebp - 4]
0x475A4A: mov      eax, dword ptr [eax + 0x148]
0x475A50: add      eax, -0xa
0x475A53: cmp      eax, 0x18
0x475A56: ja       0x475f8a
0x475A5C: mov      al, byte ptr [eax + 0x475a69]
0x475A62: jmp      dword ptr [eax*4 + 0x475a82]
0x475A69: add      dword ptr [edx], eax
0x475A6B: add      byte ptr [eax], al
0x475A6D: add      eax, dword ptr [eax]
0x475A6F: add      byte ptr [eax], al
0x475A71: add      byte ptr [eax], al
0x475A73: add      byte ptr [eax], al
0x475A75: add      byte ptr [ebx], al
0x475A77: add      byte ptr [eax], al
0x475A79: add      byte ptr [eax], al
0x475A7B: add      byte ptr [eax], al
0x475A7D: add      byte ptr [eax + 0x5f8a0607], al
0x475A84: inc      edi
0x475A85: add      byte ptr [edx + 0x2f00475a], ah
0x475A8B: pop      ebx
0x475A8C: inc      edi
0x475A8D: add      byte ptr [esi], dh
0x475A8F: pop      esp
0x475A90: inc      edi
0x475A91: add      dh, ah
0x475A93: pop      ebp
0x475A94: inc      edi
0x475A95: add      byte ptr [eax + 0x5e], dl
0x475A98: inc      edi
0x475A99: add      dl, cl
0x475A9B: pop      esi
0x475A9C: inc      edi
0x475A9D: add      byte ptr [edx + 0x5f], al
0x475AA0: inc      edi
0x475AA1: add      byte ptr [ebx - 0x48f007bb], cl
0x475AA7: inc      eax
0x475AA8: add      cl, byte ptr [ebx - 0x48f007ab]
0x475AAE: push     edx
0x475AAF: add      al, 3
0x475AB1: ret      0x558b
0x475AB4: cld      
0x475AB5: movzx    edx, byte ptr [edx + 0xc]
0x475AB9: imul     edx
0x475ABB: mov      edx, dword ptr [ebp - 8]
0x475ABE: movzx    edx, word ptr [edx]
0x475AC1: add      eax, edx
0x475AC3: mov      edx, dword ptr [ebp - 4]
0x475AC6: mov      dword ptr [edx + 0x1b0], eax
0x475ACC: mov      eax, dword ptr [ebp - 8]
0x475ACF: movzx    eax, word ptr [eax + 2]
0x475AD3: mov      edx, dword ptr [ebp - 4]
0x475AD6: add      eax, dword ptr [edx + 0x1b0]
0x475ADC: dec      eax
0x475ADD: mov      edx, dword ptr [ebp - 4]
0x475AE0: mov      dword ptr [edx + 0x1b4], eax
0x475AE6: mov      eax, dword ptr [ebp - 8]
0x475AE9: movzx    eax, word ptr [eax + 6]
0x475AED: mov      edx, dword ptr [ebp - 4]
0x475AF0: mov      dword ptr [edx + 0x1d0], eax
0x475AF6: call     0x406154
0x475AFB: mov      edx, dword ptr [ebp - 4]
0x475AFE: mov      dword ptr [edx + 0x1d4], eax
0x475B04: mov      eax, dword ptr [ebp - 8]
0x475B07: movzx    eax, word ptr [eax + 2]
0x475B0B: mov      edx, dword ptr [ebp - 4]
0x475B0E: mov      dword ptr [edx + 0x1f4], eax
0x475B14: push     0
0x475B16: push     1
0x475B18: mov      eax, dword ptr [ebp - 4]
0x475B1B: xor      edx, edx
0x475B1D: mov      dl, byte ptr [eax + 0xc]
0x475B20: xor      ecx, ecx
0x475B22: mov      eax, dword ptr [ebp - 4]
0x475B25: call     0x497610
0x475B2A: jmp      0x475f8a
0x475B2F: mov      eax, dword ptr [ebp - 8]
0x475B32: movzx    eax, word ptr [eax + 0xc]
0x475B36: mov      edx, dword ptr [ebp - 8]
0x475B39: movzx    edx, word ptr [edx + 0xe]
0x475B3D: add      eax, edx
0x475B3F: mov      edx, dword ptr [ebp - 4]
0x475B42: movzx    edx, byte ptr [edx + 0xc]
0x475B46: imul     edx
0x475B48: mov      edx, dword ptr [ebp - 8]
0x475B4B: movzx    edx, word ptr [edx + 0xa]
0x475B4F: add      eax, edx
0x475B51: mov      edx, dword ptr [ebp - 4]
0x475B54: mov      dword ptr [edx + 0x1b0], eax
0x475B5A: mov      eax, dword ptr [ebp - 8]
0x475B5D: movzx    eax, word ptr [eax + 0xc]
0x475B61: mov      edx, dword ptr [ebp - 4]
0x475B64: add      eax, dword ptr [edx + 0x1b0]
0x475B6A: dec      eax
0x475B6B: mov      edx, dword ptr [ebp - 4]
0x475B6E: mov      dword ptr [edx + 0x1b4], eax
0x475B74: mov      eax, dword ptr [ebp - 4]
0x475B77: mov      eax, dword ptr [eax + 0x160]
0x475B7D: mov      edx, dword ptr [ebp - 4]
0x475B80: mov      dword ptr [edx + 0x1d0], eax
0x475B86: call     0x406154
0x475B8B: mov      edx, dword ptr [ebp - 4]
0x475B8E: mov      dword ptr [edx + 0x1d4], eax
0x475B94: mov      eax, dword ptr [ebp - 8]
0x475B97: movzx    eax, byte ptr [eax + 0x12]
0x475B9B: mov      edx, dword ptr [ebp - 4]
0x475B9E: mov      dword ptr [edx + 0x1d8], eax
0x475BA4: mov      eax, dword ptr [ebp - 4]
0x475BA7: xor      edx, edx
0x475BA9: mov      dword ptr [eax + 0x1dc], edx
0x475BAF: mov      eax, dword ptr [ebp - 4]
0x475BB2: mov      dword ptr [eax + 0x1e0], 1
0x475BBC: mov      eax, dword ptr [ebp - 4]
0x475BBF: cmp      dword ptr [eax + 0x148], 0xb
0x475BC6: jne      0x475bfc
0x475BC8: push     0
0x475BCA: mov      eax, dword ptr [ebp - 4]
0x475BCD: mov      eax, dword ptr [eax + 0x1b4]
0x475BD3: mov      edx, dword ptr [ebp - 4]
0x475BD6: sub      eax, dword ptr [edx + 0x1b0]
0x475BDC: inc      eax
0x475BDD: push     eax
0x475BDE: mov      eax, dword ptr [ebp - 4]
0x475BE1: xor      edx, edx
0x475BE3: mov      dl, byte ptr [eax + 0xc]
0x475BE6: mov      eax, dword ptr [ebp - 4]
0x475BE9: mov      ecx, dword ptr [eax + 0x1e0]
0x475BEF: mov      eax, dword ptr [ebp - 4]
0x475BF2: call     0x497610
0x475BF7: jmp      0x475f8a
0x475BFC: push     0
0x475BFE: mov      eax, dword ptr [ebp - 4]
0x475C01: mov      eax, dword ptr [eax + 0x1b4]
0x475C07: mov      edx, dword ptr [ebp - 4]
0x475C0A: sub      eax, dword ptr [edx + 0x1b0]
0x475C10: inc      eax
0x475C11: push     eax
0x475C12: mov      eax, dword ptr [ebp - 4]
0x475C15: movzx    eax, byte ptr [eax + 0xc]
0x475C19: call     0x4a023c
0x475C1E: mov      edx, eax
0x475C20: mov      eax, dword ptr [ebp - 4]
0x475C23: mov      ecx, dword ptr [eax + 0x1e0]
0x475C29: mov      eax, dword ptr [ebp - 4]
0x475C2C: call     0x497610
0x475C31: jmp      0x475f8a
0x475C36: mov      eax, dword ptr [ebp - 8]
0x475C39: movzx    eax, word ptr [eax + 0x16]
0x475C3D: mov      edx, dword ptr [ebp - 8]
0x475C40: movzx    edx, word ptr [edx + 0x18]
0x475C44: add      eax, edx
0x475C46: mov      edx, dword ptr [ebp - 4]
0x475C49: movzx    edx, byte ptr [edx + 0xc]
0x475C4D: imul     edx
0x475C4F: mov      edx, dword ptr [ebp - 8]
0x475C52: movzx    edx, word ptr [edx + 0x14]
0x475C56: add      eax, edx
0x475C58: mov      edx, dword ptr [ebp - 4]
0x475C5B: mov      dword ptr [edx + 0x1b0], eax
0x475C61: mov      eax, dword ptr [ebp - 8]
0x475C64: movzx    eax, word ptr [eax + 0x16]
0x475C68: mov      edx, dword ptr [ebp - 4]
0x475C6B: add      eax, dword ptr [edx + 0x1b0]
0x475C71: dec      eax
0x475C72: mov      edx, dword ptr [ebp - 4]
0x475C75: mov      dword ptr [edx + 0x1b4], eax
0x475C7B: mov      eax, dword ptr [ebp - 8]
0x475C7E: movzx    eax, word ptr [eax + 0x1a]
0x475C82: mov      edx, dword ptr [ebp - 4]
0x475C85: mov      dword ptr [edx + 0x1d0], eax
0x475C8B: call     0x406154
0x475C90: mov      edx, dword ptr [ebp - 4]
0x475C93: mov      dword ptr [edx + 0x1d4], eax
0x475C99: call     0x406154
0x475C9E: mov      edx, dword ptr [ebp - 4]
0x475CA1: mov      dword ptr [edx + 0x150], eax
0x475CA7: push     0
0x475CA9: push     1
0x475CAB: mov      eax, dword ptr [ebp - 4]
0x475CAE: xor      edx, edx
0x475CB0: mov      dl, byte ptr [eax + 0xc]
0x475CB3: xor      ecx, ecx
0x475CB5: mov      eax, dword ptr [ebp - 4]
0x475CB8: call     0x497610
0x475CBD: mov      eax, dword ptr [ebp - 4]
0x475CC0: mov      byte ptr [eax + 0xee], 1
0x475CC7: mov      eax, dword ptr [ebp - 4]
0x475CCA: movzx    eax, byte ptr [eax + 0xc]
0x475CCE: mov      edx, dword ptr [ebp - 4]
0x475CD1: mov      dword ptr [edx + 0x250], eax
0x475CD7: mov      eax, dword ptr [ebp - 4]
0x475CDA: mov      eax, dword ptr [eax + 0x1b0]
0x475CE0: mov      edx, dword ptr [ebp - 4]
0x475CE3: mov      dword ptr [edx + 0x1c0], eax
0x475CE9: mov      eax, dword ptr [ebp - 4]
0x475CEC: mov      eax, dword ptr [eax + 0x1b0]
0x475CF2: mov      edx, dword ptr [ebp - 4]
0x475CF5: mov      dword ptr [edx + 0x1bc], eax
0x475CFB: mov      eax, dword ptr [ebp - 4]
0x475CFE: cmp      byte ptr [eax + 0xe], 0x14
0x475D02: jne      0x475d19
0x475D04: mov      eax, dword ptr [ebp - 4]
0x475D07: mov      eax, dword ptr [eax + 0x1b4]
0x475D0D: inc      eax
0x475D0E: mov      edx, dword ptr [ebp - 4]
0x475D11: mov      dword ptr [edx + 0x1c4], eax
0x475D17: jmp      0x475d2b
0x475D19: mov      eax, dword ptr [ebp - 4]
0x475D1C: mov      eax, dword ptr [eax + 0x1b4]
0x475D22: mov      edx, dword ptr [ebp - 4]
0x475D25: mov      dword ptr [edx + 0x1c4], eax
0x475D2B: call     0x406154
0x475D30: mov      edx, dword ptr [ebp - 4]
0x475D33: mov      dword ptr [edx + 0x1c8], eax
0x475D39: mov      eax, dword ptr [ebp - 4]
0x475D3C: mov      eax, dword ptr [eax + 0x1d0]
0x475D42: mov      edx, dword ptr [ebp - 4]
0x475D45: mov      dword ptr [edx + 0x1cc], eax
0x475D4B: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x475D50: mov      eax, dword ptr [eax]
0x475D52: mov      edx, dword ptr [ebp - 4]
0x475D55: mov      edx, dword ptr [edx + 0xcc]
0x475D5B: call     0x472db0
0x475D60: mov      dword ptr [ebp - 0xc], eax
0x475D63: cmp      dword ptr [ebp - 0xc], 0
0x475D67: je       0x475dcd
0x475D69: lea      eax, [ebp - 0x10]
0x475D6C: push     eax
0x475D6D: lea      eax, [ebp - 0x14]
0x475D70: push     eax
0x475D71: mov      eax, dword ptr [ebp - 4]
0x475D74: movzx    ecx, word ptr [eax + 0xa]
0x475D78: mov      eax, dword ptr [ebp - 4]
0x475D7B: movzx    edx, word ptr [eax + 8]
0x475D7F: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x475D84: mov      eax, dword ptr [eax]
0x475D86: call     0x472494
0x475D8B: lea      eax, [ebp - 0x18]
0x475D8E: push     eax
0x475D8F: lea      eax, [ebp - 0x1c]
0x475D92: push     eax
0x475D93: mov      eax, dword ptr [ebp - 0xc]
0x475D96: movzx    ecx, word ptr [eax + 0xa]
0x475D9A: mov      eax, dword ptr [ebp - 0xc]
0x475D9D: movzx    edx, word ptr [eax + 8]
0x475DA1: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x475DA6: mov      eax, dword ptr [eax]
0x475DA8: call     0x472494
0x475DAD: mov      eax, dword ptr [ebp - 0x1c]
0x475DB0: push     eax
0x475DB1: mov      ecx, dword ptr [ebp - 0x18]
0x475DB4: mov      edx, dword ptr [ebp - 0x14]
0x475DB7: mov      eax, dword ptr [ebp - 0x10]
0x475DBA: call     0x4a0630
0x475DBF: mov      edx, dword ptr [ebp - 4]
0x475DC2: mov      dword ptr [edx + 0x254], eax
0x475DC8: jmp      0x475f8a
0x475DCD: mov      eax, dword ptr [ebp - 4]
0x475DD0: mov      eax, dword ptr [eax + 0x250]
0x475DD6: add      eax, eax
0x475DD8: mov      edx, dword ptr [ebp - 4]
0x475DDB: mov      dword ptr [edx + 0x254], eax
0x475DE1: jmp      0x475f8a
0x475DE6: mov      eax, dword ptr [ebp - 8]
0x475DE9: movzx    eax, word ptr [eax + 0x2a]
0x475DED: mov      edx, dword ptr [ebp - 8]
0x475DF0: movzx    edx, word ptr [edx + 0x2c]
0x475DF4: add      eax, edx
0x475DF6: mov      edx, dword ptr [ebp - 4]
0x475DF9: movzx    edx, byte ptr [edx + 0xc]
0x475DFD: imul     edx
0x475DFF: mov      edx, dword ptr [ebp - 8]
0x475E02: movzx    edx, word ptr [edx + 0x28]
0x475E06: add      eax, edx
0x475E08: mov      edx, dword ptr [ebp - 4]
0x475E0B: mov      dword ptr [edx + 0x1b0], eax
0x475E11: mov      eax, dword ptr [ebp - 8]
0x475E14: movzx    eax, word ptr [eax + 0x2a]
0x475E18: mov      edx, dword ptr [ebp - 4]
0x475E1B: add      eax, dword ptr [edx + 0x1b0]
0x475E21: dec      eax
0x475E22: mov      edx, dword ptr [ebp - 4]
0x475E25: mov      dword ptr [edx + 0x1b4], eax
0x475E2B: mov      eax, dword ptr [ebp - 4]
0x475E2E: mov      eax, dword ptr [eax + 0x1e8]
0x475E34: mov      edx, dword ptr [ebp - 4]
0x475E37: mov      dword ptr [edx + 0x1d0], eax
0x475E3D: call     0x406154
0x475E42: mov      edx, dword ptr [ebp - 4]
0x475E45: mov      dword ptr [edx + 0x1d4], eax
0x475E4B: jmp      0x475f8a
0x475E50: mov      eax, dword ptr [ebp - 8]
0x475E53: movzx    eax, word ptr [eax + 0x34]
0x475E57: mov      edx, dword ptr [ebp - 8]
0x475E5A: movzx    edx, word ptr [edx + 0x36]
0x475E5E: add      eax, edx
0x475E60: mov      edx, dword ptr [ebp - 4]
0x475E63: movzx    edx, byte ptr [edx + 0xc]
0x475E67: imul     edx
0x475E69: mov      edx, dword ptr [ebp - 8]
0x475E6C: movzx    edx, word ptr [edx + 0x32]
0x475E70: add      eax, edx
0x475E72: mov      edx, dword ptr [ebp - 4]
0x475E75: mov      dword ptr [edx + 0x1b0], eax
0x475E7B: mov      eax, dword ptr [ebp - 8]
0x475E7E: movzx    eax, word ptr [eax + 0x34]
0x475E82: mov      edx, dword ptr [ebp - 4]
0x475E85: add      eax, dword ptr [edx + 0x1b0]
0x475E8B: dec      eax
0x475E8C: mov      edx, dword ptr [ebp - 4]
0x475E8F: mov      dword ptr [edx + 0x1b4], eax
0x475E95: mov      eax, dword ptr [ebp - 4]
0x475E98: mov      eax, dword ptr [eax + 0x1b4]
0x475E9E: mov      edx, dword ptr [ebp - 4]
0x475EA1: mov      dword ptr [edx + 0x1b0], eax
0x475EA7: mov      eax, dword ptr [ebp - 8]
0x475EAA: movzx    eax, word ptr [eax + 0x38]
0x475EAE: mov      edx, dword ptr [ebp - 4]
0x475EB1: mov      dword ptr [edx + 0x1d0], eax
0x475EB7: call     0x406154
0x475EBC: mov      edx, dword ptr [ebp - 4]
0x475EBF: mov      dword ptr [edx + 0x1d4], eax
0x475EC5: jmp      0x475f8a
0x475ECA: mov      eax, dword ptr [ebp - 8]
0x475ECD: movzx    eax, word ptr [eax + 0x34]
0x475ED1: mov      edx, dword ptr [ebp - 8]
0x475ED4: movzx    edx, word ptr [edx + 0x36]
0x475ED8: add      eax, edx
0x475EDA: mov      edx, dword ptr [ebp - 4]
0x475EDD: movzx    edx, byte ptr [edx + 0xc]
0x475EE1: imul     edx
0x475EE3: mov      edx, dword ptr [ebp - 8]
0x475EE6: movzx    edx, word ptr [edx + 0x32]
0x475EEA: add      eax, edx
0x475EEC: mov      edx, dword ptr [ebp - 4]
0x475EEF: mov      dword ptr [edx + 0x1b0], eax
0x475EF5: mov      eax, dword ptr [ebp - 8]
0x475EF8: movzx    eax, word ptr [eax + 0x34]
0x475EFC: mov      edx, dword ptr [ebp - 4]
0x475EFF: add      eax, dword ptr [edx + 0x1b0]
0x475F05: dec      eax
0x475F06: mov      edx, dword ptr [ebp - 4]
0x475F09: mov      dword ptr [edx + 0x1b4], eax
0x475F0F: mov      eax, dword ptr [ebp - 8]
0x475F12: movzx    eax, word ptr [eax + 0x38]
0x475F16: mov      edx, dword ptr [ebp - 4]
0x475F19: mov      dword ptr [edx + 0x1d0], eax
0x475F1F: call     0x406154
0x475F24: mov      edx, dword ptr [ebp - 4]
0x475F27: mov      dword ptr [edx + 0x1d4], eax
0x475F2D: mov      eax, dword ptr [ebp - 4]
0x475F30: cmp      byte ptr [eax + 0xe], 0x28
0x475F34: jne      0x475f8a
0x475F36: mov      eax, dword ptr [ebp - 4]
0x475F39: mov      byte ptr [eax + 0x24c], 1
0x475F40: jmp      0x475f8a
0x475F42: mov      eax, dword ptr [ebp - 8]
0x475F45: movzx    eax, word ptr [eax + 0x3c]
0x475F49: mov      edx, dword ptr [ebp - 4]
0x475F4C: mov      dword ptr [edx + 0x1b0], eax
0x475F52: mov      eax, dword ptr [ebp - 8]
0x475F55: movzx    eax, word ptr [eax + 0x3e]
0x475F59: mov      edx, dword ptr [ebp - 4]
0x475F5C: add      eax, dword ptr [edx + 0x1b0]
0x475F62: dec      eax
0x475F63: mov      edx, dword ptr [ebp - 4]
0x475F66: mov      dword ptr [edx + 0x1b4], eax
0x475F6C: mov      eax, dword ptr [ebp - 8]
0x475F6F: movzx    eax, word ptr [eax + 0x42]
0x475F73: mov      edx, dword ptr [ebp - 4]
0x475F76: mov      dword ptr [edx + 0x1d0], eax
0x475F7C: call     0x406154
0x475F81: mov      edx, dword ptr [ebp - 4]
0x475F84: mov      dword ptr [edx + 0x1d4], eax
0x475F8A: mov      esp, ebp
0x475F8C: pop      ebp
0x475F8D: ret      
0x475F8E: mov      eax, eax
0x475F90: push     ebp
0x475F91: mov      ebp, esp
0x475F93: add      esp, -0x18
0x475F96: mov      byte ptr [ebp - 0x11], dl
0x475F99: mov      dword ptr [ebp - 4], eax
0x475F9C: mov      eax, dword ptr [ebp - 4]
0x475F9F: movzx    edx, word ptr [eax + 0x14]
0x475FA3: mov      eax, dword ptr [ebp - 4]
0x475FA6: movzx    eax, byte ptr [eax + 0xe]
0x475FAA: call     0x495ca8
0x475FAF: mov      dword ptr [ebp - 0x10], eax
0x475FB2: cmp      dword ptr [ebp - 0x10], 0
0x475FB6: je       0x47607d
0x475FBC: mov      eax, dword ptr [ebp - 4]
0x475FBF: cmp      byte ptr [eax + 0x20], 0
0x475FC3: je       0x47600e
0x475FC5: mov      eax, dword ptr [ebp - 4]
0x475FC8: cmp      byte ptr [eax + 0x21], 0
0x475FCC: je       0x475fdd
0x475FCE: mov      eax, dword ptr [ebp - 0x10]
0x475FD1: movzx    eax, word ptr [eax + 0x3c]
0x475FD5: mov      dword ptr [ebp - 8], eax
0x475FD8: jmp      0x47607d
0x475FDD: mov      eax, dword ptr [ebp - 0x10]
0x475FE0: movzx    eax, word ptr [eax + 0x34]
0x475FE4: mov      edx, dword ptr [ebp - 0x10]
0x475FE7: movzx    edx, word ptr [edx + 0x36]
0x475FEB: add      eax, edx
0x475FED: mov      edx, dword ptr [ebp - 4]
0x475FF0: movzx    edx, byte ptr [edx + 0xc]
0x475FF4: imul     edx
0x475FF6: mov      edx, dword ptr [ebp - 0x10]
0x475FF9: movzx    edx, word ptr [edx + 0x32]
0x475FFD: add      eax, edx
0x475FFF: mov      edx, dword ptr [ebp - 0x10]
0x476002: movzx    edx, word ptr [edx + 0x34]
0x476006: dec      edx
0x476007: add      eax, edx
0x476009: mov      dword ptr [ebp - 8], eax
0x47600C: jmp      0x47607d
0x47600E: mov      eax, dword ptr [ebp - 0x10]
0x476011: movzx    eax, word ptr [eax + 2]
0x476015: mov      edx, dword ptr [ebp - 4]
0x476018: mov      dword ptr [edx + 0x1f4], eax
0x47601E: mov      eax, dword ptr [ebp - 4]
0x476021: cmp      dword ptr [eax + 0x1ec], 0
0x476028: jge      0x476031
0x47602A: xor      eax, eax
0x47602C: mov      dword ptr [ebp - 0xc], eax
0x47602F: jmp      0x476056
0x476031: mov      eax, dword ptr [ebp - 0x10]
0x476034: movzx    eax, word ptr [eax + 2]
0x476038: mov      edx, dword ptr [ebp - 4]
0x47603B: cmp      eax, dword ptr [edx + 0x1ec]
0x476041: jg       0x47604a
0x476043: xor      eax, eax
0x476045: mov      dword ptr [ebp - 0xc], eax
0x476048: jmp      0x476056
0x47604A: mov      eax, dword ptr [ebp - 4]
0x47604D: mov      eax, dword ptr [eax + 0x1ec]
0x476053: mov      dword ptr [ebp - 0xc], eax
0x476056: mov      eax, dword ptr [ebp - 0x10]
0x476059: movzx    eax, word ptr [eax + 2]
0x47605D: mov      edx, dword ptr [ebp - 0x10]
0x476060: movzx    edx, word ptr [edx + 4]
0x476064: add      eax, edx
0x476066: mov      edx, dword ptr [ebp - 4]
0x476069: movzx    edx, byte ptr [edx + 0xc]
0x47606D: imul     edx
0x47606F: mov      edx, dword ptr [ebp - 0x10]
0x476072: movzx    edx, word ptr [edx]
0x476075: add      eax, edx
0x476077: add      eax, dword ptr [ebp - 0xc]
0x47607A: mov      dword ptr [ebp - 8], eax
0x47607D: mov      eax, dword ptr [ebp - 8]
0x476080: mov      esp, ebp
0x476082: pop      ebp
0x476083: ret      

; === TGasKuDeGi.DMT_-13 @ 0x476084 ===
0x476084: push     ebp
0x476085: mov      ebp, esp
0x476087: push     ecx
0x476088: mov      dword ptr [ebp - 4], eax
0x47608B: mov      eax, dword ptr [ebp - 4]
0x47608E: call     0x497df4 ; TActor.DMT_-13
0x476093: mov      eax, dword ptr [ebp - 4]
0x476096: movzx    eax, byte ptr [eax + 0xe]
0x47609A: cmp      eax, 0x18
0x47609D: jg       0x4760bf
0x47609F: je       0x4760dd
0x4760A1: sub      eax, 0x10
0x4760A4: je       0x476141
0x4760AA: sub      eax, 4
0x4760AD: je       0x4761a7
0x4760B3: dec      eax
0x4760B4: je       0x47620d
0x4760BA: jmp      0x4763ef
0x4760BF: sub      eax, 0x28
0x4760C2: je       0x476273
0x4760C8: sub      eax, 0xc
0x4760CB: je       0x47632a
0x4760D1: dec      eax
0x4760D2: je       0x476390
0x4760D8: jmp      0x4763ef
0x4760DD: mov      eax, dword ptr [ebp - 4]
0x4760E0: cmp      byte ptr [eax + 0xee], 0
0x4760E7: je       0x4763ef
0x4760ED: mov      eax, dword ptr [ebp - 4]
0x4760F0: add      eax, 0x25c
0x4760F5: push     eax
0x4760F6: mov      eax, dword ptr [ebp - 4]
0x4760F9: lea      ecx, [eax + 0x258]
0x4760FF: mov      eax, dword ptr [ebp - 4]
0x476102: movzx    eax, byte ptr [eax + 0xc]
0x476106: mov      edx, eax
0x476108: shl      edx, 3
0x47610B: add      edx, 0x2f8
0x476111: mov      eax, dword ptr [ebp - 4]
0x476114: add      edx, dword ptr [eax + 0x1c0]
0x47611A: mov      eax, dword ptr [ebp - 4]
0x47611D: sub      edx, dword ptr [eax + 0x1bc]
0x476123: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x476128: mov      eax, dword ptr [eax]
0x47612A: mov      eax, dword ptr [eax + 0x318]
0x476130: call     0x453a48
0x476135: mov      edx, dword ptr [ebp - 4]
0x476138: mov      dword ptr [edx + 0x244], eax
0x47613E: pop      ecx
0x47613F: pop      ebp
0x476140: ret      
0x476141: mov      eax, dword ptr [ebp - 4]
0x476144: cmp      byte ptr [eax + 0xee], 0
0x47614B: je       0x4763ef
0x476151: mov      eax, dword ptr [ebp - 4]
0x476154: add      eax, 0x25c
0x476159: push     eax
0x47615A: mov      eax, dword ptr [ebp - 4]
0x47615D: lea      ecx, [eax + 0x258]
0x476163: mov      eax, dword ptr [ebp - 4]
0x476166: mov      edx, dword ptr [eax + 0x250]
0x47616C: add      edx, edx
0x47616E: lea      edx, [edx + edx*4]
0x476171: add      edx, 0x5a4
0x476177: mov      eax, dword ptr [ebp - 4]
0x47617A: add      edx, dword ptr [eax + 0x1c0]
0x476180: mov      eax, dword ptr [ebp - 4]
0x476183: sub      edx, dword ptr [eax + 0x1bc]
0x476189: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47618E: mov      eax, dword ptr [eax]
0x476190: mov      eax, dword ptr [eax + 0x358]
0x476196: call     0x453a48
0x47619B: mov      edx, dword ptr [ebp - 4]
0x47619E: mov      dword ptr [edx + 0x244], eax
0x4761A4: pop      ecx
0x4761A5: pop      ebp
0x4761A6: ret      
0x4761A7: mov      eax, dword ptr [ebp - 4]
0x4761AA: cmp      byte ptr [eax + 0xee], 0
0x4761B1: je       0x4763ef
0x4761B7: mov      eax, dword ptr [ebp - 4]
0x4761BA: add      eax, 0x25c
0x4761BF: push     eax
0x4761C0: mov      eax, dword ptr [ebp - 4]
0x4761C3: lea      ecx, [eax + 0x258]
0x4761C9: mov      eax, dword ptr [ebp - 4]
0x4761CC: mov      edx, dword ptr [eax + 0x250]
0x4761D2: add      edx, edx
0x4761D4: lea      edx, [edx + edx*4]
0x4761D7: add      edx, 0x708
0x4761DD: mov      eax, dword ptr [ebp - 4]
0x4761E0: add      edx, dword ptr [eax + 0x1c0]
0x4761E6: mov      eax, dword ptr [ebp - 4]
0x4761E9: sub      edx, dword ptr [eax + 0x1bc]
0x4761EF: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4761F4: mov      eax, dword ptr [eax]
0x4761F6: mov      eax, dword ptr [eax + 0x360]
0x4761FC: call     0x453a48
0x476201: mov      edx, dword ptr [ebp - 4]
0x476204: mov      dword ptr [edx + 0x244], eax
0x47620A: pop      ecx
0x47620B: pop      ebp
0x47620C: ret      
0x47620D: mov      eax, dword ptr [ebp - 4]
0x476210: cmp      byte ptr [eax + 0xee], 0
0x476217: je       0x4763ef
0x47621D: mov      eax, dword ptr [ebp - 4]
0x476220: add      eax, 0x25c
0x476225: push     eax
0x476226: mov      eax, dword ptr [ebp - 4]
0x476229: lea      ecx, [eax + 0x258]
0x47622F: mov      eax, dword ptr [ebp - 4]
0x476232: mov      edx, dword ptr [eax + 0x250]
0x476238: add      edx, edx
0x47623A: lea      edx, [edx + edx*4]
0x47623D: add      edx, 0x76c
0x476243: mov      eax, dword ptr [ebp - 4]
0x476246: add      edx, dword ptr [eax + 0x1c0]
0x47624C: mov      eax, dword ptr [ebp - 4]
0x47624F: sub      edx, dword ptr [eax + 0x1bc]
0x476255: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47625A: mov      eax, dword ptr [eax]
0x47625C: mov      eax, dword ptr [eax + 0x360]
0x476262: call     0x453a48
0x476267: mov      edx, dword ptr [ebp - 4]
0x47626A: mov      dword ptr [edx + 0x244], eax
0x476270: pop      ecx
0x476271: pop      ebp
0x476272: ret      
0x476273: mov      eax, dword ptr [ebp - 4]
0x476276: cmp      byte ptr [eax + 0xee], 0
0x47627D: je       0x4762d2
0x47627F: mov      eax, dword ptr [ebp - 4]
0x476282: add      eax, 0x25c
0x476287: push     eax
0x476288: mov      eax, dword ptr [ebp - 4]
0x47628B: lea      ecx, [eax + 0x258]
0x476291: mov      eax, dword ptr [ebp - 4]
0x476294: mov      edx, dword ptr [eax + 0x254]
0x47629A: add      edx, edx
0x47629C: lea      edx, [edx + edx*4]
0x47629F: add      edx, 0x15e
0x4762A5: mov      eax, dword ptr [ebp - 4]
0x4762A8: add      edx, dword ptr [eax + 0x1c0]
0x4762AE: mov      eax, dword ptr [ebp - 4]
0x4762B1: sub      edx, dword ptr [eax + 0x1bc]
0x4762B7: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4762BC: mov      eax, dword ptr [eax]
0x4762BE: mov      eax, dword ptr [eax + 0x368]
0x4762C4: call     0x453a48
0x4762C9: mov      edx, dword ptr [ebp - 4]
0x4762CC: mov      dword ptr [edx + 0x244], eax
0x4762D2: mov      eax, dword ptr [ebp - 4]
0x4762D5: cmp      byte ptr [eax + 0x24c], 0
0x4762DC: je       0x4763ef
0x4762E2: mov      eax, dword ptr [ebp - 4]
0x4762E5: add      eax, 0x264
0x4762EA: push     eax
0x4762EB: mov      eax, dword ptr [ebp - 4]
0x4762EE: lea      ecx, [eax + 0x260]
0x4762F4: mov      eax, dword ptr [ebp - 4]
0x4762F7: mov      edx, dword ptr [eax + 0x1b8]
0x4762FD: add      edx, 0x154
0x476303: mov      eax, dword ptr [ebp - 4]
0x476306: sub      edx, dword ptr [eax + 0x1b0]
0x47630C: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x476311: mov      eax, dword ptr [eax]
0x476313: mov      eax, dword ptr [eax + 0x368]
0x476319: call     0x453a48
0x47631E: mov      edx, dword ptr [ebp - 4]
0x476321: mov      dword ptr [edx + 0x248], eax
0x476327: pop      ecx
0x476328: pop      ebp
0x476329: ret      
0x47632A: mov      eax, dword ptr [ebp - 4]
0x47632D: cmp      byte ptr [eax + 0xee], 0
0x476334: je       0x4763ef
0x47633A: mov      eax, dword ptr [ebp - 4]
0x47633D: add      eax, 0x25c
0x476342: push     eax
0x476343: mov      eax, dword ptr [ebp - 4]
0x476346: lea      ecx, [eax + 0x258]
0x47634C: mov      eax, dword ptr [ebp - 4]
0x47634F: mov      edx, dword ptr [eax + 0x250]
0x476355: add      edx, edx
0x476357: lea      edx, [edx + edx*4]
0x47635A: add      edx, 0xe06
0x476360: mov      eax, dword ptr [ebp - 4]
0x476363: add      edx, dword ptr [eax + 0x1c0]
0x476369: mov      eax, dword ptr [ebp - 4]
0x47636C: sub      edx, dword ptr [eax + 0x1bc]
0x476372: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x476377: mov      eax, dword ptr [eax]
0x476379: mov      eax, dword ptr [eax + 0x360]
0x47637F: call     0x453a48
0x476384: mov      edx, dword ptr [ebp - 4]
0x476387: mov      dword ptr [edx + 0x244], eax
0x47638D: pop      ecx
0x47638E: pop      ebp
0x47638F: ret      
0x476390: mov      eax, dword ptr [ebp - 4]
0x476393: cmp      byte ptr [eax + 0xee], 0
0x47639A: je       0x4763ef
0x47639C: mov      eax, dword ptr [ebp - 4]
0x47639F: add      eax, 0x25c
0x4763A4: push     eax
0x4763A5: mov      eax, dword ptr [ebp - 4]
0x4763A8: lea      ecx, [eax + 0x258]
0x4763AE: mov      eax, dword ptr [ebp - 4]
0x4763B1: mov      edx, dword ptr [eax + 0x250]
0x4763B7: add      edx, edx
0x4763B9: lea      edx, [edx + edx*4]
0x4763BC: add      edx, 0xe06
0x4763C2: mov      eax, dword ptr [ebp - 4]
0x4763C5: add      edx, dword ptr [eax + 0x1c0]
0x4763CB: mov      eax, dword ptr [ebp - 4]
0x4763CE: sub      edx, dword ptr [eax + 0x1bc]
0x4763D4: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4763D9: mov      eax, dword ptr [eax]
0x4763DB: mov      eax, dword ptr [eax + 0x358]
0x4763E1: call     0x453a48
0x4763E6: mov      edx, dword ptr [ebp - 4]
0x4763E9: mov      dword ptr [edx + 0x244], eax
0x4763EF: pop      ecx
0x4763F0: pop      ebp
0x4763F1: ret      
0x4763F2: mov      eax, eax
0x4763F4: push     ebp
0x4763F5: mov      ebp, esp
0x4763F7: add      esp, -0x18
0x4763FA: push     ebx
0x4763FB: mov      dword ptr [ebp - 4], eax
0x4763FE: mov      eax, dword ptr [ebp - 4]
0x476401: cmp      dword ptr [eax + 0x148], 0xb
0x476408: je       0x4766bd
0x47640E: mov      eax, dword ptr [ebp - 4]
0x476411: cmp      dword ptr [eax + 0x148], 9
0x476418: je       0x4766bd
0x47641E: mov      eax, dword ptr [ebp - 4]
0x476421: cmp      dword ptr [eax + 0x148], 0xd
0x476428: je       0x4766bd
0x47642E: mov      eax, dword ptr [ebp - 4]
0x476431: mov      byte ptr [eax + 0x1e4], 0
0x476438: mov      eax, dword ptr [ebp - 4]
0x47643B: mov      eax, dword ptr [eax + 0x220]
0x476441: cmp      dword ptr [eax + 8], 2
0x476445: jl       0x476451
0x476447: mov      eax, dword ptr [ebp - 4]
0x47644A: mov      byte ptr [eax + 0x1e4], 1
0x476451: mov      eax, dword ptr [ebp - 4]
0x476454: mov      edx, dword ptr [eax + 0x1b8]
0x47645A: mov      eax, dword ptr [ebp - 4]
0x47645D: sub      edx, dword ptr [eax + 0x1b0]
0x476463: mov      eax, dword ptr [ebp - 4]
0x476466: mov      bx, 0xfff8
0x47646A: call     0x403080
0x47646F: mov      eax, dword ptr [ebp - 4]
0x476472: mov      edx, dword ptr [eax + 0x1b8]
0x476478: mov      eax, dword ptr [ebp - 4]
0x47647B: sub      edx, dword ptr [eax + 0x1b0]
0x476481: mov      eax, dword ptr [ebp - 4]
0x476484: mov      bx, 0xfff7
0x476488: call     0x403080
0x47648D: mov      eax, dword ptr [ebp - 4]
0x476490: cmp      byte ptr [eax + 0xee], 0
0x476497: je       0x476525
0x47649D: mov      eax, dword ptr [ebp - 4]
0x4764A0: cmp      byte ptr [eax + 0x1e4], 0
0x4764A7: je       0x4764cf
0x4764A9: mov      eax, dword ptr [ebp - 4]
0x4764AC: mov      eax, dword ptr [eax + 0x1cc]
0x4764B2: add      eax, eax
0x4764B4: mov      dword ptr [ebp - 0x18], eax
0x4764B7: xor      eax, eax
0x4764B9: mov      dword ptr [ebp - 0x14], eax
0x4764BC: fild     qword ptr [ebp - 0x18]
0x4764BF: fdiv     dword ptr [0x4766c4]
0x4764C5: call     0x4029a0
0x4764CA: mov      dword ptr [ebp - 0xc], eax
0x4764CD: jmp      0x4764db
0x4764CF: mov      eax, dword ptr [ebp - 4]
0x4764D2: mov      eax, dword ptr [eax + 0x1cc]
0x4764D8: mov      dword ptr [ebp - 0xc], eax
0x4764DB: call     0x406154
0x4764E0: mov      edx, dword ptr [ebp - 4]
0x4764E3: sub      eax, dword ptr [edx + 0x1c8]
0x4764E9: cmp      eax, dword ptr [ebp - 0xc]
0x4764EC: jbe      0x476525
0x4764EE: call     0x406154
0x4764F3: mov      edx, dword ptr [ebp - 4]
0x4764F6: mov      dword ptr [edx + 0x1c8], eax
0x4764FC: mov      eax, dword ptr [ebp - 4]
0x4764FF: mov      eax, dword ptr [eax + 0x1c0]
0x476505: mov      edx, dword ptr [ebp - 4]
0x476508: cmp      eax, dword ptr [edx + 0x1c4]
0x47650E: jge      0x47651b
0x476510: mov      eax, dword ptr [ebp - 4]
0x476513: inc      dword ptr [eax + 0x1c0]
0x476519: jmp      0x476525
0x47651B: mov      eax, dword ptr [ebp - 4]
0x47651E: mov      byte ptr [eax + 0xee], 0
0x476525: mov      eax, dword ptr [ebp - 4]
0x476528: mov      eax, dword ptr [eax + 0x1b8]
0x47652E: mov      dword ptr [ebp - 8], eax
0x476531: mov      eax, dword ptr [ebp - 4]
0x476534: cmp      dword ptr [eax + 0x148], 0
0x47653B: je       0x476629
0x476541: mov      eax, dword ptr [ebp - 4]
0x476544: mov      eax, dword ptr [eax + 0x1b8]
0x47654A: mov      edx, dword ptr [ebp - 4]
0x47654D: cmp      eax, dword ptr [edx + 0x1b0]
0x476553: jl       0x476569
0x476555: mov      eax, dword ptr [ebp - 4]
0x476558: mov      eax, dword ptr [eax + 0x1b8]
0x47655E: mov      edx, dword ptr [ebp - 4]
0x476561: cmp      eax, dword ptr [edx + 0x1b4]
0x476567: jle      0x47657b
0x476569: mov      eax, dword ptr [ebp - 4]
0x47656C: mov      eax, dword ptr [eax + 0x1b0]
0x476572: mov      edx, dword ptr [ebp - 4]
0x476575: mov      dword ptr [edx + 0x1b8], eax
0x47657B: mov      eax, dword ptr [ebp - 4]
0x47657E: cmp      byte ptr [eax + 0x1e4], 0
0x476585: je       0x4765ad
0x476587: mov      eax, dword ptr [ebp - 4]
0x47658A: mov      eax, dword ptr [eax + 0x1d0]
0x476590: add      eax, eax
0x476592: mov      dword ptr [ebp - 0x18], eax
0x476595: xor      eax, eax
0x476597: mov      dword ptr [ebp - 0x14], eax
0x47659A: fild     qword ptr [ebp - 0x18]
0x47659D: fdiv     dword ptr [0x4766c4]
0x4765A3: call     0x4029a0
0x4765A8: mov      dword ptr [ebp - 0x10], eax
0x4765AB: jmp      0x4765b9
0x4765AD: mov      eax, dword ptr [ebp - 4]
0x4765B0: mov      eax, dword ptr [eax + 0x1d0]
0x4765B6: mov      dword ptr [ebp - 0x10], eax
0x4765B9: call     0x406154
0x4765BE: mov      edx, dword ptr [ebp - 4]
0x4765C1: sub      eax, dword ptr [edx + 0x1d4]
0x4765C7: cmp      eax, dword ptr [ebp - 0x10]
0x4765CA: jbe      0x47660e
0x4765CC: mov      eax, dword ptr [ebp - 4]
0x4765CF: mov      eax, dword ptr [eax + 0x1b8]
0x4765D5: mov      edx, dword ptr [ebp - 4]
0x4765D8: cmp      eax, dword ptr [edx + 0x1b4]
0x4765DE: jge      0x4765f9
0x4765E0: mov      eax, dword ptr [ebp - 4]
0x4765E3: inc      dword ptr [eax + 0x1b8]
0x4765E9: call     0x406154
0x4765EE: mov      edx, dword ptr [ebp - 4]
0x4765F1: mov      dword ptr [edx + 0x1d4], eax
0x4765F7: jmp      0x47660e
0x4765F9: mov      eax, dword ptr [ebp - 4]
0x4765FC: xor      edx, edx
0x4765FE: mov      dword ptr [eax + 0x148], edx
0x476604: mov      eax, dword ptr [ebp - 4]
0x476607: mov      byte ptr [eax + 0x24c], 0
0x47660E: mov      eax, dword ptr [ebp - 4]
0x476611: xor      edx, edx
0x476613: mov      dword ptr [eax + 0x1ec], edx
0x476619: call     0x406154
0x47661E: mov      edx, dword ptr [ebp - 4]
0x476621: mov      dword ptr [edx + 0x1f0], eax
0x476627: jmp      0x476695
0x476629: call     0x406154
0x47662E: mov      edx, dword ptr [ebp - 4]
0x476631: sub      eax, dword ptr [edx + 0x1fc]
0x476637: cmp      eax, 0xc8
0x47663C: jbe      0x476695
0x47663E: call     0x406154
0x476643: mov      edx, dword ptr [ebp - 4]
0x476646: sub      eax, dword ptr [edx + 0x1f0]
0x47664C: cmp      eax, 0x1f4
0x476651: jbe      0x476689
0x476653: call     0x406154
0x476658: mov      edx, dword ptr [ebp - 4]
0x47665B: mov      dword ptr [edx + 0x1f0], eax
0x476661: mov      eax, dword ptr [ebp - 4]
0x476664: inc      dword ptr [eax + 0x1ec]
0x47666A: mov      eax, dword ptr [ebp - 4]
0x47666D: mov      eax, dword ptr [eax + 0x1ec]
0x476673: mov      edx, dword ptr [ebp - 4]
0x476676: cmp      eax, dword ptr [edx + 0x1f4]
0x47667C: jl       0x476689
0x47667E: mov      eax, dword ptr [ebp - 4]
0x476681: xor      edx, edx
0x476683: mov      dword ptr [eax + 0x1ec], edx
0x476689: mov      eax, dword ptr [ebp - 4]
0x47668C: mov      bx, 0xfffe
0x476690: call     0x403080
0x476695: mov      eax, dword ptr [ebp - 8]
0x476698: mov      edx, dword ptr [ebp - 4]
0x47669B: cmp      eax, dword ptr [edx + 0x1b8]
0x4766A1: je       0x4766bd
0x4766A3: call     0x406154
0x4766A8: mov      edx, dword ptr [ebp - 4]
0x4766AB: mov      dword ptr [edx + 0x204], eax
0x4766B1: mov      eax, dword ptr [ebp - 4]
0x4766B4: mov      bx, 0xfff3
0x4766B8: call     0x403080
0x4766BD: pop      ebx
0x4766BE: mov      esp, ebp
0x4766C0: pop      ebp
0x4766C1: ret      
0x4766C2: add      byte ptr [eax], al
0x4766C4: add      byte ptr [eax], al
0x4766C6: inc      eax
0x4766C7: inc      eax
0x4766C8: push     ebp
0x4766C9: mov      ebp, esp
0x4766CB: add      esp, -0x18
0x4766CE: push     ebx
0x4766CF: mov      dword ptr [ebp - 0xc], ecx
0x4766D2: mov      dword ptr [ebp - 8], edx
0x4766D5: mov      dword ptr [ebp - 4], eax
0x4766D8: mov      eax, dword ptr [ebp - 4]
0x4766DB: mov      al, byte ptr [eax + 0xc]
0x4766DE: sub      al, 8
0x4766E0: jae      0x476774
0x4766E6: call     0x406154
0x4766EB: mov      edx, dword ptr [ebp - 4]
0x4766EE: sub      eax, dword ptr [edx + 0x204]
0x4766F4: cmp      eax, 0xea60
0x4766F9: jbe      0x476715
0x4766FB: call     0x406154
0x476700: mov      edx, dword ptr [ebp - 4]
0x476703: mov      dword ptr [edx + 0x204], eax
0x476709: mov      eax, dword ptr [ebp - 4]
0x47670C: mov      bx, 0xfff3
0x476710: call     0x403080
0x476715: mov      eax, dword ptr [ebp - 4]
0x476718: call     0x498170
0x47671D: mov      byte ptr [ebp - 0xd], al
0x476720: mov      eax, dword ptr [ebp - 4]
0x476723: cmp      dword ptr [eax + 0x140], 0
0x47672A: je       0x476774
0x47672C: mov      eax, dword ptr [ebp - 0xc]
0x47672F: mov      edx, dword ptr [ebp - 4]
0x476732: add      eax, dword ptr [edx + 0xa0]
0x476738: mov      edx, dword ptr [ebp - 4]
0x47673B: add      eax, dword ptr [edx + 0x98]
0x476741: push     eax
0x476742: mov      eax, dword ptr [ebp + 0xc]
0x476745: mov      edx, dword ptr [ebp - 4]
0x476748: add      eax, dword ptr [edx + 0xac]
0x47674E: mov      edx, dword ptr [ebp - 4]
0x476751: add      eax, dword ptr [edx + 0x9c]
0x476757: push     eax
0x476758: mov      al, byte ptr [ebp + 8]
0x47675B: push     eax
0x47675C: mov      al, byte ptr [ebp - 0xd]
0x47675F: push     eax
0x476760: mov      eax, dword ptr [ebp - 4]
0x476763: mov      ecx, dword ptr [eax + 0x140]
0x476769: mov      edx, dword ptr [ebp - 8]
0x47676C: mov      eax, dword ptr [ebp - 4]
0x47676F: call     0x497fd0
0x476774: pop      ebx
0x476775: mov      esp, ebp
0x476777: pop      ebp
0x476778: ret      8
0x47677B: nop      
0x47677C: push     ebp
0x47677D: mov      ebp, esp
0x47677F: add      esp, -0x18
0x476782: mov      dword ptr [ebp - 0xc], ecx
0x476785: mov      dword ptr [ebp - 8], edx
0x476788: mov      dword ptr [ebp - 4], eax
0x47678B: mov      eax, dword ptr [ebp - 4]
0x47678E: cmp      byte ptr [eax + 0xee], 0
0x476795: je       0x4767e1
0x476797: mov      eax, dword ptr [ebp - 4]
0x47679A: cmp      dword ptr [eax + 0x244], 0
0x4767A1: je       0x4767e1
0x4767A3: mov      eax, dword ptr [ebp - 4]
0x4767A6: mov      eax, dword ptr [eax + 0x244]
0x4767AC: push     eax
0x4767AD: push     1
0x4767AF: mov      ecx, dword ptr [ebp + 8]
0x4767B2: mov      eax, dword ptr [ebp - 4]
0x4767B5: add      ecx, dword ptr [eax + 0x25c]
0x4767BB: mov      eax, dword ptr [ebp - 4]
0x4767BE: add      ecx, dword ptr [eax + 0x9c]
0x4767C4: mov      edx, dword ptr [ebp - 0xc]
0x4767C7: mov      eax, dword ptr [ebp - 4]
0x4767CA: add      edx, dword ptr [eax + 0x258]
0x4767D0: mov      eax, dword ptr [ebp - 4]
0x4767D3: add      edx, dword ptr [eax + 0x98]
0x4767D9: mov      eax, dword ptr [ebp - 8]
0x4767DC: call     0x455578
0x4767E1: mov      eax, dword ptr [ebp - 4]
0x4767E4: cmp      byte ptr [eax + 0x24c], 0
0x4767EB: je       0x476837
0x4767ED: mov      eax, dword ptr [ebp - 4]
0x4767F0: cmp      dword ptr [eax + 0x248], 0
0x4767F7: je       0x476837
0x4767F9: mov      eax, dword ptr [ebp - 4]
0x4767FC: mov      eax, dword ptr [eax + 0x248]
0x476802: push     eax
0x476803: push     1
0x476805: mov      ecx, dword ptr [ebp + 8]
0x476808: mov      eax, dword ptr [ebp - 4]
0x47680B: add      ecx, dword ptr [eax + 0x264]
0x476811: mov      eax, dword ptr [ebp - 4]
0x476814: add      ecx, dword ptr [eax + 0x9c]
0x47681A: mov      edx, dword ptr [ebp - 0xc]
0x47681D: mov      eax, dword ptr [ebp - 4]
0x476820: add      edx, dword ptr [eax + 0x260]
0x476826: mov      eax, dword ptr [ebp - 4]
0x476829: add      edx, dword ptr [eax + 0x98]
0x47682F: mov      eax, dword ptr [ebp - 8]
0x476832: call     0x455578
0x476837: mov      esp, ebp
0x476839: pop      ebp
0x47683A: ret      4
0x47683D: lea      eax, [eax]
0x476840: push     ebp
0x476841: mov      ebp, esp
0x476843: add      esp, -8
0x476846: mov      dword ptr [ebp - 4], eax
0x476849: mov      eax, dword ptr [ebp - 4]
0x47684C: call     0x4759fc ; TGasKuDeGi.DMT_-1
0x476851: mov      eax, dword ptr [ebp - 4]
0x476854: mov      eax, dword ptr [eax + 0x148]
0x47685A: sub      eax, 0xe
0x47685D: je       0x476866
0x47685F: sub      eax, 0x14
0x476862: je       0x476872
0x476864: jmp      0x4768d2
0x476866: mov      eax, dword ptr [ebp - 4]
0x476869: mov      byte ptr [eax + 0xee], 0
0x476870: jmp      0x4768d2
0x476872: mov      eax, dword ptr [ebp - 4]
0x476875: mov      eax, dword ptr [eax + 0x1b0]
0x47687B: mov      edx, dword ptr [ebp - 4]
0x47687E: mov      dword ptr [edx + 0x1bc], eax
0x476884: mov      eax, dword ptr [ebp - 4]
0x476887: mov      eax, dword ptr [eax + 0x1b0]
0x47688D: mov      edx, dword ptr [ebp - 4]
0x476890: mov      dword ptr [edx + 0x1c0], eax
0x476896: call     0x406154
0x47689B: mov      edx, dword ptr [ebp - 4]
0x47689E: mov      dword ptr [edx + 0x1c8], eax
0x4768A4: mov      eax, dword ptr [ebp - 4]
0x4768A7: mov      eax, dword ptr [eax + 0x1d0]
0x4768AD: mov      edx, dword ptr [ebp - 4]
0x4768B0: mov      dword ptr [edx + 0x1cc], eax
0x4768B6: mov      eax, dword ptr [ebp - 4]
0x4768B9: mov      eax, dword ptr [eax + 0x1b4]
0x4768BF: mov      edx, dword ptr [ebp - 4]
0x4768C2: mov      dword ptr [edx + 0x1c4], eax
0x4768C8: mov      eax, dword ptr [ebp - 4]
0x4768CB: mov      byte ptr [eax + 0xee], 1
0x4768D2: pop      ecx
0x4768D3: pop      ecx
0x4768D4: pop      ebp
0x4768D5: ret      
0x4768D6: mov      eax, eax
0x4768D8: push     ebp
0x4768D9: mov      ebp, esp
0x4768DB: push     ecx
0x4768DC: mov      dword ptr [ebp - 4], eax
0x4768DF: mov      eax, dword ptr [ebp - 4]
0x4768E2: call     0x476084 ; TGasKuDeGi.DMT_-13
0x4768E7: mov      eax, dword ptr [ebp - 4]
0x4768EA: cmp      byte ptr [eax + 0xee], 0
0x4768F1: je       0x476938
0x4768F3: mov      eax, dword ptr [ebp - 4]
0x4768F6: add      eax, 0x25c
0x4768FB: push     eax
0x4768FC: mov      eax, dword ptr [ebp - 4]
0x4768FF: lea      ecx, [eax + 0x258]
0x476905: mov      eax, dword ptr [ebp - 4]
0x476908: mov      edx, dword ptr [eax + 0x1c0]
0x47690E: add      edx, 0x2da
0x476914: mov      eax, dword ptr [ebp - 4]
0x476917: sub      edx, dword ptr [eax + 0x1bc]
0x47691D: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x476922: mov      eax, dword ptr [eax]
0x476924: mov      eax, dword ptr [eax + 0x3a8]
0x47692A: call     0x453a48
0x47692F: mov      edx, dword ptr [ebp - 4]
0x476932: mov      dword ptr [edx + 0x244], eax
0x476938: pop      ecx
0x476939: pop      ebp
0x47693A: ret      
0x47693B: nop      
0x47693C: push     ebp
0x47693D: mov      ebp, esp
0x47693F: add      esp, -0xc
0x476942: mov      dword ptr [ebp - 4], eax
0x476945: mov      eax, dword ptr [ebp - 4]
0x476948: call     0x4745f4 ; TSkeletonOma.DMT_-1
0x47694D: mov      eax, dword ptr [ebp - 4]
0x476950: mov      eax, dword ptr [eax + 0x148]
0x476956: sub      eax, 0x22
0x476959: jne      0x4769c2
0x47695B: mov      eax, dword ptr [ebp - 4]
0x47695E: movzx    eax, word ptr [eax + 0xa]
0x476962: push     eax
0x476963: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x476968: mov      eax, dword ptr [eax]
0x47696A: mov      eax, dword ptr [eax + 0x3a0]
0x476970: push     eax
0x476971: push     0x58c
0x476976: push     0x14
0x476978: mov      eax, dword ptr [ebp - 4]
0x47697B: mov      eax, dword ptr [eax + 0x1d0]
0x476981: push     eax
0x476982: push     1
0x476984: mov      eax, dword ptr [ebp - 4]
0x476987: movzx    ecx, word ptr [eax + 8]
0x47698B: mov      dl, 1
0x47698D: mov      eax, dword ptr [0x49c550]
0x476992: call     0x49e834
0x476997: mov      dword ptr [ebp - 8], eax
0x47699A: cmp      dword ptr [ebp - 8], 0
0x47699E: je       0x4769c2
0x4769A0: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4769A5: mov      eax, dword ptr [eax]
0x4769A7: mov      edx, dword ptr [ebp - 8]
0x4769AA: mov      dword ptr [edx + 0xc], eax
0x4769AD: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x4769B2: mov      eax, dword ptr [eax]
0x4769B4: mov      eax, dword ptr [eax + 0x5a860]
0x4769BA: mov      edx, dword ptr [ebp - 8]
0x4769BD: call     0x40f588
0x4769C2: mov      esp, ebp
0x4769C4: pop      ebp
0x4769C5: ret      
0x4769C6: mov      eax, eax
0x4769C8: push     ebp
0x4769C9: mov      ebp, esp
0x4769CB: add      esp, -0xc
0x4769CE: mov      dword ptr [ebp - 4], eax
0x4769D1: mov      eax, dword ptr [ebp - 4]
0x4769D4: mov      eax, dword ptr [eax + 0x154]
0x4769DA: mov      dword ptr [ebp - 0xc], eax
0x4769DD: cmp      dword ptr [ebp - 0xc], 2
0x4769E1: jge      0x4769f6
0x4769E3: mov      eax, dword ptr [ebp - 4]
0x4769E6: cmp      byte ptr [eax + 0xee], 0
0x4769ED: je       0x4769f6
0x4769EF: mov      dword ptr [ebp - 0xc], 2
0x4769F6: mov      eax, dword ptr [ebp - 0xc]
0x4769F9: mov      dword ptr [ebp - 8], eax
0x4769FC: mov      eax, dword ptr [ebp - 8]
0x4769FF: mov      esp, ebp
0x476A01: pop      ebp
0x476A02: ret      
0x476A03: nop      
0x476A04: push     ebp
0x476A05: mov      ebp, esp
0x476A07: add      esp, -0xc
0x476A0A: mov      dword ptr [ebp - 4], eax
0x476A0D: mov      eax, dword ptr [ebp - 4]
0x476A10: mov      eax, dword ptr [eax + 0x154]
0x476A16: mov      dword ptr [ebp - 0xc], eax
0x476A19: cmp      dword ptr [ebp - 0xc], 2
0x476A1D: jge      0x476a32
0x476A1F: mov      eax, dword ptr [ebp - 4]
0x476A22: cmp      byte ptr [eax + 0xee], 0
0x476A29: je       0x476a32
0x476A2B: mov      dword ptr [ebp - 0xc], 2
0x476A32: mov      eax, dword ptr [ebp - 0xc]
0x476A35: mov      dword ptr [ebp - 8], eax
0x476A38: mov      eax, dword ptr [ebp - 8]
0x476A3B: mov      esp, ebp
0x476A3D: pop      ebp
0x476A3E: ret      
0x476A3F: nop      
0x476A40: push     ebp
0x476A41: mov      ebp, esp
0x476A43: add      esp, -0xc
0x476A46: mov      dword ptr [ebp - 4], eax
0x476A49: mov      eax, dword ptr [ebp - 4]
0x476A4C: mov      dword ptr [eax + 0x1b8], 0xffffffff
0x476A56: mov      eax, dword ptr [ebp - 4]
0x476A59: movzx    eax, word ptr [eax + 0x14]
0x476A5D: call     0x496120
0x476A62: mov      edx, dword ptr [ebp - 4]
0x476A65: mov      dword ptr [edx + 0xe0], eax
0x476A6B: mov      eax, dword ptr [ebp - 4]
0x476A6E: movzx    edx, word ptr [eax + 0x14]
0x476A72: mov      eax, dword ptr [ebp - 4]
0x476A75: movzx    eax, byte ptr [eax + 0xe]
0x476A79: call     0x495ca8
0x476A7E: mov      dword ptr [ebp - 8], eax
0x476A81: cmp      dword ptr [ebp - 8], 0
0x476A85: je       0x476ff5
0x476A8B: mov      eax, dword ptr [ebp - 4]
0x476A8E: mov      byte ptr [eax + 0xee], 0
0x476A95: mov      eax, dword ptr [ebp - 4]
0x476A98: mov      eax, dword ptr [eax + 0x148]
0x476A9E: add      eax, -9
0x476AA1: cmp      eax, 0x19
0x476AA4: ja       0x476ff5
0x476AAA: mov      al, byte ptr [eax + 0x476ab7]
0x476AB0: jmp      dword ptr [eax*4 + 0x476ad1]
0x476AB7: add      al, byte ptr [ecx]
0x476AB9: add      al, byte ptr [eax]
0x476ABB: add      byte ptr [eax + eax], al
0x476ABE: add      byte ptr [eax], al
0x476AC0: add      byte ptr [eax], al
0x476AC2: add      eax, dword ptr [eax]
0x476AC4: add      byte ptr [eax], al
0x476AC6: add      byte ptr [eax], al
0x476AC8: add      byte ptr [eax], al
0x476ACA: add      byte ptr [eax], al
0x476ACC: add      byte ptr [0xf5070006], al
0x476AD2: outsd    dx, dword ptr [esi]
0x476AD3: inc      edi
0x476AD4: add      cl, dh
0x476AD6: push     0x47
0x476AD8: add      byte ptr [esp + ebp*2], bl
0x476ADB: inc      edi
0x476ADC: add      byte ptr [ebx], ah
0x476ADE: insd     dword ptr es:[edi], dx
0x476ADF: inc      edi
0x476AE0: add      ch, al
0x476AE2: insd     dword ptr es:[edi], dx
0x476AE3: inc      edi
0x476AE4: add      byte ptr [ecx + 0x1b00476e], dh
0x476AEA: outsd    dx, dword ptr [esi]
0x476AEB: inc      edi
0x476AEC: add      byte ptr [edx - 0x74ffb891], dl
0x476AF2: inc      ebp
0x476AF3: cld      
0x476AF4: test     byte ptr [eax + 0x1c], 1
0x476AF8: je       0x476b8f
0x476AFE: mov      eax, dword ptr [ebp - 4]
0x476B01: cmp      byte ptr [eax + 0xe], 0x30
0x476B05: je       0x476b10
0x476B07: mov      eax, dword ptr [ebp - 4]
0x476B0A: cmp      byte ptr [eax + 0xe], 0x31
0x476B0E: jne      0x476b22
0x476B10: mov      eax, dword ptr [ebp - 8]
0x476B13: movzx    eax, word ptr [eax + 0x3c]
0x476B17: mov      edx, dword ptr [ebp - 4]
0x476B1A: mov      dword ptr [edx + 0x1b0], eax
0x476B20: jmp      0x476b4d
0x476B22: mov      eax, dword ptr [ebp - 8]
0x476B25: movzx    eax, word ptr [eax + 0x3e]
0x476B29: mov      edx, dword ptr [ebp - 8]
0x476B2C: movzx    edx, word ptr [edx + 0x40]
0x476B30: add      eax, edx
0x476B32: mov      edx, dword ptr [ebp - 4]
0x476B35: movzx    edx, byte ptr [edx + 0xc]
0x476B39: imul     edx
0x476B3B: mov      edx, dword ptr [ebp - 8]
0x476B3E: movzx    edx, word ptr [edx + 0x3c]
0x476B42: add      eax, edx
0x476B44: mov      edx, dword ptr [ebp - 4]
0x476B47: mov      dword ptr [edx + 0x1b0], eax
0x476B4D: mov      eax, dword ptr [ebp - 4]
0x476B50: mov      eax, dword ptr [eax + 0x1b0]
0x476B56: mov      edx, dword ptr [ebp - 4]
0x476B59: mov      dword ptr [edx + 0x1b4], eax
0x476B5F: mov      eax, dword ptr [ebp - 8]
0x476B62: movzx    eax, word ptr [eax + 0x42]
0x476B66: mov      edx, dword ptr [ebp - 4]
0x476B69: mov      dword ptr [edx + 0x1d0], eax
0x476B6F: call     0x406154
0x476B74: mov      edx, dword ptr [ebp - 4]
0x476B77: mov      dword ptr [edx + 0x1d4], eax
0x476B7D: mov      eax, dword ptr [ebp - 8]
0x476B80: movzx    eax, word ptr [eax + 0x3e]
0x476B84: mov      edx, dword ptr [ebp - 4]
0x476B87: mov      dword ptr [edx + 0x1f4], eax
0x476B8D: jmp      0x476c01
0x476B8F: mov      eax, dword ptr [ebp - 8]
0x476B92: movzx    eax, word ptr [eax + 2]
0x476B96: mov      edx, dword ptr [ebp - 8]
0x476B99: movzx    edx, word ptr [edx + 4]
0x476B9D: add      eax, edx
0x476B9F: mov      edx, dword ptr [ebp - 4]
0x476BA2: movzx    edx, byte ptr [edx + 0xc]
0x476BA6: imul     edx
0x476BA8: mov      edx, dword ptr [ebp - 8]
0x476BAB: movzx    edx, word ptr [edx]
0x476BAE: add      eax, edx
0x476BB0: mov      edx, dword ptr [ebp - 4]
0x476BB3: mov      dword ptr [edx + 0x1b0], eax
0x476BB9: mov      eax, dword ptr [ebp - 8]
0x476BBC: movzx    eax, word ptr [eax + 2]
0x476BC0: mov      edx, dword ptr [ebp - 4]
0x476BC3: add      eax, dword ptr [edx + 0x1b0]
0x476BC9: dec      eax
0x476BCA: mov      edx, dword ptr [ebp - 4]
0x476BCD: mov      dword ptr [edx + 0x1b4], eax
0x476BD3: mov      eax, dword ptr [ebp - 8]
0x476BD6: movzx    eax, word ptr [eax + 6]
0x476BDA: mov      edx, dword ptr [ebp - 4]
0x476BDD: mov      dword ptr [edx + 0x1d0], eax
0x476BE3: call     0x406154
0x476BE8: mov      edx, dword ptr [ebp - 4]
0x476BEB: mov      dword ptr [edx + 0x1d4], eax
0x476BF1: mov      eax, dword ptr [ebp - 8]
0x476BF4: movzx    eax, word ptr [eax + 2]
0x476BF8: mov      edx, dword ptr [ebp - 4]
0x476BFB: mov      dword ptr [edx + 0x1f4], eax
0x476C01: push     0
0x476C03: push     1
0x476C05: mov      eax, dword ptr [ebp - 4]
0x476C08: xor      edx, edx
0x476C0A: mov      dl, byte ptr [eax + 0xc]
0x476C0D: xor      ecx, ecx
0x476C0F: mov      eax, dword ptr [ebp - 4]
0x476C12: call     0x497610
0x476C17: jmp      0x476ff5
0x476C1C: mov      eax, dword ptr [ebp - 8]
0x476C1F: movzx    eax, word ptr [eax + 0xc]
0x476C23: mov      edx, dword ptr [ebp - 8]
0x476C26: movzx    edx, word ptr [edx + 0xe]
0x476C2A: add      eax, edx
0x476C2C: mov      edx, dword ptr [ebp - 4]
0x476C2F: movzx    edx, byte ptr [edx + 0xc]
0x476C33: imul     edx
0x476C35: mov      edx, dword ptr [ebp - 8]
0x476C38: movzx    edx, word ptr [edx + 0xa]
0x476C3C: add      eax, edx
0x476C3E: mov      edx, dword ptr [ebp - 4]
0x476C41: mov      dword ptr [edx + 0x1b0], eax
0x476C47: mov      eax, dword ptr [ebp - 8]
0x476C4A: movzx    eax, word ptr [eax + 0xc]
0x476C4E: mov      edx, dword ptr [ebp - 4]
0x476C51: add      eax, dword ptr [edx + 0x1b0]
0x476C57: dec      eax
0x476C58: mov      edx, dword ptr [ebp - 4]
0x476C5B: mov      dword ptr [edx + 0x1b4], eax
0x476C61: mov      eax, dword ptr [ebp - 4]
0x476C64: mov      eax, dword ptr [eax + 0x160]
0x476C6A: mov      edx, dword ptr [ebp - 4]
0x476C6D: mov      dword ptr [edx + 0x1d0], eax
0x476C73: call     0x406154
0x476C78: mov      edx, dword ptr [ebp - 4]
0x476C7B: mov      dword ptr [edx + 0x1d4], eax
0x476C81: mov      eax, dword ptr [ebp - 8]
0x476C84: movzx    eax, byte ptr [eax + 0x12]
0x476C88: mov      edx, dword ptr [ebp - 4]
0x476C8B: mov      dword ptr [edx + 0x1d8], eax
0x476C91: mov      eax, dword ptr [ebp - 4]
0x476C94: xor      edx, edx
0x476C96: mov      dword ptr [eax + 0x1dc], edx
0x476C9C: mov      eax, dword ptr [ebp - 4]
0x476C9F: mov      dword ptr [eax + 0x1e0], 1
0x476CA9: mov      eax, dword ptr [ebp - 4]
0x476CAC: cmp      dword ptr [eax + 0x148], 0xb
0x476CB3: jne      0x476ce9
0x476CB5: push     0
0x476CB7: mov      eax, dword ptr [ebp - 4]
0x476CBA: mov      eax, dword ptr [eax + 0x1b4]
0x476CC0: mov      edx, dword ptr [ebp - 4]
0x476CC3: sub      eax, dword ptr [edx + 0x1b0]
0x476CC9: inc      eax
0x476CCA: push     eax
0x476CCB: mov      eax, dword ptr [ebp - 4]
0x476CCE: xor      edx, edx
0x476CD0: mov      dl, byte ptr [eax + 0xc]
0x476CD3: mov      eax, dword ptr [ebp - 4]
0x476CD6: mov      ecx, dword ptr [eax + 0x1e0]
0x476CDC: mov      eax, dword ptr [ebp - 4]
0x476CDF: call     0x497610
0x476CE4: jmp      0x476ff5
0x476CE9: push     0
0x476CEB: mov      eax, dword ptr [ebp - 4]
0x476CEE: mov      eax, dword ptr [eax + 0x1b4]
0x476CF4: mov      edx, dword ptr [ebp - 4]
0x476CF7: sub      eax, dword ptr [edx + 0x1b0]
0x476CFD: inc      eax
0x476CFE: push     eax
0x476CFF: mov      eax, dword ptr [ebp - 4]
0x476D02: movzx    eax, byte ptr [eax + 0xc]
0x476D06: call     0x4a023c
0x476D0B: mov      edx, eax
0x476D0D: mov      eax, dword ptr [ebp - 4]
0x476D10: mov      ecx, dword ptr [eax + 0x1e0]
0x476D16: mov      eax, dword ptr [ebp - 4]
0x476D19: call     0x497610
0x476D1E: jmp      0x476ff5
0x476D23: mov      eax, dword ptr [ebp - 4]
0x476D26: cmp      byte ptr [eax + 0xe], 0x30
0x476D2A: je       0x476d35
0x476D2C: mov      eax, dword ptr [ebp - 4]
0x476D2F: cmp      byte ptr [eax + 0xe], 0x31
0x476D33: jne      0x476d47
0x476D35: mov      eax, dword ptr [ebp - 8]
0x476D38: movzx    eax, word ptr [eax + 0x3c]
0x476D3C: mov      edx, dword ptr [ebp - 4]
0x476D3F: mov      dword ptr [edx + 0x1b0], eax
0x476D45: jmp      0x476d72
0x476D47: mov      eax, dword ptr [ebp - 8]
0x476D4A: movzx    eax, word ptr [eax + 0x3e]
0x476D4E: mov      edx, dword ptr [ebp - 8]
0x476D51: movzx    edx, word ptr [edx + 0x40]
0x476D55: add      eax, edx
0x476D57: mov      edx, dword ptr [ebp - 4]
0x476D5A: movzx    edx, byte ptr [edx + 0xc]
0x476D5E: imul     edx
0x476D60: mov      edx, dword ptr [ebp - 8]
0x476D63: movzx    edx, word ptr [edx + 0x3c]
0x476D67: add      eax, edx
0x476D69: mov      edx, dword ptr [ebp - 4]
0x476D6C: mov      dword ptr [edx + 0x1b0], eax
0x476D72: mov      eax, dword ptr [ebp - 8]
0x476D75: movzx    eax, word ptr [eax + 0x3e]
0x476D79: mov      edx, dword ptr [ebp - 4]
0x476D7C: add      eax, dword ptr [edx + 0x1b0]
0x476D82: dec      eax
0x476D83: mov      edx, dword ptr [ebp - 4]
0x476D86: mov      dword ptr [edx + 0x1b4], eax
0x476D8C: mov      eax, dword ptr [ebp - 8]
0x476D8F: movzx    eax, word ptr [eax + 0x42]
0x476D93: mov      edx, dword ptr [ebp - 4]
0x476D96: mov      dword ptr [edx + 0x1d0], eax
0x476D9C: call     0x406154
0x476DA1: mov      edx, dword ptr [ebp - 4]
0x476DA4: mov      dword ptr [edx + 0x1d4], eax
0x476DAA: push     0
0x476DAC: push     1
0x476DAE: mov      eax, dword ptr [ebp - 4]
0x476DB1: xor      edx, edx
0x476DB3: mov      dl, byte ptr [eax + 0xc]
0x476DB6: xor      ecx, ecx
0x476DB8: mov      eax, dword ptr [ebp - 4]
0x476DBB: call     0x497610
0x476DC0: jmp      0x476ff5
0x476DC5: mov      eax, dword ptr [ebp - 8]
0x476DC8: movzx    eax, word ptr [eax + 0x16]
0x476DCC: mov      edx, dword ptr [ebp - 8]
0x476DCF: movzx    edx, word ptr [edx + 0x18]
0x476DD3: add      eax, edx
0x476DD5: mov      edx, dword ptr [ebp - 4]
0x476DD8: movzx    edx, byte ptr [edx + 0xc]
0x476DDC: imul     edx
0x476DDE: mov      edx, dword ptr [ebp - 8]
0x476DE1: movzx    edx, word ptr [edx + 0x14]
0x476DE5: add      eax, edx
0x476DE7: mov      edx, dword ptr [ebp - 4]
0x476DEA: mov      dword ptr [edx + 0x1b0], eax
0x476DF0: mov      eax, dword ptr [ebp - 8]
0x476DF3: movzx    eax, word ptr [eax + 0x16]
0x476DF7: mov      edx, dword ptr [ebp - 4]
0x476DFA: add      eax, dword ptr [edx + 0x1b0]
0x476E00: dec      eax
0x476E01: mov      edx, dword ptr [ebp - 4]
0x476E04: mov      dword ptr [edx + 0x1b4], eax
0x476E0A: mov      eax, dword ptr [ebp - 8]
0x476E0D: movzx    eax, word ptr [eax + 0x1a]
0x476E11: mov      edx, dword ptr [ebp - 4]
0x476E14: mov      dword ptr [edx + 0x1d0], eax
0x476E1A: call     0x406154
0x476E1F: mov      edx, dword ptr [ebp - 4]
0x476E22: mov      dword ptr [edx + 0x1d4], eax
0x476E28: mov      eax, dword ptr [ebp - 4]
0x476E2B: cmp      byte ptr [eax + 0xe], 0x31
0x476E2F: jne      0x476e96
0x476E31: mov      eax, dword ptr [ebp - 4]
0x476E34: mov      byte ptr [eax + 0xee], 1
0x476E3B: mov      eax, dword ptr [ebp - 4]
0x476E3E: movzx    eax, byte ptr [eax + 0xc]
0x476E42: mov      edx, dword ptr [ebp - 4]
0x476E45: mov      dword ptr [edx + 0x25c], eax
0x476E4B: mov      eax, dword ptr [ebp - 4]
0x476E4E: xor      edx, edx
0x476E50: mov      dword ptr [eax + 0x1c0], edx
0x476E56: mov      eax, dword ptr [ebp - 4]
0x476E59: xor      edx, edx
0x476E5B: mov      dword ptr [eax + 0x1bc], edx
0x476E61: mov      eax, dword ptr [ebp - 4]
0x476E64: mov      eax, dword ptr [eax + 0x1bc]
0x476E6A: add      eax, 8
0x476E6D: mov      edx, dword ptr [ebp - 4]
0x476E70: mov      dword ptr [edx + 0x1c4], eax
0x476E76: call     0x406154
0x476E7B: mov      edx, dword ptr [ebp - 4]
0x476E7E: mov      dword ptr [edx + 0x1c8], eax
0x476E84: mov      eax, dword ptr [ebp - 4]
0x476E87: mov      eax, dword ptr [eax + 0x1d0]
0x476E8D: mov      edx, dword ptr [ebp - 4]
0x476E90: mov      dword ptr [edx + 0x1cc], eax
0x476E96: push     0
0x476E98: push     1
0x476E9A: mov      eax, dword ptr [ebp - 4]
0x476E9D: xor      edx, edx
0x476E9F: mov      dl, byte ptr [eax + 0xc]
0x476EA2: xor      ecx, ecx
0x476EA4: mov      eax, dword ptr [ebp - 4]
0x476EA7: call     0x497610
0x476EAC: jmp      0x476ff5
0x476EB1: mov      eax, dword ptr [ebp - 8]
0x476EB4: movzx    eax, word ptr [eax + 0x2a]
0x476EB8: mov      edx, dword ptr [ebp - 8]
0x476EBB: movzx    edx, word ptr [edx + 0x2c]
0x476EBF: add      eax, edx
0x476EC1: mov      edx, dword ptr [ebp - 4]
0x476EC4: movzx    edx, byte ptr [edx + 0xc]
0x476EC8: imul     edx
0x476ECA: mov      edx, dword ptr [ebp - 8]
0x476ECD: movzx    edx, word ptr [edx + 0x28]
0x476ED1: add      eax, edx
0x476ED3: mov      edx, dword ptr [ebp - 4]
0x476ED6: mov      dword ptr [edx + 0x1b0], eax
0x476EDC: mov      eax, dword ptr [ebp - 8]
0x476EDF: movzx    eax, word ptr [eax + 0x2a]
0x476EE3: mov      edx, dword ptr [ebp - 4]
0x476EE6: add      eax, dword ptr [edx + 0x1b0]
0x476EEC: dec      eax
0x476EED: mov      edx, dword ptr [ebp - 4]
0x476EF0: mov      dword ptr [edx + 0x1b4], eax
0x476EF6: mov      eax, dword ptr [ebp - 4]
0x476EF9: mov      eax, dword ptr [eax + 0x1e8]
0x476EFF: mov      edx, dword ptr [ebp - 4]
0x476F02: mov      dword ptr [edx + 0x1d0], eax
0x476F08: call     0x406154
0x476F0D: mov      edx, dword ptr [ebp - 4]
0x476F10: mov      dword ptr [edx + 0x1d4], eax
0x476F16: jmp      0x476ff5
0x476F1B: mov      eax, dword ptr [ebp - 8]
0x476F1E: movzx    eax, word ptr [eax + 0x34]
0x476F22: mov      edx, dword ptr [ebp - 8]
0x476F25: movzx    edx, word ptr [edx + 0x36]
0x476F29: add      eax, edx
0x476F2B: mov      edx, dword ptr [ebp - 4]
0x476F2E: movzx    edx, byte ptr [edx + 0xc]
0x476F32: imul     edx
0x476F34: mov      edx, dword ptr [ebp - 8]
0x476F37: movzx    edx, word ptr [edx + 0x32]
0x476F3B: add      eax, edx
0x476F3D: mov      edx, dword ptr [ebp - 4]
0x476F40: mov      dword ptr [edx + 0x1b0], eax
0x476F46: mov      eax, dword ptr [ebp - 8]
0x476F49: movzx    eax, word ptr [eax + 0x34]
0x476F4D: mov      edx, dword ptr [ebp - 4]
0x476F50: add      eax, dword ptr [edx + 0x1b0]
0x476F56: dec      eax
0x476F57: mov      edx, dword ptr [ebp - 4]
0x476F5A: mov      dword ptr [edx + 0x1b4], eax
0x476F60: mov      eax, dword ptr [ebp - 4]
0x476F63: mov      eax, dword ptr [eax + 0x1b4]
0x476F69: mov      edx, dword ptr [ebp - 4]
0x476F6C: mov      dword ptr [edx + 0x1b0], eax
0x476F72: mov      eax, dword ptr [ebp - 8]
0x476F75: movzx    eax, word ptr [eax + 0x38]
0x476F79: mov      edx, dword ptr [ebp - 4]
0x476F7C: mov      dword ptr [edx + 0x1d0], eax
0x476F82: call     0x406154
0x476F87: mov      edx, dword ptr [ebp - 4]
0x476F8A: mov      dword ptr [edx + 0x1d4], eax
0x476F90: jmp      0x476ff5
0x476F92: mov      eax, dword ptr [ebp - 8]
0x476F95: movzx    eax, word ptr [eax + 0x34]
0x476F99: mov      edx, dword ptr [ebp - 8]
0x476F9C: movzx    edx, word ptr [edx + 0x36]
0x476FA0: add      eax, edx
0x476FA2: mov      edx, dword ptr [ebp - 4]
0x476FA5: movzx    edx, byte ptr [edx + 0xc]
0x476FA9: imul     edx
0x476FAB: mov      edx, dword ptr [ebp - 8]
0x476FAE: movzx    edx, word ptr [edx + 0x32]
0x476FB2: add      eax, edx
0x476FB4: mov      edx, dword ptr [ebp - 4]
0x476FB7: mov      dword ptr [edx + 0x1b0], eax
0x476FBD: mov      eax, dword ptr [ebp - 8]
0x476FC0: movzx    eax, word ptr [eax + 0x34]
0x476FC4: mov      edx, dword ptr [ebp - 4]
0x476FC7: add      eax, dword ptr [edx + 0x1b0]
0x476FCD: dec      eax
0x476FCE: mov      edx, dword ptr [ebp - 4]
0x476FD1: mov      dword ptr [edx + 0x1b4], eax
0x476FD7: mov      eax, dword ptr [ebp - 8]
0x476FDA: movzx    eax, word ptr [eax + 0x38]
0x476FDE: mov      edx, dword ptr [ebp - 4]
0x476FE1: mov      dword ptr [edx + 0x1d0], eax
0x476FE7: call     0x406154
0x476FEC: mov      edx, dword ptr [ebp - 4]
0x476FEF: mov      dword ptr [edx + 0x1d4], eax
0x476FF5: mov      esp, ebp
0x476FF7: pop      ebp
0x476FF8: ret      
0x476FF9: lea      eax, [eax]
0x476FFC: push     ebp
0x476FFD: mov      ebp, esp
0x476FFF: push     ecx
0x477000: mov      dword ptr [ebp - 4], eax
0x477003: mov      eax, dword ptr [ebp - 4]
0x477006: call     0x474d54 ; TSkeletonOma.DMT_-13
0x47700B: mov      eax, dword ptr [ebp - 4]
0x47700E: mov      al, byte ptr [eax + 0xe]
0x477011: add      al, 0xd0
0x477013: sub      al, 2
0x477015: jae      0x477076
0x477017: mov      eax, dword ptr [ebp - 4]
0x47701A: cmp      byte ptr [eax + 0xee], 0
0x477021: je       0x477076
0x477023: mov      eax, dword ptr [ebp - 4]
0x477026: add      eax, 0x258
0x47702B: push     eax
0x47702C: mov      eax, dword ptr [ebp - 4]
0x47702F: lea      ecx, [eax + 0x254]
0x477035: mov      eax, dword ptr [ebp - 4]
0x477038: mov      edx, dword ptr [eax + 0x25c]
0x47703E: add      edx, edx
0x477040: lea      edx, [edx + edx*4]
0x477043: add      edx, 0x690
0x477049: mov      eax, dword ptr [ebp - 4]
0x47704C: add      edx, dword ptr [eax + 0x1c0]
0x477052: mov      eax, dword ptr [ebp - 4]
0x477055: sub      edx, dword ptr [eax + 0x1bc]
0x47705B: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x477060: mov      eax, dword ptr [eax]
0x477062: mov      eax, dword ptr [eax + 0x38c]
0x477068: call     0x453a48
0x47706D: mov      edx, dword ptr [ebp - 4]
0x477070: mov      dword ptr [edx + 0x250], eax
0x477076: pop      ecx
0x477077: pop      ebp
0x477078: ret      
0x477079: lea      eax, [eax]
0x47707C: push     ebp
0x47707D: mov      ebp, esp
0x47707F: add      esp, -0x18
0x477082: mov      byte ptr [ebp - 0x11], dl
0x477085: mov      dword ptr [ebp - 4], eax
0x477088: mov      eax, dword ptr [ebp - 4]
0x47708B: movzx    edx, word ptr [eax + 0x14]
0x47708F: mov      eax, dword ptr [ebp - 4]
0x477092: movzx    eax, byte ptr [eax + 0xe]
0x477096: call     0x495ca8
0x47709B: mov      dword ptr [ebp - 0x10], eax
0x47709E: cmp      dword ptr [ebp - 0x10], 0
0x4770A2: je       0x4771a4
0x4770A8: mov      eax, dword ptr [ebp - 4]
0x4770AB: cmp      byte ptr [eax + 0x20], 0
0x4770AF: je       0x4770e5
0x4770B1: mov      eax, dword ptr [ebp - 0x10]
0x4770B4: movzx    eax, word ptr [eax + 0x34]
0x4770B8: mov      edx, dword ptr [ebp - 0x10]
0x4770BB: movzx    edx, word ptr [edx + 0x36]
0x4770BF: add      eax, edx
0x4770C1: mov      edx, dword ptr [ebp - 4]
0x4770C4: movzx    edx, byte ptr [edx + 0xc]
0x4770C8: imul     edx
0x4770CA: mov      edx, dword ptr [ebp - 0x10]
0x4770CD: movzx    edx, word ptr [edx + 0x32]
0x4770D1: add      eax, edx
0x4770D3: mov      edx, dword ptr [ebp - 0x10]
0x4770D6: movzx    edx, word ptr [edx + 0x34]
0x4770DA: dec      edx
0x4770DB: add      eax, edx
0x4770DD: mov      dword ptr [ebp - 8], eax
0x4770E0: jmp      0x4771a4
0x4770E5: mov      eax, dword ptr [ebp - 4]
0x4770E8: test     byte ptr [eax + 0x1c], 1
0x4770EC: je       0x477135
0x4770EE: mov      eax, dword ptr [ebp - 4]
0x4770F1: mov      al, byte ptr [eax + 0xe]
0x4770F4: sub      al, 0x2f
0x4770F6: je       0x477102
0x4770F8: dec      eax
0x4770F9: sub      al, 2
0x4770FB: jb       0x477129
0x4770FD: jmp      0x4771a4
0x477102: mov      eax, dword ptr [ebp - 0x10]
0x477105: movzx    eax, word ptr [eax + 0x3e]
0x477109: mov      edx, dword ptr [ebp - 0x10]
0x47710C: movzx    edx, word ptr [edx + 0x40]
0x477110: add      eax, edx
0x477112: mov      edx, dword ptr [ebp - 4]
0x477115: movzx    edx, byte ptr [edx + 0xc]
0x477119: imul     edx
0x47711B: mov      edx, dword ptr [ebp - 0x10]
0x47711E: movzx    edx, word ptr [edx + 0x3c]
0x477122: add      eax, edx
0x477124: mov      dword ptr [ebp - 8], eax
0x477127: jmp      0x4771a4
0x477129: mov      eax, dword ptr [ebp - 0x10]
0x47712C: movzx    eax, word ptr [eax + 0x3c]
0x477130: mov      dword ptr [ebp - 8], eax
0x477133: jmp      0x4771a4
0x477135: mov      eax, dword ptr [ebp - 0x10]
0x477138: movzx    eax, word ptr [eax + 2]
0x47713C: mov      edx, dword ptr [ebp - 4]
0x47713F: mov      dword ptr [edx + 0x1f4], eax
0x477145: mov      eax, dword ptr [ebp - 4]
0x477148: cmp      dword ptr [eax + 0x1ec], 0
0x47714F: jge      0x477158
0x477151: xor      eax, eax
0x477153: mov      dword ptr [ebp - 0xc], eax
0x477156: jmp      0x47717d
0x477158: mov      eax, dword ptr [ebp - 0x10]
0x47715B: movzx    eax, word ptr [eax + 2]
0x47715F: mov      edx, dword ptr [ebp - 4]
0x477162: cmp      eax, dword ptr [edx + 0x1ec]
0x477168: jg       0x477171
0x47716A: xor      eax, eax
0x47716C: mov      dword ptr [ebp - 0xc], eax
0x47716F: jmp      0x47717d
0x477171: mov      eax, dword ptr [ebp - 4]
0x477174: mov      eax, dword ptr [eax + 0x1ec]
0x47717A: mov      dword ptr [ebp - 0xc], eax
0x47717D: mov      eax, dword ptr [ebp - 0x10]
0x477180: movzx    eax, word ptr [eax + 2]
0x477184: mov      edx, dword ptr [ebp - 0x10]
0x477187: movzx    edx, word ptr [edx + 4]
0x47718B: add      eax, edx
0x47718D: mov      edx, dword ptr [ebp - 4]
0x477190: movzx    edx, byte ptr [edx + 0xc]
0x477194: imul     edx
0x477196: mov      edx, dword ptr [ebp - 0x10]
0x477199: movzx    edx, word ptr [edx]
0x47719C: add      eax, edx
0x47719E: add      eax, dword ptr [ebp - 0xc]
0x4771A1: mov      dword ptr [ebp - 8], eax
0x4771A4: mov      eax, dword ptr [ebp - 8]
0x4771A7: mov      esp, ebp
0x4771A9: pop      ebp
0x4771AA: ret      
0x4771AB: nop      
0x4771AC: push     ebp
0x4771AD: mov      ebp, esp
0x4771AF: add      esp, -0x18
0x4771B2: mov      dword ptr [ebp - 0xc], ecx
0x4771B5: mov      dword ptr [ebp - 8], edx
0x4771B8: mov      dword ptr [ebp - 4], eax
0x4771BB: mov      eax, dword ptr [ebp - 4]
0x4771BE: cmp      byte ptr [eax + 0xee], 0
0x4771C5: je       0x477211
0x4771C7: mov      eax, dword ptr [ebp - 4]
0x4771CA: cmp      dword ptr [eax + 0x250], 0
0x4771D1: je       0x477211
0x4771D3: mov      eax, dword ptr [ebp - 4]
0x4771D6: mov      eax, dword ptr [eax + 0x250]
0x4771DC: push     eax
0x4771DD: push     1
0x4771DF: mov      ecx, dword ptr [ebp + 8]
0x4771E2: mov      eax, dword ptr [ebp - 4]
0x4771E5: add      ecx, dword ptr [eax + 0x258]
0x4771EB: mov      eax, dword ptr [ebp - 4]
0x4771EE: add      ecx, dword ptr [eax + 0x9c]
0x4771F4: mov      edx, dword ptr [ebp - 0xc]
0x4771F7: mov      eax, dword ptr [ebp - 4]
0x4771FA: add      edx, dword ptr [eax + 0x254]
0x477200: mov      eax, dword ptr [ebp - 4]
0x477203: add      edx, dword ptr [eax + 0x98]
0x477209: mov      eax, dword ptr [ebp - 8]
0x47720C: call     0x455578
0x477211: mov      esp, ebp
0x477213: pop      ebp
0x477214: ret      4
0x477217: nop      
0x477218: push     ebp
0x477219: mov      ebp, esp
0x47721B: add      esp, -0xc
0x47721E: mov      dword ptr [ebp - 4], eax
0x477221: mov      eax, dword ptr [ebp - 4]
0x477224: cmp      dword ptr [eax + 0x148], 0xb
0x47722B: je       0x4772b3
0x477231: mov      eax, dword ptr [ebp - 4]
0x477234: cmp      dword ptr [eax + 0x148], 9
0x47723B: je       0x4772b3
0x47723D: mov      eax, dword ptr [ebp - 4]
0x477240: cmp      dword ptr [eax + 0x148], 0xd
0x477247: je       0x4772b3
0x477249: mov      eax, dword ptr [ebp - 4]
0x47724C: cmp      byte ptr [eax + 0xee], 0
0x477253: je       0x4772ab
0x477255: mov      eax, dword ptr [ebp - 4]
0x477258: mov      eax, dword ptr [eax + 0x1cc]
0x47725E: mov      dword ptr [ebp - 8], eax
0x477261: call     0x406154
0x477266: mov      edx, dword ptr [ebp - 4]
0x477269: sub      eax, dword ptr [edx + 0x1c8]
0x47726F: cmp      eax, dword ptr [ebp - 8]
0x477272: jbe      0x4772ab
0x477274: call     0x406154
0x477279: mov      edx, dword ptr [ebp - 4]
0x47727C: mov      dword ptr [edx + 0x1c8], eax
0x477282: mov      eax, dword ptr [ebp - 4]
0x477285: mov      eax, dword ptr [eax + 0x1c0]
0x47728B: mov      edx, dword ptr [ebp - 4]
0x47728E: cmp      eax, dword ptr [edx + 0x1c4]
0x477294: jge      0x4772a1
0x477296: mov      eax, dword ptr [ebp - 4]
0x477299: inc      dword ptr [eax + 0x1c0]
0x47729F: jmp      0x4772ab
0x4772A1: mov      eax, dword ptr [ebp - 4]
0x4772A4: mov      byte ptr [eax + 0xee], 0
0x4772AB: mov      eax, dword ptr [ebp - 4]
0x4772AE: call     0x474e58 ; TSkeletonOma.DMT_-6
0x4772B3: mov      esp, ebp
0x4772B5: pop      ebp
0x4772B6: ret      
0x4772B7: nop      
0x4772B8: push     ebp
0x4772B9: mov      ebp, esp
0x4772BB: xor      eax, eax
0x4772BD: push     ebp
0x4772BE: push     0x4772dd
0x4772C3: push     dword ptr fs:[eax]
0x4772C6: mov      dword ptr fs:[eax], esp
0x4772C9: inc      dword ptr [0x4f95b8]
0x4772CF: xor      eax, eax
0x4772D1: pop      edx
0x4772D2: pop      ecx
0x4772D3: pop      ecx
0x4772D4: mov      dword ptr fs:[eax], edx
0x4772D7: push     0x4772e4
0x4772DC: ret      
0x4772DD: jmp      0x403518
0x4772E2: jmp      0x4772dc
0x4772E4: pop      ebp
0x4772E5: ret      
0x4772E6: mov      eax, eax
0x4772E8: sub      dword ptr [0x4f95b8], 1
0x4772EF: ret      
0x4772F0: cmp      al, 0x73
0x4772F2: inc      edi
0x4772F3: add      byte ptr [eax], al
0x4772F5: add      byte ptr [eax], al
0x4772F7: add      byte ptr [eax], al
0x4772F9: add      byte ptr [eax], al
0x4772FB: add      byte ptr [eax], al
0x4772FD: add      byte ptr [eax], al
0x4772FF: add      byte ptr [eax], al
0x477301: add      byte ptr [eax], al
0x477303: add      byte ptr [eax], al
0x477305: add      byte ptr [eax], al
0x477307: add      byte ptr [eax], al
0x477309: add      byte ptr [eax], al
0x47730B: add      byte ptr [ebx + esi*2], bh
0x47730E: inc      edi
0x47730F: add      byte ptr [edx + 0x73], cl
0x477312: inc      edi
0x477313: add      byte ptr [eax], bh
0x477315: add      byte ptr [eax], al
0x477317: add      byte ptr [eax - 0x37ffbff0], ah
0x47731D: xor      byte ptr [eax], al
0x477320: aam      0x30
0x477322: inc      eax
0x477323: add      al, bl
0x477325: xor      byte ptr [eax], al
0x477328: fdiv     qword ptr [eax]
0x47732A: inc      eax
0x47732B: add      al, dl
0x47732D: xor      byte ptr [eax], al
0x477330: inc      eax
0x477331: inc      eax
0x477333: add      byte ptr [esi + ebp + 0x40], dl
0x477337: add      byte ptr [esp + esi*2], bh
0x47733A: inc      edi
