
; === VMT_TKillingHerb @ 0x477A40 ===
0x477A40: add      eax, dword ptr [eax]
0x477A42: cld      
0x477A46: std      
0x477A47: jmp      ptr [eax + 0x7d]
0x477A4A: inc      edi
0x477A4B: add      al, ah
0x477A4D: jge      0x477a96
0x477A4F: add      byte ptr [eax + 0xc004781], bh
0x477A55: push     esp
0x477A56: dec      ebx
0x477A57: imul     ebp, dword ptr [esp + ebp*2 + 0x69], 0x6548676e
0x477A5F: jb       0x477ac3
0x477A61: lea      eax, [eax]
0x477A64: mov      al, 0x7a
0x477A66: inc      edi
0x477A67: add      byte ptr [eax], al
0x477A69: add      byte ptr [eax], al
0x477A6B: add      byte ptr [eax], al
0x477A6D: add      byte ptr [eax], al
0x477A6F: add      byte ptr [eax], al
0x477A71: add      byte ptr [eax], al
0x477A73: add      byte ptr [eax], al
0x477A75: add      byte ptr [eax], al
0x477A77: add      byte ptr [eax], al
0x477A79: add      byte ptr [eax], al
0x477A7B: add      byte ptr [eax], al
0x477A7D: add      byte ptr [eax], al
0x477A7F: add      byte ptr [eax - 0x41ffb886], dh
0x477A85: jp       0x477ace
0x477A87: add      byte ptr [edx + eax], al
0x477A8B: add      ah, cl
0x477A8D: pop      edx
0x477A8E: dec      ecx
0x477A8F: add      al, cl
0x477A91: xor      byte ptr [eax], al
0x477A94: aam      0x30
0x477A96: inc      eax
0x477A97: add      al, bl
0x477A99: xor      byte ptr [eax], al
0x477A9C: fdiv     qword ptr [eax]
0x477A9E: inc      eax
0x477A9F: add      al, dl
0x477AA1: xor      byte ptr [eax], al
0x477AA4: inc      eax
0x477AA5: inc      eax
0x477AA7: add      byte ptr [esi + ebp + 0x40], dl
0x477AAB: add      byte ptr [eax], cl
0x477AAD: dec      ecx
0x477AAF: add      byte ptr [edx], al
0x477AB1: add      bh, bh
0x477AB4: std      
0x477AB5: call     dword ptr [edx + eax*4 - 0x7ae7ffb9]
0x477ABC: inc      edi
0x477ABD: add      byte ptr [ecx], cl
0x477ABF: push     esp
0x477AC0: inc      edx
0x477AC1: push     ecx
0x477AC4: jne      0x477b2b
0x477AC6: outsb    dx, byte ptr gs:[esi]
0x477AC8: adc      al, 0x7b
0x477ACA: inc      edi
0x477ACB: add      byte ptr [eax], al
0x477ACD: add      byte ptr [eax], al
0x477ACF: add      byte ptr [eax], al
0x477AD1: add      byte ptr [eax], al
0x477AD3: add      byte ptr [eax], al
0x477AD5: add      byte ptr [eax], al
0x477AD7: add      byte ptr [eax], al
0x477AD9: add      byte ptr [eax], al
0x477ADB: add      byte ptr [eax], al
0x477ADD: add      byte ptr [eax], al
0x477ADF: add      byte ptr [eax], al
0x477AE1: add      byte ptr [eax], al
0x477AE3: add      byte ptr [ebx + edi*2], dl
0x477AE6: inc      edi
0x477AE7: add      byte ptr [esi], ch
0x477AE9: jnp      0x477b32
0x477AEB: add      byte ptr [edx + eax], dl
0x477AEF: add      ah, dh
0x477AF1: jns      0x477b3a
0x477AF3: add      al, cl
0x477AF5: xor      byte ptr [eax], al
0x477AF8: aam      0x30
0x477AFA: inc      eax
0x477AFB: add      al, bl
0x477AFD: xor      byte ptr [eax], al
0x477B00: fdiv     qword ptr [eax]
0x477B02: inc      eax
0x477B03: add      al, dl
0x477B05: xor      byte ptr [eax], al
0x477B08: inc      eax
0x477B09: inc      eax
0x477B0B: add      byte ptr [esi + ebp + 0x40], dl
0x477B0F: add      byte ptr [ebp + edi*2 + 0x40047], ch
0x477B17: push     ebx
0x477B19: push     ecx
0x477B1C: cli      
0x477B1E: mov      esp, 0x84004785
0x477B23: xchg     dword ptr [edi], eax
0x477B26: clc      
0x477B27: xchg     dword ptr [edi], eax
0x477B2A: mov      byte ptr fs:[edi], al
0x477B2E: adc      dword ptr [ebx + eax*2 + 0x65], edx
0x477B32: outsb    dx, byte ptr [esi]
0x477B33: je       0x477b9e
0x477B35: jo       0x477b9c
0x477B37: dec      ebx
0x477B3A: imul     ebp, dword ptr [esi + 0x67], 0x8c6e6f4d
0x477B41: jnp      0x477b8a
0x477B43: add      byte ptr [eax], al
0x477B45: add      byte ptr [eax], al
0x477B47: add      byte ptr [eax], al
0x477B49: add      byte ptr [eax], al
0x477B4B: add      byte ptr [eax], al
0x477B4D: add      byte ptr [eax], al
0x477B4F: add      byte ptr [eax], al
0x477B51: add      byte ptr [eax], al
0x477B53: add      byte ptr [eax], al
0x477B55: add      byte ptr [eax], al
0x477B57: add      byte ptr [eax], al
0x477B59: add      byte ptr [eax], al
0x477B5B: add      byte ptr [ebx + edi*2 + 0x7b940047], cl
0x477B62: inc      edi
0x477B63: add      byte ptr [edx + eax], al
0x477B67: add      ah, dh
0x477B69: jns      0x477bb2
0x477B6B: add      al, cl
0x477B6D: xor      byte ptr [eax], al
0x477B70: aam      0x30
0x477B72: inc      eax
0x477B73: add      al, bl
0x477B75: xor      byte ptr [eax], al
0x477B78: fdiv     qword ptr [eax]
0x477B7A: inc      eax
0x477B7B: add      al, dl
0x477B7D: xor      byte ptr [eax], al
0x477B80: inc      eax
0x477B81: inc      eax
0x477B83: add      byte ptr [esi + ebp + 0x40], dl
0x477B87: add      byte ptr [ebp + edi*2 + 0x10047], ch
0x477B8F: jmp      ptr [ecx + ecx*4 + 0x47]
0x477B93: add      byte ptr [esp + edx*2], cl
0x477B96: inc      edx
0x477B97: imul     esp, dword ptr [edi + 0x48], 0x74726165
0x477B9E: dec      ebp
0x477B9F: outsd    dx, dword ptr [esi]
0x477BA0: outsb    dx, byte ptr [esi]
0x477BA1: lea      eax, [eax]
0x477BA5: jnp      0x477bee
0x477BA7: add      byte ptr [eax], al
0x477BA9: add      byte ptr [eax], al
0x477BAB: add      byte ptr [eax], al
0x477BAD: add      byte ptr [eax], al
0x477BAF: add      byte ptr [eax], al
0x477BB1: add      byte ptr [eax], al
0x477BB3: add      byte ptr [eax], al
0x477BB5: add      byte ptr [eax], al
0x477BB7: add      byte ptr [eax], al
0x477BB9: add      byte ptr [eax], al
0x477BBB: add      byte ptr [eax], al
0x477BBD: add      byte ptr [eax], al
0x477BBF: add      al, dh
0x477BC1: jnp      0x477c0a
0x477BC3: add      al, bh
0x477BC5: jnp      0x477c0e
0x477BC7: add      byte ptr [edx + eax], al
0x477BCB: add      ah, dh
0x477BCD: jns      0x477c16
0x477BCF: add      al, cl
0x477BD1: xor      byte ptr [eax], al
0x477BD4: aam      0x30
0x477BD6: inc      eax
0x477BD7: add      al, bl
0x477BD9: xor      byte ptr [eax], al
0x477BDC: fdiv     qword ptr [eax]
0x477BDE: inc      eax
0x477BDF: add      al, dl
0x477BE1: xor      byte ptr [eax], al
0x477BE4: inc      eax
0x477BE5: inc      eax
0x477BE7: add      byte ptr [esi + ebp + 0x40], dl
0x477BEB: add      byte ptr [ebp + edi*2 + 0x10047], ch
0x477BF3: dec      dword ptr [eax + 0xf004789]
0x477BF9: push     esp
0x477BFA: push     ebx
0x477BFB: jo       0x477c66
0x477BFD: jb       0x477c49
0x477C01: outsd    dx, dword ptr [esi]
0x477C02: jne      0x477c77
0x477C04: dec      ebp
0x477C06: outsd    dx, dword ptr [esi]
0x477C07: outsb    dx, byte ptr [esi]
0x477C08: push     esp
0x477C09: jl       0x477c52
0x477C0B: add      byte ptr [eax], al
0x477C0D: add      byte ptr [eax], al
0x477C0F: add      byte ptr [eax], al
0x477C11: add      byte ptr [eax], al
0x477C13: add      byte ptr [eax], al
0x477C15: add      byte ptr [eax], al
0x477C17: add      byte ptr [eax], al
0x477C19: add      byte ptr [eax], al
0x477C1B: add      byte ptr [eax], al
0x477C1D: add      byte ptr [eax], al
0x477C1F: add      byte ptr [eax], al
0x477C21: add      byte ptr [eax], al
0x477C23: add      byte ptr [esp + edi*2 + 0x47], dl
0x477C27: add      byte ptr [eax + 0x5c00477c], al
0x477C2D: add      al, byte ptr [eax]
0x477C2F: add      ah, cl
0x477C31: pop      edx
0x477C32: dec      ecx
0x477C33: add      al, cl
0x477C35: xor      byte ptr [eax], al
0x477C38: aam      0x30
0x477C3A: inc      eax
0x477C3B: add      al, bl
0x477C3D: xor      byte ptr [eax], al
0x477C40: fdiv     qword ptr [eax]
0x477C42: inc      eax
0x477C43: add      al, dl
0x477C45: xor      byte ptr [eax], al
0x477C48: inc      eax
0x477C49: inc      eax
0x477C4B: add      byte ptr [esi + ebp + 0x40], dl
0x477C4F: add      byte ptr [eax], cl
0x477C51: dec      ecx
0x477C53: add      byte ptr [edi], al
0x477C55: add      ah, bh
0x477C59: push     ebx
0x477C5C: std      
0x477C5D: push     esi
0x477C60: cli      
0x477C61: push     edx
0x477C63: jmp      dword ptr [ecx + ecx*4 - 0x7393ffb9]
0x477C6A: inc      edi
0x477C6B: add      byte ptr [esp + ecx*4], al
0x477C6E: inc      edi
0x477C6F: add      byte ptr [eax + edx*4], dl
0x477C72: inc      edi
0x477C73: add      al, ah
0x477C75: nop      
0x477C76: inc      edi
0x477C77: add      byte ptr [eax], cl
0x477C79: xchg     ecx, eax
0x477C7A: inc      edi
0x477C7B: add      byte ptr [eax + 0xb004791], dh
0x477C81: push     esp
0x477C82: inc      ebx
0x477C83: popal    
0x477C84: jae      0x477cfa
0x477C86: insb     byte ptr es:[edi], dx
0x477C87: inc      esp
0x477C89: outsd    dx, dword ptr [esi]
0x477C8A: outsd    dx, dword ptr [esi]
0x477C8B: jb       0x477c65
0x477C8D: jl       0x477cd6
0x477C8F: add      byte ptr [eax], al
0x477C91: add      byte ptr [eax], al
0x477C93: add      byte ptr [eax], al
0x477C95: add      byte ptr [eax], al
0x477C97: add      byte ptr [eax], al
0x477C99: add      byte ptr [eax], al
0x477C9B: add      byte ptr [eax], al
0x477C9D: add      byte ptr [eax], al
0x477C9F: add      byte ptr [eax], al
0x477CA1: add      byte ptr [eax], al
0x477CA3: add      byte ptr [eax], al
0x477CA5: add      byte ptr [eax], al
0x477CA7: add      al, bl
0x477CA9: jl       0x477cf2
0x477CAB: add      dh, bh
0x477CAD: jl       0x477cf6
0x477CAF: add      byte ptr [edx + eax], ah
0x477CB3: add      ah, cl
0x477CB5: pop      edx
0x477CB6: dec      ecx
0x477CB7: add      al, cl
0x477CB9: xor      byte ptr [eax], al
0x477CBC: aam      0x30
0x477CBE: inc      eax
0x477CBF: add      al, bl
0x477CC1: xor      byte ptr [eax], al
0x477CC4: fdiv     qword ptr [eax]
0x477CC6: inc      eax
0x477CC7: add      al, dl
0x477CC9: xor      byte ptr [eax], al
0x477CCC: inc      eax
0x477CCD: inc      eax
0x477CCF: add      byte ptr [esi + ebp + 0x40], dl
0x477CD3: add      byte ptr [eax], cl
0x477CD5: dec      ecx
0x477CD7: add      byte ptr [esi], al
0x477CD9: add      ah, bh
0x477CDD: push     ebx
0x477CE0: std      
0x477CE1: push     edx
0x477CE4: cli      
0x477CE6: cmp      byte ptr [edx - 0x6d5fffb9], dl
0x477CEC: inc      edi
0x477CED: add      ah, ch
0x477CEF: xchg     esp, eax
0x477CF0: inc      edi
0x477CF1: add      byte ptr [eax], ch
0x477CF3: xchg     esi, eax
0x477CF4: inc      edi
0x477CF5: add      byte ptr [eax + 0x6c004796], al
0x477CFB: xchg     edi, eax
0x477CFC: inc      edi
0x477CFD: add      byte ptr [esi], cl
0x477CFF: push     esp
0x477D00: push     edi
0x477D01: popal    
0x477D02: insb     byte ptr es:[edi], dx
0x477D03: insb     byte ptr es:[edi], dx
0x477D04: push     ebx
0x477D05: je       0x477d79
0x477D07: jne      0x477d6c
0x477D09: je       0x477d80
0x477D0B: jb       0x477d72
0x477D0D: lea      eax, [eax]
0x477D10: pop      esp
0x477D11: jge      0x477d5a
0x477D13: add      byte ptr [eax], al
0x477D15: add      byte ptr [eax], al
0x477D17: add      byte ptr [eax], al
0x477D19: add      byte ptr [eax], al
0x477D1B: add      byte ptr [eax], al
0x477D1D: add      byte ptr [eax], al
0x477D1F: add      byte ptr [eax], al
0x477D21: add      byte ptr [eax], al
0x477D23: add      byte ptr [eax], al
0x477D25: add      byte ptr [eax], al
0x477D27: add      byte ptr [eax], al
0x477D29: add      byte ptr [eax], al
0x477D2B: add      byte ptr [eax], al
0x477D2D: add      byte ptr [eax], al
0x477D2F: add      byte ptr [ebp + edi*2 + 0x47], bl
0x477D33: add      byte ptr [edx + eax], al
0x477D37: add      ah, cl
0x477D39: pop      edx
0x477D3A: dec      ecx
0x477D3B: add      al, cl
0x477D3D: xor      byte ptr [eax], al
0x477D40: aam      0x30
0x477D42: inc      eax
0x477D43: add      al, bl
0x477D45: xor      byte ptr [eax], al
0x477D48: fdiv     qword ptr [eax]
0x477D4A: inc      eax
0x477D4B: add      al, dl
0x477D4D: xor      byte ptr [eax], al
0x477D50: inc      eax
0x477D51: inc      eax
0x477D53: add      byte ptr [esi + ebp + 0x40], dl
0x477D57: add      byte ptr [eax], cl
0x477D59: dec      ecx
0x477D5B: add      byte ptr [ebx], cl
0x477D5D: push     esp
0x477D5E: push     ebx
0x477D5F: outsd    dx, dword ptr [esi]
0x477D60: arpl     word ptr [ebx + 0x65], sp
0x477D63: jb       0x477da7
0x477D65: popal    
0x477D66: insb     byte ptr es:[edi], dx
0x477D67: insb     byte ptr es:[edi], dx
0x477D68: push     ebp
0x477D69: mov      ebp, esp
0x477D6B: add      esp, -8
0x477D6E: test     dl, dl
0x477D70: je       0x477d7a
0x477D72: add      esp, -0x10
0x477D75: call     0x4031a4
0x477D7A: mov      byte ptr [ebp - 5], dl
0x477D7D: mov      dword ptr [ebp - 4], eax
0x477D80: xor      edx, edx
0x477D82: mov      eax, dword ptr [ebp - 4]
0x477D85: call     0x4962d0 ; TActor.DMT_-4
0x477D8A: mov      eax, dword ptr [ebp - 4]
0x477D8D: cmp      byte ptr [ebp - 5], 0
0x477D91: je       0x477da2
0x477D93: call     0x4031fc
0x477D98: pop      dword ptr fs:[0]
0x477D9F: add      esp, 0xc
0x477DA2: mov      eax, dword ptr [ebp - 4]
0x477DA5: pop      ecx
0x477DA6: pop      ecx
0x477DA7: pop      ebp
0x477DA8: ret      
0x477DA9: lea      eax, [eax]
0x477DAC: push     ebp
0x477DAD: mov      ebp, esp
0x477DAF: add      esp, -8
0x477DB2: call     0x403204
0x477DB7: mov      byte ptr [ebp - 5], dl
0x477DBA: mov      dword ptr [ebp - 4], eax
0x477DBD: mov      dl, byte ptr [ebp - 5]
0x477DC0: and      dl, 0xfc
0x477DC3: mov      eax, dword ptr [ebp - 4]
0x477DC6: call     0x496508 ; TSkeletonOma.Destroy
0x477DCB: cmp      byte ptr [ebp - 5], 0
0x477DCF: jle      0x477dd9
0x477DD1: mov      eax, dword ptr [ebp - 4]
0x477DD4: call     0x4031f4
0x477DD9: pop      ecx
0x477DDA: pop      ecx
0x477DDB: pop      ebp
0x477DDC: ret      
0x477DDD: lea      eax, [eax]
0x477DE0: push     ebp
0x477DE1: mov      ebp, esp
0x477DE3: add      esp, -0xc
0x477DE6: mov      dword ptr [ebp - 4], eax
0x477DE9: mov      eax, dword ptr [ebp - 4]
0x477DEC: mov      byte ptr [eax + 0xec], 0
0x477DF3: mov      eax, dword ptr [ebp - 4]
0x477DF6: mov      dword ptr [eax + 0x1b8], 0xffffffff
0x477E00: mov      eax, dword ptr [ebp - 4]
0x477E03: movzx    eax, word ptr [eax + 0x14]
0x477E07: call     0x496120
0x477E0C: mov      edx, dword ptr [ebp - 4]
0x477E0F: mov      dword ptr [edx + 0xe0], eax
0x477E15: mov      eax, dword ptr [ebp - 4]
0x477E18: movzx    edx, word ptr [eax + 0x14]
0x477E1C: mov      eax, dword ptr [ebp - 4]
0x477E1F: movzx    eax, byte ptr [eax + 0xe]
0x477E23: call     0x495ca8
0x477E28: mov      dword ptr [ebp - 8], eax
0x477E2B: cmp      dword ptr [ebp - 8], 0
0x477E2F: je       0x4781b1
0x477E35: mov      eax, dword ptr [ebp - 4]
0x477E38: mov      eax, dword ptr [eax + 0x148]
0x477E3E: add      eax, -0xa
0x477E41: cmp      eax, 0x18
0x477E44: ja       0x4781b1
0x477E4A: mov      al, byte ptr [eax + 0x477e57]
0x477E50: jmp      dword ptr [eax*4 + 0x477e70]
0x477E57: add      dword ptr [eax], eax
0x477E59: add      byte ptr [eax], al
0x477E5B: add      eax, dword ptr [eax]
0x477E5D: add      byte ptr [eax], al
0x477E5F: add      byte ptr [eax], al
0x477E61: add      al, byte ptr [edi]
0x477E63: add      byte ptr [eax], al
0x477E65: add      byte ptr [eax], al
0x477E67: add      byte ptr [eax], al
0x477E69: add      byte ptr [eax], al
0x477E6B: add      byte ptr [eax - 0x7e4efa00], al
0x477E72: inc      edi
0x477E73: add      byte ptr [eax + 0x200477e], dl
0x477E79: jg       0x477ec2
0x477E7B: add      byte ptr [ebp + 0x1900477f], cl
0x477E81: add      byte ptr [edi], 0x83
0x477E85: add      byte ptr [edi], 0xfd
0x477E89: add      byte ptr [edi], 0x62
0x477E8D: add      dword ptr [edi], 0xff8458b
0x477E94: mov      bh, 0
0x477E96: mov      edx, dword ptr [ebp - 4]
0x477E99: mov      dword ptr [edx + 0x1b0], eax
0x477E9F: mov      eax, dword ptr [ebp - 8]
0x477EA2: movzx    eax, word ptr [eax + 2]
0x477EA6: mov      edx, dword ptr [ebp - 4]
0x477EA9: add      eax, dword ptr [edx + 0x1b0]
0x477EAF: dec      eax
0x477EB0: mov      edx, dword ptr [ebp - 4]
0x477EB3: mov      dword ptr [edx + 0x1b4], eax
0x477EB9: mov      eax, dword ptr [ebp - 8]
0x477EBC: movzx    eax, word ptr [eax + 6]
0x477EC0: mov      edx, dword ptr [ebp - 4]
0x477EC3: mov      dword ptr [edx + 0x1d0], eax
0x477EC9: call     0x406154
0x477ECE: mov      edx, dword ptr [ebp - 4]
0x477ED1: mov      dword ptr [edx + 0x1d4], eax
0x477ED7: mov      eax, dword ptr [ebp - 8]
0x477EDA: movzx    eax, word ptr [eax + 2]
0x477EDE: mov      edx, dword ptr [ebp - 4]
0x477EE1: mov      dword ptr [edx + 0x1f4], eax
0x477EE7: push     0
0x477EE9: push     1
0x477EEB: mov      eax, dword ptr [ebp - 4]
0x477EEE: xor      edx, edx
0x477EF0: mov      dl, byte ptr [eax + 0xc]
0x477EF3: xor      ecx, ecx
0x477EF5: mov      eax, dword ptr [ebp - 4]
0x477EF8: call     0x497610
0x477EFD: jmp      0x4781b1
0x477F02: mov      eax, dword ptr [ebp - 8]
0x477F05: movzx    eax, word ptr [eax + 0xa]
0x477F09: mov      edx, dword ptr [ebp - 4]
0x477F0C: mov      dword ptr [edx + 0x1b0], eax
0x477F12: mov      eax, dword ptr [ebp - 8]
0x477F15: movzx    eax, word ptr [eax + 0xc]
0x477F19: mov      edx, dword ptr [ebp - 4]
0x477F1C: add      eax, dword ptr [edx + 0x1b0]
0x477F22: dec      eax
0x477F23: mov      edx, dword ptr [ebp - 4]
0x477F26: mov      dword ptr [edx + 0x1b4], eax
0x477F2C: mov      eax, dword ptr [ebp - 8]
0x477F2F: movzx    eax, word ptr [eax + 0x10]
0x477F33: mov      edx, dword ptr [ebp - 4]
0x477F36: mov      dword ptr [edx + 0x1d0], eax
0x477F3C: call     0x406154
0x477F41: mov      edx, dword ptr [ebp - 4]
0x477F44: mov      dword ptr [edx + 0x1d4], eax
0x477F4A: mov      eax, dword ptr [ebp - 8]
0x477F4D: movzx    eax, byte ptr [eax + 0x12]
0x477F51: mov      edx, dword ptr [ebp - 4]
0x477F54: mov      dword ptr [edx + 0x1d8], eax
0x477F5A: mov      eax, dword ptr [ebp - 4]
0x477F5D: xor      edx, edx
0x477F5F: mov      dword ptr [eax + 0x1dc], edx
0x477F65: mov      eax, dword ptr [ebp - 4]
0x477F68: mov      dword ptr [eax + 0x1e0], 1
0x477F72: push     0
0x477F74: push     1
0x477F76: mov      eax, dword ptr [ebp - 4]
0x477F79: xor      edx, edx
0x477F7B: mov      dl, byte ptr [eax + 0xc]
0x477F7E: xor      ecx, ecx
0x477F80: mov      eax, dword ptr [ebp - 4]
0x477F83: call     0x497610
0x477F88: jmp      0x4781b1
0x477F8D: mov      eax, dword ptr [ebp - 8]
0x477F90: movzx    eax, word ptr [eax + 0x16]
0x477F94: mov      edx, dword ptr [ebp - 8]
0x477F97: movzx    edx, word ptr [edx + 0x18]
0x477F9B: add      eax, edx
0x477F9D: mov      edx, dword ptr [ebp - 4]
0x477FA0: movzx    edx, byte ptr [edx + 0xc]
0x477FA4: imul     edx
0x477FA6: mov      edx, dword ptr [ebp - 8]
0x477FA9: movzx    edx, word ptr [edx + 0x14]
0x477FAD: add      eax, edx
0x477FAF: mov      edx, dword ptr [ebp - 4]
0x477FB2: mov      dword ptr [edx + 0x1b0], eax
0x477FB8: mov      eax, dword ptr [ebp - 8]
0x477FBB: movzx    eax, word ptr [eax + 0x16]
0x477FBF: mov      edx, dword ptr [ebp - 4]
0x477FC2: add      eax, dword ptr [edx + 0x1b0]
0x477FC8: dec      eax
0x477FC9: mov      edx, dword ptr [ebp - 4]
0x477FCC: mov      dword ptr [edx + 0x1b4], eax
0x477FD2: mov      eax, dword ptr [ebp - 8]
0x477FD5: movzx    eax, word ptr [eax + 0x1a]
0x477FD9: mov      edx, dword ptr [ebp - 4]
0x477FDC: mov      dword ptr [edx + 0x1d0], eax
0x477FE2: call     0x406154
0x477FE7: mov      edx, dword ptr [ebp - 4]
0x477FEA: mov      dword ptr [edx + 0x1d4], eax
0x477FF0: call     0x406154
0x477FF5: mov      edx, dword ptr [ebp - 4]
0x477FF8: mov      dword ptr [edx + 0x150], eax
0x477FFE: push     0
0x478000: push     1
0x478002: mov      eax, dword ptr [ebp - 4]
0x478005: xor      edx, edx
0x478007: mov      dl, byte ptr [eax + 0xc]
0x47800A: xor      ecx, ecx
0x47800C: mov      eax, dword ptr [ebp - 4]
0x47800F: call     0x497610
0x478014: jmp      0x4781b1
0x478019: mov      eax, dword ptr [ebp - 8]
0x47801C: movzx    eax, word ptr [eax + 0x2a]
0x478020: mov      edx, dword ptr [ebp - 8]
0x478023: movzx    edx, word ptr [edx + 0x2c]
0x478027: add      eax, edx
0x478029: mov      edx, dword ptr [ebp - 4]
0x47802C: movzx    edx, byte ptr [edx + 0xc]
0x478030: imul     edx
0x478032: mov      edx, dword ptr [ebp - 8]
0x478035: movzx    edx, word ptr [edx + 0x28]
0x478039: add      eax, edx
0x47803B: mov      edx, dword ptr [ebp - 4]
0x47803E: mov      dword ptr [edx + 0x1b0], eax
0x478044: mov      eax, dword ptr [ebp - 8]
0x478047: movzx    eax, word ptr [eax + 0x2a]
0x47804B: mov      edx, dword ptr [ebp - 4]
0x47804E: add      eax, dword ptr [edx + 0x1b0]
0x478054: dec      eax
0x478055: mov      edx, dword ptr [ebp - 4]
0x478058: mov      dword ptr [edx + 0x1b4], eax
0x47805E: mov      eax, dword ptr [ebp - 4]
0x478061: mov      eax, dword ptr [eax + 0x1e8]
0x478067: mov      edx, dword ptr [ebp - 4]
0x47806A: mov      dword ptr [edx + 0x1d0], eax
0x478070: call     0x406154
0x478075: mov      edx, dword ptr [ebp - 4]
0x478078: mov      dword ptr [edx + 0x1d4], eax
0x47807E: jmp      0x4781b1
0x478083: mov      eax, dword ptr [ebp - 8]
0x478086: movzx    eax, word ptr [eax + 0x34]
0x47808A: mov      edx, dword ptr [ebp - 8]
0x47808D: movzx    edx, word ptr [edx + 0x36]
0x478091: add      eax, edx
0x478093: mov      edx, dword ptr [ebp - 4]
0x478096: movzx    edx, byte ptr [edx + 0xc]
0x47809A: imul     edx
0x47809C: mov      edx, dword ptr [ebp - 8]
0x47809F: movzx    edx, word ptr [edx + 0x32]
0x4780A3: add      eax, edx
0x4780A5: mov      edx, dword ptr [ebp - 4]
0x4780A8: mov      dword ptr [edx + 0x1b0], eax
0x4780AE: mov      eax, dword ptr [ebp - 8]
0x4780B1: movzx    eax, word ptr [eax + 0x34]
0x4780B5: mov      edx, dword ptr [ebp - 4]
0x4780B8: add      eax, dword ptr [edx + 0x1b0]
0x4780BE: dec      eax
0x4780BF: mov      edx, dword ptr [ebp - 4]
0x4780C2: mov      dword ptr [edx + 0x1b4], eax
0x4780C8: mov      eax, dword ptr [ebp - 4]
0x4780CB: mov      eax, dword ptr [eax + 0x1b4]
0x4780D1: mov      edx, dword ptr [ebp - 4]
0x4780D4: mov      dword ptr [edx + 0x1b0], eax
0x4780DA: mov      eax, dword ptr [ebp - 8]
0x4780DD: movzx    eax, word ptr [eax + 0x38]
0x4780E1: mov      edx, dword ptr [ebp - 4]
0x4780E4: mov      dword ptr [edx + 0x1d0], eax
0x4780EA: call     0x406154
0x4780EF: mov      edx, dword ptr [ebp - 4]
0x4780F2: mov      dword ptr [edx + 0x1d4], eax
0x4780F8: jmp      0x4781b1
0x4780FD: mov      eax, dword ptr [ebp - 8]
0x478100: movzx    eax, word ptr [eax + 0x34]
0x478104: mov      edx, dword ptr [ebp - 8]
0x478107: movzx    edx, word ptr [edx + 0x36]
0x47810B: add      eax, edx
0x47810D: mov      edx, dword ptr [ebp - 4]
0x478110: movzx    edx, byte ptr [edx + 0xc]
0x478114: imul     edx
0x478116: mov      edx, dword ptr [ebp - 8]
0x478119: movzx    edx, word ptr [edx + 0x32]
0x47811D: add      eax, edx
0x47811F: mov      edx, dword ptr [ebp - 4]
0x478122: mov      dword ptr [edx + 0x1b0], eax
0x478128: mov      eax, dword ptr [ebp - 8]
0x47812B: movzx    eax, word ptr [eax + 0x34]
0x47812F: mov      edx, dword ptr [ebp - 4]
0x478132: add      eax, dword ptr [edx + 0x1b0]
0x478138: dec      eax
0x478139: mov      edx, dword ptr [ebp - 4]
0x47813C: mov      dword ptr [edx + 0x1b4], eax
0x478142: mov      eax, dword ptr [ebp - 8]
0x478145: movzx    eax, word ptr [eax + 0x38]
0x478149: mov      edx, dword ptr [ebp - 4]
0x47814C: mov      dword ptr [edx + 0x1d0], eax
0x478152: call     0x406154
0x478157: mov      edx, dword ptr [ebp - 4]
0x47815A: mov      dword ptr [edx + 0x1d4], eax
0x478160: jmp      0x4781b1
0x478162: mov      eax, dword ptr [ebp - 8]
0x478165: movzx    eax, word ptr [eax + 0x3c]
0x478169: mov      edx, dword ptr [ebp - 4]
0x47816C: mov      dword ptr [edx + 0x1b0], eax
0x478172: mov      eax, dword ptr [ebp - 8]
0x478175: movzx    eax, word ptr [eax + 0x3e]
0x478179: mov      edx, dword ptr [ebp - 4]
0x47817C: add      eax, dword ptr [edx + 0x1b0]
0x478182: dec      eax
0x478183: mov      edx, dword ptr [ebp - 4]
0x478186: mov      dword ptr [edx + 0x1b4], eax
0x47818C: mov      eax, dword ptr [ebp - 8]
0x47818F: movzx    eax, word ptr [eax + 0x42]
0x478193: mov      edx, dword ptr [ebp - 4]
0x478196: mov      dword ptr [edx + 0x1d0], eax
0x47819C: call     0x406154
0x4781A1: mov      edx, dword ptr [ebp - 4]
0x4781A4: mov      dword ptr [edx + 0x1d4], eax
0x4781AA: mov      eax, dword ptr [ebp - 4]
0x4781AD: mov      byte ptr [eax + 0x23], 1
0x4781B1: mov      esp, ebp
0x4781B3: pop      ebp
0x4781B4: ret      
0x4781B5: lea      eax, [eax]
0x4781B8: push     ebp
0x4781B9: mov      ebp, esp
0x4781BB: add      esp, -0x18
0x4781BE: mov      byte ptr [ebp - 0x11], dl
0x4781C1: mov      dword ptr [ebp - 4], eax
0x4781C4: mov      eax, dword ptr [ebp - 4]
0x4781C7: movzx    edx, word ptr [eax + 0x14]
0x4781CB: mov      eax, dword ptr [ebp - 4]
0x4781CE: movzx    eax, byte ptr [eax + 0xe]
0x4781D2: call     0x495ca8
0x4781D7: mov      dword ptr [ebp - 0x10], eax
0x4781DA: cmp      dword ptr [ebp - 0x10], 0
0x4781DE: je       0x47828a
0x4781E4: mov      eax, dword ptr [ebp - 4]
0x4781E7: cmp      byte ptr [eax + 0x20], 0
0x4781EB: je       0x478236
0x4781ED: mov      eax, dword ptr [ebp - 4]
0x4781F0: cmp      byte ptr [eax + 0x21], 0
0x4781F4: je       0x478205
0x4781F6: mov      eax, dword ptr [ebp - 0x10]
0x4781F9: movzx    eax, word ptr [eax + 0x3c]
0x4781FD: mov      dword ptr [ebp - 8], eax
0x478200: jmp      0x47828a
0x478205: mov      eax, dword ptr [ebp - 0x10]
0x478208: movzx    eax, word ptr [eax + 0x34]
0x47820C: mov      edx, dword ptr [ebp - 0x10]
0x47820F: movzx    edx, word ptr [edx + 0x36]
0x478213: add      eax, edx
0x478215: mov      edx, dword ptr [ebp - 4]
0x478218: movzx    edx, byte ptr [edx + 0xc]
0x47821C: imul     edx
0x47821E: mov      edx, dword ptr [ebp - 0x10]
0x478221: movzx    edx, word ptr [edx + 0x32]
0x478225: add      eax, edx
0x478227: mov      edx, dword ptr [ebp - 0x10]
0x47822A: movzx    edx, word ptr [edx + 0x34]
0x47822E: dec      edx
0x47822F: add      eax, edx
0x478231: mov      dword ptr [ebp - 8], eax
0x478234: jmp      0x47828a
0x478236: mov      eax, dword ptr [ebp - 0x10]
0x478239: movzx    eax, word ptr [eax + 2]
0x47823D: mov      edx, dword ptr [ebp - 4]
0x478240: mov      dword ptr [edx + 0x1f4], eax
0x478246: mov      eax, dword ptr [ebp - 4]
0x478249: cmp      dword ptr [eax + 0x1ec], 0
0x478250: jge      0x478259
0x478252: xor      eax, eax
0x478254: mov      dword ptr [ebp - 0xc], eax
0x478257: jmp      0x47827e
0x478259: mov      eax, dword ptr [ebp - 0x10]
0x47825C: movzx    eax, word ptr [eax + 2]
0x478260: mov      edx, dword ptr [ebp - 4]
0x478263: cmp      eax, dword ptr [edx + 0x1ec]
0x478269: jg       0x478272
0x47826B: xor      eax, eax
0x47826D: mov      dword ptr [ebp - 0xc], eax
0x478270: jmp      0x47827e
0x478272: mov      eax, dword ptr [ebp - 4]
0x478275: mov      eax, dword ptr [eax + 0x1ec]
0x47827B: mov      dword ptr [ebp - 0xc], eax
0x47827E: mov      eax, dword ptr [ebp - 0x10]
0x478281: movzx    eax, word ptr [eax]
0x478284: add      eax, dword ptr [ebp - 0xc]
0x478287: mov      dword ptr [ebp - 8], eax
0x47828A: mov      eax, dword ptr [ebp - 8]
0x47828D: mov      esp, ebp
0x47828F: pop      ebp
0x478290: ret      
0x478291: lea      eax, [eax]
0x478294: push     ebp
0x478295: mov      ebp, esp
0x478297: add      esp, -8
0x47829A: mov      dword ptr [ebp - 4], eax
0x47829D: mov      eax, dword ptr [ebp - 4]
0x4782A0: mov      byte ptr [eax + 0xec], 0
0x4782A7: mov      eax, dword ptr [ebp - 4]
0x4782AA: mov      dword ptr [eax + 0x1b8], 0xffffffff
0x4782B4: mov      eax, dword ptr [ebp - 4]
0x4782B7: movzx    eax, word ptr [eax + 0x14]
0x4782BB: call     0x496120
0x4782C0: mov      edx, dword ptr [ebp - 4]
0x4782C3: mov      dword ptr [edx + 0xe0], eax
0x4782C9: mov      eax, dword ptr [ebp - 4]
0x4782CC: movzx    edx, word ptr [eax + 0x14]
0x4782D0: mov      eax, dword ptr [ebp - 4]
0x4782D3: movzx    eax, byte ptr [eax + 0xe]
0x4782D7: call     0x495ca8
0x4782DC: mov      dword ptr [ebp - 8], eax
0x4782DF: cmp      dword ptr [ebp - 8], 0
0x4782E3: je       0x478512
0x4782E9: mov      eax, dword ptr [ebp - 4]
0x4782EC: mov      eax, dword ptr [eax + 0x148]
0x4782F2: add      eax, -0xa
0x4782F5: cmp      eax, 0x18
0x4782F8: ja       0x478512
0x4782FE: mov      al, byte ptr [eax + 0x47830b]
0x478304: jmp      dword ptr [eax*4 + 0x478324]
0x47830B: add      dword ptr [eax], eax
0x47830D: add      byte ptr [eax], al
0x47830F: add      al, byte ptr [eax]
0x478311: add      byte ptr [eax], al
0x478313: add      byte ptr [eax], al
0x478315: add      byte ptr [eax], al
0x478317: add      byte ptr [eax], al
0x478319: add      byte ptr [eax], al
0x47831B: add      byte ptr [eax], al
0x47831D: add      byte ptr [eax], al
0x47831F: add      byte ptr [ebx], al
0x478321: add      al, 0
0x478323: add      eax, 0x478512
0x478328: cmp      al, 0x83
0x47832A: inc      edi
0x47832B: add      byte ptr [esi + 0x1f004783], ch
0x478331: test     byte ptr [edi], al
0x478334: outsb    dx, byte ptr [esi]
0x478335: test     byte ptr [edi], al
0x478338: retf     0x4784
0x47833B: add      byte ptr [ebx - 0x48f007bb], cl
0x478341: add      byte ptr [ebx - 0x7d7603ab], cl
0x478347: mov      al, 1
0x478349: add      byte ptr [eax], al
0x47834B: mov      eax, dword ptr [ebp - 8]
0x47834E: movzx    eax, word ptr [eax + 2]
0x478352: mov      edx, dword ptr [ebp - 4]
0x478355: add      eax, dword ptr [edx + 0x1b0]
0x47835B: dec      eax
0x47835C: mov      edx, dword ptr [ebp - 4]
0x47835F: mov      dword ptr [edx + 0x1b4], eax
0x478365: mov      eax, dword ptr [ebp - 8]
0x478368: movzx    eax, word ptr [eax + 6]
0x47836C: mov      edx, dword ptr [ebp - 4]
0x47836F: mov      dword ptr [edx + 0x1d0], eax
0x478375: call     0x406154
0x47837A: mov      edx, dword ptr [ebp - 4]
0x47837D: mov      dword ptr [edx + 0x1d4], eax
0x478383: mov      eax, dword ptr [ebp - 8]
0x478386: movzx    eax, word ptr [eax + 2]
0x47838A: mov      edx, dword ptr [ebp - 4]
0x47838D: mov      dword ptr [edx + 0x1f4], eax
0x478393: push     0
0x478395: push     1
0x478397: mov      eax, dword ptr [ebp - 4]
0x47839A: xor      edx, edx
0x47839C: mov      dl, byte ptr [eax + 0xc]
0x47839F: xor      ecx, ecx
0x4783A1: mov      eax, dword ptr [ebp - 4]
0x4783A4: call     0x497610
0x4783A9: jmp      0x478512
0x4783AE: mov      eax, dword ptr [ebp - 8]
0x4783B1: movzx    eax, word ptr [eax + 0x14]
0x4783B5: mov      edx, dword ptr [ebp - 4]
0x4783B8: mov      dword ptr [edx + 0x1b0], eax
0x4783BE: mov      eax, dword ptr [ebp - 8]
0x4783C1: movzx    eax, word ptr [eax + 0x16]
0x4783C5: mov      edx, dword ptr [ebp - 4]
0x4783C8: add      eax, dword ptr [edx + 0x1b0]
0x4783CE: dec      eax
0x4783CF: mov      edx, dword ptr [ebp - 4]
0x4783D2: mov      dword ptr [edx + 0x1b4], eax
0x4783D8: mov      eax, dword ptr [ebp - 8]
0x4783DB: movzx    eax, word ptr [eax + 0x1a]
0x4783DF: mov      edx, dword ptr [ebp - 4]
0x4783E2: mov      dword ptr [edx + 0x1d0], eax
0x4783E8: call     0x406154
0x4783ED: mov      edx, dword ptr [ebp - 4]
0x4783F0: mov      dword ptr [edx + 0x1d4], eax
0x4783F6: call     0x406154
0x4783FB: mov      edx, dword ptr [ebp - 4]
0x4783FE: mov      dword ptr [edx + 0x150], eax
0x478404: push     0
0x478406: push     1
0x478408: mov      eax, dword ptr [ebp - 4]
0x47840B: xor      edx, edx
0x47840D: mov      dl, byte ptr [eax + 0xc]
0x478410: xor      ecx, ecx
0x478412: mov      eax, dword ptr [ebp - 4]
0x478415: call     0x497610
0x47841A: jmp      0x478512
0x47841F: mov      eax, dword ptr [ebp - 8]
0x478422: movzx    eax, word ptr [eax + 0x28]
0x478426: mov      edx, dword ptr [ebp - 4]
0x478429: mov      dword ptr [edx + 0x1b0], eax
0x47842F: mov      eax, dword ptr [ebp - 8]
0x478432: movzx    eax, word ptr [eax + 0x2a]
0x478436: mov      edx, dword ptr [ebp - 4]
0x478439: add      eax, dword ptr [edx + 0x1b0]
0x47843F: dec      eax
0x478440: mov      edx, dword ptr [ebp - 4]
0x478443: mov      dword ptr [edx + 0x1b4], eax
0x478449: mov      eax, dword ptr [ebp - 4]
0x47844C: mov      eax, dword ptr [eax + 0x1e8]
0x478452: mov      edx, dword ptr [ebp - 4]
0x478455: mov      dword ptr [edx + 0x1d0], eax
0x47845B: call     0x406154
0x478460: mov      edx, dword ptr [ebp - 4]
0x478463: mov      dword ptr [edx + 0x1d4], eax
0x478469: jmp      0x478512
0x47846E: mov      eax, dword ptr [ebp - 8]
0x478471: movzx    eax, word ptr [eax + 0x32]
0x478475: mov      edx, dword ptr [ebp - 4]
0x478478: mov      dword ptr [edx + 0x1b0], eax
0x47847E: mov      eax, dword ptr [ebp - 8]
0x478481: movzx    eax, word ptr [eax + 0x34]
0x478485: mov      edx, dword ptr [ebp - 4]
0x478488: add      eax, dword ptr [edx + 0x1b0]
0x47848E: dec      eax
0x47848F: mov      edx, dword ptr [ebp - 4]
0x478492: mov      dword ptr [edx + 0x1b4], eax
0x478498: mov      eax, dword ptr [ebp - 4]
0x47849B: mov      eax, dword ptr [eax + 0x1b4]
0x4784A1: mov      edx, dword ptr [ebp - 4]
0x4784A4: mov      dword ptr [edx + 0x1b0], eax
0x4784AA: mov      eax, dword ptr [ebp - 8]
0x4784AD: movzx    eax, word ptr [eax + 0x38]
0x4784B1: mov      edx, dword ptr [ebp - 4]
0x4784B4: mov      dword ptr [edx + 0x1d0], eax
0x4784BA: call     0x406154
0x4784BF: mov      edx, dword ptr [ebp - 4]
0x4784C2: mov      dword ptr [edx + 0x1d4], eax
0x4784C8: jmp      0x478512
0x4784CA: mov      eax, dword ptr [ebp - 8]
0x4784CD: movzx    eax, word ptr [eax + 0x32]
0x4784D1: mov      edx, dword ptr [ebp - 4]
0x4784D4: mov      dword ptr [edx + 0x1b0], eax
0x4784DA: mov      eax, dword ptr [ebp - 8]
0x4784DD: movzx    eax, word ptr [eax + 0x34]
0x4784E1: mov      edx, dword ptr [ebp - 4]
0x4784E4: add      eax, dword ptr [edx + 0x1b0]
0x4784EA: dec      eax
0x4784EB: mov      edx, dword ptr [ebp - 4]
0x4784EE: mov      dword ptr [edx + 0x1b4], eax
0x4784F4: mov      eax, dword ptr [ebp - 8]
0x4784F7: movzx    eax, word ptr [eax + 0x38]
0x4784FB: mov      edx, dword ptr [ebp - 4]
0x4784FE: mov      dword ptr [edx + 0x1d0], eax
0x478504: call     0x406154
0x478509: mov      edx, dword ptr [ebp - 4]
0x47850C: mov      dword ptr [edx + 0x1d4], eax
0x478512: pop      ecx
0x478513: pop      ecx
0x478514: pop      ebp
0x478515: ret      
0x478516: mov      eax, eax
0x478518: push     ebp
0x478519: mov      ebp, esp
0x47851B: add      esp, -0x14
0x47851E: mov      byte ptr [ebp - 0x11], dl
0x478521: mov      dword ptr [ebp - 4], eax
0x478524: mov      eax, dword ptr [ebp - 4]
0x478527: movzx    edx, word ptr [eax + 0x14]
0x47852B: mov      eax, dword ptr [ebp - 4]
0x47852E: movzx    eax, byte ptr [eax + 0xe]
0x478532: call     0x495ca8
0x478537: mov      dword ptr [ebp - 0xc], eax
0x47853A: cmp      dword ptr [ebp - 0xc], 0
0x47853E: je       0x4785b3
0x478540: mov      eax, dword ptr [ebp - 4]
0x478543: cmp      byte ptr [eax + 0x20], 0
0x478547: je       0x47855f
0x478549: mov      eax, dword ptr [ebp - 0xc]
0x47854C: movzx    eax, word ptr [eax + 0x32]
0x478550: mov      edx, dword ptr [ebp - 0xc]
0x478553: movzx    edx, word ptr [edx + 0x34]
0x478557: dec      edx
0x478558: add      eax, edx
0x47855A: mov      dword ptr [ebp - 8], eax
0x47855D: jmp      0x4785b3
0x47855F: mov      eax, dword ptr [ebp - 0xc]
0x478562: movzx    eax, word ptr [eax + 2]
0x478566: mov      edx, dword ptr [ebp - 4]
0x478569: mov      dword ptr [edx + 0x1f4], eax
0x47856F: mov      eax, dword ptr [ebp - 4]
0x478572: cmp      dword ptr [eax + 0x1ec], 0
0x478579: jge      0x478582
0x47857B: xor      eax, eax
0x47857D: mov      dword ptr [ebp - 0x10], eax
0x478580: jmp      0x4785a7
0x478582: mov      eax, dword ptr [ebp - 0xc]
0x478585: movzx    eax, word ptr [eax + 2]
0x478589: mov      edx, dword ptr [ebp - 4]
0x47858C: cmp      eax, dword ptr [edx + 0x1ec]
0x478592: jg       0x47859b
0x478594: xor      eax, eax
0x478596: mov      dword ptr [ebp - 0x10], eax
0x478599: jmp      0x4785a7
0x47859B: mov      eax, dword ptr [ebp - 4]
0x47859E: mov      eax, dword ptr [eax + 0x1ec]
0x4785A4: mov      dword ptr [ebp - 0x10], eax
0x4785A7: mov      eax, dword ptr [ebp - 0xc]
0x4785AA: movzx    eax, word ptr [eax]
0x4785AD: add      eax, dword ptr [ebp - 0x10]
0x4785B0: mov      dword ptr [ebp - 8], eax
0x4785B3: mov      eax, dword ptr [ebp - 8]
0x4785B6: mov      esp, ebp
0x4785B8: pop      ebp
0x4785B9: ret      
0x4785BA: mov      eax, eax
0x4785BC: push     ebp
0x4785BD: mov      ebp, esp
0x4785BF: add      esp, -8
0x4785C2: mov      dword ptr [ebp - 4], eax
0x4785C5: mov      eax, dword ptr [ebp - 4]
0x4785C8: mov      byte ptr [eax + 0xec], 0
0x4785CF: mov      eax, dword ptr [ebp - 4]
0x4785D2: mov      byte ptr [eax + 0xee], 0
0x4785D9: mov      eax, dword ptr [ebp - 4]
0x4785DC: mov      byte ptr [eax + 0x248], 0
0x4785E3: mov      eax, dword ptr [ebp - 4]
0x4785E6: mov      dword ptr [eax + 0x1b8], 0xffffffff
0x4785F0: mov      eax, dword ptr [ebp - 4]
0x4785F3: movzx    eax, word ptr [eax + 0x14]
0x4785F7: call     0x496120
0x4785FC: mov      edx, dword ptr [ebp - 4]
0x4785FF: mov      dword ptr [edx + 0xe0], eax
0x478605: mov      eax, dword ptr [ebp - 4]
0x478608: movzx    edx, word ptr [eax + 0x14]
0x47860C: mov      eax, dword ptr [ebp - 4]
0x47860F: movzx    eax, byte ptr [eax + 0xe]
0x478613: call     0x495ca8
0x478618: mov      dword ptr [ebp - 8], eax
0x47861B: cmp      dword ptr [ebp - 8], 0
0x47861F: je       0x47877e
0x478625: mov      eax, dword ptr [ebp - 4]
0x478628: mov      eax, dword ptr [eax + 0x148]
0x47862E: sub      eax, 0xa
0x478631: je       0x478646
0x478633: sub      eax, 4
0x478636: je       0x47865a
0x478638: sub      eax, 7
0x47863B: je       0x47871e
0x478641: jmp      0x47876f
0x478646: mov      eax, dword ptr [ebp - 4]
0x478649: mov      byte ptr [eax + 0xc], 0
0x47864D: mov      eax, dword ptr [ebp - 4]
0x478650: call     0x477de0 ; TKillingHerb.DMT_-1
0x478655: jmp      0x47877e
0x47865A: mov      eax, dword ptr [ebp - 4]
0x47865D: mov      byte ptr [eax + 0xc], 0
0x478661: mov      eax, dword ptr [ebp - 8]
0x478664: movzx    eax, word ptr [eax + 0x20]
0x478668: mov      edx, dword ptr [ebp - 8]
0x47866B: movzx    edx, word ptr [edx + 0x22]
0x47866F: add      eax, edx
0x478671: mov      edx, dword ptr [ebp - 4]
0x478674: movzx    edx, byte ptr [edx + 0xc]
0x478678: imul     edx
0x47867A: mov      edx, dword ptr [ebp - 8]
0x47867D: movzx    edx, word ptr [edx + 0x1e]
0x478681: add      eax, edx
0x478683: mov      edx, dword ptr [ebp - 4]
0x478686: mov      dword ptr [edx + 0x1b0], eax
0x47868C: mov      eax, dword ptr [ebp - 8]
0x47868F: movzx    eax, word ptr [eax + 0x20]
0x478693: mov      edx, dword ptr [ebp - 4]
0x478696: add      eax, dword ptr [edx + 0x1b0]
0x47869C: dec      eax
0x47869D: mov      edx, dword ptr [ebp - 4]
0x4786A0: mov      dword ptr [edx + 0x1b4], eax
0x4786A6: mov      eax, dword ptr [ebp - 8]
0x4786A9: movzx    eax, word ptr [eax + 0x24]
0x4786AD: mov      edx, dword ptr [ebp - 4]
0x4786B0: mov      dword ptr [edx + 0x1d0], eax
0x4786B6: call     0x406154
0x4786BB: mov      edx, dword ptr [ebp - 4]
0x4786BE: mov      dword ptr [edx + 0x1d4], eax
0x4786C4: mov      eax, dword ptr [ebp - 4]
0x4786C7: mov      byte ptr [eax + 0x248], 1
0x4786CE: mov      eax, dword ptr [ebp - 4]
0x4786D1: xor      edx, edx
0x4786D3: mov      dword ptr [eax + 0x1c0], edx
0x4786D9: mov      eax, dword ptr [ebp - 4]
0x4786DC: xor      edx, edx
0x4786DE: mov      dword ptr [eax + 0x1bc], edx
0x4786E4: mov      eax, dword ptr [ebp - 4]
0x4786E7: mov      eax, dword ptr [eax + 0x1bc]
0x4786ED: add      eax, 9
0x4786F0: mov      edx, dword ptr [ebp - 4]
0x4786F3: mov      dword ptr [edx + 0x1c4], eax
0x4786F9: mov      eax, dword ptr [ebp - 4]
0x4786FC: mov      dword ptr [eax + 0x1cc], 0x32
0x478706: push     0
0x478708: push     1
0x47870A: mov      eax, dword ptr [ebp - 4]
0x47870D: xor      edx, edx
0x47870F: mov      dl, byte ptr [eax + 0xc]
0x478712: xor      ecx, ecx
0x478714: mov      eax, dword ptr [ebp - 4]
0x478717: call     0x497610
0x47871C: jmp      0x47877e
0x47871E: mov      eax, dword ptr [ebp - 8]
0x478721: movzx    eax, word ptr [eax + 0x3c]
0x478725: mov      edx, dword ptr [ebp - 4]
0x478728: mov      dword ptr [edx + 0x1b0], eax
0x47872E: mov      eax, dword ptr [ebp - 8]
0x478731: movzx    eax, word ptr [eax + 0x3e]
0x478735: mov      edx, dword ptr [ebp - 4]
0x478738: add      eax, dword ptr [edx + 0x1b0]
0x47873E: dec      eax
0x47873F: mov      edx, dword ptr [ebp - 4]
0x478742: mov      dword ptr [edx + 0x1b4], eax
0x478748: mov      eax, dword ptr [ebp - 8]
0x47874B: movzx    eax, word ptr [eax + 0x42]
0x47874F: mov      edx, dword ptr [ebp - 4]
0x478752: mov      dword ptr [edx + 0x1d0], eax
0x478758: call     0x406154
0x47875D: mov      edx, dword ptr [ebp - 4]
0x478760: mov      dword ptr [edx + 0x1d4], eax
0x478766: mov      eax, dword ptr [ebp - 4]
0x478769: mov      byte ptr [eax + 0x23], 1
0x47876D: jmp      0x47877e
0x47876F: mov      eax, dword ptr [ebp - 4]
0x478772: mov      byte ptr [eax + 0xc], 0
0x478776: mov      eax, dword ptr [ebp - 4]
0x478779: call     0x477de0 ; TKillingHerb.DMT_-1
0x47877E: pop      ecx
0x47877F: pop      ecx
0x478780: pop      ebp
0x478781: ret      
0x478782: mov      eax, eax
0x478784: push     ebp
0x478785: mov      ebp, esp
0x478787: push     ecx
0x478788: mov      dword ptr [ebp - 4], eax
0x47878B: mov      eax, dword ptr [ebp - 4]
0x47878E: call     0x497df4 ; TActor.DMT_-13
0x478793: mov      eax, dword ptr [ebp - 4]
0x478796: call     0x4787a0
0x47879B: pop      ecx
0x47879C: pop      ebp
0x47879D: ret      
0x47879E: mov      eax, eax
0x4787A0: push     ebp
0x4787A1: mov      ebp, esp
0x4787A3: push     ecx
0x4787A4: mov      dword ptr [ebp - 4], eax
0x4787A7: mov      eax, dword ptr [ebp - 4]
0x4787AA: cmp      byte ptr [eax + 0xee], 0
0x4787B1: je       0x4787f5
0x4787B3: mov      eax, dword ptr [ebp - 4]
0x4787B6: add      eax, 0x250
0x4787BB: push     eax
0x4787BC: mov      eax, dword ptr [ebp - 4]
0x4787BF: lea      ecx, [eax + 0x24c]
0x4787C5: mov      eax, dword ptr [ebp - 4]
0x4787C8: mov      edx, dword ptr [eax + 0x1c0]
0x4787CE: add      edx, 0x64
0x4787D1: mov      eax, dword ptr [ebp - 4]
0x4787D4: sub      edx, dword ptr [eax + 0x1bc]
0x4787DA: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4787DF: mov      eax, dword ptr [eax]
0x4787E1: mov      eax, dword ptr [eax + 0x3ac]
0x4787E7: call     0x453a48
0x4787EC: mov      edx, dword ptr [ebp - 4]
0x4787EF: mov      dword ptr [edx + 0x244], eax
0x4787F5: pop      ecx
0x4787F6: pop      ebp
0x4787F7: ret      

