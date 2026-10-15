
; === VMT_TWMImages @ 0x452CF8 ===
0x452CF8: sar      dh, 0x40
0x452CFB: add      byte ptr [eax], cl
0x452CFD: dec      esp
0x452CFE: inc      ecx
0x452CFF: add      byte ptr [ebp + edi*8 + 0x4cdc0040], bh
0x452D06: inc      ecx
0x452D07: add      byte ptr [ebx + ecx*2 + 0x4ce80041], bl
0x452D0E: inc      ecx
0x452D0F: add      byte ptr [esi + ecx*2], dl
0x452D12: inc      ecx
0x452D13: add      byte ptr [eax + edx*2 + 0x4cf80041], dh
0x452D1A: inc      ecx
0x452D1B: add      al, dh
0x452D1D: dec      esp
0x452D1E: inc      ecx
0x452D1F: add      byte ptr [eax + edx*2 + 0x2e180041], bh
0x452D26: inc      ebp
0x452D27: add      byte ptr [esi], cl
0x452D29: add      byte ptr [eax], al
0x452D2B: add      byte ptr [eax], al
0x452D2D: add      byte ptr [ecx], al
0x452D2F: add      byte ptr [eax], al
0x452D31: add      byte ptr [eax + edx + 0x240040], dl
0x452D38: add      byte ptr [eax], al
0x452D3A: or       dword ptr [edi + edx*2 + 0x4d], edx
0x452D3E: dec      ecx
0x452D3F: insd     dword ptr es:[edi], dx
0x452D40: popal    
0x452D41: jae      0x452d8d
0x452D45: sub      eax, 0x9070045
0x452D4A: push     esp
0x452D4B: push     edi
0x452D4C: dec      ebp
0x452D4D: dec      ecx
0x452D4E: insd     dword ptr es:[edi], dx
0x452D4F: popal    
0x452D50: jae      0x452d4c
0x452D54: sub      al, 0x45
0x452D56: add      al, cl
0x452D58: ljmp     0x5703:0x70040
0x452D5F: dec      ecx
0x452D60: dec      esp
0x452D61: add      eax, 0x40109400
0x452D66: add      byte ptr [eax + eax], ah
0x452D69: add      bh, bh
0x452D6B: and      al, 0
0x452D6D: add      bh, bh
0x452D6F: add      dword ptr [eax], eax
0x452D71: add      byte ptr [eax], al
0x452D73: add      byte ptr [eax], al
0x452D75: add      byte ptr [eax - 0x80000000], al
0x452D7B: add      al, byte ptr [eax]
0x452D7D: or       byte ptr [esi + 0x69], al
0x452D80: insb     byte ptr es:[edi], dx
0x452D81: dec      esi
0x452D83: popal    
0x452D84: insd     dword ptr es:[edi], dx
0x452D85: cmp      al, 0x10
0x452D88: inc      eax
0x452D89: add      byte ptr [eax], ch
0x452D8B: add      byte ptr [eax], al
0x452D8D: inc      dword ptr [eax]
0x452D8F: add      byte ptr [eax], al
0x452D91: add      byte ptr [ecx], al
0x452D93: add      byte ptr [eax], al
0x452D95: add      byte ptr [eax], al
0x452D97: add      byte ptr [eax], al
0x452D99: add      byte ptr [eax], 0
0x452D9C: add      byte ptr [eax + 0x490a0003], al
0x452DA2: insd     dword ptr es:[edi], dx
0x452DA3: popal    
0x452DA4: inc      ebx
0x452DA7: outsd    dx, dword ptr [esi]
0x452DA8: jne      0x452e18
0x452DAA: je       0x452d88
0x452DAC: mov      ebp, 0x300044
0x452DB1: add      bh, bh
0x452DB3: jl       0x452dea
0x452DB5: inc      ebp
0x452DB6: add      byte ptr [ecx], al
0x452DB8: add      byte ptr [eax], al
0x452DBA: add      byte ptr [eax], al
0x452DBC: add      byte ptr [eax], al
0x452DBE: add      byte ptr [eax], 0
0x452DC1: add      byte ptr [eax + 0x44060004], al
0x452DC7: js       0x452e0d
0x452DC9: jb       0x452e2c
0x452DCB: ja       0x452e2d
0x452DCD: sub      al, 0x45
0x452DCF: add      byte ptr [eax + eax], ch
0x452DD2: add      bh, bh
0x452DD4: sub      al, 0
0x452DD6: add      bh, bh
0x452DD8: add      dword ptr [eax], eax
0x452DDA: add      byte ptr [eax], al
0x452DDC: add      byte ptr [eax], al
0x452DDE: add      byte ptr [eax - 0x80000000], al
0x452DE4: add      eax, 0x694c0700
0x452DE9: bound    edx, qword ptr [ecx + edi*2 + 0x70]
0x452DED: cmp      al, 0x10
0x452DF0: inc      eax
0x452DF1: add      byte ptr [eax], bh
0x452DF3: add      byte ptr [eax], al
0x452DF6: cmp      byte ptr [eax], al
0x452DF8: add      bh, bh
0x452DFA: add      dword ptr [eax], eax
0x452DFC: add      byte ptr [eax], al
0x452DFE: add      byte ptr [eax], al
0x452E00: add      byte ptr [eax - 0x80000000], al
0x452E06: push     es
0x452E07: add      byte ptr [0x4d78614d], cl
0x452E0D: insd     dword ptr es:[edi], dx
0x452E0F: outsd    dx, dword ptr [esi]
0x452E10: jb       0x452e8b
0x452E12: push     ebx
0x452E13: imul     edi, dword ptr [edx + 0x65], 0x8b55c08b
0x452E1A: in       al, dx
0x452E1B: add      esp, -0xc
0x452E1E: test     dl, dl
0x452E20: je       0x452e2a
0x452E22: add      esp, -0x10
0x452E25: call     0x4031a4
0x452E2A: mov      dword ptr [ebp - 0xc], ecx
0x452E2D: mov      byte ptr [ebp - 5], dl
0x452E30: mov      dword ptr [ebp - 4], eax
0x452E33: mov      ecx, dword ptr [ebp - 0xc]
0x452E36: xor      edx, edx
0x452E38: mov      eax, dword ptr [ebp - 4]
0x452E3B: call     0x4148f4
0x452E40: mov      eax, dword ptr [ebp - 4]
0x452E43: add      eax, 0x24
0x452E46: call     0x403a58
0x452E4B: mov      eax, dword ptr [ebp - 4]
0x452E4E: mov      byte ptr [eax + 0x2c], 0
0x452E52: mov      eax, dword ptr [ebp - 4]
0x452E55: xor      edx, edx
0x452E57: mov      dword ptr [eax + 0x28], edx
0x452E5A: mov      eax, dword ptr [ebp - 4]
0x452E5D: mov      dword ptr [eax + 0x38], 0xfa000
0x452E64: mov      eax, dword ptr [ebp - 4]
0x452E67: xor      edx, edx
0x452E69: mov      dword ptr [eax + 0x34], edx
0x452E6C: mov      eax, dword ptr [ebp - 4]
0x452E6F: xor      edx, edx
0x452E71: mov      dword ptr [eax + 0x30], edx
0x452E74: mov      eax, dword ptr [ebp - 4]
0x452E77: xor      edx, edx
0x452E79: mov      dword ptr [eax + 0x50], edx
0x452E7C: mov      eax, dword ptr [ebp - 4]
0x452E7F: xor      edx, edx
0x452E81: mov      dword ptr [eax + 0x44], edx
0x452E84: mov      eax, dword ptr [ebp - 4]
0x452E87: xor      edx, edx
0x452E89: mov      dword ptr [eax + 0x48], edx
0x452E8C: mov      dl, 1
0x452E8E: mov      eax, dword ptr [0x40df00]
0x452E93: call     0x402e7c
0x452E98: mov      edx, dword ptr [ebp - 4]
0x452E9B: mov      dword ptr [edx + 0x4c], eax
0x452E9E: mov      dl, 1
0x452EA0: mov      eax, dword ptr [0x447be8]
0x452EA5: call     0x449758
0x452EAA: mov      edx, dword ptr [ebp - 4]
0x452EAD: mov      dword ptr [edx + 0x3c], eax
0x452EB0: mov      edx, 8
0x452EB5: mov      eax, dword ptr [ebp - 4]
0x452EB8: mov      eax, dword ptr [eax + 0x3c]
0x452EBB: call     0x44a404
0x452EC0: call     0x406154
0x452EC5: mov      edx, dword ptr [ebp - 4]
0x452EC8: mov      dword ptr [edx + 0x40], eax
0x452ECB: mov      eax, dword ptr [ebp - 4]
0x452ECE: cmp      byte ptr [ebp - 5], 0
0x452ED2: je       0x452ee3
0x452ED4: call     0x4031fc
0x452ED9: pop      dword ptr fs:[0]
0x452EE0: add      esp, 0xc
0x452EE3: mov      eax, dword ptr [ebp - 4]
0x452EE6: mov      esp, ebp
0x452EE8: pop      ebp
0x452EE9: ret      
0x452EEA: mov      eax, eax
0x452EEC: push     ebp
0x452EED: mov      ebp, esp
0x452EEF: add      esp, -8
0x452EF2: call     0x403204
0x452EF7: mov      byte ptr [ebp - 5], dl
0x452EFA: mov      dword ptr [ebp - 4], eax
0x452EFD: mov      eax, dword ptr [ebp - 4]
0x452F00: mov      eax, dword ptr [eax + 0x4c]
0x452F03: call     0x402eac
0x452F08: mov      eax, dword ptr [ebp - 4]
0x452F0B: cmp      dword ptr [eax + 0x50], 0
0x452F0F: je       0x452f1c
0x452F11: mov      eax, dword ptr [ebp - 4]
0x452F14: mov      eax, dword ptr [eax + 0x50]
0x452F17: call     0x402eac
0x452F1C: mov      eax, dword ptr [ebp - 4]
0x452F1F: mov      eax, dword ptr [eax + 0x3c]
0x452F22: call     0x402eac
0x452F27: mov      dl, byte ptr [ebp - 5]
0x452F2A: and      dl, 0xfc
0x452F2D: mov      eax, dword ptr [ebp - 4]
0x452F30: call     0x414940
0x452F35: cmp      byte ptr [ebp - 5], 0
0x452F39: jle      0x452f43
0x452F3B: mov      eax, dword ptr [ebp - 4]
0x452F3E: call     0x4031f4
0x452F43: pop      ecx
0x452F44: pop      ecx
0x452F45: pop      ebp
0x452F46: ret      
0x452F47: nop      
0x452F48: push     ebp
0x452F49: mov      ebp, esp
0x452F4B: add      esp, -0x4c
0x452F4E: push     ebx
0x452F4F: xor      edx, edx
0x452F51: mov      dword ptr [ebp - 0x48], edx
0x452F54: mov      dword ptr [ebp - 0x4c], edx
0x452F57: mov      dword ptr [ebp - 0x44], edx
0x452F5A: mov      dword ptr [ebp - 8], edx
0x452F5D: mov      dword ptr [ebp - 4], eax
0x452F60: xor      eax, eax
0x452F62: push     ebp
0x452F63: push     0x453161
0x452F68: push     dword ptr fs:[eax]
0x452F6B: mov      dword ptr fs:[eax], esp
0x452F6E: mov      eax, dword ptr [ebp - 4]
0x452F71: test     byte ptr [eax + 0x20], 0x10
0x452F75: jne      0x453136
0x452F7B: mov      eax, dword ptr [ebp - 4]
0x452F7E: cmp      dword ptr [eax + 0x24], 0
0x452F82: jne      0x452f9f
0x452F84: mov      ecx, 0x453178 ; "FileName not assigned.."
0x452F89: mov      dl, 1
0x452F8B: mov      eax, dword ptr [0x406ef0]
0x452F90: call     0x40ae40
0x452F95: call     0x403550
0x452F9A: jmp      0x453136
0x452F9F: mov      eax, dword ptr [ebp - 4]
0x452FA2: cmp      byte ptr [eax + 0x2c], 0
0x452FA6: je       0x452fcc
0x452FA8: mov      eax, dword ptr [ebp - 4]
0x452FAB: cmp      dword ptr [eax + 0x34], 0
0x452FAF: jne      0x452fcc
0x452FB1: mov      ecx, 0x453198 ; "DDraw not assigned.."
0x452FB6: mov      dl, 1
0x452FB8: mov      eax, dword ptr [0x406ef0]
0x452FBD: call     0x40ae40
0x452FC2: call     0x403550
0x452FC7: jmp      0x453136
0x452FCC: mov      eax, dword ptr [ebp - 4]
0x452FCF: mov      eax, dword ptr [eax + 0x24]
0x452FD2: call     0x407f90
0x452FD7: test     al, al
0x452FD9: je       0x453110
0x452FDF: mov      eax, dword ptr [ebp - 4]
0x452FE2: cmp      dword ptr [eax + 0x50], 0
0x452FE6: jne      0x453002
0x452FE8: push     0x40
0x452FEA: mov      eax, dword ptr [ebp - 4]
0x452FED: mov      ecx, dword ptr [eax + 0x24]
0x452FF0: mov      dl, 1
0x452FF2: mov      eax, dword ptr [0x40e54c]
0x452FF7: call     0x411d68
0x452FFC: mov      edx, dword ptr [ebp - 4]
0x452FFF: mov      dword ptr [edx + 0x50], eax
0x453002: lea      edx, [ebp - 0x40]
0x453005: mov      ecx, 0x38
0x45300A: mov      eax, dword ptr [ebp - 4]
0x45300D: mov      eax, dword ptr [eax + 0x50]
0x453010: mov      ebx, dword ptr [eax]
0x453012: call     dword ptr [ebx + 4]
0x453015: mov      eax, dword ptr [ebp - 0x14]
0x453018: mov      edx, dword ptr [ebp - 4]
0x45301B: mov      dword ptr [edx + 0x28], eax
0x45301E: mov      eax, dword ptr [ebp - 4]
0x453021: cmp      byte ptr [eax + 0x2c], 0
0x453025: jne      0x45306d
0x453027: mov      eax, dword ptr [ebp - 4]
0x45302A: mov      eax, dword ptr [eax + 0x28]
0x45302D: shl      eax, 3
0x453030: call     0x4079e8
0x453035: mov      edx, dword ptr [ebp - 4]
0x453038: mov      dword ptr [edx + 0x48], eax
0x45303B: mov      eax, dword ptr [ebp - 4]
0x45303E: cmp      dword ptr [eax + 0x48], 0
0x453042: jne      0x4530b4
0x453044: lea      eax, [ebp - 0x44]
0x453047: mov      ecx, 0x4531b8 ; " BmpArr = nil"
0x45304C: mov      edx, dword ptr [ebp - 4]
0x45304F: mov      edx, dword ptr [edx + 8]
0x453052: call     0x403d20
0x453057: mov      ecx, dword ptr [ebp - 0x44]
0x45305A: mov      dl, 1
0x45305C: mov      eax, dword ptr [0x406ef0]
0x453061: call     0x40ae40
0x453066: call     0x403550
0x45306B: jmp      0x4530b4
0x45306D: mov      eax, dword ptr [ebp - 4]
0x453070: mov      eax, dword ptr [eax + 0x28]
0x453073: shl      eax, 2
0x453076: lea      eax, [eax + eax*2]
0x453079: call     0x4079e8
0x45307E: mov      edx, dword ptr [ebp - 4]
0x453081: mov      dword ptr [edx + 0x44], eax
0x453084: mov      eax, dword ptr [ebp - 4]
0x453087: cmp      dword ptr [eax + 0x44], 0
0x45308B: jne      0x4530b4
0x45308D: lea      eax, [ebp - 0x44]
0x453090: mov      ecx, 0x4531d0 ; " ImgArr = nil"
0x453095: mov      edx, dword ptr [ebp - 4]
0x453098: mov      edx, dword ptr [edx + 8]
0x45309B: call     0x403d20
0x4530A0: mov      ecx, dword ptr [ebp - 0x44]
0x4530A3: mov      dl, 1
0x4530A5: mov      eax, dword ptr [0x406ef0]
0x4530AA: call     0x40ae40
0x4530AF: call     0x403550
0x4530B4: lea      edx, [ebp - 0x48]
0x4530B7: mov      eax, dword ptr [ebp - 4]
0x4530BA: mov      eax, dword ptr [eax + 0x24]
0x4530BD: call     0x4081c8
0x4530C2: push     dword ptr [ebp - 0x48]
0x4530C5: lea      edx, [ebp - 0x4c]
0x4530C8: mov      eax, dword ptr [ebp - 4]
0x4530CB: mov      eax, dword ptr [eax + 0x24]
0x4530CE: call     0x452198
0x4530D3: push     dword ptr [ebp - 0x4c]
0x4530D6: push     0x4531e8 ; ".WIX"
0x4530DB: lea      eax, [ebp - 8]
0x4530DE: mov      edx, 3
0x4530E3: call     0x403d94
0x4530E8: mov      eax, dword ptr [ebp - 4]
0x4530EB: call     0x4533dc
0x4530F0: mov      eax, dword ptr [ebp - 4]
0x4530F3: cmp      byte ptr [eax + 0x2c], 1
0x4530F7: jne      0x453103
0x4530F9: mov      eax, dword ptr [ebp - 4]
0x4530FC: call     0x453290
0x453101: jmp      0x453136
0x453103: mov      edx, dword ptr [ebp - 8]
0x453106: mov      eax, dword ptr [ebp - 4]
0x453109: call     0x453418
0x45310E: jmp      0x453136
0x453110: push     0
0x453112: lea      eax, [ebp - 0x48]
0x453115: mov      ecx, 0x4531f8 ; " 不能发现文件。"
0x45311A: mov      edx, dword ptr [ebp - 4]
0x45311D: mov      edx, dword ptr [edx + 0x24]
0x453120: call     0x403d20
0x453125: mov      eax, dword ptr [ebp - 0x48]
0x453128: mov      cx, word ptr [0x453208]
0x45312F: xor      edx, edx
0x453131: call     0x445ee4
0x453136: xor      eax, eax
0x453138: pop      edx
0x453139: pop      ecx
0x45313A: pop      ecx
0x45313B: mov      dword ptr fs:[eax], edx
0x45313E: push     0x453168
0x453143: lea      eax, [ebp - 0x4c]
0x453146: mov      edx, 2
0x45314B: call     0x403a7c
0x453150: lea      eax, [ebp - 0x44]
0x453153: call     0x403a58
0x453158: lea      eax, [ebp - 8]
0x45315B: call     0x403a58
0x453160: ret      
0x453161: jmp      0x403518
0x453166: jmp      0x453143
0x453168: pop      ebx
0x453169: mov      esp, ebp
0x45316B: pop      ebp
0x45316C: ret      
0x45316D: add      byte ptr [eax], al
0x45316F: add      bh, bh
0x453173: call     dword ptr [edi]
0x453175: add      byte ptr [eax], al
0x453177: add      byte ptr [esi + 0x69], al
0x45317A: insb     byte ptr es:[edi], dx
0x45317B: dec      esi
0x45317D: popal    
0x45317E: insd     dword ptr es:[edi], dx
0x45317F: and      byte ptr gs:[esi + 0x6f], ch
0x453183: je       0x4531a5
0x453185: popal    
0x453186: jae      0x4531fb
0x453188: imul     esp, dword ptr [edi + 0x6e], 0x2e2e6465
0x45318F: add      bh, bh
0x453193: call     dword ptr [eax + eax]
0x453196: add      byte ptr [eax], al
0x453198: inc      esp
0x453199: inc      esp
0x45319A: jb       0x4531fd
0x45319C: ja       0x4531be
0x45319E: outsb    dx, byte ptr [esi]
0x45319F: outsd    dx, dword ptr [esi]
0x4531A0: je       0x4531c2
0x4531A2: popal    
0x4531A3: jae      0x453218
0x4531A5: imul     esp, dword ptr [edi + 0x6e], 0x2e2e6465
0x4531AC: add      byte ptr [eax], al
0x4531AE: add      byte ptr [eax], al
0x4531B8: [STR len=13] " BmpArr = nil"
0x4531D0: [STR len=13] " ImgArr = nil"
0x4531E8: [STR len=4] ".WIX"
0x4531F8: [STR len=15] " 不能发现文件。"
0x453208: add      al, 0
0x45320A: add      byte ptr [eax], al
0x45320C: push     ebp
0x45320D: mov      ebp, esp
0x45320F: add      esp, -0xc
0x453212: mov      dword ptr [ebp - 4], eax
0x453215: mov      eax, dword ptr [ebp - 4]
0x453218: mov      eax, dword ptr [eax + 0x28]
0x45321B: dec      eax
0x45321C: test     eax, eax
0x45321E: jl       0x45326d
0x453220: inc      eax
0x453221: mov      dword ptr [ebp - 0xc], eax
0x453224: mov      dword ptr [ebp - 8], 0
0x45322B: mov      eax, dword ptr [ebp - 8]
0x45322E: lea      eax, [eax + eax*2]
0x453231: mov      edx, dword ptr [ebp - 4]
0x453234: mov      edx, dword ptr [edx + 0x44]
0x453237: cmp      dword ptr [edx + eax*4 + 4], 0
0x45323C: je       0x453265
0x45323E: mov      eax, dword ptr [ebp - 8]
0x453241: lea      eax, [eax + eax*2]
0x453244: mov      edx, dword ptr [ebp - 4]
0x453247: mov      edx, dword ptr [edx + 0x44]
0x45324A: mov      eax, dword ptr [edx + eax*4 + 4]
0x45324E: call     0x402eac
0x453253: mov      eax, dword ptr [ebp - 8]
0x453256: lea      eax, [eax + eax*2]
0x453259: mov      edx, dword ptr [ebp - 4]
0x45325C: mov      edx, dword ptr [edx + 0x44]
0x45325F: xor      ecx, ecx
0x453261: mov      dword ptr [edx + eax*4 + 4], ecx
0x453265: inc      dword ptr [ebp - 8]
0x453268: dec      dword ptr [ebp - 0xc]
0x45326B: jne      0x45322b
0x45326D: mov      eax, dword ptr [ebp - 4]
0x453270: cmp      dword ptr [eax + 0x50], 0
0x453274: je       0x453289
0x453276: mov      eax, dword ptr [ebp - 4]
0x453279: mov      eax, dword ptr [eax + 0x50]
0x45327C: call     0x402eac
0x453281: mov      eax, dword ptr [ebp - 4]
0x453284: xor      edx, edx
0x453286: mov      dword ptr [eax + 0x50], edx
0x453289: mov      esp, ebp
0x45328B: pop      ebp
0x45328C: ret      
0x45328D: lea      eax, [eax]
0x453290: push     ebp
0x453291: mov      ebp, esp
0x453293: add      esp, -0x28
0x453296: push     ebx
0x453297: push     esi
0x453298: push     edi
0x453299: mov      dword ptr [ebp - 4], eax
0x45329C: mov      dl, 1
0x45329E: mov      eax, dword ptr [0x447be8]
0x4532A3: call     0x449758
0x4532A8: mov      dword ptr [ebp - 0xc], eax
0x4532AB: mov      eax, dword ptr [ebp - 4]
0x4532AE: mov      eax, dword ptr [eax + 0x28]
0x4532B1: dec      eax
0x4532B2: test     eax, eax
0x4532B4: jl       0x4533ca
0x4532BA: inc      eax
0x4532BB: mov      dword ptr [ebp - 0x10], eax
0x4532BE: mov      dword ptr [ebp - 8], 0
0x4532C5: lea      edx, [ebp - 0x1c]
0x4532C8: mov      ecx, 8
0x4532CD: mov      eax, dword ptr [ebp - 4]
0x4532D0: mov      eax, dword ptr [eax + 0x50]
0x4532D3: mov      ebx, dword ptr [eax]
0x4532D5: call     dword ptr [ebx + 4]
0x4532D8: movsx    edx, word ptr [ebp - 0x1c]
0x4532DC: mov      eax, dword ptr [ebp - 0xc]
0x4532DF: mov      ecx, dword ptr [eax]
0x4532E1: call     dword ptr [ecx + 0x40]
0x4532E4: movsx    edx, word ptr [ebp - 0x1a]
0x4532E8: mov      eax, dword ptr [ebp - 0xc]
0x4532EB: mov      ecx, dword ptr [eax]
0x4532ED: call     dword ptr [ecx + 0x34]
0x4532F0: mov      eax, dword ptr [ebp - 4]
0x4532F3: mov      edx, dword ptr [ebp - 0xc]
0x4532F6: lea      esi, [eax + 0x54]
0x4532F9: lea      edi, [edx + 0x84]
0x4532FF: mov      ecx, 0x100
0x453304: rep movsd dword ptr es:[edi], dword ptr [esi]
0x453306: mov      eax, dword ptr [ebp - 0xc]
0x453309: call     0x44a7c4
0x45330E: mov      eax, dword ptr [ebp - 0xc]
0x453311: call     0x449edc
0x453316: mov      edx, eax
0x453318: movsx    ecx, word ptr [ebp - 0x1c]
0x45331C: movsx    eax, word ptr [ebp - 0x1a]
0x453320: imul     ecx, eax
0x453323: mov      eax, dword ptr [ebp - 4]
0x453326: mov      eax, dword ptr [eax + 0x50]
0x453329: mov      ebx, dword ptr [eax]
0x45332B: call     dword ptr [ebx + 4]
0x45332E: mov      ax, word ptr [ebp - 0x18]
0x453332: mov      word ptr [ebp - 0x28], ax
0x453336: mov      ax, word ptr [ebp - 0x16]
0x45333A: mov      word ptr [ebp - 0x26], ax
0x45333E: mov      eax, dword ptr [ebp - 4]
0x453341: mov      ecx, dword ptr [eax + 0x34]
0x453344: mov      dl, 1
0x453346: mov      eax, dword ptr [0x44b6a0]
0x45334B: call     0x44d190
0x453350: mov      dword ptr [ebp - 0x24], eax
0x453353: mov      eax, dword ptr [ebp - 0x24]
0x453356: mov      byte ptr [eax + 0x18], 1
0x45335A: movsx    ecx, word ptr [ebp - 0x1a]
0x45335E: movsx    edx, word ptr [ebp - 0x1c]
0x453362: mov      eax, dword ptr [ebp - 0x24]
0x453365: call     0x44e480
0x45336A: mov      eax, dword ptr [ebp - 0xc]
0x45336D: push     eax
0x45336E: mov      eax, dword ptr [ebp - 0x24]
0x453371: call     0x44d8b4
0x453376: xor      ecx, ecx
0x453378: xor      edx, edx
0x45337A: call     0x417eb8
0x45337F: mov      eax, dword ptr [ebp - 0x24]
0x453382: call     0x44d8b4
0x453387: call     0x44d104
0x45338C: mov      eax, dword ptr [ebp - 0xc]
0x45338F: mov      edx, dword ptr [eax]
0x453391: call     dword ptr [edx + 0x60]
0x453394: xor      edx, edx
0x453396: mov      eax, dword ptr [ebp - 0x24]
0x453399: call     0x44e5ac
0x45339E: mov      eax, dword ptr [ebp - 8]
0x4533A1: lea      eax, [eax + eax*2]
0x4533A4: mov      edx, dword ptr [ebp - 4]
0x4533A7: mov      edx, dword ptr [edx + 0x44]
0x4533AA: mov      ecx, dword ptr [ebp - 0x28]
0x4533AD: mov      dword ptr [edx + eax*4], ecx
0x4533B0: mov      ecx, dword ptr [ebp - 0x24]
0x4533B3: mov      dword ptr [edx + eax*4 + 4], ecx
0x4533B7: mov      ecx, dword ptr [ebp - 0x20]
0x4533BA: mov      dword ptr [edx + eax*4 + 8], ecx
0x4533BE: inc      dword ptr [ebp - 8]
0x4533C1: dec      dword ptr [ebp - 0x10]
0x4533C4: jne      0x4532c5
0x4533CA: mov      eax, dword ptr [ebp - 0xc]
0x4533CD: call     0x402eac
0x4533D2: pop      edi
0x4533D3: pop      esi
0x4533D4: pop      ebx
0x4533D5: mov      esp, ebp
0x4533D7: pop      ebp
0x4533D8: ret      
0x4533D9: lea      eax, [eax]
0x4533DC: push     ebp
0x4533DD: mov      ebp, esp
0x4533DF: add      esp, 0xfffffbfc
0x4533E5: push     ebx
0x4533E6: mov      dword ptr [ebp - 4], eax
0x4533E9: xor      ecx, ecx
0x4533EB: mov      edx, 0x38
0x4533F0: mov      eax, dword ptr [ebp - 4]
0x4533F3: mov      eax, dword ptr [eax + 0x50]
0x4533F6: mov      ebx, dword ptr [eax]
0x4533F8: call     dword ptr [ebx + 0xc]
0x4533FB: mov      eax, dword ptr [ebp - 4]
0x4533FE: lea      edx, [eax + 0x54]
0x453401: mov      ecx, 0x400
0x453406: mov      eax, dword ptr [ebp - 4]
0x453409: mov      eax, dword ptr [eax + 0x50]
0x45340C: mov      ebx, dword ptr [eax]
0x45340E: call     dword ptr [ebx + 4]
0x453411: pop      ebx
0x453412: mov      esp, ebp
0x453414: pop      ebp
0x453415: ret      
0x453416: mov      eax, eax
0x453418: push     ebp
0x453419: mov      ebp, esp
0x45341B: add      esp, -0x50
0x45341E: push     ebx
0x45341F: mov      dword ptr [ebp - 8], edx
0x453422: mov      dword ptr [ebp - 4], eax
0x453425: mov      eax, dword ptr [ebp - 8]
0x453428: call     0x403e88
0x45342D: xor      eax, eax
0x45342F: push     ebp
0x453430: push     0x45350d
0x453435: push     dword ptr fs:[eax]
0x453438: mov      dword ptr fs:[eax], esp
0x45343B: mov      eax, dword ptr [ebp - 4]
0x45343E: mov      eax, dword ptr [eax + 0x4c]
0x453441: mov      bx, 0xffff
0x453445: call     0x403080
0x45344A: mov      eax, dword ptr [ebp - 8]
0x45344D: call     0x407f90
0x453452: test     al, al
0x453454: je       0x4534f7
0x45345A: mov      edx, 0x40
0x45345F: mov      eax, dword ptr [ebp - 8]
0x453462: call     0x407e58
0x453467: mov      dword ptr [ebp - 0xc], eax
0x45346A: cmp      dword ptr [ebp - 0xc], 0
0x45346E: jle      0x4534f7
0x453474: lea      edx, [ebp - 0x50]
0x453477: mov      ecx, 0x30
0x45347C: mov      eax, dword ptr [ebp - 0xc]
0x45347F: call     0x407ebc
0x453484: mov      eax, dword ptr [ebp - 0x24]
0x453487: shl      eax, 2
0x45348A: call     0x4026b4
0x45348F: mov      dword ptr [ebp - 0x1c], eax
0x453492: mov      ecx, dword ptr [ebp - 0x24]
0x453495: shl      ecx, 2
0x453498: mov      edx, dword ptr [ebp - 0x1c]
0x45349B: mov      eax, dword ptr [ebp - 0xc]
0x45349E: call     0x407ebc
0x4534A3: mov      eax, dword ptr [ebp - 0x24]
0x4534A6: dec      eax
0x4534A7: test     eax, eax
0x4534A9: jl       0x4534e7
0x4534AB: inc      eax
0x4534AC: mov      dword ptr [ebp - 0x20], eax
0x4534AF: mov      dword ptr [ebp - 0x10], 0
0x4534B6: mov      eax, 8
0x4534BB: call     0x4026b4
0x4534C0: mov      dword ptr [ebp - 0x18], eax
0x4534C3: mov      eax, dword ptr [ebp - 0x10]
0x4534C6: shl      eax, 2
0x4534C9: add      eax, dword ptr [ebp - 0x1c]
0x4534CC: mov      eax, dword ptr [eax]
0x4534CE: mov      dword ptr [ebp - 0x14], eax
0x4534D1: mov      edx, dword ptr [ebp - 0x14]
0x4534D4: mov      eax, dword ptr [ebp - 4]
0x4534D7: mov      eax, dword ptr [eax + 0x4c]
0x4534DA: call     0x40f588
0x4534DF: inc      dword ptr [ebp - 0x10]
0x4534E2: dec      dword ptr [ebp - 0x20]
0x4534E5: jne      0x4534b6
0x4534E7: mov      eax, dword ptr [ebp - 0x1c]
0x4534EA: call     0x4026cc
0x4534EF: mov      eax, dword ptr [ebp - 0xc]
0x4534F2: call     0x407f20
0x4534F7: xor      eax, eax
0x4534F9: pop      edx
0x4534FA: pop      ecx
0x4534FB: pop      ecx
0x4534FC: mov      dword ptr fs:[eax], edx
0x4534FF: push     0x453514
0x453504: lea      eax, [ebp - 8]
0x453507: call     0x403a58
0x45350C: ret      
0x45350D: jmp      0x403518
0x453512: jmp      0x453504
0x453514: pop      ebx
0x453515: mov      esp, ebp
0x453517: pop      ebp
0x453518: ret      
0x453519: lea      eax, [eax]
0x45351C: push     ebp
0x45351D: mov      ebp, esp
0x45351F: add      esp, -0xc
0x453522: mov      dword ptr [ebp - 8], edx
0x453525: mov      dword ptr [ebp - 4], eax
0x453528: xor      eax, eax
0x45352A: mov      dword ptr [ebp - 0xc], eax
0x45352D: mov      eax, dword ptr [ebp - 4]
0x453530: cmp      byte ptr [eax + 0x2c], 3
0x453534: jne      0x453546
0x453536: mov      edx, dword ptr [ebp - 8]
0x453539: mov      eax, dword ptr [ebp - 4]
0x45353C: call     0x453924
0x453541: mov      dword ptr [ebp - 0xc], eax
0x453544: jmp      0x453573
0x453546: mov      eax, dword ptr [ebp - 4]
0x453549: cmp      byte ptr [eax + 0x2c], 1
0x45354D: jne      0x453573
0x45354F: cmp      dword ptr [ebp - 8], 0
0x453553: jl       0x453573
0x453555: mov      eax, dword ptr [ebp - 8]
0x453558: mov      edx, dword ptr [ebp - 4]
0x45355B: cmp      eax, dword ptr [edx + 0x28]
0x45355E: jge      0x453573
0x453560: mov      eax, dword ptr [ebp - 8]
0x453563: lea      eax, [eax + eax*2]
0x453566: mov      edx, dword ptr [ebp - 4]
0x453569: mov      edx, dword ptr [edx + 0x44]
0x45356C: mov      eax, dword ptr [edx + eax*4 + 4]
0x453570: mov      dword ptr [ebp - 0xc], eax
0x453573: mov      eax, dword ptr [ebp - 0xc]
0x453576: mov      esp, ebp
0x453578: pop      ebp
0x453579: ret      
0x45357A: mov      eax, eax
0x45357C: push     ebp
0x45357D: mov      ebp, esp
0x45357F: add      esp, -8
0x453582: mov      dword ptr [ebp - 8], edx
0x453585: mov      dword ptr [ebp - 4], eax
0x453588: mov      eax, dword ptr [ebp - 8]
0x45358B: mov      edx, dword ptr [ebp - 4]
0x45358E: mov      dword ptr [edx + 0x30], eax
0x453591: pop      ecx
0x453592: pop      ecx
0x453593: pop      ebp
0x453594: ret      
0x453595: lea      eax, [eax]
0x453598: push     ebp
0x453599: mov      ebp, esp
0x45359B: add      esp, 0xffffff64
0x4535A1: push     ebx
0x4535A2: push     esi
0x4535A3: push     edi
0x4535A4: mov      dword ptr [ebp - 0xc], ecx
0x4535A7: mov      dword ptr [ebp - 8], edx
0x4535AA: mov      dword ptr [ebp - 4], eax
0x4535AD: mov      eax, dword ptr [ebp - 4]
0x4535B0: mov      eax, dword ptr [eax + 0x50]
0x4535B3: xor      ecx, ecx
0x4535B5: mov      edx, dword ptr [ebp - 8]
0x4535B8: mov      ebx, dword ptr [eax]
0x4535BA: call     dword ptr [ebx + 0xc]
0x4535BD: lea      edx, [ebp - 0x2c]
0x4535C0: mov      eax, dword ptr [ebp - 4]
0x4535C3: mov      eax, dword ptr [eax + 0x50]
0x4535C6: mov      ecx, 8
0x4535CB: mov      ebx, dword ptr [eax]
0x4535CD: call     dword ptr [ebx + 4]
0x4535D0: cmp      byte ptr [0x4a2cbc], 0 ; *0x4A2CBC->0x408D00
0x4535D7: je       0x4536fa
0x4535DD: xor      eax, eax
0x4535DF: push     ebp
0x4535E0: push     0x45361e
0x4535E5: push     dword ptr fs:[eax]
0x4535E8: mov      dword ptr fs:[eax], esp
0x4535EB: mov      eax, dword ptr [ebp - 4]
0x4535EE: mov      eax, dword ptr [eax + 0x3c]
0x4535F1: mov      edx, dword ptr [eax]
0x4535F3: call     dword ptr [edx + 0x60]
0x4535F6: movsx    edx, word ptr [ebp - 0x2c]
0x4535FA: mov      eax, dword ptr [ebp - 4]
0x4535FD: mov      eax, dword ptr [eax + 0x3c]
0x453600: mov      ecx, dword ptr [eax]
0x453602: call     dword ptr [ecx + 0x40]
0x453605: movsx    edx, word ptr [ebp - 0x2a]
0x453609: mov      eax, dword ptr [ebp - 4]
0x45360C: mov      eax, dword ptr [eax + 0x3c]
0x45360F: mov      ecx, dword ptr [eax]
0x453611: call     dword ptr [ecx + 0x34]
0x453614: xor      eax, eax
0x453616: pop      edx
0x453617: pop      ecx
0x453618: pop      ecx
0x453619: mov      dword ptr fs:[eax], edx
0x45361C: jmp      0x453628
0x45361E: jmp      0x403310
0x453623: call     0x4035c0
0x453628: mov      eax, dword ptr [ebp - 4]
0x45362B: mov      edx, dword ptr [ebp - 4]
0x45362E: mov      edx, dword ptr [edx + 0x3c]
0x453631: lea      esi, [eax + 0x54]
0x453634: lea      edi, [edx + 0x84]
0x45363A: mov      ecx, 0x100
0x45363F: rep movsd dword ptr es:[edi], dword ptr [esi]
0x453641: mov      eax, dword ptr [ebp - 4]
0x453644: mov      eax, dword ptr [eax + 0x3c]
0x453647: call     0x44a7c4
0x45364C: mov      eax, dword ptr [ebp - 4]
0x45364F: mov      eax, dword ptr [eax + 0x3c]
0x453652: call     0x449edc
0x453657: mov      dword ptr [ebp - 0x18], eax
0x45365A: movsx    ecx, word ptr [ebp - 0x2c]
0x45365E: movsx    eax, word ptr [ebp - 0x2a]
0x453662: imul     ecx, eax
0x453665: mov      edx, dword ptr [ebp - 0x18]
0x453668: mov      eax, dword ptr [ebp - 4]
0x45366B: mov      eax, dword ptr [eax + 0x50]
0x45366E: mov      ebx, dword ptr [eax]
0x453670: call     dword ptr [ebx + 4]
0x453673: mov      eax, dword ptr [ebp - 0xc]
0x453676: mov      dx, word ptr [ebp - 0x28]
0x45367A: mov      word ptr [eax], dx
0x45367D: mov      eax, dword ptr [ebp - 0xc]
0x453680: mov      dx, word ptr [ebp - 0x26]
0x453684: mov      word ptr [eax + 2], dx
0x453688: mov      eax, dword ptr [ebp - 4]
0x45368B: mov      ecx, dword ptr [eax + 0x34]
0x45368E: mov      dl, 1
0x453690: mov      eax, dword ptr [0x44b6a0]
0x453695: call     0x44d190
0x45369A: mov      edx, dword ptr [ebp - 0xc]
0x45369D: mov      dword ptr [edx + 4], eax
0x4536A0: mov      eax, dword ptr [ebp - 0xc]
0x4536A3: mov      eax, dword ptr [eax + 4]
0x4536A6: mov      byte ptr [eax + 0x18], 1
0x4536AA: movsx    ecx, word ptr [ebp - 0x2a]
0x4536AE: movsx    edx, word ptr [ebp - 0x2c]
0x4536B2: mov      eax, dword ptr [ebp - 0xc]
0x4536B5: mov      eax, dword ptr [eax + 4]
0x4536B8: call     0x44e480
0x4536BD: mov      eax, dword ptr [ebp - 4]
0x4536C0: mov      eax, dword ptr [eax + 0x3c]
0x4536C3: push     eax
0x4536C4: mov      eax, dword ptr [ebp - 0xc]
0x4536C7: mov      eax, dword ptr [eax + 4]
0x4536CA: call     0x44d8b4
0x4536CF: xor      ecx, ecx
0x4536D1: xor      edx, edx
0x4536D3: call     0x417eb8
0x4536D8: mov      eax, dword ptr [ebp - 0xc]
0x4536DB: mov      eax, dword ptr [eax + 4]
0x4536DE: call     0x44d8b4
0x4536E3: call     0x44d104
0x4536E8: mov      eax, dword ptr [ebp - 0xc]
0x4536EB: mov      eax, dword ptr [eax + 4]
0x4536EE: xor      edx, edx
0x4536F0: call     0x44e5ac
0x4536F5: jmp      0x45381e
0x4536FA: movsx    eax, word ptr [ebp - 0x2c]
0x4536FE: call     0x451f00
0x453703: mov      dword ptr [ebp - 0x20], eax
0x453706: movsx    eax, word ptr [ebp - 0x2a]
0x45370A: imul     dword ptr [ebp - 0x20]
0x45370D: call     0x4026b4
0x453712: mov      dword ptr [ebp - 0x14], eax
0x453715: mov      eax, dword ptr [ebp - 0x14]
0x453718: mov      dword ptr [ebp - 0x10], eax
0x45371B: movsx    ecx, word ptr [ebp - 0x2a]
0x45371F: imul     ecx, dword ptr [ebp - 0x20]
0x453723: mov      edx, dword ptr [ebp - 0x14]
0x453726: mov      eax, dword ptr [ebp - 4]
0x453729: mov      eax, dword ptr [eax + 0x50]
0x45372C: mov      ebx, dword ptr [eax]
0x45372E: call     dword ptr [ebx + 4]
0x453731: xor      eax, eax
0x453733: push     ebp
0x453734: push     0x453817
0x453739: push     dword ptr fs:[eax]
0x45373C: mov      dword ptr fs:[eax], esp
0x45373F: mov      eax, dword ptr [ebp - 4]
0x453742: mov      ecx, dword ptr [eax + 0x34]
0x453745: mov      dl, 1
0x453747: mov      eax, dword ptr [0x44b6a0]
0x45374C: call     0x44d190
0x453751: mov      edx, dword ptr [ebp - 0xc]
0x453754: mov      dword ptr [edx + 4], eax
0x453757: mov      eax, dword ptr [ebp - 0xc]
0x45375A: mov      eax, dword ptr [eax + 4]
0x45375D: mov      byte ptr [eax + 0x18], 1
0x453761: movsx    ecx, word ptr [ebp - 0x2a]
0x453765: mov      eax, dword ptr [ebp - 0xc]
0x453768: mov      eax, dword ptr [eax + 4]
0x45376B: mov      edx, dword ptr [ebp - 0x20]
0x45376E: call     0x44e480
0x453773: mov      eax, dword ptr [ebp - 0xc]
0x453776: mov      dx, word ptr [ebp - 0x28]
0x45377A: mov      word ptr [eax], dx
0x45377D: mov      eax, dword ptr [ebp - 0xc]
0x453780: mov      dx, word ptr [ebp - 0x26]
0x453784: mov      word ptr [eax + 2], dx
0x453788: mov      dword ptr [ebp - 0x98], 0x6c
0x453792: lea      ecx, [ebp - 0x98]
0x453798: xor      eax, eax
0x45379A: mov      edx, eax
0x45379C: mov      eax, dword ptr [ebp - 0xc]
0x45379F: mov      eax, dword ptr [eax + 4]
0x4537A2: call     0x44de90
0x4537A7: mov      eax, dword ptr [ebp - 0x74]
0x4537AA: mov      dword ptr [ebp - 0x18], eax
0x4537AD: movsx    eax, word ptr [ebp - 0x2a]
0x4537B1: dec      eax
0x4537B2: cmp      eax, 0
0x4537B5: jl       0x4537e6
0x4537B7: mov      dword ptr [ebp - 0x1c], eax
0x4537BA: mov      eax, dword ptr [ebp - 0x20]
0x4537BD: imul     dword ptr [ebp - 0x1c]
0x4537C0: add      eax, dword ptr [ebp - 0x14]
0x4537C3: mov      dword ptr [ebp - 0x10], eax
0x4537C6: mov      edx, dword ptr [ebp - 0x18]
0x4537C9: mov      eax, dword ptr [ebp - 0x10]
0x4537CC: mov      ecx, dword ptr [ebp - 0x20]
0x4537CF: call     0x4027cc
0x4537D4: mov      eax, dword ptr [ebp - 0x88]
0x4537DA: add      dword ptr [ebp - 0x18], eax
0x4537DD: dec      dword ptr [ebp - 0x1c]
0x4537E0: cmp      dword ptr [ebp - 0x1c], -1
0x4537E4: jne      0x4537ba
0x4537E6: mov      eax, dword ptr [ebp - 0xc]
0x4537E9: mov      eax, dword ptr [eax + 4]
0x4537EC: xor      edx, edx
0x4537EE: call     0x44e5ac
0x4537F3: xor      eax, eax
0x4537F5: pop      edx
0x4537F6: pop      ecx
0x4537F7: pop      ecx
0x4537F8: mov      dword ptr fs:[eax], edx
0x4537FB: push     0x45381e
0x453800: mov      eax, dword ptr [ebp - 0xc]
0x453803: mov      eax, dword ptr [eax + 4]
0x453806: mov      edx, dword ptr [ebp - 0x74]
0x453809: call     0x44e5d8
0x45380E: mov      eax, dword ptr [ebp - 0x14]
0x453811: call     0x4026cc
0x453816: ret      
0x453817: jmp      0x403518
0x45381C: jmp      0x453800
0x45381E: pop      edi
0x45381F: pop      esi
0x453820: pop      ebx
0x453821: mov      esp, ebp
0x453823: pop      ebp
0x453824: ret      
0x453825: lea      eax, [eax]
0x453828: push     ebp
0x453829: mov      ebp, esp
0x45382B: add      esp, -0xc
0x45382E: mov      dword ptr [ebp - 4], eax
0x453831: mov      eax, dword ptr [ebp - 4]
0x453834: mov      eax, dword ptr [eax + 0x28]
0x453837: dec      eax
0x453838: test     eax, eax
0x45383A: jl       0x453889
0x45383C: inc      eax
0x45383D: mov      dword ptr [ebp - 0xc], eax
0x453840: mov      dword ptr [ebp - 8], 0
0x453847: mov      eax, dword ptr [ebp - 8]
0x45384A: lea      eax, [eax + eax*2]
0x45384D: mov      edx, dword ptr [ebp - 4]
0x453850: mov      edx, dword ptr [edx + 0x44]
0x453853: cmp      dword ptr [edx + eax*4 + 4], 0
0x453858: je       0x453881
0x45385A: mov      eax, dword ptr [ebp - 8]
0x45385D: lea      eax, [eax + eax*2]
0x453860: mov      edx, dword ptr [ebp - 4]
0x453863: mov      edx, dword ptr [edx + 0x44]
0x453866: mov      eax, dword ptr [edx + eax*4 + 4]
0x45386A: call     0x402eac
0x45386F: mov      eax, dword ptr [ebp - 8]
0x453872: lea      eax, [eax + eax*2]
0x453875: mov      edx, dword ptr [ebp - 4]
0x453878: mov      edx, dword ptr [edx + 0x44]
0x45387B: xor      ecx, ecx
0x45387D: mov      dword ptr [edx + eax*4 + 4], ecx
0x453881: inc      dword ptr [ebp - 8]
0x453884: dec      dword ptr [ebp - 0xc]
0x453887: jne      0x453847
0x453889: mov      esp, ebp
0x45388B: pop      ebp
0x45388C: ret      
0x45388D: lea      eax, [eax]
0x453890: push     ebp
0x453891: mov      ebp, esp
0x453893: add      esp, -0x1c
0x453896: mov      dword ptr [ebp - 4], eax
0x453899: mov      dword ptr [ebp - 0xc], 0xffffffff
0x4538A0: xor      eax, eax
0x4538A2: mov      dword ptr [ebp - 0x10], eax
0x4538A5: call     0x406154
0x4538AA: mov      dword ptr [ebp - 0x14], eax
0x4538AD: mov      eax, dword ptr [ebp - 4]
0x4538B0: mov      eax, dword ptr [eax + 0x28]
0x4538B3: dec      eax
0x4538B4: test     eax, eax
0x4538B6: jl       0x453920
0x4538B8: inc      eax
0x4538B9: mov      dword ptr [ebp - 0x18], eax
0x4538BC: mov      dword ptr [ebp - 8], 0
0x4538C3: mov      eax, dword ptr [ebp - 8]
0x4538C6: lea      eax, [eax + eax*2]
0x4538C9: mov      edx, dword ptr [ebp - 4]
0x4538CC: mov      edx, dword ptr [edx + 0x44]
0x4538CF: cmp      dword ptr [edx + eax*4 + 4], 0
0x4538D4: je       0x453918
0x4538D6: mov      eax, dword ptr [ebp - 8]
0x4538D9: lea      eax, [eax + eax*2]
0x4538DC: mov      edx, dword ptr [ebp - 4]
0x4538DF: mov      edx, dword ptr [edx + 0x44]
0x4538E2: mov      ecx, dword ptr [ebp - 0x14]
0x4538E5: sub      ecx, dword ptr [edx + eax*4 + 8]
0x4538E9: cmp      ecx, 0x493e0
0x4538EF: jle      0x453918
0x4538F1: mov      eax, dword ptr [ebp - 8]
0x4538F4: lea      eax, [eax + eax*2]
0x4538F7: mov      edx, dword ptr [ebp - 4]
0x4538FA: mov      edx, dword ptr [edx + 0x44]
0x4538FD: mov      eax, dword ptr [edx + eax*4 + 4]
0x453901: call     0x402eac
0x453906: mov      eax, dword ptr [ebp - 8]
0x453909: lea      eax, [eax + eax*2]
0x45390C: mov      edx, dword ptr [ebp - 4]
0x45390F: mov      edx, dword ptr [edx + 0x44]
0x453912: xor      ecx, ecx
0x453914: mov      dword ptr [edx + eax*4 + 4], ecx
0x453918: inc      dword ptr [ebp - 8]
0x45391B: dec      dword ptr [ebp - 0x18]
0x45391E: jne      0x4538c3
0x453920: mov      esp, ebp
0x453922: pop      ebp
0x453923: ret      
0x453924: push     ebp
0x453925: mov      ebp, esp
0x453927: add      esp, -0x10
0x45392A: push     ebx
0x45392B: push     esi
0x45392C: push     edi
0x45392D: mov      dword ptr [ebp - 8], edx
0x453930: mov      dword ptr [ebp - 4], eax
0x453933: xor      eax, eax
0x453935: mov      dword ptr [ebp - 0xc], eax
0x453938: xor      eax, eax
0x45393A: push     ebp
0x45393B: push     0x453a31
0x453940: push     dword ptr fs:[eax]
0x453943: mov      dword ptr fs:[eax], esp
0x453946: cmp      dword ptr [ebp - 8], 0
0x45394A: jl       0x453957
0x45394C: mov      eax, dword ptr [ebp - 4]
0x45394F: mov      eax, dword ptr [eax + 0x28]
0x453952: cmp      eax, dword ptr [ebp - 8]
0x453955: jg       0x453964
0x453957: xor      eax, eax
0x453959: pop      edx
0x45395A: pop      ecx
0x45395B: pop      ecx
0x45395C: mov      dword ptr fs:[eax], edx
0x45395F: jmp      0x453a3b
0x453964: call     0x406154
0x453969: mov      edx, dword ptr [ebp - 4]
0x45396C: sub      eax, dword ptr [edx + 0x40]
0x45396F: cmp      eax, 0x2710
0x453974: jbe      0x453989
0x453976: call     0x406154
0x45397B: mov      edx, dword ptr [ebp - 4]
0x45397E: mov      dword ptr [edx + 0x40], eax
0x453981: mov      eax, dword ptr [ebp - 4]
0x453984: call     0x453890
0x453989: mov      eax, dword ptr [ebp - 8]
0x45398C: lea      eax, [eax + eax*2]
0x45398F: mov      edx, dword ptr [ebp - 4]
0x453992: mov      edx, dword ptr [edx + 0x44]
0x453995: cmp      dword ptr [edx + eax*4 + 4], 0
0x45399A: jne      0x4539ff
0x45399C: mov      eax, dword ptr [ebp - 4]
0x45399F: mov      eax, dword ptr [eax + 0x4c]
0x4539A2: mov      eax, dword ptr [eax + 8]
0x4539A5: cmp      eax, dword ptr [ebp - 8]
0x4539A8: jle      0x453a27
0x4539AA: mov      eax, dword ptr [ebp - 4]
0x4539AD: mov      eax, dword ptr [eax + 0x4c]
0x4539B0: mov      edx, dword ptr [ebp - 8]
0x4539B3: call     0x40f6a0
0x4539B8: mov      dword ptr [ebp - 0x10], eax
0x4539BB: mov      eax, dword ptr [ebp - 8]
0x4539BE: lea      eax, [eax + eax*2]
0x4539C1: mov      edx, dword ptr [ebp - 4]
0x4539C4: mov      edx, dword ptr [edx + 0x44]
0x4539C7: lea      ecx, [edx + eax*4]
0x4539CA: mov      edx, dword ptr [ebp - 0x10]
0x4539CD: mov      eax, dword ptr [ebp - 4]
0x4539D0: call     0x453598
0x4539D5: call     0x406154
0x4539DA: mov      edx, dword ptr [ebp - 8]
0x4539DD: lea      edx, [edx + edx*2]
0x4539E0: mov      ecx, dword ptr [ebp - 4]
0x4539E3: mov      ecx, dword ptr [ecx + 0x44]
0x4539E6: mov      dword ptr [ecx + edx*4 + 8], eax
0x4539EA: mov      eax, dword ptr [ebp - 8]
0x4539ED: lea      eax, [eax + eax*2]
0x4539F0: mov      edx, dword ptr [ebp - 4]
0x4539F3: mov      edx, dword ptr [edx + 0x44]
0x4539F6: mov      eax, dword ptr [edx + eax*4 + 4]
0x4539FA: mov      dword ptr [ebp - 0xc], eax
0x4539FD: jmp      0x453a27
0x4539FF: call     0x406154
0x453A04: mov      edx, dword ptr [ebp - 8]
0x453A07: lea      edx, [edx + edx*2]
0x453A0A: mov      ecx, dword ptr [ebp - 4]
0x453A0D: mov      ecx, dword ptr [ecx + 0x44]
0x453A10: mov      dword ptr [ecx + edx*4 + 8], eax
0x453A14: mov      eax, dword ptr [ebp - 8]
0x453A17: lea      eax, [eax + eax*2]
0x453A1A: mov      edx, dword ptr [ebp - 4]
0x453A1D: mov      edx, dword ptr [edx + 0x44]
0x453A20: mov      eax, dword ptr [edx + eax*4 + 4]
0x453A24: mov      dword ptr [ebp - 0xc], eax
0x453A27: xor      eax, eax
0x453A29: pop      edx
0x453A2A: pop      ecx
0x453A2B: pop      ecx
0x453A2C: mov      dword ptr fs:[eax], edx
0x453A2F: jmp      0x453a3b
0x453A31: jmp      0x403310
0x453A36: call     0x4035c0
0x453A3B: mov      eax, dword ptr [ebp - 0xc]
0x453A3E: pop      edi
0x453A3F: pop      esi
0x453A40: pop      ebx
0x453A41: mov      esp, ebp
0x453A43: pop      ebp
0x453A44: ret      
0x453A45: lea      eax, [eax]
0x453A48: push     ebp
0x453A49: mov      ebp, esp
0x453A4B: add      esp, -0x14
0x453A4E: push     ebx
0x453A4F: push     esi
0x453A50: push     edi
0x453A51: mov      dword ptr [ebp - 0xc], ecx
0x453A54: mov      dword ptr [ebp - 8], edx
0x453A57: mov      dword ptr [ebp - 4], eax
0x453A5A: xor      eax, eax
0x453A5C: mov      dword ptr [ebp - 0x10], eax
0x453A5F: xor      eax, eax
0x453A61: push     ebp
0x453A62: push     0x453bb6
0x453A67: push     dword ptr fs:[eax]
0x453A6A: mov      dword ptr fs:[eax], esp
0x453A6D: cmp      dword ptr [ebp - 8], 0
0x453A71: jl       0x453a7e
0x453A73: mov      eax, dword ptr [ebp - 4]
0x453A76: mov      eax, dword ptr [eax + 0x28]
0x453A79: cmp      eax, dword ptr [ebp - 8]
0x453A7C: jg       0x453a8b
0x453A7E: xor      eax, eax
0x453A80: pop      edx
0x453A81: pop      ecx
0x453A82: pop      ecx
0x453A83: mov      dword ptr fs:[eax], edx
0x453A86: jmp      0x453bc0
0x453A8B: call     0x406154
0x453A90: mov      edx, dword ptr [ebp - 4]
0x453A93: sub      eax, dword ptr [edx + 0x40]
0x453A96: cmp      eax, 0x2710
0x453A9B: jbe      0x453ab0
0x453A9D: call     0x406154
0x453AA2: mov      edx, dword ptr [ebp - 4]
0x453AA5: mov      dword ptr [edx + 0x40], eax
0x453AA8: mov      eax, dword ptr [ebp - 4]
0x453AAB: call     0x453890
0x453AB0: mov      eax, dword ptr [ebp - 8]
0x453AB3: lea      eax, [eax + eax*2]
0x453AB6: mov      edx, dword ptr [ebp - 4]
0x453AB9: mov      edx, dword ptr [edx + 0x44]
0x453ABC: cmp      dword ptr [edx + eax*4 + 4], 0
0x453AC1: jne      0x453b59
0x453AC7: mov      eax, dword ptr [ebp - 4]
0x453ACA: mov      eax, dword ptr [eax + 0x4c]
0x453ACD: mov      eax, dword ptr [eax + 8]
0x453AD0: cmp      eax, dword ptr [ebp - 8]
0x453AD3: jle      0x453bac
0x453AD9: mov      eax, dword ptr [ebp - 4]
0x453ADC: mov      eax, dword ptr [eax + 0x4c]
0x453ADF: mov      edx, dword ptr [ebp - 8]
0x453AE2: call     0x40f6a0
0x453AE7: mov      dword ptr [ebp - 0x14], eax
0x453AEA: mov      eax, dword ptr [ebp - 8]
0x453AED: lea      eax, [eax + eax*2]
0x453AF0: mov      edx, dword ptr [ebp - 4]
0x453AF3: mov      edx, dword ptr [edx + 0x44]
0x453AF6: lea      ecx, [edx + eax*4]
0x453AF9: mov      edx, dword ptr [ebp - 0x14]
0x453AFC: mov      eax, dword ptr [ebp - 4]
0x453AFF: call     0x453598
0x453B04: call     0x406154
0x453B09: mov      edx, dword ptr [ebp - 8]
0x453B0C: lea      edx, [edx + edx*2]
0x453B0F: mov      ecx, dword ptr [ebp - 4]
0x453B12: mov      ecx, dword ptr [ecx + 0x44]
0x453B15: mov      dword ptr [ecx + edx*4 + 8], eax
0x453B19: mov      eax, dword ptr [ebp - 8]
0x453B1C: lea      eax, [eax + eax*2]
0x453B1F: mov      edx, dword ptr [ebp - 4]
0x453B22: mov      edx, dword ptr [edx + 0x44]
0x453B25: movsx    eax, word ptr [edx + eax*4]
0x453B29: mov      edx, dword ptr [ebp - 0xc]
0x453B2C: mov      dword ptr [edx], eax
0x453B2E: mov      eax, dword ptr [ebp - 8]
0x453B31: lea      eax, [eax + eax*2]
0x453B34: mov      edx, dword ptr [ebp - 4]
0x453B37: mov      edx, dword ptr [edx + 0x44]
0x453B3A: movsx    eax, word ptr [edx + eax*4 + 2]
0x453B3F: mov      edx, dword ptr [ebp + 8]
0x453B42: mov      dword ptr [edx], eax
0x453B44: mov      eax, dword ptr [ebp - 8]
0x453B47: lea      eax, [eax + eax*2]
0x453B4A: mov      edx, dword ptr [ebp - 4]
0x453B4D: mov      edx, dword ptr [edx + 0x44]
0x453B50: mov      eax, dword ptr [edx + eax*4 + 4]
0x453B54: mov      dword ptr [ebp - 0x10], eax
0x453B57: jmp      0x453bac
0x453B59: call     0x406154
0x453B5E: mov      edx, dword ptr [ebp - 8]
0x453B61: lea      edx, [edx + edx*2]
0x453B64: mov      ecx, dword ptr [ebp - 4]
0x453B67: mov      ecx, dword ptr [ecx + 0x44]
0x453B6A: mov      dword ptr [ecx + edx*4 + 8], eax
0x453B6E: mov      eax, dword ptr [ebp - 8]
0x453B71: lea      eax, [eax + eax*2]
0x453B74: mov      edx, dword ptr [ebp - 4]
0x453B77: mov      edx, dword ptr [edx + 0x44]
0x453B7A: movsx    eax, word ptr [edx + eax*4]
0x453B7E: mov      edx, dword ptr [ebp - 0xc]
0x453B81: mov      dword ptr [edx], eax
0x453B83: mov      eax, dword ptr [ebp - 8]
0x453B86: lea      eax, [eax + eax*2]
0x453B89: mov      edx, dword ptr [ebp - 4]
0x453B8C: mov      edx, dword ptr [edx + 0x44]
0x453B8F: movsx    eax, word ptr [edx + eax*4 + 2]
0x453B94: mov      edx, dword ptr [ebp + 8]
0x453B97: mov      dword ptr [edx], eax
0x453B99: mov      eax, dword ptr [ebp - 8]
0x453B9C: lea      eax, [eax + eax*2]
0x453B9F: mov      edx, dword ptr [ebp - 4]
0x453BA2: mov      edx, dword ptr [edx + 0x44]
0x453BA5: mov      eax, dword ptr [edx + eax*4 + 4]
0x453BA9: mov      dword ptr [ebp - 0x10], eax
0x453BAC: xor      eax, eax
0x453BAE: pop      edx
0x453BAF: pop      ecx
0x453BB0: pop      ecx
0x453BB1: mov      dword ptr fs:[eax], edx
0x453BB4: jmp      0x453bc0
0x453BB6: jmp      0x403310
0x453BBB: call     0x4035c0
0x453BC0: mov      eax, dword ptr [ebp - 0x10]
0x453BC3: pop      edi
0x453BC4: pop      esi
0x453BC5: pop      ebx
0x453BC6: mov      esp, ebp
0x453BC8: pop      ebp
0x453BC9: ret      4
0x453BCC: push     ebp
0x453BCD: mov      ebp, esp
0x453BCF: xor      eax, eax
0x453BD1: push     ebp
0x453BD2: push     0x453bf1
0x453BD7: push     dword ptr fs:[eax]
0x453BDA: mov      dword ptr fs:[eax], esp
0x453BDD: inc      dword ptr [0x4a7290]
0x453BE3: xor      eax, eax
0x453BE5: pop      edx
0x453BE6: pop      ecx
0x453BE7: pop      ecx
0x453BE8: mov      dword ptr fs:[eax], edx
0x453BEB: push     0x453bf8
0x453BF0: ret      
0x453BF1: jmp      0x403518
0x453BF6: jmp      0x453bf0
0x453BF8: pop      ebp
0x453BF9: ret      
0x453BFA: mov      eax, eax
0x453BFC: sub      dword ptr [0x4a7290], 1
0x453C03: ret      
0x453C04: push     ebp
0x453C05: mov      ebp, esp
0x453C07: push     ecx
0x453C08: mov      eax, 1
0x453C0D: cpuid    
0x453C0F: test     edx, 0x800000
0x453C15: mov      byte ptr [ebp - 1], 1
0x453C19: jne      0x453c1f
0x453C1B: mov      byte ptr [ebp - 1], 0
0x453C1F: cmp      byte ptr [ebp - 1], 1
0x453C23: jne      0x453c2b
0x453C25: mov      byte ptr [ebp - 2], 1
0x453C29: jmp      0x453c2f
0x453C2B: mov      byte ptr [ebp - 2], 0
0x453C2F: mov      al, byte ptr [ebp - 2]
0x453C32: pop      ecx
0x453C33: pop      ebp
0x453C34: ret      
0x453C35: lea      eax, [eax]
0x453C38: push     ebp
0x453C39: mov      ebp, esp
0x453C3B: add      esp, -0xc
0x453C3E: xor      eax, eax
0x453C40: mov      dword ptr [ebp - 4], eax
0x453C43: mov      eax, dword ptr [ebp + 8]
0x453C46: mov      edx, dword ptr [ebp - 4]
0x453C49: mov      eax, dword ptr [eax + edx*4 - 0x400]
0x453C50: mov      edx, dword ptr [ebp + 8]
0x453C53: mov      dword ptr [edx - 0x404], eax
0x453C59: xor      eax, eax
0x453C5B: mov      dword ptr [ebp - 8], eax
0x453C5E: mov      eax, dword ptr [ebp + 8]
0x453C61: mov      edx, dword ptr [ebp - 8]
0x453C64: mov      eax, dword ptr [eax + edx*4 - 0x400]
0x453C6B: mov      edx, dword ptr [ebp + 8]
0x453C6E: mov      dword ptr [edx - 0x408], eax
0x453C74: mov      eax, dword ptr [ebp + 8]
0x453C77: movzx    eax, byte ptr [eax - 0x402]
0x453C7E: shr      eax, 1
0x453C80: mov      edx, dword ptr [ebp + 8]
0x453C83: movzx    edx, byte ptr [edx - 0x406]
0x453C8A: shr      edx, 1
0x453C8C: add      al, dl
0x453C8E: mov      edx, dword ptr [ebp + 8]
0x453C91: mov      byte ptr [edx - 0x406], al
0x453C97: mov      eax, dword ptr [ebp + 8]
0x453C9A: movzx    eax, byte ptr [eax - 0x403]
0x453CA1: shr      eax, 1
0x453CA3: mov      edx, dword ptr [ebp + 8]
0x453CA6: movzx    edx, byte ptr [edx - 0x407]
0x453CAD: shr      edx, 1
0x453CAF: add      al, dl
0x453CB1: mov      edx, dword ptr [ebp + 8]
0x453CB4: mov      byte ptr [edx - 0x407], al
0x453CBA: mov      eax, dword ptr [ebp + 8]
0x453CBD: movzx    eax, byte ptr [eax - 0x404]
0x453CC4: shr      eax, 1
0x453CC6: mov      edx, dword ptr [ebp + 8]
0x453CC9: movzx    edx, byte ptr [edx - 0x408]
0x453CD0: shr      edx, 1
0x453CD2: add      al, dl
0x453CD4: mov      edx, dword ptr [ebp + 8]
0x453CD7: mov      byte ptr [edx - 0x408], al
0x453CDD: mov      eax, dword ptr [ebp + 8]
0x453CE0: mov      dword ptr [eax - 0x40c], 0x300
0x453CEA: mov      eax, dword ptr [ebp + 8]
0x453CED: mov      byte ptr [eax - 0x40d], 0
0x453CF4: xor      eax, eax
0x453CF6: mov      dword ptr [ebp - 0xc], eax
0x453CF9: mov      eax, dword ptr [ebp + 8]
0x453CFC: mov      edx, dword ptr [ebp - 0xc]
0x453CFF: mov      eax, dword ptr [eax + edx*4 - 0x400]
0x453D06: mov      edx, dword ptr [ebp + 8]
0x453D09: mov      dword ptr [edx - 0x411], eax
0x453D0F: mov      eax, dword ptr [ebp + 8]
0x453D12: movzx    eax, byte ptr [eax - 0x40f]
0x453D19: mov      edx, dword ptr [ebp + 8]
0x453D1C: movzx    edx, byte ptr [edx - 0x406]
0x453D23: sub      eax, edx
0x453D25: cdq      
0x453D26: xor      eax, edx
0x453D28: sub      eax, edx
0x453D2A: mov      ecx, eax
0x453D2C: mov      eax, dword ptr [ebp + 8]
0x453D2F: movzx    eax, byte ptr [eax - 0x410]
0x453D36: mov      edx, dword ptr [ebp + 8]
0x453D39: movzx    edx, byte ptr [edx - 0x407]
0x453D40: sub      eax, edx
0x453D42: cdq      
0x453D43: xor      eax, edx
0x453D45: sub      eax, edx
0x453D47: add      ecx, eax
0x453D49: mov      eax, dword ptr [ebp + 8]
0x453D4C: movzx    eax, byte ptr [eax - 0x411]
0x453D53: mov      edx, dword ptr [ebp + 8]
0x453D56: movzx    edx, byte ptr [edx - 0x408]
0x453D5D: sub      eax, edx
0x453D5F: cdq      
0x453D60: xor      eax, edx
0x453D62: sub      eax, edx
0x453D64: add      ecx, eax
0x453D66: mov      eax, dword ptr [ebp + 8]
0x453D69: mov      dword ptr [eax - 0x418], ecx
0x453D6F: mov      eax, dword ptr [ebp + 8]
0x453D72: mov      eax, dword ptr [eax - 0x418]
0x453D78: mov      edx, dword ptr [ebp + 8]
0x453D7B: cmp      eax, dword ptr [edx - 0x40c]
0x453D81: jge      0x453da1
0x453D83: mov      eax, dword ptr [ebp + 8]
0x453D86: mov      eax, dword ptr [eax - 0x418]
0x453D8C: mov      edx, dword ptr [ebp + 8]
0x453D8F: mov      dword ptr [edx - 0x40c], eax
0x453D95: mov      al, byte ptr [ebp - 0xc]
0x453D98: mov      edx, dword ptr [ebp + 8]
0x453D9B: mov      byte ptr [edx - 0x40d], al
0x453DA1: inc      dword ptr [ebp - 0xc]
0x453DA4: cmp      dword ptr [ebp - 0xc], 0x100
0x453DAB: jne      0x453cf9
0x453DB1: mov      eax, dword ptr [ebp - 4]
0x453DB4: shl      eax, 5
0x453DB7: lea      eax, [eax*8 + 0x4a729c]
0x453DBE: mov      edx, dword ptr [ebp - 8]
0x453DC1: mov      ecx, dword ptr [ebp + 8]
0x453DC4: mov      cl, byte ptr [ecx - 0x40d]
0x453DCA: mov      byte ptr [eax + edx], cl
0x453DCD: inc      dword ptr [ebp - 8]
0x453DD0: cmp      dword ptr [ebp - 8], 0x100
0x453DD7: jne      0x453c5e
0x453DDD: inc      dword ptr [ebp - 4]
0x453DE0: cmp      dword ptr [ebp - 4], 0x100
0x453DE7: jne      0x453c43
0x453DED: mov      esp, ebp
0x453DEF: pop      ebp
0x453DF0: ret      
0x453DF1: lea      eax, [eax]
0x453DF4: push     ebp
0x453DF5: mov      ebp, esp
0x453DF7: add      esp, -0x1c
0x453DFA: xor      eax, eax
0x453DFC: mov      dword ptr [ebp - 4], eax
0x453DFF: mov      eax, dword ptr [ebp + 8]
0x453E02: mov      edx, dword ptr [ebp - 4]
0x453E05: mov      eax, dword ptr [eax + edx*4 - 0x400]
0x453E0C: mov      edx, dword ptr [ebp + 8]
0x453E0F: mov      dword ptr [edx - 0x404], eax
0x453E15: xor      eax, eax
0x453E17: mov      dword ptr [ebp - 8], eax
0x453E1A: mov      eax, dword ptr [ebp + 8]
0x453E1D: mov      edx, dword ptr [ebp - 8]
0x453E20: mov      eax, dword ptr [eax + edx*4 - 0x400]
0x453E27: mov      edx, dword ptr [ebp + 8]
0x453E2A: mov      dword ptr [edx - 0x408], eax
0x453E30: mov      eax, dword ptr [ebp + 8]
0x453E33: xor      edx, edx
0x453E35: mov      dl, byte ptr [eax - 0x403]
0x453E3B: mov      eax, dword ptr [ebp + 8]
0x453E3E: movzx    eax, byte ptr [eax - 0x402]
0x453E45: call     0x452bbc
0x453E4A: mov      dword ptr [ebp - 0x10], eax
0x453E4D: mov      eax, dword ptr [ebp + 8]
0x453E50: xor      edx, edx
0x453E52: mov      dl, byte ptr [eax - 0x404]
0x453E58: mov      eax, dword ptr [ebp - 0x10]
0x453E5B: call     0x452bbc
0x453E60: mov      dword ptr [ebp - 0x10], eax
0x453E63: mov      eax, dword ptr [ebp + 8]
0x453E66: movzx    eax, byte ptr [eax - 0x402]
0x453E6D: mov      edx, 0xff
0x453E72: sub      edx, eax
0x453E74: mov      dword ptr [ebp - 0x14], edx
0x453E77: fild     dword ptr [ebp - 0x14]
0x453E7A: fdiv     dword ptr [0x454088]
0x453E80: mov      eax, dword ptr [ebp + 8]
0x453E83: movzx    eax, byte ptr [eax - 0x406]
0x453E8A: mov      dword ptr [ebp - 0x18], eax
0x453E8D: fild     dword ptr [ebp - 0x18]
0x453E90: fmulp    st(1)
0x453E92: mov      eax, dword ptr [ebp + 8]
0x453E95: movzx    eax, byte ptr [eax - 0x402]
0x453E9C: mov      dword ptr [ebp - 0x1c], eax
0x453E9F: fild     dword ptr [ebp - 0x1c]
0x453EA2: faddp    st(1)
0x453EA4: call     0x4029a0
0x453EA9: mov      edx, eax
0x453EAB: mov      eax, 0xff
0x453EB0: call     0x452b90
0x453EB5: mov      edx, dword ptr [ebp + 8]
0x453EB8: mov      byte ptr [edx - 0x406], al
0x453EBE: mov      eax, dword ptr [ebp + 8]
0x453EC1: movzx    eax, byte ptr [eax - 0x403]
0x453EC8: mov      edx, 0xff
0x453ECD: sub      edx, eax
0x453ECF: mov      dword ptr [ebp - 0x14], edx
0x453ED2: fild     dword ptr [ebp - 0x14]
0x453ED5: fdiv     dword ptr [0x454088]
0x453EDB: mov      eax, dword ptr [ebp + 8]
0x453EDE: movzx    eax, byte ptr [eax - 0x407]
0x453EE5: mov      dword ptr [ebp - 0x18], eax
0x453EE8: fild     dword ptr [ebp - 0x18]
0x453EEB: fmulp    st(1)
0x453EED: mov      eax, dword ptr [ebp + 8]
0x453EF0: movzx    eax, byte ptr [eax - 0x403]
0x453EF7: mov      dword ptr [ebp - 0x1c], eax
0x453EFA: fild     dword ptr [ebp - 0x1c]
0x453EFD: faddp    st(1)
0x453EFF: call     0x4029a0
0x453F04: mov      edx, eax
0x453F06: mov      eax, 0xff
0x453F0B: call     0x452b90
0x453F10: mov      edx, dword ptr [ebp + 8]
0x453F13: mov      byte ptr [edx - 0x407], al
0x453F19: mov      eax, dword ptr [ebp + 8]
0x453F1C: movzx    eax, byte ptr [eax - 0x404]
0x453F23: mov      edx, 0xff
0x453F28: sub      edx, eax
0x453F2A: mov      dword ptr [ebp - 0x14], edx
0x453F2D: fild     dword ptr [ebp - 0x14]
0x453F30: fdiv     dword ptr [0x454088]
0x453F36: mov      eax, dword ptr [ebp + 8]
0x453F39: movzx    eax, byte ptr [eax - 0x408]
0x453F40: mov      dword ptr [ebp - 0x18], eax
0x453F43: fild     dword ptr [ebp - 0x18]
0x453F46: fmulp    st(1)
0x453F48: mov      eax, dword ptr [ebp + 8]
0x453F4B: movzx    eax, byte ptr [eax - 0x404]
0x453F52: mov      dword ptr [ebp - 0x1c], eax
0x453F55: fild     dword ptr [ebp - 0x1c]
0x453F58: faddp    st(1)
0x453F5A: call     0x4029a0
0x453F5F: mov      edx, eax
0x453F61: mov      eax, 0xff
0x453F66: call     0x452b90
0x453F6B: mov      edx, dword ptr [ebp + 8]
0x453F6E: mov      byte ptr [edx - 0x408], al
0x453F74: mov      eax, dword ptr [ebp + 8]
0x453F77: mov      dword ptr [eax - 0x40c], 0x300
0x453F81: mov      eax, dword ptr [ebp + 8]
0x453F84: mov      byte ptr [eax - 0x40d], 0
0x453F8B: xor      eax, eax
0x453F8D: mov      dword ptr [ebp - 0xc], eax
0x453F90: mov      eax, dword ptr [ebp + 8]
0x453F93: mov      edx, dword ptr [ebp - 0xc]
0x453F96: mov      eax, dword ptr [eax + edx*4 - 0x400]
0x453F9D: mov      edx, dword ptr [ebp + 8]
0x453FA0: mov      dword ptr [edx - 0x411], eax
0x453FA6: mov      eax, dword ptr [ebp + 8]
0x453FA9: movzx    eax, byte ptr [eax - 0x40f]
0x453FB0: mov      edx, dword ptr [ebp + 8]
0x453FB3: movzx    edx, byte ptr [edx - 0x406]
0x453FBA: sub      eax, edx
0x453FBC: cdq      
0x453FBD: xor      eax, edx
0x453FBF: sub      eax, edx
0x453FC1: mov      ecx, eax
0x453FC3: mov      eax, dword ptr [ebp + 8]
0x453FC6: movzx    eax, byte ptr [eax - 0x410]
0x453FCD: mov      edx, dword ptr [ebp + 8]
0x453FD0: movzx    edx, byte ptr [edx - 0x407]
0x453FD7: sub      eax, edx
0x453FD9: cdq      
0x453FDA: xor      eax, edx
0x453FDC: sub      eax, edx
0x453FDE: add      ecx, eax
0x453FE0: mov      eax, dword ptr [ebp + 8]
0x453FE3: movzx    eax, byte ptr [eax - 0x411]
0x453FEA: mov      edx, dword ptr [ebp + 8]
0x453FED: movzx    edx, byte ptr [edx - 0x408]
0x453FF4: sub      eax, edx
0x453FF6: cdq      
0x453FF7: xor      eax, edx
0x453FF9: sub      eax, edx
0x453FFB: add      ecx, eax
0x453FFD: mov      eax, dword ptr [ebp + 8]
0x454000: mov      dword ptr [eax - 0x418], ecx
0x454006: mov      eax, dword ptr [ebp + 8]
0x454009: mov      eax, dword ptr [eax - 0x418]
0x45400F: mov      edx, dword ptr [ebp + 8]
0x454012: cmp      eax, dword ptr [edx - 0x40c]
0x454018: jge      0x454038
0x45401A: mov      eax, dword ptr [ebp + 8]
0x45401D: mov      eax, dword ptr [eax - 0x418]
0x454023: mov      edx, dword ptr [ebp + 8]
0x454026: mov      dword ptr [edx - 0x40c], eax
0x45402C: mov      al, byte ptr [ebp - 0xc]
0x45402F: mov      edx, dword ptr [ebp + 8]
0x454032: mov      byte ptr [edx - 0x40d], al
0x454038: inc      dword ptr [ebp - 0xc]
0x45403B: cmp      dword ptr [ebp - 0xc], 0x100
0x454042: jne      0x453f90
0x454048: mov      eax, dword ptr [ebp - 4]
0x45404B: shl      eax, 5
0x45404E: lea      eax, [eax*8 + 0x4b729c]
0x454055: mov      edx, dword ptr [ebp - 8]
0x454058: mov      ecx, dword ptr [ebp + 8]
0x45405B: mov      cl, byte ptr [ecx - 0x40d]
0x454061: mov      byte ptr [eax + edx], cl
0x454064: inc      dword ptr [ebp - 8]
0x454067: cmp      dword ptr [ebp - 8], 0x100
0x45406E: jne      0x453e1a
0x454074: inc      dword ptr [ebp - 4]
0x454077: cmp      dword ptr [ebp - 4], 0x100
0x45407E: jne      0x453dff
0x454084: mov      esp, ebp
0x454086: pop      ebp
0x454087: ret      
0x454088: add      byte ptr [eax], al
0x45408A: jg       0x4540cf
0x45408C: push     ebp
0x45408D: mov      ebp, esp
0x45408F: add      esp, -0x1c
0x454092: xor      eax, eax
0x454094: mov      dword ptr [ebp - 4], eax
0x454097: xor      eax, eax
0x454099: mov      dword ptr [ebp - 8], eax
0x45409C: mov      eax, dword ptr [ebp + 8]
0x45409F: mov      edx, dword ptr [ebp - 8]
0x4540A2: mov      eax, dword ptr [eax + edx*4 - 0x400]
0x4540A9: mov      edx, dword ptr [ebp + 8]
0x4540AC: mov      dword ptr [edx - 0x408], eax
0x4540B2: mov      eax, dword ptr [ebp + 8]
0x4540B5: movzx    eax, byte ptr [eax - 0x406]
0x4540BC: mov      edx, dword ptr [ebp - 4]
0x4540BF: inc      edx
0x4540C0: imul     edx
0x4540C2: mov      dword ptr [ebp - 0x1c], eax
0x4540C5: fild     dword ptr [ebp - 0x1c]
0x4540C8: fdiv     dword ptr [0x4546b8]
0x4540CE: call     0x4029a0
0x4540D3: sub      eax, 5
0x4540D6: mov      edx, 0xff
0x4540DB: call     0x452b90
0x4540E0: mov      dword ptr [ebp - 0x10], eax
0x4540E3: mov      eax, dword ptr [ebp + 8]
0x4540E6: movzx    eax, byte ptr [eax - 0x407]
0x4540ED: mov      edx, dword ptr [ebp - 4]
0x4540F0: inc      edx
0x4540F1: imul     edx
0x4540F3: mov      dword ptr [ebp - 0x1c], eax
0x4540F6: fild     dword ptr [ebp - 0x1c]
0x4540F9: fdiv     dword ptr [0x4546b8]
0x4540FF: call     0x4029a0
0x454104: sub      eax, 5
0x454107: mov      edx, 0xff
0x45410C: call     0x452b90
0x454111: mov      dword ptr [ebp - 0x14], eax
0x454114: mov      eax, dword ptr [ebp + 8]
0x454117: movzx    eax, byte ptr [eax - 0x408]
0x45411E: mov      edx, dword ptr [ebp - 4]
0x454121: inc      edx
0x454122: imul     edx
0x454124: mov      dword ptr [ebp - 0x1c], eax
0x454127: fild     dword ptr [ebp - 0x1c]
0x45412A: fdiv     dword ptr [0x4546b8]
0x454130: call     0x4029a0
0x454135: sub      eax, 5
0x454138: mov      edx, 0xff
0x45413D: call     0x452b90
0x454142: mov      dword ptr [ebp - 0x18], eax
0x454145: mov      edx, dword ptr [ebp - 0x10]
0x454148: xor      eax, eax
0x45414A: call     0x452bbc
0x45414F: mov      edx, dword ptr [ebp + 8]
0x454152: mov      byte ptr [edx - 0x406], al
0x454158: mov      edx, dword ptr [ebp - 0x14]
0x45415B: xor      eax, eax
0x45415D: call     0x452bbc
0x454162: mov      edx, dword ptr [ebp + 8]
0x454165: mov      byte ptr [edx - 0x407], al
0x45416B: mov      edx, dword ptr [ebp - 0x18]
0x45416E: xor      eax, eax
0x454170: call     0x452bbc
0x454175: mov      edx, dword ptr [ebp + 8]
0x454178: mov      byte ptr [edx - 0x408], al
0x45417E: mov      eax, dword ptr [ebp + 8]
0x454181: mov      dword ptr [eax - 0x40c], 0x300
0x45418B: mov      eax, dword ptr [ebp + 8]
0x45418E: mov      byte ptr [eax - 0x40d], 0
0x454195: xor      eax, eax
0x454197: mov      dword ptr [ebp - 0xc], eax
0x45419A: mov      eax, dword ptr [ebp + 8]
0x45419D: mov      edx, dword ptr [ebp - 0xc]
0x4541A0: mov      eax, dword ptr [eax + edx*4 - 0x400]
0x4541A7: mov      edx, dword ptr [ebp + 8]
0x4541AA: mov      dword ptr [edx - 0x411], eax
0x4541B0: mov      eax, dword ptr [ebp + 8]
0x4541B3: movzx    eax, byte ptr [eax - 0x40f]
0x4541BA: mov      edx, dword ptr [ebp + 8]
0x4541BD: movzx    edx, byte ptr [edx - 0x406]
0x4541C4: sub      eax, edx
0x4541C6: cdq      
0x4541C7: xor      eax, edx
0x4541C9: sub      eax, edx
0x4541CB: mov      ecx, eax
0x4541CD: mov      eax, dword ptr [ebp + 8]
0x4541D0: movzx    eax, byte ptr [eax - 0x410]
0x4541D7: mov      edx, dword ptr [ebp + 8]
0x4541DA: movzx    edx, byte ptr [edx - 0x407]
0x4541E1: sub      eax, edx
0x4541E3: cdq      
0x4541E4: xor      eax, edx
0x4541E6: sub      eax, edx
0x4541E8: add      ecx, eax
0x4541EA: mov      eax, dword ptr [ebp + 8]
0x4541ED: movzx    eax, byte ptr [eax - 0x411]
0x4541F4: mov      edx, dword ptr [ebp + 8]
0x4541F7: movzx    edx, byte ptr [edx - 0x408]
0x4541FE: sub      eax, edx
0x454200: cdq      
0x454201: xor      eax, edx
0x454203: sub      eax, edx
0x454205: add      ecx, eax
0x454207: mov      eax, dword ptr [ebp + 8]
0x45420A: mov      dword ptr [eax - 0x418], ecx
0x454210: mov      eax, dword ptr [ebp + 8]
0x454213: mov      eax, dword ptr [eax - 0x418]
0x454219: mov      edx, dword ptr [ebp + 8]
0x45421C: cmp      eax, dword ptr [edx - 0x40c]
0x454222: jge      0x454242
0x454224: mov      eax, dword ptr [ebp + 8]
0x454227: mov      eax, dword ptr [eax - 0x418]
0x45422D: mov      edx, dword ptr [ebp + 8]
0x454230: mov      dword ptr [edx - 0x40c], eax
0x454236: mov      al, byte ptr [ebp - 0xc]
0x454239: mov      edx, dword ptr [ebp + 8]
0x45423C: mov      byte ptr [edx - 0x40d], al
0x454242: inc      dword ptr [ebp - 0xc]
0x454245: cmp      dword ptr [ebp - 0xc], 0x100
0x45424C: jne      0x45419a
0x454252: mov      eax, dword ptr [ebp - 4]
0x454255: shl      eax, 5
0x454258: lea      eax, [eax*8 + 0x4c729c]
0x45425F: mov      edx, dword ptr [ebp - 8]
0x454262: mov      ecx, dword ptr [ebp + 8]
0x454265: mov      cl, byte ptr [ecx - 0x40d]
0x45426B: mov      byte ptr [eax + edx], cl
0x45426E: inc      dword ptr [ebp - 8]
0x454271: cmp      dword ptr [ebp - 8], 0x100
0x454278: jne      0x45409c
0x45427E: inc      dword ptr [ebp - 4]
0x454281: cmp      dword ptr [ebp - 4], 0x1f
0x454285: jne      0x454097
0x45428B: xor      eax, eax
0x45428D: mov      dword ptr [ebp - 4], eax
0x454290: xor      eax, eax
0x454292: mov      dword ptr [ebp - 8], eax
0x454295: mov      eax, dword ptr [ebp + 8]
0x454298: mov      edx, dword ptr [ebp - 8]
0x45429B: mov      eax, dword ptr [eax + edx*4 - 0x400]
0x4542A2: mov      edx, dword ptr [ebp + 8]
0x4542A5: mov      dword ptr [edx - 0x408], eax
0x4542AB: mov      eax, dword ptr [ebp + 8]
0x4542AE: movzx    eax, byte ptr [eax - 0x406]
0x4542B5: mov      edx, dword ptr [ebp - 4]
0x4542B8: lea      edx, [edx + edx*2]
0x4542BB: add      edx, 0x2f
0x4542BE: imul     edx
0x4542C0: mov      dword ptr [ebp - 0x1c], eax
0x4542C3: fild     dword ptr [ebp - 0x1c]
0x4542C6: fdiv     dword ptr [0x4546bc]
0x4542CC: call     0x4029a0
0x4542D1: mov      edx, 0xff
0x4542D6: call     0x452b90
0x4542DB: mov      edx, dword ptr [ebp + 8]
0x4542DE: mov      byte ptr [edx - 0x406], al
0x4542E4: mov      eax, dword ptr [ebp + 8]
0x4542E7: movzx    eax, byte ptr [eax - 0x407]
0x4542EE: mov      edx, dword ptr [ebp - 4]
0x4542F1: lea      edx, [edx + edx*2]
0x4542F4: add      edx, 0x2f
0x4542F7: imul     edx
0x4542F9: mov      dword ptr [ebp - 0x1c], eax
0x4542FC: fild     dword ptr [ebp - 0x1c]
0x4542FF: fdiv     dword ptr [0x4546bc]
0x454305: call     0x4029a0
0x45430A: mov      edx, 0xff
0x45430F: call     0x452b90
0x454314: mov      edx, dword ptr [ebp + 8]
0x454317: mov      byte ptr [edx - 0x407], al
0x45431D: mov      eax, dword ptr [ebp + 8]
0x454320: movzx    eax, byte ptr [eax - 0x408]
0x454327: mov      edx, dword ptr [ebp - 4]
0x45432A: lea      edx, [edx + edx*2]
0x45432D: add      edx, 0x2f
0x454330: imul     edx
0x454332: mov      dword ptr [ebp - 0x1c], eax
0x454335: fild     dword ptr [ebp - 0x1c]
0x454338: fdiv     dword ptr [0x4546bc]
0x45433E: call     0x4029a0
0x454343: mov      edx, 0xff
0x454348: call     0x452b90
0x45434D: mov      edx, dword ptr [ebp + 8]
0x454350: mov      byte ptr [edx - 0x408], al
0x454356: mov      eax, dword ptr [ebp + 8]
0x454359: mov      dword ptr [eax - 0x40c], 0x300
0x454363: mov      eax, dword ptr [ebp + 8]
0x454366: mov      byte ptr [eax - 0x40d], 0
0x45436D: xor      eax, eax
0x45436F: mov      dword ptr [ebp - 0xc], eax
0x454372: mov      eax, dword ptr [ebp + 8]
0x454375: mov      edx, dword ptr [ebp - 0xc]
0x454378: mov      eax, dword ptr [eax + edx*4 - 0x400]
0x45437F: mov      edx, dword ptr [ebp + 8]
0x454382: mov      dword ptr [edx - 0x411], eax
0x454388: mov      eax, dword ptr [ebp + 8]
0x45438B: movzx    eax, byte ptr [eax - 0x40f]
0x454392: mov      edx, dword ptr [ebp + 8]
0x454395: movzx    edx, byte ptr [edx - 0x406]
0x45439C: sub      eax, edx
0x45439E: cdq      
0x45439F: xor      eax, edx
0x4543A1: sub      eax, edx
0x4543A3: mov      ecx, eax
0x4543A5: mov      eax, dword ptr [ebp + 8]
0x4543A8: movzx    eax, byte ptr [eax - 0x410]
0x4543AF: mov      edx, dword ptr [ebp + 8]
0x4543B2: movzx    edx, byte ptr [edx - 0x407]
0x4543B9: sub      eax, edx
0x4543BB: cdq      
0x4543BC: xor      eax, edx
0x4543BE: sub      eax, edx
0x4543C0: add      ecx, eax
0x4543C2: mov      eax, dword ptr [ebp + 8]
0x4543C5: movzx    eax, byte ptr [eax - 0x411]
0x4543CC: mov      edx, dword ptr [ebp + 8]
0x4543CF: movzx    edx, byte ptr [edx - 0x408]
0x4543D6: sub      eax, edx
0x4543D8: cdq      
0x4543D9: xor      eax, edx
0x4543DB: sub      eax, edx
0x4543DD: add      ecx, eax
0x4543DF: mov      eax, dword ptr [ebp + 8]
0x4543E2: mov      dword ptr [eax - 0x418], ecx
0x4543E8: mov      eax, dword ptr [ebp + 8]
0x4543EB: mov      eax, dword ptr [eax - 0x418]
0x4543F1: mov      edx, dword ptr [ebp + 8]
0x4543F4: cmp      eax, dword ptr [edx - 0x40c]
0x4543FA: jge      0x45441a
0x4543FC: mov      eax, dword ptr [ebp + 8]
0x4543FF: mov      eax, dword ptr [eax - 0x418]
0x454405: mov      edx, dword ptr [ebp + 8]
0x454408: mov      dword ptr [edx - 0x40c], eax
0x45440E: mov      al, byte ptr [ebp - 0xc]
0x454411: mov      edx, dword ptr [ebp + 8]
0x454414: mov      byte ptr [edx - 0x40d], al
0x45441A: inc      dword ptr [ebp - 0xc]
0x45441D: cmp      dword ptr [ebp - 0xc], 0x100
0x454424: jne      0x454372
0x45442A: mov      eax, dword ptr [ebp - 4]
0x45442D: shl      eax, 5
0x454430: lea      eax, [eax*8 + 0x4d729c]
0x454437: mov      edx, dword ptr [ebp - 8]
0x45443A: mov      ecx, dword ptr [ebp + 8]
0x45443D: mov      cl, byte ptr [ecx - 0x40d]
0x454443: mov      byte ptr [eax + edx], cl
0x454446: inc      dword ptr [ebp - 8]
0x454449: cmp      dword ptr [ebp - 8], 0x100
0x454450: jne      0x454295
0x454456: inc      dword ptr [ebp - 4]
0x454459: cmp      dword ptr [ebp - 4], 0x1f
0x45445D: jne      0x454290
0x454463: xor      eax, eax
0x454465: mov      dword ptr [ebp - 4], eax
0x454468: xor      eax, eax
0x45446A: mov      dword ptr [ebp - 8], eax
0x45446D: mov      eax, dword ptr [ebp + 8]
0x454470: mov      edx, dword ptr [ebp - 8]
0x454473: mov      eax, dword ptr [eax + edx*4 - 0x400]
0x45447A: mov      edx, dword ptr [ebp + 8]
0x45447D: mov      dword ptr [edx - 0x408], eax
0x454483: mov      eax, dword ptr [ebp + 8]
0x454486: movzx    eax, byte ptr [eax - 0x406]
0x45448D: mov      edx, dword ptr [ebp - 4]
0x454490: lea      edx, [edx + edx*2]
0x454493: add      edx, 0x78
0x454496: imul     edx
0x454498: mov      dword ptr [ebp - 0x1c], eax
0x45449B: fild     dword ptr [ebp - 0x1c]
0x45449E: fdiv     dword ptr [0x4546c0]
0x4544A4: call     0x4029a0
0x4544A9: mov      edx, 0xff
0x4544AE: call     0x452b90
0x4544B3: mov      edx, dword ptr [ebp + 8]
0x4544B6: mov      byte ptr [edx - 0x406], al
0x4544BC: mov      eax, dword ptr [ebp + 8]
0x4544BF: movzx    eax, byte ptr [eax - 0x407]
0x4544C6: mov      edx, dword ptr [ebp - 4]
0x4544C9: lea      edx, [edx + edx*2]
0x4544CC: add      edx, 0x78
0x4544CF: imul     edx
0x4544D1: mov      dword ptr [ebp - 0x1c], eax
0x4544D4: fild     dword ptr [ebp - 0x1c]
0x4544D7: fdiv     dword ptr [0x4546c0]
0x4544DD: call     0x4029a0
0x4544E2: mov      edx, 0xff
0x4544E7: call     0x452b90
0x4544EC: mov      edx, dword ptr [ebp + 8]
0x4544EF: mov      byte ptr [edx - 0x407], al
0x4544F5: mov      eax, dword ptr [ebp + 8]
0x4544F8: movzx    eax, byte ptr [eax - 0x408]
0x4544FF: mov      edx, dword ptr [ebp - 4]
0x454502: lea      edx, [edx + edx*2]
0x454505: add      edx, 0x78
0x454508: imul     edx
0x45450A: mov      dword ptr [ebp - 0x1c], eax
0x45450D: fild     dword ptr [ebp - 0x1c]
0x454510: fdiv     dword ptr [0x4546c0]
0x454516: call     0x4029a0
0x45451B: mov      edx, 0xff
0x454520: call     0x452b90
0x454525: mov      edx, dword ptr [ebp + 8]
0x454528: mov      byte ptr [edx - 0x408], al
0x45452E: mov      eax, dword ptr [ebp + 8]
0x454531: mov      dword ptr [eax - 0x40c], 0x300
0x45453B: mov      eax, dword ptr [ebp + 8]
0x45453E: mov      byte ptr [eax - 0x40d], 0
0x454545: xor      eax, eax
0x454547: mov      dword ptr [ebp - 0xc], eax
0x45454A: mov      eax, dword ptr [ebp + 8]
0x45454D: mov      edx, dword ptr [ebp - 0xc]
0x454550: mov      eax, dword ptr [eax + edx*4 - 0x400]
0x454557: mov      edx, dword ptr [ebp + 8]
0x45455A: mov      dword ptr [edx - 0x411], eax
0x454560: mov      eax, dword ptr [ebp + 8]
0x454563: movzx    eax, byte ptr [eax - 0x40f]
0x45456A: mov      edx, dword ptr [ebp + 8]
0x45456D: movzx    edx, byte ptr [edx - 0x406]
0x454574: sub      eax, edx
0x454576: cdq      
0x454577: xor      eax, edx
0x454579: sub      eax, edx
0x45457B: mov      ecx, eax
0x45457D: mov      eax, dword ptr [ebp + 8]
0x454580: movzx    eax, byte ptr [eax - 0x410]
0x454587: mov      edx, dword ptr [ebp + 8]
0x45458A: movzx    edx, byte ptr [edx - 0x407]
0x454591: sub      eax, edx
0x454593: cdq      
0x454594: xor      eax, edx
0x454596: sub      eax, edx
0x454598: add      ecx, eax
0x45459A: mov      eax, dword ptr [ebp + 8]
0x45459D: movzx    eax, byte ptr [eax - 0x411]
0x4545A4: mov      edx, dword ptr [ebp + 8]
0x4545A7: movzx    edx, byte ptr [edx - 0x408]
0x4545AE: sub      eax, edx
0x4545B0: cdq      
0x4545B1: xor      eax, edx
0x4545B3: sub      eax, edx
0x4545B5: add      ecx, eax
0x4545B7: mov      eax, dword ptr [ebp + 8]
0x4545BA: mov      dword ptr [eax - 0x418], ecx
0x4545C0: mov      eax, dword ptr [ebp + 8]
0x4545C3: mov      eax, dword ptr [eax - 0x418]
0x4545C9: mov      edx, dword ptr [ebp + 8]
0x4545CC: cmp      eax, dword ptr [edx - 0x40c]
0x4545D2: jge      0x4545f2
0x4545D4: mov      eax, dword ptr [ebp + 8]
0x4545D7: mov      eax, dword ptr [eax - 0x418]
0x4545DD: mov      edx, dword ptr [ebp + 8]
0x4545E0: mov      dword ptr [edx - 0x40c], eax
0x4545E6: mov      al, byte ptr [ebp - 0xc]
0x4545E9: mov      edx, dword ptr [ebp + 8]
0x4545EC: mov      byte ptr [edx - 0x40d], al
0x4545F2: inc      dword ptr [ebp - 0xc]
0x4545F5: cmp      dword ptr [ebp - 0xc], 0x100
0x4545FC: jne      0x45454a
0x454602: mov      eax, dword ptr [ebp - 4]
0x454605: shl      eax, 5
0x454608: lea      eax, [eax*8 + 0x4e729c]
0x45460F: mov      edx, dword ptr [ebp - 8]
0x454612: mov      ecx, dword ptr [ebp + 8]
0x454615: mov      cl, byte ptr [ecx - 0x40d]
0x45461B: mov      byte ptr [eax + edx], cl
0x45461E: inc      dword ptr [ebp - 8]
0x454621: cmp      dword ptr [ebp - 8], 0x100
0x454628: jne      0x45446d
0x45462E: inc      dword ptr [ebp - 4]
0x454631: cmp      dword ptr [ebp - 4], 0x1f
0x454635: jne      0x454468
0x45463B: mov      dword ptr [ebp - 4], 0x1f
0x454642: xor      eax, eax
0x454644: mov      dword ptr [ebp - 8], eax
0x454647: mov      eax, dword ptr [ebp - 4]
0x45464A: shl      eax, 5
0x45464D: lea      eax, [eax*8 + 0x4c729c]
0x454654: mov      edx, dword ptr [ebp - 8]
0x454657: mov      ecx, dword ptr [ebp - 8]
0x45465A: mov      cl, byte ptr [ecx + 0x4c909c]
0x454660: mov      byte ptr [eax + edx], cl
0x454663: mov      eax, dword ptr [ebp - 4]
0x454666: shl      eax, 5
0x454669: lea      eax, [eax*8 + 0x4d729c]
0x454670: mov      edx, dword ptr [ebp - 8]
0x454673: mov      ecx, dword ptr [ebp - 8]
0x454676: mov      cl, byte ptr [ecx + 0x4d909c]
0x45467C: mov      byte ptr [eax + edx], cl
0x45467F: mov      eax, dword ptr [ebp - 4]
0x454682: shl      eax, 5
0x454685: lea      eax, [eax*8 + 0x4e729c]
0x45468C: mov      edx, dword ptr [ebp - 8]
0x45468F: mov      ecx, dword ptr [ebp - 8]
0x454692: mov      cl, byte ptr [ecx + 0x4e909c]
0x454698: mov      byte ptr [eax + edx], cl
0x45469B: inc      dword ptr [ebp - 8]
0x45469E: cmp      dword ptr [ebp - 8], 0x100
0x4546A5: jne      0x454647
0x4546A7: inc      dword ptr [ebp - 4]
0x4546AA: cmp      dword ptr [ebp - 4], 0x100
0x4546B1: jne      0x454642
0x4546B3: mov      esp, ebp
0x4546B5: pop      ebp
0x4546B6: ret      
0x4546B7: add      byte ptr [eax], al
0x4546B9: add      al, bh
0x4546BB: inc      ecx
0x4546BC: add      byte ptr [eax], al
0x4546BE: or       al, 0x43
0x4546C0: add      byte ptr [eax], al
0x4546C2: push     esi
0x4546C3: inc      ebx
0x4546C4: push     ebp
0x4546C5: mov      ebp, esp
0x4546C7: add      esp, 0xfffffbcc
0x4546CD: push     esi
0x4546CE: push     edi
0x4546CF: mov      esi, eax
0x4546D1: lea      edi, [ebp - 0x400]
0x4546D7: mov      ecx, 0x100
0x4546DC: rep movsd dword ptr es:[edi], dword ptr [esi]
0x4546DE: push     ebp
0x4546DF: call     0x453c38
0x4546E4: pop      ecx
0x4546E5: push     ebp
0x4546E6: call     0x453df4
0x4546EB: pop      ecx
0x4546EC: push     ebp
0x4546ED: call     0x45408c
0x4546F2: pop      ecx
0x4546F3: pop      edi
0x4546F4: pop      esi
0x4546F5: mov      esp, ebp
0x4546F7: pop      ebp
0x4546F8: ret      
0x4546F9: lea      eax, [eax]
0x4546FC: push     ebp
0x4546FD: mov      ebp, esp
0x4546FF: add      esp, 0xfffffbdc
0x454705: push     esi
0x454706: push     edi
0x454707: mov      esi, eax
0x454709: lea      edi, [ebp - 0x41d]
0x45470F: mov      ecx, 0x100
0x454714: rep movsd dword ptr es:[edi], dword ptr [esi]
0x454716: mov      byte ptr [0x4f729c], 0
0x45471D: mov      dword ptr [ebp - 8], 1
0x454724: mov      eax, dword ptr [ebp - 8]
0x454727: mov      eax, dword ptr [ebp + eax*4 - 0x41d]
0x45472E: mov      dword ptr [ebp - 0x18], eax
0x454731: xor      eax, eax
0x454733: mov      al, byte ptr [ebp - 0x16]
0x454736: mov      dword ptr [ebp - 0x424], eax
0x45473C: fild     dword ptr [ebp - 0x424]
0x454742: fld      xword ptr [0x454f90]
0x454748: fmulp    st(1)
0x45474A: call     0x4029a0
0x45474F: mov      edx, 0xff
0x454754: call     0x452b90
0x454759: mov      byte ptr [ebp - 0x16], al
0x45475C: xor      eax, eax
0x45475E: mov      al, byte ptr [ebp - 0x17]
0x454761: mov      dword ptr [ebp - 0x424], eax
0x454767: fild     dword ptr [ebp - 0x424]
0x45476D: fld      xword ptr [0x454f90]
0x454773: fmulp    st(1)
0x454775: call     0x4029a0
0x45477A: mov      edx, 0xff
0x45477F: call     0x452b90
0x454784: mov      byte ptr [ebp - 0x17], al
0x454787: xor      eax, eax
0x454789: mov      al, byte ptr [ebp - 0x18]
0x45478C: mov      dword ptr [ebp - 0x424], eax
0x454792: fild     dword ptr [ebp - 0x424]
0x454798: fld      xword ptr [0x454f90]
0x45479E: fmulp    st(1)
0x4547A0: call     0x4029a0
0x4547A5: mov      edx, 0xff
0x4547AA: call     0x452b90
0x4547AF: mov      byte ptr [ebp - 0x18], al
0x4547B2: mov      dword ptr [ebp - 0x10], 0x300
0x4547B9: mov      byte ptr [ebp - 0x1d], 0
0x4547BD: mov      dword ptr [ebp - 0xc], 1
0x4547C4: mov      eax, dword ptr [ebp - 0xc]
0x4547C7: mov      eax, dword ptr [ebp + eax*4 - 0x41d]
0x4547CE: mov      dword ptr [ebp - 0x1c], eax
0x4547D1: xor      eax, eax
0x4547D3: mov      al, byte ptr [ebp - 0x1a]
0x4547D6: xor      edx, edx
0x4547D8: mov      dl, byte ptr [ebp - 0x16]
0x4547DB: sub      eax, edx
0x4547DD: cdq      
0x4547DE: xor      eax, edx
0x4547E0: sub      eax, edx
0x4547E2: mov      ecx, eax
0x4547E4: xor      eax, eax
0x4547E6: mov      al, byte ptr [ebp - 0x1b]
0x4547E9: xor      edx, edx
0x4547EB: mov      dl, byte ptr [ebp - 0x17]
0x4547EE: sub      eax, edx
0x4547F0: cdq      
0x4547F1: xor      eax, edx
0x4547F3: sub      eax, edx
0x4547F5: add      ecx, eax
0x4547F7: xor      eax, eax
0x4547F9: mov      al, byte ptr [ebp - 0x1c]
0x4547FC: xor      edx, edx
0x4547FE: mov      dl, byte ptr [ebp - 0x18]
0x454801: sub      eax, edx
0x454803: cdq      
0x454804: xor      eax, edx
0x454806: sub      eax, edx
0x454808: add      ecx, eax
0x45480A: mov      dword ptr [ebp - 0x14], ecx
0x45480D: mov      eax, dword ptr [ebp - 0x14]
0x454810: cmp      eax, dword ptr [ebp - 0x10]
0x454813: jge      0x454821
0x454815: mov      eax, dword ptr [ebp - 0x14]
0x454818: mov      dword ptr [ebp - 0x10], eax
0x45481B: mov      al, byte ptr [ebp - 0xc]
0x45481E: mov      byte ptr [ebp - 0x1d], al
0x454821: inc      dword ptr [ebp - 0xc]
0x454824: cmp      dword ptr [ebp - 0xc], 0x100
0x45482B: jne      0x4547c4
0x45482D: mov      eax, dword ptr [ebp - 8]
0x454830: mov      dl, byte ptr [ebp - 0x1d]
0x454833: mov      byte ptr [eax + 0x4f729c], dl
0x454839: inc      dword ptr [ebp - 8]
0x45483C: cmp      dword ptr [ebp - 8], 0x100
0x454843: jne      0x454724
0x454849: mov      byte ptr [0x4f739c], 0
0x454850: mov      dword ptr [ebp - 8], 1
0x454857: mov      eax, dword ptr [ebp - 8]
0x45485A: mov      eax, dword ptr [ebp + eax*4 - 0x41d]
0x454861: mov      dword ptr [ebp - 0x18], eax
0x454864: xor      eax, eax
0x454866: mov      al, byte ptr [ebp - 0x16]
0x454869: xor      edx, edx
0x45486B: mov      dl, byte ptr [ebp - 0x17]
0x45486E: add      eax, edx
0x454870: xor      edx, edx
0x454872: mov      dl, byte ptr [ebp - 0x18]
0x454875: add      eax, edx
0x454877: mov      ecx, 3
0x45487C: xor      edx, edx
0x45487E: div      ecx
0x454880: mov      dword ptr [ebp - 4], eax
0x454883: mov      al, byte ptr [ebp - 4]
0x454886: mov      byte ptr [ebp - 0x16], al
0x454889: mov      al, byte ptr [ebp - 4]
0x45488C: mov      byte ptr [ebp - 0x17], al
0x45488F: mov      al, byte ptr [ebp - 4]
0x454892: mov      byte ptr [ebp - 0x18], al
0x454895: mov      dword ptr [ebp - 0x10], 0x300
0x45489C: mov      byte ptr [ebp - 0x1d], 0
0x4548A0: mov      dword ptr [ebp - 0xc], 1
0x4548A7: mov      eax, dword ptr [ebp - 0xc]
0x4548AA: mov      eax, dword ptr [ebp + eax*4 - 0x41d]
0x4548B1: mov      dword ptr [ebp - 0x1c], eax
0x4548B4: xor      eax, eax
0x4548B6: mov      al, byte ptr [ebp - 0x1a]
0x4548B9: xor      edx, edx
0x4548BB: mov      dl, byte ptr [ebp - 0x16]
0x4548BE: sub      eax, edx
0x4548C0: cdq      
0x4548C1: xor      eax, edx
0x4548C3: sub      eax, edx
0x4548C5: mov      ecx, eax
0x4548C7: xor      eax, eax
0x4548C9: mov      al, byte ptr [ebp - 0x1b]
0x4548CC: xor      edx, edx
0x4548CE: mov      dl, byte ptr [ebp - 0x17]
0x4548D1: sub      eax, edx
0x4548D3: cdq      
0x4548D4: xor      eax, edx
0x4548D6: sub      eax, edx
0x4548D8: add      ecx, eax
0x4548DA: xor      eax, eax
0x4548DC: mov      al, byte ptr [ebp - 0x1c]
0x4548DF: xor      edx, edx
0x4548E1: mov      dl, byte ptr [ebp - 0x18]
0x4548E4: sub      eax, edx
0x4548E6: cdq      
0x4548E7: xor      eax, edx
0x4548E9: sub      eax, edx
0x4548EB: add      ecx, eax
0x4548ED: mov      dword ptr [ebp - 0x14], ecx
0x4548F0: mov      eax, dword ptr [ebp - 0x14]
0x4548F3: cmp      eax, dword ptr [ebp - 0x10]
0x4548F6: jge      0x454904
0x4548F8: mov      eax, dword ptr [ebp - 0x14]
0x4548FB: mov      dword ptr [ebp - 0x10], eax
0x4548FE: mov      al, byte ptr [ebp - 0xc]
0x454901: mov      byte ptr [ebp - 0x1d], al
0x454904: inc      dword ptr [ebp - 0xc]
0x454907: cmp      dword ptr [ebp - 0xc], 0x100
0x45490E: jne      0x4548a7
0x454910: mov      eax, dword ptr [ebp - 8]
0x454913: mov      dl, byte ptr [ebp - 0x1d]
0x454916: mov      byte ptr [eax + 0x4f739c], dl
0x45491C: inc      dword ptr [ebp - 8]
0x45491F: cmp      dword ptr [ebp - 8], 0x100
0x454926: jne      0x454857
0x45492C: mov      byte ptr [0x4f759c], 0
0x454933: mov      dword ptr [ebp - 8], 1
0x45493A: mov      eax, dword ptr [ebp - 8]
0x45493D: mov      eax, dword ptr [ebp + eax*4 - 0x41d]
0x454944: mov      dword ptr [ebp - 0x18], eax
0x454947: xor      eax, eax
0x454949: mov      al, byte ptr [ebp - 0x16]
0x45494C: xor      edx, edx
0x45494E: mov      dl, byte ptr [ebp - 0x17]
0x454951: add      eax, edx
0x454953: xor      edx, edx
0x454955: mov      dl, byte ptr [ebp - 0x18]
0x454958: add      eax, edx
0x45495A: mov      dword ptr [ebp - 0x424], eax
0x454960: fild     dword ptr [ebp - 0x424]
0x454966: fdiv     dword ptr [0x454f9c]
0x45496C: fld      xword ptr [0x454fa0]
0x454972: fmulp    st(1)
0x454974: call     0x4029a0
0x454979: mov      dword ptr [ebp - 4], eax
0x45497C: mov      al, byte ptr [ebp - 4]
0x45497F: mov      byte ptr [ebp - 0x16], al
0x454982: mov      al, byte ptr [ebp - 4]
0x454985: mov      byte ptr [ebp - 0x17], al
0x454988: mov      al, byte ptr [ebp - 4]
0x45498B: mov      byte ptr [ebp - 0x18], al
0x45498E: mov      dword ptr [ebp - 0x10], 0x300
0x454995: mov      byte ptr [ebp - 0x1d], 0
0x454999: mov      dword ptr [ebp - 0xc], 1
0x4549A0: mov      eax, dword ptr [ebp - 0xc]
0x4549A3: mov      eax, dword ptr [ebp + eax*4 - 0x41d]
0x4549AA: mov      dword ptr [ebp - 0x1c], eax
0x4549AD: xor      eax, eax
0x4549AF: mov      al, byte ptr [ebp - 0x1a]
0x4549B2: xor      edx, edx
0x4549B4: mov      dl, byte ptr [ebp - 0x16]
0x4549B7: sub      eax, edx
0x4549B9: cdq      
0x4549BA: xor      eax, edx
0x4549BC: sub      eax, edx
0x4549BE: mov      ecx, eax
0x4549C0: xor      eax, eax
0x4549C2: mov      al, byte ptr [ebp - 0x1b]
0x4549C5: xor      edx, edx
0x4549C7: mov      dl, byte ptr [ebp - 0x17]
0x4549CA: sub      eax, edx
0x4549CC: cdq      
0x4549CD: xor      eax, edx
0x4549CF: sub      eax, edx
0x4549D1: add      ecx, eax
0x4549D3: xor      eax, eax
0x4549D5: mov      al, byte ptr [ebp - 0x1c]
0x4549D8: xor      edx, edx
0x4549DA: mov      dl, byte ptr [ebp - 0x18]
0x4549DD: sub      eax, edx
0x4549DF: cdq      
0x4549E0: xor      eax, edx
0x4549E2: sub      eax, edx
0x4549E4: add      ecx, eax
0x4549E6: mov      dword ptr [ebp - 0x14], ecx
0x4549E9: mov      eax, dword ptr [ebp - 0x14]
0x4549EC: cmp      eax, dword ptr [ebp - 0x10]
0x4549EF: jge      0x4549fd
0x4549F1: mov      eax, dword ptr [ebp - 0x14]
0x4549F4: mov      dword ptr [ebp - 0x10], eax
0x4549F7: mov      al, byte ptr [ebp - 0xc]
0x4549FA: mov      byte ptr [ebp - 0x1d], al
0x4549FD: inc      dword ptr [ebp - 0xc]
0x454A00: cmp      dword ptr [ebp - 0xc], 0x100
0x454A07: jne      0x4549a0
0x454A09: mov      eax, dword ptr [ebp - 8]
0x454A0C: mov      dl, byte ptr [ebp - 0x1d]
0x454A0F: mov      byte ptr [eax + 0x4f759c], dl
0x454A15: inc      dword ptr [ebp - 8]
0x454A18: cmp      dword ptr [ebp - 8], 0x100
0x454A1F: jne      0x45493a
0x454A25: mov      byte ptr [0x4f769c], 0
0x454A2C: mov      dword ptr [ebp - 8], 1
0x454A33: mov      eax, dword ptr [ebp - 8]
0x454A36: mov      eax, dword ptr [ebp + eax*4 - 0x41d]
0x454A3D: mov      dword ptr [ebp - 0x18], eax
0x454A40: xor      eax, eax
0x454A42: mov      al, byte ptr [ebp - 0x16]
0x454A45: xor      edx, edx
0x454A47: mov      dl, byte ptr [ebp - 0x17]
0x454A4A: add      eax, edx
0x454A4C: xor      edx, edx
0x454A4E: mov      dl, byte ptr [ebp - 0x18]
0x454A51: add      eax, edx
0x454A53: mov      dword ptr [ebp - 0x424], eax
0x454A59: fild     dword ptr [ebp - 0x424]
0x454A5F: fdiv     dword ptr [0x454f9c]
0x454A65: fld      xword ptr [0x454fac]
0x454A6B: fmulp    st(1)
0x454A6D: call     0x4029a0
0x454A72: mov      edx, 0xff
0x454A77: call     0x452b90
0x454A7C: mov      dword ptr [ebp - 4], eax
0x454A7F: mov      al, byte ptr [ebp - 4]
0x454A82: mov      byte ptr [ebp - 0x16], al
0x454A85: mov      al, byte ptr [ebp - 4]
0x454A88: mov      byte ptr [ebp - 0x17], al
0x454A8B: mov      al, byte ptr [ebp - 4]
0x454A8E: mov      byte ptr [ebp - 0x18], al
0x454A91: mov      dword ptr [ebp - 0x10], 0x300
0x454A98: mov      byte ptr [ebp - 0x1d], 0
0x454A9C: mov      dword ptr [ebp - 0xc], 1
0x454AA3: mov      eax, dword ptr [ebp - 0xc]
0x454AA6: mov      eax, dword ptr [ebp + eax*4 - 0x41d]
0x454AAD: mov      dword ptr [ebp - 0x1c], eax
0x454AB0: xor      eax, eax
0x454AB2: mov      al, byte ptr [ebp - 0x1a]
0x454AB5: xor      edx, edx
0x454AB7: mov      dl, byte ptr [ebp - 0x16]
0x454ABA: sub      eax, edx
0x454ABC: cdq      
0x454ABD: xor      eax, edx
0x454ABF: sub      eax, edx
0x454AC1: mov      ecx, eax
0x454AC3: xor      eax, eax
0x454AC5: mov      al, byte ptr [ebp - 0x1b]
0x454AC8: xor      edx, edx
0x454ACA: mov      dl, byte ptr [ebp - 0x17]
0x454ACD: sub      eax, edx
0x454ACF: cdq      
0x454AD0: xor      eax, edx
0x454AD2: sub      eax, edx
0x454AD4: add      ecx, eax
0x454AD6: xor      eax, eax
0x454AD8: mov      al, byte ptr [ebp - 0x1c]
0x454ADB: xor      edx, edx
0x454ADD: mov      dl, byte ptr [ebp - 0x18]
0x454AE0: sub      eax, edx
0x454AE2: cdq      
0x454AE3: xor      eax, edx
0x454AE5: sub      eax, edx
0x454AE7: add      ecx, eax
0x454AE9: mov      dword ptr [ebp - 0x14], ecx
0x454AEC: mov      eax, dword ptr [ebp - 0x14]
0x454AEF: cmp      eax, dword ptr [ebp - 0x10]
0x454AF2: jge      0x454b00
0x454AF4: mov      eax, dword ptr [ebp - 0x14]
0x454AF7: mov      dword ptr [ebp - 0x10], eax
0x454AFA: mov      al, byte ptr [ebp - 0xc]
0x454AFD: mov      byte ptr [ebp - 0x1d], al
0x454B00: inc      dword ptr [ebp - 0xc]
0x454B03: cmp      dword ptr [ebp - 0xc], 0x100
0x454B0A: jne      0x454aa3
0x454B0C: mov      eax, dword ptr [ebp - 8]
0x454B0F: mov      dl, byte ptr [ebp - 0x1d]
0x454B12: mov      byte ptr [eax + 0x4f769c], dl
0x454B18: inc      dword ptr [ebp - 8]
0x454B1B: cmp      dword ptr [ebp - 8], 0x100
0x454B22: jne      0x454a33
0x454B28: mov      byte ptr [0x4f749c], 0
0x454B2F: mov      dword ptr [ebp - 8], 1
0x454B36: mov      eax, dword ptr [ebp - 8]
0x454B39: mov      eax, dword ptr [ebp + eax*4 - 0x41d]
0x454B40: mov      dword ptr [ebp - 0x18], eax
0x454B43: xor      eax, eax
0x454B45: mov      al, byte ptr [ebp - 0x16]
0x454B48: xor      edx, edx
0x454B4A: mov      dl, byte ptr [ebp - 0x17]
0x454B4D: add      eax, edx
0x454B4F: xor      edx, edx
0x454B51: mov      dl, byte ptr [ebp - 0x18]
0x454B54: add      eax, edx
0x454B56: mov      ecx, 3
0x454B5B: xor      edx, edx
0x454B5D: div      ecx
0x454B5F: mov      dword ptr [ebp - 4], eax
0x454B62: mov      al, byte ptr [ebp - 4]
0x454B65: mov      byte ptr [ebp - 0x16], al
0x454B68: mov      byte ptr [ebp - 0x17], 0
0x454B6C: mov      byte ptr [ebp - 0x18], 0
0x454B70: mov      dword ptr [ebp - 0x10], 0x300
0x454B77: mov      byte ptr [ebp - 0x1d], 0
0x454B7B: mov      dword ptr [ebp - 0xc], 1
0x454B82: mov      eax, dword ptr [ebp - 0xc]
0x454B85: mov      eax, dword ptr [ebp + eax*4 - 0x41d]
0x454B8C: mov      dword ptr [ebp - 0x1c], eax
0x454B8F: xor      eax, eax
0x454B91: mov      al, byte ptr [ebp - 0x1a]
0x454B94: xor      edx, edx
0x454B96: mov      dl, byte ptr [ebp - 0x16]
0x454B99: sub      eax, edx
0x454B9B: cdq      
0x454B9C: xor      eax, edx
0x454B9E: sub      eax, edx
0x454BA0: mov      ecx, eax
0x454BA2: xor      eax, eax
0x454BA4: mov      al, byte ptr [ebp - 0x1b]
0x454BA7: xor      edx, edx
0x454BA9: mov      dl, byte ptr [ebp - 0x17]
0x454BAC: sub      eax, edx
0x454BAE: cdq      
0x454BAF: xor      eax, edx
0x454BB1: sub      eax, edx
0x454BB3: add      ecx, eax
0x454BB5: xor      eax, eax
0x454BB7: mov      al, byte ptr [ebp - 0x1c]
0x454BBA: xor      edx, edx
0x454BBC: mov      dl, byte ptr [ebp - 0x18]
0x454BBF: sub      eax, edx
0x454BC1: cdq      
0x454BC2: xor      eax, edx
0x454BC4: sub      eax, edx
0x454BC6: add      ecx, eax
0x454BC8: mov      dword ptr [ebp - 0x14], ecx
0x454BCB: mov      eax, dword ptr [ebp - 0x14]
0x454BCE: cmp      eax, dword ptr [ebp - 0x10]
0x454BD1: jge      0x454bdf
0x454BD3: mov      eax, dword ptr [ebp - 0x14]
0x454BD6: mov      dword ptr [ebp - 0x10], eax
0x454BD9: mov      al, byte ptr [ebp - 0xc]
0x454BDC: mov      byte ptr [ebp - 0x1d], al
0x454BDF: inc      dword ptr [ebp - 0xc]
0x454BE2: cmp      dword ptr [ebp - 0xc], 0x100
0x454BE9: jne      0x454b82
0x454BEB: mov      eax, dword ptr [ebp - 8]
0x454BEE: mov      dl, byte ptr [ebp - 0x1d]
0x454BF1: mov      byte ptr [eax + 0x4f749c], dl
0x454BF7: inc      dword ptr [ebp - 8]
0x454BFA: cmp      dword ptr [ebp - 8], 0x100
0x454C01: jne      0x454b36
0x454C07: mov      byte ptr [0x4f779c], 0
0x454C0E: mov      dword ptr [ebp - 8], 1
0x454C15: mov      eax, dword ptr [ebp - 8]
0x454C18: mov      eax, dword ptr [ebp + eax*4 - 0x41d]
0x454C1F: mov      dword ptr [ebp - 0x18], eax
0x454C22: xor      eax, eax
0x454C24: mov      al, byte ptr [ebp - 0x16]
0x454C27: xor      edx, edx
0x454C29: mov      dl, byte ptr [ebp - 0x17]
0x454C2C: add      eax, edx
0x454C2E: xor      edx, edx
0x454C30: mov      dl, byte ptr [ebp - 0x18]
0x454C33: add      eax, edx
0x454C35: mov      ecx, 3
0x454C3A: xor      edx, edx
0x454C3C: div      ecx
0x454C3E: mov      dword ptr [ebp - 4], eax
0x454C41: mov      byte ptr [ebp - 0x16], 0
0x454C45: mov      al, byte ptr [ebp - 4]
0x454C48: mov      byte ptr [ebp - 0x17], al
0x454C4B: mov      byte ptr [ebp - 0x18], 0
0x454C4F: mov      dword ptr [ebp - 0x10], 0x300
0x454C56: mov      byte ptr [ebp - 0x1d], 0
0x454C5A: mov      dword ptr [ebp - 0xc], 1
0x454C61: mov      eax, dword ptr [ebp - 0xc]
0x454C64: mov      eax, dword ptr [ebp + eax*4 - 0x41d]
0x454C6B: mov      dword ptr [ebp - 0x1c], eax
0x454C6E: xor      eax, eax
0x454C70: mov      al, byte ptr [ebp - 0x1a]
0x454C73: xor      edx, edx
0x454C75: mov      dl, byte ptr [ebp - 0x16]
0x454C78: sub      eax, edx
0x454C7A: cdq      
0x454C7B: xor      eax, edx
0x454C7D: sub      eax, edx
0x454C7F: mov      ecx, eax
0x454C81: xor      eax, eax
0x454C83: mov      al, byte ptr [ebp - 0x1b]
0x454C86: xor      edx, edx
0x454C88: mov      dl, byte ptr [ebp - 0x17]
0x454C8B: sub      eax, edx
0x454C8D: cdq      
0x454C8E: xor      eax, edx
0x454C90: sub      eax, edx
0x454C92: add      ecx, eax
0x454C94: xor      eax, eax
0x454C96: mov      al, byte ptr [ebp - 0x1c]
0x454C99: xor      edx, edx
0x454C9B: mov      dl, byte ptr [ebp - 0x18]
0x454C9E: sub      eax, edx
0x454CA0: cdq      
0x454CA1: xor      eax, edx
0x454CA3: sub      eax, edx
0x454CA5: add      ecx, eax
0x454CA7: mov      dword ptr [ebp - 0x14], ecx
0x454CAA: mov      eax, dword ptr [ebp - 0x14]
0x454CAD: cmp      eax, dword ptr [ebp - 0x10]
0x454CB0: jge      0x454cbe
0x454CB2: mov      eax, dword ptr [ebp - 0x14]
0x454CB5: mov      dword ptr [ebp - 0x10], eax
0x454CB8: mov      al, byte ptr [ebp - 0xc]
0x454CBB: mov      byte ptr [ebp - 0x1d], al
0x454CBE: inc      dword ptr [ebp - 0xc]
0x454CC1: cmp      dword ptr [ebp - 0xc], 0x100
0x454CC8: jne      0x454c61
0x454CCA: mov      eax, dword ptr [ebp - 8]
0x454CCD: mov      dl, byte ptr [ebp - 0x1d]
0x454CD0: mov      byte ptr [eax + 0x4f779c], dl
0x454CD6: inc      dword ptr [ebp - 8]
0x454CD9: cmp      dword ptr [ebp - 8], 0x100
0x454CE0: jne      0x454c15
0x454CE6: mov      byte ptr [0x4f789c], 0
0x454CED: mov      dword ptr [ebp - 8], 1
0x454CF4: mov      eax, dword ptr [ebp - 8]
0x454CF7: mov      eax, dword ptr [ebp + eax*4 - 0x41d]
0x454CFE: mov      dword ptr [ebp - 0x18], eax
0x454D01: xor      eax, eax
0x454D03: mov      al, byte ptr [ebp - 0x16]
0x454D06: xor      edx, edx
0x454D08: mov      dl, byte ptr [ebp - 0x17]
0x454D0B: add      eax, edx
0x454D0D: xor      edx, edx
0x454D0F: mov      dl, byte ptr [ebp - 0x18]
0x454D12: add      eax, edx
0x454D14: mov      ecx, 3
0x454D19: xor      edx, edx
0x454D1B: div      ecx
0x454D1D: mov      dword ptr [ebp - 4], eax
0x454D20: mov      al, byte ptr [ebp - 4]
0x454D23: mov      byte ptr [ebp - 0x16], al
0x454D26: mov      al, byte ptr [ebp - 4]
0x454D29: mov      byte ptr [ebp - 0x17], al
0x454D2C: mov      byte ptr [ebp - 0x18], 0
0x454D30: mov      dword ptr [ebp - 0x10], 0x300
0x454D37: mov      byte ptr [ebp - 0x1d], 0
0x454D3B: mov      dword ptr [ebp - 0xc], 1
0x454D42: mov      eax, dword ptr [ebp - 0xc]
0x454D45: mov      eax, dword ptr [ebp + eax*4 - 0x41d]
0x454D4C: mov      dword ptr [ebp - 0x1c], eax
0x454D4F: xor      eax, eax
0x454D51: mov      al, byte ptr [ebp - 0x1a]
0x454D54: xor      edx, edx
0x454D56: mov      dl, byte ptr [ebp - 0x16]
0x454D59: sub      eax, edx
0x454D5B: cdq      
0x454D5C: xor      eax, edx
0x454D5E: sub      eax, edx
0x454D60: mov      ecx, eax
0x454D62: xor      eax, eax
0x454D64: mov      al, byte ptr [ebp - 0x1b]
0x454D67: xor      edx, edx
0x454D69: mov      dl, byte ptr [ebp - 0x17]
0x454D6C: sub      eax, edx
0x454D6E: cdq      
0x454D6F: xor      eax, edx
0x454D71: sub      eax, edx
0x454D73: add      ecx, eax
0x454D75: xor      eax, eax
0x454D77: mov      al, byte ptr [ebp - 0x1c]
0x454D7A: xor      edx, edx
0x454D7C: mov      dl, byte ptr [ebp - 0x18]
0x454D7F: sub      eax, edx
0x454D81: cdq      
0x454D82: xor      eax, edx
0x454D84: sub      eax, edx
0x454D86: add      ecx, eax
0x454D88: mov      dword ptr [ebp - 0x14], ecx
0x454D8B: mov      eax, dword ptr [ebp - 0x14]
0x454D8E: cmp      eax, dword ptr [ebp - 0x10]
0x454D91: jge      0x454d9f
0x454D93: mov      eax, dword ptr [ebp - 0x14]
0x454D96: mov      dword ptr [ebp - 0x10], eax
0x454D99: mov      al, byte ptr [ebp - 0xc]
0x454D9C: mov      byte ptr [ebp - 0x1d], al
0x454D9F: inc      dword ptr [ebp - 0xc]
0x454DA2: cmp      dword ptr [ebp - 0xc], 0x100
0x454DA9: jne      0x454d42
0x454DAB: mov      eax, dword ptr [ebp - 8]
0x454DAE: mov      dl, byte ptr [ebp - 0x1d]
0x454DB1: mov      byte ptr [eax + 0x4f789c], dl
0x454DB7: inc      dword ptr [ebp - 8]
0x454DBA: cmp      dword ptr [ebp - 8], 0x100
0x454DC1: jne      0x454cf4
0x454DC7: mov      byte ptr [0x4f799c], 0
0x454DCE: mov      dword ptr [ebp - 8], 1
0x454DD5: mov      eax, dword ptr [ebp - 8]
0x454DD8: mov      eax, dword ptr [ebp + eax*4 - 0x41d]
0x454DDF: mov      dword ptr [ebp - 0x18], eax
0x454DE2: xor      eax, eax
0x454DE4: mov      al, byte ptr [ebp - 0x16]
0x454DE7: xor      edx, edx
0x454DE9: mov      dl, byte ptr [ebp - 0x17]
0x454DEC: add      eax, edx
0x454DEE: xor      edx, edx
0x454DF0: mov      dl, byte ptr [ebp - 0x18]
0x454DF3: add      eax, edx
0x454DF5: mov      ecx, 3
0x454DFA: xor      edx, edx
0x454DFC: div      ecx
0x454DFE: mov      dword ptr [ebp - 4], eax
0x454E01: mov      byte ptr [ebp - 0x16], 0
0x454E05: mov      byte ptr [ebp - 0x17], 0
0x454E09: mov      al, byte ptr [ebp - 4]
0x454E0C: mov      byte ptr [ebp - 0x18], al
0x454E0F: mov      dword ptr [ebp - 0x10], 0x300
0x454E16: mov      byte ptr [ebp - 0x1d], 0
0x454E1A: mov      dword ptr [ebp - 0xc], 1
0x454E21: mov      eax, dword ptr [ebp - 0xc]
0x454E24: mov      eax, dword ptr [ebp + eax*4 - 0x41d]
0x454E2B: mov      dword ptr [ebp - 0x1c], eax
0x454E2E: xor      eax, eax
0x454E30: mov      al, byte ptr [ebp - 0x1a]
0x454E33: xor      edx, edx
0x454E35: mov      dl, byte ptr [ebp - 0x16]
0x454E38: sub      eax, edx
0x454E3A: cdq      
0x454E3B: xor      eax, edx
0x454E3D: sub      eax, edx
0x454E3F: mov      ecx, eax
0x454E41: xor      eax, eax
0x454E43: mov      al, byte ptr [ebp - 0x1b]
0x454E46: xor      edx, edx
0x454E48: mov      dl, byte ptr [ebp - 0x17]
0x454E4B: sub      eax, edx
0x454E4D: cdq      
0x454E4E: xor      eax, edx
0x454E50: sub      eax, edx
0x454E52: add      ecx, eax
0x454E54: xor      eax, eax
0x454E56: mov      al, byte ptr [ebp - 0x1c]
0x454E59: xor      edx, edx
0x454E5B: mov      dl, byte ptr [ebp - 0x18]
0x454E5E: sub      eax, edx
0x454E60: cdq      
0x454E61: xor      eax, edx
0x454E63: sub      eax, edx
0x454E65: add      ecx, eax
0x454E67: mov      dword ptr [ebp - 0x14], ecx
0x454E6A: mov      eax, dword ptr [ebp - 0x14]
0x454E6D: cmp      eax, dword ptr [ebp - 0x10]
0x454E70: jge      0x454e7e
0x454E72: mov      eax, dword ptr [ebp - 0x14]
0x454E75: mov      dword ptr [ebp - 0x10], eax
0x454E78: mov      al, byte ptr [ebp - 0xc]
0x454E7B: mov      byte ptr [ebp - 0x1d], al
0x454E7E: inc      dword ptr [ebp - 0xc]
0x454E81: cmp      dword ptr [ebp - 0xc], 0x100
0x454E88: jne      0x454e21
0x454E8A: mov      eax, dword ptr [ebp - 8]
0x454E8D: mov      dl, byte ptr [ebp - 0x1d]
0x454E90: mov      byte ptr [eax + 0x4f799c], dl
0x454E96: inc      dword ptr [ebp - 8]
0x454E99: cmp      dword ptr [ebp - 8], 0x100
0x454EA0: jne      0x454dd5
0x454EA6: mov      byte ptr [0x4f7a9c], 0
0x454EAD: mov      dword ptr [ebp - 8], 1
0x454EB4: mov      eax, dword ptr [ebp - 8]
0x454EB7: mov      eax, dword ptr [ebp + eax*4 - 0x41d]
0x454EBE: mov      dword ptr [ebp - 0x18], eax
0x454EC1: xor      eax, eax
0x454EC3: mov      al, byte ptr [ebp - 0x16]
0x454EC6: xor      edx, edx
0x454EC8: mov      dl, byte ptr [ebp - 0x17]
0x454ECB: add      eax, edx
0x454ECD: xor      edx, edx
0x454ECF: mov      dl, byte ptr [ebp - 0x18]
0x454ED2: add      eax, edx
0x454ED4: mov      ecx, 3
0x454ED9: xor      edx, edx
0x454EDB: div      ecx
0x454EDD: mov      dword ptr [ebp - 4], eax
0x454EE0: mov      al, byte ptr [ebp - 4]
0x454EE3: mov      byte ptr [ebp - 0x16], al
0x454EE6: mov      byte ptr [ebp - 0x17], 0
0x454EEA: mov      al, byte ptr [ebp - 4]
0x454EED: mov      byte ptr [ebp - 0x18], al
0x454EF0: mov      dword ptr [ebp - 0x10], 0x300
0x454EF7: mov      byte ptr [ebp - 0x1d], 0
0x454EFB: mov      dword ptr [ebp - 0xc], 1
0x454F02: mov      eax, dword ptr [ebp - 0xc]
0x454F05: mov      eax, dword ptr [ebp + eax*4 - 0x41d]
0x454F0C: mov      dword ptr [ebp - 0x1c], eax
0x454F0F: xor      eax, eax
0x454F11: mov      al, byte ptr [ebp - 0x1a]
0x454F14: xor      edx, edx
0x454F16: mov      dl, byte ptr [ebp - 0x16]
0x454F19: sub      eax, edx
0x454F1B: cdq      
0x454F1C: xor      eax, edx
0x454F1E: sub      eax, edx
0x454F20: mov      ecx, eax
0x454F22: xor      eax, eax
0x454F24: mov      al, byte ptr [ebp - 0x1b]
0x454F27: xor      edx, edx
0x454F29: mov      dl, byte ptr [ebp - 0x17]
0x454F2C: sub      eax, edx
0x454F2E: cdq      
0x454F2F: xor      eax, edx
0x454F31: sub      eax, edx
0x454F33: add      ecx, eax
0x454F35: xor      eax, eax
0x454F37: mov      al, byte ptr [ebp - 0x1c]
0x454F3A: xor      edx, edx
0x454F3C: mov      dl, byte ptr [ebp - 0x18]
0x454F3F: sub      eax, edx
0x454F41: cdq      
0x454F42: xor      eax, edx
0x454F44: sub      eax, edx
0x454F46: add      ecx, eax
0x454F48: mov      dword ptr [ebp - 0x14], ecx
0x454F4B: mov      eax, dword ptr [ebp - 0x14]
0x454F4E: cmp      eax, dword ptr [ebp - 0x10]
0x454F51: jge      0x454f5f
0x454F53: mov      eax, dword ptr [ebp - 0x14]
0x454F56: mov      dword ptr [ebp - 0x10], eax
0x454F59: mov      al, byte ptr [ebp - 0xc]
0x454F5C: mov      byte ptr [ebp - 0x1d], al
0x454F5F: inc      dword ptr [ebp - 0xc]
0x454F62: cmp      dword ptr [ebp - 0xc], 0x100
0x454F69: jne      0x454f02
0x454F6B: mov      eax, dword ptr [ebp - 8]
0x454F6E: mov      dl, byte ptr [ebp - 0x1d]
0x454F71: mov      byte ptr [eax + 0x4f7a9c], dl
0x454F77: inc      dword ptr [ebp - 8]
0x454F7A: cmp      dword ptr [ebp - 8], 0x100
0x454F81: jne      0x454eb4
0x454F87: pop      edi
0x454F88: pop      esi
0x454F89: mov      esp, ebp
0x454F8B: pop      ebp
0x454F8C: ret      
0x454F8D: add      byte ptr [eax], al
0x454F8F: add      byte ptr [esi + 0x66], ah
0x454F92: cmpsb    byte ptr [esi], byte ptr es:[edi]
0x454F99: aas      
0x454F9A: add      byte ptr [eax], al
0x454F9C: add      byte ptr [eax], al
0x454F9E: inc      eax
0x454F9F: inc      eax
0x454FA0: lcall    0x9999, 0x99999999
0x454FA7: cdq      
0x454FA9: aas      
0x454FAA: add      byte ptr [eax], al
0x454FAC: int      0xcc
0x454FAE: int3     
0x454FAF: int3     
0x454FB0: int3     
0x454FB1: int3     
0x454FB2: int3     
0x454FB3: int3     
0x454FB5: aas      
0x454FB6: add      byte ptr [eax], al
0x454FB8: push     ebp
0x454FB9: mov      ebp, esp
0x454FBB: add      esp, -0x38
0x454FBE: push     esi
0x454FBF: push     edi
0x454FC0: mov      dword ptr [ebp - 4], eax
0x454FC3: mov      eax, dword ptr [ebp - 4]
0x454FC6: call     0x403e88
0x454FCB: xor      eax, eax
0x454FCD: push     ebp
0x454FCE: push     0x4550a8
0x454FD3: push     dword ptr fs:[eax]
0x454FD6: mov      dword ptr fs:[eax], esp
0x454FD9: mov      esi, 0x4550b8 ; "WEMADE Entertainment Inc."
0x454FDE: lea      edi, [ebp - 0x38]
0x454FE1: mov      ecx, 6
0x454FE6: rep movsd dword ptr es:[edi], dword ptr [esi]
0x454FE8: movsw    word ptr es:[edi], word ptr [esi]
0x454FEA: mov      dword ptr [ebp - 0x18], 0x10000
0x454FF1: mov      eax, dword ptr [ebp - 4]
0x454FF4: call     0x407f90
0x454FF9: test     al, al
0x454FFB: je       0x45500f
0x454FFD: mov      edx, 0x41
0x455002: mov      eax, dword ptr [ebp - 4]
0x455005: call     0x407e58
0x45500A: mov      dword ptr [ebp - 8], eax
0x45500D: jmp      0x45501a
0x45500F: mov      eax, dword ptr [ebp - 4]
0x455012: call     0x407e98
0x455017: mov      dword ptr [ebp - 8], eax
0x45501A: cmp      dword ptr [ebp - 8], 0
0x45501E: jle      0x455092
0x455020: lea      edx, [ebp - 0x38]
0x455023: mov      ecx, 0x30
0x455028: mov      eax, dword ptr [ebp - 8]
0x45502B: call     0x407ee8
0x455030: mov      edx, 0x4a729c
0x455035: mov      ecx, 0x10000
0x45503A: mov      eax, dword ptr [ebp - 8]
0x45503D: call     0x407ee8
0x455042: mov      edx, 0x4b729c
0x455047: mov      ecx, 0x10000
0x45504C: mov      eax, dword ptr [ebp - 8]
0x45504F: call     0x407ee8
0x455054: mov      edx, 0x4c729c
0x455059: mov      ecx, 0x10000
0x45505E: mov      eax, dword ptr [ebp - 8]
0x455061: call     0x407ee8
0x455066: mov      edx, 0x4d729c
0x45506B: mov      ecx, 0x10000
0x455070: mov      eax, dword ptr [ebp - 8]
0x455073: call     0x407ee8
0x455078: mov      edx, 0x4e729c
0x45507D: mov      ecx, 0x10000
0x455082: mov      eax, dword ptr [ebp - 8]
0x455085: call     0x407ee8
0x45508A: mov      eax, dword ptr [ebp - 8]
0x45508D: call     0x407f20
0x455092: xor      eax, eax
0x455094: pop      edx
0x455095: pop      ecx
0x455096: pop      ecx
0x455097: mov      dword ptr fs:[eax], edx
0x45509A: push     0x4550af
0x45509F: lea      eax, [ebp - 4]
0x4550A2: call     0x403a58
0x4550A7: ret      
0x4550A8: jmp      0x403518
0x4550AD: jmp      0x45509f
0x4550AF: pop      edi
0x4550B0: pop      esi
0x4550B1: mov      esp, ebp
0x4550B3: pop      ebp
0x4550B4: ret      
0x4550B5: add      byte ptr [eax], al
0x4550B7: add      byte ptr [ecx], bl
0x4550B9: push     edi
0x4550BA: inc      ebp
0x4550BB: dec      ebp
0x4550BC: inc      ecx
0x4550BD: inc      esp
0x4550BE: inc      ebp
0x4550BF: and      byte ptr [ebp + 0x6e], al
0x4550C2: je       0x455129
0x4550C4: jb       0x45513a
0x4550C6: popal    
0x4550C7: imul     ebp, dword ptr [esi + 0x6d], 0x20746e65
0x4550CE: dec      ecx
0x4550CF: outsb    dx, byte ptr [esi]
0x4550D0: arpl     word ptr [esi], bp
0x4550D2: add      byte ptr [eax], al
0x4550D4: push     ebp
0x4550D5: mov      ebp, esp
0x4550D7: add      esp, -0x40
0x4550DA: mov      dword ptr [ebp - 4], eax
0x4550DD: mov      eax, dword ptr [ebp - 4]
0x4550E0: call     0x403e88
0x4550E5: xor      eax, eax
0x4550E7: push     ebp
0x4550E8: push     0x4551ee
0x4550ED: push     dword ptr fs:[eax]
0x4550F0: mov      dword ptr fs:[eax], esp
0x4550F3: mov      byte ptr [ebp - 5], 0
0x4550F7: mov      eax, dword ptr [ebp - 4]
0x4550FA: call     0x407f90
0x4550FF: test     al, al
0x455101: je       0x4551d8
0x455107: mov      edx, 0x40
0x45510C: mov      eax, dword ptr [ebp - 4]
0x45510F: call     0x407e58
0x455114: mov      dword ptr [ebp - 0xc], eax
0x455117: cmp      dword ptr [ebp - 0xc], 0
0x45511B: jle      0x4551d8
0x455121: lea      edx, [ebp - 0x40]
0x455124: mov      ecx, 0x30
0x455129: mov      eax, dword ptr [ebp - 0xc]
0x45512C: call     0x407ebc
0x455131: cmp      dword ptr [ebp - 0x20], 0x10000
0x455138: jne      0x4551d0
0x45513E: mov      byte ptr [ebp - 5], 1
0x455142: mov      dword ptr [ebp - 0x10], 0x10000
0x455149: mov      edx, 0x4a729c
0x45514E: mov      ecx, 0x10000
0x455153: mov      eax, dword ptr [ebp - 0xc]
0x455156: call     0x407ebc
0x45515B: cmp      eax, dword ptr [ebp - 0x10]
0x45515E: je       0x455164
0x455160: mov      byte ptr [ebp - 5], 0
0x455164: mov      edx, 0x4b729c
0x455169: mov      ecx, 0x10000
0x45516E: mov      eax, dword ptr [ebp - 0xc]
0x455171: call     0x407ebc
0x455176: cmp      eax, dword ptr [ebp - 0x10]
0x455179: je       0x45517f
0x45517B: mov      byte ptr [ebp - 5], 0
0x45517F: mov      edx, 0x4c729c
0x455184: mov      ecx, 0x10000
0x455189: mov      eax, dword ptr [ebp - 0xc]
0x45518C: call     0x407ebc
0x455191: cmp      eax, dword ptr [ebp - 0x10]
0x455194: je       0x45519a
0x455196: mov      byte ptr [ebp - 5], 0
0x45519A: mov      edx, 0x4d729c
0x45519F: mov      ecx, 0x10000
0x4551A4: mov      eax, dword ptr [ebp - 0xc]
0x4551A7: call     0x407ebc
0x4551AC: cmp      eax, dword ptr [ebp - 0x10]
0x4551AF: je       0x4551b5
0x4551B1: mov      byte ptr [ebp - 5], 0
0x4551B5: mov      edx, 0x4e729c
0x4551BA: mov      ecx, 0x10000
0x4551BF: mov      eax, dword ptr [ebp - 0xc]
0x4551C2: call     0x407ebc
0x4551C7: cmp      eax, dword ptr [ebp - 0x10]
0x4551CA: je       0x4551d0
0x4551CC: mov      byte ptr [ebp - 5], 0
0x4551D0: mov      eax, dword ptr [ebp - 0xc]
0x4551D3: call     0x407f20
0x4551D8: xor      eax, eax
0x4551DA: pop      edx
0x4551DB: pop      ecx
0x4551DC: pop      ecx
0x4551DD: mov      dword ptr fs:[eax], edx
0x4551E0: push     0x4551f5
0x4551E5: lea      eax, [ebp - 4]
0x4551E8: call     0x403a58
0x4551ED: ret      
0x4551EE: jmp      0x403518
0x4551F3: jmp      0x4551e5
0x4551F5: mov      al, byte ptr [ebp - 5]
0x4551F8: mov      esp, ebp
0x4551FA: pop      ebp
0x4551FB: ret      
0x4551FC: push     ebp
0x4551FD: mov      ebp, esp
0x4551FF: add      esp, -0x44
0x455202: mov      dword ptr [ebp - 0x10], ecx
0x455205: mov      dword ptr [ebp - 0x18], edx
0x455208: mov      dword ptr [ebp - 0xc], eax
0x45520B: cmp      dword ptr [ebp - 0xc], 0
0x45520F: je       0x4552e9
0x455215: cmp      dword ptr [ebp + 0x1c], 0
0x455219: je       0x4552e9
0x45521F: mov      eax, dword ptr [ebp + 0x24]
0x455222: mov      dword ptr [ebp - 0x14], eax
0x455225: mov      eax, dword ptr [ebp + 0x10]
0x455228: mov      dword ptr [ebp - 0x1c], eax
0x45522B: cmp      dword ptr [ebp + 0x18], 0
0x45522F: jge      0x455242
0x455231: mov      eax, dword ptr [ebp + 0x18]
0x455234: sub      dword ptr [ebp - 0x18], eax
0x455237: mov      eax, dword ptr [ebp + 0x18]
0x45523A: add      dword ptr [ebp + 0x24], eax
0x45523D: xor      eax, eax
0x45523F: mov      dword ptr [ebp + 0x18], eax
0x455242: cmp      dword ptr [ebp + 0x14], 0
0x455246: jge      0x455259
0x455248: mov      eax, dword ptr [ebp + 0x14]
0x45524B: sub      dword ptr [ebp - 0x10], eax
0x45524E: mov      eax, dword ptr [ebp + 0x14]
0x455251: add      dword ptr [ebp + 0x20], eax
0x455254: xor      eax, eax
0x455256: mov      dword ptr [ebp + 0x14], eax
0x455259: mov      edx, dword ptr [ebp + 0x10]
0x45525C: sub      edx, dword ptr [ebp + 0x18]
0x45525F: mov      eax, dword ptr [ebp + 0x24]
0x455262: call     0x452b90
0x455267: mov      dword ptr [ebp - 0x20], eax
0x45526A: mov      edx, dword ptr [ebp + 0xc]
0x45526D: sub      edx, dword ptr [ebp + 0x14]
0x455270: mov      eax, dword ptr [ebp + 0x20]
0x455273: call     0x452b90
0x455278: mov      dword ptr [ebp - 8], eax
0x45527B: cmp      dword ptr [ebp - 0x20], 0
0x45527F: jle      0x4552e9
0x455281: cmp      dword ptr [ebp - 8], 0
0x455285: jle      0x4552e9
0x455287: mov      dword ptr [ebp - 4], 0
0x45528E: mov      eax, dword ptr [ebp - 4]
0x455291: cmp      eax, dword ptr [ebp - 8]
0x455294: jae      0x4552e7
0x455296: mov      esi, dword ptr [ebp - 0xc]
0x455299: mov      eax, dword ptr [ebp - 0x10]
0x45529C: add      eax, dword ptr [ebp - 4]
0x45529F: mov      ebx, dword ptr [ebp - 0x14]
0x4552A2: imul     eax, ebx
0x4552A5: add      eax, dword ptr [ebp - 0x18]
0x4552A8: add      esi, eax
0x4552AA: mov      edi, dword ptr [ebp + 0x1c]
0x4552AD: mov      eax, dword ptr [ebp + 0x14]
0x4552B0: add      eax, dword ptr [ebp - 4]
0x4552B3: mov      ebx, dword ptr [ebp - 0x1c]
0x4552B6: imul     eax, ebx
0x4552B9: add      eax, dword ptr [ebp + 0x18]
0x4552BC: add      edi, eax
0x4552BE: mov      ebx, dword ptr [ebp - 0x20]
0x4552C1: cmp      ebx, 0
0x4552C4: jbe      0x4552e2
0x4552C6: cmp      ebx, 8
0x4552C9: jb       0x4552e2
0x4552CB: movq     mm0, qword ptr [esi]
0x4552CE: movq     mm1, qword ptr [edi]
0x4552D1: paddd    mm1, mm0
0x4552D4: movq     qword ptr [edi], mm1
0x4552D7: sub      ebx, 8
0x4552DA: add      esi, 8
0x4552DD: add      edi, 8
0x4552E0: jmp      0x4552c1
0x4552E2: inc      dword ptr [ebp - 4]
0x4552E5: jmp      0x45528e
0x4552E7: emms     
0x4552E9: mov      esp, ebp
0x4552EB: pop      ebp
0x4552EC: ret      0x20
0x4552EF: nop      
0x4552F0: push     ebp
0x4552F1: mov      ebp, esp
0x4552F3: add      esp, 0xffffff2c
0x4552F9: mov      dword ptr [ebp - 0x18], ecx
0x4552FC: mov      dword ptr [ebp - 0x14], edx
0x4552FF: mov      dword ptr [ebp - 0x24], eax
0x455302: mov      eax, dword ptr [ebp - 0x24]
0x455305: call     0x44dc60
0x45530A: cmp      eax, 0x384
0x45530F: jg       0x45541b
0x455315: mov      eax, dword ptr [0x4a7294]
0x45531A: dec      eax
0x45531B: je       0x455328
0x45531D: dec      eax
0x45531E: je       0x455331
0x455320: dec      eax
0x455321: je       0x45533a
0x455323: jmp      0x45541b
0x455328: mov      dword ptr [ebp - 0x20], 0x4c729c
0x45532F: jmp      0x455341
0x455331: mov      dword ptr [ebp - 0x20], 0x4d729c
0x455338: jmp      0x455341
0x45533A: mov      dword ptr [ebp - 0x20], 0x4e729c
0x455341: xor      eax, eax
0x455343: push     ebp
0x455344: push     0x455414
0x455349: push     dword ptr fs:[eax]
0x45534C: mov      dword ptr fs:[eax], esp
0x45534F: mov      dword ptr [ebp - 0xb0], 0x6c
0x455359: lea      ecx, [ebp - 0xb0]
0x45535F: xor      eax, eax
0x455361: mov      edx, eax
0x455363: mov      eax, dword ptr [ebp - 0x24]
0x455366: call     0x44de90
0x45536B: mov      eax, dword ptr [ebp - 0x24]
0x45536E: call     0x44dc60
0x455373: mov      edx, dword ptr [ebp - 0x18]
0x455376: call     0x452b90
0x45537B: mov      dword ptr [ebp - 0x1c], eax
0x45537E: lea      eax, [ebp - 0x2c]
0x455381: mov      dword ptr [ebp - 0x30], eax
0x455384: mov      eax, dword ptr [ebp - 0x24]
0x455387: call     0x44d944
0x45538C: mov      dword ptr [ebp - 8], eax
0x45538F: mov      eax, dword ptr [ebp - 0xa0]
0x455395: mov      dword ptr [ebp - 0x10], eax
0x455398: mov      eax, dword ptr [ebp - 0x8c]
0x45539E: mov      dword ptr [ebp - 0xc], eax
0x4553A1: mov      dword ptr [ebp - 4], 0
0x4553A8: mov      ebx, dword ptr [ebp - 4]
0x4553AB: mov      eax, dword ptr [ebp - 8]
0x4553AE: cmp      ebx, eax
0x4553B0: jae      0x4553f6
0x4553B2: mov      esi, dword ptr [ebp - 0xc]
0x4553B5: mov      eax, dword ptr [ebp - 0x10]
0x4553B8: mov      ebx, dword ptr [ebp - 4]
0x4553BB: imul     eax, ebx
0x4553BE: add      esi, eax
0x4553C0: mov      edi, dword ptr [ebp - 0x14]
0x4553C3: mov      eax, dword ptr [ebp - 0x18]
0x4553C6: mov      ebx, dword ptr [ebp - 4]
0x4553C9: imul     eax, ebx
0x4553CC: add      edi, eax
0x4553CE: mov      ecx, dword ptr [ebp - 0x1c]
0x4553D1: mov      edx, dword ptr [ebp - 0x20]
0x4553D4: cmp      ecx, 0
0x4553D7: jbe      0x4553f1
0x4553D9: movzx    eax, byte ptr [edi]
0x4553DC: imul     eax, eax, 0x100
0x4553E2: movzx    ebx, byte ptr [esi]
0x4553E5: add      eax, ebx
0x4553E7: mov      al, byte ptr [edx + eax]
0x4553EA: mov      byte ptr [esi], al
0x4553EC: dec      ecx
0x4553ED: inc      esi
0x4553EE: inc      edi
0x4553EF: jmp      0x4553d4
0x4553F1: inc      dword ptr [ebp - 4]
0x4553F4: jmp      0x4553a8
0x4553F6: emms     
0x4553F8: xor      eax, eax
0x4553FA: pop      edx
0x4553FB: pop      ecx
0x4553FC: pop      ecx
0x4553FD: mov      dword ptr fs:[eax], edx
0x455400: push     0x45541b
0x455405: mov      edx, dword ptr [ebp - 0x8c]
0x45540B: mov      eax, dword ptr [ebp - 0x24]
0x45540E: call     0x44e5d8
0x455413: ret      
0x455414: jmp      0x403518
0x455419: jmp      0x455405
0x45541B: mov      esp, ebp
0x45541D: pop      ebp
0x45541E: ret      
0x45541F: nop      
0x455420: push     ebp
0x455421: mov      ebp, esp
0x455423: add      esp, 0xffffff30
0x455429: mov      dword ptr [ebp - 0x24], edx
0x45542C: mov      dword ptr [ebp - 0x2c], eax
0x45542F: mov      eax, dword ptr [ebp - 0x24]
0x455432: not      eax
0x455434: dec      eax
0x455435: sub      eax, 0x1e
0x455438: jb       0x455573
0x45543E: mov      eax, dword ptr [ebp - 0x2c]
0x455441: call     0x44dc60
0x455446: cmp      eax, 0x384
0x45544B: jg       0x455573
0x455451: xor      eax, eax
0x455453: push     ebp
0x455454: push     0x45556c
0x455459: push     dword ptr fs:[eax]
0x45545C: mov      dword ptr fs:[eax], esp
0x45545F: mov      dword ptr [ebp - 0xb0], 0x6c
0x455469: lea      ecx, [ebp - 0xb0]
0x45546F: xor      eax, eax
0x455471: mov      edx, eax
0x455473: mov      eax, dword ptr [ebp - 0x2c]
0x455476: call     0x44de90
0x45547B: mov      eax, dword ptr [ebp - 0x2c]
0x45547E: call     0x44dc60
0x455483: mov      dword ptr [ebp - 0x14], eax
0x455486: mov      eax, dword ptr [ebp - 0x2c]
0x455489: call     0x44d944
0x45548E: mov      dword ptr [ebp - 8], eax
0x455491: lea      eax, [ebp - 0x34]
0x455494: mov      dword ptr [ebp - 0x1c], eax
0x455497: mov      dword ptr [ebp - 0x28], 0x4c729c
0x45549E: mov      eax, dword ptr [ebp - 0xa0]
0x4554A4: mov      dword ptr [ebp - 0x10], eax
0x4554A7: mov      eax, dword ptr [ebp - 0x8c]
0x4554AD: mov      dword ptr [ebp - 0xc], eax
0x4554B0: mov      dword ptr [ebp - 4], 0
0x4554B7: mov      ebx, dword ptr [ebp - 4]
0x4554BA: mov      eax, dword ptr [ebp - 8]
0x4554BD: cmp      ebx, eax
0x4554BF: jae      0x45554e
0x4554C5: mov      esi, dword ptr [ebp - 0xc]
0x4554C8: mov      eax, dword ptr [ebp - 0x10]
0x4554CB: mov      ebx, dword ptr [ebp - 4]
0x4554CE: imul     eax, ebx
0x4554D1: add      esi, eax
0x4554D3: mov      eax, dword ptr [ebp - 0x14]
0x4554D6: mov      dword ptr [ebp - 0x18], eax
0x4554D9: mov      edx, dword ptr [ebp - 0x1c]
0x4554DC: mov      ebx, dword ptr [ebp - 0x18]
0x4554DF: cmp      ebx, 0
0x4554E2: jbe      0x455546
0x4554E4: cmp      ebx, 8
0x4554E7: jb       0x455529
0x4554E9: movq     mm0, qword ptr [esi]
0x4554EC: movq     qword ptr [edx], mm0
0x4554EF: mov      dword ptr [ebp - 0x20], 8
0x4554F6: mov      eax, dword ptr [ebp - 0x24]
0x4554F9: imul     eax, eax, 0x100
0x4554FF: movzx    ebx, byte ptr [edx]
0x455502: add      eax, ebx
0x455504: mov      ebx, dword ptr [ebp - 0x28]
0x455507: mov      al, byte ptr [ebx + eax]
0x45550A: mov      byte ptr [edx], al
0x45550C: dec      dword ptr [ebp - 0x20]
0x45550F: inc      edx
0x455510: inc      edi
0x455511: cmp      dword ptr [ebp - 0x20], 0
0x455515: ja       0x4554f6
0x455517: sub      edx, 8
0x45551A: movq     mm0, qword ptr [edx]
0x45551D: movq     qword ptr [esi], mm0
0x455520: sub      dword ptr [ebp - 0x18], 8
0x455524: add      esi, 8
0x455527: jmp      0x4554d9
0x455529: mov      eax, dword ptr [ebp - 0x24]
0x45552C: imul     eax, eax, 0x100
0x455532: movzx    ebx, byte ptr [edx]
0x455535: add      eax, ebx
0x455537: mov      ebx, dword ptr [ebp - 0x28]
0x45553A: mov      al, byte ptr [ebx + eax]
0x45553D: mov      byte ptr [esi], al
0x45553F: inc      edi
0x455540: inc      esi
0x455541: dec      dword ptr [ebp - 0x18]
0x455544: jmp      0x4554d9
0x455546: inc      dword ptr [ebp - 4]
0x455549: jmp      0x4554b7
0x45554E: emms     
0x455550: xor      eax, eax
0x455552: pop      edx
0x455553: pop      ecx
0x455554: pop      ecx
0x455555: mov      dword ptr fs:[eax], edx
0x455558: push     0x455573
0x45555D: mov      edx, dword ptr [ebp - 0x8c]
0x455563: mov      eax, dword ptr [ebp - 0x2c]
0x455566: call     0x44e5d8
0x45556B: ret      
0x45556C: jmp      0x403518
0x455571: jmp      0x45555d
0x455573: mov      esp, ebp
0x455575: pop      ebp
0x455576: ret      
0x455577: nop      
0x455578: push     ebp
0x455579: mov      ebp, esp
0x45557B: add      esp, -0xc
0x45557E: mov      dword ptr [ebp - 0xc], ecx
0x455581: mov      dword ptr [ebp - 8], edx
0x455584: mov      dword ptr [ebp - 4], eax
0x455587: mov      eax, dword ptr [ebp + 0xc]
0x45558A: push     eax
0x45558B: push     0
0x45558D: push     0
0x45558F: mov      eax, dword ptr [ebp + 0xc]
0x455592: call     0x44dc60
0x455597: push     eax
0x455598: mov      eax, dword ptr [ebp + 0xc]
0x45559B: call     0x44d944
0x4555A0: push     eax
0x4555A1: mov      eax, dword ptr [ebp + 8]
0x4555A4: push     eax
0x4555A5: mov      ecx, dword ptr [ebp - 0xc]
0x4555A8: mov      edx, dword ptr [ebp - 8]
0x4555AB: mov      eax, dword ptr [ebp - 4]
0x4555AE: call     0x4555bc
0x4555B3: mov      esp, ebp
0x4555B5: pop      ebp
0x4555B6: ret      8
0x4555B9: lea      eax, [eax]
0x4555BC: push     ebp
0x4555BD: mov      ebp, esp
0x4555BF: add      esp, 0xfffff7ac
0x4555C5: push     ebx
0x4555C6: mov      dword ptr [ebp - 0x758], ecx
0x4555CC: mov      dword ptr [ebp - 0x754], edx
0x4555D2: mov      dword ptr [ebp - 0x750], eax
0x4555D8: mov      eax, dword ptr [ebp - 0x750]
0x4555DE: call     0x44d8b4
0x4555E3: test     eax, eax
0x4555E5: je       0x455a42
0x4555EB: mov      eax, dword ptr [ebp + 0x1c]
0x4555EE: call     0x44d8b4
0x4555F3: test     eax, eax
0x4555F5: je       0x455a42
0x4555FB: mov      eax, dword ptr [ebp - 0x750]
0x455601: call     0x44dc60
0x455606: cmp      eax, dword ptr [ebp - 0x754]
0x45560C: jle      0x455a42
0x455612: mov      eax, dword ptr [ebp - 0x750]
0x455618: call     0x44d944
0x45561D: cmp      eax, dword ptr [ebp - 0x758]
0x455623: jle      0x455a42
0x455629: cmp      dword ptr [ebp - 0x754], 0
0x455630: jge      0x455659
0x455632: mov      eax, dword ptr [ebp - 0x754]
0x455638: neg      eax
0x45563A: mov      dword ptr [ebp - 0x760], eax
0x455640: mov      eax, dword ptr [ebp + 0x10]
0x455643: add      eax, dword ptr [ebp - 0x754]
0x455649: mov      dword ptr [ebp - 0x768], eax
0x45564F: xor      eax, eax
0x455651: mov      dword ptr [ebp - 0x754], eax
0x455657: jmp      0x45566b
0x455659: mov      eax, dword ptr [ebp + 0x18]
0x45565C: mov      dword ptr [ebp - 0x760], eax
0x455662: mov      eax, dword ptr [ebp + 0x10]
0x455665: mov      dword ptr [ebp - 0x768], eax
0x45566B: cmp      dword ptr [ebp - 0x758], 0
0x455672: jge      0x455695
0x455674: mov      eax, dword ptr [ebp - 0x758]
0x45567A: neg      eax
0x45567C: mov      dword ptr [ebp - 0x764], eax
0x455682: mov      eax, dword ptr [ebp + 0xc]
0x455685: mov      dword ptr [ebp - 0x76c], eax
0x45568B: xor      eax, eax
0x45568D: mov      dword ptr [ebp - 0x758], eax
0x455693: jmp      0x4556ad
0x455695: mov      eax, dword ptr [ebp + 0x14]
0x455698: mov      dword ptr [ebp - 0x764], eax
0x45569E: mov      eax, dword ptr [ebp - 0x764]
0x4556A4: add      eax, dword ptr [ebp + 0xc]
0x4556A7: mov      dword ptr [ebp - 0x76c], eax
0x4556AD: mov      eax, dword ptr [ebp + 0x1c]
0x4556B0: call     0x44dc60
0x4556B5: mov      edx, dword ptr [ebp - 0x760]
0x4556BB: add      edx, dword ptr [ebp - 0x768]
0x4556C1: cmp      eax, edx
0x4556C3: jge      0x4556d9
0x4556C5: mov      eax, dword ptr [ebp + 0x1c]
0x4556C8: call     0x44dc60
0x4556CD: sub      eax, dword ptr [ebp - 0x760]
0x4556D3: mov      dword ptr [ebp - 0x768], eax
0x4556D9: mov      eax, dword ptr [ebp + 0x1c]
0x4556DC: call     0x44d944
0x4556E1: cmp      eax, dword ptr [ebp - 0x76c]
0x4556E7: jge      0x4556f7
0x4556E9: mov      eax, dword ptr [ebp + 0x1c]
0x4556EC: call     0x44d944
0x4556F1: mov      dword ptr [ebp - 0x76c], eax
0x4556F7: mov      eax, dword ptr [ebp - 0x750]
0x4556FD: call     0x44dc60
0x455702: mov      edx, dword ptr [ebp - 0x754]
0x455708: add      edx, dword ptr [ebp - 0x768]
0x45570E: cmp      eax, edx
0x455710: jge      0x455736
0x455712: mov      eax, dword ptr [ebp - 0x750]
0x455718: call     0x44dc60
0x45571D: sub      eax, dword ptr [ebp - 0x754]
0x455723: test     eax, eax
0x455725: jns      0x45572a
0x455727: add      eax, 3
0x45572A: sar      eax, 2
0x45572D: shl      eax, 2
0x455730: mov      dword ptr [ebp - 0x768], eax
0x455736: mov      eax, dword ptr [ebp - 0x750]
0x45573C: call     0x44d944
0x455741: mov      edx, dword ptr [ebp - 0x758]
0x455747: add      edx, dword ptr [ebp - 0x76c]
0x45574D: sub      edx, dword ptr [ebp - 0x764]
0x455753: cmp      eax, edx
0x455755: jge      0x455774
0x455757: mov      eax, dword ptr [ebp - 0x750]
0x45575D: call     0x44d944
0x455762: sub      eax, dword ptr [ebp - 0x758]
0x455768: add      eax, dword ptr [ebp - 0x764]
0x45576E: mov      dword ptr [ebp - 0x76c], eax
0x455774: mov      eax, dword ptr [ebp - 0x750]
0x45577A: call     0x44dc60
0x45577F: mov      ebx, eax
0x455781: mov      eax, dword ptr [ebp - 0x750]
0x455787: call     0x44d944
0x45578C: imul     ebx, eax
0x45578F: mov      eax, dword ptr [ebp - 0x754]
0x455795: add      eax, dword ptr [ebp - 0x768]
0x45579B: mov      edx, dword ptr [ebp - 0x758]
0x4557A1: add      edx, dword ptr [ebp - 0x76c]
0x4557A7: sub      edx, dword ptr [ebp - 0x764]
0x4557AD: imul     edx
0x4557AF: cmp      ebx, eax
0x4557B1: jge      0x4557d2
0x4557B3: mov      eax, dword ptr [ebp - 0x76c]
0x4557B9: sub      eax, dword ptr [ebp - 0x764]
0x4557BF: sar      eax, 1
0x4557C1: jns      0x4557c6
0x4557C3: adc      eax, 0
0x4557C6: add      eax, dword ptr [ebp - 0x764]
0x4557CC: mov      dword ptr [ebp - 0x76c], eax
0x4557D2: cmp      dword ptr [ebp - 0x768], 0
0x4557D9: jle      0x455a42
0x4557DF: cmp      dword ptr [ebp - 0x76c], 0
0x4557E6: jle      0x455a42
0x4557EC: mov      eax, dword ptr [ebp + 0x1c]
0x4557EF: call     0x44dc60
0x4557F4: cmp      eax, dword ptr [ebp - 0x760]
0x4557FA: jle      0x455a42
0x455800: mov      eax, dword ptr [ebp + 0x1c]
0x455803: call     0x44d944
0x455808: cmp      eax, dword ptr [ebp - 0x764]
0x45580E: jle      0x455a42
0x455814: cmp      dword ptr [ebp - 0x768], 0x384
0x45581E: jg       0x455a42
0x455824: xor      eax, eax
0x455826: push     ebp
0x455827: push     0x455a3b
0x45582C: push     dword ptr fs:[eax]
0x45582F: mov      dword ptr fs:[eax], esp
0x455832: mov      dword ptr [ebp - 0x7e4], 0x6c
0x45583C: mov      dword ptr [ebp - 0x850], 0x6c
0x455846: lea      ecx, [ebp - 0x7e4]
0x45584C: xor      eax, eax
0x45584E: mov      edx, eax
0x455850: mov      eax, dword ptr [ebp + 0x1c]
0x455853: call     0x44de90
0x455858: lea      ecx, [ebp - 0x850]
0x45585E: xor      eax, eax
0x455860: mov      edx, eax
0x455862: mov      eax, dword ptr [ebp - 0x750]
0x455868: call     0x44de90
0x45586D: mov      eax, dword ptr [ebp - 0x768]
0x455873: test     eax, eax
0x455875: jns      0x45587a
0x455877: add      eax, 3
0x45587A: sar      eax, 2
0x45587D: mov      dword ptr [ebp - 0x74c], eax
0x455883: mov      eax, dword ptr [ebp - 0x768]
0x455889: mov      dword ptr [ebp - 0x748], eax
0x45588F: mov      eax, dword ptr [ebp - 0x768]
0x455895: mov      dword ptr [ebp - 0x39c], eax
0x45589B: mov      eax, dword ptr [ebp - 0x768]
0x4558A1: mov      dword ptr [ebp - 0x738], eax
0x4558A7: mov      eax, dword ptr [ebp + 8]
0x4558AA: sub      eax, 1
0x4558AD: jae      0x4558bb
0x4558AF: mov      dword ptr [ebp - 0x744], 0x4a729c
0x4558B9: jmp      0x4558c5
0x4558BB: mov      dword ptr [ebp - 0x744], 0x4b729c
0x4558C5: mov      eax, dword ptr [ebp - 0x764]
0x4558CB: mov      edx, dword ptr [ebp - 0x76c]
0x4558D1: dec      edx
0x4558D2: sub      edx, eax
0x4558D4: jl       0x455a0c
0x4558DA: inc      edx
0x4558DB: mov      dword ptr [ebp - 0x770], edx
0x4558E1: mov      dword ptr [ebp - 0x75c], eax
0x4558E7: mov      eax, dword ptr [ebp - 0x7d4]
0x4558ED: imul     dword ptr [ebp - 0x75c]
0x4558F3: add      eax, dword ptr [ebp - 0x7c0]
0x4558F9: add      eax, dword ptr [ebp - 0x760]
0x4558FF: mov      dword ptr [ebp - 8], eax
0x455902: mov      eax, dword ptr [ebp - 0x758]
0x455908: add      eax, dword ptr [ebp - 0x75c]
0x45590E: sub      eax, dword ptr [ebp - 0x764]
0x455914: imul     dword ptr [ebp - 0x840]
0x45591A: add      eax, dword ptr [ebp - 0x82c]
0x455920: add      eax, dword ptr [ebp - 0x754]
0x455926: mov      dword ptr [ebp - 0x3a4], eax
0x45592C: mov      dword ptr [ebp - 4], 0
0x455933: mov      esi, dword ptr [ebp - 8]
0x455936: lea      edi, [ebp - 0x397]
0x45593C: mov      ebx, dword ptr [ebp - 4]
0x45593F: cmp      ebx, dword ptr [ebp - 0x39c]
0x455945: jae      0x455954
0x455947: movq     mm0, qword ptr [esi + ebx]
0x45594B: movq     qword ptr [edi + ebx], mm0
0x45594F: add      ebx, 8
0x455952: jmp      0x45593f
0x455954: mov      dword ptr [ebp - 0x3a0], 0
0x45595E: mov      esi, dword ptr [ebp - 0x3a4]
0x455964: lea      edi, [ebp - 0x733]
0x45596A: mov      ebx, dword ptr [ebp - 0x3a0]
0x455970: cmp      ebx, dword ptr [ebp - 0x738]
0x455976: jae      0x455985
0x455978: movq     mm0, qword ptr [esi + ebx]
0x45597C: movq     qword ptr [edi + ebx], mm0
0x455980: add      ebx, 8
0x455983: jmp      0x455970
0x455985: lea      esi, [ebp - 0x397]
0x45598B: lea      edi, [ebp - 0x733]
0x455991: mov      dword ptr [ebp - 0x73c], 0
0x45599B: mov      ebx, dword ptr [ebp - 0x73c]
0x4559A1: cmp      byte ptr [esi + ebx], 0
0x4559A5: je       0x4559d1
0x4559A7: movzx    eax, byte ptr [esi + ebx]
0x4559AB: shl      eax, 8
0x4559AE: mov      dword ptr [ebp - 0x740], eax
0x4559B4: movzx    eax, byte ptr [edi + ebx]
0x4559B8: add      dword ptr [ebp - 0x740], eax
0x4559BE: mov      edx, dword ptr [ebp - 0x744]
0x4559C4: mov      ecx, dword ptr [ebp - 0x740]
0x4559CA: movzx    eax, byte ptr [edx + ecx]
0x4559CE: mov      byte ptr [edi + ebx], al
0x4559D1: inc      dword ptr [ebp - 0x73c]
0x4559D7: mov      eax, dword ptr [ebp - 0x748]
0x4559DD: cmp      dword ptr [ebp - 0x73c], eax
0x4559E3: jb       0x45599b
0x4559E5: lea      esi, [ebp - 0x733]
0x4559EB: mov      edi, dword ptr [ebp - 0x3a4]
0x4559F1: mov      ecx, dword ptr [ebp - 0x74c]
0x4559F7: cld      
0x4559F8: rep movsd dword ptr es:[edi], dword ptr [esi]
0x4559FA: inc      dword ptr [ebp - 0x75c]
0x455A00: dec      dword ptr [ebp - 0x770]
0x455A06: jne      0x4558e7
0x455A0C: emms     
0x455A0E: xor      eax, eax
0x455A10: pop      edx
0x455A11: pop      ecx
0x455A12: pop      ecx
0x455A13: mov      dword ptr fs:[eax], edx
0x455A16: push     0x455a42
0x455A1B: mov      edx, dword ptr [ebp - 0x7c0]
0x455A21: mov      eax, dword ptr [ebp + 0x1c]
0x455A24: call     0x44e5d8
0x455A29: mov      edx, dword ptr [ebp - 0x82c]
0x455A2F: mov      eax, dword ptr [ebp - 0x750]
0x455A35: call     0x44e5d8
0x455A3A: ret      
0x455A3B: jmp      0x403518
0x455A40: jmp      0x455a1b
0x455A42: pop      ebx
0x455A43: mov      esp, ebp
0x455A45: pop      ebp
0x455A46: ret      0x18
0x455A49: lea      eax, [eax]
0x455A4C: push     ebp
0x455A4D: mov      ebp, esp
0x455A4F: add      esp, 0xfffffc3c
0x455A55: mov      dword ptr [ebp - 0x348], ecx
0x455A5B: mov      dword ptr [ebp - 0x344], edx
0x455A61: mov      dword ptr [ebp - 0x340], eax
0x455A67: cmp      dword ptr [ebp - 0x348], 0x320
0x455A71: jg       0x455c44
0x455A77: cmp      byte ptr [ebp + 8], 0
0x455A7B: je       0x455c44
0x455A81: xor      eax, eax
0x455A83: mov      dword ptr [ebp - 0x33c], eax
0x455A89: xor      eax, eax
0x455A8B: mov      al, byte ptr [ebp + 8]
0x455A8E: cmp      eax, 9
0x455A91: ja       0x455b30
0x455A97: jmp      dword ptr [eax*4 + 0x455a9e]
0x455A9E: xor      byte ptr [ebx + 0x45], bl
0x455AA1: add      dh, al
0x455AA3: pop      edx
0x455AA4: inc      ebp
0x455AA5: add      dl, dl
0x455AA7: pop      edx
0x455AA8: inc      ebp
0x455AA9: add      dh, bl
0x455AAB: pop      edx
0x455AAC: inc      ebp
0x455AAD: add      dl, ch
0x455AAF: pop      edx
0x455AB0: inc      ebp
0x455AB1: add      dh, dh
0x455AB3: pop      edx
0x455AB4: inc      ebp
0x455AB5: add      byte ptr [edx], al
0x455AB7: pop      ebx
0x455AB8: inc      ebp
0x455AB9: add      byte ptr [esi], cl
0x455ABB: pop      ebx
0x455ABC: inc      ebp
0x455ABD: add      byte ptr [edx], bl
0x455ABF: pop      ebx
0x455AC0: inc      ebp
0x455AC1: add      byte ptr [esi], ah
0x455AC3: pop      ebx
0x455AC4: inc      ebp
0x455AC5: add      bh, al
0x455AC7: test     esp, eax
0x455AC9: cld      
0x455ACB: call     ptr [ebx + esi*2 + 0x5eeb004f]
0x455AD2: mov      dword ptr [ebp - 0x33c], 0x4f729c
0x455ADC: jmp      0x455b30
0x455ADE: mov      dword ptr [ebp - 0x33c], 0x4f759c
0x455AE8: jmp      0x455b30
0x455AEA: mov      dword ptr [ebp - 0x33c], 0x4f769c
0x455AF4: jmp      0x455b30
0x455AF6: mov      dword ptr [ebp - 0x33c], 0x4f749c
0x455B00: jmp      0x455b30
0x455B02: mov      dword ptr [ebp - 0x33c], 0x4f779c
0x455B0C: jmp      0x455b30
0x455B0E: mov      dword ptr [ebp - 0x33c], 0x4f799c
0x455B18: jmp      0x455b30
0x455B1A: mov      dword ptr [ebp - 0x33c], 0x4f789c
0x455B24: jmp      0x455b30
0x455B26: mov      dword ptr [ebp - 0x33c], 0x4f7a9c
0x455B30: cmp      dword ptr [ebp - 0x33c], 0
0x455B37: jne      0x455b46
0x455B39: xor      eax, eax
0x455B3B: mov      dword ptr [ebp - 0x33c], eax
0x455B41: jmp      0x455c44
0x455B46: xor      eax, eax
0x455B48: push     ebp
0x455B49: push     0x455c3d
0x455B4E: push     dword ptr fs:[eax]
0x455B51: mov      dword ptr fs:[eax], esp
0x455B54: mov      dword ptr [ebp - 0x3c4], 0x6c
0x455B5E: lea      ecx, [ebp - 0x3c4]
0x455B64: xor      eax, eax
0x455B66: mov      edx, eax
0x455B68: mov      eax, dword ptr [ebp + 0xc]
0x455B6B: call     0x44de90
0x455B70: mov      eax, dword ptr [ebp - 0x348]
0x455B76: mov      dword ptr [ebp - 0x338], eax
0x455B7C: mov      eax, dword ptr [ebp + 0x10]
0x455B7F: dec      eax
0x455B80: test     eax, eax
0x455B82: jl       0x455c21
0x455B88: inc      eax
0x455B89: mov      dword ptr [ebp - 0x350], eax
0x455B8F: mov      dword ptr [ebp - 0x34c], 0
0x455B99: mov      eax, dword ptr [ebp - 0x344]
0x455B9F: add      eax, dword ptr [ebp - 0x34c]
0x455BA5: imul     dword ptr [ebp - 0x3b4]
0x455BAB: add      eax, dword ptr [ebp - 0x3a0]
0x455BB1: add      eax, dword ptr [ebp - 0x340]
0x455BB7: mov      dword ptr [ebp - 8], eax
0x455BBA: mov      dword ptr [ebp - 4], 0
0x455BC1: mov      esi, dword ptr [ebp - 8]
0x455BC4: lea      edi, [ebp - 0x333]
0x455BCA: mov      ebx, dword ptr [ebp - 4]
0x455BCD: cmp      ebx, dword ptr [ebp - 0x338]
0x455BD3: jae      0x455c0d
0x455BD5: movq     mm0, qword ptr [esi + ebx]
0x455BD9: movq     qword ptr [edi], mm0
0x455BDC: mov      ebx, 0
0x455BE1: cmp      ebx, 8
0x455BE4: je       0x455bfa
0x455BE6: movzx    eax, byte ptr [edi + ebx]
0x455BEA: mov      edx, dword ptr [ebp - 0x33c]
0x455BF0: movzx    eax, byte ptr [edx + eax]
0x455BF4: mov      byte ptr [edi + ebx], al
0x455BF7: inc      ebx
0x455BF8: jmp      0x455be1
0x455BFA: mov      ebx, dword ptr [ebp - 4]
0x455BFD: movq     mm0, qword ptr [edi]
0x455C00: movq     qword ptr [esi + ebx], mm0
0x455C04: add      edi, 8
0x455C07: add      dword ptr [ebp - 4], 8
0x455C0B: jmp      0x455bca
0x455C0D: emms     
0x455C0F: inc      dword ptr [ebp - 0x34c]
0x455C15: dec      dword ptr [ebp - 0x350]
0x455C1B: jne      0x455b99
0x455C21: xor      eax, eax
0x455C23: pop      edx
0x455C24: pop      ecx
0x455C25: pop      ecx
0x455C26: mov      dword ptr fs:[eax], edx
0x455C29: push     0x455c44
0x455C2E: mov      edx, dword ptr [ebp - 0x3a0]
0x455C34: mov      eax, dword ptr [ebp + 0xc]
0x455C37: call     0x44e5d8
0x455C3C: ret      
0x455C3D: jmp      0x403518
0x455C42: jmp      0x455c2e
0x455C44: mov      esp, ebp
0x455C46: pop      ebp
0x455C47: ret      0xc
0x455C4A: mov      eax, eax
0x455C4C: push     ebp
0x455C4D: mov      ebp, esp
0x455C4F: xor      eax, eax
0x455C51: push     ebp
0x455C52: push     0x455c71
0x455C57: push     dword ptr fs:[eax]
0x455C5A: mov      dword ptr fs:[eax], esp
0x455C5D: inc      dword ptr [0x4a7298]
0x455C63: xor      eax, eax
0x455C65: pop      edx
0x455C66: pop      ecx
0x455C67: pop      ecx
0x455C68: mov      dword ptr fs:[eax], edx
0x455C6B: push     0x455c78
0x455C70: ret      
0x455C71: jmp      0x403518
0x455C76: jmp      0x455c70
0x455C78: pop      ebp
0x455C79: ret      
0x455C7A: mov      eax, eax
0x455C7C: sub      dword ptr [0x4a7298], 1
0x455C83: ret      
0x455C84: mov      byte ptr [ebp + eax*2], bl
0x455C88: add      ecx, dword ptr [ebx]
0x455C8A: push     esp
0x455C8B: inc      ebx
0x455C8C: insb     byte ptr es:[edi], dx
0x455C8D: imul     esp, dword ptr [ebx + 0x6b], 0x6e756f53
0x455C94: add      dword ptr fs:[eax], eax
0x455C97: add      byte ptr [eax], al
0x455C99: add      byte ptr [ebx], al
0x455C9B: add      byte ptr [eax], al
0x455C9D: add      byte ptr [esp + ebx*2 + 0x63060045], al
0x455CA4: jae      0x455cf4
0x455CA6: outsd    dx, dword ptr [esi]
0x455CA7: outsb    dx, byte ptr [esi]
0x455CA8: pop      es
0x455CAA: arpl     word ptr [ebx + 0x53], si
0x455CAD: je       0x455d1e
0x455CAF: outsb    dx, byte ptr [esi]
0x455CB0: pop      es
0x455CB2: arpl     word ptr [ebx + 0x47], si
0x455CB5: insb     byte ptr es:[edi], dx
0x455CB6: popal    
0x455CB7: jae      0x455d2c ; "Char"
0x455CB9: push     es
0x455CBA: arpl     word ptr [ebx + 0x4e], si
0x455CBD: outsd    dx, dword ptr [esi]
0x455CBE: jb       0x455d2d
0x455CC0: les      ebx, ptr [ebp + eax*2]
0x455CC4: or       byte ptr [esi], cl
0x455CC6: push     esp
0x455CC7: dec      edi
0x455CC8: outsb    dx, byte ptr [esi]
0x455CC9: inc      esp
0x455CCA: imul     esi, dword ptr [edx + 0x65], 0x61507463
0x455CD1: imul     ebp, dword ptr [esi + 0x74], 0x6080200
0x455CD8: push     ebx
0x455CD9: outsb    dx, byte ptr gs:[esi]
0x455CDB: jb       0x455ce6
0x455CDF: push     esp
0x455CE0: dec      edi
0x455CE1: bound    ebp, qword ptr [edx + 0x65]
0x455CE4: arpl     word ptr [eax + ecx + 8], si
0x455CE8: jae      0x455d60
0x455CEB: jb       0x455d53
0x455CED: popal    
0x455CEE: arpl     word ptr [ebp + 0x12], sp
0x455CF1: push     esp
0x455CF2: inc      esp
0x455CF3: imul     esi, dword ptr [edx + 0x65], 0x72447463
0x455CFA: popal    
0x455CFB: ja       0x455d50
0x455CFD: jne      0x455d71
0x455CFF: popaw    
0x455D01: arpl     word ptr [ebp - 0x70], sp
0x455D04: or       byte ptr [ebp + 0x45], bl
0x455D07: add      byte ptr [eax], cl
0x455D09: or       edx, dword ptr [edi + ecx*2 + 0x6e]
0x455D0D: dec      ebx
0x455D0E: jns      0x455d61 ; "Shift"
0x455D11: jb       0x455d78
0x455D13: jae      0x455d88
0x455D15: add      byte ptr [edx], al
0x455D17: or       byte ptr [esi], al
0x455D19: push     ebx
0x455D1A: outsb    dx, byte ptr gs:[esi]
0x455D1C: jb       0x455d27
0x455D20: push     esp
0x455D21: dec      edi
0x455D22: bound    ebp, qword ptr [edx + 0x65]
0x455D25: arpl     word ptr [ecx + eax + 3], si
0x455D29: dec      ebx
0x455D2A: jns      0x455d31
0x455D2D: inc      ebx
0x455D2E: push     0x408d7261
0x455D33: add      byte ptr [eax], bh
0x455D35: pop      ebp
0x455D36: inc      ebp
0x455D37: add      byte ptr [eax], cl
0x455D39: or       dl, byte ptr [edi + ecx*2 + 0x6e]
0x455D3D: dec      ebx
0x455D3E: jns      0x455d85
0x455D41: outsd    dx, dword ptr [esi]
0x455D42: ja       0x455db2
0x455D44: add      byte ptr [ebx], al
0x455D46: or       byte ptr [esi], al
0x455D48: push     ebx
0x455D49: outsb    dx, byte ptr gs:[esi]
0x455D4B: jb       0x455d56
0x455D4F: push     esp
0x455D50: dec      edi
0x455D51: bound    ebp, qword ptr [edx + 0x65]
0x455D54: arpl     word ptr [ecx + eax + 3], si
0x455D58: dec      ebx
0x455D59: jns      0x455d60
0x455D5C: push     edi
0x455D5D: outsd    dx, dword ptr [esi]
0x455D5E: jb       0x455dc4
0x455D60: add      byte ptr [0x66696853], al
0x455D66: je       0x455d73
0x455D68: push     esp
0x455D69: push     ebx
0x455D6A: push     0x53746669
0x455D6F: je       0x455dd2
0x455D71: je       0x455dd8
0x455D73: nop      
0x455D74: js       0x455dd3
0x455D76: inc      ebp
0x455D77: add      byte ptr [eax], cl
0x455D79: or       al, 0x54
0x455D7B: dec      edi
0x455D7C: outsb    dx, byte ptr [esi]
0x455D7D: dec      ebp
0x455D7E: outsd    dx, dword ptr [esi]
0x455D7F: jne      0x455df4
0x455D81: dec      ebp
0x455D83: outsd    dx, dword ptr [esi]
0x455D84: jbe      0x455deb
0x455D86: add      byte ptr [eax + ecx], al
0x455D89: push     es
0x455D8A: push     ebx
0x455D8B: outsb    dx, byte ptr gs:[esi]
0x455D8D: jb       0x455d98
0x455D91: push     esp
0x455D92: dec      edi
0x455D93: bound    ebp, qword ptr [edx + 0x65]
0x455D96: arpl     word ptr [eax + eax + 5], si
0x455D9A: push     ebx
0x455D9B: push     0xb746669
0x455DA0: push     esp
0x455DA1: push     ebx
0x455DA2: push     0x53746669
0x455DA7: je       0x455e0a
0x455DA9: je       0x455e10
0x455DAB: add      byte ptr [ecx], al
0x455DAD: pop      eax
0x455DAE: pop      es
0x455DAF: dec      ecx
0x455DB0: outsb    dx, byte ptr [esi]
0x455DB1: je       0x455e18
0x455DB3: jb       0x455db7 ; "Y"
0x455DB7: add      dword ptr [ecx + 7], ebx
0x455DBA: dec      ecx
0x455DBB: outsb    dx, byte ptr [esi]
0x455DBC: je       0x455e23
0x455DBE: jb       0x455d4f
0x455DC2: inc      eax
0x455DC3: add      al, cl
0x455DC5: pop      ebp
0x455DC6: inc      ebp
0x455DC7: add      byte ptr [eax], cl
0x455DC9: or       al, 0x54
0x455DCB: dec      edi
0x455DCC: outsb    dx, byte ptr [esi]
0x455DCD: dec      ebp
0x455DCE: outsd    dx, dword ptr [esi]
0x455DCF: jne      0x455e44
0x455DD1: inc      esp
0x455DD3: outsd    dx, dword ptr [esi]
0x455DD4: ja       0x455e44
0x455DD6: add      byte ptr [0x65530608], al
0x455DDC: outsb    dx, byte ptr [esi]
0x455DDD: jb       0x455de8
0x455DE1: push     esp
0x455DE2: dec      edi
0x455DE3: bound    ebp, qword ptr [edx + 0x65]
0x455DE6: arpl     word ptr [eax + eax + 6], si
0x455DEA: inc      edx
0x455DEB: jne      0x455e61
0x455DED: je       0x455e5e
0x455DEF: outsb    dx, byte ptr [esi]
0x455DF0: or       al, 0x54
0x455DF2: dec      ebp
0x455DF3: outsd    dx, dword ptr [esi]
0x455DF4: jne      0x455e69
0x455DF6: inc      edx
0x455DF8: jne      0x455e6e
0x455DFA: je       0x455e6b
0x455DFC: outsb    dx, byte ptr [esi]
0x455DFD: add      byte ptr [0x66696853], al
0x455E03: je       0x455e10
0x455E05: push     esp
0x455E06: push     ebx
0x455E07: push     0x53746669
0x455E0C: je       0x455e6f
0x455E0E: je       0x455e75 ; "Integer"
0x455E10: add      byte ptr [ecx], al
0x455E12: pop      eax
0x455E13: pop      es
0x455E14: dec      ecx
0x455E15: outsb    dx, byte ptr [esi]
0x455E16: je       0x455e7d
0x455E18: jb       0x455e1c ; "Y"
0x455E1C: add      dword ptr [ecx + 7], ebx
0x455E1F: dec      ecx
0x455E20: outsb    dx, byte ptr [esi]
0x455E21: je       0x455e88
0x455E23: jb       0x455db2
0x455E27: shr      byte ptr [esi + ebx*2], 0x45
0x455E2B: add      byte ptr [eax], cl
0x455E2D: or       dl, byte ptr [edi + ecx*2 + 0x6e]
0x455E31: dec      ebp
0x455E32: outsd    dx, dword ptr [esi]
0x455E33: jne      0x455ea8
0x455E35: push     ebp
0x455E37: jo       0x455e39
0x455E39: add      eax, 0x65530608
0x455E3E: outsb    dx, byte ptr [esi]
0x455E3F: jb       0x455e4a
0x455E43: push     esp
0x455E44: dec      edi
0x455E45: bound    ebp, qword ptr [edx + 0x65]
0x455E48: arpl     word ptr [eax + eax + 6], si
0x455E4C: inc      edx
0x455E4D: jne      0x455ec3
0x455E4F: je       0x455ec0
0x455E51: outsb    dx, byte ptr [esi]
0x455E52: or       al, 0x54
0x455E54: dec      ebp
0x455E55: outsd    dx, dword ptr [esi]
0x455E56: jne      0x455ecb ; "X"
0x455E58: inc      edx
0x455E5A: jne      0x455ed0
0x455E5C: je       0x455ecd ; "Integer"
0x455E5E: outsb    dx, byte ptr [esi]
0x455E5F: add      byte ptr [0x66696853], al
0x455E65: je       0x455e72
0x455E67: push     esp
0x455E68: push     ebx
0x455E69: push     0x53746669
0x455E6E: je       0x455ed1
0x455E70: je       0x455ed7
0x455E72: add      byte ptr [ecx], al
0x455E74: pop      eax
0x455E75: pop      es
0x455E76: dec      ecx
0x455E77: outsb    dx, byte ptr [esi]
0x455E78: je       0x455edf
0x455E7A: jb       0x455e7e ; "Y"
0x455E7E: add      dword ptr [ecx + 7], ebx
0x455E81: dec      ecx
0x455E82: outsb    dx, byte ptr [esi]
0x455E83: je       0x455eea
0x455E85: jb       0x455e15
0x455E89: pop      esi
0x455E8A: inc      ebp
0x455E8B: add      byte ptr [eax], cl
0x455E8D: or       byte ptr [edi + ecx*2 + 0x6e], dl
0x455E91: inc      ebx
0x455E92: insb     byte ptr es:[edi], dx
0x455E93: imul     esp, dword ptr [ebx + 0x6b], 0x6080100
0x455E9A: push     ebx
0x455E9B: outsb    dx, byte ptr gs:[esi]
0x455E9D: jb       0x455ea8
0x455EA1: push     esp
0x455EA2: dec      edi
0x455EA3: bound    ebp, qword ptr [edx + 0x65]
0x455EA6: arpl     word ptr [esp + ebp*4 + 0x5e], si
0x455EAA: inc      ebp
0x455EAB: add      byte ptr [eax], cl
0x455EAD: or       dl, byte ptr [edi + ecx*2 + 0x6e]
0x455EB1: inc      ebx
0x455EB2: insb     byte ptr es:[edi], dx
0x455EB3: imul     esp, dword ptr [ebx + 0x6b], 0x3007845
0x455EBA: or       byte ptr [esi], al
0x455EBC: push     ebx
0x455EBD: outsb    dx, byte ptr gs:[esi]
0x455EBF: jb       0x455eca
0x455EC3: push     esp
0x455EC4: dec      edi
0x455EC5: bound    ebp, qword ptr [edx + 0x65]
0x455EC8: arpl     word ptr [eax + eax + 1], si
0x455ECC: pop      eax
0x455ECD: pop      es
0x455ECE: dec      ecx
0x455ECF: outsb    dx, byte ptr [esi]
0x455ED0: je       0x455f37
0x455ED2: jb       0x455ed6 ; "Y"
0x455ED6: add      dword ptr [ecx + 7], ebx
0x455ED9: dec      ecx
0x455EDA: outsb    dx, byte ptr [esi]
0x455EDB: je       0x455f42
0x455EDD: jb       0x455ec5
0x455EE1: pop      esi
0x455EE2: inc      ebp
0x455EE3: add      byte ptr [eax], cl
0x455EE5: or       eax, 0x496e4f54
0x455EEA: outsb    dx, byte ptr [esi]
0x455EEB: push     edx
0x455EEC: popal    
0x455EEE: insb     byte ptr es:[edi], dx
0x455EEF: inc      ecx
0x455EF0: jb       0x455f57
0x455EF2: popal    
0x455EF3: add      byte ptr [eax + ecx], al
0x455EF6: push     es
0x455EF7: push     ebx
0x455EF8: outsb    dx, byte ptr gs:[esi]
0x455EFA: jb       0x455f05
0x455EFE: push     esp
0x455EFF: dec      edi
0x455F00: bound    ebp, qword ptr [edx + 0x65]
0x455F03: arpl     word ptr [eax + eax + 1], si
0x455F07: pop      eax
0x455F08: pop      es
0x455F09: dec      ecx
0x455F0A: outsb    dx, byte ptr [esi]
0x455F0B: je       0x455f72 ; "Shift"
0x455F0D: jb       0x455f11 ; "Y"
0x455F11: add      dword ptr [ecx + 7], ebx
0x455F14: dec      ecx
0x455F15: outsb    dx, byte ptr [esi]
0x455F16: je       0x455f7d
0x455F18: jb       0x455f1d
0x455F1C: or       cl, byte ptr [ecx + 0x73]
0x455F1F: push     edx
0x455F20: popal    
0x455F22: insb     byte ptr es:[edi], dx
0x455F23: inc      ecx
0x455F24: jb       0x455f8b
0x455F26: popal    
0x455F27: pop      es
0x455F28: inc      edx
0x455F29: outsd    dx, dword ptr [esi]
0x455F2A: outsd    dx, dword ptr [esi]
0x455F2B: insb     byte ptr es:[edi], dx
0x455F2C: popal    
0x455F2E: outsb    dx, byte ptr [esi]
0x455F2F: nop      
0x455F30: xor      al, 0x5f
0x455F32: inc      ebp
0x455F33: add      byte ptr [eax], cl
0x455F35: or       eax, 0x476e4f54
0x455F3A: jb       0x455fa5
0x455F3C: push     ebx
0x455F3E: insb     byte ptr es:[edi], dx
0x455F40: arpl     word ptr gs:[eax + eax + 4], si
0x455F45: or       byte ptr [esi], al
0x455F47: push     ebx
0x455F48: outsb    dx, byte ptr gs:[esi]
0x455F4A: jb       0x455f55
0x455F4E: push     esp
0x455F4F: dec      edi
0x455F50: bound    ebp, qword ptr [edx + 0x65]
0x455F53: arpl     word ptr [eax + eax + 4], si
0x455F57: inc      ecx
0x455F58: inc      ebx
0x455F59: outsd    dx, dword ptr [esi]
0x455F5A: insb     byte ptr es:[edi], dx
0x455F5B: pop      es
0x455F5C: dec      ecx
0x455F5D: outsb    dx, byte ptr [esi]
0x455F5E: je       0x455fc5 ; "Rect"
0x455F60: jb       0x455f64 ; "ARow"
0x455F64: add      al, 0x41
0x455F66: push     edx
0x455F67: outsd    dx, dword ptr [esi]
0x455F68: ja       0x455f71
0x455F6A: dec      ecx
0x455F6B: outsb    dx, byte ptr [esi]
0x455F6C: je       0x455fd3
0x455F6E: jb       0x455f72 ; "Shift"
0x455F72: add      eax, 0x66696853
0x455F77: je       0x455f84
0x455F79: push     esp
0x455F7A: push     ebx
0x455F7B: push     0x53746669
0x455F80: je       0x455fe3
0x455F82: je       0x455fe9
0x455F84: mov      byte ptr [edi + 0x45], bl
0x455F87: add      byte ptr [eax], cl
0x455F89: or       al, 0x54
0x455F8B: dec      edi
0x455F8C: outsb    dx, byte ptr [esi]
0x455F8D: inc      edi
0x455F8E: jb       0x455ff9
0x455F90: push     eax
0x455F92: popal    
0x455F93: imul     ebp, dword ptr [esi + 0x74], 0x6080600
0x455F9A: push     ebx
0x455F9B: outsb    dx, byte ptr gs:[esi]
0x455F9D: jb       0x455fa8
0x455FA1: push     esp
0x455FA2: dec      edi
0x455FA3: bound    ebp, qword ptr [edx + 0x65]
0x455FA6: arpl     word ptr [eax + eax + 4], si
0x455FAA: inc      ecx
0x455FAB: inc      ebx
0x455FAC: outsd    dx, dword ptr [esi]
0x455FAD: insb     byte ptr es:[edi], dx
0x455FAE: pop      es
0x455FAF: dec      ecx
0x455FB0: outsb    dx, byte ptr [esi]
0x455FB1: je       0x456018
0x455FB3: jb       0x455fb7 ; "ARow"
0x455FB7: add      al, 0x41
0x455FB9: push     edx
0x455FBA: outsd    dx, dword ptr [esi]
0x455FBB: ja       0x455fc4
0x455FBD: dec      ecx
0x455FBE: outsb    dx, byte ptr [esi]
0x455FBF: je       0x456026
0x455FC1: jb       0x455fd5
0x455FC5: add      al, 0x52
0x455FC7: arpl     word ptr gs:[ebp + eax + 0x54], si
0x455FCC: push     edx
0x455FCD: arpl     word ptr gs:[eax + eax + 5], si
0x455FD2: push     ebx
0x455FD3: je       0x456036
0x455FD5: je       0x45603c
0x455FD7: push     cs
0x455FD8: push     esp
0x455FD9: inc      edi
0x455FDA: jb       0x456045
0x455FDC: inc      esp
0x455FDE: jb       0x456041
0x455FE0: ja       0x456035 ; "TClickSound"
0x455FE2: je       0x456045
0x455FE4: je       0x45604b
0x455FE6: or       byte ptr [eax], cl
0x455FE8: jae      0x456060
0x455FEB: jb       0x456053
0x455FED: popal    
0x455FEE: arpl     word ptr [ebp + 0x12], sp
0x455FF1: push     esp
0x455FF2: inc      esp
0x455FF3: imul     esi, dword ptr [edx + 0x65], 0x72447463
0x455FFA: popal    
0x455FFB: ja       0x456050
0x455FFD: jne      0x456071
0x455FFF: popaw    
0x456001: arpl     word ptr [ebp - 0x70], sp
0x456004: or       byte ptr [eax + 0x45], ah
0x456007: add      byte ptr [eax], cl
0x456009: or       eax, 0x436e4f54
0x45600E: insb     byte ptr es:[edi], dx
0x45600F: imul     esp, dword ptr [ebx + 0x6b], 0x6e756f53
0x456016: add      byte ptr fs:[edx], al
0x456019: or       byte ptr [esi], al
0x45601B: push     ebx
0x45601C: outsb    dx, byte ptr gs:[esi]
0x45601E: jb       0x456029
0x456022: push     esp
0x456023: dec      edi
0x456024: bound    ebp, qword ptr [edx + 0x65]
0x456027: arpl     word ptr [eax + eax + 0xa], si
0x45602B: inc      ebx
0x45602C: insb     byte ptr es:[edi], dx
0x45602D: imul     esp, dword ptr [ebx + 0x6b], 0x6e756f73
0x456034: or       edx, dword ptr fs:[ebx + eax*2 + 0x6c]
0x456039: imul     esp, dword ptr [ebx + 0x6b], 0x6e756f53
0x456040: lea      eax, fs:[eax]
0x456044: nop      
0x456045: pushal   
0x456046: inc      ebp
0x456047: add      byte ptr [eax], al
0x456049: add      byte ptr [eax], al
0x45604B: add      byte ptr [eax], al
0x45604D: add      byte ptr [eax], al
0x45604F: add      byte ptr [ecx + 0x45], cl
0x456053: add      byte ptr [eax + 0x4561], ah
0x456059: add      byte ptr [eax], al
0x45605B: add      byte ptr [eax], al
0x45605D: add      byte ptr [eax], al
0x45605F: add      byte ptr [esi + 0x61], bl
0x456062: inc      ebp
0x456063: add      byte ptr [eax + 0x60004561], dl
0x456069: add      al, byte ptr [eax]
0x45606B: add      byte ptr [eax], dl
0x45606D: inc      edi
0x45606E: inc      edx
0x45606F: add      byte ptr [eax + edx*2 + 0x30d40041], bl
0x456076: inc      eax
0x456077: add      al, bl
0x456079: xor      byte ptr [eax], al
0x45607C: fdiv     qword ptr [eax]
0x45607E: inc      eax
0x45607F: add      ah, al
0x456081: mov      bl, 0x42
0x456083: add      byte ptr [eax + 0x2e], al
0x456086: inc      eax
0x456087: add      byte ptr [esi + ebp + 0x40], dl
0x45608B: add      byte ptr [esp + ebp*2 + 0x45], bh
