
; === VMT_TDControl @ 0x456090 ===
0x456090: in       al, dx
0x456091: loop     0x4560d5
0x456093: add      byte ptr [esp + ecx*4 - 0x243ffbe], bl
0x45609A: inc      eax
0x45609B: add      byte ptr [eax], cl
0x45609D: outsd    dx, dword ptr [esi]
0x45609E: inc      ebp
0x45609F: add      byte ptr [eax], cl
0x4560A1: jo       0x4560e5
0x4560A3: add      byte ptr [eax], dh
0x4560A5: popfd    
0x4560A6: inc      edx
0x4560A7: add      byte ptr [edi + esi*2 + 0x42], dl
0x4560AB: add      byte ptr [eax + edx*2 + 0x4cf80041], dh
0x4560B2: inc      ecx
0x4560B3: add      al, dh
0x4560B5: dec      esp
0x4560B6: inc      ecx
0x4560B7: add      byte ptr [eax + edx*2 + 0x6b2c0041], bh
0x4560BE: inc      ebp
0x4560BF: add      al, al
0x4560C1: fld      dword ptr [edx]
0x4560C4: hlt      
0x4560C5: fld      qword ptr [edx]
0x4560C8: sbb      al, 0xe3
0x4560CA: inc      edx
0x4560CB: add      byte ptr [eax - 0x1e], cl
0x4560CE: inc      edx
0x4560CF: add      ah, bl
0x4560D1: xlatb    
0x4560D2: inc      edx
0x4560D3: add      ah, bh
0x4560D5: xlatb    
0x4560D6: inc      edx
0x4560D7: add      byte ptr [eax], dl
0x4560D9: aad      0x42
0x4560DB: add      ah, ah
0x4560DD: insd     dword ptr es:[edi], dx
0x4560DE: inc      edx
0x4560DF: add      al, ch
0x4560E1: insd     dword ptr es:[edi], dx
0x4560E2: inc      edx
0x4560E3: add      byte ptr [ecx + ebx*4], dh
0x4560E6: inc      edx
0x4560E7: add      byte ptr [eax - 0x67], ah
0x4560EA: inc      edx
0x4560EB: add      byte ptr [eax + 0x6f], al
0x4560EE: inc      edx
0x4560EF: add      byte ptr [ecx + edi*2], al
0x4560F2: inc      edx
0x4560F3: add      byte ptr [eax + edi*2], bh
0x4560F6: inc      edx
0x4560F7: add      byte ptr [eax + 0x6400427b], ah
0x4560FD: jp       0x456141
0x4560FF: add      byte ptr [eax], cl
0x456101: mov      dl, 0x42
0x456103: add      ah, ah
0x456105: cdq      
0x456106: inc      edx
0x456107: add      byte ptr [eax + 0x240042d5], ch
0x45610D: salc     
0x45610E: inc      edx
0x45610F: add      byte ptr [eax - 0x2f], dl
0x456112: inc      edx
0x456113: add      byte ptr [eax], cl
0x456115: salc     
0x456116: inc      edx
0x456117: add      byte ptr [eax - 0x7fffbd63], ch
0x45611D: mov      byte ptr [0xadf80042], al
0x456122: inc      edx
0x456123: add      byte ptr [edx + ebp*4], bl
0x456126: inc      edx
0x456127: add      byte ptr [eax + 0x4c0042ac], bh
0x45612D: stosd    dword ptr es:[edi], eax
0x45612E: inc      edx
0x45612F: add      byte ptr [ebp + ebp*4 + 0x42], dl
0x456133: add      ah, bh
0x456135: lodsb    al, byte ptr [esi]
0x456136: inc      edx
0x456137: add      byte ptr [eax - 0x29], cl
0x45613A: inc      edx
0x45613B: add      byte ptr [eax - 0x3fffbd0f], dh
0x456141: rol      dword ptr [edx], cl
0x456144: mov      ah, 0xd6
0x456146: inc      edx
0x456147: add      byte ptr [eax], ah
0x456149: insd     dword ptr es:[edi], dx
0x45614A: inc      ebp
0x45614B: add      byte ptr [esi], cl
0x45614D: add      byte ptr [eax], al
0x45614F: add      byte ptr [eax], al
0x456151: add      byte ptr [ecx], al
0x456153: add      byte ptr [eax], al
0x456155: add      byte ptr [eax + edx + 0x1f00040], dl
0x45615C: add      byte ptr [eax], al
0x45615E: or       byte ptr [eax], al
0x456160: mov      eax, 0xb6ffb7ff
0x456165: push     dword ptr [ebp - 0x4c004b01]
0x45616B: push     dword ptr [edx + 0x5cffb1ff]
0x456171: jb       0x4561b8
0x456173: add      byte ptr [ebx + esi*2], dl
0x456176: inc      ebp
0x456177: add      byte ptr [eax], ah
0x456179: je       0x4561c0
0x45617B: add      byte ptr [ebp + esi*2 + 0x45], al
0x45617F: add      byte ptr [eax - 0x33ffba8a], bl
0x456185: ja       0x4561cc
0x456187: add      al, bl
0x456189: js       0x4561d0
0x45618B: add      byte ptr [edx + edi*2 + 0x45], bl
0x45618F: add      byte ptr [ecx], cl
0x456191: push     esp
0x456192: inc      esp
0x456193: inc      ebx
0x456194: outsd    dx, dword ptr [esi]
0x456195: outsb    dx, byte ptr [esi]
0x456196: je       0x45620a
0x456198: outsd    dx, dword ptr [esi]
0x456199: insb     byte ptr es:[edi], dx
0x45619A: mov      eax, eax
0x45619C: mov      al, byte ptr [0x7004561]
0x4561A1: or       dword ptr [esp + eax*2 + 0x43], edx
0x4561A5: outsd    dx, dword ptr [esi]
0x4561A6: outsb    dx, byte ptr [esi]
0x4561A7: je       0x45621b
0x4561A9: outsd    dx, dword ptr [esi]
0x4561AA: insb     byte ptr es:[edi], dx
0x4561AB: nop      
0x4561AC: pushal   
0x4561AD: inc      ebp
0x4561AE: add      byte ptr [eax], dh
0x4561B0: dec      eax
0x4561B1: inc      edx
0x4561B2: add      byte ptr [ebx], bl
0x4561B4: add      byte ptr [edi], al
0x4561B6: inc      esp
0x4561B7: push     edi
0x4561B8: imul     ebp, dword ptr [esi + 0x43], 0x136c74
0x4561BF: rcr      byte ptr [ebp + eax*2], 0xfc
0x4561C4: add      dword ptr [eax], eax
0x4561C7: cld      
0x4561C8: add      dword ptr [eax], eax
0x4561CA: inc      dword ptr [ecx]
0x4561CC: add      byte ptr [eax], al
0x4561CE: add      byte ptr [eax], al
0x4561D0: add      byte ptr [eax], al
0x4561D2: add      byte ptr [eax], 0
0x4561D5: add      byte ptr [eax + 0x4f0d0009], al
0x4561DB: outsb    dx, byte ptr [esi]
0x4561DC: inc      esp
0x4561DD: imul     esi, dword ptr [edx + 0x65], 0x61507463
0x4561E4: imul     ebp, dword ptr [esi + 0x74], 0x455d04
0x4561EB: add      al, 2
0x4561ED: add      bh, bh
0x4561EF: add      al, 2
0x4561F1: add      bh, bh
0x4561F3: add      dword ptr [eax], eax
0x4561F5: add      byte ptr [eax], al
0x4561F7: add      byte ptr [eax], al
0x4561F9: add      byte ptr [eax - 0x80000000], al
0x4561FF: or       al, byte ptr [eax]
0x456201: or       cl, byte ptr [edi + 0x6e]
0x456204: dec      ebx
0x456205: jns      0x456258
0x456208: jb       0x45626f
0x45620A: jae      0x45627f
0x45620C: xor      al, 0x5d
0x45620E: inc      ebp
0x45620F: add      byte ptr [edx + eax], cl
0x456212: add      bh, bh
0x456214: or       al, 2
0x456216: add      bh, bh
0x456218: add      dword ptr [eax], eax
0x45621A: add      byte ptr [eax], al
0x45621C: add      byte ptr [eax], al
0x45621E: add      byte ptr [eax - 0x80000000], al
0x456224: or       eax, dword ptr [eax]
0x456226: or       dword ptr [edi + 0x6e], ecx
0x456229: dec      ebx
0x45622A: jns      0x456271
0x45622D: outsd    dx, dword ptr [esi]
0x45622E: ja       0x45629e
0x456230: je       0x45628f
0x456232: inc      ebp
0x456233: add      byte ptr [edx + eax], dl
0x456236: add      bh, bh
0x456238: adc      al, 2
0x45623A: add      bh, bh
0x45623C: add      dword ptr [eax], eax
0x45623E: add      byte ptr [eax], al
0x456240: add      byte ptr [eax], al
0x456242: add      byte ptr [eax - 0x80000000], al
0x456248: or       al, 0
0x45624A: or       ecx, dword ptr [edi + 0x6e]
0x45624D: dec      ebp
0x45624E: outsd    dx, dword ptr [esi]
0x45624F: jne      0x4562c4
0x456251: dec      ebp
0x456253: outsd    dx, dword ptr [esi]
0x456254: jbe      0x4562bb
0x456256: les      ebx, ptr [ebp + 0x45]
0x456259: add      byte ptr [edx + eax], bl
0x45625C: add      bh, bh
0x45625E: sbb      al, 2
0x456260: add      bh, bh
0x456262: add      dword ptr [eax], eax
0x456264: add      byte ptr [eax], al
0x456266: add      byte ptr [eax], al
0x456268: add      byte ptr [eax - 0x80000000], al
0x45626E: or       eax, 0x6e4f0b00
0x456273: dec      ebp
0x456274: outsd    dx, dword ptr [esi]
0x456275: jne      0x4562ea
0x456277: inc      esp
0x456279: outsd    dx, dword ptr [esi]
0x45627A: ja       0x4562ea
0x45627C: sub      byte ptr [esi + 0x45], bl
0x45627F: add      byte ptr [edx + eax], ah
0x456282: add      bh, bh
0x456284: and      al, 2
0x456286: add      bh, bh
0x456288: add      dword ptr [eax], eax
0x45628A: add      byte ptr [eax], al
0x45628C: add      byte ptr [eax], al
0x45628E: add      byte ptr [eax - 0x80000000], al
0x456294: push     cs
0x456295: add      byte ptr [ecx], cl
0x456297: dec      edi
0x456298: outsb    dx, byte ptr [esi]
0x456299: dec      ebp
0x45629A: outsd    dx, dword ptr [esi]
0x45629B: jne      0x456310
0x45629D: push     ebp
0x45629F: jo       0x45623d
0x4562A1: fld      dword ptr [eax]
0x4562A4: sub      al, 2
0x4562A6: add      bh, bh
0x4562A8: sub      al, 2
0x4562AA: add      bh, bh
0x4562AC: add      dword ptr [eax], eax
0x4562AE: add      byte ptr [eax], al
0x4562B0: add      byte ptr [eax], al
0x4562B2: add      byte ptr [eax - 0x80000000], al
0x4562B8: str      word ptr [edx]
0x4562BB: dec      edi
0x4562BC: outsb    dx, byte ptr [esi]
0x4562BD: inc      esp
0x4562BE: bound    ebp, qword ptr [ebx + eax*2 + 0x6c]
0x4562C2: imul     esp, dword ptr [ebx + 0x6b], 0x455ea8
0x4562C9: xor      al, 2
0x4562CB: add      bh, bh
0x4562CD: xor      al, 2
0x4562CF: add      bh, bh
0x4562D1: add      dword ptr [eax], eax
0x4562D3: add      byte ptr [eax], al
0x4562D5: add      byte ptr [eax], al
0x4562D7: add      byte ptr [eax - 0x80000000], al
0x4562DD: adc      byte ptr [eax], al
0x4562DF: pop      es
0x4562E0: dec      edi
0x4562E1: outsb    dx, byte ptr [esi]
0x4562E2: inc      ebx
0x4562E3: insb     byte ptr es:[edi], dx
0x4562E4: imul     esp, dword ptr [ebx + 0x6b], 0x455ee0
0x4562EB: cmp      al, 2
0x4562ED: add      bh, bh
0x4562EF: cmp      al, 2
0x4562F1: add      bh, bh
0x4562F3: add      dword ptr [eax], eax
0x4562F5: add      byte ptr [eax], al
0x4562F7: add      byte ptr [eax], al
0x4562F9: add      byte ptr [eax - 0x80000000], al
0x4562FF: adc      dword ptr [eax], eax
0x456301: or       al, 0x4f
0x456303: outsb    dx, byte ptr [esi]
0x456304: dec      ecx
0x456305: outsb    dx, byte ptr [esi]
0x456306: push     edx
0x456307: popal    
0x456309: insb     byte ptr es:[edi], dx
0x45630A: inc      ecx
0x45630B: jb       0x456372
0x45630D: popal    
0x45630E: mov      byte ptr [esi + 0x45], bl
0x456311: add      byte ptr [edx + eax], al
0x456315: inc      dword ptr [edx + eax]
0x456319: inc      dword ptr [ecx]
0x45631B: add      byte ptr [eax], al
0x45631D: add      byte ptr [eax], al
0x45631F: add      byte ptr [eax], al
0x456321: add      byte ptr [eax], 0
0x456324: add      byte ptr [eax + 0x4f110012], al
0x45632A: outsb    dx, byte ptr [esi]
0x45632B: inc      edx
0x45632C: popal    
0x45632D: arpl     word ptr [ebx + 0x67], bp
0x456330: jb       0x4563a1
0x456332: jne      0x4563a2
0x456334: inc      ebx
0x456336: insb     byte ptr es:[edi], dx
0x456337: imul     esp, dword ptr [ebx + 0x6b], 0x401094
0x45633E: lock add dword ptr [eax], eax
0x456342: mov      esp, 0x100456c
0x456347: add      byte ptr [eax], al
0x456349: add      byte ptr [eax], al
0x45634B: add      byte ptr [eax], al
0x45634D: add      byte ptr [eax], 0
0x456350: add      byte ptr [eax + 0x43070013], al
0x456356: popal    
0x456357: jo       0x4563cd
0x456359: imul     ebp, dword ptr [edi + 0x6e], 0x45619c
0x456360: hlt      
0x456361: add      dword ptr [eax], eax
0x456363: push     esp
0x456365: add      dword ptr [eax], eax
0x456367: inc      dword ptr [ecx]
0x456369: add      byte ptr [eax], al
0x45636B: add      byte ptr [eax], al
0x45636D: add      byte ptr [eax], al
0x45636F: add      byte ptr [eax], 0
0x456372: add      byte ptr [eax + 0x44070014], al
0x456378: push     eax
0x456379: popal    
0x45637A: jb       0x4563e1
0x45637C: outsb    dx, byte ptr [esi]
0x45637D: je       0x45637f
0x45637F: adc      byte ptr [eax], al
0x456382: dec      esp
0x456383: add      al, byte ptr [eax]
0x456385: dec      dword ptr [edx + eax]
0x456389: inc      dword ptr [ecx]
0x45638B: add      byte ptr [eax], al
0x45638D: add      byte ptr [eax], al
0x45638F: add      byte ptr [eax], al
0x456391: add      byte ptr [eax], 0
0x456394: add      byte ptr [eax + 0x56070015], al
0x45639A: imul     esi, dword ptr [ebx + 0x69], 0x656c62
0x4563A1: adc      byte ptr [eax], al
0x4563A4: clc      
0x4563A5: add      dword ptr [eax], eax
0x4563A8: clc      
0x4563A9: add      dword ptr [eax], eax
0x4563AB: inc      dword ptr [ecx]
0x4563AD: add      byte ptr [eax], al
0x4563AF: add      byte ptr [eax], al
0x4563B1: add      byte ptr [eax], al
0x4563B3: add      byte ptr [eax], 0
0x4563B6: add      byte ptr [eax + 0x450b0016], al
0x4563BC: outsb    dx, byte ptr [esi]
0x4563BD: popal    
0x4563BE: bound    ebp, qword ptr [ebp + 0x46]
0x4563C2: outsd    dx, dword ptr [esi]
0x4563C3: arpl     word ptr [ebp + 0x73], si
0x4563C7: pop      eax
0x4563C8: inc      ecx
0x4563C9: add      byte ptr [eax + eax], ah
0x4563CD: call     ptr [ebx + edi*2 + 0x42]
0x4563D1: add      byte ptr [eax + 0x7b], bh
0x4563D4: inc      edx
0x4563D5: add      byte ptr [eax], al
0x4563D7: add      byte ptr [eax], al
0x4563D9: add      byte ptr [0x17800000], 0
0x4563E0: add      eax, 0x6f6c6f43
0x4563E5: jb       0x45644f
0x4563E7: pop      ebp
0x4563E8: inc      ecx
0x4563E9: add      byte ptr [eax], bl
0x4563EC: add      bh, bh
0x4563EE: mov      esp, 0xcc00427a
0x4563F3: jp       0x456437
0x4563F5: add      byte ptr [eax], al
0x4563F7: add      byte ptr [eax], al
0x4563F9: add      byte ptr [eax], 0
0x4563FC: add      byte ptr [eax + 0x46040018], al
0x456402: outsd    dx, dword ptr [esi]
0x456403: outsb    dx, byte ptr [esi]
0x456404: je       0x45639a
0x456406: adc      byte ptr [eax], al
0x456409: je       0x45640b
0x45640B: add      bh, bh
0x45640D: je       0x45640f
0x45640F: add      bh, bh
0x456411: adc      byte ptr [edi + 0x42], dl
0x456417: add      byte ptr [eax - 0x80000000], al
0x45641D: pop      es
0x45641E: add      byte ptr [eax + ecx*2], al
0x456421: imul     ebp, dword ptr [esi + 0x74], 0x401000
0x456428: test     dword ptr [eax], eax
0x45642A: add      bh, bh
0x45642C: adc      al, 0x7b
0x45642E: inc      edx
0x45642F: add      al, ah
0x456431: jp       0x456475
0x456433: add      byte ptr [eax], al
0x456435: add      byte ptr [eax], al
0x456437: add      byte ptr [eax], 0
0x45643A: add      byte ptr [eax + 0x53080019], al
0x456440: push     0x6948776f
0x456445: outsb    dx, byte ptr [esi]
0x456446: je       0x45643c
0x456448: xor      byte ptr [edx], al
0x45644B: dec      ebx
0x45644C: add      byte ptr [eax], al
0x45644E: dec      dword ptr [eax + esi*2 + 0x42]
0x456452: add      byte ptr [ecx], al
0x456454: add      byte ptr [eax], al
0x456456: add      byte ptr [eax], al
0x456458: add      byte ptr [eax], al
0x45645A: add      byte ptr [eax], 0
0x45645D: add      byte ptr [eax], al
0x45645F: sbb      al, byte ptr [eax]
0x456461: add      eax, 0x67696c41
0x456466: outsb    dx, byte ptr [esi]
0x456467: nop      
0x456468: mov      ah, 0x64
0x45646A: inc      ebp
0x45646B: add      byte ptr [eax], al
0x45646D: add      byte ptr [eax], al
0x45646F: add      byte ptr [eax], al
0x456471: add      byte ptr [eax], al
0x456473: add      byte ptr [eax], al
0x456475: add      byte ptr [eax], al
0x456477: add      byte ptr [ebp + 0x45], dl
0x45647E: add      byte ptr [eax], al
0x456480: add      byte ptr [eax], al
0x456482: add      byte ptr [eax], al
0x456484: jo       0x4564eb
0x456486: inc      ebp
0x456487: add      byte ptr [ebp + 0x2780045], al
0x45648E: add      byte ptr [eax], al
0x456490: inc      esp
0x456491: pushal   
0x456492: inc      ebp
0x456493: add      byte ptr [eax + edx*2 + 0x30d40041], bl
0x45649A: inc      eax
0x45649B: add      al, bl
0x45649D: xor      byte ptr [eax], al
0x4564A0: fdiv     qword ptr [eax]
0x4564A2: inc      eax
0x4564A3: add      ah, al
0x4564A5: mov      bl, 0x42
0x4564A7: add      byte ptr [eax + 0x2e], al
0x4564AA: inc      eax
0x4564AB: add      byte ptr [esi + ebp + 0x40], dl
0x4564AF: add      byte ptr [esp + ebp*2 + 0x45], bh
0x4564B3: add      ah, ch
0x4564B5: loop     0x4564f9
0x4564B7: add      byte ptr [esp + ecx*4 - 0x243ffbe], bl
0x4564BE: inc      eax
0x4564BF: add      byte ptr [eax], cl
0x4564C1: outsd    dx, dword ptr [esi]
0x4564C2: inc      ebp
0x4564C3: add      byte ptr [eax], cl
0x4564C5: jo       0x456509
0x4564C7: add      byte ptr [eax], dh
0x4564C9: popfd    
0x4564CA: inc      edx
0x4564CB: add      byte ptr [edi + esi*2 + 0x42], dl
0x4564CF: add      byte ptr [eax + edx*2 + 0x4cf80041], dh
0x4564D6: inc      ecx
0x4564D7: add      al, dh
0x4564D9: dec      esp
0x4564DA: inc      ecx
0x4564DB: add      byte ptr [eax + edx*2 + 0x7b580041], bh
0x4564E2: inc      ebp
0x4564E3: add      al, al
0x4564E5: fld      dword ptr [edx]
0x4564E8: hlt      
0x4564E9: fld      qword ptr [edx]
0x4564EC: sbb      al, 0xe3
0x4564EE: inc      edx
0x4564EF: add      byte ptr [eax - 0x1e], cl
0x4564F2: inc      edx
0x4564F3: add      ah, bl
0x4564F5: xlatb    
0x4564F6: inc      edx
0x4564F7: add      ah, bh
0x4564F9: xlatb    
0x4564FA: inc      edx
0x4564FB: add      byte ptr [eax], dl
0x4564FD: aad      0x42
0x4564FF: add      ah, ah
0x456501: insd     dword ptr es:[edi], dx
0x456502: inc      edx
0x456503: add      al, ch
0x456505: insd     dword ptr es:[edi], dx
0x456506: inc      edx
0x456507: add      byte ptr [ecx + ebx*4], dh
0x45650A: inc      edx
0x45650B: add      byte ptr [eax - 0x67], ah
0x45650E: inc      edx
0x45650F: add      byte ptr [eax + 0x6f], al
0x456512: inc      edx
0x456513: add      byte ptr [ecx + edi*2], al
0x456516: inc      edx
0x456517: add      byte ptr [eax + edi*2], bh
0x45651A: inc      edx
0x45651B: add      byte ptr [eax + 0x6400427b], ah
0x456521: jp       0x456565
0x456523: add      byte ptr [eax], cl
0x456525: mov      dl, 0x42
0x456527: add      ah, ah
0x456529: cdq      
0x45652A: inc      edx
0x45652B: add      byte ptr [eax + 0x240042d5], ch
0x456531: salc     
0x456532: inc      edx
0x456533: add      byte ptr [eax - 0x2f], dl
0x456536: inc      edx
0x456537: add      byte ptr [eax], cl
0x456539: salc     
0x45653A: inc      edx
0x45653B: add      byte ptr [eax - 0x7fffbd63], ch
0x456541: mov      byte ptr [0xadf80042], al
0x456546: inc      edx
0x456547: add      byte ptr [edx + ebp*4], bl
0x45654A: inc      edx
0x45654B: add      byte ptr [eax + 0x4c0042ac], bh
0x456551: stosd    dword ptr es:[edi], eax
0x456552: inc      edx
0x456553: add      byte ptr [ebp + ebp*4 + 0x42], dl
0x456557: add      ah, bh
0x456559: lodsb    al, byte ptr [esi]
0x45655A: inc      edx
0x45655B: add      byte ptr [eax - 0x29], cl
0x45655E: inc      edx
0x45655F: add      byte ptr [eax - 0x3fffbd0f], dh
0x456565: rol      dword ptr [edx], cl
0x456568: mov      ah, 0xd6
0x45656A: inc      edx
0x45656B: add      byte ptr [eax], ah
0x45656D: insd     dword ptr es:[edi], dx
0x45656E: inc      ebp
0x45656F: add      byte ptr [ebx], al
0x456571: add      byte ptr [esi - 0x4b004a01], dh
0x456577: call     eax
0x456579: jnp      0x4565c0
0x45657B: add      byte ptr [eax + 0x7c], bl
0x45657E: inc      ebp
0x45657F: add      byte ptr [esp + edi*2 + 0x54080045], bh
0x456586: inc      esp
0x456587: inc      edx
0x456588: jne      0x4565fe
0x45658A: je       0x4565fb
0x45658C: outsb    dx, byte ptr [esi]
0x45658D: lea      eax, [eax]
0x456590: xchg     esp, eax
0x456591: inc      ebp
0x456593: add      byte ptr [edi], al
0x456595: or       byte ptr [esp + eax*2 + 0x42], dl
0x456599: jne      0x45660f
0x45659B: je       0x45660c
0x45659D: outsb    dx, byte ptr [esi]
0x45659E: mov      ah, 0x64
0x4565A0: inc      ebp
0x4565A1: add      byte ptr [ecx + 0x1d0045], bl
0x4565A8: pop      es
0x4565A9: inc      esp
0x4565AA: push     edi
0x4565AB: imul     ebp, dword ptr [esi + 0x43], 0x36c74
0x4565B2: test     byte ptr [ebp + eax*2], bl
0x4565B6: pushal   
0x4565B7: add      al, byte ptr [eax]
0x4565B9: jmp      dword ptr [eax + 2]
0x4565BC: add      bh, bh
0x4565BE: add      dword ptr [eax], eax
0x4565C0: add      byte ptr [eax], al
0x4565C2: add      byte ptr [eax], al
0x4565C4: add      byte ptr [eax - 0x80000000], al
0x4565CA: sbb      eax, dword ptr [eax]
0x4565CC: or       al, byte ptr [ebx + 0x6c]
0x4565CF: imul     esp, dword ptr [ebx + 0x6b], 0x6e756f43
0x4565D6: je       0x456580
0x4565D8: pop      esi
0x4565D9: inc      ebp
0x4565DA: add      byte ptr [edx + eax], ah
0x4565DE: jmp      dword ptr [edx + eax]
0x4565E2: inc      dword ptr [ecx]
0x4565E4: add      byte ptr [eax], al
0x4565E6: add      byte ptr [eax], al
0x4565E8: add      byte ptr [eax], al
0x4565EA: add      byte ptr [eax], 0
0x4565ED: add      byte ptr [eax + 0x4f070010], al
0x4565F3: outsb    dx, byte ptr [esi]
0x4565F4: inc      ebx
0x4565F5: insb     byte ptr es:[edi], dx
0x4565F6: imul     esp, dword ptr [ebx + 0x6b], 0x456004
0x4565FD: insb     byte ptr es:[edi], dx
0x4565FE: add      al, byte ptr [eax]
0x456600: jmp      ptr [edx + eax]
0x456604: inc      dword ptr [ecx]
0x456606: add      byte ptr [eax], al
0x456608: add      byte ptr [eax], al
0x45660A: add      byte ptr [eax], al
0x45660C: add      byte ptr [eax], 0
0x45660F: add      byte ptr [eax + 0x4f0c001c], al
0x456615: outsb    dx, byte ptr [esi]
0x456616: inc      ebx
0x456617: insb     byte ptr es:[edi], dx
0x456618: imul     esp, dword ptr [ebx + 0x6b], 0x6e756f53
0x45661F: insb     byte ptr es:[edi], dx
0x456621: inc      bp
0x456623: add      byte ptr [eax], al
0x456625: add      byte ptr [eax], al
0x456627: add      byte ptr [eax], al
0x456629: add      byte ptr [eax], al
0x45662B: add      byte ptr [eax], al
0x45662D: add      byte ptr [eax], al
0x45662F: add      byte ptr [edi + 0x45], dl
0x456633: add      byte ptr [eax], al
0x456635: add      byte ptr [eax], al
0x456637: add      byte ptr [eax], al
0x456639: add      byte ptr [eax], al
0x45663B: add      byte ptr [eax], ch
0x45663D: inc      ebp
0x45663F: add      byte ptr [eax + 0x67], cl
0x456642: inc      ebp
0x456643: add      byte ptr [edx + eax + 0x60440000], ch
0x45664A: inc      ebp
0x45664B: add      byte ptr [eax + edx*2 + 0x30d40041], bl
0x456652: inc      eax
0x456653: add      al, bl
0x456655: xor      byte ptr [eax], al
0x456658: fdiv     qword ptr [eax]
0x45665A: inc      eax
0x45665B: add      ah, al
0x45665D: mov      bl, 0x42
0x45665F: add      byte ptr [eax + 0x2e], al
0x456662: inc      eax
0x456663: add      byte ptr [esi + ebp + 0x40], dl
0x456667: add      byte ptr [esp + ebp*2 + 0x45], bh
0x45666B: add      ah, ch
0x45666D: loop     0x4566b1
0x45666F: add      byte ptr [esp + ecx*4 - 0x243ffbe], bl
0x456676: inc      eax
0x456677: add      byte ptr [eax], cl
0x456679: outsd    dx, dword ptr [esi]
0x45667A: inc      ebp
0x45667B: add      byte ptr [eax], cl
0x45667D: jo       0x4566c1
0x45667F: add      byte ptr [eax], dh
0x456681: popfd    
0x456682: inc      edx
0x456683: add      byte ptr [edi + esi*2 + 0x42], dl
0x456687: add      byte ptr [eax + edx*2 + 0x4cf80041], dh
0x45668E: inc      ecx
0x45668F: add      al, dh
0x456691: dec      esp
0x456692: inc      ecx
0x456693: add      byte ptr [eax + edx*2 + 0x7d880041], bh
0x45669A: inc      ebp
0x45669B: add      al, al
0x45669D: fld      dword ptr [edx]
0x4566A0: hlt      
0x4566A1: fld      qword ptr [edx]
0x4566A4: sbb      al, 0xe3
0x4566A6: inc      edx
0x4566A7: add      byte ptr [eax - 0x1e], cl
0x4566AA: inc      edx
0x4566AB: add      ah, bl
0x4566AD: xlatb    
0x4566AE: inc      edx
0x4566AF: add      ah, bh
0x4566B1: xlatb    
0x4566B2: inc      edx
0x4566B3: add      byte ptr [eax], dl
0x4566B5: aad      0x42
0x4566B7: add      ah, ah
0x4566B9: insd     dword ptr es:[edi], dx
0x4566BA: inc      edx
0x4566BB: add      al, ch
0x4566BD: insd     dword ptr es:[edi], dx
0x4566BE: inc      edx
0x4566BF: add      byte ptr [ecx + ebx*4], dh
0x4566C2: inc      edx
0x4566C3: add      byte ptr [eax - 0x67], ah
0x4566C6: inc      edx
0x4566C7: add      byte ptr [eax + 0x6f], al
0x4566CA: inc      edx
0x4566CB: add      byte ptr [ecx + edi*2], al
0x4566CE: inc      edx
0x4566CF: add      byte ptr [eax + edi*2], bh
0x4566D2: inc      edx
0x4566D3: add      byte ptr [eax + 0x6400427b], ah
0x4566D9: jp       0x45671d
0x4566DB: add      byte ptr [eax], cl
0x4566DD: mov      dl, 0x42
0x4566DF: add      ah, ah
0x4566E1: cdq      
0x4566E2: inc      edx
0x4566E3: add      byte ptr [eax + 0x240042d5], ch
0x4566E9: salc     
0x4566EA: inc      edx
0x4566EB: add      byte ptr [eax - 0x2f], dl
0x4566EE: inc      edx
0x4566EF: add      byte ptr [eax], cl
0x4566F1: salc     
0x4566F2: inc      edx
0x4566F3: add      byte ptr [eax - 0x7fffbd63], ch
0x4566F9: mov      byte ptr [0xadf80042], al
0x4566FE: inc      edx
0x4566FF: add      byte ptr [edx + ebp*4], bl
0x456702: inc      edx
0x456703: add      byte ptr [eax + 0x4c0042ac], bh
0x456709: stosd    dword ptr es:[edi], eax
0x45670A: inc      edx
0x45670B: add      byte ptr [ebp + ebp*4 + 0x42], dl
0x45670F: add      ah, bh
0x456711: lodsb    al, byte ptr [esi]
0x456712: inc      edx
0x456713: add      byte ptr [eax - 0x29], cl
0x456716: inc      edx
0x456717: add      byte ptr [eax - 0x3fffbd0f], dh
0x45671D: rol      dword ptr [edx], cl
0x456720: mov      ah, 0xd6
0x456722: inc      edx
0x456723: add      byte ptr [eax], ah
0x456725: insd     dword ptr es:[edi], dx
0x456726: inc      ebp
0x456727: add      byte ptr [0xb5ffb600], al
0x45672D: push     dword ptr [edi + edi*8 - 0x4e004e]
0x456734: sbb      byte ptr [edi + 0x45], bh
0x456737: add      byte ptr [eax - 0x6fffba82], ah
0x45673D: jg       0x456784
0x45673F: add      byte ptr [eax + eax*4], bh
0x456742: inc      ebp
0x456743: add      byte ptr [eax - 0x80], bl
0x456746: inc      ebp
0x456747: add      byte ptr [esi], al
0x456749: push     esp
0x45674A: inc      esp
0x45674B: inc      edi
0x45674C: jb       0x4567b7
0x45674E: nop      
0x456750: push     esp
0x456751: inc      ebp
0x456753: add      byte ptr [edi], al
0x456755: push     es
0x456756: push     esp
0x456757: inc      esp
0x456758: inc      edi
0x456759: jb       0x4567c4
0x45675B: insb     byte ptr es:[edi], dx
0x45675D: inc      bp
0x45675F: add      byte ptr [ecx + 0x230045], bl
0x456766: pop      es
0x456767: inc      esp
0x456768: push     edi
0x456769: imul     ebp, dword ptr [esi + 0x43], 0x86c74
0x456770: cmp      al, 0x10
0x456772: inc      eax
0x456773: add      byte ptr [eax + 2], ah
0x456776: add      bh, bh
0x456778: pushal   
0x456779: add      al, byte ptr [eax]
0x45677B: inc      dword ptr [ecx]
0x45677D: add      byte ptr [eax], al
0x45677F: add      byte ptr [eax], al
0x456781: add      byte ptr [eax], al
0x456783: add      byte ptr [eax], 0
0x456786: add      byte ptr [eax + 0x4308001b], al
0x45678C: outsd    dx, dword ptr [esi]
0x45678D: insb     byte ptr es:[edi], dx
0x45678E: inc      ebx
0x45678F: outsd    dx, dword ptr [esi]
0x456790: jne      0x456800
0x456792: je       0x4567d0 ; "ColWidth"
0x456794: adc      byte ptr [eax], al
0x456797: add      al, byte ptr fs:[eax]
0x45679A: jmp      dword ptr [edx + eax]
0x45679E: inc      dword ptr [ecx]
0x4567A0: add      byte ptr [eax], al
0x4567A2: add      byte ptr [eax], al
0x4567A4: add      byte ptr [eax], al
0x4567A6: add      byte ptr [eax], 0
0x4567A9: add      byte ptr [eax + 0x5208001c], al
0x4567AF: outsd    dx, dword ptr [esi]
0x4567B0: ja       0x4567f5
0x4567B2: outsd    dx, dword ptr [esi]
0x4567B3: jne      0x456823
0x4567B5: je       0x4567f3 ; "RowHeight"
0x4567B7: adc      byte ptr [eax], al
0x4567BA: push     0x68ff0002
0x4567BF: add      al, byte ptr [eax]
0x4567C1: inc      dword ptr [ecx]
0x4567C3: add      byte ptr [eax], al
0x4567C5: add      byte ptr [eax], al
0x4567C7: add      byte ptr [eax], al
0x4567C9: add      byte ptr [eax], 0
0x4567CC: add      byte ptr [eax + 0x4308001d], al
0x4567D2: outsd    dx, dword ptr [esi]
0x4567D3: insb     byte ptr es:[edi], dx
0x4567D4: push     edi
0x4567D5: imul     esp, dword ptr [esp + esi*2 + 0x68], 0x40103c
0x4567DD: insb     byte ptr es:[edi], dx
0x4567DE: add      al, byte ptr [eax]
0x4567E0: jmp      ptr [edx + eax]
0x4567E4: inc      dword ptr [ecx]
0x4567E6: add      byte ptr [eax], al
0x4567E8: add      byte ptr [eax], al
0x4567EA: add      byte ptr [eax], al
0x4567EC: add      byte ptr [eax], 0
0x4567EF: add      byte ptr [eax + 0x5209001e], al
0x4567F5: outsd    dx, dword ptr [esi]
0x4567F6: ja       0x456840
0x4567F8: imul     esp, dword ptr gs:[edi + 0x68], 0x40103c74
0x456800: add      byte ptr [eax + 2], dh
0x456803: add      bh, bh
0x456805: jo       0x456809
0x456807: add      bh, bh
0x456809: add      dword ptr [eax], eax
0x45680B: add      byte ptr [eax], al
0x45680D: add      byte ptr [eax], al
0x45680F: add      byte ptr [eax - 0x80000000], al
0x456815: pop      ds
0x456816: add      byte ptr [ebx], cl
0x456818: push     esi
0x456819: imul     esp, dword ptr [ebp + 0x77], 0x4c706f54
0x456820: imul     ebp, dword ptr [esi + 0x65], 0x455f30
0x456827: test     byte ptr [edx], al
0x456829: add      bh, bh
0x45682B: test     byte ptr [edx], al
0x45682D: add      bh, bh
0x45682F: add      dword ptr [eax], eax
0x456831: add      byte ptr [eax], al
0x456833: add      byte ptr [eax], al
0x456835: add      byte ptr [eax - 0x80000000], al
0x45683B: and      byte ptr [eax], al
0x45683D: or       al, 0x4f
0x45683F: outsb    dx, byte ptr [esi]
0x456840: inc      edi
0x456841: jb       0x4568ac
0x456843: push     ebx
0x456845: insb     byte ptr es:[edi], dx
0x456847: arpl     word ptr gs:[eax + esi + 0x5f], si
0x45684C: inc      ebp
0x45684D: add      byte ptr [edx + eax + 0x28cff00], cl
0x456854: add      bh, bh
0x456856: add      dword ptr [eax], eax
0x456858: add      byte ptr [eax], al
0x45685A: add      byte ptr [eax], al
0x45685C: add      byte ptr [eax - 0x80000000], al
0x456862: and      dword ptr [eax], eax
0x456864: cmovg    ebp, dword ptr [esi + 0x47]
0x456868: jb       0x4568d3
0x45686A: dec      ebp
0x45686C: outsd    dx, dword ptr [esi]
0x45686D: jne      0x4568e2
0x45686F: dec      ebp
0x456871: outsd    dx, dword ptr [esi]
0x456872: jbe      0x4568d9
0x456874: test     byte ptr [edi + 0x45], bl
0x456877: add      byte ptr [edx + eax + 0x294ff00], dl
0x45687E: add      bh, bh
0x456880: add      dword ptr [eax], eax
0x456882: add      byte ptr [eax], al
0x456884: add      byte ptr [eax], al
0x456886: add      byte ptr [eax - 0x80000000], al
0x45688C: and      al, byte ptr [eax]
0x45688E: or       ecx, dword ptr [edi + 0x6e]
0x456891: inc      edi
0x456892: jb       0x4568fd
0x456894: push     eax
0x456896: popal    
0x456897: imul     ebp, dword ptr [esi + 0x74], 0x68e8c08b
0x45689E: inc      ebp
0x45689F: add      byte ptr [eax], al
0x4568A1: add      byte ptr [eax], al
0x4568A3: add      byte ptr [eax], al
0x4568A5: add      byte ptr [eax], al
0x4568A7: add      byte ptr [eax], al
0x4568A9: add      byte ptr [eax], al
0x4568AB: add      al, cl
0x4568AD: imul     eax, dword ptr [ebp], 0
0x4568B4: add      byte ptr [eax], al
0x4568B6: add      byte ptr [eax], al
0x4568B8: movsb    byte ptr es:[edi], byte ptr [esi]
0x4568B9: imul     eax, dword ptr [ebp], 0x4569b8 ; "TDWindow"
0x4568C0: mov      byte ptr [edx], al
0x4568C2: add      byte ptr [eax], al
0x4568C4: push     0x9c004564
0x4568C9: push     eax
0x4568CA: inc      ecx
0x4568CB: add      ah, dl
0x4568CD: xor      byte ptr [eax], al
0x4568D0: fdiv     dword ptr [eax]
0x4568D2: inc      eax
0x4568D3: add      ah, bl
0x4568D5: xor      byte ptr [eax], al
0x4568D8: les      esi, ptr [ebx + 0x2e400042]
0x4568DE: inc      eax
0x4568DF: add      byte ptr [esi + ebp + 0x40], dl
0x4568E3: add      byte ptr [esp + ebp*2 + 0x45], bh
0x4568E7: add      ah, ch
0x4568E9: loop     0x45692d
0x4568EB: add      byte ptr [esp + ecx*4 - 0x243ffbe], bl
0x4568F2: inc      eax
0x4568F3: add      byte ptr [eax], cl
0x4568F5: outsd    dx, dword ptr [esi]
0x4568F6: inc      ebp
0x4568F7: add      byte ptr [eax], cl
0x4568F9: jo       0x45693d
0x4568FB: add      byte ptr [eax], dh
0x4568FD: popfd    
0x4568FE: inc      edx
0x4568FF: add      byte ptr [edi + esi*2 + 0x42], dl
0x456903: add      byte ptr [eax + edx*2 + 0x4cf80041], dh
0x45690A: inc      ecx
0x45690B: add      al, dh
0x45690D: dec      esp
0x45690E: inc      ecx
0x45690F: add      byte ptr [eax + edx*2 - 0x7e63ffbf], bh
0x456916: inc      ebp
0x456917: add      al, al
0x456919: fld      dword ptr [edx]
0x45691C: hlt      
0x45691D: fld      qword ptr [edx]
0x456920: sbb      al, 0xe3
0x456922: inc      edx
0x456923: add      byte ptr [eax - 0x1e], cl
0x456926: inc      edx
0x456927: add      ah, bl
0x456929: xlatb    
0x45692A: inc      edx
0x45692B: add      ah, bh
0x45692D: xlatb    
0x45692E: inc      edx
0x45692F: add      byte ptr [eax], dl
0x456931: aad      0x42
0x456933: add      ah, ah
0x456935: insd     dword ptr es:[edi], dx
0x456936: inc      edx
0x456937: add      al, ch
0x456939: insd     dword ptr es:[edi], dx
0x45693A: inc      edx
0x45693B: add      byte ptr [ecx + ebx*4], dh
0x45693E: inc      edx
0x45693F: add      byte ptr [eax - 0x67], ah
0x456942: inc      edx
0x456943: add      byte ptr [eax + 0x6f], al
0x456946: inc      edx
0x456947: add      byte ptr [ecx + edi*2], al
0x45694A: inc      edx
0x45694B: add      byte ptr [eax + edi*2], bh
0x45694E: inc      edx
0x45694F: add      byte ptr [eax + 0x6400427b], ah
0x456955: jp       0x456999
0x456957: add      byte ptr [eax], cl
0x456959: mov      dl, 0x42
0x45695B: add      ah, ah
0x45695D: cdq      
0x45695E: inc      edx
0x45695F: add      byte ptr [eax + 0x240042d5], ch
0x456965: salc     
0x456966: inc      edx
0x456967: add      byte ptr [eax - 0x2f], dl
0x45696A: inc      edx
0x45696B: add      byte ptr [eax], cl
0x45696D: salc     
0x45696E: inc      edx
0x45696F: add      byte ptr [eax - 0x7fffbd63], ch
0x456975: mov      byte ptr [0xadf80042], al
0x45697A: inc      edx
0x45697B: add      byte ptr [edx + ebp*4], bl
0x45697E: inc      edx
0x45697F: add      byte ptr [eax + 0x4c0042ac], bh
0x456985: stosd    dword ptr es:[edi], eax
0x456986: inc      edx
0x456987: add      byte ptr [ebp + ebp*4 + 0x42], dl
0x45698B: add      ah, bh
0x45698D: lodsb    al, byte ptr [esi]
0x45698E: inc      edx
0x45698F: add      byte ptr [eax - 0x29], cl
0x456992: inc      edx
0x456993: add      byte ptr [eax - 0x3fffbd0f], dh
0x456999: rol      dword ptr [edx], cl
0x45699C: mov      ah, 0xd6
0x45699E: inc      edx
0x45699F: add      byte ptr [eax], ah
0x4569A1: insd     dword ptr es:[edi], dx
0x4569A2: inc      ebp
0x4569A3: add      byte ptr [ebx], al
0x4569A5: add      byte ptr [esi - 0x4b004a01], dh
0x4569AB: call     ptr [edx + eax*4 + 0x45]
0x4569AF: add      byte ptr [eax + 0x8004583], dl
0x4569B5: test     byte ptr [ebp], al
0x4569B8: or       byte ptr [esp + eax*2 + 0x57], dl
0x4569BC: imul     ebp, dword ptr [esi + 0x64], 0x408d776f
0x4569C3: add      al, cl
0x4569C5: imul     eax, dword ptr [ebp], 0x44540807
0x4569CC: push     edi
0x4569CD: imul     ebp, dword ptr [esi + 0x64], 0x68e8776f
0x4569D4: inc      ebp
0x4569D5: add      byte ptr [eax + 0x1e004565], dl
0x4569DB: add      byte ptr [edi], al
0x4569DD: inc      esp
0x4569DE: push     edi
0x4569DF: imul     ebp, dword ptr [esi + 0x43], 0x26c74
0x4569E6: add      byte ptr [eax], dl
0x4569E8: inc      eax
0x4569E9: add      byte ptr [edx + eax], cl
0x4569ED: call     dword ptr [edx + eax*4]
0x4569F0: inc      ebp
0x4569F1: add      byte ptr [ecx], al
0x4569F3: add      byte ptr [eax], al
0x4569F5: add      byte ptr [eax], al
0x4569F7: add      byte ptr [eax], al
0x4569F9: add      byte ptr [eax], 0
0x4569FC: add      byte ptr [eax + 0x56070015], al
0x456A02: imul     esi, dword ptr [ebx + 0x69], 0x656c62
0x456A09: adc      byte ptr [eax], al
0x456A0C: js       0x456a10
0x456A0E: add      bh, bh
0x456A10: js       0x456a14
0x456A12: add      bh, bh
0x456A14: add      dword ptr [eax], eax
0x456A16: add      byte ptr [eax], al
0x456A18: add      byte ptr [eax], al
0x456A1A: add      byte ptr [eax - 0x80000000], al
0x456A20: sbb      eax, 0x6c460800
0x456A25: outsd    dx, dword ptr [esi]
0x456A26: popal    
0x456A27: je       0x456a92
0x456A29: outsb    dx, byte ptr [esi]
0x456A2A: nop      
0x456A2C: js       0x456a98
0x456A2E: inc      ebp
0x456A2F: add      byte ptr [eax], al
0x456A31: add      byte ptr [eax], al
0x456A33: add      byte ptr [eax], al
0x456A35: add      byte ptr [eax], al
0x456A37: add      byte ptr [eax], al
0x456A39: add      byte ptr [eax], al
0x456A3B: add      byte ptr [edx + ebp*2 + 0x45], bh
0x456A42: add      byte ptr [eax], al
0x456A44: add      byte ptr [eax], al
0x456A46: add      byte ptr [eax], al
0x456A48: add      byte ptr [eax], al
0x456A4A: add      byte ptr [eax], al
0x456A4C: test     al, 0x6a
0x456A4E: inc      ebp
0x456A4F: add      byte ptr [eax], ch
0x456A51: add      byte ptr [eax], al
0x456A53: add      al, dl
0x456A55: jmp      0x50e16a9a
0x456A5A: inc      ecx
0x456A5B: add      ah, dl
0x456A5D: xor      byte ptr [eax], al
0x456A60: fdiv     dword ptr [eax]
0x456A62: inc      eax
0x456A63: add      ah, bl
0x456A65: xor      byte ptr [eax], al
0x456A68: sal      byte ptr [eax], 1
0x456A6A: inc      eax
0x456A6B: add      byte ptr [eax + 0x2e], al
0x456A6E: inc      eax
0x456A6F: add      byte ptr [esi + ebp + 0x40], dl
0x456A73: add      ah, bl
0x456A75: test     byte ptr [ebp], al
0x456A78: sar      dh, 0x40
0x456A7B: add      byte ptr [eax], cl
0x456A7D: dec      esp
0x456A7E: inc      ecx
0x456A7F: add      byte ptr [ebp + edi*8 + 0x4cdc0040], bh
0x456A86: inc      ecx
0x456A87: add      byte ptr [ebx + ecx*2 + 0x4ce80041], bl
0x456A8E: inc      ecx
0x456A8F: add      byte ptr [esi + ecx*2], dl
0x456A92: inc      ecx
0x456A93: add      byte ptr [eax + edx*2 + 0x4cf80041], dh
0x456A9A: inc      ecx
0x456A9B: add      al, dh
0x456A9D: dec      esp
0x456A9E: inc      ecx
0x456A9F: add      byte ptr [eax + edx*2 - 0x7b8bffbf], bh
0x456AA6: inc      ebp
0x456AA7: add      byte ptr [esp + edx*2], cl
0x456AAA: inc      esp
0x456AAB: push     edi
0x456AAC: imul     ebp, dword ptr [esi + 0x4d], 0x67616e61
0x456AB3: jb       0x456a43
0x456AB6: inc      eax
0x456AB7: add      byte ptr [edx + ebp*2 + 0xc070045], bh
0x456ABE: push     esp
0x456ABF: inc      esp
0x456AC0: push     edi
0x456AC1: imul     ebp, dword ptr [esi + 0x4d], 0x67616e61
0x456AC8: jb       0x456b43
0x456ACB: push     0x45
0x456ACD: add      al, cl
0x456ACF: ljmp     0x4407:0x20040
0x456AD6: push     edi
0x456AD7: imul     ebp, dword ptr [esi + 0x43], 0x6c74
0x456ADE: mov      eax, eax
0x456AE0: push     ebp
0x456AE1: mov      ebp, esp
0x456AE3: push     ecx
0x456AE4: mov      dword ptr [ebp - 4], eax
0x456AE7: mov      eax, dword ptr [ebp - 4]
0x456AEA: mov      dword ptr [0x4f7ba0], eax
0x456AEF: pop      ecx
0x456AF0: pop      ebp
0x456AF1: ret      
0x456AF2: mov      eax, eax
0x456AF4: xor      eax, eax
0x456AF6: mov      dword ptr [0x4f7ba0], eax
0x456AFB: ret      
0x456AFC: push     ebp
0x456AFD: mov      ebp, esp
0x456AFF: push     ecx
0x456B00: mov      dword ptr [ebp - 4], eax
0x456B03: mov      eax, dword ptr [0x4f7ba4]
0x456B08: push     eax
0x456B09: call     0x40685c
0x456B0E: mov      eax, dword ptr [ebp - 4]
0x456B11: mov      dword ptr [0x4f7b9c], eax
0x456B16: pop      ecx
0x456B17: pop      ebp
0x456B18: ret      
0x456B19: lea      eax, [eax]
0x456B1C: call     0x40681c
0x456B21: xor      eax, eax
0x456B23: mov      dword ptr [0x4f7b9c], eax
0x456B28: ret      
0x456B29: lea      eax, [eax]
0x456B2C: push     ebp
0x456B2D: mov      ebp, esp
0x456B2F: add      esp, -0xc
0x456B32: test     dl, dl
0x456B34: je       0x456b3e
0x456B36: add      esp, -0x10
0x456B39: call     0x4031a4
0x456B3E: mov      dword ptr [ebp - 0xc], ecx
0x456B41: mov      byte ptr [ebp - 5], dl
0x456B44: mov      dword ptr [ebp - 4], eax
0x456B47: mov      ecx, dword ptr [ebp - 0xc]
0x456B4A: xor      edx, edx
0x456B4C: mov      eax, dword ptr [ebp - 4]
0x456B4F: call     0x42f104
0x456B54: mov      eax, dword ptr [ebp - 4]
0x456B57: xor      edx, edx
0x456B59: mov      dword ptr [eax + 0x1f4], edx
0x456B5F: xor      edx, edx
0x456B61: mov      eax, dword ptr [ebp - 4]
0x456B64: call     0x4278c0
0x456B69: mov      eax, dword ptr [ebp - 4]
0x456B6C: mov      byte ptr [eax + 0x1f8], 0
0x456B73: mov      eax, dword ptr [ebp - 4]
0x456B76: mov      byte ptr [eax + 0x24d], 0
0x456B7D: mov      eax, dword ptr [ebp - 4]
0x456B80: xor      edx, edx
0x456B82: mov      dword ptr [eax + 0x1fc], edx
0x456B88: mov      dword ptr [eax + 0x200], edx
0x456B8E: mov      eax, dword ptr [ebp - 4]
0x456B91: xor      edx, edx
0x456B93: mov      dword ptr [eax + 0x204], edx
0x456B99: mov      dword ptr [eax + 0x208], edx
0x456B9F: mov      eax, dword ptr [ebp - 4]
0x456BA2: xor      edx, edx
0x456BA4: mov      dword ptr [eax + 0x20c], edx
0x456BAA: mov      dword ptr [eax + 0x210], edx
0x456BB0: mov      eax, dword ptr [ebp - 4]
0x456BB3: xor      edx, edx
0x456BB5: mov      dword ptr [eax + 0x214], edx
0x456BBB: mov      dword ptr [eax + 0x218], edx
0x456BC1: mov      eax, dword ptr [ebp - 4]
0x456BC4: xor      edx, edx
0x456BC6: mov      dword ptr [eax + 0x21c], edx
0x456BCC: mov      dword ptr [eax + 0x220], edx
0x456BD2: mov      eax, dword ptr [ebp - 4]
0x456BD5: xor      edx, edx
0x456BD7: mov      dword ptr [eax + 0x224], edx
0x456BDD: mov      dword ptr [eax + 0x228], edx
0x456BE3: mov      eax, dword ptr [ebp - 4]
0x456BE6: xor      edx, edx
0x456BE8: mov      dword ptr [eax + 0x23c], edx
0x456BEE: mov      dword ptr [eax + 0x240], edx
0x456BF4: mov      dl, 1
0x456BF6: mov      eax, dword ptr [0x40df00]
0x456BFB: call     0x402e7c
0x456C00: mov      edx, dword ptr [ebp - 4]
0x456C03: mov      dword ptr [edx + 0x250], eax
0x456C09: mov      eax, dword ptr [ebp - 4]
0x456C0C: xor      edx, edx
0x456C0E: mov      dword ptr [eax + 0x1f4], edx
0x456C14: mov      edx, 0x50
0x456C19: mov      eax, dword ptr [ebp - 4]
0x456C1C: call     0x4271d8
0x456C21: mov      edx, 0x18
0x456C26: mov      eax, dword ptr [ebp - 4]
0x456C29: call     0x4271fc
0x456C2E: mov      eax, dword ptr [ebp - 4]
0x456C31: add      eax, 0x1f0
0x456C36: call     0x403a58
0x456C3B: mov      eax, dword ptr [ebp - 4]
0x456C3E: mov      byte ptr [eax + 0x24c], 1
0x456C45: mov      eax, dword ptr [ebp - 4]
0x456C48: xor      edx, edx
0x456C4A: mov      dword ptr [eax + 0x254], edx
0x456C50: mov      eax, dword ptr [ebp - 4]
0x456C53: xor      edx, edx
0x456C55: mov      dword ptr [eax + 0x258], edx
0x456C5B: mov      eax, dword ptr [ebp - 4]
0x456C5E: cmp      byte ptr [ebp - 5], 0
0x456C62: je       0x456c73
0x456C64: call     0x4031fc
0x456C69: pop      dword ptr fs:[0]
0x456C70: add      esp, 0xc
0x456C73: mov      eax, dword ptr [ebp - 4]
0x456C76: mov      esp, ebp
0x456C78: pop      ebp
0x456C79: ret      
0x456C7A: mov      eax, eax
0x456C7C: push     ebp
0x456C7D: mov      ebp, esp
0x456C7F: add      esp, -8
0x456C82: call     0x403204
0x456C87: mov      byte ptr [ebp - 5], dl
0x456C8A: mov      dword ptr [ebp - 4], eax
0x456C8D: mov      eax, dword ptr [ebp - 4]
0x456C90: mov      eax, dword ptr [eax + 0x250]
0x456C96: call     0x402eac
0x456C9B: mov      dl, byte ptr [ebp - 5]
0x456C9E: and      dl, 0xfc
0x456CA1: mov      eax, dword ptr [ebp - 4]
0x456CA4: call     0x42f160
0x456CA9: cmp      byte ptr [ebp - 5], 0
0x456CAD: jle      0x456cb7
0x456CAF: mov      eax, dword ptr [ebp - 4]
0x456CB2: call     0x4031f4
0x456CB7: pop      ecx
0x456CB8: pop      ecx
0x456CB9: pop      ebp
0x456CBA: ret      
0x456CBB: nop      
0x456CBC: push     ebp
0x456CBD: mov      ebp, esp
0x456CBF: add      esp, -8
0x456CC2: mov      dword ptr [ebp - 8], edx
0x456CC5: mov      dword ptr [ebp - 4], eax
0x456CC8: mov      eax, dword ptr [ebp - 8]
0x456CCB: call     0x403e88
0x456CD0: xor      eax, eax
0x456CD2: push     ebp
0x456CD3: push     0x456d15
0x456CD8: push     dword ptr fs:[eax]
0x456CDB: mov      dword ptr fs:[eax], esp
0x456CDE: mov      eax, dword ptr [ebp - 4]
0x456CE1: add      eax, 0x1f0
0x456CE6: mov      edx, dword ptr [ebp - 8]
0x456CE9: call     0x403aac
0x456CEE: mov      eax, dword ptr [ebp - 4]
0x456CF1: test     byte ptr [eax + 0x20], 0x10
0x456CF5: je       0x456cff
0x456CF7: mov      eax, dword ptr [ebp - 4]
0x456CFA: call     0x427eac
0x456CFF: xor      eax, eax
0x456D01: pop      edx
0x456D02: pop      ecx
0x456D03: pop      ecx
0x456D04: mov      dword ptr fs:[eax], edx
0x456D07: push     0x456d1c
0x456D0C: lea      eax, [ebp - 8]
0x456D0F: call     0x403a58
0x456D14: ret      
0x456D15: jmp      0x403518
0x456D1A: jmp      0x456d0c
0x456D1C: pop      ecx
0x456D1D: pop      ecx
0x456D1E: pop      ebp
0x456D1F: ret      
0x456D20: push     ebp
0x456D21: mov      ebp, esp
0x456D23: add      esp, -0xc
0x456D26: mov      dword ptr [ebp - 4], eax
0x456D29: mov      eax, dword ptr [ebp - 4]
0x456D2C: test     byte ptr [eax + 0x20], 0x10
0x456D30: je       0x456f03
0x456D36: mov      eax, dword ptr [ebp - 4]
0x456D39: mov      edx, dword ptr [0x45689c]
0x456D3F: call     0x40301c
0x456D44: test     al, al
0x456D46: je       0x456e46
0x456D4C: mov      eax, dword ptr [ebp - 4]
0x456D4F: mov      eax, dword ptr [eax + 0x1ec]
0x456D55: mov      dword ptr [ebp - 8], eax
0x456D58: xor      edx, edx
0x456D5A: mov      eax, dword ptr [ebp - 8]
0x456D5D: mov      eax, dword ptr [eax + 0x10]
0x456D60: call     0x4178dc
0x456D65: xor      ecx, ecx
0x456D67: xor      edx, edx
0x456D69: mov      eax, dword ptr [ebp - 8]
0x456D6C: call     0x418000
0x456D71: mov      eax, dword ptr [ebp - 4]
0x456D74: mov      edx, dword ptr [eax + 0x38]
0x456D77: dec      edx
0x456D78: xor      ecx, ecx
0x456D7A: mov      eax, dword ptr [ebp - 8]
0x456D7D: call     0x417fa0
0x456D82: mov      eax, dword ptr [ebp - 4]
0x456D85: mov      ecx, dword ptr [eax + 0x3c]
0x456D88: dec      ecx
0x456D89: mov      eax, dword ptr [ebp - 4]
0x456D8C: mov      edx, dword ptr [eax + 0x38]
0x456D8F: dec      edx
0x456D90: mov      eax, dword ptr [ebp - 8]
0x456D93: call     0x417fa0
0x456D98: mov      eax, dword ptr [ebp - 4]
0x456D9B: mov      ecx, dword ptr [eax + 0x3c]
0x456D9E: dec      ecx
0x456D9F: xor      edx, edx
0x456DA1: mov      eax, dword ptr [ebp - 8]
0x456DA4: call     0x417fa0
0x456DA9: xor      ecx, ecx
0x456DAB: xor      edx, edx
0x456DAD: mov      eax, dword ptr [ebp - 8]
0x456DB0: call     0x417fa0
0x456DB5: mov      eax, dword ptr [ebp - 4]
0x456DB8: mov      ecx, dword ptr [eax + 0x3c]
0x456DBB: dec      ecx
0x456DBC: mov      eax, dword ptr [ebp - 4]
0x456DBF: mov      edx, dword ptr [eax + 0x38]
0x456DC2: dec      edx
0x456DC3: mov      eax, dword ptr [ebp - 8]
0x456DC6: call     0x417fa0
0x456DCB: mov      eax, dword ptr [ebp - 4]
0x456DCE: mov      edx, dword ptr [eax + 0x38]
0x456DD1: dec      edx
0x456DD2: xor      ecx, ecx
0x456DD4: mov      eax, dword ptr [ebp - 8]
0x456DD7: call     0x418000
0x456DDC: mov      eax, dword ptr [ebp - 4]
0x456DDF: mov      ecx, dword ptr [eax + 0x3c]
0x456DE2: dec      ecx
0x456DE3: xor      edx, edx
0x456DE5: mov      eax, dword ptr [ebp - 8]
0x456DE8: call     0x417fa0
0x456DED: mov      eax, dword ptr [ebp - 4]
0x456DF0: mov      eax, dword ptr [eax + 0x1f0]
0x456DF6: push     eax
0x456DF7: mov      eax, dword ptr [ebp - 4]
0x456DFA: mov      edx, dword ptr [eax + 0x1f0]
0x456E00: mov      eax, dword ptr [ebp - 8]
0x456E03: call     0x4181c8
0x456E08: mov      edx, dword ptr [ebp - 4]
0x456E0B: mov      edx, dword ptr [edx + 0x3c]
0x456E0E: sub      edx, eax
0x456E10: sar      edx, 1
0x456E12: jns      0x456e17
0x456E14: adc      edx, 0
0x456E17: push     edx
0x456E18: mov      eax, dword ptr [ebp - 4]
0x456E1B: mov      edx, dword ptr [eax + 0x1f0]
0x456E21: mov      eax, dword ptr [ebp - 8]
0x456E24: call     0x4181ac
0x456E29: mov      edx, dword ptr [ebp - 4]
0x456E2C: mov      edx, dword ptr [edx + 0x38]
0x456E2F: sub      edx, eax
0x456E31: sar      edx, 1
0x456E33: jns      0x456e38
0x456E35: adc      edx, 0
0x456E38: mov      eax, dword ptr [ebp - 8]
0x456E3B: pop      ecx
0x456E3C: call     0x4180dc
0x456E41: jmp      0x456f03
0x456E46: mov      eax, dword ptr [ebp - 4]
0x456E49: mov      eax, dword ptr [eax + 0x1ec]
0x456E4F: mov      dword ptr [ebp - 0xc], eax
0x456E52: mov      eax, dword ptr [ebp - 0xc]
0x456E55: mov      eax, dword ptr [eax + 0x10]
0x456E58: xor      edx, edx
0x456E5A: call     0x4178dc
0x456E5F: xor      ecx, ecx
0x456E61: xor      edx, edx
0x456E63: mov      eax, dword ptr [ebp - 0xc]
0x456E66: call     0x418000
0x456E6B: mov      eax, dword ptr [ebp - 4]
0x456E6E: mov      edx, dword ptr [eax + 0x38]
0x456E71: dec      edx
0x456E72: xor      ecx, ecx
0x456E74: mov      eax, dword ptr [ebp - 0xc]
0x456E77: call     0x417fa0
0x456E7C: mov      eax, dword ptr [ebp - 4]
0x456E7F: mov      ecx, dword ptr [eax + 0x3c]
0x456E82: dec      ecx
0x456E83: mov      eax, dword ptr [ebp - 4]
0x456E86: mov      edx, dword ptr [eax + 0x38]
0x456E89: dec      edx
0x456E8A: mov      eax, dword ptr [ebp - 0xc]
0x456E8D: call     0x417fa0
0x456E92: mov      eax, dword ptr [ebp - 4]
0x456E95: mov      ecx, dword ptr [eax + 0x3c]
0x456E98: dec      ecx
0x456E99: xor      edx, edx
0x456E9B: mov      eax, dword ptr [ebp - 0xc]
0x456E9E: call     0x417fa0
0x456EA3: xor      ecx, ecx
0x456EA5: xor      edx, edx
0x456EA7: mov      eax, dword ptr [ebp - 0xc]
0x456EAA: call     0x417fa0
0x456EAF: mov      eax, dword ptr [ebp - 4]
0x456EB2: mov      eax, dword ptr [eax + 0x1f0]
0x456EB8: push     eax
0x456EB9: mov      eax, dword ptr [ebp - 4]
0x456EBC: mov      edx, dword ptr [eax + 0x1f0]
0x456EC2: mov      eax, dword ptr [ebp - 0xc]
0x456EC5: call     0x4181c8
0x456ECA: mov      edx, dword ptr [ebp - 4]
0x456ECD: mov      edx, dword ptr [edx + 0x3c]
0x456ED0: sub      edx, eax
0x456ED2: sar      edx, 1
0x456ED4: jns      0x456ed9
0x456ED6: adc      edx, 0
0x456ED9: push     edx
0x456EDA: mov      eax, dword ptr [ebp - 4]
0x456EDD: mov      edx, dword ptr [eax + 0x1f0]
0x456EE3: mov      eax, dword ptr [ebp - 0xc]
0x456EE6: call     0x4181ac
0x456EEB: mov      edx, dword ptr [ebp - 4]
0x456EEE: mov      edx, dword ptr [edx + 0x38]
0x456EF1: sub      edx, eax
0x456EF3: sar      edx, 1
0x456EF5: jns      0x456efa
0x456EF7: adc      edx, 0
0x456EFA: mov      eax, dword ptr [ebp - 0xc]
0x456EFD: pop      ecx
0x456EFE: call     0x4180dc
0x456F03: mov      esp, ebp
0x456F05: pop      ebp
0x456F06: ret      
0x456F07: nop      
0x456F08: push     ebp
0x456F09: mov      ebp, esp
0x456F0B: add      esp, -0x10
0x456F0E: mov      dword ptr [ebp - 4], eax
0x456F11: mov      eax, dword ptr [ebp - 4]
0x456F14: test     byte ptr [eax + 0x20], 0x10
0x456F18: jne      0x456f8d
0x456F1A: mov      eax, dword ptr [ebp - 4]
0x456F1D: cmp      dword ptr [eax + 0x24], 0
0x456F21: je       0x456f8d
0x456F23: mov      eax, dword ptr [ebp - 4]
0x456F26: mov      eax, dword ptr [eax + 0x24]
0x456F29: call     0x414f60
0x456F2E: dec      eax
0x456F2F: test     eax, eax
0x456F31: jl       0x456f8d
0x456F33: inc      eax
0x456F34: mov      dword ptr [ebp - 0x10], eax
0x456F37: mov      dword ptr [ebp - 8], 0
0x456F3E: mov      edx, dword ptr [ebp - 8]
0x456F41: mov      eax, dword ptr [ebp - 4]
0x456F44: mov      eax, dword ptr [eax + 0x24]
0x456F47: call     0x414ef4
0x456F4C: mov      edx, dword ptr [0x456044]
0x456F52: call     0x40301c
0x456F57: test     al, al
0x456F59: je       0x456f85
0x456F5B: mov      edx, dword ptr [ebp - 8]
0x456F5E: mov      eax, dword ptr [ebp - 4]
0x456F61: mov      eax, dword ptr [eax + 0x24]
0x456F64: call     0x414ef4
0x456F69: mov      dword ptr [ebp - 0xc], eax
0x456F6C: mov      eax, dword ptr [ebp - 0xc]
0x456F6F: mov      eax, dword ptr [eax + 0x1f4]
0x456F75: cmp      eax, dword ptr [ebp - 4]
0x456F78: jne      0x456f85
0x456F7A: mov      edx, dword ptr [ebp - 0xc]
0x456F7D: mov      eax, dword ptr [ebp - 4]
0x456F80: call     0x4570b4
0x456F85: inc      dword ptr [ebp - 8]
0x456F88: dec      dword ptr [ebp - 0x10]
0x456F8B: jne      0x456f3e
0x456F8D: mov      esp, ebp
0x456F8F: pop      ebp
0x456F90: ret      
0x456F91: lea      eax, [eax]
0x456F94: push     ebp
0x456F95: mov      ebp, esp
0x456F97: add      esp, -0x10
0x456F9A: mov      dword ptr [ebp - 8], edx
0x456F9D: mov      dword ptr [ebp - 4], eax
0x456FA0: mov      eax, dword ptr [ebp - 4]
0x456FA3: mov      dword ptr [ebp - 0x10], eax
0x456FA6: mov      eax, dword ptr [ebp - 0x10]
0x456FA9: cmp      dword ptr [eax + 0x1f4], 0
0x456FB0: je       0x456fcf
0x456FB2: mov      eax, dword ptr [ebp - 0x10]
0x456FB5: mov      eax, dword ptr [eax + 0x1f4]
0x456FBB: mov      eax, dword ptr [eax + 0x30]
0x456FBE: add      dword ptr [ebp - 8], eax
0x456FC1: mov      eax, dword ptr [ebp - 0x10]
0x456FC4: mov      eax, dword ptr [eax + 0x1f4]
0x456FCA: mov      dword ptr [ebp - 0x10], eax
0x456FCD: jmp      0x456fa6
0x456FCF: mov      eax, dword ptr [ebp - 8]
0x456FD2: mov      dword ptr [ebp - 0xc], eax
0x456FD5: mov      eax, dword ptr [ebp - 0xc]
0x456FD8: mov      esp, ebp
0x456FDA: pop      ebp
0x456FDB: ret      
0x456FDC: push     ebp
0x456FDD: mov      ebp, esp
0x456FDF: add      esp, -0x10
0x456FE2: mov      dword ptr [ebp - 8], edx
0x456FE5: mov      dword ptr [ebp - 4], eax
0x456FE8: mov      eax, dword ptr [ebp - 4]
0x456FEB: mov      dword ptr [ebp - 0x10], eax
0x456FEE: mov      eax, dword ptr [ebp - 0x10]
0x456FF1: cmp      dword ptr [eax + 0x1f4], 0
0x456FF8: je       0x457017
0x456FFA: mov      eax, dword ptr [ebp - 0x10]
0x456FFD: mov      eax, dword ptr [eax + 0x1f4]
0x457003: mov      eax, dword ptr [eax + 0x34]
0x457006: add      dword ptr [ebp - 8], eax
0x457009: mov      eax, dword ptr [ebp - 0x10]
0x45700C: mov      eax, dword ptr [eax + 0x1f4]
0x457012: mov      dword ptr [ebp - 0x10], eax
0x457015: jmp      0x456fee
0x457017: mov      eax, dword ptr [ebp - 8]
0x45701A: mov      dword ptr [ebp - 0xc], eax
0x45701D: mov      eax, dword ptr [ebp - 0xc]
0x457020: mov      esp, ebp
0x457022: pop      ebp
0x457023: ret      
0x457024: push     ebp
0x457025: mov      ebp, esp
0x457027: add      esp, -0x10
0x45702A: mov      dword ptr [ebp - 8], edx
0x45702D: mov      dword ptr [ebp - 4], eax
0x457030: mov      eax, dword ptr [ebp - 4]
0x457033: mov      dword ptr [ebp - 0x10], eax
0x457036: mov      eax, dword ptr [ebp - 0x10]
0x457039: cmp      dword ptr [eax + 0x1f4], 0
0x457040: je       0x45705f
0x457042: mov      eax, dword ptr [ebp - 0x10]
0x457045: mov      eax, dword ptr [eax + 0x1f4]
0x45704B: mov      eax, dword ptr [eax + 0x30]
0x45704E: sub      dword ptr [ebp - 8], eax
0x457051: mov      eax, dword ptr [ebp - 0x10]
0x457054: mov      eax, dword ptr [eax + 0x1f4]
0x45705A: mov      dword ptr [ebp - 0x10], eax
0x45705D: jmp      0x457036
0x45705F: mov      eax, dword ptr [ebp - 8]
0x457062: mov      dword ptr [ebp - 0xc], eax
0x457065: mov      eax, dword ptr [ebp - 0xc]
0x457068: mov      esp, ebp
0x45706A: pop      ebp
0x45706B: ret      
0x45706C: push     ebp
0x45706D: mov      ebp, esp
0x45706F: add      esp, -0x10
0x457072: mov      dword ptr [ebp - 8], edx
0x457075: mov      dword ptr [ebp - 4], eax
0x457078: mov      eax, dword ptr [ebp - 4]
0x45707B: mov      dword ptr [ebp - 0x10], eax
0x45707E: mov      eax, dword ptr [ebp - 0x10]
0x457081: cmp      dword ptr [eax + 0x1f4], 0
0x457088: je       0x4570a7
0x45708A: mov      eax, dword ptr [ebp - 0x10]
0x45708D: mov      eax, dword ptr [eax + 0x1f4]
0x457093: mov      eax, dword ptr [eax + 0x34]
0x457096: sub      dword ptr [ebp - 8], eax
0x457099: mov      eax, dword ptr [ebp - 0x10]
0x45709C: mov      eax, dword ptr [eax + 0x1f4]
0x4570A2: mov      dword ptr [ebp - 0x10], eax
0x4570A5: jmp      0x45707e
0x4570A7: mov      eax, dword ptr [ebp - 8]
0x4570AA: mov      dword ptr [ebp - 0xc], eax
0x4570AD: mov      eax, dword ptr [ebp - 0xc]
0x4570B0: mov      esp, ebp
0x4570B2: pop      ebp
0x4570B3: ret      
0x4570B4: push     ebp
0x4570B5: mov      ebp, esp
0x4570B7: add      esp, -8
0x4570BA: mov      dword ptr [ebp - 8], edx
0x4570BD: mov      dword ptr [ebp - 4], eax
0x4570C0: mov      edx, dword ptr [ebp - 8]
0x4570C3: mov      eax, dword ptr [ebp - 4]
0x4570C6: mov      eax, dword ptr [eax + 0x250]
0x4570CC: call     0x40f588
0x4570D1: pop      ecx
0x4570D2: pop      ecx
0x4570D3: pop      ebp
0x4570D4: ret      
0x4570D5: lea      eax, [eax]
0x4570D8: push     ebp
0x4570D9: mov      ebp, esp
0x4570DB: add      esp, -0x10
0x4570DE: mov      dword ptr [ebp - 8], edx
0x4570E1: mov      dword ptr [ebp - 4], eax
0x4570E4: mov      eax, dword ptr [ebp - 8]
0x4570E7: mov      edx, dword ptr [0x45689c]
0x4570ED: call     0x40301c
0x4570F2: test     al, al
0x4570F4: je       0x457160
0x4570F6: mov      eax, dword ptr [ebp - 8]
0x4570F9: cmp      byte ptr [eax + 0x278], 0
0x457100: je       0x457160
0x457102: mov      eax, dword ptr [ebp - 4]
0x457105: mov      eax, dword ptr [eax + 0x250]
0x45710B: mov      eax, dword ptr [eax + 8]
0x45710E: dec      eax
0x45710F: test     eax, eax
0x457111: jl       0x45714f
0x457113: inc      eax
0x457114: mov      dword ptr [ebp - 0x10], eax
0x457117: mov      dword ptr [ebp - 0xc], 0
0x45711E: mov      eax, dword ptr [ebp - 4]
0x457121: mov      eax, dword ptr [eax + 0x250]
0x457127: mov      edx, dword ptr [ebp - 0xc]
0x45712A: call     0x40f6a0
0x45712F: cmp      eax, dword ptr [ebp - 8]
0x457132: jne      0x457147
0x457134: mov      eax, dword ptr [ebp - 4]
0x457137: mov      eax, dword ptr [eax + 0x250]
0x45713D: mov      edx, dword ptr [ebp - 0xc]
0x457140: call     0x40f5c4
0x457145: jmp      0x45714f
0x457147: inc      dword ptr [ebp - 0xc]
0x45714A: dec      dword ptr [ebp - 0x10]
0x45714D: jne      0x45711e
0x45714F: mov      eax, dword ptr [ebp - 4]
0x457152: mov      eax, dword ptr [eax + 0x250]
0x457158: mov      edx, dword ptr [ebp - 8]
0x45715B: call     0x40f588
0x457160: mov      esp, ebp
0x457162: pop      ebp
0x457163: ret      
0x457164: push     ebp
0x457165: mov      ebp, esp
0x457167: add      esp, -0x14
0x45716A: push     ebx
0x45716B: mov      dword ptr [ebp - 0xc], ecx
0x45716E: mov      dword ptr [ebp - 8], edx
0x457171: mov      dword ptr [ebp - 4], eax
0x457174: mov      eax, dword ptr [ebp - 8]
0x457177: mov      edx, dword ptr [ebp - 4]
0x45717A: cmp      eax, dword ptr [edx + 0x30]
0x45717D: jl       0x45724e
0x457183: mov      eax, dword ptr [ebp - 4]
0x457186: mov      eax, dword ptr [eax + 0x30]
0x457189: mov      edx, dword ptr [ebp - 4]
0x45718C: add      eax, dword ptr [edx + 0x38]
0x45718F: cmp      eax, dword ptr [ebp - 8]
0x457192: jle      0x45724e
0x457198: mov      eax, dword ptr [ebp - 0xc]
0x45719B: mov      edx, dword ptr [ebp - 4]
0x45719E: cmp      eax, dword ptr [edx + 0x34]
0x4571A1: jl       0x45724e
0x4571A7: mov      eax, dword ptr [ebp - 4]
0x4571AA: mov      eax, dword ptr [eax + 0x34]
0x4571AD: mov      edx, dword ptr [ebp - 4]
0x4571B0: add      eax, dword ptr [edx + 0x3c]
0x4571B3: cmp      eax, dword ptr [ebp - 0xc]
0x4571B6: jle      0x45724e
0x4571BC: mov      byte ptr [ebp - 0xe], 1
0x4571C0: mov      eax, dword ptr [ebp - 4]
0x4571C3: cmp      word ptr [eax + 0x23e], 0
0x4571CB: je       0x4571f8
0x4571CD: mov      eax, dword ptr [ebp - 0xc]
0x4571D0: mov      edx, dword ptr [ebp - 4]
0x4571D3: sub      eax, dword ptr [edx + 0x34]
0x4571D6: push     eax
0x4571D7: lea      eax, [ebp - 0xe]
0x4571DA: push     eax
0x4571DB: mov      ecx, dword ptr [ebp - 8]
0x4571DE: mov      eax, dword ptr [ebp - 4]
0x4571E1: sub      ecx, dword ptr [eax + 0x30]
0x4571E4: mov      ebx, dword ptr [ebp - 4]
0x4571E7: mov      edx, dword ptr [ebp - 4]
0x4571EA: mov      eax, dword ptr [ebx + 0x240]
0x4571F0: call     dword ptr [ebx + 0x23c]
0x4571F6: jmp      0x457246
0x4571F8: mov      eax, dword ptr [ebp - 4]
0x4571FB: cmp      dword ptr [eax + 0x254], 0
0x457202: je       0x457246
0x457204: mov      eax, dword ptr [ebp - 4]
0x457207: mov      edx, dword ptr [eax + 0x258]
0x45720D: mov      eax, dword ptr [ebp - 4]
0x457210: mov      eax, dword ptr [eax + 0x254]
0x457216: call     0x45351c
0x45721B: mov      dword ptr [ebp - 0x14], eax
0x45721E: cmp      dword ptr [ebp - 0x14], 0
0x457222: je       0x457246
0x457224: mov      ecx, dword ptr [ebp - 0xc]
0x457227: mov      eax, dword ptr [ebp - 4]
0x45722A: sub      ecx, dword ptr [eax + 0x34]
0x45722D: mov      edx, dword ptr [ebp - 8]
0x457230: mov      eax, dword ptr [ebp - 4]
0x457233: sub      edx, dword ptr [eax + 0x30]
0x457236: mov      eax, dword ptr [ebp - 0x14]
0x457239: call     0x44da34
0x45723E: test     eax, eax
0x457240: jg       0x457246
0x457242: mov      byte ptr [ebp - 0xe], 0
0x457246: mov      al, byte ptr [ebp - 0xe]
0x457249: mov      byte ptr [ebp - 0xd], al
0x45724C: jmp      0x457252
0x45724E: mov      byte ptr [ebp - 0xd], 0
0x457252: mov      al, byte ptr [ebp - 0xd]
0x457255: pop      ebx
0x457256: mov      esp, ebp
0x457258: pop      ebp
0x457259: ret      
0x45725A: mov      eax, eax
0x45725C: push     ebp
0x45725D: mov      ebp, esp
0x45725F: add      esp, -0x10
0x457262: push     ebx
0x457263: mov      dword ptr [ebp - 8], edx
0x457266: mov      dword ptr [ebp - 4], eax
0x457269: mov      byte ptr [ebp - 9], 0
0x45726D: mov      eax, dword ptr [ebp - 4]
0x457270: cmp      byte ptr [eax + 0x24d], 0
0x457277: jne      0x45730c
0x45727D: mov      eax, dword ptr [ebp - 4]
0x457280: mov      eax, dword ptr [eax + 0x250]
0x457286: mov      eax, dword ptr [eax + 8]
0x457289: dec      eax
0x45728A: cmp      eax, 0
0x45728D: jl       0x4572dc
0x45728F: mov      dword ptr [ebp - 0x10], eax
0x457292: mov      edx, dword ptr [ebp - 0x10]
0x457295: mov      eax, dword ptr [ebp - 4]
0x457298: mov      eax, dword ptr [eax + 0x250]
0x45729E: call     0x40f6a0
0x4572A3: cmp      byte ptr [eax + 0x24c], 0
0x4572AA: je       0x4572d3
0x4572AC: mov      edx, dword ptr [ebp - 0x10]
0x4572AF: mov      eax, dword ptr [ebp - 4]
0x4572B2: mov      eax, dword ptr [eax + 0x250]
0x4572B8: call     0x40f6a0
0x4572BD: mov      edx, dword ptr [ebp - 8]
0x4572C0: mov      bx, 0xffb8
0x4572C4: call     0x403080
0x4572C9: test     al, al
0x4572CB: je       0x4572d3
0x4572CD: mov      byte ptr [ebp - 9], 1
0x4572D1: jmp      0x45730c
0x4572D3: dec      dword ptr [ebp - 0x10]
0x4572D6: cmp      dword ptr [ebp - 0x10], -1
0x4572DA: jne      0x457292
0x4572DC: mov      eax, dword ptr [0x4f7ba0]
0x4572E1: cmp      eax, dword ptr [ebp - 4]
0x4572E4: jne      0x45730c
0x4572E6: mov      eax, dword ptr [ebp - 4]
0x4572E9: cmp      word ptr [eax + 0x206], 0
0x4572F1: je       0x457308
0x4572F3: mov      ecx, dword ptr [ebp - 8]
0x4572F6: mov      ebx, dword ptr [ebp - 4]
0x4572F9: mov      edx, dword ptr [ebp - 4]
0x4572FC: mov      eax, dword ptr [ebx + 0x208]
0x457302: call     dword ptr [ebx + 0x204]
0x457308: mov      byte ptr [ebp - 9], 1
0x45730C: mov      al, byte ptr [ebp - 9]
0x45730F: pop      ebx
0x457310: mov      esp, ebp
0x457312: pop      ebp
0x457313: ret      