; === TCentipedeKingMon.DMT_-15 @ 0x4787F8 ===
0x4787F8: push     ebp
0x4787F9: mov      ebp, esp
0x4787FB: add      esp, -0x18
0x4787FE: mov      dword ptr [ebp - 0xc], ecx
0x478801: mov      dword ptr [ebp - 8], edx
0x478804: mov      dword ptr [ebp - 4], eax
0x478807: mov      eax, dword ptr [ebp - 4]
0x47880A: cmp      byte ptr [eax + 0xee], 0
0x478811: je       0x47885d
0x478813: mov      eax, dword ptr [ebp - 4]
0x478816: cmp      dword ptr [eax + 0x244], 0
0x47881D: je       0x47885d
0x47881F: mov      eax, dword ptr [ebp - 4]
0x478822: mov      eax, dword ptr [eax + 0x244]
0x478828: push     eax
0x478829: push     1
0x47882B: mov      ecx, dword ptr [ebp + 8]
0x47882E: mov      eax, dword ptr [ebp - 4]
0x478831: add      ecx, dword ptr [eax + 0x250]
0x478837: mov      eax, dword ptr [ebp - 4]
0x47883A: add      ecx, dword ptr [eax + 0x9c]
0x478840: mov      edx, dword ptr [ebp - 0xc]
0x478843: mov      eax, dword ptr [ebp - 4]
0x478846: add      edx, dword ptr [eax + 0x24c]
0x47884C: mov      eax, dword ptr [ebp - 4]
0x47884F: add      edx, dword ptr [eax + 0x98]
0x478855: mov      eax, dword ptr [ebp - 8]
0x478858: call     0x455578
0x47885D: mov      esp, ebp
0x47885F: pop      ebp
0x478860: ret      4
0x478863: nop      
0x478864: push     ebp
0x478865: mov      ebp, esp
0x478867: add      esp, -0xc
0x47886A: mov      dword ptr [ebp - 4], eax
0x47886D: mov      eax, dword ptr [ebp - 4]
0x478870: cmp      dword ptr [eax + 0x148], 0xb
0x478877: je       0x478967
0x47887D: mov      eax, dword ptr [ebp - 4]
0x478880: cmp      dword ptr [eax + 0x148], 9
0x478887: je       0x478967
0x47888D: mov      eax, dword ptr [ebp - 4]
0x478890: cmp      dword ptr [eax + 0x148], 0xd
0x478897: je       0x478967
0x47889D: mov      eax, dword ptr [ebp - 4]
0x4788A0: cmp      byte ptr [eax + 0x248], 0
0x4788A7: je       0x4788f5
0x4788A9: mov      eax, dword ptr [ebp - 4]
0x4788AC: mov      eax, dword ptr [eax + 0x1b8]
0x4788B2: mov      edx, dword ptr [ebp - 4]
0x4788B5: sub      eax, dword ptr [edx + 0x1b0]
0x4788BB: cmp      eax, 5
0x4788BE: jl       0x4788f5
0x4788C0: mov      eax, dword ptr [ebp - 4]
0x4788C3: mov      byte ptr [eax + 0x248], 0
0x4788CA: mov      eax, dword ptr [ebp - 4]
0x4788CD: mov      byte ptr [eax + 0xee], 1
0x4788D4: call     0x406154
0x4788D9: mov      edx, dword ptr [ebp - 4]
0x4788DC: mov      dword ptr [edx + 0x1c8], eax
0x4788E2: mov      eax, dword ptr [ebp - 4]
0x4788E5: xor      edx, edx
0x4788E7: mov      dword ptr [eax + 0x1c0], edx
0x4788ED: mov      eax, dword ptr [ebp - 4]
0x4788F0: call     0x4787a0
0x4788F5: mov      eax, dword ptr [ebp - 4]
0x4788F8: cmp      byte ptr [eax + 0xee], 0
0x4788FF: je       0x47895f
0x478901: mov      eax, dword ptr [ebp - 4]
0x478904: mov      eax, dword ptr [eax + 0x1cc]
0x47890A: mov      dword ptr [ebp - 8], eax
0x47890D: call     0x406154
0x478912: mov      edx, dword ptr [ebp - 4]
0x478915: sub      eax, dword ptr [edx + 0x1c8]
0x47891B: cmp      eax, dword ptr [ebp - 8]
0x47891E: jbe      0x47895f
0x478920: call     0x406154
0x478925: mov      edx, dword ptr [ebp - 4]
0x478928: mov      dword ptr [edx + 0x1c8], eax
0x47892E: mov      eax, dword ptr [ebp - 4]
0x478931: mov      eax, dword ptr [eax + 0x1c0]
0x478937: mov      edx, dword ptr [ebp - 4]
0x47893A: cmp      eax, dword ptr [edx + 0x1c4]
0x478940: jge      0x478955
0x478942: mov      eax, dword ptr [ebp - 4]
0x478945: inc      dword ptr [eax + 0x1c0]
0x47894B: mov      eax, dword ptr [ebp - 4]
0x47894E: call     0x4787a0
0x478953: jmp      0x47895f
0x478955: mov      eax, dword ptr [ebp - 4]
0x478958: mov      byte ptr [eax + 0xee], 0
0x47895F: mov      eax, dword ptr [ebp - 4]
0x478962: call     0x4994fc ; TActor.DMT_-6
0x478967: mov      esp, ebp
0x478969: pop      ebp
0x47896A: ret      
0x47896B: nop      
0x47896C: push     ebp
0x47896D: mov      ebp, esp
0x47896F: push     ecx
0x478970: mov      dword ptr [ebp - 4], eax
0x478973: mov      eax, dword ptr [ebp - 4]
0x478976: mov      byte ptr [eax + 0xc], 0
0x47897A: mov      eax, dword ptr [ebp - 4]
0x47897D: call     0x477de0 ; TKillingHerb.DMT_-1
0x478982: pop      ecx
0x478983: pop      ebp
0x478984: ret      
0x478985: lea      eax, [eax]
0x478988: push     ebp
0x478989: mov      ebp, esp
0x47898B: push     ecx
0x47898C: mov      dword ptr [ebp - 4], eax
0x47898F: mov      eax, dword ptr [ebp - 4]
0x478992: mov      byte ptr [eax + 0xc], 0
0x478996: mov      eax, dword ptr [ebp - 4]
0x478999: call     0x477de0 ; TKillingHerb.DMT_-1
0x47899E: pop      ecx
0x47899F: pop      ebp
0x4789A0: ret      
0x4789A1: lea      eax, [eax]
0x4789A4: push     ebp
0x4789A5: mov      ebp, esp
0x4789A7: add      esp, -8
0x4789AA: test     dl, dl
0x4789AC: je       0x4789b6
0x4789AE: add      esp, -0x10
0x4789B1: call     0x4031a4
0x4789B6: mov      byte ptr [ebp - 5], dl
0x4789B9: mov      dword ptr [ebp - 4], eax
0x4789BC: xor      edx, edx
0x4789BE: mov      eax, dword ptr [ebp - 4]
0x4789C1: call     0x4962d0 ; TActor.DMT_-4
0x4789C6: mov      eax, dword ptr [ebp - 4]
0x4789C9: mov      byte ptr [eax + 0xc], 0
0x4789CD: mov      eax, dword ptr [ebp - 4]
0x4789D0: xor      edx, edx
0x4789D2: mov      dword ptr [eax + 0x244], edx
0x4789D8: mov      eax, dword ptr [ebp - 4]
0x4789DB: mov      dword ptr [eax + 0xc0], 1
0x4789E5: mov      eax, dword ptr [ebp - 4]
0x4789E8: cmp      byte ptr [ebp - 5], 0
0x4789EC: je       0x4789fd
0x4789EE: call     0x4031fc
0x4789F3: pop      dword ptr fs:[0]
0x4789FA: add      esp, 0xc
0x4789FD: mov      eax, dword ptr [ebp - 4]
0x478A00: pop      ecx
0x478A01: pop      ecx
0x478A02: pop      ebp
0x478A03: ret      
0x478A04: push     ebp
0x478A05: mov      ebp, esp
0x478A07: add      esp, -8
0x478A0A: mov      byte ptr [ebp - 5], dl
0x478A0D: mov      dword ptr [ebp - 4], eax
0x478A10: push     1
0x478A12: mov      eax, dword ptr [ebp - 4]
0x478A15: movzx    ecx, word ptr [eax + 0xa]
0x478A19: sub      ecx, 2
0x478A1C: mov      eax, dword ptr [ebp - 4]
0x478A1F: movzx    edx, word ptr [eax + 8]
0x478A23: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x478A28: mov      eax, dword ptr [eax]
0x478A2A: call     0x49f098
0x478A2F: push     1
0x478A31: mov      eax, dword ptr [ebp - 4]
0x478A34: movzx    ecx, word ptr [eax + 0xa]
0x478A38: dec      ecx
0x478A39: mov      eax, dword ptr [ebp - 4]
0x478A3C: movzx    edx, word ptr [eax + 8]
0x478A40: inc      edx
0x478A41: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x478A46: mov      eax, dword ptr [eax]
0x478A48: call     0x49f098
0x478A4D: push     1
0x478A4F: mov      eax, dword ptr [ebp - 4]
0x478A52: movzx    ecx, word ptr [eax + 0xa]
0x478A56: sub      ecx, 2
0x478A59: mov      eax, dword ptr [ebp - 4]
0x478A5C: movzx    edx, word ptr [eax + 8]
0x478A60: inc      edx
0x478A61: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x478A66: mov      eax, dword ptr [eax]
0x478A68: call     0x49f098
0x478A6D: cmp      byte ptr [ebp - 5], 1
0x478A71: jne      0x478a79
0x478A73: mov      byte ptr [ebp - 6], 0
0x478A77: jmp      0x478a7d
0x478A79: mov      byte ptr [ebp - 6], 1
0x478A7D: mov      al, byte ptr [ebp - 6]
0x478A80: push     eax
0x478A81: mov      eax, dword ptr [ebp - 4]
0x478A84: movzx    ecx, word ptr [eax + 0xa]
0x478A88: mov      eax, dword ptr [ebp - 4]
0x478A8B: movzx    edx, word ptr [eax + 8]
0x478A8F: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x478A94: mov      eax, dword ptr [eax]
0x478A96: call     0x49f098
0x478A9B: mov      al, byte ptr [ebp - 6]
0x478A9E: push     eax
0x478A9F: mov      eax, dword ptr [ebp - 4]
0x478AA2: movzx    ecx, word ptr [eax + 0xa]
0x478AA6: dec      ecx
0x478AA7: mov      eax, dword ptr [ebp - 4]
0x478AAA: movzx    edx, word ptr [eax + 8]
0x478AAE: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x478AB3: mov      eax, dword ptr [eax]
0x478AB5: call     0x49f098
0x478ABA: mov      al, byte ptr [ebp - 6]
0x478ABD: push     eax
0x478ABE: mov      eax, dword ptr [ebp - 4]
0x478AC1: movzx    ecx, word ptr [eax + 0xa]
0x478AC5: sub      ecx, 2
0x478AC8: mov      eax, dword ptr [ebp - 4]
0x478ACB: movzx    edx, word ptr [eax + 8]
0x478ACF: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x478AD4: mov      eax, dword ptr [eax]
0x478AD6: call     0x49f098
0x478ADB: mov      al, byte ptr [ebp - 6]
0x478ADE: push     eax
0x478ADF: mov      eax, dword ptr [ebp - 4]
0x478AE2: movzx    ecx, word ptr [eax + 0xa]
0x478AE6: dec      ecx
0x478AE7: mov      eax, dword ptr [ebp - 4]
0x478AEA: movzx    edx, word ptr [eax + 8]
0x478AEE: inc      edx
0x478AEF: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x478AF4: mov      eax, dword ptr [eax]
0x478AF6: call     0x49f098
0x478AFB: mov      al, byte ptr [ebp - 6]
0x478AFE: push     eax
0x478AFF: mov      eax, dword ptr [ebp - 4]
0x478B02: movzx    ecx, word ptr [eax + 0xa]
0x478B06: sub      ecx, 2
0x478B09: mov      eax, dword ptr [ebp - 4]
0x478B0C: movzx    edx, word ptr [eax + 8]
0x478B10: inc      edx
0x478B11: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x478B16: mov      eax, dword ptr [eax]
0x478B18: call     0x49f098
0x478B1D: mov      al, byte ptr [ebp - 6]
0x478B20: push     eax
0x478B21: mov      eax, dword ptr [ebp - 4]
0x478B24: movzx    ecx, word ptr [eax + 0xa]
0x478B28: dec      ecx
0x478B29: mov      eax, dword ptr [ebp - 4]
0x478B2C: movzx    edx, word ptr [eax + 8]
0x478B30: dec      edx
0x478B31: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x478B36: mov      eax, dword ptr [eax]
0x478B38: call     0x49f098
0x478B3D: mov      al, byte ptr [ebp - 6]
0x478B40: push     eax
0x478B41: mov      eax, dword ptr [ebp - 4]
0x478B44: movzx    ecx, word ptr [eax + 0xa]
0x478B48: mov      eax, dword ptr [ebp - 4]
0x478B4B: movzx    edx, word ptr [eax + 8]
0x478B4F: dec      edx
0x478B50: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x478B55: mov      eax, dword ptr [eax]
0x478B57: call     0x49f098
0x478B5C: mov      al, byte ptr [ebp - 6]
0x478B5F: push     eax
0x478B60: mov      eax, dword ptr [ebp - 4]
0x478B63: movzx    ecx, word ptr [eax + 0xa]
0x478B67: inc      ecx
0x478B68: mov      eax, dword ptr [ebp - 4]
0x478B6B: movzx    edx, word ptr [eax + 8]
0x478B6F: dec      edx
0x478B70: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x478B75: mov      eax, dword ptr [eax]
0x478B77: call     0x49f098
0x478B7C: mov      al, byte ptr [ebp - 6]
0x478B7F: push     eax
0x478B80: mov      eax, dword ptr [ebp - 4]
0x478B83: movzx    ecx, word ptr [eax + 0xa]
0x478B87: mov      eax, dword ptr [ebp - 4]
0x478B8A: movzx    edx, word ptr [eax + 8]
0x478B8E: sub      edx, 2
0x478B91: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x478B96: mov      eax, dword ptr [eax]
0x478B98: call     0x49f098
0x478B9D: cmp      byte ptr [ebp - 5], 0
0x478BA1: jne      0x478c00
0x478BA3: push     0
0x478BA5: mov      eax, dword ptr [ebp - 4]
0x478BA8: movzx    ecx, word ptr [eax + 0xa]
0x478BAC: sub      ecx, 2
0x478BAF: mov      eax, dword ptr [ebp - 4]
0x478BB2: movzx    edx, word ptr [eax + 8]
0x478BB6: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x478BBB: mov      eax, dword ptr [eax]
0x478BBD: call     0x49f098
0x478BC2: push     0
0x478BC4: mov      eax, dword ptr [ebp - 4]
0x478BC7: movzx    ecx, word ptr [eax + 0xa]
0x478BCB: dec      ecx
0x478BCC: mov      eax, dword ptr [ebp - 4]
0x478BCF: movzx    edx, word ptr [eax + 8]
0x478BD3: inc      edx
0x478BD4: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x478BD9: mov      eax, dword ptr [eax]
0x478BDB: call     0x49f098
0x478BE0: push     0
0x478BE2: mov      eax, dword ptr [ebp - 4]
0x478BE5: movzx    ecx, word ptr [eax + 0xa]
0x478BE9: sub      ecx, 2
0x478BEC: mov      eax, dword ptr [ebp - 4]
0x478BEF: movzx    edx, word ptr [eax + 8]
0x478BF3: inc      edx
0x478BF4: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x478BF9: mov      eax, dword ptr [eax]
0x478BFB: call     0x49f098
0x478C00: pop      ecx
0x478C01: pop      ecx
0x478C02: pop      ebp
0x478C03: ret      

