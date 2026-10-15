
; === VMT_TActor @ 0x495B18 ===
0x495B18: push     cs
0x495B19: add      byte ptr [eax], al
0x495B1B: add      byte ptr [eax], al
0x495B1D: add      byte ptr [eax + eax], al
0x495B20: add      byte ptr [eax], al
0x495B22: xchg     esp, eax
0x495B23: adc      byte ptr [eax], al
0x495B26: and      al, 0
0x495B28: add      byte ptr [eax], al
0x495B2A: xchg     esp, eax
0x495B2B: adc      byte ptr [eax], al
0x495B2E: sub      byte ptr [eax], al
0x495B30: add      byte ptr [eax], al
0x495B32: mov      eax, 0x6000495a
0x495B37: add      byte ptr [eax], al
0x495B39: add      byte ptr [esp + ecx*4 + 0x2240045], cl
0x495B40: add      byte ptr [eax], al
0x495B43: add      bh, bh
0x495B48: std      
0x495B4A: cld      
0x495B4C: sti      
0x495B4E: cli      
0x495B50: stc      
0x495B52: clc      
0x495B53: push     edi
0x495B55: push     esi
0x495B57: push     ebp
0x495B59: push     esp
0x495B5B: push     ebx
0x495B5D: push     edx
0x495B5F: push     ecx
0x495B61: call     ptr [eax + ebp*2 + 0x49]
0x495B65: add      byte ptr [eax - 0x7c], dh
0x495B68: dec      ecx
0x495B69: add      byte ptr [ebx + eax*4 + 0x49], bh
0x495B6D: add      al, dl
0x495B6F: bound    ecx, qword ptr [ecx]
0x495B72: call     0xfc49a4fb
0x495B77: xchg     esp, eax
0x495B78: dec      ecx
0x495B79: add      byte ptr [ecx + edx*4], ah
0x495B7C: dec      ecx
0x495B7D: add      byte ptr [edx + edx*4 + 0x49], al
0x495B81: add      byte ptr [esp + edx*4 + 0x49], bh
0x495B85: add      byte ptr [esp + edx*4 + 0x7cac0049], cl
0x495B8C: dec      ecx
0x495B8D: add      al, bl
0x495B8F: jge      0x495bda
0x495B91: add      ah, dh
0x495B93: jge      0x495bde
0x495B95: add      ah, dh
0x495B97: or       dword ptr [ecx], 0x498364 ; TActor.DMT_-15
0x495B9E: push     es
0x495B9F: push     esp
0x495BA0: inc      ecx
0x495BA1: arpl     word ptr [edi + ebp*2 + 0x72], si
0x495BA5: lea      eax, [eax]
0x495BA8: hlt      
0x495BA9: pop      ebx
0x495BAA: dec      ecx
0x495BAB: add      byte ptr [eax], al
0x495BAD: add      byte ptr [eax], al
0x495BAF: add      byte ptr [eax], al
0x495BB1: add      byte ptr [eax], al
0x495BB3: add      byte ptr [eax], al
0x495BB5: add      byte ptr [eax], al
0x495BB7: add      byte ptr [eax], al
0x495BB9: add      byte ptr [eax], al
0x495BBB: add      byte ptr [eax], al
0x495BBD: add      byte ptr [eax], al
0x495BBF: add      byte ptr [eax], al
0x495BC1: add      byte ptr [eax], al
0x495BC3: add      ah, dh
0x495BC5: pop      ebx
0x495BC6: dec      ecx
0x495BC7: add      byte ptr [esi], cl
0x495BC9: pop      esp
0x495BCA: dec      ecx
0x495BCB: add      byte ptr [edx + eax], al
0x495BCF: add      ah, cl
0x495BD1: pop      edx
0x495BD2: dec      ecx
0x495BD3: add      al, cl
0x495BD5: xor      byte ptr [eax], al
0x495BD8: aam      0x30
0x495BDA: inc      eax
0x495BDB: add      al, bl
0x495BDD: xor      byte ptr [eax], al
0x495BE0: fdiv     qword ptr [eax]
0x495BE2: inc      eax
0x495BE3: add      al, dl
0x495BE5: xor      byte ptr [eax], al
0x495BE8: inc      eax
0x495BE9: inc      eax
0x495BEB: add      byte ptr [esi + ebp + 0x40], dl
0x495BEF: add      byte ptr [eax], cl
0x495BF1: dec      ecx
0x495BF3: add      byte ptr [eax + eax], al
0x495BF6: cli      
0x495BFA: std      
0x495BFB: push     ebx
0x495BFD: jmp      dword ptr [0xa2b40049]
0x495C04: dec      ecx
0x495C05: add      byte ptr [esp], ch
0x495C08: dec      ecx
0x495C09: add      al, dl
0x495C0B: movsb    byte ptr es:[edi], byte ptr [esi]
0x495C0C: dec      ecx
0x495C0D: add      byte ptr [ecx], cl
0x495C0F: push     esp
0x495C10: dec      esi
0x495C11: jo       0x495c76
0x495C13: inc      ecx
0x495C14: arpl     word ptr [edi + ebp*2 + 0x72], si
0x495C18: pop      esp
0x495C1A: dec      ecx
0x495C1B: add      byte ptr [eax], al
0x495C1D: add      byte ptr [eax], al
0x495C1F: add      byte ptr [eax], al
0x495C21: add      byte ptr [eax], al
0x495C23: add      byte ptr [eax], al
0x495C25: add      byte ptr [eax], al
0x495C27: add      byte ptr [eax], al
0x495C29: add      byte ptr [eax], al
0x495C2B: add      byte ptr [eax], al
0x495C2D: add      byte ptr [eax], al
0x495C2F: add      byte ptr [eax], al
0x495C31: add      byte ptr [eax], al
0x495C33: add      byte ptr [esp + ebx*2 + 0x49], ah
0x495C37: add      byte ptr [esp + ebx*2 + 0x2600049], bl
0x495C3E: add      byte ptr [eax], al
0x495C40: int3     
0x495C41: pop      edx
0x495C42: dec      ecx
0x495C43: add      al, cl
0x495C45: xor      byte ptr [eax], al
0x495C48: aam      0x30
0x495C4A: inc      eax
0x495C4B: add      al, bl
0x495C4D: xor      byte ptr [eax], al
0x495C50: fdiv     qword ptr [eax]
0x495C52: inc      eax
0x495C53: add      al, dl
0x495C55: xor      byte ptr [eax], al
0x495C58: inc      eax
0x495C59: inc      eax
0x495C5B: add      byte ptr [esi + ebp + 0x40], dl
0x495C5F: add      byte ptr [ebp + 0x90049], bl
0x495C6A: std      
0x495C6C: cld      
0x495C6E: cli      
0x495C6F: push     edi
0x495C71: push     esp
0x495C73: push     ebx
0x495C75: push     edx
0x495C77: call     eax
0x495C79: movsd    dword ptr es:[edi], dword ptr [esi]
0x495C7A: dec      ecx
0x495C7B: add      al, ch
0x495C7D: mov      cl, 0x49
0x495C7F: add      ah, bh
0x495C81: mov      cl, 0x49
0x495C83: add      byte ptr [eax], bh
0x495C85: movsd    dword ptr es:[edi], dword ptr [esi]
0x495C86: dec      ecx
0x495C87: add      byte ptr [eax], cl
0x495C89: mov      ch, 0x49
0x495C8B: add      ah, ch
0x495C8D: mov      dl, 0x49
0x495C8F: add      byte ptr [edx + edi*4 + 0x49], al
0x495C93: add      byte ptr [eax + 0x780049ba], bl
0x495C99: mov      ebx, 0x54090049
0x495C9E: dec      eax
0x495C9F: jne      0x495d0e
0x495CA1: inc      ecx
0x495CA2: arpl     word ptr [edi + ebp*2 + 0x72], si
0x495CA6: mov      eax, eax
0x495CA8: push     ebp
0x495CA9: mov      ebp, esp
0x495CAB: add      esp, -0xc
0x495CAE: mov      dword ptr [ebp - 8], edx
0x495CB1: mov      dword ptr [ebp - 4], eax
0x495CB4: xor      eax, eax
0x495CB6: mov      dword ptr [ebp - 0xc], eax
0x495CB9: mov      eax, dword ptr [ebp - 4]
0x495CBC: add      eax, -9
0x495CBF: cmp      eax, 0x5a
0x495CC2: ja       0x495eb3
0x495CC8: mov      al, byte ptr [eax + 0x495cd5]
0x495CCE: jmp      dword ptr [eax*4 + 0x495d30]
0x495CD5: add      dword ptr [edx], eax
0x495CD7: add      eax, dword ptr [eax + 0x6080706]
0x495CDE: push     es
0x495CDF: or       cl, byte ptr [edx]
0x495CE1: or       al, byte ptr [edi]
0x495CE3: push     es
0x495CE4: add      al, 0
0x495CE6: add      byte ptr [eax], al
0x495CE8: add      byte ptr [eax], al
0x495CEA: or       dword ptr [ecx], ecx
0x495CEC: movups   xmm2, xmmword ptr [ecx]
0x495CEF: adc      dl, byte ptr [ebx]
0x495CF1: or       al, byte ptr [eax]
0x495CF3: add      byte ptr [edx], cl
0x495CF5: or       ecx, dword ptr [ebx]
0x495CF7: or       al, 0
0x495CF9: or       al, byte ptr [eax]
0x495CFB: or       eax, 0x180e0e
0x495D00: or       cl, byte ptr [edx]
0x495D02: adc      al, 0x15
0x495D04: add      byte ptr [eax], al
0x495D06: add      byte ptr [eax], al
0x495D08: add      byte ptr [eax], al
0x495D0A: add      byte ptr [eax], al
0x495D0C: add      byte ptr [eax], al
0x495D0E: add      byte ptr [eax], al
0x495D10: add      byte ptr [eax], al
0x495D12: add      byte ptr [eax], al
0x495D14: add      byte ptr [eax], al
0x495D16: add      byte ptr [eax], al
0x495D18: add      byte ptr [eax], al
0x495D1A: add      byte ptr [eax], al
0x495D1C: add      byte ptr [eax], al
0x495D1E: add      byte ptr [eax], al
0x495D20: add      byte ptr [eax], al
0x495D22: add      byte ptr [eax], al
0x495D24: add      byte ptr [eax], al
0x495D26: add      byte ptr [eax], al
0x495D28: add      byte ptr [eax], al
0x495D2A: add      byte ptr [eax], al
0x495D2C: add      byte ptr [eax], al
0x495D2E: push     ss
0x495D2F: pop      ss
0x495D30: mov      bl, 0x5e
0x495D32: dec      ecx
0x495D33: add      byte ptr [ebp + ebx*2 + 0x5da00049], dl
0x495D3A: dec      ecx
0x495D3B: add      byte ptr [ebp + ebx*2 + 0x5db80049], ch
0x495D42: dec      ecx
0x495D43: add      ah, al
0x495D45: pop      ebp
0x495D46: dec      ecx
0x495D47: add      al, dl
0x495D49: pop      ebp
0x495D4A: dec      ecx
0x495D4B: add      ah, bl
0x495D4D: pop      ebp
0x495D4E: dec      ecx
0x495D4F: add      al, ch
0x495D51: pop      ebp
0x495D52: dec      ecx
0x495D53: add      ah, dh
0x495D55: pop      ebp
0x495D56: dec      ecx
0x495D57: add      byte ptr [eax], al
0x495D59: pop      esi
0x495D5A: dec      ecx
0x495D5B: add      byte ptr [esi + ebx*2], cl
0x495D5E: dec      ecx
0x495D5F: add      byte ptr [eax], bl
0x495D61: pop      esi
0x495D62: dec      ecx
0x495D63: add      byte ptr [esi + ebx*2], ah
0x495D66: dec      ecx
0x495D67: add      byte ptr [eax], dh
0x495D69: pop      esi
0x495D6A: dec      ecx
0x495D6B: add      byte ptr [ecx], bh
0x495D6D: pop      esi
0x495D6E: dec      ecx
0x495D6F: add      byte ptr [edx + 0x5e], al
0x495D72: dec      ecx
0x495D73: add      byte ptr [ebx + 0x5e], cl
0x495D76: dec      ecx
0x495D77: add      byte ptr [esi + ebx*2 + 0x49], dl
0x495D7B: add      byte ptr [ebp + 0x5e], bl
0x495D7E: dec      ecx
0x495D7F: add      byte ptr [esi + 0x5e], ah
0x495D82: dec      ecx
0x495D83: add      byte ptr [edi + 0x5e], ch
0x495D86: dec      ecx
0x495D87: add      byte ptr [eax + 0x5e], bh
0x495D8A: dec      ecx
0x495D8B: add      byte ptr [ecx - 0x75ffb6a2], al
0x495D91: pop      esi
0x495D92: dec      ecx
0x495D93: add      bh, al
0x495D95: inc      ebp
0x495D96: hlt      
0x495D97: in       al, 0x30
0x495D99: dec      edx
0x495D9A: add      cl, ch
0x495D9C: adc      eax, dword ptr [ecx]
0x495D9E: add      byte ptr [eax], al
0x495DA0: mov      dword ptr [ebp - 0xc], 0x4a312c ; *0x4A312C->0x040000
0x495DA7: jmp      0x495eb3
0x495DAC: mov      dword ptr [ebp - 0xc], 0x4a3174 ; *0x4A3174->0x040000
0x495DB3: jmp      0x495eb3
0x495DB8: mov      dword ptr [ebp - 0xc], 0x4a31bc ; *0x4A31BC->0x040000
0x495DBF: jmp      0x495eb3
0x495DC4: mov      dword ptr [ebp - 0xc], 0x4a3204 ; *0x4A3204->0x040000
0x495DCB: jmp      0x495eb3
0x495DD0: mov      dword ptr [ebp - 0xc], 0x4a324c ; *0x4A324C->0x040000
0x495DD7: jmp      0x495eb3
0x495DDC: mov      dword ptr [ebp - 0xc], 0x4a3294 ; *0x4A3294->0x040000
0x495DE3: jmp      0x495eb3
0x495DE8: mov      dword ptr [ebp - 0xc], 0x4a32dc ; *0x4A32DC->0x040000
0x495DEF: jmp      0x495eb3
0x495DF4: mov      dword ptr [ebp - 0xc], 0x4a3324 ; *0x4A3324->0x040000
0x495DFB: jmp      0x495eb3
0x495E00: mov      dword ptr [ebp - 0xc], 0x4a336c ; *0x4A336C->0x040000
0x495E07: jmp      0x495eb3
0x495E0C: mov      dword ptr [ebp - 0xc], 0x4a33b4 ; *0x4A33B4->0x040000
0x495E13: jmp      0x495eb3
0x495E18: mov      dword ptr [ebp - 0xc], 0x4a33fc ; *0x4A33FC->0x040000
0x495E1F: jmp      0x495eb3
0x495E24: mov      dword ptr [ebp - 0xc], 0x4a3444 ; *0x4A3444->0x040050
0x495E2B: jmp      0x495eb3
0x495E30: mov      dword ptr [ebp - 0xc], 0x4a348c ; *0x4A348C->0x040014
0x495E37: jmp      0x495eb3
0x495E39: mov      dword ptr [ebp - 0xc], 0x4a34d4 ; *0x4A34D4->0x040000
0x495E40: jmp      0x495eb3
0x495E42: mov      dword ptr [ebp - 0xc], 0x4a351c ; *0x4A351C->0x040000
0x495E49: jmp      0x495eb3
0x495E4B: mov      dword ptr [ebp - 0xc], 0x4a3684 ; *0x4A3684->0x040000
0x495E52: jmp      0x495eb3
0x495E54: mov      dword ptr [ebp - 0xc], 0x4a36cc ; *0x4A36CC->0x040000
0x495E5B: jmp      0x495eb3
0x495E5D: mov      dword ptr [ebp - 0xc], 0x4a3714 ; *0x4A3714->0x010000
0x495E64: jmp      0x495eb3
0x495E66: mov      dword ptr [ebp - 0xc], 0x4a35f4 ; *0x4A35F4->0x040050
0x495E6D: jmp      0x495eb3
0x495E6F: mov      dword ptr [ebp - 0xc], 0x4a363c ; *0x4A363C->0x040050
0x495E76: jmp      0x495eb3
0x495E78: mov      dword ptr [ebp - 0xc], 0x4a35ac ; *0x4A35AC->0x010000
0x495E7F: jmp      0x495eb3
0x495E81: mov      dword ptr [ebp - 0xc], 0x4a3564 ; *0x4A3564->0x010000
0x495E88: jmp      0x495eb3
0x495E8A: mov      eax, dword ptr [ebp - 8]
0x495E8D: sub      eax, 0x17
0x495E90: je       0x495e9a
0x495E92: dec      eax
0x495E93: sub      eax, 2
0x495E96: jb       0x495ea3
0x495E98: jmp      0x495eac
0x495E9A: mov      dword ptr [ebp - 0xc], 0x4a37a4 ; *0x4A37A4->0x040000
0x495EA1: jmp      0x495eb3
0x495EA3: mov      dword ptr [ebp - 0xc], 0x4a37ec ; *0x4A37EC->0x04001E
0x495EAA: jmp      0x495eb3
0x495EAC: mov      dword ptr [ebp - 0xc], 0x4a375c ; *0x4A375C->0x040000
0x495EB3: mov      eax, dword ptr [ebp - 0xc]
0x495EB6: mov      esp, ebp
0x495EB8: pop      ebp
0x495EB9: ret      
0x495EBA: mov      eax, eax
0x495EBC: push     ebp
0x495EBD: mov      ebp, esp
0x495EBF: add      esp, -8
0x495EC2: mov      dword ptr [ebp - 4], eax
0x495EC5: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x495ECA: mov      eax, dword ptr [eax]
0x495ECC: mov      eax, dword ptr [eax + 0x318]
0x495ED2: mov      dword ptr [ebp - 8], eax
0x495ED5: mov      eax, dword ptr [ebp - 4]
0x495ED8: mov      ecx, 0xa
0x495EDD: cdq      
0x495EDE: idiv     ecx
0x495EE0: cmp      eax, 0x5a
0x495EE3: ja       0x496116
0x495EE9: mov      al, byte ptr [eax + 0x495ef6]
0x495EEF: jmp      dword ptr [eax*4 + 0x495f51]
0x495EF6: add      dword ptr [edx], eax
0x495EF8: add      eax, dword ptr [eax + 0x9080706]
0x495EFF: or       cl, byte ptr [ebx]
0x495F01: or       al, 0xd
0x495F03: push     cs
0x495F04: movups   xmm2, xmmword ptr [ecx]
0x495F07: adc      al, byte ptr [eax]
0x495F09: add      byte ptr [eax], al
0x495F0B: add      byte ptr [eax], al
0x495F0D: add      byte ptr [eax], al
0x495F0F: add      byte ptr [eax], al
0x495F11: add      byte ptr [eax], al
0x495F13: add      byte ptr [eax], al
0x495F15: add      byte ptr [eax], al
0x495F17: add      byte ptr [eax], al
0x495F19: add      byte ptr [eax], al
0x495F1B: add      byte ptr [eax], al
0x495F1D: add      byte ptr [eax], al
0x495F1F: add      byte ptr [eax], al
0x495F21: add      byte ptr [eax], al
0x495F23: add      byte ptr [eax], al
0x495F25: add      byte ptr [eax], al
0x495F27: add      byte ptr [eax], al
0x495F29: add      byte ptr [eax], al
0x495F2B: add      byte ptr [eax], al
0x495F2D: add      byte ptr [eax], al
0x495F2F: add      byte ptr [eax], al
0x495F31: add      byte ptr [eax], al
0x495F33: add      byte ptr [eax], al
0x495F35: add      byte ptr [eax], al
0x495F37: add      byte ptr [eax], al
0x495F39: add      byte ptr [eax], al
0x495F3B: add      byte ptr [eax], al
0x495F3D: add      byte ptr [eax], al
0x495F3F: add      byte ptr [eax], al
0x495F41: add      byte ptr [eax], al
0x495F43: add      byte ptr [eax], al
0x495F45: add      byte ptr [eax], al
0x495F47: add      byte ptr [eax], al
0x495F49: add      byte ptr [eax], al
0x495F4B: add      byte ptr [eax], al
0x495F4D: add      byte ptr [eax], al
0x495F4F: add      byte ptr [ebx], dl
0x495F51: push     ss
0x495F52: popal    
0x495F53: dec      ecx
0x495F54: add      byte ptr [ecx - 0x49ffb6a1], ah
0x495F5A: pop      edi
0x495F5B: dec      ecx
0x495F5C: add      bl, cl
0x495F5E: pop      edi
0x495F5F: dec      ecx
0x495F60: add      al, ah
0x495F62: pop      edi
0x495F63: dec      ecx
0x495F64: add      ch, dh
0x495F66: pop      edi
0x495F67: dec      ecx
0x495F68: add      byte ptr [edx], cl
0x495F6A: pushal   
0x495F6B: dec      ecx
0x495F6C: add      byte ptr [edi], bl
0x495F6E: pushal   
0x495F6F: dec      ecx
0x495F70: add      byte ptr [eax], dh
0x495F73: dec      ecx
0x495F74: add      byte ptr [ecx + 0x60], cl
0x495F77: dec      ecx
0x495F78: add      byte ptr [esi + 0x60], bl
0x495F7B: dec      ecx
0x495F7C: add      byte ptr [ebx + 0x60], dh
0x495F7F: dec      ecx
0x495F80: add      byte ptr [eax - 0x65ffb6a0], cl
0x495F86: pushal   
0x495F87: dec      ecx
0x495F88: add      byte ptr [eax + 0x60be0049], ch
0x495F8F: dec      ecx
0x495F90: add      al, dl
0x495F92: pushal   
0x495F93: dec      ecx
0x495F94: add      dl, ah
0x495F96: pushal   
0x495F97: dec      ecx
0x495F98: add      ah, dh
0x495F9A: pushal   
0x495F9B: dec      ecx
0x495F9C: add      byte ptr [esi], al
0x495F9E: popal    
0x495F9F: dec      ecx
0x495FA0: add      byte ptr [ecx + 0x4a3fe8], ah ; *0x4A3FE8->0x4F7D88
0x495FA6: mov      eax, dword ptr [eax]
0x495FA8: mov      eax, dword ptr [eax + 0x318]
0x495FAE: mov      dword ptr [ebp - 8], eax
0x495FB1: jmp      0x496116
0x495FB6: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x495FBB: mov      eax, dword ptr [eax]
0x495FBD: mov      eax, dword ptr [eax + 0x354]
0x495FC3: mov      dword ptr [ebp - 8], eax
0x495FC6: jmp      0x496116
0x495FCB: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x495FD0: mov      eax, dword ptr [eax]
0x495FD2: mov      eax, dword ptr [eax + 0x358]
0x495FD8: mov      dword ptr [ebp - 8], eax
0x495FDB: jmp      0x496116
0x495FE0: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x495FE5: mov      eax, dword ptr [eax]
0x495FE7: mov      eax, dword ptr [eax + 0x360]
0x495FED: mov      dword ptr [ebp - 8], eax
0x495FF0: jmp      0x496116
0x495FF5: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x495FFA: mov      eax, dword ptr [eax]
0x495FFC: mov      eax, dword ptr [eax + 0x368]
0x496002: mov      dword ptr [ebp - 8], eax
0x496005: jmp      0x496116
0x49600A: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x49600F: mov      eax, dword ptr [eax]
0x496011: mov      eax, dword ptr [eax + 0x36c]
0x496017: mov      dword ptr [ebp - 8], eax
0x49601A: jmp      0x496116
0x49601F: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x496024: mov      eax, dword ptr [eax]
0x496026: mov      eax, dword ptr [eax + 0x38c]
0x49602C: mov      dword ptr [ebp - 8], eax
0x49602F: jmp      0x496116
0x496034: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x496039: mov      eax, dword ptr [eax]
0x49603B: mov      eax, dword ptr [eax + 0x390]
0x496041: mov      dword ptr [ebp - 8], eax
0x496044: jmp      0x496116
0x496049: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x49604E: mov      eax, dword ptr [eax]
0x496050: mov      eax, dword ptr [eax + 0x394]
0x496056: mov      dword ptr [ebp - 8], eax
0x496059: jmp      0x496116
0x49605E: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x496063: mov      eax, dword ptr [eax]
0x496065: mov      eax, dword ptr [eax + 0x398]
0x49606B: mov      dword ptr [ebp - 8], eax
0x49606E: jmp      0x496116
0x496073: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x496078: mov      eax, dword ptr [eax]
0x49607A: mov      eax, dword ptr [eax + 0x39c]
0x496080: mov      dword ptr [ebp - 8], eax
0x496083: jmp      0x496116
0x496088: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x49608D: mov      eax, dword ptr [eax]
0x49608F: mov      eax, dword ptr [eax + 0x3a0]
0x496095: mov      dword ptr [ebp - 8], eax
0x496098: jmp      0x496116
0x49609A: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x49609F: mov      eax, dword ptr [eax]
0x4960A1: mov      eax, dword ptr [eax + 0x3a4]
0x4960A7: mov      dword ptr [ebp - 8], eax
0x4960AA: jmp      0x496116
0x4960AC: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4960B1: mov      eax, dword ptr [eax]
0x4960B3: mov      eax, dword ptr [eax + 0x3a8]
0x4960B9: mov      dword ptr [ebp - 8], eax
0x4960BC: jmp      0x496116
0x4960BE: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4960C3: mov      eax, dword ptr [eax]
0x4960C5: mov      eax, dword ptr [eax + 0x3ac]
0x4960CB: mov      dword ptr [ebp - 8], eax
0x4960CE: jmp      0x496116
0x4960D0: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4960D5: mov      eax, dword ptr [eax]
0x4960D7: mov      eax, dword ptr [eax + 0x3b0]
0x4960DD: mov      dword ptr [ebp - 8], eax
0x4960E0: jmp      0x496116
0x4960E2: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4960E7: mov      eax, dword ptr [eax]
0x4960E9: mov      eax, dword ptr [eax + 0x3b4]
0x4960EF: mov      dword ptr [ebp - 8], eax
0x4960F2: jmp      0x496116
0x4960F4: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4960F9: mov      eax, dword ptr [eax]
0x4960FB: mov      eax, dword ptr [eax + 0x3b8]
0x496101: mov      dword ptr [ebp - 8], eax
0x496104: jmp      0x496116
0x496106: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x49610B: mov      eax, dword ptr [eax]
0x49610D: mov      eax, dword ptr [eax + 0x370]
0x496113: mov      dword ptr [ebp - 8], eax
0x496116: mov      eax, dword ptr [ebp - 8]
0x496119: pop      ecx
0x49611A: pop      ecx
0x49611B: pop      ebp
0x49611C: ret      
0x49611D: lea      eax, [eax]
0x496120: push     ebp
0x496121: mov      ebp, esp
0x496123: add      esp, -0x10
0x496126: mov      dword ptr [ebp - 4], eax
0x496129: xor      eax, eax
0x49612B: mov      dword ptr [ebp - 8], eax
0x49612E: mov      eax, dword ptr [ebp - 4]
0x496131: mov      ecx, 0xa
0x496136: cdq      
0x496137: idiv     ecx
0x496139: mov      dword ptr [ebp - 0xc], eax
0x49613C: mov      eax, dword ptr [ebp - 4]
0x49613F: mov      ecx, 0xa
0x496144: cdq      
0x496145: idiv     ecx
0x496147: mov      dword ptr [ebp - 0x10], edx
0x49614A: mov      eax, dword ptr [ebp - 0xc]
0x49614D: cmp      eax, 6
0x496150: jg       0x496180
0x496152: je       0x49623d
0x496158: cmp      eax, 5
0x49615B: ja       0x496289
0x496161: jmp      dword ptr [eax*4 + 0x496168]
0x496168: mov      al, 0x61
0x49616A: dec      ecx
0x49616B: add      byte ptr [edi - 0x31ffb69f], bh
0x496171: popal    
0x496172: dec      ecx
0x496173: add      dh, cl
0x496175: popal    
0x496176: dec      ecx
0x496177: add      byte ptr [eax], bl
0x496179: bound    ecx, qword ptr [ecx]
0x49617C: xor      dword ptr [edx + 0x49], esp
0x49617F: add      byte ptr [ebx + 0xf7d0ef8], al
0x496185: add      eax, -7
0x496188: sub      eax, 6
0x49618B: jb       0x4961ce
0x49618D: je       0x4961dd
0x49618F: jmp      0x496289
0x496194: add      eax, -0xe
0x496197: sub      eax, 3
0x49619A: jb       0x4961ce
0x49619C: je       0x496249
0x4961A2: sub      eax, 0x49
0x4961A5: je       0x496255
0x4961AB: jmp      0x496289
0x4961B0: imul     eax, dword ptr [ebp - 0x10], 0x118
0x4961B7: mov      dword ptr [ebp - 8], eax
0x4961BA: jmp      0x496289
0x4961BF: imul     eax, dword ptr [ebp - 0x10], 0xe6
0x4961C6: mov      dword ptr [ebp - 8], eax
0x4961C9: jmp      0x496289
0x4961CE: imul     eax, dword ptr [ebp - 0x10], 0x168
0x4961D5: mov      dword ptr [ebp - 8], eax
0x4961D8: jmp      0x496289
0x4961DD: mov      eax, dword ptr [ebp - 0x10]
0x4961E0: dec      eax
0x4961E1: je       0x4961eb
0x4961E3: dec      eax
0x4961E4: je       0x4961f7
0x4961E6: dec      eax
0x4961E7: je       0x496203
0x4961E9: jmp      0x49620c
0x4961EB: mov      dword ptr [ebp - 8], 0x168
0x4961F2: jmp      0x496289
0x4961F7: mov      dword ptr [ebp - 8], 0x1b8
0x4961FE: jmp      0x496289
0x496203: mov      dword ptr [ebp - 8], 0x226
0x49620A: jmp      0x496289
0x49620C: imul     eax, dword ptr [ebp - 0x10], 0x168
0x496213: mov      dword ptr [ebp - 8], eax
0x496216: jmp      0x496289
0x496218: imul     eax, dword ptr [ebp - 0x10], 0x168
0x49621F: mov      dword ptr [ebp - 8], eax
0x496222: cmp      dword ptr [ebp - 0x10], 1
0x496226: jne      0x496289
0x496228: mov      dword ptr [ebp - 8], 0x258
0x49622F: jmp      0x496289
0x496231: imul     eax, dword ptr [ebp - 0x10], 0x1ae
0x496238: mov      dword ptr [ebp - 8], eax
0x49623B: jmp      0x496289
0x49623D: imul     eax, dword ptr [ebp - 0x10], 0x1b8
0x496244: mov      dword ptr [ebp - 8], eax
0x496247: jmp      0x496289
0x496249: imul     eax, dword ptr [ebp - 0x10], 0x15e
0x496250: mov      dword ptr [ebp - 8], eax
0x496253: jmp      0x496289
0x496255: mov      eax, dword ptr [ebp - 0x10]
0x496258: sub      eax, 1
0x49625B: jb       0x496267
0x49625D: je       0x496270
0x49625F: dec      eax
0x496260: je       0x496279
0x496262: dec      eax
0x496263: je       0x496282
0x496265: jmp      0x496289
0x496267: mov      dword ptr [ebp - 8], 0x50
0x49626E: jmp      0x496289
0x496270: mov      dword ptr [ebp - 8], 0xa8
0x496277: jmp      0x496289
0x496279: mov      dword ptr [ebp - 8], 0xb8
0x496280: jmp      0x496289
0x496282: mov      dword ptr [ebp - 8], 0xc8
0x496289: mov      eax, dword ptr [ebp - 8]
0x49628C: mov      esp, ebp
0x49628E: pop      ebp
0x49628F: ret      
0x496290: push     ebp
0x496291: mov      ebp, esp
0x496293: add      esp, -8
0x496296: mov      dword ptr [ebp - 4], eax
0x496299: mov      eax, dword ptr [ebp - 4]
0x49629C: sub      eax, 0x17
0x49629F: jb       0x4962a5
0x4962A1: je       0x4962ae
0x4962A3: jmp      0x4962b7
0x4962A5: imul     eax, dword ptr [ebp - 4], 0x3c
0x4962A9: mov      dword ptr [ebp - 8], eax
0x4962AC: jmp      0x4962c8
0x4962AE: mov      dword ptr [ebp - 8], 0x564
0x4962B5: jmp      0x4962c8
0x4962B7: mov      eax, dword ptr [ebp - 4]
0x4962BA: sub      eax, 0x18
0x4962BD: imul     eax, eax, 0x3c
0x4962C0: add      eax, 0x5be
0x4962C5: mov      dword ptr [ebp - 8], eax
0x4962C8: mov      eax, dword ptr [ebp - 8]
0x4962CB: pop      ecx
0x4962CC: pop      ecx
0x4962CD: pop      ebp
0x4962CE: ret      
0x4962CF: nop      
0x4962D0: push     ebp
0x4962D1: mov      ebp, esp
0x4962D3: add      esp, -8
0x4962D6: test     dl, dl
0x4962D8: je       0x4962e2
0x4962DA: add      esp, -0x10
0x4962DD: call     0x4031a4
0x4962E2: mov      byte ptr [ebp - 5], dl
0x4962E5: mov      dword ptr [ebp - 4], eax
0x4962E8: xor      edx, edx
0x4962EA: mov      eax, dword ptr [ebp - 4]
0x4962ED: call     0x402e7c
0x4962F2: mov      dl, 1
0x4962F4: mov      eax, dword ptr [0x40df00]
0x4962F9: call     0x402e7c
0x4962FE: mov      edx, dword ptr [ebp - 4]
0x496301: mov      dword ptr [edx + 0x220], eax
0x496307: mov      eax, dword ptr [ebp - 4]
0x49630A: xor      edx, edx
0x49630C: mov      dword ptr [eax + 4], edx
0x49630F: mov      eax, dword ptr [ebp - 4]
0x496312: xor      edx, edx
0x496314: mov      dword ptr [eax + 0x140], edx
0x49631A: mov      eax, dword ptr [ebp - 4]
0x49631D: add      eax, 0x30
0x496320: xor      ecx, ecx
0x496322: mov      edx, 0x28
0x496327: call     0x402b54
0x49632C: mov      eax, dword ptr [ebp - 4]
0x49632F: xor      edx, edx
0x496331: mov      dword ptr [eax + 0x58], edx
0x496334: mov      eax, dword ptr [ebp - 4]
0x496337: mov      byte ptr [eax + 0x5d], 1
0x49633B: mov      eax, dword ptr [ebp - 4]
0x49633E: mov      byte ptr [eax + 0x5e], 1
0x496342: mov      eax, dword ptr [ebp - 4]
0x496345: xor      edx, edx
0x496347: mov      dword ptr [eax + 0x148], edx
0x49634D: mov      eax, dword ptr [ebp - 4]
0x496350: mov      byte ptr [eax + 0x14c], 0
0x496357: mov      eax, dword ptr [ebp - 4]
0x49635A: xor      edx, edx
0x49635C: mov      dword ptr [eax + 0x98], edx
0x496362: mov      eax, dword ptr [ebp - 4]
0x496365: xor      edx, edx
0x496367: mov      dword ptr [eax + 0x9c], edx
0x49636D: mov      eax, dword ptr [ebp - 4]
0x496370: xor      edx, edx
0x496372: mov      dword ptr [eax + 0xc0], edx
0x496378: mov      eax, dword ptr [ebp - 4]
0x49637B: mov      dword ptr [eax + 0x1b8], 0xffffffff
0x496385: mov      eax, dword ptr [ebp - 4]
0x496388: mov      dword ptr [eax + 0x1c0], 0xffffffff
0x496392: mov      eax, dword ptr [ebp - 4]
0x496395: xor      edx, edx
0x496397: mov      dword ptr [eax + 0x224], edx
0x49639D: mov      eax, dword ptr [ebp - 4]
0x4963A0: add      eax, 0x28
0x4963A3: call     0x403a58
0x4963A8: mov      eax, dword ptr [ebp - 4]
0x4963AB: mov      dword ptr [eax + 0x2c], 0xffffff
0x4963B2: mov      eax, dword ptr [ebp - 4]
0x4963B5: xor      edx, edx
0x4963B7: mov      dword ptr [eax + 0x16c], edx
0x4963BD: mov      eax, dword ptr [ebp - 4]
0x4963C0: mov      byte ptr [eax + 0x14d], 0
0x4963C7: mov      eax, dword ptr [ebp - 4]
0x4963CA: xor      edx, edx
0x4963CC: mov      dword ptr [eax + 0x150], edx
0x4963D2: mov      eax, dword ptr [ebp - 4]
0x4963D5: mov      byte ptr [eax + 0x20], 0
0x4963D9: mov      eax, dword ptr [ebp - 4]
0x4963DC: mov      byte ptr [eax + 0x21], 0
0x4963E0: mov      eax, dword ptr [ebp - 4]
0x4963E3: mov      byte ptr [eax + 0x22], 0
0x4963E7: mov      eax, dword ptr [ebp - 4]
0x4963EA: mov      byte ptr [eax + 0x23], 0
0x4963EE: mov      eax, dword ptr [ebp - 4]
0x4963F1: xor      edx, edx
0x4963F3: mov      dword ptr [eax + 0x154], edx
0x4963F9: mov      eax, dword ptr [ebp - 4]
0x4963FC: xor      edx, edx
0x4963FE: mov      dword ptr [eax + 0x158], edx
0x496404: mov      eax, dword ptr [ebp - 4]
0x496407: mov      byte ptr [eax + 0x164], 0
0x49640E: mov      eax, dword ptr [ebp - 4]
0x496411: xor      edx, edx
0x496413: mov      dword ptr [eax + 0x1fc], edx
0x496419: mov      eax, dword ptr [ebp - 4]
0x49641C: xor      edx, edx
0x49641E: mov      dword ptr [eax + 0x200], edx
0x496424: mov      eax, dword ptr [ebp - 4]
0x496427: xor      edx, edx
0x496429: mov      dword ptr [eax + 0x1f0], edx
0x49642F: call     0x406154
0x496434: mov      edx, dword ptr [ebp - 4]
0x496437: mov      dword ptr [edx + 0x204], eax
0x49643D: mov      eax, dword ptr [ebp - 4]
0x496440: mov      byte ptr [eax + 0x144], 0
0x496447: mov      eax, dword ptr [ebp - 4]
0x49644A: mov      byte ptr [eax + 0x134], 0
0x496451: mov      eax, dword ptr [ebp - 4]
0x496454: mov      byte ptr [eax + 0x135], 0
0x49645B: mov      eax, dword ptr [ebp - 4]
0x49645E: xor      edx, edx
0x496460: mov      dword ptr [eax + 0x10c], edx
0x496466: mov      eax, dword ptr [ebp - 4]
0x496469: mov      dword ptr [eax + 0x108], 0xa
0x496473: mov      eax, dword ptr [ebp - 4]
0x496476: mov      dword ptr [eax + 0x18c], 0xffffffff
0x496480: mov      eax, dword ptr [ebp - 4]
0x496483: mov      dword ptr [eax + 0x17c], 0xffffffff
0x49648D: mov      eax, dword ptr [ebp - 4]
0x496490: mov      dword ptr [eax + 0x190], 0xffffffff
0x49649A: mov      eax, dword ptr [ebp - 4]
0x49649D: mov      dword ptr [eax + 0x194], 0xffffffff
0x4964A7: mov      eax, dword ptr [ebp - 4]
0x4964AA: mov      dword ptr [eax + 0x180], 0x48
0x4964B4: mov      eax, dword ptr [ebp - 4]
0x4964B7: mov      dword ptr [eax + 0x184], 0xffffffff
0x4964C1: mov      eax, dword ptr [ebp - 4]
0x4964C4: mov      dword ptr [eax + 0x198], 0xffffffff
0x4964CE: mov      eax, dword ptr [ebp - 4]
0x4964D1: mov      dword ptr [eax + 0x19c], 0xffffffff
0x4964DB: mov      eax, dword ptr [ebp - 4]
0x4964DE: mov      dword ptr [eax + 0x1a0], 0xffffffff
0x4964E8: mov      eax, dword ptr [ebp - 4]
0x4964EB: cmp      byte ptr [ebp - 5], 0
0x4964EF: je       0x496500
0x4964F1: call     0x4031fc
0x4964F6: pop      dword ptr fs:[0]
0x4964FD: add      esp, 0xc
0x496500: mov      eax, dword ptr [ebp - 4]
0x496503: pop      ecx
0x496504: pop      ecx
0x496505: pop      ebp
0x496506: ret      
0x496507: nop      
0x496508: push     ebp
0x496509: mov      ebp, esp
0x49650B: add      esp, -8
0x49650E: call     0x403204
0x496513: mov      byte ptr [ebp - 5], dl
0x496516: mov      dword ptr [ebp - 4], eax
0x496519: mov      eax, dword ptr [ebp - 4]
0x49651C: mov      eax, dword ptr [eax + 0x220]
0x496522: call     0x402eac
0x496527: mov      dl, byte ptr [ebp - 5]
0x49652A: and      dl, 0xfc
0x49652D: mov      eax, dword ptr [ebp - 4]
0x496530: call     0x402e9c
0x496535: cmp      byte ptr [ebp - 5], 0
0x496539: jle      0x496543
0x49653B: mov      eax, dword ptr [ebp - 4]
0x49653E: call     0x4031f4
0x496543: pop      ecx
0x496544: pop      ecx
0x496545: pop      ebp
0x496546: ret      
0x496547: nop      
0x496548: push     ebp
0x496549: mov      ebp, esp
0x49654B: add      esp, -0x10
0x49654E: mov      dword ptr [ebp - 0xc], ecx
0x496551: mov      word ptr [ebp - 6], dx
0x496555: mov      dword ptr [ebp - 4], eax
0x496558: mov      eax, dword ptr [ebp + 0xc]
0x49655B: call     0x403e88
0x496560: xor      eax, eax
0x496562: push     ebp
0x496563: push     0x4965f5
0x496568: push     dword ptr fs:[eax]
0x49656B: mov      dword ptr fs:[eax], esp
0x49656E: mov      edx, dword ptr [0x458c8c]
0x496574: mov      eax, 0x20
0x496579: call     0x4046d8
0x49657E: mov      dword ptr [ebp - 0x10], eax
0x496581: movzx    eax, word ptr [ebp - 6]
0x496585: mov      edx, dword ptr [ebp - 0x10]
0x496588: mov      dword ptr [edx], eax
0x49658A: mov      eax, dword ptr [ebp - 0xc]
0x49658D: mov      edx, dword ptr [ebp - 0x10]
0x496590: mov      dword ptr [edx + 4], eax
0x496593: mov      eax, dword ptr [ebp + 0x1c]
0x496596: mov      edx, dword ptr [ebp - 0x10]
0x496599: mov      dword ptr [edx + 8], eax
0x49659C: mov      eax, dword ptr [ebp + 0x18]
0x49659F: mov      edx, dword ptr [ebp - 0x10]
0x4965A2: mov      dword ptr [edx + 0xc], eax
0x4965A5: mov      eax, dword ptr [ebp + 0x14]
0x4965A8: mov      edx, dword ptr [ebp - 0x10]
0x4965AB: mov      dword ptr [edx + 0x10], eax
0x4965AE: mov      eax, dword ptr [ebp + 0x10]
0x4965B1: mov      edx, dword ptr [ebp - 0x10]
0x4965B4: mov      dword ptr [edx + 0x14], eax
0x4965B7: mov      eax, dword ptr [ebp - 0x10]
0x4965BA: add      eax, 0x18
0x4965BD: mov      edx, dword ptr [ebp + 0xc]
0x4965C0: call     0x403aac
0x4965C5: mov      eax, dword ptr [ebp + 8]
0x4965C8: mov      edx, dword ptr [ebp - 0x10]
0x4965CB: mov      dword ptr [edx + 0x1c], eax
0x4965CE: mov      edx, dword ptr [ebp - 0x10]
0x4965D1: mov      eax, dword ptr [ebp - 4]
0x4965D4: mov      eax, dword ptr [eax + 0x220]
0x4965DA: call     0x40f588
0x4965DF: xor      eax, eax
0x4965E1: pop      edx
0x4965E2: pop      ecx
0x4965E3: pop      ecx
0x4965E4: mov      dword ptr fs:[eax], edx
0x4965E7: push     0x4965fc
0x4965EC: lea      eax, [ebp + 0xc]
0x4965EF: call     0x403a58
0x4965F4: ret      
0x4965F5: jmp      0x403518
0x4965FA: jmp      0x4965ec
0x4965FC: mov      esp, ebp
0x4965FE: pop      ebp
0x4965FF: ret      0x18
0x496602: mov      eax, eax
0x496604: push     ebp
0x496605: mov      ebp, esp
0x496607: add      esp, -0x1c
0x49660A: mov      dword ptr [ebp - 0xc], ecx
0x49660D: mov      word ptr [ebp - 6], dx
0x496611: mov      dword ptr [ebp - 4], eax
0x496614: mov      eax, dword ptr [ebp + 0xc]
0x496617: call     0x403e88
0x49661C: xor      eax, eax
0x49661E: push     ebp
0x49661F: push     0x4967c1
0x496624: push     dword ptr fs:[eax]
0x496627: mov      dword ptr fs:[eax], esp
0x49662A: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49662F: mov      eax, dword ptr [eax]
0x496631: cmp      eax, dword ptr [ebp - 4]
0x496634: jne      0x496707
0x49663A: xor      eax, eax
0x49663C: mov      dword ptr [ebp - 0x14], eax
0x49663F: mov      eax, dword ptr [ebp - 4]
0x496642: mov      eax, dword ptr [eax + 0x220]
0x496648: mov      eax, dword ptr [eax + 8]
0x49664B: cmp      eax, dword ptr [ebp - 0x14]
0x49664E: jle      0x4966db
0x496654: mov      edx, dword ptr [ebp - 0x14]
0x496657: mov      eax, dword ptr [ebp - 4]
0x49665A: mov      eax, dword ptr [eax + 0x220]
0x496660: call     0x40f6a0
0x496665: cmp      dword ptr [eax], 0xbb8
0x49666B: jl       0x496686
0x49666D: mov      edx, dword ptr [ebp - 0x14]
0x496670: mov      eax, dword ptr [ebp - 4]
0x496673: mov      eax, dword ptr [eax + 0x220]
0x496679: call     0x40f6a0
0x49667E: cmp      dword ptr [eax], 0xc1b
0x496684: jle      0x4966a1
0x496686: mov      edx, dword ptr [ebp - 0x14]
0x496689: mov      eax, dword ptr [ebp - 4]
0x49668C: mov      eax, dword ptr [eax + 0x220]
0x496692: call     0x40f6a0
0x496697: mov      eax, dword ptr [eax]
0x496699: movzx    edx, word ptr [ebp - 6]
0x49669D: cmp      eax, edx
0x49669F: jne      0x4966d3
0x4966A1: mov      edx, dword ptr [ebp - 0x14]
0x4966A4: mov      eax, dword ptr [ebp - 4]
0x4966A7: mov      eax, dword ptr [eax + 0x220]
0x4966AD: call     0x40f6a0
0x4966B2: mov      edx, dword ptr [0x458c8c]
0x4966B8: call     0x4046ec
0x4966BD: mov      edx, dword ptr [ebp - 0x14]
0x4966C0: mov      eax, dword ptr [ebp - 4]
0x4966C3: mov      eax, dword ptr [eax + 0x220]
0x4966C9: call     0x40f5c4
0x4966CE: jmp      0x49663f
0x4966D3: inc      dword ptr [ebp - 0x14]
0x4966D6: jmp      0x49663f
0x4966DB: mov      eax, dword ptr [ebp + 0x1c]
0x4966DE: push     eax
0x4966DF: mov      eax, dword ptr [ebp + 0x18]
0x4966E2: push     eax
0x4966E3: mov      eax, dword ptr [ebp + 0x14]
0x4966E6: push     eax
0x4966E7: mov      eax, dword ptr [ebp + 0x10]
0x4966EA: push     eax
0x4966EB: mov      eax, dword ptr [ebp + 0xc]
0x4966EE: push     eax
0x4966EF: mov      eax, dword ptr [ebp + 8]
0x4966F2: push     eax
0x4966F3: mov      ecx, dword ptr [ebp - 0xc]
0x4966F6: mov      dx, word ptr [ebp - 6]
0x4966FA: mov      eax, dword ptr [ebp - 4]
0x4966FD: call     0x496548
0x496702: jmp      0x4967ab
0x496707: mov      eax, dword ptr [ebp - 4]
0x49670A: mov      eax, dword ptr [eax + 0x220]
0x496710: cmp      dword ptr [eax + 8], 0
0x496714: jle      0x496784
0x496716: mov      eax, dword ptr [ebp - 4]
0x496719: mov      eax, dword ptr [eax + 0x220]
0x49671F: mov      eax, dword ptr [eax + 8]
0x496722: dec      eax
0x496723: test     eax, eax
0x496725: jl       0x496784
0x496727: inc      eax
0x496728: mov      dword ptr [ebp - 0x18], eax
0x49672B: mov      dword ptr [ebp - 0x10], 0
0x496732: mov      edx, dword ptr [ebp - 0x10]
0x496735: mov      eax, dword ptr [ebp - 4]
0x496738: mov      eax, dword ptr [eax + 0x220]
0x49673E: call     0x40f6a0
0x496743: mov      eax, dword ptr [eax]
0x496745: movzx    edx, word ptr [ebp - 6]
0x496749: cmp      eax, edx
0x49674B: jne      0x49677c
0x49674D: mov      edx, dword ptr [ebp - 0x10]
0x496750: mov      eax, dword ptr [ebp - 4]
0x496753: mov      eax, dword ptr [eax + 0x220]
0x496759: call     0x40f6a0
0x49675E: mov      edx, dword ptr [0x458c8c]
0x496764: call     0x4046ec
0x496769: mov      edx, dword ptr [ebp - 0x10]
0x49676C: mov      eax, dword ptr [ebp - 4]
0x49676F: mov      eax, dword ptr [eax + 0x220]
0x496775: call     0x40f5c4
0x49677A: jmp      0x496784
0x49677C: inc      dword ptr [ebp - 0x10]
0x49677F: dec      dword ptr [ebp - 0x18]
0x496782: jne      0x496732
0x496784: mov      eax, dword ptr [ebp + 0x1c]
0x496787: push     eax
0x496788: mov      eax, dword ptr [ebp + 0x18]
0x49678B: push     eax
0x49678C: mov      eax, dword ptr [ebp + 0x14]
0x49678F: push     eax
0x496790: mov      eax, dword ptr [ebp + 0x10]
0x496793: push     eax
0x496794: mov      eax, dword ptr [ebp + 0xc]
0x496797: push     eax
0x496798: mov      eax, dword ptr [ebp + 8]
0x49679B: push     eax
0x49679C: mov      ecx, dword ptr [ebp - 0xc]
0x49679F: mov      dx, word ptr [ebp - 6]
0x4967A3: mov      eax, dword ptr [ebp - 4]
0x4967A6: call     0x496548
0x4967AB: xor      eax, eax
0x4967AD: pop      edx
0x4967AE: pop      ecx
0x4967AF: pop      ecx
0x4967B0: mov      dword ptr fs:[eax], edx
0x4967B3: push     0x4967c8
0x4967B8: lea      eax, [ebp + 0xc]
0x4967BB: call     0x403a58
0x4967C0: ret      
0x4967C1: jmp      0x403518
0x4967C6: jmp      0x4967b8
0x4967C8: mov      esp, ebp
0x4967CA: pop      ebp
0x4967CB: ret      0x18
0x4967CE: mov      eax, eax
0x4967D0: push     ebp
0x4967D1: mov      ebp, esp
0x4967D3: add      esp, -8
0x4967D6: mov      dword ptr [ebp - 4], eax
0x4967D9: xor      eax, eax
0x4967DB: mov      dword ptr [ebp - 8], eax
0x4967DE: mov      eax, dword ptr [ebp - 4]
0x4967E1: mov      eax, dword ptr [eax + 0x220]
0x4967E7: mov      eax, dword ptr [eax + 8]
0x4967EA: cmp      eax, dword ptr [ebp - 8]
0x4967ED: jle      0x496855
0x4967EF: mov      edx, dword ptr [ebp - 8]
0x4967F2: mov      eax, dword ptr [ebp - 4]
0x4967F5: mov      eax, dword ptr [eax + 0x220]
0x4967FB: call     0x40f6a0
0x496800: cmp      dword ptr [eax], 0xbb8
0x496806: jl       0x496850
0x496808: mov      edx, dword ptr [ebp - 8]
0x49680B: mov      eax, dword ptr [ebp - 4]
0x49680E: mov      eax, dword ptr [eax + 0x220]
0x496814: call     0x40f6a0
0x496819: cmp      dword ptr [eax], 0xc1b
0x49681F: jg       0x496850
0x496821: mov      edx, dword ptr [ebp - 8]
0x496824: mov      eax, dword ptr [ebp - 4]
0x496827: mov      eax, dword ptr [eax + 0x220]
0x49682D: call     0x40f6a0
0x496832: mov      edx, dword ptr [0x458c8c]
0x496838: call     0x4046ec
0x49683D: mov      edx, dword ptr [ebp - 8]
0x496840: mov      eax, dword ptr [ebp - 4]
0x496843: mov      eax, dword ptr [eax + 0x220]
0x496849: call     0x40f5c4
0x49684E: jmp      0x4967de
0x496850: inc      dword ptr [ebp - 8]
0x496853: jmp      0x4967de
0x496855: pop      ecx
0x496856: pop      ecx
0x496857: pop      ebp
0x496858: ret      
0x496859: lea      eax, [eax]
0x49685C: push     ebp
0x49685D: mov      ebp, esp
0x49685F: add      esp, -0xc
0x496862: mov      dword ptr [ebp - 4], eax
0x496865: mov      eax, dword ptr [ebp - 4]
0x496868: mov      byte ptr [eax + 0xec], 0
0x49686F: mov      eax, dword ptr [ebp - 4]
0x496872: mov      dword ptr [eax + 0x1b8], 0xffffffff
0x49687C: mov      eax, dword ptr [ebp - 4]
0x49687F: movzx    eax, word ptr [eax + 0x14]
0x496883: call     0x496120
0x496888: mov      edx, dword ptr [ebp - 4]
0x49688B: mov      dword ptr [edx + 0xe0], eax
0x496891: mov      eax, dword ptr [ebp - 4]
0x496894: movzx    edx, word ptr [eax + 0x14]
0x496898: mov      eax, dword ptr [ebp - 4]
0x49689B: movzx    eax, byte ptr [eax + 0xe]
0x49689F: call     0x495ca8
0x4968A4: mov      dword ptr [ebp - 8], eax
0x4968A7: cmp      dword ptr [ebp - 8], 0
0x4968AB: je       0x496ce0
0x4968B1: mov      eax, dword ptr [ebp - 4]
0x4968B4: mov      eax, dword ptr [eax + 0x148]
0x4968BA: add      eax, -6
0x4968BD: cmp      eax, 0x1c
0x4968C0: ja       0x496ce0
0x4968C6: mov      al, byte ptr [eax + 0x4968d3]
0x4968CC: jmp      dword ptr [eax*4 + 0x4968f0]
0x4968D3: add      al, byte ptr [edx]
0x4968D5: add      byte ptr [edx], al
0x4968D7: add      dword ptr [edx], eax
0x4968D9: add      byte ptr [eax], al
0x4968DB: add      eax, dword ptr [eax]
0x4968DD: add      byte ptr [eax], al
0x4968DF: add      byte ptr [eax], al
0x4968E1: add      byte ptr [eax], al
0x4968E3: add      byte ptr [eax], al
0x4968E5: add      byte ptr [eax], al
0x4968E7: add      byte ptr [eax], al
0x4968E9: add      byte ptr [eax], al
0x4968EB: add      byte ptr [eax + 0x6ce00607], al
0x4968F2: dec      ecx
0x4968F3: add      byte ptr [eax], dl
0x4968F5: imul     ecx, dword ptr [ecx], 0x49699d
0x4968FC: movsb    byte ptr es:[edi], byte ptr [esi]
0x4968FD: push     0x49
0x4968FF: add      byte ptr [eax], dh
0x496901: imul     ecx, dword ptr [ecx], -0x50
0x496905: imul     ecx, dword ptr [ecx], 0x2a
0x496909: insb     byte ptr es:[edi], dx
0x49690A: dec      ecx
0x49690B: add      byte ptr [edi - 0x74ffb694], cl
0x496911: inc      ebp
0x496912: clc      
0x496913: movzx    eax, word ptr [eax + 2]
0x496917: mov      edx, dword ptr [ebp - 8]
0x49691A: movzx    edx, word ptr [edx + 4]
0x49691E: add      eax, edx
0x496920: mov      edx, dword ptr [ebp - 4]
0x496923: movzx    edx, byte ptr [edx + 0xc]
0x496927: imul     edx
0x496929: mov      edx, dword ptr [ebp - 8]
0x49692C: movzx    edx, word ptr [edx]
0x49692F: add      eax, edx
0x496931: mov      edx, dword ptr [ebp - 4]
0x496934: mov      dword ptr [edx + 0x1b0], eax
0x49693A: mov      eax, dword ptr [ebp - 8]
0x49693D: movzx    eax, word ptr [eax + 2]
0x496941: mov      edx, dword ptr [ebp - 4]
0x496944: add      eax, dword ptr [edx + 0x1b0]
0x49694A: dec      eax
0x49694B: mov      edx, dword ptr [ebp - 4]
0x49694E: mov      dword ptr [edx + 0x1b4], eax
0x496954: mov      eax, dword ptr [ebp - 8]
0x496957: movzx    eax, word ptr [eax + 6]
0x49695B: mov      edx, dword ptr [ebp - 4]
0x49695E: mov      dword ptr [edx + 0x1d0], eax
0x496964: call     0x406154
0x496969: mov      edx, dword ptr [ebp - 4]
0x49696C: mov      dword ptr [edx + 0x1d4], eax
0x496972: mov      eax, dword ptr [ebp - 8]
0x496975: movzx    eax, word ptr [eax + 2]
0x496979: mov      edx, dword ptr [ebp - 4]
0x49697C: mov      dword ptr [edx + 0x1f4], eax
0x496982: push     0
0x496984: push     1
0x496986: mov      eax, dword ptr [ebp - 4]
0x496989: xor      edx, edx
0x49698B: mov      dl, byte ptr [eax + 0xc]
0x49698E: xor      ecx, ecx
0x496990: mov      eax, dword ptr [ebp - 4]
0x496993: call     0x497610
0x496998: jmp      0x496ce0
0x49699D: mov      eax, dword ptr [ebp - 8]
0x4969A0: movzx    eax, word ptr [eax + 0xc]
0x4969A4: mov      edx, dword ptr [ebp - 8]
0x4969A7: movzx    edx, word ptr [edx + 0xe]
0x4969AB: add      eax, edx
0x4969AD: mov      edx, dword ptr [ebp - 4]
0x4969B0: movzx    edx, byte ptr [edx + 0xc]
0x4969B4: imul     edx
0x4969B6: mov      edx, dword ptr [ebp - 8]
0x4969B9: movzx    edx, word ptr [edx + 0xa]
0x4969BD: add      eax, edx
0x4969BF: mov      edx, dword ptr [ebp - 4]
0x4969C2: mov      dword ptr [edx + 0x1b0], eax
0x4969C8: mov      eax, dword ptr [ebp - 8]
0x4969CB: movzx    eax, word ptr [eax + 0xc]
0x4969CF: mov      edx, dword ptr [ebp - 4]
0x4969D2: add      eax, dword ptr [edx + 0x1b0]
0x4969D8: dec      eax
0x4969D9: mov      edx, dword ptr [ebp - 4]
0x4969DC: mov      dword ptr [edx + 0x1b4], eax
0x4969E2: mov      eax, dword ptr [ebp - 4]
0x4969E5: mov      eax, dword ptr [eax + 0x160]
0x4969EB: mov      edx, dword ptr [ebp - 4]
0x4969EE: mov      dword ptr [edx + 0x1d0], eax
0x4969F4: call     0x406154
0x4969F9: mov      edx, dword ptr [ebp - 4]
0x4969FC: mov      dword ptr [edx + 0x1d4], eax
0x496A02: mov      eax, dword ptr [ebp - 8]
0x496A05: movzx    eax, byte ptr [eax + 0x12]
0x496A09: mov      edx, dword ptr [ebp - 4]
0x496A0C: mov      dword ptr [edx + 0x1d8], eax
0x496A12: mov      eax, dword ptr [ebp - 4]
0x496A15: xor      edx, edx
0x496A17: mov      dword ptr [eax + 0x1dc], edx
0x496A1D: mov      eax, dword ptr [ebp - 4]
0x496A20: mov      dword ptr [eax + 0x1e0], 1
0x496A2A: mov      eax, dword ptr [ebp - 4]
0x496A2D: cmp      dword ptr [eax + 0x148], 9
0x496A34: jne      0x496a70
0x496A36: push     0
0x496A38: mov      eax, dword ptr [ebp - 4]
0x496A3B: mov      eax, dword ptr [eax + 0x1b4]
0x496A41: mov      edx, dword ptr [ebp - 4]
0x496A44: sub      eax, dword ptr [edx + 0x1b0]
0x496A4A: inc      eax
0x496A4B: push     eax
0x496A4C: mov      eax, dword ptr [ebp - 4]
0x496A4F: movzx    eax, byte ptr [eax + 0xc]
0x496A53: call     0x4a023c
0x496A58: mov      edx, eax
0x496A5A: mov      eax, dword ptr [ebp - 4]
0x496A5D: mov      ecx, dword ptr [eax + 0x1e0]
0x496A63: mov      eax, dword ptr [ebp - 4]
0x496A66: call     0x497610
0x496A6B: jmp      0x496ce0
0x496A70: push     0
0x496A72: mov      eax, dword ptr [ebp - 4]
0x496A75: mov      eax, dword ptr [eax + 0x1b4]
0x496A7B: mov      edx, dword ptr [ebp - 4]
0x496A7E: sub      eax, dword ptr [edx + 0x1b0]
0x496A84: inc      eax
0x496A85: push     eax
0x496A86: mov      eax, dword ptr [ebp - 4]
0x496A89: xor      edx, edx
0x496A8B: mov      dl, byte ptr [eax + 0xc]
0x496A8E: mov      eax, dword ptr [ebp - 4]
0x496A91: mov      ecx, dword ptr [eax + 0x1e0]
0x496A97: mov      eax, dword ptr [ebp - 4]
0x496A9A: call     0x497610
0x496A9F: jmp      0x496ce0
0x496AA4: mov      eax, dword ptr [ebp - 8]
0x496AA7: movzx    eax, word ptr [eax + 0x16]
0x496AAB: mov      edx, dword ptr [ebp - 8]
0x496AAE: movzx    edx, word ptr [edx + 0x18]
0x496AB2: add      eax, edx
0x496AB4: mov      edx, dword ptr [ebp - 4]
0x496AB7: movzx    edx, byte ptr [edx + 0xc]
0x496ABB: imul     edx
0x496ABD: mov      edx, dword ptr [ebp - 8]
0x496AC0: movzx    edx, word ptr [edx + 0x14]
0x496AC4: add      eax, edx
0x496AC6: mov      edx, dword ptr [ebp - 4]
0x496AC9: mov      dword ptr [edx + 0x1b0], eax
0x496ACF: mov      eax, dword ptr [ebp - 8]
0x496AD2: movzx    eax, word ptr [eax + 0x16]
0x496AD6: mov      edx, dword ptr [ebp - 4]
0x496AD9: add      eax, dword ptr [edx + 0x1b0]
0x496ADF: dec      eax
0x496AE0: mov      edx, dword ptr [ebp - 4]
0x496AE3: mov      dword ptr [edx + 0x1b4], eax
0x496AE9: mov      eax, dword ptr [ebp - 8]
0x496AEC: movzx    eax, word ptr [eax + 0x1a]
0x496AF0: mov      edx, dword ptr [ebp - 4]
0x496AF3: mov      dword ptr [edx + 0x1d0], eax
0x496AF9: call     0x406154
0x496AFE: mov      edx, dword ptr [ebp - 4]
0x496B01: mov      dword ptr [edx + 0x1d4], eax
0x496B07: call     0x406154
0x496B0C: mov      edx, dword ptr [ebp - 4]
0x496B0F: mov      dword ptr [edx + 0x150], eax
0x496B15: push     0
0x496B17: push     1
0x496B19: mov      eax, dword ptr [ebp - 4]
0x496B1C: xor      edx, edx
0x496B1E: mov      dl, byte ptr [eax + 0xc]
0x496B21: xor      ecx, ecx
0x496B23: mov      eax, dword ptr [ebp - 4]
0x496B26: call     0x497610
0x496B2B: jmp      0x496ce0
0x496B30: mov      eax, dword ptr [ebp - 8]
0x496B33: movzx    eax, word ptr [eax + 0x2a]
0x496B37: mov      edx, dword ptr [ebp - 8]
0x496B3A: movzx    edx, word ptr [edx + 0x2c]
0x496B3E: add      eax, edx
0x496B40: mov      edx, dword ptr [ebp - 4]
0x496B43: movzx    edx, byte ptr [edx + 0xc]
0x496B47: imul     edx
0x496B49: mov      edx, dword ptr [ebp - 8]
0x496B4C: movzx    edx, word ptr [edx + 0x28]
0x496B50: add      eax, edx
0x496B52: mov      edx, dword ptr [ebp - 4]
0x496B55: mov      dword ptr [edx + 0x1b0], eax
0x496B5B: mov      eax, dword ptr [ebp - 8]
0x496B5E: movzx    eax, word ptr [eax + 0x2a]
0x496B62: mov      edx, dword ptr [ebp - 4]
0x496B65: add      eax, dword ptr [edx + 0x1b0]
0x496B6B: dec      eax
0x496B6C: mov      edx, dword ptr [ebp - 4]
0x496B6F: mov      dword ptr [edx + 0x1b4], eax
0x496B75: mov      eax, dword ptr [ebp - 4]
0x496B78: mov      eax, dword ptr [eax + 0x1e8]
0x496B7E: mov      edx, dword ptr [ebp - 4]
0x496B81: mov      dword ptr [edx + 0x1d0], eax
0x496B87: call     0x406154
0x496B8C: mov      edx, dword ptr [ebp - 4]
0x496B8F: mov      dword ptr [edx + 0x1d4], eax
0x496B95: push     0
0x496B97: push     1
0x496B99: mov      eax, dword ptr [ebp - 4]
0x496B9C: xor      edx, edx
0x496B9E: mov      dl, byte ptr [eax + 0xc]
0x496BA1: xor      ecx, ecx
0x496BA3: mov      eax, dword ptr [ebp - 4]
0x496BA6: call     0x497610
0x496BAB: jmp      0x496ce0
0x496BB0: mov      eax, dword ptr [ebp - 8]
0x496BB3: movzx    eax, word ptr [eax + 0x34]
0x496BB7: mov      edx, dword ptr [ebp - 8]
0x496BBA: movzx    edx, word ptr [edx + 0x36]
0x496BBE: add      eax, edx
0x496BC0: mov      edx, dword ptr [ebp - 4]
0x496BC3: movzx    edx, byte ptr [edx + 0xc]
0x496BC7: imul     edx
0x496BC9: mov      edx, dword ptr [ebp - 8]
0x496BCC: movzx    edx, word ptr [edx + 0x32]
0x496BD0: add      eax, edx
0x496BD2: mov      edx, dword ptr [ebp - 4]
0x496BD5: mov      dword ptr [edx + 0x1b0], eax
0x496BDB: mov      eax, dword ptr [ebp - 8]
0x496BDE: movzx    eax, word ptr [eax + 0x34]
0x496BE2: mov      edx, dword ptr [ebp - 4]
0x496BE5: add      eax, dword ptr [edx + 0x1b0]
0x496BEB: dec      eax
0x496BEC: mov      edx, dword ptr [ebp - 4]
0x496BEF: mov      dword ptr [edx + 0x1b4], eax
0x496BF5: mov      eax, dword ptr [ebp - 4]
0x496BF8: mov      eax, dword ptr [eax + 0x1b4]
0x496BFE: mov      edx, dword ptr [ebp - 4]
0x496C01: mov      dword ptr [edx + 0x1b0], eax
0x496C07: mov      eax, dword ptr [ebp - 8]
0x496C0A: movzx    eax, word ptr [eax + 0x38]
0x496C0E: mov      edx, dword ptr [ebp - 4]
0x496C11: mov      dword ptr [edx + 0x1d0], eax
0x496C17: call     0x406154
0x496C1C: mov      edx, dword ptr [ebp - 4]
0x496C1F: mov      dword ptr [edx + 0x1d4], eax
0x496C25: jmp      0x496ce0
0x496C2A: mov      eax, dword ptr [ebp - 8]
0x496C2D: movzx    eax, word ptr [eax + 0x34]
0x496C31: mov      edx, dword ptr [ebp - 8]
0x496C34: movzx    edx, word ptr [edx + 0x36]
0x496C38: add      eax, edx
0x496C3A: mov      edx, dword ptr [ebp - 4]
0x496C3D: movzx    edx, byte ptr [edx + 0xc]
0x496C41: imul     edx
0x496C43: mov      edx, dword ptr [ebp - 8]
0x496C46: movzx    edx, word ptr [edx + 0x32]
0x496C4A: add      eax, edx
0x496C4C: mov      edx, dword ptr [ebp - 4]
0x496C4F: mov      dword ptr [edx + 0x1b0], eax
0x496C55: mov      eax, dword ptr [ebp - 8]
0x496C58: movzx    eax, word ptr [eax + 0x34]
0x496C5C: mov      edx, dword ptr [ebp - 4]
0x496C5F: add      eax, dword ptr [edx + 0x1b0]
0x496C65: dec      eax
0x496C66: mov      edx, dword ptr [ebp - 4]
0x496C69: mov      dword ptr [edx + 0x1b4], eax
0x496C6F: mov      eax, dword ptr [ebp - 8]
0x496C72: movzx    eax, word ptr [eax + 0x38]
0x496C76: mov      edx, dword ptr [ebp - 4]
0x496C79: mov      dword ptr [edx + 0x1d0], eax
0x496C7F: call     0x406154
0x496C84: mov      edx, dword ptr [ebp - 4]
0x496C87: mov      dword ptr [edx + 0x1d4], eax
0x496C8D: jmp      0x496ce0
0x496C8F: mov      eax, dword ptr [ebp - 8]
0x496C92: movzx    eax, word ptr [eax + 0x3c]
0x496C96: mov      edx, dword ptr [ebp - 4]
0x496C99: movzx    edx, byte ptr [edx + 0xc]
0x496C9D: add      eax, edx
0x496C9F: mov      edx, dword ptr [ebp - 4]
0x496CA2: mov      dword ptr [edx + 0x1b0], eax
0x496CA8: mov      eax, dword ptr [ebp - 8]
0x496CAB: movzx    eax, word ptr [eax + 0x3e]
0x496CAF: mov      edx, dword ptr [ebp - 4]
0x496CB2: add      eax, dword ptr [edx + 0x1b0]
0x496CB8: dec      eax
0x496CB9: mov      edx, dword ptr [ebp - 4]
0x496CBC: mov      dword ptr [edx + 0x1b4], eax
0x496CC2: mov      eax, dword ptr [ebp - 8]
0x496CC5: movzx    eax, word ptr [eax + 0x42]
0x496CC9: mov      edx, dword ptr [ebp - 4]
0x496CCC: mov      dword ptr [edx + 0x1d0], eax
0x496CD2: call     0x406154
0x496CD7: mov      edx, dword ptr [ebp - 4]
0x496CDA: mov      dword ptr [edx + 0x1d4], eax
0x496CE0: mov      esp, ebp
0x496CE2: pop      ebp
0x496CE3: ret      
0x496CE4: push     ebp
0x496CE5: mov      ebp, esp
0x496CE7: add      esp, -0x30
0x496CEA: push     ebx
0x496CEB: push     esi
0x496CEC: push     edi
0x496CED: mov      esi, edx
0x496CEF: lea      edi, [ebp - 0x2c]
0x496CF2: mov      ecx, 8
0x496CF7: rep movsd dword ptr es:[edi], dword ptr [esi]
0x496CF9: mov      dword ptr [ebp - 4], eax
0x496CFC: lea      eax, [ebp - 0x2c]
0x496CFF: mov      edx, dword ptr [0x458c8c]
0x496D05: call     0x4043d0
0x496D0A: xor      eax, eax
0x496D0C: push     ebp
0x496D0D: push     0x4970ed
0x496D12: push     dword ptr fs:[eax]
0x496D15: mov      dword ptr fs:[eax], esp
0x496D18: mov      eax, dword ptr [ebp - 4]
0x496D1B: movzx    eax, word ptr [eax + 8]
0x496D1F: mov      edx, dword ptr [ebp - 4]
0x496D22: mov      dword ptr [edx + 0x214], eax
0x496D28: mov      eax, dword ptr [ebp - 4]
0x496D2B: movzx    eax, word ptr [eax + 0xa]
0x496D2F: mov      edx, dword ptr [ebp - 4]
0x496D32: mov      dword ptr [edx + 0x218], eax
0x496D38: cmp      dword ptr [ebp - 0x2c], 0x1b
0x496D3C: jne      0x496d4c
0x496D3E: mov      eax, dword ptr [ebp - 4]
0x496D41: mov      byte ptr [eax + 0x20], 0
0x496D45: mov      eax, dword ptr [ebp - 4]
0x496D48: mov      byte ptr [eax + 0x21], 0
0x496D4C: mov      eax, dword ptr [ebp - 4]
0x496D4F: cmp      byte ptr [eax + 0x20], 0
0x496D53: jne      0x49707e
0x496D59: mov      eax, dword ptr [ebp - 0x2c]
0x496D5C: add      eax, -6
0x496D5F: cmp      eax, 0x15
0x496D62: ja       0x496dc0
0x496D64: mov      al, byte ptr [eax + 0x496d71]
0x496D6A: jmp      dword ptr [eax*4 + 0x496d87]
0x496D71: add      dword ptr [ecx], eax
0x496D73: add      byte ptr [ecx], al
0x496D75: add      dword ptr [ecx], eax
0x496D77: add      byte ptr [ecx], al
0x496D79: add      byte ptr [eax], al
0x496D7B: add      byte ptr [eax], al
0x496D7D: add      byte ptr [eax], al
0x496D7F: add      dword ptr [eax], eax
0x496D81: add      byte ptr [eax], al
0x496D83: add      byte ptr [eax], al
0x496D85: add      byte ptr [ecx], al
0x496D87: shr      byte ptr [ebp + 0x49], 0
0x496D8C: insd     dword ptr es:[edi], dx
0x496D8D: dec      ecx
0x496D8E: add      byte ptr [ebx + 0x558be445], cl
0x496D94: cld      
0x496D95: mov      dword ptr [edx + 0x18], eax
0x496D98: mov      eax, dword ptr [ebp - 0x18]
0x496D9B: mov      edx, dword ptr [ebp - 4]
0x496D9E: mov      dword ptr [edx + 0x1c], eax
0x496DA1: mov      eax, dword ptr [ebp - 4]
0x496DA4: test     byte ptr [eax + 0x1c], 2
0x496DA8: je       0x496db6
0x496DAA: mov      eax, dword ptr [ebp - 4]
0x496DAD: mov      byte ptr [eax + 0x134], 1
0x496DB4: jmp      0x496dc0
0x496DB6: mov      eax, dword ptr [ebp - 4]
0x496DB9: mov      byte ptr [eax + 0x134], 0
0x496DC0: cmp      dword ptr [ebp - 0x2c], 0x17
0x496DC4: jne      0x496dcb
0x496DC6: xor      eax, eax
0x496DC8: mov      dword ptr [ebp - 8], eax
0x496DCB: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x496DD0: mov      eax, dword ptr [eax]
0x496DD2: cmp      eax, dword ptr [ebp - 4]
0x496DD5: jne      0x496f7a
0x496DDB: cmp      dword ptr [ebp - 0x2c], 0xbc3
0x496DE2: jne      0x496dfe
0x496DE4: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x496DE9: mov      eax, dword ptr [eax]
0x496DEB: mov      ecx, dword ptr [ebp - 0x24]
0x496DEE: mov      edx, dword ptr [ebp - 0x28]
0x496DF1: call     0x472ce0
0x496DF6: test     al, al
0x496DF8: je       0x4970d1
0x496DFE: cmp      dword ptr [ebp - 0x2c], 0xbc5
0x496E05: jne      0x496e39
0x496E07: mov      eax, dword ptr [ebp - 0x28]
0x496E0A: push     eax
0x496E0B: mov      eax, dword ptr [ebp - 0x24]
0x496E0E: push     eax
0x496E0F: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x496E14: mov      eax, dword ptr [eax]
0x496E16: movzx    ecx, word ptr [eax + 0xa]
0x496E1A: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x496E1F: mov      eax, dword ptr [eax]
0x496E21: movzx    edx, word ptr [eax + 8]
0x496E25: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x496E2A: mov      eax, dword ptr [eax]
0x496E2C: call     0x472c64
0x496E31: test     al, al
0x496E33: je       0x4970d1
0x496E39: mov      eax, dword ptr [ebp - 0x2c]
0x496E3C: add      eax, 0xfffff443
0x496E41: cmp      eax, 0x14
0x496E44: ja       0x496f4a
0x496E4A: mov      al, byte ptr [eax + 0x496e57]
0x496E50: jmp      dword ptr [eax*4 + 0x496e6c]
0x496E57: add      al, byte ptr [eax]
0x496E59: add      byte ptr [eax], al
0x496E5B: add      byte ptr [ecx], al
0x496E5D: add      dword ptr [ecx], eax
0x496E5F: add      dword ptr [ecx], eax
0x496E61: add      dword ptr [ecx], eax
0x496E63: add      al, 1
0x496E65: add      dword ptr [eax], eax
0x496E67: add      byte ptr [eax], al
0x496E69: add      byte ptr [ecx], al
0x496E6B: add      ecx, dword ptr [edx + 0x6f]
0x496E6E: dec      ecx
0x496E6F: add      byte ptr [eax - 0x5dffb692], al
0x496E75: outsb    dx, byte ptr [esi]
0x496E76: dec      ecx
0x496E77: add      cl, bh
0x496E79: outsb    dx, byte ptr [esi]
0x496E7A: dec      ecx
0x496E7B: add      byte ptr [eax], bl
0x496E7D: outsd    dx, dword ptr [esi]
0x496E7E: dec      ecx
0x496E7F: add      byte ptr [ebx + 0x2405fc45], cl
0x496E85: add      al, byte ptr [eax]
0x496E87: add      byte ptr [ebp + 0xd8bd455], cl
0x496E8D: mov      word ptr [ebp + eax*2 - 0x29cd1800], cs
0x496E94: idiv     bh
0x496E96: sub      dword ptr [ebp - 0x2c], 0xbb8
0x496E9D: jmp      0x496f4a
0x496EA2: mov      eax, dword ptr [ebp - 4]
0x496EA5: cmp      dword ptr [eax + 0x18], 0
0x496EA9: je       0x496eda
0x496EAB: mov      eax, dword ptr [ebp - 0x1c]
0x496EAE: movzx    eax, word ptr [eax + 8]
0x496EB2: mov      edx, dword ptr [ebp - 4]
0x496EB5: mov      dword ptr [edx + 0xc4], eax
0x496EBB: mov      eax, dword ptr [ebp - 0x1c]
0x496EBE: movzx    eax, word ptr [eax + 0xa]
0x496EC2: mov      edx, dword ptr [ebp - 4]
0x496EC5: mov      dword ptr [edx + 0xc8], eax
0x496ECB: mov      eax, dword ptr [ebp - 0x1c]
0x496ECE: mov      eax, dword ptr [eax + 4]
0x496ED1: mov      edx, dword ptr [ebp - 4]
0x496ED4: mov      dword ptr [edx + 0xcc], eax
0x496EDA: mov      eax, dword ptr [ebp - 4]
0x496EDD: add      eax, 0x224
0x496EE2: lea      edx, [ebp - 0x2c]
0x496EE5: mov      ecx, dword ptr [0x458c8c]
0x496EEB: call     0x4044c8
0x496EF0: mov      dword ptr [ebp - 0x2c], 5
0x496EF7: jmp      0x496f4a
0x496EF9: mov      eax, dword ptr [ebp - 4]
0x496EFC: add      eax, 0x224
0x496F01: lea      edx, [ebp - 0x2c]
0x496F04: mov      ecx, dword ptr [0x458c8c]
0x496F0A: call     0x4044c8
0x496F0F: mov      dword ptr [ebp - 0x2c], 8
0x496F16: jmp      0x496f4a
0x496F18: mov      eax, dword ptr [ebp - 4]
0x496F1B: add      eax, 0x224
0x496F20: lea      edx, [ebp - 0x2c]
0x496F23: mov      ecx, dword ptr [0x458c8c]
0x496F29: call     0x4044c8
0x496F2E: mov      eax, dword ptr [ebp - 0x1c]
0x496F31: mov      dword ptr [ebp - 0xc], eax
0x496F34: mov      eax, dword ptr [ebp - 0xc]
0x496F37: mov      eax, dword ptr [eax + 4]
0x496F3A: mov      edx, dword ptr [ebp - 4]
0x496F3D: mov      dword ptr [edx + 0x230], eax
0x496F43: sub      dword ptr [ebp - 0x2c], 0xbb8
0x496F4A: mov      eax, dword ptr [ebp - 4]
0x496F4D: movzx    eax, word ptr [eax + 8]
0x496F51: mov      edx, dword ptr [ebp - 4]
0x496F54: mov      dword ptr [edx + 0x208], eax
0x496F5A: mov      eax, dword ptr [ebp - 4]
0x496F5D: movzx    eax, word ptr [eax + 0xa]
0x496F61: mov      edx, dword ptr [ebp - 4]
0x496F64: mov      dword ptr [edx + 0x20c], eax
0x496F6A: mov      eax, dword ptr [ebp - 4]
0x496F6D: movzx    eax, byte ptr [eax + 0xc]
0x496F71: mov      edx, dword ptr [ebp - 4]
0x496F74: mov      dword ptr [edx + 0x210], eax
0x496F7A: mov      eax, dword ptr [ebp - 0x2c]
0x496F7D: sub      eax, 0x11
0x496F80: je       0x496fe7
0x496F82: sub      eax, 0xe
0x496F85: jne      0x497045
0x496F8B: mov      eax, dword ptr [ebp - 0x28]
0x496F8E: mov      edx, dword ptr [ebp - 4]
0x496F91: mov      dword ptr [edx + 0x174], eax
0x496F97: mov      eax, dword ptr [ebp - 4]
0x496F9A: movzx    eax, byte ptr [eax + 0x30]
0x496F9E: lea      eax, [eax + eax*4]
0x496FA1: mov      edx, 0xc8
0x496FA6: sub      edx, eax
0x496FA8: mov      dword ptr [ebp - 0x30], edx
0x496FAB: fild     dword ptr [ebp - 0x30]
0x496FAE: call     0x4029a0
0x496FB3: mov      dword ptr [ebp - 8], eax
0x496FB6: cmp      dword ptr [ebp - 8], 0x50
0x496FBA: jle      0x496fca
0x496FBC: mov      eax, dword ptr [ebp - 8]
0x496FBF: mov      edx, dword ptr [ebp - 4]
0x496FC2: mov      dword ptr [edx + 0x1e8], eax
0x496FC8: jmp      0x496fd7
0x496FCA: mov      eax, dword ptr [ebp - 4]
0x496FCD: mov      dword ptr [eax + 0x1e8], 0x50
0x496FD7: call     0x406154
0x496FDC: mov      edx, dword ptr [ebp - 4]
0x496FDF: mov      dword ptr [edx + 0x168], eax
0x496FE5: jmp      0x497064
0x496FE7: mov      al, byte ptr [ebp - 0x20]
0x496FEA: mov      edx, dword ptr [ebp - 4]
0x496FED: mov      byte ptr [edx + 0xc], al
0x496FF0: mov      eax, dword ptr [ebp - 0x1c]
0x496FF3: mov      dword ptr [ebp - 0xc], eax
0x496FF6: cmp      dword ptr [ebp - 0xc], 0
0x496FFA: je       0x497064
0x496FFC: mov      eax, dword ptr [ebp - 0xc]
0x496FFF: mov      edx, dword ptr [ebp - 4]
0x497002: mov      esi, eax
0x497004: lea      edi, [edx + 0x10c]
0x49700A: mov      ecx, 9
0x49700F: rep movsd dword ptr es:[edi], dword ptr [esi]
0x497011: mov      eax, dword ptr [ebp - 4]
0x497014: mov      dword ptr [eax + 0x10c], 0xffffffff
0x49701E: mov      eax, dword ptr [ebp - 0x28]
0x497021: mov      edx, dword ptr [ebp - 4]
0x497024: mov      dword ptr [edx + 0x120], eax
0x49702A: mov      eax, dword ptr [ebp - 0x24]
0x49702D: mov      edx, dword ptr [ebp - 4]
0x497030: mov      dword ptr [edx + 0x124], eax
0x497036: mov      edx, 0x24
0x49703B: mov      eax, dword ptr [ebp - 0xc]
0x49703E: call     0x4026cc
0x497043: jmp      0x497064
0x497045: mov      ax, word ptr [ebp - 0x28]
0x497049: mov      edx, dword ptr [ebp - 4]
0x49704C: mov      word ptr [edx + 8], ax
0x497050: mov      ax, word ptr [ebp - 0x24]
0x497054: mov      edx, dword ptr [ebp - 4]
0x497057: mov      word ptr [edx + 0xa], ax
0x49705B: mov      al, byte ptr [ebp - 0x20]
0x49705E: mov      edx, dword ptr [ebp - 4]
0x497061: mov      byte ptr [edx + 0xc], al
0x497064: mov      eax, dword ptr [ebp - 0x2c]
0x497067: mov      edx, dword ptr [ebp - 4]
0x49706A: mov      dword ptr [edx + 0x148], eax
0x497070: mov      eax, dword ptr [ebp - 4]
0x497073: mov      bx, 0xffff
0x497077: call     0x403080
0x49707C: jmp      0x4970a3
0x49707E: cmp      dword ptr [ebp - 0x2c], 0x21
0x497082: jne      0x4970a3
0x497084: mov      eax, dword ptr [ebp - 0x2c]
0x497087: mov      edx, dword ptr [ebp - 4]
0x49708A: mov      dword ptr [edx + 0x148], eax
0x497090: mov      eax, dword ptr [ebp - 4]
0x497093: mov      bx, 0xffff
0x497097: call     0x403080
0x49709C: mov      eax, dword ptr [ebp - 4]
0x49709F: mov      byte ptr [eax + 0x21], 1
0x4970A3: cmp      dword ptr [ebp - 0x2c], 0x20
0x4970A7: je       0x4970af
0x4970A9: cmp      dword ptr [ebp - 0x2c], 0x22
0x4970AD: jne      0x4970c5
0x4970AF: mov      eax, dword ptr [ebp - 4]
0x4970B2: mov      byte ptr [eax + 0x20], 1
0x4970B6: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x4970BB: mov      eax, dword ptr [eax]
0x4970BD: mov      edx, dword ptr [ebp - 4]
0x4970C0: call     0x473490
0x4970C5: mov      eax, dword ptr [ebp - 4]
0x4970C8: mov      bx, 0xfff9
0x4970CC: call     0x403080
0x4970D1: xor      eax, eax
0x4970D3: pop      edx
0x4970D4: pop      ecx
0x4970D5: pop      ecx
0x4970D6: mov      dword ptr fs:[eax], edx
0x4970D9: push     0x4970f4
0x4970DE: lea      eax, [ebp - 0x2c]
0x4970E1: mov      edx, dword ptr [0x458c8c]
0x4970E7: call     0x4042b8
0x4970EC: ret      
0x4970ED: jmp      0x403518
0x4970F2: jmp      0x4970de
0x4970F4: pop      edi
0x4970F5: pop      esi
0x4970F6: pop      ebx
0x4970F7: mov      esp, ebp
0x4970F9: pop      ebp
0x4970FA: ret      
0x4970FB: nop      
0x4970FC: push     ebp
0x4970FD: mov      ebp, esp
0x4970FF: add      esp, -0x28
0x497102: mov      dword ptr [ebp - 4], eax
0x497105: lea      eax, [ebp - 0x28]
0x497108: mov      edx, dword ptr [0x458c8c]
0x49710E: call     0x4041ec
0x497113: xor      eax, eax
0x497115: push     ebp
0x497116: push     0x497379
0x49711B: push     dword ptr fs:[eax]
0x49711E: mov      dword ptr fs:[eax], esp
0x497121: mov      eax, dword ptr [ebp - 4]
0x497124: mov      eax, dword ptr [eax + 0x220]
0x49712A: cmp      dword ptr [eax + 8], 0
0x49712E: jle      0x49735d
0x497134: mov      eax, dword ptr [ebp - 4]
0x497137: cmp      dword ptr [eax + 0x148], 0
0x49713E: jne      0x49735d
0x497144: xor      edx, edx
0x497146: mov      eax, dword ptr [ebp - 4]
0x497149: mov      eax, dword ptr [eax + 0x220]
0x49714F: call     0x40f6a0
0x497154: mov      edx, eax
0x497156: lea      eax, [ebp - 0x28]
0x497159: mov      ecx, dword ptr [0x458c8c]
0x49715F: call     0x4044c8
0x497164: xor      edx, edx
0x497166: mov      eax, dword ptr [ebp - 4]
0x497169: mov      eax, dword ptr [eax + 0x220]
0x49716F: call     0x40f6a0
0x497174: mov      edx, dword ptr [0x458c8c]
0x49717A: call     0x4046ec
0x49717F: xor      edx, edx
0x497181: mov      eax, dword ptr [ebp - 4]
0x497184: mov      eax, dword ptr [eax + 0x220]
0x49718A: call     0x40f5c4
0x49718F: mov      eax, dword ptr [ebp - 0x28]
0x497192: cmp      eax, 0x321
0x497197: jg       0x4971ca
0x497199: je       0x4972bd
0x49719F: cmp      eax, 0x1f
0x4971A2: jg       0x4971b6
0x4971A4: je       0x4971f5
0x4971A6: add      eax, -5
0x4971A9: sub      eax, 0x15
0x4971AC: jb       0x497211
0x4971AE: dec      eax
0x4971AF: je       0x497211
0x4971B1: jmp      0x497121
0x4971B6: add      eax, -0x20
0x4971B9: sub      eax, 3
0x4971BC: jb       0x497211
0x4971BE: sub      eax, 0x2fd
0x4971C3: je       0x497221
0x4971C5: jmp      0x497121
0x4971CA: sub      eax, 0x326
0x4971CF: je       0x49726f
0x4971D5: dec      eax
0x4971D6: je       0x49730d
0x4971DC: add      eax, 0xffffff3f
0x4971E1: sub      eax, 0x64
0x4971E4: jb       0x497211
0x4971E6: add      eax, 0xfffff894
0x4971EB: sub      eax, 0x64
0x4971EE: jb       0x497211
0x4971F0: jmp      0x497121
0x4971F5: mov      eax, dword ptr [ebp - 0xc]
0x4971F8: mov      edx, dword ptr [ebp - 4]
0x4971FB: mov      dword ptr [edx + 0xd0], eax
0x497201: lea      edx, [ebp - 0x28]
0x497204: mov      eax, dword ptr [ebp - 4]
0x497207: call     0x496ce4
0x49720C: jmp      0x497121
0x497211: lea      edx, [ebp - 0x28]
0x497214: mov      eax, dword ptr [ebp - 4]
0x497217: call     0x496ce4
0x49721C: jmp      0x497121
0x497221: push     0xa
0x497223: mov      eax, dword ptr [ebp - 4]
0x497226: movzx    eax, word ptr [eax + 8]
0x49722A: push     eax
0x49722B: mov      eax, dword ptr [ebp - 4]
0x49722E: movzx    eax, word ptr [eax + 0xa]
0x497232: push     eax
0x497233: mov      eax, dword ptr [ebp - 4]
0x497236: push     eax
0x497237: mov      ecx, 0xfa
0x49723C: mov      dl, 1
0x49723E: mov      eax, dword ptr [0x49c2d8]
0x497243: call     0x49dbd0
0x497248: mov      dword ptr [ebp - 8], eax
0x49724B: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x497250: mov      eax, dword ptr [eax]
0x497252: mov      eax, dword ptr [eax + 0x5a860]
0x497258: mov      edx, dword ptr [ebp - 8]
0x49725B: call     0x40f588
0x497260: mov      eax, 0x6d
0x497265: call     0x495794
0x49726A: jmp      0x497121
0x49726F: push     0xa
0x497271: mov      eax, dword ptr [ebp - 4]
0x497274: movzx    eax, word ptr [eax + 8]
0x497278: push     eax
0x497279: mov      eax, dword ptr [ebp - 4]
0x49727C: movzx    eax, word ptr [eax + 0xa]
0x497280: push     eax
0x497281: mov      eax, dword ptr [ebp - 4]
0x497284: push     eax
0x497285: mov      ecx, 0x636
0x49728A: mov      dl, 1
0x49728C: mov      eax, dword ptr [0x49c2d8]
0x497291: call     0x49dbd0
0x497296: mov      dword ptr [ebp - 8], eax
0x497299: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x49729E: mov      eax, dword ptr [eax]
0x4972A0: mov      eax, dword ptr [eax + 0x5a860]
0x4972A6: mov      edx, dword ptr [ebp - 8]
0x4972A9: call     0x40f588
0x4972AE: mov      eax, 0x6d
0x4972B3: call     0x495794
0x4972B8: jmp      0x497121
0x4972BD: push     0xa
0x4972BF: mov      eax, dword ptr [ebp - 4]
0x4972C2: push     eax
0x4972C3: mov      ecx, 0x104
0x4972C8: mov      dl, 1
0x4972CA: mov      eax, dword ptr [0x49c208]
0x4972CF: call     0x49d7fc
0x4972D4: mov      dword ptr [ebp - 8], eax
0x4972D7: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x4972DC: mov      eax, dword ptr [eax]
0x4972DE: mov      eax, dword ptr [eax + 0x5a860]
0x4972E4: mov      edx, dword ptr [ebp - 8]
0x4972E7: call     0x40f588
0x4972EC: mov      dword ptr [ebp - 0x28], 0xa
0x4972F3: lea      edx, [ebp - 0x28]
0x4972F6: mov      eax, dword ptr [ebp - 4]
0x4972F9: call     0x496ce4
0x4972FE: mov      eax, 0x6e
0x497303: call     0x495794
0x497308: jmp      0x497121
0x49730D: push     0xa
0x49730F: mov      eax, dword ptr [ebp - 4]
0x497312: push     eax
0x497313: mov      ecx, 0x640
0x497318: mov      dl, 1
0x49731A: mov      eax, dword ptr [0x49c208]
0x49731F: call     0x49d7fc
0x497324: mov      dword ptr [ebp - 8], eax
0x497327: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x49732C: mov      eax, dword ptr [eax]
0x49732E: mov      eax, dword ptr [eax + 0x5a860]
0x497334: mov      edx, dword ptr [ebp - 8]
0x497337: call     0x40f588
0x49733C: mov      dword ptr [ebp - 0x28], 0xa
0x497343: lea      edx, [ebp - 0x28]
0x497346: mov      eax, dword ptr [ebp - 4]
0x497349: call     0x496ce4
0x49734E: mov      eax, 0x6e
0x497353: call     0x495794
0x497358: jmp      0x497121
0x49735D: xor      eax, eax
0x49735F: pop      edx
0x497360: pop      ecx
0x497361: pop      ecx
0x497362: mov      dword ptr fs:[eax], edx
0x497365: push     0x497380
0x49736A: lea      eax, [ebp - 0x28]
0x49736D: mov      edx, dword ptr [0x458c8c]
0x497373: call     0x4042b8
0x497378: ret      
0x497379: jmp      0x403518
0x49737E: jmp      0x49736a
0x497380: mov      esp, ebp
0x497382: pop      ebp
0x497383: ret      
0x497384: push     ebp
0x497385: mov      ebp, esp
0x497387: add      esp, -0x2c
0x49738A: mov      dword ptr [ebp - 4], eax
0x49738D: lea      eax, [ebp - 0x2c]
0x497390: mov      edx, dword ptr [0x458c8c]
0x497396: call     0x4041ec
0x49739B: xor      eax, eax
0x49739D: push     ebp
0x49739E: push     0x4974de
0x4973A3: push     dword ptr fs:[eax]
0x4973A6: mov      dword ptr fs:[eax], esp
0x4973A9: xor      eax, eax
0x4973AB: mov      dword ptr [ebp - 8], eax
0x4973AE: mov      eax, dword ptr [ebp - 4]
0x4973B1: mov      eax, dword ptr [eax + 0x220]
0x4973B7: mov      eax, dword ptr [eax + 8]
0x4973BA: cmp      eax, dword ptr [ebp - 8]
0x4973BD: jle      0x4974c2
0x4973C3: mov      edx, dword ptr [ebp - 8]
0x4973C6: mov      eax, dword ptr [ebp - 4]
0x4973C9: mov      eax, dword ptr [eax + 0x220]
0x4973CF: call     0x40f6a0
0x4973D4: mov      edx, eax
0x4973D6: lea      eax, [ebp - 0x2c]
0x4973D9: mov      ecx, dword ptr [0x458c8c]
0x4973DF: call     0x4044c8
0x4973E4: mov      byte ptr [ebp - 9], 0
0x4973E8: mov      eax, dword ptr [ebp - 0x2c]
0x4973EB: sub      eax, 0x27e
0x4973F0: je       0x4973fa
0x4973F2: dec      eax
0x4973F3: je       0x497467
0x4973F5: jmp      0x497482
0x4973FA: mov      eax, dword ptr [ebp - 4]
0x4973FD: cmp      dword ptr [eax + 0x10c], 0
0x497404: je       0x497482
0x497406: mov      eax, dword ptr [ebp - 4]
0x497409: mov      dword ptr [eax + 0x10c], 0x6f
0x497413: mov      eax, dword ptr [ebp - 0x28]
0x497416: mov      edx, dword ptr [ebp - 4]
0x497419: mov      dword ptr [edx + 0x114], eax
0x49741F: mov      eax, dword ptr [ebp - 0x24]
0x497422: sub      eax, 0xc
0x497425: jae      0x497433
0x497427: mov      al, byte ptr [ebp - 0x24]
0x49742A: mov      edx, dword ptr [ebp - 4]
0x49742D: mov      byte ptr [edx + 0x118], al
0x497433: mov      eax, dword ptr [ebp - 0x20]
0x497436: mov      edx, dword ptr [ebp - 4]
0x497439: mov      dword ptr [edx + 0x11c], eax
0x49743F: mov      eax, dword ptr [ebp - 0x1c]
0x497442: mov      edx, dword ptr [ebp - 4]
0x497445: mov      dword ptr [edx + 0x120], eax
0x49744B: mov      eax, dword ptr [ebp - 0x18]
0x49744E: mov      edx, dword ptr [ebp - 4]
0x497451: mov      dword ptr [edx + 0x124], eax
0x497457: mov      eax, dword ptr [ebp - 4]
0x49745A: mov      byte ptr [eax + 0x128], 1
0x497461: mov      byte ptr [ebp - 9], 1
0x497465: jmp      0x497482
0x497467: mov      eax, dword ptr [ebp - 4]
0x49746A: cmp      dword ptr [eax + 0x10c], 0
0x497471: je       0x497482
0x497473: mov      eax, dword ptr [ebp - 4]
0x497476: xor      edx, edx
0x497478: mov      dword ptr [eax + 0x10c], edx
0x49747E: mov      byte ptr [ebp - 9], 1
0x497482: cmp      byte ptr [ebp - 9], 0
0x497486: je       0x4974ba
0x497488: mov      edx, dword ptr [ebp - 8]
0x49748B: mov      eax, dword ptr [ebp - 4]
0x49748E: mov      eax, dword ptr [eax + 0x220]
0x497494: call     0x40f6a0
0x497499: mov      edx, dword ptr [0x458c8c]
0x49749F: call     0x4046ec
0x4974A4: mov      edx, dword ptr [ebp - 8]
0x4974A7: mov      eax, dword ptr [ebp - 4]
0x4974AA: mov      eax, dword ptr [eax + 0x220]
0x4974B0: call     0x40f5c4
0x4974B5: jmp      0x4973ae
0x4974BA: inc      dword ptr [ebp - 8]
0x4974BD: jmp      0x4973ae
0x4974C2: xor      eax, eax
0x4974C4: pop      edx
0x4974C5: pop      ecx
0x4974C6: pop      ecx
0x4974C7: mov      dword ptr fs:[eax], edx
0x4974CA: push     0x4974e5
0x4974CF: lea      eax, [ebp - 0x2c]
0x4974D2: mov      edx, dword ptr [0x458c8c]
0x4974D8: call     0x4042b8
0x4974DD: ret      
0x4974DE: jmp      0x403518
0x4974E3: jmp      0x4974cf
0x4974E5: mov      esp, ebp
0x4974E7: pop      ebp
0x4974E8: ret      
0x4974E9: lea      eax, [eax]
0x4974EC: push     ebp
0x4974ED: mov      ebp, esp
0x4974EF: add      esp, -8
0x4974F2: mov      dword ptr [ebp - 4], eax
0x4974F5: mov      eax, dword ptr [ebp - 4]
0x4974F8: cmp      dword ptr [eax + 0x148], 0
0x4974FF: jne      0x497516
0x497501: mov      eax, dword ptr [ebp - 4]
0x497504: mov      eax, dword ptr [eax + 0x220]
0x49750A: cmp      dword ptr [eax + 8], 0
0x49750E: jne      0x497516
0x497510: mov      byte ptr [ebp - 5], 1
0x497514: jmp      0x49751a
0x497516: mov      byte ptr [ebp - 5], 0
0x49751A: mov      al, byte ptr [ebp - 5]
0x49751D: pop      ecx
0x49751E: pop      ecx
0x49751F: pop      ebp
0x497520: ret      
0x497521: lea      eax, [eax]
0x497524: push     ebp
0x497525: mov      ebp, esp
0x497527: add      esp, -8
0x49752A: mov      dword ptr [ebp - 4], eax
0x49752D: mov      eax, dword ptr [ebp - 4]
0x497530: cmp      dword ptr [eax + 0x148], 0
0x497537: je       0x49754d
0x497539: mov      eax, dword ptr [ebp - 4]
0x49753C: mov      eax, dword ptr [eax + 0x1b8]
0x497542: mov      edx, dword ptr [ebp - 4]
0x497545: cmp      eax, dword ptr [edx + 0x1b4]
0x49754B: jl       0x497553
0x49754D: mov      byte ptr [ebp - 5], 1
0x497551: jmp      0x497557
0x497553: mov      byte ptr [ebp - 5], 0
0x497557: mov      al, byte ptr [ebp - 5]
0x49755A: pop      ecx
0x49755B: pop      ecx
0x49755C: pop      ebp
0x49755D: ret      
0x49755E: mov      eax, eax
0x497560: push     ebp
0x497561: mov      ebp, esp
0x497563: add      esp, -8
0x497566: mov      dword ptr [ebp - 4], eax
0x497569: call     0x406154
0x49756E: mov      edx, dword ptr [0x4a3e7c] ; *0x4A3E7C->0x4F944C
0x497574: sub      eax, dword ptr [edx]
0x497576: mov      edx, dword ptr [0x4a40e0] ; *0x4A40E0->0x4F94BC
0x49757C: cmp      eax, dword ptr [edx]
0x49757E: jae      0x497589
0x497580: mov      dword ptr [ebp - 8], 0xffffffff
0x497587: jmp      0x497590
0x497589: mov      dword ptr [ebp - 8], 1
0x497590: mov      eax, dword ptr [ebp - 8]
0x497593: pop      ecx
0x497594: pop      ecx
0x497595: pop      ebp
0x497596: ret      
0x497597: nop      
0x497598: push     ebp
0x497599: mov      ebp, esp
0x49759B: add      esp, -8
0x49759E: mov      dword ptr [ebp - 4], eax
0x4975A1: mov      dword ptr [ebp - 8], 1
0x4975A8: mov      eax, dword ptr [ebp - 4]
0x4975AB: cmp      word ptr [eax + 0x3c], 0xa
0x4975B0: jae      0x4975bb
0x4975B2: mov      dword ptr [ebp - 8], 0xffffffff
0x4975B9: jmp      0x497606
0x4975BB: call     0x406154
0x4975C0: mov      edx, dword ptr [ebp - 4]
0x4975C3: sub      eax, dword ptr [edx + 0x168]
0x4975C9: xor      edx, edx
0x4975CB: push     edx
0x4975CC: push     eax
0x4975CD: mov      eax, dword ptr [0x4a3fa0] ; *0x4A3FA0->0x4A2D10
0x4975D2: mov      eax, dword ptr [eax]
0x4975D4: cdq      
0x4975D5: cmp      edx, dword ptr [esp + 4]
0x4975D9: jne      0x4975e4
0x4975DB: cmp      eax, dword ptr [esp]
0x4975DE: pop      edx
0x4975DF: pop      eax
0x4975E0: ja       0x4975ff
0x4975E2: jmp      0x4975e8
0x4975E4: pop      edx
0x4975E5: pop      eax
0x4975E6: jg       0x4975ff
0x4975E8: call     0x406154
0x4975ED: mov      edx, dword ptr [0x4a3e7c] ; *0x4A3E7C->0x4F944C
0x4975F3: sub      eax, dword ptr [edx]
0x4975F5: mov      edx, dword ptr [0x4a40e0] ; *0x4A40E0->0x4F94BC
0x4975FB: cmp      eax, dword ptr [edx]
0x4975FD: jae      0x497606
0x4975FF: mov      dword ptr [ebp - 8], 0xfffffffe
0x497606: mov      eax, dword ptr [ebp - 8]
0x497609: pop      ecx
0x49760A: pop      ecx
0x49760B: pop      ebp
0x49760C: ret      
0x49760D: lea      eax, [eax]
0x497610: push     ebp
0x497611: mov      ebp, esp
0x497613: add      esp, -0x20
0x497616: mov      dword ptr [ebp - 0xc], ecx
0x497619: mov      dword ptr [ebp - 8], edx
0x49761C: mov      dword ptr [ebp - 4], eax
0x49761F: mov      eax, dword ptr [ebp - 0xc]
0x497622: shl      eax, 4
0x497625: lea      eax, [eax + eax*2]
0x497628: mov      dword ptr [ebp - 0x10], eax
0x49762B: mov      eax, dword ptr [ebp - 0xc]
0x49762E: shl      eax, 5
0x497631: mov      dword ptr [ebp - 0x14], eax
0x497634: mov      eax, dword ptr [ebp + 0xc]
0x497637: cmp      eax, dword ptr [ebp + 8]
0x49763A: jle      0x497642
0x49763C: mov      eax, dword ptr [ebp + 8]
0x49763F: mov      dword ptr [ebp + 0xc], eax
0x497642: mov      eax, dword ptr [ebp - 4]
0x497645: movzx    eax, word ptr [eax + 8]
0x497649: mov      edx, dword ptr [ebp - 4]
0x49764C: mov      dword ptr [edx + 0xb8], eax
0x497652: mov      eax, dword ptr [ebp - 4]
0x497655: movzx    eax, word ptr [eax + 0xa]
0x497659: mov      edx, dword ptr [ebp - 4]
0x49765C: mov      dword ptr [edx + 0xbc], eax
0x497662: mov      eax, dword ptr [ebp + 8]
0x497665: sub      eax, dword ptr [ebp + 0xc]
0x497668: dec      eax
0x497669: mov      dword ptr [ebp - 0x20], eax
0x49766C: fild     dword ptr [ebp - 0x20]
0x49766F: fild     dword ptr [ebp + 8]
0x497672: fdivp    st(1)
0x497674: call     0x4029a0
0x497679: imul     dword ptr [ebp - 0xc]
0x49767C: mov      dword ptr [ebp - 0x18], eax
0x49767F: mov      eax, dword ptr [ebp - 8]
0x497682: cmp      eax, 7
0x497685: ja       0x497ca3
0x49768B: jmp      dword ptr [eax*4 + 0x497692]
0x497692: mov      dl, 0x76
0x497694: dec      ecx
0x497695: add      byte ptr [edi], bh
0x497697: ja       0x4976e2
0x497699: add      byte ptr [eax + edi*2], ch
0x49769C: dec      ecx
0x49769D: add      byte ptr [esi - 0x5cffb688], dh
0x4976A3: jns      0x4976ee
0x4976A5: add      byte ptr [edi + 0x7a], al
0x4976A8: dec      ecx
0x4976A9: add      byte ptr [ebx + edi*2], dh
0x4976AC: dec      ecx
0x4976AD: add      byte ptr [esi - 0x74ffb685], bh
0x4976B3: inc      ebp
0x4976B4: or       byte ptr [ebx], ch
0x4976B6: inc      ebp
0x4976B7: or       al, 0x89
0x4976B9: inc      ebp
0x4976BA: loopne   0x497697
0x4976BC: inc      ebp
0x4976BD: loopne   0x49769a
0x4976BF: inc      ebp
0x4976C0: or       dh, bl
0x4976C2: stc      
0x4976C3: call     0x4029a0
0x4976C8: imul     dword ptr [ebp - 0xc]
0x4976CB: mov      dword ptr [ebp - 0x18], eax
0x4976CE: mov      eax, dword ptr [ebp - 4]
0x4976D1: xor      edx, edx
0x4976D3: mov      dword ptr [eax + 0x98], edx
0x4976D9: mov      eax, dword ptr [ebp - 4]
0x4976DC: movzx    eax, word ptr [eax + 0xa]
0x4976E0: add      eax, dword ptr [ebp - 0x18]
0x4976E3: mov      edx, dword ptr [ebp - 4]
0x4976E6: mov      dword ptr [edx + 0xbc], eax
0x4976EC: mov      eax, dword ptr [ebp - 0x18]
0x4976EF: cmp      eax, dword ptr [ebp - 0xc]
0x4976F2: jne      0x497716
0x4976F4: fild     dword ptr [ebp - 0x14]
0x4976F7: fild     dword ptr [ebp + 8]
0x4976FA: fdivp    st(1)
0x4976FC: fild     dword ptr [ebp + 0xc]
0x4976FF: fmulp    st(1)
0x497701: call     0x4029a0
0x497706: neg      eax
0x497708: mov      edx, dword ptr [ebp - 4]
0x49770B: mov      dword ptr [edx + 0x9c], eax
0x497711: jmp      0x497ca3
0x497716: fild     dword ptr [ebp - 0x14]
0x497719: fild     dword ptr [ebp + 8]
0x49771C: fdivp    st(1)
0x49771E: mov      eax, dword ptr [ebp + 8]
0x497721: sub      eax, dword ptr [ebp + 0xc]
0x497724: mov      dword ptr [ebp - 0x20], eax
0x497727: fild     dword ptr [ebp - 0x20]
0x49772A: fmulp    st(1)
0x49772C: call     0x4029a0
0x497731: mov      edx, dword ptr [ebp - 4]
0x497734: mov      dword ptr [edx + 0x9c], eax
0x49773A: jmp      0x497ca3
0x49773F: cmp      dword ptr [ebp + 8], 6
0x497743: jl       0x49774e
0x497745: mov      dword ptr [ebp - 0x1c], 2
0x49774C: jmp      0x497753
0x49774E: xor      eax, eax
0x497750: mov      dword ptr [ebp - 0x1c], eax
0x497753: mov      eax, dword ptr [ebp + 8]
0x497756: sub      eax, dword ptr [ebp + 0xc]
0x497759: add      eax, dword ptr [ebp - 0x1c]
0x49775C: mov      dword ptr [ebp - 0x20], eax
0x49775F: fild     dword ptr [ebp - 0x20]
0x497762: fild     dword ptr [ebp + 8]
0x497765: fdivp    st(1)
0x497767: call     0x4029a0
0x49776C: imul     dword ptr [ebp - 0xc]
0x49776F: mov      dword ptr [ebp - 0x18], eax
0x497772: mov      eax, dword ptr [ebp - 4]
0x497775: movzx    eax, word ptr [eax + 8]
0x497779: sub      eax, dword ptr [ebp - 0x18]
0x49777C: mov      edx, dword ptr [ebp - 4]
0x49777F: mov      dword ptr [edx + 0xb8], eax
0x497785: mov      eax, dword ptr [ebp - 4]
0x497788: movzx    eax, word ptr [eax + 0xa]
0x49778C: add      eax, dword ptr [ebp - 0x18]
0x49778F: mov      edx, dword ptr [ebp - 4]
0x497792: mov      dword ptr [edx + 0xbc], eax
0x497798: mov      eax, dword ptr [ebp - 0x18]
0x49779B: cmp      eax, dword ptr [ebp - 0xc]
0x49779E: jne      0x4977dd
0x4977A0: fild     dword ptr [ebp - 0x10]
0x4977A3: fild     dword ptr [ebp + 8]
0x4977A6: fdivp    st(1)
0x4977A8: fild     dword ptr [ebp + 0xc]
0x4977AB: fmulp    st(1)
0x4977AD: call     0x4029a0
0x4977B2: mov      edx, dword ptr [ebp - 4]
0x4977B5: mov      dword ptr [edx + 0x98], eax
0x4977BB: fild     dword ptr [ebp - 0x14]
0x4977BE: fild     dword ptr [ebp + 8]
0x4977C1: fdivp    st(1)
0x4977C3: fild     dword ptr [ebp + 0xc]
0x4977C6: fmulp    st(1)
0x4977C8: call     0x4029a0
0x4977CD: neg      eax
0x4977CF: mov      edx, dword ptr [ebp - 4]
0x4977D2: mov      dword ptr [edx + 0x9c], eax
0x4977D8: jmp      0x497ca3
0x4977DD: fild     dword ptr [ebp - 0x10]
0x4977E0: fild     dword ptr [ebp + 8]
0x4977E3: fdivp    st(1)
0x4977E5: mov      eax, dword ptr [ebp + 8]
0x4977E8: sub      eax, dword ptr [ebp + 0xc]
0x4977EB: mov      dword ptr [ebp - 0x20], eax
0x4977EE: fild     dword ptr [ebp - 0x20]
0x4977F1: fmulp    st(1)
0x4977F3: call     0x4029a0
0x4977F8: neg      eax
0x4977FA: mov      edx, dword ptr [ebp - 4]
0x4977FD: mov      dword ptr [edx + 0x98], eax
0x497803: fild     dword ptr [ebp - 0x14]
0x497806: fild     dword ptr [ebp + 8]
0x497809: fdivp    st(1)
0x49780B: mov      eax, dword ptr [ebp + 8]
0x49780E: sub      eax, dword ptr [ebp + 0xc]
0x497811: mov      dword ptr [ebp - 0x20], eax
0x497814: fild     dword ptr [ebp - 0x20]
0x497817: fmulp    st(1)
0x497819: call     0x4029a0
0x49781E: mov      edx, dword ptr [ebp - 4]
0x497821: mov      dword ptr [edx + 0x9c], eax
0x497827: jmp      0x497ca3
0x49782C: mov      eax, dword ptr [ebp + 8]
0x49782F: sub      eax, dword ptr [ebp + 0xc]
0x497832: mov      dword ptr [ebp - 0x20], eax
0x497835: fild     dword ptr [ebp - 0x20]
0x497838: fild     dword ptr [ebp + 8]
0x49783B: fdivp    st(1)
0x49783D: call     0x4029a0
0x497842: imul     dword ptr [ebp - 0xc]
0x497845: mov      dword ptr [ebp - 0x18], eax
0x497848: mov      eax, dword ptr [ebp - 4]
0x49784B: movzx    eax, word ptr [eax + 8]
0x49784F: sub      eax, dword ptr [ebp - 0x18]
0x497852: mov      edx, dword ptr [ebp - 4]
0x497855: mov      dword ptr [edx + 0xb8], eax
0x49785B: mov      eax, dword ptr [ebp - 0x18]
0x49785E: cmp      eax, dword ptr [ebp - 0xc]
0x497861: jne      0x497880
0x497863: fild     dword ptr [ebp - 0x10]
0x497866: fild     dword ptr [ebp + 8]
0x497869: fdivp    st(1)
0x49786B: fild     dword ptr [ebp + 0xc]
0x49786E: fmulp    st(1)
0x497870: call     0x4029a0
0x497875: mov      edx, dword ptr [ebp - 4]
0x497878: mov      dword ptr [edx + 0x98], eax
0x49787E: jmp      0x4978a6
0x497880: fild     dword ptr [ebp - 0x10]
0x497883: fild     dword ptr [ebp + 8]
0x497886: fdivp    st(1)
0x497888: mov      eax, dword ptr [ebp + 8]
0x49788B: sub      eax, dword ptr [ebp + 0xc]
0x49788E: mov      dword ptr [ebp - 0x20], eax
0x497891: fild     dword ptr [ebp - 0x20]
0x497894: fmulp    st(1)
0x497896: call     0x4029a0
0x49789B: neg      eax
0x49789D: mov      edx, dword ptr [ebp - 4]
0x4978A0: mov      dword ptr [edx + 0x98], eax
0x4978A6: mov      eax, dword ptr [ebp - 4]
0x4978A9: xor      edx, edx
0x4978AB: mov      dword ptr [eax + 0x9c], edx
0x4978B1: jmp      0x497ca3
0x4978B6: cmp      dword ptr [ebp + 8], 6
0x4978BA: jl       0x4978c5
0x4978BC: mov      dword ptr [ebp - 0x1c], 2
0x4978C3: jmp      0x4978ca
0x4978C5: xor      eax, eax
0x4978C7: mov      dword ptr [ebp - 0x1c], eax
0x4978CA: mov      eax, dword ptr [ebp + 8]
0x4978CD: sub      eax, dword ptr [ebp + 0xc]
0x4978D0: sub      eax, dword ptr [ebp - 0x1c]
0x4978D3: mov      dword ptr [ebp - 0x20], eax
0x4978D6: fild     dword ptr [ebp - 0x20]
0x4978D9: fild     dword ptr [ebp + 8]
0x4978DC: fdivp    st(1)
0x4978DE: call     0x4029a0
0x4978E3: imul     dword ptr [ebp - 0xc]
0x4978E6: mov      dword ptr [ebp - 0x18], eax
0x4978E9: mov      eax, dword ptr [ebp - 4]
0x4978EC: movzx    eax, word ptr [eax + 8]
0x4978F0: sub      eax, dword ptr [ebp - 0x18]
0x4978F3: mov      edx, dword ptr [ebp - 4]
0x4978F6: mov      dword ptr [edx + 0xb8], eax
0x4978FC: mov      eax, dword ptr [ebp - 4]
0x4978FF: movzx    eax, word ptr [eax + 0xa]
0x497903: sub      eax, dword ptr [ebp - 0x18]
0x497906: mov      edx, dword ptr [ebp - 4]
0x497909: mov      dword ptr [edx + 0xbc], eax
0x49790F: mov      eax, dword ptr [ebp - 0x18]
0x497912: cmp      eax, dword ptr [ebp - 0xc]
0x497915: jne      0x497952
0x497917: fild     dword ptr [ebp - 0x10]
0x49791A: fild     dword ptr [ebp + 8]
0x49791D: fdivp    st(1)
0x49791F: fild     dword ptr [ebp + 0xc]
0x497922: fmulp    st(1)
0x497924: call     0x4029a0
0x497929: mov      edx, dword ptr [ebp - 4]
0x49792C: mov      dword ptr [edx + 0x98], eax
0x497932: fild     dword ptr [ebp - 0x14]
0x497935: fild     dword ptr [ebp + 8]
0x497938: fdivp    st(1)
0x49793A: fild     dword ptr [ebp + 0xc]
0x49793D: fmulp    st(1)
0x49793F: call     0x4029a0
0x497944: mov      edx, dword ptr [ebp - 4]
0x497947: mov      dword ptr [edx + 0x9c], eax
0x49794D: jmp      0x497ca3
0x497952: fild     dword ptr [ebp - 0x10]
0x497955: fild     dword ptr [ebp + 8]
0x497958: fdivp    st(1)
0x49795A: mov      eax, dword ptr [ebp + 8]
0x49795D: sub      eax, dword ptr [ebp + 0xc]
0x497960: mov      dword ptr [ebp - 0x20], eax
0x497963: fild     dword ptr [ebp - 0x20]
0x497966: fmulp    st(1)
0x497968: call     0x4029a0
0x49796D: neg      eax
0x49796F: mov      edx, dword ptr [ebp - 4]
0x497972: mov      dword ptr [edx + 0x98], eax
0x497978: fild     dword ptr [ebp - 0x14]
0x49797B: fild     dword ptr [ebp + 8]
0x49797E: fdivp    st(1)
0x497980: mov      eax, dword ptr [ebp + 8]
0x497983: sub      eax, dword ptr [ebp + 0xc]
0x497986: mov      dword ptr [ebp - 0x20], eax
0x497989: fild     dword ptr [ebp - 0x20]
0x49798C: fmulp    st(1)
0x49798E: call     0x4029a0
0x497993: neg      eax
0x497995: mov      edx, dword ptr [ebp - 4]
0x497998: mov      dword ptr [edx + 0x9c], eax
0x49799E: jmp      0x497ca3
0x4979A3: cmp      dword ptr [ebp + 8], 6
0x4979A7: jl       0x4979b2
0x4979A9: mov      dword ptr [ebp - 0x1c], 1
0x4979B0: jmp      0x4979b7
0x4979B2: xor      eax, eax
0x4979B4: mov      dword ptr [ebp - 0x1c], eax
0x4979B7: mov      eax, dword ptr [ebp + 8]
0x4979BA: sub      eax, dword ptr [ebp + 0xc]
0x4979BD: sub      eax, dword ptr [ebp - 0x1c]
0x4979C0: mov      dword ptr [ebp - 0x20], eax
0x4979C3: fild     dword ptr [ebp - 0x20]
0x4979C6: fild     dword ptr [ebp + 8]
0x4979C9: fdivp    st(1)
0x4979CB: call     0x4029a0
0x4979D0: imul     dword ptr [ebp - 0xc]
0x4979D3: mov      dword ptr [ebp - 0x18], eax
0x4979D6: mov      eax, dword ptr [ebp - 4]
0x4979D9: xor      edx, edx
0x4979DB: mov      dword ptr [eax + 0x98], edx
0x4979E1: mov      eax, dword ptr [ebp - 4]
0x4979E4: movzx    eax, word ptr [eax + 0xa]
0x4979E8: sub      eax, dword ptr [ebp - 0x18]
0x4979EB: mov      edx, dword ptr [ebp - 4]
0x4979EE: mov      dword ptr [edx + 0xbc], eax
0x4979F4: mov      eax, dword ptr [ebp - 0x18]
0x4979F7: cmp      eax, dword ptr [ebp - 0xc]
0x4979FA: jne      0x497a1c
0x4979FC: fild     dword ptr [ebp - 0x14]
0x4979FF: fild     dword ptr [ebp + 8]
0x497A02: fdivp    st(1)
0x497A04: fild     dword ptr [ebp + 0xc]
0x497A07: fmulp    st(1)
0x497A09: call     0x4029a0
0x497A0E: mov      edx, dword ptr [ebp - 4]
0x497A11: mov      dword ptr [edx + 0x9c], eax
0x497A17: jmp      0x497ca3
0x497A1C: fild     dword ptr [ebp - 0x14]
0x497A1F: fild     dword ptr [ebp + 8]
0x497A22: fdivp    st(1)
0x497A24: mov      eax, dword ptr [ebp + 8]
0x497A27: sub      eax, dword ptr [ebp + 0xc]
0x497A2A: mov      dword ptr [ebp - 0x20], eax
0x497A2D: fild     dword ptr [ebp - 0x20]
0x497A30: fmulp    st(1)
0x497A32: call     0x4029a0
0x497A37: neg      eax
0x497A39: mov      edx, dword ptr [ebp - 4]
0x497A3C: mov      dword ptr [edx + 0x9c], eax
0x497A42: jmp      0x497ca3
0x497A47: cmp      dword ptr [ebp + 8], 6
0x497A4B: jl       0x497a56
0x497A4D: mov      dword ptr [ebp - 0x1c], 2
0x497A54: jmp      0x497a5b
0x497A56: xor      eax, eax
0x497A58: mov      dword ptr [ebp - 0x1c], eax
0x497A5B: mov      eax, dword ptr [ebp + 8]
0x497A5E: sub      eax, dword ptr [ebp + 0xc]
0x497A61: sub      eax, dword ptr [ebp - 0x1c]
0x497A64: mov      dword ptr [ebp - 0x20], eax
0x497A67: fild     dword ptr [ebp - 0x20]
0x497A6A: fild     dword ptr [ebp + 8]
0x497A6D: fdivp    st(1)
0x497A6F: call     0x4029a0
0x497A74: imul     dword ptr [ebp - 0xc]
0x497A77: mov      dword ptr [ebp - 0x18], eax
0x497A7A: mov      eax, dword ptr [ebp - 4]
0x497A7D: movzx    eax, word ptr [eax + 8]
0x497A81: add      eax, dword ptr [ebp - 0x18]
0x497A84: mov      edx, dword ptr [ebp - 4]
0x497A87: mov      dword ptr [edx + 0xb8], eax
0x497A8D: mov      eax, dword ptr [ebp - 4]
0x497A90: movzx    eax, word ptr [eax + 0xa]
0x497A94: sub      eax, dword ptr [ebp - 0x18]
0x497A97: mov      edx, dword ptr [ebp - 4]
0x497A9A: mov      dword ptr [edx + 0xbc], eax
0x497AA0: mov      eax, dword ptr [ebp - 0x18]
0x497AA3: cmp      eax, dword ptr [ebp - 0xc]
0x497AA6: jne      0x497ae5
0x497AA8: fild     dword ptr [ebp - 0x10]
0x497AAB: fild     dword ptr [ebp + 8]
0x497AAE: fdivp    st(1)
0x497AB0: fild     dword ptr [ebp + 0xc]
0x497AB3: fmulp    st(1)
0x497AB5: call     0x4029a0
0x497ABA: neg      eax
0x497ABC: mov      edx, dword ptr [ebp - 4]
0x497ABF: mov      dword ptr [edx + 0x98], eax
0x497AC5: fild     dword ptr [ebp - 0x14]
0x497AC8: fild     dword ptr [ebp + 8]
0x497ACB: fdivp    st(1)
0x497ACD: fild     dword ptr [ebp + 0xc]
0x497AD0: fmulp    st(1)
0x497AD2: call     0x4029a0
0x497AD7: mov      edx, dword ptr [ebp - 4]
0x497ADA: mov      dword ptr [edx + 0x9c], eax
0x497AE0: jmp      0x497ca3
0x497AE5: fild     dword ptr [ebp - 0x10]
0x497AE8: fild     dword ptr [ebp + 8]
0x497AEB: fdivp    st(1)
0x497AED: mov      eax, dword ptr [ebp + 8]
0x497AF0: sub      eax, dword ptr [ebp + 0xc]
0x497AF3: mov      dword ptr [ebp - 0x20], eax
0x497AF6: fild     dword ptr [ebp - 0x20]
0x497AF9: fmulp    st(1)
0x497AFB: call     0x4029a0
0x497B00: mov      edx, dword ptr [ebp - 4]
0x497B03: mov      dword ptr [edx + 0x98], eax
0x497B09: fild     dword ptr [ebp - 0x14]
0x497B0C: fild     dword ptr [ebp + 8]
0x497B0F: fdivp    st(1)
0x497B11: mov      eax, dword ptr [ebp + 8]
0x497B14: sub      eax, dword ptr [ebp + 0xc]
0x497B17: mov      dword ptr [ebp - 0x20], eax
0x497B1A: fild     dword ptr [ebp - 0x20]
0x497B1D: fmulp    st(1)
0x497B1F: call     0x4029a0
0x497B24: neg      eax
0x497B26: mov      edx, dword ptr [ebp - 4]
0x497B29: mov      dword ptr [edx + 0x9c], eax
0x497B2F: jmp      0x497ca3
0x497B34: mov      eax, dword ptr [ebp + 8]
0x497B37: sub      eax, dword ptr [ebp + 0xc]
0x497B3A: mov      dword ptr [ebp - 0x20], eax
0x497B3D: fild     dword ptr [ebp - 0x20]
0x497B40: fild     dword ptr [ebp + 8]
0x497B43: fdivp    st(1)
0x497B45: call     0x4029a0
0x497B4A: imul     dword ptr [ebp - 0xc]
0x497B4D: mov      dword ptr [ebp - 0x18], eax
0x497B50: mov      eax, dword ptr [ebp - 4]
0x497B53: movzx    eax, word ptr [eax + 8]
0x497B57: add      eax, dword ptr [ebp - 0x18]
0x497B5A: mov      edx, dword ptr [ebp - 4]
0x497B5D: mov      dword ptr [edx + 0xb8], eax
0x497B63: mov      eax, dword ptr [ebp - 0x18]
0x497B66: cmp      eax, dword ptr [ebp - 0xc]
0x497B69: jne      0x497b8a
0x497B6B: fild     dword ptr [ebp - 0x10]
0x497B6E: fild     dword ptr [ebp + 8]
0x497B71: fdivp    st(1)
0x497B73: fild     dword ptr [ebp + 0xc]
0x497B76: fmulp    st(1)
0x497B78: call     0x4029a0
0x497B7D: neg      eax
0x497B7F: mov      edx, dword ptr [ebp - 4]
0x497B82: mov      dword ptr [edx + 0x98], eax
0x497B88: jmp      0x497bae
0x497B8A: fild     dword ptr [ebp - 0x10]
0x497B8D: fild     dword ptr [ebp + 8]
0x497B90: fdivp    st(1)
0x497B92: mov      eax, dword ptr [ebp + 8]
0x497B95: sub      eax, dword ptr [ebp + 0xc]
0x497B98: mov      dword ptr [ebp - 0x20], eax
0x497B9B: fild     dword ptr [ebp - 0x20]
0x497B9E: fmulp    st(1)
0x497BA0: call     0x4029a0
0x497BA5: mov      edx, dword ptr [ebp - 4]
0x497BA8: mov      dword ptr [edx + 0x98], eax
0x497BAE: mov      eax, dword ptr [ebp - 4]
0x497BB1: xor      edx, edx
0x497BB3: mov      dword ptr [eax + 0x9c], edx
0x497BB9: jmp      0x497ca3
0x497BBE: cmp      dword ptr [ebp + 8], 6
0x497BC2: jl       0x497bcd
0x497BC4: mov      dword ptr [ebp - 0x1c], 2
0x497BCB: jmp      0x497bd2
0x497BCD: xor      eax, eax
0x497BCF: mov      dword ptr [ebp - 0x1c], eax
0x497BD2: mov      eax, dword ptr [ebp + 8]
0x497BD5: sub      eax, dword ptr [ebp + 0xc]
0x497BD8: add      eax, dword ptr [ebp - 0x1c]
0x497BDB: mov      dword ptr [ebp - 0x20], eax
0x497BDE: fild     dword ptr [ebp - 0x20]
0x497BE1: fild     dword ptr [ebp + 8]
0x497BE4: fdivp    st(1)
0x497BE6: call     0x4029a0
0x497BEB: imul     dword ptr [ebp - 0xc]
0x497BEE: mov      dword ptr [ebp - 0x18], eax
0x497BF1: mov      eax, dword ptr [ebp - 4]
0x497BF4: movzx    eax, word ptr [eax + 8]
0x497BF8: add      eax, dword ptr [ebp - 0x18]
0x497BFB: mov      edx, dword ptr [ebp - 4]
0x497BFE: mov      dword ptr [edx + 0xb8], eax
0x497C04: mov      eax, dword ptr [ebp - 4]
0x497C07: movzx    eax, word ptr [eax + 0xa]
0x497C0B: add      eax, dword ptr [ebp - 0x18]
0x497C0E: mov      edx, dword ptr [ebp - 4]
0x497C11: mov      dword ptr [edx + 0xbc], eax
0x497C17: mov      eax, dword ptr [ebp - 0x18]
0x497C1A: cmp      eax, dword ptr [ebp - 0xc]
0x497C1D: jne      0x497c5b
0x497C1F: fild     dword ptr [ebp - 0x10]
0x497C22: fild     dword ptr [ebp + 8]
0x497C25: fdivp    st(1)
0x497C27: fild     dword ptr [ebp + 0xc]
0x497C2A: fmulp    st(1)
0x497C2C: call     0x4029a0
0x497C31: neg      eax
0x497C33: mov      edx, dword ptr [ebp - 4]
0x497C36: mov      dword ptr [edx + 0x98], eax
0x497C3C: fild     dword ptr [ebp - 0x14]
0x497C3F: fild     dword ptr [ebp + 8]
0x497C42: fdivp    st(1)
0x497C44: fild     dword ptr [ebp + 0xc]
0x497C47: fmulp    st(1)
0x497C49: call     0x4029a0
0x497C4E: neg      eax
0x497C50: mov      edx, dword ptr [ebp - 4]
0x497C53: mov      dword ptr [edx + 0x9c], eax
0x497C59: jmp      0x497ca3
0x497C5B: fild     dword ptr [ebp - 0x10]
0x497C5E: fild     dword ptr [ebp + 8]
0x497C61: fdivp    st(1)
0x497C63: mov      eax, dword ptr [ebp + 8]
0x497C66: sub      eax, dword ptr [ebp + 0xc]
0x497C69: mov      dword ptr [ebp - 0x20], eax
0x497C6C: fild     dword ptr [ebp - 0x20]
0x497C6F: fmulp    st(1)
0x497C71: call     0x4029a0
0x497C76: mov      edx, dword ptr [ebp - 4]
0x497C79: mov      dword ptr [edx + 0x98], eax
0x497C7F: fild     dword ptr [ebp - 0x14]
0x497C82: fild     dword ptr [ebp + 8]
0x497C85: fdivp    st(1)
0x497C87: mov      eax, dword ptr [ebp + 8]
0x497C8A: sub      eax, dword ptr [ebp + 0xc]
0x497C8D: mov      dword ptr [ebp - 0x20], eax
0x497C90: fild     dword ptr [ebp - 0x20]
0x497C93: fmulp    st(1)
0x497C95: call     0x4029a0
0x497C9A: mov      edx, dword ptr [ebp - 4]
0x497C9D: mov      dword ptr [edx + 0x9c], eax
0x497CA3: mov      esp, ebp
0x497CA5: pop      ebp
0x497CA6: ret      8
0x497CA9: lea      eax, [eax]
0x497CAC: push     ebp
0x497CAD: mov      ebp, esp
0x497CAF: add      esp, -8
0x497CB2: mov      dword ptr [ebp - 4], eax
0x497CB5: mov      eax, dword ptr [ebp - 4]
0x497CB8: mov      al, byte ptr [eax + 0xe]
0x497CBB: sub      al, 1
0x497CBD: jb       0x497ccc
0x497CBF: sub      al, 0x31
0x497CC1: je       0x497dd4
0x497CC7: jmp      0x497dad
0x497CCC: mov      eax, dword ptr [ebp - 4]
0x497CCF: mov      eax, dword ptr [eax + 0x18]
0x497CD2: call     0x458ce0
0x497CD7: mov      edx, dword ptr [ebp - 4]
0x497CDA: mov      byte ptr [edx + 0xf], al
0x497CDD: mov      eax, dword ptr [ebp - 4]
0x497CE0: mov      eax, dword ptr [eax + 0x18]
0x497CE3: call     0x458cfc
0x497CE8: mov      edx, dword ptr [ebp - 4]
0x497CEB: mov      byte ptr [edx + 0x10], al
0x497CEE: mov      eax, dword ptr [ebp - 4]
0x497CF1: mov      eax, dword ptr [eax + 0x18]
0x497CF4: call     0x458cc4
0x497CF9: mov      edx, dword ptr [ebp - 4]
0x497CFC: mov      byte ptr [edx + 0x11], al
0x497CFF: mov      eax, dword ptr [ebp - 4]
0x497D02: movzx    eax, byte ptr [eax + 0x10]
0x497D06: imul     eax, eax, 0x258
0x497D0C: mov      edx, dword ptr [ebp - 4]
0x497D0F: mov      dword ptr [edx + 0xe0], eax
0x497D15: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x497D1A: mov      eax, dword ptr [eax]
0x497D1C: mov      eax, dword ptr [eax + 0x320]
0x497D22: mov      eax, dword ptr [eax + 0x28]
0x497D25: mov      ecx, 0x258
0x497D2A: cdq      
0x497D2B: idiv     ecx
0x497D2D: sar      eax, 1
0x497D2F: jns      0x497d34
0x497D31: adc      eax, 0
0x497D34: mov      dword ptr [ebp - 8], eax
0x497D37: mov      eax, dword ptr [ebp - 4]
0x497D3A: movzx    eax, byte ptr [eax + 0xf]
0x497D3E: mov      edx, dword ptr [ebp - 8]
0x497D41: dec      edx
0x497D42: cmp      eax, edx
0x497D44: jle      0x497d50
0x497D46: mov      al, byte ptr [ebp - 8]
0x497D49: dec      eax
0x497D4A: mov      edx, dword ptr [ebp - 4]
0x497D4D: mov      byte ptr [edx + 0xf], al
0x497D50: mov      eax, dword ptr [ebp - 4]
0x497D53: mov      al, byte ptr [eax + 0xf]
0x497D56: add      eax, eax
0x497D58: mov      edx, dword ptr [ebp - 4]
0x497D5B: mov      byte ptr [edx + 0xf], al
0x497D5E: mov      eax, dword ptr [ebp - 4]
0x497D61: cmp      byte ptr [eax + 0xf], 1
0x497D65: jbe      0x497d88
0x497D67: mov      eax, dword ptr [ebp - 4]
0x497D6A: movzx    eax, byte ptr [eax + 0xf]
0x497D6E: mov      edx, dword ptr [ebp - 4]
0x497D71: movzx    edx, byte ptr [edx + 0xd]
0x497D75: add      eax, edx
0x497D77: imul     eax, eax, 0x258
0x497D7D: mov      edx, dword ptr [ebp - 4]
0x497D80: mov      dword ptr [edx + 0xe4], eax
0x497D86: jmp      0x497d95
0x497D88: mov      eax, dword ptr [ebp - 4]
0x497D8B: mov      dword ptr [eax + 0xe4], 0xffffffff
0x497D95: mov      eax, dword ptr [ebp - 4]
0x497D98: movzx    eax, byte ptr [eax + 0x11]
0x497D9C: imul     eax, eax, 0x258
0x497DA2: mov      edx, dword ptr [ebp - 4]
0x497DA5: mov      dword ptr [edx + 0xe8], eax
0x497DAB: jmp      0x497dd4
0x497DAD: mov      eax, dword ptr [ebp - 4]
0x497DB0: mov      eax, dword ptr [eax + 0x18]
0x497DB3: call     0x458d1c
0x497DB8: mov      edx, dword ptr [ebp - 4]
0x497DBB: mov      word ptr [edx + 0x14], ax
0x497DBF: mov      eax, dword ptr [ebp - 4]
0x497DC2: movzx    eax, word ptr [eax + 0x14]
0x497DC6: call     0x496120
0x497DCB: mov      edx, dword ptr [ebp - 4]
0x497DCE: mov      dword ptr [edx + 0xe0], eax
0x497DD4: pop      ecx
0x497DD5: pop      ecx
0x497DD6: pop      ebp
0x497DD7: ret      

; === TActor.DMT_-12 @ 0x497DD8 ===
0x497DD8: push     ebp
0x497DD9: mov      ebp, esp
0x497DDB: add      esp, -8
0x497DDE: mov      dword ptr [ebp - 4], eax
0x497DE1: mov      eax, dword ptr [ebp - 4]
0x497DE4: mov      eax, dword ptr [eax + 0x154]
0x497DEA: mov      dword ptr [ebp - 8], eax
0x497DED: mov      eax, dword ptr [ebp - 8]
0x497DF0: pop      ecx
0x497DF1: pop      ecx
0x497DF2: pop      ebp
0x497DF3: ret      

; === TActor.DMT_-13 @ 0x497DF4 ===
0x497DF4: push     ebp
0x497DF5: mov      ebp, esp
0x497DF7: add      esp, -8
0x497DFA: mov      dword ptr [ebp - 4], eax
0x497DFD: mov      eax, dword ptr [ebp - 4]
0x497E00: movzx    eax, word ptr [eax + 0x14]
0x497E04: call     0x495ebc
0x497E09: mov      dword ptr [ebp - 8], eax
0x497E0C: cmp      dword ptr [ebp - 8], 0
0x497E10: je       0x497eac
0x497E16: mov      eax, dword ptr [ebp - 4]
0x497E19: cmp      byte ptr [eax + 0x14c], 0
0x497E20: jne      0x497e5e
0x497E22: mov      eax, dword ptr [ebp - 4]
0x497E25: add      eax, 0xac
0x497E2A: push     eax
0x497E2B: mov      eax, dword ptr [ebp - 4]
0x497E2E: movzx    eax, word ptr [eax + 0x14]
0x497E32: call     0x496120
0x497E37: mov      edx, eax
0x497E39: mov      eax, dword ptr [ebp - 4]
0x497E3C: add      edx, dword ptr [eax + 0x1b8]
0x497E42: mov      eax, dword ptr [ebp - 4]
0x497E45: lea      ecx, [eax + 0xa0]
0x497E4B: mov      eax, dword ptr [ebp - 8]
0x497E4E: call     0x453a48
0x497E53: mov      edx, dword ptr [ebp - 4]
0x497E56: mov      dword ptr [edx + 0x140], eax
0x497E5C: jmp      0x497eac
0x497E5E: mov      eax, dword ptr [ebp - 4]
0x497E61: add      eax, 0xac
0x497E66: push     eax
0x497E67: mov      eax, dword ptr [ebp - 4]
0x497E6A: movzx    eax, word ptr [eax + 0x14]
0x497E6E: call     0x496120
0x497E73: mov      edx, eax
0x497E75: mov      eax, dword ptr [ebp - 4]
0x497E78: add      edx, dword ptr [eax + 0x1b4]
0x497E7E: mov      eax, dword ptr [ebp - 4]
0x497E81: mov      eax, dword ptr [eax + 0x1b8]
0x497E87: mov      ecx, dword ptr [ebp - 4]
0x497E8A: sub      eax, dword ptr [ecx + 0x1b0]
0x497E90: sub      edx, eax
0x497E92: mov      eax, dword ptr [ebp - 4]
0x497E95: lea      ecx, [eax + 0xa0]
0x497E9B: mov      eax, dword ptr [ebp - 8]
0x497E9E: call     0x453a48
0x497EA3: mov      edx, dword ptr [ebp - 4]
0x497EA6: mov      dword ptr [edx + 0x140], eax
0x497EAC: pop      ecx
0x497EAD: pop      ecx
0x497EAE: pop      ebp
0x497EAF: ret      
0x497EB0: push     ebp
0x497EB1: mov      ebp, esp
0x497EB3: add      esp, -8
0x497EB6: mov      dword ptr [ebp - 4], eax
0x497EB9: mov      eax, dword ptr [ebp - 4]
0x497EBC: cmp      dword ptr [eax + 0x140], 0
0x497EC3: je       0x497ed8
0x497EC5: mov      eax, dword ptr [ebp - 4]
0x497EC8: mov      eax, dword ptr [eax + 0x140]
0x497ECE: call     0x44dc60
0x497ED3: mov      dword ptr [ebp - 8], eax
0x497ED6: jmp      0x497edf
0x497ED8: mov      dword ptr [ebp - 8], 0x30
0x497EDF: mov      eax, dword ptr [ebp - 8]
0x497EE2: pop      ecx
0x497EE3: pop      ecx
0x497EE4: pop      ebp
0x497EE5: ret      
0x497EE6: mov      eax, eax
0x497EE8: push     ebp
0x497EE9: mov      ebp, esp
0x497EEB: add      esp, -8
0x497EEE: mov      dword ptr [ebp - 4], eax
0x497EF1: mov      eax, dword ptr [ebp - 4]
0x497EF4: cmp      dword ptr [eax + 0x140], 0
0x497EFB: je       0x497f10
0x497EFD: mov      eax, dword ptr [ebp - 4]
0x497F00: mov      eax, dword ptr [eax + 0x140]
0x497F06: call     0x44d944
0x497F0B: mov      dword ptr [ebp - 8], eax
0x497F0E: jmp      0x497f17
0x497F10: mov      dword ptr [ebp - 8], 0x46
0x497F17: mov      eax, dword ptr [ebp - 8]
0x497F1A: pop      ecx
0x497F1B: pop      ecx
0x497F1C: pop      ebp
0x497F1D: ret      
0x497F1E: mov      eax, eax
0x497F20: push     ebp
0x497F21: mov      ebp, esp
0x497F23: add      esp, -0x14
0x497F26: mov      dword ptr [ebp - 0xc], ecx
0x497F29: mov      dword ptr [ebp - 8], edx
0x497F2C: mov      dword ptr [ebp - 4], eax
0x497F2F: mov      byte ptr [ebp - 0xd], 0
0x497F33: mov      eax, dword ptr [ebp - 4]
0x497F36: cmp      dword ptr [eax + 0x140], 0
0x497F3D: je       0x497fc8
0x497F43: mov      ecx, dword ptr [ebp - 0xc]
0x497F46: mov      edx, dword ptr [ebp - 8]
0x497F49: mov      eax, dword ptr [ebp - 4]
0x497F4C: mov      eax, dword ptr [eax + 0x140]
0x497F52: call     0x44da34
0x497F57: mov      dword ptr [ebp - 0x14], eax
0x497F5A: cmp      dword ptr [ebp - 0x14], 0
0x497F5E: je       0x497fc8
0x497F60: mov      edx, dword ptr [ebp - 8]
0x497F63: dec      edx
0x497F64: mov      ecx, dword ptr [ebp - 0xc]
0x497F67: mov      eax, dword ptr [ebp - 4]
0x497F6A: mov      eax, dword ptr [eax + 0x140]
0x497F70: call     0x44da34
0x497F75: test     eax, eax
0x497F77: je       0x497fc8
0x497F79: mov      edx, dword ptr [ebp - 8]
0x497F7C: inc      edx
0x497F7D: mov      ecx, dword ptr [ebp - 0xc]
0x497F80: mov      eax, dword ptr [ebp - 4]
0x497F83: mov      eax, dword ptr [eax + 0x140]
0x497F89: call     0x44da34
0x497F8E: test     eax, eax
0x497F90: je       0x497fc8
0x497F92: mov      ecx, dword ptr [ebp - 0xc]
0x497F95: dec      ecx
0x497F96: mov      edx, dword ptr [ebp - 8]
0x497F99: mov      eax, dword ptr [ebp - 4]
0x497F9C: mov      eax, dword ptr [eax + 0x140]
0x497FA2: call     0x44da34
0x497FA7: test     eax, eax
0x497FA9: je       0x497fc8
0x497FAB: mov      ecx, dword ptr [ebp - 0xc]
0x497FAE: inc      ecx
0x497FAF: mov      edx, dword ptr [ebp - 8]
0x497FB2: mov      eax, dword ptr [ebp - 4]
0x497FB5: mov      eax, dword ptr [eax + 0x140]
0x497FBB: call     0x44da34
0x497FC0: test     eax, eax
0x497FC2: je       0x497fc8
0x497FC4: mov      byte ptr [ebp - 0xd], 1
0x497FC8: mov      al, byte ptr [ebp - 0xd]
0x497FCB: mov      esp, ebp
0x497FCD: pop      ebp
0x497FCE: ret      
0x497FCF: nop      
0x497FD0: push     ebp
0x497FD1: mov      ebp, esp
0x497FD3: add      esp, -0x1c
0x497FD6: mov      dword ptr [ebp - 0xc], ecx
0x497FD9: mov      dword ptr [ebp - 8], edx
0x497FDC: mov      dword ptr [ebp - 4], eax
0x497FDF: mov      eax, dword ptr [ebp - 4]
0x497FE2: test     byte ptr [eax + 0x1e], 0x80
0x497FE6: je       0x497fec
0x497FE8: mov      byte ptr [ebp + 0xc], 1
0x497FEC: cmp      byte ptr [ebp + 0xc], 0
0x497FF0: jne      0x4980b1
0x497FF6: cmp      byte ptr [ebp + 8], 0
0x497FFA: jne      0x49802e
0x497FFC: cmp      dword ptr [ebp - 0xc], 0
0x498000: je       0x498150
0x498006: lea      edx, [ebp - 0x1c]
0x498009: mov      eax, dword ptr [ebp - 0xc]
0x49800C: call     0x44d8ec
0x498011: lea      eax, [ebp - 0x1c]
0x498014: push     eax
0x498015: mov      eax, dword ptr [ebp - 0xc]
0x498018: push     eax
0x498019: push     1
0x49801B: mov      ecx, dword ptr [ebp + 0x10]
0x49801E: mov      edx, dword ptr [ebp + 0x14]
0x498021: mov      eax, dword ptr [ebp - 8]
0x498024: call     0x44d6cc
0x498029: jmp      0x498150
0x49802E: cmp      dword ptr [ebp - 0xc], 0
0x498032: je       0x498150
0x498038: lea      edx, [ebp - 0x1c]
0x49803B: mov      eax, dword ptr [ebp - 0xc]
0x49803E: call     0x44d8ec
0x498043: lea      eax, [ebp - 0x1c]
0x498046: push     eax
0x498047: mov      eax, dword ptr [ebp - 0xc]
0x49804A: push     eax
0x49804B: push     0
0x49804D: mov      eax, dword ptr [0x4a41dc] ; *0x4A41DC->0x4F94D4
0x498052: mov      eax, dword ptr [eax]
0x498054: xor      ecx, ecx
0x498056: xor      edx, edx
0x498058: call     0x44d6cc
0x49805D: mov      eax, dword ptr [ebp - 0xc]
0x498060: call     0x44d944
0x498065: push     eax
0x498066: mov      eax, dword ptr [0x4a41dc] ; *0x4A41DC->0x4F94D4
0x49806B: mov      eax, dword ptr [eax]
0x49806D: push     eax
0x49806E: mov      al, byte ptr [ebp + 8]
0x498071: push     eax
0x498072: mov      eax, dword ptr [ebp - 0xc]
0x498075: call     0x44dc60
0x49807A: mov      ecx, eax
0x49807C: xor      edx, edx
0x49807E: xor      eax, eax
0x498080: call     0x455a4c
0x498085: lea      edx, [ebp - 0x1c]
0x498088: mov      eax, dword ptr [ebp - 0xc]
0x49808B: call     0x44d8ec
0x498090: lea      eax, [ebp - 0x1c]
0x498093: push     eax
0x498094: mov      eax, dword ptr [0x4a41dc] ; *0x4A41DC->0x4F94D4
0x498099: mov      eax, dword ptr [eax]
0x49809B: push     eax
0x49809C: push     1
0x49809E: mov      ecx, dword ptr [ebp + 0x10]
0x4980A1: mov      edx, dword ptr [ebp + 0x14]
0x4980A4: mov      eax, dword ptr [ebp - 8]
0x4980A7: call     0x44d6cc
0x4980AC: jmp      0x498150
0x4980B1: cmp      byte ptr [ebp + 8], 0
0x4980B5: jne      0x4980d7
0x4980B7: cmp      dword ptr [ebp - 0xc], 0
0x4980BB: je       0x498150
0x4980C1: mov      eax, dword ptr [ebp - 0xc]
0x4980C4: push     eax
0x4980C5: push     0
0x4980C7: mov      ecx, dword ptr [ebp + 0x10]
0x4980CA: mov      edx, dword ptr [ebp + 0x14]
0x4980CD: mov      eax, dword ptr [ebp - 8]
0x4980D0: call     0x455578
0x4980D5: jmp      0x498150
0x4980D7: cmp      dword ptr [ebp - 0xc], 0
0x4980DB: je       0x498150
0x4980DD: mov      eax, dword ptr [0x4a41dc] ; *0x4A41DC->0x4F94D4
0x4980E2: mov      eax, dword ptr [eax]
0x4980E4: xor      edx, edx
0x4980E6: call     0x44d430
0x4980EB: lea      edx, [ebp - 0x1c]
0x4980EE: mov      eax, dword ptr [ebp - 0xc]
0x4980F1: call     0x44d8ec
0x4980F6: lea      eax, [ebp - 0x1c]
0x4980F9: push     eax
0x4980FA: mov      eax, dword ptr [ebp - 0xc]
0x4980FD: push     eax
0x4980FE: push     0
0x498100: mov      eax, dword ptr [0x4a41dc] ; *0x4A41DC->0x4F94D4
0x498105: mov      eax, dword ptr [eax]
0x498107: xor      ecx, ecx
0x498109: xor      edx, edx
0x49810B: call     0x44d6cc
0x498110: mov      eax, dword ptr [ebp - 0xc]
0x498113: call     0x44d944
0x498118: push     eax
0x498119: mov      eax, dword ptr [0x4a41dc] ; *0x4A41DC->0x4F94D4
0x49811E: mov      eax, dword ptr [eax]
0x498120: push     eax
0x498121: mov      al, byte ptr [ebp + 8]
0x498124: push     eax
0x498125: mov      eax, dword ptr [ebp - 0xc]
0x498128: call     0x44dc60
0x49812D: mov      ecx, eax
0x49812F: xor      edx, edx
0x498131: xor      eax, eax
0x498133: call     0x455a4c
0x498138: mov      eax, dword ptr [0x4a41dc] ; *0x4A41DC->0x4F94D4
0x49813D: mov      eax, dword ptr [eax]
0x49813F: push     eax
0x498140: push     0
0x498142: mov      ecx, dword ptr [ebp + 0x10]
0x498145: mov      edx, dword ptr [ebp + 0x14]
0x498148: mov      eax, dword ptr [ebp - 8]
0x49814B: call     0x455578
0x498150: mov      esp, ebp
0x498152: pop      ebp
0x498153: ret      0x10
0x498156: mov      eax, eax
0x498158: push     ebp
0x498159: mov      ebp, esp
0x49815B: add      esp, -0x1c
0x49815E: mov      dword ptr [ebp - 0xc], ecx
0x498161: mov      dword ptr [ebp - 8], edx
0x498164: mov      dword ptr [ebp - 4], eax
0x498167: mov      esp, ebp
0x498169: pop      ebp
0x49816A: ret      4
0x49816D: lea      eax, [eax]
0x498170: push     ebp
0x498171: mov      ebp, esp
0x498173: add      esp, -8
0x498176: mov      dword ptr [ebp - 4], eax
0x498179: mov      byte ptr [ebp - 6], 0
0x49817D: mov      eax, dword ptr [0x4a3f10] ; *0x4A3F10->0x4F94A8
0x498182: mov      eax, dword ptr [eax]
0x498184: cmp      eax, dword ptr [ebp - 4]
0x498187: je       0x498195
0x498189: mov      eax, dword ptr [0x4a414c] ; *0x4A414C->0x4F94AC
0x49818E: mov      eax, dword ptr [eax]
0x498190: cmp      eax, dword ptr [ebp - 4]
0x498193: jne      0x498199
0x498195: mov      byte ptr [ebp - 6], 2
0x498199: mov      eax, dword ptr [ebp - 4]
0x49819C: test     byte ptr [eax + 0x1f], 0x80
0x4981A0: je       0x4981a6
0x4981A2: mov      byte ptr [ebp - 6], 6
0x4981A6: mov      eax, dword ptr [ebp - 4]
0x4981A9: test     byte ptr [eax + 0x1f], 0x40
0x4981AD: je       0x4981b3
0x4981AF: mov      byte ptr [ebp - 6], 5
0x4981B3: mov      eax, dword ptr [ebp - 4]
0x4981B6: test     byte ptr [eax + 0x1f], 0x20
0x4981BA: je       0x4981c0
0x4981BC: mov      byte ptr [ebp - 6], 7
0x4981C0: mov      eax, dword ptr [ebp - 4]
0x4981C3: test     byte ptr [eax + 0x1f], 0x10
0x4981C7: je       0x4981cd
0x4981C9: mov      byte ptr [ebp - 6], 8
0x4981CD: mov      eax, dword ptr [ebp - 4]
0x4981D0: test     byte ptr [eax + 0x1f], 8
0x4981D4: je       0x4981da
0x4981D6: mov      byte ptr [ebp - 6], 9
0x4981DA: mov      eax, dword ptr [ebp - 4]
0x4981DD: test     byte ptr [eax + 0x1f], 4
0x4981E1: je       0x4981e7
0x4981E3: mov      byte ptr [ebp - 6], 1
0x4981E7: mov      al, byte ptr [ebp - 6]
0x4981EA: mov      byte ptr [ebp - 5], al
0x4981ED: mov      al, byte ptr [ebp - 5]
0x4981F0: pop      ecx
0x4981F1: pop      ecx
0x4981F2: pop      ebp
0x4981F3: ret      

; === TActor.DMT_-14 @ 0x4981F4 ===
0x4981F4: push     ebp
0x4981F5: mov      ebp, esp
0x4981F7: add      esp, -0x44
0x4981FA: push     ebx
0x4981FB: mov      dword ptr [ebp - 0xc], ecx
0x4981FE: mov      dword ptr [ebp - 8], edx
0x498201: mov      dword ptr [ebp - 4], eax
0x498204: mov      eax, dword ptr [ebp - 4]
0x498207: mov      al, byte ptr [eax + 0xc]
0x49820A: sub      al, 8
0x49820C: jae      0x49835d
0x498212: call     0x406154
0x498217: mov      edx, dword ptr [ebp - 4]
0x49821A: sub      eax, dword ptr [edx + 0x204]
0x498220: cmp      eax, 0xea60
0x498225: jbe      0x498241
0x498227: call     0x406154
0x49822C: mov      edx, dword ptr [ebp - 4]
0x49822F: mov      dword ptr [edx + 0x204], eax
0x498235: mov      eax, dword ptr [ebp - 4]
0x498238: mov      bx, 0xfff3
0x49823C: call     0x403080
0x498241: mov      eax, dword ptr [ebp - 4]
0x498244: call     0x498170
0x498249: mov      byte ptr [ebp - 0x1d], al
0x49824C: mov      eax, dword ptr [ebp - 4]
0x49824F: cmp      dword ptr [eax + 0x140], 0
0x498256: je       0x4982a0
0x498258: mov      eax, dword ptr [ebp - 0xc]
0x49825B: mov      edx, dword ptr [ebp - 4]
0x49825E: add      eax, dword ptr [edx + 0xa0]
0x498264: mov      edx, dword ptr [ebp - 4]
0x498267: add      eax, dword ptr [edx + 0x98]
0x49826D: push     eax
0x49826E: mov      eax, dword ptr [ebp + 0xc]
0x498271: mov      edx, dword ptr [ebp - 4]
0x498274: add      eax, dword ptr [edx + 0xac]
0x49827A: mov      edx, dword ptr [ebp - 4]
0x49827D: add      eax, dword ptr [edx + 0x9c]
0x498283: push     eax
0x498284: mov      al, byte ptr [ebp + 8]
0x498287: push     eax
0x498288: mov      al, byte ptr [ebp - 0x1d]
0x49828B: push     eax
0x49828C: mov      eax, dword ptr [ebp - 4]
0x49828F: mov      ecx, dword ptr [eax + 0x140]
0x498295: mov      edx, dword ptr [ebp - 8]
0x498298: mov      eax, dword ptr [ebp - 4]
0x49829B: call     0x497fd0
0x4982A0: mov      eax, dword ptr [ebp - 4]
0x4982A3: cmp      byte ptr [eax + 0xec], 0
0x4982AA: je       0x49835d
0x4982B0: mov      eax, dword ptr [ebp - 4]
0x4982B3: cmp      dword ptr [eax + 0x11c], 0
0x4982BA: jle      0x49835d
0x4982C0: xor      eax, eax
0x4982C2: mov      edx, dword ptr [ebp - 4]
0x4982C5: mov      edx, dword ptr [edx + 0x108]
0x4982CB: dec      edx
0x4982CC: lea      ecx, [ebp - 0x44]
0x4982CF: mov      ah, 0x20
0x4982D1: call     0x402c94
0x4982D6: mov      eax, dword ptr [ebp - 4]
0x4982D9: mov      eax, dword ptr [eax + 0x104]
0x4982DF: cmp      eax, 0xff
0x4982E4: ja       0x4982ea
0x4982E6: bt       dword ptr [ebp - 0x44], eax
0x4982EA: jae      0x49835d
0x4982EC: lea      eax, [ebp - 0x10]
0x4982EF: push     eax
0x4982F0: lea      ecx, [ebp - 0x24]
0x4982F3: mov      eax, dword ptr [ebp - 4]
0x4982F6: mov      eax, dword ptr [eax + 0x11c]
0x4982FC: dec      eax
0x4982FD: xor      edx, edx
0x4982FF: call     0x49c5bc
0x498304: mov      eax, dword ptr [ebp - 4]
0x498307: mov      eax, dword ptr [eax + 0x104]
0x49830D: add      dword ptr [ebp - 0x10], eax
0x498310: cmp      dword ptr [ebp - 0x24], 0
0x498314: je       0x49832b
0x498316: lea      eax, [ebp - 0x18]
0x498319: push     eax
0x49831A: lea      ecx, [ebp - 0x14]
0x49831D: mov      edx, dword ptr [ebp - 0x10]
0x498320: mov      eax, dword ptr [ebp - 0x24]
0x498323: call     0x453a48
0x498328: mov      dword ptr [ebp - 0x1c], eax
0x49832B: cmp      dword ptr [ebp - 0x1c], 0
0x49832F: je       0x49835d
0x498331: mov      eax, dword ptr [ebp - 0x1c]
0x498334: push     eax
0x498335: push     1
0x498337: mov      ecx, dword ptr [ebp + 0xc]
0x49833A: add      ecx, dword ptr [ebp - 0x18]
0x49833D: mov      eax, dword ptr [ebp - 4]
0x498340: add      ecx, dword ptr [eax + 0x9c]
0x498346: mov      edx, dword ptr [ebp - 0xc]
0x498349: add      edx, dword ptr [ebp - 0x14]
0x49834C: mov      eax, dword ptr [ebp - 4]
0x49834F: add      edx, dword ptr [eax + 0x98]
0x498355: mov      eax, dword ptr [ebp - 8]
0x498358: call     0x455578
0x49835D: pop      ebx
0x49835E: mov      esp, ebp
0x498360: pop      ebp
0x498361: ret      8

; === TActor.DMT_-15 @ 0x498364 ===
0x498364: push     ebp
0x498365: mov      ebp, esp
0x498367: add      esp, -0xc
0x49836A: mov      dword ptr [ebp - 0xc], ecx
0x49836D: mov      dword ptr [ebp - 8], edx
0x498370: mov      dword ptr [ebp - 4], eax
0x498373: mov      esp, ebp
0x498375: pop      ebp
0x498376: ret      4
0x498379: lea      eax, [eax]
0x49837C: push     ebp
0x49837D: mov      ebp, esp
0x49837F: add      esp, -0x18
0x498382: mov      byte ptr [ebp - 0x11], dl
0x498385: mov      dword ptr [ebp - 4], eax
0x498388: mov      eax, dword ptr [ebp - 4]
0x49838B: movzx    edx, word ptr [eax + 0x14]
0x49838F: mov      eax, dword ptr [ebp - 4]
0x498392: movzx    eax, byte ptr [eax + 0xe]
0x498396: call     0x495ca8
0x49839B: mov      dword ptr [ebp - 0x10], eax
0x49839E: cmp      dword ptr [ebp - 0x10], 0
0x4983A2: je       0x498469
0x4983A8: mov      eax, dword ptr [ebp - 4]
0x4983AB: cmp      byte ptr [eax + 0x20], 0
0x4983AF: je       0x4983fa
0x4983B1: mov      eax, dword ptr [ebp - 4]
0x4983B4: cmp      byte ptr [eax + 0x21], 0
0x4983B8: je       0x4983c9
0x4983BA: mov      eax, dword ptr [ebp - 0x10]
0x4983BD: movzx    eax, word ptr [eax + 0x3c]
0x4983C1: mov      dword ptr [ebp - 8], eax
0x4983C4: jmp      0x498469
0x4983C9: mov      eax, dword ptr [ebp - 0x10]
0x4983CC: movzx    eax, word ptr [eax + 0x34]
0x4983D0: mov      edx, dword ptr [ebp - 0x10]
0x4983D3: movzx    edx, word ptr [edx + 0x36]
0x4983D7: add      eax, edx
0x4983D9: mov      edx, dword ptr [ebp - 4]
0x4983DC: movzx    edx, byte ptr [edx + 0xc]
0x4983E0: imul     edx
0x4983E2: mov      edx, dword ptr [ebp - 0x10]
0x4983E5: movzx    edx, word ptr [edx + 0x32]
0x4983E9: add      eax, edx
0x4983EB: mov      edx, dword ptr [ebp - 0x10]
0x4983EE: movzx    edx, word ptr [edx + 0x34]
0x4983F2: dec      edx
0x4983F3: add      eax, edx
0x4983F5: mov      dword ptr [ebp - 8], eax
0x4983F8: jmp      0x498469
0x4983FA: mov      eax, dword ptr [ebp - 0x10]
0x4983FD: movzx    eax, word ptr [eax + 2]
0x498401: mov      edx, dword ptr [ebp - 4]
0x498404: mov      dword ptr [edx + 0x1f4], eax
0x49840A: mov      eax, dword ptr [ebp - 4]
0x49840D: cmp      dword ptr [eax + 0x1ec], 0
0x498414: jge      0x49841d
0x498416: xor      eax, eax
0x498418: mov      dword ptr [ebp - 0xc], eax
0x49841B: jmp      0x498442
0x49841D: mov      eax, dword ptr [ebp - 0x10]
0x498420: movzx    eax, word ptr [eax + 2]
0x498424: mov      edx, dword ptr [ebp - 4]
0x498427: cmp      eax, dword ptr [edx + 0x1ec]
0x49842D: jg       0x498436
0x49842F: xor      eax, eax
0x498431: mov      dword ptr [ebp - 0xc], eax
0x498434: jmp      0x498442
0x498436: mov      eax, dword ptr [ebp - 4]
0x498439: mov      eax, dword ptr [eax + 0x1ec]
0x49843F: mov      dword ptr [ebp - 0xc], eax
0x498442: mov      eax, dword ptr [ebp - 0x10]
0x498445: movzx    eax, word ptr [eax + 2]
0x498449: mov      edx, dword ptr [ebp - 0x10]
0x49844C: movzx    edx, word ptr [edx + 4]
0x498450: add      eax, edx
0x498452: mov      edx, dword ptr [ebp - 4]
0x498455: movzx    edx, byte ptr [edx + 0xc]
0x498459: imul     edx
0x49845B: mov      edx, dword ptr [ebp - 0x10]
0x49845E: movzx    edx, word ptr [edx]
0x498461: add      eax, edx
0x498463: add      eax, dword ptr [ebp - 0xc]
0x498466: mov      dword ptr [ebp - 8], eax
0x498469: mov      eax, dword ptr [ebp - 8]
0x49846C: mov      esp, ebp
0x49846E: pop      ebp
0x49846F: ret      

; === TActor.DMT_-2 @ 0x498470 ===
0x498470: push     ebp
0x498471: mov      ebp, esp
0x498473: push     ecx
0x498474: push     ebx
0x498475: mov      dword ptr [ebp - 4], eax
0x498478: mov      eax, dword ptr [ebp - 4]
0x49847B: mov      byte ptr [eax + 0x14c], 0
0x498482: mov      eax, dword ptr [ebp - 4]
0x498485: cmp      byte ptr [eax + 0x14d], 0
0x49848C: je       0x4984ad
0x49848E: call     0x406154
0x498493: mov      edx, dword ptr [ebp - 4]
0x498496: sub      eax, dword ptr [edx + 0x150]
0x49849C: cmp      eax, 0xfa0
0x4984A1: jbe      0x4984ad
0x4984A3: mov      eax, dword ptr [ebp - 4]
0x4984A6: mov      byte ptr [eax + 0x14d], 0
0x4984AD: mov      eax, dword ptr [ebp - 4]
0x4984B0: mov      dl, byte ptr [eax + 0x14d]
0x4984B6: mov      eax, dword ptr [ebp - 4]
0x4984B9: mov      bx, 0xfffd
0x4984BD: call     0x403080
0x4984C2: mov      edx, dword ptr [ebp - 4]
0x4984C5: mov      dword ptr [edx + 0x1b8], eax
0x4984CB: push     1
0x4984CD: push     1
0x4984CF: mov      eax, dword ptr [ebp - 4]
0x4984D2: xor      edx, edx
0x4984D4: mov      dl, byte ptr [eax + 0xc]
0x4984D7: xor      ecx, ecx
0x4984D9: mov      eax, dword ptr [ebp - 4]
0x4984DC: call     0x497610
0x4984E1: pop      ebx
0x4984E2: pop      ecx
0x4984E3: pop      ebp
0x4984E4: ret      
0x4984E5: lea      eax, [eax]
0x4984E8: push     ebp
0x4984E9: mov      ebp, esp
0x4984EB: add      esp, -0x1c
0x4984EE: mov      dword ptr [ebp - 4], eax
0x4984F1: mov      eax, dword ptr [ebp - 4]
0x4984F4: cmp      byte ptr [eax + 0xe], 0
0x4984F8: jne      0x498e6a
0x4984FE: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x498503: mov      eax, dword ptr [eax]
0x498505: cmp      eax, dword ptr [ebp - 4]
0x498508: jne      0x498afb
0x49850E: mov      eax, dword ptr [ebp - 4]
0x498511: cmp      dword ptr [eax + 0x148], 0xb
0x498518: je       0x49854e
0x49851A: mov      eax, dword ptr [ebp - 4]
0x49851D: cmp      dword ptr [eax + 0x148], 9
0x498524: je       0x49854e
0x498526: mov      eax, dword ptr [ebp - 4]
0x498529: cmp      dword ptr [eax + 0x148], 0xd
0x498530: je       0x49854e
0x498532: mov      eax, dword ptr [ebp - 4]
0x498535: cmp      dword ptr [eax + 0x148], 6
0x49853C: je       0x49854e
0x49853E: mov      eax, dword ptr [ebp - 4]
0x498541: cmp      dword ptr [eax + 0x148], 7
0x498548: jne      0x498afb
0x49854E: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x498553: mov      eax, dword ptr [eax]
0x498555: movzx    eax, word ptr [eax + 8]
0x498559: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x49855F: mov      edx, dword ptr [edx]
0x498561: sub      eax, dword ptr [edx + 0x2ae74]
0x498567: mov      dword ptr [ebp - 8], eax
0x49856A: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49856F: mov      eax, dword ptr [eax]
0x498571: movzx    eax, word ptr [eax + 0xa]
0x498575: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x49857B: mov      edx, dword ptr [edx]
0x49857D: sub      eax, dword ptr [edx + 0x2ae78]
0x498583: mov      dword ptr [ebp - 0xc], eax
0x498586: mov      eax, dword ptr [ebp - 8]
0x498589: sar      eax, 1
0x49858B: jns      0x498590
0x49858D: adc      eax, 0
0x498590: add      eax, eax
0x498592: mov      dword ptr [ebp - 8], eax
0x498595: mov      eax, dword ptr [ebp - 0xc]
0x498598: sar      eax, 1
0x49859A: jns      0x49859f
0x49859C: adc      eax, 0
0x49859F: add      eax, eax
0x4985A1: mov      dword ptr [ebp - 0xc], eax
0x4985A4: imul     eax, dword ptr [ebp - 8], 0x16b
0x4985AB: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x4985B1: mov      edx, dword ptr [edx]
0x4985B3: lea      eax, [edx + eax*4]
0x4985B6: mov      edx, dword ptr [ebp - 0xc]
0x4985B9: lea      edx, [edx + edx*2]
0x4985BC: mov      ax, word ptr [eax + edx*4 + 8]
0x4985C1: and      ax, 0x7fff
0x4985C5: movzx    eax, ax
0x4985C8: mov      dword ptr [ebp - 0x10], eax
0x4985CB: imul     eax, dword ptr [ebp - 8], 0x16b
0x4985D2: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x4985D8: mov      edx, dword ptr [edx]
0x4985DA: lea      eax, [edx + eax*4]
0x4985DD: mov      edx, dword ptr [ebp - 0xc]
0x4985E0: lea      edx, [edx + edx*2]
0x4985E3: movzx    eax, byte ptr [eax + edx*4 + 0x12]
0x4985E8: mov      dword ptr [ebp - 0x14], eax
0x4985EB: imul     eax, dword ptr [ebp - 0x14], 0x2710
0x4985F2: add      eax, dword ptr [ebp - 0x10]
0x4985F5: dec      eax
0x4985F6: mov      dword ptr [ebp - 0x10], eax
0x4985F9: mov      eax, dword ptr [ebp - 0x10]
0x4985FC: cmp      eax, 0x672
0x498601: jge      0x498734
0x498607: cmp      eax, 0x3ed
0x49860C: jge      0x4986a3
0x498612: cmp      eax, 0x25d
0x498617: jge      0x498650
0x498619: add      eax, 0xffffff06
0x49861E: sub      eax, 5
0x498621: jb       0x498873
0x498627: add      eax, -0x4b
0x49862A: sub      eax, 0x14
0x49862D: jb       0x498861
0x498633: add      eax, -0x64
0x498636: sub      eax, 5
0x498639: jb       0x498861
0x49863F: add      eax, -0x5f
0x498642: sub      eax, 5
0x498645: jb       0x498861
0x49864B: jmp      0x498914
0x498650: cmp      eax, 0x294
0x498655: jge      0x498676
0x498657: add      eax, 0xfffffda3
0x49865C: sub      eax, 5
0x49865F: jb       0x498885
0x498665: add      eax, -0x28
0x498668: sub      eax, 5
0x49866B: jb       0x498885
0x498671: jmp      0x498914
0x498676: add      eax, 0xfffffd6c
0x49867B: sub      eax, 5
0x49867E: jb       0x498885
0x498684: add      eax, -0x55
0x498687: sub      eax, 5
0x49868A: jb       0x498861
0x498690: add      eax, 0xffffff3d
0x498695: sub      eax, 5
0x498698: jb       0x498861
0x49869E: jmp      0x498914
0x4986A3: cmp      eax, 0x578
0x4986A8: jge      0x4986e3
0x4986AA: add      eax, 0xfffffc13
0x4986AF: sub      eax, 5
0x4986B2: jb       0x498873
0x4986B8: add      eax, -0x28
0x4986BB: sub      eax, 5
0x4986BE: jb       0x498873
0x4986C4: add      eax, -5
0x4986C7: sub      eax, 5
0x4986CA: jb       0x498873
0x4986D0: add      eax, 0xffffff47
0x4986D5: sub      eax, 5
0x4986D8: jb       0x498861
0x4986DE: jmp      0x498914
0x4986E3: cmp      eax, 0x5af
0x4986E8: jge      0x498709
0x4986EA: add      eax, 0xfffffa88
0x4986EF: sub      eax, 0x19
0x4986F2: jb       0x498861
0x4986F8: add      eax, -0x19
0x4986FB: sub      eax, 5
0x4986FE: jb       0x498873
0x498704: jmp      0x498914
0x498709: add      eax, 0xfffffa51
0x49870E: sub      eax, 0x14
0x498711: jb       0x498861
0x498717: add      eax, -0x19
0x49871A: sub      eax, 0x19
0x49871D: jb       0x498861
0x498723: add      eax, -0x19
0x498726: sub      eax, 0x19
0x498729: jb       0x498861
0x49872F: jmp      0x498914
0x498734: cmp      eax, 0xc03
0x498739: jge      0x4987d1
0x49873F: cmp      eax, 0x81b
0x498744: jge      0x49877c
0x498746: add      eax, 0xfffff98e
0x49874B: sub      eax, 5
0x49874E: jb       0x498873
0x498754: add      eax, 0xffffff6f
0x498759: sub      eax, 0x19
0x49875C: jb       0x498905
0x498762: sub      eax, 0x64
0x498765: jb       0x498897
0x49876B: add      eax, -0x4b
0x49876E: sub      eax, 0x32
0x498771: jb       0x498885
0x498777: jmp      0x498914
0x49877C: cmp      eax, 0x866
0x498781: jge      0x4987a2
0x498783: add      eax, 0xfffff7e5
0x498788: sub      eax, 0x19
0x49878B: jb       0x4988f6
0x498791: add      eax, -0x19
0x498794: sub      eax, 0x19
0x498797: jb       0x4988f6
0x49879D: jmp      0x498914
0x4987A2: add      eax, 0xfffff79a
0x4987A7: sub      eax, 0x19
0x4987AA: jb       0x498897
0x4987B0: add      eax, 0xffffff1f
0x4987B5: sub      eax, 0x19
0x4987B8: jb       0x498885
0x4987BE: add      eax, 0xfffffda8
0x4987C3: sub      eax, 0x19
0x4987C6: jb       0x498885
0x4987CC: jmp      0x498914
0x4987D1: cmp      eax, 0xd2f
0x4987D6: jge      0x49881e
0x4987D8: cmp      eax, 0xcae
0x4987DD: jg       0x498806
0x4987DF: je       0x4988a6
0x4987E5: add      eax, 0xfffff3fd
0x4987EA: sub      eax, 0x19
0x4987ED: jb       0x498897
0x4987F3: add      eax, 0xffffff7e
0x4987F8: sub      eax, 2
0x4987FB: jb       0x4988a6
0x498801: jmp      0x498914
0x498806: sub      eax, 0xccd
0x49880B: je       0x4988a6
0x498811: add      eax, -0x30
0x498814: sub      eax, 0x19
0x498817: jb       0x498897
0x498819: jmp      0x498914
0x49881E: cmp      eax, 0xef1
0x498823: jge      0x49883e
0x498825: add      eax, 0xfffff2d1
0x49882A: sub      eax, 0x19
0x49882D: jb       0x498897
0x49882F: add      eax, 0xfffffe84
0x498834: sub      eax, 0x14
0x498837: jb       0x4988b5
0x498839: jmp      0x498914
0x49883E: add      eax, 0xfffff10f
0x498843: sub      eax, 0x262
0x498848: jb       0x4988c4
0x49884A: add      eax, 0xffffff42
0x49884F: sub      eax, 0x19
0x498852: jb       0x498885
0x498854: add      eax, -0x19
0x498857: sub      eax, 4
0x49885A: jb       0x498885
0x49885C: jmp      0x498914
0x498861: mov      eax, dword ptr [ebp - 4]
0x498864: mov      dword ptr [eax + 0x17c], 9
0x49886E: jmp      0x498921
0x498873: mov      eax, dword ptr [ebp - 4]
0x498876: mov      dword ptr [eax + 0x17c], 0xd
0x498880: jmp      0x498921
0x498885: mov      eax, dword ptr [ebp - 4]
0x498888: mov      dword ptr [eax + 0x17c], 5
0x498892: jmp      0x498921
0x498897: mov      eax, dword ptr [ebp - 4]
0x49889A: mov      dword ptr [eax + 0x17c], 0x15
0x4988A4: jmp      0x498921
0x4988A6: mov      eax, dword ptr [ebp - 4]
0x4988A9: mov      dword ptr [eax + 0x17c], 0x11
0x4988B3: jmp      0x498921
0x4988B5: mov      eax, dword ptr [ebp - 4]
0x4988B8: mov      dword ptr [eax + 0x17c], 0x11
0x4988C2: jmp      0x498921
0x4988C4: mov      eax, dword ptr [ebp - 0x10]
0x4988C7: sub      eax, 0xef1
0x4988CC: mov      ecx, 0x19
0x4988D1: cdq      
0x4988D2: idiv     ecx
0x4988D4: test     edx, edx
0x4988D6: jne      0x4988e7
0x4988D8: mov      eax, dword ptr [ebp - 4]
0x4988DB: mov      dword ptr [eax + 0x17c], 0x11
0x4988E5: jmp      0x498921
0x4988E7: mov      eax, dword ptr [ebp - 4]
0x4988EA: mov      dword ptr [eax + 0x17c], 1
0x4988F4: jmp      0x498921
0x4988F6: mov      eax, dword ptr [ebp - 4]
0x4988F9: mov      dword ptr [eax + 0x17c], 0x19
0x498903: jmp      0x498921
0x498905: mov      eax, dword ptr [ebp - 4]
0x498908: mov      dword ptr [eax + 0x17c], 0x1d
0x498912: jmp      0x498921
0x498914: mov      eax, dword ptr [ebp - 4]
0x498917: mov      dword ptr [eax + 0x17c], 1
0x498921: cmp      dword ptr [ebp - 0x10], 0x339
0x498928: jl       0x498960
0x49892A: cmp      dword ptr [ebp - 0x10], 0x545
0x498931: jg       0x498960
0x498933: mov      eax, dword ptr [ebp - 0x10]
0x498936: sub      eax, 0x339
0x49893B: mov      ecx, 0x19
0x498940: cdq      
0x498941: idiv     ecx
0x498943: and      eax, 0x80000001
0x498948: jns      0x49894f
0x49894A: dec      eax
0x49894B: or       eax, 0xfffffffe
0x49894E: inc      eax
0x49894F: test     eax, eax
0x498951: jne      0x498960
0x498953: mov      eax, dword ptr [ebp - 4]
0x498956: mov      dword ptr [eax + 0x17c], 5
0x498960: cmp      dword ptr [ebp - 0x10], 0x55f
0x498967: jl       0x49899f
0x498969: cmp      dword ptr [ebp - 0x10], 0x707
0x498970: jg       0x49899f
0x498972: mov      eax, dword ptr [ebp - 0x10]
0x498975: sub      eax, 0x55f
0x49897A: mov      ecx, 0x19
0x49897F: cdq      
0x498980: idiv     ecx
0x498982: and      eax, 0x80000001
0x498987: jns      0x49898e
0x498989: dec      eax
0x49898A: or       eax, 0xfffffffe
0x49898D: inc      eax
0x49898E: test     eax, eax
0x498990: jne      0x49899f
0x498992: mov      eax, dword ptr [ebp - 4]
0x498995: mov      dword ptr [eax + 0x17c], 0x15
0x49899F: mov      eax, dword ptr [ebp - 0x10]
0x4989A2: add      eax, 0xfffffa97
0x4989A7: sub      eax, 2
0x4989AA: jb       0x4989b4
0x4989AC: add      eax, -4
0x4989AF: sub      eax, 2
0x4989B2: jae      0x4989c1
0x4989B4: mov      eax, dword ptr [ebp - 4]
0x4989B7: mov      dword ptr [eax + 0x17c], 0x11
0x4989C1: imul     eax, dword ptr [ebp - 8], 0x16b
0x4989C8: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x4989CE: mov      edx, dword ptr [edx]
0x4989D0: lea      eax, [edx + eax*4]
0x4989D3: mov      edx, dword ptr [ebp - 0xc]
0x4989D6: lea      edx, [edx + edx*2]
0x4989D9: mov      ax, word ptr [eax + edx*4 + 0xa]
0x4989DE: and      ax, 0x7fff
0x4989E2: movzx    eax, ax
0x4989E5: mov      dword ptr [ebp - 0x10], eax
0x4989E8: dec      dword ptr [ebp - 0x10]
0x4989EB: mov      eax, dword ptr [ebp - 0x10]
0x4989EE: sub      eax, 0x74
0x4989F1: jb       0x4989fd
0x4989F3: add      eax, -4
0x4989F6: sub      eax, 5
0x4989F9: jb       0x498a0c
0x4989FB: jmp      0x498a19
0x4989FD: mov      eax, dword ptr [ebp - 4]
0x498A00: mov      dword ptr [eax + 0x17c], 1
0x498A0A: jmp      0x498a19
0x498A0C: mov      eax, dword ptr [ebp - 4]
0x498A0F: mov      dword ptr [eax + 0x17c], 9
0x498A19: imul     eax, dword ptr [ebp - 8], 0x16b
0x498A20: mov      edx, dword ptr [0x4a42dc] ; *0x4A42DC->0x4F7EF4
0x498A26: mov      edx, dword ptr [edx]
0x498A28: lea      eax, [edx + eax*4]
0x498A2B: mov      edx, dword ptr [ebp - 0xc]
0x498A2E: lea      edx, [edx + edx*2]
0x498A31: mov      ax, word ptr [eax + edx*4 + 0xc]
0x498A36: and      ax, 0x7fff
0x498A3A: movzx    eax, ax
0x498A3D: mov      dword ptr [ebp - 0x10], eax
0x498A40: dec      dword ptr [ebp - 0x10]
0x498A43: mov      eax, dword ptr [ebp - 0x10]
0x498A46: cmp      eax, 0xcf4
0x498A4B: jge      0x498a79
0x498A4D: add      eax, 0xffffff23
0x498A52: sub      eax, 0x45
0x498A55: jb       0x498aba
0x498A57: add      eax, 0xfffffedb
0x498A5C: sub      eax, 0x4c
0x498A5F: jb       0x498aba
0x498A61: add      eax, 0xfffffdf4
0x498A66: sub      eax, 0x18
0x498A69: jb       0x498aba
0x498A6B: add      eax, 0xfffff882
0x498A70: sub      eax, 0x8f
0x498A75: jb       0x498ac9
0x498A77: jmp      0x498ae5
0x498A79: cmp      eax, 0x178e
0x498A7E: jge      0x498a9a
0x498A80: add      eax, 0xfffff30c
0x498A85: sub      eax, 0x112
0x498A8A: jb       0x498ad8
0x498A8C: add      eax, 0xffffff59
0x498A91: sub      eax, 0xc0
0x498A96: jb       0x498ac9
0x498A98: jmp      0x498ae5
0x498A9A: add      eax, 0xffffe872
0x498A9F: sub      eax, 0x3ca
0x498AA4: jb       0x498ac9
0x498AA6: add      eax, 0xffffff5d
0x498AAB: sub      eax, 0x85
0x498AB0: jb       0x498aba
0x498AB2: add      eax, -0x6c
0x498AB5: sub      eax, 0xb
0x498AB8: jae      0x498ae5
0x498ABA: mov      eax, dword ptr [ebp - 4]
0x498ABD: mov      dword ptr [eax + 0x17c], 5
0x498AC7: jmp      0x498ae5
0x498AC9: mov      eax, dword ptr [ebp - 4]
0x498ACC: mov      dword ptr [eax + 0x17c], 0x11
0x498AD6: jmp      0x498ae5
0x498AD8: mov      eax, dword ptr [ebp - 4]
0x498ADB: mov      dword ptr [eax + 0x17c], 0x19
0x498AE5: mov      eax, dword ptr [ebp - 4]
0x498AE8: cmp      dword ptr [eax + 0x148], 0xd
0x498AEF: jne      0x498afb
0x498AF1: mov      eax, dword ptr [ebp - 4]
0x498AF4: add      dword ptr [eax + 0x17c], 2
0x498AFB: mov      eax, dword ptr [ebp - 4]
0x498AFE: cmp      byte ptr [eax + 0xd], 0
0x498B02: jne      0x498b20
0x498B04: mov      eax, dword ptr [ebp - 4]
0x498B07: mov      dword ptr [eax + 0x198], 0x8a
0x498B11: mov      eax, dword ptr [ebp - 4]
0x498B14: mov      dword ptr [eax + 0x19c], 0x90
0x498B1E: jmp      0x498b3a
0x498B20: mov      eax, dword ptr [ebp - 4]
0x498B23: mov      dword ptr [eax + 0x198], 0x8b
0x498B2D: mov      eax, dword ptr [ebp - 4]
0x498B30: mov      dword ptr [eax + 0x19c], 0x91
0x498B3A: mov      eax, dword ptr [ebp - 4]
0x498B3D: mov      eax, dword ptr [eax + 0x148]
0x498B43: add      eax, -5
0x498B46: cmp      eax, 0x1a
0x498B49: ja       0x498ded
0x498B4F: mov      al, byte ptr [eax + 0x498b5c]
0x498B55: jmp      dword ptr [eax*4 + 0x498b77]
0x498B5C: add      dword ptr [eax], eax
0x498B5E: add      byte ptr [ecx], al
0x498B60: add      byte ptr [eax], al
0x498B62: add      byte ptr [eax], al
0x498B64: add      byte ptr [ecx], al
0x498B66: add      dword ptr [ecx], eax
0x498B68: add      byte ptr [ecx], al
0x498B6A: add      dword ptr [eax], eax
0x498B6C: add      byte ptr [eax], al
0x498B6E: add      byte ptr [ecx], al
0x498B70: add      byte ptr [eax], al
0x498B72: add      byte ptr [eax], al
0x498B74: add      byte ptr [eax], al
0x498B76: add      ch, ch
0x498B78: lea      ecx, [ecx]
0x498B7B: or       dword ptr [ebx - 0x738fffb7], 0x49
0x498B82: add      byte ptr [ebx - 0x49f003bb], cl
0x498B88: inc      eax
0x498B89: adc      ecx, edx
0x498B8B: call     0xf678413
0x498B90: xchg     ecx, ecx
0x498B92: add      byte ptr [eax], al
0x498B94: add      byte ptr [edx + 0x498ba280], cl
0x498B9A: add      bh, bh
0x498B9C: and      al, 0x85
0x498B9E: ror      byte ptr [ebx + 0x2000049], 3
0x498BA5: add      eax, 0x5010304
0x498BAA: pop      es
0x498BAB: add      eax, dword ptr [eax + 0x4030307]
0x498BB2: add      al, 4
0x498BB4: pop      es
0x498BB5: add      byte ptr [ecx], al
0x498BB7: pop      es
0x498BB8: add      eax, dword ptr [esi + eax]
0x498BBB: add      eax, dword ptr [edx + eax]
0x498BBE: add      al, byte ptr [esi + ebx*2]
0x498BC1: mov      word ptr [ecx], cs
0x498BC4: loopne   0x498b51
0x498BC6: dec      ecx
0x498BC7: add      dl, dh
0x498BC9: mov      ecx, dword ptr [ecx]
0x498BCC: add      al, 0x8c
0x498BCE: dec      ecx
0x498BCF: add      byte ptr [esi], dl
0x498BD1: mov      word ptr [ecx], cs
0x498BD4: sub      byte ptr [ecx + ecx*2 + 0x498c3a00], cl
0x498BDB: add      byte ptr [esp + ecx*4 + 0x49], cl
0x498BDF: add      byte ptr [ebx - 0x7f3803bb], cl
0x498BE5: xchg     esp, eax
0x498BE6: add      dword ptr [eax], eax
0x498BE8: add      byte ptr [edx], dh
0x498BEA: add      byte ptr [eax], al
0x498BEC: add      cl, ch
0x498BEE: sti      
0x498BEF: add      dword ptr [eax], eax
0x498BF1: add      byte ptr [ebx - 0x7f3803bb], cl
0x498BF7: xchg     esp, eax
0x498BF8: add      dword ptr [eax], eax
0x498BFA: add      byte ptr [ebx], dh
0x498BFC: add      byte ptr [eax], al
0x498BFE: add      cl, ch
0x498C00: jmp      0x8b498c06
0x498C05: inc      ebp
0x498C06: cld      
0x498C07: mov      dword ptr [eax + 0x194], 0x34
0x498C11: jmp      0x498ded
0x498C16: mov      eax, dword ptr [ebp - 4]
0x498C19: mov      dword ptr [eax + 0x194], 0x35
0x498C23: jmp      0x498ded
0x498C28: mov      eax, dword ptr [ebp - 4]
0x498C2B: mov      dword ptr [eax + 0x194], 0x36
0x498C35: jmp      0x498ded
0x498C3A: mov      eax, dword ptr [ebp - 4]
0x498C3D: mov      dword ptr [eax + 0x194], 0x37
0x498C47: jmp      0x498ded
0x498C4C: mov      eax, dword ptr [ebp - 4]
0x498C4F: mov      dword ptr [eax + 0x194], 0x38
0x498C59: jmp      0x498ded
0x498C5E: mov      eax, dword ptr [ebp - 4]
0x498C61: mov      dword ptr [eax + 0x194], 0x39
0x498C6B: jmp      0x498ded
0x498C70: mov      eax, dword ptr [ebp - 4]
0x498C73: cmp      dword ptr [eax + 0x174], 1
0x498C7A: jge      0x498ded
0x498C80: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x498C85: mov      eax, dword ptr [eax]
0x498C87: mov      edx, dword ptr [ebp - 4]
0x498C8A: mov      edx, dword ptr [edx + 0xd0]
0x498C90: call     0x472db0
0x498C95: mov      dword ptr [ebp - 0x1c], eax
0x498C98: xor      eax, eax
0x498C9A: mov      dword ptr [ebp - 0x18], eax
0x498C9D: cmp      dword ptr [ebp - 0x1c], 0
0x498CA1: je       0x498ded
0x498CA7: mov      eax, dword ptr [ebp - 0x1c]
0x498CAA: movzx    eax, byte ptr [eax + 0x11]
0x498CAE: shr      eax, 1
0x498CB0: mov      dword ptr [ebp - 0x18], eax
0x498CB3: mov      eax, dword ptr [ebp - 0x1c]
0x498CB6: cmp      byte ptr [eax + 0xe], 0
0x498CBA: jne      0x498ded
0x498CC0: mov      eax, dword ptr [ebp - 4]
0x498CC3: movzx    eax, byte ptr [eax + 0x10]
0x498CC7: shr      eax, 1
0x498CC9: sub      eax, 3
0x498CCC: jne      0x498d68
0x498CD2: mov      eax, dword ptr [ebp - 0x18]
0x498CD5: cmp      eax, 0x12
0x498CD8: ja       0x498d56
0x498CDA: mov      al, byte ptr [eax + 0x498ce7]
0x498CE0: jmp      dword ptr [eax*4 + 0x498cfa]
0x498CE7: add      byte ptr [edx], al
0x498CE9: add      al, byte ptr [ebx]
0x498CEB: add      al, byte ptr [edx]
0x498CED: add      dword ptr [ebx], eax
0x498CEF: add      al, 2
0x498CF1: add      al, byte ptr [ebx]
0x498CF3: add      al, 2
0x498CF5: add      al, byte ptr [edx]
0x498CF7: add      al, byte ptr [edx]
0x498CF9: add      al, 0x56
0x498CFB: lea      ecx, [ecx]
0x498CFE: push     cs
0x498CFF: lea      ecx, [ecx]
0x498D02: and      byte ptr [ebp - 0x72cdffb7], cl
0x498D08: dec      ecx
0x498D09: add      byte ptr [ebp + ecx*4 + 0x49], al
0x498D0D: add      byte ptr [ebx - 0x7f3803bb], cl
0x498D13: add      byte ptr [ecx], 0
0x498D16: add      byte ptr [eax], dl
0x498D19: add      byte ptr [eax], al
0x498D1B: jmp      0x498ded
0x498D20: mov      eax, dword ptr [ebp - 4]
0x498D23: mov      dword ptr [eax + 0x180], 0x50
0x498D2D: jmp      0x498ded
0x498D32: mov      eax, dword ptr [ebp - 4]
0x498D35: mov      dword ptr [eax + 0x180], 0x51
0x498D3F: jmp      0x498ded
0x498D44: mov      eax, dword ptr [ebp - 4]
0x498D47: mov      dword ptr [eax + 0x180], 0x52
0x498D51: jmp      0x498ded
0x498D56: mov      eax, dword ptr [ebp - 4]
0x498D59: mov      dword ptr [eax + 0x180], 0x53
0x498D63: jmp      0x498ded
0x498D68: mov      eax, dword ptr [ebp - 0x18]
0x498D6B: cmp      eax, 0x12
0x498D6E: ja       0x498de0
0x498D70: mov      al, byte ptr [eax + 0x498d7d]
0x498D76: jmp      dword ptr [eax*4 + 0x498d90]
0x498D7D: add      byte ptr [edx], al
0x498D7F: add      al, byte ptr [ebx]
0x498D81: add      al, byte ptr [edx]
0x498D83: add      dword ptr [ebx], eax
0x498D85: add      al, 2
0x498D87: add      al, byte ptr [ebx]
0x498D89: add      al, 2
0x498D8B: add      al, byte ptr [edx]
0x498D8D: add      al, byte ptr [edx]
0x498D8F: add      al, 0xe0
0x498D91: lea      ecx, [ecx]
0x498D94: movsb    byte ptr es:[edi], byte ptr [esi]
0x498D95: lea      ecx, [ecx]
0x498D98: mov      bl, 0x8d
0x498D9A: dec      ecx
0x498D9B: add      dl, al
0x498D9D: lea      ecx, [ecx]
0x498DA0: ror      dword ptr [ebp + 0x458b0049], 1
0x498DA6: cld      
0x498DA7: mov      dword ptr [eax + 0x180], 0x46
0x498DB1: jmp      0x498ded
0x498DB3: mov      eax, dword ptr [ebp - 4]
0x498DB6: mov      dword ptr [eax + 0x180], 0x46
0x498DC0: jmp      0x498ded
0x498DC2: mov      eax, dword ptr [ebp - 4]
0x498DC5: mov      dword ptr [eax + 0x180], 0x47
0x498DCF: jmp      0x498ded
0x498DD1: mov      eax, dword ptr [ebp - 4]
0x498DD4: mov      dword ptr [eax + 0x180], 0x48
0x498DDE: jmp      0x498ded
0x498DE0: mov      eax, dword ptr [ebp - 4]
0x498DE3: mov      dword ptr [eax + 0x180], 0x49
0x498DED: mov      eax, dword ptr [ebp - 4]
0x498DF0: cmp      byte ptr [eax + 0xec], 0
0x498DF7: je       0x499010
0x498DFD: mov      eax, dword ptr [ebp - 4]
0x498E00: cmp      dword ptr [eax + 0x110], 0
0x498E07: jle      0x499010
0x498E0D: mov      eax, dword ptr [ebp - 4]
0x498E10: mov      eax, dword ptr [eax + 0x110]
0x498E16: add      eax, eax
0x498E18: lea      eax, [eax + eax*4]
0x498E1B: add      eax, 0x2710
0x498E20: mov      edx, dword ptr [ebp - 4]
0x498E23: mov      dword ptr [edx + 0x1a4], eax
0x498E29: mov      eax, dword ptr [ebp - 4]
0x498E2C: mov      eax, dword ptr [eax + 0x110]
0x498E32: add      eax, eax
0x498E34: lea      eax, [eax + eax*4]
0x498E37: add      eax, 0x2710
0x498E3C: inc      eax
0x498E3D: mov      edx, dword ptr [ebp - 4]
0x498E40: mov      dword ptr [edx + 0x1a8], eax
0x498E46: mov      eax, dword ptr [ebp - 4]
0x498E49: mov      eax, dword ptr [eax + 0x110]
0x498E4F: add      eax, eax
0x498E51: lea      eax, [eax + eax*4]
0x498E54: add      eax, 0x2710
0x498E59: add      eax, 2
0x498E5C: mov      edx, dword ptr [ebp - 4]
0x498E5F: mov      dword ptr [edx + 0x1ac], eax
0x498E65: jmp      0x499010
0x498E6A: mov      eax, dword ptr [ebp - 4]
0x498E6D: cmp      dword ptr [eax + 0x148], 0x1f
0x498E74: jne      0x498f3d
0x498E7A: mov      eax, dword ptr [ebp - 4]
0x498E7D: cmp      dword ptr [eax + 0x174], 1
0x498E84: jge      0x498f3d
0x498E8A: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x498E8F: mov      eax, dword ptr [eax]
0x498E91: mov      edx, dword ptr [ebp - 4]
0x498E94: mov      edx, dword ptr [edx + 0xd0]
0x498E9A: call     0x472db0
0x498E9F: mov      dword ptr [ebp - 0x1c], eax
0x498EA2: cmp      dword ptr [ebp - 0x1c], 0
0x498EA6: je       0x498f3d
0x498EAC: mov      eax, dword ptr [ebp - 0x1c]
0x498EAF: movzx    eax, byte ptr [eax + 0x11]
0x498EB3: shr      eax, 1
0x498EB5: mov      dword ptr [ebp - 0x18], eax
0x498EB8: mov      eax, dword ptr [ebp - 0x18]
0x498EBB: cmp      eax, 0x12
0x498EBE: ja       0x498f30
0x498EC0: mov      al, byte ptr [eax + 0x498ecd]
0x498EC6: jmp      dword ptr [eax*4 + 0x498ee0]
0x498ECD: add      byte ptr [edx], al
0x498ECF: add      al, byte ptr [ebx]
0x498ED1: add      al, byte ptr [edx]
0x498ED3: add      dword ptr [eax], eax
0x498ED5: add      al, 2
0x498ED7: add      al, byte ptr [ebx]
0x498ED9: add      al, 2
0x498EDB: add      al, byte ptr [edx]
0x498EDD: add      al, byte ptr [edx]
0x498EDF: add      al, 0x30
0x498EE2: dec      ecx
0x498EE3: add      ah, dh
0x498EE5: mov      cs, word ptr [ecx]
0x498EE8: add      ecx, dword ptr [edi - 0x70edffb7]
0x498EEE: dec      ecx
0x498EEF: add      byte ptr [ecx], ah
0x498EF2: dec      ecx
0x498EF3: add      byte ptr [ebx - 0x7f3803bb], cl
0x498EF9: add      byte ptr [ecx], 0
0x498EFC: add      byte ptr [esi], al
0x498EFF: add      byte ptr [eax], al
0x498F01: jmp      0x498f3d
0x498F03: mov      eax, dword ptr [ebp - 4]
0x498F06: mov      dword ptr [eax + 0x180], 0x46
0x498F10: jmp      0x498f3d
0x498F12: mov      eax, dword ptr [ebp - 4]
0x498F15: mov      dword ptr [eax + 0x180], 0x47
0x498F1F: jmp      0x498f3d
0x498F21: mov      eax, dword ptr [ebp - 4]
0x498F24: mov      dword ptr [eax + 0x180], 0x48
0x498F2E: jmp      0x498f3d
0x498F30: mov      eax, dword ptr [ebp - 4]
0x498F33: mov      dword ptr [eax + 0x180], 0x49
0x498F3D: mov      eax, dword ptr [ebp - 4]
0x498F40: cmp      byte ptr [eax + 0xe], 0x32
0x498F44: je       0x499010
0x498F4A: mov      eax, dword ptr [ebp - 4]
0x498F4D: movzx    eax, word ptr [eax + 0x14]
0x498F51: add      eax, eax
0x498F53: lea      eax, [eax + eax*4]
0x498F56: add      eax, 0xc8
0x498F5B: mov      edx, dword ptr [ebp - 4]
0x498F5E: mov      dword ptr [edx + 0x188], eax
0x498F64: mov      eax, dword ptr [ebp - 4]
0x498F67: movzx    eax, word ptr [eax + 0x14]
0x498F6B: add      eax, eax
0x498F6D: lea      eax, [eax + eax*4]
0x498F70: add      eax, 0xc8
0x498F75: inc      eax
0x498F76: mov      edx, dword ptr [ebp - 4]
0x498F79: mov      dword ptr [edx + 0x18c], eax
0x498F7F: mov      eax, dword ptr [ebp - 4]
0x498F82: movzx    eax, word ptr [eax + 0x14]
0x498F86: add      eax, eax
0x498F88: lea      eax, [eax + eax*4]
0x498F8B: add      eax, 0xc8
0x498F90: add      eax, 2
0x498F93: mov      edx, dword ptr [ebp - 4]
0x498F96: mov      dword ptr [edx + 0x190], eax
0x498F9C: mov      eax, dword ptr [ebp - 4]
0x498F9F: movzx    eax, word ptr [eax + 0x14]
0x498FA3: add      eax, eax
0x498FA5: lea      eax, [eax + eax*4]
0x498FA8: add      eax, 0xc8
0x498FAD: add      eax, 3
0x498FB0: mov      edx, dword ptr [ebp - 4]
0x498FB3: mov      dword ptr [edx + 0x194], eax
0x498FB9: mov      eax, dword ptr [ebp - 4]
0x498FBC: movzx    eax, word ptr [eax + 0x14]
0x498FC0: add      eax, eax
0x498FC2: lea      eax, [eax + eax*4]
0x498FC5: add      eax, 0xc8
0x498FCA: add      eax, 4
0x498FCD: mov      edx, dword ptr [ebp - 4]
0x498FD0: mov      dword ptr [edx + 0x198], eax
0x498FD6: mov      eax, dword ptr [ebp - 4]
0x498FD9: movzx    eax, word ptr [eax + 0x14]
0x498FDD: add      eax, eax
0x498FDF: lea      eax, [eax + eax*4]
0x498FE2: add      eax, 0xc8
0x498FE7: add      eax, 5
0x498FEA: mov      edx, dword ptr [ebp - 4]
0x498FED: mov      dword ptr [edx + 0x19c], eax
0x498FF3: mov      eax, dword ptr [ebp - 4]
0x498FF6: movzx    eax, word ptr [eax + 0x14]
0x498FFA: add      eax, eax
0x498FFC: lea      eax, [eax + eax*4]
0x498FFF: add      eax, 0xc8
0x499004: add      eax, 6
0x499007: mov      edx, dword ptr [ebp - 4]
0x49900A: mov      dword ptr [edx + 0x1a0], eax
0x499010: mov      eax, dword ptr [ebp - 4]
0x499013: cmp      dword ptr [eax + 0x148], 0x1f
0x49901A: jne      0x499120
0x499020: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x499025: mov      eax, dword ptr [eax]
0x499027: mov      edx, dword ptr [ebp - 4]
0x49902A: mov      edx, dword ptr [edx + 0xd0]
0x499030: call     0x472db0
0x499035: mov      dword ptr [ebp - 0x1c], eax
0x499038: xor      eax, eax
0x49903A: mov      dword ptr [ebp - 0x18], eax
0x49903D: cmp      dword ptr [ebp - 0x1c], 0
0x499041: je       0x499120
0x499047: mov      eax, dword ptr [ebp - 0x1c]
0x49904A: movzx    eax, byte ptr [eax + 0x11]
0x49904E: shr      eax, 1
0x499050: mov      dword ptr [ebp - 0x18], eax
0x499053: mov      eax, dword ptr [ebp - 0x1c]
0x499056: cmp      byte ptr [eax + 0xe], 0
0x49905A: jne      0x499120
0x499060: mov      eax, dword ptr [ebp - 0x18]
0x499063: sar      eax, 1
0x499065: jns      0x49906a
0x499067: adc      eax, 0
0x49906A: cmp      eax, 0x18
0x49906D: ja       0x499120
0x499073: mov      al, byte ptr [eax + 0x499080]
0x499079: jmp      dword ptr [eax*4 + 0x499099]
0x499080: add      byte ptr [edx], al
0x499082: add      eax, dword ptr [0x5010304]
0x499088: pop      es
0x499089: add      eax, dword ptr [eax + 0x4030307]
0x499090: add      al, 4
0x499092: pop      es
0x499093: add      byte ptr [ecx], al
0x499095: pop      es
0x499096: add      eax, dword ptr [esi + eax]
0x499099: and      byte ptr [ecx - 0x6f46ffb7], dl
0x49909F: dec      ecx
0x4990A0: add      al, cl
0x4990A2: nop      
0x4990A3: dec      ecx
0x4990A4: add      bh, dl
0x4990A6: nop      
0x4990A7: dec      ecx
0x4990A8: add      dh, ah
0x4990AA: nop      
0x4990AB: dec      ecx
0x4990AC: add      ch, dh
0x4990AE: nop      
0x4990AF: dec      ecx
0x4990B0: add      byte ptr [ecx + edx*4], al
0x4990B3: dec      ecx
0x4990B4: add      byte ptr [ebx], dl
0x4990B6: xchg     ecx, eax
0x4990B7: dec      ecx
0x4990B8: add      byte ptr [ebx - 0x7f3803bb], cl
0x4990BE: test     byte ptr [ecx], al
0x4990C0: add      byte ptr [eax], al
0x4990C2: cmp      al, 0
0x4990C4: add      byte ptr [eax], al
0x4990C6: jmp      0x499120
0x4990C8: mov      eax, dword ptr [ebp - 4]
0x4990CB: mov      dword ptr [eax + 0x184], 0x3d
0x4990D5: jmp      0x499120
0x4990D7: mov      eax, dword ptr [ebp - 4]
0x4990DA: mov      dword ptr [eax + 0x184], 0x3e
0x4990E4: jmp      0x499120
0x4990E6: mov      eax, dword ptr [ebp - 4]
0x4990E9: mov      dword ptr [eax + 0x184], 0x3f
0x4990F3: jmp      0x499120
0x4990F5: mov      eax, dword ptr [ebp - 4]
0x4990F8: mov      dword ptr [eax + 0x184], 0x40
0x499102: jmp      0x499120
0x499104: mov      eax, dword ptr [ebp - 4]
0x499107: mov      dword ptr [eax + 0x184], 0x41
0x499111: jmp      0x499120
0x499113: mov      eax, dword ptr [ebp - 4]
0x499116: mov      dword ptr [eax + 0x184], 0x3d
0x499120: mov      esp, ebp
0x499122: pop      ebp
0x499123: ret      

; === TActor.DMT_-7 @ 0x499124 ===
0x499124: push     ebp
0x499125: mov      ebp, esp
0x499127: push     ecx
0x499128: push     ebx
0x499129: mov      dword ptr [ebp - 4], eax
0x49912C: mov      eax, dword ptr [ebp - 4]
0x49912F: mov      byte ptr [eax + 0x178], 1
0x499136: mov      eax, dword ptr [ebp - 4]
0x499139: mov      bx, 0xfffb
0x49913D: call     0x403080
0x499142: mov      eax, dword ptr [ebp - 4]
0x499145: mov      eax, dword ptr [eax + 0x148]
0x49914B: add      eax, -5
0x49914E: cmp      eax, 0x1d
0x499151: ja       0x499240
0x499157: mov      al, byte ptr [eax + 0x499164]
0x49915D: jmp      dword ptr [eax*4 + 0x499182]
0x499164: add      eax, dword ptr [eax]
0x499166: add      byte ptr [eax], al
0x499168: add      byte ptr [eax], al
0x49916A: add      byte ptr [eax], al
0x49916C: add      byte ptr [ebx], al
0x49916E: add      byte ptr [eax], al
0x499170: add      eax, 0x3040000
0x499175: add      eax, dword ptr [ebx]
0x499177: add      byte ptr [eax], al
0x499179: add      byte ptr [eax + eax], al
0x49917C: add      byte ptr [eax], al
0x49917E: add      dword ptr [eax], eax
0x499180: add      byte ptr [edx], al
0x499182: inc      eax
0x499183: xchg     edx, eax
0x499184: dec      ecx
0x499185: add      byte ptr [edx - 0x15ffb66f], bl
0x49918B: xchg     ecx, eax
0x49918C: dec      ecx
0x49918D: add      byte ptr [esi], al
0x49918F: xchg     edx, eax
0x499190: dec      ecx
0x499191: add      byte ptr [edx], ah
0x499193: xchg     edx, eax
0x499194: dec      ecx
0x499195: add      byte ptr [edx], dh
0x499197: xchg     edx, eax
0x499198: dec      ecx
0x499199: add      byte ptr [ebx - 0x477c03bb], cl
0x49919F: test     byte ptr [ecx], al
0x4991A1: add      byte ptr [eax], al
0x4991A3: add      byte ptr [esi + ecx - 0x75], bh
0x4991A7: inc      ebp
0x4991A8: cld      
0x4991A9: mov      eax, dword ptr [eax + 0x184]
0x4991AF: call     0x495794
0x4991B4: mov      eax, dword ptr [ebp - 4]
0x4991B7: cmp      dword ptr [eax + 0x180], 0
0x4991BE: jl       0x4991ce
0x4991C0: mov      eax, dword ptr [ebp - 4]
0x4991C3: mov      eax, dword ptr [eax + 0x180]
0x4991C9: call     0x495794
0x4991CE: mov      eax, dword ptr [ebp - 4]
0x4991D1: cmp      dword ptr [eax + 0x198], 0
0x4991D8: jl       0x499240
0x4991DA: mov      eax, dword ptr [ebp - 4]
0x4991DD: mov      eax, dword ptr [eax + 0x198]
0x4991E3: call     0x495794
0x4991E8: jmp      0x499240
0x4991EA: mov      eax, dword ptr [ebp - 4]
0x4991ED: cmp      dword ptr [eax + 0x19c], 0
0x4991F4: jl       0x499240
0x4991F6: mov      eax, dword ptr [ebp - 4]
0x4991F9: mov      eax, dword ptr [eax + 0x19c]
0x4991FF: call     0x495794
0x499204: jmp      0x499240
0x499206: mov      eax, dword ptr [ebp - 4]
0x499209: cmp      dword ptr [eax + 0x190], 0
0x499210: jl       0x499240
0x499212: mov      eax, dword ptr [ebp - 4]
0x499215: mov      eax, dword ptr [eax + 0x190]
0x49921B: call     0x495794
0x499220: jmp      0x499240
0x499222: mov      eax, dword ptr [ebp - 4]
0x499225: mov      eax, dword ptr [eax + 0x188]
0x49922B: call     0x495794
0x499230: jmp      0x499240
0x499232: mov      eax, dword ptr [ebp - 4]
0x499235: mov      eax, dword ptr [eax + 0x1a4]
0x49923B: call     0x495794
0x499240: pop      ebx
0x499241: pop      ecx
0x499242: pop      ebp
0x499243: ret      

; === TActor.DMT_-8 @ 0x499244 ===
0x499244: push     ebp
0x499245: mov      ebp, esp
0x499247: add      esp, -8
0x49924A: mov      dword ptr [ebp - 8], edx
0x49924D: mov      dword ptr [ebp - 4], eax
0x499250: mov      eax, dword ptr [ebp - 4]
0x499253: cmp      byte ptr [eax + 0x178], 0
0x49925A: je       0x499478
0x499260: mov      eax, dword ptr [ebp - 4]
0x499263: cmp      byte ptr [eax + 0xe], 0
0x499267: jne      0x4993bb
0x49926D: mov      eax, dword ptr [ebp - 4]
0x499270: mov      eax, dword ptr [eax + 0x148]
0x499276: add      eax, -5
0x499279: cmp      eax, 0x13
0x49927C: ja       0x499478
0x499282: mov      al, byte ptr [eax + 0x49928f]
0x499288: jmp      dword ptr [eax*4 + 0x4992a3]
0x49928F: add      dword ptr [eax], eax
0x499291: add      byte ptr [0], al
0x499297: add      byte ptr [ecx], al
0x499299: add      dword ptr [ecx], eax
0x49929B: add      byte ptr [edx], al
0x49929D: add      eax, dword ptr [eax]
0x49929F: add      byte ptr [eax], al
0x4992A1: add      byte ptr [eax + edi*2], al
0x4992A4: xchg     esp, eax
0x4992A5: dec      ecx
0x4992A6: add      byte ptr [ebx - 0x1dffb66e], bh
0x4992AC: xchg     edx, eax
0x4992AD: dec      ecx
0x4992AE: add      byte ptr [eax], ch
0x4992B0: xchg     ebx, eax
0x4992B1: dec      ecx
0x4992B2: add      byte ptr [ecx - 0x6d], bl
0x4992B5: dec      ecx
0x4992B6: add      byte ptr [edx - 0x7cffb66d], cl
0x4992BC: jge      0x4992b6
0x4992BE: add      cl, byte ptr [edi]
0x4992C0: test     dword ptr [ebx - 0x74ffffff], esi
0x4992C6: inc      ebp
0x4992C7: cld      
0x4992C8: mov      eax, dword ptr [eax + 0x194]
0x4992CE: call     0x495794
0x4992D3: mov      eax, dword ptr [ebp - 4]
0x4992D6: mov      byte ptr [eax + 0x178], 0
0x4992DD: jmp      0x499478
0x4992E2: cmp      dword ptr [ebp - 8], 2
0x4992E6: jne      0x499478
0x4992EC: mov      eax, dword ptr [ebp - 4]
0x4992EF: mov      eax, dword ptr [eax + 0x194]
0x4992F5: call     0x495794
0x4992FA: mov      eax, dword ptr [ebp - 4]
0x4992FD: cmp      byte ptr [eax + 0xd], 0
0x499301: jne      0x49930f
0x499303: mov      eax, 0x82
0x499308: call     0x495794
0x49930D: jmp      0x499319
0x49930F: mov      eax, 0x83
0x499314: call     0x495794
0x499319: mov      eax, dword ptr [ebp - 4]
0x49931C: mov      byte ptr [eax + 0x178], 0
0x499323: jmp      0x499478
0x499328: cmp      dword ptr [ebp - 8], 2
0x49932C: jne      0x499478
0x499332: mov      eax, dword ptr [ebp - 4]
0x499335: mov      eax, dword ptr [eax + 0x194]
0x49933B: call     0x495794
0x499340: mov      eax, 0x84
0x499345: call     0x495794
0x49934A: mov      eax, dword ptr [ebp - 4]
0x49934D: mov      byte ptr [eax + 0x178], 0
0x499354: jmp      0x499478
0x499359: cmp      dword ptr [ebp - 8], 2
0x49935D: jne      0x499478
0x499363: mov      eax, dword ptr [ebp - 4]
0x499366: mov      eax, dword ptr [eax + 0x194]
0x49936C: call     0x495794
0x499371: mov      eax, 0x85
0x499376: call     0x495794
0x49937B: mov      eax, dword ptr [ebp - 4]
0x49937E: mov      byte ptr [eax + 0x178], 0
0x499385: jmp      0x499478
0x49938A: cmp      dword ptr [ebp - 8], 2
0x49938E: jne      0x499478
0x499394: mov      eax, dword ptr [ebp - 4]
0x499397: mov      eax, dword ptr [eax + 0x194]
0x49939D: call     0x495794
0x4993A2: mov      eax, 0x89
0x4993A7: call     0x495794
0x4993AC: mov      eax, dword ptr [ebp - 4]
0x4993AF: mov      byte ptr [eax + 0x178], 0
0x4993B6: jmp      0x499478
0x4993BB: mov      eax, dword ptr [ebp - 4]
0x4993BE: cmp      byte ptr [eax + 0xe], 0x32
0x4993C2: je       0x499478
0x4993C8: mov      eax, dword ptr [ebp - 4]
0x4993CB: cmp      dword ptr [eax + 0x148], 0xb
0x4993D2: je       0x4993e0
0x4993D4: mov      eax, dword ptr [ebp - 4]
0x4993D7: cmp      dword ptr [eax + 0x148], 0xa
0x4993DE: jne      0x49940b
0x4993E0: cmp      dword ptr [ebp - 8], 1
0x4993E4: jne      0x49940b
0x4993E6: mov      eax, 8
0x4993EB: call     0x402b74
0x4993F0: dec      eax
0x4993F1: jne      0x49940b
0x4993F3: mov      eax, dword ptr [ebp - 4]
0x4993F6: mov      eax, dword ptr [eax + 0x18c]
0x4993FC: call     0x495794
0x499401: mov      eax, dword ptr [ebp - 4]
0x499404: mov      byte ptr [eax + 0x178], 0
0x49940B: mov      eax, dword ptr [ebp - 4]
0x49940E: cmp      dword ptr [eax + 0x148], 0xe
0x499415: jne      0x499441
0x499417: cmp      dword ptr [ebp - 8], 3
0x49941B: jne      0x499441
0x49941D: mov      eax, dword ptr [ebp - 4]
0x499420: cmp      dword ptr [eax + 0x190], 0
0x499427: jl       0x499441
0x499429: mov      eax, dword ptr [ebp - 4]
0x49942C: mov      eax, dword ptr [eax + 0x194]
0x499432: call     0x495794
0x499437: mov      eax, dword ptr [ebp - 4]
0x49943A: mov      byte ptr [eax + 0x178], 0
0x499441: mov      eax, dword ptr [ebp - 4]
0x499444: mov      ax, word ptr [eax + 0x14]
0x499448: sub      ax, 0x50
0x49944C: jne      0x499478
0x49944E: mov      eax, dword ptr [ebp - 4]
0x499451: cmp      dword ptr [eax + 0x148], 0x22
0x499458: jne      0x499478
0x49945A: cmp      dword ptr [ebp - 8], 2
0x49945E: jne      0x499478
0x499460: mov      eax, dword ptr [ebp - 4]
0x499463: mov      eax, dword ptr [eax + 0x1a0]
0x499469: call     0x495794
0x49946E: mov      eax, dword ptr [ebp - 4]
0x499471: mov      byte ptr [eax + 0x178], 0
0x499478: pop      ecx
0x499479: pop      ecx
0x49947A: pop      ebp
0x49947B: ret      

; === TActor.DMT_-9 @ 0x49947C ===
0x49947C: push     ebp
0x49947D: mov      ebp, esp
0x49947F: add      esp, -8
0x499482: mov      dword ptr [ebp - 8], edx
0x499485: mov      dword ptr [ebp - 4], eax
0x499488: pop      ecx
0x499489: pop      ecx
0x49948A: pop      ebp
0x49948B: ret      

; === TActor.DMT_-10 @ 0x49948C ===
0x49948C: push     ebp
0x49948D: mov      ebp, esp
0x49948F: push     ecx
0x499490: mov      dword ptr [ebp - 4], eax
0x499493: pop      ecx
0x499494: pop      ebp
0x499495: ret      
0x499496: mov      eax, eax
0x499498: push     ebp
0x499499: mov      ebp, esp
0x49949B: push     ecx
0x49949C: mov      eax, dword ptr [ebp + 8]
0x49949F: mov      eax, dword ptr [eax - 4]
0x4994A2: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4994A8: cmp      eax, dword ptr [edx]
0x4994AA: jne      0x4994c8
0x4994AC: call     0x406154
0x4994B1: mov      edx, dword ptr [ebp + 8]
0x4994B4: mov      edx, dword ptr [edx - 4]
0x4994B7: sub      eax, dword ptr [edx + 0xf4]
0x4994BD: cmp      eax, 0xbb8
0x4994C2: seta     byte ptr [ebp - 1]
0x4994C6: jmp      0x4994e2
0x4994C8: call     0x406154
0x4994CD: mov      edx, dword ptr [ebp + 8]
0x4994D0: mov      edx, dword ptr [edx - 4]
0x4994D3: sub      eax, dword ptr [edx + 0xf4]
0x4994D9: cmp      eax, 0x7d0
0x4994DE: seta     byte ptr [ebp - 1]
0x4994E2: cmp      byte ptr [ebp - 1], 0
0x4994E6: je       0x4994f6
0x4994E8: mov      eax, dword ptr [ebp + 8]
0x4994EB: mov      eax, dword ptr [eax - 4]
0x4994EE: xor      edx, edx
0x4994F0: mov      dword ptr [eax + 0x10c], edx
0x4994F6: mov      al, byte ptr [ebp - 1]
0x4994F9: pop      ecx
0x4994FA: pop      ebp
0x4994FB: ret      

; === TActor.DMT_-6 @ 0x4994FC ===
0x4994FC: push     ebp
0x4994FD: mov      ebp, esp
0x4994FF: add      esp, -0x1c
0x499502: push     ebx
0x499503: mov      dword ptr [ebp - 4], eax
0x499506: mov      eax, dword ptr [ebp - 4]
0x499509: cmp      dword ptr [eax + 0x148], 0xb
0x499510: je       0x49999a
0x499516: mov      eax, dword ptr [ebp - 4]
0x499519: cmp      dword ptr [eax + 0x148], 9
0x499520: je       0x49999a
0x499526: mov      eax, dword ptr [ebp - 4]
0x499529: cmp      dword ptr [eax + 0x148], 0xd
0x499530: je       0x49999a
0x499536: mov      eax, dword ptr [ebp - 4]
0x499539: cmp      dword ptr [eax + 0x148], 6
0x499540: je       0x49999a
0x499546: mov      eax, dword ptr [ebp - 4]
0x499549: cmp      dword ptr [eax + 0x148], 7
0x499550: je       0x49999a
0x499556: mov      eax, dword ptr [ebp - 4]
0x499559: mov      byte ptr [eax + 0x1e4], 0
0x499560: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x499565: mov      eax, dword ptr [eax]
0x499567: cmp      eax, dword ptr [ebp - 4]
0x49956A: je       0x499585
0x49956C: mov      eax, dword ptr [ebp - 4]
0x49956F: mov      eax, dword ptr [eax + 0x220]
0x499575: cmp      dword ptr [eax + 8], 2
0x499579: jl       0x499585
0x49957B: mov      eax, dword ptr [ebp - 4]
0x49957E: mov      byte ptr [eax + 0x1e4], 1
0x499585: mov      eax, dword ptr [ebp - 4]
0x499588: mov      edx, dword ptr [eax + 0x1b8]
0x49958E: mov      eax, dword ptr [ebp - 4]
0x499591: sub      edx, dword ptr [eax + 0x1b0]
0x499597: mov      eax, dword ptr [ebp - 4]
0x49959A: mov      bx, 0xfff8
0x49959E: call     0x403080
0x4995A3: mov      eax, dword ptr [ebp - 4]
0x4995A6: mov      edx, dword ptr [eax + 0x1b8]
0x4995AC: mov      eax, dword ptr [ebp - 4]
0x4995AF: sub      edx, dword ptr [eax + 0x1b0]
0x4995B5: mov      eax, dword ptr [ebp - 4]
0x4995B8: mov      bx, 0xfff7
0x4995BC: call     0x403080
0x4995C1: mov      eax, dword ptr [ebp - 4]
0x4995C4: mov      eax, dword ptr [eax + 0x1b8]
0x4995CA: mov      dword ptr [ebp - 8], eax
0x4995CD: mov      eax, dword ptr [ebp - 4]
0x4995D0: cmp      dword ptr [eax + 0x148], 0
0x4995D7: je       0x499906
0x4995DD: mov      eax, dword ptr [ebp - 4]
0x4995E0: mov      eax, dword ptr [eax + 0x1b8]
0x4995E6: mov      edx, dword ptr [ebp - 4]
0x4995E9: cmp      eax, dword ptr [edx + 0x1b0]
0x4995EF: jl       0x499605
0x4995F1: mov      eax, dword ptr [ebp - 4]
0x4995F4: mov      eax, dword ptr [eax + 0x1b8]
0x4995FA: mov      edx, dword ptr [ebp - 4]
0x4995FD: cmp      eax, dword ptr [edx + 0x1b4]
0x499603: jle      0x499617
0x499605: mov      eax, dword ptr [ebp - 4]
0x499608: mov      eax, dword ptr [eax + 0x1b0]
0x49960E: mov      edx, dword ptr [ebp - 4]
0x499611: mov      dword ptr [edx + 0x1b8], eax
0x499617: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49961C: mov      eax, dword ptr [eax]
0x49961E: cmp      eax, dword ptr [ebp - 4]
0x499621: je       0x499655
0x499623: mov      eax, dword ptr [ebp - 4]
0x499626: cmp      byte ptr [eax + 0xec], 0
0x49962D: je       0x499655
0x49962F: mov      eax, dword ptr [ebp - 4]
0x499632: mov      eax, dword ptr [eax + 0x1d0]
0x499638: mov      dword ptr [ebp - 0x1c], eax
0x49963B: xor      eax, eax
0x49963D: mov      dword ptr [ebp - 0x18], eax
0x499640: fild     qword ptr [ebp - 0x1c]
0x499643: fld      xword ptr [0x4999a0]
0x499649: fdivp    st(1)
0x49964B: call     0x4029a0
0x499650: mov      dword ptr [ebp - 0xc], eax
0x499653: jmp      0x499693
0x499655: mov      eax, dword ptr [ebp - 4]
0x499658: cmp      byte ptr [eax + 0x1e4], 0
0x49965F: je       0x499687
0x499661: mov      eax, dword ptr [ebp - 4]
0x499664: mov      eax, dword ptr [eax + 0x1d0]
0x49966A: add      eax, eax
0x49966C: mov      dword ptr [ebp - 0x1c], eax
0x49966F: xor      eax, eax
0x499671: mov      dword ptr [ebp - 0x18], eax
0x499674: fild     qword ptr [ebp - 0x1c]
0x499677: fdiv     dword ptr [0x4999ac]
0x49967D: call     0x4029a0
0x499682: mov      dword ptr [ebp - 0xc], eax
0x499685: jmp      0x499693
0x499687: mov      eax, dword ptr [ebp - 4]
0x49968A: mov      eax, dword ptr [eax + 0x1d0]
0x499690: mov      dword ptr [ebp - 0xc], eax
0x499693: call     0x406154
0x499698: mov      edx, dword ptr [ebp - 4]
0x49969B: sub      eax, dword ptr [edx + 0x1d4]
0x4996A1: cmp      eax, dword ptr [ebp - 0xc]
0x4996A4: jbe      0x4998c9
0x4996AA: mov      eax, dword ptr [ebp - 4]
0x4996AD: mov      eax, dword ptr [eax + 0x1b8]
0x4996B3: mov      edx, dword ptr [ebp - 4]
0x4996B6: cmp      eax, dword ptr [edx + 0x1b4]
0x4996BC: jge      0x49978b
0x4996C2: mov      eax, dword ptr [ebp - 4]
0x4996C5: cmp      byte ptr [eax + 0xec], 0
0x4996CC: je       0x499772
0x4996D2: mov      eax, dword ptr [ebp - 4]
0x4996D5: mov      eax, dword ptr [eax + 0x104]
0x4996DB: mov      edx, dword ptr [ebp - 4]
0x4996DE: mov      edx, dword ptr [edx + 0x108]
0x4996E4: sub      edx, 2
0x4996E7: cmp      eax, edx
0x4996E9: je       0x4996f6
0x4996EB: push     ebp
0x4996EC: call     0x499498
0x4996F1: pop      ecx
0x4996F2: test     al, al
0x4996F4: je       0x499736
0x4996F6: mov      eax, dword ptr [ebp - 4]
0x4996F9: cmp      dword ptr [eax + 0x10c], 0
0x499700: jge      0x499711
0x499702: push     ebp
0x499703: call     0x499498
0x499708: pop      ecx
0x499709: test     al, al
0x49970B: je       0x4997fb
0x499711: mov      eax, dword ptr [ebp - 4]
0x499714: inc      dword ptr [eax + 0x1b8]
0x49971A: mov      eax, dword ptr [ebp - 4]
0x49971D: inc      dword ptr [eax + 0x104]
0x499723: call     0x406154
0x499728: mov      edx, dword ptr [ebp - 4]
0x49972B: mov      dword ptr [edx + 0x1d4], eax
0x499731: jmp      0x4997fb
0x499736: mov      eax, dword ptr [ebp - 4]
0x499739: mov      eax, dword ptr [eax + 0x1b8]
0x49973F: mov      edx, dword ptr [ebp - 4]
0x499742: mov      edx, dword ptr [edx + 0x1b4]
0x499748: dec      edx
0x499749: cmp      eax, edx
0x49974B: jge      0x499756
0x49974D: mov      eax, dword ptr [ebp - 4]
0x499750: inc      dword ptr [eax + 0x1b8]
0x499756: mov      eax, dword ptr [ebp - 4]
0x499759: inc      dword ptr [eax + 0x104]
0x49975F: call     0x406154
0x499764: mov      edx, dword ptr [ebp - 4]
0x499767: mov      dword ptr [edx + 0x1d4], eax
0x49976D: jmp      0x4997fb
0x499772: mov      eax, dword ptr [ebp - 4]
0x499775: inc      dword ptr [eax + 0x1b8]
0x49977B: call     0x406154
0x499780: mov      edx, dword ptr [ebp - 4]
0x499783: mov      dword ptr [edx + 0x1d4], eax
0x499789: jmp      0x4997fb
0x49978B: mov      eax, dword ptr [ebp - 4]
0x49978E: cmp      byte ptr [eax + 0x23], 0
0x499792: je       0x49979b
0x499794: mov      eax, dword ptr [ebp - 4]
0x499797: mov      byte ptr [eax + 0x22], 1
0x49979B: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4997A0: mov      eax, dword ptr [eax]
0x4997A2: cmp      eax, dword ptr [ebp - 4]
0x4997A5: jne      0x4997da
0x4997A7: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4997AC: mov      eax, dword ptr [eax]
0x4997AE: call     0x466f1c
0x4997B3: test     al, al
0x4997B5: je       0x4997fb
0x4997B7: mov      eax, dword ptr [ebp - 4]
0x4997BA: mov      bx, 0xfff6
0x4997BE: call     0x403080
0x4997C3: mov      eax, dword ptr [ebp - 4]
0x4997C6: xor      edx, edx
0x4997C8: mov      dword ptr [eax + 0x148], edx
0x4997CE: mov      eax, dword ptr [ebp - 4]
0x4997D1: mov      byte ptr [eax + 0xec], 0
0x4997D8: jmp      0x4997fb
0x4997DA: mov      eax, dword ptr [ebp - 4]
0x4997DD: mov      bx, 0xfff6
0x4997E1: call     0x403080
0x4997E6: mov      eax, dword ptr [ebp - 4]
0x4997E9: xor      edx, edx
0x4997EB: mov      dword ptr [eax + 0x148], edx
0x4997F1: mov      eax, dword ptr [ebp - 4]
0x4997F4: mov      byte ptr [eax + 0xec], 0
0x4997FB: mov      eax, dword ptr [ebp - 4]
0x4997FE: cmp      byte ptr [eax + 0xec], 0
0x499805: je       0x4998c9
0x49980B: mov      eax, dword ptr [ebp - 4]
0x49980E: mov      eax, dword ptr [eax + 0x104]
0x499814: mov      edx, dword ptr [ebp - 4]
0x499817: mov      edx, dword ptr [edx + 0x108]
0x49981D: dec      edx
0x49981E: cmp      eax, edx
0x499820: jne      0x4998c9
0x499826: mov      eax, dword ptr [ebp - 4]
0x499829: cmp      dword ptr [eax + 0x10c], 0
0x499830: jle      0x4998be
0x499836: mov      eax, dword ptr [ebp - 4]
0x499839: add      eax, 0x10c
0x49983E: mov      dword ptr [ebp - 0x14], eax
0x499841: mov      eax, dword ptr [ebp - 0x14]
0x499844: mov      eax, dword ptr [eax + 0x10]
0x499847: push     eax
0x499848: mov      eax, dword ptr [ebp - 4]
0x49984B: movzx    eax, word ptr [eax + 8]
0x49984F: push     eax
0x499850: mov      eax, dword ptr [ebp - 4]
0x499853: movzx    eax, word ptr [eax + 0xa]
0x499857: push     eax
0x499858: mov      eax, dword ptr [ebp - 0x14]
0x49985B: mov      eax, dword ptr [eax + 0x14]
0x49985E: push     eax
0x49985F: mov      eax, dword ptr [ebp - 0x14]
0x499862: mov      eax, dword ptr [eax + 0x18]
0x499865: push     eax
0x499866: mov      eax, dword ptr [ebp - 0x14]
0x499869: mov      eax, dword ptr [eax + 8]
0x49986C: push     eax
0x49986D: mov      eax, dword ptr [ebp - 0x14]
0x499870: mov      al, byte ptr [eax + 0xc]
0x499873: push     eax
0x499874: mov      eax, dword ptr [ebp - 0x14]
0x499877: mov      al, byte ptr [eax + 0x1c]
0x49987A: push     eax
0x49987B: mov      eax, dword ptr [ebp - 0x14]
0x49987E: mov      eax, dword ptr [eax + 0x20]
0x499881: push     eax
0x499882: lea      eax, [ebp - 0xd]
0x499885: push     eax
0x499886: mov      eax, dword ptr [ebp - 0x14]
0x499889: mov      ecx, dword ptr [eax]
0x49988B: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x499890: mov      eax, dword ptr [eax]
0x499892: mov      edx, dword ptr [ebp - 4]
0x499895: call     0x471da4
0x49989A: cmp      byte ptr [ebp - 0xd], 0
0x49989E: je       0x4998b0
0x4998A0: mov      eax, dword ptr [ebp - 4]
0x4998A3: mov      eax, dword ptr [eax + 0x1a8]
0x4998A9: call     0x495794
0x4998AE: jmp      0x4998be
0x4998B0: mov      eax, dword ptr [ebp - 4]
0x4998B3: mov      eax, dword ptr [eax + 0x1ac]
0x4998B9: call     0x495794
0x4998BE: mov      eax, dword ptr [ebp - 4]
0x4998C1: xor      edx, edx
0x4998C3: mov      dword ptr [eax + 0x10c], edx
0x4998C9: mov      eax, dword ptr [ebp - 4]
0x4998CC: mov      ax, word ptr [eax + 0x14]
0x4998D0: sub      ax, 2
0x4998D4: jb       0x4998dc
0x4998D6: sub      ax, 0x29
0x4998DA: jne      0x4998eb
0x4998DC: mov      eax, dword ptr [ebp - 4]
0x4998DF: mov      dword ptr [eax + 0x1ec], 0xfffffff6
0x4998E9: jmp      0x4998f6
0x4998EB: mov      eax, dword ptr [ebp - 4]
0x4998EE: xor      edx, edx
0x4998F0: mov      dword ptr [eax + 0x1ec], edx
0x4998F6: call     0x406154
0x4998FB: mov      edx, dword ptr [ebp - 4]
0x4998FE: mov      dword ptr [edx + 0x1f0], eax
0x499904: jmp      0x499972
0x499906: call     0x406154
0x49990B: mov      edx, dword ptr [ebp - 4]
0x49990E: sub      eax, dword ptr [edx + 0x1fc]
0x499914: cmp      eax, 0xc8
0x499919: jbe      0x499972
0x49991B: call     0x406154
0x499920: mov      edx, dword ptr [ebp - 4]
0x499923: sub      eax, dword ptr [edx + 0x1f0]
0x499929: cmp      eax, 0x1f4
0x49992E: jbe      0x499966
0x499930: call     0x406154
0x499935: mov      edx, dword ptr [ebp - 4]
0x499938: mov      dword ptr [edx + 0x1f0], eax
0x49993E: mov      eax, dword ptr [ebp - 4]
0x499941: inc      dword ptr [eax + 0x1ec]
0x499947: mov      eax, dword ptr [ebp - 4]
0x49994A: mov      eax, dword ptr [eax + 0x1ec]
0x499950: mov      edx, dword ptr [ebp - 4]
0x499953: cmp      eax, dword ptr [edx + 0x1f4]
0x499959: jl       0x499966
0x49995B: mov      eax, dword ptr [ebp - 4]
0x49995E: xor      edx, edx
0x499960: mov      dword ptr [eax + 0x1ec], edx
0x499966: mov      eax, dword ptr [ebp - 4]
0x499969: mov      bx, 0xfffe
0x49996D: call     0x403080
0x499972: mov      eax, dword ptr [ebp - 4]
0x499975: mov      eax, dword ptr [eax + 0x1b8]
0x49997B: cmp      eax, dword ptr [ebp - 8]
0x49997E: je       0x49999a
0x499980: call     0x406154
0x499985: mov      edx, dword ptr [ebp - 4]
0x499988: mov      dword ptr [edx + 0x204], eax
0x49998E: mov      eax, dword ptr [ebp - 4]
0x499991: mov      bx, 0xfff3
0x499995: call     0x403080
0x49999A: pop      ebx
0x49999B: mov      esp, ebp
0x49999D: pop      ebp
0x49999E: ret      
0x49999F: add      byte ptr [esi + 0x66], ah
0x4999A2: out      0xff, al
0x4999A9: aas      
0x4999AA: add      byte ptr [eax], al
0x4999AC: add      byte ptr [eax], al
0x4999AE: inc      eax
0x4999AF: inc      eax
0x4999B0: push     ebp
0x4999B1: mov      ebp, esp
0x4999B3: add      esp, -0x1c
0x4999B6: push     ebx
0x4999B7: mov      dword ptr [ebp - 0x1c], edx
0x4999BA: mov      dword ptr [ebp - 4], eax
0x4999BD: mov      byte ptr [ebp - 5], 0
0x4999C1: mov      byte ptr [ebp - 0x15], 0
0x4999C5: mov      byte ptr [ebp - 0x16], 0
0x4999C9: mov      eax, dword ptr [ebp - 4]
0x4999CC: cmp      dword ptr [eax + 0x148], 9
0x4999D3: jne      0x4999d9
0x4999D5: mov      byte ptr [ebp - 0x15], 1
0x4999D9: mov      eax, dword ptr [ebp - 4]
0x4999DC: cmp      dword ptr [eax + 0x148], 6
0x4999E3: je       0x4999f1
0x4999E5: mov      eax, dword ptr [ebp - 4]
0x4999E8: cmp      dword ptr [eax + 0x148], 7
0x4999EF: jne      0x4999f5
0x4999F1: mov      byte ptr [ebp - 0x16], 1
0x4999F5: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4999FA: mov      eax, dword ptr [eax]
0x4999FC: cmp      eax, dword ptr [ebp - 4]
0x4999FF: jne      0x499b5a
0x499A05: cmp      byte ptr [ebp - 0x15], 0
0x499A09: jne      0x499b5a
0x499A0F: cmp      byte ptr [ebp - 0x16], 0
0x499A13: jne      0x499b5a
0x499A19: mov      eax, dword ptr [0x4a424c] ; *0x4A424C->0x4F9472
0x499A1E: mov      byte ptr [eax], 0
0x499A21: mov      eax, dword ptr [0x4a3e98] ; *0x4A3E98->0x4F9471
0x499A26: mov      byte ptr [eax], 0
0x499A29: mov      eax, dword ptr [0x4a3ea8] ; *0x4A3EA8->0x4F9474
0x499A2E: xor      edx, edx
0x499A30: mov      dword ptr [eax], edx
0x499A32: mov      eax, dword ptr [ebp - 4]
0x499A35: mov      ax, word ptr [eax + 0x50]
0x499A39: mov      edx, dword ptr [ebp - 4]
0x499A3C: cmp      ax, word ptr [edx + 0x52]
0x499A40: jbe      0x499a66
0x499A42: mov      eax, dword ptr [ebp - 4]
0x499A45: movzx    eax, word ptr [eax + 0x50]
0x499A49: mov      edx, dword ptr [ebp - 4]
0x499A4C: movzx    edx, word ptr [edx + 0x52]
0x499A50: mov      ecx, edx
0x499A52: xor      edx, edx
0x499A54: div      ecx
0x499A56: mov      edx, dword ptr [0x4a3ea8] ; *0x4A3EA8->0x4F9474
0x499A5C: mov      dword ptr [edx], eax
0x499A5E: mov      eax, dword ptr [0x4a424c] ; *0x4A424C->0x4F9472
0x499A63: mov      byte ptr [eax], 1
0x499A66: mov      eax, dword ptr [ebp - 4]
0x499A69: mov      al, byte ptr [eax + 0x54]
0x499A6C: mov      edx, dword ptr [ebp - 4]
0x499A6F: cmp      al, byte ptr [edx + 0x55]
0x499A72: jbe      0x499a98
0x499A74: mov      eax, dword ptr [ebp - 4]
0x499A77: movzx    eax, byte ptr [eax + 0x54]
0x499A7B: mov      edx, dword ptr [ebp - 4]
0x499A7E: movzx    edx, byte ptr [edx + 0x55]
0x499A82: mov      ecx, edx
0x499A84: xor      edx, edx
0x499A86: div      ecx
0x499A88: mov      edx, dword ptr [0x4a3ea8] ; *0x4A3EA8->0x4F9474
0x499A8E: add      dword ptr [edx], eax
0x499A90: mov      eax, dword ptr [0x4a424c] ; *0x4A424C->0x4F9472
0x499A95: mov      byte ptr [eax], 1
0x499A98: mov      eax, dword ptr [ebp - 4]
0x499A9B: mov      al, byte ptr [eax + 0x56]
0x499A9E: mov      edx, dword ptr [ebp - 4]
0x499AA1: cmp      al, byte ptr [edx + 0x57]
0x499AA4: jbe      0x499aae
0x499AA6: mov      eax, dword ptr [0x4a3e98] ; *0x4A3E98->0x4F9471
0x499AAB: mov      byte ptr [eax], 1
0x499AAE: mov      eax, dword ptr [0x4a424c] ; *0x4A424C->0x4F9472
0x499AB3: cmp      byte ptr [eax], 0
0x499AB6: je       0x499ad8
0x499AB8: mov      eax, dword ptr [0x4a3ea8] ; *0x4A3EA8->0x4F9474
0x499ABD: mov      eax, dword ptr [eax]
0x499ABF: mov      edx, dword ptr [ebp - 4]
0x499AC2: cmp      eax, dword ptr [edx + 0x1f8]
0x499AC8: jle      0x499ad8
0x499ACA: mov      eax, dword ptr [ebp - 4]
0x499ACD: inc      dword ptr [eax + 0x1f8]
0x499AD3: jmp      0x499f69
0x499AD8: mov      eax, dword ptr [ebp - 4]
0x499ADB: xor      edx, edx
0x499ADD: mov      dword ptr [eax + 0x1f8], edx
0x499AE3: mov      eax, dword ptr [ebp - 4]
0x499AE6: cmp      dword ptr [eax + 0x148], 0xb
0x499AED: je       0x499b1f
0x499AEF: mov      eax, dword ptr [ebp - 4]
0x499AF2: cmp      dword ptr [eax + 0x148], 9
0x499AF9: je       0x499b1f
0x499AFB: mov      eax, dword ptr [ebp - 4]
0x499AFE: cmp      dword ptr [eax + 0x148], 0xd
0x499B05: je       0x499b1f
0x499B07: mov      eax, dword ptr [ebp - 4]
0x499B0A: cmp      dword ptr [eax + 0x148], 6
0x499B11: je       0x499b1f
0x499B13: mov      eax, dword ptr [ebp - 4]
0x499B16: cmp      dword ptr [eax + 0x148], 7
0x499B1D: jne      0x499b5a
0x499B1F: mov      eax, dword ptr [ebp - 4]
0x499B22: mov      eax, dword ptr [eax + 0x1b8]
0x499B28: mov      edx, dword ptr [ebp - 4]
0x499B2B: sub      eax, dword ptr [edx + 0x1b0]
0x499B31: dec      eax
0x499B32: je       0x499b3b
0x499B34: sub      eax, 3
0x499B37: je       0x499b4b
0x499B39: jmp      0x499b5a
0x499B3B: mov      eax, dword ptr [ebp - 4]
0x499B3E: mov      eax, dword ptr [eax + 0x17c]
0x499B44: call     0x495794
0x499B49: jmp      0x499b5a
0x499B4B: mov      eax, dword ptr [ebp - 4]
0x499B4E: mov      eax, dword ptr [eax + 0x17c]
0x499B54: inc      eax
0x499B55: call     0x495794
0x499B5A: mov      byte ptr [ebp - 5], 0
0x499B5E: mov      eax, dword ptr [ebp - 4]
0x499B61: mov      byte ptr [eax + 0x1e4], 0
0x499B68: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x499B6D: mov      eax, dword ptr [eax]
0x499B6F: cmp      eax, dword ptr [ebp - 4]
0x499B72: je       0x499b8d
0x499B74: mov      eax, dword ptr [ebp - 4]
0x499B77: mov      eax, dword ptr [eax + 0x220]
0x499B7D: cmp      dword ptr [eax + 8], 2
0x499B81: jl       0x499b8d
0x499B83: mov      eax, dword ptr [ebp - 4]
0x499B86: mov      byte ptr [eax + 0x1e4], 1
0x499B8D: mov      eax, dword ptr [ebp - 4]
0x499B90: mov      eax, dword ptr [eax + 0x1b8]
0x499B96: mov      dword ptr [ebp - 0xc], eax
0x499B99: mov      eax, dword ptr [ebp - 4]
0x499B9C: cmp      dword ptr [eax + 0x148], 0xb
0x499BA3: je       0x499bcd
0x499BA5: mov      eax, dword ptr [ebp - 4]
0x499BA8: cmp      dword ptr [eax + 0x148], 0xd
0x499BAF: je       0x499bcd
0x499BB1: mov      eax, dword ptr [ebp - 4]
0x499BB4: cmp      dword ptr [eax + 0x148], 6
0x499BBB: je       0x499bcd
0x499BBD: mov      eax, dword ptr [ebp - 4]
0x499BC0: cmp      dword ptr [eax + 0x148], 7
0x499BC7: jne      0x499dcf
0x499BCD: mov      eax, dword ptr [ebp - 4]
0x499BD0: mov      eax, dword ptr [eax + 0x1b8]
0x499BD6: mov      edx, dword ptr [ebp - 4]
0x499BD9: cmp      eax, dword ptr [edx + 0x1b0]
0x499BDF: jl       0x499bf5
0x499BE1: mov      eax, dword ptr [ebp - 4]
0x499BE4: mov      eax, dword ptr [eax + 0x1b8]
0x499BEA: mov      edx, dword ptr [ebp - 4]
0x499BED: cmp      eax, dword ptr [edx + 0x1b4]
0x499BF3: jle      0x499c08
0x499BF5: mov      eax, dword ptr [ebp - 4]
0x499BF8: mov      eax, dword ptr [eax + 0x1b0]
0x499BFE: dec      eax
0x499BFF: mov      edx, dword ptr [ebp - 4]
0x499C02: mov      dword ptr [edx + 0x1b8], eax
0x499C08: mov      eax, dword ptr [ebp - 4]
0x499C0B: mov      eax, dword ptr [eax + 0x1b8]
0x499C11: mov      edx, dword ptr [ebp - 4]
0x499C14: cmp      eax, dword ptr [edx + 0x1b4]
0x499C1A: jge      0x499ca5
0x499C20: mov      eax, dword ptr [ebp - 4]
0x499C23: inc      dword ptr [eax + 0x1b8]
0x499C29: mov      eax, dword ptr [ebp - 4]
0x499C2C: cmp      byte ptr [eax + 0x1e4], 0
0x499C33: je       0x499c58
0x499C35: cmp      byte ptr [ebp - 0x16], 0
0x499C39: jne      0x499c58
0x499C3B: mov      eax, dword ptr [ebp - 4]
0x499C3E: mov      eax, dword ptr [eax + 0x1b8]
0x499C44: mov      edx, dword ptr [ebp - 4]
0x499C47: cmp      eax, dword ptr [edx + 0x1b4]
0x499C4D: jge      0x499c58
0x499C4F: mov      eax, dword ptr [ebp - 4]
0x499C52: inc      dword ptr [eax + 0x1b8]
0x499C58: mov      eax, dword ptr [ebp - 4]
0x499C5B: mov      eax, dword ptr [eax + 0x1b8]
0x499C61: mov      edx, dword ptr [ebp - 4]
0x499C64: sub      eax, dword ptr [edx + 0x1b0]
0x499C6A: inc      eax
0x499C6B: mov      dword ptr [ebp - 0x10], eax
0x499C6E: mov      eax, dword ptr [ebp - 4]
0x499C71: mov      eax, dword ptr [eax + 0x1b4]
0x499C77: mov      edx, dword ptr [ebp - 4]
0x499C7A: sub      eax, dword ptr [edx + 0x1b0]
0x499C80: inc      eax
0x499C81: mov      dword ptr [ebp - 0x14], eax
0x499C84: mov      eax, dword ptr [ebp - 0x10]
0x499C87: push     eax
0x499C88: mov      eax, dword ptr [ebp - 0x14]
0x499C8B: push     eax
0x499C8C: mov      eax, dword ptr [ebp - 4]
0x499C8F: xor      edx, edx
0x499C91: mov      dl, byte ptr [eax + 0xc]
0x499C94: mov      eax, dword ptr [ebp - 4]
0x499C97: mov      ecx, dword ptr [eax + 0x1e0]
0x499C9D: mov      eax, dword ptr [ebp - 4]
0x499CA0: call     0x497610
0x499CA5: mov      eax, dword ptr [ebp - 4]
0x499CA8: mov      eax, dword ptr [eax + 0x1b8]
0x499CAE: mov      edx, dword ptr [ebp - 4]
0x499CB1: cmp      eax, dword ptr [edx + 0x1b4]
0x499CB7: jl       0x499d21
0x499CB9: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x499CBE: mov      eax, dword ptr [eax]
0x499CC0: cmp      eax, dword ptr [ebp - 4]
0x499CC3: jne      0x499cfa
0x499CC5: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x499CCA: mov      eax, dword ptr [eax]
0x499CCC: call     0x466f1c
0x499CD1: test     al, al
0x499CD3: je       0x499d1d
0x499CD5: mov      eax, dword ptr [ebp - 4]
0x499CD8: xor      edx, edx
0x499CDA: mov      dword ptr [eax + 0x148], edx
0x499CE0: mov      eax, dword ptr [ebp - 4]
0x499CE3: mov      byte ptr [eax + 0x164], 1
0x499CEA: call     0x406154
0x499CEF: mov      edx, dword ptr [ebp - 4]
0x499CF2: mov      dword ptr [edx + 0x1fc], eax
0x499CF8: jmp      0x499d1d
0x499CFA: mov      eax, dword ptr [ebp - 4]
0x499CFD: xor      edx, edx
0x499CFF: mov      dword ptr [eax + 0x148], edx
0x499D05: mov      eax, dword ptr [ebp - 4]
0x499D08: mov      byte ptr [eax + 0x164], 1
0x499D0F: call     0x406154
0x499D14: mov      edx, dword ptr [ebp - 4]
0x499D17: mov      dword ptr [edx + 0x1fc], eax
0x499D1D: mov      byte ptr [ebp - 5], 1
0x499D21: mov      eax, dword ptr [ebp - 4]
0x499D24: cmp      dword ptr [eax + 0x148], 6
0x499D2B: jne      0x499d51
0x499D2D: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x499D32: mov      eax, dword ptr [eax]
0x499D34: cmp      eax, dword ptr [ebp - 4]
0x499D37: jne      0x499d51
0x499D39: call     0x406154
0x499D3E: mov      edx, dword ptr [0x4a3f90] ; *0x4A3F90->0x4F9460
0x499D44: mov      dword ptr [edx], eax
0x499D46: mov      eax, dword ptr [0x4a41a0] ; *0x4A41A0->0x4F9464
0x499D4B: mov      dword ptr [eax], 0x12c
0x499D51: mov      eax, dword ptr [ebp - 4]
0x499D54: cmp      dword ptr [eax + 0x148], 7
0x499D5B: jne      0x499dcb
0x499D5D: mov      eax, dword ptr [ebp - 4]
0x499D60: mov      eax, dword ptr [eax + 0x1b4]
0x499D66: sub      eax, 3
0x499D69: mov      edx, dword ptr [ebp - 4]
0x499D6C: cmp      eax, dword ptr [edx + 0x1b8]
0x499D72: jg       0x499dcb
0x499D74: mov      eax, dword ptr [ebp - 4]
0x499D77: mov      ax, word ptr [eax + 0x214]
0x499D7E: mov      edx, dword ptr [ebp - 4]
0x499D81: mov      word ptr [edx + 8], ax
0x499D85: mov      eax, dword ptr [ebp - 4]
0x499D88: mov      ax, word ptr [eax + 0x218]
0x499D8F: mov      edx, dword ptr [ebp - 4]
0x499D92: mov      word ptr [edx + 0xa], ax
0x499D96: mov      eax, dword ptr [ebp - 4]
0x499D99: movzx    eax, word ptr [eax + 8]
0x499D9D: mov      edx, dword ptr [ebp - 4]
0x499DA0: mov      dword ptr [edx + 0xb8], eax
0x499DA6: mov      eax, dword ptr [ebp - 4]
0x499DA9: movzx    eax, word ptr [eax + 0xa]
0x499DAD: mov      edx, dword ptr [ebp - 4]
0x499DB0: mov      dword ptr [edx + 0xbc], eax
0x499DB6: mov      eax, dword ptr [ebp - 4]
0x499DB9: xor      edx, edx
0x499DBB: mov      dword ptr [eax + 0x148], edx
0x499DC1: mov      eax, dword ptr [ebp - 4]
0x499DC4: mov      byte ptr [eax + 0x164], 1
0x499DCB: mov      byte ptr [ebp - 5], 1
0x499DCF: mov      eax, dword ptr [ebp - 4]
0x499DD2: cmp      dword ptr [eax + 0x148], 9
0x499DD9: jne      0x499f41
0x499DDF: mov      eax, dword ptr [ebp - 4]
0x499DE2: mov      eax, dword ptr [eax + 0x1b8]
0x499DE8: mov      edx, dword ptr [ebp - 4]
0x499DEB: cmp      eax, dword ptr [edx + 0x1b4]
0x499DF1: jg       0x499e07
0x499DF3: mov      eax, dword ptr [ebp - 4]
0x499DF6: mov      eax, dword ptr [eax + 0x1b8]
0x499DFC: mov      edx, dword ptr [ebp - 4]
0x499DFF: cmp      eax, dword ptr [edx + 0x1b0]
0x499E05: jge      0x499e1a
0x499E07: mov      eax, dword ptr [ebp - 4]
0x499E0A: mov      eax, dword ptr [eax + 0x1b4]
0x499E10: inc      eax
0x499E11: mov      edx, dword ptr [ebp - 4]
0x499E14: mov      dword ptr [edx + 0x1b8], eax
0x499E1A: mov      eax, dword ptr [ebp - 4]
0x499E1D: mov      eax, dword ptr [eax + 0x1b8]
0x499E23: mov      edx, dword ptr [ebp - 4]
0x499E26: cmp      eax, dword ptr [edx + 0x1b0]
0x499E2C: jle      0x499eb9
0x499E32: mov      eax, dword ptr [ebp - 4]
0x499E35: dec      dword ptr [eax + 0x1b8]
0x499E3B: mov      eax, dword ptr [ebp - 4]
0x499E3E: mov      al, byte ptr [eax + 0x1e4]
0x499E44: or       al, byte ptr [ebp - 0x15]
0x499E47: je       0x499e66
0x499E49: mov      eax, dword ptr [ebp - 4]
0x499E4C: mov      eax, dword ptr [eax + 0x1b8]
0x499E52: mov      edx, dword ptr [ebp - 4]
0x499E55: cmp      eax, dword ptr [edx + 0x1b0]
0x499E5B: jle      0x499e66
0x499E5D: mov      eax, dword ptr [ebp - 4]
0x499E60: dec      dword ptr [eax + 0x1b8]
0x499E66: mov      eax, dword ptr [ebp - 4]
0x499E69: mov      eax, dword ptr [eax + 0x1b4]
0x499E6F: mov      edx, dword ptr [ebp - 4]
0x499E72: sub      eax, dword ptr [edx + 0x1b8]
0x499E78: inc      eax
0x499E79: mov      dword ptr [ebp - 0x10], eax
0x499E7C: mov      eax, dword ptr [ebp - 4]
0x499E7F: mov      eax, dword ptr [eax + 0x1b4]
0x499E85: mov      edx, dword ptr [ebp - 4]
0x499E88: sub      eax, dword ptr [edx + 0x1b0]
0x499E8E: inc      eax
0x499E8F: mov      dword ptr [ebp - 0x14], eax
0x499E92: mov      eax, dword ptr [ebp - 0x10]
0x499E95: push     eax
0x499E96: mov      eax, dword ptr [ebp - 0x14]
0x499E99: push     eax
0x499E9A: mov      eax, dword ptr [ebp - 4]
0x499E9D: movzx    eax, byte ptr [eax + 0xc]
0x499EA1: call     0x4a023c
0x499EA6: mov      edx, eax
0x499EA8: mov      eax, dword ptr [ebp - 4]
0x499EAB: mov      ecx, dword ptr [eax + 0x1e0]
0x499EB1: mov      eax, dword ptr [ebp - 4]
0x499EB4: call     0x497610
0x499EB9: mov      eax, dword ptr [ebp - 4]
0x499EBC: mov      eax, dword ptr [eax + 0x1b8]
0x499EC2: mov      edx, dword ptr [ebp - 4]
0x499EC5: cmp      eax, dword ptr [edx + 0x1b0]
0x499ECB: jg       0x499f3d
0x499ECD: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x499ED2: mov      eax, dword ptr [eax]
0x499ED4: cmp      eax, dword ptr [ebp - 4]
0x499ED7: jne      0x499f16
0x499ED9: mov      eax, dword ptr [ebp - 4]
0x499EDC: xor      edx, edx
0x499EDE: mov      dword ptr [eax + 0x148], edx
0x499EE4: mov      eax, dword ptr [ebp - 4]
0x499EE7: mov      byte ptr [eax + 0x164], 1
0x499EEE: call     0x406154
0x499EF3: mov      edx, dword ptr [ebp - 4]
0x499EF6: mov      dword ptr [edx + 0x1fc], eax
0x499EFC: call     0x406154
0x499F01: mov      edx, dword ptr [0x4a3f90] ; *0x4A3F90->0x4F9460
0x499F07: mov      dword ptr [edx], eax
0x499F09: mov      eax, dword ptr [0x4a41a0] ; *0x4A41A0->0x4F9464
0x499F0E: mov      dword ptr [eax], 0x3e8
0x499F14: jmp      0x499f39
0x499F16: mov      eax, dword ptr [ebp - 4]
0x499F19: xor      edx, edx
0x499F1B: mov      dword ptr [eax + 0x148], edx
0x499F21: mov      eax, dword ptr [ebp - 4]
0x499F24: mov      byte ptr [eax + 0x164], 1
0x499F2B: call     0x406154
0x499F30: mov      edx, dword ptr [ebp - 4]
0x499F33: mov      dword ptr [edx + 0x1fc], eax
0x499F39: mov      byte ptr [ebp - 5], 1
0x499F3D: mov      byte ptr [ebp - 5], 1
0x499F41: mov      eax, dword ptr [ebp - 0xc]
0x499F44: mov      edx, dword ptr [ebp - 4]
0x499F47: cmp      eax, dword ptr [edx + 0x1b8]
0x499F4D: je       0x499f69
0x499F4F: call     0x406154
0x499F54: mov      edx, dword ptr [ebp - 4]
0x499F57: mov      dword ptr [edx + 0x204], eax
0x499F5D: mov      eax, dword ptr [ebp - 4]
0x499F60: mov      bx, 0xfff3
0x499F64: call     0x403080
0x499F69: mov      al, byte ptr [ebp - 5]
0x499F6C: pop      ebx
0x499F6D: mov      esp, ebp
0x499F6F: pop      ebp
0x499F70: ret      
0x499F71: lea      eax, [eax]
0x499F74: push     ebp
0x499F75: mov      ebp, esp
0x499F77: push     ecx
0x499F78: mov      dword ptr [ebp - 4], eax
0x499F7B: mov      eax, dword ptr [ebp - 4]
0x499F7E: xor      edx, edx
0x499F80: mov      dword ptr [eax + 0x148], edx
0x499F86: mov      eax, dword ptr [ebp - 4]
0x499F89: mov      byte ptr [eax + 0x164], 1
0x499F90: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x499F95: mov      eax, dword ptr [eax]
0x499F97: mov      edx, dword ptr [ebp - 4]
0x499F9A: mov      dx, word ptr [edx + 0x208]
0x499FA1: mov      word ptr [eax + 8], dx
0x499FA5: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x499FAA: mov      eax, dword ptr [eax]
0x499FAC: mov      edx, dword ptr [ebp - 4]
0x499FAF: mov      dx, word ptr [edx + 0x20c]
0x499FB6: mov      word ptr [eax + 0xa], dx
0x499FBA: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x499FBF: mov      eax, dword ptr [eax]
0x499FC1: mov      edx, dword ptr [ebp - 4]
0x499FC4: mov      dl, byte ptr [edx + 0x210]
0x499FCA: mov      byte ptr [eax + 0xc], dl
0x499FCD: mov      eax, dword ptr [ebp - 4]
0x499FD0: call     0x4967d0
0x499FD5: pop      ecx
0x499FD6: pop      ebp
0x499FD7: ret      
0x499FD8: push     ebp
0x499FD9: mov      ebp, esp
0x499FDB: add      esp, -8
0x499FDE: mov      dword ptr [ebp - 4], eax
0x499FE1: mov      byte ptr [ebp - 5], 0
0x499FE5: mov      eax, dword ptr [ebp - 4]
0x499FE8: cmp      dword ptr [eax + 0x148], 0xe
0x499FEF: jne      0x49a001
0x499FF1: mov      eax, dword ptr [ebp - 4]
0x499FF4: cmp      byte ptr [eax + 0xee], 0
0x499FFB: jne      0x49a001
0x499FFD: mov      byte ptr [ebp - 5], 1
0x49A001: mov      al, byte ptr [ebp - 5]
0x49A004: pop      ecx
0x49A005: pop      ecx
0x49A006: pop      ebp
0x49A007: ret      
0x49A008: push     ebp
0x49A009: mov      ebp, esp
0x49A00B: push     ecx
0x49A00C: mov      dword ptr [ebp - 4], eax
0x49A00F: mov      eax, dword ptr [ebp - 4]
0x49A012: xor      edx, edx
0x49A014: mov      dword ptr [eax + 0x148], edx
0x49A01A: mov      eax, dword ptr [ebp - 4]
0x49A01D: mov      byte ptr [eax + 0x164], 1
0x49A024: pop      ecx
0x49A025: pop      ebp
0x49A026: ret      
0x49A027: nop      
0x49A028: push     ebp
0x49A029: mov      ebp, esp
0x49A02B: add      esp, -0xc
0x49A02E: mov      dword ptr [ebp - 0xc], ecx
0x49A031: mov      dword ptr [ebp - 8], edx
0x49A034: mov      dword ptr [ebp - 4], eax
0x49A037: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49A03C: mov      eax, dword ptr [eax]
0x49A03E: mov      dx, word ptr [ebp - 8]
0x49A042: mov      word ptr [eax + 8], dx
0x49A046: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49A04B: mov      eax, dword ptr [eax]
0x49A04D: mov      dx, word ptr [ebp - 0xc]
0x49A051: mov      word ptr [eax + 0xa], dx
0x49A055: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49A05A: mov      eax, dword ptr [eax]
0x49A05C: mov      edx, dword ptr [ebp - 8]
0x49A05F: mov      dword ptr [eax + 0xb8], edx
0x49A065: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49A06A: mov      eax, dword ptr [eax]
0x49A06C: mov      edx, dword ptr [ebp - 0xc]
0x49A06F: mov      dword ptr [eax + 0xbc], edx
0x49A075: mov      eax, dword ptr [ebp - 8]
0x49A078: mov      edx, dword ptr [ebp - 4]
0x49A07B: mov      dword ptr [edx + 0x208], eax
0x49A081: mov      eax, dword ptr [ebp - 0xc]
0x49A084: mov      edx, dword ptr [ebp - 4]
0x49A087: mov      dword ptr [edx + 0x20c], eax
0x49A08D: mov      eax, dword ptr [ebp - 4]
0x49A090: xor      edx, edx
0x49A092: mov      dword ptr [eax + 0x148], edx
0x49A098: mov      eax, dword ptr [ebp - 4]
0x49A09B: mov      dword ptr [eax + 0x1b8], 0xffffffff
0x49A0A5: mov      eax, dword ptr [ebp - 4]
0x49A0A8: call     0x4967d0
0x49A0AD: mov      esp, ebp
0x49A0AF: pop      ebp
0x49A0B0: ret      
0x49A0B1: lea      eax, [eax]
0x49A0B4: push     ebp
0x49A0B5: mov      ebp, esp
0x49A0B7: add      esp, -0x28
0x49A0BA: xor      ecx, ecx
0x49A0BC: mov      dword ptr [ebp - 0x28], ecx
0x49A0BF: mov      dword ptr [ebp - 0x24], ecx
0x49A0C2: mov      dword ptr [ebp - 0x1c], ecx
0x49A0C5: mov      dword ptr [ebp - 8], edx
0x49A0C8: mov      dword ptr [ebp - 4], eax
0x49A0CB: mov      eax, dword ptr [ebp - 8]
0x49A0CE: call     0x403e88
0x49A0D3: xor      eax, eax
0x49A0D5: push     ebp
0x49A0D6: push     0x49a2a9
0x49A0DB: push     dword ptr fs:[eax]
0x49A0DE: mov      dword ptr fs:[eax], esp
0x49A0E1: call     0x406154
0x49A0E6: mov      edx, dword ptr [ebp - 4]
0x49A0E9: mov      dword ptr [edx + 0x88], eax
0x49A0EF: mov      eax, dword ptr [ebp - 4]
0x49A0F2: xor      edx, edx
0x49A0F4: mov      dword ptr [eax + 0x94], edx
0x49A0FA: xor      eax, eax
0x49A0FC: mov      dword ptr [ebp - 0x18], eax
0x49A0FF: mov      byte ptr [ebp - 0x1d], 1
0x49A103: jmp      0x49a274
0x49A108: lea      eax, [ebp - 0x1c]
0x49A10B: call     0x403a58
0x49A110: mov      dword ptr [ebp - 0xc], 1
0x49A117: mov      eax, dword ptr [ebp - 8]
0x49A11A: call     0x403cd4
0x49A11F: mov      dword ptr [ebp - 0x10], eax
0x49A122: mov      eax, dword ptr [ebp - 0xc]
0x49A125: cmp      eax, dword ptr [ebp - 0x10]
0x49A128: jle      0x49a133
0x49A12A: mov      byte ptr [ebp - 0x1d], 0
0x49A12E: jmp      0x49a22c
0x49A133: mov      eax, dword ptr [ebp - 8]
0x49A136: mov      edx, dword ptr [ebp - 0xc]
0x49A139: cmp      byte ptr [eax + edx - 1], 0x80
0x49A13E: jb       0x49a190
0x49A140: lea      eax, [ebp - 0x28]
0x49A143: mov      edx, dword ptr [ebp - 8]
0x49A146: mov      ecx, dword ptr [ebp - 0xc]
0x49A149: mov      dl, byte ptr [edx + ecx - 1]
0x49A14D: call     0x403bfc
0x49A152: mov      edx, dword ptr [ebp - 0x28]
0x49A155: lea      eax, [ebp - 0x1c]
0x49A158: call     0x403cdc
0x49A15D: inc      dword ptr [ebp - 0xc]
0x49A160: mov      eax, dword ptr [ebp - 0xc]
0x49A163: cmp      eax, dword ptr [ebp - 0x10]
0x49A166: jg       0x49a187
0x49A168: lea      eax, [ebp - 0x28]
0x49A16B: mov      edx, dword ptr [ebp - 8]
0x49A16E: mov      ecx, dword ptr [ebp - 0xc]
0x49A171: mov      dl, byte ptr [edx + ecx - 1]
0x49A175: call     0x403bfc
0x49A17A: mov      edx, dword ptr [ebp - 0x28]
0x49A17D: lea      eax, [ebp - 0x1c]
0x49A180: call     0x403cdc
0x49A185: jmp      0x49a1ad
0x49A187: mov      byte ptr [ebp - 0x1d], 0
0x49A18B: jmp      0x49a22c
0x49A190: lea      eax, [ebp - 0x28]
0x49A193: mov      edx, dword ptr [ebp - 8]
0x49A196: mov      ecx, dword ptr [ebp - 0xc]
0x49A199: mov      dl, byte ptr [edx + ecx - 1]
0x49A19D: call     0x403bfc
0x49A1A2: mov      edx, dword ptr [ebp - 0x28]
0x49A1A5: lea      eax, [ebp - 0x1c]
0x49A1A8: call     0x403cdc
0x49A1AD: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x49A1B2: mov      eax, dword ptr [eax]
0x49A1B4: call     0x43e1f0
0x49A1B9: mov      edx, dword ptr [ebp - 0x1c]
0x49A1BC: call     0x4181ac
0x49A1C1: mov      dword ptr [ebp - 0x14], eax
0x49A1C4: cmp      dword ptr [ebp - 0x14], 0x96
0x49A1CB: jle      0x49a224
0x49A1CD: mov      eax, dword ptr [ebp - 4]
0x49A1D0: mov      edx, dword ptr [ebp - 0x18]
0x49A1D3: lea      eax, [eax + edx*4 + 0x60]
0x49A1D7: mov      edx, dword ptr [ebp - 0x1c]
0x49A1DA: call     0x403aac
0x49A1DF: mov      eax, dword ptr [ebp - 4]
0x49A1E2: mov      edx, dword ptr [ebp - 0x18]
0x49A1E5: mov      ecx, dword ptr [ebp - 0x14]
0x49A1E8: mov      dword ptr [eax + edx*4 + 0x74], ecx
0x49A1EC: mov      eax, dword ptr [ebp - 4]
0x49A1EF: inc      dword ptr [eax + 0x94]
0x49A1F5: inc      dword ptr [ebp - 0x18]
0x49A1F8: cmp      dword ptr [ebp - 0x18], 5
0x49A1FC: jl       0x49a204
0x49A1FE: mov      byte ptr [ebp - 0x1d], 0
0x49A202: jmp      0x49a22c
0x49A204: lea      eax, [ebp - 8]
0x49A207: push     eax
0x49A208: mov      ecx, dword ptr [ebp - 0x10]
0x49A20B: sub      ecx, dword ptr [ebp - 0xc]
0x49A20E: mov      edx, dword ptr [ebp - 0xc]
0x49A211: inc      edx
0x49A212: mov      eax, dword ptr [ebp - 8]
0x49A215: call     0x403ed8
0x49A21A: lea      eax, [ebp - 0x1c]
0x49A21D: call     0x403a58
0x49A222: jmp      0x49a22c
0x49A224: inc      dword ptr [ebp - 0xc]
0x49A227: jmp      0x49a122
0x49A22C: cmp      dword ptr [ebp - 0x1c], 0
0x49A230: je       0x49a274
0x49A232: cmp      dword ptr [ebp - 0x18], 0x96
0x49A239: jge      0x49a274
0x49A23B: mov      eax, dword ptr [ebp - 4]
0x49A23E: mov      edx, dword ptr [ebp - 0x18]
0x49A241: lea      eax, [eax + edx*4 + 0x60]
0x49A245: mov      edx, dword ptr [ebp - 0x1c]
0x49A248: call     0x403aac
0x49A24D: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x49A252: mov      eax, dword ptr [eax]
0x49A254: call     0x43e1f0
0x49A259: mov      edx, dword ptr [ebp - 0x1c]
0x49A25C: call     0x4181ac
0x49A261: mov      edx, dword ptr [ebp - 4]
0x49A264: mov      ecx, dword ptr [ebp - 0x18]
0x49A267: mov      dword ptr [edx + ecx*4 + 0x74], eax
0x49A26B: mov      eax, dword ptr [ebp - 4]
0x49A26E: inc      dword ptr [eax + 0x94]
0x49A274: cmp      byte ptr [ebp - 0x1d], 0
0x49A278: jne      0x49a108
0x49A27E: xor      eax, eax
0x49A280: pop      edx
0x49A281: pop      ecx
0x49A282: pop      ecx
0x49A283: mov      dword ptr fs:[eax], edx
0x49A286: push     0x49a2b0
0x49A28B: lea      eax, [ebp - 0x28]
0x49A28E: mov      edx, 2
0x49A293: call     0x403a7c
0x49A298: lea      eax, [ebp - 0x1c]
0x49A29B: call     0x403a58
0x49A2A0: lea      eax, [ebp - 8]
0x49A2A3: call     0x403a58
0x49A2A8: ret      
0x49A2A9: jmp      0x403518
0x49A2AE: jmp      0x49a28b
0x49A2B0: mov      esp, ebp
0x49A2B2: pop      ebp
0x49A2B3: ret      

; === TNpcActor.DMT_-1 @ 0x49A2B4 ===
0x49A2B4: push     ebp
0x49A2B5: mov      ebp, esp
0x49A2B7: add      esp, -0xc
0x49A2BA: mov      dword ptr [ebp - 4], eax
0x49A2BD: mov      eax, dword ptr [ebp - 4]
0x49A2C0: mov      byte ptr [eax + 0xec], 0
0x49A2C7: mov      eax, dword ptr [ebp - 4]
0x49A2CA: mov      dword ptr [eax + 0x1b8], 0xffffffff
0x49A2D4: mov      eax, dword ptr [ebp - 4]
0x49A2D7: movzx    eax, word ptr [eax + 0x14]
0x49A2DB: call     0x496290
0x49A2E0: mov      edx, dword ptr [ebp - 4]
0x49A2E3: mov      dword ptr [edx + 0xe0], eax
0x49A2E9: mov      eax, dword ptr [ebp - 4]
0x49A2EC: movzx    edx, word ptr [eax + 0x14]
0x49A2F0: mov      eax, dword ptr [ebp - 4]
0x49A2F3: movzx    eax, byte ptr [eax + 0xe]
0x49A2F7: call     0x495ca8
0x49A2FC: mov      dword ptr [ebp - 8], eax
0x49A2FF: cmp      dword ptr [ebp - 8], 0
0x49A303: je       0x49a428
0x49A309: mov      eax, dword ptr [ebp - 4]
0x49A30C: movzx    eax, byte ptr [eax + 0xc]
0x49A310: mov      ecx, 3
0x49A315: xor      edx, edx
0x49A317: div      ecx
0x49A319: mov      eax, dword ptr [ebp - 4]
0x49A31C: mov      byte ptr [eax + 0xc], dl
0x49A31F: mov      eax, dword ptr [ebp - 4]
0x49A322: mov      eax, dword ptr [eax + 0x148]
0x49A328: sub      eax, 0xa
0x49A32B: je       0x49a33b
0x49A32D: sub      eax, 4
0x49A330: je       0x49a3c5
0x49A336: jmp      0x49a428
0x49A33B: mov      eax, dword ptr [ebp - 8]
0x49A33E: movzx    eax, word ptr [eax + 2]
0x49A342: mov      edx, dword ptr [ebp - 8]
0x49A345: movzx    edx, word ptr [edx + 4]
0x49A349: add      eax, edx
0x49A34B: mov      edx, dword ptr [ebp - 4]
0x49A34E: movzx    edx, byte ptr [edx + 0xc]
0x49A352: imul     edx
0x49A354: mov      edx, dword ptr [ebp - 8]
0x49A357: movzx    edx, word ptr [edx]
0x49A35A: add      eax, edx
0x49A35C: mov      edx, dword ptr [ebp - 4]
0x49A35F: mov      dword ptr [edx + 0x1b0], eax
0x49A365: mov      eax, dword ptr [ebp - 8]
0x49A368: movzx    eax, word ptr [eax + 2]
0x49A36C: mov      edx, dword ptr [ebp - 4]
0x49A36F: add      eax, dword ptr [edx + 0x1b0]
0x49A375: dec      eax
0x49A376: mov      edx, dword ptr [ebp - 4]
0x49A379: mov      dword ptr [edx + 0x1b4], eax
0x49A37F: mov      eax, dword ptr [ebp - 8]
0x49A382: movzx    eax, word ptr [eax + 6]
0x49A386: mov      edx, dword ptr [ebp - 4]
0x49A389: mov      dword ptr [edx + 0x1d0], eax
0x49A38F: call     0x406154
0x49A394: mov      edx, dword ptr [ebp - 4]
0x49A397: mov      dword ptr [edx + 0x1d4], eax
0x49A39D: mov      eax, dword ptr [ebp - 8]
0x49A3A0: movzx    eax, word ptr [eax + 2]
0x49A3A4: mov      edx, dword ptr [ebp - 4]
0x49A3A7: mov      dword ptr [edx + 0x1f4], eax
0x49A3AD: push     0
0x49A3AF: push     1
0x49A3B1: mov      eax, dword ptr [ebp - 4]
0x49A3B4: xor      edx, edx
0x49A3B6: mov      dl, byte ptr [eax + 0xc]
0x49A3B9: xor      ecx, ecx
0x49A3BB: mov      eax, dword ptr [ebp - 4]
0x49A3BE: call     0x497610
0x49A3C3: jmp      0x49a428
0x49A3C5: mov      eax, dword ptr [ebp - 8]
0x49A3C8: movzx    eax, word ptr [eax + 0x16]
0x49A3CC: mov      edx, dword ptr [ebp - 8]
0x49A3CF: movzx    edx, word ptr [edx + 0x18]
0x49A3D3: add      eax, edx
0x49A3D5: mov      edx, dword ptr [ebp - 4]
0x49A3D8: movzx    edx, byte ptr [edx + 0xc]
0x49A3DC: imul     edx
0x49A3DE: mov      edx, dword ptr [ebp - 8]
0x49A3E1: movzx    edx, word ptr [edx + 0x14]
0x49A3E5: add      eax, edx
0x49A3E7: mov      edx, dword ptr [ebp - 4]
0x49A3EA: mov      dword ptr [edx + 0x1b0], eax
0x49A3F0: mov      eax, dword ptr [ebp - 8]
0x49A3F3: movzx    eax, word ptr [eax + 0x16]
0x49A3F7: mov      edx, dword ptr [ebp - 4]
0x49A3FA: add      eax, dword ptr [edx + 0x1b0]
0x49A400: dec      eax
0x49A401: mov      edx, dword ptr [ebp - 4]
0x49A404: mov      dword ptr [edx + 0x1b4], eax
0x49A40A: mov      eax, dword ptr [ebp - 8]
0x49A40D: movzx    eax, word ptr [eax + 0x1a]
0x49A411: mov      edx, dword ptr [ebp - 4]
0x49A414: mov      dword ptr [edx + 0x1d0], eax
0x49A41A: call     0x406154
0x49A41F: mov      edx, dword ptr [ebp - 4]
0x49A422: mov      dword ptr [edx + 0x1d4], eax
0x49A428: mov      esp, ebp
0x49A42A: pop      ebp
0x49A42B: ret      

; === TNpcActor.DMT_-3 @ 0x49A42C ===
0x49A42C: push     ebp
0x49A42D: mov      ebp, esp
0x49A42F: add      esp, -0x18
0x49A432: mov      byte ptr [ebp - 0x11], dl
0x49A435: mov      dword ptr [ebp - 4], eax
0x49A438: mov      eax, dword ptr [ebp - 4]
0x49A43B: movzx    edx, word ptr [eax + 0x14]
0x49A43F: mov      eax, dword ptr [ebp - 4]
0x49A442: movzx    eax, byte ptr [eax + 0xe]
0x49A446: call     0x495ca8
0x49A44B: mov      dword ptr [ebp - 0x10], eax
0x49A44E: cmp      dword ptr [ebp - 0x10], 0
0x49A452: je       0x49a4c9
0x49A454: mov      eax, dword ptr [ebp - 4]
0x49A457: movzx    eax, byte ptr [eax + 0xc]
0x49A45B: mov      ecx, 3
0x49A460: xor      edx, edx
0x49A462: div      ecx
0x49A464: mov      eax, dword ptr [ebp - 4]
0x49A467: mov      byte ptr [eax + 0xc], dl
0x49A46A: mov      eax, dword ptr [ebp - 4]
0x49A46D: cmp      dword ptr [eax + 0x1ec], 0
0x49A474: jge      0x49a47d
0x49A476: xor      eax, eax
0x49A478: mov      dword ptr [ebp - 0xc], eax
0x49A47B: jmp      0x49a4a2
0x49A47D: mov      eax, dword ptr [ebp - 0x10]
0x49A480: movzx    eax, word ptr [eax + 2]
0x49A484: mov      edx, dword ptr [ebp - 4]
0x49A487: cmp      eax, dword ptr [edx + 0x1ec]
0x49A48D: jg       0x49a496
0x49A48F: xor      eax, eax
0x49A491: mov      dword ptr [ebp - 0xc], eax
0x49A494: jmp      0x49a4a2
0x49A496: mov      eax, dword ptr [ebp - 4]
0x49A499: mov      eax, dword ptr [eax + 0x1ec]
0x49A49F: mov      dword ptr [ebp - 0xc], eax
0x49A4A2: mov      eax, dword ptr [ebp - 0x10]
0x49A4A5: movzx    eax, word ptr [eax + 2]
0x49A4A9: mov      edx, dword ptr [ebp - 0x10]
0x49A4AC: movzx    edx, word ptr [edx + 4]
0x49A4B0: add      eax, edx
0x49A4B2: mov      edx, dword ptr [ebp - 4]
0x49A4B5: movzx    edx, byte ptr [edx + 0xc]
0x49A4B9: imul     edx
0x49A4BB: mov      edx, dword ptr [ebp - 0x10]
0x49A4BE: movzx    edx, word ptr [edx]
0x49A4C1: add      eax, edx
0x49A4C3: add      eax, dword ptr [ebp - 0xc]
0x49A4C6: mov      dword ptr [ebp - 8], eax
0x49A4C9: mov      eax, dword ptr [ebp - 8]
0x49A4CC: mov      esp, ebp
0x49A4CE: pop      ebp
0x49A4CF: ret      

; === TNpcActor.DMT_-13 @ 0x49A4D0 ===
0x49A4D0: push     ebp
0x49A4D1: mov      ebp, esp
0x49A4D3: push     ecx
0x49A4D4: mov      dword ptr [ebp - 4], eax
0x49A4D7: mov      eax, dword ptr [ebp - 4]
0x49A4DA: mov      al, byte ptr [eax + 0xe]
0x49A4DD: sub      al, 0x32
0x49A4DF: jne      0x49a520
0x49A4E1: mov      eax, dword ptr [ebp - 4]
0x49A4E4: add      eax, 0xac
0x49A4E9: push     eax
0x49A4EA: mov      eax, dword ptr [ebp - 4]
0x49A4ED: lea      ecx, [eax + 0xa0]
0x49A4F3: mov      eax, dword ptr [ebp - 4]
0x49A4F6: mov      edx, dword ptr [eax + 0xe0]
0x49A4FC: mov      eax, dword ptr [ebp - 4]
0x49A4FF: add      edx, dword ptr [eax + 0x1b8]
0x49A505: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x49A50A: mov      eax, dword ptr [eax]
0x49A50C: mov      eax, dword ptr [eax + 0x344]
0x49A512: call     0x453a48
0x49A517: mov      edx, dword ptr [ebp - 4]
0x49A51A: mov      dword ptr [edx + 0x140], eax
0x49A520: pop      ecx
0x49A521: pop      ebp
0x49A522: ret      
0x49A523: nop      
0x49A524: push     ebp
0x49A525: mov      ebp, esp
0x49A527: push     ecx
0x49A528: mov      dword ptr [ebp - 4], eax
0x49A52B: mov      eax, dword ptr [ebp - 4]
0x49A52E: call     0x4994fc ; TActor.DMT_-6
0x49A533: pop      ecx
0x49A534: pop      ebp
0x49A535: ret      
0x49A536: mov      eax, eax
0x49A538: push     ebp
0x49A539: mov      ebp, esp
0x49A53B: add      esp, -8
0x49A53E: test     dl, dl
0x49A540: je       0x49a54a
0x49A542: add      esp, -0x10
0x49A545: call     0x4031a4
0x49A54A: mov      byte ptr [ebp - 5], dl
0x49A54D: mov      dword ptr [ebp - 4], eax
0x49A550: xor      edx, edx
0x49A552: mov      eax, dword ptr [ebp - 4]
0x49A555: call     0x4962d0 ; TActor.DMT_-4
0x49A55A: mov      eax, dword ptr [ebp - 4]
0x49A55D: xor      edx, edx
0x49A55F: mov      dword ptr [eax + 0x244], edx
0x49A565: mov      eax, dword ptr [ebp - 4]
0x49A568: xor      edx, edx
0x49A56A: mov      dword ptr [eax + 0x248], edx
0x49A570: mov      eax, dword ptr [ebp - 4]
0x49A573: mov      byte ptr [eax + 0x24c], 0
0x49A57A: mov      eax, dword ptr [ebp - 4]
0x49A57D: cmp      byte ptr [ebp - 5], 0
0x49A581: je       0x49a592
0x49A583: call     0x4031fc
0x49A588: pop      dword ptr fs:[0]
0x49A58F: add      esp, 0xc
0x49A592: mov      eax, dword ptr [ebp - 4]
0x49A595: pop      ecx
0x49A596: pop      ecx
0x49A597: pop      ebp
0x49A598: ret      
0x49A599: lea      eax, [eax]
0x49A59C: push     ebp
0x49A59D: mov      ebp, esp
0x49A59F: add      esp, -8
0x49A5A2: call     0x403204
0x49A5A7: mov      byte ptr [ebp - 5], dl
0x49A5AA: mov      dword ptr [ebp - 4], eax
0x49A5AD: mov      dl, byte ptr [ebp - 5]
0x49A5B0: and      dl, 0xfc
0x49A5B3: mov      eax, dword ptr [ebp - 4]
0x49A5B6: call     0x496508 ; TSkeletonOma.Destroy
0x49A5BB: cmp      byte ptr [ebp - 5], 0
0x49A5BF: jle      0x49a5c9
0x49A5C1: mov      eax, dword ptr [ebp - 4]
0x49A5C4: call     0x4031f4
0x49A5C9: pop      ecx
0x49A5CA: pop      ecx
0x49A5CB: pop      ebp
0x49A5CC: ret      
0x49A5CD: lea      eax, [eax]
0x49A5D0: push     ebp
0x49A5D1: mov      ebp, esp
0x49A5D3: add      esp, -8
0x49A5D6: mov      dword ptr [ebp - 4], eax
0x49A5D9: mov      eax, dword ptr [ebp - 4]
0x49A5DC: mov      byte ptr [eax + 0xec], 0
0x49A5E3: mov      eax, dword ptr [ebp - 4]
0x49A5E6: mov      byte ptr [eax + 0xed], 0
0x49A5ED: mov      eax, dword ptr [ebp - 4]
0x49A5F0: mov      dword ptr [eax + 0x1b8], 0xffffffff
0x49A5FA: mov      eax, dword ptr [ebp - 4]
0x49A5FD: mov      eax, dword ptr [eax + 0x18]
0x49A600: call     0x458ce0
0x49A605: mov      edx, dword ptr [ebp - 4]
0x49A608: mov      byte ptr [edx + 0xf], al
0x49A60B: mov      eax, dword ptr [ebp - 4]
0x49A60E: mov      eax, dword ptr [eax + 0x18]
0x49A611: call     0x458cfc
0x49A616: mov      edx, dword ptr [ebp - 4]
0x49A619: mov      byte ptr [edx + 0x10], al
0x49A61C: mov      eax, dword ptr [ebp - 4]
0x49A61F: mov      eax, dword ptr [eax + 0x18]
0x49A622: call     0x458cc4
0x49A627: mov      edx, dword ptr [ebp - 4]
0x49A62A: mov      byte ptr [edx + 0x11], al
0x49A62D: mov      eax, dword ptr [ebp - 4]
0x49A630: movzx    eax, byte ptr [eax + 0x10]
0x49A634: imul     eax, eax, 0x258
0x49A63A: mov      edx, dword ptr [ebp - 4]
0x49A63D: mov      dword ptr [edx + 0xe0], eax
0x49A643: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x49A648: mov      eax, dword ptr [eax]
0x49A64A: mov      eax, dword ptr [eax + 0x320]
0x49A650: mov      eax, dword ptr [eax + 0x28]
0x49A653: mov      ecx, 0x258
0x49A658: cdq      
0x49A659: idiv     ecx
0x49A65B: sar      eax, 1
0x49A65D: jns      0x49a662
0x49A65F: adc      eax, 0
0x49A662: mov      dword ptr [ebp - 8], eax
0x49A665: mov      eax, dword ptr [ebp - 4]
0x49A668: movzx    eax, byte ptr [eax + 0xf]
0x49A66C: mov      edx, dword ptr [ebp - 8]
0x49A66F: dec      edx
0x49A670: cmp      eax, edx
0x49A672: jle      0x49a67e
0x49A674: mov      al, byte ptr [ebp - 8]
0x49A677: dec      eax
0x49A678: mov      edx, dword ptr [ebp - 4]
0x49A67B: mov      byte ptr [edx + 0xf], al
0x49A67E: mov      eax, dword ptr [ebp - 4]
0x49A681: mov      al, byte ptr [eax + 0xf]
0x49A684: add      eax, eax
0x49A686: mov      edx, dword ptr [ebp - 4]
0x49A689: mov      byte ptr [edx + 0xf], al
0x49A68C: mov      eax, dword ptr [ebp - 4]
0x49A68F: cmp      byte ptr [eax + 0xf], 1
0x49A693: jbe      0x49a6b6
0x49A695: mov      eax, dword ptr [ebp - 4]
0x49A698: movzx    eax, byte ptr [eax + 0xf]
0x49A69C: mov      edx, dword ptr [ebp - 4]
0x49A69F: movzx    edx, byte ptr [edx + 0xd]
0x49A6A3: add      eax, edx
0x49A6A5: imul     eax, eax, 0x258
0x49A6AB: mov      edx, dword ptr [ebp - 4]
0x49A6AE: mov      dword ptr [edx + 0xe4], eax
0x49A6B4: jmp      0x49a6c3
0x49A6B6: mov      eax, dword ptr [ebp - 4]
0x49A6B9: mov      dword ptr [eax + 0xe4], 0xffffffff
0x49A6C3: mov      eax, dword ptr [ebp - 4]
0x49A6C6: movzx    eax, byte ptr [eax + 0x11]
0x49A6CA: imul     eax, eax, 0x258
0x49A6D0: mov      edx, dword ptr [ebp - 4]
0x49A6D3: mov      dword ptr [edx + 0xe8], eax
0x49A6D9: mov      eax, dword ptr [ebp - 4]
0x49A6DC: mov      eax, dword ptr [eax + 0x148]
0x49A6E2: add      eax, -5
0x49A6E5: cmp      eax, 0x1d
0x49A6E8: ja       0x49b1e4
0x49A6EE: mov      al, byte ptr [eax + 0x49a6fb]
0x49A6F4: jmp      dword ptr [eax*4 + 0x49a719]
0x49A6FB: pop      es
0x49A6FC: add      eax, dword ptr [eax + ecx]
0x49A6FF: add      al, byte ptr [ecx]
0x49A701: add      al, byte ptr [0xa090806]
0x49A707: or       ecx, dword ptr [eax]
0x49A709: or       byte ptr [eax], al
0x49A70B: add      byte ptr [eax], al
0x49A70D: add      byte ptr [eax], cl
0x49A70F: add      byte ptr [eax], al
0x49A711: add      byte ptr [eax], al
0x49A713: add      byte ptr [eax], al
0x49A715: or       al, 0
0x49A717: add      byte ptr [0x49b1e4], cl
0x49A71D: push     ecx
0x49A71E: cmpsd    dword ptr [esi], dword ptr es:[edi]
0x49A71F: dec      ecx
0x49A720: add      cl, dh
0x49A722: cmpsd    dword ptr [esi], dword ptr es:[edi]
0x49A723: dec      ecx
0x49A724: add      dh, dh
0x49A726: test     al, 0x49
0x49A728: add      byte ptr [edx + ebp*4 - 0x54acffb7], dl
0x49A72F: dec      ecx
0x49A730: add      byte ptr [ebx - 0x6affb655], bh
0x49A736: lodsb    al, byte ptr [esi]
0x49A737: dec      ecx
0x49A738: add      byte ptr [0x8b0049ad], dh
0x49A73E: scasb    al, byte ptr es:[edi]
0x49A73F: dec      ecx
0x49A740: add      byte ptr [ecx], ah
0x49A742: scasd    eax, dword ptr es:[edi]
0x49A743: dec      ecx
0x49A744: add      byte ptr [edi - 0x14ffb651], dh
0x49A74A: mov      al, 0x49
0x49A74C: add      byte ptr [ecx + 0xf0049b1], al
0x49A752: mov      bh, 5
0x49A754: pop      edx
0x49A755: xor      byte ptr [edx], cl
0x49A758: movzx    edx, word ptr [0x4a305c] ; *0x4A305C->0xC80004
0x49A75F: add      eax, edx
0x49A761: mov      edx, dword ptr [ebp - 4]
0x49A764: movzx    edx, byte ptr [edx + 0xc]
0x49A768: imul     edx
0x49A76A: movzx    edx, word ptr [0x4a3058] ; *0x4A3058->0x040000
0x49A771: add      eax, edx
0x49A773: mov      edx, dword ptr [ebp - 4]
0x49A776: mov      dword ptr [edx + 0x1b0], eax
0x49A77C: movzx    eax, word ptr [0x4a305a] ; *0x4A305A->0x040004
0x49A783: mov      edx, dword ptr [ebp - 4]
0x49A786: add      eax, dword ptr [edx + 0x1b0]
0x49A78C: dec      eax
0x49A78D: mov      edx, dword ptr [ebp - 4]
0x49A790: mov      dword ptr [edx + 0x1b4], eax
0x49A796: movzx    eax, word ptr [0x4a305e] ; *0x4A305E->0x0000C8
0x49A79D: mov      edx, dword ptr [ebp - 4]
0x49A7A0: mov      dword ptr [edx + 0x1d0], eax
0x49A7A6: call     0x406154
0x49A7AB: mov      edx, dword ptr [ebp - 4]
0x49A7AE: mov      dword ptr [edx + 0x1d4], eax
0x49A7B4: movzx    eax, word ptr [0x4a305a] ; *0x4A305A->0x040004
0x49A7BB: mov      edx, dword ptr [ebp - 4]
0x49A7BE: mov      dword ptr [edx + 0x1f4], eax
0x49A7C4: push     0
0x49A7C6: mov      eax, dword ptr [ebp - 4]
0x49A7C9: mov      eax, dword ptr [eax + 0x1b4]
0x49A7CF: mov      edx, dword ptr [ebp - 4]
0x49A7D2: sub      eax, dword ptr [edx + 0x1b0]
0x49A7D8: inc      eax
0x49A7D9: push     eax
0x49A7DA: mov      eax, dword ptr [ebp - 4]
0x49A7DD: xor      edx, edx
0x49A7DF: mov      dl, byte ptr [eax + 0xc]
0x49A7E2: xor      ecx, ecx
0x49A7E4: mov      eax, dword ptr [ebp - 4]
0x49A7E7: call     0x497610
0x49A7EC: jmp      0x49b1e4
0x49A7F1: movzx    eax, word ptr [0x4a3064] ; *0x4A3064->0x020006
0x49A7F8: movzx    edx, word ptr [0x4a3066] ; *0x4A3066->0x5A0002
0x49A7FF: add      eax, edx
0x49A801: mov      edx, dword ptr [ebp - 4]
0x49A804: movzx    edx, byte ptr [edx + 0xc]
0x49A808: imul     edx
0x49A80A: movzx    edx, word ptr [0x4a3062] ; *0x4A3062->0x060040
0x49A811: add      eax, edx
0x49A813: mov      edx, dword ptr [ebp - 4]
0x49A816: mov      dword ptr [edx + 0x1b0], eax
0x49A81C: movzx    eax, word ptr [0x4a3064] ; *0x4A3064->0x020006
0x49A823: mov      edx, dword ptr [ebp - 4]
0x49A826: add      eax, dword ptr [edx + 0x1b0]
0x49A82C: dec      eax
0x49A82D: mov      edx, dword ptr [ebp - 4]
0x49A830: mov      dword ptr [edx + 0x1b4], eax
0x49A836: movzx    eax, word ptr [0x4a3068] ; *0x4A3068->0x02005A
0x49A83D: mov      edx, dword ptr [ebp - 4]
0x49A840: mov      dword ptr [edx + 0x1d0], eax
0x49A846: call     0x406154
0x49A84B: mov      edx, dword ptr [ebp - 4]
0x49A84E: mov      dword ptr [edx + 0x1d4], eax
0x49A854: xor      eax, eax
0x49A856: mov      al, byte ptr [0x4a306a] ; *0x4A306A->0x800002
0x49A85B: mov      edx, dword ptr [ebp - 4]
0x49A85E: mov      dword ptr [edx + 0x1d8], eax
0x49A864: mov      eax, dword ptr [ebp - 4]
0x49A867: xor      edx, edx
0x49A869: mov      dword ptr [eax + 0x1dc], edx
0x49A86F: mov      eax, dword ptr [ebp - 4]
0x49A872: mov      dword ptr [eax + 0x1e0], 1
0x49A87C: mov      eax, dword ptr [ebp - 4]
0x49A87F: cmp      dword ptr [eax + 0x148], 9
0x49A886: jne      0x49a8c2
0x49A888: push     0
0x49A88A: mov      eax, dword ptr [ebp - 4]
0x49A88D: mov      eax, dword ptr [eax + 0x1b4]
0x49A893: mov      edx, dword ptr [ebp - 4]
0x49A896: sub      eax, dword ptr [edx + 0x1b0]
0x49A89C: inc      eax
0x49A89D: push     eax
0x49A89E: mov      eax, dword ptr [ebp - 4]
0x49A8A1: movzx    eax, byte ptr [eax + 0xc]
0x49A8A5: call     0x4a023c
0x49A8AA: mov      edx, eax
0x49A8AC: mov      eax, dword ptr [ebp - 4]
0x49A8AF: mov      ecx, dword ptr [eax + 0x1e0]
0x49A8B5: mov      eax, dword ptr [ebp - 4]
0x49A8B8: call     0x497610
0x49A8BD: jmp      0x49b1e4
0x49A8C2: push     0
0x49A8C4: mov      eax, dword ptr [ebp - 4]
0x49A8C7: mov      eax, dword ptr [eax + 0x1b4]
0x49A8CD: mov      edx, dword ptr [ebp - 4]
0x49A8D0: sub      eax, dword ptr [edx + 0x1b0]
0x49A8D6: inc      eax
0x49A8D7: push     eax
0x49A8D8: mov      eax, dword ptr [ebp - 4]
0x49A8DB: xor      edx, edx
0x49A8DD: mov      dl, byte ptr [eax + 0xc]
0x49A8E0: mov      eax, dword ptr [ebp - 4]
0x49A8E3: mov      ecx, dword ptr [eax + 0x1e0]
0x49A8E9: mov      eax, dword ptr [ebp - 4]
0x49A8EC: call     0x497610
0x49A8F1: jmp      0x49b1e4
0x49A8F6: mov      eax, dword ptr [ebp - 4]
0x49A8F9: cmp      dword ptr [eax + 0x15c], 0
0x49A900: jne      0x49a9ce
0x49A906: mov      eax, dword ptr [ebp - 4]
0x49A909: mov      dword ptr [eax + 0x15c], 1
0x49A913: movzx    eax, word ptr [0x4a3078] ; *0x4A3078->0x050003
0x49A91A: movzx    edx, word ptr [0x4a307a] ; *0x4A307A->0x780005
0x49A921: add      eax, edx
0x49A923: mov      edx, dword ptr [ebp - 4]
0x49A926: movzx    edx, byte ptr [edx + 0xc]
0x49A92A: imul     edx
0x49A92C: movzx    edx, word ptr [0x4a3076] ; *0x4A3076->0x030080
0x49A933: add      eax, edx
0x49A935: mov      edx, dword ptr [ebp - 4]
0x49A938: mov      dword ptr [edx + 0x1b0], eax
0x49A93E: movzx    eax, word ptr [0x4a3078] ; *0x4A3078->0x050003
0x49A945: mov      edx, dword ptr [ebp - 4]
0x49A948: add      eax, dword ptr [edx + 0x1b0]
0x49A94E: dec      eax
0x49A94F: mov      edx, dword ptr [ebp - 4]
0x49A952: mov      dword ptr [edx + 0x1b4], eax
0x49A958: movzx    eax, word ptr [0x4a307c] ; *0x4A307C->0x030078
0x49A95F: mov      edx, dword ptr [ebp - 4]
0x49A962: mov      dword ptr [edx + 0x1d0], eax
0x49A968: call     0x406154
0x49A96D: mov      edx, dword ptr [ebp - 4]
0x49A970: mov      dword ptr [edx + 0x1d4], eax
0x49A976: xor      eax, eax
0x49A978: mov      al, byte ptr [0x4a307e] ; *0x4A307E->0x830003
0x49A97D: mov      edx, dword ptr [ebp - 4]
0x49A980: mov      dword ptr [edx + 0x1d8], eax
0x49A986: mov      eax, dword ptr [ebp - 4]
0x49A989: xor      edx, edx
0x49A98B: mov      dword ptr [eax + 0x1dc], edx
0x49A991: mov      eax, dword ptr [ebp - 4]
0x49A994: mov      dword ptr [eax + 0x1e0], 1
0x49A99E: push     0
0x49A9A0: mov      eax, dword ptr [ebp - 4]
0x49A9A3: mov      eax, dword ptr [eax + 0x1b4]
0x49A9A9: mov      edx, dword ptr [ebp - 4]
0x49A9AC: sub      eax, dword ptr [edx + 0x1b0]
0x49A9B2: inc      eax
0x49A9B3: push     eax
0x49A9B4: mov      eax, dword ptr [ebp - 4]
0x49A9B7: xor      edx, edx
0x49A9B9: mov      dl, byte ptr [eax + 0xc]
0x49A9BC: mov      ecx, 1
0x49A9C1: mov      eax, dword ptr [ebp - 4]
0x49A9C4: call     0x497610
0x49A9C9: jmp      0x49b1e4
0x49A9CE: mov      eax, dword ptr [ebp - 4]
0x49A9D1: xor      edx, edx
0x49A9D3: mov      dword ptr [eax + 0x15c], edx
0x49A9D9: movzx    eax, word ptr [0x4a3082] ; *0x4A3082->0x050003
0x49A9E0: movzx    edx, word ptr [0x4a3084] ; *0x4A3084->0x780005
0x49A9E7: add      eax, edx
0x49A9E9: mov      edx, dword ptr [ebp - 4]
0x49A9EC: movzx    edx, byte ptr [edx + 0xc]
0x49A9F0: imul     edx
0x49A9F2: movzx    edx, word ptr [0x4a3080] ; *0x4A3080->0x030083
0x49A9F9: add      eax, edx
0x49A9FB: mov      edx, dword ptr [ebp - 4]
0x49A9FE: mov      dword ptr [edx + 0x1b0], eax
0x49AA04: movzx    eax, word ptr [0x4a3082] ; *0x4A3082->0x050003
0x49AA0B: mov      edx, dword ptr [ebp - 4]
0x49AA0E: add      eax, dword ptr [edx + 0x1b0]
0x49AA14: dec      eax
0x49AA15: mov      edx, dword ptr [ebp - 4]
0x49AA18: mov      dword ptr [edx + 0x1b4], eax
0x49AA1E: movzx    eax, word ptr [0x4a3086] ; *0x4A3086->0x030078
0x49AA25: mov      edx, dword ptr [ebp - 4]
0x49AA28: mov      dword ptr [edx + 0x1d0], eax
0x49AA2E: call     0x406154
0x49AA33: mov      edx, dword ptr [ebp - 4]
0x49AA36: mov      dword ptr [edx + 0x1d4], eax
0x49AA3C: xor      eax, eax
0x49AA3E: mov      al, byte ptr [0x4a3088] ; *0x4A3088->0xC00003
0x49AA43: mov      edx, dword ptr [ebp - 4]
0x49AA46: mov      dword ptr [edx + 0x1d8], eax
0x49AA4C: mov      eax, dword ptr [ebp - 4]
0x49AA4F: xor      edx, edx
0x49AA51: mov      dword ptr [eax + 0x1dc], edx
0x49AA57: mov      eax, dword ptr [ebp - 4]
0x49AA5A: mov      dword ptr [eax + 0x1e0], 1
0x49AA64: push     0
0x49AA66: mov      eax, dword ptr [ebp - 4]
0x49AA69: mov      eax, dword ptr [eax + 0x1b4]
0x49AA6F: mov      edx, dword ptr [ebp - 4]
0x49AA72: sub      eax, dword ptr [edx + 0x1b0]
0x49AA78: inc      eax
0x49AA79: push     eax
0x49AA7A: mov      eax, dword ptr [ebp - 4]
0x49AA7D: xor      edx, edx
0x49AA7F: mov      dl, byte ptr [eax + 0xc]
0x49AA82: mov      ecx, 1
0x49AA87: mov      eax, dword ptr [ebp - 4]
0x49AA8A: call     0x497610
0x49AA8F: jmp      0x49b1e4
0x49AA94: movzx    eax, word ptr [0x4a306e] ; *0x4A306E->0x020006
0x49AA9B: movzx    edx, word ptr [0x4a3070] ; *0x4A3070->0x780002
0x49AAA2: add      eax, edx
0x49AAA4: mov      edx, dword ptr [ebp - 4]
0x49AAA7: movzx    edx, byte ptr [edx + 0xc]
0x49AAAB: imul     edx
0x49AAAD: movzx    edx, word ptr [0x4a306c] ; *0x4A306C->0x060080
0x49AAB4: add      eax, edx
0x49AAB6: mov      edx, dword ptr [ebp - 4]
0x49AAB9: mov      dword ptr [edx + 0x1b0], eax
0x49AABF: movzx    eax, word ptr [0x4a306e] ; *0x4A306E->0x020006
0x49AAC6: mov      edx, dword ptr [ebp - 4]
0x49AAC9: add      eax, dword ptr [edx + 0x1b0]
0x49AACF: dec      eax
0x49AAD0: mov      edx, dword ptr [ebp - 4]
0x49AAD3: mov      dword ptr [edx + 0x1b4], eax
0x49AAD9: movzx    eax, word ptr [0x4a3072] ; *0x4A3072->0x030078
0x49AAE0: mov      edx, dword ptr [ebp - 4]
0x49AAE3: mov      dword ptr [edx + 0x1d0], eax
0x49AAE9: call     0x406154
0x49AAEE: mov      edx, dword ptr [ebp - 4]
0x49AAF1: mov      dword ptr [edx + 0x1d4], eax
0x49AAF7: xor      eax, eax
0x49AAF9: mov      al, byte ptr [0x4a3074] ; *0x4A3074->0x800003
0x49AAFE: mov      edx, dword ptr [ebp - 4]
0x49AB01: mov      dword ptr [edx + 0x1d8], eax
0x49AB07: mov      eax, dword ptr [ebp - 4]
0x49AB0A: xor      edx, edx
0x49AB0C: mov      dword ptr [eax + 0x1dc], edx
0x49AB12: mov      eax, dword ptr [ebp - 4]
0x49AB15: mov      dword ptr [eax + 0x1e0], 1
0x49AB1F: push     0
0x49AB21: mov      eax, dword ptr [ebp - 4]
0x49AB24: mov      eax, dword ptr [eax + 0x1b4]
0x49AB2A: mov      edx, dword ptr [ebp - 4]
0x49AB2D: sub      eax, dword ptr [edx + 0x1b0]
0x49AB33: inc      eax
0x49AB34: push     eax
0x49AB35: mov      eax, dword ptr [ebp - 4]
0x49AB38: xor      edx, edx
0x49AB3A: mov      dl, byte ptr [eax + 0xc]
0x49AB3D: mov      eax, dword ptr [ebp - 4]
0x49AB40: mov      ecx, dword ptr [eax + 0x1e0]
0x49AB46: mov      eax, dword ptr [ebp - 4]
0x49AB49: call     0x497610
0x49AB4E: jmp      0x49b1e4
0x49AB53: movzx    eax, word ptr [0x4a30c8] ; *0x4A30C8->0x000002
0x49AB5A: movzx    edx, word ptr [0x4a30ca] ; *0x4A30CA->0x12C0000
0x49AB61: add      eax, edx
0x49AB63: mov      edx, dword ptr [ebp - 4]
0x49AB66: movzx    edx, byte ptr [edx + 0xc]
0x49AB6A: imul     edx
0x49AB6C: movzx    edx, word ptr [0x4a30c6] ; *0x4A30C6->0x0201C8
0x49AB73: add      eax, edx
0x49AB75: mov      edx, dword ptr [ebp - 4]
0x49AB78: mov      dword ptr [edx + 0x1b0], eax
0x49AB7E: movzx    eax, word ptr [0x4a30c8] ; *0x4A30C8->0x000002
0x49AB85: mov      edx, dword ptr [ebp - 4]
0x49AB88: add      eax, dword ptr [edx + 0x1b0]
0x49AB8E: dec      eax
0x49AB8F: mov      edx, dword ptr [ebp - 4]
0x49AB92: mov      dword ptr [edx + 0x1b4], eax
0x49AB98: movzx    eax, word ptr [0x4a30cc] ; *0x4A30CC->0x00012C
0x49AB9F: mov      edx, dword ptr [ebp - 4]
0x49ABA2: mov      dword ptr [edx + 0x1d0], eax
0x49ABA8: call     0x406154
0x49ABAD: mov      edx, dword ptr [ebp - 4]
0x49ABB0: mov      dword ptr [edx + 0x1d4], eax
0x49ABB6: jmp      0x49b1e4
0x49ABBB: movzx    eax, word ptr [0x4a306e] ; *0x4A306E->0x020006
0x49ABC2: movzx    edx, word ptr [0x4a3070] ; *0x4A3070->0x780002
0x49ABC9: add      eax, edx
0x49ABCB: mov      edx, dword ptr [ebp - 4]
0x49ABCE: movzx    edx, byte ptr [edx + 0xc]
0x49ABD2: imul     edx
0x49ABD4: movzx    edx, word ptr [0x4a306c] ; *0x4A306C->0x060080
0x49ABDB: add      eax, edx
0x49ABDD: mov      edx, dword ptr [ebp - 4]
0x49ABE0: mov      dword ptr [edx + 0x1b0], eax
0x49ABE6: movzx    eax, word ptr [0x4a306e] ; *0x4A306E->0x020006
0x49ABED: mov      edx, dword ptr [ebp - 4]
0x49ABF0: add      eax, dword ptr [edx + 0x1b0]
0x49ABF6: dec      eax
0x49ABF7: mov      edx, dword ptr [ebp - 4]
0x49ABFA: mov      dword ptr [edx + 0x1b4], eax
0x49AC00: movzx    eax, word ptr [0x4a3072] ; *0x4A3072->0x030078
0x49AC07: mov      edx, dword ptr [ebp - 4]
0x49AC0A: mov      dword ptr [edx + 0x1d0], eax
0x49AC10: call     0x406154
0x49AC15: mov      edx, dword ptr [ebp - 4]
0x49AC18: mov      dword ptr [edx + 0x1d4], eax
0x49AC1E: xor      eax, eax
0x49AC20: mov      al, byte ptr [0x4a3074] ; *0x4A3074->0x800003
0x49AC25: mov      edx, dword ptr [ebp - 4]
0x49AC28: mov      dword ptr [edx + 0x1d8], eax
0x49AC2E: mov      eax, dword ptr [ebp - 4]
0x49AC31: xor      edx, edx
0x49AC33: mov      dword ptr [eax + 0x1dc], edx
0x49AC39: mov      eax, dword ptr [ebp - 4]
0x49AC3C: cmp      dword ptr [eax + 0x148], 0xd
0x49AC43: jne      0x49ac54
0x49AC45: mov      eax, dword ptr [ebp - 4]
0x49AC48: mov      dword ptr [eax + 0x1e0], 2
0x49AC52: jmp      0x49ac61
0x49AC54: mov      eax, dword ptr [ebp - 4]
0x49AC57: mov      dword ptr [eax + 0x1e0], 1
0x49AC61: push     0
0x49AC63: mov      eax, dword ptr [ebp - 4]
0x49AC66: mov      eax, dword ptr [eax + 0x1b4]
0x49AC6C: mov      edx, dword ptr [ebp - 4]
0x49AC6F: sub      eax, dword ptr [edx + 0x1b0]
0x49AC75: inc      eax
0x49AC76: push     eax
0x49AC77: mov      eax, dword ptr [ebp - 4]
0x49AC7A: xor      edx, edx
0x49AC7C: mov      dl, byte ptr [eax + 0xc]
0x49AC7F: mov      eax, dword ptr [ebp - 4]
0x49AC82: mov      ecx, dword ptr [eax + 0x1e0]
0x49AC88: mov      eax, dword ptr [ebp - 4]
0x49AC8B: call     0x497610
0x49AC90: jmp      0x49b1e4
0x49AC95: movzx    eax, word ptr [0x4a3096] ; *0x4A3096->0x020006
0x49AC9C: movzx    edx, word ptr [0x4a3098] ; *0x4A3098->0x550002
0x49ACA3: add      eax, edx
0x49ACA5: mov      edx, dword ptr [ebp - 4]
0x49ACA8: movzx    edx, byte ptr [edx + 0xc]
0x49ACAC: imul     edx
0x49ACAE: movzx    edx, word ptr [0x4a3094] ; *0x4A3094->0x0600C8
0x49ACB5: add      eax, edx
0x49ACB7: mov      edx, dword ptr [ebp - 4]
0x49ACBA: mov      dword ptr [edx + 0x1b0], eax
0x49ACC0: movzx    eax, word ptr [0x4a3096] ; *0x4A3096->0x020006
0x49ACC7: mov      edx, dword ptr [ebp - 4]
0x49ACCA: add      eax, dword ptr [edx + 0x1b0]
0x49ACD0: dec      eax
0x49ACD1: mov      edx, dword ptr [ebp - 4]
0x49ACD4: mov      dword ptr [edx + 0x1b4], eax
0x49ACDA: movzx    eax, word ptr [0x4a309a] ; *0x4A309A->0x000055
0x49ACE1: mov      edx, dword ptr [ebp - 4]
0x49ACE4: mov      dword ptr [edx + 0x1d0], eax
0x49ACEA: call     0x406154
0x49ACEF: mov      edx, dword ptr [ebp - 4]
0x49ACF2: mov      dword ptr [edx + 0x1d4], eax
0x49ACF8: mov      eax, dword ptr [ebp - 4]
0x49ACFB: mov      byte ptr [eax + 0x14d], 1
0x49AD02: call     0x406154
0x49AD07: mov      edx, dword ptr [ebp - 4]
0x49AD0A: mov      dword ptr [edx + 0x150], eax
0x49AD10: mov      eax, dword ptr [ebp - 4]
0x49AD13: mov      byte ptr [eax + 0xdd], 1
0x49AD1A: push     0
0x49AD1C: push     1
0x49AD1E: mov      eax, dword ptr [ebp - 4]
0x49AD21: xor      edx, edx
0x49AD23: mov      dl, byte ptr [eax + 0xc]
0x49AD26: xor      ecx, ecx
0x49AD28: mov      eax, dword ptr [ebp - 4]
0x49AD2B: call     0x497610
0x49AD30: jmp      0x49b1e4
0x49AD35: movzx    eax, word ptr [0x4a3096] ; *0x4A3096->0x020006
0x49AD3C: movzx    edx, word ptr [0x4a3098] ; *0x4A3098->0x550002
0x49AD43: add      eax, edx
0x49AD45: mov      edx, dword ptr [ebp - 4]
0x49AD48: movzx    edx, byte ptr [edx + 0xc]
0x49AD4C: imul     edx
0x49AD4E: movzx    edx, word ptr [0x4a3094] ; *0x4A3094->0x0600C8
0x49AD55: add      eax, edx
0x49AD57: mov      edx, dword ptr [ebp - 4]
0x49AD5A: mov      dword ptr [edx + 0x1b0], eax
0x49AD60: movzx    eax, word ptr [0x4a3096] ; *0x4A3096->0x020006
0x49AD67: mov      edx, dword ptr [ebp - 4]
0x49AD6A: add      eax, dword ptr [edx + 0x1b0]
0x49AD70: dec      eax
0x49AD71: mov      edx, dword ptr [ebp - 4]
0x49AD74: mov      dword ptr [edx + 0x1b4], eax
0x49AD7A: movzx    eax, word ptr [0x4a309a] ; *0x4A309A->0x000055
0x49AD81: mov      edx, dword ptr [ebp - 4]
0x49AD84: mov      dword ptr [edx + 0x1d0], eax
0x49AD8A: call     0x406154
0x49AD8F: mov      edx, dword ptr [ebp - 4]
0x49AD92: mov      dword ptr [edx + 0x1d4], eax
0x49AD98: mov      eax, dword ptr [ebp - 4]
0x49AD9B: mov      byte ptr [eax + 0x14d], 1
0x49ADA2: call     0x406154
0x49ADA7: mov      edx, dword ptr [ebp - 4]
0x49ADAA: mov      dword ptr [edx + 0x150], eax
0x49ADB0: mov      eax, dword ptr [ebp - 4]
0x49ADB3: cmp      dword ptr [eax + 0x148], 0x12
0x49ADBA: jne      0x49ade0
0x49ADBC: mov      eax, dword ptr [ebp - 4]
0x49ADBF: mov      byte ptr [eax + 0xed], 1
0x49ADC6: mov      eax, dword ptr [ebp - 4]
0x49ADC9: mov      dword ptr [eax + 0x158], 2
0x49ADD3: mov      eax, dword ptr [ebp - 4]
0x49ADD6: mov      dword ptr [eax + 0xf0], 1
0x49ADE0: mov      eax, dword ptr [ebp - 4]
0x49ADE3: cmp      dword ptr [eax + 0x148], 0x13
0x49ADEA: jne      0x49ae10
0x49ADEC: mov      eax, dword ptr [ebp - 4]
0x49ADEF: mov      byte ptr [eax + 0xed], 1
0x49ADF6: mov      eax, dword ptr [ebp - 4]
0x49ADF9: mov      dword ptr [eax + 0x158], 2
0x49AE03: mov      eax, dword ptr [ebp - 4]
0x49AE06: mov      dword ptr [eax + 0xf0], 2
0x49AE10: mov      eax, dword ptr [ebp - 4]
0x49AE13: cmp      dword ptr [eax + 0x148], 0x18
0x49AE1A: jne      0x49ae40
0x49AE1C: mov      eax, dword ptr [ebp - 4]
0x49AE1F: mov      byte ptr [eax + 0xed], 1
0x49AE26: mov      eax, dword ptr [ebp - 4]
0x49AE29: mov      dword ptr [eax + 0x158], 2
0x49AE33: mov      eax, dword ptr [ebp - 4]
0x49AE36: mov      dword ptr [eax + 0xf0], 3
0x49AE40: mov      eax, dword ptr [ebp - 4]
0x49AE43: cmp      dword ptr [eax + 0x148], 8
0x49AE4A: jne      0x49ae70
0x49AE4C: mov      eax, dword ptr [ebp - 4]
0x49AE4F: mov      byte ptr [eax + 0xed], 1
0x49AE56: mov      eax, dword ptr [ebp - 4]
0x49AE59: mov      dword ptr [eax + 0x158], 2
0x49AE63: mov      eax, dword ptr [ebp - 4]
0x49AE66: mov      dword ptr [eax + 0xf0], 4
0x49AE70: push     0
0x49AE72: push     1
0x49AE74: mov      eax, dword ptr [ebp - 4]
0x49AE77: xor      edx, edx
0x49AE79: mov      dl, byte ptr [eax + 0xc]
0x49AE7C: xor      ecx, ecx
0x49AE7E: mov      eax, dword ptr [ebp - 4]
0x49AE81: call     0x497610
0x49AE86: jmp      0x49b1e4
0x49AE8B: movzx    eax, word ptr [0x4a30a0] ; *0x4A30A0->0x020006
0x49AE92: movzx    edx, word ptr [0x4a30a2] ; *0x4A30A2->0x5A0002
0x49AE99: add      eax, edx
0x49AE9B: mov      edx, dword ptr [ebp - 4]
0x49AE9E: movzx    edx, byte ptr [edx + 0xc]
0x49AEA2: imul     edx
0x49AEA4: movzx    edx, word ptr [0x4a309e] ; *0x4A309E->0x060108
0x49AEAB: add      eax, edx
0x49AEAD: mov      edx, dword ptr [ebp - 4]
0x49AEB0: mov      dword ptr [edx + 0x1b0], eax
0x49AEB6: movzx    eax, word ptr [0x4a30a0] ; *0x4A30A0->0x020006
0x49AEBD: mov      edx, dword ptr [ebp - 4]
0x49AEC0: add      eax, dword ptr [edx + 0x1b0]
0x49AEC6: dec      eax
0x49AEC7: mov      edx, dword ptr [ebp - 4]
0x49AECA: mov      dword ptr [edx + 0x1b4], eax
0x49AED0: movzx    eax, word ptr [0x4a30a4] ; *0x4A30A4->0x00005A
0x49AED7: mov      edx, dword ptr [ebp - 4]
0x49AEDA: mov      dword ptr [edx + 0x1d0], eax
0x49AEE0: call     0x406154
0x49AEE5: mov      edx, dword ptr [ebp - 4]
0x49AEE8: mov      dword ptr [edx + 0x1d4], eax
0x49AEEE: mov      eax, dword ptr [ebp - 4]
0x49AEF1: mov      byte ptr [eax + 0x14d], 1
0x49AEF8: call     0x406154
0x49AEFD: mov      edx, dword ptr [ebp - 4]
0x49AF00: mov      dword ptr [edx + 0x150], eax
0x49AF06: push     0
0x49AF08: push     1
0x49AF0A: mov      eax, dword ptr [ebp - 4]
0x49AF0D: xor      edx, edx
0x49AF0F: mov      dl, byte ptr [eax + 0xc]
0x49AF12: xor      ecx, ecx
0x49AF14: mov      eax, dword ptr [ebp - 4]
0x49AF17: call     0x497610
0x49AF1C: jmp      0x49b1e4
0x49AF21: movzx    eax, word ptr [0x4a30aa] ; *0x4A30AA->0x000008
0x49AF28: movzx    edx, word ptr [0x4a30ac] ; *0x4A30AC->0x460000
0x49AF2F: add      eax, edx
0x49AF31: mov      edx, dword ptr [ebp - 4]
0x49AF34: movzx    edx, byte ptr [edx + 0xc]
0x49AF38: imul     edx
0x49AF3A: movzx    edx, word ptr [0x4a30a8] ; *0x4A30A8->0x080148
0x49AF41: add      eax, edx
0x49AF43: mov      edx, dword ptr [ebp - 4]
0x49AF46: mov      dword ptr [edx + 0x1b0], eax
0x49AF4C: movzx    eax, word ptr [0x4a30aa] ; *0x4A30AA->0x000008
0x49AF53: mov      edx, dword ptr [ebp - 4]
0x49AF56: add      eax, dword ptr [edx + 0x1b0]
0x49AF5C: dec      eax
0x49AF5D: mov      edx, dword ptr [ebp - 4]
0x49AF60: mov      dword ptr [edx + 0x1b4], eax
0x49AF66: movzx    eax, word ptr [0x4a30ae] ; *0x4A30AE->0x000046
0x49AF6D: mov      edx, dword ptr [ebp - 4]
0x49AF70: mov      dword ptr [edx + 0x1d0], eax
0x49AF76: call     0x406154
0x49AF7B: mov      edx, dword ptr [ebp - 4]
0x49AF7E: mov      dword ptr [edx + 0x1d4], eax
0x49AF84: mov      eax, dword ptr [ebp - 4]
0x49AF87: mov      byte ptr [eax + 0x14d], 1
0x49AF8E: call     0x406154
0x49AF93: mov      edx, dword ptr [ebp - 4]
0x49AF96: mov      dword ptr [edx + 0x150], eax
0x49AF9C: push     0
0x49AF9E: push     1
0x49AFA0: mov      eax, dword ptr [ebp - 4]
0x49AFA3: xor      edx, edx
0x49AFA5: mov      dl, byte ptr [eax + 0xc]
0x49AFA8: xor      ecx, ecx
0x49AFAA: mov      eax, dword ptr [ebp - 4]
0x49AFAD: call     0x497610
0x49AFB2: jmp      0x49b1e4
0x49AFB7: movzx    eax, word ptr [0x4a30be] ; *0x4A30BE->0x020006
0x49AFBE: movzx    edx, word ptr [0x4a30c0] ; *0x4A30C0->0x3C0002
0x49AFC5: add      eax, edx
0x49AFC7: mov      edx, dword ptr [ebp - 4]
0x49AFCA: movzx    edx, byte ptr [edx + 0xc]
0x49AFCE: imul     edx
0x49AFD0: movzx    edx, word ptr [0x4a30bc] ; *0x4A30BC->0x060188
0x49AFD7: add      eax, edx
0x49AFD9: mov      edx, dword ptr [ebp - 4]
0x49AFDC: mov      dword ptr [edx + 0x1b0], eax
0x49AFE2: movzx    eax, word ptr [0x4a30be] ; *0x4A30BE->0x020006
0x49AFE9: mov      edx, dword ptr [ebp - 4]
0x49AFEC: add      eax, dword ptr [edx + 0x1b0]
0x49AFF2: dec      eax
0x49AFF3: mov      edx, dword ptr [ebp - 4]
0x49AFF6: mov      dword ptr [edx + 0x1b4], eax
0x49AFFC: movzx    eax, word ptr [0x4a30c2] ; *0x4A30C2->0x00003C
0x49B003: mov      edx, dword ptr [ebp - 4]
0x49B006: mov      dword ptr [edx + 0x1d0], eax
0x49B00C: call     0x406154
0x49B011: mov      edx, dword ptr [ebp - 4]
0x49B014: mov      dword ptr [edx + 0x1d4], eax
0x49B01A: mov      eax, dword ptr [ebp - 4]
0x49B01D: xor      edx, edx
0x49B01F: mov      dword ptr [eax + 0x104], edx
0x49B025: mov      eax, dword ptr [ebp - 4]
0x49B028: mov      byte ptr [eax + 0xec], 1
0x49B02F: mov      eax, dword ptr [ebp - 4]
0x49B032: mov      eax, dword ptr [eax + 0x11c]
0x49B038: sub      eax, 0x16
0x49B03B: je       0x49b044
0x49B03D: sub      eax, 4
0x49B040: je       0x49b060
0x49B042: jmp      0x49b090
0x49B044: mov      eax, dword ptr [ebp - 4]
0x49B047: mov      dword ptr [eax + 0x158], 4
0x49B051: mov      eax, dword ptr [ebp - 4]
0x49B054: mov      dword ptr [eax + 0x108], 0xa
0x49B05E: jmp      0x49b0aa
0x49B060: mov      eax, dword ptr [ebp - 4]
0x49B063: mov      dword ptr [eax + 0x158], 2
0x49B06D: mov      eax, dword ptr [ebp - 4]
0x49B070: mov      dword ptr [eax + 0x108], 0x14
0x49B07A: mov      eax, dword ptr [ebp - 4]
0x49B07D: mov      eax, dword ptr [eax + 0x1d0]
0x49B083: shr      eax, 1
0x49B085: mov      edx, dword ptr [ebp - 4]
0x49B088: mov      dword ptr [edx + 0x1d0], eax
0x49B08E: jmp      0x49b0aa
0x49B090: mov      eax, dword ptr [ebp - 4]
0x49B093: mov      dword ptr [eax + 0x158], 2
0x49B09D: mov      eax, dword ptr [ebp - 4]
0x49B0A0: mov      dword ptr [eax + 0x108], 0xa
0x49B0AA: call     0x406154
0x49B0AF: mov      edx, dword ptr [ebp - 4]
0x49B0B2: mov      dword ptr [edx + 0xf4], eax
0x49B0B8: mov      eax, dword ptr [ebp - 4]
0x49B0BB: mov      byte ptr [eax + 0x14d], 1
0x49B0C2: call     0x406154
0x49B0C7: mov      edx, dword ptr [ebp - 4]
0x49B0CA: mov      dword ptr [edx + 0x150], eax
0x49B0D0: push     0
0x49B0D2: push     1
0x49B0D4: mov      eax, dword ptr [ebp - 4]
0x49B0D7: xor      edx, edx
0x49B0D9: mov      dl, byte ptr [eax + 0xc]
0x49B0DC: xor      ecx, ecx
0x49B0DE: mov      eax, dword ptr [ebp - 4]
0x49B0E1: call     0x497610
0x49B0E6: jmp      0x49b1e4
0x49B0EB: movzx    eax, word ptr [0x4a30d2] ; *0x4A30D2->0x050003
0x49B0F2: movzx    edx, word ptr [0x4a30d4] ; *0x4A30D4->0x460005
0x49B0F9: add      eax, edx
0x49B0FB: mov      edx, dword ptr [ebp - 4]
0x49B0FE: movzx    edx, byte ptr [edx + 0xc]
0x49B102: imul     edx
0x49B104: movzx    edx, word ptr [0x4a30d0] ; *0x4A30D0->0x0301D8
0x49B10B: add      eax, edx
0x49B10D: mov      edx, dword ptr [ebp - 4]
0x49B110: mov      dword ptr [edx + 0x1b0], eax
0x49B116: movzx    eax, word ptr [0x4a30d2] ; *0x4A30D2->0x050003
0x49B11D: mov      edx, dword ptr [ebp - 4]
0x49B120: add      eax, dword ptr [edx + 0x1b0]
0x49B126: dec      eax
0x49B127: mov      edx, dword ptr [ebp - 4]
0x49B12A: mov      dword ptr [edx + 0x1b4], eax
0x49B130: mov      eax, dword ptr [ebp - 4]
0x49B133: mov      eax, dword ptr [eax + 0x1e8]
0x49B139: mov      edx, dword ptr [ebp - 4]
0x49B13C: mov      dword ptr [edx + 0x1d0], eax
0x49B142: call     0x406154
0x49B147: mov      edx, dword ptr [ebp - 4]
0x49B14A: mov      dword ptr [edx + 0x1d4], eax
0x49B150: push     0
0x49B152: push     1
0x49B154: mov      eax, dword ptr [ebp - 4]
0x49B157: xor      edx, edx
0x49B159: mov      dl, byte ptr [eax + 0xc]
0x49B15C: xor      ecx, ecx
0x49B15E: mov      eax, dword ptr [ebp - 4]
0x49B161: call     0x497610
0x49B166: call     0x406154
0x49B16B: mov      edx, dword ptr [ebp - 4]
0x49B16E: mov      dword ptr [edx + 0x200], eax
0x49B174: mov      eax, dword ptr [ebp - 4]
0x49B177: xor      edx, edx
0x49B179: mov      dword ptr [eax + 0x254], edx
0x49B17F: jmp      0x49b1e4
0x49B181: movzx    eax, word ptr [0x4a30dc] ; *0x4A30DC->0x040004
0x49B188: movzx    edx, word ptr [0x4a30de] ; *0x4A30DE->0x780004
0x49B18F: add      eax, edx
0x49B191: mov      edx, dword ptr [ebp - 4]
0x49B194: movzx    edx, byte ptr [edx + 0xc]
0x49B198: imul     edx
0x49B19A: movzx    edx, word ptr [0x4a30da] ; *0x4A30DA->0x040218
0x49B1A1: add      eax, edx
0x49B1A3: mov      edx, dword ptr [ebp - 4]
0x49B1A6: mov      dword ptr [edx + 0x1b0], eax
0x49B1AC: movzx    eax, word ptr [0x4a30dc] ; *0x4A30DC->0x040004
0x49B1B3: mov      edx, dword ptr [ebp - 4]
0x49B1B6: add      eax, dword ptr [edx + 0x1b0]
0x49B1BC: dec      eax
0x49B1BD: mov      edx, dword ptr [ebp - 4]
0x49B1C0: mov      dword ptr [edx + 0x1b4], eax
0x49B1C6: movzx    eax, word ptr [0x4a30e0] ; *0x4A30E0->0x000078
0x49B1CD: mov      edx, dword ptr [ebp - 4]
0x49B1D0: mov      dword ptr [edx + 0x1d0], eax
0x49B1D6: call     0x406154
0x49B1DB: mov      edx, dword ptr [ebp - 4]
0x49B1DE: mov      dword ptr [edx + 0x1d4], eax
0x49B1E4: pop      ecx
0x49B1E5: pop      ecx
0x49B1E6: pop      ebp
0x49B1E7: ret      

; === THumActor.DMT_-2 @ 0x49B1E8 ===
0x49B1E8: push     ebp
0x49B1E9: mov      ebp, esp
0x49B1EB: push     ecx
0x49B1EC: mov      dword ptr [ebp - 4], eax
0x49B1EF: mov      eax, dword ptr [ebp - 4]
0x49B1F2: call     0x498470 ; TActor.DMT_-2
0x49B1F7: pop      ecx
0x49B1F8: pop      ebp
0x49B1F9: ret      
0x49B1FA: mov      eax, eax
0x49B1FC: push     ebp
0x49B1FD: mov      ebp, esp
0x49B1FF: add      esp, -0x18
0x49B202: mov      byte ptr [ebp - 5], dl
0x49B205: mov      dword ptr [ebp - 4], eax
0x49B208: mov      eax, dword ptr [ebp - 4]
0x49B20B: cmp      byte ptr [eax + 0x20], 0
0x49B20F: je       0x49b245
0x49B211: movzx    eax, word ptr [0x4a30dc] ; *0x4A30DC->0x040004
0x49B218: movzx    edx, word ptr [0x4a30de] ; *0x4A30DE->0x780004
0x49B21F: add      eax, edx
0x49B221: mov      edx, dword ptr [ebp - 4]
0x49B224: movzx    edx, byte ptr [edx + 0xc]
0x49B228: imul     edx
0x49B22A: movzx    edx, word ptr [0x4a30da] ; *0x4A30DA->0x040218
0x49B231: add      eax, edx
0x49B233: movzx    edx, word ptr [0x4a30dc] ; *0x4A30DC->0x040004
0x49B23A: dec      edx
0x49B23B: add      eax, edx
0x49B23D: mov      dword ptr [ebp - 0xc], eax
0x49B240: jmp      0x49b2e2
0x49B245: cmp      byte ptr [ebp - 5], 0
0x49B249: je       0x49b272
0x49B24B: movzx    eax, word ptr [0x4a308c] ; *0x4A308C->0x000001
0x49B252: movzx    edx, word ptr [0x4a308e] ; *0x4A308E->0xC80000
0x49B259: add      eax, edx
0x49B25B: mov      edx, dword ptr [ebp - 4]
0x49B25E: movzx    edx, byte ptr [edx + 0xc]
0x49B262: imul     edx
0x49B264: movzx    edx, word ptr [0x4a308a] ; *0x4A308A->0x0100C0
0x49B26B: add      eax, edx
0x49B26D: mov      dword ptr [ebp - 0xc], eax
0x49B270: jmp      0x49b2e2
0x49B272: movzx    eax, word ptr [0x4a305a] ; *0x4A305A->0x040004
0x49B279: mov      edx, dword ptr [ebp - 4]
0x49B27C: mov      dword ptr [edx + 0x1f4], eax
0x49B282: mov      eax, dword ptr [ebp - 4]
0x49B285: cmp      dword ptr [eax + 0x1ec], 0
0x49B28C: jge      0x49b295
0x49B28E: xor      eax, eax
0x49B290: mov      dword ptr [ebp - 0x10], eax
0x49B293: jmp      0x49b2ba
0x49B295: movzx    eax, word ptr [0x4a305a] ; *0x4A305A->0x040004
0x49B29C: mov      edx, dword ptr [ebp - 4]
0x49B29F: cmp      eax, dword ptr [edx + 0x1ec]
0x49B2A5: jg       0x49b2ae
0x49B2A7: xor      eax, eax
0x49B2A9: mov      dword ptr [ebp - 0x10], eax
0x49B2AC: jmp      0x49b2ba
0x49B2AE: mov      eax, dword ptr [ebp - 4]
0x49B2B1: mov      eax, dword ptr [eax + 0x1ec]
0x49B2B7: mov      dword ptr [ebp - 0x10], eax
0x49B2BA: movzx    eax, word ptr [0x4a305a] ; *0x4A305A->0x040004
0x49B2C1: movzx    edx, word ptr [0x4a305c] ; *0x4A305C->0xC80004
0x49B2C8: add      eax, edx
0x49B2CA: mov      edx, dword ptr [ebp - 4]
0x49B2CD: movzx    edx, byte ptr [edx + 0xc]
0x49B2D1: imul     edx
0x49B2D3: movzx    edx, word ptr [0x4a3058] ; *0x4A3058->0x040000
0x49B2DA: add      eax, edx
0x49B2DC: add      eax, dword ptr [ebp - 0x10]
0x49B2DF: mov      dword ptr [ebp - 0xc], eax
0x49B2E2: mov      eax, dword ptr [ebp - 0xc]
0x49B2E5: mov      esp, ebp
0x49B2E7: pop      ebp
0x49B2E8: ret      
0x49B2E9: lea      eax, [eax]
0x49B2EC: push     ebp
0x49B2ED: mov      ebp, esp
0x49B2EF: add      esp, -0x14
0x49B2F2: mov      dword ptr [ebp - 8], edx
0x49B2F5: mov      dword ptr [ebp - 4], eax
0x49B2F8: mov      eax, dword ptr [ebp - 4]
0x49B2FB: mov      byte ptr [eax + 0x25c], 0
0x49B302: mov      eax, dword ptr [ebp - 4]
0x49B305: cmp      dword ptr [eax + 0x148], 0xf
0x49B30C: jne      0x49b3ca
0x49B312: cmp      dword ptr [ebp - 8], 5
0x49B316: jne      0x49b3ca
0x49B31C: mov      eax, dword ptr [ebp - 4]
0x49B31F: cmp      byte ptr [eax + 0xdc], 0
0x49B326: je       0x49b3ca
0x49B32C: mov      eax, dword ptr [ebp - 4]
0x49B32F: mov      byte ptr [eax + 0xdc], 0
0x49B336: push     3
0x49B338: mov      eax, dword ptr [ebp - 4]
0x49B33B: movzx    eax, word ptr [eax + 8]
0x49B33F: push     eax
0x49B340: mov      eax, dword ptr [ebp - 4]
0x49B343: movzx    eax, word ptr [eax + 0xa]
0x49B347: push     eax
0x49B348: mov      eax, dword ptr [ebp - 4]
0x49B34B: movzx    eax, byte ptr [eax + 0xc]
0x49B34F: mov      ecx, eax
0x49B351: shl      ecx, 3
0x49B354: mov      dl, 1
0x49B356: mov      eax, dword ptr [0x49c270]
0x49B35B: call     0x49d9f4
0x49B360: mov      dword ptr [ebp - 0xc], eax
0x49B363: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x49B368: mov      eax, dword ptr [eax]
0x49B36A: mov      eax, dword ptr [eax + 0x370]
0x49B370: mov      edx, dword ptr [ebp - 0xc]
0x49B373: mov      dword ptr [edx + 0x14], eax
0x49B376: mov      eax, dword ptr [ebp - 0xc]
0x49B379: mov      dword ptr [eax + 0x74], 0x50
0x49B380: mov      eax, 0x5b
0x49B385: call     0x495794
0x49B38A: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x49B38F: mov      eax, dword ptr [eax]
0x49B391: mov      eax, dword ptr [eax + 0x5a860]
0x49B397: mov      edx, dword ptr [ebp - 0xc]
0x49B39A: call     0x40f588
0x49B39F: push     3
0x49B3A1: mov      eax, dword ptr [ebp - 4]
0x49B3A4: movzx    ecx, word ptr [eax + 0xa]
0x49B3A8: mov      eax, dword ptr [ebp - 4]
0x49B3AB: movzx    edx, word ptr [eax + 8]
0x49B3AF: mov      eax, dword ptr [0x4a40a8] ; *0x4A40A8->0x4F7DCC
0x49B3B4: mov      eax, dword ptr [eax]
0x49B3B6: call     0x4778e0
0x49B3BB: mov      dword ptr [ebp - 0x10], eax
0x49B3BE: cmp      dword ptr [ebp - 0x10], 0
0x49B3C2: je       0x49b3ca
0x49B3C4: mov      eax, dword ptr [ebp - 0x10]
0x49B3C7: inc      dword ptr [eax + 0x1c]
0x49B3CA: mov      eax, dword ptr [ebp - 4]
0x49B3CD: cmp      dword ptr [eax + 0x148], 5
0x49B3D4: jne      0x49b47e
0x49B3DA: cmp      dword ptr [ebp - 8], 3
0x49B3DE: jne      0x49b46e
0x49B3E4: mov      eax, dword ptr [ebp - 4]
0x49B3E7: cmp      byte ptr [eax + 0xdd], 0
0x49B3EE: je       0x49b46e
0x49B3F0: mov      eax, dword ptr [ebp - 4]
0x49B3F3: mov      byte ptr [eax + 0xdd], 0
0x49B3FA: mov      eax, dword ptr [ebp - 4]
0x49B3FD: movzx    eax, word ptr [eax + 0xa]
0x49B401: push     eax
0x49B402: mov      eax, dword ptr [ebp - 4]
0x49B405: mov      eax, dword ptr [eax + 0xc4]
0x49B40B: push     eax
0x49B40C: mov      eax, dword ptr [ebp - 4]
0x49B40F: mov      eax, dword ptr [eax + 0xc8]
0x49B415: push     eax
0x49B416: mov      eax, dword ptr [ebp - 4]
0x49B419: mov      eax, dword ptr [eax + 0xcc]
0x49B41F: push     eax
0x49B420: push     3
0x49B422: mov      eax, dword ptr [ebp - 4]
0x49B425: movzx    ecx, word ptr [eax + 8]
0x49B429: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x49B42E: mov      eax, dword ptr [eax]
0x49B430: mov      edx, dword ptr [ebp - 4]
0x49B433: call     0x4723a8
0x49B438: mov      dword ptr [ebp - 0x14], eax
0x49B43B: cmp      dword ptr [ebp - 0x14], 0
0x49B43F: je       0x49b46e
0x49B441: mov      eax, dword ptr [ebp - 0x14]
0x49B444: mov      dword ptr [eax + 0xc4], 0x28
0x49B44E: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x49B453: mov      eax, dword ptr [eax]
0x49B455: mov      eax, dword ptr [eax + 0x358]
0x49B45B: mov      edx, dword ptr [ebp - 0x14]
0x49B45E: mov      dword ptr [edx + 0x14], eax
0x49B461: mov      eax, dword ptr [ebp - 0x14]
0x49B464: mov      dword ptr [eax + 0xc0], 0x1bf
0x49B46E: cmp      dword ptr [ebp - 8], 3
0x49B472: jl       0x49b47e
0x49B474: mov      eax, dword ptr [ebp - 4]
0x49B477: mov      byte ptr [eax + 0x25c], 1
0x49B47E: mov      esp, ebp
0x49B480: pop      ebp
0x49B481: ret      
0x49B482: mov      eax, eax
0x49B484: push     ebp
0x49B485: mov      ebp, esp
0x49B487: push     ecx
0x49B488: mov      dword ptr [ebp - 4], eax
0x49B48B: mov      eax, dword ptr [ebp - 4]
0x49B48E: mov      byte ptr [eax + 0x24c], 1
0x49B495: mov      eax, dword ptr [ebp - 4]
0x49B498: xor      edx, edx
0x49B49A: mov      dword ptr [eax + 0x250], edx
0x49B4A0: pop      ecx
0x49B4A1: pop      ebp
0x49B4A2: ret      
0x49B4A3: nop      
0x49B4A4: push     ebp
0x49B4A5: mov      ebp, esp
0x49B4A7: push     ecx
0x49B4A8: mov      eax, dword ptr [ebp + 8]
0x49B4AB: mov      eax, dword ptr [eax - 4]
0x49B4AE: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49B4B4: cmp      eax, dword ptr [edx]
0x49B4B6: jne      0x49b4d4
0x49B4B8: call     0x406154
0x49B4BD: mov      edx, dword ptr [ebp + 8]
0x49B4C0: mov      edx, dword ptr [edx - 4]
0x49B4C3: sub      eax, dword ptr [edx + 0xf4]
0x49B4C9: cmp      eax, 0xbb8
0x49B4CE: seta     byte ptr [ebp - 1]
0x49B4D2: jmp      0x49b4ee
0x49B4D4: call     0x406154
0x49B4D9: mov      edx, dword ptr [ebp + 8]
0x49B4DC: mov      edx, dword ptr [edx - 4]
0x49B4DF: sub      eax, dword ptr [edx + 0xf4]
0x49B4E5: cmp      eax, 0x7d0
0x49B4EA: seta     byte ptr [ebp - 1]
0x49B4EE: cmp      byte ptr [ebp - 1], 0
0x49B4F2: je       0x49b502
0x49B4F4: mov      eax, dword ptr [ebp + 8]
0x49B4F7: mov      eax, dword ptr [eax - 4]
0x49B4FA: xor      edx, edx
0x49B4FC: mov      dword ptr [eax + 0x10c], edx
0x49B502: mov      al, byte ptr [ebp - 1]
0x49B505: pop      ecx
0x49B506: pop      ebp
0x49B507: ret      

; === THumActor.DMT_-6 @ 0x49B508 ===
0x49B508: push     ebp
0x49B509: mov      ebp, esp
0x49B50B: add      esp, -0x1c
0x49B50E: push     ebx
0x49B50F: mov      dword ptr [ebp - 4], eax
0x49B512: call     0x406154
0x49B517: mov      edx, dword ptr [ebp - 4]
0x49B51A: sub      eax, dword ptr [edx + 0x200]
0x49B520: cmp      eax, 0x78
0x49B523: jbe      0x49b55f
0x49B525: call     0x406154
0x49B52A: mov      edx, dword ptr [ebp - 4]
0x49B52D: mov      dword ptr [edx + 0x200], eax
0x49B533: mov      eax, dword ptr [ebp - 4]
0x49B536: inc      dword ptr [eax + 0x130]
0x49B53C: mov      eax, dword ptr [ebp - 4]
0x49B53F: cmp      dword ptr [eax + 0x130], 0x186a0
0x49B549: jle      0x49b556
0x49B54B: mov      eax, dword ptr [ebp - 4]
0x49B54E: xor      edx, edx
0x49B550: mov      dword ptr [eax + 0x130], edx
0x49B556: mov      eax, dword ptr [ebp - 4]
0x49B559: inc      dword ptr [eax + 0x254]
0x49B55F: mov      eax, dword ptr [ebp - 4]
0x49B562: cmp      byte ptr [eax + 0x24c], 0
0x49B569: je       0x49b5ab
0x49B56B: call     0x406154
0x49B570: mov      edx, dword ptr [ebp - 4]
0x49B573: sub      eax, dword ptr [edx + 0x258]
0x49B579: cmp      eax, 0x78
0x49B57C: jbe      0x49b5ab
0x49B57E: call     0x406154
0x49B583: mov      edx, dword ptr [ebp - 4]
0x49B586: mov      dword ptr [edx + 0x258], eax
0x49B58C: mov      eax, dword ptr [ebp - 4]
0x49B58F: inc      dword ptr [eax + 0x250]
0x49B595: mov      eax, dword ptr [ebp - 4]
0x49B598: cmp      dword ptr [eax + 0x250], 5
0x49B59F: jl       0x49b5ab
0x49B5A1: mov      eax, dword ptr [ebp - 4]
0x49B5A4: mov      byte ptr [eax + 0x24c], 0
0x49B5AB: mov      eax, dword ptr [ebp - 4]
0x49B5AE: cmp      dword ptr [eax + 0x148], 0xb
0x49B5B5: je       0x49ba2d
0x49B5BB: mov      eax, dword ptr [ebp - 4]
0x49B5BE: cmp      dword ptr [eax + 0x148], 9
0x49B5C5: je       0x49ba2d
0x49B5CB: mov      eax, dword ptr [ebp - 4]
0x49B5CE: cmp      dword ptr [eax + 0x148], 0xd
0x49B5D5: je       0x49ba2d
0x49B5DB: mov      eax, dword ptr [ebp - 4]
0x49B5DE: cmp      dword ptr [eax + 0x148], 6
0x49B5E5: je       0x49ba2d
0x49B5EB: mov      eax, dword ptr [ebp - 4]
0x49B5EE: cmp      dword ptr [eax + 0x148], 7
0x49B5F5: je       0x49ba2d
0x49B5FB: mov      eax, dword ptr [ebp - 4]
0x49B5FE: mov      byte ptr [eax + 0x1e4], 0
0x49B605: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49B60A: mov      eax, dword ptr [eax]
0x49B60C: cmp      eax, dword ptr [ebp - 4]
0x49B60F: je       0x49b62a
0x49B611: mov      eax, dword ptr [ebp - 4]
0x49B614: mov      eax, dword ptr [eax + 0x220]
0x49B61A: cmp      dword ptr [eax + 8], 2
0x49B61E: jl       0x49b62a
0x49B620: mov      eax, dword ptr [ebp - 4]
0x49B623: mov      byte ptr [eax + 0x1e4], 1
0x49B62A: mov      eax, dword ptr [ebp - 4]
0x49B62D: mov      edx, dword ptr [eax + 0x1b8]
0x49B633: mov      eax, dword ptr [ebp - 4]
0x49B636: sub      edx, dword ptr [eax + 0x1b0]
0x49B63C: mov      eax, dword ptr [ebp - 4]
0x49B63F: mov      bx, 0xfff8
0x49B643: call     0x403080
0x49B648: mov      eax, dword ptr [ebp - 4]
0x49B64B: mov      edx, dword ptr [eax + 0x1b8]
0x49B651: mov      eax, dword ptr [ebp - 4]
0x49B654: sub      edx, dword ptr [eax + 0x1b0]
0x49B65A: mov      eax, dword ptr [ebp - 4]
0x49B65D: mov      bx, 0xfff7
0x49B661: call     0x403080
0x49B666: mov      eax, dword ptr [ebp - 4]
0x49B669: mov      eax, dword ptr [eax + 0x1b8]
0x49B66F: mov      dword ptr [ebp - 8], eax
0x49B672: mov      eax, dword ptr [ebp - 4]
0x49B675: cmp      dword ptr [eax + 0x148], 0
0x49B67C: je       0x49b999
0x49B682: mov      eax, dword ptr [ebp - 4]
0x49B685: mov      eax, dword ptr [eax + 0x1b8]
0x49B68B: mov      edx, dword ptr [ebp - 4]
0x49B68E: cmp      eax, dword ptr [edx + 0x1b0]
0x49B694: jl       0x49b6aa
0x49B696: mov      eax, dword ptr [ebp - 4]
0x49B699: mov      eax, dword ptr [eax + 0x1b8]
0x49B69F: mov      edx, dword ptr [ebp - 4]
0x49B6A2: cmp      eax, dword ptr [edx + 0x1b4]
0x49B6A8: jle      0x49b6bc
0x49B6AA: mov      eax, dword ptr [ebp - 4]
0x49B6AD: mov      eax, dword ptr [eax + 0x1b0]
0x49B6B3: mov      edx, dword ptr [ebp - 4]
0x49B6B6: mov      dword ptr [edx + 0x1b8], eax
0x49B6BC: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49B6C1: mov      eax, dword ptr [eax]
0x49B6C3: cmp      eax, dword ptr [ebp - 4]
0x49B6C6: je       0x49b6fa
0x49B6C8: mov      eax, dword ptr [ebp - 4]
0x49B6CB: cmp      byte ptr [eax + 0xec], 0
0x49B6D2: je       0x49b6fa
0x49B6D4: mov      eax, dword ptr [ebp - 4]
0x49B6D7: mov      eax, dword ptr [eax + 0x1d0]
0x49B6DD: mov      dword ptr [ebp - 0x1c], eax
0x49B6E0: xor      eax, eax
0x49B6E2: mov      dword ptr [ebp - 0x18], eax
0x49B6E5: fild     qword ptr [ebp - 0x1c]
0x49B6E8: fld      xword ptr [0x49ba34]
0x49B6EE: fdivp    st(1)
0x49B6F0: call     0x4029a0
0x49B6F5: mov      dword ptr [ebp - 0xc], eax
0x49B6F8: jmp      0x49b738
0x49B6FA: mov      eax, dword ptr [ebp - 4]
0x49B6FD: cmp      byte ptr [eax + 0x1e4], 0
0x49B704: je       0x49b72c
0x49B706: mov      eax, dword ptr [ebp - 4]
0x49B709: mov      eax, dword ptr [eax + 0x1d0]
0x49B70F: add      eax, eax
0x49B711: mov      dword ptr [ebp - 0x1c], eax
0x49B714: xor      eax, eax
0x49B716: mov      dword ptr [ebp - 0x18], eax
0x49B719: fild     qword ptr [ebp - 0x1c]
0x49B71C: fdiv     dword ptr [0x49ba40]
0x49B722: call     0x4029a0
0x49B727: mov      dword ptr [ebp - 0xc], eax
0x49B72A: jmp      0x49b738
0x49B72C: mov      eax, dword ptr [ebp - 4]
0x49B72F: mov      eax, dword ptr [eax + 0x1d0]
0x49B735: mov      dword ptr [ebp - 0xc], eax
0x49B738: call     0x406154
0x49B73D: mov      edx, dword ptr [ebp - 4]
0x49B740: sub      eax, dword ptr [edx + 0x1d4]
0x49B746: cmp      eax, dword ptr [ebp - 0xc]
0x49B749: jbe      0x49b966
0x49B74F: mov      eax, dword ptr [ebp - 4]
0x49B752: mov      eax, dword ptr [eax + 0x1b8]
0x49B758: mov      edx, dword ptr [ebp - 4]
0x49B75B: cmp      eax, dword ptr [edx + 0x1b4]
0x49B761: jge      0x49b82d
0x49B767: mov      eax, dword ptr [ebp - 4]
0x49B76A: cmp      byte ptr [eax + 0xec], 0
0x49B771: je       0x49b814
0x49B777: mov      eax, dword ptr [ebp - 4]
0x49B77A: mov      eax, dword ptr [eax + 0x104]
0x49B780: mov      edx, dword ptr [ebp - 4]
0x49B783: mov      edx, dword ptr [edx + 0x108]
0x49B789: sub      edx, 2
0x49B78C: cmp      eax, edx
0x49B78E: je       0x49b79b
0x49B790: push     ebp
0x49B791: call     0x49b4a4
0x49B796: pop      ecx
0x49B797: test     al, al
0x49B799: je       0x49b7db
0x49B79B: mov      eax, dword ptr [ebp - 4]
0x49B79E: cmp      dword ptr [eax + 0x10c], 0
0x49B7A5: jge      0x49b7b6
0x49B7A7: push     ebp
0x49B7A8: call     0x49b4a4
0x49B7AD: pop      ecx
0x49B7AE: test     al, al
0x49B7B0: je       0x49b87f
0x49B7B6: mov      eax, dword ptr [ebp - 4]
0x49B7B9: inc      dword ptr [eax + 0x1b8]
0x49B7BF: mov      eax, dword ptr [ebp - 4]
0x49B7C2: inc      dword ptr [eax + 0x104]
0x49B7C8: call     0x406154
0x49B7CD: mov      edx, dword ptr [ebp - 4]
0x49B7D0: mov      dword ptr [edx + 0x1d4], eax
0x49B7D6: jmp      0x49b87f
0x49B7DB: mov      eax, dword ptr [ebp - 4]
0x49B7DE: mov      eax, dword ptr [eax + 0x1b8]
0x49B7E4: mov      edx, dword ptr [ebp - 4]
0x49B7E7: mov      edx, dword ptr [edx + 0x1b4]
0x49B7ED: dec      edx
0x49B7EE: cmp      eax, edx
0x49B7F0: jge      0x49b7fb
0x49B7F2: mov      eax, dword ptr [ebp - 4]
0x49B7F5: inc      dword ptr [eax + 0x1b8]
0x49B7FB: mov      eax, dword ptr [ebp - 4]
0x49B7FE: inc      dword ptr [eax + 0x104]
0x49B804: call     0x406154
0x49B809: mov      edx, dword ptr [ebp - 4]
0x49B80C: mov      dword ptr [edx + 0x1d4], eax
0x49B812: jmp      0x49b87f
0x49B814: mov      eax, dword ptr [ebp - 4]
0x49B817: inc      dword ptr [eax + 0x1b8]
0x49B81D: call     0x406154
0x49B822: mov      edx, dword ptr [ebp - 4]
0x49B825: mov      dword ptr [edx + 0x1d4], eax
0x49B82B: jmp      0x49b87f
0x49B82D: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49B832: mov      eax, dword ptr [eax]
0x49B834: cmp      eax, dword ptr [ebp - 4]
0x49B837: jne      0x49b860
0x49B839: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x49B83E: mov      eax, dword ptr [eax]
0x49B840: call     0x466f1c
0x49B845: test     al, al
0x49B847: je       0x49b875
0x49B849: mov      eax, dword ptr [ebp - 4]
0x49B84C: xor      edx, edx
0x49B84E: mov      dword ptr [eax + 0x148], edx
0x49B854: mov      eax, dword ptr [ebp - 4]
0x49B857: mov      byte ptr [eax + 0xec], 0
0x49B85E: jmp      0x49b875
0x49B860: mov      eax, dword ptr [ebp - 4]
0x49B863: xor      edx, edx
0x49B865: mov      dword ptr [eax + 0x148], edx
0x49B86B: mov      eax, dword ptr [ebp - 4]
0x49B86E: mov      byte ptr [eax + 0xec], 0
0x49B875: mov      eax, dword ptr [ebp - 4]
0x49B878: mov      byte ptr [eax + 0xed], 0
0x49B87F: mov      eax, dword ptr [ebp - 4]
0x49B882: cmp      byte ptr [eax + 0xec], 0
0x49B889: je       0x49b966
0x49B88F: mov      eax, dword ptr [ebp - 4]
0x49B892: mov      eax, dword ptr [eax + 0x104]
0x49B898: mov      edx, dword ptr [ebp - 4]
0x49B89B: mov      edx, dword ptr [edx + 0x108]
0x49B8A1: dec      edx
0x49B8A2: cmp      eax, edx
0x49B8A4: jne      0x49b966
0x49B8AA: mov      eax, dword ptr [ebp - 4]
0x49B8AD: cmp      dword ptr [eax + 0x10c], 0
0x49B8B4: jle      0x49b942
0x49B8BA: mov      eax, dword ptr [ebp - 4]
0x49B8BD: add      eax, 0x10c
0x49B8C2: mov      dword ptr [ebp - 0x14], eax
0x49B8C5: mov      eax, dword ptr [ebp - 0x14]
0x49B8C8: mov      eax, dword ptr [eax + 0x10]
0x49B8CB: push     eax
0x49B8CC: mov      eax, dword ptr [ebp - 4]
0x49B8CF: movzx    eax, word ptr [eax + 8]
0x49B8D3: push     eax
0x49B8D4: mov      eax, dword ptr [ebp - 4]
0x49B8D7: movzx    eax, word ptr [eax + 0xa]
0x49B8DB: push     eax
0x49B8DC: mov      eax, dword ptr [ebp - 0x14]
0x49B8DF: mov      eax, dword ptr [eax + 0x14]
0x49B8E2: push     eax
0x49B8E3: mov      eax, dword ptr [ebp - 0x14]
0x49B8E6: mov      eax, dword ptr [eax + 0x18]
0x49B8E9: push     eax
0x49B8EA: mov      eax, dword ptr [ebp - 0x14]
0x49B8ED: mov      eax, dword ptr [eax + 8]
0x49B8F0: push     eax
0x49B8F1: mov      eax, dword ptr [ebp - 0x14]
0x49B8F4: mov      al, byte ptr [eax + 0xc]
0x49B8F7: push     eax
0x49B8F8: mov      eax, dword ptr [ebp - 0x14]
0x49B8FB: mov      al, byte ptr [eax + 0x1c]
0x49B8FE: push     eax
0x49B8FF: mov      eax, dword ptr [ebp - 0x14]
0x49B902: mov      eax, dword ptr [eax + 0x20]
0x49B905: push     eax
0x49B906: lea      eax, [ebp - 0xd]
0x49B909: push     eax
0x49B90A: mov      eax, dword ptr [ebp - 0x14]
0x49B90D: mov      ecx, dword ptr [eax]
0x49B90F: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x49B914: mov      eax, dword ptr [eax]
0x49B916: mov      edx, dword ptr [ebp - 4]
0x49B919: call     0x471da4
0x49B91E: cmp      byte ptr [ebp - 0xd], 0
0x49B922: je       0x49b934
0x49B924: mov      eax, dword ptr [ebp - 4]
0x49B927: mov      eax, dword ptr [eax + 0x1a8]
0x49B92D: call     0x495794
0x49B932: jmp      0x49b942
0x49B934: mov      eax, dword ptr [ebp - 4]
0x49B937: mov      eax, dword ptr [eax + 0x1ac]
0x49B93D: call     0x495794
0x49B942: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49B947: mov      eax, dword ptr [eax]
0x49B949: cmp      eax, dword ptr [ebp - 4]
0x49B94C: jne      0x49b95b
0x49B94E: call     0x406154
0x49B953: mov      edx, dword ptr [0x4a3e7c] ; *0x4A3E7C->0x4F944C
0x49B959: mov      dword ptr [edx], eax
0x49B95B: mov      eax, dword ptr [ebp - 4]
0x49B95E: xor      edx, edx
0x49B960: mov      dword ptr [eax + 0x10c], edx
0x49B966: mov      eax, dword ptr [ebp - 4]
0x49B969: cmp      byte ptr [eax + 0xe], 0
0x49B96D: jne      0x49b97c
0x49B96F: mov      eax, dword ptr [ebp - 4]
0x49B972: xor      edx, edx
0x49B974: mov      dword ptr [eax + 0x1ec], edx
0x49B97A: jmp      0x49b989
0x49B97C: mov      eax, dword ptr [ebp - 4]
0x49B97F: mov      dword ptr [eax + 0x1ec], 0xfffffff6
0x49B989: call     0x406154
0x49B98E: mov      edx, dword ptr [ebp - 4]
0x49B991: mov      dword ptr [edx + 0x1f0], eax
0x49B997: jmp      0x49ba05
0x49B999: call     0x406154
0x49B99E: mov      edx, dword ptr [ebp - 4]
0x49B9A1: sub      eax, dword ptr [edx + 0x1fc]
0x49B9A7: cmp      eax, 0xc8
0x49B9AC: jbe      0x49ba05
0x49B9AE: call     0x406154
0x49B9B3: mov      edx, dword ptr [ebp - 4]
0x49B9B6: sub      eax, dword ptr [edx + 0x1f0]
0x49B9BC: cmp      eax, 0x1f4
0x49B9C1: jbe      0x49b9f9
0x49B9C3: call     0x406154
0x49B9C8: mov      edx, dword ptr [ebp - 4]
0x49B9CB: mov      dword ptr [edx + 0x1f0], eax
0x49B9D1: mov      eax, dword ptr [ebp - 4]
0x49B9D4: inc      dword ptr [eax + 0x1ec]
0x49B9DA: mov      eax, dword ptr [ebp - 4]
0x49B9DD: mov      eax, dword ptr [eax + 0x1ec]
0x49B9E3: mov      edx, dword ptr [ebp - 4]
0x49B9E6: cmp      eax, dword ptr [edx + 0x1f4]
0x49B9EC: jl       0x49b9f9
0x49B9EE: mov      eax, dword ptr [ebp - 4]
0x49B9F1: xor      edx, edx
0x49B9F3: mov      dword ptr [eax + 0x1ec], edx
0x49B9F9: mov      eax, dword ptr [ebp - 4]
0x49B9FC: mov      bx, 0xfffe
0x49BA00: call     0x403080
0x49BA05: mov      eax, dword ptr [ebp - 4]
0x49BA08: mov      eax, dword ptr [eax + 0x1b8]
0x49BA0E: cmp      eax, dword ptr [ebp - 8]
0x49BA11: je       0x49ba2d
0x49BA13: call     0x406154
0x49BA18: mov      edx, dword ptr [ebp - 4]
0x49BA1B: mov      dword ptr [edx + 0x204], eax
0x49BA21: mov      eax, dword ptr [ebp - 4]
0x49BA24: mov      bx, 0xfff3
0x49BA28: call     0x403080
0x49BA2D: pop      ebx
0x49BA2E: mov      esp, ebp
0x49BA30: pop      ebp
0x49BA31: ret      
0x49BA32: add      byte ptr [eax], al
0x49BA34: out      0xff, al
0x49BA3D: aas      
0x49BA3E: add      byte ptr [eax], al
0x49BA40: add      byte ptr [eax], al
0x49BA42: inc      eax
0x49BA43: inc      eax
0x49BA44: push     ebp
0x49BA45: mov      ebp, esp
0x49BA47: add      esp, -0xc
0x49BA4A: mov      dword ptr [ebp - 4], eax
0x49BA4D: mov      eax, dword ptr [ebp - 4]
0x49BA50: mov      eax, dword ptr [eax + 0x154]
0x49BA56: mov      dword ptr [ebp - 0xc], eax
0x49BA59: mov      eax, dword ptr [ebp - 0xc]
0x49BA5C: mov      edx, dword ptr [ebp - 4]
0x49BA5F: cmp      eax, dword ptr [edx + 0x158]
0x49BA65: jge      0x49ba8b
0x49BA67: mov      eax, dword ptr [ebp - 4]
0x49BA6A: cmp      byte ptr [eax + 0xec], 0
0x49BA71: jne      0x49ba7f
0x49BA73: mov      eax, dword ptr [ebp - 4]
0x49BA76: cmp      byte ptr [eax + 0xed], 0
0x49BA7D: je       0x49ba8b
0x49BA7F: mov      eax, dword ptr [ebp - 4]
0x49BA82: mov      eax, dword ptr [eax + 0x158]
0x49BA88: mov      dword ptr [ebp - 0xc], eax
0x49BA8B: mov      eax, dword ptr [ebp - 0xc]
0x49BA8E: mov      dword ptr [ebp - 8], eax
0x49BA91: mov      eax, dword ptr [ebp - 8]
0x49BA94: mov      esp, ebp
0x49BA96: pop      ebp
0x49BA97: ret      

; === THumActor.DMT_-13 @ 0x49BA98 ===
0x49BA98: push     ebp
0x49BA99: mov      ebp, esp
0x49BA9B: push     ecx
0x49BA9C: mov      dword ptr [ebp - 4], eax
0x49BA9F: mov      eax, dword ptr [ebp - 4]
0x49BAA2: add      eax, 0xac
0x49BAA7: push     eax
0x49BAA8: mov      eax, dword ptr [ebp - 4]
0x49BAAB: lea      ecx, [eax + 0xa0]
0x49BAB1: mov      eax, dword ptr [ebp - 4]
0x49BAB4: mov      edx, dword ptr [eax + 0xe0]
0x49BABA: mov      eax, dword ptr [ebp - 4]
0x49BABD: add      edx, dword ptr [eax + 0x1b8]
0x49BAC3: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x49BAC8: mov      eax, dword ptr [eax]
0x49BACA: mov      eax, dword ptr [eax + 0x304]
0x49BAD0: call     0x453a48
0x49BAD5: mov      edx, dword ptr [ebp - 4]
0x49BAD8: mov      dword ptr [edx + 0x140], eax
0x49BADE: mov      eax, dword ptr [ebp - 4]
0x49BAE1: cmp      dword ptr [eax + 0xe4], 0
0x49BAE8: jl       0x49bb2b
0x49BAEA: mov      eax, dword ptr [ebp - 4]
0x49BAED: add      eax, 0xb0
0x49BAF2: push     eax
0x49BAF3: mov      eax, dword ptr [ebp - 4]
0x49BAF6: lea      ecx, [eax + 0xa4]
0x49BAFC: mov      eax, dword ptr [ebp - 4]
0x49BAFF: mov      edx, dword ptr [eax + 0xe4]
0x49BB05: mov      eax, dword ptr [ebp - 4]
0x49BB08: add      edx, dword ptr [eax + 0x1b8]
0x49BB0E: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x49BB13: mov      eax, dword ptr [eax]
0x49BB15: mov      eax, dword ptr [eax + 0x320]
0x49BB1B: call     0x453a48
0x49BB20: mov      edx, dword ptr [ebp - 4]
0x49BB23: mov      dword ptr [edx + 0x244], eax
0x49BB29: jmp      0x49bb36
0x49BB2B: mov      eax, dword ptr [ebp - 4]
0x49BB2E: xor      edx, edx
0x49BB30: mov      dword ptr [eax + 0x244], edx
0x49BB36: mov      eax, dword ptr [ebp - 4]
0x49BB39: add      eax, 0xb4
0x49BB3E: push     eax
0x49BB3F: mov      eax, dword ptr [ebp - 4]
0x49BB42: lea      ecx, [eax + 0xa8]
0x49BB48: mov      eax, dword ptr [ebp - 4]
0x49BB4B: mov      edx, dword ptr [eax + 0xe8]
0x49BB51: mov      eax, dword ptr [ebp - 4]
0x49BB54: add      edx, dword ptr [eax + 0x1b8]
0x49BB5A: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x49BB5F: mov      eax, dword ptr [eax]
0x49BB61: mov      eax, dword ptr [eax + 0x328]
0x49BB67: call     0x453a48
0x49BB6C: mov      edx, dword ptr [ebp - 4]
0x49BB6F: mov      dword ptr [edx + 0x248], eax
0x49BB75: pop      ecx
0x49BB76: pop      ebp
0x49BB77: ret      

; === THumActor.DMT_-14 @ 0x49BB78 ===
0x49BB78: push     ebp
0x49BB79: mov      ebp, esp
0x49BB7B: add      esp, -0x44
0x49BB7E: push     ebx
0x49BB7F: mov      dword ptr [ebp - 0xc], ecx
0x49BB82: mov      dword ptr [ebp - 8], edx
0x49BB85: mov      dword ptr [ebp - 4], eax
0x49BB88: mov      eax, dword ptr [ebp - 4]
0x49BB8B: mov      al, byte ptr [eax + 0xc]
0x49BB8E: sub      al, 8
0x49BB90: jae      0x49c097
0x49BB96: call     0x406154
0x49BB9B: mov      edx, dword ptr [ebp - 4]
0x49BB9E: sub      eax, dword ptr [edx + 0x204]
0x49BBA4: cmp      eax, 0xea60
0x49BBA9: jbe      0x49bbc5
0x49BBAB: call     0x406154
0x49BBB0: mov      edx, dword ptr [ebp - 4]
0x49BBB3: mov      dword ptr [edx + 0x204], eax
0x49BBB9: mov      eax, dword ptr [ebp - 4]
0x49BBBC: mov      bx, 0xfff3
0x49BBC0: call     0x403080
0x49BBC5: mov      eax, dword ptr [ebp - 4]
0x49BBC8: call     0x498170
0x49BBCD: mov      byte ptr [ebp - 0x1d], al
0x49BBD0: mov      eax, dword ptr [ebp - 4]
0x49BBD3: cmp      byte ptr [eax + 0xe], 0
0x49BBD7: jne      0x49beb2
0x49BBDD: mov      eax, dword ptr [ebp - 4]
0x49BBE0: cmp      dword ptr [eax + 0x1b8], 0
0x49BBE7: jl       0x49bc1f
0x49BBE9: mov      eax, dword ptr [ebp - 4]
0x49BBEC: cmp      dword ptr [eax + 0x1b8], 0x257
0x49BBF6: jg       0x49bc1f
0x49BBF8: mov      eax, dword ptr [ebp - 4]
0x49BBFB: movzx    eax, byte ptr [eax + 0xd]
0x49BBFF: imul     eax, eax, 0x4b
0x49BC02: lea      eax, [eax*8 + 0x4a3834] ; *0x4A3834->0x000000
0x49BC09: mov      edx, dword ptr [ebp - 4]
0x49BC0C: mov      edx, dword ptr [edx + 0x1b8]
0x49BC12: movzx    eax, byte ptr [eax + edx]
0x49BC16: mov      edx, dword ptr [ebp - 4]
0x49BC19: mov      dword ptr [edx + 0x21c], eax
0x49BC1F: mov      eax, dword ptr [ebp - 4]
0x49BC22: cmp      dword ptr [eax + 0x21c], 0
0x49BC29: jne      0x49bcc8
0x49BC2F: cmp      byte ptr [ebp + 8], 0
0x49BC33: jne      0x49bcc8
0x49BC39: mov      eax, dword ptr [ebp - 4]
0x49BC3C: cmp      byte ptr [eax + 0x11], 2
0x49BC40: jb       0x49bcc8
0x49BC46: mov      eax, dword ptr [ebp - 4]
0x49BC49: cmp      dword ptr [eax + 0x248], 0
0x49BC50: je       0x49bcc8
0x49BC52: mov      eax, dword ptr [ebp - 4]
0x49BC55: cmp      byte ptr [eax + 0x25c], 0
0x49BC5C: jne      0x49bcc8
0x49BC5E: mov      eax, dword ptr [ebp - 0xc]
0x49BC61: mov      edx, dword ptr [ebp - 4]
0x49BC64: add      eax, dword ptr [edx + 0xa8]
0x49BC6A: mov      edx, dword ptr [ebp - 4]
0x49BC6D: add      eax, dword ptr [edx + 0x98]
0x49BC73: push     eax
0x49BC74: mov      eax, dword ptr [ebp + 0xc]
0x49BC77: mov      edx, dword ptr [ebp - 4]
0x49BC7A: add      eax, dword ptr [edx + 0xb4]
0x49BC80: mov      edx, dword ptr [ebp - 4]
0x49BC83: add      eax, dword ptr [edx + 0x9c]
0x49BC89: push     eax
0x49BC8A: mov      al, byte ptr [ebp + 8]
0x49BC8D: push     eax
0x49BC8E: push     0
0x49BC90: mov      eax, dword ptr [ebp - 4]
0x49BC93: mov      ecx, dword ptr [eax + 0x248]
0x49BC99: mov      edx, dword ptr [ebp - 8]
0x49BC9C: mov      eax, dword ptr [ebp - 4]
0x49BC9F: call     0x497fd0
0x49BCA4: mov      eax, dword ptr [ebp + 0xc]
0x49BCA7: mov      edx, dword ptr [ebp - 4]
0x49BCAA: add      eax, dword ptr [edx + 0x9c]
0x49BCB0: push     eax
0x49BCB1: mov      ecx, dword ptr [ebp - 0xc]
0x49BCB4: mov      eax, dword ptr [ebp - 4]
0x49BCB7: add      ecx, dword ptr [eax + 0x98]
0x49BCBD: mov      edx, dword ptr [ebp - 8]
0x49BCC0: mov      eax, dword ptr [ebp - 4]
0x49BCC3: call     0x498158
0x49BCC8: mov      eax, dword ptr [ebp - 4]
0x49BCCB: cmp      dword ptr [eax + 0x140], 0
0x49BCD2: je       0x49bd1c
0x49BCD4: mov      eax, dword ptr [ebp - 0xc]
0x49BCD7: mov      edx, dword ptr [ebp - 4]
0x49BCDA: add      eax, dword ptr [edx + 0xa0]
0x49BCE0: mov      edx, dword ptr [ebp - 4]
0x49BCE3: add      eax, dword ptr [edx + 0x98]
0x49BCE9: push     eax
0x49BCEA: mov      eax, dword ptr [ebp + 0xc]
0x49BCED: mov      edx, dword ptr [ebp - 4]
0x49BCF0: add      eax, dword ptr [edx + 0xac]
0x49BCF6: mov      edx, dword ptr [ebp - 4]
0x49BCF9: add      eax, dword ptr [edx + 0x9c]
0x49BCFF: push     eax
0x49BD00: mov      al, byte ptr [ebp + 8]
0x49BD03: push     eax
0x49BD04: mov      al, byte ptr [ebp - 0x1d]
0x49BD07: push     eax
0x49BD08: mov      eax, dword ptr [ebp - 4]
0x49BD0B: mov      ecx, dword ptr [eax + 0x140]
0x49BD11: mov      edx, dword ptr [ebp - 8]
0x49BD14: mov      eax, dword ptr [ebp - 4]
0x49BD17: call     0x497fd0
0x49BD1C: mov      eax, dword ptr [ebp - 4]
0x49BD1F: cmp      dword ptr [eax + 0x244], 0
0x49BD26: je       0x49bd70
0x49BD28: mov      eax, dword ptr [ebp - 0xc]
0x49BD2B: mov      edx, dword ptr [ebp - 4]
0x49BD2E: add      eax, dword ptr [edx + 0xa4]
0x49BD34: mov      edx, dword ptr [ebp - 4]
0x49BD37: add      eax, dword ptr [edx + 0x98]
0x49BD3D: push     eax
0x49BD3E: mov      eax, dword ptr [ebp + 0xc]
0x49BD41: mov      edx, dword ptr [ebp - 4]
0x49BD44: add      eax, dword ptr [edx + 0xb0]
0x49BD4A: mov      edx, dword ptr [ebp - 4]
0x49BD4D: add      eax, dword ptr [edx + 0x9c]
0x49BD53: push     eax
0x49BD54: mov      al, byte ptr [ebp + 8]
0x49BD57: push     eax
0x49BD58: mov      al, byte ptr [ebp - 0x1d]
0x49BD5B: push     eax
0x49BD5C: mov      eax, dword ptr [ebp - 4]
0x49BD5F: mov      ecx, dword ptr [eax + 0x244]
0x49BD65: mov      edx, dword ptr [ebp - 8]
0x49BD68: mov      eax, dword ptr [ebp - 4]
0x49BD6B: call     0x497fd0
0x49BD70: mov      eax, dword ptr [ebp - 4]
0x49BD73: cmp      dword ptr [eax + 0x21c], 1
0x49BD7A: jne      0x49be0f
0x49BD80: mov      eax, dword ptr [ebp - 4]
0x49BD83: cmp      byte ptr [eax + 0x11], 2
0x49BD87: jb       0x49be0f
0x49BD8D: mov      eax, dword ptr [ebp - 4]
0x49BD90: cmp      dword ptr [eax + 0x248], 0
0x49BD97: je       0x49be0f
0x49BD99: mov      eax, dword ptr [ebp - 4]
0x49BD9C: cmp      byte ptr [eax + 0x25c], 0
0x49BDA3: jne      0x49be0f
0x49BDA5: mov      eax, dword ptr [ebp - 0xc]
0x49BDA8: mov      edx, dword ptr [ebp - 4]
0x49BDAB: add      eax, dword ptr [edx + 0xa8]
0x49BDB1: mov      edx, dword ptr [ebp - 4]
0x49BDB4: add      eax, dword ptr [edx + 0x98]
0x49BDBA: push     eax
0x49BDBB: mov      eax, dword ptr [ebp + 0xc]
0x49BDBE: mov      edx, dword ptr [ebp - 4]
0x49BDC1: add      eax, dword ptr [edx + 0xb4]
0x49BDC7: mov      edx, dword ptr [ebp - 4]
0x49BDCA: add      eax, dword ptr [edx + 0x9c]
0x49BDD0: push     eax
0x49BDD1: mov      al, byte ptr [ebp + 8]
0x49BDD4: push     eax
0x49BDD5: push     0
0x49BDD7: mov      eax, dword ptr [ebp - 4]
0x49BDDA: mov      ecx, dword ptr [eax + 0x248]
0x49BDE0: mov      edx, dword ptr [ebp - 8]
0x49BDE3: mov      eax, dword ptr [ebp - 4]
0x49BDE6: call     0x497fd0
0x49BDEB: mov      eax, dword ptr [ebp + 0xc]
0x49BDEE: mov      edx, dword ptr [ebp - 4]
0x49BDF1: add      eax, dword ptr [edx + 0x9c]
0x49BDF7: push     eax
0x49BDF8: mov      ecx, dword ptr [ebp - 0xc]
0x49BDFB: mov      eax, dword ptr [ebp - 4]
0x49BDFE: add      ecx, dword ptr [eax + 0x98]
0x49BE04: mov      edx, dword ptr [ebp - 8]
0x49BE07: mov      eax, dword ptr [ebp - 4]
0x49BE0A: call     0x498158
0x49BE0F: mov      eax, dword ptr [ebp - 4]
0x49BE12: test     byte ptr [eax + 0x1e], 0x10
0x49BE16: je       0x49beb2
0x49BE1C: mov      eax, dword ptr [ebp - 4]
0x49BE1F: cmp      dword ptr [eax + 0x148], 0x1f
0x49BE26: jne      0x49be47
0x49BE28: mov      eax, dword ptr [ebp - 4]
0x49BE2B: cmp      dword ptr [eax + 0x254], 3
0x49BE32: jge      0x49be47
0x49BE34: mov      eax, dword ptr [ebp - 4]
0x49BE37: mov      eax, dword ptr [eax + 0x254]
0x49BE3D: add      eax, 0xf3c
0x49BE42: mov      dword ptr [ebp - 0x10], eax
0x49BE45: jmp      0x49be61
0x49BE47: mov      eax, dword ptr [ebp - 4]
0x49BE4A: mov      eax, dword ptr [eax + 0x130]
0x49BE50: mov      ecx, 3
0x49BE55: cdq      
0x49BE56: idiv     ecx
0x49BE58: add      edx, 0xf32
0x49BE5E: mov      dword ptr [ebp - 0x10], edx
0x49BE61: lea      eax, [ebp - 0x18]
0x49BE64: push     eax
0x49BE65: lea      ecx, [ebp - 0x14]
0x49BE68: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x49BE6D: mov      eax, dword ptr [eax]
0x49BE6F: mov      eax, dword ptr [eax + 0x33c]
0x49BE75: mov      edx, dword ptr [ebp - 0x10]
0x49BE78: call     0x453a48
0x49BE7D: mov      dword ptr [ebp - 0x1c], eax
0x49BE80: cmp      dword ptr [ebp - 0x1c], 0
0x49BE84: je       0x49beb2
0x49BE86: mov      eax, dword ptr [ebp - 0x1c]
0x49BE89: push     eax
0x49BE8A: push     1
0x49BE8C: mov      ecx, dword ptr [ebp + 0xc]
0x49BE8F: add      ecx, dword ptr [ebp - 0x18]
0x49BE92: mov      eax, dword ptr [ebp - 4]
0x49BE95: add      ecx, dword ptr [eax + 0x9c]
0x49BE9B: mov      edx, dword ptr [ebp - 0xc]
0x49BE9E: add      edx, dword ptr [ebp - 0x14]
0x49BEA1: mov      eax, dword ptr [ebp - 4]
0x49BEA4: add      edx, dword ptr [eax + 0x98]
0x49BEAA: mov      eax, dword ptr [ebp - 8]
0x49BEAD: call     0x455578
0x49BEB2: mov      eax, dword ptr [ebp - 4]
0x49BEB5: cmp      byte ptr [eax + 0xec], 0
0x49BEBC: je       0x49bf6f
0x49BEC2: mov      eax, dword ptr [ebp - 4]
0x49BEC5: cmp      dword ptr [eax + 0x11c], 0
0x49BECC: jle      0x49bf6f
0x49BED2: xor      eax, eax
0x49BED4: mov      edx, dword ptr [ebp - 4]
0x49BED7: mov      edx, dword ptr [edx + 0x108]
0x49BEDD: dec      edx
0x49BEDE: lea      ecx, [ebp - 0x44]
0x49BEE1: mov      ah, 0x20
0x49BEE3: call     0x402c94
0x49BEE8: mov      eax, dword ptr [ebp - 4]
0x49BEEB: mov      eax, dword ptr [eax + 0x104]
0x49BEF1: cmp      eax, 0xff
0x49BEF6: ja       0x49befc
0x49BEF8: bt       dword ptr [ebp - 0x44], eax
0x49BEFC: jae      0x49bf6f
0x49BEFE: lea      eax, [ebp - 0x10]
0x49BF01: push     eax
0x49BF02: lea      ecx, [ebp - 0x24]
0x49BF05: mov      eax, dword ptr [ebp - 4]
0x49BF08: mov      eax, dword ptr [eax + 0x11c]
0x49BF0E: dec      eax
0x49BF0F: xor      edx, edx
0x49BF11: call     0x49c5bc
0x49BF16: mov      eax, dword ptr [ebp - 4]
0x49BF19: mov      eax, dword ptr [eax + 0x104]
0x49BF1F: add      dword ptr [ebp - 0x10], eax
0x49BF22: cmp      dword ptr [ebp - 0x24], 0
0x49BF26: je       0x49bf3d
0x49BF28: lea      eax, [ebp - 0x18]
0x49BF2B: push     eax
0x49BF2C: lea      ecx, [ebp - 0x14]
0x49BF2F: mov      edx, dword ptr [ebp - 0x10]
0x49BF32: mov      eax, dword ptr [ebp - 0x24]
0x49BF35: call     0x453a48
0x49BF3A: mov      dword ptr [ebp - 0x1c], eax
0x49BF3D: cmp      dword ptr [ebp - 0x1c], 0
0x49BF41: je       0x49bf6f
0x49BF43: mov      eax, dword ptr [ebp - 0x1c]
0x49BF46: push     eax
0x49BF47: push     1
0x49BF49: mov      ecx, dword ptr [ebp + 0xc]
0x49BF4C: add      ecx, dword ptr [ebp - 0x18]
0x49BF4F: mov      eax, dword ptr [ebp - 4]
0x49BF52: add      ecx, dword ptr [eax + 0x9c]
0x49BF58: mov      edx, dword ptr [ebp - 0xc]
0x49BF5B: add      edx, dword ptr [ebp - 0x14]
0x49BF5E: mov      eax, dword ptr [ebp - 4]
0x49BF61: add      edx, dword ptr [eax + 0x98]
0x49BF67: mov      eax, dword ptr [ebp - 8]
0x49BF6A: call     0x455578
0x49BF6F: mov      eax, dword ptr [ebp - 4]
0x49BF72: cmp      byte ptr [eax + 0xed], 0
0x49BF79: je       0x49c01d
0x49BF7F: mov      eax, dword ptr [ebp - 4]
0x49BF82: cmp      dword ptr [eax + 0xf0], 0
0x49BF89: jle      0x49c01d
0x49BF8F: lea      eax, [ebp - 0x10]
0x49BF92: push     eax
0x49BF93: lea      ecx, [ebp - 0x24]
0x49BF96: mov      eax, dword ptr [ebp - 4]
0x49BF99: mov      eax, dword ptr [eax + 0xf0]
0x49BF9F: dec      eax
0x49BFA0: mov      edx, 1
0x49BFA5: call     0x49c5bc
0x49BFAA: mov      eax, dword ptr [ebp - 4]
0x49BFAD: movzx    eax, byte ptr [eax + 0xc]
0x49BFB1: add      eax, eax
0x49BFB3: lea      eax, [eax + eax*4]
0x49BFB6: add      eax, dword ptr [ebp - 0x10]
0x49BFB9: mov      edx, dword ptr [ebp - 4]
0x49BFBC: mov      edx, dword ptr [edx + 0x1b8]
0x49BFC2: mov      ecx, dword ptr [ebp - 4]
0x49BFC5: sub      edx, dword ptr [ecx + 0x1b0]
0x49BFCB: add      eax, edx
0x49BFCD: mov      dword ptr [ebp - 0x10], eax
0x49BFD0: cmp      dword ptr [ebp - 0x24], 0
0x49BFD4: je       0x49bfeb
0x49BFD6: lea      eax, [ebp - 0x18]
0x49BFD9: push     eax
0x49BFDA: lea      ecx, [ebp - 0x14]
0x49BFDD: mov      edx, dword ptr [ebp - 0x10]
0x49BFE0: mov      eax, dword ptr [ebp - 0x24]
0x49BFE3: call     0x453a48
0x49BFE8: mov      dword ptr [ebp - 0x1c], eax
0x49BFEB: cmp      dword ptr [ebp - 0x1c], 0
0x49BFEF: je       0x49c01d
0x49BFF1: mov      eax, dword ptr [ebp - 0x1c]
0x49BFF4: push     eax
0x49BFF5: push     1
0x49BFF7: mov      ecx, dword ptr [ebp + 0xc]
0x49BFFA: add      ecx, dword ptr [ebp - 0x18]
0x49BFFD: mov      eax, dword ptr [ebp - 4]
0x49C000: add      ecx, dword ptr [eax + 0x9c]
0x49C006: mov      edx, dword ptr [ebp - 0xc]
0x49C009: add      edx, dword ptr [ebp - 0x14]
0x49C00C: mov      eax, dword ptr [ebp - 4]
0x49C00F: add      edx, dword ptr [eax + 0x98]
0x49C015: mov      eax, dword ptr [ebp - 8]
0x49C018: call     0x455578
0x49C01D: mov      eax, dword ptr [ebp - 4]
0x49C020: cmp      byte ptr [eax + 0x24c], 0
0x49C027: je       0x49c097
0x49C029: mov      eax, dword ptr [ebp - 4]
0x49C02C: movzx    eax, byte ptr [eax + 0xc]
0x49C030: add      eax, eax
0x49C032: lea      eax, [eax + eax*4]
0x49C035: add      eax, 0xea6
0x49C03A: mov      edx, dword ptr [ebp - 4]
0x49C03D: add      eax, dword ptr [edx + 0x250]
0x49C043: mov      dword ptr [ebp - 0x10], eax
0x49C046: lea      eax, [ebp - 0x18]
0x49C049: push     eax
0x49C04A: lea      ecx, [ebp - 0x14]
0x49C04D: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x49C052: mov      eax, dword ptr [eax]
0x49C054: mov      eax, dword ptr [eax + 0x33c]
0x49C05A: mov      edx, dword ptr [ebp - 0x10]
0x49C05D: call     0x453a48
0x49C062: mov      dword ptr [ebp - 0x1c], eax
0x49C065: cmp      dword ptr [ebp - 0x1c], 0
0x49C069: je       0x49c097
0x49C06B: mov      eax, dword ptr [ebp - 0x1c]
0x49C06E: push     eax
0x49C06F: push     1
0x49C071: mov      ecx, dword ptr [ebp + 0xc]
0x49C074: add      ecx, dword ptr [ebp - 0x18]
0x49C077: mov      eax, dword ptr [ebp - 4]
0x49C07A: add      ecx, dword ptr [eax + 0x9c]
0x49C080: mov      edx, dword ptr [ebp - 0xc]
0x49C083: add      edx, dword ptr [ebp - 0x14]
0x49C086: mov      eax, dword ptr [ebp - 4]
0x49C089: add      edx, dword ptr [eax + 0x98]
0x49C08F: mov      eax, dword ptr [ebp - 8]
0x49C092: call     0x455578
0x49C097: pop      ebx
0x49C098: mov      esp, ebp
0x49C09A: pop      ebp
0x49C09B: ret      8
0x49C09E: mov      eax, eax
0x49C0A0: push     ebp
0x49C0A1: mov      ebp, esp
0x49C0A3: xor      eax, eax
0x49C0A5: push     ebp
0x49C0A6: push     0x49c0c5
0x49C0AB: push     dword ptr fs:[eax]
0x49C0AE: mov      dword ptr fs:[eax], esp
0x49C0B1: inc      dword ptr [0x4f95d8]
0x49C0B7: xor      eax, eax
0x49C0B9: pop      edx
0x49C0BA: pop      ecx
0x49C0BB: pop      ecx
0x49C0BC: mov      dword ptr fs:[eax], edx
0x49C0BF: push     0x49c0cc
0x49C0C4: ret      
0x49C0C5: jmp      0x403518
0x49C0CA: jmp      0x49c0c4
0x49C0CC: pop      ebp
0x49C0CD: ret      
0x49C0CE: mov      eax, eax
0x49C0D0: sub      dword ptr [0x4f95d8], 1
0x49C0D7: ret      
0x49C0D8: and      al, 0xc1
0x49C0DA: dec      ecx
0x49C0DB: add      byte ptr [eax], al
0x49C0DD: add      byte ptr [eax], al
0x49C0DF: add      byte ptr [eax], al
0x49C0E1: add      byte ptr [eax], al
0x49C0E3: add      byte ptr [eax], al
0x49C0E5: add      byte ptr [eax], al
0x49C0E7: add      byte ptr [eax], al
0x49C0E9: add      byte ptr [eax], al
0x49C0EB: add      byte ptr [eax], al
0x49C0ED: add      byte ptr [eax], al
0x49C0EF: add      byte ptr [eax], al
0x49C0F1: add      byte ptr [eax], al
0x49C0F3: add      byte ptr [ecx + eax*8], ah
0x49C0F6: dec      ecx
0x49C0F7: add      byte ptr [eax], bh
0x49C0F9: ror      dword ptr [ecx], 0xc0
0x49C0FD: add      byte ptr [eax], al
0x49C0FF: add      byte ptr [eax - 0x37ffbff0], ah
0x49C105: xor      byte ptr [eax], al
0x49C108: aam      0x30
0x49C10A: inc      eax
0x49C10B: add      al, bl
0x49C10D: xor      byte ptr [eax], al
0x49C110: fdiv     qword ptr [eax]
0x49C112: inc      eax
0x49C113: add      al, dl
0x49C115: xor      byte ptr [eax], al
0x49C118: inc      eax
0x49C119: inc      eax
0x49C11B: add      byte ptr [esi + ebp + 0x40], dl
0x49C11F: add      al, dl
0x49C121: retf     0x49