; === TDControl.DMT_-73 @ 0x457314 ===
0x457314: push     ebp
0x457315: mov      ebp, esp
0x457317: add      esp, -0x10
0x45731A: push     ebx
0x45731B: mov      byte ptr [ebp - 9], cl
0x45731E: mov      dword ptr [ebp - 8], edx
0x457321: mov      dword ptr [ebp - 4], eax
0x457324: mov      byte ptr [ebp - 0xa], 0
0x457328: mov      eax, dword ptr [ebp - 4]
0x45732B: cmp      byte ptr [eax + 0x24d], 0
0x457332: jne      0x4573ce
0x457338: mov      eax, dword ptr [ebp - 4]
0x45733B: mov      eax, dword ptr [eax + 0x250]
0x457341: mov      eax, dword ptr [eax + 8]
0x457344: dec      eax
0x457345: cmp      eax, 0
0x457348: jl       0x45739a
0x45734A: mov      dword ptr [ebp - 0x10], eax
0x45734D: mov      edx, dword ptr [ebp - 0x10]
0x457350: mov      eax, dword ptr [ebp - 4]
0x457353: mov      eax, dword ptr [eax + 0x250]
0x457359: call     0x40f6a0
0x45735E: cmp      byte ptr [eax + 0x24c], 0
0x457365: je       0x457391
0x457367: mov      edx, dword ptr [ebp - 0x10]
0x45736A: mov      eax, dword ptr [ebp - 4]
0x45736D: mov      eax, dword ptr [eax + 0x250]
0x457373: call     0x40f6a0
0x457378: mov      edx, dword ptr [ebp - 8]
0x45737B: mov      cl, byte ptr [ebp - 9]
0x45737E: mov      bx, 0xffb7
0x457382: call     0x403080
0x457387: test     al, al
0x457389: je       0x457391
0x45738B: mov      byte ptr [ebp - 0xa], 1
0x45738F: jmp      0x4573ce
0x457391: dec      dword ptr [ebp - 0x10]
0x457394: cmp      dword ptr [ebp - 0x10], -1
0x457398: jne      0x45734d
0x45739A: mov      eax, dword ptr [0x4f7ba0]
0x45739F: cmp      eax, dword ptr [ebp - 4]
0x4573A2: jne      0x4573ce
0x4573A4: mov      eax, dword ptr [ebp - 4]
0x4573A7: cmp      word ptr [eax + 0x20e], 0
0x4573AF: je       0x4573ca
0x4573B1: mov      al, byte ptr [ebp - 9]
0x4573B4: push     eax
0x4573B5: mov      ecx, dword ptr [ebp - 8]
0x4573B8: mov      ebx, dword ptr [ebp - 4]
0x4573BB: mov      edx, dword ptr [ebp - 4]
0x4573BE: mov      eax, dword ptr [ebx + 0x210]
0x4573C4: call     dword ptr [ebx + 0x20c]
0x4573CA: mov      byte ptr [ebp - 0xa], 1
0x4573CE: mov      al, byte ptr [ebp - 0xa]
0x4573D1: pop      ebx
0x4573D2: mov      esp, ebp
0x4573D4: pop      ebp
0x4573D5: ret      
0x4573D6: mov      eax, eax
0x4573D8: push     ebp
0x4573D9: mov      ebp, esp
0x4573DB: add      esp, -8
0x4573DE: mov      dword ptr [ebp - 4], eax
0x4573E1: cmp      dword ptr [0x4f7b9c], 0
0x4573E8: je       0x45740d
0x4573EA: cmp      dword ptr [0x4f7b9c], 0
0x4573F1: je       0x457413
0x4573F3: mov      eax, dword ptr [0x4f7b9c]
0x4573F8: cmp      eax, dword ptr [ebp - 4]
0x4573FB: je       0x45740d
0x4573FD: mov      eax, dword ptr [0x4f7b9c]
0x457402: mov      edx, dword ptr [ebp - 4]
0x457405: cmp      eax, dword ptr [edx + 0x1f4]
0x45740B: jne      0x457413
0x45740D: mov      byte ptr [ebp - 5], 1
0x457411: jmp      0x457417
0x457413: mov      byte ptr [ebp - 5], 0
0x457417: mov      al, byte ptr [ebp - 5]
0x45741A: pop      ecx
0x45741B: pop      ecx
0x45741C: pop      ebp
0x45741D: ret      
0x45741E: mov      eax, eax
0x457420: push     ebp
0x457421: mov      ebp, esp
0x457423: add      esp, -0x14
0x457426: push     ebx
0x457427: mov      dword ptr [ebp - 0xc], ecx
0x45742A: mov      byte ptr [ebp - 5], dl
0x45742D: mov      dword ptr [ebp - 4], eax
0x457430: mov      byte ptr [ebp - 0xd], 0
0x457434: mov      eax, dword ptr [ebp - 4]
0x457437: mov      eax, dword ptr [eax + 0x250]
0x45743D: mov      eax, dword ptr [eax + 8]
0x457440: dec      eax
0x457441: cmp      eax, 0
0x457444: jl       0x4574a9
0x457446: mov      dword ptr [ebp - 0x14], eax
0x457449: mov      edx, dword ptr [ebp - 0x14]
0x45744C: mov      eax, dword ptr [ebp - 4]
0x45744F: mov      eax, dword ptr [eax + 0x250]
0x457455: call     0x40f6a0
0x45745A: cmp      byte ptr [eax + 0x24c], 0
0x457461: je       0x4574a0
0x457463: mov      eax, dword ptr [ebp + 8]
0x457466: mov      edx, dword ptr [ebp - 4]
0x457469: sub      eax, dword ptr [edx + 0x34]
0x45746C: push     eax
0x45746D: mov      edx, dword ptr [ebp - 0x14]
0x457470: mov      eax, dword ptr [ebp - 4]
0x457473: mov      eax, dword ptr [eax + 0x250]
0x457479: call     0x40f6a0
0x45747E: mov      ecx, dword ptr [ebp - 0xc]
0x457481: mov      edx, dword ptr [ebp - 4]
0x457484: sub      ecx, dword ptr [edx + 0x30]
0x457487: mov      dl, byte ptr [ebp - 5]
0x45748A: mov      bx, 0xffb6
0x45748E: call     0x403080
0x457493: test     al, al
0x457495: je       0x4574a0
0x457497: mov      byte ptr [ebp - 0xd], 1
0x45749B: jmp      0x457538
0x4574A0: dec      dword ptr [ebp - 0x14]
0x4574A3: cmp      dword ptr [ebp - 0x14], -1
0x4574A7: jne      0x457449
0x4574A9: cmp      dword ptr [0x4f7b9c], 0
0x4574B0: je       0x4574ec
0x4574B2: mov      eax, dword ptr [0x4f7b9c]
0x4574B7: cmp      eax, dword ptr [ebp - 4]
0x4574BA: jne      0x457538
0x4574BC: mov      eax, dword ptr [ebp - 4]
0x4574BF: cmp      word ptr [eax + 0x216], 0
0x4574C7: je       0x4574e6
0x4574C9: mov      eax, dword ptr [ebp - 0xc]
0x4574CC: push     eax
0x4574CD: mov      eax, dword ptr [ebp + 8]
0x4574D0: push     eax
0x4574D1: mov      ebx, dword ptr [ebp - 4]
0x4574D4: mov      cl, byte ptr [ebp - 5]
0x4574D7: mov      edx, dword ptr [ebp - 4]
0x4574DA: mov      eax, dword ptr [ebx + 0x218]
0x4574E0: call     dword ptr [ebx + 0x214]
0x4574E6: mov      byte ptr [ebp - 0xd], 1
0x4574EA: jmp      0x457538
0x4574EC: mov      eax, dword ptr [ebp - 4]
0x4574EF: cmp      byte ptr [eax + 0x24d], 0
0x4574F6: jne      0x457538
0x4574F8: mov      ecx, dword ptr [ebp + 8]
0x4574FB: mov      edx, dword ptr [ebp - 0xc]
0x4574FE: mov      eax, dword ptr [ebp - 4]
0x457501: call     0x457164
0x457506: test     al, al
0x457508: je       0x457538
0x45750A: mov      eax, dword ptr [ebp - 4]
0x45750D: cmp      word ptr [eax + 0x216], 0
0x457515: je       0x457534
0x457517: mov      eax, dword ptr [ebp - 0xc]
0x45751A: push     eax
0x45751B: mov      eax, dword ptr [ebp + 8]
0x45751E: push     eax
0x45751F: mov      ebx, dword ptr [ebp - 4]
0x457522: mov      cl, byte ptr [ebp - 5]
0x457525: mov      edx, dword ptr [ebp - 4]
0x457528: mov      eax, dword ptr [ebx + 0x218]
0x45752E: call     dword ptr [ebx + 0x214]
0x457534: mov      byte ptr [ebp - 0xd], 1
0x457538: mov      al, byte ptr [ebp - 0xd]
0x45753B: pop      ebx
0x45753C: mov      esp, ebp
0x45753E: pop      ebp
0x45753F: ret      4
0x457542: mov      eax, eax
0x457544: push     ebp
0x457545: mov      ebp, esp
0x457547: add      esp, -0xc
0x45754A: push     ebx
0x45754B: mov      byte ptr [ebp - 6], cl
0x45754E: mov      byte ptr [ebp - 5], dl
0x457551: mov      dword ptr [ebp - 4], eax
0x457554: mov      byte ptr [ebp - 7], 0
0x457558: mov      eax, dword ptr [ebp - 4]
0x45755B: mov      eax, dword ptr [eax + 0x250]
0x457561: mov      eax, dword ptr [eax + 8]
0x457564: dec      eax
0x457565: cmp      eax, 0
0x457568: jl       0x4575d1
0x45756A: mov      dword ptr [ebp - 0xc], eax
0x45756D: mov      edx, dword ptr [ebp - 0xc]
0x457570: mov      eax, dword ptr [ebp - 4]
0x457573: mov      eax, dword ptr [eax + 0x250]
0x457579: call     0x40f6a0
0x45757E: cmp      byte ptr [eax + 0x24c], 0
0x457585: je       0x4575c8
0x457587: mov      eax, dword ptr [ebp + 0xc]
0x45758A: mov      edx, dword ptr [ebp - 4]
0x45758D: sub      eax, dword ptr [edx + 0x30]
0x457590: push     eax
0x457591: mov      eax, dword ptr [ebp + 8]
0x457594: mov      edx, dword ptr [ebp - 4]
0x457597: sub      eax, dword ptr [edx + 0x34]
0x45759A: push     eax
0x45759B: mov      edx, dword ptr [ebp - 0xc]
0x45759E: mov      eax, dword ptr [ebp - 4]
0x4575A1: mov      eax, dword ptr [eax + 0x250]
0x4575A7: call     0x40f6a0
0x4575AC: mov      cl, byte ptr [ebp - 6]
0x4575AF: mov      dl, byte ptr [ebp - 5]
0x4575B2: mov      bx, 0xffb5
0x4575B6: call     0x403080
0x4575BB: test     al, al
0x4575BD: je       0x4575c8
0x4575BF: mov      byte ptr [ebp - 7], 1
0x4575C3: jmp      0x45768b
0x4575C8: dec      dword ptr [ebp - 0xc]
0x4575CB: cmp      dword ptr [ebp - 0xc], -1
0x4575CF: jne      0x45756d
0x4575D1: mov      eax, dword ptr [ebp - 4]
0x4575D4: cmp      byte ptr [eax + 0x24d], 0
0x4575DB: je       0x45761d
0x4575DD: mov      eax, dword ptr [ebp - 4]
0x4575E0: cmp      word ptr [eax + 0x246], 0
0x4575E8: je       0x457616
0x4575EA: mov      eax, dword ptr [ebp - 4]
0x4575ED: mov      byte ptr [eax + 0x25c], 0
0x4575F4: mov      ebx, dword ptr [ebp - 4]
0x4575F7: mov      edx, dword ptr [ebp - 4]
0x4575FA: mov      eax, dword ptr [ebx + 0x248]
0x457600: call     dword ptr [ebx + 0x244]
0x457606: mov      eax, dword ptr [ebp - 4]
0x457609: cmp      byte ptr [eax + 0x25c], 0
0x457610: je       0x457616
0x457612: mov      byte ptr [ebp - 7], 1
0x457616: call     0x456af4
0x45761B: jmp      0x45768b
0x45761D: mov      eax, dword ptr [ebp - 4]
0x457620: call     0x4573d8
0x457625: test     al, al
0x457627: je       0x45768b
0x457629: mov      ecx, dword ptr [ebp + 8]
0x45762C: mov      edx, dword ptr [ebp + 0xc]
0x45762F: mov      eax, dword ptr [ebp - 4]
0x457632: call     0x457164
0x457637: test     al, al
0x457639: jne      0x457645
0x45763B: mov      eax, dword ptr [0x4f7b9c]
0x457640: cmp      eax, dword ptr [ebp - 4]
0x457643: jne      0x45768b
0x457645: mov      eax, dword ptr [ebp - 4]
0x457648: cmp      word ptr [eax + 0x21e], 0
0x457650: je       0x457673
0x457652: mov      al, byte ptr [ebp - 6]
0x457655: push     eax
0x457656: mov      eax, dword ptr [ebp + 0xc]
0x457659: push     eax
0x45765A: mov      eax, dword ptr [ebp + 8]
0x45765D: push     eax
0x45765E: mov      ebx, dword ptr [ebp - 4]
0x457661: mov      cl, byte ptr [ebp - 5]
0x457664: mov      edx, dword ptr [ebp - 4]
0x457667: mov      eax, dword ptr [ebx + 0x220]
0x45766D: call     dword ptr [ebx + 0x21c]
0x457673: mov      eax, dword ptr [ebp - 4]
0x457676: cmp      byte ptr [eax + 0x1f8], 0
0x45767D: je       0x457687
0x45767F: mov      eax, dword ptr [ebp - 4]
0x457682: call     0x456ae0
0x457687: mov      byte ptr [ebp - 7], 1
0x45768B: mov      al, byte ptr [ebp - 7]
0x45768E: pop      ebx
0x45768F: mov      esp, ebp
0x457691: pop      ebp
0x457692: ret      8
0x457695: lea      eax, [eax]
0x457698: push     ebp
0x457699: mov      ebp, esp
0x45769B: add      esp, -0xc
0x45769E: push     ebx
0x45769F: mov      byte ptr [ebp - 6], cl
0x4576A2: mov      byte ptr [ebp - 5], dl
0x4576A5: mov      dword ptr [ebp - 4], eax
0x4576A8: mov      byte ptr [ebp - 7], 0
0x4576AC: mov      eax, dword ptr [ebp - 4]
0x4576AF: mov      eax, dword ptr [eax + 0x250]
0x4576B5: mov      eax, dword ptr [eax + 8]
0x4576B8: dec      eax
0x4576B9: cmp      eax, 0
0x4576BC: jl       0x457725
0x4576BE: mov      dword ptr [ebp - 0xc], eax
0x4576C1: mov      edx, dword ptr [ebp - 0xc]
0x4576C4: mov      eax, dword ptr [ebp - 4]
0x4576C7: mov      eax, dword ptr [eax + 0x250]
0x4576CD: call     0x40f6a0
0x4576D2: cmp      byte ptr [eax + 0x24c], 0
0x4576D9: je       0x45771c
0x4576DB: mov      eax, dword ptr [ebp + 0xc]
0x4576DE: mov      edx, dword ptr [ebp - 4]
0x4576E1: sub      eax, dword ptr [edx + 0x30]
0x4576E4: push     eax
0x4576E5: mov      eax, dword ptr [ebp + 8]
0x4576E8: mov      edx, dword ptr [ebp - 4]
0x4576EB: sub      eax, dword ptr [edx + 0x34]
0x4576EE: push     eax
0x4576EF: mov      edx, dword ptr [ebp - 0xc]
0x4576F2: mov      eax, dword ptr [ebp - 4]
0x4576F5: mov      eax, dword ptr [eax + 0x250]
0x4576FB: call     0x40f6a0
0x457700: mov      cl, byte ptr [ebp - 6]
0x457703: mov      dl, byte ptr [ebp - 5]
0x457706: mov      bx, 0xffb4
0x45770A: call     0x403080
0x45770F: test     al, al
0x457711: je       0x45771c
0x457713: mov      byte ptr [ebp - 7], 1
0x457717: jmp      0x4577c0
0x45771C: dec      dword ptr [ebp - 0xc]
0x45771F: cmp      dword ptr [ebp - 0xc], -1
0x457723: jne      0x4576c1
0x457725: cmp      dword ptr [0x4f7b9c], 0
0x45772C: je       0x457770
0x45772E: mov      eax, dword ptr [0x4f7b9c]
0x457733: cmp      eax, dword ptr [ebp - 4]
0x457736: jne      0x4577c0
0x45773C: mov      eax, dword ptr [ebp - 4]
0x45773F: cmp      word ptr [eax + 0x226], 0
0x457747: je       0x45776a
0x457749: mov      al, byte ptr [ebp - 6]
0x45774C: push     eax
0x45774D: mov      eax, dword ptr [ebp + 0xc]
0x457750: push     eax
0x457751: mov      eax, dword ptr [ebp + 8]
0x457754: push     eax
0x457755: mov      ebx, dword ptr [ebp - 4]
0x457758: mov      cl, byte ptr [ebp - 5]
0x45775B: mov      edx, dword ptr [ebp - 4]
0x45775E: mov      eax, dword ptr [ebx + 0x228]
0x457764: call     dword ptr [ebx + 0x224]
0x45776A: mov      byte ptr [ebp - 7], 1
0x45776E: jmp      0x4577c0
0x457770: mov      eax, dword ptr [ebp - 4]
0x457773: cmp      byte ptr [eax + 0x24d], 0
0x45777A: jne      0x4577c0
0x45777C: mov      ecx, dword ptr [ebp + 8]
0x45777F: mov      edx, dword ptr [ebp + 0xc]
0x457782: mov      eax, dword ptr [ebp - 4]
0x457785: call     0x457164
0x45778A: test     al, al
0x45778C: je       0x4577c0
0x45778E: mov      eax, dword ptr [ebp - 4]
0x457791: cmp      word ptr [eax + 0x226], 0
0x457799: je       0x4577bc
0x45779B: mov      al, byte ptr [ebp - 6]
0x45779E: push     eax
0x45779F: mov      eax, dword ptr [ebp + 0xc]
0x4577A2: push     eax
0x4577A3: mov      eax, dword ptr [ebp + 8]
0x4577A6: push     eax
0x4577A7: mov      ebx, dword ptr [ebp - 4]
0x4577AA: mov      cl, byte ptr [ebp - 5]
0x4577AD: mov      edx, dword ptr [ebp - 4]
0x4577B0: mov      eax, dword ptr [ebx + 0x228]
0x4577B6: call     dword ptr [ebx + 0x224]
0x4577BC: mov      byte ptr [ebp - 7], 1
0x4577C0: mov      al, byte ptr [ebp - 7]
0x4577C3: pop      ebx
0x4577C4: mov      esp, ebp
0x4577C6: pop      ebp
0x4577C7: ret      8
0x4577CA: mov      eax, eax
0x4577CC: push     ebp
0x4577CD: mov      ebp, esp
0x4577CF: add      esp, -0x14
0x4577D2: push     ebx
0x4577D3: mov      dword ptr [ebp - 0xc], ecx
0x4577D6: mov      dword ptr [ebp - 8], edx
0x4577D9: mov      dword ptr [ebp - 4], eax
0x4577DC: mov      byte ptr [ebp - 0xd], 0
0x4577E0: cmp      dword ptr [0x4f7b9c], 0
0x4577E7: je       0x45781f
0x4577E9: mov      eax, dword ptr [0x4f7b9c]
0x4577EE: cmp      eax, dword ptr [ebp - 4]
0x4577F1: jne      0x4578ce
0x4577F7: mov      eax, dword ptr [ebp - 4]
0x4577FA: cmp      word ptr [eax + 0x22e], 0
0x457802: je       0x457816
0x457804: mov      ebx, dword ptr [ebp - 4]
0x457807: mov      edx, dword ptr [ebp - 4]
0x45780A: mov      eax, dword ptr [ebx + 0x230]
0x457810: call     dword ptr [ebx + 0x22c]
0x457816: mov      byte ptr [ebp - 0xd], 1
0x45781A: jmp      0x4578ce
0x45781F: mov      eax, dword ptr [ebp - 4]
0x457822: mov      eax, dword ptr [eax + 0x250]
0x457828: mov      eax, dword ptr [eax + 8]
0x45782B: dec      eax
0x45782C: cmp      eax, 0
0x45782F: jl       0x45788d
0x457831: mov      dword ptr [ebp - 0x14], eax
0x457834: mov      edx, dword ptr [ebp - 0x14]
0x457837: mov      eax, dword ptr [ebp - 4]
0x45783A: mov      eax, dword ptr [eax + 0x250]
0x457840: call     0x40f6a0
0x457845: cmp      byte ptr [eax + 0x24c], 0
0x45784C: je       0x457884
0x45784E: mov      edx, dword ptr [ebp - 0x14]
0x457851: mov      eax, dword ptr [ebp - 4]
0x457854: mov      eax, dword ptr [eax + 0x250]
0x45785A: call     0x40f6a0
0x45785F: mov      ecx, dword ptr [ebp - 0xc]
0x457862: mov      edx, dword ptr [ebp - 4]
0x457865: sub      ecx, dword ptr [edx + 0x34]
0x457868: mov      edx, dword ptr [ebp - 8]
0x45786B: mov      ebx, dword ptr [ebp - 4]
0x45786E: sub      edx, dword ptr [ebx + 0x30]
0x457871: mov      bx, 0xffb3
0x457875: call     0x403080
0x45787A: test     al, al
0x45787C: je       0x457884
0x45787E: mov      byte ptr [ebp - 0xd], 1
0x457882: jmp      0x4578ce
0x457884: dec      dword ptr [ebp - 0x14]
0x457887: cmp      dword ptr [ebp - 0x14], -1
0x45788B: jne      0x457834
0x45788D: mov      eax, dword ptr [ebp - 4]
0x457890: cmp      byte ptr [eax + 0x24d], 0
0x457897: jne      0x4578ce
0x457899: mov      ecx, dword ptr [ebp - 0xc]
0x45789C: mov      edx, dword ptr [ebp - 8]
0x45789F: mov      eax, dword ptr [ebp - 4]
0x4578A2: call     0x457164
0x4578A7: test     al, al
0x4578A9: je       0x4578ce
0x4578AB: mov      eax, dword ptr [ebp - 4]
0x4578AE: cmp      word ptr [eax + 0x22e], 0
0x4578B6: je       0x4578ca
0x4578B8: mov      ebx, dword ptr [ebp - 4]
0x4578BB: mov      edx, dword ptr [ebp - 4]
0x4578BE: mov      eax, dword ptr [ebx + 0x230]
0x4578C4: call     dword ptr [ebx + 0x22c]
0x4578CA: mov      byte ptr [ebp - 0xd], 1
0x4578CE: mov      al, byte ptr [ebp - 0xd]
0x4578D1: pop      ebx
0x4578D2: mov      esp, ebp
0x4578D4: pop      ebp
0x4578D5: ret      
0x4578D6: mov      eax, eax
0x4578D8: push     ebp
0x4578D9: mov      ebp, esp
0x4578DB: add      esp, -0x14
0x4578DE: push     ebx
0x4578DF: mov      dword ptr [ebp - 0xc], ecx
0x4578E2: mov      dword ptr [ebp - 8], edx
0x4578E5: mov      dword ptr [ebp - 4], eax
0x4578E8: mov      byte ptr [ebp - 0xd], 0
0x4578EC: cmp      dword ptr [0x4f7b9c], 0
0x4578F3: je       0x457932
0x4578F5: mov      eax, dword ptr [0x4f7b9c]
0x4578FA: cmp      eax, dword ptr [ebp - 4]
0x4578FD: jne      0x4579e8
0x457903: mov      eax, dword ptr [ebp - 4]
0x457906: cmp      word ptr [eax + 0x236], 0
0x45790E: je       0x457929
0x457910: mov      eax, dword ptr [ebp - 0xc]
0x457913: push     eax
0x457914: mov      ebx, dword ptr [ebp - 4]
0x457917: mov      ecx, dword ptr [ebp - 8]
0x45791A: mov      edx, dword ptr [ebp - 4]
0x45791D: mov      eax, dword ptr [ebx + 0x238]
0x457923: call     dword ptr [ebx + 0x234]
0x457929: mov      byte ptr [ebp - 0xd], 1
0x45792D: jmp      0x4579e8
0x457932: mov      eax, dword ptr [ebp - 4]
0x457935: mov      eax, dword ptr [eax + 0x250]
0x45793B: mov      eax, dword ptr [eax + 8]
0x45793E: dec      eax
0x45793F: cmp      eax, 0
0x457942: jl       0x4579a0
0x457944: mov      dword ptr [ebp - 0x14], eax
0x457947: mov      edx, dword ptr [ebp - 0x14]
0x45794A: mov      eax, dword ptr [ebp - 4]
0x45794D: mov      eax, dword ptr [eax + 0x250]
0x457953: call     0x40f6a0
0x457958: cmp      byte ptr [eax + 0x24c], 0
0x45795F: je       0x457997
0x457961: mov      edx, dword ptr [ebp - 0x14]
0x457964: mov      eax, dword ptr [ebp - 4]
0x457967: mov      eax, dword ptr [eax + 0x250]
0x45796D: call     0x40f6a0
0x457972: mov      ecx, dword ptr [ebp - 0xc]
0x457975: mov      edx, dword ptr [ebp - 4]
0x457978: sub      ecx, dword ptr [edx + 0x34]
0x45797B: mov      edx, dword ptr [ebp - 8]
0x45797E: mov      ebx, dword ptr [ebp - 4]
0x457981: sub      edx, dword ptr [ebx + 0x30]
0x457984: mov      bx, 0xffb2
0x457988: call     0x403080
0x45798D: test     al, al
0x45798F: je       0x457997
0x457991: mov      byte ptr [ebp - 0xd], 1
0x457995: jmp      0x4579e8
0x457997: dec      dword ptr [ebp - 0x14]
0x45799A: cmp      dword ptr [ebp - 0x14], -1
0x45799E: jne      0x457947
0x4579A0: mov      eax, dword ptr [ebp - 4]
0x4579A3: cmp      byte ptr [eax + 0x24d], 0
0x4579AA: jne      0x4579e8
0x4579AC: mov      ecx, dword ptr [ebp - 0xc]
0x4579AF: mov      edx, dword ptr [ebp - 8]
0x4579B2: mov      eax, dword ptr [ebp - 4]
0x4579B5: call     0x457164
0x4579BA: test     al, al
0x4579BC: je       0x4579e8
0x4579BE: mov      eax, dword ptr [ebp - 4]
0x4579C1: cmp      word ptr [eax + 0x236], 0
0x4579C9: je       0x4579e4
0x4579CB: mov      eax, dword ptr [ebp - 0xc]
0x4579CE: push     eax
0x4579CF: mov      ebx, dword ptr [ebp - 4]
0x4579D2: mov      ecx, dword ptr [ebp - 8]
0x4579D5: mov      edx, dword ptr [ebp - 4]
0x4579D8: mov      eax, dword ptr [ebx + 0x238]
0x4579DE: call     dword ptr [ebx + 0x234]
0x4579E4: mov      byte ptr [ebp - 0xd], 1
0x4579E8: mov      al, byte ptr [ebp - 0xd]
0x4579EB: pop      ebx
0x4579EC: mov      esp, ebp
0x4579EE: pop      ebp
0x4579EF: ret      
0x4579F0: push     ebp
0x4579F1: mov      ebp, esp
0x4579F3: add      esp, -0x10
0x4579F6: mov      dword ptr [ebp - 0xc], ecx
0x4579F9: mov      dword ptr [ebp - 8], edx
0x4579FC: mov      dword ptr [ebp - 4], eax
0x4579FF: cmp      dword ptr [ebp - 8], 0
0x457A03: je       0x457a55
0x457A05: mov      edx, dword ptr [ebp - 0xc]
0x457A08: mov      eax, dword ptr [ebp - 8]
0x457A0B: call     0x45351c
0x457A10: mov      dword ptr [ebp - 0x10], eax
0x457A13: mov      eax, dword ptr [ebp - 8]
0x457A16: mov      edx, dword ptr [ebp - 4]
0x457A19: mov      dword ptr [edx + 0x254], eax
0x457A1F: mov      eax, dword ptr [ebp - 0xc]
0x457A22: mov      edx, dword ptr [ebp - 4]
0x457A25: mov      dword ptr [edx + 0x258], eax
0x457A2B: cmp      dword ptr [ebp - 0x10], 0
0x457A2F: je       0x457a55
0x457A31: mov      eax, dword ptr [ebp - 0x10]
0x457A34: call     0x44dc60
0x457A39: mov      edx, eax
0x457A3B: mov      eax, dword ptr [ebp - 4]
0x457A3E: call     0x4271d8
0x457A43: mov      eax, dword ptr [ebp - 0x10]
0x457A46: call     0x44d944
0x457A4B: mov      edx, eax
0x457A4D: mov      eax, dword ptr [ebp - 4]
0x457A50: call     0x4271fc
0x457A55: mov      esp, ebp
0x457A57: pop      ebp
0x457A58: ret      
0x457A59: lea      eax, [eax]
0x457A5C: push     ebp
0x457A5D: mov      ebp, esp
0x457A5F: add      esp, -0x24
0x457A62: push     ebx
0x457A63: mov      dword ptr [ebp - 8], edx
0x457A66: mov      dword ptr [ebp - 4], eax
0x457A69: mov      eax, dword ptr [ebp - 4]
0x457A6C: cmp      word ptr [eax + 0x1fe], 0
0x457A74: je       0x457a8d
0x457A76: mov      ebx, dword ptr [ebp - 4]
0x457A79: mov      ecx, dword ptr [ebp - 8]
0x457A7C: mov      edx, dword ptr [ebp - 4]
0x457A7F: mov      eax, dword ptr [ebx + 0x200]
0x457A85: call     dword ptr [ebx + 0x1fc]
0x457A8B: jmp      0x457af6
0x457A8D: mov      eax, dword ptr [ebp - 4]
0x457A90: cmp      dword ptr [eax + 0x254], 0
0x457A97: je       0x457af6
0x457A99: mov      eax, dword ptr [ebp - 4]
0x457A9C: mov      edx, dword ptr [eax + 0x258]
0x457AA2: mov      eax, dword ptr [ebp - 4]
0x457AA5: mov      eax, dword ptr [eax + 0x254]
0x457AAB: call     0x45351c
0x457AB0: mov      dword ptr [ebp - 0x10], eax
0x457AB3: cmp      dword ptr [ebp - 0x10], 0
0x457AB7: je       0x457af6
0x457AB9: lea      edx, [ebp - 0x24]
0x457ABC: mov      eax, dword ptr [ebp - 0x10]
0x457ABF: call     0x44d8ec
0x457AC4: lea      eax, [ebp - 0x24]
0x457AC7: push     eax
0x457AC8: mov      eax, dword ptr [ebp - 0x10]
0x457ACB: push     eax
0x457ACC: push     1
0x457ACE: mov      eax, dword ptr [ebp - 4]
0x457AD1: mov      edx, dword ptr [eax + 0x34]
0x457AD4: mov      eax, dword ptr [ebp - 4]
0x457AD7: call     0x456fdc
0x457ADC: push     eax
0x457ADD: mov      eax, dword ptr [ebp - 4]
0x457AE0: mov      edx, dword ptr [eax + 0x30]
0x457AE3: mov      eax, dword ptr [ebp - 4]
0x457AE6: call     0x456f94
0x457AEB: mov      edx, eax
0x457AED: mov      eax, dword ptr [ebp - 8]
0x457AF0: pop      ecx
0x457AF1: call     0x44d6cc
0x457AF6: mov      eax, dword ptr [ebp - 4]
0x457AF9: mov      eax, dword ptr [eax + 0x250]
0x457AFF: mov      eax, dword ptr [eax + 8]
0x457B02: dec      eax
0x457B03: test     eax, eax
0x457B05: jl       0x457b51
0x457B07: inc      eax
0x457B08: mov      dword ptr [ebp - 0x14], eax
0x457B0B: mov      dword ptr [ebp - 0xc], 0
0x457B12: mov      edx, dword ptr [ebp - 0xc]
0x457B15: mov      eax, dword ptr [ebp - 4]
0x457B18: mov      eax, dword ptr [eax + 0x250]
0x457B1E: call     0x40f6a0
0x457B23: cmp      byte ptr [eax + 0x24c], 0
0x457B2A: je       0x457b49
0x457B2C: mov      edx, dword ptr [ebp - 0xc]
0x457B2F: mov      eax, dword ptr [ebp - 4]
0x457B32: mov      eax, dword ptr [eax + 0x250]
0x457B38: call     0x40f6a0
0x457B3D: mov      edx, dword ptr [ebp - 8]
0x457B40: mov      bx, 0xffb1
0x457B44: call     0x403080
0x457B49: inc      dword ptr [ebp - 0xc]
0x457B4C: dec      dword ptr [ebp - 0x14]
0x457B4F: jne      0x457b12
0x457B51: pop      ebx
0x457B52: mov      esp, ebp
0x457B54: pop      ebp
0x457B55: ret      
0x457B56: mov      eax, eax
0x457B58: push     ebp
0x457B59: mov      ebp, esp
0x457B5B: add      esp, -0xc
0x457B5E: test     dl, dl
0x457B60: je       0x457b6a
0x457B62: add      esp, -0x10
0x457B65: call     0x4031a4
0x457B6A: mov      dword ptr [ebp - 0xc], ecx
0x457B6D: mov      byte ptr [ebp - 5], dl
0x457B70: mov      dword ptr [ebp - 4], eax
0x457B73: mov      ecx, dword ptr [ebp - 0xc]
0x457B76: xor      edx, edx
0x457B78: mov      eax, dword ptr [ebp - 4]
0x457B7B: call     0x456b2c
0x457B80: mov      eax, dword ptr [ebp - 4]
0x457B83: mov      byte ptr [eax + 0x274], 0
0x457B8A: mov      eax, dword ptr [ebp - 4]
0x457B8D: xor      edx, edx
0x457B8F: mov      dword ptr [eax + 0x264], edx
0x457B95: mov      dword ptr [eax + 0x268], edx
0x457B9B: mov      eax, dword ptr [ebp - 4]
0x457B9E: mov      byte ptr [eax + 0x1f8], 1
0x457BA5: mov      eax, dword ptr [ebp - 4]
0x457BA8: mov      byte ptr [eax + 0x260], 0
0x457BAF: mov      eax, dword ptr [ebp - 4]
0x457BB2: cmp      byte ptr [ebp - 5], 0
0x457BB6: je       0x457bc7
0x457BB8: call     0x4031fc
0x457BBD: pop      dword ptr fs:[0]
0x457BC4: add      esp, 0xc
0x457BC7: mov      eax, dword ptr [ebp - 4]
0x457BCA: mov      esp, ebp
0x457BCC: pop      ebp
0x457BCD: ret      
0x457BCE: mov      eax, eax
0x457BD0: push     ebp
0x457BD1: mov      ebp, esp
0x457BD3: add      esp, -0x10
0x457BD6: mov      dword ptr [ebp - 0xc], ecx
0x457BD9: mov      byte ptr [ebp - 5], dl
0x457BDC: mov      dword ptr [ebp - 4], eax
0x457BDF: mov      eax, dword ptr [ebp + 8]
0x457BE2: push     eax
0x457BE3: mov      ecx, dword ptr [ebp - 0xc]
0x457BE6: mov      dl, byte ptr [ebp - 5]
0x457BE9: mov      eax, dword ptr [ebp - 4]
0x457BEC: call     0x457420 ; TDControl.DMT_-74
0x457BF1: mov      byte ptr [ebp - 0xd], al
0x457BF4: mov      eax, dword ptr [ebp - 4]
0x457BF7: cmp      byte ptr [eax + 0x24d], 0
0x457BFE: jne      0x457c4d
0x457C00: cmp      byte ptr [ebp - 0xd], 0
0x457C04: jne      0x457c4d
0x457C06: mov      eax, dword ptr [ebp + 8]
0x457C09: push     eax
0x457C0A: mov      ecx, dword ptr [ebp - 0xc]
0x457C0D: mov      dl, byte ptr [ebp - 5]
0x457C10: mov      eax, dword ptr [ebp - 4]
0x457C13: call     0x457420 ; TDControl.DMT_-74
0x457C18: mov      byte ptr [ebp - 0xd], al
0x457C1B: mov      eax, dword ptr [0x4f7b9c]
0x457C20: cmp      eax, dword ptr [ebp - 4]
0x457C23: jne      0x457c4d
0x457C25: mov      ecx, dword ptr [ebp + 8]
0x457C28: mov      edx, dword ptr [ebp - 0xc]
0x457C2B: mov      eax, dword ptr [ebp - 4]
0x457C2E: call     0x457164
0x457C33: test     al, al
0x457C35: je       0x457c43
0x457C37: mov      eax, dword ptr [ebp - 4]
0x457C3A: mov      byte ptr [eax + 0x274], 1
0x457C41: jmp      0x457c4d
0x457C43: mov      eax, dword ptr [ebp - 4]
0x457C46: mov      byte ptr [eax + 0x274], 0
0x457C4D: mov      al, byte ptr [ebp - 0xd]
0x457C50: mov      esp, ebp
0x457C52: pop      ebp
0x457C53: ret      4
0x457C56: mov      eax, eax
0x457C58: push     ebp
0x457C59: mov      ebp, esp
0x457C5B: add      esp, -8
0x457C5E: mov      byte ptr [ebp - 6], cl
0x457C61: mov      byte ptr [ebp - 5], dl
0x457C64: mov      dword ptr [ebp - 4], eax
0x457C67: mov      byte ptr [ebp - 7], 0
0x457C6B: mov      eax, dword ptr [ebp + 0xc]
0x457C6E: push     eax
0x457C6F: mov      eax, dword ptr [ebp + 8]
0x457C72: push     eax
0x457C73: mov      cl, byte ptr [ebp - 6]
0x457C76: mov      dl, byte ptr [ebp - 5]
0x457C79: mov      eax, dword ptr [ebp - 4]
0x457C7C: call     0x457544 ; TDControl.DMT_-75
0x457C81: test     al, al
0x457C83: je       0x457cb0
0x457C85: mov      eax, dword ptr [ebp - 4]
0x457C88: cmp      byte ptr [eax + 0x24d], 0
0x457C8F: jne      0x457cac
0x457C91: cmp      dword ptr [0x4f7b9c], 0
0x457C98: jne      0x457cac
0x457C9A: mov      eax, dword ptr [ebp - 4]
0x457C9D: mov      byte ptr [eax + 0x274], 1
0x457CA4: mov      eax, dword ptr [ebp - 4]
0x457CA7: call     0x456afc
0x457CAC: mov      byte ptr [ebp - 7], 1
0x457CB0: mov      al, byte ptr [ebp - 7]
0x457CB3: pop      ecx
0x457CB4: pop      ecx
0x457CB5: pop      ebp
0x457CB6: ret      8
0x457CB9: lea      eax, [eax]
0x457CBC: push     ebp
0x457CBD: mov      ebp, esp
0x457CBF: add      esp, -8
0x457CC2: push     ebx
0x457CC3: mov      byte ptr [ebp - 6], cl
0x457CC6: mov      byte ptr [ebp - 5], dl
0x457CC9: mov      dword ptr [ebp - 4], eax
0x457CCC: mov      byte ptr [ebp - 7], 0
0x457CD0: mov      eax, dword ptr [ebp + 0xc]
0x457CD3: push     eax
0x457CD4: mov      eax, dword ptr [ebp + 8]
0x457CD7: push     eax
0x457CD8: mov      cl, byte ptr [ebp - 6]
0x457CDB: mov      dl, byte ptr [ebp - 5]
0x457CDE: mov      eax, dword ptr [ebp - 4]
0x457CE1: call     0x457698 ; TDControl.DMT_-76
0x457CE6: test     al, al
0x457CE8: je       0x457d6f
0x457CEE: call     0x456b1c
0x457CF3: mov      eax, dword ptr [ebp - 4]
0x457CF6: cmp      byte ptr [eax + 0x24d], 0
0x457CFD: jne      0x457d5f
0x457CFF: mov      ecx, dword ptr [ebp + 8]
0x457D02: mov      edx, dword ptr [ebp + 0xc]
0x457D05: mov      eax, dword ptr [ebp - 4]
0x457D08: call     0x457164
0x457D0D: test     al, al
0x457D0F: je       0x457d5f
0x457D11: mov      eax, dword ptr [ebp - 4]
0x457D14: cmp      word ptr [eax + 0x26e], 0
0x457D1C: je       0x457d39
0x457D1E: mov      ebx, dword ptr [ebp - 4]
0x457D21: mov      eax, dword ptr [ebp - 4]
0x457D24: mov      cl, byte ptr [eax + 0x260]
0x457D2A: mov      edx, dword ptr [ebp - 4]
0x457D2D: mov      eax, dword ptr [ebx + 0x270]
0x457D33: call     dword ptr [ebx + 0x26c]
0x457D39: mov      eax, dword ptr [ebp - 4]
0x457D3C: cmp      word ptr [eax + 0x266], 0
0x457D44: je       0x457d5f
0x457D46: mov      eax, dword ptr [ebp + 8]
0x457D49: push     eax
0x457D4A: mov      ebx, dword ptr [ebp - 4]
0x457D4D: mov      ecx, dword ptr [ebp + 0xc]
0x457D50: mov      edx, dword ptr [ebp - 4]
0x457D53: mov      eax, dword ptr [ebx + 0x268]
0x457D59: call     dword ptr [ebx + 0x264]
0x457D5F: mov      eax, dword ptr [ebp - 4]
0x457D62: mov      byte ptr [eax + 0x274], 0
0x457D69: mov      byte ptr [ebp - 7], 1
0x457D6D: jmp      0x457d7e
0x457D6F: call     0x456b1c
0x457D74: mov      eax, dword ptr [ebp - 4]
0x457D77: mov      byte ptr [eax + 0x274], 0
0x457D7E: mov      al, byte ptr [ebp - 7]
0x457D81: pop      ebx
0x457D82: pop      ecx
0x457D83: pop      ecx
0x457D84: pop      ebp
0x457D85: ret      8
0x457D88: push     ebp
0x457D89: mov      ebp, esp
0x457D8B: add      esp, -0xc
0x457D8E: test     dl, dl
0x457D90: je       0x457d9a
0x457D92: add      esp, -0x10
0x457D95: call     0x4031a4
0x457D9A: mov      dword ptr [ebp - 0xc], ecx
0x457D9D: mov      byte ptr [ebp - 5], dl
0x457DA0: mov      dword ptr [ebp - 4], eax
0x457DA3: mov      ecx, dword ptr [ebp - 0xc]
0x457DA6: xor      edx, edx
0x457DA8: mov      eax, dword ptr [ebp - 4]
0x457DAB: call     0x456b2c
0x457DB0: mov      eax, dword ptr [ebp - 4]
0x457DB3: mov      dword ptr [eax + 0x260], 8
0x457DBD: mov      eax, dword ptr [ebp - 4]
0x457DC0: mov      dword ptr [eax + 0x264], 5
0x457DCA: mov      eax, dword ptr [ebp - 4]
0x457DCD: mov      dword ptr [eax + 0x268], 0x24
0x457DD7: mov      eax, dword ptr [ebp - 4]
0x457DDA: mov      dword ptr [eax + 0x26c], 0x20
0x457DE4: mov      eax, dword ptr [ebp - 4]
0x457DE7: xor      edx, edx
0x457DE9: mov      dword ptr [eax + 0x284], edx
0x457DEF: mov      dword ptr [eax + 0x288], edx
0x457DF5: mov      eax, dword ptr [ebp - 4]
0x457DF8: xor      edx, edx
0x457DFA: mov      dword ptr [eax + 0x28c], edx
0x457E00: mov      dword ptr [eax + 0x290], edx
0x457E06: mov      eax, dword ptr [ebp - 4]
0x457E09: xor      edx, edx
0x457E0B: mov      dword ptr [eax + 0x294], edx
0x457E11: mov      dword ptr [eax + 0x298], edx
0x457E17: mov      eax, dword ptr [ebp - 4]
0x457E1A: cmp      byte ptr [ebp - 5], 0
0x457E1E: je       0x457e2f
0x457E20: call     0x4031fc
0x457E25: pop      dword ptr fs:[0]
0x457E2C: add      esp, 0xc
0x457E2F: mov      eax, dword ptr [ebp - 4]
0x457E32: mov      esp, ebp
0x457E34: pop      ebp
0x457E35: ret      
0x457E36: mov      eax, eax
0x457E38: push     ebp
0x457E39: mov      ebp, esp
0x457E3B: add      esp, -0x10
0x457E3E: mov      dword ptr [ebp - 0xc], ecx
0x457E41: mov      dword ptr [ebp - 8], edx
0x457E44: mov      dword ptr [ebp - 4], eax
0x457E47: mov      byte ptr [ebp - 0xd], 0
0x457E4B: mov      ecx, dword ptr [ebp - 0xc]
0x457E4E: mov      edx, dword ptr [ebp - 8]
0x457E51: mov      eax, dword ptr [ebp - 4]
0x457E54: call     0x457164
0x457E59: test     al, al
0x457E5B: je       0x457e95
0x457E5D: mov      eax, dword ptr [ebp - 8]
0x457E60: mov      edx, dword ptr [ebp - 4]
0x457E63: sub      eax, dword ptr [edx + 0x30]
0x457E66: mov      edx, dword ptr [ebp - 4]
0x457E69: mov      ecx, dword ptr [edx + 0x268]
0x457E6F: cdq      
0x457E70: idiv     ecx
0x457E72: mov      edx, dword ptr [ebp + 0xc]
0x457E75: mov      dword ptr [edx], eax
0x457E77: mov      eax, dword ptr [ebp - 0xc]
0x457E7A: mov      edx, dword ptr [ebp - 4]
0x457E7D: sub      eax, dword ptr [edx + 0x34]
0x457E80: mov      edx, dword ptr [ebp - 4]
0x457E83: mov      ecx, dword ptr [edx + 0x26c]
0x457E89: cdq      
0x457E8A: idiv     ecx
0x457E8C: mov      edx, dword ptr [ebp + 8]
0x457E8F: mov      dword ptr [edx], eax
0x457E91: mov      byte ptr [ebp - 0xd], 1
0x457E95: mov      al, byte ptr [ebp - 0xd]
0x457E98: mov      esp, ebp
0x457E9A: pop      ebp
0x457E9B: ret      8
0x457E9E: mov      eax, eax
0x457EA0: push     ebp
0x457EA1: mov      ebp, esp
0x457EA3: add      esp, -0x14
0x457EA6: mov      byte ptr [ebp - 0x11], cl
0x457EA9: mov      byte ptr [ebp - 5], dl
0x457EAC: mov      dword ptr [ebp - 4], eax
0x457EAF: mov      byte ptr [ebp - 6], 0
0x457EB3: cmp      byte ptr [ebp - 5], 0
0x457EB7: jne      0x457f0f
0x457EB9: lea      eax, [ebp - 0xc]
0x457EBC: push     eax
0x457EBD: lea      eax, [ebp - 0x10]
0x457EC0: push     eax
0x457EC1: mov      ecx, dword ptr [ebp + 8]
0x457EC4: mov      edx, dword ptr [ebp + 0xc]
0x457EC7: mov      eax, dword ptr [ebp - 4]
0x457ECA: call     0x457e38
0x457ECF: test     al, al
0x457ED1: je       0x457f0f
0x457ED3: mov      eax, dword ptr [ebp - 0xc]
0x457ED6: mov      edx, dword ptr [ebp - 4]
0x457ED9: mov      dword ptr [edx + 0x274], eax
0x457EDF: mov      eax, dword ptr [ebp - 0x10]
0x457EE2: mov      edx, dword ptr [ebp - 4]
0x457EE5: mov      dword ptr [edx + 0x278], eax
0x457EEB: mov      eax, dword ptr [ebp + 0xc]
0x457EEE: mov      edx, dword ptr [ebp - 4]
0x457EF1: mov      dword ptr [edx + 0x27c], eax
0x457EF7: mov      eax, dword ptr [ebp + 8]
0x457EFA: mov      edx, dword ptr [ebp - 4]
0x457EFD: mov      dword ptr [edx + 0x280], eax
0x457F03: mov      eax, dword ptr [ebp - 4]
0x457F06: call     0x456afc
0x457F0B: mov      byte ptr [ebp - 6], 1
0x457F0F: mov      al, byte ptr [ebp - 6]
0x457F12: mov      esp, ebp
0x457F14: pop      ebp
0x457F15: ret      8