; === TCastleDoor.DMT_-13 @ 0x478C04 ===
0x478C04: push     ebp
0x478C05: mov      ebp, esp
0x478C07: add      esp, -8
0x478C0A: mov      dword ptr [ebp - 4], eax
0x478C0D: mov      eax, dword ptr [ebp - 4]
0x478C10: call     0x497df4 ; TActor.DMT_-13
0x478C15: mov      eax, dword ptr [ebp - 4]
0x478C18: movzx    eax, word ptr [eax + 0x14]
0x478C1C: call     0x495ebc
0x478C21: mov      dword ptr [ebp - 8], eax
0x478C24: mov      eax, dword ptr [ebp - 4]
0x478C27: cmp      byte ptr [eax + 0xee], 0
0x478C2E: je       0x478c68
0x478C30: mov      eax, dword ptr [ebp - 4]
0x478C33: add      eax, 0x24c
0x478C38: push     eax
0x478C39: mov      eax, dword ptr [ebp - 4]
0x478C3C: lea      ecx, [eax + 0x248]
0x478C42: mov      eax, dword ptr [ebp - 4]
0x478C45: mov      edx, dword ptr [eax + 0x1b8]
0x478C4B: mov      eax, dword ptr [ebp - 4]
0x478C4E: sub      edx, dword ptr [eax + 0x1b0]
0x478C54: add      edx, 0x78
0x478C57: mov      eax, dword ptr [ebp - 8]
0x478C5A: call     0x453a48
0x478C5F: mov      edx, dword ptr [ebp - 4]
0x478C62: mov      dword ptr [edx + 0x244], eax
0x478C68: pop      ecx
0x478C69: pop      ecx
0x478C6A: pop      ebp
0x478C6B: ret      

