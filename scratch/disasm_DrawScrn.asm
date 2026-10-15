
; === VMT_TDrawScreen @ 0x479884 ===
0x479884: or       edx, dword ptr [esp + eax*2 + 0x72]
0x479888: popal    
0x479889: ja       0x4798de
0x47988B: arpl     word ptr [edx + 0x65], si
0x47988E: outsb    dx, byte ptr gs:[esi]
0x479890: push     ebp
0x479891: mov      ebp, esp
0x479893: add      esp, -0xc
0x479896: test     dl, dl
0x479898: je       0x4798a2
0x47989A: add      esp, -0x10
0x47989D: call     0x4031a4
0x4798A2: mov      byte ptr [ebp - 5], dl
0x4798A5: mov      dword ptr [ebp - 4], eax
0x4798A8: mov      eax, dword ptr [ebp - 4]
0x4798AB: xor      edx, edx
0x4798AD: mov      dword ptr [eax + 0x14], edx
0x4798B0: call     0x406154
0x4798B5: mov      edx, dword ptr [ebp - 4]
0x4798B8: mov      dword ptr [edx + 4], eax
0x4798BB: mov      eax, dword ptr [ebp - 4]
0x4798BE: xor      edx, edx
0x4798C0: mov      dword ptr [eax + 8], edx
0x4798C3: mov      dl, 1
0x4798C5: mov      eax, dword ptr [0x40e37c]
0x4798CA: call     0x402e7c
0x4798CF: mov      edx, dword ptr [ebp - 4]
0x4798D2: mov      dword ptr [edx + 0x10], eax
0x4798D5: mov      dl, 1
0x4798D7: mov      eax, dword ptr [0x40e37c]
0x4798DC: call     0x402e7c
0x4798E1: mov      edx, dword ptr [ebp - 4]
0x4798E4: mov      dword ptr [edx + 0x18], eax
0x4798E7: mov      dl, 1
0x4798E9: mov      eax, dword ptr [0x40df00]
0x4798EE: call     0x402e7c
0x4798F3: mov      edx, dword ptr [ebp - 4]
0x4798F6: mov      dword ptr [edx + 0x1c], eax
0x4798F9: mov      eax, dword ptr [ebp - 4]
0x4798FC: xor      edx, edx
0x4798FE: mov      dword ptr [eax + 0x20], edx
0x479901: mov      dl, 1
0x479903: mov      eax, dword ptr [0x40e37c]
0x479908: call     0x402e7c
0x47990D: mov      edx, dword ptr [ebp - 4]
0x479910: mov      dword ptr [edx + 0x24], eax
0x479913: mov      eax, dword ptr [ebp - 4]
0x479916: cmp      byte ptr [ebp - 5], 0
0x47991A: je       0x47992b
0x47991C: call     0x4031fc
0x479921: pop      dword ptr fs:[0]
0x479928: add      esp, 0xc
0x47992B: mov      eax, dword ptr [ebp - 4]
0x47992E: mov      esp, ebp
0x479930: pop      ebp
0x479931: ret      
0x479932: mov      eax, eax
0x479934: push     ebp
0x479935: mov      ebp, esp
0x479937: add      esp, -8
0x47993A: call     0x403204
0x47993F: mov      byte ptr [ebp - 5], dl
0x479942: mov      dword ptr [ebp - 4], eax
0x479945: mov      eax, dword ptr [ebp - 4]
0x479948: mov      eax, dword ptr [eax + 0x10]
0x47994B: call     0x402eac
0x479950: mov      eax, dword ptr [ebp - 4]
0x479953: mov      eax, dword ptr [eax + 0x18]
0x479956: call     0x402eac
0x47995B: mov      eax, dword ptr [ebp - 4]
0x47995E: mov      eax, dword ptr [eax + 0x1c]
0x479961: call     0x402eac
0x479966: mov      eax, dword ptr [ebp - 4]
0x479969: mov      eax, dword ptr [eax + 0x24]
0x47996C: call     0x402eac
0x479971: mov      dl, byte ptr [ebp - 5]
0x479974: and      dl, 0xfc
0x479977: mov      eax, dword ptr [ebp - 4]
0x47997A: call     0x402e9c
0x47997F: cmp      byte ptr [ebp - 5], 0
0x479983: jle      0x47998d
0x479985: mov      eax, dword ptr [ebp - 4]
0x479988: call     0x4031f4
0x47998D: pop      ecx
0x47998E: pop      ecx
0x47998F: pop      ebp
0x479990: ret      
0x479991: lea      eax, [eax]
0x479994: push     ebp
0x479995: mov      ebp, esp
0x479997: push     ecx
0x479998: mov      dword ptr [ebp - 4], eax
0x47999B: pop      ecx
0x47999C: pop      ebp
0x47999D: ret      
0x47999E: mov      eax, eax
0x4799A0: push     ebp
0x4799A1: mov      ebp, esp
0x4799A3: push     ecx
0x4799A4: mov      dword ptr [ebp - 4], eax
0x4799A7: pop      ecx
0x4799A8: pop      ebp
0x4799A9: ret      
0x4799AA: mov      eax, eax
0x4799AC: push     ebp
0x4799AD: mov      ebp, esp
0x4799AF: add      esp, -8
0x4799B2: push     ebx
0x4799B3: mov      byte ptr [ebp - 5], dl
0x4799B6: mov      dword ptr [ebp - 4], eax
0x4799B9: mov      eax, dword ptr [ebp - 4]
0x4799BC: cmp      dword ptr [eax + 0x14], 0
0x4799C0: je       0x4799d1
0x4799C2: mov      eax, dword ptr [ebp - 4]
0x4799C5: mov      eax, dword ptr [eax + 0x14]
0x4799C8: mov      bx, 0xfffc
0x4799CC: call     0x403080
0x4799D1: xor      eax, eax
0x4799D3: mov      al, byte ptr [ebp - 5]
0x4799D6: cmp      eax, 7
0x4799D9: ja       0x479a4b
0x4799DB: jmp      dword ptr [eax*4 + 0x4799e2]
0x4799E2: add      bl, byte ptr [edx - 0x65eeffb9]
0x4799E8: inc      edi
0x4799E9: add      byte ptr [ebx - 0x66], cl
0x4799EC: inc      edi
0x4799ED: add      byte ptr [eax], ah
0x4799EF: lcall    0x47, 0x9a4b0047
0x4799F6: dec      ebx
0x4799F7: lcall    0x47, 0x9a2f0047
0x4799FE: lcall    0x4a40, 0xbca10047
0x479A06: add      byte ptr [ebx - 0x3aa7500], cl
0x479A0C: mov      dword ptr [edx + 0x14], eax
0x479A0F: jmp      0x479a4b
0x479A11: mov      eax, dword ptr [0x4a4134] ; *0x4A4134->0x4F7D94
0x479A16: mov      eax, dword ptr [eax]
0x479A18: mov      edx, dword ptr [ebp - 4]
0x479A1B: mov      dword ptr [edx + 0x14], eax
0x479A1E: jmp      0x479a4b
0x479A20: mov      eax, dword ptr [0x4a4054] ; *0x4A4054->0x4F7D98
0x479A25: mov      eax, dword ptr [eax]
0x479A27: mov      edx, dword ptr [ebp - 4]
0x479A2A: mov      dword ptr [edx + 0x14], eax
0x479A2D: jmp      0x479a4b
0x479A2F: mov      eax, dword ptr [0x4a3f9c] ; *0x4A3F9C->0x4F7DA0
0x479A34: mov      eax, dword ptr [eax]
0x479A36: mov      edx, dword ptr [ebp - 4]
0x479A39: mov      dword ptr [edx + 0x14], eax
0x479A3C: jmp      0x479a4b
0x479A3E: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x479A43: mov      eax, dword ptr [eax]
0x479A45: mov      edx, dword ptr [ebp - 4]
0x479A48: mov      dword ptr [edx + 0x14], eax
0x479A4B: mov      eax, dword ptr [ebp - 4]
0x479A4E: cmp      dword ptr [eax + 0x14], 0
0x479A52: je       0x479a63
0x479A54: mov      eax, dword ptr [ebp - 4]
0x479A57: mov      eax, dword ptr [eax + 0x14]
0x479A5A: mov      bx, 0xfffd
0x479A5E: call     0x403080
0x479A63: pop      ebx
0x479A64: pop      ecx
0x479A65: pop      ecx
0x479A66: pop      ebp
0x479A67: ret      
0x479A68: push     ebp
0x479A69: mov      ebp, esp
0x479A6B: add      esp, -8
0x479A6E: push     ebx
0x479A6F: mov      dword ptr [ebp - 8], edx
0x479A72: mov      dword ptr [ebp - 4], eax
0x479A75: mov      eax, dword ptr [ebp - 8]
0x479A78: call     0x403e88
0x479A7D: xor      eax, eax
0x479A7F: push     ebp
0x479A80: push     0x479ad3
0x479A85: push     dword ptr fs:[eax]
0x479A88: mov      dword ptr fs:[eax], esp
0x479A8B: mov      eax, dword ptr [ebp - 4]
0x479A8E: mov      eax, dword ptr [eax + 0x10]
0x479A91: mov      edx, dword ptr [eax]
0x479A93: call     dword ptr [edx + 0x14]
0x479A96: cmp      eax, 0xa
0x479A99: jl       0x479aa8
0x479A9B: xor      edx, edx
0x479A9D: mov      eax, dword ptr [ebp - 4]
0x479AA0: mov      eax, dword ptr [eax + 0x10]
0x479AA3: mov      ecx, dword ptr [eax]
0x479AA5: call     dword ptr [ecx + 0x44]
0x479AA8: call     0x406154
0x479AAD: mov      ecx, eax
0x479AAF: mov      edx, dword ptr [ebp - 8]
0x479AB2: mov      eax, dword ptr [ebp - 4]
0x479AB5: mov      eax, dword ptr [eax + 0x10]
0x479AB8: mov      ebx, dword ptr [eax]
0x479ABA: call     dword ptr [ebx + 0x38]
0x479ABD: xor      eax, eax
0x479ABF: pop      edx
0x479AC0: pop      ecx
0x479AC1: pop      ecx
0x479AC2: mov      dword ptr fs:[eax], edx
0x479AC5: push     0x479ada
0x479ACA: lea      eax, [ebp - 8]
0x479ACD: call     0x403a58
0x479AD2: ret      
0x479AD3: jmp      0x403518
0x479AD8: jmp      0x479aca
0x479ADA: pop      ebx
0x479ADB: pop      ecx
0x479ADC: pop      ecx
0x479ADD: pop      ebp
0x479ADE: ret      
0x479ADF: nop      
0x479AE0: push     ebp
0x479AE1: mov      ebp, esp
0x479AE3: add      esp, -0x24
0x479AE6: push     ebx
0x479AE7: xor      ebx, ebx
0x479AE9: mov      dword ptr [ebp - 0x24], ebx
0x479AEC: mov      dword ptr [ebp - 0x20], ebx
0x479AEF: mov      dword ptr [ebp - 0x1c], ebx
0x479AF2: mov      dword ptr [ebp - 0xc], ecx
0x479AF5: mov      dword ptr [ebp - 8], edx
0x479AF8: mov      dword ptr [ebp - 4], eax
0x479AFB: mov      eax, dword ptr [ebp - 8]
0x479AFE: call     0x403e88
0x479B03: xor      eax, eax
0x479B05: push     ebp
0x479B06: push     0x479d1a
0x479B0B: push     dword ptr fs:[eax]
0x479B0E: mov      dword ptr fs:[eax], esp
0x479B11: mov      eax, dword ptr [ebp - 8]
0x479B14: call     0x403cd4
0x479B19: mov      dword ptr [ebp - 0x14], eax
0x479B1C: lea      eax, [ebp - 0x1c]
0x479B1F: call     0x403a58
0x479B24: mov      dword ptr [ebp - 0x10], 1
0x479B2B: mov      eax, dword ptr [ebp - 0x10]
0x479B2E: cmp      eax, dword ptr [ebp - 0x14]
0x479B31: jg       0x479c1f
0x479B37: mov      eax, dword ptr [ebp - 8]
0x479B3A: mov      edx, dword ptr [ebp - 0x10]
0x479B3D: cmp      byte ptr [eax + edx - 1], 0x80
0x479B42: jb       0x479b8f
0x479B44: lea      eax, [ebp - 0x24]
0x479B47: mov      edx, dword ptr [ebp - 8]
0x479B4A: mov      ecx, dword ptr [ebp - 0x10]
0x479B4D: mov      dl, byte ptr [edx + ecx - 1]
0x479B51: call     0x403bfc
0x479B56: mov      edx, dword ptr [ebp - 0x24]
0x479B59: lea      eax, [ebp - 0x1c]
0x479B5C: call     0x403cdc
0x479B61: inc      dword ptr [ebp - 0x10]
0x479B64: mov      eax, dword ptr [ebp - 0x10]
0x479B67: cmp      eax, dword ptr [ebp - 0x14]
0x479B6A: jg       0x479c1f
0x479B70: lea      eax, [ebp - 0x24]
0x479B73: mov      edx, dword ptr [ebp - 8]
0x479B76: mov      ecx, dword ptr [ebp - 0x10]
0x479B79: mov      dl, byte ptr [edx + ecx - 1]
0x479B7D: call     0x403bfc
0x479B82: mov      edx, dword ptr [ebp - 0x24]
0x479B85: lea      eax, [ebp - 0x1c]
0x479B88: call     0x403cdc
0x479B8D: jmp      0x479bac
0x479B8F: lea      eax, [ebp - 0x24]
0x479B92: mov      edx, dword ptr [ebp - 8]
0x479B95: mov      ecx, dword ptr [ebp - 0x10]
0x479B98: mov      dl, byte ptr [edx + ecx - 1]
0x479B9C: call     0x403bfc
0x479BA1: mov      edx, dword ptr [ebp - 0x24]
0x479BA4: lea      eax, [ebp - 0x1c]
0x479BA7: call     0x403cdc
0x479BAC: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x479BB1: mov      eax, dword ptr [eax]
0x479BB3: mov      eax, dword ptr [eax + 0x2f4]
0x479BB9: mov      eax, dword ptr [eax + 0x294]
0x479BBF: call     0x44d8b4
0x479BC4: mov      edx, dword ptr [ebp - 0x1c]
0x479BC7: call     0x4181ac
0x479BCC: mov      dword ptr [ebp - 0x18], eax
0x479BCF: cmp      dword ptr [ebp - 0x18], 0x176
0x479BD6: jle      0x479c17
0x479BD8: mov      ecx, dword ptr [ebp - 0xc]
0x479BDB: mov      edx, dword ptr [ebp - 0x1c]
0x479BDE: mov      eax, dword ptr [ebp - 4]
0x479BE1: mov      eax, dword ptr [eax + 0x18]
0x479BE4: mov      ebx, dword ptr [eax]
0x479BE6: call     dword ptr [ebx + 0x38]
0x479BE9: mov      edx, dword ptr [ebp + 8]
0x479BEC: mov      eax, dword ptr [ebp - 4]
0x479BEF: mov      eax, dword ptr [eax + 0x1c]
0x479BF2: call     0x40f588
0x479BF7: lea      eax, [ebp - 8]
0x479BFA: push     eax
0x479BFB: mov      ecx, dword ptr [ebp - 0x14]
0x479BFE: sub      ecx, dword ptr [ebp - 0x10]
0x479C01: mov      edx, dword ptr [ebp - 0x10]
0x479C04: inc      edx
0x479C05: mov      eax, dword ptr [ebp - 8]
0x479C08: call     0x403ed8
0x479C0D: lea      eax, [ebp - 0x1c]
0x479C10: call     0x403a58
0x479C15: jmp      0x479c1f
0x479C17: inc      dword ptr [ebp - 0x10]
0x479C1A: jmp      0x479b2b
0x479C1F: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x479C24: mov      eax, dword ptr [eax]
0x479C26: mov      eax, dword ptr [eax + 0x2f4]
0x479C2C: mov      eax, dword ptr [eax + 0x294]
0x479C32: call     0x44d8b4
0x479C37: call     0x44d104
0x479C3C: cmp      dword ptr [ebp - 0x1c], 0
0x479C40: je       0x479c69
0x479C42: mov      ecx, dword ptr [ebp - 0xc]
0x479C45: mov      edx, dword ptr [ebp - 0x1c]
0x479C48: mov      eax, dword ptr [ebp - 4]
0x479C4B: mov      eax, dword ptr [eax + 0x18]
0x479C4E: mov      ebx, dword ptr [eax]
0x479C50: call     dword ptr [ebx + 0x38]
0x479C53: mov      edx, dword ptr [ebp + 8]
0x479C56: mov      eax, dword ptr [ebp - 4]
0x479C59: mov      eax, dword ptr [eax + 0x1c]
0x479C5C: call     0x40f588
0x479C61: lea      eax, [ebp - 8]
0x479C64: call     0x403a58
0x479C69: mov      eax, dword ptr [ebp - 4]
0x479C6C: mov      eax, dword ptr [eax + 0x18]
0x479C6F: mov      edx, dword ptr [eax]
0x479C71: call     dword ptr [edx + 0x14]
0x479C74: cmp      eax, 0xc8
0x479C79: jle      0x479cb3
0x479C7B: xor      edx, edx
0x479C7D: mov      eax, dword ptr [ebp - 4]
0x479C80: mov      eax, dword ptr [eax + 0x18]
0x479C83: mov      ecx, dword ptr [eax]
0x479C85: call     dword ptr [ecx + 0x44]
0x479C88: xor      edx, edx
0x479C8A: mov      eax, dword ptr [ebp - 4]
0x479C8D: mov      eax, dword ptr [eax + 0x1c]
0x479C90: call     0x40f5c4
0x479C95: mov      eax, dword ptr [ebp - 4]
0x479C98: mov      eax, dword ptr [eax + 0x18]
0x479C9B: mov      edx, dword ptr [eax]
0x479C9D: call     dword ptr [edx + 0x14]
0x479CA0: mov      edx, dword ptr [ebp - 4]
0x479CA3: sub      eax, dword ptr [edx + 0x20]
0x479CA6: cmp      eax, 9
0x479CA9: jge      0x479ccf
0x479CAB: mov      eax, dword ptr [ebp - 4]
0x479CAE: dec      dword ptr [eax + 0x20]
0x479CB1: jmp      0x479ccf
0x479CB3: mov      eax, dword ptr [ebp - 4]
0x479CB6: mov      eax, dword ptr [eax + 0x18]
0x479CB9: mov      edx, dword ptr [eax]
0x479CBB: call     dword ptr [edx + 0x14]
0x479CBE: mov      edx, dword ptr [ebp - 4]
0x479CC1: sub      eax, dword ptr [edx + 0x20]
0x479CC4: cmp      eax, 9
0x479CC7: jle      0x479ccf
0x479CC9: mov      eax, dword ptr [ebp - 4]
0x479CCC: inc      dword ptr [eax + 0x20]
0x479CCF: cmp      dword ptr [ebp - 8], 0
0x479CD3: je       0x479cf7
0x479CD5: mov      eax, dword ptr [ebp + 8]
0x479CD8: push     eax
0x479CD9: lea      eax, [ebp - 0x24]
0x479CDC: mov      ecx, dword ptr [ebp - 8]
0x479CDF: mov      edx, 0x479d30 ; " "
0x479CE4: call     0x403d20
0x479CE9: mov      edx, dword ptr [ebp - 0x24]
0x479CEC: mov      ecx, dword ptr [ebp - 0xc]
0x479CEF: mov      eax, dword ptr [ebp - 4]
0x479CF2: call     0x479ae0
0x479CF7: xor      eax, eax
0x479CF9: pop      edx
0x479CFA: pop      ecx
0x479CFB: pop      ecx
0x479CFC: mov      dword ptr fs:[eax], edx
0x479CFF: push     0x479d21
0x479D04: lea      eax, [ebp - 0x24]
0x479D07: mov      edx, 3
0x479D0C: call     0x403a7c
0x479D11: lea      eax, [ebp - 8]
0x479D14: call     0x403a58
0x479D19: ret      
0x479D1A: jmp      0x403518
0x479D1F: jmp      0x479d04
0x479D21: pop      ebx
0x479D22: mov      esp, ebp
0x479D24: pop      ebp
0x479D25: ret      4
0x479D30: [STR len=1] " "
0x479D34: push     ebp
0x479D35: mov      ebp, esp
0x479D37: add      esp, -0x20
0x479D3A: push     ebx
0x479D3B: xor      ebx, ebx
0x479D3D: mov      dword ptr [ebp - 0x1c], ebx
0x479D40: mov      dword ptr [ebp - 0x10], ebx
0x479D43: mov      dword ptr [ebp - 0xc], ecx
0x479D46: mov      dword ptr [ebp - 8], edx
0x479D49: mov      dword ptr [ebp - 4], eax
0x479D4C: mov      eax, dword ptr [ebp + 0x10]
0x479D4F: call     0x403e88
0x479D54: xor      eax, eax
0x479D56: push     ebp
0x479D57: push     0x479e9f
0x479D5C: push     dword ptr fs:[eax]
0x479D5F: mov      dword ptr fs:[eax], esp
0x479D62: mov      eax, dword ptr [ebp - 4]
0x479D65: call     0x479ebc
0x479D6A: mov      eax, dword ptr [ebp - 8]
0x479D6D: mov      edx, dword ptr [ebp - 4]
0x479D70: mov      dword ptr [edx + 0x28], eax
0x479D73: mov      eax, dword ptr [ebp - 0xc]
0x479D76: mov      edx, dword ptr [ebp - 4]
0x479D79: mov      dword ptr [edx + 0x2c], eax
0x479D7C: mov      eax, dword ptr [ebp - 4]
0x479D7F: xor      edx, edx
0x479D81: mov      dword ptr [eax + 0x30], edx
0x479D84: mov      eax, dword ptr [ebp - 4]
0x479D87: xor      edx, edx
0x479D89: mov      dword ptr [eax + 0x34], edx
0x479D8C: mov      al, byte ptr [ebp + 8]
0x479D8F: mov      edx, dword ptr [ebp - 4]
0x479D92: mov      byte ptr [edx + 0x38], al
0x479D95: mov      eax, dword ptr [ebp + 0xc]
0x479D98: mov      edx, dword ptr [ebp - 4]
0x479D9B: mov      dword ptr [edx + 0x3c], eax
0x479D9E: cmp      dword ptr [ebp + 0x10], 0
0x479DA2: je       0x479e17
0x479DA4: push     0
0x479DA6: lea      eax, [ebp - 0x1c]
0x479DA9: push     eax
0x479DAA: mov      byte ptr [ebp - 0x20], 0x5c
0x479DAE: lea      ecx, [ebp - 0x20]
0x479DB1: lea      edx, [ebp - 0x10]
0x479DB4: mov      eax, dword ptr [ebp + 0x10]
0x479DB7: call     0x452530
0x479DBC: mov      edx, dword ptr [ebp - 0x1c]
0x479DBF: lea      eax, [ebp + 0x10]
0x479DC2: call     0x403af0
0x479DC7: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x479DCC: mov      eax, dword ptr [eax]
0x479DCE: mov      eax, dword ptr [eax + 0x2f4]
0x479DD4: mov      eax, dword ptr [eax + 0x294]
0x479DDA: call     0x44d8b4
0x479DDF: mov      edx, dword ptr [ebp - 0x10]
0x479DE2: call     0x4181ac
0x479DE7: add      eax, 8
0x479DEA: mov      dword ptr [ebp - 0x14], eax
0x479DED: mov      eax, dword ptr [ebp - 0x14]
0x479DF0: mov      edx, dword ptr [ebp - 4]
0x479DF3: cmp      eax, dword ptr [edx + 0x30]
0x479DF6: jle      0x479e01
0x479DF8: mov      eax, dword ptr [ebp - 0x14]
0x479DFB: mov      edx, dword ptr [ebp - 4]
0x479DFE: mov      dword ptr [edx + 0x30], eax
0x479E01: cmp      dword ptr [ebp - 0x10], 0
0x479E05: je       0x479d9e
0x479E07: mov      edx, dword ptr [ebp - 0x10]
0x479E0A: mov      eax, dword ptr [ebp - 4]
0x479E0D: mov      eax, dword ptr [eax + 0x24]
0x479E10: mov      ecx, dword ptr [eax]
0x479E12: call     dword ptr [ecx + 0x34]
0x479E15: jmp      0x479d9e
0x479E17: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x479E1C: mov      eax, dword ptr [eax]
0x479E1E: mov      eax, dword ptr [eax + 0x2f4]
0x479E24: mov      eax, dword ptr [eax + 0x294]
0x479E2A: call     0x44d8b4
0x479E2F: call     0x44d104
0x479E34: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x479E39: mov      eax, dword ptr [eax]
0x479E3B: call     0x43e1f0
0x479E40: mov      edx, 0x479eb8 ; "A"
0x479E45: call     0x4181c8
0x479E4A: mov      ebx, eax
0x479E4C: inc      ebx
0x479E4D: mov      eax, dword ptr [ebp - 4]
0x479E50: mov      eax, dword ptr [eax + 0x24]
0x479E53: mov      edx, dword ptr [eax]
0x479E55: call     dword ptr [edx + 0x14]
0x479E58: imul     ebx, eax
0x479E5B: add      ebx, 6
0x479E5E: mov      eax, dword ptr [ebp - 4]
0x479E61: mov      dword ptr [eax + 0x34], ebx
0x479E64: mov      eax, dword ptr [ebp - 4]
0x479E67: cmp      byte ptr [eax + 0x38], 0
0x479E6B: je       0x479e79
0x479E6D: mov      eax, dword ptr [ebp - 4]
0x479E70: mov      eax, dword ptr [eax + 0x34]
0x479E73: mov      edx, dword ptr [ebp - 4]
0x479E76: sub      dword ptr [edx + 0x2c], eax
0x479E79: xor      eax, eax
0x479E7B: pop      edx
0x479E7C: pop      ecx
0x479E7D: pop      ecx
0x479E7E: mov      dword ptr fs:[eax], edx
0x479E81: push     0x479ea6
0x479E86: lea      eax, [ebp - 0x1c]
0x479E89: call     0x403a58
0x479E8E: lea      eax, [ebp - 0x10]
0x479E91: call     0x403a58
0x479E96: lea      eax, [ebp + 0x10]
0x479E99: call     0x403a58
0x479E9E: ret      
0x479E9F: jmp      0x403518
0x479EA4: jmp      0x479e86
0x479EA6: pop      ebx
0x479EA7: mov      esp, ebp
0x479EA9: pop      ebp
0x479EAA: ret      0xc
0x479EAD: add      byte ptr [eax], al
0x479EAF: add      bh, bh
0x479EB3: inc      dword ptr [ecx]
0x479EB5: add      byte ptr [eax], al
0x479EB7: add      byte ptr [ecx], al
0x479EBA: add      byte ptr [eax], al
0x479EBC: push     ebp
0x479EBD: mov      ebp, esp
0x479EBF: push     ecx
0x479EC0: mov      dword ptr [ebp - 4], eax
0x479EC3: mov      eax, dword ptr [ebp - 4]
0x479EC6: mov      eax, dword ptr [eax + 0x24]
0x479EC9: mov      edx, dword ptr [eax]
0x479ECB: call     dword ptr [edx + 0x40]
0x479ECE: pop      ecx
0x479ECF: pop      ebp
0x479ED0: ret      
0x479ED1: lea      eax, [eax]
0x479ED4: push     ebp
0x479ED5: mov      ebp, esp
0x479ED7: push     ecx
0x479ED8: push     ebx
0x479ED9: mov      dword ptr [ebp - 4], eax
0x479EDC: mov      eax, dword ptr [ebp - 4]
0x479EDF: mov      eax, dword ptr [eax + 0x10]
0x479EE2: mov      edx, dword ptr [eax]
0x479EE4: call     dword ptr [edx + 0x40]
0x479EE7: mov      eax, dword ptr [ebp - 4]
0x479EEA: mov      eax, dword ptr [eax + 0x18]
0x479EED: mov      edx, dword ptr [eax]
0x479EEF: call     dword ptr [edx + 0x40]
0x479EF2: mov      eax, dword ptr [ebp - 4]
0x479EF5: mov      eax, dword ptr [eax + 0x1c]
0x479EF8: mov      bx, 0xffff
0x479EFC: call     0x403080
0x479F01: mov      eax, dword ptr [ebp - 4]
0x479F04: xor      edx, edx
0x479F06: mov      dword ptr [eax + 0x20], edx
0x479F09: pop      ebx
0x479F0A: pop      ecx
0x479F0B: pop      ebp
0x479F0C: ret      
0x479F0D: lea      eax, [eax]
0x479F10: push     ebp
0x479F11: mov      ebp, esp
0x479F13: add      esp, -0x20
0x479F16: push     ebx
0x479F17: xor      ebx, ebx
0x479F19: mov      dword ptr [ebp - 0x1c], ebx
0x479F1C: mov      dword ptr [ebp - 0x18], ebx
0x479F1F: mov      dword ptr [ebp - 0xc], ecx
0x479F22: mov      dword ptr [ebp - 8], edx
0x479F25: mov      dword ptr [ebp - 4], eax
0x479F28: mov      eax, dword ptr [ebp + 8]
0x479F2B: call     0x403e88
0x479F30: xor      eax, eax
0x479F32: push     ebp
0x479F33: push     0x479fdc
0x479F38: push     dword ptr fs:[eax]
0x479F3B: mov      dword ptr fs:[eax], esp
0x479F3E: xor      eax, eax
0x479F40: mov      dword ptr [ebp - 0x14], eax
0x479F43: xor      eax, eax
0x479F45: mov      dword ptr [ebp - 0x10], eax
0x479F48: cmp      dword ptr [ebp + 8], 0
0x479F4C: je       0x479fb9
0x479F4E: push     0
0x479F50: lea      eax, [ebp - 0x1c]
0x479F53: push     eax
0x479F54: mov      byte ptr [ebp - 0x20], 0x5c
0x479F58: lea      ecx, [ebp - 0x20]
0x479F5B: lea      edx, [ebp - 0x18]
0x479F5E: mov      eax, dword ptr [ebp + 8]
0x479F61: call     0x452530
0x479F66: mov      edx, dword ptr [ebp - 0x1c]
0x479F69: lea      eax, [ebp + 8]
0x479F6C: call     0x403af0
0x479F71: mov      eax, dword ptr [ebp + 0x10]
0x479F74: push     eax
0x479F75: mov      eax, dword ptr [ebp + 0xc]
0x479F78: push     eax
0x479F79: mov      eax, dword ptr [ebp - 0x18]
0x479F7C: push     eax
0x479F7D: mov      eax, dword ptr [ebp - 4]
0x479F80: call     0x44d8b4
0x479F85: mov      edx, dword ptr [ebp - 0x18]
0x479F88: call     0x4181ac
0x479F8D: sar      eax, 1
0x479F8F: jns      0x479f94
0x479F91: adc      eax, 0
0x479F94: mov      edx, dword ptr [ebp - 8]
0x479F97: sub      edx, eax
0x479F99: mov      ecx, dword ptr [ebp - 0x14]
0x479F9C: shl      ecx, 2
0x479F9F: lea      ecx, [ecx + ecx*2]
0x479FA2: add      ecx, dword ptr [ebp - 0xc]
0x479FA5: mov      eax, dword ptr [ebp - 4]
0x479FA8: call     0x4a09a0
0x479FAD: inc      dword ptr [ebp - 0x14]
0x479FB0: inc      dword ptr [ebp - 0x10]
0x479FB3: cmp      dword ptr [ebp - 0x10], 0xb
0x479FB7: jne      0x479f48
0x479FB9: xor      eax, eax
0x479FBB: pop      edx
0x479FBC: pop      ecx
0x479FBD: pop      ecx
0x479FBE: mov      dword ptr fs:[eax], edx
0x479FC1: push     0x479fe3
0x479FC6: lea      eax, [ebp - 0x1c]
0x479FC9: mov      edx, 2
0x479FCE: call     0x403a7c
0x479FD3: lea      eax, [ebp + 8]
0x479FD6: call     0x403a58
0x479FDB: ret      
0x479FDC: jmp      0x403518
0x479FE1: jmp      0x479fc6
0x479FE3: pop      ebx
0x479FE4: mov      esp, ebp
0x479FE6: pop      ebp
0x479FE7: ret      0xc
0x479FEA: mov      eax, eax
0x479FEC: push     ebp
0x479FED: mov      ebp, esp
0x479FEF: add      esp, -0x6c
0x479FF2: push     ebx
0x479FF3: xor      ecx, ecx
0x479FF5: mov      dword ptr [ebp - 0x3c], ecx
0x479FF8: mov      dword ptr [ebp - 0x18], ecx
0x479FFB: mov      dword ptr [ebp - 8], edx
0x479FFE: mov      dword ptr [ebp - 4], eax
0x47A001: xor      eax, eax
0x47A003: push     ebp
0x47A004: push     0x47a573
0x47A009: push     dword ptr fs:[eax]
0x47A00C: mov      dword ptr fs:[eax], esp
0x47A00F: xor      edx, edx
0x47A011: mov      eax, dword ptr [ebp - 8]
0x47A014: call     0x44d430
0x47A019: mov      eax, dword ptr [ebp - 4]
0x47A01C: cmp      dword ptr [eax + 0x14], 0
0x47A020: je       0x47a034
0x47A022: mov      eax, dword ptr [ebp - 4]
0x47A025: mov      eax, dword ptr [eax + 0x14]
0x47A028: mov      edx, dword ptr [ebp - 8]
0x47A02B: mov      bx, 0xfff6
0x47A02F: call     0x403080
0x47A034: call     0x406154
0x47A039: mov      edx, dword ptr [ebp - 4]
0x47A03C: sub      eax, dword ptr [edx + 4]
0x47A03F: cmp      eax, 0x3e8
0x47A044: jbe      0x47a065
0x47A046: call     0x406154
0x47A04B: mov      edx, dword ptr [ebp - 4]
0x47A04E: mov      dword ptr [edx + 4], eax
0x47A051: mov      eax, dword ptr [ebp - 4]
0x47A054: mov      eax, dword ptr [eax + 8]
0x47A057: mov      edx, dword ptr [ebp - 4]
0x47A05A: mov      dword ptr [edx + 0xc], eax
0x47A05D: mov      eax, dword ptr [ebp - 4]
0x47A060: xor      edx, edx
0x47A062: mov      dword ptr [eax + 8], edx
0x47A065: mov      eax, dword ptr [ebp - 4]
0x47A068: inc      dword ptr [eax + 8]
0x47A06B: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x47A070: cmp      dword ptr [eax], 0
0x47A073: je       0x47a555
0x47A079: mov      eax, dword ptr [ebp - 4]
0x47A07C: mov      eax, dword ptr [eax + 0x14]
0x47A07F: mov      edx, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x47A085: cmp      eax, dword ptr [edx]
0x47A087: jne      0x47a555
0x47A08D: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x47A092: mov      eax, dword ptr [eax]
0x47A094: mov      eax, dword ptr [eax + 0x5a854]
0x47A09A: mov      eax, dword ptr [eax + 8]
0x47A09D: dec      eax
0x47A09E: test     eax, eax
0x47A0A0: jl       0x47a242
0x47A0A6: inc      eax
0x47A0A7: mov      dword ptr [ebp - 0x20], eax
0x47A0AA: mov      dword ptr [ebp - 0x10], 0
0x47A0B1: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x47A0B6: mov      eax, dword ptr [eax]
0x47A0B8: mov      eax, dword ptr [eax + 0x5a854]
0x47A0BE: mov      edx, dword ptr [ebp - 0x10]
0x47A0C1: call     0x40f6a0
0x47A0C6: mov      dword ptr [ebp - 0x14], eax
0x47A0C9: mov      eax, dword ptr [ebp - 0x14]
0x47A0CC: cmp      byte ptr [eax + 0x134], 0
0x47A0D3: jne      0x47a0e5
0x47A0D5: mov      eax, dword ptr [ebp - 0x14]
0x47A0D8: cmp      byte ptr [eax + 0x135], 0
0x47A0DF: je       0x47a236
0x47A0E5: mov      eax, dword ptr [ebp - 0x14]
0x47A0E8: cmp      byte ptr [eax + 0x20], 0
0x47A0EC: jne      0x47a236
0x47A0F2: mov      eax, dword ptr [ebp - 0x14]
0x47A0F5: cmp      byte ptr [eax + 0x135], 0
0x47A0FC: je       0x47a137
0x47A0FE: call     0x406154
0x47A103: mov      edx, dword ptr [ebp - 0x14]
0x47A106: sub      eax, dword ptr [edx + 0x138]
0x47A10C: xor      edx, edx
0x47A10E: push     edx
0x47A10F: push     eax
0x47A110: mov      eax, dword ptr [ebp - 0x14]
0x47A113: mov      eax, dword ptr [eax + 0x13c]
0x47A119: cdq      
0x47A11A: cmp      edx, dword ptr [esp + 4]
0x47A11E: jne      0x47a129
0x47A120: cmp      eax, dword ptr [esp]
0x47A123: pop      edx
0x47A124: pop      eax
0x47A125: jae      0x47a137
0x47A127: jmp      0x47a12d
0x47A129: pop      edx
0x47A12A: pop      eax
0x47A12B: jge      0x47a137
0x47A12D: mov      eax, dword ptr [ebp - 0x14]
0x47A130: mov      byte ptr [eax + 0x135], 0
0x47A137: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47A13C: mov      eax, dword ptr [eax]
0x47A13E: mov      eax, dword ptr [eax + 0x3c0]
0x47A144: xor      edx, edx
0x47A146: call     0x45351c
0x47A14B: mov      dword ptr [ebp - 0x1c], eax
0x47A14E: cmp      dword ptr [ebp - 0x1c], 0
0x47A152: je       0x47a197
0x47A154: lea      edx, [ebp - 0x60]
0x47A157: mov      eax, dword ptr [ebp - 0x1c]
0x47A15A: call     0x44d8ec
0x47A15F: lea      eax, [ebp - 0x60]
0x47A162: push     eax
0x47A163: mov      eax, dword ptr [ebp - 0x1c]
0x47A166: push     eax
0x47A167: push     1
0x47A169: mov      eax, dword ptr [ebp - 0x1c]
0x47A16C: call     0x44dc60
0x47A171: sar      eax, 1
0x47A173: jns      0x47a178
0x47A175: adc      eax, 0
0x47A178: mov      edx, dword ptr [ebp - 0x14]
0x47A17B: mov      edx, dword ptr [edx + 0x8c]
0x47A181: sub      edx, eax
0x47A183: mov      eax, dword ptr [ebp - 0x14]
0x47A186: mov      ecx, dword ptr [eax + 0x90]
0x47A18C: sub      ecx, 0xa
0x47A18F: mov      eax, dword ptr [ebp - 8]
0x47A192: call     0x44d6cc
0x47A197: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47A19C: mov      eax, dword ptr [eax]
0x47A19E: mov      eax, dword ptr [eax + 0x3c0]
0x47A1A4: mov      edx, 1
0x47A1A9: call     0x45351c
0x47A1AE: mov      dword ptr [ebp - 0x1c], eax
0x47A1B1: cmp      dword ptr [ebp - 0x1c], 0
0x47A1B5: je       0x47a236
0x47A1B7: lea      edx, [ebp - 0x50]
0x47A1BA: mov      eax, dword ptr [ebp - 0x1c]
0x47A1BD: call     0x44d8ec
0x47A1C2: mov      eax, dword ptr [ebp - 0x14]
0x47A1C5: cmp      word ptr [eax + 0x40], 0
0x47A1CA: jbe      0x47a1fe
0x47A1CC: mov      eax, dword ptr [ebp - 0x48]
0x47A1CF: sub      eax, dword ptr [ebp - 0x50]
0x47A1D2: mov      dword ptr [ebp - 0x64], eax
0x47A1D5: fild     dword ptr [ebp - 0x64]
0x47A1D8: mov      eax, dword ptr [ebp - 0x14]
0x47A1DB: movzx    eax, word ptr [eax + 0x40]
0x47A1DF: mov      dword ptr [ebp - 0x68], eax
0x47A1E2: fild     dword ptr [ebp - 0x68]
0x47A1E5: fdivp    st(1)
0x47A1E7: mov      eax, dword ptr [ebp - 0x14]
0x47A1EA: movzx    eax, word ptr [eax + 0x3c]
0x47A1EE: mov      dword ptr [ebp - 0x6c], eax
0x47A1F1: fild     dword ptr [ebp - 0x6c]
0x47A1F4: fmulp    st(1)
0x47A1F6: call     0x4029a0
0x47A1FB: mov      dword ptr [ebp - 0x48], eax
0x47A1FE: lea      eax, [ebp - 0x50]
0x47A201: push     eax
0x47A202: mov      eax, dword ptr [ebp - 0x1c]
0x47A205: push     eax
0x47A206: push     1
0x47A208: mov      eax, dword ptr [ebp - 0x1c]
0x47A20B: call     0x44dc60
0x47A210: sar      eax, 1
0x47A212: jns      0x47a217
0x47A214: adc      eax, 0
0x47A217: mov      edx, dword ptr [ebp - 0x14]
0x47A21A: mov      edx, dword ptr [edx + 0x8c]
0x47A220: sub      edx, eax
0x47A222: mov      eax, dword ptr [ebp - 0x14]
0x47A225: mov      ecx, dword ptr [eax + 0x90]
0x47A22B: sub      ecx, 0xa
0x47A22E: mov      eax, dword ptr [ebp - 8]
0x47A231: call     0x44d6cc
0x47A236: inc      dword ptr [ebp - 0x10]
0x47A239: dec      dword ptr [ebp - 0x20]
0x47A23C: jne      0x47a0b1
0x47A242: push     1
0x47A244: mov      eax, dword ptr [ebp - 8]
0x47A247: call     0x44d8b4
0x47A24C: call     0x41834c
0x47A251: push     eax
0x47A252: call     0x406434
0x47A257: mov      eax, dword ptr [0x4a3f10] ; *0x4A3F10->0x4F94A8
0x47A25C: cmp      dword ptr [eax], 0
0x47A25F: je       0x47a2d7
0x47A261: mov      edx, dword ptr [0x4a3f10] ; *0x4A3F10->0x4F94A8
0x47A267: mov      edx, dword ptr [edx]
0x47A269: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x47A26E: mov      eax, dword ptr [eax]
0x47A270: call     0x472ec0
0x47A275: test     al, al
0x47A277: je       0x47a2d7
0x47A279: mov      eax, dword ptr [0x4a3f10] ; *0x4A3F10->0x4F94A8
0x47A27E: mov      eax, dword ptr [eax]
0x47A280: push     dword ptr [eax + 0x24]
0x47A283: push     0x47a588 ; "\"
0x47A288: mov      eax, dword ptr [0x4a3f10] ; *0x4A3F10->0x4F94A8
0x47A28D: mov      eax, dword ptr [eax]
0x47A28F: push     dword ptr [eax + 0x28]
0x47A292: lea      eax, [ebp - 0x18]
0x47A295: mov      edx, 3
0x47A29A: call     0x403d94
0x47A29F: push     ebp
0x47A2A0: mov      eax, dword ptr [0x4a3f10] ; *0x4A3F10->0x4F94A8
0x47A2A5: mov      eax, dword ptr [eax]
0x47A2A7: mov      eax, dword ptr [eax + 0x2c]
0x47A2AA: push     eax
0x47A2AB: push     0
0x47A2AD: mov      eax, dword ptr [ebp - 0x18]
0x47A2B0: push     eax
0x47A2B1: mov      eax, dword ptr [0x4a3f10] ; *0x4A3F10->0x4F94A8
0x47A2B6: mov      eax, dword ptr [eax]
0x47A2B8: mov      ecx, dword ptr [eax + 0x90]
0x47A2BE: add      ecx, 0x1e
0x47A2C1: mov      eax, dword ptr [0x4a3f10] ; *0x4A3F10->0x4F94A8
0x47A2C6: mov      eax, dword ptr [eax]
0x47A2C8: mov      edx, dword ptr [eax + 0x8c]
0x47A2CE: mov      eax, dword ptr [ebp - 8]
0x47A2D1: call     0x479f10
0x47A2D6: pop      ecx
0x47A2D7: mov      eax, dword ptr [0x4a3ed0] ; *0x4A3ED0->0x4F94B1
0x47A2DC: cmp      byte ptr [eax], 0
0x47A2DF: je       0x47a33f
0x47A2E1: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x47A2E6: mov      eax, dword ptr [eax]
0x47A2E8: push     dword ptr [eax + 0x24]
0x47A2EB: push     0x47a588 ; "\"
0x47A2F0: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x47A2F5: mov      eax, dword ptr [eax]
0x47A2F7: push     dword ptr [eax + 0x28]
0x47A2FA: lea      eax, [ebp - 0x18]
0x47A2FD: mov      edx, 3
0x47A302: call     0x403d94
0x47A307: push     ebp
0x47A308: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x47A30D: mov      eax, dword ptr [eax]
0x47A30F: mov      eax, dword ptr [eax + 0x2c]
0x47A312: push     eax
0x47A313: push     0
0x47A315: mov      eax, dword ptr [ebp - 0x18]
0x47A318: push     eax
0x47A319: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x47A31E: mov      eax, dword ptr [eax]
0x47A320: mov      ecx, dword ptr [eax + 0x90]
0x47A326: add      ecx, 0x1e
0x47A329: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x47A32E: mov      eax, dword ptr [eax]
0x47A330: mov      edx, dword ptr [eax + 0x8c]
0x47A336: mov      eax, dword ptr [ebp - 8]
0x47A339: call     0x479f10
0x47A33E: pop      ecx
0x47A33F: mov      eax, dword ptr [ebp - 8]
0x47A342: call     0x44d8b4
0x47A347: mov      eax, dword ptr [eax + 0xc]
0x47A34A: mov      edx, 0xffffff
0x47A34F: call     0x4173fc
0x47A354: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x47A359: mov      eax, dword ptr [eax]
0x47A35B: mov      eax, dword ptr [eax + 0x5a854]
0x47A361: mov      eax, dword ptr [eax + 8]
0x47A364: dec      eax
0x47A365: test     eax, eax
0x47A367: jl       0x47a4a9
0x47A36D: inc      eax
0x47A36E: mov      dword ptr [ebp - 0x20], eax
0x47A371: mov      dword ptr [ebp - 0x10], 0
0x47A378: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x47A37D: mov      eax, dword ptr [eax]
0x47A37F: mov      eax, dword ptr [eax + 0x5a854]
0x47A385: mov      edx, dword ptr [ebp - 0x10]
0x47A388: call     0x40f6a0
0x47A38D: mov      dword ptr [ebp - 0x14], eax
0x47A390: mov      eax, dword ptr [ebp - 0x14]
0x47A393: cmp      dword ptr [eax + 0x60], 0
0x47A397: je       0x47a49d
0x47A39D: call     0x406154
0x47A3A2: mov      edx, dword ptr [ebp - 0x14]
0x47A3A5: sub      eax, dword ptr [edx + 0x88]
0x47A3AB: cmp      eax, 0xfa0
0x47A3B0: jae      0x47a492
0x47A3B6: mov      eax, dword ptr [ebp - 0x14]
0x47A3B9: mov      eax, dword ptr [eax + 0x94]
0x47A3BF: dec      eax
0x47A3C0: test     eax, eax
0x47A3C2: jl       0x47a49d
0x47A3C8: inc      eax
0x47A3C9: mov      dword ptr [ebp - 0x24], eax
0x47A3CC: mov      dword ptr [ebp - 0xc], 0
0x47A3D3: mov      eax, dword ptr [ebp - 0x14]
0x47A3D6: cmp      byte ptr [eax + 0x20], 0
0x47A3DA: je       0x47a431
0x47A3DC: push     0x808080
0x47A3E1: push     0
0x47A3E3: mov      eax, dword ptr [ebp - 0xc]
0x47A3E6: mov      edx, dword ptr [ebp - 0x14]
0x47A3E9: mov      eax, dword ptr [edx + eax*4 + 0x60]
0x47A3ED: push     eax
0x47A3EE: mov      eax, dword ptr [ebp - 0x14]
0x47A3F1: mov      eax, dword ptr [eax + 0x94]
0x47A3F7: shl      eax, 4
0x47A3FA: mov      edx, dword ptr [ebp - 0x14]
0x47A3FD: mov      ecx, dword ptr [edx + 0x90]
0x47A403: sub      ecx, eax
0x47A405: imul     eax, dword ptr [ebp - 0xc], 0xe
0x47A409: add      ecx, eax
0x47A40B: mov      eax, dword ptr [ebp - 0xc]
0x47A40E: mov      edx, dword ptr [ebp - 0x14]
0x47A411: mov      eax, dword ptr [edx + eax*4 + 0x74]
0x47A415: sar      eax, 1
0x47A417: jns      0x47a41c
0x47A419: adc      eax, 0
0x47A41C: mov      edx, dword ptr [ebp - 0x14]
0x47A41F: mov      edx, dword ptr [edx + 0x8c]
0x47A425: sub      edx, eax
0x47A427: mov      eax, dword ptr [ebp - 8]
0x47A42A: call     0x4a09a0
0x47A42F: jmp      0x47a484
0x47A431: push     0xffffff
0x47A436: push     0
0x47A438: mov      eax, dword ptr [ebp - 0xc]
0x47A43B: mov      edx, dword ptr [ebp - 0x14]
0x47A43E: mov      eax, dword ptr [edx + eax*4 + 0x60]
0x47A442: push     eax
0x47A443: mov      eax, dword ptr [ebp - 0x14]
0x47A446: mov      eax, dword ptr [eax + 0x94]
0x47A44C: shl      eax, 4
0x47A44F: mov      edx, dword ptr [ebp - 0x14]
0x47A452: mov      ecx, dword ptr [edx + 0x90]
0x47A458: sub      ecx, eax
0x47A45A: imul     eax, dword ptr [ebp - 0xc], 0xe
0x47A45E: add      ecx, eax
0x47A460: mov      eax, dword ptr [ebp - 0xc]
0x47A463: mov      edx, dword ptr [ebp - 0x14]
0x47A466: mov      eax, dword ptr [edx + eax*4 + 0x74]
0x47A46A: sar      eax, 1
0x47A46C: jns      0x47a471
0x47A46E: adc      eax, 0
0x47A471: mov      edx, dword ptr [ebp - 0x14]
0x47A474: mov      edx, dword ptr [edx + 0x8c]
0x47A47A: sub      edx, eax
0x47A47C: mov      eax, dword ptr [ebp - 8]
0x47A47F: call     0x4a09a0
0x47A484: inc      dword ptr [ebp - 0xc]
0x47A487: dec      dword ptr [ebp - 0x24]
0x47A48A: jne      0x47a3d3
0x47A490: jmp      0x47a49d
0x47A492: mov      eax, dword ptr [ebp - 0x14]
0x47A495: add      eax, 0x60
0x47A498: call     0x403a58
0x47A49D: inc      dword ptr [ebp - 0x10]
0x47A4A0: dec      dword ptr [ebp - 0x20]
0x47A4A3: jne      0x47a378
0x47A4A9: mov      eax, dword ptr [0x4a40f8] ; *0x4A40F8->0x4F9388
0x47A4AE: test     byte ptr [eax], 4
0x47A4B1: je       0x47a4cb
0x47A4B3: push     0xffffff
0x47A4B8: push     0
0x47A4BA: push     0x47a594 ; "攻城区域"
0x47A4BF: xor      ecx, ecx
0x47A4C1: xor      edx, edx
0x47A4C3: mov      eax, dword ptr [ebp - 8]
0x47A4C6: call     0x4a09a0
0x47A4CB: mov      eax, dword ptr [ebp - 8]
0x47A4CE: call     0x44d8b4
0x47A4D3: call     0x44d104
0x47A4D8: xor      eax, eax
0x47A4DA: mov      dword ptr [ebp - 0x10], eax
0x47A4DD: xor      eax, eax
0x47A4DF: mov      dword ptr [ebp - 0xc], eax
0x47A4E2: mov      ecx, dword ptr [ebp - 0xc]
0x47A4E5: mov      eax, 1
0x47A4EA: shr      eax, cl
0x47A4EC: mov      edx, dword ptr [0x4a40f8] ; *0x4A40F8->0x4F9388
0x47A4F2: and      eax, dword ptr [edx]
0x47A4F4: je       0x47a54c
0x47A4F6: mov      edx, dword ptr [ebp - 0xc]
0x47A4F9: add      edx, 0x96
0x47A4FF: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47A504: mov      eax, dword ptr [eax]
0x47A506: mov      eax, dword ptr [eax + 0x308]
0x47A50C: call     0x45351c
0x47A511: mov      dword ptr [ebp - 0x1c], eax
0x47A514: cmp      dword ptr [ebp - 0x1c], 0
0x47A518: je       0x47a54c
0x47A51A: mov      eax, dword ptr [ebp - 0x1c]
0x47A51D: call     0x44dc60
0x47A522: add      dword ptr [ebp - 0x10], eax
0x47A525: lea      edx, [ebp - 0x60]
0x47A528: mov      eax, dword ptr [ebp - 0x1c]
0x47A52B: call     0x44d8ec
0x47A530: lea      eax, [ebp - 0x60]
0x47A533: push     eax
0x47A534: mov      eax, dword ptr [ebp - 0x1c]
0x47A537: push     eax
0x47A538: push     1
0x47A53A: mov      edx, 0x320
0x47A53F: sub      edx, dword ptr [ebp - 0x10]
0x47A542: xor      ecx, ecx
0x47A544: mov      eax, dword ptr [ebp - 8]
0x47A547: call     0x44d6cc
0x47A54C: inc      dword ptr [ebp - 0xc]
0x47A54F: cmp      dword ptr [ebp - 0xc], 0x10
0x47A553: jne      0x47a4e2
0x47A555: xor      eax, eax
0x47A557: pop      edx
0x47A558: pop      ecx
0x47A559: pop      ecx
0x47A55A: mov      dword ptr fs:[eax], edx
0x47A55D: push     0x47a57a
0x47A562: lea      eax, [ebp - 0x3c]
0x47A565: call     0x403a58
0x47A56A: lea      eax, [ebp - 0x18]
0x47A56D: call     0x403a58
0x47A572: ret      
0x47A573: jmp      0x403518
0x47A578: jmp      0x47a562
0x47A57A: pop      ebx
0x47A57B: mov      esp, ebp
0x47A57D: pop      ebp
0x47A57E: ret      
0x47A57F: add      bh, bh
0x47A583: inc      dword ptr [ecx]
0x47A585: add      byte ptr [eax], al
0x47A587: add      byte ptr [eax + eax], bl
0x47A58B: add      bh, bh
0x47A58F: dec      dword ptr [eax]
0x47A591: add      byte ptr [eax], al
0x47A593: add      byte ptr [ecx - 0x38384c5b], bh
0x47A599: clc      
0x47A59A: sal      edx, cl
0x47A59C: add      byte ptr [eax], al
0x47A59E: add      byte ptr [eax], al
0x47A5A0: push     ebp
0x47A5A1: mov      ebp, esp
0x47A5A3: add      esp, -0x1c
0x47A5A6: push     ebx
0x47A5A7: xor      ecx, ecx
0x47A5A9: mov      dword ptr [ebp - 0x1c], ecx
0x47A5AC: mov      dword ptr [ebp - 8], edx
0x47A5AF: mov      dword ptr [ebp - 4], eax
0x47A5B2: xor      eax, eax
0x47A5B4: push     ebp
0x47A5B5: push     0x47a6b5
0x47A5BA: push     dword ptr fs:[eax]
0x47A5BD: mov      dword ptr fs:[eax], esp
0x47A5C0: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x47A5C5: cmp      dword ptr [eax], 0
0x47A5C8: je       0x47a69f
0x47A5CE: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x47A5D3: mov      eax, dword ptr [eax]
0x47A5D5: mov      edx, dword ptr [ebp - 4]
0x47A5D8: cmp      eax, dword ptr [edx + 0x14]
0x47A5DB: jne      0x47a69f
0x47A5E1: push     1
0x47A5E3: mov      eax, dword ptr [ebp - 8]
0x47A5E6: call     0x44d8b4
0x47A5EB: call     0x41834c
0x47A5F0: push     eax
0x47A5F1: call     0x406434
0x47A5F6: mov      eax, dword ptr [ebp - 4]
0x47A5F9: mov      eax, dword ptr [eax + 0x10]
0x47A5FC: mov      edx, dword ptr [eax]
0x47A5FE: call     dword ptr [edx + 0x14]
0x47A601: test     eax, eax
0x47A603: jle      0x47a692
0x47A609: mov      dword ptr [ebp - 0x10], 0x1e
0x47A610: mov      dword ptr [ebp - 0x14], 0x28
0x47A617: mov      eax, dword ptr [ebp - 4]
0x47A61A: mov      eax, dword ptr [eax + 0x10]
0x47A61D: mov      edx, dword ptr [eax]
0x47A61F: call     dword ptr [edx + 0x14]
0x47A622: dec      eax
0x47A623: test     eax, eax
0x47A625: jl       0x47a668
0x47A627: inc      eax
0x47A628: mov      dword ptr [ebp - 0x18], eax
0x47A62B: mov      dword ptr [ebp - 0xc], 0
0x47A632: push     0x8000
0x47A637: push     0
0x47A639: lea      ecx, [ebp - 0x1c]
0x47A63C: mov      edx, dword ptr [ebp - 0xc]
0x47A63F: mov      eax, dword ptr [ebp - 4]
0x47A642: mov      eax, dword ptr [eax + 0x10]
0x47A645: mov      ebx, dword ptr [eax]
0x47A647: call     dword ptr [ebx + 0xc]
0x47A64A: mov      eax, dword ptr [ebp - 0x1c]
0x47A64D: push     eax
0x47A64E: mov      ecx, dword ptr [ebp - 0x14]
0x47A651: mov      edx, dword ptr [ebp - 0x10]
0x47A654: mov      eax, dword ptr [ebp - 8]
0x47A657: call     0x4a09a0
0x47A65C: add      dword ptr [ebp - 0x14], 0x10
0x47A660: inc      dword ptr [ebp - 0xc]
0x47A663: dec      dword ptr [ebp - 0x18]
0x47A666: jne      0x47a632
0x47A668: xor      edx, edx
0x47A66A: mov      eax, dword ptr [ebp - 4]
0x47A66D: mov      eax, dword ptr [eax + 0x10]
0x47A670: mov      ecx, dword ptr [eax]
0x47A672: call     dword ptr [ecx + 0x18]
0x47A675: push     eax
0x47A676: call     0x406154
0x47A67B: pop      edx
0x47A67C: sub      eax, edx
0x47A67E: cmp      eax, 0xbb8
0x47A683: jb       0x47a692
0x47A685: xor      edx, edx
0x47A687: mov      eax, dword ptr [ebp - 4]
0x47A68A: mov      eax, dword ptr [eax + 0x10]
0x47A68D: mov      ecx, dword ptr [eax]
0x47A68F: call     dword ptr [ecx + 0x44]
0x47A692: mov      eax, dword ptr [ebp - 8]
0x47A695: call     0x44d8b4
0x47A69A: call     0x44d104
0x47A69F: xor      eax, eax
0x47A6A1: pop      edx
0x47A6A2: pop      ecx
0x47A6A3: pop      ecx
0x47A6A4: mov      dword ptr fs:[eax], edx
0x47A6A7: push     0x47a6bc
0x47A6AC: lea      eax, [ebp - 0x1c]
0x47A6AF: call     0x403a58
0x47A6B4: ret      
0x47A6B5: jmp      0x403518
0x47A6BA: jmp      0x47a6ac
0x47A6BC: pop      ebx
0x47A6BD: mov      esp, ebp
0x47A6BF: pop      ebp
0x47A6C0: ret      
0x47A6C1: lea      eax, [eax]
0x47A6C4: push     ebp
0x47A6C5: mov      ebp, esp
0x47A6C7: mov      ecx, 9
0x47A6CC: push     0
0x47A6CE: push     0
0x47A6D0: dec      ecx
0x47A6D1: jne      0x47a6cc
0x47A6D3: push     ecx
0x47A6D4: push     ebx
0x47A6D5: mov      dword ptr [ebp - 8], edx
0x47A6D8: mov      dword ptr [ebp - 4], eax
0x47A6DB: xor      eax, eax
0x47A6DD: push     ebp
0x47A6DE: push     0x47aa4b
0x47A6E3: push     dword ptr fs:[eax]
0x47A6E6: mov      dword ptr fs:[eax], esp
0x47A6E9: mov      eax, dword ptr [ebp - 4]
0x47A6EC: mov      eax, dword ptr [eax + 0x24]
0x47A6EF: mov      edx, dword ptr [eax]
0x47A6F1: call     dword ptr [edx + 0x14]
0x47A6F4: test     eax, eax
0x47A6F6: jle      0x47a7d2
0x47A6FC: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47A701: mov      eax, dword ptr [eax]
0x47A703: mov      eax, dword ptr [eax + 0x308]
0x47A709: mov      edx, 0x18a
0x47A70E: call     0x45351c
0x47A713: mov      dword ptr [ebp - 0xc], eax
0x47A716: cmp      dword ptr [ebp - 0xc], 0
0x47A71A: je       0x47a7d2
0x47A720: mov      eax, dword ptr [ebp - 0xc]
0x47A723: call     0x44dc60
0x47A728: mov      edx, dword ptr [ebp - 4]
0x47A72B: cmp      eax, dword ptr [edx + 0x30]
0x47A72E: jge      0x47a73e
0x47A730: mov      eax, dword ptr [ebp - 0xc]
0x47A733: call     0x44dc60
0x47A738: mov      edx, dword ptr [ebp - 4]
0x47A73B: mov      dword ptr [edx + 0x30], eax
0x47A73E: mov      eax, dword ptr [ebp - 0xc]
0x47A741: call     0x44d944
0x47A746: mov      edx, dword ptr [ebp - 4]
0x47A749: cmp      eax, dword ptr [edx + 0x34]
0x47A74C: jge      0x47a75c
0x47A74E: mov      eax, dword ptr [ebp - 0xc]
0x47A751: call     0x44d944
0x47A756: mov      edx, dword ptr [ebp - 4]
0x47A759: mov      dword ptr [edx + 0x34], eax
0x47A75C: mov      eax, dword ptr [ebp - 4]
0x47A75F: mov      eax, dword ptr [eax + 0x28]
0x47A762: mov      edx, dword ptr [ebp - 4]
0x47A765: add      eax, dword ptr [edx + 0x30]
0x47A768: cmp      eax, 0x320
0x47A76D: jle      0x47a77f
0x47A76F: mov      eax, 0x320
0x47A774: mov      edx, dword ptr [ebp - 4]
0x47A777: sub      eax, dword ptr [edx + 0x30]
0x47A77A: mov      dword ptr [ebp - 0x14], eax
0x47A77D: jmp      0x47a788
0x47A77F: mov      eax, dword ptr [ebp - 4]
0x47A782: mov      eax, dword ptr [eax + 0x28]
0x47A785: mov      dword ptr [ebp - 0x14], eax
0x47A788: mov      eax, dword ptr [ebp - 4]
0x47A78B: cmp      dword ptr [eax + 0x2c], 0
0x47A78F: jge      0x47a798
0x47A791: xor      eax, eax
0x47A793: mov      dword ptr [ebp - 0x18], eax
0x47A796: jmp      0x47a7a1
0x47A798: mov      eax, dword ptr [ebp - 4]
0x47A79B: mov      eax, dword ptr [eax + 0x2c]
0x47A79E: mov      dword ptr [ebp - 0x18], eax
0x47A7A1: cmp      dword ptr [ebp - 0x14], 0
0x47A7A5: jge      0x47a7ac
0x47A7A7: xor      eax, eax
0x47A7A9: mov      dword ptr [ebp - 0x14], eax
0x47A7AC: mov      eax, dword ptr [ebp - 0xc]
0x47A7AF: push     eax
0x47A7B0: push     0
0x47A7B2: push     0
0x47A7B4: mov      eax, dword ptr [ebp - 4]
0x47A7B7: mov      eax, dword ptr [eax + 0x30]
0x47A7BA: push     eax
0x47A7BB: mov      eax, dword ptr [ebp - 4]
0x47A7BE: mov      eax, dword ptr [eax + 0x34]
0x47A7C1: push     eax
0x47A7C2: push     0
0x47A7C4: mov      ecx, dword ptr [ebp - 0x18]
0x47A7C7: mov      edx, dword ptr [ebp - 0x14]
0x47A7CA: mov      eax, dword ptr [ebp - 8]
0x47A7CD: call     0x4555bc
0x47A7D2: push     1
0x47A7D4: mov      eax, dword ptr [ebp - 8]
0x47A7D7: call     0x44d8b4
0x47A7DC: call     0x41834c
0x47A7E1: push     eax
0x47A7E2: call     0x406434
0x47A7E7: mov      eax, dword ptr [ebp - 4]
0x47A7EA: mov      eax, dword ptr [eax + 0x24]
0x47A7ED: mov      edx, dword ptr [eax]
0x47A7EF: call     dword ptr [edx + 0x14]
0x47A7F2: test     eax, eax
0x47A7F4: jle      0x47a87b
0x47A7FA: mov      eax, dword ptr [ebp - 8]
0x47A7FD: call     0x44d8b4
0x47A802: mov      eax, dword ptr [eax + 0xc]
0x47A805: mov      edx, dword ptr [ebp - 4]
0x47A808: mov      edx, dword ptr [edx + 0x3c]
0x47A80B: call     0x4173fc
0x47A810: mov      eax, dword ptr [ebp - 4]
0x47A813: mov      eax, dword ptr [eax + 0x24]
0x47A816: mov      edx, dword ptr [eax]
0x47A818: call     dword ptr [edx + 0x14]
0x47A81B: dec      eax
0x47A81C: test     eax, eax
0x47A81E: jl       0x47a87b
0x47A820: inc      eax
0x47A821: mov      dword ptr [ebp - 0x20], eax
0x47A824: mov      dword ptr [ebp - 0x10], 0
0x47A82B: lea      ecx, [ebp - 0x28]
0x47A82E: mov      edx, dword ptr [ebp - 0x10]
0x47A831: mov      eax, dword ptr [ebp - 4]
0x47A834: mov      eax, dword ptr [eax + 0x24]
0x47A837: mov      ebx, dword ptr [eax]
0x47A839: call     dword ptr [ebx + 0xc]
0x47A83C: mov      eax, dword ptr [ebp - 0x28]
0x47A83F: push     eax
0x47A840: mov      eax, dword ptr [ebp - 8]
0x47A843: call     0x44d8b4
0x47A848: mov      edx, 0x47aa60 ; "A"
0x47A84D: call     0x4181c8
0x47A852: inc      eax
0x47A853: imul     dword ptr [ebp - 0x10]
0x47A856: mov      edx, dword ptr [ebp - 0x18]
0x47A859: add      edx, 3
0x47A85C: add      eax, edx
0x47A85E: push     eax
0x47A85F: mov      eax, dword ptr [ebp - 8]
0x47A862: call     0x44d8b4
0x47A867: mov      edx, dword ptr [ebp - 0x14]
0x47A86A: add      edx, 4
0x47A86D: pop      ecx
0x47A86E: call     0x4180dc
0x47A873: inc      dword ptr [ebp - 0x10]
0x47A876: dec      dword ptr [ebp - 0x20]
0x47A879: jne      0x47a82b
0x47A87B: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x47A880: cmp      dword ptr [eax], 0
0x47A883: je       0x47aa1b
0x47A889: mov      eax, dword ptr [0x4a4028] ; *0x4A4028->0x4F947A
0x47A88E: cmp      byte ptr [eax], 0
0x47A891: je       0x47a99a
0x47A897: mov      eax, dword ptr [ebp - 4]
0x47A89A: mov      eax, dword ptr [eax + 0xc]
0x47A89D: xor      edx, edx
0x47A89F: push     edx
0x47A8A0: push     eax
0x47A8A1: lea      eax, [ebp - 0x28]
0x47A8A4: call     0x407d94
0x47A8A9: push     dword ptr [ebp - 0x28]
0x47A8AC: push     0x47aa6c ; " "
0x47A8B1: push     0x47aa78 ; "  Mouse "
0x47A8B6: lea      edx, [ebp - 0x2c]
0x47A8B9: mov      eax, dword ptr [0x4a3e90] ; *0x4A3E90->0x4F9494
0x47A8BE: mov      eax, dword ptr [eax]
0x47A8C0: call     0x407d64
0x47A8C5: push     dword ptr [ebp - 0x2c]
0x47A8C8: push     0x47aa8c ; ":"
0x47A8CD: lea      edx, [ebp - 0x30]
0x47A8D0: mov      eax, dword ptr [0x4a3e28] ; *0x4A3E28->0x4F9498
0x47A8D5: mov      eax, dword ptr [eax]
0x47A8D7: call     0x407d64
0x47A8DC: push     dword ptr [ebp - 0x30]
0x47A8DF: push     0x47aa98 ; "("
0x47A8E4: lea      edx, [ebp - 0x34]
0x47A8E7: mov      eax, dword ptr [0x4a41f0] ; *0x4A41F0->0x4F948C
0x47A8EC: mov      eax, dword ptr [eax]
0x47A8EE: call     0x407d64
0x47A8F3: push     dword ptr [ebp - 0x34]
0x47A8F6: push     0x47aa8c ; ":"
0x47A8FB: lea      edx, [ebp - 0x38]
0x47A8FE: mov      eax, dword ptr [0x4a4178] ; *0x4A4178->0x4F9490
0x47A903: mov      eax, dword ptr [eax]
0x47A905: call     0x407d64
0x47A90A: push     dword ptr [ebp - 0x38]
0x47A90D: push     0x47aaa4 ; ")"
0x47A912: push     0x47aab0 ; "  HP"
0x47A917: lea      edx, [ebp - 0x3c]
0x47A91A: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x47A91F: mov      eax, dword ptr [eax]
0x47A921: movzx    eax, word ptr [eax + 0x3c]
0x47A925: call     0x407d64
0x47A92A: push     dword ptr [ebp - 0x3c]
0x47A92D: push     0x47aac0 ; "/"
0x47A932: lea      edx, [ebp - 0x40]
0x47A935: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x47A93A: mov      eax, dword ptr [eax]
0x47A93C: movzx    eax, word ptr [eax + 0x40]
0x47A940: call     0x407d64
0x47A945: push     dword ptr [ebp - 0x40]
0x47A948: push     0x47aacc ; "  D0 "
0x47A94D: lea      edx, [ebp - 0x44]
0x47A950: mov      eax, dword ptr [0x4a42d0] ; *0x4A42D0->0x4F9544
0x47A955: mov      eax, dword ptr [eax]
0x47A957: call     0x407d64
0x47A95C: push     dword ptr [ebp - 0x44]
0x47A95F: push     0x47aadc ; " D1 "
0x47A964: lea      edx, [ebp - 0x48]
0x47A967: mov      eax, dword ptr [0x4a4174] ; *0x4A4174->0x4F9548
0x47A96C: mov      eax, dword ptr [eax]
0x47A96E: call     0x407d64
0x47A973: push     dword ptr [ebp - 0x48]
0x47A976: push     0x47aaec ; " D2 "
0x47A97B: lea      edx, [ebp - 0x4c]
0x47A97E: mov      eax, dword ptr [0x4a41c0] ; *0x4A41C0->0x4F954C
0x47A983: mov      eax, dword ptr [eax]
0x47A985: call     0x407d64
0x47A98A: push     dword ptr [ebp - 0x4c]
0x47A98D: lea      eax, [ebp - 0x1c]
0x47A990: mov      edx, 0x15
0x47A995: call     0x403d94
0x47A99A: push     0xffffff
0x47A99F: push     0
0x47A9A1: mov      eax, dword ptr [ebp - 0x1c]
0x47A9A4: push     eax
0x47A9A5: xor      ecx, ecx
0x47A9A7: mov      edx, 0xa
0x47A9AC: mov      eax, dword ptr [ebp - 8]
0x47A9AF: call     0x4a09a0
0x47A9B4: push     0xffffff
0x47A9B9: push     0
0x47A9BB: mov      eax, dword ptr [0x4a4124] ; *0x4A4124->0x4F7EE8
0x47A9C0: push     dword ptr [eax]
0x47A9C2: push     0x47aa6c ; " "
0x47A9C7: lea      edx, [ebp - 0x2c]
0x47A9CA: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x47A9CF: mov      eax, dword ptr [eax]
0x47A9D1: movzx    eax, word ptr [eax + 8]
0x47A9D5: call     0x407d64
0x47A9DA: push     dword ptr [ebp - 0x2c]
0x47A9DD: push     0x47aa8c ; ":"
0x47A9E2: lea      edx, [ebp - 0x30]
0x47A9E5: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x47A9EA: mov      eax, dword ptr [eax]
0x47A9EC: movzx    eax, word ptr [eax + 0xa]
0x47A9F0: call     0x407d64
0x47A9F5: push     dword ptr [ebp - 0x30]
0x47A9F8: lea      eax, [ebp - 0x28]
0x47A9FB: mov      edx, 5
0x47AA00: call     0x403d94
0x47AA05: mov      eax, dword ptr [ebp - 0x28]
0x47AA08: push     eax
0x47AA09: mov      ecx, 0x244
0x47AA0E: mov      edx, 8
0x47AA13: mov      eax, dword ptr [ebp - 8]
0x47AA16: call     0x4a09a0
0x47AA1B: mov      eax, dword ptr [ebp - 8]
0x47AA1E: call     0x44d8b4
0x47AA23: call     0x44d104
0x47AA28: xor      eax, eax
0x47AA2A: pop      edx
0x47AA2B: pop      ecx
0x47AA2C: pop      ecx
0x47AA2D: mov      dword ptr fs:[eax], edx
0x47AA30: push     0x47aa52
0x47AA35: lea      eax, [ebp - 0x4c]
0x47AA38: mov      edx, 0xa
0x47AA3D: call     0x403a7c
0x47AA42: lea      eax, [ebp - 0x1c]
0x47AA45: call     0x403a58
0x47AA4A: ret      
0x47AA4B: jmp      0x403518
0x47AA50: jmp      0x47aa35
0x47AA52: pop      ebx
0x47AA53: mov      esp, ebp
0x47AA55: pop      ebp
0x47AA56: ret      
0x47AA57: add      bh, bh
0x47AA5B: inc      dword ptr [ecx]
0x47AA5D: add      byte ptr [eax], al
0x47AA5F: add      byte ptr [ecx], al
0x47AA62: add      byte ptr [eax], al
0x47AA6C: [STR len=1] " "
0x47AA78: [STR len=8] "  Mouse "
0x47AA8C: [STR len=1] ":"
0x47AA98: [STR len=1] "("
0x47AAA4: [STR len=1] ")"
0x47AAB0: [STR len=4] "  HP"
0x47AAC0: [STR len=1] "/"
0x47AACC: [STR len=5] "  D0 "
0x47AADC: [STR len=4] " D1 "
0x47AAEC: [STR len=4] " D2 "
0x47AAF4: push     ebp
0x47AAF5: mov      ebp, esp
0x47AAF7: xor      eax, eax
0x47AAF9: push     ebp
0x47AAFA: push     0x47ab19
0x47AAFF: push     dword ptr fs:[eax]
0x47AB02: mov      dword ptr fs:[eax], esp
0x47AB05: inc      dword ptr [0x4f95c4]
0x47AB0B: xor      eax, eax
0x47AB0D: pop      edx
0x47AB0E: pop      ecx
0x47AB0F: pop      ecx
0x47AB10: mov      dword ptr fs:[eax], edx
0x47AB13: push     0x47ab20
0x47AB18: ret      
0x47AB19: jmp      0x403518
0x47AB1E: jmp      0x47ab18
0x47AB20: pop      ebp
0x47AB21: ret      
0x47AB22: mov      eax, eax
0x47AB24: sub      dword ptr [0x4f95c4], 1
0x47AB2B: ret      
0x47AB2C: js       0x47aad9
0x47AB2E: inc      edi
0x47AB2F: add      byte ptr [eax], al
0x47AB31: add      byte ptr [eax], al
0x47AB33: add      byte ptr [eax], al
0x47AB35: add      byte ptr [eax], al
0x47AB37: add      byte ptr [eax], al
0x47AB39: add      byte ptr [eax], al
0x47AB3B: add      byte ptr [eax], al
0x47AB3D: add      byte ptr [eax], al
0x47AB3F: add      byte ptr [eax], al
0x47AB41: add      byte ptr [eax], al
0x47AB43: add      byte ptr [eax], al
0x47AB45: add      byte ptr [eax], al
0x47AB47: add      byte ptr [eax - 0x55], bh
0x47AB4A: inc      edi
0x47AB4B: add      byte ptr [esi + 0x80047ab], dh
0x47AB51: add      byte ptr [eax], al
0x47AB53: add      byte ptr [eax - 0x37ffbff0], ah
0x47AB59: xor      byte ptr [eax], al
0x47AB5C: aam      0x30
0x47AB5E: inc      eax
0x47AB5F: add      al, bl
0x47AB61: xor      byte ptr [eax], al
0x47AB64: fdiv     qword ptr [eax]
0x47AB66: inc      eax
0x47AB67: add      al, dl
0x47AB69: xor      byte ptr [eax], al
0x47AB6C: inc      eax
0x47AB6D: inc      eax
0x47AB6F: add      byte ptr [esi + ebp + 0x40], dl
0x47AB74: pushfd   
0x47AB75: inc      eax