; === TDGrid.DMT_-74 @ 0x457F18 ===
0x457F18: push     ebp
0x457F19: mov      ebp, esp
0x457F1B: add      esp, -0x18
0x457F1E: push     ebx
0x457F1F: mov      dword ptr [ebp - 0xc], ecx
0x457F22: mov      byte ptr [ebp - 5], dl
0x457F25: mov      dword ptr [ebp - 4], eax
0x457F28: mov      byte ptr [ebp - 0xd], 0
0x457F2C: mov      ecx, dword ptr [ebp + 8]
0x457F2F: mov      edx, dword ptr [ebp - 0xc]
0x457F32: mov      eax, dword ptr [ebp - 4]
0x457F35: call     0x457164
0x457F3A: test     al, al
0x457F3C: je       0x457f86
0x457F3E: lea      eax, [ebp - 0x14]
0x457F41: push     eax
0x457F42: lea      eax, [ebp - 0x18]
0x457F45: push     eax
0x457F46: mov      ecx, dword ptr [ebp + 8]
0x457F49: mov      edx, dword ptr [ebp - 0xc]
0x457F4C: mov      eax, dword ptr [ebp - 4]
0x457F4F: call     0x457e38
0x457F54: test     al, al
0x457F56: je       0x457f82
0x457F58: mov      eax, dword ptr [ebp - 4]
0x457F5B: cmp      word ptr [eax + 0x28e], 0
0x457F63: je       0x457f82
0x457F65: mov      eax, dword ptr [ebp - 0x18]
0x457F68: push     eax
0x457F69: mov      al, byte ptr [ebp - 5]
0x457F6C: push     eax
0x457F6D: mov      ebx, dword ptr [ebp - 4]
0x457F70: mov      ecx, dword ptr [ebp - 0x14]
0x457F73: mov      edx, dword ptr [ebp - 4]
0x457F76: mov      eax, dword ptr [ebx + 0x290]
0x457F7C: call     dword ptr [ebx + 0x28c]
0x457F82: mov      byte ptr [ebp - 0xd], 1
0x457F86: mov      al, byte ptr [ebp - 0xd]
0x457F89: pop      ebx
0x457F8A: mov      esp, ebp
0x457F8C: pop      ebp
0x457F8D: ret      4

