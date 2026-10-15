
; === VMT_TScene @ 0x47AB78 ===
0x47AB78: or       al, byte ptr [eax]
0x47AB7E: std      
0x47AB80: cld      
0x47AB82: sti      
0x47AB84: cli      
0x47AB86: stc      
0x47AB88: clc      
0x47AB89: push     edi
0x47AB8B: push     esi
0x47AB8D: inc      eax
0x47AB8F: lodsd    eax, dword ptr [esi]
0x47AB90: inc      edi
0x47AB91: add      ah, cl
0x47AB93: lodsd    eax, dword ptr [esi]
0x47AB94: inc      edi
0x47AB95: add      al, bl
0x47AB97: lodsd    eax, dword ptr [esi]
0x47AB98: inc      edi
0x47AB99: add      ah, ah
0x47AB9B: lodsd    eax, dword ptr [esi]
0x47AB9C: inc      edi
0x47AB9D: add      al, dh
0x47AB9F: lodsd    eax, dword ptr [esi]
0x47ABA0: inc      edi
0x47ABA1: add      ah, bh
0x47ABA3: lodsd    eax, dword ptr [esi]
0x47ABA4: inc      edi
0x47ABA5: add      byte ptr [esi + ebp*4], cl
0x47ABA8: inc      edi
0x47ABA9: add      byte ptr [eax], ah
0x47ABAB: scasb    al, byte ptr es:[edi]
0x47ABAC: inc      edi
0x47ABAD: add      byte ptr [eax], bh
0x47ABAF: scasb    al, byte ptr es:[edi]
0x47ABB0: inc      edi
0x47ABB1: add      byte ptr [eax - 0x52], dl
0x47ABB4: inc      edi
0x47ABB5: add      byte ptr [esi], al
0x47ABB7: push     esp
0x47ABB8: push     ebx
0x47ABB9: arpl     word ptr [ebp + 0x6e], sp
0x47ABBC: lea      eax, gs:[eax]
0x47ABC0: or       al, 0xac
0x47ABC2: inc      edi
0x47ABC3: add      byte ptr [eax], al
0x47ABC5: add      byte ptr [eax], al
0x47ABC7: add      byte ptr [eax], al
0x47ABC9: add      byte ptr [eax], al
0x47ABCB: add      byte ptr [eax], al
0x47ABCD: add      byte ptr [eax], al
0x47ABCF: add      byte ptr [eax], al
0x47ABD1: add      byte ptr [eax], al
0x47ABD3: add      byte ptr [eax], al
0x47ABD5: add      byte ptr [eax], al
0x47ABD7: add      byte ptr [eax], al
0x47ABD9: add      byte ptr [eax], al
0x47ABDB: add      byte ptr [esp + ebp*4], cl
0x47ABDE: inc      edi
0x47ABDF: add      byte ptr [eax], ah
0x47ABE1: lodsb    al, byte ptr [esi]
0x47ABE2: inc      edi
0x47ABE3: add      byte ptr [eax], cl
0x47ABE5: add      byte ptr [eax], al
0x47ABE7: add      byte ptr [ebx + ebp*4], ch
0x47ABEA: inc      edi
0x47ABEB: add      al, cl
0x47ABED: xor      byte ptr [eax], al
0x47ABF0: aam      0x30
0x47ABF2: inc      eax
0x47ABF3: add      al, bl
0x47ABF5: xor      byte ptr [eax], al
0x47ABF8: fdiv     qword ptr [eax]
0x47ABFA: inc      eax
0x47ABFB: add      al, dl
0x47ABFD: xor      byte ptr [eax], al
0x47AC00: inc      eax
0x47AC01: inc      eax
0x47AC03: add      byte ptr [esi + ebp + 0x40], dl
0x47AC07: add      byte ptr [esi + ebp*4 + 0x30047], ah
0x47AC0E: std      
0x47AC10: cld      
0x47AC11: push     esi
0x47AC14: fsubr    dword ptr [esi - 0x511bffb9]
0x47AC1A: inc      edi
0x47AC1B: add      al, dh
0x47AC1D: scasb    al, byte ptr es:[edi]
0x47AC1E: inc      edi
0x47AC1F: add      byte ptr [ebx], cl
0x47AC21: push     esp
0x47AC22: dec      ecx
0x47AC23: outsb    dx, byte ptr [esi]
0x47AC24: je       0x47ac98
0x47AC26: outsd    dx, dword ptr [esi]
0x47AC27: push     ebx
0x47AC28: arpl     word ptr [ebp + 0x6e], sp
0x47AC2B: js       0x47abda
0x47AC2E: inc      edi
0x47AC2F: add      byte ptr [eax], al
0x47AC31: add      byte ptr [eax], al
0x47AC33: add      byte ptr [eax], al
0x47AC35: add      byte ptr [eax], al
0x47AC37: add      byte ptr [eax - 0x54], bh
0x47AC3A: inc      edi
0x47AC3B: add      byte ptr [eax], al
0x47AC3D: add      byte ptr [eax], al
0x47AC3F: add      byte ptr [eax], al
0x47AC41: add      byte ptr [eax], al
0x47AC43: add      byte ptr [eax], al
0x47AC45: add      byte ptr [eax], al
0x47AC47: add      byte ptr [edx - 0x59ffb854], dl
0x47AC4D: lodsb    al, byte ptr [esi]
0x47AC4E: inc      edi
0x47AC4F: add      byte ptr [eax + 1], ch
0x47AC52: add      byte ptr [eax], al
0x47AC54: sub      al, 0xab
0x47AC56: inc      edi
0x47AC57: add      al, cl
0x47AC59: xor      byte ptr [eax], al
0x47AC5C: aam      0x30
0x47AC5E: inc      eax
0x47AC5F: add      al, bl
0x47AC61: xor      byte ptr [eax], al
0x47AC64: fdiv     qword ptr [eax]
0x47AC66: inc      eax
0x47AC67: add      al, dl
0x47AC69: xor      byte ptr [eax], al
0x47AC6C: inc      eax
0x47AC6D: inc      eax
0x47AC6F: add      byte ptr [esi + ebp + 0x40], dl
0x47AC73: add      al, bl
0x47AC75: mov      edi, 0xe0047
0x47AC7A: add      byte ptr [eax], al
0x47AC7C: add      byte ptr [eax], al
0x47AC7E: add      al, byte ptr [eax]
0x47AC80: add      byte ptr [eax], al
0x47AC82: xchg     esp, eax
0x47AC83: adc      byte ptr [eax], al
0x47AC86: pop      esp
0x47AC87: add      dword ptr [eax], eax
0x47AC89: add      byte ptr [eax + edx + 0x1600040], dl
0x47AC90: add      byte ptr [eax], al
0x47AC92: add      eax, dword ptr [eax]
0x47AC94: std      
0x47AC96: cld      
0x47AC97: push     esi
0x47AC99: dec      dword ptr [eax + eax*8]
0x47AC9C: inc      edi
0x47AC9D: add      byte ptr [ecx + eax*8], dh
0x47ACA0: inc      edi
0x47ACA1: add      ah, cl
0x47ACA3: salc     
0x47ACA4: inc      edi
0x47ACA5: add      byte ptr [ebx], cl
0x47ACA7: push     esp
0x47ACA8: dec      esp
0x47ACA9: outsd    dx, dword ptr [esi]
0x47ACAA: imul     ebp, dword ptr [bp + 0x53], 0x656e6563
0x47ACB2: mov      eax, eax
0x47ACB4: add      byte ptr [ebp + 0x47], ch
0x47ACBA: add      byte ptr [eax], al
0x47ACBC: add      byte ptr [eax], al
0x47ACBE: add      byte ptr [eax], al
0x47ACC0: add      byte ptr [eax], al
0x47ACC2: add      byte ptr [eax], al
0x47ACC4: add      byte ptr [eax], al
0x47ACC6: add      byte ptr [eax], al
0x47ACC8: add      byte ptr [eax], al
0x47ACCA: add      byte ptr [eax], al
0x47ACCC: add      byte ptr [eax], al
0x47ACCE: add      byte ptr [eax], al
0x47ACD0: add      byte ptr [ebp - 0x52ebffb9], ch
0x47ACD6: inc      edi
0x47ACD7: add      byte ptr [eax + 0x2c000000], cl
0x47ACDD: stosd    dword ptr es:[edi], eax
0x47ACDE: inc      edi
0x47ACDF: add      al, cl
0x47ACE1: xor      byte ptr [eax], al
0x47ACE4: aam      0x30
0x47ACE6: inc      eax
0x47ACE7: add      al, bl
0x47ACE9: xor      byte ptr [eax], al
0x47ACEC: fdiv     qword ptr [eax]
0x47ACEE: inc      eax
0x47ACEF: add      al, dl
0x47ACF1: xor      byte ptr [eax], al
0x47ACF4: inc      eax
0x47ACF5: inc      eax
0x47ACF7: add      byte ptr [esi + ebp + 0x40], dl
0x47ACFB: add      ah, ah
0x47ACFD: out      0x47, al
0x47ACFF: add      byte ptr [ebx], al
0x47AD01: add      ch, bh
0x47AD04: cld      
0x47AD05: push     esi
0x47AD07: call     ptr [eax]
0x47AD09: out      0x47, eax
0x47AD0B: add      byte ptr [edi + 0x47], dl
0x47AD0F: add      byte ptr [edx + esi*8 + 0x47], ah
0x47AD13: add      byte ptr [edi], cl
0x47AD15: push     esp
0x47AD16: push     ebx
0x47AD17: insb     byte ptr es:[edi], dx
0x47AD19: arpl     word ptr gs:[ebx + eax*2 + 0x68], si
0x47AD1E: jb       0x47ad73
0x47AD20: arpl     word ptr [ebp + 0x6e], sp
0x47AD23: jo       0x47acd3
0x47AD26: inc      edi
0x47AD27: add      byte ptr [eax], al
0x47AD29: add      byte ptr [eax], al
0x47AD2B: add      byte ptr [eax], al
0x47AD2D: add      byte ptr [eax], al
0x47AD2F: add      byte ptr [eax], al
0x47AD31: add      byte ptr [eax], al
0x47AD33: add      byte ptr [eax], al
0x47AD35: add      byte ptr [eax], al
0x47AD37: add      byte ptr [eax], al
0x47AD39: add      byte ptr [eax], al
0x47AD3B: add      byte ptr [eax], al
0x47AD3D: add      byte ptr [eax], al
0x47AD3F: add      byte ptr [eax], al
0x47AD41: add      byte ptr [eax], al
0x47AD43: add      byte ptr [eax - 0x53], dh
0x47AD46: inc      edi
0x47AD47: add      byte ptr [eax], cl
0x47AD49: add      byte ptr [eax], al
0x47AD4B: add      byte ptr [ebx + ebp*4], ch
0x47AD4E: inc      edi
0x47AD4F: add      al, cl
0x47AD51: xor      byte ptr [eax], al
0x47AD54: aam      0x30
0x47AD56: inc      eax
0x47AD57: add      al, bl
0x47AD59: xor      byte ptr [eax], al
0x47AD5C: fdiv     qword ptr [eax]
0x47AD5E: inc      eax
0x47AD5F: add      al, dl
0x47AD61: xor      byte ptr [eax], al
0x47AD64: inc      eax
0x47AD65: inc      eax
0x47AD67: add      byte ptr [esi + ebp + 0x40], dl
0x47AD6B: add      byte ptr [ebp + edi*8 + 0x47], al
0x47AD6F: add      byte ptr [esp + edx*2], cl
0x47AD72: dec      esp
0x47AD73: outsd    dx, dword ptr [esi]
0x47AD74: imul     ebp, dword ptr [bp + 0x4e], 0x6369746f
0x47AD7C: lea      eax, gs:[eax]
0x47AD80: push     ebp
0x47AD81: mov      ebp, esp
0x47AD83: add      esp, -8
0x47AD86: test     dl, dl
0x47AD88: je       0x47ad92
0x47AD8A: add      esp, -0x10
0x47AD8D: call     0x4031a4
0x47AD92: mov      byte ptr [ebp - 6], cl
0x47AD95: mov      byte ptr [ebp - 5], dl
0x47AD98: mov      dword ptr [ebp - 4], eax
0x47AD9B: mov      al, byte ptr [ebp - 6]
0x47AD9E: mov      byte ptr [ebp - 6], al
0x47ADA1: mov      eax, dword ptr [ebp - 4]
0x47ADA4: cmp      byte ptr [ebp - 5], 0
0x47ADA8: je       0x47adb9
0x47ADAA: call     0x4031fc
0x47ADAF: pop      dword ptr fs:[0]
0x47ADB6: add      esp, 0xc
0x47ADB9: mov      eax, dword ptr [ebp - 4]
0x47ADBC: pop      ecx
0x47ADBD: pop      ecx
0x47ADBE: pop      ebp
0x47ADBF: ret      

; === TScene.DMT_-1 @ 0x47ADC0 ===
0x47ADC0: push     ebp
0x47ADC1: mov      ebp, esp
0x47ADC3: push     ecx
0x47ADC4: mov      dword ptr [ebp - 4], eax
0x47ADC7: pop      ecx
0x47ADC8: pop      ebp
0x47ADC9: ret      
0x47ADCA: mov      eax, eax
0x47ADCC: push     ebp
0x47ADCD: mov      ebp, esp
0x47ADCF: push     ecx
0x47ADD0: mov      dword ptr [ebp - 4], eax
0x47ADD3: pop      ecx
0x47ADD4: pop      ebp
0x47ADD5: ret      
0x47ADD6: mov      eax, eax
0x47ADD8: push     ebp
0x47ADD9: mov      ebp, esp
0x47ADDB: push     ecx
0x47ADDC: mov      dword ptr [ebp - 4], eax
0x47ADDF: pop      ecx
0x47ADE0: pop      ebp
0x47ADE1: ret      
0x47ADE2: mov      eax, eax
0x47ADE4: push     ebp
0x47ADE5: mov      ebp, esp
0x47ADE7: push     ecx
0x47ADE8: mov      dword ptr [ebp - 4], eax
0x47ADEB: pop      ecx
0x47ADEC: pop      ebp
0x47ADED: ret      
0x47ADEE: mov      eax, eax
0x47ADF0: push     ebp
0x47ADF1: mov      ebp, esp
0x47ADF3: push     ecx
0x47ADF4: mov      dword ptr [ebp - 4], eax
0x47ADF7: pop      ecx
0x47ADF8: pop      ebp
0x47ADF9: ret      
0x47ADFA: mov      eax, eax
0x47ADFC: push     ebp
0x47ADFD: mov      ebp, esp
0x47ADFF: add      esp, -8
0x47AE02: mov      dword ptr [ebp - 8], edx
0x47AE05: mov      dword ptr [ebp - 4], eax
0x47AE08: pop      ecx
0x47AE09: pop      ecx
0x47AE0A: pop      ebp
0x47AE0B: ret      

; === TScene.DMT_-7 @ 0x47AE0C ===
0x47AE0C: push     ebp
0x47AE0D: mov      ebp, esp
0x47AE0F: add      esp, -0xc
0x47AE12: mov      byte ptr [ebp - 9], cl
0x47AE15: mov      dword ptr [ebp - 8], edx
0x47AE18: mov      dword ptr [ebp - 4], eax
0x47AE1B: mov      esp, ebp
0x47AE1D: pop      ebp
0x47AE1E: ret      
0x47AE1F: nop      
0x47AE20: push     ebp
0x47AE21: mov      ebp, esp
0x47AE23: add      esp, -0xc
0x47AE26: mov      dword ptr [ebp - 0xc], ecx
0x47AE29: mov      byte ptr [ebp - 5], dl
0x47AE2C: mov      dword ptr [ebp - 4], eax
0x47AE2F: mov      esp, ebp
0x47AE31: pop      ebp
0x47AE32: ret      4
0x47AE35: lea      eax, [eax]
0x47AE38: push     ebp
0x47AE39: mov      ebp, esp
0x47AE3B: add      esp, -8
0x47AE3E: mov      byte ptr [ebp - 6], cl
0x47AE41: mov      byte ptr [ebp - 5], dl
0x47AE44: mov      dword ptr [ebp - 4], eax
0x47AE47: pop      ecx
0x47AE48: pop      ecx
0x47AE49: pop      ebp
0x47AE4A: ret      8
0x47AE4D: lea      eax, [eax]
0x47AE50: push     ebp
0x47AE51: mov      ebp, esp
0x47AE53: add      esp, -8
0x47AE56: mov      dword ptr [ebp - 8], edx
0x47AE59: mov      dword ptr [ebp - 4], eax
0x47AE5C: pop      ecx
0x47AE5D: pop      ecx
0x47AE5E: pop      ebp
0x47AE5F: ret      
0x47AE60: push     ebp
0x47AE61: mov      ebp, esp
0x47AE63: add      esp, -8
0x47AE66: test     dl, dl
0x47AE68: je       0x47ae72
0x47AE6A: add      esp, -0x10
0x47AE6D: call     0x4031a4
0x47AE72: mov      byte ptr [ebp - 5], dl
0x47AE75: mov      dword ptr [ebp - 4], eax
0x47AE78: xor      ecx, ecx
0x47AE7A: xor      edx, edx
0x47AE7C: mov      eax, dword ptr [ebp - 4]
0x47AE7F: call     0x47ad80
0x47AE84: mov      eax, dword ptr [ebp - 4]
0x47AE87: cmp      byte ptr [ebp - 5], 0
0x47AE8B: je       0x47ae9c
0x47AE8D: call     0x4031fc
0x47AE92: pop      dword ptr fs:[0]
0x47AE99: add      esp, 0xc
0x47AE9C: mov      eax, dword ptr [ebp - 4]
0x47AE9F: pop      ecx
0x47AEA0: pop      ecx
0x47AEA1: pop      ebp
0x47AEA2: ret      
0x47AEA3: nop      
0x47AEA4: push     ebp
0x47AEA5: mov      ebp, esp
0x47AEA7: add      esp, -8
0x47AEAA: call     0x403204
0x47AEAF: mov      byte ptr [ebp - 5], dl
0x47AEB2: mov      dword ptr [ebp - 4], eax
0x47AEB5: mov      dl, byte ptr [ebp - 5]
0x47AEB8: and      dl, 0xfc
0x47AEBB: mov      eax, dword ptr [ebp - 4]
0x47AEBE: call     0x402e9c
0x47AEC3: cmp      byte ptr [ebp - 5], 0
0x47AEC7: jle      0x47aed1
0x47AEC9: mov      eax, dword ptr [ebp - 4]
0x47AECC: call     0x4031f4
0x47AED1: pop      ecx
0x47AED2: pop      ecx
0x47AED3: pop      ebp
0x47AED4: ret      
0x47AED5: lea      eax, [eax]
0x47AED8: push     ebp
0x47AED9: mov      ebp, esp
0x47AEDB: push     ecx
0x47AEDC: mov      dword ptr [ebp - 4], eax
0x47AEDF: pop      ecx
0x47AEE0: pop      ebp
0x47AEE1: ret      
0x47AEE2: mov      eax, eax
0x47AEE4: push     ebp
0x47AEE5: mov      ebp, esp
0x47AEE7: push     ecx
0x47AEE8: mov      dword ptr [ebp - 4], eax
0x47AEEB: pop      ecx
0x47AEEC: pop      ebp
0x47AEED: ret      
0x47AEEE: mov      eax, eax
0x47AEF0: push     ebp
0x47AEF1: mov      ebp, esp
0x47AEF3: add      esp, -8
0x47AEF6: mov      dword ptr [ebp - 8], edx
0x47AEF9: mov      dword ptr [ebp - 4], eax
0x47AEFC: pop      ecx
0x47AEFD: pop      ecx
0x47AEFE: pop      ebp
0x47AEFF: ret      
0x47AF00: push     ebp
0x47AF01: mov      ebp, esp
0x47AF03: add      esp, -0x5c
0x47AF06: test     dl, dl
0x47AF08: je       0x47af12
0x47AF0A: add      esp, -0x10
0x47AF0D: call     0x4031a4
0x47AF12: mov      byte ptr [ebp - 5], dl
0x47AF15: mov      dword ptr [ebp - 4], eax
0x47AF18: mov      cl, 1
0x47AF1A: xor      edx, edx
0x47AF1C: mov      eax, dword ptr [ebp - 4]
0x47AF1F: call     0x47ad80
0x47AF24: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47AF29: mov      eax, dword ptr [eax]
0x47AF2B: mov      ecx, dword ptr [eax + 4]
0x47AF2E: mov      dl, 1
0x47AF30: mov      eax, dword ptr [0x41f814]
0x47AF35: call     0x4219b8
0x47AF3A: mov      edx, dword ptr [ebp - 4]
0x47AF3D: mov      dword ptr [edx + 8], eax
0x47AF40: mov      eax, dword ptr [ebp - 4]
0x47AF43: mov      eax, dword ptr [eax + 8]
0x47AF46: mov      dword ptr [ebp - 0x14], eax
0x47AF49: mov      edx, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47AF4F: mov      edx, dword ptr [edx]
0x47AF51: mov      eax, dword ptr [ebp - 0x14]
0x47AF54: mov      ecx, dword ptr [eax]
0x47AF56: call     dword ptr [ecx + 0x64]
0x47AF59: xor      edx, edx
0x47AF5B: mov      eax, dword ptr [ebp - 0x14]
0x47AF5E: call     0x427b5c
0x47AF63: mov      eax, dword ptr [ebp - 0x14]
0x47AF66: mov      eax, dword ptr [eax + 0x58]
0x47AF69: mov      edx, 0xffffff
0x47AF6E: call     0x4173fc
0x47AF73: mov      eax, dword ptr [ebp - 0x14]
0x47AF76: mov      eax, dword ptr [eax + 0x58]
0x47AF79: mov      edx, 0xa
0x47AF7E: call     0x417690
0x47AF83: mov      edx, 0xa
0x47AF88: mov      eax, dword ptr [ebp - 0x14]
0x47AF8B: call     0x421ae0
0x47AF90: xor      edx, edx
0x47AF92: mov      eax, dword ptr [ebp - 0x14]
0x47AF95: call     0x421a94
0x47AF9A: mov      eax, dword ptr [ebp - 0x14]
0x47AF9D: mov      edx, dword ptr [ebp - 4]
0x47AFA0: mov      dword ptr [eax + 0x1bc], edx
0x47AFA6: mov      dword ptr [eax + 0x1b8], 0x47c174
0x47AFB0: xor      edx, edx
0x47AFB2: mov      eax, dword ptr [ebp - 0x14]
0x47AFB5: call     0x4278c0
0x47AFBA: mov      eax, dword ptr [ebp - 0x14]
0x47AFBD: mov      dword ptr [eax + 0xc], 0xa
0x47AFC4: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47AFC9: mov      eax, dword ptr [eax]
0x47AFCB: mov      ecx, dword ptr [eax + 4]
0x47AFCE: mov      dl, 1
0x47AFD0: mov      eax, dword ptr [0x41f814]
0x47AFD5: call     0x4219b8
0x47AFDA: mov      edx, dword ptr [ebp - 4]
0x47AFDD: mov      dword ptr [edx + 0xc], eax
0x47AFE0: mov      eax, dword ptr [ebp - 4]
0x47AFE3: mov      eax, dword ptr [eax + 0xc]
0x47AFE6: mov      dword ptr [ebp - 0x18], eax
0x47AFE9: mov      edx, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47AFEF: mov      edx, dword ptr [edx]
0x47AFF1: mov      eax, dword ptr [ebp - 0x18]
0x47AFF4: mov      ecx, dword ptr [eax]
0x47AFF6: call     dword ptr [ecx + 0x64]
0x47AFF9: xor      edx, edx
0x47AFFB: mov      eax, dword ptr [ebp - 0x18]
0x47AFFE: call     0x427b5c
0x47B003: mov      eax, dword ptr [ebp - 0x18]
0x47B006: mov      eax, dword ptr [eax + 0x58]
0x47B009: mov      edx, 0xa
0x47B00E: call     0x417690
0x47B013: mov      edx, 0xa
0x47B018: mov      eax, dword ptr [ebp - 0x18]
0x47B01B: call     0x421ae0
0x47B020: mov      eax, dword ptr [ebp - 0x18]
0x47B023: mov      eax, dword ptr [eax + 0x58]
0x47B026: mov      edx, 0xffffff
0x47B02B: call     0x4173fc
0x47B030: xor      edx, edx
0x47B032: mov      eax, dword ptr [ebp - 0x18]
0x47B035: call     0x421a94
0x47B03A: mov      dl, 0x2a
0x47B03C: mov      eax, dword ptr [ebp - 0x18]
0x47B03F: call     0x421b94
0x47B044: mov      eax, dword ptr [ebp - 0x18]
0x47B047: mov      edx, dword ptr [ebp - 4]
0x47B04A: mov      dword ptr [eax + 0x1bc], edx
0x47B050: mov      dword ptr [eax + 0x1b8], 0x47c218
0x47B05A: xor      edx, edx
0x47B05C: mov      eax, dword ptr [ebp - 0x18]
0x47B05F: call     0x4278c0
0x47B064: mov      eax, dword ptr [ebp - 0x18]
0x47B067: mov      dword ptr [eax + 0xc], 0xa
0x47B06E: mov      dword ptr [ebp - 0xc], 0x4f
0x47B075: mov      dword ptr [ebp - 0x10], 0x40
0x47B07C: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47B081: mov      eax, dword ptr [eax]
0x47B083: mov      ecx, dword ptr [eax + 4]
0x47B086: mov      dl, 1
0x47B088: mov      eax, dword ptr [0x41f814]
0x47B08D: call     0x4219b8
0x47B092: mov      edx, dword ptr [ebp - 4]
0x47B095: mov      dword ptr [edx + 0x10], eax
0x47B098: mov      eax, dword ptr [ebp - 4]
0x47B09B: mov      eax, dword ptr [eax + 0x10]
0x47B09E: mov      dword ptr [ebp - 0x1c], eax
0x47B0A1: mov      edx, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47B0A7: mov      edx, dword ptr [edx]
0x47B0A9: mov      eax, dword ptr [ebp - 0x1c]
0x47B0AC: mov      ecx, dword ptr [eax]
0x47B0AE: call     dword ptr [ecx + 0x64]
0x47B0B1: mov      edx, 0x10
0x47B0B6: mov      eax, dword ptr [ebp - 0x1c]
0x47B0B9: call     0x4271fc
0x47B0BE: mov      edx, 0x74
0x47B0C3: mov      eax, dword ptr [ebp - 0x1c]
0x47B0C6: call     0x4271d8
0x47B0CB: mov      edx, dword ptr [ebp - 0xc]
0x47B0CE: add      edx, 0xa1
0x47B0D4: mov      eax, dword ptr [ebp - 0x1c]
0x47B0D7: call     0x42718c
0x47B0DC: mov      edx, dword ptr [ebp - 0x10]
0x47B0DF: add      edx, 0x74
0x47B0E2: mov      eax, dword ptr [ebp - 0x1c]
0x47B0E5: call     0x4271b0
0x47B0EA: xor      edx, edx
0x47B0EC: mov      eax, dword ptr [ebp - 0x1c]
0x47B0EF: call     0x421a94
0x47B0F4: xor      edx, edx
0x47B0F6: mov      eax, dword ptr [ebp - 0x1c]
0x47B0F9: call     0x427b5c
0x47B0FE: mov      eax, dword ptr [ebp - 0x1c]
0x47B101: mov      eax, dword ptr [eax + 0x58]
0x47B104: mov      edx, 0xffffff
0x47B109: call     0x4173fc
0x47B10E: mov      edx, 0xa
0x47B113: mov      eax, dword ptr [ebp - 0x1c]
0x47B116: call     0x421ae0
0x47B11B: xor      edx, edx
0x47B11D: mov      eax, dword ptr [ebp - 0x1c]
0x47B120: call     0x4278c0
0x47B125: mov      eax, dword ptr [ebp - 0x1c]
0x47B128: mov      edx, dword ptr [ebp - 4]
0x47B12B: mov      dword ptr [eax + 0x1bc], edx
0x47B131: mov      dword ptr [eax + 0x1b8], 0x47c758
0x47B13B: mov      eax, dword ptr [ebp - 0x1c]
0x47B13E: mov      edx, dword ptr [ebp - 4]
0x47B141: mov      dword ptr [eax + 0x19c], edx
0x47B147: mov      dword ptr [eax + 0x198], 0x47cb78
0x47B151: mov      eax, dword ptr [ebp - 0x1c]
0x47B154: mov      dword ptr [eax + 0xc], 0xb
0x47B15B: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47B160: mov      eax, dword ptr [eax]
0x47B162: mov      ecx, dword ptr [eax + 4]
0x47B165: mov      dl, 1
0x47B167: mov      eax, dword ptr [0x41f814]
0x47B16C: call     0x4219b8
0x47B171: mov      edx, dword ptr [ebp - 4]
0x47B174: mov      dword ptr [edx + 0x14], eax
0x47B177: mov      eax, dword ptr [ebp - 4]
0x47B17A: mov      eax, dword ptr [eax + 0x14]
0x47B17D: mov      dword ptr [ebp - 0x20], eax
0x47B180: mov      edx, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47B186: mov      edx, dword ptr [edx]
0x47B188: mov      eax, dword ptr [ebp - 0x20]
0x47B18B: mov      ecx, dword ptr [eax]
0x47B18D: call     dword ptr [ecx + 0x64]
0x47B190: mov      edx, 0x10
0x47B195: mov      eax, dword ptr [ebp - 0x20]
0x47B198: call     0x4271fc
0x47B19D: mov      edx, 0x74
0x47B1A2: mov      eax, dword ptr [ebp - 0x20]
0x47B1A5: call     0x4271d8
0x47B1AA: mov      edx, dword ptr [ebp - 0xc]
0x47B1AD: add      edx, 0xa1
0x47B1B3: mov      eax, dword ptr [ebp - 0x20]
0x47B1B6: call     0x42718c
0x47B1BB: mov      edx, dword ptr [ebp - 0x10]
0x47B1BE: add      edx, 0x89
0x47B1C4: mov      eax, dword ptr [ebp - 0x20]
0x47B1C7: call     0x4271b0
0x47B1CC: xor      edx, edx
0x47B1CE: mov      eax, dword ptr [ebp - 0x20]
0x47B1D1: call     0x421a94
0x47B1D6: xor      edx, edx
0x47B1D8: mov      eax, dword ptr [ebp - 0x20]
0x47B1DB: call     0x427b5c
0x47B1E0: mov      eax, dword ptr [ebp - 0x20]
0x47B1E3: mov      eax, dword ptr [eax + 0x58]
0x47B1E6: mov      edx, 0xffffff
0x47B1EB: call     0x4173fc
0x47B1F0: mov      edx, 0xa
0x47B1F5: mov      eax, dword ptr [ebp - 0x20]
0x47B1F8: call     0x421ae0
0x47B1FD: mov      dl, 0x2a
0x47B1FF: mov      eax, dword ptr [ebp - 0x20]
0x47B202: call     0x421b94
0x47B207: xor      edx, edx
0x47B209: mov      eax, dword ptr [ebp - 0x20]
0x47B20C: call     0x4278c0
0x47B211: mov      eax, dword ptr [ebp - 0x20]
0x47B214: mov      edx, dword ptr [ebp - 4]
0x47B217: mov      dword ptr [eax + 0x1bc], edx
0x47B21D: mov      dword ptr [eax + 0x1b8], 0x47c758
0x47B227: mov      eax, dword ptr [ebp - 0x20]
0x47B22A: mov      edx, dword ptr [ebp - 4]
0x47B22D: mov      dword ptr [eax + 0x19c], edx
0x47B233: mov      dword ptr [eax + 0x198], 0x47cb78
0x47B23D: mov      eax, dword ptr [ebp - 0x20]
0x47B240: mov      dword ptr [eax + 0xc], 0xb
0x47B247: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47B24C: mov      eax, dword ptr [eax]
0x47B24E: mov      ecx, dword ptr [eax + 4]
0x47B251: mov      dl, 1
0x47B253: mov      eax, dword ptr [0x41f814]
0x47B258: call     0x4219b8
0x47B25D: mov      edx, dword ptr [ebp - 4]
0x47B260: mov      dword ptr [edx + 0x18], eax
0x47B263: mov      eax, dword ptr [ebp - 4]
0x47B266: mov      eax, dword ptr [eax + 0x18]
0x47B269: mov      dword ptr [ebp - 0x24], eax
0x47B26C: mov      edx, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47B272: mov      edx, dword ptr [edx]
0x47B274: mov      eax, dword ptr [ebp - 0x24]
0x47B277: mov      ecx, dword ptr [eax]
0x47B279: call     dword ptr [ecx + 0x64]
0x47B27C: mov      edx, 0x10
0x47B281: mov      eax, dword ptr [ebp - 0x24]
0x47B284: call     0x4271fc
0x47B289: mov      edx, 0x74
0x47B28E: mov      eax, dword ptr [ebp - 0x24]
0x47B291: call     0x4271d8
0x47B296: mov      edx, dword ptr [ebp - 0xc]
0x47B299: add      edx, 0xa1
0x47B29F: mov      eax, dword ptr [ebp - 0x24]
0x47B2A2: call     0x42718c
0x47B2A7: mov      edx, dword ptr [ebp - 0x10]
0x47B2AA: add      edx, 0x9e
0x47B2B0: mov      eax, dword ptr [ebp - 0x24]
0x47B2B3: call     0x4271b0
0x47B2B8: xor      edx, edx
0x47B2BA: mov      eax, dword ptr [ebp - 0x24]
0x47B2BD: call     0x421a94
0x47B2C2: xor      edx, edx
0x47B2C4: mov      eax, dword ptr [ebp - 0x24]
0x47B2C7: call     0x427b5c
0x47B2CC: mov      eax, dword ptr [ebp - 0x24]
0x47B2CF: mov      eax, dword ptr [eax + 0x58]
0x47B2D2: mov      edx, 0xffffff
0x47B2D7: call     0x4173fc
0x47B2DC: mov      edx, 0xa
0x47B2E1: mov      eax, dword ptr [ebp - 0x24]
0x47B2E4: call     0x421ae0
0x47B2E9: mov      dl, 0x2a
0x47B2EB: mov      eax, dword ptr [ebp - 0x24]
0x47B2EE: call     0x421b94
0x47B2F3: xor      edx, edx
0x47B2F5: mov      eax, dword ptr [ebp - 0x24]
0x47B2F8: call     0x4278c0
0x47B2FD: mov      eax, dword ptr [ebp - 0x24]
0x47B300: mov      edx, dword ptr [ebp - 4]
0x47B303: mov      dword ptr [eax + 0x1bc], edx
0x47B309: mov      dword ptr [eax + 0x1b8], 0x47c758
0x47B313: mov      eax, dword ptr [ebp - 0x24]
0x47B316: mov      edx, dword ptr [ebp - 4]
0x47B319: mov      dword ptr [eax + 0x19c], edx
0x47B31F: mov      dword ptr [eax + 0x198], 0x47cb78
0x47B329: mov      eax, dword ptr [ebp - 0x24]
0x47B32C: mov      dword ptr [eax + 0xc], 0xb
0x47B333: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47B338: mov      eax, dword ptr [eax]
0x47B33A: mov      ecx, dword ptr [eax + 4]
0x47B33D: mov      dl, 1
0x47B33F: mov      eax, dword ptr [0x41f814]
0x47B344: call     0x4219b8
0x47B349: mov      edx, dword ptr [ebp - 4]
0x47B34C: mov      dword ptr [edx + 0x1c], eax
0x47B34F: mov      eax, dword ptr [ebp - 4]
0x47B352: mov      eax, dword ptr [eax + 0x1c]
0x47B355: mov      dword ptr [ebp - 0x28], eax
0x47B358: mov      edx, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47B35E: mov      edx, dword ptr [edx]
0x47B360: mov      eax, dword ptr [ebp - 0x28]
0x47B363: mov      ecx, dword ptr [eax]
0x47B365: call     dword ptr [ecx + 0x64]
0x47B368: mov      edx, 0x10
0x47B36D: mov      eax, dword ptr [ebp - 0x28]
0x47B370: call     0x4271fc
0x47B375: mov      edx, 0x74
0x47B37A: mov      eax, dword ptr [ebp - 0x28]
0x47B37D: call     0x4271d8
0x47B382: mov      edx, dword ptr [ebp - 0xc]
0x47B385: add      edx, 0xa1
0x47B38B: mov      eax, dword ptr [ebp - 0x28]
0x47B38E: call     0x42718c
0x47B393: mov      edx, dword ptr [ebp - 0x10]
0x47B396: add      edx, 0xbb
0x47B39C: mov      eax, dword ptr [ebp - 0x28]
0x47B39F: call     0x4271b0
0x47B3A4: xor      edx, edx
0x47B3A6: mov      eax, dword ptr [ebp - 0x28]
0x47B3A9: call     0x421a94
0x47B3AE: xor      edx, edx
0x47B3B0: mov      eax, dword ptr [ebp - 0x28]
0x47B3B3: call     0x427b5c
0x47B3B8: mov      eax, dword ptr [ebp - 0x28]
0x47B3BB: mov      eax, dword ptr [eax + 0x58]
0x47B3BE: mov      edx, 0xffffff
0x47B3C3: call     0x4173fc
0x47B3C8: mov      edx, 0x14
0x47B3CD: mov      eax, dword ptr [ebp - 0x28]
0x47B3D0: call     0x421ae0
0x47B3D5: xor      edx, edx
0x47B3D7: mov      eax, dword ptr [ebp - 0x28]
0x47B3DA: call     0x4278c0
0x47B3DF: mov      eax, dword ptr [ebp - 0x28]
0x47B3E2: mov      edx, dword ptr [ebp - 4]
0x47B3E5: mov      dword ptr [eax + 0x1bc], edx
0x47B3EB: mov      dword ptr [eax + 0x1b8], 0x47c758
0x47B3F5: mov      eax, dword ptr [ebp - 0x28]
0x47B3F8: mov      edx, dword ptr [ebp - 4]
0x47B3FB: mov      dword ptr [eax + 0x19c], edx
0x47B401: mov      dword ptr [eax + 0x198], 0x47cb78
0x47B40B: mov      eax, dword ptr [ebp - 0x28]
0x47B40E: mov      dword ptr [eax + 0xc], 0xb
0x47B415: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47B41A: mov      eax, dword ptr [eax]
0x47B41C: mov      ecx, dword ptr [eax + 4]
0x47B41F: mov      dl, 1
0x47B421: mov      eax, dword ptr [0x41f814]
0x47B426: call     0x4219b8
0x47B42B: mov      edx, dword ptr [ebp - 4]
0x47B42E: mov      dword ptr [edx + 0x20], eax
0x47B431: mov      eax, dword ptr [ebp - 4]
0x47B434: mov      eax, dword ptr [eax + 0x20]
0x47B437: mov      dword ptr [ebp - 0x2c], eax
0x47B43A: mov      edx, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47B440: mov      edx, dword ptr [edx]
0x47B442: mov      eax, dword ptr [ebp - 0x2c]
0x47B445: mov      ecx, dword ptr [eax]
0x47B447: call     dword ptr [ecx + 0x64]
0x47B44A: mov      edx, 0x10
0x47B44F: mov      eax, dword ptr [ebp - 0x2c]
0x47B452: call     0x4271fc
0x47B457: mov      edx, 0x74
0x47B45C: mov      eax, dword ptr [ebp - 0x2c]
0x47B45F: call     0x4271d8
0x47B464: mov      edx, dword ptr [ebp - 0xc]
0x47B467: add      edx, 0xa1
0x47B46D: mov      eax, dword ptr [ebp - 0x2c]
0x47B470: call     0x42718c
0x47B475: mov      edx, dword ptr [ebp - 0x10]
0x47B478: add      edx, 0xcf
0x47B47E: mov      eax, dword ptr [ebp - 0x2c]
0x47B481: call     0x4271b0
0x47B486: xor      edx, edx
0x47B488: mov      eax, dword ptr [ebp - 0x2c]
0x47B48B: call     0x421a94
0x47B490: xor      edx, edx
0x47B492: mov      eax, dword ptr [ebp - 0x2c]
0x47B495: call     0x427b5c
0x47B49A: mov      eax, dword ptr [ebp - 0x2c]
0x47B49D: mov      eax, dword ptr [eax + 0x58]
0x47B4A0: mov      edx, 0xffffff
0x47B4A5: call     0x4173fc
0x47B4AA: mov      edx, 0xe
0x47B4AF: mov      eax, dword ptr [ebp - 0x2c]
0x47B4B2: call     0x421ae0
0x47B4B7: xor      edx, edx
0x47B4B9: mov      eax, dword ptr [ebp - 0x2c]
0x47B4BC: call     0x4278c0
0x47B4C1: mov      eax, dword ptr [ebp - 0x2c]
0x47B4C4: mov      edx, dword ptr [ebp - 4]
0x47B4C7: mov      dword ptr [eax + 0x1bc], edx
0x47B4CD: mov      dword ptr [eax + 0x1b8], 0x47c758
0x47B4D7: mov      eax, dword ptr [ebp - 0x2c]
0x47B4DA: mov      edx, dword ptr [ebp - 4]
0x47B4DD: mov      dword ptr [eax + 0x19c], edx
0x47B4E3: mov      dword ptr [eax + 0x198], 0x47cb78
0x47B4ED: mov      eax, dword ptr [ebp - 0x2c]
0x47B4F0: mov      dword ptr [eax + 0xc], 0xb
0x47B4F7: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47B4FC: mov      eax, dword ptr [eax]
0x47B4FE: mov      ecx, dword ptr [eax + 4]
0x47B501: mov      dl, 1
0x47B503: mov      eax, dword ptr [0x41f814]
0x47B508: call     0x4219b8
0x47B50D: mov      edx, dword ptr [ebp - 4]
0x47B510: mov      dword ptr [edx + 0x24], eax
0x47B513: mov      eax, dword ptr [ebp - 4]
0x47B516: mov      eax, dword ptr [eax + 0x24]
0x47B519: mov      dword ptr [ebp - 0x30], eax
0x47B51C: mov      edx, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47B522: mov      edx, dword ptr [edx]
0x47B524: mov      eax, dword ptr [ebp - 0x30]
0x47B527: mov      ecx, dword ptr [eax]
0x47B529: call     dword ptr [ecx + 0x64]
0x47B52C: mov      edx, 0x10
0x47B531: mov      eax, dword ptr [ebp - 0x30]
0x47B534: call     0x4271fc
0x47B539: mov      edx, 0x74
0x47B53E: mov      eax, dword ptr [ebp - 0x30]
0x47B541: call     0x4271d8
0x47B546: mov      edx, dword ptr [ebp - 0xc]
0x47B549: add      edx, 0xa1
0x47B54F: mov      eax, dword ptr [ebp - 0x30]
0x47B552: call     0x42718c
0x47B557: mov      edx, dword ptr [ebp - 0x10]
0x47B55A: add      edx, 0xe3
0x47B560: mov      eax, dword ptr [ebp - 0x30]
0x47B563: call     0x4271b0
0x47B568: xor      edx, edx
0x47B56A: mov      eax, dword ptr [ebp - 0x30]
0x47B56D: call     0x421a94
0x47B572: xor      edx, edx
0x47B574: mov      eax, dword ptr [ebp - 0x30]
0x47B577: call     0x427b5c
0x47B57C: mov      eax, dword ptr [ebp - 0x30]
0x47B57F: mov      eax, dword ptr [eax + 0x58]
0x47B582: mov      edx, 0xffffff
0x47B587: call     0x4173fc
0x47B58C: mov      edx, 0xa
0x47B591: mov      eax, dword ptr [ebp - 0x30]
0x47B594: call     0x421ae0
0x47B599: xor      edx, edx
0x47B59B: mov      eax, dword ptr [ebp - 0x30]
0x47B59E: call     0x4278c0
0x47B5A3: mov      eax, dword ptr [ebp - 0x30]
0x47B5A6: mov      edx, dword ptr [ebp - 4]
0x47B5A9: mov      dword ptr [eax + 0x1bc], edx
0x47B5AF: mov      dword ptr [eax + 0x1b8], 0x47c758
0x47B5B9: mov      eax, dword ptr [ebp - 0x30]
0x47B5BC: mov      edx, dword ptr [ebp - 4]
0x47B5BF: mov      dword ptr [eax + 0x19c], edx
0x47B5C5: mov      dword ptr [eax + 0x198], 0x47cb78
0x47B5CF: mov      eax, dword ptr [ebp - 0x30]
0x47B5D2: mov      dword ptr [eax + 0xc], 0xb
0x47B5D9: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47B5DE: mov      eax, dword ptr [eax]
0x47B5E0: mov      ecx, dword ptr [eax + 4]
0x47B5E3: mov      dl, 1
0x47B5E5: mov      eax, dword ptr [0x41f814]
0x47B5EA: call     0x4219b8
0x47B5EF: mov      edx, dword ptr [ebp - 4]
0x47B5F2: mov      dword ptr [edx + 0x28], eax
0x47B5F5: mov      eax, dword ptr [ebp - 4]
0x47B5F8: mov      eax, dword ptr [eax + 0x28]
0x47B5FB: mov      dword ptr [ebp - 0x34], eax
0x47B5FE: mov      edx, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47B604: mov      edx, dword ptr [edx]
0x47B606: mov      eax, dword ptr [ebp - 0x34]
0x47B609: mov      ecx, dword ptr [eax]
0x47B60B: call     dword ptr [ecx + 0x64]
0x47B60E: mov      edx, 0x10
0x47B613: mov      eax, dword ptr [ebp - 0x34]
0x47B616: call     0x4271fc
0x47B61B: mov      edx, 0xa3
0x47B620: mov      eax, dword ptr [ebp - 0x34]
0x47B623: call     0x4271d8
0x47B628: mov      edx, dword ptr [ebp - 0xc]
0x47B62B: add      edx, 0xa1
0x47B631: mov      eax, dword ptr [ebp - 0x34]
0x47B634: call     0x42718c
0x47B639: mov      edx, dword ptr [ebp - 0x10]
0x47B63C: add      edx, 0x100
0x47B642: mov      eax, dword ptr [ebp - 0x34]
0x47B645: call     0x4271b0
0x47B64A: xor      edx, edx
0x47B64C: mov      eax, dword ptr [ebp - 0x34]
0x47B64F: call     0x421a94
0x47B654: xor      edx, edx
0x47B656: mov      eax, dword ptr [ebp - 0x34]
0x47B659: call     0x427b5c
0x47B65E: mov      eax, dword ptr [ebp - 0x34]
0x47B661: mov      eax, dword ptr [eax + 0x58]
0x47B664: mov      edx, 0xffffff
0x47B669: call     0x4173fc
0x47B66E: mov      edx, 0x14
0x47B673: mov      eax, dword ptr [ebp - 0x34]
0x47B676: call     0x421ae0
0x47B67B: xor      edx, edx
0x47B67D: mov      eax, dword ptr [ebp - 0x34]
0x47B680: call     0x4278c0
0x47B685: mov      eax, dword ptr [ebp - 0x34]
0x47B688: mov      edx, dword ptr [ebp - 4]
0x47B68B: mov      dword ptr [eax + 0x1bc], edx
0x47B691: mov      dword ptr [eax + 0x1b8], 0x47c758
0x47B69B: mov      eax, dword ptr [ebp - 0x34]
0x47B69E: mov      edx, dword ptr [ebp - 4]
0x47B6A1: mov      dword ptr [eax + 0x19c], edx
0x47B6A7: mov      dword ptr [eax + 0x198], 0x47cb78
0x47B6B1: mov      eax, dword ptr [ebp - 0x34]
0x47B6B4: mov      dword ptr [eax + 0xc], 0xb
0x47B6BB: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47B6C0: mov      eax, dword ptr [eax]
0x47B6C2: mov      ecx, dword ptr [eax + 4]
0x47B6C5: mov      dl, 1
0x47B6C7: mov      eax, dword ptr [0x41f814]
0x47B6CC: call     0x4219b8
0x47B6D1: mov      edx, dword ptr [ebp - 4]
0x47B6D4: mov      dword ptr [edx + 0x2c], eax
0x47B6D7: mov      eax, dword ptr [ebp - 4]
0x47B6DA: mov      eax, dword ptr [eax + 0x2c]
0x47B6DD: mov      dword ptr [ebp - 0x38], eax
0x47B6E0: mov      edx, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47B6E6: mov      edx, dword ptr [edx]
0x47B6E8: mov      eax, dword ptr [ebp - 0x38]
0x47B6EB: mov      ecx, dword ptr [eax]
0x47B6ED: call     dword ptr [ecx + 0x64]
0x47B6F0: mov      edx, 0x10
0x47B6F5: mov      eax, dword ptr [ebp - 0x38]
0x47B6F8: call     0x4271fc
0x47B6FD: mov      edx, 0xa3
0x47B702: mov      eax, dword ptr [ebp - 0x38]
0x47B705: call     0x4271d8
0x47B70A: mov      edx, dword ptr [ebp - 0xc]
0x47B70D: add      edx, 0xa1
0x47B713: mov      eax, dword ptr [ebp - 0x38]
0x47B716: call     0x42718c
0x47B71B: mov      edx, dword ptr [ebp - 0x10]
0x47B71E: add      edx, 0x114
0x47B724: mov      eax, dword ptr [ebp - 0x38]
0x47B727: call     0x4271b0
0x47B72C: xor      edx, edx
0x47B72E: mov      eax, dword ptr [ebp - 0x38]
0x47B731: call     0x421a94
0x47B736: xor      edx, edx
0x47B738: mov      eax, dword ptr [ebp - 0x38]
0x47B73B: call     0x427b5c
0x47B740: mov      eax, dword ptr [ebp - 0x38]
0x47B743: mov      eax, dword ptr [eax + 0x58]
0x47B746: mov      edx, 0xffffff
0x47B74B: call     0x4173fc
0x47B750: mov      edx, 0xc
0x47B755: mov      eax, dword ptr [ebp - 0x38]
0x47B758: call     0x421ae0
0x47B75D: xor      edx, edx
0x47B75F: mov      eax, dword ptr [ebp - 0x38]
0x47B762: call     0x4278c0
0x47B767: mov      eax, dword ptr [ebp - 0x38]
0x47B76A: mov      edx, dword ptr [ebp - 4]
0x47B76D: mov      dword ptr [eax + 0x1bc], edx
0x47B773: mov      dword ptr [eax + 0x1b8], 0x47c758
0x47B77D: mov      eax, dword ptr [ebp - 0x38]
0x47B780: mov      edx, dword ptr [ebp - 4]
0x47B783: mov      dword ptr [eax + 0x19c], edx
0x47B789: mov      dword ptr [eax + 0x198], 0x47cb78
0x47B793: mov      eax, dword ptr [ebp - 0x38]
0x47B796: mov      dword ptr [eax + 0xc], 0xb
0x47B79D: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47B7A2: mov      eax, dword ptr [eax]
0x47B7A4: mov      ecx, dword ptr [eax + 4]
0x47B7A7: mov      dl, 1
0x47B7A9: mov      eax, dword ptr [0x41f814]
0x47B7AE: call     0x4219b8
0x47B7B3: mov      edx, dword ptr [ebp - 4]
0x47B7B6: mov      dword ptr [edx + 0x30], eax
0x47B7B9: mov      eax, dword ptr [ebp - 4]
0x47B7BC: mov      eax, dword ptr [eax + 0x30]
0x47B7BF: mov      dword ptr [ebp - 0x3c], eax
0x47B7C2: mov      edx, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47B7C8: mov      edx, dword ptr [edx]
0x47B7CA: mov      eax, dword ptr [ebp - 0x3c]
0x47B7CD: mov      ecx, dword ptr [eax]
0x47B7CF: call     dword ptr [ecx + 0x64]
0x47B7D2: mov      edx, 0x10
0x47B7D7: mov      eax, dword ptr [ebp - 0x3c]
0x47B7DA: call     0x4271fc
0x47B7DF: mov      edx, 0xa3
0x47B7E4: mov      eax, dword ptr [ebp - 0x3c]
0x47B7E7: call     0x4271d8
0x47B7EC: mov      edx, dword ptr [ebp - 0xc]
0x47B7EF: add      edx, 0xa1
0x47B7F5: mov      eax, dword ptr [ebp - 0x3c]
0x47B7F8: call     0x42718c
0x47B7FD: mov      edx, dword ptr [ebp - 0x10]
0x47B800: add      edx, 0x129
0x47B806: mov      eax, dword ptr [ebp - 0x3c]
0x47B809: call     0x4271b0
0x47B80E: xor      edx, edx
0x47B810: mov      eax, dword ptr [ebp - 0x3c]
0x47B813: call     0x421a94
0x47B818: xor      edx, edx
0x47B81A: mov      eax, dword ptr [ebp - 0x3c]
0x47B81D: call     0x427b5c
0x47B822: mov      eax, dword ptr [ebp - 0x3c]
0x47B825: mov      eax, dword ptr [eax + 0x58]
0x47B828: mov      edx, 0xffffff
0x47B82D: call     0x4173fc
0x47B832: mov      edx, 0x14
0x47B837: mov      eax, dword ptr [ebp - 0x3c]
0x47B83A: call     0x421ae0
0x47B83F: xor      edx, edx
0x47B841: mov      eax, dword ptr [ebp - 0x3c]
0x47B844: call     0x4278c0
0x47B849: mov      eax, dword ptr [ebp - 0x3c]
0x47B84C: mov      edx, dword ptr [ebp - 4]
0x47B84F: mov      dword ptr [eax + 0x1bc], edx
0x47B855: mov      dword ptr [eax + 0x1b8], 0x47c758
0x47B85F: mov      eax, dword ptr [ebp - 0x3c]
0x47B862: mov      edx, dword ptr [ebp - 4]
0x47B865: mov      dword ptr [eax + 0x19c], edx
0x47B86B: mov      dword ptr [eax + 0x198], 0x47cb78
0x47B875: mov      eax, dword ptr [ebp - 0x3c]
0x47B878: mov      dword ptr [eax + 0xc], 0xb
0x47B87F: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47B884: mov      eax, dword ptr [eax]
0x47B886: mov      ecx, dword ptr [eax + 4]
0x47B889: mov      dl, 1
0x47B88B: mov      eax, dword ptr [0x41f814]
0x47B890: call     0x4219b8
0x47B895: mov      edx, dword ptr [ebp - 4]
0x47B898: mov      dword ptr [edx + 0x34], eax
0x47B89B: mov      eax, dword ptr [ebp - 4]
0x47B89E: mov      eax, dword ptr [eax + 0x34]
0x47B8A1: mov      dword ptr [ebp - 0x40], eax
0x47B8A4: mov      edx, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47B8AA: mov      edx, dword ptr [edx]
0x47B8AC: mov      eax, dword ptr [ebp - 0x40]
0x47B8AF: mov      ecx, dword ptr [eax]
0x47B8B1: call     dword ptr [ecx + 0x64]
0x47B8B4: mov      edx, 0x10
0x47B8B9: mov      eax, dword ptr [ebp - 0x40]
0x47B8BC: call     0x4271fc
0x47B8C1: mov      edx, 0xa3
0x47B8C6: mov      eax, dword ptr [ebp - 0x40]
0x47B8C9: call     0x4271d8
0x47B8CE: mov      edx, dword ptr [ebp - 0xc]
0x47B8D1: add      edx, 0xa1
0x47B8D7: mov      eax, dword ptr [ebp - 0x40]
0x47B8DA: call     0x42718c
0x47B8DF: mov      edx, dword ptr [ebp - 0x10]
0x47B8E2: add      edx, 0x13d
0x47B8E8: mov      eax, dword ptr [ebp - 0x40]
0x47B8EB: call     0x4271b0
0x47B8F0: xor      edx, edx
0x47B8F2: mov      eax, dword ptr [ebp - 0x40]
0x47B8F5: call     0x421a94
0x47B8FA: xor      edx, edx
0x47B8FC: mov      eax, dword ptr [ebp - 0x40]
0x47B8FF: call     0x427b5c
0x47B904: mov      eax, dword ptr [ebp - 0x40]
0x47B907: mov      eax, dword ptr [eax + 0x58]
0x47B90A: mov      edx, 0xffffff
0x47B90F: call     0x4173fc
0x47B914: mov      edx, 0xc
0x47B919: mov      eax, dword ptr [ebp - 0x40]
0x47B91C: call     0x421ae0
0x47B921: xor      edx, edx
0x47B923: mov      eax, dword ptr [ebp - 0x40]
0x47B926: call     0x4278c0
0x47B92B: mov      eax, dword ptr [ebp - 0x40]
0x47B92E: mov      edx, dword ptr [ebp - 4]
0x47B931: mov      dword ptr [eax + 0x1bc], edx
0x47B937: mov      dword ptr [eax + 0x1b8], 0x47c758
0x47B941: mov      eax, dword ptr [ebp - 0x40]
0x47B944: mov      edx, dword ptr [ebp - 4]
0x47B947: mov      dword ptr [eax + 0x19c], edx
0x47B94D: mov      dword ptr [eax + 0x198], 0x47cb78
0x47B957: mov      eax, dword ptr [ebp - 0x40]
0x47B95A: mov      dword ptr [eax + 0xc], 0xb
0x47B961: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47B966: mov      eax, dword ptr [eax]
0x47B968: mov      ecx, dword ptr [eax + 4]
0x47B96B: mov      dl, 1
0x47B96D: mov      eax, dword ptr [0x41f814]
0x47B972: call     0x4219b8
0x47B977: mov      edx, dword ptr [ebp - 4]
0x47B97A: mov      dword ptr [edx + 0x38], eax
0x47B97D: mov      eax, dword ptr [ebp - 4]
0x47B980: mov      eax, dword ptr [eax + 0x38]
0x47B983: mov      dword ptr [ebp - 0x44], eax
0x47B986: mov      edx, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47B98C: mov      edx, dword ptr [edx]
0x47B98E: mov      eax, dword ptr [ebp - 0x44]
0x47B991: mov      ecx, dword ptr [eax]
0x47B993: call     dword ptr [ecx + 0x64]
0x47B996: mov      edx, 0x10
0x47B99B: mov      eax, dword ptr [ebp - 0x44]
0x47B99E: call     0x4271fc
0x47B9A3: mov      edx, 0x74
0x47B9A8: mov      eax, dword ptr [ebp - 0x44]
0x47B9AB: call     0x4271d8
0x47B9B0: mov      edx, dword ptr [ebp - 0xc]
0x47B9B3: add      edx, 0xa1
0x47B9B9: mov      eax, dword ptr [ebp - 0x44]
0x47B9BC: call     0x42718c
0x47B9C1: mov      edx, dword ptr [ebp - 0x10]
0x47B9C4: add      edx, 0x15b
0x47B9CA: mov      eax, dword ptr [ebp - 0x44]
0x47B9CD: call     0x4271b0
0x47B9D2: xor      edx, edx
0x47B9D4: mov      eax, dword ptr [ebp - 0x44]
0x47B9D7: call     0x421a94
0x47B9DC: xor      edx, edx
0x47B9DE: mov      eax, dword ptr [ebp - 0x44]
0x47B9E1: call     0x427b5c
0x47B9E6: mov      eax, dword ptr [ebp - 0x44]
0x47B9E9: mov      eax, dword ptr [eax + 0x58]
0x47B9EC: mov      edx, 0xffffff
0x47B9F1: call     0x4173fc
0x47B9F6: mov      edx, 0xe
0x47B9FB: mov      eax, dword ptr [ebp - 0x44]
0x47B9FE: call     0x421ae0
0x47BA03: xor      edx, edx
0x47BA05: mov      eax, dword ptr [ebp - 0x44]
0x47BA08: call     0x4278c0
0x47BA0D: mov      eax, dword ptr [ebp - 0x44]
0x47BA10: mov      edx, dword ptr [ebp - 4]
0x47BA13: mov      dword ptr [eax + 0x1bc], edx
0x47BA19: mov      dword ptr [eax + 0x1b8], 0x47c758
0x47BA23: mov      eax, dword ptr [ebp - 0x44]
0x47BA26: mov      edx, dword ptr [ebp - 4]
0x47BA29: mov      dword ptr [eax + 0x19c], edx
0x47BA2F: mov      dword ptr [eax + 0x198], 0x47cb78
0x47BA39: mov      eax, dword ptr [ebp - 0x44]
0x47BA3C: mov      dword ptr [eax + 0xc], 0xb
0x47BA43: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47BA48: mov      eax, dword ptr [eax]
0x47BA4A: mov      ecx, dword ptr [eax + 4]
0x47BA4D: mov      dl, 1
0x47BA4F: mov      eax, dword ptr [0x41f814]
0x47BA54: call     0x4219b8
0x47BA59: mov      edx, dword ptr [ebp - 4]
0x47BA5C: mov      dword ptr [edx + 0x3c], eax
0x47BA5F: mov      eax, dword ptr [ebp - 4]
0x47BA62: mov      eax, dword ptr [eax + 0x3c]
0x47BA65: mov      dword ptr [ebp - 0x48], eax
0x47BA68: mov      edx, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47BA6E: mov      edx, dword ptr [edx]
0x47BA70: mov      eax, dword ptr [ebp - 0x48]
0x47BA73: mov      ecx, dword ptr [eax]
0x47BA75: call     dword ptr [ecx + 0x64]
0x47BA78: mov      edx, 0x10
0x47BA7D: mov      eax, dword ptr [ebp - 0x48]
0x47BA80: call     0x4271fc
0x47BA85: mov      edx, 0x74
0x47BA8A: mov      eax, dword ptr [ebp - 0x48]
0x47BA8D: call     0x4271d8
0x47BA92: mov      edx, dword ptr [ebp - 0xc]
0x47BA95: add      edx, 0xa1
0x47BA9B: mov      eax, dword ptr [ebp - 0x48]
0x47BA9E: call     0x42718c
0x47BAA3: mov      edx, dword ptr [ebp - 0x10]
0x47BAA6: add      edx, 0x170
0x47BAAC: mov      eax, dword ptr [ebp - 0x48]
0x47BAAF: call     0x4271b0
0x47BAB4: xor      edx, edx
0x47BAB6: mov      eax, dword ptr [ebp - 0x48]
0x47BAB9: call     0x421a94
0x47BABE: xor      edx, edx
0x47BAC0: mov      eax, dword ptr [ebp - 0x48]
0x47BAC3: call     0x427b5c
0x47BAC8: mov      eax, dword ptr [ebp - 0x48]
0x47BACB: mov      eax, dword ptr [eax + 0x58]
0x47BACE: mov      edx, 0xffffff
0x47BAD3: call     0x4173fc
0x47BAD8: mov      edx, 0xd
0x47BADD: mov      eax, dword ptr [ebp - 0x48]
0x47BAE0: call     0x421ae0
0x47BAE5: xor      edx, edx
0x47BAE7: mov      eax, dword ptr [ebp - 0x48]
0x47BAEA: call     0x4278c0
0x47BAEF: mov      eax, dword ptr [ebp - 0x48]
0x47BAF2: mov      edx, dword ptr [ebp - 4]
0x47BAF5: mov      dword ptr [eax + 0x1bc], edx
0x47BAFB: mov      dword ptr [eax + 0x1b8], 0x47c758
0x47BB05: mov      eax, dword ptr [ebp - 0x48]
0x47BB08: mov      edx, dword ptr [ebp - 4]
0x47BB0B: mov      dword ptr [eax + 0x19c], edx
0x47BB11: mov      dword ptr [eax + 0x198], 0x47cb78
0x47BB1B: mov      eax, dword ptr [ebp - 0x48]
0x47BB1E: mov      dword ptr [eax + 0xc], 0xb
0x47BB25: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47BB2A: mov      eax, dword ptr [eax]
0x47BB2C: mov      ecx, dword ptr [eax + 4]
0x47BB2F: mov      dl, 1
0x47BB31: mov      eax, dword ptr [0x41f814]
0x47BB36: call     0x4219b8
0x47BB3B: mov      edx, dword ptr [ebp - 4]
0x47BB3E: mov      dword ptr [edx + 0x40], eax
0x47BB41: mov      eax, dword ptr [ebp - 4]
0x47BB44: mov      eax, dword ptr [eax + 0x40]
0x47BB47: mov      dword ptr [ebp - 0x4c], eax
0x47BB4A: mov      edx, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47BB50: mov      edx, dword ptr [edx]
0x47BB52: mov      eax, dword ptr [ebp - 0x4c]
0x47BB55: mov      ecx, dword ptr [eax]
0x47BB57: call     dword ptr [ecx + 0x64]
0x47BB5A: mov      edx, 0x10
0x47BB5F: mov      eax, dword ptr [ebp - 0x4c]
0x47BB62: call     0x4271fc
0x47BB67: mov      edx, 0x74
0x47BB6C: mov      eax, dword ptr [ebp - 0x4c]
0x47BB6F: call     0x4271d8
0x47BB74: mov      edx, dword ptr [ebp - 0xc]
0x47BB77: add      edx, 0xa1
0x47BB7D: mov      eax, dword ptr [ebp - 0x4c]
0x47BB80: call     0x42718c
0x47BB85: mov      edx, dword ptr [ebp - 0x10]
0x47BB88: add      edx, 0x184
0x47BB8E: mov      eax, dword ptr [ebp - 0x4c]
0x47BB91: call     0x4271b0
0x47BB96: xor      edx, edx
0x47BB98: mov      eax, dword ptr [ebp - 0x4c]
0x47BB9B: call     0x421a94
0x47BBA0: xor      edx, edx
0x47BBA2: mov      eax, dword ptr [ebp - 0x4c]
0x47BBA5: call     0x427b5c
0x47BBAA: mov      eax, dword ptr [ebp - 0x4c]
0x47BBAD: mov      eax, dword ptr [eax + 0x58]
0x47BBB0: mov      edx, 0xffffff
0x47BBB5: call     0x4173fc
0x47BBBA: mov      edx, 0x28
0x47BBBF: mov      eax, dword ptr [ebp - 0x4c]
0x47BBC2: call     0x421ae0
0x47BBC7: xor      edx, edx
0x47BBC9: mov      eax, dword ptr [ebp - 0x4c]
0x47BBCC: call     0x4278c0
0x47BBD1: mov      eax, dword ptr [ebp - 0x4c]
0x47BBD4: mov      edx, dword ptr [ebp - 4]
0x47BBD7: mov      dword ptr [eax + 0x1bc], edx
0x47BBDD: mov      dword ptr [eax + 0x1b8], 0x47c758
0x47BBE7: mov      eax, dword ptr [ebp - 0x4c]
0x47BBEA: mov      edx, dword ptr [ebp - 4]
0x47BBED: mov      dword ptr [eax + 0x19c], edx
0x47BBF3: mov      dword ptr [eax + 0x198], 0x47cb78
0x47BBFD: mov      eax, dword ptr [ebp - 0x4c]
0x47BC00: mov      dword ptr [eax + 0xc], 0xb
0x47BC07: mov      dword ptr [ebp - 0xc], 0xc0
0x47BC0E: mov      dword ptr [ebp - 0x10], 0x96
0x47BC15: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47BC1A: mov      eax, dword ptr [eax]
0x47BC1C: mov      ecx, dword ptr [eax + 4]
0x47BC1F: mov      dl, 1
0x47BC21: mov      eax, dword ptr [0x41f814]
0x47BC26: call     0x4219b8
0x47BC2B: mov      edx, dword ptr [ebp - 4]
0x47BC2E: mov      dword ptr [edx + 0x44], eax
0x47BC31: mov      eax, dword ptr [ebp - 4]
0x47BC34: mov      eax, dword ptr [eax + 0x44]
0x47BC37: mov      dword ptr [ebp - 0x50], eax
0x47BC3A: mov      edx, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47BC40: mov      edx, dword ptr [edx]
0x47BC42: mov      eax, dword ptr [ebp - 0x50]
0x47BC45: mov      ecx, dword ptr [eax]
0x47BC47: call     dword ptr [ecx + 0x64]
0x47BC4A: mov      edx, 0x10
0x47BC4F: mov      eax, dword ptr [ebp - 0x50]
0x47BC52: call     0x4271fc
0x47BC57: mov      edx, 0x89
0x47BC5C: mov      eax, dword ptr [ebp - 0x50]
0x47BC5F: call     0x4271d8
0x47BC64: mov      edx, dword ptr [ebp - 0xc]
0x47BC67: add      edx, 0xef
0x47BC6D: mov      eax, dword ptr [ebp - 0x50]
0x47BC70: call     0x42718c
0x47BC75: mov      edx, dword ptr [ebp - 0x10]
0x47BC78: add      edx, 0x75
0x47BC7B: mov      eax, dword ptr [ebp - 0x50]
0x47BC7E: call     0x4271b0
0x47BC83: xor      edx, edx
0x47BC85: mov      eax, dword ptr [ebp - 0x50]
0x47BC88: call     0x421a94
0x47BC8D: xor      edx, edx
0x47BC8F: mov      eax, dword ptr [ebp - 0x50]
0x47BC92: call     0x427b5c
0x47BC97: mov      eax, dword ptr [ebp - 0x50]
0x47BC9A: mov      eax, dword ptr [eax + 0x58]
0x47BC9D: mov      edx, 0xffffff
0x47BCA2: call     0x4173fc
0x47BCA7: mov      edx, 0xa
0x47BCAC: mov      eax, dword ptr [ebp - 0x50]
0x47BCAF: call     0x421ae0
0x47BCB4: xor      edx, edx
0x47BCB6: mov      eax, dword ptr [ebp - 0x50]
0x47BCB9: call     0x4278c0
0x47BCBE: mov      eax, dword ptr [ebp - 0x50]
0x47BCC1: mov      edx, dword ptr [ebp - 4]
0x47BCC4: mov      dword ptr [eax + 0x1bc], edx
0x47BCCA: mov      dword ptr [eax + 0x1b8], 0x47c758
0x47BCD4: mov      eax, dword ptr [ebp - 0x50]
0x47BCD7: mov      edx, dword ptr [ebp - 4]
0x47BCDA: mov      dword ptr [eax + 0x19c], edx
0x47BCE0: mov      dword ptr [eax + 0x198], 0x47cb78
0x47BCEA: mov      eax, dword ptr [ebp - 0x50]
0x47BCED: mov      dword ptr [eax + 0xc], 0xc
0x47BCF4: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47BCF9: mov      eax, dword ptr [eax]
0x47BCFB: mov      ecx, dword ptr [eax + 4]
0x47BCFE: mov      dl, 1
0x47BD00: mov      eax, dword ptr [0x41f814]
0x47BD05: call     0x4219b8
0x47BD0A: mov      edx, dword ptr [ebp - 4]
0x47BD0D: mov      dword ptr [edx + 0x48], eax
0x47BD10: mov      eax, dword ptr [ebp - 4]
0x47BD13: mov      eax, dword ptr [eax + 0x48]
0x47BD16: mov      dword ptr [ebp - 0x54], eax
0x47BD19: mov      edx, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47BD1F: mov      edx, dword ptr [edx]
0x47BD21: mov      eax, dword ptr [ebp - 0x54]
0x47BD24: mov      ecx, dword ptr [eax]
0x47BD26: call     dword ptr [ecx + 0x64]
0x47BD29: mov      edx, 0x10
0x47BD2E: mov      eax, dword ptr [ebp - 0x54]
0x47BD31: call     0x4271fc
0x47BD36: mov      edx, 0x89
0x47BD3B: mov      eax, dword ptr [ebp - 0x54]
0x47BD3E: call     0x4271d8
0x47BD43: mov      edx, dword ptr [ebp - 0xc]
0x47BD46: add      edx, 0xef
0x47BD4C: mov      eax, dword ptr [ebp - 0x54]
0x47BD4F: call     0x42718c
0x47BD54: mov      edx, dword ptr [ebp - 0x10]
0x47BD57: add      edx, 0x95
0x47BD5D: mov      eax, dword ptr [ebp - 0x54]
0x47BD60: call     0x4271b0
0x47BD65: xor      edx, edx
0x47BD67: mov      eax, dword ptr [ebp - 0x54]
0x47BD6A: call     0x421a94
0x47BD6F: xor      edx, edx
0x47BD71: mov      eax, dword ptr [ebp - 0x54]
0x47BD74: call     0x427b5c
0x47BD79: mov      eax, dword ptr [ebp - 0x54]
0x47BD7C: mov      eax, dword ptr [eax + 0x58]
0x47BD7F: mov      edx, 0xffffff
0x47BD84: call     0x4173fc
0x47BD89: mov      edx, 0xa
0x47BD8E: mov      eax, dword ptr [ebp - 0x54]
0x47BD91: call     0x421ae0
0x47BD96: mov      dl, 0x2a
0x47BD98: mov      eax, dword ptr [ebp - 0x54]
0x47BD9B: call     0x421b94
0x47BDA0: xor      edx, edx
0x47BDA2: mov      eax, dword ptr [ebp - 0x54]
0x47BDA5: call     0x4278c0
0x47BDAA: mov      eax, dword ptr [ebp - 0x54]
0x47BDAD: mov      edx, dword ptr [ebp - 4]
0x47BDB0: mov      dword ptr [eax + 0x1bc], edx
0x47BDB6: mov      dword ptr [eax + 0x1b8], 0x47c758
0x47BDC0: mov      eax, dword ptr [ebp - 0x54]
0x47BDC3: mov      edx, dword ptr [ebp - 4]
0x47BDC6: mov      dword ptr [eax + 0x19c], edx
0x47BDCC: mov      dword ptr [eax + 0x198], 0x47cb78
0x47BDD6: mov      eax, dword ptr [ebp - 0x54]
0x47BDD9: mov      dword ptr [eax + 0xc], 0xc
0x47BDE0: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47BDE5: mov      eax, dword ptr [eax]
0x47BDE7: mov      ecx, dword ptr [eax + 4]
0x47BDEA: mov      dl, 1
0x47BDEC: mov      eax, dword ptr [0x41f814]
0x47BDF1: call     0x4219b8
0x47BDF6: mov      edx, dword ptr [ebp - 4]
0x47BDF9: mov      dword ptr [edx + 0x4c], eax
0x47BDFC: mov      eax, dword ptr [ebp - 4]
0x47BDFF: mov      eax, dword ptr [eax + 0x4c]
0x47BE02: mov      dword ptr [ebp - 0x58], eax
0x47BE05: mov      edx, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47BE0B: mov      edx, dword ptr [edx]
0x47BE0D: mov      eax, dword ptr [ebp - 0x58]
0x47BE10: mov      ecx, dword ptr [eax]
0x47BE12: call     dword ptr [ecx + 0x64]
0x47BE15: mov      edx, 0x10
0x47BE1A: mov      eax, dword ptr [ebp - 0x58]
0x47BE1D: call     0x4271fc
0x47BE22: mov      edx, 0x89
0x47BE27: mov      eax, dword ptr [ebp - 0x58]
0x47BE2A: call     0x4271d8
0x47BE2F: mov      edx, dword ptr [ebp - 0xc]
0x47BE32: add      edx, 0xef
0x47BE38: mov      eax, dword ptr [ebp - 0x58]
0x47BE3B: call     0x42718c
0x47BE40: mov      edx, dword ptr [ebp - 0x10]
0x47BE43: add      edx, 0xb0
0x47BE49: mov      eax, dword ptr [ebp - 0x58]
0x47BE4C: call     0x4271b0
0x47BE51: xor      edx, edx
0x47BE53: mov      eax, dword ptr [ebp - 0x58]
0x47BE56: call     0x421a94
0x47BE5B: xor      edx, edx
0x47BE5D: mov      eax, dword ptr [ebp - 0x58]
0x47BE60: call     0x427b5c
0x47BE65: mov      eax, dword ptr [ebp - 0x58]
0x47BE68: mov      eax, dword ptr [eax + 0x58]
0x47BE6B: mov      edx, 0xffffff
0x47BE70: call     0x4173fc
0x47BE75: mov      edx, 0xa
0x47BE7A: mov      eax, dword ptr [ebp - 0x58]
0x47BE7D: call     0x421ae0
0x47BE82: mov      dl, 0x2a
0x47BE84: mov      eax, dword ptr [ebp - 0x58]
0x47BE87: call     0x421b94
0x47BE8C: xor      edx, edx
0x47BE8E: mov      eax, dword ptr [ebp - 0x58]
0x47BE91: call     0x4278c0
0x47BE96: mov      eax, dword ptr [ebp - 0x58]
0x47BE99: mov      edx, dword ptr [ebp - 4]
0x47BE9C: mov      dword ptr [eax + 0x1bc], edx
0x47BEA2: mov      dword ptr [eax + 0x1b8], 0x47c758
0x47BEAC: mov      eax, dword ptr [ebp - 0x58]
0x47BEAF: mov      edx, dword ptr [ebp - 4]
0x47BEB2: mov      dword ptr [eax + 0x19c], edx
0x47BEB8: mov      dword ptr [eax + 0x198], 0x47cb78
0x47BEC2: mov      eax, dword ptr [ebp - 0x58]
0x47BEC5: mov      dword ptr [eax + 0xc], 0xc
0x47BECC: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47BED1: mov      eax, dword ptr [eax]
0x47BED3: mov      ecx, dword ptr [eax + 4]
0x47BED6: mov      dl, 1
0x47BED8: mov      eax, dword ptr [0x41f814]
0x47BEDD: call     0x4219b8
0x47BEE2: mov      edx, dword ptr [ebp - 4]
0x47BEE5: mov      dword ptr [edx + 0x50], eax
0x47BEE8: mov      eax, dword ptr [ebp - 4]
0x47BEEB: mov      eax, dword ptr [eax + 0x50]
0x47BEEE: mov      dword ptr [ebp - 0x5c], eax
0x47BEF1: mov      edx, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47BEF7: mov      edx, dword ptr [edx]
0x47BEF9: mov      eax, dword ptr [ebp - 0x5c]
0x47BEFC: mov      ecx, dword ptr [eax]
0x47BEFE: call     dword ptr [ecx + 0x64]
0x47BF01: mov      edx, 0x10
0x47BF06: mov      eax, dword ptr [ebp - 0x5c]
0x47BF09: call     0x4271fc
0x47BF0E: mov      edx, 0x89
0x47BF13: mov      eax, dword ptr [ebp - 0x5c]
0x47BF16: call     0x4271d8
0x47BF1B: mov      edx, dword ptr [ebp - 0xc]
0x47BF1E: add      edx, 0xef
0x47BF24: mov      eax, dword ptr [ebp - 0x5c]
0x47BF27: call     0x42718c
0x47BF2C: mov      edx, dword ptr [ebp - 0x10]
0x47BF2F: add      edx, 0xd0
0x47BF35: mov      eax, dword ptr [ebp - 0x5c]
0x47BF38: call     0x4271b0
0x47BF3D: xor      edx, edx
0x47BF3F: mov      eax, dword ptr [ebp - 0x5c]
0x47BF42: call     0x421a94
0x47BF47: xor      edx, edx
0x47BF49: mov      eax, dword ptr [ebp - 0x5c]
0x47BF4C: call     0x427b5c
0x47BF51: mov      eax, dword ptr [ebp - 0x5c]
0x47BF54: mov      eax, dword ptr [eax + 0x58]
0x47BF57: mov      edx, 0xffffff
0x47BF5C: call     0x4173fc
0x47BF61: mov      edx, 0xa
0x47BF66: mov      eax, dword ptr [ebp - 0x5c]
0x47BF69: call     0x421ae0
0x47BF6E: mov      dl, 0x2a
0x47BF70: mov      eax, dword ptr [ebp - 0x5c]
0x47BF73: call     0x421b94
0x47BF78: xor      edx, edx
0x47BF7A: mov      eax, dword ptr [ebp - 0x5c]
0x47BF7D: call     0x4278c0
0x47BF82: mov      eax, dword ptr [ebp - 0x5c]
0x47BF85: mov      edx, dword ptr [ebp - 4]
0x47BF88: mov      dword ptr [eax + 0x1bc], edx
0x47BF8E: mov      dword ptr [eax + 0x1b8], 0x47c758
0x47BF98: mov      eax, dword ptr [ebp - 0x5c]
0x47BF9B: mov      edx, dword ptr [ebp - 4]
0x47BF9E: mov      dword ptr [eax + 0x19c], edx
0x47BFA4: mov      dword ptr [eax + 0x198], 0x47cb78
0x47BFAE: mov      eax, dword ptr [ebp - 0x5c]
0x47BFB1: mov      dword ptr [eax + 0xc], 0xc
0x47BFB8: mov      eax, dword ptr [ebp - 4]
0x47BFBB: cmp      byte ptr [ebp - 5], 0
0x47BFBF: je       0x47bfd0
0x47BFC1: call     0x4031fc
0x47BFC6: pop      dword ptr fs:[0]
0x47BFCD: add      esp, 0xc
0x47BFD0: mov      eax, dword ptr [ebp - 4]
0x47BFD3: mov      esp, ebp
0x47BFD5: pop      ebp
0x47BFD6: ret      
0x47BFD7: nop      
0x47BFD8: push     ebp
0x47BFD9: mov      ebp, esp
0x47BFDB: add      esp, -8
0x47BFDE: call     0x403204
0x47BFE3: mov      byte ptr [ebp - 5], dl
0x47BFE6: mov      dword ptr [ebp - 4], eax
0x47BFE9: mov      dl, byte ptr [ebp - 5]
0x47BFEC: and      dl, 0xfc
0x47BFEF: mov      eax, dword ptr [ebp - 4]
0x47BFF2: call     0x402e9c
0x47BFF7: cmp      byte ptr [ebp - 5], 0
0x47BFFB: jle      0x47c005
0x47BFFD: mov      eax, dword ptr [ebp - 4]
0x47C000: call     0x4031f4
0x47C005: pop      ecx
0x47C006: pop      ecx
0x47C007: pop      ebp
0x47C008: ret      
0x47C009: lea      eax, [eax]
0x47C00C: push     ebp
0x47C00D: mov      ebp, esp
0x47C00F: add      esp, -0x14
0x47C012: mov      dword ptr [ebp - 4], eax
0x47C015: mov      eax, dword ptr [ebp - 4]
0x47C018: xor      edx, edx
0x47C01A: mov      dword ptr [eax + 0x54], edx
0x47C01D: mov      eax, dword ptr [ebp - 4]
0x47C020: mov      dword ptr [eax + 0x58], 0xa
0x47C027: mov      eax, dword ptr [ebp - 4]
0x47C02A: add      eax, 0x15c
0x47C02F: call     0x403a58
0x47C034: mov      eax, dword ptr [ebp - 4]
0x47C037: add      eax, 0x160
0x47C03C: call     0x403a58
0x47C041: mov      eax, dword ptr [ebp - 4]
0x47C044: mov      eax, dword ptr [eax + 8]
0x47C047: mov      dword ptr [ebp - 8], eax
0x47C04A: mov      edx, 0x15e
0x47C04F: mov      eax, dword ptr [ebp - 8]
0x47C052: call     0x42718c
0x47C057: mov      edx, 0x103
0x47C05C: mov      eax, dword ptr [ebp - 8]
0x47C05F: call     0x4271b0
0x47C064: mov      edx, 0x10
0x47C069: mov      eax, dword ptr [ebp - 8]
0x47C06C: call     0x4271fc
0x47C071: mov      edx, 0x89
0x47C076: mov      eax, dword ptr [ebp - 8]
0x47C079: call     0x4271d8
0x47C07E: xor      edx, edx
0x47C080: mov      eax, dword ptr [ebp - 8]
0x47C083: call     0x4278c0
0x47C088: mov      eax, dword ptr [ebp - 4]
0x47C08B: mov      eax, dword ptr [eax + 0xc]
0x47C08E: mov      dword ptr [ebp - 0xc], eax
0x47C091: mov      edx, 0x15e
0x47C096: mov      eax, dword ptr [ebp - 0xc]
0x47C099: call     0x42718c
0x47C09E: mov      edx, 0x123
0x47C0A3: mov      eax, dword ptr [ebp - 0xc]
0x47C0A6: call     0x4271b0
0x47C0AB: mov      edx, 0x10
0x47C0B0: mov      eax, dword ptr [ebp - 0xc]
0x47C0B3: call     0x4271fc
0x47C0B8: mov      edx, 0x89
0x47C0BD: mov      eax, dword ptr [ebp - 0xc]
0x47C0C0: call     0x4271d8
0x47C0C5: xor      edx, edx
0x47C0C7: mov      eax, dword ptr [ebp - 0xc]
0x47C0CA: call     0x4278c0
0x47C0CF: mov      eax, dword ptr [ebp - 4]
0x47C0D2: mov      byte ptr [eax + 0x61], 1
0x47C0D6: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47C0DB: mov      eax, dword ptr [eax]
0x47C0DD: mov      eax, dword ptr [eax + 0x2f8]
0x47C0E3: mov      dl, 1
0x47C0E5: call     0x458214
0x47C0EA: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47C0EF: mov      eax, dword ptr [eax]
0x47C0F1: mov      eax, dword ptr [eax + 0x304]
0x47C0F7: xor      edx, edx
0x47C0F9: call     0x458214
0x47C0FE: mov      eax, dword ptr [ebp - 4]
0x47C101: mov      byte ptr [eax + 0x60], 0
0x47C105: mov      eax, 0x47c11c ; "wav\log-in-long2.wav"
0x47C10A: call     0x49588c
0x47C10F: mov      esp, ebp
0x47C111: pop      ebp
0x47C112: ret      
0x47C113: add      bh, bh
0x47C117: call     dword ptr [eax + eax]
0x47C11A: add      byte ptr [eax], al
0x47C11C: ja       0x47c17f
0x47C11E: jbe      0x47c17c
0x47C120: insb     byte ptr es:[edi], dx
0x47C121: outsd    dx, dword ptr [esi]
0x47C122: sub      eax, 0x6c2d6e69
0x47C128: outsd    dx, dword ptr [esi]
0x47C129: outsb    dx, byte ptr [esi]
0x47C12A: xor      ch, byte ptr [0x6177]
0x47C12F: jbe      0x47c131
0x47C131: add      byte ptr [eax], al
0x47C133: add      byte ptr [ebp - 0x75], dl
0x47C136: in       al, dx
0x47C137: push     ecx
0x47C138: mov      dword ptr [ebp - 4], eax
0x47C13B: xor      edx, edx
0x47C13D: mov      eax, dword ptr [ebp - 4]
0x47C140: mov      eax, dword ptr [eax + 8]
0x47C143: call     0x4278c0
0x47C148: xor      edx, edx
0x47C14A: mov      eax, dword ptr [ebp - 4]
0x47C14D: mov      eax, dword ptr [eax + 0xc]
0x47C150: call     0x4278c0
0x47C155: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47C15A: mov      eax, dword ptr [eax]
0x47C15C: mov      eax, dword ptr [eax + 0x2f8]
0x47C162: xor      edx, edx
0x47C164: call     0x458214
0x47C169: call     0x495928
0x47C16E: pop      ecx
0x47C16F: pop      ebp
0x47C170: ret      
0x47C171: lea      eax, [eax]
0x47C174: push     ebp
0x47C175: mov      ebp, esp
0x47C177: add      esp, -0x14
0x47C17A: push     ebx
0x47C17B: xor      ebx, ebx
0x47C17D: mov      dword ptr [ebp - 0x10], ebx
0x47C180: mov      dword ptr [ebp - 0x14], ebx
0x47C183: mov      dword ptr [ebp - 8], ecx
0x47C186: mov      dword ptr [ebp - 0xc], edx
0x47C189: mov      dword ptr [ebp - 4], eax
0x47C18C: xor      eax, eax
0x47C18E: push     ebp
0x47C18F: push     0x47c209
0x47C194: push     dword ptr fs:[eax]
0x47C197: mov      dword ptr fs:[eax], esp
0x47C19A: mov      eax, dword ptr [ebp - 8]
0x47C19D: cmp      byte ptr [eax], 0xd
0x47C1A0: jne      0x47c1eb
0x47C1A2: mov      eax, dword ptr [ebp - 8]
0x47C1A5: mov      byte ptr [eax], 0
0x47C1A8: lea      edx, [ebp - 0x14]
0x47C1AB: mov      eax, dword ptr [ebp - 4]
0x47C1AE: mov      eax, dword ptr [eax + 8]
0x47C1B1: call     0x4279d0
0x47C1B6: mov      eax, dword ptr [ebp - 0x14]
0x47C1B9: lea      edx, [ebp - 0x10]
0x47C1BC: call     0x407aa8
0x47C1C1: mov      edx, dword ptr [ebp - 0x10]
0x47C1C4: mov      eax, dword ptr [ebp - 4]
0x47C1C7: add      eax, 0x15c
0x47C1CC: call     0x403aac
0x47C1D1: mov      eax, dword ptr [ebp - 4]
0x47C1D4: cmp      dword ptr [eax + 0x15c], 0
0x47C1DB: je       0x47c1eb
0x47C1DD: mov      eax, dword ptr [ebp - 4]
0x47C1E0: mov      eax, dword ptr [eax + 0xc]
0x47C1E3: mov      edx, dword ptr [eax]
0x47C1E5: call     dword ptr [edx + 0xb4]
0x47C1EB: xor      eax, eax
0x47C1ED: pop      edx
0x47C1EE: pop      ecx
0x47C1EF: pop      ecx
0x47C1F0: mov      dword ptr fs:[eax], edx
0x47C1F3: push     0x47c210
0x47C1F8: lea      eax, [ebp - 0x14]
0x47C1FB: call     0x403a58
0x47C200: lea      eax, [ebp - 0x10]
0x47C203: call     0x403a58
0x47C208: ret      
0x47C209: jmp      0x403518
0x47C20E: jmp      0x47c1f8
0x47C210: pop      ebx
0x47C211: mov      esp, ebp
0x47C213: pop      ebp
0x47C214: ret      
0x47C215: lea      eax, [eax]
0x47C218: push     ebp
0x47C219: mov      ebp, esp
0x47C21B: add      esp, -0x14
0x47C21E: push     ebx
0x47C21F: xor      ebx, ebx
0x47C221: mov      dword ptr [ebp - 0x14], ebx
0x47C224: mov      dword ptr [ebp - 0x10], ebx
0x47C227: mov      dword ptr [ebp - 8], ecx
0x47C22A: mov      dword ptr [ebp - 0xc], edx
0x47C22D: mov      dword ptr [ebp - 4], eax
0x47C230: xor      eax, eax
0x47C232: push     ebp
0x47C233: push     0x47c365
0x47C238: push     dword ptr fs:[eax]
0x47C23B: mov      dword ptr fs:[eax], esp
0x47C23E: mov      eax, dword ptr [ebp - 8]
0x47C241: cmp      byte ptr [eax], 0x7e
0x47C244: je       0x47c24e
0x47C246: mov      eax, dword ptr [ebp - 8]
0x47C249: cmp      byte ptr [eax], 0x27
0x47C24C: jne      0x47c254
0x47C24E: mov      eax, dword ptr [ebp - 8]
0x47C251: mov      byte ptr [eax], 0x5f
0x47C254: mov      eax, dword ptr [ebp - 8]
0x47C257: cmp      byte ptr [eax], 0xd
0x47C25A: jne      0x47c347
0x47C260: mov      eax, dword ptr [ebp - 8]
0x47C263: mov      byte ptr [eax], 0
0x47C266: lea      edx, [ebp - 0x14]
0x47C269: mov      eax, dword ptr [ebp - 4]
0x47C26C: mov      eax, dword ptr [eax + 8]
0x47C26F: call     0x4279d0
0x47C274: mov      eax, dword ptr [ebp - 0x14]
0x47C277: lea      edx, [ebp - 0x10]
0x47C27A: call     0x407aa8
0x47C27F: mov      edx, dword ptr [ebp - 0x10]
0x47C282: mov      eax, dword ptr [ebp - 4]
0x47C285: add      eax, 0x15c
0x47C28A: call     0x403aac
0x47C28F: lea      edx, [ebp - 0x14]
0x47C292: mov      eax, dword ptr [ebp - 4]
0x47C295: mov      eax, dword ptr [eax + 0xc]
0x47C298: call     0x4279d0
0x47C29D: mov      edx, dword ptr [ebp - 0x14]
0x47C2A0: mov      eax, dword ptr [ebp - 4]
0x47C2A3: add      eax, 0x160
0x47C2A8: call     0x403aac
0x47C2AD: mov      eax, dword ptr [ebp - 4]
0x47C2B0: cmp      dword ptr [eax + 0x15c], 0
0x47C2B7: je       0x47c319
0x47C2B9: mov      eax, dword ptr [ebp - 4]
0x47C2BC: cmp      dword ptr [eax + 0x160], 0
0x47C2C3: je       0x47c319
0x47C2C5: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47C2CA: mov      eax, dword ptr [eax]
0x47C2CC: mov      edx, dword ptr [ebp - 4]
0x47C2CF: mov      ecx, dword ptr [edx + 0x160]
0x47C2D5: mov      edx, dword ptr [ebp - 4]
0x47C2D8: mov      edx, dword ptr [edx + 0x15c]
0x47C2DE: call     0x464b94
0x47C2E3: xor      edx, edx
0x47C2E5: mov      eax, dword ptr [ebp - 4]
0x47C2E8: mov      eax, dword ptr [eax + 8]
0x47C2EB: call     0x427a00
0x47C2F0: xor      edx, edx
0x47C2F2: mov      eax, dword ptr [ebp - 4]
0x47C2F5: mov      eax, dword ptr [eax + 0xc]
0x47C2F8: call     0x427a00
0x47C2FD: xor      edx, edx
0x47C2FF: mov      eax, dword ptr [ebp - 4]
0x47C302: mov      eax, dword ptr [eax + 8]
0x47C305: call     0x4278c0
0x47C30A: xor      edx, edx
0x47C30C: mov      eax, dword ptr [ebp - 4]
0x47C30F: mov      eax, dword ptr [eax + 0xc]
0x47C312: call     0x4278c0
0x47C317: jmp      0x47c347
0x47C319: mov      eax, dword ptr [ebp - 4]
0x47C31C: mov      eax, dword ptr [eax + 8]
0x47C31F: cmp      byte ptr [eax + 0x47], 0
0x47C323: je       0x47c347
0x47C325: lea      edx, [ebp - 0x14]
0x47C328: mov      eax, dword ptr [ebp - 4]
0x47C32B: mov      eax, dword ptr [eax + 8]
0x47C32E: call     0x4279d0
0x47C333: cmp      dword ptr [ebp - 0x14], 0
0x47C337: jne      0x47c347
0x47C339: mov      eax, dword ptr [ebp - 4]
0x47C33C: mov      eax, dword ptr [eax + 8]
0x47C33F: mov      edx, dword ptr [eax]
0x47C341: call     dword ptr [edx + 0xb4]
0x47C347: xor      eax, eax
0x47C349: pop      edx
0x47C34A: pop      ecx
0x47C34B: pop      ecx
0x47C34C: mov      dword ptr fs:[eax], edx
0x47C34F: push     0x47c36c
0x47C354: lea      eax, [ebp - 0x14]
0x47C357: call     0x403a58
0x47C35C: lea      eax, [ebp - 0x10]
0x47C35F: call     0x403a58
0x47C364: ret      
0x47C365: jmp      0x403518
0x47C36A: jmp      0x47c354
0x47C36C: pop      ebx
0x47C36D: mov      esp, ebp
0x47C36F: pop      ebp
0x47C370: ret      
0x47C371: lea      eax, [eax]
0x47C374: push     ebp
0x47C375: mov      ebp, esp
0x47C377: push     ecx
0x47C378: mov      dword ptr [ebp - 4], eax
0x47C37B: mov      dl, 1
0x47C37D: mov      eax, dword ptr [ebp - 4]
0x47C380: mov      eax, dword ptr [eax + 8]
0x47C383: call     0x4278c0
0x47C388: mov      dl, 1
0x47C38A: mov      eax, dword ptr [ebp - 4]
0x47C38D: mov      eax, dword ptr [eax + 0xc]
0x47C390: call     0x4278c0
0x47C395: mov      eax, dword ptr [ebp - 4]
0x47C398: mov      eax, dword ptr [eax + 8]
0x47C39B: mov      edx, dword ptr [eax]
0x47C39D: call     dword ptr [edx + 0xb4]
0x47C3A3: pop      ecx
0x47C3A4: pop      ebp
0x47C3A5: ret      
0x47C3A6: mov      eax, eax
0x47C3A8: push     ebp
0x47C3A9: mov      ebp, esp
0x47C3AB: add      esp, -0x10
0x47C3AE: xor      edx, edx
0x47C3B0: mov      dword ptr [ebp - 0x10], edx
0x47C3B3: mov      dword ptr [ebp - 0xc], edx
0x47C3B6: mov      dword ptr [ebp - 4], eax
0x47C3B9: xor      eax, eax
0x47C3BB: push     ebp
0x47C3BC: push     0x47c442
0x47C3C1: push     dword ptr fs:[eax]
0x47C3C4: mov      dword ptr fs:[eax], esp
0x47C3C7: mov      byte ptr [ebp - 5], 1
0x47C3CB: lea      edx, [ebp - 0x10]
0x47C3CE: mov      eax, dword ptr [ebp - 4]
0x47C3D1: mov      eax, dword ptr [eax + 0x10]
0x47C3D4: call     0x4279d0
0x47C3D9: mov      eax, dword ptr [ebp - 0x10]
0x47C3DC: lea      edx, [ebp - 0xc]
0x47C3DF: call     0x407be4
0x47C3E4: mov      edx, dword ptr [ebp - 0xc]
0x47C3E7: mov      eax, dword ptr [ebp - 4]
0x47C3EA: mov      eax, dword ptr [eax + 0x10]
0x47C3ED: call     0x427a00
0x47C3F2: lea      edx, [ebp - 0x10]
0x47C3F5: mov      eax, dword ptr [ebp - 4]
0x47C3F8: mov      eax, dword ptr [eax + 0x10]
0x47C3FB: call     0x4279d0
0x47C400: mov      eax, dword ptr [ebp - 0x10]
0x47C403: call     0x403cd4
0x47C408: cmp      eax, 3
0x47C40B: jge      0x47c424
0x47C40D: call     0x40b5fc
0x47C412: mov      eax, dword ptr [ebp - 4]
0x47C415: mov      eax, dword ptr [eax + 0x10]
0x47C418: mov      edx, dword ptr [eax]
0x47C41A: call     dword ptr [edx + 0xb4]
0x47C420: mov      byte ptr [ebp - 5], 0
0x47C424: xor      eax, eax
0x47C426: pop      edx
0x47C427: pop      ecx
0x47C428: pop      ecx
0x47C429: mov      dword ptr fs:[eax], edx
0x47C42C: push     0x47c449
0x47C431: lea      eax, [ebp - 0x10]
0x47C434: call     0x403a58
0x47C439: lea      eax, [ebp - 0xc]
0x47C43C: call     0x403a58
0x47C441: ret      
0x47C442: jmp      0x403518
0x47C447: jmp      0x47c431
0x47C449: mov      al, byte ptr [ebp - 5]
0x47C44C: mov      esp, ebp
0x47C44E: pop      ebp
0x47C44F: ret      
0x47C450: push     ebp
0x47C451: mov      ebp, esp
0x47C453: add      esp, -0x40
0x47C456: xor      edx, edx
0x47C458: mov      dword ptr [ebp - 0x3c], edx
0x47C45B: mov      dword ptr [ebp - 0xc], edx
0x47C45E: mov      dword ptr [ebp - 0x10], edx
0x47C461: mov      dword ptr [ebp - 0x14], edx
0x47C464: mov      dword ptr [ebp - 0x30], edx
0x47C467: mov      dword ptr [ebp - 0x34], edx
0x47C46A: mov      dword ptr [ebp - 0x18], edx
0x47C46D: mov      dword ptr [ebp - 0x1c], edx
0x47C470: mov      dword ptr [ebp - 4], eax
0x47C473: xor      eax, eax
0x47C475: push     ebp
0x47C476: push     0x47c5e0
0x47C47B: push     dword ptr fs:[eax]
0x47C47E: mov      dword ptr fs:[eax], esp
0x47C481: mov      byte ptr [ebp - 5], 1
0x47C485: lea      edx, [ebp - 0xc]
0x47C488: mov      eax, dword ptr [ebp - 4]
0x47C48B: mov      eax, dword ptr [eax + 0x20]
0x47C48E: call     0x4279d0
0x47C493: push     0
0x47C495: lea      eax, [ebp - 0x3c]
0x47C498: push     eax
0x47C499: mov      byte ptr [ebp - 0x40], 0x2d
0x47C49D: lea      ecx, [ebp - 0x40]
0x47C4A0: lea      edx, [ebp - 0x10]
0x47C4A3: mov      eax, dword ptr [ebp - 0xc]
0x47C4A6: call     0x452530
0x47C4AB: mov      edx, dword ptr [ebp - 0x3c]
0x47C4AE: lea      eax, [ebp - 0xc]
0x47C4B1: call     0x403af0
0x47C4B6: push     0
0x47C4B8: lea      eax, [ebp - 0x3c]
0x47C4BB: push     eax
0x47C4BC: mov      byte ptr [ebp - 0x40], 0x2d
0x47C4C0: lea      ecx, [ebp - 0x40]
0x47C4C3: lea      edx, [ebp - 0x14]
0x47C4C6: mov      eax, dword ptr [ebp - 0xc]
0x47C4C9: call     0x452530
0x47C4CE: mov      byte ptr [ebp - 0x29], 1
0x47C4D2: mov      eax, dword ptr [ebp - 0x10]
0x47C4D5: call     0x403cd4
0x47C4DA: cmp      eax, 6
0x47C4DD: jne      0x47c58f
0x47C4E3: mov      eax, dword ptr [ebp - 0x14]
0x47C4E6: call     0x403cd4
0x47C4EB: cmp      eax, 7
0x47C4EE: jne      0x47c58f
0x47C4F4: lea      eax, [ebp - 0x18]
0x47C4F7: push     eax
0x47C4F8: mov      ecx, 2
0x47C4FD: mov      edx, 3
0x47C502: mov      eax, dword ptr [ebp - 0x10]
0x47C505: call     0x403ed8
0x47C50A: lea      eax, [ebp - 0x1c]
0x47C50D: push     eax
0x47C50E: mov      ecx, 2
0x47C513: mov      edx, 5
0x47C518: mov      eax, dword ptr [ebp - 0x10]
0x47C51B: call     0x403ed8
0x47C520: xor      edx, edx
0x47C522: mov      eax, dword ptr [ebp - 0x18]
0x47C525: call     0x4520e8
0x47C52A: mov      dword ptr [ebp - 0x20], eax
0x47C52D: xor      edx, edx
0x47C52F: mov      eax, dword ptr [ebp - 0x1c]
0x47C532: call     0x4520e8
0x47C537: mov      dword ptr [ebp - 0x24], eax
0x47C53A: cmp      dword ptr [ebp - 0x20], 0
0x47C53E: jle      0x47c546
0x47C540: cmp      dword ptr [ebp - 0x20], 0xc
0x47C544: jle      0x47c54a
0x47C546: mov      byte ptr [ebp - 0x29], 0
0x47C54A: cmp      dword ptr [ebp - 0x24], 0
0x47C54E: jle      0x47c556
0x47C550: cmp      dword ptr [ebp - 0x24], 0x1f
0x47C554: jle      0x47c55a
0x47C556: mov      byte ptr [ebp - 0x29], 0
0x47C55A: lea      eax, [ebp - 0x3c]
0x47C55D: push     eax
0x47C55E: mov      ecx, 1
0x47C563: mov      edx, 1
0x47C568: mov      eax, dword ptr [ebp - 0x14]
0x47C56B: call     0x403ed8
0x47C570: mov      eax, dword ptr [ebp - 0x3c]
0x47C573: xor      edx, edx
0x47C575: call     0x4520e8
0x47C57A: mov      dword ptr [ebp - 0x28], eax
0x47C57D: cmp      dword ptr [ebp - 0x28], 0
0x47C581: jle      0x47c589
0x47C583: cmp      dword ptr [ebp - 0x28], 2
0x47C587: jle      0x47c593
0x47C589: mov      byte ptr [ebp - 0x29], 0
0x47C58D: jmp      0x47c593
0x47C58F: mov      byte ptr [ebp - 0x29], 0
0x47C593: cmp      byte ptr [ebp - 0x29], 0
0x47C597: jne      0x47c5b0
0x47C599: call     0x40b5fc
0x47C59E: mov      eax, dword ptr [ebp - 4]
0x47C5A1: mov      eax, dword ptr [eax + 0x20]
0x47C5A4: mov      edx, dword ptr [eax]
0x47C5A6: call     dword ptr [edx + 0xb4]
0x47C5AC: mov      byte ptr [ebp - 5], 0
0x47C5B0: xor      eax, eax
0x47C5B2: pop      edx
0x47C5B3: pop      ecx
0x47C5B4: pop      ecx
0x47C5B5: mov      dword ptr fs:[eax], edx
0x47C5B8: push     0x47c5e7
0x47C5BD: lea      eax, [ebp - 0x3c]
0x47C5C0: call     0x403a58
0x47C5C5: lea      eax, [ebp - 0x34]
0x47C5C8: mov      edx, 2
0x47C5CD: call     0x403a7c
0x47C5D2: lea      eax, [ebp - 0x1c]
0x47C5D5: mov      edx, 5
0x47C5DA: call     0x403a7c
0x47C5DF: ret      
0x47C5E0: jmp      0x403518
0x47C5E5: jmp      0x47c5bd
0x47C5E7: mov      al, byte ptr [ebp - 5]
0x47C5EA: mov      esp, ebp
0x47C5EC: pop      ebp
0x47C5ED: ret      
0x47C5EE: mov      eax, eax
0x47C5F0: push     ebp
0x47C5F1: mov      ebp, esp
0x47C5F3: add      esp, -0x40
0x47C5F6: xor      edx, edx
0x47C5F8: mov      dword ptr [ebp - 0x3c], edx
0x47C5FB: mov      dword ptr [ebp - 0xc], edx
0x47C5FE: mov      dword ptr [ebp - 0x2c], edx
0x47C601: mov      dword ptr [ebp - 0x30], edx
0x47C604: mov      dword ptr [ebp - 0x34], edx
0x47C607: mov      dword ptr [ebp - 0x10], edx
0x47C60A: mov      dword ptr [ebp - 0x14], edx
0x47C60D: mov      dword ptr [ebp - 0x18], edx
0x47C610: mov      dword ptr [ebp - 4], eax
0x47C613: xor      eax, eax
0x47C615: push     ebp
0x47C616: push     0x47c74a
0x47C61B: push     dword ptr fs:[eax]
0x47C61E: mov      dword ptr fs:[eax], esp
0x47C621: mov      byte ptr [ebp - 5], 1
0x47C625: mov      byte ptr [ebp - 0x25], 1
0x47C629: lea      edx, [ebp - 0xc]
0x47C62C: mov      eax, dword ptr [ebp - 4]
0x47C62F: mov      eax, dword ptr [eax + 0x24]
0x47C632: call     0x4279d0
0x47C637: push     0
0x47C639: lea      eax, [ebp - 0x3c]
0x47C63C: push     eax
0x47C63D: mov      byte ptr [ebp - 0x40], 0x2f
0x47C641: lea      ecx, [ebp - 0x40]
0x47C644: lea      edx, [ebp - 0x10]
0x47C647: mov      eax, dword ptr [ebp - 0xc]
0x47C64A: call     0x452530
0x47C64F: mov      edx, dword ptr [ebp - 0x3c]
0x47C652: lea      eax, [ebp - 0xc]
0x47C655: call     0x403af0
0x47C65A: push     0
0x47C65C: lea      eax, [ebp - 0x3c]
0x47C65F: push     eax
0x47C660: mov      byte ptr [ebp - 0x40], 0x2f
0x47C664: lea      ecx, [ebp - 0x40]
0x47C667: lea      edx, [ebp - 0x14]
0x47C66A: mov      eax, dword ptr [ebp - 0xc]
0x47C66D: call     0x452530
0x47C672: mov      edx, dword ptr [ebp - 0x3c]
0x47C675: lea      eax, [ebp - 0xc]
0x47C678: call     0x403af0
0x47C67D: push     0
0x47C67F: lea      eax, [ebp - 0x3c]
0x47C682: push     eax
0x47C683: mov      byte ptr [ebp - 0x40], 0x2f
0x47C687: lea      ecx, [ebp - 0x40]
0x47C68A: lea      edx, [ebp - 0x18]
0x47C68D: mov      eax, dword ptr [ebp - 0xc]
0x47C690: call     0x452530
0x47C695: mov      edx, dword ptr [ebp - 0x3c]
0x47C698: lea      eax, [ebp - 0xc]
0x47C69B: call     0x403af0
0x47C6A0: xor      edx, edx
0x47C6A2: mov      eax, dword ptr [ebp - 0x10]
0x47C6A5: call     0x4520e8
0x47C6AA: mov      dword ptr [ebp - 0x1c], eax
0x47C6AD: xor      edx, edx
0x47C6AF: mov      eax, dword ptr [ebp - 0x14]
0x47C6B2: call     0x4520e8
0x47C6B7: mov      dword ptr [ebp - 0x20], eax
0x47C6BA: xor      edx, edx
0x47C6BC: mov      eax, dword ptr [ebp - 0x18]
0x47C6BF: call     0x4520e8
0x47C6C4: mov      dword ptr [ebp - 0x24], eax
0x47C6C7: cmp      dword ptr [ebp - 0x1c], 0x762
0x47C6CE: jle      0x47c6d9
0x47C6D0: cmp      dword ptr [ebp - 0x1c], 0x835
0x47C6D7: jle      0x47c6dd
0x47C6D9: mov      byte ptr [ebp - 0x25], 0
0x47C6DD: cmp      dword ptr [ebp - 0x20], 0
0x47C6E1: jle      0x47c6e9
0x47C6E3: cmp      dword ptr [ebp - 0x20], 0xc
0x47C6E7: jle      0x47c6ed
0x47C6E9: mov      byte ptr [ebp - 0x25], 0
0x47C6ED: cmp      dword ptr [ebp - 0x24], 0
0x47C6F1: jle      0x47c6f9
0x47C6F3: cmp      dword ptr [ebp - 0x24], 0x1f
0x47C6F7: jle      0x47c6fd
0x47C6F9: mov      byte ptr [ebp - 0x25], 0
0x47C6FD: cmp      byte ptr [ebp - 0x25], 0
0x47C701: jne      0x47c71a
0x47C703: call     0x40b5fc
0x47C708: mov      eax, dword ptr [ebp - 4]
0x47C70B: mov      eax, dword ptr [eax + 0x24]
0x47C70E: mov      edx, dword ptr [eax]
0x47C710: call     dword ptr [edx + 0xb4]
0x47C716: mov      byte ptr [ebp - 5], 0
0x47C71A: xor      eax, eax
0x47C71C: pop      edx
0x47C71D: pop      ecx
0x47C71E: pop      ecx
0x47C71F: mov      dword ptr fs:[eax], edx
0x47C722: push     0x47c751
0x47C727: lea      eax, [ebp - 0x3c]
0x47C72A: call     0x403a58
0x47C72F: lea      eax, [ebp - 0x34]
0x47C732: mov      edx, 3
0x47C737: call     0x403a7c
0x47C73C: lea      eax, [ebp - 0x18]
0x47C73F: mov      edx, 4
0x47C744: call     0x403a7c
0x47C749: ret      
0x47C74A: jmp      0x403518
0x47C74F: jmp      0x47c727
0x47C751: mov      al, byte ptr [ebp - 5]
0x47C754: mov      esp, ebp
0x47C756: pop      ebp
0x47C757: ret      
0x47C758: push     ebp
0x47C759: mov      ebp, esp
0x47C75B: push     ecx
0x47C75C: mov      ecx, 8
0x47C761: push     0
0x47C763: push     0
0x47C765: dec      ecx
0x47C766: jne      0x47c761
0x47C768: push     ecx
0x47C769: xchg     dword ptr [ebp - 4], ecx
0x47C76C: push     ebx
0x47C76D: mov      dword ptr [ebp - 0xc], ecx
0x47C770: mov      dword ptr [ebp - 8], edx
0x47C773: mov      dword ptr [ebp - 4], eax
0x47C776: xor      eax, eax
0x47C778: push     ebp
0x47C779: push     0x47cb69
0x47C77E: push     dword ptr fs:[eax]
0x47C781: mov      dword ptr fs:[eax], esp
0x47C784: mov      eax, dword ptr [ebp - 8]
0x47C787: mov      edx, dword ptr [ebp - 4]
0x47C78A: cmp      eax, dword ptr [edx + 0x14]
0x47C78D: je       0x47c7a5
0x47C78F: mov      eax, dword ptr [ebp - 8]
0x47C792: mov      edx, dword ptr [ebp - 4]
0x47C795: cmp      eax, dword ptr [edx + 0x4c]
0x47C798: je       0x47c7a5
0x47C79A: mov      eax, dword ptr [ebp - 8]
0x47C79D: mov      edx, dword ptr [ebp - 4]
0x47C7A0: cmp      eax, dword ptr [edx + 0x50]
0x47C7A3: jne      0x47c7c3
0x47C7A5: mov      eax, dword ptr [ebp - 0xc]
0x47C7A8: cmp      byte ptr [eax], 0x7e
0x47C7AB: je       0x47c7bd
0x47C7AD: mov      eax, dword ptr [ebp - 0xc]
0x47C7B0: cmp      byte ptr [eax], 0x27
0x47C7B3: je       0x47c7bd
0x47C7B5: mov      eax, dword ptr [ebp - 0xc]
0x47C7B8: cmp      byte ptr [eax], 0x20
0x47C7BB: jne      0x47c7c3
0x47C7BD: mov      eax, dword ptr [ebp - 0xc]
0x47C7C0: mov      byte ptr [eax], 0
0x47C7C3: mov      eax, dword ptr [ebp - 0xc]
0x47C7C6: cmp      byte ptr [eax], 0xd
0x47C7C9: jne      0x47cb39
0x47C7CF: mov      eax, dword ptr [ebp - 0xc]
0x47C7D2: mov      byte ptr [eax], 0
0x47C7D5: mov      eax, dword ptr [ebp - 8]
0x47C7D8: mov      edx, dword ptr [ebp - 4]
0x47C7DB: cmp      eax, dword ptr [edx + 0x10]
0x47C7DE: jne      0x47c7f0
0x47C7E0: mov      eax, dword ptr [ebp - 4]
0x47C7E3: call     0x47c3a8
0x47C7E8: test     al, al
0x47C7EA: je       0x47cb39
0x47C7F0: mov      eax, dword ptr [ebp - 8]
0x47C7F3: mov      edx, dword ptr [ebp - 4]
0x47C7F6: cmp      eax, dword ptr [edx + 0x14]
0x47C7F9: jne      0x47c82e
0x47C7FB: lea      edx, [ebp - 0x40]
0x47C7FE: mov      eax, dword ptr [ebp - 4]
0x47C801: mov      eax, dword ptr [eax + 0x14]
0x47C804: call     0x4279d0
0x47C809: mov      eax, dword ptr [ebp - 0x40]
0x47C80C: call     0x403cd4
0x47C811: cmp      eax, 4
0x47C814: jge      0x47c82e
0x47C816: call     0x40b5fc
0x47C81B: mov      eax, dword ptr [ebp - 4]
0x47C81E: mov      eax, dword ptr [eax + 0x14]
0x47C821: mov      edx, dword ptr [eax]
0x47C823: call     dword ptr [edx + 0xb4]
0x47C829: jmp      0x47cb39
0x47C82E: mov      eax, dword ptr [ebp - 8]
0x47C831: mov      edx, dword ptr [ebp - 4]
0x47C834: cmp      eax, dword ptr [edx + 0x18]
0x47C837: jne      0x47c87c
0x47C839: lea      edx, [ebp - 0x40]
0x47C83C: mov      eax, dword ptr [ebp - 4]
0x47C83F: mov      eax, dword ptr [eax + 0x14]
0x47C842: call     0x4279d0
0x47C847: mov      eax, dword ptr [ebp - 0x40]
0x47C84A: push     eax
0x47C84B: lea      edx, [ebp - 0x44]
0x47C84E: mov      eax, dword ptr [ebp - 4]
0x47C851: mov      eax, dword ptr [eax + 0x18]
0x47C854: call     0x4279d0
0x47C859: mov      edx, dword ptr [ebp - 0x44]
0x47C85C: pop      eax
0x47C85D: call     0x403de4
0x47C862: je       0x47c87c
0x47C864: call     0x40b5fc
0x47C869: mov      eax, dword ptr [ebp - 4]
0x47C86C: mov      eax, dword ptr [eax + 0x18]
0x47C86F: mov      edx, dword ptr [eax]
0x47C871: call     dword ptr [edx + 0xb4]
0x47C877: jmp      0x47cb39
0x47C87C: mov      eax, dword ptr [ebp - 8]
0x47C87F: mov      edx, dword ptr [ebp - 4]
0x47C882: cmp      eax, dword ptr [edx + 0x1c]
0x47C885: je       0x47c8d4
0x47C887: mov      eax, dword ptr [ebp - 8]
0x47C88A: mov      edx, dword ptr [ebp - 4]
0x47C88D: cmp      eax, dword ptr [edx + 0x28]
0x47C890: je       0x47c8d4
0x47C892: mov      eax, dword ptr [ebp - 8]
0x47C895: mov      edx, dword ptr [ebp - 4]
0x47C898: cmp      eax, dword ptr [edx + 0x2c]
0x47C89B: je       0x47c8d4
0x47C89D: mov      eax, dword ptr [ebp - 8]
0x47C8A0: mov      edx, dword ptr [ebp - 4]
0x47C8A3: cmp      eax, dword ptr [edx + 0x30]
0x47C8A6: je       0x47c8d4
0x47C8A8: mov      eax, dword ptr [ebp - 8]
0x47C8AB: mov      edx, dword ptr [ebp - 4]
0x47C8AE: cmp      eax, dword ptr [edx + 0x34]
0x47C8B1: je       0x47c8d4
0x47C8B3: mov      eax, dword ptr [ebp - 8]
0x47C8B6: mov      edx, dword ptr [ebp - 4]
0x47C8B9: cmp      eax, dword ptr [edx + 0x38]
0x47C8BC: je       0x47c8d4
0x47C8BE: mov      eax, dword ptr [ebp - 8]
0x47C8C1: mov      edx, dword ptr [ebp - 4]
0x47C8C4: cmp      eax, dword ptr [edx + 0x3c]
0x47C8C7: je       0x47c8d4
0x47C8C9: mov      eax, dword ptr [ebp - 8]
0x47C8CC: mov      edx, dword ptr [ebp - 4]
0x47C8CF: cmp      eax, dword ptr [edx + 0x40]
0x47C8D2: jne      0x47c91b
0x47C8D4: lea      edx, [ebp - 0x40]
0x47C8D7: mov      eax, dword ptr [ebp - 8]
0x47C8DA: call     0x4279d0
0x47C8DF: mov      eax, dword ptr [ebp - 0x40]
0x47C8E2: lea      edx, [ebp - 0x48]
0x47C8E5: call     0x407be4
0x47C8EA: mov      edx, dword ptr [ebp - 0x48]
0x47C8ED: mov      eax, dword ptr [ebp - 8]
0x47C8F0: call     0x427a00
0x47C8F5: lea      edx, [ebp - 0x40]
0x47C8F8: mov      eax, dword ptr [ebp - 8]
0x47C8FB: call     0x4279d0
0x47C900: cmp      dword ptr [ebp - 0x40], 0
0x47C904: jne      0x47c91b
0x47C906: call     0x40b5fc
0x47C90B: mov      eax, dword ptr [ebp - 8]
0x47C90E: mov      edx, dword ptr [eax]
0x47C910: call     dword ptr [edx + 0xb4]
0x47C916: jmp      0x47cb39
0x47C91B: mov      eax, dword ptr [ebp - 8]
0x47C91E: mov      edx, dword ptr [ebp - 4]
0x47C921: cmp      eax, dword ptr [edx + 0x20]
0x47C924: jne      0x47c932
0x47C926: jmp      0x47c932
0x47C928: mov      eax, dword ptr [ebp - 4]
0x47C92B: call     0x47c450
0x47C930: test     al, al
0x47C932: mov      eax, dword ptr [ebp - 8]
0x47C935: mov      edx, dword ptr [ebp - 4]
0x47C938: cmp      eax, dword ptr [edx + 0x24]
0x47C93B: jne      0x47c94d
0x47C93D: mov      eax, dword ptr [ebp - 4]
0x47C940: call     0x47c5f0
0x47C945: test     al, al
0x47C947: je       0x47cb39
0x47C94D: lea      edx, [ebp - 0x40]
0x47C950: mov      eax, dword ptr [ebp - 8]
0x47C953: call     0x4279d0
0x47C958: cmp      dword ptr [ebp - 0x40], 0
0x47C95C: je       0x47cb39
0x47C962: mov      eax, dword ptr [ebp - 8]
0x47C965: mov      edx, dword ptr [ebp - 4]
0x47C968: cmp      eax, dword ptr [edx + 0x10]
0x47C96B: jne      0x47c97b
0x47C96D: mov      eax, dword ptr [ebp - 4]
0x47C970: mov      eax, dword ptr [eax + 0x14]
0x47C973: mov      edx, dword ptr [eax]
0x47C975: call     dword ptr [edx + 0xb4]
0x47C97B: mov      eax, dword ptr [ebp - 8]
0x47C97E: mov      edx, dword ptr [ebp - 4]
0x47C981: cmp      eax, dword ptr [edx + 0x14]
0x47C984: jne      0x47c994
0x47C986: mov      eax, dword ptr [ebp - 4]
0x47C989: mov      eax, dword ptr [eax + 0x18]
0x47C98C: mov      edx, dword ptr [eax]
0x47C98E: call     dword ptr [edx + 0xb4]
0x47C994: mov      eax, dword ptr [ebp - 8]
0x47C997: mov      edx, dword ptr [ebp - 4]
0x47C99A: cmp      eax, dword ptr [edx + 0x18]
0x47C99D: jne      0x47c9ad
0x47C99F: mov      eax, dword ptr [ebp - 4]
0x47C9A2: mov      eax, dword ptr [eax + 0x1c]
0x47C9A5: mov      edx, dword ptr [eax]
0x47C9A7: call     dword ptr [edx + 0xb4]
0x47C9AD: mov      eax, dword ptr [ebp - 8]
0x47C9B0: mov      edx, dword ptr [ebp - 4]
0x47C9B3: cmp      eax, dword ptr [edx + 0x1c]
0x47C9B6: jne      0x47c9c6
0x47C9B8: mov      eax, dword ptr [ebp - 4]
0x47C9BB: mov      eax, dword ptr [eax + 0x20]
0x47C9BE: mov      edx, dword ptr [eax]
0x47C9C0: call     dword ptr [edx + 0xb4]
0x47C9C6: mov      eax, dword ptr [ebp - 8]
0x47C9C9: mov      edx, dword ptr [ebp - 4]
0x47C9CC: cmp      eax, dword ptr [edx + 0x20]
0x47C9CF: jne      0x47c9df
0x47C9D1: mov      eax, dword ptr [ebp - 4]
0x47C9D4: mov      eax, dword ptr [eax + 0x24]
0x47C9D7: mov      edx, dword ptr [eax]
0x47C9D9: call     dword ptr [edx + 0xb4]
0x47C9DF: mov      eax, dword ptr [ebp - 8]
0x47C9E2: mov      edx, dword ptr [ebp - 4]
0x47C9E5: cmp      eax, dword ptr [edx + 0x24]
0x47C9E8: jne      0x47c9f8
0x47C9EA: mov      eax, dword ptr [ebp - 4]
0x47C9ED: mov      eax, dword ptr [eax + 0x28]
0x47C9F0: mov      edx, dword ptr [eax]
0x47C9F2: call     dword ptr [edx + 0xb4]
0x47C9F8: mov      eax, dword ptr [ebp - 8]
0x47C9FB: mov      edx, dword ptr [ebp - 4]
0x47C9FE: cmp      eax, dword ptr [edx + 0x28]
0x47CA01: jne      0x47ca11
0x47CA03: mov      eax, dword ptr [ebp - 4]
0x47CA06: mov      eax, dword ptr [eax + 0x2c]
0x47CA09: mov      edx, dword ptr [eax]
0x47CA0B: call     dword ptr [edx + 0xb4]
0x47CA11: mov      eax, dword ptr [ebp - 8]
0x47CA14: mov      edx, dword ptr [ebp - 4]
0x47CA17: cmp      eax, dword ptr [edx + 0x2c]
0x47CA1A: jne      0x47ca2a
0x47CA1C: mov      eax, dword ptr [ebp - 4]
0x47CA1F: mov      eax, dword ptr [eax + 0x30]
0x47CA22: mov      edx, dword ptr [eax]
0x47CA24: call     dword ptr [edx + 0xb4]
0x47CA2A: mov      eax, dword ptr [ebp - 8]
0x47CA2D: mov      edx, dword ptr [ebp - 4]
0x47CA30: cmp      eax, dword ptr [edx + 0x30]
0x47CA33: jne      0x47ca43
0x47CA35: mov      eax, dword ptr [ebp - 4]
0x47CA38: mov      eax, dword ptr [eax + 0x34]
0x47CA3B: mov      edx, dword ptr [eax]
0x47CA3D: call     dword ptr [edx + 0xb4]
0x47CA43: mov      eax, dword ptr [ebp - 8]
0x47CA46: mov      edx, dword ptr [ebp - 4]
0x47CA49: cmp      eax, dword ptr [edx + 0x34]
0x47CA4C: jne      0x47ca5c
0x47CA4E: mov      eax, dword ptr [ebp - 4]
0x47CA51: mov      eax, dword ptr [eax + 0x38]
0x47CA54: mov      edx, dword ptr [eax]
0x47CA56: call     dword ptr [edx + 0xb4]
0x47CA5C: mov      eax, dword ptr [ebp - 8]
0x47CA5F: mov      edx, dword ptr [ebp - 4]
0x47CA62: cmp      eax, dword ptr [edx + 0x38]
0x47CA65: jne      0x47ca75
0x47CA67: mov      eax, dword ptr [ebp - 4]
0x47CA6A: mov      eax, dword ptr [eax + 0x3c]
0x47CA6D: mov      edx, dword ptr [eax]
0x47CA6F: call     dword ptr [edx + 0xb4]
0x47CA75: mov      eax, dword ptr [ebp - 8]
0x47CA78: mov      edx, dword ptr [ebp - 4]
0x47CA7B: cmp      eax, dword ptr [edx + 0x3c]
0x47CA7E: jne      0x47ca8e
0x47CA80: mov      eax, dword ptr [ebp - 4]
0x47CA83: mov      eax, dword ptr [eax + 0x40]
0x47CA86: mov      edx, dword ptr [eax]
0x47CA88: call     dword ptr [edx + 0xb4]
0x47CA8E: mov      eax, dword ptr [ebp - 8]
0x47CA91: mov      edx, dword ptr [ebp - 4]
0x47CA94: cmp      eax, dword ptr [edx + 0x40]
0x47CA97: jne      0x47cad5
0x47CA99: mov      eax, dword ptr [ebp - 4]
0x47CA9C: mov      eax, dword ptr [eax + 0x10]
0x47CA9F: mov      edx, dword ptr [eax]
0x47CAA1: call     dword ptr [edx + 0x50]
0x47CAA4: test     al, al
0x47CAA6: je       0x47cab8
0x47CAA8: mov      eax, dword ptr [ebp - 4]
0x47CAAB: mov      eax, dword ptr [eax + 0x10]
0x47CAAE: mov      edx, dword ptr [eax]
0x47CAB0: call     dword ptr [edx + 0xb4]
0x47CAB6: jmp      0x47cad5
0x47CAB8: mov      eax, dword ptr [ebp - 4]
0x47CABB: mov      eax, dword ptr [eax + 0x14]
0x47CABE: mov      edx, dword ptr [eax]
0x47CAC0: call     dword ptr [edx + 0x50]
0x47CAC3: test     al, al
0x47CAC5: je       0x47cad5
0x47CAC7: mov      eax, dword ptr [ebp - 4]
0x47CACA: mov      eax, dword ptr [eax + 0x14]
0x47CACD: mov      edx, dword ptr [eax]
0x47CACF: call     dword ptr [edx + 0xb4]
0x47CAD5: mov      eax, dword ptr [ebp - 8]
0x47CAD8: mov      edx, dword ptr [ebp - 4]
0x47CADB: cmp      eax, dword ptr [edx + 0x44]
0x47CADE: jne      0x47caee
0x47CAE0: mov      eax, dword ptr [ebp - 4]
0x47CAE3: mov      eax, dword ptr [eax + 0x48]
0x47CAE6: mov      edx, dword ptr [eax]
0x47CAE8: call     dword ptr [edx + 0xb4]
0x47CAEE: mov      eax, dword ptr [ebp - 8]
0x47CAF1: mov      edx, dword ptr [ebp - 4]
0x47CAF4: cmp      eax, dword ptr [edx + 0x48]
0x47CAF7: jne      0x47cb07
0x47CAF9: mov      eax, dword ptr [ebp - 4]
0x47CAFC: mov      eax, dword ptr [eax + 0x4c]
0x47CAFF: mov      edx, dword ptr [eax]
0x47CB01: call     dword ptr [edx + 0xb4]
0x47CB07: mov      eax, dword ptr [ebp - 8]
0x47CB0A: mov      edx, dword ptr [ebp - 4]
0x47CB0D: cmp      eax, dword ptr [edx + 0x4c]
0x47CB10: jne      0x47cb20
0x47CB12: mov      eax, dword ptr [ebp - 4]
0x47CB15: mov      eax, dword ptr [eax + 0x50]
0x47CB18: mov      edx, dword ptr [eax]
0x47CB1A: call     dword ptr [edx + 0xb4]
0x47CB20: mov      eax, dword ptr [ebp - 8]
0x47CB23: mov      edx, dword ptr [ebp - 4]
0x47CB26: cmp      eax, dword ptr [edx + 0x50]
0x47CB29: jne      0x47cb39
0x47CB2B: mov      eax, dword ptr [ebp - 4]
0x47CB2E: mov      eax, dword ptr [eax + 0x44]
0x47CB31: mov      edx, dword ptr [eax]
0x47CB33: call     dword ptr [edx + 0xb4]
0x47CB39: xor      eax, eax
0x47CB3B: pop      edx
0x47CB3C: pop      ecx
0x47CB3D: pop      ecx
0x47CB3E: mov      dword ptr fs:[eax], edx
0x47CB41: push     0x47cb70
0x47CB46: lea      eax, [ebp - 0x48]
0x47CB49: call     0x403a58
0x47CB4E: lea      eax, [ebp - 0x44]
0x47CB51: mov      edx, 2
0x47CB56: call     0x403a7c
0x47CB5B: lea      eax, [ebp - 0x28]
0x47CB5E: mov      edx, 7
0x47CB63: call     0x403a7c
0x47CB68: ret      
0x47CB69: jmp      0x403518
0x47CB6E: jmp      0x47cb46
0x47CB70: pop      ebx
0x47CB71: mov      esp, ebp
0x47CB73: pop      ebp
0x47CB74: ret      
0x47CB75: lea      eax, [eax]
0x47CB78: push     ebp
0x47CB79: mov      ebp, esp
0x47CB7B: add      esp, -0x10
0x47CB7E: mov      dword ptr [ebp - 8], edx
0x47CB81: mov      dword ptr [ebp - 4], eax
0x47CB84: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CB89: mov      eax, dword ptr [eax]
0x47CB8B: mov      eax, dword ptr [eax + 0x77c]
0x47CB91: mov      edx, dword ptr [eax]
0x47CB93: call     dword ptr [edx + 0x40]
0x47CB96: mov      eax, dword ptr [ebp - 8]
0x47CB99: mov      eax, dword ptr [eax + 0x30]
0x47CB9C: mov      edx, dword ptr [ebp - 8]
0x47CB9F: add      eax, dword ptr [edx + 0x38]
0x47CBA2: add      eax, 0xa
0x47CBA5: mov      dword ptr [ebp - 0xc], eax
0x47CBA8: mov      eax, dword ptr [ebp - 8]
0x47CBAB: mov      eax, dword ptr [eax + 0x34]
0x47CBAE: mov      edx, dword ptr [ebp - 8]
0x47CBB1: add      eax, dword ptr [edx + 0x3c]
0x47CBB4: sub      eax, 0x12
0x47CBB7: mov      dword ptr [ebp - 0x10], eax
0x47CBBA: mov      eax, dword ptr [ebp - 8]
0x47CBBD: mov      edx, dword ptr [ebp - 4]
0x47CBC0: cmp      eax, dword ptr [edx + 0x10]
0x47CBC3: jne      0x47ccc3
0x47CBC9: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CBCE: mov      eax, dword ptr [eax]
0x47CBD0: mov      eax, dword ptr [eax + 0x77c]
0x47CBD6: mov      edx, 0x47d188 ; "你的ID可以是以下内容的组合："
0x47CBDB: mov      ecx, dword ptr [eax]
0x47CBDD: call     dword ptr [ecx + 0x34]
0x47CBE0: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CBE5: mov      eax, dword ptr [eax]
0x47CBE7: mov      eax, dword ptr [eax + 0x77c]
0x47CBED: mov      edx, 0x47d1b0 ; "字符、数字"
0x47CBF2: mov      ecx, dword ptr [eax]
0x47CBF4: call     dword ptr [ecx + 0x34]
0x47CBF7: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CBFC: mov      eax, dword ptr [eax]
0x47CBFE: mov      eax, dword ptr [eax + 0x77c]
0x47CC04: mov      edx, 0x47d1c4 ; "ID必须至少有4位。"
0x47CC09: mov      ecx, dword ptr [eax]
0x47CC0B: call     dword ptr [ecx + 0x34]
0x47CC0E: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CC13: mov      eax, dword ptr [eax]
0x47CC15: mov      eax, dword ptr [eax + 0x77c]
0x47CC1B: mov      edx, 0x47d1e0 ; "你输入的名字是你游戏中角色的名字"
0x47CC20: mov      ecx, dword ptr [eax]
0x47CC22: call     dword ptr [ecx + 0x34]
0x47CC25: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CC2A: mov      eax, dword ptr [eax]
0x47CC2C: mov      eax, dword ptr [eax + 0x77c]
0x47CC32: mov      edx, 0x47d20c ; "请仔细选择你的ID,"
0x47CC37: mov      ecx, dword ptr [eax]
0x47CC39: call     dword ptr [ecx + 0x34]
0x47CC3C: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CC41: mov      eax, dword ptr [eax]
0x47CC43: mov      eax, dword ptr [eax + 0x77c]
0x47CC49: mov      edx, 0x47d228 ; "你的登陆名可以"
0x47CC4E: mov      ecx, dword ptr [eax]
0x47CC50: call     dword ptr [ecx + 0x34]
0x47CC53: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CC58: mov      eax, dword ptr [eax]
0x47CC5A: mov      eax, dword ptr [eax + 0x77c]
0x47CC60: mov      edx, 0x47d240 ; " 用于我们所有服务器。"
0x47CC65: mov      ecx, dword ptr [eax]
0x47CC67: call     dword ptr [ecx + 0x34]
0x47CC6A: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CC6F: mov      eax, dword ptr [eax]
0x47CC71: mov      eax, dword ptr [eax + 0x77c]
0x47CC77: xor      edx, edx
0x47CC79: mov      ecx, dword ptr [eax]
0x47CC7B: call     dword ptr [ecx + 0x34]
0x47CC7E: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CC83: mov      eax, dword ptr [eax]
0x47CC85: mov      eax, dword ptr [eax + 0x77c]
0x47CC8B: mov      edx, 0x47d260 ; "我们建议你用一个不同的"
0x47CC90: mov      ecx, dword ptr [eax]
0x47CC92: call     dword ptr [ecx + 0x34]
0x47CC95: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CC9A: mov      eax, dword ptr [eax]
0x47CC9C: mov      eax, dword ptr [eax + 0x77c]
0x47CCA2: mov      edx, 0x47d280 ; "名字，和你想给游戏角色用"
0x47CCA7: mov      ecx, dword ptr [eax]
0x47CCA9: call     dword ptr [ecx + 0x34]
0x47CCAC: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CCB1: mov      eax, dword ptr [eax]
0x47CCB3: mov      eax, dword ptr [eax + 0x77c]
0x47CCB9: mov      edx, 0x47d2a4 ; "的那个区别开来。"
0x47CCBE: mov      ecx, dword ptr [eax]
0x47CCC0: call     dword ptr [ecx + 0x34]
0x47CCC3: mov      eax, dword ptr [ebp - 8]
0x47CCC6: mov      edx, dword ptr [ebp - 4]
0x47CCC9: cmp      eax, dword ptr [edx + 0x14]
0x47CCCC: jne      0x47cdcf
0x47CCD2: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CCD7: mov      eax, dword ptr [eax]
0x47CCD9: mov      eax, dword ptr [eax + 0x77c]
0x47CCDF: mov      edx, 0x47d2c0 ; "你的密码可以是一个"
0x47CCE4: mov      ecx, dword ptr [eax]
0x47CCE6: call     dword ptr [ecx + 0x34]
0x47CCE9: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CCEE: mov      eax, dword ptr [eax]
0x47CCF0: mov      eax, dword ptr [eax + 0x77c]
0x47CCF6: mov      edx, 0x47d2dc ; "组合，包括：字符"
0x47CCFB: mov      ecx, dword ptr [eax]
0x47CCFD: call     dword ptr [ecx + 0x34]
0x47CD00: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CD05: mov      eax, dword ptr [eax]
0x47CD07: mov      eax, dword ptr [eax + 0x77c]
0x47CD0D: mov      edx, 0x47d2f8 ; "和数字。而且"
0x47CD12: mov      ecx, dword ptr [eax]
0x47CD14: call     dword ptr [ecx + 0x34]
0x47CD17: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CD1C: mov      eax, dword ptr [eax]
0x47CD1E: mov      eax, dword ptr [eax + 0x77c]
0x47CD24: mov      edx, 0x47d310 ; "它至少要有4位。"
0x47CD29: mov      ecx, dword ptr [eax]
0x47CD2B: call     dword ptr [ecx + 0x34]
0x47CD2E: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CD33: mov      eax, dword ptr [eax]
0x47CD35: mov      eax, dword ptr [eax + 0x77c]
0x47CD3B: mov      edx, 0x47d328 ; "把你的密码记住是"
0x47CD40: mov      ecx, dword ptr [eax]
0x47CD42: call     dword ptr [ecx + 0x34]
0x47CD45: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CD4A: mov      eax, dword ptr [eax]
0x47CD4C: mov      eax, dword ptr [eax + 0x77c]
0x47CD52: mov      edx, 0x47d344 ; "玩我们的游戏最基本的要素,"
0x47CD57: mov      ecx, dword ptr [eax]
0x47CD59: call     dword ptr [ecx + 0x34]
0x47CD5C: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CD61: mov      eax, dword ptr [eax]
0x47CD63: mov      eax, dword ptr [eax + 0x77c]
0x47CD69: mov      edx, 0x47d368 ; "所以请确认你已经记好了它。"
0x47CD6E: mov      ecx, dword ptr [eax]
0x47CD70: call     dword ptr [ecx + 0x34]
0x47CD73: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CD78: mov      eax, dword ptr [eax]
0x47CD7A: mov      eax, dword ptr [eax + 0x77c]
0x47CD80: mov      edx, 0x47d38c ; "我们建议你最好不要用"
0x47CD85: mov      ecx, dword ptr [eax]
0x47CD87: call     dword ptr [ecx + 0x34]
0x47CD8A: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CD8F: mov      eax, dword ptr [eax]
0x47CD91: mov      eax, dword ptr [eax + 0x77c]
0x47CD97: mov      edx, 0x47d3ac ; "一个简单的密码"
0x47CD9C: mov      ecx, dword ptr [eax]
0x47CD9E: call     dword ptr [ecx + 0x34]
0x47CDA1: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CDA6: mov      eax, dword ptr [eax]
0x47CDA8: mov      eax, dword ptr [eax + 0x77c]
0x47CDAE: mov      edx, 0x47d3c4 ; "以消除一些"
0x47CDB3: mov      ecx, dword ptr [eax]
0x47CDB5: call     dword ptr [ecx + 0x34]
0x47CDB8: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CDBD: mov      eax, dword ptr [eax]
0x47CDBF: mov      eax, dword ptr [eax + 0x77c]
0x47CDC5: mov      edx, 0x47d3d8 ; "不安全因素"
0x47CDCA: mov      ecx, dword ptr [eax]
0x47CDCC: call     dword ptr [ecx + 0x34]
0x47CDCF: mov      eax, dword ptr [ebp - 8]
0x47CDD2: mov      edx, dword ptr [ebp - 4]
0x47CDD5: cmp      eax, dword ptr [edx + 0x18]
0x47CDD8: jne      0x47ce08
0x47CDDA: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CDDF: mov      eax, dword ptr [eax]
0x47CDE1: mov      eax, dword ptr [eax + 0x77c]
0x47CDE7: mov      edx, 0x47d3ec ; "再次输入密码"
0x47CDEC: mov      ecx, dword ptr [eax]
0x47CDEE: call     dword ptr [ecx + 0x34]
0x47CDF1: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CDF6: mov      eax, dword ptr [eax]
0x47CDF8: mov      eax, dword ptr [eax + 0x77c]
0x47CDFE: mov      edx, 0x47d404 ; "以确认"
0x47CE03: mov      ecx, dword ptr [eax]
0x47CE05: call     dword ptr [ecx + 0x34]
0x47CE08: mov      eax, dword ptr [ebp - 8]
0x47CE0B: mov      edx, dword ptr [ebp - 4]
0x47CE0E: cmp      eax, dword ptr [edx + 0x1c]
0x47CE11: jne      0x47ce2a
0x47CE13: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CE18: mov      eax, dword ptr [eax]
0x47CE1A: mov      eax, dword ptr [eax + 0x77c]
0x47CE20: mov      edx, 0x47d414 ; "键入你的全名."
0x47CE25: mov      ecx, dword ptr [eax]
0x47CE27: call     dword ptr [ecx + 0x34]
0x47CE2A: mov      eax, dword ptr [ebp - 8]
0x47CE2D: mov      edx, dword ptr [ebp - 4]
0x47CE30: cmp      eax, dword ptr [edx + 0x20]
0x47CE33: jne      0x47ce63
0x47CE35: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CE3A: mov      eax, dword ptr [eax]
0x47CE3C: mov      eax, dword ptr [eax + 0x77c]
0x47CE42: mov      edx, 0x47d42c ; "不用"
0x47CE47: mov      ecx, dword ptr [eax]
0x47CE49: call     dword ptr [ecx + 0x34]
0x47CE4C: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CE51: mov      eax, dword ptr [eax]
0x47CE53: mov      eax, dword ptr [eax + 0x77c]
0x47CE59: mov      edx, 0x47d43c ; "在这次."
0x47CE5E: mov      ecx, dword ptr [eax]
0x47CE60: call     dword ptr [ecx + 0x34]
0x47CE63: mov      eax, dword ptr [ebp - 8]
0x47CE66: mov      edx, dword ptr [ebp - 4]
0x47CE69: cmp      eax, dword ptr [edx + 0x24]
0x47CE6C: jne      0x47ce9c
0x47CE6E: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CE73: mov      eax, dword ptr [eax]
0x47CE75: mov      eax, dword ptr [eax + 0x77c]
0x47CE7B: mov      edx, 0x47d44c ; "请键入你的出生日期和月份,"
0x47CE80: mov      ecx, dword ptr [eax]
0x47CE82: call     dword ptr [ecx + 0x34]
0x47CE85: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CE8A: mov      eax, dword ptr [eax]
0x47CE8C: mov      eax, dword ptr [eax + 0x77c]
0x47CE92: mov      edx, 0x47d470 ; "年/月/日/:1975/08/21"
0x47CE97: mov      ecx, dword ptr [eax]
0x47CE99: call     dword ptr [ecx + 0x34]
0x47CE9C: mov      eax, dword ptr [ebp - 8]
0x47CE9F: mov      edx, dword ptr [ebp - 4]
0x47CEA2: cmp      eax, dword ptr [edx + 0x28]
0x47CEA5: jne      0x47cee9
0x47CEA7: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CEAC: mov      eax, dword ptr [eax]
0x47CEAE: mov      eax, dword ptr [eax + 0x77c]
0x47CEB4: mov      edx, 0x47d490 ; "请输入一个密码提示问题"
0x47CEB9: mov      ecx, dword ptr [eax]
0x47CEBB: call     dword ptr [ecx + 0x34]
0x47CEBE: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CEC3: mov      eax, dword ptr [eax]
0x47CEC5: mov      eax, dword ptr [eax + 0x77c]
0x47CECB: mov      edx, 0x47d4b0 ; "请明确,只有你本人才知道这个问题."
0x47CED0: mov      ecx, dword ptr [eax]
0x47CED2: call     dword ptr [ecx + 0x34]
0x47CED5: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CEDA: mov      eax, dword ptr [eax]
0x47CEDC: mov      eax, dword ptr [eax + 0x77c]
0x47CEE2: xor      edx, edx
0x47CEE4: mov      ecx, dword ptr [eax]
0x47CEE6: call     dword ptr [ecx + 0x34]
0x47CEE9: mov      eax, dword ptr [ebp - 8]
0x47CEEC: mov      edx, dword ptr [ebp - 4]
0x47CEEF: cmp      eax, dword ptr [edx + 0x2c]
0x47CEF2: jne      0x47cf22
0x47CEF4: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CEF9: mov      eax, dword ptr [eax]
0x47CEFB: mov      eax, dword ptr [eax + 0x77c]
0x47CF01: mov      edx, 0x47d4dc ; "请输入上面问题的"
0x47CF06: mov      ecx, dword ptr [eax]
0x47CF08: call     dword ptr [ecx + 0x34]
0x47CF0B: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CF10: mov      eax, dword ptr [eax]
0x47CF12: mov      eax, dword ptr [eax + 0x77c]
0x47CF18: mov      edx, 0x47d4f8 ; "答案。"
0x47CF1D: mov      ecx, dword ptr [eax]
0x47CF1F: call     dword ptr [ecx + 0x34]
0x47CF22: mov      eax, dword ptr [ebp - 8]
0x47CF25: mov      edx, dword ptr [ebp - 4]
0x47CF28: cmp      eax, dword ptr [edx + 0x30]
0x47CF2B: jne      0x47cf6f
0x47CF2D: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CF32: mov      eax, dword ptr [eax]
0x47CF34: mov      eax, dword ptr [eax + 0x77c]
0x47CF3A: mov      edx, 0x47d490 ; "请输入一个密码提示问题"
0x47CF3F: mov      ecx, dword ptr [eax]
0x47CF41: call     dword ptr [ecx + 0x34]
0x47CF44: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CF49: mov      eax, dword ptr [eax]
0x47CF4B: mov      eax, dword ptr [eax + 0x77c]
0x47CF51: mov      edx, 0x47d4b0 ; "请明确,只有你本人才知道这个问题."
0x47CF56: mov      ecx, dword ptr [eax]
0x47CF58: call     dword ptr [ecx + 0x34]
0x47CF5B: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CF60: mov      eax, dword ptr [eax]
0x47CF62: mov      eax, dword ptr [eax + 0x77c]
0x47CF68: xor      edx, edx
0x47CF6A: mov      ecx, dword ptr [eax]
0x47CF6C: call     dword ptr [ecx + 0x34]
0x47CF6F: mov      eax, dword ptr [ebp - 8]
0x47CF72: mov      edx, dword ptr [ebp - 4]
0x47CF75: cmp      eax, dword ptr [edx + 0x34]
0x47CF78: jne      0x47cfa8
0x47CF7A: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CF7F: mov      eax, dword ptr [eax]
0x47CF81: mov      eax, dword ptr [eax + 0x77c]
0x47CF87: mov      edx, 0x47d4dc ; "请输入上面问题的"
0x47CF8C: mov      ecx, dword ptr [eax]
0x47CF8E: call     dword ptr [ecx + 0x34]
0x47CF91: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CF96: mov      eax, dword ptr [eax]
0x47CF98: mov      eax, dword ptr [eax + 0x77c]
0x47CF9E: mov      edx, 0x47d4f8 ; "答案。"
0x47CFA3: mov      ecx, dword ptr [eax]
0x47CFA5: call     dword ptr [ecx + 0x34]
0x47CFA8: mov      eax, dword ptr [ebp - 8]
0x47CFAB: mov      edx, dword ptr [ebp - 4]
0x47CFAE: cmp      eax, dword ptr [edx + 0x1c]
0x47CFB1: je       0x47cfee
0x47CFB3: mov      eax, dword ptr [ebp - 8]
0x47CFB6: mov      edx, dword ptr [ebp - 4]
0x47CFB9: cmp      eax, dword ptr [edx + 0x20]
0x47CFBC: je       0x47cfee
0x47CFBE: mov      eax, dword ptr [ebp - 8]
0x47CFC1: mov      edx, dword ptr [ebp - 4]
0x47CFC4: cmp      eax, dword ptr [edx + 0x28]
0x47CFC7: je       0x47cfee
0x47CFC9: mov      eax, dword ptr [ebp - 8]
0x47CFCC: mov      edx, dword ptr [ebp - 4]
0x47CFCF: cmp      eax, dword ptr [edx + 0x30]
0x47CFD2: je       0x47cfee
0x47CFD4: mov      eax, dword ptr [ebp - 8]
0x47CFD7: mov      edx, dword ptr [ebp - 4]
0x47CFDA: cmp      eax, dword ptr [edx + 0x2c]
0x47CFDD: je       0x47cfee
0x47CFDF: mov      eax, dword ptr [ebp - 8]
0x47CFE2: mov      edx, dword ptr [ebp - 4]
0x47CFE5: cmp      eax, dword ptr [edx + 0x34]
0x47CFE8: jne      0x47d0ba
0x47CFEE: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47CFF3: mov      eax, dword ptr [eax]
0x47CFF5: mov      eax, dword ptr [eax + 0x77c]
0x47CFFB: mov      edx, 0x47d508 ; "你必须输入正确"
0x47D000: mov      ecx, dword ptr [eax]
0x47D002: call     dword ptr [ecx + 0x34]
0x47D005: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47D00A: mov      eax, dword ptr [eax]
0x47D00C: mov      eax, dword ptr [eax + 0x77c]
0x47D012: mov      edx, 0x47d520 ; "的信息。"
0x47D017: mov      ecx, dword ptr [eax]
0x47D019: call     dword ptr [ecx + 0x34]
0x47D01C: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47D021: mov      eax, dword ptr [eax]
0x47D023: mov      eax, dword ptr [eax + 0x77c]
0x47D029: mov      edx, 0x47d534 ; "如果使用了错误信息,"
0x47D02E: mov      ecx, dword ptr [eax]
0x47D030: call     dword ptr [ecx + 0x34]
0x47D033: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47D038: mov      eax, dword ptr [eax]
0x47D03A: mov      eax, dword ptr [eax + 0x77c]
0x47D040: xor      edx, edx
0x47D042: mov      ecx, dword ptr [eax]
0x47D044: call     dword ptr [ecx + 0x34]
0x47D047: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47D04C: mov      eax, dword ptr [eax]
0x47D04E: mov      eax, dword ptr [eax + 0x77c]
0x47D054: mov      edx, 0x47d550 ; "你将不能接受"
0x47D059: mov      ecx, dword ptr [eax]
0x47D05B: call     dword ptr [ecx + 0x34]
0x47D05E: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47D063: mov      eax, dword ptr [eax]
0x47D065: mov      eax, dword ptr [eax + 0x77c]
0x47D06B: mov      edx, 0x47d568 ; "我们所有的服务。"
0x47D070: mov      ecx, dword ptr [eax]
0x47D072: call     dword ptr [ecx + 0x34]
0x47D075: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47D07A: mov      eax, dword ptr [eax]
0x47D07C: mov      eax, dword ptr [eax + 0x77c]
0x47D082: mov      edx, 0x47d584 ; "如果你提供了"
0x47D087: mov      ecx, dword ptr [eax]
0x47D089: call     dword ptr [ecx + 0x34]
0x47D08C: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47D091: mov      eax, dword ptr [eax]
0x47D093: mov      eax, dword ptr [eax + 0x77c]
0x47D099: mov      edx, 0x47d59c ; "错误信息,你的帐户将被 "
0x47D09E: mov      ecx, dword ptr [eax]
0x47D0A0: call     dword ptr [ecx + 0x34]
0x47D0A3: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47D0A8: mov      eax, dword ptr [eax]
0x47D0AA: mov      eax, dword ptr [eax + 0x77c]
0x47D0B0: mov      edx, 0x47d5bc ; "取消 。"
0x47D0B5: mov      ecx, dword ptr [eax]
0x47D0B7: call     dword ptr [ecx + 0x34]
0x47D0BA: mov      eax, dword ptr [ebp - 8]
0x47D0BD: mov      edx, dword ptr [ebp - 4]
0x47D0C0: cmp      eax, dword ptr [edx + 0x38]
0x47D0C3: jne      0x47d0f3
0x47D0C5: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47D0CA: mov      eax, dword ptr [eax]
0x47D0CC: mov      eax, dword ptr [eax + 0x77c]
0x47D0D2: mov      edx, 0x47d5cc ; "请键入你的电话"
0x47D0D7: mov      ecx, dword ptr [eax]
0x47D0D9: call     dword ptr [ecx + 0x34]
0x47D0DC: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47D0E1: mov      eax, dword ptr [eax]
0x47D0E3: mov      eax, dword ptr [eax + 0x77c]
0x47D0E9: mov      edx, 0x47d5e4 ; "号码."
0x47D0EE: mov      ecx, dword ptr [eax]
0x47D0F0: call     dword ptr [ecx + 0x34]
0x47D0F3: mov      eax, dword ptr [ebp - 8]
0x47D0F6: mov      edx, dword ptr [ebp - 4]
0x47D0F9: cmp      eax, dword ptr [edx + 0x3c]
0x47D0FC: jne      0x47d115
0x47D0FE: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47D103: mov      eax, dword ptr [eax]
0x47D105: mov      eax, dword ptr [eax + 0x77c]
0x47D10B: mov      edx, 0x47d5f4 ; "请键入你的手机号码."
0x47D110: mov      ecx, dword ptr [eax]
0x47D112: call     dword ptr [ecx + 0x34]
0x47D115: mov      eax, dword ptr [ebp - 8]
0x47D118: mov      edx, dword ptr [ebp - 4]
0x47D11B: cmp      eax, dword ptr [edx + 0x40]
0x47D11E: jne      0x47d17c
0x47D120: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47D125: mov      eax, dword ptr [eax]
0x47D127: mov      eax, dword ptr [eax + 0x77c]
0x47D12D: mov      edx, 0x47d610 ; "请键入你的邮件地址。你的邮件将被"
0x47D132: mov      ecx, dword ptr [eax]
0x47D134: call     dword ptr [ecx + 0x34]
0x47D137: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47D13C: mov      eax, dword ptr [eax]
0x47D13E: mov      eax, dword ptr [eax + 0x77c]
0x47D144: mov      edx, 0x47d63c ; "用于访问我们的一些服务器，"
0x47D149: mov      ecx, dword ptr [eax]
0x47D14B: call     dword ptr [ecx + 0x34]
0x47D14E: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47D153: mov      eax, dword ptr [eax]
0x47D155: mov      eax, dword ptr [eax + 0x77c]
0x47D15B: mov      edx, 0x47d660 ; "你能收到最近更新的一些"
0x47D160: mov      ecx, dword ptr [eax]
0x47D162: call     dword ptr [ecx + 0x34]
0x47D165: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47D16A: mov      eax, dword ptr [eax]
0x47D16C: mov      eax, dword ptr [eax + 0x77c]
0x47D172: mov      edx, 0x47d680 ; "信息。"
0x47D177: mov      ecx, dword ptr [eax]
0x47D179: call     dword ptr [ecx + 0x34]
0x47D17C: mov      esp, ebp
0x47D17E: pop      ebp
0x47D17F: ret      
0x47D188: [STR len=28] "你的ID可以是以下内容的组合："
0x47D1B0: [STR len=10] "字符、数字"
0x47D1C4: [STR len=17] "ID必须至少有4位。"
0x47D1E0: [STR len=32] "你输入的名字是你游戏中角色的名字"
0x47D20C: [STR len=17] "请仔细选择你的ID,"
0x47D228: [STR len=14] "你的登陆名可以"
0x47D240: [STR len=21] " 用于我们所有服务器。"
0x47D260: [STR len=22] "我们建议你用一个不同的"
0x47D280: [STR len=24] "名字，和你想给游戏角色用"
0x47D2A4: [STR len=16] "的那个区别开来。"
0x47D2C0: [STR len=18] "你的密码可以是一个"
0x47D2DC: [STR len=16] "组合，包括：字符"
0x47D2F8: [STR len=12] "和数字。而且"
0x47D310: [STR len=15] "它至少要有4位。"
0x47D328: [STR len=16] "把你的密码记住是"
0x47D344: [STR len=25] "玩我们的游戏最基本的要素,"
0x47D368: [STR len=26] "所以请确认你已经记好了它。"
0x47D38C: [STR len=20] "我们建议你最好不要用"
0x47D3AC: [STR len=14] "一个简单的密码"
0x47D3C4: [STR len=10] "以消除一些"
0x47D3D8: [STR len=10] "不安全因素"
0x47D3EC: [STR len=12] "再次输入密码"
0x47D404: [STR len=6] "以确认"
0x47D414: [STR len=13] "键入你的全名."
0x47D42C: [STR len=4] "不用"
0x47D43C: [STR len=7] "在这次."
0x47D44C: [STR len=25] "请键入你的出生日期和月份,"
0x47D470: [STR len=20] "年/月/日/:1975/08/21"
0x47D490: [STR len=22] "请输入一个密码提示问题"
0x47D4B0: [STR len=32] "请明确,只有你本人才知道这个问题."
0x47D4DC: [STR len=16] "请输入上面问题的"
0x47D4F8: [STR len=6] "答案。"
0x47D508: [STR len=14] "你必须输入正确"
0x47D520: [STR len=8] "的信息。"
0x47D534: [STR len=19] "如果使用了错误信息,"
0x47D550: [STR len=12] "你将不能接受"
0x47D568: [STR len=16] "我们所有的服务。"
0x47D584: [STR len=12] "如果你提供了"
0x47D59C: [STR len=22] "错误信息,你的帐户将被 "
0x47D5BC: [STR len=7] "取消 。"
0x47D5CC: [STR len=14] "请键入你的电话"
0x47D5E4: [STR len=5] "号码."
0x47D5F4: [STR len=19] "请键入你的手机号码."
0x47D610: [STR len=32] "请键入你的邮件地址。你的邮件将被"
0x47D63C: [STR len=26] "用于访问我们的一些服务器，"
0x47D660: [STR len=22] "你能收到最近更新的一些"
0x47D680: [STR len=6] "信息。"
0x47D688: push     ebp
0x47D689: mov      ebp, esp
0x47D68B: push     ecx
0x47D68C: mov      dword ptr [ebp - 4], eax
0x47D68F: mov      dl, 4
0x47D691: mov      eax, dword ptr [ebp - 4]
0x47D694: call     0x47d864
0x47D699: pop      ecx
0x47D69A: pop      ebp
0x47D69B: ret      
0x47D69C: push     ebp
0x47D69D: mov      ebp, esp
0x47D69F: push     ecx
0x47D6A0: mov      dword ptr [ebp - 4], eax
0x47D6A3: mov      eax, dword ptr [ebp - 4]
0x47D6A6: mov      byte ptr [eax + 0x60], 1
0x47D6AA: call     0x406154
0x47D6AF: mov      edx, dword ptr [ebp - 4]
0x47D6B2: mov      dword ptr [edx + 0x5c], eax
0x47D6B5: mov      eax, dword ptr [ebp - 4]
0x47D6B8: call     0x47d688
0x47D6BD: mov      eax, 0x64
0x47D6C2: call     0x495794
0x47D6C7: pop      ecx
0x47D6C8: pop      ebp
0x47D6C9: ret      
0x47D6CA: mov      eax, eax
0x47D6CC: push     ebp
0x47D6CD: mov      ebp, esp
0x47D6CF: add      esp, -0x1c
0x47D6D2: mov      dword ptr [ebp - 8], edx
0x47D6D5: mov      dword ptr [ebp - 4], eax
0x47D6D8: mov      eax, dword ptr [ebp - 4]
0x47D6DB: cmp      byte ptr [eax + 0x61], 0
0x47D6DF: je       0x47d710
0x47D6E1: mov      eax, dword ptr [ebp - 4]
0x47D6E4: mov      byte ptr [eax + 0x61], 0
0x47D6E8: mov      dl, 1
0x47D6EA: mov      eax, dword ptr [ebp - 4]
0x47D6ED: mov      eax, dword ptr [eax + 8]
0x47D6F0: call     0x4278c0
0x47D6F5: mov      dl, 1
0x47D6F7: mov      eax, dword ptr [ebp - 4]
0x47D6FA: mov      eax, dword ptr [eax + 0xc]
0x47D6FD: call     0x4278c0
0x47D702: mov      eax, dword ptr [ebp - 4]
0x47D705: mov      eax, dword ptr [eax + 8]
0x47D708: mov      edx, dword ptr [eax]
0x47D70A: call     dword ptr [edx + 0xb4]
0x47D710: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47D715: mov      eax, dword ptr [eax]
0x47D717: mov      eax, dword ptr [eax + 0x34c]
0x47D71D: mov      edx, 0x16
0x47D722: call     0x45351c
0x47D727: mov      dword ptr [ebp - 0xc], eax
0x47D72A: cmp      dword ptr [ebp - 0xc], 0
0x47D72E: je       0x47d751
0x47D730: lea      edx, [ebp - 0x1c]
0x47D733: mov      eax, dword ptr [ebp - 0xc]
0x47D736: call     0x44d8ec
0x47D73B: lea      eax, [ebp - 0x1c]
0x47D73E: push     eax
0x47D73F: mov      eax, dword ptr [ebp - 0xc]
0x47D742: push     eax
0x47D743: push     0
0x47D745: xor      ecx, ecx
0x47D747: xor      edx, edx
0x47D749: mov      eax, dword ptr [ebp - 8]
0x47D74C: call     0x44d6cc
0x47D751: mov      eax, dword ptr [ebp - 4]
0x47D754: cmp      byte ptr [eax + 0x60], 0
0x47D758: je       0x47d860
0x47D75E: call     0x406154
0x47D763: mov      edx, dword ptr [ebp - 4]
0x47D766: sub      eax, dword ptr [edx + 0x5c]
0x47D769: cmp      eax, 0xe6
0x47D76E: jbe      0x47d781
0x47D770: call     0x406154
0x47D775: mov      edx, dword ptr [ebp - 4]
0x47D778: mov      dword ptr [edx + 0x5c], eax
0x47D77B: mov      eax, dword ptr [ebp - 4]
0x47D77E: inc      dword ptr [eax + 0x54]
0x47D781: mov      eax, dword ptr [ebp - 4]
0x47D784: mov      eax, dword ptr [eax + 0x58]
0x47D787: dec      eax
0x47D788: mov      edx, dword ptr [ebp - 4]
0x47D78B: cmp      eax, dword ptr [edx + 0x54]
0x47D78E: jg       0x47d7cc
0x47D790: mov      eax, dword ptr [ebp - 4]
0x47D793: mov      eax, dword ptr [eax + 0x58]
0x47D796: dec      eax
0x47D797: mov      edx, dword ptr [ebp - 4]
0x47D79A: mov      dword ptr [edx + 0x54], eax
0x47D79D: mov      eax, dword ptr [0x4a3ec8] ; *0x4A3EC8->0x4F9468
0x47D7A2: cmp      byte ptr [eax], 0
0x47D7A5: jne      0x47d7cc
0x47D7A7: mov      eax, dword ptr [0x4a4074] ; *0x4A4074->0x4F9469
0x47D7AC: cmp      byte ptr [eax], 0
0x47D7AF: jne      0x47d7cc
0x47D7B1: mov      eax, dword ptr [0x4a3ec8] ; *0x4A3EC8->0x4F9468
0x47D7B6: mov      byte ptr [eax], 1
0x47D7B9: mov      eax, dword ptr [0x4a4074] ; *0x4A4074->0x4F9469
0x47D7BE: mov      byte ptr [eax], 1
0x47D7C1: mov      eax, dword ptr [0x4a42d8] ; *0x4A42D8->0x4F946C
0x47D7C6: mov      dword ptr [eax], 0x1d
0x47D7CC: mov      eax, dword ptr [ebp - 4]
0x47D7CF: mov      edx, dword ptr [eax + 0x54]
0x47D7D2: add      edx, 0x67
0x47D7D5: sub      edx, 0x50
0x47D7D8: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47D7DD: mov      eax, dword ptr [eax]
0x47D7DF: mov      eax, dword ptr [eax + 0x34c]
0x47D7E5: call     0x45351c
0x47D7EA: mov      dword ptr [ebp - 0xc], eax
0x47D7ED: cmp      dword ptr [ebp - 0xc], 0
0x47D7F1: je       0x47d81a
0x47D7F3: lea      edx, [ebp - 0x1c]
0x47D7F6: mov      eax, dword ptr [ebp - 0xc]
0x47D7F9: call     0x44d8ec
0x47D7FE: lea      eax, [ebp - 0x1c]
0x47D801: push     eax
0x47D802: mov      eax, dword ptr [ebp - 0xc]
0x47D805: push     eax
0x47D806: push     1
0x47D808: mov      ecx, 0x60
0x47D80D: mov      edx, 0x98
0x47D812: mov      eax, dword ptr [ebp - 8]
0x47D815: call     0x44d6cc
0x47D81A: mov      eax, dword ptr [0x4a3ec8] ; *0x4A3EC8->0x4F9468
0x47D81F: cmp      byte ptr [eax], 0
0x47D822: je       0x47d860
0x47D824: mov      eax, dword ptr [0x4a42d8] ; *0x4A42D8->0x4F946C
0x47D829: cmp      dword ptr [eax], 1
0x47D82C: jg       0x47d860
0x47D82E: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47D833: mov      eax, dword ptr [eax]
0x47D835: mov      eax, dword ptr [eax + 0x308]
0x47D83B: call     0x453828
0x47D840: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47D845: mov      eax, dword ptr [eax]
0x47D847: mov      eax, dword ptr [eax + 0x34c]
0x47D84D: call     0x453828
0x47D852: mov      eax, dword ptr [0x4a3fb8] ; *0x4A3FB8->0x4F7D8C
0x47D857: mov      eax, dword ptr [eax]
0x47D859: mov      dl, 3
0x47D85B: call     0x4799ac
0x47D860: mov      esp, ebp
0x47D862: pop      ebp
0x47D863: ret      
0x47D864: push     ebp
0x47D865: mov      ebp, esp
0x47D867: add      esp, -0x18
0x47D86A: mov      byte ptr [ebp - 5], dl
0x47D86D: mov      dword ptr [ebp - 4], eax
0x47D870: mov      dword ptr [ebp - 0x10], 0xffffffff
0x47D877: mov      al, byte ptr [ebp - 5]
0x47D87A: sub      al, 1
0x47D87C: jb       0x47d88a
0x47D87E: sub      al, 2
0x47D880: jb       0x47d893
0x47D882: je       0x47d89c
0x47D884: dec      al
0x47D886: je       0x47d8a5
0x47D888: jmp      0x47d8ac
0x47D88A: mov      dword ptr [ebp - 0x10], 0xa
0x47D891: jmp      0x47d8ac
0x47D893: mov      dword ptr [ebp - 0x10], 0xb
0x47D89A: jmp      0x47d8ac
0x47D89C: mov      dword ptr [ebp - 0x10], 0xc
0x47D8A3: jmp      0x47d8ac
0x47D8A5: mov      dword ptr [ebp - 0x10], 0xffffffff
0x47D8AC: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47D8B1: mov      eax, dword ptr [eax]
0x47D8B3: call     0x42a8bc
0x47D8B8: dec      eax
0x47D8B9: test     eax, eax
0x47D8BB: jl       0x47d937
0x47D8BD: inc      eax
0x47D8BE: mov      dword ptr [ebp - 0x18], eax
0x47D8C1: mov      dword ptr [ebp - 0xc], 0
0x47D8C8: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47D8CD: mov      eax, dword ptr [eax]
0x47D8CF: mov      edx, dword ptr [ebp - 0xc]
0x47D8D2: call     0x42a880
0x47D8D7: mov      dword ptr [ebp - 0x14], eax
0x47D8DA: mov      eax, dword ptr [ebp - 0x14]
0x47D8DD: mov      edx, dword ptr [0x41f814]
0x47D8E3: call     0x40301c
0x47D8E8: test     al, al
0x47D8EA: je       0x47d92f
0x47D8EC: mov      eax, dword ptr [ebp - 0x14]
0x47D8EF: mov      eax, dword ptr [eax + 0xc]
0x47D8F2: add      eax, -0xa
0x47D8F5: sub      eax, 3
0x47D8F8: jae      0x47d92f
0x47D8FA: mov      eax, dword ptr [ebp - 0x14]
0x47D8FD: mov      eax, dword ptr [eax + 0xc]
0x47D900: cmp      eax, dword ptr [ebp - 0x10]
0x47D903: jne      0x47d91b
0x47D905: mov      dl, 1
0x47D907: mov      eax, dword ptr [ebp - 0x14]
0x47D90A: call     0x4278c0
0x47D90F: xor      edx, edx
0x47D911: mov      eax, dword ptr [ebp - 0x14]
0x47D914: call     0x427a00
0x47D919: jmp      0x47d92f
0x47D91B: xor      edx, edx
0x47D91D: mov      eax, dword ptr [ebp - 0x14]
0x47D920: call     0x4278c0
0x47D925: xor      edx, edx
0x47D927: mov      eax, dword ptr [ebp - 0x14]
0x47D92A: call     0x427a00
0x47D92F: inc      dword ptr [ebp - 0xc]
0x47D932: dec      dword ptr [ebp - 0x18]
0x47D935: jne      0x47d8c8
0x47D937: mov      eax, dword ptr [ebp - 4]
0x47D93A: mov      eax, dword ptr [eax + 0x20]
0x47D93D: xor      edx, edx
0x47D93F: call     0x4278c0
0x47D944: mov      al, byte ptr [ebp - 5]
0x47D947: sub      al, 1
0x47D949: jb       0x47d962
0x47D94B: sub      al, 2
0x47D94D: jb       0x47d9c1
0x47D94F: je       0x47da89
0x47D955: dec      al
0x47D957: je       0x47dae1
0x47D95D: jmp      0x47db1d
0x47D962: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47D967: mov      eax, dword ptr [eax]
0x47D969: mov      eax, dword ptr [eax + 0x304]
0x47D96F: xor      edx, edx
0x47D971: call     0x458214
0x47D976: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47D97B: mov      eax, dword ptr [eax]
0x47D97D: mov      eax, dword ptr [eax + 0x3bc]
0x47D983: xor      edx, edx
0x47D985: call     0x458214
0x47D98A: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47D98F: mov      eax, dword ptr [eax]
0x47D991: mov      eax, dword ptr [eax + 0x2f8]
0x47D997: mov      dl, 1
0x47D999: call     0x458214
0x47D99E: mov      eax, dword ptr [ebp - 4]
0x47D9A1: mov      eax, dword ptr [eax + 8]
0x47D9A4: cmp      byte ptr [eax + 0x47], 0
0x47D9A8: je       0x47db1d
0x47D9AE: mov      eax, dword ptr [ebp - 4]
0x47D9B1: mov      eax, dword ptr [eax + 8]
0x47D9B4: mov      edx, dword ptr [eax]
0x47D9B6: call     dword ptr [edx + 0xb4]
0x47D9BC: jmp      0x47db1d
0x47D9C1: mov      eax, dword ptr [ebp - 4]
0x47D9C4: cmp      byte ptr [eax + 0x164], 0
0x47D9CB: je       0x47d9dc
0x47D9CD: mov      eax, dword ptr [ebp - 4]
0x47D9D0: mov      eax, dword ptr [eax + 0x10]
0x47D9D3: xor      edx, edx
0x47D9D5: mov      ecx, dword ptr [eax]
0x47D9D7: call     dword ptr [ecx + 0x60]
0x47D9DA: jmp      0x47d9e9
0x47D9DC: mov      eax, dword ptr [ebp - 4]
0x47D9DF: mov      eax, dword ptr [eax + 0x10]
0x47D9E2: mov      dl, 1
0x47D9E4: mov      ecx, dword ptr [eax]
0x47D9E6: call     dword ptr [ecx + 0x60]
0x47D9E9: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47D9EE: mov      eax, dword ptr [eax]
0x47D9F0: mov      eax, dword ptr [eax + 0x304]
0x47D9F6: mov      dl, 1
0x47D9F8: call     0x458214
0x47D9FD: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47DA02: mov      eax, dword ptr [eax]
0x47DA04: mov      eax, dword ptr [eax + 0x3bc]
0x47DA0A: xor      edx, edx
0x47DA0C: call     0x458214
0x47DA11: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47DA16: mov      eax, dword ptr [eax]
0x47DA18: mov      eax, dword ptr [eax + 0x2f8]
0x47DA1E: xor      edx, edx
0x47DA20: call     0x458214
0x47DA25: mov      eax, dword ptr [ebp - 4]
0x47DA28: mov      eax, dword ptr [eax + 0x10]
0x47DA2B: cmp      byte ptr [eax + 0x47], 0
0x47DA2F: je       0x47da53
0x47DA31: mov      eax, dword ptr [ebp - 4]
0x47DA34: mov      eax, dword ptr [eax + 0x10]
0x47DA37: mov      edx, dword ptr [eax]
0x47DA39: call     dword ptr [edx + 0x50]
0x47DA3C: test     al, al
0x47DA3E: je       0x47da53
0x47DA40: mov      eax, dword ptr [ebp - 4]
0x47DA43: mov      eax, dword ptr [eax + 0x10]
0x47DA46: mov      edx, dword ptr [eax]
0x47DA48: call     dword ptr [edx + 0xb4]
0x47DA4E: jmp      0x47db1d
0x47DA53: mov      eax, dword ptr [ebp - 4]
0x47DA56: mov      eax, dword ptr [eax + 0x18]
0x47DA59: cmp      byte ptr [eax + 0x47], 0
0x47DA5D: je       0x47db1d
0x47DA63: mov      eax, dword ptr [ebp - 4]
0x47DA66: mov      eax, dword ptr [eax + 0x18]
0x47DA69: mov      edx, dword ptr [eax]
0x47DA6B: call     dword ptr [edx + 0x50]
0x47DA6E: test     al, al
0x47DA70: je       0x47db1d
0x47DA76: mov      eax, dword ptr [ebp - 4]
0x47DA79: mov      eax, dword ptr [eax + 0x18]
0x47DA7C: mov      edx, dword ptr [eax]
0x47DA7E: call     dword ptr [edx + 0xb4]
0x47DA84: jmp      0x47db1d
0x47DA89: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47DA8E: mov      eax, dword ptr [eax]
0x47DA90: mov      eax, dword ptr [eax + 0x304]
0x47DA96: xor      edx, edx
0x47DA98: call     0x458214
0x47DA9D: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47DAA2: mov      eax, dword ptr [eax]
0x47DAA4: mov      eax, dword ptr [eax + 0x3bc]
0x47DAAA: mov      dl, 1
0x47DAAC: call     0x458214
0x47DAB1: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47DAB6: mov      eax, dword ptr [eax]
0x47DAB8: mov      eax, dword ptr [eax + 0x2f8]
0x47DABE: xor      edx, edx
0x47DAC0: call     0x458214
0x47DAC5: mov      eax, dword ptr [ebp - 4]
0x47DAC8: mov      eax, dword ptr [eax + 0x44]
0x47DACB: cmp      byte ptr [eax + 0x47], 0
0x47DACF: je       0x47db1d
0x47DAD1: mov      eax, dword ptr [ebp - 4]
0x47DAD4: mov      eax, dword ptr [eax + 0x44]
0x47DAD7: mov      edx, dword ptr [eax]
0x47DAD9: call     dword ptr [edx + 0xb4]
0x47DADF: jmp      0x47db1d
0x47DAE1: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47DAE6: mov      eax, dword ptr [eax]
0x47DAE8: mov      eax, dword ptr [eax + 0x304]
0x47DAEE: xor      edx, edx
0x47DAF0: call     0x458214
0x47DAF5: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47DAFA: mov      eax, dword ptr [eax]
0x47DAFC: mov      eax, dword ptr [eax + 0x3bc]
0x47DB02: xor      edx, edx
0x47DB04: call     0x458214
0x47DB09: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47DB0E: mov      eax, dword ptr [eax]
0x47DB10: mov      eax, dword ptr [eax + 0x2f8]
0x47DB16: xor      edx, edx
0x47DB18: call     0x458214
0x47DB1D: mov      esp, ebp
0x47DB1F: pop      ebp
0x47DB20: ret      
0x47DB21: lea      eax, [eax]
0x47DB24: push     ebp
0x47DB25: mov      ebp, esp
0x47DB27: push     ecx
0x47DB28: mov      dword ptr [ebp - 4], eax
0x47DB2B: mov      eax, dword ptr [ebp - 4]
0x47DB2E: mov      byte ptr [eax + 0x164], 0
0x47DB35: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47DB3A: mov      eax, dword ptr [eax]
0x47DB3C: add      eax, 0x780
0x47DB41: call     0x403a58
0x47DB46: mov      dl, 1
0x47DB48: mov      eax, dword ptr [ebp - 4]
0x47DB4B: call     0x47d864
0x47DB50: pop      ecx
0x47DB51: pop      ebp
0x47DB52: ret      
0x47DB53: nop      
0x47DB54: push     ebp
0x47DB55: mov      ebp, esp
0x47DB57: add      esp, -0xc
0x47DB5A: xor      ecx, ecx
0x47DB5C: mov      dword ptr [ebp - 0xc], ecx
0x47DB5F: mov      byte ptr [ebp - 5], dl
0x47DB62: mov      dword ptr [ebp - 4], eax
0x47DB65: xor      eax, eax
0x47DB67: push     ebp
0x47DB68: push     0x47dd0a
0x47DB6D: push     dword ptr fs:[eax]
0x47DB70: mov      dword ptr fs:[eax], esp
0x47DB73: mov      al, byte ptr [ebp - 5]
0x47DB76: mov      edx, dword ptr [ebp - 4]
0x47DB79: mov      byte ptr [edx + 0x164], al
0x47DB7F: mov      dl, 2
0x47DB81: mov      eax, dword ptr [ebp - 4]
0x47DB84: call     0x47d864
0x47DB89: lea      eax, [ebp - 0xc]
0x47DB8C: mov      edx, dword ptr [ebp - 4]
0x47DB8F: add      edx, 0x62
0x47DB92: call     0x403c78
0x47DB97: mov      edx, dword ptr [ebp - 0xc]
0x47DB9A: mov      eax, dword ptr [ebp - 4]
0x47DB9D: mov      eax, dword ptr [eax + 0x10]
0x47DBA0: call     0x427a00
0x47DBA5: lea      eax, [ebp - 0xc]
0x47DBA8: mov      edx, dword ptr [ebp - 4]
0x47DBAB: add      edx, 0x6d
0x47DBAE: call     0x403c78
0x47DBB3: mov      edx, dword ptr [ebp - 0xc]
0x47DBB6: mov      eax, dword ptr [ebp - 4]
0x47DBB9: mov      eax, dword ptr [eax + 0x14]
0x47DBBC: call     0x427a00
0x47DBC1: lea      eax, [ebp - 0xc]
0x47DBC4: mov      edx, dword ptr [ebp - 4]
0x47DBC7: add      edx, 0x78
0x47DBCA: call     0x403c78
0x47DBCF: mov      edx, dword ptr [ebp - 0xc]
0x47DBD2: mov      eax, dword ptr [ebp - 4]
0x47DBD5: mov      eax, dword ptr [eax + 0x1c]
0x47DBD8: call     0x427a00
0x47DBDD: lea      eax, [ebp - 0xc]
0x47DBE0: mov      edx, dword ptr [ebp - 4]
0x47DBE3: add      edx, 0x8d
0x47DBE9: call     0x403c78
0x47DBEE: mov      edx, dword ptr [ebp - 0xc]
0x47DBF1: mov      eax, dword ptr [ebp - 4]
0x47DBF4: mov      eax, dword ptr [eax + 0x20]
0x47DBF7: call     0x427a00
0x47DBFC: lea      eax, [ebp - 0xc]
0x47DBFF: mov      edx, dword ptr [ebp - 4]
0x47DC02: add      edx, 0xab
0x47DC08: call     0x403c78
0x47DC0D: mov      edx, dword ptr [ebp - 0xc]
0x47DC10: mov      eax, dword ptr [ebp - 4]
0x47DC13: mov      eax, dword ptr [eax + 0x28]
0x47DC16: call     0x427a00
0x47DC1B: lea      eax, [ebp - 0xc]
0x47DC1E: mov      edx, dword ptr [ebp - 4]
0x47DC21: add      edx, 0xc0
0x47DC27: call     0x403c78
0x47DC2C: mov      edx, dword ptr [ebp - 0xc]
0x47DC2F: mov      eax, dword ptr [ebp - 4]
0x47DC32: mov      eax, dword ptr [eax + 0x2c]
0x47DC35: call     0x427a00
0x47DC3A: lea      eax, [ebp - 0xc]
0x47DC3D: mov      edx, dword ptr [ebp - 4]
0x47DC40: add      edx, 0x9c
0x47DC46: call     0x403c78
0x47DC4B: mov      edx, dword ptr [ebp - 0xc]
0x47DC4E: mov      eax, dword ptr [ebp - 4]
0x47DC51: mov      eax, dword ptr [eax + 0x38]
0x47DC54: call     0x427a00
0x47DC59: lea      eax, [ebp - 0xc]
0x47DC5C: mov      edx, dword ptr [ebp - 4]
0x47DC5F: add      edx, 0xcd
0x47DC65: call     0x403c78
0x47DC6A: mov      edx, dword ptr [ebp - 0xc]
0x47DC6D: mov      eax, dword ptr [ebp - 4]
0x47DC70: mov      eax, dword ptr [eax + 0x40]
0x47DC73: call     0x427a00
0x47DC78: lea      eax, [ebp - 0xc]
0x47DC7B: mov      edx, dword ptr [ebp - 4]
0x47DC7E: add      edx, 0xf6
0x47DC84: call     0x403c78
0x47DC89: mov      edx, dword ptr [ebp - 0xc]
0x47DC8C: mov      eax, dword ptr [ebp - 4]
0x47DC8F: mov      eax, dword ptr [eax + 0x30]
0x47DC92: call     0x427a00
0x47DC97: lea      eax, [ebp - 0xc]
0x47DC9A: mov      edx, dword ptr [ebp - 4]
0x47DC9D: add      edx, 0x10b
0x47DCA3: call     0x403c78
0x47DCA8: mov      edx, dword ptr [ebp - 0xc]
0x47DCAB: mov      eax, dword ptr [ebp - 4]
0x47DCAE: mov      eax, dword ptr [eax + 0x34]
0x47DCB1: call     0x427a00
0x47DCB6: lea      eax, [ebp - 0xc]
0x47DCB9: mov      edx, dword ptr [ebp - 4]
0x47DCBC: add      edx, 0x123
0x47DCC2: call     0x403c78
0x47DCC7: mov      edx, dword ptr [ebp - 0xc]
0x47DCCA: mov      eax, dword ptr [ebp - 4]
0x47DCCD: mov      eax, dword ptr [eax + 0x3c]
0x47DCD0: call     0x427a00
0x47DCD5: lea      eax, [ebp - 0xc]
0x47DCD8: mov      edx, dword ptr [ebp - 4]
0x47DCDB: add      edx, 0x118
0x47DCE1: call     0x403c78
0x47DCE6: mov      edx, dword ptr [ebp - 0xc]
0x47DCE9: mov      eax, dword ptr [ebp - 4]
0x47DCEC: mov      eax, dword ptr [eax + 0x24]
0x47DCEF: call     0x427a00
0x47DCF4: xor      eax, eax
0x47DCF6: pop      edx
0x47DCF7: pop      ecx
0x47DCF8: pop      ecx
0x47DCF9: mov      dword ptr fs:[eax], edx
0x47DCFC: push     0x47dd11
0x47DD01: lea      eax, [ebp - 0xc]
0x47DD04: call     0x403a58
0x47DD09: ret      
0x47DD0A: jmp      0x403518
0x47DD0F: jmp      0x47dd01
0x47DD11: mov      esp, ebp
0x47DD13: pop      ebp
0x47DD14: ret      
0x47DD15: lea      eax, [eax]
0x47DD18: push     ebp
0x47DD19: mov      ebp, esp
0x47DD1B: add      esp, 0xffffff68
0x47DD21: push     esi
0x47DD22: push     edi
0x47DD23: mov      esi, edx
0x47DD25: lea      edi, [ebp - 0x98]
0x47DD2B: mov      ecx, 0x25
0x47DD30: rep movsd dword ptr es:[edi], dword ptr [esi]
0x47DD32: mov      dword ptr [ebp - 4], eax
0x47DD35: mov      eax, dword ptr [ebp - 4]
0x47DD38: lea      esi, [ebp - 0x98]
0x47DD3E: lea      edi, [eax + 0x62]
0x47DD41: mov      ecx, 0x25
0x47DD46: rep movsd dword ptr es:[edi], dword ptr [esi]
0x47DD48: mov      eax, dword ptr [ebp - 4]
0x47DD4B: add      eax, 0xf6
0x47DD50: xor      ecx, ecx
0x47DD52: mov      edx, 0x65
0x47DD57: call     0x402b54
0x47DD5C: mov      eax, dword ptr [ebp - 4]
0x47DD5F: mov      byte ptr [eax + 0x164], 1
0x47DD66: mov      dl, 1
0x47DD68: mov      eax, dword ptr [ebp - 4]
0x47DD6B: call     0x47db54
0x47DD70: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47DD75: mov      eax, dword ptr [eax]
0x47DD77: add      eax, 0x780
0x47DD7C: mov      edx, 0x47dd94 ; "(请填写好帐户信息中所有必填的内容。)"
0x47DD81: call     0x403aac
0x47DD86: pop      edi
0x47DD87: pop      esi
0x47DD88: mov      esp, ebp
0x47DD8A: pop      ebp
0x47DD8B: ret      
0x47DD94: [STR len=36] "(请填写好帐户信息中所有必填的内容。)"
0x47DDBC: push     ebp
0x47DDBD: mov      ebp, esp
0x47DDBF: add      esp, -8
0x47DDC2: mov      dword ptr [ebp - 4], eax
0x47DDC5: mov      byte ptr [ebp - 5], 0xd
0x47DDC9: lea      ecx, [ebp - 5]
0x47DDCC: mov      edx, dword ptr [ebp - 4]
0x47DDCF: mov      eax, dword ptr [ebp - 4]
0x47DDD2: call     0x47c218
0x47DDD7: pop      ecx
0x47DDD8: pop      ecx
0x47DDD9: pop      ebp
0x47DDDA: ret      
0x47DDDB: nop      
0x47DDDC: push     ebp
0x47DDDD: mov      ebp, esp
0x47DDDF: push     ecx
0x47DDE0: mov      dword ptr [ebp - 4], eax
0x47DDE3: mov      dl, 3
0x47DDE5: mov      eax, dword ptr [ebp - 4]
0x47DDE8: call     0x47d864
0x47DDED: pop      ecx
0x47DDEE: pop      ebp
0x47DDEF: ret      
0x47DDF0: push     ebp
0x47DDF1: mov      ebp, esp
0x47DDF3: xor      ecx, ecx
0x47DDF5: push     ecx
0x47DDF6: push     ecx
0x47DDF7: push     ecx
0x47DDF8: push     ecx
0x47DDF9: push     ecx
0x47DDFA: mov      dword ptr [ebp - 4], eax
0x47DDFD: xor      eax, eax
0x47DDFF: push     ebp
0x47DE00: push     0x47e035
0x47DE05: push     dword ptr fs:[eax]
0x47DE08: mov      dword ptr fs:[eax], esp
0x47DE0B: mov      byte ptr [ebp - 5], 0
0x47DE0F: lea      edx, [ebp - 0x10]
0x47DE12: mov      eax, dword ptr [ebp - 4]
0x47DE15: mov      eax, dword ptr [eax + 0x10]
0x47DE18: call     0x4279d0
0x47DE1D: mov      eax, dword ptr [ebp - 0x10]
0x47DE20: lea      edx, [ebp - 0xc]
0x47DE23: call     0x407be4
0x47DE28: mov      edx, dword ptr [ebp - 0xc]
0x47DE2B: mov      eax, dword ptr [ebp - 4]
0x47DE2E: mov      eax, dword ptr [eax + 0x10]
0x47DE31: call     0x427a00
0x47DE36: lea      edx, [ebp - 0x10]
0x47DE39: mov      eax, dword ptr [ebp - 4]
0x47DE3C: mov      eax, dword ptr [eax + 0x28]
0x47DE3F: call     0x4279d0
0x47DE44: mov      eax, dword ptr [ebp - 0x10]
0x47DE47: lea      edx, [ebp - 0xc]
0x47DE4A: call     0x407be4
0x47DE4F: mov      edx, dword ptr [ebp - 0xc]
0x47DE52: mov      eax, dword ptr [ebp - 4]
0x47DE55: mov      eax, dword ptr [eax + 0x28]
0x47DE58: call     0x427a00
0x47DE5D: lea      edx, [ebp - 0x10]
0x47DE60: mov      eax, dword ptr [ebp - 4]
0x47DE63: mov      eax, dword ptr [eax + 0x1c]
0x47DE66: call     0x4279d0
0x47DE6B: mov      eax, dword ptr [ebp - 0x10]
0x47DE6E: lea      edx, [ebp - 0xc]
0x47DE71: call     0x407be4
0x47DE76: mov      edx, dword ptr [ebp - 0xc]
0x47DE79: mov      eax, dword ptr [ebp - 4]
0x47DE7C: mov      eax, dword ptr [eax + 0x1c]
0x47DE7F: call     0x427a00
0x47DE84: mov      eax, dword ptr [ebp - 4]
0x47DE87: call     0x47c3a8
0x47DE8C: test     al, al
0x47DE8E: je       0x47e012
0x47DE94: mov      eax, dword ptr [ebp - 4]
0x47DE97: call     0x47c5f0
0x47DE9C: test     al, al
0x47DE9E: je       0x47e012
0x47DEA4: lea      edx, [ebp - 0x10]
0x47DEA7: mov      eax, dword ptr [ebp - 4]
0x47DEAA: mov      eax, dword ptr [eax + 0x10]
0x47DEAD: call     0x4279d0
0x47DEB2: mov      eax, dword ptr [ebp - 0x10]
0x47DEB5: call     0x403cd4
0x47DEBA: cmp      eax, 3
0x47DEBD: jge      0x47ded2
0x47DEBF: mov      eax, dword ptr [ebp - 4]
0x47DEC2: mov      eax, dword ptr [eax + 0x10]
0x47DEC5: mov      edx, dword ptr [eax]
0x47DEC7: call     dword ptr [edx + 0xb4]
0x47DECD: jmp      0x47e012
0x47DED2: lea      edx, [ebp - 0x10]
0x47DED5: mov      eax, dword ptr [ebp - 4]
0x47DED8: mov      eax, dword ptr [eax + 0x14]
0x47DEDB: call     0x4279d0
0x47DEE0: mov      eax, dword ptr [ebp - 0x10]
0x47DEE3: call     0x403cd4
0x47DEE8: cmp      eax, 3
0x47DEEB: jge      0x47df00
0x47DEED: mov      eax, dword ptr [ebp - 4]
0x47DEF0: mov      eax, dword ptr [eax + 0x14]
0x47DEF3: mov      edx, dword ptr [eax]
0x47DEF5: call     dword ptr [edx + 0xb4]
0x47DEFB: jmp      0x47e012
0x47DF00: lea      edx, [ebp - 0x10]
0x47DF03: mov      eax, dword ptr [ebp - 4]
0x47DF06: mov      eax, dword ptr [eax + 0x14]
0x47DF09: call     0x4279d0
0x47DF0E: mov      eax, dword ptr [ebp - 0x10]
0x47DF11: push     eax
0x47DF12: lea      edx, [ebp - 0x14]
0x47DF15: mov      eax, dword ptr [ebp - 4]
0x47DF18: mov      eax, dword ptr [eax + 0x18]
0x47DF1B: call     0x4279d0
0x47DF20: mov      edx, dword ptr [ebp - 0x14]
0x47DF23: pop      eax
0x47DF24: call     0x403de4
0x47DF29: je       0x47df3e
0x47DF2B: mov      eax, dword ptr [ebp - 4]
0x47DF2E: mov      eax, dword ptr [eax + 0x18]
0x47DF31: mov      edx, dword ptr [eax]
0x47DF33: call     dword ptr [edx + 0xb4]
0x47DF39: jmp      0x47e012
0x47DF3E: lea      edx, [ebp - 0x10]
0x47DF41: mov      eax, dword ptr [ebp - 4]
0x47DF44: mov      eax, dword ptr [eax + 0x28]
0x47DF47: call     0x4279d0
0x47DF4C: mov      eax, dword ptr [ebp - 0x10]
0x47DF4F: call     0x403cd4
0x47DF54: dec      eax
0x47DF55: jge      0x47df6a
0x47DF57: mov      eax, dword ptr [ebp - 4]
0x47DF5A: mov      eax, dword ptr [eax + 0x28]
0x47DF5D: mov      edx, dword ptr [eax]
0x47DF5F: call     dword ptr [edx + 0xb4]
0x47DF65: jmp      0x47e012
0x47DF6A: lea      edx, [ebp - 0x10]
0x47DF6D: mov      eax, dword ptr [ebp - 4]
0x47DF70: mov      eax, dword ptr [eax + 0x2c]
0x47DF73: call     0x4279d0
0x47DF78: mov      eax, dword ptr [ebp - 0x10]
0x47DF7B: call     0x403cd4
0x47DF80: dec      eax
0x47DF81: jge      0x47df93
0x47DF83: mov      eax, dword ptr [ebp - 4]
0x47DF86: mov      eax, dword ptr [eax + 0x2c]
0x47DF89: mov      edx, dword ptr [eax]
0x47DF8B: call     dword ptr [edx + 0xb4]
0x47DF91: jmp      0x47e012
0x47DF93: lea      edx, [ebp - 0x10]
0x47DF96: mov      eax, dword ptr [ebp - 4]
0x47DF99: mov      eax, dword ptr [eax + 0x30]
0x47DF9C: call     0x4279d0
0x47DFA1: mov      eax, dword ptr [ebp - 0x10]
0x47DFA4: call     0x403cd4
0x47DFA9: dec      eax
0x47DFAA: jge      0x47dfbc
0x47DFAC: mov      eax, dword ptr [ebp - 4]
0x47DFAF: mov      eax, dword ptr [eax + 0x30]
0x47DFB2: mov      edx, dword ptr [eax]
0x47DFB4: call     dword ptr [edx + 0xb4]
0x47DFBA: jmp      0x47e012
0x47DFBC: lea      edx, [ebp - 0x10]
0x47DFBF: mov      eax, dword ptr [ebp - 4]
0x47DFC2: mov      eax, dword ptr [eax + 0x34]
0x47DFC5: call     0x4279d0
0x47DFCA: mov      eax, dword ptr [ebp - 0x10]
0x47DFCD: call     0x403cd4
0x47DFD2: dec      eax
0x47DFD3: jge      0x47dfe5
0x47DFD5: mov      eax, dword ptr [ebp - 4]
0x47DFD8: mov      eax, dword ptr [eax + 0x34]
0x47DFDB: mov      edx, dword ptr [eax]
0x47DFDD: call     dword ptr [edx + 0xb4]
0x47DFE3: jmp      0x47e012
0x47DFE5: lea      edx, [ebp - 0x10]
0x47DFE8: mov      eax, dword ptr [ebp - 4]
0x47DFEB: mov      eax, dword ptr [eax + 0x1c]
0x47DFEE: call     0x4279d0
0x47DFF3: mov      eax, dword ptr [ebp - 0x10]
0x47DFF6: call     0x403cd4
0x47DFFB: dec      eax
0x47DFFC: jge      0x47e00e
0x47DFFE: mov      eax, dword ptr [ebp - 4]
0x47E001: mov      eax, dword ptr [eax + 0x1c]
0x47E004: mov      edx, dword ptr [eax]
0x47E006: call     dword ptr [edx + 0xb4]
0x47E00C: jmp      0x47e012
0x47E00E: mov      byte ptr [ebp - 5], 1
0x47E012: xor      eax, eax
0x47E014: pop      edx
0x47E015: pop      ecx
0x47E016: pop      ecx
0x47E017: mov      dword ptr fs:[eax], edx
0x47E01A: push     0x47e03c
0x47E01F: lea      eax, [ebp - 0x14]
0x47E022: mov      edx, 2
0x47E027: call     0x403a7c
0x47E02C: lea      eax, [ebp - 0xc]
0x47E02F: call     0x403a58
0x47E034: ret      
0x47E035: jmp      0x403518
0x47E03A: jmp      0x47e01f
0x47E03C: mov      al, byte ptr [ebp - 5]
0x47E03F: mov      esp, ebp
0x47E041: pop      ebp
0x47E042: ret      
0x47E043: nop      
0x47E044: push     ebp
0x47E045: mov      ebp, esp
0x47E047: add      esp, 0xfffffdf8
0x47E04D: push     esi
0x47E04E: push     edi
0x47E04F: xor      edx, edx
0x47E051: mov      dword ptr [ebp - 0x208], edx
0x47E057: mov      dword ptr [ebp - 0x204], edx
0x47E05D: mov      dword ptr [ebp - 4], eax
0x47E060: xor      eax, eax
0x47E062: push     ebp
0x47E063: push     0x47e416
0x47E068: push     dword ptr fs:[eax]
0x47E06B: mov      dword ptr fs:[eax], esp
0x47E06E: mov      eax, dword ptr [ebp - 4]
0x47E071: call     0x47ddf0
0x47E076: test     al, al
0x47E078: je       0x47e3f2
0x47E07E: lea      eax, [ebp - 0x98]
0x47E084: xor      ecx, ecx
0x47E086: mov      edx, 0x94
0x47E08B: call     0x402b54
0x47E090: lea      eax, [ebp - 0xfd]
0x47E096: xor      ecx, ecx
0x47E098: mov      edx, 0x65
0x47E09D: call     0x402b54
0x47E0A2: lea      edx, [ebp - 0x208]
0x47E0A8: mov      eax, dword ptr [ebp - 4]
0x47E0AB: mov      eax, dword ptr [eax + 0x10]
0x47E0AE: call     0x4279d0
0x47E0B3: mov      eax, dword ptr [ebp - 0x208]
0x47E0B9: lea      edx, [ebp - 0x204]
0x47E0BF: call     0x407aa8
0x47E0C4: mov      edx, dword ptr [ebp - 0x204]
0x47E0CA: lea      eax, [ebp - 0x200]
0x47E0D0: mov      ecx, 0xff
0x47E0D5: call     0x403cb0
0x47E0DA: lea      edx, [ebp - 0x200]
0x47E0E0: lea      eax, [ebp - 0x98]
0x47E0E6: mov      cl, 0xa
0x47E0E8: call     0x402a30
0x47E0ED: lea      edx, [ebp - 0x208]
0x47E0F3: mov      eax, dword ptr [ebp - 4]
0x47E0F6: mov      eax, dword ptr [eax + 0x14]
0x47E0F9: call     0x4279d0
0x47E0FE: mov      edx, dword ptr [ebp - 0x208]
0x47E104: lea      eax, [ebp - 0x200]
0x47E10A: mov      ecx, 0xff
0x47E10F: call     0x403cb0
0x47E114: lea      edx, [ebp - 0x200]
0x47E11A: lea      eax, [ebp - 0x8d]
0x47E120: mov      cl, 0xa
0x47E122: call     0x402a30
0x47E127: lea      edx, [ebp - 0x208]
0x47E12D: mov      eax, dword ptr [ebp - 4]
0x47E130: mov      eax, dword ptr [eax + 0x1c]
0x47E133: call     0x4279d0
0x47E138: mov      edx, dword ptr [ebp - 0x208]
0x47E13E: lea      eax, [ebp - 0x200]
0x47E144: mov      ecx, 0xff
0x47E149: call     0x403cb0
0x47E14E: lea      edx, [ebp - 0x200]
0x47E154: lea      eax, [ebp - 0x82]
0x47E15A: mov      cl, 0x14
0x47E15C: call     0x402a30
0x47E161: mov      esi, 0x47e424 ; "650101-1455111"
0x47E166: lea      edi, [ebp - 0x6d]
0x47E169: mov      ecx, 3
0x47E16E: rep movsd dword ptr es:[edi], dword ptr [esi]
0x47E170: movsw    word ptr es:[edi], word ptr [esi]
0x47E172: movsb    byte ptr es:[edi], byte ptr [esi]
0x47E173: lea      edx, [ebp - 0x208]
0x47E179: mov      eax, dword ptr [ebp - 4]
0x47E17C: mov      eax, dword ptr [eax + 0x28]
0x47E17F: call     0x4279d0
0x47E184: mov      edx, dword ptr [ebp - 0x208]
0x47E18A: lea      eax, [ebp - 0x200]
0x47E190: mov      ecx, 0xff
0x47E195: call     0x403cb0
0x47E19A: lea      edx, [ebp - 0x200]
0x47E1A0: lea      eax, [ebp - 0x4f]
0x47E1A3: mov      cl, 0x14
0x47E1A5: call     0x402a30
0x47E1AA: lea      edx, [ebp - 0x208]
0x47E1B0: mov      eax, dword ptr [ebp - 4]
0x47E1B3: mov      eax, dword ptr [eax + 0x2c]
0x47E1B6: call     0x4279d0
0x47E1BB: mov      eax, dword ptr [ebp - 0x208]
0x47E1C1: lea      edx, [ebp - 0x204]
0x47E1C7: call     0x407be4
0x47E1CC: mov      edx, dword ptr [ebp - 0x204]
0x47E1D2: lea      eax, [ebp - 0x200]
0x47E1D8: mov      ecx, 0xff
0x47E1DD: call     0x403cb0
0x47E1E2: lea      edx, [ebp - 0x200]
0x47E1E8: lea      eax, [ebp - 0x3a]
0x47E1EB: mov      cl, 0xc
0x47E1ED: call     0x402a30
0x47E1F2: lea      edx, [ebp - 0x208]
0x47E1F8: mov      eax, dword ptr [ebp - 4]
0x47E1FB: mov      eax, dword ptr [eax + 0x38]
0x47E1FE: call     0x4279d0
0x47E203: mov      edx, dword ptr [ebp - 0x208]
0x47E209: lea      eax, [ebp - 0x200]
0x47E20F: mov      ecx, 0xff
0x47E214: call     0x403cb0
0x47E219: lea      edx, [ebp - 0x200]
0x47E21F: lea      eax, [ebp - 0x5e]
0x47E222: mov      cl, 0xe
0x47E224: call     0x402a30
0x47E229: lea      edx, [ebp - 0x208]
0x47E22F: mov      eax, dword ptr [ebp - 4]
0x47E232: mov      eax, dword ptr [eax + 0x40]
0x47E235: call     0x4279d0
0x47E23A: mov      eax, dword ptr [ebp - 0x208]
0x47E240: lea      edx, [ebp - 0x204]
0x47E246: call     0x407be4
0x47E24B: mov      edx, dword ptr [ebp - 0x204]
0x47E251: lea      eax, [ebp - 0x200]
0x47E257: mov      ecx, 0xff
0x47E25C: call     0x403cb0
0x47E261: lea      edx, [ebp - 0x200]
0x47E267: lea      eax, [ebp - 0x2d]
0x47E26A: mov      cl, 0x28
0x47E26C: call     0x402a30
0x47E271: lea      edx, [ebp - 0x208]
0x47E277: mov      eax, dword ptr [ebp - 4]
0x47E27A: mov      eax, dword ptr [eax + 0x30]
0x47E27D: call     0x4279d0
0x47E282: mov      edx, dword ptr [ebp - 0x208]
0x47E288: lea      eax, [ebp - 0x200]
0x47E28E: mov      ecx, 0xff
0x47E293: call     0x403cb0
0x47E298: lea      edx, [ebp - 0x200]
0x47E29E: lea      eax, [ebp - 0xfd]
0x47E2A4: mov      cl, 0x14
0x47E2A6: call     0x402a30
0x47E2AB: lea      edx, [ebp - 0x208]
0x47E2B1: mov      eax, dword ptr [ebp - 4]
0x47E2B4: mov      eax, dword ptr [eax + 0x34]
0x47E2B7: call     0x4279d0
0x47E2BC: mov      eax, dword ptr [ebp - 0x208]
0x47E2C2: lea      edx, [ebp - 0x204]
0x47E2C8: call     0x407be4
0x47E2CD: mov      edx, dword ptr [ebp - 0x204]
0x47E2D3: lea      eax, [ebp - 0x200]
0x47E2D9: mov      ecx, 0xff
0x47E2DE: call     0x403cb0
0x47E2E3: lea      edx, [ebp - 0x200]
0x47E2E9: lea      eax, [ebp - 0xe8]
0x47E2EF: mov      cl, 0xc
0x47E2F1: call     0x402a30
0x47E2F6: lea      edx, [ebp - 0x208]
0x47E2FC: mov      eax, dword ptr [ebp - 4]
0x47E2FF: mov      eax, dword ptr [eax + 0x24]
0x47E302: call     0x4279d0
0x47E307: mov      edx, dword ptr [ebp - 0x208]
0x47E30D: lea      eax, [ebp - 0x200]
0x47E313: mov      ecx, 0xff
0x47E318: call     0x403cb0
0x47E31D: lea      edx, [ebp - 0x200]
0x47E323: lea      eax, [ebp - 0xdb]
0x47E329: mov      cl, 0xa
0x47E32B: call     0x402a30
0x47E330: lea      edx, [ebp - 0x208]
0x47E336: mov      eax, dword ptr [ebp - 4]
0x47E339: mov      eax, dword ptr [eax + 0x3c]
0x47E33C: call     0x4279d0
0x47E341: mov      edx, dword ptr [ebp - 0x208]
0x47E347: lea      eax, [ebp - 0x200]
0x47E34D: mov      ecx, 0xff
0x47E352: call     0x403cb0
0x47E357: lea      edx, [ebp - 0x200]
0x47E35D: lea      eax, [ebp - 0xd0]
0x47E363: mov      cl, 0xd
0x47E365: call     0x402a30
0x47E36A: mov      eax, dword ptr [ebp - 4]
0x47E36D: lea      esi, [ebp - 0x98]
0x47E373: lea      edi, [eax + 0x62]
0x47E376: mov      ecx, 0x25
0x47E37B: rep movsd dword ptr es:[edi], dword ptr [esi]
0x47E37D: mov      eax, dword ptr [ebp - 4]
0x47E380: mov      byte ptr [eax + 0x62], 0
0x47E384: mov      eax, dword ptr [ebp - 4]
0x47E387: mov      byte ptr [eax + 0x6d], 0
0x47E38B: mov      eax, dword ptr [ebp - 4]
0x47E38E: lea      esi, [ebp - 0xfd]
0x47E394: lea      edi, [eax + 0xf6]
0x47E39A: mov      ecx, 0x19
0x47E39F: rep movsd dword ptr es:[edi], dword ptr [esi]
0x47E3A1: movsb    byte ptr es:[edi], byte ptr [esi]
0x47E3A2: mov      eax, dword ptr [ebp - 4]
0x47E3A5: cmp      byte ptr [eax + 0x164], 0
0x47E3AC: jne      0x47e3c8
0x47E3AE: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47E3B3: mov      eax, dword ptr [eax]
0x47E3B5: lea      ecx, [ebp - 0xfd]
0x47E3BB: lea      edx, [ebp - 0x98]
0x47E3C1: call     0x464c90
0x47E3C6: jmp      0x47e3e0
0x47E3C8: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47E3CD: mov      eax, dword ptr [eax]
0x47E3CF: lea      ecx, [ebp - 0xfd]
0x47E3D5: lea      edx, [ebp - 0x98]
0x47E3DB: call     0x464dac
0x47E3E0: mov      eax, dword ptr [ebp - 4]
0x47E3E3: mov      byte ptr [eax + 0x164], 0
0x47E3EA: mov      eax, dword ptr [ebp - 4]
0x47E3ED: call     0x47e434
0x47E3F2: xor      eax, eax
0x47E3F4: pop      edx
0x47E3F5: pop      ecx
0x47E3F6: pop      ecx
0x47E3F7: mov      dword ptr fs:[eax], edx
0x47E3FA: push     0x47e41d
0x47E3FF: lea      eax, [ebp - 0x208]
0x47E405: call     0x403a58
0x47E40A: lea      eax, [ebp - 0x204]
0x47E410: call     0x403a58
0x47E415: ret      
0x47E416: jmp      0x403518
0x47E41B: jmp      0x47e3ff
0x47E41D: pop      edi
0x47E41E: pop      esi
0x47E41F: mov      esp, ebp
0x47E421: pop      ebp
0x47E422: ret      
0x47E423: add      byte ptr [esi], cl
0x47E425: xor      eax, 0x31303130
0x47E42B: sub      eax, 0x35353431
0x47E430: xor      dword ptr [ecx], esi
0x47E432: xor      dword ptr [eax], eax
0x47E434: push     ebp
0x47E435: mov      ebp, esp
0x47E437: push     ecx
0x47E438: mov      dword ptr [ebp - 4], eax
0x47E43B: mov      eax, dword ptr [ebp - 4]
0x47E43E: cmp      byte ptr [eax + 0x164], 0
0x47E445: jne      0x47e451
0x47E447: xor      edx, edx
0x47E449: mov      eax, dword ptr [ebp - 4]
0x47E44C: call     0x47d864
0x47E451: pop      ecx
0x47E452: pop      ebp
0x47E453: ret      
0x47E454: push     ebp
0x47E455: mov      ebp, esp
0x47E457: xor      ecx, ecx
0x47E459: push     ecx
0x47E45A: push     ecx
0x47E45B: push     ecx
0x47E45C: push     ecx
0x47E45D: push     ecx
0x47E45E: push     ecx
0x47E45F: mov      dword ptr [ebp - 4], eax
0x47E462: xor      eax, eax
0x47E464: push     ebp
0x47E465: push     0x47e533
0x47E46A: push     dword ptr fs:[eax]
0x47E46D: mov      dword ptr fs:[eax], esp
0x47E470: lea      edx, [ebp - 0x14]
0x47E473: mov      eax, dword ptr [ebp - 4]
0x47E476: mov      eax, dword ptr [eax + 0x4c]
0x47E479: call     0x4279d0
0x47E47E: mov      eax, dword ptr [ebp - 0x14]
0x47E481: push     eax
0x47E482: lea      edx, [ebp - 0x18]
0x47E485: mov      eax, dword ptr [ebp - 4]
0x47E488: mov      eax, dword ptr [eax + 0x50]
0x47E48B: call     0x4279d0
0x47E490: mov      edx, dword ptr [ebp - 0x18]
0x47E493: pop      eax
0x47E494: call     0x403de4
0x47E499: jne      0x47e4e5
0x47E49B: lea      edx, [ebp - 8]
0x47E49E: mov      eax, dword ptr [ebp - 4]
0x47E4A1: mov      eax, dword ptr [eax + 0x44]
0x47E4A4: call     0x4279d0
0x47E4A9: lea      edx, [ebp - 0xc]
0x47E4AC: mov      eax, dword ptr [ebp - 4]
0x47E4AF: mov      eax, dword ptr [eax + 0x48]
0x47E4B2: call     0x4279d0
0x47E4B7: lea      edx, [ebp - 0x10]
0x47E4BA: mov      eax, dword ptr [ebp - 4]
0x47E4BD: mov      eax, dword ptr [eax + 0x4c]
0x47E4C0: call     0x4279d0
0x47E4C5: mov      eax, dword ptr [ebp - 0x10]
0x47E4C8: push     eax
0x47E4C9: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47E4CE: mov      eax, dword ptr [eax]
0x47E4D0: mov      ecx, dword ptr [ebp - 0xc]
0x47E4D3: mov      edx, dword ptr [ebp - 8]
0x47E4D6: call     0x464f64
0x47E4DB: mov      eax, dword ptr [ebp - 4]
0x47E4DE: call     0x47e560
0x47E4E3: jmp      0x47e50b
0x47E4E5: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47E4EA: mov      eax, dword ptr [eax]
0x47E4EC: mov      cx, word ptr [0x47e540]
0x47E4F3: mov      edx, 0x47e54c ; "密码确认不正确。"
0x47E4F8: call     0x486df0
0x47E4FD: mov      eax, dword ptr [ebp - 4]
0x47E500: mov      eax, dword ptr [eax + 0x4c]
0x47E503: mov      edx, dword ptr [eax]
0x47E505: call     dword ptr [edx + 0xb4]
0x47E50B: xor      eax, eax
0x47E50D: pop      edx
0x47E50E: pop      ecx
0x47E50F: pop      ecx
0x47E510: mov      dword ptr fs:[eax], edx
0x47E513: push     0x47e53a
0x47E518: lea      eax, [ebp - 0x18]
0x47E51B: mov      edx, 2
0x47E520: call     0x403a7c
0x47E525: lea      eax, [ebp - 0x10]
0x47E528: mov      edx, 3
0x47E52D: call     0x403a7c
0x47E532: ret      
0x47E533: jmp      0x403518
0x47E538: jmp      0x47e518
0x47E53A: mov      esp, ebp
0x47E53C: pop      ebp
0x47E53D: ret      
0x47E53E: add      byte ptr [eax], al
0x47E540: add      al, 0
0x47E542: add      byte ptr [eax], al
0x47E54C: [STR len=16] "密码确认不正确。"
0x47E560: push     ebp
0x47E561: mov      ebp, esp
0x47E563: push     ecx
0x47E564: mov      dword ptr [ebp - 4], eax
0x47E567: xor      edx, edx
0x47E569: mov      eax, dword ptr [ebp - 4]
0x47E56C: call     0x47d864
0x47E571: pop      ecx
0x47E572: pop      ebp
0x47E573: ret      
0x47E574: push     ebp
0x47E575: mov      ebp, esp
0x47E577: add      esp, -0x10
0x47E57A: test     dl, dl
0x47E57C: je       0x47e586
0x47E57E: add      esp, -0x10
0x47E581: call     0x4031a4
0x47E586: mov      byte ptr [ebp - 5], dl
0x47E589: mov      dword ptr [ebp - 4], eax
0x47E58C: mov      eax, dword ptr [ebp - 4]
0x47E58F: mov      byte ptr [eax + 0xc], 0
0x47E593: mov      eax, dword ptr [ebp - 4]
0x47E596: add      eax, 0x18
0x47E599: xor      ecx, ecx
0x47E59B: mov      edx, 0x70
0x47E5A0: call     0x402b54
0x47E5A5: mov      eax, dword ptr [ebp - 4]
0x47E5A8: mov      byte ptr [eax + 0x35], 1
0x47E5AC: mov      eax, dword ptr [ebp - 4]
0x47E5AF: mov      byte ptr [eax + 0x6d], 1
0x47E5B3: mov      eax, dword ptr [ebp - 4]
0x47E5B6: xor      edx, edx
0x47E5B8: mov      dword ptr [eax + 0x14], edx
0x47E5BB: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47E5C0: mov      eax, dword ptr [eax]
0x47E5C2: mov      ecx, dword ptr [eax + 4]
0x47E5C5: mov      dl, 1
0x47E5C7: mov      eax, dword ptr [0x41f814]
0x47E5CC: call     0x4219b8
0x47E5D1: mov      edx, dword ptr [ebp - 4]
0x47E5D4: mov      dword ptr [edx + 0x10], eax
0x47E5D7: mov      eax, dword ptr [ebp - 4]
0x47E5DA: mov      eax, dword ptr [eax + 0x10]
0x47E5DD: mov      dword ptr [ebp - 0xc], eax
0x47E5E0: mov      edx, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47E5E6: mov      edx, dword ptr [edx]
0x47E5E8: mov      eax, dword ptr [ebp - 0xc]
0x47E5EB: mov      ecx, dword ptr [eax]
0x47E5ED: call     dword ptr [ecx + 0x64]
0x47E5F0: mov      edx, 0x10
0x47E5F5: mov      eax, dword ptr [ebp - 0xc]
0x47E5F8: call     0x4271fc
0x47E5FD: mov      edx, 0x89
0x47E602: mov      eax, dword ptr [ebp - 0xc]
0x47E605: call     0x4271d8
0x47E60A: xor      edx, edx
0x47E60C: mov      eax, dword ptr [ebp - 0xc]
0x47E60F: call     0x421a94
0x47E614: xor      edx, edx
0x47E616: mov      eax, dword ptr [ebp - 0xc]
0x47E619: call     0x427b5c
0x47E61E: mov      edx, 0xffffff
0x47E623: mov      eax, dword ptr [ebp - 0xc]
0x47E626: mov      eax, dword ptr [eax + 0x58]
0x47E629: call     0x4173fc
0x47E62E: mov      eax, dword ptr [0x4a3e30] ; *0x4A3E30->0x4A2CE0
0x47E633: mov      al, byte ptr [eax]
0x47E635: mov      edx, dword ptr [ebp - 0xc]
0x47E638: mov      byte ptr [edx + 0x148], al
0x47E63E: mov      edx, 0xe
0x47E643: mov      eax, dword ptr [ebp - 0xc]
0x47E646: call     0x421ae0
0x47E64B: xor      edx, edx
0x47E64D: mov      eax, dword ptr [ebp - 0xc]
0x47E650: call     0x4278c0
0x47E655: mov      eax, dword ptr [ebp - 4]
0x47E658: mov      edx, dword ptr [ebp - 0xc]
0x47E65B: mov      dword ptr [edx + 0x1bc], eax
0x47E661: mov      dword ptr [edx + 0x1b8], 0x47eef4
0x47E66B: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47E670: mov      eax, dword ptr [eax]
0x47E672: mov      ecx, dword ptr [eax + 4]
0x47E675: mov      dl, 1
0x47E677: mov      eax, dword ptr [0x444e18]
0x47E67C: call     0x44544c
0x47E681: mov      edx, dword ptr [ebp - 4]
0x47E684: mov      dword ptr [edx + 8], eax
0x47E687: mov      eax, dword ptr [ebp - 4]
0x47E68A: mov      eax, dword ptr [eax + 8]
0x47E68D: mov      dword ptr [ebp - 0x10], eax
0x47E690: mov      eax, dword ptr [ebp - 4]
0x47E693: push     eax
0x47E694: push     0x47e784
0x47E699: mov      eax, dword ptr [ebp - 0x10]
0x47E69C: call     0x4455f4
0x47E6A1: mov      edx, 1
0x47E6A6: mov      eax, dword ptr [ebp - 0x10]
0x47E6A9: call     0x4455e4
0x47E6AE: xor      edx, edx
0x47E6B0: mov      eax, dword ptr [ebp - 0x10]
0x47E6B3: call     0x4455d4
0x47E6B8: mov      cl, 3
0x47E6BA: xor      edx, edx
0x47E6BC: mov      eax, dword ptr [ebp - 4]
0x47E6BF: call     0x47ad80
0x47E6C4: mov      eax, dword ptr [ebp - 4]
0x47E6C7: cmp      byte ptr [ebp - 5], 0
0x47E6CB: je       0x47e6dc
0x47E6CD: call     0x4031fc
0x47E6D2: pop      dword ptr fs:[0]
0x47E6D9: add      esp, 0xc
0x47E6DC: mov      eax, dword ptr [ebp - 4]
0x47E6DF: mov      esp, ebp
0x47E6E1: pop      ebp
0x47E6E2: ret      
0x47E6E3: nop      
0x47E6E4: push     ebp
0x47E6E5: mov      ebp, esp
0x47E6E7: add      esp, -8
0x47E6EA: call     0x403204
0x47E6EF: mov      byte ptr [ebp - 5], dl
0x47E6F2: mov      dword ptr [ebp - 4], eax
0x47E6F5: mov      dl, byte ptr [ebp - 5]
0x47E6F8: and      dl, 0xfc
0x47E6FB: mov      eax, dword ptr [ebp - 4]
0x47E6FE: call     0x402e9c
0x47E703: cmp      byte ptr [ebp - 5], 0
0x47E707: jle      0x47e711
0x47E709: mov      eax, dword ptr [ebp - 4]
0x47E70C: call     0x4031f4
0x47E711: pop      ecx
0x47E712: pop      ecx
0x47E713: pop      ebp
0x47E714: ret      
0x47E715: lea      eax, [eax]
0x47E718: push     ebp
0x47E719: mov      ebp, esp
0x47E71B: push     ecx
0x47E71C: mov      dword ptr [ebp - 4], eax
0x47E71F: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47E724: mov      eax, dword ptr [eax]
0x47E726: mov      eax, dword ptr [eax + 0x314]
0x47E72C: mov      dl, 1
0x47E72E: call     0x458214
0x47E733: mov      dl, 1
0x47E735: mov      eax, dword ptr [ebp - 4]
0x47E738: mov      eax, dword ptr [eax + 8]
0x47E73B: call     0x4455d4
0x47E740: mov      edx, 1
0x47E745: mov      eax, dword ptr [ebp - 4]
0x47E748: mov      eax, dword ptr [eax + 8]
0x47E74B: call     0x4455e4
0x47E750: pop      ecx
0x47E751: pop      ebp
0x47E752: ret      
0x47E753: nop      
0x47E754: push     ebp
0x47E755: mov      ebp, esp
0x47E757: push     ecx
0x47E758: mov      dword ptr [ebp - 4], eax
0x47E75B: call     0x495928
0x47E760: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47E765: mov      eax, dword ptr [eax]
0x47E767: mov      eax, dword ptr [eax + 0x314]
0x47E76D: xor      edx, edx
0x47E76F: call     0x458214
0x47E774: xor      edx, edx
0x47E776: mov      eax, dword ptr [ebp - 4]
0x47E779: mov      eax, dword ptr [eax + 8]
0x47E77C: call     0x4455d4
0x47E781: pop      ecx
0x47E782: pop      ebp
0x47E783: ret      
0x47E784: push     ebp
0x47E785: mov      ebp, esp
0x47E787: add      esp, -8
0x47E78A: mov      dword ptr [ebp - 8], edx
0x47E78D: mov      dword ptr [ebp - 4], eax
0x47E790: mov      eax, 0x47e7b4 ; "wav\sellect-loop2.wav"
0x47E795: call     0x49588c
0x47E79A: xor      edx, edx
0x47E79C: mov      eax, dword ptr [ebp - 4]
0x47E79F: mov      eax, dword ptr [eax + 8]
0x47E7A2: call     0x4455d4
0x47E7A7: pop      ecx
0x47E7A8: pop      ecx
0x47E7A9: pop      ebp
0x47E7AA: ret      
0x47E7AB: add      bh, bh
0x47E7AF: call     dword ptr [0x77000000]
0x47E7B5: popal    
0x47E7B6: jbe      0x47e814
0x47E7B8: jae      0x47e81f
0x47E7BA: insb     byte ptr es:[edi], dx
0x47E7BB: insb     byte ptr es:[edi], dx
0x47E7BC: arpl     word ptr gs:[ebp + ebp + 0x6c], si
0x47E7C1: outsd    dx, dword ptr [esi]
0x47E7C2: outsd    dx, dword ptr [esi]
0x47E7C3: jo       0x47e7f7
0x47E7C5: ja       0x47e829
0x47E7C8: jbe      0x47e7ca
0x47E7CA: add      byte ptr [eax], al
0x47E7CC: push     ebp
0x47E7CD: mov      ebp, esp
0x47E7CF: push     ecx
0x47E7D0: mov      dword ptr [ebp - 4], eax
0x47E7D3: mov      eax, dword ptr [ebp - 4]
0x47E7D6: cmp      byte ptr [eax + 0x34], 0
0x47E7DA: jne      0x47e83d
0x47E7DC: mov      eax, dword ptr [ebp - 4]
0x47E7DF: cmp      byte ptr [eax + 0x18], 0
0x47E7E3: je       0x47e83d
0x47E7E5: mov      eax, dword ptr [ebp - 4]
0x47E7E8: mov      byte ptr [eax + 0x34], 1
0x47E7EC: mov      eax, dword ptr [ebp - 4]
0x47E7EF: mov      byte ptr [eax + 0x6c], 0
0x47E7F3: mov      eax, dword ptr [ebp - 4]
0x47E7F6: mov      byte ptr [eax + 0x36], 1
0x47E7FA: mov      eax, dword ptr [ebp - 4]
0x47E7FD: xor      edx, edx
0x47E7FF: mov      dword ptr [eax + 0x38], edx
0x47E802: mov      eax, dword ptr [ebp - 4]
0x47E805: xor      edx, edx
0x47E807: mov      dword ptr [eax + 0x3c], edx
0x47E80A: mov      eax, dword ptr [ebp - 4]
0x47E80D: xor      edx, edx
0x47E80F: mov      dword ptr [eax + 0x40], edx
0x47E812: call     0x406154
0x47E817: mov      edx, dword ptr [ebp - 4]
0x47E81A: mov      dword ptr [edx + 0x44], eax
0x47E81D: call     0x406154
0x47E822: mov      edx, dword ptr [ebp - 4]
0x47E825: mov      dword ptr [edx + 0x48], eax
0x47E828: call     0x406154
0x47E82D: mov      edx, dword ptr [ebp - 4]
0x47E830: mov      dword ptr [edx + 0x4c], eax
0x47E833: mov      eax, 0x65
0x47E838: call     0x495794
0x47E83D: pop      ecx
0x47E83E: pop      ebp
0x47E83F: ret      
0x47E840: push     ebp
0x47E841: mov      ebp, esp
0x47E843: push     ecx
0x47E844: mov      dword ptr [ebp - 4], eax
0x47E847: mov      eax, dword ptr [ebp - 4]
0x47E84A: cmp      byte ptr [eax + 0x6c], 0
0x47E84E: jne      0x47e8b7
0x47E850: mov      eax, dword ptr [ebp - 4]
0x47E853: cmp      byte ptr [eax + 0x50], 0
0x47E857: je       0x47e8b7
0x47E859: mov      eax, dword ptr [ebp - 4]
0x47E85C: mov      byte ptr [eax + 0x6c], 1
0x47E860: mov      eax, dword ptr [ebp - 4]
0x47E863: mov      byte ptr [eax + 0x34], 0
0x47E867: mov      eax, dword ptr [ebp - 4]
0x47E86A: mov      byte ptr [eax + 0x6e], 1
0x47E86E: mov      eax, dword ptr [ebp - 4]
0x47E871: xor      edx, edx
0x47E873: mov      dword ptr [eax + 0x70], edx
0x47E876: mov      eax, dword ptr [ebp - 4]
0x47E879: xor      edx, edx
0x47E87B: mov      dword ptr [eax + 0x74], edx
0x47E87E: mov      eax, dword ptr [ebp - 4]
0x47E881: xor      edx, edx
0x47E883: mov      dword ptr [eax + 0x78], edx
0x47E886: call     0x406154
0x47E88B: mov      edx, dword ptr [ebp - 4]
0x47E88E: mov      dword ptr [edx + 0x7c], eax
0x47E891: call     0x406154
0x47E896: mov      edx, dword ptr [ebp - 4]
0x47E899: mov      dword ptr [edx + 0x80], eax
0x47E89F: call     0x406154
0x47E8A4: mov      edx, dword ptr [ebp - 4]
0x47E8A7: mov      dword ptr [edx + 0x84], eax
0x47E8AD: mov      eax, 0x65
0x47E8B2: call     0x495794
0x47E8B7: pop      ecx
0x47E8B8: pop      ebp
0x47E8B9: ret      
0x47E8BA: mov      eax, eax
0x47E8BC: push     ebp
0x47E8BD: mov      ebp, esp
0x47E8BF: add      esp, -8
0x47E8C2: xor      edx, edx
0x47E8C4: mov      dword ptr [ebp - 8], edx
0x47E8C7: mov      dword ptr [ebp - 4], eax
0x47E8CA: xor      eax, eax
0x47E8CC: push     ebp
0x47E8CD: push     0x47e98c
0x47E8D2: push     dword ptr fs:[eax]
0x47E8D5: mov      dword ptr fs:[eax], esp
0x47E8D8: lea      eax, [ebp - 8]
0x47E8DB: call     0x403a58
0x47E8E0: mov      eax, dword ptr [ebp - 4]
0x47E8E3: cmp      byte ptr [eax + 0x18], 0
0x47E8E7: je       0x47e900
0x47E8E9: mov      eax, dword ptr [ebp - 4]
0x47E8EC: cmp      byte ptr [eax + 0x34], 0
0x47E8F0: je       0x47e900
0x47E8F2: lea      eax, [ebp - 8]
0x47E8F5: mov      edx, dword ptr [ebp - 4]
0x47E8F8: add      edx, 0x1c
0x47E8FB: call     0x403c78
0x47E900: mov      eax, dword ptr [ebp - 4]
0x47E903: cmp      byte ptr [eax + 0x50], 0
0x47E907: je       0x47e920
0x47E909: mov      eax, dword ptr [ebp - 4]
0x47E90C: cmp      byte ptr [eax + 0x6c], 0
0x47E910: je       0x47e920
0x47E912: lea      eax, [ebp - 8]
0x47E915: mov      edx, dword ptr [ebp - 4]
0x47E918: add      edx, 0x54
0x47E91B: call     0x403c78
0x47E920: cmp      dword ptr [ebp - 8], 0
0x47E924: je       0x47e95e
0x47E926: mov      eax, dword ptr [0x4a3ec8] ; *0x4A3EC8->0x4F9468
0x47E92B: cmp      byte ptr [eax], 0
0x47E92E: jne      0x47e94d
0x47E930: mov      eax, dword ptr [0x4a4074] ; *0x4A4074->0x4F9469
0x47E935: cmp      byte ptr [eax], 0
0x47E938: jne      0x47e94d
0x47E93A: mov      eax, dword ptr [0x4a3f98] ; *0x4A3F98->0x4F9470
0x47E93F: mov      byte ptr [eax], 1
0x47E942: mov      eax, dword ptr [0x4a42d8] ; *0x4A42D8->0x4F946C
0x47E947: mov      dword ptr [eax], 0x1d
0x47E94D: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47E952: mov      eax, dword ptr [eax]
0x47E954: mov      edx, dword ptr [ebp - 8]
0x47E957: call     0x4652d0
0x47E95C: jmp      0x47e976
0x47E95E: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47E963: mov      eax, dword ptr [eax]
0x47E965: mov      cx, word ptr [0x47e998]
0x47E96C: mov      edx, 0x47e9a4 ; "一开始你应该创建一个新角色。\如果你选择 < 创建角色>你就能建立一个新角色了。"
0x47E971: call     0x486df0
0x47E976: xor      eax, eax
0x47E978: pop      edx
0x47E979: pop      ecx
0x47E97A: pop      ecx
0x47E97B: mov      dword ptr fs:[eax], edx
0x47E97E: push     0x47e993
0x47E983: lea      eax, [ebp - 8]
0x47E986: call     0x403a58
0x47E98B: ret      
0x47E98C: jmp      0x403518
0x47E991: jmp      0x47e983
0x47E993: pop      ecx
0x47E994: pop      ecx
0x47E995: pop      ebp
0x47E996: ret      
0x47E997: add      byte ptr [eax + eax], al
0x47E99A: add      byte ptr [eax], al
0x47E9A4: [STR len=75] "一开始你应该创建一个新角色。\如果你选择 < 创建角色>你就能建立一个新角色了。"
0x47E9F0: push     ebp
0x47E9F1: mov      ebp, esp
0x47E9F3: push     ecx
0x47E9F4: mov      dword ptr [ebp - 4], eax
0x47E9F7: mov      eax, dword ptr [ebp - 4]
0x47E9FA: cmp      byte ptr [eax + 0x18], 0
0x47E9FE: je       0x47ea09
0x47EA00: mov      eax, dword ptr [ebp - 4]
0x47EA03: cmp      byte ptr [eax + 0x50], 0
0x47EA07: jne      0x47ea2e
0x47EA09: mov      eax, dword ptr [ebp - 4]
0x47EA0C: cmp      byte ptr [eax + 0x18], 0
0x47EA10: jne      0x47ea1e
0x47EA12: xor      edx, edx
0x47EA14: mov      eax, dword ptr [ebp - 4]
0x47EA17: call     0x47edac
0x47EA1C: jmp      0x47ea46
0x47EA1E: mov      edx, 1
0x47EA23: mov      eax, dword ptr [ebp - 4]
0x47EA26: call     0x47edac
0x47EA2B: pop      ecx
0x47EA2C: pop      ebp
0x47EA2D: ret      
0x47EA2E: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47EA33: mov      eax, dword ptr [eax]
0x47EA35: mov      cx, word ptr [0x47ea4c]
0x47EA3C: mov      edx, 0x47ea58 ; "你可以为每个单独的帐户创建两个角色。"
0x47EA41: call     0x486df0
0x47EA46: pop      ecx
0x47EA47: pop      ebp
0x47EA48: ret      
0x47EA49: add      byte ptr [eax], al
0x47EA4B: add      byte ptr [eax + eax], al
0x47EA4E: add      byte ptr [eax], al
0x47EA58: [STR len=36] "你可以为每个单独的帐户创建两个角色。"
0x47EA80: push     ebp
0x47EA81: mov      ebp, esp
0x47EA83: add      esp, 0xffffff78
0x47EA89: xor      edx, edx
0x47EA8B: mov      dword ptr [ebp - 0xc], edx
0x47EA8E: mov      dword ptr [ebp - 4], eax
0x47EA91: xor      eax, eax
0x47EA93: push     ebp
0x47EA94: push     0x47ebcb
0x47EA99: push     dword ptr fs:[eax]
0x47EA9C: mov      dword ptr fs:[eax], esp
0x47EA9F: xor      eax, eax
0x47EAA1: mov      dword ptr [ebp - 8], eax
0x47EAA4: mov      eax, dword ptr [ebp - 4]
0x47EAA7: cmp      byte ptr [eax + 0x18], 0
0x47EAAB: je       0x47eabb
0x47EAAD: mov      eax, dword ptr [ebp - 4]
0x47EAB0: cmp      byte ptr [eax + 0x34], 0
0x47EAB4: je       0x47eabb
0x47EAB6: xor      eax, eax
0x47EAB8: mov      dword ptr [ebp - 8], eax
0x47EABB: mov      eax, dword ptr [ebp - 4]
0x47EABE: cmp      byte ptr [eax + 0x50], 0
0x47EAC2: je       0x47ead4
0x47EAC4: mov      eax, dword ptr [ebp - 4]
0x47EAC7: cmp      byte ptr [eax + 0x6c], 0
0x47EACB: je       0x47ead4
0x47EACD: mov      dword ptr [ebp - 8], 1
0x47EAD4: mov      eax, dword ptr [ebp - 8]
0x47EAD7: mov      edx, eax
0x47EAD9: shl      eax, 3
0x47EADC: sub      eax, edx
0x47EADE: mov      edx, dword ptr [ebp - 4]
0x47EAE1: cmp      byte ptr [edx + eax*8 + 0x18], 0
0x47EAE6: je       0x47ebb5
0x47EAEC: mov      eax, dword ptr [ebp - 8]
0x47EAEF: mov      edx, eax
0x47EAF1: shl      eax, 3
0x47EAF4: sub      eax, edx
0x47EAF6: mov      edx, dword ptr [ebp - 4]
0x47EAF9: cmp      byte ptr [edx + eax*8 + 0x35], 0
0x47EAFE: jne      0x47ebb5
0x47EB04: mov      eax, dword ptr [ebp - 8]
0x47EB07: mov      edx, eax
0x47EB09: shl      eax, 3
0x47EB0C: sub      eax, edx
0x47EB0E: mov      edx, dword ptr [ebp - 4]
0x47EB11: cmp      byte ptr [edx + eax*8 + 0x1c], 0
0x47EB16: je       0x47ebb5
0x47EB1C: mov      edx, 0x47ebd8 ; """
0x47EB21: lea      eax, [ebp - 0x1c]
0x47EB24: call     0x402a14
0x47EB29: mov      eax, dword ptr [ebp - 8]
0x47EB2C: mov      edx, eax
0x47EB2E: shl      eax, 3
0x47EB31: sub      eax, edx
0x47EB33: mov      edx, dword ptr [ebp - 4]
0x47EB36: lea      edx, [edx + eax*8 + 0x1c]
0x47EB3A: lea      eax, [ebp - 0x1c]
0x47EB3D: mov      cl, 0xf
0x47EB3F: call     0x4029e4
0x47EB44: lea      edx, [ebp - 0x1c]
0x47EB47: lea      eax, [ebp - 0x88]
0x47EB4D: call     0x402a14
0x47EB52: mov      edx, 0x47ebdc
0x47EB57: lea      eax, [ebp - 0x88]
0x47EB5D: mov      cl, 0x69
0x47EB5F: call     0x4029e4
0x47EB64: lea      edx, [ebp - 0x88]
0x47EB6A: lea      eax, [ebp - 0xc]
0x47EB6D: call     0x403c78
0x47EB72: mov      edx, dword ptr [ebp - 0xc]
0x47EB75: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47EB7A: mov      eax, dword ptr [eax]
0x47EB7C: mov      cx, word ptr [0x47ec38]
0x47EB83: call     0x486df0
0x47EB88: cmp      eax, 6
0x47EB8B: jne      0x47ebb5
0x47EB8D: lea      eax, [ebp - 0xc]
0x47EB90: mov      edx, dword ptr [ebp - 8]
0x47EB93: mov      ecx, edx
0x47EB95: shl      edx, 3
0x47EB98: sub      edx, ecx
0x47EB9A: mov      ecx, dword ptr [ebp - 4]
0x47EB9D: lea      edx, [ecx + edx*8 + 0x1c]
0x47EBA1: call     0x403c78
0x47EBA6: mov      edx, dword ptr [ebp - 0xc]
0x47EBA9: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47EBAE: mov      eax, dword ptr [eax]
0x47EBB0: call     0x465234
0x47EBB5: xor      eax, eax
0x47EBB7: pop      edx
0x47EBB8: pop      ecx
0x47EBB9: pop      ecx
0x47EBBA: mov      dword ptr fs:[eax], edx
0x47EBBD: push     0x47ebd2
0x47EBC2: lea      eax, [ebp - 0xc]
0x47EBC5: call     0x403a58
0x47EBCA: ret      
0x47EBCB: jmp      0x403518
0x47EBD0: jmp      0x47ebc2
0x47EBD2: mov      esp, ebp
0x47EBD4: pop      ebp
0x47EBD5: ret      
0x47EBD6: add      byte ptr [eax], al
0x47EBD8: add      dword ptr [edx], esp
0x47EBDA: add      byte ptr [eax], al
0x47EBDC: pop      edx
0x47EBDD: and      ah, byte ptr [eax]
0x47EBDF: leave    
0x47EBE0: mov      esi, 0xc4b5fdb3
0x47EBE5: mov      ebp, 0xcaabc9c7
0x47EBEB: mov      dl, 0xbb
0x47EBEE: fdiv     qword ptr [ecx - 0x47294445]
0x47EBF4: mov      ah, 0xb5
0x47EBF6: les      esp, ptr [ecx - 0x442da35d]
0x47EBFC: mov      dh, 0xce
0x47EBFE: retf     0xbcb1
0x47EC01: in       al, 0xc4
0x47EC03: fisub    dword ptr [ebx - 0x421c3b54]
0x47EC09: stosd    dword ptr es:[edi], eax
0x47EC0A: mov      dl, 0xbb
0x47EC0D: fmul     st(2), st(0)
0x47EC0F: mov      ecx, 0xe0cfc3d3
0x47EC14: int      0xac
0x47EC16: mov      ch, 0xc4
0x47EC18: mov      ebp, 0xc3abc9c7
0x47EC1D: sti      
0x47EC1E: mov      eax, dword ptr [0xe3c45ca3]
0x47EC23: aad      0xe6
0x47EC25: mov      ch, 0xc4
0x47EC27: iretd    
0x47EC28: jmp      0x47ebfc
0x47EC2A: stosb    byte ptr es:[edi], al
0x47EC2B: leave    
0x47EC2C: mov      esi, 0xc7bdfdb3
0x47EC31: leave    
0x47EC32: stosd    dword ptr es:[edi], eax
0x47EC33: ret      0xa3f0
0x47EC36: mov      edi, 0xb00
0x47EC3B: add      byte ptr [ebp - 0x75], dl
0x47EC3E: in       al, dx
0x47EC3F: push     ecx
0x47EC40: mov      dword ptr [ebp - 4], eax
0x47EC43: pop      ecx
0x47EC44: pop      ebp
0x47EC45: ret      
0x47EC46: mov      eax, eax
0x47EC48: push     ebp
0x47EC49: mov      ebp, esp
0x47EC4B: push     ecx
0x47EC4C: mov      dword ptr [ebp - 4], eax
0x47EC4F: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47EC54: mov      eax, dword ptr [eax]
0x47EC56: call     0x4405bc
0x47EC5B: pop      ecx
0x47EC5C: pop      ebp
0x47EC5D: ret      
0x47EC5E: mov      eax, eax
0x47EC60: push     ebp
0x47EC61: mov      ebp, esp
0x47EC63: push     ecx
0x47EC64: mov      dword ptr [ebp - 4], eax
0x47EC67: mov      eax, dword ptr [ebp - 4]
0x47EC6A: add      eax, 0x18
0x47EC6D: xor      ecx, ecx
0x47EC6F: mov      edx, 0x70
0x47EC74: call     0x402b54
0x47EC79: mov      eax, dword ptr [ebp - 4]
0x47EC7C: mov      byte ptr [eax + 0x35], 0
0x47EC80: mov      eax, dword ptr [ebp - 4]
0x47EC83: mov      byte ptr [eax + 0x6d], 1
0x47EC87: mov      eax, dword ptr [ebp - 4]
0x47EC8A: mov      byte ptr [eax + 0x34], 1
0x47EC8E: mov      eax, dword ptr [ebp - 4]
0x47EC91: mov      byte ptr [eax + 0x6c], 0
0x47EC95: mov      eax, dword ptr [ebp - 4]
0x47EC98: mov      byte ptr [eax + 0x1c], 0
0x47EC9C: mov      eax, dword ptr [ebp - 4]
0x47EC9F: mov      byte ptr [eax + 0x54], 0
0x47ECA3: pop      ecx
0x47ECA4: pop      ebp
0x47ECA5: ret      
0x47ECA6: mov      eax, eax
0x47ECA8: push     ebp
0x47ECA9: mov      ebp, esp
0x47ECAB: add      esp, 0xfffffef0
0x47ECB1: mov      dword ptr [ebp - 0xc], ecx
0x47ECB4: mov      dword ptr [ebp - 8], edx
0x47ECB7: mov      dword ptr [ebp - 4], eax
0x47ECBA: mov      eax, dword ptr [ebp - 8]
0x47ECBD: call     0x403e88
0x47ECC2: xor      eax, eax
0x47ECC4: push     ebp
0x47ECC5: push     0x47ed9d
0x47ECCA: push     dword ptr fs:[eax]
0x47ECCD: mov      dword ptr fs:[eax], esp
0x47ECD0: mov      eax, dword ptr [ebp - 4]
0x47ECD3: cmp      byte ptr [eax + 0x18], 0
0x47ECD7: jne      0x47ece0
0x47ECD9: xor      eax, eax
0x47ECDB: mov      dword ptr [ebp - 0x10], eax
0x47ECDE: jmp      0x47ecf4
0x47ECE0: mov      eax, dword ptr [ebp - 4]
0x47ECE3: cmp      byte ptr [eax + 0x50], 0
0x47ECE7: jne      0x47ed87
0x47ECED: mov      dword ptr [ebp - 0x10], 1
0x47ECF4: lea      eax, [ebp - 0x110]
0x47ECFA: mov      edx, dword ptr [ebp - 8]
0x47ECFD: mov      ecx, 0xff
0x47ED02: call     0x403cb0
0x47ED07: lea      edx, [ebp - 0x110]
0x47ED0D: mov      eax, dword ptr [ebp - 0x10]
0x47ED10: mov      ecx, eax
0x47ED12: shl      eax, 3
0x47ED15: sub      eax, ecx
0x47ED17: mov      ecx, dword ptr [ebp - 4]
0x47ED1A: lea      eax, [ecx + eax*8 + 0x1c]
0x47ED1E: mov      cl, 0xe
0x47ED20: call     0x402a30
0x47ED25: mov      al, byte ptr [ebp - 0xc]
0x47ED28: mov      edx, dword ptr [ebp - 0x10]
0x47ED2B: mov      ecx, edx
0x47ED2D: shl      edx, 3
0x47ED30: sub      edx, ecx
0x47ED32: mov      ecx, dword ptr [ebp - 4]
0x47ED35: mov      byte ptr [ecx + edx*8 + 0x2d], al
0x47ED39: mov      al, byte ptr [ebp + 0x10]
0x47ED3C: mov      edx, dword ptr [ebp - 0x10]
0x47ED3F: mov      ecx, edx
0x47ED41: shl      edx, 3
0x47ED44: sub      edx, ecx
0x47ED46: mov      ecx, dword ptr [ebp - 4]
0x47ED49: mov      byte ptr [ecx + edx*8 + 0x2c], al
0x47ED4D: mov      al, byte ptr [ebp + 0xc]
0x47ED50: mov      edx, dword ptr [ebp - 0x10]
0x47ED53: mov      ecx, edx
0x47ED55: shl      edx, 3
0x47ED58: sub      edx, ecx
0x47ED5A: mov      ecx, dword ptr [ebp - 4]
0x47ED5D: mov      byte ptr [ecx + edx*8 + 0x2e], al
0x47ED61: mov      al, byte ptr [ebp + 8]
0x47ED64: mov      edx, dword ptr [ebp - 0x10]
0x47ED67: mov      ecx, edx
0x47ED69: shl      edx, 3
0x47ED6C: sub      edx, ecx
0x47ED6E: mov      ecx, dword ptr [ebp - 4]
0x47ED71: mov      byte ptr [ecx + edx*8 + 0x2b], al
0x47ED75: mov      eax, dword ptr [ebp - 0x10]
0x47ED78: mov      edx, eax
0x47ED7A: shl      eax, 3
0x47ED7D: sub      eax, edx
0x47ED7F: mov      edx, dword ptr [ebp - 4]
0x47ED82: mov      byte ptr [edx + eax*8 + 0x18], 1
0x47ED87: xor      eax, eax
0x47ED89: pop      edx
0x47ED8A: pop      ecx
0x47ED8B: pop      ecx
0x47ED8C: mov      dword ptr fs:[eax], edx
0x47ED8F: push     0x47eda4
0x47ED94: lea      eax, [ebp - 8]
0x47ED97: call     0x403a58
0x47ED9C: ret      
0x47ED9D: jmp      0x403518
0x47EDA2: jmp      0x47ed94
0x47EDA4: mov      esp, ebp
0x47EDA6: pop      ebp
0x47EDA7: ret      0xc
0x47EDAA: mov      eax, eax
0x47EDAC: push     ebp
0x47EDAD: mov      ebp, esp
0x47EDAF: add      esp, -8
0x47EDB2: mov      dword ptr [ebp - 8], edx
0x47EDB5: mov      dword ptr [ebp - 4], eax
0x47EDB8: mov      eax, dword ptr [ebp - 4]
0x47EDBB: mov      byte ptr [eax + 0xc], 1
0x47EDBF: mov      eax, dword ptr [ebp - 8]
0x47EDC2: mov      edx, dword ptr [ebp - 4]
0x47EDC5: mov      dword ptr [edx + 0x14], eax
0x47EDC8: cmp      dword ptr [ebp - 8], 0
0x47EDCC: jne      0x47edfe
0x47EDCE: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47EDD3: mov      eax, dword ptr [eax]
0x47EDD5: mov      eax, dword ptr [eax + 0x334]
0x47EDDB: mov      edx, 0x19f
0x47EDE0: call     0x42718c
0x47EDE5: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47EDEA: mov      eax, dword ptr [eax]
0x47EDEC: mov      eax, dword ptr [eax + 0x334]
0x47EDF2: mov      edx, 0xf
0x47EDF7: call     0x4271b0
0x47EDFC: jmp      0x47ee2c
0x47EDFE: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47EE03: mov      eax, dword ptr [eax]
0x47EE05: mov      eax, dword ptr [eax + 0x334]
0x47EE0B: mov      edx, 0x4b
0x47EE10: call     0x42718c
0x47EE15: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47EE1A: mov      eax, dword ptr [eax]
0x47EE1C: mov      eax, dword ptr [eax + 0x334]
0x47EE22: mov      edx, 0xf
0x47EE27: call     0x4271b0
0x47EE2C: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47EE31: mov      eax, dword ptr [eax]
0x47EE33: mov      eax, dword ptr [eax + 0x334]
0x47EE39: mov      dl, 1
0x47EE3B: call     0x458214
0x47EE40: mov      eax, dword ptr [ebp - 4]
0x47EE43: mov      eax, dword ptr [eax + 0x14]
0x47EE46: mov      edx, eax
0x47EE48: shl      eax, 3
0x47EE4B: sub      eax, edx
0x47EE4D: mov      edx, dword ptr [ebp - 4]
0x47EE50: mov      byte ptr [edx + eax*8 + 0x18], 1
0x47EE55: mov      eax, dword ptr [ebp - 4]
0x47EE58: mov      eax, dword ptr [eax + 0x14]
0x47EE5B: mov      edx, eax
0x47EE5D: shl      eax, 3
0x47EE60: sub      eax, edx
0x47EE62: mov      edx, dword ptr [ebp - 4]
0x47EE65: mov      byte ptr [edx + eax*8 + 0x35], 0
0x47EE6A: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47EE6F: mov      eax, dword ptr [eax]
0x47EE71: mov      eax, dword ptr [eax + 0x334]
0x47EE77: mov      edx, dword ptr [eax + 0x30]
0x47EE7A: add      edx, 0x47
0x47EE7D: mov      eax, dword ptr [ebp - 4]
0x47EE80: mov      eax, dword ptr [eax + 0x10]
0x47EE83: call     0x42718c
0x47EE88: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47EE8D: mov      eax, dword ptr [eax]
0x47EE8F: mov      eax, dword ptr [eax + 0x334]
0x47EE95: mov      edx, dword ptr [eax + 0x34]
0x47EE98: add      edx, 0x6b
0x47EE9B: mov      eax, dword ptr [ebp - 4]
0x47EE9E: mov      eax, dword ptr [eax + 0x10]
0x47EEA1: call     0x4271b0
0x47EEA6: mov      dl, 1
0x47EEA8: mov      eax, dword ptr [ebp - 4]
0x47EEAB: mov      eax, dword ptr [eax + 0x10]
0x47EEAE: call     0x4278c0
0x47EEB3: mov      eax, dword ptr [ebp - 4]
0x47EEB6: mov      eax, dword ptr [eax + 0x10]
0x47EEB9: mov      edx, dword ptr [eax]
0x47EEBB: call     dword ptr [edx + 0xb4]
0x47EEC1: mov      eax, dword ptr [ebp - 4]
0x47EEC4: mov      edx, dword ptr [eax + 0x14]
0x47EEC7: mov      eax, dword ptr [ebp - 4]
0x47EECA: call     0x47ef90
0x47EECF: mov      eax, dword ptr [ebp - 4]
0x47EED2: mov      eax, dword ptr [eax + 0x14]
0x47EED5: mov      edx, eax
0x47EED7: shl      eax, 3
0x47EEDA: sub      eax, edx
0x47EEDC: mov      edx, dword ptr [ebp - 4]
0x47EEDF: lea      eax, [edx + eax*8 + 0x1c]
0x47EEE3: xor      ecx, ecx
0x47EEE5: mov      edx, 0x18
0x47EEEA: call     0x402b54
0x47EEEF: pop      ecx
0x47EEF0: pop      ecx
0x47EEF1: pop      ebp
0x47EEF2: ret      
0x47EEF3: nop      
0x47EEF4: push     ebp
0x47EEF5: mov      ebp, esp
0x47EEF7: add      esp, -0xc
0x47EEFA: mov      dword ptr [ebp - 0xc], ecx
0x47EEFD: mov      dword ptr [ebp - 8], edx
0x47EF00: mov      dword ptr [ebp - 4], eax
0x47EF03: mov      esp, ebp
0x47EF05: pop      ebp
0x47EF06: ret      
0x47EF07: nop      
0x47EF08: push     ebp
0x47EF09: mov      ebp, esp
0x47EF0B: add      esp, -0xc
0x47EF0E: mov      dword ptr [ebp - 0xc], ecx
0x47EF11: mov      dword ptr [ebp - 8], edx
0x47EF14: mov      dword ptr [ebp - 4], eax
0x47EF17: mov      eax, dword ptr [ebp - 0xc]
0x47EF1A: call     0x403a58
0x47EF1F: mov      eax, dword ptr [ebp - 8]
0x47EF22: sub      eax, 1
0x47EF25: jb       0x47ef2e
0x47EF27: je       0x47ef3d
0x47EF29: dec      eax
0x47EF2A: je       0x47ef4c
0x47EF2C: jmp      0x47ef59
0x47EF2E: mov      eax, dword ptr [ebp - 0xc]
0x47EF31: mov      edx, 0x47ef68 ; "武士"
0x47EF36: call     0x403af0
0x47EF3B: jmp      0x47ef59
0x47EF3D: mov      eax, dword ptr [ebp - 0xc]
0x47EF40: mov      edx, 0x47ef78 ; "魔法师"
0x47EF45: call     0x403af0
0x47EF4A: jmp      0x47ef59
0x47EF4C: mov      eax, dword ptr [ebp - 0xc]
0x47EF4F: mov      edx, 0x47ef88 ; "道士"
0x47EF54: call     0x403af0
0x47EF59: mov      esp, ebp
0x47EF5B: pop      ebp
0x47EF5C: ret      
0x47EF5D: add      byte ptr [eax], al
0x47EF5F: add      bh, bh
0x47EF63: inc      dword ptr [eax + eax]
0x47EF66: add      byte ptr [eax], al
0x47EF68: into     
0x47EF69: in       al, 0xca
0x47EF6B: mov      edi, 0
0x47EF78: [STR len=6] "魔法师"
0x47EF88: [STR len=4] "道士"
0x47EF90: push     ebp
0x47EF91: mov      ebp, esp
0x47EF93: add      esp, -8
0x47EF96: mov      dword ptr [ebp - 8], edx
0x47EF99: mov      dword ptr [ebp - 4], eax
0x47EF9C: mov      eax, dword ptr [ebp - 8]
0x47EF9F: mov      edx, eax
0x47EFA1: shl      eax, 3
0x47EFA4: sub      eax, edx
0x47EFA6: mov      edx, dword ptr [ebp - 4]
0x47EFA9: mov      byte ptr [edx + eax*8 + 0x34], 1
0x47EFAE: mov      eax, dword ptr [ebp - 8]
0x47EFB1: mov      edx, eax
0x47EFB3: shl      eax, 3
0x47EFB6: sub      eax, edx
0x47EFB8: mov      edx, dword ptr [ebp - 4]
0x47EFBB: mov      dword ptr [edx + eax*8 + 0x3c], 0x1e
0x47EFC3: call     0x406154
0x47EFC8: mov      edx, dword ptr [ebp - 8]
0x47EFCB: mov      ecx, edx
0x47EFCD: shl      edx, 3
0x47EFD0: sub      edx, ecx
0x47EFD2: mov      ecx, dword ptr [ebp - 4]
0x47EFD5: mov      dword ptr [ecx + edx*8 + 0x44], eax
0x47EFD9: call     0x406154
0x47EFDE: mov      edx, dword ptr [ebp - 8]
0x47EFE1: mov      ecx, edx
0x47EFE3: shl      edx, 3
0x47EFE6: sub      edx, ecx
0x47EFE8: mov      ecx, dword ptr [ebp - 4]
0x47EFEB: mov      dword ptr [ecx + edx*8 + 0x48], eax
0x47EFEF: cmp      dword ptr [ebp - 8], 0
0x47EFF3: jne      0x47effe
0x47EFF5: mov      eax, dword ptr [ebp - 4]
0x47EFF8: mov      byte ptr [eax + 0x6c], 0
0x47EFFC: jmp      0x47f005
0x47EFFE: mov      eax, dword ptr [ebp - 4]
0x47F001: mov      byte ptr [eax + 0x34], 0
0x47F005: pop      ecx
0x47F006: pop      ecx
0x47F007: pop      ebp
0x47F008: ret      
0x47F009: lea      eax, [eax]
0x47F00C: push     ebp
0x47F00D: mov      ebp, esp
0x47F00F: push     ecx
0x47F010: mov      dword ptr [ebp - 4], eax
0x47F013: mov      eax, dword ptr [ebp - 4]
0x47F016: mov      eax, dword ptr [eax + 0x14]
0x47F019: mov      edx, eax
0x47F01B: shl      eax, 3
0x47F01E: sub      eax, edx
0x47F020: mov      edx, dword ptr [ebp - 4]
0x47F023: mov      byte ptr [edx + eax*8 + 0x18], 0
0x47F028: mov      eax, dword ptr [ebp - 4]
0x47F02B: mov      byte ptr [eax + 0xc], 0
0x47F02F: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47F034: mov      eax, dword ptr [eax]
0x47F036: mov      eax, dword ptr [eax + 0x334]
0x47F03C: xor      edx, edx
0x47F03E: call     0x458214
0x47F043: xor      edx, edx
0x47F045: mov      eax, dword ptr [ebp - 4]
0x47F048: mov      eax, dword ptr [eax + 0x10]
0x47F04B: call     0x4278c0
0x47F050: mov      eax, dword ptr [ebp - 4]
0x47F053: cmp      dword ptr [eax + 0x14], 1
0x47F057: jne      0x47f067
0x47F059: mov      eax, dword ptr [ebp - 4]
0x47F05C: mov      byte ptr [eax + 0x34], 1
0x47F060: mov      eax, dword ptr [ebp - 4]
0x47F063: mov      byte ptr [eax + 0x35], 0
0x47F067: pop      ecx
0x47F068: pop      ebp
0x47F069: ret      
0x47F06A: mov      eax, eax
0x47F06C: push     ebp
0x47F06D: mov      ebp, esp
0x47F06F: xor      ecx, ecx
0x47F071: push     ecx
0x47F072: push     ecx
0x47F073: push     ecx
0x47F074: push     ecx
0x47F075: push     ecx
0x47F076: push     ecx
0x47F077: mov      dword ptr [ebp - 4], eax
0x47F07A: xor      eax, eax
0x47F07C: push     ebp
0x47F07D: push     0x47f197
0x47F082: push     dword ptr fs:[eax]
0x47F085: mov      dword ptr fs:[eax], esp
0x47F088: lea      edx, [ebp - 0x18]
0x47F08B: mov      eax, dword ptr [ebp - 4]
0x47F08E: mov      eax, dword ptr [eax + 0x10]
0x47F091: call     0x4279d0
0x47F096: mov      eax, dword ptr [ebp - 0x18]
0x47F099: lea      edx, [ebp - 8]
0x47F09C: call     0x407be4
0x47F0A1: cmp      dword ptr [ebp - 8], 0
0x47F0A5: je       0x47f174
0x47F0AB: mov      eax, dword ptr [ebp - 4]
0x47F0AE: mov      eax, dword ptr [eax + 0x14]
0x47F0B1: mov      edx, eax
0x47F0B3: shl      eax, 3
0x47F0B6: sub      eax, edx
0x47F0B8: mov      edx, dword ptr [ebp - 4]
0x47F0BB: mov      byte ptr [edx + eax*8 + 0x18], 0
0x47F0C0: mov      eax, dword ptr [ebp - 4]
0x47F0C3: mov      byte ptr [eax + 0xc], 0
0x47F0C7: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x47F0CC: mov      eax, dword ptr [eax]
0x47F0CE: mov      eax, dword ptr [eax + 0x334]
0x47F0D4: xor      edx, edx
0x47F0D6: call     0x458214
0x47F0DB: xor      edx, edx
0x47F0DD: mov      eax, dword ptr [ebp - 4]
0x47F0E0: mov      eax, dword ptr [eax + 0x10]
0x47F0E3: call     0x4278c0
0x47F0E8: mov      eax, dword ptr [ebp - 4]
0x47F0EB: cmp      dword ptr [eax + 0x14], 1
0x47F0EF: jne      0x47f0ff
0x47F0F1: mov      eax, dword ptr [ebp - 4]
0x47F0F4: mov      byte ptr [eax + 0x34], 1
0x47F0F8: mov      eax, dword ptr [ebp - 4]
0x47F0FB: mov      byte ptr [eax + 0x35], 0
0x47F0FF: mov      eax, 5
0x47F104: call     0x402b74
0x47F109: inc      eax
0x47F10A: lea      edx, [ebp - 0xc]
0x47F10D: call     0x407d64
0x47F112: lea      edx, [ebp - 0x10]
0x47F115: mov      eax, dword ptr [ebp - 4]
0x47F118: mov      eax, dword ptr [eax + 0x14]
0x47F11B: mov      ecx, eax
0x47F11D: shl      eax, 3
0x47F120: sub      eax, ecx
0x47F122: mov      ecx, dword ptr [ebp - 4]
0x47F125: movzx    eax, byte ptr [ecx + eax*8 + 0x2d]
0x47F12A: call     0x407d64
0x47F12F: lea      edx, [ebp - 0x14]
0x47F132: mov      eax, dword ptr [ebp - 4]
0x47F135: mov      eax, dword ptr [eax + 0x14]
0x47F138: mov      ecx, eax
0x47F13A: shl      eax, 3
0x47F13D: sub      eax, ecx
0x47F13F: mov      ecx, dword ptr [ebp - 4]
0x47F142: movzx    eax, byte ptr [ecx + eax*8 + 0x2b]
0x47F147: call     0x407d64
0x47F14C: mov      eax, dword ptr [ebp - 0xc]
0x47F14F: push     eax
0x47F150: mov      eax, dword ptr [ebp - 0x10]
0x47F153: push     eax
0x47F154: mov      eax, dword ptr [ebp - 0x14]
0x47F157: push     eax
0x47F158: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47F15D: mov      eax, dword ptr [eax]
0x47F15F: mov      edx, dword ptr [eax + 0x410]
0x47F165: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47F16A: mov      eax, dword ptr [eax]
0x47F16C: mov      ecx, dword ptr [ebp - 8]
0x47F16F: call     0x465054
0x47F174: xor      eax, eax
0x47F176: pop      edx
0x47F177: pop      ecx
0x47F178: pop      ecx
0x47F179: mov      dword ptr fs:[eax], edx
0x47F17C: push     0x47f19e
0x47F181: lea      eax, [ebp - 0x18]
0x47F184: call     0x403a58
0x47F189: lea      eax, [ebp - 0x14]
0x47F18C: mov      edx, 4
0x47F191: call     0x403a7c
0x47F196: ret      
0x47F197: jmp      0x403518
0x47F19C: jmp      0x47f181
0x47F19E: mov      esp, ebp
0x47F1A0: pop      ebp
0x47F1A1: ret      
0x47F1A2: mov      eax, eax
0x47F1A4: push     ebp
0x47F1A5: mov      ebp, esp
0x47F1A7: add      esp, -8
0x47F1AA: mov      dword ptr [ebp - 8], edx
0x47F1AD: mov      dword ptr [ebp - 4], eax
0x47F1B0: mov      eax, dword ptr [ebp - 8]
0x47F1B3: sub      eax, 3
0x47F1B6: jae      0x47f1f7
0x47F1B8: mov      eax, dword ptr [ebp - 4]
0x47F1BB: mov      eax, dword ptr [eax + 0x14]
0x47F1BE: mov      edx, eax
0x47F1C0: shl      eax, 3
0x47F1C3: sub      eax, edx
0x47F1C5: mov      edx, dword ptr [ebp - 4]
0x47F1C8: movzx    eax, byte ptr [edx + eax*8 + 0x2d]
0x47F1CD: cmp      eax, dword ptr [ebp - 8]
0x47F1D0: je       0x47f1f7
0x47F1D2: mov      al, byte ptr [ebp - 8]
0x47F1D5: mov      edx, dword ptr [ebp - 4]
0x47F1D8: mov      edx, dword ptr [edx + 0x14]
0x47F1DB: mov      ecx, edx
0x47F1DD: shl      edx, 3
0x47F1E0: sub      edx, ecx
0x47F1E2: mov      ecx, dword ptr [ebp - 4]
0x47F1E5: mov      byte ptr [ecx + edx*8 + 0x2d], al
0x47F1E9: mov      eax, dword ptr [ebp - 4]
0x47F1EC: mov      edx, dword ptr [eax + 0x14]
0x47F1EF: mov      eax, dword ptr [ebp - 4]
0x47F1F2: call     0x47ef90
0x47F1F7: pop      ecx
0x47F1F8: pop      ecx
0x47F1F9: pop      ebp
0x47F1FA: ret      
0x47F1FB: nop      
0x47F1FC: push     ebp
0x47F1FD: mov      ebp, esp
0x47F1FF: add      esp, -8
0x47F202: mov      dword ptr [ebp - 8], edx
0x47F205: mov      dword ptr [ebp - 4], eax
0x47F208: mov      eax, dword ptr [ebp - 4]
0x47F20B: mov      eax, dword ptr [eax + 0x14]
0x47F20E: mov      edx, eax
0x47F210: shl      eax, 3
0x47F213: sub      eax, edx
0x47F215: mov      edx, dword ptr [ebp - 4]
0x47F218: movzx    eax, byte ptr [edx + eax*8 + 0x2b]
0x47F21D: cmp      eax, dword ptr [ebp - 8]
0x47F220: je       0x47f247
0x47F222: mov      al, byte ptr [ebp - 8]
0x47F225: mov      edx, dword ptr [ebp - 4]
0x47F228: mov      edx, dword ptr [edx + 0x14]
0x47F22B: mov      ecx, edx
0x47F22D: shl      edx, 3
0x47F230: sub      edx, ecx
0x47F232: mov      ecx, dword ptr [ebp - 4]
0x47F235: mov      byte ptr [ecx + edx*8 + 0x2b], al
0x47F239: mov      eax, dword ptr [ebp - 4]
0x47F23C: mov      edx, dword ptr [eax + 0x14]
0x47F23F: mov      eax, dword ptr [ebp - 4]
0x47F242: call     0x47ef90
0x47F247: pop      ecx
0x47F248: pop      ecx
0x47F249: pop      ebp
0x47F24A: ret      
0x47F24B: nop      
0x47F24C: push     ebp
0x47F24D: mov      ebp, esp
0x47F24F: push     ecx
0x47F250: mov      dword ptr [ebp - 4], eax
0x47F253: pop      ecx
0x47F254: pop      ebp
0x47F255: ret      
0x47F256: mov      eax, eax
0x47F258: push     ebp
0x47F259: mov      ebp, esp
0x47F25B: push     ecx
0x47F25C: mov      dword ptr [ebp - 4], eax
0x47F25F: pop      ecx
0x47F260: pop      ebp
0x47F261: ret      
0x47F262: mov      eax, eax
0x47F264: push     ebp
0x47F265: mov      ebp, esp
0x47F267: add      esp, -0x4c
0x47F26A: xor      ecx, ecx
0x47F26C: mov      dword ptr [ebp - 0x4c], ecx
0x47F26F: mov      dword ptr [ebp - 0x38], ecx
0x47F272: mov      dword ptr [ebp - 8], edx
0x47F275: mov      dword ptr [ebp - 4], eax
0x47F278: xor      eax, eax
0x47F27A: push     ebp
0x47F27B: push     0x47fcf5
0x47F280: push     dword ptr fs:[eax]
0x47F283: mov      dword ptr fs:[eax], esp
0x47F286: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47F28B: mov      eax, dword ptr [eax]
0x47F28D: mov      eax, dword ptr [eax + 0x308]
0x47F293: mov      edx, 0x41
0x47F298: call     0x45351c
0x47F29D: mov      dword ptr [ebp - 0x2c], eax
0x47F2A0: cmp      dword ptr [ebp - 0x2c], 0
0x47F2A4: je       0x47f2c7
0x47F2A6: lea      edx, [ebp - 0x48]
0x47F2A9: mov      eax, dword ptr [ebp - 0x2c]
0x47F2AC: call     0x44d8ec
0x47F2B1: lea      eax, [ebp - 0x48]
0x47F2B4: push     eax
0x47F2B5: mov      eax, dword ptr [ebp - 0x2c]
0x47F2B8: push     eax
0x47F2B9: push     0
0x47F2BB: xor      ecx, ecx
0x47F2BD: xor      edx, edx
0x47F2BF: mov      eax, dword ptr [ebp - 8]
0x47F2C2: call     0x44d6cc
0x47F2C7: xor      eax, eax
0x47F2C9: mov      dword ptr [ebp - 0xc], eax
0x47F2CC: mov      eax, dword ptr [ebp - 0xc]
0x47F2CF: mov      edx, eax
0x47F2D1: shl      eax, 3
0x47F2D4: sub      eax, edx
0x47F2D6: mov      edx, dword ptr [ebp - 4]
0x47F2D9: cmp      byte ptr [edx + eax*8 + 0x18], 0
0x47F2DE: je       0x47fcca
0x47F2E4: mov      dword ptr [ebp - 0x18], 0x5a
0x47F2EB: mov      dword ptr [ebp - 0x1c], 0x3a
0x47F2F2: mov      eax, dword ptr [ebp - 0xc]
0x47F2F5: mov      edx, eax
0x47F2F7: shl      eax, 3
0x47F2FA: sub      eax, edx
0x47F2FC: mov      edx, dword ptr [ebp - 4]
0x47F2FF: mov      al, byte ptr [edx + eax*8 + 0x2d]
0x47F303: sub      al, 1
0x47F305: jb       0x47f316
0x47F307: je       0x47f374
0x47F309: dec      al
0x47F30B: je       0x47f3c6
0x47F311: jmp      0x47f416
0x47F316: mov      eax, dword ptr [ebp - 0xc]
0x47F319: mov      edx, eax
0x47F31B: shl      eax, 3
0x47F31E: sub      eax, edx
0x47F320: mov      edx, dword ptr [ebp - 4]
0x47F323: cmp      byte ptr [edx + eax*8 + 0x2b], 0
0x47F328: jne      0x47f349
0x47F32A: mov      dword ptr [ebp - 0x10], 0x47
0x47F331: mov      dword ptr [ebp - 0x14], 0x34
0x47F338: mov      eax, dword ptr [ebp - 0x10]
0x47F33B: mov      dword ptr [ebp - 0x20], eax
0x47F33E: mov      eax, dword ptr [ebp - 0x14]
0x47F341: mov      dword ptr [ebp - 0x24], eax
0x47F344: jmp      0x47f416
0x47F349: mov      dword ptr [ebp - 0x10], 0x41
0x47F350: mov      dword ptr [ebp - 0x14], 0x37
0x47F357: mov      eax, dword ptr [ebp - 0x10]
0x47F35A: sub      eax, 0x1c
0x47F35D: add      eax, 0x1c
0x47F360: mov      dword ptr [ebp - 0x20], eax
0x47F363: mov      eax, dword ptr [ebp - 0x14]
0x47F366: sub      eax, 0x10
0x47F369: add      eax, 0x10
0x47F36C: mov      dword ptr [ebp - 0x24], eax
0x47F36F: jmp      0x47f416
0x47F374: mov      eax, dword ptr [ebp - 0xc]
0x47F377: mov      edx, eax
0x47F379: shl      eax, 3
0x47F37C: sub      eax, edx
0x47F37E: mov      edx, dword ptr [ebp - 4]
0x47F381: cmp      byte ptr [edx + eax*8 + 0x2b], 0
0x47F386: jne      0x47f3a4
0x47F388: mov      dword ptr [ebp - 0x10], 0x4d
0x47F38F: mov      dword ptr [ebp - 0x14], 0x2e
0x47F396: mov      eax, dword ptr [ebp - 0x10]
0x47F399: mov      dword ptr [ebp - 0x20], eax
0x47F39C: mov      eax, dword ptr [ebp - 0x14]
0x47F39F: mov      dword ptr [ebp - 0x24], eax
0x47F3A2: jmp      0x47f416
0x47F3A4: mov      dword ptr [ebp - 0x10], 0xab
0x47F3AB: mov      dword ptr [ebp - 0x14], 0x61
0x47F3B2: mov      eax, dword ptr [ebp - 0x10]
0x47F3B5: sub      eax, 0x1e
0x47F3B8: mov      dword ptr [ebp - 0x20], eax
0x47F3BB: mov      eax, dword ptr [ebp - 0x14]
0x47F3BE: sub      eax, 0xe
0x47F3C1: mov      dword ptr [ebp - 0x24], eax
0x47F3C4: jmp      0x47f416
0x47F3C6: mov      eax, dword ptr [ebp - 0xc]
0x47F3C9: mov      edx, eax
0x47F3CB: shl      eax, 3
0x47F3CE: sub      eax, edx
0x47F3D0: mov      edx, dword ptr [ebp - 4]
0x47F3D3: cmp      byte ptr [edx + eax*8 + 0x2b], 0
0x47F3D8: jne      0x47f3f6
0x47F3DA: mov      dword ptr [ebp - 0x10], 0x55
0x47F3E1: mov      dword ptr [ebp - 0x14], 0x3f
0x47F3E8: mov      eax, dword ptr [ebp - 0x10]
0x47F3EB: mov      dword ptr [ebp - 0x20], eax
0x47F3EE: mov      eax, dword ptr [ebp - 0x14]
0x47F3F1: mov      dword ptr [ebp - 0x24], eax
0x47F3F4: jmp      0x47f416
0x47F3F6: mov      dword ptr [ebp - 0x10], 0xa4
0x47F3FD: mov      dword ptr [ebp - 0x14], 0x67
0x47F404: mov      eax, dword ptr [ebp - 0x10]
0x47F407: sub      eax, 0x17
0x47F40A: mov      dword ptr [ebp - 0x20], eax
0x47F40D: mov      eax, dword ptr [ebp - 0x14]
0x47F410: sub      eax, 0x14
0x47F413: mov      dword ptr [ebp - 0x24], eax
0x47F416: cmp      dword ptr [ebp - 0xc], 1
0x47F41A: jne      0x47f440
0x47F41C: mov      dword ptr [ebp - 0x18], 0x1ae
0x47F423: mov      dword ptr [ebp - 0x1c], 0x3c
0x47F42A: add      dword ptr [ebp - 0x10], 0x154
0x47F431: add      dword ptr [ebp - 0x14], 2
0x47F435: add      dword ptr [ebp - 0x20], 0x154
0x47F43C: add      dword ptr [ebp - 0x24], 2
0x47F440: mov      eax, dword ptr [ebp - 0xc]
0x47F443: mov      edx, eax
0x47F445: shl      eax, 3
0x47F448: sub      eax, edx
0x47F44A: mov      edx, dword ptr [ebp - 4]
0x47F44D: cmp      byte ptr [edx + eax*8 + 0x36], 0
0x47F452: je       0x47f5f7
0x47F458: mov      eax, dword ptr [ebp - 0xc]
0x47F45B: mov      edx, eax
0x47F45D: shl      eax, 3
0x47F460: sub      eax, edx
0x47F462: mov      edx, dword ptr [ebp - 4]
0x47F465: movzx    eax, byte ptr [edx + eax*8 + 0x2d]
0x47F46A: shl      eax, 3
0x47F46D: lea      eax, [eax + eax*4]
0x47F470: add      eax, 0x3c
0x47F473: mov      edx, dword ptr [ebp - 0xc]
0x47F476: mov      ecx, edx
0x47F478: shl      edx, 3
0x47F47B: sub      edx, ecx
0x47F47D: mov      ecx, dword ptr [ebp - 4]
0x47F480: movzx    edx, byte ptr [ecx + edx*8 + 0x2b]
0x47F485: imul     edx, edx, 0x78
0x47F488: add      eax, edx
0x47F48A: mov      dword ptr [ebp - 0x28], eax
0x47F48D: mov      eax, dword ptr [ebp - 0xc]
0x47F490: mov      edx, eax
0x47F492: shl      eax, 3
0x47F495: sub      eax, edx
0x47F497: mov      edx, dword ptr [ebp - 4]
0x47F49A: mov      edx, dword ptr [edx + eax*8 + 0x38]
0x47F49E: add      edx, dword ptr [ebp - 0x28]
0x47F4A1: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47F4A6: mov      eax, dword ptr [eax]
0x47F4A8: mov      eax, dword ptr [eax + 0x34c]
0x47F4AE: call     0x45351c
0x47F4B3: mov      dword ptr [ebp - 0x2c], eax
0x47F4B6: mov      eax, dword ptr [ebp - 0xc]
0x47F4B9: mov      edx, eax
0x47F4BB: shl      eax, 3
0x47F4BE: sub      eax, edx
0x47F4C0: mov      edx, dword ptr [ebp - 4]
0x47F4C3: mov      edx, dword ptr [edx + eax*8 + 0x40]
0x47F4C7: add      edx, 4
0x47F4CA: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47F4CF: mov      eax, dword ptr [eax]
0x47F4D1: mov      eax, dword ptr [eax + 0x34c]
0x47F4D7: call     0x45351c
0x47F4DC: mov      dword ptr [ebp - 0x30], eax
0x47F4DF: cmp      dword ptr [ebp - 0x2c], 0
0x47F4E3: je       0x47f508
0x47F4E5: lea      edx, [ebp - 0x48]
0x47F4E8: mov      eax, dword ptr [ebp - 0x2c]
0x47F4EB: call     0x44d8ec
0x47F4F0: lea      eax, [ebp - 0x48]
0x47F4F3: push     eax
0x47F4F4: mov      eax, dword ptr [ebp - 0x2c]
0x47F4F7: push     eax
0x47F4F8: push     1
0x47F4FA: mov      ecx, dword ptr [ebp - 0x14]
0x47F4FD: mov      edx, dword ptr [ebp - 0x10]
0x47F500: mov      eax, dword ptr [ebp - 8]
0x47F503: call     0x44d6cc
0x47F508: cmp      dword ptr [ebp - 0x30], 0
0x47F50C: je       0x47f522
0x47F50E: mov      eax, dword ptr [ebp - 0x30]
0x47F511: push     eax
0x47F512: push     1
0x47F514: mov      ecx, dword ptr [ebp - 0x1c]
0x47F517: mov      edx, dword ptr [ebp - 0x18]
0x47F51A: mov      eax, dword ptr [ebp - 8]
0x47F51D: call     0x455578
0x47F522: call     0x406154
0x47F527: mov      edx, dword ptr [ebp - 0xc]
0x47F52A: mov      ecx, edx
0x47F52C: shl      edx, 3
0x47F52F: sub      edx, ecx
0x47F531: mov      ecx, dword ptr [ebp - 4]
0x47F534: sub      eax, dword ptr [ecx + edx*8 + 0x44]
0x47F538: cmp      eax, 0x78
0x47F53B: jbe      0x47f564
0x47F53D: call     0x406154
0x47F542: mov      edx, dword ptr [ebp - 0xc]
0x47F545: mov      ecx, edx
0x47F547: shl      edx, 3
0x47F54A: sub      edx, ecx
0x47F54C: mov      ecx, dword ptr [ebp - 4]
0x47F54F: mov      dword ptr [ecx + edx*8 + 0x44], eax
0x47F553: mov      eax, dword ptr [ebp - 0xc]
0x47F556: mov      edx, eax
0x47F558: shl      eax, 3
0x47F55B: sub      eax, edx
0x47F55D: mov      edx, dword ptr [ebp - 4]
0x47F560: inc      dword ptr [edx + eax*8 + 0x38]
0x47F564: call     0x406154
0x47F569: mov      edx, dword ptr [ebp - 0xc]
0x47F56C: mov      ecx, edx
0x47F56E: shl      edx, 3
0x47F571: sub      edx, ecx
0x47F573: mov      ecx, dword ptr [ebp - 4]
0x47F576: sub      eax, dword ptr [ecx + edx*8 + 0x4c]
0x47F57A: cmp      eax, 0x6e
0x47F57D: jbe      0x47f5a6
0x47F57F: call     0x406154
0x47F584: mov      edx, dword ptr [ebp - 0xc]
0x47F587: mov      ecx, edx
0x47F589: shl      edx, 3
0x47F58C: sub      edx, ecx
0x47F58E: mov      ecx, dword ptr [ebp - 4]
0x47F591: mov      dword ptr [ecx + edx*8 + 0x4c], eax
0x47F595: mov      eax, dword ptr [ebp - 0xc]
0x47F598: mov      edx, eax
0x47F59A: shl      eax, 3
0x47F59D: sub      eax, edx
0x47F59F: mov      edx, dword ptr [ebp - 4]
0x47F5A2: inc      dword ptr [edx + eax*8 + 0x40]
0x47F5A6: mov      eax, dword ptr [ebp - 0xc]
0x47F5A9: mov      edx, eax
0x47F5AB: shl      eax, 3
0x47F5AE: sub      eax, edx
0x47F5B0: mov      edx, dword ptr [ebp - 4]
0x47F5B3: cmp      dword ptr [edx + eax*8 + 0x38], 0xc
0x47F5B8: jle      0x47f66e
0x47F5BE: mov      eax, dword ptr [ebp - 0xc]
0x47F5C1: mov      edx, eax
0x47F5C3: shl      eax, 3
0x47F5C6: sub      eax, edx
0x47F5C8: mov      edx, dword ptr [ebp - 4]
0x47F5CB: mov      byte ptr [edx + eax*8 + 0x36], 0
0x47F5D0: mov      eax, dword ptr [ebp - 0xc]
0x47F5D3: mov      edx, eax
0x47F5D5: shl      eax, 3
0x47F5D8: sub      eax, edx
0x47F5DA: mov      edx, dword ptr [ebp - 4]
0x47F5DD: mov      byte ptr [edx + eax*8 + 0x35], 0
0x47F5E2: mov      eax, dword ptr [ebp - 0xc]
0x47F5E5: mov      edx, eax
0x47F5E7: shl      eax, 3
0x47F5EA: sub      eax, edx
0x47F5EC: mov      edx, dword ptr [ebp - 4]
0x47F5EF: xor      ecx, ecx
0x47F5F1: mov      dword ptr [edx + eax*8 + 0x38], ecx
0x47F5F5: jmp      0x47f66e
0x47F5F7: mov      eax, dword ptr [ebp - 0xc]
0x47F5FA: mov      edx, eax
0x47F5FC: shl      eax, 3
0x47F5FF: sub      eax, edx
0x47F601: mov      edx, dword ptr [ebp - 4]
0x47F604: cmp      byte ptr [edx + eax*8 + 0x34], 0
0x47F609: jne      0x47f66e
0x47F60B: mov      eax, dword ptr [ebp - 0xc]
0x47F60E: mov      edx, eax
0x47F610: shl      eax, 3
0x47F613: sub      eax, edx
0x47F615: mov      edx, dword ptr [ebp - 4]
0x47F618: cmp      byte ptr [edx + eax*8 + 0x35], 0
0x47F61D: jne      0x47f66e
0x47F61F: mov      eax, dword ptr [ebp - 0xc]
0x47F622: mov      edx, eax
0x47F624: shl      eax, 3
0x47F627: sub      eax, edx
0x47F629: mov      edx, dword ptr [ebp - 4]
0x47F62C: cmp      byte ptr [edx + eax*8 + 0x37], 0
0x47F631: jne      0x47f66e
0x47F633: mov      eax, dword ptr [ebp - 0xc]
0x47F636: mov      edx, eax
0x47F638: shl      eax, 3
0x47F63B: sub      eax, edx
0x47F63D: mov      edx, dword ptr [ebp - 4]
0x47F640: mov      byte ptr [edx + eax*8 + 0x37], 1
0x47F645: mov      eax, dword ptr [ebp - 0xc]
0x47F648: mov      edx, eax
0x47F64A: shl      eax, 3
0x47F64D: sub      eax, edx
0x47F64F: mov      edx, dword ptr [ebp - 4]
0x47F652: xor      ecx, ecx
0x47F654: mov      dword ptr [edx + eax*8 + 0x38], ecx
0x47F658: call     0x406154
0x47F65D: mov      edx, dword ptr [ebp - 0xc]
0x47F660: mov      ecx, edx
0x47F662: shl      edx, 3
0x47F665: sub      edx, ecx
0x47F667: mov      ecx, dword ptr [ebp - 4]
0x47F66A: mov      dword ptr [ecx + edx*8 + 0x44], eax
0x47F66E: mov      eax, dword ptr [ebp - 0xc]
0x47F671: mov      edx, eax
0x47F673: shl      eax, 3
0x47F676: sub      eax, edx
0x47F678: mov      edx, dword ptr [ebp - 4]
0x47F67B: cmp      byte ptr [edx + eax*8 + 0x37], 0
0x47F680: je       0x47f79e
0x47F686: mov      eax, dword ptr [ebp - 0xc]
0x47F689: mov      edx, eax
0x47F68B: shl      eax, 3
0x47F68E: sub      eax, edx
0x47F690: mov      edx, dword ptr [ebp - 4]
0x47F693: movzx    eax, byte ptr [edx + eax*8 + 0x2d]
0x47F698: shl      eax, 3
0x47F69B: lea      eax, [eax + eax*4]
0x47F69E: add      eax, 0x3c
0x47F6A1: mov      edx, dword ptr [ebp - 0xc]
0x47F6A4: mov      ecx, edx
0x47F6A6: shl      edx, 3
0x47F6A9: sub      edx, ecx
0x47F6AB: mov      ecx, dword ptr [ebp - 4]
0x47F6AE: movzx    edx, byte ptr [ecx + edx*8 + 0x2b]
0x47F6B3: imul     edx, edx, 0x78
0x47F6B6: add      eax, edx
0x47F6B8: mov      dword ptr [ebp - 0x28], eax
0x47F6BB: mov      edx, dword ptr [ebp - 0x28]
0x47F6BE: add      edx, 0xd
0x47F6C1: mov      eax, dword ptr [ebp - 0xc]
0x47F6C4: mov      ecx, eax
0x47F6C6: shl      eax, 3
0x47F6C9: sub      eax, ecx
0x47F6CB: mov      ecx, dword ptr [ebp - 4]
0x47F6CE: sub      edx, dword ptr [ecx + eax*8 + 0x38]
0x47F6D2: dec      edx
0x47F6D3: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47F6D8: mov      eax, dword ptr [eax]
0x47F6DA: mov      eax, dword ptr [eax + 0x34c]
0x47F6E0: call     0x45351c
0x47F6E5: mov      dword ptr [ebp - 0x2c], eax
0x47F6E8: cmp      dword ptr [ebp - 0x2c], 0
0x47F6EC: je       0x47f711
0x47F6EE: lea      edx, [ebp - 0x48]
0x47F6F1: mov      eax, dword ptr [ebp - 0x2c]
0x47F6F4: call     0x44d8ec
0x47F6F9: lea      eax, [ebp - 0x48]
0x47F6FC: push     eax
0x47F6FD: mov      eax, dword ptr [ebp - 0x2c]
0x47F700: push     eax
0x47F701: push     1
0x47F703: mov      ecx, dword ptr [ebp - 0x14]
0x47F706: mov      edx, dword ptr [ebp - 0x10]
0x47F709: mov      eax, dword ptr [ebp - 8]
0x47F70C: call     0x44d6cc
0x47F711: call     0x406154
0x47F716: mov      edx, dword ptr [ebp - 0xc]
0x47F719: mov      ecx, edx
0x47F71B: shl      edx, 3
0x47F71E: sub      edx, ecx
0x47F720: mov      ecx, dword ptr [ebp - 4]
0x47F723: sub      eax, dword ptr [ecx + edx*8 + 0x44]
0x47F727: cmp      eax, 0x32
0x47F72A: jbe      0x47f753
0x47F72C: call     0x406154
0x47F731: mov      edx, dword ptr [ebp - 0xc]
0x47F734: mov      ecx, edx
0x47F736: shl      edx, 3
0x47F739: sub      edx, ecx
0x47F73B: mov      ecx, dword ptr [ebp - 4]
0x47F73E: mov      dword ptr [ecx + edx*8 + 0x44], eax
0x47F742: mov      eax, dword ptr [ebp - 0xc]
0x47F745: mov      edx, eax
0x47F747: shl      eax, 3
0x47F74A: sub      eax, edx
0x47F74C: mov      edx, dword ptr [ebp - 4]
0x47F74F: inc      dword ptr [edx + eax*8 + 0x38]
0x47F753: mov      eax, dword ptr [ebp - 0xc]
0x47F756: mov      edx, eax
0x47F758: shl      eax, 3
0x47F75B: sub      eax, edx
0x47F75D: mov      edx, dword ptr [ebp - 4]
0x47F760: cmp      dword ptr [edx + eax*8 + 0x38], 0xc
0x47F765: jle      0x47f79e
0x47F767: mov      eax, dword ptr [ebp - 0xc]
0x47F76A: mov      edx, eax
0x47F76C: shl      eax, 3
0x47F76F: sub      eax, edx
0x47F771: mov      edx, dword ptr [ebp - 4]
0x47F774: mov      byte ptr [edx + eax*8 + 0x37], 0
0x47F779: mov      eax, dword ptr [ebp - 0xc]
0x47F77C: mov      edx, eax
0x47F77E: shl      eax, 3
0x47F781: sub      eax, edx
0x47F783: mov      edx, dword ptr [ebp - 4]
0x47F786: mov      byte ptr [edx + eax*8 + 0x35], 1
0x47F78B: mov      eax, dword ptr [ebp - 0xc]
0x47F78E: mov      edx, eax
0x47F790: shl      eax, 3
0x47F793: sub      eax, edx
0x47F795: mov      edx, dword ptr [ebp - 4]
0x47F798: xor      ecx, ecx
0x47F79A: mov      dword ptr [edx + eax*8 + 0x38], ecx
0x47F79E: mov      eax, dword ptr [ebp - 0xc]
0x47F7A1: mov      edx, eax
0x47F7A3: shl      eax, 3
0x47F7A6: sub      eax, edx
0x47F7A8: mov      edx, dword ptr [ebp - 4]
0x47F7AB: cmp      byte ptr [edx + eax*8 + 0x36], 0
0x47F7B0: jne      0x47fa91
0x47F7B6: mov      eax, dword ptr [ebp - 0xc]
0x47F7B9: mov      edx, eax
0x47F7BB: shl      eax, 3
0x47F7BE: sub      eax, edx
0x47F7C0: mov      edx, dword ptr [ebp - 4]
0x47F7C3: cmp      byte ptr [edx + eax*8 + 0x37], 0
0x47F7C8: jne      0x47fa91
0x47F7CE: mov      eax, dword ptr [ebp - 0xc]
0x47F7D1: mov      edx, eax
0x47F7D3: shl      eax, 3
0x47F7D6: sub      eax, edx
0x47F7D8: mov      edx, dword ptr [ebp - 4]
0x47F7DB: cmp      byte ptr [edx + eax*8 + 0x35], 0
0x47F7E0: jne      0x47f942
0x47F7E6: mov      eax, dword ptr [ebp - 0xc]
0x47F7E9: mov      edx, eax
0x47F7EB: shl      eax, 3
0x47F7EE: sub      eax, edx
0x47F7F0: mov      edx, dword ptr [ebp - 4]
0x47F7F3: movzx    eax, byte ptr [edx + eax*8 + 0x2d]
0x47F7F8: shl      eax, 3
0x47F7FB: lea      eax, [eax + eax*4]
0x47F7FE: add      eax, 0x28
0x47F801: mov      edx, dword ptr [ebp - 0xc]
0x47F804: mov      ecx, edx
0x47F806: shl      edx, 3
0x47F809: sub      edx, ecx
0x47F80B: mov      ecx, dword ptr [ebp - 4]
0x47F80E: add      eax, dword ptr [ecx + edx*8 + 0x38]
0x47F812: mov      edx, dword ptr [ebp - 0xc]
0x47F815: mov      ecx, edx
0x47F817: shl      edx, 3
0x47F81A: sub      edx, ecx
0x47F81C: mov      ecx, dword ptr [ebp - 4]
0x47F81F: movzx    edx, byte ptr [ecx + edx*8 + 0x2b]
0x47F824: imul     edx, edx, 0x78
0x47F827: add      eax, edx
0x47F829: mov      dword ptr [ebp - 0x28], eax
0x47F82C: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47F831: mov      eax, dword ptr [eax]
0x47F833: mov      eax, dword ptr [eax + 0x34c]
0x47F839: mov      edx, dword ptr [ebp - 0x28]
0x47F83C: call     0x45351c
0x47F841: mov      dword ptr [ebp - 0x2c], eax
0x47F844: cmp      dword ptr [ebp - 0x2c], 0
0x47F848: je       0x47f9b8
0x47F84E: mov      eax, dword ptr [ebp - 0xc]
0x47F851: mov      edx, eax
0x47F853: shl      eax, 3
0x47F856: sub      eax, edx
0x47F858: mov      edx, dword ptr [ebp - 4]
0x47F85B: cmp      dword ptr [edx + eax*8 + 0x3c], 0
0x47F860: jle      0x47f91d
0x47F866: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47F86B: mov      eax, dword ptr [eax]
0x47F86D: mov      eax, dword ptr [eax + 0x2f4]
0x47F873: mov      ecx, dword ptr [eax + 0x268]
0x47F879: mov      dl, 1
0x47F87B: mov      eax, dword ptr [0x44b6a0]
0x47F880: call     0x44d190
0x47F885: mov      dword ptr [ebp - 0x34], eax
0x47F888: mov      eax, dword ptr [ebp - 0x34]
0x47F88B: mov      byte ptr [eax + 0x18], 1
0x47F88F: mov      eax, dword ptr [ebp - 0x2c]
0x47F892: call     0x44d944
0x47F897: push     eax
0x47F898: mov      eax, dword ptr [ebp - 0x2c]
0x47F89B: call     0x44dc60
0x47F8A0: mov      edx, eax
0x47F8A2: mov      eax, dword ptr [ebp - 0x34]
0x47F8A5: pop      ecx
0x47F8A6: call     0x44e480
0x47F8AB: lea      edx, [ebp - 0x48]
0x47F8AE: mov      eax, dword ptr [ebp - 0x2c]
0x47F8B1: call     0x44d8ec
0x47F8B6: lea      eax, [ebp - 0x48]
0x47F8B9: push     eax
0x47F8BA: mov      eax, dword ptr [ebp - 0x2c]
0x47F8BD: push     eax
0x47F8BE: push     0
0x47F8C0: xor      ecx, ecx
0x47F8C2: xor      edx, edx
0x47F8C4: mov      eax, dword ptr [ebp - 0x34]
0x47F8C7: call     0x44d6cc
0x47F8CC: mov      eax, dword ptr [ebp - 0xc]
0x47F8CF: mov      edx, eax
0x47F8D1: shl      eax, 3
0x47F8D4: sub      eax, edx
0x47F8D6: mov      edx, dword ptr [ebp - 4]
0x47F8D9: push     dword ptr [edx + eax*8 + 0x3c]
0x47F8DD: mov      edx, 0x1e
0x47F8E2: pop      eax
0x47F8E3: sub      edx, eax
0x47F8E5: mov      eax, dword ptr [ebp - 0x34]
0x47F8E8: call     0x455420
0x47F8ED: lea      edx, [ebp - 0x48]
0x47F8F0: mov      eax, dword ptr [ebp - 0x34]
0x47F8F3: call     0x44d8ec
0x47F8F8: lea      eax, [ebp - 0x48]
0x47F8FB: push     eax
0x47F8FC: mov      eax, dword ptr [ebp - 0x34]
0x47F8FF: push     eax
0x47F900: push     1
0x47F902: mov      ecx, dword ptr [ebp - 0x24]
0x47F905: mov      edx, dword ptr [ebp - 0x20]
0x47F908: mov      eax, dword ptr [ebp - 8]
0x47F90B: call     0x44d6cc
0x47F910: mov      eax, dword ptr [ebp - 0x34]
0x47F913: call     0x402eac
0x47F918: jmp      0x47f9b8
0x47F91D: lea      edx, [ebp - 0x48]
0x47F920: mov      eax, dword ptr [ebp - 0x2c]
0x47F923: call     0x44d8ec
0x47F928: lea      eax, [ebp - 0x48]
0x47F92B: push     eax
0x47F92C: mov      eax, dword ptr [ebp - 0x2c]
0x47F92F: push     eax
0x47F930: push     1
0x47F932: mov      ecx, dword ptr [ebp - 0x24]
0x47F935: mov      edx, dword ptr [ebp - 0x20]
0x47F938: mov      eax, dword ptr [ebp - 8]
0x47F93B: call     0x44d6cc
0x47F940: jmp      0x47f9b8
0x47F942: mov      eax, dword ptr [ebp - 0xc]
0x47F945: mov      edx, eax
0x47F947: shl      eax, 3
0x47F94A: sub      eax, edx
0x47F94C: mov      edx, dword ptr [ebp - 4]
0x47F94F: movzx    eax, byte ptr [edx + eax*8 + 0x2d]
0x47F954: shl      eax, 3
0x47F957: lea      eax, [eax + eax*4]
0x47F95A: add      eax, 0x3c
0x47F95D: mov      edx, dword ptr [ebp - 0xc]
0x47F960: mov      ecx, edx
0x47F962: shl      edx, 3
0x47F965: sub      edx, ecx
0x47F967: mov      ecx, dword ptr [ebp - 4]
0x47F96A: movzx    edx, byte ptr [ecx + edx*8 + 0x2b]
0x47F96F: imul     edx, edx, 0x78
0x47F972: add      eax, edx
0x47F974: mov      dword ptr [ebp - 0x28], eax
0x47F977: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47F97C: mov      eax, dword ptr [eax]
0x47F97E: mov      eax, dword ptr [eax + 0x34c]
0x47F984: mov      edx, dword ptr [ebp - 0x28]
0x47F987: call     0x45351c
0x47F98C: mov      dword ptr [ebp - 0x2c], eax
0x47F98F: cmp      dword ptr [ebp - 0x2c], 0
0x47F993: je       0x47f9b8
0x47F995: lea      edx, [ebp - 0x48]
0x47F998: mov      eax, dword ptr [ebp - 0x2c]
0x47F99B: call     0x44d8ec
0x47F9A0: lea      eax, [ebp - 0x48]
0x47F9A3: push     eax
0x47F9A4: mov      eax, dword ptr [ebp - 0x2c]
0x47F9A7: push     eax
0x47F9A8: push     1
0x47F9AA: mov      ecx, dword ptr [ebp - 0x14]
0x47F9AD: mov      edx, dword ptr [ebp - 0x10]
0x47F9B0: mov      eax, dword ptr [ebp - 8]
0x47F9B3: call     0x44d6cc
0x47F9B8: mov      eax, dword ptr [ebp - 0xc]
0x47F9BB: mov      edx, eax
0x47F9BD: shl      eax, 3
0x47F9C0: sub      eax, edx
0x47F9C2: mov      edx, dword ptr [ebp - 4]
0x47F9C5: cmp      byte ptr [edx + eax*8 + 0x34], 0
0x47F9CA: je       0x47fa91
0x47F9D0: call     0x406154
0x47F9D5: mov      edx, dword ptr [ebp - 0xc]
0x47F9D8: mov      ecx, edx
0x47F9DA: shl      edx, 3
0x47F9DD: sub      edx, ecx
0x47F9DF: mov      ecx, dword ptr [ebp - 4]
0x47F9E2: sub      eax, dword ptr [ecx + edx*8 + 0x44]
0x47F9E6: cmp      eax, 0x12c
0x47F9EB: jbe      0x47fa3b
0x47F9ED: call     0x406154
0x47F9F2: mov      edx, dword ptr [ebp - 0xc]
0x47F9F5: mov      ecx, edx
0x47F9F7: shl      edx, 3
0x47F9FA: sub      edx, ecx
0x47F9FC: mov      ecx, dword ptr [ebp - 4]
0x47F9FF: mov      dword ptr [ecx + edx*8 + 0x44], eax
0x47FA03: mov      eax, dword ptr [ebp - 0xc]
0x47FA06: mov      edx, eax
0x47FA08: shl      eax, 3
0x47FA0B: sub      eax, edx
0x47FA0D: mov      edx, dword ptr [ebp - 4]
0x47FA10: inc      dword ptr [edx + eax*8 + 0x38]
0x47FA14: mov      eax, dword ptr [ebp - 0xc]
0x47FA17: mov      edx, eax
0x47FA19: shl      eax, 3
0x47FA1C: sub      eax, edx
0x47FA1E: mov      edx, dword ptr [ebp - 4]
0x47FA21: cmp      dword ptr [edx + eax*8 + 0x38], 0xf
0x47FA26: jle      0x47fa3b
0x47FA28: mov      eax, dword ptr [ebp - 0xc]
0x47FA2B: mov      edx, eax
0x47FA2D: shl      eax, 3
0x47FA30: sub      eax, edx
0x47FA32: mov      edx, dword ptr [ebp - 4]
0x47FA35: xor      ecx, ecx
0x47FA37: mov      dword ptr [edx + eax*8 + 0x38], ecx
0x47FA3B: call     0x406154
0x47FA40: mov      edx, dword ptr [ebp - 0xc]
0x47FA43: mov      ecx, edx
0x47FA45: shl      edx, 3
0x47FA48: sub      edx, ecx
0x47FA4A: mov      ecx, dword ptr [ebp - 4]
0x47FA4D: sub      eax, dword ptr [ecx + edx*8 + 0x48]
0x47FA51: cmp      eax, 0x19
0x47FA54: jbe      0x47fa91
0x47FA56: call     0x406154
0x47FA5B: mov      edx, dword ptr [ebp - 0xc]
0x47FA5E: mov      ecx, edx
0x47FA60: shl      edx, 3
0x47FA63: sub      edx, ecx
0x47FA65: mov      ecx, dword ptr [ebp - 4]
0x47FA68: mov      dword ptr [ecx + edx*8 + 0x48], eax
0x47FA6C: mov      eax, dword ptr [ebp - 0xc]
0x47FA6F: mov      edx, eax
0x47FA71: shl      eax, 3
0x47FA74: sub      eax, edx
0x47FA76: mov      edx, dword ptr [ebp - 4]
0x47FA79: cmp      dword ptr [edx + eax*8 + 0x3c], 0
0x47FA7E: jle      0x47fa91
0x47FA80: mov      eax, dword ptr [ebp - 0xc]
0x47FA83: mov      edx, eax
0x47FA85: shl      eax, 3
0x47FA88: sub      eax, edx
0x47FA8A: mov      edx, dword ptr [ebp - 4]
0x47FA8D: dec      dword ptr [edx + eax*8 + 0x3c]
0x47FA91: cmp      dword ptr [ebp - 0xc], 0
0x47FA95: jne      0x47fb81
0x47FA9B: mov      eax, dword ptr [ebp - 0xc]
0x47FA9E: mov      edx, eax
0x47FAA0: shl      eax, 3
0x47FAA3: sub      eax, edx
0x47FAA5: mov      edx, dword ptr [ebp - 4]
0x47FAA8: cmp      byte ptr [edx + eax*8 + 0x1c], 0
0x47FAAD: je       0x47fc62
0x47FAB3: push     1
0x47FAB5: mov      eax, dword ptr [ebp - 8]
0x47FAB8: call     0x44d8b4
0x47FABD: call     0x41834c
0x47FAC2: push     eax
0x47FAC3: call     0x406434
0x47FAC8: push     0xffffff
0x47FACD: push     0
0x47FACF: lea      eax, [ebp - 0x4c]
0x47FAD2: mov      edx, dword ptr [ebp - 0xc]
0x47FAD5: mov      ecx, edx
0x47FAD7: shl      edx, 3
0x47FADA: sub      edx, ecx
0x47FADC: mov      ecx, dword ptr [ebp - 4]
0x47FADF: lea      edx, [ecx + edx*8 + 0x1c]
0x47FAE3: call     0x403c78
0x47FAE8: mov      eax, dword ptr [ebp - 0x4c]
0x47FAEB: push     eax
0x47FAEC: mov      ecx, 0x1ee
0x47FAF1: mov      edx, 0x75
0x47FAF6: mov      eax, dword ptr [ebp - 8]
0x47FAF9: call     0x4a09a0
0x47FAFE: push     0xffffff
0x47FB03: push     0
0x47FB05: lea      edx, [ebp - 0x4c]
0x47FB08: mov      eax, dword ptr [ebp - 0xc]
0x47FB0B: mov      ecx, eax
0x47FB0D: shl      eax, 3
0x47FB10: sub      eax, ecx
0x47FB12: mov      ecx, dword ptr [ebp - 4]
0x47FB15: movzx    eax, byte ptr [ecx + eax*8 + 0x2e]
0x47FB1A: call     0x407d64
0x47FB1F: mov      eax, dword ptr [ebp - 0x4c]
0x47FB22: push     eax
0x47FB23: mov      ecx, 0x20b
0x47FB28: mov      edx, 0x75
0x47FB2D: mov      eax, dword ptr [ebp - 8]
0x47FB30: call     0x4a09a0
0x47FB35: push     0xffffff
0x47FB3A: push     0
0x47FB3C: lea      ecx, [ebp - 0x4c]
0x47FB3F: mov      eax, dword ptr [ebp - 0xc]
0x47FB42: mov      edx, eax
0x47FB44: shl      eax, 3
0x47FB47: sub      eax, edx
0x47FB49: mov      edx, dword ptr [ebp - 4]
0x47FB4C: movzx    edx, byte ptr [edx + eax*8 + 0x2d]
0x47FB51: mov      eax, dword ptr [ebp - 4]
0x47FB54: call     0x47ef08
0x47FB59: mov      eax, dword ptr [ebp - 0x4c]
0x47FB5C: push     eax
0x47FB5D: mov      ecx, 0x229
0x47FB62: mov      edx, 0x75
0x47FB67: mov      eax, dword ptr [ebp - 8]
0x47FB6A: call     0x4a09a0
0x47FB6F: mov      eax, dword ptr [ebp - 8]
0x47FB72: call     0x44d8b4
0x47FB77: call     0x44d104
0x47FB7C: jmp      0x47fc62
0x47FB81: mov      eax, dword ptr [ebp - 0xc]
0x47FB84: mov      edx, eax
0x47FB86: shl      eax, 3
0x47FB89: sub      eax, edx
0x47FB8B: mov      edx, dword ptr [ebp - 4]
0x47FB8E: cmp      byte ptr [edx + eax*8 + 0x1c], 0
0x47FB93: je       0x47fc62
0x47FB99: push     1
0x47FB9B: mov      eax, dword ptr [ebp - 8]
0x47FB9E: call     0x44d8b4
0x47FBA3: call     0x41834c
0x47FBA8: push     eax
0x47FBA9: call     0x406434
0x47FBAE: push     0xffffff
0x47FBB3: push     0
0x47FBB5: lea      eax, [ebp - 0x4c]
0x47FBB8: mov      edx, dword ptr [ebp - 0xc]
0x47FBBB: mov      ecx, edx
0x47FBBD: shl      edx, 3
0x47FBC0: sub      edx, ecx
0x47FBC2: mov      ecx, dword ptr [ebp - 4]
0x47FBC5: lea      edx, [ecx + edx*8 + 0x1c]
0x47FBC9: call     0x403c78
0x47FBCE: mov      eax, dword ptr [ebp - 0x4c]
0x47FBD1: push     eax
0x47FBD2: mov      ecx, 0x1f0
0x47FBD7: mov      edx, 0x29f
0x47FBDC: mov      eax, dword ptr [ebp - 8]
0x47FBDF: call     0x4a09a0
0x47FBE4: push     0xffffff
0x47FBE9: push     0
0x47FBEB: lea      edx, [ebp - 0x4c]
0x47FBEE: mov      eax, dword ptr [ebp - 0xc]
0x47FBF1: mov      ecx, eax
0x47FBF3: shl      eax, 3
0x47FBF6: sub      eax, ecx
0x47FBF8: mov      ecx, dword ptr [ebp - 4]
0x47FBFB: movzx    eax, byte ptr [ecx + eax*8 + 0x2e]
0x47FC00: call     0x407d64
0x47FC05: mov      eax, dword ptr [ebp - 0x4c]
0x47FC08: push     eax
0x47FC09: mov      ecx, 0x20d
0x47FC0E: mov      edx, 0x29f
0x47FC13: mov      eax, dword ptr [ebp - 8]
0x47FC16: call     0x4a09a0
0x47FC1B: push     0xffffff
0x47FC20: push     0
0x47FC22: lea      ecx, [ebp - 0x4c]
0x47FC25: mov      eax, dword ptr [ebp - 0xc]
0x47FC28: mov      edx, eax
0x47FC2A: shl      eax, 3
0x47FC2D: sub      eax, edx
0x47FC2F: mov      edx, dword ptr [ebp - 4]
0x47FC32: movzx    edx, byte ptr [edx + eax*8 + 0x2d]
0x47FC37: mov      eax, dword ptr [ebp - 4]
0x47FC3A: call     0x47ef08
0x47FC3F: mov      eax, dword ptr [ebp - 0x4c]
0x47FC42: push     eax
0x47FC43: mov      ecx, 0x22b
0x47FC48: mov      edx, 0x29f
0x47FC4D: mov      eax, dword ptr [ebp - 8]
0x47FC50: call     0x4a09a0
0x47FC55: mov      eax, dword ptr [ebp - 8]
0x47FC58: call     0x44d8b4
0x47FC5D: call     0x44d104
0x47FC62: push     1
0x47FC64: mov      eax, dword ptr [ebp - 8]
0x47FC67: call     0x44d8b4
0x47FC6C: call     0x41834c
0x47FC71: push     eax
0x47FC72: call     0x406434
0x47FC77: lea      eax, [ebp - 0x38]
0x47FC7A: mov      edx, dword ptr [0x4a4018] ; *0x4A4018->0x4F7EE4
0x47FC80: mov      edx, dword ptr [edx]
0x47FC82: call     0x403af0
0x47FC87: push     0xffffff
0x47FC8C: push     0
0x47FC8E: mov      eax, dword ptr [ebp - 0x38]
0x47FC91: push     eax
0x47FC92: mov      eax, dword ptr [ebp - 8]
0x47FC95: call     0x44d8b4
0x47FC9A: mov      edx, dword ptr [ebp - 0x38]
0x47FC9D: call     0x4181ac
0x47FCA2: sar      eax, 1
0x47FCA4: jns      0x47fca9
0x47FCA6: adc      eax, 0
0x47FCA9: mov      edx, 0x195
0x47FCAE: sub      edx, eax
0x47FCB0: mov      ecx, 8
0x47FCB5: mov      eax, dword ptr [ebp - 8]
0x47FCB8: call     0x4a09a0
0x47FCBD: mov      eax, dword ptr [ebp - 8]
0x47FCC0: call     0x44d8b4
0x47FCC5: call     0x44d104
0x47FCCA: inc      dword ptr [ebp - 0xc]
0x47FCCD: cmp      dword ptr [ebp - 0xc], 2
0x47FCD1: jne      0x47f2cc
0x47FCD7: xor      eax, eax
0x47FCD9: pop      edx
0x47FCDA: pop      ecx
0x47FCDB: pop      ecx
0x47FCDC: mov      dword ptr fs:[eax], edx
0x47FCDF: push     0x47fcfc
0x47FCE4: lea      eax, [ebp - 0x4c]
0x47FCE7: call     0x403a58
0x47FCEC: lea      eax, [ebp - 0x38]
0x47FCEF: call     0x403a58
0x47FCF4: ret      
0x47FCF5: jmp      0x403518
0x47FCFA: jmp      0x47fce4
0x47FCFC: mov      esp, ebp
0x47FCFE: pop      ebp
0x47FCFF: ret      
0x47FD00: push     ebp
0x47FD01: mov      ebp, esp
0x47FD03: add      esp, -8
0x47FD06: test     dl, dl
0x47FD08: je       0x47fd12
0x47FD0A: add      esp, -0x10
0x47FD0D: call     0x4031a4
0x47FD12: mov      byte ptr [ebp - 5], dl
0x47FD15: mov      dword ptr [ebp - 4], eax
0x47FD18: mov      cl, 6
0x47FD1A: xor      edx, edx
0x47FD1C: mov      eax, dword ptr [ebp - 4]
0x47FD1F: call     0x47ad80
0x47FD24: mov      eax, dword ptr [ebp - 4]
0x47FD27: cmp      byte ptr [ebp - 5], 0
0x47FD2B: je       0x47fd3c
0x47FD2D: call     0x4031fc
0x47FD32: pop      dword ptr fs:[0]
0x47FD39: add      esp, 0xc
0x47FD3C: mov      eax, dword ptr [ebp - 4]
0x47FD3F: pop      ecx
0x47FD40: pop      ecx
0x47FD41: pop      ebp
0x47FD42: ret      
0x47FD43: nop      
0x47FD44: push     ebp
0x47FD45: mov      ebp, esp
0x47FD47: add      esp, -8
0x47FD4A: call     0x403204
0x47FD4F: mov      byte ptr [ebp - 5], dl
0x47FD52: mov      dword ptr [ebp - 4], eax
0x47FD55: mov      dl, byte ptr [ebp - 5]
0x47FD58: and      dl, 0xfc
0x47FD5B: mov      eax, dword ptr [ebp - 4]
0x47FD5E: call     0x402e9c
0x47FD63: cmp      byte ptr [ebp - 5], 0
0x47FD67: jle      0x47fd71
0x47FD69: mov      eax, dword ptr [ebp - 4]
0x47FD6C: call     0x4031f4
0x47FD71: pop      ecx
0x47FD72: pop      ecx
0x47FD73: pop      ebp
0x47FD74: ret      
0x47FD75: lea      eax, [eax]
0x47FD78: push     ebp
0x47FD79: mov      ebp, esp
0x47FD7B: xor      eax, eax
0x47FD7D: push     ebp
0x47FD7E: push     0x47fd9d
0x47FD83: push     dword ptr fs:[eax]
0x47FD86: mov      dword ptr fs:[eax], esp
0x47FD89: inc      dword ptr [0x4f95c8]
0x47FD8F: xor      eax, eax
0x47FD91: pop      edx
0x47FD92: pop      ecx
0x47FD93: pop      ecx
0x47FD94: mov      dword ptr fs:[eax], edx
0x47FD97: push     0x47fda4
0x47FD9C: ret      
0x47FD9D: jmp      0x403518
0x47FDA2: jmp      0x47fd9c
0x47FDA4: pop      ebp
0x47FDA5: ret      
0x47FDA6: mov      eax, eax
0x47FDA8: sub      dword ptr [0x4f95c8], 1
0x47FDAF: ret      
0x47FDB8: [STR len=11] "物理攻击力 "
0x47FDCC: [STR len=15] "魔法力 (魔法师)"
0x47FDE4: [STR len=13] "精神力 (道士)"
0x47FDFC: [STR len=7] "防御力 "
0x47FE0C: [STR len=10] "魔法防御力"
0x47FE20: [STR len=5] "体力 "
0x47FE30: [STR len=6] "魔法力"
0x47FE40: [STR len=7] "准确度 "
0x47FE50: [STR len=7] "躲避率 "
0x47FE58: pop      esp
0x47FE59: inc      byte ptr [edi]
0x47FE5C: push     cs
0x47FE5D: or       edx, dword ptr [ebx + eax*2 + 0x6c]
0x47FE61: imul     esp, dword ptr [ebx + 0x6b], 0x6e696f50
0x47FE68: je       0x47fe7e
0x47FE6A: add      byte ptr [eax], al
0x47FE6C: add      byte ptr [ecx], al
0x47FE6E: add      byte ptr [eax], al
0x47FE70: add      byte ptr [eax + edx + 0x100040], dl
0x47FE77: add      byte ptr [eax], al
0x47FE79: lea      eax, [eax]
0x47FE7C: enter    0x47fe, 0
0x47FE80: add      byte ptr [eax], al
0x47FE82: add      byte ptr [eax], al
0x47FE84: add      byte ptr [eax], al
0x47FE86: add      byte ptr [eax], al
0x47FE88: pushfd   
0x47FE89: inc      dword ptr [edi]
0x47FE8C: jo       0x47fea7
0x47FE8E: dec      eax
0x47FE8F: add      dh, bh
0x47FE91: inc      dword ptr [edi]
0x47FE94: ja       0x47fea2
0x47FE96: dec      eax
0x47FE97: add      byte ptr [eax], al
0x47FE99: add      byte ptr [eax], al
0x47FE9B: add      byte ptr [ebx + 0x19], dl
0x47FE9E: dec      eax
0x47FE9F: add      al, al
0x47FEA1: or       dword ptr [eax], eax
0x47FEA3: add      byte ptr [ebx + ebx*4 + 0x509c0043], bh
0x47FEAA: inc      ecx
0x47FEAB: add      byte ptr [eax - 0xfffbc36], dh
0x47FEB1: retf     
0x47FEB2: inc      ebx
0x47FEB3: add      ah, bl
0x47FEB5: xor      byte ptr [eax], al
0x47FEB8: and      dh, ch
0x47FEBA: inc      ebx
0x47FEBB: add      byte ptr [eax + 0x2e], al
0x47FEBE: inc      eax
0x47FEBF: add      byte ptr [esi + ebp + 0x40], dl
0x47FEC3: add      byte ptr [esp + ecx*8 + 0x43], dl
