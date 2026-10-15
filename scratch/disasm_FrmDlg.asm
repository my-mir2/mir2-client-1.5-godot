
; === VMT_TFrmDlg @ 0x47FEC8 ===
0x47FEC8: in       al, dx
0x47FEC9: loop     0x47ff0d
0x47FECB: add      byte ptr [eax - 0x2f], bl
0x47FECE: inc      ebx
0x47FECF: add      byte ptr [ebp + edi*8 - 0x31e7ffc0], bh
0x47FED6: inc      ebx
0x47FED7: add      byte ptr [eax - 0x32], dl
0x47FEDA: inc      ebx
0x47FEDB: add      byte ptr [edi + ecx*8 + 0x43], al
0x47FEDF: add      byte ptr [edi + esi*2 + 0x42], dl
0x47FEE3: add      byte ptr [eax + edx*2 - 0x289bffbf], dh
0x47FEEA: inc      ebx
0x47FEEB: add      al, dh
0x47FEED: dec      esp
0x47FEEE: inc      ecx
0x47FEEF: add      byte ptr [esi + ecx - 0x36d7ffbc], ch
0x47FEF6: inc      ebx
0x47FEF7: add      al, al
0x47FEF9: fld      dword ptr [edx]
0x47FEFC: hlt      
0x47FEFD: fld      qword ptr [edx]
0x47FF00: sbb      al, 0xe3
0x47FF02: inc      edx
0x47FF03: add      byte ptr [eax - 0x1e], cl
0x47FF06: inc      edx
0x47FF07: add      ah, bl
0x47FF09: xlatb    
0x47FF0A: inc      edx
0x47FF0B: add      byte ptr [esp + edx*8 + 0x43], al
0x47FF0F: add      byte ptr [eax], dl
0x47FF11: aad      0x42
0x47FF13: add      ah, ah
0x47FF15: insd     dword ptr es:[edi], dx
0x47FF16: inc      edx
0x47FF17: add      al, ch
0x47FF19: insd     dword ptr es:[edi], dx
0x47FF1A: inc      edx
0x47FF1B: add      byte ptr [eax], bh
0x47FF1D: aad      0x43
0x47FF1F: add      byte ptr [eax - 0x67], ah
0x47FF22: inc      edx
0x47FF23: add      byte ptr [eax + 0x6f], al
0x47FF26: inc      edx
0x47FF27: add      byte ptr [ecx + edi*2], al
0x47FF2A: inc      edx
0x47FF2B: add      byte ptr [edi + edx*8], ch
0x47FF2E: inc      ebx
0x47FF2F: add      al, bh
0x47FF31: aad      0x43
0x47FF33: add      byte ptr [edx + edi*2 + 0x42], ah
0x47FF37: add      al, ch
0x47FF39: xlatb    
0x47FF3A: inc      ebx
0x47FF3B: add      ah, ah
0x47FF3D: cdq      
0x47FF3E: inc      edx
0x47FF3F: add      byte ptr [eax + 0x240042d5], ch
0x47FF45: salc     
0x47FF46: inc      edx
0x47FF47: add      byte ptr [eax - 0x2f], dl
0x47FF4A: inc      edx
0x47FF4B: add      byte ptr [eax], cl
0x47FF4D: salc     
0x47FF4E: inc      edx
0x47FF4F: add      byte ptr [eax], bl
0x47FF51: mov      dword ptr [ebx], 0x43de64
0x47FF58: clc      
0x47FF59: lodsd    eax, dword ptr [esi]
0x47FF5A: inc      edx
0x47FF5B: add      byte ptr [ecx + ebp*8 - 0x1323ffbd], bl
0x47FF62: inc      ebx
0x47FF63: add      byte ptr [ebx + ebp*8 - 0x1223ffbd], bl
0x47FF6A: inc      ebx
0x47FF6B: add      ah, bh
0x47FF6D: lodsb    al, byte ptr [esi]
0x47FF6E: inc      edx
0x47FF6F: add      byte ptr [eax - 0x29], cl
0x47FF72: inc      edx
0x47FF73: add      ah, al
0x47FF75: hlt      
0x47FF76: inc      ebx
0x47FF77: add      al, al
0x47FF79: rol      dword ptr [edx], cl
0x47FF7C: mov      word ptr [edi], es
0x47FF7E: inc      esp
0x47FF7F: add      byte ptr [ecx + eax*8 + 0x43], bh
0x47FF83: add      byte ptr [ebx + ecx], dh
0x47FF86: inc      esp
0x47FF87: add      al, al
0x47FF89: retf     0x43
0x47FF8C: pushal   
0x47FF8D: push     es
0x47FF8E: inc      esp
0x47FF8F: add      byte ptr [eax + 0x240043ef], ch
0x47FF95: or       byte ptr [eax + eax + 0x28], al
0x47FF99: xlatb    
0x47FF9A: inc      ebx
0x47FF9B: add      byte ptr [esi], cl
0x47FF9D: add      byte ptr [eax], al
0x47FF9F: add      byte ptr [eax], al
0x47FFA1: add      byte ptr [ebx], cl
0x47FFA3: add      byte ptr [eax], al
0x47FFA5: add      byte ptr [eax + edx + 0x6080040], dl
0x47FFAC: add      byte ptr [eax], al
0x47FFAE: xchg     esp, eax
0x47FFAF: adc      byte ptr [eax], al
0x47FFB2: sbb      al, 6
0x47FFB4: add      byte ptr [eax], al
0x47FFB6: xchg     esp, eax
0x47FFB7: adc      byte ptr [eax], al
0x47FFBA: inc      esp
0x47FFBB: pop      es
0x47FFBC: add      byte ptr [eax], al
0x47FFBE: xchg     esp, eax
0x47FFBF: adc      byte ptr [eax], al
0x47FFC2: dec      esp
0x47FFC3: pop      es
0x47FFC4: add      byte ptr [eax], al
0x47FFC6: xchg     esp, eax
0x47FFC7: adc      byte ptr [eax], al
0x47FFCA: pop      eax
0x47FFCB: pop      es
0x47FFCC: add      byte ptr [eax], al
0x47FFCE: xchg     esp, eax
0x47FFCF: adc      byte ptr [eax], al
0x47FFD2: insb     byte ptr es:[edi], dx
0x47FFD3: pop      es
0x47FFD4: add      byte ptr [eax], al
0x47FFD6: xchg     esp, eax
0x47FFD7: adc      byte ptr [eax], al
0x47FFDA: add      byte ptr [edi], 0
0x47FFDD: add      byte ptr [eax + edx + 0x7840040], dl
0x47FFE4: add      byte ptr [eax], al
0x47FFE6: xchg     esp, eax
0x47FFE7: adc      byte ptr [eax], al
0x47FFEA: xchg     esp, eax
0x47FFEB: or       dword ptr [eax], eax
0x47FFED: add      byte ptr [eax + edx + 0x9980040], dl
0x47FFF4: add      byte ptr [eax], al
0x47FFF6: xchg     esp, eax
0x47FFF7: adc      byte ptr [eax], al
0x47FFFA: mov      ah, 9
0x47FFFC: add      byte ptr [eax], al
0x47FFFE: mov      byte ptr [eax], 0x5b
0x480001: sbb      dword ptr [eax], ecx
0x480004: les      eax, ptr [edx]
0x480006: add      byte ptr [eax], al
0x480008: add      byte ptr [eax], al
0x48000A: or       dword ptr [ebx + edx*2 + 0x74], eax
0x48000E: popal    
0x48000F: je       0x480076
0x480011: push     edi
0x480012: imul     ebp, dword ptr [esi - 0x38], 2
0x480019: add      byte ptr [ebx], cl
0x48001B: inc      esp
0x48001C: inc      edx
0x48001D: popal    
0x48001E: arpl     word ptr [ebx + 0x67], bp
0x480021: jb       0x480092
0x480023: jne      0x480093
0x480025: int3     
0x480027: add      al, byte ptr [eax]
0x480029: add      byte ptr [eax], al
0x48002B: add      byte ptr [eax], cl
0x48002D: inc      esp
0x48002E: dec      ecx
0x48002F: je       0x480096
0x480031: insd     dword ptr es:[edi], dx
0x480032: inc      edx
0x480033: popal    
0x480034: rol      byte ptr [bp + si], 1
0x480037: add      byte ptr [eax], al
0x480039: add      byte ptr [eax], al
0x48003B: pop      es
0x48003C: inc      esp
0x48003D: inc      edx
0x48003E: outsd    dx, dword ptr [esi]
0x48003F: je       0x4800b5
0x480041: outsd    dx, dword ptr [esi]
0x480042: insd     dword ptr es:[edi], dx
0x480043: aam      2
0x480045: add      byte ptr [eax], al
0x480047: add      dword ptr [eax], eax
0x480049: or       byte ptr [ebp + ecx*2 + 0x79], al
0x48004D: push     ebx
0x48004E: je       0x4800b1 ; "DCloseBag"
0x480050: je       0x4800b7
0x480052: fadd     dword ptr [edx]
0x480054: add      byte ptr [eax], al
0x480056: add      dword ptr [eax], eax
0x480058: push     es
0x480059: inc      esp
0x48005A: dec      ebp
0x48005B: jns      0x48009f ; "DRepairItem"
0x48005D: popal    
0x48005E: fadd     qword ptr [bp + si]
0x480061: add      byte ptr [eax], al
0x480063: add      dword ptr [eax], eax
0x480065: or       byte ptr [ebp + ecx*2 + 0x79], al
0x480069: dec      ebp
0x48006A: popal    
0x48006B: imul     esp, dword ptr [bp + di - 0x20], 0x1000002
0x480073: add      byte ptr [edi], al
0x480075: inc      esp
0x480076: dec      edi
0x480077: jo       0x4800ed
0x480079: imul     ebp, dword ptr [edi + 0x6e], 0x2e4
0x480080: add      dword ptr [eax], eax
0x480082: add      eax, 0x6c6f4744
0x480087: call     0x148008f
0x48008D: add      byte ptr [edx], cl
0x48008F: inc      esp
0x480090: push     eax
0x480091: jb       0x4800f8
0x480093: jbe      0x4800e8
0x480095: je       0x4800f8
0x480097: je       0x4800fe
0x480099: in       al, dx
0x48009A: add      al, byte ptr [eax]
0x48009C: add      byte ptr [ecx], al
0x48009E: add      byte ptr [ebx], cl
0x4800A0: inc      esp
0x4800A1: push     edx
0x4800A2: jo       0x480106
0x4800A5: imul     esi, dword ptr [edx + 0x49], 0xf06d6574
0x4800AC: add      al, byte ptr [eax]
0x4800AE: add      byte ptr [ecx], al
0x4800B0: add      byte ptr [ecx], cl
0x4800B2: inc      esp
0x4800B3: inc      ebx
0x4800B4: insb     byte ptr es:[edi], dx
0x4800B5: outsd    dx, dword ptr [esi]
0x4800B6: jae      0x48011d
0x4800B8: inc      edx
0x4800B9: popal    
0x4800BA: hlt      
0x4800BC: add      al, byte ptr [eax]
0x4800BE: add      byte ptr [ecx], al
0x4800C0: add      byte ptr [ebx], cl
0x4800C2: inc      esp
0x4800C3: inc      ebx
0x4800C4: insb     byte ptr es:[edi], dx
0x4800C5: outsd    dx, dword ptr [esi]
0x4800C6: jae      0x48012d
0x4800C8: push     ebx
0x4800C9: je       0x48012c
0x4800CB: je       0x480132
0x4800CD: clc      
0x4800CE: add      al, byte ptr [eax]
0x4800D0: add      byte ptr [eax], al
0x4800D2: add      byte ptr [esi], al
0x4800D4: inc      esp
0x4800D5: dec      esp
0x4800D6: outsd    dx, dword ptr [esi]
0x4800D7: dec      ecx
0x4800D9: outsb    dx, byte ptr [esi]
0x4800DA: cld      
0x4800DB: add      al, byte ptr [eax]
0x4800DD: add      byte ptr [ecx], al
0x4800DF: add      byte ptr [ecx], cl
0x4800E1: inc      esp
0x4800E2: dec      esp
0x4800E3: outsd    dx, dword ptr [esi]
0x4800E4: imul     ebp, dword ptr [bp + 0x4e], 0x3007765
0x4800EC: add      byte ptr [eax], al
0x4800EE: add      dword ptr [eax], eax
0x4800F0: or       byte ptr [esp + ecx*2 + 0x6f], al
0x4800F4: imul     ebp, dword ptr [bp + 0x4f], 0x3046b
0x4800FC: add      byte ptr [eax], al
0x4800FE: add      byte ptr [ebx], cl
0x480100: inc      esp
0x480101: dec      esi
0x480102: ja       0x480146
0x480105: arpl     word ptr [ebx + 0x6f], sp
0x480108: jne      0x480178
0x48010A: je       0x480114
0x48010C: add      eax, dword ptr [eax]
0x48010E: add      byte ptr [ecx], al
0x480110: add      byte ptr [0x77654e44], cl
0x480116: inc      ecx
0x480117: arpl     word ptr [ebx + 0x6f], sp
0x48011A: jne      0x48018a
0x48011C: je       0x48016d
0x48011E: imul     ecx, dword ptr [ebx + eax], 0
0x480122: add      byte ptr [ecx], al
0x480124: add      byte ptr [ebx], cl
0x480126: inc      esp
0x480127: dec      esp
0x480128: outsd    dx, dword ptr [esi]
0x480129: imul     ebp, dword ptr [bp + 0x43], 0x65736f6c
0x480131: adc      byte ptr [ebx], al
0x480133: add      byte ptr [eax], al
0x480135: add      dword ptr [eax], eax
0x480137: adc      byte ptr [esi + ecx*2 + 0x65], al
0x48013B: ja       0x48017e
0x48013D: arpl     word ptr [ebx + 0x6f], sp
0x480140: jne      0x4801b0
0x480142: je       0x480187
0x480144: insb     byte ptr es:[edi], dx
0x480145: outsd    dx, dword ptr [esi]
0x480146: jae      0x4801ad
0x480148: adc      al, 3
0x48014A: add      byte ptr [eax], al
0x48014C: add      byte ptr [eax], al
0x48014E: or       al, byte ptr [ebx + edx*2 + 0x65]
0x480152: insb     byte ptr es:[edi], dx
0x480153: arpl     word ptr gs:[ebx + eax*2 + 0x68], si
0x480158: jb       0x480172
0x48015A: add      eax, dword ptr [eax]
0x48015C: add      byte ptr [ecx], al
0x48015E: add      byte ptr [edx], cl
0x480160: inc      esp
0x480161: jae      0x4801c6
0x480163: push     ebx
0x480164: insb     byte ptr es:[edi], dx
0x480166: arpl     word ptr gs:[ecx + esi + 0x1c], si
0x48016B: add      eax, dword ptr [eax]
0x48016D: add      byte ptr [ecx], al
0x48016F: add      byte ptr [edx], cl
0x480171: inc      esp
0x480172: jae      0x4801d7
0x480174: push     ebx
0x480175: insb     byte ptr es:[edi], dx
0x480177: arpl     word ptr gs:[edx + esi + 0x20], si
0x48017C: add      eax, dword ptr [eax]
0x48017E: add      byte ptr [ecx], al
0x480180: add      byte ptr [eax], cl
0x480182: inc      esp
0x480183: jae      0x4801e8
0x480185: push     ebx
0x480186: je       0x4801e9
0x480188: jb       0x4801fe
0x48018A: and      al, 3
0x48018C: add      byte ptr [eax], al
0x48018E: add      dword ptr [eax], eax
0x480190: or       dword ptr [ebx + esi*2 + 0x63], eax
0x480194: dec      esi
0x480195: ja       0x4801db
0x480198: push     0x32872
0x48019D: add      byte ptr [ecx], al
0x48019F: add      byte ptr [ebx], cl
0x4801A1: inc      esp
0x4801A2: jae      0x480207
0x4801A4: inc      ebp
0x4801A5: jb       0x480208
0x4801A7: jae      0x48020e
0x4801A9: inc      ebx
0x4801AA: push     0x32c72
0x4801AF: add      byte ptr [ecx], al
0x4801B1: add      byte ptr [edx], cl
0x4801B3: inc      esp
0x4801B4: jae      0x480219
0x4801B6: inc      ebx
0x4801B7: jb       0x48021e
0x4801B9: imul     esi, dword ptr fs:[ebx + esi*2 + 0x30], 0x1000003
0x4801C2: add      byte ptr [edi], al
0x4801C4: inc      esp
0x4801C5: jae      0x48022a
0x4801C7: inc      ebp
0x4801C8: js       0x480233
0x4801CA: je       0x480200
0x4801CC: add      eax, dword ptr [eax]
0x4801CE: add      byte ptr [eax], al
0x4801D0: add      byte ptr [edx], cl
0x4801D2: inc      esp
0x4801D3: inc      ebx
0x4801D4: jb       0x48023b
0x4801D6: popal    
0x4801D7: je       0x48023e
0x4801D9: inc      ebx
0x4801DA: push     0x33872
0x4801DF: add      byte ptr [ecx], al
0x4801E1: add      byte ptr [edx], cl
0x4801E3: inc      esp
0x4801E4: arpl     word ptr [ebx + 0x57], sp
0x4801E7: popal    
0x4801E8: jb       0x48025c
0x4801EA: imul     ebp, dword ptr [edi + 0x72], 0x33c
0x4801F1: add      dword ptr [eax], eax
0x4801F3: or       al, byte ptr [ebx + 0x63]
0x4801F7: push     edi
0x4801F8: imul     edi, dword ptr [edx + 0x7a], 0x40647261
0x4801FF: add      eax, dword ptr [eax]
0x480201: add      byte ptr [ecx], al
0x480203: add      byte ptr [edi], al
0x480205: inc      esp
0x480206: arpl     word ptr [ebx + 0x4d], sp
0x480209: outsd    dx, dword ptr [esi]
0x48020A: outsb    dx, byte ptr [esi]
0x48020B: imul     eax, dword ptr [ebx + eax], 0
0x480210: add      dword ptr [eax], eax
0x480212: or       eax, dword ptr [ebx + 0x63]
0x480216: push     edx
0x480217: jae      0x48027f
0x48021A: jb       0x480292 ; "DLoginChgPw"
0x48021C: dec      eax
0x48021F: add      eax, dword ptr [eax]
0x480221: add      byte ptr [ecx], al
0x480223: add      byte ptr [edi], al
0x480225: inc      esp
0x480226: arpl     word ptr [ebx + 0x4d], sp
0x480229: popal    
0x48022A: insb     byte ptr es:[edi], dx
0x48022B: dec      esp
0x48022D: add      eax, dword ptr [eax]
0x48022F: add      byte ptr [ecx], al
0x480231: add      byte ptr [ecx], cl
0x480233: inc      esp
0x480234: arpl     word ptr [ebx + 0x46], sp
0x480237: insd     dword ptr es:[edi], dx
0x480239: popal    
0x48023A: insb     byte ptr es:[edi], dx
0x48023B: push     eax
0x48023D: add      eax, dword ptr [eax]
0x48023F: add      byte ptr [ecx], al
0x480241: add      byte ptr [ebx], cl
0x480243: inc      esp
0x480244: arpl     word ptr [ebx + 0x4c], sp
0x480247: je       0x480293
0x48024B: popal    
0x48024C: imul     esi, dword ptr [edx + 0x54], 0x1000003
0x480253: add      byte ptr [esp + eax*2], cl
0x480256: arpl     word ptr [ebx + 0x52], sp
0x480259: imul     esp, dword ptr [edi + 0x68], 0x69614874
0x480260: jb       0x4802ba
0x480262: add      eax, dword ptr [eax]
0x480264: add      byte ptr [ecx], al
0x480266: add      byte ptr [0x4f636344], al
0x48026C: imul     ebx, dword ptr [ebx + eax], 0
0x480271: add      dword ptr [eax], eax
0x480273: or       byte ptr [ebx + 0x63], al
0x480277: inc      ebx
0x480278: insb     byte ptr es:[edi], dx
0x480279: outsd    dx, dword ptr [esi]
0x48027A: jae      0x4802e1
0x48027C: pushal   
0x48027D: add      eax, dword ptr [eax]
0x48027F: add      byte ptr [edx], al
0x480281: add      byte ptr [ecx], cl
0x480283: inc      esp
0x480284: dec      ecx
0x480285: je       0x4802ec
0x480287: insd     dword ptr es:[edi], dx
0x480288: inc      edi
0x480289: jb       0x4802f4
0x48028B: add      eax, dword ptr fs:[eax]
0x48028F: add      byte ptr [ecx], al
0x480291: add      byte ptr [ebx], cl
0x480293: inc      esp
0x480294: dec      esp
0x480295: outsd    dx, dword ptr [esi]
0x480296: imul     ebp, dword ptr [bp + 0x43], 0x77506768
0x48029E: push     3
0x4802A3: add      byte ptr [edi], al
0x4802A5: inc      esp
0x4802A6: dec      ebp
0x4802A7: jae      0x480310
0x4802A9: inc      esp
0x4802AA: insb     byte ptr es:[edi], dx
0x4802AB: insb     byte ptr es:[di], dx
0x4802AD: add      eax, dword ptr [eax]
0x4802AF: add      byte ptr [ecx], al
0x4802B1: add      byte ptr [ecx], cl
0x4802B3: inc      esp
0x4802B4: dec      ebp
0x4802B5: jae      0x48031e
0x4802B7: inc      esp
0x4802B8: insb     byte ptr es:[edi], dx
0x4802B9: dec      edi
0x4802BB: imul     esi, dword ptr [eax + 3], 0
0x4802BF: add      byte ptr [ecx], al
0x4802C1: add      byte ptr [edx], cl
0x4802C3: inc      esp
0x4802C4: dec      ebp
0x4802C5: jae      0x48032e
0x4802C7: inc      esp
0x4802C8: insb     byte ptr es:[edi], dx
0x4802C9: pop      ecx
0x4802CB: jae      0x480342
0x4802CE: add      eax, dword ptr [eax]
0x4802D0: add      byte ptr [ecx], al
0x4802D2: add      byte ptr [0x67734d44], cl
0x4802D8: inc      esp
0x4802D9: insb     byte ptr es:[edi], dx
0x4802DA: inc      ebx
0x4802DC: popal    
0x4802DD: outsb    dx, byte ptr [esi]
0x4802DE: arpl     word ptr [ebp + 0x6c], sp
0x4802E1: js       0x4802e6
0x4802E3: add      byte ptr [eax], al
0x4802E5: add      dword ptr [eax], eax
0x4802E7: or       dword ptr [ebp + ecx*2 + 0x73], eax
0x4802EB: inc      esp
0x4802ED: insb     byte ptr es:[edi], dx
0x4802EE: dec      esi
0x4802F0: outsd    dx, dword ptr [esi]
0x4802F1: jl       0x4802f6
0x4802F3: add      byte ptr [eax], al
0x4802F5: add      dword ptr [eax], eax
0x4802F7: or       al, byte ptr [esi + ecx*2 + 0x65]
0x4802FB: js       0x480371
0x4802FD: push     ebx
0x4802FE: je       0x480361
0x480300: je       0x480367
0x480302: add      byte ptr [ebx], 0
0x480305: add      byte ptr [ecx], al
0x480307: add      byte ptr [ebx], cl
0x480309: inc      esp
0x48030A: push     ebx
0x48030B: push     edi
0x48030C: dec      esi
0x48030D: arpl     word ptr gs:[ebx + 0x6c], bp
0x480311: popal    
0x480312: arpl     word ptr [ebp - 0x7c], sp
0x480315: add      eax, dword ptr [eax]
0x480317: add      byte ptr [ecx], al
0x480319: add      byte ptr [eax], cl
0x48031B: inc      esp
0x48031C: push     ebx
0x48031D: push     edi
0x48031E: dec      esp
0x48031F: imul     esp, dword ptr [edi + 0x68], 0x38874
0x480326: add      byte ptr [ecx], al
0x480328: add      byte ptr [ebx], cl
0x48032A: inc      esp
0x48032B: push     ebx
0x48032C: push     edi
0x48032D: inc      ecx
0x48032E: jb       0x48039d
0x480330: push     edx
0x480331: imul     ebp, dword ptr [esi + 0x67], 0x38c52
0x480338: add      byte ptr [ecx], al
0x48033A: add      byte ptr [ebx], cl
0x48033C: inc      esp
0x48033D: push     ebx
0x48033E: push     edi
0x48033F: inc      ecx
0x480340: jb       0x4803af
0x480342: push     edx
0x480343: imul     ebp, dword ptr [esi + 0x67], 0x3904c
0x48034A: add      byte ptr [ecx], al
0x48034C: add      byte ptr [eax], cl
0x48034E: inc      esp
0x48034F: push     ebx
0x480350: push     edi
0x480351: push     edx
0x480352: imul     ebp, dword ptr [esi + 0x67], 0x39452
0x480359: add      byte ptr [ecx], al
0x48035B: add      byte ptr [eax], cl
0x48035D: inc      esp
0x48035E: push     ebx
0x48035F: push     edi
0x480360: push     edx
0x480361: imul     ebp, dword ptr [esi + 0x67], 0x3984c
0x480368: add      byte ptr [ecx], al
0x48036A: add      byte ptr [ecx], cl
0x48036C: inc      esp
0x48036D: push     ebx
0x48036E: push     edi
0x48036F: push     edi
0x480370: popal    
0x480372: jo       0x4803e3
0x480374: outsb    dx, byte ptr [esi]
0x480375: pushfd   
0x480376: add      eax, dword ptr [eax]
0x480378: add      byte ptr [ecx], al
0x48037A: add      byte ptr [eax], cl
0x48037C: inc      esp
0x48037D: push     ebx
0x48037E: push     edi
0x48037F: inc      esp
0x480380: jb       0x4803e7
0x480382: jae      0x4803f7
0x480384: mov      al, byte ptr [0x1000003]
0x480389: add      byte ptr [ecx], cl
0x48038B: inc      esp
0x48038C: push     ebx
0x48038D: push     edi
0x48038E: dec      eax
0x48038F: insb     byte ptr es:[edi], dx
0x480391: insd     dword ptr es:[edi], dx
0x480392: je       0x480339
0x480395: add      eax, dword ptr [eax]
0x480397: add      byte ptr [ecx], al
0x480399: add      byte ptr [esi], al
0x48039B: inc      esp
0x48039C: inc      edx
0x48039D: insb     byte ptr es:[edi], dx
0x48039F: je       0x4803d2
0x4803A1: test     al, 3
0x4803A3: add      byte ptr [eax], al
0x4803A5: add      dword ptr [eax], eax
0x4803A7: push     es
0x4803A8: inc      esp
0x4803A9: inc      edx
0x4803AA: insb     byte ptr es:[edi], dx
0x4803AC: je       0x4803e0
0x4803AE: lodsb    al, byte ptr [esi]
0x4803AF: add      eax, dword ptr [eax]
0x4803B1: add      byte ptr [ecx], al
0x4803B3: add      byte ptr [esi], al
0x4803B5: inc      esp
0x4803B6: inc      edx
0x4803B7: insb     byte ptr es:[edi], dx
0x4803B9: je       0x4803ee
0x4803BB: mov      al, 3
0x4803BD: add      byte ptr [eax], al
0x4803BF: add      dword ptr [eax], eax
0x4803C1: push     es
0x4803C2: inc      esp
0x4803C3: inc      edx
0x4803C4: insb     byte ptr es:[edi], dx
0x4803C6: je       0x4803fc
0x4803C8: mov      ah, 3
0x4803CA: add      byte ptr [eax], al
0x4803CC: add      dword ptr [eax], eax
0x4803CE: push     es
0x4803CF: inc      esp
0x4803D0: inc      edx
0x4803D1: insb     byte ptr es:[edi], dx
0x4803D3: je       0x48040a
0x4803D5: mov      eax, 0x1000003
0x4803DA: add      byte ptr [esi], al
0x4803DC: inc      esp
0x4803DD: inc      edx
0x4803DE: insb     byte ptr es:[edi], dx
0x4803E0: je       0x480418
0x4803E2: mov      esp, 3
0x4803E7: add      byte ptr [esi], al
0x4803E9: inc      esp
0x4803EA: inc      ebx
0x4803EB: push     0xc0775067
0x4803F0: add      eax, dword ptr [eax]
0x4803F2: add      byte ptr [ecx], al
0x4803F4: add      byte ptr [eax], cl
0x4803F6: inc      esp
0x4803F7: inc      ebx
0x4803F8: push     0x4f777067
0x4803FD: imul     eax, esp, 3
0x480400: add      byte ptr [eax], al
0x480402: add      dword ptr [eax], eax
0x480404: or       al, 0x44
0x480406: inc      ebx
0x480407: push     0x43777067
0x48040C: popal    
0x48040D: outsb    dx, byte ptr [esi]
0x48040E: arpl     word ptr [ebp + 0x6c], sp
0x480411: enter    3, 0
0x480415: add      byte ptr [eax], al
0x480417: or       al, 0x44
0x480419: dec      ebp
0x48041A: jb       0x480480 ; "DMenuClose"
0x48041D: push     0x44746e61
0x480422: insb     byte ptr es:[edi], dx
0x480423: int3     
0x480425: add      eax, dword ptr [eax]
0x480427: add      byte ptr [ecx], al
0x480429: add      byte ptr [ecx], dl
0x48042B: inc      esp
0x48042C: dec      ebp
0x48042D: jb       0x480493
0x480430: push     0x44746e61
0x480435: insb     byte ptr es:[edi], dx
0x480436: inc      ebx
0x480438: insb     byte ptr es:[edi], dx
0x480439: outsd    dx, dword ptr [esi]
0x48043A: jae      0x4804a1
0x48043C: rol      byte ptr [ebx], 1
0x48043E: add      byte ptr [eax], al
0x480440: add      byte ptr [eax], al
0x480442: or       byte ptr [ebp + ecx*2 + 0x65], al
0x480446: outsb    dx, byte ptr [esi]
0x480447: jne      0x48048d
0x480449: insb     byte ptr es:[edi], dx
0x48044A: aam      3
0x48044D: add      byte ptr [eax], al
0x48044F: add      dword ptr [eax], eax
0x480451: or       dword ptr [ebp + ecx*2 + 0x65], eax
0x480455: outsb    dx, byte ptr [esi]
0x480456: jne      0x4804a8
0x480458: jb       0x4804bf
0x48045A: jbe      0x480434
0x48045C: add      eax, dword ptr [eax]
0x48045E: add      byte ptr [ecx], al
0x480460: add      byte ptr [ecx], cl
0x480462: inc      esp
0x480463: dec      ebp
0x480464: outsb    dx, byte ptr gs:[esi]
0x480466: jne      0x4804b6
0x480468: js       0x4804df
0x48046B: fadd     qword ptr [ebx]
0x48046D: add      byte ptr [eax], al
0x48046F: add      dword ptr [eax], eax
0x480471: or       byte ptr [ebp + ecx*2 + 0x65], al
0x480475: outsb    dx, byte ptr [esi]
0x480476: jne      0x4804ba
0x480478: jne      0x4804f3
0x48047A: loopne   0x48047f
0x48047C: add      byte ptr [eax], al
0x48047E: add      dword ptr [eax], eax
0x480480: or       al, byte ptr [ebp + ecx*2 + 0x65]
0x480484: outsb    dx, byte ptr [esi]
0x480485: jne      0x4804ca
0x480487: insb     byte ptr es:[edi], dx
0x480488: outsd    dx, dword ptr [esi]
0x480489: jae      0x4804f0
0x48048B: in       al, 3
0x48048D: add      byte ptr [eax], al
0x48048F: add      byte ptr [eax], al
0x480491: or       byte ptr [ebx + edx*2 + 0x65], al
0x480495: insb     byte ptr es:[edi], dx
0x480496: insb     byte ptr es:[edi], dx
0x480497: inc      esp
0x480498: insb     byte ptr es:[edi], dx
0x480499: call     0x14804a2
0x48049F: add      byte ptr [edx], cl
0x4804A1: inc      esp
0x4804A2: push     ebx
0x4804A3: insb     byte ptr es:[edi], dx
0x4804A5: insb     byte ptr es:[edi], dx
0x4804A6: inc      esp
0x4804A7: insb     byte ptr es:[edi], dx
0x4804A8: dec      edi
0x4804AA: imul     ebp, esp, 3
0x4804AD: add      byte ptr [eax], al
0x4804AF: add      dword ptr [eax], eax
0x4804B1: or       eax, 0x6c655344
0x4804B6: insb     byte ptr es:[edi], dx
0x4804B7: inc      esp
0x4804B8: insb     byte ptr es:[edi], dx
0x4804B9: inc      ebx
0x4804BB: insb     byte ptr es:[edi], dx
0x4804BC: outsd    dx, dword ptr [esi]
0x4804BD: jae      0x480524
0x4804BF: lock add eax, dword ptr [eax]
0x4804C2: add      byte ptr [ecx], al
0x4804C4: add      byte ptr [esp + eax*2], cl
0x4804C7: push     ebx
0x4804C8: insb     byte ptr es:[edi], dx
0x4804CA: insb     byte ptr es:[edi], dx
0x4804CB: inc      esp
0x4804CC: insb     byte ptr es:[edi], dx
0x4804CD: push     ebx
0x4804CF: jo       0x480540
0x4804D1: je       0x4804c7
0x4804D3: add      eax, dword ptr [eax]
0x4804D5: add      byte ptr [ecx], al
0x4804D7: add      byte ptr [edi], al
0x4804D9: inc      esp
0x4804DA: push     ebx
0x4804DB: je       0x48052a
0x4804DD: popal    
0x4804DE: xor      eax, edi
0x4804E1: add      eax, dword ptr [eax]
0x4804E3: add      byte ptr [ecx], al
0x4804E5: add      byte ptr [edi], al
0x4804E7: inc      esp
0x4804E8: push     ebx
0x4804E9: je       0x480538
0x4804EB: popal    
0x4804EC: xor      bh, ah
0x4804EF: add      eax, dword ptr [eax]
0x4804F1: add      byte ptr [ecx], al
0x4804F3: add      byte ptr [edi], al
0x4804F5: inc      esp
0x4804F6: push     ebx
0x4804F7: je       0x480546
0x4804F9: popal    
0x4804FA: xor      eax, dword ptr [bx + si]
0x4804FD: add      al, 0
0x4804FF: add      byte ptr [ecx], al
0x480501: add      byte ptr [edi], al
0x480503: inc      esp
0x480504: push     ebx
0x480505: je       0x480554
0x480507: popal    
0x480508: xor      al, 4
0x48050B: add      al, 0
0x48050D: add      byte ptr [ecx], al
0x48050F: add      byte ptr [edi], al
0x480511: inc      esp
0x480512: push     ebx
0x480513: je       0x480562
0x480515: popal    
0x480516: xor      eax, 0x408
0x48051C: add      byte ptr [eax], al
0x48051E: or       al, byte ptr [ebx + ecx*2 + 0x65]
0x480522: jns      0x480577
0x480524: insb     byte ptr es:[edi], dx
0x480526: inc      esp
0x480527: insb     byte ptr es:[edi], dx
0x480528: or       al, 4
0x48052B: add      byte ptr [eax], al
0x48052D: add      dword ptr [eax], eax
0x48052F: pop      es
0x480530: inc      esp
0x480531: dec      ebx
0x480532: jae      0x48057d
0x480534: arpl     word ptr [edi + 0x6e], bp
0x480537: adc      byte ptr [eax + eax], al
0x48053A: add      byte ptr [ecx], al
0x48053C: add      byte ptr [0x46734b44], al
0x480542: xor      dword ptr [esp + eax], edx
0x480545: add      byte ptr [eax], al
0x480547: add      dword ptr [eax], eax
0x480549: add      eax, 0x46734b44
0x48054E: xor      bl, byte ptr [eax]
0x480550: add      al, 0
0x480552: add      byte ptr [ecx], al
0x480554: add      byte ptr [0x46734b44], al
0x48055A: xor      ebx, dword ptr [esp + eax]
0x48055D: add      byte ptr [eax], al
0x48055F: add      dword ptr [eax], eax
0x480561: add      eax, 0x46734b44
0x480566: xor      al, 0x20
0x480568: add      al, 0
0x48056A: add      byte ptr [ecx], al
0x48056C: add      byte ptr [edi], al
0x48056E: inc      esp
0x48056F: dec      ebx
0x480570: jae      0x4805c0
0x480572: outsd    dx, dword ptr [esi]
0x480573: outsb    dx, byte ptr [esi]
0x480574: and      al, 4
0x480577: add      byte ptr [eax], al
0x480579: add      dword ptr [eax], eax
0x48057B: add      eax, 0x4f734b44
0x480580: imul     ebp, dword ptr [eax], 4
0x480583: add      byte ptr [eax], al
0x480585: add      dword ptr [eax], eax
0x480587: or       dword ptr [edx + eax*2 + 0x6f], eax
0x48058B: je       0x4805d4
0x48058D: jb       0x4805fe
0x48058F: jne      0x480601
0x480591: sub      al, 4
0x480593: add      byte ptr [eax], al
0x480595: add      dword ptr [eax], eax
0x480597: or       dword ptr [edx + eax*2 + 0x6f], eax
0x48059B: je       0x4805f1 ; "DGrpCreate"
0x48059D: jb       0x480600
0x48059F: xor      byte ptr gs:[eax + eax], al
0x4805A4: add      byte ptr [ecx], al
0x4805A6: add      byte ptr [ebx], cl
0x4805A8: inc      esp
0x4805A9: inc      edx
0x4805AA: outsd    dx, dword ptr [esi]
0x4805AB: je       0x4805fa
0x4805AD: imul     ebp, dword ptr [esi + 0x69], 0x3470614d
0x4805B4: add      al, 0
0x4805B6: add      byte ptr [eax], al
0x4805B8: add      byte ptr [ecx], cl
0x4805BA: inc      esp
0x4805BB: inc      edi
0x4805BC: jb       0x48062d
0x4805BE: jne      0x480630
0x4805C0: inc      esp
0x4805C1: insb     byte ptr es:[edi], dx
0x4805C2: cmp      byte ptr [si], al
0x4805C5: add      byte ptr [eax], al
0x4805C7: add      dword ptr [eax], eax
0x4805C9: push     cs
0x4805CA: inc      esp
0x4805CB: inc      edi
0x4805CC: jb       0x48063e
0x4805CE: inc      ecx
0x4805CF: insb     byte ptr es:[edi], dx
0x4805D0: insb     byte ptr es:[edi], dx
0x4805D1: outsd    dx, dword ptr [esi]
0x4805D2: ja       0x48061b
0x4805D4: jb       0x480645
0x4805D6: jne      0x480648
0x4805D8: cmp      al, 4
0x4805DA: add      byte ptr [eax], al
0x4805DC: add      dword ptr [eax], eax
0x4805DE: or       al, 0x44
0x4805E0: inc      edi
0x4805E1: jb       0x480653
0x4805E3: inc      esp
0x4805E4: insb     byte ptr es:[edi], dx
0x4805E5: inc      ebx
0x4805E7: insb     byte ptr es:[edi], dx
0x4805E8: outsd    dx, dword ptr [esi]
0x4805E9: jae      0x480650
0x4805EB: inc      eax
0x4805EC: add      al, 0
0x4805EE: add      byte ptr [ecx], al
0x4805F0: add      byte ptr [edx], cl
0x4805F2: inc      esp
0x4805F3: inc      edi
0x4805F4: jb       0x480666
0x4805F6: inc      ebx
0x4805F7: jb       0x48065e
0x4805F9: popal    
0x4805FA: je       0x480661
0x4805FC: inc      esp
0x4805FD: add      al, 0
0x4805FF: add      byte ptr [ecx], al
0x480601: add      byte ptr [edx], cl
0x480603: inc      esp
0x480604: inc      edi
0x480605: jb       0x480677
0x480607: inc      ecx
0x480608: dec      ebp
0x48060B: insd     dword ptr es:[edi], dx
0x48060D: dec      eax
0x48060E: add      al, 0
0x480610: add      byte ptr [ecx], al
0x480612: add      byte ptr [edx], cl
0x480614: inc      esp
0x480615: inc      edi
0x480616: jb       0x480688
0x480618: inc      esp
0x480619: insb     byte ptr es:[edi], dx
0x48061B: dec      ebp
0x48061C: insd     dword ptr es:[edi], dx
0x48061E: dec      esp
0x48061F: add      al, 0
0x480621: add      byte ptr [ecx], al
0x480623: add      byte ptr [edx], cl
0x480625: inc      esp
0x480626: inc      edx
0x480627: outsd    dx, dword ptr [esi]
0x480628: je       0x480676 ; "DDealRemoteDlg"
0x48062A: outsd    dx, dword ptr [esi]
0x48062B: outsd    dx, dword ptr [si]
0x48062D: jne      0x4806a3
0x48062F: push     eax
0x480630: add      al, 0
0x480632: add      byte ptr [ecx], al
0x480634: add      byte ptr [eax], cl
0x480636: inc      esp
0x480637: inc      edx
0x480638: outsd    dx, dword ptr [esi]
0x480639: je       0x480680
0x48063B: js       0x4806a6
0x48063D: je       0x480693
0x48063F: add      al, 0
0x480641: add      byte ptr [ecx], al
0x480643: add      byte ptr [ecx], cl
0x480645: inc      esp
0x480646: inc      edx
0x480647: outsd    dx, dword ptr [esi]
0x480648: je       0x480691
0x48064A: jne      0x4806b5 ; "DDealOk"
0x48064C: insb     byte ptr es:[edi], dx
0x48064D: pop      eax
0x48064F: add      al, 0
0x480651: add      byte ptr [ecx], al
0x480653: add      byte ptr [ecx], cl
0x480655: inc      esp
0x480656: push     ebx
0x480657: je       0x4806a9
0x480659: popal    
0x48065A: push     ebp
0x48065D: jo       0x4806bb
0x48065F: add      al, 0
0x480661: add      byte ptr [ecx], al
0x480663: add      byte ptr [ebx], cl
0x480665: inc      esp
0x480666: push     ebx
0x480667: je       0x4806b9
0x480669: popal    
0x48066A: inc      esp
0x48066D: outsd    dx, dword ptr [esi]
0x48066E: ja       0x4806de
0x480670: pushal   
0x480671: add      al, 0
0x480673: add      byte ptr [eax], al
0x480675: add      byte ptr [esi], cl
0x480677: inc      esp
0x480678: inc      esp
0x480679: popal    
0x48067B: insb     byte ptr es:[edi], dx
0x48067C: push     edx
0x48067D: insd     dword ptr es:[edi], dx
0x48067F: outsd    dx, dword ptr [esi]
0x480680: je       0x4806e7
0x480682: inc      esp
0x480683: insb     byte ptr es:[edi], dx
0x480684: add      al, 0
0x480688: add      byte ptr [eax], al
0x48068A: add      byte ptr [eax], cl
0x48068C: inc      esp
0x48068D: inc      esp
0x48068E: popal    
0x480690: insb     byte ptr es:[edi], dx
0x480691: inc      esp
0x480692: insb     byte ptr es:[edi], dx
0x480693: push     0x2000004
0x480699: add      byte ptr [edi], al
0x48069B: inc      esp
0x48069C: inc      esp
0x48069D: push     edx
0x48069E: inc      edi
0x48069F: jb       0x48070a
0x4806A1: insb     byte ptr es:[edi], dx
0x4806A3: add      al, 0
0x4806A5: add      byte ptr [edx], al
0x4806A7: add      byte ptr [esi], al
0x4806A9: inc      esp
0x4806AA: inc      esp
0x4806AB: inc      edi
0x4806AC: jb       0x480717
0x4806AE: jo       0x4806b5 ; "DDealOk"
0x4806B1: add      byte ptr [eax], al
0x4806B3: add      dword ptr [eax], eax
0x4806B5: pop      es
0x4806B6: inc      esp
0x4806B7: inc      esp
0x4806B8: popal    
0x4806BA: insb     byte ptr es:[edi], dx
0x4806BB: dec      edi
0x4806BC: imul     esi, dword ptr [esp + eax], 0
0x4806C1: add      dword ptr [eax], eax
0x4806C3: or       al, byte ptr [esp + eax*2 + 0x65]
0x4806C7: popal    
0x4806C8: insb     byte ptr es:[edi], dx
0x4806C9: inc      ebx
0x4806CA: insb     byte ptr es:[edi], dx
0x4806CB: outsd    dx, dword ptr [esi]
0x4806CC: jae      0x480733
0x4806CE: js       0x4806d4 ; "DDGold"
0x4806D0: add      byte ptr [eax], al
0x4806D2: add      dword ptr [eax], eax
0x4806D4: push     es
0x4806D5: inc      esp
0x4806D6: inc      esp
0x4806D7: inc      edi
0x4806D8: outsd    dx, dword ptr [esi]
0x4806D9: insb     byte ptr es:[edi], dx
0x4806DA: jl       0x4806e1 ; "DDRGold"
0x4806DD: add      byte ptr [eax], al
0x4806DF: add      dword ptr [eax], eax
0x4806E1: pop      es
0x4806E2: inc      esp
0x4806E3: inc      esp
0x4806E4: push     edx
0x4806E5: inc      edi
0x4806E6: outsd    dx, dword ptr [esi]
0x4806E7: insb     byte ptr es:[edi], dx
0x4806E8: add      byte ptr fs:[eax + eax], 0
0x4806ED: add      byte ptr [eax], al
0x4806EF: or       eax, 0x6c655344
0x4806F4: push     ebx
0x4806F5: jb       0x48076e
0x4806F8: jb       0x48073f
0x4806FB: insb     byte ptr es:[edi], dx
0x4806FC: test     byte ptr [si], al
0x4806FF: add      byte ptr [eax], al
0x480701: add      dword ptr [eax], eax
0x480703: or       al, byte ptr [ebx + edx*2 + 0x53]
0x480707: jb       0x48077f
0x480709: inc      ebx
0x48070A: insb     byte ptr es:[edi], dx
0x48070B: outsd    dx, dword ptr [esi]
0x48070C: jae      0x480773
0x48070E: mov      byte ptr [eax + eax], al
0x480711: add      byte ptr [ecx], al
0x480713: add      byte ptr [ecx], cl
0x480715: inc      esp
0x480716: push     ebx
0x480717: push     ebx
0x480718: jb       0x480791
0x48071B: jb       0x48074f
0x48071E: mov      word ptr [eax + eax], es
0x480721: add      byte ptr [ecx], al
0x480723: add      byte ptr [ecx], cl
0x480725: inc      esp
0x480726: push     ebx
0x480727: push     ebx
0x480728: jb       0x4807a1
0x48072B: jb       0x480760
0x48072E: nop      
0x48072F: add      al, 0
0x480731: add      byte ptr [eax], al
0x480733: add      byte ptr [ebx], cl
0x480735: inc      esp
0x480736: push     ebp
0x480737: jae      0x48079e
0x480739: jb       0x48078e
0x48073B: je       0x48079e
0x48073D: je       0x4807a4
0x48073F: xor      dword ptr [esp + eax + 0x10000], edx
0x480746: or       dword ptr [ebx + eax*2 + 0x6c], eax
0x48074A: outsd    dx, dword ptr [esi]
0x48074B: jae      0x4807b2
0x48074D: push     ebp
0x48074E: push     ebx
0x48074F: xor      dword ptr [eax + 0x1000004], ebx
0x480755: add      byte ptr [edx], cl
0x480757: inc      esp
0x480758: push     edi
0x480759: popal    
0x48075B: jo       0x4807cc
0x48075D: outsb    dx, byte ptr [esi]
0x48075E: push     ebp
0x48075F: push     ebx
0x480760: xor      dword ptr [esp + eax + 0x10000], ebx
0x480767: or       al, byte ptr [eax + ecx*2 + 0x65]
0x48076B: insb     byte ptr es:[edi], dx
0x48076C: insd     dword ptr es:[edi], dx
0x48076D: je       0x4807c5
0x480770: push     ebx
0x480771: xor      dword ptr [eax + 0x1000004], esp
0x480777: add      byte ptr [esp + eax*2], cl
0x48077A: dec      esi
0x48077B: arpl     word ptr gs:[ebx + 0x6c], bp
0x48077F: popal    
0x480780: arpl     word ptr [ebp + 0x55], sp
0x480783: push     ebx
0x480784: xor      dword ptr [esp + eax + 0x10000], esp
0x48078B: or       dword ptr [esp + eax*2 + 0x72], eax
0x48078F: jae      0x480805
0x480792: push     ebp
0x480793: push     ebx
0x480794: xor      dword ptr [eax + 0x1000004], ebp
0x48079A: add      byte ptr [ecx], cl
0x48079C: inc      esp
0x48079D: dec      esp
0x48079E: imul     esp, dword ptr [edi + 0x68], 0x31535574
0x4807A5: lodsb    al, byte ptr [esi]
0x4807A6: add      al, 0
0x4807A8: add      byte ptr [ecx], al
0x4807AA: add      byte ptr [esp + eax*2], cl
0x4807AD: inc      ecx
0x4807AE: jb       0x48081d
0x4807B0: jb       0x48081b
0x4807B2: outsb    dx, byte ptr [esi]
0x4807B3: push     edx
0x4807B5: push     ebp
0x4807B6: push     ebx
0x4807B7: xor      dword ptr [eax + 0x1000004], esi
0x4807BD: add      byte ptr [ecx], cl
0x4807BF: inc      esp
0x4807C0: push     edx
0x4807C1: imul     ebp, dword ptr [esi + 0x67], 0x31535552
0x4807C8: mov      ah, 4
0x4807CA: add      byte ptr [eax], al
0x4807CC: add      dword ptr [eax], eax
0x4807CE: or       al, 0x44
0x4807D0: inc      ecx
0x4807D1: jb       0x480840
0x4807D3: jb       0x48083e
0x4807D5: outsb    dx, byte ptr [esi]
0x4807D6: dec      esp
0x4807D8: push     ebp
0x4807D9: push     ebx
0x4807DA: xor      dword ptr [eax + 0x1000004], edi
0x4807E0: add      byte ptr [ecx], cl
0x4807E2: inc      esp
0x4807E3: push     edx
0x4807E4: imul     ebp, dword ptr [esi + 0x67], 0x3153554c
0x4807EB: mov      esp, 0x1000004
0x4807F0: add      byte ptr [ecx], cl
0x4807F2: inc      esp
0x4807F3: push     ebx
0x4807F4: push     ebx
0x4807F5: jb       0x48086e
0x4807F8: jb       0x48082e
0x4807FB: rol      byte ptr [eax + eax], 0
0x4807FF: add      dword ptr [eax], eax
0x480801: or       dword ptr [ebx + edx*2 + 0x53], eax
0x480805: jb       0x48087e
0x480808: jb       0x48083f
0x48080B: les      eax, ptr [eax + eax]
0x48080E: add      byte ptr [eax], al
0x480810: add      byte ptr [ecx], cl
0x480812: inc      esp
0x480813: inc      edi
0x480814: jne      0x48087f ; "DGDEditGrade"
0x480816: insb     byte ptr es:[edi], dx
0x480817: inc      esp
0x480819: insb     byte ptr es:[edi], dx
0x48081A: enter    4, 0
0x48081F: add      dword ptr [eax], eax
0x480821: pop      es
0x480822: inc      esp
0x480823: inc      edi
0x480824: inc      esp
0x480825: dec      eax
0x480826: outsd    dx, dword ptr [esi]
0x480827: insd     dword ptr es:[edi], dx
0x480828: int3     
0x48082A: add      al, 0
0x48082C: add      byte ptr [ecx], al
0x48082E: add      byte ptr [edi], al
0x480830: inc      esp
0x480831: inc      edi
0x480832: inc      esp
0x480833: dec      esp
0x480834: imul     esi, dword ptr [ebx + 0x74], 0x4d0
0x48083B: add      dword ptr [eax], eax
0x48083D: pop      es
0x48083E: inc      esp
0x48083F: inc      edi
0x480840: inc      esp
0x480841: inc      ebx
0x480842: push     0x4d47461
0x480847: add      byte ptr [eax], al
0x480849: add      dword ptr [eax], eax
0x48084B: or       dword ptr [edi + eax*2 + 0x44], eax
0x48084F: inc      ecx
0x480850: dec      ebp
0x480853: insd     dword ptr es:[edi], dx
0x480855: fadd     dword ptr [eax + eax]
0x480858: add      byte ptr [ecx], al
0x48085A: add      byte ptr [ecx], cl
0x48085C: inc      esp
0x48085D: inc      edi
0x48085E: inc      esp
0x48085F: inc      esp
0x480860: insb     byte ptr es:[edi], dx
0x480862: dec      ebp
0x480863: insd     dword ptr es:[edi], dx
0x480865: fadd     qword ptr [eax + eax]
0x480868: add      byte ptr [ecx], al
0x48086A: add      byte ptr [0x45444744], cl
0x480870: imul     esi, dword ptr fs:[esi + ecx*2 + 0x6f], 0x65636974
0x480879: loopne   0x48087f ; "DGDEditGrade"
0x48087B: add      byte ptr [eax], al
0x48087D: add      dword ptr [eax], eax
0x48087F: or       al, 0x44
0x480881: inc      edi
0x480882: inc      esp
0x480883: inc      ebp
0x480884: imul     esi, dword ptr fs:[edi + eax*2 + 0x72], 0xe4656461
0x48088D: add      al, 0
0x48088F: add      byte ptr [ecx], al
0x480891: add      byte ptr [edi], al
0x480893: inc      esp
0x480894: inc      edi
0x480895: inc      esp
0x480896: inc      ecx
0x480897: insb     byte ptr es:[edi], dx
0x480898: insb     byte ptr es:[edi], dx
0x480899: jns      0x480883
0x48089B: add      al, 0
0x48089D: add      byte ptr [ecx], al
0x48089F: add      byte ptr [esp + eax*2], cl
0x4808A2: inc      edi
0x4808A3: inc      esp
0x4808A4: inc      edx
0x4808A5: jb       0x48090c
0x4808A7: popal    
0x4808A8: imul     eax, dword ptr [ecx + 0x6c], 0x6c
0x4808AC: jns      0x48089a
0x4808AE: add      al, 0
0x4808B0: add      byte ptr [ecx], al
0x4808B2: add      byte ptr [esi], al
0x4808B4: inc      esp
0x4808B5: inc      edi
0x4808B6: inc      esp
0x4808B7: push     edi
0x4808B8: popal    
0x4808B9: jb       0x4808ab
0x4808BB: add      al, 0
0x4808BD: add      byte ptr [ecx], al
0x4808BF: add      byte ptr [esp + eax*2], cl
0x4808C2: inc      edi
0x4808C3: inc      esp
0x4808C4: inc      ebx
0x4808C5: popal    
0x4808C6: outsb    dx, byte ptr [esi]
0x4808C7: arpl     word ptr [ebp + 0x6c], sp
0x4808CA: push     edi
0x4808CB: popal    
0x4808CC: jb       0x4808c2
0x4808CE: add      al, 0
0x4808D0: add      byte ptr [ecx], al
0x4808D2: add      byte ptr [0x55444744], al
0x4808D8: jo       0x4808d2
0x4808DA: add      al, 0
0x4808DC: add      byte ptr [ecx], al
0x4808DE: add      byte ptr [edi], al
0x4808E0: inc      esp
0x4808E1: inc      edi
0x4808E2: inc      esp
0x4808E3: inc      esp
0x4808E4: outsd    dx, dword ptr [esi]
0x4808E5: ja       0x480955
0x4808E7: cld      
0x4808E8: add      al, 0
0x4808EA: add      byte ptr [ecx], al
0x4808EC: add      byte ptr [eax], cl
0x4808EE: inc      esp
0x4808EF: inc      edi
0x4808F0: inc      esp
0x4808F1: inc      ebx
0x4808F2: insb     byte ptr es:[edi], dx
0x4808F3: outsd    dx, dword ptr [esi]
0x4808F4: jae      0x48095b
0x4808F6: add      byte ptr [0], al
0x4808FC: adc      byte ptr [edi + eax*2 + 0x75], al
0x480900: imul     ebp, dword ptr [esp + 0x45], 0x4e746964
0x480908: outsd    dx, dword ptr [esi]
0x480909: je       0x480974
0x48090B: arpl     word ptr [ebp + 4], sp
0x48090E: add      eax, 0x10000
0x480913: or       byte ptr [edi + eax*2 + 0x45], al
0x480917: inc      ebx
0x480918: insb     byte ptr es:[edi], dx
0x480919: outsd    dx, dword ptr [esi]
0x48091A: jae      0x480981
0x48091C: or       byte ptr [0x10000], al
0x480922: add      eax, 0x4f454744
0x480927: imul     ecx, dword ptr [eax + 0x10000], 9
0x48092F: inc      esp
0x480930: push     ebx
0x480931: push     ebx
0x480932: jb       0x4809ab
0x480935: jb       0x48096d
0x480938: adc      byte ptr [0x10000], al
0x48093E: or       dword ptr [ebx + edx*2 + 0x53], eax
0x480942: jb       0x4809bb
0x480945: jb       0x48097e
0x480948: adc      al, 5
0x48094A: add      byte ptr [eax], al
0x48094C: add      dword ptr [eax], eax
0x48094E: adc      dword ptr [esi + ecx*2 + 0x65], eax
0x480952: ja       0x480995
0x480954: arpl     word ptr [ebx + 0x6f], sp
0x480957: jne      0x4809c7
0x480959: je       0x48099e
0x48095B: popal    
0x48095C: outsb    dx, byte ptr [esi]
0x48095D: arpl     word ptr [ebp + 0x6c], sp
0x480960: sbb      byte ptr [0], al
0x480966: push     cs
0x480967: inc      esp
0x480968: inc      ecx
0x480969: push     0x75
0x48096C: jae      0x4809e2
0x48096E: inc      ecx
0x48096F: bound    ebp, qword ptr [ecx + 0x6c]
0x480972: imul     esi, dword ptr [ecx + edi*2 + 0x1c], 0x1000005
0x48097A: add      byte ptr [edi], al
0x48097C: inc      esp
0x48097D: push     eax
0x48097E: insb     byte ptr es:[edi], dx
0x48097F: jne      0x4809f4
0x480981: inc      esp
0x480982: inc      ebx
0x480983: and      byte ptr [0x10000], al
0x480989: pop      es
0x48098A: inc      esp
0x48098B: push     eax
0x48098C: insb     byte ptr es:[edi], dx
0x48098D: jne      0x480a02
0x48098F: dec      ebp
0x480990: inc      ebx
0x480991: and      al, 5
0x480993: add      byte ptr [eax], al
0x480995: add      dword ptr [eax], eax
0x480997: pop      es
0x480998: inc      esp
0x480999: push     eax
0x48099A: insb     byte ptr es:[edi], dx
0x48099B: jne      0x480a10
0x48099D: push     ebx
0x48099E: inc      ebx
0x48099F: sub      byte ptr [0x10000], al
0x4809A5: pop      es
0x4809A6: inc      esp
0x4809A7: push     eax
0x4809A8: insb     byte ptr es:[edi], dx
0x4809A9: jne      0x480a1e
0x4809AB: inc      ecx
0x4809AC: inc      ebx
0x4809AD: sub      al, 5
0x4809AF: add      byte ptr [eax], al
0x4809B1: add      dword ptr [eax], eax
0x4809B3: or       byte ptr [eax + edx*2 + 0x6c], al
0x4809B7: jne      0x480a2c
0x4809B9: dec      ebp
0x4809BA: inc      ecx
0x4809BB: inc      ebx
0x4809BC: xor      byte ptr [0x10000], al
0x4809C2: pop      es
0x4809C3: inc      esp
0x4809C4: push     eax
0x4809C5: insb     byte ptr es:[edi], dx
0x4809C6: jne      0x480a3b
0x4809C8: dec      eax
0x4809C9: push     eax
0x4809CA: xor      al, 5
0x4809CC: add      byte ptr [eax], al
0x4809CE: add      dword ptr [eax], eax
0x4809D0: pop      es
0x4809D1: inc      esp
0x4809D2: push     eax
0x4809D3: insb     byte ptr es:[edi], dx
0x4809D4: jne      0x480a49
0x4809D6: dec      ebp
0x4809D7: push     eax
0x4809D8: cmp      byte ptr [0x10000], al
0x4809DE: or       byte ptr [eax + edx*2 + 0x6c], al
0x4809E2: jne      0x480a57
0x4809E4: dec      eax
0x4809E5: imul     esi, dword ptr [esp + edi + 5], 0x10000
0x4809ED: or       al, byte ptr [eax + edx*2 + 0x6c]
0x4809F1: jne      0x480a66
0x4809F3: push     ebx
0x4809F4: jo       0x480a5b
0x4809F6: inc      eax
0x4809F9: add      eax, 0x10000
0x4809FE: or       byte ptr [ebp + ecx*2 + 0x69], al
0x480A02: outsb    dx, byte ptr [esi]
0x480A03: jne      0x480a78 ; "DMinusSpeed"
0x480A05: inc      esp
0x480A06: inc      ebx
0x480A07: inc      esp
0x480A08: add      eax, 0x10000
0x480A0D: or       byte ptr [ebp + ecx*2 + 0x69], al
0x480A11: outsb    dx, byte ptr [esi]
0x480A12: jne      0x480a87
0x480A14: dec      ebp
0x480A15: inc      ebx
0x480A16: dec      eax
0x480A17: add      eax, 0x10000
0x480A1C: or       byte ptr [ebp + ecx*2 + 0x69], al
0x480A20: outsb    dx, byte ptr [esi]
0x480A21: jne      0x480a96
0x480A23: push     ebx
0x480A24: inc      ebx
0x480A25: dec      esp
0x480A26: add      eax, 0x10000
0x480A2B: or       byte ptr [ebp + ecx*2 + 0x69], al
0x480A2F: outsb    dx, byte ptr [esi]
0x480A30: jne      0x480aa5
0x480A32: inc      ecx
0x480A33: inc      ebx
0x480A34: push     eax
0x480A35: add      eax, 0x10000
0x480A3A: or       dword ptr [ebp + ecx*2 + 0x69], eax
0x480A3E: outsb    dx, byte ptr [esi]
0x480A3F: jne      0x480ab4
0x480A41: dec      ebp
0x480A42: inc      ecx
0x480A43: inc      ebx
0x480A44: push     esp
0x480A45: add      eax, 0x10000
0x480A4A: or       byte ptr [ebp + ecx*2 + 0x69], al
0x480A4E: outsb    dx, byte ptr [esi]
0x480A4F: jne      0x480ac4
0x480A51: dec      ebp
0x480A52: push     eax
0x480A53: pop      eax
0x480A54: add      eax, 0x10000
0x480A59: or       byte ptr [ebp + ecx*2 + 0x69], al
0x480A5D: outsb    dx, byte ptr [esi]
0x480A5E: jne      0x480ad3
0x480A60: dec      eax
0x480A61: push     eax
0x480A62: pop      esp
0x480A63: add      eax, 0x10000
0x480A68: or       dword ptr [ebp + ecx*2 + 0x69], eax
0x480A6C: outsb    dx, byte ptr [esi]
0x480A6D: jne      0x480ae2
0x480A6F: dec      eax
0x480A70: imul     esi, dword ptr [eax + 5], 0x10000
0x480A78: or       eax, dword ptr [ebp + ecx*2 + 0x69]
0x480A7C: outsb    dx, byte ptr [esi]
0x480A7D: jne      0x480af2
0x480A7F: push     ebx
0x480A80: jo       0x480ae7
0x480A82: add      eax, 0x10000
0x480A8A: adc      byte ptr [ecx + eax*2 + 0x64], al
0x480A8E: push     0x75
0x480A90: jae      0x480b06
0x480A92: inc      ecx
0x480A93: bound    ebp, qword ptr [ecx + 0x6c]
0x480A96: inc      ebx
0x480A97: insb     byte ptr es:[edi], dx
0x480A98: outsd    dx, dword ptr [esi]
0x480A99: jae      0x480b00
0x480A9B: push     0x1000005
0x480AA0: add      byte ptr [0x6a644144], cl
0x480AA6: jne      0x480b1b
0x480AA8: je       0x480aeb
0x480AAA: bound    ebp, qword ptr [ecx + 0x6c]
0x480AAD: dec      edi
0x480AAE: imul     ebp, dword ptr [ebp + eax], 0
0x480AB3: add      dword ptr [eax], eax
0x480AB5: or       al, 0x44
0x480AB7: inc      edx
0x480AB8: outsd    dx, dword ptr [esi]
0x480AB9: je       0x480b0b
0x480ABB: insb     byte ptr es:[edi], dx
0x480ABC: jne      0x480b31
0x480ABE: inc      ecx
0x480ABF: bound    ebp, qword ptr [ecx + 0x6c]
0x480AC2: jo       0x480ac9
0x480AC4: add      byte ptr [eax], al
0x480AC6: add      dword ptr [eax], eax
0x480AC8: add      eax, 0x46734b44
0x480ACD: xor      eax, 0x574
0x480AD2: add      dword ptr [eax], eax
0x480AD4: add      eax, 0x46734b44
0x480AD9: js       0x480ae1
0x480ADC: add      byte ptr [eax], al
0x480ADE: add      dword ptr [eax], eax
0x480AE0: add      eax, 0x46734b44
0x480AE5: aaa      
0x480AE6: jl       0x480aed
0x480AE8: add      byte ptr [eax], al
0x480AEA: add      dword ptr [eax], eax
0x480AEC: add      eax, 0x46734b44
0x480AF1: cmp      byte ptr [eax + 0x1000005], al
0x480AF7: add      byte ptr [ebx], cl
0x480AF9: inc      esp
0x480AFA: inc      ebp
0x480AFB: outsb    dx, byte ptr [esi]
0x480AFC: push     ebx
0x480AFE: jb       0x480b77
0x480B01: jb       0x480b35
0x480B04: test     byte ptr [0x10000], al
0x480B0A: or       dword ptr [ebx + edx*2 + 0x53], eax
0x480B0E: jb       0x480b87
0x480B11: jb       0x480b4c
0x480B14: mov      byte ptr [0x10000], al
0x480B1A: or       dword ptr [ebx + edx*2 + 0x53], eax
0x480B1E: jb       0x480b97
0x480B21: jb       0x480b5b
0x480B24: mov      word ptr [0x10000], es
0x480B2A: or       dword ptr [ebx + edx*2 + 0x53], eax
0x480B2E: jb       0x480ba7
0x480B31: jb       0x480b6d ; "DSServer13"
0x480B34: nop      
0x480B35: add      eax, 0x10000
0x480B3A: or       al, byte ptr [ebx + edx*2 + 0x53]
0x480B3E: jb       0x480bb7
0x480B41: jb       0x480b75
0x480B44: xor      byte ptr [ebp + eax + 0x10000], dl
0x480B4B: or       al, byte ptr [ebx + edx*2 + 0x53]
0x480B4F: jb       0x480bc8
0x480B52: jb       0x480b86
0x480B55: xor      dword ptr [eax + 0x1000005], ebx
0x480B5B: add      byte ptr [edx], cl
0x480B5D: inc      esp
0x480B5E: push     ebx
0x480B5F: push     ebx
0x480B60: jb       0x480bd9
0x480B63: jb       0x480b97
0x480B66: xor      bl, byte ptr [ebp + eax + 0x10000]
0x480B6D: or       al, byte ptr [ebx + edx*2 + 0x53]
0x480B71: jb       0x480bea
0x480B74: jb       0x480ba8
0x480B77: xor      esp, dword ptr [eax + 0x1000005]
0x480B7D: add      byte ptr [edx], cl
0x480B7F: inc      esp
0x480B80: push     ebx
0x480B81: push     ebx
0x480B82: jb       0x480bfb
0x480B85: jb       0x480bb9
0x480B88: xor      al, 0xa4
0x480B8A: add      eax, 0x10000
0x480B8F: or       al, byte ptr [ebx + edx*2 + 0x53]
0x480B93: jb       0x480c0c
0x480B96: jb       0x480bca
0x480B99: xor      eax, 0x5a8
0x480B9E: add      dword ptr [eax], eax
0x480BA0: or       al, byte ptr [ebx + edx*2 + 0x53]
0x480BA4: jb       0x480c1d
0x480BA7: jb       0x480bdb
0x480BAA: lodsb    al, byte ptr ss:[esi]
0x480BAC: add      eax, 0x10000
0x480BB1: or       al, byte ptr [ebx + edx*2 + 0x53]
0x480BB5: jb       0x480c2e
0x480BB8: jb       0x480bec
0x480BBB: aaa      
0x480BBC: mov      al, 5
0x480BBE: add      byte ptr [eax], al
0x480BC0: add      dword ptr [eax], eax
0x480BC2: or       al, byte ptr [ebx + edx*2 + 0x53]
0x480BC6: jb       0x480c3f
0x480BC9: jb       0x480bfd
0x480BCC: cmp      byte ptr [ebp + eax + 0x10000], dh
0x480BD3: or       al, byte ptr [ebx + edx*2 + 0x53]
0x480BD7: jb       0x480c50
0x480BDA: jb       0x480c0e
0x480BDD: cmp      dword ptr [eax + 0x1000005], edi
0x480BE3: add      byte ptr [edx], cl
0x480BE5: inc      esp
0x480BE6: push     ebx
0x480BE7: push     ebx
0x480BE8: jb       0x480c61
0x480BEB: jb       0x480c20
0x480BEE: xor      byte ptr [ebp + eax + 0x10000], bh
0x480BF5: or       al, byte ptr [ebx + edx*2 + 0x53]
0x480BF9: jb       0x480c72
0x480BFC: jb       0x480c31
0x480BFF: xor      eax, eax
0x480C01: add      eax, 0x10000
0x480C06: or       al, byte ptr [ebx + edx*2 + 0x53]
0x480C0A: jb       0x480c83
0x480C0D: jb       0x480c42
0x480C10: xor      al, ah
0x480C12: add      eax, 0x10000
0x480C17: or       al, byte ptr [ebx + edx*2 + 0x53]
0x480C1B: jb       0x480c94
0x480C1E: jb       0x480c53
0x480C21: xor      ecx, eax
0x480C23: add      eax, 0x10000
0x480C28: or       al, byte ptr [ebx + edx*2 + 0x53]
0x480C2C: jb       0x480ca5
0x480C2F: jb       0x480c64
0x480C32: xor      al, 0xcc
0x480C34: add      eax, 0x10000
0x480C39: or       al, byte ptr [ebx + edx*2 + 0x53]
0x480C3D: jb       0x480cb6
0x480C40: jb       0x480c75
0x480C43: xor      eax, 0x5d0
0x480C48: add      dword ptr [eax], eax
0x480C4A: or       al, byte ptr [ebx + edx*2 + 0x53]
0x480C4E: jb       0x480cc7
0x480C51: jb       0x480c86
0x480C54: aam      5
0x480C57: add      byte ptr [eax], al
0x480C59: add      dword ptr [eax], eax
0x480C5B: or       al, byte ptr [ebx + edx*2 + 0x53]
0x480C5F: jb       0x480cd8
0x480C62: jb       0x480c97 ; "DBottomDirectPaint"
0x480C65: aaa      
0x480C66: fadd     dword ptr [0x10000]
0x480C6C: or       al, byte ptr [ebx + edx*2 + 0x53]
0x480C70: jb       0x480ce9
0x480C73: jb       0x480ca8
0x480C76: cmp      byte ptr [edx + 0x74001800], cl
0x480C7C: mov      edx, 0x44110048
0x480C81: inc      edx
0x480C82: outsd    dx, dword ptr [esi]
0x480C83: je       0x480cf9
0x480C85: outsd    dx, dword ptr [esi]
0x480C86: insd     dword ptr es:[edi], dx
0x480C87: dec      ecx
0x480C88: outsb    dx, byte ptr [esi]
0x480C89: push     edx
0x480C8A: popal    
0x480C8C: insb     byte ptr es:[edi], dx
0x480C8D: inc      ecx
0x480C8E: jb       0x480cf5
0x480C90: popal    
0x480C91: sbb      dword ptr [eax], eax
0x480C93: mov      al, byte ptr [0x120048b2]
0x480C98: inc      esp
0x480C99: inc      edx
0x480C9A: outsd    dx, dword ptr [esi]
0x480C9B: je       0x480d11
0x480C9D: outsd    dx, dword ptr [esi]
0x480C9E: insd     dword ptr es:[edi], dx
0x480C9F: inc      esp
0x480CA0: imul     esi, dword ptr [edx + 0x65], 0x61507463
0x480CA7: imul     ebp, dword ptr [esi + 0x74], 0xbacc001a
0x480CAE: dec      eax
0x480CAF: add      byte ptr [ebx], dl
0x480CB1: inc      esp
0x480CB2: dec      ebp
0x480CB3: jns      0x480d08
0x480CB5: je       0x480d18
0x480CB7: je       0x480d1e
0x480CB9: inc      esp
0x480CBA: imul     esi, dword ptr [edx + 0x65], 0x61507463
0x480CC1: imul     ebp, dword ptr [esi + 0x74], 0xbe700013
0x480CC8: dec      eax
0x480CC9: add      byte ptr [esp + eax*2], cl
0x480CCC: dec      edi
0x480CCD: jo       0x480d43
0x480CCF: imul     ebp, dword ptr [edi + 0x6e], 0x63696c43
0x480CD6: imul     ebx, dword ptr [edx], 0
0x480CD9: and      al, 0xdc
0x480CDB: dec      eax
0x480CDC: add      byte ptr [ebx], dl
0x480CDE: inc      esp
0x480CDF: dec      ecx
0x480CE0: je       0x480d47
0x480CE2: insd     dword ptr es:[edi], dx
0x480CE3: inc      edx
0x480CE4: popal    
0x480CE5: inc      esp
0x480CE7: imul     esi, dword ptr [edx + 0x65], 0x61507463
0x480CEE: imul     ebp, dword ptr [esi + 0x74], 0xdf10001d
0x480CF5: dec      eax
0x480CF6: add      byte ptr [esi], dl
0x480CF8: inc      esp
0x480CF9: push     edx
0x480CFA: jo       0x480d5e
0x480CFD: imul     esi, dword ptr [edx + 0x49], 0x446d6574
0x480D04: imul     esi, dword ptr [edx + 0x65], 0x61507463
0x480D0B: imul     ebp, dword ptr [esi + 0x74], 0xdebc001c
0x480D12: dec      eax
0x480D13: add      byte ptr [0x70655244], dl
0x480D19: popal    
0x480D1A: imul     esi, dword ptr [edx + 0x49], 0x496d6574
0x480D21: outsb    dx, byte ptr [esi]
0x480D22: push     edx
0x480D23: popal    
0x480D25: insb     byte ptr es:[edi], dx
0x480D26: inc      ecx
0x480D27: jb       0x480d8e
0x480D29: popal    
0x480D2A: sbb      eax, dword ptr [eax]
0x480D2C: hlt      
0x480D2D: mov      cl, byte ptr [eax]
0x480D30: adc      al, 0x44
0x480D32: push     ebx
0x480D33: je       0x480d96
0x480D35: je       0x480d9c
0x480D37: push     edi
0x480D38: imul     ebp, dword ptr [esi + 0x44], 0x63657269
0x480D3F: je       0x480d91
0x480D41: popal    
0x480D42: imul     ebp, dword ptr [esi + 0x74], 0x198c0011
0x480D49: dec      eax
0x480D4A: add      byte ptr [edx], cl
0x480D4C: inc      esi
0x480D4D: outsd    dx, dword ptr [esi]
0x480D4E: jb       0x480dbd
0x480D50: inc      ebx
0x480D51: jb       0x480db8
0x480D53: popal    
0x480D54: je       0x480dbb
0x480D56: sbb      al, 0
0x480D58: movsb    byte ptr es:[edi], byte ptr [esi]
0x480D59: cmpsd    dword ptr [esi], dword ptr es:[edi]
0x480D5A: dec      eax
0x480D5B: add      byte ptr [0x65725044], dl
0x480D61: jbe      0x480db6
0x480D63: je       0x480dc6
0x480D65: je       0x480dcc
0x480D67: inc      esp
0x480D68: imul     esi, dword ptr [edx + 0x65], 0x61507463
0x480D6F: imul     ebp, dword ptr [esi + 0x74], 0x7a6c001b
0x480D76: dec      eax
0x480D77: add      byte ptr [esp + eax*2], dl
0x480D7A: dec      esp
0x480D7B: outsd    dx, dword ptr [esi]
0x480D7C: imul     ebp, dword ptr [bp + 0x4e], 0x69447765
0x480D84: jb       0x480deb
0x480D86: arpl     word ptr [eax + edx*2 + 0x61], si
0x480D8A: imul     ebp, dword ptr [esi + 0x74], 0x8644001c
0x480D91: dec      eax
0x480D92: add      byte ptr [0x53637344], dl
0x480D98: insb     byte ptr es:[edi], dx
0x480D9A: arpl     word ptr gs:[ecx + esi + 0x44], si
0x480D9F: imul     esi, dword ptr [edx + 0x65], 0x61507463
0x480DA6: imul     ebp, dword ptr [esi + 0x74], 0x878c001a
0x480DAD: dec      eax
0x480DAE: add      byte ptr [ebx], dl
0x480DB0: inc      esp
0x480DB1: arpl     word ptr [ebx + 0x43], sp
0x480DB4: insb     byte ptr es:[edi], dx
0x480DB5: outsd    dx, dword ptr [esi]
0x480DB6: jae      0x480e1d
0x480DB8: inc      esp
0x480DB9: imul     esi, dword ptr [edx + 0x65], 0x61507463
0x480DC0: imul     ebp, dword ptr [esi + 0x74], 0xe118001a
0x480DC7: dec      eax
0x480DC8: add      byte ptr [ebx], dl
0x480DCA: inc      esp
0x480DCB: dec      ecx
0x480DCC: je       0x480e33
0x480DCE: insd     dword ptr es:[edi], dx
0x480DCF: inc      edi
0x480DD0: jb       0x480e3b
0x480DD2: inc      edi
0x480DD4: jb       0x480e3f
0x480DD6: push     ebx
0x480DD8: insb     byte ptr es:[edi], dx
0x480DDA: arpl     word ptr gs:[ecx + ebx], si
0x480DDF: pop      eax
0x480DE0: in       al, 0x48
0x480DE2: add      byte ptr [edx], dl
0x480DE4: inc      esp
0x480DE5: dec      ecx
0x480DE6: je       0x480e4d
0x480DE8: insd     dword ptr es:[edi], dx
0x480DE9: inc      edi
0x480DEA: jb       0x480e55
0x480DEC: inc      edi
0x480DEE: jb       0x480e59
0x480DF0: push     eax
0x480DF2: popal    
0x480DF3: imul     ebp, dword ptr [esi + 0x74], 0xe33c0018
0x480DFA: dec      eax
0x480DFB: add      byte ptr [ecx], dl
0x480DFD: inc      esp
0x480DFE: dec      ecx
0x480DFF: je       0x480e66
0x480E01: insd     dword ptr es:[edi], dx
0x480E02: inc      edi
0x480E03: jb       0x480e6e
0x480E05: inc      esp
0x480E07: bound    ebp, qword ptr [ebx + eax*2 + 0x6c]
0x480E0B: imul     esp, dword ptr [ebx + 0x6b], 0x7724001b
0x480E12: dec      eax
0x480E13: add      byte ptr [esp + eax*2], dl
0x480E16: dec      ebp
0x480E17: jae      0x480e80
0x480E19: inc      esp
0x480E1A: insb     byte ptr es:[edi], dx
0x480E1B: dec      edi
0x480E1D: imul     eax, dword ptr [ecx + ebp*2 + 0x72], 0x65
0x480E22: arpl     word ptr [eax + edx*2 + 0x61], si
0x480E26: imul     ebp, dword ptr [esi + 0x74], 0x77d00019
0x480E2D: dec      eax
0x480E2E: add      byte ptr [edx], dl
0x480E30: inc      esp
0x480E31: dec      ebp
0x480E32: jae      0x480e9b
0x480E34: inc      esp
0x480E35: insb     byte ptr es:[edi], dx
0x480E36: inc      esp
0x480E38: imul     esi, dword ptr [edx + 0x65], 0x61507463
0x480E3F: imul     ebp, dword ptr [esi + 0x74], 0x75ec0015
0x480E46: dec      eax
0x480E47: add      byte ptr [esi], cl
0x480E49: inc      esp
0x480E4A: dec      ebp
0x480E4B: jae      0x480eb4
0x480E4D: inc      esp
0x480E4E: insb     byte ptr es:[edi], dx
0x480E4F: dec      ebx
0x480E51: jns      0x480e98
0x480E54: outsd    dx, dword ptr [esi]
0x480E55: ja       0x480ec5
0x480E57: sbb      eax, dword ptr [eax]
0x480E59: pushfd   
0x480E5A: fisttp   word ptr [eax]
0x480E5D: adc      al, 0x44
0x480E5F: inc      ebx
0x480E60: insb     byte ptr es:[edi], dx
0x480E61: outsd    dx, dword ptr [esi]
0x480E62: jae      0x480ec9
0x480E64: inc      edx
0x480E65: popal    
0x480E66: inc      esp
0x480E68: imul     esi, dword ptr [edx + 0x65], 0x61507463
0x480E6F: imul     ebp, dword ptr [esi + 0x74], 0x67300021
0x480E76: dec      eax
0x480E77: add      byte ptr [edx], bl
0x480E79: inc      esp
0x480E7A: inc      edx
0x480E7B: popal    
0x480E7C: arpl     word ptr [ebx + 0x67], bp
0x480E7F: jb       0x480ef0
0x480E81: jne      0x480ef1
0x480E83: inc      edx
0x480E85: popal    
0x480E86: arpl     word ptr [ebx + 0x67], bp
0x480E89: jb       0x480efa
0x480E8B: jne      0x480efb
0x480E8D: inc      ebx
0x480E8F: insb     byte ptr es:[edi], dx
0x480E90: imul     esp, dword ptr [ebx + 0x6b], 0xe054001d
0x480E97: dec      eax
0x480E98: add      byte ptr [esi], dl
0x480E9A: inc      esp
0x480E9B: dec      ecx
0x480E9C: je       0x480f03
0x480E9E: insd     dword ptr es:[edi], dx
0x480E9F: inc      edi
0x480EA0: jb       0x480f0b
0x480EA2: inc      edi
0x480EA4: jb       0x480f0f
0x480EA6: dec      ebp
0x480EA8: outsd    dx, dword ptr [esi]
0x480EA9: jne      0x480f1e
0x480EAB: dec      ebp
0x480EAD: outsd    dx, dword ptr [esi]
0x480EAE: jbe      0x480f15
0x480EB0: sbb      byte ptr [eax], al
0x480EB2: cmp      byte ptr [esi + 0x44110048], 0x42
0x480EB9: insb     byte ptr es:[edi], dx
0x480EBB: je       0x480eee
0x480EBD: inc      esp
0x480EBE: imul     esi, dword ptr [edx + 0x65], 0x61507463
0x480EC5: imul     ebp, dword ptr [esi + 0x74], 0x1c5c0012
0x480ECC: dec      eax
0x480ECD: add      byte ptr [ebx], cl
0x480ECF: inc      esi
0x480ED0: outsd    dx, dword ptr [esi]
0x480ED1: jb       0x480f40
0x480ED3: inc      esp
0x480ED4: jae      0x480f4b
0x480ED7: jb       0x480f48
0x480ED9: jns      0x480ef0
0x480EDB: add      byte ptr [eax + 0xe0048c1], cl
0x480EE1: inc      esp
0x480EE2: inc      edx
0x480EE3: insb     byte ptr es:[edi], dx
0x480EE5: je       0x480f18
0x480EE7: inc      esp
0x480EE8: bound    ebp, qword ptr [ebx + eax*2 + 0x6c]
0x480EEC: imul     esp, dword ptr [ebx + 0x6b], 0x7b440017
0x480EF3: dec      eax
0x480EF4: add      byte ptr [eax], dl
0x480EF6: inc      esp
0x480EF7: dec      esp
0x480EF8: outsd    dx, dword ptr [esi]
0x480EF9: imul     ebp, dword ptr [bp + 0x43], 0x65736f6c
0x480F01: inc      ebx
0x480F02: insb     byte ptr es:[edi], dx
0x480F03: imul     esp, dword ptr [ebx + 0x6b], 0x7b200014
0x480F0A: dec      eax
0x480F0B: add      byte ptr [0x676f4c44], cl
0x480F11: imul     ebp, dword ptr [esi + 0x4f], 0x696c436b
0x480F18: arpl     word ptr [ebx + 0x15], bp
0x480F1B: add      ah, bh
0x480F1D: jp       0x480f67
0x480F1F: add      byte ptr [esi], cl
0x480F21: inc      esp
0x480F22: dec      esp
0x480F23: outsd    dx, dword ptr [esi]
0x480F24: imul     ebp, dword ptr [bp + 0x4e], 0x6c437765
0x480F2C: imul     esp, dword ptr [ebx + 0x6b], 0x7b680017
0x480F33: dec      eax
0x480F34: add      byte ptr [eax], dl
0x480F36: inc      esp
0x480F37: dec      esp
0x480F38: outsd    dx, dword ptr [esi]
0x480F39: imul     ebp, dword ptr [bp + 0x43], 0x77506768
0x480F41: inc      ebx
0x480F42: insb     byte ptr es:[edi], dx
0x480F43: imul     esp, dword ptr [ebx + 0x6b], 0x84500019
0x480F4A: dec      eax
0x480F4B: add      byte ptr [edx], dl
0x480F4D: inc      esp
0x480F4E: dec      esi
0x480F4F: ja       0x480f93
0x480F52: arpl     word ptr [ebx + 0x6f], sp
0x480F55: jne      0x480fc5
0x480F57: je       0x480fa8
0x480F59: imul     eax, dword ptr [ebx + 0x6c], 0x69
0x480F5D: arpl     word ptr [ebx + 0x1c], bp
0x480F60: add      byte ptr [esp + eax*4 + 0x48], dh
0x480F64: add      byte ptr [0x77654e44], dl
0x480F6A: inc      ecx
0x480F6B: arpl     word ptr [ebx + 0x6f], sp
0x480F6E: jne      0x480fde
0x480F70: je       0x480fb5
0x480F72: insb     byte ptr es:[edi], dx
0x480F73: outsd    dx, dword ptr [esi]
0x480F74: jae      0x480fdb
0x480F76: inc      ebx
0x480F77: insb     byte ptr es:[edi], dx
0x480F78: imul     esp, dword ptr [ebx + 0x6b], 0x89c00014
0x480F7F: dec      eax
0x480F80: add      byte ptr [0x43636344], cl
0x480F86: insb     byte ptr es:[edi], dx
0x480F87: outsd    dx, dword ptr [esi]
0x480F88: jae      0x480fef
0x480F8A: inc      ebx
0x480F8B: insb     byte ptr es:[edi], dx
0x480F8C: imul     esp, dword ptr [ebx + 0x6b], 0x85f80014
0x480F93: dec      eax
0x480F94: add      byte ptr [0x67684344], cl
0x480F9A: jo       0x481013
0x480F9C: dec      edi
0x480F9D: imul     eax, dword ptr [ebx + 0x6c], 0x69
0x480FA1: arpl     word ptr [ebx + 0x16], bp
0x480FA4: add      al, al
0x480FA6: xchg     byte ptr [eax], cl
0x480FA9: cmove    esi, dword ptr [ebx + 0x63]
0x480FAD: push     ebx
0x480FAE: insb     byte ptr es:[edi], dx
0x480FB0: arpl     word ptr gs:[ecx + esi + 0x43], si
0x480FB5: insb     byte ptr es:[edi], dx
0x480FB6: imul     esp, dword ptr [ebx + 0x6b], 0xa77c0017
0x480FBD: dec      eax
0x480FBE: add      byte ptr [eax], dl
0x480FC0: inc      esp
0x480FC1: inc      ebx
0x480FC2: insb     byte ptr es:[edi], dx
0x480FC3: outsd    dx, dword ptr [esi]
0x480FC4: jae      0x48102b
0x480FC6: push     ebx
0x480FC7: je       0x48102a
0x480FC9: je       0x481030
0x480FCB: inc      ebx
0x480FCC: insb     byte ptr es:[edi], dx
0x480FCD: imul     esp, dword ptr [ebx + 0x6b], 0xa93c0016
0x480FD4: dec      eax
0x480FD5: add      byte ptr [edi], cl
0x480FD7: inc      esp
0x480FD8: push     eax
0x480FD9: jb       0x481040
0x480FDB: jbe      0x481030
0x480FDD: je       0x481040
0x480FDF: je       0x481046
0x480FE1: inc      ebx
0x480FE2: insb     byte ptr es:[edi], dx
0x480FE3: imul     esp, dword ptr [ebx + 0x6b], 0xa97c0016
0x480FEA: dec      eax
0x480FEB: add      byte ptr [edi], cl
0x480FED: inc      esp
0x480FEE: dec      esi
0x480FEF: js       0x481066
0x480FF2: push     ebx
0x480FF3: je       0x481056
0x480FF5: je       0x48105c
0x480FF7: inc      ebx
0x480FF8: insb     byte ptr es:[edi], dx
0x480FF9: imul     esp, dword ptr [ebx + 0x6b], 0xa9bc0015
0x481000: dec      eax
0x481001: add      byte ptr [esi], cl
0x481003: inc      esp
0x481004: push     ebx
0x481005: push     edi
0x481006: push     edi
0x481007: popal    
0x481009: jo       0x48107a
0x48100B: outsb    dx, byte ptr [esi]
0x48100C: inc      ebx
0x48100D: insb     byte ptr es:[edi], dx
0x48100E: imul     esp, dword ptr [ebx + 0x6b], 0x75400015
0x481015: dec      eax
0x481016: add      byte ptr [esi], cl
0x481018: inc      esp
0x481019: dec      ebp
0x48101A: jae      0x481083
0x48101C: inc      esp
0x48101D: insb     byte ptr es:[edi], dx
0x48101E: dec      edi
0x481020: imul     eax, dword ptr [ebx + 0x6c], 0x69
0x481024: arpl     word ptr [ebx + 0x15], bp
0x481027: add      byte ptr [eax], ch
0x48102A: dec      eax
0x48102B: add      byte ptr [esi], cl
0x48102D: inc      esp
0x48102E: inc      ebx
0x48102F: insb     byte ptr es:[edi], dx
0x481030: outsd    dx, dword ptr [esi]
0x481031: jae      0x481098
0x481033: inc      edx
0x481034: popal    
0x481035: inc      ebx
0x481037: insb     byte ptr es:[edi], dx
0x481038: imul     esp, dword ptr [ebx + 0x6b], 0xc02c0012
0x48103F: dec      eax
0x481040: add      byte ptr [ebx], cl
0x481042: inc      esp
0x481043: inc      edx
0x481044: insb     byte ptr es:[edi], dx
0x481046: je       0x481079
0x481048: inc      ebx
0x481049: insb     byte ptr es:[edi], dx
0x48104A: imul     esp, dword ptr [ebx + 0x6b], 0xbdec0014
0x481051: dec      eax
0x481052: add      byte ptr [0x53794d44], cl
0x481058: je       0x4810bb
0x48105A: je       0x4810c1
0x48105C: inc      ebx
0x48105D: insb     byte ptr es:[edi], dx
0x48105E: imul     esp, dword ptr [ebx + 0x6b], 0xa6a40015
0x481065: dec      eax
0x481066: add      byte ptr [esi], cl
0x481068: inc      esp
0x481069: push     ebx
0x48106A: je       0x4810cd
0x48106C: je       0x4810d3
0x48106E: push     edi
0x48106F: imul     ebp, dword ptr [esi + 0x43], 0x6b63696c
0x481076: sbb      dword ptr [eax], eax
0x481078: sub      al, 0xae
0x48107A: dec      eax
0x48107B: add      byte ptr [edx], dl
0x48107D: inc      esp
0x48107E: push     ebx
0x48107F: push     edi
0x481080: push     edi
0x481081: popal    
0x481083: jo       0x4810f4
0x481085: outsb    dx, byte ptr [esi]
0x481086: dec      ebp
0x481087: outsd    dx, dword ptr [esi]
0x481088: jne      0x4810fd
0x48108A: dec      ebp
0x48108C: outsd    dx, dword ptr [esi]
0x48108D: jbe      0x4810f4
0x48108F: push     ss
0x481090: add      ah, dl
0x481092: mov      edi, 0x440f0048
0x481097: inc      edx
0x481098: insb     byte ptr es:[edi], dx
0x48109A: je       0x4810cd
0x48109C: dec      ebp
0x48109D: outsd    dx, dword ptr [esi]
0x48109E: jne      0x481113
0x4810A0: dec      ebp
0x4810A2: outsd    dx, dword ptr [esi]
0x4810A3: jbe      0x48110a
0x4810A5: push     ds
0x4810A6: add      al, al
0x4810A8: ljmp     0x654d:0x44170048
0x4810AF: jb       0x481114
0x4810B1: push     0x44746e61
0x4810B6: insb     byte ptr es:[edi], dx
0x4810B7: inc      esp
0x4810B9: imul     esi, dword ptr [edx + 0x65], 0x61507463
0x4810C0: imul     ebp, dword ptr [esi + 0x74], 0xef90001d
0x4810C7: dec      eax
0x4810C8: add      byte ptr [esi], dl
0x4810CA: inc      esp
0x4810CB: dec      ebp
0x4810CC: jb       0x481132
0x4810CF: push     0x44746e61
0x4810D4: insb     byte ptr es:[edi], dx
0x4810D5: inc      ebx
0x4810D7: insb     byte ptr es:[edi], dx
0x4810D8: outsd    dx, dword ptr [esi]
0x4810D9: jae      0x481140
0x4810DB: inc      ebx
0x4810DC: insb     byte ptr es:[edi], dx
0x4810DD: imul     esp, dword ptr [ebx + 0x6b], 0xfeec0018
0x4810E4: dec      eax
0x4810E5: add      byte ptr [ecx], dl
0x4810E7: inc      esp
0x4810E8: dec      ebp
0x4810E9: jb       0x48114f
0x4810EC: push     0x44746e61
0x4810F1: insb     byte ptr es:[edi], dx
0x4810F2: inc      ebx
0x4810F4: insb     byte ptr es:[edi], dx
0x4810F5: imul     esp, dword ptr [ebx + 0x6b], 0x10001c
0x4810FC: dec      ecx
0x4810FD: add      byte ptr [0x72654d44], dl
0x481103: arpl     word ptr [eax + 0x61], bp
0x481106: outsb    dx, byte ptr [esi]
0x481107: je       0x48114d
0x481109: insb     byte ptr es:[edi], dx
0x48110A: dec      ebp
0x48110C: outsd    dx, dword ptr [esi]
0x48110D: jne      0x481182
0x48110F: inc      esp
0x481111: outsd    dx, dword ptr [esi]
0x481112: ja       0x481182
0x481114: sbb      al, byte ptr [eax]
0x481116: sbb      al, 1
0x481118: dec      ecx
0x481119: add      byte ptr [ebx], dl
0x48111B: inc      esp
0x48111C: dec      ebp
0x48111D: jb       0x481183
0x481120: push     0x44746e61
0x481125: insb     byte ptr es:[edi], dx
0x481126: dec      ebp
0x481128: outsd    dx, dword ptr [esi]
0x481129: jne      0x48119e
0x48112B: push     ebp
0x48112D: jo       0x481145
0x48112F: add      ah, al
0x481131: dec      byte ptr [eax]
0x481134: cmove    ecx, dword ptr [ebp + 0x65]
0x481138: outsb    dx, byte ptr [esi]
0x481139: jne      0x48117e
0x48113B: insb     byte ptr es:[edi], dx
0x48113C: outsd    dx, dword ptr [esi]
0x48113D: jae      0x4811a4
0x48113F: inc      ebx
0x481140: insb     byte ptr es:[edi], dx
0x481141: imul     esp, dword ptr [ebx + 0x6b], 0xf020001a
0x481148: dec      eax
0x481149: add      byte ptr [ebx], dl
0x48114B: inc      esp
0x48114C: dec      ebp
0x48114D: outsb    dx, byte ptr gs:[esi]
0x48114F: jne      0x481195
0x481151: insb     byte ptr es:[edi], dx
0x481152: inc      esp
0x481154: imul     esi, dword ptr [edx + 0x65], 0x61507463
0x48115B: imul     ebp, dword ptr [esi + 0x74], 0xf6580014
0x481162: dec      eax
0x481163: add      byte ptr [0x6e654d44], cl
0x481169: jne      0x4811af
0x48116B: insb     byte ptr es:[edi], dx
0x48116C: inc      ebx
0x48116E: insb     byte ptr es:[edi], dx
0x48116F: imul     esp, dword ptr [ebx + 0x6b], 0x140001a
0x481176: dec      ecx
0x481177: add      byte ptr [ebx], dl
0x481179: inc      esp
0x48117A: push     ebx
0x48117B: insb     byte ptr es:[edi], dx
0x48117D: insb     byte ptr es:[edi], dx
0x48117E: inc      esp
0x48117F: insb     byte ptr es:[edi], dx
0x481180: inc      esp
0x481182: imul     esi, dword ptr [edx + 0x65], 0x61507463
0x481189: imul     ebp, dword ptr [esi + 0x74], 0x2f40019
0x481190: dec      ecx
0x481191: add      byte ptr [edx], dl
0x481193: inc      esp
0x481194: push     ebx
0x481195: insb     byte ptr es:[edi], dx
0x481197: insb     byte ptr es:[edi], dx
0x481198: inc      esp
0x481199: insb     byte ptr es:[edi], dx
0x48119A: inc      ebx
0x48119C: insb     byte ptr es:[edi], dx
0x48119D: outsd    dx, dword ptr [esi]
0x48119E: jae      0x481205
0x4811A0: inc      ebx
0x4811A1: insb     byte ptr es:[edi], dx
0x4811A2: imul     esp, dword ptr [ebx + 0x6b], 0x3140018
0x4811A9: dec      ecx
0x4811AA: add      byte ptr [ecx], dl
0x4811AC: inc      esp
0x4811AD: push     ebx
0x4811AE: insb     byte ptr es:[edi], dx
0x4811B0: insb     byte ptr es:[edi], dx
0x4811B1: inc      esp
0x4811B2: insb     byte ptr es:[edi], dx
0x4811B3: push     ebx
0x4811B5: jo       0x481226
0x4811B7: je       0x4811fc
0x4811B9: insb     byte ptr es:[edi], dx
0x4811BA: imul     esp, dword ptr [ebx + 0x6b], 0x460001e
0x4811C1: dec      ecx
0x4811C2: add      byte ptr [edi], dl
0x4811C4: inc      esp
0x4811C5: push     ebx
0x4811C6: insb     byte ptr es:[edi], dx
0x4811C8: insb     byte ptr es:[edi], dx
0x4811C9: inc      esp
0x4811CA: insb     byte ptr es:[edi], dx
0x4811CB: push     ebx
0x4811CD: jo       0x48123e
0x4811CF: je       0x481215
0x4811D1: imul     esi, dword ptr [edx + 0x65], 0x61507463
0x4811D8: imul     ebp, dword ptr [esi + 0x74], 0x520001c
0x4811DF: dec      ecx
0x4811E0: add      byte ptr [0x6c655344], dl
0x4811E6: insb     byte ptr es:[edi], dx
0x4811E7: inc      esp
0x4811E8: insb     byte ptr es:[edi], dx
0x4811E9: push     ebx
0x4811EB: jo       0x48125c
0x4811ED: je       0x48123c ; "DMenuNextClick"
0x4811EF: outsd    dx, dword ptr [esi]
0x4811F0: jne      0x481265
0x4811F2: dec      ebp
0x4811F4: outsd    dx, dword ptr [esi]
0x4811F5: jbe      0x48125c
0x4811F7: push     ss
0x4811F8: add      byte ptr [eax + 5], dl
0x4811FB: dec      ecx
0x4811FC: add      byte ptr [edi], cl
0x4811FE: inc      esp
0x4811FF: push     ebx
0x481200: insb     byte ptr es:[edi], dx
0x481202: insb     byte ptr es:[edi], dx
0x481203: inc      esp
0x481204: insb     byte ptr es:[edi], dx
0x481205: dec      edi
0x481207: imul     eax, dword ptr [ebx + 0x6c], 0x69
0x48120B: arpl     word ptr [ebx + 0x14], bp
0x48120E: add      byte ptr [eax + 0xd0048fa], al
0x481214: inc      esp
0x481215: dec      ebp
0x481216: outsb    dx, byte ptr gs:[esi]
0x481218: jne      0x48125c
0x48121A: jne      0x481295
0x48121C: inc      ebx
0x48121D: insb     byte ptr es:[edi], dx
0x48121E: imul     esp, dword ptr [ebx + 0x6b], 0xfc140015
0x481225: dec      eax
0x481226: add      byte ptr [esi], cl
0x481228: inc      esp
0x481229: dec      ebp
0x48122A: outsb    dx, byte ptr gs:[esi]
0x48122C: jne      0x48127e
0x48122E: jb       0x481295
0x481230: jbe      0x481275
0x481232: insb     byte ptr es:[edi], dx
0x481233: imul     esp, dword ptr [ebx + 0x6b], 0xfcb40015
0x48123A: dec      eax
0x48123B: add      byte ptr [esi], cl
0x48123D: inc      esp
0x48123E: dec      ebp
0x48123F: outsb    dx, byte ptr gs:[esi]
0x481241: jne      0x481291
0x481243: js       0x4812ba
0x481246: inc      ebx
0x481247: insb     byte ptr es:[edi], dx
0x481248: imul     esp, dword ptr [ebx + 0x6b], 0xe5640011
0x48124F: dec      eax
0x481250: add      byte ptr [edx], cl
0x481252: inc      esp
0x481253: inc      edi
0x481254: outsd    dx, dword ptr [esi]
0x481255: insb     byte ptr es:[edi], dx
0x481256: inc      ebx
0x481258: insb     byte ptr es:[edi], dx
0x481259: imul     esp, dword ptr [ebx + 0x6b], 0xa0f0001a
0x481260: dec      eax
0x481261: add      byte ptr [ebx], dl
0x481263: inc      esp
0x481264: push     ebx
0x481265: push     edi
0x481266: dec      esp
0x481267: imul     esp, dword ptr [edi + 0x68], 0x72694474
0x48126E: arpl     word ptr gs:[eax + edx*2 + 0x61], si
0x481273: imul     ebp, dword ptr [esi + 0x74], 0x6858001b
0x48127A: dec      eax
0x48127B: add      byte ptr [esp + eax*2], dl
0x48127E: inc      edx
0x48127F: popal    
0x481280: arpl     word ptr [ebx + 0x67], bp
0x481283: jb       0x4812f4
0x481285: jne      0x4812f5
0x481287: dec      ebp
0x481289: outsd    dx, dword ptr [esi]
0x48128A: jne      0x4812ff
0x48128C: inc      esp
0x48128E: outsd    dx, dword ptr [esi]
0x48128F: ja       0x4812ff
0x481291: sbb      dword ptr [eax], eax
0x481293: jl       0x481244
0x481295: dec      eax
0x481296: add      byte ptr [edx], dl
0x481298: inc      esp
0x481299: push     ebx
0x48129A: je       0x4812fd
0x48129C: je       0x481303
0x48129E: push     edi
0x48129F: imul     ebp, dword ptr [esi + 0x4d], 0x6573756f
0x4812A6: dec      ebp
0x4812A7: outsd    dx, dword ptr [esi]
0x4812A8: jbe      0x48130f ; "DKsF1DirectPaint"
0x4812AA: sbb      al, byte ptr [eax]
0x4812AD: jnp      0x4812f7
0x4812AF: add      byte ptr [ebx], dl
0x4812B1: inc      esp
0x4812B2: dec      esp
0x4812B3: outsd    dx, dword ptr [esi]
0x4812B4: imul     ebp, dword ptr [bp + 0x4e], 0x6c437765
0x4812BC: imul     esp, dword ptr [ebx + 0x6b], 0x6e756f53
0x4812C3: sbb      dword ptr fs:[eax], eax
0x4812C6: pushfd   
0x4812C7: scasd    eax, dword ptr es:[edi]
0x4812C8: dec      eax
0x4812C9: add      byte ptr [edx], dl
0x4812CB: inc      esp
0x4812CC: push     ebx
0x4812CD: je       0x48131c
0x4812CF: popal    
0x4812D0: xor      dword ptr [si + 0x69], eax
0x4812D4: jb       0x48133b
0x4812D6: arpl     word ptr [eax + edx*2 + 0x61], si
0x4812DA: imul     ebp, dword ptr [esi + 0x74], 0xb0e00013
0x4812E1: dec      eax
0x4812E2: add      byte ptr [esp + eax*2], cl
0x4812E5: push     ebx
0x4812E6: je       0x481335
0x4812E8: popal    
0x4812E9: xor      dword ptr [bp + di + 0x6c], eax
0x4812ED: imul     esp, dword ptr [ebx + 0x6b], 0x9200019
0x4812F4: dec      ecx
0x4812F5: add      byte ptr [edx], dl
0x4812F7: inc      esp
0x4812F8: dec      ebx
0x4812F9: jae      0x481344
0x4812FB: arpl     word ptr [edi + 0x6e], bp
0x4812FE: inc      esp
0x4812FF: imul     esi, dword ptr [edx + 0x65], 0x61507463
0x481306: imul     ebp, dword ptr [esi + 0x74], 0x9a00017
0x48130D: dec      ecx
0x48130E: add      byte ptr [eax], dl
0x481310: inc      esp
0x481311: dec      ebx
0x481312: jae      0x48135a
0x481314: xor      dword ptr [ecx + ebp*2 + 0x72], eax
0x481318: arpl     word ptr gs:[eax + edx*2 + 0x61], si
0x48131D: imul     ebp, dword ptr [esi + 0x74], 0xb500011
0x481324: dec      ecx
0x481325: add      byte ptr [edx], cl
0x481327: inc      esp
0x481328: dec      ebx
0x481329: jae      0x48137a
0x48132B: imul     eax, dword ptr [ebx + 0x6c], 0x69
0x48132F: arpl     word ptr [ebx + 0x11], bp
0x481332: add      byte ptr [eax + 0xb], bh
0x481335: dec      ecx
0x481336: add      byte ptr [edx], cl
0x481338: inc      esp
0x481339: dec      ebx
0x48133A: jae      0x481382
0x48133C: xor      dword ptr [ebx + 0x6c], eax
0x48133F: imul     esp, dword ptr [ebx + 0x6b], 0x7d8001c
0x481346: dec      ecx
0x481347: add      byte ptr [0x79654b44], dl
0x48134D: push     ebx
0x48134E: insb     byte ptr es:[edi], dx
0x481350: inc      esp
0x481351: insb     byte ptr es:[edi], dx
0x481352: inc      esp
0x481354: imul     esi, dword ptr [edx + 0x65], 0x61507463
0x48135B: imul     ebp, dword ptr [esi + 0x74], 0xbb60001b
0x481362: dec      eax
0x481363: add      byte ptr [esp + eax*2], dl
0x481366: inc      edx
0x481367: outsd    dx, dword ptr [esi]
0x481368: je       0x4813b1
0x48136A: jb       0x4813db
0x48136C: jne      0x4813de
0x48136E: inc      esp
0x48136F: imul     esi, dword ptr [edx + 0x65], 0x61507463
0x481376: imul     ebp, dword ptr [esi + 0x74], 0x10080020
0x48137D: dec      ecx
0x48137E: add      byte ptr [ecx], bl
0x481380: inc      esp
0x481381: inc      edi
0x481382: jb       0x4813f4
0x481384: inc      ecx
0x481385: insb     byte ptr es:[edi], dx
0x481386: insb     byte ptr es:[edi], dx
0x481387: outsd    dx, dword ptr [esi]
0x481388: ja       0x4813d1
0x48138A: jb       0x4813fb
0x48138C: jne      0x4813fe
0x48138E: inc      esp
0x48138F: imul     esi, dword ptr [edx + 0x65], 0x61507463
0x481396: imul     ebp, dword ptr [esi + 0x74], 0xfe00018
0x48139D: dec      ecx
0x48139E: add      byte ptr [ecx], dl
0x4813A0: inc      esp
0x4813A1: inc      edi
0x4813A2: jb       0x481414
0x4813A4: inc      esp
0x4813A5: insb     byte ptr es:[edi], dx
0x4813A6: inc      ebx
0x4813A8: insb     byte ptr es:[edi], dx
0x4813A9: outsd    dx, dword ptr [esi]
0x4813AA: jae      0x481411
0x4813AC: inc      ebx
0x4813AD: insb     byte ptr es:[edi], dx
0x4813AE: imul     esp, dword ptr [ebx + 0x6b], 0xdb00015
0x4813B5: dec      ecx
0x4813B6: add      byte ptr [esi], cl
0x4813B8: inc      esp
0x4813B9: inc      edx
0x4813BA: outsd    dx, dword ptr [esi]
0x4813BB: je       0x481404
0x4813BD: jb       0x48142e
0x4813BF: jne      0x481431
0x4813C1: inc      ebx
0x4813C2: insb     byte ptr es:[edi], dx
0x4813C3: imul     esp, dword ptr [ebx + 0x6b], 0x1104001a
0x4813CA: dec      ecx
0x4813CB: add      byte ptr [ebx], dl
0x4813CD: inc      esp
0x4813CE: inc      edi
0x4813CF: jb       0x481441
0x4813D1: inc      ecx
0x4813D2: insb     byte ptr es:[edi], dx
0x4813D3: insb     byte ptr es:[edi], dx
0x4813D4: outsd    dx, dword ptr [esi]
0x4813D5: ja       0x48141e
0x4813D7: jb       0x481448
0x4813D9: jne      0x48144b
0x4813DB: inc      ebx
0x4813DC: insb     byte ptr es:[edi], dx
0x4813DD: imul     esp, dword ptr [ebx + 0x6b], 0x11600016
0x4813E4: dec      ecx
0x4813E5: add      byte ptr [edi], cl
0x4813E7: inc      esp
0x4813E8: inc      edi
0x4813E9: jb       0x48145b
0x4813EB: inc      ebx
0x4813EC: jb       0x481453
0x4813EE: popal    
0x4813EF: je       0x481456
0x4813F1: inc      ebx
0x4813F2: insb     byte ptr es:[edi], dx
0x4813F3: imul     esp, dword ptr [ebx + 0x6b], 0xdfc001b
0x4813FA: dec      ecx
0x4813FB: add      byte ptr [esp + eax*2], dl
0x4813FE: inc      edi
0x4813FF: jb       0x481470
0x481401: jne      0x481473
0x481403: inc      esp
0x481404: insb     byte ptr es:[edi], dx
0x481405: inc      esp
0x481407: imul     esi, dword ptr [edx + 0x65], 0x61507463
0x48140E: imul     ebp, dword ptr [esi + 0x74], 0x12640016
0x481415: dec      ecx
0x481416: add      byte ptr [edi], cl
0x481418: inc      esp
0x481419: inc      edi
0x48141A: jb       0x48148c
0x48141C: inc      ecx
0x48141D: dec      ebp
0x481420: insd     dword ptr es:[edi], dx
0x481422: inc      ebx
0x481423: insb     byte ptr es:[edi], dx
0x481424: imul     esp, dword ptr [ebx + 0x6b], 0x13680016
0x48142B: dec      ecx
0x48142C: add      byte ptr [edi], cl
0x48142E: inc      esp
0x48142F: inc      edi
0x481430: jb       0x4814a2
0x481432: inc      esp
0x481433: insb     byte ptr es:[edi], dx
0x481435: dec      ebp
0x481436: insd     dword ptr es:[edi], dx
0x481438: inc      ebx
0x481439: insb     byte ptr es:[edi], dx
0x48143A: imul     esp, dword ptr [ebx + 0x6b], 0x14740016
0x481441: dec      ecx
0x481442: add      byte ptr [edi], cl
0x481444: inc      esp
0x481445: inc      edx
0x481446: outsd    dx, dword ptr [esi]
0x481447: je       0x481495
0x481449: outsd    dx, dword ptr [esi]
0x48144A: outsd    dx, dword ptr [si]
0x48144C: jne      0x4814c2 ; "DBotTradeClick"
0x48144E: inc      ebx
0x48144F: insb     byte ptr es:[edi], dx
0x481450: imul     esp, dword ptr [ebx + 0x6b], 0x15200014
0x481457: dec      ecx
0x481458: add      byte ptr [0x746f4244], cl
0x48145E: inc      ebp
0x48145F: js       0x4814ca
0x481461: je       0x4814a6
0x481463: insb     byte ptr es:[edi], dx
0x481464: imul     esp, dword ptr [ebx + 0x6b], 0xb23c0015
0x48146B: dec      eax
0x48146C: add      byte ptr [esi], cl
0x48146E: inc      esp
0x48146F: push     ebx
0x481470: je       0x4814c2 ; "DBotTradeClick"
0x481472: popal    
0x481473: push     ebp
0x481476: jo       0x4814bb
0x481478: insb     byte ptr es:[edi], dx
0x481479: imul     esp, dword ptr [ebx + 0x6b], 0x69640017
0x481480: dec      eax
0x481481: add      byte ptr [eax], dl
0x481483: inc      esp
0x481484: inc      edx
0x481485: outsd    dx, dword ptr [esi]
0x481486: je       0x4814fc
0x481488: outsd    dx, dword ptr [esi]
0x481489: insd     dword ptr es:[edi], dx
0x48148A: dec      ebp
0x48148B: outsd    dx, dword ptr [esi]
0x48148C: jne      0x481501
0x48148E: inc      esp
0x481490: outsd    dx, dword ptr [esi]
0x481491: ja       0x481501
0x481493: adc      eax, dword ptr [eax]
0x481495: adc      byte ptr [edi], dl
0x481497: dec      ecx
0x481498: add      byte ptr [esp + eax*2], cl
0x48149B: inc      esp
0x48149C: popal    
0x48149E: insb     byte ptr es:[edi], dx
0x48149F: dec      edi
0x4814A0: imul     eax, dword ptr [ebx + 0x6c], 0x69
0x4814A4: arpl     word ptr [ebx + 0x16], bp
0x4814A7: add      byte ptr [eax + 0xf004917], bl
0x4814AD: inc      esp
0x4814AE: inc      esp
0x4814AF: popal    
0x4814B1: insb     byte ptr es:[edi], dx
0x4814B2: inc      ebx
0x4814B3: insb     byte ptr es:[edi], dx
0x4814B4: outsd    dx, dword ptr [esi]
0x4814B5: jae      0x48151c
0x4814B7: inc      ebx
0x4814B8: insb     byte ptr es:[edi], dx
0x4814B9: imul     esp, dword ptr [ebx + 0x6b], 0xd040015
0x4814C0: dec      ecx
0x4814C1: add      byte ptr [esi], cl
0x4814C3: inc      esp
0x4814C4: inc      edx
0x4814C5: outsd    dx, dword ptr [esi]
0x4814C6: je       0x48151c
0x4814C8: jb       0x48152b
0x4814CA: inc      ebx
0x4814CD: insb     byte ptr es:[edi], dx
0x4814CE: imul     esp, dword ptr [ebx + 0x6b], 0x17d00020
0x4814D5: dec      ecx
0x4814D6: add      byte ptr [ecx], bl
0x4814D8: inc      esp
0x4814D9: inc      esp
0x4814DA: popal    
0x4814DC: insb     byte ptr es:[edi], dx
0x4814DD: push     edx
0x4814DE: insd     dword ptr es:[edi], dx
0x4814E0: outsd    dx, dword ptr [esi]
0x4814E1: je       0x481548
0x4814E3: inc      esp
0x4814E4: insb     byte ptr es:[edi], dx
0x4814E5: inc      esp
0x4814E7: imul     esi, dword ptr [edx + 0x65], 0x61507463
0x4814EE: imul     ebp, dword ptr [esi + 0x74], 0x1954001a
0x4814F5: dec      ecx
0x4814F6: add      byte ptr [ebx], dl
0x4814F8: inc      esp
0x4814F9: inc      esp
0x4814FA: popal    
0x4814FC: insb     byte ptr es:[edi], dx
0x4814FD: inc      esp
0x4814FE: insb     byte ptr es:[edi], dx
0x4814FF: inc      esp
0x481501: imul     esi, dword ptr [edx + 0x65], 0x61507463
0x481508: imul     ebp, dword ptr [esi + 0x74], 0x1b3c0017
0x48150F: dec      ecx
0x481510: add      byte ptr [eax], dl
0x481512: inc      esp
0x481513: inc      esp
0x481514: inc      edi
0x481515: jb       0x481580
0x481517: inc      edi
0x481519: jb       0x481584
0x48151B: push     ebx
0x48151D: insb     byte ptr es:[edi], dx
0x48151F: arpl     word ptr gs:[esi + edx], si
0x481524: mov      eax, 0xf00491c
0x481529: inc      esp
0x48152A: inc      esp
0x48152B: inc      edi
0x48152C: jb       0x481597
0x48152E: inc      edi
0x481530: jb       0x48159b
0x481532: push     eax
0x481534: popal    
0x481535: imul     ebp, dword ptr [esi + 0x74], 0x1dbc001a
0x48153C: dec      ecx
0x48153D: add      byte ptr [ebx], dl
0x48153F: inc      esp
0x481540: inc      esp
0x481541: inc      edi
0x481542: jb       0x4815ad
0x481544: inc      edi
0x481546: jb       0x4815b1 ; "DSSrvCloseClick"
0x481548: dec      ebp
0x48154A: outsd    dx, dword ptr [esi]
0x48154B: jne      0x4815c0
0x48154D: dec      ebp
0x48154F: outsd    dx, dword ptr [esi]
0x481550: jbe      0x4815b7
0x481552: pop      ss
0x481553: add      byte ptr [esi + ebx], dl
0x481556: dec      ecx
0x481557: add      byte ptr [eax], dl
0x481559: inc      esp
0x48155A: inc      esp
0x48155B: push     edx
0x48155C: inc      edi
0x48155D: jb       0x4815c8
0x48155F: inc      edi
0x481561: jb       0x4815cc
0x481563: push     eax
0x481565: popal    
0x481566: imul     ebp, dword ptr [esi + 0x74], 0x1f18001b
0x48156D: dec      ecx
0x48156E: add      byte ptr [esp + eax*2], dl
0x481571: inc      esp
0x481572: push     edx
0x481573: inc      edi
0x481574: jb       0x4815df
0x481576: inc      edi
0x481578: jb       0x4815e3
0x48157A: dec      ebp
0x48157C: outsd    dx, dword ptr [esi]
0x48157D: jne      0x4815f2
0x48157F: dec      ebp
0x481581: outsd    dx, dword ptr [esi]
0x481582: jbe      0x4815e9
0x481584: adc      al, byte ptr [eax]
0x481586: mov      al, 0x1f
0x481588: dec      ecx
0x481589: add      byte ptr [ebx], cl
0x48158B: inc      esp
0x48158C: inc      esp
0x48158D: inc      edi
0x48158E: outsd    dx, dword ptr [esi]
0x48158F: insb     byte ptr es:[edi], dx
0x481590: inc      ebx
0x481592: insb     byte ptr es:[edi], dx
0x481593: imul     esp, dword ptr [ebx + 0x6b], 0x80280015
0x48159A: dec      eax
0x48159B: add      byte ptr [esi], cl
0x48159D: inc      esp
0x48159E: push     ebx
0x48159F: push     ebx
0x4815A0: jb       0x481619
0x4815A3: jb       0x4815d7
0x4815A6: inc      ebx
0x4815A7: insb     byte ptr es:[edi], dx
0x4815A8: imul     esp, dword ptr [ebx + 0x6b], 0x841c0016
0x4815AF: dec      eax
0x4815B0: add      byte ptr [edi], cl
0x4815B2: inc      esp
0x4815B3: push     ebx
0x4815B4: push     ebx
0x4815B5: jb       0x48162d
0x4815B7: inc      ebx
0x4815B8: insb     byte ptr es:[edi], dx
0x4815B9: outsd    dx, dword ptr [esi]
0x4815BA: jae      0x481621
0x4815BC: inc      ebx
0x4815BD: insb     byte ptr es:[edi], dx
0x4815BE: imul     esp, dword ptr [ebx + 0x6b], 0xc800017
0x4815C5: dec      ecx
0x4815C6: add      byte ptr [eax], dl
0x4815C8: inc      esp
0x4815C9: inc      edx
0x4815CA: outsd    dx, dword ptr [esi]
0x4815CB: je       0x48161a
0x4815CD: imul     ebp, dword ptr [esi + 0x69], 0x4370614d
0x4815D4: insb     byte ptr es:[edi], dx
0x4815D5: imul     esp, dword ptr [ebx + 0x6b], 0xf9ec0018
0x4815DC: dec      eax
0x4815DD: add      byte ptr [ecx], dl
0x4815DF: inc      esp
0x4815E0: dec      ebp
0x4815E1: outsb    dx, byte ptr gs:[esi]
0x4815E3: jne      0x481629
0x4815E5: insb     byte ptr es:[edi], dx
0x4815E6: dec      ebp
0x4815E8: outsd    dx, dword ptr [esi]
0x4815E9: jne      0x48165e
0x4815EB: dec      ebp
0x4815ED: outsd    dx, dword ptr [esi]
0x4815EE: jbe      0x481655
0x4815F0: sbb      eax, 0x49219c00
0x4815F5: add      byte ptr [esi], dl
0x4815F7: inc      esp
0x4815F8: push     ebp
0x4815F9: jae      0x481660
0x4815FB: jb       0x481650
0x4815FD: je       0x481660
0x4815FF: je       0x481666
0x481601: xor      dword ptr [ecx + ebp*2 + 0x72], eax
0x481605: arpl     word ptr gs:[eax + edx*2 + 0x61], si
0x48160A: imul     ebp, dword ptr [esi + 0x74], 0x29c8001b
0x481611: dec      ecx
0x481612: add      byte ptr [esp + eax*2], dl
0x481615: push     ebp
0x481616: jae      0x48167d
0x481618: jb       0x48166d
0x48161A: je       0x48167d
0x48161C: je       0x481683
0x48161E: xor      dword ptr [ebp + 0x6f], ecx
0x481621: jne      0x481696
0x481623: dec      ebp
0x481625: outsd    dx, dword ptr [esi]
0x481626: jbe      0x48168d
0x481628: sbb      al, byte ptr [eax]
0x48162A: call     0x13485f58
0x48162F: inc      esp
0x481630: push     edi
0x481631: popal    
0x481633: jo       0x4816a4
0x481635: outsb    dx, byte ptr [esi]
0x481636: push     ebp
0x481637: push     ebx
0x481638: xor      dword ptr [ebp + 0x6f], ecx
0x48163B: jne      0x4816b0
0x48163D: dec      ebp
0x48163F: outsd    dx, dword ptr [esi]
0x481640: jbe      0x4816a7
0x481642: adc      eax, 0x492ae800
0x481647: add      byte ptr [esi], cl
0x481649: inc      esp
0x48164A: inc      ebx
0x48164B: insb     byte ptr es:[edi], dx
0x48164C: outsd    dx, dword ptr [esi]
0x48164D: jae      0x4816b4
0x48164F: push     ebp
0x481650: push     ebx
0x481651: xor      dword ptr [ebx + 0x6c], eax
0x481654: imul     esp, dword ptr [ebx + 0x6b], 0x2b10001e
0x48165B: dec      ecx
0x48165C: add      byte ptr [edi], dl
0x48165E: inc      esp
0x48165F: dec      esi
0x481660: arpl     word ptr gs:[ebx + 0x6c], bp
0x481664: popal    
0x481665: arpl     word ptr [ebp + 0x55], sp
0x481668: push     ebx
0x481669: xor      dword ptr [ecx + ebp*2 + 0x72], eax
0x48166D: arpl     word ptr gs:[eax + edx*2 + 0x61], si
0x481672: imul     ebp, dword ptr [esi + 0x74], 0xd480015
0x481679: dec      ecx
0x48167A: add      byte ptr [esi], cl
0x48167C: inc      esp
0x48167D: inc      edx
0x48167E: outsd    dx, dword ptr [esi]
0x48167F: je       0x4816c8
0x481681: jne      0x4816ec
0x481683: insb     byte ptr es:[edi], dx
0x481684: inc      ebx
0x481686: insb     byte ptr es:[edi], dx
0x481687: imul     esp, dword ptr [ebx + 0x6b], 0x3844001b
0x48168E: dec      ecx
0x48168F: add      byte ptr [esp + eax*2], dl
0x481692: inc      edi
0x481693: jne      0x4816fe
0x481695: insb     byte ptr es:[edi], dx
0x481696: inc      esp
0x481698: insb     byte ptr es:[edi], dx
0x481699: inc      esp
0x48169B: imul     esi, dword ptr [edx + 0x65], 0x61507463
0x4816A2: imul     ebp, dword ptr [esi + 0x74], 0x3a480011
0x4816A9: dec      ecx
0x4816AA: add      byte ptr [edx], cl
0x4816AC: inc      esp
0x4816AD: inc      edi
0x4816AE: inc      esp
0x4816AF: push     ebp
0x4816B0: jo       0x4816f5
0x4816B2: insb     byte ptr es:[edi], dx
0x4816B3: imul     esp, dword ptr [ebx + 0x6b], 0x3a8c0013
0x4816BA: dec      ecx
0x4816BB: add      byte ptr [esp + eax*2], cl
0x4816BE: inc      edi
0x4816BF: inc      esp
0x4816C0: inc      esp
0x4816C1: outsd    dx, dword ptr [esi]
0x4816C2: ja       0x481732
0x4816C4: inc      ebx
0x4816C5: insb     byte ptr es:[edi], dx
0x4816C6: imul     esp, dword ptr [ebx + 0x6b], 0x3acc0014
0x4816CD: dec      ecx
0x4816CE: add      byte ptr [0x43444744], cl
0x4816D4: insb     byte ptr es:[edi], dx
0x4816D5: outsd    dx, dword ptr [esi]
0x4816D6: jae      0x48173d
0x4816D8: inc      ebx
0x4816D9: insb     byte ptr es:[edi], dx
0x4816DA: imul     esp, dword ptr [ebx + 0x6b], 0x3afc0013
0x4816E1: dec      ecx
0x4816E2: add      byte ptr [esp + eax*2], cl
0x4816E5: inc      edi
0x4816E6: inc      esp
0x4816E7: dec      eax
0x4816E8: outsd    dx, dword ptr [esi]
0x4816E9: insd     dword ptr es:[edi], dx
0x4816EA: inc      ebx
0x4816EC: insb     byte ptr es:[edi], dx
0x4816ED: imul     esp, dword ptr [ebx + 0x6b], 0x3b480013
0x4816F4: dec      ecx
0x4816F5: add      byte ptr [esp + eax*2], cl
0x4816F8: inc      edi
0x4816F9: inc      esp
0x4816FA: dec      esp
0x4816FB: imul     esi, dword ptr [ebx + 0x74], 0x63696c43
0x481702: imul     edx, dword ptr [0x493b9400], 0
0x481709: push     cs
0x48170A: inc      esp
0x48170B: inc      edi
0x48170C: inc      esp
0x48170D: inc      ecx
0x48170E: dec      ebp
0x481711: insd     dword ptr es:[edi], dx
0x481713: inc      ebx
0x481714: insb     byte ptr es:[edi], dx
0x481715: imul     esp, dword ptr [ebx + 0x6b], 0x3c500015
0x48171C: dec      ecx
0x48171D: add      byte ptr [esi], cl
0x48171F: inc      esp
0x481720: inc      edi
0x481721: inc      esp
0x481722: inc      esp
0x481723: insb     byte ptr es:[edi], dx
0x481725: dec      ebp
0x481726: insd     dword ptr es:[edi], dx
0x481728: inc      ebx
0x481729: insb     byte ptr es:[edi], dx
0x48172A: imul     esp, dword ptr [ebx + 0x6b], 0x3d0c0019
0x481731: dec      ecx
0x481732: add      byte ptr [edx], dl
0x481734: inc      esp
0x481735: inc      edi
0x481736: inc      esp
0x481737: inc      ebp
0x481738: imul     esi, dword ptr fs:[esi + ecx*2 + 0x6f], 0x65636974
0x481741: inc      ebx
0x481742: insb     byte ptr es:[edi], dx
0x481743: imul     esp, dword ptr [ebx + 0x6b], 0x3d580018
0x48174A: dec      ecx
0x48174B: add      byte ptr [ecx], dl
0x48174D: inc      esp
0x48174E: inc      edi
0x48174F: inc      esp
0x481750: inc      ebp
0x481751: imul     esi, dword ptr fs:[edi + eax*2 + 0x72], 0x43656461
0x48175A: insb     byte ptr es:[edi], dx
0x48175B: imul     esp, dword ptr [ebx + 0x6b], 0x40300014
0x481762: dec      ecx
0x481763: add      byte ptr [0x43454744], cl
0x481769: insb     byte ptr es:[edi], dx
0x48176A: outsd    dx, dword ptr [esi]
0x48176B: jae      0x4817d2
0x48176D: inc      ebx
0x48176E: insb     byte ptr es:[edi], dx
0x48176F: imul     esp, dword ptr [ebx + 0x6b], 0x40780011
0x481776: dec      ecx
0x481777: add      byte ptr [edx], cl
0x481779: inc      esp
0x48177A: inc      edi
0x48177B: inc      ebp
0x48177C: dec      edi
0x48177D: imul     eax, dword ptr [ebx + 0x6c], 0x69
0x481781: arpl     word ptr [ebx + 0x22], bp
0x481784: add      byte ptr [eax + 0x3f], bl
0x481787: dec      ecx
0x481788: add      byte ptr [ebx], bl
0x48178A: inc      esp
0x48178B: inc      edi
0x48178C: jne      0x4817f7
0x48178E: insb     byte ptr es:[edi], dx
0x48178F: inc      ebp
0x481791: imul     esi, dword ptr fs:[esi + ecx*2 + 0x6f], 0x65636974
0x48179A: inc      esp
0x48179B: imul     esi, dword ptr [edx + 0x65], 0x61507463
0x4817A2: imul     ebp, dword ptr [esi + 0x74], 0x415c0013
0x4817A9: dec      ecx
0x4817AA: add      byte ptr [esp + eax*2], cl
0x4817AD: inc      edi
0x4817AE: inc      esp
0x4817AF: inc      ebx
0x4817B0: push     0x6c437461
0x4817B5: imul     esp, dword ptr [ebx + 0x6b], 0x41d80017
0x4817BC: dec      ecx
0x4817BD: add      byte ptr [eax], dl
0x4817BF: inc      esp
0x4817C0: inc      edi
0x4817C1: outsd    dx, dword ptr [esi]
0x4817C2: insb     byte ptr es:[edi], dx
0x4817C3: inc      esp
0x4817C5: imul     esi, dword ptr [edx + 0x65], 0x61507463
0x4817CC: imul     ebp, dword ptr [esi + 0x74], 0x8498001d
0x4817D3: dec      eax
0x4817D4: add      byte ptr [esi], dl
0x4817D6: inc      esp
0x4817D7: dec      esi
0x4817D8: ja       0x48181c
0x4817DB: arpl     word ptr [ebx + 0x6f], sp
0x4817DE: jne      0x48184e
0x4817E0: je       0x481826
0x4817E2: imul     esi, dword ptr [edx + 0x65], 0x61507463
0x4817E9: imul     ebp, dword ptr [esi + 0x74], 0x4260001c
0x4817F0: dec      ecx
0x4817F1: add      byte ptr [0x6a644144], dl
0x4817F7: jne      0x48186c
0x4817F9: je       0x48183c
0x4817FB: bound    ebp, qword ptr [ecx + 0x6c]
0x4817FE: inc      ebx
0x4817FF: insb     byte ptr es:[edi], dx
0x481800: outsd    dx, dword ptr [esi]
0x481801: jae      0x481868
0x481803: inc      ebx
0x481804: insb     byte ptr es:[edi], dx
0x481805: imul     esp, dword ptr [ebx + 0x6b], 0x43040020
0x48180C: dec      ecx
0x48180D: add      byte ptr [ecx], bl
0x48180F: inc      esp
0x481810: inc      ecx
0x481811: push     0x75
0x481814: jae      0x48188a
0x481816: inc      ecx
0x481817: bound    ebp, qword ptr [ecx + 0x6c]
0x48181A: imul     esi, dword ptr [ecx + edi*2 + 0x44], 0x63657269
0x481822: je       0x481874
0x481824: popal    
0x481825: imul     ebp, dword ptr [esi + 0x74], 0x15cc0018
0x48182C: dec      ecx
0x48182D: add      byte ptr [ecx], dl
0x48182F: inc      esp
0x481830: inc      edx
0x481831: outsd    dx, dword ptr [esi]
0x481832: je       0x481884
0x481834: insb     byte ptr es:[edi], dx
0x481835: jne      0x4818aa
0x481837: inc      ecx
0x481838: bound    ebp, qword ptr [ecx + 0x6c]
0x48183B: inc      ebx
0x48183C: insb     byte ptr es:[edi], dx
0x48183D: imul     esp, dword ptr [ebx + 0x6b], 0x4f5c0013
0x481844: dec      ecx
0x481845: add      byte ptr [esp + eax*2], cl
0x481848: push     eax
0x481849: insb     byte ptr es:[edi], dx
0x48184A: jne      0x4818bf
0x48184C: inc      esp
0x48184D: inc      ebx
0x48184E: inc      ebx
0x48184F: insb     byte ptr es:[edi], dx
0x481850: imul     esp, dword ptr [ebx + 0x6b], 0x50a00014
0x481857: dec      ecx
0x481858: add      byte ptr [0x6e694d44], cl
0x48185E: jne      0x4818d3
0x481860: inc      esp
0x481861: inc      ebx
0x481862: inc      ebx
0x481863: insb     byte ptr es:[edi], dx
0x481864: imul     esp, dword ptr [ebx + 0x6b], 0x52a80019
0x48186B: dec      ecx
0x48186C: add      byte ptr [edx], dl
0x48186E: inc      esp
0x48186F: inc      ecx
0x481870: push     0x75
0x481873: jae      0x4818e9
0x481875: inc      ecx
0x481876: bound    ebp, qword ptr [ecx + 0x6c]
0x481879: dec      edi
0x48187A: imul     eax, dword ptr [ebx + 0x6c], 0x69
0x48187E: arpl     word ptr [ebx + 0x1e], bp
0x481881: add      byte ptr [esp + edi*4 + 0x48], bl
0x481885: add      byte ptr [edi], dl
0x481887: inc      esp
0x481888: inc      edx
0x481889: outsd    dx, dword ptr [esi]
0x48188A: je       0x4818dc
0x48188C: insb     byte ptr es:[edi], dx
0x48188D: jne      0x481902
0x48188F: inc      ecx
0x481890: bound    ebp, qword ptr [ecx + 0x6c]
0x481893: inc      esp
0x481894: imul     esi, dword ptr [edx + 0x65], 0x61507463
0x48189B: imul     ebp, dword ptr [esi + 0x74], 0x52e8001e
0x4818A2: dec      ecx
0x4818A3: add      byte ptr [edi], dl
0x4818A5: inc      esp
0x4818A6: inc      ecx
0x4818A7: push     0x75
0x4818AA: jae      0x481920
0x4818AC: inc      ecx
0x4818AD: bound    ebp, qword ptr [ecx + 0x6c]
0x4818B0: imul     esi, dword ptr [ecx + edi*2 + 0x4d], 0x6573756f
0x4818B8: dec      ebp
0x4818B9: outsd    dx, dword ptr [esi]
0x4818BA: jbe      0x481921
0x4818BC: sbb      eax, dword ptr [eax]
0x4818BE: inc      eax
0x4818BF: sub      byte ptr [ecx], cl
0x4818C2: adc      al, 0x44
0x4818C4: push     ebp
0x4818C5: jae      0x48192c
0x4818C7: jb       0x48191c
0x4818C9: je       0x48192c
0x4818CB: je       0x481932
0x4818CD: xor      dword ptr [ebp + 0x6f], ecx
0x4818D0: jne      0x481945
0x4818D2: inc      esp
0x4818D4: outsd    dx, dword ptr [esi]
0x4818D5: ja       0x481945
0x4818D7: pop      ss
0x4818D8: add      byte ptr [eax + 0x10004883], al
0x4818DE: inc      esp
0x4818DF: inc      ebp
0x4818E0: outsb    dx, byte ptr [esi]
0x4818E1: push     ebx
0x4818E3: jb       0x48195c
0x4818E6: jb       0x48191a
0x4818E9: inc      ebx
0x4818EA: insb     byte ptr es:[edi], dx
0x4818EB: imul     esp, dword ptr [ebx + 0x6b], 0x3dd80013
0x4818F2: dec      ecx
0x4818F3: add      byte ptr [esp + eax*2], cl
0x4818F6: inc      edi
0x4818F7: inc      esp
0x4818F8: inc      ecx
0x4818F9: insb     byte ptr es:[edi], dx
0x4818FA: insb     byte ptr es:[edi], dx
0x4818FB: jns      0x481940
0x4818FD: insb     byte ptr es:[edi], dx
0x4818FE: imul     esp, dword ptr [ebx + 0x6b], 0x3e880018
0x481905: dec      ecx
0x481906: add      byte ptr [ecx], dl
0x481908: inc      esp
0x481909: inc      edi
0x48190A: inc      esp
0x48190B: inc      edx
0x48190C: jb       0x481973
0x48190E: popal    
0x48190F: imul     eax, dword ptr [ecx + 0x6c], 0x6c
0x481913: jns      0x481958
0x481915: insb     byte ptr es:[edi], dx
0x481916: imul     esp, dword ptr [ebx + 0x6b], 0x7bf4001f
0x48191D: dec      eax
0x48191E: add      byte ptr [eax], bl
0x481920: inc      esp
0x481921: push     ebx
0x481922: insb     byte ptr es:[edi], dx
0x481924: push     ebx
0x481925: jb       0x48199e
0x481928: jb       0x48196f
0x48192B: insb     byte ptr es:[edi], dx
0x48192C: inc      esp
0x48192E: imul     esi, dword ptr [edx + 0x65], 0x61507463
0x481935: imul     ebp, dword ptr [esi + 0x74], 0x53e0001b
0x48193C: dec      ecx
0x48193D: add      byte ptr [esp + eax*2], dl
0x481940: push     ebx
0x481941: push     ebx
0x481942: jb       0x4819bb
0x481945: jb       0x481979
0x481948: inc      esp
0x481949: imul     esi, dword ptr [edx + 0x65], 0x61507463
0x481950: imul     ebp, dword ptr [esi + 0x74], 0x72465407
0x481957: insd     dword ptr es:[edi], dx
0x481958: inc      esp
0x481959: insb     byte ptr es:[edi], dx
0x48195A: add      eax, dword ptr [bx + si]
0x48195D: pushfd   
0x48195E: push     0x64680045
0x481963: inc      ebp
0x481964: add      byte ptr [eax], ah
0x481966: inc      bp
0x481968: add      byte ptr [ebp + 0x19700040], cl
0x48196E: dec      eax
0x48196F: add      byte ptr [edi], al
0x481971: pop      es
0x481972: push     esp
0x481973: inc      esi
0x481974: jb       0x4819e3
0x481976: inc      esp
0x481977: insb     byte ptr es:[edi], dx
0x481978: enter    0x47fe, 0
0x48197D: in       al, 0x9c
0x48197F: inc      ebx
0x481980: add      byte ptr [ebp], dl
0x481983: push     es
0x481984: inc      esi
0x481985: push     ebx
0x481986: je       0x4819e9
0x481988: je       0x4819ef
0x48198A: add      byte ptr [eax], al
0x48198C: push     ebp
0x48198D: mov      ebp, esp
0x48198F: add      esp, -0x10
0x481992: mov      dword ptr [ebp - 0x10], edx
0x481995: mov      dword ptr [ebp - 4], eax
0x481998: mov      eax, dword ptr [ebp - 4]
0x48199B: xor      edx, edx
0x48199D: mov      dword ptr [eax + 0x618], edx
0x4819A3: mov      dl, 1
0x4819A5: mov      eax, dword ptr [0x40df00]
0x4819AA: call     0x402e7c
0x4819AF: mov      edx, dword ptr [ebp - 4]
0x4819B2: mov      dword ptr [edx + 0x5dc], eax
0x4819B8: mov      eax, dword ptr [ebp - 4]
0x4819BB: mov      dword ptr [eax + 0x620], 1
0x4819C5: mov      eax, dword ptr [ebp - 4]
0x4819C8: xor      edx, edx
0x4819CA: mov      dword ptr [eax + 0x624], edx
0x4819D0: mov      eax, dword ptr [ebp - 4]
0x4819D3: mov      byte ptr [eax + 0x628], 0
0x4819DA: mov      eax, dword ptr [ebp - 4]
0x4819DD: xor      edx, edx
0x4819DF: mov      dword ptr [eax + 0x5e0], edx
0x4819E5: mov      eax, dword ptr [ebp - 4]
0x4819E8: xor      edx, edx
0x4819EA: mov      dword ptr [eax + 0x5e4], edx
0x4819F0: mov      dl, 1
0x4819F2: mov      eax, dword ptr [0x40df00]
0x4819F7: call     0x402e7c
0x4819FC: mov      edx, dword ptr [ebp - 4]
0x4819FF: mov      dword ptr [edx + 0x750], eax
0x481A05: mov      eax, dword ptr [ebp - 4]
0x481A08: add      eax, 0x758
0x481A0D: call     0x403a58
0x481A12: mov      dl, 1
0x481A14: mov      eax, dword ptr [0x40df00]
0x481A19: call     0x402e7c
0x481A1E: mov      edx, dword ptr [ebp - 4]
0x481A21: mov      dword ptr [edx + 0x764], eax
0x481A27: mov      eax, dword ptr [ebp - 4]
0x481A2A: mov      dword ptr [eax + 0x768], 0xffffffff
0x481A34: mov      eax, dword ptr [ebp - 4]
0x481A37: xor      edx, edx
0x481A39: mov      dword ptr [eax + 0x770], edx
0x481A3F: mov      eax, dword ptr [ebp - 4]
0x481A42: mov      byte ptr [eax + 0x774], 0
0x481A49: mov      eax, dword ptr [ebp - 4]
0x481A4C: mov      byte ptr [eax + 0x775], 0
0x481A53: mov      eax, dword ptr [ebp - 4]
0x481A56: mov      byte ptr [eax + 0x776], 0
0x481A5D: mov      eax, dword ptr [ebp - 4]
0x481A60: mov      byte ptr [eax + 0x777], 0
0x481A67: mov      eax, dword ptr [ebp - 4]
0x481A6A: xor      edx, edx
0x481A6C: mov      dword ptr [eax + 0x60c], edx
0x481A72: mov      dl, 1
0x481A74: mov      eax, dword ptr [0x40e37c]
0x481A79: call     0x402e7c
0x481A7E: mov      edx, dword ptr [ebp - 4]
0x481A81: mov      dword ptr [edx + 0x77c], eax
0x481A87: call     0x406154
0x481A8C: mov      edx, dword ptr [ebp - 4]
0x481A8F: mov      dword ptr [edx + 0x610], eax
0x481A95: mov      eax, dword ptr [ebp - 4]
0x481A98: xor      edx, edx
0x481A9A: mov      dword ptr [eax + 0x614], edx
0x481AA0: mov      eax, dword ptr [0x4a3e2c] ; *0x4A3E2C->0x4F92D4
0x481AA5: mov      byte ptr [eax], 0
0x481AA8: mov      eax, dword ptr [ebp - 4]
0x481AAB: add      eax, 0x994
0x481AB0: call     0x403a58
0x481AB5: mov      eax, dword ptr [ebp - 4]
0x481AB8: add      eax, 0x998
0x481ABD: call     0x403a58
0x481AC2: mov      eax, dword ptr [ebp - 4]
0x481AC5: mov      byte ptr [eax + 0x99c], 0
0x481ACC: mov      dl, 1
0x481ACE: mov      eax, dword ptr [0x40e37c]
0x481AD3: call     0x402e7c
0x481AD8: mov      edx, dword ptr [ebp - 4]
0x481ADB: mov      dword ptr [edx + 0x9a0], eax
0x481AE1: mov      dl, 1
0x481AE3: mov      eax, dword ptr [0x40e37c]
0x481AE8: call     0x402e7c
0x481AED: mov      edx, dword ptr [ebp - 4]
0x481AF0: mov      dword ptr [edx + 0x9a4], eax
0x481AF6: mov      dl, 1
0x481AF8: mov      eax, dword ptr [0x40e37c]
0x481AFD: call     0x402e7c
0x481B02: mov      edx, dword ptr [ebp - 4]
0x481B05: mov      dword ptr [edx + 0x9a8], eax
0x481B0B: mov      dl, 1
0x481B0D: mov      eax, dword ptr [0x40e37c]
0x481B12: call     0x402e7c
0x481B17: mov      edx, dword ptr [ebp - 4]
0x481B1A: mov      dword ptr [edx + 0x9ac], eax
0x481B20: mov      dl, 1
0x481B22: mov      eax, dword ptr [0x40e37c]
0x481B27: call     0x402e7c
0x481B2C: mov      edx, dword ptr [ebp - 4]
0x481B2F: mov      dword ptr [edx + 0x9b8], eax
0x481B35: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x481B3A: mov      eax, dword ptr [eax]
0x481B3C: mov      ecx, dword ptr [eax + 4]
0x481B3F: mov      dl, 1
0x481B41: mov      eax, dword ptr [0x41f814]
0x481B46: call     0x4219b8
0x481B4B: mov      edx, dword ptr [ebp - 4]
0x481B4E: mov      dword ptr [edx + 0x5e8], eax
0x481B54: mov      eax, dword ptr [ebp - 4]
0x481B57: mov      eax, dword ptr [eax + 0x5e8]
0x481B5D: mov      dword ptr [ebp - 8], eax
0x481B60: mov      edx, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x481B66: mov      edx, dword ptr [edx]
0x481B68: mov      eax, dword ptr [ebp - 8]
0x481B6B: mov      ecx, dword ptr [eax]
0x481B6D: call     dword ptr [ecx + 0x64]
0x481B70: xor      edx, edx
0x481B72: mov      eax, dword ptr [ebp - 8]
0x481B75: call     0x427b5c
0x481B7A: mov      edx, 0xffffff
0x481B7F: mov      eax, dword ptr [ebp - 8]
0x481B82: mov      eax, dword ptr [eax + 0x58]
0x481B85: call     0x4173fc
0x481B8A: mov      edx, 0xa
0x481B8F: mov      eax, dword ptr [ebp - 8]
0x481B92: mov      eax, dword ptr [eax + 0x58]
0x481B95: call     0x417690
0x481B9A: mov      edx, 0x1e
0x481B9F: mov      eax, dword ptr [ebp - 8]
0x481BA2: call     0x421ae0
0x481BA7: mov      edx, 0x10
0x481BAC: mov      eax, dword ptr [ebp - 8]
0x481BAF: call     0x4271fc
0x481BB4: xor      edx, edx
0x481BB6: mov      eax, dword ptr [ebp - 8]
0x481BB9: call     0x42d874
0x481BBE: mov      dl, 1
0x481BC0: mov      eax, dword ptr [ebp - 8]
0x481BC3: call     0x421a94
0x481BC8: xor      edx, edx
0x481BCA: mov      eax, dword ptr [ebp - 8]
0x481BCD: call     0x4278c0
0x481BD2: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x481BD7: mov      eax, dword ptr [eax]
0x481BD9: mov      ecx, dword ptr [eax + 4]
0x481BDC: mov      dl, 1
0x481BDE: mov      eax, dword ptr [0x420200]
0x481BE3: call     0x422710
0x481BE8: mov      edx, dword ptr [ebp - 4]
0x481BEB: mov      dword ptr [edx + 0x5ec], eax
0x481BF1: mov      eax, dword ptr [ebp - 4]
0x481BF4: mov      eax, dword ptr [eax + 0x5ec]
0x481BFA: mov      dword ptr [ebp - 0xc], eax
0x481BFD: mov      edx, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x481C03: mov      edx, dword ptr [edx]
0x481C05: mov      eax, dword ptr [ebp - 0xc]
0x481C08: mov      ecx, dword ptr [eax]
0x481C0A: call     dword ptr [ecx + 0x64]
0x481C0D: xor      edx, edx
0x481C0F: mov      eax, dword ptr [ebp - 0xc]
0x481C12: call     0x427b5c
0x481C17: mov      eax, dword ptr [ebp - 0xc]
0x481C1A: mov      eax, dword ptr [eax + 0x58]
0x481C1D: mov      edx, 0xffffff
0x481C22: call     0x4173fc
0x481C27: mov      eax, dword ptr [ebp - 0xc]
0x481C2A: mov      eax, dword ptr [eax + 0x58]
0x481C2D: mov      edx, 0xa
0x481C32: call     0x417690
0x481C37: xor      edx, edx
0x481C39: mov      eax, dword ptr [ebp - 0xc]
0x481C3C: call     0x42d874
0x481C41: mov      dl, 1
0x481C43: mov      eax, dword ptr [ebp - 0xc]
0x481C46: call     0x421a94
0x481C4B: xor      edx, edx
0x481C4D: mov      eax, dword ptr [ebp - 0xc]
0x481C50: call     0x4278c0
0x481C55: mov      esp, ebp
0x481C57: pop      ebp
0x481C58: ret      
0x481C59: lea      eax, [eax]
0x481C5C: push     ebp
0x481C5D: mov      ebp, esp
0x481C5F: add      esp, -8
0x481C62: mov      dword ptr [ebp - 8], edx
0x481C65: mov      dword ptr [ebp - 4], eax
0x481C68: mov      eax, dword ptr [ebp - 4]
0x481C6B: mov      eax, dword ptr [eax + 0x5dc]
0x481C71: call     0x402eac
0x481C76: mov      eax, dword ptr [ebp - 4]
0x481C79: mov      eax, dword ptr [eax + 0x750]
0x481C7F: call     0x402eac
0x481C84: mov      eax, dword ptr [ebp - 4]
0x481C87: mov      eax, dword ptr [eax + 0x764]
0x481C8D: call     0x402eac
0x481C92: mov      eax, dword ptr [ebp - 4]
0x481C95: mov      eax, dword ptr [eax + 0x77c]
0x481C9B: call     0x402eac
0x481CA0: mov      eax, dword ptr [ebp - 4]
0x481CA3: mov      eax, dword ptr [eax + 0x9a0]
0x481CA9: call     0x402eac
0x481CAE: mov      eax, dword ptr [ebp - 4]
0x481CB1: mov      eax, dword ptr [eax + 0x9a4]
0x481CB7: call     0x402eac
0x481CBC: mov      eax, dword ptr [ebp - 4]
0x481CBF: mov      eax, dword ptr [eax + 0x9a8]
0x481CC5: call     0x402eac
0x481CCA: mov      eax, dword ptr [ebp - 4]
0x481CCD: mov      eax, dword ptr [eax + 0x9ac]
0x481CD3: call     0x402eac
0x481CD8: mov      eax, dword ptr [ebp - 4]
0x481CDB: mov      eax, dword ptr [eax + 0x9b8]
0x481CE1: call     0x402eac
0x481CE6: pop      ecx
0x481CE7: pop      ecx
0x481CE8: pop      ebp
0x481CE9: ret      
0x481CEA: mov      eax, eax
0x481CEC: push     ebp
0x481CED: mov      ebp, esp
0x481CEF: add      esp, -0x10
0x481CF2: push     ebx
0x481CF3: mov      dword ptr [ebp - 4], eax
0x481CF6: mov      eax, dword ptr [ebp - 4]
0x481CF9: mov      eax, dword ptr [eax + 0x5dc]
0x481CFF: mov      bx, 0xffff
0x481D03: call     0x403080
0x481D08: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x481D0D: mov      eax, dword ptr [eax]
0x481D0F: call     0x42a8bc
0x481D14: dec      eax
0x481D15: test     eax, eax
0x481D17: jl       0x481d82
0x481D19: inc      eax
0x481D1A: mov      dword ptr [ebp - 0x10], eax
0x481D1D: mov      dword ptr [ebp - 8], 0
0x481D24: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x481D29: mov      eax, dword ptr [eax]
0x481D2B: mov      edx, dword ptr [ebp - 8]
0x481D2E: call     0x42a880
0x481D33: mov      dword ptr [ebp - 0xc], eax
0x481D36: mov      eax, dword ptr [ebp - 0xc]
0x481D39: mov      edx, dword ptr [0x41f814]
0x481D3F: call     0x40301c
0x481D44: test     al, al
0x481D46: je       0x481d7a
0x481D48: mov      eax, dword ptr [ebp - 0xc]
0x481D4B: cmp      byte ptr [eax + 0x47], 0
0x481D4F: je       0x481d7a
0x481D51: mov      eax, dword ptr [ebp - 4]
0x481D54: mov      eax, dword ptr [eax + 0x5e8]
0x481D5A: cmp      eax, dword ptr [ebp - 0xc]
0x481D5D: je       0x481d7a
0x481D5F: mov      eax, dword ptr [ebp - 4]
0x481D62: mov      eax, dword ptr [eax + 0x5dc]
0x481D68: mov      edx, dword ptr [ebp - 0xc]
0x481D6B: call     0x40f588
0x481D70: xor      edx, edx
0x481D72: mov      eax, dword ptr [ebp - 0xc]
0x481D75: call     0x4278c0
0x481D7A: inc      dword ptr [ebp - 8]
0x481D7D: dec      dword ptr [ebp - 0x10]
0x481D80: jne      0x481d24
0x481D82: pop      ebx
0x481D83: mov      esp, ebp
0x481D85: pop      ebp
0x481D86: ret      
0x481D87: nop      
0x481D88: push     ebp
0x481D89: mov      ebp, esp
0x481D8B: add      esp, -0x10
0x481D8E: mov      dword ptr [ebp - 4], eax
0x481D91: mov      eax, dword ptr [ebp - 4]
0x481D94: mov      eax, dword ptr [eax + 0x5dc]
0x481D9A: mov      eax, dword ptr [eax + 8]
0x481D9D: dec      eax
0x481D9E: test     eax, eax
0x481DA0: jl       0x481dcd
0x481DA2: inc      eax
0x481DA3: mov      dword ptr [ebp - 0xc], eax
0x481DA6: mov      dword ptr [ebp - 8], 0
0x481DAD: mov      eax, dword ptr [ebp - 4]
0x481DB0: mov      eax, dword ptr [eax + 0x5dc]
0x481DB6: mov      edx, dword ptr [ebp - 8]
0x481DB9: call     0x40f6a0
0x481DBE: mov      dl, 1
0x481DC0: call     0x4278c0
0x481DC5: inc      dword ptr [ebp - 8]
0x481DC8: dec      dword ptr [ebp - 0xc]
0x481DCB: jne      0x481dad
0x481DCD: mov      esp, ebp
0x481DCF: pop      ebp
0x481DD0: ret      
0x481DD1: lea      eax, [eax]
0x481DD4: push     ebp
0x481DD5: mov      ebp, esp
0x481DD7: add      esp, -0x14
0x481DDA: mov      dword ptr [ebp - 4], eax
0x481DDD: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x481DE2: mov      eax, dword ptr [eax]
0x481DE4: mov      eax, dword ptr [eax + 0x31c]
0x481DEA: call     0x458510
0x481DEF: xor      edx, edx
0x481DF1: mov      eax, dword ptr [ebp - 4]
0x481DF4: mov      eax, dword ptr [eax + 0x2c8]
0x481DFA: call     0x42718c
0x481DFF: xor      edx, edx
0x481E01: mov      eax, dword ptr [ebp - 4]
0x481E04: mov      eax, dword ptr [eax + 0x2c8]
0x481E0A: call     0x4271b0
0x481E0F: mov      edx, 0x320
0x481E14: mov      eax, dword ptr [ebp - 4]
0x481E17: mov      eax, dword ptr [eax + 0x2c8]
0x481E1D: call     0x4271d8
0x481E22: mov      edx, 0x258
0x481E27: mov      eax, dword ptr [ebp - 4]
0x481E2A: mov      eax, dword ptr [eax + 0x2c8]
0x481E30: call     0x4271fc
0x481E35: mov      eax, dword ptr [ebp - 4]
0x481E38: mov      eax, dword ptr [eax + 0x2c8]
0x481E3E: mov      byte ptr [eax + 0x24d], 1
0x481E45: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x481E4A: mov      eax, dword ptr [eax]
0x481E4C: mov      eax, dword ptr [eax + 0x31c]
0x481E52: mov      cl, 1
0x481E54: mov      edx, dword ptr [ebp - 4]
0x481E57: mov      edx, dword ptr [edx + 0x2c8]
0x481E5D: call     0x45852c
0x481E62: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x481E67: mov      eax, dword ptr [eax]
0x481E69: mov      eax, dword ptr [eax + 0x308]
0x481E6F: mov      edx, 0x168
0x481E74: call     0x45351c
0x481E79: mov      dword ptr [ebp - 0x10], eax
0x481E7C: cmp      dword ptr [ebp - 0x10], 0
0x481E80: je       0x481eea
0x481E82: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x481E87: mov      eax, dword ptr [eax]
0x481E89: mov      edx, dword ptr [eax + 0x308]
0x481E8F: mov      ecx, 0x168
0x481E94: mov      eax, dword ptr [ebp - 4]
0x481E97: mov      eax, dword ptr [eax + 0x368]
0x481E9D: call     0x4579f0
0x481EA2: mov      eax, dword ptr [ebp - 0x10]
0x481EA5: call     0x44dc60
0x481EAA: mov      edx, 0x320
0x481EAF: sub      edx, eax
0x481EB1: sar      edx, 1
0x481EB3: jns      0x481eb8
0x481EB5: adc      edx, 0
0x481EB8: mov      eax, dword ptr [ebp - 4]
0x481EBB: mov      eax, dword ptr [eax + 0x368]
0x481EC1: call     0x42718c
0x481EC6: mov      eax, dword ptr [ebp - 0x10]
0x481EC9: call     0x44d944
0x481ECE: mov      edx, 0x258
0x481ED3: sub      edx, eax
0x481ED5: sar      edx, 1
0x481ED7: jns      0x481edc
0x481ED9: adc      edx, 0
0x481EDC: mov      eax, dword ptr [ebp - 4]
0x481EDF: mov      eax, dword ptr [eax + 0x368]
0x481EE5: call     0x4271b0
0x481EEA: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x481EEF: mov      eax, dword ptr [eax]
0x481EF1: mov      edx, dword ptr [eax + 0x308]
0x481EF7: mov      ecx, 0x169
0x481EFC: mov      eax, dword ptr [ebp - 4]
0x481EFF: mov      eax, dword ptr [eax + 0x36c]
0x481F05: call     0x4579f0
0x481F0A: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x481F0F: mov      eax, dword ptr [eax]
0x481F11: mov      edx, dword ptr [eax + 0x308]
0x481F17: mov      ecx, 0x16b
0x481F1C: mov      eax, dword ptr [ebp - 4]
0x481F1F: mov      eax, dword ptr [eax + 0x370]
0x481F25: call     0x4579f0
0x481F2A: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x481F2F: mov      eax, dword ptr [eax]
0x481F31: mov      edx, dword ptr [eax + 0x308]
0x481F37: mov      ecx, 0x16d
0x481F3C: mov      eax, dword ptr [ebp - 4]
0x481F3F: mov      eax, dword ptr [eax + 0x374]
0x481F45: call     0x4579f0
0x481F4A: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x481F4F: mov      eax, dword ptr [eax]
0x481F51: mov      edx, dword ptr [eax + 0x308]
0x481F57: mov      ecx, 0x16f
0x481F5C: mov      eax, dword ptr [ebp - 4]
0x481F5F: mov      eax, dword ptr [eax + 0x378]
0x481F65: call     0x4579f0
0x481F6A: mov      edx, 0x7e
0x481F6F: mov      eax, dword ptr [ebp - 4]
0x481F72: mov      eax, dword ptr [eax + 0x36c]
0x481F78: call     0x4271b0
0x481F7D: mov      edx, 0x7e
0x481F82: mov      eax, dword ptr [ebp - 4]
0x481F85: mov      eax, dword ptr [eax + 0x370]
0x481F8B: call     0x4271b0
0x481F90: mov      edx, 0x7e
0x481F95: mov      eax, dword ptr [ebp - 4]
0x481F98: mov      eax, dword ptr [eax + 0x374]
0x481F9E: call     0x4271b0
0x481FA3: mov      edx, 0x7e
0x481FA8: mov      eax, dword ptr [ebp - 4]
0x481FAB: mov      eax, dword ptr [eax + 0x378]
0x481FB1: call     0x4271b0
0x481FB6: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x481FBB: mov      eax, dword ptr [eax]
0x481FBD: mov      eax, dword ptr [eax + 0x308]
0x481FC3: mov      edx, 0x3c
0x481FC8: call     0x45351c
0x481FCD: mov      dword ptr [ebp - 0x10], eax
0x481FD0: cmp      dword ptr [ebp - 0x10], 0
0x481FD4: je       0x48203e
0x481FD6: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x481FDB: mov      eax, dword ptr [eax]
0x481FDD: mov      edx, dword ptr [eax + 0x308]
0x481FE3: mov      ecx, 0x3c
0x481FE8: mov      eax, dword ptr [ebp - 4]
0x481FEB: mov      eax, dword ptr [eax + 0x2f8]
0x481FF1: call     0x4579f0
0x481FF6: mov      eax, dword ptr [ebp - 0x10]
0x481FF9: call     0x44dc60
0x481FFE: mov      edx, 0x320
0x482003: sub      edx, eax
0x482005: sar      edx, 1
0x482007: jns      0x48200c
0x482009: adc      edx, 0
0x48200C: mov      eax, dword ptr [ebp - 4]
0x48200F: mov      eax, dword ptr [eax + 0x2f8]
0x482015: call     0x42718c
0x48201A: mov      eax, dword ptr [ebp - 0x10]
0x48201D: call     0x44d944
0x482022: mov      edx, 0x258
0x482027: sub      edx, eax
0x482029: sar      edx, 1
0x48202B: jns      0x482030
0x48202D: adc      edx, 0
0x482030: mov      eax, dword ptr [ebp - 4]
0x482033: mov      eax, dword ptr [eax + 0x2f8]
0x482039: call     0x4271b0
0x48203E: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x482043: mov      eax, dword ptr [eax]
0x482045: mov      edx, dword ptr [eax + 0x308]
0x48204B: mov      ecx, 0x3e
0x482050: mov      eax, dword ptr [ebp - 4]
0x482053: mov      eax, dword ptr [eax + 0x300]
0x482059: call     0x4579f0
0x48205E: mov      edx, 0xa9
0x482063: mov      eax, dword ptr [ebp - 4]
0x482066: mov      eax, dword ptr [eax + 0x300]
0x48206C: call     0x42718c
0x482071: mov      edx, 0xa4
0x482076: mov      eax, dword ptr [ebp - 4]
0x482079: mov      eax, dword ptr [eax + 0x300]
0x48207F: call     0x4271b0
0x482084: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x482089: mov      eax, dword ptr [eax]
0x48208B: mov      edx, dword ptr [eax + 0x308]
0x482091: mov      ecx, 0x3d
0x482096: mov      eax, dword ptr [ebp - 4]
0x482099: mov      eax, dword ptr [eax + 0x2fc]
0x48209F: call     0x4579f0
0x4820A4: mov      edx, 0x19
0x4820A9: mov      eax, dword ptr [ebp - 4]
0x4820AC: mov      eax, dword ptr [eax + 0x2fc]
0x4820B2: call     0x42718c
0x4820B7: mov      edx, 0xcf
0x4820BC: mov      eax, dword ptr [ebp - 4]
0x4820BF: mov      eax, dword ptr [eax + 0x2fc]
0x4820C5: call     0x4271b0
0x4820CA: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4820CF: mov      eax, dword ptr [eax]
0x4820D1: mov      edx, dword ptr [eax + 0x308]
0x4820D7: mov      ecx, 0x35
0x4820DC: mov      eax, dword ptr [ebp - 4]
0x4820DF: mov      eax, dword ptr [eax + 0x364]
0x4820E5: call     0x4579f0
0x4820EA: mov      edx, 0x82
0x4820EF: mov      eax, dword ptr [ebp - 4]
0x4820F2: mov      eax, dword ptr [eax + 0x364]
0x4820F8: call     0x42718c
0x4820FD: mov      edx, 0xcf
0x482102: mov      eax, dword ptr [ebp - 4]
0x482105: mov      eax, dword ptr [eax + 0x364]
0x48210B: call     0x4271b0
0x482110: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x482115: mov      eax, dword ptr [eax]
0x482117: mov      edx, dword ptr [eax + 0x308]
0x48211D: mov      ecx, 0x40
0x482122: mov      eax, dword ptr [ebp - 4]
0x482125: mov      eax, dword ptr [eax + 0x30c]
0x48212B: call     0x4579f0
0x482130: mov      edx, 0xfc
0x482135: mov      eax, dword ptr [ebp - 4]
0x482138: mov      eax, dword ptr [eax + 0x30c]
0x48213E: call     0x42718c
0x482143: mov      edx, 0x1c
0x482148: mov      eax, dword ptr [ebp - 4]
0x48214B: mov      eax, dword ptr [eax + 0x30c]
0x482151: call     0x4271b0
0x482156: mov      eax, dword ptr [ebp - 4]
0x482159: mov      eax, dword ptr [eax + 0x580]
0x48215F: mov      byte ptr [eax + 0x24c], 0
0x482166: mov      eax, dword ptr [ebp - 4]
0x482169: mov      eax, dword ptr [eax + 0x488]
0x48216F: mov      byte ptr [eax + 0x24c], 0
0x482176: mov      eax, dword ptr [ebp - 4]
0x482179: mov      eax, dword ptr [eax + 0x48c]
0x48217F: mov      byte ptr [eax + 0x24c], 0
0x482186: mov      eax, dword ptr [ebp - 4]
0x482189: mov      eax, dword ptr [eax + 0x4bc]
0x48218F: mov      byte ptr [eax + 0x24c], 0
0x482196: mov      eax, dword ptr [ebp - 4]
0x482199: mov      eax, dword ptr [eax + 0x4c0]
0x48219F: mov      byte ptr [eax + 0x24c], 0
0x4821A6: mov      eax, dword ptr [ebp - 4]
0x4821A9: mov      eax, dword ptr [eax + 0x50c]
0x4821AF: mov      byte ptr [eax + 0x24c], 0
0x4821B6: mov      eax, dword ptr [ebp - 4]
0x4821B9: mov      eax, dword ptr [eax + 0x510]
0x4821BF: mov      byte ptr [eax + 0x24c], 0
0x4821C6: mov      eax, dword ptr [ebp - 4]
0x4821C9: mov      eax, dword ptr [eax + 0x588]
0x4821CF: mov      byte ptr [eax + 0x24c], 0
0x4821D6: mov      eax, dword ptr [ebp - 4]
0x4821D9: mov      eax, dword ptr [eax + 0x584]
0x4821DF: mov      byte ptr [eax + 0x24c], 0
0x4821E6: mov      eax, dword ptr [ebp - 4]
0x4821E9: mov      eax, dword ptr [eax + 0x58c]
0x4821EF: mov      byte ptr [eax + 0x24c], 0
0x4821F6: mov      eax, dword ptr [ebp - 4]
0x4821F9: mov      eax, dword ptr [eax + 0x590]
0x4821FF: mov      byte ptr [eax + 0x24c], 0
0x482206: mov      eax, dword ptr [ebp - 4]
0x482209: mov      eax, dword ptr [eax + 0x594]
0x48220F: mov      byte ptr [eax + 0x24c], 0
0x482216: mov      eax, dword ptr [ebp - 4]
0x482219: mov      eax, dword ptr [eax + 0x598]
0x48221F: mov      byte ptr [eax + 0x24c], 0
0x482226: mov      eax, dword ptr [ebp - 4]
0x482229: mov      eax, dword ptr [eax + 0x59c]
0x48222F: mov      byte ptr [eax + 0x24c], 0
0x482236: mov      eax, dword ptr [ebp - 4]
0x482239: mov      eax, dword ptr [eax + 0x5a0]
0x48223F: mov      byte ptr [eax + 0x24c], 0
0x482246: mov      eax, dword ptr [ebp - 4]
0x482249: mov      eax, dword ptr [eax + 0x5a4]
0x48224F: mov      byte ptr [eax + 0x24c], 0
0x482256: mov      eax, dword ptr [ebp - 4]
0x482259: mov      eax, dword ptr [eax + 0x5a8]
0x48225F: mov      byte ptr [eax + 0x24c], 0
0x482266: mov      eax, dword ptr [ebp - 4]
0x482269: mov      eax, dword ptr [eax + 0x5ac]
0x48226F: mov      byte ptr [eax + 0x24c], 0
0x482276: mov      eax, dword ptr [ebp - 4]
0x482279: mov      eax, dword ptr [eax + 0x5b0]
0x48227F: mov      byte ptr [eax + 0x24c], 0
0x482286: mov      eax, dword ptr [ebp - 4]
0x482289: mov      eax, dword ptr [eax + 0x5b4]
0x48228F: mov      byte ptr [eax + 0x24c], 0
0x482296: mov      eax, dword ptr [ebp - 4]
0x482299: mov      eax, dword ptr [eax + 0x5b8]
0x48229F: mov      byte ptr [eax + 0x24c], 0
0x4822A6: mov      eax, dword ptr [ebp - 4]
0x4822A9: mov      eax, dword ptr [eax + 0x5bc]
0x4822AF: mov      byte ptr [eax + 0x24c], 0
0x4822B6: mov      eax, dword ptr [ebp - 4]
0x4822B9: mov      eax, dword ptr [eax + 0x5c0]
0x4822BF: mov      byte ptr [eax + 0x24c], 0
0x4822C6: mov      eax, dword ptr [ebp - 4]
0x4822C9: mov      eax, dword ptr [eax + 0x5c4]
0x4822CF: mov      byte ptr [eax + 0x24c], 0
0x4822D6: mov      eax, dword ptr [ebp - 4]
0x4822D9: mov      eax, dword ptr [eax + 0x5c8]
0x4822DF: mov      byte ptr [eax + 0x24c], 0
0x4822E6: mov      eax, dword ptr [ebp - 4]
0x4822E9: mov      eax, dword ptr [eax + 0x5cc]
0x4822EF: mov      byte ptr [eax + 0x24c], 0
0x4822F6: mov      eax, dword ptr [ebp - 4]
0x4822F9: mov      eax, dword ptr [eax + 0x5d0]
0x4822FF: mov      byte ptr [eax + 0x24c], 0
0x482306: mov      eax, dword ptr [ebp - 4]
0x482309: mov      eax, dword ptr [eax + 0x5d4]
0x48230F: mov      byte ptr [eax + 0x24c], 0
0x482316: mov      eax, dword ptr [ebp - 4]
0x482319: mov      eax, dword ptr [eax + 0x5d8]
0x48231F: mov      byte ptr [eax + 0x24c], 0
0x482326: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x48232B: cmp      dword ptr [eax], 1
0x48232E: jl       0x482340
0x482330: mov      eax, dword ptr [ebp - 4]
0x482333: mov      eax, dword ptr [eax + 0x488]
0x482339: mov      byte ptr [eax + 0x24c], 1
0x482340: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x482345: cmp      dword ptr [eax], 2
0x482348: jl       0x48235a
0x48234A: mov      eax, dword ptr [ebp - 4]
0x48234D: mov      eax, dword ptr [eax + 0x48c]
0x482353: mov      byte ptr [eax + 0x24c], 1
0x48235A: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x48235F: cmp      dword ptr [eax], 3
0x482362: jl       0x482374
0x482364: mov      eax, dword ptr [ebp - 4]
0x482367: mov      eax, dword ptr [eax + 0x4bc]
0x48236D: mov      byte ptr [eax + 0x24c], 1
0x482374: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x482379: cmp      dword ptr [eax], 4
0x48237C: jl       0x48238e
0x48237E: mov      eax, dword ptr [ebp - 4]
0x482381: mov      eax, dword ptr [eax + 0x4c0]
0x482387: mov      byte ptr [eax + 0x24c], 1
0x48238E: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x482393: cmp      dword ptr [eax], 5
0x482396: jl       0x4823a8
0x482398: mov      eax, dword ptr [ebp - 4]
0x48239B: mov      eax, dword ptr [eax + 0x50c]
0x4823A1: mov      byte ptr [eax + 0x24c], 1
0x4823A8: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x4823AD: cmp      dword ptr [eax], 6
0x4823B0: jl       0x4823c2
0x4823B2: mov      eax, dword ptr [ebp - 4]
0x4823B5: mov      eax, dword ptr [eax + 0x510]
0x4823BB: mov      byte ptr [eax + 0x24c], 1
0x4823C2: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x4823C7: cmp      dword ptr [eax], 7
0x4823CA: jl       0x4823dc
0x4823CC: mov      eax, dword ptr [ebp - 4]
0x4823CF: mov      eax, dword ptr [eax + 0x588]
0x4823D5: mov      byte ptr [eax + 0x24c], 1
0x4823DC: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x4823E1: cmp      dword ptr [eax], 8
0x4823E4: jl       0x4823f6
0x4823E6: mov      eax, dword ptr [ebp - 4]
0x4823E9: mov      eax, dword ptr [eax + 0x584]
0x4823EF: mov      byte ptr [eax + 0x24c], 1
0x4823F6: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x4823FB: cmp      dword ptr [eax], 9
0x4823FE: jl       0x482410
0x482400: mov      eax, dword ptr [ebp - 4]
0x482403: mov      eax, dword ptr [eax + 0x58c]
0x482409: mov      byte ptr [eax + 0x24c], 1
0x482410: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x482415: cmp      dword ptr [eax], 0xa
0x482418: jl       0x48242a
0x48241A: mov      eax, dword ptr [ebp - 4]
0x48241D: mov      eax, dword ptr [eax + 0x590]
0x482423: mov      byte ptr [eax + 0x24c], 1
0x48242A: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x48242F: cmp      dword ptr [eax], 0xb
0x482432: jl       0x482444
0x482434: mov      eax, dword ptr [ebp - 4]
0x482437: mov      eax, dword ptr [eax + 0x594]
0x48243D: mov      byte ptr [eax + 0x24c], 1
0x482444: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x482449: cmp      dword ptr [eax], 0xc
0x48244C: jl       0x48245e
0x48244E: mov      eax, dword ptr [ebp - 4]
0x482451: mov      eax, dword ptr [eax + 0x598]
0x482457: mov      byte ptr [eax + 0x24c], 1
0x48245E: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x482463: cmp      dword ptr [eax], 0xd
0x482466: jl       0x482478
0x482468: mov      eax, dword ptr [ebp - 4]
0x48246B: mov      eax, dword ptr [eax + 0x59c]
0x482471: mov      byte ptr [eax + 0x24c], 1
0x482478: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x48247D: cmp      dword ptr [eax], 0xe
0x482480: jl       0x482492
0x482482: mov      eax, dword ptr [ebp - 4]
0x482485: mov      eax, dword ptr [eax + 0x5a0]
0x48248B: mov      byte ptr [eax + 0x24c], 1
0x482492: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x482497: cmp      dword ptr [eax], 0xf
0x48249A: jl       0x4824ac
0x48249C: mov      eax, dword ptr [ebp - 4]
0x48249F: mov      eax, dword ptr [eax + 0x5a4]
0x4824A5: mov      byte ptr [eax + 0x24c], 1
0x4824AC: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x4824B1: cmp      dword ptr [eax], 0x10
0x4824B4: jl       0x4824c6
0x4824B6: mov      eax, dword ptr [ebp - 4]
0x4824B9: mov      eax, dword ptr [eax + 0x5a8]
0x4824BF: mov      byte ptr [eax + 0x24c], 1
0x4824C6: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x4824CB: cmp      dword ptr [eax], 0x11
0x4824CE: jl       0x4824e0
0x4824D0: mov      eax, dword ptr [ebp - 4]
0x4824D3: mov      eax, dword ptr [eax + 0x5ac]
0x4824D9: mov      byte ptr [eax + 0x24c], 1
0x4824E0: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x4824E5: cmp      dword ptr [eax], 0x12
0x4824E8: jl       0x4824fa
0x4824EA: mov      eax, dword ptr [ebp - 4]
0x4824ED: mov      eax, dword ptr [eax + 0x5b0]
0x4824F3: mov      byte ptr [eax + 0x24c], 1
0x4824FA: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x4824FF: cmp      dword ptr [eax], 0x13
0x482502: jl       0x482514
0x482504: mov      eax, dword ptr [ebp - 4]
0x482507: mov      eax, dword ptr [eax + 0x5b4]
0x48250D: mov      byte ptr [eax + 0x24c], 1
0x482514: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x482519: cmp      dword ptr [eax], 0x14
0x48251C: jl       0x48252e
0x48251E: mov      eax, dword ptr [ebp - 4]
0x482521: mov      eax, dword ptr [eax + 0x5b8]
0x482527: mov      byte ptr [eax + 0x24c], 1
0x48252E: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x482533: cmp      dword ptr [eax], 0x15
0x482536: jl       0x482548
0x482538: mov      eax, dword ptr [ebp - 4]
0x48253B: mov      eax, dword ptr [eax + 0x5bc]
0x482541: mov      byte ptr [eax + 0x24c], 1
0x482548: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x48254D: cmp      dword ptr [eax], 0x16
0x482550: jl       0x482562
0x482552: mov      eax, dword ptr [ebp - 4]
0x482555: mov      eax, dword ptr [eax + 0x5c0]
0x48255B: mov      byte ptr [eax + 0x24c], 1
0x482562: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x482567: cmp      dword ptr [eax], 0x17
0x48256A: jl       0x48257c
0x48256C: mov      eax, dword ptr [ebp - 4]
0x48256F: mov      eax, dword ptr [eax + 0x5c4]
0x482575: mov      byte ptr [eax + 0x24c], 1
0x48257C: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x482581: cmp      dword ptr [eax], 0x18
0x482584: jl       0x482596
0x482586: mov      eax, dword ptr [ebp - 4]
0x482589: mov      eax, dword ptr [eax + 0x5c8]
0x48258F: mov      byte ptr [eax + 0x24c], 1
0x482596: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x48259B: cmp      dword ptr [eax], 0x19
0x48259E: jl       0x4825b0
0x4825A0: mov      eax, dword ptr [ebp - 4]
0x4825A3: mov      eax, dword ptr [eax + 0x5cc]
0x4825A9: mov      byte ptr [eax + 0x24c], 1
0x4825B0: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x4825B5: cmp      dword ptr [eax], 0x1a
0x4825B8: jl       0x4825ca
0x4825BA: mov      eax, dword ptr [ebp - 4]
0x4825BD: mov      eax, dword ptr [eax + 0x5d0]
0x4825C3: mov      byte ptr [eax + 0x24c], 1
0x4825CA: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x4825CF: cmp      dword ptr [eax], 0x1b
0x4825D2: jl       0x4825e4
0x4825D4: mov      eax, dword ptr [ebp - 4]
0x4825D7: mov      eax, dword ptr [eax + 0x5d4]
0x4825DD: mov      byte ptr [eax + 0x24c], 1
0x4825E4: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x4825E9: cmp      dword ptr [eax], 0x1c
0x4825EC: jl       0x4825fe
0x4825EE: mov      eax, dword ptr [ebp - 4]
0x4825F1: mov      eax, dword ptr [eax + 0x5d8]
0x4825F7: mov      byte ptr [eax + 0x24c], 1
0x4825FE: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x482603: cmp      dword ptr [eax], 8
0x482606: jg       0x48294a
0x48260C: mov      dword ptr [ebp - 0xc], 0x2a
0x482613: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x482618: mov      eax, dword ptr [eax]
0x48261A: imul     dword ptr [ebp - 0xc]
0x48261D: sar      eax, 1
0x48261F: jns      0x482624
0x482621: adc      eax, 0
0x482624: mov      edx, 0xeb
0x482629: sub      edx, eax
0x48262B: mov      dword ptr [ebp - 8], edx
0x48262E: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x482633: mov      eax, dword ptr [eax]
0x482635: mov      eax, dword ptr [eax + 0x308]
0x48263B: mov      edx, 0x100
0x482640: call     0x45351c
0x482645: mov      dword ptr [ebp - 0x10], eax
0x482648: cmp      dword ptr [ebp - 0x10], 0
0x48264C: je       0x4826b6
0x48264E: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x482653: mov      eax, dword ptr [eax]
0x482655: mov      edx, dword ptr [eax + 0x308]
0x48265B: mov      ecx, 0x100
0x482660: mov      eax, dword ptr [ebp - 4]
0x482663: mov      eax, dword ptr [eax + 0x480]
0x482669: call     0x4579f0
0x48266E: mov      eax, dword ptr [ebp - 0x10]
0x482671: call     0x44dc60
0x482676: mov      edx, 0x320
0x48267B: sub      edx, eax
0x48267D: sar      edx, 1
0x48267F: jns      0x482684
0x482681: adc      edx, 0
0x482684: mov      eax, dword ptr [ebp - 4]
0x482687: mov      eax, dword ptr [eax + 0x480]
0x48268D: call     0x42718c
0x482692: mov      eax, dword ptr [ebp - 0x10]
0x482695: call     0x44d944
0x48269A: mov      edx, 0x258
0x48269F: sub      edx, eax
0x4826A1: sar      edx, 1
0x4826A3: jns      0x4826a8
0x4826A5: adc      edx, 0
0x4826A8: mov      eax, dword ptr [ebp - 4]
0x4826AB: mov      eax, dword ptr [eax + 0x480]
0x4826B1: call     0x4271b0
0x4826B6: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4826BB: mov      eax, dword ptr [eax]
0x4826BD: mov      edx, dword ptr [eax + 0x308]
0x4826C3: mov      ecx, 0x40
0x4826C8: mov      eax, dword ptr [ebp - 4]
0x4826CB: mov      eax, dword ptr [eax + 0x484]
0x4826D1: call     0x4579f0
0x4826D6: mov      edx, 0xf4
0x4826DB: mov      eax, dword ptr [ebp - 4]
0x4826DE: mov      eax, dword ptr [eax + 0x484]
0x4826E4: call     0x42718c
0x4826E9: mov      edx, 0x1e
0x4826EE: mov      eax, dword ptr [ebp - 4]
0x4826F1: mov      eax, dword ptr [eax + 0x484]
0x4826F7: call     0x4271b0
0x4826FC: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x482701: mov      eax, dword ptr [eax]
0x482703: mov      edx, dword ptr [eax + 0x3c0]
0x482709: mov      ecx, 2
0x48270E: mov      eax, dword ptr [ebp - 4]
0x482711: mov      eax, dword ptr [eax + 0x488]
0x482717: call     0x4579f0
0x48271C: mov      edx, 0x3f
0x482721: mov      eax, dword ptr [ebp - 4]
0x482724: mov      eax, dword ptr [eax + 0x488]
0x48272A: call     0x42718c
0x48272F: xor      edx, edx
0x482731: add      edx, dword ptr [ebp - 8]
0x482734: mov      eax, dword ptr [ebp - 4]
0x482737: mov      eax, dword ptr [eax + 0x488]
0x48273D: call     0x4271b0
0x482742: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x482747: mov      eax, dword ptr [eax]
0x482749: mov      edx, dword ptr [eax + 0x3c0]
0x48274F: mov      ecx, 2
0x482754: mov      eax, dword ptr [ebp - 4]
0x482757: mov      eax, dword ptr [eax + 0x48c]
0x48275D: call     0x4579f0
0x482762: mov      edx, 0x3f
0x482767: mov      eax, dword ptr [ebp - 4]
0x48276A: mov      eax, dword ptr [eax + 0x48c]
0x482770: call     0x42718c
0x482775: mov      edx, dword ptr [ebp - 0xc]
0x482778: add      edx, dword ptr [ebp - 8]
0x48277B: mov      eax, dword ptr [ebp - 4]
0x48277E: mov      eax, dword ptr [eax + 0x48c]
0x482784: call     0x4271b0
0x482789: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48278E: mov      eax, dword ptr [eax]
0x482790: mov      edx, dword ptr [eax + 0x3c0]
0x482796: mov      ecx, 2
0x48279B: mov      eax, dword ptr [ebp - 4]
0x48279E: mov      eax, dword ptr [eax + 0x4bc]
0x4827A4: call     0x4579f0
0x4827A9: mov      edx, 0x3f
0x4827AE: mov      eax, dword ptr [ebp - 4]
0x4827B1: mov      eax, dword ptr [eax + 0x4bc]
0x4827B7: call     0x42718c
0x4827BC: mov      edx, dword ptr [ebp - 0xc]
0x4827BF: add      edx, edx
0x4827C1: add      edx, dword ptr [ebp - 8]
0x4827C4: mov      eax, dword ptr [ebp - 4]
0x4827C7: mov      eax, dword ptr [eax + 0x4bc]
0x4827CD: call     0x4271b0
0x4827D2: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4827D7: mov      eax, dword ptr [eax]
0x4827D9: mov      edx, dword ptr [eax + 0x3c0]
0x4827DF: mov      ecx, 2
0x4827E4: mov      eax, dword ptr [ebp - 4]
0x4827E7: mov      eax, dword ptr [eax + 0x4c0]
0x4827ED: call     0x4579f0
0x4827F2: mov      edx, 0x3f
0x4827F7: mov      eax, dword ptr [ebp - 4]
0x4827FA: mov      eax, dword ptr [eax + 0x4c0]
0x482800: call     0x42718c
0x482805: mov      edx, dword ptr [ebp - 0xc]
0x482808: lea      edx, [edx + edx*2]
0x48280B: add      edx, dword ptr [ebp - 8]
0x48280E: mov      eax, dword ptr [ebp - 4]
0x482811: mov      eax, dword ptr [eax + 0x4c0]
0x482817: call     0x4271b0
0x48281C: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x482821: mov      eax, dword ptr [eax]
0x482823: mov      edx, dword ptr [eax + 0x3c0]
0x482829: mov      ecx, 2
0x48282E: mov      eax, dword ptr [ebp - 4]
0x482831: mov      eax, dword ptr [eax + 0x50c]
0x482837: call     0x4579f0
0x48283C: mov      edx, 0x3f
0x482841: mov      eax, dword ptr [ebp - 4]
0x482844: mov      eax, dword ptr [eax + 0x50c]
0x48284A: call     0x42718c
0x48284F: mov      edx, dword ptr [ebp - 0xc]
0x482852: shl      edx, 2
0x482855: add      edx, dword ptr [ebp - 8]
0x482858: mov      eax, dword ptr [ebp - 4]
0x48285B: mov      eax, dword ptr [eax + 0x50c]
0x482861: call     0x4271b0
0x482866: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48286B: mov      eax, dword ptr [eax]
0x48286D: mov      edx, dword ptr [eax + 0x3c0]
0x482873: mov      ecx, 2
0x482878: mov      eax, dword ptr [ebp - 4]
0x48287B: mov      eax, dword ptr [eax + 0x510]
0x482881: call     0x4579f0
0x482886: mov      edx, 0x3f
0x48288B: mov      eax, dword ptr [ebp - 4]
0x48288E: mov      eax, dword ptr [eax + 0x510]
0x482894: call     0x42718c
0x482899: mov      edx, dword ptr [ebp - 0xc]
0x48289C: lea      edx, [edx + edx*4]
0x48289F: add      edx, dword ptr [ebp - 8]
0x4828A2: mov      eax, dword ptr [ebp - 4]
0x4828A5: mov      eax, dword ptr [eax + 0x510]
0x4828AB: call     0x4271b0
0x4828B0: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4828B5: mov      eax, dword ptr [eax]
0x4828B7: mov      edx, dword ptr [eax + 0x3c0]
0x4828BD: mov      ecx, 2
0x4828C2: mov      eax, dword ptr [ebp - 4]
0x4828C5: mov      eax, dword ptr [eax + 0x588]
0x4828CB: call     0x4579f0
0x4828D0: mov      edx, 0x3f
0x4828D5: mov      eax, dword ptr [ebp - 4]
0x4828D8: mov      eax, dword ptr [eax + 0x588]
0x4828DE: call     0x42718c
0x4828E3: mov      edx, dword ptr [ebp - 0xc]
0x4828E6: add      edx, edx
0x4828E8: lea      edx, [edx + edx*2]
0x4828EB: add      edx, dword ptr [ebp - 8]
0x4828EE: mov      eax, dword ptr [ebp - 4]
0x4828F1: mov      eax, dword ptr [eax + 0x588]
0x4828F7: call     0x4271b0
0x4828FC: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x482901: mov      eax, dword ptr [eax]
0x482903: mov      edx, dword ptr [eax + 0x3c0]
0x482909: mov      ecx, 2
0x48290E: mov      eax, dword ptr [ebp - 4]
0x482911: mov      eax, dword ptr [eax + 0x584]
0x482917: call     0x4579f0
0x48291C: mov      edx, 0x3f
0x482921: mov      eax, dword ptr [ebp - 4]
0x482924: mov      eax, dword ptr [eax + 0x584]
0x48292A: call     0x42718c
0x48292F: mov      edx, dword ptr [ebp - 0xc]
0x482932: mov      eax, edx
0x482934: shl      edx, 3
0x482937: sub      edx, eax
0x482939: add      edx, dword ptr [ebp - 8]
0x48293C: mov      eax, dword ptr [ebp - 4]
0x48293F: mov      eax, dword ptr [eax + 0x584]
0x482945: call     0x4271b0
0x48294A: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x48294F: cmp      dword ptr [eax], 8
0x482952: jle      0x482ef5
0x482958: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x48295D: cmp      dword ptr [eax], 0x10
0x482960: jg       0x482ef5
0x482966: mov      dword ptr [ebp - 0xc], 0x2a
0x48296D: mov      eax, dword ptr [ebp - 0xc]
0x482970: shl      eax, 4
0x482973: sar      eax, 1
0x482975: jns      0x48297a
0x482977: adc      eax, 0
0x48297A: sar      eax, 1
0x48297C: jns      0x482981
0x48297E: adc      eax, 0
0x482981: mov      edx, 0xeb
0x482986: sub      edx, eax
0x482988: mov      dword ptr [ebp - 8], edx
0x48298B: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x482990: mov      eax, dword ptr [eax]
0x482992: mov      eax, dword ptr [eax + 0x3c0]
0x482998: mov      edx, 4
0x48299D: call     0x45351c
0x4829A2: mov      dword ptr [ebp - 0x10], eax
0x4829A5: cmp      dword ptr [ebp - 0x10], 0
0x4829A9: je       0x482a13
0x4829AB: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4829B0: mov      eax, dword ptr [eax]
0x4829B2: mov      edx, dword ptr [eax + 0x3c0]
0x4829B8: mov      ecx, 4
0x4829BD: mov      eax, dword ptr [ebp - 4]
0x4829C0: mov      eax, dword ptr [eax + 0x480]
0x4829C6: call     0x4579f0
0x4829CB: mov      eax, dword ptr [ebp - 0x10]
0x4829CE: call     0x44dc60
0x4829D3: mov      edx, 0x320
0x4829D8: sub      edx, eax
0x4829DA: sar      edx, 1
0x4829DC: jns      0x4829e1
0x4829DE: adc      edx, 0
0x4829E1: mov      eax, dword ptr [ebp - 4]
0x4829E4: mov      eax, dword ptr [eax + 0x480]
0x4829EA: call     0x42718c
0x4829EF: mov      eax, dword ptr [ebp - 0x10]
0x4829F2: call     0x44d944
0x4829F7: mov      edx, 0x258
0x4829FC: sub      edx, eax
0x4829FE: sar      edx, 1
0x482A00: jns      0x482a05
0x482A02: adc      edx, 0
0x482A05: mov      eax, dword ptr [ebp - 4]
0x482A08: mov      eax, dword ptr [eax + 0x480]
0x482A0E: call     0x4271b0
0x482A13: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x482A18: mov      eax, dword ptr [eax]
0x482A1A: mov      edx, dword ptr [eax + 0x308]
0x482A20: mov      ecx, 0x40
0x482A25: mov      eax, dword ptr [ebp - 4]
0x482A28: mov      eax, dword ptr [eax + 0x484]
0x482A2E: call     0x4579f0
0x482A33: mov      edx, 0x15c
0x482A38: mov      eax, dword ptr [ebp - 4]
0x482A3B: mov      eax, dword ptr [eax + 0x484]
0x482A41: call     0x42718c
0x482A46: mov      edx, 0x1f
0x482A4B: mov      eax, dword ptr [ebp - 4]
0x482A4E: mov      eax, dword ptr [eax + 0x484]
0x482A54: call     0x4271b0
0x482A59: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x482A5E: mov      eax, dword ptr [eax]
0x482A60: mov      edx, dword ptr [eax + 0x3c0]
0x482A66: mov      ecx, 2
0x482A6B: mov      eax, dword ptr [ebp - 4]
0x482A6E: mov      eax, dword ptr [eax + 0x488]
0x482A74: call     0x4579f0
0x482A79: mov      edx, 0x19
0x482A7E: mov      eax, dword ptr [ebp - 4]
0x482A81: mov      eax, dword ptr [eax + 0x488]
0x482A87: call     0x42718c
0x482A8C: xor      edx, edx
0x482A8E: add      edx, dword ptr [ebp - 8]
0x482A91: mov      eax, dword ptr [ebp - 4]
0x482A94: mov      eax, dword ptr [eax + 0x488]
0x482A9A: call     0x4271b0
0x482A9F: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x482AA4: mov      eax, dword ptr [eax]
0x482AA6: mov      edx, dword ptr [eax + 0x3c0]
0x482AAC: mov      ecx, 2
0x482AB1: mov      eax, dword ptr [ebp - 4]
0x482AB4: mov      eax, dword ptr [eax + 0x48c]
0x482ABA: call     0x4579f0
0x482ABF: mov      edx, 0x19
0x482AC4: mov      eax, dword ptr [ebp - 4]
0x482AC7: mov      eax, dword ptr [eax + 0x48c]
0x482ACD: call     0x42718c
0x482AD2: mov      edx, dword ptr [ebp - 0xc]
0x482AD5: add      edx, dword ptr [ebp - 8]
0x482AD8: mov      eax, dword ptr [ebp - 4]
0x482ADB: mov      eax, dword ptr [eax + 0x48c]
0x482AE1: call     0x4271b0
0x482AE6: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x482AEB: mov      eax, dword ptr [eax]
0x482AED: mov      edx, dword ptr [eax + 0x3c0]
0x482AF3: mov      ecx, 2
0x482AF8: mov      eax, dword ptr [ebp - 4]
0x482AFB: mov      eax, dword ptr [eax + 0x4bc]
0x482B01: call     0x4579f0
0x482B06: mov      edx, 0x19
0x482B0B: mov      eax, dword ptr [ebp - 4]
0x482B0E: mov      eax, dword ptr [eax + 0x4bc]
0x482B14: call     0x42718c
0x482B19: mov      edx, dword ptr [ebp - 0xc]
0x482B1C: add      edx, edx
0x482B1E: add      edx, dword ptr [ebp - 8]
0x482B21: mov      eax, dword ptr [ebp - 4]
0x482B24: mov      eax, dword ptr [eax + 0x4bc]
0x482B2A: call     0x4271b0
0x482B2F: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x482B34: mov      eax, dword ptr [eax]
0x482B36: mov      edx, dword ptr [eax + 0x3c0]
0x482B3C: mov      ecx, 2
0x482B41: mov      eax, dword ptr [ebp - 4]
0x482B44: mov      eax, dword ptr [eax + 0x4c0]
0x482B4A: call     0x4579f0
0x482B4F: mov      edx, 0x19
0x482B54: mov      eax, dword ptr [ebp - 4]
0x482B57: mov      eax, dword ptr [eax + 0x4c0]
0x482B5D: call     0x42718c
0x482B62: mov      edx, dword ptr [ebp - 0xc]
0x482B65: lea      edx, [edx + edx*2]
0x482B68: add      edx, dword ptr [ebp - 8]
0x482B6B: mov      eax, dword ptr [ebp - 4]
0x482B6E: mov      eax, dword ptr [eax + 0x4c0]
0x482B74: call     0x4271b0
0x482B79: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x482B7E: mov      eax, dword ptr [eax]
0x482B80: mov      edx, dword ptr [eax + 0x3c0]
0x482B86: mov      ecx, 2
0x482B8B: mov      eax, dword ptr [ebp - 4]
0x482B8E: mov      eax, dword ptr [eax + 0x50c]
0x482B94: call     0x4579f0
0x482B99: mov      edx, 0x19
0x482B9E: mov      eax, dword ptr [ebp - 4]
0x482BA1: mov      eax, dword ptr [eax + 0x50c]
0x482BA7: call     0x42718c
0x482BAC: mov      edx, dword ptr [ebp - 0xc]
0x482BAF: shl      edx, 2
0x482BB2: add      edx, dword ptr [ebp - 8]
0x482BB5: mov      eax, dword ptr [ebp - 4]
0x482BB8: mov      eax, dword ptr [eax + 0x50c]
0x482BBE: call     0x4271b0
0x482BC3: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x482BC8: mov      eax, dword ptr [eax]
0x482BCA: mov      edx, dword ptr [eax + 0x3c0]
0x482BD0: mov      ecx, 2
0x482BD5: mov      eax, dword ptr [ebp - 4]
0x482BD8: mov      eax, dword ptr [eax + 0x510]
0x482BDE: call     0x4579f0
0x482BE3: mov      edx, 0x19
0x482BE8: mov      eax, dword ptr [ebp - 4]
0x482BEB: mov      eax, dword ptr [eax + 0x510]
0x482BF1: call     0x42718c
0x482BF6: mov      edx, dword ptr [ebp - 0xc]
0x482BF9: lea      edx, [edx + edx*4]
0x482BFC: add      edx, dword ptr [ebp - 8]
0x482BFF: mov      eax, dword ptr [ebp - 4]
0x482C02: mov      eax, dword ptr [eax + 0x510]
0x482C08: call     0x4271b0
0x482C0D: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x482C12: mov      eax, dword ptr [eax]
0x482C14: mov      edx, dword ptr [eax + 0x3c0]
0x482C1A: mov      ecx, 2
0x482C1F: mov      eax, dword ptr [ebp - 4]
0x482C22: mov      eax, dword ptr [eax + 0x588]
0x482C28: call     0x4579f0
0x482C2D: mov      edx, 0x19
0x482C32: mov      eax, dword ptr [ebp - 4]
0x482C35: mov      eax, dword ptr [eax + 0x588]
0x482C3B: call     0x42718c
0x482C40: mov      edx, dword ptr [ebp - 0xc]
0x482C43: add      edx, edx
0x482C45: lea      edx, [edx + edx*2]
0x482C48: add      edx, dword ptr [ebp - 8]
0x482C4B: mov      eax, dword ptr [ebp - 4]
0x482C4E: mov      eax, dword ptr [eax + 0x588]
0x482C54: call     0x4271b0
0x482C59: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x482C5E: mov      eax, dword ptr [eax]
0x482C60: mov      edx, dword ptr [eax + 0x3c0]
0x482C66: mov      ecx, 2
0x482C6B: mov      eax, dword ptr [ebp - 4]
0x482C6E: mov      eax, dword ptr [eax + 0x584]
0x482C74: call     0x4579f0
0x482C79: mov      edx, 0x19
0x482C7E: mov      eax, dword ptr [ebp - 4]
0x482C81: mov      eax, dword ptr [eax + 0x584]
0x482C87: call     0x42718c
0x482C8C: mov      edx, dword ptr [ebp - 0xc]
0x482C8F: mov      eax, edx
0x482C91: shl      edx, 3
0x482C94: sub      edx, eax
0x482C96: add      edx, dword ptr [ebp - 8]
0x482C99: mov      eax, dword ptr [ebp - 4]
0x482C9C: mov      eax, dword ptr [eax + 0x584]
0x482CA2: call     0x4271b0
0x482CA7: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x482CAC: mov      eax, dword ptr [eax]
0x482CAE: mov      edx, dword ptr [eax + 0x3c0]
0x482CB4: mov      ecx, 2
0x482CB9: mov      eax, dword ptr [ebp - 4]
0x482CBC: mov      eax, dword ptr [eax + 0x58c]
0x482CC2: call     0x4579f0
0x482CC7: mov      edx, 0xc3
0x482CCC: mov      eax, dword ptr [ebp - 4]
0x482CCF: mov      eax, dword ptr [eax + 0x58c]
0x482CD5: call     0x42718c
0x482CDA: xor      edx, edx
0x482CDC: add      edx, dword ptr [ebp - 8]
0x482CDF: mov      eax, dword ptr [ebp - 4]
0x482CE2: mov      eax, dword ptr [eax + 0x58c]
0x482CE8: call     0x4271b0
0x482CED: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x482CF2: mov      eax, dword ptr [eax]
0x482CF4: mov      edx, dword ptr [eax + 0x3c0]
0x482CFA: mov      ecx, 2
0x482CFF: mov      eax, dword ptr [ebp - 4]
0x482D02: mov      eax, dword ptr [eax + 0x590]
0x482D08: call     0x4579f0
0x482D0D: mov      edx, 0xc3
0x482D12: mov      eax, dword ptr [ebp - 4]
0x482D15: mov      eax, dword ptr [eax + 0x590]
0x482D1B: call     0x42718c
0x482D20: mov      edx, dword ptr [ebp - 0xc]
0x482D23: add      edx, dword ptr [ebp - 8]
0x482D26: mov      eax, dword ptr [ebp - 4]
0x482D29: mov      eax, dword ptr [eax + 0x590]
0x482D2F: call     0x4271b0
0x482D34: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x482D39: mov      eax, dword ptr [eax]
0x482D3B: mov      edx, dword ptr [eax + 0x3c0]
0x482D41: mov      ecx, 2
0x482D46: mov      eax, dword ptr [ebp - 4]
0x482D49: mov      eax, dword ptr [eax + 0x594]
0x482D4F: call     0x4579f0
0x482D54: mov      edx, 0xc3
0x482D59: mov      eax, dword ptr [ebp - 4]
0x482D5C: mov      eax, dword ptr [eax + 0x594]
0x482D62: call     0x42718c
0x482D67: mov      edx, dword ptr [ebp - 0xc]
0x482D6A: add      edx, edx
0x482D6C: add      edx, dword ptr [ebp - 8]
0x482D6F: mov      eax, dword ptr [ebp - 4]
0x482D72: mov      eax, dword ptr [eax + 0x594]
0x482D78: call     0x4271b0
0x482D7D: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x482D82: mov      eax, dword ptr [eax]
0x482D84: mov      edx, dword ptr [eax + 0x3c0]
0x482D8A: mov      ecx, 2
0x482D8F: mov      eax, dword ptr [ebp - 4]
0x482D92: mov      eax, dword ptr [eax + 0x598]
0x482D98: call     0x4579f0
0x482D9D: mov      edx, 0xc3
0x482DA2: mov      eax, dword ptr [ebp - 4]
0x482DA5: mov      eax, dword ptr [eax + 0x598]
0x482DAB: call     0x42718c
0x482DB0: mov      edx, dword ptr [ebp - 0xc]
0x482DB3: lea      edx, [edx + edx*2]
0x482DB6: add      edx, dword ptr [ebp - 8]
0x482DB9: mov      eax, dword ptr [ebp - 4]
0x482DBC: mov      eax, dword ptr [eax + 0x598]
0x482DC2: call     0x4271b0
0x482DC7: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x482DCC: mov      eax, dword ptr [eax]
0x482DCE: mov      edx, dword ptr [eax + 0x3c0]
0x482DD4: mov      ecx, 2
0x482DD9: mov      eax, dword ptr [ebp - 4]
0x482DDC: mov      eax, dword ptr [eax + 0x59c]
0x482DE2: call     0x4579f0
0x482DE7: mov      edx, 0xc3
0x482DEC: mov      eax, dword ptr [ebp - 4]
0x482DEF: mov      eax, dword ptr [eax + 0x59c]
0x482DF5: call     0x42718c
0x482DFA: mov      edx, dword ptr [ebp - 0xc]
0x482DFD: shl      edx, 2
0x482E00: add      edx, dword ptr [ebp - 8]
0x482E03: mov      eax, dword ptr [ebp - 4]
0x482E06: mov      eax, dword ptr [eax + 0x59c]
0x482E0C: call     0x4271b0
0x482E11: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x482E16: mov      eax, dword ptr [eax]
0x482E18: mov      edx, dword ptr [eax + 0x3c0]
0x482E1E: mov      ecx, 2
0x482E23: mov      eax, dword ptr [ebp - 4]
0x482E26: mov      eax, dword ptr [eax + 0x5a0]
0x482E2C: call     0x4579f0
0x482E31: mov      edx, 0xc3
0x482E36: mov      eax, dword ptr [ebp - 4]
0x482E39: mov      eax, dword ptr [eax + 0x5a0]
0x482E3F: call     0x42718c
0x482E44: mov      edx, dword ptr [ebp - 0xc]
0x482E47: lea      edx, [edx + edx*4]
0x482E4A: add      edx, dword ptr [ebp - 8]
0x482E4D: mov      eax, dword ptr [ebp - 4]
0x482E50: mov      eax, dword ptr [eax + 0x5a0]
0x482E56: call     0x4271b0
0x482E5B: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x482E60: mov      eax, dword ptr [eax]
0x482E62: mov      edx, dword ptr [eax + 0x3c0]
0x482E68: mov      ecx, 2
0x482E6D: mov      eax, dword ptr [ebp - 4]
0x482E70: mov      eax, dword ptr [eax + 0x5a4]
0x482E76: call     0x4579f0
0x482E7B: mov      edx, 0xc3
0x482E80: mov      eax, dword ptr [ebp - 4]
0x482E83: mov      eax, dword ptr [eax + 0x5a4]
0x482E89: call     0x42718c
0x482E8E: mov      edx, dword ptr [ebp - 0xc]
0x482E91: add      edx, edx
0x482E93: lea      edx, [edx + edx*2]
0x482E96: add      edx, dword ptr [ebp - 8]
0x482E99: mov      eax, dword ptr [ebp - 4]
0x482E9C: mov      eax, dword ptr [eax + 0x5a4]
0x482EA2: call     0x4271b0
0x482EA7: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x482EAC: mov      eax, dword ptr [eax]
0x482EAE: mov      edx, dword ptr [eax + 0x3c0]
0x482EB4: mov      ecx, 2
0x482EB9: mov      eax, dword ptr [ebp - 4]
0x482EBC: mov      eax, dword ptr [eax + 0x5a8]
0x482EC2: call     0x4579f0
0x482EC7: mov      edx, 0xc3
0x482ECC: mov      eax, dword ptr [ebp - 4]
0x482ECF: mov      eax, dword ptr [eax + 0x5a8]
0x482ED5: call     0x42718c
0x482EDA: mov      edx, dword ptr [ebp - 0xc]
0x482EDD: mov      eax, edx
0x482EDF: shl      edx, 3
0x482EE2: sub      edx, eax
0x482EE4: add      edx, dword ptr [ebp - 8]
0x482EE7: mov      eax, dword ptr [ebp - 4]
0x482EEA: mov      eax, dword ptr [eax + 0x5a8]
0x482EF0: call     0x4271b0
0x482EF5: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x482EFA: cmp      dword ptr [eax], 0x10
0x482EFD: jle      0x4836d9
0x482F03: mov      dword ptr [ebp - 0xc], 0x2a
0x482F0A: mov      eax, dword ptr [ebp - 0xc]
0x482F0D: shl      eax, 3
0x482F10: sar      eax, 1
0x482F12: jns      0x482f17
0x482F14: adc      eax, 0
0x482F17: mov      edx, 0xeb
0x482F1C: sub      edx, eax
0x482F1E: mov      dword ptr [ebp - 8], edx
0x482F21: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x482F26: mov      eax, dword ptr [eax]
0x482F28: mov      eax, dword ptr [eax + 0x3c0]
0x482F2E: mov      edx, 5
0x482F33: call     0x45351c
0x482F38: mov      dword ptr [ebp - 0x10], eax
0x482F3B: cmp      dword ptr [ebp - 0x10], 0
0x482F3F: je       0x482fa9
0x482F41: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x482F46: mov      eax, dword ptr [eax]
0x482F48: mov      edx, dword ptr [eax + 0x3c0]
0x482F4E: mov      ecx, 5
0x482F53: mov      eax, dword ptr [ebp - 4]
0x482F56: mov      eax, dword ptr [eax + 0x480]
0x482F5C: call     0x4579f0
0x482F61: mov      eax, dword ptr [ebp - 0x10]
0x482F64: call     0x44dc60
0x482F69: mov      edx, 0x320
0x482F6E: sub      edx, eax
0x482F70: sar      edx, 1
0x482F72: jns      0x482f77
0x482F74: adc      edx, 0
0x482F77: mov      eax, dword ptr [ebp - 4]
0x482F7A: mov      eax, dword ptr [eax + 0x480]
0x482F80: call     0x42718c
0x482F85: mov      eax, dword ptr [ebp - 0x10]
0x482F88: call     0x44d944
0x482F8D: mov      edx, 0x258
0x482F92: sub      edx, eax
0x482F94: sar      edx, 1
0x482F96: jns      0x482f9b
0x482F98: adc      edx, 0
0x482F9B: mov      eax, dword ptr [ebp - 4]
0x482F9E: mov      eax, dword ptr [eax + 0x480]
0x482FA4: call     0x4271b0
0x482FA9: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x482FAE: mov      eax, dword ptr [eax]
0x482FB0: mov      edx, dword ptr [eax + 0x308]
0x482FB6: mov      ecx, 0x40
0x482FBB: mov      eax, dword ptr [ebp - 4]
0x482FBE: mov      eax, dword ptr [eax + 0x484]
0x482FC4: call     0x4579f0
0x482FC9: mov      edx, 0x20f
0x482FCE: mov      eax, dword ptr [ebp - 4]
0x482FD1: mov      eax, dword ptr [eax + 0x484]
0x482FD7: call     0x42718c
0x482FDC: mov      edx, 0x23
0x482FE1: mov      eax, dword ptr [ebp - 4]
0x482FE4: mov      eax, dword ptr [eax + 0x484]
0x482FEA: call     0x4271b0
0x482FEF: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x482FF4: mov      eax, dword ptr [eax]
0x482FF6: mov      edx, dword ptr [eax + 0x3c0]
0x482FFC: mov      ecx, 2
0x483001: mov      eax, dword ptr [ebp - 4]
0x483004: mov      eax, dword ptr [eax + 0x488]
0x48300A: call     0x4579f0
0x48300F: mov      edx, 0x19
0x483014: mov      eax, dword ptr [ebp - 4]
0x483017: mov      eax, dword ptr [eax + 0x488]
0x48301D: call     0x42718c
0x483022: xor      edx, edx
0x483024: add      edx, dword ptr [ebp - 8]
0x483027: mov      eax, dword ptr [ebp - 4]
0x48302A: mov      eax, dword ptr [eax + 0x488]
0x483030: call     0x4271b0
0x483035: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48303A: mov      eax, dword ptr [eax]
0x48303C: mov      edx, dword ptr [eax + 0x3c0]
0x483042: mov      ecx, 2
0x483047: mov      eax, dword ptr [ebp - 4]
0x48304A: mov      eax, dword ptr [eax + 0x48c]
0x483050: call     0x4579f0
0x483055: mov      edx, 0x19
0x48305A: mov      eax, dword ptr [ebp - 4]
0x48305D: mov      eax, dword ptr [eax + 0x48c]
0x483063: call     0x42718c
0x483068: mov      edx, dword ptr [ebp - 0xc]
0x48306B: add      edx, dword ptr [ebp - 8]
0x48306E: mov      eax, dword ptr [ebp - 4]
0x483071: mov      eax, dword ptr [eax + 0x48c]
0x483077: call     0x4271b0
0x48307C: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x483081: mov      eax, dword ptr [eax]
0x483083: mov      edx, dword ptr [eax + 0x3c0]
0x483089: mov      ecx, 2
0x48308E: mov      eax, dword ptr [ebp - 4]
0x483091: mov      eax, dword ptr [eax + 0x4bc]
0x483097: call     0x4579f0
0x48309C: mov      edx, 0x19
0x4830A1: mov      eax, dword ptr [ebp - 4]
0x4830A4: mov      eax, dword ptr [eax + 0x4bc]
0x4830AA: call     0x42718c
0x4830AF: mov      edx, dword ptr [ebp - 0xc]
0x4830B2: add      edx, edx
0x4830B4: add      edx, dword ptr [ebp - 8]
0x4830B7: mov      eax, dword ptr [ebp - 4]
0x4830BA: mov      eax, dword ptr [eax + 0x4bc]
0x4830C0: call     0x4271b0
0x4830C5: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4830CA: mov      eax, dword ptr [eax]
0x4830CC: mov      edx, dword ptr [eax + 0x3c0]
0x4830D2: mov      ecx, 2
0x4830D7: mov      eax, dword ptr [ebp - 4]
0x4830DA: mov      eax, dword ptr [eax + 0x4c0]
0x4830E0: call     0x4579f0
0x4830E5: mov      edx, 0x19
0x4830EA: mov      eax, dword ptr [ebp - 4]
0x4830ED: mov      eax, dword ptr [eax + 0x4c0]
0x4830F3: call     0x42718c
0x4830F8: mov      edx, dword ptr [ebp - 0xc]
0x4830FB: lea      edx, [edx + edx*2]
0x4830FE: add      edx, dword ptr [ebp - 8]
0x483101: mov      eax, dword ptr [ebp - 4]
0x483104: mov      eax, dword ptr [eax + 0x4c0]
0x48310A: call     0x4271b0
0x48310F: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x483114: mov      eax, dword ptr [eax]
0x483116: mov      edx, dword ptr [eax + 0x3c0]
0x48311C: mov      ecx, 2
0x483121: mov      eax, dword ptr [ebp - 4]
0x483124: mov      eax, dword ptr [eax + 0x50c]
0x48312A: call     0x4579f0
0x48312F: mov      edx, 0x19
0x483134: mov      eax, dword ptr [ebp - 4]
0x483137: mov      eax, dword ptr [eax + 0x50c]
0x48313D: call     0x42718c
0x483142: mov      edx, dword ptr [ebp - 0xc]
0x483145: shl      edx, 2
0x483148: add      edx, dword ptr [ebp - 8]
0x48314B: mov      eax, dword ptr [ebp - 4]
0x48314E: mov      eax, dword ptr [eax + 0x50c]
0x483154: call     0x4271b0
0x483159: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48315E: mov      eax, dword ptr [eax]
0x483160: mov      edx, dword ptr [eax + 0x3c0]
0x483166: mov      ecx, 2
0x48316B: mov      eax, dword ptr [ebp - 4]
0x48316E: mov      eax, dword ptr [eax + 0x510]
0x483174: call     0x4579f0
0x483179: mov      edx, 0x19
0x48317E: mov      eax, dword ptr [ebp - 4]
0x483181: mov      eax, dword ptr [eax + 0x510]
0x483187: call     0x42718c
0x48318C: mov      edx, dword ptr [ebp - 0xc]
0x48318F: lea      edx, [edx + edx*4]
0x483192: add      edx, dword ptr [ebp - 8]
0x483195: mov      eax, dword ptr [ebp - 4]
0x483198: mov      eax, dword ptr [eax + 0x510]
0x48319E: call     0x4271b0
0x4831A3: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4831A8: mov      eax, dword ptr [eax]
0x4831AA: mov      edx, dword ptr [eax + 0x3c0]
0x4831B0: mov      ecx, 2
0x4831B5: mov      eax, dword ptr [ebp - 4]
0x4831B8: mov      eax, dword ptr [eax + 0x588]
0x4831BE: call     0x4579f0
0x4831C3: mov      edx, 0x19
0x4831C8: mov      eax, dword ptr [ebp - 4]
0x4831CB: mov      eax, dword ptr [eax + 0x588]
0x4831D1: call     0x42718c
0x4831D6: mov      edx, dword ptr [ebp - 0xc]
0x4831D9: add      edx, edx
0x4831DB: lea      edx, [edx + edx*2]
0x4831DE: add      edx, dword ptr [ebp - 8]
0x4831E1: mov      eax, dword ptr [ebp - 4]
0x4831E4: mov      eax, dword ptr [eax + 0x588]
0x4831EA: call     0x4271b0
0x4831EF: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4831F4: mov      eax, dword ptr [eax]
0x4831F6: mov      edx, dword ptr [eax + 0x3c0]
0x4831FC: mov      ecx, 2
0x483201: mov      eax, dword ptr [ebp - 4]
0x483204: mov      eax, dword ptr [eax + 0x584]
0x48320A: call     0x4579f0
0x48320F: mov      edx, 0x19
0x483214: mov      eax, dword ptr [ebp - 4]
0x483217: mov      eax, dword ptr [eax + 0x584]
0x48321D: call     0x42718c
0x483222: mov      edx, dword ptr [ebp - 0xc]
0x483225: mov      eax, edx
0x483227: shl      edx, 3
0x48322A: sub      edx, eax
0x48322C: add      edx, dword ptr [ebp - 8]
0x48322F: mov      eax, dword ptr [ebp - 4]
0x483232: mov      eax, dword ptr [eax + 0x584]
0x483238: call     0x4271b0
0x48323D: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x483242: mov      eax, dword ptr [eax]
0x483244: mov      edx, dword ptr [eax + 0x3c0]
0x48324A: mov      ecx, 2
0x48324F: mov      eax, dword ptr [ebp - 4]
0x483252: mov      eax, dword ptr [eax + 0x58c]
0x483258: call     0x4579f0
0x48325D: mov      edx, 0xc3
0x483262: mov      eax, dword ptr [ebp - 4]
0x483265: mov      eax, dword ptr [eax + 0x58c]
0x48326B: call     0x42718c
0x483270: xor      edx, edx
0x483272: add      edx, dword ptr [ebp - 8]
0x483275: mov      eax, dword ptr [ebp - 4]
0x483278: mov      eax, dword ptr [eax + 0x58c]
0x48327E: call     0x4271b0
0x483283: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x483288: mov      eax, dword ptr [eax]
0x48328A: mov      edx, dword ptr [eax + 0x3c0]
0x483290: mov      ecx, 2
0x483295: mov      eax, dword ptr [ebp - 4]
0x483298: mov      eax, dword ptr [eax + 0x590]
0x48329E: call     0x4579f0
0x4832A3: mov      edx, 0xc3
0x4832A8: mov      eax, dword ptr [ebp - 4]
0x4832AB: mov      eax, dword ptr [eax + 0x590]
0x4832B1: call     0x42718c
0x4832B6: mov      edx, dword ptr [ebp - 0xc]
0x4832B9: add      edx, dword ptr [ebp - 8]
0x4832BC: mov      eax, dword ptr [ebp - 4]
0x4832BF: mov      eax, dword ptr [eax + 0x590]
0x4832C5: call     0x4271b0
0x4832CA: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4832CF: mov      eax, dword ptr [eax]
0x4832D1: mov      edx, dword ptr [eax + 0x3c0]
0x4832D7: mov      ecx, 2
0x4832DC: mov      eax, dword ptr [ebp - 4]
0x4832DF: mov      eax, dword ptr [eax + 0x594]
0x4832E5: call     0x4579f0
0x4832EA: mov      edx, 0xc3
0x4832EF: mov      eax, dword ptr [ebp - 4]
0x4832F2: mov      eax, dword ptr [eax + 0x594]
0x4832F8: call     0x42718c
0x4832FD: mov      edx, dword ptr [ebp - 0xc]
0x483300: add      edx, edx
0x483302: add      edx, dword ptr [ebp - 8]
0x483305: mov      eax, dword ptr [ebp - 4]
0x483308: mov      eax, dword ptr [eax + 0x594]
0x48330E: call     0x4271b0
0x483313: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x483318: mov      eax, dword ptr [eax]
0x48331A: mov      edx, dword ptr [eax + 0x3c0]
0x483320: mov      ecx, 2
0x483325: mov      eax, dword ptr [ebp - 4]
0x483328: mov      eax, dword ptr [eax + 0x598]
0x48332E: call     0x4579f0
0x483333: mov      edx, 0xc3
0x483338: mov      eax, dword ptr [ebp - 4]
0x48333B: mov      eax, dword ptr [eax + 0x598]
0x483341: call     0x42718c
0x483346: mov      edx, dword ptr [ebp - 0xc]
0x483349: lea      edx, [edx + edx*2]
0x48334C: add      edx, dword ptr [ebp - 8]
0x48334F: mov      eax, dword ptr [ebp - 4]
0x483352: mov      eax, dword ptr [eax + 0x598]
0x483358: call     0x4271b0
0x48335D: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x483362: mov      eax, dword ptr [eax]
0x483364: mov      edx, dword ptr [eax + 0x3c0]
0x48336A: mov      ecx, 2
0x48336F: mov      eax, dword ptr [ebp - 4]
0x483372: mov      eax, dword ptr [eax + 0x59c]
0x483378: call     0x4579f0
0x48337D: mov      edx, 0xc3
0x483382: mov      eax, dword ptr [ebp - 4]
0x483385: mov      eax, dword ptr [eax + 0x59c]
0x48338B: call     0x42718c
0x483390: mov      edx, dword ptr [ebp - 0xc]
0x483393: shl      edx, 2
0x483396: add      edx, dword ptr [ebp - 8]
0x483399: mov      eax, dword ptr [ebp - 4]
0x48339C: mov      eax, dword ptr [eax + 0x59c]
0x4833A2: call     0x4271b0
0x4833A7: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4833AC: mov      eax, dword ptr [eax]
0x4833AE: mov      edx, dword ptr [eax + 0x3c0]
0x4833B4: mov      ecx, 2
0x4833B9: mov      eax, dword ptr [ebp - 4]
0x4833BC: mov      eax, dword ptr [eax + 0x5a0]
0x4833C2: call     0x4579f0
0x4833C7: mov      edx, 0xc3
0x4833CC: mov      eax, dword ptr [ebp - 4]
0x4833CF: mov      eax, dword ptr [eax + 0x5a0]
0x4833D5: call     0x42718c
0x4833DA: mov      edx, dword ptr [ebp - 0xc]
0x4833DD: lea      edx, [edx + edx*4]
0x4833E0: add      edx, dword ptr [ebp - 8]
0x4833E3: mov      eax, dword ptr [ebp - 4]
0x4833E6: mov      eax, dword ptr [eax + 0x5a0]
0x4833EC: call     0x4271b0
0x4833F1: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4833F6: mov      eax, dword ptr [eax]
0x4833F8: mov      edx, dword ptr [eax + 0x3c0]
0x4833FE: mov      ecx, 2
0x483403: mov      eax, dword ptr [ebp - 4]
0x483406: mov      eax, dword ptr [eax + 0x5a4]
0x48340C: call     0x4579f0
0x483411: mov      edx, 0xc3
0x483416: mov      eax, dword ptr [ebp - 4]
0x483419: mov      eax, dword ptr [eax + 0x5a4]
0x48341F: call     0x42718c
0x483424: mov      edx, dword ptr [ebp - 0xc]
0x483427: add      edx, edx
0x483429: lea      edx, [edx + edx*2]
0x48342C: add      edx, dword ptr [ebp - 8]
0x48342F: mov      eax, dword ptr [ebp - 4]
0x483432: mov      eax, dword ptr [eax + 0x5a4]
0x483438: call     0x4271b0
0x48343D: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x483442: mov      eax, dword ptr [eax]
0x483444: mov      edx, dword ptr [eax + 0x3c0]
0x48344A: mov      ecx, 2
0x48344F: mov      eax, dword ptr [ebp - 4]
0x483452: mov      eax, dword ptr [eax + 0x5a8]
0x483458: call     0x4579f0
0x48345D: mov      edx, 0xc3
0x483462: mov      eax, dword ptr [ebp - 4]
0x483465: mov      eax, dword ptr [eax + 0x5a8]
0x48346B: call     0x42718c
0x483470: mov      edx, dword ptr [ebp - 0xc]
0x483473: mov      eax, edx
0x483475: shl      edx, 3
0x483478: sub      edx, eax
0x48347A: add      edx, dword ptr [ebp - 8]
0x48347D: mov      eax, dword ptr [ebp - 4]
0x483480: mov      eax, dword ptr [eax + 0x5a8]
0x483486: call     0x4271b0
0x48348B: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x483490: mov      eax, dword ptr [eax]
0x483492: mov      edx, dword ptr [eax + 0x3c0]
0x483498: mov      ecx, 2
0x48349D: mov      eax, dword ptr [ebp - 4]
0x4834A0: mov      eax, dword ptr [eax + 0x5ac]
0x4834A6: call     0x4579f0
0x4834AB: mov      edx, 0x16d
0x4834B0: mov      eax, dword ptr [ebp - 4]
0x4834B3: mov      eax, dword ptr [eax + 0x5ac]
0x4834B9: call     0x42718c
0x4834BE: xor      edx, edx
0x4834C0: add      edx, dword ptr [ebp - 8]
0x4834C3: mov      eax, dword ptr [ebp - 4]
0x4834C6: mov      eax, dword ptr [eax + 0x5ac]
0x4834CC: call     0x4271b0
0x4834D1: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4834D6: mov      eax, dword ptr [eax]
0x4834D8: mov      edx, dword ptr [eax + 0x3c0]
0x4834DE: mov      ecx, 2
0x4834E3: mov      eax, dword ptr [ebp - 4]
0x4834E6: mov      eax, dword ptr [eax + 0x5b0]
0x4834EC: call     0x4579f0
0x4834F1: mov      edx, 0x16d
0x4834F6: mov      eax, dword ptr [ebp - 4]
0x4834F9: mov      eax, dword ptr [eax + 0x5b0]
0x4834FF: call     0x42718c
0x483504: mov      edx, dword ptr [ebp - 0xc]
0x483507: add      edx, dword ptr [ebp - 8]
0x48350A: mov      eax, dword ptr [ebp - 4]
0x48350D: mov      eax, dword ptr [eax + 0x5b0]
0x483513: call     0x4271b0
0x483518: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48351D: mov      eax, dword ptr [eax]
0x48351F: mov      edx, dword ptr [eax + 0x3c0]
0x483525: mov      ecx, 2
0x48352A: mov      eax, dword ptr [ebp - 4]
0x48352D: mov      eax, dword ptr [eax + 0x5b4]
0x483533: call     0x4579f0
0x483538: mov      edx, 0x16d
0x48353D: mov      eax, dword ptr [ebp - 4]
0x483540: mov      eax, dword ptr [eax + 0x5b4]
0x483546: call     0x42718c
0x48354B: mov      edx, dword ptr [ebp - 0xc]
0x48354E: add      edx, edx
0x483550: add      edx, dword ptr [ebp - 8]
0x483553: mov      eax, dword ptr [ebp - 4]
0x483556: mov      eax, dword ptr [eax + 0x5b4]
0x48355C: call     0x4271b0
0x483561: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x483566: mov      eax, dword ptr [eax]
0x483568: mov      edx, dword ptr [eax + 0x3c0]
0x48356E: mov      ecx, 2
0x483573: mov      eax, dword ptr [ebp - 4]
0x483576: mov      eax, dword ptr [eax + 0x5b8]
0x48357C: call     0x4579f0
0x483581: mov      edx, 0x16d
0x483586: mov      eax, dword ptr [ebp - 4]
0x483589: mov      eax, dword ptr [eax + 0x5b8]
0x48358F: call     0x42718c
0x483594: mov      edx, dword ptr [ebp - 0xc]
0x483597: lea      edx, [edx + edx*2]
0x48359A: add      edx, dword ptr [ebp - 8]
0x48359D: mov      eax, dword ptr [ebp - 4]
0x4835A0: mov      eax, dword ptr [eax + 0x5b8]
0x4835A6: call     0x4271b0
0x4835AB: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4835B0: mov      eax, dword ptr [eax]
0x4835B2: mov      edx, dword ptr [eax + 0x3c0]
0x4835B8: mov      ecx, 2
0x4835BD: mov      eax, dword ptr [ebp - 4]
0x4835C0: mov      eax, dword ptr [eax + 0x5bc]
0x4835C6: call     0x4579f0
0x4835CB: mov      edx, 0x16d
0x4835D0: mov      eax, dword ptr [ebp - 4]
0x4835D3: mov      eax, dword ptr [eax + 0x5bc]
0x4835D9: call     0x42718c
0x4835DE: mov      edx, dword ptr [ebp - 0xc]
0x4835E1: shl      edx, 2
0x4835E4: add      edx, dword ptr [ebp - 8]
0x4835E7: mov      eax, dword ptr [ebp - 4]
0x4835EA: mov      eax, dword ptr [eax + 0x5bc]
0x4835F0: call     0x4271b0
0x4835F5: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4835FA: mov      eax, dword ptr [eax]
0x4835FC: mov      edx, dword ptr [eax + 0x3c0]
0x483602: mov      ecx, 2
0x483607: mov      eax, dword ptr [ebp - 4]
0x48360A: mov      eax, dword ptr [eax + 0x5c0]
0x483610: call     0x4579f0
0x483615: mov      edx, 0x16d
0x48361A: mov      eax, dword ptr [ebp - 4]
0x48361D: mov      eax, dword ptr [eax + 0x5c0]
0x483623: call     0x42718c
0x483628: mov      edx, dword ptr [ebp - 0xc]
0x48362B: lea      edx, [edx + edx*4]
0x48362E: add      edx, dword ptr [ebp - 8]
0x483631: mov      eax, dword ptr [ebp - 4]
0x483634: mov      eax, dword ptr [eax + 0x5c0]
0x48363A: call     0x4271b0
0x48363F: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x483644: mov      eax, dword ptr [eax]
0x483646: mov      edx, dword ptr [eax + 0x3c0]
0x48364C: mov      ecx, 2
0x483651: mov      eax, dword ptr [ebp - 4]
0x483654: mov      eax, dword ptr [eax + 0x5c4]
0x48365A: call     0x4579f0
0x48365F: mov      edx, 0x16d
0x483664: mov      eax, dword ptr [ebp - 4]
0x483667: mov      eax, dword ptr [eax + 0x5c4]
0x48366D: call     0x42718c
0x483672: mov      edx, dword ptr [ebp - 0xc]
0x483675: add      edx, edx
0x483677: lea      edx, [edx + edx*2]
0x48367A: add      edx, dword ptr [ebp - 8]
0x48367D: mov      eax, dword ptr [ebp - 4]
0x483680: mov      eax, dword ptr [eax + 0x5c4]
0x483686: call     0x4271b0
0x48368B: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x483690: mov      eax, dword ptr [eax]
0x483692: mov      edx, dword ptr [eax + 0x3c0]
0x483698: mov      ecx, 2
0x48369D: mov      eax, dword ptr [ebp - 4]
0x4836A0: mov      eax, dword ptr [eax + 0x5c8]
0x4836A6: call     0x4579f0
0x4836AB: mov      edx, 0x16d
0x4836B0: mov      eax, dword ptr [ebp - 4]
0x4836B3: mov      eax, dword ptr [eax + 0x5c8]
0x4836B9: call     0x42718c
0x4836BE: mov      edx, dword ptr [ebp - 0xc]
0x4836C1: mov      eax, edx
0x4836C3: shl      edx, 3
0x4836C6: sub      edx, eax
0x4836C8: add      edx, dword ptr [ebp - 8]
0x4836CB: mov      eax, dword ptr [ebp - 4]
0x4836CE: mov      eax, dword ptr [eax + 0x5c8]
0x4836D4: call     0x4271b0
0x4836D9: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4836DE: mov      eax, dword ptr [eax]
0x4836E0: mov      eax, dword ptr [eax + 0x308]
0x4836E6: mov      edx, 0x3f
0x4836EB: call     0x45351c
0x4836F0: mov      dword ptr [ebp - 0x10], eax
0x4836F3: cmp      dword ptr [ebp - 0x10], 0
0x4836F7: je       0x483761
0x4836F9: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4836FE: mov      eax, dword ptr [eax]
0x483700: mov      edx, dword ptr [eax + 0x308]
0x483706: mov      ecx, 0x3f
0x48370B: mov      eax, dword ptr [ebp - 4]
0x48370E: mov      eax, dword ptr [eax + 0x304]
0x483714: call     0x4579f0
0x483719: mov      eax, dword ptr [ebp - 0x10]
0x48371C: call     0x44dc60
0x483721: mov      edx, 0x320
0x483726: sub      edx, eax
0x483728: sar      edx, 1
0x48372A: jns      0x48372f
0x48372C: adc      edx, 0
0x48372F: mov      eax, dword ptr [ebp - 4]
0x483732: mov      eax, dword ptr [eax + 0x304]
0x483738: call     0x42718c
0x48373D: mov      eax, dword ptr [ebp - 0x10]
0x483740: call     0x44d944
0x483745: mov      edx, 0x258
0x48374A: sub      edx, eax
0x48374C: sar      edx, 1
0x48374E: jns      0x483753
0x483750: adc      edx, 0
0x483753: mov      eax, dword ptr [ebp - 4]
0x483756: mov      eax, dword ptr [eax + 0x304]
0x48375C: call     0x4271b0
0x483761: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x483766: mov      eax, dword ptr [eax]
0x483768: mov      edx, dword ptr [eax + 0x308]
0x48376E: mov      ecx, 0x3e
0x483773: mov      eax, dword ptr [ebp - 4]
0x483776: mov      eax, dword ptr [eax + 0x308]
0x48377C: call     0x4579f0
0x483781: mov      edx, 0x9f
0x483786: mov      eax, dword ptr [ebp - 4]
0x483789: mov      eax, dword ptr [eax + 0x308]
0x48378F: call     0x42718c
0x483794: mov      edx, 0x1a0
0x483799: mov      eax, dword ptr [ebp - 4]
0x48379C: mov      eax, dword ptr [eax + 0x308]
0x4837A2: call     0x4271b0
0x4837A7: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4837AC: mov      eax, dword ptr [eax]
0x4837AE: mov      edx, dword ptr [eax + 0x308]
0x4837B4: mov      ecx, 0x34
0x4837B9: mov      eax, dword ptr [ebp - 4]
0x4837BC: mov      eax, dword ptr [eax + 0x514]
0x4837C2: call     0x4579f0
0x4837C7: mov      edx, 0x1bf
0x4837CC: mov      eax, dword ptr [ebp - 4]
0x4837CF: mov      eax, dword ptr [eax + 0x514]
0x4837D5: call     0x42718c
0x4837DA: mov      edx, 0x1a3
0x4837DF: mov      eax, dword ptr [ebp - 4]
0x4837E2: mov      eax, dword ptr [eax + 0x514]
0x4837E8: call     0x4271b0
0x4837ED: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4837F2: mov      eax, dword ptr [eax]
0x4837F4: mov      edx, dword ptr [eax + 0x308]
0x4837FA: mov      ecx, 0x40
0x4837FF: mov      eax, dword ptr [ebp - 4]
0x483802: mov      eax, dword ptr [eax + 0x310]
0x483808: call     0x4579f0
0x48380D: mov      edx, 0x24b
0x483812: mov      eax, dword ptr [ebp - 4]
0x483815: mov      eax, dword ptr [eax + 0x310]
0x48381B: call     0x42718c
0x483820: mov      edx, 0x21
0x483825: mov      eax, dword ptr [ebp - 4]
0x483828: mov      eax, dword ptr [eax + 0x310]
0x48382E: call     0x4271b0
0x483833: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x483838: mov      eax, dword ptr [eax]
0x48383A: mov      eax, dword ptr [eax + 0x308]
0x483840: mov      edx, 0x32
0x483845: call     0x45351c
0x48384A: mov      dword ptr [ebp - 0x10], eax
0x48384D: cmp      dword ptr [ebp - 0x10], 0
0x483851: je       0x4838bb
0x483853: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x483858: mov      eax, dword ptr [eax]
0x48385A: mov      edx, dword ptr [eax + 0x308]
0x483860: mov      ecx, 0x32
0x483865: mov      eax, dword ptr [ebp - 4]
0x483868: mov      eax, dword ptr [eax + 0x3bc]
0x48386E: call     0x4579f0
0x483873: mov      eax, dword ptr [ebp - 0x10]
0x483876: call     0x44dc60
0x48387B: mov      edx, 0x320
0x483880: sub      edx, eax
0x483882: sar      edx, 1
0x483884: jns      0x483889
0x483886: adc      edx, 0
0x483889: mov      eax, dword ptr [ebp - 4]
0x48388C: mov      eax, dword ptr [eax + 0x3bc]
0x483892: call     0x42718c
0x483897: mov      eax, dword ptr [ebp - 0x10]
0x48389A: call     0x44d944
0x48389F: mov      edx, 0x258
0x4838A4: sub      edx, eax
0x4838A6: sar      edx, 1
0x4838A8: jns      0x4838ad
0x4838AA: adc      edx, 0
0x4838AD: mov      eax, dword ptr [ebp - 4]
0x4838B0: mov      eax, dword ptr [eax + 0x3bc]
0x4838B6: call     0x4271b0
0x4838BB: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4838C0: mov      eax, dword ptr [eax]
0x4838C2: mov      edx, dword ptr [eax + 0x308]
0x4838C8: mov      ecx, 0x51
0x4838CD: mov      eax, dword ptr [ebp - 4]
0x4838D0: mov      eax, dword ptr [eax + 0x3c0]
0x4838D6: call     0x4579f0
0x4838DB: mov      edx, 0xb5
0x4838E0: mov      eax, dword ptr [ebp - 4]
0x4838E3: mov      eax, dword ptr [eax + 0x3c0]
0x4838E9: call     0x42718c
0x4838EE: mov      edx, 0xfd
0x4838F3: mov      eax, dword ptr [ebp - 4]
0x4838F6: mov      eax, dword ptr [eax + 0x3c0]
0x4838FC: call     0x4271b0
0x483901: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x483906: mov      eax, dword ptr [eax]
0x483908: mov      edx, dword ptr [eax + 0x308]
0x48390E: mov      ecx, 0x34
0x483913: mov      eax, dword ptr [ebp - 4]
0x483916: mov      eax, dword ptr [eax + 0x3c4]
0x48391C: call     0x4579f0
0x483921: mov      edx, 0x114
0x483926: mov      eax, dword ptr [ebp - 4]
0x483929: mov      eax, dword ptr [eax + 0x3c4]
0x48392F: call     0x42718c
0x483934: mov      edx, 0xfc
0x483939: mov      eax, dword ptr [ebp - 4]
0x48393C: mov      eax, dword ptr [eax + 0x3c4]
0x483942: call     0x4271b0
0x483947: xor      edx, edx
0x483949: mov      eax, dword ptr [ebp - 4]
0x48394C: mov      eax, dword ptr [eax + 0x314]
0x483952: call     0x42718c
0x483957: xor      edx, edx
0x483959: mov      eax, dword ptr [ebp - 4]
0x48395C: mov      eax, dword ptr [eax + 0x314]
0x483962: call     0x4271b0
0x483967: mov      edx, 0x320
0x48396C: mov      eax, dword ptr [ebp - 4]
0x48396F: mov      eax, dword ptr [eax + 0x314]
0x483975: call     0x4271d8
0x48397A: mov      edx, 0x258
0x48397F: mov      eax, dword ptr [ebp - 4]
0x483982: mov      eax, dword ptr [eax + 0x314]
0x483988: call     0x4271fc
0x48398D: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x483992: mov      eax, dword ptr [eax]
0x483994: mov      edx, dword ptr [eax + 0x308]
0x48399A: mov      ecx, 0x42
0x48399F: mov      eax, dword ptr [ebp - 4]
0x4839A2: mov      eax, dword ptr [eax + 0x318]
0x4839A8: call     0x4579f0
0x4839AD: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4839B2: mov      eax, dword ptr [eax]
0x4839B4: mov      edx, dword ptr [eax + 0x308]
0x4839BA: mov      ecx, 0x43
0x4839BF: mov      eax, dword ptr [ebp - 4]
0x4839C2: mov      eax, dword ptr [eax + 0x31c]
0x4839C8: call     0x4579f0
0x4839CD: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4839D2: mov      eax, dword ptr [eax]
0x4839D4: mov      edx, dword ptr [eax + 0x308]
0x4839DA: mov      ecx, 0x44
0x4839DF: mov      eax, dword ptr [ebp - 4]
0x4839E2: mov      eax, dword ptr [eax + 0x320]
0x4839E8: call     0x4579f0
0x4839ED: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4839F2: mov      eax, dword ptr [eax]
0x4839F4: mov      edx, dword ptr [eax + 0x308]
0x4839FA: mov      ecx, 0x45
0x4839FF: mov      eax, dword ptr [ebp - 4]
0x483A02: mov      eax, dword ptr [eax + 0x324]
0x483A08: call     0x4579f0
0x483A0D: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x483A12: mov      eax, dword ptr [eax]
0x483A14: mov      edx, dword ptr [eax + 0x308]
0x483A1A: mov      ecx, 0x46
0x483A1F: mov      eax, dword ptr [ebp - 4]
0x483A22: mov      eax, dword ptr [eax + 0x328]
0x483A28: call     0x4579f0
0x483A2D: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x483A32: mov      eax, dword ptr [eax]
0x483A34: mov      edx, dword ptr [eax + 0x308]
0x483A3A: mov      ecx, 0x47
0x483A3F: mov      eax, dword ptr [ebp - 4]
0x483A42: mov      eax, dword ptr [eax + 0x32c]
0x483A48: call     0x4579f0
0x483A4D: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x483A52: mov      eax, dword ptr [eax]
0x483A54: mov      edx, dword ptr [eax + 0x308]
0x483A5A: mov      ecx, 0x48
0x483A5F: mov      eax, dword ptr [ebp - 4]
0x483A62: mov      eax, dword ptr [eax + 0x330]
0x483A68: call     0x4579f0
0x483A6D: mov      edx, 0x85
0x483A72: mov      eax, dword ptr [ebp - 4]
0x483A75: mov      eax, dword ptr [eax + 0x318]
0x483A7B: call     0x42718c
0x483A80: mov      edx, 0x1c5
0x483A85: mov      eax, dword ptr [ebp - 4]
0x483A88: mov      eax, dword ptr [eax + 0x318]
0x483A8E: call     0x4271b0
0x483A93: mov      edx, 0x2ad
0x483A98: mov      eax, dword ptr [ebp - 4]
0x483A9B: mov      eax, dword ptr [eax + 0x31c]
0x483AA1: call     0x42718c
0x483AA6: mov      edx, 0x1c6
0x483AAB: mov      eax, dword ptr [ebp - 4]
0x483AAE: mov      eax, dword ptr [eax + 0x31c]
0x483AB4: call     0x4271b0
0x483AB9: mov      edx, 0x181
0x483ABE: mov      eax, dword ptr [ebp - 4]
0x483AC1: mov      eax, dword ptr [eax + 0x320]
0x483AC7: call     0x42718c
0x483ACC: mov      edx, 0x1c8
0x483AD1: mov      eax, dword ptr [ebp - 4]
0x483AD4: mov      eax, dword ptr [eax + 0x320]
0x483ADA: call     0x4271b0
0x483ADF: mov      edx, 0x15c
0x483AE4: mov      eax, dword ptr [ebp - 4]
0x483AE7: mov      eax, dword ptr [eax + 0x324]
0x483AED: call     0x42718c
0x483AF2: mov      edx, 0x1e6
0x483AF7: mov      eax, dword ptr [ebp - 4]
0x483AFA: mov      eax, dword ptr [eax + 0x324]
0x483B00: call     0x4271b0
0x483B05: mov      edx, 0x15b
0x483B0A: mov      eax, dword ptr [ebp - 4]
0x483B0D: mov      eax, dword ptr [eax + 0x328]
0x483B13: call     0x42718c
0x483B18: mov      edx, 0x1fa
0x483B1D: mov      eax, dword ptr [ebp - 4]
0x483B20: mov      eax, dword ptr [eax + 0x328]
0x483B26: call     0x4271b0
0x483B2B: mov      edx, 0x16a
0x483B30: mov      eax, dword ptr [ebp - 4]
0x483B33: mov      eax, dword ptr [eax + 0x32c]
0x483B39: call     0x42718c
0x483B3E: mov      edx, 0x20f
0x483B43: mov      eax, dword ptr [ebp - 4]
0x483B46: mov      eax, dword ptr [eax + 0x32c]
0x483B4C: call     0x4271b0
0x483B51: mov      edx, 0x17b
0x483B56: mov      eax, dword ptr [ebp - 4]
0x483B59: mov      eax, dword ptr [eax + 0x330]
0x483B5F: call     0x42718c
0x483B64: mov      edx, 0x223
0x483B69: mov      eax, dword ptr [ebp - 4]
0x483B6C: mov      eax, dword ptr [eax + 0x330]
0x483B72: call     0x4271b0
0x483B77: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x483B7C: mov      eax, dword ptr [eax]
0x483B7E: mov      eax, dword ptr [eax + 0x308]
0x483B84: mov      edx, 0x49
0x483B89: call     0x45351c
0x483B8E: mov      dword ptr [ebp - 0x10], eax
0x483B91: cmp      dword ptr [ebp - 0x10], 0
0x483B95: je       0x483bff
0x483B97: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x483B9C: mov      eax, dword ptr [eax]
0x483B9E: mov      edx, dword ptr [eax + 0x308]
0x483BA4: mov      ecx, 0x49
0x483BA9: mov      eax, dword ptr [ebp - 4]
0x483BAC: mov      eax, dword ptr [eax + 0x334]
0x483BB2: call     0x4579f0
0x483BB7: mov      eax, dword ptr [ebp - 0x10]
0x483BBA: call     0x44dc60
0x483BBF: mov      edx, 0x320
0x483BC4: sub      edx, eax
0x483BC6: sar      edx, 1
0x483BC8: jns      0x483bcd
0x483BCA: adc      edx, 0
0x483BCD: mov      eax, dword ptr [ebp - 4]
0x483BD0: mov      eax, dword ptr [eax + 0x334]
0x483BD6: call     0x42718c
0x483BDB: mov      eax, dword ptr [ebp - 0x10]
0x483BDE: call     0x44d944
0x483BE3: mov      edx, 0x258
0x483BE8: sub      edx, eax
0x483BEA: sar      edx, 1
0x483BEC: jns      0x483bf1
0x483BEE: adc      edx, 0
0x483BF1: mov      eax, dword ptr [ebp - 4]
0x483BF4: mov      eax, dword ptr [eax + 0x334]
0x483BFA: call     0x4271b0
0x483BFF: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x483C04: mov      eax, dword ptr [eax]
0x483C06: mov      edx, dword ptr [eax + 0x308]
0x483C0C: mov      ecx, 0x4a
0x483C11: mov      eax, dword ptr [ebp - 4]
0x483C14: mov      eax, dword ptr [eax + 0x338]
0x483C1A: call     0x4579f0
0x483C1F: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x483C24: mov      eax, dword ptr [eax]
0x483C26: mov      edx, dword ptr [eax + 0x308]
0x483C2C: mov      ecx, 0x4b
0x483C31: mov      eax, dword ptr [ebp - 4]
0x483C34: mov      eax, dword ptr [eax + 0x33c]
0x483C3A: call     0x4579f0
0x483C3F: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x483C44: mov      eax, dword ptr [eax]
0x483C46: mov      edx, dword ptr [eax + 0x308]
0x483C4C: mov      ecx, 0x4c
0x483C51: mov      eax, dword ptr [ebp - 4]
0x483C54: mov      eax, dword ptr [eax + 0x340]
0x483C5A: call     0x4579f0
0x483C5F: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x483C64: mov      eax, dword ptr [eax]
0x483C66: mov      edx, dword ptr [eax + 0x308]
0x483C6C: mov      ecx, 0x4d
0x483C71: mov      eax, dword ptr [ebp - 4]
0x483C74: mov      eax, dword ptr [eax + 0x348]
0x483C7A: call     0x4579f0
0x483C7F: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x483C84: mov      eax, dword ptr [eax]
0x483C86: mov      edx, dword ptr [eax + 0x308]
0x483C8C: mov      ecx, 0x4e
0x483C91: mov      eax, dword ptr [ebp - 4]
0x483C94: mov      eax, dword ptr [eax + 0x34c]
0x483C9A: call     0x4579f0
0x483C9F: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x483CA4: mov      eax, dword ptr [eax]
0x483CA6: mov      edx, dword ptr [eax + 0x308]
0x483CAC: mov      ecx, 0x4f
0x483CB1: mov      eax, dword ptr [ebp - 4]
0x483CB4: mov      eax, dword ptr [eax + 0x350]
0x483CBA: call     0x4579f0
0x483CBF: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x483CC4: mov      eax, dword ptr [eax]
0x483CC6: mov      edx, dword ptr [eax + 0x308]
0x483CCC: mov      ecx, 0x50
0x483CD1: mov      eax, dword ptr [ebp - 4]
0x483CD4: mov      eax, dword ptr [eax + 0x354]
0x483CDA: call     0x4579f0
0x483CDF: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x483CE4: mov      eax, dword ptr [eax]
0x483CE6: mov      edx, dword ptr [eax + 0x308]
0x483CEC: mov      ecx, 0x3e
0x483CF1: mov      eax, dword ptr [ebp - 4]
0x483CF4: mov      eax, dword ptr [eax + 0x358]
0x483CFA: call     0x4579f0
0x483CFF: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x483D04: mov      eax, dword ptr [eax]
0x483D06: mov      edx, dword ptr [eax + 0x308]
0x483D0C: mov      ecx, 0x40
0x483D11: mov      eax, dword ptr [ebp - 4]
0x483D14: mov      eax, dword ptr [eax + 0x35c]
0x483D1A: call     0x4579f0
0x483D1F: mov      edx, 0x30
0x483D24: mov      eax, dword ptr [ebp - 4]
0x483D27: mov      eax, dword ptr [eax + 0x338]
0x483D2D: call     0x42718c
0x483D32: mov      edx, 0x9d
0x483D37: mov      eax, dword ptr [ebp - 4]
0x483D3A: mov      eax, dword ptr [eax + 0x338]
0x483D40: call     0x4271b0
0x483D45: mov      edx, 0x5d
0x483D4A: mov      eax, dword ptr [ebp - 4]
0x483D4D: mov      eax, dword ptr [eax + 0x33c]
0x483D53: call     0x42718c
0x483D58: mov      edx, 0x9d
0x483D5D: mov      eax, dword ptr [ebp - 4]
0x483D60: mov      eax, dword ptr [eax + 0x33c]
0x483D66: call     0x4271b0
0x483D6B: mov      edx, 0x8a
0x483D70: mov      eax, dword ptr [ebp - 4]
0x483D73: mov      eax, dword ptr [eax + 0x340]
0x483D79: call     0x42718c
0x483D7E: mov      edx, 0x9d
0x483D83: mov      eax, dword ptr [ebp - 4]
0x483D86: mov      eax, dword ptr [eax + 0x340]
0x483D8C: call     0x4271b0
0x483D91: mov      edx, 0x5d
0x483D96: mov      eax, dword ptr [ebp - 4]
0x483D99: mov      eax, dword ptr [eax + 0x348]
0x483D9F: call     0x42718c
0x483DA4: mov      edx, 0xe7
0x483DA9: mov      eax, dword ptr [ebp - 4]
0x483DAC: mov      eax, dword ptr [eax + 0x348]
0x483DB2: call     0x4271b0
0x483DB7: mov      edx, 0x8a
0x483DBC: mov      eax, dword ptr [ebp - 4]
0x483DBF: mov      eax, dword ptr [eax + 0x34c]
0x483DC5: call     0x42718c
0x483DCA: mov      edx, 0xe7
0x483DCF: mov      eax, dword ptr [ebp - 4]
0x483DD2: mov      eax, dword ptr [eax + 0x34c]
0x483DD8: call     0x4271b0
0x483DDD: mov      edx, 0x4c
0x483DE2: mov      eax, dword ptr [ebp - 4]
0x483DE5: mov      eax, dword ptr [eax + 0x350]
0x483DEB: call     0x42718c
0x483DF0: mov      edx, 0x134
0x483DF5: mov      eax, dword ptr [ebp - 4]
0x483DF8: mov      eax, dword ptr [eax + 0x350]
0x483DFE: call     0x4271b0
0x483E03: mov      edx, 0xaa
0x483E08: mov      eax, dword ptr [ebp - 4]
0x483E0B: mov      eax, dword ptr [eax + 0x354]
0x483E11: call     0x42718c
0x483E16: mov      edx, 0x134
0x483E1B: mov      eax, dword ptr [ebp - 4]
0x483E1E: mov      eax, dword ptr [eax + 0x354]
0x483E24: call     0x4271b0
0x483E29: mov      edx, 0xf8
0x483E2E: mov      eax, dword ptr [ebp - 4]
0x483E31: mov      eax, dword ptr [eax + 0x35c]
0x483E37: call     0x42718c
0x483E3C: mov      edx, 0x1f
0x483E41: mov      eax, dword ptr [ebp - 4]
0x483E44: mov      eax, dword ptr [eax + 0x35c]
0x483E4A: call     0x4271b0
0x483E4F: mov      edx, 0x66
0x483E54: mov      eax, dword ptr [ebp - 4]
0x483E57: mov      eax, dword ptr [eax + 0x358]
0x483E5D: call     0x42718c
0x483E62: mov      edx, 0x167
0x483E67: mov      eax, dword ptr [ebp - 4]
0x483E6A: mov      eax, dword ptr [eax + 0x358]
0x483E70: call     0x4271b0
0x483E75: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x483E7A: mov      eax, dword ptr [eax]
0x483E7C: mov      eax, dword ptr [eax + 0x308]
0x483E82: mov      edx, 0x172
0x483E87: call     0x45351c
0x483E8C: mov      dword ptr [ebp - 0x10], eax
0x483E8F: cmp      dword ptr [ebp - 0x10], 0
0x483E93: je       0x483ee2
0x483E95: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x483E9A: mov      eax, dword ptr [eax]
0x483E9C: mov      edx, dword ptr [eax + 0x308]
0x483EA2: mov      ecx, 0x172
0x483EA7: mov      eax, dword ptr [ebp - 4]
0x483EAA: mov      eax, dword ptr [eax + 0x2c4]
0x483EB0: call     0x4579f0
0x483EB5: mov      eax, dword ptr [ebp - 0x10]
0x483EB8: call     0x44dc60
0x483EBD: mov      edx, 0x320
0x483EC2: sub      edx, eax
0x483EC4: mov      eax, dword ptr [ebp - 4]
0x483EC7: mov      eax, dword ptr [eax + 0x2c4]
0x483ECD: call     0x42718c
0x483ED2: xor      edx, edx
0x483ED4: mov      eax, dword ptr [ebp - 4]
0x483ED7: mov      eax, dword ptr [eax + 0x2c4]
0x483EDD: call     0x4271b0
0x483EE2: mov      edx, 0xa8
0x483EE7: mov      eax, dword ptr [ebp - 4]
0x483EEA: mov      eax, dword ptr [eax + 0x380]
0x483EF0: call     0x42718c
0x483EF5: mov      edx, 0x57
0x483EFA: mov      eax, dword ptr [ebp - 4]
0x483EFD: mov      eax, dword ptr [eax + 0x380]
0x483F03: call     0x4271b0
0x483F08: mov      edx, 0x22
0x483F0D: mov      eax, dword ptr [ebp - 4]
0x483F10: mov      eax, dword ptr [eax + 0x380]
0x483F16: call     0x4271d8
0x483F1B: mov      edx, 0x1f
0x483F20: mov      eax, dword ptr [ebp - 4]
0x483F23: mov      eax, dword ptr [eax + 0x380]
0x483F29: call     0x4271fc
0x483F2E: mov      edx, 0x73
0x483F33: mov      eax, dword ptr [ebp - 4]
0x483F36: mov      eax, dword ptr [eax + 0x3a0]
0x483F3C: call     0x42718c
0x483F41: mov      edx, 0x5d
0x483F46: mov      eax, dword ptr [ebp - 4]
0x483F49: mov      eax, dword ptr [eax + 0x3a0]
0x483F4F: call     0x4271b0
0x483F54: mov      edx, 0x12
0x483F59: mov      eax, dword ptr [ebp - 4]
0x483F5C: mov      eax, dword ptr [eax + 0x3a0]
0x483F62: call     0x4271d8
0x483F67: mov      edx, 0x12
0x483F6C: mov      eax, dword ptr [ebp - 4]
0x483F6F: mov      eax, dword ptr [eax + 0x3a0]
0x483F75: call     0x4271fc
0x483F7A: mov      edx, 0xa8
0x483F7F: mov      eax, dword ptr [ebp - 4]
0x483F82: mov      eax, dword ptr [eax + 0x384]
0x483F88: call     0x42718c
0x483F8D: mov      edx, 0x7d
0x483F92: mov      eax, dword ptr [ebp - 4]
0x483F95: mov      eax, dword ptr [eax + 0x384]
0x483F9B: call     0x4271b0
0x483FA0: mov      edx, 0x22
0x483FA5: mov      eax, dword ptr [ebp - 4]
0x483FA8: mov      eax, dword ptr [eax + 0x384]
0x483FAE: call     0x4271d8
0x483FB3: mov      edx, 0x1f
0x483FB8: mov      eax, dword ptr [ebp - 4]
0x483FBB: mov      eax, dword ptr [eax + 0x384]
0x483FC1: call     0x4271fc
0x483FC6: mov      edx, 0x2a
0x483FCB: mov      eax, dword ptr [ebp - 4]
0x483FCE: mov      eax, dword ptr [eax + 0x388]
0x483FD4: call     0x42718c
0x483FD9: mov      edx, 0xb0
0x483FDE: mov      eax, dword ptr [ebp - 4]
0x483FE1: mov      eax, dword ptr [eax + 0x388]
0x483FE7: call     0x4271b0
0x483FEC: mov      edx, 0x22
0x483FF1: mov      eax, dword ptr [ebp - 4]
0x483FF4: mov      eax, dword ptr [eax + 0x388]
0x483FFA: call     0x4271d8
0x483FFF: mov      edx, 0x1f
0x484004: mov      eax, dword ptr [ebp - 4]
0x484007: mov      eax, dword ptr [eax + 0x388]
0x48400D: call     0x4271fc
0x484012: mov      edx, 0xa8
0x484017: mov      eax, dword ptr [ebp - 4]
0x48401A: mov      eax, dword ptr [eax + 0x38c]
0x484020: call     0x42718c
0x484025: mov      edx, 0xb0
0x48402A: mov      eax, dword ptr [ebp - 4]
0x48402D: mov      eax, dword ptr [eax + 0x38c]
0x484033: call     0x4271b0
0x484038: mov      edx, 0x22
0x48403D: mov      eax, dword ptr [ebp - 4]
0x484040: mov      eax, dword ptr [eax + 0x38c]
0x484046: call     0x4271d8
0x48404B: mov      edx, 0x1f
0x484050: mov      eax, dword ptr [ebp - 4]
0x484053: mov      eax, dword ptr [eax + 0x38c]
0x484059: call     0x4271fc
0x48405E: mov      edx, 0x2a
0x484063: mov      eax, dword ptr [ebp - 4]
0x484066: mov      eax, dword ptr [eax + 0x390]
0x48406C: call     0x42718c
0x484071: mov      edx, 0xd7
0x484076: mov      eax, dword ptr [ebp - 4]
0x484079: mov      eax, dword ptr [eax + 0x390]
0x48407F: call     0x4271b0
0x484084: mov      edx, 0x22
0x484089: mov      eax, dword ptr [ebp - 4]
0x48408C: mov      eax, dword ptr [eax + 0x390]
0x484092: call     0x4271d8
0x484097: mov      edx, 0x1f
0x48409C: mov      eax, dword ptr [ebp - 4]
0x48409F: mov      eax, dword ptr [eax + 0x390]
0x4840A5: call     0x4271fc
0x4840AA: mov      edx, 0xa8
0x4840AF: mov      eax, dword ptr [ebp - 4]
0x4840B2: mov      eax, dword ptr [eax + 0x394]
0x4840B8: call     0x42718c
0x4840BD: mov      edx, 0xd7
0x4840C2: mov      eax, dword ptr [ebp - 4]
0x4840C5: mov      eax, dword ptr [eax + 0x394]
0x4840CB: call     0x4271b0
0x4840D0: mov      edx, 0x22
0x4840D5: mov      eax, dword ptr [ebp - 4]
0x4840D8: mov      eax, dword ptr [eax + 0x394]
0x4840DE: call     0x4271d8
0x4840E3: mov      edx, 0x1f
0x4840E8: mov      eax, dword ptr [ebp - 4]
0x4840EB: mov      eax, dword ptr [eax + 0x394]
0x4840F1: call     0x4271fc
0x4840F6: mov      edx, 0x2f
0x4840FB: mov      eax, dword ptr [ebp - 4]
0x4840FE: mov      eax, dword ptr [eax + 0x398]
0x484104: call     0x42718c
0x484109: mov      edx, 0x50
0x48410E: mov      eax, dword ptr [ebp - 4]
0x484111: mov      eax, dword ptr [eax + 0x398]
0x484117: call     0x4271b0
0x48411C: mov      edx, 0x2f
0x484121: mov      eax, dword ptr [ebp - 4]
0x484124: mov      eax, dword ptr [eax + 0x398]
0x48412A: call     0x4271d8
0x48412F: mov      edx, 0x57
0x484134: mov      eax, dword ptr [ebp - 4]
0x484137: mov      eax, dword ptr [eax + 0x398]
0x48413D: call     0x4271fc
0x484142: mov      edx, 0x60
0x484147: mov      eax, dword ptr [ebp - 4]
0x48414A: mov      eax, dword ptr [eax + 0x39c]
0x484150: call     0x42718c
0x484155: mov      edx, 0x7a
0x48415A: mov      eax, dword ptr [ebp - 4]
0x48415D: mov      eax, dword ptr [eax + 0x39c]
0x484163: call     0x4271b0
0x484168: mov      edx, 0x35
0x48416D: mov      eax, dword ptr [ebp - 4]
0x484170: mov      eax, dword ptr [eax + 0x39c]
0x484176: call     0x4271d8
0x48417B: mov      edx, 0x70
0x484180: mov      eax, dword ptr [ebp - 4]
0x484183: mov      eax, dword ptr [eax + 0x39c]
0x484189: call     0x4271fc
0x48418E: mov      edx, 0x2e
0x484193: mov      eax, dword ptr [ebp - 4]
0x484196: mov      eax, dword ptr [eax + 0x3f4]
0x48419C: call     0x42718c
0x4841A1: mov      edx, 0x3b
0x4841A6: mov      eax, dword ptr [ebp - 4]
0x4841A9: mov      eax, dword ptr [eax + 0x3f4]
0x4841AF: call     0x4271b0
0x4841B4: mov      edx, 0x1f
0x4841B9: mov      eax, dword ptr [ebp - 4]
0x4841BC: mov      eax, dword ptr [eax + 0x3f4]
0x4841C2: call     0x4271d8
0x4841C7: mov      edx, 0x21
0x4841CC: mov      eax, dword ptr [ebp - 4]
0x4841CF: mov      eax, dword ptr [eax + 0x3f4]
0x4841D5: call     0x4271fc
0x4841DA: mov      edx, 0x2e
0x4841DF: mov      eax, dword ptr [ebp - 4]
0x4841E2: mov      eax, dword ptr [eax + 0x3f8]
0x4841E8: call     0x42718c
0x4841ED: mov      edx, 0x60
0x4841F2: mov      eax, dword ptr [ebp - 4]
0x4841F5: mov      eax, dword ptr [eax + 0x3f8]
0x4841FB: call     0x4271b0
0x484200: mov      edx, 0x1f
0x484205: mov      eax, dword ptr [ebp - 4]
0x484208: mov      eax, dword ptr [eax + 0x3f8]
0x48420E: call     0x4271d8
0x484213: mov      edx, 0x21
0x484218: mov      eax, dword ptr [ebp - 4]
0x48421B: mov      eax, dword ptr [eax + 0x3f8]
0x484221: call     0x4271fc
0x484226: mov      edx, 0x2e
0x48422B: mov      eax, dword ptr [ebp - 4]
0x48422E: mov      eax, dword ptr [eax + 0x3fc]
0x484234: call     0x42718c
0x484239: mov      edx, 0x86
0x48423E: mov      eax, dword ptr [ebp - 4]
0x484241: mov      eax, dword ptr [eax + 0x3fc]
0x484247: call     0x4271b0
0x48424C: mov      edx, 0x1f
0x484251: mov      eax, dword ptr [ebp - 4]
0x484254: mov      eax, dword ptr [eax + 0x3fc]
0x48425A: call     0x4271d8
0x48425F: mov      edx, 0x21
0x484264: mov      eax, dword ptr [ebp - 4]
0x484267: mov      eax, dword ptr [eax + 0x3fc]
0x48426D: call     0x4271fc
0x484272: mov      edx, 0x2e
0x484277: mov      eax, dword ptr [ebp - 4]
0x48427A: mov      eax, dword ptr [eax + 0x400]
0x484280: call     0x42718c
0x484285: mov      edx, 0xab
0x48428A: mov      eax, dword ptr [ebp - 4]
0x48428D: mov      eax, dword ptr [eax + 0x400]
0x484293: call     0x4271b0
0x484298: mov      edx, 0x1f
0x48429D: mov      eax, dword ptr [ebp - 4]
0x4842A0: mov      eax, dword ptr [eax + 0x400]
0x4842A6: call     0x4271d8
0x4842AB: mov      edx, 0x21
0x4842B0: mov      eax, dword ptr [ebp - 4]
0x4842B3: mov      eax, dword ptr [eax + 0x400]
0x4842B9: call     0x4271fc
0x4842BE: mov      edx, 0x2e
0x4842C3: mov      eax, dword ptr [ebp - 4]
0x4842C6: mov      eax, dword ptr [eax + 0x404]
0x4842CC: call     0x42718c
0x4842D1: mov      edx, 0xd0
0x4842D6: mov      eax, dword ptr [ebp - 4]
0x4842D9: mov      eax, dword ptr [eax + 0x404]
0x4842DF: call     0x4271b0
0x4842E4: mov      edx, 0x1f
0x4842E9: mov      eax, dword ptr [ebp - 4]
0x4842EC: mov      eax, dword ptr [eax + 0x404]
0x4842F2: call     0x4271d8
0x4842F7: mov      edx, 0x21
0x4842FC: mov      eax, dword ptr [ebp - 4]
0x4842FF: mov      eax, dword ptr [eax + 0x404]
0x484305: call     0x4271fc
0x48430A: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48430F: mov      eax, dword ptr [eax]
0x484311: mov      edx, dword ptr [eax + 0x308]
0x484317: mov      ecx, 0x18e
0x48431C: mov      eax, dword ptr [ebp - 4]
0x48431F: mov      eax, dword ptr [eax + 0x458]
0x484325: call     0x4579f0
0x48432A: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48432F: mov      eax, dword ptr [eax]
0x484331: mov      edx, dword ptr [eax + 0x308]
0x484337: mov      ecx, 0x18c
0x48433C: mov      eax, dword ptr [ebp - 4]
0x48433F: mov      eax, dword ptr [eax + 0x45c]
0x484345: call     0x4579f0
0x48434A: mov      edx, 0xd5
0x48434F: mov      eax, dword ptr [ebp - 4]
0x484352: mov      eax, dword ptr [eax + 0x458]
0x484358: call     0x42718c
0x48435D: mov      edx, 0x71
0x484362: mov      eax, dword ptr [ebp - 4]
0x484365: mov      eax, dword ptr [eax + 0x458]
0x48436B: call     0x4271b0
0x484370: mov      edx, 0xd5
0x484375: mov      eax, dword ptr [ebp - 4]
0x484378: mov      eax, dword ptr [eax + 0x45c]
0x48437E: call     0x42718c
0x484383: mov      edx, 0x8f
0x484388: mov      eax, dword ptr [ebp - 4]
0x48438B: mov      eax, dword ptr [eax + 0x45c]
0x484391: call     0x4271b0
0x484396: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48439B: mov      eax, dword ptr [eax]
0x48439D: mov      edx, dword ptr [eax + 0x308]
0x4843A3: mov      ecx, 0x173
0x4843A8: mov      eax, dword ptr [ebp - 4]
0x4843AB: mov      eax, dword ptr [eax + 0x2f4]
0x4843B1: call     0x4579f0
0x4843B6: mov      edx, 8
0x4843BB: mov      eax, dword ptr [ebp - 4]
0x4843BE: mov      eax, dword ptr [eax + 0x2f4]
0x4843C4: call     0x42718c
0x4843C9: mov      edx, 0x27
0x4843CE: mov      eax, dword ptr [ebp - 4]
0x4843D1: mov      eax, dword ptr [eax + 0x2f4]
0x4843D7: call     0x4271b0
0x4843DC: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4843E1: mov      eax, dword ptr [eax]
0x4843E3: mov      edx, dword ptr [eax + 0x308]
0x4843E9: mov      ecx, 0x175
0x4843EE: mov      eax, dword ptr [ebp - 4]
0x4843F1: mov      eax, dword ptr [eax + 0x2e8]
0x4843F7: call     0x4579f0
0x4843FC: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x484401: mov      eax, dword ptr [eax]
0x484403: mov      edx, dword ptr [eax + 0x308]
0x484409: mov      ecx, 0x174
0x48440E: mov      eax, dword ptr [ebp - 4]
0x484411: mov      eax, dword ptr [eax + 0x37c]
0x484417: call     0x4579f0
0x48441C: mov      edx, 7
0x484421: mov      eax, dword ptr [ebp - 4]
0x484424: mov      eax, dword ptr [eax + 0x2e8]
0x48442A: call     0x42718c
0x48442F: mov      edx, 0x80
0x484434: mov      eax, dword ptr [ebp - 4]
0x484437: mov      eax, dword ptr [eax + 0x2e8]
0x48443D: call     0x4271b0
0x484442: mov      edx, 7
0x484447: mov      eax, dword ptr [ebp - 4]
0x48444A: mov      eax, dword ptr [eax + 0x37c]
0x484450: call     0x42718c
0x484455: mov      edx, 0xbb
0x48445A: mov      eax, dword ptr [ebp - 4]
0x48445D: mov      eax, dword ptr [eax + 0x37c]
0x484463: call     0x4271b0
0x484468: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48446D: mov      eax, dword ptr [eax]
0x48446F: mov      eax, dword ptr [eax + 0x308]
0x484475: mov      edx, 0x172
0x48447A: call     0x45351c
0x48447F: mov      dword ptr [ebp - 0x10], eax
0x484482: cmp      dword ptr [ebp - 0x10], 0
0x484486: je       0x4844e1
0x484488: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48448D: mov      eax, dword ptr [eax]
0x48448F: mov      edx, dword ptr [eax + 0x308]
0x484495: mov      ecx, 0x172
0x48449A: mov      eax, dword ptr [ebp - 4]
0x48449D: mov      eax, dword ptr [eax + 0x490]
0x4844A3: call     0x4579f0
0x4844A8: mov      eax, dword ptr [ebp - 0x10]
0x4844AB: call     0x44dc60
0x4844B0: mov      edx, 0x320
0x4844B5: sub      edx, eax
0x4844B7: push     edx
0x4844B8: mov      eax, dword ptr [ebp - 0x10]
0x4844BB: call     0x44dc60
0x4844C0: pop      edx
0x4844C1: sub      edx, eax
0x4844C3: mov      eax, dword ptr [ebp - 4]
0x4844C6: mov      eax, dword ptr [eax + 0x490]
0x4844CC: call     0x42718c
0x4844D1: xor      edx, edx
0x4844D3: mov      eax, dword ptr [ebp - 4]
0x4844D6: mov      eax, dword ptr [eax + 0x490]
0x4844DC: call     0x4271b0
0x4844E1: mov      edx, 0xa8
0x4844E6: mov      eax, dword ptr [ebp - 4]
0x4844E9: mov      eax, dword ptr [eax + 0x4a0]
0x4844EF: call     0x42718c
0x4844F4: mov      edx, 0x57
0x4844F9: mov      eax, dword ptr [ebp - 4]
0x4844FC: mov      eax, dword ptr [eax + 0x4a0]
0x484502: call     0x4271b0
0x484507: mov      edx, 0x22
0x48450C: mov      eax, dword ptr [ebp - 4]
0x48450F: mov      eax, dword ptr [eax + 0x4a0]
0x484515: call     0x4271d8
0x48451A: mov      edx, 0x1f
0x48451F: mov      eax, dword ptr [ebp - 4]
0x484522: mov      eax, dword ptr [eax + 0x4a0]
0x484528: call     0x4271fc
0x48452D: mov      edx, 0x73
0x484532: mov      eax, dword ptr [ebp - 4]
0x484535: mov      eax, dword ptr [eax + 0x49c]
0x48453B: call     0x42718c
0x484540: mov      edx, 0x5d
0x484545: mov      eax, dword ptr [ebp - 4]
0x484548: mov      eax, dword ptr [eax + 0x49c]
0x48454E: call     0x4271b0
0x484553: mov      edx, 0x12
0x484558: mov      eax, dword ptr [ebp - 4]
0x48455B: mov      eax, dword ptr [eax + 0x49c]
0x484561: call     0x4271d8
0x484566: mov      edx, 0x12
0x48456B: mov      eax, dword ptr [ebp - 4]
0x48456E: mov      eax, dword ptr [eax + 0x49c]
0x484574: call     0x4271fc
0x484579: mov      edx, 0xa8
0x48457E: mov      eax, dword ptr [ebp - 4]
0x484581: mov      eax, dword ptr [eax + 0x4a8]
0x484587: call     0x42718c
0x48458C: mov      edx, 0x7d
0x484591: mov      eax, dword ptr [ebp - 4]
0x484594: mov      eax, dword ptr [eax + 0x4a8]
0x48459A: call     0x4271b0
0x48459F: mov      edx, 0x22
0x4845A4: mov      eax, dword ptr [ebp - 4]
0x4845A7: mov      eax, dword ptr [eax + 0x4a8]
0x4845AD: call     0x4271d8
0x4845B2: mov      edx, 0x1f
0x4845B7: mov      eax, dword ptr [ebp - 4]
0x4845BA: mov      eax, dword ptr [eax + 0x4a8]
0x4845C0: call     0x4271fc
0x4845C5: mov      edx, 0x2a
0x4845CA: mov      eax, dword ptr [ebp - 4]
0x4845CD: mov      eax, dword ptr [eax + 0x4ac]
0x4845D3: call     0x42718c
0x4845D8: mov      edx, 0xb0
0x4845DD: mov      eax, dword ptr [ebp - 4]
0x4845E0: mov      eax, dword ptr [eax + 0x4ac]
0x4845E6: call     0x4271b0
0x4845EB: mov      edx, 0x22
0x4845F0: mov      eax, dword ptr [ebp - 4]
0x4845F3: mov      eax, dword ptr [eax + 0x4ac]
0x4845F9: call     0x4271d8
0x4845FE: mov      edx, 0x1f
0x484603: mov      eax, dword ptr [ebp - 4]
0x484606: mov      eax, dword ptr [eax + 0x4ac]
0x48460C: call     0x4271fc
0x484611: mov      edx, 0xa8
0x484616: mov      eax, dword ptr [ebp - 4]
0x484619: mov      eax, dword ptr [eax + 0x4b4]
0x48461F: call     0x42718c
0x484624: mov      edx, 0xb0
0x484629: mov      eax, dword ptr [ebp - 4]
0x48462C: mov      eax, dword ptr [eax + 0x4b4]
0x484632: call     0x4271b0
0x484637: mov      edx, 0x22
0x48463C: mov      eax, dword ptr [ebp - 4]
0x48463F: mov      eax, dword ptr [eax + 0x4b4]
0x484645: call     0x4271d8
0x48464A: mov      edx, 0x1f
0x48464F: mov      eax, dword ptr [ebp - 4]
0x484652: mov      eax, dword ptr [eax + 0x4b4]
0x484658: call     0x4271fc
0x48465D: mov      edx, 0x2a
0x484662: mov      eax, dword ptr [ebp - 4]
0x484665: mov      eax, dword ptr [eax + 0x4b0]
0x48466B: call     0x42718c
0x484670: mov      edx, 0xd7
0x484675: mov      eax, dword ptr [ebp - 4]
0x484678: mov      eax, dword ptr [eax + 0x4b0]
0x48467E: call     0x4271b0
0x484683: mov      edx, 0x22
0x484688: mov      eax, dword ptr [ebp - 4]
0x48468B: mov      eax, dword ptr [eax + 0x4b0]
0x484691: call     0x4271d8
0x484696: mov      edx, 0x1f
0x48469B: mov      eax, dword ptr [ebp - 4]
0x48469E: mov      eax, dword ptr [eax + 0x4b0]
0x4846A4: call     0x4271fc
0x4846A9: mov      edx, 0xa8
0x4846AE: mov      eax, dword ptr [ebp - 4]
0x4846B1: mov      eax, dword ptr [eax + 0x4b8]
0x4846B7: call     0x42718c
0x4846BC: mov      edx, 0xd7
0x4846C1: mov      eax, dword ptr [ebp - 4]
0x4846C4: mov      eax, dword ptr [eax + 0x4b8]
0x4846CA: call     0x4271b0
0x4846CF: mov      edx, 0x22
0x4846D4: mov      eax, dword ptr [ebp - 4]
0x4846D7: mov      eax, dword ptr [eax + 0x4b8]
0x4846DD: call     0x4271d8
0x4846E2: mov      edx, 0x1f
0x4846E7: mov      eax, dword ptr [ebp - 4]
0x4846EA: mov      eax, dword ptr [eax + 0x4b8]
0x4846F0: call     0x4271fc
0x4846F5: mov      edx, 0x2f
0x4846FA: mov      eax, dword ptr [ebp - 4]
0x4846FD: mov      eax, dword ptr [eax + 0x498]
0x484703: call     0x42718c
0x484708: mov      edx, 0x50
0x48470D: mov      eax, dword ptr [ebp - 4]
0x484710: mov      eax, dword ptr [eax + 0x498]
0x484716: call     0x4271b0
0x48471B: mov      edx, 0x2f
0x484720: mov      eax, dword ptr [ebp - 4]
0x484723: mov      eax, dword ptr [eax + 0x498]
0x484729: call     0x4271d8
0x48472E: mov      edx, 0x57
0x484733: mov      eax, dword ptr [ebp - 4]
0x484736: mov      eax, dword ptr [eax + 0x498]
0x48473C: call     0x4271fc
0x484741: mov      edx, 0x60
0x484746: mov      eax, dword ptr [ebp - 4]
0x484749: mov      eax, dword ptr [eax + 0x4a4]
0x48474F: call     0x42718c
0x484754: mov      edx, 0x7a
0x484759: mov      eax, dword ptr [ebp - 4]
0x48475C: mov      eax, dword ptr [eax + 0x4a4]
0x484762: call     0x4271b0
0x484767: mov      edx, 0x35
0x48476C: mov      eax, dword ptr [ebp - 4]
0x48476F: mov      eax, dword ptr [eax + 0x4a4]
0x484775: call     0x4271d8
0x48477A: mov      edx, 0x70
0x48477F: mov      eax, dword ptr [ebp - 4]
0x484782: mov      eax, dword ptr [eax + 0x4a4]
0x484788: call     0x4271fc
0x48478D: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x484792: mov      eax, dword ptr [eax]
0x484794: mov      edx, dword ptr [eax + 0x308]
0x48479A: mov      ecx, 0x173
0x48479F: mov      eax, dword ptr [ebp - 4]
0x4847A2: mov      eax, dword ptr [eax + 0x494]
0x4847A8: call     0x4579f0
0x4847AD: mov      edx, 8
0x4847B2: mov      eax, dword ptr [ebp - 4]
0x4847B5: mov      eax, dword ptr [eax + 0x494]
0x4847BB: call     0x42718c
0x4847C0: mov      edx, 0x27
0x4847C5: mov      eax, dword ptr [ebp - 4]
0x4847C8: mov      eax, dword ptr [eax + 0x494]
0x4847CE: call     0x4271b0
0x4847D3: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4847D8: mov      eax, dword ptr [eax]
0x4847DA: mov      edx, dword ptr [eax + 0x308]
0x4847E0: mov      ecx, 3
0x4847E5: mov      eax, dword ptr [ebp - 4]
0x4847E8: mov      eax, dword ptr [eax + 0x2cc]
0x4847EE: call     0x4579f0
0x4847F3: xor      edx, edx
0x4847F5: mov      eax, dword ptr [ebp - 4]
0x4847F8: mov      eax, dword ptr [eax + 0x2cc]
0x4847FE: call     0x42718c
0x484803: xor      edx, edx
0x484805: mov      eax, dword ptr [ebp - 4]
0x484808: mov      eax, dword ptr [eax + 0x2cc]
0x48480E: call     0x4271b0
0x484813: mov      edx, 0x14
0x484818: mov      eax, dword ptr [ebp - 4]
0x48481B: mov      eax, dword ptr [eax + 0x360]
0x484821: call     0x42718c
0x484826: mov      edx, 0xd
0x48482B: mov      eax, dword ptr [ebp - 4]
0x48482E: mov      eax, dword ptr [eax + 0x360]
0x484834: call     0x4271b0
0x484839: mov      edx, 0x11e
0x48483E: mov      eax, dword ptr [ebp - 4]
0x484841: mov      eax, dword ptr [eax + 0x360]
0x484847: call     0x4271d8
0x48484C: mov      edx, 0xa2
0x484851: mov      eax, dword ptr [ebp - 4]
0x484854: mov      eax, dword ptr [eax + 0x360]
0x48485A: call     0x4271fc
0x48485F: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x484864: mov      eax, dword ptr [eax]
0x484866: mov      eax, dword ptr [eax + 0x308]
0x48486C: mov      edx, 1
0x484871: call     0x45351c
0x484876: mov      dword ptr [ebp - 0x10], eax
0x484879: cmp      dword ptr [ebp - 0x10], 0
0x48487D: je       0x4848dc
0x48487F: xor      edx, edx
0x484881: mov      eax, dword ptr [ebp - 4]
0x484884: mov      eax, dword ptr [eax + 0x2d0]
0x48488A: call     0x42718c
0x48488F: mov      eax, dword ptr [ebp - 0x10]
0x484892: call     0x44d944
0x484897: mov      edx, 0x258
0x48489C: sub      edx, eax
0x48489E: mov      eax, dword ptr [ebp - 4]
0x4848A1: mov      eax, dword ptr [eax + 0x2d0]
0x4848A7: call     0x4271b0
0x4848AC: mov      eax, dword ptr [ebp - 0x10]
0x4848AF: call     0x44dc60
0x4848B4: mov      edx, eax
0x4848B6: mov      eax, dword ptr [ebp - 4]
0x4848B9: mov      eax, dword ptr [eax + 0x2d0]
0x4848BF: call     0x4271d8
0x4848C4: mov      eax, dword ptr [ebp - 0x10]
0x4848C7: call     0x44d944
0x4848CC: mov      edx, eax
0x4848CE: mov      eax, dword ptr [ebp - 4]
0x4848D1: mov      eax, dword ptr [eax + 0x2d0]
0x4848D7: call     0x4271fc
0x4848DC: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4848E1: mov      eax, dword ptr [eax]
0x4848E3: mov      edx, dword ptr [eax + 0x308]
0x4848E9: mov      ecx, 8
0x4848EE: mov      eax, dword ptr [ebp - 4]
0x4848F1: mov      eax, dword ptr [eax + 0x2d4]
0x4848F7: call     0x4579f0
0x4848FC: mov      edx, 0x283
0x484901: mov      eax, dword ptr [ebp - 4]
0x484904: mov      eax, dword ptr [eax + 0x2d4]
0x48490A: call     0x42718c
0x48490F: mov      edx, 0x3d
0x484914: mov      eax, dword ptr [ebp - 4]
0x484917: mov      eax, dword ptr [eax + 0x2d4]
0x48491D: call     0x4271b0
0x484922: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x484927: mov      eax, dword ptr [eax]
0x484929: mov      edx, dword ptr [eax + 0x308]
0x48492F: mov      ecx, 9
0x484934: mov      eax, dword ptr [ebp - 4]
0x484937: mov      eax, dword ptr [eax + 0x2d8]
0x48493D: call     0x4579f0
0x484942: mov      edx, 0x2aa
0x484947: mov      eax, dword ptr [ebp - 4]
0x48494A: mov      eax, dword ptr [eax + 0x2d8]
0x484950: call     0x42718c
0x484955: mov      edx, 0x29
0x48495A: mov      eax, dword ptr [ebp - 4]
0x48495D: mov      eax, dword ptr [eax + 0x2d8]
0x484963: call     0x4271b0
0x484968: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48496D: mov      eax, dword ptr [eax]
0x48496F: mov      edx, dword ptr [eax + 0x308]
0x484975: mov      ecx, 0xa
0x48497A: mov      eax, dword ptr [ebp - 4]
0x48497D: mov      eax, dword ptr [eax + 0x2dc]
0x484983: call     0x4579f0
0x484988: mov      edx, 0x2d2
0x48498D: mov      eax, dword ptr [ebp - 4]
0x484990: mov      eax, dword ptr [eax + 0x2dc]
0x484996: call     0x42718c
0x48499B: mov      edx, 0x15
0x4849A0: mov      eax, dword ptr [ebp - 4]
0x4849A3: mov      eax, dword ptr [eax + 0x2dc]
0x4849A9: call     0x4271b0
0x4849AE: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4849B3: mov      eax, dword ptr [eax]
0x4849B5: mov      edx, dword ptr [eax + 0x308]
0x4849BB: mov      ecx, 0xb
0x4849C0: mov      eax, dword ptr [ebp - 4]
0x4849C3: mov      eax, dword ptr [eax + 0x2e0]
0x4849C9: call     0x4579f0
0x4849CE: mov      edx, 0x2fc
0x4849D3: mov      eax, dword ptr [ebp - 4]
0x4849D6: mov      eax, dword ptr [eax + 0x2e0]
0x4849DC: call     0x42718c
0x4849E1: mov      edx, 0xb
0x4849E6: mov      eax, dword ptr [ebp - 4]
0x4849E9: mov      eax, dword ptr [eax + 0x2e0]
0x4849EF: call     0x4271b0
0x4849F4: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4849F9: mov      eax, dword ptr [eax]
0x4849FB: mov      edx, dword ptr [eax + 0x308]
0x484A01: mov      ecx, 0x82
0x484A06: mov      eax, dword ptr [ebp - 4]
0x484A09: mov      eax, dword ptr [eax + 0x430]
0x484A0F: call     0x4579f0
0x484A14: mov      edx, 0xdb
0x484A19: mov      eax, dword ptr [ebp - 4]
0x484A1C: mov      eax, dword ptr [eax + 0x430]
0x484A22: call     0x42718c
0x484A27: mov      edx, 0x68
0x484A2C: mov      eax, dword ptr [ebp - 4]
0x484A2F: mov      eax, dword ptr [eax + 0x430]
0x484A35: call     0x4271b0
0x484A3A: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x484A3F: mov      eax, dword ptr [eax]
0x484A41: mov      edx, dword ptr [eax + 0x308]
0x484A47: mov      ecx, 0x84
0x484A4C: mov      eax, dword ptr [ebp - 4]
0x484A4F: mov      eax, dword ptr [eax + 0x42c]
0x484A55: call     0x4579f0
0x484A5A: mov      edx, 0xf9
0x484A5F: mov      eax, dword ptr [ebp - 4]
0x484A62: mov      eax, dword ptr [eax + 0x42c]
0x484A68: call     0x42718c
0x484A6D: mov      edx, 0x68
0x484A72: mov      eax, dword ptr [ebp - 4]
0x484A75: mov      eax, dword ptr [eax + 0x42c]
0x484A7B: call     0x4271b0
0x484A80: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x484A85: mov      eax, dword ptr [eax]
0x484A87: mov      edx, dword ptr [eax + 0x308]
0x484A8D: mov      ecx, 0x86
0x484A92: mov      eax, dword ptr [ebp - 4]
0x484A95: mov      eax, dword ptr [eax + 0x454]
0x484A9B: call     0x4579f0
0x484AA0: mov      edx, 0x117
0x484AA5: mov      eax, dword ptr [ebp - 4]
0x484AA8: mov      eax, dword ptr [eax + 0x454]
0x484AAE: call     0x42718c
0x484AB3: mov      edx, 0x68
0x484AB8: mov      eax, dword ptr [ebp - 4]
0x484ABB: mov      eax, dword ptr [eax + 0x454]
0x484AC1: call     0x4271b0
0x484AC6: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x484ACB: mov      eax, dword ptr [eax]
0x484ACD: mov      edx, dword ptr [eax + 0x308]
0x484AD3: mov      ecx, 0x80
0x484AD8: mov      eax, dword ptr [ebp - 4]
0x484ADB: mov      eax, dword ptr [eax + 0x428]
0x484AE1: call     0x4579f0
0x484AE6: mov      edx, 0x135
0x484AEB: mov      eax, dword ptr [ebp - 4]
0x484AEE: mov      eax, dword ptr [eax + 0x428]
0x484AF4: call     0x42718c
0x484AF9: mov      edx, 0x68
0x484AFE: mov      eax, dword ptr [ebp - 4]
0x484B01: mov      eax, dword ptr [eax + 0x428]
0x484B07: call     0x4271b0
0x484B0C: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x484B11: mov      eax, dword ptr [eax]
0x484B13: mov      edx, dword ptr [eax + 0x308]
0x484B19: mov      ecx, 0x8c
0x484B1E: mov      eax, dword ptr [ebp - 4]
0x484B21: mov      eax, dword ptr [eax + 0x56c]
0x484B27: call     0x4579f0
0x484B2C: mov      edx, 0x153
0x484B31: mov      eax, dword ptr [ebp - 4]
0x484B34: mov      eax, dword ptr [eax + 0x56c]
0x484B3A: call     0x42718c
0x484B3F: mov      edx, 0x68
0x484B44: mov      eax, dword ptr [ebp - 4]
0x484B47: mov      eax, dword ptr [eax + 0x56c]
0x484B4D: call     0x4271b0
0x484B52: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x484B57: mov      eax, dword ptr [eax]
0x484B59: mov      edx, dword ptr [eax + 0x308]
0x484B5F: mov      ecx, 0x8a
0x484B64: mov      eax, dword ptr [ebp - 4]
0x484B67: mov      eax, dword ptr [eax + 0x450]
0x484B6D: call     0x4579f0
0x484B72: mov      edx, 0x230
0x484B77: mov      eax, dword ptr [ebp - 4]
0x484B7A: mov      eax, dword ptr [eax + 0x450]
0x484B80: call     0x42718c
0x484B85: mov      edx, 0x68
0x484B8A: mov      eax, dword ptr [ebp - 4]
0x484B8D: mov      eax, dword ptr [eax + 0x450]
0x484B93: call     0x4271b0
0x484B98: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x484B9D: mov      eax, dword ptr [eax]
0x484B9F: mov      edx, dword ptr [eax + 0x308]
0x484BA5: mov      ecx, 0x88
0x484BAA: mov      eax, dword ptr [ebp - 4]
0x484BAD: mov      eax, dword ptr [eax + 0x44c]
0x484BB3: call     0x4579f0
0x484BB8: mov      edx, 0x212
0x484BBD: mov      eax, dword ptr [ebp - 4]
0x484BC0: mov      eax, dword ptr [eax + 0x44c]
0x484BC6: call     0x42718c
0x484BCB: mov      edx, 0x68
0x484BD0: mov      eax, dword ptr [ebp - 4]
0x484BD3: mov      eax, dword ptr [eax + 0x44c]
0x484BD9: call     0x4271b0
0x484BDE: mov      edx, 0x11d
0x484BE3: mov      eax, dword ptr [ebp - 4]
0x484BE6: mov      eax, dword ptr [eax + 0x3a4]
0x484BEC: call     0x42718c
0x484BF1: mov      edx, 0x20
0x484BF6: mov      eax, dword ptr [ebp - 4]
0x484BF9: mov      eax, dword ptr [eax + 0x3a4]
0x484BFF: call     0x4271d8
0x484C04: mov      edx, 0x3b
0x484C09: mov      eax, dword ptr [ebp - 4]
0x484C0C: mov      eax, dword ptr [eax + 0x3a4]
0x484C12: call     0x4271b0
0x484C17: mov      edx, 0x1d
0x484C1C: mov      eax, dword ptr [ebp - 4]
0x484C1F: mov      eax, dword ptr [eax + 0x3a4]
0x484C25: call     0x4271fc
0x484C2A: mov      edx, 0x148
0x484C2F: mov      eax, dword ptr [ebp - 4]
0x484C32: mov      eax, dword ptr [eax + 0x3a8]
0x484C38: call     0x42718c
0x484C3D: mov      edx, 0x20
0x484C42: mov      eax, dword ptr [ebp - 4]
0x484C45: mov      eax, dword ptr [eax + 0x3a8]
0x484C4B: call     0x4271d8
0x484C50: mov      edx, 0x3b
0x484C55: mov      eax, dword ptr [ebp - 4]
0x484C58: mov      eax, dword ptr [eax + 0x3a8]
0x484C5E: call     0x4271b0
0x484C63: mov      edx, 0x1d
0x484C68: mov      eax, dword ptr [ebp - 4]
0x484C6B: mov      eax, dword ptr [eax + 0x3a8]
0x484C71: call     0x4271fc
0x484C76: mov      edx, 0x173
0x484C7B: mov      eax, dword ptr [ebp - 4]
0x484C7E: mov      eax, dword ptr [eax + 0x3ac]
0x484C84: call     0x42718c
0x484C89: mov      edx, 0x20
0x484C8E: mov      eax, dword ptr [ebp - 4]
0x484C91: mov      eax, dword ptr [eax + 0x3ac]
0x484C97: call     0x4271d8
0x484C9C: mov      edx, 0x3b
0x484CA1: mov      eax, dword ptr [ebp - 4]
0x484CA4: mov      eax, dword ptr [eax + 0x3ac]
0x484CAA: call     0x4271b0
0x484CAF: mov      edx, 0x1d
0x484CB4: mov      eax, dword ptr [ebp - 4]
0x484CB7: mov      eax, dword ptr [eax + 0x3ac]
0x484CBD: call     0x4271fc
0x484CC2: mov      edx, 0x19f
0x484CC7: mov      eax, dword ptr [ebp - 4]
0x484CCA: mov      eax, dword ptr [eax + 0x3b0]
0x484CD0: call     0x42718c
0x484CD5: mov      edx, 0x20
0x484CDA: mov      eax, dword ptr [ebp - 4]
0x484CDD: mov      eax, dword ptr [eax + 0x3b0]
0x484CE3: call     0x4271d8
0x484CE8: mov      edx, 0x3b
0x484CED: mov      eax, dword ptr [ebp - 4]
0x484CF0: mov      eax, dword ptr [eax + 0x3b0]
0x484CF6: call     0x4271b0
0x484CFB: mov      edx, 0x1d
0x484D00: mov      eax, dword ptr [ebp - 4]
0x484D03: mov      eax, dword ptr [eax + 0x3b0]
0x484D09: call     0x4271fc
0x484D0E: mov      edx, 0x1cb
0x484D13: mov      eax, dword ptr [ebp - 4]
0x484D16: mov      eax, dword ptr [eax + 0x3b4]
0x484D1C: call     0x42718c
0x484D21: mov      edx, 0x20
0x484D26: mov      eax, dword ptr [ebp - 4]
0x484D29: mov      eax, dword ptr [eax + 0x3b4]
0x484D2F: call     0x4271d8
0x484D34: mov      edx, 0x3b
0x484D39: mov      eax, dword ptr [ebp - 4]
0x484D3C: mov      eax, dword ptr [eax + 0x3b4]
0x484D42: call     0x4271b0
0x484D47: mov      edx, 0x1d
0x484D4C: mov      eax, dword ptr [ebp - 4]
0x484D4F: mov      eax, dword ptr [eax + 0x3b4]
0x484D55: call     0x4271fc
0x484D5A: mov      edx, 0x1f7
0x484D5F: mov      eax, dword ptr [ebp - 4]
0x484D62: mov      eax, dword ptr [eax + 0x3b8]
0x484D68: call     0x42718c
0x484D6D: mov      edx, 0x20
0x484D72: mov      eax, dword ptr [ebp - 4]
0x484D75: mov      eax, dword ptr [eax + 0x3b8]
0x484D7B: call     0x4271d8
0x484D80: mov      edx, 0x3b
0x484D85: mov      eax, dword ptr [ebp - 4]
0x484D88: mov      eax, dword ptr [eax + 0x3b8]
0x484D8E: call     0x4271b0
0x484D93: mov      edx, 0x1d
0x484D98: mov      eax, dword ptr [ebp - 4]
0x484D9B: mov      eax, dword ptr [eax + 0x3b8]
0x484DA1: call     0x4271fc
0x484DA6: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x484DAB: mov      eax, dword ptr [eax]
0x484DAD: mov      edx, dword ptr [eax + 0x308]
0x484DB3: mov      ecx, 0x1d
0x484DB8: mov      eax, dword ptr [ebp - 4]
0x484DBB: mov      eax, dword ptr [eax + 0x2e4]
0x484DC1: call     0x4579f0
0x484DC6: mov      edx, 0xa
0x484DCB: mov      eax, dword ptr [ebp - 4]
0x484DCE: mov      eax, dword ptr [eax + 0x2e4]
0x484DD4: call     0x42718c
0x484DD9: mov      edx, 0xbe
0x484DDE: mov      eax, dword ptr [ebp - 4]
0x484DE1: mov      eax, dword ptr [eax + 0x2e4]
0x484DE7: call     0x4271b0
0x484DEC: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x484DF1: mov      eax, dword ptr [eax]
0x484DF3: mov      edx, dword ptr [eax + 0x308]
0x484DF9: mov      ecx, 0x1a
0x484DFE: mov      eax, dword ptr [ebp - 4]
0x484E01: mov      eax, dword ptr [eax + 0x2ec]
0x484E07: call     0x4579f0
0x484E0C: mov      edx, 0xfe
0x484E11: mov      eax, dword ptr [ebp - 4]
0x484E14: mov      eax, dword ptr [eax + 0x2ec]
0x484E1A: call     0x42718c
0x484E1F: mov      edx, 0xb7
0x484E24: mov      eax, dword ptr [ebp - 4]
0x484E27: mov      eax, dword ptr [eax + 0x2ec]
0x484E2D: call     0x4271b0
0x484E32: mov      edx, 0x30
0x484E37: mov      eax, dword ptr [ebp - 4]
0x484E3A: mov      eax, dword ptr [eax + 0x2ec]
0x484E40: call     0x4271d8
0x484E45: mov      edx, 0x16
0x484E4A: mov      eax, dword ptr [ebp - 4]
0x484E4D: mov      eax, dword ptr [eax + 0x2ec]
0x484E53: call     0x4271fc
0x484E58: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x484E5D: mov      eax, dword ptr [eax]
0x484E5F: mov      edx, dword ptr [eax + 0x308]
0x484E65: mov      ecx, 0x173
0x484E6A: mov      eax, dword ptr [ebp - 4]
0x484E6D: mov      eax, dword ptr [eax + 0x2f0]
0x484E73: call     0x4579f0
0x484E78: mov      edx, 0x135
0x484E7D: mov      eax, dword ptr [ebp - 4]
0x484E80: mov      eax, dword ptr [eax + 0x2f0]
0x484E86: call     0x42718c
0x484E8B: mov      edx, 0xcb
0x484E90: mov      eax, dword ptr [ebp - 4]
0x484E93: mov      eax, dword ptr [eax + 0x2f0]
0x484E99: call     0x4271b0
0x484E9E: mov      edx, 0xe
0x484EA3: mov      eax, dword ptr [ebp - 4]
0x484EA6: mov      eax, dword ptr [eax + 0x2f0]
0x484EAC: call     0x4271d8
0x484EB1: mov      edx, 0x14
0x484EB6: mov      eax, dword ptr [ebp - 4]
0x484EB9: mov      eax, dword ptr [eax + 0x2f0]
0x484EBF: call     0x4271fc
0x484EC4: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x484EC9: mov      eax, dword ptr [eax]
0x484ECB: mov      eax, dword ptr [eax + 0x308]
0x484ED1: mov      edx, 0x180
0x484ED6: call     0x45351c
0x484EDB: mov      dword ptr [ebp - 0x10], eax
0x484EDE: cmp      dword ptr [ebp - 0x10], 0
0x484EE2: je       0x484f24
0x484EE4: xor      edx, edx
0x484EE6: mov      eax, dword ptr [ebp - 4]
0x484EE9: mov      eax, dword ptr [eax + 0x3c8]
0x484EEF: call     0x42718c
0x484EF4: xor      edx, edx
0x484EF6: mov      eax, dword ptr [ebp - 4]
0x484EF9: mov      eax, dword ptr [eax + 0x3c8]
0x484EFF: call     0x4271b0
0x484F04: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x484F09: mov      eax, dword ptr [eax]
0x484F0B: mov      edx, dword ptr [eax + 0x308]
0x484F11: mov      ecx, 0x180
0x484F16: mov      eax, dword ptr [ebp - 4]
0x484F19: mov      eax, dword ptr [eax + 0x3c8]
0x484F1F: call     0x4579f0
0x484F24: mov      edx, 0x18f
0x484F29: mov      eax, dword ptr [ebp - 4]
0x484F2C: mov      eax, dword ptr [eax + 0x3cc]
0x484F32: call     0x42718c
0x484F37: mov      edx, 1
0x484F3C: mov      eax, dword ptr [ebp - 4]
0x484F3F: mov      eax, dword ptr [eax + 0x3cc]
0x484F45: call     0x4271b0
0x484F4A: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x484F4F: mov      eax, dword ptr [eax]
0x484F51: mov      edx, dword ptr [eax + 0x308]
0x484F57: mov      ecx, 0x40
0x484F5C: mov      eax, dword ptr [ebp - 4]
0x484F5F: mov      eax, dword ptr [eax + 0x3cc]
0x484F65: call     0x4579f0
0x484F6A: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x484F6F: mov      eax, dword ptr [eax]
0x484F71: mov      eax, dword ptr [eax + 0x308]
0x484F77: mov      edx, 0x181
0x484F7C: call     0x45351c
0x484F81: mov      dword ptr [ebp - 0x10], eax
0x484F84: cmp      dword ptr [ebp - 0x10], 0
0x484F88: je       0x484fd0
0x484F8A: mov      edx, 0x8a
0x484F8F: mov      eax, dword ptr [ebp - 4]
0x484F92: mov      eax, dword ptr [eax + 0x3d0]
0x484F98: call     0x42718c
0x484F9D: mov      edx, 0xa3
0x484FA2: mov      eax, dword ptr [ebp - 4]
0x484FA5: mov      eax, dword ptr [eax + 0x3d0]
0x484FAB: call     0x4271b0
0x484FB0: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x484FB5: mov      eax, dword ptr [eax]
0x484FB7: mov      edx, dword ptr [eax + 0x308]
0x484FBD: mov      ecx, 0x181
0x484FC2: mov      eax, dword ptr [ebp - 4]
0x484FC5: mov      eax, dword ptr [eax + 0x3d0]
0x484FCB: call     0x4579f0
0x484FD0: mov      edx, 0x2b
0x484FD5: mov      eax, dword ptr [ebp - 4]
0x484FD8: mov      eax, dword ptr [eax + 0x3d4]
0x484FDE: call     0x42718c
0x484FE3: mov      edx, 0xaf
0x484FE8: mov      eax, dword ptr [ebp - 4]
0x484FEB: mov      eax, dword ptr [eax + 0x3d4]
0x484FF1: call     0x4271b0
0x484FF6: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x484FFB: mov      eax, dword ptr [eax]
0x484FFD: mov      edx, dword ptr [eax + 0x308]
0x485003: mov      ecx, 0x184
0x485008: mov      eax, dword ptr [ebp - 4]
0x48500B: mov      eax, dword ptr [eax + 0x3d4]
0x485011: call     0x4579f0
0x485016: mov      edx, 0x5a
0x48501B: mov      eax, dword ptr [ebp - 4]
0x48501E: mov      eax, dword ptr [eax + 0x3d8]
0x485024: call     0x42718c
0x485029: mov      edx, 0xaf
0x48502E: mov      eax, dword ptr [ebp - 4]
0x485031: mov      eax, dword ptr [eax + 0x3d8]
0x485037: call     0x4271b0
0x48503C: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485041: mov      eax, dword ptr [eax]
0x485043: mov      edx, dword ptr [eax + 0x308]
0x485049: mov      ecx, 0x183
0x48504E: mov      eax, dword ptr [ebp - 4]
0x485051: mov      eax, dword ptr [eax + 0x3d8]
0x485057: call     0x4579f0
0x48505C: mov      edx, 0xd7
0x485061: mov      eax, dword ptr [ebp - 4]
0x485064: mov      eax, dword ptr [eax + 0x3dc]
0x48506A: call     0x42718c
0x48506F: mov      edx, 0xab
0x485074: mov      eax, dword ptr [ebp - 4]
0x485077: mov      eax, dword ptr [eax + 0x3dc]
0x48507D: call     0x4271b0
0x485082: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485087: mov      eax, dword ptr [eax]
0x485089: mov      edx, dword ptr [eax + 0x308]
0x48508F: mov      ecx, 0x182
0x485094: mov      eax, dword ptr [ebp - 4]
0x485097: mov      eax, dword ptr [eax + 0x3dc]
0x48509D: call     0x4579f0
0x4850A2: mov      edx, 0x123
0x4850A7: mov      eax, dword ptr [ebp - 4]
0x4850AA: mov      eax, dword ptr [eax + 0x3e0]
0x4850B0: call     0x42718c
0x4850B5: xor      edx, edx
0x4850B7: mov      eax, dword ptr [ebp - 4]
0x4850BA: mov      eax, dword ptr [eax + 0x3e0]
0x4850C0: call     0x4271b0
0x4850C5: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4850CA: mov      eax, dword ptr [eax]
0x4850CC: mov      edx, dword ptr [eax + 0x308]
0x4850D2: mov      ecx, 0x40
0x4850D7: mov      eax, dword ptr [ebp - 4]
0x4850DA: mov      eax, dword ptr [eax + 0x3e0]
0x4850E0: call     0x4579f0
0x4850E5: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4850EA: mov      eax, dword ptr [eax]
0x4850EC: mov      eax, dword ptr [eax + 0x308]
0x4850F2: mov      edx, 0x188
0x4850F7: call     0x45351c
0x4850FC: mov      dword ptr [ebp - 0x10], eax
0x4850FF: cmp      dword ptr [ebp - 0x10], 0
0x485103: je       0x48514b
0x485105: mov      edx, 0x148
0x48510A: mov      eax, dword ptr [ebp - 4]
0x48510D: mov      eax, dword ptr [eax + 0x3e4]
0x485113: call     0x42718c
0x485118: mov      edx, 0xa3
0x48511D: mov      eax, dword ptr [ebp - 4]
0x485120: mov      eax, dword ptr [eax + 0x3e4]
0x485126: call     0x4271b0
0x48512B: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485130: mov      eax, dword ptr [eax]
0x485132: mov      edx, dword ptr [eax + 0x308]
0x485138: mov      ecx, 0x188
0x48513D: mov      eax, dword ptr [ebp - 4]
0x485140: mov      eax, dword ptr [eax + 0x3e4]
0x485146: call     0x4579f0
0x48514B: mov      edx, 0x55
0x485150: mov      eax, dword ptr [ebp - 4]
0x485153: mov      eax, dword ptr [eax + 0x3e8]
0x485159: call     0x42718c
0x48515E: mov      edx, 0x96
0x485163: mov      eax, dword ptr [ebp - 4]
0x485166: mov      eax, dword ptr [eax + 0x3e8]
0x48516C: call     0x4271b0
0x485171: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485176: mov      eax, dword ptr [eax]
0x485178: mov      edx, dword ptr [eax + 0x308]
0x48517E: mov      ecx, 0x189
0x485183: mov      eax, dword ptr [ebp - 4]
0x485186: mov      eax, dword ptr [eax + 0x3e8]
0x48518C: call     0x4579f0
0x485191: mov      edx, 0x73
0x485196: mov      eax, dword ptr [ebp - 4]
0x485199: mov      eax, dword ptr [eax + 0x3ec]
0x48519F: call     0x42718c
0x4851A4: xor      edx, edx
0x4851A6: mov      eax, dword ptr [ebp - 4]
0x4851A9: mov      eax, dword ptr [eax + 0x3ec]
0x4851AF: call     0x4271b0
0x4851B4: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4851B9: mov      eax, dword ptr [eax]
0x4851BB: mov      edx, dword ptr [eax + 0x308]
0x4851C1: mov      ecx, 0x40
0x4851C6: mov      eax, dword ptr [ebp - 4]
0x4851C9: mov      eax, dword ptr [eax + 0x3ec]
0x4851CF: call     0x4579f0
0x4851D4: mov      edx, 0x1b
0x4851D9: mov      eax, dword ptr [ebp - 4]
0x4851DC: mov      eax, dword ptr [eax + 0x3f0]
0x4851E2: call     0x42718c
0x4851E7: mov      edx, 0x43
0x4851EC: mov      eax, dword ptr [ebp - 4]
0x4851EF: mov      eax, dword ptr [eax + 0x3f0]
0x4851F5: call     0x4271b0
0x4851FA: mov      edx, 0x3d
0x4851FF: mov      eax, dword ptr [ebp - 4]
0x485202: mov      eax, dword ptr [eax + 0x3f0]
0x485208: call     0x4271d8
0x48520D: mov      edx, 0x34
0x485212: mov      eax, dword ptr [ebp - 4]
0x485215: mov      eax, dword ptr [eax + 0x3f0]
0x48521B: call     0x4271fc
0x485220: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485225: mov      eax, dword ptr [eax]
0x485227: mov      eax, dword ptr [eax + 0x308]
0x48522D: mov      edx, 0xe5
0x485232: call     0x45351c
0x485237: mov      dword ptr [ebp - 0x10], eax
0x48523A: cmp      dword ptr [ebp - 0x10], 0
0x48523E: je       0x4852a8
0x485240: mov      eax, dword ptr [ebp - 0x10]
0x485243: call     0x44dc60
0x485248: mov      edx, 0x320
0x48524D: sub      edx, eax
0x48524F: sar      edx, 1
0x485251: jns      0x485256
0x485253: adc      edx, 0
0x485256: mov      eax, dword ptr [ebp - 4]
0x485259: mov      eax, dword ptr [eax + 0x408]
0x48525F: call     0x42718c
0x485264: mov      eax, dword ptr [ebp - 0x10]
0x485267: call     0x44d944
0x48526C: mov      edx, 0x258
0x485271: sub      edx, eax
0x485273: sar      edx, 1
0x485275: jns      0x48527a
0x485277: adc      edx, 0
0x48527A: mov      eax, dword ptr [ebp - 4]
0x48527D: mov      eax, dword ptr [eax + 0x408]
0x485283: call     0x4271b0
0x485288: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48528D: mov      eax, dword ptr [eax]
0x48528F: mov      edx, dword ptr [eax + 0x308]
0x485295: mov      ecx, 0xe5
0x48529A: mov      eax, dword ptr [ebp - 4]
0x48529D: mov      eax, dword ptr [eax + 0x408]
0x4852A3: call     0x4579f0
0x4852A8: mov      edx, 0x34
0x4852AD: mov      eax, dword ptr [ebp - 4]
0x4852B0: mov      eax, dword ptr [eax + 0x40c]
0x4852B6: call     0x42718c
0x4852BB: mov      edx, 0x1d
0x4852C0: mov      eax, dword ptr [ebp - 4]
0x4852C3: mov      eax, dword ptr [eax + 0x40c]
0x4852C9: call     0x4271b0
0x4852CE: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4852D3: mov      eax, dword ptr [eax]
0x4852D5: mov      edx, dword ptr [eax + 0x308]
0x4852DB: mov      ecx, 0xe8
0x4852E0: mov      eax, dword ptr [ebp - 4]
0x4852E3: mov      eax, dword ptr [eax + 0x410]
0x4852E9: call     0x4579f0
0x4852EE: mov      edx, 0x22
0x4852F3: mov      eax, dword ptr [ebp - 4]
0x4852F6: mov      eax, dword ptr [eax + 0x410]
0x4852FC: call     0x42718c
0x485301: mov      edx, 0x53
0x485306: mov      eax, dword ptr [ebp - 4]
0x485309: mov      eax, dword ptr [eax + 0x410]
0x48530F: call     0x4271b0
0x485314: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485319: mov      eax, dword ptr [eax]
0x48531B: mov      edx, dword ptr [eax + 0x308]
0x485321: mov      ecx, 0xea
0x485326: mov      eax, dword ptr [ebp - 4]
0x485329: mov      eax, dword ptr [eax + 0x414]
0x48532F: call     0x4579f0
0x485334: mov      edx, 0x42
0x485339: mov      eax, dword ptr [ebp - 4]
0x48533C: mov      eax, dword ptr [eax + 0x414]
0x485342: call     0x42718c
0x485347: mov      edx, 0x53
0x48534C: mov      eax, dword ptr [ebp - 4]
0x48534F: mov      eax, dword ptr [eax + 0x414]
0x485355: call     0x4271b0
0x48535A: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48535F: mov      eax, dword ptr [eax]
0x485361: mov      edx, dword ptr [eax + 0x308]
0x485367: mov      ecx, 0xec
0x48536C: mov      eax, dword ptr [ebp - 4]
0x48536F: mov      eax, dword ptr [eax + 0x418]
0x485375: call     0x4579f0
0x48537A: mov      edx, 0x62
0x48537F: mov      eax, dword ptr [ebp - 4]
0x485382: mov      eax, dword ptr [eax + 0x418]
0x485388: call     0x42718c
0x48538D: mov      edx, 0x53
0x485392: mov      eax, dword ptr [ebp - 4]
0x485395: mov      eax, dword ptr [eax + 0x418]
0x48539B: call     0x4271b0
0x4853A0: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4853A5: mov      eax, dword ptr [eax]
0x4853A7: mov      edx, dword ptr [eax + 0x308]
0x4853AD: mov      ecx, 0xee
0x4853B2: mov      eax, dword ptr [ebp - 4]
0x4853B5: mov      eax, dword ptr [eax + 0x41c]
0x4853BB: call     0x4579f0
0x4853C0: mov      edx, 0x82
0x4853C5: mov      eax, dword ptr [ebp - 4]
0x4853C8: mov      eax, dword ptr [eax + 0x41c]
0x4853CE: call     0x42718c
0x4853D3: mov      edx, 0x53
0x4853D8: mov      eax, dword ptr [ebp - 4]
0x4853DB: mov      eax, dword ptr [eax + 0x41c]
0x4853E1: call     0x4271b0
0x4853E6: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4853EB: mov      eax, dword ptr [eax]
0x4853ED: mov      edx, dword ptr [eax + 0x308]
0x4853F3: mov      ecx, 0xf0
0x4853F8: mov      eax, dword ptr [ebp - 4]
0x4853FB: mov      eax, dword ptr [eax + 0x570]
0x485401: call     0x4579f0
0x485406: mov      edx, 0xab
0x48540B: mov      eax, dword ptr [ebp - 4]
0x48540E: mov      eax, dword ptr [eax + 0x570]
0x485414: call     0x42718c
0x485419: mov      edx, 0x53
0x48541E: mov      eax, dword ptr [ebp - 4]
0x485421: mov      eax, dword ptr [eax + 0x570]
0x485427: call     0x4271b0
0x48542C: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485431: mov      eax, dword ptr [eax]
0x485433: mov      edx, dword ptr [eax + 0x308]
0x485439: mov      ecx, 0xf2
0x48543E: mov      eax, dword ptr [ebp - 4]
0x485441: mov      eax, dword ptr [eax + 0x574]
0x485447: call     0x4579f0
0x48544C: mov      edx, 0xcb
0x485451: mov      eax, dword ptr [ebp - 4]
0x485454: mov      eax, dword ptr [eax + 0x574]
0x48545A: call     0x42718c
0x48545F: mov      edx, 0x53
0x485464: mov      eax, dword ptr [ebp - 4]
0x485467: mov      eax, dword ptr [eax + 0x574]
0x48546D: call     0x4271b0
0x485472: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485477: mov      eax, dword ptr [eax]
0x485479: mov      edx, dword ptr [eax + 0x308]
0x48547F: mov      ecx, 0xf4
0x485484: mov      eax, dword ptr [ebp - 4]
0x485487: mov      eax, dword ptr [eax + 0x578]
0x48548D: call     0x4579f0
0x485492: mov      edx, 0xeb
0x485497: mov      eax, dword ptr [ebp - 4]
0x48549A: mov      eax, dword ptr [eax + 0x578]
0x4854A0: call     0x42718c
0x4854A5: mov      edx, 0x53
0x4854AA: mov      eax, dword ptr [ebp - 4]
0x4854AD: mov      eax, dword ptr [eax + 0x578]
0x4854B3: call     0x4271b0
0x4854B8: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4854BD: mov      eax, dword ptr [eax]
0x4854BF: mov      edx, dword ptr [eax + 0x308]
0x4854C5: mov      ecx, 0xf6
0x4854CA: mov      eax, dword ptr [ebp - 4]
0x4854CD: mov      eax, dword ptr [eax + 0x57c]
0x4854D3: call     0x4579f0
0x4854D8: mov      edx, 0x10b
0x4854DD: mov      eax, dword ptr [ebp - 4]
0x4854E0: mov      eax, dword ptr [eax + 0x57c]
0x4854E6: call     0x42718c
0x4854EB: mov      edx, 0x53
0x4854F0: mov      eax, dword ptr [ebp - 4]
0x4854F3: mov      eax, dword ptr [eax + 0x57c]
0x4854F9: call     0x4271b0
0x4854FE: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485503: mov      eax, dword ptr [eax]
0x485505: mov      edx, dword ptr [eax + 0x308]
0x48550B: mov      ecx, 0xe6
0x485510: mov      eax, dword ptr [ebp - 4]
0x485513: mov      eax, dword ptr [eax + 0x420]
0x485519: call     0x4579f0
0x48551E: mov      edx, 0x12b
0x485523: mov      eax, dword ptr [ebp - 4]
0x485526: mov      eax, dword ptr [eax + 0x420]
0x48552C: call     0x42718c
0x485531: mov      edx, 0x53
0x485536: mov      eax, dword ptr [ebp - 4]
0x485539: mov      eax, dword ptr [eax + 0x420]
0x48553F: call     0x4271b0
0x485544: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485549: mov      eax, dword ptr [eax]
0x48554B: mov      edx, dword ptr [eax + 0x308]
0x485551: mov      ecx, 0x3e
0x485556: mov      eax, dword ptr [ebp - 4]
0x485559: mov      eax, dword ptr [eax + 0x424]
0x48555F: call     0x4579f0
0x485564: mov      edx, 0xde
0x485569: mov      eax, dword ptr [ebp - 4]
0x48556C: mov      eax, dword ptr [eax + 0x424]
0x485572: call     0x42718c
0x485577: mov      edx, 0x83
0x48557C: mov      eax, dword ptr [ebp - 4]
0x48557F: mov      eax, dword ptr [eax + 0x424]
0x485585: call     0x4271b0
0x48558A: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48558F: mov      eax, dword ptr [eax]
0x485591: mov      eax, dword ptr [eax + 0x308]
0x485597: mov      edx, 0x78
0x48559C: call     0x45351c
0x4855A1: mov      dword ptr [ebp - 0x10], eax
0x4855A4: cmp      dword ptr [ebp - 0x10], 0
0x4855A8: je       0x485612
0x4855AA: mov      eax, dword ptr [ebp - 0x10]
0x4855AD: call     0x44dc60
0x4855B2: mov      edx, 0x320
0x4855B7: sub      edx, eax
0x4855B9: sar      edx, 1
0x4855BB: jns      0x4855c0
0x4855BD: adc      edx, 0
0x4855C0: mov      eax, dword ptr [ebp - 4]
0x4855C3: mov      eax, dword ptr [eax + 0x434]
0x4855C9: call     0x42718c
0x4855CE: mov      eax, dword ptr [ebp - 0x10]
0x4855D1: call     0x44d944
0x4855D6: mov      edx, 0x258
0x4855DB: sub      edx, eax
0x4855DD: sar      edx, 1
0x4855DF: jns      0x4855e4
0x4855E1: adc      edx, 0
0x4855E4: mov      eax, dword ptr [ebp - 4]
0x4855E7: mov      eax, dword ptr [eax + 0x434]
0x4855ED: call     0x4271b0
0x4855F2: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4855F7: mov      eax, dword ptr [eax]
0x4855F9: mov      edx, dword ptr [eax + 0x308]
0x4855FF: mov      ecx, 0x78
0x485604: mov      eax, dword ptr [ebp - 4]
0x485607: mov      eax, dword ptr [eax + 0x434]
0x48560D: call     0x4579f0
0x485612: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485617: mov      eax, dword ptr [eax]
0x485619: mov      edx, dword ptr [eax + 0x308]
0x48561F: mov      ecx, 0x40
0x485624: mov      eax, dword ptr [ebp - 4]
0x485627: mov      eax, dword ptr [eax + 0x43c]
0x48562D: call     0x4579f0
0x485632: mov      edx, 0x104
0x485637: mov      eax, dword ptr [ebp - 4]
0x48563A: mov      eax, dword ptr [eax + 0x43c]
0x485640: call     0x42718c
0x485645: xor      edx, edx
0x485647: mov      eax, dword ptr [ebp - 4]
0x48564A: mov      eax, dword ptr [eax + 0x43c]
0x485650: call     0x4271b0
0x485655: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48565A: mov      eax, dword ptr [eax]
0x48565C: mov      edx, dword ptr [eax + 0x308]
0x485662: mov      ecx, 0x7a
0x485667: mov      eax, dword ptr [ebp - 4]
0x48566A: mov      eax, dword ptr [eax + 0x438]
0x485670: call     0x4579f0
0x485675: mov      edx, 0x14
0x48567A: mov      eax, dword ptr [ebp - 4]
0x48567D: mov      eax, dword ptr [eax + 0x438]
0x485683: call     0x42718c
0x485688: mov      edx, 0x12
0x48568D: mov      eax, dword ptr [ebp - 4]
0x485690: mov      eax, dword ptr [eax + 0x438]
0x485696: call     0x4271b0
0x48569B: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4856A0: mov      eax, dword ptr [eax]
0x4856A2: mov      edx, dword ptr [eax + 0x308]
0x4856A8: mov      ecx, 0x7b
0x4856AD: mov      eax, dword ptr [ebp - 4]
0x4856B0: mov      eax, dword ptr [eax + 0x440]
0x4856B6: call     0x4579f0
0x4856BB: mov      edx, 0x16
0x4856C0: mov      eax, dword ptr [ebp - 4]
0x4856C3: mov      eax, dword ptr [eax + 0x440]
0x4856C9: call     0x42718c
0x4856CE: mov      edx, 0xcb
0x4856D3: mov      eax, dword ptr [ebp - 4]
0x4856D6: mov      eax, dword ptr [eax + 0x440]
0x4856DC: call     0x4271b0
0x4856E1: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4856E6: mov      eax, dword ptr [eax]
0x4856E8: mov      edx, dword ptr [eax + 0x308]
0x4856EE: mov      ecx, 0x7c
0x4856F3: mov      eax, dword ptr [ebp - 4]
0x4856F6: mov      eax, dword ptr [eax + 0x444]
0x4856FC: call     0x4579f0
0x485701: mov      edx, 0x61
0x485706: mov      eax, dword ptr [ebp - 4]
0x485709: mov      eax, dword ptr [eax + 0x444]
0x48570F: call     0x42718c
0x485714: mov      edx, 0xcb
0x485719: mov      eax, dword ptr [ebp - 4]
0x48571C: mov      eax, dword ptr [eax + 0x444]
0x485722: call     0x4271b0
0x485727: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48572C: mov      eax, dword ptr [eax]
0x48572E: mov      edx, dword ptr [eax + 0x308]
0x485734: mov      ecx, 0x7d
0x485739: mov      eax, dword ptr [ebp - 4]
0x48573C: mov      eax, dword ptr [eax + 0x448]
0x485742: call     0x4579f0
0x485747: mov      edx, 0xac
0x48574C: mov      eax, dword ptr [ebp - 4]
0x48574F: mov      eax, dword ptr [eax + 0x448]
0x485755: call     0x42718c
0x48575A: mov      edx, 0xcb
0x48575F: mov      eax, dword ptr [ebp - 4]
0x485762: mov      eax, dword ptr [eax + 0x448]
0x485768: call     0x4271b0
0x48576D: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485772: mov      eax, dword ptr [eax]
0x485774: mov      eax, dword ptr [eax + 0x308]
0x48577A: mov      edx, 0x185
0x48577F: call     0x45351c
0x485784: mov      dword ptr [ebp - 0x10], eax
0x485787: cmp      dword ptr [ebp - 0x10], 0
0x48578B: je       0x4857da
0x48578D: mov      eax, dword ptr [ebp - 0x10]
0x485790: call     0x44dc60
0x485795: mov      edx, 0x320
0x48579A: sub      edx, eax
0x48579C: mov      eax, dword ptr [ebp - 4]
0x48579F: mov      eax, dword ptr [eax + 0x464]
0x4857A5: call     0x42718c
0x4857AA: xor      edx, edx
0x4857AC: mov      eax, dword ptr [ebp - 4]
0x4857AF: mov      eax, dword ptr [eax + 0x464]
0x4857B5: call     0x4271b0
0x4857BA: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4857BF: mov      eax, dword ptr [eax]
0x4857C1: mov      edx, dword ptr [eax + 0x308]
0x4857C7: mov      ecx, 0x185
0x4857CC: mov      eax, dword ptr [ebp - 4]
0x4857CF: mov      eax, dword ptr [eax + 0x464]
0x4857D5: call     0x4579f0
0x4857DA: mov      edx, 0x15
0x4857DF: mov      eax, dword ptr [ebp - 4]
0x4857E2: mov      eax, dword ptr [eax + 0x46c]
0x4857E8: call     0x42718c
0x4857ED: mov      edx, 0x38
0x4857F2: mov      eax, dword ptr [ebp - 4]
0x4857F5: mov      eax, dword ptr [eax + 0x46c]
0x4857FB: call     0x4271b0
0x485800: mov      edx, 0xb4
0x485805: mov      eax, dword ptr [ebp - 4]
0x485808: mov      eax, dword ptr [eax + 0x46c]
0x48580E: call     0x4271d8
0x485813: mov      edx, 0x42
0x485818: mov      eax, dword ptr [ebp - 4]
0x48581B: mov      eax, dword ptr [eax + 0x46c]
0x485821: call     0x4271fc
0x485826: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48582B: mov      eax, dword ptr [eax]
0x48582D: mov      edx, dword ptr [eax + 0x308]
0x485833: mov      ecx, 0x187
0x485838: mov      eax, dword ptr [ebp - 4]
0x48583B: mov      eax, dword ptr [eax + 0x470]
0x485841: call     0x4579f0
0x485846: mov      edx, 0x9b
0x48584B: mov      eax, dword ptr [ebp - 4]
0x48584E: mov      eax, dword ptr [eax + 0x470]
0x485854: call     0x42718c
0x485859: mov      edx, 0x80
0x48585E: mov      eax, dword ptr [ebp - 4]
0x485861: mov      eax, dword ptr [eax + 0x470]
0x485867: call     0x4271b0
0x48586C: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485871: mov      eax, dword ptr [eax]
0x485873: mov      edx, dword ptr [eax + 0x308]
0x485879: mov      ecx, 0x40
0x48587E: mov      eax, dword ptr [ebp - 4]
0x485881: mov      eax, dword ptr [eax + 0x474]
0x485887: call     0x4579f0
0x48588C: mov      edx, 0xdc
0x485891: mov      eax, dword ptr [ebp - 4]
0x485894: mov      eax, dword ptr [eax + 0x474]
0x48589A: call     0x42718c
0x48589F: mov      edx, 0x2a
0x4858A4: mov      eax, dword ptr [ebp - 4]
0x4858A7: mov      eax, dword ptr [eax + 0x474]
0x4858AD: call     0x4271b0
0x4858B2: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4858B7: mov      eax, dword ptr [eax]
0x4858B9: mov      edx, dword ptr [eax + 0x308]
0x4858BF: mov      ecx, 0x1c
0x4858C4: mov      eax, dword ptr [ebp - 4]
0x4858C7: mov      eax, dword ptr [eax + 0x478]
0x4858CD: call     0x4579f0
0x4858D2: mov      edx, 0xb
0x4858D7: mov      eax, dword ptr [ebp - 4]
0x4858DA: mov      eax, dword ptr [eax + 0x478]
0x4858E0: call     0x42718c
0x4858E5: mov      edx, 0x89
0x4858EA: mov      eax, dword ptr [ebp - 4]
0x4858ED: mov      eax, dword ptr [eax + 0x478]
0x4858F3: call     0x4271b0
0x4858F8: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4858FD: mov      eax, dword ptr [eax]
0x4858FF: mov      eax, dword ptr [eax + 0x308]
0x485905: mov      edx, 0x186
0x48590A: call     0x45351c
0x48590F: mov      dword ptr [ebp - 0x10], eax
0x485912: cmp      dword ptr [ebp - 0x10], 0
0x485916: je       0x48596c
0x485918: mov      eax, dword ptr [ebp - 0x10]
0x48591B: call     0x44dc60
0x485920: mov      edx, dword ptr [ebp - 4]
0x485923: mov      edx, dword ptr [edx + 0x464]
0x485929: mov      edx, dword ptr [edx + 0x30]
0x48592C: sub      edx, eax
0x48592E: mov      eax, dword ptr [ebp - 4]
0x485931: mov      eax, dword ptr [eax + 0x460]
0x485937: call     0x42718c
0x48593C: xor      edx, edx
0x48593E: mov      eax, dword ptr [ebp - 4]
0x485941: mov      eax, dword ptr [eax + 0x460]
0x485947: call     0x4271b0
0x48594C: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485951: mov      eax, dword ptr [eax]
0x485953: mov      edx, dword ptr [eax + 0x308]
0x485959: mov      ecx, 0x186
0x48595E: mov      eax, dword ptr [ebp - 4]
0x485961: mov      eax, dword ptr [eax + 0x460]
0x485967: call     0x4579f0
0x48596C: mov      edx, 0x15
0x485971: mov      eax, dword ptr [ebp - 4]
0x485974: mov      eax, dword ptr [eax + 0x468]
0x48597A: call     0x42718c
0x48597F: mov      edx, 0x38
0x485984: mov      eax, dword ptr [ebp - 4]
0x485987: mov      eax, dword ptr [eax + 0x468]
0x48598D: call     0x4271b0
0x485992: mov      edx, 0xb4
0x485997: mov      eax, dword ptr [ebp - 4]
0x48599A: mov      eax, dword ptr [eax + 0x468]
0x4859A0: call     0x4271d8
0x4859A5: mov      edx, 0x42
0x4859AA: mov      eax, dword ptr [ebp - 4]
0x4859AD: mov      eax, dword ptr [eax + 0x468]
0x4859B3: call     0x4271fc
0x4859B8: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4859BD: mov      eax, dword ptr [eax]
0x4859BF: mov      edx, dword ptr [eax + 0x308]
0x4859C5: mov      ecx, 0x1c
0x4859CA: mov      eax, dword ptr [ebp - 4]
0x4859CD: mov      eax, dword ptr [eax + 0x47c]
0x4859D3: call     0x4579f0
0x4859D8: mov      edx, 0xb
0x4859DD: mov      eax, dword ptr [ebp - 4]
0x4859E0: mov      eax, dword ptr [eax + 0x47c]
0x4859E6: call     0x42718c
0x4859EB: mov      edx, 0x89
0x4859F0: mov      eax, dword ptr [ebp - 4]
0x4859F3: mov      eax, dword ptr [eax + 0x47c]
0x4859F9: call     0x4271b0
0x4859FE: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485A03: mov      eax, dword ptr [eax]
0x485A05: mov      eax, dword ptr [eax + 0x308]
0x485A0B: mov      edx, 0xb4
0x485A10: call     0x45351c
0x485A15: mov      dword ptr [ebp - 0x10], eax
0x485A18: cmp      dword ptr [ebp - 0x10], 0
0x485A1C: je       0x485a5e
0x485A1E: xor      edx, edx
0x485A20: mov      eax, dword ptr [ebp - 4]
0x485A23: mov      eax, dword ptr [eax + 0x4c4]
0x485A29: call     0x42718c
0x485A2E: xor      edx, edx
0x485A30: mov      eax, dword ptr [ebp - 4]
0x485A33: mov      eax, dword ptr [eax + 0x4c4]
0x485A39: call     0x4271b0
0x485A3E: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485A43: mov      eax, dword ptr [eax]
0x485A45: mov      edx, dword ptr [eax + 0x308]
0x485A4B: mov      ecx, 0xb4
0x485A50: mov      eax, dword ptr [ebp - 4]
0x485A53: mov      eax, dword ptr [eax + 0x4c4]
0x485A59: call     0x4579f0
0x485A5E: mov      edx, 0x248
0x485A63: mov      eax, dword ptr [ebp - 4]
0x485A66: mov      eax, dword ptr [eax + 0x4fc]
0x485A6C: call     0x42718c
0x485A71: mov      edx, 6
0x485A76: mov      eax, dword ptr [ebp - 4]
0x485A79: mov      eax, dword ptr [eax + 0x4fc]
0x485A7F: call     0x4271b0
0x485A84: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485A89: mov      eax, dword ptr [eax]
0x485A8B: mov      edx, dword ptr [eax + 0x308]
0x485A91: mov      ecx, 0x40
0x485A96: mov      eax, dword ptr [ebp - 4]
0x485A99: mov      eax, dword ptr [eax + 0x4fc]
0x485A9F: call     0x4579f0
0x485AA4: mov      edx, 0xd
0x485AA9: mov      eax, dword ptr [ebp - 4]
0x485AAC: mov      eax, dword ptr [eax + 0x4c8]
0x485AB2: call     0x42718c
0x485AB7: mov      edx, 0x19b
0x485ABC: mov      eax, dword ptr [ebp - 4]
0x485ABF: mov      eax, dword ptr [eax + 0x4c8]
0x485AC5: call     0x4271b0
0x485ACA: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485ACF: mov      eax, dword ptr [eax]
0x485AD1: mov      edx, dword ptr [eax + 0x308]
0x485AD7: mov      ecx, 0xc6
0x485ADC: mov      eax, dword ptr [ebp - 4]
0x485ADF: mov      eax, dword ptr [eax + 0x4c8]
0x485AE5: call     0x4579f0
0x485AEA: mov      edx, 0xd
0x485AEF: mov      eax, dword ptr [ebp - 4]
0x485AF2: mov      eax, dword ptr [eax + 0x4cc]
0x485AF8: call     0x42718c
0x485AFD: mov      edx, 0x1ad
0x485B02: mov      eax, dword ptr [ebp - 4]
0x485B05: mov      eax, dword ptr [eax + 0x4cc]
0x485B0B: call     0x4271b0
0x485B10: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485B15: mov      eax, dword ptr [eax]
0x485B17: mov      edx, dword ptr [eax + 0x308]
0x485B1D: mov      ecx, 0xc8
0x485B22: mov      eax, dword ptr [ebp - 4]
0x485B25: mov      eax, dword ptr [eax + 0x4cc]
0x485B2B: call     0x4579f0
0x485B30: mov      edx, 0x5e
0x485B35: mov      eax, dword ptr [ebp - 4]
0x485B38: mov      eax, dword ptr [eax + 0x4d0]
0x485B3E: call     0x42718c
0x485B43: mov      edx, 0x1ad
0x485B48: mov      eax, dword ptr [ebp - 4]
0x485B4B: mov      eax, dword ptr [eax + 0x4d0]
0x485B51: call     0x4271b0
0x485B56: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485B5B: mov      eax, dword ptr [eax]
0x485B5D: mov      edx, dword ptr [eax + 0x308]
0x485B63: mov      ecx, 0xbe
0x485B68: mov      eax, dword ptr [ebp - 4]
0x485B6B: mov      eax, dword ptr [eax + 0x4d0]
0x485B71: call     0x4579f0
0x485B76: mov      edx, 0xf3
0x485B7B: mov      eax, dword ptr [ebp - 4]
0x485B7E: mov      eax, dword ptr [eax + 0x4d4]
0x485B84: call     0x42718c
0x485B89: mov      edx, 0x19b
0x485B8E: mov      eax, dword ptr [ebp - 4]
0x485B91: mov      eax, dword ptr [eax + 0x4d4]
0x485B97: call     0x4271b0
0x485B9C: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485BA1: mov      eax, dword ptr [eax]
0x485BA3: mov      edx, dword ptr [eax + 0x308]
0x485BA9: mov      ecx, 0xb6
0x485BAE: mov      eax, dword ptr [ebp - 4]
0x485BB1: mov      eax, dword ptr [eax + 0x4d4]
0x485BB7: call     0x4579f0
0x485BBC: mov      edx, 0xf3
0x485BC1: mov      eax, dword ptr [ebp - 4]
0x485BC4: mov      eax, dword ptr [eax + 0x4d8]
0x485BCA: call     0x42718c
0x485BCF: mov      edx, 0x1ad
0x485BD4: mov      eax, dword ptr [ebp - 4]
0x485BD7: mov      eax, dword ptr [eax + 0x4d8]
0x485BDD: call     0x4271b0
0x485BE2: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485BE7: mov      eax, dword ptr [eax]
0x485BE9: mov      edx, dword ptr [eax + 0x308]
0x485BEF: mov      ecx, 0xc0
0x485BF4: mov      eax, dword ptr [ebp - 4]
0x485BF7: mov      eax, dword ptr [eax + 0x4d8]
0x485BFD: call     0x4579f0
0x485C02: mov      edx, 0x145
0x485C07: mov      eax, dword ptr [ebp - 4]
0x485C0A: mov      eax, dword ptr [eax + 0x4dc]
0x485C10: call     0x42718c
0x485C15: mov      edx, 0x19b
0x485C1A: mov      eax, dword ptr [ebp - 4]
0x485C1D: mov      eax, dword ptr [eax + 0x4dc]
0x485C23: call     0x4271b0
0x485C28: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485C2D: mov      eax, dword ptr [eax]
0x485C2F: mov      edx, dword ptr [eax + 0x308]
0x485C35: mov      ecx, 0xc4
0x485C3A: mov      eax, dword ptr [ebp - 4]
0x485C3D: mov      eax, dword ptr [eax + 0x4dc]
0x485C43: call     0x4579f0
0x485C48: mov      edx, 0x145
0x485C4D: mov      eax, dword ptr [ebp - 4]
0x485C50: mov      eax, dword ptr [eax + 0x4e0]
0x485C56: call     0x42718c
0x485C5B: mov      edx, 0x1ad
0x485C60: mov      eax, dword ptr [ebp - 4]
0x485C63: mov      eax, dword ptr [eax + 0x4e0]
0x485C69: call     0x4271b0
0x485C6E: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485C73: mov      eax, dword ptr [eax]
0x485C75: mov      edx, dword ptr [eax + 0x308]
0x485C7B: mov      ecx, 0xc2
0x485C80: mov      eax, dword ptr [ebp - 4]
0x485C83: mov      eax, dword ptr [eax + 0x4e0]
0x485C89: call     0x4579f0
0x485C8E: mov      edx, 0x197
0x485C93: mov      eax, dword ptr [ebp - 4]
0x485C96: mov      eax, dword ptr [eax + 0x4e4]
0x485C9C: call     0x42718c
0x485CA1: mov      edx, 0x19b
0x485CA6: mov      eax, dword ptr [ebp - 4]
0x485CA9: mov      eax, dword ptr [eax + 0x4e4]
0x485CAF: call     0x4271b0
0x485CB4: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485CB9: mov      eax, dword ptr [eax]
0x485CBB: mov      edx, dword ptr [eax + 0x308]
0x485CC1: mov      ecx, 0xb8
0x485CC6: mov      eax, dword ptr [ebp - 4]
0x485CC9: mov      eax, dword ptr [eax + 0x4e4]
0x485CCF: call     0x4579f0
0x485CD4: mov      edx, 0x197
0x485CD9: mov      eax, dword ptr [ebp - 4]
0x485CDC: mov      eax, dword ptr [eax + 0x4e8]
0x485CE2: call     0x42718c
0x485CE7: mov      edx, 0x1ad
0x485CEC: mov      eax, dword ptr [ebp - 4]
0x485CEF: mov      eax, dword ptr [eax + 0x4e8]
0x485CF5: call     0x4271b0
0x485CFA: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485CFF: mov      eax, dword ptr [eax]
0x485D01: mov      edx, dword ptr [eax + 0x308]
0x485D07: mov      ecx, 0xba
0x485D0C: mov      eax, dword ptr [ebp - 4]
0x485D0F: mov      eax, dword ptr [eax + 0x4e8]
0x485D15: call     0x4579f0
0x485D1A: mov      edx, 0x211
0x485D1F: mov      eax, dword ptr [ebp - 4]
0x485D22: mov      eax, dword ptr [eax + 0x4ec]
0x485D28: call     0x42718c
0x485D2D: mov      edx, 0x19b
0x485D32: mov      eax, dword ptr [ebp - 4]
0x485D35: mov      eax, dword ptr [eax + 0x4ec]
0x485D3B: call     0x4271b0
0x485D40: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485D45: mov      eax, dword ptr [eax]
0x485D47: mov      edx, dword ptr [eax + 0x308]
0x485D4D: mov      ecx, 0xca
0x485D52: mov      eax, dword ptr [ebp - 4]
0x485D55: mov      eax, dword ptr [eax + 0x4ec]
0x485D5B: call     0x4579f0
0x485D60: mov      edx, 0x211
0x485D65: mov      eax, dword ptr [ebp - 4]
0x485D68: mov      eax, dword ptr [eax + 0x4f0]
0x485D6E: call     0x42718c
0x485D73: mov      edx, 0x1ad
0x485D78: mov      eax, dword ptr [ebp - 4]
0x485D7B: mov      eax, dword ptr [eax + 0x4f0]
0x485D81: call     0x4271b0
0x485D86: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485D8B: mov      eax, dword ptr [eax]
0x485D8D: mov      edx, dword ptr [eax + 0x308]
0x485D93: mov      ecx, 0xbc
0x485D98: mov      eax, dword ptr [ebp - 4]
0x485D9B: mov      eax, dword ptr [eax + 0x4f0]
0x485DA1: call     0x4579f0
0x485DA6: mov      edx, 0x253
0x485DAB: mov      eax, dword ptr [ebp - 4]
0x485DAE: mov      eax, dword ptr [eax + 0x4f4]
0x485DB4: call     0x42718c
0x485DB9: mov      edx, 0xef
0x485DBE: mov      eax, dword ptr [ebp - 4]
0x485DC1: mov      eax, dword ptr [eax + 0x4f4]
0x485DC7: call     0x4271b0
0x485DCC: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485DD1: mov      eax, dword ptr [eax]
0x485DD3: mov      edx, dword ptr [eax + 0x308]
0x485DD9: mov      ecx, 0x175
0x485DDE: mov      eax, dword ptr [ebp - 4]
0x485DE1: mov      eax, dword ptr [eax + 0x4f4]
0x485DE7: call     0x4579f0
0x485DEC: mov      edx, 0x253
0x485DF1: mov      eax, dword ptr [ebp - 4]
0x485DF4: mov      eax, dword ptr [eax + 0x4f8]
0x485DFA: call     0x42718c
0x485DFF: mov      edx, 0x123
0x485E04: mov      eax, dword ptr [ebp - 4]
0x485E07: mov      eax, dword ptr [eax + 0x4f8]
0x485E0D: call     0x4271b0
0x485E12: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485E17: mov      eax, dword ptr [eax]
0x485E19: mov      edx, dword ptr [eax + 0x308]
0x485E1F: mov      ecx, 0x174
0x485E24: mov      eax, dword ptr [ebp - 4]
0x485E27: mov      eax, dword ptr [eax + 0x4f8]
0x485E2D: call     0x4579f0
0x485E32: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485E37: mov      eax, dword ptr [eax]
0x485E39: mov      edx, dword ptr [eax + 0x308]
0x485E3F: mov      ecx, 0xcc
0x485E44: mov      eax, dword ptr [ebp - 4]
0x485E47: mov      eax, dword ptr [eax + 0x500]
0x485E4D: call     0x4579f0
0x485E52: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485E57: mov      eax, dword ptr [eax]
0x485E59: mov      edx, dword ptr [eax + 0x308]
0x485E5F: mov      ecx, 0x169
0x485E64: mov      eax, dword ptr [ebp - 4]
0x485E67: mov      eax, dword ptr [eax + 0x508]
0x485E6D: call     0x4579f0
0x485E72: mov      edx, 0x202
0x485E77: mov      eax, dword ptr [ebp - 4]
0x485E7A: mov      eax, dword ptr [eax + 0x508]
0x485E80: call     0x42718c
0x485E85: mov      edx, 0x11f
0x485E8A: mov      eax, dword ptr [ebp - 4]
0x485E8D: mov      eax, dword ptr [eax + 0x508]
0x485E93: call     0x4271b0
0x485E98: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485E9D: mov      eax, dword ptr [eax]
0x485E9F: mov      edx, dword ptr [eax + 0x308]
0x485EA5: mov      ecx, 0x40
0x485EAA: mov      eax, dword ptr [ebp - 4]
0x485EAD: mov      eax, dword ptr [eax + 0x504]
0x485EB3: call     0x4579f0
0x485EB8: mov      edx, 0x248
0x485EBD: mov      eax, dword ptr [ebp - 4]
0x485EC0: mov      eax, dword ptr [eax + 0x504]
0x485EC6: call     0x42718c
0x485ECB: mov      edx, 6
0x485ED0: mov      eax, dword ptr [ebp - 4]
0x485ED3: mov      eax, dword ptr [eax + 0x504]
0x485ED9: call     0x4271b0
0x485EDE: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485EE3: mov      eax, dword ptr [eax]
0x485EE5: mov      edx, dword ptr [eax + 0x308]
0x485EEB: mov      ecx, 0xe2
0x485EF0: mov      eax, dword ptr [ebp - 4]
0x485EF3: mov      eax, dword ptr [eax + 0x518]
0x485EF9: call     0x4579f0
0x485EFE: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485F03: mov      eax, dword ptr [eax]
0x485F05: mov      edx, dword ptr [eax + 0x308]
0x485F0B: mov      ecx, 0x40
0x485F10: mov      eax, dword ptr [ebp - 4]
0x485F13: mov      eax, dword ptr [eax + 0x564]
0x485F19: call     0x4579f0
0x485F1E: mov      edx, 0x13c
0x485F23: mov      eax, dword ptr [ebp - 4]
0x485F26: mov      eax, dword ptr [eax + 0x564]
0x485F2C: call     0x42718c
0x485F31: mov      edx, 1
0x485F36: mov      eax, dword ptr [ebp - 4]
0x485F39: mov      eax, dword ptr [eax + 0x564]
0x485F3F: call     0x4271b0
0x485F44: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485F49: mov      eax, dword ptr [eax]
0x485F4B: mov      edx, dword ptr [eax + 0x308]
0x485F51: mov      ecx, 0x3e
0x485F56: mov      eax, dword ptr [ebp - 4]
0x485F59: mov      eax, dword ptr [eax + 0x568]
0x485F5F: call     0x4579f0
0x485F64: mov      edx, 0xdc
0x485F69: mov      eax, dword ptr [ebp - 4]
0x485F6C: mov      eax, dword ptr [eax + 0x568]
0x485F72: call     0x42718c
0x485F77: mov      edx, 0x12a
0x485F7C: mov      eax, dword ptr [ebp - 4]
0x485F7F: mov      eax, dword ptr [eax + 0x568]
0x485F85: call     0x4271b0
0x485F8A: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485F8F: mov      eax, dword ptr [eax]
0x485F91: mov      edx, dword ptr [eax + 0x308]
0x485F97: mov      ecx, 0xe3
0x485F9C: mov      eax, dword ptr [ebp - 4]
0x485F9F: mov      eax, dword ptr [eax + 0x51c]
0x485FA5: call     0x4579f0
0x485FAA: mov      edx, 0xd9
0x485FAF: mov      eax, dword ptr [ebp - 4]
0x485FB2: mov      eax, dword ptr [eax + 0x51c]
0x485FB8: call     0x42718c
0x485FBD: mov      edx, 0x65
0x485FC2: mov      eax, dword ptr [ebp - 4]
0x485FC5: mov      eax, dword ptr [eax + 0x51c]
0x485FCB: call     0x4271b0
0x485FD0: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x485FD5: mov      eax, dword ptr [eax]
0x485FD7: mov      edx, dword ptr [eax + 0x308]
0x485FDD: mov      ecx, 0xe3
0x485FE2: mov      eax, dword ptr [ebp - 4]
0x485FE5: mov      eax, dword ptr [eax + 0x520]
0x485FEB: call     0x4579f0
0x485FF0: mov      edx, 0xd9
0x485FF5: mov      eax, dword ptr [ebp - 4]
0x485FF8: mov      eax, dword ptr [eax + 0x520]
0x485FFE: call     0x42718c
0x486003: mov      edx, 0x79
0x486008: mov      eax, dword ptr [ebp - 4]
0x48600B: mov      eax, dword ptr [eax + 0x520]
0x486011: call     0x4271b0
0x486016: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48601B: mov      eax, dword ptr [eax]
0x48601D: mov      edx, dword ptr [eax + 0x308]
0x486023: mov      ecx, 0xe3
0x486028: mov      eax, dword ptr [ebp - 4]
0x48602B: mov      eax, dword ptr [eax + 0x524]
0x486031: call     0x4579f0
0x486036: mov      edx, 0xd9
0x48603B: mov      eax, dword ptr [ebp - 4]
0x48603E: mov      eax, dword ptr [eax + 0x524]
0x486044: call     0x42718c
0x486049: mov      edx, 0x8c
0x48604E: mov      eax, dword ptr [ebp - 4]
0x486051: mov      eax, dword ptr [eax + 0x524]
0x486057: call     0x4271b0
0x48605C: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x486061: mov      eax, dword ptr [eax]
0x486063: mov      edx, dword ptr [eax + 0x308]
0x486069: mov      ecx, 0xe3
0x48606E: mov      eax, dword ptr [ebp - 4]
0x486071: mov      eax, dword ptr [eax + 0x528]
0x486077: call     0x4579f0
0x48607C: mov      edx, 0xd9
0x486081: mov      eax, dword ptr [ebp - 4]
0x486084: mov      eax, dword ptr [eax + 0x528]
0x48608A: call     0x42718c
0x48608F: mov      edx, 0xa0
0x486094: mov      eax, dword ptr [ebp - 4]
0x486097: mov      eax, dword ptr [eax + 0x528]
0x48609D: call     0x4271b0
0x4860A2: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4860A7: mov      eax, dword ptr [eax]
0x4860A9: mov      edx, dword ptr [eax + 0x308]
0x4860AF: mov      ecx, 0xe3
0x4860B4: mov      eax, dword ptr [ebp - 4]
0x4860B7: mov      eax, dword ptr [eax + 0x52c]
0x4860BD: call     0x4579f0
0x4860C2: mov      edx, 0xd9
0x4860C7: mov      eax, dword ptr [ebp - 4]
0x4860CA: mov      eax, dword ptr [eax + 0x52c]
0x4860D0: call     0x42718c
0x4860D5: mov      edx, 0xb5
0x4860DA: mov      eax, dword ptr [ebp - 4]
0x4860DD: mov      eax, dword ptr [eax + 0x52c]
0x4860E3: call     0x4271b0
0x4860E8: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4860ED: mov      eax, dword ptr [eax]
0x4860EF: mov      edx, dword ptr [eax + 0x308]
0x4860F5: mov      ecx, 0xe3
0x4860FA: mov      eax, dword ptr [ebp - 4]
0x4860FD: mov      eax, dword ptr [eax + 0x530]
0x486103: call     0x4579f0
0x486108: mov      edx, 0xd9
0x48610D: mov      eax, dword ptr [ebp - 4]
0x486110: mov      eax, dword ptr [eax + 0x530]
0x486116: call     0x42718c
0x48611B: mov      edx, 0xc9
0x486120: mov      eax, dword ptr [ebp - 4]
0x486123: mov      eax, dword ptr [eax + 0x530]
0x486129: call     0x4271b0
0x48612E: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x486133: mov      eax, dword ptr [eax]
0x486135: mov      edx, dword ptr [eax + 0x308]
0x48613B: mov      ecx, 0xe3
0x486140: mov      eax, dword ptr [ebp - 4]
0x486143: mov      eax, dword ptr [eax + 0x534]
0x486149: call     0x4579f0
0x48614E: mov      edx, 0xd9
0x486153: mov      eax, dword ptr [ebp - 4]
0x486156: mov      eax, dword ptr [eax + 0x534]
0x48615C: call     0x42718c
0x486161: mov      edx, 0xdc
0x486166: mov      eax, dword ptr [ebp - 4]
0x486169: mov      eax, dword ptr [eax + 0x534]
0x48616F: call     0x4271b0
0x486174: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x486179: mov      eax, dword ptr [eax]
0x48617B: mov      edx, dword ptr [eax + 0x308]
0x486181: mov      ecx, 0xe3
0x486186: mov      eax, dword ptr [ebp - 4]
0x486189: mov      eax, dword ptr [eax + 0x538]
0x48618F: call     0x4579f0
0x486194: mov      edx, 0xd9
0x486199: mov      eax, dword ptr [ebp - 4]
0x48619C: mov      eax, dword ptr [eax + 0x538]
0x4861A2: call     0x42718c
0x4861A7: mov      edx, 0xf0
0x4861AC: mov      eax, dword ptr [ebp - 4]
0x4861AF: mov      eax, dword ptr [eax + 0x538]
0x4861B5: call     0x4271b0
0x4861BA: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4861BF: mov      eax, dword ptr [eax]
0x4861C1: mov      edx, dword ptr [eax + 0x308]
0x4861C7: mov      ecx, 0xe3
0x4861CC: mov      eax, dword ptr [ebp - 4]
0x4861CF: mov      eax, dword ptr [eax + 0x53c]
0x4861D5: call     0x4579f0
0x4861DA: mov      edx, 0xd9
0x4861DF: mov      eax, dword ptr [ebp - 4]
0x4861E2: mov      eax, dword ptr [eax + 0x53c]
0x4861E8: call     0x42718c
0x4861ED: mov      edx, 0x105
0x4861F2: mov      eax, dword ptr [ebp - 4]
0x4861F5: mov      eax, dword ptr [eax + 0x53c]
0x4861FB: call     0x4271b0
0x486200: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x486205: mov      eax, dword ptr [eax]
0x486207: mov      edx, dword ptr [eax + 0x308]
0x48620D: mov      ecx, 0xe4
0x486212: mov      eax, dword ptr [ebp - 4]
0x486215: mov      eax, dword ptr [eax + 0x540]
0x48621B: call     0x4579f0
0x486220: mov      edx, 0xe3
0x486225: mov      eax, dword ptr [ebp - 4]
0x486228: mov      eax, dword ptr [eax + 0x540]
0x48622E: call     0x42718c
0x486233: mov      edx, 0x65
0x486238: mov      eax, dword ptr [ebp - 4]
0x48623B: mov      eax, dword ptr [eax + 0x540]
0x486241: call     0x4271b0
0x486246: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48624B: mov      eax, dword ptr [eax]
0x48624D: mov      edx, dword ptr [eax + 0x308]
0x486253: mov      ecx, 0xe4
0x486258: mov      eax, dword ptr [ebp - 4]
0x48625B: mov      eax, dword ptr [eax + 0x544]
0x486261: call     0x4579f0
0x486266: mov      edx, 0xe3
0x48626B: mov      eax, dword ptr [ebp - 4]
0x48626E: mov      eax, dword ptr [eax + 0x544]
0x486274: call     0x42718c
0x486279: mov      edx, 0x79
0x48627E: mov      eax, dword ptr [ebp - 4]
0x486281: mov      eax, dword ptr [eax + 0x544]
0x486287: call     0x4271b0
0x48628C: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x486291: mov      eax, dword ptr [eax]
0x486293: mov      edx, dword ptr [eax + 0x308]
0x486299: mov      ecx, 0xe4
0x48629E: mov      eax, dword ptr [ebp - 4]
0x4862A1: mov      eax, dword ptr [eax + 0x548]
0x4862A7: call     0x4579f0
0x4862AC: mov      edx, 0xe3
0x4862B1: mov      eax, dword ptr [ebp - 4]
0x4862B4: mov      eax, dword ptr [eax + 0x548]
0x4862BA: call     0x42718c
0x4862BF: mov      edx, 0x8c
0x4862C4: mov      eax, dword ptr [ebp - 4]
0x4862C7: mov      eax, dword ptr [eax + 0x548]
0x4862CD: call     0x4271b0
0x4862D2: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4862D7: mov      eax, dword ptr [eax]
0x4862D9: mov      edx, dword ptr [eax + 0x308]
0x4862DF: mov      ecx, 0xe4
0x4862E4: mov      eax, dword ptr [ebp - 4]
0x4862E7: mov      eax, dword ptr [eax + 0x54c]
0x4862ED: call     0x4579f0
0x4862F2: mov      edx, 0xe3
0x4862F7: mov      eax, dword ptr [ebp - 4]
0x4862FA: mov      eax, dword ptr [eax + 0x54c]
0x486300: call     0x42718c
0x486305: mov      edx, 0xa0
0x48630A: mov      eax, dword ptr [ebp - 4]
0x48630D: mov      eax, dword ptr [eax + 0x54c]
0x486313: call     0x4271b0
0x486318: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48631D: mov      eax, dword ptr [eax]
0x48631F: mov      edx, dword ptr [eax + 0x308]
0x486325: mov      ecx, 0xe4
0x48632A: mov      eax, dword ptr [ebp - 4]
0x48632D: mov      eax, dword ptr [eax + 0x550]
0x486333: call     0x4579f0
0x486338: mov      edx, 0xe3
0x48633D: mov      eax, dword ptr [ebp - 4]
0x486340: mov      eax, dword ptr [eax + 0x550]
0x486346: call     0x42718c
0x48634B: mov      edx, 0xb5
0x486350: mov      eax, dword ptr [ebp - 4]
0x486353: mov      eax, dword ptr [eax + 0x550]
0x486359: call     0x4271b0
0x48635E: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x486363: mov      eax, dword ptr [eax]
0x486365: mov      edx, dword ptr [eax + 0x308]
0x48636B: mov      ecx, 0xe4
0x486370: mov      eax, dword ptr [ebp - 4]
0x486373: mov      eax, dword ptr [eax + 0x558]
0x486379: call     0x4579f0
0x48637E: mov      edx, 0xe3
0x486383: mov      eax, dword ptr [ebp - 4]
0x486386: mov      eax, dword ptr [eax + 0x558]
0x48638C: call     0x42718c
0x486391: mov      edx, 0xc9
0x486396: mov      eax, dword ptr [ebp - 4]
0x486399: mov      eax, dword ptr [eax + 0x558]
0x48639F: call     0x4271b0
0x4863A4: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4863A9: mov      eax, dword ptr [eax]
0x4863AB: mov      edx, dword ptr [eax + 0x308]
0x4863B1: mov      ecx, 0xe4
0x4863B6: mov      eax, dword ptr [ebp - 4]
0x4863B9: mov      eax, dword ptr [eax + 0x554]
0x4863BF: call     0x4579f0
0x4863C4: mov      edx, 0xe3
0x4863C9: mov      eax, dword ptr [ebp - 4]
0x4863CC: mov      eax, dword ptr [eax + 0x554]
0x4863D2: call     0x42718c
0x4863D7: mov      edx, 0xdc
0x4863DC: mov      eax, dword ptr [ebp - 4]
0x4863DF: mov      eax, dword ptr [eax + 0x554]
0x4863E5: call     0x4271b0
0x4863EA: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4863EF: mov      eax, dword ptr [eax]
0x4863F1: mov      edx, dword ptr [eax + 0x308]
0x4863F7: mov      ecx, 0xe4
0x4863FC: mov      eax, dword ptr [ebp - 4]
0x4863FF: mov      eax, dword ptr [eax + 0x55c]
0x486405: call     0x4579f0
0x48640A: mov      edx, 0xe3
0x48640F: mov      eax, dword ptr [ebp - 4]
0x486412: mov      eax, dword ptr [eax + 0x55c]
0x486418: call     0x42718c
0x48641D: mov      edx, 0xf0
0x486422: mov      eax, dword ptr [ebp - 4]
0x486425: mov      eax, dword ptr [eax + 0x55c]
0x48642B: call     0x4271b0
0x486430: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x486435: mov      eax, dword ptr [eax]
0x486437: mov      edx, dword ptr [eax + 0x308]
0x48643D: mov      ecx, 0xe4
0x486442: mov      eax, dword ptr [ebp - 4]
0x486445: mov      eax, dword ptr [eax + 0x560]
0x48644B: call     0x4579f0
0x486450: mov      edx, 0xe3
0x486455: mov      eax, dword ptr [ebp - 4]
0x486458: mov      eax, dword ptr [eax + 0x560]
0x48645E: call     0x42718c
0x486463: mov      edx, 0x105
0x486468: mov      eax, dword ptr [ebp - 4]
0x48646B: mov      eax, dword ptr [eax + 0x560]
0x486471: call     0x4271b0
0x486476: mov      esp, ebp
0x486478: pop      ebp
0x486479: ret      
0x48647A: mov      eax, eax
0x48647C: push     ebp
0x48647D: mov      ebp, esp
0x48647F: push     ecx
0x486480: mov      dword ptr [ebp - 4], eax
0x486483: mov      eax, dword ptr [ebp - 4]
0x486486: mov      eax, dword ptr [eax + 0x2c4]
0x48648C: mov      dl, byte ptr [eax + 0x24c]
0x486492: xor      dl, 1
0x486495: mov      eax, dword ptr [ebp - 4]
0x486498: mov      eax, dword ptr [eax + 0x2c4]
0x48649E: call     0x458214
0x4864A3: mov      eax, dword ptr [ebp - 4]
0x4864A6: call     0x48a834
0x4864AB: pop      ecx
0x4864AC: pop      ebp
0x4864AD: ret      
0x4864AE: mov      eax, eax
0x4864B0: push     ebp
0x4864B1: mov      ebp, esp
0x4864B3: add      esp, 0xfffffdf0
0x4864B9: push     esi
0x4864BA: push     edi
0x4864BB: mov      esi, edx
0x4864BD: lea      edi, [ebp - 0x210]
0x4864C3: mov      ecx, 0x83
0x4864C8: rep movsd dword ptr es:[edi], dword ptr [esi]
0x4864CA: mov      dword ptr [ebp - 4], eax
0x4864CD: mov      eax, dword ptr [ebp - 4]
0x4864D0: lea      esi, [ebp - 0x210]
0x4864D6: lea      edi, [eax + 0x788]
0x4864DC: mov      ecx, 0x83
0x4864E1: rep movsd dword ptr es:[edi], dword ptr [esi]
0x4864E3: mov      dl, 1
0x4864E5: mov      eax, dword ptr [ebp - 4]
0x4864E8: mov      eax, dword ptr [eax + 0x490]
0x4864EE: call     0x458214
0x4864F3: pop      edi
0x4864F4: pop      esi
0x4864F5: mov      esp, ebp
0x4864F7: pop      ebp
0x4864F8: ret      
0x4864F9: lea      eax, [eax]
0x4864FC: push     ebp
0x4864FD: mov      ebp, esp
0x4864FF: push     ecx
0x486500: mov      dword ptr [ebp - 4], eax
0x486503: mov      eax, dword ptr [ebp - 4]
0x486506: mov      eax, dword ptr [eax + 0x2cc]
0x48650C: mov      dl, byte ptr [eax + 0x24c]
0x486512: xor      dl, 1
0x486515: mov      eax, dword ptr [ebp - 4]
0x486518: mov      eax, dword ptr [eax + 0x2cc]
0x48651E: call     0x458214
0x486523: mov      eax, dword ptr [ebp - 4]
0x486526: mov      eax, dword ptr [eax + 0x2cc]
0x48652C: cmp      byte ptr [eax + 0x24c], 0
0x486533: je       0x48653a
0x486535: call     0x49faa4
0x48653A: pop      ecx
0x48653B: pop      ebp
0x48653C: ret      
0x48653D: lea      eax, [eax]
0x486540: push     ebp
0x486541: mov      ebp, esp
0x486543: add      esp, -8
0x486546: mov      byte ptr [ebp - 5], dl
0x486549: mov      dword ptr [ebp - 4], eax
0x48654C: mov      dl, byte ptr [ebp - 5]
0x48654F: mov      eax, dword ptr [ebp - 4]
0x486552: mov      eax, dword ptr [eax + 0x2d0]
0x486558: call     0x458214
0x48655D: pop      ecx
0x48655E: pop      ecx
0x48655F: pop      ebp
0x486560: ret      
0x486561: lea      eax, [eax]
0x486564: push     ebp
0x486565: mov      ebp, esp
0x486567: add      esp, -0xc
0x48656A: push     esi
0x48656B: push     edi
0x48656C: mov      dword ptr [ebp - 4], eax
0x48656F: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x486574: cmp      byte ptr [eax], 0
0x486577: je       0x48662d
0x48657D: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x486582: mov      byte ptr [eax], 0
0x486585: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48658A: mov      eax, dword ptr [eax]
0x48658C: mov      dword ptr [ebp - 8], eax
0x48658F: cmp      dword ptr [ebp - 8], 0
0x486593: jge      0x4865e0
0x486595: cmp      dword ptr [ebp - 8], -0x14
0x486599: jg       0x4865b0
0x48659B: cmp      dword ptr [ebp - 8], -0x1e
0x48659F: jle      0x4865b0
0x4865A1: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x4865A6: add      eax, 4
0x4865A9: call     0x49fde4
0x4865AE: jmp      0x486624
0x4865B0: mov      eax, dword ptr [ebp - 8]
0x4865B3: inc      eax
0x4865B4: neg      eax
0x4865B6: mov      dword ptr [ebp - 0xc], eax
0x4865B9: mov      eax, dword ptr [ebp - 0xc]
0x4865BC: sub      eax, 9
0x4865BF: jae      0x486624
0x4865C1: imul     eax, dword ptr [ebp - 0xc], 0xd
0x4865C5: mov      edx, dword ptr [0x4a3db0] ; *0x4A3DB0->0x4F7EFC
0x4865CB: mov      ecx, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x4865D1: lea      edi, [edx + eax*4]
0x4865D4: lea      esi, [ecx + 4]
0x4865D7: mov      ecx, 0xd
0x4865DC: rep movsd dword ptr es:[edi], dword ptr [esi]
0x4865DE: jmp      0x486624
0x4865E0: mov      eax, dword ptr [ebp - 8]
0x4865E3: sub      eax, 0x2e
0x4865E6: jae      0x486624
0x4865E8: imul     eax, dword ptr [ebp - 8], 0xd
0x4865EC: mov      edx, dword ptr [0x4a4264] ; *0x4A4264->0x4F80D0
0x4865F2: cmp      byte ptr [edx + eax*4], 0
0x4865F6: jne      0x486617
0x4865F8: imul     eax, dword ptr [ebp - 8], 0xd
0x4865FC: mov      edx, dword ptr [0x4a4264] ; *0x4A4264->0x4F80D0
0x486602: mov      ecx, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x486608: lea      edi, [edx + eax*4]
0x48660B: lea      esi, [ecx + 4]
0x48660E: mov      ecx, 0xd
0x486613: rep movsd dword ptr es:[edi], dword ptr [esi]
0x486615: jmp      0x486624
0x486617: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48661C: add      eax, 4
0x48661F: call     0x49f874
0x486624: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x486629: mov      byte ptr [eax + 4], 0
0x48662D: call     0x49faa4
0x486632: pop      edi
0x486633: pop      esi
0x486634: mov      esp, ebp
0x486636: pop      ebp
0x486637: ret      
0x486638: push     ebp
0x486639: mov      ebp, esp
0x48663B: add      esp, -0xc
0x48663E: xor      edx, edx
0x486640: mov      dword ptr [ebp - 0xc], edx
0x486643: mov      dword ptr [ebp - 4], eax
0x486646: xor      eax, eax
0x486648: push     ebp
0x486649: push     0x4866c6
0x48664E: push     dword ptr fs:[eax]
0x486651: mov      dword ptr fs:[eax], esp
0x486654: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x486659: cmp      byte ptr [eax], 0
0x48665C: je       0x4866b0
0x48665E: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x486663: mov      byte ptr [eax], 0
0x486666: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48666B: cmp      byte ptr [eax + 4], 0
0x48666F: je       0x4866b0
0x486671: lea      eax, [ebp - 0xc]
0x486674: mov      edx, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48667A: add      edx, 4
0x48667D: call     0x403c78
0x486682: mov      edx, dword ptr [ebp - 0xc]
0x486685: mov      ecx, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48668B: mov      ecx, dword ptr [ecx + 0x30]
0x48668E: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x486693: mov      eax, dword ptr [eax]
0x486695: call     0x4658a8
0x48669A: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48669F: add      eax, 4
0x4866A2: call     0x49fc0c
0x4866A7: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x4866AC: mov      byte ptr [eax + 4], 0
0x4866B0: xor      eax, eax
0x4866B2: pop      edx
0x4866B3: pop      ecx
0x4866B4: pop      ecx
0x4866B5: mov      dword ptr fs:[eax], edx
0x4866B8: push     0x4866cd
0x4866BD: lea      eax, [ebp - 0xc]
0x4866C0: call     0x403a58
0x4866C5: ret      
0x4866C6: jmp      0x403518
0x4866CB: jmp      0x4866bd
0x4866CD: mov      esp, ebp
0x4866CF: pop      ebp
0x4866D0: ret      
0x4866D1: lea      eax, [eax]
0x4866D4: push     ebp
0x4866D5: mov      ebp, esp
0x4866D7: push     ecx
0x4866D8: mov      dword ptr [ebp - 4], eax
0x4866DB: xor      edx, edx
0x4866DD: mov      eax, dword ptr [ebp - 4]
0x4866E0: mov      eax, dword ptr [eax + 0x518]
0x4866E6: call     0x42718c
0x4866EB: xor      edx, edx
0x4866ED: mov      eax, dword ptr [ebp - 4]
0x4866F0: mov      eax, dword ptr [eax + 0x518]
0x4866F6: call     0x4271b0
0x4866FB: mov      eax, dword ptr [0x4a3fc0] ; *0x4A3FC0->0x4F927C
0x486700: mov      eax, dword ptr [eax]
0x486702: mov      edx, dword ptr [0x4a4038] ; *0x4A4038->0x4F9280
0x486708: mov      dword ptr [edx], eax
0x48670A: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x48670F: xor      ecx, ecx
0x486711: mov      edx, 0x14
0x486716: call     0x402b54
0x48671B: mov      dl, 1
0x48671D: mov      eax, dword ptr [ebp - 4]
0x486720: mov      eax, dword ptr [eax + 0x518]
0x486726: call     0x458214
0x48672B: pop      ecx
0x48672C: pop      ebp
0x48672D: ret      
0x48672E: mov      eax, eax
0x486730: push     ebp
0x486731: mov      ebp, esp
0x486733: add      esp, -0x18
0x486736: xor      ecx, ecx
0x486738: mov      dword ptr [ebp - 0x14], ecx
0x48673B: mov      dword ptr [ebp - 0xc], ecx
0x48673E: mov      dword ptr [ebp - 0x10], edx
0x486741: mov      dword ptr [ebp - 4], eax
0x486744: xor      eax, eax
0x486746: push     ebp
0x486747: push     0x486825
0x48674C: push     dword ptr fs:[eax]
0x48674F: mov      dword ptr fs:[eax], esp
0x486752: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x486757: cmp      byte ptr [eax], 0
0x48675A: je       0x486807
0x486760: mov      eax, dword ptr [ebp - 4]
0x486763: mov      eax, dword ptr [eax + 0x2c8]
0x486769: mov      byte ptr [eax + 0x25c], 1
0x486770: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x486775: add      eax, 4
0x486778: mov      edx, 0x486830
0x48677D: xor      ecx, ecx
0x48677F: mov      cl, byte ptr [eax]
0x486781: inc      ecx
0x486782: call     0x402ae4
0x486787: jne      0x4867f5
0x486789: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x48678E: mov      byte ptr [eax], 0
0x486791: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x486796: mov      byte ptr [eax + 4], 0
0x48679A: mov      eax, dword ptr [ebp - 4]
0x48679D: mov      dword ptr [eax + 0x620], 1
0x4867A7: mov      cx, word ptr [0x486838]
0x4867AE: mov      edx, 0x486844 ; "你想放下多少金币?"
0x4867B3: mov      eax, dword ptr [ebp - 4]
0x4867B6: call     0x486df0
0x4867BB: push     0
0x4867BD: lea      eax, [ebp - 0x14]
0x4867C0: push     eax
0x4867C1: mov      byte ptr [ebp - 0x18], 0x20
0x4867C5: lea      ecx, [ebp - 0x18]
0x4867C8: lea      edx, [ebp - 0xc]
0x4867CB: mov      eax, dword ptr [ebp - 4]
0x4867CE: mov      eax, dword ptr [eax + 0x784]
0x4867D4: call     0x452708
0x4867D9: xor      edx, edx
0x4867DB: mov      eax, dword ptr [ebp - 0xc]
0x4867DE: call     0x4520e8
0x4867E3: mov      dword ptr [ebp - 8], eax
0x4867E6: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4867EB: mov      eax, dword ptr [eax]
0x4867ED: mov      edx, dword ptr [ebp - 8]
0x4867F0: call     0x466444
0x4867F5: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x4867FA: cmp      dword ptr [eax], 0
0x4867FD: jl       0x486807
0x4867FF: mov      eax, dword ptr [ebp - 4]
0x486802: call     0x486638
0x486807: xor      eax, eax
0x486809: pop      edx
0x48680A: pop      ecx
0x48680B: pop      ecx
0x48680C: mov      dword ptr fs:[eax], edx
0x48680F: push     0x48682c
0x486814: lea      eax, [ebp - 0x14]
0x486817: call     0x403a58
0x48681C: lea      eax, [ebp - 0xc]
0x48681F: call     0x403a58
0x486824: ret      
0x486825: jmp      0x403518
0x48682A: jmp      0x486814
0x48682C: mov      esp, ebp
0x48682E: pop      ebp
0x48682F: ret      
0x486830: add      al, 0xbd
0x486833: mov      cl, 0xd2
0x486835: add      byte ptr [eax], al
0x486837: add      byte ptr [eax + eax], dl
0x48683A: add      byte ptr [eax], al
0x486844: [STR len=17] "你想放下多少金币?"
0x486858: push     ebp
0x486859: mov      ebp, esp
0x48685B: add      esp, -0xc
0x48685E: mov      byte ptr [ebp - 9], cl
0x486861: mov      dword ptr [ebp - 8], edx
0x486864: mov      dword ptr [ebp - 4], eax
0x486867: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x48686C: cmp      byte ptr [eax], 0
0x48686F: je       0x486881
0x486871: mov      eax, dword ptr [ebp - 4]
0x486874: mov      eax, dword ptr [eax + 0x2c8]
0x48687A: mov      byte ptr [eax + 0x25c], 1
0x486881: mov      esp, ebp
0x486883: pop      ebp
0x486884: ret      0xc
0x486887: nop      
0x486888: push     ebp
0x486889: mov      ebp, esp
0x48688B: add      esp, -0x1c
0x48688E: xor      ecx, ecx
0x486890: mov      dword ptr [ebp - 0x10], ecx
0x486893: mov      dword ptr [ebp - 0xc], ecx
0x486896: mov      dword ptr [ebp - 8], edx
0x486899: mov      dword ptr [ebp - 4], eax
0x48689C: mov      eax, dword ptr [ebp - 4]
0x48689F: call     0x403e88
0x4868A4: xor      eax, eax
0x4868A6: push     ebp
0x4868A7: push     0x486956
0x4868AC: push     dword ptr fs:[eax]
0x4868AF: mov      dword ptr fs:[eax], esp
0x4868B2: push     4
0x4868B4: lea      eax, [ebp - 0x10]
0x4868B7: push     eax
0x4868B8: mov      byte ptr [ebp - 0x18], 0x28
0x4868BC: mov      byte ptr [ebp - 0x17], 0x21
0x4868C0: mov      byte ptr [ebp - 0x16], 0x2a
0x4868C4: mov      byte ptr [ebp - 0x15], 0x2f
0x4868C8: mov      byte ptr [ebp - 0x14], 0x29
0x4868CC: lea      ecx, [ebp - 0x18]
0x4868CF: lea      edx, [ebp - 4]
0x4868D2: mov      eax, dword ptr [ebp - 4]
0x4868D5: call     0x452530
0x4868DA: push     2
0x4868DC: lea      eax, [ebp - 0x10]
0x4868DF: push     eax
0x4868E0: mov      byte ptr [ebp - 0x1c], 0x20
0x4868E4: mov      byte ptr [ebp - 0x1b], 0x3d
0x4868E8: mov      byte ptr [ebp - 0x1a], 0x3a
0x4868EC: lea      ecx, [ebp - 0x1c]
0x4868EF: lea      edx, [ebp - 0xc]
0x4868F2: mov      eax, dword ptr [ebp - 4]
0x4868F5: call     0x452530
0x4868FA: cmp      dword ptr [ebp - 0xc], 0
0x4868FE: je       0x486928
0x486900: mov      eax, dword ptr [ebp - 0xc]
0x486903: cmp      byte ptr [eax], 0x2f
0x486906: je       0x486920
0x486908: mov      eax, dword ptr [ebp - 0xc]
0x48690B: cmp      byte ptr [eax], 0x28
0x48690E: je       0x486920
0x486910: mov      eax, dword ptr [ebp - 0xc]
0x486913: cmp      byte ptr [eax], 0x20
0x486916: je       0x486920
0x486918: mov      eax, dword ptr [ebp - 0xc]
0x48691B: cmp      byte ptr [eax], 0x5b
0x48691E: jne      0x486928
0x486920: lea      eax, [ebp - 0xc]
0x486923: call     0x403a58
0x486928: mov      eax, dword ptr [ebp - 8]
0x48692B: mov      edx, dword ptr [ebp - 0xc]
0x48692E: call     0x403af0
0x486933: xor      eax, eax
0x486935: pop      edx
0x486936: pop      ecx
0x486937: pop      ecx
0x486938: mov      dword ptr fs:[eax], edx
0x48693B: push     0x48695d
0x486940: lea      eax, [ebp - 0x10]
0x486943: mov      edx, 2
0x486948: call     0x403a7c
0x48694D: lea      eax, [ebp - 4]
0x486950: call     0x403a58
0x486955: ret      
0x486956: jmp      0x403518
0x48695B: jmp      0x486940
0x48695D: mov      esp, ebp
0x48695F: pop      ebp
0x486960: ret      
0x486961: lea      eax, [eax]
0x486964: push     ebp
0x486965: mov      ebp, esp
0x486967: push     ecx
0x486968: mov      ecx, 4
0x48696D: push     0
0x48696F: push     0
0x486971: dec      ecx
0x486972: jne      0x48696d
0x486974: xchg     dword ptr [ebp - 4], ecx
0x486977: push     ebx
0x486978: mov      byte ptr [ebp - 0xd], cl
0x48697B: mov      dword ptr [ebp - 0xc], edx
0x48697E: mov      dword ptr [ebp - 4], eax
0x486981: xor      eax, eax
0x486983: push     ebp
0x486984: push     0x486b0a
0x486989: push     dword ptr fs:[eax]
0x48698C: mov      dword ptr fs:[eax], esp
0x48698F: cmp      dword ptr [ebp + 0xc], 0xd0
0x486996: jl       0x486ae7
0x48699C: cmp      dword ptr [ebp + 0xc], 0x246
0x4869A3: jg       0x486ae7
0x4869A9: cmp      dword ptr [ebp + 8], 0x1d6
0x4869B0: jl       0x486ae7
0x4869B6: cmp      dword ptr [ebp + 8], 0x242
0x4869BD: jg       0x486ae7
0x4869C3: mov      eax, dword ptr [ebp + 8]
0x4869C6: sub      eax, 0x1d6
0x4869CB: mov      ecx, 0xc
0x4869D0: cdq      
0x4869D1: idiv     ecx
0x4869D3: mov      edx, dword ptr [0x4a3fb8] ; *0x4A3FB8->0x4F7D8C
0x4869D9: mov      edx, dword ptr [edx]
0x4869DB: add      eax, dword ptr [edx + 0x20]
0x4869DE: mov      dword ptr [ebp - 8], eax
0x4869E1: mov      eax, dword ptr [0x4a3fb8] ; *0x4A3FB8->0x4F7D8C
0x4869E6: mov      eax, dword ptr [eax]
0x4869E8: mov      eax, dword ptr [eax + 0x18]
0x4869EB: mov      edx, dword ptr [eax]
0x4869ED: call     dword ptr [edx + 0x14]
0x4869F0: cmp      eax, dword ptr [ebp - 8]
0x4869F3: jle      0x486ad3
0x4869F9: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x4869FE: mov      eax, dword ptr [eax]
0x486A00: mov      eax, dword ptr [eax + 0x5a850]
0x486A06: cmp      byte ptr [eax + 0x47], 0
0x486A0A: jne      0x486a35
0x486A0C: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x486A11: mov      eax, dword ptr [eax]
0x486A13: mov      eax, dword ptr [eax + 0x5a850]
0x486A19: mov      dl, 1
0x486A1B: call     0x4278c0
0x486A20: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x486A25: mov      eax, dword ptr [eax]
0x486A27: mov      eax, dword ptr [eax + 0x5a850]
0x486A2D: mov      edx, dword ptr [eax]
0x486A2F: call     dword ptr [edx + 0xb4]
0x486A35: push     0x486b20 ; "/"
0x486A3A: push     ebp
0x486A3B: lea      ecx, [ebp - 0x20]
0x486A3E: mov      eax, dword ptr [0x4a3fb8] ; *0x4A3FB8->0x4F7D8C
0x486A43: mov      eax, dword ptr [eax]
0x486A45: mov      eax, dword ptr [eax + 0x18]
0x486A48: mov      edx, dword ptr [ebp - 8]
0x486A4B: mov      ebx, dword ptr [eax]
0x486A4D: call     dword ptr [ebx + 0xc]
0x486A50: mov      eax, dword ptr [ebp - 0x20]
0x486A53: lea      edx, [ebp - 0x1c]
0x486A56: call     0x486888
0x486A5B: pop      ecx
0x486A5C: push     dword ptr [ebp - 0x1c]
0x486A5F: push     0x486b2c ; " "
0x486A64: lea      eax, [ebp - 0x18]
0x486A67: mov      edx, 3
0x486A6C: call     0x403d94
0x486A71: mov      edx, dword ptr [ebp - 0x18]
0x486A74: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x486A79: mov      eax, dword ptr [eax]
0x486A7B: mov      eax, dword ptr [eax + 0x5a850]
0x486A81: call     0x427a00
0x486A86: lea      edx, [ebp - 0x24]
0x486A89: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x486A8E: mov      eax, dword ptr [eax]
0x486A90: mov      eax, dword ptr [eax + 0x5a850]
0x486A96: call     0x4279d0
0x486A9B: mov      eax, dword ptr [ebp - 0x24]
0x486A9E: call     0x403cd4
0x486AA3: mov      edx, eax
0x486AA5: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x486AAA: mov      eax, dword ptr [eax]
0x486AAC: mov      eax, dword ptr [eax + 0x5a850]
0x486AB2: mov      ecx, dword ptr [eax]
0x486AB4: call     dword ptr [ecx + 0xcc]
0x486ABA: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x486ABF: mov      eax, dword ptr [eax]
0x486AC1: mov      eax, dword ptr [eax + 0x5a850]
0x486AC7: xor      edx, edx
0x486AC9: mov      ecx, dword ptr [eax]
0x486ACB: call     dword ptr [ecx + 0xc8]
0x486AD1: jmp      0x486ae7
0x486AD3: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x486AD8: mov      eax, dword ptr [eax]
0x486ADA: mov      eax, dword ptr [eax + 0x5a850]
0x486AE0: xor      edx, edx
0x486AE2: call     0x427a00
0x486AE7: xor      eax, eax
0x486AE9: pop      edx
0x486AEA: pop      ecx
0x486AEB: pop      ecx
0x486AEC: mov      dword ptr fs:[eax], edx
0x486AEF: push     0x486b11
0x486AF4: lea      eax, [ebp - 0x24]
0x486AF7: call     0x403a58
0x486AFC: lea      eax, [ebp - 0x20]
0x486AFF: mov      edx, 4
0x486B04: call     0x403a7c
0x486B09: ret      
0x486B0A: jmp      0x403518
0x486B0F: jmp      0x486af4
0x486B11: pop      ebx
0x486B12: mov      esp, ebp
0x486B14: pop      ebp
0x486B15: ret      0xc
0x486B20: [STR len=1] "/"
0x486B2C: [STR len=1] " "
0x486B30: push     ebp
0x486B31: mov      ebp, esp
0x486B33: add      esp, -0x10
0x486B36: mov      eax, dword ptr [ebp + 8]
0x486B39: mov      eax, dword ptr [eax - 4]
0x486B3C: cmp      dword ptr [eax + 0x624], 1
0x486B43: jne      0x486c39
0x486B49: mov      eax, dword ptr [ebp + 8]
0x486B4C: mov      eax, dword ptr [eax - 4]
0x486B4F: cmp      dword ptr [eax + 0x63c], 0x14
0x486B56: jge      0x486bed
0x486B5C: call     0x406154
0x486B61: mov      edx, dword ptr [ebp + 8]
0x486B64: mov      edx, dword ptr [edx - 4]
0x486B67: sub      eax, dword ptr [edx + 0x644]
0x486B6D: cmp      eax, 0x64
0x486B70: jbe      0x486dea
0x486B76: mov      eax, dword ptr [ebp + 8]
0x486B79: mov      eax, dword ptr [eax - 4]
0x486B7C: mov      eax, dword ptr [eax + 0x63c]
0x486B82: mov      ecx, 5
0x486B87: cdq      
0x486B88: idiv     ecx
0x486B8A: cmp      edx, 4
0x486B8D: jne      0x486ba8
0x486B8F: mov      eax, 6
0x486B94: call     0x402b74
0x486B99: inc      eax
0x486B9A: mov      edx, dword ptr [ebp + 8]
0x486B9D: mov      edx, dword ptr [edx - 4]
0x486BA0: mov      dword ptr [edx + 0x630], eax
0x486BA6: jmp      0x486bcb
0x486BA8: mov      eax, dword ptr [ebp + 8]
0x486BAB: mov      eax, dword ptr [eax - 4]
0x486BAE: mov      eax, dword ptr [eax + 0x63c]
0x486BB4: mov      ecx, 5
0x486BB9: cdq      
0x486BBA: idiv     ecx
0x486BBC: add      edx, 8
0x486BBF: mov      eax, dword ptr [ebp + 8]
0x486BC2: mov      eax, dword ptr [eax - 4]
0x486BC5: mov      dword ptr [eax + 0x630], edx
0x486BCB: call     0x406154
0x486BD0: mov      edx, dword ptr [ebp + 8]
0x486BD3: mov      edx, dword ptr [edx - 4]
0x486BD6: mov      dword ptr [edx + 0x644], eax
0x486BDC: mov      eax, dword ptr [ebp + 8]
0x486BDF: mov      eax, dword ptr [eax - 4]
0x486BE2: inc      dword ptr [eax + 0x63c]
0x486BE8: jmp      0x486dea
0x486BED: mov      eax, dword ptr [ebp + 8]
0x486BF0: mov      eax, dword ptr [eax - 4]
0x486BF3: mov      eax, dword ptr [eax + 0x62c]
0x486BF9: mov      edx, dword ptr [ebp + 8]
0x486BFC: mov      edx, dword ptr [edx - 4]
0x486BFF: mov      dword ptr [edx + 0x630], eax
0x486C05: call     0x406154
0x486C0A: mov      edx, dword ptr [ebp + 8]
0x486C0D: mov      edx, dword ptr [edx - 4]
0x486C10: sub      eax, dword ptr [edx + 0x644]
0x486C16: cmp      eax, 0x5dc
0x486C1B: jbe      0x486dea
0x486C21: mov      eax, dword ptr [ebp + 8]
0x486C24: mov      eax, dword ptr [eax - 4]
0x486C27: mov      eax, dword ptr [eax + 0x368]
0x486C2D: xor      edx, edx
0x486C2F: call     0x458214
0x486C34: jmp      0x486dea
0x486C39: mov      byte ptr [ebp - 5], 1
0x486C3D: mov      eax, dword ptr [ebp + 8]
0x486C40: mov      eax, dword ptr [eax - 4]
0x486C43: mov      eax, dword ptr [eax + 0x624]
0x486C49: dec      eax
0x486C4A: test     eax, eax
0x486C4C: jl       0x486dd1
0x486C52: inc      eax
0x486C53: mov      dword ptr [ebp - 0xc], eax
0x486C56: mov      dword ptr [ebp - 4], 0
0x486C5D: mov      eax, dword ptr [ebp - 4]
0x486C60: mov      edx, eax
0x486C62: shl      eax, 3
0x486C65: sub      eax, edx
0x486C67: mov      edx, dword ptr [ebp + 8]
0x486C6A: mov      edx, dword ptr [edx - 4]
0x486C6D: mov      eax, dword ptr [edx + eax*4 + 0x63c]
0x486C74: mov      edx, dword ptr [ebp - 4]
0x486C77: mov      ecx, edx
0x486C79: shl      edx, 3
0x486C7C: sub      edx, ecx
0x486C7E: mov      ecx, dword ptr [ebp + 8]
0x486C81: mov      ecx, dword ptr [ecx - 4]
0x486C84: cmp      eax, dword ptr [ecx + edx*4 + 0x640]
0x486C8B: jge      0x486d70
0x486C91: call     0x406154
0x486C96: mov      edx, dword ptr [ebp - 4]
0x486C99: mov      ecx, edx
0x486C9B: shl      edx, 3
0x486C9E: sub      edx, ecx
0x486CA0: mov      ecx, dword ptr [ebp + 8]
0x486CA3: mov      ecx, dword ptr [ecx - 4]
0x486CA6: sub      eax, dword ptr [ecx + edx*4 + 0x644]
0x486CAD: cmp      eax, 0x64
0x486CB0: jbe      0x486d6a
0x486CB6: mov      eax, dword ptr [ebp - 4]
0x486CB9: mov      edx, eax
0x486CBB: shl      eax, 3
0x486CBE: sub      eax, edx
0x486CC0: mov      edx, dword ptr [ebp + 8]
0x486CC3: mov      edx, dword ptr [edx - 4]
0x486CC6: mov      eax, dword ptr [edx + eax*4 + 0x63c]
0x486CCD: mov      ecx, 5
0x486CD2: cdq      
0x486CD3: idiv     ecx
0x486CD5: cmp      edx, 4
0x486CD8: jne      0x486cfe
0x486CDA: mov      eax, 6
0x486CDF: call     0x402b74
0x486CE4: inc      eax
0x486CE5: mov      edx, dword ptr [ebp - 4]
0x486CE8: mov      ecx, edx
0x486CEA: shl      edx, 3
0x486CED: sub      edx, ecx
0x486CEF: mov      ecx, dword ptr [ebp + 8]
0x486CF2: mov      ecx, dword ptr [ecx - 4]
0x486CF5: mov      dword ptr [ecx + edx*4 + 0x630], eax
0x486CFC: jmp      0x486d37
0x486CFE: mov      eax, dword ptr [ebp - 4]
0x486D01: mov      edx, eax
0x486D03: shl      eax, 3
0x486D06: sub      eax, edx
0x486D08: mov      edx, dword ptr [ebp + 8]
0x486D0B: mov      edx, dword ptr [edx - 4]
0x486D0E: mov      eax, dword ptr [edx + eax*4 + 0x63c]
0x486D15: mov      ecx, 5
0x486D1A: cdq      
0x486D1B: idiv     ecx
0x486D1D: add      edx, 8
0x486D20: mov      eax, dword ptr [ebp - 4]
0x486D23: mov      ecx, eax
0x486D25: shl      eax, 3
0x486D28: sub      eax, ecx
0x486D2A: mov      ecx, dword ptr [ebp + 8]
0x486D2D: mov      ecx, dword ptr [ecx - 4]
0x486D30: mov      dword ptr [ecx + eax*4 + 0x630], edx
0x486D37: call     0x406154
0x486D3C: mov      edx, dword ptr [ebp - 4]
0x486D3F: mov      ecx, edx
0x486D41: shl      edx, 3
0x486D44: sub      edx, ecx
0x486D46: mov      ecx, dword ptr [ebp + 8]
0x486D49: mov      ecx, dword ptr [ecx - 4]
0x486D4C: mov      dword ptr [ecx + edx*4 + 0x644], eax
0x486D53: mov      eax, dword ptr [ebp - 4]
0x486D56: mov      edx, eax
0x486D58: shl      eax, 3
0x486D5B: sub      eax, edx
0x486D5D: mov      edx, dword ptr [ebp + 8]
0x486D60: mov      edx, dword ptr [edx - 4]
0x486D63: inc      dword ptr [edx + eax*4 + 0x63c]
0x486D6A: mov      byte ptr [ebp - 5], 0
0x486D6E: jmp      0x486dc5
0x486D70: mov      eax, dword ptr [ebp - 4]
0x486D73: mov      edx, eax
0x486D75: shl      eax, 3
0x486D78: sub      eax, edx
0x486D7A: mov      edx, dword ptr [ebp + 8]
0x486D7D: mov      edx, dword ptr [edx - 4]
0x486D80: mov      eax, dword ptr [edx + eax*4 + 0x62c]
0x486D87: mov      edx, dword ptr [ebp - 4]
0x486D8A: mov      ecx, edx
0x486D8C: shl      edx, 3
0x486D8F: sub      edx, ecx
0x486D91: mov      ecx, dword ptr [ebp + 8]
0x486D94: mov      ecx, dword ptr [ecx - 4]
0x486D97: mov      dword ptr [ecx + edx*4 + 0x630], eax
0x486D9E: call     0x406154
0x486DA3: mov      edx, dword ptr [ebp - 4]
0x486DA6: mov      ecx, edx
0x486DA8: shl      edx, 3
0x486DAB: sub      edx, ecx
0x486DAD: mov      ecx, dword ptr [ebp + 8]
0x486DB0: mov      ecx, dword ptr [ecx - 4]
0x486DB3: sub      eax, dword ptr [ecx + edx*4 + 0x644]
0x486DBA: cmp      eax, 0x7d0
0x486DBF: jae      0x486dc5
0x486DC1: mov      byte ptr [ebp - 5], 0
0x486DC5: inc      dword ptr [ebp - 4]
0x486DC8: dec      dword ptr [ebp - 0xc]
0x486DCB: jne      0x486c5d
0x486DD1: cmp      byte ptr [ebp - 5], 0
0x486DD5: je       0x486dea
0x486DD7: mov      eax, dword ptr [ebp + 8]
0x486DDA: mov      eax, dword ptr [eax - 4]
0x486DDD: mov      eax, dword ptr [eax + 0x368]
0x486DE3: xor      edx, edx
0x486DE5: call     0x458214
0x486DEA: mov      esp, ebp
0x486DEC: pop      ebp
0x486DED: ret      
0x486DEE: mov      eax, eax
0x486DF0: push     ebp
0x486DF1: mov      ebp, esp
0x486DF3: add      esp, -0x2c
0x486DF6: push     ebx
0x486DF7: xor      ebx, ebx
0x486DF9: mov      dword ptr [ebp - 0x2c], ebx
0x486DFC: mov      word ptr [ebp - 0xa], cx
0x486E00: mov      dword ptr [ebp - 8], edx
0x486E03: mov      dword ptr [ebp - 4], eax
0x486E06: mov      eax, dword ptr [ebp - 8]
0x486E09: call     0x403e88
0x486E0E: xor      eax, eax
0x486E10: push     ebp
0x486E11: push     0x48752e
0x486E16: push     dword ptr fs:[eax]
0x486E19: mov      dword ptr fs:[eax], esp
0x486E1C: mov      dword ptr [ebp - 0x14], 0x144
0x486E23: mov      dword ptr [ebp - 0x18], 0x7e
0x486E2A: mov      eax, dword ptr [ebp - 4]
0x486E2D: mov      eax, dword ptr [eax + 0x620]
0x486E33: sub      eax, 1
0x486E36: jb       0x486e4a
0x486E38: je       0x486f03
0x486E3E: dec      eax
0x486E3F: je       0x486fbc
0x486E45: jmp      0x487070
0x486E4A: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x486E4F: mov      eax, dword ptr [eax]
0x486E51: mov      eax, dword ptr [eax + 0x308]
0x486E57: mov      edx, 0x17d
0x486E5C: call     0x45351c
0x486E61: mov      dword ptr [ebp - 0x20], eax
0x486E64: cmp      dword ptr [ebp - 0x20], 0
0x486E68: je       0x487070
0x486E6E: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x486E73: mov      eax, dword ptr [eax]
0x486E75: mov      edx, dword ptr [eax + 0x308]
0x486E7B: mov      eax, dword ptr [ebp - 4]
0x486E7E: mov      eax, dword ptr [eax + 0x368]
0x486E84: mov      ecx, 0x17d
0x486E89: call     0x4579f0
0x486E8E: mov      eax, dword ptr [ebp - 0x20]
0x486E91: call     0x44dc60
0x486E96: mov      edx, 0x320
0x486E9B: sub      edx, eax
0x486E9D: sar      edx, 1
0x486E9F: jns      0x486ea4
0x486EA1: adc      edx, 0
0x486EA4: mov      eax, dword ptr [ebp - 4]
0x486EA7: mov      eax, dword ptr [eax + 0x368]
0x486EAD: call     0x42718c
0x486EB2: mov      eax, dword ptr [ebp - 0x20]
0x486EB5: call     0x44d944
0x486EBA: mov      edx, 0x258
0x486EBF: sub      edx, eax
0x486EC1: sar      edx, 1
0x486EC3: jns      0x486ec8
0x486EC5: adc      edx, 0
0x486EC8: mov      eax, dword ptr [ebp - 4]
0x486ECB: mov      eax, dword ptr [eax + 0x368]
0x486ED1: call     0x4271b0
0x486ED6: mov      eax, dword ptr [ebp - 4]
0x486ED9: mov      dword ptr [eax + 0x5f4], 0x27
0x486EE3: mov      eax, dword ptr [ebp - 4]
0x486EE6: mov      dword ptr [eax + 0x5f8], 0x26
0x486EF0: mov      dword ptr [ebp - 0x14], 0x5a
0x486EF7: mov      dword ptr [ebp - 0x18], 0x24
0x486EFE: jmp      0x487070
0x486F03: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x486F08: mov      eax, dword ptr [eax]
0x486F0A: mov      eax, dword ptr [eax + 0x308]
0x486F10: mov      edx, 0x168
0x486F15: call     0x45351c
0x486F1A: mov      dword ptr [ebp - 0x20], eax
0x486F1D: cmp      dword ptr [ebp - 0x20], 0
0x486F21: je       0x487070
0x486F27: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x486F2C: mov      eax, dword ptr [eax]
0x486F2E: mov      edx, dword ptr [eax + 0x308]
0x486F34: mov      eax, dword ptr [ebp - 4]
0x486F37: mov      eax, dword ptr [eax + 0x368]
0x486F3D: mov      ecx, 0x168
0x486F42: call     0x4579f0
0x486F47: mov      eax, dword ptr [ebp - 0x20]
0x486F4A: call     0x44dc60
0x486F4F: mov      edx, 0x320
0x486F54: sub      edx, eax
0x486F56: sar      edx, 1
0x486F58: jns      0x486f5d
0x486F5A: adc      edx, 0
0x486F5D: mov      eax, dword ptr [ebp - 4]
0x486F60: mov      eax, dword ptr [eax + 0x368]
0x486F66: call     0x42718c
0x486F6B: mov      eax, dword ptr [ebp - 0x20]
0x486F6E: call     0x44d944
0x486F73: mov      edx, 0x258
0x486F78: sub      edx, eax
0x486F7A: sar      edx, 1
0x486F7C: jns      0x486f81
0x486F7E: adc      edx, 0
0x486F81: mov      eax, dword ptr [ebp - 4]
0x486F84: mov      eax, dword ptr [eax + 0x368]
0x486F8A: call     0x4271b0
0x486F8F: mov      eax, dword ptr [ebp - 4]
0x486F92: mov      dword ptr [eax + 0x5f4], 0x27
0x486F9C: mov      eax, dword ptr [ebp - 4]
0x486F9F: mov      dword ptr [eax + 0x5f8], 0x26
0x486FA9: mov      dword ptr [ebp - 0x14], 0x144
0x486FB0: mov      dword ptr [ebp - 0x18], 0x7e
0x486FB7: jmp      0x487070
0x486FBC: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x486FC1: mov      eax, dword ptr [eax]
0x486FC3: mov      eax, dword ptr [eax + 0x308]
0x486FC9: mov      edx, 0x17c
0x486FCE: call     0x45351c
0x486FD3: mov      dword ptr [ebp - 0x20], eax
0x486FD6: cmp      dword ptr [ebp - 0x20], 0
0x486FDA: je       0x487070
0x486FE0: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x486FE5: mov      eax, dword ptr [eax]
0x486FE7: mov      edx, dword ptr [eax + 0x308]
0x486FED: mov      eax, dword ptr [ebp - 4]
0x486FF0: mov      eax, dword ptr [eax + 0x368]
0x486FF6: mov      ecx, 0x17c
0x486FFB: call     0x4579f0
0x487000: mov      eax, dword ptr [ebp - 0x20]
0x487003: call     0x44dc60
0x487008: mov      edx, 0x320
0x48700D: sub      edx, eax
0x48700F: sar      edx, 1
0x487011: jns      0x487016
0x487013: adc      edx, 0
0x487016: mov      eax, dword ptr [ebp - 4]
0x487019: mov      eax, dword ptr [eax + 0x368]
0x48701F: call     0x42718c
0x487024: mov      eax, dword ptr [ebp - 0x20]
0x487027: call     0x44d944
0x48702C: mov      edx, 0x258
0x487031: sub      edx, eax
0x487033: sar      edx, 1
0x487035: jns      0x48703a
0x487037: adc      edx, 0
0x48703A: mov      eax, dword ptr [ebp - 4]
0x48703D: mov      eax, dword ptr [eax + 0x368]
0x487043: call     0x4271b0
0x487048: mov      eax, dword ptr [ebp - 4]
0x48704B: mov      dword ptr [eax + 0x5f4], 0x17
0x487055: mov      eax, dword ptr [ebp - 4]
0x487058: mov      dword ptr [eax + 0x5f8], 0x14
0x487062: mov      dword ptr [ebp - 0x14], 0x5a
0x487069: mov      dword ptr [ebp - 0x18], 0x131
0x487070: mov      eax, dword ptr [ebp - 4]
0x487073: add      eax, 0x61c
0x487078: mov      edx, dword ptr [ebp - 8]
0x48707B: call     0x403aac
0x487080: mov      eax, dword ptr [ebp - 4]
0x487083: mov      byte ptr [eax + 0x5f0], 0
0x48708A: mov      eax, dword ptr [ebp - 4]
0x48708D: mov      eax, dword ptr [eax + 0x368]
0x487093: mov      byte ptr [eax + 0x278], 1
0x48709A: mov      eax, dword ptr [ebp - 4]
0x48709D: mov      eax, dword ptr [eax + 0x36c]
0x4870A3: mov      byte ptr [eax + 0x24c], 0
0x4870AA: mov      eax, dword ptr [ebp - 4]
0x4870AD: mov      eax, dword ptr [eax + 0x370]
0x4870B3: mov      byte ptr [eax + 0x24c], 0
0x4870BA: mov      eax, dword ptr [ebp - 4]
0x4870BD: mov      eax, dword ptr [eax + 0x374]
0x4870C3: mov      byte ptr [eax + 0x24c], 0
0x4870CA: mov      eax, dword ptr [ebp - 4]
0x4870CD: mov      eax, dword ptr [eax + 0x378]
0x4870D3: mov      byte ptr [eax + 0x24c], 0
0x4870DA: mov      eax, dword ptr [ebp - 4]
0x4870DD: mov      eax, dword ptr [eax + 0x368]
0x4870E3: mov      edx, 0x320
0x4870E8: sub      edx, dword ptr [eax + 0x38]
0x4870EB: sar      edx, 1
0x4870ED: jns      0x4870f2
0x4870EF: adc      edx, 0
0x4870F2: mov      eax, dword ptr [ebp - 4]
0x4870F5: mov      eax, dword ptr [eax + 0x368]
0x4870FB: call     0x42718c
0x487100: mov      eax, dword ptr [ebp - 4]
0x487103: mov      eax, dword ptr [eax + 0x368]
0x487109: mov      edx, 0x258
0x48710E: sub      edx, dword ptr [eax + 0x3c]
0x487111: sar      edx, 1
0x487113: jns      0x487118
0x487115: adc      edx, 0
0x487118: mov      eax, dword ptr [ebp - 4]
0x48711B: mov      eax, dword ptr [eax + 0x368]
0x487121: call     0x4271b0
0x487126: mov      eax, dword ptr [ebp - 4]
0x487129: mov      eax, dword ptr [eax + 0x624]
0x48712F: dec      eax
0x487130: test     eax, eax
0x487132: jl       0x4871bd
0x487138: inc      eax
0x487139: mov      dword ptr [ebp - 0x24], eax
0x48713C: mov      dword ptr [ebp - 0x1c], 0
0x487143: mov      eax, dword ptr [ebp - 0x1c]
0x487146: mov      edx, eax
0x487148: shl      eax, 3
0x48714B: sub      eax, edx
0x48714D: mov      edx, dword ptr [ebp - 4]
0x487150: xor      ecx, ecx
0x487152: mov      dword ptr [edx + eax*4 + 0x63c], ecx
0x487159: mov      eax, dword ptr [ebp - 4]
0x48715C: mov      eax, dword ptr [eax + 0x624]
0x487162: add      eax, 2
0x487165: call     0x402b74
0x48716A: lea      eax, [eax + eax*4]
0x48716D: add      eax, 0xa
0x487170: mov      edx, dword ptr [ebp - 0x1c]
0x487173: mov      ecx, edx
0x487175: shl      edx, 3
0x487178: sub      edx, ecx
0x48717A: mov      ecx, dword ptr [ebp - 4]
0x48717D: mov      dword ptr [ecx + edx*4 + 0x640], eax
0x487184: mov      eax, dword ptr [ebp - 0x1c]
0x487187: mov      edx, eax
0x487189: shl      eax, 3
0x48718C: sub      eax, edx
0x48718E: mov      edx, dword ptr [ebp - 4]
0x487191: mov      dword ptr [edx + eax*4 + 0x630], 1
0x48719C: call     0x406154
0x4871A1: mov      edx, dword ptr [ebp - 0x1c]
0x4871A4: mov      ecx, edx
0x4871A6: shl      edx, 3
0x4871A9: sub      edx, ecx
0x4871AB: mov      ecx, dword ptr [ebp - 4]
0x4871AE: mov      dword ptr [ecx + edx*4 + 0x644], eax
0x4871B5: inc      dword ptr [ebp - 0x1c]
0x4871B8: dec      dword ptr [ebp - 0x24]
0x4871BB: jne      0x487143
0x4871BD: test     byte ptr [ebp - 0xa], 8
0x4871C1: je       0x4871f9
0x4871C3: mov      eax, dword ptr [ebp - 4]
0x4871C6: mov      eax, dword ptr [eax + 0x374]
0x4871CC: mov      edx, dword ptr [ebp - 0x14]
0x4871CF: call     0x42718c
0x4871D4: mov      eax, dword ptr [ebp - 4]
0x4871D7: mov      eax, dword ptr [eax + 0x374]
0x4871DD: mov      edx, dword ptr [ebp - 0x18]
0x4871E0: call     0x4271b0
0x4871E5: mov      eax, dword ptr [ebp - 4]
0x4871E8: mov      eax, dword ptr [eax + 0x374]
0x4871EE: mov      byte ptr [eax + 0x24c], 1
0x4871F5: sub      dword ptr [ebp - 0x14], 0x6e
0x4871F9: test     byte ptr [ebp - 0xa], 2
0x4871FD: je       0x487235
0x4871FF: mov      eax, dword ptr [ebp - 4]
0x487202: mov      eax, dword ptr [eax + 0x378]
0x487208: mov      edx, dword ptr [ebp - 0x14]
0x48720B: call     0x42718c
0x487210: mov      eax, dword ptr [ebp - 4]
0x487213: mov      eax, dword ptr [eax + 0x378]
0x487219: mov      edx, dword ptr [ebp - 0x18]
0x48721C: call     0x4271b0
0x487221: mov      eax, dword ptr [ebp - 4]
0x487224: mov      eax, dword ptr [eax + 0x378]
0x48722A: mov      byte ptr [eax + 0x24c], 1
0x487231: sub      dword ptr [ebp - 0x14], 0x6e
0x487235: test     byte ptr [ebp - 0xa], 1
0x487239: je       0x487271
0x48723B: mov      eax, dword ptr [ebp - 4]
0x48723E: mov      eax, dword ptr [eax + 0x370]
0x487244: mov      edx, dword ptr [ebp - 0x14]
0x487247: call     0x42718c
0x48724C: mov      eax, dword ptr [ebp - 4]
0x48724F: mov      eax, dword ptr [eax + 0x370]
0x487255: mov      edx, dword ptr [ebp - 0x18]
0x487258: call     0x4271b0
0x48725D: mov      eax, dword ptr [ebp - 4]
0x487260: mov      eax, dword ptr [eax + 0x370]
0x487266: mov      byte ptr [eax + 0x24c], 1
0x48726D: sub      dword ptr [ebp - 0x14], 0x6e
0x487271: test     byte ptr [ebp - 0xa], 4
0x487275: jne      0x487280
0x487277: cmp      dword ptr [ebp - 0x14], 0x144
0x48727E: jne      0x4872b6
0x487280: mov      eax, dword ptr [ebp - 4]
0x487283: mov      eax, dword ptr [eax + 0x36c]
0x487289: mov      edx, dword ptr [ebp - 0x14]
0x48728C: call     0x42718c
0x487291: mov      eax, dword ptr [ebp - 4]
0x487294: mov      eax, dword ptr [eax + 0x36c]
0x48729A: mov      edx, dword ptr [ebp - 0x18]
0x48729D: call     0x4271b0
0x4872A2: mov      eax, dword ptr [ebp - 4]
0x4872A5: mov      eax, dword ptr [eax + 0x36c]
0x4872AB: mov      byte ptr [eax + 0x24c], 1
0x4872B2: sub      dword ptr [ebp - 0x14], 0x6e
0x4872B6: mov      eax, dword ptr [ebp - 4]
0x4872B9: call     0x481cec
0x4872BE: mov      eax, dword ptr [ebp - 4]
0x4872C1: mov      eax, dword ptr [eax + 0x368]
0x4872C7: call     0x45843c
0x4872CC: test     byte ptr [ebp - 0xa], 0x10
0x4872D0: je       0x487360
0x4872D6: mov      eax, dword ptr [ebp - 4]
0x4872D9: mov      byte ptr [eax + 0x5f0], 1
0x4872E0: mov      eax, dword ptr [ebp - 4]
0x4872E3: mov      eax, dword ptr [eax + 0x368]
0x4872E9: mov      byte ptr [eax + 0x278], 0
0x4872F0: mov      eax, dword ptr [ebp - 4]
0x4872F3: mov      eax, dword ptr [eax + 0x5e8]
0x4872F9: mov      dword ptr [ebp - 0x28], eax
0x4872FC: xor      edx, edx
0x4872FE: mov      eax, dword ptr [ebp - 0x28]
0x487301: call     0x427a00
0x487306: mov      eax, dword ptr [ebp - 4]
0x487309: mov      eax, dword ptr [eax + 0x368]
0x48730F: mov      edx, dword ptr [eax + 0x38]
0x487312: sub      edx, 0x46
0x487315: mov      eax, dword ptr [ebp - 0x28]
0x487318: call     0x4271d8
0x48731D: mov      eax, dword ptr [ebp - 4]
0x487320: mov      eax, dword ptr [eax + 0x5e8]
0x487326: mov      edx, 0x320
0x48732B: sub      edx, dword ptr [eax + 0x38]
0x48732E: sar      edx, 1
0x487330: jns      0x487335
0x487332: adc      edx, 0
0x487335: mov      eax, dword ptr [ebp - 0x28]
0x487338: call     0x42718c
0x48733D: mov      eax, dword ptr [ebp - 4]
0x487340: mov      eax, dword ptr [eax + 0x5e8]
0x487346: mov      edx, 0x258
0x48734B: sub      edx, dword ptr [eax + 0x3c]
0x48734E: sar      edx, 1
0x487350: jns      0x487355
0x487352: adc      edx, 0
0x487355: sub      edx, 0xa
0x487358: mov      eax, dword ptr [ebp - 0x28]
0x48735B: call     0x4271b0
0x487360: mov      dword ptr [ebp - 0x10], 1
0x487367: mov      eax, dword ptr [ebp - 4]
0x48736A: mov      eax, dword ptr [eax + 0x368]
0x487370: cmp      byte ptr [eax + 0x24c], 0
0x487377: je       0x487471
0x48737D: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x487382: mov      eax, dword ptr [eax]
0x487384: call     0x460c34
0x487389: mov      eax, dword ptr [0x4a4110] ; *0x4A4110->0x4A57A4
0x48738E: mov      eax, dword ptr [eax]
0x487390: call     0x443574
0x487395: mov      eax, dword ptr [ebp - 4]
0x487398: cmp      dword ptr [eax + 0x624], 0
0x48739F: jle      0x487458
0x4873A5: mov      eax, dword ptr [ebp - 4]
0x4873A8: mov      byte ptr [eax + 0x628], 1
0x4873AF: mov      eax, dword ptr [ebp - 4]
0x4873B2: mov      eax, dword ptr [eax + 0x624]
0x4873B8: dec      eax
0x4873B9: test     eax, eax
0x4873BB: jl       0x487451
0x4873C1: inc      eax
0x4873C2: mov      dword ptr [ebp - 0x24], eax
0x4873C5: mov      dword ptr [ebp - 0x1c], 0
0x4873CC: mov      eax, dword ptr [ebp - 4]
0x4873CF: mov      eax, dword ptr [eax + 0x368]
0x4873D5: mov      eax, dword ptr [eax + 0x38]
0x4873D8: sar      eax, 1
0x4873DA: jns      0x4873df
0x4873DC: adc      eax, 0
0x4873DF: add      eax, 6
0x4873E2: mov      edx, dword ptr [ebp - 4]
0x4873E5: mov      edx, dword ptr [edx + 0x624]
0x4873EB: mov      ecx, edx
0x4873ED: shl      edx, 5
0x4873F0: add      edx, ecx
0x4873F2: sar      edx, 1
0x4873F4: jns      0x4873f9
0x4873F6: adc      edx, 0
0x4873F9: sub      eax, edx
0x4873FB: mov      edx, dword ptr [ebp - 0x1c]
0x4873FE: mov      ecx, edx
0x487400: shl      edx, 5
0x487403: add      edx, ecx
0x487405: add      eax, edx
0x487407: mov      edx, dword ptr [ebp - 0x1c]
0x48740A: mov      ecx, edx
0x48740C: shl      edx, 3
0x48740F: sub      edx, ecx
0x487411: mov      ecx, dword ptr [ebp - 4]
0x487414: mov      dword ptr [ecx + edx*4 + 0x634], eax
0x48741B: mov      eax, dword ptr [ebp - 0x1c]
0x48741E: mov      edx, eax
0x487420: shl      eax, 3
0x487423: sub      eax, edx
0x487425: mov      edx, dword ptr [ebp - 4]
0x487428: mov      ecx, dword ptr [ebp - 4]
0x48742B: mov      ecx, dword ptr [ecx + 0x368]
0x487431: mov      ecx, dword ptr [ecx + 0x3c]
0x487434: sar      ecx, 1
0x487436: jns      0x48743b
0x487438: adc      ecx, 0
0x48743B: sub      ecx, 0xe
0x48743E: mov      dword ptr [edx + eax*4 + 0x638], ecx
0x487445: inc      dword ptr [ebp - 0x1c]
0x487448: dec      dword ptr [ebp - 0x24]
0x48744B: jne      0x4873cc
0x487451: push     ebp
0x487452: call     0x486b30
0x487457: pop      ecx
0x487458: mov      eax, dword ptr [0x4a4110] ; *0x4A4110->0x4A57A4
0x48745D: mov      eax, dword ptr [eax]
0x48745F: cmp      byte ptr [eax + 0x84], 0
0x487466: jne      0x487510
0x48746C: jmp      0x487367
0x487471: mov      eax, dword ptr [ebp - 4]
0x487474: mov      eax, dword ptr [eax + 0x5e8]
0x48747A: xor      edx, edx
0x48747C: call     0x4278c0
0x487481: mov      eax, dword ptr [ebp - 4]
0x487484: call     0x481d88
0x487489: lea      edx, [ebp - 0x2c]
0x48748C: mov      eax, dword ptr [ebp - 4]
0x48748F: mov      eax, dword ptr [eax + 0x5e8]
0x487495: call     0x4279d0
0x48749A: mov      edx, dword ptr [ebp - 0x2c]
0x48749D: mov      eax, dword ptr [ebp - 4]
0x4874A0: add      eax, 0x784
0x4874A5: call     0x403aac
0x4874AA: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x4874AF: mov      eax, dword ptr [eax]
0x4874B1: mov      eax, dword ptr [eax + 0x5a850]
0x4874B7: cmp      byte ptr [eax + 0x47], 0
0x4874BB: je       0x4874d2
0x4874BD: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x4874C2: mov      eax, dword ptr [eax]
0x4874C4: mov      eax, dword ptr [eax + 0x5a850]
0x4874CA: mov      edx, dword ptr [eax]
0x4874CC: call     dword ptr [edx + 0xb4]
0x4874D2: mov      eax, dword ptr [ebp - 4]
0x4874D5: mov      byte ptr [eax + 0x5f0], 0
0x4874DC: mov      eax, dword ptr [ebp - 4]
0x4874DF: mov      eax, dword ptr [eax + 0x368]
0x4874E5: mov      eax, dword ptr [eax + 0x284]
0x4874EB: mov      dword ptr [ebp - 0x10], eax
0x4874EE: mov      eax, dword ptr [ebp - 4]
0x4874F1: mov      dword ptr [eax + 0x620], 1
0x4874FB: mov      eax, dword ptr [ebp - 4]
0x4874FE: xor      edx, edx
0x487500: mov      dword ptr [eax + 0x624], edx
0x487506: mov      eax, dword ptr [ebp - 4]
0x487509: mov      byte ptr [eax + 0x628], 0
0x487510: xor      eax, eax
0x487512: pop      edx
0x487513: pop      ecx
0x487514: pop      ecx
0x487515: mov      dword ptr fs:[eax], edx
0x487518: push     0x487535
0x48751D: lea      eax, [ebp - 0x2c]
0x487520: call     0x403a58
0x487525: lea      eax, [ebp - 8]
0x487528: call     0x403a58
0x48752D: ret      
0x48752E: jmp      0x403518
0x487533: jmp      0x48751d
0x487535: mov      eax, dword ptr [ebp - 0x10]
0x487538: pop      ebx
0x487539: mov      esp, ebp
0x48753B: pop      ebp
0x48753C: ret      
0x48753D: lea      eax, [eax]
0x487540: push     ebp
0x487541: mov      ebp, esp
0x487543: add      esp, -0xc
0x487546: mov      dword ptr [ebp - 0xc], ecx
0x487549: mov      dword ptr [ebp - 8], edx
0x48754C: mov      dword ptr [ebp - 4], eax
0x48754F: mov      eax, dword ptr [ebp - 8]
0x487552: mov      edx, dword ptr [ebp - 4]
0x487555: cmp      eax, dword ptr [edx + 0x36c]
0x48755B: jne      0x487570
0x48755D: mov      eax, dword ptr [ebp - 4]
0x487560: mov      eax, dword ptr [eax + 0x368]
0x487566: mov      dword ptr [eax + 0x284], 1
0x487570: mov      eax, dword ptr [ebp - 8]
0x487573: mov      edx, dword ptr [ebp - 4]
0x487576: cmp      eax, dword ptr [edx + 0x370]
0x48757C: jne      0x487591
0x48757E: mov      eax, dword ptr [ebp - 4]
0x487581: mov      eax, dword ptr [eax + 0x368]
0x487587: mov      dword ptr [eax + 0x284], 6
0x487591: mov      eax, dword ptr [ebp - 8]
0x487594: mov      edx, dword ptr [ebp - 4]
0x487597: cmp      eax, dword ptr [edx + 0x374]
0x48759D: jne      0x4875b2
0x48759F: mov      eax, dword ptr [ebp - 4]
0x4875A2: mov      eax, dword ptr [eax + 0x368]
0x4875A8: mov      dword ptr [eax + 0x284], 2
0x4875B2: mov      eax, dword ptr [ebp - 8]
0x4875B5: mov      edx, dword ptr [ebp - 4]
0x4875B8: cmp      eax, dword ptr [edx + 0x378]
0x4875BE: jne      0x4875d3
0x4875C0: mov      eax, dword ptr [ebp - 4]
0x4875C3: mov      eax, dword ptr [eax + 0x368]
0x4875C9: mov      dword ptr [eax + 0x284], 7
0x4875D3: xor      edx, edx
0x4875D5: mov      eax, dword ptr [ebp - 4]
0x4875D8: mov      eax, dword ptr [eax + 0x368]
0x4875DE: call     0x458214
0x4875E3: mov      esp, ebp
0x4875E5: pop      ebp
0x4875E6: ret      4
0x4875E9: lea      eax, [eax]
0x4875EC: push     ebp
0x4875ED: mov      ebp, esp
0x4875EF: add      esp, -0xc
0x4875F2: mov      dword ptr [ebp - 8], ecx
0x4875F5: mov      dword ptr [ebp - 0xc], edx
0x4875F8: mov      dword ptr [ebp - 4], eax
0x4875FB: mov      eax, dword ptr [ebp - 8]
0x4875FE: cmp      word ptr [eax], 0xd
0x487602: jne      0x4876de
0x487608: mov      eax, dword ptr [ebp - 4]
0x48760B: mov      eax, dword ptr [eax + 0x36c]
0x487611: cmp      byte ptr [eax + 0x24c], 0
0x487618: je       0x487673
0x48761A: mov      eax, dword ptr [ebp - 4]
0x48761D: mov      eax, dword ptr [eax + 0x370]
0x487623: cmp      byte ptr [eax + 0x24c], 0
0x48762A: jne      0x487673
0x48762C: mov      eax, dword ptr [ebp - 4]
0x48762F: mov      eax, dword ptr [eax + 0x374]
0x487635: cmp      byte ptr [eax + 0x24c], 0
0x48763C: jne      0x487673
0x48763E: mov      eax, dword ptr [ebp - 4]
0x487641: mov      eax, dword ptr [eax + 0x378]
0x487647: cmp      byte ptr [eax + 0x24c], 0
0x48764E: jne      0x487673
0x487650: mov      eax, dword ptr [ebp - 4]
0x487653: mov      eax, dword ptr [eax + 0x368]
0x487659: mov      dword ptr [eax + 0x284], 1
0x487663: xor      edx, edx
0x487665: mov      eax, dword ptr [ebp - 4]
0x487668: mov      eax, dword ptr [eax + 0x368]
0x48766E: call     0x458214
0x487673: mov      eax, dword ptr [ebp - 4]
0x487676: mov      eax, dword ptr [eax + 0x370]
0x48767C: cmp      byte ptr [eax + 0x24c], 0
0x487683: je       0x4876de
0x487685: mov      eax, dword ptr [ebp - 4]
0x487688: mov      eax, dword ptr [eax + 0x36c]
0x48768E: cmp      byte ptr [eax + 0x24c], 0
0x487695: jne      0x4876de
0x487697: mov      eax, dword ptr [ebp - 4]
0x48769A: mov      eax, dword ptr [eax + 0x374]
0x4876A0: cmp      byte ptr [eax + 0x24c], 0
0x4876A7: jne      0x4876de
0x4876A9: mov      eax, dword ptr [ebp - 4]
0x4876AC: mov      eax, dword ptr [eax + 0x378]
0x4876B2: cmp      byte ptr [eax + 0x24c], 0
0x4876B9: jne      0x4876de
0x4876BB: mov      eax, dword ptr [ebp - 4]
0x4876BE: mov      eax, dword ptr [eax + 0x368]
0x4876C4: mov      dword ptr [eax + 0x284], 6
0x4876CE: xor      edx, edx
0x4876D0: mov      eax, dword ptr [ebp - 4]
0x4876D3: mov      eax, dword ptr [eax + 0x368]
0x4876D9: call     0x458214
0x4876DE: mov      eax, dword ptr [ebp - 8]
0x4876E1: cmp      word ptr [eax], 0x1b
0x4876E5: jne      0x48771c
0x4876E7: mov      eax, dword ptr [ebp - 4]
0x4876EA: mov      eax, dword ptr [eax + 0x374]
0x4876F0: cmp      byte ptr [eax + 0x24c], 0
0x4876F7: je       0x48771c
0x4876F9: mov      eax, dword ptr [ebp - 4]
0x4876FC: mov      eax, dword ptr [eax + 0x368]
0x487702: mov      dword ptr [eax + 0x284], 2
0x48770C: xor      edx, edx
0x48770E: mov      eax, dword ptr [ebp - 4]
0x487711: mov      eax, dword ptr [eax + 0x368]
0x487717: call     0x458214
0x48771C: mov      esp, ebp
0x48771E: pop      ebp
0x48771F: ret      4
0x487722: mov      eax, eax
0x487724: push     ebp
0x487725: mov      ebp, esp
0x487727: add      esp, -0x24
0x48772A: mov      dword ptr [ebp - 0xc], ecx
0x48772D: mov      dword ptr [ebp - 8], edx
0x487730: mov      dword ptr [ebp - 4], eax
0x487733: mov      eax, dword ptr [ebp - 8]
0x487736: mov      edx, dword ptr [0x456468]
0x48773C: call     0x403034
0x487741: mov      dword ptr [ebp - 0x14], eax
0x487744: mov      eax, dword ptr [ebp - 0x14]
0x487747: cmp      byte ptr [eax + 0x274], 0
0x48774E: jne      0x48776c
0x487750: mov      eax, dword ptr [ebp - 0x14]
0x487753: mov      edx, dword ptr [eax + 0x258]
0x487759: mov      eax, dword ptr [ebp - 0x14]
0x48775C: mov      eax, dword ptr [eax + 0x254]
0x487762: call     0x45351c
0x487767: mov      dword ptr [ebp - 0x10], eax
0x48776A: jmp      0x487787
0x48776C: mov      eax, dword ptr [ebp - 0x14]
0x48776F: mov      edx, dword ptr [eax + 0x258]
0x487775: inc      edx
0x487776: mov      eax, dword ptr [ebp - 0x14]
0x487779: mov      eax, dword ptr [eax + 0x254]
0x48777F: call     0x45351c
0x487784: mov      dword ptr [ebp - 0x10], eax
0x487787: cmp      dword ptr [ebp - 0x10], 0
0x48778B: je       0x4877ca
0x48778D: lea      edx, [ebp - 0x24]
0x487790: mov      eax, dword ptr [ebp - 0x10]
0x487793: call     0x44d8ec
0x487798: lea      eax, [ebp - 0x24]
0x48779B: push     eax
0x48779C: mov      eax, dword ptr [ebp - 0x10]
0x48779F: push     eax
0x4877A0: push     1
0x4877A2: mov      eax, dword ptr [ebp - 0x14]
0x4877A5: mov      edx, dword ptr [eax + 0x34]
0x4877A8: mov      eax, dword ptr [ebp - 0x14]
0x4877AB: call     0x456fdc
0x4877B0: push     eax
0x4877B1: mov      eax, dword ptr [ebp - 0x14]
0x4877B4: mov      edx, dword ptr [eax + 0x30]
0x4877B7: mov      eax, dword ptr [ebp - 0x14]
0x4877BA: call     0x456f94
0x4877BF: mov      edx, eax
0x4877C1: mov      eax, dword ptr [ebp - 0xc]
0x4877C4: pop      ecx
0x4877C5: call     0x44d6cc
0x4877CA: mov      esp, ebp
0x4877CC: pop      ebp
0x4877CD: ret      
0x4877CE: mov      eax, eax
0x4877D0: push     ebp
0x4877D1: mov      ebp, esp
0x4877D3: add      esp, -0x4c
0x4877D6: push     ebx
0x4877D7: xor      ebx, ebx
0x4877D9: mov      dword ptr [ebp - 0x48], ebx
0x4877DC: mov      dword ptr [ebp - 0x28], ebx
0x4877DF: mov      dword ptr [ebp - 0x2c], ebx
0x4877E2: mov      dword ptr [ebp - 0xc], ecx
0x4877E5: mov      dword ptr [ebp - 8], edx
0x4877E8: mov      dword ptr [ebp - 4], eax
0x4877EB: xor      eax, eax
0x4877ED: push     ebp
0x4877EE: push     0x487a60
0x4877F3: push     dword ptr fs:[eax]
0x4877F6: mov      dword ptr fs:[eax], esp
0x4877F9: mov      eax, dword ptr [ebp - 8]
0x4877FC: mov      edx, dword ptr [0x45689c]
0x487802: call     0x403034
0x487807: mov      dword ptr [ebp - 0x30], eax
0x48780A: mov      eax, dword ptr [ebp - 0x30]
0x48780D: mov      edx, dword ptr [eax + 0x258]
0x487813: mov      eax, dword ptr [ebp - 0x30]
0x487816: mov      eax, dword ptr [eax + 0x254]
0x48781C: call     0x45351c
0x487821: mov      dword ptr [ebp - 0x10], eax
0x487824: cmp      dword ptr [ebp - 0x10], 0
0x487828: je       0x487867
0x48782A: lea      edx, [ebp - 0x44]
0x48782D: mov      eax, dword ptr [ebp - 0x10]
0x487830: call     0x44d8ec
0x487835: lea      eax, [ebp - 0x44]
0x487838: push     eax
0x487839: mov      eax, dword ptr [ebp - 0x10]
0x48783C: push     eax
0x48783D: push     1
0x48783F: mov      eax, dword ptr [ebp - 0x30]
0x487842: mov      edx, dword ptr [eax + 0x34]
0x487845: mov      eax, dword ptr [ebp - 0x30]
0x487848: call     0x456fdc
0x48784D: push     eax
0x48784E: mov      eax, dword ptr [ebp - 0x30]
0x487851: mov      edx, dword ptr [eax + 0x30]
0x487854: mov      eax, dword ptr [ebp - 0x30]
0x487857: call     0x456f94
0x48785C: mov      edx, eax
0x48785E: mov      eax, dword ptr [ebp - 0xc]
0x487861: pop      ecx
0x487862: call     0x44d6cc
0x487867: mov      eax, dword ptr [ebp - 4]
0x48786A: cmp      byte ptr [eax + 0x628], 0
0x487871: je       0x48794e
0x487877: mov      eax, dword ptr [ebp - 4]
0x48787A: mov      eax, dword ptr [eax + 0x624]
0x487880: dec      eax
0x487881: test     eax, eax
0x487883: jl       0x48794e
0x487889: inc      eax
0x48788A: mov      dword ptr [ebp - 0x34], eax
0x48788D: mov      dword ptr [ebp - 0x24], 0
0x487894: lea      eax, [ebp - 0x20]
0x487897: push     eax
0x487898: lea      ecx, [ebp - 0x1c]
0x48789B: mov      eax, dword ptr [ebp - 0x24]
0x48789E: mov      edx, eax
0x4878A0: shl      eax, 3
0x4878A3: sub      eax, edx
0x4878A5: mov      edx, dword ptr [ebp - 4]
0x4878A8: mov      edx, dword ptr [edx + eax*4 + 0x630]
0x4878AF: add      edx, 0x178
0x4878B5: dec      edx
0x4878B6: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4878BB: mov      eax, dword ptr [eax]
0x4878BD: mov      eax, dword ptr [eax + 0x324]
0x4878C3: call     0x453a48
0x4878C8: mov      dword ptr [ebp - 0x14], eax
0x4878CB: cmp      dword ptr [ebp - 0x14], 0
0x4878CF: je       0x487942
0x4878D1: lea      edx, [ebp - 0x44]
0x4878D4: mov      eax, dword ptr [ebp - 0x14]
0x4878D7: call     0x44d8ec
0x4878DC: lea      eax, [ebp - 0x44]
0x4878DF: push     eax
0x4878E0: mov      eax, dword ptr [ebp - 0x14]
0x4878E3: push     eax
0x4878E4: push     1
0x4878E6: mov      eax, dword ptr [ebp - 0x30]
0x4878E9: mov      edx, dword ptr [eax + 0x34]
0x4878EC: mov      eax, dword ptr [ebp - 0x30]
0x4878EF: call     0x456fdc
0x4878F4: mov      edx, dword ptr [ebp - 0x24]
0x4878F7: mov      ecx, edx
0x4878F9: shl      edx, 3
0x4878FC: sub      edx, ecx
0x4878FE: mov      ecx, dword ptr [ebp - 4]
0x487901: add      eax, dword ptr [ecx + edx*4 + 0x638]
0x487908: add      eax, dword ptr [ebp - 0x20]
0x48790B: add      eax, 0x26
0x48790E: push     eax
0x48790F: mov      eax, dword ptr [ebp - 0x30]
0x487912: mov      edx, dword ptr [eax + 0x30]
0x487915: mov      eax, dword ptr [ebp - 0x30]
0x487918: call     0x456f94
0x48791D: mov      edx, eax
0x48791F: mov      eax, dword ptr [ebp - 0x24]
0x487922: mov      ecx, eax
0x487924: shl      eax, 3
0x487927: sub      eax, ecx
0x487929: mov      ecx, dword ptr [ebp - 4]
0x48792C: add      edx, dword ptr [ecx + eax*4 + 0x634]
0x487933: add      edx, dword ptr [ebp - 0x1c]
0x487936: sub      edx, 0xe
0x487939: mov      eax, dword ptr [ebp - 0xc]
0x48793C: pop      ecx
0x48793D: call     0x44d6cc
0x487942: inc      dword ptr [ebp - 0x24]
0x487945: dec      dword ptr [ebp - 0x34]
0x487948: jne      0x487894
0x48794E: push     1
0x487950: mov      eax, dword ptr [ebp - 0xc]
0x487953: call     0x44d8b4
0x487958: call     0x41834c
0x48795D: push     eax
0x48795E: call     0x406434
0x487963: mov      eax, dword ptr [ebp - 4]
0x487966: mov      eax, dword ptr [eax + 0x5f8]
0x48796C: mov      dword ptr [ebp - 0x18], eax
0x48796F: lea      eax, [ebp - 0x28]
0x487972: mov      edx, dword ptr [ebp - 4]
0x487975: mov      edx, dword ptr [edx + 0x61c]
0x48797B: call     0x403af0
0x487980: cmp      dword ptr [ebp - 0x28], 0
0x487984: je       0x4879f4
0x487986: push     0
0x487988: lea      eax, [ebp - 0x48]
0x48798B: push     eax
0x48798C: mov      byte ptr [ebp - 0x4c], 0x5c
0x487990: lea      ecx, [ebp - 0x4c]
0x487993: lea      edx, [ebp - 0x2c]
0x487996: mov      eax, dword ptr [ebp - 0x28]
0x487999: call     0x452530
0x48799E: mov      edx, dword ptr [ebp - 0x48]
0x4879A1: lea      eax, [ebp - 0x28]
0x4879A4: call     0x403af0
0x4879A9: cmp      dword ptr [ebp - 0x2c], 0
0x4879AD: je       0x4879ee
0x4879AF: push     0xffffff
0x4879B4: push     0
0x4879B6: mov      eax, dword ptr [ebp - 0x2c]
0x4879B9: push     eax
0x4879BA: mov      eax, dword ptr [ebp - 0x30]
0x4879BD: mov      edx, dword ptr [eax + 0x34]
0x4879C0: add      edx, dword ptr [ebp - 0x18]
0x4879C3: mov      eax, dword ptr [ebp - 0x30]
0x4879C6: call     0x456fdc
0x4879CB: push     eax
0x4879CC: mov      eax, dword ptr [ebp - 0x30]
0x4879CF: mov      edx, dword ptr [eax + 0x30]
0x4879D2: mov      eax, dword ptr [ebp - 4]
0x4879D5: add      edx, dword ptr [eax + 0x5f4]
0x4879DB: mov      eax, dword ptr [ebp - 0x30]
0x4879DE: call     0x456f94
0x4879E3: mov      edx, eax
0x4879E5: mov      eax, dword ptr [ebp - 0xc]
0x4879E8: pop      ecx
0x4879E9: call     0x4a09a0
0x4879EE: add      dword ptr [ebp - 0x18], 0xe
0x4879F2: jmp      0x487980
0x4879F4: mov      eax, dword ptr [ebp - 0xc]
0x4879F7: call     0x44d8b4
0x4879FC: call     0x44d104
0x487A01: mov      eax, dword ptr [ebp - 4]
0x487A04: cmp      byte ptr [eax + 0x5f0], 0
0x487A0B: je       0x487a3d
0x487A0D: mov      eax, dword ptr [ebp - 4]
0x487A10: mov      eax, dword ptr [eax + 0x5e8]
0x487A16: cmp      byte ptr [eax + 0x47], 0
0x487A1A: jne      0x487a3d
0x487A1C: mov      dl, 1
0x487A1E: mov      eax, dword ptr [ebp - 4]
0x487A21: mov      eax, dword ptr [eax + 0x5e8]
0x487A27: call     0x4278c0
0x487A2C: mov      eax, dword ptr [ebp - 4]
0x487A2F: mov      eax, dword ptr [eax + 0x5e8]
0x487A35: mov      edx, dword ptr [eax]
0x487A37: call     dword ptr [edx + 0xb4]
0x487A3D: xor      eax, eax
0x487A3F: pop      edx
0x487A40: pop      ecx
0x487A41: pop      ecx
0x487A42: mov      dword ptr fs:[eax], edx
0x487A45: push     0x487a67
0x487A4A: lea      eax, [ebp - 0x48]
0x487A4D: call     0x403a58
0x487A52: lea      eax, [ebp - 0x2c]
0x487A55: mov      edx, 2
0x487A5A: call     0x403a7c
0x487A5F: ret      
0x487A60: jmp      0x403518
0x487A65: jmp      0x487a4a
0x487A67: pop      ebx
0x487A68: mov      esp, ebp
0x487A6A: pop      ebp
0x487A6B: ret      
0x487A6C: push     ebp
0x487A6D: mov      ebp, esp
0x487A6F: add      esp, -0x24
0x487A72: mov      dword ptr [ebp - 0xc], ecx
0x487A75: mov      dword ptr [ebp - 8], edx
0x487A78: mov      dword ptr [ebp - 4], eax
0x487A7B: mov      eax, dword ptr [ebp - 8]
0x487A7E: mov      edx, dword ptr [0x456468]
0x487A84: call     0x403034
0x487A89: mov      dword ptr [ebp - 0x14], eax
0x487A8C: mov      eax, dword ptr [ebp - 8]
0x487A8F: cmp      byte ptr [eax + 0x274], 0
0x487A96: je       0x487af5
0x487A98: mov      eax, dword ptr [ebp - 0x14]
0x487A9B: mov      edx, dword ptr [eax + 0x258]
0x487AA1: mov      eax, dword ptr [ebp - 0x14]
0x487AA4: mov      eax, dword ptr [eax + 0x254]
0x487AAA: call     0x45351c
0x487AAF: mov      dword ptr [ebp - 0x10], eax
0x487AB2: cmp      dword ptr [ebp - 0x10], 0
0x487AB6: je       0x487af5
0x487AB8: lea      edx, [ebp - 0x24]
0x487ABB: mov      eax, dword ptr [ebp - 0x10]
0x487ABE: call     0x44d8ec
0x487AC3: lea      eax, [ebp - 0x24]
0x487AC6: push     eax
0x487AC7: mov      eax, dword ptr [ebp - 0x10]
0x487ACA: push     eax
0x487ACB: push     1
0x487ACD: mov      eax, dword ptr [ebp - 0x14]
0x487AD0: mov      edx, dword ptr [eax + 0x34]
0x487AD3: mov      eax, dword ptr [ebp - 0x14]
0x487AD6: call     0x456fdc
0x487ADB: push     eax
0x487ADC: mov      eax, dword ptr [ebp - 0x14]
0x487ADF: mov      edx, dword ptr [eax + 0x30]
0x487AE2: mov      eax, dword ptr [ebp - 0x14]
0x487AE5: call     0x456f94
0x487AEA: mov      edx, eax
0x487AEC: mov      eax, dword ptr [ebp - 0xc]
0x487AEF: pop      ecx
0x487AF0: call     0x44d6cc
0x487AF5: mov      esp, ebp
0x487AF7: pop      ebp
0x487AF8: ret      
0x487AF9: lea      eax, [eax]
0x487AFC: push     ebp
0x487AFD: mov      ebp, esp
0x487AFF: add      esp, -0xc
0x487B02: mov      dword ptr [ebp - 0xc], ecx
0x487B05: mov      dword ptr [ebp - 8], edx
0x487B08: mov      dword ptr [ebp - 4], eax
0x487B0B: mov      eax, dword ptr [0x4a4134] ; *0x4A4134->0x4F7D94
0x487B10: mov      eax, dword ptr [eax]
0x487B12: call     0x47db24
0x487B17: mov      esp, ebp
0x487B19: pop      ebp
0x487B1A: ret      4
0x487B1D: lea      eax, [eax]
0x487B20: push     ebp
0x487B21: mov      ebp, esp
0x487B23: add      esp, -0xc
0x487B26: mov      dword ptr [ebp - 0xc], ecx
0x487B29: mov      dword ptr [ebp - 8], edx
0x487B2C: mov      dword ptr [ebp - 4], eax
0x487B2F: mov      eax, dword ptr [0x4a4134] ; *0x4A4134->0x4F7D94
0x487B34: mov      eax, dword ptr [eax]
0x487B36: call     0x47ddbc
0x487B3B: mov      esp, ebp
0x487B3D: pop      ebp
0x487B3E: ret      4
0x487B41: lea      eax, [eax]
0x487B44: push     ebp
0x487B45: mov      ebp, esp
0x487B47: add      esp, -0xc
0x487B4A: mov      dword ptr [ebp - 0xc], ecx
0x487B4D: mov      dword ptr [ebp - 8], edx
0x487B50: mov      dword ptr [ebp - 4], eax
0x487B53: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x487B58: mov      eax, dword ptr [eax]
0x487B5A: call     0x4405bc
0x487B5F: mov      esp, ebp
0x487B61: pop      ebp
0x487B62: ret      4
0x487B65: lea      eax, [eax]
0x487B68: push     ebp
0x487B69: mov      ebp, esp
0x487B6B: add      esp, -0xc
0x487B6E: mov      dword ptr [ebp - 0xc], ecx
0x487B71: mov      dword ptr [ebp - 8], edx
0x487B74: mov      dword ptr [ebp - 4], eax
0x487B77: mov      eax, dword ptr [0x4a4134] ; *0x4A4134->0x4F7D94
0x487B7C: mov      eax, dword ptr [eax]
0x487B7E: call     0x47dddc
0x487B83: mov      esp, ebp
0x487B85: pop      ebp
0x487B86: ret      4
0x487B89: lea      eax, [eax]
0x487B8C: push     ebp
0x487B8D: mov      ebp, esp
0x487B8F: add      esp, -0xc
0x487B92: mov      byte ptr [ebp - 5], cl
0x487B95: mov      dword ptr [ebp - 0xc], edx
0x487B98: mov      dword ptr [ebp - 4], eax
0x487B9B: mov      al, byte ptr [ebp - 5]
0x487B9E: dec      al
0x487BA0: je       0x487bb6
0x487BA2: dec      al
0x487BA4: je       0x487bc2
0x487BA6: dec      al
0x487BA8: jne      0x487bcc
0x487BAA: mov      eax, 0x67
0x487BAF: call     0x495794
0x487BB4: jmp      0x487bcc
0x487BB6: mov      eax, 0x68
0x487BBB: call     0x495794
0x487BC0: jmp      0x487bcc
0x487BC2: mov      eax, 0x69
0x487BC7: call     0x495794
0x487BCC: mov      esp, ebp
0x487BCE: pop      ebp
0x487BCF: ret      
0x487BD0: push     ebp
0x487BD1: mov      ebp, esp
0x487BD3: push     ecx
0x487BD4: mov      dword ptr [ebp - 4], eax
0x487BD7: mov      dl, 1
0x487BD9: mov      eax, dword ptr [ebp - 4]
0x487BDC: mov      eax, dword ptr [eax + 0x480]
0x487BE2: call     0x458214
0x487BE7: mov      eax, dword ptr [ebp - 4]
0x487BEA: mov      byte ptr [eax + 0x778], 1
0x487BF1: pop      ecx
0x487BF2: pop      ebp
0x487BF3: ret      
0x487BF4: push     ebp
0x487BF5: mov      ebp, esp
0x487BF7: add      esp, -0x24
0x487BFA: mov      dword ptr [ebp - 8], ecx
0x487BFD: mov      dword ptr [ebp - 0x14], edx
0x487C00: mov      dword ptr [ebp - 4], eax
0x487C03: mov      eax, dword ptr [ebp - 4]
0x487C06: mov      eax, dword ptr [eax + 0x480]
0x487C0C: mov      dword ptr [ebp - 0x10], eax
0x487C0F: mov      eax, dword ptr [ebp - 0x10]
0x487C12: mov      edx, dword ptr [eax + 0x258]
0x487C18: mov      eax, dword ptr [ebp - 0x10]
0x487C1B: mov      eax, dword ptr [eax + 0x254]
0x487C21: call     0x45351c
0x487C26: mov      dword ptr [ebp - 0xc], eax
0x487C29: cmp      dword ptr [ebp - 0xc], 0
0x487C2D: je       0x487c6c
0x487C2F: lea      edx, [ebp - 0x24]
0x487C32: mov      eax, dword ptr [ebp - 0xc]
0x487C35: call     0x44d8ec
0x487C3A: lea      eax, [ebp - 0x24]
0x487C3D: push     eax
0x487C3E: mov      eax, dword ptr [ebp - 0xc]
0x487C41: push     eax
0x487C42: push     1
0x487C44: mov      eax, dword ptr [ebp - 0x10]
0x487C47: mov      edx, dword ptr [eax + 0x34]
0x487C4A: mov      eax, dword ptr [ebp - 0x10]
0x487C4D: call     0x456fdc
0x487C52: push     eax
0x487C53: mov      eax, dword ptr [ebp - 0x10]
0x487C56: mov      edx, dword ptr [eax + 0x30]
0x487C59: mov      eax, dword ptr [ebp - 0x10]
0x487C5C: call     0x456f94
0x487C61: mov      edx, eax
0x487C63: mov      eax, dword ptr [ebp - 8]
0x487C66: pop      ecx
0x487C67: call     0x44d6cc
0x487C6C: mov      eax, dword ptr [ebp - 4]
0x487C6F: cmp      byte ptr [eax + 0x778], 0
0x487C76: je       0x488021
0x487C7C: mov      eax, dword ptr [ebp - 4]
0x487C7F: mov      byte ptr [eax + 0x778], 0
0x487C86: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x487C8B: cmp      dword ptr [eax], 1
0x487C8E: jl       0x487ca6
0x487C90: mov      edx, dword ptr [0x4a3fd8] ; *0x4A3FD8->0x4F7DD4
0x487C96: mov      edx, dword ptr [edx]
0x487C98: mov      eax, dword ptr [ebp - 4]
0x487C9B: mov      eax, dword ptr [eax + 0x488]
0x487CA1: call     0x456cbc
0x487CA6: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x487CAB: cmp      dword ptr [eax], 2
0x487CAE: jl       0x487cc7
0x487CB0: mov      edx, dword ptr [0x4a3fd8] ; *0x4A3FD8->0x4F7DD4
0x487CB6: mov      edx, dword ptr [edx + 4]
0x487CB9: mov      eax, dword ptr [ebp - 4]
0x487CBC: mov      eax, dword ptr [eax + 0x48c]
0x487CC2: call     0x456cbc
0x487CC7: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x487CCC: cmp      dword ptr [eax], 3
0x487CCF: jl       0x487ce8
0x487CD1: mov      edx, dword ptr [0x4a3fd8] ; *0x4A3FD8->0x4F7DD4
0x487CD7: mov      edx, dword ptr [edx + 8]
0x487CDA: mov      eax, dword ptr [ebp - 4]
0x487CDD: mov      eax, dword ptr [eax + 0x4bc]
0x487CE3: call     0x456cbc
0x487CE8: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x487CED: cmp      dword ptr [eax], 4
0x487CF0: jl       0x487d09
0x487CF2: mov      edx, dword ptr [0x4a3fd8] ; *0x4A3FD8->0x4F7DD4
0x487CF8: mov      edx, dword ptr [edx + 0xc]
0x487CFB: mov      eax, dword ptr [ebp - 4]
0x487CFE: mov      eax, dword ptr [eax + 0x4c0]
0x487D04: call     0x456cbc
0x487D09: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x487D0E: cmp      dword ptr [eax], 5
0x487D11: jl       0x487d2a
0x487D13: mov      edx, dword ptr [0x4a3fd8] ; *0x4A3FD8->0x4F7DD4
0x487D19: mov      edx, dword ptr [edx + 0x10]
0x487D1C: mov      eax, dword ptr [ebp - 4]
0x487D1F: mov      eax, dword ptr [eax + 0x50c]
0x487D25: call     0x456cbc
0x487D2A: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x487D2F: cmp      dword ptr [eax], 6
0x487D32: jl       0x487d4b
0x487D34: mov      edx, dword ptr [0x4a3fd8] ; *0x4A3FD8->0x4F7DD4
0x487D3A: mov      edx, dword ptr [edx + 0x14]
0x487D3D: mov      eax, dword ptr [ebp - 4]
0x487D40: mov      eax, dword ptr [eax + 0x510]
0x487D46: call     0x456cbc
0x487D4B: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x487D50: cmp      dword ptr [eax], 7
0x487D53: jl       0x487d6c
0x487D55: mov      edx, dword ptr [0x4a3fd8] ; *0x4A3FD8->0x4F7DD4
0x487D5B: mov      edx, dword ptr [edx + 0x18]
0x487D5E: mov      eax, dword ptr [ebp - 4]
0x487D61: mov      eax, dword ptr [eax + 0x588]
0x487D67: call     0x456cbc
0x487D6C: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x487D71: cmp      dword ptr [eax], 8
0x487D74: jl       0x487d8d
0x487D76: mov      edx, dword ptr [0x4a3fd8] ; *0x4A3FD8->0x4F7DD4
0x487D7C: mov      edx, dword ptr [edx + 0x1c]
0x487D7F: mov      eax, dword ptr [ebp - 4]
0x487D82: mov      eax, dword ptr [eax + 0x584]
0x487D88: call     0x456cbc
0x487D8D: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x487D92: cmp      dword ptr [eax], 9
0x487D95: jl       0x487dae
0x487D97: mov      edx, dword ptr [0x4a3fd8] ; *0x4A3FD8->0x4F7DD4
0x487D9D: mov      edx, dword ptr [edx + 0x20]
0x487DA0: mov      eax, dword ptr [ebp - 4]
0x487DA3: mov      eax, dword ptr [eax + 0x58c]
0x487DA9: call     0x456cbc
0x487DAE: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x487DB3: cmp      dword ptr [eax], 0xa
0x487DB6: jl       0x487dcf
0x487DB8: mov      edx, dword ptr [0x4a3fd8] ; *0x4A3FD8->0x4F7DD4
0x487DBE: mov      edx, dword ptr [edx + 0x24]
0x487DC1: mov      eax, dword ptr [ebp - 4]
0x487DC4: mov      eax, dword ptr [eax + 0x590]
0x487DCA: call     0x456cbc
0x487DCF: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x487DD4: cmp      dword ptr [eax], 0xb
0x487DD7: jl       0x487df0
0x487DD9: mov      edx, dword ptr [0x4a3fd8] ; *0x4A3FD8->0x4F7DD4
0x487DDF: mov      edx, dword ptr [edx + 0x28]
0x487DE2: mov      eax, dword ptr [ebp - 4]
0x487DE5: mov      eax, dword ptr [eax + 0x594]
0x487DEB: call     0x456cbc
0x487DF0: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x487DF5: cmp      dword ptr [eax], 0xc
0x487DF8: jl       0x487e11
0x487DFA: mov      edx, dword ptr [0x4a3fd8] ; *0x4A3FD8->0x4F7DD4
0x487E00: mov      edx, dword ptr [edx + 0x2c]
0x487E03: mov      eax, dword ptr [ebp - 4]
0x487E06: mov      eax, dword ptr [eax + 0x598]
0x487E0C: call     0x456cbc
0x487E11: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x487E16: cmp      dword ptr [eax], 0xd
0x487E19: jl       0x487e32
0x487E1B: mov      edx, dword ptr [0x4a3fd8] ; *0x4A3FD8->0x4F7DD4
0x487E21: mov      edx, dword ptr [edx + 0x30]
0x487E24: mov      eax, dword ptr [ebp - 4]
0x487E27: mov      eax, dword ptr [eax + 0x59c]
0x487E2D: call     0x456cbc
0x487E32: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x487E37: cmp      dword ptr [eax], 0xe
0x487E3A: jl       0x487e53
0x487E3C: mov      edx, dword ptr [0x4a3fd8] ; *0x4A3FD8->0x4F7DD4
0x487E42: mov      edx, dword ptr [edx + 0x34]
0x487E45: mov      eax, dword ptr [ebp - 4]
0x487E48: mov      eax, dword ptr [eax + 0x5a0]
0x487E4E: call     0x456cbc
0x487E53: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x487E58: cmp      dword ptr [eax], 0xf
0x487E5B: jl       0x487e74
0x487E5D: mov      edx, dword ptr [0x4a3fd8] ; *0x4A3FD8->0x4F7DD4
0x487E63: mov      edx, dword ptr [edx + 0x38]
0x487E66: mov      eax, dword ptr [ebp - 4]
0x487E69: mov      eax, dword ptr [eax + 0x5a4]
0x487E6F: call     0x456cbc
0x487E74: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x487E79: cmp      dword ptr [eax], 0x10
0x487E7C: jl       0x487e95
0x487E7E: mov      edx, dword ptr [0x4a3fd8] ; *0x4A3FD8->0x4F7DD4
0x487E84: mov      edx, dword ptr [edx + 0x3c]
0x487E87: mov      eax, dword ptr [ebp - 4]
0x487E8A: mov      eax, dword ptr [eax + 0x5a8]
0x487E90: call     0x456cbc
0x487E95: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x487E9A: cmp      dword ptr [eax], 0x11
0x487E9D: jl       0x487eb6
0x487E9F: mov      edx, dword ptr [0x4a3fd8] ; *0x4A3FD8->0x4F7DD4
0x487EA5: mov      edx, dword ptr [edx + 0x40]
0x487EA8: mov      eax, dword ptr [ebp - 4]
0x487EAB: mov      eax, dword ptr [eax + 0x5ac]
0x487EB1: call     0x456cbc
0x487EB6: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x487EBB: cmp      dword ptr [eax], 0x12
0x487EBE: jl       0x487ed7
0x487EC0: mov      edx, dword ptr [0x4a3fd8] ; *0x4A3FD8->0x4F7DD4
0x487EC6: mov      edx, dword ptr [edx + 0x44]
0x487EC9: mov      eax, dword ptr [ebp - 4]
0x487ECC: mov      eax, dword ptr [eax + 0x5b0]
0x487ED2: call     0x456cbc
0x487ED7: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x487EDC: cmp      dword ptr [eax], 0x13
0x487EDF: jl       0x487ef8
0x487EE1: mov      edx, dword ptr [0x4a3fd8] ; *0x4A3FD8->0x4F7DD4
0x487EE7: mov      edx, dword ptr [edx + 0x48]
0x487EEA: mov      eax, dword ptr [ebp - 4]
0x487EED: mov      eax, dword ptr [eax + 0x5b4]
0x487EF3: call     0x456cbc
0x487EF8: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x487EFD: cmp      dword ptr [eax], 0x14
0x487F00: jl       0x487f19
0x487F02: mov      edx, dword ptr [0x4a3fd8] ; *0x4A3FD8->0x4F7DD4
0x487F08: mov      edx, dword ptr [edx + 0x4c]
0x487F0B: mov      eax, dword ptr [ebp - 4]
0x487F0E: mov      eax, dword ptr [eax + 0x5b8]
0x487F14: call     0x456cbc
0x487F19: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x487F1E: cmp      dword ptr [eax], 0x15
0x487F21: jl       0x487f3a
0x487F23: mov      edx, dword ptr [0x4a3fd8] ; *0x4A3FD8->0x4F7DD4
0x487F29: mov      edx, dword ptr [edx + 0x50]
0x487F2C: mov      eax, dword ptr [ebp - 4]
0x487F2F: mov      eax, dword ptr [eax + 0x5bc]
0x487F35: call     0x456cbc
0x487F3A: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x487F3F: cmp      dword ptr [eax], 0x16
0x487F42: jl       0x487f5b
0x487F44: mov      edx, dword ptr [0x4a3fd8] ; *0x4A3FD8->0x4F7DD4
0x487F4A: mov      edx, dword ptr [edx + 0x54]
0x487F4D: mov      eax, dword ptr [ebp - 4]
0x487F50: mov      eax, dword ptr [eax + 0x5c0]
0x487F56: call     0x456cbc
0x487F5B: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x487F60: cmp      dword ptr [eax], 0x17
0x487F63: jl       0x487f7c
0x487F65: mov      edx, dword ptr [0x4a3fd8] ; *0x4A3FD8->0x4F7DD4
0x487F6B: mov      edx, dword ptr [edx + 0x58]
0x487F6E: mov      eax, dword ptr [ebp - 4]
0x487F71: mov      eax, dword ptr [eax + 0x5c4]
0x487F77: call     0x456cbc
0x487F7C: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x487F81: cmp      dword ptr [eax], 0x18
0x487F84: jl       0x487f9d
0x487F86: mov      edx, dword ptr [0x4a3fd8] ; *0x4A3FD8->0x4F7DD4
0x487F8C: mov      edx, dword ptr [edx + 0x5c]
0x487F8F: mov      eax, dword ptr [ebp - 4]
0x487F92: mov      eax, dword ptr [eax + 0x5c8]
0x487F98: call     0x456cbc
0x487F9D: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x487FA2: cmp      dword ptr [eax], 0x19
0x487FA5: jl       0x487fbe
0x487FA7: mov      edx, dword ptr [0x4a3fd8] ; *0x4A3FD8->0x4F7DD4
0x487FAD: mov      edx, dword ptr [edx + 0x60]
0x487FB0: mov      eax, dword ptr [ebp - 4]
0x487FB3: mov      eax, dword ptr [eax + 0x5cc]
0x487FB9: call     0x456cbc
0x487FBE: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x487FC3: cmp      dword ptr [eax], 0x1a
0x487FC6: jl       0x487fdf
0x487FC8: mov      edx, dword ptr [0x4a3fd8] ; *0x4A3FD8->0x4F7DD4
0x487FCE: mov      edx, dword ptr [edx + 0x64]
0x487FD1: mov      eax, dword ptr [ebp - 4]
0x487FD4: mov      eax, dword ptr [eax + 0x5d0]
0x487FDA: call     0x456cbc
0x487FDF: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x487FE4: cmp      dword ptr [eax], 0x1b
0x487FE7: jl       0x488000
0x487FE9: mov      edx, dword ptr [0x4a3fd8] ; *0x4A3FD8->0x4F7DD4
0x487FEF: mov      edx, dword ptr [edx + 0x68]
0x487FF2: mov      eax, dword ptr [ebp - 4]
0x487FF5: mov      eax, dword ptr [eax + 0x5d4]
0x487FFB: call     0x456cbc
0x488000: mov      eax, dword ptr [0x4a40ec] ; *0x4A40EC->0x4F7DD0
0x488005: cmp      dword ptr [eax], 0x1c
0x488008: jl       0x488021
0x48800A: mov      edx, dword ptr [0x4a3fd8] ; *0x4A3FD8->0x4F7DD4
0x488010: mov      edx, dword ptr [edx + 0x6c]
0x488013: mov      eax, dword ptr [ebp - 4]
0x488016: mov      eax, dword ptr [eax + 0x5d8]
0x48801C: call     0x456cbc
0x488021: mov      esp, ebp
0x488023: pop      ebp
0x488024: ret      
0x488025: lea      eax, [eax]
0x488028: push     ebp
0x488029: mov      ebp, esp
0x48802B: add      esp, -0x10
0x48802E: push     ebx
0x48802F: xor      ebx, ebx
0x488031: mov      dword ptr [ebp - 0xc], ebx
0x488034: mov      dword ptr [ebp - 0x10], ecx
0x488037: mov      dword ptr [ebp - 8], edx
0x48803A: mov      dword ptr [ebp - 4], eax
0x48803D: xor      eax, eax
0x48803F: push     ebp
0x488040: push     0x488372
0x488045: push     dword ptr fs:[eax]
0x488048: mov      dword ptr fs:[eax], esp
0x48804B: lea      eax, [ebp - 0xc]
0x48804E: call     0x403a58
0x488053: mov      eax, dword ptr [ebp - 8]
0x488056: cmp      dword ptr [eax + 0xc], 0
0x48805A: jne      0x48806c
0x48805C: lea      eax, [ebp - 0xc]
0x48805F: mov      edx, dword ptr [0x4a4194] ; *0x4A4194->0x4F7E54
0x488065: mov      edx, dword ptr [edx]
0x488067: call     0x403af0
0x48806C: mov      eax, dword ptr [ebp - 8]
0x48806F: cmp      dword ptr [eax + 0xc], 1
0x488073: jne      0x488086
0x488075: lea      eax, [ebp - 0xc]
0x488078: mov      edx, dword ptr [0x4a4194] ; *0x4A4194->0x4F7E54
0x48807E: mov      edx, dword ptr [edx + 4]
0x488081: call     0x403af0
0x488086: mov      eax, dword ptr [ebp - 8]
0x488089: cmp      dword ptr [eax + 0xc], 2
0x48808D: jne      0x4880a0
0x48808F: lea      eax, [ebp - 0xc]
0x488092: mov      edx, dword ptr [0x4a4194] ; *0x4A4194->0x4F7E54
0x488098: mov      edx, dword ptr [edx + 8]
0x48809B: call     0x403af0
0x4880A0: mov      eax, dword ptr [ebp - 8]
0x4880A3: cmp      dword ptr [eax + 0xc], 3
0x4880A7: jne      0x4880ba
0x4880A9: lea      eax, [ebp - 0xc]
0x4880AC: mov      edx, dword ptr [0x4a4194] ; *0x4A4194->0x4F7E54
0x4880B2: mov      edx, dword ptr [edx + 0xc]
0x4880B5: call     0x403af0
0x4880BA: mov      eax, dword ptr [ebp - 8]
0x4880BD: cmp      dword ptr [eax + 0xc], 4
0x4880C1: jne      0x4880d4
0x4880C3: lea      eax, [ebp - 0xc]
0x4880C6: mov      edx, dword ptr [0x4a4194] ; *0x4A4194->0x4F7E54
0x4880CC: mov      edx, dword ptr [edx + 0x10]
0x4880CF: call     0x403af0
0x4880D4: mov      eax, dword ptr [ebp - 8]
0x4880D7: cmp      dword ptr [eax + 0xc], 5
0x4880DB: jne      0x4880ee
0x4880DD: lea      eax, [ebp - 0xc]
0x4880E0: mov      edx, dword ptr [0x4a4194] ; *0x4A4194->0x4F7E54
0x4880E6: mov      edx, dword ptr [edx + 0x14]
0x4880E9: call     0x403af0
0x4880EE: mov      eax, dword ptr [ebp - 8]
0x4880F1: cmp      dword ptr [eax + 0xc], 6
0x4880F5: jne      0x488108
0x4880F7: lea      eax, [ebp - 0xc]
0x4880FA: mov      edx, dword ptr [0x4a4194] ; *0x4A4194->0x4F7E54
0x488100: mov      edx, dword ptr [edx + 0x18]
0x488103: call     0x403af0
0x488108: mov      eax, dword ptr [ebp - 8]
0x48810B: cmp      dword ptr [eax + 0xc], 7
0x48810F: jne      0x488122
0x488111: lea      eax, [ebp - 0xc]
0x488114: mov      edx, dword ptr [0x4a4194] ; *0x4A4194->0x4F7E54
0x48811A: mov      edx, dword ptr [edx + 0x1c]
0x48811D: call     0x403af0
0x488122: mov      eax, dword ptr [ebp - 8]
0x488125: cmp      dword ptr [eax + 0xc], 8
0x488129: jne      0x48813c
0x48812B: lea      eax, [ebp - 0xc]
0x48812E: mov      edx, dword ptr [0x4a4194] ; *0x4A4194->0x4F7E54
0x488134: mov      edx, dword ptr [edx + 0x20]
0x488137: call     0x403af0
0x48813C: mov      eax, dword ptr [ebp - 8]
0x48813F: cmp      dword ptr [eax + 0xc], 9
0x488143: jne      0x488156
0x488145: lea      eax, [ebp - 0xc]
0x488148: mov      edx, dword ptr [0x4a4194] ; *0x4A4194->0x4F7E54
0x48814E: mov      edx, dword ptr [edx + 0x24]
0x488151: call     0x403af0
0x488156: mov      eax, dword ptr [ebp - 8]
0x488159: cmp      dword ptr [eax + 0xc], 0xa
0x48815D: jne      0x488170
0x48815F: lea      eax, [ebp - 0xc]
0x488162: mov      edx, dword ptr [0x4a4194] ; *0x4A4194->0x4F7E54
0x488168: mov      edx, dword ptr [edx + 0x28]
0x48816B: call     0x403af0
0x488170: mov      eax, dword ptr [ebp - 8]
0x488173: cmp      dword ptr [eax + 0xc], 0xb
0x488177: jne      0x48818a
0x488179: lea      eax, [ebp - 0xc]
0x48817C: mov      edx, dword ptr [0x4a4194] ; *0x4A4194->0x4F7E54
0x488182: mov      edx, dword ptr [edx + 0x2c]
0x488185: call     0x403af0
0x48818A: mov      eax, dword ptr [ebp - 8]
0x48818D: cmp      dword ptr [eax + 0xc], 0xc
0x488191: jne      0x4881a4
0x488193: lea      eax, [ebp - 0xc]
0x488196: mov      edx, dword ptr [0x4a4194] ; *0x4A4194->0x4F7E54
0x48819C: mov      edx, dword ptr [edx + 0x30]
0x48819F: call     0x403af0
0x4881A4: mov      eax, dword ptr [ebp - 8]
0x4881A7: cmp      dword ptr [eax + 0xc], 0xd
0x4881AB: jne      0x4881be
0x4881AD: lea      eax, [ebp - 0xc]
0x4881B0: mov      edx, dword ptr [0x4a4194] ; *0x4A4194->0x4F7E54
0x4881B6: mov      edx, dword ptr [edx + 0x34]
0x4881B9: call     0x403af0
0x4881BE: mov      eax, dword ptr [ebp - 8]
0x4881C1: cmp      dword ptr [eax + 0xc], 0xe
0x4881C5: jne      0x4881d8
0x4881C7: lea      eax, [ebp - 0xc]
0x4881CA: mov      edx, dword ptr [0x4a4194] ; *0x4A4194->0x4F7E54
0x4881D0: mov      edx, dword ptr [edx + 0x38]
0x4881D3: call     0x403af0
0x4881D8: mov      eax, dword ptr [ebp - 8]
0x4881DB: cmp      dword ptr [eax + 0xc], 0xf
0x4881DF: jne      0x4881f2
0x4881E1: lea      eax, [ebp - 0xc]
0x4881E4: mov      edx, dword ptr [0x4a4194] ; *0x4A4194->0x4F7E54
0x4881EA: mov      edx, dword ptr [edx + 0x3c]
0x4881ED: call     0x403af0
0x4881F2: mov      eax, dword ptr [ebp - 8]
0x4881F5: cmp      dword ptr [eax + 0xc], 0x10
0x4881F9: jne      0x48820c
0x4881FB: lea      eax, [ebp - 0xc]
0x4881FE: mov      edx, dword ptr [0x4a4194] ; *0x4A4194->0x4F7E54
0x488204: mov      edx, dword ptr [edx + 0x40]
0x488207: call     0x403af0
0x48820C: mov      eax, dword ptr [ebp - 8]
0x48820F: cmp      dword ptr [eax + 0xc], 0x11
0x488213: jne      0x488226
0x488215: lea      eax, [ebp - 0xc]
0x488218: mov      edx, dword ptr [0x4a4194] ; *0x4A4194->0x4F7E54
0x48821E: mov      edx, dword ptr [edx + 0x44]
0x488221: call     0x403af0
0x488226: mov      eax, dword ptr [ebp - 8]
0x488229: cmp      dword ptr [eax + 0xc], 0x12
0x48822D: jne      0x488240
0x48822F: lea      eax, [ebp - 0xc]
0x488232: mov      edx, dword ptr [0x4a4194] ; *0x4A4194->0x4F7E54
0x488238: mov      edx, dword ptr [edx + 0x48]
0x48823B: call     0x403af0
0x488240: mov      eax, dword ptr [ebp - 8]
0x488243: cmp      dword ptr [eax + 0xc], 0x13
0x488247: jne      0x48825a
0x488249: lea      eax, [ebp - 0xc]
0x48824C: mov      edx, dword ptr [0x4a4194] ; *0x4A4194->0x4F7E54
0x488252: mov      edx, dword ptr [edx + 0x4c]
0x488255: call     0x403af0
0x48825A: mov      eax, dword ptr [ebp - 8]
0x48825D: cmp      dword ptr [eax + 0xc], 0x14
0x488261: jne      0x488274
0x488263: lea      eax, [ebp - 0xc]
0x488266: mov      edx, dword ptr [0x4a4194] ; *0x4A4194->0x4F7E54
0x48826C: mov      edx, dword ptr [edx + 0x50]
0x48826F: call     0x403af0
0x488274: mov      eax, dword ptr [ebp - 8]
0x488277: cmp      dword ptr [eax + 0xc], 0x15
0x48827B: jne      0x48828e
0x48827D: lea      eax, [ebp - 0xc]
0x488280: mov      edx, dword ptr [0x4a4194] ; *0x4A4194->0x4F7E54
0x488286: mov      edx, dword ptr [edx + 0x54]
0x488289: call     0x403af0
0x48828E: mov      eax, dword ptr [ebp - 8]
0x488291: cmp      dword ptr [eax + 0xc], 0x16
0x488295: jne      0x4882a8
0x488297: lea      eax, [ebp - 0xc]
0x48829A: mov      edx, dword ptr [0x4a4194] ; *0x4A4194->0x4F7E54
0x4882A0: mov      edx, dword ptr [edx + 0x58]
0x4882A3: call     0x403af0
0x4882A8: mov      eax, dword ptr [ebp - 8]
0x4882AB: cmp      dword ptr [eax + 0xc], 0x17
0x4882AF: jne      0x4882c2
0x4882B1: lea      eax, [ebp - 0xc]
0x4882B4: mov      edx, dword ptr [0x4a4194] ; *0x4A4194->0x4F7E54
0x4882BA: mov      edx, dword ptr [edx + 0x5c]
0x4882BD: call     0x403af0
0x4882C2: mov      eax, dword ptr [ebp - 8]
0x4882C5: cmp      dword ptr [eax + 0xc], 0x18
0x4882C9: jne      0x4882dc
0x4882CB: lea      eax, [ebp - 0xc]
0x4882CE: mov      edx, dword ptr [0x4a4194] ; *0x4A4194->0x4F7E54
0x4882D4: mov      edx, dword ptr [edx + 0x60]
0x4882D7: call     0x403af0
0x4882DC: mov      eax, dword ptr [ebp - 8]
0x4882DF: cmp      dword ptr [eax + 0xc], 0x19
0x4882E3: jne      0x4882f6
0x4882E5: lea      eax, [ebp - 0xc]
0x4882E8: mov      edx, dword ptr [0x4a4194] ; *0x4A4194->0x4F7E54
0x4882EE: mov      edx, dword ptr [edx + 0x64]
0x4882F1: call     0x403af0
0x4882F6: mov      eax, dword ptr [ebp - 8]
0x4882F9: cmp      dword ptr [eax + 0xc], 0x1a
0x4882FD: jne      0x488310
0x4882FF: lea      eax, [ebp - 0xc]
0x488302: mov      edx, dword ptr [0x4a4194] ; *0x4A4194->0x4F7E54
0x488308: mov      edx, dword ptr [edx + 0x68]
0x48830B: call     0x403af0
0x488310: mov      eax, dword ptr [ebp - 8]
0x488313: cmp      dword ptr [eax + 0xc], 0x1b
0x488317: jne      0x48832a
0x488319: lea      eax, [ebp - 0xc]
0x48831C: mov      edx, dword ptr [0x4a4194] ; *0x4A4194->0x4F7E54
0x488322: mov      edx, dword ptr [edx + 0x6c]
0x488325: call     0x403af0
0x48832A: cmp      dword ptr [ebp - 0xc], 0
0x48832E: je       0x48835c
0x488330: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x488335: mov      eax, dword ptr [eax]
0x488337: mov      edx, dword ptr [ebp - 0xc]
0x48833A: call     0x464ec8
0x48833F: mov      eax, dword ptr [ebp - 4]
0x488342: mov      eax, dword ptr [eax + 0x480]
0x488348: xor      edx, edx
0x48834A: call     0x458214
0x48834F: mov      eax, dword ptr [0x4a4018] ; *0x4A4018->0x4F7EE4
0x488354: mov      edx, dword ptr [ebp - 0xc]
0x488357: call     0x403aac
0x48835C: xor      eax, eax
0x48835E: pop      edx
0x48835F: pop      ecx
0x488360: pop      ecx
0x488361: mov      dword ptr fs:[eax], edx
0x488364: push     0x488379
0x488369: lea      eax, [ebp - 0xc]
0x48836C: call     0x403a58
0x488371: ret      
0x488372: jmp      0x403518
0x488377: jmp      0x488369
0x488379: pop      ebx
0x48837A: mov      esp, ebp
0x48837C: pop      ebp
0x48837D: ret      4
0x488380: push     ebp
0x488381: mov      ebp, esp
0x488383: add      esp, -0x10
0x488386: push     ebx
0x488387: xor      ebx, ebx
0x488389: mov      dword ptr [ebp - 8], ebx
0x48838C: mov      dword ptr [ebp - 0x10], ecx
0x48838F: mov      dword ptr [ebp - 0xc], edx
0x488392: mov      dword ptr [ebp - 4], eax
0x488395: xor      eax, eax
0x488397: push     ebp
0x488398: push     0x4883f8
0x48839D: push     dword ptr fs:[eax]
0x4883A0: mov      dword ptr fs:[eax], esp
0x4883A3: lea      eax, [ebp - 8]
0x4883A6: mov      edx, 0x488410 ; "泅公辑滚"
0x4883AB: call     0x403af0
0x4883B0: cmp      dword ptr [ebp - 8], 0
0x4883B4: je       0x4883e2
0x4883B6: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4883BB: mov      eax, dword ptr [eax]
0x4883BD: mov      edx, dword ptr [ebp - 8]
0x4883C0: call     0x464ec8
0x4883C5: xor      edx, edx
0x4883C7: mov      eax, dword ptr [ebp - 4]
0x4883CA: mov      eax, dword ptr [eax + 0x480]
0x4883D0: call     0x458214
0x4883D5: mov      eax, dword ptr [0x4a4018] ; *0x4A4018->0x4F7EE4
0x4883DA: mov      edx, dword ptr [ebp - 8]
0x4883DD: call     0x403aac
0x4883E2: xor      eax, eax
0x4883E4: pop      edx
0x4883E5: pop      ecx
0x4883E6: pop      ecx
0x4883E7: mov      dword ptr fs:[eax], edx
0x4883EA: push     0x4883ff
0x4883EF: lea      eax, [ebp - 8]
0x4883F2: call     0x403a58
0x4883F7: ret      
0x4883F8: jmp      0x403518
0x4883FD: jmp      0x4883ef
0x4883FF: pop      ebx
0x488400: mov      esp, ebp
0x488402: pop      ebp
0x488403: ret      4
0x488406: add      byte ptr [eax], al
0x488410: [STR len=8] "泅公辑滚"
0x48841C: push     ebp
0x48841D: mov      ebp, esp
0x48841F: add      esp, -0xc
0x488422: mov      dword ptr [ebp - 0xc], ecx
0x488425: mov      dword ptr [ebp - 8], edx
0x488428: mov      dword ptr [ebp - 4], eax
0x48842B: xor      edx, edx
0x48842D: mov      eax, dword ptr [ebp - 4]
0x488430: mov      eax, dword ptr [eax + 0x480]
0x488436: call     0x458214
0x48843B: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x488440: mov      eax, dword ptr [eax]
0x488442: call     0x4405bc
0x488447: mov      esp, ebp
0x488449: pop      ebp
0x48844A: ret      4
0x48844D: lea      eax, [eax]
0x488450: push     ebp
0x488451: mov      ebp, esp
0x488453: add      esp, -0xc
0x488456: mov      dword ptr [ebp - 0xc], ecx
0x488459: mov      dword ptr [ebp - 8], edx
0x48845C: mov      dword ptr [ebp - 4], eax
0x48845F: mov      eax, dword ptr [0x4a4134] ; *0x4A4134->0x4F7D94
0x488464: mov      eax, dword ptr [eax]
0x488466: call     0x47e044
0x48846B: mov      esp, ebp
0x48846D: pop      ebp
0x48846E: ret      4
0x488471: lea      eax, [eax]
0x488474: push     ebp
0x488475: mov      ebp, esp
0x488477: add      esp, -0xc
0x48847A: mov      dword ptr [ebp - 0xc], ecx
0x48847D: mov      dword ptr [ebp - 8], edx
0x488480: mov      dword ptr [ebp - 4], eax
0x488483: mov      eax, dword ptr [0x4a4134] ; *0x4A4134->0x4F7D94
0x488488: mov      eax, dword ptr [eax]
0x48848A: call     0x47e434
0x48848F: mov      esp, ebp
0x488491: pop      ebp
0x488492: ret      4
0x488495: lea      eax, [eax]
0x488498: push     ebp
0x488499: mov      ebp, esp
0x48849B: add      esp, -0x34
0x48849E: push     ebx
0x48849F: xor      ebx, ebx
0x4884A1: mov      dword ptr [ebp - 0x34], ebx
0x4884A4: mov      dword ptr [ebp - 8], ecx
0x4884A7: mov      dword ptr [ebp - 0x20], edx
0x4884AA: mov      dword ptr [ebp - 4], eax
0x4884AD: xor      eax, eax
0x4884AF: push     ebp
0x4884B0: push     0x4885eb
0x4884B5: push     dword ptr fs:[eax]
0x4884B8: mov      dword ptr fs:[eax], esp
0x4884BB: mov      eax, dword ptr [ebp - 8]
0x4884BE: call     0x44d8b4
0x4884C3: mov      dword ptr [ebp - 0x14], eax
0x4884C6: mov      eax, dword ptr [ebp - 4]
0x4884C9: mov      eax, dword ptr [eax + 0x304]
0x4884CF: mov      dword ptr [ebp - 0x18], eax
0x4884D2: mov      eax, dword ptr [ebp - 0x18]
0x4884D5: mov      edx, dword ptr [eax + 0x258]
0x4884DB: mov      eax, dword ptr [ebp - 4]
0x4884DE: mov      eax, dword ptr [eax + 0x3d0]
0x4884E4: mov      eax, dword ptr [eax + 0x254]
0x4884EA: call     0x45351c
0x4884EF: mov      dword ptr [ebp - 0xc], eax
0x4884F2: cmp      dword ptr [ebp - 0xc], 0
0x4884F6: je       0x488535
0x4884F8: lea      edx, [ebp - 0x30]
0x4884FB: mov      eax, dword ptr [ebp - 0xc]
0x4884FE: call     0x44d8ec
0x488503: lea      eax, [ebp - 0x30]
0x488506: push     eax
0x488507: mov      eax, dword ptr [ebp - 0xc]
0x48850A: push     eax
0x48850B: push     1
0x48850D: mov      eax, dword ptr [ebp - 0x18]
0x488510: mov      edx, dword ptr [eax + 0x34]
0x488513: mov      eax, dword ptr [ebp - 0x18]
0x488516: call     0x456fdc
0x48851B: push     eax
0x48851C: mov      eax, dword ptr [ebp - 0x18]
0x48851F: mov      edx, dword ptr [eax + 0x30]
0x488522: mov      eax, dword ptr [ebp - 0x18]
0x488525: call     0x456f94
0x48852A: mov      edx, eax
0x48852C: mov      eax, dword ptr [ebp - 8]
0x48852F: pop      ecx
0x488530: call     0x44d6cc
0x488535: push     1
0x488537: mov      eax, dword ptr [ebp - 0x14]
0x48853A: call     0x41834c
0x48853F: push     eax
0x488540: call     0x406434
0x488545: mov      eax, dword ptr [ebp - 0x14]
0x488548: mov      eax, dword ptr [eax + 0xc]
0x48854B: mov      edx, 0xc0c0c0
0x488550: call     0x4173fc
0x488555: mov      eax, dword ptr [ebp - 4]
0x488558: mov      eax, dword ptr [eax + 0x77c]
0x48855E: mov      edx, dword ptr [eax]
0x488560: call     dword ptr [edx + 0x14]
0x488563: dec      eax
0x488564: test     eax, eax
0x488566: jl       0x4885aa
0x488568: inc      eax
0x488569: mov      dword ptr [ebp - 0x1c], eax
0x48856C: mov      dword ptr [ebp - 0x10], 0
0x488573: lea      ecx, [ebp - 0x34]
0x488576: mov      eax, dword ptr [ebp - 4]
0x488579: mov      eax, dword ptr [eax + 0x77c]
0x48857F: mov      edx, dword ptr [ebp - 0x10]
0x488582: mov      ebx, dword ptr [eax]
0x488584: call     dword ptr [ebx + 0xc]
0x488587: mov      eax, dword ptr [ebp - 0x34]
0x48858A: push     eax
0x48858B: imul     ecx, dword ptr [ebp - 0x10], 0xe
0x48858F: add      ecx, 0xbc
0x488595: mov      edx, 0x1db
0x48859A: mov      eax, dword ptr [ebp - 0x14]
0x48859D: call     0x4180dc
0x4885A2: inc      dword ptr [ebp - 0x10]
0x4885A5: dec      dword ptr [ebp - 0x1c]
0x4885A8: jne      0x488573
0x4885AA: push     0xffffff
0x4885AF: push     0
0x4885B1: mov      eax, dword ptr [ebp - 4]
0x4885B4: mov      eax, dword ptr [eax + 0x780]
0x4885BA: push     eax
0x4885BB: mov      ecx, 0x79
0x4885C0: mov      edx, 0x16a
0x4885C5: mov      eax, dword ptr [ebp - 8]
0x4885C8: call     0x4a09a0
0x4885CD: mov      eax, dword ptr [ebp - 0x14]
0x4885D0: call     0x44d104
0x4885D5: xor      eax, eax
0x4885D7: pop      edx
0x4885D8: pop      ecx
0x4885D9: pop      ecx
0x4885DA: mov      dword ptr fs:[eax], edx
0x4885DD: push     0x4885f2
0x4885E2: lea      eax, [ebp - 0x34]
0x4885E5: call     0x403a58
0x4885EA: ret      
0x4885EB: jmp      0x403518
0x4885F0: jmp      0x4885e2
0x4885F2: pop      ebx
0x4885F3: mov      esp, ebp
0x4885F5: pop      ebp
0x4885F6: ret      
0x4885F7: nop      
0x4885F8: push     ebp
0x4885F9: mov      ebp, esp
0x4885FB: add      esp, -0xc
0x4885FE: mov      dword ptr [ebp - 0xc], ecx
0x488601: mov      dword ptr [ebp - 8], edx
0x488604: mov      dword ptr [ebp - 4], eax
0x488607: mov      eax, dword ptr [ebp - 8]
0x48860A: mov      edx, dword ptr [ebp - 4]
0x48860D: cmp      eax, dword ptr [edx + 0x3c0]
0x488613: jne      0x488621
0x488615: mov      eax, dword ptr [0x4a4134] ; *0x4A4134->0x4F7D94
0x48861A: mov      eax, dword ptr [eax]
0x48861C: call     0x47e454
0x488621: mov      eax, dword ptr [ebp - 8]
0x488624: mov      edx, dword ptr [ebp - 4]
0x488627: cmp      eax, dword ptr [edx + 0x3c4]
0x48862D: jne      0x48863b
0x48862F: mov      eax, dword ptr [0x4a4134] ; *0x4A4134->0x4F7D94
0x488634: mov      eax, dword ptr [eax]
0x488636: call     0x47e560
0x48863B: mov      esp, ebp
0x48863D: pop      ebp
0x48863E: ret      4
0x488641: lea      eax, [eax]
0x488644: push     ebp
0x488645: mov      ebp, esp
0x488647: add      esp, -0x24
0x48864A: mov      dword ptr [ebp - 0xc], ecx
0x48864D: mov      dword ptr [ebp - 8], edx
0x488650: mov      dword ptr [ebp - 4], eax
0x488653: mov      eax, dword ptr [ebp - 8]
0x488656: mov      edx, dword ptr [0x456468]
0x48865C: call     0x403034
0x488661: mov      dword ptr [ebp - 0x14], eax
0x488664: mov      eax, dword ptr [ebp - 0x14]
0x488667: cmp      byte ptr [eax + 0x274], 0
0x48866E: je       0x4886b9
0x488670: mov      eax, dword ptr [ebp - 0x14]
0x488673: mov      edx, dword ptr [eax + 0x258]
0x488679: mov      eax, dword ptr [ebp - 0x14]
0x48867C: mov      eax, dword ptr [eax + 0x254]
0x488682: call     0x45351c
0x488687: mov      dword ptr [ebp - 0x10], eax
0x48868A: cmp      dword ptr [ebp - 0x10], 0
0x48868E: je       0x4886b9
0x488690: lea      edx, [ebp - 0x24]
0x488693: mov      eax, dword ptr [ebp - 0x10]
0x488696: call     0x44d8ec
0x48869B: lea      eax, [ebp - 0x24]
0x48869E: push     eax
0x48869F: mov      eax, dword ptr [ebp - 0x10]
0x4886A2: push     eax
0x4886A3: push     1
0x4886A5: mov      eax, dword ptr [ebp - 0x14]
0x4886A8: mov      ecx, dword ptr [eax + 0x34]
0x4886AB: mov      eax, dword ptr [ebp - 0x14]
0x4886AE: mov      edx, dword ptr [eax + 0x30]
0x4886B1: mov      eax, dword ptr [ebp - 0xc]
0x4886B4: call     0x44d6cc
0x4886B9: mov      esp, ebp
0x4886BB: pop      ebp
0x4886BC: ret      
0x4886BD: lea      eax, [eax]
0x4886C0: push     ebp
0x4886C1: mov      ebp, esp
0x4886C3: add      esp, -0xc
0x4886C6: mov      dword ptr [ebp - 0xc], ecx
0x4886C9: mov      dword ptr [ebp - 8], edx
0x4886CC: mov      dword ptr [ebp - 4], eax
0x4886CF: mov      eax, dword ptr [ebp - 8]
0x4886D2: mov      edx, dword ptr [ebp - 4]
0x4886D5: cmp      eax, dword ptr [edx + 0x318]
0x4886DB: jne      0x4886e9
0x4886DD: mov      eax, dword ptr [0x4a4054] ; *0x4A4054->0x4F7D98
0x4886E2: mov      eax, dword ptr [eax]
0x4886E4: call     0x47e7cc
0x4886E9: mov      eax, dword ptr [ebp - 8]
0x4886EC: mov      edx, dword ptr [ebp - 4]
0x4886EF: cmp      eax, dword ptr [edx + 0x31c]
0x4886F5: jne      0x488703
0x4886F7: mov      eax, dword ptr [0x4a4054] ; *0x4A4054->0x4F7D98
0x4886FC: mov      eax, dword ptr [eax]
0x4886FE: call     0x47e840
0x488703: mov      eax, dword ptr [ebp - 8]
0x488706: mov      edx, dword ptr [ebp - 4]
0x488709: cmp      eax, dword ptr [edx + 0x320]
0x48870F: jne      0x48871d
0x488711: mov      eax, dword ptr [0x4a4054] ; *0x4A4054->0x4F7D98
0x488716: mov      eax, dword ptr [eax]
0x488718: call     0x47e8bc
0x48871D: mov      eax, dword ptr [ebp - 8]
0x488720: mov      edx, dword ptr [ebp - 4]
0x488723: cmp      eax, dword ptr [edx + 0x324]
0x488729: jne      0x488737
0x48872B: mov      eax, dword ptr [0x4a4054] ; *0x4A4054->0x4F7D98
0x488730: mov      eax, dword ptr [eax]
0x488732: call     0x47e9f0
0x488737: mov      eax, dword ptr [ebp - 8]
0x48873A: mov      edx, dword ptr [ebp - 4]
0x48873D: cmp      eax, dword ptr [edx + 0x328]
0x488743: jne      0x488751
0x488745: mov      eax, dword ptr [0x4a4054] ; *0x4A4054->0x4F7D98
0x48874A: mov      eax, dword ptr [eax]
0x48874C: call     0x47ea80
0x488751: mov      eax, dword ptr [ebp - 8]
0x488754: mov      edx, dword ptr [ebp - 4]
0x488757: cmp      eax, dword ptr [edx + 0x32c]
0x48875D: jne      0x48876b
0x48875F: mov      eax, dword ptr [0x4a4054] ; *0x4A4054->0x4F7D98
0x488764: mov      eax, dword ptr [eax]
0x488766: call     0x47ec3c
0x48876B: mov      eax, dword ptr [ebp - 8]
0x48876E: mov      edx, dword ptr [ebp - 4]
0x488771: cmp      eax, dword ptr [edx + 0x330]
0x488777: jne      0x488785
0x488779: mov      eax, dword ptr [0x4a4054] ; *0x4A4054->0x4F7D98
0x48877E: mov      eax, dword ptr [eax]
0x488780: call     0x47ec48
0x488785: mov      esp, ebp
0x488787: pop      ebp
0x488788: ret      4
0x48878B: nop      
0x48878C: push     ebp
0x48878D: mov      ebp, esp
0x48878F: add      esp, -0x24
0x488792: mov      dword ptr [ebp - 0xc], ecx
0x488795: mov      dword ptr [ebp - 8], edx
0x488798: mov      dword ptr [ebp - 4], eax
0x48879B: mov      eax, dword ptr [ebp - 8]
0x48879E: mov      edx, dword ptr [0x456468]
0x4887A4: call     0x403034
0x4887A9: mov      dword ptr [ebp - 0x14], eax
0x4887AC: mov      eax, dword ptr [ebp - 0x14]
0x4887AF: cmp      byte ptr [eax + 0x274], 0
0x4887B6: je       0x48881e
0x4887B8: mov      eax, dword ptr [ebp - 0x14]
0x4887BB: mov      edx, dword ptr [eax + 0x258]
0x4887C1: mov      eax, dword ptr [ebp - 0x14]
0x4887C4: mov      eax, dword ptr [eax + 0x254]
0x4887CA: call     0x45351c
0x4887CF: mov      dword ptr [ebp - 0x10], eax
0x4887D2: cmp      dword ptr [ebp - 0x10], 0
0x4887D6: je       0x4889ba
0x4887DC: lea      edx, [ebp - 0x24]
0x4887DF: mov      eax, dword ptr [ebp - 0x10]
0x4887E2: call     0x44d8ec
0x4887E7: lea      eax, [ebp - 0x24]
0x4887EA: push     eax
0x4887EB: mov      eax, dword ptr [ebp - 0x10]
0x4887EE: push     eax
0x4887EF: push     1
0x4887F1: mov      eax, dword ptr [ebp - 0x14]
0x4887F4: mov      edx, dword ptr [eax + 0x34]
0x4887F7: mov      eax, dword ptr [ebp - 0x14]
0x4887FA: call     0x456fdc
0x4887FF: push     eax
0x488800: mov      eax, dword ptr [ebp - 0x14]
0x488803: mov      edx, dword ptr [eax + 0x30]
0x488806: mov      eax, dword ptr [ebp - 0x14]
0x488809: call     0x456f94
0x48880E: mov      edx, eax
0x488810: mov      eax, dword ptr [ebp - 0xc]
0x488813: pop      ecx
0x488814: call     0x44d6cc
0x488819: jmp      0x4889ba
0x48881E: xor      eax, eax
0x488820: mov      dword ptr [ebp - 0x10], eax
0x488823: mov      eax, dword ptr [ebp - 4]
0x488826: mov      eax, dword ptr [eax + 0x338]
0x48882C: cmp      eax, dword ptr [ebp - 8]
0x48882F: jne      0x488867
0x488831: mov      eax, dword ptr [0x4a4054] ; *0x4A4054->0x4F7D98
0x488836: mov      eax, dword ptr [eax]
0x488838: mov      eax, dword ptr [eax + 0x14]
0x48883B: mov      edx, eax
0x48883D: shl      eax, 3
0x488840: sub      eax, edx
0x488842: mov      edx, dword ptr [0x4a4054] ; *0x4A4054->0x4F7D98
0x488848: mov      edx, dword ptr [edx]
0x48884A: cmp      byte ptr [edx + eax*8 + 0x2d], 0
0x48884F: jne      0x488867
0x488851: mov      edx, 0x37
0x488856: mov      eax, dword ptr [ebp - 0x14]
0x488859: mov      eax, dword ptr [eax + 0x254]
0x48885F: call     0x45351c
0x488864: mov      dword ptr [ebp - 0x10], eax
0x488867: mov      eax, dword ptr [ebp - 4]
0x48886A: mov      eax, dword ptr [eax + 0x33c]
0x488870: cmp      eax, dword ptr [ebp - 8]
0x488873: jne      0x4888ab
0x488875: mov      eax, dword ptr [0x4a4054] ; *0x4A4054->0x4F7D98
0x48887A: mov      eax, dword ptr [eax]
0x48887C: mov      eax, dword ptr [eax + 0x14]
0x48887F: mov      edx, eax
0x488881: shl      eax, 3
0x488884: sub      eax, edx
0x488886: mov      edx, dword ptr [0x4a4054] ; *0x4A4054->0x4F7D98
0x48888C: mov      edx, dword ptr [edx]
0x48888E: cmp      byte ptr [edx + eax*8 + 0x2d], 1
0x488893: jne      0x4888ab
0x488895: mov      edx, 0x38
0x48889A: mov      eax, dword ptr [ebp - 0x14]
0x48889D: mov      eax, dword ptr [eax + 0x254]
0x4888A3: call     0x45351c
0x4888A8: mov      dword ptr [ebp - 0x10], eax
0x4888AB: mov      eax, dword ptr [ebp - 4]
0x4888AE: mov      eax, dword ptr [eax + 0x340]
0x4888B4: cmp      eax, dword ptr [ebp - 8]
0x4888B7: jne      0x4888ef
0x4888B9: mov      eax, dword ptr [0x4a4054] ; *0x4A4054->0x4F7D98
0x4888BE: mov      eax, dword ptr [eax]
0x4888C0: mov      eax, dword ptr [eax + 0x14]
0x4888C3: mov      edx, eax
0x4888C5: shl      eax, 3
0x4888C8: sub      eax, edx
0x4888CA: mov      edx, dword ptr [0x4a4054] ; *0x4A4054->0x4F7D98
0x4888D0: mov      edx, dword ptr [edx]
0x4888D2: cmp      byte ptr [edx + eax*8 + 0x2d], 2
0x4888D7: jne      0x4888ef
0x4888D9: mov      edx, 0x39
0x4888DE: mov      eax, dword ptr [ebp - 0x14]
0x4888E1: mov      eax, dword ptr [eax + 0x254]
0x4888E7: call     0x45351c
0x4888EC: mov      dword ptr [ebp - 0x10], eax
0x4888EF: mov      eax, dword ptr [ebp - 4]
0x4888F2: mov      eax, dword ptr [eax + 0x348]
0x4888F8: cmp      eax, dword ptr [ebp - 8]
0x4888FB: jne      0x488933
0x4888FD: mov      eax, dword ptr [0x4a4054] ; *0x4A4054->0x4F7D98
0x488902: mov      eax, dword ptr [eax]
0x488904: mov      eax, dword ptr [eax + 0x14]
0x488907: mov      edx, eax
0x488909: shl      eax, 3
0x48890C: sub      eax, edx
0x48890E: mov      edx, dword ptr [0x4a4054] ; *0x4A4054->0x4F7D98
0x488914: mov      edx, dword ptr [edx]
0x488916: cmp      byte ptr [edx + eax*8 + 0x2b], 0
0x48891B: jne      0x488933
0x48891D: mov      edx, 0x3a
0x488922: mov      eax, dword ptr [ebp - 0x14]
0x488925: mov      eax, dword ptr [eax + 0x254]
0x48892B: call     0x45351c
0x488930: mov      dword ptr [ebp - 0x10], eax
0x488933: mov      eax, dword ptr [ebp - 4]
0x488936: mov      eax, dword ptr [eax + 0x34c]
0x48893C: cmp      eax, dword ptr [ebp - 8]
0x48893F: jne      0x488977
0x488941: mov      eax, dword ptr [0x4a4054] ; *0x4A4054->0x4F7D98
0x488946: mov      eax, dword ptr [eax]
0x488948: mov      eax, dword ptr [eax + 0x14]
0x48894B: mov      edx, eax
0x48894D: shl      eax, 3
0x488950: sub      eax, edx
0x488952: mov      edx, dword ptr [0x4a4054] ; *0x4A4054->0x4F7D98
0x488958: mov      edx, dword ptr [edx]
0x48895A: cmp      byte ptr [edx + eax*8 + 0x2b], 1
0x48895F: jne      0x488977
0x488961: mov      edx, 0x3b
0x488966: mov      eax, dword ptr [ebp - 0x14]
0x488969: mov      eax, dword ptr [eax + 0x254]
0x48896F: call     0x45351c
0x488974: mov      dword ptr [ebp - 0x10], eax
0x488977: cmp      dword ptr [ebp - 0x10], 0
0x48897B: je       0x4889ba
0x48897D: lea      edx, [ebp - 0x24]
0x488980: mov      eax, dword ptr [ebp - 0x10]
0x488983: call     0x44d8ec
0x488988: lea      eax, [ebp - 0x24]
0x48898B: push     eax
0x48898C: mov      eax, dword ptr [ebp - 0x10]
0x48898F: push     eax
0x488990: push     1
0x488992: mov      eax, dword ptr [ebp - 0x14]
0x488995: mov      edx, dword ptr [eax + 0x34]
0x488998: mov      eax, dword ptr [ebp - 0x14]
0x48899B: call     0x456fdc
0x4889A0: push     eax
0x4889A1: mov      eax, dword ptr [ebp - 0x14]
0x4889A4: mov      edx, dword ptr [eax + 0x30]
0x4889A7: mov      eax, dword ptr [ebp - 0x14]
0x4889AA: call     0x456f94
0x4889AF: mov      edx, eax
0x4889B1: mov      eax, dword ptr [ebp - 0xc]
0x4889B4: pop      ecx
0x4889B5: call     0x44d6cc
0x4889BA: mov      esp, ebp
0x4889BC: pop      ebp
0x4889BD: ret      
0x4889BE: mov      eax, eax
0x4889C0: push     ebp
0x4889C1: mov      ebp, esp
0x4889C3: add      esp, -0xc
0x4889C6: mov      dword ptr [ebp - 0xc], ecx
0x4889C9: mov      dword ptr [ebp - 8], edx
0x4889CC: mov      dword ptr [ebp - 4], eax
0x4889CF: mov      eax, dword ptr [ebp - 8]
0x4889D2: mov      edx, dword ptr [ebp - 4]
0x4889D5: cmp      eax, dword ptr [edx + 0x35c]
0x4889DB: jne      0x4889e9
0x4889DD: mov      eax, dword ptr [0x4a4054] ; *0x4A4054->0x4F7D98
0x4889E2: mov      eax, dword ptr [eax]
0x4889E4: call     0x47f00c
0x4889E9: mov      eax, dword ptr [ebp - 8]
0x4889EC: mov      edx, dword ptr [ebp - 4]
0x4889EF: cmp      eax, dword ptr [edx + 0x338]
0x4889F5: jne      0x488a05
0x4889F7: mov      eax, dword ptr [0x4a4054] ; *0x4A4054->0x4F7D98
0x4889FC: mov      eax, dword ptr [eax]
0x4889FE: xor      edx, edx
0x488A00: call     0x47f1a4
0x488A05: mov      eax, dword ptr [ebp - 8]
0x488A08: mov      edx, dword ptr [ebp - 4]
0x488A0B: cmp      eax, dword ptr [edx + 0x33c]
0x488A11: jne      0x488a24
0x488A13: mov      eax, dword ptr [0x4a4054] ; *0x4A4054->0x4F7D98
0x488A18: mov      eax, dword ptr [eax]
0x488A1A: mov      edx, 1
0x488A1F: call     0x47f1a4
0x488A24: mov      eax, dword ptr [ebp - 8]
0x488A27: mov      edx, dword ptr [ebp - 4]
0x488A2A: cmp      eax, dword ptr [edx + 0x340]
0x488A30: jne      0x488a43
0x488A32: mov      eax, dword ptr [0x4a4054] ; *0x4A4054->0x4F7D98
0x488A37: mov      eax, dword ptr [eax]
0x488A39: mov      edx, 2
0x488A3E: call     0x47f1a4
0x488A43: mov      eax, dword ptr [ebp - 8]
0x488A46: mov      edx, dword ptr [ebp - 4]
0x488A49: cmp      eax, dword ptr [edx + 0x344]
0x488A4F: jne      0x488a62
0x488A51: mov      eax, dword ptr [0x4a4054] ; *0x4A4054->0x4F7D98
0x488A56: mov      eax, dword ptr [eax]
0x488A58: mov      edx, 3
0x488A5D: call     0x47f1a4
0x488A62: mov      eax, dword ptr [ebp - 8]
0x488A65: mov      edx, dword ptr [ebp - 4]
0x488A68: cmp      eax, dword ptr [edx + 0x348]
0x488A6E: jne      0x488a7e
0x488A70: mov      eax, dword ptr [0x4a4054] ; *0x4A4054->0x4F7D98
0x488A75: mov      eax, dword ptr [eax]
0x488A77: xor      edx, edx
0x488A79: call     0x47f1fc
0x488A7E: mov      eax, dword ptr [ebp - 8]
0x488A81: mov      edx, dword ptr [ebp - 4]
0x488A84: cmp      eax, dword ptr [edx + 0x34c]
0x488A8A: jne      0x488a9d
0x488A8C: mov      eax, dword ptr [0x4a4054] ; *0x4A4054->0x4F7D98
0x488A91: mov      eax, dword ptr [eax]
0x488A93: mov      edx, 1
0x488A98: call     0x47f1fc
0x488A9D: mov      eax, dword ptr [ebp - 8]
0x488AA0: mov      edx, dword ptr [ebp - 4]
0x488AA3: cmp      eax, dword ptr [edx + 0x350]
0x488AA9: jne      0x488ab7
0x488AAB: mov      eax, dword ptr [0x4a4054] ; *0x4A4054->0x4F7D98
0x488AB0: mov      eax, dword ptr [eax]
0x488AB2: call     0x47f24c
0x488AB7: mov      eax, dword ptr [ebp - 8]
0x488ABA: mov      edx, dword ptr [ebp - 4]
0x488ABD: cmp      eax, dword ptr [edx + 0x354]
0x488AC3: jne      0x488ad1
0x488AC5: mov      eax, dword ptr [0x4a4054] ; *0x4A4054->0x4F7D98
0x488ACA: mov      eax, dword ptr [eax]
0x488ACC: call     0x47f258
0x488AD1: mov      eax, dword ptr [ebp - 8]
0x488AD4: mov      edx, dword ptr [ebp - 4]
0x488AD7: cmp      eax, dword ptr [edx + 0x358]
0x488ADD: jne      0x488aeb
0x488ADF: mov      eax, dword ptr [0x4a4054] ; *0x4A4054->0x4F7D98
0x488AE4: mov      eax, dword ptr [eax]
0x488AE6: call     0x47f06c
0x488AEB: mov      esp, ebp
0x488AED: pop      ebp
0x488AEE: ret      4
0x488AF1: lea      eax, [eax]
0x488AF4: push     ebp
0x488AF5: mov      ebp, esp
0x488AF7: add      esp, 0xffffff54
0x488AFD: push     ebx
0x488AFE: push     esi
0x488AFF: push     edi
0x488B00: xor      ebx, ebx
0x488B02: mov      dword ptr [ebp - 0x94], ebx
0x488B08: mov      dword ptr [ebp - 0x98], ebx
0x488B0E: mov      dword ptr [ebp - 0x9c], ebx
0x488B14: mov      dword ptr [ebp - 0x50], ebx
0x488B17: mov      dword ptr [ebp - 0x54], ebx
0x488B1A: mov      dword ptr [ebp - 0x58], ebx
0x488B1D: mov      dword ptr [ebp - 0x5c], ebx
0x488B20: mov      dword ptr [ebp - 8], ecx
0x488B23: mov      dword ptr [ebp - 0x80], edx
0x488B26: mov      dword ptr [ebp - 4], eax
0x488B29: xor      eax, eax
0x488B2B: push     ebp
0x488B2C: push     0x489fd1
0x488B31: push     dword ptr fs:[eax]
0x488B34: mov      dword ptr fs:[eax], esp
0x488B37: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x488B3C: cmp      dword ptr [eax], 0
0x488B3F: je       0x489fa6
0x488B45: mov      eax, dword ptr [ebp - 4]
0x488B48: mov      eax, dword ptr [eax + 0x2c4]
0x488B4E: mov      dword ptr [ebp - 0x64], eax
0x488B51: mov      eax, dword ptr [ebp - 0x64]
0x488B54: mov      edx, dword ptr [eax + 0x258]
0x488B5A: mov      eax, dword ptr [ebp - 0x64]
0x488B5D: mov      eax, dword ptr [eax + 0x254]
0x488B63: call     0x45351c
0x488B68: mov      dword ptr [ebp - 0x40], eax
0x488B6B: cmp      dword ptr [ebp - 0x40], 0
0x488B6F: je       0x488bb4
0x488B71: lea      edx, [ebp - 0x90]
0x488B77: mov      eax, dword ptr [ebp - 0x40]
0x488B7A: call     0x44d8ec
0x488B7F: lea      eax, [ebp - 0x90]
0x488B85: push     eax
0x488B86: mov      eax, dword ptr [ebp - 0x40]
0x488B89: push     eax
0x488B8A: push     1
0x488B8C: mov      eax, dword ptr [ebp - 0x64]
0x488B8F: mov      edx, dword ptr [eax + 0x34]
0x488B92: mov      eax, dword ptr [ebp - 0x64]
0x488B95: call     0x456fdc
0x488B9A: push     eax
0x488B9B: mov      eax, dword ptr [ebp - 0x64]
0x488B9E: mov      edx, dword ptr [eax + 0x30]
0x488BA1: mov      eax, dword ptr [ebp - 0x64]
0x488BA4: call     0x456f94
0x488BA9: mov      edx, eax
0x488BAB: mov      eax, dword ptr [ebp - 8]
0x488BAE: pop      ecx
0x488BAF: call     0x44d6cc
0x488BB4: mov      eax, dword ptr [ebp - 4]
0x488BB7: mov      eax, dword ptr [eax + 0x618]
0x488BBD: sub      eax, 1
0x488BC0: jb       0x488bdb
0x488BC2: je       0x488ec5
0x488BC8: dec      eax
0x488BC9: je       0x489293
0x488BCF: dec      eax
0x488BD0: je       0x48987b
0x488BD6: jmp      0x489cbd
0x488BDB: mov      dword ptr [ebp - 0x18], 0x178
0x488BE2: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x488BE7: cmp      dword ptr [eax], 0
0x488BEA: je       0x488c00
0x488BEC: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x488BF1: mov      eax, dword ptr [eax]
0x488BF3: cmp      byte ptr [eax + 0xd], 1
0x488BF7: jne      0x488c00
0x488BF9: mov      dword ptr [ebp - 0x18], 0x179
0x488C00: mov      eax, dword ptr [ebp - 0x64]
0x488C03: mov      eax, dword ptr [eax + 0x30]
0x488C06: add      eax, 0x26
0x488C09: mov      dword ptr [ebp - 0x20], eax
0x488C0C: mov      eax, dword ptr [ebp - 0x64]
0x488C0F: mov      eax, dword ptr [eax + 0x34]
0x488C12: add      eax, 0x34
0x488C15: mov      dword ptr [ebp - 0x24], eax
0x488C18: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x488C1D: mov      eax, dword ptr [eax]
0x488C1F: mov      eax, dword ptr [eax + 0x308]
0x488C25: mov      edx, dword ptr [ebp - 0x18]
0x488C28: call     0x45351c
0x488C2D: mov      dword ptr [ebp - 0x40], eax
0x488C30: cmp      dword ptr [ebp - 0x40], 0
0x488C34: je       0x488c73
0x488C36: lea      edx, [ebp - 0x90]
0x488C3C: mov      eax, dword ptr [ebp - 0x40]
0x488C3F: call     0x44d8ec
0x488C44: lea      eax, [ebp - 0x90]
0x488C4A: push     eax
0x488C4B: mov      eax, dword ptr [ebp - 0x40]
0x488C4E: push     eax
0x488C4F: push     0
0x488C51: mov      edx, dword ptr [ebp - 0x24]
0x488C54: mov      eax, dword ptr [ebp - 0x64]
0x488C57: call     0x456fdc
0x488C5C: push     eax
0x488C5D: mov      edx, dword ptr [ebp - 0x20]
0x488C60: mov      eax, dword ptr [ebp - 0x64]
0x488C63: call     0x456f94
0x488C68: mov      edx, eax
0x488C6A: mov      eax, dword ptr [ebp - 8]
0x488C6D: pop      ecx
0x488C6E: call     0x44d6cc
0x488C73: sub      dword ptr [ebp - 0x20], 7
0x488C77: add      dword ptr [ebp - 0x24], 0x2c
0x488C7B: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x488C80: mov      eax, dword ptr [eax]
0x488C82: movzx    eax, byte ptr [eax + 0xf]
0x488C86: shr      eax, 1
0x488C88: add      eax, 0x1b8
0x488C8D: mov      dword ptr [ebp - 0x2c], eax
0x488C90: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x488C95: mov      eax, dword ptr [eax]
0x488C97: cmp      byte ptr [eax + 0xd], 1
0x488C9B: jne      0x488cb2
0x488C9D: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x488CA2: mov      eax, dword ptr [eax]
0x488CA4: movzx    eax, byte ptr [eax + 0xf]
0x488CA8: shr      eax, 1
0x488CAA: add      eax, 0x1e0
0x488CAF: mov      dword ptr [ebp - 0x2c], eax
0x488CB2: cmp      dword ptr [ebp - 0x2c], 0
0x488CB6: jle      0x488d20
0x488CB8: lea      eax, [ebp - 0x34]
0x488CBB: push     eax
0x488CBC: lea      ecx, [ebp - 0x30]
0x488CBF: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x488CC4: mov      eax, dword ptr [eax]
0x488CC6: mov      eax, dword ptr [eax + 0x308]
0x488CCC: mov      edx, dword ptr [ebp - 0x2c]
0x488CCF: call     0x453a48
0x488CD4: mov      dword ptr [ebp - 0x40], eax
0x488CD7: cmp      dword ptr [ebp - 0x40], 0
0x488CDB: je       0x488d20
0x488CDD: lea      edx, [ebp - 0x90]
0x488CE3: mov      eax, dword ptr [ebp - 0x40]
0x488CE6: call     0x44d8ec
0x488CEB: lea      eax, [ebp - 0x90]
0x488CF1: push     eax
0x488CF2: mov      eax, dword ptr [ebp - 0x40]
0x488CF5: push     eax
0x488CF6: push     1
0x488CF8: mov      edx, dword ptr [ebp - 0x24]
0x488CFB: add      edx, dword ptr [ebp - 0x34]
0x488CFE: mov      eax, dword ptr [ebp - 0x64]
0x488D01: call     0x456fdc
0x488D06: push     eax
0x488D07: mov      edx, dword ptr [ebp - 0x20]
0x488D0A: add      edx, dword ptr [ebp - 0x30]
0x488D0D: mov      eax, dword ptr [ebp - 0x64]
0x488D10: call     0x456f94
0x488D15: mov      edx, eax
0x488D17: mov      eax, dword ptr [ebp - 8]
0x488D1A: pop      ecx
0x488D1B: call     0x44d6cc
0x488D20: mov      eax, dword ptr [0x4a3db0] ; *0x4A3DB0->0x4F7EFC
0x488D25: cmp      byte ptr [eax], 0
0x488D28: je       0x488da4
0x488D2A: mov      eax, dword ptr [0x4a3db0] ; *0x4A3DB0->0x4F7EFC
0x488D2F: movzx    eax, word ptr [eax + 0x16]
0x488D33: mov      dword ptr [ebp - 0x2c], eax
0x488D36: cmp      dword ptr [ebp - 0x2c], 0
0x488D3A: jl       0x488da4
0x488D3C: lea      eax, [ebp - 0x34]
0x488D3F: push     eax
0x488D40: lea      ecx, [ebp - 0x30]
0x488D43: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x488D48: mov      eax, dword ptr [eax]
0x488D4A: mov      eax, dword ptr [eax + 0x32c]
0x488D50: mov      edx, dword ptr [ebp - 0x2c]
0x488D53: call     0x453a48
0x488D58: mov      dword ptr [ebp - 0x40], eax
0x488D5B: cmp      dword ptr [ebp - 0x40], 0
0x488D5F: je       0x488da4
0x488D61: lea      edx, [ebp - 0x90]
0x488D67: mov      eax, dword ptr [ebp - 0x40]
0x488D6A: call     0x44d8ec
0x488D6F: lea      eax, [ebp - 0x90]
0x488D75: push     eax
0x488D76: mov      eax, dword ptr [ebp - 0x40]
0x488D79: push     eax
0x488D7A: push     1
0x488D7C: mov      edx, dword ptr [ebp - 0x24]
0x488D7F: add      edx, dword ptr [ebp - 0x34]
0x488D82: mov      eax, dword ptr [ebp - 0x64]
0x488D85: call     0x456fdc
0x488D8A: push     eax
0x488D8B: mov      edx, dword ptr [ebp - 0x20]
0x488D8E: add      edx, dword ptr [ebp - 0x30]
0x488D91: mov      eax, dword ptr [ebp - 0x64]
0x488D94: call     0x456f94
0x488D99: mov      edx, eax
0x488D9B: mov      eax, dword ptr [ebp - 8]
0x488D9E: pop      ecx
0x488D9F: call     0x44d6cc
0x488DA4: mov      eax, dword ptr [0x4a3db0] ; *0x4A3DB0->0x4F7EFC
0x488DA9: cmp      byte ptr [eax + 0x34], 0
0x488DAD: je       0x488e29
0x488DAF: mov      eax, dword ptr [0x4a3db0] ; *0x4A3DB0->0x4F7EFC
0x488DB4: movzx    eax, word ptr [eax + 0x4a]
0x488DB8: mov      dword ptr [ebp - 0x2c], eax
0x488DBB: cmp      dword ptr [ebp - 0x2c], 0
0x488DBF: jl       0x488e29
0x488DC1: lea      eax, [ebp - 0x34]
0x488DC4: push     eax
0x488DC5: lea      ecx, [ebp - 0x30]
0x488DC8: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x488DCD: mov      eax, dword ptr [eax]
0x488DCF: mov      eax, dword ptr [eax + 0x32c]
0x488DD5: mov      edx, dword ptr [ebp - 0x2c]
0x488DD8: call     0x453a48
0x488DDD: mov      dword ptr [ebp - 0x40], eax
0x488DE0: cmp      dword ptr [ebp - 0x40], 0
0x488DE4: je       0x488e29
0x488DE6: lea      edx, [ebp - 0x90]
0x488DEC: mov      eax, dword ptr [ebp - 0x40]
0x488DEF: call     0x44d8ec
0x488DF4: lea      eax, [ebp - 0x90]
0x488DFA: push     eax
0x488DFB: mov      eax, dword ptr [ebp - 0x40]
0x488DFE: push     eax
0x488DFF: push     1
0x488E01: mov      edx, dword ptr [ebp - 0x24]
0x488E04: add      edx, dword ptr [ebp - 0x34]
0x488E07: mov      eax, dword ptr [ebp - 0x64]
0x488E0A: call     0x456fdc
0x488E0F: push     eax
0x488E10: mov      edx, dword ptr [ebp - 0x20]
0x488E13: add      edx, dword ptr [ebp - 0x30]
0x488E16: mov      eax, dword ptr [ebp - 0x64]
0x488E19: call     0x456f94
0x488E1E: mov      edx, eax
0x488E20: mov      eax, dword ptr [ebp - 8]
0x488E23: pop      ecx
0x488E24: call     0x44d6cc
0x488E29: mov      eax, dword ptr [0x4a3db0] ; *0x4A3DB0->0x4F7EFC
0x488E2E: cmp      byte ptr [eax + 0xd0], 0
0x488E35: je       0x489cbd
0x488E3B: mov      eax, dword ptr [0x4a3db0] ; *0x4A3DB0->0x4F7EFC
0x488E40: movzx    eax, word ptr [eax + 0xe6]
0x488E47: mov      dword ptr [ebp - 0x2c], eax
0x488E4A: cmp      dword ptr [ebp - 0x2c], 0
0x488E4E: jl       0x489cbd
0x488E54: lea      eax, [ebp - 0x34]
0x488E57: push     eax
0x488E58: lea      ecx, [ebp - 0x30]
0x488E5B: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x488E60: mov      eax, dword ptr [eax]
0x488E62: mov      eax, dword ptr [eax + 0x32c]
0x488E68: mov      edx, dword ptr [ebp - 0x2c]
0x488E6B: call     0x453a48
0x488E70: mov      dword ptr [ebp - 0x40], eax
0x488E73: cmp      dword ptr [ebp - 0x40], 0
0x488E77: je       0x489cbd
0x488E7D: lea      edx, [ebp - 0x90]
0x488E83: mov      eax, dword ptr [ebp - 0x40]
0x488E86: call     0x44d8ec
0x488E8B: lea      eax, [ebp - 0x90]
0x488E91: push     eax
0x488E92: mov      eax, dword ptr [ebp - 0x40]
0x488E95: push     eax
0x488E96: push     1
0x488E98: mov      edx, dword ptr [ebp - 0x24]
0x488E9B: add      edx, dword ptr [ebp - 0x34]
0x488E9E: mov      eax, dword ptr [ebp - 0x64]
0x488EA1: call     0x456fdc
0x488EA6: push     eax
0x488EA7: mov      edx, dword ptr [ebp - 0x20]
0x488EAA: add      edx, dword ptr [ebp - 0x30]
0x488EAD: mov      eax, dword ptr [ebp - 0x64]
0x488EB0: call     0x456f94
0x488EB5: mov      edx, eax
0x488EB7: mov      eax, dword ptr [ebp - 8]
0x488EBA: pop      ecx
0x488EBB: call     0x44d6cc
0x488EC0: jmp      0x489cbd
0x488EC5: mov      eax, dword ptr [ebp - 0x64]
0x488EC8: mov      eax, dword ptr [eax + 0x30]
0x488ECB: add      eax, 0x70
0x488ECE: mov      dword ptr [ebp - 0x10], eax
0x488ED1: mov      eax, dword ptr [ebp - 0x64]
0x488ED4: mov      eax, dword ptr [eax + 0x34]
0x488ED7: add      eax, 0x63
0x488EDA: mov      dword ptr [ebp - 0x14], eax
0x488EDD: mov      eax, dword ptr [ebp - 8]
0x488EE0: call     0x44d8b4
0x488EE5: mov      dword ptr [ebp - 0x68], eax
0x488EE8: push     1
0x488EEA: mov      eax, dword ptr [ebp - 0x68]
0x488EED: call     0x41834c
0x488EF2: push     eax
0x488EF3: call     0x406434
0x488EF8: mov      eax, dword ptr [ebp - 0x68]
0x488EFB: mov      eax, dword ptr [eax + 0xc]
0x488EFE: mov      edx, 0xffffff
0x488F03: call     0x4173fc
0x488F08: lea      edx, [ebp - 0x98]
0x488F0E: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x488F13: mov      eax, dword ptr [eax]
0x488F15: movzx    eax, byte ptr [eax + 0x32]
0x488F19: call     0x407d64
0x488F1E: push     dword ptr [ebp - 0x98]
0x488F24: push     0x489fe8 ; "-"
0x488F29: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x488F2E: mov      eax, dword ptr [eax]
0x488F30: mov      ax, word ptr [eax + 0x32]
0x488F34: call     0x406974
0x488F39: and      eax, 0xff
0x488F3E: lea      edx, [ebp - 0x9c]
0x488F44: call     0x407d64
0x488F49: push     dword ptr [ebp - 0x9c]
0x488F4F: lea      eax, [ebp - 0x94]
0x488F55: mov      edx, 3
0x488F5A: call     0x403d94
0x488F5F: mov      eax, dword ptr [ebp - 0x94]
0x488F65: push     eax
0x488F66: mov      edx, dword ptr [ebp - 0x14]
0x488F69: mov      eax, dword ptr [ebp - 0x64]
0x488F6C: call     0x456fdc
0x488F71: push     eax
0x488F72: mov      edx, dword ptr [ebp - 0x10]
0x488F75: mov      eax, dword ptr [ebp - 0x64]
0x488F78: call     0x456f94
0x488F7D: mov      edx, eax
0x488F7F: mov      eax, dword ptr [ebp - 0x68]
0x488F82: pop      ecx
0x488F83: call     0x4180dc
0x488F88: lea      edx, [ebp - 0x98]
0x488F8E: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x488F93: mov      eax, dword ptr [eax]
0x488F95: movzx    eax, byte ptr [eax + 0x34]
0x488F99: call     0x407d64
0x488F9E: push     dword ptr [ebp - 0x98]
0x488FA4: push     0x489fe8 ; "-"
0x488FA9: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x488FAE: mov      eax, dword ptr [eax]
0x488FB0: mov      ax, word ptr [eax + 0x34]
0x488FB4: call     0x406974
0x488FB9: and      eax, 0xff
0x488FBE: lea      edx, [ebp - 0x9c]
0x488FC4: call     0x407d64
0x488FC9: push     dword ptr [ebp - 0x9c]
0x488FCF: lea      eax, [ebp - 0x94]
0x488FD5: mov      edx, 3
0x488FDA: call     0x403d94
0x488FDF: mov      eax, dword ptr [ebp - 0x94]
0x488FE5: push     eax
0x488FE6: mov      edx, dword ptr [ebp - 0x14]
0x488FE9: add      edx, 0x14
0x488FEC: mov      eax, dword ptr [ebp - 0x64]
0x488FEF: call     0x456fdc
0x488FF4: push     eax
0x488FF5: mov      edx, dword ptr [ebp - 0x10]
0x488FF8: mov      eax, dword ptr [ebp - 0x64]
0x488FFB: call     0x456f94
0x489000: mov      edx, eax
0x489002: mov      eax, dword ptr [ebp - 0x68]
0x489005: pop      ecx
0x489006: call     0x4180dc
0x48900B: lea      edx, [ebp - 0x98]
0x489011: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x489016: mov      eax, dword ptr [eax]
0x489018: movzx    eax, byte ptr [eax + 0x36]
0x48901C: call     0x407d64
0x489021: push     dword ptr [ebp - 0x98]
0x489027: push     0x489fe8 ; "-"
0x48902C: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x489031: mov      eax, dword ptr [eax]
0x489033: mov      ax, word ptr [eax + 0x36]
0x489037: call     0x406974
0x48903C: and      eax, 0xff
0x489041: lea      edx, [ebp - 0x9c]
0x489047: call     0x407d64
0x48904C: push     dword ptr [ebp - 0x9c]
0x489052: lea      eax, [ebp - 0x94]
0x489058: mov      edx, 3
0x48905D: call     0x403d94
0x489062: mov      eax, dword ptr [ebp - 0x94]
0x489068: push     eax
0x489069: mov      edx, dword ptr [ebp - 0x14]
0x48906C: add      edx, 0x28
0x48906F: mov      eax, dword ptr [ebp - 0x64]
0x489072: call     0x456fdc
0x489077: push     eax
0x489078: mov      edx, dword ptr [ebp - 0x10]
0x48907B: mov      eax, dword ptr [ebp - 0x64]
0x48907E: call     0x456f94
0x489083: mov      edx, eax
0x489085: mov      eax, dword ptr [ebp - 0x68]
0x489088: pop      ecx
0x489089: call     0x4180dc
0x48908E: lea      edx, [ebp - 0x98]
0x489094: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x489099: mov      eax, dword ptr [eax]
0x48909B: movzx    eax, byte ptr [eax + 0x38]
0x48909F: call     0x407d64
0x4890A4: push     dword ptr [ebp - 0x98]
0x4890AA: push     0x489fe8 ; "-"
0x4890AF: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4890B4: mov      eax, dword ptr [eax]
0x4890B6: mov      ax, word ptr [eax + 0x38]
0x4890BA: call     0x406974
0x4890BF: and      eax, 0xff
0x4890C4: lea      edx, [ebp - 0x9c]
0x4890CA: call     0x407d64
0x4890CF: push     dword ptr [ebp - 0x9c]
0x4890D5: lea      eax, [ebp - 0x94]
0x4890DB: mov      edx, 3
0x4890E0: call     0x403d94
0x4890E5: mov      eax, dword ptr [ebp - 0x94]
0x4890EB: push     eax
0x4890EC: mov      edx, dword ptr [ebp - 0x14]
0x4890EF: add      edx, 0x3c
0x4890F2: mov      eax, dword ptr [ebp - 0x64]
0x4890F5: call     0x456fdc
0x4890FA: push     eax
0x4890FB: mov      edx, dword ptr [ebp - 0x10]
0x4890FE: mov      eax, dword ptr [ebp - 0x64]
0x489101: call     0x456f94
0x489106: mov      edx, eax
0x489108: mov      eax, dword ptr [ebp - 0x68]
0x48910B: pop      ecx
0x48910C: call     0x4180dc
0x489111: lea      edx, [ebp - 0x98]
0x489117: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48911C: mov      eax, dword ptr [eax]
0x48911E: movzx    eax, byte ptr [eax + 0x3a]
0x489122: call     0x407d64
0x489127: push     dword ptr [ebp - 0x98]
0x48912D: push     0x489fe8 ; "-"
0x489132: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x489137: mov      eax, dword ptr [eax]
0x489139: mov      ax, word ptr [eax + 0x3a]
0x48913D: call     0x406974
0x489142: and      eax, 0xff
0x489147: lea      edx, [ebp - 0x9c]
0x48914D: call     0x407d64
0x489152: push     dword ptr [ebp - 0x9c]
0x489158: lea      eax, [ebp - 0x94]
0x48915E: mov      edx, 3
0x489163: call     0x403d94
0x489168: mov      eax, dword ptr [ebp - 0x94]
0x48916E: push     eax
0x48916F: mov      edx, dword ptr [ebp - 0x14]
0x489172: add      edx, 0x50
0x489175: mov      eax, dword ptr [ebp - 0x64]
0x489178: call     0x456fdc
0x48917D: push     eax
0x48917E: mov      edx, dword ptr [ebp - 0x10]
0x489181: mov      eax, dword ptr [ebp - 0x64]
0x489184: call     0x456f94
0x489189: mov      edx, eax
0x48918B: mov      eax, dword ptr [ebp - 0x68]
0x48918E: pop      ecx
0x48918F: call     0x4180dc
0x489194: lea      edx, [ebp - 0x98]
0x48919A: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48919F: mov      eax, dword ptr [eax]
0x4891A1: movzx    eax, word ptr [eax + 0x3c]
0x4891A5: call     0x407d64
0x4891AA: push     dword ptr [ebp - 0x98]
0x4891B0: push     0x489ff4 ; "/"
0x4891B5: lea      edx, [ebp - 0x9c]
0x4891BB: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4891C0: mov      eax, dword ptr [eax]
0x4891C2: movzx    eax, word ptr [eax + 0x40]
0x4891C6: call     0x407d64
0x4891CB: push     dword ptr [ebp - 0x9c]
0x4891D1: lea      eax, [ebp - 0x94]
0x4891D7: mov      edx, 3
0x4891DC: call     0x403d94
0x4891E1: mov      eax, dword ptr [ebp - 0x94]
0x4891E7: push     eax
0x4891E8: mov      edx, dword ptr [ebp - 0x14]
0x4891EB: add      edx, 0x64
0x4891EE: mov      eax, dword ptr [ebp - 0x64]
0x4891F1: call     0x456fdc
0x4891F6: push     eax
0x4891F7: mov      edx, dword ptr [ebp - 0x10]
0x4891FA: mov      eax, dword ptr [ebp - 0x64]
0x4891FD: call     0x456f94
0x489202: mov      edx, eax
0x489204: mov      eax, dword ptr [ebp - 0x68]
0x489207: pop      ecx
0x489208: call     0x4180dc
0x48920D: lea      edx, [ebp - 0x98]
0x489213: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x489218: mov      eax, dword ptr [eax]
0x48921A: movzx    eax, word ptr [eax + 0x3e]
0x48921E: call     0x407d64
0x489223: push     dword ptr [ebp - 0x98]
0x489229: push     0x489ff4 ; "/"
0x48922E: lea      edx, [ebp - 0x9c]
0x489234: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x489239: mov      eax, dword ptr [eax]
0x48923B: movzx    eax, word ptr [eax + 0x42]
0x48923F: call     0x407d64
0x489244: push     dword ptr [ebp - 0x9c]
0x48924A: lea      eax, [ebp - 0x94]
0x489250: mov      edx, 3
0x489255: call     0x403d94
0x48925A: mov      eax, dword ptr [ebp - 0x94]
0x489260: push     eax
0x489261: mov      edx, dword ptr [ebp - 0x14]
0x489264: add      edx, 0x78
0x489267: mov      eax, dword ptr [ebp - 0x64]
0x48926A: call     0x456fdc
0x48926F: push     eax
0x489270: mov      edx, dword ptr [ebp - 0x10]
0x489273: mov      eax, dword ptr [ebp - 0x64]
0x489276: call     0x456f94
0x48927B: mov      edx, eax
0x48927D: mov      eax, dword ptr [ebp - 0x68]
0x489280: pop      ecx
0x489281: call     0x4180dc
0x489286: mov      eax, dword ptr [ebp - 0x68]
0x489289: call     0x44d104
0x48928E: jmp      0x489cbd
0x489293: mov      eax, dword ptr [ebp - 0x64]
0x489296: mov      eax, dword ptr [eax + 0x30]
0x489299: add      eax, 0x26
0x48929C: mov      dword ptr [ebp - 0x20], eax
0x48929F: mov      eax, dword ptr [ebp - 0x64]
0x4892A2: mov      eax, dword ptr [eax + 0x34]
0x4892A5: add      eax, 0x34
0x4892A8: mov      dword ptr [ebp - 0x24], eax
0x4892AB: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4892B0: mov      eax, dword ptr [eax]
0x4892B2: mov      eax, dword ptr [eax + 0x308]
0x4892B8: mov      edx, 0x17e
0x4892BD: call     0x45351c
0x4892C2: mov      dword ptr [ebp - 0x40], eax
0x4892C5: cmp      dword ptr [ebp - 0x40], 0
0x4892C9: je       0x489308
0x4892CB: lea      edx, [ebp - 0x90]
0x4892D1: mov      eax, dword ptr [ebp - 0x40]
0x4892D4: call     0x44d8ec
0x4892D9: lea      eax, [ebp - 0x90]
0x4892DF: push     eax
0x4892E0: mov      eax, dword ptr [ebp - 0x40]
0x4892E3: push     eax
0x4892E4: push     0
0x4892E6: mov      edx, dword ptr [ebp - 0x24]
0x4892E9: mov      eax, dword ptr [ebp - 0x64]
0x4892EC: call     0x456fdc
0x4892F1: push     eax
0x4892F2: mov      edx, dword ptr [ebp - 0x20]
0x4892F5: mov      eax, dword ptr [ebp - 0x64]
0x4892F8: call     0x456f94
0x4892FD: mov      edx, eax
0x4892FF: mov      eax, dword ptr [ebp - 8]
0x489302: pop      ecx
0x489303: call     0x44d6cc
0x489308: add      dword ptr [ebp - 0x20], 0x14
0x48930C: add      dword ptr [ebp - 0x24], 0xa
0x489310: mov      eax, dword ptr [ebp - 8]
0x489313: call     0x44d8b4
0x489318: mov      dword ptr [ebp - 0x6c], eax
0x48931B: push     1
0x48931D: mov      eax, dword ptr [ebp - 0x6c]
0x489320: call     0x41834c
0x489325: push     eax
0x489326: call     0x406434
0x48932B: mov      eax, dword ptr [ebp - 0x20]
0x48932E: add      eax, 0x55
0x489331: mov      dword ptr [ebp - 0x28], eax
0x489334: mov      eax, dword ptr [ebp - 0x6c]
0x489337: mov      eax, dword ptr [eax + 0xc]
0x48933A: mov      edx, 0xc0c0c0
0x48933F: call     0x4173fc
0x489344: push     0x48a000 ; "经验值"
0x489349: mov      ecx, dword ptr [ebp - 0x24]
0x48934C: mov      edx, dword ptr [ebp - 0x20]
0x48934F: mov      eax, dword ptr [ebp - 0x6c]
0x489352: call     0x4180dc
0x489357: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48935C: mov      eax, dword ptr [eax]
0x48935E: imul     eax, dword ptr [eax + 0x48], 0x64
0x489362: mov      dword ptr [ebp - 0xa4], eax
0x489368: xor      eax, eax
0x48936A: mov      dword ptr [ebp - 0xa0], eax
0x489370: fild     qword ptr [ebp - 0xa4]
0x489376: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48937B: mov      eax, dword ptr [eax]
0x48937D: mov      eax, dword ptr [eax + 0x4c]
0x489380: mov      dword ptr [ebp - 0xac], eax
0x489386: xor      eax, eax
0x489388: mov      dword ptr [ebp - 0xa8], eax
0x48938E: fild     qword ptr [ebp - 0xac]
0x489394: fdivp    st(1)
0x489396: add      esp, -8
0x489399: fstp     qword ptr [esp]
0x48939C: wait     
0x48939D: lea      ecx, [ebp - 0x94]
0x4893A3: mov      edx, 2
0x4893A8: mov      eax, 3
0x4893AD: call     0x45222c
0x4893B2: lea      eax, [ebp - 0x94]
0x4893B8: mov      edx, 0x48a010 ; "%"
0x4893BD: call     0x403cdc
0x4893C2: mov      eax, dword ptr [ebp - 0x94]
0x4893C8: push     eax
0x4893C9: mov      ecx, dword ptr [ebp - 0x24]
0x4893CC: mov      edx, dword ptr [ebp - 0x28]
0x4893CF: mov      eax, dword ptr [ebp - 0x6c]
0x4893D2: call     0x4180dc
0x4893D7: push     0x48a01c ; "背包重量"
0x4893DC: mov      ecx, dword ptr [ebp - 0x24]
0x4893DF: add      ecx, 0xe
0x4893E2: mov      edx, dword ptr [ebp - 0x20]
0x4893E5: mov      eax, dword ptr [ebp - 0x6c]
0x4893E8: call     0x4180dc
0x4893ED: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4893F2: mov      eax, dword ptr [eax]
0x4893F4: mov      ax, word ptr [eax + 0x50]
0x4893F8: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4893FE: mov      edx, dword ptr [edx]
0x489400: cmp      ax, word ptr [edx + 0x52]
0x489404: jbe      0x489416
0x489406: mov      eax, dword ptr [ebp - 0x6c]
0x489409: mov      eax, dword ptr [eax + 0xc]
0x48940C: mov      edx, 0xff
0x489411: call     0x4173fc
0x489416: lea      edx, [ebp - 0x98]
0x48941C: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x489421: mov      eax, dword ptr [eax]
0x489423: movzx    eax, word ptr [eax + 0x50]
0x489427: call     0x407d64
0x48942C: push     dword ptr [ebp - 0x98]
0x489432: push     0x489ff4 ; "/"
0x489437: lea      edx, [ebp - 0x9c]
0x48943D: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x489442: mov      eax, dword ptr [eax]
0x489444: movzx    eax, word ptr [eax + 0x52]
0x489448: call     0x407d64
0x48944D: push     dword ptr [ebp - 0x9c]
0x489453: lea      eax, [ebp - 0x94]
0x489459: mov      edx, 3
0x48945E: call     0x403d94
0x489463: mov      eax, dword ptr [ebp - 0x94]
0x489469: push     eax
0x48946A: mov      ecx, dword ptr [ebp - 0x24]
0x48946D: add      ecx, 0xe
0x489470: mov      edx, dword ptr [ebp - 0x28]
0x489473: mov      eax, dword ptr [ebp - 0x6c]
0x489476: call     0x4180dc
0x48947B: mov      eax, dword ptr [ebp - 0x6c]
0x48947E: mov      eax, dword ptr [eax + 0xc]
0x489481: mov      edx, 0xc0c0c0
0x489486: call     0x4173fc
0x48948B: push     0x48a030 ; "负重量"
0x489490: mov      ecx, dword ptr [ebp - 0x24]
0x489493: add      ecx, 0x1c
0x489496: mov      edx, dword ptr [ebp - 0x20]
0x489499: mov      eax, dword ptr [ebp - 0x6c]
0x48949C: call     0x4180dc
0x4894A1: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4894A6: mov      eax, dword ptr [eax]
0x4894A8: mov      al, byte ptr [eax + 0x54]
0x4894AB: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4894B1: mov      edx, dword ptr [edx]
0x4894B3: cmp      al, byte ptr [edx + 0x55]
0x4894B6: jbe      0x4894c8
0x4894B8: mov      eax, dword ptr [ebp - 0x6c]
0x4894BB: mov      eax, dword ptr [eax + 0xc]
0x4894BE: mov      edx, 0xff
0x4894C3: call     0x4173fc
0x4894C8: lea      edx, [ebp - 0x98]
0x4894CE: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4894D3: mov      eax, dword ptr [eax]
0x4894D5: movzx    eax, byte ptr [eax + 0x54]
0x4894D9: call     0x407d64
0x4894DE: push     dword ptr [ebp - 0x98]
0x4894E4: push     0x489ff4 ; "/"
0x4894E9: lea      edx, [ebp - 0x9c]
0x4894EF: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4894F4: mov      eax, dword ptr [eax]
0x4894F6: movzx    eax, byte ptr [eax + 0x55]
0x4894FA: call     0x407d64
0x4894FF: push     dword ptr [ebp - 0x9c]
0x489505: lea      eax, [ebp - 0x94]
0x48950B: mov      edx, 3
0x489510: call     0x403d94
0x489515: mov      eax, dword ptr [ebp - 0x94]
0x48951B: push     eax
0x48951C: mov      ecx, dword ptr [ebp - 0x24]
0x48951F: add      ecx, 0x1c
0x489522: mov      edx, dword ptr [ebp - 0x28]
0x489525: mov      eax, dword ptr [ebp - 0x6c]
0x489528: call     0x4180dc
0x48952D: mov      eax, dword ptr [ebp - 0x6c]
0x489530: mov      eax, dword ptr [eax + 0xc]
0x489533: mov      edx, 0xc0c0c0
0x489538: call     0x4173fc
0x48953D: push     0x48a040 ; "腕力"
0x489542: mov      ecx, dword ptr [ebp - 0x24]
0x489545: add      ecx, 0x2a
0x489548: mov      edx, dword ptr [ebp - 0x20]
0x48954B: mov      eax, dword ptr [ebp - 0x6c]
0x48954E: call     0x4180dc
0x489553: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x489558: mov      eax, dword ptr [eax]
0x48955A: mov      al, byte ptr [eax + 0x56]
0x48955D: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x489563: mov      edx, dword ptr [edx]
0x489565: cmp      al, byte ptr [edx + 0x57]
0x489568: jbe      0x48957a
0x48956A: mov      eax, dword ptr [ebp - 0x6c]
0x48956D: mov      eax, dword ptr [eax + 0xc]
0x489570: mov      edx, 0xff
0x489575: call     0x4173fc
0x48957A: lea      edx, [ebp - 0x98]
0x489580: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x489585: mov      eax, dword ptr [eax]
0x489587: movzx    eax, byte ptr [eax + 0x56]
0x48958B: call     0x407d64
0x489590: push     dword ptr [ebp - 0x98]
0x489596: push     0x489ff4 ; "/"
0x48959B: lea      edx, [ebp - 0x9c]
0x4895A1: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4895A6: mov      eax, dword ptr [eax]
0x4895A8: movzx    eax, byte ptr [eax + 0x57]
0x4895AC: call     0x407d64
0x4895B1: push     dword ptr [ebp - 0x9c]
0x4895B7: lea      eax, [ebp - 0x94]
0x4895BD: mov      edx, 3
0x4895C2: call     0x403d94
0x4895C7: mov      eax, dword ptr [ebp - 0x94]
0x4895CD: push     eax
0x4895CE: mov      ecx, dword ptr [ebp - 0x24]
0x4895D1: add      ecx, 0x2a
0x4895D4: mov      edx, dword ptr [ebp - 0x28]
0x4895D7: mov      eax, dword ptr [ebp - 0x6c]
0x4895DA: call     0x4180dc
0x4895DF: mov      eax, dword ptr [ebp - 0x6c]
0x4895E2: mov      eax, dword ptr [eax + 0xc]
0x4895E5: mov      edx, 0xc0c0c0
0x4895EA: call     0x4173fc
0x4895EF: push     0x48a050 ; "精确度 "
0x4895F4: mov      ecx, dword ptr [ebp - 0x24]
0x4895F7: add      ecx, 0x38
0x4895FA: mov      edx, dword ptr [ebp - 0x20]
0x4895FD: mov      eax, dword ptr [ebp - 0x6c]
0x489600: call     0x4180dc
0x489605: lea      edx, [ebp - 0x94]
0x48960B: mov      eax, dword ptr [0x4a3f50] ; *0x4A3F50->0x4F9504
0x489610: mov      eax, dword ptr [eax]
0x489612: call     0x407d64
0x489617: mov      eax, dword ptr [ebp - 0x94]
0x48961D: push     eax
0x48961E: mov      ecx, dword ptr [ebp - 0x24]
0x489621: add      ecx, 0x38
0x489624: mov      edx, dword ptr [ebp - 0x28]
0x489627: mov      eax, dword ptr [ebp - 0x6c]
0x48962A: call     0x4180dc
0x48962F: push     0x48a060 ; "敏捷度 "
0x489634: mov      ecx, dword ptr [ebp - 0x24]
0x489637: add      ecx, 0x46
0x48963A: mov      edx, dword ptr [ebp - 0x20]
0x48963D: mov      eax, dword ptr [ebp - 0x6c]
0x489640: call     0x4180dc
0x489645: lea      edx, [ebp - 0x94]
0x48964B: mov      eax, dword ptr [0x4a41d0] ; *0x4A41D0->0x4F9500
0x489650: mov      eax, dword ptr [eax]
0x489652: call     0x407d64
0x489657: mov      eax, dword ptr [ebp - 0x94]
0x48965D: push     eax
0x48965E: mov      ecx, dword ptr [ebp - 0x24]
0x489661: add      ecx, 0x46
0x489664: mov      edx, dword ptr [ebp - 0x28]
0x489667: mov      eax, dword ptr [ebp - 0x6c]
0x48966A: call     0x4180dc
0x48966F: push     0x48a070 ; "魔法防御"
0x489674: mov      ecx, dword ptr [ebp - 0x24]
0x489677: add      ecx, 0x54
0x48967A: mov      edx, dword ptr [ebp - 0x20]
0x48967D: mov      eax, dword ptr [ebp - 0x6c]
0x489680: call     0x4180dc
0x489685: push     0x48a084 ; "+"
0x48968A: lea      edx, [ebp - 0x98]
0x489690: mov      eax, dword ptr [0x4a4118] ; *0x4A4118->0x4F9518
0x489695: mov      eax, dword ptr [eax]
0x489697: add      eax, eax
0x489699: lea      eax, [eax + eax*4]
0x48969C: call     0x407d64
0x4896A1: push     dword ptr [ebp - 0x98]
0x4896A7: push     0x48a010 ; "%"
0x4896AC: lea      eax, [ebp - 0x94]
0x4896B2: mov      edx, 3
0x4896B7: call     0x403d94
0x4896BC: mov      eax, dword ptr [ebp - 0x94]
0x4896C2: push     eax
0x4896C3: mov      ecx, dword ptr [ebp - 0x24]
0x4896C6: add      ecx, 0x54
0x4896C9: mov      edx, dword ptr [ebp - 0x28]
0x4896CC: mov      eax, dword ptr [ebp - 0x6c]
0x4896CF: call     0x4180dc
0x4896D4: push     0x48a090 ; "中毒防御"
0x4896D9: mov      ecx, dword ptr [ebp - 0x24]
0x4896DC: add      ecx, 0x62
0x4896DF: mov      edx, dword ptr [ebp - 0x20]
0x4896E2: mov      eax, dword ptr [ebp - 0x6c]
0x4896E5: call     0x4180dc
0x4896EA: push     0x48a084 ; "+"
0x4896EF: lea      edx, [ebp - 0x98]
0x4896F5: mov      eax, dword ptr [0x4a4234] ; *0x4A4234->0x4F9508
0x4896FA: mov      eax, dword ptr [eax]
0x4896FC: add      eax, eax
0x4896FE: lea      eax, [eax + eax*4]
0x489701: call     0x407d64
0x489706: push     dword ptr [ebp - 0x98]
0x48970C: push     0x48a010 ; "%"
0x489711: lea      eax, [ebp - 0x94]
0x489717: mov      edx, 3
0x48971C: call     0x403d94
0x489721: mov      eax, dword ptr [ebp - 0x94]
0x489727: push     eax
0x489728: mov      ecx, dword ptr [ebp - 0x24]
0x48972B: add      ecx, 0x62
0x48972E: mov      edx, dword ptr [ebp - 0x28]
0x489731: mov      eax, dword ptr [ebp - 0x6c]
0x489734: call     0x4180dc
0x489739: push     0x48a0a4 ; "中毒恢复"
0x48973E: mov      ecx, dword ptr [ebp - 0x24]
0x489741: add      ecx, 0x70
0x489744: mov      edx, dword ptr [ebp - 0x20]
0x489747: mov      eax, dword ptr [ebp - 0x6c]
0x48974A: call     0x4180dc
0x48974F: push     0x48a084 ; "+"
0x489754: lea      edx, [ebp - 0x98]
0x48975A: mov      eax, dword ptr [0x4a4230] ; *0x4A4230->0x4F950C
0x48975F: mov      eax, dword ptr [eax]
0x489761: add      eax, eax
0x489763: lea      eax, [eax + eax*4]
0x489766: call     0x407d64
0x48976B: push     dword ptr [ebp - 0x98]
0x489771: push     0x48a010 ; "%"
0x489776: lea      eax, [ebp - 0x94]
0x48977C: mov      edx, 3
0x489781: call     0x403d94
0x489786: mov      eax, dword ptr [ebp - 0x94]
0x48978C: push     eax
0x48978D: mov      ecx, dword ptr [ebp - 0x24]
0x489790: add      ecx, 0x70
0x489793: mov      edx, dword ptr [ebp - 0x28]
0x489796: mov      eax, dword ptr [ebp - 0x6c]
0x489799: call     0x4180dc
0x48979E: push     0x48a0b8 ; "体力恢复"
0x4897A3: mov      ecx, dword ptr [ebp - 0x24]
0x4897A6: add      ecx, 0x7e
0x4897A9: mov      edx, dword ptr [ebp - 0x20]
0x4897AC: mov      eax, dword ptr [ebp - 0x6c]
0x4897AF: call     0x4180dc
0x4897B4: push     0x48a084 ; "+"
0x4897B9: lea      edx, [ebp - 0x98]
0x4897BF: mov      eax, dword ptr [0x4a4280] ; *0x4A4280->0x4F9510
0x4897C4: mov      eax, dword ptr [eax]
0x4897C6: add      eax, eax
0x4897C8: lea      eax, [eax + eax*4]
0x4897CB: call     0x407d64
0x4897D0: push     dword ptr [ebp - 0x98]
0x4897D6: push     0x48a010 ; "%"
0x4897DB: lea      eax, [ebp - 0x94]
0x4897E1: mov      edx, 3
0x4897E6: call     0x403d94
0x4897EB: mov      eax, dword ptr [ebp - 0x94]
0x4897F1: push     eax
0x4897F2: mov      ecx, dword ptr [ebp - 0x24]
0x4897F5: add      ecx, 0x7e
0x4897F8: mov      edx, dword ptr [ebp - 0x28]
0x4897FB: mov      eax, dword ptr [ebp - 0x6c]
0x4897FE: call     0x4180dc
0x489803: push     0x48a0cc ; "魔法恢复"
0x489808: mov      ecx, dword ptr [ebp - 0x24]
0x48980B: add      ecx, 0x8c
0x489811: mov      edx, dword ptr [ebp - 0x20]
0x489814: mov      eax, dword ptr [ebp - 0x6c]
0x489817: call     0x4180dc
0x48981C: push     0x48a084 ; "+"
0x489821: lea      edx, [ebp - 0x98]
0x489827: mov      eax, dword ptr [0x4a4260] ; *0x4A4260->0x4F9514
0x48982C: mov      eax, dword ptr [eax]
0x48982E: add      eax, eax
0x489830: lea      eax, [eax + eax*4]
0x489833: call     0x407d64
0x489838: push     dword ptr [ebp - 0x98]
0x48983E: push     0x48a010 ; "%"
0x489843: lea      eax, [ebp - 0x94]
0x489849: mov      edx, 3
0x48984E: call     0x403d94
0x489853: mov      eax, dword ptr [ebp - 0x94]
0x489859: push     eax
0x48985A: mov      ecx, dword ptr [ebp - 0x24]
0x48985D: add      ecx, 0x8c
0x489863: mov      edx, dword ptr [ebp - 0x28]
0x489866: mov      eax, dword ptr [ebp - 0x6c]
0x489869: call     0x4180dc
0x48986E: mov      eax, dword ptr [ebp - 0x6c]
0x489871: call     0x44d104
0x489876: jmp      0x489cbd
0x48987B: mov      eax, dword ptr [ebp - 0x64]
0x48987E: mov      eax, dword ptr [eax + 0x30]
0x489881: add      eax, 0x26
0x489884: mov      dword ptr [ebp - 0x20], eax
0x489887: mov      eax, dword ptr [ebp - 0x64]
0x48988A: mov      eax, dword ptr [eax + 0x34]
0x48988D: add      eax, 0x34
0x489890: mov      dword ptr [ebp - 0x24], eax
0x489893: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x489898: mov      eax, dword ptr [eax]
0x48989A: mov      eax, dword ptr [eax + 0x308]
0x4898A0: mov      edx, 0x17f
0x4898A5: call     0x45351c
0x4898AA: mov      dword ptr [ebp - 0x40], eax
0x4898AD: cmp      dword ptr [ebp - 0x40], 0
0x4898B1: je       0x4898f0
0x4898B3: lea      edx, [ebp - 0x90]
0x4898B9: mov      eax, dword ptr [ebp - 0x40]
0x4898BC: call     0x44d8ec
0x4898C1: lea      eax, [ebp - 0x90]
0x4898C7: push     eax
0x4898C8: mov      eax, dword ptr [ebp - 0x40]
0x4898CB: push     eax
0x4898CC: push     0
0x4898CE: mov      edx, dword ptr [ebp - 0x24]
0x4898D1: mov      eax, dword ptr [ebp - 0x64]
0x4898D4: call     0x456fdc
0x4898D9: push     eax
0x4898DA: mov      edx, dword ptr [ebp - 0x20]
0x4898DD: mov      eax, dword ptr [ebp - 0x64]
0x4898E0: call     0x456f94
0x4898E5: mov      edx, eax
0x4898E7: mov      eax, dword ptr [ebp - 8]
0x4898EA: pop      ecx
0x4898EB: call     0x44d6cc
0x4898F0: mov      eax, dword ptr [ebp - 4]
0x4898F3: mov      eax, dword ptr [eax + 0x60c]
0x4898F9: lea      eax, [eax + eax*4]
0x4898FC: mov      edx, dword ptr [ebp - 4]
0x4898FF: mov      dword ptr [edx + 0x5e4], eax
0x489905: mov      eax, dword ptr [0x4a3f00] ; *0x4A3F00->0x4F9190
0x48990A: mov      eax, dword ptr [eax]
0x48990C: mov      edx, dword ptr [eax + 8]
0x48990F: mov      eax, dword ptr [ebp - 4]
0x489912: mov      eax, dword ptr [eax + 0x60c]
0x489918: lea      eax, [eax + eax*4]
0x48991B: add      eax, 5
0x48991E: call     0x452b90
0x489923: mov      dword ptr [ebp - 0x1c], eax
0x489926: mov      eax, dword ptr [ebp - 4]
0x489929: mov      eax, dword ptr [eax + 0x5e4]
0x48992F: mov      edx, dword ptr [ebp - 0x1c]
0x489932: dec      edx
0x489933: sub      edx, eax
0x489935: jl       0x489afe
0x48993B: inc      edx
0x48993C: mov      dword ptr [ebp - 0x70], edx
0x48993F: mov      dword ptr [ebp - 0xc], eax
0x489942: mov      eax, dword ptr [0x4a3f00] ; *0x4A3F00->0x4F9190
0x489947: mov      eax, dword ptr [eax]
0x489949: mov      edx, dword ptr [ebp - 0xc]
0x48994C: call     0x40f6a0
0x489951: mov      dword ptr [ebp - 0x3c], eax
0x489954: mov      eax, dword ptr [ebp - 4]
0x489957: mov      edx, dword ptr [ebp - 0xc]
0x48995A: sub      edx, dword ptr [eax + 0x5e4]
0x489960: mov      dword ptr [ebp - 0x14], edx
0x489963: xor      eax, eax
0x489965: mov      dword ptr [ebp - 0x4c], eax
0x489968: mov      eax, dword ptr [ebp - 0x3c]
0x48996B: movzx    eax, byte ptr [eax]
0x48996E: add      eax, -0x31
0x489971: cmp      eax, 7
0x489974: ja       0x4899e3
0x489976: jmp      dword ptr [eax*4 + 0x48997d]
0x48997D: popfd    
0x48997E: cdq      
0x48997F: dec      eax
0x489980: add      byte ptr [esi - 0x50ffb767], ah
0x489986: cdq      
0x489987: dec      eax
0x489988: add      byte ptr [eax - 0x3effb767], bh
0x48998E: cdq      
0x48998F: dec      eax
0x489990: add      dl, cl
0x489992: cdq      
0x489993: dec      eax
0x489994: add      bl, dl
0x489996: cdq      
0x489997: dec      eax
0x489998: add      ah, bl
0x48999A: cdq      
0x48999B: dec      eax
0x48999C: add      bh, al
0x48999E: inc      ebp
0x48999F: mov      ah, 0xf8
0x4899A1: add      byte ptr [eax], al
0x4899A3: add      bl, ch
0x4899A5: cmp      eax, 0xf9b445c7
0x4899AA: add      byte ptr [eax], al
0x4899AC: add      bl, ch
0x4899AE: xor      al, 0xc7
0x4899B0: inc      ebp
0x4899B1: mov      ah, 0xfa
0x4899B3: add      byte ptr [eax], al
0x4899B5: add      bl, ch
0x4899B7: sub      eax, edi
0x4899B9: inc      ebp
0x4899BA: mov      ah, 0xfb
0x4899BC: add      byte ptr [eax], al
0x4899BE: add      bl, ch
0x4899C0: and      al, bh
0x4899C2: inc      ebp
0x4899C3: mov      ah, 0xfc
0x4899C5: add      byte ptr [eax], al
0x4899C7: add      bl, ch
0x4899C9: sbb      edi, eax
0x4899CB: inc      ebp
0x4899CC: mov      ah, 0xfd
0x4899CE: add      byte ptr [eax], al
0x4899D0: add      bl, ch
0x4899D2: adc      bh, al
0x4899D4: inc      ebp
0x4899D5: mov      ah, 0xfe
0x4899D7: add      byte ptr [eax], al
0x4899D9: add      bl, ch
0x4899DB: pop      es
0x4899DC: mov      dword ptr [ebp - 0x4c], 0xff
0x4899E3: cmp      dword ptr [ebp - 0x4c], 0
0x4899E7: jle      0x489a3f
0x4899E9: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4899EE: mov      eax, dword ptr [eax]
0x4899F0: mov      eax, dword ptr [eax + 0x308]
0x4899F6: mov      edx, dword ptr [ebp - 0x4c]
0x4899F9: call     0x45351c
0x4899FE: mov      dword ptr [ebp - 0x40], eax
0x489A01: cmp      dword ptr [ebp - 0x40], 0
0x489A05: je       0x489a3f
0x489A07: lea      edx, [ebp - 0x90]
0x489A0D: mov      eax, dword ptr [ebp - 0x40]
0x489A10: call     0x44d8ec
0x489A15: lea      eax, [ebp - 0x90]
0x489A1B: push     eax
0x489A1C: mov      eax, dword ptr [ebp - 0x40]
0x489A1F: push     eax
0x489A20: push     1
0x489A22: mov      ecx, dword ptr [ebp - 0x24]
0x489A25: add      ecx, 8
0x489A28: imul     eax, dword ptr [ebp - 0x14], 0x25
0x489A2C: add      ecx, eax
0x489A2E: mov      edx, dword ptr [ebp - 0x20]
0x489A31: add      edx, 0x91
0x489A37: mov      eax, dword ptr [ebp - 8]
0x489A3A: call     0x44d6cc
0x489A3F: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x489A44: mov      eax, dword ptr [eax]
0x489A46: mov      eax, dword ptr [eax + 0x308]
0x489A4C: mov      edx, 0x70
0x489A51: call     0x45351c
0x489A56: mov      dword ptr [ebp - 0x40], eax
0x489A59: cmp      dword ptr [ebp - 0x40], 0
0x489A5D: je       0x489a97
0x489A5F: lea      edx, [ebp - 0x90]
0x489A65: mov      eax, dword ptr [ebp - 0x40]
0x489A68: call     0x44d8ec
0x489A6D: lea      eax, [ebp - 0x90]
0x489A73: push     eax
0x489A74: mov      eax, dword ptr [ebp - 0x40]
0x489A77: push     eax
0x489A78: push     1
0x489A7A: mov      ecx, dword ptr [ebp - 0x24]
0x489A7D: add      ecx, 8
0x489A80: add      ecx, 0xf
0x489A83: imul     eax, dword ptr [ebp - 0x14], 0x25
0x489A87: add      ecx, eax
0x489A89: mov      edx, dword ptr [ebp - 0x20]
0x489A8C: add      edx, 0x30
0x489A8F: mov      eax, dword ptr [ebp - 8]
0x489A92: call     0x44d6cc
0x489A97: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x489A9C: mov      eax, dword ptr [eax]
0x489A9E: mov      eax, dword ptr [eax + 0x308]
0x489AA4: mov      edx, 0x6f
0x489AA9: call     0x45351c
0x489AAE: mov      dword ptr [ebp - 0x40], eax
0x489AB1: cmp      dword ptr [ebp - 0x40], 0
0x489AB5: je       0x489af2
0x489AB7: lea      edx, [ebp - 0x90]
0x489ABD: mov      eax, dword ptr [ebp - 0x40]
0x489AC0: call     0x44d8ec
0x489AC5: lea      eax, [ebp - 0x90]
0x489ACB: push     eax
0x489ACC: mov      eax, dword ptr [ebp - 0x40]
0x489ACF: push     eax
0x489AD0: push     1
0x489AD2: mov      ecx, dword ptr [ebp - 0x24]
0x489AD5: add      ecx, 8
0x489AD8: add      ecx, 0xf
0x489ADB: imul     eax, dword ptr [ebp - 0x14], 0x25
0x489ADF: add      ecx, eax
0x489AE1: mov      edx, dword ptr [ebp - 0x20]
0x489AE4: add      edx, 0x30
0x489AE7: add      edx, 0x1a
0x489AEA: mov      eax, dword ptr [ebp - 8]
0x489AED: call     0x44d6cc
0x489AF2: inc      dword ptr [ebp - 0xc]
0x489AF5: dec      dword ptr [ebp - 0x70]
0x489AF8: jne      0x489942
0x489AFE: mov      eax, dword ptr [ebp - 8]
0x489B01: call     0x44d8b4
0x489B06: mov      dword ptr [ebp - 0x74], eax
0x489B09: push     1
0x489B0B: mov      eax, dword ptr [ebp - 0x74]
0x489B0E: call     0x41834c
0x489B13: push     eax
0x489B14: call     0x406434
0x489B19: mov      eax, dword ptr [ebp - 0x74]
0x489B1C: mov      eax, dword ptr [eax + 0xc]
0x489B1F: mov      edx, 0xc0c0c0
0x489B24: call     0x4173fc
0x489B29: mov      eax, dword ptr [ebp - 4]
0x489B2C: mov      eax, dword ptr [eax + 0x5e4]
0x489B32: mov      edx, dword ptr [ebp - 0x1c]
0x489B35: dec      edx
0x489B36: sub      edx, eax
0x489B38: jl       0x489cb5
0x489B3E: inc      edx
0x489B3F: mov      dword ptr [ebp - 0x70], edx
0x489B42: mov      dword ptr [ebp - 0xc], eax
0x489B45: mov      eax, dword ptr [0x4a3f00] ; *0x4A3F00->0x4F9190
0x489B4A: mov      eax, dword ptr [eax]
0x489B4C: mov      edx, dword ptr [ebp - 0xc]
0x489B4F: call     0x40f6a0
0x489B54: mov      dword ptr [ebp - 0x3c], eax
0x489B57: mov      eax, dword ptr [ebp - 4]
0x489B5A: mov      edx, dword ptr [ebp - 0xc]
0x489B5D: sub      edx, dword ptr [eax + 0x5e4]
0x489B63: mov      dword ptr [ebp - 0x14], edx
0x489B66: mov      eax, dword ptr [ebp - 0x3c]
0x489B69: mov      al, byte ptr [eax + 1]
0x489B6C: sub      al, 4
0x489B6E: jb       0x489b77
0x489B70: mov      eax, dword ptr [ebp - 0x3c]
0x489B73: mov      byte ptr [eax + 1], 0
0x489B77: lea      eax, [ebp - 0x94]
0x489B7D: mov      edx, dword ptr [ebp - 0x3c]
0x489B80: add      edx, 0xa
0x489B83: call     0x403c78
0x489B88: mov      eax, dword ptr [ebp - 0x94]
0x489B8E: push     eax
0x489B8F: mov      ecx, dword ptr [ebp - 0x24]
0x489B92: add      ecx, 8
0x489B95: imul     eax, dword ptr [ebp - 0x14], 0x25
0x489B99: add      ecx, eax
0x489B9B: mov      edx, dword ptr [ebp - 0x20]
0x489B9E: add      edx, 0x30
0x489BA1: mov      eax, dword ptr [ebp - 0x74]
0x489BA4: call     0x4180dc
0x489BA9: mov      eax, dword ptr [ebp - 0x3c]
0x489BAC: mov      al, byte ptr [eax + 1]
0x489BAF: sub      al, 4
0x489BB1: jae      0x489bbf
0x489BB3: mov      eax, dword ptr [ebp - 0x3c]
0x489BB6: movzx    eax, byte ptr [eax + 1]
0x489BBA: mov      dword ptr [ebp - 0x38], eax
0x489BBD: jmp      0x489bc4
0x489BBF: xor      eax, eax
0x489BC1: mov      dword ptr [ebp - 0x38], eax
0x489BC4: lea      edx, [ebp - 0x94]
0x489BCA: mov      eax, dword ptr [ebp - 0x3c]
0x489BCD: movzx    eax, byte ptr [eax + 1]
0x489BD1: call     0x407d64
0x489BD6: mov      eax, dword ptr [ebp - 0x94]
0x489BDC: push     eax
0x489BDD: mov      ecx, dword ptr [ebp - 0x24]
0x489BE0: add      ecx, 8
0x489BE3: add      ecx, 0xf
0x489BE6: imul     eax, dword ptr [ebp - 0x14], 0x25
0x489BEA: add      ecx, eax
0x489BEC: mov      edx, dword ptr [ebp - 0x20]
0x489BEF: add      edx, 0x30
0x489BF2: add      edx, 0x10
0x489BF5: mov      eax, dword ptr [ebp - 0x74]
0x489BF8: call     0x4180dc
0x489BFD: mov      eax, dword ptr [ebp - 0x3c]
0x489C00: mov      edx, dword ptr [ebp - 0x38]
0x489C03: cmp      dword ptr [eax + edx*4 + 0x24], 0
0x489C08: jle      0x489ca9
0x489C0E: cmp      dword ptr [ebp - 0x38], 3
0x489C12: jge      0x489c84
0x489C14: lea      edx, [ebp - 0x98]
0x489C1A: mov      eax, dword ptr [ebp - 0x3c]
0x489C1D: mov      eax, dword ptr [eax + 4]
0x489C20: call     0x407d64
0x489C25: push     dword ptr [ebp - 0x98]
0x489C2B: push     0x489ff4 ; "/"
0x489C30: mov      eax, dword ptr [ebp - 0x3c]
0x489C33: mov      edx, dword ptr [ebp - 0x38]
0x489C36: mov      eax, dword ptr [eax + edx*4 + 0x24]
0x489C3A: lea      edx, [ebp - 0x9c]
0x489C40: call     0x407d64
0x489C45: push     dword ptr [ebp - 0x9c]
0x489C4B: lea      eax, [ebp - 0x94]
0x489C51: mov      edx, 3
0x489C56: call     0x403d94
0x489C5B: mov      eax, dword ptr [ebp - 0x94]
0x489C61: push     eax
0x489C62: mov      ecx, dword ptr [ebp - 0x24]
0x489C65: add      ecx, 8
0x489C68: add      ecx, 0xf
0x489C6B: imul     eax, dword ptr [ebp - 0x14], 0x25
0x489C6F: add      ecx, eax
0x489C71: mov      edx, dword ptr [ebp - 0x20]
0x489C74: add      edx, 0x30
0x489C77: add      edx, 0x2e
0x489C7A: mov      eax, dword ptr [ebp - 0x74]
0x489C7D: call     0x4180dc
0x489C82: jmp      0x489ca9
0x489C84: push     0x489fe8 ; "-"
0x489C89: mov      ecx, dword ptr [ebp - 0x24]
0x489C8C: add      ecx, 8
0x489C8F: add      ecx, 0xf
0x489C92: imul     eax, dword ptr [ebp - 0x14], 0x25
0x489C96: add      ecx, eax
0x489C98: mov      edx, dword ptr [ebp - 0x20]
0x489C9B: add      edx, 0x30
0x489C9E: add      edx, 0x2e
0x489CA1: mov      eax, dword ptr [ebp - 0x74]
0x489CA4: call     0x4180dc
0x489CA9: inc      dword ptr [ebp - 0xc]
0x489CAC: dec      dword ptr [ebp - 0x70]
0x489CAF: jne      0x489b45
0x489CB5: mov      eax, dword ptr [ebp - 0x74]
0x489CB8: call     0x44d104
0x489CBD: mov      eax, dword ptr [0x4a413c] ; *0x4A413C->0x4F91C8
0x489CC2: cmp      byte ptr [eax], 0
0x489CC5: je       0x489ea3
0x489CCB: mov      eax, dword ptr [0x4a413c] ; *0x4A413C->0x4F91C8
0x489CD0: mov      edx, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x489CD6: mov      esi, eax
0x489CD8: mov      edi, edx
0x489CDA: mov      ecx, 0xd
0x489CDF: rep movsd dword ptr es:[edi], dword ptr [esi]
0x489CE1: lea      eax, [ebp - 0x58]
0x489CE4: push     eax
0x489CE5: lea      eax, [ebp - 0x5c]
0x489CE8: push     eax
0x489CE9: lea      eax, [ebp - 0x5d]
0x489CEC: push     eax
0x489CED: push     1
0x489CEF: lea      ecx, [ebp - 0x54]
0x489CF2: lea      edx, [ebp - 0x50]
0x489CF5: mov      eax, dword ptr [ebp - 4]
0x489CF8: call     0x48c3c8
0x489CFD: cmp      dword ptr [ebp - 0x50], 0
0x489D01: je       0x489e9b
0x489D07: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x489D0C: cmp      word ptr [eax + 0x30], 0
0x489D11: jne      0x489d1c
0x489D13: mov      dword ptr [ebp - 0x44], 0xff
0x489D1A: jmp      0x489d23
0x489D1C: mov      dword ptr [ebp - 0x44], 0xffffff
0x489D23: mov      eax, dword ptr [ebp - 8]
0x489D26: call     0x44d8b4
0x489D2B: mov      dword ptr [ebp - 0x78], eax
0x489D2E: push     1
0x489D30: mov      eax, dword ptr [ebp - 0x78]
0x489D33: call     0x41834c
0x489D38: push     eax
0x489D39: call     0x406434
0x489D3E: mov      eax, dword ptr [ebp - 0x78]
0x489D41: mov      eax, dword ptr [eax + 0xc]
0x489D44: call     0x417674
0x489D49: mov      dword ptr [ebp - 0x48], eax
0x489D4C: mov      eax, dword ptr [ebp - 0x78]
0x489D4F: mov      eax, dword ptr [eax + 0xc]
0x489D52: mov      edx, 8
0x489D57: call     0x417690
0x489D5C: mov      eax, dword ptr [ebp - 0x78]
0x489D5F: mov      eax, dword ptr [eax + 0xc]
0x489D62: mov      edx, 0xffff
0x489D67: call     0x4173fc
0x489D6C: mov      eax, dword ptr [ebp - 0x50]
0x489D6F: push     eax
0x489D70: mov      eax, dword ptr [ebp - 0x64]
0x489D73: mov      edx, dword ptr [eax + 0x34]
0x489D76: add      edx, 0x110
0x489D7C: mov      eax, dword ptr [ebp - 0x64]
0x489D7F: call     0x456fdc
0x489D84: push     eax
0x489D85: mov      eax, dword ptr [ebp - 0x64]
0x489D88: mov      edx, dword ptr [eax + 0x30]
0x489D8B: add      edx, 0x25
0x489D8E: mov      eax, dword ptr [ebp - 0x64]
0x489D91: call     0x456f94
0x489D96: mov      edx, eax
0x489D98: mov      eax, dword ptr [ebp - 0x78]
0x489D9B: pop      ecx
0x489D9C: call     0x4180dc
0x489DA1: mov      eax, dword ptr [ebp - 0x78]
0x489DA4: mov      eax, dword ptr [eax + 0xc]
0x489DA7: mov      edx, dword ptr [ebp - 0x44]
0x489DAA: call     0x4173fc
0x489DAF: mov      eax, dword ptr [ebp - 0x54]
0x489DB2: push     eax
0x489DB3: mov      eax, dword ptr [ebp - 0x64]
0x489DB6: mov      edx, dword ptr [eax + 0x34]
0x489DB9: add      edx, 0x110
0x489DBF: mov      eax, dword ptr [ebp - 0x64]
0x489DC2: call     0x456fdc
0x489DC7: push     eax
0x489DC8: mov      edx, dword ptr [ebp - 0x50]
0x489DCB: mov      eax, dword ptr [ebp - 0x78]
0x489DCE: call     0x4181ac
0x489DD3: mov      edx, eax
0x489DD5: mov      eax, dword ptr [ebp - 0x64]
0x489DD8: mov      eax, dword ptr [eax + 0x30]
0x489DDB: add      eax, 0x25
0x489DDE: add      edx, eax
0x489DE0: mov      eax, dword ptr [ebp - 0x64]
0x489DE3: call     0x456f94
0x489DE8: mov      edx, eax
0x489DEA: mov      eax, dword ptr [ebp - 0x78]
0x489DED: pop      ecx
0x489DEE: call     0x4180dc
0x489DF3: mov      eax, dword ptr [ebp - 0x58]
0x489DF6: push     eax
0x489DF7: mov      edx, 0x48a0e0 ; "A"
0x489DFC: mov      eax, dword ptr [ebp - 0x78]
0x489DFF: call     0x4181c8
0x489E04: mov      edx, eax
0x489E06: mov      eax, dword ptr [ebp - 0x64]
0x489E09: mov      eax, dword ptr [eax + 0x34]
0x489E0C: add      eax, 0x110
0x489E11: add      edx, eax
0x489E13: add      edx, 2
0x489E16: mov      eax, dword ptr [ebp - 0x64]
0x489E19: call     0x456fdc
0x489E1E: push     eax
0x489E1F: mov      eax, dword ptr [ebp - 0x64]
0x489E22: mov      edx, dword ptr [eax + 0x30]
0x489E25: add      edx, 0x25
0x489E28: mov      eax, dword ptr [ebp - 0x64]
0x489E2B: call     0x456f94
0x489E30: mov      edx, eax
0x489E32: mov      eax, dword ptr [ebp - 0x78]
0x489E35: pop      ecx
0x489E36: call     0x4180dc
0x489E3B: mov      eax, dword ptr [ebp - 0x5c]
0x489E3E: push     eax
0x489E3F: mov      edx, 0x48a0e0 ; "A"
0x489E44: mov      eax, dword ptr [ebp - 0x78]
0x489E47: call     0x4181c8
0x489E4C: add      eax, 2
0x489E4F: mov      edx, eax
0x489E51: add      edx, edx
0x489E53: mov      eax, dword ptr [ebp - 0x64]
0x489E56: mov      eax, dword ptr [eax + 0x34]
0x489E59: add      eax, 0x110
0x489E5E: add      edx, eax
0x489E60: mov      eax, dword ptr [ebp - 0x64]
0x489E63: call     0x456fdc
0x489E68: push     eax
0x489E69: mov      eax, dword ptr [ebp - 0x64]
0x489E6C: mov      edx, dword ptr [eax + 0x30]
0x489E6F: add      edx, 0x25
0x489E72: mov      eax, dword ptr [ebp - 0x64]
0x489E75: call     0x456f94
0x489E7A: mov      edx, eax
0x489E7C: mov      eax, dword ptr [ebp - 0x78]
0x489E7F: pop      ecx
0x489E80: call     0x4180dc
0x489E85: mov      eax, dword ptr [ebp - 0x78]
0x489E88: mov      eax, dword ptr [eax + 0xc]
0x489E8B: mov      edx, dword ptr [ebp - 0x48]
0x489E8E: call     0x417690
0x489E93: mov      eax, dword ptr [ebp - 0x78]
0x489E96: call     0x44d104
0x489E9B: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x489EA0: mov      byte ptr [eax], 0
0x489EA3: mov      eax, dword ptr [ebp - 8]
0x489EA6: call     0x44d8b4
0x489EAB: mov      dword ptr [ebp - 0x7c], eax
0x489EAE: push     1
0x489EB0: mov      eax, dword ptr [ebp - 0x7c]
0x489EB3: call     0x41834c
0x489EB8: push     eax
0x489EB9: call     0x406434
0x489EBE: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x489EC3: mov      eax, dword ptr [eax]
0x489EC5: mov      edx, dword ptr [eax + 0x2c]
0x489EC8: mov      eax, dword ptr [ebp - 0x7c]
0x489ECB: mov      eax, dword ptr [eax + 0xc]
0x489ECE: call     0x4173fc
0x489ED3: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x489ED8: mov      eax, dword ptr [eax]
0x489EDA: mov      eax, dword ptr [eax + 0x28]
0x489EDD: push     eax
0x489EDE: mov      eax, dword ptr [ebp - 0x64]
0x489EE1: mov      edx, dword ptr [eax + 0x34]
0x489EE4: add      edx, 0x17
0x489EE7: mov      eax, dword ptr [ebp - 0x64]
0x489EEA: call     0x456fdc
0x489EEF: push     eax
0x489EF0: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x489EF5: mov      eax, dword ptr [eax]
0x489EF7: mov      edx, dword ptr [eax + 0x418]
0x489EFD: mov      eax, dword ptr [ebp - 0x7c]
0x489F00: call     0x4181ac
0x489F05: sar      eax, 1
0x489F07: jns      0x489f0c
0x489F09: adc      eax, 0
0x489F0C: mov      edx, dword ptr [ebp - 0x64]
0x489F0F: mov      edx, dword ptr [edx + 0x30]
0x489F12: add      edx, 0x7a
0x489F15: sub      edx, eax
0x489F17: mov      eax, dword ptr [ebp - 0x64]
0x489F1A: call     0x456f94
0x489F1F: mov      edx, eax
0x489F21: mov      eax, dword ptr [ebp - 0x7c]
0x489F24: pop      ecx
0x489F25: call     0x4180dc
0x489F2A: mov      eax, dword ptr [ebp - 4]
0x489F2D: cmp      dword ptr [eax + 0x618], 0
0x489F34: jne      0x489f9e
0x489F36: mov      eax, dword ptr [ebp - 0x7c]
0x489F39: mov      eax, dword ptr [eax + 0xc]
0x489F3C: mov      edx, 0xc0c0c0
0x489F41: call     0x4173fc
0x489F46: mov      eax, dword ptr [0x4a3de4] ; *0x4A3DE4->0x4F7EEC
0x489F4B: push     dword ptr [eax]
0x489F4D: push     0x48a0ec ; " "
0x489F52: mov      eax, dword ptr [0x4a3e88] ; *0x4A3E88->0x4F7EF0
0x489F57: push     dword ptr [eax]
0x489F59: lea      eax, [ebp - 0x94]
0x489F5F: mov      edx, 3
0x489F64: call     0x403d94
0x489F69: mov      eax, dword ptr [ebp - 0x94]
0x489F6F: push     eax
0x489F70: mov      eax, dword ptr [ebp - 0x64]
0x489F73: mov      edx, dword ptr [eax + 0x34]
0x489F76: add      edx, 0x37
0x489F79: mov      eax, dword ptr [ebp - 0x64]
0x489F7C: call     0x456fdc
0x489F81: push     eax
0x489F82: mov      eax, dword ptr [ebp - 0x64]
0x489F85: mov      edx, dword ptr [eax + 0x30]
0x489F88: add      edx, 0x2d
0x489F8B: mov      eax, dword ptr [ebp - 0x64]
0x489F8E: call     0x456f94
0x489F93: mov      edx, eax
0x489F95: mov      eax, dword ptr [ebp - 0x7c]
0x489F98: pop      ecx
0x489F99: call     0x4180dc
0x489F9E: mov      eax, dword ptr [ebp - 0x7c]
0x489FA1: call     0x44d104
0x489FA6: xor      eax, eax
0x489FA8: pop      edx
0x489FA9: pop      ecx
0x489FAA: pop      ecx
0x489FAB: mov      dword ptr fs:[eax], edx
0x489FAE: push     0x489fd8
0x489FB3: lea      eax, [ebp - 0x9c]
0x489FB9: mov      edx, 3
0x489FBE: call     0x403a7c
0x489FC3: lea      eax, [ebp - 0x5c]
0x489FC6: mov      edx, 4
0x489FCB: call     0x403a7c
0x489FD0: ret      
0x489FD1: jmp      0x403518
0x489FD6: jmp      0x489fb3
0x489FD8: pop      edi
0x489FD9: pop      esi
0x489FDA: pop      ebx
0x489FDB: mov      esp, ebp
0x489FDD: pop      ebp
0x489FDE: ret      
0x489FDF: add      bh, bh
0x489FE3: inc      dword ptr [ecx]
0x489FE5: add      byte ptr [eax], al
0x489FE7: add      byte ptr [0xff000000], ch
0x489FEF: inc      dword ptr [ecx]
0x489FF1: add      byte ptr [eax], al
0x489FF3: add      byte ptr [edi], ch
0x489FF5: add      byte ptr [eax], al
0x489FF7: add      bh, bh
0x489FFB: inc      dword ptr [esi]
0x489FFD: add      byte ptr [eax], al
0x489FFF: add      byte ptr [esi - 0x29162e53], bh
0x48A005: mov      ch, 0
0x48A007: add      bh, bh
0x48A00B: inc      dword ptr [ecx]
0x48A00D: add      byte ptr [eax], al
0x48A00F: add      byte ptr [0xff000000], ah
0x48A017: dec      dword ptr [eax]
0x48A019: add      byte ptr [eax], al
0x48A01B: add      byte ptr [ecx - 0x29034f4d], dh
0x48A021: fadd     st(1)
0x48A023: mov      edi, 0
0x48A030: [STR len=6] "负重量"
0x48A040: [STR len=4] "腕力"
0x48A050: [STR len=7] "精确度 "
0x48A060: [STR len=7] "敏捷度 "
0x48A070: [STR len=8] "魔法防御"
0x48A084: [STR len=1] "+"
0x48A090: [STR len=8] "中毒防御"
0x48A0A4: [STR len=8] "中毒恢复"
0x48A0B8: [STR len=8] "体力恢复"
0x48A0CC: [STR len=8] "魔法恢复"
0x48A0E0: [STR len=1] "A"
0x48A0EC: [STR len=1] " "
0x48A0F0: push     ebp
0x48A0F1: mov      ebp, esp
0x48A0F3: add      esp, -0x24
0x48A0F6: mov      dword ptr [ebp - 0xc], ecx
0x48A0F9: mov      dword ptr [ebp - 8], edx
0x48A0FC: mov      dword ptr [ebp - 4], eax
0x48A0FF: mov      eax, dword ptr [ebp - 4]
0x48A102: cmp      dword ptr [eax + 0x618], 0
0x48A109: jne      0x48a69d
0x48A10F: mov      eax, dword ptr [ebp - 8]
0x48A112: mov      edx, dword ptr [ebp - 4]
0x48A115: cmp      eax, dword ptr [edx + 0x380]
0x48A11B: jne      0x48a1fd
0x48A121: mov      eax, dword ptr [0x4a3db0] ; *0x4A3DB0->0x4F7EFC
0x48A126: cmp      byte ptr [eax + 0x9c], 0
0x48A12D: je       0x48a1fd
0x48A133: mov      eax, dword ptr [0x4a3db0] ; *0x4A3DB0->0x4F7EFC
0x48A138: movzx    eax, word ptr [eax + 0xb2]
0x48A13F: mov      dword ptr [ebp - 0x10], eax
0x48A142: cmp      dword ptr [ebp - 0x10], 0
0x48A146: jl       0x48a1fd
0x48A14C: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48A151: mov      eax, dword ptr [eax]
0x48A153: mov      eax, dword ptr [eax + 0x32c]
0x48A159: mov      edx, dword ptr [ebp - 0x10]
0x48A15C: call     0x45351c
0x48A161: mov      dword ptr [ebp - 0x14], eax
0x48A164: cmp      dword ptr [ebp - 0x14], 0
0x48A168: je       0x48a1fd
0x48A16E: lea      edx, [ebp - 0x24]
0x48A171: mov      eax, dword ptr [ebp - 0x14]
0x48A174: call     0x44d8ec
0x48A179: lea      eax, [ebp - 0x24]
0x48A17C: push     eax
0x48A17D: mov      eax, dword ptr [ebp - 0x14]
0x48A180: push     eax
0x48A181: push     1
0x48A183: mov      eax, dword ptr [ebp - 0x14]
0x48A186: call     0x44d944
0x48A18B: mov      edx, dword ptr [ebp - 4]
0x48A18E: mov      edx, dword ptr [edx + 0x380]
0x48A194: mov      edx, dword ptr [edx + 0x3c]
0x48A197: sub      edx, eax
0x48A199: sar      edx, 1
0x48A19B: jns      0x48a1a0
0x48A19D: adc      edx, 0
0x48A1A0: mov      eax, dword ptr [ebp - 4]
0x48A1A3: mov      eax, dword ptr [eax + 0x380]
0x48A1A9: add      edx, dword ptr [eax + 0x34]
0x48A1AC: mov      eax, dword ptr [ebp - 4]
0x48A1AF: mov      eax, dword ptr [eax + 0x380]
0x48A1B5: call     0x456fdc
0x48A1BA: push     eax
0x48A1BB: mov      eax, dword ptr [ebp - 0x14]
0x48A1BE: call     0x44dc60
0x48A1C3: mov      edx, dword ptr [ebp - 4]
0x48A1C6: mov      edx, dword ptr [edx + 0x380]
0x48A1CC: mov      edx, dword ptr [edx + 0x38]
0x48A1CF: sub      edx, eax
0x48A1D1: sar      edx, 1
0x48A1D3: jns      0x48a1d8
0x48A1D5: adc      edx, 0
0x48A1D8: mov      eax, dword ptr [ebp - 4]
0x48A1DB: mov      eax, dword ptr [eax + 0x380]
0x48A1E1: add      edx, dword ptr [eax + 0x30]
0x48A1E4: mov      eax, dword ptr [ebp - 4]
0x48A1E7: mov      eax, dword ptr [eax + 0x380]
0x48A1ED: call     0x456f94
0x48A1F2: mov      edx, eax
0x48A1F4: mov      eax, dword ptr [ebp - 0xc]
0x48A1F7: pop      ecx
0x48A1F8: call     0x44d6cc
0x48A1FD: mov      eax, dword ptr [ebp - 8]
0x48A200: mov      edx, dword ptr [ebp - 4]
0x48A203: cmp      eax, dword ptr [edx + 0x384]
0x48A209: jne      0x48a2e5
0x48A20F: mov      eax, dword ptr [0x4a3db0] ; *0x4A3DB0->0x4F7EFC
0x48A214: cmp      byte ptr [eax + 0x68], 0
0x48A218: je       0x48a2e5
0x48A21E: mov      eax, dword ptr [0x4a3db0] ; *0x4A3DB0->0x4F7EFC
0x48A223: movzx    eax, word ptr [eax + 0x7e]
0x48A227: mov      dword ptr [ebp - 0x10], eax
0x48A22A: cmp      dword ptr [ebp - 0x10], 0
0x48A22E: jl       0x48a2e5
0x48A234: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48A239: mov      eax, dword ptr [eax]
0x48A23B: mov      eax, dword ptr [eax + 0x32c]
0x48A241: mov      edx, dword ptr [ebp - 0x10]
0x48A244: call     0x45351c
0x48A249: mov      dword ptr [ebp - 0x14], eax
0x48A24C: cmp      dword ptr [ebp - 0x14], 0
0x48A250: je       0x48a2e5
0x48A256: lea      edx, [ebp - 0x24]
0x48A259: mov      eax, dword ptr [ebp - 0x14]
0x48A25C: call     0x44d8ec
0x48A261: lea      eax, [ebp - 0x24]
0x48A264: push     eax
0x48A265: mov      eax, dword ptr [ebp - 0x14]
0x48A268: push     eax
0x48A269: push     1
0x48A26B: mov      eax, dword ptr [ebp - 0x14]
0x48A26E: call     0x44d944
0x48A273: mov      edx, dword ptr [ebp - 4]
0x48A276: mov      edx, dword ptr [edx + 0x384]
0x48A27C: mov      edx, dword ptr [edx + 0x3c]
0x48A27F: sub      edx, eax
0x48A281: sar      edx, 1
0x48A283: jns      0x48a288
0x48A285: adc      edx, 0
0x48A288: mov      eax, dword ptr [ebp - 4]
0x48A28B: mov      eax, dword ptr [eax + 0x384]
0x48A291: add      edx, dword ptr [eax + 0x34]
0x48A294: mov      eax, dword ptr [ebp - 4]
0x48A297: mov      eax, dword ptr [eax + 0x384]
0x48A29D: call     0x456fdc
0x48A2A2: push     eax
0x48A2A3: mov      eax, dword ptr [ebp - 0x14]
0x48A2A6: call     0x44dc60
0x48A2AB: mov      edx, dword ptr [ebp - 4]
0x48A2AE: mov      edx, dword ptr [edx + 0x384]
0x48A2B4: mov      edx, dword ptr [edx + 0x38]
0x48A2B7: sub      edx, eax
0x48A2B9: sar      edx, 1
0x48A2BB: jns      0x48a2c0
0x48A2BD: adc      edx, 0
0x48A2C0: mov      eax, dword ptr [ebp - 4]
0x48A2C3: mov      eax, dword ptr [eax + 0x384]
0x48A2C9: add      edx, dword ptr [eax + 0x30]
0x48A2CC: mov      eax, dword ptr [ebp - 4]
0x48A2CF: mov      eax, dword ptr [eax + 0x384]
0x48A2D5: call     0x456f94
0x48A2DA: mov      edx, eax
0x48A2DC: mov      eax, dword ptr [ebp - 0xc]
0x48A2DF: pop      ecx
0x48A2E0: call     0x44d6cc
0x48A2E5: mov      eax, dword ptr [ebp - 8]
0x48A2E8: mov      edx, dword ptr [ebp - 4]
0x48A2EB: cmp      eax, dword ptr [edx + 0x388]
0x48A2F1: jne      0x48a3d3
0x48A2F7: mov      eax, dword ptr [0x4a3db0] ; *0x4A3DB0->0x4F7EFC
0x48A2FC: cmp      byte ptr [eax + 0x138], 0
0x48A303: je       0x48a3d3
0x48A309: mov      eax, dword ptr [0x4a3db0] ; *0x4A3DB0->0x4F7EFC
0x48A30E: movzx    eax, word ptr [eax + 0x14e]
0x48A315: mov      dword ptr [ebp - 0x10], eax
0x48A318: cmp      dword ptr [ebp - 0x10], 0
0x48A31C: jl       0x48a3d3
0x48A322: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48A327: mov      eax, dword ptr [eax]
0x48A329: mov      eax, dword ptr [eax + 0x32c]
0x48A32F: mov      edx, dword ptr [ebp - 0x10]
0x48A332: call     0x45351c
0x48A337: mov      dword ptr [ebp - 0x14], eax
0x48A33A: cmp      dword ptr [ebp - 0x14], 0
0x48A33E: je       0x48a3d3
0x48A344: lea      edx, [ebp - 0x24]
0x48A347: mov      eax, dword ptr [ebp - 0x14]
0x48A34A: call     0x44d8ec
0x48A34F: lea      eax, [ebp - 0x24]
0x48A352: push     eax
0x48A353: mov      eax, dword ptr [ebp - 0x14]
0x48A356: push     eax
0x48A357: push     1
0x48A359: mov      eax, dword ptr [ebp - 0x14]
0x48A35C: call     0x44d944
0x48A361: mov      edx, dword ptr [ebp - 4]
0x48A364: mov      edx, dword ptr [edx + 0x388]
0x48A36A: mov      edx, dword ptr [edx + 0x3c]
0x48A36D: sub      edx, eax
0x48A36F: sar      edx, 1
0x48A371: jns      0x48a376
0x48A373: adc      edx, 0
0x48A376: mov      eax, dword ptr [ebp - 4]
0x48A379: mov      eax, dword ptr [eax + 0x388]
0x48A37F: add      edx, dword ptr [eax + 0x34]
0x48A382: mov      eax, dword ptr [ebp - 4]
0x48A385: mov      eax, dword ptr [eax + 0x388]
0x48A38B: call     0x456fdc
0x48A390: push     eax
0x48A391: mov      eax, dword ptr [ebp - 0x14]
0x48A394: call     0x44dc60
0x48A399: mov      edx, dword ptr [ebp - 4]
0x48A39C: mov      edx, dword ptr [edx + 0x388]
0x48A3A2: mov      edx, dword ptr [edx + 0x38]
0x48A3A5: sub      edx, eax
0x48A3A7: sar      edx, 1
0x48A3A9: jns      0x48a3ae
0x48A3AB: adc      edx, 0
0x48A3AE: mov      eax, dword ptr [ebp - 4]
0x48A3B1: mov      eax, dword ptr [eax + 0x388]
0x48A3B7: add      edx, dword ptr [eax + 0x30]
0x48A3BA: mov      eax, dword ptr [ebp - 4]
0x48A3BD: mov      eax, dword ptr [eax + 0x388]
0x48A3C3: call     0x456f94
0x48A3C8: mov      edx, eax
0x48A3CA: mov      eax, dword ptr [ebp - 0xc]
0x48A3CD: pop      ecx
0x48A3CE: call     0x44d6cc
0x48A3D3: mov      eax, dword ptr [ebp - 8]
0x48A3D6: mov      edx, dword ptr [ebp - 4]
0x48A3D9: cmp      eax, dword ptr [edx + 0x38c]
0x48A3DF: jne      0x48a4c1
0x48A3E5: mov      eax, dword ptr [0x4a3db0] ; *0x4A3DB0->0x4F7EFC
0x48A3EA: cmp      byte ptr [eax + 0x104], 0
0x48A3F1: je       0x48a4c1
0x48A3F7: mov      eax, dword ptr [0x4a3db0] ; *0x4A3DB0->0x4F7EFC
0x48A3FC: movzx    eax, word ptr [eax + 0x11a]
0x48A403: mov      dword ptr [ebp - 0x10], eax
0x48A406: cmp      dword ptr [ebp - 0x10], 0
0x48A40A: jl       0x48a4c1
0x48A410: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48A415: mov      eax, dword ptr [eax]
0x48A417: mov      eax, dword ptr [eax + 0x32c]
0x48A41D: mov      edx, dword ptr [ebp - 0x10]
0x48A420: call     0x45351c
0x48A425: mov      dword ptr [ebp - 0x14], eax
0x48A428: cmp      dword ptr [ebp - 0x14], 0
0x48A42C: je       0x48a4c1
0x48A432: lea      edx, [ebp - 0x24]
0x48A435: mov      eax, dword ptr [ebp - 0x14]
0x48A438: call     0x44d8ec
0x48A43D: lea      eax, [ebp - 0x24]
0x48A440: push     eax
0x48A441: mov      eax, dword ptr [ebp - 0x14]
0x48A444: push     eax
0x48A445: push     1
0x48A447: mov      eax, dword ptr [ebp - 0x14]
0x48A44A: call     0x44d944
0x48A44F: mov      edx, dword ptr [ebp - 4]
0x48A452: mov      edx, dword ptr [edx + 0x38c]
0x48A458: mov      edx, dword ptr [edx + 0x3c]
0x48A45B: sub      edx, eax
0x48A45D: sar      edx, 1
0x48A45F: jns      0x48a464
0x48A461: adc      edx, 0
0x48A464: mov      eax, dword ptr [ebp - 4]
0x48A467: mov      eax, dword ptr [eax + 0x38c]
0x48A46D: add      edx, dword ptr [eax + 0x34]
0x48A470: mov      eax, dword ptr [ebp - 4]
0x48A473: mov      eax, dword ptr [eax + 0x38c]
0x48A479: call     0x456fdc
0x48A47E: push     eax
0x48A47F: mov      eax, dword ptr [ebp - 0x14]
0x48A482: call     0x44dc60
0x48A487: mov      edx, dword ptr [ebp - 4]
0x48A48A: mov      edx, dword ptr [edx + 0x38c]
0x48A490: mov      edx, dword ptr [edx + 0x38]
0x48A493: sub      edx, eax
0x48A495: sar      edx, 1
0x48A497: jns      0x48a49c
0x48A499: adc      edx, 0
0x48A49C: mov      eax, dword ptr [ebp - 4]
0x48A49F: mov      eax, dword ptr [eax + 0x38c]
0x48A4A5: add      edx, dword ptr [eax + 0x30]
0x48A4A8: mov      eax, dword ptr [ebp - 4]
0x48A4AB: mov      eax, dword ptr [eax + 0x38c]
0x48A4B1: call     0x456f94
0x48A4B6: mov      edx, eax
0x48A4B8: mov      eax, dword ptr [ebp - 0xc]
0x48A4BB: pop      ecx
0x48A4BC: call     0x44d6cc
0x48A4C1: mov      eax, dword ptr [ebp - 8]
0x48A4C4: mov      edx, dword ptr [ebp - 4]
0x48A4C7: cmp      eax, dword ptr [edx + 0x390]
0x48A4CD: jne      0x48a5af
0x48A4D3: mov      eax, dword ptr [0x4a3db0] ; *0x4A3DB0->0x4F7EFC
0x48A4D8: cmp      byte ptr [eax + 0x1a0], 0
0x48A4DF: je       0x48a5af
0x48A4E5: mov      eax, dword ptr [0x4a3db0] ; *0x4A3DB0->0x4F7EFC
0x48A4EA: movzx    eax, word ptr [eax + 0x1b6]
0x48A4F1: mov      dword ptr [ebp - 0x10], eax
0x48A4F4: cmp      dword ptr [ebp - 0x10], 0
0x48A4F8: jl       0x48a5af
0x48A4FE: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48A503: mov      eax, dword ptr [eax]
0x48A505: mov      eax, dword ptr [eax + 0x32c]
0x48A50B: mov      edx, dword ptr [ebp - 0x10]
0x48A50E: call     0x45351c
0x48A513: mov      dword ptr [ebp - 0x14], eax
0x48A516: cmp      dword ptr [ebp - 0x14], 0
0x48A51A: je       0x48a5af
0x48A520: lea      edx, [ebp - 0x24]
0x48A523: mov      eax, dword ptr [ebp - 0x14]
0x48A526: call     0x44d8ec
0x48A52B: lea      eax, [ebp - 0x24]
0x48A52E: push     eax
0x48A52F: mov      eax, dword ptr [ebp - 0x14]
0x48A532: push     eax
0x48A533: push     1
0x48A535: mov      eax, dword ptr [ebp - 0x14]
0x48A538: call     0x44d944
0x48A53D: mov      edx, dword ptr [ebp - 4]
0x48A540: mov      edx, dword ptr [edx + 0x390]
0x48A546: mov      edx, dword ptr [edx + 0x3c]
0x48A549: sub      edx, eax
0x48A54B: sar      edx, 1
0x48A54D: jns      0x48a552
0x48A54F: adc      edx, 0
0x48A552: mov      eax, dword ptr [ebp - 4]
0x48A555: mov      eax, dword ptr [eax + 0x390]
0x48A55B: add      edx, dword ptr [eax + 0x34]
0x48A55E: mov      eax, dword ptr [ebp - 4]
0x48A561: mov      eax, dword ptr [eax + 0x390]
0x48A567: call     0x456fdc
0x48A56C: push     eax
0x48A56D: mov      eax, dword ptr [ebp - 0x14]
0x48A570: call     0x44dc60
0x48A575: mov      edx, dword ptr [ebp - 4]
0x48A578: mov      edx, dword ptr [edx + 0x390]
0x48A57E: mov      edx, dword ptr [edx + 0x38]
0x48A581: sub      edx, eax
0x48A583: sar      edx, 1
0x48A585: jns      0x48a58a
0x48A587: adc      edx, 0
0x48A58A: mov      eax, dword ptr [ebp - 4]
0x48A58D: mov      eax, dword ptr [eax + 0x390]
0x48A593: add      edx, dword ptr [eax + 0x30]
0x48A596: mov      eax, dword ptr [ebp - 4]
0x48A599: mov      eax, dword ptr [eax + 0x390]
0x48A59F: call     0x456f94
0x48A5A4: mov      edx, eax
0x48A5A6: mov      eax, dword ptr [ebp - 0xc]
0x48A5A9: pop      ecx
0x48A5AA: call     0x44d6cc
0x48A5AF: mov      eax, dword ptr [ebp - 8]
0x48A5B2: mov      edx, dword ptr [ebp - 4]
0x48A5B5: cmp      eax, dword ptr [edx + 0x394]
0x48A5BB: jne      0x48a69d
0x48A5C1: mov      eax, dword ptr [0x4a3db0] ; *0x4A3DB0->0x4F7EFC
0x48A5C6: cmp      byte ptr [eax + 0x16c], 0
0x48A5CD: je       0x48a69d
0x48A5D3: mov      eax, dword ptr [0x4a3db0] ; *0x4A3DB0->0x4F7EFC
0x48A5D8: movzx    eax, word ptr [eax + 0x182]
0x48A5DF: mov      dword ptr [ebp - 0x10], eax
0x48A5E2: cmp      dword ptr [ebp - 0x10], 0
0x48A5E6: jl       0x48a69d
0x48A5EC: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48A5F1: mov      eax, dword ptr [eax]
0x48A5F3: mov      eax, dword ptr [eax + 0x32c]
0x48A5F9: mov      edx, dword ptr [ebp - 0x10]
0x48A5FC: call     0x45351c
0x48A601: mov      dword ptr [ebp - 0x14], eax
0x48A604: cmp      dword ptr [ebp - 0x14], 0
0x48A608: je       0x48a69d
0x48A60E: lea      edx, [ebp - 0x24]
0x48A611: mov      eax, dword ptr [ebp - 0x14]
0x48A614: call     0x44d8ec
0x48A619: lea      eax, [ebp - 0x24]
0x48A61C: push     eax
0x48A61D: mov      eax, dword ptr [ebp - 0x14]
0x48A620: push     eax
0x48A621: push     1
0x48A623: mov      eax, dword ptr [ebp - 0x14]
0x48A626: call     0x44d944
0x48A62B: mov      edx, dword ptr [ebp - 4]
0x48A62E: mov      edx, dword ptr [edx + 0x394]
0x48A634: mov      edx, dword ptr [edx + 0x3c]
0x48A637: sub      edx, eax
0x48A639: sar      edx, 1
0x48A63B: jns      0x48a640
0x48A63D: adc      edx, 0
0x48A640: mov      eax, dword ptr [ebp - 4]
0x48A643: mov      eax, dword ptr [eax + 0x394]
0x48A649: add      edx, dword ptr [eax + 0x34]
0x48A64C: mov      eax, dword ptr [ebp - 4]
0x48A64F: mov      eax, dword ptr [eax + 0x394]
0x48A655: call     0x456fdc
0x48A65A: push     eax
0x48A65B: mov      eax, dword ptr [ebp - 0x14]
0x48A65E: call     0x44dc60
0x48A663: mov      edx, dword ptr [ebp - 4]
0x48A666: mov      edx, dword ptr [edx + 0x394]
0x48A66C: mov      edx, dword ptr [edx + 0x38]
0x48A66F: sub      edx, eax
0x48A671: sar      edx, 1
0x48A673: jns      0x48a678
0x48A675: adc      edx, 0
0x48A678: mov      eax, dword ptr [ebp - 4]
0x48A67B: mov      eax, dword ptr [eax + 0x394]
0x48A681: add      edx, dword ptr [eax + 0x30]
0x48A684: mov      eax, dword ptr [ebp - 4]
0x48A687: mov      eax, dword ptr [eax + 0x394]
0x48A68D: call     0x456f94
0x48A692: mov      edx, eax
0x48A694: mov      eax, dword ptr [ebp - 0xc]
0x48A697: pop      ecx
0x48A698: call     0x44d6cc
0x48A69D: mov      esp, ebp
0x48A69F: pop      ebp
0x48A6A0: ret      
0x48A6A1: lea      eax, [eax]
0x48A6A4: push     ebp
0x48A6A5: mov      ebp, esp
0x48A6A7: add      esp, -0xc
0x48A6AA: mov      dword ptr [ebp - 8], ecx
0x48A6AD: mov      dword ptr [ebp - 0xc], edx
0x48A6B0: mov      dword ptr [ebp - 4], eax
0x48A6B3: mov      eax, dword ptr [ebp - 4]
0x48A6B6: cmp      dword ptr [eax + 0x618], 3
0x48A6BD: jne      0x48a774
0x48A6C3: mov      edx, dword ptr [ebp - 8]
0x48A6C6: mov      eax, dword ptr [ebp - 4]
0x48A6C9: mov      eax, dword ptr [eax + 0x2c4]
0x48A6CF: call     0x457024
0x48A6D4: mov      edx, dword ptr [ebp - 4]
0x48A6D7: mov      edx, dword ptr [edx + 0x2c4]
0x48A6DD: sub      eax, dword ptr [edx + 0x30]
0x48A6E0: mov      dword ptr [ebp - 8], eax
0x48A6E3: mov      edx, dword ptr [ebp + 8]
0x48A6E6: mov      eax, dword ptr [ebp - 4]
0x48A6E9: mov      eax, dword ptr [eax + 0x2c4]
0x48A6EF: call     0x45706c
0x48A6F4: mov      edx, dword ptr [ebp - 4]
0x48A6F7: mov      edx, dword ptr [edx + 0x2c4]
0x48A6FD: sub      eax, dword ptr [edx + 0x34]
0x48A700: mov      dword ptr [ebp + 8], eax
0x48A703: cmp      dword ptr [ebp - 8], 0x21
0x48A707: jl       0x48a774
0x48A709: cmp      dword ptr [ebp - 8], 0xc7
0x48A710: jg       0x48a774
0x48A712: cmp      dword ptr [ebp + 8], 0x37
0x48A716: jl       0x48a774
0x48A718: cmp      dword ptr [ebp + 8], 0xf0
0x48A71F: jg       0x48a774
0x48A721: mov      eax, dword ptr [ebp + 8]
0x48A724: sub      eax, 0x37
0x48A727: mov      ecx, 0x25
0x48A72C: cdq      
0x48A72D: idiv     ecx
0x48A72F: mov      edx, dword ptr [ebp - 4]
0x48A732: mov      dword ptr [edx + 0x5e0], eax
0x48A738: mov      eax, dword ptr [ebp - 4]
0x48A73B: mov      eax, dword ptr [eax + 0x5e0]
0x48A741: mov      edx, dword ptr [ebp - 4]
0x48A744: add      eax, dword ptr [edx + 0x5e4]
0x48A74A: mov      edx, dword ptr [0x4a3f00] ; *0x4A3F00->0x4F9190
0x48A750: mov      edx, dword ptr [edx]
0x48A752: cmp      eax, dword ptr [edx + 8]
0x48A755: jl       0x48a774
0x48A757: mov      eax, dword ptr [0x4a3f00] ; *0x4A3F00->0x4F9190
0x48A75C: mov      eax, dword ptr [eax]
0x48A75E: mov      eax, dword ptr [eax + 8]
0x48A761: dec      eax
0x48A762: mov      edx, dword ptr [ebp - 4]
0x48A765: sub      eax, dword ptr [edx + 0x5e4]
0x48A76B: mov      edx, dword ptr [ebp - 4]
0x48A76E: mov      dword ptr [edx + 0x5e0], eax
0x48A774: mov      esp, ebp
0x48A776: pop      ebp
0x48A777: ret      4
0x48A77A: mov      eax, eax
0x48A77C: push     ebp
0x48A77D: mov      ebp, esp
0x48A77F: add      esp, -0xc
0x48A782: mov      dword ptr [ebp - 0xc], ecx
0x48A785: mov      dword ptr [ebp - 8], edx
0x48A788: mov      dword ptr [ebp - 4], eax
0x48A78B: xor      edx, edx
0x48A78D: mov      eax, dword ptr [ebp - 4]
0x48A790: mov      eax, dword ptr [eax + 0x2c4]
0x48A796: call     0x458214
0x48A79B: mov      esp, ebp
0x48A79D: pop      ebp
0x48A79E: ret      4
0x48A7A1: lea      eax, [eax]
0x48A7A4: push     ebp
0x48A7A5: mov      ebp, esp
0x48A7A7: add      esp, -0x24
0x48A7AA: mov      dword ptr [ebp - 0xc], ecx
0x48A7AD: mov      dword ptr [ebp - 8], edx
0x48A7B0: mov      dword ptr [ebp - 4], eax
0x48A7B3: mov      eax, dword ptr [ebp - 8]
0x48A7B6: mov      edx, dword ptr [0x456468]
0x48A7BC: call     0x403034
0x48A7C1: mov      dword ptr [ebp - 0x14], eax
0x48A7C4: mov      eax, dword ptr [ebp - 8]
0x48A7C7: cmp      byte ptr [eax + 0x274], 0
0x48A7CE: je       0x48a82d
0x48A7D0: mov      eax, dword ptr [ebp - 0x14]
0x48A7D3: mov      edx, dword ptr [eax + 0x258]
0x48A7D9: mov      eax, dword ptr [ebp - 0x14]
0x48A7DC: mov      eax, dword ptr [eax + 0x254]
0x48A7E2: call     0x45351c
0x48A7E7: mov      dword ptr [ebp - 0x10], eax
0x48A7EA: cmp      dword ptr [ebp - 0x10], 0
0x48A7EE: je       0x48a82d
0x48A7F0: lea      edx, [ebp - 0x24]
0x48A7F3: mov      eax, dword ptr [ebp - 0x10]
0x48A7F6: call     0x44d8ec
0x48A7FB: lea      eax, [ebp - 0x24]
0x48A7FE: push     eax
0x48A7FF: mov      eax, dword ptr [ebp - 0x10]
0x48A802: push     eax
0x48A803: push     1
0x48A805: mov      eax, dword ptr [ebp - 0x14]
0x48A808: mov      edx, dword ptr [eax + 0x34]
0x48A80B: mov      eax, dword ptr [ebp - 0x14]
0x48A80E: call     0x456fdc
0x48A813: push     eax
0x48A814: mov      eax, dword ptr [ebp - 0x14]
0x48A817: mov      edx, dword ptr [eax + 0x30]
0x48A81A: mov      eax, dword ptr [ebp - 0x14]
0x48A81D: call     0x456f94
0x48A822: mov      edx, eax
0x48A824: mov      eax, dword ptr [ebp - 0xc]
0x48A827: pop      ecx
0x48A828: call     0x44d6cc
0x48A82D: mov      esp, ebp
0x48A82F: pop      ebp
0x48A830: ret      
0x48A831: lea      eax, [eax]
0x48A834: push     ebp
0x48A835: mov      ebp, esp
0x48A837: push     ecx
0x48A838: mov      dword ptr [ebp - 4], eax
0x48A83B: mov      eax, dword ptr [ebp - 4]
0x48A83E: mov      eax, dword ptr [eax + 0x618]
0x48A844: sub      eax, 3
0x48A847: jne      0x48a8c7
0x48A849: mov      eax, dword ptr [ebp - 4]
0x48A84C: mov      eax, dword ptr [eax + 0x3f4]
0x48A852: mov      byte ptr [eax + 0x24c], 1
0x48A859: mov      eax, dword ptr [ebp - 4]
0x48A85C: mov      eax, dword ptr [eax + 0x3f8]
0x48A862: mov      byte ptr [eax + 0x24c], 1
0x48A869: mov      eax, dword ptr [ebp - 4]
0x48A86C: mov      eax, dword ptr [eax + 0x3fc]
0x48A872: mov      byte ptr [eax + 0x24c], 1
0x48A879: mov      eax, dword ptr [ebp - 4]
0x48A87C: mov      eax, dword ptr [eax + 0x400]
0x48A882: mov      byte ptr [eax + 0x24c], 1
0x48A889: mov      eax, dword ptr [ebp - 4]
0x48A88C: mov      eax, dword ptr [eax + 0x404]
0x48A892: mov      byte ptr [eax + 0x24c], 1
0x48A899: mov      eax, dword ptr [ebp - 4]
0x48A89C: mov      eax, dword ptr [eax + 0x458]
0x48A8A2: mov      byte ptr [eax + 0x24c], 1
0x48A8A9: mov      eax, dword ptr [ebp - 4]
0x48A8AC: mov      eax, dword ptr [eax + 0x45c]
0x48A8B2: mov      byte ptr [eax + 0x24c], 1
0x48A8B9: mov      eax, dword ptr [ebp - 4]
0x48A8BC: xor      edx, edx
0x48A8BE: mov      dword ptr [eax + 0x60c], edx
0x48A8C4: pop      ecx
0x48A8C5: pop      ebp
0x48A8C6: ret      
0x48A8C7: mov      eax, dword ptr [ebp - 4]
0x48A8CA: mov      eax, dword ptr [eax + 0x3f4]
0x48A8D0: mov      byte ptr [eax + 0x24c], 0
0x48A8D7: mov      eax, dword ptr [ebp - 4]
0x48A8DA: mov      eax, dword ptr [eax + 0x3f8]
0x48A8E0: mov      byte ptr [eax + 0x24c], 0
0x48A8E7: mov      eax, dword ptr [ebp - 4]
0x48A8EA: mov      eax, dword ptr [eax + 0x3fc]
0x48A8F0: mov      byte ptr [eax + 0x24c], 0
0x48A8F7: mov      eax, dword ptr [ebp - 4]
0x48A8FA: mov      eax, dword ptr [eax + 0x400]
0x48A900: mov      byte ptr [eax + 0x24c], 0
0x48A907: mov      eax, dword ptr [ebp - 4]
0x48A90A: mov      eax, dword ptr [eax + 0x404]
0x48A910: mov      byte ptr [eax + 0x24c], 0
0x48A917: mov      eax, dword ptr [ebp - 4]
0x48A91A: mov      eax, dword ptr [eax + 0x458]
0x48A920: mov      byte ptr [eax + 0x24c], 0
0x48A927: mov      eax, dword ptr [ebp - 4]
0x48A92A: mov      eax, dword ptr [eax + 0x45c]
0x48A930: mov      byte ptr [eax + 0x24c], 0
0x48A937: pop      ecx
0x48A938: pop      ebp
0x48A939: ret      
0x48A93A: mov      eax, eax
0x48A93C: push     ebp
0x48A93D: mov      ebp, esp
0x48A93F: add      esp, -0xc
0x48A942: mov      dword ptr [ebp - 0xc], ecx
0x48A945: mov      dword ptr [ebp - 8], edx
0x48A948: mov      dword ptr [ebp - 4], eax
0x48A94B: mov      eax, dword ptr [ebp - 4]
0x48A94E: dec      dword ptr [eax + 0x618]
0x48A954: mov      eax, dword ptr [ebp - 4]
0x48A957: cmp      dword ptr [eax + 0x618], 0
0x48A95E: jge      0x48a96d
0x48A960: mov      eax, dword ptr [ebp - 4]
0x48A963: mov      dword ptr [eax + 0x618], 3
0x48A96D: mov      eax, dword ptr [ebp - 4]
0x48A970: call     0x48a834
0x48A975: mov      esp, ebp
0x48A977: pop      ebp
0x48A978: ret      4
0x48A97B: nop      
0x48A97C: push     ebp
0x48A97D: mov      ebp, esp
0x48A97F: add      esp, -0xc
0x48A982: mov      dword ptr [ebp - 0xc], ecx
0x48A985: mov      dword ptr [ebp - 8], edx
0x48A988: mov      dword ptr [ebp - 4], eax
0x48A98B: mov      eax, dword ptr [ebp - 4]
0x48A98E: inc      dword ptr [eax + 0x618]
0x48A994: mov      eax, dword ptr [ebp - 4]
0x48A997: cmp      dword ptr [eax + 0x618], 3
0x48A99E: jle      0x48a9ab
0x48A9A0: mov      eax, dword ptr [ebp - 4]
0x48A9A3: xor      edx, edx
0x48A9A5: mov      dword ptr [eax + 0x618], edx
0x48A9AB: mov      eax, dword ptr [ebp - 4]
0x48A9AE: call     0x48a834
0x48A9B3: mov      esp, ebp
0x48A9B5: pop      ebp
0x48A9B6: ret      4
0x48A9B9: lea      eax, [eax]
0x48A9BC: push     ebp
0x48A9BD: mov      ebp, esp
0x48A9BF: add      esp, -0x20
0x48A9C2: push     ebx
0x48A9C3: push     esi
0x48A9C4: push     edi
0x48A9C5: xor      ebx, ebx
0x48A9C7: mov      dword ptr [ebp - 0x20], ebx
0x48A9CA: mov      dword ptr [ebp - 0x1c], ecx
0x48A9CD: mov      dword ptr [ebp - 8], edx
0x48A9D0: mov      dword ptr [ebp - 4], eax
0x48A9D3: xor      eax, eax
0x48A9D5: push     ebp
0x48A9D6: push     0x48ae1b
0x48A9DB: push     dword ptr fs:[eax]
0x48A9DE: mov      dword ptr fs:[eax], esp
0x48A9E1: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48A9E6: cmp      dword ptr [eax], 0
0x48A9E9: je       0x48ae05
0x48A9EF: mov      eax, dword ptr [ebp - 4]
0x48A9F2: cmp      dword ptr [eax + 0x618], 0
0x48A9F9: jne      0x48ae05
0x48A9FF: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x48AA04: cmp      byte ptr [eax], 0
0x48AA07: je       0x48acb8
0x48AA0D: mov      byte ptr [ebp - 0x15], 0
0x48AA11: mov      byte ptr [ebp - 0x16], 0
0x48AA15: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48AA1A: cmp      dword ptr [eax], -0x61
0x48AA1D: je       0x48ae05
0x48AA23: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48AA28: cmp      dword ptr [eax], -0x62
0x48AA2B: je       0x48ae05
0x48AA31: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48AA36: cmp      byte ptr [eax + 4], 0
0x48AA3A: je       0x48ae05
0x48AA40: mov      eax, dword ptr [0x4a41e4] ; *0x4A41E4->0x4F93D8
0x48AA45: cmp      byte ptr [eax + 4], 0
0x48AA49: jne      0x48ae05
0x48AA4F: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48AA54: movzx    eax, byte ptr [eax + 0x13]
0x48AA58: call     0x4a0a88
0x48AA5D: mov      dword ptr [ebp - 0xc], eax
0x48AA60: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48AA65: cmp      dword ptr [eax], 0
0x48AA68: jl       0x48abee
0x48AA6E: mov      eax, dword ptr [ebp - 0xc]
0x48AA71: cmp      eax, 8
0x48AA74: ja       0x48ac3e
0x48AA7A: jmp      dword ptr [eax*4 + 0x48aa81]
0x48AA81: movsd    dword ptr es:[edi], dword ptr [esi]
0x48AA82: stosb    byte ptr es:[edi], al
0x48AA83: dec      eax
0x48AA84: add      al, bh
0x48AA86: stosb    byte ptr es:[edi], al
0x48AA87: dec      eax
0x48AA88: add      byte ptr [esi], ch
0x48AA8A: stosd    dword ptr es:[edi], eax
0x48AA8B: dec      eax
0x48AA8C: add      byte ptr [ebx], dl
0x48AA8E: stosd    dword ptr es:[edi], eax
0x48AA8F: dec      eax
0x48AA90: add      byte ptr [ecx - 0x55], cl
0x48AA93: dec      eax
0x48AA94: add      bl, dl
0x48AA96: stosd    dword ptr es:[edi], eax
0x48AA97: dec      eax
0x48AA98: add      byte ptr [edi + 0x640048ab], bl
0x48AA9E: stosd    dword ptr es:[edi], eax
0x48AA9F: dec      eax
0x48AAA0: add      byte ptr [ebx + ebp*4 + 0x48], ah
0x48AAA4: add      byte ptr [ebx + 0x558bf845], cl
0x48AAAA: cld      
0x48AAAB: cmp      eax, dword ptr [edx + 0x39c]
0x48AAB1: jne      0x48ac3e
0x48AAB7: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48AABC: mov      eax, dword ptr [eax]
0x48AABE: cmp      byte ptr [eax + 0xd], 0
0x48AAC2: jne      0x48aad3
0x48AAC4: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48AAC9: cmp      byte ptr [eax + 0x13], 0xa
0x48AACD: jne      0x48ae05
0x48AAD3: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48AAD8: mov      eax, dword ptr [eax]
0x48AADA: cmp      byte ptr [eax + 0xd], 1
0x48AADE: jne      0x48aaef
0x48AAE0: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48AAE5: cmp      byte ptr [eax + 0x13], 0xb
0x48AAE9: jne      0x48ae05
0x48AAEF: mov      byte ptr [ebp - 0x15], 1
0x48AAF3: jmp      0x48ac3e
0x48AAF8: mov      eax, dword ptr [ebp - 8]
0x48AAFB: mov      edx, dword ptr [ebp - 4]
0x48AAFE: cmp      eax, dword ptr [edx + 0x398]
0x48AB04: jne      0x48ac3e
0x48AB0A: mov      byte ptr [ebp - 0x15], 1
0x48AB0E: jmp      0x48ac3e
0x48AB13: mov      eax, dword ptr [ebp - 8]
0x48AB16: mov      edx, dword ptr [ebp - 4]
0x48AB19: cmp      eax, dword ptr [edx + 0x380]
0x48AB1F: jne      0x48ac3e
0x48AB25: mov      byte ptr [ebp - 0x15], 1
0x48AB29: jmp      0x48ac3e
0x48AB2E: mov      eax, dword ptr [ebp - 8]
0x48AB31: mov      edx, dword ptr [ebp - 4]
0x48AB34: cmp      eax, dword ptr [edx + 0x384]
0x48AB3A: jne      0x48ac3e
0x48AB40: mov      byte ptr [ebp - 0x15], 1
0x48AB44: jmp      0x48ac3e
0x48AB49: mov      eax, dword ptr [ebp - 8]
0x48AB4C: mov      edx, dword ptr [ebp - 4]
0x48AB4F: cmp      eax, dword ptr [edx + 0x3a0]
0x48AB55: jne      0x48ac3e
0x48AB5B: mov      byte ptr [ebp - 0x15], 1
0x48AB5F: jmp      0x48ac3e
0x48AB64: mov      eax, dword ptr [ebp - 8]
0x48AB67: mov      edx, dword ptr [ebp - 4]
0x48AB6A: cmp      eax, dword ptr [edx + 0x394]
0x48AB70: jne      0x48ab7d
0x48AB72: mov      dword ptr [ebp - 0xc], 7
0x48AB79: mov      byte ptr [ebp - 0x15], 1
0x48AB7D: mov      eax, dword ptr [ebp - 8]
0x48AB80: mov      edx, dword ptr [ebp - 4]
0x48AB83: cmp      eax, dword ptr [edx + 0x390]
0x48AB89: jne      0x48ac3e
0x48AB8F: mov      dword ptr [ebp - 0xc], 8
0x48AB96: mov      byte ptr [ebp - 0x15], 1
0x48AB9A: jmp      0x48ac3e
0x48AB9F: mov      eax, dword ptr [ebp - 8]
0x48ABA2: mov      edx, dword ptr [ebp - 4]
0x48ABA5: cmp      eax, dword ptr [edx + 0x38c]
0x48ABAB: jne      0x48abb8
0x48ABAD: mov      dword ptr [ebp - 0xc], 5
0x48ABB4: mov      byte ptr [ebp - 0x15], 1
0x48ABB8: mov      eax, dword ptr [ebp - 8]
0x48ABBB: mov      edx, dword ptr [ebp - 4]
0x48ABBE: cmp      eax, dword ptr [edx + 0x388]
0x48ABC4: jne      0x48ac3e
0x48ABC6: mov      dword ptr [ebp - 0xc], 6
0x48ABCD: mov      byte ptr [ebp - 0x15], 1
0x48ABD1: jmp      0x48ac3e
0x48ABD3: mov      eax, dword ptr [ebp - 8]
0x48ABD6: mov      edx, dword ptr [ebp - 4]
0x48ABD9: cmp      eax, dword ptr [edx + 0x38c]
0x48ABDF: jne      0x48ac3e
0x48ABE1: mov      dword ptr [ebp - 0xc], 5
0x48ABE8: mov      byte ptr [ebp - 0x15], 1
0x48ABEC: jmp      0x48ac3e
0x48ABEE: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48ABF3: mov      eax, dword ptr [eax]
0x48ABF5: inc      eax
0x48ABF6: neg      eax
0x48ABF8: mov      dword ptr [ebp - 0x10], eax
0x48ABFB: mov      eax, dword ptr [ebp - 0x10]
0x48ABFE: sub      eax, 9
0x48AC01: jae      0x48ac3e
0x48AC03: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48AC08: add      eax, 4
0x48AC0B: call     0x495940
0x48AC10: imul     eax, dword ptr [ebp - 0x10], 0xd
0x48AC14: mov      edx, dword ptr [0x4a3db0] ; *0x4A3DB0->0x4F7EFC
0x48AC1A: mov      ecx, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48AC20: lea      edi, [edx + eax*4]
0x48AC23: lea      esi, [ecx + 4]
0x48AC26: mov      ecx, 0xd
0x48AC2B: rep movsd dword ptr es:[edi], dword ptr [esi]
0x48AC2D: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48AC32: mov      byte ptr [eax + 4], 0
0x48AC36: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x48AC3B: mov      byte ptr [eax], 0
0x48AC3E: cmp      byte ptr [ebp - 0x15], 0
0x48AC42: je       0x48ae05
0x48AC48: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48AC4D: add      eax, 4
0x48AC50: call     0x495940
0x48AC55: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48AC5A: mov      edx, dword ptr [0x4a41e4] ; *0x4A41E4->0x4F93D8
0x48AC60: mov      esi, eax
0x48AC62: mov      edi, edx
0x48AC64: mov      ecx, 0xe
0x48AC69: rep movsd dword ptr es:[edi], dword ptr [esi]
0x48AC6B: mov      eax, dword ptr [0x4a41e4] ; *0x4A41E4->0x4F93D8
0x48AC70: mov      edx, dword ptr [ebp - 0xc]
0x48AC73: mov      dword ptr [eax], edx
0x48AC75: lea      eax, [ebp - 0x20]
0x48AC78: mov      edx, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48AC7E: add      edx, 4
0x48AC81: call     0x403c78
0x48AC86: mov      eax, dword ptr [ebp - 0x20]
0x48AC89: push     eax
0x48AC8A: mov      dl, byte ptr [ebp - 0xc]
0x48AC8D: mov      ecx, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48AC93: mov      ecx, dword ptr [ecx + 0x30]
0x48AC96: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48AC9B: mov      eax, dword ptr [eax]
0x48AC9D: call     0x4659c4
0x48ACA2: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48ACA7: mov      byte ptr [eax + 4], 0
0x48ACAB: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x48ACB0: mov      byte ptr [eax], 0
0x48ACB3: jmp      0x48ae05
0x48ACB8: mov      byte ptr [ebp - 0x15], 0
0x48ACBC: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48ACC1: cmp      byte ptr [eax + 4], 0
0x48ACC5: jne      0x48ae05
0x48ACCB: mov      eax, dword ptr [0x4a41e4] ; *0x4A41E4->0x4F93D8
0x48ACD0: cmp      byte ptr [eax + 4], 0
0x48ACD4: jne      0x48ae05
0x48ACDA: mov      dword ptr [ebp - 0x14], 0xffffffff
0x48ACE1: mov      eax, dword ptr [ebp - 8]
0x48ACE4: mov      edx, dword ptr [ebp - 4]
0x48ACE7: cmp      eax, dword ptr [edx + 0x39c]
0x48ACED: jne      0x48acf4
0x48ACEF: xor      eax, eax
0x48ACF1: mov      dword ptr [ebp - 0x14], eax
0x48ACF4: mov      eax, dword ptr [ebp - 8]
0x48ACF7: mov      edx, dword ptr [ebp - 4]
0x48ACFA: cmp      eax, dword ptr [edx + 0x398]
0x48AD00: jne      0x48ad09
0x48AD02: mov      dword ptr [ebp - 0x14], 1
0x48AD09: mov      eax, dword ptr [ebp - 8]
0x48AD0C: mov      edx, dword ptr [ebp - 4]
0x48AD0F: cmp      eax, dword ptr [edx + 0x3a0]
0x48AD15: jne      0x48ad1e
0x48AD17: mov      dword ptr [ebp - 0x14], 4
0x48AD1E: mov      eax, dword ptr [ebp - 8]
0x48AD21: mov      edx, dword ptr [ebp - 4]
0x48AD24: cmp      eax, dword ptr [edx + 0x380]
0x48AD2A: jne      0x48ad33
0x48AD2C: mov      dword ptr [ebp - 0x14], 3
0x48AD33: mov      eax, dword ptr [ebp - 8]
0x48AD36: mov      edx, dword ptr [ebp - 4]
0x48AD39: cmp      eax, dword ptr [edx + 0x384]
0x48AD3F: jne      0x48ad48
0x48AD41: mov      dword ptr [ebp - 0x14], 2
0x48AD48: mov      eax, dword ptr [ebp - 8]
0x48AD4B: mov      edx, dword ptr [ebp - 4]
0x48AD4E: cmp      eax, dword ptr [edx + 0x394]
0x48AD54: jne      0x48ad5d
0x48AD56: mov      dword ptr [ebp - 0x14], 7
0x48AD5D: mov      eax, dword ptr [ebp - 8]
0x48AD60: mov      edx, dword ptr [ebp - 4]
0x48AD63: cmp      eax, dword ptr [edx + 0x390]
0x48AD69: jne      0x48ad72
0x48AD6B: mov      dword ptr [ebp - 0x14], 8
0x48AD72: mov      eax, dword ptr [ebp - 8]
0x48AD75: mov      edx, dword ptr [ebp - 4]
0x48AD78: cmp      eax, dword ptr [edx + 0x38c]
0x48AD7E: jne      0x48ad87
0x48AD80: mov      dword ptr [ebp - 0x14], 5
0x48AD87: mov      eax, dword ptr [ebp - 8]
0x48AD8A: mov      edx, dword ptr [ebp - 4]
0x48AD8D: cmp      eax, dword ptr [edx + 0x388]
0x48AD93: jne      0x48ad9c
0x48AD95: mov      dword ptr [ebp - 0x14], 6
0x48AD9C: cmp      dword ptr [ebp - 0x14], 0
0x48ADA0: jl       0x48ae05
0x48ADA2: imul     eax, dword ptr [ebp - 0x14], 0xd
0x48ADA6: mov      edx, dword ptr [0x4a3db0] ; *0x4A3DB0->0x4F7EFC
0x48ADAC: cmp      byte ptr [edx + eax*4], 0
0x48ADB0: je       0x48ae05
0x48ADB2: imul     eax, dword ptr [ebp - 0x14], 0xd
0x48ADB6: mov      edx, dword ptr [0x4a3db0] ; *0x4A3DB0->0x4F7EFC
0x48ADBC: lea      eax, [edx + eax*4]
0x48ADBF: call     0x495940
0x48ADC4: mov      eax, dword ptr [ebp - 0x14]
0x48ADC7: inc      eax
0x48ADC8: neg      eax
0x48ADCA: mov      edx, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48ADD0: mov      dword ptr [edx], eax
0x48ADD2: imul     eax, dword ptr [ebp - 0x14], 0xd
0x48ADD6: mov      edx, dword ptr [0x4a3db0] ; *0x4A3DB0->0x4F7EFC
0x48ADDC: mov      ecx, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48ADE2: lea      esi, [edx + eax*4]
0x48ADE5: lea      edi, [ecx + 4]
0x48ADE8: mov      ecx, 0xd
0x48ADED: rep movsd dword ptr es:[edi], dword ptr [esi]
0x48ADEF: imul     eax, dword ptr [ebp - 0x14], 0xd
0x48ADF3: mov      edx, dword ptr [0x4a3db0] ; *0x4A3DB0->0x4F7EFC
0x48ADF9: mov      byte ptr [edx + eax*4], 0
0x48ADFD: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x48AE02: mov      byte ptr [eax], 1
0x48AE05: xor      eax, eax
0x48AE07: pop      edx
0x48AE08: pop      ecx
0x48AE09: pop      ecx
0x48AE0A: mov      dword ptr fs:[eax], edx
0x48AE0D: push     0x48ae22
0x48AE12: lea      eax, [ebp - 0x20]
0x48AE15: call     0x403a58
0x48AE1A: ret      
0x48AE1B: jmp      0x403518
0x48AE20: jmp      0x48ae12
0x48AE22: pop      edi
0x48AE23: pop      esi
0x48AE24: pop      ebx
0x48AE25: mov      esp, ebp
0x48AE27: pop      ebp
0x48AE28: ret      4
0x48AE2B: nop      
0x48AE2C: push     ebp
0x48AE2D: mov      ebp, esp
0x48AE2F: add      esp, -0x28
0x48AE32: push     ebx
0x48AE33: push     esi
0x48AE34: push     edi
0x48AE35: xor      ebx, ebx
0x48AE37: mov      dword ptr [ebp - 0x14], ebx
0x48AE3A: mov      dword ptr [ebp - 0x18], ebx
0x48AE3D: mov      dword ptr [ebp - 0x1c], ebx
0x48AE40: mov      dword ptr [ebp - 0x20], ebx
0x48AE43: mov      byte ptr [ebp - 0xd], cl
0x48AE46: mov      dword ptr [ebp - 8], edx
0x48AE49: mov      dword ptr [ebp - 4], eax
0x48AE4C: xor      eax, eax
0x48AE4E: push     ebp
0x48AE4F: push     0x48af69
0x48AE54: push     dword ptr fs:[eax]
0x48AE57: mov      dword ptr fs:[eax], esp
0x48AE5A: mov      eax, dword ptr [ebp - 4]
0x48AE5D: cmp      dword ptr [eax + 0x618], 0
0x48AE64: jne      0x48af4e
0x48AE6A: mov      dword ptr [ebp - 0xc], 0xffffffff
0x48AE71: mov      eax, dword ptr [ebp - 8]
0x48AE74: mov      edx, dword ptr [ebp - 4]
0x48AE77: cmp      eax, dword ptr [edx + 0x39c]
0x48AE7D: jne      0x48ae84
0x48AE7F: xor      eax, eax
0x48AE81: mov      dword ptr [ebp - 0xc], eax
0x48AE84: mov      eax, dword ptr [ebp - 8]
0x48AE87: mov      edx, dword ptr [ebp - 4]
0x48AE8A: cmp      eax, dword ptr [edx + 0x398]
0x48AE90: jne      0x48ae99
0x48AE92: mov      dword ptr [ebp - 0xc], 1
0x48AE99: mov      eax, dword ptr [ebp - 8]
0x48AE9C: mov      edx, dword ptr [ebp - 4]
0x48AE9F: cmp      eax, dword ptr [edx + 0x3a0]
0x48AEA5: jne      0x48aeae
0x48AEA7: mov      dword ptr [ebp - 0xc], 4
0x48AEAE: mov      eax, dword ptr [ebp - 8]
0x48AEB1: mov      edx, dword ptr [ebp - 4]
0x48AEB4: cmp      eax, dword ptr [edx + 0x380]
0x48AEBA: jne      0x48aec3
0x48AEBC: mov      dword ptr [ebp - 0xc], 3
0x48AEC3: mov      eax, dword ptr [ebp - 8]
0x48AEC6: mov      edx, dword ptr [ebp - 4]
0x48AEC9: cmp      eax, dword ptr [edx + 0x384]
0x48AECF: jne      0x48aed8
0x48AED1: mov      dword ptr [ebp - 0xc], 2
0x48AED8: mov      eax, dword ptr [ebp - 8]
0x48AEDB: mov      edx, dword ptr [ebp - 4]
0x48AEDE: cmp      eax, dword ptr [edx + 0x394]
0x48AEE4: jne      0x48aeed
0x48AEE6: mov      dword ptr [ebp - 0xc], 7
0x48AEED: mov      eax, dword ptr [ebp - 8]
0x48AEF0: mov      edx, dword ptr [ebp - 4]
0x48AEF3: cmp      eax, dword ptr [edx + 0x390]
0x48AEF9: jne      0x48af02
0x48AEFB: mov      dword ptr [ebp - 0xc], 8
0x48AF02: mov      eax, dword ptr [ebp - 8]
0x48AF05: mov      edx, dword ptr [ebp - 4]
0x48AF08: cmp      eax, dword ptr [edx + 0x38c]
0x48AF0E: jne      0x48af17
0x48AF10: mov      dword ptr [ebp - 0xc], 5
0x48AF17: mov      eax, dword ptr [ebp - 8]
0x48AF1A: mov      edx, dword ptr [ebp - 4]
0x48AF1D: cmp      eax, dword ptr [edx + 0x388]
0x48AF23: jne      0x48af2c
0x48AF25: mov      dword ptr [ebp - 0xc], 6
0x48AF2C: cmp      dword ptr [ebp - 0xc], 0
0x48AF30: jl       0x48af4e
0x48AF32: imul     eax, dword ptr [ebp - 0xc], 0xd
0x48AF36: mov      edx, dword ptr [0x4a3db0] ; *0x4A3DB0->0x4F7EFC
0x48AF3C: mov      ecx, dword ptr [0x4a413c] ; *0x4A413C->0x4F91C8
0x48AF42: lea      esi, [edx + eax*4]
0x48AF45: mov      edi, ecx
0x48AF47: mov      ecx, 0xd
0x48AF4C: rep movsd dword ptr es:[edi], dword ptr [esi]
0x48AF4E: xor      eax, eax
0x48AF50: pop      edx
0x48AF51: pop      ecx
0x48AF52: pop      ecx
0x48AF53: mov      dword ptr fs:[eax], edx
0x48AF56: push     0x48af70
0x48AF5B: lea      eax, [ebp - 0x20]
0x48AF5E: mov      edx, 4
0x48AF63: call     0x403a7c
0x48AF68: ret      
0x48AF69: jmp      0x403518
0x48AF6E: jmp      0x48af5b
0x48AF70: pop      edi
0x48AF71: pop      esi
0x48AF72: pop      ebx
0x48AF73: mov      esp, ebp
0x48AF75: pop      ebp
0x48AF76: ret      8
0x48AF79: lea      eax, [eax]
0x48AF7C: push     ebp
0x48AF7D: mov      ebp, esp
0x48AF7F: add      esp, -0xc
0x48AF82: mov      byte ptr [ebp - 9], cl
0x48AF85: mov      dword ptr [ebp - 8], edx
0x48AF88: mov      dword ptr [ebp - 4], eax
0x48AF8B: mov      eax, dword ptr [0x4a413c] ; *0x4A413C->0x4F91C8
0x48AF90: mov      byte ptr [eax], 0
0x48AF93: mov      esp, ebp
0x48AF95: pop      ebp
0x48AF96: ret      8
0x48AF99: lea      eax, [eax]
0x48AF9C: push     ebp
0x48AF9D: mov      ebp, esp
0x48AF9F: add      esp, -0x30
0x48AFA2: mov      dword ptr [ebp - 0xc], ecx
0x48AFA5: mov      dword ptr [ebp - 8], edx
0x48AFA8: mov      dword ptr [ebp - 4], eax
0x48AFAB: mov      eax, dword ptr [ebp - 8]
0x48AFAE: mov      edx, dword ptr [0x456468]
0x48AFB4: call     0x403034
0x48AFB9: mov      dword ptr [ebp - 0x20], eax
0x48AFBC: mov      eax, dword ptr [ebp - 4]
0x48AFBF: mov      eax, dword ptr [eax + 0x60c]
0x48AFC5: lea      eax, [eax + eax*4]
0x48AFC8: mov      edx, dword ptr [ebp - 0x20]
0x48AFCB: add      eax, dword ptr [edx + 0xc]
0x48AFCE: xor      edx, edx
0x48AFD0: call     0x452bbc
0x48AFD5: mov      dword ptr [ebp - 0x10], eax
0x48AFD8: mov      eax, dword ptr [0x4a3f00] ; *0x4A3F00->0x4F9190
0x48AFDD: mov      eax, dword ptr [eax]
0x48AFDF: mov      eax, dword ptr [eax + 8]
0x48AFE2: cmp      eax, dword ptr [ebp - 0x10]
0x48AFE5: jle      0x48b0dc
0x48AFEB: mov      eax, dword ptr [0x4a3f00] ; *0x4A3F00->0x4F9190
0x48AFF0: mov      eax, dword ptr [eax]
0x48AFF2: mov      edx, dword ptr [ebp - 0x10]
0x48AFF5: call     0x40f6a0
0x48AFFA: mov      dword ptr [ebp - 0x1c], eax
0x48AFFD: mov      eax, dword ptr [ebp - 0x1c]
0x48B000: movzx    eax, byte ptr [eax + 0x18]
0x48B004: add      eax, eax
0x48B006: mov      dword ptr [ebp - 0x14], eax
0x48B009: cmp      dword ptr [ebp - 0x14], 0
0x48B00D: jl       0x48b0dc
0x48B013: mov      eax, dword ptr [ebp - 0x20]
0x48B016: cmp      byte ptr [eax + 0x274], 0
0x48B01D: jne      0x48b080
0x48B01F: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48B024: mov      eax, dword ptr [eax]
0x48B026: mov      eax, dword ptr [eax + 0x348]
0x48B02C: mov      edx, dword ptr [ebp - 0x14]
0x48B02F: call     0x45351c
0x48B034: mov      dword ptr [ebp - 0x18], eax
0x48B037: cmp      dword ptr [ebp - 0x18], 0
0x48B03B: je       0x48b0dc
0x48B041: lea      edx, [ebp - 0x30]
0x48B044: mov      eax, dword ptr [ebp - 0x18]
0x48B047: call     0x44d8ec
0x48B04C: lea      eax, [ebp - 0x30]
0x48B04F: push     eax
0x48B050: mov      eax, dword ptr [ebp - 0x18]
0x48B053: push     eax
0x48B054: push     1
0x48B056: mov      eax, dword ptr [ebp - 0x20]
0x48B059: mov      edx, dword ptr [eax + 0x34]
0x48B05C: mov      eax, dword ptr [ebp - 0x20]
0x48B05F: call     0x456fdc
0x48B064: push     eax
0x48B065: mov      eax, dword ptr [ebp - 0x20]
0x48B068: mov      edx, dword ptr [eax + 0x30]
0x48B06B: mov      eax, dword ptr [ebp - 0x20]
0x48B06E: call     0x456f94
0x48B073: mov      edx, eax
0x48B075: mov      eax, dword ptr [ebp - 0xc]
0x48B078: pop      ecx
0x48B079: call     0x44d6cc
0x48B07E: jmp      0x48b0dc
0x48B080: mov      edx, dword ptr [ebp - 0x14]
0x48B083: inc      edx
0x48B084: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48B089: mov      eax, dword ptr [eax]
0x48B08B: mov      eax, dword ptr [eax + 0x348]
0x48B091: call     0x45351c
0x48B096: mov      dword ptr [ebp - 0x18], eax
0x48B099: cmp      dword ptr [ebp - 0x18], 0
0x48B09D: je       0x48b0dc
0x48B09F: lea      edx, [ebp - 0x30]
0x48B0A2: mov      eax, dword ptr [ebp - 0x18]
0x48B0A5: call     0x44d8ec
0x48B0AA: lea      eax, [ebp - 0x30]
0x48B0AD: push     eax
0x48B0AE: mov      eax, dword ptr [ebp - 0x18]
0x48B0B1: push     eax
0x48B0B2: push     1
0x48B0B4: mov      eax, dword ptr [ebp - 0x20]
0x48B0B7: mov      edx, dword ptr [eax + 0x34]
0x48B0BA: mov      eax, dword ptr [ebp - 0x20]
0x48B0BD: call     0x456fdc
0x48B0C2: push     eax
0x48B0C3: mov      eax, dword ptr [ebp - 0x20]
0x48B0C6: mov      edx, dword ptr [eax + 0x30]
0x48B0C9: mov      eax, dword ptr [ebp - 0x20]
0x48B0CC: call     0x456f94
0x48B0D1: mov      edx, eax
0x48B0D3: mov      eax, dword ptr [ebp - 0xc]
0x48B0D6: pop      ecx
0x48B0D7: call     0x44d6cc
0x48B0DC: mov      esp, ebp
0x48B0DE: pop      ebp
0x48B0DF: ret      
0x48B0E0: push     ebp
0x48B0E1: mov      ebp, esp
0x48B0E3: add      esp, -0x24
0x48B0E6: push     ebx
0x48B0E7: xor      ebx, ebx
0x48B0E9: mov      dword ptr [ebp - 0x24], ebx
0x48B0EC: mov      dword ptr [ebp - 0x20], ecx
0x48B0EF: mov      dword ptr [ebp - 8], edx
0x48B0F2: mov      dword ptr [ebp - 4], eax
0x48B0F5: xor      eax, eax
0x48B0F7: push     ebp
0x48B0F8: push     0x48b22b
0x48B0FD: push     dword ptr fs:[eax]
0x48B100: mov      dword ptr fs:[eax], esp
0x48B103: mov      eax, dword ptr [ebp - 4]
0x48B106: cmp      dword ptr [eax + 0x618], 3
0x48B10D: jne      0x48b215
0x48B113: mov      eax, dword ptr [ebp - 8]
0x48B116: mov      eax, dword ptr [eax + 0xc]
0x48B119: mov      edx, dword ptr [ebp - 4]
0x48B11C: add      eax, dword ptr [edx + 0x5e4]
0x48B122: mov      dword ptr [ebp - 0x10], eax
0x48B125: cmp      dword ptr [ebp - 0x10], 0
0x48B129: jl       0x48b215
0x48B12F: mov      eax, dword ptr [0x4a3f00] ; *0x4A3F00->0x4F9190
0x48B134: mov      eax, dword ptr [eax]
0x48B136: mov      eax, dword ptr [eax + 8]
0x48B139: cmp      eax, dword ptr [ebp - 0x10]
0x48B13C: jle      0x48b215
0x48B142: mov      eax, dword ptr [0x4a3f00] ; *0x4A3F00->0x4F9190
0x48B147: mov      eax, dword ptr [eax]
0x48B149: mov      edx, dword ptr [ebp - 0x10]
0x48B14C: call     0x40f6a0
0x48B151: mov      dword ptr [ebp - 0x18], eax
0x48B154: mov      eax, dword ptr [ebp - 0x18]
0x48B157: movzx    eax, byte ptr [eax]
0x48B15A: mov      word ptr [ebp - 0x12], ax
0x48B15E: lea      eax, [ebp - 0x12]
0x48B161: push     eax
0x48B162: lea      eax, [ebp - 0x24]
0x48B165: mov      edx, dword ptr [ebp - 0x18]
0x48B168: add      edx, 0xa
0x48B16B: call     0x403c78
0x48B170: mov      ecx, dword ptr [ebp - 0x24]
0x48B173: mov      eax, dword ptr [ebp - 0x18]
0x48B176: movzx    eax, byte ptr [eax + 0x18]
0x48B17A: mov      edx, eax
0x48B17C: add      edx, edx
0x48B17E: mov      eax, dword ptr [ebp - 4]
0x48B181: call     0x4906ac
0x48B186: mov      al, byte ptr [ebp - 0x12]
0x48B189: mov      byte ptr [ebp - 0x13], al
0x48B18C: mov      eax, dword ptr [0x4a3f00] ; *0x4A3F00->0x4F9190
0x48B191: mov      eax, dword ptr [eax]
0x48B193: mov      eax, dword ptr [eax + 8]
0x48B196: dec      eax
0x48B197: test     eax, eax
0x48B199: jl       0x48b1e5
0x48B19B: inc      eax
0x48B19C: mov      dword ptr [ebp - 0x1c], eax
0x48B19F: mov      dword ptr [ebp - 0xc], 0
0x48B1A6: mov      eax, dword ptr [0x4a3f00] ; *0x4A3F00->0x4F9190
0x48B1AB: mov      eax, dword ptr [eax]
0x48B1AD: mov      edx, dword ptr [ebp - 0xc]
0x48B1B0: call     0x40f6a0
0x48B1B5: mov      dword ptr [ebp - 0x18], eax
0x48B1B8: mov      eax, dword ptr [ebp - 0x18]
0x48B1BB: mov      al, byte ptr [eax]
0x48B1BD: cmp      al, byte ptr [ebp - 0x13]
0x48B1C0: jne      0x48b1dd
0x48B1C2: mov      eax, dword ptr [ebp - 0x18]
0x48B1C5: mov      byte ptr [eax], 0
0x48B1C8: mov      eax, dword ptr [ebp - 0x18]
0x48B1CB: movzx    edx, word ptr [eax + 8]
0x48B1CF: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48B1D4: mov      eax, dword ptr [eax]
0x48B1D6: xor      ecx, ecx
0x48B1D8: call     0x465c34
0x48B1DD: inc      dword ptr [ebp - 0xc]
0x48B1E0: dec      dword ptr [ebp - 0x1c]
0x48B1E3: jne      0x48b1a6
0x48B1E5: mov      eax, dword ptr [0x4a3f00] ; *0x4A3F00->0x4F9190
0x48B1EA: mov      eax, dword ptr [eax]
0x48B1EC: mov      edx, dword ptr [ebp - 0x10]
0x48B1EF: call     0x40f6a0
0x48B1F4: mov      dword ptr [ebp - 0x18], eax
0x48B1F7: mov      al, byte ptr [ebp - 0x13]
0x48B1FA: mov      edx, dword ptr [ebp - 0x18]
0x48B1FD: mov      byte ptr [edx], al
0x48B1FF: mov      eax, dword ptr [ebp - 0x18]
0x48B202: movzx    edx, word ptr [eax + 8]
0x48B206: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48B20B: mov      eax, dword ptr [eax]
0x48B20D: mov      cl, byte ptr [ebp - 0x13]
0x48B210: call     0x465c34
0x48B215: xor      eax, eax
0x48B217: pop      edx
0x48B218: pop      ecx
0x48B219: pop      ecx
0x48B21A: mov      dword ptr fs:[eax], edx
0x48B21D: push     0x48b232
0x48B222: lea      eax, [ebp - 0x24]
0x48B225: call     0x403a58
0x48B22A: ret      
0x48B22B: jmp      0x403518
0x48B230: jmp      0x48b222
0x48B232: pop      ebx
0x48B233: mov      esp, ebp
0x48B235: pop      ebp
0x48B236: ret      4
0x48B239: lea      eax, [eax]
0x48B23C: push     ebp
0x48B23D: mov      ebp, esp
0x48B23F: add      esp, -0xc
0x48B242: mov      dword ptr [ebp - 0xc], ecx
0x48B245: mov      dword ptr [ebp - 8], edx
0x48B248: mov      dword ptr [ebp - 4], eax
0x48B24B: mov      eax, dword ptr [ebp - 8]
0x48B24E: mov      edx, dword ptr [ebp - 4]
0x48B251: cmp      eax, dword ptr [edx + 0x458]
0x48B257: jne      0x48b270
0x48B259: mov      eax, dword ptr [ebp - 4]
0x48B25C: cmp      dword ptr [eax + 0x60c], 0
0x48B263: jle      0x48b29a
0x48B265: mov      eax, dword ptr [ebp - 4]
0x48B268: dec      dword ptr [eax + 0x60c]
0x48B26E: jmp      0x48b29a
0x48B270: mov      eax, dword ptr [0x4a3f00] ; *0x4A3F00->0x4F9190
0x48B275: mov      eax, dword ptr [eax]
0x48B277: mov      eax, dword ptr [eax + 8]
0x48B27A: add      eax, 4
0x48B27D: mov      ecx, 5
0x48B282: cdq      
0x48B283: idiv     ecx
0x48B285: dec      eax
0x48B286: mov      edx, dword ptr [ebp - 4]
0x48B289: cmp      eax, dword ptr [edx + 0x60c]
0x48B28F: jle      0x48b29a
0x48B291: mov      eax, dword ptr [ebp - 4]
0x48B294: inc      dword ptr [eax + 0x60c]
0x48B29A: mov      esp, ebp
0x48B29C: pop      ebp
0x48B29D: ret      4
0x48B2A0: push     ebp
0x48B2A1: mov      ebp, esp
0x48B2A3: add      esp, 0xffffff68
0x48B2A9: push     ebx
0x48B2AA: xor      ebx, ebx
0x48B2AC: mov      dword ptr [ebp - 0x88], ebx
0x48B2B2: mov      dword ptr [ebp - 0x4c], ebx
0x48B2B5: mov      dword ptr [ebp - 8], ecx
0x48B2B8: mov      dword ptr [ebp - 0x38], edx
0x48B2BB: mov      dword ptr [ebp - 4], eax
0x48B2BE: xor      eax, eax
0x48B2C0: push     ebp
0x48B2C1: push     0x48ba64
0x48B2C6: push     dword ptr fs:[eax]
0x48B2C9: mov      dword ptr fs:[eax], esp
0x48B2CC: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48B2D1: mov      eax, dword ptr [eax]
0x48B2D3: mov      eax, dword ptr [eax + 0x308]
0x48B2D9: mov      edx, 1
0x48B2DE: call     0x45351c
0x48B2E3: mov      dword ptr [ebp - 0xc], eax
0x48B2E6: cmp      dword ptr [ebp - 0xc], 0
0x48B2EA: je       0x48b321
0x48B2EC: lea      edx, [ebp - 0x7c]
0x48B2EF: mov      eax, dword ptr [ebp - 0xc]
0x48B2F2: call     0x44d8ec
0x48B2F7: lea      eax, [ebp - 0x7c]
0x48B2FA: push     eax
0x48B2FB: mov      eax, dword ptr [ebp - 0xc]
0x48B2FE: push     eax
0x48B2FF: push     1
0x48B301: mov      eax, dword ptr [ebp - 4]
0x48B304: mov      eax, dword ptr [eax + 0x2d0]
0x48B30A: mov      ecx, dword ptr [eax + 0x34]
0x48B30D: mov      eax, dword ptr [ebp - 4]
0x48B310: mov      eax, dword ptr [eax + 0x2d0]
0x48B316: mov      edx, dword ptr [eax + 0x30]
0x48B319: mov      eax, dword ptr [ebp - 8]
0x48B31C: call     0x44d6cc
0x48B321: xor      eax, eax
0x48B323: mov      dword ptr [ebp - 0x10], eax
0x48B326: cmp      dword ptr [ebp - 0xc], 0
0x48B32A: je       0x48b3bb
0x48B330: lea      edx, [ebp - 0x5c]
0x48B333: mov      eax, dword ptr [ebp - 0xc]
0x48B336: call     0x44d8ec
0x48B33B: mov      eax, dword ptr [ebp - 0x58]
0x48B33E: add      eax, 0x78
0x48B341: push     eax
0x48B342: lea      eax, [ebp - 0x48]
0x48B345: push     eax
0x48B346: mov      ecx, dword ptr [ebp - 0x54]
0x48B349: mov      edx, dword ptr [ebp - 0x58]
0x48B34C: mov      eax, dword ptr [ebp - 0x5c]
0x48B34F: call     0x40ecc0
0x48B354: mov      eax, dword ptr [ebp - 0xc]
0x48B357: call     0x44d944
0x48B35C: mov      edx, 0x258
0x48B361: sub      edx, eax
0x48B363: mov      dword ptr [ebp - 0x10], edx
0x48B366: lea      eax, [ebp - 0x48]
0x48B369: push     eax
0x48B36A: mov      eax, dword ptr [ebp - 0xc]
0x48B36D: push     eax
0x48B36E: push     1
0x48B370: mov      ecx, dword ptr [ebp - 0x10]
0x48B373: xor      edx, edx
0x48B375: mov      eax, dword ptr [ebp - 8]
0x48B378: call     0x44d6cc
0x48B37D: lea      edx, [ebp - 0x6c]
0x48B380: mov      eax, dword ptr [ebp - 0xc]
0x48B383: call     0x44d8ec
0x48B388: mov      eax, dword ptr [ebp - 0x60]
0x48B38B: push     eax
0x48B38C: lea      eax, [ebp - 0x48]
0x48B38F: push     eax
0x48B390: mov      edx, dword ptr [ebp - 0x68]
0x48B393: add      edx, 0x78
0x48B396: mov      ecx, dword ptr [ebp - 0x64]
0x48B399: mov      eax, dword ptr [ebp - 0x6c]
0x48B39C: call     0x40ecc0
0x48B3A1: lea      eax, [ebp - 0x48]
0x48B3A4: push     eax
0x48B3A5: mov      eax, dword ptr [ebp - 0xc]
0x48B3A8: push     eax
0x48B3A9: push     0
0x48B3AB: mov      ecx, dword ptr [ebp - 0x10]
0x48B3AE: add      ecx, 0x78
0x48B3B1: xor      edx, edx
0x48B3B3: mov      eax, dword ptr [ebp - 8]
0x48B3B6: call     0x44d6cc
0x48B3BB: xor      eax, eax
0x48B3BD: mov      dword ptr [ebp - 0xc], eax
0x48B3C0: mov      eax, dword ptr [0x4a3dd0] ; *0x4A3DD0->0x4F9384
0x48B3C5: mov      eax, dword ptr [eax]
0x48B3C7: sub      eax, 1
0x48B3CA: jb       0x48b3d6
0x48B3CC: je       0x48b3f2
0x48B3CE: dec      eax
0x48B3CF: je       0x48b40e
0x48B3D1: dec      eax
0x48B3D2: je       0x48b42a
0x48B3D4: jmp      0x48b444
0x48B3D6: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48B3DB: mov      eax, dword ptr [eax]
0x48B3DD: mov      eax, dword ptr [eax + 0x308]
0x48B3E3: mov      edx, 0xf
0x48B3E8: call     0x45351c
0x48B3ED: mov      dword ptr [ebp - 0xc], eax
0x48B3F0: jmp      0x48b444
0x48B3F2: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48B3F7: mov      eax, dword ptr [eax]
0x48B3F9: mov      eax, dword ptr [eax + 0x308]
0x48B3FF: mov      edx, 0xc
0x48B404: call     0x45351c
0x48B409: mov      dword ptr [ebp - 0xc], eax
0x48B40C: jmp      0x48b444
0x48B40E: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48B413: mov      eax, dword ptr [eax]
0x48B415: mov      eax, dword ptr [eax + 0x308]
0x48B41B: mov      edx, 0xd
0x48B420: call     0x45351c
0x48B425: mov      dword ptr [ebp - 0xc], eax
0x48B428: jmp      0x48b444
0x48B42A: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48B42F: mov      eax, dword ptr [eax]
0x48B431: mov      eax, dword ptr [eax + 0x308]
0x48B437: mov      edx, 0xe
0x48B43C: call     0x45351c
0x48B441: mov      dword ptr [ebp - 0xc], eax
0x48B444: cmp      dword ptr [ebp - 0xc], 0
0x48B448: je       0x48b47b
0x48B44A: lea      edx, [ebp - 0x7c]
0x48B44D: mov      eax, dword ptr [ebp - 0xc]
0x48B450: call     0x44d8ec
0x48B455: lea      eax, [ebp - 0x7c]
0x48B458: push     eax
0x48B459: mov      eax, dword ptr [ebp - 0xc]
0x48B45C: push     eax
0x48B45D: push     0
0x48B45F: mov      eax, dword ptr [ebp - 4]
0x48B462: mov      eax, dword ptr [eax + 0x2d0]
0x48B468: mov      ecx, dword ptr [eax + 0x34]
0x48B46B: add      ecx, 0x4f
0x48B46E: mov      edx, 0x2ec
0x48B473: mov      eax, dword ptr [ebp - 8]
0x48B476: call     0x44d6cc
0x48B47B: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48B480: cmp      dword ptr [eax], 0
0x48B483: je       0x48b930
0x48B489: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48B48E: mov      eax, dword ptr [eax]
0x48B490: cmp      word ptr [eax + 0x40], 0
0x48B495: jbe      0x48b721
0x48B49B: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48B4A0: mov      eax, dword ptr [eax]
0x48B4A2: cmp      word ptr [eax + 0x42], 0
0x48B4A7: jbe      0x48b721
0x48B4AD: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48B4B2: mov      eax, dword ptr [eax]
0x48B4B4: cmp      byte ptr [eax + 0x12], 0
0x48B4B8: jne      0x48b5d8
0x48B4BE: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48B4C3: mov      eax, dword ptr [eax]
0x48B4C5: cmp      byte ptr [eax + 0x30], 0x1c
0x48B4C9: jae      0x48b5d8
0x48B4CF: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48B4D4: mov      eax, dword ptr [eax]
0x48B4D6: mov      eax, dword ptr [eax + 0x308]
0x48B4DC: mov      edx, 5
0x48B4E1: call     0x45351c
0x48B4E6: mov      dword ptr [ebp - 0xc], eax
0x48B4E9: cmp      dword ptr [ebp - 0xc], 0
0x48B4ED: je       0x48b52b
0x48B4EF: lea      edx, [ebp - 0x48]
0x48B4F2: mov      eax, dword ptr [ebp - 0xc]
0x48B4F5: call     0x44d8ec
0x48B4FA: lea      edx, [ebp - 0x7c]
0x48B4FD: mov      eax, dword ptr [ebp - 0xc]
0x48B500: call     0x44d8ec
0x48B505: mov      eax, dword ptr [ebp - 0x74]
0x48B508: sub      eax, 2
0x48B50B: mov      dword ptr [ebp - 0x40], eax
0x48B50E: lea      eax, [ebp - 0x48]
0x48B511: push     eax
0x48B512: mov      eax, dword ptr [ebp - 0xc]
0x48B515: push     eax
0x48B516: push     0
0x48B518: mov      ecx, dword ptr [ebp - 0x10]
0x48B51B: add      ecx, 0x5a
0x48B51E: mov      edx, 0x26
0x48B523: mov      eax, dword ptr [ebp - 8]
0x48B526: call     0x44d6cc
0x48B52B: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48B530: mov      eax, dword ptr [eax]
0x48B532: mov      eax, dword ptr [eax + 0x308]
0x48B538: mov      edx, 6
0x48B53D: call     0x45351c
0x48B542: mov      dword ptr [ebp - 0xc], eax
0x48B545: cmp      dword ptr [ebp - 0xc], 0
0x48B549: je       0x48b721
0x48B54F: lea      edx, [ebp - 0x48]
0x48B552: mov      eax, dword ptr [ebp - 0xc]
0x48B555: call     0x44d8ec
0x48B55A: lea      edx, [ebp - 0x7c]
0x48B55D: mov      eax, dword ptr [ebp - 0xc]
0x48B560: call     0x44d8ec
0x48B565: mov      eax, dword ptr [ebp - 0x74]
0x48B568: sub      eax, 2
0x48B56B: mov      dword ptr [ebp - 0x40], eax
0x48B56E: fild     dword ptr [ebp - 0x3c]
0x48B571: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48B576: mov      eax, dword ptr [eax]
0x48B578: movzx    eax, word ptr [eax + 0x40]
0x48B57C: mov      dword ptr [ebp - 0x80], eax
0x48B57F: fild     dword ptr [ebp - 0x80]
0x48B582: fdivp    st(1)
0x48B584: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48B589: mov      eax, dword ptr [eax]
0x48B58B: movzx    eax, word ptr [eax + 0x40]
0x48B58F: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48B595: mov      edx, dword ptr [edx]
0x48B597: movzx    edx, word ptr [edx + 0x3c]
0x48B59B: sub      eax, edx
0x48B59D: mov      dword ptr [ebp - 0x84], eax
0x48B5A3: fild     dword ptr [ebp - 0x84]
0x48B5A9: fmulp    st(1)
0x48B5AB: call     0x4029a0
0x48B5B0: mov      dword ptr [ebp - 0x44], eax
0x48B5B3: lea      eax, [ebp - 0x48]
0x48B5B6: push     eax
0x48B5B7: mov      eax, dword ptr [ebp - 0xc]
0x48B5BA: push     eax
0x48B5BB: push     0
0x48B5BD: mov      ecx, dword ptr [ebp - 0x10]
0x48B5C0: add      ecx, 0x5a
0x48B5C3: add      ecx, dword ptr [ebp - 0x44]
0x48B5C6: mov      edx, 0x26
0x48B5CB: mov      eax, dword ptr [ebp - 8]
0x48B5CE: call     0x44d6cc
0x48B5D3: jmp      0x48b721
0x48B5D8: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48B5DD: mov      eax, dword ptr [eax]
0x48B5DF: mov      eax, dword ptr [eax + 0x308]
0x48B5E5: mov      edx, 4
0x48B5EA: call     0x45351c
0x48B5EF: mov      dword ptr [ebp - 0xc], eax
0x48B5F2: cmp      dword ptr [ebp - 0xc], 0
0x48B5F6: je       0x48b721
0x48B5FC: lea      edx, [ebp - 0x48]
0x48B5FF: mov      eax, dword ptr [ebp - 0xc]
0x48B602: call     0x44d8ec
0x48B607: lea      edx, [ebp - 0x7c]
0x48B60A: mov      eax, dword ptr [ebp - 0xc]
0x48B60D: call     0x44d8ec
0x48B612: mov      eax, dword ptr [ebp - 0x74]
0x48B615: sar      eax, 1
0x48B617: jns      0x48b61c
0x48B619: adc      eax, 0
0x48B61C: dec      eax
0x48B61D: mov      dword ptr [ebp - 0x40], eax
0x48B620: fild     dword ptr [ebp - 0x3c]
0x48B623: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48B628: mov      eax, dword ptr [eax]
0x48B62A: movzx    eax, word ptr [eax + 0x40]
0x48B62E: mov      dword ptr [ebp - 0x80], eax
0x48B631: fild     dword ptr [ebp - 0x80]
0x48B634: fdivp    st(1)
0x48B636: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48B63B: mov      eax, dword ptr [eax]
0x48B63D: movzx    eax, word ptr [eax + 0x40]
0x48B641: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48B647: mov      edx, dword ptr [edx]
0x48B649: movzx    edx, word ptr [edx + 0x3c]
0x48B64D: sub      eax, edx
0x48B64F: mov      dword ptr [ebp - 0x84], eax
0x48B655: fild     dword ptr [ebp - 0x84]
0x48B65B: fmulp    st(1)
0x48B65D: call     0x4029a0
0x48B662: mov      dword ptr [ebp - 0x44], eax
0x48B665: lea      eax, [ebp - 0x48]
0x48B668: push     eax
0x48B669: mov      eax, dword ptr [ebp - 0xc]
0x48B66C: push     eax
0x48B66D: push     0
0x48B66F: mov      ecx, dword ptr [ebp - 0x10]
0x48B672: add      ecx, 0x5b
0x48B675: add      ecx, dword ptr [ebp - 0x44]
0x48B678: mov      edx, 0x28
0x48B67D: mov      eax, dword ptr [ebp - 8]
0x48B680: call     0x44d6cc
0x48B685: lea      edx, [ebp - 0x48]
0x48B688: mov      eax, dword ptr [ebp - 0xc]
0x48B68B: call     0x44d8ec
0x48B690: lea      edx, [ebp - 0x7c]
0x48B693: mov      eax, dword ptr [ebp - 0xc]
0x48B696: call     0x44d8ec
0x48B69B: mov      eax, dword ptr [ebp - 0x74]
0x48B69E: sar      eax, 1
0x48B6A0: jns      0x48b6a5
0x48B6A2: adc      eax, 0
0x48B6A5: inc      eax
0x48B6A6: mov      dword ptr [ebp - 0x48], eax
0x48B6A9: lea      edx, [ebp - 0x7c]
0x48B6AC: mov      eax, dword ptr [ebp - 0xc]
0x48B6AF: call     0x44d8ec
0x48B6B4: mov      eax, dword ptr [ebp - 0x74]
0x48B6B7: dec      eax
0x48B6B8: mov      dword ptr [ebp - 0x40], eax
0x48B6BB: fild     dword ptr [ebp - 0x3c]
0x48B6BE: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48B6C3: mov      eax, dword ptr [eax]
0x48B6C5: movzx    eax, word ptr [eax + 0x42]
0x48B6C9: mov      dword ptr [ebp - 0x80], eax
0x48B6CC: fild     dword ptr [ebp - 0x80]
0x48B6CF: fdivp    st(1)
0x48B6D1: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48B6D6: mov      eax, dword ptr [eax]
0x48B6D8: movzx    eax, word ptr [eax + 0x42]
0x48B6DC: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48B6E2: mov      edx, dword ptr [edx]
0x48B6E4: movzx    edx, word ptr [edx + 0x3e]
0x48B6E8: sub      eax, edx
0x48B6EA: mov      dword ptr [ebp - 0x84], eax
0x48B6F0: fild     dword ptr [ebp - 0x84]
0x48B6F6: fmulp    st(1)
0x48B6F8: call     0x4029a0
0x48B6FD: mov      dword ptr [ebp - 0x44], eax
0x48B700: lea      eax, [ebp - 0x48]
0x48B703: push     eax
0x48B704: mov      eax, dword ptr [ebp - 0xc]
0x48B707: push     eax
0x48B708: push     0
0x48B70A: mov      ecx, dword ptr [ebp - 0x10]
0x48B70D: add      ecx, 0x5b
0x48B710: add      ecx, dword ptr [ebp - 0x44]
0x48B713: mov      edx, dword ptr [ebp - 0x48]
0x48B716: add      edx, 0x28
0x48B719: mov      eax, dword ptr [ebp - 8]
0x48B71C: call     0x44d6cc
0x48B721: mov      eax, dword ptr [ebp - 8]
0x48B724: call     0x44d8b4
0x48B729: mov      dword ptr [ebp - 0x30], eax
0x48B72C: lea      edx, [ebp - 0x88]
0x48B732: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48B737: mov      eax, dword ptr [eax]
0x48B739: movzx    eax, byte ptr [eax + 0x30]
0x48B73D: call     0x407d64
0x48B742: mov      eax, dword ptr [ebp - 0x88]
0x48B748: push     eax
0x48B749: mov      ecx, 0x1f0
0x48B74E: mov      edx, 0x294
0x48B753: mov      eax, dword ptr [ebp - 8]
0x48B756: call     0x45ec70
0x48B75B: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48B760: mov      eax, dword ptr [eax]
0x48B762: cmp      dword ptr [eax + 0x4c], 0
0x48B766: jbe      0x48b8d5
0x48B76C: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48B771: mov      eax, dword ptr [eax]
0x48B773: cmp      word ptr [eax + 0x52], 0
0x48B778: jbe      0x48b8d5
0x48B77E: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48B783: mov      eax, dword ptr [eax]
0x48B785: mov      eax, dword ptr [eax + 0x308]
0x48B78B: mov      edx, 7
0x48B790: call     0x45351c
0x48B795: mov      dword ptr [ebp - 0xc], eax
0x48B798: cmp      dword ptr [ebp - 0xc], 0
0x48B79C: je       0x48b8d5
0x48B7A2: lea      edx, [ebp - 0x48]
0x48B7A5: mov      eax, dword ptr [ebp - 0xc]
0x48B7A8: call     0x44d8ec
0x48B7AD: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48B7B2: mov      eax, dword ptr [eax]
0x48B7B4: cmp      dword ptr [eax + 0x48], 0
0x48B7B8: jbe      0x48b7fe
0x48B7BA: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48B7BF: mov      eax, dword ptr [eax]
0x48B7C1: mov      eax, dword ptr [eax + 0x4c]
0x48B7C4: mov      dword ptr [ebp - 0x90], eax
0x48B7CA: xor      eax, eax
0x48B7CC: mov      dword ptr [ebp - 0x8c], eax
0x48B7D2: fild     qword ptr [ebp - 0x90]
0x48B7D8: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48B7DD: mov      eax, dword ptr [eax]
0x48B7DF: mov      eax, dword ptr [eax + 0x48]
0x48B7E2: mov      dword ptr [ebp - 0x98], eax
0x48B7E8: xor      eax, eax
0x48B7EA: mov      dword ptr [ebp - 0x94], eax
0x48B7F0: fild     qword ptr [ebp - 0x98]
0x48B7F6: fdivp    st(1)
0x48B7F8: fstp     qword ptr [ebp - 0x2c]
0x48B7FB: wait     
0x48B7FC: jmp      0x48b806
0x48B7FE: xor      eax, eax
0x48B800: mov      dword ptr [ebp - 0x2c], eax
0x48B803: mov      dword ptr [ebp - 0x28], eax
0x48B806: fld      qword ptr [ebp - 0x2c]
0x48B809: fcomp    dword ptr [0x48ba70]
0x48B80F: fnstsw   ax
0x48B811: sahf     
0x48B812: jbe      0x48b824
0x48B814: fild     dword ptr [ebp - 0x40]
0x48B817: fdiv     qword ptr [ebp - 0x2c]
0x48B81A: call     0x4029a0
0x48B81F: mov      dword ptr [ebp - 0x40], eax
0x48B822: jmp      0x48b829
0x48B824: xor      eax, eax
0x48B826: mov      dword ptr [ebp - 0x40], eax
0x48B829: lea      eax, [ebp - 0x48]
0x48B82C: push     eax
0x48B82D: mov      eax, dword ptr [ebp - 0xc]
0x48B830: push     eax
0x48B831: push     0
0x48B833: mov      ecx, 0x20f
0x48B838: mov      edx, 0x29a
0x48B83D: mov      eax, dword ptr [ebp - 8]
0x48B840: call     0x44d6cc
0x48B845: lea      edx, [ebp - 0x48]
0x48B848: mov      eax, dword ptr [ebp - 0xc]
0x48B84B: call     0x44d8ec
0x48B850: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48B855: mov      eax, dword ptr [eax]
0x48B857: cmp      word ptr [eax + 0x50], 0
0x48B85C: jbe      0x48b88e
0x48B85E: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48B863: mov      eax, dword ptr [eax]
0x48B865: movzx    eax, word ptr [eax + 0x52]
0x48B869: mov      dword ptr [ebp - 0x80], eax
0x48B86C: fild     dword ptr [ebp - 0x80]
0x48B86F: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48B874: mov      eax, dword ptr [eax]
0x48B876: movzx    eax, word ptr [eax + 0x50]
0x48B87A: mov      dword ptr [ebp - 0x84], eax
0x48B880: fild     dword ptr [ebp - 0x84]
0x48B886: fdivp    st(1)
0x48B888: fstp     qword ptr [ebp - 0x2c]
0x48B88B: wait     
0x48B88C: jmp      0x48b896
0x48B88E: xor      eax, eax
0x48B890: mov      dword ptr [ebp - 0x2c], eax
0x48B893: mov      dword ptr [ebp - 0x28], eax
0x48B896: fld      qword ptr [ebp - 0x2c]
0x48B899: fcomp    dword ptr [0x48ba70]
0x48B89F: fnstsw   ax
0x48B8A1: sahf     
0x48B8A2: jbe      0x48b8b4
0x48B8A4: fild     dword ptr [ebp - 0x40]
0x48B8A7: fdiv     qword ptr [ebp - 0x2c]
0x48B8AA: call     0x4029a0
0x48B8AF: mov      dword ptr [ebp - 0x40], eax
0x48B8B2: jmp      0x48b8b9
0x48B8B4: xor      eax, eax
0x48B8B6: mov      dword ptr [ebp - 0x40], eax
0x48B8B9: lea      eax, [ebp - 0x48]
0x48B8BC: push     eax
0x48B8BD: mov      eax, dword ptr [ebp - 0xc]
0x48B8C0: push     eax
0x48B8C1: push     0
0x48B8C3: mov      ecx, 0x230
0x48B8C8: mov      edx, 0x29a
0x48B8CD: mov      eax, dword ptr [ebp - 8]
0x48B8D0: call     0x44d6cc
0x48B8D5: mov      eax, dword ptr [0x4a3d88] ; *0x4A3D88->0x4F938C
0x48B8DA: mov      eax, dword ptr [eax]
0x48B8DC: dec      eax
0x48B8DD: sub      eax, 4
0x48B8E0: jae      0x48b930
0x48B8E2: mov      edx, dword ptr [0x4a3d88] ; *0x4A3D88->0x4F938C
0x48B8E8: mov      edx, dword ptr [edx]
0x48B8EA: add      edx, 0x10
0x48B8ED: dec      edx
0x48B8EE: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48B8F3: mov      eax, dword ptr [eax]
0x48B8F5: mov      eax, dword ptr [eax + 0x308]
0x48B8FB: call     0x45351c
0x48B900: mov      dword ptr [ebp - 0xc], eax
0x48B903: cmp      dword ptr [ebp - 0xc], 0
0x48B907: je       0x48b930
0x48B909: lea      edx, [ebp - 0x7c]
0x48B90C: mov      eax, dword ptr [ebp - 0xc]
0x48B90F: call     0x44d8ec
0x48B914: lea      eax, [ebp - 0x7c]
0x48B917: push     eax
0x48B918: mov      eax, dword ptr [ebp - 0xc]
0x48B91B: push     eax
0x48B91C: push     1
0x48B91E: mov      ecx, 0x229
0x48B923: mov      edx, 0x2f2
0x48B928: mov      eax, dword ptr [ebp - 8]
0x48B92B: call     0x44d6cc
0x48B930: mov      dword ptr [ebp - 0x14], 0xd0
0x48B937: mov      dword ptr [ebp - 0x18], 0x1d6
0x48B93E: push     2
0x48B940: mov      eax, dword ptr [ebp - 8]
0x48B943: call     0x44d8b4
0x48B948: call     0x41834c
0x48B94D: push     eax
0x48B94E: call     0x406434
0x48B953: mov      eax, dword ptr [0x4a3fb8] ; *0x4A3FB8->0x4F7D8C
0x48B958: mov      eax, dword ptr [eax]
0x48B95A: mov      eax, dword ptr [eax + 0x20]
0x48B95D: mov      edx, dword ptr [0x4a3fb8] ; *0x4A3FB8->0x4F7D8C
0x48B963: mov      edx, dword ptr [edx]
0x48B965: mov      edx, dword ptr [edx + 0x20]
0x48B968: add      edx, 9
0x48B96B: dec      edx
0x48B96C: sub      edx, eax
0x48B96E: jl       0x48ba36
0x48B974: inc      edx
0x48B975: mov      dword ptr [ebp - 0x34], edx
0x48B978: mov      dword ptr [ebp - 0x1c], eax
0x48B97B: mov      eax, dword ptr [0x4a3fb8] ; *0x4A3FB8->0x4F7D8C
0x48B980: mov      eax, dword ptr [eax]
0x48B982: mov      eax, dword ptr [eax + 0x18]
0x48B985: mov      edx, dword ptr [eax]
0x48B987: call     dword ptr [edx + 0x14]
0x48B98A: dec      eax
0x48B98B: cmp      eax, dword ptr [ebp - 0x1c]
0x48B98E: jl       0x48ba36
0x48B994: mov      eax, dword ptr [0x4a3fb8] ; *0x4A3FB8->0x4F7D8C
0x48B999: mov      eax, dword ptr [eax]
0x48B99B: mov      eax, dword ptr [eax + 0x18]
0x48B99E: mov      edx, dword ptr [ebp - 0x1c]
0x48B9A1: mov      ecx, dword ptr [eax]
0x48B9A3: call     dword ptr [ecx + 0x18]
0x48B9A6: mov      dword ptr [ebp - 0x20], eax
0x48B9A9: mov      eax, dword ptr [0x4a3fb8] ; *0x4A3FB8->0x4F7D8C
0x48B9AE: mov      eax, dword ptr [eax]
0x48B9B0: mov      eax, dword ptr [eax + 0x1c]
0x48B9B3: mov      edx, dword ptr [ebp - 0x1c]
0x48B9B6: call     0x40f6a0
0x48B9BB: mov      dword ptr [ebp - 0x24], eax
0x48B9BE: mov      eax, dword ptr [ebp - 8]
0x48B9C1: call     0x44d8b4
0x48B9C6: mov      eax, dword ptr [eax + 0xc]
0x48B9C9: mov      edx, dword ptr [ebp - 0x20]
0x48B9CC: call     0x4173fc
0x48B9D1: mov      eax, dword ptr [ebp - 8]
0x48B9D4: call     0x44d8b4
0x48B9D9: mov      eax, dword ptr [eax + 0x14]
0x48B9DC: mov      edx, dword ptr [ebp - 0x24]
0x48B9DF: call     0x417bc0
0x48B9E4: lea      ecx, [ebp - 0x88]
0x48B9EA: mov      eax, dword ptr [0x4a3fb8] ; *0x4A3FB8->0x4F7D8C
0x48B9EF: mov      eax, dword ptr [eax]
0x48B9F1: mov      eax, dword ptr [eax + 0x18]
0x48B9F4: mov      edx, dword ptr [ebp - 0x1c]
0x48B9F7: mov      ebx, dword ptr [eax]
0x48B9F9: call     dword ptr [ebx + 0xc]
0x48B9FC: mov      eax, dword ptr [ebp - 0x88]
0x48BA02: push     eax
0x48BA03: mov      eax, dword ptr [ebp - 8]
0x48BA06: call     0x44d8b4
0x48BA0B: mov      edx, dword ptr [0x4a3fb8] ; *0x4A3FB8->0x4F7D8C
0x48BA11: mov      edx, dword ptr [edx]
0x48BA13: mov      ecx, dword ptr [ebp - 0x1c]
0x48BA16: sub      ecx, dword ptr [edx + 0x20]
0x48BA19: shl      ecx, 2
0x48BA1C: lea      ecx, [ecx + ecx*2]
0x48BA1F: add      ecx, dword ptr [ebp - 0x18]
0x48BA22: mov      edx, dword ptr [ebp - 0x14]
0x48BA25: call     0x4180dc
0x48BA2A: inc      dword ptr [ebp - 0x1c]
0x48BA2D: dec      dword ptr [ebp - 0x34]
0x48BA30: jne      0x48b97b
0x48BA36: mov      eax, dword ptr [ebp - 8]
0x48BA39: call     0x44d8b4
0x48BA3E: call     0x44d104
0x48BA43: xor      eax, eax
0x48BA45: pop      edx
0x48BA46: pop      ecx
0x48BA47: pop      ecx
0x48BA48: mov      dword ptr fs:[eax], edx
0x48BA4B: push     0x48ba6b
0x48BA50: lea      eax, [ebp - 0x88]
0x48BA56: call     0x403a58
0x48BA5B: lea      eax, [ebp - 0x4c]
0x48BA5E: call     0x403a58
0x48BA63: ret      
0x48BA64: jmp      0x403518
0x48BA69: jmp      0x48ba50
0x48BA6B: pop      ebx
0x48BA6C: mov      esp, ebp
0x48BA6E: pop      ebp
0x48BA6F: ret      
0x48BA70: add      byte ptr [eax], al
0x48BA72: add      byte ptr [eax], al
0x48BA74: push     ebp
0x48BA75: mov      ebp, esp
0x48BA77: add      esp, -0x10
0x48BA7A: mov      dword ptr [ebp - 8], ecx
0x48BA7D: mov      dword ptr [ebp - 0x10], edx
0x48BA80: mov      dword ptr [ebp - 4], eax
0x48BA83: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48BA88: mov      eax, dword ptr [eax]
0x48BA8A: mov      eax, dword ptr [eax + 0x308]
0x48BA90: mov      edx, 1
0x48BA95: call     0x45351c
0x48BA9A: mov      dword ptr [ebp - 0xc], eax
0x48BA9D: cmp      dword ptr [ebp - 0xc], 0
0x48BAA1: je       0x48bac3
0x48BAA3: mov      ecx, dword ptr [ebp + 0xc]
0x48BAA6: mov      edx, dword ptr [ebp - 8]
0x48BAA9: mov      eax, dword ptr [ebp - 0xc]
0x48BAAC: call     0x44da34
0x48BAB1: test     eax, eax
0x48BAB3: jle      0x48babd
0x48BAB5: mov      eax, dword ptr [ebp + 8]
0x48BAB8: mov      byte ptr [eax], 1
0x48BABB: jmp      0x48bac3
0x48BABD: mov      eax, dword ptr [ebp + 8]
0x48BAC0: mov      byte ptr [eax], 0
0x48BAC3: mov      esp, ebp
0x48BAC5: pop      ebp
0x48BAC6: ret      8
0x48BAC9: lea      eax, [eax]
0x48BACC: push     ebp
0x48BACD: mov      ebp, esp
0x48BACF: add      esp, -0x24
0x48BAD2: mov      dword ptr [ebp - 0xc], ecx
0x48BAD5: mov      dword ptr [ebp - 8], edx
0x48BAD8: mov      dword ptr [ebp - 4], eax
0x48BADB: mov      eax, dword ptr [ebp - 8]
0x48BADE: mov      edx, dword ptr [0x456468]
0x48BAE4: call     0x40301c
0x48BAE9: test     al, al
0x48BAEB: je       0x48bb5c
0x48BAED: mov      eax, dword ptr [ebp - 8]
0x48BAF0: mov      dword ptr [ebp - 0x10], eax
0x48BAF3: mov      eax, dword ptr [ebp - 0x10]
0x48BAF6: cmp      byte ptr [eax + 0x274], 0
0x48BAFD: je       0x48bb5c
0x48BAFF: mov      eax, dword ptr [ebp - 0x10]
0x48BB02: mov      edx, dword ptr [eax + 0x258]
0x48BB08: mov      eax, dword ptr [ebp - 0x10]
0x48BB0B: mov      eax, dword ptr [eax + 0x254]
0x48BB11: call     0x45351c
0x48BB16: mov      dword ptr [ebp - 0x14], eax
0x48BB19: cmp      dword ptr [ebp - 0x14], 0
0x48BB1D: je       0x48bb5c
0x48BB1F: lea      edx, [ebp - 0x24]
0x48BB22: mov      eax, dword ptr [ebp - 0x14]
0x48BB25: call     0x44d8ec
0x48BB2A: lea      eax, [ebp - 0x24]
0x48BB2D: push     eax
0x48BB2E: mov      eax, dword ptr [ebp - 0x14]
0x48BB31: push     eax
0x48BB32: push     1
0x48BB34: mov      eax, dword ptr [ebp - 0x10]
0x48BB37: mov      edx, dword ptr [eax + 0x34]
0x48BB3A: mov      eax, dword ptr [ebp - 0x10]
0x48BB3D: call     0x456fdc
0x48BB42: push     eax
0x48BB43: mov      eax, dword ptr [ebp - 0x10]
0x48BB46: mov      edx, dword ptr [eax + 0x30]
0x48BB49: mov      eax, dword ptr [ebp - 0x10]
0x48BB4C: call     0x456f94
0x48BB51: mov      edx, eax
0x48BB53: mov      eax, dword ptr [ebp - 0xc]
0x48BB56: pop      ecx
0x48BB57: call     0x44d6cc
0x48BB5C: mov      esp, ebp
0x48BB5E: pop      ebp
0x48BB5F: ret      
0x48BB60: push     ebp
0x48BB61: mov      ebp, esp
0x48BB63: add      esp, -0x24
0x48BB66: mov      dword ptr [ebp - 0xc], ecx
0x48BB69: mov      dword ptr [ebp - 8], edx
0x48BB6C: mov      dword ptr [ebp - 4], eax
0x48BB6F: mov      eax, dword ptr [ebp - 8]
0x48BB72: mov      edx, dword ptr [0x456468]
0x48BB78: call     0x40301c
0x48BB7D: test     al, al
0x48BB7F: je       0x48bc58
0x48BB85: mov      eax, dword ptr [ebp - 8]
0x48BB88: mov      dword ptr [ebp - 0x10], eax
0x48BB8B: mov      eax, dword ptr [ebp - 0x10]
0x48BB8E: cmp      byte ptr [eax + 0x274], 0
0x48BB95: jne      0x48bbfa
0x48BB97: mov      eax, dword ptr [ebp - 0x10]
0x48BB9A: mov      edx, dword ptr [eax + 0x258]
0x48BBA0: mov      eax, dword ptr [ebp - 0x10]
0x48BBA3: mov      eax, dword ptr [eax + 0x254]
0x48BBA9: call     0x45351c
0x48BBAE: mov      dword ptr [ebp - 0x14], eax
0x48BBB1: cmp      dword ptr [ebp - 0x14], 0
0x48BBB5: je       0x48bc58
0x48BBBB: lea      edx, [ebp - 0x24]
0x48BBBE: mov      eax, dword ptr [ebp - 0x14]
0x48BBC1: call     0x44d8ec
0x48BBC6: lea      eax, [ebp - 0x24]
0x48BBC9: push     eax
0x48BBCA: mov      eax, dword ptr [ebp - 0x14]
0x48BBCD: push     eax
0x48BBCE: push     1
0x48BBD0: mov      eax, dword ptr [ebp - 0x10]
0x48BBD3: mov      edx, dword ptr [eax + 0x34]
0x48BBD6: mov      eax, dword ptr [ebp - 0x10]
0x48BBD9: call     0x456fdc
0x48BBDE: push     eax
0x48BBDF: mov      eax, dword ptr [ebp - 0x10]
0x48BBE2: mov      edx, dword ptr [eax + 0x30]
0x48BBE5: mov      eax, dword ptr [ebp - 0x10]
0x48BBE8: call     0x456f94
0x48BBED: mov      edx, eax
0x48BBEF: mov      eax, dword ptr [ebp - 0xc]
0x48BBF2: pop      ecx
0x48BBF3: call     0x44d6cc
0x48BBF8: jmp      0x48bc58
0x48BBFA: mov      eax, dword ptr [ebp - 0x10]
0x48BBFD: mov      edx, dword ptr [eax + 0x258]
0x48BC03: inc      edx
0x48BC04: mov      eax, dword ptr [ebp - 0x10]
0x48BC07: mov      eax, dword ptr [eax + 0x254]
0x48BC0D: call     0x45351c
0x48BC12: mov      dword ptr [ebp - 0x14], eax
0x48BC15: cmp      dword ptr [ebp - 0x14], 0
0x48BC19: je       0x48bc58
0x48BC1B: lea      edx, [ebp - 0x24]
0x48BC1E: mov      eax, dword ptr [ebp - 0x14]
0x48BC21: call     0x44d8ec
0x48BC26: lea      eax, [ebp - 0x24]
0x48BC29: push     eax
0x48BC2A: mov      eax, dword ptr [ebp - 0x14]
0x48BC2D: push     eax
0x48BC2E: push     1
0x48BC30: mov      eax, dword ptr [ebp - 0x10]
0x48BC33: mov      edx, dword ptr [eax + 0x34]
0x48BC36: mov      eax, dword ptr [ebp - 0x10]
0x48BC39: call     0x456fdc
0x48BC3E: push     eax
0x48BC3F: mov      eax, dword ptr [ebp - 0x10]
0x48BC42: mov      edx, dword ptr [eax + 0x30]
0x48BC45: mov      eax, dword ptr [ebp - 0x10]
0x48BC48: call     0x456f94
0x48BC4D: mov      edx, eax
0x48BC4F: mov      eax, dword ptr [ebp - 0xc]
0x48BC52: pop      ecx
0x48BC53: call     0x44d6cc
0x48BC58: mov      esp, ebp
0x48BC5A: pop      ebp
0x48BC5B: ret      
0x48BC5C: push     ebp
0x48BC5D: mov      ebp, esp
0x48BC5F: add      esp, -0x24
0x48BC62: mov      dword ptr [ebp - 0xc], ecx
0x48BC65: mov      dword ptr [ebp - 8], edx
0x48BC68: mov      dword ptr [ebp - 4], eax
0x48BC6B: mov      eax, dword ptr [ebp - 8]
0x48BC6E: mov      edx, dword ptr [0x456468]
0x48BC74: call     0x40301c
0x48BC79: test     al, al
0x48BC7B: je       0x48bde5
0x48BC81: mov      eax, dword ptr [ebp - 8]
0x48BC84: mov      dword ptr [ebp - 0x10], eax
0x48BC87: mov      eax, dword ptr [ebp - 0x10]
0x48BC8A: cmp      byte ptr [eax + 0x274], 0
0x48BC91: jne      0x48bd44
0x48BC97: mov      eax, dword ptr [ebp - 4]
0x48BC9A: mov      eax, dword ptr [eax + 0x614]
0x48BCA0: and      eax, 0x80000001
0x48BCA5: jns      0x48bcac
0x48BCA7: dec      eax
0x48BCA8: or       eax, 0xfffffffe
0x48BCAB: inc      eax
0x48BCAC: test     eax, eax
0x48BCAE: jne      0x48bcde
0x48BCB0: mov      eax, dword ptr [ebp - 4]
0x48BCB3: mov      eax, dword ptr [eax + 0x518]
0x48BCB9: cmp      byte ptr [eax + 0x24c], 0
0x48BCC0: jne      0x48bcde
0x48BCC2: mov      eax, dword ptr [ebp - 0x10]
0x48BCC5: mov      edx, dword ptr [eax + 0x258]
0x48BCCB: mov      eax, dword ptr [ebp - 0x10]
0x48BCCE: mov      eax, dword ptr [eax + 0x254]
0x48BCD4: call     0x45351c
0x48BCD9: mov      dword ptr [ebp - 0x14], eax
0x48BCDC: jmp      0x48bcfb
0x48BCDE: mov      eax, dword ptr [ebp - 0x10]
0x48BCE1: mov      edx, dword ptr [eax + 0x258]
0x48BCE7: add      edx, 2
0x48BCEA: mov      eax, dword ptr [ebp - 0x10]
0x48BCED: mov      eax, dword ptr [eax + 0x254]
0x48BCF3: call     0x45351c
0x48BCF8: mov      dword ptr [ebp - 0x14], eax
0x48BCFB: cmp      dword ptr [ebp - 0x14], 0
0x48BCFF: je       0x48bda2
0x48BD05: lea      edx, [ebp - 0x24]
0x48BD08: mov      eax, dword ptr [ebp - 0x14]
0x48BD0B: call     0x44d8ec
0x48BD10: lea      eax, [ebp - 0x24]
0x48BD13: push     eax
0x48BD14: mov      eax, dword ptr [ebp - 0x14]
0x48BD17: push     eax
0x48BD18: push     1
0x48BD1A: mov      eax, dword ptr [ebp - 0x10]
0x48BD1D: mov      edx, dword ptr [eax + 0x34]
0x48BD20: mov      eax, dword ptr [ebp - 0x10]
0x48BD23: call     0x456fdc
0x48BD28: push     eax
0x48BD29: mov      eax, dword ptr [ebp - 0x10]
0x48BD2C: mov      edx, dword ptr [eax + 0x30]
0x48BD2F: mov      eax, dword ptr [ebp - 0x10]
0x48BD32: call     0x456f94
0x48BD37: mov      edx, eax
0x48BD39: mov      eax, dword ptr [ebp - 0xc]
0x48BD3C: pop      ecx
0x48BD3D: call     0x44d6cc
0x48BD42: jmp      0x48bda2
0x48BD44: mov      eax, dword ptr [ebp - 0x10]
0x48BD47: mov      edx, dword ptr [eax + 0x258]
0x48BD4D: inc      edx
0x48BD4E: mov      eax, dword ptr [ebp - 0x10]
0x48BD51: mov      eax, dword ptr [eax + 0x254]
0x48BD57: call     0x45351c
0x48BD5C: mov      dword ptr [ebp - 0x14], eax
0x48BD5F: cmp      dword ptr [ebp - 0x14], 0
0x48BD63: je       0x48bda2
0x48BD65: lea      edx, [ebp - 0x24]
0x48BD68: mov      eax, dword ptr [ebp - 0x14]
0x48BD6B: call     0x44d8ec
0x48BD70: lea      eax, [ebp - 0x24]
0x48BD73: push     eax
0x48BD74: mov      eax, dword ptr [ebp - 0x14]
0x48BD77: push     eax
0x48BD78: push     1
0x48BD7A: mov      eax, dword ptr [ebp - 0x10]
0x48BD7D: mov      edx, dword ptr [eax + 0x34]
0x48BD80: mov      eax, dword ptr [ebp - 0x10]
0x48BD83: call     0x456fdc
0x48BD88: push     eax
0x48BD89: mov      eax, dword ptr [ebp - 0x10]
0x48BD8C: mov      edx, dword ptr [eax + 0x30]
0x48BD8F: mov      eax, dword ptr [ebp - 0x10]
0x48BD92: call     0x456f94
0x48BD97: mov      edx, eax
0x48BD99: mov      eax, dword ptr [ebp - 0xc]
0x48BD9C: pop      ecx
0x48BD9D: call     0x44d6cc
0x48BDA2: call     0x406154
0x48BDA7: mov      edx, dword ptr [ebp - 4]
0x48BDAA: sub      eax, dword ptr [edx + 0x610]
0x48BDB0: cmp      eax, 0x1f4
0x48BDB5: jb       0x48bde5
0x48BDB7: call     0x406154
0x48BDBC: mov      edx, dword ptr [ebp - 4]
0x48BDBF: mov      dword ptr [edx + 0x610], eax
0x48BDC5: mov      eax, dword ptr [ebp - 4]
0x48BDC8: inc      dword ptr [eax + 0x614]
0x48BDCE: mov      eax, dword ptr [ebp - 4]
0x48BDD1: cmp      dword ptr [eax + 0x614], 0xa
0x48BDD8: jl       0x48bde5
0x48BDDA: mov      eax, dword ptr [ebp - 4]
0x48BDDD: xor      edx, edx
0x48BDDF: mov      dword ptr [eax + 0x614], edx
0x48BDE5: mov      esp, ebp
0x48BDE7: pop      ebp
0x48BDE8: ret      
0x48BDE9: lea      eax, [eax]
0x48BDEC: push     ebp
0x48BDED: mov      ebp, esp
0x48BDEF: add      esp, -0xc
0x48BDF2: mov      dword ptr [ebp - 0xc], ecx
0x48BDF5: mov      dword ptr [ebp - 8], edx
0x48BDF8: mov      dword ptr [ebp - 4], eax
0x48BDFB: mov      eax, dword ptr [ebp - 8]
0x48BDFE: mov      edx, dword ptr [ebp - 4]
0x48BE01: cmp      eax, dword ptr [edx + 0x2d4]
0x48BE07: jne      0x48be1c
0x48BE09: mov      eax, dword ptr [ebp - 4]
0x48BE0C: xor      edx, edx
0x48BE0E: mov      dword ptr [eax + 0x618], edx
0x48BE14: mov      eax, dword ptr [ebp - 4]
0x48BE17: call     0x48647c
0x48BE1C: mov      eax, dword ptr [ebp - 8]
0x48BE1F: mov      edx, dword ptr [ebp - 4]
0x48BE22: cmp      eax, dword ptr [edx + 0x2d8]
0x48BE28: jne      0x48be32
0x48BE2A: mov      eax, dword ptr [ebp - 4]
0x48BE2D: call     0x4864fc
0x48BE32: mov      eax, dword ptr [ebp - 8]
0x48BE35: mov      edx, dword ptr [ebp - 4]
0x48BE38: cmp      eax, dword ptr [edx + 0x2dc]
0x48BE3E: jne      0x48be55
0x48BE40: mov      eax, dword ptr [ebp - 4]
0x48BE43: mov      dword ptr [eax + 0x618], 3
0x48BE4D: mov      eax, dword ptr [ebp - 4]
0x48BE50: call     0x48647c
0x48BE55: mov      eax, dword ptr [ebp - 8]
0x48BE58: mov      edx, dword ptr [ebp - 4]
0x48BE5B: cmp      eax, dword ptr [edx + 0x2e0]
0x48BE61: jne      0x48be68
0x48BE63: call     0x45f008
0x48BE68: mov      esp, ebp
0x48BE6A: pop      ebp
0x48BE6B: ret      4
0x48BE6E: mov      eax, eax
0x48BE70: push     ebp
0x48BE71: mov      ebp, esp
0x48BE73: add      esp, -8
0x48BE76: mov      dword ptr [ebp - 8], edx
0x48BE79: mov      dword ptr [ebp - 4], eax
0x48BE7C: pop      ecx
0x48BE7D: pop      ecx
0x48BE7E: pop      ebp
0x48BE7F: ret      
0x48BE80: push     ebp
0x48BE81: mov      ebp, esp
0x48BE83: add      esp, -0x2c
0x48BE86: push     ebx
0x48BE87: xor      ebx, ebx
0x48BE89: mov      dword ptr [ebp - 0x2c], ebx
0x48BE8C: mov      dword ptr [ebp - 0xc], ecx
0x48BE8F: mov      dword ptr [ebp - 8], edx
0x48BE92: mov      dword ptr [ebp - 4], eax
0x48BE95: xor      eax, eax
0x48BE97: push     ebp
0x48BE98: push     0x48bfc6
0x48BE9D: push     dword ptr fs:[eax]
0x48BEA0: mov      dword ptr fs:[eax], esp
0x48BEA3: mov      eax, dword ptr [ebp - 8]
0x48BEA6: mov      edx, dword ptr [0x456468]
0x48BEAC: call     0x403034
0x48BEB1: mov      dword ptr [ebp - 0x18], eax
0x48BEB4: mov      eax, dword ptr [ebp - 0x18]
0x48BEB7: mov      eax, dword ptr [eax + 0xc]
0x48BEBA: mov      dword ptr [ebp - 0x10], eax
0x48BEBD: mov      eax, dword ptr [ebp - 0x10]
0x48BEC0: sub      eax, 6
0x48BEC3: jae      0x48bf72
0x48BEC9: imul     eax, dword ptr [ebp - 0x10], 0xd
0x48BECD: mov      edx, dword ptr [0x4a4264] ; *0x4A4264->0x4F80D0
0x48BED3: cmp      byte ptr [edx + eax*4], 0
0x48BED7: je       0x48bf72
0x48BEDD: imul     eax, dword ptr [ebp - 0x10], 0xd
0x48BEE1: mov      edx, dword ptr [0x4a4264] ; *0x4A4264->0x4F80D0
0x48BEE7: movzx    edx, word ptr [edx + eax*4 + 0x16]
0x48BEEC: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48BEF1: mov      eax, dword ptr [eax]
0x48BEF3: mov      eax, dword ptr [eax + 0x324]
0x48BEF9: call     0x45351c
0x48BEFE: mov      dword ptr [ebp - 0x14], eax
0x48BF01: cmp      dword ptr [ebp - 0x14], 0
0x48BF05: je       0x48bf72
0x48BF07: lea      edx, [ebp - 0x28]
0x48BF0A: mov      eax, dword ptr [ebp - 0x14]
0x48BF0D: call     0x44d8ec
0x48BF12: lea      eax, [ebp - 0x28]
0x48BF15: push     eax
0x48BF16: mov      eax, dword ptr [ebp - 0x14]
0x48BF19: push     eax
0x48BF1A: push     1
0x48BF1C: mov      eax, dword ptr [ebp - 0x14]
0x48BF1F: call     0x44d944
0x48BF24: mov      edx, dword ptr [ebp - 0x18]
0x48BF27: mov      edx, dword ptr [edx + 0x3c]
0x48BF2A: sub      edx, eax
0x48BF2C: sar      edx, 1
0x48BF2E: jns      0x48bf33
0x48BF30: adc      edx, 0
0x48BF33: mov      eax, dword ptr [ebp - 0x18]
0x48BF36: add      edx, dword ptr [eax + 0x34]
0x48BF39: mov      eax, dword ptr [ebp - 0x18]
0x48BF3C: call     0x456fdc
0x48BF41: push     eax
0x48BF42: mov      eax, dword ptr [ebp - 0x14]
0x48BF45: call     0x44dc60
0x48BF4A: mov      edx, dword ptr [ebp - 0x18]
0x48BF4D: mov      edx, dword ptr [edx + 0x38]
0x48BF50: sub      edx, eax
0x48BF52: sar      edx, 1
0x48BF54: jns      0x48bf59
0x48BF56: adc      edx, 0
0x48BF59: mov      eax, dword ptr [ebp - 0x18]
0x48BF5C: add      edx, dword ptr [eax + 0x30]
0x48BF5F: mov      eax, dword ptr [ebp - 0x18]
0x48BF62: call     0x456f94
0x48BF67: mov      edx, eax
0x48BF69: mov      eax, dword ptr [ebp - 0xc]
0x48BF6C: pop      ecx
0x48BF6D: call     0x44d6cc
0x48BF72: lea      edx, [ebp - 0x2c]
0x48BF75: mov      eax, dword ptr [ebp - 0x10]
0x48BF78: inc      eax
0x48BF79: call     0x407d64
0x48BF7E: mov      eax, dword ptr [ebp - 0x2c]
0x48BF81: push     eax
0x48BF82: mov      eax, dword ptr [ebp - 0x18]
0x48BF85: mov      edx, dword ptr [eax + 0x34]
0x48BF88: add      edx, 0x13
0x48BF8B: mov      eax, dword ptr [ebp - 0x18]
0x48BF8E: call     0x456fdc
0x48BF93: push     eax
0x48BF94: mov      eax, dword ptr [ebp - 0x18]
0x48BF97: mov      edx, dword ptr [eax + 0x30]
0x48BF9A: add      edx, 0xd
0x48BF9D: mov      eax, dword ptr [ebp - 0x18]
0x48BFA0: call     0x456f94
0x48BFA5: mov      edx, eax
0x48BFA7: mov      eax, dword ptr [ebp - 0xc]
0x48BFAA: pop      ecx
0x48BFAB: call     0x45ec70
0x48BFB0: xor      eax, eax
0x48BFB2: pop      edx
0x48BFB3: pop      ecx
0x48BFB4: pop      ecx
0x48BFB5: mov      dword ptr fs:[eax], edx
0x48BFB8: push     0x48bfcd
0x48BFBD: lea      eax, [ebp - 0x2c]
0x48BFC0: call     0x403a58
0x48BFC5: ret      
0x48BFC6: jmp      0x403518
0x48BFCB: jmp      0x48bfbd
0x48BFCD: pop      ebx
0x48BFCE: mov      esp, ebp
0x48BFD0: pop      ebp
0x48BFD1: ret      
0x48BFD2: mov      eax, eax
0x48BFD4: push     ebp
0x48BFD5: mov      ebp, esp
0x48BFD7: add      esp, -0x10
0x48BFDA: push     esi
0x48BFDB: push     edi
0x48BFDC: mov      byte ptr [ebp - 0xd], cl
0x48BFDF: mov      dword ptr [ebp - 8], edx
0x48BFE2: mov      dword ptr [ebp - 4], eax
0x48BFE5: mov      eax, dword ptr [ebp - 8]
0x48BFE8: mov      eax, dword ptr [eax + 0xc]
0x48BFEB: mov      dword ptr [ebp - 0xc], eax
0x48BFEE: mov      eax, dword ptr [ebp - 0xc]
0x48BFF1: sub      eax, 6
0x48BFF4: jae      0x48c022
0x48BFF6: imul     eax, dword ptr [ebp - 0xc], 0xd
0x48BFFA: mov      edx, dword ptr [0x4a4264] ; *0x4A4264->0x4F80D0
0x48C000: cmp      byte ptr [edx + eax*4], 0
0x48C004: je       0x48c022
0x48C006: imul     eax, dword ptr [ebp - 0xc], 0xd
0x48C00A: mov      edx, dword ptr [0x4a4264] ; *0x4A4264->0x4F80D0
0x48C010: mov      ecx, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C016: lea      esi, [edx + eax*4]
0x48C019: mov      edi, ecx
0x48C01B: mov      ecx, 0xd
0x48C020: rep movsd dword ptr es:[edi], dword ptr [esi]
0x48C022: pop      edi
0x48C023: pop      esi
0x48C024: mov      esp, ebp
0x48C026: pop      ebp
0x48C027: ret      8
0x48C02A: mov      eax, eax
0x48C02C: push     ebp
0x48C02D: mov      ebp, esp
0x48C02F: add      esp, -0x44
0x48C032: push     esi
0x48C033: push     edi
0x48C034: mov      dword ptr [ebp - 0x10], ecx
0x48C037: mov      dword ptr [ebp - 8], edx
0x48C03A: mov      dword ptr [ebp - 4], eax
0x48C03D: mov      eax, dword ptr [ebp - 8]
0x48C040: mov      eax, dword ptr [eax + 0xc]
0x48C043: mov      dword ptr [ebp - 0xc], eax
0x48C046: mov      eax, dword ptr [ebp - 0xc]
0x48C049: sub      eax, 6
0x48C04C: jae      0x48c17f
0x48C052: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x48C057: cmp      byte ptr [eax], 0
0x48C05A: jne      0x48c0c4
0x48C05C: imul     eax, dword ptr [ebp - 0xc], 0xd
0x48C060: mov      edx, dword ptr [0x4a4264] ; *0x4A4264->0x4F80D0
0x48C066: cmp      byte ptr [edx + eax*4], 0
0x48C06A: je       0x48c17f
0x48C070: imul     eax, dword ptr [ebp - 0xc], 0xd
0x48C074: mov      edx, dword ptr [0x4a4264] ; *0x4A4264->0x4F80D0
0x48C07A: lea      eax, [edx + eax*4]
0x48C07D: call     0x495940
0x48C082: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x48C087: mov      byte ptr [eax], 1
0x48C08A: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48C08F: mov      edx, dword ptr [ebp - 0xc]
0x48C092: mov      dword ptr [eax], edx
0x48C094: imul     eax, dword ptr [ebp - 0xc], 0xd
0x48C098: mov      edx, dword ptr [0x4a4264] ; *0x4A4264->0x4F80D0
0x48C09E: mov      ecx, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48C0A4: lea      esi, [edx + eax*4]
0x48C0A7: lea      edi, [ecx + 4]
0x48C0AA: mov      ecx, 0xd
0x48C0AF: rep movsd dword ptr es:[edi], dword ptr [esi]
0x48C0B1: imul     eax, dword ptr [ebp - 0xc], 0xd
0x48C0B5: mov      edx, dword ptr [0x4a4264] ; *0x4A4264->0x4F80D0
0x48C0BB: mov      byte ptr [edx + eax*4], 0
0x48C0BF: jmp      0x48c17f
0x48C0C4: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48C0C9: cmp      dword ptr [eax], -0x61
0x48C0CC: je       0x48c17f
0x48C0D2: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48C0D7: cmp      dword ptr [eax], -0x62
0x48C0DA: je       0x48c17f
0x48C0E0: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48C0E5: cmp      byte ptr [eax + 0x13], 3
0x48C0E9: ja       0x48c17f
0x48C0EF: imul     eax, dword ptr [ebp - 0xc], 0xd
0x48C0F3: mov      edx, dword ptr [0x4a4264] ; *0x4A4264->0x4F80D0
0x48C0F9: cmp      byte ptr [edx + eax*4], 0
0x48C0FD: je       0x48c151
0x48C0FF: imul     eax, dword ptr [ebp - 0xc], 0xd
0x48C103: mov      edx, dword ptr [0x4a4264] ; *0x4A4264->0x4F80D0
0x48C109: lea      esi, [edx + eax*4]
0x48C10C: lea      edi, [ebp - 0x44]
0x48C10F: mov      ecx, 0xd
0x48C114: rep movsd dword ptr es:[edi], dword ptr [esi]
0x48C116: imul     eax, dword ptr [ebp - 0xc], 0xd
0x48C11A: mov      edx, dword ptr [0x4a4264] ; *0x4A4264->0x4F80D0
0x48C120: mov      ecx, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48C126: lea      edi, [edx + eax*4]
0x48C129: lea      esi, [ecx + 4]
0x48C12C: mov      ecx, 0xd
0x48C131: rep movsd dword ptr es:[edi], dword ptr [esi]
0x48C133: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48C138: mov      edx, dword ptr [ebp - 0xc]
0x48C13B: mov      dword ptr [eax], edx
0x48C13D: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48C142: lea      edi, [eax + 4]
0x48C145: lea      esi, [ebp - 0x44]
0x48C148: mov      ecx, 0xd
0x48C14D: rep movsd dword ptr es:[edi], dword ptr [esi]
0x48C14F: jmp      0x48c17f
0x48C151: imul     eax, dword ptr [ebp - 0xc], 0xd
0x48C155: mov      edx, dword ptr [0x4a4264] ; *0x4A4264->0x4F80D0
0x48C15B: mov      ecx, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48C161: lea      edi, [edx + eax*4]
0x48C164: lea      esi, [ecx + 4]
0x48C167: mov      ecx, 0xd
0x48C16C: rep movsd dword ptr es:[edi], dword ptr [esi]
0x48C16E: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48C173: mov      byte ptr [eax + 4], 0
0x48C177: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x48C17C: mov      byte ptr [eax], 0
0x48C17F: pop      edi
0x48C180: pop      esi
0x48C181: mov      esp, ebp
0x48C183: pop      ebp
0x48C184: ret      4
0x48C187: nop      
0x48C188: push     ebp
0x48C189: mov      ebp, esp
0x48C18B: add      esp, -0xc
0x48C18E: mov      dword ptr [ebp - 8], edx
0x48C191: mov      dword ptr [ebp - 4], eax
0x48C194: mov      eax, dword ptr [ebp - 8]
0x48C197: mov      eax, dword ptr [eax + 0xc]
0x48C19A: mov      dword ptr [ebp - 0xc], eax
0x48C19D: mov      eax, dword ptr [ebp - 0xc]
0x48C1A0: sub      eax, 6
0x48C1A3: jae      0x48c223
0x48C1A5: imul     eax, dword ptr [ebp - 0xc], 0xd
0x48C1A9: mov      edx, dword ptr [0x4a4264] ; *0x4A4264->0x4F80D0
0x48C1AF: cmp      byte ptr [edx + eax*4], 0
0x48C1B3: je       0x48c1e8
0x48C1B5: imul     eax, dword ptr [ebp - 0xc], 0xd
0x48C1B9: mov      edx, dword ptr [0x4a4264] ; *0x4A4264->0x4F80D0
0x48C1BF: cmp      byte ptr [edx + eax*4 + 0xf], 4
0x48C1C4: jbe      0x48c1d7
0x48C1C6: imul     eax, dword ptr [ebp - 0xc], 0xd
0x48C1CA: mov      edx, dword ptr [0x4a4264] ; *0x4A4264->0x4F80D0
0x48C1D0: cmp      byte ptr [edx + eax*4 + 0xf], 0x1f
0x48C1D5: jne      0x48c223
0x48C1D7: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48C1DC: mov      eax, dword ptr [eax]
0x48C1DE: mov      edx, dword ptr [ebp - 0xc]
0x48C1E1: call     0x462b1c
0x48C1E6: jmp      0x48c223
0x48C1E8: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x48C1ED: cmp      byte ptr [eax], 0
0x48C1F0: je       0x48c209
0x48C1F2: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48C1F7: mov      eax, dword ptr [eax]
0x48C1F9: cmp      eax, dword ptr [ebp - 0xc]
0x48C1FC: jne      0x48c209
0x48C1FE: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48C203: cmp      byte ptr [eax + 0x13], 4
0x48C207: jbe      0x48c214
0x48C209: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48C20E: cmp      byte ptr [eax + 0x13], 0x1f
0x48C212: jne      0x48c223
0x48C214: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48C219: mov      eax, dword ptr [eax]
0x48C21B: or       edx, 0xffffffff
0x48C21E: call     0x462b1c
0x48C223: mov      esp, ebp
0x48C225: pop      ebp
0x48C226: ret      
0x48C227: nop      
0x48C228: push     ebp
0x48C229: mov      ebp, esp
0x48C22B: add      esp, -0x14
0x48C22E: push     ebx
0x48C22F: xor      ebx, ebx
0x48C231: mov      dword ptr [ebp - 0x10], ebx
0x48C234: mov      dword ptr [ebp - 0x14], ebx
0x48C237: mov      dword ptr [ebp - 0xc], ecx
0x48C23A: mov      dword ptr [ebp - 8], edx
0x48C23D: mov      dword ptr [ebp - 4], eax
0x48C240: xor      eax, eax
0x48C242: push     ebp
0x48C243: push     0x48c2da
0x48C248: push     dword ptr fs:[eax]
0x48C24B: mov      dword ptr fs:[eax], esp
0x48C24E: mov      eax, dword ptr [ebp + 8]
0x48C251: mov      eax, dword ptr [eax - 4]
0x48C254: cmp      byte ptr [eax + 0x776], 0
0x48C25B: jne      0x48c2a7
0x48C25D: fild     dword ptr [ebp - 4]
0x48C260: fdiv     dword ptr [0x48c2e8]
0x48C266: call     0x4029a0
0x48C26B: push     edx
0x48C26C: push     eax
0x48C26D: lea      eax, [ebp - 0x10]
0x48C270: call     0x407d94
0x48C275: push     dword ptr [ebp - 0x10]
0x48C278: push     0x48c2f4 ; "/"
0x48C27D: fild     dword ptr [ebp - 8]
0x48C280: fdiv     dword ptr [0x48c2e8]
0x48C286: call     0x4029a0
0x48C28B: push     edx
0x48C28C: push     eax
0x48C28D: lea      eax, [ebp - 0x14]
0x48C290: call     0x407d94
0x48C295: push     dword ptr [ebp - 0x14]
0x48C298: mov      eax, dword ptr [ebp - 0xc]
0x48C29B: mov      edx, 3
0x48C2A0: call     0x403d94
0x48C2A5: jmp      0x48c2bf
0x48C2A7: fild     dword ptr [ebp - 4]
0x48C2AA: fdiv     dword ptr [0x48c2e8]
0x48C2B0: call     0x4029a0
0x48C2B5: push     edx
0x48C2B6: push     eax
0x48C2B7: mov      eax, dword ptr [ebp - 0xc]
0x48C2BA: call     0x407d94
0x48C2BF: xor      eax, eax
0x48C2C1: pop      edx
0x48C2C2: pop      ecx
0x48C2C3: pop      ecx
0x48C2C4: mov      dword ptr fs:[eax], edx
0x48C2C7: push     0x48c2e1
0x48C2CC: lea      eax, [ebp - 0x14]
0x48C2CF: mov      edx, 2
0x48C2D4: call     0x403a7c
0x48C2D9: ret      
0x48C2DA: jmp      0x403518
0x48C2DF: jmp      0x48c2cc
0x48C2E1: pop      ebx
0x48C2E2: mov      esp, ebp
0x48C2E4: pop      ebp
0x48C2E5: ret      
0x48C2E6: add      byte ptr [eax], al
0x48C2E8: add      byte ptr [eax], al
0x48C2EA: jp       0x48c330
0x48C2F4: [STR len=1] "/"
0x48C2F8: push     ebp
0x48C2F9: mov      ebp, esp
0x48C2FB: add      esp, -0x14
0x48C2FE: push     ebx
0x48C2FF: xor      ebx, ebx
0x48C301: mov      dword ptr [ebp - 0x10], ebx
0x48C304: mov      dword ptr [ebp - 0x14], ebx
0x48C307: mov      dword ptr [ebp - 0xc], ecx
0x48C30A: mov      dword ptr [ebp - 8], edx
0x48C30D: mov      dword ptr [ebp - 4], eax
0x48C310: xor      eax, eax
0x48C312: push     ebp
0x48C313: push     0x48c3aa
0x48C318: push     dword ptr fs:[eax]
0x48C31B: mov      dword ptr fs:[eax], esp
0x48C31E: mov      eax, dword ptr [ebp + 8]
0x48C321: mov      eax, dword ptr [eax - 4]
0x48C324: cmp      byte ptr [eax + 0x776], 0
0x48C32B: jne      0x48c377
0x48C32D: fild     dword ptr [ebp - 4]
0x48C330: fdiv     dword ptr [0x48c3b8]
0x48C336: call     0x4029a0
0x48C33B: push     edx
0x48C33C: push     eax
0x48C33D: lea      eax, [ebp - 0x10]
0x48C340: call     0x407d94
0x48C345: push     dword ptr [ebp - 0x10]
0x48C348: push     0x48c3c4 ; "/"
0x48C34D: fild     dword ptr [ebp - 8]
0x48C350: fdiv     dword ptr [0x48c3b8]
0x48C356: call     0x4029a0
0x48C35B: push     edx
0x48C35C: push     eax
0x48C35D: lea      eax, [ebp - 0x14]
0x48C360: call     0x407d94
0x48C365: push     dword ptr [ebp - 0x14]
0x48C368: mov      eax, dword ptr [ebp - 0xc]
0x48C36B: mov      edx, 3
0x48C370: call     0x403d94
0x48C375: jmp      0x48c38f
0x48C377: fild     dword ptr [ebp - 4]
0x48C37A: fdiv     dword ptr [0x48c3b8]
0x48C380: call     0x4029a0
0x48C385: push     edx
0x48C386: push     eax
0x48C387: mov      eax, dword ptr [ebp - 0xc]
0x48C38A: call     0x407d94
0x48C38F: xor      eax, eax
0x48C391: pop      edx
0x48C392: pop      ecx
0x48C393: pop      ecx
0x48C394: mov      dword ptr fs:[eax], edx
0x48C397: push     0x48c3b1
0x48C39C: lea      eax, [ebp - 0x14]
0x48C39F: mov      edx, 2
0x48C3A4: call     0x403a7c
0x48C3A9: ret      
0x48C3AA: jmp      0x403518
0x48C3AF: jmp      0x48c39c
0x48C3B1: pop      ebx
0x48C3B2: mov      esp, ebp
0x48C3B4: pop      ebp
0x48C3B5: ret      
0x48C3B6: add      byte ptr [eax], al
0x48C3B8: add      byte ptr [eax], al
0x48C3BA: enter    -0xbe, -1
0x48C3BF: inc      dword ptr [ecx]
0x48C3C1: add      byte ptr [eax], al
0x48C3C3: add      byte ptr [edi], ch
0x48C3C5: add      byte ptr [eax], al
0x48C3C7: add      byte ptr [ebp - 0x75], dl
0x48C3CA: in       al, dx
0x48C3CB: add      esp, -0x28
0x48C3CE: push     ebx
0x48C3CF: xor      ebx, ebx
0x48C3D1: mov      dword ptr [ebp - 0x20], ebx
0x48C3D4: mov      dword ptr [ebp - 0x24], ebx
0x48C3D7: mov      dword ptr [ebp - 0xc], ecx
0x48C3DA: mov      dword ptr [ebp - 8], edx
0x48C3DD: mov      dword ptr [ebp - 4], eax
0x48C3E0: xor      eax, eax
0x48C3E2: push     ebp
0x48C3E3: push     0x48d92f
0x48C3E8: push     dword ptr fs:[eax]
0x48C3EB: mov      dword ptr fs:[eax], esp
0x48C3EE: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48C3F3: cmp      dword ptr [eax], 0
0x48C3F6: je       0x48d914
0x48C3FC: mov      eax, dword ptr [ebp - 8]
0x48C3FF: call     0x403a58
0x48C404: mov      eax, dword ptr [ebp - 0xc]
0x48C407: call     0x403a58
0x48C40C: mov      eax, dword ptr [ebp + 0x14]
0x48C40F: call     0x403a58
0x48C414: mov      eax, dword ptr [ebp + 0x10]
0x48C417: call     0x403a58
0x48C41C: mov      eax, dword ptr [ebp + 0xc]
0x48C41F: mov      byte ptr [eax], 1
0x48C422: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C427: cmp      byte ptr [eax], 0
0x48C42A: je       0x48d914
0x48C430: mov      edx, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C436: lea      eax, [ebp - 0x1c]
0x48C439: call     0x402a14
0x48C43E: mov      edx, 0x48d940 ; " "
0x48C443: lea      eax, [ebp - 0x1c]
0x48C446: mov      cl, 0xf
0x48C448: call     0x4029e4
0x48C44D: lea      edx, [ebp - 0x1c]
0x48C450: mov      eax, dword ptr [ebp - 8]
0x48C453: call     0x403c78
0x48C458: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C45D: movzx    eax, byte ptr [eax + 0xf]
0x48C461: cmp      eax, 0x2b
0x48C464: ja       0x48d8e9
0x48C46A: mov      al, byte ptr [eax + 0x48c477]
0x48C470: jmp      dword ptr [eax*4 + 0x48c4a3]
0x48C477: add      dword ptr [edx], eax
0x48C479: add      al, byte ptr [edx]
0x48C47B: add      eax, dword ptr [esp + eax]
0x48C47E: add      byte ptr [eax], al
0x48C480: add      byte ptr [5], al
0x48C486: push     es
0x48C487: add      byte ptr [eax], al
0x48C489: add      byte ptr [esi], al
0x48C48B: push     es
0x48C48C: push     es
0x48C48D: push     es
0x48C48E: push     es
0x48C48F: push     es
0x48C490: pop      es
0x48C491: push     es
0x48C492: add      byte ptr [eax], al
0x48C494: add      byte ptr [eax], cl
0x48C496: add      byte ptr [eax], al
0x48C498: add      byte ptr [eax], al
0x48C49A: add      byte ptr [eax], al
0x48C49C: add      byte ptr [eax], al
0x48C49E: add      byte ptr [ecx], cl
0x48C4A0: add      byte ptr [edx], cl
0x48C4A2: or       ebp, ecx
0x48C4A4: fmul     dword ptr [eax]
0x48C4A7: rol      esp, cl
0x48C4A9: dec      eax
0x48C4AA: add      byte ptr [esi - 0x3b], dh
0x48C4AD: dec      eax
0x48C4AE: add      byte ptr [esi + 0x210048c5], ah
0x48C4B5: dec      eax
0x48C4B6: add      byte ptr [ecx - 0x34], ah
0x48C4B9: dec      eax
0x48C4BA: add      cl, al
0x48C4BC: iretd    
0x48C4BD: dec      eax
0x48C4BE: add      byte ptr [edi + edx*8 + 0x48], dl
0x48C4C2: add      byte ptr [ecx + 0x60048d7], dh
0x48C4C8: fmul     dword ptr [eax]
0x48C4CB: pop      ebx
0x48C4CC: fmul     dword ptr [eax]
0x48C4CF: nop      
0x48C4D0: fmul     dword ptr [eax]
0x48C4D3: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C4D8: cmp      word ptr [eax + 0x1a], 0
0x48C4DD: jbe      0x48c50a
0x48C4DF: push     0x48d94c ; "+"
0x48C4E4: lea      edx, [ebp - 0x20]
0x48C4E7: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C4EC: movzx    eax, word ptr [eax + 0x1a]
0x48C4F0: call     0x407d64
0x48C4F5: push     dword ptr [ebp - 0x20]
0x48C4F8: push     0x48d958 ; "HP "
0x48C4FD: mov      eax, dword ptr [ebp - 0xc]
0x48C500: mov      edx, 3
0x48C505: call     0x403d94
0x48C50A: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C50F: cmp      word ptr [eax + 0x1c], 0
0x48C514: jbe      0x48c546
0x48C516: mov      eax, dword ptr [ebp - 0xc]
0x48C519: push     dword ptr [eax]
0x48C51B: push     0x48d94c ; "+"
0x48C520: lea      edx, [ebp - 0x20]
0x48C523: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C528: movzx    eax, word ptr [eax + 0x1c]
0x48C52C: call     0x407d64
0x48C531: push     dword ptr [ebp - 0x20]
0x48C534: push     0x48d964 ; "MP"
0x48C539: mov      eax, dword ptr [ebp - 0xc]
0x48C53C: mov      edx, 4
0x48C541: call     0x403d94
0x48C546: mov      eax, dword ptr [ebp - 0xc]
0x48C549: push     dword ptr [eax]
0x48C54B: push     0x48d970 ; " 重量"
0x48C550: lea      edx, [ebp - 0x20]
0x48C553: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C558: movzx    eax, byte ptr [eax + 0x11]
0x48C55C: call     0x407d64
0x48C561: push     dword ptr [ebp - 0x20]
0x48C564: mov      eax, dword ptr [ebp - 0xc]
0x48C567: mov      edx, 3
0x48C56C: call     0x403d94
0x48C571: jmp      0x48d914
0x48C576: mov      eax, dword ptr [ebp - 0xc]
0x48C579: push     dword ptr [eax]
0x48C57B: push     0x48d980 ; "重量 "
0x48C580: lea      edx, [ebp - 0x20]
0x48C583: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C588: movzx    eax, byte ptr [eax + 0x11]
0x48C58C: call     0x407d64
0x48C591: push     dword ptr [ebp - 0x20]
0x48C594: mov      eax, dword ptr [ebp - 0xc]
0x48C597: mov      edx, 3
0x48C59C: call     0x403d94
0x48C5A1: jmp      0x48d914
0x48C5A6: mov      eax, dword ptr [ebp - 0xc]
0x48C5A9: push     dword ptr [eax]
0x48C5AB: push     0x48d980 ; "重量 "
0x48C5B0: lea      edx, [ebp - 0x20]
0x48C5B3: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C5B8: movzx    eax, byte ptr [eax + 0x11]
0x48C5BC: call     0x407d64
0x48C5C1: push     dword ptr [ebp - 0x20]
0x48C5C4: mov      eax, dword ptr [ebp - 0xc]
0x48C5C7: mov      edx, 3
0x48C5CC: call     0x403d94
0x48C5D1: mov      eax, dword ptr [ebp + 0xc]
0x48C5D4: mov      byte ptr [eax], 0
0x48C5D7: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C5DC: mov      al, byte ptr [eax + 0x10]
0x48C5DF: sub      al, 1
0x48C5E1: jb       0x48c5f2
0x48C5E3: je       0x48c657
0x48C5E5: dec      al
0x48C5E7: je       0x48c6bc
0x48C5ED: jmp      0x48d914
0x48C5F2: mov      eax, dword ptr [ebp + 0x14]
0x48C5F5: mov      edx, 0x48d990 ; "武术秘技 "
0x48C5FA: call     0x403aac
0x48C5FF: lea      edx, [ebp - 0x20]
0x48C602: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C607: movzx    eax, word ptr [eax + 0x18]
0x48C60B: call     0x407d64
0x48C610: mov      ecx, dword ptr [ebp - 0x20]
0x48C613: mov      eax, dword ptr [ebp + 0x10]
0x48C616: mov      edx, 0x48d9a4 ; "需要等级  "
0x48C61B: call     0x403d20
0x48C620: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48C625: mov      eax, dword ptr [eax]
0x48C627: cmp      byte ptr [eax + 0x12], 0
0x48C62B: jne      0x48d914
0x48C631: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48C636: mov      eax, dword ptr [eax]
0x48C638: movzx    eax, byte ptr [eax + 0x30]
0x48C63C: mov      edx, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C642: cmp      ax, word ptr [edx + 0x18]
0x48C646: jb       0x48d914
0x48C64C: mov      eax, dword ptr [ebp + 0xc]
0x48C64F: mov      byte ptr [eax], 1
0x48C652: jmp      0x48d914
0x48C657: mov      eax, dword ptr [ebp + 0x14]
0x48C65A: mov      edx, 0x48d9b8 ; "魔法书"
0x48C65F: call     0x403aac
0x48C664: lea      edx, [ebp - 0x20]
0x48C667: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C66C: movzx    eax, word ptr [eax + 0x18]
0x48C670: call     0x407d64
0x48C675: mov      ecx, dword ptr [ebp - 0x20]
0x48C678: mov      eax, dword ptr [ebp + 0x10]
0x48C67B: mov      edx, 0x48d9a4 ; "需要等级  "
0x48C680: call     0x403d20
0x48C685: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48C68A: mov      eax, dword ptr [eax]
0x48C68C: cmp      byte ptr [eax + 0x12], 1
0x48C690: jne      0x48d914
0x48C696: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48C69B: mov      eax, dword ptr [eax]
0x48C69D: movzx    eax, byte ptr [eax + 0x30]
0x48C6A1: mov      edx, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C6A7: cmp      ax, word ptr [edx + 0x18]
0x48C6AB: jb       0x48d914
0x48C6B1: mov      eax, dword ptr [ebp + 0xc]
0x48C6B4: mov      byte ptr [eax], 1
0x48C6B7: jmp      0x48d914
0x48C6BC: mov      eax, dword ptr [ebp + 0x14]
0x48C6BF: mov      edx, 0x48d9c8 ; "道士秘技"
0x48C6C4: call     0x403aac
0x48C6C9: lea      edx, [ebp - 0x20]
0x48C6CC: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C6D1: movzx    eax, word ptr [eax + 0x18]
0x48C6D5: call     0x407d64
0x48C6DA: mov      ecx, dword ptr [ebp - 0x20]
0x48C6DD: mov      eax, dword ptr [ebp + 0x10]
0x48C6E0: mov      edx, 0x48d9a4 ; "需要等级  "
0x48C6E5: call     0x403d20
0x48C6EA: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48C6EF: mov      eax, dword ptr [eax]
0x48C6F1: cmp      byte ptr [eax + 0x12], 2
0x48C6F5: jne      0x48d914
0x48C6FB: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48C700: mov      eax, dword ptr [eax]
0x48C702: movzx    eax, byte ptr [eax + 0x30]
0x48C706: mov      edx, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C70C: cmp      ax, word ptr [edx + 0x18]
0x48C710: jb       0x48d914
0x48C716: mov      eax, dword ptr [ebp + 0xc]
0x48C719: mov      byte ptr [eax], 1
0x48C71C: jmp      0x48d914
0x48C721: mov      eax, dword ptr [ebp + 0xc]
0x48C724: mov      byte ptr [eax], 0
0x48C727: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C72C: test     byte ptr [eax + 0x14], 1
0x48C730: je       0x48c744
0x48C732: mov      ecx, dword ptr [ebp - 8]
0x48C735: mov      ecx, dword ptr [ecx]
0x48C737: mov      eax, dword ptr [ebp - 8]
0x48C73A: mov      edx, 0x48d9dc ; "(*)"
0x48C73F: call     0x403d20
0x48C744: mov      eax, dword ptr [ebp - 0xc]
0x48C747: push     dword ptr [eax]
0x48C749: push     0x48d9e8 ; "重量"
0x48C74E: lea      edx, [ebp - 0x20]
0x48C751: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C756: movzx    eax, byte ptr [eax + 0x11]
0x48C75A: call     0x407d64
0x48C75F: push     dword ptr [ebp - 0x20]
0x48C762: push     0x48d9f8 ; " 持久力"
0x48C767: push     ebp
0x48C768: lea      ecx, [ebp - 0x24]
0x48C76B: mov      edx, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C771: movzx    edx, word ptr [edx + 0x32]
0x48C775: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C77A: movzx    eax, word ptr [eax + 0x30]
0x48C77E: call     0x48c228
0x48C783: pop      ecx
0x48C784: push     dword ptr [ebp - 0x24]
0x48C787: mov      eax, dword ptr [ebp - 0xc]
0x48C78A: mov      edx, 5
0x48C78F: call     0x403d94
0x48C794: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C799: cmp      word ptr [eax + 0x1e], 0
0x48C79E: jbe      0x48c7ee
0x48C7A0: push     0x48da08 ; "攻击"
0x48C7A5: lea      edx, [ebp - 0x20]
0x48C7A8: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C7AD: movzx    eax, byte ptr [eax + 0x1e]
0x48C7B1: call     0x407d64
0x48C7B6: push     dword ptr [ebp - 0x20]
0x48C7B9: push     0x48da18 ; "-"
0x48C7BE: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C7C3: mov      ax, word ptr [eax + 0x1e]
0x48C7C7: call     0x406974
0x48C7CC: and      eax, 0xff
0x48C7D1: lea      edx, [ebp - 0x24]
0x48C7D4: call     0x407d64
0x48C7D9: push     dword ptr [ebp - 0x24]
0x48C7DC: push     0x48da24 ; " "
0x48C7E1: mov      eax, dword ptr [ebp + 0x14]
0x48C7E4: mov      edx, 5
0x48C7E9: call     0x403d94
0x48C7EE: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C7F3: cmp      word ptr [eax + 0x20], 0
0x48C7F8: jbe      0x48c84d
0x48C7FA: mov      eax, dword ptr [ebp + 0x14]
0x48C7FD: push     dword ptr [eax]
0x48C7FF: push     0x48da30 ; "魔法"
0x48C804: lea      edx, [ebp - 0x20]
0x48C807: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C80C: movzx    eax, byte ptr [eax + 0x20]
0x48C810: call     0x407d64
0x48C815: push     dword ptr [ebp - 0x20]
0x48C818: push     0x48da18 ; "-"
0x48C81D: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C822: mov      ax, word ptr [eax + 0x20]
0x48C826: call     0x406974
0x48C82B: and      eax, 0xff
0x48C830: lea      edx, [ebp - 0x24]
0x48C833: call     0x407d64
0x48C838: push     dword ptr [ebp - 0x24]
0x48C83B: push     0x48da24 ; " "
0x48C840: mov      eax, dword ptr [ebp + 0x14]
0x48C843: mov      edx, 6
0x48C848: call     0x403d94
0x48C84D: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C852: cmp      word ptr [eax + 0x22], 0
0x48C857: jbe      0x48c8ac
0x48C859: mov      eax, dword ptr [ebp + 0x14]
0x48C85C: push     dword ptr [eax]
0x48C85E: push     0x48da40 ; "道术"
0x48C863: lea      edx, [ebp - 0x20]
0x48C866: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C86B: movzx    eax, byte ptr [eax + 0x22]
0x48C86F: call     0x407d64
0x48C874: push     dword ptr [ebp - 0x20]
0x48C877: push     0x48da18 ; "-"
0x48C87C: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C881: mov      ax, word ptr [eax + 0x22]
0x48C885: call     0x406974
0x48C88A: and      eax, 0xff
0x48C88F: lea      edx, [ebp - 0x24]
0x48C892: call     0x407d64
0x48C897: push     dword ptr [ebp - 0x24]
0x48C89A: push     0x48da24 ; " "
0x48C89F: mov      eax, dword ptr [ebp + 0x14]
0x48C8A2: mov      edx, 6
0x48C8A7: call     0x403d94
0x48C8AC: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C8B1: mov      al, byte ptr [eax + 0x13]
0x48C8B4: dec      eax
0x48C8B5: sub      al, 0xa
0x48C8B7: jae      0x48c8e9
0x48C8B9: mov      eax, dword ptr [ebp + 0x14]
0x48C8BC: push     dword ptr [eax]
0x48C8BE: push     0x48da50 ; "强度+"
0x48C8C3: lea      edx, [ebp - 0x20]
0x48C8C6: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C8CB: movsx    eax, byte ptr [eax + 0x13]
0x48C8CF: call     0x407d64
0x48C8D4: push     dword ptr [ebp - 0x20]
0x48C8D7: push     0x48da24 ; " "
0x48C8DC: mov      eax, dword ptr [ebp + 0x14]
0x48C8DF: mov      edx, 4
0x48C8E4: call     0x403d94
0x48C8E9: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C8EE: cmp      byte ptr [eax + 0x13], 0xff
0x48C8F2: jg       0x48c931
0x48C8F4: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C8F9: cmp      byte ptr [eax + 0x13], 0xce
0x48C8FD: jl       0x48c931
0x48C8FF: mov      eax, dword ptr [ebp + 0x14]
0x48C902: push     dword ptr [eax]
0x48C904: push     0x48da60 ; "神圣+"
0x48C909: lea      edx, [ebp - 0x20]
0x48C90C: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C911: movsx    eax, byte ptr [eax + 0x13]
0x48C915: neg      eax
0x48C917: call     0x407d64
0x48C91C: push     dword ptr [ebp - 0x20]
0x48C91F: push     0x48da24 ; " "
0x48C924: mov      eax, dword ptr [ebp + 0x14]
0x48C927: mov      edx, 4
0x48C92C: call     0x403d94
0x48C931: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C936: cmp      byte ptr [eax + 0x13], 0xcd
0x48C93A: jg       0x48c97c
0x48C93C: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C941: cmp      byte ptr [eax + 0x13], 0x9c
0x48C945: jl       0x48c97c
0x48C947: mov      eax, dword ptr [ebp + 0x14]
0x48C94A: push     dword ptr [eax]
0x48C94C: push     0x48da70 ; "神圣-"
0x48C951: lea      edx, [ebp - 0x20]
0x48C954: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C959: movsx    eax, byte ptr [eax + 0x13]
0x48C95D: neg      eax
0x48C95F: sub      eax, 0x32
0x48C962: call     0x407d64
0x48C967: push     dword ptr [ebp - 0x20]
0x48C96A: push     0x48da24 ; " "
0x48C96F: mov      eax, dword ptr [ebp + 0x14]
0x48C972: mov      edx, 4
0x48C977: call     0x403d94
0x48C97C: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C981: mov      ax, word ptr [eax + 0x1a]
0x48C985: call     0x406974
0x48C98A: test     al, al
0x48C98C: jbe      0x48c9c8
0x48C98E: mov      eax, dword ptr [ebp + 0x10]
0x48C991: push     dword ptr [eax]
0x48C993: push     0x48da80 ; "准确+"
0x48C998: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C99D: mov      ax, word ptr [eax + 0x1a]
0x48C9A1: call     0x406974
0x48C9A6: and      eax, 0xff
0x48C9AB: lea      edx, [ebp - 0x20]
0x48C9AE: call     0x407d64
0x48C9B3: push     dword ptr [ebp - 0x20]
0x48C9B6: push     0x48da24 ; " "
0x48C9BB: mov      eax, dword ptr [ebp + 0x10]
0x48C9BE: mov      edx, 4
0x48C9C3: call     0x403d94
0x48C9C8: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C9CD: mov      ax, word ptr [eax + 0x1c]
0x48C9D1: call     0x406974
0x48C9D6: test     al, al
0x48C9D8: jbe      0x48ca69
0x48C9DE: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C9E3: mov      ax, word ptr [eax + 0x1c]
0x48C9E7: call     0x406974
0x48C9EC: cmp      al, 0xa
0x48C9EE: jbe      0x48ca2f
0x48C9F0: mov      eax, dword ptr [ebp + 0x10]
0x48C9F3: push     dword ptr [eax]
0x48C9F5: push     0x48da90 ; "攻击速度+"
0x48C9FA: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48C9FF: mov      ax, word ptr [eax + 0x1c]
0x48CA03: call     0x406974
0x48CA08: and      eax, 0xff
0x48CA0D: sub      eax, 0xa
0x48CA10: lea      edx, [ebp - 0x20]
0x48CA13: call     0x407d64
0x48CA18: push     dword ptr [ebp - 0x20]
0x48CA1B: push     0x48da24 ; " "
0x48CA20: mov      eax, dword ptr [ebp + 0x10]
0x48CA23: mov      edx, 4
0x48CA28: call     0x403d94
0x48CA2D: jmp      0x48ca69
0x48CA2F: mov      eax, dword ptr [ebp + 0x10]
0x48CA32: push     dword ptr [eax]
0x48CA34: push     0x48daa4 ; "攻击速度 -"
0x48CA39: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CA3E: mov      ax, word ptr [eax + 0x1c]
0x48CA42: call     0x406974
0x48CA47: and      eax, 0xff
0x48CA4C: lea      edx, [ebp - 0x20]
0x48CA4F: call     0x407d64
0x48CA54: push     dword ptr [ebp - 0x20]
0x48CA57: push     0x48da24 ; " "
0x48CA5C: mov      eax, dword ptr [ebp + 0x10]
0x48CA5F: mov      edx, 4
0x48CA64: call     0x403d94
0x48CA69: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CA6E: test     byte ptr [eax + 0x1a], 0x80
0x48CA72: je       0x48ca84
0x48CA74: mov      eax, dword ptr [ebp + 0x10]
0x48CA77: mov      edx, 0x48dab8 ; "祝 "
0x48CA7C: call     0x403cdc
0x48CA81: mov      eax, dword ptr [ebp + 0x10]
0x48CA84: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CA89: test     byte ptr [eax + 0x1a], 0x7f
0x48CA8D: jbe      0x48cac5
0x48CA8F: mov      eax, dword ptr [ebp + 0x10]
0x48CA92: push     dword ptr [eax]
0x48CA94: push     0x48dac4 ; "幸运+"
0x48CA99: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CA9E: mov      al, byte ptr [eax + 0x1a]
0x48CAA1: and      al, 0x7f
0x48CAA3: and      eax, 0xff
0x48CAA8: lea      edx, [ebp - 0x20]
0x48CAAB: call     0x407d64
0x48CAB0: push     dword ptr [ebp - 0x20]
0x48CAB3: push     0x48da24 ; " "
0x48CAB8: mov      eax, dword ptr [ebp + 0x10]
0x48CABB: mov      edx, 4
0x48CAC0: call     0x403d94
0x48CAC5: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CACA: cmp      byte ptr [eax + 0x1c], 0
0x48CACE: jbe      0x48cb00
0x48CAD0: mov      eax, dword ptr [ebp + 0x10]
0x48CAD3: push     dword ptr [eax]
0x48CAD5: push     0x48dad4 ; "诅咒+"
0x48CADA: lea      edx, [ebp - 0x20]
0x48CADD: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CAE2: movzx    eax, byte ptr [eax + 0x1c]
0x48CAE6: call     0x407d64
0x48CAEB: push     dword ptr [ebp - 0x20]
0x48CAEE: push     0x48da24 ; " "
0x48CAF3: mov      eax, dword ptr [ebp + 0x10]
0x48CAF6: mov      edx, 4
0x48CAFB: call     0x403d94
0x48CB00: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CB05: mov      al, byte ptr [eax + 0x24]
0x48CB08: sub      al, 1
0x48CB0A: jb       0x48cb23
0x48CB0C: je       0x48cb6e
0x48CB0E: dec      al
0x48CB10: je       0x48cbbf
0x48CB16: dec      al
0x48CB18: je       0x48cc10
0x48CB1E: jmp      0x48d914
0x48CB23: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48CB28: mov      eax, dword ptr [eax]
0x48CB2A: mov      al, byte ptr [eax + 0x30]
0x48CB2D: mov      edx, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CB33: cmp      al, byte ptr [edx + 0x25]
0x48CB36: jb       0x48cb3e
0x48CB38: mov      eax, dword ptr [ebp + 0xc]
0x48CB3B: mov      byte ptr [eax], 1
0x48CB3E: mov      eax, dword ptr [ebp + 0x10]
0x48CB41: push     dword ptr [eax]
0x48CB43: push     0x48dae4 ; "需要等级"
0x48CB48: lea      edx, [ebp - 0x20]
0x48CB4B: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CB50: movzx    eax, byte ptr [eax + 0x25]
0x48CB54: call     0x407d64
0x48CB59: push     dword ptr [ebp - 0x20]
0x48CB5C: mov      eax, dword ptr [ebp + 0x10]
0x48CB5F: mov      edx, 3
0x48CB64: call     0x403d94
0x48CB69: jmp      0x48d914
0x48CB6E: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48CB73: mov      eax, dword ptr [eax]
0x48CB75: mov      ax, word ptr [eax + 0x36]
0x48CB79: call     0x406974
0x48CB7E: mov      edx, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CB84: cmp      al, byte ptr [edx + 0x25]
0x48CB87: jb       0x48cb8f
0x48CB89: mov      eax, dword ptr [ebp + 0xc]
0x48CB8C: mov      byte ptr [eax], 1
0x48CB8F: mov      eax, dword ptr [ebp + 0x10]
0x48CB92: push     dword ptr [eax]
0x48CB94: push     0x48daf8 ; "需要攻击力"
0x48CB99: lea      edx, [ebp - 0x20]
0x48CB9C: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CBA1: movzx    eax, byte ptr [eax + 0x25]
0x48CBA5: call     0x407d64
0x48CBAA: push     dword ptr [ebp - 0x20]
0x48CBAD: mov      eax, dword ptr [ebp + 0x10]
0x48CBB0: mov      edx, 3
0x48CBB5: call     0x403d94
0x48CBBA: jmp      0x48d914
0x48CBBF: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48CBC4: mov      eax, dword ptr [eax]
0x48CBC6: mov      ax, word ptr [eax + 0x38]
0x48CBCA: call     0x406974
0x48CBCF: mov      edx, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CBD5: cmp      al, byte ptr [edx + 0x25]
0x48CBD8: jb       0x48cbe0
0x48CBDA: mov      eax, dword ptr [ebp + 0xc]
0x48CBDD: mov      byte ptr [eax], 1
0x48CBE0: mov      eax, dword ptr [ebp + 0x10]
0x48CBE3: push     dword ptr [eax]
0x48CBE5: push     0x48db0c ; "需要魔法力"
0x48CBEA: lea      edx, [ebp - 0x20]
0x48CBED: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CBF2: movzx    eax, byte ptr [eax + 0x25]
0x48CBF6: call     0x407d64
0x48CBFB: push     dword ptr [ebp - 0x20]
0x48CBFE: mov      eax, dword ptr [ebp + 0x10]
0x48CC01: mov      edx, 3
0x48CC06: call     0x403d94
0x48CC0B: jmp      0x48d914
0x48CC10: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48CC15: mov      eax, dword ptr [eax]
0x48CC17: mov      ax, word ptr [eax + 0x3a]
0x48CC1B: call     0x406974
0x48CC20: mov      edx, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CC26: cmp      al, byte ptr [edx + 0x25]
0x48CC29: jb       0x48cc31
0x48CC2B: mov      eax, dword ptr [ebp + 0xc]
0x48CC2E: mov      byte ptr [eax], 1
0x48CC31: mov      eax, dword ptr [ebp + 0x10]
0x48CC34: push     dword ptr [eax]
0x48CC36: push     0x48db20 ; "需要精神力"
0x48CC3B: lea      edx, [ebp - 0x20]
0x48CC3E: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CC43: movzx    eax, byte ptr [eax + 0x25]
0x48CC47: call     0x407d64
0x48CC4C: push     dword ptr [ebp - 0x20]
0x48CC4F: mov      eax, dword ptr [ebp + 0x10]
0x48CC52: mov      edx, 3
0x48CC57: call     0x403d94
0x48CC5C: jmp      0x48d914
0x48CC61: mov      eax, dword ptr [ebp + 0xc]
0x48CC64: mov      byte ptr [eax], 0
0x48CC67: mov      eax, dword ptr [ebp - 0xc]
0x48CC6A: push     dword ptr [eax]
0x48CC6C: push     0x48d9e8 ; "重量"
0x48CC71: lea      edx, [ebp - 0x20]
0x48CC74: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CC79: movzx    eax, byte ptr [eax + 0x11]
0x48CC7D: call     0x407d64
0x48CC82: push     dword ptr [ebp - 0x20]
0x48CC85: push     0x48d9f8 ; " 持久力"
0x48CC8A: push     ebp
0x48CC8B: lea      ecx, [ebp - 0x24]
0x48CC8E: mov      edx, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CC94: movzx    edx, word ptr [edx + 0x32]
0x48CC98: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CC9D: movzx    eax, word ptr [eax + 0x30]
0x48CCA1: call     0x48c228
0x48CCA6: pop      ecx
0x48CCA7: push     dword ptr [ebp - 0x24]
0x48CCAA: mov      eax, dword ptr [ebp - 0xc]
0x48CCAD: mov      edx, 5
0x48CCB2: call     0x403d94
0x48CCB7: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CCBC: cmp      word ptr [eax + 0x1a], 0
0x48CCC1: jbe      0x48cd11
0x48CCC3: push     0x48db34 ; "防御"
0x48CCC8: lea      edx, [ebp - 0x20]
0x48CCCB: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CCD0: movzx    eax, byte ptr [eax + 0x1a]
0x48CCD4: call     0x407d64
0x48CCD9: push     dword ptr [ebp - 0x20]
0x48CCDC: push     0x48da18 ; "-"
0x48CCE1: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CCE6: mov      ax, word ptr [eax + 0x1a]
0x48CCEA: call     0x406974
0x48CCEF: and      eax, 0xff
0x48CCF4: lea      edx, [ebp - 0x24]
0x48CCF7: call     0x407d64
0x48CCFC: push     dword ptr [ebp - 0x24]
0x48CCFF: push     0x48da24 ; " "
0x48CD04: mov      eax, dword ptr [ebp + 0x14]
0x48CD07: mov      edx, 5
0x48CD0C: call     0x403d94
0x48CD11: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CD16: cmp      word ptr [eax + 0x1c], 0
0x48CD1B: jbe      0x48cd70
0x48CD1D: mov      eax, dword ptr [ebp + 0x14]
0x48CD20: push     dword ptr [eax]
0x48CD22: push     0x48db44 ; "魔御"
0x48CD27: lea      edx, [ebp - 0x20]
0x48CD2A: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CD2F: movzx    eax, byte ptr [eax + 0x1c]
0x48CD33: call     0x407d64
0x48CD38: push     dword ptr [ebp - 0x20]
0x48CD3B: push     0x48da18 ; "-"
0x48CD40: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CD45: mov      ax, word ptr [eax + 0x1c]
0x48CD49: call     0x406974
0x48CD4E: and      eax, 0xff
0x48CD53: lea      edx, [ebp - 0x24]
0x48CD56: call     0x407d64
0x48CD5B: push     dword ptr [ebp - 0x24]
0x48CD5E: push     0x48da24 ; " "
0x48CD63: mov      eax, dword ptr [ebp + 0x14]
0x48CD66: mov      edx, 6
0x48CD6B: call     0x403d94
0x48CD70: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CD75: cmp      word ptr [eax + 0x1e], 0
0x48CD7A: jbe      0x48cdcf
0x48CD7C: mov      eax, dword ptr [ebp + 0x14]
0x48CD7F: push     dword ptr [eax]
0x48CD81: push     0x48da08 ; "攻击"
0x48CD86: lea      edx, [ebp - 0x20]
0x48CD89: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CD8E: movzx    eax, byte ptr [eax + 0x1e]
0x48CD92: call     0x407d64
0x48CD97: push     dword ptr [ebp - 0x20]
0x48CD9A: push     0x48da18 ; "-"
0x48CD9F: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CDA4: mov      ax, word ptr [eax + 0x1e]
0x48CDA8: call     0x406974
0x48CDAD: and      eax, 0xff
0x48CDB2: lea      edx, [ebp - 0x24]
0x48CDB5: call     0x407d64
0x48CDBA: push     dword ptr [ebp - 0x24]
0x48CDBD: push     0x48da24 ; " "
0x48CDC2: mov      eax, dword ptr [ebp + 0x14]
0x48CDC5: mov      edx, 6
0x48CDCA: call     0x403d94
0x48CDCF: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CDD4: cmp      word ptr [eax + 0x20], 0
0x48CDD9: jbe      0x48ce2e
0x48CDDB: mov      eax, dword ptr [ebp + 0x14]
0x48CDDE: push     dword ptr [eax]
0x48CDE0: push     0x48da30 ; "魔法"
0x48CDE5: lea      edx, [ebp - 0x20]
0x48CDE8: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CDED: movzx    eax, byte ptr [eax + 0x20]
0x48CDF1: call     0x407d64
0x48CDF6: push     dword ptr [ebp - 0x20]
0x48CDF9: push     0x48da18 ; "-"
0x48CDFE: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CE03: mov      ax, word ptr [eax + 0x20]
0x48CE07: call     0x406974
0x48CE0C: and      eax, 0xff
0x48CE11: lea      edx, [ebp - 0x24]
0x48CE14: call     0x407d64
0x48CE19: push     dword ptr [ebp - 0x24]
0x48CE1C: push     0x48da24 ; " "
0x48CE21: mov      eax, dword ptr [ebp + 0x14]
0x48CE24: mov      edx, 6
0x48CE29: call     0x403d94
0x48CE2E: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CE33: cmp      word ptr [eax + 0x22], 0
0x48CE38: jbe      0x48ce88
0x48CE3A: mov      eax, dword ptr [ebp + 0x14]
0x48CE3D: push     dword ptr [eax]
0x48CE3F: push     0x48da40 ; "道术"
0x48CE44: lea      edx, [ebp - 0x20]
0x48CE47: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CE4C: movzx    eax, byte ptr [eax + 0x22]
0x48CE50: call     0x407d64
0x48CE55: push     dword ptr [ebp - 0x20]
0x48CE58: push     0x48da18 ; "-"
0x48CE5D: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CE62: mov      ax, word ptr [eax + 0x22]
0x48CE66: call     0x406974
0x48CE6B: and      eax, 0xff
0x48CE70: lea      edx, [ebp - 0x24]
0x48CE73: call     0x407d64
0x48CE78: push     dword ptr [ebp - 0x24]
0x48CE7B: mov      eax, dword ptr [ebp + 0x14]
0x48CE7E: mov      edx, 5
0x48CE83: call     0x403d94
0x48CE88: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CE8D: mov      al, byte ptr [eax + 0x24]
0x48CE90: sub      al, 1
0x48CE92: jb       0x48ceab
0x48CE94: je       0x48ceec
0x48CE96: dec      al
0x48CE98: je       0x48cf33
0x48CE9E: dec      al
0x48CEA0: je       0x48cf7a
0x48CEA6: jmp      0x48d914
0x48CEAB: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48CEB0: mov      eax, dword ptr [eax]
0x48CEB2: mov      al, byte ptr [eax + 0x30]
0x48CEB5: mov      edx, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CEBB: cmp      al, byte ptr [edx + 0x25]
0x48CEBE: jb       0x48cec6
0x48CEC0: mov      eax, dword ptr [ebp + 0xc]
0x48CEC3: mov      byte ptr [eax], 1
0x48CEC6: lea      edx, [ebp - 0x20]
0x48CEC9: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CECE: movzx    eax, byte ptr [eax + 0x25]
0x48CED2: call     0x407d64
0x48CED7: mov      ecx, dword ptr [ebp - 0x20]
0x48CEDA: mov      eax, dword ptr [ebp + 0x10]
0x48CEDD: mov      edx, 0x48dae4 ; "需要等级"
0x48CEE2: call     0x403d20
0x48CEE7: jmp      0x48d914
0x48CEEC: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48CEF1: mov      eax, dword ptr [eax]
0x48CEF3: mov      ax, word ptr [eax + 0x36]
0x48CEF7: call     0x406974
0x48CEFC: mov      edx, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CF02: cmp      al, byte ptr [edx + 0x25]
0x48CF05: jb       0x48cf0d
0x48CF07: mov      eax, dword ptr [ebp + 0xc]
0x48CF0A: mov      byte ptr [eax], 1
0x48CF0D: lea      edx, [ebp - 0x20]
0x48CF10: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CF15: movzx    eax, byte ptr [eax + 0x25]
0x48CF19: call     0x407d64
0x48CF1E: mov      ecx, dword ptr [ebp - 0x20]
0x48CF21: mov      eax, dword ptr [ebp + 0x10]
0x48CF24: mov      edx, 0x48daf8 ; "需要攻击力"
0x48CF29: call     0x403d20
0x48CF2E: jmp      0x48d914
0x48CF33: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48CF38: mov      eax, dword ptr [eax]
0x48CF3A: mov      ax, word ptr [eax + 0x38]
0x48CF3E: call     0x406974
0x48CF43: mov      edx, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CF49: cmp      al, byte ptr [edx + 0x25]
0x48CF4C: jb       0x48cf54
0x48CF4E: mov      eax, dword ptr [ebp + 0xc]
0x48CF51: mov      byte ptr [eax], 1
0x48CF54: lea      edx, [ebp - 0x20]
0x48CF57: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CF5C: movzx    eax, byte ptr [eax + 0x25]
0x48CF60: call     0x407d64
0x48CF65: mov      ecx, dword ptr [ebp - 0x20]
0x48CF68: mov      eax, dword ptr [ebp + 0x10]
0x48CF6B: mov      edx, 0x48db0c ; "需要魔法力"
0x48CF70: call     0x403d20
0x48CF75: jmp      0x48d914
0x48CF7A: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48CF7F: mov      eax, dword ptr [eax]
0x48CF81: mov      ax, word ptr [eax + 0x3a]
0x48CF85: call     0x406974
0x48CF8A: mov      edx, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CF90: cmp      al, byte ptr [edx + 0x25]
0x48CF93: jb       0x48cf9b
0x48CF95: mov      eax, dword ptr [ebp + 0xc]
0x48CF98: mov      byte ptr [eax], 1
0x48CF9B: lea      edx, [ebp - 0x20]
0x48CF9E: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CFA3: movzx    eax, byte ptr [eax + 0x25]
0x48CFA7: call     0x407d64
0x48CFAC: mov      ecx, dword ptr [ebp - 0x20]
0x48CFAF: mov      eax, dword ptr [ebp + 0x10]
0x48CFB2: mov      edx, 0x48db20 ; "需要精神力"
0x48CFB7: call     0x403d20
0x48CFBC: jmp      0x48d914
0x48CFC1: mov      eax, dword ptr [ebp + 0xc]
0x48CFC4: mov      byte ptr [eax], 0
0x48CFC7: mov      eax, dword ptr [ebp - 0xc]
0x48CFCA: push     dword ptr [eax]
0x48CFCC: push     0x48d9e8 ; "重量"
0x48CFD1: lea      edx, [ebp - 0x20]
0x48CFD4: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CFD9: movzx    eax, byte ptr [eax + 0x11]
0x48CFDD: call     0x407d64
0x48CFE2: push     dword ptr [ebp - 0x20]
0x48CFE5: push     0x48d9f8 ; " 持久力"
0x48CFEA: push     ebp
0x48CFEB: lea      ecx, [ebp - 0x24]
0x48CFEE: mov      edx, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CFF4: movzx    edx, word ptr [edx + 0x32]
0x48CFF8: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48CFFD: movzx    eax, word ptr [eax + 0x30]
0x48D001: call     0x48c228
0x48D006: pop      ecx
0x48D007: push     dword ptr [ebp - 0x24]
0x48D00A: mov      eax, dword ptr [ebp - 0xc]
0x48D00D: mov      edx, 5
0x48D012: call     0x403d94
0x48D017: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D01C: cmp      byte ptr [eax + 0x10], 0x82
0x48D020: je       0x48d038
0x48D022: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D027: cmp      byte ptr [eax + 0x10], 0x83
0x48D02B: je       0x48d038
0x48D02D: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D032: cmp      byte ptr [eax + 0x10], 0x84
0x48D036: jne      0x48d050
0x48D038: cmp      byte ptr [ebp + 8], 0
0x48D03C: jne      0x48d050
0x48D03E: mov      eax, dword ptr [ebp + 0x14]
0x48D041: mov      edx, 0x48db54 ; "????????"
0x48D046: call     0x403aac
0x48D04B: jmp      0x48d914
0x48D050: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D055: movzx    eax, byte ptr [eax + 0xf]
0x48D059: add      eax, -0x13
0x48D05C: cmp      eax, 5
0x48D05F: ja       0x48d41d
0x48D065: jmp      dword ptr [eax*4 + 0x48d06c]
0x48D06C: test     al, dl
0x48D06E: dec      eax
0x48D06F: add      byte ptr [edx - 0x2f], bl
0x48D072: dec      eax
0x48D073: add      bh, ch
0x48D075: ror      dword ptr [eax], 1
0x48D078: sbb      eax, 0x60048d4
0x48D07D: ror      dword ptr [eax], cl
0x48D080: pop      edx
0x48D081: ror      dword ptr [eax], 1
0x48D084: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D089: cmp      word ptr [eax + 0x1a], 0
0x48D08E: jbe      0x48d0ca
0x48D090: mov      eax, dword ptr [ebp + 0x14]
0x48D093: push     dword ptr [eax]
0x48D095: push     0x48db68 ; "魔法躲避+"
0x48D09A: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D09F: mov      ax, word ptr [eax + 0x1a]
0x48D0A3: call     0x406974
0x48D0A8: and      eax, 0xff
0x48D0AD: lea      edx, [ebp - 0x20]
0x48D0B0: call     0x407d64
0x48D0B5: push     dword ptr [ebp - 0x20]
0x48D0B8: push     0x48db7c ; "0% "
0x48D0BD: mov      eax, dword ptr [ebp + 0x14]
0x48D0C0: mov      edx, 4
0x48D0C5: call     0x403d94
0x48D0CA: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D0CF: cmp      byte ptr [eax + 0x1c], 0
0x48D0D3: jbe      0x48d105
0x48D0D5: mov      eax, dword ptr [ebp + 0x14]
0x48D0D8: push     dword ptr [eax]
0x48D0DA: push     0x48dad4 ; "诅咒+"
0x48D0DF: lea      edx, [ebp - 0x20]
0x48D0E2: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D0E7: movzx    eax, byte ptr [eax + 0x1c]
0x48D0EB: call     0x407d64
0x48D0F0: push     dword ptr [ebp - 0x20]
0x48D0F3: push     0x48da24 ; " "
0x48D0F8: mov      eax, dword ptr [ebp + 0x14]
0x48D0FB: mov      edx, 4
0x48D100: call     0x403d94
0x48D105: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D10A: mov      ax, word ptr [eax + 0x1c]
0x48D10E: call     0x406974
0x48D113: test     al, al
0x48D115: jbe      0x48d4db
0x48D11B: mov      eax, dword ptr [ebp + 0x14]
0x48D11E: push     dword ptr [eax]
0x48D120: push     0x48dac4 ; "幸运+"
0x48D125: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D12A: mov      ax, word ptr [eax + 0x1c]
0x48D12E: call     0x406974
0x48D133: and      eax, 0xff
0x48D138: lea      edx, [ebp - 0x20]
0x48D13B: call     0x407d64
0x48D140: push     dword ptr [ebp - 0x20]
0x48D143: push     0x48da24 ; " "
0x48D148: mov      eax, dword ptr [ebp + 0x14]
0x48D14B: mov      edx, 4
0x48D150: call     0x403d94
0x48D155: jmp      0x48d4db
0x48D15A: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D15F: cmp      word ptr [eax + 0x1a], 0
0x48D164: jbe      0x48d1a0
0x48D166: mov      eax, dword ptr [ebp + 0x14]
0x48D169: push     dword ptr [eax]
0x48D16B: push     0x48da80 ; "准确+"
0x48D170: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D175: mov      ax, word ptr [eax + 0x1a]
0x48D179: call     0x406974
0x48D17E: and      eax, 0xff
0x48D183: lea      edx, [ebp - 0x20]
0x48D186: call     0x407d64
0x48D18B: push     dword ptr [ebp - 0x20]
0x48D18E: push     0x48da24 ; " "
0x48D193: mov      eax, dword ptr [ebp + 0x14]
0x48D196: mov      edx, 4
0x48D19B: call     0x403d94
0x48D1A0: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D1A5: cmp      word ptr [eax + 0x1c], 0
0x48D1AA: jbe      0x48d4db
0x48D1B0: mov      eax, dword ptr [ebp + 0x14]
0x48D1B3: push     dword ptr [eax]
0x48D1B5: push     0x48db88 ; "敏捷+"
0x48D1BA: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D1BF: mov      ax, word ptr [eax + 0x1c]
0x48D1C3: call     0x406974
0x48D1C8: and      eax, 0xff
0x48D1CD: lea      edx, [ebp - 0x20]
0x48D1D0: call     0x407d64
0x48D1D5: push     dword ptr [ebp - 0x20]
0x48D1D8: push     0x48da24 ; " "
0x48D1DD: mov      eax, dword ptr [ebp + 0x14]
0x48D1E0: mov      edx, 4
0x48D1E5: call     0x403d94
0x48D1EA: jmp      0x48d4db
0x48D1EF: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D1F4: mov      ax, word ptr [eax + 0x1a]
0x48D1F8: call     0x406974
0x48D1FD: test     al, al
0x48D1FF: jbe      0x48d23b
0x48D201: mov      eax, dword ptr [ebp + 0x14]
0x48D204: push     dword ptr [eax]
0x48D206: push     0x48db98 ; "体力恢复+"
0x48D20B: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D210: mov      ax, word ptr [eax + 0x1a]
0x48D214: call     0x406974
0x48D219: and      eax, 0xff
0x48D21E: lea      edx, [ebp - 0x20]
0x48D221: call     0x407d64
0x48D226: push     dword ptr [ebp - 0x20]
0x48D229: push     0x48db7c ; "0% "
0x48D22E: mov      eax, dword ptr [ebp + 0x14]
0x48D231: mov      edx, 4
0x48D236: call     0x403d94
0x48D23B: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D240: mov      ax, word ptr [eax + 0x1c]
0x48D244: call     0x406974
0x48D249: test     al, al
0x48D24B: jbe      0x48d287
0x48D24D: mov      eax, dword ptr [ebp + 0x14]
0x48D250: push     dword ptr [eax]
0x48D252: push     0x48dbac ; "魔法恢复+"
0x48D257: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D25C: mov      ax, word ptr [eax + 0x1c]
0x48D260: call     0x406974
0x48D265: and      eax, 0xff
0x48D26A: lea      edx, [ebp - 0x20]
0x48D26D: call     0x407d64
0x48D272: push     dword ptr [ebp - 0x20]
0x48D275: push     0x48db7c ; "0% "
0x48D27A: mov      eax, dword ptr [ebp + 0x14]
0x48D27D: mov      edx, 4
0x48D282: call     0x403d94
0x48D287: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D28C: cmp      byte ptr [eax + 0x1a], 0
0x48D290: jbe      0x48d2c2
0x48D292: mov      eax, dword ptr [ebp + 0x14]
0x48D295: push     dword ptr [eax]
0x48D297: push     0x48da90 ; "攻击速度+"
0x48D29C: lea      edx, [ebp - 0x20]
0x48D29F: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D2A4: movzx    eax, byte ptr [eax + 0x1a]
0x48D2A8: call     0x407d64
0x48D2AD: push     dword ptr [ebp - 0x20]
0x48D2B0: push     0x48da24 ; " "
0x48D2B5: mov      eax, dword ptr [ebp + 0x14]
0x48D2B8: mov      edx, 4
0x48D2BD: call     0x403d94
0x48D2C2: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D2C7: cmp      byte ptr [eax + 0x1c], 0
0x48D2CB: jbe      0x48d4db
0x48D2D1: mov      eax, dword ptr [ebp + 0x14]
0x48D2D4: push     dword ptr [eax]
0x48D2D6: push     0x48daa4 ; "攻击速度 -"
0x48D2DB: lea      edx, [ebp - 0x20]
0x48D2DE: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D2E3: movzx    eax, byte ptr [eax + 0x1c]
0x48D2E7: call     0x407d64
0x48D2EC: push     dword ptr [ebp - 0x20]
0x48D2EF: push     0x48da24 ; " "
0x48D2F4: mov      eax, dword ptr [ebp + 0x14]
0x48D2F7: mov      edx, 4
0x48D2FC: call     0x403d94
0x48D301: jmp      0x48d4db
0x48D306: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D30B: mov      ax, word ptr [eax + 0x1a]
0x48D30F: call     0x406974
0x48D314: test     al, al
0x48D316: jbe      0x48d352
0x48D318: mov      eax, dword ptr [ebp + 0x14]
0x48D31B: push     dword ptr [eax]
0x48D31D: push     0x48dbc0 ; "毒物躲避+"
0x48D322: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D327: mov      ax, word ptr [eax + 0x1a]
0x48D32B: call     0x406974
0x48D330: and      eax, 0xff
0x48D335: lea      edx, [ebp - 0x20]
0x48D338: call     0x407d64
0x48D33D: push     dword ptr [ebp - 0x20]
0x48D340: push     0x48db7c ; "0% "
0x48D345: mov      eax, dword ptr [ebp + 0x14]
0x48D348: mov      edx, 4
0x48D34D: call     0x403d94
0x48D352: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D357: mov      ax, word ptr [eax + 0x1c]
0x48D35B: call     0x406974
0x48D360: test     al, al
0x48D362: jbe      0x48d39e
0x48D364: mov      eax, dword ptr [ebp + 0x14]
0x48D367: push     dword ptr [eax]
0x48D369: push     0x48dbd4 ; "中毒恢复+"
0x48D36E: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D373: mov      ax, word ptr [eax + 0x1c]
0x48D377: call     0x406974
0x48D37C: and      eax, 0xff
0x48D381: lea      edx, [ebp - 0x20]
0x48D384: call     0x407d64
0x48D389: push     dword ptr [ebp - 0x20]
0x48D38C: push     0x48db7c ; "0% "
0x48D391: mov      eax, dword ptr [ebp + 0x14]
0x48D394: mov      edx, 4
0x48D399: call     0x403d94
0x48D39E: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D3A3: cmp      byte ptr [eax + 0x1a], 0
0x48D3A7: jbe      0x48d3d9
0x48D3A9: mov      eax, dword ptr [ebp + 0x14]
0x48D3AC: push     dword ptr [eax]
0x48D3AE: push     0x48da90 ; "攻击速度+"
0x48D3B3: lea      edx, [ebp - 0x20]
0x48D3B6: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D3BB: movzx    eax, byte ptr [eax + 0x1a]
0x48D3BF: call     0x407d64
0x48D3C4: push     dword ptr [ebp - 0x20]
0x48D3C7: push     0x48da24 ; " "
0x48D3CC: mov      eax, dword ptr [ebp + 0x14]
0x48D3CF: mov      edx, 4
0x48D3D4: call     0x403d94
0x48D3D9: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D3DE: cmp      byte ptr [eax + 0x1c], 0
0x48D3E2: jbe      0x48d4db
0x48D3E8: mov      eax, dword ptr [ebp + 0x14]
0x48D3EB: push     dword ptr [eax]
0x48D3ED: push     0x48daa4 ; "攻击速度 -"
0x48D3F2: lea      edx, [ebp - 0x20]
0x48D3F5: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D3FA: movzx    eax, byte ptr [eax + 0x1c]
0x48D3FE: call     0x407d64
0x48D403: push     dword ptr [ebp - 0x20]
0x48D406: push     0x48da24 ; " "
0x48D40B: mov      eax, dword ptr [ebp + 0x14]
0x48D40E: mov      edx, 4
0x48D413: call     0x403d94
0x48D418: jmp      0x48d4db
0x48D41D: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D422: cmp      word ptr [eax + 0x1a], 0
0x48D427: jbe      0x48d47c
0x48D429: mov      eax, dword ptr [ebp + 0x14]
0x48D42C: push     dword ptr [eax]
0x48D42E: push     0x48db34 ; "防御"
0x48D433: lea      edx, [ebp - 0x20]
0x48D436: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D43B: movzx    eax, byte ptr [eax + 0x1a]
0x48D43F: call     0x407d64
0x48D444: push     dword ptr [ebp - 0x20]
0x48D447: push     0x48da18 ; "-"
0x48D44C: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D451: mov      ax, word ptr [eax + 0x1a]
0x48D455: call     0x406974
0x48D45A: and      eax, 0xff
0x48D45F: lea      edx, [ebp - 0x24]
0x48D462: call     0x407d64
0x48D467: push     dword ptr [ebp - 0x24]
0x48D46A: push     0x48da24 ; " "
0x48D46F: mov      eax, dword ptr [ebp + 0x14]
0x48D472: mov      edx, 6
0x48D477: call     0x403d94
0x48D47C: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D481: cmp      word ptr [eax + 0x1c], 0
0x48D486: jbe      0x48d4db
0x48D488: mov      eax, dword ptr [ebp + 0x14]
0x48D48B: push     dword ptr [eax]
0x48D48D: push     0x48db44 ; "魔御"
0x48D492: lea      edx, [ebp - 0x20]
0x48D495: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D49A: movzx    eax, byte ptr [eax + 0x1c]
0x48D49E: call     0x407d64
0x48D4A3: push     dword ptr [ebp - 0x20]
0x48D4A6: push     0x48da18 ; "-"
0x48D4AB: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D4B0: mov      ax, word ptr [eax + 0x1c]
0x48D4B4: call     0x406974
0x48D4B9: and      eax, 0xff
0x48D4BE: lea      edx, [ebp - 0x24]
0x48D4C1: call     0x407d64
0x48D4C6: push     dword ptr [ebp - 0x24]
0x48D4C9: push     0x48da24 ; " "
0x48D4CE: mov      eax, dword ptr [ebp + 0x14]
0x48D4D1: mov      edx, 6
0x48D4D6: call     0x403d94
0x48D4DB: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D4E0: cmp      word ptr [eax + 0x1e], 0
0x48D4E5: jbe      0x48d53a
0x48D4E7: mov      eax, dword ptr [ebp + 0x14]
0x48D4EA: push     dword ptr [eax]
0x48D4EC: push     0x48da08 ; "攻击"
0x48D4F1: lea      edx, [ebp - 0x20]
0x48D4F4: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D4F9: movzx    eax, byte ptr [eax + 0x1e]
0x48D4FD: call     0x407d64
0x48D502: push     dword ptr [ebp - 0x20]
0x48D505: push     0x48da18 ; "-"
0x48D50A: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D50F: mov      ax, word ptr [eax + 0x1e]
0x48D513: call     0x406974
0x48D518: and      eax, 0xff
0x48D51D: lea      edx, [ebp - 0x24]
0x48D520: call     0x407d64
0x48D525: push     dword ptr [ebp - 0x24]
0x48D528: push     0x48da24 ; " "
0x48D52D: mov      eax, dword ptr [ebp + 0x14]
0x48D530: mov      edx, 6
0x48D535: call     0x403d94
0x48D53A: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D53F: cmp      word ptr [eax + 0x20], 0
0x48D544: jbe      0x48d599
0x48D546: mov      eax, dword ptr [ebp + 0x14]
0x48D549: push     dword ptr [eax]
0x48D54B: push     0x48da30 ; "魔法"
0x48D550: lea      edx, [ebp - 0x20]
0x48D553: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D558: movzx    eax, byte ptr [eax + 0x20]
0x48D55C: call     0x407d64
0x48D561: push     dword ptr [ebp - 0x20]
0x48D564: push     0x48da18 ; "-"
0x48D569: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D56E: mov      ax, word ptr [eax + 0x20]
0x48D572: call     0x406974
0x48D577: and      eax, 0xff
0x48D57C: lea      edx, [ebp - 0x24]
0x48D57F: call     0x407d64
0x48D584: push     dword ptr [ebp - 0x24]
0x48D587: push     0x48da24 ; " "
0x48D58C: mov      eax, dword ptr [ebp + 0x14]
0x48D58F: mov      edx, 6
0x48D594: call     0x403d94
0x48D599: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D59E: cmp      word ptr [eax + 0x22], 0
0x48D5A3: jbe      0x48d5f3
0x48D5A5: mov      eax, dword ptr [ebp + 0x14]
0x48D5A8: push     dword ptr [eax]
0x48D5AA: push     0x48da40 ; "道术"
0x48D5AF: lea      edx, [ebp - 0x20]
0x48D5B2: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D5B7: movzx    eax, byte ptr [eax + 0x22]
0x48D5BB: call     0x407d64
0x48D5C0: push     dword ptr [ebp - 0x20]
0x48D5C3: push     0x48da18 ; "-"
0x48D5C8: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D5CD: mov      ax, word ptr [eax + 0x22]
0x48D5D1: call     0x406974
0x48D5D6: and      eax, 0xff
0x48D5DB: lea      edx, [ebp - 0x24]
0x48D5DE: call     0x407d64
0x48D5E3: push     dword ptr [ebp - 0x24]
0x48D5E6: mov      eax, dword ptr [ebp + 0x14]
0x48D5E9: mov      edx, 5
0x48D5EE: call     0x403d94
0x48D5F3: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D5F8: mov      al, byte ptr [eax + 0x24]
0x48D5FB: sub      al, 1
0x48D5FD: jb       0x48d616
0x48D5FF: je       0x48d661
0x48D601: dec      al
0x48D603: je       0x48d6b2
0x48D609: dec      al
0x48D60B: je       0x48d703
0x48D611: jmp      0x48d914
0x48D616: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48D61B: mov      eax, dword ptr [eax]
0x48D61D: mov      al, byte ptr [eax + 0x30]
0x48D620: mov      edx, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D626: cmp      al, byte ptr [edx + 0x25]
0x48D629: jb       0x48d631
0x48D62B: mov      eax, dword ptr [ebp + 0xc]
0x48D62E: mov      byte ptr [eax], 1
0x48D631: mov      eax, dword ptr [ebp + 0x10]
0x48D634: push     dword ptr [eax]
0x48D636: push     0x48dae4 ; "需要等级"
0x48D63B: lea      edx, [ebp - 0x20]
0x48D63E: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D643: movzx    eax, byte ptr [eax + 0x25]
0x48D647: call     0x407d64
0x48D64C: push     dword ptr [ebp - 0x20]
0x48D64F: mov      eax, dword ptr [ebp + 0x10]
0x48D652: mov      edx, 3
0x48D657: call     0x403d94
0x48D65C: jmp      0x48d914
0x48D661: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48D666: mov      eax, dword ptr [eax]
0x48D668: mov      ax, word ptr [eax + 0x36]
0x48D66C: call     0x406974
0x48D671: mov      edx, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D677: cmp      al, byte ptr [edx + 0x25]
0x48D67A: jb       0x48d682
0x48D67C: mov      eax, dword ptr [ebp + 0xc]
0x48D67F: mov      byte ptr [eax], 1
0x48D682: mov      eax, dword ptr [ebp + 0x10]
0x48D685: push     dword ptr [eax]
0x48D687: push     0x48daf8 ; "需要攻击力"
0x48D68C: lea      edx, [ebp - 0x20]
0x48D68F: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D694: movzx    eax, byte ptr [eax + 0x25]
0x48D698: call     0x407d64
0x48D69D: push     dword ptr [ebp - 0x20]
0x48D6A0: mov      eax, dword ptr [ebp + 0x10]
0x48D6A3: mov      edx, 3
0x48D6A8: call     0x403d94
0x48D6AD: jmp      0x48d914
0x48D6B2: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48D6B7: mov      eax, dword ptr [eax]
0x48D6B9: mov      ax, word ptr [eax + 0x38]
0x48D6BD: call     0x406974
0x48D6C2: mov      edx, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D6C8: cmp      al, byte ptr [edx + 0x25]
0x48D6CB: jb       0x48d6d3
0x48D6CD: mov      eax, dword ptr [ebp + 0xc]
0x48D6D0: mov      byte ptr [eax], 1
0x48D6D3: mov      eax, dword ptr [ebp + 0x10]
0x48D6D6: push     dword ptr [eax]
0x48D6D8: push     0x48db0c ; "需要魔法力"
0x48D6DD: lea      edx, [ebp - 0x20]
0x48D6E0: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D6E5: movzx    eax, byte ptr [eax + 0x25]
0x48D6E9: call     0x407d64
0x48D6EE: push     dword ptr [ebp - 0x20]
0x48D6F1: mov      eax, dword ptr [ebp + 0x10]
0x48D6F4: mov      edx, 3
0x48D6F9: call     0x403d94
0x48D6FE: jmp      0x48d914
0x48D703: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48D708: mov      eax, dword ptr [eax]
0x48D70A: mov      ax, word ptr [eax + 0x3a]
0x48D70E: call     0x406974
0x48D713: mov      edx, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D719: cmp      al, byte ptr [edx + 0x25]
0x48D71C: jb       0x48d724
0x48D71E: mov      eax, dword ptr [ebp + 0xc]
0x48D721: mov      byte ptr [eax], 1
0x48D724: mov      eax, dword ptr [ebp + 0x10]
0x48D727: push     dword ptr [eax]
0x48D729: push     0x48db20 ; "需要精神力"
0x48D72E: lea      edx, [ebp - 0x20]
0x48D731: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D736: movzx    eax, byte ptr [eax + 0x25]
0x48D73A: call     0x407d64
0x48D73F: push     dword ptr [ebp - 0x20]
0x48D742: mov      eax, dword ptr [ebp + 0x10]
0x48D745: mov      edx, 3
0x48D74A: call     0x403d94
0x48D74F: jmp      0x48d914
0x48D754: mov      eax, dword ptr [ebp - 0xc]
0x48D757: push     dword ptr [eax]
0x48D759: push     0x48d9e8 ; "重量"
0x48D75E: lea      edx, [ebp - 0x20]
0x48D761: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D766: movzx    eax, byte ptr [eax + 0x11]
0x48D76A: call     0x407d64
0x48D76F: push     dword ptr [ebp - 0x20]
0x48D772: mov      eax, dword ptr [ebp - 0xc]
0x48D775: mov      edx, 3
0x48D77A: call     0x403d94
0x48D77F: push     ebp
0x48D780: lea      ecx, [ebp - 0x20]
0x48D783: mov      edx, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D789: movzx    edx, word ptr [edx + 0x32]
0x48D78D: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D792: movzx    eax, word ptr [eax + 0x30]
0x48D796: call     0x48c2f8
0x48D79B: pop      ecx
0x48D79C: mov      ecx, dword ptr [ebp - 0x20]
0x48D79F: mov      eax, dword ptr [ebp + 0x14]
0x48D7A2: mov      edx, 0x48dbe8 ; "使用"
0x48D7A7: call     0x403d20
0x48D7AC: jmp      0x48d914
0x48D7B1: mov      eax, dword ptr [ebp - 0xc]
0x48D7B4: push     dword ptr [eax]
0x48D7B6: push     0x48d9e8 ; "重量"
0x48D7BB: lea      edx, [ebp - 0x20]
0x48D7BE: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D7C3: movzx    eax, byte ptr [eax + 0x11]
0x48D7C7: call     0x407d64
0x48D7CC: push     dword ptr [ebp - 0x20]
0x48D7CF: push     0x48d9f8 ; " 持久力"
0x48D7D4: push     ebp
0x48D7D5: lea      ecx, [ebp - 0x24]
0x48D7D8: mov      edx, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D7DE: movzx    edx, word ptr [edx + 0x32]
0x48D7E2: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D7E7: movzx    eax, word ptr [eax + 0x30]
0x48D7EB: call     0x48c228
0x48D7F0: pop      ecx
0x48D7F1: push     dword ptr [ebp - 0x24]
0x48D7F4: mov      eax, dword ptr [ebp - 0xc]
0x48D7F7: mov      edx, 5
0x48D7FC: call     0x403d94
0x48D801: jmp      0x48d914
0x48D806: mov      eax, dword ptr [ebp - 0xc]
0x48D809: push     dword ptr [eax]
0x48D80B: push     0x48d9e8 ; "重量"
0x48D810: lea      edx, [ebp - 0x20]
0x48D813: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D818: movzx    eax, byte ptr [eax + 0x11]
0x48D81C: call     0x407d64
0x48D821: push     dword ptr [ebp - 0x20]
0x48D824: push     0x48dbf8 ; " 品质"
0x48D829: push     ebp
0x48D82A: lea      ecx, [ebp - 0x24]
0x48D82D: mov      edx, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D833: movzx    edx, word ptr [edx + 0x32]
0x48D837: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D83C: movzx    eax, word ptr [eax + 0x30]
0x48D840: call     0x48c228
0x48D845: pop      ecx
0x48D846: push     dword ptr [ebp - 0x24]
0x48D849: mov      eax, dword ptr [ebp - 0xc]
0x48D84C: mov      edx, 5
0x48D851: call     0x403d94
0x48D856: jmp      0x48d914
0x48D85B: mov      eax, dword ptr [ebp - 0xc]
0x48D85E: push     dword ptr [eax]
0x48D860: push     0x48d9e8 ; "重量"
0x48D865: lea      edx, [ebp - 0x20]
0x48D868: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D86D: movzx    eax, byte ptr [eax + 0x11]
0x48D871: call     0x407d64
0x48D876: push     dword ptr [ebp - 0x20]
0x48D879: push     0x48dc08 ; " 药材"
0x48D87E: mov      eax, dword ptr [ebp - 0xc]
0x48D881: mov      edx, 4
0x48D886: call     0x403d94
0x48D88B: jmp      0x48d914
0x48D890: mov      eax, dword ptr [ebp - 0xc]
0x48D893: push     dword ptr [eax]
0x48D895: push     0x48d9e8 ; "重量"
0x48D89A: lea      edx, [ebp - 0x20]
0x48D89D: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D8A2: movzx    eax, byte ptr [eax + 0x11]
0x48D8A6: call     0x407d64
0x48D8AB: push     dword ptr [ebp - 0x20]
0x48D8AE: push     0x48dc18 ; " 纯度"
0x48D8B3: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D8B8: movzx    eax, word ptr [eax + 0x30]
0x48D8BC: mov      dword ptr [ebp - 0x28], eax
0x48D8BF: fild     dword ptr [ebp - 0x28]
0x48D8C2: fdiv     dword ptr [0x48dc20]
0x48D8C8: call     0x4029a0
0x48D8CD: push     edx
0x48D8CE: push     eax
0x48D8CF: lea      eax, [ebp - 0x24]
0x48D8D2: call     0x407d94
0x48D8D7: push     dword ptr [ebp - 0x24]
0x48D8DA: mov      eax, dword ptr [ebp - 0xc]
0x48D8DD: mov      edx, 5
0x48D8E2: call     0x403d94
0x48D8E7: jmp      0x48d914
0x48D8E9: mov      eax, dword ptr [ebp - 0xc]
0x48D8EC: push     dword ptr [eax]
0x48D8EE: push     0x48d9e8 ; "重量"
0x48D8F3: lea      edx, [ebp - 0x20]
0x48D8F6: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48D8FB: movzx    eax, byte ptr [eax + 0x11]
0x48D8FF: call     0x407d64
0x48D904: push     dword ptr [ebp - 0x20]
0x48D907: mov      eax, dword ptr [ebp - 0xc]
0x48D90A: mov      edx, 3
0x48D90F: call     0x403d94
0x48D914: xor      eax, eax
0x48D916: pop      edx
0x48D917: pop      ecx
0x48D918: pop      ecx
0x48D919: mov      dword ptr fs:[eax], edx
0x48D91C: push     0x48d936
0x48D921: lea      eax, [ebp - 0x24]
0x48D924: mov      edx, 2
0x48D929: call     0x403a7c
0x48D92E: ret      
0x48D92F: jmp      0x403518
0x48D934: jmp      0x48d921
0x48D936: pop      ebx
0x48D937: mov      esp, ebp
0x48D939: pop      ebp
0x48D93A: ret      0x10
0x48D93D: add      byte ptr [eax], al
0x48D93F: add      byte ptr [ecx], al
0x48D941: and      byte ptr [eax], al
0x48D943: add      bh, bh
0x48D947: inc      dword ptr [ecx]
0x48D949: add      byte ptr [eax], al
0x48D94B: add      byte ptr [ebx], ch
0x48D94D: add      byte ptr [eax], al
0x48D94F: add      bh, bh
0x48D953: inc      dword ptr [ebx]
0x48D955: add      byte ptr [eax], al
0x48D957: add      byte ptr [eax + 0x50], cl
0x48D95A: and      byte ptr [eax], al
0x48D964: [STR len=2] "MP"
0x48D970: [STR len=5] " 重量"
0x48D980: [STR len=5] "重量 "
0x48D990: [STR len=9] "武术秘技 "
0x48D9A4: [STR len=10] "需要等级  "
0x48D9B8: [STR len=6] "魔法书"
0x48D9C8: [STR len=8] "道士秘技"
0x48D9DC: [STR len=3] "(*)"
0x48D9E8: [STR len=4] "重量"
0x48D9F8: [STR len=7] " 持久力"
0x48DA08: [STR len=4] "攻击"
0x48DA18: [STR len=1] "-"
0x48DA24: [STR len=1] " "
0x48DA30: [STR len=4] "魔法"
0x48DA40: [STR len=4] "道术"
0x48DA50: [STR len=5] "强度+"
0x48DA60: [STR len=5] "神圣+"
0x48DA70: [STR len=5] "神圣-"
0x48DA80: [STR len=5] "准确+"
0x48DA90: [STR len=9] "攻击速度+"
0x48DAA4: [STR len=10] "攻击速度 -"
0x48DAB8: [STR len=3] "祝 "
0x48DAC4: [STR len=5] "幸运+"
0x48DAD4: [STR len=5] "诅咒+"
0x48DAE4: [STR len=8] "需要等级"
0x48DAF8: [STR len=10] "需要攻击力"
0x48DB0C: [STR len=10] "需要魔法力"
0x48DB20: [STR len=10] "需要精神力"
0x48DB34: [STR len=4] "防御"
0x48DB44: [STR len=4] "魔御"
0x48DB54: [STR len=8] "????????"
0x48DB68: [STR len=9] "魔法躲避+"
0x48DB7C: [STR len=3] "0% "
0x48DB88: [STR len=5] "敏捷+"
0x48DB98: [STR len=9] "体力恢复+"
0x48DBAC: [STR len=9] "魔法恢复+"
0x48DBC0: [STR len=9] "毒物躲避+"
0x48DBD4: [STR len=9] "中毒恢复+"
0x48DBE8: [STR len=4] "使用"
0x48DBF8: [STR len=5] " 品质"
0x48DC08: [STR len=5] " 药材"
0x48DC18: [STR len=5] " 纯度"
0x48DC20: add      byte ptr [eax], al
0x48DC22: jp       0x48dc68
0x48DC24: push     ebp
0x48DC25: mov      ebp, esp
0x48DC27: add      esp, -0x44
0x48DC2A: push     ebx
0x48DC2B: xor      ebx, ebx
0x48DC2D: mov      dword ptr [ebp - 0x44], ebx
0x48DC30: mov      dword ptr [ebp - 0xc], ebx
0x48DC33: mov      dword ptr [ebp - 0x10], ebx
0x48DC36: mov      dword ptr [ebp - 0x14], ebx
0x48DC39: mov      dword ptr [ebp - 0x18], ebx
0x48DC3C: mov      dword ptr [ebp - 8], ecx
0x48DC3F: mov      dword ptr [ebp - 0x30], edx
0x48DC42: mov      dword ptr [ebp - 4], eax
0x48DC45: xor      eax, eax
0x48DC47: push     ebp
0x48DC48: push     0x48deae
0x48DC4D: push     dword ptr fs:[eax]
0x48DC50: mov      dword ptr fs:[eax], esp
0x48DC53: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48DC58: cmp      dword ptr [eax], 0
0x48DC5B: je       0x48de8b
0x48DC61: mov      eax, dword ptr [ebp - 4]
0x48DC64: mov      eax, dword ptr [eax + 0x2cc]
0x48DC6A: mov      dword ptr [ebp - 0x28], eax
0x48DC6D: mov      eax, dword ptr [ebp - 0x28]
0x48DC70: mov      edx, dword ptr [eax + 0x258]
0x48DC76: mov      eax, dword ptr [ebp - 0x28]
0x48DC79: mov      eax, dword ptr [eax + 0x254]
0x48DC7F: call     0x45351c
0x48DC84: mov      dword ptr [ebp - 0x24], eax
0x48DC87: cmp      dword ptr [ebp - 0x24], 0
0x48DC8B: je       0x48dcca
0x48DC8D: lea      edx, [ebp - 0x40]
0x48DC90: mov      eax, dword ptr [ebp - 0x24]
0x48DC93: call     0x44d8ec
0x48DC98: lea      eax, [ebp - 0x40]
0x48DC9B: push     eax
0x48DC9C: mov      eax, dword ptr [ebp - 0x24]
0x48DC9F: push     eax
0x48DCA0: push     1
0x48DCA2: mov      eax, dword ptr [ebp - 0x28]
0x48DCA5: mov      edx, dword ptr [eax + 0x34]
0x48DCA8: mov      eax, dword ptr [ebp - 0x28]
0x48DCAB: call     0x456fdc
0x48DCB0: push     eax
0x48DCB1: mov      eax, dword ptr [ebp - 0x28]
0x48DCB4: mov      edx, dword ptr [eax + 0x30]
0x48DCB7: mov      eax, dword ptr [ebp - 0x28]
0x48DCBA: call     0x456f94
0x48DCBF: mov      edx, eax
0x48DCC1: mov      eax, dword ptr [ebp - 8]
0x48DCC4: pop      ecx
0x48DCC5: call     0x44d6cc
0x48DCCA: lea      eax, [ebp - 0x14]
0x48DCCD: push     eax
0x48DCCE: lea      eax, [ebp - 0x18]
0x48DCD1: push     eax
0x48DCD2: lea      eax, [ebp - 0x1d]
0x48DCD5: push     eax
0x48DCD6: push     0
0x48DCD8: lea      ecx, [ebp - 0x10]
0x48DCDB: lea      edx, [ebp - 0xc]
0x48DCDE: mov      eax, dword ptr [ebp - 4]
0x48DCE1: call     0x48c3c8
0x48DCE6: mov      eax, dword ptr [ebp - 8]
0x48DCE9: call     0x44d8b4
0x48DCEE: mov      dword ptr [ebp - 0x2c], eax
0x48DCF1: push     1
0x48DCF3: mov      eax, dword ptr [ebp - 0x2c]
0x48DCF6: call     0x41834c
0x48DCFB: push     eax
0x48DCFC: call     0x406434
0x48DD01: mov      edx, 0xffffff
0x48DD06: mov      eax, dword ptr [ebp - 0x2c]
0x48DD09: mov      eax, dword ptr [eax + 0xc]
0x48DD0C: call     0x4173fc
0x48DD11: lea      edx, [ebp - 0x44]
0x48DD14: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48DD19: mov      eax, dword ptr [eax]
0x48DD1B: mov      eax, dword ptr [eax + 0x58]
0x48DD1E: call     0x49f640
0x48DD23: mov      eax, dword ptr [ebp - 0x44]
0x48DD26: push     eax
0x48DD27: mov      eax, dword ptr [ebp - 0x28]
0x48DD2A: mov      edx, dword ptr [eax + 0x34]
0x48DD2D: add      edx, 0xb9
0x48DD33: mov      eax, dword ptr [ebp - 0x28]
0x48DD36: call     0x456fdc
0x48DD3B: push     eax
0x48DD3C: mov      eax, dword ptr [ebp - 0x28]
0x48DD3F: mov      edx, dword ptr [eax + 0x30]
0x48DD42: add      edx, 0x40
0x48DD45: mov      eax, dword ptr [ebp - 0x28]
0x48DD48: call     0x456f94
0x48DD4D: mov      edx, eax
0x48DD4F: mov      eax, dword ptr [ebp - 0x2c]
0x48DD52: pop      ecx
0x48DD53: call     0x4180dc
0x48DD58: cmp      dword ptr [ebp - 0xc], 0
0x48DD5C: je       0x48de83
0x48DD62: mov      edx, dword ptr [ebp - 0xc]
0x48DD65: mov      eax, dword ptr [ebp - 0x2c]
0x48DD68: call     0x4181ac
0x48DD6D: mov      dword ptr [ebp - 0x1c], eax
0x48DD70: mov      edx, 0xffff
0x48DD75: mov      eax, dword ptr [ebp - 0x2c]
0x48DD78: mov      eax, dword ptr [eax + 0xc]
0x48DD7B: call     0x4173fc
0x48DD80: mov      eax, dword ptr [ebp - 0xc]
0x48DD83: push     eax
0x48DD84: mov      eax, dword ptr [ebp - 0x28]
0x48DD87: mov      edx, dword ptr [eax + 0x34]
0x48DD8A: add      edx, 0xd7
0x48DD90: mov      eax, dword ptr [ebp - 0x28]
0x48DD93: call     0x456fdc
0x48DD98: push     eax
0x48DD99: mov      eax, dword ptr [ebp - 0x28]
0x48DD9C: mov      edx, dword ptr [eax + 0x30]
0x48DD9F: add      edx, 0x46
0x48DDA2: mov      eax, dword ptr [ebp - 0x28]
0x48DDA5: call     0x456f94
0x48DDAA: mov      edx, eax
0x48DDAC: mov      eax, dword ptr [ebp - 0x2c]
0x48DDAF: pop      ecx
0x48DDB0: call     0x4180dc
0x48DDB5: mov      edx, 0xffffff
0x48DDBA: mov      eax, dword ptr [ebp - 0x2c]
0x48DDBD: mov      eax, dword ptr [eax + 0xc]
0x48DDC0: call     0x4173fc
0x48DDC5: mov      eax, dword ptr [ebp - 0x10]
0x48DDC8: push     eax
0x48DDC9: mov      eax, dword ptr [ebp - 0x28]
0x48DDCC: mov      edx, dword ptr [eax + 0x34]
0x48DDCF: add      edx, 0xd7
0x48DDD5: mov      eax, dword ptr [ebp - 0x28]
0x48DDD8: call     0x456fdc
0x48DDDD: push     eax
0x48DDDE: mov      eax, dword ptr [ebp - 0x28]
0x48DDE1: mov      edx, dword ptr [eax + 0x30]
0x48DDE4: add      edx, 0x46
0x48DDE7: mov      eax, dword ptr [ebp - 0x28]
0x48DDEA: call     0x456f94
0x48DDEF: mov      edx, eax
0x48DDF1: add      edx, dword ptr [ebp - 0x1c]
0x48DDF4: mov      eax, dword ptr [ebp - 0x2c]
0x48DDF7: pop      ecx
0x48DDF8: call     0x4180dc
0x48DDFD: mov      eax, dword ptr [ebp - 0x14]
0x48DE00: push     eax
0x48DE01: mov      eax, dword ptr [ebp - 0x28]
0x48DE04: mov      edx, dword ptr [eax + 0x34]
0x48DE07: add      edx, 0xd7
0x48DE0D: add      edx, 0xe
0x48DE10: mov      eax, dword ptr [ebp - 0x28]
0x48DE13: call     0x456fdc
0x48DE18: push     eax
0x48DE19: mov      eax, dword ptr [ebp - 0x28]
0x48DE1C: mov      edx, dword ptr [eax + 0x30]
0x48DE1F: add      edx, 0x46
0x48DE22: mov      eax, dword ptr [ebp - 0x28]
0x48DE25: call     0x456f94
0x48DE2A: mov      edx, eax
0x48DE2C: mov      eax, dword ptr [ebp - 0x2c]
0x48DE2F: pop      ecx
0x48DE30: call     0x4180dc
0x48DE35: cmp      byte ptr [ebp - 0x1d], 0
0x48DE39: jne      0x48de4b
0x48DE3B: mov      edx, 0xff
0x48DE40: mov      eax, dword ptr [ebp - 0x2c]
0x48DE43: mov      eax, dword ptr [eax + 0xc]
0x48DE46: call     0x4173fc
0x48DE4B: mov      eax, dword ptr [ebp - 0x18]
0x48DE4E: push     eax
0x48DE4F: mov      eax, dword ptr [ebp - 0x28]
0x48DE52: mov      edx, dword ptr [eax + 0x34]
0x48DE55: add      edx, 0xd7
0x48DE5B: add      edx, 0x1c
0x48DE5E: mov      eax, dword ptr [ebp - 0x28]
0x48DE61: call     0x456fdc
0x48DE66: push     eax
0x48DE67: mov      eax, dword ptr [ebp - 0x28]
0x48DE6A: mov      edx, dword ptr [eax + 0x30]
0x48DE6D: add      edx, 0x46
0x48DE70: mov      eax, dword ptr [ebp - 0x28]
0x48DE73: call     0x456f94
0x48DE78: mov      edx, eax
0x48DE7A: mov      eax, dword ptr [ebp - 0x2c]
0x48DE7D: pop      ecx
0x48DE7E: call     0x4180dc
0x48DE83: mov      eax, dword ptr [ebp - 0x2c]
0x48DE86: call     0x44d104
0x48DE8B: xor      eax, eax
0x48DE8D: pop      edx
0x48DE8E: pop      ecx
0x48DE8F: pop      ecx
0x48DE90: mov      dword ptr fs:[eax], edx
0x48DE93: push     0x48deb5
0x48DE98: lea      eax, [ebp - 0x44]
0x48DE9B: call     0x403a58
0x48DEA0: lea      eax, [ebp - 0x18]
0x48DEA3: mov      edx, 4
0x48DEA8: call     0x403a7c
0x48DEAD: ret      
0x48DEAE: jmp      0x403518
0x48DEB3: jmp      0x48de98
0x48DEB5: pop      ebx
0x48DEB6: mov      esp, ebp
0x48DEB8: pop      ebp
0x48DEB9: ret      
0x48DEBA: mov      eax, eax
0x48DEBC: push     ebp
0x48DEBD: mov      ebp, esp
0x48DEBF: add      esp, -0xc
0x48DEC2: mov      dword ptr [ebp - 8], ecx
0x48DEC5: mov      dword ptr [ebp - 0xc], edx
0x48DEC8: mov      dword ptr [ebp - 4], eax
0x48DECB: cmp      dword ptr [ebp - 8], 0
0x48DECF: jl       0x48df01
0x48DED1: cmp      dword ptr [ebp + 0xc], 0
0x48DED5: jl       0x48df01
0x48DED7: mov      eax, dword ptr [ebp - 4]
0x48DEDA: mov      eax, dword ptr [eax + 0x2ec]
0x48DEE0: mov      eax, dword ptr [eax + 0x38]
0x48DEE3: cmp      eax, dword ptr [ebp - 8]
0x48DEE6: jl       0x48df01
0x48DEE8: mov      eax, dword ptr [ebp - 4]
0x48DEEB: mov      eax, dword ptr [eax + 0x2ec]
0x48DEF1: mov      eax, dword ptr [eax + 0x3c]
0x48DEF4: cmp      eax, dword ptr [ebp + 0xc]
0x48DEF7: jl       0x48df01
0x48DEF9: mov      eax, dword ptr [ebp + 8]
0x48DEFC: mov      byte ptr [eax], 1
0x48DEFF: jmp      0x48df07
0x48DF01: mov      eax, dword ptr [ebp + 8]
0x48DF04: mov      byte ptr [eax], 0
0x48DF07: mov      esp, ebp
0x48DF09: pop      ebp
0x48DF0A: ret      8
0x48DF0D: lea      eax, [eax]
0x48DF10: push     ebp
0x48DF11: mov      ebp, esp
0x48DF13: add      esp, -0x24
0x48DF16: mov      dword ptr [ebp - 8], ecx
0x48DF19: mov      dword ptr [ebp - 0x14], edx
0x48DF1C: mov      dword ptr [ebp - 4], eax
0x48DF1F: mov      eax, dword ptr [ebp - 4]
0x48DF22: mov      eax, dword ptr [eax + 0x2ec]
0x48DF28: mov      dword ptr [ebp - 0x10], eax
0x48DF2B: mov      eax, dword ptr [ebp - 0x10]
0x48DF2E: mov      edx, dword ptr [eax + 0x258]
0x48DF34: mov      eax, dword ptr [ebp - 0x10]
0x48DF37: mov      eax, dword ptr [eax + 0x254]
0x48DF3D: call     0x45351c
0x48DF42: mov      dword ptr [ebp - 0xc], eax
0x48DF45: mov      eax, dword ptr [ebp - 4]
0x48DF48: mov      eax, dword ptr [eax + 0x2ec]
0x48DF4E: cmp      byte ptr [eax + 0x274], 0
0x48DF55: je       0x48df98
0x48DF57: cmp      dword ptr [ebp - 0xc], 0
0x48DF5B: je       0x48df98
0x48DF5D: lea      edx, [ebp - 0x24]
0x48DF60: mov      eax, dword ptr [ebp - 0xc]
0x48DF63: call     0x44d8ec
0x48DF68: lea      eax, [ebp - 0x24]
0x48DF6B: push     eax
0x48DF6C: mov      eax, dword ptr [ebp - 0xc]
0x48DF6F: push     eax
0x48DF70: push     1
0x48DF72: mov      edx, 0xb7
0x48DF77: mov      eax, dword ptr [ebp - 0x10]
0x48DF7A: call     0x456fdc
0x48DF7F: push     eax
0x48DF80: mov      edx, 0xfe
0x48DF85: mov      eax, dword ptr [ebp - 0x10]
0x48DF88: call     0x456f94
0x48DF8D: mov      edx, eax
0x48DF8F: mov      eax, dword ptr [ebp - 8]
0x48DF92: pop      ecx
0x48DF93: call     0x44d6cc
0x48DF98: mov      esp, ebp
0x48DF9A: pop      ebp
0x48DF9B: ret      
0x48DF9C: push     ebp
0x48DF9D: mov      ebp, esp
0x48DF9F: add      esp, -0x24
0x48DFA2: mov      dword ptr [ebp - 8], ecx
0x48DFA5: mov      dword ptr [ebp - 0x14], edx
0x48DFA8: mov      dword ptr [ebp - 4], eax
0x48DFAB: mov      eax, dword ptr [ebp - 4]
0x48DFAE: mov      eax, dword ptr [eax + 0x2f0]
0x48DFB4: mov      dword ptr [ebp - 0x10], eax
0x48DFB7: mov      eax, dword ptr [ebp - 4]
0x48DFBA: mov      eax, dword ptr [eax + 0x2f0]
0x48DFC0: cmp      byte ptr [eax + 0x274], 0
0x48DFC7: je       0x48e026
0x48DFC9: mov      eax, dword ptr [ebp - 0x10]
0x48DFCC: mov      edx, dword ptr [eax + 0x258]
0x48DFD2: mov      eax, dword ptr [ebp - 0x10]
0x48DFD5: mov      eax, dword ptr [eax + 0x254]
0x48DFDB: call     0x45351c
0x48DFE0: mov      dword ptr [ebp - 0xc], eax
0x48DFE3: cmp      dword ptr [ebp - 0xc], 0
0x48DFE7: je       0x48e026
0x48DFE9: lea      edx, [ebp - 0x24]
0x48DFEC: mov      eax, dword ptr [ebp - 0xc]
0x48DFEF: call     0x44d8ec
0x48DFF4: lea      eax, [ebp - 0x24]
0x48DFF7: push     eax
0x48DFF8: mov      eax, dword ptr [ebp - 0xc]
0x48DFFB: push     eax
0x48DFFC: push     1
0x48DFFE: mov      eax, dword ptr [ebp - 0x10]
0x48E001: mov      edx, dword ptr [eax + 0x34]
0x48E004: mov      eax, dword ptr [ebp - 0x10]
0x48E007: call     0x456fdc
0x48E00C: push     eax
0x48E00D: mov      eax, dword ptr [ebp - 0x10]
0x48E010: mov      edx, dword ptr [eax + 0x30]
0x48E013: mov      eax, dword ptr [ebp - 0x10]
0x48E016: call     0x456f94
0x48E01B: mov      edx, eax
0x48E01D: mov      eax, dword ptr [ebp - 8]
0x48E020: pop      ecx
0x48E021: call     0x44d6cc
0x48E026: mov      esp, ebp
0x48E028: pop      ebp
0x48E029: ret      
0x48E02A: mov      eax, eax
0x48E02C: push     ebp
0x48E02D: mov      ebp, esp
0x48E02F: add      esp, -0xc
0x48E032: mov      dword ptr [ebp - 0xc], ecx
0x48E035: mov      dword ptr [ebp - 8], edx
0x48E038: mov      dword ptr [ebp - 4], eax
0x48E03B: xor      edx, edx
0x48E03D: mov      eax, dword ptr [ebp - 4]
0x48E040: mov      eax, dword ptr [eax + 0x2cc]
0x48E046: call     0x458214
0x48E04B: mov      esp, ebp
0x48E04D: pop      ebp
0x48E04E: ret      4
0x48E051: lea      eax, [eax]
0x48E054: push     ebp
0x48E055: mov      ebp, esp
0x48E057: add      esp, -0x5c
0x48E05A: push     ebx
0x48E05B: push     esi
0x48E05C: push     edi
0x48E05D: xor      ebx, ebx
0x48E05F: mov      dword ptr [ebp - 0x48], ebx
0x48E062: mov      dword ptr [ebp - 0x4c], ebx
0x48E065: mov      dword ptr [ebp - 0x50], ebx
0x48E068: mov      dword ptr [ebp - 0x54], ebx
0x48E06B: mov      dword ptr [ebp - 8], ecx
0x48E06E: mov      dword ptr [ebp - 0x10], edx
0x48E071: mov      dword ptr [ebp - 4], eax
0x48E074: xor      eax, eax
0x48E076: push     ebp
0x48E077: push     0x48e107
0x48E07C: push     dword ptr fs:[eax]
0x48E07F: mov      dword ptr fs:[eax], esp
0x48E082: test     byte ptr [ebp + 8], 0x10
0x48E086: je       0x48e0aa
0x48E088: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x48E08D: cmp      byte ptr [eax], 0
0x48E090: je       0x48e0ec
0x48E092: mov      eax, dword ptr [ebp + 0xc]
0x48E095: push     eax
0x48E096: mov      al, byte ptr [ebp + 8]
0x48E099: push     eax
0x48E09A: mov      ecx, dword ptr [ebp - 8]
0x48E09D: mov      edx, dword ptr [ebp - 4]
0x48E0A0: mov      eax, dword ptr [ebp - 4]
0x48E0A3: call     0x48e118
0x48E0A8: jmp      0x48e0ec
0x48E0AA: mov      eax, dword ptr [ebp - 4]
0x48E0AD: mov      eax, dword ptr [eax + 0x360]
0x48E0B3: mov      eax, dword ptr [eax + 0x260]
0x48E0B9: imul     dword ptr [ebp + 0xc]
0x48E0BC: add      eax, dword ptr [ebp - 8]
0x48E0BF: add      eax, 6
0x48E0C2: mov      dword ptr [ebp - 0xc], eax
0x48E0C5: mov      eax, dword ptr [ebp - 0xc]
0x48E0C8: add      eax, -6
0x48E0CB: sub      eax, 0x28
0x48E0CE: jae      0x48e0ec
0x48E0D0: imul     eax, dword ptr [ebp - 0xc], 0xd
0x48E0D4: mov      edx, dword ptr [0x4a4264] ; *0x4A4264->0x4F80D0
0x48E0DA: mov      ecx, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48E0E0: lea      esi, [edx + eax*4]
0x48E0E3: mov      edi, ecx
0x48E0E5: mov      ecx, 0xd
0x48E0EA: rep movsd dword ptr es:[edi], dword ptr [esi]
0x48E0EC: xor      eax, eax
0x48E0EE: pop      edx
0x48E0EF: pop      ecx
0x48E0F0: pop      ecx
0x48E0F1: mov      dword ptr fs:[eax], edx
0x48E0F4: push     0x48e10e
0x48E0F9: lea      eax, [ebp - 0x54]
0x48E0FC: mov      edx, 4
0x48E101: call     0x403a7c
0x48E106: ret      
0x48E107: jmp      0x403518
0x48E10C: jmp      0x48e0f9
0x48E10E: pop      edi
0x48E10F: pop      esi
0x48E110: pop      ebx
0x48E111: mov      esp, ebp
0x48E113: pop      ebp
0x48E114: ret      8
0x48E117: nop      
0x48E118: push     ebp
0x48E119: mov      ebp, esp
0x48E11B: add      esp, -0x4c
0x48E11E: push     ebx
0x48E11F: push     esi
0x48E120: push     edi
0x48E121: xor      ebx, ebx
0x48E123: mov      dword ptr [ebp - 0x4c], ebx
0x48E126: mov      dword ptr [ebp - 8], ecx
0x48E129: mov      dword ptr [ebp - 0x14], edx
0x48E12C: mov      dword ptr [ebp - 4], eax
0x48E12F: xor      eax, eax
0x48E131: push     ebp
0x48E132: push     0x48e32c
0x48E137: push     dword ptr fs:[eax]
0x48E13A: mov      dword ptr fs:[eax], esp
0x48E13D: mov      eax, dword ptr [ebp - 4]
0x48E140: mov      eax, dword ptr [eax + 0x360]
0x48E146: mov      eax, dword ptr [eax + 0x260]
0x48E14C: imul     dword ptr [ebp + 0xc]
0x48E14F: add      eax, dword ptr [ebp - 8]
0x48E152: add      eax, 6
0x48E155: mov      dword ptr [ebp - 0xc], eax
0x48E158: mov      eax, dword ptr [ebp - 0xc]
0x48E15B: add      eax, -6
0x48E15E: sub      eax, 0x28
0x48E161: jae      0x48e311
0x48E167: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x48E16C: cmp      byte ptr [eax], 0
0x48E16F: jne      0x48e1d9
0x48E171: imul     eax, dword ptr [ebp - 0xc], 0xd
0x48E175: mov      edx, dword ptr [0x4a4264] ; *0x4A4264->0x4F80D0
0x48E17B: cmp      byte ptr [edx + eax*4], 0
0x48E17F: je       0x48e311
0x48E185: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x48E18A: mov      byte ptr [eax], 1
0x48E18D: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48E192: mov      edx, dword ptr [ebp - 0xc]
0x48E195: mov      dword ptr [eax], edx
0x48E197: imul     eax, dword ptr [ebp - 0xc], 0xd
0x48E19B: mov      edx, dword ptr [0x4a4264] ; *0x4A4264->0x4F80D0
0x48E1A1: mov      ecx, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48E1A7: lea      esi, [edx + eax*4]
0x48E1AA: lea      edi, [ecx + 4]
0x48E1AD: mov      ecx, 0xd
0x48E1B2: rep movsd dword ptr es:[edi], dword ptr [esi]
0x48E1B4: imul     eax, dword ptr [ebp - 0xc], 0xd
0x48E1B8: mov      edx, dword ptr [0x4a4264] ; *0x4A4264->0x4F80D0
0x48E1BE: mov      byte ptr [edx + eax*4], 0
0x48E1C2: imul     eax, dword ptr [ebp - 0xc], 0xd
0x48E1C6: mov      edx, dword ptr [0x4a4264] ; *0x4A4264->0x4F80D0
0x48E1CC: lea      eax, [edx + eax*4]
0x48E1CF: call     0x495940
0x48E1D4: jmp      0x48e311
0x48E1D9: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48E1DE: mov      eax, dword ptr [eax]
0x48E1E0: mov      dword ptr [ebp - 0x10], eax
0x48E1E3: cmp      dword ptr [ebp - 0x10], -0x61
0x48E1E7: je       0x48e316
0x48E1ED: cmp      dword ptr [ebp - 0x10], -0x62
0x48E1F1: je       0x48e316
0x48E1F7: cmp      dword ptr [ebp - 0x10], 0
0x48E1FB: jge      0x48e264
0x48E1FD: cmp      dword ptr [ebp - 0x10], -9
0x48E201: jl       0x48e264
0x48E203: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48E208: mov      edx, dword ptr [0x4a41e4] ; *0x4A41E4->0x4F93D8
0x48E20E: mov      esi, eax
0x48E210: mov      edi, edx
0x48E212: mov      ecx, 0xe
0x48E217: rep movsd dword ptr es:[edi], dword ptr [esi]
0x48E219: lea      eax, [ebp - 0x4c]
0x48E21C: mov      edx, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48E222: add      edx, 4
0x48E225: call     0x403c78
0x48E22A: mov      eax, dword ptr [ebp - 0x4c]
0x48E22D: push     eax
0x48E22E: mov      edx, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48E234: mov      dl, byte ptr [edx]
0x48E236: inc      edx
0x48E237: neg      dl
0x48E239: mov      ecx, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48E23F: mov      ecx, dword ptr [ecx + 0x30]
0x48E242: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48E247: mov      eax, dword ptr [eax]
0x48E249: call     0x465a6c
0x48E24E: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48E253: mov      byte ptr [eax + 4], 0
0x48E257: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x48E25C: mov      byte ptr [eax], 0
0x48E25F: jmp      0x48e311
0x48E264: cmp      dword ptr [ebp - 0x10], -0x14
0x48E268: jg       0x48e281
0x48E26A: cmp      dword ptr [ebp - 0x10], -0x1e
0x48E26E: jle      0x48e281
0x48E270: mov      edx, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48E276: add      edx, 4
0x48E279: mov      eax, dword ptr [ebp - 4]
0x48E27C: call     0x491ae0
0x48E281: imul     eax, dword ptr [ebp - 0xc], 0xd
0x48E285: mov      edx, dword ptr [0x4a4264] ; *0x4A4264->0x4F80D0
0x48E28B: cmp      byte ptr [edx + eax*4], 0
0x48E28F: je       0x48e2e3
0x48E291: imul     eax, dword ptr [ebp - 0xc], 0xd
0x48E295: mov      edx, dword ptr [0x4a4264] ; *0x4A4264->0x4F80D0
0x48E29B: lea      esi, [edx + eax*4]
0x48E29E: lea      edi, [ebp - 0x48]
0x48E2A1: mov      ecx, 0xd
0x48E2A6: rep movsd dword ptr es:[edi], dword ptr [esi]
0x48E2A8: imul     eax, dword ptr [ebp - 0xc], 0xd
0x48E2AC: mov      edx, dword ptr [0x4a4264] ; *0x4A4264->0x4F80D0
0x48E2B2: mov      ecx, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48E2B8: lea      edi, [edx + eax*4]
0x48E2BB: lea      esi, [ecx + 4]
0x48E2BE: mov      ecx, 0xd
0x48E2C3: rep movsd dword ptr es:[edi], dword ptr [esi]
0x48E2C5: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48E2CA: mov      edx, dword ptr [ebp - 0xc]
0x48E2CD: mov      dword ptr [eax], edx
0x48E2CF: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48E2D4: lea      edi, [eax + 4]
0x48E2D7: lea      esi, [ebp - 0x48]
0x48E2DA: mov      ecx, 0xd
0x48E2DF: rep movsd dword ptr es:[edi], dword ptr [esi]
0x48E2E1: jmp      0x48e311
0x48E2E3: imul     eax, dword ptr [ebp - 0xc], 0xd
0x48E2E7: mov      edx, dword ptr [0x4a4264] ; *0x4A4264->0x4F80D0
0x48E2ED: mov      ecx, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48E2F3: lea      edi, [edx + eax*4]
0x48E2F6: lea      esi, [ecx + 4]
0x48E2F9: mov      ecx, 0xd
0x48E2FE: rep movsd dword ptr es:[edi], dword ptr [esi]
0x48E300: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48E305: mov      byte ptr [eax + 4], 0
0x48E309: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x48E30E: mov      byte ptr [eax], 0
0x48E311: call     0x49faa4
0x48E316: xor      eax, eax
0x48E318: pop      edx
0x48E319: pop      ecx
0x48E31A: pop      ecx
0x48E31B: mov      dword ptr fs:[eax], edx
0x48E31E: push     0x48e333
0x48E323: lea      eax, [ebp - 0x4c]
0x48E326: call     0x403a58
0x48E32B: ret      
0x48E32C: jmp      0x403518
0x48E331: jmp      0x48e323
0x48E333: pop      edi
0x48E334: pop      esi
0x48E335: pop      ebx
0x48E336: mov      esp, ebp
0x48E338: pop      ebp
0x48E339: ret      8
0x48E33C: push     ebp
0x48E33D: mov      ebp, esp
0x48E33F: add      esp, 0xfffffebc
0x48E345: push     esi
0x48E346: push     edi
0x48E347: mov      dword ptr [ebp - 0xc], edx
0x48E34A: mov      dword ptr [ebp - 4], eax
0x48E34D: mov      eax, dword ptr [ebp - 4]
0x48E350: mov      eax, dword ptr [eax + 0x360]
0x48E356: mov      eax, dword ptr [eax + 0x2a8]
0x48E35C: mov      edx, dword ptr [ebp - 4]
0x48E35F: mov      edx, dword ptr [edx + 0x360]
0x48E365: imul     dword ptr [edx + 0x260]
0x48E36B: mov      edx, dword ptr [ebp - 4]
0x48E36E: mov      edx, dword ptr [edx + 0x360]
0x48E374: add      eax, dword ptr [edx + 0x2a4]
0x48E37A: add      eax, 6
0x48E37D: mov      dword ptr [ebp - 8], eax
0x48E380: mov      eax, dword ptr [ebp - 8]
0x48E383: add      eax, -6
0x48E386: sub      eax, 0x28
0x48E389: jae      0x48e451
0x48E38F: imul     eax, dword ptr [ebp - 8], 0xd
0x48E393: mov      edx, dword ptr [0x4a4264] ; *0x4A4264->0x4F80D0
0x48E399: cmp      byte ptr [edx + eax*4], 0
0x48E39D: jne      0x48e451
0x48E3A3: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x48E3A8: cmp      byte ptr [eax], 0
0x48E3AB: je       0x48e451
0x48E3B1: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48E3B6: cmp      byte ptr [eax + 4], 0
0x48E3BA: je       0x48e451
0x48E3C0: lea      eax, [ebp - 0x110]
0x48E3C6: xor      ecx, ecx
0x48E3C8: mov      edx, 0x100
0x48E3CD: call     0x402b54
0x48E3D2: lea      eax, [ebp - 0x110]
0x48E3D8: push     eax
0x48E3D9: call     0x406654
0x48E3DE: cmp      byte ptr [ebp - 0xff], 0x80
0x48E3E5: jne      0x48e41a
0x48E3E7: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48E3EC: lea      esi, [eax + 4]
0x48E3EF: lea      edi, [ebp - 0x144]
0x48E3F5: mov      ecx, 0xd
0x48E3FA: rep movsd dword ptr es:[edi], dword ptr [esi]
0x48E3FC: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48E401: mov      byte ptr [eax + 4], 0
0x48E405: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x48E40A: mov      byte ptr [eax], 0
0x48E40D: lea      eax, [ebp - 0x144]
0x48E413: call     0x49f874
0x48E418: jmp      0x48e451
0x48E41A: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48E41F: mov      eax, dword ptr [eax]
0x48E421: cmp      eax, dword ptr [ebp - 8]
0x48E424: jne      0x48e431
0x48E426: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48E42B: cmp      byte ptr [eax + 0x13], 4
0x48E42F: jbe      0x48e442
0x48E431: imul     eax, dword ptr [ebp - 8], 0xd
0x48E435: mov      edx, dword ptr [0x4a4264] ; *0x4A4264->0x4F80D0
0x48E43B: cmp      byte ptr [edx + eax*4 + 0xf], 0x1f
0x48E440: jne      0x48e451
0x48E442: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48E447: mov      eax, dword ptr [eax]
0x48E449: or       edx, 0xffffffff
0x48E44C: call     0x462b1c
0x48E451: pop      edi
0x48E452: pop      esi
0x48E453: mov      esp, ebp
0x48E455: pop      ebp
0x48E456: ret      
0x48E457: nop      
0x48E458: push     ebp
0x48E459: mov      ebp, esp
0x48E45B: add      esp, -0x38
0x48E45E: push     esi
0x48E45F: push     edi
0x48E460: mov      esi, dword ptr [ebp + 0x10]
0x48E463: lea      edi, [ebp - 0x28]
0x48E466: push     ecx
0x48E467: mov      ecx, 4
0x48E46C: rep movsd dword ptr es:[edi], dword ptr [esi]
0x48E46E: pop      ecx
0x48E46F: mov      dword ptr [ebp - 8], ecx
0x48E472: mov      dword ptr [ebp - 0x18], edx
0x48E475: mov      dword ptr [ebp - 4], eax
0x48E478: mov      eax, dword ptr [ebp - 4]
0x48E47B: mov      eax, dword ptr [eax + 0x360]
0x48E481: mov      eax, dword ptr [eax + 0x260]
0x48E487: imul     dword ptr [ebp + 0x14]
0x48E48A: add      eax, dword ptr [ebp - 8]
0x48E48D: add      eax, 6
0x48E490: mov      dword ptr [ebp - 0xc], eax
0x48E493: mov      eax, dword ptr [ebp - 0xc]
0x48E496: add      eax, -6
0x48E499: sub      eax, 0x28
0x48E49C: jae      0x48e559
0x48E4A2: imul     eax, dword ptr [ebp - 0xc], 0xd
0x48E4A6: mov      edx, dword ptr [0x4a4264] ; *0x4A4264->0x4F80D0
0x48E4AC: cmp      byte ptr [edx + eax*4], 0
0x48E4B0: je       0x48e559
0x48E4B6: imul     eax, dword ptr [ebp - 0xc], 0xd
0x48E4BA: mov      edx, dword ptr [0x4a4264] ; *0x4A4264->0x4F80D0
0x48E4C0: movzx    edx, word ptr [edx + eax*4 + 0x16]
0x48E4C5: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48E4CA: mov      eax, dword ptr [eax]
0x48E4CC: mov      eax, dword ptr [eax + 0x324]
0x48E4D2: call     0x45351c
0x48E4D7: mov      dword ptr [ebp - 0x10], eax
0x48E4DA: cmp      dword ptr [ebp - 0x10], 0
0x48E4DE: je       0x48e559
0x48E4E0: mov      eax, dword ptr [ebp - 4]
0x48E4E3: mov      eax, dword ptr [eax + 0x360]
0x48E4E9: mov      dword ptr [ebp - 0x14], eax
0x48E4EC: lea      edx, [ebp - 0x38]
0x48E4EF: mov      eax, dword ptr [ebp - 0x10]
0x48E4F2: call     0x44d8ec
0x48E4F7: lea      eax, [ebp - 0x38]
0x48E4FA: push     eax
0x48E4FB: mov      eax, dword ptr [ebp - 0x10]
0x48E4FE: push     eax
0x48E4FF: push     1
0x48E501: mov      eax, dword ptr [ebp - 0x10]
0x48E504: call     0x44d944
0x48E509: mov      edx, dword ptr [ebp - 0x14]
0x48E50C: mov      edx, dword ptr [edx + 0x26c]
0x48E512: sub      edx, eax
0x48E514: sar      edx, 1
0x48E516: jns      0x48e51b
0x48E518: adc      edx, 0
0x48E51B: add      edx, dword ptr [ebp - 0x24]
0x48E51E: inc      edx
0x48E51F: mov      eax, dword ptr [ebp - 0x14]
0x48E522: call     0x456fdc
0x48E527: push     eax
0x48E528: mov      eax, dword ptr [ebp - 0x10]
0x48E52B: call     0x44dc60
0x48E530: mov      edx, dword ptr [ebp - 0x14]
0x48E533: mov      edx, dword ptr [edx + 0x268]
0x48E539: sub      edx, eax
0x48E53B: sar      edx, 1
0x48E53D: jns      0x48e542
0x48E53F: adc      edx, 0
0x48E542: add      edx, dword ptr [ebp - 0x28]
0x48E545: dec      edx
0x48E546: mov      eax, dword ptr [ebp - 0x14]
0x48E549: call     0x456f94
0x48E54E: mov      edx, eax
0x48E550: mov      eax, dword ptr [ebp + 8]
0x48E553: pop      ecx
0x48E554: call     0x44d6cc
0x48E559: pop      edi
0x48E55A: pop      esi
0x48E55B: mov      esp, ebp
0x48E55D: pop      ebp
0x48E55E: ret      0x10
0x48E561: lea      eax, [eax]
0x48E564: push     ebp
0x48E565: mov      ebp, esp
0x48E567: add      esp, -0xc
0x48E56A: push     esi
0x48E56B: push     edi
0x48E56C: mov      dword ptr [ebp - 0xc], ecx
0x48E56F: mov      dword ptr [ebp - 8], edx
0x48E572: mov      dword ptr [ebp - 4], eax
0x48E575: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48E57A: cmp      dword ptr [eax], 0
0x48E57D: je       0x48e5fb
0x48E57F: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x48E584: cmp      byte ptr [eax], 0
0x48E587: jne      0x48e5c4
0x48E589: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x48E58E: mov      eax, dword ptr [eax]
0x48E590: cmp      dword ptr [eax + 0x58], 0
0x48E594: jle      0x48e5fb
0x48E596: mov      eax, 0x6a
0x48E59B: call     0x495794
0x48E5A0: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x48E5A5: mov      byte ptr [eax], 1
0x48E5A8: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48E5AD: mov      dword ptr [eax], 0xffffff9e
0x48E5B3: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48E5B8: lea      edi, [eax + 4]
0x48E5BB: mov      esi, 0x48e604
0x48E5C0: movsd    dword ptr es:[edi], dword ptr [esi]
0x48E5C1: movsb    byte ptr es:[edi], byte ptr [esi]
0x48E5C2: jmp      0x48e5fb
0x48E5C4: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48E5C9: cmp      dword ptr [eax], -0x61
0x48E5CC: je       0x48e5d8
0x48E5CE: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48E5D3: cmp      dword ptr [eax], -0x62
0x48E5D6: jne      0x48e5fb
0x48E5D8: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x48E5DD: mov      byte ptr [eax], 0
0x48E5E0: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48E5E5: mov      byte ptr [eax + 4], 0
0x48E5E9: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x48E5EE: cmp      dword ptr [eax], -0x61
0x48E5F1: jne      0x48e5fb
0x48E5F3: mov      eax, dword ptr [ebp - 4]
0x48E5F6: call     0x491f70
0x48E5FB: pop      edi
0x48E5FC: pop      esi
0x48E5FD: mov      esp, ebp
0x48E5FF: pop      ebp
0x48E600: ret      4
0x48E603: add      byte ptr [edi*4 + 0xd2b1f0], al
0x48E60A: add      byte ptr [eax], al
0x48E60C: push     ebp
0x48E60D: mov      ebp, esp
0x48E60F: add      esp, -0x14
0x48E612: push     ebx
0x48E613: mov      dword ptr [ebp - 0xc], ecx
0x48E616: mov      dword ptr [ebp - 8], edx
0x48E619: mov      dword ptr [ebp - 4], eax
0x48E61C: mov      eax, dword ptr [ebp - 0xc]
0x48E61F: call     0x403e88
0x48E624: mov      eax, dword ptr [ebp + 8]
0x48E627: call     0x403e88
0x48E62C: xor      eax, eax
0x48E62E: push     ebp
0x48E62F: push     0x48e741
0x48E634: push     dword ptr fs:[eax]
0x48E637: mov      dword ptr fs:[eax], esp
0x48E63A: xor      edx, edx
0x48E63C: mov      eax, dword ptr [ebp - 4]
0x48E63F: mov      eax, dword ptr [eax + 0x3c8]
0x48E645: call     0x42718c
0x48E64A: xor      edx, edx
0x48E64C: mov      eax, dword ptr [ebp - 4]
0x48E64F: mov      eax, dword ptr [eax + 0x3c8]
0x48E655: call     0x4271b0
0x48E65A: mov      eax, dword ptr [ebp - 8]
0x48E65D: mov      edx, dword ptr [ebp - 4]
0x48E660: mov      dword ptr [edx + 0x748], eax
0x48E666: mov      eax, dword ptr [ebp - 4]
0x48E669: add      eax, 0x744
0x48E66E: mov      edx, dword ptr [ebp - 0xc]
0x48E671: call     0x403aac
0x48E676: mov      eax, dword ptr [ebp - 4]
0x48E679: add      eax, 0x74c
0x48E67E: mov      edx, dword ptr [ebp + 8]
0x48E681: call     0x403aac
0x48E686: mov      dl, 1
0x48E688: mov      eax, dword ptr [ebp - 4]
0x48E68B: mov      eax, dword ptr [eax + 0x3c8]
0x48E691: call     0x458214
0x48E696: mov      edx, 0x1db
0x48E69B: mov      eax, dword ptr [ebp - 4]
0x48E69E: mov      eax, dword ptr [eax + 0x2cc]
0x48E6A4: call     0x42718c
0x48E6A9: xor      edx, edx
0x48E6AB: mov      eax, dword ptr [ebp - 4]
0x48E6AE: mov      eax, dword ptr [eax + 0x2cc]
0x48E6B4: call     0x4271b0
0x48E6B9: mov      eax, dword ptr [ebp - 4]
0x48E6BC: mov      eax, dword ptr [eax + 0x750]
0x48E6C2: mov      eax, dword ptr [eax + 8]
0x48E6C5: dec      eax
0x48E6C6: test     eax, eax
0x48E6C8: jl       0x48e6f9
0x48E6CA: inc      eax
0x48E6CB: mov      dword ptr [ebp - 0x14], eax
0x48E6CE: mov      dword ptr [ebp - 0x10], 0
0x48E6D5: mov      edx, dword ptr [ebp - 0x10]
0x48E6D8: mov      eax, dword ptr [ebp - 4]
0x48E6DB: mov      eax, dword ptr [eax + 0x750]
0x48E6E1: call     0x40f6a0
0x48E6E6: mov      edx, dword ptr [0x47fe58]
0x48E6EC: call     0x4046ec
0x48E6F1: inc      dword ptr [ebp - 0x10]
0x48E6F4: dec      dword ptr [ebp - 0x14]
0x48E6F7: jne      0x48e6d5
0x48E6F9: mov      eax, dword ptr [ebp - 4]
0x48E6FC: mov      eax, dword ptr [eax + 0x750]
0x48E702: mov      bx, 0xffff
0x48E706: call     0x403080
0x48E70B: mov      eax, dword ptr [ebp - 4]
0x48E70E: mov      byte ptr [eax + 0x754], 1
0x48E715: call     0x406154
0x48E71A: mov      edx, dword ptr [ebp - 4]
0x48E71D: mov      dword ptr [edx + 0x75c], eax
0x48E723: xor      eax, eax
0x48E725: pop      edx
0x48E726: pop      ecx
0x48E727: pop      ecx
0x48E728: mov      dword ptr fs:[eax], edx
0x48E72B: push     0x48e748
0x48E730: lea      eax, [ebp - 0xc]
0x48E733: call     0x403a58
0x48E738: lea      eax, [ebp + 8]
0x48E73B: call     0x403a58
0x48E740: ret      
0x48E741: jmp      0x403518
0x48E746: jmp      0x48e730
0x48E748: pop      ebx
0x48E749: mov      esp, ebp
0x48E74B: pop      ebp
0x48E74C: ret      4
0x48E74F: nop      
0x48E750: push     ebp
0x48E751: mov      ebp, esp
0x48E753: add      esp, -0xc
0x48E756: push     ebx
0x48E757: mov      dword ptr [ebp - 4], eax
0x48E75A: mov      eax, dword ptr [ebp - 4]
0x48E75D: call     0x48ea88
0x48E762: mov      eax, dword ptr [0x4a4158] ; *0x4A4158->0x4F917C
0x48E767: mov      eax, dword ptr [eax]
0x48E769: mov      eax, dword ptr [eax + 8]
0x48E76C: dec      eax
0x48E76D: test     eax, eax
0x48E76F: jl       0x48e79d
0x48E771: inc      eax
0x48E772: mov      dword ptr [ebp - 0xc], eax
0x48E775: mov      dword ptr [ebp - 8], 0
0x48E77C: mov      eax, dword ptr [0x4a4158] ; *0x4A4158->0x4F917C
0x48E781: mov      eax, dword ptr [eax]
0x48E783: mov      edx, dword ptr [ebp - 8]
0x48E786: call     0x40f6a0
0x48E78B: mov      edx, 0x34
0x48E790: call     0x4026cc
0x48E795: inc      dword ptr [ebp - 8]
0x48E798: dec      dword ptr [ebp - 0xc]
0x48E79B: jne      0x48e77c
0x48E79D: mov      eax, dword ptr [0x4a4158] ; *0x4A4158->0x4F917C
0x48E7A2: mov      eax, dword ptr [eax]
0x48E7A4: mov      bx, 0xffff
0x48E7A8: call     0x403080
0x48E7AD: mov      eax, dword ptr [ebp - 4]
0x48E7B0: mov      eax, dword ptr [eax + 0x764]
0x48E7B6: mov      eax, dword ptr [eax + 8]
0x48E7B9: dec      eax
0x48E7BA: test     eax, eax
0x48E7BC: jl       0x48e7ec
0x48E7BE: inc      eax
0x48E7BF: mov      dword ptr [ebp - 0xc], eax
0x48E7C2: mov      dword ptr [ebp - 8], 0
0x48E7C9: mov      eax, dword ptr [ebp - 4]
0x48E7CC: mov      eax, dword ptr [eax + 0x764]
0x48E7D2: mov      edx, dword ptr [ebp - 8]
0x48E7D5: call     0x40f6a0
0x48E7DA: mov      edx, 0x1c
0x48E7DF: call     0x4026cc
0x48E7E4: inc      dword ptr [ebp - 8]
0x48E7E7: dec      dword ptr [ebp - 0xc]
0x48E7EA: jne      0x48e7c9
0x48E7EC: mov      eax, dword ptr [ebp - 4]
0x48E7EF: mov      eax, dword ptr [eax + 0x764]
0x48E7F5: mov      bx, 0xffff
0x48E7F9: call     0x403080
0x48E7FE: mov      eax, dword ptr [ebp - 4]
0x48E801: mov      dword ptr [eax + 0x768], 0xffffffff
0x48E80B: mov      eax, dword ptr [ebp - 4]
0x48E80E: xor      edx, edx
0x48E810: mov      dword ptr [eax + 0x770], edx
0x48E816: mov      eax, dword ptr [ebp - 4]
0x48E819: mov      byte ptr [eax + 0x774], 0
0x48E820: mov      eax, dword ptr [ebp - 4]
0x48E823: mov      byte ptr [eax + 0x775], 0
0x48E82A: mov      eax, dword ptr [ebp - 4]
0x48E82D: mov      byte ptr [eax + 0x777], 0
0x48E834: mov      eax, dword ptr [ebp - 4]
0x48E837: mov      eax, dword ptr [eax + 0x3e4]
0x48E83D: xor      edx, edx
0x48E83F: call     0x458214
0x48E844: mov      eax, dword ptr [ebp - 4]
0x48E847: mov      eax, dword ptr [eax + 0x3d0]
0x48E84D: xor      edx, edx
0x48E84F: call     0x458214
0x48E854: pop      ebx
0x48E855: mov      esp, ebp
0x48E857: pop      ebp
0x48E858: ret      
0x48E859: lea      eax, [eax]
0x48E85C: push     ebp
0x48E85D: mov      ebp, esp
0x48E85F: push     ecx
0x48E860: mov      dword ptr [ebp - 4], eax
0x48E863: mov      eax, dword ptr [ebp - 4]
0x48E866: mov      dword ptr [eax + 0x768], 0xffffffff
0x48E870: xor      edx, edx
0x48E872: mov      eax, dword ptr [ebp - 4]
0x48E875: mov      eax, dword ptr [eax + 0x3c8]
0x48E87B: call     0x42718c
0x48E880: xor      edx, edx
0x48E882: mov      eax, dword ptr [ebp - 4]
0x48E885: mov      eax, dword ptr [eax + 0x3c8]
0x48E88B: call     0x4271b0
0x48E890: mov      dl, 1
0x48E892: mov      eax, dword ptr [ebp - 4]
0x48E895: mov      eax, dword ptr [eax + 0x3c8]
0x48E89B: call     0x458214
0x48E8A0: xor      edx, edx
0x48E8A2: mov      eax, dword ptr [ebp - 4]
0x48E8A5: mov      eax, dword ptr [eax + 0x3e4]
0x48E8AB: call     0x458214
0x48E8B0: xor      edx, edx
0x48E8B2: mov      eax, dword ptr [ebp - 4]
0x48E8B5: mov      eax, dword ptr [eax + 0x3d0]
0x48E8BB: call     0x42718c
0x48E8C0: mov      edx, 0xb0
0x48E8C5: mov      eax, dword ptr [ebp - 4]
0x48E8C8: mov      eax, dword ptr [eax + 0x3d0]
0x48E8CE: call     0x4271b0
0x48E8D3: mov      dl, 1
0x48E8D5: mov      eax, dword ptr [ebp - 4]
0x48E8D8: mov      eax, dword ptr [eax + 0x3d0]
0x48E8DE: call     0x458214
0x48E8E3: mov      eax, dword ptr [ebp - 4]
0x48E8E6: xor      edx, edx
0x48E8E8: mov      dword ptr [eax + 0x5fc], edx
0x48E8EE: mov      edx, 0x1db
0x48E8F3: mov      eax, dword ptr [ebp - 4]
0x48E8F6: mov      eax, dword ptr [eax + 0x2cc]
0x48E8FC: call     0x42718c
0x48E901: xor      edx, edx
0x48E903: mov      eax, dword ptr [ebp - 4]
0x48E906: mov      eax, dword ptr [eax + 0x2cc]
0x48E90C: call     0x4271b0
0x48E911: mov      dl, 1
0x48E913: mov      eax, dword ptr [ebp - 4]
0x48E916: mov      eax, dword ptr [eax + 0x2cc]
0x48E91C: call     0x458214
0x48E921: call     0x406154
0x48E926: mov      edx, dword ptr [ebp - 4]
0x48E929: mov      dword ptr [edx + 0x75c], eax
0x48E92F: pop      ecx
0x48E930: pop      ebp
0x48E931: ret      
0x48E932: mov      eax, eax
0x48E934: push     ebp
0x48E935: mov      ebp, esp
0x48E937: push     ecx
0x48E938: mov      dword ptr [ebp - 4], eax
0x48E93B: mov      edx, 0x104
0x48E940: mov      eax, dword ptr [ebp - 4]
0x48E943: mov      eax, dword ptr [eax + 0x3e4]
0x48E949: call     0x42718c
0x48E94E: mov      edx, 0xb0
0x48E953: mov      eax, dword ptr [ebp - 4]
0x48E956: mov      eax, dword ptr [eax + 0x3e4]
0x48E95C: call     0x4271b0
0x48E961: mov      dl, 1
0x48E963: mov      eax, dword ptr [ebp - 4]
0x48E966: mov      eax, dword ptr [eax + 0x3e4]
0x48E96C: call     0x458214
0x48E971: xor      edx, edx
0x48E973: mov      eax, dword ptr [ebp - 4]
0x48E976: mov      eax, dword ptr [eax + 0x3d0]
0x48E97C: call     0x458214
0x48E981: mov      edx, 0x1db
0x48E986: mov      eax, dword ptr [ebp - 4]
0x48E989: mov      eax, dword ptr [eax + 0x2cc]
0x48E98F: call     0x42718c
0x48E994: xor      edx, edx
0x48E996: mov      eax, dword ptr [ebp - 4]
0x48E999: mov      eax, dword ptr [eax + 0x2cc]
0x48E99F: call     0x4271b0
0x48E9A4: mov      dl, 1
0x48E9A6: mov      eax, dword ptr [ebp - 4]
0x48E9A9: mov      eax, dword ptr [eax + 0x2cc]
0x48E9AF: call     0x458214
0x48E9B4: call     0x406154
0x48E9B9: mov      edx, dword ptr [ebp - 4]
0x48E9BC: mov      dword ptr [edx + 0x75c], eax
0x48E9C2: mov      eax, dword ptr [0x4a3fac] ; *0x4A3FAC->0x4F9378
0x48E9C7: call     0x403a58
0x48E9CC: pop      ecx
0x48E9CD: pop      ebp
0x48E9CE: ret      
0x48E9CF: nop      
0x48E9D0: push     ebp
0x48E9D1: mov      ebp, esp
0x48E9D3: add      esp, -0xc
0x48E9D6: push     ebx
0x48E9D7: mov      dword ptr [ebp - 4], eax
0x48E9DA: mov      eax, dword ptr [ebp - 4]
0x48E9DD: add      eax, 0x74c
0x48E9E2: call     0x403a58
0x48E9E7: xor      edx, edx
0x48E9E9: mov      eax, dword ptr [ebp - 4]
0x48E9EC: mov      eax, dword ptr [eax + 0x3c8]
0x48E9F2: call     0x458214
0x48E9F7: mov      eax, dword ptr [ebp - 4]
0x48E9FA: mov      eax, dword ptr [eax + 0x750]
0x48EA00: mov      eax, dword ptr [eax + 8]
0x48EA03: dec      eax
0x48EA04: test     eax, eax
0x48EA06: jl       0x48ea37
0x48EA08: inc      eax
0x48EA09: mov      dword ptr [ebp - 0xc], eax
0x48EA0C: mov      dword ptr [ebp - 8], 0
0x48EA13: mov      edx, dword ptr [ebp - 8]
0x48EA16: mov      eax, dword ptr [ebp - 4]
0x48EA19: mov      eax, dword ptr [eax + 0x750]
0x48EA1F: call     0x40f6a0
0x48EA24: mov      edx, dword ptr [0x47fe58]
0x48EA2A: call     0x4046ec
0x48EA2F: inc      dword ptr [ebp - 8]
0x48EA32: dec      dword ptr [ebp - 0xc]
0x48EA35: jne      0x48ea13
0x48EA37: mov      eax, dword ptr [ebp - 4]
0x48EA3A: mov      eax, dword ptr [eax + 0x750]
0x48EA40: mov      bx, 0xffff
0x48EA44: call     0x403080
0x48EA49: xor      edx, edx
0x48EA4B: mov      eax, dword ptr [ebp - 4]
0x48EA4E: mov      eax, dword ptr [eax + 0x2cc]
0x48EA54: call     0x42718c
0x48EA59: xor      edx, edx
0x48EA5B: mov      eax, dword ptr [ebp - 4]
0x48EA5E: mov      eax, dword ptr [eax + 0x2cc]
0x48EA64: call     0x4271b0
0x48EA69: xor      edx, edx
0x48EA6B: mov      eax, dword ptr [ebp - 4]
0x48EA6E: mov      eax, dword ptr [eax + 0x3d0]
0x48EA74: call     0x458214
0x48EA79: mov      eax, dword ptr [ebp - 4]
0x48EA7C: call     0x48ea88
0x48EA81: pop      ebx
0x48EA82: mov      esp, ebp
0x48EA84: pop      ebp
0x48EA85: ret      
0x48EA86: mov      eax, eax
0x48EA88: push     ebp
0x48EA89: mov      ebp, esp
0x48EA8B: push     ecx
0x48EA8C: mov      dword ptr [ebp - 4], eax
0x48EA8F: xor      edx, edx
0x48EA91: mov      eax, dword ptr [ebp - 4]
0x48EA94: mov      eax, dword ptr [eax + 0x3e4]
0x48EA9A: call     0x458214
0x48EA9F: mov      eax, dword ptr [0x4a3e2c] ; *0x4A3E2C->0x4F92D4
0x48EAA4: cmp      byte ptr [eax], 0
0x48EAA7: je       0x48eab3
0x48EAA9: mov      eax, dword ptr [0x4a3e2c] ; *0x4A3E2C->0x4F92D4
0x48EAAE: call     0x49f874
0x48EAB3: mov      eax, dword ptr [0x4a3e2c] ; *0x4A3E2C->0x4F92D4
0x48EAB8: mov      byte ptr [eax], 0
0x48EABB: pop      ecx
0x48EABC: pop      ebp
0x48EABD: ret      
0x48EABE: mov      eax, eax
0x48EAC0: push     ebp
0x48EAC1: mov      ebp, esp
0x48EAC3: add      esp, -0x58
0x48EAC6: push     ebx
0x48EAC7: push     esi
0x48EAC8: push     edi
0x48EAC9: xor      ebx, ebx
0x48EACB: mov      dword ptr [ebp - 0x54], ebx
0x48EACE: mov      dword ptr [ebp - 0x14], ebx
0x48EAD1: mov      dword ptr [ebp - 0x18], ebx
0x48EAD4: mov      dword ptr [ebp - 0x1c], ebx
0x48EAD7: mov      dword ptr [ebp - 0x20], ebx
0x48EADA: mov      dword ptr [ebp - 0x40], ebx
0x48EADD: mov      dword ptr [ebp - 0x24], ebx
0x48EAE0: mov      dword ptr [ebp - 0xc], ecx
0x48EAE3: mov      dword ptr [ebp - 8], edx
0x48EAE6: mov      dword ptr [ebp - 4], eax
0x48EAE9: xor      eax, eax
0x48EAEB: push     ebp
0x48EAEC: push     0x48ef4d
0x48EAF1: push     dword ptr fs:[eax]
0x48EAF4: mov      dword ptr fs:[eax], esp
0x48EAF7: mov      eax, dword ptr [ebp - 8]
0x48EAFA: mov      edx, dword ptr [0x45689c]
0x48EB00: call     0x403034
0x48EB05: mov      dword ptr [ebp - 0x3c], eax
0x48EB08: mov      eax, dword ptr [ebp - 0x3c]
0x48EB0B: mov      edx, dword ptr [eax + 0x258]
0x48EB11: mov      eax, dword ptr [ebp - 0x3c]
0x48EB14: mov      eax, dword ptr [eax + 0x254]
0x48EB1A: call     0x45351c
0x48EB1F: mov      dword ptr [ebp - 0x10], eax
0x48EB22: cmp      dword ptr [ebp - 0x10], 0
0x48EB26: je       0x48eb65
0x48EB28: lea      edx, [ebp - 0x50]
0x48EB2B: mov      eax, dword ptr [ebp - 0x10]
0x48EB2E: call     0x44d8ec
0x48EB33: lea      eax, [ebp - 0x50]
0x48EB36: push     eax
0x48EB37: mov      eax, dword ptr [ebp - 0x10]
0x48EB3A: push     eax
0x48EB3B: push     1
0x48EB3D: mov      eax, dword ptr [ebp - 0x3c]
0x48EB40: mov      edx, dword ptr [eax + 0x34]
0x48EB43: mov      eax, dword ptr [ebp - 0x3c]
0x48EB46: call     0x456fdc
0x48EB4B: push     eax
0x48EB4C: mov      eax, dword ptr [ebp - 0x3c]
0x48EB4F: mov      edx, dword ptr [eax + 0x30]
0x48EB52: mov      eax, dword ptr [ebp - 0x3c]
0x48EB55: call     0x456f94
0x48EB5A: mov      edx, eax
0x48EB5C: mov      eax, dword ptr [ebp - 0xc]
0x48EB5F: pop      ecx
0x48EB60: call     0x44d6cc
0x48EB65: push     1
0x48EB67: mov      eax, dword ptr [ebp - 0xc]
0x48EB6A: call     0x44d8b4
0x48EB6F: call     0x41834c
0x48EB74: push     eax
0x48EB75: call     0x406434
0x48EB7A: mov      dword ptr [ebp - 0x28], 0x1e
0x48EB81: mov      dword ptr [ebp - 0x2c], 0x14
0x48EB88: lea      eax, [ebp - 0x14]
0x48EB8B: mov      edx, dword ptr [ebp - 4]
0x48EB8E: mov      edx, dword ptr [edx + 0x74c]
0x48EB94: call     0x403af0
0x48EB99: mov      byte ptr [ebp - 0x31], 0
0x48EB9D: cmp      dword ptr [ebp - 0x14], 0
0x48EBA1: je       0x48ef0b
0x48EBA7: push     0
0x48EBA9: lea      eax, [ebp - 0x54]
0x48EBAC: push     eax
0x48EBAD: mov      byte ptr [ebp - 0x58], 0x5c
0x48EBB1: lea      ecx, [ebp - 0x58]
0x48EBB4: lea      edx, [ebp - 0x18]
0x48EBB7: mov      eax, dword ptr [ebp - 0x14]
0x48EBBA: call     0x452530
0x48EBBF: mov      edx, dword ptr [ebp - 0x54]
0x48EBC2: lea      eax, [ebp - 0x14]
0x48EBC5: call     0x403af0
0x48EBCA: cmp      dword ptr [ebp - 0x18], 0
0x48EBCE: je       0x48ef02
0x48EBD4: xor      eax, eax
0x48EBD6: mov      dword ptr [ebp - 0x30], eax
0x48EBD9: lea      eax, [ebp - 0x1c]
0x48EBDC: call     0x403a58
0x48EBE1: jmp      0x48ee94
0x48EBE6: mov      eax, dword ptr [ebp - 0x18]
0x48EBE9: cmp      byte ptr [eax], 0x3c
0x48EBEC: je       0x48ec16
0x48EBEE: push     0
0x48EBF0: lea      eax, [ebp - 0x54]
0x48EBF3: push     eax
0x48EBF4: mov      byte ptr [ebp - 0x58], 0x3c
0x48EBF8: lea      ecx, [ebp - 0x58]
0x48EBFB: lea      edx, [ebp - 0x1c]
0x48EBFE: mov      eax, dword ptr [ebp - 0x18]
0x48EC01: call     0x452530
0x48EC06: mov      ecx, dword ptr [ebp - 0x54]
0x48EC09: lea      eax, [ebp - 0x18]
0x48EC0C: mov      edx, 0x48ef64 ; "<"
0x48EC11: call     0x403d20
0x48EC16: lea      eax, [ebp - 0x20]
0x48EC19: push     eax
0x48EC1A: lea      eax, [ebp - 0x54]
0x48EC1D: push     eax
0x48EC1E: mov      ecx, 0x48ef70 ; ">"
0x48EC23: mov      edx, 0x48ef64 ; "<"
0x48EC28: mov      eax, dword ptr [ebp - 0x18]
0x48EC2B: call     0x4529b8
0x48EC30: mov      edx, dword ptr [ebp - 0x54]
0x48EC33: lea      eax, [ebp - 0x18]
0x48EC36: call     0x403af0
0x48EC3B: cmp      dword ptr [ebp - 0x20], 0
0x48EC3F: je       0x48eca1
0x48EC41: lea      edx, [ebp - 0x54]
0x48EC44: mov      eax, dword ptr [ebp - 0x20]
0x48EC47: call     0x407a6c
0x48EC4C: mov      eax, dword ptr [ebp - 0x54]
0x48EC4F: mov      edx, 0x48ef7c ; "C"
0x48EC54: call     0x403de4
0x48EC59: jne      0x48ec64
0x48EC5B: mov      byte ptr [ebp - 0x31], 1
0x48EC5F: jmp      0x48ee94
0x48EC64: lea      edx, [ebp - 0x54]
0x48EC67: mov      eax, dword ptr [ebp - 0x20]
0x48EC6A: call     0x407a6c
0x48EC6F: mov      eax, dword ptr [ebp - 0x54]
0x48EC72: mov      edx, 0x48ef88 ; "/C"
0x48EC77: call     0x403de4
0x48EC7C: jne      0x48ec87
0x48EC7E: mov      byte ptr [ebp - 0x31], 0
0x48EC82: jmp      0x48ee94
0x48EC87: push     0
0x48EC89: lea      eax, [ebp - 0x24]
0x48EC8C: push     eax
0x48EC8D: mov      byte ptr [ebp - 0x58], 0x2f
0x48EC91: lea      ecx, [ebp - 0x58]
0x48EC94: lea      edx, [ebp - 0x20]
0x48EC97: mov      eax, dword ptr [ebp - 0x20]
0x48EC9A: call     0x452530
0x48EC9F: jmp      0x48ecc1
0x48ECA1: mov      eax, dword ptr [ebp - 4]
0x48ECA4: mov      eax, dword ptr [eax + 0x3d0]
0x48ECAA: xor      edx, edx
0x48ECAC: call     0x458214
0x48ECB1: mov      eax, dword ptr [ebp - 4]
0x48ECB4: mov      eax, dword ptr [eax + 0x3e4]
0x48ECBA: xor      edx, edx
0x48ECBC: call     0x458214
0x48ECC1: cmp      dword ptr [ebp - 0x1c], 0
0x48ECC5: je       0x48ed16
0x48ECC7: push     0xffffff
0x48ECCC: push     0
0x48ECCE: mov      eax, dword ptr [ebp - 0x1c]
0x48ECD1: push     eax
0x48ECD2: mov      eax, dword ptr [ebp - 0x3c]
0x48ECD5: mov      edx, dword ptr [eax + 0x34]
0x48ECD8: add      edx, dword ptr [ebp - 0x2c]
0x48ECDB: mov      eax, dword ptr [ebp - 0x3c]
0x48ECDE: call     0x456fdc
0x48ECE3: push     eax
0x48ECE4: mov      eax, dword ptr [ebp - 0x3c]
0x48ECE7: mov      edx, dword ptr [eax + 0x30]
0x48ECEA: add      edx, dword ptr [ebp - 0x28]
0x48ECED: add      edx, dword ptr [ebp - 0x30]
0x48ECF0: mov      eax, dword ptr [ebp - 0x3c]
0x48ECF3: call     0x456f94
0x48ECF8: mov      edx, eax
0x48ECFA: mov      eax, dword ptr [ebp - 0xc]
0x48ECFD: pop      ecx
0x48ECFE: call     0x4a09a0
0x48ED03: mov      eax, dword ptr [ebp - 0xc]
0x48ED06: call     0x44d8b4
0x48ED0B: mov      edx, dword ptr [ebp - 0x1c]
0x48ED0E: call     0x4181ac
0x48ED13: add      dword ptr [ebp - 0x30], eax
0x48ED16: cmp      dword ptr [ebp - 0x20], 0
0x48ED1A: je       0x48ee94
0x48ED20: mov      eax, dword ptr [ebp - 4]
0x48ED23: cmp      byte ptr [eax + 0x754], 0
0x48ED2A: je       0x48eda0
0x48ED2C: mov      edx, dword ptr [0x47fe58]
0x48ED32: mov      eax, 0x14
0x48ED37: call     0x4046d8
0x48ED3C: mov      dword ptr [ebp - 0x38], eax
0x48ED3F: mov      eax, dword ptr [ebp - 0x2c]
0x48ED42: add      eax, 0xe
0x48ED45: push     eax
0x48ED46: lea      eax, [ebp - 0x50]
0x48ED49: push     eax
0x48ED4A: mov      eax, dword ptr [ebp - 0xc]
0x48ED4D: call     0x44d8b4
0x48ED52: mov      edx, dword ptr [ebp - 0x20]
0x48ED55: call     0x4181ac
0x48ED5A: mov      ecx, eax
0x48ED5C: mov      eax, dword ptr [ebp - 0x28]
0x48ED5F: add      eax, dword ptr [ebp - 0x30]
0x48ED62: add      ecx, eax
0x48ED64: mov      eax, dword ptr [ebp - 0x28]
0x48ED67: add      eax, dword ptr [ebp - 0x30]
0x48ED6A: mov      edx, dword ptr [ebp - 0x2c]
0x48ED6D: call     0x40ecc0
0x48ED72: mov      eax, dword ptr [ebp - 0x38]
0x48ED75: lea      esi, [ebp - 0x50]
0x48ED78: mov      edi, eax
0x48ED7A: mov      ecx, 4
0x48ED7F: rep movsd dword ptr es:[edi], dword ptr [esi]
0x48ED81: mov      eax, dword ptr [ebp - 0x38]
0x48ED84: add      eax, 0x10
0x48ED87: mov      edx, dword ptr [ebp - 0x24]
0x48ED8A: call     0x403aac
0x48ED8F: mov      eax, dword ptr [ebp - 4]
0x48ED92: mov      eax, dword ptr [eax + 0x750]
0x48ED98: mov      edx, dword ptr [ebp - 0x38]
0x48ED9B: call     0x40f588
0x48EDA0: mov      eax, dword ptr [ebp - 0xc]
0x48EDA3: call     0x44d8b4
0x48EDA8: mov      eax, dword ptr [eax + 0xc]
0x48EDAB: call     0x4176b0
0x48EDB0: or       al, byte ptr [0x48ef8c]
0x48EDB6: push     eax
0x48EDB7: mov      eax, dword ptr [ebp - 0xc]
0x48EDBA: call     0x44d8b4
0x48EDBF: mov      eax, dword ptr [eax + 0xc]
0x48EDC2: pop      edx
0x48EDC3: call     0x4176bc
0x48EDC8: mov      eax, dword ptr [ebp - 4]
0x48EDCB: mov      eax, dword ptr [eax + 0x758]
0x48EDD1: mov      edx, dword ptr [ebp - 0x24]
0x48EDD4: call     0x403de4
0x48EDD9: jne      0x48ee19
0x48EDDB: push     0xff
0x48EDE0: push     0
0x48EDE2: mov      eax, dword ptr [ebp - 0x20]
0x48EDE5: push     eax
0x48EDE6: mov      eax, dword ptr [ebp - 0x3c]
0x48EDE9: mov      edx, dword ptr [eax + 0x34]
0x48EDEC: add      edx, dword ptr [ebp - 0x2c]
0x48EDEF: mov      eax, dword ptr [ebp - 0x3c]
0x48EDF2: call     0x456fdc
0x48EDF7: push     eax
0x48EDF8: mov      eax, dword ptr [ebp - 0x3c]
0x48EDFB: mov      edx, dword ptr [eax + 0x30]
0x48EDFE: add      edx, dword ptr [ebp - 0x28]
0x48EE01: add      edx, dword ptr [ebp - 0x30]
0x48EE04: mov      eax, dword ptr [ebp - 0x3c]
0x48EE07: call     0x456f94
0x48EE0C: mov      edx, eax
0x48EE0E: mov      eax, dword ptr [ebp - 0xc]
0x48EE11: pop      ecx
0x48EE12: call     0x4a09a0
0x48EE17: jmp      0x48ee55
0x48EE19: push     0xffff
0x48EE1E: push     0
0x48EE20: mov      eax, dword ptr [ebp - 0x20]
0x48EE23: push     eax
0x48EE24: mov      eax, dword ptr [ebp - 0x3c]
0x48EE27: mov      edx, dword ptr [eax + 0x34]
0x48EE2A: add      edx, dword ptr [ebp - 0x2c]
0x48EE2D: mov      eax, dword ptr [ebp - 0x3c]
0x48EE30: call     0x456fdc
0x48EE35: push     eax
0x48EE36: mov      eax, dword ptr [ebp - 0x3c]
0x48EE39: mov      edx, dword ptr [eax + 0x30]
0x48EE3C: add      edx, dword ptr [ebp - 0x28]
0x48EE3F: add      edx, dword ptr [ebp - 0x30]
0x48EE42: mov      eax, dword ptr [ebp - 0x3c]
0x48EE45: call     0x456f94
0x48EE4A: mov      edx, eax
0x48EE4C: mov      eax, dword ptr [ebp - 0xc]
0x48EE4F: pop      ecx
0x48EE50: call     0x4a09a0
0x48EE55: mov      eax, dword ptr [ebp - 0xc]
0x48EE58: call     0x44d8b4
0x48EE5D: mov      edx, dword ptr [ebp - 0x20]
0x48EE60: call     0x4181ac
0x48EE65: add      dword ptr [ebp - 0x30], eax
0x48EE68: mov      eax, dword ptr [ebp - 0xc]
0x48EE6B: call     0x44d8b4
0x48EE70: mov      eax, dword ptr [eax + 0xc]
0x48EE73: call     0x4176b0
0x48EE78: mov      dl, byte ptr [0x48ef8c]
0x48EE7E: not      edx
0x48EE80: and      dl, al
0x48EE82: push     edx
0x48EE83: mov      eax, dword ptr [ebp - 0xc]
0x48EE86: call     0x44d8b4
0x48EE8B: mov      eax, dword ptr [eax + 0xc]
0x48EE8E: pop      edx
0x48EE8F: call     0x4176bc
0x48EE94: mov      edx, dword ptr [ebp - 0x18]
0x48EE97: mov      eax, 0x48ef64 ; "<"
0x48EE9C: call     0x403f60
0x48EEA1: test     eax, eax
0x48EEA3: jle      0x48eec0
0x48EEA5: mov      edx, dword ptr [ebp - 0x18]
0x48EEA8: mov      eax, 0x48ef70 ; ">"
0x48EEAD: call     0x403f60
0x48EEB2: test     eax, eax
0x48EEB4: jle      0x48eec0
0x48EEB6: cmp      dword ptr [ebp - 0x18], 0
0x48EEBA: jne      0x48ebe6
0x48EEC0: cmp      dword ptr [ebp - 0x18], 0
0x48EEC4: je       0x48ef02
0x48EEC6: push     0xffffff
0x48EECB: push     0
0x48EECD: mov      eax, dword ptr [ebp - 0x18]
0x48EED0: push     eax
0x48EED1: mov      eax, dword ptr [ebp - 0x3c]
0x48EED4: mov      edx, dword ptr [eax + 0x34]
0x48EED7: add      edx, dword ptr [ebp - 0x2c]
0x48EEDA: mov      eax, dword ptr [ebp - 0x3c]
0x48EEDD: call     0x456fdc
0x48EEE2: push     eax
0x48EEE3: mov      eax, dword ptr [ebp - 0x3c]
0x48EEE6: mov      edx, dword ptr [eax + 0x30]
0x48EEE9: add      edx, dword ptr [ebp - 0x28]
0x48EEEC: add      edx, dword ptr [ebp - 0x30]
0x48EEEF: mov      eax, dword ptr [ebp - 0x3c]
0x48EEF2: call     0x456f94
0x48EEF7: mov      edx, eax
0x48EEF9: mov      eax, dword ptr [ebp - 0xc]
0x48EEFC: pop      ecx
0x48EEFD: call     0x4a09a0
0x48EF02: add      dword ptr [ebp - 0x2c], 0x10
0x48EF06: jmp      0x48eb9d
0x48EF0B: mov      eax, dword ptr [ebp - 0xc]
0x48EF0E: call     0x44d8b4
0x48EF13: call     0x44d104
0x48EF18: mov      eax, dword ptr [ebp - 4]
0x48EF1B: mov      byte ptr [eax + 0x754], 0
0x48EF22: xor      eax, eax
0x48EF24: pop      edx
0x48EF25: pop      ecx
0x48EF26: pop      ecx
0x48EF27: mov      dword ptr fs:[eax], edx
0x48EF2A: push     0x48ef54
0x48EF2F: lea      eax, [ebp - 0x54]
0x48EF32: call     0x403a58
0x48EF37: lea      eax, [ebp - 0x40]
0x48EF3A: call     0x403a58
0x48EF3F: lea      eax, [ebp - 0x24]
0x48EF42: mov      edx, 5
0x48EF47: call     0x403a7c
0x48EF4C: ret      
0x48EF4D: jmp      0x403518
0x48EF52: jmp      0x48ef2f
0x48EF54: pop      edi
0x48EF55: pop      esi
0x48EF56: pop      ebx
0x48EF57: mov      esp, ebp
0x48EF59: pop      ebp
0x48EF5A: ret      
0x48EF5B: add      bh, bh
0x48EF5F: inc      dword ptr [ecx]
0x48EF61: add      byte ptr [eax], al
0x48EF63: add      byte ptr [eax + eax], bh
0x48EF66: add      byte ptr [eax], al
0x48EF70: [STR len=1] ">"
0x48EF7C: [STR len=1] "C"
0x48EF88: [STR len=2] "/C"
0x48EF8C: add      al, 0
0x48EF8E: add      byte ptr [eax], al
0x48EF90: push     ebp
0x48EF91: mov      ebp, esp
0x48EF93: add      esp, -0xc
0x48EF96: mov      dword ptr [ebp - 0xc], ecx
0x48EF99: mov      dword ptr [ebp - 8], edx
0x48EF9C: mov      dword ptr [ebp - 4], eax
0x48EF9F: mov      eax, dword ptr [ebp - 4]
0x48EFA2: call     0x48e9d0
0x48EFA7: mov      esp, ebp
0x48EFA9: pop      ebp
0x48EFAA: ret      4
0x48EFAD: lea      eax, [eax]
0x48EFB0: push     ebp
0x48EFB1: mov      ebp, esp
0x48EFB3: add      esp, -8
0x48EFB6: mov      dword ptr [ebp - 4], eax
0x48EFB9: mov      eax, dword ptr [ebp + 8]
0x48EFBC: mov      eax, dword ptr [eax - 4]
0x48EFBF: mov      eax, dword ptr [eax + 0x3d0]
0x48EFC5: mov      edx, dword ptr [eax + 0x30]
0x48EFC8: add      edx, dword ptr [ebp - 4]
0x48EFCB: mov      eax, dword ptr [ebp + 8]
0x48EFCE: mov      eax, dword ptr [eax - 4]
0x48EFD1: mov      eax, dword ptr [eax + 0x3d0]
0x48EFD7: call     0x456f94
0x48EFDC: mov      dword ptr [ebp - 8], eax
0x48EFDF: mov      eax, dword ptr [ebp - 8]
0x48EFE2: pop      ecx
0x48EFE3: pop      ecx
0x48EFE4: pop      ebp
0x48EFE5: ret      
0x48EFE6: mov      eax, eax
0x48EFE8: push     ebp
0x48EFE9: mov      ebp, esp
0x48EFEB: add      esp, -8
0x48EFEE: mov      dword ptr [ebp - 4], eax
0x48EFF1: mov      eax, dword ptr [ebp + 8]
0x48EFF4: mov      eax, dword ptr [eax - 4]
0x48EFF7: mov      eax, dword ptr [eax + 0x3d0]
0x48EFFD: mov      edx, dword ptr [eax + 0x34]
0x48F000: add      edx, dword ptr [ebp - 4]
0x48F003: mov      eax, dword ptr [ebp + 8]
0x48F006: mov      eax, dword ptr [eax - 4]
0x48F009: mov      eax, dword ptr [eax + 0x3d0]
0x48F00F: call     0x456fdc
0x48F014: mov      dword ptr [ebp - 8], eax
0x48F017: mov      eax, dword ptr [ebp - 8]
0x48F01A: pop      ecx
0x48F01B: pop      ecx
0x48F01C: pop      ebp
0x48F01D: ret      
0x48F01E: mov      eax, eax
0x48F020: push     ebp
0x48F021: mov      ebp, esp
0x48F023: add      esp, -0x54
0x48F026: push     ebx
0x48F027: xor      ebx, ebx
0x48F029: mov      dword ptr [ebp - 0x4c], ebx
0x48F02C: mov      dword ptr [ebp - 0x50], ebx
0x48F02F: mov      dword ptr [ebp - 0x54], ebx
0x48F032: mov      dword ptr [ebp - 0x24], ebx
0x48F035: mov      dword ptr [ebp - 8], ecx
0x48F038: mov      dword ptr [ebp - 0x34], edx
0x48F03B: mov      dword ptr [ebp - 4], eax
0x48F03E: xor      eax, eax
0x48F040: push     ebp
0x48F041: push     0x48f5bf
0x48F046: push     dword ptr fs:[eax]
0x48F049: mov      dword ptr fs:[eax], esp
0x48F04C: mov      eax, dword ptr [ebp - 8]
0x48F04F: call     0x44d8b4
0x48F054: mov      dword ptr [ebp - 0x28], eax
0x48F057: mov      eax, dword ptr [ebp - 4]
0x48F05A: mov      eax, dword ptr [eax + 0x3d0]
0x48F060: mov      dword ptr [ebp - 0x2c], eax
0x48F063: mov      eax, dword ptr [ebp - 0x2c]
0x48F066: mov      edx, dword ptr [eax + 0x258]
0x48F06C: mov      eax, dword ptr [ebp - 4]
0x48F06F: mov      eax, dword ptr [eax + 0x3d0]
0x48F075: mov      eax, dword ptr [eax + 0x254]
0x48F07B: call     0x45351c
0x48F080: mov      dword ptr [ebp - 0x1c], eax
0x48F083: cmp      dword ptr [ebp - 0x1c], 0
0x48F087: je       0x48f0c6
0x48F089: lea      edx, [ebp - 0x48]
0x48F08C: mov      eax, dword ptr [ebp - 0x1c]
0x48F08F: call     0x44d8ec
0x48F094: lea      eax, [ebp - 0x48]
0x48F097: push     eax
0x48F098: mov      eax, dword ptr [ebp - 0x1c]
0x48F09B: push     eax
0x48F09C: push     1
0x48F09E: mov      eax, dword ptr [ebp - 0x2c]
0x48F0A1: mov      edx, dword ptr [eax + 0x34]
0x48F0A4: mov      eax, dword ptr [ebp - 0x2c]
0x48F0A7: call     0x456fdc
0x48F0AC: push     eax
0x48F0AD: mov      eax, dword ptr [ebp - 0x2c]
0x48F0B0: mov      edx, dword ptr [eax + 0x30]
0x48F0B3: mov      eax, dword ptr [ebp - 0x2c]
0x48F0B6: call     0x456f94
0x48F0BB: mov      edx, eax
0x48F0BD: mov      eax, dword ptr [ebp - 8]
0x48F0C0: pop      ecx
0x48F0C1: call     0x44d6cc
0x48F0C6: push     1
0x48F0C8: mov      eax, dword ptr [ebp - 8]
0x48F0CB: call     0x44d8b4
0x48F0D0: call     0x41834c
0x48F0D5: push     eax
0x48F0D6: call     0x406434
0x48F0DB: push     1
0x48F0DD: mov      eax, dword ptr [ebp - 0x28]
0x48F0E0: call     0x41834c
0x48F0E5: push     eax
0x48F0E6: call     0x406434
0x48F0EB: mov      edx, 0xffffff
0x48F0F0: mov      eax, dword ptr [ebp - 0x28]
0x48F0F3: mov      eax, dword ptr [eax + 0xc]
0x48F0F6: call     0x4173fc
0x48F0FB: mov      eax, dword ptr [ebp - 4]
0x48F0FE: cmp      byte ptr [eax + 0x775], 0
0x48F105: jne      0x48f374
0x48F10B: push     0x48f5d4 ; "物品列表"
0x48F110: push     ebp
0x48F111: mov      eax, 0xb
0x48F116: call     0x48efe8
0x48F11B: pop      ecx
0x48F11C: push     eax
0x48F11D: push     ebp
0x48F11E: mov      eax, 0x13
0x48F123: call     0x48efb0
0x48F128: pop      ecx
0x48F129: mov      edx, eax
0x48F12B: mov      eax, dword ptr [ebp - 0x28]
0x48F12E: pop      ecx
0x48F12F: call     0x4180dc
0x48F134: push     0x48f5e8 ; "价格"
0x48F139: push     ebp
0x48F13A: mov      eax, 0xb
0x48F13F: call     0x48efe8
0x48F144: pop      ecx
0x48F145: push     eax
0x48F146: push     ebp
0x48F147: mov      eax, 0x9c
0x48F14C: call     0x48efb0
0x48F151: pop      ecx
0x48F152: mov      edx, eax
0x48F154: mov      eax, dword ptr [ebp - 0x28]
0x48F157: pop      ecx
0x48F158: call     0x4180dc
0x48F15D: push     0x48f5f8 ; "持久"
0x48F162: push     ebp
0x48F163: mov      eax, 0xb
0x48F168: call     0x48efe8
0x48F16D: pop      ecx
0x48F16E: push     eax
0x48F16F: push     ebp
0x48F170: mov      eax, 0xf5
0x48F175: call     0x48efb0
0x48F17A: pop      ecx
0x48F17B: mov      edx, eax
0x48F17D: mov      eax, dword ptr [ebp - 0x28]
0x48F180: pop      ecx
0x48F181: call     0x4180dc
0x48F186: mov      dword ptr [ebp - 0x10], 0xd
0x48F18D: mov      eax, dword ptr [ebp - 4]
0x48F190: mov      eax, dword ptr [eax + 0x764]
0x48F196: mov      edx, dword ptr [eax + 8]
0x48F199: mov      eax, dword ptr [ebp - 4]
0x48F19C: sub      edx, dword ptr [eax + 0x5fc]
0x48F1A2: mov      eax, 0xa
0x48F1A7: call     0x452b90
0x48F1AC: mov      dword ptr [ebp - 0x18], eax
0x48F1AF: mov      eax, dword ptr [ebp - 4]
0x48F1B2: mov      eax, dword ptr [eax + 0x5fc]
0x48F1B8: mov      edx, dword ptr [ebp - 4]
0x48F1BB: mov      edx, dword ptr [edx + 0x5fc]
0x48F1C1: add      edx, dword ptr [ebp - 0x18]
0x48F1C4: dec      edx
0x48F1C5: sub      edx, eax
0x48F1C7: jl       0x48f594
0x48F1CD: inc      edx
0x48F1CE: mov      dword ptr [ebp - 0x30], edx
0x48F1D1: mov      dword ptr [ebp - 0xc], eax
0x48F1D4: mov      eax, dword ptr [ebp - 4]
0x48F1D7: mov      edx, dword ptr [ebp - 0xc]
0x48F1DA: sub      edx, dword ptr [eax + 0x5fc]
0x48F1E0: mov      dword ptr [ebp - 0x14], edx
0x48F1E3: mov      eax, dword ptr [ebp - 4]
0x48F1E6: mov      eax, dword ptr [eax + 0x768]
0x48F1EC: cmp      eax, dword ptr [ebp - 0xc]
0x48F1EF: jne      0x48f230
0x48F1F1: mov      edx, 0xff
0x48F1F6: mov      eax, dword ptr [ebp - 0x28]
0x48F1F9: mov      eax, dword ptr [eax + 0xc]
0x48F1FC: call     0x4173fc
0x48F201: push     0x48f608 ; ""
0x48F206: push     ebp
0x48F207: mov      eax, dword ptr [ebp - 0x14]
0x48F20A: imul     dword ptr [ebp - 0x10]
0x48F20D: add      eax, 0x20
0x48F210: call     0x48efe8
0x48F215: pop      ecx
0x48F216: push     eax
0x48F217: push     ebp
0x48F218: mov      eax, 0xc
0x48F21D: call     0x48efb0
0x48F222: pop      ecx
0x48F223: mov      edx, eax
0x48F225: mov      eax, dword ptr [ebp - 0x28]
0x48F228: pop      ecx
0x48F229: call     0x4180dc
0x48F22E: jmp      0x48f240
0x48F230: mov      edx, 0xffffff
0x48F235: mov      eax, dword ptr [ebp - 0x28]
0x48F238: mov      eax, dword ptr [eax + 0xc]
0x48F23B: call     0x4173fc
0x48F240: mov      eax, dword ptr [ebp - 4]
0x48F243: mov      eax, dword ptr [eax + 0x764]
0x48F249: mov      edx, dword ptr [ebp - 0xc]
0x48F24C: call     0x40f6a0
0x48F251: mov      dword ptr [ebp - 0x20], eax
0x48F254: lea      eax, [ebp - 0x4c]
0x48F257: mov      edx, dword ptr [ebp - 0x20]
0x48F25A: call     0x403c78
0x48F25F: mov      eax, dword ptr [ebp - 0x4c]
0x48F262: push     eax
0x48F263: push     ebp
0x48F264: mov      eax, dword ptr [ebp - 0x14]
0x48F267: imul     dword ptr [ebp - 0x10]
0x48F26A: add      eax, 0x20
0x48F26D: call     0x48efe8
0x48F272: pop      ecx
0x48F273: push     eax
0x48F274: push     ebp
0x48F275: mov      eax, 0x13
0x48F27A: call     0x48efb0
0x48F27F: pop      ecx
0x48F280: mov      edx, eax
0x48F282: mov      eax, dword ptr [ebp - 0x28]
0x48F285: pop      ecx
0x48F286: call     0x4180dc
0x48F28B: mov      eax, dword ptr [ebp - 0x20]
0x48F28E: cmp      byte ptr [eax + 0xf], 1
0x48F292: jb       0x48f2c1
0x48F294: push     0x48f614 ; ""
0x48F299: push     ebp
0x48F29A: mov      eax, dword ptr [ebp - 0x14]
0x48F29D: imul     dword ptr [ebp - 0x10]
0x48F2A0: add      eax, 0x20
0x48F2A3: call     0x48efe8
0x48F2A8: pop      ecx
0x48F2A9: push     eax
0x48F2AA: push     ebp
0x48F2AB: mov      eax, 0x89
0x48F2B0: call     0x48efb0
0x48F2B5: pop      ecx
0x48F2B6: mov      edx, eax
0x48F2B8: mov      eax, dword ptr [ebp - 0x28]
0x48F2BB: pop      ecx
0x48F2BC: call     0x4180dc
0x48F2C1: lea      edx, [ebp - 0x4c]
0x48F2C4: mov      eax, dword ptr [ebp - 0x20]
0x48F2C7: mov      eax, dword ptr [eax + 0x10]
0x48F2CA: call     0x407d64
0x48F2CF: lea      eax, [ebp - 0x4c]
0x48F2D2: mov      edx, 0x48f620 ; "金币"
0x48F2D7: call     0x403cdc
0x48F2DC: mov      eax, dword ptr [ebp - 0x4c]
0x48F2DF: push     eax
0x48F2E0: push     ebp
0x48F2E1: mov      eax, dword ptr [ebp - 0x14]
0x48F2E4: imul     dword ptr [ebp - 0x10]
0x48F2E7: add      eax, 0x20
0x48F2EA: call     0x48efe8
0x48F2EF: pop      ecx
0x48F2F0: push     eax
0x48F2F1: push     ebp
0x48F2F2: mov      eax, 0x9c
0x48F2F7: call     0x48efb0
0x48F2FC: pop      ecx
0x48F2FD: mov      edx, eax
0x48F2FF: mov      eax, dword ptr [ebp - 0x28]
0x48F302: pop      ecx
0x48F303: call     0x4180dc
0x48F308: lea      eax, [ebp - 0x24]
0x48F30B: call     0x403a58
0x48F310: mov      eax, dword ptr [ebp - 0x20]
0x48F313: cmp      byte ptr [eax + 0x18], 0xff
0x48F317: jne      0x48f328
0x48F319: lea      eax, [ebp - 0x24]
0x48F31C: mov      edx, 0x48f630 ; "-"
0x48F321: call     0x403af0
0x48F326: jmp      0x48f363
0x48F328: lea      edx, [ebp - 0x4c]
0x48F32B: mov      eax, dword ptr [ebp - 0x20]
0x48F32E: movsx    eax, byte ptr [eax + 0x18]
0x48F332: call     0x407d64
0x48F337: mov      eax, dword ptr [ebp - 0x4c]
0x48F33A: push     eax
0x48F33B: push     ebp
0x48F33C: mov      eax, dword ptr [ebp - 0x14]
0x48F33F: imul     dword ptr [ebp - 0x10]
0x48F342: add      eax, 0x20
0x48F345: call     0x48efe8
0x48F34A: pop      ecx
0x48F34B: push     eax
0x48F34C: push     ebp
0x48F34D: mov      eax, 0xf5
0x48F352: call     0x48efb0
0x48F357: pop      ecx
0x48F358: mov      edx, eax
0x48F35A: mov      eax, dword ptr [ebp - 0x28]
0x48F35D: pop      ecx
0x48F35E: call     0x4180dc
0x48F363: inc      dword ptr [ebp - 0xc]
0x48F366: dec      dword ptr [ebp - 0x30]
0x48F369: jne      0x48f1d4
0x48F36F: jmp      0x48f594
0x48F374: push     0x48f63c ; "托管物品列表"
0x48F379: push     ebp
0x48F37A: mov      eax, 0xb
0x48F37F: call     0x48efe8
0x48F384: pop      ecx
0x48F385: push     eax
0x48F386: push     ebp
0x48F387: mov      eax, 0x13
0x48F38C: call     0x48efb0
0x48F391: pop      ecx
0x48F392: mov      edx, eax
0x48F394: mov      eax, dword ptr [ebp - 0x28]
0x48F397: pop      ecx
0x48F398: call     0x4180dc
0x48F39D: push     0x48f5f8 ; "持久"
0x48F3A2: push     ebp
0x48F3A3: mov      eax, 0xb
0x48F3A8: call     0x48efe8
0x48F3AD: pop      ecx
0x48F3AE: push     eax
0x48F3AF: push     ebp
0x48F3B0: mov      eax, 0x9c
0x48F3B5: call     0x48efb0
0x48F3BA: pop      ecx
0x48F3BB: mov      edx, eax
0x48F3BD: mov      eax, dword ptr [ebp - 0x28]
0x48F3C0: pop      ecx
0x48F3C1: call     0x4180dc
0x48F3C6: push     0
0x48F3C8: push     ebp
0x48F3C9: mov      eax, 0xb
0x48F3CE: call     0x48efe8
0x48F3D3: pop      ecx
0x48F3D4: push     eax
0x48F3D5: push     ebp
0x48F3D6: mov      eax, 0xf5
0x48F3DB: call     0x48efb0
0x48F3E0: pop      ecx
0x48F3E1: mov      edx, eax
0x48F3E3: mov      eax, dword ptr [ebp - 0x28]
0x48F3E6: pop      ecx
0x48F3E7: call     0x4180dc
0x48F3EC: mov      dword ptr [ebp - 0x10], 0xd
0x48F3F3: mov      eax, dword ptr [ebp - 4]
0x48F3F6: mov      eax, dword ptr [eax + 0x764]
0x48F3FC: mov      edx, dword ptr [eax + 8]
0x48F3FF: mov      eax, dword ptr [ebp - 4]
0x48F402: sub      edx, dword ptr [eax + 0x5fc]
0x48F408: mov      eax, 0xa
0x48F40D: call     0x452b90
0x48F412: mov      dword ptr [ebp - 0x18], eax
0x48F415: mov      eax, dword ptr [ebp - 4]
0x48F418: mov      eax, dword ptr [eax + 0x5fc]
0x48F41E: mov      edx, dword ptr [ebp - 4]
0x48F421: mov      edx, dword ptr [edx + 0x5fc]
0x48F427: add      edx, dword ptr [ebp - 0x18]
0x48F42A: dec      edx
0x48F42B: sub      edx, eax
0x48F42D: jl       0x48f594
0x48F433: inc      edx
0x48F434: mov      dword ptr [ebp - 0x30], edx
0x48F437: mov      dword ptr [ebp - 0xc], eax
0x48F43A: mov      eax, dword ptr [ebp - 4]
0x48F43D: mov      edx, dword ptr [ebp - 0xc]
0x48F440: sub      edx, dword ptr [eax + 0x5fc]
0x48F446: mov      dword ptr [ebp - 0x14], edx
0x48F449: mov      eax, dword ptr [ebp - 4]
0x48F44C: mov      eax, dword ptr [eax + 0x768]
0x48F452: cmp      eax, dword ptr [ebp - 0xc]
0x48F455: jne      0x48f496
0x48F457: mov      edx, 0xff
0x48F45C: mov      eax, dword ptr [ebp - 0x28]
0x48F45F: mov      eax, dword ptr [eax + 0xc]
0x48F462: call     0x4173fc
0x48F467: push     0x48f608 ; ""
0x48F46C: push     ebp
0x48F46D: mov      eax, dword ptr [ebp - 0x14]
0x48F470: imul     dword ptr [ebp - 0x10]
0x48F473: add      eax, 0x20
0x48F476: call     0x48efe8
0x48F47B: pop      ecx
0x48F47C: push     eax
0x48F47D: push     ebp
0x48F47E: mov      eax, 0xc
0x48F483: call     0x48efb0
0x48F488: pop      ecx
0x48F489: mov      edx, eax
0x48F48B: mov      eax, dword ptr [ebp - 0x28]
0x48F48E: pop      ecx
0x48F48F: call     0x4180dc
0x48F494: jmp      0x48f4a6
0x48F496: mov      edx, 0xffffff
0x48F49B: mov      eax, dword ptr [ebp - 0x28]
0x48F49E: mov      eax, dword ptr [eax + 0xc]
0x48F4A1: call     0x4173fc
0x48F4A6: mov      eax, dword ptr [ebp - 4]
0x48F4A9: mov      eax, dword ptr [eax + 0x764]
0x48F4AF: mov      edx, dword ptr [ebp - 0xc]
0x48F4B2: call     0x40f6a0
0x48F4B7: mov      dword ptr [ebp - 0x20], eax
0x48F4BA: lea      eax, [ebp - 0x4c]
0x48F4BD: mov      edx, dword ptr [ebp - 0x20]
0x48F4C0: call     0x403c78
0x48F4C5: mov      eax, dword ptr [ebp - 0x4c]
0x48F4C8: push     eax
0x48F4C9: push     ebp
0x48F4CA: mov      eax, dword ptr [ebp - 0x14]
0x48F4CD: imul     dword ptr [ebp - 0x10]
0x48F4D0: add      eax, 0x20
0x48F4D3: call     0x48efe8
0x48F4D8: pop      ecx
0x48F4D9: push     eax
0x48F4DA: push     ebp
0x48F4DB: mov      eax, 0x13
0x48F4E0: call     0x48efb0
0x48F4E5: pop      ecx
0x48F4E6: mov      edx, eax
0x48F4E8: mov      eax, dword ptr [ebp - 0x28]
0x48F4EB: pop      ecx
0x48F4EC: call     0x4180dc
0x48F4F1: mov      eax, dword ptr [ebp - 0x20]
0x48F4F4: cmp      byte ptr [eax + 0xf], 1
0x48F4F8: jb       0x48f527
0x48F4FA: push     0x48f614 ; ""
0x48F4FF: push     ebp
0x48F500: mov      eax, dword ptr [ebp - 0x14]
0x48F503: imul     dword ptr [ebp - 0x10]
0x48F506: add      eax, 0x20
0x48F509: call     0x48efe8
0x48F50E: pop      ecx
0x48F50F: push     eax
0x48F510: push     ebp
0x48F511: mov      eax, 0x89
0x48F516: call     0x48efb0
0x48F51B: pop      ecx
0x48F51C: mov      edx, eax
0x48F51E: mov      eax, dword ptr [ebp - 0x28]
0x48F521: pop      ecx
0x48F522: call     0x4180dc
0x48F527: lea      edx, [ebp - 0x50]
0x48F52A: mov      eax, dword ptr [ebp - 0x20]
0x48F52D: mov      eax, dword ptr [eax + 0x14]
0x48F530: call     0x407d64
0x48F535: push     dword ptr [ebp - 0x50]
0x48F538: push     0x48f654 ; "/"
0x48F53D: lea      edx, [ebp - 0x54]
0x48F540: mov      eax, dword ptr [ebp - 0x20]
0x48F543: movsx    eax, byte ptr [eax + 0x18]
0x48F547: call     0x407d64
0x48F54C: push     dword ptr [ebp - 0x54]
0x48F54F: lea      eax, [ebp - 0x4c]
0x48F552: mov      edx, 3
0x48F557: call     0x403d94
0x48F55C: mov      eax, dword ptr [ebp - 0x4c]
0x48F55F: push     eax
0x48F560: push     ebp
0x48F561: mov      eax, dword ptr [ebp - 0x14]
0x48F564: imul     dword ptr [ebp - 0x10]
0x48F567: add      eax, 0x20
0x48F56A: call     0x48efe8
0x48F56F: pop      ecx
0x48F570: push     eax
0x48F571: push     ebp
0x48F572: mov      eax, 0x9c
0x48F577: call     0x48efb0
0x48F57C: pop      ecx
0x48F57D: mov      edx, eax
0x48F57F: mov      eax, dword ptr [ebp - 0x28]
0x48F582: pop      ecx
0x48F583: call     0x4180dc
0x48F588: inc      dword ptr [ebp - 0xc]
0x48F58B: dec      dword ptr [ebp - 0x30]
0x48F58E: jne      0x48f43a
0x48F594: mov      eax, dword ptr [ebp - 0x28]
0x48F597: call     0x44d104
0x48F59C: xor      eax, eax
0x48F59E: pop      edx
0x48F59F: pop      ecx
0x48F5A0: pop      ecx
0x48F5A1: mov      dword ptr fs:[eax], edx
0x48F5A4: push     0x48f5c6
0x48F5A9: lea      eax, [ebp - 0x54]
0x48F5AC: mov      edx, 3
0x48F5B1: call     0x403a7c
0x48F5B6: lea      eax, [ebp - 0x24]
0x48F5B9: call     0x403a58
0x48F5BE: ret      
0x48F5BF: jmp      0x403518
0x48F5C4: jmp      0x48f5a9
0x48F5C6: pop      ebx
0x48F5C7: mov      esp, ebp
0x48F5C9: pop      ebp
0x48F5CA: ret      
0x48F5CB: add      bh, bh
0x48F5CF: dec      dword ptr [eax]
0x48F5D1: add      byte ptr [eax], al
0x48F5D3: add      dh, cl
0x48F5D5: out      dx, eax
0x48F5D7: mov      bh, 0xc1
0x48F5D9: sal      byte ptr [ecx + 0xed], 1
0x48F5DF: add      bh, bh
0x48F5E3: inc      dword ptr [eax + eax]
0x48F5E6: add      byte ptr [eax], al
0x48F5E8: mov      esp, 0xf1b8db
0x48F5ED: add      byte ptr [eax], al
0x48F5EF: add      bh, bh
0x48F5F3: inc      dword ptr [eax + eax]
0x48F5F6: add      byte ptr [eax], al
0x48F5F8: mov      bl, 0xd6
0x48F5FA: mov      esi, 0xc3
0x48F5FF: add      bh, bh
0x48F603: inc      dword ptr [ecx]
0x48F605: add      byte ptr [eax], al
0x48F607: add      byte ptr [edi], al
0x48F609: add      byte ptr [eax], al
0x48F60B: add      bh, bh
0x48F60F: inc      dword ptr [ecx]
0x48F611: add      byte ptr [eax], al
0x48F613: add      byte ptr [edi], bl
0x48F615: add      byte ptr [eax], al
0x48F617: add      bh, bh
0x48F61B: inc      dword ptr [eax + eax]
0x48F61E: add      byte ptr [eax], al
0x48F620: mov      ebp, 0xd2b1f0
0x48F625: add      byte ptr [eax], al
0x48F627: add      bh, bh
0x48F62B: inc      dword ptr [ecx]
0x48F62D: add      byte ptr [eax], al
0x48F62F: add      byte ptr [0xff000000], ch
0x48F637: dec      dword ptr [eax + eax]
0x48F63A: add      byte ptr [eax], al
0x48F63C: int      0xd0
0x48F63E: mov      ecx, 0xc6efcedc
0x48F643: mov      bh, 0xc1
0x48F645: sal      byte ptr [ecx + 0xed], 1
0x48F64B: add      bh, bh
0x48F64F: inc      dword ptr [ecx]
0x48F651: add      byte ptr [eax], al
0x48F653: add      byte ptr [edi], ch
0x48F655: add      byte ptr [eax], al
0x48F657: add      byte ptr [ebp - 0x75], dl
0x48F65A: in       al, dx
0x48F65B: add      esp, -0x38
0x48F65E: push     ebx
0x48F65F: push     esi
0x48F660: push     edi
0x48F661: xor      ebx, ebx
0x48F663: mov      dword ptr [ebp - 0x38], ebx
0x48F666: mov      dword ptr [ebp - 0x1c], ebx
0x48F669: mov      dword ptr [ebp - 0x20], ebx
0x48F66C: mov      dword ptr [ebp - 0x24], ebx
0x48F66F: mov      dword ptr [ebp - 0x28], ebx
0x48F672: mov      dword ptr [ebp - 0xc], ecx
0x48F675: mov      dword ptr [ebp - 8], edx
0x48F678: mov      dword ptr [ebp - 4], eax
0x48F67B: xor      eax, eax
0x48F67D: push     ebp
0x48F67E: push     0x48f9d0
0x48F683: push     dword ptr fs:[eax]
0x48F686: mov      dword ptr fs:[eax], esp
0x48F689: mov      eax, dword ptr [0x4a3fb8] ; *0x4A3FB8->0x4F7D8C
0x48F68E: mov      eax, dword ptr [eax]
0x48F690: call     0x479ebc
0x48F695: mov      edx, dword ptr [ebp - 0xc]
0x48F698: mov      eax, dword ptr [ebp - 4]
0x48F69B: mov      eax, dword ptr [eax + 0x3d0]
0x48F6A1: call     0x457024
0x48F6A6: mov      edx, dword ptr [ebp - 4]
0x48F6A9: mov      edx, dword ptr [edx + 0x3d0]
0x48F6AF: sub      eax, dword ptr [edx + 0x30]
0x48F6B2: mov      dword ptr [ebp - 0x10], eax
0x48F6B5: mov      edx, dword ptr [ebp + 8]
0x48F6B8: mov      eax, dword ptr [ebp - 4]
0x48F6BB: mov      eax, dword ptr [eax + 0x3d0]
0x48F6C1: call     0x45706c
0x48F6C6: mov      edx, dword ptr [ebp - 4]
0x48F6C9: mov      edx, dword ptr [edx + 0x3d0]
0x48F6CF: sub      eax, dword ptr [edx + 0x34]
0x48F6D2: mov      dword ptr [ebp - 0x14], eax
0x48F6D5: cmp      dword ptr [ebp - 0x10], 0xe
0x48F6D9: jl       0x48f72b
0x48F6DB: cmp      dword ptr [ebp - 0x10], 0x117
0x48F6E2: jg       0x48f72b
0x48F6E4: cmp      dword ptr [ebp - 0x14], 0x20
0x48F6E8: jl       0x48f72b
0x48F6EA: mov      eax, dword ptr [ebp - 0x14]
0x48F6ED: sub      eax, 0x20
0x48F6F0: mov      ecx, 0xd
0x48F6F5: cdq      
0x48F6F6: idiv     ecx
0x48F6F8: mov      edx, dword ptr [ebp - 4]
0x48F6FB: add      eax, dword ptr [edx + 0x5fc]
0x48F701: mov      dword ptr [ebp - 0x18], eax
0x48F704: mov      eax, dword ptr [ebp - 4]
0x48F707: mov      eax, dword ptr [eax + 0x764]
0x48F70D: mov      eax, dword ptr [eax + 8]
0x48F710: cmp      eax, dword ptr [ebp - 0x18]
0x48F713: jle      0x48f72b
0x48F715: mov      eax, 0x69
0x48F71A: call     0x495794
0x48F71F: mov      eax, dword ptr [ebp - 0x18]
0x48F722: mov      edx, dword ptr [ebp - 4]
0x48F725: mov      dword ptr [edx + 0x768], eax
0x48F72B: mov      eax, dword ptr [ebp - 4]
0x48F72E: cmp      byte ptr [eax + 0x775], 0
0x48F735: je       0x48f85c
0x48F73B: mov      eax, dword ptr [ebp - 4]
0x48F73E: cmp      dword ptr [eax + 0x768], 0
0x48F745: jl       0x48f9ad
0x48F74B: mov      eax, dword ptr [0x4a42c8] ; *0x4A42C8->0x4F9178
0x48F750: mov      eax, dword ptr [eax]
0x48F752: mov      eax, dword ptr [eax + 8]
0x48F755: mov      edx, dword ptr [ebp - 4]
0x48F758: cmp      eax, dword ptr [edx + 0x768]
0x48F75E: jle      0x48f9ad
0x48F764: mov      eax, dword ptr [0x4a42c8] ; *0x4A42C8->0x4F9178
0x48F769: mov      eax, dword ptr [eax]
0x48F76B: mov      edx, dword ptr [ebp - 4]
0x48F76E: mov      edx, dword ptr [edx + 0x768]
0x48F774: call     0x40f6a0
0x48F779: mov      edx, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48F77F: mov      esi, eax
0x48F781: mov      edi, edx
0x48F783: mov      ecx, 0xd
0x48F788: rep movsd dword ptr es:[edi], dword ptr [esi]
0x48F78A: lea      eax, [ebp - 0x24]
0x48F78D: push     eax
0x48F78E: lea      eax, [ebp - 0x28]
0x48F791: push     eax
0x48F792: lea      eax, [ebp - 0x29]
0x48F795: push     eax
0x48F796: push     0
0x48F798: lea      ecx, [ebp - 0x20]
0x48F79B: lea      edx, [ebp - 0x1c]
0x48F79E: mov      eax, dword ptr [ebp - 4]
0x48F7A1: call     0x48c3c8
0x48F7A6: cmp      dword ptr [ebp - 0x1c], 0
0x48F7AA: je       0x48f84f
0x48F7B0: mov      dword ptr [ebp - 0x10], 0xf0
0x48F7B7: mov      eax, dword ptr [ebp - 4]
0x48F7BA: mov      eax, dword ptr [eax + 0x768]
0x48F7C0: mov      edx, dword ptr [ebp - 4]
0x48F7C3: sub      eax, dword ptr [edx + 0x5fc]
0x48F7C9: imul     eax, eax, 0xd
0x48F7CC: add      eax, 0x20
0x48F7CF: mov      dword ptr [ebp - 0x14], eax
0x48F7D2: mov      eax, dword ptr [ebp - 8]
0x48F7D5: mov      edx, dword ptr [0x456468]
0x48F7DB: call     0x403034
0x48F7E0: mov      dword ptr [ebp - 0x30], eax
0x48F7E3: push     dword ptr [ebp - 0x1c]
0x48F7E6: push     dword ptr [ebp - 0x20]
0x48F7E9: push     0x48f9e8 ; "\"
0x48F7EE: push     dword ptr [ebp - 0x24]
0x48F7F1: push     0x48f9e8 ; "\"
0x48F7F6: push     dword ptr [ebp - 0x28]
0x48F7F9: lea      eax, [ebp - 0x38]
0x48F7FC: mov      edx, 6
0x48F801: call     0x403d94
0x48F806: mov      eax, dword ptr [ebp - 0x38]
0x48F809: push     eax
0x48F80A: push     0xffff
0x48F80F: push     0
0x48F811: mov      eax, dword ptr [ebp - 0x30]
0x48F814: mov      edx, dword ptr [eax + 0x34]
0x48F817: add      edx, dword ptr [ebp - 0x14]
0x48F81A: mov      eax, dword ptr [ebp - 4]
0x48F81D: mov      eax, dword ptr [eax + 0x3d0]
0x48F823: call     0x456fdc
0x48F828: push     eax
0x48F829: mov      eax, dword ptr [ebp - 0x30]
0x48F82C: mov      edx, dword ptr [eax + 0x30]
0x48F82F: add      edx, dword ptr [ebp - 0x10]
0x48F832: mov      eax, dword ptr [ebp - 4]
0x48F835: mov      eax, dword ptr [eax + 0x3d0]
0x48F83B: call     0x456f94
0x48F840: mov      edx, eax
0x48F842: mov      eax, dword ptr [0x4a3fb8] ; *0x4A3FB8->0x4F7D8C
0x48F847: mov      eax, dword ptr [eax]
0x48F849: pop      ecx
0x48F84A: call     0x479d34
0x48F84F: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48F854: mov      byte ptr [eax], 0
0x48F857: jmp      0x48f9ad
0x48F85C: mov      eax, dword ptr [ebp - 4]
0x48F85F: cmp      dword ptr [eax + 0x768], 0
0x48F866: jl       0x48f9ad
0x48F86C: mov      eax, dword ptr [0x4a4158] ; *0x4A4158->0x4F917C
0x48F871: mov      eax, dword ptr [eax]
0x48F873: mov      eax, dword ptr [eax + 8]
0x48F876: mov      edx, dword ptr [ebp - 4]
0x48F879: cmp      eax, dword ptr [edx + 0x768]
0x48F87F: jle      0x48f9ad
0x48F885: mov      eax, dword ptr [ebp - 4]
0x48F888: mov      edx, dword ptr [eax + 0x768]
0x48F88E: mov      eax, dword ptr [ebp - 4]
0x48F891: mov      eax, dword ptr [eax + 0x764]
0x48F897: call     0x40f6a0
0x48F89C: cmp      byte ptr [eax + 0xf], 0
0x48F8A0: jne      0x48f9ad
0x48F8A6: mov      eax, dword ptr [0x4a4158] ; *0x4A4158->0x4F917C
0x48F8AB: mov      eax, dword ptr [eax]
0x48F8AD: mov      edx, dword ptr [ebp - 4]
0x48F8B0: mov      edx, dword ptr [edx + 0x768]
0x48F8B6: call     0x40f6a0
0x48F8BB: mov      edx, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48F8C1: mov      esi, eax
0x48F8C3: mov      edi, edx
0x48F8C5: mov      ecx, 0xd
0x48F8CA: rep movsd dword ptr es:[edi], dword ptr [esi]
0x48F8CC: mov      eax, dword ptr [ebp - 4]
0x48F8CF: mov      byte ptr [eax + 0x776], 1
0x48F8D6: lea      eax, [ebp - 0x24]
0x48F8D9: push     eax
0x48F8DA: lea      eax, [ebp - 0x28]
0x48F8DD: push     eax
0x48F8DE: lea      eax, [ebp - 0x29]
0x48F8E1: push     eax
0x48F8E2: push     0
0x48F8E4: lea      ecx, [ebp - 0x20]
0x48F8E7: lea      edx, [ebp - 0x1c]
0x48F8EA: mov      eax, dword ptr [ebp - 4]
0x48F8ED: call     0x48c3c8
0x48F8F2: mov      eax, dword ptr [ebp - 4]
0x48F8F5: mov      byte ptr [eax + 0x776], 0
0x48F8FC: cmp      dword ptr [ebp - 0x1c], 0
0x48F900: je       0x48f9a5
0x48F906: mov      dword ptr [ebp - 0x10], 0xf0
0x48F90D: mov      eax, dword ptr [ebp - 4]
0x48F910: mov      eax, dword ptr [eax + 0x768]
0x48F916: mov      edx, dword ptr [ebp - 4]
0x48F919: sub      eax, dword ptr [edx + 0x5fc]
0x48F91F: imul     eax, eax, 0xd
0x48F922: add      eax, 0x20
0x48F925: mov      dword ptr [ebp - 0x14], eax
0x48F928: mov      eax, dword ptr [ebp - 8]
0x48F92B: mov      edx, dword ptr [0x456468]
0x48F931: call     0x403034
0x48F936: mov      dword ptr [ebp - 0x34], eax
0x48F939: push     dword ptr [ebp - 0x1c]
0x48F93C: push     dword ptr [ebp - 0x20]
0x48F93F: push     0x48f9e8 ; "\"
0x48F944: push     dword ptr [ebp - 0x24]
0x48F947: push     0x48f9e8 ; "\"
0x48F94C: push     dword ptr [ebp - 0x28]
0x48F94F: lea      eax, [ebp - 0x38]
0x48F952: mov      edx, 6
0x48F957: call     0x403d94
0x48F95C: mov      eax, dword ptr [ebp - 0x38]
0x48F95F: push     eax
0x48F960: push     0xffff
0x48F965: push     0
0x48F967: mov      eax, dword ptr [ebp - 0x34]
0x48F96A: mov      edx, dword ptr [eax + 0x34]
0x48F96D: add      edx, dword ptr [ebp - 0x14]
0x48F970: mov      eax, dword ptr [ebp - 4]
0x48F973: mov      eax, dword ptr [eax + 0x3d0]
0x48F979: call     0x456fdc
0x48F97E: push     eax
0x48F97F: mov      eax, dword ptr [ebp - 0x34]
0x48F982: mov      edx, dword ptr [eax + 0x30]
0x48F985: add      edx, dword ptr [ebp - 0x10]
0x48F988: mov      eax, dword ptr [ebp - 4]
0x48F98B: mov      eax, dword ptr [eax + 0x3d0]
0x48F991: call     0x456f94
0x48F996: mov      edx, eax
0x48F998: mov      eax, dword ptr [0x4a3fb8] ; *0x4A3FB8->0x4F7D8C
0x48F99D: mov      eax, dword ptr [eax]
0x48F99F: pop      ecx
0x48F9A0: call     0x479d34
0x48F9A5: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x48F9AA: mov      byte ptr [eax], 0
0x48F9AD: xor      eax, eax
0x48F9AF: pop      edx
0x48F9B0: pop      ecx
0x48F9B1: pop      ecx
0x48F9B2: mov      dword ptr fs:[eax], edx
0x48F9B5: push     0x48f9d7
0x48F9BA: lea      eax, [ebp - 0x38]
0x48F9BD: call     0x403a58
0x48F9C2: lea      eax, [ebp - 0x28]
0x48F9C5: mov      edx, 4
0x48F9CA: call     0x403a7c
0x48F9CF: ret      
0x48F9D0: jmp      0x403518
0x48F9D5: jmp      0x48f9ba
0x48F9D7: pop      edi
0x48F9D8: pop      esi
0x48F9D9: pop      ebx
0x48F9DA: mov      esp, ebp
0x48F9DC: pop      ebp
0x48F9DD: ret      4
0x48F9E8: [STR len=1] "\"
0x48F9EC: push     ebp
0x48F9ED: mov      ebp, esp
0x48F9EF: add      esp, -0x10
0x48F9F2: mov      byte ptr [ebp - 0xd], cl
0x48F9F5: mov      dword ptr [ebp - 0xc], edx
0x48F9F8: mov      dword ptr [ebp - 4], eax
0x48F9FB: mov      eax, dword ptr [ebp - 4]
0x48F9FE: mov      eax, dword ptr [eax + 0x3d0]
0x48FA04: mov      dword ptr [ebp - 8], eax
0x48FA07: mov      eax, dword ptr [ebp - 8]
0x48FA0A: mov      edx, dword ptr [eax + 0x30]
0x48FA0D: add      edx, 0xa
0x48FA10: mov      eax, dword ptr [ebp - 8]
0x48FA13: call     0x456f94
0x48FA18: cmp      eax, dword ptr [ebp + 0xc]
0x48FA1B: jg       0x48fa6b
0x48FA1D: mov      eax, dword ptr [ebp - 8]
0x48FA20: mov      edx, dword ptr [eax + 0x30]
0x48FA23: mov      eax, dword ptr [ebp - 8]
0x48FA26: add      edx, dword ptr [eax + 0x38]
0x48FA29: sub      edx, 0x14
0x48FA2C: mov      eax, dword ptr [ebp - 8]
0x48FA2F: call     0x456f94
0x48FA34: cmp      eax, dword ptr [ebp + 0xc]
0x48FA37: jl       0x48fa6b
0x48FA39: mov      eax, dword ptr [ebp - 8]
0x48FA3C: mov      edx, dword ptr [eax + 0x34]
0x48FA3F: add      edx, 0x1e
0x48FA42: mov      eax, dword ptr [ebp - 8]
0x48FA45: call     0x456fdc
0x48FA4A: cmp      eax, dword ptr [ebp + 8]
0x48FA4D: jg       0x48fa6b
0x48FA4F: mov      eax, dword ptr [ebp - 8]
0x48FA52: mov      edx, dword ptr [eax + 0x34]
0x48FA55: mov      eax, dword ptr [ebp - 8]
0x48FA58: add      edx, dword ptr [eax + 0x3c]
0x48FA5B: sub      edx, 0x32
0x48FA5E: mov      eax, dword ptr [ebp - 8]
0x48FA61: call     0x456fdc
0x48FA66: cmp      eax, dword ptr [ebp + 8]
0x48FA69: jge      0x48fa77
0x48FA6B: mov      eax, dword ptr [0x4a3fb8] ; *0x4A3FB8->0x4F7D8C
0x48FA70: mov      eax, dword ptr [eax]
0x48FA72: call     0x479ebc
0x48FA77: mov      esp, ebp
0x48FA79: pop      ebp
0x48FA7A: ret      8
0x48FA7D: lea      eax, [eax]
0x48FA80: push     ebp
0x48FA81: mov      ebp, esp
0x48FA83: add      esp, -0x14
0x48FA86: push     ebx
0x48FA87: xor      ebx, ebx
0x48FA89: mov      dword ptr [ebp - 0x14], ebx
0x48FA8C: mov      dword ptr [ebp - 0x10], ecx
0x48FA8F: mov      dword ptr [ebp - 0xc], edx
0x48FA92: mov      dword ptr [ebp - 4], eax
0x48FA95: xor      eax, eax
0x48FA97: push     ebp
0x48FA98: push     0x48fc03
0x48FA9D: push     dword ptr fs:[eax]
0x48FAA0: mov      dword ptr fs:[eax], esp
0x48FAA3: call     0x406154
0x48FAA8: mov      edx, dword ptr [ebp - 4]
0x48FAAB: cmp      eax, dword ptr [edx + 0x75c]
0x48FAB1: jb       0x48fbed
0x48FAB7: mov      eax, dword ptr [ebp - 4]
0x48FABA: cmp      dword ptr [eax + 0x768], 0
0x48FAC1: jl       0x48fbed
0x48FAC7: mov      eax, dword ptr [ebp - 4]
0x48FACA: mov      eax, dword ptr [eax + 0x764]
0x48FAD0: mov      eax, dword ptr [eax + 8]
0x48FAD3: mov      edx, dword ptr [ebp - 4]
0x48FAD6: cmp      eax, dword ptr [edx + 0x768]
0x48FADC: jle      0x48fbed
0x48FAE2: mov      eax, dword ptr [ebp - 4]
0x48FAE5: mov      edx, dword ptr [eax + 0x768]
0x48FAEB: mov      eax, dword ptr [ebp - 4]
0x48FAEE: mov      eax, dword ptr [eax + 0x764]
0x48FAF4: call     0x40f6a0
0x48FAF9: mov      dword ptr [ebp - 8], eax
0x48FAFC: call     0x406154
0x48FB01: add      eax, 0x1388
0x48FB06: mov      edx, dword ptr [ebp - 4]
0x48FB09: mov      dword ptr [edx + 0x75c], eax
0x48FB0F: mov      eax, dword ptr [ebp - 8]
0x48FB12: cmp      byte ptr [eax + 0xf], 0
0x48FB16: jbe      0x48fb5d
0x48FB18: lea      eax, [ebp - 0x14]
0x48FB1B: mov      edx, dword ptr [ebp - 8]
0x48FB1E: call     0x403c78
0x48FB23: mov      eax, dword ptr [ebp - 0x14]
0x48FB26: push     eax
0x48FB27: mov      edx, dword ptr [0x4a3dec] ; *0x4A3DEC->0x4F94DC
0x48FB2D: mov      edx, dword ptr [edx]
0x48FB2F: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48FB34: mov      eax, dword ptr [eax]
0x48FB36: xor      ecx, ecx
0x48FB38: call     0x4661a0
0x48FB3D: mov      eax, dword ptr [ebp - 4]
0x48FB40: xor      edx, edx
0x48FB42: mov      dword ptr [eax + 0x770], edx
0x48FB48: mov      eax, dword ptr [ebp - 4]
0x48FB4B: add      eax, 0x76c
0x48FB50: mov      edx, dword ptr [ebp - 8]
0x48FB53: call     0x403c78
0x48FB58: jmp      0x48fbed
0x48FB5D: mov      eax, dword ptr [ebp - 4]
0x48FB60: cmp      byte ptr [eax + 0x775], 0
0x48FB67: je       0x48fb94
0x48FB69: lea      eax, [ebp - 0x14]
0x48FB6C: mov      edx, dword ptr [ebp - 8]
0x48FB6F: call     0x403c78
0x48FB74: mov      eax, dword ptr [ebp - 0x14]
0x48FB77: push     eax
0x48FB78: mov      edx, dword ptr [0x4a3dec] ; *0x4A3DEC->0x4F94DC
0x48FB7E: mov      edx, dword ptr [edx]
0x48FB80: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48FB85: mov      eax, dword ptr [eax]
0x48FB87: mov      ecx, dword ptr [ebp - 8]
0x48FB8A: mov      ecx, dword ptr [ecx + 0x10]
0x48FB8D: call     0x4662f4
0x48FB92: jmp      0x48fbed
0x48FB94: mov      eax, dword ptr [ebp - 4]
0x48FB97: cmp      byte ptr [eax + 0x777], 0
0x48FB9E: je       0x48fbc4
0x48FBA0: lea      eax, [ebp - 0x14]
0x48FBA3: mov      edx, dword ptr [ebp - 8]
0x48FBA6: call     0x403c78
0x48FBAB: mov      ecx, dword ptr [ebp - 0x14]
0x48FBAE: mov      edx, dword ptr [0x4a3dec] ; *0x4A3DEC->0x4F94DC
0x48FBB4: mov      edx, dword ptr [edx]
0x48FBB6: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48FBBB: mov      eax, dword ptr [eax]
0x48FBBD: call     0x4663a0
0x48FBC2: jmp      0x48fbed
0x48FBC4: lea      eax, [ebp - 0x14]
0x48FBC7: mov      edx, dword ptr [ebp - 8]
0x48FBCA: call     0x403c78
0x48FBCF: mov      eax, dword ptr [ebp - 0x14]
0x48FBD2: push     eax
0x48FBD3: mov      edx, dword ptr [0x4a3dec] ; *0x4A3DEC->0x4F94DC
0x48FBD9: mov      edx, dword ptr [edx]
0x48FBDB: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48FBE0: mov      eax, dword ptr [eax]
0x48FBE2: mov      ecx, dword ptr [ebp - 8]
0x48FBE5: mov      ecx, dword ptr [ecx + 0x14]
0x48FBE8: call     0x466248
0x48FBED: xor      eax, eax
0x48FBEF: pop      edx
0x48FBF0: pop      ecx
0x48FBF1: pop      ecx
0x48FBF2: mov      dword ptr fs:[eax], edx
0x48FBF5: push     0x48fc0a
0x48FBFA: lea      eax, [ebp - 0x14]
0x48FBFD: call     0x403a58
0x48FC02: ret      
0x48FC03: jmp      0x403518
0x48FC08: jmp      0x48fbfa
0x48FC0A: pop      ebx
0x48FC0B: mov      esp, ebp
0x48FC0D: pop      ebp
0x48FC0E: ret      4
0x48FC11: lea      eax, [eax]
0x48FC14: push     ebp
0x48FC15: mov      ebp, esp
0x48FC17: add      esp, -0xc
0x48FC1A: mov      dword ptr [ebp - 0xc], ecx
0x48FC1D: mov      dword ptr [ebp - 8], edx
0x48FC20: mov      dword ptr [ebp - 4], eax
0x48FC23: mov      eax, dword ptr [ebp - 4]
0x48FC26: cmp      byte ptr [eax + 0x774], 0
0x48FC2D: jne      0x48fc5e
0x48FC2F: mov      eax, dword ptr [ebp - 4]
0x48FC32: cmp      dword ptr [eax + 0x5fc], 0
0x48FC39: jle      0x48fc45
0x48FC3B: mov      eax, dword ptr [ebp - 4]
0x48FC3E: sub      dword ptr [eax + 0x5fc], 9
0x48FC45: mov      eax, dword ptr [ebp - 4]
0x48FC48: cmp      dword ptr [eax + 0x5fc], 0
0x48FC4F: jge      0x48fcad
0x48FC51: mov      eax, dword ptr [ebp - 4]
0x48FC54: xor      edx, edx
0x48FC56: mov      dword ptr [eax + 0x5fc], edx
0x48FC5C: jmp      0x48fcad
0x48FC5E: mov      eax, dword ptr [ebp - 4]
0x48FC61: cmp      dword ptr [eax + 0x770], 0
0x48FC68: jle      0x48fcad
0x48FC6A: mov      eax, dword ptr [ebp - 4]
0x48FC6D: mov      edx, dword ptr [eax + 0x770]
0x48FC73: sub      edx, 0xa
0x48FC76: xor      eax, eax
0x48FC78: call     0x452bbc
0x48FC7D: mov      edx, dword ptr [ebp - 4]
0x48FC80: mov      dword ptr [edx + 0x770], eax
0x48FC86: mov      eax, dword ptr [ebp - 4]
0x48FC89: mov      eax, dword ptr [eax + 0x76c]
0x48FC8F: push     eax
0x48FC90: mov      edx, dword ptr [0x4a3dec] ; *0x4A3DEC->0x4F94DC
0x48FC96: mov      edx, dword ptr [edx]
0x48FC98: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48FC9D: mov      eax, dword ptr [eax]
0x48FC9F: mov      ecx, dword ptr [ebp - 4]
0x48FCA2: mov      ecx, dword ptr [ecx + 0x770]
0x48FCA8: call     0x4661a0
0x48FCAD: mov      esp, ebp
0x48FCAF: pop      ebp
0x48FCB0: ret      4
0x48FCB3: nop      
0x48FCB4: push     ebp
0x48FCB5: mov      ebp, esp
0x48FCB7: add      esp, -0xc
0x48FCBA: mov      dword ptr [ebp - 0xc], ecx
0x48FCBD: mov      dword ptr [ebp - 8], edx
0x48FCC0: mov      dword ptr [ebp - 4], eax
0x48FCC3: mov      eax, dword ptr [ebp - 4]
0x48FCC6: cmp      byte ptr [eax + 0x774], 0
0x48FCCD: jne      0x48fcf5
0x48FCCF: mov      eax, dword ptr [ebp - 4]
0x48FCD2: mov      eax, dword ptr [eax + 0x5fc]
0x48FCD8: add      eax, 0xa
0x48FCDB: mov      edx, dword ptr [ebp - 4]
0x48FCDE: mov      edx, dword ptr [edx + 0x764]
0x48FCE4: cmp      eax, dword ptr [edx + 8]
0x48FCE7: jge      0x48fd26
0x48FCE9: mov      eax, dword ptr [ebp - 4]
0x48FCEC: add      dword ptr [eax + 0x5fc], 9
0x48FCF3: jmp      0x48fd26
0x48FCF5: mov      eax, dword ptr [ebp - 4]
0x48FCF8: add      dword ptr [eax + 0x770], 0xa
0x48FCFF: mov      eax, dword ptr [ebp - 4]
0x48FD02: mov      eax, dword ptr [eax + 0x76c]
0x48FD08: push     eax
0x48FD09: mov      edx, dword ptr [0x4a3dec] ; *0x4A3DEC->0x4F94DC
0x48FD0F: mov      edx, dword ptr [edx]
0x48FD11: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48FD16: mov      eax, dword ptr [eax]
0x48FD18: mov      ecx, dword ptr [ebp - 4]
0x48FD1B: mov      ecx, dword ptr [ecx + 0x770]
0x48FD21: call     0x4661a0
0x48FD26: mov      esp, ebp
0x48FD28: pop      ebp
0x48FD29: ret      4
0x48FD2C: push     ebp
0x48FD2D: mov      ebp, esp
0x48FD2F: add      esp, -0x14
0x48FD32: mov      dword ptr [ebp - 8], edx
0x48FD35: mov      dword ptr [ebp - 4], eax
0x48FD38: mov      eax, dword ptr [ebp - 4]
0x48FD3B: mov      eax, dword ptr [eax + 0x764]
0x48FD41: mov      eax, dword ptr [eax + 8]
0x48FD44: dec      eax
0x48FD45: test     eax, eax
0x48FD47: jl       0x48fdf8
0x48FD4D: inc      eax
0x48FD4E: mov      dword ptr [ebp - 0x14], eax
0x48FD51: mov      dword ptr [ebp - 0xc], 0
0x48FD58: mov      edx, dword ptr [ebp - 0xc]
0x48FD5B: mov      eax, dword ptr [ebp - 4]
0x48FD5E: mov      eax, dword ptr [eax + 0x764]
0x48FD64: call     0x40f6a0
0x48FD69: mov      dword ptr [ebp - 0x10], eax
0x48FD6C: mov      eax, dword ptr [ebp - 0x10]
0x48FD6F: cmp      byte ptr [eax + 0x18], 0
0x48FD73: jl       0x48fdec
0x48FD75: mov      eax, dword ptr [ebp - 0x10]
0x48FD78: mov      eax, dword ptr [eax + 0x14]
0x48FD7B: cmp      eax, dword ptr [ebp - 8]
0x48FD7E: jne      0x48fdec
0x48FD80: mov      edx, 0x1c
0x48FD85: mov      eax, dword ptr [ebp - 0x10]
0x48FD88: call     0x4026cc
0x48FD8D: mov      edx, dword ptr [ebp - 0xc]
0x48FD90: mov      eax, dword ptr [ebp - 4]
0x48FD93: mov      eax, dword ptr [eax + 0x764]
0x48FD99: call     0x40f5c4
0x48FD9E: mov      eax, dword ptr [0x4a4158] ; *0x4A4158->0x4F917C
0x48FDA3: mov      eax, dword ptr [eax]
0x48FDA5: mov      eax, dword ptr [eax + 8]
0x48FDA8: cmp      eax, dword ptr [ebp - 0xc]
0x48FDAB: jle      0x48fdbc
0x48FDAD: mov      eax, dword ptr [0x4a4158] ; *0x4A4158->0x4F917C
0x48FDB2: mov      eax, dword ptr [eax]
0x48FDB4: mov      edx, dword ptr [ebp - 0xc]
0x48FDB7: call     0x40f5c4
0x48FDBC: mov      eax, dword ptr [ebp - 4]
0x48FDBF: mov      eax, dword ptr [eax + 0x764]
0x48FDC5: mov      eax, dword ptr [eax + 8]
0x48FDC8: dec      eax
0x48FDC9: mov      edx, dword ptr [ebp - 4]
0x48FDCC: cmp      eax, dword ptr [edx + 0x768]
0x48FDD2: jge      0x48fdf8
0x48FDD4: mov      eax, dword ptr [ebp - 4]
0x48FDD7: mov      eax, dword ptr [eax + 0x764]
0x48FDDD: mov      eax, dword ptr [eax + 8]
0x48FDE0: dec      eax
0x48FDE1: mov      edx, dword ptr [ebp - 4]
0x48FDE4: mov      dword ptr [edx + 0x768], eax
0x48FDEA: jmp      0x48fdf8
0x48FDEC: inc      dword ptr [ebp - 0xc]
0x48FDEF: dec      dword ptr [ebp - 0x14]
0x48FDF2: jne      0x48fd58
0x48FDF8: mov      esp, ebp
0x48FDFA: pop      ebp
0x48FDFB: ret      
0x48FDFC: push     ebp
0x48FDFD: mov      ebp, esp
0x48FDFF: add      esp, -0x14
0x48FE02: mov      dword ptr [ebp - 8], edx
0x48FE05: mov      dword ptr [ebp - 4], eax
0x48FE08: mov      eax, dword ptr [ebp - 4]
0x48FE0B: mov      eax, dword ptr [eax + 0x764]
0x48FE11: mov      eax, dword ptr [eax + 8]
0x48FE14: dec      eax
0x48FE15: test     eax, eax
0x48FE17: jl       0x48febf
0x48FE1D: inc      eax
0x48FE1E: mov      dword ptr [ebp - 0x14], eax
0x48FE21: mov      dword ptr [ebp - 0xc], 0
0x48FE28: mov      edx, dword ptr [ebp - 0xc]
0x48FE2B: mov      eax, dword ptr [ebp - 4]
0x48FE2E: mov      eax, dword ptr [eax + 0x764]
0x48FE34: call     0x40f6a0
0x48FE39: mov      dword ptr [ebp - 0x10], eax
0x48FE3C: mov      eax, dword ptr [ebp - 0x10]
0x48FE3F: mov      eax, dword ptr [eax + 0x10]
0x48FE42: cmp      eax, dword ptr [ebp - 8]
0x48FE45: jne      0x48feb3
0x48FE47: mov      edx, 0x1c
0x48FE4C: mov      eax, dword ptr [ebp - 0x10]
0x48FE4F: call     0x4026cc
0x48FE54: mov      edx, dword ptr [ebp - 0xc]
0x48FE57: mov      eax, dword ptr [ebp - 4]
0x48FE5A: mov      eax, dword ptr [eax + 0x764]
0x48FE60: call     0x40f5c4
0x48FE65: mov      eax, dword ptr [0x4a42c8] ; *0x4A42C8->0x4F9178
0x48FE6A: mov      eax, dword ptr [eax]
0x48FE6C: mov      eax, dword ptr [eax + 8]
0x48FE6F: cmp      eax, dword ptr [ebp - 0xc]
0x48FE72: jle      0x48fe83
0x48FE74: mov      eax, dword ptr [0x4a42c8] ; *0x4A42C8->0x4F9178
0x48FE79: mov      eax, dword ptr [eax]
0x48FE7B: mov      edx, dword ptr [ebp - 0xc]
0x48FE7E: call     0x40f5c4
0x48FE83: mov      eax, dword ptr [ebp - 4]
0x48FE86: mov      eax, dword ptr [eax + 0x764]
0x48FE8C: mov      eax, dword ptr [eax + 8]
0x48FE8F: dec      eax
0x48FE90: mov      edx, dword ptr [ebp - 4]
0x48FE93: cmp      eax, dword ptr [edx + 0x768]
0x48FE99: jge      0x48febf
0x48FE9B: mov      eax, dword ptr [ebp - 4]
0x48FE9E: mov      eax, dword ptr [eax + 0x764]
0x48FEA4: mov      eax, dword ptr [eax + 8]
0x48FEA7: dec      eax
0x48FEA8: mov      edx, dword ptr [ebp - 4]
0x48FEAB: mov      dword ptr [edx + 0x768], eax
0x48FEB1: jmp      0x48febf
0x48FEB3: inc      dword ptr [ebp - 0xc]
0x48FEB6: dec      dword ptr [ebp - 0x14]
0x48FEB9: jne      0x48fe28
0x48FEBF: mov      esp, ebp
0x48FEC1: pop      ebp
0x48FEC2: ret      
0x48FEC3: nop      
0x48FEC4: push     ebp
0x48FEC5: mov      ebp, esp
0x48FEC7: add      esp, -0xc
0x48FECA: mov      dword ptr [ebp - 0xc], ecx
0x48FECD: mov      dword ptr [ebp - 8], edx
0x48FED0: mov      dword ptr [ebp - 4], eax
0x48FED3: xor      edx, edx
0x48FED5: mov      eax, dword ptr [ebp - 4]
0x48FED8: mov      eax, dword ptr [eax + 0x3d0]
0x48FEDE: call     0x458214
0x48FEE3: mov      esp, ebp
0x48FEE5: pop      ebp
0x48FEE6: ret      4
0x48FEE9: lea      eax, [eax]
0x48FEEC: push     ebp
0x48FEED: mov      ebp, esp
0x48FEEF: add      esp, -0x24
0x48FEF2: mov      dword ptr [ebp - 8], ecx
0x48FEF5: mov      dword ptr [ebp - 0x24], edx
0x48FEF8: mov      dword ptr [ebp - 4], eax
0x48FEFB: call     0x406154
0x48FF00: mov      edx, dword ptr [ebp - 4]
0x48FF03: cmp      eax, dword ptr [edx + 0x75c]
0x48FF09: jb       0x490009
0x48FF0F: mov      eax, dword ptr [ebp - 4]
0x48FF12: mov      eax, dword ptr [eax + 0x3c8]
0x48FF18: mov      eax, dword ptr [eax + 0x30]
0x48FF1B: mov      dword ptr [ebp - 0x10], eax
0x48FF1E: mov      eax, dword ptr [ebp - 4]
0x48FF21: mov      eax, dword ptr [eax + 0x3c8]
0x48FF27: mov      eax, dword ptr [eax + 0x34]
0x48FF2A: mov      dword ptr [ebp - 0x14], eax
0x48FF2D: mov      eax, dword ptr [ebp - 4]
0x48FF30: mov      eax, dword ptr [eax + 0x3c8]
0x48FF36: mov      dword ptr [ebp - 0x1c], eax
0x48FF39: mov      eax, dword ptr [ebp - 4]
0x48FF3C: mov      eax, dword ptr [eax + 0x750]
0x48FF42: mov      eax, dword ptr [eax + 8]
0x48FF45: dec      eax
0x48FF46: test     eax, eax
0x48FF48: jl       0x490009
0x48FF4E: inc      eax
0x48FF4F: mov      dword ptr [ebp - 0x20], eax
0x48FF52: mov      dword ptr [ebp - 0xc], 0
0x48FF59: mov      eax, dword ptr [ebp - 4]
0x48FF5C: mov      eax, dword ptr [eax + 0x750]
0x48FF62: mov      edx, dword ptr [ebp - 0xc]
0x48FF65: call     0x40f6a0
0x48FF6A: mov      dword ptr [ebp - 0x18], eax
0x48FF6D: mov      edx, dword ptr [ebp - 0x10]
0x48FF70: mov      eax, dword ptr [ebp - 0x18]
0x48FF73: add      edx, dword ptr [eax]
0x48FF75: mov      eax, dword ptr [ebp - 0x1c]
0x48FF78: call     0x456f94
0x48FF7D: cmp      eax, dword ptr [ebp - 8]
0x48FF80: jg       0x48fffd
0x48FF82: mov      edx, dword ptr [ebp - 0x10]
0x48FF85: mov      eax, dword ptr [ebp - 0x18]
0x48FF88: add      edx, dword ptr [eax + 8]
0x48FF8B: mov      eax, dword ptr [ebp - 0x1c]
0x48FF8E: call     0x456f94
0x48FF93: cmp      eax, dword ptr [ebp - 8]
0x48FF96: jl       0x48fffd
0x48FF98: mov      edx, dword ptr [ebp - 0x14]
0x48FF9B: mov      eax, dword ptr [ebp - 0x18]
0x48FF9E: add      edx, dword ptr [eax + 4]
0x48FFA1: mov      eax, dword ptr [ebp - 0x1c]
0x48FFA4: call     0x456fdc
0x48FFA9: cmp      eax, dword ptr [ebp + 8]
0x48FFAC: jg       0x48fffd
0x48FFAE: mov      edx, dword ptr [ebp - 0x14]
0x48FFB1: mov      eax, dword ptr [ebp - 0x18]
0x48FFB4: add      edx, dword ptr [eax + 0xc]
0x48FFB7: mov      eax, dword ptr [ebp - 0x1c]
0x48FFBA: call     0x456fdc
0x48FFBF: cmp      eax, dword ptr [ebp + 8]
0x48FFC2: jl       0x48fffd
0x48FFC4: mov      eax, 0x69
0x48FFC9: call     0x495794
0x48FFCE: mov      edx, dword ptr [0x4a3dec] ; *0x4A3DEC->0x4F94DC
0x48FFD4: mov      edx, dword ptr [edx]
0x48FFD6: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x48FFDB: mov      eax, dword ptr [eax]
0x48FFDD: mov      ecx, dword ptr [ebp - 0x18]
0x48FFE0: mov      ecx, dword ptr [ecx + 0x10]
0x48FFE3: call     0x465ca8
0x48FFE8: call     0x406154
0x48FFED: add      eax, 0x1388
0x48FFF2: mov      edx, dword ptr [ebp - 4]
0x48FFF5: mov      dword ptr [edx + 0x75c], eax
0x48FFFB: jmp      0x490009
0x48FFFD: inc      dword ptr [ebp - 0xc]
0x490000: dec      dword ptr [ebp - 0x20]
0x490003: jne      0x48ff59
0x490009: mov      esp, ebp
0x49000B: pop      ebp
0x49000C: ret      4
0x49000F: nop      
0x490010: push     ebp
0x490011: mov      ebp, esp
0x490013: add      esp, -0x24
0x490016: mov      byte ptr [ebp - 0x21], cl
0x490019: mov      dword ptr [ebp - 0x20], edx
0x49001C: mov      dword ptr [ebp - 4], eax
0x49001F: call     0x406154
0x490024: mov      edx, dword ptr [ebp - 4]
0x490027: cmp      eax, dword ptr [edx + 0x75c]
0x49002D: jb       0x490116
0x490033: mov      eax, dword ptr [ebp - 4]
0x490036: add      eax, 0x758
0x49003B: call     0x403a58
0x490040: mov      eax, dword ptr [ebp - 4]
0x490043: mov      eax, dword ptr [eax + 0x3c8]
0x490049: mov      eax, dword ptr [eax + 0x30]
0x49004C: mov      dword ptr [ebp - 0xc], eax
0x49004F: mov      eax, dword ptr [ebp - 4]
0x490052: mov      eax, dword ptr [eax + 0x3c8]
0x490058: mov      eax, dword ptr [eax + 0x34]
0x49005B: mov      dword ptr [ebp - 0x10], eax
0x49005E: mov      eax, dword ptr [ebp - 4]
0x490061: mov      eax, dword ptr [eax + 0x3c8]
0x490067: mov      dword ptr [ebp - 0x18], eax
0x49006A: mov      eax, dword ptr [ebp - 4]
0x49006D: mov      eax, dword ptr [eax + 0x750]
0x490073: mov      eax, dword ptr [eax + 8]
0x490076: dec      eax
0x490077: test     eax, eax
0x490079: jl       0x490116
0x49007F: inc      eax
0x490080: mov      dword ptr [ebp - 0x1c], eax
0x490083: mov      dword ptr [ebp - 8], 0
0x49008A: mov      eax, dword ptr [ebp - 4]
0x49008D: mov      eax, dword ptr [eax + 0x750]
0x490093: mov      edx, dword ptr [ebp - 8]
0x490096: call     0x40f6a0
0x49009B: mov      dword ptr [ebp - 0x14], eax
0x49009E: mov      edx, dword ptr [ebp - 0xc]
0x4900A1: mov      eax, dword ptr [ebp - 0x14]
0x4900A4: add      edx, dword ptr [eax]
0x4900A6: mov      eax, dword ptr [ebp - 0x18]
0x4900A9: call     0x456f94
0x4900AE: cmp      eax, dword ptr [ebp + 0xc]
0x4900B1: jg       0x49010a
0x4900B3: mov      edx, dword ptr [ebp - 0xc]
0x4900B6: mov      eax, dword ptr [ebp - 0x14]
0x4900B9: add      edx, dword ptr [eax + 8]
0x4900BC: mov      eax, dword ptr [ebp - 0x18]
0x4900BF: call     0x456f94
0x4900C4: cmp      eax, dword ptr [ebp + 0xc]
0x4900C7: jl       0x49010a
0x4900C9: mov      edx, dword ptr [ebp - 0x10]
0x4900CC: mov      eax, dword ptr [ebp - 0x14]
0x4900CF: add      edx, dword ptr [eax + 4]
0x4900D2: mov      eax, dword ptr [ebp - 0x18]
0x4900D5: call     0x456fdc
0x4900DA: cmp      eax, dword ptr [ebp + 8]
0x4900DD: jg       0x49010a
0x4900DF: mov      edx, dword ptr [ebp - 0x10]
0x4900E2: mov      eax, dword ptr [ebp - 0x14]
0x4900E5: add      edx, dword ptr [eax + 0xc]
0x4900E8: mov      eax, dword ptr [ebp - 0x18]
0x4900EB: call     0x456fdc
0x4900F0: cmp      eax, dword ptr [ebp + 8]
0x4900F3: jl       0x49010a
0x4900F5: mov      eax, dword ptr [ebp - 4]
0x4900F8: add      eax, 0x758
0x4900FD: mov      edx, dword ptr [ebp - 0x14]
0x490100: mov      edx, dword ptr [edx + 0x10]
0x490103: call     0x403aac
0x490108: jmp      0x490116
0x49010A: inc      dword ptr [ebp - 8]
0x49010D: dec      dword ptr [ebp - 0x1c]
0x490110: jne      0x49008a
0x490116: mov      esp, ebp
0x490118: pop      ebp
0x490119: ret      0xc
0x49011C: push     ebp
0x49011D: mov      ebp, esp
0x49011F: add      esp, -0xc
0x490122: mov      byte ptr [ebp - 9], cl
0x490125: mov      dword ptr [ebp - 8], edx
0x490128: mov      dword ptr [ebp - 4], eax
0x49012B: mov      eax, dword ptr [ebp - 4]
0x49012E: add      eax, 0x758
0x490133: call     0x403a58
0x490138: mov      esp, ebp
0x49013A: pop      ebp
0x49013B: ret      0xc
0x49013E: mov      eax, eax
0x490140: push     ebp
0x490141: mov      ebp, esp
0x490143: add      esp, -0x30
0x490146: push     ebx
0x490147: xor      ebx, ebx
0x490149: mov      dword ptr [ebp - 0x30], ebx
0x49014C: mov      dword ptr [ebp - 0x10], ebx
0x49014F: mov      dword ptr [ebp - 8], ecx
0x490152: mov      dword ptr [ebp - 0x1c], edx
0x490155: mov      dword ptr [ebp - 4], eax
0x490158: xor      eax, eax
0x49015A: push     ebp
0x49015B: push     0x4902b3
0x490160: push     dword ptr fs:[eax]
0x490163: mov      dword ptr fs:[eax], esp
0x490166: mov      eax, dword ptr [ebp - 4]
0x490169: mov      eax, dword ptr [eax + 0x3e4]
0x49016F: mov      dword ptr [ebp - 0x14], eax
0x490172: mov      eax, dword ptr [ebp - 4]
0x490175: mov      eax, dword ptr [eax + 0x3d0]
0x49017B: mov      eax, dword ptr [eax + 0x254]
0x490181: mov      edx, dword ptr [ebp - 0x14]
0x490184: mov      edx, dword ptr [edx + 0x258]
0x49018A: call     0x45351c
0x49018F: mov      dword ptr [ebp - 0xc], eax
0x490192: cmp      dword ptr [ebp - 0xc], 0
0x490196: je       0x4901d5
0x490198: lea      edx, [ebp - 0x2c]
0x49019B: mov      eax, dword ptr [ebp - 0xc]
0x49019E: call     0x44d8ec
0x4901A3: lea      eax, [ebp - 0x2c]
0x4901A6: push     eax
0x4901A7: mov      eax, dword ptr [ebp - 0xc]
0x4901AA: push     eax
0x4901AB: push     1
0x4901AD: mov      eax, dword ptr [ebp - 0x14]
0x4901B0: mov      edx, dword ptr [eax + 0x34]
0x4901B3: mov      eax, dword ptr [ebp - 0x14]
0x4901B6: call     0x456fdc
0x4901BB: push     eax
0x4901BC: mov      eax, dword ptr [ebp - 0x14]
0x4901BF: mov      edx, dword ptr [eax + 0x30]
0x4901C2: mov      eax, dword ptr [ebp - 0x14]
0x4901C5: call     0x456f94
0x4901CA: mov      edx, eax
0x4901CC: mov      eax, dword ptr [ebp - 8]
0x4901CF: pop      ecx
0x4901D0: call     0x44d6cc
0x4901D5: mov      eax, dword ptr [ebp - 8]
0x4901D8: call     0x44d8b4
0x4901DD: mov      dword ptr [ebp - 0x18], eax
0x4901E0: push     1
0x4901E2: mov      eax, dword ptr [ebp - 0x18]
0x4901E5: call     0x41834c
0x4901EA: push     eax
0x4901EB: call     0x406434
0x4901F0: mov      edx, 0xffffff
0x4901F5: mov      eax, dword ptr [ebp - 0x18]
0x4901F8: mov      eax, dword ptr [eax + 0xc]
0x4901FB: call     0x4173fc
0x490200: lea      eax, [ebp - 0x10]
0x490203: call     0x403a58
0x490208: mov      eax, dword ptr [ebp - 4]
0x49020B: mov      al, byte ptr [eax + 0x760]
0x490211: sub      al, 1
0x490213: jb       0x49021d
0x490215: je       0x49022c
0x490217: dec      al
0x490219: je       0x49023b
0x49021B: jmp      0x490248
0x49021D: lea      eax, [ebp - 0x10]
0x490220: mov      edx, 0x4902c8 ; "卖: "
0x490225: call     0x403af0
0x49022A: jmp      0x490248
0x49022C: lea      eax, [ebp - 0x10]
0x49022F: mov      edx, 0x4902d8 ; "修理: "
0x490234: call     0x403af0
0x490239: jmp      0x490248
0x49023B: lea      eax, [ebp - 0x10]
0x49023E: mov      edx, 0x4902e8 ; " 托管物品"
0x490243: call     0x403af0
0x490248: mov      ecx, dword ptr [0x4a3fac] ; *0x4A3FAC->0x4F9378
0x49024E: mov      ecx, dword ptr [ecx]
0x490250: lea      eax, [ebp - 0x30]
0x490253: mov      edx, dword ptr [ebp - 0x10]
0x490256: call     0x403d20
0x49025B: mov      eax, dword ptr [ebp - 0x30]
0x49025E: push     eax
0x49025F: mov      eax, dword ptr [ebp - 0x14]
0x490262: mov      edx, dword ptr [eax + 0x34]
0x490265: add      edx, 6
0x490268: mov      eax, dword ptr [ebp - 0x14]
0x49026B: call     0x456fdc
0x490270: push     eax
0x490271: mov      eax, dword ptr [ebp - 0x14]
0x490274: mov      edx, dword ptr [eax + 0x30]
0x490277: add      edx, 8
0x49027A: mov      eax, dword ptr [ebp - 0x14]
0x49027D: call     0x456f94
0x490282: mov      edx, eax
0x490284: mov      eax, dword ptr [ebp - 0x18]
0x490287: pop      ecx
0x490288: call     0x4180dc
0x49028D: mov      eax, dword ptr [ebp - 0x18]
0x490290: call     0x44d104
0x490295: xor      eax, eax
0x490297: pop      edx
0x490298: pop      ecx
0x490299: pop      ecx
0x49029A: mov      dword ptr fs:[eax], edx
0x49029D: push     0x4902ba
0x4902A2: lea      eax, [ebp - 0x30]
0x4902A5: call     0x403a58
0x4902AA: lea      eax, [ebp - 0x10]
0x4902AD: call     0x403a58
0x4902B2: ret      
0x4902B3: jmp      0x403518
0x4902B8: jmp      0x4902a2
0x4902BA: pop      ebx
0x4902BB: mov      esp, ebp
0x4902BD: pop      ebp
0x4902BE: ret      
0x4902BF: add      bh, bh
0x4902C3: inc      dword ptr [eax + eax]
0x4902C6: add      byte ptr [eax], al
0x4902C8: ret      0x3af4
0x4902CB: and      byte ptr [eax], al
0x4902CD: add      byte ptr [eax], al
0x4902CF: add      bh, bh
0x4902D3: inc      dword ptr [esi]
0x4902D5: add      byte ptr [eax], al
0x4902D7: add      al, dl
0x4902D9: faddp    st(0)
0x4902DB: in       eax, dx
0x4902DC: cmp      ah, byte ptr [eax]
0x4902DE: add      byte ptr [eax], al
0x4902E8: [STR len=9] " 托管物品"
0x4902F4: push     ebp
0x4902F5: mov      ebp, esp
0x4902F7: add      esp, -0xc
0x4902FA: mov      dword ptr [ebp - 0xc], ecx
0x4902FD: mov      dword ptr [ebp - 8], edx
0x490300: mov      dword ptr [ebp - 4], eax
0x490303: mov      eax, dword ptr [ebp - 4]
0x490306: call     0x48ea88
0x49030B: mov      esp, ebp
0x49030D: pop      ebp
0x49030E: ret      4
0x490311: lea      eax, [eax]
0x490314: push     ebp
0x490315: mov      ebp, esp
0x490317: add      esp, -0x40
0x49031A: push     esi
0x49031B: push     edi
0x49031C: mov      dword ptr [ebp - 0xc], ecx
0x49031F: mov      dword ptr [ebp - 8], edx
0x490322: mov      dword ptr [ebp - 4], eax
0x490325: mov      eax, dword ptr [0x4a3fac] ; *0x4A3FAC->0x4F9378
0x49032A: call     0x403a58
0x49032F: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x490334: cmp      byte ptr [eax], 0
0x490337: jne      0x490388
0x490339: mov      eax, dword ptr [0x4a3e2c] ; *0x4A3E2C->0x4F92D4
0x49033E: cmp      byte ptr [eax], 0
0x490341: je       0x490457
0x490347: mov      eax, dword ptr [0x4a3e2c] ; *0x4A3E2C->0x4F92D4
0x49034C: call     0x495940
0x490351: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x490356: mov      byte ptr [eax], 1
0x490359: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x49035E: mov      dword ptr [eax], 0xffffff9d
0x490364: mov      eax, dword ptr [0x4a3e2c] ; *0x4A3E2C->0x4F92D4
0x490369: mov      edx, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x49036F: mov      esi, eax
0x490371: lea      edi, [edx + 4]
0x490374: mov      ecx, 0xd
0x490379: rep movsd dword ptr es:[edi], dword ptr [esi]
0x49037B: mov      eax, dword ptr [0x4a3e2c] ; *0x4A3E2C->0x4F92D4
0x490380: mov      byte ptr [eax], 0
0x490383: jmp      0x490457
0x490388: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x49038D: cmp      dword ptr [eax], -0x61
0x490390: je       0x490457
0x490396: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x49039B: cmp      dword ptr [eax], -0x62
0x49039E: je       0x490457
0x4903A4: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x4903A9: cmp      dword ptr [eax], 0
0x4903AC: jge      0x4903bc
0x4903AE: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x4903B3: cmp      dword ptr [eax], -0x63
0x4903B6: jne      0x490457
0x4903BC: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x4903C1: add      eax, 4
0x4903C4: call     0x495940
0x4903C9: mov      eax, dword ptr [0x4a3e2c] ; *0x4A3E2C->0x4F92D4
0x4903CE: cmp      byte ptr [eax], 0
0x4903D1: je       0x49041a
0x4903D3: mov      eax, dword ptr [0x4a3e2c] ; *0x4A3E2C->0x4F92D4
0x4903D8: mov      esi, eax
0x4903DA: lea      edi, [ebp - 0x40]
0x4903DD: mov      ecx, 0xd
0x4903E2: rep movsd dword ptr es:[edi], dword ptr [esi]
0x4903E4: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x4903E9: mov      edx, dword ptr [0x4a3e2c] ; *0x4A3E2C->0x4F92D4
0x4903EF: lea      esi, [eax + 4]
0x4903F2: mov      edi, edx
0x4903F4: mov      ecx, 0xd
0x4903F9: rep movsd dword ptr es:[edi], dword ptr [esi]
0x4903FB: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x490400: mov      dword ptr [eax], 0xffffff9d
0x490406: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x49040B: lea      edi, [eax + 4]
0x49040E: lea      esi, [ebp - 0x40]
0x490411: mov      ecx, 0xd
0x490416: rep movsd dword ptr es:[edi], dword ptr [esi]
0x490418: jmp      0x490442
0x49041A: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x49041F: mov      edx, dword ptr [0x4a3e2c] ; *0x4A3E2C->0x4F92D4
0x490425: lea      esi, [eax + 4]
0x490428: mov      edi, edx
0x49042A: mov      ecx, 0xd
0x49042F: rep movsd dword ptr es:[edi], dword ptr [esi]
0x490431: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x490436: mov      byte ptr [eax + 4], 0
0x49043A: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x49043F: mov      byte ptr [eax], 0
0x490442: mov      eax, dword ptr [0x4a3ff8] ; *0x4A3FF8->0x4F9370
0x490447: mov      byte ptr [eax], 1
0x49044A: call     0x406154
0x49044F: mov      edx, dword ptr [0x4a4320] ; *0x4A4320->0x4F9374
0x490455: mov      dword ptr [edx], eax
0x490457: pop      edi
0x490458: pop      esi
0x490459: mov      esp, ebp
0x49045B: pop      ebp
0x49045C: ret      4
0x49045F: nop      
0x490460: push     ebp
0x490461: mov      ebp, esp
0x490463: add      esp, -0x24
0x490466: mov      dword ptr [ebp - 8], ecx
0x490469: mov      dword ptr [ebp - 0x14], edx
0x49046C: mov      dword ptr [ebp - 4], eax
0x49046F: mov      eax, dword ptr [0x4a3e2c] ; *0x4A3E2C->0x4F92D4
0x490474: cmp      byte ptr [eax], 0
0x490477: je       0x490519
0x49047D: mov      edx, dword ptr [0x4a3e2c] ; *0x4A3E2C->0x4F92D4
0x490483: movzx    edx, word ptr [edx + 0x16]
0x490487: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x49048C: mov      eax, dword ptr [eax]
0x49048E: mov      eax, dword ptr [eax + 0x324]
0x490494: call     0x45351c
0x490499: mov      dword ptr [ebp - 0xc], eax
0x49049C: cmp      dword ptr [ebp - 0xc], 0
0x4904A0: je       0x490519
0x4904A2: mov      eax, dword ptr [ebp - 4]
0x4904A5: mov      eax, dword ptr [eax + 0x3f0]
0x4904AB: mov      dword ptr [ebp - 0x10], eax
0x4904AE: lea      edx, [ebp - 0x24]
0x4904B1: mov      eax, dword ptr [ebp - 0xc]
0x4904B4: call     0x44d8ec
0x4904B9: lea      eax, [ebp - 0x24]
0x4904BC: push     eax
0x4904BD: mov      eax, dword ptr [ebp - 0xc]
0x4904C0: push     eax
0x4904C1: push     1
0x4904C3: mov      eax, dword ptr [ebp - 0xc]
0x4904C6: call     0x44d944
0x4904CB: mov      edx, dword ptr [ebp - 0x10]
0x4904CE: mov      edx, dword ptr [edx + 0x3c]
0x4904D1: sub      edx, eax
0x4904D3: sar      edx, 1
0x4904D5: jns      0x4904da
0x4904D7: adc      edx, 0
0x4904DA: mov      eax, dword ptr [ebp - 0x10]
0x4904DD: add      edx, dword ptr [eax + 0x34]
0x4904E0: mov      eax, dword ptr [ebp - 0x10]
0x4904E3: call     0x456fdc
0x4904E8: push     eax
0x4904E9: mov      eax, dword ptr [ebp - 0xc]
0x4904EC: call     0x44dc60
0x4904F1: mov      edx, dword ptr [ebp - 0x10]
0x4904F4: mov      edx, dword ptr [edx + 0x38]
0x4904F7: sub      edx, eax
0x4904F9: sar      edx, 1
0x4904FB: jns      0x490500
0x4904FD: adc      edx, 0
0x490500: mov      eax, dword ptr [ebp - 0x10]
0x490503: add      edx, dword ptr [eax + 0x30]
0x490506: mov      eax, dword ptr [ebp - 0x10]
0x490509: call     0x456f94
0x49050E: mov      edx, eax
0x490510: mov      eax, dword ptr [ebp - 8]
0x490513: pop      ecx
0x490514: call     0x44d6cc
0x490519: mov      esp, ebp
0x49051B: pop      ebp
0x49051C: ret      
0x49051D: lea      eax, [eax]
0x490520: push     ebp
0x490521: mov      ebp, esp
0x490523: add      esp, -0xc
0x490526: push     esi
0x490527: push     edi
0x490528: mov      byte ptr [ebp - 9], cl
0x49052B: mov      dword ptr [ebp - 8], edx
0x49052E: mov      dword ptr [ebp - 4], eax
0x490531: mov      eax, dword ptr [0x4a3e2c] ; *0x4A3E2C->0x4F92D4
0x490536: mov      edx, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x49053C: mov      esi, eax
0x49053E: mov      edi, edx
0x490540: mov      ecx, 0xd
0x490545: rep movsd dword ptr es:[edi], dword ptr [esi]
0x490547: pop      edi
0x490548: pop      esi
0x490549: mov      esp, ebp
0x49054B: pop      ebp
0x49054C: ret      8
0x49054F: nop      
0x490550: push     ebp
0x490551: mov      ebp, esp
0x490553: add      esp, -0x10
0x490556: push     ebx
0x490557: push     esi
0x490558: push     edi
0x490559: xor      ebx, ebx
0x49055B: mov      dword ptr [ebp - 0x10], ebx
0x49055E: mov      dword ptr [ebp - 0xc], ecx
0x490561: mov      dword ptr [ebp - 8], edx
0x490564: mov      dword ptr [ebp - 4], eax
0x490567: xor      eax, eax
0x490569: push     ebp
0x49056A: push     0x49069b
0x49056F: push     dword ptr fs:[eax]
0x490572: mov      dword ptr fs:[eax], esp
0x490575: mov      eax, dword ptr [0x4a3e2c] ; *0x4A3E2C->0x4F92D4
0x49057A: cmp      byte ptr [eax], 0
0x49057D: jne      0x49058d
0x49057F: mov      eax, dword ptr [0x4a4114] ; *0x4A4114->0x4F9308
0x490584: cmp      byte ptr [eax], 0
0x490587: je       0x490685
0x49058D: call     0x406154
0x490592: mov      edx, dword ptr [ebp - 4]
0x490595: cmp      eax, dword ptr [edx + 0x75c]
0x49059B: jb       0x490685
0x4905A1: mov      eax, dword ptr [ebp - 4]
0x4905A4: mov      al, byte ptr [eax + 0x760]
0x4905AA: sub      al, 1
0x4905AC: jb       0x4905b9
0x4905AE: je       0x4905ea
0x4905B0: dec      al
0x4905B2: je       0x49061b
0x4905B4: jmp      0x49064a
0x4905B9: lea      eax, [ebp - 0x10]
0x4905BC: mov      edx, dword ptr [0x4a3e2c] ; *0x4A3E2C->0x4F92D4
0x4905C2: call     0x403c78
0x4905C7: mov      eax, dword ptr [ebp - 0x10]
0x4905CA: push     eax
0x4905CB: mov      ecx, dword ptr [0x4a3e2c] ; *0x4A3E2C->0x4F92D4
0x4905D1: mov      ecx, dword ptr [ecx + 0x2c]
0x4905D4: mov      edx, dword ptr [0x4a3dec] ; *0x4A3DEC->0x4F94DC
0x4905DA: mov      edx, dword ptr [edx]
0x4905DC: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4905E1: mov      eax, dword ptr [eax]
0x4905E3: call     0x465f9c
0x4905E8: jmp      0x49064a
0x4905EA: lea      eax, [ebp - 0x10]
0x4905ED: mov      edx, dword ptr [0x4a3e2c] ; *0x4A3E2C->0x4F92D4
0x4905F3: call     0x403c78
0x4905F8: mov      eax, dword ptr [ebp - 0x10]
0x4905FB: push     eax
0x4905FC: mov      ecx, dword ptr [0x4a3e2c] ; *0x4A3E2C->0x4F92D4
0x490602: mov      ecx, dword ptr [ecx + 0x2c]
0x490605: mov      edx, dword ptr [0x4a3dec] ; *0x4A3DEC->0x4F94DC
0x49060B: mov      edx, dword ptr [edx]
0x49060D: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x490612: mov      eax, dword ptr [eax]
0x490614: call     0x466048
0x490619: jmp      0x49064a
0x49061B: lea      eax, [ebp - 0x10]
0x49061E: mov      edx, dword ptr [0x4a3e2c] ; *0x4A3E2C->0x4F92D4
0x490624: call     0x403c78
0x490629: mov      eax, dword ptr [ebp - 0x10]
0x49062C: push     eax
0x49062D: mov      ecx, dword ptr [0x4a3e2c] ; *0x4A3E2C->0x4F92D4
0x490633: mov      ecx, dword ptr [ecx + 0x2c]
0x490636: mov      edx, dword ptr [0x4a3dec] ; *0x4A3DEC->0x4F94DC
0x49063C: mov      edx, dword ptr [edx]
0x49063E: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x490643: mov      eax, dword ptr [eax]
0x490645: call     0x4660f4
0x49064A: mov      eax, dword ptr [0x4a3e2c] ; *0x4A3E2C->0x4F92D4
0x49064F: mov      edx, dword ptr [0x4a4114] ; *0x4A4114->0x4F9308
0x490655: mov      esi, eax
0x490657: mov      edi, edx
0x490659: mov      ecx, 0xd
0x49065E: rep movsd dword ptr es:[edi], dword ptr [esi]
0x490660: mov      eax, dword ptr [0x4a3e2c] ; *0x4A3E2C->0x4F92D4
0x490665: mov      byte ptr [eax], 0
0x490668: call     0x406154
0x49066D: add      eax, 0x1388
0x490672: mov      edx, dword ptr [ebp - 4]
0x490675: mov      dword ptr [edx + 0x75c], eax
0x49067B: mov      eax, dword ptr [0x4a3fac] ; *0x4A3FAC->0x4F9378
0x490680: call     0x403a58
0x490685: xor      eax, eax
0x490687: pop      edx
0x490688: pop      ecx
0x490689: pop      ecx
0x49068A: mov      dword ptr fs:[eax], edx
0x49068D: push     0x4906a2
0x490692: lea      eax, [ebp - 0x10]
0x490695: call     0x403a58
0x49069A: ret      
0x49069B: jmp      0x403518
0x4906A0: jmp      0x490692
0x4906A2: pop      edi
0x4906A3: pop      esi
0x4906A4: pop      ebx
0x4906A5: mov      esp, ebp
0x4906A7: pop      ebp
0x4906A8: ret      4
0x4906AB: nop      
0x4906AC: push     ebp
0x4906AD: mov      ebp, esp
0x4906AF: add      esp, -0xc
0x4906B2: mov      dword ptr [ebp - 0xc], ecx
0x4906B5: mov      dword ptr [ebp - 8], edx
0x4906B8: mov      dword ptr [ebp - 4], eax
0x4906BB: mov      eax, dword ptr [ebp - 0xc]
0x4906BE: call     0x403e88
0x4906C3: xor      eax, eax
0x4906C5: push     ebp
0x4906C6: push     0x4907c8
0x4906CB: push     dword ptr fs:[eax]
0x4906CE: mov      dword ptr fs:[eax], esp
0x4906D1: mov      eax, dword ptr [ebp - 8]
0x4906D4: mov      edx, dword ptr [ebp - 4]
0x4906D7: mov      dword ptr [edx + 0x600], eax
0x4906DD: mov      eax, dword ptr [ebp - 4]
0x4906E0: add      eax, 0x608
0x4906E5: mov      edx, dword ptr [ebp - 0xc]
0x4906E8: call     0x403aac
0x4906ED: mov      eax, dword ptr [ebp + 8]
0x4906F0: movzx    eax, word ptr [eax]
0x4906F3: mov      edx, dword ptr [ebp - 4]
0x4906F6: mov      dword ptr [edx + 0x604], eax
0x4906FC: mov      eax, dword ptr [ebp - 4]
0x4906FF: mov      eax, dword ptr [eax + 0x408]
0x490705: mov      edx, 0x320
0x49070A: sub      edx, dword ptr [eax + 0x38]
0x49070D: sar      edx, 1
0x49070F: jns      0x490714
0x490711: adc      edx, 0
0x490714: mov      eax, dword ptr [ebp - 4]
0x490717: mov      eax, dword ptr [eax + 0x408]
0x49071D: call     0x42718c
0x490722: mov      eax, dword ptr [ebp - 4]
0x490725: mov      eax, dword ptr [eax + 0x408]
0x49072B: mov      edx, 0x258
0x490730: sub      edx, dword ptr [eax + 0x3c]
0x490733: sar      edx, 1
0x490735: jns      0x49073a
0x490737: adc      edx, 0
0x49073A: mov      eax, dword ptr [ebp - 4]
0x49073D: mov      eax, dword ptr [eax + 0x408]
0x490743: call     0x4271b0
0x490748: mov      eax, dword ptr [ebp - 4]
0x49074B: call     0x481cec
0x490750: mov      eax, dword ptr [ebp - 4]
0x490753: mov      eax, dword ptr [eax + 0x408]
0x490759: call     0x45843c
0x49075E: mov      eax, dword ptr [ebp - 4]
0x490761: mov      eax, dword ptr [eax + 0x408]
0x490767: cmp      byte ptr [eax + 0x24c], 0
0x49076E: je       0x49079a
0x490770: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x490775: mov      eax, dword ptr [eax]
0x490777: call     0x460c34
0x49077C: mov      eax, dword ptr [0x4a4110] ; *0x4A4110->0x4A57A4
0x490781: mov      eax, dword ptr [eax]
0x490783: call     0x443574
0x490788: mov      eax, dword ptr [0x4a4110] ; *0x4A4110->0x4A57A4
0x49078D: mov      eax, dword ptr [eax]
0x49078F: cmp      byte ptr [eax + 0x84], 0
0x490796: jne      0x4907b2
0x490798: jmp      0x49075e
0x49079A: mov      eax, dword ptr [ebp - 4]
0x49079D: call     0x481d88
0x4907A2: mov      eax, dword ptr [ebp + 8]
0x4907A5: mov      edx, dword ptr [ebp - 4]
0x4907A8: mov      dx, word ptr [edx + 0x604]
0x4907AF: mov      word ptr [eax], dx
0x4907B2: xor      eax, eax
0x4907B4: pop      edx
0x4907B5: pop      ecx
0x4907B6: pop      ecx
0x4907B7: mov      dword ptr fs:[eax], edx
0x4907BA: push     0x4907cf
0x4907BF: lea      eax, [ebp - 0xc]
0x4907C2: call     0x403a58
0x4907C7: ret      
0x4907C8: jmp      0x403518
0x4907CD: jmp      0x4907bf
0x4907CF: mov      esp, ebp
0x4907D1: pop      ebp
0x4907D2: ret      4
0x4907D5: lea      eax, [eax]
0x4907D8: push     ebp
0x4907D9: mov      ebp, esp
0x4907DB: add      esp, -0x2c
0x4907DE: push     ebx
0x4907DF: xor      ebx, ebx
0x4907E1: mov      dword ptr [ebp - 0x2c], ebx
0x4907E4: mov      dword ptr [ebp - 8], ecx
0x4907E7: mov      dword ptr [ebp - 0x18], edx
0x4907EA: mov      dword ptr [ebp - 4], eax
0x4907ED: xor      eax, eax
0x4907EF: push     ebp
0x4907F0: push     0x4908f5
0x4907F5: push     dword ptr fs:[eax]
0x4907F8: mov      dword ptr fs:[eax], esp
0x4907FB: mov      eax, dword ptr [ebp - 4]
0x4907FE: mov      eax, dword ptr [eax + 0x408]
0x490804: mov      dword ptr [ebp - 0x10], eax
0x490807: mov      eax, dword ptr [ebp - 0x10]
0x49080A: mov      edx, dword ptr [eax + 0x258]
0x490810: mov      eax, dword ptr [ebp - 0x10]
0x490813: mov      eax, dword ptr [eax + 0x254]
0x490819: call     0x45351c
0x49081E: mov      dword ptr [ebp - 0xc], eax
0x490821: cmp      dword ptr [ebp - 0xc], 0
0x490825: je       0x490864
0x490827: lea      edx, [ebp - 0x28]
0x49082A: mov      eax, dword ptr [ebp - 0xc]
0x49082D: call     0x44d8ec
0x490832: lea      eax, [ebp - 0x28]
0x490835: push     eax
0x490836: mov      eax, dword ptr [ebp - 0xc]
0x490839: push     eax
0x49083A: push     1
0x49083C: mov      eax, dword ptr [ebp - 0x10]
0x49083F: mov      edx, dword ptr [eax + 0x34]
0x490842: mov      eax, dword ptr [ebp - 0x10]
0x490845: call     0x456fdc
0x49084A: push     eax
0x49084B: mov      eax, dword ptr [ebp - 0x10]
0x49084E: mov      edx, dword ptr [eax + 0x30]
0x490851: mov      eax, dword ptr [ebp - 0x10]
0x490854: call     0x456f94
0x490859: mov      edx, eax
0x49085B: mov      eax, dword ptr [ebp - 8]
0x49085E: pop      ecx
0x49085F: call     0x44d6cc
0x490864: mov      eax, dword ptr [ebp - 8]
0x490867: call     0x44d8b4
0x49086C: mov      dword ptr [ebp - 0x14], eax
0x49086F: push     1
0x490871: mov      eax, dword ptr [ebp - 0x14]
0x490874: call     0x41834c
0x490879: push     eax
0x49087A: call     0x406434
0x49087F: mov      edx, 0xc0c0c0
0x490884: mov      eax, dword ptr [ebp - 0x14]
0x490887: mov      eax, dword ptr [eax + 0xc]
0x49088A: call     0x4173fc
0x49088F: mov      eax, dword ptr [ebp - 4]
0x490892: mov      edx, dword ptr [eax + 0x608]
0x490898: lea      eax, [ebp - 0x2c]
0x49089B: mov      ecx, 0x49090c ; "快捷键盘被设置为."
0x4908A0: call     0x403d20
0x4908A5: mov      eax, dword ptr [ebp - 0x2c]
0x4908A8: push     eax
0x4908A9: mov      eax, dword ptr [ebp - 0x10]
0x4908AC: mov      edx, dword ptr [eax + 0x34]
0x4908AF: add      edx, 0x26
0x4908B2: mov      eax, dword ptr [ebp - 0x10]
0x4908B5: call     0x456fdc
0x4908BA: push     eax
0x4908BB: mov      eax, dword ptr [ebp - 0x10]
0x4908BE: mov      edx, dword ptr [eax + 0x30]
0x4908C1: add      edx, 0x5f
0x4908C4: mov      eax, dword ptr [ebp - 0x10]
0x4908C7: call     0x456f94
0x4908CC: mov      edx, eax
0x4908CE: mov      eax, dword ptr [ebp - 0x14]
0x4908D1: pop      ecx
0x4908D2: call     0x4180dc
0x4908D7: mov      eax, dword ptr [ebp - 0x14]
0x4908DA: call     0x44d104
0x4908DF: xor      eax, eax
0x4908E1: pop      edx
0x4908E2: pop      ecx
0x4908E3: pop      ecx
0x4908E4: mov      dword ptr fs:[eax], edx
0x4908E7: push     0x4908fc
0x4908EC: lea      eax, [ebp - 0x2c]
0x4908EF: call     0x403a58
0x4908F4: ret      
0x4908F5: jmp      0x403518
0x4908FA: jmp      0x4908ec
0x4908FC: pop      ebx
0x4908FD: mov      esp, ebp
0x4908FF: pop      ebp
0x490900: ret      
0x490901: add      byte ptr [eax], al
0x490903: add      bh, bh
0x490907: call     dword ptr [ecx]
0x490909: add      byte ptr [eax], al
0x49090B: add      byte ptr [edi - 0x43224214], bh
0x490911: cld      
0x490913: int3     
0x490914: mov      cl, 0xbb
0x490916: leave    
0x490917: call     0xab17ccf2
0x49091C: add      byte ptr cs:[eax], al
0x49091F: add      byte ptr [ebp - 0x75], dl
0x490922: in       al, dx
0x490923: add      esp, -0x24
0x490926: mov      dword ptr [ebp - 8], ecx
0x490929: mov      dword ptr [ebp - 0x14], edx
0x49092C: mov      dword ptr [ebp - 4], eax
0x49092F: mov      eax, dword ptr [ebp - 4]
0x490932: mov      eax, dword ptr [eax + 0x40c]
0x490938: mov      dword ptr [ebp - 0x10], eax
0x49093B: mov      eax, dword ptr [ebp - 4]
0x49093E: mov      edx, dword ptr [eax + 0x600]
0x490944: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x490949: mov      eax, dword ptr [eax]
0x49094B: mov      eax, dword ptr [eax + 0x348]
0x490951: call     0x45351c
0x490956: mov      dword ptr [ebp - 0xc], eax
0x490959: cmp      dword ptr [ebp - 0xc], 0
0x49095D: je       0x49099c
0x49095F: lea      edx, [ebp - 0x24]
0x490962: mov      eax, dword ptr [ebp - 0xc]
0x490965: call     0x44d8ec
0x49096A: lea      eax, [ebp - 0x24]
0x49096D: push     eax
0x49096E: mov      eax, dword ptr [ebp - 0xc]
0x490971: push     eax
0x490972: push     1
0x490974: mov      eax, dword ptr [ebp - 0x10]
0x490977: mov      edx, dword ptr [eax + 0x34]
0x49097A: mov      eax, dword ptr [ebp - 0x10]
0x49097D: call     0x456fdc
0x490982: push     eax
0x490983: mov      eax, dword ptr [ebp - 0x10]
0x490986: mov      edx, dword ptr [eax + 0x30]
0x490989: mov      eax, dword ptr [ebp - 0x10]
0x49098C: call     0x456f94
0x490991: mov      edx, eax
0x490993: mov      eax, dword ptr [ebp - 8]
0x490996: pop      ecx
0x490997: call     0x44d6cc
0x49099C: mov      esp, ebp
0x49099E: pop      ebp
0x49099F: ret      
0x4909A0: push     ebp
0x4909A1: mov      ebp, esp
0x4909A3: add      esp, -0x28
0x4909A6: mov      dword ptr [ebp - 0xc], ecx
0x4909A9: mov      dword ptr [ebp - 8], edx
0x4909AC: mov      dword ptr [ebp - 4], eax
0x4909AF: xor      eax, eax
0x4909B1: mov      dword ptr [ebp - 0x10], eax
0x4909B4: mov      eax, dword ptr [ebp - 4]
0x4909B7: mov      eax, dword ptr [eax + 0x604]
0x4909BD: add      eax, -0x31
0x4909C0: cmp      eax, 7
0x4909C3: ja       0x490a60
0x4909C9: jmp      dword ptr [eax*4 + 0x4909d0]
0x4909D0: lock or  dword ptr [ecx], ecx
0x4909D4: dec      byte ptr [ecx]
0x4909D6: dec      ecx
0x4909D7: add      byte ptr [edx + ecx], cl
0x4909DA: dec      ecx
0x4909DB: add      byte ptr [edx], bl
0x4909DD: or       cl, byte ptr [ecx]
0x4909E0: sub      byte ptr [edx], cl
0x4909E2: dec      ecx
0x4909E3: add      byte ptr [esi], dh
0x4909E5: or       cl, byte ptr [ecx]
0x4909E8: inc      esp
0x4909E9: or       cl, byte ptr [ecx]
0x4909EC: push     edx
0x4909ED: or       cl, byte ptr [ecx]
0x4909F0: mov      eax, dword ptr [ebp - 4]
0x4909F3: mov      eax, dword ptr [eax + 0x410]
0x4909F9: mov      dword ptr [ebp - 0x10], eax
0x4909FC: jmp      0x490a6c
0x4909FE: mov      eax, dword ptr [ebp - 4]
0x490A01: mov      eax, dword ptr [eax + 0x414]
0x490A07: mov      dword ptr [ebp - 0x10], eax
0x490A0A: jmp      0x490a6c
0x490A0C: mov      eax, dword ptr [ebp - 4]
0x490A0F: mov      eax, dword ptr [eax + 0x418]
0x490A15: mov      dword ptr [ebp - 0x10], eax
0x490A18: jmp      0x490a6c
0x490A1A: mov      eax, dword ptr [ebp - 4]
0x490A1D: mov      eax, dword ptr [eax + 0x41c]
0x490A23: mov      dword ptr [ebp - 0x10], eax
0x490A26: jmp      0x490a6c
0x490A28: mov      eax, dword ptr [ebp - 4]
0x490A2B: mov      eax, dword ptr [eax + 0x570]
0x490A31: mov      dword ptr [ebp - 0x10], eax
0x490A34: jmp      0x490a6c
0x490A36: mov      eax, dword ptr [ebp - 4]
0x490A39: mov      eax, dword ptr [eax + 0x574]
0x490A3F: mov      dword ptr [ebp - 0x10], eax
0x490A42: jmp      0x490a6c
0x490A44: mov      eax, dword ptr [ebp - 4]
0x490A47: mov      eax, dword ptr [eax + 0x578]
0x490A4D: mov      dword ptr [ebp - 0x10], eax
0x490A50: jmp      0x490a6c
0x490A52: mov      eax, dword ptr [ebp - 4]
0x490A55: mov      eax, dword ptr [eax + 0x57c]
0x490A5B: mov      dword ptr [ebp - 0x10], eax
0x490A5E: jmp      0x490a6c
0x490A60: mov      eax, dword ptr [ebp - 4]
0x490A63: mov      eax, dword ptr [eax + 0x420]
0x490A69: mov      dword ptr [ebp - 0x10], eax
0x490A6C: mov      eax, dword ptr [ebp - 0x10]
0x490A6F: cmp      eax, dword ptr [ebp - 8]
0x490A72: jne      0x490ad2
0x490A74: mov      eax, dword ptr [ebp - 0x10]
0x490A77: mov      edx, dword ptr [eax + 0x258]
0x490A7D: inc      edx
0x490A7E: mov      eax, dword ptr [ebp - 0x10]
0x490A81: mov      eax, dword ptr [eax + 0x254]
0x490A87: call     0x45351c
0x490A8C: mov      dword ptr [ebp - 0x14], eax
0x490A8F: cmp      dword ptr [ebp - 0x14], 0
0x490A93: je       0x490ad2
0x490A95: lea      edx, [ebp - 0x28]
0x490A98: mov      eax, dword ptr [ebp - 0x14]
0x490A9B: call     0x44d8ec
0x490AA0: lea      eax, [ebp - 0x28]
0x490AA3: push     eax
0x490AA4: mov      eax, dword ptr [ebp - 0x14]
0x490AA7: push     eax
0x490AA8: push     1
0x490AAA: mov      eax, dword ptr [ebp - 0x10]
0x490AAD: mov      edx, dword ptr [eax + 0x34]
0x490AB0: mov      eax, dword ptr [ebp - 0x10]
0x490AB3: call     0x456fdc
0x490AB8: push     eax
0x490AB9: mov      eax, dword ptr [ebp - 0x10]
0x490ABC: mov      edx, dword ptr [eax + 0x30]
0x490ABF: mov      eax, dword ptr [ebp - 0x10]
0x490AC2: call     0x456f94
0x490AC7: mov      edx, eax
0x490AC9: mov      eax, dword ptr [ebp - 0xc]
0x490ACC: pop      ecx
0x490ACD: call     0x44d6cc
0x490AD2: mov      eax, dword ptr [ebp - 8]
0x490AD5: mov      edx, dword ptr [0x456468]
0x490ADB: call     0x403034
0x490AE0: mov      dword ptr [ebp - 0x18], eax
0x490AE3: mov      eax, dword ptr [ebp - 0x18]
0x490AE6: cmp      byte ptr [eax + 0x274], 0
0x490AED: je       0x490b4c
0x490AEF: mov      eax, dword ptr [ebp - 0x18]
0x490AF2: mov      edx, dword ptr [eax + 0x258]
0x490AF8: mov      eax, dword ptr [ebp - 0x18]
0x490AFB: mov      eax, dword ptr [eax + 0x254]
0x490B01: call     0x45351c
0x490B06: mov      dword ptr [ebp - 0x14], eax
0x490B09: cmp      dword ptr [ebp - 0x14], 0
0x490B0D: je       0x490b4c
0x490B0F: lea      edx, [ebp - 0x28]
0x490B12: mov      eax, dword ptr [ebp - 0x14]
0x490B15: call     0x44d8ec
0x490B1A: lea      eax, [ebp - 0x28]
0x490B1D: push     eax
0x490B1E: mov      eax, dword ptr [ebp - 0x14]
0x490B21: push     eax
0x490B22: push     1
0x490B24: mov      eax, dword ptr [ebp - 0x18]
0x490B27: mov      edx, dword ptr [eax + 0x34]
0x490B2A: mov      eax, dword ptr [ebp - 0x18]
0x490B2D: call     0x456fdc
0x490B32: push     eax
0x490B33: mov      eax, dword ptr [ebp - 0x18]
0x490B36: mov      edx, dword ptr [eax + 0x30]
0x490B39: mov      eax, dword ptr [ebp - 0x18]
0x490B3C: call     0x456f94
0x490B41: mov      edx, eax
0x490B43: mov      eax, dword ptr [ebp - 0xc]
0x490B46: pop      ecx
0x490B47: call     0x44d6cc
0x490B4C: mov      esp, ebp
0x490B4E: pop      ebp
0x490B4F: ret      
0x490B50: push     ebp
0x490B51: mov      ebp, esp
0x490B53: add      esp, -0xc
0x490B56: mov      dword ptr [ebp - 0xc], ecx
0x490B59: mov      dword ptr [ebp - 8], edx
0x490B5C: mov      dword ptr [ebp - 4], eax
0x490B5F: xor      edx, edx
0x490B61: mov      eax, dword ptr [ebp - 4]
0x490B64: mov      eax, dword ptr [eax + 0x408]
0x490B6A: call     0x458214
0x490B6F: mov      esp, ebp
0x490B71: pop      ebp
0x490B72: ret      4
0x490B75: lea      eax, [eax]
0x490B78: push     ebp
0x490B79: mov      ebp, esp
0x490B7B: add      esp, -0xc
0x490B7E: mov      dword ptr [ebp - 0xc], ecx
0x490B81: mov      dword ptr [ebp - 8], edx
0x490B84: mov      dword ptr [ebp - 4], eax
0x490B87: mov      eax, dword ptr [ebp - 8]
0x490B8A: mov      edx, dword ptr [ebp - 4]
0x490B8D: cmp      eax, dword ptr [edx + 0x410]
0x490B93: jne      0x490ba2
0x490B95: mov      eax, dword ptr [ebp - 4]
0x490B98: mov      dword ptr [eax + 0x604], 0x31
0x490BA2: mov      eax, dword ptr [ebp - 8]
0x490BA5: mov      edx, dword ptr [ebp - 4]
0x490BA8: cmp      eax, dword ptr [edx + 0x414]
0x490BAE: jne      0x490bbd
0x490BB0: mov      eax, dword ptr [ebp - 4]
0x490BB3: mov      dword ptr [eax + 0x604], 0x32
0x490BBD: mov      eax, dword ptr [ebp - 8]
0x490BC0: mov      edx, dword ptr [ebp - 4]
0x490BC3: cmp      eax, dword ptr [edx + 0x418]
0x490BC9: jne      0x490bd8
0x490BCB: mov      eax, dword ptr [ebp - 4]
0x490BCE: mov      dword ptr [eax + 0x604], 0x33
0x490BD8: mov      eax, dword ptr [ebp - 8]
0x490BDB: mov      edx, dword ptr [ebp - 4]
0x490BDE: cmp      eax, dword ptr [edx + 0x41c]
0x490BE4: jne      0x490bf3
0x490BE6: mov      eax, dword ptr [ebp - 4]
0x490BE9: mov      dword ptr [eax + 0x604], 0x34
0x490BF3: mov      eax, dword ptr [ebp - 8]
0x490BF6: mov      edx, dword ptr [ebp - 4]
0x490BF9: cmp      eax, dword ptr [edx + 0x570]
0x490BFF: jne      0x490c0e
0x490C01: mov      eax, dword ptr [ebp - 4]
0x490C04: mov      dword ptr [eax + 0x604], 0x35
0x490C0E: mov      eax, dword ptr [ebp - 8]
0x490C11: mov      edx, dword ptr [ebp - 4]
0x490C14: cmp      eax, dword ptr [edx + 0x574]
0x490C1A: jne      0x490c29
0x490C1C: mov      eax, dword ptr [ebp - 4]
0x490C1F: mov      dword ptr [eax + 0x604], 0x36
0x490C29: mov      eax, dword ptr [ebp - 8]
0x490C2C: mov      edx, dword ptr [ebp - 4]
0x490C2F: cmp      eax, dword ptr [edx + 0x578]
0x490C35: jne      0x490c44
0x490C37: mov      eax, dword ptr [ebp - 4]
0x490C3A: mov      dword ptr [eax + 0x604], 0x37
0x490C44: mov      eax, dword ptr [ebp - 8]
0x490C47: mov      edx, dword ptr [ebp - 4]
0x490C4A: cmp      eax, dword ptr [edx + 0x57c]
0x490C50: jne      0x490c5f
0x490C52: mov      eax, dword ptr [ebp - 4]
0x490C55: mov      dword ptr [eax + 0x604], 0x38
0x490C5F: mov      eax, dword ptr [ebp - 8]
0x490C62: mov      edx, dword ptr [ebp - 4]
0x490C65: cmp      eax, dword ptr [edx + 0x420]
0x490C6B: jne      0x490c78
0x490C6D: mov      eax, dword ptr [ebp - 4]
0x490C70: xor      edx, edx
0x490C72: mov      dword ptr [eax + 0x604], edx
0x490C78: mov      esp, ebp
0x490C7A: pop      ebp
0x490C7B: ret      4
0x490C7E: mov      eax, eax
0x490C80: push     ebp
0x490C81: mov      ebp, esp
0x490C83: add      esp, -0xc
0x490C86: mov      dword ptr [ebp - 0xc], ecx
0x490C89: mov      dword ptr [ebp - 8], edx
0x490C8C: mov      dword ptr [ebp - 4], eax
0x490C8F: mov      eax, dword ptr [0x4a4084] ; *0x4A4084->0x4F9480
0x490C94: cmp      dword ptr [eax], 0
0x490C97: jne      0x490cd0
0x490C99: call     0x406154
0x490C9E: mov      edx, dword ptr [0x4a3e48] ; *0x4A3E48->0x4F94F0
0x490CA4: cmp      eax, dword ptr [edx]
0x490CA6: jbe      0x490cfd
0x490CA8: call     0x406154
0x490CAD: add      eax, 0xbb8
0x490CB2: mov      edx, dword ptr [0x4a3e48] ; *0x4A3E48->0x4F94F0
0x490CB8: mov      dword ptr [edx], eax
0x490CBA: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x490CBF: mov      eax, dword ptr [eax]
0x490CC1: call     0x4665e0
0x490CC6: mov      eax, dword ptr [0x4a42a0] ; *0x4A42A0->0x4F947C
0x490CCB: mov      byte ptr [eax], 1
0x490CCE: jmp      0x490cfd
0x490CD0: mov      eax, dword ptr [0x4a4084] ; *0x4A4084->0x4F9480
0x490CD5: inc      dword ptr [eax]
0x490CD7: mov      eax, dword ptr [0x4a4084] ; *0x4A4084->0x4F9480
0x490CDC: cmp      dword ptr [eax], 2
0x490CDF: jle      0x490cfd
0x490CE1: mov      eax, dword ptr [0x4a4084] ; *0x4A4084->0x4F9480
0x490CE6: xor      edx, edx
0x490CE8: mov      dword ptr [eax], edx
0x490CEA: mov      eax, dword ptr [0x4a4058] ; *0x4A4058->0x4F9484
0x490CEF: mov      dword ptr [eax], 1
0x490CF5: mov      eax, dword ptr [0x4a42a0] ; *0x4A42A0->0x4F947C
0x490CFA: mov      byte ptr [eax], 0
0x490CFD: mov      esp, ebp
0x490CFF: pop      ebp
0x490D00: ret      4
0x490D03: nop      
0x490D04: push     ebp
0x490D05: mov      ebp, esp
0x490D07: add      esp, -0xc
0x490D0A: mov      dword ptr [ebp - 0xc], ecx
0x490D0D: mov      dword ptr [ebp - 8], edx
0x490D10: mov      dword ptr [ebp - 4], eax
0x490D13: call     0x406154
0x490D18: mov      edx, dword ptr [0x4a3e48] ; *0x4A3E48->0x4F94F0
0x490D1E: cmp      eax, dword ptr [edx]
0x490D20: jbe      0x490d40
0x490D22: call     0x406154
0x490D27: add      eax, 0xbb8
0x490D2C: mov      edx, dword ptr [0x4a3e48] ; *0x4A3E48->0x4F94F0
0x490D32: mov      dword ptr [edx], eax
0x490D34: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x490D39: mov      eax, dword ptr [eax]
0x490D3B: call     0x466648
0x490D40: mov      esp, ebp
0x490D42: pop      ebp
0x490D43: ret      4
0x490D46: mov      eax, eax
0x490D48: push     ebp
0x490D49: mov      ebp, esp
0x490D4B: add      esp, -0xc
0x490D4E: mov      dword ptr [ebp - 0xc], ecx
0x490D51: mov      dword ptr [ebp - 8], edx
0x490D54: mov      dword ptr [ebp - 4], eax
0x490D57: mov      eax, dword ptr [ebp - 4]
0x490D5A: mov      eax, dword ptr [eax + 0x4c4]
0x490D60: cmp      byte ptr [eax + 0x24c], 0
0x490D67: je       0x490d7b
0x490D69: xor      edx, edx
0x490D6B: mov      eax, dword ptr [ebp - 4]
0x490D6E: mov      eax, dword ptr [eax + 0x4c4]
0x490D74: call     0x458214
0x490D79: jmp      0x490da8
0x490D7B: call     0x406154
0x490D80: mov      edx, dword ptr [0x4a3e48] ; *0x4A3E48->0x4F94F0
0x490D86: cmp      eax, dword ptr [edx]
0x490D88: jbe      0x490da8
0x490D8A: call     0x406154
0x490D8F: add      eax, 0xbb8
0x490D94: mov      edx, dword ptr [0x4a3e48] ; *0x4A3E48->0x4F94F0
0x490D9A: mov      dword ptr [edx], eax
0x490D9C: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x490DA1: mov      eax, dword ptr [eax]
0x490DA3: call     0x4666dc
0x490DA8: mov      esp, ebp
0x490DAA: pop      ebp
0x490DAB: ret      4
0x490DAE: mov      eax, eax
0x490DB0: push     ebp
0x490DB1: mov      ebp, esp
0x490DB3: add      esp, -0xc
0x490DB6: mov      dword ptr [ebp - 0xc], ecx
0x490DB9: mov      dword ptr [ebp - 8], edx
0x490DBC: mov      dword ptr [ebp - 4], eax
0x490DBF: mov      eax, dword ptr [ebp - 4]
0x490DC2: call     0x490dd0
0x490DC7: mov      esp, ebp
0x490DC9: pop      ebp
0x490DCA: ret      4
0x490DCD: lea      eax, [eax]
0x490DD0: push     ebp
0x490DD1: mov      ebp, esp
0x490DD3: push     ecx
0x490DD4: mov      dword ptr [ebp - 4], eax
0x490DD7: mov      eax, dword ptr [ebp - 4]
0x490DDA: mov      eax, dword ptr [eax + 0x434]
0x490DE0: mov      dl, byte ptr [eax + 0x24c]
0x490DE6: xor      dl, 1
0x490DE9: mov      eax, dword ptr [ebp - 4]
0x490DEC: mov      eax, dword ptr [eax + 0x434]
0x490DF2: call     0x458214
0x490DF7: pop      ecx
0x490DF8: pop      ebp
0x490DF9: ret      
0x490DFA: mov      eax, eax
0x490DFC: push     ebp
0x490DFD: mov      ebp, esp
0x490DFF: add      esp, -0x3c
0x490E02: push     ebx
0x490E03: xor      ebx, ebx
0x490E05: mov      dword ptr [ebp - 0x3c], ebx
0x490E08: mov      dword ptr [ebp - 8], ecx
0x490E0B: mov      dword ptr [ebp - 0x28], edx
0x490E0E: mov      dword ptr [ebp - 4], eax
0x490E11: xor      eax, eax
0x490E13: push     ebp
0x490E14: push     0x490fd1
0x490E19: push     dword ptr fs:[eax]
0x490E1C: mov      dword ptr fs:[eax], esp
0x490E1F: mov      eax, dword ptr [ebp - 4]
0x490E22: mov      eax, dword ptr [eax + 0x434]
0x490E28: mov      dword ptr [ebp - 0x1c], eax
0x490E2B: mov      eax, dword ptr [ebp - 0x1c]
0x490E2E: mov      edx, dword ptr [eax + 0x258]
0x490E34: mov      eax, dword ptr [ebp - 0x1c]
0x490E37: mov      eax, dword ptr [eax + 0x254]
0x490E3D: call     0x45351c
0x490E42: mov      dword ptr [ebp - 0xc], eax
0x490E45: cmp      dword ptr [ebp - 0xc], 0
0x490E49: je       0x490e88
0x490E4B: lea      edx, [ebp - 0x38]
0x490E4E: mov      eax, dword ptr [ebp - 0xc]
0x490E51: call     0x44d8ec
0x490E56: lea      eax, [ebp - 0x38]
0x490E59: push     eax
0x490E5A: mov      eax, dword ptr [ebp - 0xc]
0x490E5D: push     eax
0x490E5E: push     1
0x490E60: mov      eax, dword ptr [ebp - 0x1c]
0x490E63: mov      edx, dword ptr [eax + 0x34]
0x490E66: mov      eax, dword ptr [ebp - 0x1c]
0x490E69: call     0x456fdc
0x490E6E: push     eax
0x490E6F: mov      eax, dword ptr [ebp - 0x1c]
0x490E72: mov      edx, dword ptr [eax + 0x30]
0x490E75: mov      eax, dword ptr [ebp - 0x1c]
0x490E78: call     0x456f94
0x490E7D: mov      edx, eax
0x490E7F: mov      eax, dword ptr [ebp - 8]
0x490E82: pop      ecx
0x490E83: call     0x44d6cc
0x490E88: mov      eax, dword ptr [0x4a41cc] ; *0x4A41CC->0x4F94FC
0x490E8D: mov      eax, dword ptr [eax]
0x490E8F: mov      edx, dword ptr [eax]
0x490E91: call     dword ptr [edx + 0x14]
0x490E94: test     eax, eax
0x490E96: jle      0x490fbb
0x490E9C: mov      eax, dword ptr [ebp - 8]
0x490E9F: call     0x44d8b4
0x490EA4: mov      dword ptr [ebp - 0x20], eax
0x490EA7: push     1
0x490EA9: mov      eax, dword ptr [ebp - 0x20]
0x490EAC: call     0x41834c
0x490EB1: push     eax
0x490EB2: call     0x406434
0x490EB7: mov      eax, dword ptr [ebp - 0x20]
0x490EBA: mov      eax, dword ptr [eax + 0xc]
0x490EBD: mov      edx, 0xc0c0c0
0x490EC2: call     0x4173fc
0x490EC7: mov      edx, 0x1c
0x490ECC: mov      eax, dword ptr [ebp - 0x1c]
0x490ECF: call     0x456f94
0x490ED4: mov      edx, dword ptr [ebp - 0x1c]
0x490ED7: add      eax, dword ptr [edx + 0x30]
0x490EDA: mov      dword ptr [ebp - 0x10], eax
0x490EDD: mov      edx, 0x50
0x490EE2: mov      eax, dword ptr [ebp - 0x1c]
0x490EE5: call     0x456fdc
0x490EEA: mov      edx, dword ptr [ebp - 0x1c]
0x490EED: add      eax, dword ptr [edx + 0x34]
0x490EF0: mov      dword ptr [ebp - 0x14], eax
0x490EF3: lea      ecx, [ebp - 0x3c]
0x490EF6: mov      eax, dword ptr [0x4a41cc] ; *0x4A41CC->0x4F94FC
0x490EFB: mov      eax, dword ptr [eax]
0x490EFD: xor      edx, edx
0x490EFF: mov      ebx, dword ptr [eax]
0x490F01: call     dword ptr [ebx + 0xc]
0x490F04: mov      eax, dword ptr [ebp - 0x3c]
0x490F07: push     eax
0x490F08: mov      ecx, dword ptr [ebp - 0x14]
0x490F0B: mov      edx, dword ptr [ebp - 0x10]
0x490F0E: mov      eax, dword ptr [ebp - 0x20]
0x490F11: call     0x4180dc
0x490F16: mov      eax, dword ptr [0x4a41cc] ; *0x4A41CC->0x4F94FC
0x490F1B: mov      eax, dword ptr [eax]
0x490F1D: mov      edx, dword ptr [eax]
0x490F1F: call     dword ptr [edx + 0x14]
0x490F22: dec      eax
0x490F23: test     eax, eax
0x490F25: jle      0x490fb3
0x490F2B: mov      dword ptr [ebp - 0x24], eax
0x490F2E: mov      dword ptr [ebp - 0x18], 1
0x490F35: mov      edx, 0x1c
0x490F3A: mov      eax, dword ptr [ebp - 0x1c]
0x490F3D: call     0x456f94
0x490F42: mov      edx, dword ptr [ebp - 0x1c]
0x490F45: add      eax, dword ptr [edx + 0x30]
0x490F48: mov      edx, dword ptr [ebp - 0x18]
0x490F4B: dec      edx
0x490F4C: and      edx, 0x80000001
0x490F52: jns      0x490f59
0x490F54: dec      edx
0x490F55: or       edx, 0xfffffffe
0x490F58: inc      edx
0x490F59: imul     edx, edx, 0x64
0x490F5C: add      eax, edx
0x490F5E: mov      dword ptr [ebp - 0x10], eax
0x490F61: mov      edx, 0x60
0x490F66: mov      eax, dword ptr [ebp - 0x1c]
0x490F69: call     0x456fdc
0x490F6E: mov      edx, dword ptr [ebp - 0x1c]
0x490F71: add      eax, dword ptr [edx + 0x34]
0x490F74: mov      edx, dword ptr [ebp - 0x18]
0x490F77: dec      edx
0x490F78: sar      edx, 1
0x490F7A: jns      0x490f7f
0x490F7C: adc      edx, 0
0x490F7F: shl      edx, 4
0x490F82: add      eax, edx
0x490F84: mov      dword ptr [ebp - 0x14], eax
0x490F87: lea      ecx, [ebp - 0x3c]
0x490F8A: mov      eax, dword ptr [0x4a41cc] ; *0x4A41CC->0x4F94FC
0x490F8F: mov      eax, dword ptr [eax]
0x490F91: mov      edx, dword ptr [ebp - 0x18]
0x490F94: mov      ebx, dword ptr [eax]
0x490F96: call     dword ptr [ebx + 0xc]
0x490F99: mov      eax, dword ptr [ebp - 0x3c]
0x490F9C: push     eax
0x490F9D: mov      ecx, dword ptr [ebp - 0x14]
0x490FA0: mov      edx, dword ptr [ebp - 0x10]
0x490FA3: mov      eax, dword ptr [ebp - 0x20]
0x490FA6: call     0x4180dc
0x490FAB: inc      dword ptr [ebp - 0x18]
0x490FAE: dec      dword ptr [ebp - 0x24]
0x490FB1: jne      0x490f35
0x490FB3: mov      eax, dword ptr [ebp - 0x20]
0x490FB6: call     0x44d104
0x490FBB: xor      eax, eax
0x490FBD: pop      edx
0x490FBE: pop      ecx
0x490FBF: pop      ecx
0x490FC0: mov      dword ptr fs:[eax], edx
0x490FC3: push     0x490fd8
0x490FC8: lea      eax, [ebp - 0x3c]
0x490FCB: call     0x403a58
0x490FD0: ret      
0x490FD1: jmp      0x403518
0x490FD6: jmp      0x490fc8
0x490FD8: pop      ebx
0x490FD9: mov      esp, ebp
0x490FDB: pop      ebp
0x490FDC: ret      
0x490FDD: lea      eax, [eax]
0x490FE0: push     ebp
0x490FE1: mov      ebp, esp
0x490FE3: add      esp, -0xc
0x490FE6: mov      dword ptr [ebp - 0xc], ecx
0x490FE9: mov      dword ptr [ebp - 8], edx
0x490FEC: mov      dword ptr [ebp - 4], eax
0x490FEF: xor      edx, edx
0x490FF1: mov      eax, dword ptr [ebp - 4]
0x490FF4: mov      eax, dword ptr [eax + 0x434]
0x490FFA: call     0x458214
0x490FFF: mov      esp, ebp
0x491001: pop      ebp
0x491002: ret      4
0x491005: lea      eax, [eax]
0x491008: push     ebp
0x491009: mov      ebp, esp
0x49100B: add      esp, -0x24
0x49100E: mov      dword ptr [ebp - 0xc], ecx
0x491011: mov      dword ptr [ebp - 8], edx
0x491014: mov      dword ptr [ebp - 4], eax
0x491017: mov      eax, dword ptr [ebp - 8]
0x49101A: mov      edx, dword ptr [0x456468]
0x491020: call     0x403034
0x491025: mov      dword ptr [ebp - 0x14], eax
0x491028: mov      eax, dword ptr [ebp - 0x14]
0x49102B: cmp      byte ptr [eax + 0x274], 0
0x491032: je       0x491098
0x491034: mov      eax, dword ptr [ebp - 0x14]
0x491037: mov      edx, dword ptr [eax + 0x258]
0x49103D: dec      edx
0x49103E: mov      eax, dword ptr [ebp - 0x14]
0x491041: mov      eax, dword ptr [eax + 0x254]
0x491047: call     0x45351c
0x49104C: mov      dword ptr [ebp - 0x10], eax
0x49104F: cmp      dword ptr [ebp - 0x10], 0
0x491053: je       0x4910ff
0x491059: lea      edx, [ebp - 0x24]
0x49105C: mov      eax, dword ptr [ebp - 0x10]
0x49105F: call     0x44d8ec
0x491064: lea      eax, [ebp - 0x24]
0x491067: push     eax
0x491068: mov      eax, dword ptr [ebp - 0x10]
0x49106B: push     eax
0x49106C: push     1
0x49106E: mov      eax, dword ptr [ebp - 0x14]
0x491071: mov      edx, dword ptr [eax + 0x34]
0x491074: mov      eax, dword ptr [ebp - 0x14]
0x491077: call     0x456fdc
0x49107C: push     eax
0x49107D: mov      eax, dword ptr [ebp - 0x14]
0x491080: mov      edx, dword ptr [eax + 0x30]
0x491083: mov      eax, dword ptr [ebp - 0x14]
0x491086: call     0x456f94
0x49108B: mov      edx, eax
0x49108D: mov      eax, dword ptr [ebp - 0xc]
0x491090: pop      ecx
0x491091: call     0x44d6cc
0x491096: jmp      0x4910ff
0x491098: mov      eax, dword ptr [0x4a430c] ; *0x4A430C->0x4F94F8
0x49109D: cmp      byte ptr [eax], 0
0x4910A0: je       0x4910ff
0x4910A2: mov      eax, dword ptr [ebp - 0x14]
0x4910A5: mov      edx, dword ptr [eax + 0x258]
0x4910AB: mov      eax, dword ptr [ebp - 0x14]
0x4910AE: mov      eax, dword ptr [eax + 0x254]
0x4910B4: call     0x45351c
0x4910B9: mov      dword ptr [ebp - 0x10], eax
0x4910BC: cmp      dword ptr [ebp - 0x10], 0
0x4910C0: je       0x4910ff
0x4910C2: lea      edx, [ebp - 0x24]
0x4910C5: mov      eax, dword ptr [ebp - 0x10]
0x4910C8: call     0x44d8ec
0x4910CD: lea      eax, [ebp - 0x24]
0x4910D0: push     eax
0x4910D1: mov      eax, dword ptr [ebp - 0x10]
0x4910D4: push     eax
0x4910D5: push     1
0x4910D7: mov      eax, dword ptr [ebp - 0x14]
0x4910DA: mov      edx, dword ptr [eax + 0x34]
0x4910DD: mov      eax, dword ptr [ebp - 0x14]
0x4910E0: call     0x456fdc
0x4910E5: push     eax
0x4910E6: mov      eax, dword ptr [ebp - 0x14]
0x4910E9: mov      edx, dword ptr [eax + 0x30]
0x4910EC: mov      eax, dword ptr [ebp - 0x14]
0x4910EF: call     0x456f94
0x4910F4: mov      edx, eax
0x4910F6: mov      eax, dword ptr [ebp - 0xc]
0x4910F9: pop      ecx
0x4910FA: call     0x44d6cc
0x4910FF: mov      esp, ebp
0x491101: pop      ebp
0x491102: ret      
0x491103: nop      
0x491104: push     ebp
0x491105: mov      ebp, esp
0x491107: add      esp, -0xc
0x49110A: mov      dword ptr [ebp - 0xc], ecx
0x49110D: mov      dword ptr [ebp - 8], edx
0x491110: mov      dword ptr [ebp - 4], eax
0x491113: call     0x406154
0x491118: mov      edx, dword ptr [0x4a4200] ; *0x4A4200->0x4F94E8
0x49111E: cmp      eax, dword ptr [edx]
0x491120: jbe      0x491159
0x491122: mov      eax, dword ptr [0x4a430c] ; *0x4A430C->0x4F94F8
0x491127: mov      al, byte ptr [eax]
0x491129: xor      al, 1
0x49112B: mov      edx, dword ptr [0x4a430c] ; *0x4A430C->0x4F94F8
0x491131: mov      byte ptr [edx], al
0x491133: call     0x406154
0x491138: add      eax, 0x1388
0x49113D: mov      edx, dword ptr [0x4a4200] ; *0x4A4200->0x4F94E8
0x491143: mov      dword ptr [edx], eax
0x491145: mov      edx, dword ptr [0x4a430c] ; *0x4A430C->0x4F94F8
0x49114B: mov      dl, byte ptr [edx]
0x49114D: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x491152: mov      eax, dword ptr [eax]
0x491154: call     0x4664b0
0x491159: mov      esp, ebp
0x49115B: pop      ebp
0x49115C: ret      4
0x49115F: nop      
0x491160: push     ebp
0x491161: mov      ebp, esp
0x491163: add      esp, -0x14
0x491166: push     ebx
0x491167: xor      ebx, ebx
0x491169: mov      dword ptr [ebp - 0x14], ebx
0x49116C: mov      dword ptr [ebp - 8], ebx
0x49116F: mov      dword ptr [ebp - 0x10], ecx
0x491172: mov      dword ptr [ebp - 0xc], edx
0x491175: mov      dword ptr [ebp - 4], eax
0x491178: xor      eax, eax
0x49117A: push     ebp
0x49117B: push     0x49122d
0x491180: push     dword ptr fs:[eax]
0x491183: mov      dword ptr fs:[eax], esp
0x491186: call     0x406154
0x49118B: mov      edx, dword ptr [0x4a4200] ; *0x4A4200->0x4F94E8
0x491191: cmp      eax, dword ptr [edx]
0x491193: jbe      0x49120f
0x491195: mov      eax, dword ptr [0x4a41cc] ; *0x4A41CC->0x4F94FC
0x49119A: mov      eax, dword ptr [eax]
0x49119C: mov      edx, dword ptr [eax]
0x49119E: call     dword ptr [edx + 0x14]
0x4911A1: test     eax, eax
0x4911A3: jne      0x49120f
0x4911A5: mov      eax, dword ptr [ebp - 4]
0x4911A8: mov      dword ptr [eax + 0x620], 1
0x4911B2: mov      cx, word ptr [0x49123c]
0x4911B9: mov      edx, 0x491248 ; "输入你想加入编组的人的名字."
0x4911BE: mov      eax, dword ptr [ebp - 4]
0x4911C1: call     0x486df0
0x4911C6: lea      edx, [ebp - 8]
0x4911C9: mov      eax, dword ptr [ebp - 4]
0x4911CC: mov      eax, dword ptr [eax + 0x784]
0x4911D2: call     0x407be4
0x4911D7: cmp      dword ptr [ebp - 8], 0
0x4911DB: je       0x49120f
0x4911DD: call     0x406154
0x4911E2: add      eax, 0x1388
0x4911E7: mov      edx, dword ptr [0x4a4200] ; *0x4A4200->0x4F94E8
0x4911ED: mov      dword ptr [edx], eax
0x4911EF: lea      edx, [ebp - 0x14]
0x4911F2: mov      eax, dword ptr [ebp - 4]
0x4911F5: mov      eax, dword ptr [eax + 0x784]
0x4911FB: call     0x407be4
0x491200: mov      edx, dword ptr [ebp - 0x14]
0x491203: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x491208: mov      eax, dword ptr [eax]
0x49120A: call     0x46653c
0x49120F: xor      eax, eax
0x491211: pop      edx
0x491212: pop      ecx
0x491213: pop      ecx
0x491214: mov      dword ptr fs:[eax], edx
0x491217: push     0x491234
0x49121C: lea      eax, [ebp - 0x14]
0x49121F: call     0x403a58
0x491224: lea      eax, [ebp - 8]
0x491227: call     0x403a58
0x49122C: ret      
0x49122D: jmp      0x403518
0x491232: jmp      0x49121c
0x491234: pop      ebx
0x491235: mov      esp, ebp
0x491237: pop      ebp
0x491238: ret      4
0x49123B: add      byte ptr [eax + eax], dl
0x49123E: add      byte ptr [eax], al
0x491248: [STR len=27] "输入你想加入编组的人的名字."
0x491264: push     ebp
0x491265: mov      ebp, esp
0x491267: add      esp, -0x14
0x49126A: push     ebx
0x49126B: xor      ebx, ebx
0x49126D: mov      dword ptr [ebp - 0x14], ebx
0x491270: mov      dword ptr [ebp - 8], ebx
0x491273: mov      dword ptr [ebp - 0x10], ecx
0x491276: mov      dword ptr [ebp - 0xc], edx
0x491279: mov      dword ptr [ebp - 4], eax
0x49127C: xor      eax, eax
0x49127E: push     ebp
0x49127F: push     0x491331
0x491284: push     dword ptr fs:[eax]
0x491287: mov      dword ptr fs:[eax], esp
0x49128A: call     0x406154
0x49128F: mov      edx, dword ptr [0x4a4200] ; *0x4A4200->0x4F94E8
0x491295: cmp      eax, dword ptr [edx]
0x491297: jbe      0x491313
0x491299: mov      eax, dword ptr [0x4a41cc] ; *0x4A41CC->0x4F94FC
0x49129E: mov      eax, dword ptr [eax]
0x4912A0: mov      edx, dword ptr [eax]
0x4912A2: call     dword ptr [edx + 0x14]
0x4912A5: test     eax, eax
0x4912A7: jle      0x491313
0x4912A9: mov      eax, dword ptr [ebp - 4]
0x4912AC: mov      dword ptr [eax + 0x620], 1
0x4912B6: mov      cx, word ptr [0x491340]
0x4912BD: mov      edx, 0x49134c ; "输入你想加入编组的人的名字."
0x4912C2: mov      eax, dword ptr [ebp - 4]
0x4912C5: call     0x486df0
0x4912CA: lea      edx, [ebp - 8]
0x4912CD: mov      eax, dword ptr [ebp - 4]
0x4912D0: mov      eax, dword ptr [eax + 0x784]
0x4912D6: call     0x407be4
0x4912DB: cmp      dword ptr [ebp - 8], 0
0x4912DF: je       0x491313
0x4912E1: call     0x406154
0x4912E6: add      eax, 0x1388
0x4912EB: mov      edx, dword ptr [0x4a4200] ; *0x4A4200->0x4F94E8
0x4912F1: mov      dword ptr [edx], eax
0x4912F3: lea      edx, [ebp - 0x14]
0x4912F6: mov      eax, dword ptr [ebp - 4]
0x4912F9: mov      eax, dword ptr [eax + 0x784]
0x4912FF: call     0x407be4
0x491304: mov      edx, dword ptr [ebp - 0x14]
0x491307: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x49130C: mov      eax, dword ptr [eax]
0x49130E: call     0x4669d0
0x491313: xor      eax, eax
0x491315: pop      edx
0x491316: pop      ecx
0x491317: pop      ecx
0x491318: mov      dword ptr fs:[eax], edx
0x49131B: push     0x491338
0x491320: lea      eax, [ebp - 0x14]
0x491323: call     0x403a58
0x491328: lea      eax, [ebp - 8]
0x49132B: call     0x403a58
0x491330: ret      
0x491331: jmp      0x403518
0x491336: jmp      0x491320
0x491338: pop      ebx
0x491339: mov      esp, ebp
0x49133B: pop      ebp
0x49133C: ret      4
0x49133F: add      byte ptr [eax + eax], dl
0x491342: add      byte ptr [eax], al
0x49134C: [STR len=27] "输入你想加入编组的人的名字."
0x491368: push     ebp
0x491369: mov      ebp, esp
0x49136B: add      esp, -0x14
0x49136E: push     ebx
0x49136F: xor      ebx, ebx
0x491371: mov      dword ptr [ebp - 0x14], ebx
0x491374: mov      dword ptr [ebp - 8], ebx
0x491377: mov      dword ptr [ebp - 0x10], ecx
0x49137A: mov      dword ptr [ebp - 0xc], edx
0x49137D: mov      dword ptr [ebp - 4], eax
0x491380: xor      eax, eax
0x491382: push     ebp
0x491383: push     0x491435
0x491388: push     dword ptr fs:[eax]
0x49138B: mov      dword ptr fs:[eax], esp
0x49138E: call     0x406154
0x491393: mov      edx, dword ptr [0x4a4200] ; *0x4A4200->0x4F94E8
0x491399: cmp      eax, dword ptr [edx]
0x49139B: jbe      0x491417
0x49139D: mov      eax, dword ptr [0x4a41cc] ; *0x4A41CC->0x4F94FC
0x4913A2: mov      eax, dword ptr [eax]
0x4913A4: mov      edx, dword ptr [eax]
0x4913A6: call     dword ptr [edx + 0x14]
0x4913A9: test     eax, eax
0x4913AB: jle      0x491417
0x4913AD: mov      eax, dword ptr [ebp - 4]
0x4913B0: mov      dword ptr [eax + 0x620], 1
0x4913BA: mov      cx, word ptr [0x491444]
0x4913C1: mov      edx, 0x491450 ; "输入你想从编组中删除的人的名字。"
0x4913C6: mov      eax, dword ptr [ebp - 4]
0x4913C9: call     0x486df0
0x4913CE: lea      edx, [ebp - 8]
0x4913D1: mov      eax, dword ptr [ebp - 4]
0x4913D4: mov      eax, dword ptr [eax + 0x784]
0x4913DA: call     0x407be4
0x4913DF: cmp      dword ptr [ebp - 8], 0
0x4913E3: je       0x491417
0x4913E5: call     0x406154
0x4913EA: add      eax, 0x1388
0x4913EF: mov      edx, dword ptr [0x4a4200] ; *0x4A4200->0x4F94E8
0x4913F5: mov      dword ptr [edx], eax
0x4913F7: lea      edx, [ebp - 0x14]
0x4913FA: mov      eax, dword ptr [ebp - 4]
0x4913FD: mov      eax, dword ptr [eax + 0x784]
0x491403: call     0x407be4
0x491408: mov      edx, dword ptr [ebp - 0x14]
0x49140B: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x491410: mov      eax, dword ptr [eax]
0x491412: call     0x466a74
0x491417: xor      eax, eax
0x491419: pop      edx
0x49141A: pop      ecx
0x49141B: pop      ecx
0x49141C: mov      dword ptr fs:[eax], edx
0x49141F: push     0x49143c
0x491424: lea      eax, [ebp - 0x14]
0x491427: call     0x403a58
0x49142C: lea      eax, [ebp - 8]
0x49142F: call     0x403a58
0x491434: ret      
0x491435: jmp      0x403518
0x49143A: jmp      0x491424
0x49143C: pop      ebx
0x49143D: mov      esp, ebp
0x49143F: pop      ebp
0x491440: ret      4
0x491443: add      byte ptr [eax + eax], dl
0x491446: add      byte ptr [eax], al
0x491450: [STR len=32] "输入你想从编组中删除的人的名字。"
0x491474: push     ebp
0x491475: mov      ebp, esp
0x491477: add      esp, -0xc
0x49147A: mov      dword ptr [ebp - 0xc], ecx
0x49147D: mov      dword ptr [ebp - 8], edx
0x491480: mov      dword ptr [ebp - 4], eax
0x491483: call     0x406154
0x491488: mov      edx, dword ptr [0x4a41ec] ; *0x4A41EC->0x4F9448
0x49148E: sub      eax, dword ptr [edx]
0x491490: cmp      eax, 0x2710
0x491495: jbe      0x4914bf
0x491497: call     0x406154
0x49149C: mov      edx, dword ptr [0x4a4338] ; *0x4A4338->0x4F945C
0x4914A2: sub      eax, dword ptr [edx]
0x4914A4: cmp      eax, 0x2710
0x4914A9: jbe      0x4914bf
0x4914AB: call     0x406154
0x4914B0: mov      edx, dword ptr [0x4a41c8] ; *0x4A41C8->0x4F9458
0x4914B6: sub      eax, dword ptr [edx]
0x4914B8: cmp      eax, 0x2710
0x4914BD: ja       0x4914cc
0x4914BF: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4914C4: mov      eax, dword ptr [eax]
0x4914C6: cmp      byte ptr [eax + 0x20], 0
0x4914CA: je       0x4914da
0x4914CC: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4914D1: mov      eax, dword ptr [eax]
0x4914D3: call     0x461144
0x4914D8: jmp      0x4914f5
0x4914DA: push     0xff
0x4914DF: mov      eax, dword ptr [0x4a3fb8] ; *0x4A3FB8->0x4F7D8C
0x4914E4: mov      eax, dword ptr [eax]
0x4914E6: mov      ecx, 0xffff
0x4914EB: mov      edx, 0x491504 ; "在战斗的时候你不能退出游戏."
0x4914F0: call     0x479ae0
0x4914F5: mov      esp, ebp
0x4914F7: pop      ebp
0x4914F8: ret      4
0x4914FB: add      bh, bh
0x4914FF: call     ptr [ebx]
0x491501: add      byte ptr [eax], al
0x491503: add      ah, dl
0x491505: fcmovbe  st(0), st(5)
0x491507: mov      ebp, 0xc4b5b7b6
0x49150C: retf     0xbab1
0x491511: jecxz    0x4914c5
0x491513: mov      ebx, 0xcbcddcc4
0x491518: mov      bl, 0xf6
0x49151A: ror      esi, cl
0x49151C: iretd    
0x49151D: mov      bh, 0x2e
0x49151F: add      byte ptr [ebp - 0x75], dl
0x491522: in       al, dx
0x491523: add      esp, -0xc
0x491526: mov      dword ptr [ebp - 0xc], ecx
0x491529: mov      dword ptr [ebp - 8], edx
0x49152C: mov      dword ptr [ebp - 4], eax
0x49152F: call     0x406154
0x491534: mov      edx, dword ptr [0x4a41ec] ; *0x4A41EC->0x4F9448
0x49153A: sub      eax, dword ptr [edx]
0x49153C: cmp      eax, 0x2710
0x491541: jbe      0x49156b
0x491543: call     0x406154
0x491548: mov      edx, dword ptr [0x4a4338] ; *0x4A4338->0x4F945C
0x49154E: sub      eax, dword ptr [edx]
0x491550: cmp      eax, 0x2710
0x491555: jbe      0x49156b
0x491557: call     0x406154
0x49155C: mov      edx, dword ptr [0x4a41c8] ; *0x4A41C8->0x4F9458
0x491562: sub      eax, dword ptr [edx]
0x491564: cmp      eax, 0x2710
0x491569: ja       0x491578
0x49156B: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x491570: mov      eax, dword ptr [eax]
0x491572: cmp      byte ptr [eax + 0x20], 0
0x491576: je       0x491586
0x491578: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x49157D: mov      eax, dword ptr [eax]
0x49157F: call     0x461280
0x491584: jmp      0x4915a1
0x491586: push     0xff
0x49158B: mov      eax, dword ptr [0x4a3fb8] ; *0x4A3FB8->0x4F7D8C
0x491590: mov      eax, dword ptr [eax]
0x491592: mov      ecx, 0xffff
0x491597: mov      edx, 0x4915b0 ; "在战斗的时候你不能退出游戏."
0x49159C: call     0x479ae0
0x4915A1: mov      esp, ebp
0x4915A3: pop      ebp
0x4915A4: ret      4
0x4915A7: add      bh, bh
0x4915AB: call     ptr [ebx]
0x4915AD: add      byte ptr [eax], al
0x4915AF: add      ah, dl
0x4915B1: fcmovbe  st(0), st(5)
0x4915B3: mov      ebp, 0xc4b5b7b6
0x4915B8: retf     0xbab1
0x4915BD: jecxz    0x491571
0x4915BF: mov      ebx, 0xcbcddcc4
0x4915C4: mov      bl, 0xf6
0x4915C6: ror      esi, cl
0x4915C8: iretd    
0x4915C9: mov      bh, 0x2e
0x4915CB: add      byte ptr [ebp - 0x75], dl
0x4915CE: in       al, dx
0x4915CF: add      esp, -0xc
0x4915D2: mov      dword ptr [ebp - 0xc], ecx
0x4915D5: mov      dword ptr [ebp - 8], edx
0x4915D8: mov      dword ptr [ebp - 4], eax
0x4915DB: mov      eax, dword ptr [0x4f95cc]
0x4915E0: call     0x4866d4
0x4915E5: mov      esp, ebp
0x4915E7: pop      ebp
0x4915E8: ret      4
0x4915EB: nop      
0x4915EC: push     ebp
0x4915ED: mov      ebp, esp
0x4915EF: add      esp, -8
0x4915F2: mov      dword ptr [ebp - 4], eax
0x4915F5: mov      edx, 0x1d0
0x4915FA: mov      eax, dword ptr [ebp - 4]
0x4915FD: mov      eax, dword ptr [eax + 0x460]
0x491603: call     0x42718c
0x491608: xor      edx, edx
0x49160A: mov      eax, dword ptr [ebp - 4]
0x49160D: mov      eax, dword ptr [eax + 0x460]
0x491613: call     0x4271b0
0x491618: mov      edx, 0x1d0
0x49161D: mov      eax, dword ptr [ebp - 4]
0x491620: mov      eax, dword ptr [eax + 0x464]
0x491626: call     0x42718c
0x49162B: mov      eax, dword ptr [ebp - 4]
0x49162E: mov      eax, dword ptr [eax + 0x460]
0x491634: mov      edx, dword ptr [eax + 0x3c]
0x491637: sub      edx, 0xf
0x49163A: mov      eax, dword ptr [ebp - 4]
0x49163D: mov      eax, dword ptr [eax + 0x464]
0x491643: call     0x4271b0
0x491648: xor      edx, edx
0x49164A: mov      eax, dword ptr [ebp - 4]
0x49164D: mov      eax, dword ptr [eax + 0x2cc]
0x491653: call     0x42718c
0x491658: xor      edx, edx
0x49165A: mov      eax, dword ptr [ebp - 4]
0x49165D: mov      eax, dword ptr [eax + 0x2cc]
0x491663: call     0x4271b0
0x491668: mov      dl, 1
0x49166A: mov      eax, dword ptr [ebp - 4]
0x49166D: mov      eax, dword ptr [eax + 0x2cc]
0x491673: call     0x458214
0x491678: mov      dl, 1
0x49167A: mov      eax, dword ptr [ebp - 4]
0x49167D: mov      eax, dword ptr [eax + 0x464]
0x491683: call     0x458214
0x491688: mov      dl, 1
0x49168A: mov      eax, dword ptr [ebp - 4]
0x49168D: mov      eax, dword ptr [eax + 0x460]
0x491693: call     0x458214
0x491698: mov      eax, dword ptr [0x4a4328] ; *0x4A4328->0x4F8B60
0x49169D: xor      ecx, ecx
0x49169F: mov      edx, 0x208
0x4916A4: call     0x402b54
0x4916A9: mov      eax, dword ptr [0x4a3d98] ; *0x4A3D98->0x4F8D68
0x4916AE: xor      ecx, ecx
0x4916B0: mov      edx, 0x410
0x4916B5: call     0x402b54
0x4916BA: mov      eax, dword ptr [0x4a3efc] ; *0x4A3EFC->0x4F9180
0x4916BF: xor      edx, edx
0x4916C1: mov      dword ptr [eax], edx
0x4916C3: mov      eax, dword ptr [0x4a3f2c] ; *0x4A3F2C->0x4F9184
0x4916C8: xor      edx, edx
0x4916CA: mov      dword ptr [eax], edx
0x4916CC: mov      eax, dword ptr [0x4a409c] ; *0x4A409C->0x4F9188
0x4916D1: mov      byte ptr [eax], 0
0x4916D4: call     0x49faa4
0x4916D9: pop      ecx
0x4916DA: pop      ecx
0x4916DB: pop      ebp
0x4916DC: ret      
0x4916DD: lea      eax, [eax]
0x4916E0: push     ebp
0x4916E1: mov      ebp, esp
0x4916E3: push     ecx
0x4916E4: mov      dword ptr [ebp - 4], eax
0x4916E7: xor      edx, edx
0x4916E9: mov      eax, dword ptr [ebp - 4]
0x4916EC: mov      eax, dword ptr [eax + 0x464]
0x4916F2: call     0x458214
0x4916F7: xor      edx, edx
0x4916F9: mov      eax, dword ptr [ebp - 4]
0x4916FC: mov      eax, dword ptr [eax + 0x460]
0x491702: call     0x458214
0x491707: call     0x49faa4
0x49170C: pop      ecx
0x49170D: pop      ebp
0x49170E: ret      
0x49170F: nop      
0x491710: push     ebp
0x491711: mov      ebp, esp
0x491713: add      esp, -0x10
0x491716: mov      dword ptr [ebp - 0x10], ecx
0x491719: mov      dword ptr [ebp - 0xc], edx
0x49171C: mov      dword ptr [ebp - 4], eax
0x49171F: call     0x406154
0x491724: mov      edx, dword ptr [0x4a403c] ; *0x4A403C->0x4F94EC
0x49172A: cmp      eax, dword ptr [edx]
0x49172C: jbe      0x491792
0x49172E: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x491733: mov      eax, dword ptr [eax]
0x491735: call     0x466968
0x49173A: call     0x406154
0x49173F: add      eax, 0xfa0
0x491744: mov      edx, dword ptr [0x4a403c] ; *0x4A403C->0x4F94EC
0x49174A: mov      dword ptr [edx], eax
0x49174C: mov      eax, dword ptr [0x4a409c] ; *0x4A409C->0x4F9188
0x491751: mov      byte ptr [eax], 1
0x491754: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x491759: cmp      byte ptr [eax], 0
0x49175C: je       0x491792
0x49175E: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x491763: mov      eax, dword ptr [eax]
0x491765: mov      dword ptr [ebp - 8], eax
0x491768: cmp      dword ptr [ebp - 8], -0x14
0x49176C: jg       0x491792
0x49176E: cmp      dword ptr [ebp - 8], -0x1e
0x491772: jle      0x491792
0x491774: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x491779: add      eax, 4
0x49177C: call     0x49fde4
0x491781: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x491786: mov      byte ptr [eax], 0
0x491789: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x49178E: mov      byte ptr [eax + 4], 0
0x491792: mov      esp, ebp
0x491794: pop      ebp
0x491795: ret      4
0x491798: push     ebp
0x491799: mov      ebp, esp
0x49179B: add      esp, -0xc
0x49179E: mov      dword ptr [ebp - 0xc], ecx
0x4917A1: mov      dword ptr [ebp - 8], edx
0x4917A4: mov      dword ptr [ebp - 4], eax
0x4917A7: call     0x406154
0x4917AC: mov      edx, dword ptr [0x4a403c] ; *0x4A403C->0x4F94EC
0x4917B2: cmp      eax, dword ptr [edx]
0x4917B4: jbe      0x4917ca
0x4917B6: mov      eax, dword ptr [ebp - 4]
0x4917B9: call     0x4916e0
0x4917BE: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4917C3: mov      eax, dword ptr [eax]
0x4917C5: call     0x466744
0x4917CA: mov      esp, ebp
0x4917CC: pop      ebp
0x4917CD: ret      4
0x4917D0: push     ebp
0x4917D1: mov      ebp, esp
0x4917D3: add      esp, -0x2c
0x4917D6: push     ebx
0x4917D7: xor      ebx, ebx
0x4917D9: mov      dword ptr [ebp - 0x2c], ebx
0x4917DC: mov      dword ptr [ebp - 8], ecx
0x4917DF: mov      dword ptr [ebp - 0x18], edx
0x4917E2: mov      dword ptr [ebp - 4], eax
0x4917E5: xor      eax, eax
0x4917E7: push     ebp
0x4917E8: push     0x491945
0x4917ED: push     dword ptr fs:[eax]
0x4917F0: mov      dword ptr fs:[eax], esp
0x4917F3: mov      eax, dword ptr [ebp - 4]
0x4917F6: mov      eax, dword ptr [eax + 0x460]
0x4917FC: mov      dword ptr [ebp - 0x10], eax
0x4917FF: mov      eax, dword ptr [ebp - 0x10]
0x491802: mov      edx, dword ptr [eax + 0x258]
0x491808: mov      eax, dword ptr [ebp - 0x10]
0x49180B: mov      eax, dword ptr [eax + 0x254]
0x491811: call     0x45351c
0x491816: mov      dword ptr [ebp - 0xc], eax
0x491819: cmp      dword ptr [ebp - 0xc], 0
0x49181D: je       0x49185c
0x49181F: lea      edx, [ebp - 0x28]
0x491822: mov      eax, dword ptr [ebp - 0xc]
0x491825: call     0x44d8ec
0x49182A: lea      eax, [ebp - 0x28]
0x49182D: push     eax
0x49182E: mov      eax, dword ptr [ebp - 0xc]
0x491831: push     eax
0x491832: push     1
0x491834: mov      eax, dword ptr [ebp - 0x10]
0x491837: mov      edx, dword ptr [eax + 0x34]
0x49183A: mov      eax, dword ptr [ebp - 0x10]
0x49183D: call     0x456fdc
0x491842: push     eax
0x491843: mov      eax, dword ptr [ebp - 0x10]
0x491846: mov      edx, dword ptr [eax + 0x30]
0x491849: mov      eax, dword ptr [ebp - 0x10]
0x49184C: call     0x456f94
0x491851: mov      edx, eax
0x491853: mov      eax, dword ptr [ebp - 8]
0x491856: pop      ecx
0x491857: call     0x44d6cc
0x49185C: mov      eax, dword ptr [ebp - 8]
0x49185F: call     0x44d8b4
0x491864: mov      dword ptr [ebp - 0x14], eax
0x491867: push     1
0x491869: mov      eax, dword ptr [ebp - 0x14]
0x49186C: call     0x41834c
0x491871: push     eax
0x491872: call     0x406434
0x491877: mov      edx, 0xffffff
0x49187C: mov      eax, dword ptr [ebp - 0x14]
0x49187F: mov      eax, dword ptr [eax + 0xc]
0x491882: call     0x4173fc
0x491887: lea      edx, [ebp - 0x2c]
0x49188A: mov      eax, dword ptr [0x4a3f2c] ; *0x4A3F2C->0x4F9184
0x49188F: mov      eax, dword ptr [eax]
0x491891: call     0x49f640
0x491896: mov      eax, dword ptr [ebp - 0x2c]
0x491899: push     eax
0x49189A: mov      eax, dword ptr [ebp - 0x10]
0x49189D: mov      edx, dword ptr [eax + 0x34]
0x4918A0: add      edx, 0xc4
0x4918A6: sub      edx, 0x41
0x4918A9: mov      eax, dword ptr [ebp - 0x10]
0x4918AC: call     0x456fdc
0x4918B1: push     eax
0x4918B2: mov      eax, dword ptr [ebp - 0x10]
0x4918B5: mov      edx, dword ptr [eax + 0x30]
0x4918B8: add      edx, 0x40
0x4918BB: mov      eax, dword ptr [ebp - 0x10]
0x4918BE: call     0x456f94
0x4918C3: mov      edx, eax
0x4918C5: mov      eax, dword ptr [ebp - 0x14]
0x4918C8: pop      ecx
0x4918C9: call     0x4180dc
0x4918CE: mov      eax, dword ptr [0x4a42ac] ; *0x4A42AC->0x4F918C
0x4918D3: mov      eax, dword ptr [eax]
0x4918D5: push     eax
0x4918D6: mov      eax, dword ptr [ebp - 0x10]
0x4918D9: mov      edx, dword ptr [eax + 0x34]
0x4918DC: add      edx, 3
0x4918DF: mov      eax, dword ptr [ebp - 0x10]
0x4918E2: call     0x456fdc
0x4918E7: add      eax, 3
0x4918EA: push     eax
0x4918EB: mov      edx, dword ptr [0x4a42ac] ; *0x4A42AC->0x4F918C
0x4918F1: mov      edx, dword ptr [edx]
0x4918F3: mov      eax, dword ptr [ebp - 0x14]
0x4918F6: call     0x4181ac
0x4918FB: mov      edx, 0x6a
0x491900: sub      edx, eax
0x491902: sar      edx, 1
0x491904: jns      0x491909
0x491906: adc      edx, 0
0x491909: mov      eax, dword ptr [ebp - 0x10]
0x49190C: mov      eax, dword ptr [eax + 0x30]
0x49190F: add      eax, 0x3b
0x491912: add      edx, eax
0x491914: mov      eax, dword ptr [ebp - 0x10]
0x491917: call     0x456f94
0x49191C: mov      edx, eax
0x49191E: mov      eax, dword ptr [ebp - 0x14]
0x491921: pop      ecx
0x491922: call     0x4180dc
0x491927: mov      eax, dword ptr [ebp - 0x14]
0x49192A: call     0x44d104
0x49192F: xor      eax, eax
0x491931: pop      edx
0x491932: pop      ecx
0x491933: pop      ecx
0x491934: mov      dword ptr fs:[eax], edx
0x491937: push     0x49194c
0x49193C: lea      eax, [ebp - 0x2c]
0x49193F: call     0x403a58
0x491944: ret      
0x491945: jmp      0x403518
0x49194A: jmp      0x49193c
0x49194C: pop      ebx
0x49194D: mov      esp, ebp
0x49194F: pop      ebp
0x491950: ret      
0x491951: lea      eax, [eax]
0x491954: push     ebp
0x491955: mov      ebp, esp
0x491957: add      esp, -0x2c
0x49195A: push     ebx
0x49195B: xor      ebx, ebx
0x49195D: mov      dword ptr [ebp - 0x2c], ebx
0x491960: mov      dword ptr [ebp - 8], ecx
0x491963: mov      dword ptr [ebp - 0x18], edx
0x491966: mov      dword ptr [ebp - 4], eax
0x491969: xor      eax, eax
0x49196B: push     ebp
0x49196C: push     0x491ad4
0x491971: push     dword ptr fs:[eax]
0x491974: mov      dword ptr fs:[eax], esp
0x491977: mov      eax, dword ptr [ebp - 4]
0x49197A: mov      eax, dword ptr [eax + 0x464]
0x491980: mov      dword ptr [ebp - 0x10], eax
0x491983: mov      eax, dword ptr [ebp - 0x10]
0x491986: mov      edx, dword ptr [eax + 0x258]
0x49198C: mov      eax, dword ptr [ebp - 0x10]
0x49198F: mov      eax, dword ptr [eax + 0x254]
0x491995: call     0x45351c
0x49199A: mov      dword ptr [ebp - 0xc], eax
0x49199D: cmp      dword ptr [ebp - 0xc], 0
0x4919A1: je       0x4919e0
0x4919A3: lea      edx, [ebp - 0x28]
0x4919A6: mov      eax, dword ptr [ebp - 0xc]
0x4919A9: call     0x44d8ec
0x4919AE: lea      eax, [ebp - 0x28]
0x4919B1: push     eax
0x4919B2: mov      eax, dword ptr [ebp - 0xc]
0x4919B5: push     eax
0x4919B6: push     1
0x4919B8: mov      eax, dword ptr [ebp - 0x10]
0x4919BB: mov      edx, dword ptr [eax + 0x34]
0x4919BE: mov      eax, dword ptr [ebp - 0x10]
0x4919C1: call     0x456fdc
0x4919C6: push     eax
0x4919C7: mov      eax, dword ptr [ebp - 0x10]
0x4919CA: mov      edx, dword ptr [eax + 0x30]
0x4919CD: mov      eax, dword ptr [ebp - 0x10]
0x4919D0: call     0x456f94
0x4919D5: mov      edx, eax
0x4919D7: mov      eax, dword ptr [ebp - 8]
0x4919DA: pop      ecx
0x4919DB: call     0x44d6cc
0x4919E0: mov      eax, dword ptr [ebp - 8]
0x4919E3: call     0x44d8b4
0x4919E8: mov      dword ptr [ebp - 0x14], eax
0x4919EB: push     1
0x4919ED: mov      eax, dword ptr [ebp - 0x14]
0x4919F0: call     0x41834c
0x4919F5: push     eax
0x4919F6: call     0x406434
0x4919FB: mov      edx, 0xffffff
0x491A00: mov      eax, dword ptr [ebp - 0x14]
0x491A03: mov      eax, dword ptr [eax + 0xc]
0x491A06: call     0x4173fc
0x491A0B: lea      edx, [ebp - 0x2c]
0x491A0E: mov      eax, dword ptr [0x4a3efc] ; *0x4A3EFC->0x4F9180
0x491A13: mov      eax, dword ptr [eax]
0x491A15: call     0x49f640
0x491A1A: mov      eax, dword ptr [ebp - 0x2c]
0x491A1D: push     eax
0x491A1E: mov      eax, dword ptr [ebp - 0x10]
0x491A21: mov      edx, dword ptr [eax + 0x34]
0x491A24: add      edx, 0xc4
0x491A2A: sub      edx, 0x41
0x491A2D: mov      eax, dword ptr [ebp - 0x10]
0x491A30: call     0x456fdc
0x491A35: push     eax
0x491A36: mov      eax, dword ptr [ebp - 0x10]
0x491A39: mov      edx, dword ptr [eax + 0x30]
0x491A3C: add      edx, 0x40
0x491A3F: mov      eax, dword ptr [ebp - 0x10]
0x491A42: call     0x456f94
0x491A47: mov      edx, eax
0x491A49: mov      eax, dword ptr [ebp - 0x14]
0x491A4C: pop      ecx
0x491A4D: call     0x4180dc
0x491A52: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x491A57: mov      eax, dword ptr [eax]
0x491A59: mov      eax, dword ptr [eax + 0x418]
0x491A5F: push     eax
0x491A60: mov      eax, dword ptr [ebp - 0x10]
0x491A63: mov      edx, dword ptr [eax + 0x34]
0x491A66: add      edx, 3
0x491A69: mov      eax, dword ptr [ebp - 0x10]
0x491A6C: call     0x456fdc
0x491A71: add      eax, 3
0x491A74: push     eax
0x491A75: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x491A7A: mov      eax, dword ptr [eax]
0x491A7C: mov      edx, dword ptr [eax + 0x418]
0x491A82: mov      eax, dword ptr [ebp - 0x14]
0x491A85: call     0x4181ac
0x491A8A: mov      edx, 0x6a
0x491A8F: sub      edx, eax
0x491A91: sar      edx, 1
0x491A93: jns      0x491a98
0x491A95: adc      edx, 0
0x491A98: mov      eax, dword ptr [ebp - 0x10]
0x491A9B: mov      eax, dword ptr [eax + 0x30]
0x491A9E: add      eax, 0x3b
0x491AA1: add      edx, eax
0x491AA3: mov      eax, dword ptr [ebp - 0x10]
0x491AA6: call     0x456f94
0x491AAB: mov      edx, eax
0x491AAD: mov      eax, dword ptr [ebp - 0x14]
0x491AB0: pop      ecx
0x491AB1: call     0x4180dc
0x491AB6: mov      eax, dword ptr [ebp - 0x14]
0x491AB9: call     0x44d104
0x491ABE: xor      eax, eax
0x491AC0: pop      edx
0x491AC1: pop      ecx
0x491AC2: pop      ecx
0x491AC3: mov      dword ptr fs:[eax], edx
0x491AC6: push     0x491adb
0x491ACB: lea      eax, [ebp - 0x2c]
0x491ACE: call     0x403a58
0x491AD3: ret      
0x491AD4: jmp      0x403518
0x491AD9: jmp      0x491acb
0x491ADB: pop      ebx
0x491ADC: mov      esp, ebp
0x491ADE: pop      ebp
0x491ADF: ret      
0x491AE0: push     ebp
0x491AE1: mov      ebp, esp
0x491AE3: add      esp, -0x38
0x491AE6: push     esi
0x491AE7: push     edi
0x491AE8: mov      esi, edx
0x491AEA: lea      edi, [ebp - 0x38]
0x491AED: mov      ecx, 0xd
0x491AF2: rep movsd dword ptr es:[edi], dword ptr [esi]
0x491AF4: mov      dword ptr [ebp - 4], eax
0x491AF7: mov      eax, dword ptr [0x4a409c] ; *0x4A409C->0x4F9188
0x491AFC: cmp      byte ptr [eax], 0
0x491AFF: jne      0x491b36
0x491B01: mov      eax, dword ptr [0x4a3dcc] ; *0x4A3DCC->0x4F933C
0x491B06: mov      edi, eax
0x491B08: lea      esi, [ebp - 0x38]
0x491B0B: mov      ecx, 0xd
0x491B10: rep movsd dword ptr es:[edi], dword ptr [esi]
0x491B12: mov      edx, dword ptr [0x4a3dcc] ; *0x4A3DCC->0x4F933C
0x491B18: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x491B1D: mov      eax, dword ptr [eax]
0x491B1F: call     0x466854
0x491B24: call     0x406154
0x491B29: add      eax, 0xfa0
0x491B2E: mov      edx, dword ptr [0x4a403c] ; *0x4A403C->0x4F94EC
0x491B34: mov      dword ptr [edx], eax
0x491B36: pop      edi
0x491B37: pop      esi
0x491B38: mov      esp, ebp
0x491B3A: pop      ebp
0x491B3B: ret      
0x491B3C: push     ebp
0x491B3D: mov      ebp, esp
0x491B3F: add      esp, -0x48
0x491B42: push     esi
0x491B43: push     edi
0x491B44: mov      dword ptr [ebp - 8], ecx
0x491B47: mov      dword ptr [ebp - 0x14], edx
0x491B4A: mov      dword ptr [ebp - 4], eax
0x491B4D: mov      eax, dword ptr [0x4a409c] ; *0x4A409C->0x4F9188
0x491B52: cmp      byte ptr [eax], 0
0x491B55: jne      0x491cad
0x491B5B: call     0x406154
0x491B60: mov      edx, dword ptr [0x4a403c] ; *0x4A403C->0x4F94EC
0x491B66: cmp      eax, dword ptr [edx]
0x491B68: jbe      0x491cad
0x491B6E: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x491B73: cmp      byte ptr [eax], 0
0x491B76: jne      0x491c09
0x491B7C: mov      eax, dword ptr [ebp - 4]
0x491B7F: mov      eax, dword ptr [eax + 0x46c]
0x491B85: mov      eax, dword ptr [eax + 0x260]
0x491B8B: imul     dword ptr [ebp + 0xc]
0x491B8E: add      eax, dword ptr [ebp - 8]
0x491B91: mov      dword ptr [ebp - 0x10], eax
0x491B94: mov      eax, dword ptr [ebp - 0x10]
0x491B97: sub      eax, 0xa
0x491B9A: jae      0x491ca8
0x491BA0: imul     eax, dword ptr [ebp - 0x10], 0xd
0x491BA4: mov      edx, dword ptr [0x4a4328] ; *0x4A4328->0x4F8B60
0x491BAA: cmp      byte ptr [edx + eax*4], 0
0x491BAE: je       0x491ca8
0x491BB4: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x491BB9: mov      byte ptr [eax], 1
0x491BBC: mov      eax, dword ptr [ebp - 0x10]
0x491BBF: neg      eax
0x491BC1: sub      eax, 0x14
0x491BC4: mov      edx, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x491BCA: mov      dword ptr [edx], eax
0x491BCC: imul     eax, dword ptr [ebp - 0x10], 0xd
0x491BD0: mov      edx, dword ptr [0x4a4328] ; *0x4A4328->0x4F8B60
0x491BD6: mov      ecx, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x491BDC: lea      esi, [edx + eax*4]
0x491BDF: lea      edi, [ecx + 4]
0x491BE2: mov      ecx, 0xd
0x491BE7: rep movsd dword ptr es:[edi], dword ptr [esi]
0x491BE9: imul     eax, dword ptr [ebp - 0x10], 0xd
0x491BED: mov      edx, dword ptr [0x4a4328] ; *0x4A4328->0x4F8B60
0x491BF3: mov      byte ptr [edx + eax*4], 0
0x491BF7: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x491BFC: add      eax, 4
0x491BFF: call     0x495940
0x491C04: jmp      0x491ca8
0x491C09: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x491C0E: mov      eax, dword ptr [eax]
0x491C10: mov      dword ptr [ebp - 0xc], eax
0x491C13: cmp      dword ptr [ebp - 0xc], 0
0x491C17: jge      0x491c25
0x491C19: cmp      dword ptr [ebp - 0xc], -0x14
0x491C1D: jg       0x491c93
0x491C1F: cmp      dword ptr [ebp - 0xc], -0x1e
0x491C23: jle      0x491c93
0x491C25: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x491C2A: add      eax, 4
0x491C2D: call     0x495940
0x491C32: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x491C37: mov      byte ptr [eax], 0
0x491C3A: cmp      dword ptr [ebp - 0xc], 0
0x491C3E: jl       0x491c7d
0x491C40: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x491C45: mov      edx, dword ptr [0x4a3dcc] ; *0x4A3DCC->0x4F933C
0x491C4B: lea      esi, [eax + 4]
0x491C4E: mov      edi, edx
0x491C50: mov      ecx, 0xd
0x491C55: rep movsd dword ptr es:[edi], dword ptr [esi]
0x491C57: mov      edx, dword ptr [0x4a3dcc] ; *0x4A3DCC->0x4F933C
0x491C5D: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x491C62: mov      eax, dword ptr [eax]
0x491C64: call     0x4667ac
0x491C69: call     0x406154
0x491C6E: add      eax, 0xfa0
0x491C73: mov      edx, dword ptr [0x4a403c] ; *0x4A403C->0x4F94EC
0x491C79: mov      dword ptr [edx], eax
0x491C7B: jmp      0x491c8a
0x491C7D: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x491C82: add      eax, 4
0x491C85: call     0x49fde4
0x491C8A: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x491C8F: mov      byte ptr [eax + 4], 0
0x491C93: cmp      dword ptr [ebp - 0xc], -0x62
0x491C97: jne      0x491ca8
0x491C99: push     0
0x491C9B: xor      ecx, ecx
0x491C9D: mov      edx, dword ptr [ebp - 4]
0x491CA0: mov      eax, dword ptr [ebp - 4]
0x491CA3: call     0x491fb0
0x491CA8: call     0x49faa4
0x491CAD: pop      edi
0x491CAE: pop      esi
0x491CAF: mov      esp, ebp
0x491CB1: pop      ebp
0x491CB2: ret      8
0x491CB5: lea      eax, [eax]
0x491CB8: push     ebp
0x491CB9: mov      ebp, esp
0x491CBB: add      esp, -0x38
0x491CBE: push     esi
0x491CBF: push     edi
0x491CC0: mov      esi, dword ptr [ebp + 0x10]
0x491CC3: lea      edi, [ebp - 0x28]
0x491CC6: push     ecx
0x491CC7: mov      ecx, 4
0x491CCC: rep movsd dword ptr es:[edi], dword ptr [esi]
0x491CCE: pop      ecx
0x491CCF: mov      dword ptr [ebp - 8], ecx
0x491CD2: mov      dword ptr [ebp - 0x18], edx
0x491CD5: mov      dword ptr [ebp - 4], eax
0x491CD8: mov      eax, dword ptr [ebp - 4]
0x491CDB: mov      eax, dword ptr [eax + 0x46c]
0x491CE1: mov      eax, dword ptr [eax + 0x260]
0x491CE7: imul     dword ptr [ebp + 0x14]
0x491CEA: add      eax, dword ptr [ebp - 8]
0x491CED: mov      dword ptr [ebp - 0xc], eax
0x491CF0: mov      eax, dword ptr [ebp - 0xc]
0x491CF3: sub      eax, 0xa
0x491CF6: jae      0x491db3
0x491CFC: imul     eax, dword ptr [ebp - 0xc], 0xd
0x491D00: mov      edx, dword ptr [0x4a4328] ; *0x4A4328->0x4F8B60
0x491D06: cmp      byte ptr [edx + eax*4], 0
0x491D0A: je       0x491db3
0x491D10: imul     eax, dword ptr [ebp - 0xc], 0xd
0x491D14: mov      edx, dword ptr [0x4a4328] ; *0x4A4328->0x4F8B60
0x491D1A: movzx    edx, word ptr [edx + eax*4 + 0x16]
0x491D1F: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x491D24: mov      eax, dword ptr [eax]
0x491D26: mov      eax, dword ptr [eax + 0x324]
0x491D2C: call     0x45351c
0x491D31: mov      dword ptr [ebp - 0x10], eax
0x491D34: cmp      dword ptr [ebp - 0x10], 0
0x491D38: je       0x491db3
0x491D3A: mov      eax, dword ptr [ebp - 4]
0x491D3D: mov      eax, dword ptr [eax + 0x46c]
0x491D43: mov      dword ptr [ebp - 0x14], eax
0x491D46: lea      edx, [ebp - 0x38]
0x491D49: mov      eax, dword ptr [ebp - 0x10]
0x491D4C: call     0x44d8ec
0x491D51: lea      eax, [ebp - 0x38]
0x491D54: push     eax
0x491D55: mov      eax, dword ptr [ebp - 0x10]
0x491D58: push     eax
0x491D59: push     1
0x491D5B: mov      eax, dword ptr [ebp - 0x10]
0x491D5E: call     0x44d944
0x491D63: mov      edx, dword ptr [ebp - 0x14]
0x491D66: mov      edx, dword ptr [edx + 0x26c]
0x491D6C: sub      edx, eax
0x491D6E: sar      edx, 1
0x491D70: jns      0x491d75
0x491D72: adc      edx, 0
0x491D75: add      edx, dword ptr [ebp - 0x24]
0x491D78: inc      edx
0x491D79: mov      eax, dword ptr [ebp - 0x14]
0x491D7C: call     0x456fdc
0x491D81: push     eax
0x491D82: mov      eax, dword ptr [ebp - 0x10]
0x491D85: call     0x44dc60
0x491D8A: mov      edx, dword ptr [ebp - 0x14]
0x491D8D: mov      edx, dword ptr [edx + 0x268]
0x491D93: sub      edx, eax
0x491D95: sar      edx, 1
0x491D97: jns      0x491d9c
0x491D99: adc      edx, 0
0x491D9C: add      edx, dword ptr [ebp - 0x28]
0x491D9F: dec      edx
0x491DA0: mov      eax, dword ptr [ebp - 0x14]
0x491DA3: call     0x456f94
0x491DA8: mov      edx, eax
0x491DAA: mov      eax, dword ptr [ebp + 8]
0x491DAD: pop      ecx
0x491DAE: call     0x44d6cc
0x491DB3: pop      edi
0x491DB4: pop      esi
0x491DB5: mov      esp, ebp
0x491DB7: pop      ebp
0x491DB8: ret      0x10
0x491DBB: nop      
0x491DBC: push     ebp
0x491DBD: mov      ebp, esp
0x491DBF: add      esp, -0x10
0x491DC2: push     esi
0x491DC3: push     edi
0x491DC4: mov      dword ptr [ebp - 8], ecx
0x491DC7: mov      dword ptr [ebp - 0x10], edx
0x491DCA: mov      dword ptr [ebp - 4], eax
0x491DCD: mov      eax, dword ptr [ebp - 4]
0x491DD0: mov      eax, dword ptr [eax + 0x46c]
0x491DD6: mov      eax, dword ptr [eax + 0x260]
0x491DDC: imul     dword ptr [ebp + 0xc]
0x491DDF: add      eax, dword ptr [ebp - 8]
0x491DE2: mov      dword ptr [ebp - 0xc], eax
0x491DE5: mov      eax, dword ptr [ebp - 0xc]
0x491DE8: sub      eax, 0xa
0x491DEB: jae      0x491e09
0x491DED: imul     eax, dword ptr [ebp - 0xc], 0xd
0x491DF1: mov      edx, dword ptr [0x4a4328] ; *0x4A4328->0x4F8B60
0x491DF7: mov      ecx, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x491DFD: lea      esi, [edx + eax*4]
0x491E00: mov      edi, ecx
0x491E02: mov      ecx, 0xd
0x491E07: rep movsd dword ptr es:[edi], dword ptr [esi]
0x491E09: pop      edi
0x491E0A: pop      esi
0x491E0B: mov      esp, ebp
0x491E0D: pop      ebp
0x491E0E: ret      8
0x491E11: lea      eax, [eax]
0x491E14: push     ebp
0x491E15: mov      ebp, esp
0x491E17: add      esp, -0x38
0x491E1A: push     esi
0x491E1B: push     edi
0x491E1C: mov      esi, dword ptr [ebp + 0x10]
0x491E1F: lea      edi, [ebp - 0x28]
0x491E22: push     ecx
0x491E23: mov      ecx, 4
0x491E28: rep movsd dword ptr es:[edi], dword ptr [esi]
0x491E2A: pop      ecx
0x491E2B: mov      dword ptr [ebp - 8], ecx
0x491E2E: mov      dword ptr [ebp - 0x18], edx
0x491E31: mov      dword ptr [ebp - 4], eax
0x491E34: mov      eax, dword ptr [ebp - 4]
0x491E37: mov      eax, dword ptr [eax + 0x468]
0x491E3D: mov      eax, dword ptr [eax + 0x260]
0x491E43: imul     dword ptr [ebp + 0x14]
0x491E46: add      eax, dword ptr [ebp - 8]
0x491E49: mov      dword ptr [ebp - 0xc], eax
0x491E4C: mov      eax, dword ptr [ebp - 0xc]
0x491E4F: sub      eax, 0x14
0x491E52: jae      0x491f0f
0x491E58: imul     eax, dword ptr [ebp - 0xc], 0xd
0x491E5C: mov      edx, dword ptr [0x4a3d98] ; *0x4A3D98->0x4F8D68
0x491E62: cmp      byte ptr [edx + eax*4], 0
0x491E66: je       0x491f0f
0x491E6C: imul     eax, dword ptr [ebp - 0xc], 0xd
0x491E70: mov      edx, dword ptr [0x4a3d98] ; *0x4A3D98->0x4F8D68
0x491E76: movzx    edx, word ptr [edx + eax*4 + 0x16]
0x491E7B: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x491E80: mov      eax, dword ptr [eax]
0x491E82: mov      eax, dword ptr [eax + 0x324]
0x491E88: call     0x45351c
0x491E8D: mov      dword ptr [ebp - 0x10], eax
0x491E90: cmp      dword ptr [ebp - 0x10], 0
0x491E94: je       0x491f0f
0x491E96: mov      eax, dword ptr [ebp - 4]
0x491E99: mov      eax, dword ptr [eax + 0x468]
0x491E9F: mov      dword ptr [ebp - 0x14], eax
0x491EA2: lea      edx, [ebp - 0x38]
0x491EA5: mov      eax, dword ptr [ebp - 0x10]
0x491EA8: call     0x44d8ec
0x491EAD: lea      eax, [ebp - 0x38]
0x491EB0: push     eax
0x491EB1: mov      eax, dword ptr [ebp - 0x10]
0x491EB4: push     eax
0x491EB5: push     1
0x491EB7: mov      eax, dword ptr [ebp - 0x10]
0x491EBA: call     0x44d944
0x491EBF: mov      edx, dword ptr [ebp - 0x14]
0x491EC2: mov      edx, dword ptr [edx + 0x26c]
0x491EC8: sub      edx, eax
0x491ECA: sar      edx, 1
0x491ECC: jns      0x491ed1
0x491ECE: adc      edx, 0
0x491ED1: add      edx, dword ptr [ebp - 0x24]
0x491ED4: inc      edx
0x491ED5: mov      eax, dword ptr [ebp - 0x14]
0x491ED8: call     0x456fdc
0x491EDD: push     eax
0x491EDE: mov      eax, dword ptr [ebp - 0x10]
0x491EE1: call     0x44dc60
0x491EE6: mov      edx, dword ptr [ebp - 0x14]
0x491EE9: mov      edx, dword ptr [edx + 0x268]
0x491EEF: sub      edx, eax
0x491EF1: sar      edx, 1
0x491EF3: jns      0x491ef8
0x491EF5: adc      edx, 0
0x491EF8: add      edx, dword ptr [ebp - 0x28]
0x491EFB: dec      edx
0x491EFC: mov      eax, dword ptr [ebp - 0x14]
0x491EFF: call     0x456f94
0x491F04: mov      edx, eax
0x491F06: mov      eax, dword ptr [ebp + 8]
0x491F09: pop      ecx
0x491F0A: call     0x44d6cc
0x491F0F: pop      edi
0x491F10: pop      esi
0x491F11: mov      esp, ebp
0x491F13: pop      ebp
0x491F14: ret      0x10
0x491F17: nop      
0x491F18: push     ebp
0x491F19: mov      ebp, esp
0x491F1B: add      esp, -0x10
0x491F1E: push     esi
0x491F1F: push     edi
0x491F20: mov      dword ptr [ebp - 8], ecx
0x491F23: mov      dword ptr [ebp - 0x10], edx
0x491F26: mov      dword ptr [ebp - 4], eax
0x491F29: mov      eax, dword ptr [ebp - 4]
0x491F2C: mov      eax, dword ptr [eax + 0x468]
0x491F32: mov      eax, dword ptr [eax + 0x260]
0x491F38: imul     dword ptr [ebp + 0xc]
0x491F3B: add      eax, dword ptr [ebp - 8]
0x491F3E: mov      dword ptr [ebp - 0xc], eax
0x491F41: mov      eax, dword ptr [ebp - 0xc]
0x491F44: sub      eax, 0x14
0x491F47: jae      0x491f65
0x491F49: imul     eax, dword ptr [ebp - 0xc], 0xd
0x491F4D: mov      edx, dword ptr [0x4a3d98] ; *0x4A3D98->0x4F8D68
0x491F53: mov      ecx, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x491F59: lea      esi, [edx + eax*4]
0x491F5C: mov      edi, ecx
0x491F5E: mov      ecx, 0xd
0x491F63: rep movsd dword ptr es:[edi], dword ptr [esi]
0x491F65: pop      edi
0x491F66: pop      esi
0x491F67: mov      esp, ebp
0x491F69: pop      ebp
0x491F6A: ret      8
0x491F6D: lea      eax, [eax]
0x491F70: push     ebp
0x491F71: mov      ebp, esp
0x491F73: push     ecx
0x491F74: mov      dword ptr [ebp - 4], eax
0x491F77: mov      eax, dword ptr [0x4a409c] ; *0x4A409C->0x4F9188
0x491F7C: cmp      byte ptr [eax], 0
0x491F7F: jne      0x491fab
0x491F81: mov      eax, dword ptr [0x4a3efc] ; *0x4A3EFC->0x4F9180
0x491F86: cmp      dword ptr [eax], 0
0x491F89: jle      0x491fab
0x491F8B: call     0x406154
0x491F90: add      eax, 0xfa0
0x491F95: mov      edx, dword ptr [0x4a403c] ; *0x4A403C->0x4F94EC
0x491F9B: mov      dword ptr [edx], eax
0x491F9D: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x491FA2: mov      eax, dword ptr [eax]
0x491FA4: xor      edx, edx
0x491FA6: call     0x4668fc
0x491FAB: pop      ecx
0x491FAC: pop      ebp
0x491FAD: ret      
0x491FAE: mov      eax, eax
0x491FB0: push     ebp
0x491FB1: mov      ebp, esp
0x491FB3: add      esp, -0x1c
0x491FB6: push     ebx
0x491FB7: push     esi
0x491FB8: push     edi
0x491FB9: xor      ebx, ebx
0x491FBB: mov      dword ptr [ebp - 0x18], ebx
0x491FBE: mov      dword ptr [ebp - 0xc], ebx
0x491FC1: mov      dword ptr [ebp - 0x14], ecx
0x491FC4: mov      dword ptr [ebp - 0x10], edx
0x491FC7: mov      dword ptr [ebp - 4], eax
0x491FCA: xor      eax, eax
0x491FCC: push     ebp
0x491FCD: push     0x492164
0x491FD2: push     dword ptr fs:[eax]
0x491FD5: mov      dword ptr fs:[eax], esp
0x491FD8: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x491FDD: cmp      dword ptr [eax], 0
0x491FE0: je       0x492146
0x491FE6: mov      eax, dword ptr [0x4a409c] ; *0x4A409C->0x4F9188
0x491FEB: cmp      byte ptr [eax], 0
0x491FEE: jne      0x492146
0x491FF4: call     0x406154
0x491FF9: mov      edx, dword ptr [0x4a403c] ; *0x4A403C->0x4F94EC
0x491FFF: cmp      eax, dword ptr [edx]
0x492001: jbe      0x492146
0x492007: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x49200C: cmp      byte ptr [eax], 0
0x49200F: jne      0x492050
0x492011: mov      eax, dword ptr [0x4a3efc] ; *0x4A3EFC->0x4F9180
0x492016: cmp      dword ptr [eax], 0
0x492019: jle      0x492146
0x49201F: mov      eax, 0x6a
0x492024: call     0x495794
0x492029: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x49202E: mov      byte ptr [eax], 1
0x492031: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x492036: mov      dword ptr [eax], 0xffffff9f
0x49203C: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x492041: lea      edi, [eax + 4]
0x492044: mov      esi, 0x492174
0x492049: movsd    dword ptr es:[edi], dword ptr [esi]
0x49204A: movsb    byte ptr es:[edi], byte ptr [esi]
0x49204B: jmp      0x492146
0x492050: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x492055: cmp      dword ptr [eax], -0x61
0x492058: je       0x492068
0x49205A: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x49205F: cmp      dword ptr [eax], -0x62
0x492062: jne      0x492146
0x492068: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x49206D: cmp      dword ptr [eax], -0x62
0x492070: jne      0x492135
0x492076: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x49207B: add      eax, 4
0x49207E: mov      edx, 0x492174
0x492083: xor      ecx, ecx
0x492085: mov      cl, byte ptr [eax]
0x492087: inc      ecx
0x492088: call     0x402ae4
0x49208D: jne      0x492135
0x492093: mov      eax, dword ptr [ebp - 4]
0x492096: mov      dword ptr [eax + 0x620], 1
0x4920A0: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x4920A5: mov      byte ptr [eax], 0
0x4920A8: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x4920AD: mov      byte ptr [eax + 4], 0
0x4920B1: mov      cx, word ptr [0x49217c]
0x4920B8: mov      edx, 0x492188 ; "你想支付多少金币？"
0x4920BD: mov      eax, dword ptr [ebp - 4]
0x4920C0: call     0x486df0
0x4920C5: push     0
0x4920C7: lea      eax, [ebp - 0x18]
0x4920CA: push     eax
0x4920CB: mov      byte ptr [ebp - 0x1c], 0x20
0x4920CF: lea      ecx, [ebp - 0x1c]
0x4920D2: lea      edx, [ebp - 0xc]
0x4920D5: mov      eax, dword ptr [ebp - 4]
0x4920D8: mov      eax, dword ptr [eax + 0x784]
0x4920DE: call     0x452708
0x4920E3: xor      edx, edx
0x4920E5: mov      eax, dword ptr [ebp - 0xc]
0x4920E8: call     0x4520e8
0x4920ED: mov      dword ptr [ebp - 8], eax
0x4920F0: mov      eax, dword ptr [0x4a3efc] ; *0x4A3EFC->0x4F9180
0x4920F5: mov      eax, dword ptr [eax]
0x4920F7: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4920FD: mov      edx, dword ptr [edx]
0x4920FF: add      eax, dword ptr [edx + 0x58]
0x492102: cmp      eax, dword ptr [ebp - 8]
0x492105: jl       0x492130
0x492107: cmp      dword ptr [ebp - 8], 0
0x49210B: jle      0x492130
0x49210D: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x492112: mov      eax, dword ptr [eax]
0x492114: mov      edx, dword ptr [ebp - 8]
0x492117: call     0x4668fc
0x49211C: call     0x406154
0x492121: add      eax, 0xfa0
0x492126: mov      edx, dword ptr [0x4a403c] ; *0x4A403C->0x4F94EC
0x49212C: mov      dword ptr [edx], eax
0x49212E: jmp      0x492135
0x492130: xor      eax, eax
0x492132: mov      dword ptr [ebp - 8], eax
0x492135: mov      eax, dword ptr [0x4a412c] ; *0x4A412C->0x4F939C
0x49213A: mov      byte ptr [eax], 0
0x49213D: mov      eax, dword ptr [0x4a4130] ; *0x4A4130->0x4F93A0
0x492142: mov      byte ptr [eax + 4], 0
0x492146: xor      eax, eax
0x492148: pop      edx
0x492149: pop      ecx
0x49214A: pop      ecx
0x49214B: mov      dword ptr fs:[eax], edx
0x49214E: push     0x49216b
0x492153: lea      eax, [ebp - 0x18]
0x492156: call     0x403a58
0x49215B: lea      eax, [ebp - 0xc]
0x49215E: call     0x403a58
0x492163: ret      
0x492164: jmp      0x403518
0x492169: jmp      0x492153
0x49216B: pop      edi
0x49216C: pop      esi
0x49216D: pop      ebx
0x49216E: mov      esp, ebp
0x492170: pop      ebp
0x492171: ret      4
0x492174: add      al, 0xbd
0x492177: mov      cl, 0xd2
0x492179: add      byte ptr [eax], al
0x49217B: add      byte ptr [eax + eax], dl
0x49217E: add      byte ptr [eax], al
0x492188: [STR len=18] "你想支付多少金币？"
0x49219C: push     ebp
0x49219D: mov      ebp, esp
0x49219F: add      esp, 0xffffff64
0x4921A5: push     ebx
0x4921A6: push     esi
0x4921A7: push     edi
0x4921A8: xor      ebx, ebx
0x4921AA: mov      dword ptr [ebp - 0x78], ebx
0x4921AD: mov      dword ptr [ebp - 0x7c], ebx
0x4921B0: mov      dword ptr [ebp - 0x34], ebx
0x4921B3: mov      dword ptr [ebp - 0x38], ebx
0x4921B6: mov      dword ptr [ebp - 0x3c], ebx
0x4921B9: mov      dword ptr [ebp - 0x40], ebx
0x4921BC: mov      dword ptr [ebp - 8], ecx
0x4921BF: mov      dword ptr [ebp - 0x54], edx
0x4921C2: mov      dword ptr [ebp - 4], eax
0x4921C5: xor      eax, eax
0x4921C7: push     ebp
0x4921C8: push     0x492820
0x4921CD: push     dword ptr fs:[eax]
0x4921D0: mov      dword ptr fs:[eax], esp
0x4921D3: mov      eax, dword ptr [ebp - 4]
0x4921D6: mov      eax, dword ptr [eax + 0x490]
0x4921DC: mov      dword ptr [ebp - 0x48], eax
0x4921DF: mov      eax, dword ptr [ebp - 0x48]
0x4921E2: mov      edx, dword ptr [eax + 0x258]
0x4921E8: mov      eax, dword ptr [ebp - 0x48]
0x4921EB: mov      eax, dword ptr [eax + 0x254]
0x4921F1: call     0x45351c
0x4921F6: mov      dword ptr [ebp - 0x2c], eax
0x4921F9: cmp      dword ptr [ebp - 0x2c], 0
0x4921FD: je       0x49223c
0x4921FF: lea      edx, [ebp - 0x74]
0x492202: mov      eax, dword ptr [ebp - 0x2c]
0x492205: call     0x44d8ec
0x49220A: lea      eax, [ebp - 0x74]
0x49220D: push     eax
0x49220E: mov      eax, dword ptr [ebp - 0x2c]
0x492211: push     eax
0x492212: push     1
0x492214: mov      eax, dword ptr [ebp - 0x48]
0x492217: mov      edx, dword ptr [eax + 0x34]
0x49221A: mov      eax, dword ptr [ebp - 0x48]
0x49221D: call     0x456fdc
0x492222: push     eax
0x492223: mov      eax, dword ptr [ebp - 0x48]
0x492226: mov      edx, dword ptr [eax + 0x30]
0x492229: mov      eax, dword ptr [ebp - 0x48]
0x49222C: call     0x456f94
0x492231: mov      edx, eax
0x492233: mov      eax, dword ptr [ebp - 8]
0x492236: pop      ecx
0x492237: call     0x44d6cc
0x49223C: mov      eax, dword ptr [ebp - 4]
0x49223F: mov      eax, dword ptr [eax + 0x788]
0x492245: call     0x458cfc
0x49224A: and      eax, 0xff
0x49224F: and      eax, 1
0x492252: mov      dword ptr [ebp - 0x24], eax
0x492255: mov      eax, dword ptr [ebp - 4]
0x492258: mov      eax, dword ptr [eax + 0x788]
0x49225E: call     0x458ce0
0x492263: and      eax, 0xff
0x492268: mov      dword ptr [ebp - 0x28], eax
0x49226B: cmp      dword ptr [ebp - 0x24], 1
0x49226F: jne      0x49227a
0x492271: mov      dword ptr [ebp - 0xc], 0x179
0x492278: jmp      0x492281
0x49227A: mov      dword ptr [ebp - 0xc], 0x178
0x492281: mov      eax, dword ptr [ebp - 0x48]
0x492284: mov      eax, dword ptr [eax + 0x30]
0x492287: add      eax, 0x26
0x49228A: mov      dword ptr [ebp - 0x10], eax
0x49228D: mov      eax, dword ptr [ebp - 0x48]
0x492290: mov      eax, dword ptr [eax + 0x34]
0x492293: add      eax, 0x34
0x492296: mov      dword ptr [ebp - 0x14], eax
0x492299: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x49229E: mov      eax, dword ptr [eax]
0x4922A0: mov      eax, dword ptr [eax + 0x308]
0x4922A6: mov      edx, dword ptr [ebp - 0xc]
0x4922A9: call     0x45351c
0x4922AE: mov      dword ptr [ebp - 0x2c], eax
0x4922B1: cmp      dword ptr [ebp - 0x2c], 0
0x4922B5: je       0x4922ee
0x4922B7: lea      edx, [ebp - 0x74]
0x4922BA: mov      eax, dword ptr [ebp - 0x2c]
0x4922BD: call     0x44d8ec
0x4922C2: lea      eax, [ebp - 0x74]
0x4922C5: push     eax
0x4922C6: mov      eax, dword ptr [ebp - 0x2c]
0x4922C9: push     eax
0x4922CA: push     0
0x4922CC: mov      edx, dword ptr [ebp - 0x14]
0x4922CF: mov      eax, dword ptr [ebp - 0x48]
0x4922D2: call     0x456fdc
0x4922D7: push     eax
0x4922D8: mov      edx, dword ptr [ebp - 0x10]
0x4922DB: mov      eax, dword ptr [ebp - 0x48]
0x4922DE: call     0x456f94
0x4922E3: mov      edx, eax
0x4922E5: mov      eax, dword ptr [ebp - 8]
0x4922E8: pop      ecx
0x4922E9: call     0x44d6cc
0x4922EE: sub      dword ptr [ebp - 0x10], 7
0x4922F2: add      dword ptr [ebp - 0x14], 0x2c
0x4922F6: mov      eax, dword ptr [ebp - 0x28]
0x4922F9: sar      eax, 1
0x4922FB: jns      0x492300
0x4922FD: adc      eax, 0
0x492300: add      eax, 0x1b8
0x492305: mov      dword ptr [ebp - 0x18], eax
0x492308: cmp      dword ptr [ebp - 0x24], 1
0x49230C: jne      0x492320
0x49230E: mov      eax, dword ptr [ebp - 0x28]
0x492311: sar      eax, 1
0x492313: jns      0x492318
0x492315: adc      eax, 0
0x492318: add      eax, 0x1e0
0x49231D: mov      dword ptr [ebp - 0x18], eax
0x492320: cmp      dword ptr [ebp - 0x18], 0
0x492324: jle      0x492388
0x492326: lea      eax, [ebp - 0x20]
0x492329: push     eax
0x49232A: lea      ecx, [ebp - 0x1c]
0x49232D: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x492332: mov      eax, dword ptr [eax]
0x492334: mov      eax, dword ptr [eax + 0x308]
0x49233A: mov      edx, dword ptr [ebp - 0x18]
0x49233D: call     0x453a48
0x492342: mov      dword ptr [ebp - 0x2c], eax
0x492345: cmp      dword ptr [ebp - 0x2c], 0
0x492349: je       0x492388
0x49234B: lea      edx, [ebp - 0x74]
0x49234E: mov      eax, dword ptr [ebp - 0x2c]
0x492351: call     0x44d8ec
0x492356: lea      eax, [ebp - 0x74]
0x492359: push     eax
0x49235A: mov      eax, dword ptr [ebp - 0x2c]
0x49235D: push     eax
0x49235E: push     1
0x492360: mov      edx, dword ptr [ebp - 0x14]
0x492363: add      edx, dword ptr [ebp - 0x20]
0x492366: mov      eax, dword ptr [ebp - 0x48]
0x492369: call     0x456fdc
0x49236E: push     eax
0x49236F: mov      edx, dword ptr [ebp - 0x10]
0x492372: add      edx, dword ptr [ebp - 0x1c]
0x492375: mov      eax, dword ptr [ebp - 0x48]
0x492378: call     0x456f94
0x49237D: mov      edx, eax
0x49237F: mov      eax, dword ptr [ebp - 8]
0x492382: pop      ecx
0x492383: call     0x44d6cc
0x492388: mov      eax, dword ptr [ebp - 4]
0x49238B: cmp      byte ptr [eax + 0x7c0], 0
0x492392: je       0x492409
0x492394: mov      eax, dword ptr [ebp - 4]
0x492397: movzx    eax, word ptr [eax + 0x7d6]
0x49239E: mov      dword ptr [ebp - 0x18], eax
0x4923A1: cmp      dword ptr [ebp - 0x18], 0
0x4923A5: jl       0x492409
0x4923A7: lea      eax, [ebp - 0x20]
0x4923AA: push     eax
0x4923AB: lea      ecx, [ebp - 0x1c]
0x4923AE: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4923B3: mov      eax, dword ptr [eax]
0x4923B5: mov      eax, dword ptr [eax + 0x32c]
0x4923BB: mov      edx, dword ptr [ebp - 0x18]
0x4923BE: call     0x453a48
0x4923C3: mov      dword ptr [ebp - 0x2c], eax
0x4923C6: cmp      dword ptr [ebp - 0x2c], 0
0x4923CA: je       0x492409
0x4923CC: lea      edx, [ebp - 0x74]
0x4923CF: mov      eax, dword ptr [ebp - 0x2c]
0x4923D2: call     0x44d8ec
0x4923D7: lea      eax, [ebp - 0x74]
0x4923DA: push     eax
0x4923DB: mov      eax, dword ptr [ebp - 0x2c]
0x4923DE: push     eax
0x4923DF: push     1
0x4923E1: mov      edx, dword ptr [ebp - 0x14]
0x4923E4: add      edx, dword ptr [ebp - 0x20]
0x4923E7: mov      eax, dword ptr [ebp - 0x48]
0x4923EA: call     0x456fdc
0x4923EF: push     eax
0x4923F0: mov      edx, dword ptr [ebp - 0x10]
0x4923F3: add      edx, dword ptr [ebp - 0x1c]
0x4923F6: mov      eax, dword ptr [ebp - 0x48]
0x4923F9: call     0x456f94
0x4923FE: mov      edx, eax
0x492400: mov      eax, dword ptr [ebp - 8]
0x492403: pop      ecx
0x492404: call     0x44d6cc
0x492409: mov      eax, dword ptr [ebp - 4]
0x49240C: cmp      byte ptr [eax + 0x7f4], 0
0x492413: je       0x49248a
0x492415: mov      eax, dword ptr [ebp - 4]
0x492418: movzx    eax, word ptr [eax + 0x80a]
0x49241F: mov      dword ptr [ebp - 0x18], eax
0x492422: cmp      dword ptr [ebp - 0x18], 0
0x492426: jl       0x49248a
0x492428: lea      eax, [ebp - 0x20]
0x49242B: push     eax
0x49242C: lea      ecx, [ebp - 0x1c]
0x49242F: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x492434: mov      eax, dword ptr [eax]
0x492436: mov      eax, dword ptr [eax + 0x32c]
0x49243C: mov      edx, dword ptr [ebp - 0x18]
0x49243F: call     0x453a48
0x492444: mov      dword ptr [ebp - 0x2c], eax
0x492447: cmp      dword ptr [ebp - 0x2c], 0
0x49244B: je       0x49248a
0x49244D: lea      edx, [ebp - 0x74]
0x492450: mov      eax, dword ptr [ebp - 0x2c]
0x492453: call     0x44d8ec
0x492458: lea      eax, [ebp - 0x74]
0x49245B: push     eax
0x49245C: mov      eax, dword ptr [ebp - 0x2c]
0x49245F: push     eax
0x492460: push     1
0x492462: mov      edx, dword ptr [ebp - 0x14]
0x492465: add      edx, dword ptr [ebp - 0x20]
0x492468: mov      eax, dword ptr [ebp - 0x48]
0x49246B: call     0x456fdc
0x492470: push     eax
0x492471: mov      edx, dword ptr [ebp - 0x10]
0x492474: add      edx, dword ptr [ebp - 0x1c]
0x492477: mov      eax, dword ptr [ebp - 0x48]
0x49247A: call     0x456f94
0x49247F: mov      edx, eax
0x492481: mov      eax, dword ptr [ebp - 8]
0x492484: pop      ecx
0x492485: call     0x44d6cc
0x49248A: mov      eax, dword ptr [ebp - 4]
0x49248D: cmp      byte ptr [eax + 0x890], 0
0x492494: je       0x49250b
0x492496: mov      eax, dword ptr [ebp - 4]
0x492499: movzx    eax, word ptr [eax + 0x8a6]
0x4924A0: mov      dword ptr [ebp - 0x18], eax
0x4924A3: cmp      dword ptr [ebp - 0x18], 0
0x4924A7: jl       0x49250b
0x4924A9: lea      eax, [ebp - 0x20]
0x4924AC: push     eax
0x4924AD: lea      ecx, [ebp - 0x1c]
0x4924B0: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4924B5: mov      eax, dword ptr [eax]
0x4924B7: mov      eax, dword ptr [eax + 0x32c]
0x4924BD: mov      edx, dword ptr [ebp - 0x18]
0x4924C0: call     0x453a48
0x4924C5: mov      dword ptr [ebp - 0x2c], eax
0x4924C8: cmp      dword ptr [ebp - 0x2c], 0
0x4924CC: je       0x49250b
0x4924CE: lea      edx, [ebp - 0x74]
0x4924D1: mov      eax, dword ptr [ebp - 0x2c]
0x4924D4: call     0x44d8ec
0x4924D9: lea      eax, [ebp - 0x74]
0x4924DC: push     eax
0x4924DD: mov      eax, dword ptr [ebp - 0x2c]
0x4924E0: push     eax
0x4924E1: push     1
0x4924E3: mov      edx, dword ptr [ebp - 0x14]
0x4924E6: add      edx, dword ptr [ebp - 0x20]
0x4924E9: mov      eax, dword ptr [ebp - 0x48]
0x4924EC: call     0x456fdc
0x4924F1: push     eax
0x4924F2: mov      edx, dword ptr [ebp - 0x10]
0x4924F5: add      edx, dword ptr [ebp - 0x1c]
0x4924F8: mov      eax, dword ptr [ebp - 0x48]
0x4924FB: call     0x456f94
0x492500: mov      edx, eax
0x492502: mov      eax, dword ptr [ebp - 8]
0x492505: pop      ecx
0x492506: call     0x44d6cc
0x49250B: mov      eax, dword ptr [0x4a4034] ; *0x4A4034->0x4F91FC
0x492510: cmp      byte ptr [eax], 0
0x492513: je       0x4926c5
0x492519: mov      eax, dword ptr [0x4a4034] ; *0x4A4034->0x4F91FC
0x49251E: mov      edx, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x492524: mov      esi, eax
0x492526: mov      edi, edx
0x492528: mov      ecx, 0xd
0x49252D: rep movsd dword ptr es:[edi], dword ptr [esi]
0x49252F: lea      eax, [ebp - 0x3c]
0x492532: push     eax
0x492533: lea      eax, [ebp - 0x40]
0x492536: push     eax
0x492537: lea      eax, [ebp - 0x41]
0x49253A: push     eax
0x49253B: push     0
0x49253D: lea      ecx, [ebp - 0x38]
0x492540: lea      edx, [ebp - 0x34]
0x492543: mov      eax, dword ptr [ebp - 4]
0x492546: call     0x48c3c8
0x49254B: cmp      dword ptr [ebp - 0x34], 0
0x49254F: je       0x4926bd
0x492555: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x49255A: cmp      word ptr [eax + 0x30], 0
0x49255F: jne      0x49256a
0x492561: mov      dword ptr [ebp - 0x30], 0xff
0x492568: jmp      0x492571
0x49256A: mov      dword ptr [ebp - 0x30], 0xffffff
0x492571: mov      eax, dword ptr [ebp - 8]
0x492574: call     0x44d8b4
0x492579: mov      dword ptr [ebp - 0x4c], eax
0x49257C: push     1
0x49257E: mov      eax, dword ptr [ebp - 0x4c]
0x492581: call     0x41834c
0x492586: push     eax
0x492587: call     0x406434
0x49258C: mov      eax, dword ptr [ebp - 0x4c]
0x49258F: mov      eax, dword ptr [eax + 0xc]
0x492592: mov      edx, 0xffff
0x492597: call     0x4173fc
0x49259C: mov      eax, dword ptr [ebp - 0x34]
0x49259F: push     eax
0x4925A0: mov      eax, dword ptr [ebp - 0x48]
0x4925A3: mov      edx, dword ptr [eax + 0x34]
0x4925A6: add      edx, 0x110
0x4925AC: mov      eax, dword ptr [ebp - 0x48]
0x4925AF: call     0x456fdc
0x4925B4: push     eax
0x4925B5: mov      eax, dword ptr [ebp - 0x48]
0x4925B8: mov      edx, dword ptr [eax + 0x30]
0x4925BB: add      edx, 0x25
0x4925BE: mov      eax, dword ptr [ebp - 0x48]
0x4925C1: call     0x456f94
0x4925C6: mov      edx, eax
0x4925C8: mov      eax, dword ptr [ebp - 0x4c]
0x4925CB: pop      ecx
0x4925CC: call     0x4180dc
0x4925D1: mov      eax, dword ptr [ebp - 0x4c]
0x4925D4: mov      eax, dword ptr [eax + 0xc]
0x4925D7: mov      edx, dword ptr [ebp - 0x30]
0x4925DA: call     0x4173fc
0x4925DF: mov      eax, dword ptr [ebp - 0x38]
0x4925E2: push     eax
0x4925E3: mov      eax, dword ptr [ebp - 0x48]
0x4925E6: mov      edx, dword ptr [eax + 0x34]
0x4925E9: add      edx, 0x110
0x4925EF: mov      eax, dword ptr [ebp - 0x48]
0x4925F2: call     0x456fdc
0x4925F7: push     eax
0x4925F8: mov      edx, dword ptr [ebp - 0x34]
0x4925FB: mov      eax, dword ptr [ebp - 0x4c]
0x4925FE: call     0x4181ac
0x492603: mov      edx, eax
0x492605: mov      eax, dword ptr [ebp - 0x48]
0x492608: mov      eax, dword ptr [eax + 0x30]
0x49260B: add      eax, 0x25
0x49260E: add      edx, eax
0x492610: mov      eax, dword ptr [ebp - 0x48]
0x492613: call     0x456f94
0x492618: mov      edx, eax
0x49261A: mov      eax, dword ptr [ebp - 0x4c]
0x49261D: pop      ecx
0x49261E: call     0x4180dc
0x492623: mov      eax, dword ptr [ebp - 0x3c]
0x492626: push     eax
0x492627: mov      edx, 0x492838 ; "A"
0x49262C: mov      eax, dword ptr [ebp - 0x4c]
0x49262F: call     0x4181c8
0x492634: mov      edx, eax
0x492636: mov      eax, dword ptr [ebp - 0x48]
0x492639: mov      eax, dword ptr [eax + 0x34]
0x49263C: add      eax, 0x110
0x492641: add      edx, eax
0x492643: add      edx, 2
0x492646: mov      eax, dword ptr [ebp - 0x48]
0x492649: call     0x456fdc
0x49264E: push     eax
0x49264F: mov      eax, dword ptr [ebp - 0x48]
0x492652: mov      edx, dword ptr [eax + 0x30]
0x492655: add      edx, 0x25
0x492658: mov      eax, dword ptr [ebp - 0x48]
0x49265B: call     0x456f94
0x492660: mov      edx, eax
0x492662: mov      eax, dword ptr [ebp - 0x4c]
0x492665: pop      ecx
0x492666: call     0x4180dc
0x49266B: mov      eax, dword ptr [ebp - 0x40]
0x49266E: push     eax
0x49266F: mov      edx, 0x492838 ; "A"
0x492674: mov      eax, dword ptr [ebp - 0x4c]
0x492677: call     0x4181c8
0x49267C: add      eax, 2
0x49267F: mov      edx, eax
0x492681: add      edx, edx
0x492683: mov      eax, dword ptr [ebp - 0x48]
0x492686: mov      eax, dword ptr [eax + 0x34]
0x492689: add      eax, 0x110
0x49268E: add      edx, eax
0x492690: mov      eax, dword ptr [ebp - 0x48]
0x492693: call     0x456fdc
0x492698: push     eax
0x492699: mov      eax, dword ptr [ebp - 0x48]
0x49269C: mov      edx, dword ptr [eax + 0x30]
0x49269F: add      edx, 0x25
0x4926A2: mov      eax, dword ptr [ebp - 0x48]
0x4926A5: call     0x456f94
0x4926AA: mov      edx, eax
0x4926AC: mov      eax, dword ptr [ebp - 0x4c]
0x4926AF: pop      ecx
0x4926B0: call     0x4180dc
0x4926B5: mov      eax, dword ptr [ebp - 0x4c]
0x4926B8: call     0x44d104
0x4926BD: mov      eax, dword ptr [0x4a4044] ; *0x4A4044->0x4F9194
0x4926C2: mov      byte ptr [eax], 0
0x4926C5: mov      eax, dword ptr [ebp - 8]
0x4926C8: call     0x44d8b4
0x4926CD: mov      dword ptr [ebp - 0x50], eax
0x4926D0: push     1
0x4926D2: mov      eax, dword ptr [ebp - 0x50]
0x4926D5: call     0x41834c
0x4926DA: push     eax
0x4926DB: call     0x406434
0x4926E0: mov      eax, dword ptr [ebp - 0x50]
0x4926E3: mov      eax, dword ptr [eax + 0xc]
0x4926E6: mov      edx, dword ptr [ebp - 4]
0x4926E9: mov      edx, dword ptr [edx + 0x79c]
0x4926EF: call     0x4173fc
0x4926F4: lea      eax, [ebp - 0x78]
0x4926F7: mov      edx, dword ptr [ebp - 4]
0x4926FA: add      edx, 0x78c
0x492700: call     0x403c78
0x492705: mov      eax, dword ptr [ebp - 0x78]
0x492708: push     eax
0x492709: mov      eax, dword ptr [ebp - 0x48]
0x49270C: mov      edx, dword ptr [eax + 0x34]
0x49270F: add      edx, 0x17
0x492712: mov      eax, dword ptr [ebp - 0x48]
0x492715: call     0x456fdc
0x49271A: push     eax
0x49271B: lea      eax, [ebp - 0x7c]
0x49271E: mov      edx, dword ptr [ebp - 4]
0x492721: add      edx, 0x78c
0x492727: call     0x403c78
0x49272C: mov      edx, dword ptr [ebp - 0x7c]
0x49272F: mov      eax, dword ptr [ebp - 0x50]
0x492732: call     0x4181ac
0x492737: sar      eax, 1
0x492739: jns      0x49273e
0x49273B: adc      eax, 0
0x49273E: mov      edx, dword ptr [ebp - 0x48]
0x492741: mov      edx, dword ptr [edx + 0x30]
0x492744: add      edx, 0x7a
0x492747: sub      edx, eax
0x492749: mov      eax, dword ptr [ebp - 0x48]
0x49274C: call     0x456f94
0x492751: mov      edx, eax
0x492753: mov      eax, dword ptr [ebp - 0x50]
0x492756: pop      ecx
0x492757: call     0x4180dc
0x49275C: mov      eax, dword ptr [ebp - 0x50]
0x49275F: mov      eax, dword ptr [eax + 0xc]
0x492762: mov      edx, 0xc0c0c0
0x492767: call     0x4173fc
0x49276C: mov      eax, dword ptr [ebp - 4]
0x49276F: lea      edx, [eax + 0x7a0]
0x492775: lea      eax, [ebp - 0x74]
0x492778: call     0x402a14
0x49277D: mov      edx, 0x49283c ; " "
0x492782: lea      eax, [ebp - 0x74]
0x492785: mov      cl, 0xf
0x492787: call     0x4029e4
0x49278C: lea      edx, [ebp - 0x74]
0x49278F: lea      eax, [ebp - 0x9c]
0x492795: call     0x402a14
0x49279A: mov      eax, dword ptr [ebp - 4]
0x49279D: lea      edx, [eax + 0x7af]
0x4927A3: lea      eax, [ebp - 0x9c]
0x4927A9: mov      cl, 0x1d
0x4927AB: call     0x4029e4
0x4927B0: lea      edx, [ebp - 0x9c]
0x4927B6: lea      eax, [ebp - 0x78]
0x4927B9: call     0x403c78
0x4927BE: mov      eax, dword ptr [ebp - 0x78]
0x4927C1: push     eax
0x4927C2: mov      eax, dword ptr [ebp - 0x48]
0x4927C5: mov      edx, dword ptr [eax + 0x34]
0x4927C8: add      edx, 0x3a
0x4927CB: mov      eax, dword ptr [ebp - 0x48]
0x4927CE: call     0x456fdc
0x4927D3: push     eax
0x4927D4: mov      eax, dword ptr [ebp - 0x48]
0x4927D7: mov      edx, dword ptr [eax + 0x30]
0x4927DA: add      edx, 0x2d
0x4927DD: mov      eax, dword ptr [ebp - 0x48]
0x4927E0: call     0x456f94
0x4927E5: mov      edx, eax
0x4927E7: mov      eax, dword ptr [ebp - 0x50]
0x4927EA: pop      ecx
0x4927EB: call     0x4180dc
0x4927F0: mov      eax, dword ptr [ebp - 0x50]
0x4927F3: call     0x44d104
0x4927F8: xor      eax, eax
0x4927FA: pop      edx
0x4927FB: pop      ecx
0x4927FC: pop      ecx
0x4927FD: mov      dword ptr fs:[eax], edx
0x492800: push     0x492827
0x492805: lea      eax, [ebp - 0x7c]
0x492808: mov      edx, 2
0x49280D: call     0x403a7c
0x492812: lea      eax, [ebp - 0x40]
0x492815: mov      edx, 4
0x49281A: call     0x403a7c
0x49281F: ret      
0x492820: jmp      0x403518
0x492825: jmp      0x492805
0x492827: pop      edi
0x492828: pop      esi
0x492829: pop      ebx
0x49282A: mov      esp, ebp
0x49282C: pop      ebp
0x49282D: ret      
0x49282E: add      byte ptr [eax], al
0x492838: [STR len=1] "A"
0x49283C: add      dword ptr [eax], esp
0x49283E: add      byte ptr [eax], al
0x492840: push     ebp
0x492841: mov      ebp, esp
0x492843: add      esp, -0x14
0x492846: push     ebx
0x492847: xor      ebx, ebx
0x492849: mov      dword ptr [ebp - 0x14], ebx
0x49284C: mov      dword ptr [ebp - 0x10], ebx
0x49284F: mov      byte ptr [ebp - 9], cl
0x492852: mov      dword ptr [ebp - 8], edx
0x492855: mov      dword ptr [ebp - 4], eax
0x492858: xor      eax, eax
0x49285A: push     ebp
0x49285B: push     0x4929b8
0x492860: push     dword ptr fs:[eax]
0x492863: mov      dword ptr fs:[eax], esp
0x492866: mov      edx, dword ptr [ebp + 0xc]
0x492869: mov      eax, dword ptr [ebp - 4]
0x49286C: mov      eax, dword ptr [eax + 0x490]
0x492872: call     0x457024
0x492877: mov      edx, dword ptr [ebp - 4]
0x49287A: mov      edx, dword ptr [edx + 0x490]
0x492880: sub      eax, dword ptr [edx + 0x30]
0x492883: mov      dword ptr [ebp + 0xc], eax
0x492886: mov      edx, dword ptr [ebp + 8]
0x492889: mov      eax, dword ptr [ebp - 4]
0x49288C: mov      eax, dword ptr [eax + 0x490]
0x492892: call     0x45706c
0x492897: mov      edx, dword ptr [ebp - 4]
0x49289A: mov      edx, dword ptr [edx + 0x490]
0x4928A0: sub      eax, dword ptr [edx + 0x34]
0x4928A3: mov      dword ptr [ebp + 8], eax
0x4928A6: cmp      dword ptr [ebp + 0xc], 0x2a
0x4928AA: jle      0x49299a
0x4928B0: cmp      dword ptr [ebp + 0xc], 0xc9
0x4928B7: jge      0x49299a
0x4928BD: cmp      dword ptr [ebp + 8], 0x36
0x4928C1: jle      0x49299a
0x4928C7: cmp      dword ptr [ebp + 8], 0x47
0x4928CB: jge      0x49299a
0x4928D1: mov      eax, dword ptr [ebp - 4]
0x4928D4: cmp      byte ptr [eax + 0x7a0], 0
0x4928DB: je       0x49299a
0x4928E1: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x4928E6: mov      eax, dword ptr [eax]
0x4928E8: mov      eax, dword ptr [eax + 0x5a850]
0x4928EE: mov      dl, 1
0x4928F0: call     0x4278c0
0x4928F5: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x4928FA: mov      eax, dword ptr [eax]
0x4928FC: mov      eax, dword ptr [eax + 0x5a850]
0x492902: mov      edx, dword ptr [eax]
0x492904: call     dword ptr [edx + 0xb4]
0x49290A: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x49290F: mov      eax, dword ptr [eax]
0x492911: mov      eax, dword ptr [eax + 0x5a850]
0x492917: call     0x42d734
0x49291C: mov      edx, dword ptr [0x4a3e30] ; *0x4A3E30->0x4A2CE0
0x492922: mov      dl, byte ptr [edx]
0x492924: call     0x431bb0
0x492929: lea      eax, [ebp - 0x10]
0x49292C: mov      edx, dword ptr [ebp - 4]
0x49292F: add      edx, 0x7a0
0x492935: call     0x403c78
0x49293A: mov      edx, dword ptr [ebp - 0x10]
0x49293D: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x492942: mov      eax, dword ptr [eax]
0x492944: mov      eax, dword ptr [eax + 0x5a850]
0x49294A: call     0x427a00
0x49294F: lea      edx, [ebp - 0x14]
0x492952: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x492957: mov      eax, dword ptr [eax]
0x492959: mov      eax, dword ptr [eax + 0x5a850]
0x49295F: call     0x4279d0
0x492964: mov      eax, dword ptr [ebp - 0x14]
0x492967: call     0x403cd4
0x49296C: mov      edx, eax
0x49296E: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x492973: mov      eax, dword ptr [eax]
0x492975: mov      eax, dword ptr [eax + 0x5a850]
0x49297B: mov      ecx, dword ptr [eax]
0x49297D: call     dword ptr [ecx + 0xcc]
0x492983: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x492988: mov      eax, dword ptr [eax]
0x49298A: mov      eax, dword ptr [eax + 0x5a850]
0x492990: xor      edx, edx
0x492992: mov      ecx, dword ptr [eax]
0x492994: call     dword ptr [ecx + 0xc8]
0x49299A: xor      eax, eax
0x49299C: pop      edx
0x49299D: pop      ecx
0x49299E: pop      ecx
0x49299F: mov      dword ptr fs:[eax], edx
0x4929A2: push     0x4929bf
0x4929A7: lea      eax, [ebp - 0x14]
0x4929AA: call     0x403a58
0x4929AF: lea      eax, [ebp - 0x10]
0x4929B2: call     0x403a58
0x4929B7: ret      
0x4929B8: jmp      0x403518
0x4929BD: jmp      0x4929a7
0x4929BF: pop      ebx
0x4929C0: mov      esp, ebp
0x4929C2: pop      ebp
0x4929C3: ret      0xc
0x4929C6: mov      eax, eax
0x4929C8: push     ebp
0x4929C9: mov      ebp, esp
0x4929CB: add      esp, -0xc
0x4929CE: mov      byte ptr [ebp - 9], cl
0x4929D1: mov      dword ptr [ebp - 8], edx
0x4929D4: mov      dword ptr [ebp - 4], eax
0x4929D7: mov      eax, dword ptr [0x4a4034] ; *0x4A4034->0x4F91FC
0x4929DC: mov      byte ptr [eax], 0
0x4929DF: mov      esp, ebp
0x4929E1: pop      ebp
0x4929E2: ret      8
0x4929E5: lea      eax, [eax]
0x4929E8: push     ebp
0x4929E9: mov      ebp, esp
0x4929EB: add      esp, -0x10
0x4929EE: push     esi
0x4929EF: push     edi
0x4929F0: mov      byte ptr [ebp - 0xd], cl
0x4929F3: mov      dword ptr [ebp - 8], edx
0x4929F6: mov      dword ptr [ebp - 4], eax
0x4929F9: mov      dword ptr [ebp - 0xc], 0xffffffff
0x492A00: mov      eax, dword ptr [ebp - 8]
0x492A03: mov      edx, dword ptr [ebp - 4]
0x492A06: cmp      eax, dword ptr [edx + 0x4a4]
0x492A0C: jne      0x492a13
0x492A0E: xor      eax, eax
0x492A10: mov      dword ptr [ebp - 0xc], eax
0x492A13: mov      eax, dword ptr [ebp - 8]
0x492A16: mov      edx, dword ptr [ebp - 4]
0x492A19: cmp      eax, dword ptr [edx + 0x498]
0x492A1F: jne      0x492a28
0x492A21: mov      dword ptr [ebp - 0xc], 1
0x492A28: mov      eax, dword ptr [ebp - 8]
0x492A2B: mov      edx, dword ptr [ebp - 4]
0x492A2E: cmp      eax, dword ptr [edx + 0x49c]
0x492A34: jne      0x492a3d
0x492A36: mov      dword ptr [ebp - 0xc], 4
0x492A3D: mov      eax, dword ptr [ebp - 8]
0x492A40: mov      edx, dword ptr [ebp - 4]
0x492A43: cmp      eax, dword ptr [edx + 0x4a0]
0x492A49: jne      0x492a52
0x492A4B: mov      dword ptr [ebp - 0xc], 3
0x492A52: mov      eax, dword ptr [ebp - 8]
0x492A55: mov      edx, dword ptr [ebp - 4]
0x492A58: cmp      eax, dword ptr [edx + 0x4a8]
0x492A5E: jne      0x492a67
0x492A60: mov      dword ptr [ebp - 0xc], 2
0x492A67: mov      eax, dword ptr [ebp - 8]
0x492A6A: mov      edx, dword ptr [ebp - 4]
0x492A6D: cmp      eax, dword ptr [edx + 0x4b8]
0x492A73: jne      0x492a7c
0x492A75: mov      dword ptr [ebp - 0xc], 7
0x492A7C: mov      eax, dword ptr [ebp - 8]
0x492A7F: mov      edx, dword ptr [ebp - 4]
0x492A82: cmp      eax, dword ptr [edx + 0x4b0]
0x492A88: jne      0x492a91
0x492A8A: mov      dword ptr [ebp - 0xc], 8
0x492A91: mov      eax, dword ptr [ebp - 8]
0x492A94: mov      edx, dword ptr [ebp - 4]
0x492A97: cmp      eax, dword ptr [edx + 0x4b4]
0x492A9D: jne      0x492aa6
0x492A9F: mov      dword ptr [ebp - 0xc], 5
0x492AA6: mov      eax, dword ptr [ebp - 8]
0x492AA9: mov      edx, dword ptr [ebp - 4]
0x492AAC: cmp      eax, dword ptr [edx + 0x4ac]
0x492AB2: jne      0x492abb
0x492AB4: mov      dword ptr [ebp - 0xc], 6
0x492ABB: cmp      dword ptr [ebp - 0xc], 0
0x492ABF: jl       0x492ade
0x492AC1: imul     eax, dword ptr [ebp - 0xc], 0xd
0x492AC5: mov      edx, dword ptr [ebp - 4]
0x492AC8: mov      ecx, dword ptr [0x4a4034] ; *0x4A4034->0x4F91FC
0x492ACE: lea      esi, [edx + eax*4 + 0x7c0]
0x492AD5: mov      edi, ecx
0x492AD7: mov      ecx, 0xd
0x492ADC: rep movsd dword ptr es:[edi], dword ptr [esi]
0x492ADE: pop      edi
0x492ADF: pop      esi
0x492AE0: mov      esp, ebp
0x492AE2: pop      ebp
0x492AE3: ret      8
0x492AE6: mov      eax, eax
0x492AE8: push     ebp
0x492AE9: mov      ebp, esp
0x492AEB: add      esp, -0xc
0x492AEE: mov      dword ptr [ebp - 0xc], ecx
0x492AF1: mov      dword ptr [ebp - 8], edx
0x492AF4: mov      dword ptr [ebp - 4], eax
0x492AF7: xor      edx, edx
0x492AF9: mov      eax, dword ptr [ebp - 4]
0x492AFC: mov      eax, dword ptr [eax + 0x490]
0x492B02: call     0x458214
0x492B07: mov      esp, ebp
0x492B09: pop      ebp
0x492B0A: ret      4
0x492B0D: lea      eax, [eax]
0x492B10: push     ebp
0x492B11: mov      ebp, esp
0x492B13: add      esp, -0x24
0x492B16: mov      dword ptr [ebp - 0xc], ecx
0x492B19: mov      dword ptr [ebp - 8], edx
0x492B1C: mov      dword ptr [ebp - 4], eax
0x492B1F: mov      eax, dword ptr [ebp - 8]
0x492B22: mov      edx, dword ptr [ebp - 4]
0x492B25: cmp      eax, dword ptr [edx + 0x4a0]
0x492B2B: jne      0x492c09
0x492B31: mov      eax, dword ptr [ebp - 4]
0x492B34: cmp      byte ptr [eax + 0x85c], 0
0x492B3B: je       0x492c09
0x492B41: mov      eax, dword ptr [ebp - 4]
0x492B44: movzx    eax, word ptr [eax + 0x872]
0x492B4B: mov      dword ptr [ebp - 0x10], eax
0x492B4E: cmp      dword ptr [ebp - 0x10], 0
0x492B52: jl       0x492c09
0x492B58: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x492B5D: mov      eax, dword ptr [eax]
0x492B5F: mov      eax, dword ptr [eax + 0x32c]
0x492B65: mov      edx, dword ptr [ebp - 0x10]
0x492B68: call     0x45351c
0x492B6D: mov      dword ptr [ebp - 0x14], eax
0x492B70: cmp      dword ptr [ebp - 0x14], 0
0x492B74: je       0x492c09
0x492B7A: lea      edx, [ebp - 0x24]
0x492B7D: mov      eax, dword ptr [ebp - 0x14]
0x492B80: call     0x44d8ec
0x492B85: lea      eax, [ebp - 0x24]
0x492B88: push     eax
0x492B89: mov      eax, dword ptr [ebp - 0x14]
0x492B8C: push     eax
0x492B8D: push     1
0x492B8F: mov      eax, dword ptr [ebp - 0x14]
0x492B92: call     0x44d944
0x492B97: mov      edx, dword ptr [ebp - 4]
0x492B9A: mov      edx, dword ptr [edx + 0x4a0]
0x492BA0: mov      edx, dword ptr [edx + 0x3c]
0x492BA3: sub      edx, eax
0x492BA5: sar      edx, 1
0x492BA7: jns      0x492bac
0x492BA9: adc      edx, 0
0x492BAC: mov      eax, dword ptr [ebp - 4]
0x492BAF: mov      eax, dword ptr [eax + 0x4a0]
0x492BB5: add      edx, dword ptr [eax + 0x34]
0x492BB8: mov      eax, dword ptr [ebp - 4]
0x492BBB: mov      eax, dword ptr [eax + 0x4a0]
0x492BC1: call     0x456fdc
0x492BC6: push     eax
0x492BC7: mov      eax, dword ptr [ebp - 0x14]
0x492BCA: call     0x44dc60
0x492BCF: mov      edx, dword ptr [ebp - 4]
0x492BD2: mov      edx, dword ptr [edx + 0x4a0]
0x492BD8: mov      edx, dword ptr [edx + 0x38]
0x492BDB: sub      edx, eax
0x492BDD: sar      edx, 1
0x492BDF: jns      0x492be4
0x492BE1: adc      edx, 0
0x492BE4: mov      eax, dword ptr [ebp - 4]
0x492BE7: mov      eax, dword ptr [eax + 0x4a0]
0x492BED: add      edx, dword ptr [eax + 0x30]
0x492BF0: mov      eax, dword ptr [ebp - 4]
0x492BF3: mov      eax, dword ptr [eax + 0x4a0]
0x492BF9: call     0x456f94
0x492BFE: mov      edx, eax
0x492C00: mov      eax, dword ptr [ebp - 0xc]
0x492C03: pop      ecx
0x492C04: call     0x44d6cc
0x492C09: mov      eax, dword ptr [ebp - 8]
0x492C0C: mov      edx, dword ptr [ebp - 4]
0x492C0F: cmp      eax, dword ptr [edx + 0x4a8]
0x492C15: jne      0x492cf3
0x492C1B: mov      eax, dword ptr [ebp - 4]
0x492C1E: cmp      byte ptr [eax + 0x828], 0
0x492C25: je       0x492cf3
0x492C2B: mov      eax, dword ptr [ebp - 4]
0x492C2E: movzx    eax, word ptr [eax + 0x83e]
0x492C35: mov      dword ptr [ebp - 0x10], eax
0x492C38: cmp      dword ptr [ebp - 0x10], 0
0x492C3C: jl       0x492cf3
0x492C42: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x492C47: mov      eax, dword ptr [eax]
0x492C49: mov      eax, dword ptr [eax + 0x32c]
0x492C4F: mov      edx, dword ptr [ebp - 0x10]
0x492C52: call     0x45351c
0x492C57: mov      dword ptr [ebp - 0x14], eax
0x492C5A: cmp      dword ptr [ebp - 0x14], 0
0x492C5E: je       0x492cf3
0x492C64: lea      edx, [ebp - 0x24]
0x492C67: mov      eax, dword ptr [ebp - 0x14]
0x492C6A: call     0x44d8ec
0x492C6F: lea      eax, [ebp - 0x24]
0x492C72: push     eax
0x492C73: mov      eax, dword ptr [ebp - 0x14]
0x492C76: push     eax
0x492C77: push     1
0x492C79: mov      eax, dword ptr [ebp - 0x14]
0x492C7C: call     0x44d944
0x492C81: mov      edx, dword ptr [ebp - 4]
0x492C84: mov      edx, dword ptr [edx + 0x4a8]
0x492C8A: mov      edx, dword ptr [edx + 0x3c]
0x492C8D: sub      edx, eax
0x492C8F: sar      edx, 1
0x492C91: jns      0x492c96
0x492C93: adc      edx, 0
0x492C96: mov      eax, dword ptr [ebp - 4]
0x492C99: mov      eax, dword ptr [eax + 0x4a8]
0x492C9F: add      edx, dword ptr [eax + 0x34]
0x492CA2: mov      eax, dword ptr [ebp - 4]
0x492CA5: mov      eax, dword ptr [eax + 0x4a8]
0x492CAB: call     0x456fdc
0x492CB0: push     eax
0x492CB1: mov      eax, dword ptr [ebp - 0x14]
0x492CB4: call     0x44dc60
0x492CB9: mov      edx, dword ptr [ebp - 4]
0x492CBC: mov      edx, dword ptr [edx + 0x4a8]
0x492CC2: mov      edx, dword ptr [edx + 0x38]
0x492CC5: sub      edx, eax
0x492CC7: sar      edx, 1
0x492CC9: jns      0x492cce
0x492CCB: adc      edx, 0
0x492CCE: mov      eax, dword ptr [ebp - 4]
0x492CD1: mov      eax, dword ptr [eax + 0x4a8]
0x492CD7: add      edx, dword ptr [eax + 0x30]
0x492CDA: mov      eax, dword ptr [ebp - 4]
0x492CDD: mov      eax, dword ptr [eax + 0x4a8]
0x492CE3: call     0x456f94
0x492CE8: mov      edx, eax
0x492CEA: mov      eax, dword ptr [ebp - 0xc]
0x492CED: pop      ecx
0x492CEE: call     0x44d6cc
0x492CF3: mov      eax, dword ptr [ebp - 8]
0x492CF6: mov      edx, dword ptr [ebp - 4]
0x492CF9: cmp      eax, dword ptr [edx + 0x4ac]
0x492CFF: jne      0x492ddd
0x492D05: mov      eax, dword ptr [ebp - 4]
0x492D08: cmp      byte ptr [eax + 0x8f8], 0
0x492D0F: je       0x492ddd
0x492D15: mov      eax, dword ptr [ebp - 4]
0x492D18: movzx    eax, word ptr [eax + 0x90e]
0x492D1F: mov      dword ptr [ebp - 0x10], eax
0x492D22: cmp      dword ptr [ebp - 0x10], 0
0x492D26: jl       0x492ddd
0x492D2C: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x492D31: mov      eax, dword ptr [eax]
0x492D33: mov      eax, dword ptr [eax + 0x32c]
0x492D39: mov      edx, dword ptr [ebp - 0x10]
0x492D3C: call     0x45351c
0x492D41: mov      dword ptr [ebp - 0x14], eax
0x492D44: cmp      dword ptr [ebp - 0x14], 0
0x492D48: je       0x492ddd
0x492D4E: lea      edx, [ebp - 0x24]
0x492D51: mov      eax, dword ptr [ebp - 0x14]
0x492D54: call     0x44d8ec
0x492D59: lea      eax, [ebp - 0x24]
0x492D5C: push     eax
0x492D5D: mov      eax, dword ptr [ebp - 0x14]
0x492D60: push     eax
0x492D61: push     1
0x492D63: mov      eax, dword ptr [ebp - 0x14]
0x492D66: call     0x44d944
0x492D6B: mov      edx, dword ptr [ebp - 4]
0x492D6E: mov      edx, dword ptr [edx + 0x4ac]
0x492D74: mov      edx, dword ptr [edx + 0x3c]
0x492D77: sub      edx, eax
0x492D79: sar      edx, 1
0x492D7B: jns      0x492d80
0x492D7D: adc      edx, 0
0x492D80: mov      eax, dword ptr [ebp - 4]
0x492D83: mov      eax, dword ptr [eax + 0x4ac]
0x492D89: add      edx, dword ptr [eax + 0x34]
0x492D8C: mov      eax, dword ptr [ebp - 4]
0x492D8F: mov      eax, dword ptr [eax + 0x4ac]
0x492D95: call     0x456fdc
0x492D9A: push     eax
0x492D9B: mov      eax, dword ptr [ebp - 0x14]
0x492D9E: call     0x44dc60
0x492DA3: mov      edx, dword ptr [ebp - 4]
0x492DA6: mov      edx, dword ptr [edx + 0x4ac]
0x492DAC: mov      edx, dword ptr [edx + 0x38]
0x492DAF: sub      edx, eax
0x492DB1: sar      edx, 1
0x492DB3: jns      0x492db8
0x492DB5: adc      edx, 0
0x492DB8: mov      eax, dword ptr [ebp - 4]
0x492DBB: mov      eax, dword ptr [eax + 0x4ac]
0x492DC1: add      edx, dword ptr [eax + 0x30]
0x492DC4: mov      eax, dword ptr [ebp - 4]
0x492DC7: mov      eax, dword ptr [eax + 0x4ac]
0x492DCD: call     0x456f94
0x492DD2: mov      edx, eax
0x492DD4: mov      eax, dword ptr [ebp - 0xc]
0x492DD7: pop      ecx
0x492DD8: call     0x44d6cc
0x492DDD: mov      eax, dword ptr [ebp - 8]
0x492DE0: mov      edx, dword ptr [ebp - 4]
0x492DE3: cmp      eax, dword ptr [edx + 0x4b4]
0x492DE9: jne      0x492ec7
0x492DEF: mov      eax, dword ptr [ebp - 4]
0x492DF2: cmp      byte ptr [eax + 0x8c4], 0
0x492DF9: je       0x492ec7
0x492DFF: mov      eax, dword ptr [ebp - 4]
0x492E02: movzx    eax, word ptr [eax + 0x8da]
0x492E09: mov      dword ptr [ebp - 0x10], eax
0x492E0C: cmp      dword ptr [ebp - 0x10], 0
0x492E10: jl       0x492ec7
0x492E16: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x492E1B: mov      eax, dword ptr [eax]
0x492E1D: mov      eax, dword ptr [eax + 0x32c]
0x492E23: mov      edx, dword ptr [ebp - 0x10]
0x492E26: call     0x45351c
0x492E2B: mov      dword ptr [ebp - 0x14], eax
0x492E2E: cmp      dword ptr [ebp - 0x14], 0
0x492E32: je       0x492ec7
0x492E38: lea      edx, [ebp - 0x24]
0x492E3B: mov      eax, dword ptr [ebp - 0x14]
0x492E3E: call     0x44d8ec
0x492E43: lea      eax, [ebp - 0x24]
0x492E46: push     eax
0x492E47: mov      eax, dword ptr [ebp - 0x14]
0x492E4A: push     eax
0x492E4B: push     1
0x492E4D: mov      eax, dword ptr [ebp - 0x14]
0x492E50: call     0x44d944
0x492E55: mov      edx, dword ptr [ebp - 4]
0x492E58: mov      edx, dword ptr [edx + 0x4b4]
0x492E5E: mov      edx, dword ptr [edx + 0x3c]
0x492E61: sub      edx, eax
0x492E63: sar      edx, 1
0x492E65: jns      0x492e6a
0x492E67: adc      edx, 0
0x492E6A: mov      eax, dword ptr [ebp - 4]
0x492E6D: mov      eax, dword ptr [eax + 0x4b4]
0x492E73: add      edx, dword ptr [eax + 0x34]
0x492E76: mov      eax, dword ptr [ebp - 4]
0x492E79: mov      eax, dword ptr [eax + 0x4b4]
0x492E7F: call     0x456fdc
0x492E84: push     eax
0x492E85: mov      eax, dword ptr [ebp - 0x14]
0x492E88: call     0x44dc60
0x492E8D: mov      edx, dword ptr [ebp - 4]
0x492E90: mov      edx, dword ptr [edx + 0x4b4]
0x492E96: mov      edx, dword ptr [edx + 0x38]
0x492E99: sub      edx, eax
0x492E9B: sar      edx, 1
0x492E9D: jns      0x492ea2
0x492E9F: adc      edx, 0
0x492EA2: mov      eax, dword ptr [ebp - 4]
0x492EA5: mov      eax, dword ptr [eax + 0x4b4]
0x492EAB: add      edx, dword ptr [eax + 0x30]
0x492EAE: mov      eax, dword ptr [ebp - 4]
0x492EB1: mov      eax, dword ptr [eax + 0x4b4]
0x492EB7: call     0x456f94
0x492EBC: mov      edx, eax
0x492EBE: mov      eax, dword ptr [ebp - 0xc]
0x492EC1: pop      ecx
0x492EC2: call     0x44d6cc
0x492EC7: mov      eax, dword ptr [ebp - 8]
0x492ECA: mov      edx, dword ptr [ebp - 4]
0x492ECD: cmp      eax, dword ptr [edx + 0x4b0]
0x492ED3: jne      0x492fb1
0x492ED9: mov      eax, dword ptr [ebp - 4]
0x492EDC: cmp      byte ptr [eax + 0x960], 0
0x492EE3: je       0x492fb1
0x492EE9: mov      eax, dword ptr [ebp - 4]
0x492EEC: movzx    eax, word ptr [eax + 0x976]
0x492EF3: mov      dword ptr [ebp - 0x10], eax
0x492EF6: cmp      dword ptr [ebp - 0x10], 0
0x492EFA: jl       0x492fb1
0x492F00: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x492F05: mov      eax, dword ptr [eax]
0x492F07: mov      eax, dword ptr [eax + 0x32c]
0x492F0D: mov      edx, dword ptr [ebp - 0x10]
0x492F10: call     0x45351c
0x492F15: mov      dword ptr [ebp - 0x14], eax
0x492F18: cmp      dword ptr [ebp - 0x14], 0
0x492F1C: je       0x492fb1
0x492F22: lea      edx, [ebp - 0x24]
0x492F25: mov      eax, dword ptr [ebp - 0x14]
0x492F28: call     0x44d8ec
0x492F2D: lea      eax, [ebp - 0x24]
0x492F30: push     eax
0x492F31: mov      eax, dword ptr [ebp - 0x14]
0x492F34: push     eax
0x492F35: push     1
0x492F37: mov      eax, dword ptr [ebp - 0x14]
0x492F3A: call     0x44d944
0x492F3F: mov      edx, dword ptr [ebp - 4]
0x492F42: mov      edx, dword ptr [edx + 0x4b0]
0x492F48: mov      edx, dword ptr [edx + 0x3c]
0x492F4B: sub      edx, eax
0x492F4D: sar      edx, 1
0x492F4F: jns      0x492f54
0x492F51: adc      edx, 0
0x492F54: mov      eax, dword ptr [ebp - 4]
0x492F57: mov      eax, dword ptr [eax + 0x4b0]
0x492F5D: add      edx, dword ptr [eax + 0x34]
0x492F60: mov      eax, dword ptr [ebp - 4]
0x492F63: mov      eax, dword ptr [eax + 0x4b0]
0x492F69: call     0x456fdc
0x492F6E: push     eax
0x492F6F: mov      eax, dword ptr [ebp - 0x14]
0x492F72: call     0x44dc60
0x492F77: mov      edx, dword ptr [ebp - 4]
0x492F7A: mov      edx, dword ptr [edx + 0x4b0]
0x492F80: mov      edx, dword ptr [edx + 0x38]
0x492F83: sub      edx, eax
0x492F85: sar      edx, 1
0x492F87: jns      0x492f8c
0x492F89: adc      edx, 0
0x492F8C: mov      eax, dword ptr [ebp - 4]
0x492F8F: mov      eax, dword ptr [eax + 0x4b0]
0x492F95: add      edx, dword ptr [eax + 0x30]
0x492F98: mov      eax, dword ptr [ebp - 4]
0x492F9B: mov      eax, dword ptr [eax + 0x4b0]
0x492FA1: call     0x456f94
0x492FA6: mov      edx, eax
0x492FA8: mov      eax, dword ptr [ebp - 0xc]
0x492FAB: pop      ecx
0x492FAC: call     0x44d6cc
0x492FB1: mov      eax, dword ptr [ebp - 8]
0x492FB4: mov      edx, dword ptr [ebp - 4]
0x492FB7: cmp      eax, dword ptr [edx + 0x4b8]
0x492FBD: jne      0x49309b
0x492FC3: mov      eax, dword ptr [ebp - 4]
0x492FC6: cmp      byte ptr [eax + 0x92c], 0
0x492FCD: je       0x49309b
0x492FD3: mov      eax, dword ptr [ebp - 4]
0x492FD6: movzx    eax, word ptr [eax + 0x942]
0x492FDD: mov      dword ptr [ebp - 0x10], eax
0x492FE0: cmp      dword ptr [ebp - 0x10], 0
0x492FE4: jl       0x49309b
0x492FEA: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x492FEF: mov      eax, dword ptr [eax]
0x492FF1: mov      eax, dword ptr [eax + 0x32c]
0x492FF7: mov      edx, dword ptr [ebp - 0x10]
0x492FFA: call     0x45351c
0x492FFF: mov      dword ptr [ebp - 0x14], eax
0x493002: cmp      dword ptr [ebp - 0x14], 0
0x493006: je       0x49309b
0x49300C: lea      edx, [ebp - 0x24]
0x49300F: mov      eax, dword ptr [ebp - 0x14]
0x493012: call     0x44d8ec
0x493017: lea      eax, [ebp - 0x24]
0x49301A: push     eax
0x49301B: mov      eax, dword ptr [ebp - 0x14]
0x49301E: push     eax
0x49301F: push     1
0x493021: mov      eax, dword ptr [ebp - 0x14]
0x493024: call     0x44d944
0x493029: mov      edx, dword ptr [ebp - 4]
0x49302C: mov      edx, dword ptr [edx + 0x4b8]
0x493032: mov      edx, dword ptr [edx + 0x3c]
0x493035: sub      edx, eax
0x493037: sar      edx, 1
0x493039: jns      0x49303e
0x49303B: adc      edx, 0
0x49303E: mov      eax, dword ptr [ebp - 4]
0x493041: mov      eax, dword ptr [eax + 0x4b8]
0x493047: add      edx, dword ptr [eax + 0x34]
0x49304A: mov      eax, dword ptr [ebp - 4]
0x49304D: mov      eax, dword ptr [eax + 0x4b8]
0x493053: call     0x456fdc
0x493058: push     eax
0x493059: mov      eax, dword ptr [ebp - 0x14]
0x49305C: call     0x44dc60
0x493061: mov      edx, dword ptr [ebp - 4]
0x493064: mov      edx, dword ptr [edx + 0x4b8]
0x49306A: mov      edx, dword ptr [edx + 0x38]
0x49306D: sub      edx, eax
0x49306F: sar      edx, 1
0x493071: jns      0x493076
0x493073: adc      edx, 0
0x493076: mov      eax, dword ptr [ebp - 4]
0x493079: mov      eax, dword ptr [eax + 0x4b8]
0x49307F: add      edx, dword ptr [eax + 0x30]
0x493082: mov      eax, dword ptr [ebp - 4]
0x493085: mov      eax, dword ptr [eax + 0x4b8]
0x49308B: call     0x456f94
0x493090: mov      edx, eax
0x493092: mov      eax, dword ptr [ebp - 0xc]
0x493095: pop      ecx
0x493096: call     0x44d6cc
0x49309B: mov      esp, ebp
0x49309D: pop      ebp
0x49309E: ret      
0x49309F: nop      
0x4930A0: push     ebp
0x4930A1: mov      ebp, esp
0x4930A3: push     ecx
0x4930A4: mov      dword ptr [ebp - 4], eax
0x4930A7: mov      dl, 1
0x4930A9: mov      eax, dword ptr [ebp - 4]
0x4930AC: mov      eax, dword ptr [eax + 0x4c4]
0x4930B2: call     0x458214
0x4930B7: mov      edx, 0xfffffffd
0x4930BC: mov      eax, dword ptr [ebp - 4]
0x4930BF: mov      eax, dword ptr [eax + 0x4c4]
0x4930C5: call     0x4271b0
0x4930CA: xor      edx, edx
0x4930CC: mov      eax, dword ptr [ebp - 4]
0x4930CF: mov      eax, dword ptr [eax + 0x4c4]
0x4930D5: call     0x42718c
0x4930DA: mov      eax, dword ptr [ebp - 4]
0x4930DD: mov      eax, dword ptr [eax + 0x4c4]
0x4930E3: cmp      byte ptr [eax + 0x24c], 0
0x4930EA: je       0x493205
0x4930F0: mov      eax, dword ptr [ebp - 4]
0x4930F3: cmp      byte ptr [eax + 0x99c], 0
0x4930FA: je       0x493185
0x493100: mov      eax, dword ptr [ebp - 4]
0x493103: mov      eax, dword ptr [eax + 0x4d4]
0x493109: mov      byte ptr [eax + 0x24c], 1
0x493110: mov      eax, dword ptr [ebp - 4]
0x493113: mov      eax, dword ptr [eax + 0x4d8]
0x493119: mov      byte ptr [eax + 0x24c], 1
0x493120: mov      eax, dword ptr [ebp - 4]
0x493123: mov      eax, dword ptr [eax + 0x4dc]
0x493129: mov      byte ptr [eax + 0x24c], 1
0x493130: mov      eax, dword ptr [ebp - 4]
0x493133: mov      eax, dword ptr [eax + 0x4e0]
0x493139: mov      byte ptr [eax + 0x24c], 1
0x493140: mov      eax, dword ptr [ebp - 4]
0x493143: mov      eax, dword ptr [eax + 0x4e4]
0x493149: mov      byte ptr [eax + 0x24c], 1
0x493150: mov      eax, dword ptr [ebp - 4]
0x493153: mov      eax, dword ptr [eax + 0x4e8]
0x493159: mov      byte ptr [eax + 0x24c], 1
0x493160: mov      eax, dword ptr [ebp - 4]
0x493163: mov      eax, dword ptr [eax + 0x4ec]
0x493169: mov      byte ptr [eax + 0x24c], 1
0x493170: mov      eax, dword ptr [ebp - 4]
0x493173: mov      eax, dword ptr [eax + 0x4f0]
0x493179: mov      byte ptr [eax + 0x24c], 1
0x493180: jmp      0x493205
0x493185: mov      eax, dword ptr [ebp - 4]
0x493188: mov      eax, dword ptr [eax + 0x4d4]
0x49318E: mov      byte ptr [eax + 0x24c], 0
0x493195: mov      eax, dword ptr [ebp - 4]
0x493198: mov      eax, dword ptr [eax + 0x4d8]
0x49319E: mov      byte ptr [eax + 0x24c], 0
0x4931A5: mov      eax, dword ptr [ebp - 4]
0x4931A8: mov      eax, dword ptr [eax + 0x4dc]
0x4931AE: mov      byte ptr [eax + 0x24c], 0
0x4931B5: mov      eax, dword ptr [ebp - 4]
0x4931B8: mov      eax, dword ptr [eax + 0x4e0]
0x4931BE: mov      byte ptr [eax + 0x24c], 0
0x4931C5: mov      eax, dword ptr [ebp - 4]
0x4931C8: mov      eax, dword ptr [eax + 0x4e4]
0x4931CE: mov      byte ptr [eax + 0x24c], 0
0x4931D5: mov      eax, dword ptr [ebp - 4]
0x4931D8: mov      eax, dword ptr [eax + 0x4e8]
0x4931DE: mov      byte ptr [eax + 0x24c], 0
0x4931E5: mov      eax, dword ptr [ebp - 4]
0x4931E8: mov      eax, dword ptr [eax + 0x4ec]
0x4931EE: mov      byte ptr [eax + 0x24c], 0
0x4931F5: mov      eax, dword ptr [ebp - 4]
0x4931F8: mov      eax, dword ptr [eax + 0x4f0]
0x4931FE: mov      byte ptr [eax + 0x24c], 0
0x493205: mov      eax, dword ptr [ebp - 4]
0x493208: xor      edx, edx
0x49320A: mov      dword ptr [eax + 0x9b0], edx
0x493210: pop      ecx
0x493211: pop      ebp
0x493212: ret      
0x493213: nop      
0x493214: push     ebp
0x493215: mov      ebp, esp
0x493217: add      esp, -0x1c
0x49321A: push     ebx
0x49321B: xor      edx, edx
0x49321D: mov      dword ptr [ebp - 0x1c], edx
0x493220: mov      dword ptr [ebp - 0x10], edx
0x493223: mov      dword ptr [ebp - 4], eax
0x493226: xor      eax, eax
0x493228: push     ebp
0x493229: push     0x4934dd
0x49322E: push     dword ptr fs:[eax]
0x493231: mov      dword ptr fs:[eax], esp
0x493234: mov      eax, dword ptr [ebp - 4]
0x493237: mov      eax, dword ptr [eax + 0x500]
0x49323D: mov      dword ptr [ebp - 0x14], eax
0x493240: mov      eax, dword ptr [ebp - 0x14]
0x493243: mov      edx, dword ptr [eax + 0x258]
0x493249: mov      eax, dword ptr [ebp - 0x14]
0x49324C: mov      eax, dword ptr [eax + 0x254]
0x493252: call     0x45351c
0x493257: mov      dword ptr [ebp - 8], eax
0x49325A: cmp      dword ptr [ebp - 8], 0
0x49325E: je       0x49329c
0x493260: mov      eax, dword ptr [ebp - 8]
0x493263: call     0x44dc60
0x493268: mov      edx, 0x320
0x49326D: sub      edx, eax
0x49326F: sar      edx, 1
0x493271: jns      0x493276
0x493273: adc      edx, 0
0x493276: mov      eax, dword ptr [ebp - 0x14]
0x493279: call     0x42718c
0x49327E: mov      eax, dword ptr [ebp - 8]
0x493281: call     0x44d944
0x493286: mov      edx, 0x258
0x49328B: sub      edx, eax
0x49328D: sar      edx, 1
0x49328F: jns      0x493294
0x493291: adc      edx, 0
0x493294: mov      eax, dword ptr [ebp - 0x14]
0x493297: call     0x4271b0
0x49329C: mov      eax, dword ptr [ebp - 4]
0x49329F: call     0x481cec
0x4932A4: mov      eax, dword ptr [ebp - 4]
0x4932A7: mov      eax, dword ptr [eax + 0x500]
0x4932AD: call     0x45843c
0x4932B2: mov      eax, dword ptr [ebp - 0x14]
0x4932B5: mov      edx, dword ptr [eax + 0x30]
0x4932B8: add      edx, 0x10
0x4932BB: mov      eax, dword ptr [ebp - 0x14]
0x4932BE: call     0x456f94
0x4932C3: mov      edx, eax
0x4932C5: mov      eax, dword ptr [ebp - 4]
0x4932C8: mov      eax, dword ptr [eax + 0x5ec]
0x4932CE: call     0x42718c
0x4932D3: mov      eax, dword ptr [ebp - 0x14]
0x4932D6: mov      edx, dword ptr [eax + 0x34]
0x4932D9: add      edx, 0x24
0x4932DC: mov      eax, dword ptr [ebp - 0x14]
0x4932DF: call     0x456fdc
0x4932E4: mov      edx, eax
0x4932E6: mov      eax, dword ptr [ebp - 4]
0x4932E9: mov      eax, dword ptr [eax + 0x5ec]
0x4932EF: call     0x4271b0
0x4932F4: mov      edx, 0x23b
0x4932F9: mov      eax, dword ptr [ebp - 4]
0x4932FC: mov      eax, dword ptr [eax + 0x5ec]
0x493302: call     0x4271d8
0x493307: mov      edx, 0xf6
0x49330C: mov      eax, dword ptr [ebp - 4]
0x49330F: mov      eax, dword ptr [eax + 0x5ec]
0x493315: call     0x4271fc
0x49331A: mov      eax, dword ptr [ebp - 4]
0x49331D: mov      eax, dword ptr [eax + 0x5ec]
0x493323: mov      eax, dword ptr [eax + 0x204]
0x493329: mov      edx, dword ptr [ebp - 4]
0x49332C: mov      edx, dword ptr [edx + 0x9a8]
0x493332: mov      ecx, dword ptr [eax]
0x493334: call     dword ptr [ecx + 8]
0x493337: mov      dl, 1
0x493339: mov      eax, dword ptr [ebp - 4]
0x49333C: mov      eax, dword ptr [eax + 0x5ec]
0x493342: call     0x4278c0
0x493347: mov      eax, dword ptr [ebp - 4]
0x49334A: mov      eax, dword ptr [eax + 0x500]
0x493350: cmp      byte ptr [eax + 0x24c], 0
0x493357: je       0x493387
0x493359: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x49335E: mov      eax, dword ptr [eax]
0x493360: call     0x460c34
0x493365: mov      eax, dword ptr [0x4a4110] ; *0x4A4110->0x4A57A4
0x49336A: mov      eax, dword ptr [eax]
0x49336C: call     0x443574
0x493371: mov      eax, dword ptr [0x4a4110] ; *0x4A4110->0x4A57A4
0x493376: mov      eax, dword ptr [eax]
0x493378: cmp      byte ptr [eax + 0x84], 0
0x49337F: jne      0x4934bf
0x493385: jmp      0x493347
0x493387: xor      edx, edx
0x493389: mov      eax, dword ptr [ebp - 4]
0x49338C: mov      eax, dword ptr [eax + 0x500]
0x493392: call     0x458214
0x493397: mov      eax, dword ptr [ebp - 4]
0x49339A: call     0x481d88
0x49339F: mov      eax, dword ptr [ebp - 4]
0x4933A2: mov      eax, dword ptr [eax + 0x368]
0x4933A8: cmp      dword ptr [eax + 0x284], 1
0x4933AF: jne      0x4934bf
0x4933B5: lea      eax, [ebp - 0x10]
0x4933B8: call     0x403a58
0x4933BD: mov      eax, dword ptr [ebp - 4]
0x4933C0: mov      eax, dword ptr [eax + 0x5ec]
0x4933C6: mov      eax, dword ptr [eax + 0x204]
0x4933CC: mov      edx, dword ptr [eax]
0x4933CE: call     dword ptr [edx + 0x14]
0x4933D1: dec      eax
0x4933D2: test     eax, eax
0x4933D4: jl       0x493477
0x4933DA: inc      eax
0x4933DB: mov      dword ptr [ebp - 0x18], eax
0x4933DE: mov      dword ptr [ebp - 0xc], 0
0x4933E5: lea      ecx, [ebp - 0x1c]
0x4933E8: mov      eax, dword ptr [ebp - 4]
0x4933EB: mov      eax, dword ptr [eax + 0x5ec]
0x4933F1: mov      eax, dword ptr [eax + 0x204]
0x4933F7: mov      edx, dword ptr [ebp - 0xc]
0x4933FA: mov      ebx, dword ptr [eax]
0x4933FC: call     dword ptr [ebx + 0xc]
0x4933FF: cmp      dword ptr [ebp - 0x1c], 0
0x493403: jne      0x493439
0x493405: push     dword ptr [ebp - 0x10]
0x493408: lea      ecx, [ebp - 0x1c]
0x49340B: mov      eax, dword ptr [ebp - 4]
0x49340E: mov      eax, dword ptr [eax + 0x5ec]
0x493414: mov      eax, dword ptr [eax + 0x204]
0x49341A: mov      edx, dword ptr [ebp - 0xc]
0x49341D: mov      ebx, dword ptr [eax]
0x49341F: call     dword ptr [ebx + 0xc]
0x493422: push     dword ptr [ebp - 0x1c]
0x493425: push     0x4934f4 ; " \r"
0x49342A: lea      eax, [ebp - 0x10]
0x49342D: mov      edx, 3
0x493432: call     0x403d94
0x493437: jmp      0x49346b
0x493439: push     dword ptr [ebp - 0x10]
0x49343C: lea      ecx, [ebp - 0x1c]
0x49343F: mov      eax, dword ptr [ebp - 4]
0x493442: mov      eax, dword ptr [eax + 0x5ec]
0x493448: mov      eax, dword ptr [eax + 0x204]
0x49344E: mov      edx, dword ptr [ebp - 0xc]
0x493451: mov      ebx, dword ptr [eax]
0x493453: call     dword ptr [ebx + 0xc]
0x493456: push     dword ptr [ebp - 0x1c]
0x493459: push     0x493500 ; "\r"
0x49345E: lea      eax, [ebp - 0x10]
0x493461: mov      edx, 3
0x493466: call     0x403d94
0x49346B: inc      dword ptr [ebp - 0xc]
0x49346E: dec      dword ptr [ebp - 0x18]
0x493471: jne      0x4933e5
0x493477: mov      eax, dword ptr [ebp - 0x10]
0x49347A: call     0x403cd4
0x49347F: cmp      eax, 0xfa0
0x493484: jle      0x4934b0
0x493486: lea      eax, [ebp - 0x10]
0x493489: push     eax
0x49348A: mov      ecx, 0xfa0
0x49348F: mov      edx, 1
0x493494: mov      eax, dword ptr [ebp - 0x10]
0x493497: call     0x403ed8
0x49349C: mov      cx, word ptr [0x493504]
0x4934A3: mov      edx, 0x493510 ; "由于句子太长，最后的部分被移除了。"
0x4934A8: mov      eax, dword ptr [ebp - 4]
0x4934AB: call     0x486df0
0x4934B0: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4934B5: mov      eax, dword ptr [eax]
0x4934B7: mov      edx, dword ptr [ebp - 0x10]
0x4934BA: call     0x466d40
0x4934BF: xor      eax, eax
0x4934C1: pop      edx
0x4934C2: pop      ecx
0x4934C3: pop      ecx
0x4934C4: mov      dword ptr fs:[eax], edx
0x4934C7: push     0x4934e4
0x4934CC: lea      eax, [ebp - 0x1c]
0x4934CF: call     0x403a58
0x4934D4: lea      eax, [ebp - 0x10]
0x4934D7: call     0x403a58
0x4934DC: ret      
0x4934DD: jmp      0x403518
0x4934E2: jmp      0x4934cc
0x4934E4: pop      ebx
0x4934E5: mov      esp, ebp
0x4934E7: pop      ebp
0x4934E8: ret      
0x4934E9: add      byte ptr [eax], al
0x4934EB: add      bh, bh
0x4934EF: inc      dword ptr [edx]
0x4934F1: add      byte ptr [eax], al
0x4934F3: add      byte ptr [eax], ah
0x4934F5: or       eax, 0xffff0000
0x4934FB: inc      dword ptr [ecx]
0x4934FD: add      byte ptr [eax], al
0x4934FF: add      byte ptr [0x4000000], cl
0x493505: add      byte ptr [eax], al
0x493507: add      bh, bh
0x49350B: jmp      dword ptr [edx]
0x49350D: add      byte ptr [eax], al
0x49350F: add      bl, dl
0x493511: leave    
0x493512: rcr      edx, cl
0x493514: mov      esi, 0xccd3d7e4
0x493519: stosd    dword ptr es:[edi], eax
0x49351A: mov      bl, 0xa4
0x49351C: mov      dword ptr [0xbaeed7ac], eax
0x493521: mov      ch, 0xc4
0x493524: mov      dl, 0xbf
0x493526: mov      bh, 0xd6
0x493528: mov      cl, 0xbb
0x49352A: rol      dh, cl
0x49352C: mov      bl, 0xfd
0x49352E: ror      ebx, 0xa1
0x493531: mov      dword ptr [0x8b550000], eax
0x493536: in       al, dx
0x493537: add      esp, -0x1c
0x49353A: push     ebx
0x49353B: xor      edx, edx
0x49353D: mov      dword ptr [ebp - 0x1c], edx
0x493540: mov      dword ptr [ebp - 0xc], edx
0x493543: mov      dword ptr [ebp - 4], eax
0x493546: xor      eax, eax
0x493548: push     ebp
0x493549: push     0x4937cc
0x49354E: push     dword ptr fs:[eax]
0x493551: mov      dword ptr fs:[eax], esp
0x493554: mov      eax, dword ptr [ebp - 4]
0x493557: mov      eax, dword ptr [eax + 0x9ac]
0x49355D: mov      edx, dword ptr [eax]
0x49355F: call     dword ptr [edx + 0x14]
0x493562: test     eax, eax
0x493564: jg       0x49357f
0x493566: mov      cx, word ptr [0x4937d8]
0x49356D: mov      edx, 0x4937e4 ; "请点击 [名单]按钮以调出行会成员信息。"
0x493572: mov      eax, dword ptr [ebp - 4]
0x493575: call     0x486df0
0x49357A: jmp      0x4937ae
0x49357F: mov      eax, dword ptr [ebp - 4]
0x493582: mov      eax, dword ptr [eax + 0x500]
0x493588: mov      dword ptr [ebp - 0x14], eax
0x49358B: mov      eax, dword ptr [ebp - 0x14]
0x49358E: mov      edx, dword ptr [eax + 0x258]
0x493594: mov      eax, dword ptr [ebp - 0x14]
0x493597: mov      eax, dword ptr [eax + 0x254]
0x49359D: call     0x45351c
0x4935A2: mov      dword ptr [ebp - 8], eax
0x4935A5: cmp      dword ptr [ebp - 8], 0
0x4935A9: je       0x4935e7
0x4935AB: mov      eax, dword ptr [ebp - 8]
0x4935AE: call     0x44dc60
0x4935B3: mov      edx, 0x320
0x4935B8: sub      edx, eax
0x4935BA: sar      edx, 1
0x4935BC: jns      0x4935c1
0x4935BE: adc      edx, 0
0x4935C1: mov      eax, dword ptr [ebp - 0x14]
0x4935C4: call     0x42718c
0x4935C9: mov      eax, dword ptr [ebp - 8]
0x4935CC: call     0x44d944
0x4935D1: mov      edx, 0x258
0x4935D6: sub      edx, eax
0x4935D8: sar      edx, 1
0x4935DA: jns      0x4935df
0x4935DC: adc      edx, 0
0x4935DF: mov      eax, dword ptr [ebp - 0x14]
0x4935E2: call     0x4271b0
0x4935E7: mov      eax, dword ptr [ebp - 4]
0x4935EA: call     0x481cec
0x4935EF: mov      eax, dword ptr [ebp - 4]
0x4935F2: mov      eax, dword ptr [eax + 0x500]
0x4935F8: call     0x45843c
0x4935FD: mov      eax, dword ptr [ebp - 0x14]
0x493600: mov      edx, dword ptr [eax + 0x30]
0x493603: add      edx, 0x10
0x493606: mov      eax, dword ptr [ebp - 0x14]
0x493609: call     0x456f94
0x49360E: mov      edx, eax
0x493610: mov      eax, dword ptr [ebp - 4]
0x493613: mov      eax, dword ptr [eax + 0x5ec]
0x493619: call     0x42718c
0x49361E: mov      eax, dword ptr [ebp - 0x14]
0x493621: mov      edx, dword ptr [eax + 0x34]
0x493624: add      edx, 0x24
0x493627: mov      eax, dword ptr [ebp - 0x14]
0x49362A: call     0x456fdc
0x49362F: mov      edx, eax
0x493631: mov      eax, dword ptr [ebp - 4]
0x493634: mov      eax, dword ptr [eax + 0x5ec]
0x49363A: call     0x4271b0
0x49363F: mov      edx, 0x23b
0x493644: mov      eax, dword ptr [ebp - 4]
0x493647: mov      eax, dword ptr [eax + 0x5ec]
0x49364D: call     0x4271d8
0x493652: mov      edx, 0xf6
0x493657: mov      eax, dword ptr [ebp - 4]
0x49365A: mov      eax, dword ptr [eax + 0x5ec]
0x493660: call     0x4271fc
0x493665: mov      eax, dword ptr [ebp - 4]
0x493668: mov      eax, dword ptr [eax + 0x5ec]
0x49366E: mov      eax, dword ptr [eax + 0x204]
0x493674: mov      edx, dword ptr [ebp - 4]
0x493677: mov      edx, dword ptr [edx + 0x9ac]
0x49367D: mov      ecx, dword ptr [eax]
0x49367F: call     dword ptr [ecx + 8]
0x493682: mov      dl, 1
0x493684: mov      eax, dword ptr [ebp - 4]
0x493687: mov      eax, dword ptr [eax + 0x5ec]
0x49368D: call     0x4278c0
0x493692: mov      eax, dword ptr [ebp - 4]
0x493695: mov      eax, dword ptr [eax + 0x500]
0x49369B: cmp      byte ptr [eax + 0x24c], 0
0x4936A2: je       0x4936d2
0x4936A4: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4936A9: mov      eax, dword ptr [eax]
0x4936AB: call     0x460c34
0x4936B0: mov      eax, dword ptr [0x4a4110] ; *0x4A4110->0x4A57A4
0x4936B5: mov      eax, dword ptr [eax]
0x4936B7: call     0x443574
0x4936BC: mov      eax, dword ptr [0x4a4110] ; *0x4A4110->0x4A57A4
0x4936C1: mov      eax, dword ptr [eax]
0x4936C3: cmp      byte ptr [eax + 0x84], 0
0x4936CA: jne      0x4937ae
0x4936D0: jmp      0x493692
0x4936D2: xor      edx, edx
0x4936D4: mov      eax, dword ptr [ebp - 4]
0x4936D7: mov      eax, dword ptr [eax + 0x500]
0x4936DD: call     0x458214
0x4936E2: mov      eax, dword ptr [ebp - 4]
0x4936E5: call     0x481d88
0x4936EA: mov      eax, dword ptr [ebp - 4]
0x4936ED: mov      eax, dword ptr [eax + 0x368]
0x4936F3: cmp      dword ptr [eax + 0x284], 1
0x4936FA: jne      0x4937ae
0x493700: lea      eax, [ebp - 0xc]
0x493703: call     0x403a58
0x493708: mov      eax, dword ptr [ebp - 4]
0x49370B: mov      eax, dword ptr [eax + 0x5ec]
0x493711: mov      eax, dword ptr [eax + 0x204]
0x493717: mov      edx, dword ptr [eax]
0x493719: call     dword ptr [edx + 0x14]
0x49371C: dec      eax
0x49371D: test     eax, eax
0x49371F: jl       0x493766
0x493721: inc      eax
0x493722: mov      dword ptr [ebp - 0x18], eax
0x493725: mov      dword ptr [ebp - 0x10], 0
0x49372C: push     dword ptr [ebp - 0xc]
0x49372F: lea      ecx, [ebp - 0x1c]
0x493732: mov      eax, dword ptr [ebp - 4]
0x493735: mov      eax, dword ptr [eax + 0x5ec]
0x49373B: mov      eax, dword ptr [eax + 0x204]
0x493741: mov      edx, dword ptr [ebp - 0x10]
0x493744: mov      ebx, dword ptr [eax]
0x493746: call     dword ptr [ebx + 0xc]
0x493749: push     dword ptr [ebp - 0x1c]
0x49374C: push     0x493814 ; "\r"
0x493751: lea      eax, [ebp - 0xc]
0x493754: mov      edx, 3
0x493759: call     0x403d94
0x49375E: inc      dword ptr [ebp - 0x10]
0x493761: dec      dword ptr [ebp - 0x18]
0x493764: jne      0x49372c
0x493766: mov      eax, dword ptr [ebp - 0xc]
0x493769: call     0x403cd4
0x49376E: cmp      eax, 0x1388
0x493773: jle      0x49379f
0x493775: lea      eax, [ebp - 0xc]
0x493778: push     eax
0x493779: mov      ecx, 0x1388
0x49377E: mov      edx, 1
0x493783: mov      eax, dword ptr [ebp - 0xc]
0x493786: call     0x403ed8
0x49378B: mov      cx, word ptr [0x4937d8]
0x493792: mov      edx, 0x493820 ; "由于句子太长，最后的部分被移除了。"
0x493797: mov      eax, dword ptr [ebp - 4]
0x49379A: call     0x486df0
0x49379F: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4937A4: mov      eax, dword ptr [eax]
0x4937A6: mov      edx, dword ptr [ebp - 0xc]
0x4937A9: call     0x466ddc
0x4937AE: xor      eax, eax
0x4937B0: pop      edx
0x4937B1: pop      ecx
0x4937B2: pop      ecx
0x4937B3: mov      dword ptr fs:[eax], edx
0x4937B6: push     0x4937d3
0x4937BB: lea      eax, [ebp - 0x1c]
0x4937BE: call     0x403a58
0x4937C3: lea      eax, [ebp - 0xc]
0x4937C6: call     0x403a58
0x4937CB: ret      
0x4937CC: jmp      0x403518
0x4937D1: jmp      0x4937bb
0x4937D3: pop      ebx
0x4937D4: mov      esp, ebp
0x4937D6: pop      ebp
0x4937D7: ret      
0x4937D8: add      al, 0
0x4937DA: add      byte ptr [eax], al
0x4937E4: [STR len=37] "请点击 [名单]按钮以调出行会成员信息。"
0x493814: [STR len=1] "\r"
0x493820: [STR len=34] "由于句子太长，最后的部分被移除了。"
0x493844: push     ebp
0x493845: mov      ebp, esp
0x493847: add      esp, -0x40
0x49384A: push     ebx
0x49384B: xor      ebx, ebx
0x49384D: mov      dword ptr [ebp - 0x40], ebx
0x493850: mov      dword ptr [ebp - 8], ecx
0x493853: mov      dword ptr [ebp - 0x2c], edx
0x493856: mov      dword ptr [ebp - 4], eax
0x493859: xor      eax, eax
0x49385B: push     ebp
0x49385C: push     0x493a3c
0x493861: push     dword ptr fs:[eax]
0x493864: mov      dword ptr fs:[eax], esp
0x493867: mov      eax, dword ptr [ebp - 4]
0x49386A: mov      eax, dword ptr [eax + 0x4c4]
0x493870: mov      dword ptr [ebp - 0x20], eax
0x493873: mov      eax, dword ptr [ebp - 0x20]
0x493876: mov      edx, dword ptr [eax + 0x258]
0x49387C: mov      eax, dword ptr [ebp - 0x20]
0x49387F: mov      eax, dword ptr [eax + 0x254]
0x493885: call     0x45351c
0x49388A: mov      dword ptr [ebp - 0xc], eax
0x49388D: cmp      dword ptr [ebp - 0xc], 0
0x493891: je       0x4938d0
0x493893: lea      edx, [ebp - 0x3c]
0x493896: mov      eax, dword ptr [ebp - 0xc]
0x493899: call     0x44d8ec
0x49389E: lea      eax, [ebp - 0x3c]
0x4938A1: push     eax
0x4938A2: mov      eax, dword ptr [ebp - 0xc]
0x4938A5: push     eax
0x4938A6: push     1
0x4938A8: mov      eax, dword ptr [ebp - 0x20]
0x4938AB: mov      edx, dword ptr [eax + 0x34]
0x4938AE: mov      eax, dword ptr [ebp - 0x20]
0x4938B1: call     0x456fdc
0x4938B6: push     eax
0x4938B7: mov      eax, dword ptr [ebp - 0x20]
0x4938BA: mov      edx, dword ptr [eax + 0x30]
0x4938BD: mov      eax, dword ptr [ebp - 0x20]
0x4938C0: call     0x456f94
0x4938C5: mov      edx, eax
0x4938C7: mov      eax, dword ptr [ebp - 8]
0x4938CA: pop      ecx
0x4938CB: call     0x44d6cc
0x4938D0: mov      eax, dword ptr [ebp - 8]
0x4938D3: call     0x44d8b4
0x4938D8: mov      dword ptr [ebp - 0x24], eax
0x4938DB: push     1
0x4938DD: mov      eax, dword ptr [ebp - 0x24]
0x4938E0: call     0x41834c
0x4938E5: push     eax
0x4938E6: call     0x406434
0x4938EB: mov      eax, dword ptr [ebp - 0x24]
0x4938EE: mov      eax, dword ptr [eax + 0xc]
0x4938F1: mov      edx, 0xffffff
0x4938F6: call     0x4173fc
0x4938FB: mov      eax, dword ptr [ebp - 4]
0x4938FE: mov      eax, dword ptr [eax + 0x994]
0x493904: push     eax
0x493905: mov      eax, dword ptr [ebp - 0x20]
0x493908: mov      ecx, dword ptr [eax + 0x34]
0x49390B: add      ecx, 0xd
0x49390E: mov      eax, dword ptr [ebp - 0x20]
0x493911: mov      edx, dword ptr [eax + 0x30]
0x493914: add      edx, 0x140
0x49391A: mov      eax, dword ptr [ebp - 0x24]
0x49391D: call     0x4180dc
0x493922: mov      eax, dword ptr [ebp - 0x20]
0x493925: mov      eax, dword ptr [eax + 0x30]
0x493928: add      eax, 0x18
0x49392B: mov      dword ptr [ebp - 0x18], eax
0x49392E: mov      eax, dword ptr [ebp - 0x20]
0x493931: mov      eax, dword ptr [eax + 0x34]
0x493934: add      eax, 0x29
0x493937: mov      dword ptr [ebp - 0x1c], eax
0x49393A: mov      eax, dword ptr [ebp - 4]
0x49393D: mov      eax, dword ptr [eax + 0x9b0]
0x493943: push     eax
0x493944: mov      eax, dword ptr [ebp - 4]
0x493947: mov      eax, dword ptr [eax + 0x9a0]
0x49394D: mov      edx, dword ptr [eax]
0x49394F: call     dword ptr [edx + 0x14]
0x493952: dec      eax
0x493953: pop      edx
0x493954: sub      eax, edx
0x493956: jl       0x493a1e
0x49395C: inc      eax
0x49395D: mov      dword ptr [ebp - 0x28], eax
0x493960: mov      dword ptr [ebp - 0x10], edx
0x493963: mov      eax, dword ptr [ebp - 0x10]
0x493966: mov      edx, dword ptr [ebp - 4]
0x493969: sub      eax, dword ptr [edx + 0x9b0]
0x49396F: mov      dword ptr [ebp - 0x14], eax
0x493972: imul     eax, dword ptr [ebp - 0x14], 0xe
0x493976: cmp      eax, 0x164
0x49397B: jg       0x493a1e
0x493981: mov      edx, dword ptr [ebp - 0x10]
0x493984: mov      eax, dword ptr [ebp - 4]
0x493987: mov      eax, dword ptr [eax + 0x9a0]
0x49398D: mov      ecx, dword ptr [eax]
0x49398F: call     dword ptr [ecx + 0x18]
0x493992: test     eax, eax
0x493994: je       0x4939b6
0x493996: mov      edx, dword ptr [ebp - 0x10]
0x493999: mov      eax, dword ptr [ebp - 4]
0x49399C: mov      eax, dword ptr [eax + 0x9a0]
0x4939A2: mov      ecx, dword ptr [eax]
0x4939A4: call     dword ptr [ecx + 0x18]
0x4939A7: mov      edx, eax
0x4939A9: mov      eax, dword ptr [ebp - 0x24]
0x4939AC: mov      eax, dword ptr [eax + 0xc]
0x4939AF: call     0x4173fc
0x4939B4: jmp      0x4939e8
0x4939B6: mov      eax, dword ptr [ebp - 4]
0x4939B9: cmp      byte ptr [eax + 0x9bc], 0
0x4939C0: je       0x4939d8
0x4939C2: mov      al, 2
0x4939C4: call     0x45edc8
0x4939C9: mov      edx, eax
0x4939CB: mov      eax, dword ptr [ebp - 0x24]
0x4939CE: mov      eax, dword ptr [eax + 0xc]
0x4939D1: call     0x4173fc
0x4939D6: jmp      0x4939e8
0x4939D8: mov      eax, dword ptr [ebp - 0x24]
0x4939DB: mov      eax, dword ptr [eax + 0xc]
0x4939DE: mov      edx, 0xc0c0c0
0x4939E3: call     0x4173fc
0x4939E8: lea      ecx, [ebp - 0x40]
0x4939EB: mov      edx, dword ptr [ebp - 0x10]
0x4939EE: mov      eax, dword ptr [ebp - 4]
0x4939F1: mov      eax, dword ptr [eax + 0x9a0]
0x4939F7: mov      ebx, dword ptr [eax]
0x4939F9: call     dword ptr [ebx + 0xc]
0x4939FC: mov      eax, dword ptr [ebp - 0x40]
0x4939FF: push     eax
0x493A00: imul     ecx, dword ptr [ebp - 0x14], 0xe
0x493A04: add      ecx, dword ptr [ebp - 0x1c]
0x493A07: mov      edx, dword ptr [ebp - 0x18]
0x493A0A: mov      eax, dword ptr [ebp - 0x24]
0x493A0D: call     0x4180dc
0x493A12: inc      dword ptr [ebp - 0x10]
0x493A15: dec      dword ptr [ebp - 0x28]
0x493A18: jne      0x493963
0x493A1E: mov      eax, dword ptr [ebp - 0x24]
0x493A21: call     0x44d104
0x493A26: xor      eax, eax
0x493A28: pop      edx
0x493A29: pop      ecx
0x493A2A: pop      ecx
0x493A2B: mov      dword ptr fs:[eax], edx
0x493A2E: push     0x493a43
0x493A33: lea      eax, [ebp - 0x40]
0x493A36: call     0x403a58
0x493A3B: ret      
0x493A3C: jmp      0x403518
0x493A41: jmp      0x493a33
0x493A43: pop      ebx
0x493A44: mov      esp, ebp
0x493A46: pop      ebp
0x493A47: ret      
0x493A48: push     ebp
0x493A49: mov      ebp, esp
0x493A4B: add      esp, -0xc
0x493A4E: mov      dword ptr [ebp - 0xc], ecx
0x493A51: mov      dword ptr [ebp - 8], edx
0x493A54: mov      dword ptr [ebp - 4], eax
0x493A57: mov      eax, dword ptr [ebp - 4]
0x493A5A: cmp      dword ptr [eax + 0x9b0], 0
0x493A61: jle      0x493a6d
0x493A63: mov      eax, dword ptr [ebp - 4]
0x493A66: sub      dword ptr [eax + 0x9b0], 3
0x493A6D: mov      eax, dword ptr [ebp - 4]
0x493A70: cmp      dword ptr [eax + 0x9b0], 0
0x493A77: jge      0x493a84
0x493A79: mov      eax, dword ptr [ebp - 4]
0x493A7C: xor      edx, edx
0x493A7E: mov      dword ptr [eax + 0x9b0], edx
0x493A84: mov      esp, ebp
0x493A86: pop      ebp
0x493A87: ret      4
0x493A8A: mov      eax, eax
0x493A8C: push     ebp
0x493A8D: mov      ebp, esp
0x493A8F: add      esp, -0xc
0x493A92: mov      dword ptr [ebp - 0xc], ecx
0x493A95: mov      dword ptr [ebp - 8], edx
0x493A98: mov      dword ptr [ebp - 4], eax
0x493A9B: mov      eax, dword ptr [ebp - 4]
0x493A9E: mov      eax, dword ptr [eax + 0x9a0]
0x493AA4: mov      edx, dword ptr [eax]
0x493AA6: call     dword ptr [edx + 0x14]
0x493AA9: mov      edx, dword ptr [ebp - 4]
0x493AAC: mov      edx, dword ptr [edx + 0x9b0]
0x493AB2: add      edx, 0xc
0x493AB5: cmp      eax, edx
0x493AB7: jle      0x493ac3
0x493AB9: mov      eax, dword ptr [ebp - 4]
0x493ABC: add      dword ptr [eax + 0x9b0], 3
0x493AC3: mov      esp, ebp
0x493AC5: pop      ebp
0x493AC6: ret      4
0x493AC9: lea      eax, [eax]
0x493ACC: push     ebp
0x493ACD: mov      ebp, esp
0x493ACF: add      esp, -0xc
0x493AD2: mov      dword ptr [ebp - 0xc], ecx
0x493AD5: mov      dword ptr [ebp - 8], edx
0x493AD8: mov      dword ptr [ebp - 4], eax
0x493ADB: xor      edx, edx
0x493ADD: mov      eax, dword ptr [ebp - 4]
0x493AE0: mov      eax, dword ptr [eax + 0x4c4]
0x493AE6: call     0x458214
0x493AEB: mov      eax, dword ptr [ebp - 4]
0x493AEE: mov      byte ptr [eax + 0x9bc], 0
0x493AF5: mov      esp, ebp
0x493AF7: pop      ebp
0x493AF8: ret      4
0x493AFB: nop      
0x493AFC: push     ebp
0x493AFD: mov      ebp, esp
0x493AFF: add      esp, -0xc
0x493B02: mov      dword ptr [ebp - 0xc], ecx
0x493B05: mov      dword ptr [ebp - 8], edx
0x493B08: mov      dword ptr [ebp - 4], eax
0x493B0B: call     0x406154
0x493B10: mov      edx, dword ptr [0x4a3e48] ; *0x4A3E48->0x4F94F0
0x493B16: cmp      eax, dword ptr [edx]
0x493B18: jbe      0x493b42
0x493B1A: call     0x406154
0x493B1F: add      eax, 0xbb8
0x493B24: mov      edx, dword ptr [0x4a3e48] ; *0x4A3E48->0x4F94F0
0x493B2A: mov      dword ptr [edx], eax
0x493B2C: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x493B31: mov      eax, dword ptr [eax]
0x493B33: call     0x466b18
0x493B38: mov      eax, dword ptr [ebp - 4]
0x493B3B: mov      byte ptr [eax + 0x9bc], 0
0x493B42: mov      esp, ebp
0x493B44: pop      ebp
0x493B45: ret      4
0x493B48: push     ebp
0x493B49: mov      ebp, esp
0x493B4B: add      esp, -0xc
0x493B4E: mov      dword ptr [ebp - 0xc], ecx
0x493B51: mov      dword ptr [ebp - 8], edx
0x493B54: mov      dword ptr [ebp - 4], eax
0x493B57: call     0x406154
0x493B5C: mov      edx, dword ptr [0x4a3e48] ; *0x4A3E48->0x4F94F0
0x493B62: cmp      eax, dword ptr [edx]
0x493B64: jbe      0x493b8e
0x493B66: call     0x406154
0x493B6B: add      eax, 0xbb8
0x493B70: mov      edx, dword ptr [0x4a3e48] ; *0x4A3E48->0x4F94F0
0x493B76: mov      dword ptr [edx], eax
0x493B78: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x493B7D: mov      eax, dword ptr [eax]
0x493B7F: call     0x466b80
0x493B84: mov      eax, dword ptr [ebp - 4]
0x493B87: mov      byte ptr [eax + 0x9bc], 0
0x493B8E: mov      esp, ebp
0x493B90: pop      ebp
0x493B91: ret      4
0x493B94: push     ebp
0x493B95: mov      ebp, esp
0x493B97: add      esp, -0x10
0x493B9A: push     ebx
0x493B9B: xor      ebx, ebx
0x493B9D: mov      dword ptr [ebp - 0x10], ebx
0x493BA0: mov      dword ptr [ebp - 0xc], ecx
0x493BA3: mov      dword ptr [ebp - 8], edx
0x493BA6: mov      dword ptr [ebp - 4], eax
0x493BA9: xor      eax, eax
0x493BAB: push     ebp
0x493BAC: push     0x493c16
0x493BB1: push     dword ptr fs:[eax]
0x493BB4: mov      dword ptr fs:[eax], esp
0x493BB7: lea      eax, [ebp - 0x10]
0x493BBA: mov      ecx, 0x493c2c ; "输入你想加为行会成员的角色名。"
0x493BBF: mov      edx, dword ptr [ebp - 4]
0x493BC2: mov      edx, dword ptr [edx + 0x994]
0x493BC8: call     0x403d20
0x493BCD: mov      edx, dword ptr [ebp - 0x10]
0x493BD0: mov      cx, word ptr [0x493c4c]
0x493BD7: mov      eax, dword ptr [ebp - 4]
0x493BDA: call     0x486df0
0x493BDF: mov      eax, dword ptr [ebp - 4]
0x493BE2: cmp      dword ptr [eax + 0x784], 0
0x493BE9: je       0x493c00
0x493BEB: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x493BF0: mov      eax, dword ptr [eax]
0x493BF2: mov      edx, dword ptr [ebp - 4]
0x493BF5: mov      edx, dword ptr [edx + 0x784]
0x493BFB: call     0x466be8
0x493C00: xor      eax, eax
0x493C02: pop      edx
0x493C03: pop      ecx
0x493C04: pop      ecx
0x493C05: mov      dword ptr fs:[eax], edx
0x493C08: push     0x493c1d
0x493C0D: lea      eax, [ebp - 0x10]
0x493C10: call     0x403a58
0x493C15: ret      
0x493C16: jmp      0x403518
0x493C1B: jmp      0x493c0d
0x493C1D: pop      ebx
0x493C1E: mov      esp, ebp
0x493C20: pop      ebp
0x493C21: ret      4
0x493C2C: [STR len=30] "输入你想加为行会成员的角色名。"
0x493C4C: adc      al, 0
0x493C4E: add      byte ptr [eax], al
0x493C50: push     ebp
0x493C51: mov      ebp, esp
0x493C53: add      esp, -0x10
0x493C56: push     ebx
0x493C57: xor      ebx, ebx
0x493C59: mov      dword ptr [ebp - 0x10], ebx
0x493C5C: mov      dword ptr [ebp - 0xc], ecx
0x493C5F: mov      dword ptr [ebp - 8], edx
0x493C62: mov      dword ptr [ebp - 4], eax
0x493C65: xor      eax, eax
0x493C67: push     ebp
0x493C68: push     0x493cd2
0x493C6D: push     dword ptr fs:[eax]
0x493C70: mov      dword ptr fs:[eax], esp
0x493C73: lea      eax, [ebp - 0x10]
0x493C76: mov      ecx, 0x493ce8 ; "输入你想从行会中删除的角色名。"
0x493C7B: mov      edx, dword ptr [ebp - 4]
0x493C7E: mov      edx, dword ptr [edx + 0x994]
0x493C84: call     0x403d20
0x493C89: mov      edx, dword ptr [ebp - 0x10]
0x493C8C: mov      cx, word ptr [0x493d08]
0x493C93: mov      eax, dword ptr [ebp - 4]
0x493C96: call     0x486df0
0x493C9B: mov      eax, dword ptr [ebp - 4]
0x493C9E: cmp      dword ptr [eax + 0x784], 0
0x493CA5: je       0x493cbc
0x493CA7: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x493CAC: mov      eax, dword ptr [eax]
0x493CAE: mov      edx, dword ptr [ebp - 4]
0x493CB1: mov      edx, dword ptr [edx + 0x784]
0x493CB7: call     0x466c94
0x493CBC: xor      eax, eax
0x493CBE: pop      edx
0x493CBF: pop      ecx
0x493CC0: pop      ecx
0x493CC1: mov      dword ptr fs:[eax], edx
0x493CC4: push     0x493cd9
0x493CC9: lea      eax, [ebp - 0x10]
0x493CCC: call     0x403a58
0x493CD1: ret      
0x493CD2: jmp      0x403518
0x493CD7: jmp      0x493cc9
0x493CD9: pop      ebx
0x493CDA: mov      esp, ebp
0x493CDC: pop      ebp
0x493CDD: ret      4
0x493CE8: [STR len=30] "输入你想从行会中删除的角色名。"
0x493D08: adc      al, 0
0x493D0A: add      byte ptr [eax], al
0x493D0C: push     ebp
0x493D0D: mov      ebp, esp
0x493D0F: add      esp, -0xc
0x493D12: mov      dword ptr [ebp - 0xc], ecx
0x493D15: mov      dword ptr [ebp - 8], edx
0x493D18: mov      dword ptr [ebp - 4], eax
0x493D1B: mov      eax, dword ptr [ebp - 4]
0x493D1E: add      eax, 0x9b4
0x493D23: mov      edx, 0x493d44 ; "[修改行会公告内容.]"
0x493D28: call     0x403aac
0x493D2D: mov      eax, dword ptr [ebp - 4]
0x493D30: call     0x493214
0x493D35: mov      esp, ebp
0x493D37: pop      ebp
0x493D38: ret      4
0x493D3B: add      bh, bh
0x493D3F: call     dword ptr [ebx]
0x493D41: add      byte ptr [eax], al
0x493D43: add      byte ptr [ebx - 0x30], bl
0x493D46: fidivr   word ptr [eax - 0x442f2f3c]
0x493D4C: loope    0x493d07
0x493D4E: stosd    dword ptr es:[edi], eax
0x493D4F: mov      eax, 0xc8dac4e6
0x493D55: pop      ebp
0x493D57: add      byte ptr [ebp - 0x75], dl
0x493D5A: in       al, dx
0x493D5B: add      esp, -0xc
0x493D5E: mov      dword ptr [ebp - 0xc], ecx
0x493D61: mov      dword ptr [ebp - 8], edx
0x493D64: mov      dword ptr [ebp - 4], eax
0x493D67: mov      eax, dword ptr [ebp - 4]
0x493D6A: add      eax, 0x9b4
0x493D6F: mov      edx, 0x493d90 ; "[修改行会成员的等级和职位。 # 警告 : 不能增加行会成员/删除行会成员。]"
0x493D74: call     0x403aac
0x493D79: mov      eax, dword ptr [ebp - 4]
0x493D7C: call     0x493534
0x493D81: mov      esp, ebp
0x493D83: pop      ebp
0x493D84: ret      4
0x493D87: add      bh, bh
0x493D8B: inc      dword ptr [ebp]
0x493D8E: add      byte ptr [eax], al
0x493D90: pop      ebx
0x493D91: rcr      dh, 1
0x493D93: mov      eax, 0xbbd0d0c4
0x493D98: loope    0x493d4d
0x493D9A: leave    
0x493D9B: aam      0xb1
0x493D9D: mov      ch, 0xc4
0x493D9F: mov      ch, 0xc8
0x493DA1: mov      esp, 0xd6cdbab6
0x493DA6: mov      al, 0xce
0x493DA8: mov      ebx, 0x2320a3a1
0x493DAD: and      byte ptr [esi + 0x20e6b8af], bh
0x493DB3: cmp      ah, byte ptr [eax]
0x493DB5: mov      dl, 0xbb
0x493DB9: aam      0xf6
0x493DBB: mov      esp, 0xbbd0d0d3
0x493DC0: loope    0x493d75
0x493DC2: leave    
0x493DC3: aam      0xb1
0x493DC5: das      
0x493DC6: leave    
0x493DC7: mov      esi, 0xd0d0fdb3
0x493DCC: mov      ebx, 0xd4c9b3e1
0x493DD1: mov      cl, 0xa1
0x493DD3: mov      dword ptr [0x5d], eax
0x493DD8: push     ebp
0x493DD9: mov      ebp, esp
0x493DDB: add      esp, -0xc
0x493DDE: mov      dword ptr [ebp - 0xc], ecx
0x493DE1: mov      dword ptr [ebp - 8], edx
0x493DE4: mov      dword ptr [ebp - 4], eax
0x493DE7: mov      cx, word ptr [0x493e18]
0x493DEE: mov      edx, 0x493e24 ; "和对方行会结盟应该在 [结盟可能]状态下。\而且你应该面对对方行会首领。\你想结盟吗？"
0x493DF3: mov      eax, dword ptr [ebp - 4]
0x493DF6: call     0x486df0
0x493DFB: dec      eax
0x493DFC: jne      0x493e0f
0x493DFE: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x493E03: mov      eax, dword ptr [eax]
0x493E05: mov      edx, 0x493e80 ; "@联盟"
0x493E0A: call     0x46549c
0x493E0F: mov      esp, ebp
0x493E11: pop      ebp
0x493E12: ret      4
0x493E15: add      byte ptr [eax], al
0x493E17: add      byte ptr [eax + eax], cl
0x493E1A: add      byte ptr [eax], al
0x493E24: [STR len=81] "和对方行会结盟应该在 [结盟可能]状态下。\而且你应该面对对方行会首领。\你想结盟吗？"
0x493E80: [STR len=5] "@联盟"
0x493E88: push     ebp
0x493E89: mov      ebp, esp
0x493E8B: add      esp, -0x10
0x493E8E: push     ebx
0x493E8F: xor      ebx, ebx
0x493E91: mov      dword ptr [ebp - 0x10], ebx
0x493E94: mov      dword ptr [ebp - 0xc], ecx
0x493E97: mov      dword ptr [ebp - 8], edx
0x493E9A: mov      dword ptr [ebp - 4], eax
0x493E9D: xor      eax, eax
0x493E9F: push     ebp
0x493EA0: push     0x493f06
0x493EA5: push     dword ptr fs:[eax]
0x493EA8: mov      dword ptr fs:[eax], esp
0x493EAB: mov      cx, word ptr [0x493f14]
0x493EB2: mov      edx, 0x493f20 ; "请键入你想取消结盟的行会的名字。"
0x493EB7: mov      eax, dword ptr [ebp - 4]
0x493EBA: call     0x486df0
0x493EBF: mov      eax, dword ptr [ebp - 4]
0x493EC2: cmp      dword ptr [eax + 0x784], 0
0x493EC9: je       0x493ef0
0x493ECB: lea      eax, [ebp - 0x10]
0x493ECE: mov      edx, dword ptr [ebp - 4]
0x493ED1: mov      ecx, dword ptr [edx + 0x784]
0x493ED7: mov      edx, 0x493f4c ; "@取消联盟 "
0x493EDC: call     0x403d20
0x493EE1: mov      edx, dword ptr [ebp - 0x10]
0x493EE4: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x493EE9: mov      eax, dword ptr [eax]
0x493EEB: call     0x46549c
0x493EF0: xor      eax, eax
0x493EF2: pop      edx
0x493EF3: pop      ecx
0x493EF4: pop      ecx
0x493EF5: mov      dword ptr fs:[eax], edx
0x493EF8: push     0x493f0d
0x493EFD: lea      eax, [ebp - 0x10]
0x493F00: call     0x403a58
0x493F05: ret      
0x493F06: jmp      0x403518
0x493F0B: jmp      0x493efd
0x493F0D: pop      ebx
0x493F0E: mov      esp, ebp
0x493F10: pop      ebp
0x493F11: ret      4
0x493F14: adc      al, 0
0x493F16: add      byte ptr [eax], al
0x493F20: [STR len=32] "请键入你想取消结盟的行会的名字。"
0x493F4C: [STR len=10] "@取消联盟 "
0x493F58: push     ebp
0x493F59: mov      ebp, esp
0x493F5B: add      esp, -0x28
0x493F5E: mov      dword ptr [ebp - 8], ecx
0x493F61: mov      dword ptr [ebp - 0x18], edx
0x493F64: mov      dword ptr [ebp - 4], eax
0x493F67: mov      eax, dword ptr [ebp - 4]
0x493F6A: mov      eax, dword ptr [eax + 0x500]
0x493F70: mov      dword ptr [ebp - 0x10], eax
0x493F73: mov      eax, dword ptr [ebp - 0x10]
0x493F76: mov      edx, dword ptr [eax + 0x258]
0x493F7C: mov      eax, dword ptr [ebp - 0x10]
0x493F7F: mov      eax, dword ptr [eax + 0x254]
0x493F85: call     0x45351c
0x493F8A: mov      dword ptr [ebp - 0xc], eax
0x493F8D: cmp      dword ptr [ebp - 0xc], 0
0x493F91: je       0x493fd0
0x493F93: lea      edx, [ebp - 0x28]
0x493F96: mov      eax, dword ptr [ebp - 0xc]
0x493F99: call     0x44d8ec
0x493F9E: lea      eax, [ebp - 0x28]
0x493FA1: push     eax
0x493FA2: mov      eax, dword ptr [ebp - 0xc]
0x493FA5: push     eax
0x493FA6: push     1
0x493FA8: mov      eax, dword ptr [ebp - 0x10]
0x493FAB: mov      edx, dword ptr [eax + 0x34]
0x493FAE: mov      eax, dword ptr [ebp - 0x10]
0x493FB1: call     0x456fdc
0x493FB6: push     eax
0x493FB7: mov      eax, dword ptr [ebp - 0x10]
0x493FBA: mov      edx, dword ptr [eax + 0x30]
0x493FBD: mov      eax, dword ptr [ebp - 0x10]
0x493FC0: call     0x456f94
0x493FC5: mov      edx, eax
0x493FC7: mov      eax, dword ptr [ebp - 8]
0x493FCA: pop      ecx
0x493FCB: call     0x44d6cc
0x493FD0: mov      eax, dword ptr [ebp - 8]
0x493FD3: call     0x44d8b4
0x493FD8: mov      dword ptr [ebp - 0x14], eax
0x493FDB: push     1
0x493FDD: mov      eax, dword ptr [ebp - 0x14]
0x493FE0: call     0x41834c
0x493FE5: push     eax
0x493FE6: call     0x406434
0x493FEB: mov      edx, 0xc0c0c0
0x493FF0: mov      eax, dword ptr [ebp - 0x14]
0x493FF3: mov      eax, dword ptr [eax + 0xc]
0x493FF6: call     0x4173fc
0x493FFB: mov      eax, dword ptr [ebp - 4]
0x493FFE: mov      eax, dword ptr [eax + 0x9b4]
0x494004: push     eax
0x494005: mov      eax, dword ptr [ebp - 0x10]
0x494008: mov      ecx, dword ptr [eax + 0x34]
0x49400B: add      ecx, 0x123
0x494011: mov      eax, dword ptr [ebp - 0x10]
0x494014: mov      edx, dword ptr [eax + 0x30]
0x494017: add      edx, 0x12
0x49401A: mov      eax, dword ptr [ebp - 0x14]
0x49401D: call     0x4180dc
0x494022: mov      eax, dword ptr [ebp - 0x14]
0x494025: call     0x44d104
0x49402A: mov      esp, ebp
0x49402C: pop      ebp
0x49402D: ret      
0x49402E: mov      eax, eax
0x494030: push     ebp
0x494031: mov      ebp, esp
0x494033: add      esp, -0xc
0x494036: mov      dword ptr [ebp - 0xc], ecx
0x494039: mov      dword ptr [ebp - 8], edx
0x49403C: mov      dword ptr [ebp - 4], eax
0x49403F: xor      edx, edx
0x494041: mov      eax, dword ptr [ebp - 4]
0x494044: mov      eax, dword ptr [eax + 0x500]
0x49404A: call     0x458214
0x49404F: xor      edx, edx
0x494051: mov      eax, dword ptr [ebp - 4]
0x494054: mov      eax, dword ptr [eax + 0x5ec]
0x49405A: call     0x4278c0
0x49405F: mov      eax, dword ptr [ebp - 4]
0x494062: mov      eax, dword ptr [eax + 0x368]
0x494068: mov      dword ptr [eax + 0x284], 2
0x494072: mov      esp, ebp
0x494074: pop      ebp
0x494075: ret      4
0x494078: push     ebp
0x494079: mov      ebp, esp
0x49407B: add      esp, -0xc
0x49407E: mov      dword ptr [ebp - 0xc], ecx
0x494081: mov      dword ptr [ebp - 8], edx
0x494084: mov      dword ptr [ebp - 4], eax
0x494087: push     0
0x494089: xor      ecx, ecx
0x49408B: mov      edx, dword ptr [ebp - 4]
0x49408E: mov      eax, dword ptr [ebp - 4]
0x494091: call     0x494030
0x494096: mov      eax, dword ptr [ebp - 4]
0x494099: mov      eax, dword ptr [eax + 0x368]
0x49409F: mov      dword ptr [eax + 0x284], 1
0x4940A9: mov      esp, ebp
0x4940AB: pop      ebp
0x4940AC: ret      4
0x4940AF: nop      
0x4940B0: push     ebp
0x4940B1: mov      ebp, esp
0x4940B3: add      esp, -0xc
0x4940B6: mov      dword ptr [ebp - 8], edx
0x4940B9: mov      dword ptr [ebp - 4], eax
0x4940BC: mov      eax, dword ptr [ebp - 8]
0x4940BF: call     0x403e88
0x4940C4: xor      eax, eax
0x4940C6: push     ebp
0x4940C7: push     0x49414f
0x4940CC: push     dword ptr fs:[eax]
0x4940CF: mov      dword ptr fs:[eax], esp
0x4940D2: mov      edx, dword ptr [ebp - 8]
0x4940D5: mov      eax, dword ptr [ebp - 4]
0x4940D8: mov      eax, dword ptr [eax + 0x9b8]
0x4940DE: mov      ecx, dword ptr [eax]
0x4940E0: call     dword ptr [ecx + 0x34]
0x4940E3: mov      eax, dword ptr [ebp - 4]
0x4940E6: mov      eax, dword ptr [eax + 0x9b8]
0x4940EC: mov      edx, dword ptr [eax]
0x4940EE: call     dword ptr [edx + 0x14]
0x4940F1: cmp      eax, 0x1f4
0x4940F6: jle      0x494116
0x4940F8: xor      eax, eax
0x4940FA: mov      dword ptr [ebp - 0xc], eax
0x4940FD: xor      edx, edx
0x4940FF: mov      eax, dword ptr [ebp - 4]
0x494102: mov      eax, dword ptr [eax + 0x9b8]
0x494108: mov      ecx, dword ptr [eax]
0x49410A: call     dword ptr [ecx + 0x44]
0x49410D: inc      dword ptr [ebp - 0xc]
0x494110: cmp      dword ptr [ebp - 0xc], 0x65
0x494114: jne      0x4940fd
0x494116: mov      eax, dword ptr [ebp - 4]
0x494119: cmp      byte ptr [eax + 0x9bc], 0
0x494120: je       0x494139
0x494122: mov      eax, dword ptr [ebp - 4]
0x494125: mov      edx, dword ptr [eax + 0x9b8]
0x49412B: mov      eax, dword ptr [ebp - 4]
0x49412E: mov      eax, dword ptr [eax + 0x9a0]
0x494134: mov      ecx, dword ptr [eax]
0x494136: call     dword ptr [ecx + 8]
0x494139: xor      eax, eax
0x49413B: pop      edx
0x49413C: pop      ecx
0x49413D: pop      ecx
0x49413E: mov      dword ptr fs:[eax], edx
0x494141: push     0x494156
0x494146: lea      eax, [ebp - 8]
0x494149: call     0x403a58
0x49414E: ret      
0x49414F: jmp      0x403518
0x494154: jmp      0x494146
0x494156: mov      esp, ebp
0x494158: pop      ebp
0x494159: ret      
0x49415A: mov      eax, eax
0x49415C: push     ebp
0x49415D: mov      ebp, esp
0x49415F: add      esp, -0xc
0x494162: mov      dword ptr [ebp - 0xc], ecx
0x494165: mov      dword ptr [ebp - 8], edx
0x494168: mov      dword ptr [ebp - 4], eax
0x49416B: mov      eax, dword ptr [ebp - 4]
0x49416E: mov      al, byte ptr [eax + 0x9bc]
0x494174: xor      al, 1
0x494176: mov      edx, dword ptr [ebp - 4]
0x494179: mov      byte ptr [edx + 0x9bc], al
0x49417F: mov      eax, dword ptr [ebp - 4]
0x494182: cmp      byte ptr [eax + 0x9bc], 0
0x494189: je       0x4941bb
0x49418B: mov      eax, dword ptr [ebp - 4]
0x49418E: mov      edx, dword ptr [eax + 0x9a0]
0x494194: mov      eax, dword ptr [ebp - 4]
0x494197: mov      eax, dword ptr [eax + 0x9a4]
0x49419D: mov      ecx, dword ptr [eax]
0x49419F: call     dword ptr [ecx + 8]
0x4941A2: mov      eax, dword ptr [ebp - 4]
0x4941A5: mov      edx, dword ptr [eax + 0x9b8]
0x4941AB: mov      eax, dword ptr [ebp - 4]
0x4941AE: mov      eax, dword ptr [eax + 0x9a0]
0x4941B4: mov      ecx, dword ptr [eax]
0x4941B6: call     dword ptr [ecx + 8]
0x4941B9: jmp      0x4941d2
0x4941BB: mov      eax, dword ptr [ebp - 4]
0x4941BE: mov      edx, dword ptr [eax + 0x9a4]
0x4941C4: mov      eax, dword ptr [ebp - 4]
0x4941C7: mov      eax, dword ptr [eax + 0x9a0]
0x4941CD: mov      ecx, dword ptr [eax]
0x4941CF: call     dword ptr [ecx + 8]
0x4941D2: mov      esp, ebp
0x4941D4: pop      ebp
0x4941D5: ret      4
0x4941D8: push     ebp
0x4941D9: mov      ebp, esp
0x4941DB: add      esp, -0x24
0x4941DE: mov      dword ptr [ebp - 8], ecx
0x4941E1: mov      dword ptr [ebp - 0x14], edx
0x4941E4: mov      dword ptr [ebp - 4], eax
0x4941E7: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4941EC: cmp      dword ptr [eax], 0
0x4941EF: je       0x49425a
0x4941F1: mov      eax, dword ptr [ebp - 4]
0x4941F4: mov      eax, dword ptr [eax + 0x2e4]
0x4941FA: mov      dword ptr [ebp - 0x10], eax
0x4941FD: mov      eax, dword ptr [ebp - 0x10]
0x494200: mov      edx, dword ptr [eax + 0x258]
0x494206: mov      eax, dword ptr [ebp - 0x10]
0x494209: mov      eax, dword ptr [eax + 0x254]
0x49420F: call     0x45351c
0x494214: mov      dword ptr [ebp - 0xc], eax
0x494217: cmp      dword ptr [ebp - 0xc], 0
0x49421B: je       0x49425a
0x49421D: lea      edx, [ebp - 0x24]
0x494220: mov      eax, dword ptr [ebp - 0xc]
0x494223: call     0x44d8ec
0x494228: lea      eax, [ebp - 0x24]
0x49422B: push     eax
0x49422C: mov      eax, dword ptr [ebp - 0xc]
0x49422F: push     eax
0x494230: push     1
0x494232: mov      eax, dword ptr [ebp - 0x10]
0x494235: mov      edx, dword ptr [eax + 0x34]
0x494238: mov      eax, dword ptr [ebp - 0x10]
0x49423B: call     0x456fdc
0x494240: push     eax
0x494241: mov      eax, dword ptr [ebp - 0x10]
0x494244: mov      edx, dword ptr [eax + 0x30]
0x494247: mov      eax, dword ptr [ebp - 0x10]
0x49424A: call     0x456f94
0x49424F: mov      edx, eax
0x494251: mov      eax, dword ptr [ebp - 8]
0x494254: pop      ecx
0x494255: call     0x44d6cc
0x49425A: mov      esp, ebp
0x49425C: pop      ebp
0x49425D: ret      
0x49425E: mov      eax, eax
0x494260: push     ebp
0x494261: mov      ebp, esp
0x494263: add      esp, -0xc
0x494266: mov      dword ptr [ebp - 0xc], ecx
0x494269: mov      dword ptr [ebp - 8], edx
0x49426C: mov      dword ptr [ebp - 4], eax
0x49426F: xor      edx, edx
0x494271: mov      eax, dword ptr [ebp - 4]
0x494274: mov      eax, dword ptr [eax + 0x518]
0x49427A: call     0x458214
0x49427F: mov      eax, dword ptr [0x4a4038] ; *0x4A4038->0x4F9280
0x494284: mov      eax, dword ptr [eax]
0x494286: mov      edx, dword ptr [0x4a3fc0] ; *0x4A3FC0->0x4F927C
0x49428C: mov      dword ptr [edx], eax
0x49428E: mov      esp, ebp
0x494290: pop      ebp
0x494291: ret      4
0x494294: push     ebp
0x494295: mov      ebp, esp
0x494297: add      esp, -0x14
0x49429A: mov      dword ptr [ebp - 8], ecx
0x49429D: mov      word ptr [ebp - 4], dx
0x4942A1: mov      byte ptr [ebp - 1], al
0x4942A4: mov      al, byte ptr [ebp - 1]
0x4942A7: mov      byte ptr [ebp - 9], al
0x4942AA: xor      eax, eax
0x4942AC: mov      al, byte ptr [ebp - 1]
0x4942AF: call     0x406974
0x4942B4: mov      byte ptr [ebp - 0xa], al
0x4942B7: mov      eax, dword ptr [ebp - 8]
0x4942BA: mov      byte ptr [eax], 0
0x4942BD: mov      eax, dword ptr [ebp + 8]
0x4942C0: mov      byte ptr [eax], 0
0x4942C3: movzx    eax, word ptr [ebp - 4]
0x4942C7: test     eax, eax
0x4942C9: jle      0x4942fe
0x4942CB: mov      dword ptr [ebp - 0x14], eax
0x4942CE: mov      dword ptr [ebp - 0x10], 1
0x4942D5: xor      eax, eax
0x4942D7: mov      al, byte ptr [ebp - 9]
0x4942DA: inc      eax
0x4942DB: xor      edx, edx
0x4942DD: mov      dl, byte ptr [ebp - 0xa]
0x4942E0: cmp      eax, edx
0x4942E2: jae      0x4942ee
0x4942E4: inc      byte ptr [ebp - 9]
0x4942E7: mov      eax, dword ptr [ebp - 8]
0x4942EA: inc      byte ptr [eax]
0x4942EC: jmp      0x4942f6
0x4942EE: inc      byte ptr [ebp - 0xa]
0x4942F1: mov      eax, dword ptr [ebp + 8]
0x4942F4: inc      byte ptr [eax]
0x4942F6: inc      dword ptr [ebp - 0x10]
0x4942F9: dec      dword ptr [ebp - 0x14]
0x4942FC: jne      0x4942d5
0x4942FE: mov      esp, ebp
0x494300: pop      ebp
0x494301: ret      4
0x494304: push     ebp
0x494305: mov      ebp, esp
0x494307: add      esp, -0x5c
0x49430A: push     ebx
0x49430B: xor      ebx, ebx
0x49430D: mov      dword ptr [ebp - 0x54], ebx
0x494310: mov      dword ptr [ebp - 0x58], ebx
0x494313: mov      dword ptr [ebp - 0x5c], ebx
0x494316: mov      dword ptr [ebp - 8], ecx
0x494319: mov      dword ptr [ebp - 0x40], edx
0x49431C: mov      dword ptr [ebp - 4], eax
0x49431F: xor      eax, eax
0x494321: push     ebp
0x494322: push     0x494eaa
0x494327: push     dword ptr fs:[eax]
0x49432A: mov      dword ptr fs:[eax], esp
0x49432D: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x494332: cmp      dword ptr [eax], 0
0x494335: je       0x494e8f
0x49433B: mov      eax, dword ptr [ebp - 8]
0x49433E: call     0x44d8b4
0x494343: mov      dword ptr [ebp - 0x38], eax
0x494346: mov      eax, dword ptr [ebp - 4]
0x494349: mov      eax, dword ptr [eax + 0x518]
0x49434F: mov      dword ptr [ebp - 0x3c], eax
0x494352: mov      eax, dword ptr [ebp - 0x3c]
0x494355: mov      edx, dword ptr [eax + 0x258]
0x49435B: mov      eax, dword ptr [ebp - 4]
0x49435E: mov      eax, dword ptr [eax + 0x3d0]
0x494364: mov      eax, dword ptr [eax + 0x254]
0x49436A: call     0x45351c
0x49436F: mov      dword ptr [ebp - 0xc], eax
0x494372: cmp      dword ptr [ebp - 0xc], 0
0x494376: je       0x4943b5
0x494378: lea      edx, [ebp - 0x50]
0x49437B: mov      eax, dword ptr [ebp - 0xc]
0x49437E: call     0x44d8ec
0x494383: lea      eax, [ebp - 0x50]
0x494386: push     eax
0x494387: mov      eax, dword ptr [ebp - 0xc]
0x49438A: push     eax
0x49438B: push     1
0x49438D: mov      eax, dword ptr [ebp - 0x3c]
0x494390: mov      edx, dword ptr [eax + 0x34]
0x494393: mov      eax, dword ptr [ebp - 0x3c]
0x494396: call     0x456fdc
0x49439B: push     eax
0x49439C: mov      eax, dword ptr [ebp - 0x3c]
0x49439F: mov      edx, dword ptr [eax + 0x30]
0x4943A2: mov      eax, dword ptr [ebp - 0x3c]
0x4943A5: call     0x456f94
0x4943AA: mov      edx, eax
0x4943AC: mov      eax, dword ptr [ebp - 8]
0x4943AF: pop      ecx
0x4943B0: call     0x44d6cc
0x4943B5: push     1
0x4943B7: mov      eax, dword ptr [ebp - 0x38]
0x4943BA: call     0x41834c
0x4943BF: push     eax
0x4943C0: call     0x406434
0x4943C5: mov      edx, 0xc0c0c0
0x4943CA: mov      eax, dword ptr [ebp - 0x38]
0x4943CD: mov      eax, dword ptr [eax + 0xc]
0x4943D0: call     0x4173fc
0x4943D5: mov      eax, dword ptr [ebp - 4]
0x4943D8: mov      eax, dword ptr [eax + 0x518]
0x4943DE: mov      edx, dword ptr [eax + 0x30]
0x4943E1: mov      eax, dword ptr [ebp - 4]
0x4943E4: mov      eax, dword ptr [eax + 0x518]
0x4943EA: call     0x456f94
0x4943EF: add      eax, 0x24
0x4943F2: mov      dword ptr [ebp - 0x10], eax
0x4943F5: mov      eax, dword ptr [ebp - 4]
0x4943F8: mov      eax, dword ptr [eax + 0x518]
0x4943FE: mov      edx, dword ptr [eax + 0x34]
0x494401: mov      eax, dword ptr [ebp - 4]
0x494404: mov      eax, dword ptr [eax + 0x518]
0x49440A: call     0x456fdc
0x49440F: add      eax, 0x16
0x494412: mov      dword ptr [ebp - 0x14], eax
0x494415: push     0x494ec0 ; "恭喜: 你已经到了下一个等级了!."
0x49441A: mov      ecx, dword ptr [ebp - 0x14]
0x49441D: mov      edx, dword ptr [ebp - 0x10]
0x494420: mov      eax, dword ptr [ebp - 0x38]
0x494423: call     0x4180dc
0x494428: push     0x494ee8 ; "选择你想提高的能力。"
0x49442D: mov      ecx, dword ptr [ebp - 0x14]
0x494430: add      ecx, 0xe
0x494433: mov      edx, dword ptr [ebp - 0x10]
0x494436: mov      eax, dword ptr [ebp - 0x38]
0x494439: call     0x4180dc
0x49443E: push     0x494f08 ; "这样的选择你只可以做一次。"
0x494443: mov      ecx, dword ptr [ebp - 0x14]
0x494446: add      ecx, 0x1c
0x494449: mov      edx, dword ptr [ebp - 0x10]
0x49444C: mov      eax, dword ptr [ebp - 0x38]
0x49444F: call     0x4180dc
0x494454: push     0x494f2c ; "最好能很小心的选择。."
0x494459: mov      ecx, dword ptr [ebp - 0x14]
0x49445C: add      ecx, 0x2a
0x49445F: mov      edx, dword ptr [ebp - 0x10]
0x494462: mov      eax, dword ptr [ebp - 0x38]
0x494465: call     0x4180dc
0x49446A: mov      edx, 0xffffff
0x49446F: mov      eax, dword ptr [ebp - 0x38]
0x494472: mov      eax, dword ptr [eax + 0xc]
0x494475: call     0x4173fc
0x49447A: mov      eax, dword ptr [ebp - 4]
0x49447D: mov      eax, dword ptr [eax + 0x518]
0x494483: mov      edx, dword ptr [eax + 0x30]
0x494486: mov      eax, dword ptr [ebp - 4]
0x494489: mov      eax, dword ptr [eax + 0x518]
0x49448F: call     0x456f94
0x494494: add      eax, 0x64
0x494497: mov      dword ptr [ebp - 0x10], eax
0x49449A: mov      eax, dword ptr [ebp - 4]
0x49449D: mov      eax, dword ptr [eax + 0x518]
0x4944A3: mov      edx, dword ptr [eax + 0x34]
0x4944A6: mov      eax, dword ptr [ebp - 4]
0x4944A9: mov      eax, dword ptr [eax + 0x518]
0x4944AF: call     0x456fdc
0x4944B4: add      eax, 0x65
0x4944B7: mov      dword ptr [ebp - 0x14], eax
0x4944BA: mov      eax, dword ptr [0x4a3f64] ; *0x4A3F64->0x4F9298
0x4944BF: movzx    eax, word ptr [eax]
0x4944C2: mov      edx, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x4944C8: movzx    edx, word ptr [edx]
0x4944CB: add      eax, edx
0x4944CD: mov      edx, dword ptr [0x4a4080] ; *0x4A4080->0x4F9284
0x4944D3: movzx    edx, word ptr [edx]
0x4944D6: mov      ecx, edx
0x4944D8: xor      edx, edx
0x4944DA: div      ecx
0x4944DC: mov      dword ptr [ebp - 0x18], eax
0x4944DF: mov      eax, dword ptr [0x4a3f64] ; *0x4A3F64->0x4F9298
0x4944E4: movzx    eax, word ptr [eax + 2]
0x4944E8: mov      edx, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x4944EE: movzx    edx, word ptr [edx + 2]
0x4944F2: add      eax, edx
0x4944F4: mov      edx, dword ptr [0x4a4080] ; *0x4A4080->0x4F9284
0x4944FA: movzx    edx, word ptr [edx + 2]
0x4944FE: mov      ecx, edx
0x494500: xor      edx, edx
0x494502: div      ecx
0x494504: mov      dword ptr [ebp - 0x1c], eax
0x494507: mov      eax, dword ptr [0x4a3f64] ; *0x4A3F64->0x4F9298
0x49450C: movzx    eax, word ptr [eax + 4]
0x494510: mov      edx, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x494516: movzx    edx, word ptr [edx + 4]
0x49451A: add      eax, edx
0x49451C: mov      edx, dword ptr [0x4a4080] ; *0x4A4080->0x4F9284
0x494522: movzx    edx, word ptr [edx + 4]
0x494526: mov      ecx, edx
0x494528: xor      edx, edx
0x49452A: div      ecx
0x49452C: mov      dword ptr [ebp - 0x20], eax
0x49452F: mov      eax, dword ptr [0x4a3f64] ; *0x4A3F64->0x4F9298
0x494534: movzx    eax, word ptr [eax + 6]
0x494538: mov      edx, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x49453E: movzx    edx, word ptr [edx + 6]
0x494542: add      eax, edx
0x494544: mov      edx, dword ptr [0x4a4080] ; *0x4A4080->0x4F9284
0x49454A: movzx    edx, word ptr [edx + 6]
0x49454E: mov      ecx, edx
0x494550: xor      edx, edx
0x494552: div      ecx
0x494554: mov      dword ptr [ebp - 0x24], eax
0x494557: mov      eax, dword ptr [0x4a3f64] ; *0x4A3F64->0x4F9298
0x49455C: movzx    eax, word ptr [eax + 8]
0x494560: mov      edx, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x494566: movzx    edx, word ptr [edx + 8]
0x49456A: add      eax, edx
0x49456C: mov      edx, dword ptr [0x4a4080] ; *0x4A4080->0x4F9284
0x494572: movzx    edx, word ptr [edx + 8]
0x494576: mov      ecx, edx
0x494578: xor      edx, edx
0x49457A: div      ecx
0x49457C: mov      dword ptr [ebp - 0x28], eax
0x49457F: push     ebp
0x494580: lea      eax, [ebp - 0x2e]
0x494583: push     eax
0x494584: lea      ecx, [ebp - 0x29]
0x494587: mov      eax, dword ptr [0x4a3ff4] ; *0x4A3FF4->0x4F92AC
0x49458C: mov      al, byte ptr [eax]
0x49458E: mov      dx, word ptr [ebp - 0x18]
0x494592: call     0x494294
0x494597: pop      ecx
0x494598: push     ebp
0x494599: lea      eax, [ebp - 0x2f]
0x49459C: push     eax
0x49459D: lea      ecx, [ebp - 0x2a]
0x4945A0: mov      eax, dword ptr [0x4a3ff4] ; *0x4A3FF4->0x4F92AC
0x4945A5: mov      al, byte ptr [eax + 2]
0x4945A8: mov      dx, word ptr [ebp - 0x1c]
0x4945AC: call     0x494294
0x4945B1: pop      ecx
0x4945B2: push     ebp
0x4945B3: lea      eax, [ebp - 0x30]
0x4945B6: push     eax
0x4945B7: lea      ecx, [ebp - 0x2b]
0x4945BA: mov      eax, dword ptr [0x4a3ff4] ; *0x4A3FF4->0x4F92AC
0x4945BF: mov      al, byte ptr [eax + 4]
0x4945C2: mov      dx, word ptr [ebp - 0x20]
0x4945C6: call     0x494294
0x4945CB: pop      ecx
0x4945CC: mov      byte ptr [ebp - 0x2c], 0
0x4945D0: mov      al, byte ptr [ebp - 0x24]
0x4945D3: mov      byte ptr [ebp - 0x31], al
0x4945D6: mov      byte ptr [ebp - 0x2d], 0
0x4945DA: mov      al, byte ptr [ebp - 0x28]
0x4945DD: mov      byte ptr [ebp - 0x32], al
0x4945E0: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4945E5: mov      eax, dword ptr [eax]
0x4945E7: movzx    eax, byte ptr [eax + 0x36]
0x4945EB: xor      edx, edx
0x4945ED: mov      dl, byte ptr [ebp - 0x29]
0x4945F0: add      eax, edx
0x4945F2: lea      edx, [ebp - 0x58]
0x4945F5: call     0x407d64
0x4945FA: push     dword ptr [ebp - 0x58]
0x4945FD: push     0x494f4c ; "-"
0x494602: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x494607: mov      eax, dword ptr [eax]
0x494609: mov      ax, word ptr [eax + 0x36]
0x49460D: call     0x406974
0x494612: and      eax, 0xff
0x494617: xor      edx, edx
0x494619: mov      dl, byte ptr [ebp - 0x2e]
0x49461C: add      eax, edx
0x49461E: lea      edx, [ebp - 0x5c]
0x494621: call     0x407d64
0x494626: push     dword ptr [ebp - 0x5c]
0x494629: lea      eax, [ebp - 0x54]
0x49462C: mov      edx, 3
0x494631: call     0x403d94
0x494636: mov      eax, dword ptr [ebp - 0x54]
0x494639: push     eax
0x49463A: mov      ecx, dword ptr [ebp - 0x14]
0x49463D: mov      edx, dword ptr [ebp - 0x10]
0x494640: mov      eax, dword ptr [ebp - 0x38]
0x494643: call     0x4180dc
0x494648: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49464D: mov      eax, dword ptr [eax]
0x49464F: movzx    eax, byte ptr [eax + 0x38]
0x494653: xor      edx, edx
0x494655: mov      dl, byte ptr [ebp - 0x2a]
0x494658: add      eax, edx
0x49465A: lea      edx, [ebp - 0x58]
0x49465D: call     0x407d64
0x494662: push     dword ptr [ebp - 0x58]
0x494665: push     0x494f4c ; "-"
0x49466A: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49466F: mov      eax, dword ptr [eax]
0x494671: mov      ax, word ptr [eax + 0x38]
0x494675: call     0x406974
0x49467A: and      eax, 0xff
0x49467F: xor      edx, edx
0x494681: mov      dl, byte ptr [ebp - 0x2f]
0x494684: add      eax, edx
0x494686: lea      edx, [ebp - 0x5c]
0x494689: call     0x407d64
0x49468E: push     dword ptr [ebp - 0x5c]
0x494691: lea      eax, [ebp - 0x54]
0x494694: mov      edx, 3
0x494699: call     0x403d94
0x49469E: mov      eax, dword ptr [ebp - 0x54]
0x4946A1: push     eax
0x4946A2: mov      ecx, dword ptr [ebp - 0x14]
0x4946A5: add      ecx, 0x14
0x4946A8: mov      edx, dword ptr [ebp - 0x10]
0x4946AB: mov      eax, dword ptr [ebp - 0x38]
0x4946AE: call     0x4180dc
0x4946B3: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4946B8: mov      eax, dword ptr [eax]
0x4946BA: movzx    eax, byte ptr [eax + 0x3a]
0x4946BE: xor      edx, edx
0x4946C0: mov      dl, byte ptr [ebp - 0x2b]
0x4946C3: add      eax, edx
0x4946C5: lea      edx, [ebp - 0x58]
0x4946C8: call     0x407d64
0x4946CD: push     dword ptr [ebp - 0x58]
0x4946D0: push     0x494f4c ; "-"
0x4946D5: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4946DA: mov      eax, dword ptr [eax]
0x4946DC: mov      ax, word ptr [eax + 0x3a]
0x4946E0: call     0x406974
0x4946E5: and      eax, 0xff
0x4946EA: xor      edx, edx
0x4946EC: mov      dl, byte ptr [ebp - 0x30]
0x4946EF: add      eax, edx
0x4946F1: lea      edx, [ebp - 0x5c]
0x4946F4: call     0x407d64
0x4946F9: push     dword ptr [ebp - 0x5c]
0x4946FC: lea      eax, [ebp - 0x54]
0x4946FF: mov      edx, 3
0x494704: call     0x403d94
0x494709: mov      eax, dword ptr [ebp - 0x54]
0x49470C: push     eax
0x49470D: mov      ecx, dword ptr [ebp - 0x14]
0x494710: add      ecx, 0x28
0x494713: mov      edx, dword ptr [ebp - 0x10]
0x494716: mov      eax, dword ptr [ebp - 0x38]
0x494719: call     0x4180dc
0x49471E: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x494723: mov      eax, dword ptr [eax]
0x494725: movzx    eax, byte ptr [eax + 0x32]
0x494729: xor      edx, edx
0x49472B: mov      dl, byte ptr [ebp - 0x2c]
0x49472E: add      eax, edx
0x494730: lea      edx, [ebp - 0x58]
0x494733: call     0x407d64
0x494738: push     dword ptr [ebp - 0x58]
0x49473B: push     0x494f4c ; "-"
0x494740: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x494745: mov      eax, dword ptr [eax]
0x494747: mov      ax, word ptr [eax + 0x32]
0x49474B: call     0x406974
0x494750: and      eax, 0xff
0x494755: xor      edx, edx
0x494757: mov      dl, byte ptr [ebp - 0x31]
0x49475A: add      eax, edx
0x49475C: lea      edx, [ebp - 0x5c]
0x49475F: call     0x407d64
0x494764: push     dword ptr [ebp - 0x5c]
0x494767: lea      eax, [ebp - 0x54]
0x49476A: mov      edx, 3
0x49476F: call     0x403d94
0x494774: mov      eax, dword ptr [ebp - 0x54]
0x494777: push     eax
0x494778: mov      ecx, dword ptr [ebp - 0x14]
0x49477B: add      ecx, 0x3c
0x49477E: mov      edx, dword ptr [ebp - 0x10]
0x494781: mov      eax, dword ptr [ebp - 0x38]
0x494784: call     0x4180dc
0x494789: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49478E: mov      eax, dword ptr [eax]
0x494790: movzx    eax, byte ptr [eax + 0x34]
0x494794: xor      edx, edx
0x494796: mov      dl, byte ptr [ebp - 0x2d]
0x494799: add      eax, edx
0x49479B: lea      edx, [ebp - 0x58]
0x49479E: call     0x407d64
0x4947A3: push     dword ptr [ebp - 0x58]
0x4947A6: push     0x494f4c ; "-"
0x4947AB: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x4947B0: mov      eax, dword ptr [eax]
0x4947B2: mov      ax, word ptr [eax + 0x34]
0x4947B6: call     0x406974
0x4947BB: and      eax, 0xff
0x4947C0: xor      edx, edx
0x4947C2: mov      dl, byte ptr [ebp - 0x32]
0x4947C5: add      eax, edx
0x4947C7: lea      edx, [ebp - 0x5c]
0x4947CA: call     0x407d64
0x4947CF: push     dword ptr [ebp - 0x5c]
0x4947D2: lea      eax, [ebp - 0x54]
0x4947D5: mov      edx, 3
0x4947DA: call     0x403d94
0x4947DF: mov      eax, dword ptr [ebp - 0x54]
0x4947E2: push     eax
0x4947E3: mov      ecx, dword ptr [ebp - 0x14]
0x4947E6: add      ecx, 0x50
0x4947E9: mov      edx, dword ptr [ebp - 0x10]
0x4947EC: mov      eax, dword ptr [ebp - 0x38]
0x4947EF: call     0x4180dc
0x4947F4: mov      eax, dword ptr [0x4a3f64] ; *0x4A3F64->0x4F9298
0x4947F9: movzx    eax, word ptr [eax + 0xa]
0x4947FD: mov      edx, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x494803: movzx    edx, word ptr [edx + 0xa]
0x494807: add      eax, edx
0x494809: mov      edx, dword ptr [0x4a4080] ; *0x4A4080->0x4F9284
0x49480F: movzx    edx, word ptr [edx + 0xa]
0x494813: mov      ecx, edx
0x494815: xor      edx, edx
0x494817: div      ecx
0x494819: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49481F: mov      edx, dword ptr [edx]
0x494821: movzx    edx, word ptr [edx + 0x40]
0x494825: add      eax, edx
0x494827: lea      edx, [ebp - 0x54]
0x49482A: call     0x407d64
0x49482F: mov      eax, dword ptr [ebp - 0x54]
0x494832: push     eax
0x494833: mov      ecx, dword ptr [ebp - 0x14]
0x494836: add      ecx, 0x64
0x494839: mov      edx, dword ptr [ebp - 0x10]
0x49483C: mov      eax, dword ptr [ebp - 0x38]
0x49483F: call     0x4180dc
0x494844: mov      eax, dword ptr [0x4a3f64] ; *0x4A3F64->0x4F9298
0x494849: movzx    eax, word ptr [eax + 0xc]
0x49484D: mov      edx, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x494853: movzx    edx, word ptr [edx + 0xc]
0x494857: add      eax, edx
0x494859: mov      edx, dword ptr [0x4a4080] ; *0x4A4080->0x4F9284
0x49485F: movzx    edx, word ptr [edx + 0xc]
0x494863: mov      ecx, edx
0x494865: xor      edx, edx
0x494867: div      ecx
0x494869: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49486F: mov      edx, dword ptr [edx]
0x494871: movzx    edx, word ptr [edx + 0x42]
0x494875: add      eax, edx
0x494877: lea      edx, [ebp - 0x54]
0x49487A: call     0x407d64
0x49487F: mov      eax, dword ptr [ebp - 0x54]
0x494882: push     eax
0x494883: mov      ecx, dword ptr [ebp - 0x14]
0x494886: add      ecx, 0x78
0x494889: mov      edx, dword ptr [ebp - 0x10]
0x49488C: mov      eax, dword ptr [ebp - 0x38]
0x49488F: call     0x4180dc
0x494894: mov      eax, dword ptr [0x4a3f64] ; *0x4A3F64->0x4F9298
0x494899: movzx    eax, word ptr [eax + 0xe]
0x49489D: mov      edx, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x4948A3: movzx    edx, word ptr [edx + 0xe]
0x4948A7: add      eax, edx
0x4948A9: mov      edx, dword ptr [0x4a4080] ; *0x4A4080->0x4F9284
0x4948AF: movzx    edx, word ptr [edx + 0xe]
0x4948B3: mov      ecx, edx
0x4948B5: xor      edx, edx
0x4948B7: div      ecx
0x4948B9: mov      edx, dword ptr [0x4a3f50] ; *0x4A3F50->0x4F9504
0x4948BF: add      eax, dword ptr [edx]
0x4948C1: lea      edx, [ebp - 0x54]
0x4948C4: call     0x407d64
0x4948C9: mov      eax, dword ptr [ebp - 0x54]
0x4948CC: push     eax
0x4948CD: mov      ecx, dword ptr [ebp - 0x14]
0x4948D0: add      ecx, 0x8c
0x4948D6: mov      edx, dword ptr [ebp - 0x10]
0x4948D9: mov      eax, dword ptr [ebp - 0x38]
0x4948DC: call     0x4180dc
0x4948E1: mov      eax, dword ptr [0x4a3f64] ; *0x4A3F64->0x4F9298
0x4948E6: movzx    eax, word ptr [eax + 0x10]
0x4948EA: mov      edx, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x4948F0: movzx    edx, word ptr [edx + 0x10]
0x4948F4: add      eax, edx
0x4948F6: mov      edx, dword ptr [0x4a4080] ; *0x4A4080->0x4F9284
0x4948FC: movzx    edx, word ptr [edx + 0x10]
0x494900: mov      ecx, edx
0x494902: xor      edx, edx
0x494904: div      ecx
0x494906: mov      edx, dword ptr [0x4a41d0] ; *0x4A41D0->0x4F9500
0x49490C: add      eax, dword ptr [edx]
0x49490E: lea      edx, [ebp - 0x54]
0x494911: call     0x407d64
0x494916: mov      eax, dword ptr [ebp - 0x54]
0x494919: push     eax
0x49491A: mov      ecx, dword ptr [ebp - 0x14]
0x49491D: add      ecx, 0xa0
0x494923: mov      edx, dword ptr [ebp - 0x10]
0x494926: mov      eax, dword ptr [ebp - 0x38]
0x494929: call     0x4180dc
0x49492E: mov      edx, 0xffff
0x494933: mov      eax, dword ptr [ebp - 0x38]
0x494936: mov      eax, dword ptr [eax + 0xc]
0x494939: call     0x4173fc
0x49493E: lea      edx, [ebp - 0x54]
0x494941: mov      eax, dword ptr [0x4a3fc0] ; *0x4A3FC0->0x4F927C
0x494946: mov      eax, dword ptr [eax]
0x494948: call     0x407d64
0x49494D: mov      eax, dword ptr [ebp - 0x54]
0x494950: push     eax
0x494951: mov      ecx, dword ptr [ebp - 0x14]
0x494954: add      ecx, 0xb4
0x49495A: mov      edx, dword ptr [ebp - 0x10]
0x49495D: mov      eax, dword ptr [ebp - 0x38]
0x494960: call     0x4180dc
0x494965: mov      edx, 0xffffff
0x49496A: mov      eax, dword ptr [ebp - 0x38]
0x49496D: mov      eax, dword ptr [eax + 0xc]
0x494970: call     0x4173fc
0x494975: mov      eax, dword ptr [ebp - 4]
0x494978: mov      eax, dword ptr [eax + 0x518]
0x49497E: mov      edx, dword ptr [eax + 0x30]
0x494981: mov      eax, dword ptr [ebp - 4]
0x494984: mov      eax, dword ptr [eax + 0x518]
0x49498A: call     0x456f94
0x49498F: add      eax, 0x9b
0x494994: mov      dword ptr [ebp - 0x10], eax
0x494997: mov      eax, dword ptr [ebp - 4]
0x49499A: mov      eax, dword ptr [eax + 0x518]
0x4949A0: mov      edx, dword ptr [eax + 0x34]
0x4949A3: mov      eax, dword ptr [ebp - 4]
0x4949A6: mov      eax, dword ptr [eax + 0x518]
0x4949AC: call     0x456fdc
0x4949B1: add      eax, 0x65
0x4949B4: mov      dword ptr [ebp - 0x14], eax
0x4949B7: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x4949BC: cmp      word ptr [eax], 0
0x4949C0: jbe      0x4949d4
0x4949C2: mov      edx, 0xffffff
0x4949C7: mov      eax, dword ptr [ebp - 0x38]
0x4949CA: mov      eax, dword ptr [eax + 0xc]
0x4949CD: call     0x4173fc
0x4949D2: jmp      0x4949e4
0x4949D4: mov      edx, 0xc0c0c0
0x4949D9: mov      eax, dword ptr [ebp - 0x38]
0x4949DC: mov      eax, dword ptr [eax + 0xc]
0x4949DF: call     0x4173fc
0x4949E4: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x4949E9: movzx    eax, word ptr [eax]
0x4949EC: mov      edx, dword ptr [0x4a3f64] ; *0x4A3F64->0x4F9298
0x4949F2: movzx    edx, word ptr [edx]
0x4949F5: add      eax, edx
0x4949F7: lea      edx, [ebp - 0x58]
0x4949FA: call     0x407d64
0x4949FF: push     dword ptr [ebp - 0x58]
0x494A02: push     0x494f58 ; "/"
0x494A07: lea      edx, [ebp - 0x5c]
0x494A0A: mov      eax, dword ptr [0x4a4080] ; *0x4A4080->0x4F9284
0x494A0F: movzx    eax, word ptr [eax]
0x494A12: call     0x407d64
0x494A17: push     dword ptr [ebp - 0x5c]
0x494A1A: lea      eax, [ebp - 0x54]
0x494A1D: mov      edx, 3
0x494A22: call     0x403d94
0x494A27: mov      eax, dword ptr [ebp - 0x54]
0x494A2A: push     eax
0x494A2B: mov      ecx, dword ptr [ebp - 0x14]
0x494A2E: mov      edx, dword ptr [ebp - 0x10]
0x494A31: mov      eax, dword ptr [ebp - 0x38]
0x494A34: call     0x4180dc
0x494A39: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x494A3E: cmp      word ptr [eax + 2], 0
0x494A43: jbe      0x494a57
0x494A45: mov      edx, 0xffffff
0x494A4A: mov      eax, dword ptr [ebp - 0x38]
0x494A4D: mov      eax, dword ptr [eax + 0xc]
0x494A50: call     0x4173fc
0x494A55: jmp      0x494a67
0x494A57: mov      edx, 0xc0c0c0
0x494A5C: mov      eax, dword ptr [ebp - 0x38]
0x494A5F: mov      eax, dword ptr [eax + 0xc]
0x494A62: call     0x4173fc
0x494A67: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x494A6C: movzx    eax, word ptr [eax + 2]
0x494A70: mov      edx, dword ptr [0x4a3f64] ; *0x4A3F64->0x4F9298
0x494A76: movzx    edx, word ptr [edx + 2]
0x494A7A: add      eax, edx
0x494A7C: lea      edx, [ebp - 0x58]
0x494A7F: call     0x407d64
0x494A84: push     dword ptr [ebp - 0x58]
0x494A87: push     0x494f58 ; "/"
0x494A8C: lea      edx, [ebp - 0x5c]
0x494A8F: mov      eax, dword ptr [0x4a4080] ; *0x4A4080->0x4F9284
0x494A94: movzx    eax, word ptr [eax + 2]
0x494A98: call     0x407d64
0x494A9D: push     dword ptr [ebp - 0x5c]
0x494AA0: lea      eax, [ebp - 0x54]
0x494AA3: mov      edx, 3
0x494AA8: call     0x403d94
0x494AAD: mov      eax, dword ptr [ebp - 0x54]
0x494AB0: push     eax
0x494AB1: mov      ecx, dword ptr [ebp - 0x14]
0x494AB4: add      ecx, 0x14
0x494AB7: mov      edx, dword ptr [ebp - 0x10]
0x494ABA: mov      eax, dword ptr [ebp - 0x38]
0x494ABD: call     0x4180dc
0x494AC2: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x494AC7: cmp      word ptr [eax + 4], 0
0x494ACC: jbe      0x494ae0
0x494ACE: mov      edx, 0xffffff
0x494AD3: mov      eax, dword ptr [ebp - 0x38]
0x494AD6: mov      eax, dword ptr [eax + 0xc]
0x494AD9: call     0x4173fc
0x494ADE: jmp      0x494af0
0x494AE0: mov      edx, 0xc0c0c0
0x494AE5: mov      eax, dword ptr [ebp - 0x38]
0x494AE8: mov      eax, dword ptr [eax + 0xc]
0x494AEB: call     0x4173fc
0x494AF0: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x494AF5: movzx    eax, word ptr [eax + 4]
0x494AF9: mov      edx, dword ptr [0x4a3f64] ; *0x4A3F64->0x4F9298
0x494AFF: movzx    edx, word ptr [edx + 4]
0x494B03: add      eax, edx
0x494B05: lea      edx, [ebp - 0x58]
0x494B08: call     0x407d64
0x494B0D: push     dword ptr [ebp - 0x58]
0x494B10: push     0x494f58 ; "/"
0x494B15: lea      edx, [ebp - 0x5c]
0x494B18: mov      eax, dword ptr [0x4a4080] ; *0x4A4080->0x4F9284
0x494B1D: movzx    eax, word ptr [eax + 4]
0x494B21: call     0x407d64
0x494B26: push     dword ptr [ebp - 0x5c]
0x494B29: lea      eax, [ebp - 0x54]
0x494B2C: mov      edx, 3
0x494B31: call     0x403d94
0x494B36: mov      eax, dword ptr [ebp - 0x54]
0x494B39: push     eax
0x494B3A: mov      ecx, dword ptr [ebp - 0x14]
0x494B3D: add      ecx, 0x28
0x494B40: mov      edx, dword ptr [ebp - 0x10]
0x494B43: mov      eax, dword ptr [ebp - 0x38]
0x494B46: call     0x4180dc
0x494B4B: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x494B50: cmp      word ptr [eax + 6], 0
0x494B55: jbe      0x494b69
0x494B57: mov      edx, 0xffffff
0x494B5C: mov      eax, dword ptr [ebp - 0x38]
0x494B5F: mov      eax, dword ptr [eax + 0xc]
0x494B62: call     0x4173fc
0x494B67: jmp      0x494b79
0x494B69: mov      edx, 0xc0c0c0
0x494B6E: mov      eax, dword ptr [ebp - 0x38]
0x494B71: mov      eax, dword ptr [eax + 0xc]
0x494B74: call     0x4173fc
0x494B79: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x494B7E: movzx    eax, word ptr [eax + 6]
0x494B82: mov      edx, dword ptr [0x4a3f64] ; *0x4A3F64->0x4F9298
0x494B88: movzx    edx, word ptr [edx + 6]
0x494B8C: add      eax, edx
0x494B8E: lea      edx, [ebp - 0x58]
0x494B91: call     0x407d64
0x494B96: push     dword ptr [ebp - 0x58]
0x494B99: push     0x494f58 ; "/"
0x494B9E: lea      edx, [ebp - 0x5c]
0x494BA1: mov      eax, dword ptr [0x4a4080] ; *0x4A4080->0x4F9284
0x494BA6: movzx    eax, word ptr [eax + 6]
0x494BAA: call     0x407d64
0x494BAF: push     dword ptr [ebp - 0x5c]
0x494BB2: lea      eax, [ebp - 0x54]
0x494BB5: mov      edx, 3
0x494BBA: call     0x403d94
0x494BBF: mov      eax, dword ptr [ebp - 0x54]
0x494BC2: push     eax
0x494BC3: mov      ecx, dword ptr [ebp - 0x14]
0x494BC6: add      ecx, 0x3c
0x494BC9: mov      edx, dword ptr [ebp - 0x10]
0x494BCC: mov      eax, dword ptr [ebp - 0x38]
0x494BCF: call     0x4180dc
0x494BD4: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x494BD9: cmp      word ptr [eax + 8], 0
0x494BDE: jbe      0x494bf2
0x494BE0: mov      edx, 0xffffff
0x494BE5: mov      eax, dword ptr [ebp - 0x38]
0x494BE8: mov      eax, dword ptr [eax + 0xc]
0x494BEB: call     0x4173fc
0x494BF0: jmp      0x494c02
0x494BF2: mov      edx, 0xc0c0c0
0x494BF7: mov      eax, dword ptr [ebp - 0x38]
0x494BFA: mov      eax, dword ptr [eax + 0xc]
0x494BFD: call     0x4173fc
0x494C02: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x494C07: movzx    eax, word ptr [eax + 8]
0x494C0B: mov      edx, dword ptr [0x4a3f64] ; *0x4A3F64->0x4F9298
0x494C11: movzx    edx, word ptr [edx + 8]
0x494C15: add      eax, edx
0x494C17: lea      edx, [ebp - 0x58]
0x494C1A: call     0x407d64
0x494C1F: push     dword ptr [ebp - 0x58]
0x494C22: push     0x494f58 ; "/"
0x494C27: lea      edx, [ebp - 0x5c]
0x494C2A: mov      eax, dword ptr [0x4a4080] ; *0x4A4080->0x4F9284
0x494C2F: movzx    eax, word ptr [eax + 8]
0x494C33: call     0x407d64
0x494C38: push     dword ptr [ebp - 0x5c]
0x494C3B: lea      eax, [ebp - 0x54]
0x494C3E: mov      edx, 3
0x494C43: call     0x403d94
0x494C48: mov      eax, dword ptr [ebp - 0x54]
0x494C4B: push     eax
0x494C4C: mov      ecx, dword ptr [ebp - 0x14]
0x494C4F: add      ecx, 0x50
0x494C52: mov      edx, dword ptr [ebp - 0x10]
0x494C55: mov      eax, dword ptr [ebp - 0x38]
0x494C58: call     0x4180dc
0x494C5D: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x494C62: cmp      word ptr [eax + 0xa], 0
0x494C67: jbe      0x494c7b
0x494C69: mov      edx, 0xffffff
0x494C6E: mov      eax, dword ptr [ebp - 0x38]
0x494C71: mov      eax, dword ptr [eax + 0xc]
0x494C74: call     0x4173fc
0x494C79: jmp      0x494c8b
0x494C7B: mov      edx, 0xc0c0c0
0x494C80: mov      eax, dword ptr [ebp - 0x38]
0x494C83: mov      eax, dword ptr [eax + 0xc]
0x494C86: call     0x4173fc
0x494C8B: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x494C90: movzx    eax, word ptr [eax + 0xa]
0x494C94: mov      edx, dword ptr [0x4a3f64] ; *0x4A3F64->0x4F9298
0x494C9A: movzx    edx, word ptr [edx + 0xa]
0x494C9E: add      eax, edx
0x494CA0: lea      edx, [ebp - 0x58]
0x494CA3: call     0x407d64
0x494CA8: push     dword ptr [ebp - 0x58]
0x494CAB: push     0x494f58 ; "/"
0x494CB0: lea      edx, [ebp - 0x5c]
0x494CB3: mov      eax, dword ptr [0x4a4080] ; *0x4A4080->0x4F9284
0x494CB8: movzx    eax, word ptr [eax + 0xa]
0x494CBC: call     0x407d64
0x494CC1: push     dword ptr [ebp - 0x5c]
0x494CC4: lea      eax, [ebp - 0x54]
0x494CC7: mov      edx, 3
0x494CCC: call     0x403d94
0x494CD1: mov      eax, dword ptr [ebp - 0x54]
0x494CD4: push     eax
0x494CD5: mov      ecx, dword ptr [ebp - 0x14]
0x494CD8: add      ecx, 0x64
0x494CDB: mov      edx, dword ptr [ebp - 0x10]
0x494CDE: mov      eax, dword ptr [ebp - 0x38]
0x494CE1: call     0x4180dc
0x494CE6: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x494CEB: cmp      word ptr [eax + 0xc], 0
0x494CF0: jbe      0x494d04
0x494CF2: mov      edx, 0xffffff
0x494CF7: mov      eax, dword ptr [ebp - 0x38]
0x494CFA: mov      eax, dword ptr [eax + 0xc]
0x494CFD: call     0x4173fc
0x494D02: jmp      0x494d14
0x494D04: mov      edx, 0xc0c0c0
0x494D09: mov      eax, dword ptr [ebp - 0x38]
0x494D0C: mov      eax, dword ptr [eax + 0xc]
0x494D0F: call     0x4173fc
0x494D14: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x494D19: movzx    eax, word ptr [eax + 0xc]
0x494D1D: mov      edx, dword ptr [0x4a3f64] ; *0x4A3F64->0x4F9298
0x494D23: movzx    edx, word ptr [edx + 0xc]
0x494D27: add      eax, edx
0x494D29: lea      edx, [ebp - 0x58]
0x494D2C: call     0x407d64
0x494D31: push     dword ptr [ebp - 0x58]
0x494D34: push     0x494f58 ; "/"
0x494D39: lea      edx, [ebp - 0x5c]
0x494D3C: mov      eax, dword ptr [0x4a4080] ; *0x4A4080->0x4F9284
0x494D41: movzx    eax, word ptr [eax + 0xc]
0x494D45: call     0x407d64
0x494D4A: push     dword ptr [ebp - 0x5c]
0x494D4D: lea      eax, [ebp - 0x54]
0x494D50: mov      edx, 3
0x494D55: call     0x403d94
0x494D5A: mov      eax, dword ptr [ebp - 0x54]
0x494D5D: push     eax
0x494D5E: mov      ecx, dword ptr [ebp - 0x14]
0x494D61: add      ecx, 0x78
0x494D64: mov      edx, dword ptr [ebp - 0x10]
0x494D67: mov      eax, dword ptr [ebp - 0x38]
0x494D6A: call     0x4180dc
0x494D6F: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x494D74: cmp      word ptr [eax + 0xe], 0
0x494D79: jbe      0x494d8d
0x494D7B: mov      edx, 0xffffff
0x494D80: mov      eax, dword ptr [ebp - 0x38]
0x494D83: mov      eax, dword ptr [eax + 0xc]
0x494D86: call     0x4173fc
0x494D8B: jmp      0x494d9d
0x494D8D: mov      edx, 0xc0c0c0
0x494D92: mov      eax, dword ptr [ebp - 0x38]
0x494D95: mov      eax, dword ptr [eax + 0xc]
0x494D98: call     0x4173fc
0x494D9D: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x494DA2: movzx    eax, word ptr [eax + 0xe]
0x494DA6: mov      edx, dword ptr [0x4a3f64] ; *0x4A3F64->0x4F9298
0x494DAC: movzx    edx, word ptr [edx + 0xe]
0x494DB0: add      eax, edx
0x494DB2: lea      edx, [ebp - 0x58]
0x494DB5: call     0x407d64
0x494DBA: push     dword ptr [ebp - 0x58]
0x494DBD: push     0x494f58 ; "/"
0x494DC2: lea      edx, [ebp - 0x5c]
0x494DC5: mov      eax, dword ptr [0x4a4080] ; *0x4A4080->0x4F9284
0x494DCA: movzx    eax, word ptr [eax + 0xe]
0x494DCE: call     0x407d64
0x494DD3: push     dword ptr [ebp - 0x5c]
0x494DD6: lea      eax, [ebp - 0x54]
0x494DD9: mov      edx, 3
0x494DDE: call     0x403d94
0x494DE3: mov      eax, dword ptr [ebp - 0x54]
0x494DE6: push     eax
0x494DE7: mov      ecx, dword ptr [ebp - 0x14]
0x494DEA: add      ecx, 0x8c
0x494DF0: mov      edx, dword ptr [ebp - 0x10]
0x494DF3: mov      eax, dword ptr [ebp - 0x38]
0x494DF6: call     0x4180dc
0x494DFB: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x494E00: cmp      word ptr [eax + 0x10], 0
0x494E05: jbe      0x494e19
0x494E07: mov      edx, 0xffffff
0x494E0C: mov      eax, dword ptr [ebp - 0x38]
0x494E0F: mov      eax, dword ptr [eax + 0xc]
0x494E12: call     0x4173fc
0x494E17: jmp      0x494e29
0x494E19: mov      edx, 0xc0c0c0
0x494E1E: mov      eax, dword ptr [ebp - 0x38]
0x494E21: mov      eax, dword ptr [eax + 0xc]
0x494E24: call     0x4173fc
0x494E29: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x494E2E: movzx    eax, word ptr [eax + 0x10]
0x494E32: mov      edx, dword ptr [0x4a3f64] ; *0x4A3F64->0x4F9298
0x494E38: movzx    edx, word ptr [edx + 0x10]
0x494E3C: add      eax, edx
0x494E3E: lea      edx, [ebp - 0x58]
0x494E41: call     0x407d64
0x494E46: push     dword ptr [ebp - 0x58]
0x494E49: push     0x494f58 ; "/"
0x494E4E: lea      edx, [ebp - 0x5c]
0x494E51: mov      eax, dword ptr [0x4a4080] ; *0x4A4080->0x4F9284
0x494E56: movzx    eax, word ptr [eax + 0x10]
0x494E5A: call     0x407d64
0x494E5F: push     dword ptr [ebp - 0x5c]
0x494E62: lea      eax, [ebp - 0x54]
0x494E65: mov      edx, 3
0x494E6A: call     0x403d94
0x494E6F: mov      eax, dword ptr [ebp - 0x54]
0x494E72: push     eax
0x494E73: mov      ecx, dword ptr [ebp - 0x14]
0x494E76: add      ecx, 0xa0
0x494E7C: mov      edx, dword ptr [ebp - 0x10]
0x494E7F: mov      eax, dword ptr [ebp - 0x38]
0x494E82: call     0x4180dc
0x494E87: mov      eax, dword ptr [ebp - 0x38]
0x494E8A: call     0x44d104
0x494E8F: xor      eax, eax
0x494E91: pop      edx
0x494E92: pop      ecx
0x494E93: pop      ecx
0x494E94: mov      dword ptr fs:[eax], edx
0x494E97: push     0x494eb1
0x494E9C: lea      eax, [ebp - 0x5c]
0x494E9F: mov      edx, 3
0x494EA4: call     0x403a7c
0x494EA9: ret      
0x494EAA: jmp      0x403518
0x494EAF: jmp      0x494e9c
0x494EB1: pop      ebx
0x494EB2: mov      esp, ebp
0x494EB4: pop      ebp
0x494EB5: ret      
0x494EB6: add      byte ptr [eax], al
0x494EC0: [STR len=30] "恭喜: 你已经到了下一个等级了!."
0x494EE8: [STR len=20] "选择你想提高的能力。"
0x494F08: [STR len=26] "这样的选择你只可以做一次。"
0x494F2C: [STR len=21] "最好能很小心的选择。."
0x494F4C: [STR len=1] "-"
0x494F58: [STR len=1] "/"
0x494F5C: push     ebp
0x494F5D: mov      ebp, esp
0x494F5F: add      esp, -0x10
0x494F62: mov      dword ptr [ebp - 0x10], ecx
0x494F65: mov      dword ptr [ebp - 8], edx
0x494F68: mov      dword ptr [ebp - 4], eax
0x494F6B: mov      eax, dword ptr [0x4a3fc0] ; *0x4A3FC0->0x4F927C
0x494F70: cmp      dword ptr [eax], 0
0x494F73: jle      0x49509a
0x494F79: mov      al, 0x11
0x494F7B: call     0x4a0b40
0x494F80: test     al, al
0x494F82: je       0x494f97
0x494F84: mov      eax, dword ptr [0x4a3fc0] ; *0x4A3FC0->0x4F927C
0x494F89: cmp      dword ptr [eax], 0xa
0x494F8C: jle      0x494f97
0x494F8E: mov      dword ptr [ebp - 0xc], 0xa
0x494F95: jmp      0x494f9e
0x494F97: mov      dword ptr [ebp - 0xc], 1
0x494F9E: mov      eax, dword ptr [0x4a3fc0] ; *0x4A3FC0->0x4F927C
0x494FA3: mov      edx, dword ptr [ebp - 0xc]
0x494FA6: sub      dword ptr [eax], edx
0x494FA8: mov      eax, dword ptr [ebp - 8]
0x494FAB: mov      edx, dword ptr [ebp - 4]
0x494FAE: cmp      eax, dword ptr [edx + 0x51c]
0x494FB4: jne      0x494fc2
0x494FB6: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x494FBB: mov      dx, word ptr [ebp - 0xc]
0x494FBF: add      word ptr [eax], dx
0x494FC2: mov      eax, dword ptr [ebp - 8]
0x494FC5: mov      edx, dword ptr [ebp - 4]
0x494FC8: cmp      eax, dword ptr [edx + 0x520]
0x494FCE: jne      0x494fdd
0x494FD0: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x494FD5: mov      dx, word ptr [ebp - 0xc]
0x494FD9: add      word ptr [eax + 2], dx
0x494FDD: mov      eax, dword ptr [ebp - 8]
0x494FE0: mov      edx, dword ptr [ebp - 4]
0x494FE3: cmp      eax, dword ptr [edx + 0x524]
0x494FE9: jne      0x494ff8
0x494FEB: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x494FF0: mov      dx, word ptr [ebp - 0xc]
0x494FF4: add      word ptr [eax + 4], dx
0x494FF8: mov      eax, dword ptr [ebp - 8]
0x494FFB: mov      edx, dword ptr [ebp - 4]
0x494FFE: cmp      eax, dword ptr [edx + 0x528]
0x495004: jne      0x495013
0x495006: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x49500B: mov      dx, word ptr [ebp - 0xc]
0x49500F: add      word ptr [eax + 6], dx
0x495013: mov      eax, dword ptr [ebp - 8]
0x495016: mov      edx, dword ptr [ebp - 4]
0x495019: cmp      eax, dword ptr [edx + 0x52c]
0x49501F: jne      0x49502e
0x495021: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x495026: mov      dx, word ptr [ebp - 0xc]
0x49502A: add      word ptr [eax + 8], dx
0x49502E: mov      eax, dword ptr [ebp - 8]
0x495031: mov      edx, dword ptr [ebp - 4]
0x495034: cmp      eax, dword ptr [edx + 0x530]
0x49503A: jne      0x495049
0x49503C: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x495041: mov      dx, word ptr [ebp - 0xc]
0x495045: add      word ptr [eax + 0xa], dx
0x495049: mov      eax, dword ptr [ebp - 8]
0x49504C: mov      edx, dword ptr [ebp - 4]
0x49504F: cmp      eax, dword ptr [edx + 0x534]
0x495055: jne      0x495064
0x495057: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x49505C: mov      dx, word ptr [ebp - 0xc]
0x495060: add      word ptr [eax + 0xc], dx
0x495064: mov      eax, dword ptr [ebp - 8]
0x495067: mov      edx, dword ptr [ebp - 4]
0x49506A: cmp      eax, dword ptr [edx + 0x538]
0x495070: jne      0x49507f
0x495072: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x495077: mov      dx, word ptr [ebp - 0xc]
0x49507B: add      word ptr [eax + 0xe], dx
0x49507F: mov      eax, dword ptr [ebp - 8]
0x495082: mov      edx, dword ptr [ebp - 4]
0x495085: cmp      eax, dword ptr [edx + 0x53c]
0x49508B: jne      0x49509a
0x49508D: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x495092: mov      dx, word ptr [ebp - 0xc]
0x495096: add      word ptr [eax + 0x10], dx
0x49509A: mov      esp, ebp
0x49509C: pop      ebp
0x49509D: ret      4
0x4950A0: push     ebp
0x4950A1: mov      ebp, esp
0x4950A3: add      esp, -0x10
0x4950A6: mov      dword ptr [ebp - 0x10], ecx
0x4950A9: mov      dword ptr [ebp - 8], edx
0x4950AC: mov      dword ptr [ebp - 4], eax
0x4950AF: mov      al, 0x11
0x4950B1: call     0x4a0b40
0x4950B6: test     al, al
0x4950B8: je       0x4950d1
0x4950BA: mov      eax, dword ptr [0x4a3fc0] ; *0x4A3FC0->0x4F927C
0x4950BF: mov      eax, dword ptr [eax]
0x4950C1: sub      eax, 0xa
0x4950C4: test     eax, eax
0x4950C6: jle      0x4950d1
0x4950C8: mov      dword ptr [ebp - 0xc], 0xa
0x4950CF: jmp      0x4950d8
0x4950D1: mov      dword ptr [ebp - 0xc], 1
0x4950D8: mov      eax, dword ptr [ebp - 8]
0x4950DB: mov      edx, dword ptr [ebp - 4]
0x4950DE: cmp      eax, dword ptr [edx + 0x540]
0x4950E4: jne      0x495109
0x4950E6: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x4950EB: movzx    eax, word ptr [eax]
0x4950EE: cmp      eax, dword ptr [ebp - 0xc]
0x4950F1: jl       0x495109
0x4950F3: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x4950F8: mov      dx, word ptr [ebp - 0xc]
0x4950FC: sub      word ptr [eax], dx
0x4950FF: mov      eax, dword ptr [0x4a3fc0] ; *0x4A3FC0->0x4F927C
0x495104: mov      edx, dword ptr [ebp - 0xc]
0x495107: add      dword ptr [eax], edx
0x495109: mov      eax, dword ptr [ebp - 8]
0x49510C: mov      edx, dword ptr [ebp - 4]
0x49510F: cmp      eax, dword ptr [edx + 0x544]
0x495115: jne      0x49513c
0x495117: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x49511C: movzx    eax, word ptr [eax + 2]
0x495120: cmp      eax, dword ptr [ebp - 0xc]
0x495123: jl       0x49513c
0x495125: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x49512A: mov      dx, word ptr [ebp - 0xc]
0x49512E: sub      word ptr [eax + 2], dx
0x495132: mov      eax, dword ptr [0x4a3fc0] ; *0x4A3FC0->0x4F927C
0x495137: mov      edx, dword ptr [ebp - 0xc]
0x49513A: add      dword ptr [eax], edx
0x49513C: mov      eax, dword ptr [ebp - 8]
0x49513F: mov      edx, dword ptr [ebp - 4]
0x495142: cmp      eax, dword ptr [edx + 0x548]
0x495148: jne      0x49516f
0x49514A: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x49514F: movzx    eax, word ptr [eax + 4]
0x495153: cmp      eax, dword ptr [ebp - 0xc]
0x495156: jl       0x49516f
0x495158: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x49515D: mov      dx, word ptr [ebp - 0xc]
0x495161: sub      word ptr [eax + 4], dx
0x495165: mov      eax, dword ptr [0x4a3fc0] ; *0x4A3FC0->0x4F927C
0x49516A: mov      edx, dword ptr [ebp - 0xc]
0x49516D: add      dword ptr [eax], edx
0x49516F: mov      eax, dword ptr [ebp - 8]
0x495172: mov      edx, dword ptr [ebp - 4]
0x495175: cmp      eax, dword ptr [edx + 0x54c]
0x49517B: jne      0x4951a2
0x49517D: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x495182: movzx    eax, word ptr [eax + 6]
0x495186: cmp      eax, dword ptr [ebp - 0xc]
0x495189: jl       0x4951a2
0x49518B: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x495190: mov      dx, word ptr [ebp - 0xc]
0x495194: sub      word ptr [eax + 6], dx
0x495198: mov      eax, dword ptr [0x4a3fc0] ; *0x4A3FC0->0x4F927C
0x49519D: mov      edx, dword ptr [ebp - 0xc]
0x4951A0: add      dword ptr [eax], edx
0x4951A2: mov      eax, dword ptr [ebp - 8]
0x4951A5: mov      edx, dword ptr [ebp - 4]
0x4951A8: cmp      eax, dword ptr [edx + 0x550]
0x4951AE: jne      0x4951d5
0x4951B0: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x4951B5: movzx    eax, word ptr [eax + 8]
0x4951B9: cmp      eax, dword ptr [ebp - 0xc]
0x4951BC: jl       0x4951d5
0x4951BE: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x4951C3: mov      dx, word ptr [ebp - 0xc]
0x4951C7: sub      word ptr [eax + 8], dx
0x4951CB: mov      eax, dword ptr [0x4a3fc0] ; *0x4A3FC0->0x4F927C
0x4951D0: mov      edx, dword ptr [ebp - 0xc]
0x4951D3: add      dword ptr [eax], edx
0x4951D5: mov      eax, dword ptr [ebp - 8]
0x4951D8: mov      edx, dword ptr [ebp - 4]
0x4951DB: cmp      eax, dword ptr [edx + 0x558]
0x4951E1: jne      0x495208
0x4951E3: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x4951E8: movzx    eax, word ptr [eax + 0xa]
0x4951EC: cmp      eax, dword ptr [ebp - 0xc]
0x4951EF: jl       0x495208
0x4951F1: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x4951F6: mov      dx, word ptr [ebp - 0xc]
0x4951FA: sub      word ptr [eax + 0xa], dx
0x4951FE: mov      eax, dword ptr [0x4a3fc0] ; *0x4A3FC0->0x4F927C
0x495203: mov      edx, dword ptr [ebp - 0xc]
0x495206: add      dword ptr [eax], edx
0x495208: mov      eax, dword ptr [ebp - 8]
0x49520B: mov      edx, dword ptr [ebp - 4]
0x49520E: cmp      eax, dword ptr [edx + 0x554]
0x495214: jne      0x49523b
0x495216: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x49521B: movzx    eax, word ptr [eax + 0xc]
0x49521F: cmp      eax, dword ptr [ebp - 0xc]
0x495222: jl       0x49523b
0x495224: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x495229: mov      dx, word ptr [ebp - 0xc]
0x49522D: sub      word ptr [eax + 0xc], dx
0x495231: mov      eax, dword ptr [0x4a3fc0] ; *0x4A3FC0->0x4F927C
0x495236: mov      edx, dword ptr [ebp - 0xc]
0x495239: add      dword ptr [eax], edx
0x49523B: mov      eax, dword ptr [ebp - 8]
0x49523E: mov      edx, dword ptr [ebp - 4]
0x495241: cmp      eax, dword ptr [edx + 0x55c]
0x495247: jne      0x49526e
0x495249: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x49524E: movzx    eax, word ptr [eax + 0xe]
0x495252: cmp      eax, dword ptr [ebp - 0xc]
0x495255: jl       0x49526e
0x495257: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x49525C: mov      dx, word ptr [ebp - 0xc]
0x495260: sub      word ptr [eax + 0xe], dx
0x495264: mov      eax, dword ptr [0x4a3fc0] ; *0x4A3FC0->0x4F927C
0x495269: mov      edx, dword ptr [ebp - 0xc]
0x49526C: add      dword ptr [eax], edx
0x49526E: mov      eax, dword ptr [ebp - 8]
0x495271: mov      edx, dword ptr [ebp - 4]
0x495274: cmp      eax, dword ptr [edx + 0x560]
0x49527A: jne      0x4952a1
0x49527C: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x495281: movzx    eax, word ptr [eax + 0x10]
0x495285: cmp      eax, dword ptr [ebp - 0xc]
0x495288: jl       0x4952a1
0x49528A: mov      eax, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x49528F: mov      dx, word ptr [ebp - 0xc]
0x495293: sub      word ptr [eax + 0x10], dx
0x495297: mov      eax, dword ptr [0x4a3fc0] ; *0x4A3FC0->0x4F927C
0x49529C: mov      edx, dword ptr [ebp - 0xc]
0x49529F: add      dword ptr [eax], edx
0x4952A1: mov      esp, ebp
0x4952A3: pop      ebp
0x4952A4: ret      4
0x4952A7: nop      
0x4952A8: push     ebp
0x4952A9: mov      ebp, esp
0x4952AB: add      esp, -0xc
0x4952AE: mov      dword ptr [ebp - 0xc], ecx
0x4952B1: mov      dword ptr [ebp - 8], edx
0x4952B4: mov      dword ptr [ebp - 4], eax
0x4952B7: mov      ecx, dword ptr [0x4a4324] ; *0x4A4324->0x4F92C0
0x4952BD: mov      edx, dword ptr [0x4a3fc0] ; *0x4A3FC0->0x4F927C
0x4952C3: mov      edx, dword ptr [edx]
0x4952C5: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4952CA: mov      eax, dword ptr [eax]
0x4952CC: call     0x466e78
0x4952D1: xor      edx, edx
0x4952D3: mov      eax, dword ptr [ebp - 4]
0x4952D6: mov      eax, dword ptr [eax + 0x518]
0x4952DC: call     0x458214
0x4952E1: mov      esp, ebp
0x4952E3: pop      ebp
0x4952E4: ret      4
0x4952E7: nop      
0x4952E8: push     ebp
0x4952E9: mov      ebp, esp
0x4952EB: add      esp, -0x20
0x4952EE: mov      byte ptr [ebp - 0x1d], cl
0x4952F1: mov      dword ptr [ebp - 0x1c], edx
0x4952F4: mov      dword ptr [ebp - 4], eax
0x4952F7: mov      eax, dword ptr [ebp - 4]
0x4952FA: mov      eax, dword ptr [eax + 0x518]
0x495300: mov      dword ptr [ebp - 0x18], eax
0x495303: mov      edx, dword ptr [ebp + 0xc]
0x495306: mov      eax, dword ptr [ebp - 0x18]
0x495309: sub      edx, dword ptr [eax + 0x30]
0x49530C: mov      eax, dword ptr [ebp - 0x18]
0x49530F: call     0x457024
0x495314: mov      dword ptr [ebp - 0xc], eax
0x495317: mov      edx, dword ptr [ebp + 8]
0x49531A: mov      eax, dword ptr [ebp - 0x18]
0x49531D: sub      edx, dword ptr [eax + 0x34]
0x495320: mov      eax, dword ptr [ebp - 0x18]
0x495323: call     0x45706c
0x495328: mov      dword ptr [ebp - 0x10], eax
0x49532B: mov      byte ptr [ebp - 0x11], 0
0x49532F: cmp      dword ptr [ebp - 0xc], 0x32
0x495333: jl       0x4953c7
0x495339: cmp      dword ptr [ebp - 0xc], 0x96
0x495340: jge      0x4953c7
0x495346: xor      eax, eax
0x495348: mov      dword ptr [ebp - 8], eax
0x49534B: mov      eax, dword ptr [ebp - 8]
0x49534E: shl      eax, 2
0x495351: lea      eax, [eax + eax*4]
0x495354: add      eax, 0x62
0x495357: cmp      eax, dword ptr [ebp - 0x10]
0x49535A: jg       0x4953be
0x49535C: mov      eax, dword ptr [ebp - 8]
0x49535F: inc      eax
0x495360: shl      eax, 2
0x495363: lea      eax, [eax + eax*4]
0x495366: add      eax, 0x62
0x495369: cmp      eax, dword ptr [ebp - 0x10]
0x49536C: jle      0x4953be
0x49536E: mov      eax, dword ptr [ebp - 8]
0x495371: mov      eax, dword ptr [eax*4 + 0x4a3034] ; *0x4A3034->0x47FDB8
0x495378: push     eax
0x495379: push     0xffffff
0x49537E: push     0
0x495380: mov      eax, dword ptr [ebp - 0x18]
0x495383: mov      edx, dword ptr [eax + 0x34]
0x495386: mov      eax, dword ptr [ebp - 0x18]
0x495389: call     0x456fdc
0x49538E: add      eax, dword ptr [ebp - 0x10]
0x495391: add      eax, 5
0x495394: push     eax
0x495395: mov      eax, dword ptr [ebp - 0x18]
0x495398: mov      edx, dword ptr [eax + 0x30]
0x49539B: mov      eax, dword ptr [ebp - 0x18]
0x49539E: call     0x456f94
0x4953A3: mov      edx, eax
0x4953A5: add      edx, dword ptr [ebp - 0xc]
0x4953A8: add      edx, 0xa
0x4953AB: mov      eax, dword ptr [0x4a3fb8] ; *0x4A3FB8->0x4F7D8C
0x4953B0: mov      eax, dword ptr [eax]
0x4953B2: pop      ecx
0x4953B3: call     0x479d34
0x4953B8: mov      byte ptr [ebp - 0x11], 1
0x4953BC: jmp      0x4953c7
0x4953BE: inc      dword ptr [ebp - 8]
0x4953C1: cmp      dword ptr [ebp - 8], 9
0x4953C5: jne      0x49534b
0x4953C7: cmp      byte ptr [ebp - 0x11], 0
0x4953CB: jne      0x4953d9
0x4953CD: mov      eax, dword ptr [0x4a3fb8] ; *0x4A3FB8->0x4F7D8C
0x4953D2: mov      eax, dword ptr [eax]
0x4953D4: call     0x479ebc
0x4953D9: mov      esp, ebp
0x4953DB: pop      ebp
0x4953DC: ret      8
0x4953DF: nop      
0x4953E0: push     ebp
0x4953E1: mov      ebp, esp
0x4953E3: add      esp, -0x2c
0x4953E6: push     ebx
0x4953E7: mov      dword ptr [ebp - 0xc], ecx
0x4953EA: mov      dword ptr [ebp - 8], edx
0x4953ED: mov      dword ptr [ebp - 4], eax
0x4953F0: mov      eax, dword ptr [ebp - 8]
0x4953F3: mov      edx, dword ptr [0x456468]
0x4953F9: call     0x403034
0x4953FE: mov      dword ptr [ebp - 0x1c], eax
0x495401: mov      eax, dword ptr [ebp - 0x1c]
0x495404: cmp      byte ptr [eax + 0x274], 0
0x49540B: jne      0x49542e
0x49540D: mov      eax, dword ptr [ebp - 0x1c]
0x495410: mov      edx, dword ptr [eax + 0x258]
0x495416: mov      eax, dword ptr [ebp - 0x1c]
0x495419: mov      eax, dword ptr [eax + 0x254]
0x49541F: call     0x45351c
0x495424: mov      dword ptr [ebp - 0x10], eax
0x495427: xor      eax, eax
0x495429: mov      dword ptr [ebp - 0x18], eax
0x49542C: jmp      0x495450
0x49542E: mov      eax, dword ptr [ebp - 0x1c]
0x495431: mov      edx, dword ptr [eax + 0x258]
0x495437: inc      edx
0x495438: mov      eax, dword ptr [ebp - 0x1c]
0x49543B: mov      eax, dword ptr [eax + 0x254]
0x495441: call     0x45351c
0x495446: mov      dword ptr [ebp - 0x10], eax
0x495449: mov      dword ptr [ebp - 0x18], 1
0x495450: cmp      dword ptr [ebp - 0x10], 0
0x495454: je       0x495493
0x495456: lea      edx, [ebp - 0x2c]
0x495459: mov      eax, dword ptr [ebp - 0x10]
0x49545C: call     0x44d8ec
0x495461: lea      eax, [ebp - 0x2c]
0x495464: push     eax
0x495465: mov      eax, dword ptr [ebp - 0x10]
0x495468: push     eax
0x495469: push     1
0x49546B: mov      eax, dword ptr [ebp - 0x1c]
0x49546E: mov      edx, dword ptr [eax + 0x34]
0x495471: mov      eax, dword ptr [ebp - 0x1c]
0x495474: call     0x456fdc
0x495479: push     eax
0x49547A: mov      eax, dword ptr [ebp - 0x1c]
0x49547D: mov      edx, dword ptr [eax + 0x30]
0x495480: mov      eax, dword ptr [ebp - 0x1c]
0x495483: call     0x456f94
0x495488: mov      edx, eax
0x49548A: mov      eax, dword ptr [ebp - 0xc]
0x49548D: pop      ecx
0x49548E: call     0x44d6cc
0x495493: push     1
0x495495: mov      eax, dword ptr [ebp - 0xc]
0x495498: call     0x44d8b4
0x49549D: call     0x41834c
0x4954A2: push     eax
0x4954A3: call     0x406434
0x4954A8: mov      eax, dword ptr [ebp - 0xc]
0x4954AB: call     0x44d8b4
0x4954B0: mov      eax, dword ptr [eax + 0xc]
0x4954B3: call     0x417674
0x4954B8: mov      dword ptr [ebp - 0x14], eax
0x4954BB: mov      eax, dword ptr [ebp - 0xc]
0x4954BE: call     0x44d8b4
0x4954C3: mov      eax, dword ptr [eax + 0xc]
0x4954C6: mov      edx, 0xc
0x4954CB: call     0x417690
0x4954D0: mov      eax, dword ptr [ebp - 0xc]
0x4954D3: call     0x44d8b4
0x4954D8: mov      eax, dword ptr [eax + 0xc]
0x4954DB: mov      dl, byte ptr [0x4955b8]
0x4954E1: call     0x4176bc
0x4954E6: mov      al, 0x96
0x4954E8: call     0x45edc8
0x4954ED: push     eax
0x4954EE: push     0
0x4954F0: mov      eax, dword ptr [ebp - 8]
0x4954F3: mov      eax, dword ptr [eax + 0x1f0]
0x4954F9: push     eax
0x4954FA: mov      eax, dword ptr [ebp - 0x1c]
0x4954FD: mov      edx, dword ptr [eax + 0x34]
0x495500: mov      eax, dword ptr [ebp - 0x1c]
0x495503: call     0x456fdc
0x495508: mov      ebx, eax
0x49550A: mov      eax, dword ptr [ebp - 0xc]
0x49550D: call     0x44d8b4
0x495512: mov      edx, dword ptr [ebp - 8]
0x495515: mov      edx, dword ptr [edx + 0x1f0]
0x49551B: call     0x4181c8
0x495520: mov      edx, dword ptr [ebp - 8]
0x495523: mov      edx, dword ptr [edx + 0x3c]
0x495526: sub      edx, eax
0x495528: sar      edx, 1
0x49552A: jns      0x49552f
0x49552C: adc      edx, 0
0x49552F: add      ebx, edx
0x495531: add      ebx, dword ptr [ebp - 0x18]
0x495534: push     ebx
0x495535: mov      eax, dword ptr [ebp - 0x1c]
0x495538: mov      edx, dword ptr [eax + 0x30]
0x49553B: mov      eax, dword ptr [ebp - 0x1c]
0x49553E: call     0x456f94
0x495543: mov      ebx, eax
0x495545: mov      eax, dword ptr [ebp - 0xc]
0x495548: call     0x44d8b4
0x49554D: mov      edx, dword ptr [ebp - 8]
0x495550: mov      edx, dword ptr [edx + 0x1f0]
0x495556: call     0x4181ac
0x49555B: mov      edx, dword ptr [ebp - 8]
0x49555E: mov      edx, dword ptr [edx + 0x38]
0x495561: sub      edx, eax
0x495563: sar      edx, 1
0x495565: jns      0x49556a
0x495567: adc      edx, 0
0x49556A: add      ebx, edx
0x49556C: mov      edx, ebx
0x49556E: add      edx, dword ptr [ebp - 0x18]
0x495571: mov      eax, dword ptr [ebp - 0xc]
0x495574: pop      ecx
0x495575: call     0x4a09a0
0x49557A: mov      eax, dword ptr [ebp - 0xc]
0x49557D: call     0x44d8b4
0x495582: mov      eax, dword ptr [eax + 0xc]
0x495585: mov      edx, dword ptr [ebp - 0x14]
0x495588: call     0x417690
0x49558D: mov      eax, dword ptr [ebp - 0xc]
0x495590: call     0x44d8b4
0x495595: mov      eax, dword ptr [eax + 0xc]
0x495598: mov      dl, byte ptr [0x4955bc]
0x49559E: call     0x4176bc
0x4955A3: mov      eax, dword ptr [ebp - 0xc]
0x4955A6: call     0x44d8b4
0x4955AB: call     0x44d104
0x4955B0: pop      ebx
0x4955B1: mov      esp, ebp
0x4955B3: pop      ebp
0x4955B4: ret      
0x4955B5: add      byte ptr [eax], al
0x4955B7: add      byte ptr [ecx], al
0x4955B9: add      byte ptr [eax], al
0x4955BB: add      byte ptr [eax], al
0x4955BD: add      byte ptr [eax], al
0x4955BF: add      byte ptr [ebp - 0x75], dl
0x4955C2: in       al, dx
0x4955C3: xor      eax, eax
0x4955C5: push     ebp
0x4955C6: push     0x4955fc
0x4955CB: push     dword ptr fs:[eax]
0x4955CE: mov      dword ptr fs:[eax], esp
0x4955D1: inc      dword ptr [0x4f95d0]
0x4955D7: jne      0x4955ee
0x4955D9: mov      eax, 0x4a3034 ; *0x4A3034->0x47FDB8
0x4955DE: mov      ecx, 9
0x4955E3: mov      edx, dword ptr [0x401094]
0x4955E9: call     0x4042e4
0x4955EE: xor      eax, eax
0x4955F0: pop      edx
0x4955F1: pop      ecx
0x4955F2: pop      ecx
0x4955F3: mov      dword ptr fs:[eax], edx
0x4955F6: push     0x495603
0x4955FB: ret      
0x4955FC: jmp      0x403518
0x495601: jmp      0x4955fb
0x495603: pop      ebp
0x495604: ret      
0x495605: lea      eax, [eax]
0x495608: sub      dword ptr [0x4f95d0], 1
0x49560F: ret      
0x495610: push     ebp
0x495611: mov      ebp, esp
0x495613: add      esp, -0x34
0x495616: push     ebx
0x495617: xor      edx, edx
0x495619: mov      dword ptr [ebp - 0x2c], edx
0x49561C: mov      dword ptr [ebp - 0x30], edx
0x49561F: mov      dword ptr [ebp - 0x1c], edx
0x495622: mov      dword ptr [ebp - 0x20], edx
0x495625: mov      dword ptr [ebp - 4], eax
0x495628: mov      eax, dword ptr [ebp - 4]
0x49562B: call     0x403e88
0x495630: xor      eax, eax
0x495632: push     ebp
0x495633: push     0x495788
0x495638: push     dword ptr fs:[eax]
0x49563B: mov      dword ptr fs:[eax], esp
0x49563E: mov      eax, dword ptr [ebp - 4]
0x495641: call     0x407f90
0x495646: test     al, al
0x495648: je       0x495758
0x49564E: mov      dl, 1
0x495650: mov      eax, dword ptr [0x40e37c]
0x495655: call     0x402e7c
0x49565A: mov      dword ptr [ebp - 0x18], eax
0x49565D: mov      edx, dword ptr [ebp - 4]
0x495660: mov      eax, dword ptr [ebp - 0x18]
0x495663: mov      ecx, dword ptr [eax]
0x495665: call     dword ptr [ecx + 0x58]
0x495668: xor      eax, eax
0x49566A: mov      dword ptr [ebp - 0x10], eax
0x49566D: mov      eax, dword ptr [ebp - 0x18]
0x495670: mov      edx, dword ptr [eax]
0x495672: call     dword ptr [edx + 0x14]
0x495675: dec      eax
0x495676: test     eax, eax
0x495678: jl       0x495750
0x49567E: inc      eax
0x49567F: mov      dword ptr [ebp - 0x24], eax
0x495682: mov      dword ptr [ebp - 8], 0
0x495689: lea      ecx, [ebp - 0x1c]
0x49568C: mov      edx, dword ptr [ebp - 8]
0x49568F: mov      eax, dword ptr [ebp - 0x18]
0x495692: mov      ebx, dword ptr [eax]
0x495694: call     dword ptr [ebx + 0xc]
0x495697: cmp      dword ptr [ebp - 0x1c], 0
0x49569B: je       0x495744
0x4956A1: mov      eax, dword ptr [ebp - 0x1c]
0x4956A4: cmp      byte ptr [eax], 0x3b
0x4956A7: je       0x495744
0x4956AD: push     2
0x4956AF: lea      eax, [ebp - 0x30]
0x4956B2: push     eax
0x4956B3: mov      byte ptr [ebp - 0x34], 0x3a
0x4956B7: mov      byte ptr [ebp - 0x33], 0x20
0x4956BB: mov      byte ptr [ebp - 0x32], 9
0x4956BF: lea      ecx, [ebp - 0x34]
0x4956C2: lea      edx, [ebp - 0x20]
0x4956C5: mov      eax, dword ptr [ebp - 0x1c]
0x4956C8: call     0x452530
0x4956CD: mov      eax, dword ptr [ebp - 0x30]
0x4956D0: lea      edx, [ebp - 0x2c]
0x4956D3: call     0x407be4
0x4956D8: mov      edx, dword ptr [ebp - 0x2c]
0x4956DB: lea      eax, [ebp - 0x1c]
0x4956DE: call     0x403af0
0x4956E3: xor      edx, edx
0x4956E5: mov      eax, dword ptr [ebp - 0x20]
0x4956E8: call     0x4520e8
0x4956ED: mov      dword ptr [ebp - 0x14], eax
0x4956F0: mov      eax, dword ptr [ebp - 0x14]
0x4956F3: cmp      eax, dword ptr [ebp - 0x10]
0x4956F6: jle      0x495744
0x4956F8: mov      eax, dword ptr [0x4a41fc] ; *0x4A41FC->0x4F7DC8
0x4956FD: mov      eax, dword ptr [eax]
0x4956FF: mov      edx, dword ptr [eax]
0x495701: call     dword ptr [edx + 0x14]
0x495704: mov      edx, dword ptr [ebp - 0x14]
0x495707: sub      edx, eax
0x495709: dec      edx
0x49570A: test     edx, edx
0x49570C: jl       0x49572f
0x49570E: inc      edx
0x49570F: mov      dword ptr [ebp - 0x28], edx
0x495712: mov      dword ptr [ebp - 0xc], 0
0x495719: mov      eax, dword ptr [0x4a41fc] ; *0x4A41FC->0x4F7DC8
0x49571E: mov      eax, dword ptr [eax]
0x495720: xor      edx, edx
0x495722: mov      ecx, dword ptr [eax]
0x495724: call     dword ptr [ecx + 0x34]
0x495727: inc      dword ptr [ebp - 0xc]
0x49572A: dec      dword ptr [ebp - 0x28]
0x49572D: jne      0x495719
0x49572F: mov      eax, dword ptr [0x4a41fc] ; *0x4A41FC->0x4F7DC8
0x495734: mov      eax, dword ptr [eax]
0x495736: mov      edx, dword ptr [ebp - 0x1c]
0x495739: mov      ecx, dword ptr [eax]
0x49573B: call     dword ptr [ecx + 0x34]
0x49573E: mov      eax, dword ptr [ebp - 0x14]
0x495741: mov      dword ptr [ebp - 0x10], eax
0x495744: inc      dword ptr [ebp - 8]
0x495747: dec      dword ptr [ebp - 0x24]
0x49574A: jne      0x495689
0x495750: mov      eax, dword ptr [ebp - 0x18]
0x495753: call     0x402eac
0x495758: xor      eax, eax
0x49575A: pop      edx
0x49575B: pop      ecx
0x49575C: pop      ecx
0x49575D: mov      dword ptr fs:[eax], edx
0x495760: push     0x49578f
0x495765: lea      eax, [ebp - 0x30]
0x495768: mov      edx, 2
0x49576D: call     0x403a7c
0x495772: lea      eax, [ebp - 0x20]
0x495775: mov      edx, 2
0x49577A: call     0x403a7c
0x49577F: lea      eax, [ebp - 4]
0x495782: call     0x403a58
0x495787: ret      
0x495788: jmp      0x403518
0x49578D: jmp      0x495765
0x49578F: pop      ebx
0x495790: mov      esp, ebp
0x495792: pop      ebp
0x495793: ret      
0x495794: push     ebp
0x495795: mov      ebp, esp
0x495797: add      esp, -8
0x49579A: push     ebx
0x49579B: push     esi
0x49579C: push     edi
0x49579D: xor      edx, edx
0x49579F: mov      dword ptr [ebp - 8], edx
0x4957A2: mov      dword ptr [ebp - 4], eax
0x4957A5: xor      eax, eax
0x4957A7: push     ebp
0x4957A8: push     0x49587d
0x4957AD: push     dword ptr fs:[eax]
0x4957B0: mov      dword ptr fs:[eax], esp
0x4957B3: mov      eax, dword ptr [0x4a40a4] ; *0x4A40A4->0x4F7DA8
0x4957B8: cmp      dword ptr [eax], 0
0x4957BB: je       0x495867
0x4957C1: mov      eax, dword ptr [0x4a3d9c] ; *0x4A3D9C->0x4F9390
0x4957C6: cmp      byte ptr [eax], 0
0x4957C9: je       0x495867
0x4957CF: cmp      dword ptr [ebp - 4], 0
0x4957D3: jl       0x495867
0x4957D9: mov      eax, dword ptr [0x4a41fc] ; *0x4A41FC->0x4F7DC8
0x4957DE: mov      eax, dword ptr [eax]
0x4957E0: mov      edx, dword ptr [eax]
0x4957E2: call     dword ptr [edx + 0x14]
0x4957E5: cmp      eax, dword ptr [ebp - 4]
0x4957E8: jle      0x495867
0x4957EA: lea      ecx, [ebp - 8]
0x4957ED: mov      eax, dword ptr [0x4a41fc] ; *0x4A41FC->0x4F7DC8
0x4957F2: mov      eax, dword ptr [eax]
0x4957F4: mov      edx, dword ptr [ebp - 4]
0x4957F7: mov      ebx, dword ptr [eax]
0x4957F9: call     dword ptr [ebx + 0xc]
0x4957FC: cmp      dword ptr [ebp - 8], 0
0x495800: je       0x495867
0x495802: lea      ecx, [ebp - 8]
0x495805: mov      eax, dword ptr [0x4a41fc] ; *0x4A41FC->0x4F7DC8
0x49580A: mov      eax, dword ptr [eax]
0x49580C: mov      edx, dword ptr [ebp - 4]
0x49580F: mov      ebx, dword ptr [eax]
0x495811: call     dword ptr [ebx + 0xc]
0x495814: mov      eax, dword ptr [ebp - 8]
0x495817: call     0x407f90
0x49581C: test     al, al
0x49581E: je       0x495867
0x495820: xor      eax, eax
0x495822: push     ebp
0x495823: push     0x49585d
0x495828: push     dword ptr fs:[eax]
0x49582B: mov      dword ptr fs:[eax], esp
0x49582E: push     0
0x495830: lea      ecx, [ebp - 8]
0x495833: mov      eax, dword ptr [0x4a41fc] ; *0x4A41FC->0x4F7DC8
0x495838: mov      eax, dword ptr [eax]
0x49583A: mov      edx, dword ptr [ebp - 4]
0x49583D: mov      ebx, dword ptr [eax]
0x49583F: call     dword ptr [ebx + 0xc]
0x495842: mov      edx, dword ptr [ebp - 8]
0x495845: mov      eax, dword ptr [0x4a40a4] ; *0x4A40A4->0x4F7DA8
0x49584A: mov      eax, dword ptr [eax]
0x49584C: xor      ecx, ecx
0x49584E: call     0x45d8d4
0x495853: xor      eax, eax
0x495855: pop      edx
0x495856: pop      ecx
0x495857: pop      ecx
0x495858: mov      dword ptr fs:[eax], edx
0x49585B: jmp      0x495867
0x49585D: jmp      0x403310
0x495862: call     0x4035c0
0x495867: xor      eax, eax
0x495869: pop      edx
0x49586A: pop      ecx
0x49586B: pop      ecx
0x49586C: mov      dword ptr fs:[eax], edx
0x49586F: push     0x495884
0x495874: lea      eax, [ebp - 8]
0x495877: call     0x403a58
0x49587C: ret      
0x49587D: jmp      0x403518
0x495882: jmp      0x495874
0x495884: pop      edi
0x495885: pop      esi
0x495886: pop      ebx
0x495887: pop      ecx
0x495888: pop      ecx
0x495889: pop      ebp
0x49588A: ret      
0x49588B: nop      
0x49588C: push     ebp
0x49588D: mov      ebp, esp
0x49588F: add      esp, -8
0x495892: push     ebx
0x495893: push     esi
0x495894: push     edi
0x495895: mov      dword ptr [ebp - 4], eax
0x495898: mov      eax, dword ptr [ebp - 4]
0x49589B: call     0x403e88
0x4958A0: xor      eax, eax
0x4958A2: push     ebp
0x4958A3: push     0x495915
0x4958A8: push     dword ptr fs:[eax]
0x4958AB: mov      dword ptr fs:[eax], esp
0x4958AE: mov      eax, dword ptr [0x4a40a4] ; *0x4A40A4->0x4F7DA8
0x4958B3: cmp      dword ptr [eax], 0
0x4958B6: je       0x4958ff
0x4958B8: cmp      dword ptr [ebp - 4], 0
0x4958BC: je       0x4958ff
0x4958BE: mov      eax, dword ptr [ebp - 4]
0x4958C1: call     0x407f90
0x4958C6: test     al, al
0x4958C8: je       0x4958ff
0x4958CA: xor      eax, eax
0x4958CC: push     ebp
0x4958CD: push     0x4958f5
0x4958D2: push     dword ptr fs:[eax]
0x4958D5: mov      dword ptr fs:[eax], esp
0x4958D8: push     0
0x4958DA: mov      eax, dword ptr [0x4a40a4] ; *0x4A40A4->0x4F7DA8
0x4958DF: mov      eax, dword ptr [eax]
0x4958E1: mov      cl, 1
0x4958E3: mov      edx, dword ptr [ebp - 4]
0x4958E6: call     0x45d8d4
0x4958EB: xor      eax, eax
0x4958ED: pop      edx
0x4958EE: pop      ecx
0x4958EF: pop      ecx
0x4958F0: mov      dword ptr fs:[eax], edx
0x4958F3: jmp      0x4958ff
0x4958F5: jmp      0x403310
0x4958FA: call     0x4035c0
0x4958FF: xor      eax, eax
0x495901: pop      edx
0x495902: pop      ecx
0x495903: pop      ecx
0x495904: mov      dword ptr fs:[eax], edx
0x495907: push     0x49591c
0x49590C: lea      eax, [ebp - 4]
0x49590F: call     0x403a58
0x495914: ret      
0x495915: jmp      0x403518
0x49591A: jmp      0x49590c
0x49591C: mov      eax, dword ptr [ebp - 8]
0x49591F: pop      edi
0x495920: pop      esi
0x495921: pop      ebx
0x495922: pop      ecx
0x495923: pop      ecx
0x495924: pop      ebp
0x495925: ret      
0x495926: mov      eax, eax
0x495928: mov      eax, dword ptr [0x4a40a4] ; *0x4A40A4->0x4F7DA8
0x49592D: cmp      dword ptr [eax], 0
0x495930: je       0x49593e
0x495932: mov      eax, dword ptr [0x4a40a4] ; *0x4A40A4->0x4F7DA8
0x495937: mov      eax, dword ptr [eax]
0x495939: call     0x45d88c
0x49593E: ret      
0x49593F: nop      
0x495940: push     ebp
0x495941: mov      ebp, esp
0x495943: add      esp, -0x2c
0x495946: push     esi
0x495947: push     edi
0x495948: mov      esi, eax
0x49594A: lea      edi, [ebp - 0x2c]
0x49594D: mov      ecx, 0xb
0x495952: rep movsd dword ptr es:[edi], dword ptr [esi]
0x495954: xor      eax, eax
0x495956: mov      al, byte ptr [ebp - 0x1d]
0x495959: cmp      eax, 0x1a
0x49595C: ja       0x495a2f
0x495962: mov      al, byte ptr [eax + 0x49596f]
0x495968: jmp      dword ptr [eax*4 + 0x49598a]
0x49596F: add      dword ptr [eax], eax
0x495971: add      byte ptr [eax], al
0x495973: add      byte ptr [edx], al
0x495975: add      al, byte ptr [eax]
0x495977: add      byte ptr [eax], al
0x495979: add      eax, dword ptr [ebx]
0x49597B: add      byte ptr [eax], al
0x49597D: add      byte ptr [edi], al
0x49597F: add      byte ptr [eax], al
0x495981: add      byte ptr [esi], al
0x495983: push     es
0x495984: push     es
0x495985: add      al, 4
0x495987: add      eax, 0x5a2f0500
0x49598C: dec      ecx
0x49598D: add      byte ptr [edx - 0x46ffb6a7], ch
0x495993: pop      ecx
0x495994: dec      ecx
0x495995: add      ch, al
0x495997: pop      ecx
0x495998: dec      ecx
0x495999: add      cl, dl
0x49599B: pop      ecx
0x49599C: dec      ecx
0x49599D: add      ch, bl
0x49599F: pop      ecx
0x4959A0: dec      ecx
0x4959A1: add      byte ptr [edi], dl
0x4959A3: pop      edx
0x4959A4: dec      ecx
0x4959A5: add      byte ptr [ebx], ah
0x4959A7: pop      edx
0x4959A8: dec      ecx
0x4959A9: add      byte ptr [eax + 0x6c], bh
0x4959AF: call     0x495794
0x4959B4: jmp      0x495a39
0x4959B9: mov      eax, 0x6f
0x4959BE: call     0x495794
0x4959C3: jmp      0x495a39
0x4959C5: mov      eax, 0x70
0x4959CA: call     0x495794
0x4959CF: jmp      0x495a39
0x4959D1: mov      eax, 0x71
0x4959D6: call     0x495794
0x4959DB: jmp      0x495a39
0x4959DD: lea      edx, [ebp - 0x2c]
0x4959E0: mov      eax, 0x495a40
0x4959E5: call     0x4028dc
0x4959EA: test     eax, eax
0x4959EC: jg       0x4959ff
0x4959EE: lea      edx, [ebp - 0x2c]
0x4959F1: mov      eax, 0x495a48
0x4959F6: call     0x4028dc
0x4959FB: test     eax, eax
0x4959FD: jle      0x495a0b
0x4959FF: mov      eax, 0x75
0x495A04: call     0x495794
0x495A09: jmp      0x495a39
0x495A0B: mov      eax, 0x72
0x495A10: call     0x495794
0x495A15: jmp      0x495a39
0x495A17: mov      eax, 0x73
0x495A1C: call     0x495794
0x495A21: jmp      0x495a39
0x495A23: mov      eax, 0x74
0x495A28: call     0x495794
0x495A2D: jmp      0x495a39
0x495A2F: mov      eax, 0x76
0x495A34: call     0x495794
0x495A39: pop      edi
0x495A3A: pop      esi
0x495A3B: mov      esp, ebp
0x495A3D: pop      ebp
0x495A3E: ret      
0x495A3F: add      byte ptr [eax + eax*8], al
0x495A42: in       eax, 0xb0
0x495A44: test     eax, 0x4000000
0x495A49: mov      al, 0xa9
0x495A4B: mov      ecx, 0xda
0x495A50: push     ebp
0x495A51: mov      ebp, esp
0x495A53: push     ecx
0x495A54: mov      dword ptr [ebp - 4], eax
0x495A57: mov      eax, dword ptr [ebp - 4]
0x495A5A: sub      eax, 1
0x495A5D: jb       0x495a66
0x495A5F: sub      eax, 2
0x495A62: jb       0x495a73
0x495A64: jmp      0x495a7d
0x495A66: mov      eax, 0x6c
0x495A6B: call     0x495794
0x495A70: pop      ecx
0x495A71: pop      ebp
0x495A72: ret      
0x495A73: mov      eax, 0x6b
0x495A78: call     0x495794
0x495A7D: pop      ecx
0x495A7E: pop      ebp
0x495A7F: ret      
0x495A80: push     ebp
0x495A81: mov      ebp, esp
0x495A83: xor      eax, eax
0x495A85: push     ebp
0x495A86: push     0x495aa5
0x495A8B: push     dword ptr fs:[eax]
0x495A8E: mov      dword ptr fs:[eax], esp
0x495A91: inc      dword ptr [0x4f95d4]
0x495A97: xor      eax, eax
0x495A99: pop      edx
0x495A9A: pop      ecx
0x495A9B: pop      ecx
0x495A9C: mov      dword ptr fs:[eax], edx
0x495A9F: push     0x495aac
0x495AA4: ret      
0x495AA5: jmp      0x403518
0x495AAA: jmp      0x495aa4
0x495AAC: pop      ebp
0x495AAD: ret      
0x495AAE: mov      eax, eax
0x495AB0: sub      dword ptr [0x4f95d4], 1
0x495AB7: ret      
0x495AB8: mov      esp, 0xd00495a
0x495ABD: add      ch, byte ptr [esi]
0x495ABF: xor      dl, byte ptr [eax + eax]
0x495AC2: add      byte ptr [eax], al
0x495AC4: add      eax, 0x94000000
0x495AC9: adc      byte ptr [eax], al
0x495ACC: sbb      byte ptr [ebx + 0x49], bl
0x495ACF: add      byte ptr [eax], al
0x495AD1: add      byte ptr [eax], al
0x495AD3: add      byte ptr [eax], al
0x495AD5: add      byte ptr [eax], al
0x495AD7: add      byte ptr [eax], bl
0x495AD9: pop      ebx
0x495ADA: dec      ecx
0x495ADB: add      byte ptr [eax], al
0x495ADD: add      byte ptr [eax], al
0x495ADF: add      byte ptr [eax], al
0x495AE1: add      byte ptr [eax], al
0x495AE3: add      byte ptr [eax], al
0x495AE5: add      byte ptr [eax], al
0x495AE7: add      byte ptr [edx + 0x5b], al
0x495AEA: dec      ecx
0x495AEB: add      byte ptr [esi + 0x4400495b], bl
0x495AF1: add      al, byte ptr [eax]
0x495AF3: add      byte ptr [eax - 0x37ffbff0], ah
0x495AF9: xor      byte ptr [eax], al
0x495AFC: aam      0x30
0x495AFE: inc      eax
0x495AFF: add      al, bl
0x495B01: xor      byte ptr [eax], al
0x495B04: fdiv     qword ptr [eax]
0x495B06: inc      eax
0x495B07: add      al, dl
0x495B09: xor      byte ptr [eax], al
0x495B0C: inc      eax
0x495B0D: inc      eax
0x495B0F: add      byte ptr [esi + ebp + 0x40], dl
0x495B13: add      byte ptr [eax], cl
0x495B15: dec      ecx