; === TCastleDoor.DMT_-1 @ 0x478C6C ===
0x478C6C: push     ebp
0x478C6D: mov      ebp, esp
0x478C6F: add      esp, -0xc
0x478C72: mov      dword ptr [ebp - 4], eax
0x478C75: mov      eax, dword ptr [ebp - 4]
0x478C78: mov      byte ptr [eax + 0xee], 0
0x478C7F: mov      eax, dword ptr [ebp - 4]
0x478C82: mov      dword ptr [eax + 0x1b8], 0xffffffff
0x478C8C: mov      eax, dword ptr [ebp - 4]
0x478C8F: movzx    eax, word ptr [eax + 0x14]
0x478C93: call     0x496120
0x478C98: mov      edx, dword ptr [ebp - 4]
0x478C9B: mov      dword ptr [edx + 0xe0], eax
0x478CA1: mov      eax, dword ptr [ebp - 4]
0x478CA4: movzx    edx, word ptr [eax + 0x14]
0x478CA8: mov      eax, dword ptr [ebp - 4]
0x478CAB: movzx    eax, byte ptr [eax + 0xe]
0x478CAF: call     0x495ca8
0x478CB4: mov      dword ptr [ebp - 8], eax
0x478CB7: cmp      dword ptr [ebp - 8], 0
0x478CBB: je       0x479001
0x478CC1: mov      eax, dword ptr [ebp - 4]
0x478CC4: add      eax, 0x28
0x478CC7: mov      edx, 0x479010 ; " "
0x478CCC: call     0x403aac
0x478CD1: mov      eax, dword ptr [ebp - 4]
0x478CD4: mov      eax, dword ptr [eax + 0x148]
0x478CDA: add      eax, -0x14
0x478CDD: cmp      eax, 0xe
0x478CE0: ja       0x478f14
0x478CE6: mov      al, byte ptr [eax + 0x478cf3]
0x478CEC: jmp      dword ptr [eax*4 + 0x478d02]
0x478CF3: add      eax, dword ptr [eax + eax]
0x478CF6: add      byte ptr [eax], al
0x478CF8: add      byte ptr [eax], al
0x478CFA: add      byte ptr [eax], al
0x478CFC: add      byte ptr [eax], al
0x478CFE: add      al, byte ptr [0x8f140100]
0x478D04: inc      edi
0x478D05: add      byte ptr [edx], bl
0x478D07: lea      eax, [edi]
0x478D0A: xchg     ecx, eax
0x478D0B: lea      eax, [edi]
0x478D0E: jle      0x8ead8d5b
0x478D14: inc      edi
0x478D15: add      dh, cl
0x478D17: mov      es, word ptr [edi]
0x478D1A: mov      eax, dword ptr [ebp - 8]
0x478D1D: movzx    eax, word ptr [eax + 0x32]
0x478D21: mov      edx, dword ptr [ebp - 4]
0x478D24: mov      dword ptr [edx + 0x1b0], eax
0x478D2A: mov      eax, dword ptr [ebp - 8]
0x478D2D: movzx    eax, word ptr [eax + 0x34]
0x478D31: mov      edx, dword ptr [ebp - 4]
0x478D34: add      eax, dword ptr [edx + 0x1b0]
0x478D3A: dec      eax
0x478D3B: mov      edx, dword ptr [ebp - 4]
0x478D3E: mov      dword ptr [edx + 0x1b4], eax
0x478D44: mov      eax, dword ptr [ebp - 8]
0x478D47: movzx    eax, word ptr [eax + 0x38]
0x478D4B: mov      edx, dword ptr [ebp - 4]
0x478D4E: mov      dword ptr [edx + 0x1d0], eax
0x478D54: call     0x406154
0x478D59: mov      edx, dword ptr [ebp - 4]
0x478D5C: mov      dword ptr [edx + 0x1d4], eax
0x478D62: push     0
0x478D64: push     1
0x478D66: mov      eax, dword ptr [ebp - 4]
0x478D69: xor      edx, edx
0x478D6B: mov      dl, byte ptr [eax + 0xc]
0x478D6E: xor      ecx, ecx
0x478D70: mov      eax, dword ptr [ebp - 4]
0x478D73: call     0x497610
0x478D78: mov      eax, dword ptr [ebp - 4]
0x478D7B: mov      byte ptr [eax + 0xee], 1
0x478D82: mov      dl, 2
0x478D84: mov      eax, dword ptr [ebp - 4]
0x478D87: call     0x478a04
0x478D8C: jmp      0x479001
0x478D91: mov      eax, dword ptr [ebp - 8]
0x478D94: movzx    eax, word ptr [eax + 0x2a]
0x478D98: mov      edx, dword ptr [ebp - 8]
0x478D9B: movzx    edx, word ptr [edx + 0x2c]
0x478D9F: add      eax, edx
0x478DA1: mov      edx, dword ptr [ebp - 4]
0x478DA4: movzx    edx, byte ptr [edx + 0xc]
0x478DA8: imul     edx
0x478DAA: mov      edx, dword ptr [ebp - 8]
0x478DAD: movzx    edx, word ptr [edx + 0x28]
0x478DB1: add      eax, edx
0x478DB3: mov      edx, dword ptr [ebp - 4]
0x478DB6: mov      dword ptr [edx + 0x1b0], eax
0x478DBC: mov      eax, dword ptr [ebp - 8]
0x478DBF: movzx    eax, word ptr [eax + 0x2a]
0x478DC3: mov      edx, dword ptr [ebp - 4]
0x478DC6: add      eax, dword ptr [edx + 0x1b0]
0x478DCC: dec      eax
0x478DCD: mov      edx, dword ptr [ebp - 4]
0x478DD0: mov      dword ptr [edx + 0x1b4], eax
0x478DD6: mov      eax, dword ptr [ebp - 8]
0x478DD9: movzx    eax, word ptr [eax + 6]
0x478DDD: mov      edx, dword ptr [ebp - 4]
0x478DE0: mov      dword ptr [edx + 0x1d0], eax
0x478DE6: call     0x406154
0x478DEB: mov      edx, dword ptr [ebp - 4]
0x478DEE: mov      dword ptr [edx + 0x1d4], eax
0x478DF4: push     0
0x478DF6: push     1
0x478DF8: mov      eax, dword ptr [ebp - 4]
0x478DFB: xor      edx, edx
0x478DFD: mov      dl, byte ptr [eax + 0xc]
0x478E00: xor      ecx, ecx
0x478E02: mov      eax, dword ptr [ebp - 4]
0x478E05: call     0x497610
0x478E0A: jmp      0x479001
0x478E0F: mov      eax, dword ptr [ebp - 8]
0x478E12: movzx    eax, word ptr [eax + 0x14]
0x478E16: mov      edx, dword ptr [ebp - 4]
0x478E19: mov      dword ptr [edx + 0x1b0], eax
0x478E1F: mov      eax, dword ptr [ebp - 8]
0x478E22: movzx    eax, word ptr [eax + 0x16]
0x478E26: mov      edx, dword ptr [ebp - 4]
0x478E29: add      eax, dword ptr [edx + 0x1b0]
0x478E2F: dec      eax
0x478E30: mov      edx, dword ptr [ebp - 4]
0x478E33: mov      dword ptr [edx + 0x1b4], eax
0x478E39: mov      eax, dword ptr [ebp - 8]
0x478E3C: movzx    eax, word ptr [eax + 0x1a]
0x478E40: mov      edx, dword ptr [ebp - 4]
0x478E43: mov      dword ptr [edx + 0x1d0], eax
0x478E49: call     0x406154
0x478E4E: mov      edx, dword ptr [ebp - 4]
0x478E51: mov      dword ptr [edx + 0x1d4], eax
0x478E57: xor      edx, edx
0x478E59: mov      eax, dword ptr [ebp - 4]
0x478E5C: call     0x478a04
0x478E61: jmp      0x479001
0x478E66: mov      eax, dword ptr [ebp - 8]
0x478E69: movzx    eax, word ptr [eax + 0x1e]
0x478E6D: mov      edx, dword ptr [ebp - 4]
0x478E70: mov      dword ptr [edx + 0x1b0], eax
0x478E76: mov      eax, dword ptr [ebp - 8]
0x478E79: movzx    eax, word ptr [eax + 0x20]
0x478E7D: mov      edx, dword ptr [ebp - 4]
0x478E80: add      eax, dword ptr [edx + 0x1b0]
0x478E86: dec      eax
0x478E87: mov      edx, dword ptr [ebp - 4]
0x478E8A: mov      dword ptr [edx + 0x1b4], eax
0x478E90: mov      eax, dword ptr [ebp - 8]
0x478E93: movzx    eax, word ptr [eax + 0x24]
0x478E97: mov      edx, dword ptr [ebp - 4]
0x478E9A: mov      dword ptr [edx + 0x1d0], eax
0x478EA0: call     0x406154
0x478EA5: mov      edx, dword ptr [ebp - 4]
0x478EA8: mov      dword ptr [edx + 0x1d4], eax
0x478EAE: mov      eax, dword ptr [ebp - 4]
0x478EB1: mov      byte ptr [eax + 0x258], 0
0x478EB8: mov      eax, dword ptr [ebp - 4]
0x478EBB: mov      byte ptr [eax + 0x5e], 1
0x478EBF: mov      dl, 1
0x478EC1: mov      eax, dword ptr [ebp - 4]
0x478EC4: call     0x478a04
0x478EC9: jmp      0x479001
0x478ECE: mov      eax, dword ptr [ebp - 8]
0x478ED1: movzx    eax, word ptr [eax + 0x32]
0x478ED5: mov      edx, dword ptr [ebp - 8]
0x478ED8: movzx    edx, word ptr [edx + 0x34]
0x478EDC: add      eax, edx
0x478EDE: dec      eax
0x478EDF: mov      edx, dword ptr [ebp - 4]
0x478EE2: mov      dword ptr [edx + 0x1b0], eax
0x478EE8: mov      eax, dword ptr [ebp - 4]
0x478EEB: mov      eax, dword ptr [eax + 0x1b0]
0x478EF1: mov      edx, dword ptr [ebp - 4]
0x478EF4: mov      dword ptr [edx + 0x1b4], eax
0x478EFA: mov      eax, dword ptr [ebp - 4]
0x478EFD: xor      edx, edx
0x478EFF: mov      dword ptr [eax + 0x1f4], edx
0x478F05: mov      dl, 2
0x478F07: mov      eax, dword ptr [ebp - 4]
0x478F0A: call     0x478a04
0x478F0F: jmp      0x479001
0x478F14: mov      eax, dword ptr [ebp - 4]
0x478F17: cmp      byte ptr [eax + 0xc], 3
0x478F1B: jae      0x478fb9
0x478F21: mov      eax, dword ptr [ebp - 8]
0x478F24: movzx    eax, word ptr [eax + 2]
0x478F28: mov      edx, dword ptr [ebp - 8]
0x478F2B: movzx    edx, word ptr [edx + 4]
0x478F2F: add      eax, edx
0x478F31: mov      edx, dword ptr [ebp - 4]
0x478F34: movzx    edx, byte ptr [edx + 0xc]
0x478F38: imul     edx
0x478F3A: mov      edx, dword ptr [ebp - 8]
0x478F3D: movzx    edx, word ptr [edx]
0x478F40: add      eax, edx
0x478F42: mov      edx, dword ptr [ebp - 4]
0x478F45: mov      dword ptr [edx + 0x1b0], eax
0x478F4B: mov      eax, dword ptr [ebp - 4]
0x478F4E: mov      eax, dword ptr [eax + 0x1b0]
0x478F54: mov      edx, dword ptr [ebp - 4]
0x478F57: mov      dword ptr [edx + 0x1b4], eax
0x478F5D: mov      eax, dword ptr [ebp - 8]
0x478F60: movzx    eax, word ptr [eax + 6]
0x478F64: mov      edx, dword ptr [ebp - 4]
0x478F67: mov      dword ptr [edx + 0x1d0], eax
0x478F6D: call     0x406154
0x478F72: mov      edx, dword ptr [ebp - 4]
0x478F75: mov      dword ptr [edx + 0x1d4], eax
0x478F7B: mov      eax, dword ptr [ebp - 4]
0x478F7E: xor      edx, edx
0x478F80: mov      dword ptr [eax + 0x1f4], edx
0x478F86: push     0
0x478F88: push     1
0x478F8A: mov      eax, dword ptr [ebp - 4]
0x478F8D: xor      edx, edx
0x478F8F: mov      dl, byte ptr [eax + 0xc]
0x478F92: xor      ecx, ecx
0x478F94: mov      eax, dword ptr [ebp - 4]
0x478F97: call     0x497610
0x478F9C: mov      eax, dword ptr [ebp - 4]
0x478F9F: mov      byte ptr [eax + 0x258], 0
0x478FA6: mov      eax, dword ptr [ebp - 4]
0x478FA9: mov      byte ptr [eax + 0x5e], 1
0x478FAD: mov      dl, 1
0x478FAF: mov      eax, dword ptr [ebp - 4]
0x478FB2: call     0x478a04
0x478FB7: jmp      0x479001
0x478FB9: mov      eax, dword ptr [ebp - 8]
0x478FBC: movzx    eax, word ptr [eax + 0x1e]
0x478FC0: mov      edx, dword ptr [ebp - 4]
0x478FC3: mov      dword ptr [edx + 0x1b0], eax
0x478FC9: mov      eax, dword ptr [ebp - 4]
0x478FCC: mov      eax, dword ptr [eax + 0x1b0]
0x478FD2: mov      edx, dword ptr [ebp - 4]
0x478FD5: mov      dword ptr [edx + 0x1b4], eax
0x478FDB: mov      eax, dword ptr [ebp - 4]
0x478FDE: xor      edx, edx
0x478FE0: mov      dword ptr [eax + 0x1f4], edx
0x478FE6: mov      eax, dword ptr [ebp - 4]
0x478FE9: mov      byte ptr [eax + 0x258], 1
0x478FF0: mov      eax, dword ptr [ebp - 4]
0x478FF3: mov      byte ptr [eax + 0x5e], 0
0x478FF7: xor      edx, edx
0x478FF9: mov      eax, dword ptr [ebp - 4]
0x478FFC: call     0x478a04
0x479001: mov      esp, ebp
0x479003: pop      ebp
0x479004: ret      
0x479005: add      byte ptr [eax], al
0x479007: add      bh, bh
0x47900B: inc      dword ptr [ecx]
0x47900D: add      byte ptr [eax], al
0x47900F: add      byte ptr [eax], ah
0x479011: add      byte ptr [eax], al
0x479013: add      byte ptr [ebp - 0x75], dl
0x479016: in       al, dx
0x479017: add      esp, -0x10
0x47901A: mov      byte ptr [ebp - 0xd], dl
0x47901D: mov      dword ptr [ebp - 4], eax
0x479020: mov      eax, dword ptr [ebp - 4]
0x479023: movzx    eax, word ptr [eax + 0x14]
0x479027: call     0x496120
0x47902C: mov      edx, dword ptr [ebp - 4]
0x47902F: mov      dword ptr [edx + 0xe0], eax
0x479035: mov      eax, dword ptr [ebp - 4]
0x479038: movzx    edx, word ptr [eax + 0x14]
0x47903C: mov      eax, dword ptr [ebp - 4]
0x47903F: movzx    eax, byte ptr [eax + 0xe]
0x479043: call     0x495ca8
0x479048: mov      dword ptr [ebp - 0xc], eax
0x47904B: cmp      dword ptr [ebp - 0xc], 0
0x47904F: je       0x4790d7
0x479055: mov      eax, dword ptr [ebp - 4]
0x479058: cmp      byte ptr [eax + 0x20], 0
0x47905C: je       0x479081
0x47905E: mov      eax, dword ptr [ebp - 0xc]
0x479061: movzx    eax, word ptr [eax + 0x32]
0x479065: mov      edx, dword ptr [ebp - 0xc]
0x479068: movzx    edx, word ptr [edx + 0x34]
0x47906C: add      eax, edx
0x47906E: dec      eax
0x47906F: mov      dword ptr [ebp - 8], eax
0x479072: mov      eax, dword ptr [ebp - 4]
0x479075: mov      dword ptr [eax + 0xc0], 2
0x47907F: jmp      0x4790d7
0x479081: mov      eax, dword ptr [ebp - 4]
0x479084: cmp      byte ptr [eax + 0x258], 0
0x47908B: je       0x4790a6
0x47908D: mov      eax, dword ptr [ebp - 4]
0x479090: mov      dword ptr [eax + 0xc0], 2
0x47909A: mov      eax, dword ptr [ebp - 0xc]
0x47909D: movzx    eax, word ptr [eax + 0x1e]
0x4790A1: mov      dword ptr [ebp - 8], eax
0x4790A4: jmp      0x4790d7
0x4790A6: mov      eax, dword ptr [ebp - 4]
0x4790A9: mov      dword ptr [eax + 0xc0], 1
0x4790B3: mov      eax, dword ptr [ebp - 0xc]
0x4790B6: movzx    eax, word ptr [eax + 2]
0x4790BA: mov      edx, dword ptr [ebp - 0xc]
0x4790BD: movzx    edx, word ptr [edx + 4]
0x4790C1: add      eax, edx
0x4790C3: mov      edx, dword ptr [ebp - 4]
0x4790C6: movzx    edx, byte ptr [edx + 0xc]
0x4790CA: imul     edx
0x4790CC: mov      edx, dword ptr [ebp - 0xc]
0x4790CF: movzx    edx, word ptr [edx]
0x4790D2: add      eax, edx
0x4790D4: mov      dword ptr [ebp - 8], eax
0x4790D7: mov      eax, dword ptr [ebp - 8]
0x4790DA: mov      esp, ebp
0x4790DC: pop      ebp
0x4790DD: ret      
0x4790DE: mov      eax, eax
0x4790E0: push     ebp
0x4790E1: mov      ebp, esp
0x4790E3: push     ecx
0x4790E4: mov      dword ptr [ebp - 4], eax
0x4790E7: mov      eax, dword ptr [ebp - 4]
0x4790EA: cmp      dword ptr [eax + 0x148], 0x14
0x4790F1: jne      0x479104
0x4790F3: mov      eax, dword ptr [ebp - 4]
0x4790F6: mov      byte ptr [eax + 0x258], 1
0x4790FD: mov      eax, dword ptr [ebp - 4]
0x479100: mov      byte ptr [eax + 0x5e], 0
0x479104: pop      ecx
0x479105: pop      ebp
0x479106: ret      
0x479107: nop      
0x479108: push     ebp
0x479109: mov      ebp, esp
0x47910B: push     ecx
0x47910C: mov      dword ptr [ebp - 4], eax
0x47910F: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x479114: mov      eax, dword ptr [eax]
0x479116: mov      eax, dword ptr [eax + 0x2ae88]
0x47911C: mov      edx, dword ptr [ebp - 4]
0x47911F: cmp      eax, dword ptr [edx + 0x250]
0x479125: jne      0x47913f
0x479127: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x47912C: mov      eax, dword ptr [eax]
0x47912E: mov      eax, dword ptr [eax + 0x2ae8c]
0x479134: mov      edx, dword ptr [ebp - 4]
0x479137: cmp      eax, dword ptr [edx + 0x254]
0x47913D: je       0x479176
0x47913F: mov      eax, dword ptr [ebp - 4]
0x479142: cmp      byte ptr [eax + 0x20], 0
0x479146: je       0x479154
0x479148: mov      dl, 2
0x47914A: mov      eax, dword ptr [ebp - 4]
0x47914D: call     0x478a04
0x479152: jmp      0x479176
0x479154: mov      eax, dword ptr [ebp - 4]
0x479157: cmp      byte ptr [eax + 0x258], 0
0x47915E: je       0x47916c
0x479160: xor      edx, edx
0x479162: mov      eax, dword ptr [ebp - 4]
0x479165: call     0x478a04
0x47916A: jmp      0x479176
0x47916C: mov      dl, 1
0x47916E: mov      eax, dword ptr [ebp - 4]
0x479171: call     0x478a04
0x479176: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x47917B: mov      eax, dword ptr [eax]
0x47917D: mov      eax, dword ptr [eax + 0x2ae88]
0x479183: mov      edx, dword ptr [ebp - 4]
0x479186: mov      dword ptr [edx + 0x250], eax
0x47918C: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x479191: mov      eax, dword ptr [eax]
0x479193: mov      eax, dword ptr [eax + 0x2ae8c]
0x479199: mov      edx, dword ptr [ebp - 4]
0x47919C: mov      dword ptr [edx + 0x254], eax
0x4791A2: mov      eax, dword ptr [ebp - 4]
0x4791A5: call     0x4994fc ; TActor.DMT_-6
0x4791AA: pop      ecx
0x4791AB: pop      ebp
0x4791AC: ret      
0x4791AD: lea      eax, [eax]
0x4791B0: push     ebp
0x4791B1: mov      ebp, esp
0x4791B3: add      esp, -0xc
0x4791B6: mov      dword ptr [ebp - 0xc], ecx
0x4791B9: mov      dword ptr [ebp - 8], edx
0x4791BC: mov      dword ptr [ebp - 4], eax
0x4791BF: mov      eax, dword ptr [ebp + 0xc]
0x4791C2: push     eax
0x4791C3: mov      al, byte ptr [ebp + 8]
0x4791C6: push     eax
0x4791C7: mov      ecx, dword ptr [ebp - 0xc]
0x4791CA: mov      edx, dword ptr [ebp - 8]
0x4791CD: mov      eax, dword ptr [ebp - 4]
0x4791D0: call     0x4981f4 ; TActor.DMT_-14
0x4791D5: mov      eax, dword ptr [ebp - 4]
0x4791D8: cmp      byte ptr [eax + 0xee], 0
0x4791DF: je       0x479231
0x4791E1: cmp      byte ptr [ebp + 8], 0
0x4791E5: jne      0x479231
0x4791E7: mov      eax, dword ptr [ebp - 4]
0x4791EA: cmp      dword ptr [eax + 0x244], 0
0x4791F1: je       0x479231
0x4791F3: mov      eax, dword ptr [ebp - 4]
0x4791F6: mov      eax, dword ptr [eax + 0x244]
0x4791FC: push     eax
0x4791FD: push     1
0x4791FF: mov      ecx, dword ptr [ebp + 0xc]
0x479202: mov      eax, dword ptr [ebp - 4]
0x479205: add      ecx, dword ptr [eax + 0x24c]
0x47920B: mov      eax, dword ptr [ebp - 4]
0x47920E: add      ecx, dword ptr [eax + 0x9c]
0x479214: mov      edx, dword ptr [ebp - 0xc]
0x479217: mov      eax, dword ptr [ebp - 4]
0x47921A: add      edx, dword ptr [eax + 0x248]
0x479220: mov      eax, dword ptr [ebp - 4]
0x479223: add      edx, dword ptr [eax + 0x98]
0x479229: mov      eax, dword ptr [ebp - 8]
0x47922C: call     0x455578
0x479231: mov      esp, ebp
0x479233: pop      ebp
0x479234: ret      8
0x479237: nop      
0x479238: push     ebp
0x479239: mov      ebp, esp
0x47923B: add      esp, -8
0x47923E: test     dl, dl
0x479240: je       0x47924a
0x479242: add      esp, -0x10
0x479245: call     0x4031a4
0x47924A: mov      byte ptr [ebp - 5], dl
0x47924D: mov      dword ptr [ebp - 4], eax
0x479250: xor      edx, edx
0x479252: mov      eax, dword ptr [ebp - 4]
0x479255: call     0x4962d0 ; TActor.DMT_-4
0x47925A: mov      eax, dword ptr [ebp - 4]
0x47925D: mov      byte ptr [eax + 0xc], 0
0x479261: mov      eax, dword ptr [ebp - 4]
0x479264: xor      edx, edx
0x479266: mov      dword ptr [eax + 0x244], edx
0x47926C: mov      eax, dword ptr [ebp - 4]
0x47926F: xor      edx, edx
0x479271: mov      dword ptr [eax + 0x248], edx
0x479277: mov      eax, dword ptr [ebp - 4]
0x47927A: mov      byte ptr [eax + 0x260], 0
0x479281: mov      eax, dword ptr [ebp - 4]
0x479284: cmp      byte ptr [ebp - 5], 0
0x479288: je       0x479299
0x47928A: call     0x4031fc
0x47928F: pop      dword ptr fs:[0]
0x479296: add      esp, 0xc
0x479299: mov      eax, dword ptr [ebp - 4]
0x47929C: pop      ecx
0x47929D: pop      ecx
0x47929E: pop      ebp
0x47929F: ret      

