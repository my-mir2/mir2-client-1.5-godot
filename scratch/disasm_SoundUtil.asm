0x45DE00: mov      eax, dword ptr [ebp - 4]
0x45DE03: cmp      word ptr [eax + 0x6a], 0
0x45DE08: je       0x45de16
0x45DE0A: mov      ebx, dword ptr [ebp - 4]
0x45DE0D: mov      edx, dword ptr [ebp - 4]
0x45DE10: mov      eax, dword ptr [ebx + 0x6c]
0x45DE13: call     dword ptr [ebx + 0x68]
0x45DE16: pop      ebx
0x45DE17: pop      ecx
0x45DE18: pop      ebp
0x45DE19: ret      
0x45DE1A: mov      eax, eax
0x45DE1C: push     ebp
0x45DE1D: mov      ebp, esp
0x45DE1F: push     ecx
0x45DE20: mov      dword ptr [ebp - 4], eax
0x45DE23: mov      eax, dword ptr [ebp - 4]
0x45DE26: cmp      byte ptr [eax + 0x45], 0
0x45DE2A: je       0x45deed
0x45DE30: xor      eax, eax
0x45DE32: push     ebp
0x45DE33: push     0x45dee6
0x45DE38: push     dword ptr fs:[eax]
0x45DE3B: mov      dword ptr fs:[eax], esp
0x45DE3E: mov      eax, dword ptr [ebp - 4]
0x45DE41: mov      eax, dword ptr [eax + 0x78]
0x45DE44: call     0x402eac
0x45DE49: mov      eax, dword ptr [ebp - 4]
0x45DE4C: xor      edx, edx
0x45DE4E: mov      dword ptr [eax + 0x78], edx
0x45DE51: xor      eax, eax
0x45DE53: push     ebp
0x45DE54: push     0x45de8f
0x45DE59: push     dword ptr fs:[eax]
0x45DE5C: mov      dword ptr fs:[eax], esp
0x45DE5F: mov      eax, dword ptr [ebp - 4]
0x45DE62: cmp      byte ptr [eax + 0x25], 0
0x45DE66: je       0x45de77
0x45DE68: mov      eax, dword ptr [ebp - 4]
0x45DE6B: mov      byte ptr [eax + 0x25], 0
0x45DE6F: mov      eax, dword ptr [ebp - 4]
0x45DE72: mov      edx, dword ptr [eax]
0x45DE74: call     dword ptr [edx + 0x30]
0x45DE77: xor      eax, eax
0x45DE79: pop      edx
0x45DE7A: pop      ecx
0x45DE7B: pop      ecx
0x45DE7C: mov      dword ptr fs:[eax], edx
0x45DE7F: push     0x45de96
0x45DE84: mov      dl, 3
0x45DE86: mov      eax, dword ptr [ebp - 4]
0x45DE89: call     0x45dd08
0x45DE8E: ret      
0x45DE8F: jmp      0x403518
0x45DE94: jmp      0x45de84
0x45DE96: xor      eax, eax
0x45DE98: pop      edx
0x45DE99: pop      ecx
0x45DE9A: pop      ecx
0x45DE9B: mov      dword ptr fs:[eax], edx
0x45DE9E: push     0x45deed
0x45DEA3: mov      eax, dword ptr [ebp - 4]
0x45DEA6: mov      byte ptr [eax + 0x44], 0
0x45DEAA: mov      eax, dword ptr [ebp - 4]
0x45DEAD: mov      byte ptr [eax + 0x45], 0
0x45DEB1: mov      eax, dword ptr [ebp - 4]
0x45DEB4: mov      dl, byte ptr [eax + 0x70]
0x45DEB7: mov      eax, dword ptr [ebp - 4]
0x45DEBA: call     0x45e214
0x45DEBF: mov      eax, dword ptr [ebp - 4]
0x45DEC2: mov      eax, dword ptr [eax + 0x74]
0x45DEC5: call     0x402eac
0x45DECA: mov      eax, dword ptr [ebp - 4]
0x45DECD: xor      edx, edx
0x45DECF: mov      dword ptr [eax + 0x74], edx
0x45DED2: mov      eax, dword ptr [ebp - 4]
0x45DED5: mov      eax, dword ptr [eax + 0x3c]
0x45DED8: call     0x402eac
0x45DEDD: mov      eax, dword ptr [ebp - 4]
0x45DEE0: xor      edx, edx
0x45DEE2: mov      dword ptr [eax + 0x3c], edx
0x45DEE5: ret      
0x45DEE6: jmp      0x403518
0x45DEEB: jmp      0x45dea3
0x45DEED: pop      ecx
0x45DEEE: pop      ebp
0x45DEEF: ret      
0x45DEF0: push     ebp
0x45DEF1: mov      ebp, esp
0x45DEF3: add      esp, -0x18
0x45DEF6: push     ebx
0x45DEF7: push     esi
0x45DEF8: push     edi
0x45DEF9: xor      edx, edx
0x45DEFB: mov      dword ptr [ebp - 0xc], edx
0x45DEFE: mov      dword ptr [ebp - 0x18], edx
0x45DF01: mov      dword ptr [ebp - 4], eax
0x45DF04: xor      eax, eax
0x45DF06: push     ebp
0x45DF07: push     0x45e0a7
0x45DF0C: push     dword ptr fs:[eax]
0x45DF0F: mov      dword ptr fs:[eax], esp
0x45DF12: mov      eax, dword ptr [ebp - 4]
0x45DF15: call     0x45de1c
0x45DF1A: mov      eax, dword ptr [ebp - 4]
0x45DF1D: mov      eax, dword ptr [eax + 4]
0x45DF20: mov      dword ptr [ebp - 8], eax
0x45DF23: jmp      0x45df2e
0x45DF25: mov      eax, dword ptr [ebp - 8]
0x45DF28: mov      eax, dword ptr [eax + 4]
0x45DF2B: mov      dword ptr [ebp - 8], eax
0x45DF2E: cmp      dword ptr [ebp - 8], 0
0x45DF32: je       0x45df46
0x45DF34: mov      eax, dword ptr [ebp - 8]
0x45DF37: mov      edx, dword ptr [0x4398e0]
0x45DF3D: call     0x40301c
0x45DF42: test     al, al
0x45DF44: je       0x45df25
0x45DF46: cmp      dword ptr [ebp - 8], 0
0x45DF4A: jne      0x45df6d
0x45DF4C: lea      edx, [ebp - 0xc]
0x45DF4F: mov      eax, dword ptr [0x4a42a8] ; *0x4A42A8->0x447358
0x45DF54: call     0x40520c
0x45DF59: mov      ecx, dword ptr [ebp - 0xc]
0x45DF5C: mov      dl, 1
0x45DF5E: mov      eax, dword ptr [0x45c8bc]
0x45DF63: call     0x40ae40
0x45DF68: call     0x403550
0x45DF6D: mov      dl, 1
0x45DF6F: mov      eax, dword ptr [ebp - 4]
0x45DF72: call     0x45dd08
0x45DF77: mov      eax, dword ptr [ebp - 4]
0x45DF7A: mov      edx, dword ptr [eax]
0x45DF7C: call     dword ptr [edx + 0x38]
0x45DF7F: mov      eax, dword ptr [ebp - 4]
0x45DF82: mov      byte ptr [eax + 0x45], 1
0x45DF86: xor      eax, eax
0x45DF88: push     ebp
0x45DF89: push     0x45e051
0x45DF8E: push     dword ptr fs:[eax]
0x45DF91: mov      dword ptr fs:[eax], esp
0x45DF94: mov      eax, dword ptr [ebp - 4]
0x45DF97: mov      ecx, dword ptr [eax + 0x28]
0x45DF9A: mov      dl, 1
0x45DF9C: mov      eax, dword ptr [0x45db80]
0x45DFA1: call     0x45cca0
0x45DFA6: mov      edx, dword ptr [ebp - 4]
0x45DFA9: mov      dword ptr [edx + 0x3c], eax
0x45DFAC: mov      eax, dword ptr [ebp - 4]
0x45DFAF: mov      eax, dword ptr [eax + 0x3c]
0x45DFB2: mov      edx, dword ptr [ebp - 4]
0x45DFB5: mov      dword ptr [eax + 0x18], edx
0x45DFB8: mov      eax, dword ptr [ebp - 4]
0x45DFBB: test     byte ptr [eax + 0x4c], 1
0x45DFBF: setne    al
0x45DFC2: mov      edx, dword ptr [ebp - 4]
0x45DFC5: mov      edx, dword ptr [edx + 0x3c]
0x45DFC8: mov      byte ptr [edx + 0xc], al
0x45DFCB: mov      eax, dword ptr [ebp - 4]
0x45DFCE: mov      ecx, dword ptr [eax + 0x3c]
0x45DFD1: mov      dl, 1
0x45DFD3: mov      eax, dword ptr [0x45c7ac]
0x45DFD8: call     0x45cfb0
0x45DFDD: mov      edx, dword ptr [ebp - 4]
0x45DFE0: mov      dword ptr [edx + 0x74], eax
0x45DFE3: mov      edx, 0x4a2cc8 ; *0x4A2CC8->0x000014
0x45DFE8: mov      eax, dword ptr [ebp - 4]
0x45DFEB: mov      eax, dword ptr [eax + 0x74]
0x45DFEE: call     0x45d184
0x45DFF3: test     al, al
0x45DFF5: jne      0x45e035
0x45DFF7: lea      edx, [ebp - 0xc]
0x45DFFA: mov      eax, dword ptr [0x4a3e14] ; *0x4A3E14->0x447318
0x45DFFF: call     0x40520c
0x45E004: mov      eax, dword ptr [ebp - 0xc]
0x45E007: mov      dword ptr [ebp - 0x14], eax
0x45E00A: mov      byte ptr [ebp - 0x10], 0xb
0x45E00E: lea      eax, [ebp - 0x14]
0x45E011: push     eax
0x45E012: push     0
0x45E014: lea      edx, [ebp - 0x18]
0x45E017: mov      eax, dword ptr [0x4a4164] ; *0x4A4164->0x447330
0x45E01C: call     0x40520c
0x45E021: mov      ecx, dword ptr [ebp - 0x18]
0x45E024: mov      dl, 1
0x45E026: mov      eax, dword ptr [0x45c8bc]
0x45E02B: call     0x40ae7c
0x45E030: call     0x403550
0x45E035: mov      eax, dword ptr [ebp - 4]
0x45E038: mov      byte ptr [eax + 0x44], 1
0x45E03C: mov      edx, dword ptr [ebp - 8]
0x45E03F: mov      eax, dword ptr [ebp - 4]
0x45E042: call     0x45e134
0x45E047: xor      eax, eax
0x45E049: pop      edx
0x45E04A: pop      ecx
0x45E04B: pop      ecx
0x45E04C: mov      dword ptr fs:[eax], edx
0x45E04F: jmp      0x45e068
0x45E051: jmp      0x403310
0x45E056: mov      eax, dword ptr [ebp - 4]
0x45E059: call     0x45de1c
0x45E05E: call     0x40356c
0x45E063: call     0x4035c0
0x45E068: mov      dl, 2
0x45E06A: mov      eax, dword ptr [ebp - 4]
0x45E06D: call     0x45dd08
0x45E072: mov      eax, dword ptr [ebp - 4]
0x45E075: mov      byte ptr [eax + 0x25], 1
0x45E079: mov      eax, dword ptr [ebp - 4]
0x45E07C: mov      edx, dword ptr [eax]
0x45E07E: call     dword ptr [edx + 0x34]
0x45E081: mov      eax, dword ptr [ebp - 4]
0x45E084: call     0x45e10c
0x45E089: xor      eax, eax
0x45E08B: pop      edx
0x45E08C: pop      ecx
0x45E08D: pop      ecx
0x45E08E: mov      dword ptr fs:[eax], edx
0x45E091: push     0x45e0ae
0x45E096: lea      eax, [ebp - 0x18]
0x45E099: call     0x403a58
0x45E09E: lea      eax, [ebp - 0xc]
0x45E0A1: call     0x403a58
0x45E0A6: ret      
0x45E0A7: jmp      0x403518
0x45E0AC: jmp      0x45e096
0x45E0AE: pop      edi
0x45E0AF: pop      esi
0x45E0B0: pop      ebx
0x45E0B1: mov      esp, ebp
0x45E0B3: pop      ebp
0x45E0B4: ret      
0x45E0B5: lea      eax, [eax]
0x45E0B8: push     ebp
0x45E0B9: mov      ebp, esp
0x45E0BB: push     ecx
0x45E0BC: push     ebx
0x45E0BD: push     esi
0x45E0BE: push     edi
0x45E0BF: mov      dword ptr [ebp - 4], eax
0x45E0C2: mov      eax, dword ptr [ebp - 4]
0x45E0C5: call     0x414cdc
0x45E0CA: mov      eax, dword ptr [ebp - 4]
0x45E0CD: cmp      byte ptr [eax + 0x24], 0
0x45E0D1: je       0x45e106
0x45E0D3: mov      eax, dword ptr [ebp - 4]
0x45E0D6: test     byte ptr [eax + 0x20], 0x10
0x45E0DA: jne      0x45e106
0x45E0DC: xor      eax, eax
0x45E0DE: push     ebp
0x45E0DF: push     0x45e0fc
0x45E0E4: push     dword ptr fs:[eax]
0x45E0E7: mov      dword ptr fs:[eax], esp
0x45E0EA: mov      eax, dword ptr [ebp - 4]
0x45E0ED: call     0x45def0
0x45E0F2: xor      eax, eax
0x45E0F4: pop      edx
0x45E0F5: pop      ecx
0x45E0F6: pop      ecx
0x45E0F7: mov      dword ptr fs:[eax], edx
0x45E0FA: jmp      0x45e106
0x45E0FC: jmp      0x403310
0x45E101: call     0x4035c0
0x45E106: pop      edi
0x45E107: pop      esi
0x45E108: pop      ebx
0x45E109: pop      ecx
0x45E10A: pop      ebp
0x45E10B: ret      
0x45E10C: push     ebp
0x45E10D: mov      ebp, esp
0x45E10F: push     ecx
0x45E110: mov      dword ptr [ebp - 4], eax
0x45E113: mov      eax, dword ptr [ebp - 4]
0x45E116: cmp      byte ptr [eax + 0x44], 0
0x45E11A: je       0x45e12e
0x45E11C: mov      dl, 4
0x45E11E: mov      eax, dword ptr [ebp - 4]
0x45E121: call     0x45dd08
0x45E126: mov      eax, dword ptr [ebp - 4]
0x45E129: mov      edx, dword ptr [eax]
0x45E12B: call     dword ptr [edx + 0x3c]
0x45E12E: pop      ecx
0x45E12F: pop      ebp
0x45E130: ret      
0x45E131: lea      eax, [eax]
0x45E134: push     ebp
0x45E135: mov      ebp, esp
0x45E137: add      esp, -0x10
0x45E13A: xor      ecx, ecx
0x45E13C: mov      dword ptr [ebp - 0x10], ecx
0x45E13F: mov      dword ptr [ebp - 8], edx
0x45E142: mov      dword ptr [ebp - 4], eax
0x45E145: xor      eax, eax
0x45E147: push     ebp
0x45E148: push     0x45e206
0x45E14D: push     dword ptr fs:[eax]
0x45E150: mov      dword ptr fs:[eax], esp
0x45E153: mov      eax, dword ptr [ebp - 8]
0x45E156: mov      edx, dword ptr [ebp - 4]
0x45E159: mov      dword ptr [edx + 0x40], eax
0x45E15C: mov      eax, dword ptr [ebp - 4]
0x45E15F: mov      eax, dword ptr [eax + 0x78]
0x45E162: call     0x402eac
0x45E167: mov      eax, dword ptr [ebp - 4]
0x45E16A: push     eax
0x45E16B: push     0x45dd50
0x45E170: mov      eax, dword ptr [ebp - 4]
0x45E173: mov      ecx, dword ptr [eax + 0x40]
0x45E176: mov      dl, 1
0x45E178: mov      eax, dword ptr [0x447614]
0x45E17D: call     0x447a48
0x45E182: mov      edx, dword ptr [ebp - 4]
0x45E185: mov      dword ptr [edx + 0x78], eax
0x45E188: mov      eax, dword ptr [ebp - 4]
0x45E18B: cmp      byte ptr [eax + 0x44], 0
0x45E18F: je       0x45e1f0
0x45E191: mov      eax, dword ptr [ebp - 4]
0x45E194: test     byte ptr [eax + 0x4c], 8
0x45E198: je       0x45e1a3
0x45E19A: mov      dword ptr [ebp - 0xc], 4
0x45E1A1: jmp      0x45e1bc
0x45E1A3: mov      eax, dword ptr [ebp - 4]
0x45E1A6: test     byte ptr [eax + 0x4c], 4
0x45E1AA: je       0x45e1b5
0x45E1AC: mov      dword ptr [ebp - 0xc], 3
0x45E1B3: jmp      0x45e1bc
0x45E1B5: mov      dword ptr [ebp - 0xc], 1
0x45E1BC: mov      eax, dword ptr [ebp - 0xc]
0x45E1BF: push     eax
0x45E1C0: mov      eax, dword ptr [ebp - 4]
0x45E1C3: mov      eax, dword ptr [eax + 0x40]
0x45E1C6: call     0x42d734
0x45E1CB: push     eax
0x45E1CC: lea      edx, [ebp - 0x10]
0x45E1CF: mov      eax, dword ptr [ebp - 4]
0x45E1D2: mov      eax, dword ptr [eax + 0x3c]
0x45E1D5: call     0x45cf10
0x45E1DA: mov      eax, dword ptr [ebp - 0x10]
0x45E1DD: push     eax
0x45E1DE: mov      eax, dword ptr [eax]
0x45E1E0: call     dword ptr [eax + 0x18]
0x45E1E3: mov      edx, eax
0x45E1E5: mov      eax, dword ptr [ebp - 4]
0x45E1E8: mov      eax, dword ptr [eax + 0x3c]
0x45E1EB: call     0x4478c0
0x45E1F0: xor      eax, eax
0x45E1F2: pop      edx
0x45E1F3: pop      ecx
0x45E1F4: pop      ecx
0x45E1F5: mov      dword ptr fs:[eax], edx
0x45E1F8: push     0x45e20d
0x45E1FD: lea      eax, [ebp - 0x10]
0x45E200: call     0x405264
0x45E205: ret      
0x45E206: jmp      0x403518
0x45E20B: jmp      0x45e1fd
0x45E20D: mov      esp, ebp
0x45E20F: pop      ebp
0x45E210: ret      
0x45E211: lea      eax, [eax]
0x45E214: push     ebp
0x45E215: mov      ebp, esp
0x45E217: add      esp, -8
0x45E21A: push     ebx
0x45E21B: mov      byte ptr [ebp - 5], dl
0x45E21E: mov      dword ptr [ebp - 4], eax
0x45E221: mov      al, byte ptr [ebp - 5]
0x45E224: mov      edx, dword ptr [ebp - 4]
0x45E227: mov      byte ptr [edx + 0x70], al
0x45E22A: mov      eax, dword ptr [ebp - 4]
0x45E22D: cmp      byte ptr [eax + 0x44], 0
0x45E231: je       0x45e28c
0x45E233: mov      eax, dword ptr [ebp - 4]
0x45E236: mov      al, byte ptr [eax + 0x4c]
0x45E239: mov      byte ptr [ebp - 6], al
0x45E23C: mov      bl, byte ptr [0x4a2cdc] ; *0x4A2CDC->0x408D0C
0x45E242: not      ebx
0x45E244: and      bl, byte ptr [ebp - 5]
0x45E247: mov      al, byte ptr [0x4a2cdc] ; *0x4A2CDC->0x408D0C
0x45E24C: not      eax
0x45E24E: and      al, byte ptr [0x45e2a0]
0x45E254: mov      edx, dword ptr [ebp - 4]
0x45E257: not      eax
0x45E259: and      al, byte ptr [edx + 0x4c]
0x45E25C: or       bl, al
0x45E25E: mov      eax, dword ptr [ebp - 4]
0x45E261: mov      byte ptr [eax + 0x4c], bl
0x45E264: mov      eax, dword ptr [ebp - 4]
0x45E267: test     byte ptr [eax + 0x4c], 1
0x45E26B: setne    al
0x45E26E: mov      edx, dword ptr [ebp - 4]
0x45E271: mov      edx, dword ptr [edx + 0x3c]
0x45E274: mov      byte ptr [edx + 0xc], al
0x45E277: mov      eax, dword ptr [ebp - 4]
0x45E27A: test     byte ptr [eax + 0x4c], 2
0x45E27E: setne    al
0x45E281: mov      edx, dword ptr [ebp - 4]
0x45E284: mov      edx, dword ptr [edx + 0x3c]
0x45E287: mov      byte ptr [edx + 0x15], al
0x45E28A: jmp      0x45e298
0x45E28C: mov      eax, dword ptr [ebp - 4]
0x45E28F: mov      al, byte ptr [eax + 0x70]
0x45E292: mov      edx, dword ptr [ebp - 4]
0x45E295: mov      byte ptr [edx + 0x4c], al
0x45E298: pop      ebx
0x45E299: pop      ecx
0x45E29A: pop      ecx
0x45E29B: pop      ebp
0x45E29C: ret      
0x45E29D: add      byte ptr [eax], al
0x45E29F: add      byte ptr [edi], cl
0x45E2A1: add      byte ptr [eax], al
0x45E2A3: add      byte ptr [ebp - 0x75], dl
0x45E2A6: in       al, dx
0x45E2A7: xor      eax, eax
0x45E2A9: push     ebp
0x45E2AA: push     0x45e2df
0x45E2AF: push     dword ptr fs:[eax]
0x45E2B2: mov      dword ptr fs:[eax], esp
0x45E2B5: inc      dword ptr [0x4f7d78]
0x45E2BB: jne      0x45e2d1
0x45E2BD: mov      eax, dword ptr [0x4f7d7c]
0x45E2C2: call     0x402eac
0x45E2C7: mov      eax, dword ptr [0x4f7d80]
0x45E2CC: call     0x402eac
0x45E2D1: xor      eax, eax
0x45E2D3: pop      edx
0x45E2D4: pop      ecx
0x45E2D5: pop      ecx
0x45E2D6: mov      dword ptr fs:[eax], edx
0x45E2D9: push     0x45e2e6
0x45E2DE: ret      
0x45E2DF: jmp      0x403518
0x45E2E4: jmp      0x45e2de
0x45E2E6: pop      ebp
0x45E2E7: ret      
0x45E2E8: sub      dword ptr [0x4f7d78], 1
0x45E2EF: ret      
0x45E2F0: push     ebp
0x45E2F1: mov      ebp, esp
0x45E2F3: xor      eax, eax
0x45E2F5: push     ebp
0x45E2F6: push     0x45e315
0x45E2FB: push     dword ptr fs:[eax]
0x45E2FE: mov      dword ptr fs:[eax], esp
0x45E301: inc      dword ptr [0x4f7d84]
0x45E307: xor      eax, eax
0x45E309: pop      edx
0x45E30A: pop      ecx
0x45E30B: pop      ecx
0x45E30C: mov      dword ptr fs:[eax], edx
0x45E30F: push     0x45e31c
0x45E314: ret      
0x45E315: jmp      0x403518
0x45E31A: jmp      0x45e314
0x45E31C: pop      ebp
0x45E31D: ret      
0x45E31E: mov      eax, eax
0x45E320: sub      dword ptr [0x4f7d84], 1
0x45E327: ret      
0x45E330: [STR len=15] "211.174.174.130"
0x45E348: [STR len=4] "宋体"
0x45E358: [STR len=4] "楷体"
0x45E368: [STR len=4] "仿宋"
0x45E378: [STR len=4] "黑体"
0x45E388: [STR len=11] "Courier New"
0x45E39C: [STR len=5] "Arial"
0x45E3AC: [STR len=13] "MS Sans Serif"
0x45E3C4: [STR len=20] "Microsoft Sans Serif"
0x45E3E4: [STR len=4] "楷体"
0x45E3ED: jecxz    0x45e434
0x45E3EF: add      byte ptr [esi], cl
0x45E3F1: or       al, 0x54
0x45E3F3: dec      ebx
0x45E3F4: outsd    dx, dword ptr [esi]
0x45E3F5: jb       0x45e465
0x45E3F7: je       0x45e451
0x45E3FA: outsd    dx, dword ptr [esi]
0x45E3FB: jb       0x45e469
0x45E3FD: adc      byte ptr fs:[eax], al
0x45E400: add      byte ptr [eax], al
0x45E402: add      al, 0
0x45E404: add      byte ptr [eax], al
0x45E406: xchg     esp, eax
0x45E407: adc      byte ptr [eax], al
0x45E40A: add      byte ptr [eax], al
0x45E40C: add      byte ptr [eax], al
0x45E40E: xchg     esp, eax
0x45E40F: adc      byte ptr [eax], al
0x45E412: add      al, 0
0x45E414: add      byte ptr [eax], al
0x45E416: xchg     esp, eax
0x45E417: adc      byte ptr [eax], al
0x45E41A: or       byte ptr [eax], al
0x45E41C: add      byte ptr [eax], al
0x45E41E: xchg     esp, eax
0x45E41F: adc      byte ptr [eax], al
0x45E422: or       al, 0
0x45E424: add      byte ptr [eax], al
0x45E426: mov      eax, eax
0x45E428: je       0x45e40e
0x45E42A: inc      ebp
0x45E42B: add      byte ptr [eax], al
0x45E42D: add      byte ptr [eax], al
0x45E42F: add      byte ptr [eax], al
0x45E431: add      byte ptr [eax], al
0x45E433: add      byte ptr [eax - 0x1b], cl
0x45E436: inc      ebp
0x45E437: add      al, ah
0x45E439: ljmp     0x45:0xe5920045
0x45E440: mov      dl, 0xe8
0x45E442: inc      ebp
0x45E443: add      byte ptr [esi - 0x49ffba16], ch
0x45E449: ljmp     0:0x42c0045
0x45E450: sal      byte ptr [esp + eax*2], 0x9c
0x45E455: push     eax
0x45E456: inc      ecx
0x45E457: add      byte ptr [eax - 0xfffbc36], dh
0x45E45D: retf     
0x45E45E: inc      ebx
0x45E45F: add      ah, bl
0x45E461: xor      byte ptr [eax], al
0x45E464: and      dh, ch
0x45E466: inc      ebx
0x45E467: add      byte ptr [eax + 0x2e], al
0x45E46A: inc      eax
0x45E46B: add      byte ptr [esi + ebp + 0x40], dl
0x45E46F: add      byte ptr [ecx + edi*2 + 0x44], ch