; === TDGrid.DMT_-76 @ 0x457F90 ===
0x457F90: push     ebp
0x457F91: mov      ebp, esp
0x457F93: add      esp, -0x10
0x457F96: push     ebx
0x457F97: mov      byte ptr [ebp - 6], cl
0x457F9A: mov      byte ptr [ebp - 5], dl
0x457F9D: mov      dword ptr [ebp - 4], eax
0x457FA0: mov      byte ptr [ebp - 7], 0
0x457FA4: cmp      byte ptr [ebp - 5], 0
0x457FA8: jne      0x45802f
0x457FAE: lea      eax, [ebp - 0xc]
0x457FB1: push     eax
0x457FB2: lea      eax, [ebp - 0x10]
0x457FB5: push     eax
0x457FB6: mov      ecx, dword ptr [ebp + 8]
0x457FB9: mov      edx, dword ptr [ebp + 0xc]
0x457FBC: mov      eax, dword ptr [ebp - 4]
0x457FBF: call     0x457e38
0x457FC4: test     al, al
0x457FC6: je       0x45802a
0x457FC8: mov      eax, dword ptr [ebp - 4]
0x457FCB: mov      eax, dword ptr [eax + 0x274]
0x457FD1: cmp      eax, dword ptr [ebp - 0xc]
0x457FD4: jne      0x458026
0x457FD6: mov      eax, dword ptr [ebp - 4]
0x457FD9: mov      eax, dword ptr [eax + 0x278]
0x457FDF: cmp      eax, dword ptr [ebp - 0x10]
0x457FE2: jne      0x458026
0x457FE4: mov      eax, dword ptr [ebp - 0xc]
0x457FE7: mov      edx, dword ptr [ebp - 4]
0x457FEA: mov      dword ptr [edx + 0x2a4], eax
0x457FF0: mov      eax, dword ptr [ebp - 0x10]
0x457FF3: mov      edx, dword ptr [ebp - 4]
0x457FF6: mov      dword ptr [edx + 0x2a8], eax
0x457FFC: mov      eax, dword ptr [ebp - 4]
0x457FFF: cmp      word ptr [eax + 0x286], 0
0x458007: je       0x458026
0x458009: mov      eax, dword ptr [ebp - 0x10]
0x45800C: push     eax
0x45800D: mov      al, byte ptr [ebp - 6]
0x458010: push     eax
0x458011: mov      ebx, dword ptr [ebp - 4]
0x458014: mov      ecx, dword ptr [ebp - 0xc]
0x458017: mov      edx, dword ptr [ebp - 4]
0x45801A: mov      eax, dword ptr [ebx + 0x288]
0x458020: call     dword ptr [ebx + 0x284]
0x458026: mov      byte ptr [ebp - 7], 1
0x45802A: call     0x456b1c
0x45802F: mov      al, byte ptr [ebp - 7]
0x458032: pop      ebx
0x458033: mov      esp, ebp
0x458035: pop      ebp
0x458036: ret      8
0x458039: lea      eax, [eax]
0x45803C: push     ebp
0x45803D: mov      ebp, esp
0x45803F: add      esp, -0x18
0x458042: mov      dword ptr [ebp - 0x10], ecx
0x458045: mov      dword ptr [ebp - 0xc], edx
0x458048: mov      dword ptr [ebp - 4], eax
0x45804B: mov      byte ptr [ebp - 5], 0
0x45804F: mov      al, byte ptr [ebp - 5]
0x458052: mov      esp, ebp
0x458054: pop      ebp
0x458055: ret      
0x458056: mov      eax, eax
0x458058: push     ebp
0x458059: mov      ebp, esp
0x45805B: add      esp, -0x28
0x45805E: push     ebx
0x45805F: mov      dword ptr [ebp - 8], edx
0x458062: mov      dword ptr [ebp - 4], eax
0x458065: mov      eax, dword ptr [ebp - 4]
0x458068: cmp      word ptr [eax + 0x296], 0
0x458070: je       0x45818d
0x458076: mov      eax, dword ptr [ebp - 4]
0x458079: mov      eax, dword ptr [eax + 0x264]
0x45807F: dec      eax
0x458080: test     eax, eax
0x458082: jl       0x45818d
0x458088: inc      eax
0x458089: mov      dword ptr [ebp - 0x14], eax
0x45808C: mov      dword ptr [ebp - 0xc], 0
0x458093: mov      eax, dword ptr [ebp - 4]
0x458096: mov      eax, dword ptr [eax + 0x260]
0x45809C: dec      eax
0x45809D: test     eax, eax
0x45809F: jl       0x458181
0x4580A5: inc      eax
0x4580A6: mov      dword ptr [ebp - 0x18], eax
0x4580A9: mov      dword ptr [ebp - 0x10], 0
0x4580B0: mov      eax, dword ptr [ebp - 4]
0x4580B3: mov      eax, dword ptr [eax + 0x26c]
0x4580B9: inc      eax
0x4580BA: imul     dword ptr [ebp - 0xc]
0x4580BD: mov      edx, dword ptr [ebp - 4]
0x4580C0: add      eax, dword ptr [edx + 0x34]
0x4580C3: dec      eax
0x4580C4: push     eax
0x4580C5: lea      eax, [ebp - 0x28]
0x4580C8: push     eax
0x4580C9: mov      eax, dword ptr [ebp - 4]
0x4580CC: mov      ecx, dword ptr [eax + 0x268]
0x4580D2: inc      ecx
0x4580D3: imul     ecx, dword ptr [ebp - 0x10]
0x4580D7: mov      eax, dword ptr [ebp - 4]
0x4580DA: add      ecx, dword ptr [eax + 0x30]
0x4580DD: dec      ecx
0x4580DE: mov      edx, dword ptr [ebp - 0xc]
0x4580E1: mov      eax, dword ptr [ebp - 4]
0x4580E4: imul     edx, dword ptr [eax + 0x26c]
0x4580EB: mov      eax, dword ptr [ebp - 4]
0x4580EE: add      edx, dword ptr [eax + 0x34]
0x4580F1: mov      eax, dword ptr [ebp - 0x10]
0x4580F4: mov      ebx, dword ptr [ebp - 4]
0x4580F7: imul     eax, dword ptr [ebx + 0x268]
0x4580FE: mov      ebx, dword ptr [ebp - 4]
0x458101: add      eax, dword ptr [ebx + 0x30]
0x458104: call     0x40ecc0
0x458109: mov      eax, dword ptr [ebp - 4]
0x45810C: mov      eax, dword ptr [eax + 0x278]
0x458112: cmp      eax, dword ptr [ebp - 0xc]
0x458115: jne      0x45814e
0x458117: mov      eax, dword ptr [ebp - 4]
0x45811A: mov      eax, dword ptr [eax + 0x274]
0x458120: cmp      eax, dword ptr [ebp - 0x10]
0x458123: jne      0x45814e
0x458125: mov      eax, dword ptr [ebp - 0xc]
0x458128: push     eax
0x458129: lea      eax, [ebp - 0x28]
0x45812C: push     eax
0x45812D: mov      al, byte ptr [0x458194]
0x458132: push     eax
0x458133: mov      eax, dword ptr [ebp - 8]
0x458136: push     eax
0x458137: mov      ebx, dword ptr [ebp - 4]
0x45813A: mov      ecx, dword ptr [ebp - 0x10]
0x45813D: mov      edx, dword ptr [ebp - 4]
0x458140: mov      eax, dword ptr [ebx + 0x298]
0x458146: call     dword ptr [ebx + 0x294]
0x45814C: jmp      0x458175
0x45814E: mov      eax, dword ptr [ebp - 0xc]
0x458151: push     eax
0x458152: lea      eax, [ebp - 0x28]
0x458155: push     eax
0x458156: mov      al, byte ptr [0x458198]
0x45815B: push     eax
0x45815C: mov      eax, dword ptr [ebp - 8]
0x45815F: push     eax
0x458160: mov      ebx, dword ptr [ebp - 4]
0x458163: mov      ecx, dword ptr [ebp - 0x10]
0x458166: mov      edx, dword ptr [ebp - 4]
0x458169: mov      eax, dword ptr [ebx + 0x298]
0x45816F: call     dword ptr [ebx + 0x294]
0x458175: inc      dword ptr [ebp - 0x10]
0x458178: dec      dword ptr [ebp - 0x18]
0x45817B: jne      0x4580b0
0x458181: inc      dword ptr [ebp - 0xc]
0x458184: dec      dword ptr [ebp - 0x14]
0x458187: jne      0x458093
0x45818D: pop      ebx
0x45818E: mov      esp, ebp
0x458190: pop      ebp
0x458191: ret      
0x458192: add      byte ptr [eax], al
0x458194: add      dword ptr [eax], eax
0x458196: add      byte ptr [eax], al
0x458198: add      byte ptr [eax], al
0x45819A: add      byte ptr [eax], al
0x45819C: push     ebp
0x45819D: mov      ebp, esp
0x45819F: add      esp, -0xc
0x4581A2: test     dl, dl
0x4581A4: je       0x4581ae
0x4581A6: add      esp, -0x10
0x4581A9: call     0x4031a4
0x4581AE: mov      dword ptr [ebp - 0xc], ecx
0x4581B1: mov      byte ptr [ebp - 5], dl
0x4581B4: mov      dword ptr [ebp - 4], eax
0x4581B7: mov      ecx, dword ptr [ebp - 0xc]
0x4581BA: xor      edx, edx
0x4581BC: mov      eax, dword ptr [ebp - 4]
0x4581BF: call     0x457b58
0x4581C4: mov      eax, dword ptr [ebp - 4]
0x4581C7: mov      byte ptr [eax + 0x278], 0
0x4581CE: mov      eax, dword ptr [ebp - 4]
0x4581D1: mov      byte ptr [eax + 0x1f8], 1
0x4581D8: mov      edx, 0x78
0x4581DD: mov      eax, dword ptr [ebp - 4]
0x4581E0: call     0x4271d8
0x4581E5: mov      edx, 0x78
0x4581EA: mov      eax, dword ptr [ebp - 4]
0x4581ED: call     0x4271fc
0x4581F2: mov      eax, dword ptr [ebp - 4]
0x4581F5: cmp      byte ptr [ebp - 5], 0
0x4581F9: je       0x45820a
0x4581FB: call     0x4031fc
0x458200: pop      dword ptr fs:[0]
0x458207: add      esp, 0xc
0x45820A: mov      eax, dword ptr [ebp - 4]
0x45820D: mov      esp, ebp
0x45820F: pop      ebp
0x458210: ret      
0x458211: lea      eax, [eax]
0x458214: push     ebp
0x458215: mov      ebp, esp
0x458217: add      esp, -8
0x45821A: mov      byte ptr [ebp - 5], dl
0x45821D: mov      dword ptr [ebp - 4], eax
0x458220: mov      al, byte ptr [ebp - 5]
0x458223: mov      edx, dword ptr [ebp - 4]
0x458226: mov      byte ptr [edx + 0x24c], al
0x45822C: mov      eax, dword ptr [ebp - 4]
0x45822F: cmp      byte ptr [eax + 0x278], 0
0x458236: je       0x458255
0x458238: mov      eax, dword ptr [ebp - 4]
0x45823B: cmp      dword ptr [eax + 0x1f4], 0
0x458242: je       0x458255
0x458244: mov      edx, dword ptr [ebp - 4]
0x458247: mov      eax, dword ptr [ebp - 4]
0x45824A: mov      eax, dword ptr [eax + 0x1f4]
0x458250: call     0x4570d8
0x458255: pop      ecx
0x458256: pop      ecx
0x458257: pop      ebp
0x458258: ret      
0x458259: lea      eax, [eax]
0x45825C: push     ebp
0x45825D: mov      ebp, esp
0x45825F: add      esp, -0x18
0x458262: mov      dword ptr [ebp - 0xc], ecx
0x458265: mov      byte ptr [ebp - 5], dl
0x458268: mov      dword ptr [ebp - 4], eax
0x45826B: mov      eax, dword ptr [ebp + 8]
0x45826E: push     eax
0x45826F: mov      ecx, dword ptr [ebp - 0xc]
0x458272: mov      dl, byte ptr [ebp - 5]
0x458275: mov      eax, dword ptr [ebp - 4]
0x458278: call     0x457bd0 ; TDButton.DMT_-74
0x45827D: mov      byte ptr [ebp - 0xd], al
0x458280: cmp      byte ptr [ebp - 0xd], 0
0x458284: je       0x458386
0x45828A: mov      eax, dword ptr [ebp - 4]
0x45828D: cmp      byte ptr [eax + 0x278], 0
0x458294: je       0x458386
0x45829A: mov      eax, dword ptr [0x4f7b9c]
0x45829F: cmp      eax, dword ptr [ebp - 4]
0x4582A2: jne      0x458386
0x4582A8: mov      eax, dword ptr [ebp - 4]
0x4582AB: mov      eax, dword ptr [eax + 0x27c]
0x4582B1: cmp      eax, dword ptr [ebp - 0xc]
0x4582B4: jne      0x4582c8
0x4582B6: mov      eax, dword ptr [ebp - 4]
0x4582B9: mov      eax, dword ptr [eax + 0x280]
0x4582BF: cmp      eax, dword ptr [ebp + 8]
0x4582C2: je       0x458386
0x4582C8: mov      eax, dword ptr [ebp - 0xc]
0x4582CB: mov      edx, dword ptr [ebp - 4]
0x4582CE: sub      eax, dword ptr [edx + 0x27c]
0x4582D4: mov      edx, dword ptr [ebp - 4]
0x4582D7: add      eax, dword ptr [edx + 0x30]
0x4582DA: mov      dword ptr [ebp - 0x14], eax
0x4582DD: mov      eax, dword ptr [ebp + 8]
0x4582E0: mov      edx, dword ptr [ebp - 4]
0x4582E3: sub      eax, dword ptr [edx + 0x280]
0x4582E9: mov      edx, dword ptr [ebp - 4]
0x4582EC: add      eax, dword ptr [edx + 0x34]
0x4582EF: mov      dword ptr [ebp - 0x18], eax
0x4582F2: mov      eax, dword ptr [ebp - 0x14]
0x4582F5: mov      edx, dword ptr [ebp - 4]
0x4582F8: add      eax, dword ptr [edx + 0x38]
0x4582FB: cmp      eax, 0x3c
0x4582FE: jge      0x45830e
0x458300: mov      eax, 0x3c
0x458305: mov      edx, dword ptr [ebp - 4]
0x458308: sub      eax, dword ptr [edx + 0x38]
0x45830B: mov      dword ptr [ebp - 0x14], eax
0x45830E: cmp      dword ptr [ebp - 0x14], 0x2e4
0x458315: jle      0x45831e
0x458317: mov      dword ptr [ebp - 0x14], 0x2e4
0x45831E: mov      eax, dword ptr [ebp - 0x18]
0x458321: mov      edx, dword ptr [ebp - 4]
0x458324: add      eax, dword ptr [edx + 0x3c]
0x458327: cmp      eax, 0x3c
0x45832A: jge      0x45833a
0x45832C: mov      eax, 0x3c
0x458331: mov      edx, dword ptr [ebp - 4]
0x458334: sub      eax, dword ptr [edx + 0x3c]
0x458337: mov      dword ptr [ebp - 0x18], eax
0x45833A: mov      eax, dword ptr [ebp - 0x18]
0x45833D: mov      edx, dword ptr [ebp - 4]
0x458340: add      eax, dword ptr [edx + 0x3c]
0x458343: cmp      eax, 0x23a
0x458348: jle      0x458358
0x45834A: mov      eax, 0x23a
0x45834F: mov      edx, dword ptr [ebp - 4]
0x458352: sub      eax, dword ptr [edx + 0x3c]
0x458355: mov      dword ptr [ebp - 0x18], eax
0x458358: mov      edx, dword ptr [ebp - 0x14]
0x45835B: mov      eax, dword ptr [ebp - 4]
0x45835E: call     0x42718c
0x458363: mov      edx, dword ptr [ebp - 0x18]
0x458366: mov      eax, dword ptr [ebp - 4]
0x458369: call     0x4271b0
0x45836E: mov      eax, dword ptr [ebp - 0xc]
0x458371: mov      edx, dword ptr [ebp - 4]
0x458374: mov      dword ptr [edx + 0x27c], eax
0x45837A: mov      eax, dword ptr [ebp + 8]
0x45837D: mov      edx, dword ptr [ebp - 4]
0x458380: mov      dword ptr [edx + 0x280], eax
0x458386: mov      al, byte ptr [ebp - 0xd]
0x458389: mov      esp, ebp
0x45838B: pop      ebp
0x45838C: ret      4
0x45838F: nop      
0x458390: push     ebp
0x458391: mov      ebp, esp
0x458393: add      esp, -8
0x458396: mov      byte ptr [ebp - 6], cl
0x458399: mov      byte ptr [ebp - 5], dl
0x45839C: mov      dword ptr [ebp - 4], eax
0x45839F: mov      eax, dword ptr [ebp + 0xc]
0x4583A2: push     eax
0x4583A3: mov      eax, dword ptr [ebp + 8]
0x4583A6: push     eax
0x4583A7: mov      cl, byte ptr [ebp - 6]
0x4583AA: mov      dl, byte ptr [ebp - 5]
0x4583AD: mov      eax, dword ptr [ebp - 4]
0x4583B0: call     0x457c58 ; TDButton.DMT_-75
0x4583B5: mov      byte ptr [ebp - 7], al
0x4583B8: cmp      byte ptr [ebp - 7], 0
0x4583BC: je       0x4583ff
0x4583BE: mov      eax, dword ptr [ebp - 4]
0x4583C1: cmp      byte ptr [eax + 0x278], 0
0x4583C8: je       0x4583e7
0x4583CA: mov      eax, dword ptr [ebp - 4]
0x4583CD: cmp      dword ptr [eax + 0x1f4], 0
0x4583D4: je       0x4583e7
0x4583D6: mov      edx, dword ptr [ebp - 4]
0x4583D9: mov      eax, dword ptr [ebp - 4]
0x4583DC: mov      eax, dword ptr [eax + 0x1f4]
0x4583E2: call     0x4570d8
0x4583E7: mov      eax, dword ptr [ebp + 0xc]
0x4583EA: mov      edx, dword ptr [ebp - 4]
0x4583ED: mov      dword ptr [edx + 0x27c], eax
0x4583F3: mov      eax, dword ptr [ebp + 8]
0x4583F6: mov      edx, dword ptr [ebp - 4]
0x4583F9: mov      dword ptr [edx + 0x280], eax
0x4583FF: mov      al, byte ptr [ebp - 7]
0x458402: pop      ecx
0x458403: pop      ecx
0x458404: pop      ebp
0x458405: ret      8