; === TWallStructure.DMT_-1 @ 0x4792A0 ===
0x4792A0: push     ebp
0x4792A1: mov      ebp, esp
0x4792A3: add      esp, -0xc
0x4792A6: mov      dword ptr [ebp - 4], eax
0x4792A9: mov      eax, dword ptr [ebp - 4]
0x4792AC: mov      byte ptr [eax + 0xee], 0
0x4792B3: mov      eax, dword ptr [ebp - 4]
0x4792B6: mov      dword ptr [eax + 0x1b8], 0xffffffff
0x4792C0: mov      eax, dword ptr [ebp - 4]
0x4792C3: movzx    eax, word ptr [eax + 0x14]
0x4792C7: call     0x496120
0x4792CC: mov      edx, dword ptr [ebp - 4]
0x4792CF: mov      dword ptr [edx + 0xe0], eax
0x4792D5: mov      eax, dword ptr [ebp - 4]
0x4792D8: movzx    edx, word ptr [eax + 0x14]
0x4792DC: mov      eax, dword ptr [ebp - 4]
0x4792DF: movzx    eax, byte ptr [eax + 0xe]
0x4792E3: call     0x495ca8
0x4792E8: mov      dword ptr [ebp - 8], eax
0x4792EB: cmp      dword ptr [ebp - 8], 0
0x4792EF: je       0x4794db
0x4792F5: mov      eax, dword ptr [ebp - 4]
0x4792F8: add      eax, 0x28
0x4792FB: mov      edx, 0x4794e8 ; " "
0x479300: call     0x403aac
0x479305: mov      eax, dword ptr [ebp - 4]
0x479308: xor      edx, edx
0x47930A: mov      dword ptr [eax + 0x25c], edx
0x479310: mov      eax, dword ptr [ebp - 4]
0x479313: mov      byte ptr [eax + 0xee], 0
0x47931A: mov      eax, dword ptr [ebp - 4]
0x47931D: mov      eax, dword ptr [eax + 0x148]
0x479323: sub      eax, 0x14
0x479326: je       0x4793ff
0x47932C: sub      eax, 0xc
0x47932F: je       0x4793c3
0x479335: sub      eax, 2
0x479338: jne      0x47946b
0x47933E: mov      eax, dword ptr [ebp - 8]
0x479341: movzx    eax, word ptr [eax + 0x32]
0x479345: mov      edx, dword ptr [ebp - 4]
0x479348: mov      dword ptr [edx + 0x1b0], eax
0x47934E: mov      eax, dword ptr [ebp - 8]
0x479351: movzx    eax, word ptr [eax + 0x34]
0x479355: mov      edx, dword ptr [ebp - 4]
0x479358: add      eax, dword ptr [edx + 0x1b0]
0x47935E: dec      eax
0x47935F: mov      edx, dword ptr [ebp - 4]
0x479362: mov      dword ptr [edx + 0x1b4], eax
0x479368: mov      eax, dword ptr [ebp - 8]
0x47936B: movzx    eax, word ptr [eax + 0x38]
0x47936F: mov      edx, dword ptr [ebp - 4]
0x479372: mov      dword ptr [edx + 0x1d0], eax
0x479378: call     0x406154
0x47937D: mov      edx, dword ptr [ebp - 4]
0x479380: mov      dword ptr [edx + 0x1d4], eax
0x479386: mov      eax, dword ptr [ebp - 8]
0x479389: movzx    eax, word ptr [eax]
0x47938C: mov      edx, dword ptr [ebp - 4]
0x47938F: movzx    edx, byte ptr [edx + 0xc]
0x479393: add      eax, edx
0x479395: mov      edx, dword ptr [ebp - 4]
0x479398: mov      dword ptr [edx + 0x25c], eax
0x47939E: push     0
0x4793A0: push     1
0x4793A2: mov      eax, dword ptr [ebp - 4]
0x4793A5: xor      edx, edx
0x4793A7: mov      dl, byte ptr [eax + 0xc]
0x4793AA: xor      ecx, ecx
0x4793AC: mov      eax, dword ptr [ebp - 4]
0x4793AF: call     0x497610
0x4793B4: mov      eax, dword ptr [ebp - 4]
0x4793B7: mov      byte ptr [eax + 0xee], 1
0x4793BE: jmp      0x4794db
0x4793C3: mov      eax, dword ptr [ebp - 8]
0x4793C6: movzx    eax, word ptr [eax + 0x32]
0x4793CA: mov      edx, dword ptr [ebp - 8]
0x4793CD: movzx    edx, word ptr [edx + 0x34]
0x4793D1: add      eax, edx
0x4793D3: dec      eax
0x4793D4: mov      edx, dword ptr [ebp - 4]
0x4793D7: mov      dword ptr [edx + 0x1b0], eax
0x4793DD: mov      eax, dword ptr [ebp - 4]
0x4793E0: mov      eax, dword ptr [eax + 0x1b0]
0x4793E6: mov      edx, dword ptr [ebp - 4]
0x4793E9: mov      dword ptr [edx + 0x1b4], eax
0x4793EF: mov      eax, dword ptr [ebp - 4]
0x4793F2: xor      edx, edx
0x4793F4: mov      dword ptr [eax + 0x1f4], edx
0x4793FA: jmp      0x4794db
0x4793FF: mov      eax, dword ptr [ebp - 8]
0x479402: movzx    eax, word ptr [eax + 0x32]
0x479406: mov      edx, dword ptr [ebp - 4]
0x479409: mov      dword ptr [edx + 0x1b0], eax
0x47940F: mov      eax, dword ptr [ebp - 8]
0x479412: movzx    eax, word ptr [eax + 0x34]
0x479416: mov      edx, dword ptr [ebp - 4]
0x479419: add      eax, dword ptr [edx + 0x1b0]
0x47941F: dec      eax
0x479420: mov      edx, dword ptr [ebp - 4]
0x479423: mov      dword ptr [edx + 0x1b4], eax
0x479429: mov      eax, dword ptr [ebp - 8]
0x47942C: movzx    eax, word ptr [eax + 0x38]
0x479430: mov      edx, dword ptr [ebp - 4]
0x479433: mov      dword ptr [edx + 0x1d0], eax
0x479439: call     0x406154
0x47943E: mov      edx, dword ptr [ebp - 4]
0x479441: mov      dword ptr [edx + 0x1d4], eax
0x479447: mov      eax, dword ptr [ebp - 8]
0x47944A: movzx    eax, word ptr [eax]
0x47944D: mov      edx, dword ptr [ebp - 4]
0x479450: movzx    edx, byte ptr [edx + 0xc]
0x479454: add      eax, edx
0x479456: mov      edx, dword ptr [ebp - 4]
0x479459: mov      dword ptr [edx + 0x25c], eax
0x47945F: mov      eax, dword ptr [ebp - 4]
0x479462: mov      byte ptr [eax + 0xee], 1
0x479469: jmp      0x4794db
0x47946B: mov      eax, dword ptr [ebp - 8]
0x47946E: movzx    eax, word ptr [eax]
0x479471: mov      edx, dword ptr [ebp - 4]
0x479474: movzx    edx, byte ptr [edx + 0xc]
0x479478: add      eax, edx
0x47947A: mov      edx, dword ptr [ebp - 4]
0x47947D: mov      dword ptr [edx + 0x1b0], eax
0x479483: mov      eax, dword ptr [ebp - 4]
0x479486: mov      eax, dword ptr [eax + 0x1b0]
0x47948C: mov      edx, dword ptr [ebp - 4]
0x47948F: mov      dword ptr [edx + 0x1b4], eax
0x479495: mov      eax, dword ptr [ebp - 8]
0x479498: movzx    eax, word ptr [eax + 6]
0x47949C: mov      edx, dword ptr [ebp - 4]
0x47949F: mov      dword ptr [edx + 0x1d0], eax
0x4794A5: call     0x406154
0x4794AA: mov      edx, dword ptr [ebp - 4]
0x4794AD: mov      dword ptr [edx + 0x1d4], eax
0x4794B3: mov      eax, dword ptr [ebp - 4]
0x4794B6: xor      edx, edx
0x4794B8: mov      dword ptr [eax + 0x1f4], edx
0x4794BE: push     0
0x4794C0: push     1
0x4794C2: mov      eax, dword ptr [ebp - 4]
0x4794C5: xor      edx, edx
0x4794C7: mov      dl, byte ptr [eax + 0xc]
0x4794CA: xor      ecx, ecx
0x4794CC: mov      eax, dword ptr [ebp - 4]
0x4794CF: call     0x497610
0x4794D4: mov      eax, dword ptr [ebp - 4]
0x4794D7: mov      byte ptr [eax + 0x5e], 1
0x4794DB: mov      esp, ebp
0x4794DD: pop      ebp
0x4794DE: ret      
0x4794DF: add      bh, bh
0x4794E3: inc      dword ptr [ecx]
0x4794E5: add      byte ptr [eax], al
0x4794E7: add      byte ptr [eax], ah
0x4794E9: add      byte ptr [eax], al
0x4794EB: add      byte ptr [ebp - 0x75], dl
0x4794EE: in       al, dx
0x4794EF: add      esp, -8
0x4794F2: mov      dword ptr [ebp - 4], eax
0x4794F5: mov      eax, dword ptr [ebp - 4]
0x4794F8: movzx    eax, word ptr [eax + 0x14]
0x4794FC: call     0x495ebc
0x479501: mov      dword ptr [ebp - 8], eax
0x479504: mov      eax, dword ptr [ebp - 4]
0x479507: cmp      dword ptr [eax + 0x25c], 0
0x47950E: jle      0x47954c
0x479510: mov      eax, dword ptr [ebp - 4]
0x479513: add      eax, 0xac
0x479518: push     eax
0x479519: mov      eax, dword ptr [ebp - 4]
0x47951C: movzx    eax, word ptr [eax + 0x14]
0x479520: call     0x496120
0x479525: mov      edx, eax
0x479527: mov      eax, dword ptr [ebp - 4]
0x47952A: add      edx, dword ptr [eax + 0x25c]
0x479530: mov      eax, dword ptr [ebp - 4]
0x479533: lea      ecx, [eax + 0xa0]
0x479539: mov      eax, dword ptr [ebp - 8]
0x47953C: call     0x453a48
0x479541: mov      edx, dword ptr [ebp - 4]
0x479544: mov      dword ptr [edx + 0x140], eax
0x47954A: jmp      0x479554
0x47954C: mov      eax, dword ptr [ebp - 4]
0x47954F: call     0x497df4 ; TActor.DMT_-13
0x479554: mov      eax, dword ptr [ebp - 4]
0x479557: add      eax, 0x258
0x47955C: push     eax
0x47955D: mov      eax, dword ptr [ebp - 4]
0x479560: movzx    eax, word ptr [eax + 0x14]
0x479564: call     0x496120
0x479569: mov      edx, eax
0x47956B: add      edx, 8
0x47956E: mov      eax, dword ptr [ebp - 4]
0x479571: movzx    eax, byte ptr [eax + 0xc]
0x479575: add      edx, eax
0x479577: mov      eax, dword ptr [ebp - 4]
0x47957A: lea      ecx, [eax + 0x254]
0x479580: mov      eax, dword ptr [ebp - 8]
0x479583: call     0x453a48
0x479588: mov      edx, dword ptr [ebp - 4]
0x47958B: mov      dword ptr [edx + 0x248], eax
0x479591: mov      eax, dword ptr [ebp - 4]
0x479594: cmp      byte ptr [eax + 0xee], 0
0x47959B: je       0x479624
0x4795A1: mov      eax, dword ptr [ebp - 4]
0x4795A4: cmp      word ptr [eax + 0x14], 0x385
0x4795AA: jne      0x4795e9
0x4795AC: mov      eax, dword ptr [ebp - 4]
0x4795AF: add      eax, 0x250
0x4795B4: push     eax
0x4795B5: mov      eax, dword ptr [ebp - 4]
0x4795B8: lea      ecx, [eax + 0x24c]
0x4795BE: mov      eax, dword ptr [ebp - 4]
0x4795C1: mov      edx, dword ptr [eax + 0x1b8]
0x4795C7: mov      eax, dword ptr [ebp - 4]
0x4795CA: sub      edx, dword ptr [eax + 0x1b0]
0x4795D0: add      edx, 0xe0
0x4795D6: mov      eax, dword ptr [ebp - 8]
0x4795D9: call     0x453a48
0x4795DE: mov      edx, dword ptr [ebp - 4]
0x4795E1: mov      dword ptr [edx + 0x244], eax
0x4795E7: jmp      0x479624
0x4795E9: mov      eax, dword ptr [ebp - 4]
0x4795EC: add      eax, 0x250
0x4795F1: push     eax
0x4795F2: mov      eax, dword ptr [ebp - 4]
0x4795F5: lea      ecx, [eax + 0x24c]
0x4795FB: mov      eax, dword ptr [ebp - 4]
0x4795FE: mov      edx, dword ptr [eax + 0x1b8]
0x479604: mov      eax, dword ptr [ebp - 4]
0x479607: sub      edx, dword ptr [eax + 0x1b0]
0x47960D: add      edx, 0xf0
0x479613: mov      eax, dword ptr [ebp - 8]
0x479616: call     0x453a48
0x47961B: mov      edx, dword ptr [ebp - 4]
0x47961E: mov      dword ptr [edx + 0x244], eax
0x479624: pop      ecx
0x479625: pop      ecx
0x479626: pop      ebp
0x479627: ret      

