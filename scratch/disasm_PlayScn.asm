
; === VMT_TPlayScene @ 0x46EDB8 ===
0x46EDB8: push     es
0x46EDB9: add      bh, bh
0x46EDBE: std      
0x46EDC0: cld      
0x46EDC2: sti      
0x46EDC3: push     esi
0x46EDC5: dec      dword ptr [eax - 0x73ffb90f]
0x46EDCB: inc      esi
0x46EDCD: add      al, dl
0x46EDCF: inc      esi
0x46EDD1: add      byte ptr [ebx + esi*8], dl
0x46EDD4: inc      esi
0x46EDD5: add      byte ptr [ebx + esi*8 + 0x46], al
0x46EDD9: add      byte ptr [eax + eax + 0x540a0047], bl
0x46EDE0: push     eax
0x46EDE1: insb     byte ptr es:[edi], dx
0x46EDE2: popal    
0x46EDE3: jns      0x46ee38
0x46EDE5: arpl     word ptr [ebp + 0x6e], sp
0x46EDE8: lea      eax, gs:[eax]
0x46EDEC: push     ebp
0x46EDED: mov      ebp, esp
0x46EDEF: add      esp, -0x10
0x46EDF2: test     dl, dl
0x46EDF4: je       0x46edfe
0x46EDF6: add      esp, -0x10
0x46EDF9: call     0x4031a4
0x46EDFE: mov      byte ptr [ebp - 5], dl
0x46EE01: mov      dword ptr [ebp - 4], eax
0x46EE04: mov      eax, dword ptr [ebp - 4]
0x46EE07: xor      edx, edx
0x46EE09: mov      dword ptr [eax + 8], edx
0x46EE0C: mov      eax, dword ptr [ebp - 4]
0x46EE0F: xor      edx, edx
0x46EE11: mov      dword ptr [eax + 0xc], edx
0x46EE14: mov      dl, 1
0x46EE16: mov      eax, dword ptr [0x40df00]
0x46EE1B: call     0x402e7c
0x46EE20: mov      edx, dword ptr [ebp - 4]
0x46EE23: mov      dword ptr [edx + 0x573fc], eax
0x46EE29: mov      dl, 1
0x46EE2B: mov      eax, dword ptr [0x40df00]
0x46EE30: call     0x402e7c
0x46EE35: mov      edx, dword ptr [ebp - 4]
0x46EE38: mov      dword ptr [edx + 0x5a854], eax
0x46EE3E: mov      dl, 1
0x46EE40: mov      eax, dword ptr [0x40df00]
0x46EE45: call     0x402e7c
0x46EE4A: mov      edx, dword ptr [ebp - 4]
0x46EE4D: mov      dword ptr [edx + 0x5a858], eax
0x46EE53: mov      dl, 1
0x46EE55: mov      eax, dword ptr [0x40df00]
0x46EE5A: call     0x402e7c
0x46EE5F: mov      edx, dword ptr [ebp - 4]
0x46EE62: mov      dword ptr [edx + 0x5a85c], eax
0x46EE68: mov      dl, 1
0x46EE6A: mov      eax, dword ptr [0x40df00]
0x46EE6F: call     0x402e7c
0x46EE74: mov      edx, dword ptr [ebp - 4]
0x46EE77: mov      dword ptr [edx + 0x5a860], eax
0x46EE7D: mov      dl, 1
0x46EE7F: mov      eax, dword ptr [0x40df00]
0x46EE84: call     0x402e7c
0x46EE89: mov      edx, dword ptr [ebp - 4]
0x46EE8C: mov      dword ptr [edx + 0x5a864], eax
0x46EE92: call     0x406154
0x46EE97: mov      edx, dword ptr [ebp - 4]
0x46EE9A: mov      dword ptr [edx + 0x5a868], eax
0x46EEA0: mov      eax, dword ptr [ebp - 4]
0x46EEA3: mov      byte ptr [eax + 0x5a86c], 0
0x46EEAA: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x46EEAF: mov      eax, dword ptr [eax]
0x46EEB1: mov      ecx, dword ptr [eax + 4]
0x46EEB4: mov      dl, 1
0x46EEB6: mov      eax, dword ptr [0x41f814]
0x46EEBB: call     0x4219b8
0x46EEC0: mov      edx, dword ptr [ebp - 4]
0x46EEC3: mov      dword ptr [edx + 0x5a850], eax
0x46EEC9: mov      eax, dword ptr [ebp - 4]
0x46EECC: mov      eax, dword ptr [eax + 0x5a850]
0x46EED2: mov      dword ptr [ebp - 0xc], eax
0x46EED5: mov      edx, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x46EEDB: mov      edx, dword ptr [edx]
0x46EEDD: mov      eax, dword ptr [ebp - 0xc]
0x46EEE0: mov      ecx, dword ptr [eax]
0x46EEE2: call     dword ptr [ecx + 0x64]
0x46EEE5: xor      edx, edx
0x46EEE7: mov      eax, dword ptr [ebp - 0xc]
0x46EEEA: call     0x421a94
0x46EEEF: mov      eax, dword ptr [ebp - 4]
0x46EEF2: mov      edx, dword ptr [ebp - 0xc]
0x46EEF5: mov      dword ptr [edx + 0x1bc], eax
0x46EEFB: mov      dword ptr [edx + 0x1b8], 0x46f0c4
0x46EF05: xor      edx, edx
0x46EF07: mov      eax, dword ptr [ebp - 0xc]
0x46EF0A: call     0x4278c0
0x46EF0F: mov      edx, 0x46
0x46EF14: mov      eax, dword ptr [ebp - 0xc]
0x46EF17: call     0x421ae0
0x46EF1C: xor      edx, edx
0x46EF1E: mov      eax, dword ptr [ebp - 0xc]
0x46EF21: call     0x42d874
0x46EF26: mov      edx, 0xd0
0x46EF2B: mov      eax, dword ptr [ebp - 0xc]
0x46EF2E: call     0x42718c
0x46EF33: mov      edx, 0x245
0x46EF38: mov      eax, dword ptr [ebp - 0xc]
0x46EF3B: call     0x4271b0
0x46EF40: mov      edx, 0xc
0x46EF45: mov      eax, dword ptr [ebp - 0xc]
0x46EF48: call     0x4271fc
0x46EF4D: mov      edx, 0x183
0x46EF52: mov      eax, dword ptr [ebp - 0xc]
0x46EF55: call     0x4271d8
0x46EF5A: mov      edx, 0xc0c0c0
0x46EF5F: mov      eax, dword ptr [ebp - 0xc]
0x46EF62: call     0x427b5c
0x46EF67: call     0x406154
0x46EF6C: mov      edx, dword ptr [ebp - 4]
0x46EF6F: mov      dword ptr [edx + 0x573e4], eax
0x46EF75: call     0x406154
0x46EF7A: mov      edx, dword ptr [ebp - 4]
0x46EF7D: mov      dword ptr [edx + 0x573ec], eax
0x46EF83: mov      eax, dword ptr [0x4a4304] ; *0x4A4304->0x4F9244
0x46EF88: xor      edx, edx
0x46EF8A: mov      dword ptr [eax], edx
0x46EF8C: mov      eax, dword ptr [ebp - 4]
0x46EF8F: xor      edx, edx
0x46EF91: mov      dword ptr [eax + 0x573e8], edx
0x46EF97: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x46EF9C: mov      eax, dword ptr [eax]
0x46EF9E: mov      ecx, dword ptr [eax + 4]
0x46EFA1: mov      dl, 1
0x46EFA3: mov      eax, dword ptr [0x444e18]
0x46EFA8: call     0x44544c
0x46EFAD: mov      edx, dword ptr [ebp - 4]
0x46EFB0: mov      dword ptr [edx + 0x573f8], eax
0x46EFB6: mov      eax, dword ptr [ebp - 4]
0x46EFB9: mov      eax, dword ptr [eax + 0x573f8]
0x46EFBF: mov      dword ptr [ebp - 0x10], eax
0x46EFC2: mov      eax, dword ptr [ebp - 4]
0x46EFC5: push     eax
0x46EFC6: push     0x46f094
0x46EFCB: mov      eax, dword ptr [ebp - 0x10]
0x46EFCE: call     0x4455f4
0x46EFD3: mov      edx, 1
0x46EFD8: mov      eax, dword ptr [ebp - 0x10]
0x46EFDB: call     0x4455e4
0x46EFE0: xor      edx, edx
0x46EFE2: mov      eax, dword ptr [ebp - 0x10]
0x46EFE5: call     0x4455d4
0x46EFEA: mov      eax, dword ptr [ebp - 4]
0x46EFED: cmp      byte ptr [ebp - 5], 0
0x46EFF1: je       0x46f002
0x46EFF3: call     0x4031fc
0x46EFF8: pop      dword ptr fs:[0]
0x46EFFF: add      esp, 0xc
0x46F002: mov      eax, dword ptr [ebp - 4]
0x46F005: mov      esp, ebp
0x46F007: pop      ebp
0x46F008: ret      
0x46F009: lea      eax, [eax]
0x46F00C: push     ebp
0x46F00D: mov      ebp, esp
0x46F00F: add      esp, -8
0x46F012: call     0x403204
0x46F017: mov      byte ptr [ebp - 5], dl
0x46F01A: mov      dword ptr [ebp - 4], eax
0x46F01D: mov      eax, dword ptr [ebp - 4]
0x46F020: mov      eax, dword ptr [eax + 0x573fc]
0x46F026: call     0x402eac
0x46F02B: mov      eax, dword ptr [ebp - 4]
0x46F02E: mov      eax, dword ptr [eax + 0x5a854]
0x46F034: call     0x402eac
0x46F039: mov      eax, dword ptr [ebp - 4]
0x46F03C: mov      eax, dword ptr [eax + 0x5a858]
0x46F042: call     0x402eac
0x46F047: mov      eax, dword ptr [ebp - 4]
0x46F04A: mov      eax, dword ptr [eax + 0x5a85c]
0x46F050: call     0x402eac
0x46F055: mov      eax, dword ptr [ebp - 4]
0x46F058: mov      eax, dword ptr [eax + 0x5a860]
0x46F05E: call     0x402eac
0x46F063: mov      eax, dword ptr [ebp - 4]
0x46F066: mov      eax, dword ptr [eax + 0x5a864]
0x46F06C: call     0x402eac
0x46F071: mov      dl, byte ptr [ebp - 5]
0x46F074: and      dl, 0xfc
0x46F077: mov      eax, dword ptr [ebp - 4]
0x46F07A: call     0x402e9c
0x46F07F: cmp      byte ptr [ebp - 5], 0
0x46F083: jle      0x46f08d
0x46F085: mov      eax, dword ptr [ebp - 4]
0x46F088: call     0x4031f4
0x46F08D: pop      ecx
0x46F08E: pop      ecx
0x46F08F: pop      ebp
0x46F090: ret      
0x46F091: lea      eax, [eax]
0x46F094: push     ebp
0x46F095: mov      ebp, esp
0x46F097: add      esp, -8
0x46F09A: mov      dword ptr [ebp - 8], edx
0x46F09D: mov      dword ptr [ebp - 4], eax
0x46F0A0: mov      eax, 0x66
0x46F0A5: call     0x495794
0x46F0AA: mov      edx, 0xb3b0
0x46F0AF: mov      eax, dword ptr [ebp - 4]
0x46F0B2: mov      eax, dword ptr [eax + 0x573f8]
0x46F0B8: call     0x4455e4
0x46F0BD: pop      ecx
0x46F0BE: pop      ecx
0x46F0BF: pop      ebp
0x46F0C0: ret      
0x46F0C1: lea      eax, [eax]
0x46F0C4: push     ebp
0x46F0C5: mov      ebp, esp
0x46F0C7: add      esp, -0x10
0x46F0CA: push     ebx
0x46F0CB: xor      ebx, ebx
0x46F0CD: mov      dword ptr [ebp - 0x10], ebx
0x46F0D0: mov      dword ptr [ebp - 8], ecx
0x46F0D3: mov      dword ptr [ebp - 0xc], edx
0x46F0D6: mov      dword ptr [ebp - 4], eax
0x46F0D9: xor      eax, eax
0x46F0DB: push     ebp
0x46F0DC: push     0x46f179
0x46F0E1: push     dword ptr fs:[eax]
0x46F0E4: mov      dword ptr fs:[eax], esp
0x46F0E7: mov      eax, dword ptr [ebp - 8]
0x46F0EA: cmp      byte ptr [eax], 0xd
0x46F0ED: jne      0x46f135
0x46F0EF: lea      edx, [ebp - 0x10]
0x46F0F2: mov      eax, dword ptr [ebp - 4]
0x46F0F5: mov      eax, dword ptr [eax + 0x5a850]
0x46F0FB: call     0x4279d0
0x46F100: mov      edx, dword ptr [ebp - 0x10]
0x46F103: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x46F108: mov      eax, dword ptr [eax]
0x46F10A: call     0x46549c
0x46F10F: xor      edx, edx
0x46F111: mov      eax, dword ptr [ebp - 4]
0x46F114: mov      eax, dword ptr [eax + 0x5a850]
0x46F11A: call     0x427a00
0x46F11F: xor      edx, edx
0x46F121: mov      eax, dword ptr [ebp - 4]
0x46F124: mov      eax, dword ptr [eax + 0x5a850]
0x46F12A: call     0x4278c0
0x46F12F: mov      eax, dword ptr [ebp - 8]
0x46F132: mov      byte ptr [eax], 0
0x46F135: mov      eax, dword ptr [ebp - 8]
0x46F138: cmp      byte ptr [eax], 0x1b
0x46F13B: jne      0x46f163
0x46F13D: xor      edx, edx
0x46F13F: mov      eax, dword ptr [ebp - 4]
0x46F142: mov      eax, dword ptr [eax + 0x5a850]
0x46F148: call     0x427a00
0x46F14D: xor      edx, edx
0x46F14F: mov      eax, dword ptr [ebp - 4]
0x46F152: mov      eax, dword ptr [eax + 0x5a850]
0x46F158: call     0x4278c0
0x46F15D: mov      eax, dword ptr [ebp - 8]
0x46F160: mov      byte ptr [eax], 0
0x46F163: xor      eax, eax
0x46F165: pop      edx
0x46F166: pop      ecx
0x46F167: pop      ecx
0x46F168: mov      dword ptr fs:[eax], edx
0x46F16B: push     0x46f180
0x46F170: lea      eax, [ebp - 0x10]
0x46F173: call     0x403a58
0x46F178: ret      
0x46F179: jmp      0x403518
0x46F17E: jmp      0x46f170
0x46F180: pop      ebx
0x46F181: mov      esp, ebp
0x46F183: pop      ebp
0x46F184: ret      
0x46F185: lea      eax, [eax]
0x46F188: push     ebp
0x46F189: mov      ebp, esp
0x46F18B: add      esp, -8
0x46F18E: mov      dword ptr [ebp - 4], eax
0x46F191: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x46F196: mov      eax, dword ptr [eax]
0x46F198: mov      eax, dword ptr [eax + 0x2f4]
0x46F19E: mov      ecx, dword ptr [eax + 0x268]
0x46F1A4: mov      dl, 1
0x46F1A6: mov      eax, dword ptr [0x44b6a0]
0x46F1AB: call     0x44d190
0x46F1B0: mov      edx, dword ptr [ebp - 4]
0x46F1B3: mov      dword ptr [edx + 8], eax
0x46F1B6: mov      eax, dword ptr [ebp - 4]
0x46F1B9: mov      eax, dword ptr [eax + 8]
0x46F1BC: mov      byte ptr [eax + 0x18], 1
0x46F1C0: mov      ecx, 0x23d
0x46F1C5: mov      edx, 0x3fe
0x46F1CA: mov      eax, dword ptr [ebp - 4]
0x46F1CD: mov      eax, dword ptr [eax + 8]
0x46F1D0: call     0x44e480
0x46F1D5: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x46F1DA: mov      eax, dword ptr [eax]
0x46F1DC: mov      eax, dword ptr [eax + 0x2f4]
0x46F1E2: mov      ecx, dword ptr [eax + 0x268]
0x46F1E8: mov      dl, 1
0x46F1EA: mov      eax, dword ptr [0x44b6a0]
0x46F1EF: call     0x44d190
0x46F1F4: mov      edx, dword ptr [ebp - 4]
0x46F1F7: mov      dword ptr [edx + 0xc], eax
0x46F1FA: mov      eax, dword ptr [ebp - 4]
0x46F1FD: mov      eax, dword ptr [eax + 0xc]
0x46F200: mov      byte ptr [eax + 0x18], 1
0x46F204: mov      ecx, 0x1bd
0x46F209: mov      edx, 0x320
0x46F20E: mov      eax, dword ptr [ebp - 4]
0x46F211: mov      eax, dword ptr [eax + 0xc]
0x46F214: call     0x44e480
0x46F219: mov      eax, dword ptr [ebp - 4]
0x46F21C: mov      dword ptr [eax + 0x57394], 0x320
0x46F226: mov      eax, dword ptr [ebp - 4]
0x46F229: mov      dword ptr [eax + 0x57398], 0x1bd
0x46F233: mov      eax, dword ptr [ebp - 4]
0x46F236: add      eax, 0x10
0x46F239: mov      edx, dword ptr [ebp - 4]
0x46F23C: mov      dword ptr [edx + 0x57390], eax
0x46F242: mov      edx, 0x56ea0
0x46F247: mov      eax, dword ptr [ebp - 4]
0x46F24A: mov      eax, dword ptr [eax + 0x57390]
0x46F250: call     0x406984
0x46F255: mov      eax, dword ptr [0x4a3f1c] ; *0x4A3F1C->0x4F9382
0x46F25A: mov      byte ptr [eax], 0
0x46F25D: xor      eax, eax
0x46F25F: mov      dword ptr [ebp - 8], eax
0x46F262: mov      eax, dword ptr [ebp - 8]
0x46F265: lea      eax, [eax + eax*2]
0x46F268: mov      edx, dword ptr [ebp - 4]
0x46F26B: xor      ecx, ecx
0x46F26D: mov      dword ptr [edx + eax*4 + 0x573a4], ecx
0x46F274: inc      dword ptr [ebp - 8]
0x46F277: cmp      dword ptr [ebp - 8], 6
0x46F27B: jne      0x46f262
0x46F27D: mov      eax, dword ptr [ebp - 4]
0x46F280: call     0x46f7ac
0x46F285: pop      ecx
0x46F286: pop      ecx
0x46F287: pop      ebp
0x46F288: ret      
0x46F289: lea      eax, [eax]
0x46F28C: push     ebp
0x46F28D: mov      ebp, esp
0x46F28F: push     ecx
0x46F290: mov      dword ptr [ebp - 4], eax
0x46F293: mov      eax, dword ptr [ebp - 4]
0x46F296: cmp      dword ptr [eax + 8], 0
0x46F29A: je       0x46f2a7
0x46F29C: mov      eax, dword ptr [ebp - 4]
0x46F29F: mov      eax, dword ptr [eax + 8]
0x46F2A2: call     0x402eac
0x46F2A7: mov      eax, dword ptr [ebp - 4]
0x46F2AA: cmp      dword ptr [eax + 0xc], 0
0x46F2AE: je       0x46f2bb
0x46F2B0: mov      eax, dword ptr [ebp - 4]
0x46F2B3: mov      eax, dword ptr [eax + 0xc]
0x46F2B6: call     0x402eac
0x46F2BB: mov      eax, dword ptr [ebp - 4]
0x46F2BE: xor      edx, edx
0x46F2C0: mov      dword ptr [eax + 8], edx
0x46F2C3: mov      eax, dword ptr [ebp - 4]
0x46F2C6: xor      edx, edx
0x46F2C8: mov      dword ptr [eax + 0xc], edx
0x46F2CB: pop      ecx
0x46F2CC: pop      ebp
0x46F2CD: ret      
0x46F2CE: mov      eax, eax
0x46F2D0: push     ebp
0x46F2D1: mov      ebp, esp
0x46F2D3: push     ecx
0x46F2D4: mov      dword ptr [ebp - 4], eax
0x46F2D7: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x46F2DC: mov      eax, dword ptr [eax]
0x46F2DE: mov      eax, dword ptr [eax + 0x308]
0x46F2E4: call     0x453828
0x46F2E9: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46F2EE: mov      eax, dword ptr [eax]
0x46F2F0: mov      dl, 1
0x46F2F2: call     0x486540
0x46F2F7: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x46F2FC: mov      eax, dword ptr [eax]
0x46F2FE: call     0x42d734
0x46F303: mov      edx, dword ptr [0x4a3e30] ; *0x4A3E30->0x4A2CE0
0x46F309: mov      dl, byte ptr [edx]
0x46F30B: call     0x431bb0
0x46F310: pop      ecx
0x46F311: pop      ebp
0x46F312: ret      
0x46F313: nop      
0x46F314: push     ebp
0x46F315: mov      ebp, esp
0x46F317: push     ecx
0x46F318: mov      dword ptr [ebp - 4], eax
0x46F31B: call     0x495928
0x46F320: xor      edx, edx
0x46F322: mov      eax, dword ptr [ebp - 4]
0x46F325: mov      eax, dword ptr [eax + 0x5a850]
0x46F32B: call     0x4278c0
0x46F330: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46F335: mov      eax, dword ptr [eax]
0x46F337: xor      edx, edx
0x46F339: call     0x486540
0x46F33E: pop      ecx
0x46F33F: pop      ebp
0x46F340: ret      
0x46F341: lea      eax, [eax]
0x46F344: push     ebp
0x46F345: mov      ebp, esp
0x46F347: push     ecx
0x46F348: mov      dword ptr [ebp - 4], eax
0x46F34B: pop      ecx
0x46F34C: pop      ebp
0x46F34D: ret      
0x46F34E: mov      eax, eax
0x46F350: push     ebp
0x46F351: mov      ebp, esp
0x46F353: add      esp, -0xc
0x46F356: push     ebx
0x46F357: mov      dword ptr [ebp - 4], eax
0x46F35A: mov      eax, dword ptr [ebp - 4]
0x46F35D: mov      eax, dword ptr [eax + 0x5a854]
0x46F363: mov      eax, dword ptr [eax + 8]
0x46F366: dec      eax
0x46F367: cmp      eax, 0
0x46F36A: jl       0x46f3ba
0x46F36C: mov      dword ptr [ebp - 8], eax
0x46F36F: mov      edx, dword ptr [ebp - 8]
0x46F372: mov      eax, dword ptr [ebp - 4]
0x46F375: mov      eax, dword ptr [eax + 0x5a854]
0x46F37B: call     0x40f6a0
0x46F380: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x46F386: cmp      eax, dword ptr [edx]
0x46F388: je       0x46f3b1
0x46F38A: mov      edx, dword ptr [ebp - 8]
0x46F38D: mov      eax, dword ptr [ebp - 4]
0x46F390: mov      eax, dword ptr [eax + 0x5a854]
0x46F396: call     0x40f6a0
0x46F39B: call     0x402eac
0x46F3A0: mov      edx, dword ptr [ebp - 8]
0x46F3A3: mov      eax, dword ptr [ebp - 4]
0x46F3A6: mov      eax, dword ptr [eax + 0x5a854]
0x46F3AC: call     0x40f5c4
0x46F3B1: dec      dword ptr [ebp - 8]
0x46F3B4: cmp      dword ptr [ebp - 8], -1
0x46F3B8: jne      0x46f36f
0x46F3BA: mov      eax, dword ptr [ebp - 4]
0x46F3BD: mov      eax, dword ptr [eax + 0x573fc]
0x46F3C3: mov      bx, 0xffff
0x46F3C7: call     0x403080
0x46F3CC: mov      eax, dword ptr [0x4a42c0] ; *0x4A42C0->0x4F94A4
0x46F3D1: xor      edx, edx
0x46F3D3: mov      dword ptr [eax], edx
0x46F3D5: mov      eax, dword ptr [0x4a3f10] ; *0x4A3F10->0x4F94A8
0x46F3DA: xor      edx, edx
0x46F3DC: mov      dword ptr [eax], edx
0x46F3DE: mov      eax, dword ptr [0x4a414c] ; *0x4A414C->0x4F94AC
0x46F3E3: xor      edx, edx
0x46F3E5: mov      dword ptr [eax], edx
0x46F3E7: mov      eax, dword ptr [ebp - 4]
0x46F3EA: mov      eax, dword ptr [eax + 0x5a85c]
0x46F3F0: mov      eax, dword ptr [eax + 8]
0x46F3F3: dec      eax
0x46F3F4: test     eax, eax
0x46F3F6: jl       0x46f421
0x46F3F8: inc      eax
0x46F3F9: mov      dword ptr [ebp - 0xc], eax
0x46F3FC: mov      dword ptr [ebp - 8], 0
0x46F403: mov      edx, dword ptr [ebp - 8]
0x46F406: mov      eax, dword ptr [ebp - 4]
0x46F409: mov      eax, dword ptr [eax + 0x5a85c]
0x46F40F: call     0x40f6a0
0x46F414: call     0x402eac
0x46F419: inc      dword ptr [ebp - 8]
0x46F41C: dec      dword ptr [ebp - 0xc]
0x46F41F: jne      0x46f403
0x46F421: mov      eax, dword ptr [ebp - 4]
0x46F424: mov      eax, dword ptr [eax + 0x5a85c]
0x46F42A: mov      bx, 0xffff
0x46F42E: call     0x403080
0x46F433: mov      eax, dword ptr [ebp - 4]
0x46F436: mov      eax, dword ptr [eax + 0x5a860]
0x46F43C: mov      eax, dword ptr [eax + 8]
0x46F43F: dec      eax
0x46F440: test     eax, eax
0x46F442: jl       0x46f46d
0x46F444: inc      eax
0x46F445: mov      dword ptr [ebp - 0xc], eax
0x46F448: mov      dword ptr [ebp - 8], 0
0x46F44F: mov      edx, dword ptr [ebp - 8]
0x46F452: mov      eax, dword ptr [ebp - 4]
0x46F455: mov      eax, dword ptr [eax + 0x5a860]
0x46F45B: call     0x40f6a0
0x46F460: call     0x402eac
0x46F465: inc      dword ptr [ebp - 8]
0x46F468: dec      dword ptr [ebp - 0xc]
0x46F46B: jne      0x46f44f
0x46F46D: mov      eax, dword ptr [ebp - 4]
0x46F470: mov      eax, dword ptr [eax + 0x5a860]
0x46F476: mov      bx, 0xffff
0x46F47A: call     0x403080
0x46F47F: pop      ebx
0x46F480: mov      esp, ebp
0x46F482: pop      ebp
0x46F483: ret      
0x46F484: push     ebp
0x46F485: mov      ebp, esp
0x46F487: add      esp, -0x3c
0x46F48A: push     esi
0x46F48B: push     edi
0x46F48C: mov      dword ptr [ebp - 4], eax
0x46F48F: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x46F494: mov      eax, dword ptr [eax]
0x46F496: mov      eax, dword ptr [eax + 0x2ae54]
0x46F49C: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x46F4A2: mov      edx, dword ptr [edx]
0x46F4A4: cmp      eax, dword ptr [edx + 0x2ae64]
0x46F4AA: jne      0x46f4cd
0x46F4AC: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x46F4B1: mov      eax, dword ptr [eax]
0x46F4B3: mov      eax, dword ptr [eax + 0x2ae58]
0x46F4B9: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x46F4BF: mov      edx, dword ptr [edx]
0x46F4C1: cmp      eax, dword ptr [edx + 0x2ae68]
0x46F4C7: je       0x46f7a3
0x46F4CD: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x46F4D2: mov      eax, dword ptr [eax]
0x46F4D4: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x46F4DA: mov      edx, dword ptr [edx]
0x46F4DC: lea      esi, [eax + 0x2ae54]
0x46F4E2: lea      edi, [edx + 0x2ae64]
0x46F4E8: mov      ecx, 4
0x46F4ED: rep movsd dword ptr es:[edi], dword ptr [esi]
0x46F4EF: mov      eax, dword ptr [ebp - 4]
0x46F4F2: mov      eax, dword ptr [eax + 8]
0x46F4F5: xor      edx, edx
0x46F4F7: call     0x44d430
0x46F4FC: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x46F501: mov      eax, dword ptr [eax]
0x46F503: add      eax, 0x2ae54
0x46F508: mov      dword ptr [ebp - 0x20], eax
0x46F50B: mov      dword ptr [ebp - 0x10], 0xffffffc0
0x46F512: mov      eax, dword ptr [ebp - 0x20]
0x46F515: mov      eax, dword ptr [eax + 4]
0x46F518: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x46F51E: mov      edx, dword ptr [edx]
0x46F520: sub      eax, dword ptr [edx + 0x2ae78]
0x46F526: dec      eax
0x46F527: mov      edx, dword ptr [ebp - 0x20]
0x46F52A: mov      edx, dword ptr [edx + 0xc]
0x46F52D: mov      ecx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x46F533: mov      ecx, dword ptr [ecx]
0x46F535: sub      edx, dword ptr [ecx + 0x2ae78]
0x46F53B: inc      edx
0x46F53C: sub      edx, eax
0x46F53E: jl       0x46f66e
0x46F544: inc      edx
0x46F545: mov      dword ptr [ebp - 0x24], edx
0x46F548: mov      dword ptr [ebp - 0xc], eax
0x46F54B: mov      dword ptr [ebp - 0x14], 0xffffffee
0x46F552: mov      eax, dword ptr [ebp - 0x20]
0x46F555: mov      eax, dword ptr [eax]
0x46F557: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x46F55D: mov      edx, dword ptr [edx]
0x46F55F: sub      eax, dword ptr [edx + 0x2ae74]
0x46F565: sub      eax, 2
0x46F568: mov      edx, dword ptr [ebp - 0x20]
0x46F56B: mov      edx, dword ptr [edx + 8]
0x46F56E: mov      ecx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x46F574: mov      ecx, dword ptr [ecx]
0x46F576: sub      edx, dword ptr [ecx + 0x2ae74]
0x46F57C: inc      edx
0x46F57D: sub      edx, eax
0x46F57F: jl       0x46f65e
0x46F585: inc      edx
0x46F586: mov      dword ptr [ebp - 0x28], edx
0x46F589: mov      dword ptr [ebp - 8], eax
0x46F58C: cmp      dword ptr [ebp - 8], 0
0x46F590: jl       0x46f64e
0x46F596: cmp      dword ptr [ebp - 8], 0x78
0x46F59A: jge      0x46f64e
0x46F5A0: cmp      dword ptr [ebp - 0xc], 0
0x46F5A4: jl       0x46f64e
0x46F5AA: cmp      dword ptr [ebp - 0xc], 0x78
0x46F5AE: jge      0x46f64e
0x46F5B4: imul     eax, dword ptr [ebp - 8], 0x16b
0x46F5BB: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x46F5C1: mov      edx, dword ptr [edx]
0x46F5C3: lea      eax, [edx + eax*4]
0x46F5C6: mov      edx, dword ptr [ebp - 0xc]
0x46F5C9: lea      edx, [edx + edx*2]
0x46F5CC: mov      ax, word ptr [eax + edx*4 + 8]
0x46F5D1: and      ax, 0x7fff
0x46F5D5: movzx    eax, ax
0x46F5D8: mov      dword ptr [ebp - 0x18], eax
0x46F5DB: cmp      dword ptr [ebp - 0x18], 0
0x46F5DF: jle      0x46f64e
0x46F5E1: mov      eax, dword ptr [ebp - 8]
0x46F5E4: and      eax, 0x80000001
0x46F5E9: jns      0x46f5f0
0x46F5EB: dec      eax
0x46F5EC: or       eax, 0xfffffffe
0x46F5EF: inc      eax
0x46F5F0: test     eax, eax
0x46F5F2: jne      0x46f64e
0x46F5F4: mov      eax, dword ptr [ebp - 0xc]
0x46F5F7: and      eax, 0x80000001
0x46F5FC: jns      0x46f603
0x46F5FE: dec      eax
0x46F5FF: or       eax, 0xfffffffe
0x46F602: inc      eax
0x46F603: test     eax, eax
0x46F605: jne      0x46f64e
0x46F607: dec      dword ptr [ebp - 0x18]
0x46F60A: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x46F60F: mov      eax, dword ptr [eax]
0x46F611: mov      eax, dword ptr [eax + 0x2f8]
0x46F617: mov      edx, dword ptr [ebp - 0x18]
0x46F61A: call     0x45351c
0x46F61F: mov      dword ptr [ebp - 0x1c], eax
0x46F622: cmp      dword ptr [ebp - 0x1c], 0
0x46F626: je       0x46f64e
0x46F628: lea      edx, [ebp - 0x3c]
0x46F62B: mov      eax, dword ptr [ebp - 0x1c]
0x46F62E: call     0x44d8ec
0x46F633: lea      eax, [ebp - 0x3c]
0x46F636: push     eax
0x46F637: mov      eax, dword ptr [ebp - 0x1c]
0x46F63A: push     eax
0x46F63B: push     0
0x46F63D: mov      eax, dword ptr [ebp - 4]
0x46F640: mov      eax, dword ptr [eax + 8]
0x46F643: mov      ecx, dword ptr [ebp - 0x10]
0x46F646: mov      edx, dword ptr [ebp - 0x14]
0x46F649: call     0x44d6cc
0x46F64E: add      dword ptr [ebp - 0x14], 0x30
0x46F652: inc      dword ptr [ebp - 8]
0x46F655: dec      dword ptr [ebp - 0x28]
0x46F658: jne      0x46f58c
0x46F65E: add      dword ptr [ebp - 0x10], 0x20
0x46F662: inc      dword ptr [ebp - 0xc]
0x46F665: dec      dword ptr [ebp - 0x24]
0x46F668: jne      0x46f54b
0x46F66E: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x46F673: mov      eax, dword ptr [eax]
0x46F675: add      eax, 0x2ae54
0x46F67A: mov      dword ptr [ebp - 0x2c], eax
0x46F67D: mov      dword ptr [ebp - 0x10], 0xffffffe0
0x46F684: mov      eax, dword ptr [ebp - 0x2c]
0x46F687: mov      eax, dword ptr [eax + 4]
0x46F68A: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x46F690: mov      edx, dword ptr [edx]
0x46F692: sub      eax, dword ptr [edx + 0x2ae78]
0x46F698: dec      eax
0x46F699: mov      edx, dword ptr [ebp - 0x2c]
0x46F69C: mov      edx, dword ptr [edx + 0xc]
0x46F69F: mov      ecx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x46F6A5: mov      ecx, dword ptr [ecx]
0x46F6A7: sub      edx, dword ptr [ecx + 0x2ae78]
0x46F6AD: inc      edx
0x46F6AE: sub      edx, eax
0x46F6B0: jl       0x46f7a3
0x46F6B6: inc      edx
0x46F6B7: mov      dword ptr [ebp - 0x24], edx
0x46F6BA: mov      dword ptr [ebp - 0xc], eax
0x46F6BD: mov      dword ptr [ebp - 0x14], 0xffffffee
0x46F6C4: mov      eax, dword ptr [ebp - 0x2c]
0x46F6C7: mov      eax, dword ptr [eax]
0x46F6C9: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x46F6CF: mov      edx, dword ptr [edx]
0x46F6D1: sub      eax, dword ptr [edx + 0x2ae74]
0x46F6D7: sub      eax, 2
0x46F6DA: mov      edx, dword ptr [ebp - 0x2c]
0x46F6DD: mov      edx, dword ptr [edx + 8]
0x46F6E0: mov      ecx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x46F6E6: mov      ecx, dword ptr [ecx]
0x46F6E8: sub      edx, dword ptr [ecx + 0x2ae74]
0x46F6EE: inc      edx
0x46F6EF: sub      edx, eax
0x46F6F1: jl       0x46f793
0x46F6F7: inc      edx
0x46F6F8: mov      dword ptr [ebp - 0x28], edx
0x46F6FB: mov      dword ptr [ebp - 8], eax
0x46F6FE: cmp      dword ptr [ebp - 8], 0
0x46F702: jl       0x46f783
0x46F704: cmp      dword ptr [ebp - 8], 0x78
0x46F708: jge      0x46f783
0x46F70A: cmp      dword ptr [ebp - 0xc], 0
0x46F70E: jl       0x46f783
0x46F710: cmp      dword ptr [ebp - 0xc], 0x78
0x46F714: jge      0x46f783
0x46F716: imul     eax, dword ptr [ebp - 8], 0x16b
0x46F71D: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x46F723: mov      edx, dword ptr [edx]
0x46F725: lea      eax, [edx + eax*4]
0x46F728: mov      edx, dword ptr [ebp - 0xc]
0x46F72B: lea      edx, [edx + edx*2]
0x46F72E: movzx    eax, word ptr [eax + edx*4 + 0xa]
0x46F733: mov      dword ptr [ebp - 0x18], eax
0x46F736: cmp      dword ptr [ebp - 0x18], 0
0x46F73A: jle      0x46f783
0x46F73C: dec      dword ptr [ebp - 0x18]
0x46F73F: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x46F744: mov      eax, dword ptr [eax]
0x46F746: mov      eax, dword ptr [eax + 0x300]
0x46F74C: mov      edx, dword ptr [ebp - 0x18]
0x46F74F: call     0x45351c
0x46F754: mov      dword ptr [ebp - 0x1c], eax
0x46F757: cmp      dword ptr [ebp - 0x1c], 0
0x46F75B: je       0x46f783
0x46F75D: lea      edx, [ebp - 0x3c]
0x46F760: mov      eax, dword ptr [ebp - 0x1c]
0x46F763: call     0x44d8ec
0x46F768: lea      eax, [ebp - 0x3c]
0x46F76B: push     eax
0x46F76C: mov      eax, dword ptr [ebp - 0x1c]
0x46F76F: push     eax
0x46F770: push     1
0x46F772: mov      eax, dword ptr [ebp - 4]
0x46F775: mov      eax, dword ptr [eax + 8]
0x46F778: mov      ecx, dword ptr [ebp - 0x10]
0x46F77B: mov      edx, dword ptr [ebp - 0x14]
0x46F77E: call     0x44d6cc
0x46F783: add      dword ptr [ebp - 0x14], 0x30
0x46F787: inc      dword ptr [ebp - 8]
0x46F78A: dec      dword ptr [ebp - 0x28]
0x46F78D: jne      0x46f6fe
0x46F793: add      dword ptr [ebp - 0x10], 0x20
0x46F797: inc      dword ptr [ebp - 0xc]
0x46F79A: dec      dword ptr [ebp - 0x24]
0x46F79D: jne      0x46f6bd
0x46F7A3: pop      edi
0x46F7A4: pop      esi
0x46F7A5: mov      esp, ebp
0x46F7A7: pop      ebp
0x46F7A8: ret      
0x46F7A9: lea      eax, [eax]
0x46F7AC: push     ebp
0x46F7AD: mov      ebp, esp
0x46F7AF: add      esp, -0x1c
0x46F7B2: mov      dword ptr [ebp - 4], eax
0x46F7B5: xor      eax, eax
0x46F7B7: mov      dword ptr [ebp - 0x18], eax
0x46F7BA: mov      byte ptr [ebp - 0x19], 0
0x46F7BE: xor      eax, eax
0x46F7C0: mov      dword ptr [ebp - 8], eax
0x46F7C3: mov      eax, dword ptr [ebp - 8]
0x46F7C6: mov      eax, dword ptr [eax*4 + 0x4a2d1c] ; *0x4A2D1C->0x46ECE4
0x46F7CD: call     0x407f90
0x46F7D2: test     al, al
0x46F7D4: je       0x46f896
0x46F7DA: mov      eax, dword ptr [ebp - 8]
0x46F7DD: mov      eax, dword ptr [eax*4 + 0x4a2d1c] ; *0x4A2D1C->0x46ECE4
0x46F7E4: mov      edx, 0x40
0x46F7E9: call     0x407e58
0x46F7EE: mov      dword ptr [ebp - 0xc], eax
0x46F7F1: lea      edx, [ebp - 0x10]
0x46F7F4: mov      ecx, 4
0x46F7F9: mov      eax, dword ptr [ebp - 0xc]
0x46F7FC: call     0x407ebc
0x46F801: lea      edx, [ebp - 0x14]
0x46F804: mov      ecx, 4
0x46F809: mov      eax, dword ptr [ebp - 0xc]
0x46F80C: call     0x407ebc
0x46F811: mov      eax, dword ptr [ebp - 8]
0x46F814: lea      eax, [eax + eax*2]
0x46F817: mov      edx, dword ptr [ebp - 4]
0x46F81A: mov      ecx, dword ptr [ebp - 0x10]
0x46F81D: mov      dword ptr [edx + eax*4 + 0x5739c], ecx
0x46F824: mov      eax, dword ptr [ebp - 8]
0x46F827: lea      eax, [eax + eax*2]
0x46F82A: mov      edx, dword ptr [ebp - 4]
0x46F82D: mov      ecx, dword ptr [ebp - 0x14]
0x46F830: mov      dword ptr [edx + eax*4 + 0x573a0], ecx
0x46F837: mov      eax, dword ptr [ebp - 0x10]
0x46F83A: imul     dword ptr [ebp - 0x14]
0x46F83D: add      eax, 8
0x46F840: call     0x4079e8
0x46F845: mov      edx, dword ptr [ebp - 8]
0x46F848: lea      edx, [edx + edx*2]
0x46F84B: mov      ecx, dword ptr [ebp - 4]
0x46F84E: mov      dword ptr [ecx + edx*4 + 0x573a4], eax
0x46F855: mov      eax, dword ptr [ebp - 0x10]
0x46F858: imul     dword ptr [ebp - 0x14]
0x46F85B: cmp      eax, dword ptr [ebp - 0x18]
0x46F85E: jle      0x46f881
0x46F860: mov      ecx, dword ptr [ebp - 0x10]
0x46F863: imul     ecx, dword ptr [ebp - 0x14]
0x46F867: mov      eax, dword ptr [ebp - 8]
0x46F86A: lea      eax, [eax + eax*2]
0x46F86D: mov      edx, dword ptr [ebp - 4]
0x46F870: mov      edx, dword ptr [edx + eax*4 + 0x573a4]
0x46F877: mov      eax, dword ptr [ebp - 0xc]
0x46F87A: call     0x407ebc
0x46F87F: jmp      0x46f885
0x46F881: mov      byte ptr [ebp - 0x19], 1
0x46F885: mov      eax, dword ptr [ebp - 0x10]
0x46F888: imul     dword ptr [ebp - 0x14]
0x46F88B: mov      dword ptr [ebp - 0x18], eax
0x46F88E: mov      eax, dword ptr [ebp - 0xc]
0x46F891: call     0x407f20
0x46F896: inc      dword ptr [ebp - 8]
0x46F899: cmp      dword ptr [ebp - 8], 6
0x46F89D: jne      0x46f7c3
0x46F8A3: cmp      byte ptr [ebp - 0x19], 0
0x46F8A7: je       0x46f905
0x46F8A9: xor      eax, eax
0x46F8AB: mov      dword ptr [ebp - 8], eax
0x46F8AE: mov      eax, dword ptr [ebp - 8]
0x46F8B1: lea      eax, [eax + eax*2]
0x46F8B4: mov      edx, dword ptr [ebp - 4]
0x46F8B7: cmp      dword ptr [edx + eax*4 + 0x573a4], 0
0x46F8BF: je       0x46f8fc
0x46F8C1: mov      eax, dword ptr [ebp - 8]
0x46F8C4: lea      eax, [eax + eax*2]
0x46F8C7: mov      edx, dword ptr [ebp - 4]
0x46F8CA: mov      edx, dword ptr [edx + eax*4 + 0x5739c]
0x46F8D1: mov      eax, dword ptr [ebp - 8]
0x46F8D4: lea      eax, [eax + eax*2]
0x46F8D7: mov      ecx, dword ptr [ebp - 4]
0x46F8DA: imul     edx, dword ptr [ecx + eax*4 + 0x573a0]
0x46F8E2: add      edx, 8
0x46F8E5: mov      eax, dword ptr [ebp - 8]
0x46F8E8: lea      eax, [eax + eax*2]
0x46F8EB: mov      ecx, dword ptr [ebp - 4]
0x46F8EE: mov      eax, dword ptr [ecx + eax*4 + 0x573a4]
0x46F8F5: xor      ecx, ecx
0x46F8F7: call     0x402b54
0x46F8FC: inc      dword ptr [ebp - 8]
0x46F8FF: cmp      dword ptr [ebp - 8], 6
0x46F903: jne      0x46f8ae
0x46F905: mov      esp, ebp
0x46F907: pop      ebp
0x46F908: ret      
0x46F909: lea      eax, [eax]
0x46F90C: push     ebp
0x46F90D: mov      ebp, esp
0x46F90F: add      esp, -0xc
0x46F912: mov      dword ptr [ebp - 4], eax
0x46F915: mov      eax, dword ptr [ebp - 4]
0x46F918: add      eax, 0x57400
0x46F91D: xor      ecx, ecx
0x46F91F: mov      edx, 0x3450
0x46F924: call     0x402b54
0x46F929: xor      eax, eax
0x46F92B: mov      dword ptr [ebp - 8], eax
0x46F92E: xor      eax, eax
0x46F930: mov      dword ptr [ebp - 0xc], eax
0x46F933: imul     eax, dword ptr [ebp - 8], 0x36
0x46F937: mov      edx, dword ptr [ebp - 4]
0x46F93A: lea      eax, [edx + eax*8]
0x46F93D: mov      edx, dword ptr [ebp - 0xc]
0x46F940: add      edx, edx
0x46F942: mov      dword ptr [eax + edx*8 + 0x57408], 0xffffffff
0x46F94D: inc      dword ptr [ebp - 0xc]
0x46F950: cmp      dword ptr [ebp - 0xc], 0x1b
0x46F954: jne      0x46f933
0x46F956: inc      dword ptr [ebp - 8]
0x46F959: cmp      dword ptr [ebp - 8], 0x1f
0x46F95D: jne      0x46f92e
0x46F95F: mov      esp, ebp
0x46F961: pop      ebp
0x46F962: ret      
0x46F963: nop      
0x46F964: push     ebp
0x46F965: mov      ebp, esp
0x46F967: add      esp, -0x2c
0x46F96A: mov      dword ptr [ebp - 0xc], ecx
0x46F96D: mov      dword ptr [ebp - 8], edx
0x46F970: mov      dword ptr [ebp - 4], eax
0x46F973: mov      dword ptr [ebp - 0x18], 0xffffffff
0x46F97A: mov      eax, dword ptr [ebp + 8]
0x46F97D: cmp      eax, 5
0x46F980: ja       0x46f9ff
0x46F982: jmp      dword ptr [eax*4 + 0x46f989]
0x46F989: mov      eax, dword ptr [0xb10046f9]
0x46F98E: stc      
0x46F98F: inc      esi
0x46F990: add      cl, al
0x46F992: stc      
0x46F993: inc      esi
0x46F994: add      cl, dl
0x46F996: stc      
0x46F997: inc      esi
0x46F998: add      cl, ah
0x46F99A: stc      
0x46F99B: inc      esi
0x46F99C: add      cl, dh
0x46F99E: stc      
0x46F99F: inc      esi
0x46F9A0: add      bh, al
0x46F9A2: inc      ebp
0x46F9A3: call     0x46f9aa
0x46F9A8: mov      dword ptr [ebp - 0x24], 0x4a2d34 ; *0x4A2D34->0x1000100
0x46F9AF: jmp      0x46f9ff
0x46F9B1: mov      dword ptr [ebp - 0x18], 4
0x46F9B8: mov      dword ptr [ebp - 0x24], 0x4a2d40 ; *0x4A2D40->0x1010100
0x46F9BF: jmp      0x46f9ff
0x46F9C1: mov      dword ptr [ebp - 0x18], 8
0x46F9C8: mov      dword ptr [ebp - 0x24], 0x4a2d5c ; *0x4A2D5C->0x1000000
0x46F9CF: jmp      0x46f9ff
0x46F9D1: mov      dword ptr [ebp - 0x18], 0xa
0x46F9D8: mov      dword ptr [ebp - 0x24], 0x4a2db0 ; *0x4A2DB0->0x000000
0x46F9DF: jmp      0x46f9ff
0x46F9E1: mov      dword ptr [ebp - 0x18], 0xe
0x46F9E8: mov      dword ptr [ebp - 0x24], 0x4a2e2c ; *0x4A2E2C->0x000000
0x46F9EF: jmp      0x46f9ff
0x46F9F1: mov      dword ptr [ebp - 0x18], 0x10
0x46F9F8: mov      dword ptr [ebp - 0x24], 0x4a2f10 ; *0x4A2F10->0x000000
0x46F9FF: mov      eax, dword ptr [ebp - 0x18]
0x46FA02: test     eax, eax
0x46FA04: jl       0x46fa9c
0x46FA0A: inc      eax
0x46FA0B: mov      dword ptr [ebp - 0x28], eax
0x46FA0E: mov      dword ptr [ebp - 0x10], 0
0x46FA15: mov      eax, dword ptr [ebp - 0x18]
0x46FA18: test     eax, eax
0x46FA1A: jl       0x46fa90
0x46FA1C: inc      eax
0x46FA1D: mov      dword ptr [ebp - 0x2c], eax
0x46FA20: mov      dword ptr [ebp - 0x14], 0
0x46FA27: mov      eax, dword ptr [ebp - 0x18]
0x46FA2A: sar      eax, 1
0x46FA2C: jns      0x46fa31
0x46FA2E: adc      eax, 0
0x46FA31: mov      edx, dword ptr [ebp - 8]
0x46FA34: sub      edx, eax
0x46FA36: add      edx, dword ptr [ebp - 0x10]
0x46FA39: mov      dword ptr [ebp - 0x1c], edx
0x46FA3C: mov      eax, dword ptr [ebp - 0x18]
0x46FA3F: sar      eax, 1
0x46FA41: jns      0x46fa46
0x46FA43: adc      eax, 0
0x46FA46: mov      edx, dword ptr [ebp - 0xc]
0x46FA49: sub      edx, eax
0x46FA4B: add      edx, dword ptr [ebp - 0x14]
0x46FA4E: mov      dword ptr [ebp - 0x20], edx
0x46FA51: mov      eax, dword ptr [ebp - 0x1c]
0x46FA54: sub      eax, 0x1f
0x46FA57: jae      0x46fa88
0x46FA59: mov      eax, dword ptr [ebp - 0x20]
0x46FA5C: sub      eax, 0x1b
0x46FA5F: jae      0x46fa88
0x46FA61: imul     eax, dword ptr [ebp - 0x1c], 0x36
0x46FA65: mov      edx, dword ptr [ebp - 4]
0x46FA68: lea      eax, [edx + eax*8]
0x46FA6B: mov      edx, dword ptr [ebp - 0x20]
0x46FA6E: add      edx, edx
0x46FA70: mov      ecx, dword ptr [ebp - 0x18]
0x46FA73: inc      ecx
0x46FA74: imul     ecx, dword ptr [ebp - 0x10]
0x46FA78: add      ecx, dword ptr [ebp - 0x14]
0x46FA7B: add      ecx, dword ptr [ebp - 0x24]
0x46FA7E: movsx    ecx, byte ptr [ecx]
0x46FA81: add      dword ptr [eax + edx*8 + 0x5740c], ecx
0x46FA88: inc      dword ptr [ebp - 0x14]
0x46FA8B: dec      dword ptr [ebp - 0x2c]
0x46FA8E: jne      0x46fa27
0x46FA90: inc      dword ptr [ebp - 0x10]
0x46FA93: dec      dword ptr [ebp - 0x28]
0x46FA96: jne      0x46fa15
0x46FA9C: mov      esp, ebp
0x46FA9E: pop      ebp
0x46FA9F: ret      4
0x46FAA2: mov      eax, eax
0x46FAA4: push     ebp
0x46FAA5: mov      ebp, esp
0x46FAA7: add      esp, -0x3c
0x46FAAA: mov      dword ptr [ebp - 0xc], ecx
0x46FAAD: mov      dword ptr [ebp - 8], edx
0x46FAB0: mov      dword ptr [ebp - 4], eax
0x46FAB3: mov      dword ptr [ebp - 0x1c], 0xffffffff
0x46FABA: mov      eax, dword ptr [ebp + 8]
0x46FABD: cmp      eax, 5
0x46FAC0: ja       0x46fb6b
0x46FAC6: jmp      dword ptr [eax*4 + 0x46facd]
0x46FACD: in       eax, 0xfa
0x46FACF: inc      esi
0x46FAD0: add      dl, bh
0x46FAD2: cli      
0x46FAD3: inc      esi
0x46FAD4: add      byte ptr [ecx], dl
0x46FAD6: sti      
0x46FAD7: inc      esi
0x46FAD8: add      byte ptr [eax], ch
0x46FADA: sti      
0x46FADB: inc      esi
0x46FADC: add      byte ptr [edi], bh
0x46FADE: sti      
0x46FADF: inc      esi
0x46FAE0: add      byte ptr [esi - 5], dl
0x46FAE3: inc      esi
0x46FAE4: add      bh, al
0x46FAE6: inc      ebp
0x46FAE7: in       al, 2
0x46FAE9: add      byte ptr [eax], al
0x46FAEB: add      bh, al
0x46FAED: inc      ebp
0x46FAEE: int3     
0x46FAEF: xor      al, 0x2d
0x46FAF1: dec      edx
0x46FAF2: add      byte ptr [ebx], dh
0x46FAF4: ror      byte ptr [ecx + 0x71ebd045], 0xc7
0x46FAFB: inc      ebp
0x46FAFC: in       al, 4
0x46FAFE: add      byte ptr [eax], al
0x46FB00: add      bh, al
0x46FB02: inc      ebp
0x46FB03: int3     
0x46FB04: inc      eax
0x46FB05: sub      eax, 0x45c7004a
0x46FB0A: rol      byte ptr [eax + eax], 1
0x46FB0D: add      byte ptr [eax], al
0x46FB0F: jmp      0x46fb6b
0x46FB11: mov      dword ptr [ebp - 0x1c], 8
0x46FB18: mov      dword ptr [ebp - 0x34], 0x4a2d5c ; *0x4A2D5C->0x1000000
0x46FB1F: mov      dword ptr [ebp - 0x30], 8
0x46FB26: jmp      0x46fb6b
0x46FB28: mov      dword ptr [ebp - 0x1c], 0xa
0x46FB2F: mov      dword ptr [ebp - 0x34], 0x4a2db0 ; *0x4A2DB0->0x000000
0x46FB36: mov      dword ptr [ebp - 0x30], 0x12
0x46FB3D: jmp      0x46fb6b
0x46FB3F: mov      dword ptr [ebp - 0x1c], 0xe
0x46FB46: mov      dword ptr [ebp - 0x34], 0x4a2e2c ; *0x4A2E2C->0x000000
0x46FB4D: mov      dword ptr [ebp - 0x30], 0x1e
0x46FB54: jmp      0x46fb6b
0x46FB56: mov      dword ptr [ebp - 0x1c], 0x10
0x46FB5D: mov      dword ptr [ebp - 0x34], 0x4a2f10 ; *0x4A2F10->0x000000
0x46FB64: mov      dword ptr [ebp - 0x30], 0x28
0x46FB6B: xor      eax, eax
0x46FB6D: mov      dword ptr [ebp - 0x2c], eax
0x46FB70: mov      eax, dword ptr [ebp - 0x1c]
0x46FB73: test     eax, eax
0x46FB75: jl       0x46fc46
0x46FB7B: inc      eax
0x46FB7C: mov      dword ptr [ebp - 0x38], eax
0x46FB7F: mov      dword ptr [ebp - 0x14], 0
0x46FB86: mov      eax, dword ptr [ebp - 0x1c]
0x46FB89: test     eax, eax
0x46FB8B: jl       0x46fc3a
0x46FB91: inc      eax
0x46FB92: mov      dword ptr [ebp - 0x3c], eax
0x46FB95: mov      dword ptr [ebp - 0x18], 0
0x46FB9C: mov      eax, dword ptr [ebp - 0x1c]
0x46FB9F: sar      eax, 1
0x46FBA1: jns      0x46fba6
0x46FBA3: adc      eax, 0
0x46FBA6: mov      edx, dword ptr [ebp - 8]
0x46FBA9: sub      edx, eax
0x46FBAB: add      edx, dword ptr [ebp - 0x14]
0x46FBAE: mov      dword ptr [ebp - 0x24], edx
0x46FBB1: mov      eax, dword ptr [ebp - 0x1c]
0x46FBB4: sar      eax, 1
0x46FBB6: jns      0x46fbbb
0x46FBB8: adc      eax, 0
0x46FBBB: mov      edx, dword ptr [ebp - 0xc]
0x46FBBE: sub      edx, eax
0x46FBC0: add      edx, dword ptr [ebp - 0x18]
0x46FBC3: mov      dword ptr [ebp - 0x28], edx
0x46FBC6: mov      eax, dword ptr [ebp - 0x24]
0x46FBC9: sub      eax, 0x1f
0x46FBCC: jae      0x46fc2e
0x46FBCE: mov      eax, dword ptr [ebp - 0x28]
0x46FBD1: sub      eax, 0x1b
0x46FBD4: jae      0x46fc2e
0x46FBD6: mov      eax, dword ptr [ebp - 0x1c]
0x46FBD9: inc      eax
0x46FBDA: imul     dword ptr [ebp - 0x14]
0x46FBDD: add      eax, dword ptr [ebp - 0x18]
0x46FBE0: add      eax, dword ptr [ebp - 0x34]
0x46FBE3: movsx    eax, byte ptr [eax]
0x46FBE6: mov      dword ptr [ebp - 0x20], eax
0x46FBE9: imul     eax, dword ptr [ebp - 0x24], 0x36
0x46FBED: mov      edx, dword ptr [ebp - 4]
0x46FBF0: lea      eax, [edx + eax*8]
0x46FBF3: mov      edx, dword ptr [ebp - 0x28]
0x46FBF6: add      edx, edx
0x46FBF8: mov      eax, dword ptr [eax + edx*8 + 0x5740c]
0x46FBFF: cmp      eax, dword ptr [ebp - 0x20]
0x46FC02: jge      0x46fc2e
0x46FC04: imul     eax, dword ptr [ebp - 0x24], 0x36
0x46FC08: mov      edx, dword ptr [ebp - 4]
0x46FC0B: lea      eax, [edx + eax*8]
0x46FC0E: mov      edx, dword ptr [ebp - 0x28]
0x46FC11: add      edx, edx
0x46FC13: mov      ecx, dword ptr [ebp - 0x20]
0x46FC16: sub      ecx, dword ptr [eax + edx*8 + 0x5740c]
0x46FC1D: add      dword ptr [ebp - 0x2c], ecx
0x46FC20: mov      eax, dword ptr [ebp - 0x2c]
0x46FC23: cmp      eax, dword ptr [ebp - 0x30]
0x46FC26: jl       0x46fc2e
0x46FC28: mov      byte ptr [ebp - 0xd], 0
0x46FC2C: jmp      0x46fc4a
0x46FC2E: inc      dword ptr [ebp - 0x18]
0x46FC31: dec      dword ptr [ebp - 0x3c]
0x46FC34: jne      0x46fb9c
0x46FC3A: inc      dword ptr [ebp - 0x14]
0x46FC3D: dec      dword ptr [ebp - 0x38]
0x46FC40: jne      0x46fb86
0x46FC46: mov      byte ptr [ebp - 0xd], 1
0x46FC4A: mov      al, byte ptr [ebp - 0xd]
0x46FC4D: mov      esp, ebp
0x46FC4F: pop      ebp
0x46FC50: ret      4
0x46FC53: nop      
0x46FC54: push     ebp
0x46FC55: mov      ebp, esp
0x46FC57: add      esp, -0x14
0x46FC5A: mov      dword ptr [ebp - 0xc], ecx
0x46FC5D: mov      dword ptr [ebp - 8], edx
0x46FC60: mov      dword ptr [ebp - 4], eax
0x46FC63: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x46FC68: mov      eax, dword ptr [eax]
0x46FC6A: mov      edx, dword ptr [ebp - 8]
0x46FC6D: sub      edx, dword ptr [eax + 0xb8]
0x46FC73: add      edx, 0xf
0x46FC76: mov      dword ptr [ebp - 0x10], edx
0x46FC79: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x46FC7E: mov      eax, dword ptr [eax]
0x46FC80: mov      edx, dword ptr [ebp - 0xc]
0x46FC83: sub      edx, dword ptr [eax + 0xbc]
0x46FC89: add      edx, 0xd
0x46FC8C: mov      dword ptr [ebp - 0x14], edx
0x46FC8F: cmp      dword ptr [ebp - 0x10], 1
0x46FC93: jl       0x46fd48
0x46FC99: cmp      dword ptr [ebp - 0x10], 0x1e
0x46FC9D: jge      0x46fd48
0x46FCA3: cmp      dword ptr [ebp - 0x14], 1
0x46FCA7: jl       0x46fd48
0x46FCAD: cmp      dword ptr [ebp - 0x14], 0x1a
0x46FCB1: jge      0x46fd48
0x46FCB7: imul     eax, dword ptr [ebp - 0x10], 0x36
0x46FCBB: mov      edx, dword ptr [ebp - 4]
0x46FCBE: lea      eax, [edx + eax*8]
0x46FCC1: mov      edx, dword ptr [ebp - 0x14]
0x46FCC4: add      edx, edx
0x46FCC6: mov      eax, dword ptr [eax + edx*8 + 0x57408]
0x46FCCD: cmp      eax, dword ptr [ebp + 0xc]
0x46FCD0: jge      0x46fd48
0x46FCD2: mov      eax, dword ptr [ebp + 0xc]
0x46FCD5: push     eax
0x46FCD6: mov      ecx, dword ptr [ebp - 0x14]
0x46FCD9: mov      edx, dword ptr [ebp - 0x10]
0x46FCDC: mov      eax, dword ptr [ebp - 4]
0x46FCDF: call     0x46faa4
0x46FCE4: xor      al, 1
0x46FCE6: or       al, byte ptr [ebp + 8]
0x46FCE9: je       0x46fd48
0x46FCEB: mov      eax, dword ptr [ebp + 0xc]
0x46FCEE: push     eax
0x46FCEF: mov      ecx, dword ptr [ebp - 0x14]
0x46FCF2: mov      edx, dword ptr [ebp - 0x10]
0x46FCF5: mov      eax, dword ptr [ebp - 4]
0x46FCF8: call     0x46f964
0x46FCFD: imul     eax, dword ptr [ebp - 0x10], 0x36
0x46FD01: mov      edx, dword ptr [ebp - 4]
0x46FD04: lea      eax, [edx + eax*8]
0x46FD07: mov      edx, dword ptr [ebp - 0x14]
0x46FD0A: add      edx, edx
0x46FD0C: mov      ecx, dword ptr [ebp + 0xc]
0x46FD0F: mov      dword ptr [eax + edx*8 + 0x57408], ecx
0x46FD16: imul     eax, dword ptr [ebp - 0x10], 0x36
0x46FD1A: mov      edx, dword ptr [ebp - 4]
0x46FD1D: lea      eax, [edx + eax*8]
0x46FD20: mov      edx, dword ptr [ebp - 0x14]
0x46FD23: add      edx, edx
0x46FD25: mov      ecx, dword ptr [ebp + 0x14]
0x46FD28: mov      dword ptr [eax + edx*8 + 0x57400], ecx
0x46FD2F: imul     eax, dword ptr [ebp - 0x10], 0x36
0x46FD33: mov      edx, dword ptr [ebp - 4]
0x46FD36: lea      eax, [edx + eax*8]
0x46FD39: mov      edx, dword ptr [ebp - 0x14]
0x46FD3C: add      edx, edx
0x46FD3E: mov      ecx, dword ptr [ebp + 0x10]
0x46FD41: mov      dword ptr [eax + edx*8 + 0x57404], ecx
0x46FD48: mov      esp, ebp
0x46FD4A: pop      ebp
0x46FD4B: ret      0x10
0x46FD4E: mov      eax, eax
0x46FD50: push     ebp
0x46FD51: mov      ebp, esp
0x46FD53: add      esp, -0x2c
0x46FD56: push     ebx
0x46FD57: mov      dword ptr [ebp - 4], eax
0x46FD5A: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x46FD5F: mov      eax, dword ptr [eax]
0x46FD61: mov      edx, 0xffffffbe
0x46FD66: sub      edx, dword ptr [eax + 0x98]
0x46FD6C: mov      dword ptr [ebp - 0x14], edx
0x46FD6F: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x46FD74: mov      eax, dword ptr [eax]
0x46FD76: mov      edx, 0xffffffa0
0x46FD7B: sub      edx, dword ptr [eax + 0x9c]
0x46FD81: mov      dword ptr [ebp - 0x18], edx
0x46FD84: xor      eax, eax
0x46FD86: mov      dword ptr [ebp - 0x2c], eax
0x46FD89: mov      dword ptr [ebp - 8], 1
0x46FD90: mov      dword ptr [ebp - 0xc], 1
0x46FD97: mov      eax, dword ptr [ebp - 0xc]
0x46FD9A: add      eax, eax
0x46FD9C: imul     edx, dword ptr [ebp - 8], 0x36
0x46FDA0: mov      ecx, dword ptr [ebp - 4]
0x46FDA3: lea      edx, [ecx + edx*8]
0x46FDA6: mov      eax, dword ptr [edx + eax*8 + 0x57408]
0x46FDAD: mov      dword ptr [ebp - 0x10], eax
0x46FDB0: cmp      dword ptr [ebp - 0x10], 0
0x46FDB4: jl       0x46feec
0x46FDBA: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x46FDBF: mov      eax, dword ptr [eax]
0x46FDC1: mov      eax, dword ptr [eax + 0xb8]
0x46FDC7: add      eax, dword ptr [ebp - 8]
0x46FDCA: sub      eax, 0xf
0x46FDCD: mov      dword ptr [ebp - 0x1c], eax
0x46FDD0: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x46FDD5: mov      eax, dword ptr [eax]
0x46FDD7: mov      eax, dword ptr [eax + 0xbc]
0x46FDDD: add      eax, dword ptr [ebp - 0xc]
0x46FDE0: sub      eax, 0xd
0x46FDE3: mov      dword ptr [ebp - 0x20], eax
0x46FDE6: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x46FDEB: mov      eax, dword ptr [eax]
0x46FDED: mov      edx, dword ptr [ebp - 0x1c]
0x46FDF0: sub      edx, dword ptr [eax + 0x2ae54]
0x46FDF6: mov      eax, edx
0x46FDF8: shl      eax, 4
0x46FDFB: lea      eax, [eax + eax*2]
0x46FDFE: add      eax, dword ptr [ebp - 0x14]
0x46FE01: mov      edx, dword ptr [ebp - 0xc]
0x46FE04: add      edx, edx
0x46FE06: imul     ecx, dword ptr [ebp - 8], 0x36
0x46FE0A: mov      ebx, dword ptr [ebp - 4]
0x46FE0D: lea      ecx, [ebx + ecx*8]
0x46FE10: add      eax, dword ptr [ecx + edx*8 + 0x57400]
0x46FE17: mov      dword ptr [ebp - 0x24], eax
0x46FE1A: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x46FE1F: mov      eax, dword ptr [eax]
0x46FE21: mov      edx, dword ptr [ebp - 0x20]
0x46FE24: sub      edx, dword ptr [eax + 0x2ae58]
0x46FE2A: mov      eax, edx
0x46FE2C: shl      eax, 5
0x46FE2F: add      eax, dword ptr [ebp - 0x18]
0x46FE32: mov      edx, dword ptr [ebp - 0xc]
0x46FE35: add      edx, edx
0x46FE37: imul     ecx, dword ptr [ebp - 8], 0x36
0x46FE3B: mov      ebx, dword ptr [ebp - 4]
0x46FE3E: lea      ecx, [ebx + ecx*8]
0x46FE41: add      eax, dword ptr [ecx + edx*8 + 0x57404]
0x46FE48: mov      dword ptr [ebp - 0x28], eax
0x46FE4B: mov      eax, dword ptr [ebp - 0x10]
0x46FE4E: lea      eax, [eax + eax*2]
0x46FE51: mov      edx, dword ptr [ebp - 4]
0x46FE54: mov      eax, dword ptr [edx + eax*4 + 0x5739c]
0x46FE5B: push     eax
0x46FE5C: mov      eax, dword ptr [ebp - 0x10]
0x46FE5F: lea      eax, [eax + eax*2]
0x46FE62: mov      edx, dword ptr [ebp - 4]
0x46FE65: mov      eax, dword ptr [edx + eax*4 + 0x573a0]
0x46FE6C: push     eax
0x46FE6D: mov      eax, dword ptr [ebp - 4]
0x46FE70: mov      eax, dword ptr [eax + 0x57390]
0x46FE76: push     eax
0x46FE77: mov      eax, dword ptr [ebp - 0x10]
0x46FE7A: lea      eax, [eax + eax*2]
0x46FE7D: mov      edx, dword ptr [ebp - 4]
0x46FE80: mov      eax, dword ptr [edx + eax*4 + 0x5739c]
0x46FE87: sub      eax, 0x30
0x46FE8A: sar      eax, 1
0x46FE8C: jns      0x46fe91
0x46FE8E: adc      eax, 0
0x46FE91: mov      edx, dword ptr [ebp - 0x24]
0x46FE94: sub      edx, eax
0x46FE96: push     edx
0x46FE97: mov      eax, dword ptr [ebp - 0x10]
0x46FE9A: lea      eax, [eax + eax*2]
0x46FE9D: mov      edx, dword ptr [ebp - 4]
0x46FEA0: mov      eax, dword ptr [edx + eax*4 + 0x573a0]
0x46FEA7: sub      eax, 0x20
0x46FEAA: sar      eax, 1
0x46FEAC: jns      0x46feb1
0x46FEAE: adc      eax, 0
0x46FEB1: mov      edx, dword ptr [ebp - 0x28]
0x46FEB4: sub      edx, eax
0x46FEB6: sub      edx, 5
0x46FEB9: push     edx
0x46FEBA: mov      eax, dword ptr [ebp - 4]
0x46FEBD: mov      eax, dword ptr [eax + 0x57394]
0x46FEC3: push     eax
0x46FEC4: mov      eax, dword ptr [ebp - 4]
0x46FEC7: mov      eax, dword ptr [eax + 0x57398]
0x46FECD: push     eax
0x46FECE: push     0x14
0x46FED0: mov      eax, dword ptr [ebp - 0x10]
0x46FED3: lea      eax, [eax + eax*2]
0x46FED6: mov      edx, dword ptr [ebp - 4]
0x46FED9: mov      eax, dword ptr [edx + eax*4 + 0x573a4]
0x46FEE0: xor      ecx, ecx
0x46FEE2: xor      edx, edx
0x46FEE4: call     0x4551fc
0x46FEE9: inc      dword ptr [ebp - 0x2c]
0x46FEEC: inc      dword ptr [ebp - 0xc]
0x46FEEF: cmp      dword ptr [ebp - 0xc], 0x1a
0x46FEF3: jne      0x46fd97
0x46FEF9: inc      dword ptr [ebp - 8]
0x46FEFC: cmp      dword ptr [ebp - 8], 0x1e
0x46FF00: jne      0x46fd90
0x46FF06: pop      ebx
0x46FF07: mov      esp, ebp
0x46FF09: pop      ebp
0x46FF0A: ret      
0x46FF0B: nop      
0x46FF0C: push     ebp
0x46FF0D: mov      ebp, esp
0x46FF0F: add      esp, -0x3c
0x46FF12: mov      byte ptr [ebp - 9], cl
0x46FF15: mov      dword ptr [ebp - 8], edx
0x46FF18: mov      dword ptr [ebp - 4], eax
0x46FF1B: call     0x406154
0x46FF20: mov      edx, dword ptr [ebp - 4]
0x46FF23: mov      edx, dword ptr [edx + 0x5a868]
0x46FF29: add      edx, 0x12c
0x46FF2F: cmp      eax, edx
0x46FF31: jbe      0x46ff55
0x46FF33: call     0x406154
0x46FF38: mov      edx, dword ptr [ebp - 4]
0x46FF3B: mov      dword ptr [edx + 0x5a868], eax
0x46FF41: mov      eax, dword ptr [ebp - 4]
0x46FF44: mov      al, byte ptr [eax + 0x5a86c]
0x46FF4A: xor      al, 1
0x46FF4C: mov      edx, dword ptr [ebp - 4]
0x46FF4F: mov      byte ptr [edx + 0x5a86c], al
0x46FF55: mov      edx, dword ptr [0x4a3f54] ; *0x4A3F54->0x4F9488
0x46FF5B: mov      edx, dword ptr [edx]
0x46FF5D: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x46FF62: mov      eax, dword ptr [eax]
0x46FF64: mov      eax, dword ptr [eax + 0x35c]
0x46FF6A: call     0x45351c
0x46FF6F: mov      dword ptr [ebp - 0x10], eax
0x46FF72: cmp      dword ptr [ebp - 0x10], 0
0x46FF76: je       0x470096
0x46FF7C: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x46FF81: mov      eax, dword ptr [eax]
0x46FF83: movzx    eax, word ptr [eax + 8]
0x46FF87: shl      eax, 4
0x46FF8A: lea      eax, [eax + eax*2]
0x46FF8D: shr      eax, 5
0x46FF90: mov      dword ptr [ebp - 0x14], eax
0x46FF93: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x46FF98: mov      eax, dword ptr [eax]
0x46FF9A: movzx    eax, word ptr [eax + 0xa]
0x46FF9E: shl      eax, 5
0x46FFA1: shr      eax, 5
0x46FFA4: mov      dword ptr [ebp - 0x18], eax
0x46FFA7: mov      edx, dword ptr [ebp - 0x14]
0x46FFAA: sub      edx, 0x3c
0x46FFAD: xor      eax, eax
0x46FFAF: call     0x452bbc
0x46FFB4: mov      dword ptr [ebp - 0x2c], eax
0x46FFB7: mov      edx, dword ptr [ebp - 0x18]
0x46FFBA: sub      edx, 0x3c
0x46FFBD: xor      eax, eax
0x46FFBF: call     0x452bbc
0x46FFC4: mov      dword ptr [ebp - 0x28], eax
0x46FFC7: lea      edx, [ebp - 0x3c]
0x46FFCA: mov      eax, dword ptr [ebp - 0x10]
0x46FFCD: call     0x44d8ec
0x46FFD2: mov      eax, dword ptr [ebp - 0x34]
0x46FFD5: mov      edx, dword ptr [ebp - 0x2c]
0x46FFD8: add      edx, 0x78
0x46FFDB: call     0x452b90
0x46FFE0: mov      dword ptr [ebp - 0x24], eax
0x46FFE3: lea      edx, [ebp - 0x3c]
0x46FFE6: mov      eax, dword ptr [ebp - 0x10]
0x46FFE9: call     0x44d8ec
0x46FFEE: mov      eax, dword ptr [ebp - 0x30]
0x46FFF1: mov      edx, dword ptr [ebp - 0x28]
0x46FFF4: add      edx, 0x78
0x46FFF7: call     0x452b90
0x46FFFC: mov      dword ptr [ebp - 0x20], eax
0x46FFFF: cmp      byte ptr [ebp - 9], 0
0x470003: je       0x470028
0x470005: mov      eax, dword ptr [ebp - 0x10]
0x470008: push     eax
0x470009: mov      eax, dword ptr [ebp - 0x2c]
0x47000C: push     eax
0x47000D: mov      eax, dword ptr [ebp - 0x28]
0x470010: push     eax
0x470011: push     0x78
0x470013: push     0x78
0x470015: push     0
0x470017: xor      ecx, ecx
0x470019: mov      edx, 0x2a8
0x47001E: mov      eax, dword ptr [ebp - 8]
0x470021: call     0x4555bc
0x470026: jmp      0x470041
0x470028: lea      eax, [ebp - 0x2c]
0x47002B: push     eax
0x47002C: mov      eax, dword ptr [ebp - 0x10]
0x47002F: push     eax
0x470030: push     0
0x470032: xor      ecx, ecx
0x470034: mov      edx, 0x2a8
0x470039: mov      eax, dword ptr [ebp - 8]
0x47003C: call     0x44d6cc
0x470041: mov      eax, dword ptr [ebp - 4]
0x470044: cmp      byte ptr [eax + 0x5a86c], 0
0x47004B: je       0x470096
0x47004D: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x470052: mov      eax, dword ptr [eax]
0x470054: movzx    eax, word ptr [eax + 8]
0x470058: shl      eax, 4
0x47005B: lea      eax, [eax + eax*2]
0x47005E: shr      eax, 5
0x470061: add      eax, 0x2a8
0x470066: sub      eax, dword ptr [ebp - 0x2c]
0x470069: mov      dword ptr [ebp - 0x14], eax
0x47006C: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x470071: mov      eax, dword ptr [eax]
0x470073: movzx    eax, word ptr [eax + 0xa]
0x470077: shl      eax, 5
0x47007A: shr      eax, 5
0x47007D: sub      eax, dword ptr [ebp - 0x28]
0x470080: mov      dword ptr [ebp - 0x18], eax
0x470083: push     0xff
0x470088: mov      ecx, dword ptr [ebp - 0x18]
0x47008B: mov      edx, dword ptr [ebp - 0x14]
0x47008E: mov      eax, dword ptr [ebp - 8]
0x470091: call     0x44e238
0x470096: mov      esp, ebp
0x470098: pop      ebp
0x470099: ret      
0x47009A: mov      eax, eax
0x47009C: push     ebp
0x47009D: mov      ebp, esp
0x47009F: add      esp, 0xffffff6c
0x4700A5: push     ebx
0x4700A6: push     esi
0x4700A7: push     edi
0x4700A8: xor      ecx, ecx
0x4700AA: mov      dword ptr [ebp - 0x74], ecx
0x4700AD: mov      dword ptr [ebp - 8], edx
0x4700B0: mov      dword ptr [ebp - 4], eax
0x4700B3: xor      eax, eax
0x4700B5: push     ebp
0x4700B6: push     0x471cfd
0x4700BB: push     dword ptr fs:[eax]
0x4700BE: mov      dword ptr fs:[eax], esp
0x4700C1: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4700C6: cmp      dword ptr [eax], 0
0x4700C9: jne      0x470131
0x4700CB: lea      eax, [ebp - 0x74]
0x4700CE: mov      edx, 0x471d14 ; "请稍候..."
0x4700D3: call     0x403af0
0x4700D8: mov      eax, dword ptr [ebp - 8]
0x4700DB: call     0x44d8b4
0x4700E0: mov      dword ptr [ebp - 0x78], eax
0x4700E3: push     1
0x4700E5: mov      eax, dword ptr [ebp - 0x78]
0x4700E8: call     0x41834c
0x4700ED: push     eax
0x4700EE: call     0x406434
0x4700F3: push     0xffffff
0x4700F8: push     0
0x4700FA: mov      eax, dword ptr [ebp - 0x74]
0x4700FD: push     eax
0x4700FE: mov      edx, dword ptr [ebp - 0x74]
0x470101: mov      eax, dword ptr [ebp - 0x78]
0x470104: call     0x4181ac
0x470109: mov      edx, 0x320
0x47010E: sub      edx, eax
0x470110: sar      edx, 1
0x470112: jns      0x470117
0x470114: adc      edx, 0
0x470117: mov      ecx, 0xc8
0x47011C: mov      eax, dword ptr [ebp - 8]
0x47011F: call     0x4a09a0
0x470124: mov      eax, dword ptr [ebp - 0x78]
0x470127: call     0x44d104
0x47012C: jmp      0x471ce7
0x470131: mov      eax, dword ptr [0x4a3f98] ; *0x4A3F98->0x4F9470
0x470136: mov      byte ptr [eax], 0
0x470139: mov      byte ptr [ebp - 0x5e], 0
0x47013D: call     0x406154
0x470142: mov      edx, dword ptr [ebp - 4]
0x470145: sub      eax, dword ptr [edx + 0x573e4]
0x47014B: cmp      eax, 0x64
0x47014E: jb       0x470182
0x470150: call     0x406154
0x470155: mov      edx, dword ptr [ebp - 4]
0x470158: mov      dword ptr [edx + 0x573e4], eax
0x47015E: mov      byte ptr [ebp - 0x5e], 1
0x470162: mov      eax, dword ptr [ebp - 4]
0x470165: inc      dword ptr [eax + 0x573e8]
0x47016B: mov      eax, dword ptr [ebp - 4]
0x47016E: cmp      dword ptr [eax + 0x573e8], 1
0x470175: jle      0x470182
0x470177: mov      eax, dword ptr [ebp - 4]
0x47017A: xor      edx, edx
0x47017C: mov      dword ptr [eax + 0x573e8], edx
0x470182: call     0x406154
0x470187: mov      edx, dword ptr [ebp - 4]
0x47018A: sub      eax, dword ptr [edx + 0x573ec]
0x470190: cmp      eax, 0x32
0x470193: jb       0x4701c0
0x470195: call     0x406154
0x47019A: mov      edx, dword ptr [ebp - 4]
0x47019D: mov      dword ptr [edx + 0x573ec], eax
0x4701A3: mov      eax, dword ptr [0x4a4304] ; *0x4A4304->0x4F9244
0x4701A8: inc      dword ptr [eax]
0x4701AA: mov      eax, dword ptr [0x4a4304] ; *0x4A4304->0x4F9244
0x4701AF: cmp      dword ptr [eax], 0xf4240
0x4701B5: jle      0x4701c0
0x4701B7: mov      eax, dword ptr [0x4a4304] ; *0x4A4304->0x4F9244
0x4701BC: xor      edx, edx
0x4701BE: mov      dword ptr [eax], edx
0x4701C0: xor      eax, eax
0x4701C2: push     ebp
0x4701C3: push     0x47036b
0x4701C8: push     dword ptr fs:[eax]
0x4701CB: mov      dword ptr fs:[eax], esp
0x4701CE: xor      eax, eax
0x4701D0: mov      dword ptr [ebp - 0xc], eax
0x4701D3: mov      eax, dword ptr [ebp - 4]
0x4701D6: mov      eax, dword ptr [eax + 0x5a854]
0x4701DC: mov      eax, dword ptr [eax + 8]
0x4701DF: cmp      eax, dword ptr [ebp - 0xc]
0x4701E2: jle      0x470361
0x4701E8: mov      eax, dword ptr [ebp - 4]
0x4701EB: mov      eax, dword ptr [eax + 0x5a854]
0x4701F1: mov      edx, dword ptr [ebp - 0xc]
0x4701F4: call     0x40f6a0
0x4701F9: mov      dword ptr [ebp - 0x6c], eax
0x4701FC: cmp      byte ptr [ebp - 0x5e], 0
0x470200: je       0x47020c
0x470202: mov      eax, dword ptr [ebp - 0x6c]
0x470205: mov      byte ptr [eax + 0x164], 0
0x47020C: mov      eax, dword ptr [ebp - 0x6c]
0x47020F: cmp      byte ptr [eax + 0x164], 0
0x470216: jne      0x470260
0x470218: mov      eax, dword ptr [ebp - 0x6c]
0x47021B: call     0x4970fc
0x470220: cmp      byte ptr [ebp - 0x5e], 0
0x470224: je       0x470240
0x470226: mov      eax, dword ptr [ebp - 4]
0x470229: mov      edx, dword ptr [eax + 0x573e8]
0x47022F: mov      eax, dword ptr [ebp - 0x6c]
0x470232: call     0x4999b0
0x470237: test     al, al
0x470239: je       0x470240
0x47023B: inc      dword ptr [ebp - 0xc]
0x47023E: jmp      0x4701d3
0x470240: mov      eax, dword ptr [ebp - 0x6c]
0x470243: mov      bx, 0xfffa
0x470247: call     0x403080
0x47024C: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x470251: mov      eax, dword ptr [eax]
0x470253: cmp      eax, dword ptr [ebp - 0x6c]
0x470256: je       0x470260
0x470258: mov      eax, dword ptr [ebp - 0x6c]
0x47025B: call     0x497384
0x470260: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x470265: mov      eax, dword ptr [eax]
0x470267: cmp      eax, dword ptr [ebp - 0x6c]
0x47026A: jne      0x470274
0x47026C: mov      eax, dword ptr [ebp - 0x6c]
0x47026F: call     0x497384
0x470274: mov      eax, dword ptr [ebp - 0x6c]
0x470277: cmp      dword ptr [eax + 0xf8], 0
0x47027E: je       0x4702e8
0x470280: mov      eax, dword ptr [ebp - 0x6c]
0x470283: call     0x4974ec
0x470288: test     al, al
0x47028A: je       0x4702e8
0x47028C: mov      eax, dword ptr [ebp - 0x6c]
0x47028F: mov      eax, dword ptr [eax + 0xf8]
0x470295: call     0x4a0ba8
0x47029A: mov      eax, dword ptr [ebp - 0x6c]
0x47029D: mov      ax, word ptr [eax + 0xa]
0x4702A1: push     eax
0x4702A2: mov      eax, dword ptr [ebp - 0x6c]
0x4702A5: movzx    eax, byte ptr [eax + 0xc]
0x4702A9: push     eax
0x4702AA: mov      eax, dword ptr [ebp - 0x6c]
0x4702AD: mov      eax, dword ptr [eax + 0xfc]
0x4702B3: push     eax
0x4702B4: mov      eax, dword ptr [ebp - 0x6c]
0x4702B7: mov      eax, dword ptr [eax + 0x100]
0x4702BD: push     eax
0x4702BE: mov      eax, dword ptr [ebp - 0x6c]
0x4702C1: mov      cx, word ptr [eax + 8]
0x4702C5: mov      eax, dword ptr [ebp - 0x6c]
0x4702C8: mov      edx, dword ptr [eax + 0xf8]
0x4702CE: mov      eax, dword ptr [ebp - 4]
0x4702D1: call     0x472f18
0x4702D6: mov      eax, dword ptr [ebp - 0x6c]
0x4702D9: xor      edx, edx
0x4702DB: mov      dword ptr [eax + 0xf8], edx
0x4702E1: mov      eax, dword ptr [ebp - 0x6c]
0x4702E4: mov      byte ptr [eax + 0x22], 1
0x4702E8: mov      eax, dword ptr [ebp - 0x6c]
0x4702EB: cmp      byte ptr [eax + 0x22], 0
0x4702EF: je       0x470359
0x4702F1: mov      eax, dword ptr [0x4a4078] ; *0x4A4078->0x4F9230
0x4702F6: mov      eax, dword ptr [eax]
0x4702F8: mov      edx, dword ptr [ebp - 0x6c]
0x4702FB: call     0x40f588
0x470300: mov      eax, dword ptr [ebp - 4]
0x470303: mov      eax, dword ptr [eax + 0x5a854]
0x470309: mov      edx, dword ptr [ebp - 0xc]
0x47030C: call     0x40f5c4
0x470311: mov      eax, dword ptr [0x4a42c0] ; *0x4A42C0->0x4F94A4
0x470316: mov      eax, dword ptr [eax]
0x470318: cmp      eax, dword ptr [ebp - 0x6c]
0x47031B: jne      0x470326
0x47031D: mov      eax, dword ptr [0x4a42c0] ; *0x4A42C0->0x4F94A4
0x470322: xor      edx, edx
0x470324: mov      dword ptr [eax], edx
0x470326: mov      eax, dword ptr [0x4a3f10] ; *0x4A3F10->0x4F94A8
0x47032B: mov      eax, dword ptr [eax]
0x47032D: cmp      eax, dword ptr [ebp - 0x6c]
0x470330: jne      0x47033b
0x470332: mov      eax, dword ptr [0x4a3f10] ; *0x4A3F10->0x4F94A8
0x470337: xor      edx, edx
0x470339: mov      dword ptr [eax], edx
0x47033B: mov      eax, dword ptr [0x4a414c] ; *0x4A414C->0x4F94AC
0x470340: mov      eax, dword ptr [eax]
0x470342: cmp      eax, dword ptr [ebp - 0x6c]
0x470345: jne      0x4701d3
0x47034B: mov      eax, dword ptr [0x4a414c] ; *0x4A414C->0x4F94AC
0x470350: xor      edx, edx
0x470352: mov      dword ptr [eax], edx
0x470354: jmp      0x4701d3
0x470359: inc      dword ptr [ebp - 0xc]
0x47035C: jmp      0x4701d3
0x470361: xor      eax, eax
0x470363: pop      edx
0x470364: pop      ecx
0x470365: pop      ecx
0x470366: mov      dword ptr fs:[eax], edx
0x470369: jmp      0x47037f
0x47036B: jmp      0x403310
0x470370: mov      eax, 0x471d28 ; "101"
0x470375: call     0x45ee2c
0x47037A: call     0x4035c0
0x47037F: xor      eax, eax
0x470381: push     ebp
0x470382: push     0x4704cc
0x470387: push     dword ptr fs:[eax]
0x47038A: mov      dword ptr fs:[eax], esp
0x47038D: xor      eax, eax
0x47038F: mov      dword ptr [ebp - 0xc], eax
0x470392: mov      eax, dword ptr [ebp - 4]
0x470395: mov      eax, dword ptr [eax + 0x5a85c]
0x47039B: mov      eax, dword ptr [eax + 8]
0x47039E: cmp      eax, dword ptr [ebp - 0xc]
0x4703A1: jle      0x4703f0
0x4703A3: mov      eax, dword ptr [ebp - 4]
0x4703A6: mov      eax, dword ptr [eax + 0x5a85c]
0x4703AC: mov      edx, dword ptr [ebp - 0xc]
0x4703AF: call     0x40f6a0
0x4703B4: mov      dword ptr [ebp - 0x70], eax
0x4703B7: mov      eax, dword ptr [ebp - 0x70]
0x4703BA: cmp      byte ptr [eax + 4], 0
0x4703BE: je       0x4703eb
0x4703C0: mov      eax, dword ptr [ebp - 0x70]
0x4703C3: mov      bx, 0xffff
0x4703C7: call     0x403080
0x4703CC: test     al, al
0x4703CE: jne      0x4703eb
0x4703D0: mov      eax, dword ptr [ebp - 0x70]
0x4703D3: call     0x402eac
0x4703D8: mov      eax, dword ptr [ebp - 4]
0x4703DB: mov      eax, dword ptr [eax + 0x5a85c]
0x4703E1: mov      edx, dword ptr [ebp - 0xc]
0x4703E4: call     0x40f5c4
0x4703E9: jmp      0x470392
0x4703EB: inc      dword ptr [ebp - 0xc]
0x4703EE: jmp      0x470392
0x4703F0: xor      eax, eax
0x4703F2: mov      dword ptr [ebp - 0xc], eax
0x4703F5: mov      eax, dword ptr [ebp - 4]
0x4703F8: mov      eax, dword ptr [eax + 0x5a860]
0x4703FE: mov      eax, dword ptr [eax + 8]
0x470401: cmp      eax, dword ptr [ebp - 0xc]
0x470404: jle      0x470453
0x470406: mov      eax, dword ptr [ebp - 4]
0x470409: mov      eax, dword ptr [eax + 0x5a860]
0x47040F: mov      edx, dword ptr [ebp - 0xc]
0x470412: call     0x40f6a0
0x470417: mov      dword ptr [ebp - 0x70], eax
0x47041A: mov      eax, dword ptr [ebp - 0x70]
0x47041D: cmp      byte ptr [eax + 4], 0
0x470421: je       0x47044e
0x470423: mov      eax, dword ptr [ebp - 0x70]
0x470426: mov      bx, 0xffff
0x47042A: call     0x403080
0x47042F: test     al, al
0x470431: jne      0x47044e
0x470433: mov      eax, dword ptr [ebp - 0x70]
0x470436: call     0x402eac
0x47043B: mov      eax, dword ptr [ebp - 4]
0x47043E: mov      eax, dword ptr [eax + 0x5a860]
0x470444: mov      edx, dword ptr [ebp - 0xc]
0x470447: call     0x40f5c4
0x47044C: jmp      0x4703f5
0x47044E: inc      dword ptr [ebp - 0xc]
0x470451: jmp      0x4703f5
0x470453: xor      eax, eax
0x470455: mov      dword ptr [ebp - 0xc], eax
0x470458: mov      eax, dword ptr [ebp - 4]
0x47045B: mov      eax, dword ptr [eax + 0x5a864]
0x470461: mov      eax, dword ptr [eax + 8]
0x470464: cmp      eax, dword ptr [ebp - 0xc]
0x470467: jle      0x4704b6
0x470469: mov      eax, dword ptr [ebp - 4]
0x47046C: mov      eax, dword ptr [eax + 0x5a864]
0x470472: mov      edx, dword ptr [ebp - 0xc]
0x470475: call     0x40f6a0
0x47047A: mov      dword ptr [ebp - 0x70], eax
0x47047D: mov      eax, dword ptr [ebp - 0x70]
0x470480: cmp      byte ptr [eax + 4], 0
0x470484: je       0x4704b1
0x470486: mov      eax, dword ptr [ebp - 0x70]
0x470489: mov      bx, 0xffff
0x47048D: call     0x403080
0x470492: test     al, al
0x470494: jne      0x4704b1
0x470496: mov      eax, dword ptr [ebp - 0x70]
0x470499: call     0x402eac
0x47049E: mov      eax, dword ptr [ebp - 4]
0x4704A1: mov      eax, dword ptr [eax + 0x5a864]
0x4704A7: mov      edx, dword ptr [ebp - 0xc]
0x4704AA: call     0x40f5c4
0x4704AF: jmp      0x470458
0x4704B1: inc      dword ptr [ebp - 0xc]
0x4704B4: jmp      0x470458
0x4704B6: mov      eax, dword ptr [0x4a40a8] ; *0x4A40A8->0x4F7DCC
0x4704BB: mov      eax, dword ptr [eax]
0x4704BD: call     0x477974
0x4704C2: xor      eax, eax
0x4704C4: pop      edx
0x4704C5: pop      ecx
0x4704C6: pop      ecx
0x4704C7: mov      dword ptr fs:[eax], edx
0x4704CA: jmp      0x4704e0
0x4704CC: jmp      0x403310
0x4704D1: mov      eax, 0x471d34 ; "102"
0x4704D6: call     0x45ee2c
0x4704DB: call     0x4035c0
0x4704E0: xor      eax, eax
0x4704E2: push     ebp
0x4704E3: push     0x470638
0x4704E8: push     dword ptr fs:[eax]
0x4704EB: mov      dword ptr fs:[eax], esp
0x4704EE: mov      eax, dword ptr [0x4a4204] ; *0x4A4204->0x4F7DA4
0x4704F3: mov      eax, dword ptr [eax]
0x4704F5: mov      eax, dword ptr [eax + 8]
0x4704F8: dec      eax
0x4704F9: test     eax, eax
0x4704FB: jl       0x470598
0x470501: inc      eax
0x470502: mov      dword ptr [ebp - 0x7c], eax
0x470505: mov      dword ptr [ebp - 0x14], 0
0x47050C: mov      eax, dword ptr [0x4a4204] ; *0x4A4204->0x4F7DA4
0x470511: mov      eax, dword ptr [eax]
0x470513: mov      edx, dword ptr [ebp - 0x14]
0x470516: call     0x40f6a0
0x47051B: mov      dword ptr [ebp - 0x64], eax
0x47051E: cmp      dword ptr [ebp - 0x64], 0
0x470522: je       0x47058c
0x470524: mov      eax, dword ptr [ebp - 0x64]
0x470527: movzx    eax, word ptr [eax + 4]
0x47052B: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x470531: mov      edx, dword ptr [edx]
0x470533: movzx    edx, word ptr [edx + 8]
0x470537: sub      eax, edx
0x470539: cdq      
0x47053A: xor      eax, edx
0x47053C: sub      eax, edx
0x47053E: cmp      eax, 0x1e
0x470541: jle      0x47058c
0x470543: mov      eax, dword ptr [ebp - 0x64]
0x470546: movzx    eax, word ptr [eax + 6]
0x47054A: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x470550: mov      edx, dword ptr [edx]
0x470552: movzx    edx, word ptr [edx + 0xa]
0x470556: sub      eax, edx
0x470558: cdq      
0x470559: xor      eax, edx
0x47055B: sub      eax, edx
0x47055D: cmp      eax, 0x1e
0x470560: jle      0x47058c
0x470562: mov      eax, dword ptr [0x4a4204] ; *0x4A4204->0x4F7DA4
0x470567: mov      eax, dword ptr [eax]
0x470569: mov      edx, dword ptr [ebp - 0x14]
0x47056C: call     0x40f6a0
0x470571: mov      edx, 0x2c
0x470576: call     0x4026cc
0x47057B: mov      eax, dword ptr [0x4a4204] ; *0x4A4204->0x4F7DA4
0x470580: mov      eax, dword ptr [eax]
0x470582: mov      edx, dword ptr [ebp - 0x14]
0x470585: call     0x40f5c4
0x47058A: jmp      0x470598
0x47058C: inc      dword ptr [ebp - 0x14]
0x47058F: dec      dword ptr [ebp - 0x7c]
0x470592: jne      0x47050c
0x470598: mov      eax, dword ptr [0x4a40a8] ; *0x4A40A8->0x4F7DCC
0x47059D: mov      eax, dword ptr [eax]
0x47059F: mov      eax, dword ptr [eax + 4]
0x4705A2: mov      eax, dword ptr [eax + 8]
0x4705A5: dec      eax
0x4705A6: test     eax, eax
0x4705A8: jl       0x47062e
0x4705AE: inc      eax
0x4705AF: mov      dword ptr [ebp - 0x7c], eax
0x4705B2: mov      dword ptr [ebp - 0x14], 0
0x4705B9: mov      eax, dword ptr [0x4a40a8] ; *0x4A40A8->0x4F7DCC
0x4705BE: mov      eax, dword ptr [eax]
0x4705C0: mov      eax, dword ptr [eax + 4]
0x4705C3: mov      edx, dword ptr [ebp - 0x14]
0x4705C6: call     0x40f6a0
0x4705CB: mov      dword ptr [ebp - 0x68], eax
0x4705CE: mov      eax, dword ptr [ebp - 0x68]
0x4705D1: mov      eax, dword ptr [eax + 4]
0x4705D4: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4705DA: mov      edx, dword ptr [edx]
0x4705DC: movzx    edx, word ptr [edx + 8]
0x4705E0: sub      eax, edx
0x4705E2: cdq      
0x4705E3: xor      eax, edx
0x4705E5: sub      eax, edx
0x4705E7: cmp      eax, 0x1e
0x4705EA: jle      0x470626
0x4705EC: mov      eax, dword ptr [ebp - 0x68]
0x4705EF: mov      eax, dword ptr [eax + 8]
0x4705F2: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4705F8: mov      edx, dword ptr [edx]
0x4705FA: movzx    edx, word ptr [edx + 0xa]
0x4705FE: sub      eax, edx
0x470600: cdq      
0x470601: xor      eax, edx
0x470603: sub      eax, edx
0x470605: cmp      eax, 0x1e
0x470608: jle      0x470626
0x47060A: mov      eax, dword ptr [ebp - 0x68]
0x47060D: call     0x402eac
0x470612: mov      eax, dword ptr [0x4a40a8] ; *0x4A40A8->0x4F7DCC
0x470617: mov      eax, dword ptr [eax]
0x470619: mov      eax, dword ptr [eax + 4]
0x47061C: mov      edx, dword ptr [ebp - 0x14]
0x47061F: call     0x40f5c4
0x470624: jmp      0x47062e
0x470626: inc      dword ptr [ebp - 0x14]
0x470629: dec      dword ptr [ebp - 0x7c]
0x47062C: jne      0x4705b9
0x47062E: xor      eax, eax
0x470630: pop      edx
0x470631: pop      ecx
0x470632: pop      ecx
0x470633: mov      dword ptr fs:[eax], edx
0x470636: jmp      0x47064c
0x470638: jmp      0x403310
0x47063D: mov      eax, 0x471d40 ; "103"
0x470642: call     0x45ee2c
0x470647: call     0x4035c0
0x47064C: xor      eax, eax
0x47064E: push     ebp
0x47064F: push     0x4707cc
0x470654: push     dword ptr fs:[eax]
0x470657: mov      dword ptr fs:[eax], esp
0x47065A: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x47065F: mov      eax, dword ptr [eax]
0x470661: add      eax, 0x2ae54
0x470666: mov      dword ptr [ebp - 0x80], eax
0x470669: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x47066E: mov      eax, dword ptr [eax]
0x470670: mov      eax, dword ptr [eax + 0xb8]
0x470676: sub      eax, 9
0x470679: mov      edx, dword ptr [ebp - 0x80]
0x47067C: mov      dword ptr [edx], eax
0x47067E: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x470683: mov      eax, dword ptr [eax]
0x470685: mov      eax, dword ptr [eax + 0xbc]
0x47068B: sub      eax, 9
0x47068E: mov      edx, dword ptr [ebp - 0x80]
0x470691: mov      dword ptr [edx + 4], eax
0x470694: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x470699: mov      eax, dword ptr [eax]
0x47069B: mov      eax, dword ptr [eax + 0xb8]
0x4706A1: add      eax, 9
0x4706A4: mov      edx, dword ptr [ebp - 0x80]
0x4706A7: mov      dword ptr [edx + 8], eax
0x4706AA: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4706AF: mov      eax, dword ptr [eax]
0x4706B1: mov      eax, dword ptr [eax + 0xbc]
0x4706B7: add      eax, 8
0x4706BA: mov      edx, dword ptr [ebp - 0x80]
0x4706BD: mov      dword ptr [edx + 0xc], eax
0x4706C0: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4706C5: mov      eax, dword ptr [eax]
0x4706C7: mov      ecx, dword ptr [eax + 0xbc]
0x4706CD: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4706D2: mov      eax, dword ptr [eax]
0x4706D4: mov      edx, dword ptr [eax + 0xb8]
0x4706DA: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x4706DF: mov      eax, dword ptr [eax]
0x4706E1: call     0x49ee80
0x4706E6: mov      eax, dword ptr [0x4a427c] ; *0x4A427C->0x4F94C1
0x4706EB: cmp      byte ptr [eax], 0
0x4706EE: jne      0x4706fd
0x4706F0: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4706F5: mov      eax, dword ptr [eax]
0x4706F7: cmp      byte ptr [eax + 0x20], 0
0x4706FB: je       0x470705
0x4706FD: mov      eax, dword ptr [0x4a3f1c] ; *0x4A3F1C->0x4F9382
0x470702: mov      byte ptr [eax], 0
0x470705: mov      eax, dword ptr [0x4a3f1c] ; *0x4A3F1C->0x4F9382
0x47070A: cmp      byte ptr [eax], 0
0x47070D: je       0x47072a
0x47070F: mov      eax, dword ptr [ebp - 4]
0x470712: mov      eax, dword ptr [eax + 0x57390]
0x470718: mov      edx, 0x56ea0
0x47071D: call     0x406984
0x470722: mov      eax, dword ptr [ebp - 4]
0x470725: call     0x46f90c
0x47072A: mov      dword ptr [ebp - 0x54], 0x1c2
0x470731: mov      eax, dword ptr [ebp - 4]
0x470734: mov      eax, dword ptr [eax + 0xc]
0x470737: xor      edx, edx
0x470739: call     0x44d430
0x47073E: mov      eax, dword ptr [ebp - 4]
0x470741: call     0x46f484
0x470746: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x47074B: mov      eax, dword ptr [eax]
0x47074D: mov      eax, dword ptr [eax + 0x9c]
0x470753: add      eax, 0x40
0x470756: add      eax, 0x1bd
0x47075B: push     eax
0x47075C: lea      eax, [ebp - 0x94]
0x470762: push     eax
0x470763: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x470768: mov      eax, dword ptr [eax]
0x47076A: mov      ecx, dword ptr [eax + 0x98]
0x470770: add      ecx, 0x90
0x470776: add      ecx, 0x320
0x47077C: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x470781: mov      eax, dword ptr [eax]
0x470783: mov      edx, dword ptr [eax + 0x9c]
0x470789: add      edx, 0x40
0x47078C: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x470791: mov      eax, dword ptr [eax]
0x470793: mov      eax, dword ptr [eax + 0x98]
0x470799: add      eax, 0x90
0x47079E: call     0x40ecc0
0x4707A3: lea      eax, [ebp - 0x94]
0x4707A9: push     eax
0x4707AA: mov      eax, dword ptr [ebp - 4]
0x4707AD: mov      eax, dword ptr [eax + 8]
0x4707B0: push     eax
0x4707B1: push     0
0x4707B3: mov      eax, dword ptr [ebp - 4]
0x4707B6: mov      eax, dword ptr [eax + 0xc]
0x4707B9: xor      ecx, ecx
0x4707BB: xor      edx, edx
0x4707BD: call     0x44d6cc
0x4707C2: xor      eax, eax
0x4707C4: pop      edx
0x4707C5: pop      ecx
0x4707C6: pop      ecx
0x4707C7: mov      dword ptr fs:[eax], edx
0x4707CA: jmp      0x4707e0
0x4707CC: jmp      0x403310
0x4707D1: mov      eax, 0x471d4c ; "104"
0x4707D6: call     0x45ee2c
0x4707DB: call     0x4035c0
0x4707E0: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4707E5: mov      eax, dword ptr [eax]
0x4707E7: mov      edx, 0xffffffa0
0x4707EC: sub      edx, dword ptr [eax + 0x98]
0x4707F2: add      edx, 0x10
0x4707F5: add      edx, 0xe
0x4707F8: mov      dword ptr [ebp - 0x30], edx
0x4707FB: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x470800: mov      eax, dword ptr [eax]
0x470802: mov      edx, 0xffffffc0
0x470807: sub      edx, dword ptr [eax + 0x9c]
0x47080D: mov      dword ptr [ebp - 0x34], edx
0x470810: mov      eax, dword ptr [ebp - 4]
0x470813: mov      edx, dword ptr [ebp - 0x30]
0x470816: mov      dword ptr [eax + 0x573f0], edx
0x47081C: mov      eax, dword ptr [ebp - 4]
0x47081F: mov      edx, dword ptr [ebp - 0x34]
0x470822: mov      dword ptr [eax + 0x573f4], edx
0x470828: xor      eax, eax
0x47082A: push     ebp
0x47082B: push     0x470bd1
0x470830: push     dword ptr fs:[eax]
0x470833: mov      dword ptr fs:[eax], esp
0x470836: mov      eax, dword ptr [ebp - 0x34]
0x470839: sub      eax, 0x20
0x47083C: mov      dword ptr [ebp - 0x1c], eax
0x47083F: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x470844: mov      eax, dword ptr [eax]
0x470846: mov      eax, dword ptr [eax + 0x2ae58]
0x47084C: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x470852: mov      edx, dword ptr [edx]
0x470854: sub      eax, dword ptr [edx + 0x2ae78]
0x47085A: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x470860: mov      edx, dword ptr [edx]
0x470862: mov      edx, dword ptr [edx + 0x2ae60]
0x470868: mov      ecx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x47086E: mov      ecx, dword ptr [ecx]
0x470870: sub      edx, dword ptr [ecx + 0x2ae78]
0x470876: add      edx, 0x23
0x470879: sub      edx, eax
0x47087B: jl       0x470b52
0x470881: inc      edx
0x470882: mov      dword ptr [ebp - 0x7c], edx
0x470885: mov      dword ptr [ebp - 0x10], eax
0x470888: cmp      dword ptr [ebp - 0x10], 0
0x47088C: jge      0x470897
0x47088E: add      dword ptr [ebp - 0x1c], 0x20
0x470892: jmp      0x470b46
0x470897: mov      eax, dword ptr [ebp - 0x30]
0x47089A: sub      eax, 0x60
0x47089D: mov      dword ptr [ebp - 0x18], eax
0x4708A0: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x4708A5: mov      eax, dword ptr [eax]
0x4708A7: mov      eax, dword ptr [eax + 0x2ae54]
0x4708AD: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x4708B3: mov      edx, dword ptr [edx]
0x4708B5: sub      eax, dword ptr [edx + 0x2ae74]
0x4708BB: sub      eax, 2
0x4708BE: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x4708C4: mov      edx, dword ptr [edx]
0x4708C6: mov      edx, dword ptr [edx + 0x2ae5c]
0x4708CC: mov      ecx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x4708D2: mov      ecx, dword ptr [ecx]
0x4708D4: sub      edx, dword ptr [ecx + 0x2ae74]
0x4708DA: add      edx, 2
0x4708DD: sub      edx, eax
0x4708DF: jl       0x470b42
0x4708E5: inc      edx
0x4708E6: mov      dword ptr [ebp - 0x84], edx
0x4708EC: mov      dword ptr [ebp - 0xc], eax
0x4708EF: cmp      dword ptr [ebp - 0xc], 0
0x4708F3: jl       0x470b2f
0x4708F9: cmp      dword ptr [ebp - 0xc], 0x78
0x4708FD: jge      0x470b2f
0x470903: cmp      dword ptr [ebp - 0x10], 0
0x470907: jl       0x470b2f
0x47090D: cmp      dword ptr [ebp - 0x10], 0x78
0x470911: jge      0x470b2f
0x470917: imul     eax, dword ptr [ebp - 0xc], 0x16b
0x47091E: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x470924: mov      edx, dword ptr [edx]
0x470926: lea      eax, [edx + eax*4]
0x470929: mov      edx, dword ptr [ebp - 0x10]
0x47092C: lea      edx, [edx + edx*2]
0x47092F: mov      ax, word ptr [eax + edx*4 + 0xc]
0x470934: and      ax, 0x7fff
0x470938: movzx    eax, ax
0x47093B: mov      dword ptr [ebp - 0x3c], eax
0x47093E: cmp      dword ptr [ebp - 0x3c], 0
0x470942: jle      0x470b2f
0x470948: imul     eax, dword ptr [ebp - 0xc], 0x16b
0x47094F: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x470955: mov      edx, dword ptr [edx]
0x470957: lea      eax, [edx + eax*4]
0x47095A: mov      edx, dword ptr [ebp - 0x10]
0x47095D: lea      edx, [edx + edx*2]
0x470960: movzx    eax, byte ptr [eax + edx*4 + 0x10]
0x470965: mov      dword ptr [ebp - 0x40], eax
0x470968: imul     eax, dword ptr [ebp - 0xc], 0x16b
0x47096F: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x470975: mov      edx, dword ptr [edx]
0x470977: lea      eax, [edx + eax*4]
0x47097A: mov      edx, dword ptr [ebp - 0x10]
0x47097D: lea      edx, [edx + edx*2]
0x470980: movzx    eax, byte ptr [eax + edx*4 + 0x12]
0x470985: mov      dword ptr [ebp - 0x38], eax
0x470988: test     dword ptr [ebp - 0x40], 0x80
0x47098F: jle      0x470999
0x470991: mov      byte ptr [ebp - 0x5d], 1
0x470995: and      dword ptr [ebp - 0x40], 0x7f
0x470999: cmp      dword ptr [ebp - 0x40], 0
0x47099D: jle      0x4709e3
0x47099F: imul     eax, dword ptr [ebp - 0xc], 0x16b
0x4709A6: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x4709AC: mov      edx, dword ptr [edx]
0x4709AE: lea      eax, [edx + eax*4]
0x4709B1: mov      edx, dword ptr [ebp - 0x10]
0x4709B4: lea      edx, [edx + edx*2]
0x4709B7: movzx    eax, byte ptr [eax + edx*4 + 0x11]
0x4709BC: mov      dword ptr [ebp - 0x44], eax
0x4709BF: mov      eax, dword ptr [0x4a4304] ; *0x4A4304->0x4F9244
0x4709C4: mov      eax, dword ptr [eax]
0x4709C6: mov      edx, dword ptr [ebp - 0x40]
0x4709C9: imul     edx, dword ptr [ebp - 0x44]
0x4709CD: add      edx, dword ptr [ebp - 0x40]
0x4709D0: mov      ecx, edx
0x4709D2: cdq      
0x4709D3: idiv     ecx
0x4709D5: mov      eax, edx
0x4709D7: mov      edx, dword ptr [ebp - 0x44]
0x4709DA: inc      edx
0x4709DB: mov      ecx, edx
0x4709DD: cdq      
0x4709DE: idiv     ecx
0x4709E0: add      dword ptr [ebp - 0x3c], eax
0x4709E3: imul     eax, dword ptr [ebp - 0xc], 0x16b
0x4709EA: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x4709F0: mov      edx, dword ptr [edx]
0x4709F2: lea      eax, [edx + eax*4]
0x4709F5: mov      edx, dword ptr [ebp - 0x10]
0x4709F8: lea      edx, [edx + edx*2]
0x4709FB: test     byte ptr [eax + edx*4 + 0xf], 0x80
0x470A00: jbe      0x470a47
0x470A02: imul     eax, dword ptr [ebp - 0xc], 0x16b
0x470A09: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x470A0F: mov      edx, dword ptr [edx]
0x470A11: lea      eax, [edx + eax*4]
0x470A14: mov      edx, dword ptr [ebp - 0x10]
0x470A17: lea      edx, [edx + edx*2]
0x470A1A: test     byte ptr [eax + edx*4 + 0xe], 0x7f
0x470A1F: jbe      0x470a47
0x470A21: imul     eax, dword ptr [ebp - 0xc], 0x16b
0x470A28: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x470A2E: mov      edx, dword ptr [edx]
0x470A30: lea      eax, [edx + eax*4]
0x470A33: mov      edx, dword ptr [ebp - 0x10]
0x470A36: lea      edx, [edx + edx*2]
0x470A39: mov      al, byte ptr [eax + edx*4 + 0xf]
0x470A3D: and      al, 0x7f
0x470A3F: and      eax, 0xff
0x470A44: add      dword ptr [ebp - 0x3c], eax
0x470A47: dec      dword ptr [ebp - 0x3c]
0x470A4A: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x470A4F: mov      eax, dword ptr [eax]
0x470A51: mov      ecx, dword ptr [ebp - 0x3c]
0x470A54: mov      edx, dword ptr [ebp - 0x38]
0x470A57: call     0x460a00
0x470A5C: mov      dword ptr [ebp - 0x58], eax
0x470A5F: cmp      dword ptr [ebp - 0x58], 0
0x470A63: je       0x470b2f
0x470A69: mov      eax, dword ptr [ebp - 0x58]
0x470A6C: call     0x44dc60
0x470A71: cmp      eax, 0x30
0x470A74: jne      0x470b2f
0x470A7A: mov      eax, dword ptr [ebp - 0x58]
0x470A7D: call     0x44d944
0x470A82: cmp      eax, 0x20
0x470A85: jne      0x470b2f
0x470A8B: mov      eax, dword ptr [ebp - 0x58]
0x470A8E: call     0x44d944
0x470A93: mov      edx, dword ptr [ebp - 0x1c]
0x470A96: add      edx, 0x20
0x470A99: sub      edx, eax
0x470A9B: mov      dword ptr [ebp - 0x20], edx
0x470A9E: mov      eax, dword ptr [ebp - 0x58]
0x470AA1: call     0x44dc60
0x470AA6: add      eax, dword ptr [ebp - 0x18]
0x470AA9: test     eax, eax
0x470AAB: jle      0x470afb
0x470AAD: cmp      dword ptr [ebp - 0x18], 0x320
0x470AB4: jg       0x470afb
0x470AB6: mov      eax, dword ptr [ebp - 0x58]
0x470AB9: call     0x44d944
0x470ABE: add      eax, dword ptr [ebp - 0x20]
0x470AC1: test     eax, eax
0x470AC3: jle      0x470afb
0x470AC5: mov      eax, dword ptr [ebp - 0x20]
0x470AC8: cmp      eax, dword ptr [ebp - 0x54]
0x470ACB: jge      0x470afb
0x470ACD: lea      edx, [ebp - 0x94]
0x470AD3: mov      eax, dword ptr [ebp - 0x58]
0x470AD6: call     0x44d8ec
0x470ADB: lea      eax, [ebp - 0x94]
0x470AE1: push     eax
0x470AE2: mov      eax, dword ptr [ebp - 0x58]
0x470AE5: push     eax
0x470AE6: push     1
0x470AE8: mov      eax, dword ptr [ebp - 4]
0x470AEB: mov      eax, dword ptr [eax + 0xc]
0x470AEE: mov      ecx, dword ptr [ebp - 0x20]
0x470AF1: mov      edx, dword ptr [ebp - 0x18]
0x470AF4: call     0x44d6cc
0x470AF9: jmp      0x470b2f
0x470AFB: mov      eax, dword ptr [ebp - 0x20]
0x470AFE: cmp      eax, dword ptr [ebp - 0x54]
0x470B01: jge      0x470b2f
0x470B03: lea      edx, [ebp - 0x94]
0x470B09: mov      eax, dword ptr [ebp - 0x58]
0x470B0C: call     0x44d8ec
0x470B11: lea      eax, [ebp - 0x94]
0x470B17: push     eax
0x470B18: mov      eax, dword ptr [ebp - 0x58]
0x470B1B: push     eax
0x470B1C: push     1
0x470B1E: mov      eax, dword ptr [ebp - 4]
0x470B21: mov      eax, dword ptr [eax + 0xc]
0x470B24: mov      ecx, dword ptr [ebp - 0x20]
0x470B27: mov      edx, dword ptr [ebp - 0x18]
0x470B2A: call     0x44d6cc
0x470B2F: add      dword ptr [ebp - 0x18], 0x30
0x470B33: inc      dword ptr [ebp - 0xc]
0x470B36: dec      dword ptr [ebp - 0x84]
0x470B3C: jne      0x4708ef
0x470B42: add      dword ptr [ebp - 0x1c], 0x20
0x470B46: inc      dword ptr [ebp - 0x10]
0x470B49: dec      dword ptr [ebp - 0x7c]
0x470B4C: jne      0x470888
0x470B52: mov      eax, dword ptr [ebp - 4]
0x470B55: mov      eax, dword ptr [eax + 0x5a85c]
0x470B5B: mov      eax, dword ptr [eax + 8]
0x470B5E: dec      eax
0x470B5F: test     eax, eax
0x470B61: jl       0x470bc7
0x470B63: inc      eax
0x470B64: mov      dword ptr [ebp - 0x7c], eax
0x470B67: mov      dword ptr [ebp - 0x14], 0
0x470B6E: mov      eax, dword ptr [ebp - 4]
0x470B71: mov      eax, dword ptr [eax + 0x5a85c]
0x470B77: mov      edx, dword ptr [ebp - 0x14]
0x470B7A: call     0x40f6a0
0x470B7F: mov      dword ptr [ebp - 0x70], eax
0x470B82: mov      eax, dword ptr [ebp - 4]
0x470B85: mov      edx, dword ptr [eax + 0xc]
0x470B88: mov      eax, dword ptr [ebp - 0x70]
0x470B8B: mov      bx, 0xfffd
0x470B8F: call     0x403080
0x470B94: mov      eax, dword ptr [0x4a3f1c] ; *0x4A3F1C->0x4F9382
0x470B99: cmp      byte ptr [eax], 0
0x470B9C: je       0x470bbf
0x470B9E: push     0
0x470BA0: push     0
0x470BA2: mov      eax, dword ptr [ebp - 0x70]
0x470BA5: mov      eax, dword ptr [eax + 0x78]
0x470BA8: push     eax
0x470BA9: push     0
0x470BAB: mov      eax, dword ptr [ebp - 0x70]
0x470BAE: mov      ecx, dword ptr [eax + 0x2c]
0x470BB1: mov      eax, dword ptr [ebp - 0x70]
0x470BB4: mov      edx, dword ptr [eax + 0x28]
0x470BB7: mov      eax, dword ptr [ebp - 4]
0x470BBA: call     0x46fc54
0x470BBF: inc      dword ptr [ebp - 0x14]
0x470BC2: dec      dword ptr [ebp - 0x7c]
0x470BC5: jne      0x470b6e
0x470BC7: xor      eax, eax
0x470BC9: pop      edx
0x470BCA: pop      ecx
0x470BCB: pop      ecx
0x470BCC: mov      dword ptr fs:[eax], edx
0x470BCF: jmp      0x470be5
0x470BD1: jmp      0x403310
0x470BD6: mov      eax, 0x471d58 ; "105"
0x470BDB: call     0x45ee2c
0x470BE0: call     0x4035c0
0x470BE5: xor      eax, eax
0x470BE7: push     ebp
0x470BE8: push     0x471411
0x470BED: push     dword ptr fs:[eax]
0x470BF0: mov      dword ptr fs:[eax], esp
0x470BF3: mov      eax, dword ptr [ebp - 0x34]
0x470BF6: sub      eax, 0x20
0x470BF9: mov      dword ptr [ebp - 0x1c], eax
0x470BFC: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x470C01: mov      eax, dword ptr [eax]
0x470C03: mov      eax, dword ptr [eax + 0x2ae58]
0x470C09: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x470C0F: mov      edx, dword ptr [edx]
0x470C11: sub      eax, dword ptr [edx + 0x2ae78]
0x470C17: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x470C1D: mov      edx, dword ptr [edx]
0x470C1F: mov      edx, dword ptr [edx + 0x2ae60]
0x470C25: mov      ecx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x470C2B: mov      ecx, dword ptr [ecx]
0x470C2D: sub      edx, dword ptr [ecx + 0x2ae78]
0x470C33: add      edx, 0x23
0x470C36: sub      edx, eax
0x470C38: jl       0x471407
0x470C3E: inc      edx
0x470C3F: mov      dword ptr [ebp - 0x7c], edx
0x470C42: mov      dword ptr [ebp - 0x10], eax
0x470C45: cmp      dword ptr [ebp - 0x10], 0
0x470C49: jge      0x470c54
0x470C4B: add      dword ptr [ebp - 0x1c], 0x20
0x470C4F: jmp      0x4713fb
0x470C54: mov      eax, dword ptr [ebp - 0x30]
0x470C57: sub      eax, 0x60
0x470C5A: mov      dword ptr [ebp - 0x18], eax
0x470C5D: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x470C62: mov      eax, dword ptr [eax]
0x470C64: mov      eax, dword ptr [eax + 0x2ae54]
0x470C6A: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x470C70: mov      edx, dword ptr [edx]
0x470C72: sub      eax, dword ptr [edx + 0x2ae74]
0x470C78: sub      eax, 2
0x470C7B: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x470C81: mov      edx, dword ptr [edx]
0x470C83: mov      edx, dword ptr [edx + 0x2ae5c]
0x470C89: mov      ecx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x470C8F: mov      ecx, dword ptr [ecx]
0x470C91: sub      edx, dword ptr [ecx + 0x2ae74]
0x470C97: add      edx, 2
0x470C9A: sub      edx, eax
0x470C9C: jl       0x470fb7
0x470CA2: inc      edx
0x470CA3: mov      dword ptr [ebp - 0x84], edx
0x470CA9: mov      dword ptr [ebp - 0xc], eax
0x470CAC: cmp      dword ptr [ebp - 0xc], 0
0x470CB0: jl       0x470fa4
0x470CB6: cmp      dword ptr [ebp - 0xc], 0x78
0x470CBA: jge      0x470fa4
0x470CC0: cmp      dword ptr [ebp - 0x10], 0
0x470CC4: jl       0x470fa4
0x470CCA: cmp      dword ptr [ebp - 0x10], 0x78
0x470CCE: jge      0x470fa4
0x470CD4: imul     eax, dword ptr [ebp - 0xc], 0x16b
0x470CDB: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x470CE1: mov      edx, dword ptr [edx]
0x470CE3: lea      eax, [edx + eax*4]
0x470CE6: mov      edx, dword ptr [ebp - 0x10]
0x470CE9: lea      edx, [edx + edx*2]
0x470CEC: mov      ax, word ptr [eax + edx*4 + 0xc]
0x470CF1: and      ax, 0x7fff
0x470CF5: movzx    eax, ax
0x470CF8: mov      dword ptr [ebp - 0x3c], eax
0x470CFB: cmp      dword ptr [ebp - 0x3c], 0
0x470CFF: jle      0x470fa4
0x470D05: mov      byte ptr [ebp - 0x5d], 0
0x470D09: imul     eax, dword ptr [ebp - 0xc], 0x16b
0x470D10: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x470D16: mov      edx, dword ptr [edx]
0x470D18: lea      eax, [edx + eax*4]
0x470D1B: mov      edx, dword ptr [ebp - 0x10]
0x470D1E: lea      edx, [edx + edx*2]
0x470D21: movzx    eax, byte ptr [eax + edx*4 + 0x12]
0x470D26: mov      dword ptr [ebp - 0x38], eax
0x470D29: imul     eax, dword ptr [ebp - 0xc], 0x16b
0x470D30: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x470D36: mov      edx, dword ptr [edx]
0x470D38: lea      eax, [edx + eax*4]
0x470D3B: mov      edx, dword ptr [ebp - 0x10]
0x470D3E: lea      edx, [edx + edx*2]
0x470D41: movzx    eax, byte ptr [eax + edx*4 + 0x10]
0x470D46: mov      dword ptr [ebp - 0x40], eax
0x470D49: test     dword ptr [ebp - 0x40], 0x80
0x470D50: jle      0x470d5a
0x470D52: mov      byte ptr [ebp - 0x5d], 1
0x470D56: and      dword ptr [ebp - 0x40], 0x7f
0x470D5A: cmp      dword ptr [ebp - 0x40], 0
0x470D5E: jle      0x470da4
0x470D60: imul     eax, dword ptr [ebp - 0xc], 0x16b
0x470D67: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x470D6D: mov      edx, dword ptr [edx]
0x470D6F: lea      eax, [edx + eax*4]
0x470D72: mov      edx, dword ptr [ebp - 0x10]
0x470D75: lea      edx, [edx + edx*2]
0x470D78: movzx    eax, byte ptr [eax + edx*4 + 0x11]
0x470D7D: mov      dword ptr [ebp - 0x44], eax
0x470D80: mov      eax, dword ptr [0x4a4304] ; *0x4A4304->0x4F9244
0x470D85: mov      eax, dword ptr [eax]
0x470D87: mov      edx, dword ptr [ebp - 0x40]
0x470D8A: imul     edx, dword ptr [ebp - 0x44]
0x470D8E: add      edx, dword ptr [ebp - 0x40]
0x470D91: mov      ecx, edx
0x470D93: cdq      
0x470D94: idiv     ecx
0x470D96: mov      eax, edx
0x470D98: mov      edx, dword ptr [ebp - 0x44]
0x470D9B: inc      edx
0x470D9C: mov      ecx, edx
0x470D9E: cdq      
0x470D9F: idiv     ecx
0x470DA1: add      dword ptr [ebp - 0x3c], eax
0x470DA4: imul     eax, dword ptr [ebp - 0xc], 0x16b
0x470DAB: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x470DB1: mov      edx, dword ptr [edx]
0x470DB3: lea      eax, [edx + eax*4]
0x470DB6: mov      edx, dword ptr [ebp - 0x10]
0x470DB9: lea      edx, [edx + edx*2]
0x470DBC: test     byte ptr [eax + edx*4 + 0xf], 0x80
0x470DC1: jbe      0x470e08
0x470DC3: imul     eax, dword ptr [ebp - 0xc], 0x16b
0x470DCA: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x470DD0: mov      edx, dword ptr [edx]
0x470DD2: lea      eax, [edx + eax*4]
0x470DD5: mov      edx, dword ptr [ebp - 0x10]
0x470DD8: lea      edx, [edx + edx*2]
0x470DDB: test     byte ptr [eax + edx*4 + 0xe], 0x7f
0x470DE0: jbe      0x470e08
0x470DE2: imul     eax, dword ptr [ebp - 0xc], 0x16b
0x470DE9: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x470DEF: mov      edx, dword ptr [edx]
0x470DF1: lea      eax, [edx + eax*4]
0x470DF4: mov      edx, dword ptr [ebp - 0x10]
0x470DF7: lea      edx, [edx + edx*2]
0x470DFA: mov      al, byte ptr [eax + edx*4 + 0xf]
0x470DFE: and      al, 0x7f
0x470E00: and      eax, 0xff
0x470E05: add      dword ptr [ebp - 0x3c], eax
0x470E08: dec      dword ptr [ebp - 0x3c]
0x470E0B: cmp      byte ptr [ebp - 0x5d], 0
0x470E0F: jne      0x470f02
0x470E15: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x470E1A: mov      eax, dword ptr [eax]
0x470E1C: mov      ecx, dword ptr [ebp - 0x3c]
0x470E1F: mov      edx, dword ptr [ebp - 0x38]
0x470E22: call     0x460a00
0x470E27: mov      dword ptr [ebp - 0x58], eax
0x470E2A: cmp      dword ptr [ebp - 0x58], 0
0x470E2E: je       0x470fa4
0x470E34: mov      eax, dword ptr [ebp - 0x58]
0x470E37: call     0x44dc60
0x470E3C: cmp      eax, 0x30
0x470E3F: jne      0x470e52
0x470E41: mov      eax, dword ptr [ebp - 0x58]
0x470E44: call     0x44d944
0x470E49: cmp      eax, 0x20
0x470E4C: je       0x470fa4
0x470E52: mov      eax, dword ptr [ebp - 0x58]
0x470E55: call     0x44d944
0x470E5A: mov      edx, dword ptr [ebp - 0x1c]
0x470E5D: add      edx, 0x20
0x470E60: sub      edx, eax
0x470E62: mov      dword ptr [ebp - 0x20], edx
0x470E65: mov      eax, dword ptr [ebp - 0x58]
0x470E68: call     0x44dc60
0x470E6D: add      eax, dword ptr [ebp - 0x18]
0x470E70: test     eax, eax
0x470E72: jle      0x470ec5
0x470E74: cmp      dword ptr [ebp - 0x18], 0x320
0x470E7B: jg       0x470ec5
0x470E7D: mov      eax, dword ptr [ebp - 0x58]
0x470E80: call     0x44d944
0x470E85: add      eax, dword ptr [ebp - 0x20]
0x470E88: test     eax, eax
0x470E8A: jle      0x470ec5
0x470E8C: mov      eax, dword ptr [ebp - 0x20]
0x470E8F: cmp      eax, dword ptr [ebp - 0x54]
0x470E92: jge      0x470ec5
0x470E94: lea      edx, [ebp - 0x94]
0x470E9A: mov      eax, dword ptr [ebp - 0x58]
0x470E9D: call     0x44d8ec
0x470EA2: lea      eax, [ebp - 0x94]
0x470EA8: push     eax
0x470EA9: mov      eax, dword ptr [ebp - 0x58]
0x470EAC: push     eax
0x470EAD: push     1
0x470EAF: mov      eax, dword ptr [ebp - 4]
0x470EB2: mov      eax, dword ptr [eax + 0xc]
0x470EB5: mov      ecx, dword ptr [ebp - 0x20]
0x470EB8: mov      edx, dword ptr [ebp - 0x18]
0x470EBB: call     0x44d6cc
0x470EC0: jmp      0x470fa4
0x470EC5: mov      eax, dword ptr [ebp - 0x20]
0x470EC8: cmp      eax, dword ptr [ebp - 0x54]
0x470ECB: jge      0x470fa4
0x470ED1: lea      edx, [ebp - 0x94]
0x470ED7: mov      eax, dword ptr [ebp - 0x58]
0x470EDA: call     0x44d8ec
0x470EDF: lea      eax, [ebp - 0x94]
0x470EE5: push     eax
0x470EE6: mov      eax, dword ptr [ebp - 0x58]
0x470EE9: push     eax
0x470EEA: push     1
0x470EEC: mov      eax, dword ptr [ebp - 4]
0x470EEF: mov      eax, dword ptr [eax + 0xc]
0x470EF2: mov      ecx, dword ptr [ebp - 0x20]
0x470EF5: mov      edx, dword ptr [ebp - 0x18]
0x470EF8: call     0x44d6cc
0x470EFD: jmp      0x470fa4
0x470F02: lea      eax, [ebp - 0x48]
0x470F05: push     eax
0x470F06: lea      eax, [ebp - 0x4c]
0x470F09: push     eax
0x470F0A: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x470F0F: mov      eax, dword ptr [eax]
0x470F11: mov      ecx, dword ptr [ebp - 0x3c]
0x470F14: mov      edx, dword ptr [ebp - 0x38]
0x470F17: call     0x460afc
0x470F1C: mov      dword ptr [ebp - 0x58], eax
0x470F1F: cmp      dword ptr [ebp - 0x58], 0
0x470F23: je       0x470fa4
0x470F25: mov      eax, dword ptr [ebp - 0x1c]
0x470F28: add      eax, dword ptr [ebp - 0x4c]
0x470F2B: sub      eax, 0x44
0x470F2E: mov      dword ptr [ebp - 0x20], eax
0x470F31: cmp      dword ptr [ebp - 0x18], 0
0x470F35: jle      0x470f7f
0x470F37: mov      eax, dword ptr [ebp - 0x58]
0x470F3A: call     0x44d944
0x470F3F: add      eax, dword ptr [ebp - 0x20]
0x470F42: test     eax, eax
0x470F44: jle      0x470f7f
0x470F46: mov      eax, dword ptr [ebp - 0x58]
0x470F49: call     0x44dc60
0x470F4E: add      eax, dword ptr [ebp - 0x18]
0x470F51: cmp      eax, 0x320
0x470F56: jge      0x470f7f
0x470F58: mov      eax, dword ptr [ebp - 0x20]
0x470F5B: cmp      eax, dword ptr [ebp - 0x54]
0x470F5E: jge      0x470f7f
0x470F60: mov      eax, dword ptr [ebp - 0x58]
0x470F63: push     eax
0x470F64: push     1
0x470F66: mov      edx, dword ptr [ebp - 0x18]
0x470F69: add      edx, dword ptr [ebp - 0x48]
0x470F6C: sub      edx, 2
0x470F6F: mov      eax, dword ptr [ebp - 4]
0x470F72: mov      eax, dword ptr [eax + 0xc]
0x470F75: mov      ecx, dword ptr [ebp - 0x20]
0x470F78: call     0x455578
0x470F7D: jmp      0x470fa4
0x470F7F: mov      eax, dword ptr [ebp - 0x20]
0x470F82: cmp      eax, dword ptr [ebp - 0x54]
0x470F85: jge      0x470fa4
0x470F87: mov      eax, dword ptr [ebp - 0x58]
0x470F8A: push     eax
0x470F8B: push     1
0x470F8D: mov      edx, dword ptr [ebp - 0x18]
0x470F90: add      edx, dword ptr [ebp - 0x48]
0x470F93: sub      edx, 2
0x470F96: mov      eax, dword ptr [ebp - 4]
0x470F99: mov      eax, dword ptr [eax + 0xc]
0x470F9C: mov      ecx, dword ptr [ebp - 0x20]
0x470F9F: call     0x455578
0x470FA4: add      dword ptr [ebp - 0x18], 0x30
0x470FA8: inc      dword ptr [ebp - 0xc]
0x470FAB: dec      dword ptr [ebp - 0x84]
0x470FB1: jne      0x470cac
0x470FB7: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x470FBC: mov      eax, dword ptr [eax]
0x470FBE: mov      eax, dword ptr [eax + 0x2ae60]
0x470FC4: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x470FCA: mov      edx, dword ptr [edx]
0x470FCC: sub      eax, dword ptr [edx + 0x2ae78]
0x470FD2: cmp      eax, dword ptr [ebp - 0x10]
0x470FD5: jl       0x4713f7
0x470FDB: mov      eax, dword ptr [0x4a419c] ; *0x4A419C->0x4F9234
0x470FE0: cmp      byte ptr [eax], 0
0x470FE3: jne      0x4713f7
0x470FE9: mov      eax, dword ptr [0x4a40a8] ; *0x4A40A8->0x4F7DCC
0x470FEE: mov      eax, dword ptr [eax]
0x470FF0: mov      eax, dword ptr [eax + 4]
0x470FF3: mov      eax, dword ptr [eax + 8]
0x470FF6: dec      eax
0x470FF7: test     eax, eax
0x470FF9: jl       0x471077
0x470FFB: inc      eax
0x470FFC: mov      dword ptr [ebp - 0x84], eax
0x471002: mov      dword ptr [ebp - 0x14], 0
0x471009: mov      eax, dword ptr [0x4a40a8] ; *0x4A40A8->0x4F7DCC
0x47100E: mov      eax, dword ptr [eax]
0x471010: mov      eax, dword ptr [eax + 4]
0x471013: mov      edx, dword ptr [ebp - 0x14]
0x471016: call     0x40f6a0
0x47101B: mov      dword ptr [ebp - 0x68], eax
0x47101E: mov      eax, dword ptr [ebp - 0x68]
0x471021: mov      eax, dword ptr [eax + 8]
0x471024: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x47102A: mov      edx, dword ptr [edx]
0x47102C: sub      eax, dword ptr [edx + 0x2ae78]
0x471032: cmp      eax, dword ptr [ebp - 0x10]
0x471035: jne      0x47106c
0x471037: mov      eax, dword ptr [ebp - 0x1c]
0x47103A: push     eax
0x47103B: mov      eax, dword ptr [ebp - 0x68]
0x47103E: mov      eax, dword ptr [eax + 4]
0x471041: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x471047: mov      edx, dword ptr [edx]
0x471049: sub      eax, dword ptr [edx + 0x2ae54]
0x47104F: mov      ecx, eax
0x471051: shl      ecx, 4
0x471054: lea      ecx, [ecx + ecx*2]
0x471057: add      ecx, dword ptr [ebp - 0x30]
0x47105A: mov      eax, dword ptr [ebp - 4]
0x47105D: mov      edx, dword ptr [eax + 0xc]
0x471060: mov      eax, dword ptr [ebp - 0x68]
0x471063: mov      bx, 0xffff
0x471067: call     0x403080
0x47106C: inc      dword ptr [ebp - 0x14]
0x47106F: dec      dword ptr [ebp - 0x84]
0x471075: jne      0x471009
0x471077: mov      eax, dword ptr [0x4a4204] ; *0x4A4204->0x4F7DA4
0x47107C: mov      eax, dword ptr [eax]
0x47107E: mov      eax, dword ptr [eax + 8]
0x471081: dec      eax
0x471082: test     eax, eax
0x471084: jl       0x471241
0x47108A: inc      eax
0x47108B: mov      dword ptr [ebp - 0x84], eax
0x471091: mov      dword ptr [ebp - 0x14], 0
0x471098: mov      eax, dword ptr [0x4a4204] ; *0x4A4204->0x4F7DA4
0x47109D: mov      eax, dword ptr [eax]
0x47109F: mov      edx, dword ptr [ebp - 0x14]
0x4710A2: call     0x40f6a0
0x4710A7: mov      dword ptr [ebp - 0x64], eax
0x4710AA: cmp      dword ptr [ebp - 0x64], 0
0x4710AE: je       0x471232
0x4710B4: mov      eax, dword ptr [ebp - 0x64]
0x4710B7: movzx    eax, word ptr [eax + 6]
0x4710BB: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x4710C1: mov      edx, dword ptr [edx]
0x4710C3: sub      eax, dword ptr [edx + 0x2ae78]
0x4710C9: cmp      eax, dword ptr [ebp - 0x10]
0x4710CC: jne      0x471232
0x4710D2: mov      eax, dword ptr [ebp - 0x64]
0x4710D5: movzx    edx, word ptr [eax + 8]
0x4710D9: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4710DE: mov      eax, dword ptr [eax]
0x4710E0: mov      eax, dword ptr [eax + 0x330]
0x4710E6: call     0x45351c
0x4710EB: mov      dword ptr [ebp - 0x5c], eax
0x4710EE: cmp      dword ptr [ebp - 0x5c], 0
0x4710F2: je       0x471232
0x4710F8: mov      eax, dword ptr [ebp - 0x64]
0x4710FB: movzx    eax, word ptr [eax + 4]
0x4710FF: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x471105: mov      edx, dword ptr [edx]
0x471107: sub      eax, dword ptr [edx + 0x2ae54]
0x47110D: shl      eax, 4
0x471110: lea      eax, [eax + eax*2]
0x471113: add      eax, dword ptr [ebp - 0x30]
0x471116: mov      dword ptr [ebp - 0x24], eax
0x471119: mov      eax, dword ptr [ebp - 0x1c]
0x47111C: mov      dword ptr [ebp - 0x28], eax
0x47111F: mov      eax, dword ptr [0x4a4100] ; *0x4A4100->0x4F94B4
0x471124: mov      eax, dword ptr [eax]
0x471126: cmp      eax, dword ptr [ebp - 0x64]
0x471129: jne      0x4711dc
0x47112F: lea      edx, [ebp - 0x94]
0x471135: mov      eax, dword ptr [ebp - 0x5c]
0x471138: call     0x44d8ec
0x47113D: lea      eax, [ebp - 0x94]
0x471143: push     eax
0x471144: mov      eax, dword ptr [ebp - 0x5c]
0x471147: push     eax
0x471148: push     0
0x47114A: mov      eax, dword ptr [0x4a41dc] ; *0x4A41DC->0x4F94D4
0x47114F: mov      eax, dword ptr [eax]
0x471151: xor      ecx, ecx
0x471153: xor      edx, edx
0x471155: call     0x44d6cc
0x47115A: mov      eax, dword ptr [ebp - 0x5c]
0x47115D: call     0x44d944
0x471162: push     eax
0x471163: mov      eax, dword ptr [0x4a41dc] ; *0x4A41DC->0x4F94D4
0x471168: mov      eax, dword ptr [eax]
0x47116A: push     eax
0x47116B: push     2
0x47116D: mov      eax, dword ptr [ebp - 0x5c]
0x471170: call     0x44dc60
0x471175: mov      ecx, eax
0x471177: xor      edx, edx
0x471179: xor      eax, eax
0x47117B: call     0x455a4c
0x471180: lea      edx, [ebp - 0x94]
0x471186: mov      eax, dword ptr [ebp - 0x5c]
0x471189: call     0x44d8ec
0x47118E: lea      eax, [ebp - 0x94]
0x471194: push     eax
0x471195: mov      eax, dword ptr [0x4a41dc] ; *0x4A41DC->0x4F94D4
0x47119A: mov      eax, dword ptr [eax]
0x47119C: push     eax
0x47119D: push     1
0x47119F: mov      eax, dword ptr [ebp - 0x5c]
0x4711A2: call     0x44d944
0x4711A7: sar      eax, 1
0x4711A9: jns      0x4711ae
0x4711AB: adc      eax, 0
0x4711AE: mov      edx, dword ptr [ebp - 0x28]
0x4711B1: add      edx, 0x10
0x4711B4: sub      edx, eax
0x4711B6: push     edx
0x4711B7: mov      eax, dword ptr [ebp - 0x5c]
0x4711BA: call     0x44dc60
0x4711BF: sar      eax, 1
0x4711C1: jns      0x4711c6
0x4711C3: adc      eax, 0
0x4711C6: mov      edx, dword ptr [ebp - 0x24]
0x4711C9: add      edx, 0x18
0x4711CC: sub      edx, eax
0x4711CE: mov      eax, dword ptr [ebp - 4]
0x4711D1: mov      eax, dword ptr [eax + 0xc]
0x4711D4: pop      ecx
0x4711D5: call     0x44d6cc
0x4711DA: jmp      0x471232
0x4711DC: lea      edx, [ebp - 0x94]
0x4711E2: mov      eax, dword ptr [ebp - 0x5c]
0x4711E5: call     0x44d8ec
0x4711EA: lea      eax, [ebp - 0x94]
0x4711F0: push     eax
0x4711F1: mov      eax, dword ptr [ebp - 0x5c]
0x4711F4: push     eax
0x4711F5: push     1
0x4711F7: mov      eax, dword ptr [ebp - 0x5c]
0x4711FA: call     0x44d944
0x4711FF: sar      eax, 1
0x471201: jns      0x471206
0x471203: adc      eax, 0
0x471206: mov      edx, dword ptr [ebp - 0x28]
0x471209: add      edx, 0x10
0x47120C: sub      edx, eax
0x47120E: push     edx
0x47120F: mov      eax, dword ptr [ebp - 0x5c]
0x471212: call     0x44dc60
0x471217: sar      eax, 1
0x471219: jns      0x47121e
0x47121B: adc      eax, 0
0x47121E: mov      edx, dword ptr [ebp - 0x24]
0x471221: add      edx, 0x18
0x471224: sub      edx, eax
0x471226: mov      eax, dword ptr [ebp - 4]
0x471229: mov      eax, dword ptr [eax + 0xc]
0x47122C: pop      ecx
0x47122D: call     0x44d6cc
0x471232: inc      dword ptr [ebp - 0x14]
0x471235: dec      dword ptr [ebp - 0x84]
0x47123B: jne      0x471098
0x471241: mov      eax, dword ptr [ebp - 4]
0x471244: mov      eax, dword ptr [eax + 0x5a854]
0x47124A: mov      eax, dword ptr [eax + 8]
0x47124D: dec      eax
0x47124E: test     eax, eax
0x471250: jl       0x47138e
0x471256: inc      eax
0x471257: mov      dword ptr [ebp - 0x84], eax
0x47125D: mov      dword ptr [ebp - 0x14], 0
0x471264: mov      eax, dword ptr [ebp - 4]
0x471267: mov      eax, dword ptr [eax + 0x5a854]
0x47126D: mov      edx, dword ptr [ebp - 0x14]
0x471270: call     0x40f6a0
0x471275: mov      dword ptr [ebp - 0x6c], eax
0x471278: mov      eax, dword ptr [ebp - 0x6c]
0x47127B: mov      eax, dword ptr [eax + 0xbc]
0x471281: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x471287: mov      edx, dword ptr [edx]
0x471289: sub      eax, dword ptr [edx + 0x2ae78]
0x47128F: mov      edx, dword ptr [ebp - 0x6c]
0x471292: sub      eax, dword ptr [edx + 0xc0]
0x471298: cmp      eax, dword ptr [ebp - 0x10]
0x47129B: jne      0x47137f
0x4712A1: mov      eax, dword ptr [ebp - 0x6c]
0x4712A4: mov      eax, dword ptr [eax + 0xb8]
0x4712AA: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x4712B0: mov      edx, dword ptr [edx]
0x4712B2: sub      eax, dword ptr [edx + 0x2ae54]
0x4712B8: shl      eax, 4
0x4712BB: lea      eax, [eax + eax*2]
0x4712BE: add      eax, dword ptr [ebp - 0x30]
0x4712C1: mov      edx, dword ptr [ebp - 0x6c]
0x4712C4: add      eax, dword ptr [edx + 0x98]
0x4712CA: add      eax, 0x18
0x4712CD: mov      edx, dword ptr [ebp - 0x6c]
0x4712D0: mov      dword ptr [edx + 0x8c], eax
0x4712D6: mov      eax, dword ptr [ebp - 0x6c]
0x4712D9: cmp      byte ptr [eax + 0x20], 0
0x4712DD: je       0x47130d
0x4712DF: mov      eax, dword ptr [ebp - 0x1c]
0x4712E2: add      eax, 0x20
0x4712E5: mov      edx, dword ptr [ebp - 0x6c]
0x4712E8: add      eax, dword ptr [edx + 0x9c]
0x4712EE: add      eax, 0x10
0x4712F1: sub      eax, 0x3c
0x4712F4: mov      edx, dword ptr [ebp - 0x6c]
0x4712F7: mov      edx, dword ptr [edx + 0xc0]
0x4712FD: shl      edx, 5
0x471300: add      eax, edx
0x471302: mov      edx, dword ptr [ebp - 0x6c]
0x471305: mov      dword ptr [edx + 0x90], eax
0x47130B: jmp      0x471339
0x47130D: mov      eax, dword ptr [ebp - 0x1c]
0x471310: add      eax, 0x20
0x471313: mov      edx, dword ptr [ebp - 0x6c]
0x471316: add      eax, dword ptr [edx + 0x9c]
0x47131C: add      eax, 0x10
0x47131F: sub      eax, 0x5f
0x471322: mov      edx, dword ptr [ebp - 0x6c]
0x471325: mov      edx, dword ptr [edx + 0xc0]
0x47132B: shl      edx, 5
0x47132E: add      eax, edx
0x471330: mov      edx, dword ptr [ebp - 0x6c]
0x471333: mov      dword ptr [edx + 0x90], eax
0x471339: mov      eax, dword ptr [ebp - 0x6c]
0x47133C: mov      eax, dword ptr [eax + 0xc0]
0x471342: shl      eax, 5
0x471345: add      eax, dword ptr [ebp - 0x1c]
0x471348: push     eax
0x471349: push     0
0x47134B: mov      eax, dword ptr [ebp - 0x6c]
0x47134E: mov      eax, dword ptr [eax + 0xb8]
0x471354: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x47135A: mov      edx, dword ptr [edx]
0x47135C: sub      eax, dword ptr [edx + 0x2ae54]
0x471362: mov      ecx, eax
0x471364: shl      ecx, 4
0x471367: lea      ecx, [ecx + ecx*2]
0x47136A: add      ecx, dword ptr [ebp - 0x30]
0x47136D: mov      eax, dword ptr [ebp - 4]
0x471370: mov      edx, dword ptr [eax + 0xc]
0x471373: mov      eax, dword ptr [ebp - 0x6c]
0x471376: mov      bx, 0xfff2
0x47137A: call     0x403080
0x47137F: inc      dword ptr [ebp - 0x14]
0x471382: dec      dword ptr [ebp - 0x84]
0x471388: jne      0x471264
0x47138E: mov      eax, dword ptr [ebp - 4]
0x471391: mov      eax, dword ptr [eax + 0x5a864]
0x471397: mov      eax, dword ptr [eax + 8]
0x47139A: dec      eax
0x47139B: test     eax, eax
0x47139D: jl       0x4713f7
0x47139F: inc      eax
0x4713A0: mov      dword ptr [ebp - 0x84], eax
0x4713A6: mov      dword ptr [ebp - 0x14], 0
0x4713AD: mov      eax, dword ptr [ebp - 4]
0x4713B0: mov      eax, dword ptr [eax + 0x5a864]
0x4713B6: mov      edx, dword ptr [ebp - 0x14]
0x4713B9: call     0x40f6a0
0x4713BE: mov      dword ptr [ebp - 0x70], eax
0x4713C1: mov      eax, dword ptr [ebp - 0x70]
0x4713C4: mov      eax, dword ptr [eax + 0x2c]
0x4713C7: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x4713CD: mov      edx, dword ptr [edx]
0x4713CF: sub      eax, dword ptr [edx + 0x2ae78]
0x4713D5: cmp      eax, dword ptr [ebp - 0x10]
0x4713D8: jne      0x4713ec
0x4713DA: mov      eax, dword ptr [ebp - 4]
0x4713DD: mov      edx, dword ptr [eax + 0xc]
0x4713E0: mov      eax, dword ptr [ebp - 0x70]
0x4713E3: mov      bx, 0xfffd
0x4713E7: call     0x403080
0x4713EC: inc      dword ptr [ebp - 0x14]
0x4713EF: dec      dword ptr [ebp - 0x84]
0x4713F5: jne      0x4713ad
0x4713F7: add      dword ptr [ebp - 0x1c], 0x20
0x4713FB: inc      dword ptr [ebp - 0x10]
0x4713FE: dec      dword ptr [ebp - 0x7c]
0x471401: jne      0x470c45
0x471407: xor      eax, eax
0x471409: pop      edx
0x47140A: pop      ecx
0x47140B: pop      ecx
0x47140C: mov      dword ptr fs:[eax], edx
0x47140F: jmp      0x471425
0x471411: jmp      0x403310
0x471416: mov      eax, 0x471d64 ; "106"
0x47141B: call     0x45ee2c
0x471420: call     0x4035c0
0x471425: xor      eax, eax
0x471427: push     ebp
0x471428: push     0x4716af
0x47142D: push     dword ptr fs:[eax]
0x471430: mov      dword ptr fs:[eax], esp
0x471433: mov      eax, dword ptr [0x4a3f1c] ; *0x4A3F1C->0x4F9382
0x471438: cmp      byte ptr [eax], 0
0x47143B: je       0x4716a5
0x471441: mov      eax, dword ptr [ebp - 0x34]
0x471444: sub      eax, 0x80
0x471449: mov      dword ptr [ebp - 0x1c], eax
0x47144C: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x471451: mov      eax, dword ptr [eax]
0x471453: mov      eax, dword ptr [eax + 0x2ae58]
0x471459: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x47145F: mov      edx, dword ptr [edx]
0x471461: sub      eax, dword ptr [edx + 0x2ae78]
0x471467: sub      eax, 4
0x47146A: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x471470: mov      edx, dword ptr [edx]
0x471472: mov      edx, dword ptr [edx + 0x2ae60]
0x471478: mov      ecx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x47147E: mov      ecx, dword ptr [ecx]
0x471480: sub      edx, dword ptr [ecx + 0x2ae78]
0x471486: add      edx, 0x23
0x471489: sub      edx, eax
0x47148B: jl       0x471590
0x471491: inc      edx
0x471492: mov      dword ptr [ebp - 0x7c], edx
0x471495: mov      dword ptr [ebp - 0x10], eax
0x471498: cmp      dword ptr [ebp - 0x10], 0
0x47149C: jge      0x4714a7
0x47149E: add      dword ptr [ebp - 0x1c], 0x20
0x4714A2: jmp      0x471584
0x4714A7: mov      eax, dword ptr [ebp - 0x30]
0x4714AA: sub      eax, 0xf0
0x4714AF: mov      dword ptr [ebp - 0x18], eax
0x4714B2: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x4714B7: mov      eax, dword ptr [eax]
0x4714B9: mov      eax, dword ptr [eax + 0x2ae54]
0x4714BF: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x4714C5: mov      edx, dword ptr [edx]
0x4714C7: sub      eax, dword ptr [edx + 0x2ae74]
0x4714CD: sub      eax, 5
0x4714D0: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x4714D6: mov      edx, dword ptr [edx]
0x4714D8: mov      edx, dword ptr [edx + 0x2ae5c]
0x4714DE: mov      ecx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x4714E4: mov      ecx, dword ptr [ecx]
0x4714E6: sub      edx, dword ptr [ecx + 0x2ae74]
0x4714EC: add      edx, 5
0x4714EF: sub      edx, eax
0x4714F1: jl       0x471580
0x4714F7: inc      edx
0x4714F8: mov      dword ptr [ebp - 0x84], edx
0x4714FE: mov      dword ptr [ebp - 0xc], eax
0x471501: cmp      dword ptr [ebp - 0xc], 0
0x471505: jl       0x471571
0x471507: cmp      dword ptr [ebp - 0xc], 0x78
0x47150B: jge      0x471571
0x47150D: cmp      dword ptr [ebp - 0x10], 0
0x471511: jl       0x471571
0x471513: cmp      dword ptr [ebp - 0x10], 0x78
0x471517: jge      0x471571
0x471519: imul     eax, dword ptr [ebp - 0xc], 0x16b
0x471520: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x471526: mov      edx, dword ptr [edx]
0x471528: lea      eax, [edx + eax*4]
0x47152B: mov      edx, dword ptr [ebp - 0x10]
0x47152E: lea      edx, [edx + edx*2]
0x471531: movzx    eax, byte ptr [eax + edx*4 + 0x13]
0x471536: mov      dword ptr [ebp - 0x50], eax
0x471539: cmp      dword ptr [ebp - 0x50], 0
0x47153D: jle      0x471571
0x47153F: push     0
0x471541: push     0
0x471543: mov      eax, dword ptr [ebp - 0x50]
0x471546: push     eax
0x471547: push     0
0x471549: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x47154E: mov      eax, dword ptr [eax]
0x471550: mov      ecx, dword ptr [eax + 0x2ae78]
0x471556: add      ecx, dword ptr [ebp - 0x10]
0x471559: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x47155E: mov      eax, dword ptr [eax]
0x471560: mov      edx, dword ptr [eax + 0x2ae74]
0x471566: add      edx, dword ptr [ebp - 0xc]
0x471569: mov      eax, dword ptr [ebp - 4]
0x47156C: call     0x46fc54
0x471571: add      dword ptr [ebp - 0x18], 0x30
0x471575: inc      dword ptr [ebp - 0xc]
0x471578: dec      dword ptr [ebp - 0x84]
0x47157E: jne      0x471501
0x471580: add      dword ptr [ebp - 0x1c], 0x20
0x471584: inc      dword ptr [ebp - 0x10]
0x471587: dec      dword ptr [ebp - 0x7c]
0x47158A: jne      0x471498
0x471590: mov      eax, dword ptr [ebp - 4]
0x471593: mov      eax, dword ptr [eax + 0x5a854]
0x471599: cmp      dword ptr [eax + 8], 0
0x47159D: jle      0x47164a
0x4715A3: mov      eax, dword ptr [ebp - 4]
0x4715A6: mov      eax, dword ptr [eax + 0x5a854]
0x4715AC: mov      eax, dword ptr [eax + 8]
0x4715AF: dec      eax
0x4715B0: test     eax, eax
0x4715B2: jl       0x4716a5
0x4715B8: inc      eax
0x4715B9: mov      dword ptr [ebp - 0x7c], eax
0x4715BC: mov      dword ptr [ebp - 0x14], 0
0x4715C3: mov      eax, dword ptr [ebp - 4]
0x4715C6: mov      eax, dword ptr [eax + 0x5a854]
0x4715CC: mov      edx, dword ptr [ebp - 0x14]
0x4715CF: call     0x40f6a0
0x4715D4: mov      dword ptr [ebp - 0x6c], eax
0x4715D7: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4715DC: mov      eax, dword ptr [eax]
0x4715DE: cmp      eax, dword ptr [ebp - 0x6c]
0x4715E1: je       0x4715f3
0x4715E3: mov      eax, dword ptr [ebp - 0x6c]
0x4715E6: mov      bx, 0xfff4
0x4715EA: call     0x403080
0x4715EF: test     eax, eax
0x4715F1: jle      0x47163c
0x4715F3: mov      eax, dword ptr [ebp - 0x6c]
0x4715F6: mov      eax, dword ptr [eax + 0x98]
0x4715FC: push     eax
0x4715FD: mov      eax, dword ptr [ebp - 0x6c]
0x471600: mov      eax, dword ptr [eax + 0x9c]
0x471606: push     eax
0x471607: mov      eax, dword ptr [ebp - 0x6c]
0x47160A: mov      bx, 0xfff4
0x47160E: call     0x403080
0x471613: push     eax
0x471614: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x471619: mov      eax, dword ptr [eax]
0x47161B: cmp      eax, dword ptr [ebp - 0x6c]
0x47161E: sete     al
0x471621: push     eax
0x471622: mov      eax, dword ptr [ebp - 0x6c]
0x471625: mov      ecx, dword ptr [eax + 0xbc]
0x47162B: mov      eax, dword ptr [ebp - 0x6c]
0x47162E: mov      edx, dword ptr [eax + 0xb8]
0x471634: mov      eax, dword ptr [ebp - 4]
0x471637: call     0x46fc54
0x47163C: inc      dword ptr [ebp - 0x14]
0x47163F: dec      dword ptr [ebp - 0x7c]
0x471642: jne      0x4715c3
0x471648: jmp      0x4716a5
0x47164A: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x47164F: cmp      dword ptr [eax], 0
0x471652: je       0x4716a5
0x471654: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x471659: mov      eax, dword ptr [eax]
0x47165B: mov      eax, dword ptr [eax + 0x98]
0x471661: push     eax
0x471662: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x471667: mov      eax, dword ptr [eax]
0x471669: mov      eax, dword ptr [eax + 0x9c]
0x47166F: push     eax
0x471670: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x471675: mov      eax, dword ptr [eax]
0x471677: mov      bx, 0xfff4
0x47167B: call     0x403080
0x471680: push     eax
0x471681: push     1
0x471683: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x471688: mov      eax, dword ptr [eax]
0x47168A: mov      ecx, dword ptr [eax + 0xbc]
0x471690: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x471695: mov      eax, dword ptr [eax]
0x471697: mov      edx, dword ptr [eax + 0xb8]
0x47169D: mov      eax, dword ptr [ebp - 4]
0x4716A0: call     0x46fc54
0x4716A5: xor      eax, eax
0x4716A7: pop      edx
0x4716A8: pop      ecx
0x4716A9: pop      ecx
0x4716AA: mov      dword ptr fs:[eax], edx
0x4716AD: jmp      0x4716c3
0x4716AF: jmp      0x403310
0x4716B4: mov      eax, 0x471d70 ; "107"
0x4716B9: call     0x45ee2c
0x4716BE: call     0x4035c0
0x4716C3: mov      eax, dword ptr [0x4a419c] ; *0x4A419C->0x4F9234
0x4716C8: cmp      byte ptr [eax], 0
0x4716CB: jne      0x4718b7
0x4716D1: xor      eax, eax
0x4716D3: push     ebp
0x4716D4: push     0x4718a3
0x4716D9: push     dword ptr fs:[eax]
0x4716DC: mov      dword ptr fs:[eax], esp
0x4716DF: mov      eax, dword ptr [0x4a4028] ; *0x4A4028->0x4F947A
0x4716E4: cmp      byte ptr [eax], 0
0x4716E7: jne      0x471757
0x4716E9: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4716EE: mov      eax, dword ptr [eax]
0x4716F0: test     byte ptr [eax + 0x1e], 0x80
0x4716F4: jne      0x471757
0x4716F6: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4716FB: mov      eax, dword ptr [eax]
0x4716FD: mov      eax, dword ptr [eax + 0xbc]
0x471703: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x471709: mov      edx, dword ptr [edx]
0x47170B: sub      eax, dword ptr [edx + 0x2ae58]
0x471711: dec      eax
0x471712: shl      eax, 5
0x471715: add      eax, dword ptr [ebp - 0x34]
0x471718: push     eax
0x471719: push     1
0x47171B: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x471720: mov      eax, dword ptr [eax]
0x471722: mov      eax, dword ptr [eax + 0xb8]
0x471728: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x47172E: mov      edx, dword ptr [edx]
0x471730: sub      eax, dword ptr [edx + 0x2ae54]
0x471736: mov      ecx, eax
0x471738: shl      ecx, 4
0x47173B: lea      ecx, [ecx + ecx*2]
0x47173E: add      ecx, dword ptr [ebp - 0x30]
0x471741: mov      eax, dword ptr [ebp - 4]
0x471744: mov      edx, dword ptr [eax + 0xc]
0x471747: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x47174C: mov      eax, dword ptr [eax]
0x47174E: mov      bx, 0xfff2
0x471752: call     0x403080
0x471757: mov      eax, dword ptr [0x4a3f10] ; *0x4A3F10->0x4F94A8
0x47175C: cmp      dword ptr [eax], 0
0x47175F: je       0x4717f8
0x471765: mov      edx, dword ptr [0x4a3f10] ; *0x4A3F10->0x4F94A8
0x47176B: mov      edx, dword ptr [edx]
0x47176D: mov      eax, dword ptr [ebp - 4]
0x471770: call     0x472ec0
0x471775: test     al, al
0x471777: je       0x4717f8
0x471779: mov      eax, dword ptr [0x4a3f10] ; *0x4A3F10->0x4F94A8
0x47177E: mov      eax, dword ptr [eax]
0x471780: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x471786: cmp      eax, dword ptr [edx]
0x471788: je       0x4717f8
0x47178A: mov      eax, dword ptr [0x4a3f10] ; *0x4A3F10->0x4F94A8
0x47178F: mov      eax, dword ptr [eax]
0x471791: test     byte ptr [eax + 0x1e], 0x80
0x471795: jne      0x4717f8
0x471797: mov      eax, dword ptr [0x4a3f10] ; *0x4A3F10->0x4F94A8
0x47179C: mov      eax, dword ptr [eax]
0x47179E: mov      eax, dword ptr [eax + 0xbc]
0x4717A4: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x4717AA: mov      edx, dword ptr [edx]
0x4717AC: sub      eax, dword ptr [edx + 0x2ae58]
0x4717B2: dec      eax
0x4717B3: shl      eax, 5
0x4717B6: add      eax, dword ptr [ebp - 0x34]
0x4717B9: push     eax
0x4717BA: push     1
0x4717BC: mov      eax, dword ptr [0x4a3f10] ; *0x4A3F10->0x4F94A8
0x4717C1: mov      eax, dword ptr [eax]
0x4717C3: mov      eax, dword ptr [eax + 0xb8]
0x4717C9: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x4717CF: mov      edx, dword ptr [edx]
0x4717D1: sub      eax, dword ptr [edx + 0x2ae54]
0x4717D7: mov      ecx, eax
0x4717D9: shl      ecx, 4
0x4717DC: lea      ecx, [ecx + ecx*2]
0x4717DF: add      ecx, dword ptr [ebp - 0x30]
0x4717E2: mov      eax, dword ptr [ebp - 4]
0x4717E5: mov      edx, dword ptr [eax + 0xc]
0x4717E8: mov      eax, dword ptr [0x4a3f10] ; *0x4A3F10->0x4F94A8
0x4717ED: mov      eax, dword ptr [eax]
0x4717EF: mov      bx, 0xfff2
0x4717F3: call     0x403080
0x4717F8: mov      eax, dword ptr [0x4a414c] ; *0x4A414C->0x4F94AC
0x4717FD: cmp      dword ptr [eax], 0
0x471800: je       0x471899
0x471806: mov      edx, dword ptr [0x4a414c] ; *0x4A414C->0x4F94AC
0x47180C: mov      edx, dword ptr [edx]
0x47180E: mov      eax, dword ptr [ebp - 4]
0x471811: call     0x472ec0
0x471816: test     al, al
0x471818: je       0x471899
0x47181A: mov      eax, dword ptr [0x4a414c] ; *0x4A414C->0x4F94AC
0x47181F: mov      eax, dword ptr [eax]
0x471821: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x471827: cmp      eax, dword ptr [edx]
0x471829: je       0x471899
0x47182B: mov      eax, dword ptr [0x4a414c] ; *0x4A414C->0x4F94AC
0x471830: mov      eax, dword ptr [eax]
0x471832: test     byte ptr [eax + 0x1e], 0x80
0x471836: jne      0x471899
0x471838: mov      eax, dword ptr [0x4a414c] ; *0x4A414C->0x4F94AC
0x47183D: mov      eax, dword ptr [eax]
0x47183F: mov      eax, dword ptr [eax + 0xbc]
0x471845: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x47184B: mov      edx, dword ptr [edx]
0x47184D: sub      eax, dword ptr [edx + 0x2ae58]
0x471853: dec      eax
0x471854: shl      eax, 5
0x471857: add      eax, dword ptr [ebp - 0x34]
0x47185A: push     eax
0x47185B: push     1
0x47185D: mov      eax, dword ptr [0x4a414c] ; *0x4A414C->0x4F94AC
0x471862: mov      eax, dword ptr [eax]
0x471864: mov      eax, dword ptr [eax + 0xb8]
0x47186A: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x471870: mov      edx, dword ptr [edx]
0x471872: sub      eax, dword ptr [edx + 0x2ae54]
0x471878: mov      ecx, eax
0x47187A: shl      ecx, 4
0x47187D: lea      ecx, [ecx + ecx*2]
0x471880: add      ecx, dword ptr [ebp - 0x30]
0x471883: mov      eax, dword ptr [ebp - 4]
0x471886: mov      edx, dword ptr [eax + 0xc]
0x471889: mov      eax, dword ptr [0x4a414c] ; *0x4A414C->0x4F94AC
0x47188E: mov      eax, dword ptr [eax]
0x471890: mov      bx, 0xfff2
0x471894: call     0x403080
0x471899: xor      eax, eax
0x47189B: pop      edx
0x47189C: pop      ecx
0x47189D: pop      ecx
0x47189E: mov      dword ptr fs:[eax], edx
0x4718A1: jmp      0x4718b7
0x4718A3: jmp      0x403310
0x4718A8: mov      eax, 0x471d7c ; "108"
0x4718AD: call     0x45ee2c
0x4718B2: call     0x4035c0
0x4718B7: xor      eax, eax
0x4718B9: push     ebp
0x4718BA: push     0x471a3d
0x4718BF: push     dword ptr fs:[eax]
0x4718C2: mov      dword ptr fs:[eax], esp
0x4718C5: mov      eax, dword ptr [ebp - 4]
0x4718C8: mov      eax, dword ptr [eax + 0x5a854]
0x4718CE: mov      eax, dword ptr [eax + 8]
0x4718D1: dec      eax
0x4718D2: test     eax, eax
0x4718D4: jl       0x471950
0x4718D6: inc      eax
0x4718D7: mov      dword ptr [ebp - 0x7c], eax
0x4718DA: mov      dword ptr [ebp - 0x14], 0
0x4718E1: mov      eax, dword ptr [ebp - 4]
0x4718E4: mov      eax, dword ptr [eax + 0x5a854]
0x4718EA: mov      edx, dword ptr [ebp - 0x14]
0x4718ED: call     0x40f6a0
0x4718F2: mov      dword ptr [ebp - 0x6c], eax
0x4718F5: mov      eax, dword ptr [ebp - 0x6c]
0x4718F8: mov      eax, dword ptr [eax + 0xbc]
0x4718FE: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x471904: mov      edx, dword ptr [edx]
0x471906: sub      eax, dword ptr [edx + 0x2ae58]
0x47190C: dec      eax
0x47190D: shl      eax, 5
0x471910: add      eax, dword ptr [ebp - 0x34]
0x471913: push     eax
0x471914: mov      eax, dword ptr [ebp - 0x6c]
0x471917: mov      eax, dword ptr [eax + 0xb8]
0x47191D: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x471923: mov      edx, dword ptr [edx]
0x471925: sub      eax, dword ptr [edx + 0x2ae54]
0x47192B: mov      ecx, eax
0x47192D: shl      ecx, 4
0x471930: lea      ecx, [ecx + ecx*2]
0x471933: add      ecx, dword ptr [ebp - 0x30]
0x471936: mov      eax, dword ptr [ebp - 4]
0x471939: mov      edx, dword ptr [eax + 0xc]
0x47193C: mov      eax, dword ptr [ebp - 0x6c]
0x47193F: mov      bx, 0xfff1
0x471943: call     0x403080
0x471948: inc      dword ptr [ebp - 0x14]
0x47194B: dec      dword ptr [ebp - 0x7c]
0x47194E: jne      0x4718e1
0x471950: mov      eax, dword ptr [ebp - 4]
0x471953: mov      eax, dword ptr [eax + 0x5a860]
0x471959: mov      eax, dword ptr [eax + 8]
0x47195C: dec      eax
0x47195D: test     eax, eax
0x47195F: jl       0x4719c5
0x471961: inc      eax
0x471962: mov      dword ptr [ebp - 0x7c], eax
0x471965: mov      dword ptr [ebp - 0x14], 0
0x47196C: mov      eax, dword ptr [ebp - 4]
0x47196F: mov      eax, dword ptr [eax + 0x5a860]
0x471975: mov      edx, dword ptr [ebp - 0x14]
0x471978: call     0x40f6a0
0x47197D: mov      dword ptr [ebp - 0x70], eax
0x471980: mov      eax, dword ptr [ebp - 4]
0x471983: mov      edx, dword ptr [eax + 0xc]
0x471986: mov      eax, dword ptr [ebp - 0x70]
0x471989: mov      bx, 0xfffd
0x47198D: call     0x403080
0x471992: mov      eax, dword ptr [0x4a3f1c] ; *0x4A3F1C->0x4F9382
0x471997: cmp      byte ptr [eax], 0
0x47199A: je       0x4719bd
0x47199C: push     0
0x47199E: push     0
0x4719A0: mov      eax, dword ptr [ebp - 0x70]
0x4719A3: mov      eax, dword ptr [eax + 0x78]
0x4719A6: push     eax
0x4719A7: push     0
0x4719A9: mov      eax, dword ptr [ebp - 0x70]
0x4719AC: mov      ecx, dword ptr [eax + 0x2c]
0x4719AF: mov      eax, dword ptr [ebp - 0x70]
0x4719B2: mov      edx, dword ptr [eax + 0x28]
0x4719B5: mov      eax, dword ptr [ebp - 4]
0x4719B8: call     0x46fc54
0x4719BD: inc      dword ptr [ebp - 0x14]
0x4719C0: dec      dword ptr [ebp - 0x7c]
0x4719C3: jne      0x47196c
0x4719C5: mov      eax, dword ptr [0x4a3f1c] ; *0x4A3F1C->0x4F9382
0x4719CA: cmp      byte ptr [eax], 0
0x4719CD: je       0x471a33
0x4719CF: mov      eax, dword ptr [0x4a40a8] ; *0x4A40A8->0x4F7DCC
0x4719D4: mov      eax, dword ptr [eax]
0x4719D6: mov      eax, dword ptr [eax + 4]
0x4719D9: mov      eax, dword ptr [eax + 8]
0x4719DC: dec      eax
0x4719DD: test     eax, eax
0x4719DF: jl       0x471a33
0x4719E1: inc      eax
0x4719E2: mov      dword ptr [ebp - 0x7c], eax
0x4719E5: mov      dword ptr [ebp - 0x14], 0
0x4719EC: mov      eax, dword ptr [0x4a40a8] ; *0x4A40A8->0x4F7DCC
0x4719F1: mov      eax, dword ptr [eax]
0x4719F3: mov      eax, dword ptr [eax + 4]
0x4719F6: mov      edx, dword ptr [ebp - 0x14]
0x4719F9: call     0x40f6a0
0x4719FE: mov      dword ptr [ebp - 0x68], eax
0x471A01: mov      eax, dword ptr [ebp - 0x68]
0x471A04: cmp      dword ptr [eax + 0x34], 0
0x471A08: jle      0x471a2b
0x471A0A: push     0
0x471A0C: push     0
0x471A0E: mov      eax, dword ptr [ebp - 0x68]
0x471A11: mov      eax, dword ptr [eax + 0x34]
0x471A14: push     eax
0x471A15: push     0
0x471A17: mov      eax, dword ptr [ebp - 0x68]
0x471A1A: mov      ecx, dword ptr [eax + 8]
0x471A1D: mov      eax, dword ptr [ebp - 0x68]
0x471A20: mov      edx, dword ptr [eax + 4]
0x471A23: mov      eax, dword ptr [ebp - 4]
0x471A26: call     0x46fc54
0x471A2B: inc      dword ptr [ebp - 0x14]
0x471A2E: dec      dword ptr [ebp - 0x7c]
0x471A31: jne      0x4719ec
0x471A33: xor      eax, eax
0x471A35: pop      edx
0x471A36: pop      ecx
0x471A37: pop      ecx
0x471A38: mov      dword ptr fs:[eax], edx
0x471A3B: jmp      0x471a51
0x471A3D: jmp      0x403310
0x471A42: mov      eax, 0x471d88 ; "109"
0x471A47: call     0x45ee2c
0x471A4C: call     0x4035c0
0x471A51: xor      eax, eax
0x471A53: push     ebp
0x471A54: push     0x471bb4
0x471A59: push     dword ptr fs:[eax]
0x471A5C: mov      dword ptr fs:[eax], esp
0x471A5F: mov      eax, dword ptr [0x4a4204] ; *0x4A4204->0x4F7DA4
0x471A64: mov      eax, dword ptr [eax]
0x471A66: mov      eax, dword ptr [eax + 8]
0x471A69: dec      eax
0x471A6A: test     eax, eax
0x471A6C: jl       0x471baa
0x471A72: inc      eax
0x471A73: mov      dword ptr [ebp - 0x7c], eax
0x471A76: mov      dword ptr [ebp - 0x14], 0
0x471A7D: mov      eax, dword ptr [0x4a4204] ; *0x4A4204->0x4F7DA4
0x471A82: mov      eax, dword ptr [eax]
0x471A84: mov      edx, dword ptr [ebp - 0x14]
0x471A87: call     0x40f6a0
0x471A8C: mov      dword ptr [ebp - 0x64], eax
0x471A8F: cmp      dword ptr [ebp - 0x64], 0
0x471A93: je       0x471b9e
0x471A99: call     0x406154
0x471A9E: mov      edx, dword ptr [ebp - 0x64]
0x471AA1: sub      eax, dword ptr [edx + 0xc]
0x471AA4: cmp      eax, 0x1388
0x471AA9: jbe      0x471ad0
0x471AAB: call     0x406154
0x471AB0: mov      edx, dword ptr [ebp - 0x64]
0x471AB3: mov      dword ptr [edx + 0xc], eax
0x471AB6: mov      eax, dword ptr [ebp - 0x64]
0x471AB9: mov      byte ptr [eax + 0x10], 1
0x471ABD: call     0x406154
0x471AC2: mov      edx, dword ptr [ebp - 0x64]
0x471AC5: mov      dword ptr [edx + 0x14], eax
0x471AC8: mov      eax, dword ptr [ebp - 0x64]
0x471ACB: xor      edx, edx
0x471ACD: mov      dword ptr [eax + 0x18], edx
0x471AD0: mov      eax, dword ptr [ebp - 0x64]
0x471AD3: cmp      byte ptr [eax + 0x10], 0
0x471AD7: je       0x471b9e
0x471ADD: call     0x406154
0x471AE2: mov      edx, dword ptr [ebp - 0x64]
0x471AE5: sub      eax, dword ptr [edx + 0x14]
0x471AE8: cmp      eax, 0x14
0x471AEB: jb       0x471afe
0x471AED: call     0x406154
0x471AF2: mov      edx, dword ptr [ebp - 0x64]
0x471AF5: mov      dword ptr [edx + 0x14], eax
0x471AF8: mov      eax, dword ptr [ebp - 0x64]
0x471AFB: inc      dword ptr [eax + 0x18]
0x471AFE: mov      eax, dword ptr [ebp - 0x64]
0x471B01: movzx    eax, word ptr [eax + 4]
0x471B05: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x471B0B: mov      edx, dword ptr [edx]
0x471B0D: sub      eax, dword ptr [edx + 0x2ae54]
0x471B13: shl      eax, 4
0x471B16: lea      eax, [eax + eax*2]
0x471B19: add      eax, dword ptr [ebp - 0x30]
0x471B1C: mov      dword ptr [ebp - 0x24], eax
0x471B1F: mov      eax, dword ptr [ebp - 0x64]
0x471B22: movzx    eax, word ptr [eax + 6]
0x471B26: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x471B2C: mov      edx, dword ptr [edx]
0x471B2E: sub      eax, dword ptr [edx + 0x2ae58]
0x471B34: dec      eax
0x471B35: shl      eax, 5
0x471B38: add      eax, dword ptr [ebp - 0x34]
0x471B3B: mov      dword ptr [ebp - 0x28], eax
0x471B3E: mov      eax, dword ptr [ebp - 0x64]
0x471B41: cmp      dword ptr [eax + 0x18], 0
0x471B45: jl       0x471b97
0x471B47: mov      eax, dword ptr [ebp - 0x64]
0x471B4A: cmp      dword ptr [eax + 0x18], 0xa
0x471B4E: jge      0x471b97
0x471B50: lea      eax, [ebp - 0x4c]
0x471B53: push     eax
0x471B54: lea      ecx, [ebp - 0x48]
0x471B57: mov      eax, dword ptr [ebp - 0x64]
0x471B5A: mov      edx, dword ptr [eax + 0x18]
0x471B5D: add      edx, 0x19a
0x471B63: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x471B68: mov      eax, dword ptr [eax]
0x471B6A: mov      eax, dword ptr [eax + 0x308]
0x471B70: call     0x453a48
0x471B75: mov      dword ptr [ebp - 0x58], eax
0x471B78: mov      eax, dword ptr [ebp - 0x58]
0x471B7B: push     eax
0x471B7C: push     1
0x471B7E: mov      ecx, dword ptr [ebp - 0x28]
0x471B81: add      ecx, dword ptr [ebp - 0x4c]
0x471B84: mov      edx, dword ptr [ebp - 0x24]
0x471B87: add      edx, dword ptr [ebp - 0x48]
0x471B8A: mov      eax, dword ptr [ebp - 4]
0x471B8D: mov      eax, dword ptr [eax + 0xc]
0x471B90: call     0x455578
0x471B95: jmp      0x471b9e
0x471B97: mov      eax, dword ptr [ebp - 0x64]
0x471B9A: mov      byte ptr [eax + 0x10], 0
0x471B9E: inc      dword ptr [ebp - 0x14]
0x471BA1: dec      dword ptr [ebp - 0x7c]
0x471BA4: jne      0x471a7d
0x471BAA: xor      eax, eax
0x471BAC: pop      edx
0x471BAD: pop      ecx
0x471BAE: pop      ecx
0x471BAF: mov      dword ptr fs:[eax], edx
0x471BB2: jmp      0x471bc8
0x471BB4: jmp      0x403310
0x471BB9: mov      eax, 0x471d94 ; "110"
0x471BBE: call     0x45ee2c
0x471BC3: call     0x4035c0
0x471BC8: xor      eax, eax
0x471BCA: push     ebp
0x471BCB: push     0x471ca3
0x471BD0: push     dword ptr fs:[eax]
0x471BD3: mov      dword ptr fs:[eax], esp
0x471BD6: mov      eax, dword ptr [0x4a3f1c] ; *0x4A3F1C->0x4F9382
0x471BDB: cmp      byte ptr [eax], 0
0x471BDE: je       0x471c34
0x471BE0: mov      eax, dword ptr [ebp - 4]
0x471BE3: call     0x46fd50
0x471BE8: mov      eax, dword ptr [ebp - 4]
0x471BEB: mov      ecx, dword ptr [eax + 0x57394]
0x471BF1: mov      eax, dword ptr [ebp - 4]
0x471BF4: mov      edx, dword ptr [eax + 0x57390]
0x471BFA: mov      eax, dword ptr [ebp - 4]
0x471BFD: mov      eax, dword ptr [eax + 0xc]
0x471C00: call     0x4552f0
0x471C05: lea      edx, [ebp - 0x94]
0x471C0B: mov      eax, dword ptr [ebp - 4]
0x471C0E: mov      eax, dword ptr [eax + 0xc]
0x471C11: call     0x44d8ec
0x471C16: lea      eax, [ebp - 0x94]
0x471C1C: push     eax
0x471C1D: mov      eax, dword ptr [ebp - 4]
0x471C20: mov      eax, dword ptr [eax + 0xc]
0x471C23: push     eax
0x471C24: push     0
0x471C26: xor      ecx, ecx
0x471C28: xor      edx, edx
0x471C2A: mov      eax, dword ptr [ebp - 8]
0x471C2D: call     0x44d6cc
0x471C32: jmp      0x471c99
0x471C34: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x471C39: mov      eax, dword ptr [eax]
0x471C3B: cmp      byte ptr [eax + 0x20], 0
0x471C3F: je       0x471c6c
0x471C41: mov      eax, dword ptr [ebp - 4]
0x471C44: mov      eax, dword ptr [eax + 0xc]
0x471C47: call     0x44d944
0x471C4C: push     eax
0x471C4D: mov      eax, dword ptr [ebp - 4]
0x471C50: mov      eax, dword ptr [eax + 0xc]
0x471C53: push     eax
0x471C54: push     1
0x471C56: mov      eax, dword ptr [ebp - 4]
0x471C59: mov      eax, dword ptr [eax + 0xc]
0x471C5C: call     0x44dc60
0x471C61: mov      ecx, eax
0x471C63: xor      edx, edx
0x471C65: xor      eax, eax
0x471C67: call     0x455a4c
0x471C6C: lea      edx, [ebp - 0x94]
0x471C72: mov      eax, dword ptr [ebp - 4]
0x471C75: mov      eax, dword ptr [eax + 0xc]
0x471C78: call     0x44d8ec
0x471C7D: lea      eax, [ebp - 0x94]
0x471C83: push     eax
0x471C84: mov      eax, dword ptr [ebp - 4]
0x471C87: mov      eax, dword ptr [eax + 0xc]
0x471C8A: push     eax
0x471C8B: push     0
0x471C8D: xor      ecx, ecx
0x471C8F: xor      edx, edx
0x471C91: mov      eax, dword ptr [ebp - 8]
0x471C94: call     0x44d6cc
0x471C99: xor      eax, eax
0x471C9B: pop      edx
0x471C9C: pop      ecx
0x471C9D: pop      ecx
0x471C9E: mov      dword ptr fs:[eax], edx
0x471CA1: jmp      0x471cb7
0x471CA3: jmp      0x403310
0x471CA8: mov      eax, 0x471da0 ; "111"
0x471CAD: call     0x45ee2c
0x471CB2: call     0x4035c0
0x471CB7: mov      eax, dword ptr [0x4a4084] ; *0x4A4084->0x4F9480
0x471CBC: cmp      dword ptr [eax], 0
0x471CBF: jle      0x471ce7
0x471CC1: mov      eax, dword ptr [0x4a4084] ; *0x4A4084->0x4F9480
0x471CC6: cmp      dword ptr [eax], 1
0x471CC9: jne      0x471cda
0x471CCB: mov      cl, 1
0x471CCD: mov      edx, dword ptr [ebp - 8]
0x471CD0: mov      eax, dword ptr [ebp - 4]
0x471CD3: call     0x46ff0c
0x471CD8: jmp      0x471ce7
0x471CDA: xor      ecx, ecx
0x471CDC: mov      edx, dword ptr [ebp - 8]
0x471CDF: mov      eax, dword ptr [ebp - 4]
0x471CE2: call     0x46ff0c
0x471CE7: xor      eax, eax
0x471CE9: pop      edx
0x471CEA: pop      ecx
0x471CEB: pop      ecx
0x471CEC: mov      dword ptr fs:[eax], edx
0x471CEF: push     0x471d04
0x471CF4: lea      eax, [ebp - 0x74]
0x471CF7: call     0x403a58
0x471CFC: ret      
0x471CFD: jmp      0x403518
0x471D02: jmp      0x471cf4
0x471D04: pop      edi
0x471D05: pop      esi
0x471D06: pop      ebx
0x471D07: mov      esp, ebp
0x471D09: pop      ebp
0x471D0A: ret      
0x471D0B: add      bh, bh
0x471D0F: dec      dword ptr [ecx]
0x471D11: add      byte ptr [eax], al
0x471D13: add      bh, al
0x471D15: jmp      0x471ce0
0x471D17: aam      0xba
0x471D19: add      byte ptr cs:[eax], al
0x471D1F: add      bh, bh
0x471D23: inc      dword ptr [ebx]
0x471D25: add      byte ptr [eax], al
0x471D27: add      byte ptr [ecx], dh
0x471D29: xor      byte ptr [ecx], dh
0x471D2B: add      bh, bh
0x471D2F: inc      dword ptr [ebx]
0x471D31: add      byte ptr [eax], al
0x471D33: add      byte ptr [ecx], dh
0x471D35: xor      byte ptr [edx], dh
0x471D37: add      bh, bh
0x471D3B: inc      dword ptr [ebx]
0x471D3D: add      byte ptr [eax], al
0x471D3F: add      byte ptr [ecx], dh
0x471D41: xor      byte ptr [ebx], dh
0x471D43: add      bh, bh
0x471D47: inc      dword ptr [ebx]
0x471D49: add      byte ptr [eax], al
0x471D4B: add      byte ptr [ecx], dh
0x471D4D: xor      byte ptr [eax + eax], dh
0x471D58: [STR len=3] "105"
0x471D64: [STR len=3] "106"
0x471D70: [STR len=3] "107"
0x471D7C: [STR len=3] "108"
0x471D88: [STR len=3] "109"
0x471D94: [STR len=3] "110"
0x471DA0: [STR len=3] "111"
0x471DA4: push     ebp
0x471DA5: mov      ebp, esp
0x471DA7: add      esp, -0x34
0x471DAA: mov      dword ptr [ebp - 0xc], ecx
0x471DAD: mov      dword ptr [ebp - 8], edx
0x471DB0: mov      dword ptr [ebp - 4], eax
0x471DB3: mov      eax, dword ptr [ebp + 8]
0x471DB6: mov      byte ptr [eax], 0
0x471DB9: cmp      dword ptr [ebp - 0xc], 0x6f
0x471DBD: je       0x471e00
0x471DBF: mov      eax, dword ptr [ebp - 4]
0x471DC2: mov      eax, dword ptr [eax + 0x5a860]
0x471DC8: mov      eax, dword ptr [eax + 8]
0x471DCB: dec      eax
0x471DCC: test     eax, eax
0x471DCE: jl       0x471e00
0x471DD0: inc      eax
0x471DD1: mov      dword ptr [ebp - 0x34], eax
0x471DD4: mov      dword ptr [ebp - 0x10], 0
0x471DDB: mov      edx, dword ptr [ebp - 0x10]
0x471DDE: mov      eax, dword ptr [ebp - 4]
0x471DE1: mov      eax, dword ptr [eax + 0x5a860]
0x471DE7: call     0x40f6a0
0x471DEC: mov      eax, dword ptr [eax + 8]
0x471DEF: cmp      eax, dword ptr [ebp - 0xc]
0x471DF2: je       0x4723a1
0x471DF8: inc      dword ptr [ebp - 0x10]
0x471DFB: dec      dword ptr [ebp - 0x34]
0x471DFE: jne      0x471ddb
0x471E00: lea      eax, [ebp - 0x14]
0x471E03: push     eax
0x471E04: lea      eax, [ebp - 0x18]
0x471E07: push     eax
0x471E08: mov      ecx, dword ptr [ebp + 0x24]
0x471E0B: mov      edx, dword ptr [ebp + 0x28]
0x471E0E: mov      eax, dword ptr [ebp - 4]
0x471E11: call     0x472494
0x471E16: lea      eax, [ebp - 0x1c]
0x471E19: push     eax
0x471E1A: lea      eax, [ebp - 0x20]
0x471E1D: push     eax
0x471E1E: mov      ecx, dword ptr [ebp + 0x1c]
0x471E21: mov      edx, dword ptr [ebp + 0x20]
0x471E24: mov      eax, dword ptr [ebp - 4]
0x471E27: call     0x472494
0x471E2C: cmp      dword ptr [ebp + 0x2c], 0
0x471E30: jle      0x471e46
0x471E32: lea      eax, [ebp - 0x24]
0x471E35: push     eax
0x471E36: lea      ecx, [ebp - 0x30]
0x471E39: mov      eax, dword ptr [ebp + 0x2c]
0x471E3C: dec      eax
0x471E3D: xor      edx, edx
0x471E3F: call     0x49c5bc
0x471E44: jmp      0x471e4e
0x471E46: mov      eax, dword ptr [ebp + 0x2c]
0x471E49: neg      eax
0x471E4B: mov      dword ptr [ebp - 0x24], eax
0x471E4E: mov      edx, dword ptr [ebp + 0x18]
0x471E51: mov      eax, dword ptr [ebp - 4]
0x471E54: call     0x472db0
0x471E59: mov      dword ptr [ebp - 0x2c], eax
0x471E5C: xor      eax, eax
0x471E5E: mov      dword ptr [ebp - 0x28], eax
0x471E61: xor      eax, eax
0x471E63: mov      al, byte ptr [ebp + 0x14]
0x471E66: cmp      eax, 0xa
0x471E69: ja       0x472346
0x471E6F: jmp      dword ptr [eax*4 + 0x471e76]
0x471E76: mov      byte ptr [0xa200471e], al
0x471E7B: push     ds
0x471E7C: inc      edi
0x471E7D: add      al, ch
0x471E7F: push     ds
0x471E80: inc      edi
0x471E81: add      byte ptr [edx - 0x2effb8e2], ah
0x471E87: and      dword ptr [edi], eax
0x471E8B: and      dword ptr [edi], eax
0x471E8E: inc      esp
0x471E8F: and      al, byte ptr [edi]
0x471E92: add      al, 0x22
0x471E94: inc      edi
0x471E95: add      byte ptr [ecx + 0x22], dh
0x471E98: inc      edi
0x471E99: add      dh, ch
0x471E9B: and      al, byte ptr [edi]
0x471E9E: inc      ecx
0x471E9F: and      eax, dword ptr [edi]
0x471EA2: mov      eax, dword ptr [ebp - 0x24]
0x471EA5: push     eax
0x471EA6: mov      eax, dword ptr [ebp - 0x14]
0x471EA9: push     eax
0x471EAA: mov      eax, dword ptr [ebp - 0x18]
0x471EAD: push     eax
0x471EAE: mov      eax, dword ptr [ebp - 0x1c]
0x471EB1: push     eax
0x471EB2: mov      eax, dword ptr [ebp - 0x20]
0x471EB5: push     eax
0x471EB6: mov      al, byte ptr [ebp + 0x14]
0x471EB9: push     eax
0x471EBA: mov      al, byte ptr [ebp + 0x10]
0x471EBD: push     eax
0x471EBE: mov      eax, dword ptr [ebp + 0xc]
0x471EC1: push     eax
0x471EC2: mov      ecx, dword ptr [ebp - 0xc]
0x471EC5: mov      dl, 1
0x471EC7: mov      eax, dword ptr [0x49c0d8]
0x471ECC: call     0x49c67c
0x471ED1: mov      dword ptr [ebp - 0x28], eax
0x471ED4: mov      eax, dword ptr [ebp - 0x2c]
0x471ED7: mov      edx, dword ptr [ebp - 0x28]
0x471EDA: mov      dword ptr [edx + 0x10], eax
0x471EDD: mov      eax, dword ptr [ebp + 8]
0x471EE0: mov      byte ptr [eax], 1
0x471EE3: jmp      0x472346
0x471EE8: mov      eax, dword ptr [ebp + 0x2c]
0x471EEB: add      eax, -0x12
0x471EEE: cmp      eax, 0xd
0x471EF1: ja       0x472187
0x471EF7: mov      al, byte ptr [eax + 0x471f04]
0x471EFD: jmp      dword ptr [eax*4 + 0x471f12]
0x471F04: add      dword ptr [eax], eax
0x471F06: add      byte ptr [edx], al
0x471F08: add      byte ptr [eax], al
0x471F0A: add      byte ptr [eax], al
0x471F0C: add      eax, dword ptr [eax + eax]
0x471F0F: add      byte ptr [0x47218706], al
0x471F15: add      byte ptr [esi], ch
0x471F17: pop      ds
0x471F18: inc      edi
0x471F19: add      byte ptr [edx - 0x16ffb8e1], al
0x471F1F: pop      ds
0x471F20: inc      edi
0x471F21: add      byte ptr [ecx + 0x20], dl
0x471F24: inc      edi
0x471F25: add      byte ptr [eax + 0x20004720], bh
0x471F2B: and      dword ptr [edi], eax
0x471F2E: mov      eax, dword ptr [ebp - 0x24]
0x471F31: push     eax
0x471F32: mov      eax, dword ptr [ebp - 0x14]
0x471F35: push     eax
0x471F36: mov      eax, dword ptr [ebp - 0x18]
0x471F39: push     eax
0x471F3A: mov      eax, dword ptr [ebp - 0x1c]
0x471F3D: push     eax
0x471F3E: mov      eax, dword ptr [ebp - 0x20]
0x471F41: push     eax
0x471F42: mov      al, byte ptr [ebp + 0x14]
0x471F45: push     eax
0x471F46: mov      al, byte ptr [ebp + 0x10]
0x471F49: push     eax
0x471F4A: mov      eax, dword ptr [ebp + 0xc]
0x471F4D: push     eax
0x471F4E: mov      ecx, dword ptr [ebp - 0xc]
0x471F51: mov      dl, 1
0x471F53: mov      eax, dword ptr [0x49c0d8]
0x471F58: call     0x49c67c
0x471F5D: mov      dword ptr [ebp - 0x28], eax
0x471F60: mov      eax, dword ptr [ebp - 0x28]
0x471F63: mov      dword ptr [eax + 0x1c], 0x622
0x471F6A: mov      eax, dword ptr [ebp - 0x2c]
0x471F6D: mov      edx, dword ptr [ebp - 0x28]
0x471F70: mov      dword ptr [edx + 0x10], eax
0x471F73: mov      eax, dword ptr [ebp - 0x28]
0x471F76: mov      dword ptr [eax + 0x74], 0x50
0x471F7D: jmp      0x472346
0x471F82: mov      eax, dword ptr [ebp - 0x24]
0x471F85: push     eax
0x471F86: mov      eax, dword ptr [ebp - 0x14]
0x471F89: push     eax
0x471F8A: mov      eax, dword ptr [ebp - 0x18]
0x471F8D: push     eax
0x471F8E: mov      eax, dword ptr [ebp - 0x1c]
0x471F91: push     eax
0x471F92: mov      eax, dword ptr [ebp - 0x20]
0x471F95: push     eax
0x471F96: mov      al, byte ptr [ebp + 0x14]
0x471F99: push     eax
0x471F9A: mov      al, byte ptr [ebp + 0x10]
0x471F9D: push     eax
0x471F9E: mov      eax, dword ptr [ebp + 0xc]
0x471FA1: push     eax
0x471FA2: mov      ecx, dword ptr [ebp - 0xc]
0x471FA5: mov      dl, 1
0x471FA7: mov      eax, dword ptr [0x49c0d8]
0x471FAC: call     0x49c67c
0x471FB1: mov      dword ptr [ebp - 0x28], eax
0x471FB4: mov      eax, dword ptr [ebp - 0x28]
0x471FB7: mov      dword ptr [eax + 0x1c], 0x67c
0x471FBE: mov      eax, dword ptr [ebp - 0x28]
0x471FC1: xor      edx, edx
0x471FC3: mov      dword ptr [eax + 0x10], edx
0x471FC6: mov      eax, dword ptr [ebp - 0x28]
0x471FC9: mov      dword ptr [eax + 0x74], 0x50
0x471FD0: mov      eax, dword ptr [ebp - 0x28]
0x471FD3: mov      dword ptr [eax + 0x70], 0x14
0x471FDA: mov      eax, dword ptr [ebp - 0x28]
0x471FDD: mov      dword ptr [eax + 0x78], 3
0x471FE4: jmp      0x472346
0x471FE9: mov      eax, dword ptr [ebp - 0x24]
0x471FEC: push     eax
0x471FED: mov      eax, dword ptr [ebp - 0x14]
0x471FF0: push     eax
0x471FF1: mov      eax, dword ptr [ebp - 0x18]
0x471FF4: push     eax
0x471FF5: mov      eax, dword ptr [ebp - 0x1c]
0x471FF8: push     eax
0x471FF9: mov      eax, dword ptr [ebp - 0x20]
0x471FFC: push     eax
0x471FFD: mov      al, byte ptr [ebp + 0x14]
0x472000: push     eax
0x472001: mov      al, byte ptr [ebp + 0x10]
0x472004: push     eax
0x472005: mov      eax, dword ptr [ebp + 0xc]
0x472008: push     eax
0x472009: mov      ecx, dword ptr [ebp - 0xc]
0x47200C: mov      dl, 1
0x47200E: mov      eax, dword ptr [0x49c0d8]
0x472013: call     0x49c67c
0x472018: mov      dword ptr [ebp - 0x28], eax
0x47201B: mov      eax, dword ptr [ebp - 0x28]
0x47201E: mov      dword ptr [eax + 0x1c], 0xf96
0x472025: mov      eax, dword ptr [ebp - 0x2c]
0x472028: mov      edx, dword ptr [ebp - 0x28]
0x47202B: mov      dword ptr [edx + 0x10], eax
0x47202E: mov      eax, dword ptr [ebp - 0x28]
0x472031: mov      dword ptr [eax + 0x74], 0x50
0x472038: mov      eax, dword ptr [ebp - 0x28]
0x47203B: mov      dword ptr [eax + 0x70], 0xa
0x472042: mov      eax, dword ptr [ebp - 0x28]
0x472045: mov      dword ptr [eax + 0x78], 2
0x47204C: jmp      0x472346
0x472051: mov      eax, dword ptr [ebp - 0x24]
0x472054: push     eax
0x472055: mov      eax, dword ptr [ebp - 0x14]
0x472058: push     eax
0x472059: mov      eax, dword ptr [ebp - 0x18]
0x47205C: push     eax
0x47205D: mov      eax, dword ptr [ebp - 0x1c]
0x472060: push     eax
0x472061: mov      eax, dword ptr [ebp - 0x20]
0x472064: push     eax
0x472065: mov      al, byte ptr [ebp + 0x14]
0x472068: push     eax
0x472069: mov      al, byte ptr [ebp + 0x10]
0x47206C: push     eax
0x47206D: mov      eax, dword ptr [ebp + 0xc]
0x472070: push     eax
0x472071: mov      ecx, dword ptr [ebp - 0xc]
0x472074: mov      dl, 1
0x472076: mov      eax, dword ptr [0x49c0d8]
0x47207B: call     0x49c67c
0x472080: mov      dword ptr [ebp - 0x28], eax
0x472083: mov      eax, dword ptr [ebp - 0x28]
0x472086: mov      dword ptr [eax + 0x1c], 0x708
0x47208D: mov      eax, dword ptr [ebp - 0x28]
0x472090: xor      edx, edx
0x472092: mov      dword ptr [eax + 0x10], edx
0x472095: mov      eax, dword ptr [ebp - 0x28]
0x472098: mov      dword ptr [eax + 0x74], 0x50
0x47209F: mov      eax, dword ptr [ebp - 0x28]
0x4720A2: mov      dword ptr [eax + 0x70], 0xa
0x4720A9: mov      eax, dword ptr [ebp - 0x28]
0x4720AC: mov      dword ptr [eax + 0x78], 3
0x4720B3: jmp      0x472346
0x4720B8: mov      eax, dword ptr [ebp - 0x24]
0x4720BB: push     eax
0x4720BC: mov      eax, dword ptr [ebp - 0x14]
0x4720BF: push     eax
0x4720C0: mov      eax, dword ptr [ebp - 0x18]
0x4720C3: push     eax
0x4720C4: mov      eax, dword ptr [ebp - 0x1c]
0x4720C7: push     eax
0x4720C8: mov      eax, dword ptr [ebp - 0x20]
0x4720CB: push     eax
0x4720CC: mov      al, byte ptr [ebp + 0x14]
0x4720CF: push     eax
0x4720D0: mov      al, byte ptr [ebp + 0x10]
0x4720D3: push     eax
0x4720D4: mov      eax, dword ptr [ebp + 0xc]
0x4720D7: push     eax
0x4720D8: mov      ecx, dword ptr [ebp - 0xc]
0x4720DB: mov      dl, 1
0x4720DD: mov      eax, dword ptr [0x49c0d8]
0x4720E2: call     0x49c67c
0x4720E7: mov      dword ptr [ebp - 0x28], eax
0x4720EA: mov      eax, dword ptr [ebp - 0x28]
0x4720ED: mov      dword ptr [eax + 0x1c], 0xf5a
0x4720F4: mov      eax, dword ptr [ebp - 0x2c]
0x4720F7: mov      edx, dword ptr [ebp - 0x28]
0x4720FA: mov      dword ptr [edx + 0x10], eax
0x4720FD: mov      eax, dword ptr [ebp - 0x28]
0x472100: mov      dword ptr [eax + 0x74], 0x50
0x472107: mov      eax, dword ptr [ebp - 0x28]
0x47210A: mov      dword ptr [eax + 0x70], 0x10
0x472111: mov      eax, dword ptr [ebp - 0x28]
0x472114: mov      dword ptr [eax + 0x78], 3
0x47211B: jmp      0x472346
0x472120: mov      eax, dword ptr [ebp - 0x24]
0x472123: push     eax
0x472124: mov      eax, dword ptr [ebp - 0x14]
0x472127: push     eax
0x472128: mov      eax, dword ptr [ebp - 0x18]
0x47212B: push     eax
0x47212C: mov      eax, dword ptr [ebp - 0x1c]
0x47212F: push     eax
0x472130: mov      eax, dword ptr [ebp - 0x20]
0x472133: push     eax
0x472134: mov      al, byte ptr [ebp + 0x14]
0x472137: push     eax
0x472138: mov      al, byte ptr [ebp + 0x10]
0x47213B: push     eax
0x47213C: mov      eax, dword ptr [ebp + 0xc]
0x47213F: push     eax
0x472140: mov      ecx, dword ptr [ebp - 0xc]
0x472143: mov      dl, 1
0x472145: mov      eax, dword ptr [0x49c0d8]
0x47214A: call     0x49c67c
0x47214F: mov      dword ptr [ebp - 0x28], eax
0x472152: mov      eax, dword ptr [ebp - 0x28]
0x472155: mov      dword ptr [eax + 0x1c], 0xf0a
0x47215C: mov      eax, dword ptr [ebp - 0x28]
0x47215F: xor      edx, edx
0x472161: mov      dword ptr [eax + 0x10], edx
0x472164: mov      eax, dword ptr [ebp - 0x28]
0x472167: mov      dword ptr [eax + 0x74], 0x50
0x47216E: mov      eax, dword ptr [ebp - 0x28]
0x472171: mov      dword ptr [eax + 0x70], 0x14
0x472178: mov      eax, dword ptr [ebp - 0x28]
0x47217B: mov      dword ptr [eax + 0x78], 3
0x472182: jmp      0x472346
0x472187: mov      eax, dword ptr [ebp - 0x24]
0x47218A: push     eax
0x47218B: mov      eax, dword ptr [ebp - 0x14]
0x47218E: push     eax
0x47218F: mov      eax, dword ptr [ebp - 0x18]
0x472192: push     eax
0x472193: mov      eax, dword ptr [ebp - 0x1c]
0x472196: push     eax
0x472197: mov      eax, dword ptr [ebp - 0x20]
0x47219A: push     eax
0x47219B: mov      al, byte ptr [ebp + 0x14]
0x47219E: push     eax
0x47219F: mov      al, byte ptr [ebp + 0x10]
0x4721A2: push     eax
0x4721A3: mov      eax, dword ptr [ebp + 0xc]
0x4721A6: push     eax
0x4721A7: mov      ecx, dword ptr [ebp - 0xc]
0x4721AA: mov      dl, 1
0x4721AC: mov      eax, dword ptr [0x49c0d8]
0x4721B1: call     0x49c67c
0x4721B6: mov      dword ptr [ebp - 0x28], eax
0x4721B9: mov      eax, dword ptr [ebp - 0x2c]
0x4721BC: mov      edx, dword ptr [ebp - 0x28]
0x4721BF: mov      dword ptr [edx + 0x10], eax
0x4721C2: mov      eax, dword ptr [ebp - 0x28]
0x4721C5: mov      dword ptr [eax + 0x74], 0x50
0x4721CC: jmp      0x472346
0x4721D1: xor      eax, eax
0x4721D3: mov      dword ptr [ebp - 0x28], eax
0x4721D6: jmp      0x472346
0x4721DB: mov      eax, dword ptr [ebp - 0x14]
0x4721DE: push     eax
0x4721DF: mov      eax, dword ptr [ebp - 0x18]
0x4721E2: push     eax
0x4721E3: mov      eax, dword ptr [ebp - 0x1c]
0x4721E6: push     eax
0x4721E7: mov      eax, dword ptr [ebp - 0x20]
0x4721EA: push     eax
0x4721EB: mov      ecx, 0x3a2
0x4721F0: mov      dl, 1
0x4721F2: mov      eax, dword ptr [0x49c340]
0x4721F7: call     0x49dc78
0x4721FC: mov      dword ptr [ebp - 0x28], eax
0x4721FF: jmp      0x472346
0x472204: mov      eax, dword ptr [ebp - 0x1c]
0x472207: push     eax
0x472208: mov      eax, dword ptr [ebp - 0x20]
0x47220B: push     eax
0x47220C: push     0
0x47220E: mov      ecx, 0xa
0x472213: mov      dl, 1
0x472215: mov      eax, dword ptr [0x49c3ac]
0x47221A: call     0x49e040
0x47221F: mov      dword ptr [ebp - 0x28], eax
0x472222: mov      eax, dword ptr [ebp - 0x28]
0x472225: mov      dword ptr [eax + 0x70], 6
0x47222C: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x472231: mov      eax, dword ptr [eax]
0x472233: mov      eax, dword ptr [eax + 0x3bc]
0x472239: mov      edx, dword ptr [ebp - 0x28]
0x47223C: mov      dword ptr [edx + 0x14], eax
0x47223F: jmp      0x472346
0x472244: mov      eax, dword ptr [ebp - 0x14]
0x472247: push     eax
0x472248: mov      eax, dword ptr [ebp - 0x18]
0x47224B: push     eax
0x47224C: mov      eax, dword ptr [ebp - 0x1c]
0x47224F: push     eax
0x472250: mov      eax, dword ptr [ebp - 0x20]
0x472253: push     eax
0x472254: mov      eax, dword ptr [ebp - 0x2c]
0x472257: push     eax
0x472258: mov      ecx, 0x3ca
0x47225D: mov      dl, 1
0x47225F: mov      eax, dword ptr [0x49c410]
0x472264: call     0x49e118
0x472269: mov      dword ptr [ebp - 0x28], eax
0x47226C: jmp      0x472346
0x472271: mov      eax, dword ptr [ebp + 0x2c]
0x472274: sub      eax, 0xa
0x472277: je       0x472280
0x472279: sub      eax, 7
0x47227C: je       0x4722b4
0x47227E: jmp      0x4722e6
0x472280: mov      eax, dword ptr [ebp - 0x14]
0x472283: push     eax
0x472284: mov      eax, dword ptr [ebp - 0x18]
0x472287: push     eax
0x472288: mov      eax, dword ptr [ebp - 0x1c]
0x47228B: push     eax
0x47228C: mov      eax, dword ptr [ebp - 0x20]
0x47228F: push     eax
0x472290: mov      eax, dword ptr [ebp - 0x2c]
0x472293: push     eax
0x472294: mov      ecx, 0x488
0x472299: mov      dl, 1
0x47229B: mov      eax, dword ptr [0x49c478]
0x4722A0: call     0x49e288
0x4722A5: mov      dword ptr [ebp - 0x28], eax
0x4722A8: mov      eax, dword ptr [ebp - 0x28]
0x4722AB: mov      dword ptr [eax + 0x1c], 0x550
0x4722B2: jmp      0x4722e6
0x4722B4: mov      eax, dword ptr [ebp - 0x14]
0x4722B7: push     eax
0x4722B8: mov      eax, dword ptr [ebp - 0x18]
0x4722BB: push     eax
0x4722BC: mov      eax, dword ptr [ebp - 0x1c]
0x4722BF: push     eax
0x4722C0: mov      eax, dword ptr [ebp - 0x20]
0x4722C3: push     eax
0x4722C4: mov      eax, dword ptr [ebp - 0x2c]
0x4722C7: push     eax
0x4722C8: mov      ecx, 0x488
0x4722CD: mov      dl, 1
0x4722CF: mov      eax, dword ptr [0x49c478]
0x4722D4: call     0x49e288
0x4722D9: mov      dword ptr [ebp - 0x28], eax
0x4722DC: mov      eax, dword ptr [ebp - 0x28]
0x4722DF: mov      dword ptr [eax + 0x1c], 0x604
0x4722E6: mov      eax, dword ptr [ebp + 8]
0x4722E9: mov      byte ptr [eax], 1
0x4722EC: jmp      0x472346
0x4722EE: mov      eax, dword ptr [ebp + 0x2c]
0x4722F1: push     eax
0x4722F2: mov      eax, dword ptr [ebp - 0x14]
0x4722F5: push     eax
0x4722F6: mov      eax, dword ptr [ebp - 0x18]
0x4722F9: push     eax
0x4722FA: mov      eax, dword ptr [ebp - 0x1c]
0x4722FD: push     eax
0x4722FE: mov      eax, dword ptr [ebp - 0x20]
0x472301: push     eax
0x472302: mov      ecx, 0x488
0x472307: mov      dl, 1
0x472309: mov      eax, dword ptr [0x49c4e0]
0x47230E: call     0x49e4bc
0x472313: mov      dword ptr [ebp - 0x28], eax
0x472316: mov      eax, dword ptr [ebp + 0x2c]
0x472319: sub      eax, 0xb
0x47231C: je       0x472323
0x47231E: dec      eax
0x47231F: je       0x47232f
0x472321: jmp      0x472339
0x472323: mov      eax, dword ptr [ebp - 0x28]
0x472326: mov      dword ptr [eax + 0x70], 0x10
0x47232D: jmp      0x472339
0x47232F: mov      eax, dword ptr [ebp - 0x28]
0x472332: mov      dword ptr [eax + 0x70], 0x10
0x472339: mov      eax, dword ptr [ebp + 8]
0x47233C: mov      byte ptr [eax], 1
0x47233F: jmp      0x472346
0x472341: xor      eax, eax
0x472343: mov      dword ptr [ebp - 0x28], eax
0x472346: cmp      dword ptr [ebp - 0x28], 0
0x47234A: je       0x4723a1
0x47234C: mov      eax, dword ptr [ebp + 0x20]
0x47234F: mov      edx, dword ptr [ebp - 0x28]
0x472352: mov      dword ptr [edx + 0x3c], eax
0x472355: mov      eax, dword ptr [ebp + 0x1c]
0x472358: mov      edx, dword ptr [ebp - 0x28]
0x47235B: mov      dword ptr [edx + 0x40], eax
0x47235E: mov      eax, dword ptr [ebp - 0x28]
0x472361: cmp      dword ptr [eax + 0x10], 0
0x472365: je       0x472387
0x472367: mov      eax, dword ptr [ebp - 0x28]
0x47236A: mov      eax, dword ptr [eax + 0x10]
0x47236D: movzx    eax, word ptr [eax + 8]
0x472371: mov      edx, dword ptr [ebp - 0x28]
0x472374: mov      dword ptr [edx + 0x3c], eax
0x472377: mov      eax, dword ptr [ebp - 0x28]
0x47237A: mov      eax, dword ptr [eax + 0x10]
0x47237D: movzx    eax, word ptr [eax + 0xa]
0x472381: mov      edx, dword ptr [ebp - 0x28]
0x472384: mov      dword ptr [edx + 0x40], eax
0x472387: mov      eax, dword ptr [ebp - 8]
0x47238A: mov      edx, dword ptr [ebp - 0x28]
0x47238D: mov      dword ptr [edx + 0xc], eax
0x472390: mov      edx, dword ptr [ebp - 0x28]
0x472393: mov      eax, dword ptr [ebp - 4]
0x472396: mov      eax, dword ptr [eax + 0x5a860]
0x47239C: call     0x40f588
0x4723A1: mov      esp, ebp
0x4723A3: pop      ebp
0x4723A4: ret      0x28
0x4723A7: nop      
0x4723A8: push     ebp
0x4723A9: mov      ebp, esp
0x4723AB: add      esp, -0x28
0x4723AE: mov      dword ptr [ebp - 0xc], ecx
0x4723B1: mov      dword ptr [ebp - 8], edx
0x4723B4: mov      dword ptr [ebp - 4], eax
0x4723B7: lea      eax, [ebp - 0x14]
0x4723BA: push     eax
0x4723BB: lea      eax, [ebp - 0x18]
0x4723BE: push     eax
0x4723BF: mov      ecx, dword ptr [ebp + 0x18]
0x4723C2: mov      edx, dword ptr [ebp - 0xc]
0x4723C5: mov      eax, dword ptr [ebp - 4]
0x4723C8: call     0x472494
0x4723CD: lea      eax, [ebp - 0x1c]
0x4723D0: push     eax
0x4723D1: lea      eax, [ebp - 0x20]
0x4723D4: push     eax
0x4723D5: mov      ecx, dword ptr [ebp + 0x10]
0x4723D8: mov      edx, dword ptr [ebp + 0x14]
0x4723DB: mov      eax, dword ptr [ebp - 4]
0x4723DE: call     0x472494
0x4723E3: mov      al, byte ptr [ebp + 8]
0x4723E6: sub      al, 0xb
0x4723E8: jne      0x47241a
0x4723EA: push     1
0x4723EC: mov      eax, dword ptr [ebp - 0x14]
0x4723EF: push     eax
0x4723F0: mov      eax, dword ptr [ebp - 0x18]
0x4723F3: push     eax
0x4723F4: mov      eax, dword ptr [ebp - 0x1c]
0x4723F7: push     eax
0x4723F8: mov      eax, dword ptr [ebp - 0x20]
0x4723FB: push     eax
0x4723FC: mov      al, byte ptr [ebp + 8]
0x4723FF: push     eax
0x472400: push     1
0x472402: push     0
0x472404: mov      ecx, 1
0x472409: mov      dl, 1
0x47240B: mov      eax, dword ptr [0x49c1a4]
0x472410: call     0x49d4f0
0x472415: mov      dword ptr [ebp - 0x24], eax
0x472418: jmp      0x472448
0x47241A: push     1
0x47241C: mov      eax, dword ptr [ebp - 0x14]
0x47241F: push     eax
0x472420: mov      eax, dword ptr [ebp - 0x18]
0x472423: push     eax
0x472424: mov      eax, dword ptr [ebp - 0x1c]
0x472427: push     eax
0x472428: mov      eax, dword ptr [ebp - 0x20]
0x47242B: push     eax
0x47242C: mov      al, byte ptr [ebp + 8]
0x47242F: push     eax
0x472430: push     1
0x472432: push     0
0x472434: mov      ecx, 1
0x472439: mov      dl, 1
0x47243B: mov      eax, dword ptr [0x49c144]
0x472440: call     0x49d4f0
0x472445: mov      dword ptr [ebp - 0x24], eax
0x472448: mov      eax, dword ptr [ebp + 0x14]
0x47244B: mov      edx, dword ptr [ebp - 0x24]
0x47244E: mov      dword ptr [edx + 0x3c], eax
0x472451: mov      eax, dword ptr [ebp + 0x10]
0x472454: mov      edx, dword ptr [ebp - 0x24]
0x472457: mov      dword ptr [edx + 0x40], eax
0x47245A: mov      edx, dword ptr [ebp + 0xc]
0x47245D: mov      eax, dword ptr [ebp - 4]
0x472460: call     0x472db0
0x472465: mov      edx, dword ptr [ebp - 0x24]
0x472468: mov      dword ptr [edx + 0x10], eax
0x47246B: mov      eax, dword ptr [ebp - 8]
0x47246E: mov      edx, dword ptr [ebp - 0x24]
0x472471: mov      dword ptr [edx + 0xc], eax
0x472474: mov      edx, dword ptr [ebp - 0x24]
0x472477: mov      eax, dword ptr [ebp - 4]
0x47247A: mov      eax, dword ptr [eax + 0x5a864]
0x472480: call     0x40f588
0x472485: mov      eax, dword ptr [ebp - 0x24]
0x472488: mov      dword ptr [ebp - 0x10], eax
0x47248B: mov      eax, dword ptr [ebp - 0x10]
0x47248E: mov      esp, ebp
0x472490: pop      ebp
0x472491: ret      0x14
0x472494: push     ebp
0x472495: mov      ebp, esp
0x472497: add      esp, -0xc
0x47249A: mov      dword ptr [ebp - 0xc], ecx
0x47249D: mov      dword ptr [ebp - 8], edx
0x4724A0: mov      dword ptr [ebp - 4], eax
0x4724A3: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4724A8: cmp      dword ptr [eax], 0
0x4724AB: je       0x472510
0x4724AD: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4724B2: mov      eax, dword ptr [eax]
0x4724B4: mov      edx, dword ptr [ebp - 8]
0x4724B7: sub      edx, dword ptr [eax + 0xb8]
0x4724BD: mov      eax, edx
0x4724BF: shl      eax, 4
0x4724C2: lea      eax, [eax + eax*2]
0x4724C5: add      eax, 0x16c
0x4724CA: add      eax, 0x18
0x4724CD: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4724D3: mov      edx, dword ptr [edx]
0x4724D5: sub      eax, dword ptr [edx + 0x98]
0x4724DB: mov      edx, dword ptr [ebp + 0xc]
0x4724DE: mov      dword ptr [edx], eax
0x4724E0: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4724E5: mov      eax, dword ptr [eax]
0x4724E7: mov      edx, dword ptr [ebp - 0xc]
0x4724EA: sub      edx, dword ptr [eax + 0xbc]
0x4724F0: mov      eax, edx
0x4724F2: shl      eax, 5
0x4724F5: add      eax, 0xc0
0x4724FA: add      eax, 0x10
0x4724FD: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x472503: mov      edx, dword ptr [edx]
0x472505: sub      eax, dword ptr [edx + 0x9c]
0x47250B: mov      edx, dword ptr [ebp + 8]
0x47250E: mov      dword ptr [edx], eax
0x472510: mov      esp, ebp
0x472512: pop      ebp
0x472513: ret      8
0x472516: mov      eax, eax
0x472518: push     ebp
0x472519: mov      ebp, esp
0x47251B: add      esp, -0x10
0x47251E: mov      dword ptr [ebp - 0xc], ecx
0x472521: mov      dword ptr [ebp - 8], edx
0x472524: mov      dword ptr [ebp - 4], eax
0x472527: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x47252C: cmp      dword ptr [eax], 0
0x47252F: je       0x4725bd
0x472535: mov      eax, dword ptr [ebp - 8]
0x472538: sub      eax, 0x16c
0x47253D: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x472543: mov      edx, dword ptr [edx]
0x472545: add      eax, dword ptr [edx + 0x98]
0x47254B: sub      eax, 0x30
0x47254E: mov      dword ptr [ebp - 0x10], eax
0x472551: fild     dword ptr [ebp - 0x10]
0x472554: fdiv     dword ptr [0x4725c4]
0x47255A: add      esp, -8
0x47255D: fstp     qword ptr [esp]
0x472560: wait     
0x472561: call     0x458d88
0x472566: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x47256C: mov      edx, dword ptr [edx]
0x47256E: add      eax, dword ptr [edx + 0xb8]
0x472574: mov      edx, dword ptr [ebp + 0xc]
0x472577: mov      dword ptr [edx], eax
0x472579: mov      eax, dword ptr [ebp - 0xc]
0x47257C: sub      eax, 0xc0
0x472581: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x472587: mov      edx, dword ptr [edx]
0x472589: add      eax, dword ptr [edx + 0x9c]
0x47258F: sub      eax, 0x20
0x472592: mov      dword ptr [ebp - 0x10], eax
0x472595: fild     dword ptr [ebp - 0x10]
0x472598: fdiv     dword ptr [0x4725c8]
0x47259E: add      esp, -8
0x4725A1: fstp     qword ptr [esp]
0x4725A4: wait     
0x4725A5: call     0x458d88
0x4725AA: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4725B0: mov      edx, dword ptr [edx]
0x4725B2: add      eax, dword ptr [edx + 0xbc]
0x4725B8: mov      edx, dword ptr [ebp + 8]
0x4725BB: mov      dword ptr [edx], eax
0x4725BD: mov      esp, ebp
0x4725BF: pop      ebp
0x4725C0: ret      8
0x4725C3: add      byte ptr [eax], al
0x4725C5: add      byte ptr [eax + 0x42], al
0x4725C8: add      byte ptr [eax], al
0x4725CA: add      byte ptr [edx + 0x55], al
0x4725CD: mov      ebp, esp
0x4725CF: add      esp, -0x30
0x4725D2: mov      dword ptr [ebp - 0xc], ecx
0x4725D5: mov      dword ptr [ebp - 8], edx
0x4725D8: mov      dword ptr [ebp - 4], eax
0x4725DB: xor      eax, eax
0x4725DD: mov      dword ptr [ebp - 0x10], eax
0x4725E0: mov      eax, dword ptr [ebp + 0xc]
0x4725E3: mov      dword ptr [eax], 0xffffffff
0x4725E9: lea      eax, [ebp - 0x1c]
0x4725EC: push     eax
0x4725ED: lea      eax, [ebp - 0x20]
0x4725F0: push     eax
0x4725F1: mov      ecx, dword ptr [ebp - 0xc]
0x4725F4: mov      edx, dword ptr [ebp - 8]
0x4725F7: mov      eax, dword ptr [ebp - 4]
0x4725FA: call     0x472518
0x4725FF: mov      eax, dword ptr [ebp - 0x20]
0x472602: add      eax, 8
0x472605: mov      edx, dword ptr [ebp - 0x20]
0x472608: dec      edx
0x472609: sub      edx, eax
0x47260B: jg       0x47275d
0x472611: dec      edx
0x472612: mov      dword ptr [ebp - 0x30], edx
0x472615: mov      dword ptr [ebp - 0x14], eax
0x472618: mov      eax, dword ptr [ebp - 4]
0x47261B: mov      eax, dword ptr [eax + 0x5a854]
0x472621: mov      eax, dword ptr [eax + 8]
0x472624: dec      eax
0x472625: cmp      eax, 0
0x472628: jl       0x472751
0x47262E: mov      dword ptr [ebp - 0x18], eax
0x472631: mov      eax, dword ptr [ebp - 4]
0x472634: mov      eax, dword ptr [eax + 0x5a854]
0x47263A: mov      edx, dword ptr [ebp - 0x18]
0x47263D: call     0x40f6a0
0x472642: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x472648: cmp      eax, dword ptr [edx]
0x47264A: je       0x472744
0x472650: mov      eax, dword ptr [ebp - 4]
0x472653: mov      eax, dword ptr [eax + 0x5a854]
0x472659: mov      edx, dword ptr [ebp - 0x18]
0x47265C: call     0x40f6a0
0x472661: mov      dword ptr [ebp - 0x2c], eax
0x472664: cmp      byte ptr [ebp + 8], 0
0x472668: je       0x472677
0x47266A: mov      eax, dword ptr [ebp - 0x2c]
0x47266D: cmp      byte ptr [eax + 0x20], 0
0x472671: jne      0x472744
0x472677: mov      eax, dword ptr [ebp - 0x2c]
0x47267A: cmp      byte ptr [eax + 0x5e], 0
0x47267E: je       0x472744
0x472684: mov      eax, dword ptr [ebp - 0x2c]
0x472687: cmp      byte ptr [eax + 0x5d], 0
0x47268B: je       0x472744
0x472691: mov      eax, dword ptr [ebp - 0x2c]
0x472694: movzx    eax, word ptr [eax + 0xa]
0x472698: cmp      eax, dword ptr [ebp - 0x14]
0x47269B: jne      0x472744
0x4726A1: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x4726A6: mov      eax, dword ptr [eax]
0x4726A8: mov      edx, dword ptr [ebp - 0x2c]
0x4726AB: mov      edx, dword ptr [edx + 0xb8]
0x4726B1: sub      edx, dword ptr [eax + 0x2ae54]
0x4726B7: mov      eax, edx
0x4726B9: shl      eax, 4
0x4726BC: lea      eax, [eax + eax*2]
0x4726BF: mov      edx, dword ptr [ebp - 4]
0x4726C2: add      eax, dword ptr [edx + 0x573f0]
0x4726C8: mov      edx, dword ptr [ebp - 0x2c]
0x4726CB: add      eax, dword ptr [edx + 0xa0]
0x4726D1: mov      edx, dword ptr [ebp - 0x2c]
0x4726D4: add      eax, dword ptr [edx + 0x98]
0x4726DA: mov      dword ptr [ebp - 0x24], eax
0x4726DD: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x4726E2: mov      eax, dword ptr [eax]
0x4726E4: mov      edx, dword ptr [ebp - 0x2c]
0x4726E7: mov      edx, dword ptr [edx + 0xbc]
0x4726ED: sub      edx, dword ptr [eax + 0x2ae58]
0x4726F3: dec      edx
0x4726F4: mov      eax, edx
0x4726F6: shl      eax, 5
0x4726F9: mov      edx, dword ptr [ebp - 4]
0x4726FC: add      eax, dword ptr [edx + 0x573f4]
0x472702: mov      edx, dword ptr [ebp - 0x2c]
0x472705: add      eax, dword ptr [edx + 0xac]
0x47270B: mov      edx, dword ptr [ebp - 0x2c]
0x47270E: add      eax, dword ptr [edx + 0x9c]
0x472714: mov      dword ptr [ebp - 0x28], eax
0x472717: mov      ecx, dword ptr [ebp - 0xc]
0x47271A: sub      ecx, dword ptr [ebp - 0x28]
0x47271D: mov      edx, dword ptr [ebp - 8]
0x472720: sub      edx, dword ptr [ebp - 0x24]
0x472723: mov      eax, dword ptr [ebp - 0x2c]
0x472726: call     0x497f20
0x47272B: test     al, al
0x47272D: je       0x472744
0x47272F: mov      eax, dword ptr [ebp - 0x2c]
0x472732: mov      dword ptr [ebp - 0x10], eax
0x472735: mov      eax, dword ptr [ebp + 0xc]
0x472738: inc      dword ptr [eax]
0x47273A: mov      eax, dword ptr [ebp + 0xc]
0x47273D: mov      eax, dword ptr [eax]
0x47273F: cmp      eax, dword ptr [ebp + 0x10]
0x472742: jge      0x47275d
0x472744: dec      dword ptr [ebp - 0x18]
0x472747: cmp      dword ptr [ebp - 0x18], -1
0x47274B: jne      0x472631
0x472751: dec      dword ptr [ebp - 0x14]
0x472754: inc      dword ptr [ebp - 0x30]
0x472757: jne      0x472618
0x47275D: mov      eax, dword ptr [ebp - 0x10]
0x472760: mov      esp, ebp
0x472762: pop      ebp
0x472763: ret      0xc
0x472766: mov      eax, eax
0x472768: push     ebp
0x472769: mov      ebp, esp
0x47276B: add      esp, -0x38
0x47276E: mov      dword ptr [ebp - 0xc], ecx
0x472771: mov      dword ptr [ebp - 8], edx
0x472774: mov      dword ptr [ebp - 4], eax
0x472777: mov      eax, dword ptr [ebp + 0x10]
0x47277A: push     eax
0x47277B: mov      eax, dword ptr [ebp + 0xc]
0x47277E: push     eax
0x47277F: mov      al, byte ptr [ebp + 8]
0x472782: push     eax
0x472783: mov      ecx, dword ptr [ebp - 0xc]
0x472786: mov      edx, dword ptr [ebp - 8]
0x472789: mov      eax, dword ptr [ebp - 4]
0x47278C: call     0x4725cc
0x472791: mov      dword ptr [ebp - 0x10], eax
0x472794: cmp      dword ptr [ebp - 0x10], 0
0x472798: jne      0x472995
0x47279E: mov      eax, dword ptr [ebp + 0xc]
0x4727A1: mov      dword ptr [eax], 0xffffffff
0x4727A7: lea      eax, [ebp - 0x1c]
0x4727AA: push     eax
0x4727AB: lea      eax, [ebp - 0x20]
0x4727AE: push     eax
0x4727AF: mov      ecx, dword ptr [ebp - 0xc]
0x4727B2: mov      edx, dword ptr [ebp - 8]
0x4727B5: mov      eax, dword ptr [ebp - 4]
0x4727B8: call     0x472518
0x4727BD: mov      eax, dword ptr [ebp - 0x20]
0x4727C0: add      eax, 8
0x4727C3: mov      edx, dword ptr [ebp - 0x20]
0x4727C6: dec      edx
0x4727C7: sub      edx, eax
0x4727C9: jg       0x472995
0x4727CF: dec      edx
0x4727D0: mov      dword ptr [ebp - 0x38], edx
0x4727D3: mov      dword ptr [ebp - 0x14], eax
0x4727D6: mov      eax, dword ptr [ebp - 4]
0x4727D9: mov      eax, dword ptr [eax + 0x5a854]
0x4727DF: mov      eax, dword ptr [eax + 8]
0x4727E2: dec      eax
0x4727E3: cmp      eax, 0
0x4727E6: jl       0x472989
0x4727EC: mov      dword ptr [ebp - 0x18], eax
0x4727EF: mov      eax, dword ptr [ebp - 4]
0x4727F2: mov      eax, dword ptr [eax + 0x5a854]
0x4727F8: mov      edx, dword ptr [ebp - 0x18]
0x4727FB: call     0x40f6a0
0x472800: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x472806: cmp      eax, dword ptr [edx]
0x472808: je       0x47297c
0x47280E: mov      eax, dword ptr [ebp - 4]
0x472811: mov      eax, dword ptr [eax + 0x5a854]
0x472817: mov      edx, dword ptr [ebp - 0x18]
0x47281A: call     0x40f6a0
0x47281F: mov      dword ptr [ebp - 0x34], eax
0x472822: cmp      byte ptr [ebp + 8], 0
0x472826: je       0x472835
0x472828: mov      eax, dword ptr [ebp - 0x34]
0x47282B: cmp      byte ptr [eax + 0x20], 0
0x47282F: jne      0x47297c
0x472835: mov      eax, dword ptr [ebp - 0x34]
0x472838: cmp      byte ptr [eax + 0x5e], 0
0x47283C: je       0x47297c
0x472842: mov      eax, dword ptr [ebp - 0x34]
0x472845: cmp      byte ptr [eax + 0x5d], 0
0x472849: je       0x47297c
0x47284F: mov      eax, dword ptr [ebp - 0x34]
0x472852: movzx    eax, word ptr [eax + 0xa]
0x472856: cmp      eax, dword ptr [ebp - 0x14]
0x472859: jne      0x47297c
0x47285F: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x472864: mov      eax, dword ptr [eax]
0x472866: mov      edx, dword ptr [ebp - 0x34]
0x472869: mov      edx, dword ptr [edx + 0xb8]
0x47286F: sub      edx, dword ptr [eax + 0x2ae54]
0x472875: mov      eax, edx
0x472877: shl      eax, 4
0x47287A: lea      eax, [eax + eax*2]
0x47287D: mov      edx, dword ptr [ebp - 4]
0x472880: add      eax, dword ptr [edx + 0x573f0]
0x472886: mov      edx, dword ptr [ebp - 0x34]
0x472889: add      eax, dword ptr [edx + 0xa0]
0x47288F: mov      edx, dword ptr [ebp - 0x34]
0x472892: add      eax, dword ptr [edx + 0x98]
0x472898: mov      dword ptr [ebp - 0x24], eax
0x47289B: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x4728A0: mov      eax, dword ptr [eax]
0x4728A2: mov      edx, dword ptr [ebp - 0x34]
0x4728A5: mov      edx, dword ptr [edx + 0xbc]
0x4728AB: sub      edx, dword ptr [eax + 0x2ae58]
0x4728B1: dec      edx
0x4728B2: mov      eax, edx
0x4728B4: shl      eax, 5
0x4728B7: mov      edx, dword ptr [ebp - 4]
0x4728BA: add      eax, dword ptr [edx + 0x573f4]
0x4728C0: mov      edx, dword ptr [ebp - 0x34]
0x4728C3: add      eax, dword ptr [edx + 0xac]
0x4728C9: mov      edx, dword ptr [ebp - 0x34]
0x4728CC: add      eax, dword ptr [edx + 0x9c]
0x4728D2: mov      dword ptr [ebp - 0x28], eax
0x4728D5: mov      eax, dword ptr [ebp - 0x34]
0x4728D8: call     0x497eb0
0x4728DD: cmp      eax, 0x28
0x4728E0: jle      0x4728f9
0x4728E2: mov      eax, dword ptr [ebp - 0x34]
0x4728E5: call     0x497eb0
0x4728EA: sub      eax, 0x28
0x4728ED: sar      eax, 1
0x4728EF: jns      0x4728f4
0x4728F1: adc      eax, 0
0x4728F4: mov      dword ptr [ebp - 0x2c], eax
0x4728F7: jmp      0x4728fe
0x4728F9: xor      eax, eax
0x4728FB: mov      dword ptr [ebp - 0x2c], eax
0x4728FE: mov      eax, dword ptr [ebp - 0x34]
0x472901: call     0x497ee8
0x472906: cmp      eax, 0x46
0x472909: jle      0x472922
0x47290B: mov      eax, dword ptr [ebp - 0x34]
0x47290E: call     0x497ee8
0x472913: sub      eax, 0x46
0x472916: sar      eax, 1
0x472918: jns      0x47291d
0x47291A: adc      eax, 0
0x47291D: mov      dword ptr [ebp - 0x30], eax
0x472920: jmp      0x472927
0x472922: xor      eax, eax
0x472924: mov      dword ptr [ebp - 0x30], eax
0x472927: mov      eax, dword ptr [ebp - 8]
0x47292A: sub      eax, dword ptr [ebp - 0x24]
0x47292D: cmp      eax, dword ptr [ebp - 0x2c]
0x472930: jl       0x47297c
0x472932: mov      eax, dword ptr [ebp - 0x34]
0x472935: call     0x497eb0
0x47293A: sub      eax, dword ptr [ebp - 0x2c]
0x47293D: mov      edx, dword ptr [ebp - 8]
0x472940: sub      edx, dword ptr [ebp - 0x24]
0x472943: cmp      eax, edx
0x472945: jl       0x47297c
0x472947: mov      eax, dword ptr [ebp - 0xc]
0x47294A: sub      eax, dword ptr [ebp - 0x28]
0x47294D: cmp      eax, dword ptr [ebp - 0x30]
0x472950: jl       0x47297c
0x472952: mov      eax, dword ptr [ebp - 0x34]
0x472955: call     0x497ee8
0x47295A: sub      eax, dword ptr [ebp - 0x30]
0x47295D: mov      edx, dword ptr [ebp - 0xc]
0x472960: sub      edx, dword ptr [ebp - 0x28]
0x472963: cmp      eax, edx
0x472965: jl       0x47297c
0x472967: mov      eax, dword ptr [ebp - 0x34]
0x47296A: mov      dword ptr [ebp - 0x10], eax
0x47296D: mov      eax, dword ptr [ebp + 0xc]
0x472970: inc      dword ptr [eax]
0x472972: mov      eax, dword ptr [ebp + 0xc]
0x472975: mov      eax, dword ptr [eax]
0x472977: cmp      eax, dword ptr [ebp + 0x10]
0x47297A: jge      0x472995
0x47297C: dec      dword ptr [ebp - 0x18]
0x47297F: cmp      dword ptr [ebp - 0x18], -1
0x472983: jne      0x4727ef
0x472989: dec      dword ptr [ebp - 0x14]
0x47298C: inc      dword ptr [ebp - 0x38]
0x47298F: jne      0x4727d6
0x472995: mov      eax, dword ptr [ebp - 0x10]
0x472998: mov      esp, ebp
0x47299A: pop      ebp
0x47299B: ret      0xc
0x47299E: mov      eax, eax
0x4729A0: push     ebp
0x4729A1: mov      ebp, esp
0x4729A3: add      esp, -0x2c
0x4729A6: mov      dword ptr [ebp - 0xc], ecx
0x4729A9: mov      dword ptr [ebp - 8], edx
0x4729AC: mov      dword ptr [ebp - 4], eax
0x4729AF: mov      byte ptr [ebp - 0xd], 0
0x4729B3: lea      eax, [ebp - 0x18]
0x4729B6: push     eax
0x4729B7: lea      eax, [ebp - 0x1c]
0x4729BA: push     eax
0x4729BB: mov      ecx, dword ptr [ebp - 0xc]
0x4729BE: mov      edx, dword ptr [ebp - 8]
0x4729C1: mov      eax, dword ptr [ebp - 4]
0x4729C4: call     0x472518
0x4729C9: mov      eax, dword ptr [ebp - 0x1c]
0x4729CC: add      eax, 2
0x4729CF: mov      edx, dword ptr [ebp - 0x1c]
0x4729D2: dec      edx
0x4729D3: sub      edx, eax
0x4729D5: jg       0x472ab4
0x4729DB: dec      edx
0x4729DC: mov      dword ptr [ebp - 0x28], edx
0x4729DF: mov      dword ptr [ebp - 0x14], eax
0x4729E2: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4729E7: mov      eax, dword ptr [eax]
0x4729E9: movzx    eax, word ptr [eax + 0xa]
0x4729ED: cmp      eax, dword ptr [ebp - 0x14]
0x4729F0: jne      0x472aa8
0x4729F6: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4729FB: mov      eax, dword ptr [eax]
0x4729FD: mov      eax, dword ptr [eax + 0xb8]
0x472A03: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x472A09: mov      edx, dword ptr [edx]
0x472A0B: sub      eax, dword ptr [edx + 0x2ae54]
0x472A11: shl      eax, 4
0x472A14: lea      eax, [eax + eax*2]
0x472A17: mov      edx, dword ptr [ebp - 4]
0x472A1A: add      eax, dword ptr [edx + 0x573f0]
0x472A20: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x472A26: mov      edx, dword ptr [edx]
0x472A28: add      eax, dword ptr [edx + 0xa0]
0x472A2E: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x472A34: mov      edx, dword ptr [edx]
0x472A36: add      eax, dword ptr [edx + 0x98]
0x472A3C: mov      dword ptr [ebp - 0x20], eax
0x472A3F: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x472A44: mov      eax, dword ptr [eax]
0x472A46: mov      eax, dword ptr [eax + 0xbc]
0x472A4C: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x472A52: mov      edx, dword ptr [edx]
0x472A54: sub      eax, dword ptr [edx + 0x2ae58]
0x472A5A: dec      eax
0x472A5B: shl      eax, 5
0x472A5E: mov      edx, dword ptr [ebp - 4]
0x472A61: add      eax, dword ptr [edx + 0x573f4]
0x472A67: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x472A6D: mov      edx, dword ptr [edx]
0x472A6F: add      eax, dword ptr [edx + 0xac]
0x472A75: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x472A7B: mov      edx, dword ptr [edx]
0x472A7D: add      eax, dword ptr [edx + 0x9c]
0x472A83: mov      dword ptr [ebp - 0x24], eax
0x472A86: mov      ecx, dword ptr [ebp - 0xc]
0x472A89: sub      ecx, dword ptr [ebp - 0x24]
0x472A8C: mov      edx, dword ptr [ebp - 8]
0x472A8F: sub      edx, dword ptr [ebp - 0x20]
0x472A92: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x472A97: mov      eax, dword ptr [eax]
0x472A99: call     0x497f20
0x472A9E: test     al, al
0x472AA0: je       0x472aa8
0x472AA2: mov      byte ptr [ebp - 0xd], 1
0x472AA6: jmp      0x472ab4
0x472AA8: dec      dword ptr [ebp - 0x14]
0x472AAB: inc      dword ptr [ebp - 0x28]
0x472AAE: jne      0x4729e2
0x472AB4: mov      al, byte ptr [ebp - 0xd]
0x472AB7: mov      esp, ebp
0x472AB9: pop      ebp
0x472ABA: ret      
0x472ABB: nop      
0x472ABC: push     ebp
0x472ABD: mov      ebp, esp
0x472ABF: add      esp, -0x44
0x472AC2: push     ebx
0x472AC3: xor      ebx, ebx
0x472AC5: mov      dword ptr [ebp - 0x44], ebx
0x472AC8: mov      dword ptr [ebp - 0xc], ecx
0x472ACB: mov      dword ptr [ebp - 8], edx
0x472ACE: mov      dword ptr [ebp - 4], eax
0x472AD1: xor      eax, eax
0x472AD3: push     ebp
0x472AD4: push     0x472c44
0x472AD9: push     dword ptr fs:[eax]
0x472ADC: mov      dword ptr fs:[eax], esp
0x472ADF: xor      eax, eax
0x472AE1: mov      dword ptr [ebp - 0x10], eax
0x472AE4: lea      eax, [ebp - 0x18]
0x472AE7: push     eax
0x472AE8: lea      eax, [ebp - 0x1c]
0x472AEB: push     eax
0x472AEC: mov      ecx, dword ptr [ebp - 0xc]
0x472AEF: mov      edx, dword ptr [ebp - 8]
0x472AF2: mov      eax, dword ptr [ebp - 4]
0x472AF5: call     0x472518
0x472AFA: lea      eax, [ebp - 0x20]
0x472AFD: push     eax
0x472AFE: lea      eax, [ebp - 0x24]
0x472B01: push     eax
0x472B02: mov      ecx, dword ptr [ebp - 0x1c]
0x472B05: mov      edx, dword ptr [ebp - 0x18]
0x472B08: mov      eax, dword ptr [ebp - 4]
0x472B0B: call     0x472494
0x472B10: mov      eax, dword ptr [ebp - 8]
0x472B13: sub      eax, dword ptr [ebp - 0x20]
0x472B16: mov      dword ptr [ebp - 0x28], eax
0x472B19: mov      eax, dword ptr [ebp - 0xc]
0x472B1C: sub      eax, dword ptr [ebp - 0x24]
0x472B1F: mov      dword ptr [ebp - 0x2c], eax
0x472B22: mov      eax, dword ptr [ebp + 8]
0x472B25: call     0x403a58
0x472B2A: mov      eax, dword ptr [0x4a4204] ; *0x4A4204->0x4F7DA4
0x472B2F: mov      eax, dword ptr [eax]
0x472B31: mov      eax, dword ptr [eax + 8]
0x472B34: dec      eax
0x472B35: test     eax, eax
0x472B37: jl       0x472c2e
0x472B3D: inc      eax
0x472B3E: mov      dword ptr [ebp - 0x3c], eax
0x472B41: mov      dword ptr [ebp - 0x14], 0
0x472B48: mov      eax, dword ptr [0x4a4204] ; *0x4A4204->0x4F7DA4
0x472B4D: mov      eax, dword ptr [eax]
0x472B4F: mov      edx, dword ptr [ebp - 0x14]
0x472B52: call     0x40f6a0
0x472B57: mov      dword ptr [ebp - 0x30], eax
0x472B5A: mov      eax, dword ptr [ebp - 0x30]
0x472B5D: movzx    eax, word ptr [eax + 4]
0x472B61: cmp      eax, dword ptr [ebp - 0x18]
0x472B64: jne      0x472c22
0x472B6A: mov      eax, dword ptr [ebp - 0x30]
0x472B6D: movzx    eax, word ptr [eax + 6]
0x472B71: cmp      eax, dword ptr [ebp - 0x1c]
0x472B74: jne      0x472c22
0x472B7A: mov      eax, dword ptr [ebp - 0x30]
0x472B7D: movzx    edx, word ptr [eax + 8]
0x472B81: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x472B86: mov      eax, dword ptr [eax]
0x472B88: mov      eax, dword ptr [eax + 0x330]
0x472B8E: call     0x45351c
0x472B93: mov      dword ptr [ebp - 0x34], eax
0x472B96: cmp      dword ptr [ebp - 0x34], 0
0x472B9A: je       0x472c22
0x472BA0: mov      eax, dword ptr [ebp - 0x34]
0x472BA3: call     0x44dc60
0x472BA8: sar      eax, 1
0x472BAA: jns      0x472baf
0x472BAC: adc      eax, 0
0x472BAF: mov      edx, dword ptr [ebp - 8]
0x472BB2: sub      edx, dword ptr [ebp - 0x20]
0x472BB5: add      eax, edx
0x472BB7: sub      eax, 3
0x472BBA: mov      dword ptr [ebp - 0x28], eax
0x472BBD: mov      eax, dword ptr [ebp - 0x34]
0x472BC0: call     0x44d944
0x472BC5: sar      eax, 1
0x472BC7: jns      0x472bcc
0x472BC9: adc      eax, 0
0x472BCC: mov      edx, dword ptr [ebp - 0xc]
0x472BCF: sub      edx, dword ptr [ebp - 0x24]
0x472BD2: add      eax, edx
0x472BD4: mov      dword ptr [ebp - 0x2c], eax
0x472BD7: mov      ecx, dword ptr [ebp - 0x2c]
0x472BDA: mov      edx, dword ptr [ebp - 0x28]
0x472BDD: mov      eax, dword ptr [ebp - 0x34]
0x472BE0: call     0x44da34
0x472BE5: mov      byte ptr [ebp - 0x35], al
0x472BE8: cmp      byte ptr [ebp - 0x35], 0
0x472BEC: je       0x472c22
0x472BEE: cmp      dword ptr [ebp - 0x10], 0
0x472BF2: jne      0x472bfa
0x472BF4: mov      eax, dword ptr [ebp - 0x30]
0x472BF7: mov      dword ptr [ebp - 0x10], eax
0x472BFA: mov      eax, dword ptr [ebp + 8]
0x472BFD: push     dword ptr [eax]
0x472BFF: lea      eax, [ebp - 0x44]
0x472C02: mov      edx, dword ptr [ebp - 0x30]
0x472C05: add      edx, 0x1c
0x472C08: call     0x403c78
0x472C0D: push     dword ptr [ebp - 0x44]
0x472C10: push     0x472c60 ; "\"
0x472C15: mov      eax, dword ptr [ebp + 8]
0x472C18: mov      edx, 3
0x472C1D: call     0x403d94
0x472C22: inc      dword ptr [ebp - 0x14]
0x472C25: dec      dword ptr [ebp - 0x3c]
0x472C28: jne      0x472b48
0x472C2E: xor      eax, eax
0x472C30: pop      edx
0x472C31: pop      ecx
0x472C32: pop      ecx
0x472C33: mov      dword ptr fs:[eax], edx
0x472C36: push     0x472c4b
0x472C3B: lea      eax, [ebp - 0x44]
0x472C3E: call     0x403a58
0x472C43: ret      
0x472C44: jmp      0x403518
0x472C49: jmp      0x472c3b
0x472C4B: mov      eax, dword ptr [ebp - 0x10]
0x472C4E: pop      ebx
0x472C4F: mov      esp, ebp
0x472C51: pop      ebp
0x472C52: ret      4
0x472C55: add      byte ptr [eax], al
0x472C57: add      bh, bh
0x472C5B: inc      dword ptr [ecx]
0x472C5D: add      byte ptr [eax], al
0x472C5F: add      byte ptr [eax + eax], bl
0x472C63: add      byte ptr [ebp - 0x75], dl
0x472C66: in       al, dx
0x472C67: add      esp, -0x1c
0x472C6A: mov      dword ptr [ebp - 0xc], ecx
0x472C6D: mov      dword ptr [ebp - 8], edx
0x472C70: mov      dword ptr [ebp - 4], eax
0x472C73: mov      eax, dword ptr [ebp + 8]
0x472C76: push     eax
0x472C77: mov      ecx, dword ptr [ebp + 0xc]
0x472C7A: mov      edx, dword ptr [ebp - 0xc]
0x472C7D: mov      eax, dword ptr [ebp - 8]
0x472C80: call     0x4a0104
0x472C85: and      eax, 0xff
0x472C8A: mov      dword ptr [ebp - 0x14], eax
0x472C8D: mov      eax, dword ptr [ebp - 8]
0x472C90: mov      dword ptr [ebp - 0x18], eax
0x472C93: mov      eax, dword ptr [ebp - 0xc]
0x472C96: mov      dword ptr [ebp - 0x1c], eax
0x472C99: mov      al, byte ptr [ebp - 0x14]
0x472C9C: lea      ecx, [ebp - 0x1c]
0x472C9F: lea      edx, [ebp - 0x18]
0x472CA2: call     0x49ff80
0x472CA7: mov      ecx, dword ptr [ebp - 0x1c]
0x472CAA: mov      edx, dword ptr [ebp - 0x18]
0x472CAD: mov      eax, dword ptr [ebp - 4]
0x472CB0: call     0x472ce0
0x472CB5: test     al, al
0x472CB7: je       0x472cd1
0x472CB9: mov      ecx, dword ptr [ebp + 8]
0x472CBC: mov      edx, dword ptr [ebp + 0xc]
0x472CBF: mov      eax, dword ptr [ebp - 4]
0x472CC2: call     0x472ce0
0x472CC7: test     al, al
0x472CC9: je       0x472cd1
0x472CCB: mov      byte ptr [ebp - 0xd], 1
0x472CCF: jmp      0x472cd5
0x472CD1: mov      byte ptr [ebp - 0xd], 0
0x472CD5: mov      al, byte ptr [ebp - 0xd]
0x472CD8: mov      esp, ebp
0x472CDA: pop      ebp
0x472CDB: ret      8
0x472CDE: mov      eax, eax
0x472CE0: push     ebp
0x472CE1: mov      ebp, esp
0x472CE3: add      esp, -0x10
0x472CE6: mov      dword ptr [ebp - 0xc], ecx
0x472CE9: mov      dword ptr [ebp - 8], edx
0x472CEC: mov      dword ptr [ebp - 4], eax
0x472CEF: mov      byte ptr [ebp - 0xd], 0
0x472CF3: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x472CF8: mov      eax, dword ptr [eax]
0x472CFA: mov      ecx, dword ptr [ebp - 0xc]
0x472CFD: mov      edx, dword ptr [ebp - 8]
0x472D00: call     0x49f120
0x472D05: test     al, al
0x472D07: je       0x472d1c
0x472D09: mov      ecx, dword ptr [ebp - 0xc]
0x472D0C: mov      edx, dword ptr [ebp - 8]
0x472D0F: mov      eax, dword ptr [ebp - 4]
0x472D12: call     0x472d24
0x472D17: xor      al, 1
0x472D19: mov      byte ptr [ebp - 0xd], al
0x472D1C: mov      al, byte ptr [ebp - 0xd]
0x472D1F: mov      esp, ebp
0x472D21: pop      ebp
0x472D22: ret      
0x472D23: nop      
0x472D24: push     ebp
0x472D25: mov      ebp, esp
0x472D27: add      esp, -0x1c
0x472D2A: mov      dword ptr [ebp - 0xc], ecx
0x472D2D: mov      dword ptr [ebp - 8], edx
0x472D30: mov      dword ptr [ebp - 4], eax
0x472D33: mov      byte ptr [ebp - 0xd], 0
0x472D37: mov      eax, dword ptr [ebp - 4]
0x472D3A: mov      eax, dword ptr [eax + 0x5a854]
0x472D40: mov      eax, dword ptr [eax + 8]
0x472D43: dec      eax
0x472D44: test     eax, eax
0x472D46: jl       0x472da8
0x472D48: inc      eax
0x472D49: mov      dword ptr [ebp - 0x1c], eax
0x472D4C: mov      dword ptr [ebp - 0x14], 0
0x472D53: mov      eax, dword ptr [ebp - 4]
0x472D56: mov      eax, dword ptr [eax + 0x5a854]
0x472D5C: mov      edx, dword ptr [ebp - 0x14]
0x472D5F: call     0x40f6a0
0x472D64: mov      dword ptr [ebp - 0x18], eax
0x472D67: mov      eax, dword ptr [ebp - 0x18]
0x472D6A: cmp      byte ptr [eax + 0x5d], 0
0x472D6E: je       0x472da0
0x472D70: mov      eax, dword ptr [ebp - 0x18]
0x472D73: cmp      byte ptr [eax + 0x5e], 0
0x472D77: je       0x472da0
0x472D79: mov      eax, dword ptr [ebp - 0x18]
0x472D7C: cmp      byte ptr [eax + 0x20], 0
0x472D80: jne      0x472da0
0x472D82: mov      eax, dword ptr [ebp - 0x18]
0x472D85: movzx    eax, word ptr [eax + 8]
0x472D89: cmp      eax, dword ptr [ebp - 8]
0x472D8C: jne      0x472da0
0x472D8E: mov      eax, dword ptr [ebp - 0x18]
0x472D91: movzx    eax, word ptr [eax + 0xa]
0x472D95: cmp      eax, dword ptr [ebp - 0xc]
0x472D98: jne      0x472da0
0x472D9A: mov      byte ptr [ebp - 0xd], 1
0x472D9E: jmp      0x472da8
0x472DA0: inc      dword ptr [ebp - 0x14]
0x472DA3: dec      dword ptr [ebp - 0x1c]
0x472DA6: jne      0x472d53
0x472DA8: mov      al, byte ptr [ebp - 0xd]
0x472DAB: mov      esp, ebp
0x472DAD: pop      ebp
0x472DAE: ret      
0x472DAF: nop      
0x472DB0: push     ebp
0x472DB1: mov      ebp, esp
0x472DB3: add      esp, -0x14
0x472DB6: mov      dword ptr [ebp - 8], edx
0x472DB9: mov      dword ptr [ebp - 4], eax
0x472DBC: xor      eax, eax
0x472DBE: mov      dword ptr [ebp - 0xc], eax
0x472DC1: mov      eax, dword ptr [ebp - 4]
0x472DC4: mov      eax, dword ptr [eax + 0x5a854]
0x472DCA: mov      eax, dword ptr [eax + 8]
0x472DCD: dec      eax
0x472DCE: test     eax, eax
0x472DD0: jl       0x472e14
0x472DD2: inc      eax
0x472DD3: mov      dword ptr [ebp - 0x14], eax
0x472DD6: mov      dword ptr [ebp - 0x10], 0
0x472DDD: mov      eax, dword ptr [ebp - 4]
0x472DE0: mov      eax, dword ptr [eax + 0x5a854]
0x472DE6: mov      edx, dword ptr [ebp - 0x10]
0x472DE9: call     0x40f6a0
0x472DEE: mov      eax, dword ptr [eax + 4]
0x472DF1: cmp      eax, dword ptr [ebp - 8]
0x472DF4: jne      0x472e0c
0x472DF6: mov      eax, dword ptr [ebp - 4]
0x472DF9: mov      eax, dword ptr [eax + 0x5a854]
0x472DFF: mov      edx, dword ptr [ebp - 0x10]
0x472E02: call     0x40f6a0
0x472E07: mov      dword ptr [ebp - 0xc], eax
0x472E0A: jmp      0x472e14
0x472E0C: inc      dword ptr [ebp - 0x10]
0x472E0F: dec      dword ptr [ebp - 0x14]
0x472E12: jne      0x472ddd
0x472E14: mov      eax, dword ptr [ebp - 0xc]
0x472E17: mov      esp, ebp
0x472E19: pop      ebp
0x472E1A: ret      
0x472E1B: nop      
0x472E1C: push     ebp
0x472E1D: mov      ebp, esp
0x472E1F: add      esp, -0x18
0x472E22: mov      dword ptr [ebp - 0xc], ecx
0x472E25: mov      dword ptr [ebp - 8], edx
0x472E28: mov      dword ptr [ebp - 4], eax
0x472E2B: xor      eax, eax
0x472E2D: mov      dword ptr [ebp - 0x10], eax
0x472E30: mov      eax, dword ptr [ebp - 4]
0x472E33: mov      eax, dword ptr [eax + 0x5a854]
0x472E39: mov      eax, dword ptr [eax + 8]
0x472E3C: dec      eax
0x472E3D: test     eax, eax
0x472E3F: jl       0x472eb7
0x472E41: inc      eax
0x472E42: mov      dword ptr [ebp - 0x18], eax
0x472E45: mov      dword ptr [ebp - 0x14], 0
0x472E4C: mov      edx, dword ptr [ebp - 0x14]
0x472E4F: mov      eax, dword ptr [ebp - 4]
0x472E52: mov      eax, dword ptr [eax + 0x5a854]
0x472E58: call     0x40f6a0
0x472E5D: movzx    eax, word ptr [eax + 8]
0x472E61: cmp      eax, dword ptr [ebp - 8]
0x472E64: jne      0x472eaf
0x472E66: mov      edx, dword ptr [ebp - 0x14]
0x472E69: mov      eax, dword ptr [ebp - 4]
0x472E6C: mov      eax, dword ptr [eax + 0x5a854]
0x472E72: call     0x40f6a0
0x472E77: movzx    eax, word ptr [eax + 0xa]
0x472E7B: cmp      eax, dword ptr [ebp - 0xc]
0x472E7E: jne      0x472eaf
0x472E80: mov      edx, dword ptr [ebp - 0x14]
0x472E83: mov      eax, dword ptr [ebp - 4]
0x472E86: mov      eax, dword ptr [eax + 0x5a854]
0x472E8C: call     0x40f6a0
0x472E91: mov      dword ptr [ebp - 0x10], eax
0x472E94: mov      eax, dword ptr [ebp - 0x10]
0x472E97: cmp      byte ptr [eax + 0x20], 0
0x472E9B: jne      0x472eaf
0x472E9D: mov      eax, dword ptr [ebp - 0x10]
0x472EA0: cmp      byte ptr [eax + 0x5d], 0
0x472EA4: je       0x472eaf
0x472EA6: mov      eax, dword ptr [ebp - 0x10]
0x472EA9: cmp      byte ptr [eax + 0x5e], 0
0x472EAD: jne      0x472eb7
0x472EAF: inc      dword ptr [ebp - 0x14]
0x472EB2: dec      dword ptr [ebp - 0x18]
0x472EB5: jne      0x472e4c
0x472EB7: mov      eax, dword ptr [ebp - 0x10]
0x472EBA: mov      esp, ebp
0x472EBC: pop      ebp
0x472EBD: ret      
0x472EBE: mov      eax, eax
0x472EC0: push     ebp
0x472EC1: mov      ebp, esp
0x472EC3: add      esp, -0x14
0x472EC6: mov      dword ptr [ebp - 8], edx
0x472EC9: mov      dword ptr [ebp - 4], eax
0x472ECC: mov      byte ptr [ebp - 9], 0
0x472ED0: mov      eax, dword ptr [ebp - 4]
0x472ED3: mov      eax, dword ptr [eax + 0x5a854]
0x472ED9: mov      eax, dword ptr [eax + 8]
0x472EDC: dec      eax
0x472EDD: test     eax, eax
0x472EDF: jl       0x472f10
0x472EE1: inc      eax
0x472EE2: mov      dword ptr [ebp - 0x14], eax
0x472EE5: mov      dword ptr [ebp - 0x10], 0
0x472EEC: mov      eax, dword ptr [ebp - 4]
0x472EEF: mov      eax, dword ptr [eax + 0x5a854]
0x472EF5: mov      edx, dword ptr [ebp - 0x10]
0x472EF8: call     0x40f6a0
0x472EFD: cmp      eax, dword ptr [ebp - 8]
0x472F00: jne      0x472f08
0x472F02: mov      byte ptr [ebp - 9], 1
0x472F06: jmp      0x472f10
0x472F08: inc      dword ptr [ebp - 0x10]
0x472F0B: dec      dword ptr [ebp - 0x14]
0x472F0E: jne      0x472eec
0x472F10: mov      al, byte ptr [ebp - 9]
0x472F13: mov      esp, ebp
0x472F15: pop      ebp
0x472F16: ret      
0x472F17: nop      
0x472F18: push     ebp
0x472F19: mov      ebp, esp
0x472F1B: add      esp, -0x20
0x472F1E: mov      word ptr [ebp - 0xa], cx
0x472F22: mov      dword ptr [ebp - 8], edx
0x472F25: mov      dword ptr [ebp - 4], eax
0x472F28: mov      eax, dword ptr [ebp - 4]
0x472F2B: mov      eax, dword ptr [eax + 0x5a854]
0x472F31: mov      eax, dword ptr [eax + 8]
0x472F34: dec      eax
0x472F35: test     eax, eax
0x472F37: jl       0x472f7e
0x472F39: inc      eax
0x472F3A: mov      dword ptr [ebp - 0x20], eax
0x472F3D: mov      dword ptr [ebp - 0x14], 0
0x472F44: mov      eax, dword ptr [ebp - 4]
0x472F47: mov      eax, dword ptr [eax + 0x5a854]
0x472F4D: mov      edx, dword ptr [ebp - 0x14]
0x472F50: call     0x40f6a0
0x472F55: mov      eax, dword ptr [eax + 4]
0x472F58: cmp      eax, dword ptr [ebp - 8]
0x472F5B: jne      0x472f76
0x472F5D: mov      eax, dword ptr [ebp - 4]
0x472F60: mov      eax, dword ptr [eax + 0x5a854]
0x472F66: mov      edx, dword ptr [ebp - 0x14]
0x472F69: call     0x40f6a0
0x472F6E: mov      dword ptr [ebp - 0x10], eax
0x472F71: jmp      0x473484
0x472F76: inc      dword ptr [ebp - 0x14]
0x472F79: dec      dword ptr [ebp - 0x20]
0x472F7C: jne      0x472f44
0x472F7E: mov      eax, dword ptr [ebp - 8]
0x472F81: call     0x4a0bfc
0x472F86: test     al, al
0x472F88: jne      0x473484
0x472F8E: mov      eax, dword ptr [ebp + 0xc]
0x472F91: call     0x458cac
0x472F96: and      eax, 0xff
0x472F9B: cmp      eax, 0x63
0x472F9E: ja       0x473364
0x472FA4: mov      al, byte ptr [eax + 0x472fb1]
0x472FAA: jmp      dword ptr [eax*4 + 0x473015]
0x472FB1: add      dword ptr [eax], eax
0x472FB3: add      byte ptr [eax], al
0x472FB5: add      byte ptr [eax], al
0x472FB7: add      byte ptr [eax], al
0x472FB9: add      byte ptr [edx], al
0x472FBB: add      byte ptr [eax], al
0x472FBD: add      byte ptr [ebx], al
0x472FBF: add      al, 5
0x472FC1: push     es
0x472FC2: pop      es
0x472FC3: or       byte ptr [ecx], cl
0x472FC5: or       cl, byte ptr [ebx]
0x472FC7: or       al, 0xd
0x472FC9: push     cs
0x472FCA: add      byte ptr [eax], al
0x472FCC: add      byte ptr [eax], al
0x472FCE: add      byte ptr [edi], cl
0x472FD0: adc      byte ptr [ecx], dl
0x472FD2: adc      dl, byte ptr [ebx]
0x472FD4: adc      al, 0x15
0x472FD6: push     ss
0x472FD7: add      byte ptr [eax], al
0x472FD9: pop      ss
0x472FDA: sbb      byte ptr [ecx], bl
0x472FDC: sbb      al, byte ptr [eax]
0x472FDE: sbb      eax, dword ptr [eax]
0x472FE0: sbb      al, 0x1d
0x472FE2: push     ds
0x472FE3: pop      ds
0x472FE4: add      byte ptr [eax], ah
0x472FE6: and      byte ptr [ecx], ah
0x472FE8: and      al, byte ptr [eax]
0x472FEA: add      byte ptr [eax], al
0x472FEC: add      byte ptr [eax], al
0x472FEE: add      byte ptr [eax], al
0x472FF0: add      byte ptr [eax], al
0x472FF2: add      byte ptr [eax], al
0x472FF4: add      byte ptr [eax], al
0x472FF6: add      byte ptr [eax], al
0x472FF8: add      byte ptr [eax], al
0x472FFA: add      byte ptr [eax], al
0x472FFC: add      byte ptr [eax], al
0x472FFE: add      byte ptr [eax], al
0x473000: add      byte ptr [eax], al
0x473002: add      byte ptr [eax], al
0x473004: add      byte ptr [eax], al
0x473006: add      byte ptr [eax], al
0x473008: add      byte ptr [eax], al
0x47300A: add      byte ptr [eax], al
0x47300C: add      byte ptr [eax], al
0x47300E: add      byte ptr [eax], al
0x473010: add      byte ptr [eax], al
0x473012: add      byte ptr [ebx], ah
0x473014: and      al, 0x64
0x473016: xor      eax, dword ptr [edi]
0x473019: test     eax, 0xbd004730
0x47301E: xor      byte ptr [edi], al
0x473021: sal      dword ptr [eax], 1
0x473023: inc      edi
0x473024: add      ch, ah
0x473026: xor      byte ptr [edi], al
0x473029: stc      
0x47302A: xor      byte ptr [edi], al
0x47302D: or       eax, 0x21004731
0x473032: xor      dword ptr [edi], eax
0x473035: xor      eax, 0x49004731
0x47303A: xor      dword ptr [edi], eax
0x47303D: pop      ebp
0x47303E: xor      dword ptr [edi], eax
0x473041: jno      0x473074
0x473043: inc      edi
0x473044: add      byte ptr [ebp - 0x66ffb8cf], al
0x47304A: xor      dword ptr [edi], eax
0x47304D: lodsd    eax, dword ptr [esi]
0x47304E: xor      dword ptr [edi], eax
0x473051: sal      dword ptr [ecx], 0x47
0x473054: add      ch, dl
0x473056: xor      dword ptr [edi], eax
0x473059: jmp      0xfd47778f
0x47305E: xor      dword ptr [edi], eax
0x473061: adc      dword ptr [edx], esi
0x473063: inc      edi
0x473064: add      byte ptr [0x39004732], ah
0x47306A: xor      al, byte ptr [edi]
0x47306D: dec      ebp
0x47306E: xor      al, byte ptr [edi]
0x473071: popal    
0x473072: xor      al, byte ptr [edi]
0x473075: jne      0x4730a9
0x473077: inc      edi
0x473078: add      byte ptr [ecx - 0x62ffb8ce], cl
0x47307E: xor      al, byte ptr [edi]
0x473081: mov      cl, 0x32
0x473083: inc      edi
0x473084: add      ch, al
0x473086: xor      al, byte ptr [edi]
0x473089: fnstenv  [edx]
0x47308B: inc      edi
0x47308C: add      ch, ch
0x47308E: xor      al, byte ptr [edi]
0x473092: xor      al, byte ptr [edi]
0x473095: rdpmc    
0x473097: inc      edi
0x473098: add      byte ptr [eax], ah
0x47309A: xor      eax, dword ptr [edi]
0x47309D: xor      dword ptr [ebx], esi
0x47309F: inc      edi
0x4730A0: add      byte ptr [edx + 0x33], al
0x4730A3: inc      edi
0x4730A4: add      byte ptr [ebx + 0x33], dl
0x4730A7: inc      edi
0x4730A8: add      byte ptr [edx + 0x5c18a101], dh
0x4730AE: dec      ecx
0x4730AF: add      al, ch
0x4730B1: xor      dword ptr [edx + eax], 0xffffff89
0x4730B6: inc      ebp
0x4730B7: call     0x49e7a5
0x4730BC: add      byte ptr [edx + 0x7d10a101], dh
0x4730C2: inc      edi
0x4730C3: add      al, ch
0x4730C5: pop      es
0x4730C6: xor      al, byte ptr [edx]
0x4730C8: add      byte ptr [ecx - 0x5d1617bb], cl
0x4730CE: add      al, byte ptr [eax]
0x4730D0: add      byte ptr [edx + 0x79f4a101], dh
0x4730D6: inc      edi
0x4730D7: add      al, ch
0x4730D9: mov      ecx, dword ptr [eax + eax]
0x4730DD: mov      dword ptr [ebp - 0x18], eax
0x4730E0: jmp      0x473373
0x4730E5: mov      dl, 1
0x4730E7: mov      eax, dword ptr [0x473db0]
0x4730EC: call     0x47459c ; TSkeletonOma.DMT_-4
0x4730F1: mov      dword ptr [ebp - 0x18], eax
0x4730F4: jmp      0x473373
0x4730F9: mov      dl, 1
0x4730FB: mov      eax, dword ptr [0x473e30]
0x473100: call     0x47459c ; TSkeletonOma.DMT_-4
0x473105: mov      dword ptr [ebp - 0x18], eax
0x473108: jmp      0x473373
0x47310D: mov      dl, 1
0x47310F: mov      eax, dword ptr [0x474124]
0x473114: call     0x475990 ; TGasKuDeGi.DMT_-4
0x473119: mov      dword ptr [ebp - 0x18], eax
0x47311C: jmp      0x473373
0x473121: mov      dl, 1
0x473123: mov      eax, dword ptr [0x473e90]
0x473128: call     0x47459c ; TSkeletonOma.DMT_-4
0x47312D: mov      dword ptr [ebp - 0x18], eax
0x473130: jmp      0x473373
0x473135: mov      dl, 1
0x473137: mov      eax, dword ptr [0x473fa8]
0x47313C: call     0x47459c ; TSkeletonOma.DMT_-4
0x473141: mov      dword ptr [ebp - 0x18], eax
0x473144: jmp      0x473373
0x473149: mov      dl, 1
0x47314B: mov      eax, dword ptr [0x473e90]
0x473150: call     0x47459c ; TSkeletonOma.DMT_-4
0x473155: mov      dword ptr [ebp - 0x18], eax
0x473158: jmp      0x473373
0x47315D: mov      dl, 1
0x47315F: mov      eax, dword ptr [0x4741a8]
0x473164: call     0x475990 ; TGasKuDeGi.DMT_-4
0x473169: mov      dword ptr [ebp - 0x18], eax
0x47316C: jmp      0x473373
0x473171: mov      dl, 1
0x473173: mov      eax, dword ptr [0x47420c]
0x473178: call     0x475990 ; TGasKuDeGi.DMT_-4
0x47317D: mov      dword ptr [ebp - 0x18], eax
0x473180: jmp      0x473373
0x473185: mov      dl, 1
0x473187: mov      eax, dword ptr [0x473e30]
0x47318C: call     0x47459c ; TSkeletonOma.DMT_-4
0x473191: mov      dword ptr [ebp - 0x18], eax
0x473194: jmp      0x473373
0x473199: mov      dl, 1
0x47319B: mov      eax, dword ptr [0x4740c8]
0x4731A0: call     0x47459c ; TSkeletonOma.DMT_-4
0x4731A5: mov      dword ptr [ebp - 0x18], eax
0x4731A8: jmp      0x473373
0x4731AD: mov      dl, 1
0x4731AF: mov      eax, dword ptr [0x4742cc]
0x4731B4: call     0x475990 ; TGasKuDeGi.DMT_-4
0x4731B9: mov      dword ptr [ebp - 0x18], eax
0x4731BC: jmp      0x473373
0x4731C1: mov      dl, 1
0x4731C3: mov      eax, dword ptr [0x473e90]
0x4731C8: call     0x47459c ; TSkeletonOma.DMT_-4
0x4731CD: mov      dword ptr [ebp - 0x18], eax
0x4731D0: jmp      0x473373
0x4731D5: mov      dl, 1
0x4731D7: mov      eax, dword ptr [0x473e90]
0x4731DC: call     0x47459c ; TSkeletonOma.DMT_-4
0x4731E1: mov      dword ptr [ebp - 0x18], eax
0x4731E4: jmp      0x473373
0x4731E9: mov      dl, 1
0x4731EB: mov      eax, dword ptr [0x473f4c]
0x4731F0: call     0x47459c ; TSkeletonOma.DMT_-4
0x4731F5: mov      dword ptr [ebp - 0x18], eax
0x4731F8: jmp      0x473373
0x4731FD: mov      dl, 1
0x4731FF: mov      eax, dword ptr [0x477ac8]
0x473204: call     0x477d68 ; TKillingHerb.DMT_-4
0x473209: mov      dword ptr [ebp - 0x18], eax
0x47320C: jmp      0x473373
0x473211: mov      dl, 1
0x473213: mov      eax, dword ptr [0x477b40]
0x473218: call     0x477d68 ; TKillingHerb.DMT_-4
0x47321D: mov      dword ptr [ebp - 0x18], eax
0x473220: jmp      0x473373
0x473225: mov      dl, 1
0x473227: mov      eax, dword ptr [0x477ba4]
0x47322C: call     0x477d68 ; TKillingHerb.DMT_-4
0x473231: mov      dword ptr [ebp - 0x18], eax
0x473234: jmp      0x473373
0x473239: mov      dl, 1
0x47323B: mov      eax, dword ptr [0x474328]
0x473240: call     0x475990 ; TGasKuDeGi.DMT_-4
0x473245: mov      dword ptr [ebp - 0x18], eax
0x473248: jmp      0x473373
0x47324D: mov      dl, 1
0x47324F: mov      eax, dword ptr [0x474394]
0x473254: call     0x47459c ; TSkeletonOma.DMT_-4
0x473259: mov      dword ptr [ebp - 0x18], eax
0x47325C: jmp      0x473373
0x473261: mov      dl, 1
0x473263: mov      eax, dword ptr [0x474270]
0x473268: call     0x475990 ; TGasKuDeGi.DMT_-4
0x47326D: mov      dword ptr [ebp - 0x18], eax
0x473270: jmp      0x473373
0x473275: mov      dl, 1
0x473277: mov      eax, dword ptr [0x474008]
0x47327C: call     0x47459c ; TSkeletonOma.DMT_-4
0x473281: mov      dword ptr [ebp - 0x18], eax
0x473284: jmp      0x473373
0x473289: mov      dl, 1
0x47328B: mov      eax, dword ptr [0x47406c]
0x473290: call     0x47459c ; TSkeletonOma.DMT_-4
0x473295: mov      dword ptr [ebp - 0x18], eax
0x473298: jmp      0x473373
0x47329D: mov      dl, 1
0x47329F: mov      eax, dword ptr [0x477a64]
0x4732A4: call     0x4962d0 ; TActor.DMT_-4
0x4732A9: mov      dword ptr [ebp - 0x18], eax
0x4732AC: jmp      0x473373
0x4732B1: mov      dl, 1
0x4732B3: mov      eax, dword ptr [0x473eec]
0x4732B8: call     0x47459c ; TSkeletonOma.DMT_-4
0x4732BD: mov      dword ptr [ebp - 0x18], eax
0x4732C0: jmp      0x473373
0x4732C5: mov      dl, 1
0x4732C7: mov      eax, dword ptr [0x4743f8]
0x4732CC: call     0x47459c ; TSkeletonOma.DMT_-4
0x4732D1: mov      dword ptr [ebp - 0x18], eax
0x4732D4: jmp      0x473373
0x4732D9: mov      dl, 1
0x4732DB: mov      eax, dword ptr [0x4743f8]
0x4732E0: call     0x47459c ; TSkeletonOma.DMT_-4
0x4732E5: mov      dword ptr [ebp - 0x18], eax
0x4732E8: jmp      0x473373
0x4732ED: mov      dl, 1
0x4732EF: mov      eax, dword ptr [0x474474]
0x4732F4: call     0x47459c ; TSkeletonOma.DMT_-4
0x4732F9: mov      dword ptr [ebp - 0x18], eax
0x4732FC: jmp      0x473373
0x4732FE: mov      dl, 1
0x473300: mov      eax, dword ptr [0x495ba8]
0x473305: call     0x4962d0 ; TActor.DMT_-4
0x47330A: mov      dword ptr [ebp - 0x18], eax
0x47330D: jmp      0x473373
0x47330F: mov      dl, 1
0x473311: mov      eax, dword ptr [0x474124]
0x473316: call     0x475990 ; TGasKuDeGi.DMT_-4
0x47331B: mov      dword ptr [ebp - 0x18], eax
0x47331E: jmp      0x473373
0x473320: mov      dl, 1
0x473322: mov      eax, dword ptr [0x4744d4]
0x473327: call     0x47459c ; TSkeletonOma.DMT_-4
0x47332C: mov      dword ptr [ebp - 0x18], eax
0x47332F: jmp      0x473373
0x473331: mov      dl, 1
0x473333: mov      eax, dword ptr [0x474534]
0x473338: call     0x47459c ; TSkeletonOma.DMT_-4
0x47333D: mov      dword ptr [ebp - 0x18], eax
0x473340: jmp      0x473373
0x473342: mov      dl, 1
0x473344: mov      eax, dword ptr [0x477c8c]
0x473349: call     0x479238 ; TWallStructure.DMT_-4
0x47334E: mov      dword ptr [ebp - 0x18], eax
0x473351: jmp      0x473373
0x473353: mov      dl, 1
0x473355: mov      eax, dword ptr [0x477c08]
0x47335A: call     0x4789a4 ; TCastleDoor.DMT_-4
0x47335F: mov      dword ptr [ebp - 0x18], eax
0x473362: jmp      0x473373
0x473364: mov      dl, 1
0x473366: mov      eax, dword ptr [0x495acc]
0x47336B: call     0x4962d0 ; TActor.DMT_-4
0x473370: mov      dword ptr [ebp - 0x18], eax
0x473373: mov      eax, dword ptr [ebp - 8]
0x473376: mov      edx, dword ptr [ebp - 0x18]
0x473379: mov      dword ptr [edx + 4], eax
0x47337C: mov      ax, word ptr [ebp - 0xa]
0x473380: mov      edx, dword ptr [ebp - 0x18]
0x473383: mov      word ptr [edx + 8], ax
0x473387: mov      ax, word ptr [ebp + 0x14]
0x47338B: mov      edx, dword ptr [ebp - 0x18]
0x47338E: mov      word ptr [edx + 0xa], ax
0x473392: mov      eax, dword ptr [ebp - 0x18]
0x473395: movzx    eax, word ptr [eax + 8]
0x473399: mov      edx, dword ptr [ebp - 0x18]
0x47339C: mov      dword ptr [edx + 0xb8], eax
0x4733A2: mov      eax, dword ptr [ebp - 0x18]
0x4733A5: movzx    eax, word ptr [eax + 0xa]
0x4733A9: mov      edx, dword ptr [ebp - 0x18]
0x4733AC: mov      dword ptr [edx + 0xbc], eax
0x4733B2: mov      al, byte ptr [ebp + 0x10]
0x4733B5: mov      edx, dword ptr [ebp - 0x18]
0x4733B8: mov      byte ptr [edx + 0xc], al
0x4733BB: mov      eax, dword ptr [ebp + 0xc]
0x4733BE: mov      edx, dword ptr [ebp - 0x18]
0x4733C1: mov      dword ptr [edx + 0x18], eax
0x4733C4: mov      eax, dword ptr [ebp + 0xc]
0x4733C7: call     0x458cac
0x4733CC: mov      edx, dword ptr [ebp - 0x18]
0x4733CF: mov      byte ptr [edx + 0xe], al
0x4733D2: mov      eax, dword ptr [ebp + 0xc]
0x4733D5: call     0x458ce0
0x4733DA: mov      edx, dword ptr [ebp - 0x18]
0x4733DD: mov      byte ptr [edx + 0xf], al
0x4733E0: mov      eax, dword ptr [ebp + 0xc]
0x4733E3: call     0x458cfc
0x4733E8: mov      edx, dword ptr [ebp - 0x18]
0x4733EB: mov      byte ptr [edx + 0x10], al
0x4733EE: mov      eax, dword ptr [ebp + 0xc]
0x4733F1: call     0x458cc4
0x4733F6: mov      edx, dword ptr [ebp - 0x18]
0x4733F9: mov      byte ptr [edx + 0x11], al
0x4733FC: mov      eax, dword ptr [ebp + 0xc]
0x4733FF: call     0x458d1c
0x473404: mov      edx, dword ptr [ebp - 0x18]
0x473407: mov      word ptr [edx + 0x14], ax
0x47340B: mov      eax, dword ptr [ebp - 0x18]
0x47340E: movzx    edx, word ptr [eax + 0x14]
0x473412: mov      eax, dword ptr [ebp - 0x18]
0x473415: movzx    eax, byte ptr [eax + 0xe]
0x473419: call     0x495ca8
0x47341E: mov      dword ptr [ebp - 0x1c], eax
0x473421: cmp      dword ptr [ebp - 0x1c], 0
0x473425: je       0x473437
0x473427: mov      eax, dword ptr [ebp - 0x1c]
0x47342A: movzx    eax, word ptr [eax + 0x10]
0x47342E: mov      edx, dword ptr [ebp - 0x18]
0x473431: mov      dword ptr [edx + 0x160], eax
0x473437: mov      eax, dword ptr [ebp - 0x18]
0x47343A: cmp      byte ptr [eax + 0xe], 0
0x47343E: jne      0x473452
0x473440: mov      eax, dword ptr [ebp - 0x18]
0x473443: movzx    eax, byte ptr [eax + 0x10]
0x473447: and      eax, 1
0x47344A: mov      edx, dword ptr [ebp - 0x18]
0x47344D: mov      byte ptr [edx + 0xd], al
0x473450: jmp      0x473459
0x473452: mov      eax, dword ptr [ebp - 0x18]
0x473455: mov      byte ptr [eax + 0xd], 0
0x473459: mov      eax, dword ptr [ebp + 8]
0x47345C: mov      edx, dword ptr [ebp - 0x18]
0x47345F: mov      dword ptr [edx + 0x1c], eax
0x473462: mov      eax, dword ptr [ebp - 0x18]
0x473465: add      eax, 0x60
0x473468: call     0x403a58
0x47346D: mov      eax, dword ptr [ebp - 4]
0x473470: mov      eax, dword ptr [eax + 0x5a854]
0x473476: mov      edx, dword ptr [ebp - 0x18]
0x473479: call     0x40f588
0x47347E: mov      eax, dword ptr [ebp - 0x18]
0x473481: mov      dword ptr [ebp - 0x10], eax
0x473484: mov      eax, dword ptr [ebp - 0x10]
0x473487: mov      esp, ebp
0x473489: pop      ebp
0x47348A: ret      0x10
0x47348D: lea      eax, [eax]
0x473490: push     ebp
0x473491: mov      ebp, esp
0x473493: add      esp, -0x14
0x473496: mov      dword ptr [ebp - 8], edx
0x473499: mov      dword ptr [ebp - 4], eax
0x47349C: mov      eax, dword ptr [ebp - 4]
0x47349F: mov      eax, dword ptr [eax + 0x5a854]
0x4734A5: mov      eax, dword ptr [eax + 8]
0x4734A8: dec      eax
0x4734A9: test     eax, eax
0x4734AB: jl       0x4734e9
0x4734AD: inc      eax
0x4734AE: mov      dword ptr [ebp - 0x14], eax
0x4734B1: mov      dword ptr [ebp - 0xc], 0
0x4734B8: mov      eax, dword ptr [ebp - 4]
0x4734BB: mov      eax, dword ptr [eax + 0x5a854]
0x4734C1: mov      edx, dword ptr [ebp - 0xc]
0x4734C4: call     0x40f6a0
0x4734C9: cmp      eax, dword ptr [ebp - 8]
0x4734CC: jne      0x4734e1
0x4734CE: mov      eax, dword ptr [ebp - 4]
0x4734D1: mov      eax, dword ptr [eax + 0x5a854]
0x4734D7: mov      edx, dword ptr [ebp - 0xc]
0x4734DA: call     0x40f5c4
0x4734DF: jmp      0x4734e9
0x4734E1: inc      dword ptr [ebp - 0xc]
0x4734E4: dec      dword ptr [ebp - 0x14]
0x4734E7: jne      0x4734b8
0x4734E9: mov      byte ptr [ebp - 0xd], 0
0x4734ED: mov      eax, dword ptr [ebp - 4]
0x4734F0: mov      eax, dword ptr [eax + 0x5a854]
0x4734F6: mov      eax, dword ptr [eax + 8]
0x4734F9: dec      eax
0x4734FA: test     eax, eax
0x4734FC: jl       0x473542
0x4734FE: inc      eax
0x4734FF: mov      dword ptr [ebp - 0x14], eax
0x473502: mov      dword ptr [ebp - 0xc], 0
0x473509: mov      eax, dword ptr [ebp - 4]
0x47350C: mov      eax, dword ptr [eax + 0x5a854]
0x473512: mov      edx, dword ptr [ebp - 0xc]
0x473515: call     0x40f6a0
0x47351A: cmp      byte ptr [eax + 0x20], 0
0x47351E: jne      0x47353a
0x473520: mov      eax, dword ptr [ebp - 4]
0x473523: mov      eax, dword ptr [eax + 0x5a854]
0x473529: mov      ecx, dword ptr [ebp - 8]
0x47352C: mov      edx, dword ptr [ebp - 0xc]
0x47352F: call     0x40f758
0x473534: mov      byte ptr [ebp - 0xd], 1
0x473538: jmp      0x473542
0x47353A: inc      dword ptr [ebp - 0xc]
0x47353D: dec      dword ptr [ebp - 0x14]
0x473540: jne      0x473509
0x473542: cmp      byte ptr [ebp - 0xd], 0
0x473546: jne      0x473559
0x473548: mov      eax, dword ptr [ebp - 4]
0x47354B: mov      eax, dword ptr [eax + 0x5a854]
0x473551: mov      edx, dword ptr [ebp - 8]
0x473554: call     0x40f588
0x473559: mov      esp, ebp
0x47355B: pop      ebp
0x47355C: ret      
0x47355D: lea      eax, [eax]
0x473560: push     ebp
0x473561: mov      ebp, esp
0x473563: add      esp, -0x14
0x473566: mov      dword ptr [ebp - 0xc], ecx
0x473569: mov      dword ptr [ebp - 8], edx
0x47356C: mov      dword ptr [ebp - 4], eax
0x47356F: cmp      dword ptr [ebp - 0xc], 0
0x473573: jne      0x4735d5
0x473575: mov      eax, dword ptr [ebp - 4]
0x473578: mov      eax, dword ptr [eax + 0x5a854]
0x47357E: mov      eax, dword ptr [eax + 8]
0x473581: dec      eax
0x473582: test     eax, eax
0x473584: jl       0x4735d5
0x473586: inc      eax
0x473587: mov      dword ptr [ebp - 0x14], eax
0x47358A: mov      dword ptr [ebp - 0x10], 0
0x473591: mov      edx, dword ptr [ebp - 0x10]
0x473594: mov      eax, dword ptr [ebp - 4]
0x473597: mov      eax, dword ptr [eax + 0x5a854]
0x47359D: call     0x40f6a0
0x4735A2: cmp      eax, dword ptr [ebp - 8]
0x4735A5: jne      0x4735cd
0x4735A7: mov      edx, dword ptr [ebp - 0x10]
0x4735AA: mov      eax, dword ptr [ebp - 4]
0x4735AD: mov      eax, dword ptr [eax + 0x5a854]
0x4735B3: call     0x40f5c4
0x4735B8: mov      ecx, dword ptr [ebp - 8]
0x4735BB: xor      edx, edx
0x4735BD: mov      eax, dword ptr [ebp - 4]
0x4735C0: mov      eax, dword ptr [eax + 0x5a854]
0x4735C6: call     0x40f758
0x4735CB: jmp      0x4735d5
0x4735CD: inc      dword ptr [ebp - 0x10]
0x4735D0: dec      dword ptr [ebp - 0x14]
0x4735D3: jne      0x473591
0x4735D5: mov      esp, ebp
0x4735D7: pop      ebp
0x4735D8: ret      
0x4735D9: lea      eax, [eax]
0x4735DC: push     ebp
0x4735DD: mov      ebp, esp
0x4735DF: add      esp, -0xc
0x4735E2: push     ebx
0x4735E3: mov      dword ptr [ebp - 4], eax
0x4735E6: mov      eax, dword ptr [ebp - 4]
0x4735E9: mov      eax, dword ptr [eax + 0x5a854]
0x4735EF: mov      eax, dword ptr [eax + 8]
0x4735F2: dec      eax
0x4735F3: test     eax, eax
0x4735F5: jl       0x473620
0x4735F7: inc      eax
0x4735F8: mov      dword ptr [ebp - 0xc], eax
0x4735FB: mov      dword ptr [ebp - 8], 0
0x473602: mov      eax, dword ptr [ebp - 4]
0x473605: mov      eax, dword ptr [eax + 0x5a854]
0x47360B: mov      edx, dword ptr [ebp - 8]
0x47360E: call     0x40f6a0
0x473613: call     0x402eac
0x473618: inc      dword ptr [ebp - 8]
0x47361B: dec      dword ptr [ebp - 0xc]
0x47361E: jne      0x473602
0x473620: mov      eax, dword ptr [ebp - 4]
0x473623: mov      eax, dword ptr [eax + 0x5a854]
0x473629: mov      bx, 0xffff
0x47362D: call     0x403080
0x473632: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x473637: xor      edx, edx
0x473639: mov      dword ptr [eax], edx
0x47363B: mov      eax, dword ptr [0x4a42c0] ; *0x4A42C0->0x4F94A4
0x473640: xor      edx, edx
0x473642: mov      dword ptr [eax], edx
0x473644: mov      eax, dword ptr [0x4a3f10] ; *0x4A3F10->0x4F94A8
0x473649: xor      edx, edx
0x47364B: mov      dword ptr [eax], edx
0x47364D: mov      eax, dword ptr [0x4a414c] ; *0x4A414C->0x4F94AC
0x473652: xor      edx, edx
0x473654: mov      dword ptr [eax], edx
0x473656: mov      eax, dword ptr [ebp - 4]
0x473659: mov      eax, dword ptr [eax + 0x5a860]
0x47365F: mov      eax, dword ptr [eax + 8]
0x473662: dec      eax
0x473663: test     eax, eax
0x473665: jl       0x473690
0x473667: inc      eax
0x473668: mov      dword ptr [ebp - 0xc], eax
0x47366B: mov      dword ptr [ebp - 8], 0
0x473672: mov      eax, dword ptr [ebp - 4]
0x473675: mov      eax, dword ptr [eax + 0x5a860]
0x47367B: mov      edx, dword ptr [ebp - 8]
0x47367E: call     0x40f6a0
0x473683: call     0x402eac
0x473688: inc      dword ptr [ebp - 8]
0x47368B: dec      dword ptr [ebp - 0xc]
0x47368E: jne      0x473672
0x473690: mov      eax, dword ptr [ebp - 4]
0x473693: mov      eax, dword ptr [eax + 0x5a860]
0x473699: mov      bx, 0xffff
0x47369D: call     0x403080
0x4736A2: pop      ebx
0x4736A3: mov      esp, ebp
0x4736A5: pop      ebp
0x4736A6: ret      
0x4736A7: nop      
0x4736A8: push     ebp
0x4736A9: mov      ebp, esp
0x4736AB: add      esp, -0x10
0x4736AE: mov      dword ptr [ebp - 8], edx
0x4736B1: mov      dword ptr [ebp - 4], eax
0x4736B4: xor      eax, eax
0x4736B6: mov      dword ptr [ebp - 0xc], eax
0x4736B9: xor      eax, eax
0x4736BB: mov      dword ptr [ebp - 0x10], eax
0x4736BE: mov      eax, dword ptr [ebp - 4]
0x4736C1: mov      eax, dword ptr [eax + 0x5a854]
0x4736C7: mov      eax, dword ptr [eax + 8]
0x4736CA: cmp      eax, dword ptr [ebp - 0x10]
0x4736CD: jle      0x4737b8
0x4736D3: mov      edx, dword ptr [ebp - 0x10]
0x4736D6: mov      eax, dword ptr [ebp - 4]
0x4736D9: mov      eax, dword ptr [eax + 0x5a854]
0x4736DF: call     0x40f6a0
0x4736E4: mov      eax, dword ptr [eax + 4]
0x4736E7: cmp      eax, dword ptr [ebp - 8]
0x4736EA: jne      0x4737b0
0x4736F0: mov      edx, dword ptr [ebp - 0x10]
0x4736F3: mov      eax, dword ptr [ebp - 4]
0x4736F6: mov      eax, dword ptr [eax + 0x5a854]
0x4736FC: call     0x40f6a0
0x473701: mov      edx, dword ptr [0x4a42c0] ; *0x4A42C0->0x4F94A4
0x473707: cmp      eax, dword ptr [edx]
0x473709: jne      0x473714
0x47370B: mov      eax, dword ptr [0x4a42c0] ; *0x4A42C0->0x4F94A4
0x473710: xor      edx, edx
0x473712: mov      dword ptr [eax], edx
0x473714: mov      edx, dword ptr [ebp - 0x10]
0x473717: mov      eax, dword ptr [ebp - 4]
0x47371A: mov      eax, dword ptr [eax + 0x5a854]
0x473720: call     0x40f6a0
0x473725: mov      edx, dword ptr [0x4a3f10] ; *0x4A3F10->0x4F94A8
0x47372B: cmp      eax, dword ptr [edx]
0x47372D: jne      0x473738
0x47372F: mov      eax, dword ptr [0x4a3f10] ; *0x4A3F10->0x4F94A8
0x473734: xor      edx, edx
0x473736: mov      dword ptr [eax], edx
0x473738: mov      edx, dword ptr [ebp - 0x10]
0x47373B: mov      eax, dword ptr [ebp - 4]
0x47373E: mov      eax, dword ptr [eax + 0x5a854]
0x473744: call     0x40f6a0
0x473749: mov      edx, dword ptr [0x4a414c] ; *0x4A414C->0x4F94AC
0x47374F: cmp      eax, dword ptr [edx]
0x473751: jne      0x47375c
0x473753: mov      eax, dword ptr [0x4a414c] ; *0x4A414C->0x4F94AC
0x473758: xor      edx, edx
0x47375A: mov      dword ptr [eax], edx
0x47375C: mov      edx, dword ptr [ebp - 0x10]
0x47375F: mov      eax, dword ptr [ebp - 4]
0x473762: mov      eax, dword ptr [eax + 0x5a854]
0x473768: call     0x40f6a0
0x47376D: add      eax, 0x170
0x473772: push     eax
0x473773: call     0x406154
0x473778: pop      edx
0x473779: mov      dword ptr [edx], eax
0x47377B: mov      edx, dword ptr [ebp - 0x10]
0x47377E: mov      eax, dword ptr [ebp - 4]
0x473781: mov      eax, dword ptr [eax + 0x5a854]
0x473787: call     0x40f6a0
0x47378C: mov      edx, eax
0x47378E: mov      eax, dword ptr [0x4a4078] ; *0x4A4078->0x4F9230
0x473793: mov      eax, dword ptr [eax]
0x473795: call     0x40f588
0x47379A: mov      edx, dword ptr [ebp - 0x10]
0x47379D: mov      eax, dword ptr [ebp - 4]
0x4737A0: mov      eax, dword ptr [eax + 0x5a854]
0x4737A6: call     0x40f5c4
0x4737AB: jmp      0x4736be
0x4737B0: inc      dword ptr [ebp - 0x10]
0x4737B3: jmp      0x4736be
0x4737B8: mov      eax, dword ptr [ebp - 0xc]
0x4737BB: mov      esp, ebp
0x4737BD: pop      ebp
0x4737BE: ret      
0x4737BF: nop      
0x4737C0: push     ebp
0x4737C1: mov      ebp, esp
0x4737C3: add      esp, -0x10
0x4737C6: mov      dword ptr [ebp - 8], edx
0x4737C9: mov      dword ptr [ebp - 4], eax
0x4737CC: mov      eax, dword ptr [ebp - 4]
0x4737CF: mov      eax, dword ptr [eax + 0x5a854]
0x4737D5: mov      eax, dword ptr [eax + 8]
0x4737D8: dec      eax
0x4737D9: test     eax, eax
0x4737DB: jl       0x473857
0x4737DD: inc      eax
0x4737DE: mov      dword ptr [ebp - 0x10], eax
0x4737E1: mov      dword ptr [ebp - 0xc], 0
0x4737E8: mov      edx, dword ptr [ebp - 0xc]
0x4737EB: mov      eax, dword ptr [ebp - 4]
0x4737EE: mov      eax, dword ptr [eax + 0x5a854]
0x4737F4: call     0x40f6a0
0x4737F9: cmp      eax, dword ptr [ebp - 8]
0x4737FC: jne      0x47384f
0x4737FE: mov      edx, dword ptr [ebp - 0xc]
0x473801: mov      eax, dword ptr [ebp - 4]
0x473804: mov      eax, dword ptr [eax + 0x5a854]
0x47380A: call     0x40f6a0
0x47380F: add      eax, 0x170
0x473814: push     eax
0x473815: call     0x406154
0x47381A: pop      edx
0x47381B: mov      dword ptr [edx], eax
0x47381D: mov      edx, dword ptr [ebp - 0xc]
0x473820: mov      eax, dword ptr [ebp - 4]
0x473823: mov      eax, dword ptr [eax + 0x5a854]
0x473829: call     0x40f6a0
0x47382E: mov      edx, eax
0x473830: mov      eax, dword ptr [0x4a4078] ; *0x4A4078->0x4F9230
0x473835: mov      eax, dword ptr [eax]
0x473837: call     0x40f588
0x47383C: mov      edx, dword ptr [ebp - 0xc]
0x47383F: mov      eax, dword ptr [ebp - 4]
0x473842: mov      eax, dword ptr [eax + 0x5a854]
0x473848: call     0x40f5c4
0x47384D: jmp      0x473857
0x47384F: inc      dword ptr [ebp - 0xc]
0x473852: dec      dword ptr [ebp - 0x10]
0x473855: jne      0x4737e8
0x473857: mov      esp, ebp
0x473859: pop      ebp
0x47385A: ret      
0x47385B: nop      
0x47385C: push     ebp
0x47385D: mov      ebp, esp
0x47385F: add      esp, -0x1c
0x473862: mov      dword ptr [ebp - 0xc], ecx
0x473865: mov      dword ptr [ebp - 8], edx
0x473868: mov      dword ptr [ebp - 4], eax
0x47386B: xor      eax, eax
0x47386D: mov      dword ptr [ebp - 0x10], eax
0x473870: mov      eax, dword ptr [ebp - 4]
0x473873: mov      eax, dword ptr [eax + 0x5a854]
0x473879: mov      eax, dword ptr [eax + 8]
0x47387C: dec      eax
0x47387D: test     eax, eax
0x47387F: jl       0x4738e6
0x473881: inc      eax
0x473882: mov      dword ptr [ebp - 0x1c], eax
0x473885: mov      dword ptr [ebp - 0x14], 0
0x47388C: mov      eax, dword ptr [ebp - 4]
0x47388F: mov      eax, dword ptr [eax + 0x5a854]
0x473895: mov      edx, dword ptr [ebp - 0x14]
0x473898: call     0x40f6a0
0x47389D: mov      dword ptr [ebp - 0x18], eax
0x4738A0: mov      eax, dword ptr [ebp - 0x18]
0x4738A3: cmp      byte ptr [eax + 0x20], 0
0x4738A7: je       0x4738de
0x4738A9: mov      eax, dword ptr [ebp - 0x18]
0x4738AC: cmp      byte ptr [eax + 0xe], 0
0x4738B0: je       0x4738de
0x4738B2: mov      eax, dword ptr [ebp - 0x18]
0x4738B5: movzx    eax, word ptr [eax + 8]
0x4738B9: sub      eax, dword ptr [ebp - 8]
0x4738BC: cdq      
0x4738BD: xor      eax, edx
0x4738BF: sub      eax, edx
0x4738C1: dec      eax
0x4738C2: jg       0x4738de
0x4738C4: mov      eax, dword ptr [ebp - 0x18]
0x4738C7: movzx    eax, word ptr [eax + 0xa]
0x4738CB: sub      eax, dword ptr [ebp - 0xc]
0x4738CE: cdq      
0x4738CF: xor      eax, edx
0x4738D1: sub      eax, edx
0x4738D3: dec      eax
0x4738D4: jg       0x4738de
0x4738D6: mov      eax, dword ptr [ebp - 0x18]
0x4738D9: mov      dword ptr [ebp - 0x10], eax
0x4738DC: jmp      0x4738e6
0x4738DE: inc      dword ptr [ebp - 0x14]
0x4738E1: dec      dword ptr [ebp - 0x1c]
0x4738E4: jne      0x47388c
0x4738E6: mov      eax, dword ptr [ebp - 0x10]
0x4738E9: mov      esp, ebp
0x4738EB: pop      ebp
0x4738EC: ret      
0x4738ED: lea      eax, [eax]
0x4738F0: push     ebp
0x4738F1: mov      ebp, esp
0x4738F3: add      esp, -0x10
0x4738F6: push     ebx
0x4738F7: mov      dword ptr [ebp - 0xc], ecx
0x4738FA: mov      dword ptr [ebp - 8], edx
0x4738FD: mov      dword ptr [ebp - 4], eax
0x473900: mov      eax, dword ptr [ebp + 8]
0x473903: call     0x403e88
0x473908: xor      eax, eax
0x47390A: push     ebp
0x47390B: push     0x473d43
0x473910: push     dword ptr fs:[eax]
0x473913: mov      dword ptr fs:[eax], esp
0x473916: mov      eax, dword ptr [ebp - 8]
0x473919: cmp      eax, 0x32
0x47391C: jg       0x473935
0x47391E: je       0x473a8d
0x473924: dec      eax
0x473925: je       0x473946
0x473927: sub      eax, 0x1c
0x47392A: je       0x473b2a
0x473930: jmp      0x473b6b
0x473935: sub      eax, 0x33
0x473938: je       0x47399c
0x47393A: sub      eax, 0x247
0x47393F: je       0x47399c
0x473941: jmp      0x473b6b
0x473946: push     0xd6
0x47394B: push     0
0x47394D: push     0
0x47394F: push     0
0x473951: mov      cx, 0xfe
0x473955: mov      edx, 0x6f
0x47395A: mov      eax, dword ptr [ebp - 4]
0x47395D: call     0x472f18
0x473962: mov      dword ptr [ebp - 0x10], eax
0x473965: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x47396A: mov      edx, dword ptr [ebp - 0x10]
0x47396D: mov      dword ptr [eax], edx
0x47396F: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x473974: mov      eax, dword ptr [eax]
0x473976: movzx    eax, word ptr [eax + 0xa]
0x47397A: push     eax
0x47397B: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x473980: mov      eax, dword ptr [eax]
0x473982: movzx    ecx, word ptr [eax + 8]
0x473986: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x47398B: mov      eax, dword ptr [eax]
0x47398D: mov      edx, 0x473d5c ; "0"
0x473992: call     0x49eff8
0x473997: jmp      0x473d2d
0x47399C: mov      eax, dword ptr [ebp + 0x1c]
0x47399F: push     eax
0x4739A0: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x4739A5: mov      eax, dword ptr [eax]
0x4739A7: mov      ecx, dword ptr [ebp + 0x20]
0x4739AA: mov      edx, dword ptr [ebp + 8]
0x4739AD: call     0x49eff8
0x4739B2: mov      eax, dword ptr [0x4a4144] ; *0x4A4144->0x4A7294
0x4739B7: mov      edx, dword ptr [ebp + 0x18]
0x4739BA: mov      dword ptr [eax], edx
0x4739BC: mov      eax, dword ptr [0x4a3f48] ; *0x4A3F48->0x4F9594
0x4739C1: mov      edx, dword ptr [ebp + 0x18]
0x4739C4: mov      dword ptr [eax], edx
0x4739C6: mov      eax, dword ptr [0x4a3ed4] ; *0x4A3ED4->0x4F959C
0x4739CB: mov      eax, dword ptr [eax]
0x4739CD: mov      edx, dword ptr [ebp + 0x18]
0x4739D0: mov      dword ptr [eax], edx
0x4739D2: mov      eax, dword ptr [0x4a4144] ; *0x4A4144->0x4A7294
0x4739D7: cmp      dword ptr [eax], 0
0x4739DA: jne      0x4739e6
0x4739DC: mov      eax, dword ptr [0x4a3f1c] ; *0x4A3F1C->0x4F9382
0x4739E1: mov      byte ptr [eax], 0
0x4739E4: jmp      0x4739ee
0x4739E6: mov      eax, dword ptr [0x4a3f1c] ; *0x4A3F1C->0x4F9382
0x4739EB: mov      byte ptr [eax], 1
0x4739EE: cmp      dword ptr [ebp - 8], 0x33
0x4739F2: jne      0x473a4c
0x4739F4: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4739F9: cmp      dword ptr [eax], 0
0x4739FC: je       0x473a4c
0x4739FE: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x473A03: mov      eax, dword ptr [eax]
0x473A05: mov      dx, word ptr [ebp + 0x20]
0x473A09: mov      word ptr [eax + 8], dx
0x473A0D: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x473A12: mov      eax, dword ptr [eax]
0x473A14: mov      dx, word ptr [ebp + 0x1c]
0x473A18: mov      word ptr [eax + 0xa], dx
0x473A1C: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x473A21: mov      eax, dword ptr [eax]
0x473A23: mov      edx, dword ptr [ebp + 0x20]
0x473A26: mov      dword ptr [eax + 0xb8], edx
0x473A2C: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x473A31: mov      eax, dword ptr [eax]
0x473A33: mov      edx, dword ptr [ebp + 0x1c]
0x473A36: mov      dword ptr [eax + 0xbc], edx
0x473A3C: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x473A42: mov      edx, dword ptr [edx]
0x473A44: mov      eax, dword ptr [ebp - 4]
0x473A47: call     0x4737c0
0x473A4C: mov      eax, dword ptr [0x4a42a0] ; *0x4A42A0->0x4F947C
0x473A51: cmp      byte ptr [eax], 0
0x473A54: je       0x473d2d
0x473A5A: mov      eax, dword ptr [0x4a4084] ; *0x4A4084->0x4F9480
0x473A5F: cmp      dword ptr [eax], 0
0x473A62: jle      0x473a73
0x473A64: mov      eax, dword ptr [0x4a4084] ; *0x4A4084->0x4F9480
0x473A69: mov      eax, dword ptr [eax]
0x473A6B: mov      edx, dword ptr [0x4a4058] ; *0x4A4058->0x4F9484
0x473A71: mov      dword ptr [edx], eax
0x473A73: mov      eax, dword ptr [0x4a4084] ; *0x4A4084->0x4F9480
0x473A78: xor      edx, edx
0x473A7A: mov      dword ptr [eax], edx
0x473A7C: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x473A81: mov      eax, dword ptr [eax]
0x473A83: call     0x4665e0
0x473A88: jmp      0x473d2d
0x473A8D: mov      edx, dword ptr [ebp - 0xc]
0x473A90: mov      eax, dword ptr [ebp - 4]
0x473A93: call     0x472db0
0x473A98: mov      dword ptr [ebp - 0x10], eax
0x473A9B: cmp      dword ptr [ebp - 0x10], 0
0x473A9F: jne      0x473b08
0x473AA1: mov      ax, word ptr [ebp + 0x1c]
0x473AA5: push     eax
0x473AA6: xor      eax, eax
0x473AA8: mov      al, byte ptr [ebp + 0x18]
0x473AAB: push     eax
0x473AAC: mov      eax, dword ptr [ebp + 0x14]
0x473AAF: push     eax
0x473AB0: mov      eax, dword ptr [ebp + 0x10]
0x473AB3: push     eax
0x473AB4: mov      cx, word ptr [ebp + 0x20]
0x473AB8: mov      edx, dword ptr [ebp - 0xc]
0x473ABB: mov      eax, dword ptr [ebp - 4]
0x473ABE: call     0x472f18
0x473AC3: mov      dword ptr [ebp - 0x10], eax
0x473AC6: mov      ax, word ptr [ebp + 0x18]
0x473ACA: call     0x406974
0x473ACF: and      eax, 0xff
0x473AD4: mov      edx, dword ptr [ebp - 0x10]
0x473AD7: mov      dword ptr [edx + 0x154], eax
0x473ADD: xor      eax, eax
0x473ADF: mov      al, byte ptr [ebp + 0x18]
0x473AE2: mov      dword ptr [ebp + 0x18], eax
0x473AE5: mov      eax, dword ptr [ebp + 0x1c]
0x473AE8: push     eax
0x473AE9: mov      eax, dword ptr [ebp + 0x18]
0x473AEC: push     eax
0x473AED: mov      eax, dword ptr [ebp + 0x14]
0x473AF0: push     eax
0x473AF1: mov      eax, dword ptr [ebp + 0x10]
0x473AF4: push     eax
0x473AF5: push     0
0x473AF7: push     0
0x473AF9: mov      ecx, dword ptr [ebp + 0x20]
0x473AFC: mov      dx, 0xa
0x473B00: mov      eax, dword ptr [ebp - 0x10]
0x473B03: call     0x496548
0x473B08: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x473B0D: cmp      dword ptr [eax], 0
0x473B10: je       0x473b1b
0x473B12: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x473B17: xor      edx, edx
0x473B19: mov      dword ptr [eax], edx
0x473B1B: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x473B20: mov      edx, dword ptr [ebp - 0x10]
0x473B23: mov      dword ptr [eax], edx
0x473B25: jmp      0x473d2d
0x473B2A: mov      edx, dword ptr [ebp - 0xc]
0x473B2D: mov      eax, dword ptr [ebp - 4]
0x473B30: call     0x472db0
0x473B35: mov      dword ptr [ebp - 0x10], eax
0x473B38: cmp      dword ptr [ebp - 0x10], 0
0x473B3C: je       0x473b5b
0x473B3E: mov      eax, dword ptr [ebp - 0x10]
0x473B41: cmp      byte ptr [eax + 0x23], 0
0x473B45: jne      0x473d2d
0x473B4B: mov      eax, dword ptr [ebp - 0x10]
0x473B4E: cmp      dword ptr [eax + 0xf8], 0
0x473B55: jne      0x473d2d
0x473B5B: mov      edx, dword ptr [ebp - 0xc]
0x473B5E: mov      eax, dword ptr [ebp - 4]
0x473B61: call     0x4736a8
0x473B66: jmp      0x473d2d
0x473B6B: mov      edx, dword ptr [ebp - 0xc]
0x473B6E: mov      eax, dword ptr [ebp - 4]
0x473B71: call     0x472db0
0x473B76: mov      dword ptr [ebp - 0x10], eax
0x473B79: cmp      dword ptr [ebp - 8], 0xa
0x473B7D: je       0x473ba9
0x473B7F: cmp      dword ptr [ebp - 8], 0xd
0x473B83: je       0x473ba9
0x473B85: cmp      dword ptr [ebp - 8], 0xb
0x473B89: je       0x473ba9
0x473B8B: cmp      dword ptr [ebp - 8], 9
0x473B8F: je       0x473ba9
0x473B91: cmp      dword ptr [ebp - 8], 0x20
0x473B95: je       0x473ba9
0x473B97: cmp      dword ptr [ebp - 8], 0x21
0x473B9B: je       0x473ba9
0x473B9D: cmp      dword ptr [ebp - 8], 0x14
0x473BA1: je       0x473ba9
0x473BA3: cmp      dword ptr [ebp - 8], 0x1b
0x473BA7: jne      0x473c0d
0x473BA9: cmp      dword ptr [ebp - 0x10], 0
0x473BAD: jne      0x473bd4
0x473BAF: mov      ax, word ptr [ebp + 0x1c]
0x473BB3: push     eax
0x473BB4: xor      eax, eax
0x473BB6: mov      al, byte ptr [ebp + 0x18]
0x473BB9: push     eax
0x473BBA: mov      eax, dword ptr [ebp + 0x14]
0x473BBD: push     eax
0x473BBE: mov      eax, dword ptr [ebp + 0x10]
0x473BC1: push     eax
0x473BC2: mov      cx, word ptr [ebp + 0x20]
0x473BC6: mov      edx, dword ptr [ebp - 0xc]
0x473BC9: mov      eax, dword ptr [ebp - 4]
0x473BCC: call     0x472f18
0x473BD1: mov      dword ptr [ebp - 0x10], eax
0x473BD4: cmp      dword ptr [ebp - 0x10], 0
0x473BD8: je       0x473c0d
0x473BDA: mov      ax, word ptr [ebp + 0x18]
0x473BDE: call     0x406974
0x473BE3: and      eax, 0xff
0x473BE8: mov      edx, dword ptr [ebp - 0x10]
0x473BEB: mov      dword ptr [edx + 0x154], eax
0x473BF1: xor      eax, eax
0x473BF3: mov      al, byte ptr [ebp + 0x18]
0x473BF6: mov      dword ptr [ebp + 0x18], eax
0x473BF9: cmp      dword ptr [ebp - 8], 0x21
0x473BFD: jne      0x473c0d
0x473BFF: mov      eax, dword ptr [ebp - 0x10]
0x473C02: mov      byte ptr [eax + 0x20], 1
0x473C06: mov      eax, dword ptr [ebp - 0x10]
0x473C09: mov      byte ptr [eax + 0x21], 1
0x473C0D: cmp      dword ptr [ebp - 0x10], 0
0x473C11: je       0x473d2d
0x473C17: mov      eax, dword ptr [ebp - 8]
0x473C1A: sub      eax, 0x29
0x473C1D: je       0x473c2b
0x473C1F: sub      eax, 0x268
0x473C24: je       0x473c45
0x473C26: jmp      0x473cd8
0x473C2B: mov      eax, dword ptr [ebp + 0x14]
0x473C2E: mov      edx, dword ptr [ebp - 0x10]
0x473C31: mov      dword ptr [edx + 0x18], eax
0x473C34: mov      eax, dword ptr [ebp - 0x10]
0x473C37: mov      bx, 0xfff5
0x473C3B: call     0x403080
0x473C40: jmp      0x473d2d
0x473C45: mov      eax, dword ptr [ebp + 0x14]
0x473C48: mov      edx, dword ptr [ebp - 0x10]
0x473C4B: mov      dword ptr [edx + 0x1c], eax
0x473C4E: mov      al, byte ptr [ebp + 0x10]
0x473C51: mov      edx, dword ptr [ebp - 0x10]
0x473C54: mov      byte ptr [edx + 0x5c], al
0x473C57: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x473C5C: mov      eax, dword ptr [eax]
0x473C5E: cmp      eax, dword ptr [ebp - 0x10]
0x473C61: jne      0x473d2d
0x473C67: mov      eax, dword ptr [0x4a3fa0] ; *0x4A3FA0->0x4A2D10
0x473C6C: mov      eax, dword ptr [eax]
0x473C6E: push     eax
0x473C6F: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x473C74: mov      eax, dword ptr [eax]
0x473C76: movzx    ecx, word ptr [eax + 0x50]
0x473C7A: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x473C7F: mov      eax, dword ptr [eax]
0x473C81: movzx    eax, word ptr [eax + 0x52]
0x473C85: add      ecx, eax
0x473C87: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x473C8C: mov      eax, dword ptr [eax]
0x473C8E: movzx    eax, byte ptr [eax + 0x54]
0x473C92: add      ecx, eax
0x473C94: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x473C99: mov      eax, dword ptr [eax]
0x473C9B: movzx    eax, byte ptr [eax + 0x55]
0x473C9F: add      ecx, eax
0x473CA1: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x473CA6: mov      eax, dword ptr [eax]
0x473CA8: movzx    eax, byte ptr [eax + 0x56]
0x473CAC: add      ecx, eax
0x473CAE: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x473CB3: mov      eax, dword ptr [eax]
0x473CB5: movzx    eax, byte ptr [eax + 0x57]
0x473CB9: add      ecx, eax
0x473CBB: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x473CC0: mov      eax, dword ptr [eax]
0x473CC2: movsx    edx, byte ptr [eax + 0x5c]
0x473CC6: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x473CCB: mov      eax, dword ptr [eax]
0x473CCD: movzx    eax, byte ptr [eax + 0x30]
0x473CD1: call     0x45eb00
0x473CD6: jmp      0x473d2d
0x473CD8: cmp      dword ptr [ebp - 8], 0xa
0x473CDC: jne      0x473cf2
0x473CDE: cmp      dword ptr [ebp + 8], 0
0x473CE2: je       0x473cf2
0x473CE4: mov      eax, dword ptr [ebp - 0x10]
0x473CE7: add      eax, 0x28
0x473CEA: mov      edx, dword ptr [ebp + 8]
0x473CED: call     0x403aac
0x473CF2: cmp      dword ptr [ebp - 8], 0xb
0x473CF6: jne      0x473d0a
0x473CF8: cmp      dword ptr [ebp + 0xc], 0
0x473CFC: jle      0x473d0a
0x473CFE: mov      eax, dword ptr [ebp + 0xc]
0x473D01: mov      edx, dword ptr [ebp - 0x10]
0x473D04: mov      dword ptr [edx + 0x160], eax
0x473D0A: mov      eax, dword ptr [ebp + 0x1c]
0x473D0D: push     eax
0x473D0E: mov      eax, dword ptr [ebp + 0x18]
0x473D11: push     eax
0x473D12: mov      eax, dword ptr [ebp + 0x14]
0x473D15: push     eax
0x473D16: mov      eax, dword ptr [ebp + 0x10]
0x473D19: push     eax
0x473D1A: push     0
0x473D1C: push     0
0x473D1E: mov      ecx, dword ptr [ebp + 0x20]
0x473D21: mov      dx, word ptr [ebp - 8]
0x473D25: mov      eax, dword ptr [ebp - 0x10]
0x473D28: call     0x496548
0x473D2D: xor      eax, eax
0x473D2F: pop      edx
0x473D30: pop      ecx
0x473D31: pop      ecx
0x473D32: mov      dword ptr fs:[eax], edx
0x473D35: push     0x473d4a
0x473D3A: lea      eax, [ebp + 8]
0x473D3D: call     0x403a58
0x473D42: ret      
0x473D43: jmp      0x403518
0x473D48: jmp      0x473d3a
0x473D4A: pop      ebx
0x473D4B: mov      esp, ebp
0x473D4D: pop      ebp
0x473D4E: ret      0x1c
0x473D51: add      byte ptr [eax], al
0x473D53: add      bh, bh
0x473D57: inc      dword ptr [ecx]
0x473D59: add      byte ptr [eax], al
0x473D5B: add      byte ptr [eax], dh
0x473D5D: add      byte ptr [eax], al
0x473D5F: add      byte ptr [ebp - 0x75], dl
0x473D62: in       al, dx
0x473D63: xor      eax, eax
0x473D65: push     ebp
0x473D66: push     0x473d9c
0x473D6B: push     dword ptr fs:[eax]
0x473D6E: mov      dword ptr fs:[eax], esp
0x473D71: inc      dword ptr [0x4f95b4]
0x473D77: jne      0x473d8e
0x473D79: mov      eax, 0x4a2d1c ; *0x4A2D1C->0x46ECE4
0x473D7E: mov      ecx, 6
0x473D83: mov      edx, dword ptr [0x401094]
0x473D89: call     0x4042e4
0x473D8E: xor      eax, eax
0x473D90: pop      edx
0x473D91: pop      ecx
0x473D92: pop      ecx
0x473D93: mov      dword ptr fs:[eax], edx
0x473D96: push     0x473da3
0x473D9B: ret      
0x473D9C: jmp      0x403518
0x473DA1: jmp      0x473d9b
0x473DA3: pop      ebp
0x473DA4: ret      
0x473DA5: lea      eax, [eax]
0x473DA8: sub      dword ptr [0x4f95b4], 1
0x473DAF: ret      
0x473DB0: cld      
0x473DB1: cmp      eax, 0x47
0x473DB6: add      byte ptr [eax], al
0x473DB8: add      byte ptr [eax], al
0x473DBA: add      byte ptr [eax], al
0x473DBC: add      byte ptr [eax], al
0x473DBE: add      byte ptr [eax], al
0x473DC0: add      byte ptr [eax], al
0x473DC2: add      byte ptr [eax], al
0x473DC4: add      byte ptr [eax], al
0x473DC6: add      byte ptr [eax], al
0x473DC8: add      byte ptr [eax], al
0x473DCA: add      byte ptr [eax], al
0x473DCC: cld      
0x473DCD: cmp      eax, 0x3e220047
0x473DD2: inc      edi
0x473DD3: add      byte ptr [eax + 2], dl
0x473DD6: add      byte ptr [eax], al
0x473DD8: int3     
0x473DD9: pop      edx
0x473DDA: dec      ecx
0x473DDB: add      al, cl
0x473DDD: xor      byte ptr [eax], al
0x473DE0: aam      0x30
0x473DE2: inc      eax
0x473DE3: add      al, bl
0x473DE5: xor      byte ptr [eax], al
0x473DE8: fdiv     qword ptr [eax]
0x473DEA: inc      eax
0x473DEB: add      al, dl
0x473DED: xor      byte ptr [eax], al
0x473DF0: inc      eax
0x473DF1: inc      eax
0x473DF3: add      byte ptr [esi + ebp + 0x40], dl
0x473DF7: add      byte ptr [eax], cl
0x473DF9: dec      ecx