; === TDWindow.DMT_-76 @ 0x458408 ===
0x458408: push     ebp
0x458409: mov      ebp, esp
0x45840B: add      esp, -8
0x45840E: mov      byte ptr [ebp - 6], cl
0x458411: mov      byte ptr [ebp - 5], dl
0x458414: mov      dword ptr [ebp - 4], eax
0x458417: mov      eax, dword ptr [ebp + 0xc]
0x45841A: push     eax
0x45841B: mov      eax, dword ptr [ebp + 8]
0x45841E: push     eax
0x45841F: mov      cl, byte ptr [ebp - 6]
0x458422: mov      dl, byte ptr [ebp - 5]
0x458425: mov      eax, dword ptr [ebp - 4]
0x458428: call     0x457cbc ; TDButton.DMT_-76
0x45842D: mov      byte ptr [ebp - 7], al
0x458430: mov      al, byte ptr [ebp - 7]
0x458433: pop      ecx
0x458434: pop      ecx
0x458435: pop      ebp
0x458436: ret      8
0x458439: lea      eax, [eax]
0x45843C: push     ebp
0x45843D: mov      ebp, esp
0x45843F: add      esp, -8
0x458442: mov      dword ptr [ebp - 4], eax
0x458445: mov      dl, 1
0x458447: mov      eax, dword ptr [ebp - 4]
0x45844A: call     0x458214
0x45844F: mov      eax, dword ptr [ebp - 4]
0x458452: mov      dword ptr [0x4f7ba8], eax
0x458457: mov      eax, dword ptr [ebp - 4]
0x45845A: cmp      byte ptr [eax + 0x1f8], 0
0x458461: je       0x45846b
0x458463: mov      eax, dword ptr [ebp - 4]
0x458466: call     0x456ae0
0x45846B: mov      eax, dword ptr [ebp - 8]
0x45846E: pop      ecx
0x45846F: pop      ecx
0x458470: pop      ebp
0x458471: ret      
0x458472: mov      eax, eax
0x458474: push     ebp
0x458475: mov      ebp, esp
0x458477: add      esp, -0xc
0x45847A: test     dl, dl
0x45847C: je       0x458486
0x45847E: add      esp, -0x10
0x458481: call     0x4031a4
0x458486: mov      dword ptr [ebp - 0xc], ecx
0x458489: mov      byte ptr [ebp - 5], dl
0x45848C: mov      dword ptr [ebp - 4], eax
0x45848F: mov      ecx, dword ptr [ebp - 0xc]
0x458492: xor      edx, edx
0x458494: mov      eax, dword ptr [ebp - 4]
0x458497: call     0x4148f4
0x45849C: mov      dl, 1
0x45849E: mov      eax, dword ptr [0x40df00]
0x4584A3: call     0x402e7c
0x4584A8: mov      edx, dword ptr [ebp - 4]
0x4584AB: mov      dword ptr [edx + 0x24], eax
0x4584AE: xor      eax, eax
0x4584B0: mov      dword ptr [0x4f7b9c], eax
0x4584B5: xor      eax, eax
0x4584B7: mov      dword ptr [0x4f7ba0], eax
0x4584BC: mov      eax, dword ptr [ebp - 4]
0x4584BF: cmp      byte ptr [ebp - 5], 0
0x4584C3: je       0x4584d4
0x4584C5: call     0x4031fc
0x4584CA: pop      dword ptr fs:[0]
0x4584D1: add      esp, 0xc
0x4584D4: mov      eax, dword ptr [ebp - 4]
0x4584D7: mov      esp, ebp
0x4584D9: pop      ebp
0x4584DA: ret      
0x4584DB: nop      
0x4584DC: push     ebp
0x4584DD: mov      ebp, esp
0x4584DF: add      esp, -8
0x4584E2: call     0x403204
0x4584E7: mov      byte ptr [ebp - 5], dl
0x4584EA: mov      dword ptr [ebp - 4], eax
0x4584ED: mov      dl, byte ptr [ebp - 5]
0x4584F0: and      dl, 0xfc
0x4584F3: mov      eax, dword ptr [ebp - 4]
0x4584F6: call     0x414940
0x4584FB: cmp      byte ptr [ebp - 5], 0
0x4584FF: jle      0x458509
0x458501: mov      eax, dword ptr [ebp - 4]
0x458504: call     0x4031f4
0x458509: pop      ecx
0x45850A: pop      ecx
0x45850B: pop      ebp
0x45850C: ret      
0x45850D: lea      eax, [eax]
0x458510: push     ebp
0x458511: mov      ebp, esp
0x458513: push     ecx
0x458514: push     ebx
0x458515: mov      dword ptr [ebp - 4], eax
0x458518: mov      eax, dword ptr [ebp - 4]
0x45851B: mov      eax, dword ptr [eax + 0x24]
0x45851E: mov      bx, 0xffff
0x458522: call     0x403080
0x458527: pop      ebx
0x458528: pop      ecx
0x458529: pop      ebp
0x45852A: ret      
0x45852B: nop      
0x45852C: push     ebp
0x45852D: mov      ebp, esp
0x45852F: add      esp, -0xc
0x458532: mov      byte ptr [ebp - 9], cl
0x458535: mov      dword ptr [ebp - 8], edx
0x458538: mov      dword ptr [ebp - 4], eax
0x45853B: mov      al, byte ptr [ebp - 9]
0x45853E: mov      edx, dword ptr [ebp - 8]
0x458541: mov      byte ptr [edx + 0x24c], al
0x458547: mov      edx, dword ptr [ebp - 8]
0x45854A: mov      eax, dword ptr [ebp - 4]
0x45854D: mov      eax, dword ptr [eax + 0x24]
0x458550: call     0x40f588
0x458555: mov      esp, ebp
0x458557: pop      ebp
0x458558: ret      
0x458559: lea      eax, [eax]
0x45855C: push     ebp
0x45855D: mov      ebp, esp
0x45855F: add      esp, -0x10
0x458562: push     ebx
0x458563: mov      dword ptr [ebp - 8], edx
0x458566: mov      dword ptr [ebp - 4], eax
0x458569: mov      byte ptr [ebp - 9], 0
0x45856D: cmp      dword ptr [0x4f7ba8], 0
0x458574: je       0x4585a7
0x458576: mov      eax, dword ptr [0x4f7ba8]
0x45857B: cmp      byte ptr [eax + 0x24c], 0
0x458582: je       0x45859a
0x458584: mov      edx, dword ptr [ebp - 8]
0x458587: mov      eax, dword ptr [0x4f7ba8]
0x45858C: mov      bx, 0xffb8
0x458590: call     0x403080
0x458595: mov      byte ptr [ebp - 9], al
0x458598: jmp      0x4585d9
0x45859A: xor      eax, eax
0x45859C: mov      dword ptr [0x4f7ba8], eax
0x4585A1: mov      eax, dword ptr [ebp - 8]
0x4585A4: mov      byte ptr [eax], 0
0x4585A7: cmp      dword ptr [0x4f7ba0], 0
0x4585AE: je       0x4585d9
0x4585B0: mov      eax, dword ptr [0x4f7ba0]
0x4585B5: cmp      byte ptr [eax + 0x24c], 0
0x4585BC: je       0x4585d4
0x4585BE: mov      edx, dword ptr [ebp - 8]
0x4585C1: mov      eax, dword ptr [0x4f7ba0]
0x4585C6: mov      bx, 0xffb8
0x4585CA: call     0x403080
0x4585CF: mov      byte ptr [ebp - 9], al
0x4585D2: jmp      0x4585d9
0x4585D4: call     0x456af4
0x4585D9: mov      al, byte ptr [ebp - 9]
0x4585DC: pop      ebx
0x4585DD: mov      esp, ebp
0x4585DF: pop      ebp
0x4585E0: ret      
0x4585E1: lea      eax, [eax]
0x4585E4: push     ebp
0x4585E5: mov      ebp, esp
0x4585E7: add      esp, -0x10
0x4585EA: push     ebx
0x4585EB: mov      byte ptr [ebp - 9], cl
0x4585EE: mov      dword ptr [ebp - 8], edx
0x4585F1: mov      dword ptr [ebp - 4], eax
0x4585F4: mov      byte ptr [ebp - 0xa], 0
0x4585F8: cmp      dword ptr [0x4f7ba8], 0
0x4585FF: je       0x45862f
0x458601: mov      eax, dword ptr [0x4f7ba8]
0x458606: cmp      byte ptr [eax + 0x24c], 0
0x45860D: je       0x458628
0x45860F: mov      edx, dword ptr [ebp - 8]
0x458612: mov      cl, byte ptr [ebp - 9]
0x458615: mov      eax, dword ptr [0x4f7ba8]
0x45861A: mov      bx, 0xffb7
0x45861E: call     0x403080
0x458623: mov      byte ptr [ebp - 0xa], al
0x458626: jmp      0x458664
0x458628: xor      eax, eax
0x45862A: mov      dword ptr [0x4f7ba8], eax
0x45862F: cmp      dword ptr [0x4f7ba0], 0
0x458636: je       0x458664
0x458638: mov      eax, dword ptr [0x4f7ba0]
0x45863D: cmp      byte ptr [eax + 0x24c], 0
0x458644: je       0x45865f
0x458646: mov      edx, dword ptr [ebp - 8]
0x458649: mov      cl, byte ptr [ebp - 9]
0x45864C: mov      eax, dword ptr [0x4f7ba0]
0x458651: mov      bx, 0xffb7
0x458655: call     0x403080
0x45865A: mov      byte ptr [ebp - 0xa], al
0x45865D: jmp      0x458664
0x45865F: call     0x456af4
0x458664: mov      al, byte ptr [ebp - 0xa]
0x458667: pop      ebx
0x458668: mov      esp, ebp
0x45866A: pop      ebp
0x45866B: ret      
0x45866C: push     ebp
0x45866D: mov      ebp, esp
0x45866F: add      esp, -0x18
0x458672: push     ebx
0x458673: mov      dword ptr [ebp - 0xc], ecx
0x458676: mov      byte ptr [ebp - 5], dl
0x458679: mov      dword ptr [ebp - 4], eax
0x45867C: mov      byte ptr [ebp - 0xd], 0
0x458680: cmp      dword ptr [0x4f7ba8], 0
0x458687: je       0x4586d5
0x458689: mov      eax, dword ptr [0x4f7ba8]
0x45868E: cmp      byte ptr [eax + 0x24c], 0
0x458695: je       0x4586ce
0x458697: mov      edx, dword ptr [ebp + 8]
0x45869A: mov      eax, dword ptr [0x4f7ba8]
0x45869F: call     0x45706c
0x4586A4: push     eax
0x4586A5: mov      edx, dword ptr [ebp - 0xc]
0x4586A8: mov      eax, dword ptr [0x4f7ba8]
0x4586AD: call     0x457024
0x4586B2: mov      ecx, eax
0x4586B4: mov      dl, byte ptr [ebp - 5]
0x4586B7: mov      eax, dword ptr [0x4f7ba8]
0x4586BC: mov      bx, 0xffb6
0x4586C0: call     0x403080
0x4586C5: mov      byte ptr [ebp - 0xd], 1
0x4586C9: jmp      0x458774
0x4586CE: xor      eax, eax
0x4586D0: mov      dword ptr [0x4f7ba8], eax
0x4586D5: cmp      dword ptr [0x4f7b9c], 0
0x4586DC: je       0x458711
0x4586DE: mov      edx, dword ptr [ebp + 8]
0x4586E1: mov      eax, dword ptr [0x4f7b9c]
0x4586E6: call     0x45706c
0x4586EB: push     eax
0x4586EC: mov      edx, dword ptr [ebp - 0xc]
0x4586EF: mov      eax, dword ptr [0x4f7b9c]
0x4586F4: call     0x457024
0x4586F9: mov      ecx, eax
0x4586FB: mov      dl, byte ptr [ebp - 5]
0x4586FE: mov      eax, dword ptr [0x4f7b9c]
0x458703: mov      bx, 0xffb6
0x458707: call     0x403080
0x45870C: mov      byte ptr [ebp - 0xd], al
0x45870F: jmp      0x458774
0x458711: mov      eax, dword ptr [ebp - 4]
0x458714: mov      eax, dword ptr [eax + 0x24]
0x458717: mov      eax, dword ptr [eax + 8]
0x45871A: dec      eax
0x45871B: test     eax, eax
0x45871D: jl       0x458774
0x45871F: inc      eax
0x458720: mov      dword ptr [ebp - 0x18], eax
0x458723: mov      dword ptr [ebp - 0x14], 0
0x45872A: mov      eax, dword ptr [ebp - 4]
0x45872D: mov      eax, dword ptr [eax + 0x24]
0x458730: mov      edx, dword ptr [ebp - 0x14]
0x458733: call     0x40f6a0
0x458738: cmp      byte ptr [eax + 0x24c], 0
0x45873F: je       0x45876c
0x458741: mov      eax, dword ptr [ebp + 8]
0x458744: push     eax
0x458745: mov      eax, dword ptr [ebp - 4]
0x458748: mov      eax, dword ptr [eax + 0x24]
0x45874B: mov      edx, dword ptr [ebp - 0x14]
0x45874E: call     0x40f6a0
0x458753: mov      ecx, dword ptr [ebp - 0xc]
0x458756: mov      dl, byte ptr [ebp - 5]
0x458759: mov      bx, 0xffb6
0x45875D: call     0x403080
0x458762: test     al, al
0x458764: je       0x45876c
0x458766: mov      byte ptr [ebp - 0xd], 1
0x45876A: jmp      0x458774
0x45876C: inc      dword ptr [ebp - 0x14]
0x45876F: dec      dword ptr [ebp - 0x18]
0x458772: jne      0x45872a
0x458774: mov      al, byte ptr [ebp - 0xd]
0x458777: pop      ebx
0x458778: mov      esp, ebp
0x45877A: pop      ebp
0x45877B: ret      4
0x45877E: mov      eax, eax
0x458780: push     ebp
0x458781: mov      ebp, esp
0x458783: add      esp, -0x10
0x458786: push     ebx
0x458787: mov      byte ptr [ebp - 6], cl
0x45878A: mov      byte ptr [ebp - 5], dl
0x45878D: mov      dword ptr [ebp - 4], eax
0x458790: mov      byte ptr [ebp - 7], 0
0x458794: cmp      dword ptr [0x4f7ba8], 0
0x45879B: je       0x4587eb
0x45879D: mov      eax, dword ptr [0x4f7ba8]
0x4587A2: cmp      byte ptr [eax + 0x24c], 0
0x4587A9: je       0x4587e4
0x4587AB: mov      edx, dword ptr [ebp + 0xc]
0x4587AE: mov      eax, dword ptr [0x4f7ba8]
0x4587B3: call     0x457024
0x4587B8: push     eax
0x4587B9: mov      edx, dword ptr [ebp + 8]
0x4587BC: mov      eax, dword ptr [0x4f7ba8]
0x4587C1: call     0x45706c
0x4587C6: push     eax
0x4587C7: mov      cl, byte ptr [ebp - 6]
0x4587CA: mov      dl, byte ptr [ebp - 5]
0x4587CD: mov      eax, dword ptr [0x4f7ba8]
0x4587D2: mov      bx, 0xffb5
0x4587D6: call     0x403080
0x4587DB: mov      byte ptr [ebp - 7], 1
0x4587DF: jmp      0x458890
0x4587E4: xor      eax, eax
0x4587E6: mov      dword ptr [0x4f7ba8], eax
0x4587EB: cmp      dword ptr [0x4f7b9c], 0
0x4587F2: je       0x458829
0x4587F4: mov      edx, dword ptr [ebp + 0xc]
0x4587F7: mov      eax, dword ptr [0x4f7b9c]
0x4587FC: call     0x457024
0x458801: push     eax
0x458802: mov      edx, dword ptr [ebp + 8]
0x458805: mov      eax, dword ptr [0x4f7b9c]
0x45880A: call     0x45706c
0x45880F: push     eax
0x458810: mov      cl, byte ptr [ebp - 6]
0x458813: mov      dl, byte ptr [ebp - 5]
0x458816: mov      eax, dword ptr [0x4f7b9c]
0x45881B: mov      bx, 0xffb5
0x45881F: call     0x403080
0x458824: mov      byte ptr [ebp - 7], al
0x458827: jmp      0x458890
0x458829: mov      eax, dword ptr [ebp - 4]
0x45882C: mov      eax, dword ptr [eax + 0x24]
0x45882F: mov      eax, dword ptr [eax + 8]
0x458832: dec      eax
0x458833: test     eax, eax
0x458835: jl       0x458890
0x458837: inc      eax
0x458838: mov      dword ptr [ebp - 0x10], eax
0x45883B: mov      dword ptr [ebp - 0xc], 0
0x458842: mov      eax, dword ptr [ebp - 4]
0x458845: mov      eax, dword ptr [eax + 0x24]
0x458848: mov      edx, dword ptr [ebp - 0xc]
0x45884B: call     0x40f6a0
0x458850: cmp      byte ptr [eax + 0x24c], 0
0x458857: je       0x458888
0x458859: mov      eax, dword ptr [ebp + 0xc]
0x45885C: push     eax
0x45885D: mov      eax, dword ptr [ebp + 8]
0x458860: push     eax
0x458861: mov      eax, dword ptr [ebp - 4]
0x458864: mov      eax, dword ptr [eax + 0x24]
0x458867: mov      edx, dword ptr [ebp - 0xc]
0x45886A: call     0x40f6a0
0x45886F: mov      cl, byte ptr [ebp - 6]
0x458872: mov      dl, byte ptr [ebp - 5]
0x458875: mov      bx, 0xffb5
0x458879: call     0x403080
0x45887E: test     al, al
0x458880: je       0x458888
0x458882: mov      byte ptr [ebp - 7], 1
0x458886: jmp      0x458890
0x458888: inc      dword ptr [ebp - 0xc]
0x45888B: dec      dword ptr [ebp - 0x10]
0x45888E: jne      0x458842
0x458890: mov      al, byte ptr [ebp - 7]
0x458893: pop      ebx
0x458894: mov      esp, ebp
0x458896: pop      ebp
0x458897: ret      8
0x45889A: mov      eax, eax
0x45889C: push     ebp
0x45889D: mov      ebp, esp
0x45889F: add      esp, -0x10
0x4588A2: push     ebx
0x4588A3: mov      byte ptr [ebp - 6], cl
0x4588A6: mov      byte ptr [ebp - 5], dl
0x4588A9: mov      dword ptr [ebp - 4], eax
0x4588AC: mov      byte ptr [ebp - 7], 1
0x4588B0: cmp      dword ptr [0x4f7ba8], 0
0x4588B7: je       0x458906
0x4588B9: mov      eax, dword ptr [0x4f7ba8]
0x4588BE: cmp      byte ptr [eax + 0x24c], 0
0x4588C5: je       0x4588ff
0x4588C7: mov      edx, dword ptr [ebp + 0xc]
0x4588CA: mov      eax, dword ptr [0x4f7ba8]
0x4588CF: call     0x457024
0x4588D4: push     eax
0x4588D5: mov      edx, dword ptr [ebp + 8]
0x4588D8: mov      eax, dword ptr [0x4f7ba8]
0x4588DD: call     0x45706c
0x4588E2: push     eax
0x4588E3: mov      cl, byte ptr [ebp - 6]
0x4588E6: mov      dl, byte ptr [ebp - 5]
0x4588E9: mov      eax, dword ptr [0x4f7ba8]
0x4588EE: mov      bx, 0xffb4
0x4588F2: call     0x403080
0x4588F7: mov      byte ptr [ebp - 7], al
0x4588FA: jmp      0x4589ab
0x4588FF: xor      eax, eax
0x458901: mov      dword ptr [0x4f7ba8], eax
0x458906: cmp      dword ptr [0x4f7b9c], 0
0x45890D: je       0x458944
0x45890F: mov      edx, dword ptr [ebp + 0xc]
0x458912: mov      eax, dword ptr [0x4f7b9c]
0x458917: call     0x457024
0x45891C: push     eax
0x45891D: mov      edx, dword ptr [ebp + 8]
0x458920: mov      eax, dword ptr [0x4f7b9c]
0x458925: call     0x45706c
0x45892A: push     eax
0x45892B: mov      cl, byte ptr [ebp - 6]
0x45892E: mov      dl, byte ptr [ebp - 5]
0x458931: mov      eax, dword ptr [0x4f7b9c]
0x458936: mov      bx, 0xffb4
0x45893A: call     0x403080
0x45893F: mov      byte ptr [ebp - 7], al
0x458942: jmp      0x4589ab
0x458944: mov      eax, dword ptr [ebp - 4]
0x458947: mov      eax, dword ptr [eax + 0x24]
0x45894A: mov      eax, dword ptr [eax + 8]
0x45894D: dec      eax
0x45894E: test     eax, eax
0x458950: jl       0x4589ab
0x458952: inc      eax
0x458953: mov      dword ptr [ebp - 0x10], eax
0x458956: mov      dword ptr [ebp - 0xc], 0
0x45895D: mov      eax, dword ptr [ebp - 4]
0x458960: mov      eax, dword ptr [eax + 0x24]
0x458963: mov      edx, dword ptr [ebp - 0xc]
0x458966: call     0x40f6a0
0x45896B: cmp      byte ptr [eax + 0x24c], 0
0x458972: je       0x4589a3
0x458974: mov      eax, dword ptr [ebp + 0xc]
0x458977: push     eax
0x458978: mov      eax, dword ptr [ebp + 8]
0x45897B: push     eax
0x45897C: mov      eax, dword ptr [ebp - 4]
0x45897F: mov      eax, dword ptr [eax + 0x24]
0x458982: mov      edx, dword ptr [ebp - 0xc]
0x458985: call     0x40f6a0
0x45898A: mov      cl, byte ptr [ebp - 6]
0x45898D: mov      dl, byte ptr [ebp - 5]
0x458990: mov      bx, 0xffb4
0x458994: call     0x403080
0x458999: test     al, al
0x45899B: je       0x4589a3
0x45899D: mov      byte ptr [ebp - 7], 1
0x4589A1: jmp      0x4589ab
0x4589A3: inc      dword ptr [ebp - 0xc]
0x4589A6: dec      dword ptr [ebp - 0x10]
0x4589A9: jne      0x45895d
0x4589AB: mov      al, byte ptr [ebp - 7]
0x4589AE: pop      ebx
0x4589AF: mov      esp, ebp
0x4589B1: pop      ebp
0x4589B2: ret      8
0x4589B5: lea      eax, [eax]
0x4589B8: push     ebp
0x4589B9: mov      ebp, esp
0x4589BB: add      esp, -0x18
0x4589BE: push     ebx
0x4589BF: mov      dword ptr [ebp - 0xc], ecx
0x4589C2: mov      dword ptr [ebp - 8], edx
0x4589C5: mov      dword ptr [ebp - 4], eax
0x4589C8: mov      byte ptr [ebp - 0xd], 1
0x4589CC: cmp      dword ptr [0x4f7ba8], 0
0x4589D3: je       0x458a1e
0x4589D5: mov      eax, dword ptr [0x4f7ba8]
0x4589DA: cmp      byte ptr [eax + 0x24c], 0
0x4589E1: je       0x458a17
0x4589E3: mov      edx, dword ptr [ebp - 0xc]
0x4589E6: mov      eax, dword ptr [0x4f7ba8]
0x4589EB: call     0x45706c
0x4589F0: push     eax
0x4589F1: mov      edx, dword ptr [ebp - 8]
0x4589F4: mov      eax, dword ptr [0x4f7ba8]
0x4589F9: call     0x457024
0x4589FE: mov      edx, eax
0x458A00: mov      eax, dword ptr [0x4f7ba8]
0x458A05: pop      ecx
0x458A06: mov      bx, 0xffb3
0x458A0A: call     0x403080
0x458A0F: mov      byte ptr [ebp - 0xd], al
0x458A12: jmp      0x458ab7
0x458A17: xor      eax, eax
0x458A19: mov      dword ptr [0x4f7ba8], eax
0x458A1E: cmp      dword ptr [0x4f7b9c], 0
0x458A25: je       0x458a58
0x458A27: mov      edx, dword ptr [ebp - 0xc]
0x458A2A: mov      eax, dword ptr [0x4f7b9c]
0x458A2F: call     0x45706c
0x458A34: push     eax
0x458A35: mov      edx, dword ptr [ebp - 8]
0x458A38: mov      eax, dword ptr [0x4f7b9c]
0x458A3D: call     0x457024
0x458A42: mov      edx, eax
0x458A44: mov      eax, dword ptr [0x4f7b9c]
0x458A49: pop      ecx
0x458A4A: mov      bx, 0xffb3
0x458A4E: call     0x403080
0x458A53: mov      byte ptr [ebp - 0xd], al
0x458A56: jmp      0x458ab7
0x458A58: mov      eax, dword ptr [ebp - 4]
0x458A5B: mov      eax, dword ptr [eax + 0x24]
0x458A5E: mov      eax, dword ptr [eax + 8]
0x458A61: dec      eax
0x458A62: test     eax, eax
0x458A64: jl       0x458ab7
0x458A66: inc      eax
0x458A67: mov      dword ptr [ebp - 0x18], eax
0x458A6A: mov      dword ptr [ebp - 0x14], 0
0x458A71: mov      eax, dword ptr [ebp - 4]
0x458A74: mov      eax, dword ptr [eax + 0x24]
0x458A77: mov      edx, dword ptr [ebp - 0x14]
0x458A7A: call     0x40f6a0
0x458A7F: cmp      byte ptr [eax + 0x24c], 0
0x458A86: je       0x458aaf
0x458A88: mov      eax, dword ptr [ebp - 4]
0x458A8B: mov      eax, dword ptr [eax + 0x24]
0x458A8E: mov      edx, dword ptr [ebp - 0x14]
0x458A91: call     0x40f6a0
0x458A96: mov      ecx, dword ptr [ebp - 0xc]
0x458A99: mov      edx, dword ptr [ebp - 8]
0x458A9C: mov      bx, 0xffb3
0x458AA0: call     0x403080
0x458AA5: test     al, al
0x458AA7: je       0x458aaf
0x458AA9: mov      byte ptr [ebp - 0xd], 1
0x458AAD: jmp      0x458ab7
0x458AAF: inc      dword ptr [ebp - 0x14]
0x458AB2: dec      dword ptr [ebp - 0x18]
0x458AB5: jne      0x458a71
0x458AB7: mov      al, byte ptr [ebp - 0xd]
0x458ABA: pop      ebx
0x458ABB: mov      esp, ebp
0x458ABD: pop      ebp
0x458ABE: ret      
0x458ABF: nop      
0x458AC0: push     ebp
0x458AC1: mov      ebp, esp
0x458AC3: add      esp, -0x18
0x458AC6: push     ebx
0x458AC7: mov      dword ptr [ebp - 0xc], ecx
0x458ACA: mov      dword ptr [ebp - 8], edx
0x458ACD: mov      dword ptr [ebp - 4], eax
0x458AD0: mov      byte ptr [ebp - 0xd], 1
0x458AD4: cmp      dword ptr [0x4f7ba8], 0
0x458ADB: je       0x458b26
0x458ADD: mov      eax, dword ptr [0x4f7ba8]
0x458AE2: cmp      byte ptr [eax + 0x24c], 0
0x458AE9: je       0x458b1f
0x458AEB: mov      edx, dword ptr [ebp - 0xc]
0x458AEE: mov      eax, dword ptr [0x4f7ba8]
0x458AF3: call     0x45706c
0x458AF8: push     eax
0x458AF9: mov      edx, dword ptr [ebp - 8]
0x458AFC: mov      eax, dword ptr [0x4f7ba8]
0x458B01: call     0x457024
0x458B06: mov      edx, eax
0x458B08: mov      eax, dword ptr [0x4f7ba8]
0x458B0D: pop      ecx
0x458B0E: mov      bx, 0xffb2
0x458B12: call     0x403080
0x458B17: mov      byte ptr [ebp - 0xd], al
0x458B1A: jmp      0x458bbf
0x458B1F: xor      eax, eax
0x458B21: mov      dword ptr [0x4f7ba8], eax
0x458B26: cmp      dword ptr [0x4f7b9c], 0
0x458B2D: je       0x458b60
0x458B2F: mov      edx, dword ptr [ebp - 0xc]
0x458B32: mov      eax, dword ptr [0x4f7b9c]
0x458B37: call     0x45706c
0x458B3C: push     eax
0x458B3D: mov      edx, dword ptr [ebp - 8]
0x458B40: mov      eax, dword ptr [0x4f7b9c]
0x458B45: call     0x457024
0x458B4A: mov      edx, eax
0x458B4C: mov      eax, dword ptr [0x4f7b9c]
0x458B51: pop      ecx
0x458B52: mov      bx, 0xffb2
0x458B56: call     0x403080
0x458B5B: mov      byte ptr [ebp - 0xd], al
0x458B5E: jmp      0x458bbf
0x458B60: mov      eax, dword ptr [ebp - 4]
0x458B63: mov      eax, dword ptr [eax + 0x24]
0x458B66: mov      eax, dword ptr [eax + 8]
0x458B69: dec      eax
0x458B6A: test     eax, eax
0x458B6C: jl       0x458bbf
0x458B6E: inc      eax
0x458B6F: mov      dword ptr [ebp - 0x18], eax
0x458B72: mov      dword ptr [ebp - 0x14], 0
0x458B79: mov      eax, dword ptr [ebp - 4]
0x458B7C: mov      eax, dword ptr [eax + 0x24]
0x458B7F: mov      edx, dword ptr [ebp - 0x14]
0x458B82: call     0x40f6a0
0x458B87: cmp      byte ptr [eax + 0x24c], 0
0x458B8E: je       0x458bb7
0x458B90: mov      eax, dword ptr [ebp - 4]
0x458B93: mov      eax, dword ptr [eax + 0x24]
0x458B96: mov      edx, dword ptr [ebp - 0x14]
0x458B99: call     0x40f6a0
0x458B9E: mov      ecx, dword ptr [ebp - 0xc]
0x458BA1: mov      edx, dword ptr [ebp - 8]
0x458BA4: mov      bx, 0xffb2
0x458BA8: call     0x403080
0x458BAD: test     al, al
0x458BAF: je       0x458bb7
0x458BB1: mov      byte ptr [ebp - 0xd], 1
0x458BB5: jmp      0x458bbf
0x458BB7: inc      dword ptr [ebp - 0x14]
0x458BBA: dec      dword ptr [ebp - 0x18]
0x458BBD: jne      0x458b79
0x458BBF: mov      al, byte ptr [ebp - 0xd]
0x458BC2: pop      ebx
0x458BC3: mov      esp, ebp
0x458BC5: pop      ebp
0x458BC6: ret      
0x458BC7: nop      
0x458BC8: push     ebp
0x458BC9: mov      ebp, esp
0x458BCB: add      esp, -0x10
0x458BCE: push     ebx
0x458BCF: mov      dword ptr [ebp - 8], edx
0x458BD2: mov      dword ptr [ebp - 4], eax
0x458BD5: mov      eax, dword ptr [ebp - 4]
0x458BD8: mov      eax, dword ptr [eax + 0x24]
0x458BDB: mov      eax, dword ptr [eax + 8]
0x458BDE: dec      eax
0x458BDF: test     eax, eax
0x458BE1: jl       0x458c27
0x458BE3: inc      eax
0x458BE4: mov      dword ptr [ebp - 0x10], eax
0x458BE7: mov      dword ptr [ebp - 0xc], 0
0x458BEE: mov      eax, dword ptr [ebp - 4]
0x458BF1: mov      eax, dword ptr [eax + 0x24]
0x458BF4: mov      edx, dword ptr [ebp - 0xc]
0x458BF7: call     0x40f6a0
0x458BFC: cmp      byte ptr [eax + 0x24c], 0
0x458C03: je       0x458c1f
0x458C05: mov      eax, dword ptr [ebp - 4]
0x458C08: mov      eax, dword ptr [eax + 0x24]
0x458C0B: mov      edx, dword ptr [ebp - 0xc]
0x458C0E: call     0x40f6a0
0x458C13: mov      edx, dword ptr [ebp - 8]
0x458C16: mov      bx, 0xffb1
0x458C1A: call     0x403080
0x458C1F: inc      dword ptr [ebp - 0xc]
0x458C22: dec      dword ptr [ebp - 0x10]
0x458C25: jne      0x458bee
0x458C27: cmp      dword ptr [0x4f7ba8], 0
0x458C2E: je       0x458c4f
0x458C30: mov      eax, dword ptr [0x4f7ba8]
0x458C35: cmp      byte ptr [eax + 0x24c], 0
0x458C3C: je       0x458c4f
0x458C3E: mov      edx, dword ptr [ebp - 8]
0x458C41: mov      eax, dword ptr [0x4f7ba8]
0x458C46: mov      bx, 0xffb1
0x458C4A: call     0x403080
0x458C4F: pop      ebx
0x458C50: mov      esp, ebp
0x458C52: pop      ebp
0x458C53: ret      
0x458C54: push     ebp
0x458C55: mov      ebp, esp
0x458C57: xor      eax, eax
0x458C59: push     ebp
0x458C5A: push     0x458c79
0x458C5F: push     dword ptr fs:[eax]
0x458C62: mov      dword ptr fs:[eax], esp
0x458C65: inc      dword ptr [0x4f7bac]
0x458C6B: xor      eax, eax
0x458C6D: pop      edx
0x458C6E: pop      ecx
0x458C6F: pop      ecx
0x458C70: mov      dword ptr fs:[eax], edx
0x458C73: push     0x458c80
0x458C78: ret      
0x458C79: jmp      0x403518
0x458C7E: jmp      0x458c78
0x458C80: pop      ebp
0x458C81: ret      
0x458C82: mov      eax, eax
0x458C84: sub      dword ptr [0x4f7bac], 1
0x458C8B: ret      
0x458C8C: nop      
0x458C8D: mov      word ptr [ebp], es
0x458C90: push     cs
0x458C91: pop      es
0x458C92: push     esp
0x458C93: inc      ebx
0x458C94: push     0x67734d72
0x458C99: and      byte ptr [eax], al
0x458C9B: add      byte ptr [eax], al
0x458C9D: add      dword ptr [eax], eax
0x458C9F: add      byte ptr [eax], al
0x458CA1: xchg     esp, eax
0x458CA2: adc      byte ptr [eax], al
0x458CA5: sbb      byte ptr [eax], al
0x458CA7: add      byte ptr [eax], al
0x458CA9: lea      eax, [eax]
0x458CAC: push     ebp
0x458CAD: mov      ebp, esp
0x458CAF: add      esp, -8
0x458CB2: mov      dword ptr [ebp - 4], eax
0x458CB5: mov      al, byte ptr [ebp - 4]
0x458CB8: mov      byte ptr [ebp - 5], al
0x458CBB: mov      al, byte ptr [ebp - 5]
0x458CBE: pop      ecx
0x458CBF: pop      ecx
0x458CC0: pop      ebp
0x458CC1: ret      
0x458CC2: mov      eax, eax
0x458CC4: push     ebp
0x458CC5: mov      ebp, esp
0x458CC7: add      esp, -8
0x458CCA: mov      dword ptr [ebp - 4], eax
0x458CCD: mov      ax, word ptr [ebp - 4]
0x458CD1: call     0x406974
0x458CD6: mov      byte ptr [ebp - 5], al
0x458CD9: mov      al, byte ptr [ebp - 5]
0x458CDC: pop      ecx
0x458CDD: pop      ecx
0x458CDE: pop      ebp
0x458CDF: ret      
0x458CE0: push     ebp
0x458CE1: mov      ebp, esp
0x458CE3: add      esp, -8
0x458CE6: mov      dword ptr [ebp - 4], eax
0x458CE9: mov      eax, dword ptr [ebp - 4]
0x458CEC: call     0x406970
0x458CF1: mov      byte ptr [ebp - 5], al
0x458CF4: mov      al, byte ptr [ebp - 5]
0x458CF7: pop      ecx
0x458CF8: pop      ecx
0x458CF9: pop      ebp
0x458CFA: ret      
0x458CFB: nop      
0x458CFC: push     ebp
0x458CFD: mov      ebp, esp
0x458CFF: add      esp, -8
0x458D02: mov      dword ptr [ebp - 4], eax
0x458D05: mov      eax, dword ptr [ebp - 4]
0x458D08: call     0x406970
0x458D0D: call     0x406974
0x458D12: mov      byte ptr [ebp - 5], al
0x458D15: mov      al, byte ptr [ebp - 5]
0x458D18: pop      ecx
0x458D19: pop      ecx
0x458D1A: pop      ebp
0x458D1B: ret      
0x458D1C: push     ebp
0x458D1D: mov      ebp, esp
0x458D1F: add      esp, -8
0x458D22: mov      dword ptr [ebp - 4], eax
0x458D25: mov      eax, dword ptr [ebp - 4]
0x458D28: call     0x406970
0x458D2D: mov      word ptr [ebp - 6], ax
0x458D31: mov      ax, word ptr [ebp - 6]
0x458D35: pop      ecx
0x458D36: pop      ecx
0x458D37: pop      ebp
0x458D38: ret      
0x458D39: lea      eax, [eax]
0x458D3C: push     ebp
0x458D3D: mov      ebp, esp
0x458D3F: add      esp, -0xc
0x458D42: mov      word ptr [ebp - 0xa], cx
0x458D46: mov      dword ptr [ebp - 8], edx
0x458D49: mov      word ptr [ebp - 2], ax
0x458D4D: mov      eax, dword ptr [ebp + 8]
0x458D50: mov      dx, word ptr [ebp - 2]
0x458D54: mov      word ptr [eax + 4], dx
0x458D58: mov      eax, dword ptr [ebp + 8]
0x458D5B: mov      edx, dword ptr [ebp - 8]
0x458D5E: mov      dword ptr [eax], edx
0x458D60: mov      eax, dword ptr [ebp + 8]
0x458D63: mov      dx, word ptr [ebp - 0xa]
0x458D67: mov      word ptr [eax + 6], dx
0x458D6B: mov      eax, dword ptr [ebp + 8]
0x458D6E: mov      dx, word ptr [ebp + 0x10]
0x458D72: mov      word ptr [eax + 8], dx
0x458D76: mov      eax, dword ptr [ebp + 8]
0x458D79: mov      dx, word ptr [ebp + 0xc]
0x458D7D: mov      word ptr [eax + 0xa], dx
0x458D81: mov      esp, ebp
0x458D83: pop      ebp
0x458D84: ret      0xc
0x458D87: nop      
0x458D88: push     ebp
0x458D89: mov      ebp, esp
0x458D8B: push     ecx
0x458D8C: fld      qword ptr [ebp + 8]
0x458D8F: call     0x402978
0x458D94: fcomp    qword ptr [ebp + 8]
0x458D97: fnstsw   ax
0x458D99: sahf     
0x458D9A: jae      0x458daa
0x458D9C: fld      qword ptr [ebp + 8]
0x458D9F: call     0x4029ac
0x458DA4: inc      eax
0x458DA5: mov      dword ptr [ebp - 4], eax
0x458DA8: jmp      0x458db5
0x458DAA: fld      qword ptr [ebp + 8]
0x458DAD: call     0x4029ac
0x458DB2: mov      dword ptr [ebp - 4], eax
0x458DB5: mov      eax, dword ptr [ebp - 4]
0x458DB8: pop      ecx
0x458DB9: pop      ebp
0x458DBA: ret      8
0x458DBD: lea      eax, [eax]
0x458DC0: push     ebp
0x458DC1: mov      ebp, esp
0x458DC3: xor      eax, eax
0x458DC5: push     ebp
0x458DC6: push     0x458de5
0x458DCB: push     dword ptr fs:[eax]
0x458DCE: mov      dword ptr fs:[eax], esp
0x458DD1: inc      dword ptr [0x4f7bb0]
0x458DD7: xor      eax, eax
0x458DD9: pop      edx
0x458DDA: pop      ecx
0x458DDB: pop      ecx
0x458DDC: mov      dword ptr fs:[eax], edx
0x458DDF: push     0x458dec
0x458DE4: ret      
0x458DE5: jmp      0x403518
0x458DEA: jmp      0x458de4
0x458DEC: pop      ebp
0x458DED: ret      
0x458DEE: mov      eax, eax
0x458DF0: sub      dword ptr [0x4f7bb0], 1
0x458DF7: ret      
0x458DF8: jmp      dword ptr [0x4fa73c]
0x458DFE: mov      eax, eax
0x458E00: jmp      dword ptr [0x4fa738]
0x458E06: mov      eax, eax
0x458E08: jmp      dword ptr [0x4fa734]
0x458E0E: mov      eax, eax
0x458E10: jmp      dword ptr [0x4fa730]
0x458E16: mov      eax, eax
0x458E18: jmp      dword ptr [0x4fa72c]
0x458E1E: mov      eax, eax
0x458E20: jmp      dword ptr [0x4fa728]
0x458E26: mov      eax, eax
0x458E28: jmp      dword ptr [0x4fa724]
0x458E2E: mov      eax, eax
0x458E30: jmp      dword ptr [0x4fa720]
0x458E36: mov      eax, eax
0x458E38: jmp      dword ptr [0x4fa71c]
0x458E3E: mov      eax, eax
0x458E40: jmp      dword ptr [0x4fa718]
0x458E46: mov      eax, eax
0x458E48: jmp      dword ptr [0x4fa714]
0x458E4E: mov      eax, eax
0x458E50: jmp      dword ptr [0x4fa710]
0x458E56: mov      eax, eax
0x458E58: jmp      dword ptr [0x4fa70c]
0x458E5E: mov      eax, eax
0x458E60: jmp      dword ptr [0x4fa708]
0x458E66: mov      eax, eax
0x458E68: jmp      dword ptr [0x4fa704]
0x458E6E: mov      eax, eax
0x458E70: push     ebp
0x458E71: mov      ebp, esp
0x458E73: xor      eax, eax
0x458E75: push     ebp
0x458E76: push     0x458e95
0x458E7B: push     dword ptr fs:[eax]
0x458E7E: mov      dword ptr fs:[eax], esp
0x458E81: inc      dword ptr [0x4f7bb4]
0x458E87: xor      eax, eax
0x458E89: pop      edx
0x458E8A: pop      ecx
0x458E8B: pop      ecx
0x458E8C: mov      dword ptr fs:[eax], edx
0x458E8F: push     0x458e9c
0x458E94: ret      
0x458E95: jmp      0x403518
0x458E9A: jmp      0x458e94
0x458E9C: pop      ebp
0x458E9D: ret      
0x458E9E: mov      eax, eax
0x458EA0: sub      dword ptr [0x4f7bb4], 1
0x458EA7: ret      
0x458EA8: hlt      
0x458EA9: mov      es, word ptr [ebp]
0x458EAC: add      byte ptr [eax], al
0x458EAE: add      byte ptr [eax], al
0x458EB0: add      byte ptr [eax], al
0x458EB2: add      byte ptr [eax], al
0x458EB4: add      byte ptr [eax], al
0x458EB6: add      byte ptr [eax], al
0x458EB8: add      byte ptr [eax], al
0x458EBA: add      byte ptr [eax], al
0x458EBC: add      byte ptr [eax], al
0x458EBE: add      byte ptr [eax], al
0x458EC0: add      byte ptr [eax], al
0x458EC2: add      byte ptr [eax], al
0x458EC4: add      byte ptr [eax], al
0x458EC6: add      byte ptr [eax], al
0x458EC8: cld      
0x458EC9: mov      es, word ptr [ebp]
0x458ECC: add      al, 0
0x458ECE: add      byte ptr [eax], al
0x458ED0: mov      al, byte ptr [0xc8004010]
0x458ED5: xor      byte ptr [eax], al
0x458ED8: aam      0x30
0x458EDA: inc      eax
0x458EDB: add      al, bl
0x458EDD: xor      byte ptr [eax], al
0x458EE0: fdiv     qword ptr [eax]
0x458EE2: inc      eax
0x458EE3: add      al, dl
0x458EE5: xor      byte ptr [eax], al
0x458EE8: inc      eax
0x458EE9: inc      eax
0x458EEB: add      byte ptr [esi + ebp + 0x40], dl
0x458EF0: pushfd   
0x458EF1: inc      eax