; === TWallStructure.DMT_-3 @ 0x479628 ===
0x479628: push     ebp
0x479629: mov      ebp, esp
0x47962B: add      esp, -0x10
0x47962E: mov      byte ptr [ebp - 0xd], dl
0x479631: mov      dword ptr [ebp - 4], eax
0x479634: mov      eax, dword ptr [ebp - 4]
0x479637: movzx    eax, word ptr [eax + 0x14]
0x47963B: call     0x496120
0x479640: mov      edx, dword ptr [ebp - 4]
0x479643: mov      dword ptr [edx + 0xe0], eax
0x479649: mov      eax, dword ptr [ebp - 4]
0x47964C: movzx    edx, word ptr [eax + 0x14]
0x479650: mov      eax, dword ptr [ebp - 4]
0x479653: movzx    eax, byte ptr [eax + 0xe]
0x479657: call     0x495ca8
0x47965C: mov      dword ptr [ebp - 0xc], eax
0x47965F: cmp      dword ptr [ebp - 0xc], 0
0x479663: je       0x479677
0x479665: mov      eax, dword ptr [ebp - 0xc]
0x479668: movzx    eax, word ptr [eax]
0x47966B: mov      edx, dword ptr [ebp - 4]
0x47966E: movzx    edx, byte ptr [edx + 0xc]
0x479672: add      eax, edx
0x479674: mov      dword ptr [ebp - 8], eax
0x479677: mov      eax, dword ptr [ebp - 8]
0x47967A: mov      esp, ebp
0x47967C: pop      ebp
0x47967D: ret      
0x47967E: mov      eax, eax
0x479680: push     ebp
0x479681: mov      ebp, esp
0x479683: add      esp, -0x1c
0x479686: mov      dword ptr [ebp - 0xc], ecx
0x479689: mov      dword ptr [ebp - 8], edx
0x47968C: mov      dword ptr [ebp - 4], eax
0x47968F: mov      eax, dword ptr [ebp + 0xc]
0x479692: push     eax
0x479693: mov      al, byte ptr [ebp + 8]
0x479696: push     eax
0x479697: mov      ecx, dword ptr [ebp - 0xc]
0x47969A: mov      edx, dword ptr [ebp - 8]
0x47969D: mov      eax, dword ptr [ebp - 4]
0x4796A0: call     0x4981f4 ; TActor.DMT_-14
0x4796A5: mov      eax, dword ptr [ebp - 4]
0x4796A8: cmp      dword ptr [eax + 0x248], 0
0x4796AF: je       0x47970a
0x4796B1: cmp      byte ptr [ebp + 8], 0
0x4796B5: jne      0x47970a
0x4796B7: lea      edx, [ebp - 0x1c]
0x4796BA: mov      eax, dword ptr [ebp - 4]
0x4796BD: mov      eax, dword ptr [eax + 0x248]
0x4796C3: call     0x44d8ec
0x4796C8: lea      eax, [ebp - 0x1c]
0x4796CB: push     eax
0x4796CC: mov      eax, dword ptr [ebp - 4]
0x4796CF: mov      eax, dword ptr [eax + 0x248]
0x4796D5: push     eax
0x4796D6: push     1
0x4796D8: mov      ecx, dword ptr [ebp + 0xc]
0x4796DB: mov      eax, dword ptr [ebp - 4]
0x4796DE: add      ecx, dword ptr [eax + 0x258]
0x4796E4: mov      eax, dword ptr [ebp - 4]
0x4796E7: add      ecx, dword ptr [eax + 0x9c]
0x4796ED: mov      edx, dword ptr [ebp - 0xc]
0x4796F0: mov      eax, dword ptr [ebp - 4]
0x4796F3: add      edx, dword ptr [eax + 0x254]
0x4796F9: mov      eax, dword ptr [ebp - 4]
0x4796FC: add      edx, dword ptr [eax + 0x98]
0x479702: mov      eax, dword ptr [ebp - 8]
0x479705: call     0x44d6cc
0x47970A: mov      eax, dword ptr [ebp - 4]
0x47970D: cmp      byte ptr [eax + 0xee], 0
0x479714: je       0x479766
0x479716: cmp      byte ptr [ebp + 8], 0
0x47971A: jne      0x479766
0x47971C: mov      eax, dword ptr [ebp - 4]
0x47971F: cmp      dword ptr [eax + 0x244], 0
0x479726: je       0x479766
0x479728: mov      eax, dword ptr [ebp - 4]
0x47972B: mov      eax, dword ptr [eax + 0x244]
0x479731: push     eax
0x479732: push     1
0x479734: mov      ecx, dword ptr [ebp + 0xc]
0x479737: mov      eax, dword ptr [ebp - 4]
0x47973A: add      ecx, dword ptr [eax + 0x250]
0x479740: mov      eax, dword ptr [ebp - 4]
0x479743: add      ecx, dword ptr [eax + 0x9c]
0x479749: mov      edx, dword ptr [ebp - 0xc]
0x47974C: mov      eax, dword ptr [ebp - 4]
0x47974F: add      edx, dword ptr [eax + 0x24c]
0x479755: mov      eax, dword ptr [ebp - 4]
0x479758: add      edx, dword ptr [eax + 0x98]
0x47975E: mov      eax, dword ptr [ebp - 8]
0x479761: call     0x455578
0x479766: mov      esp, ebp
0x479768: pop      ebp
0x479769: ret      8

; === TWallStructure.DMT_-6 @ 0x47976C ===
0x47976C: push     ebp
0x47976D: mov      ebp, esp
0x47976F: push     ecx
0x479770: mov      dword ptr [ebp - 4], eax
0x479773: mov      eax, dword ptr [ebp - 4]
0x479776: cmp      byte ptr [eax + 0x20], 0
0x47977A: je       0x4797b0
0x47977C: mov      eax, dword ptr [ebp - 4]
0x47977F: cmp      byte ptr [eax + 0x260], 0
0x479786: je       0x4797e2
0x479788: push     1
0x47978A: mov      eax, dword ptr [ebp - 4]
0x47978D: movzx    ecx, word ptr [eax + 0xa]
0x479791: mov      eax, dword ptr [ebp - 4]
0x479794: movzx    edx, word ptr [eax + 8]
0x479798: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x47979D: mov      eax, dword ptr [eax]
0x47979F: call     0x49f098
0x4797A4: mov      eax, dword ptr [ebp - 4]
0x4797A7: mov      byte ptr [eax + 0x260], 0
0x4797AE: jmp      0x4797e2
0x4797B0: mov      eax, dword ptr [ebp - 4]
0x4797B3: cmp      byte ptr [eax + 0x260], 0
0x4797BA: jne      0x4797e2
0x4797BC: push     0
0x4797BE: mov      eax, dword ptr [ebp - 4]
0x4797C1: movzx    ecx, word ptr [eax + 0xa]
0x4797C5: mov      eax, dword ptr [ebp - 4]
0x4797C8: movzx    edx, word ptr [eax + 8]
0x4797CC: mov      eax, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x4797D1: mov      eax, dword ptr [eax]
0x4797D3: call     0x49f098
0x4797D8: mov      eax, dword ptr [ebp - 4]
0x4797DB: mov      byte ptr [eax + 0x260], 1
0x4797E2: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x4797E7: mov      eax, dword ptr [eax]
0x4797E9: xor      ecx, ecx
0x4797EB: mov      edx, dword ptr [ebp - 4]
0x4797EE: call     0x473560
0x4797F3: mov      eax, dword ptr [ebp - 4]
0x4797F6: call     0x4994fc ; TActor.DMT_-6
0x4797FB: pop      ecx
0x4797FC: pop      ebp
0x4797FD: ret      
0x4797FE: mov      eax, eax
0x479800: push     ebp
0x479801: mov      ebp, esp
0x479803: xor      eax, eax
0x479805: push     ebp
0x479806: push     0x479825
0x47980B: push     dword ptr fs:[eax]
0x47980E: mov      dword ptr fs:[eax], esp
0x479811: inc      dword ptr [0x4f95c0]
0x479817: xor      eax, eax
0x479819: pop      edx
0x47981A: pop      ecx
0x47981B: pop      ecx
0x47981C: mov      dword ptr fs:[eax], edx
0x47981F: push     0x47982c
0x479824: ret      
0x479825: jmp      0x403518
0x47982A: jmp      0x479824
0x47982C: pop      ebp
0x47982D: ret      
0x47982E: mov      eax, eax
0x479830: sub      dword ptr [0x4f95c0], 1
0x479837: ret      
0x479838: test     byte ptr [eax + 0x47], bl
0x47983E: add      byte ptr [eax], al
0x479840: add      byte ptr [eax], al
0x479842: add      byte ptr [eax], al
0x479844: add      byte ptr [eax], al
0x479846: add      byte ptr [eax], al
0x479848: add      byte ptr [eax], al
0x47984A: add      byte ptr [eax], al
0x47984C: add      byte ptr [eax], al
0x47984E: add      byte ptr [eax], al
0x479850: add      byte ptr [eax], al
0x479852: add      byte ptr [eax], al
0x479854: add      byte ptr [eax], al
0x479856: add      byte ptr [eax], al
0x479858: test     byte ptr [eax + 0x400047], bl
0x47985E: add      byte ptr [eax], al
0x479860: mov      al, byte ptr [0xc8004010]
0x479865: xor      byte ptr [eax], al
0x479868: aam      0x30
0x47986A: inc      eax
0x47986B: add      al, bl
0x47986D: xor      byte ptr [eax], al
0x479870: fdiv     qword ptr [eax]
0x479872: inc      eax
0x479873: add      al, dl
0x479875: xor      byte ptr [eax], al
0x479878: inc      eax
0x479879: inc      eax
0x47987B: add      byte ptr [esi + ebp + 0x40], dl
0x47987F: add      byte ptr [ecx + ebx*4], dh
0x479882: inc      edi
