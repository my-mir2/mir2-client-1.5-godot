
; === VMT_TFrmMain @ 0x45E474 ===
0x45E474: in       al, dx
0x45E475: loop     0x45e4b9
0x45E477: add      byte ptr [eax - 0x2f], bl
0x45E47A: inc      ebx
0x45E47B: add      byte ptr [ebp + edi*8 - 0x31e7ffc0], bh
0x45E482: inc      ebx
0x45E483: add      byte ptr [eax - 0x32], dl
0x45E486: inc      ebx
0x45E487: add      byte ptr [edi + ecx*8 + 0x43], al
0x45E48B: add      byte ptr [edi + esi*2 + 0x42], dl
0x45E48F: add      byte ptr [eax + edx*2 - 0x289bffbf], dh
0x45E496: inc      ebx
0x45E497: add      al, dh
0x45E499: dec      esp
0x45E49A: inc      ecx
0x45E49B: add      byte ptr [esi + ecx + 0x78ec0044], ch
0x45E4A2: inc      esp
0x45E4A3: add      al, al
0x45E4A5: fld      dword ptr [edx]
0x45E4A8: hlt      
0x45E4A9: fld      qword ptr [edx]
0x45E4AC: sbb      al, 0xe3
0x45E4AE: inc      edx
0x45E4AF: add      byte ptr [eax - 0x1e], cl
0x45E4B2: inc      edx
0x45E4B3: add      ah, bl
0x45E4B5: xlatb    
0x45E4B6: inc      edx
0x45E4B7: add      byte ptr [esp + edx*8 + 0x43], al
0x45E4BB: add      byte ptr [eax], dl
0x45E4BD: aad      0x42
0x45E4BF: add      ah, ah
0x45E4C1: insd     dword ptr es:[edi], dx
0x45E4C2: inc      edx
0x45E4C3: add      al, ch
0x45E4C5: insd     dword ptr es:[edi], dx
0x45E4C6: inc      edx
0x45E4C7: add      byte ptr [eax], bh
0x45E4C9: aad      0x43
0x45E4CB: add      byte ptr [eax - 0x67], ah
0x45E4CE: inc      edx
0x45E4CF: add      byte ptr [eax + 0x6f], al
0x45E4D2: inc      edx
0x45E4D3: add      byte ptr [ecx + edi*2], al
0x45E4D6: inc      edx
0x45E4D7: add      byte ptr [edi + edx*8], ch
0x45E4DA: inc      ebx
0x45E4DB: add      al, bh
0x45E4DD: aad      0x43
0x45E4DF: add      byte ptr [edx + edi*2 + 0x42], ah
0x45E4E3: add      al, ch
0x45E4E5: xlatb    
0x45E4E6: inc      ebx
0x45E4E7: add      ah, ah
0x45E4E9: cdq      
0x45E4EA: inc      edx
0x45E4EB: add      byte ptr [eax + 0x240042d5], ch
0x45E4F1: salc     
0x45E4F2: inc      edx
0x45E4F3: add      byte ptr [eax - 0x2f], dl
0x45E4F6: inc      edx
0x45E4F7: add      byte ptr [eax], cl
0x45E4F9: salc     
0x45E4FA: inc      edx
0x45E4FB: add      byte ptr [eax], bl
0x45E4FD: mov      dword ptr [ebx], 0x43de64
0x45E504: clc      
0x45E505: lodsd    eax, dword ptr [esi]
0x45E506: inc      edx
0x45E507: add      al, ah
0x45E509: jns      0x45e54f
0x45E50B: add      ah, bl
0x45E50D: in       al, dx
0x45E50E: inc      ebx
0x45E50F: add      byte ptr [ebx + ebp*8 - 0x1223ffbd], bl
0x45E516: inc      ebx
0x45E517: add      ah, bh
0x45E519: lodsb    al, byte ptr [esi]
0x45E51A: inc      edx
0x45E51B: add      byte ptr [eax - 0x29], cl
0x45E51E: inc      edx
0x45E51F: add      ah, al
0x45E521: hlt      
0x45E522: inc      ebx
0x45E523: add      al, al
0x45E525: rol      dword ptr [edx], cl
0x45E528: mov      word ptr [edi], es
0x45E52A: inc      esp
0x45E52B: add      byte ptr [ecx + eax*8 + 0x43], bh
0x45E52F: add      byte ptr [ebx + ecx], dh
0x45E532: inc      esp
0x45E533: add      al, al
0x45E535: retf     0x43
0x45E538: pushal   
0x45E539: push     es
0x45E53A: inc      esp
0x45E53B: add      byte ptr [eax + 0x240043ef], ch
0x45E541: or       byte ptr [eax + eax + 0x28], al
0x45E545: xlatb    
0x45E546: inc      ebx
0x45E547: add      byte ptr [esi], cl
0x45E549: add      byte ptr [eax], al
0x45E54B: add      byte ptr [eax], al
0x45E54D: add      byte ptr [eax], cl
0x45E54F: add      byte ptr [eax], al
0x45E551: add      byte ptr [eax + edx + 0x3c40040], dl
0x45E558: add      byte ptr [eax], al
0x45E55A: xchg     esp, eax
0x45E55B: adc      byte ptr [eax], al
0x45E55E: enter    3, 0
0x45E562: xchg     esp, eax
0x45E563: adc      byte ptr [eax], al
0x45E566: aam      3
0x45E568: add      byte ptr [eax], al
0x45E56A: xchg     esp, eax
0x45E56B: adc      byte ptr [eax], al
0x45E56E: or       byte ptr [eax + eax], al
0x45E571: add      byte ptr [eax + edx + 0x40c0040], dl
0x45E578: add      byte ptr [eax], al
0x45E57A: xchg     esp, eax
0x45E57B: adc      byte ptr [eax], al
0x45E57E: adc      byte ptr [eax + eax], al
0x45E581: add      byte ptr [eax + edx + 0x4140040], dl
0x45E588: add      byte ptr [eax], al
0x45E58A: xchg     esp, eax
0x45E58B: adc      byte ptr [eax], al
0x45E58E: sbb      byte ptr [eax + eax], al
0x45E591: add      byte ptr [eax + eax], dh
0x45E594: mov      edi, 0xf40045ea
0x45E599: add      al, byte ptr [eax]
0x45E59B: add      byte ptr [eax], al
0x45E59D: add      byte ptr [edi], al
0x45E59F: inc      esp
0x45E5A0: pop      eax
0x45E5A1: inc      esp
0x45E5A2: jb       0x45e605
0x45E5A4: ja       0x45e5d7
0x45E5A6: clc      
0x45E5A7: add      al, byte ptr [eax]
0x45E5A9: add      byte ptr [ecx], al
0x45E5AB: add      byte ptr [esi], al
0x45E5AD: push     edi
0x45E5AE: push     esp
0x45E5AF: imul     ebp, dword ptr [ebp + 0x73], 0x2fc
0x45E5B7: add      dword ptr [eax], eax
0x45E5B9: or       dword ptr [edi + 0x4f], edx
0x45E5BC: bound    ebp, qword ptr [edx + 0x65]
0x45E5BF: arpl     word ptr [ebx + esi*2 + 0x31], si
0x45E5C3: add      byte ptr [ebx], al
0x45E5C5: add      byte ptr [eax], al
0x45E5C7: add      dword ptr [eax], eax
0x45E5C9: or       byte ptr [edi + 0x53], dl
0x45E5CC: insd     dword ptr es:[edi], dx
0x45E5CD: push     esp
0x45E5CE: imul     ebp, dword ptr [ebp + 0x73], 0x304
0x45E5D6: add      dword ptr [eax], eax
0x45E5D8: pop      es
0x45E5D9: push     edi
0x45E5DA: dec      eax
0x45E5DB: jne      0x45e64a
0x45E5DD: dec      ecx
0x45E5DE: insd     dword ptr es:[edi], dx
0x45E5DF: or       byte ptr [bp + di], al
0x45E5E2: add      byte ptr [eax], al
0x45E5E4: add      dword ptr [eax], eax
0x45E5E6: or       byte ptr [edi + 0x50], dl
0x45E5E9: jb       0x45e65a
0x45E5EB: push     ebp
0x45E5ED: jae      0x45e654
0x45E5EF: or       al, 3
0x45E5F1: add      byte ptr [eax], al
0x45E5F3: add      al, byte ptr [eax]
0x45E5F5: pop      es
0x45E5F6: inc      ebx
0x45E5F7: push     ebx
0x45E5F8: outsd    dx, dword ptr [esi]
0x45E5F9: arpl     word ptr [ebx + 0x65], bp
0x45E5FC: je       0x45e60e
0x45E5FE: add      eax, dword ptr [eax]
0x45E600: add      byte ptr [ebx], al
0x45E602: add      byte ptr [esi], al
0x45E604: push     esp
0x45E605: imul     ebp, dword ptr [ebp + 0x65], 0x3143172
0x45E60C: add      byte ptr [eax], al
0x45E60E: add      eax, dword ptr [eax]
0x45E610: or       cl, byte ptr [ebp + 0x6f]
0x45E613: jne      0x45e688 ; "WaitMsgTimer"
0x45E615: push     esp
0x45E617: imul     ebp, dword ptr [ebp + 0x65], 0x31872
0x45E61E: add      byte ptr [ecx], al
0x45E620: add      byte ptr [edi], al
0x45E622: push     edi
0x45E623: dec      ebp
0x45E624: outsd    dx, dword ptr [esi]
0x45E625: outsb    dx, byte ptr [esi]
0x45E626: dec      ecx
0x45E627: insd     dword ptr es:[edi], dx
0x45E628: sbb      al, 3
0x45E62B: add      byte ptr [eax], al
0x45E62D: add      al, 0
0x45E62F: pop      es
0x45E630: inc      esp
0x45E631: push     edi
0x45E632: imul     ebp, dword ptr [esi + 0x4d], 0x3206e61
0x45E639: add      byte ptr [eax], al
0x45E63B: add      dword ptr [eax], eax
0x45E63D: or       byte ptr [edi + 0x48], dl
0x45E640: popal    
0x45E641: imul     esi, dword ptr [edx + 0x49], 0x324676d
0x45E648: add      byte ptr [eax], al
0x45E64A: add      dword ptr [eax], eax
0x45E64C: or       byte ptr [edi + 0x42], dl
0x45E64F: popal    
0x45E650: dec      ecx
0x45E652: je       0x45e6b9
0x45E654: insd     dword ptr es:[edi], dx
0x45E655: sub      byte ptr [ebx], al
0x45E657: add      byte ptr [eax], al
0x45E659: add      dword ptr [eax], eax
0x45E65B: pop      es
0x45E65C: push     edi
0x45E65D: push     edi
0x45E65E: popal    
0x45E660: jo       0x45e6d1
0x45E662: outsb    dx, byte ptr [esi]
0x45E663: sub      al, 3
0x45E665: add      byte ptr [eax], al
0x45E667: add      dword ptr [eax], eax
0x45E669: or       dl, byte ptr [edi + 0x53]
0x45E66C: je       0x45e6cf
0x45E66E: je       0x45e6d5
0x45E670: dec      ecx
0x45E671: je       0x45e6d8
0x45E673: insd     dword ptr es:[edi], dx
0x45E674: xor      byte ptr [ebx], al
0x45E676: add      byte ptr [eax], al
0x45E678: add      dword ptr [eax], eax
0x45E67A: pop      es
0x45E67B: push     edi
0x45E67C: inc      esp
0x45E67D: outsb    dx, byte ptr [esi]
0x45E67E: dec      ecx
0x45E67F: je       0x45e6e6
0x45E681: insd     dword ptr es:[edi], dx
0x45E682: xor      al, 3
0x45E684: add      byte ptr [eax], al
0x45E686: add      eax, dword ptr [eax]
0x45E688: or       al, 0x57
0x45E68A: popal    
0x45E68B: imul     esi, dword ptr [ebp + ecx*2 + 0x73], 0x6d695467
0x45E693: jb       0x45e6ce
0x45E696: add      eax, dword ptr [eax]
0x45E698: add      byte ptr [ebx], al
0x45E69A: add      byte ptr [edi], cl
0x45E69C: push     ebx
0x45E69D: insb     byte ptr es:[edi], dx
0x45E69F: inc      ebx
0x45E6A0: push     0x69615772
0x45E6A5: je       0x45e6fb
0x45E6A7: imul     ebp, dword ptr [ebp + 0x65], 0x33c72
0x45E6AE: add      byte ptr [ecx], al
0x45E6B0: add      byte ptr [esi], al
0x45E6B2: push     edi
0x45E6B3: dec      ebp
0x45E6B4: popal    
0x45E6B5: imul     esp, dword ptr [bp + di + 0x40], 0x3000003
0x45E6BD: add      byte ptr [eax], cl
0x45E6BF: inc      ebx
0x45E6C0: insd     dword ptr es:[edi], dx
0x45E6C1: push     esp
0x45E6C3: imul     ebp, dword ptr [ebp + 0x65], 0x34472
0x45E6CA: add      byte ptr [ecx], al
0x45E6CC: add      byte ptr [edi], al
0x45E6CE: push     edi
0x45E6CF: dec      esi
0x45E6D0: jo       0x45e735
0x45E6D2: dec      ecx
0x45E6D3: insd     dword ptr es:[edi], dx
0x45E6D4: dec      eax
0x45E6D6: add      eax, dword ptr [eax]
0x45E6D8: add      byte ptr [ecx], al
0x45E6DA: add      byte ptr [eax], cl
0x45E6DC: push     edi
0x45E6DD: dec      ebp
0x45E6DE: popal    
0x45E6DF: dec      ecx
0x45E6E1: arpl     word ptr [edi + 0x6e], bp
0x45E6E4: dec      esp
0x45E6E5: add      eax, dword ptr [eax]
0x45E6E7: add      byte ptr [ecx], al
0x45E6E9: add      byte ptr [edi], al
0x45E6EB: push     edi
0x45E6EC: inc      ebx
0x45E6ED: push     0x6c655372
0x45E6F2: push     eax
0x45E6F3: add      eax, dword ptr [eax]
0x45E6F5: add      byte ptr [ebx], al
0x45E6F7: add      byte ptr [eax], cl
0x45E6F9: dec      ebp
0x45E6FA: imul     ebp, dword ptr [esi + 0x54], 0x72656d69
0x45E701: push     esp
0x45E702: add      eax, dword ptr [eax]
0x45E704: add      byte ptr [ecx], al
0x45E706: add      byte ptr [eax], cl
0x45E708: push     edi
0x45E709: dec      ebp
0x45E70A: outsd    dx, dword ptr [esi]
0x45E70B: outsb    dx, byte ptr [esi]
0x45E70C: xor      cl, byte ptr [ecx + 0x6d]
0x45E70F: pop      eax
0x45E711: add      eax, dword ptr [eax]
0x45E713: add      byte ptr [ecx], al
0x45E715: add      byte ptr [eax], cl
0x45E717: push     edi
0x45E718: dec      ebp
0x45E719: outsd    dx, dword ptr [esi]
0x45E71A: outsb    dx, byte ptr [esi]
0x45E71B: xor      ecx, dword ptr [ecx + 0x6d]
0x45E71E: pop      esp
0x45E720: add      eax, dword ptr [eax]
0x45E722: add      byte ptr [ecx], al
0x45E724: add      byte ptr [0x614d4d57], al
0x45E72A: jo       0x45e78c
0x45E72C: add      eax, dword ptr [eax]
0x45E72E: add      byte ptr [ecx], al
0x45E730: add      byte ptr [eax], cl
0x45E732: push     edi
0x45E733: dec      ebp
0x45E734: outsd    dx, dword ptr [esi]
0x45E735: outsb    dx, byte ptr [esi]
0x45E736: xor      al, 0x49
0x45E738: insd     dword ptr es:[edi], dx
0x45E739: add      eax, dword ptr fs:[bx + si]
0x45E73D: add      byte ptr [0x58440700], al
0x45E743: push     ebx
0x45E744: outsd    dx, dword ptr [esi]
0x45E745: jne      0x45e7b5
0x45E747: push     0x1000003
0x45E74D: add      byte ptr [eax], cl
0x45E74F: push     edi
0x45E750: dec      ebp
0x45E751: outsd    dx, dword ptr [esi]
0x45E752: outsb    dx, byte ptr [esi]
0x45E753: xor      eax, 0x6c676d49
0x45E758: add      eax, dword ptr [eax]
0x45E75A: add      byte ptr [ecx], al
0x45E75C: add      byte ptr [eax], cl
0x45E75E: push     edi
0x45E75F: dec      ebp
0x45E760: outsd    dx, dword ptr [esi]
0x45E761: outsb    dx, byte ptr [esi]
0x45E762: dec      ecx
0x45E764: insd     dword ptr es:[edi], dx
0x45E765: jo       0x45e76b
0x45E768: add      byte ptr [eax], al
0x45E76A: add      dword ptr [eax], eax
0x45E76C: or       dl, byte ptr [edi + 0x45]
0x45E76F: arpl     word ptr gs:[ecx + ecx*2 + 0x6d], si
0x45E776: je       0x45e77c
0x45E779: add      byte ptr [eax], al
0x45E77B: add      dword ptr [eax], eax
0x45E77D: or       dword ptr [edi + 0x4f], edx
0x45E780: bound    ebp, qword ptr [edx + 0x65]
0x45E783: arpl     word ptr [ebx + esi*2 + 0x32], si
0x45E787: js       0x45e78c
0x45E789: add      byte ptr [eax], al
0x45E78B: add      dword ptr [eax], eax
0x45E78D: or       dword ptr [edi + 0x4f], edx
0x45E790: bound    ebp, qword ptr [edx + 0x65]
0x45E793: arpl     word ptr [ebx + esi*2 + 0x33], si
0x45E797: jl       0x45e79c
0x45E799: add      byte ptr [eax], al
0x45E79B: add      dword ptr [eax], eax
0x45E79D: or       dword ptr [edi + 0x4f], edx
0x45E7A0: bound    ebp, qword ptr [edx + 0x65]
0x45E7A3: arpl     word ptr [ebx + esi*2 + 0x34], si
0x45E7A7: add      byte ptr [ebx], 0
0x45E7AA: add      byte ptr [ecx], al
0x45E7AC: add      byte ptr [ecx], cl
0x45E7AE: push     edi
0x45E7AF: dec      edi
0x45E7B0: bound    ebp, qword ptr [edx + 0x65]
0x45E7B3: arpl     word ptr [ebx + esi*2 + 0x35], si
0x45E7B7: test     byte ptr [ebx], al
0x45E7B9: add      byte ptr [eax], al
0x45E7BB: add      dword ptr [eax], eax
0x45E7BD: or       dword ptr [edi + 0x4f], edx
0x45E7C0: bound    ebp, qword ptr [edx + 0x65]
0x45E7C3: arpl     word ptr [ebx + esi*2 + 0x36], si
0x45E7C7: mov      byte ptr [ebx], al
0x45E7C9: add      byte ptr [eax], al
0x45E7CB: add      dword ptr [eax], eax
0x45E7CD: or       dword ptr [edi + 0x4f], edx
0x45E7D0: bound    ebp, qword ptr [edx + 0x65]
0x45E7D3: arpl     word ptr [ebx + esi*2 + 0x37], si
0x45E7D7: mov      word ptr [ebx], es
0x45E7D9: add      byte ptr [eax], al
0x45E7DB: add      dword ptr [eax], eax
0x45E7DD: or       byte ptr [edi + 0x4d], dl
0x45E7E0: outsd    dx, dword ptr [esi]
0x45E7E1: outsb    dx, byte ptr [esi]
0x45E7E2: aaa      
0x45E7E3: dec      ecx
0x45E7E4: insd     dword ptr es:[edi], dx
0x45E7E5: nop      
0x45E7E7: add      eax, dword ptr [eax]
0x45E7E9: add      byte ptr [ecx], al
0x45E7EB: add      byte ptr [eax], cl
0x45E7ED: push     edi
0x45E7EE: dec      ebp
0x45E7EF: outsd    dx, dword ptr [esi]
0x45E7F0: outsb    dx, byte ptr [esi]
0x45E7F1: cmp      byte ptr [ecx + 0x6d], cl
0x45E7F4: xchg     esp, eax
0x45E7F6: add      eax, dword ptr [eax]
0x45E7F8: add      byte ptr [ecx], al
0x45E7FA: add      byte ptr [eax], cl
0x45E7FC: push     edi
0x45E7FD: dec      ebp
0x45E7FE: outsd    dx, dword ptr [esi]
0x45E7FF: outsb    dx, byte ptr [esi]
0x45E800: cmp      dword ptr [ecx + 0x6d], ecx
0x45E803: cwde     
0x45E805: add      eax, dword ptr [eax]
0x45E807: add      byte ptr [ecx], al
0x45E809: add      byte ptr [ecx], cl
0x45E80B: push     edi
0x45E80C: dec      ebp
0x45E80D: outsd    dx, dword ptr [esi]
0x45E80E: outsb    dx, byte ptr [esi]
0x45E80F: xor      dword ptr [eax], esi
0x45E811: dec      ecx
0x45E812: insd     dword ptr es:[edi], dx
0x45E813: pushfd   
0x45E815: add      eax, dword ptr [eax]
0x45E817: add      byte ptr [ecx], al
0x45E819: add      byte ptr [ecx], cl
0x45E81B: push     edi
0x45E81C: dec      ebp
0x45E81D: outsd    dx, dword ptr [esi]
0x45E81E: outsb    dx, byte ptr [esi]
0x45E81F: xor      dword ptr [ecx], esi
0x45E821: dec      ecx
0x45E822: insd     dword ptr es:[edi], dx
0x45E823: mov      al, byte ptr [3]
0x45E827: add      byte ptr [ecx], al
0x45E829: add      byte ptr [ecx], cl
0x45E82B: push     edi
0x45E82C: dec      ebp
0x45E82D: outsd    dx, dword ptr [esi]
0x45E82E: outsb    dx, byte ptr [esi]
0x45E82F: xor      dword ptr [edx], esi
0x45E831: dec      ecx
0x45E832: insd     dword ptr es:[edi], dx
0x45E833: movsb    byte ptr es:[di], byte ptr [si]
0x45E835: add      eax, dword ptr [eax]
0x45E837: add      byte ptr [ecx], al
0x45E839: add      byte ptr [ecx], cl
0x45E83B: push     edi
0x45E83C: dec      ebp
0x45E83D: outsd    dx, dword ptr [esi]
0x45E83E: outsb    dx, byte ptr [esi]
0x45E83F: xor      dword ptr [ebx], esi
0x45E841: dec      ecx
0x45E842: insd     dword ptr es:[edi], dx
0x45E843: test     al, 3
0x45E846: add      byte ptr [eax], al
0x45E848: add      dword ptr [eax], eax
0x45E84A: or       dword ptr [edi + 0x4d], edx
0x45E84D: outsd    dx, dword ptr [esi]
0x45E84E: outsb    dx, byte ptr [esi]
0x45E84F: xor      dword ptr [ecx + ecx*2], esi
0x45E852: insd     dword ptr es:[edi], dx
0x45E853: lodsb    al, byte ptr [si]
0x45E855: add      eax, dword ptr [eax]
0x45E857: add      byte ptr [ecx], al
0x45E859: add      byte ptr [ecx], cl
0x45E85B: push     edi
0x45E85C: dec      ebp
0x45E85D: outsd    dx, dword ptr [esi]
0x45E85E: outsb    dx, byte ptr [esi]
0x45E85F: xor      dword ptr [0xb0676d49], esi
0x45E865: add      eax, dword ptr [eax]
0x45E867: add      byte ptr [ecx], al
0x45E869: add      byte ptr [ecx], cl
0x45E86B: push     edi
0x45E86C: dec      ebp
0x45E86D: outsd    dx, dword ptr [esi]
0x45E86E: outsb    dx, byte ptr [esi]
0x45E86F: xor      dword ptr [esi], esi
0x45E871: dec      ecx
0x45E872: insd     dword ptr es:[edi], dx
0x45E873: mov      ah, 3
0x45E876: add      byte ptr [eax], al
0x45E878: add      dword ptr [eax], eax
0x45E87A: or       dword ptr [edi + 0x4d], edx
0x45E87D: outsd    dx, dword ptr [esi]
0x45E87E: outsb    dx, byte ptr [esi]
0x45E87F: xor      dword ptr [edi], esi
0x45E881: dec      ecx
0x45E882: insd     dword ptr es:[edi], dx
0x45E883: mov      eax, 0x1000003
0x45E889: add      byte ptr [ecx], cl
0x45E88B: push     edi
0x45E88C: dec      ebp
0x45E88D: outsd    dx, dword ptr [esi]
0x45E88E: outsb    dx, byte ptr [esi]
0x45E88F: xor      dword ptr [eax], edi
0x45E891: dec      ecx
0x45E892: insd     dword ptr es:[edi], dx
0x45E893: mov      esp, 0x1000003
0x45E899: add      byte ptr [edi], al
0x45E89B: push     edi
0x45E89C: dec      ebp
0x45E89D: popal    
0x45E89E: imul     esp, dword ptr [bp + di + 0x32], 0x3c0
0x45E8A6: add      dword ptr [eax], eax
0x45E8A8: or       dword ptr [edi + 0x50], edx
0x45E8AB: jb       0x45e91c
0x45E8AD: push     ebp
0x45E8AF: jae      0x45e916
0x45E8B1: xor      bl, byte ptr [eax]
0x45E8B3: add      byte ptr [eax], bl
0x45E8B5: add      byte ptr [eax], dl
0x45E8B7: add      byte ptr [esi], al
0x45E8BA: adc      dword ptr [eax + ebx*2 + 0x44], eax
0x45E8BE: jb       0x45e921
0x45E8C0: ja       0x45e8f3
0x45E8C2: dec      ecx
0x45E8C3: outsb    dx, byte ptr [esi]
0x45E8C4: imul     esi, dword ptr [ecx + ebp*2 + 0x61], 0x657a696c
0x45E8CC: adc      dword ptr [eax], eax
0x45E8CE: cwde     
0x45E8CF: int1     
0x45E8D0: inc      ebp
0x45E8D1: add      byte ptr [edx], cl
0x45E8D3: inc      esi
0x45E8D4: outsd    dx, dword ptr [esi]
0x45E8D5: jb       0x45e944
0x45E8D7: inc      ebx
0x45E8D8: jb       0x45e93f
0x45E8DA: popal    
0x45E8DB: je       0x45e942
0x45E8DD: adc      al, byte ptr [eax]
0x45E8DF: push     0xb0045fc
0x45E8E4: inc      esi
0x45E8E5: outsd    dx, dword ptr [esi]
0x45E8E6: jb       0x45e955
0x45E8E8: inc      esp
0x45E8E9: jae      0x45e960
0x45E8EC: jb       0x45e95d
0x45E8EE: jns      0x45e902
0x45E8F0: add      ah, ch
0x45E8F2: sbb      eax, 0x460b0046
0x45E8F7: outsd    dx, dword ptr [esi]
0x45E8F8: jb       0x45e967
0x45E8FA: dec      ebx
0x45E8FB: jns      0x45e942
0x45E8FE: outsd    dx, dword ptr [esi]
0x45E8FF: ja       0x45e96f
0x45E901: pop      ss
0x45E902: add      ah, dh
0x45E904: xor      byte ptr [esi], al
0x45E907: adc      byte ptr [eax + ebx*2 + 0x44], al
0x45E90B: jb       0x45e96e
0x45E90D: ja       0x45e940
0x45E90F: dec      ebp
0x45E910: outsd    dx, dword ptr [esi]
0x45E911: jne      0x45e986
0x45E913: dec      ebp
0x45E915: outsd    dx, dword ptr [esi]
0x45E916: jbe      0x45e97d
0x45E918: pop      ss
0x45E919: add      byte ptr [eax], dl
0x45E91B: xor      eax, dword ptr [esi]
0x45E91E: adc      byte ptr [eax + ebx*2 + 0x44], al
0x45E922: jb       0x45e985
0x45E924: ja       0x45e957
0x45E926: dec      ebp
0x45E927: outsd    dx, dword ptr [esi]
0x45E928: jne      0x45e99d
0x45E92A: inc      esp
0x45E92C: outsd    dx, dword ptr [esi]
0x45E92D: ja       0x45e99d
0x45E92F: adc      eax, dword ptr [eax]
0x45E931: lock and al, byte ptr [esi]
0x45E935: or       al, 0x46
0x45E937: outsd    dx, dword ptr [esi]
0x45E938: jb       0x45e9a7
0x45E93A: dec      ebx
0x45E93B: jns      0x45e98e
0x45E93E: jb       0x45e9a5
0x45E940: jae      0x45e9b5
0x45E942: push     ss
0x45E943: add      ah, bl
0x45E945: or       dword ptr [esi], eax
0x45E948: cmove    ebx, dword ptr [eax + 0x44]
0x45E94C: jb       0x45e9af
0x45E94E: ja       0x45e981
0x45E950: inc      esi
0x45E951: imul     ebp, dword ptr [esi + 0x61], 0x657a696c
0x45E958: adc      eax, 0x46460000
0x45E95D: add      byte ptr [esi], cl
0x45E95F: inc      ebx
0x45E960: push     ebx
0x45E961: outsd    dx, dword ptr [esi]
0x45E962: arpl     word ptr [ebx + 0x65], bp
0x45E965: je       0x45e9aa
0x45E967: outsd    dx, dword ptr [esi]
0x45E968: outsb    dx, byte ptr [esi]
0x45E969: outsb    dx, byte ptr [esi]
0x45E96A: arpl     word ptr gs:[eax + ebx], si
0x45E96F: movsb    byte ptr es:[edi], byte ptr [esi]
0x45E970: dec      eax
0x45E971: inc      esi
0x45E972: add      byte ptr [ecx], dl
0x45E974: inc      ebx
0x45E975: push     ebx
0x45E976: outsd    dx, dword ptr [esi]
0x45E977: arpl     word ptr [ebx + 0x65], bp
0x45E97A: je       0x45e9c0
0x45E97C: imul     esi, dword ptr [ebx + 0x63], 0x656e6e6f
0x45E983: arpl     word ptr [ebx + edx], si
0x45E987: xor      byte ptr [ecx + 0x46], cl
0x45E98A: add      byte ptr [ebx + eax*2], cl
0x45E98D: push     ebx
0x45E98E: outsd    dx, dword ptr [esi]
0x45E98F: arpl     word ptr [ebx + 0x65], bp
0x45E992: je       0x45e9d9
0x45E994: jb       0x45ea08
0x45E996: outsd    dx, dword ptr [esi]
0x45E997: jb       0x45e9ab
0x45E999: add      byte ptr [ecx + ecx*2 + 0x46], dl
0x45E99D: add      byte ptr [ebx], cl
0x45E99F: inc      ebx
0x45E9A0: push     ebx
0x45E9A1: outsd    dx, dword ptr [esi]
0x45E9A2: arpl     word ptr [ebx + 0x65], bp
0x45E9A5: je       0x45e9f9
0x45E9A7: popal    
0x45E9A9: adc      al, byte ptr fs:[eax]
0x45E9AC: mov      al, 0x71
0x45E9AE: inc      esi
0x45E9AF: add      byte ptr [ebx], cl
0x45E9B1: push     esp
0x45E9B2: imul     ebp, dword ptr [ebp + 0x65], 0x69543172
0x45E9B9: insd     dword ptr es:[edi], dx
0x45E9BA: jb       0x45e9d2
0x45E9BD: add      byte ptr [esp + edi + 0x440e0046], dl
0x45E9C4: pop      eax
0x45E9C5: inc      esp
0x45E9C6: jb       0x45ea29
0x45E9C8: ja       0x45e9fb
0x45E9CA: dec      ebp
0x45E9CB: outsd    dx, dword ptr [esi]
0x45E9CC: jne      0x45ea41
0x45E9CE: push     ebp
0x45E9D0: jo       0x45e9e8
0x45E9D2: add      byte ptr [edi + 0x4d0f0046], al
0x45E9D9: outsd    dx, dword ptr [esi]
0x45E9DA: jne      0x45ea4f
0x45E9DC: push     esp
0x45E9DE: imul     ebp, dword ptr [ebp + 0x65], 0x6d695472
0x45E9E5: jb       0x45e9f8
0x45E9E8: add      ah, ch
0x45E9EA: or       dword ptr [esi], eax
0x45E9ED: or       dword ptr [esi + 0x6f], eax
0x45E9F0: jb       0x45ea5f
0x45E9F2: inc      ebx
0x45E9F3: insb     byte ptr es:[edi], dx
0x45E9F4: outsd    dx, dword ptr [esi]
0x45E9F5: jae      0x45ea5c
0x45E9F7: push     ss
0x45E9F8: add      ah, bh
0x45E9FA: cmp      eax, dword ptr [esi]
0x45E9FD: cmove    ebx, dword ptr [eax + 0x44]
0x45EA01: jb       0x45ea64
0x45EA03: ja       0x45ea36
0x45EA05: inc      esp
0x45EA06: bound    ebp, qword ptr [ebx + eax*2 + 0x6c]
0x45EA0A: imul     esp, dword ptr [ebx + 0x6b], 0x3eac0018
0x45EA11: inc      esi
0x45EA12: add      byte ptr [ecx], dl
0x45EA14: push     edi
0x45EA15: popal    
0x45EA16: imul     esi, dword ptr [ebp + ecx*2 + 0x73], 0x6d695467
0x45EA1E: jb       0x45ea75
0x45EA21: imul     ebp, dword ptr [ebp + 0x65], 0xc0001b72
0x45EA28: aas      
0x45EA29: inc      esi
0x45EA2A: add      byte ptr [ebx + edx*2], dl
0x45EA2D: insb     byte ptr es:[edi], dx
0x45EA2F: inc      ebx
0x45EA30: push     0x69615772
0x45EA35: je       0x45ea8b
0x45EA37: imul     ebp, dword ptr [ebp + 0x65], 0x6d695472
0x45EA3E: jb       0x45ea54
0x45EA41: add      ah, dl
0x45EA43: cmp      al, 0x46
0x45EA45: add      byte ptr [esp + eax*2], cl
0x45EA48: pop      eax
0x45EA49: inc      esp
0x45EA4A: jb       0x45eaad
0x45EA4C: ja       0x45ea7f
0x45EA4E: inc      ebx
0x45EA4F: insb     byte ptr es:[edi], dx
0x45EA50: imul     esp, dword ptr [ebx + 0x6b], 0x40140014
0x45EA57: inc      esi
0x45EA58: add      byte ptr [0x54646d43], cl
0x45EA5E: imul     ebp, dword ptr [ebp + 0x65], 0x6d695472
0x45EA65: jb       0x45ea7c
0x45EA68: add      byte ptr [eax + ebp*8 + 0x4d0d0046], cl
0x45EA6F: imul     ebp, dword ptr [esi + 0x54], 0x72656d69
0x45EA76: push     esp
0x45EA77: imul     ebp, dword ptr [ebp + 0x65], 0x78001a72
0x45EA7E: jmp      0x4358eac9
0x45EA83: push     0x486b6365
0x45EA88: popal    
0x45EA89: arpl     word ptr [ebx + 0x54], bp
0x45EA8C: imul     ebp, dword ptr [ebp + 0x65], 0x6d695472
0x45EA93: jb       0x45eaaf
0x45EA96: add      byte ptr [ebx + ebp*8 + 0x53120046], dl
0x45EA9D: outsb    dx, byte ptr gs:[esi]
0x45EA9F: push     esp
0x45EAA1: imul     ebp, dword ptr [ebp + 0x65], 0x656d6954
0x45EAA8: jb       0x45eafe
0x45EAAA: imul     ebp, dword ptr [ebp + 0x65], 0x12000172
0x45EAB1: add      dword ptr [esp + edi*8 + 0x45], ecx
0x45EAB5: add      byte ptr [eax], cl
0x45EAB7: push     esp
0x45EAB8: inc      esi
0x45EAB9: jb       0x45eb28
0x45EABB: dec      ebp
0x45EABC: popal    
0x45EABD: imul     ebp, dword ptr [esi + 6], 0x44bcb400
0x45EAC4: add      byte ptr [esp + ebp - 0x6b63ffbb], ch
0x45EACB: inc      ebp
0x45EACC: add      byte ptr [eax], bl
0x45EACE: dec      esi
0x45EACF: inc      esp
0x45EAD0: add      byte ptr [edx + ebp*2], ch
0x45EAD3: inc      ebp
0x45EAD4: add      byte ptr [edx + ecx*8 + 0x45], dl
0x45EAD8: add      byte ptr [ebp - 0x151fffc0], cl
0x45EADE: inc      ebp
0x45EADF: add      byte ptr [edi], al
0x45EAE1: or       byte ptr [esi + eax*2 + 0x72], dl
0x45EAE5: insd     dword ptr es:[edi], dx
0x45EAE6: dec      ebp
0x45EAE7: popal    
0x45EAE8: imul     ebp, dword ptr [esi + 0x74], 0xf00045e4
0x45EAEF: jne      0x45eb35
0x45EAF1: add      byte ptr [ebp], dl
0x45EAF4: push     es
0x45EAF5: inc      ebx
0x45EAF6: insb     byte ptr es:[edi], dx
0x45EAF7: dec      ebp
0x45EAF8: popal    
0x45EAF9: imul     ebp, dword ptr [esi], 0x408d00
0x45EB00: push     ebp
0x45EB01: mov      ebp, esp
0x45EB03: add      esp, -0xc
0x45EB06: mov      dword ptr [ebp - 0xc], ecx
0x45EB09: mov      dword ptr [ebp - 8], edx
0x45EB0C: mov      dword ptr [ebp - 4], eax
0x45EB0F: mov      eax, 0x3e8
0x45EB14: call     0x402b74
0x45EB19: add      eax, 0xa
0x45EB1C: mov      dword ptr [0x4f9560], eax
0x45EB21: mov      eax, 0x3e8
0x45EB26: call     0x402b74
0x45EB2B: add      eax, 0x64
0x45EB2E: mov      dword ptr [0x4f9564], eax
0x45EB33: mov      eax, 0x3e8
0x45EB38: call     0x402b74
0x45EB3D: add      eax, 0x3e8
0x45EB42: mov      dword ptr [0x4f9568], eax
0x45EB47: mov      eax, 0x64
0x45EB4C: call     0x402b74
0x45EB51: mov      dword ptr [0x4f956c], eax
0x45EB56: mov      eax, 0x2710
0x45EB5B: call     0x402b74
0x45EB60: mov      edx, dword ptr [0x4f9580]
0x45EB66: mov      dword ptr [edx], eax
0x45EB68: mov      eax, 0x2710
0x45EB6D: call     0x402b74
0x45EB72: mov      edx, dword ptr [0x4f9588]
0x45EB78: mov      dword ptr [edx], eax
0x45EB7A: mov      eax, 0x3e8
0x45EB7F: call     0x402b74
0x45EB84: add      eax, 0xa
0x45EB87: mov      dword ptr [0x4f9574], eax
0x45EB8C: mov      eax, 0x3e8
0x45EB91: call     0x402b74
0x45EB96: add      eax, 0x64
0x45EB99: mov      dword ptr [0x4f9578], eax
0x45EB9E: mov      eax, 0x3e8
0x45EBA3: call     0x402b74
0x45EBA8: add      eax, 0x3e8
0x45EBAD: mov      dword ptr [0x4f957c], eax
0x45EBB2: mov      eax, dword ptr [ebp - 8]
0x45EBB5: cdq      
0x45EBB6: xor      eax, edx
0x45EBB8: sub      eax, edx
0x45EBBA: imul     eax, eax, 0x3c
0x45EBBD: imul     edx, dword ptr [ebp - 4], 0xe
0x45EBC1: add      eax, edx
0x45EBC3: add      eax, dword ptr [ebp - 0xc]
0x45EBC6: add      eax, dword ptr [ebp + 8]
0x45EBC9: mov      dword ptr [0x4f9570], eax
0x45EBCE: mov      eax, dword ptr [0x4f9570]
0x45EBD3: shl      eax, 2
0x45EBD6: xor      eax, 0xffffffff
0x45EBD9: mov      edx, dword ptr [0x4f9584]
0x45EBDF: mov      dword ptr [edx], eax
0x45EBE1: mov      eax, dword ptr [0x4f9570]
0x45EBE6: shl      eax, 2
0x45EBE9: lea      eax, [eax + eax*4]
0x45EBEC: xor      eax, 0xffffffff
0x45EBEF: mov      edx, dword ptr [0x4f958c]
0x45EBF5: mov      dword ptr [edx], eax
0x45EBF7: mov      esp, ebp
0x45EBF9: pop      ebp
0x45EBFA: ret      4
0x45EBFD: lea      eax, [eax]
0x45EC00: push     ebp
0x45EC01: mov      ebp, esp
0x45EC03: add      esp, 0xfffffdf8
0x45EC09: mov      byte ptr [ebp - 1], 0
0x45EC0D: lea      eax, [ebp - 0x108]
0x45EC13: mov      edx, 0x45ec60 ; "legend of mir2"
0x45EC18: call     0x40841c
0x45EC1D: lea      eax, [ebp - 0x108]
0x45EC23: push     eax
0x45EC24: push     0
0x45EC26: call     0x4065bc
0x45EC2B: mov      dword ptr [ebp - 8], eax
0x45EC2E: cmp      dword ptr [ebp - 8], 0
0x45EC32: je       0x45ec50
0x45EC34: mov      eax, dword ptr [0x4a4110] ; *0x4A4110->0x4A57A4
0x45EC39: mov      eax, dword ptr [eax]
0x45EC3B: mov      eax, dword ptr [eax + 0x24]
0x45EC3E: cmp      eax, dword ptr [ebp - 8]
0x45EC41: je       0x45ec50
0x45EC43: mov      eax, dword ptr [ebp - 8]
0x45EC46: push     eax
0x45EC47: call     0x406854
0x45EC4C: mov      byte ptr [ebp - 1], 1
0x45EC50: mov      al, byte ptr [ebp - 1]
0x45EC53: mov      esp, ebp
0x45EC55: pop      ebp
0x45EC56: ret      
0x45EC57: add      bh, bh
0x45EC5B: dec      dword ptr [esi]
0x45EC5D: add      byte ptr [eax], al
0x45EC5F: add      byte ptr [ebp + 0x67], ch
0x45EC63: outsb    dx, byte ptr gs:[esi]
0x45EC65: and      byte ptr fs:[edi + 0x66], ch
0x45EC69: and      byte ptr [ebp + 0x69], ch
0x45EC6C: jb       0x45eca0
0x45EC6E: add      byte ptr [eax], al
0x45EC70: push     ebp
0x45EC71: mov      ebp, esp
0x45EC73: add      esp, -0x2c
0x45EC76: mov      dword ptr [ebp - 0xc], ecx
0x45EC79: mov      dword ptr [ebp - 8], edx
0x45EC7C: mov      dword ptr [ebp - 4], eax
0x45EC7F: mov      eax, dword ptr [ebp + 8]
0x45EC82: call     0x403e88
0x45EC87: xor      eax, eax
0x45EC89: push     ebp
0x45EC8A: push     0x45ed88
0x45EC8F: push     dword ptr fs:[eax]
0x45EC92: mov      dword ptr fs:[eax], esp
0x45EC95: mov      eax, dword ptr [ebp + 8]
0x45EC98: call     0x403cd4
0x45EC9D: test     eax, eax
0x45EC9F: jle      0x45ed72
0x45ECA5: mov      dword ptr [ebp - 0x1c], eax
0x45ECA8: mov      dword ptr [ebp - 0x10], 1
0x45ECAF: mov      eax, dword ptr [ebp + 8]
0x45ECB2: mov      edx, dword ptr [ebp - 0x10]
0x45ECB5: movzx    eax, byte ptr [eax + edx - 1]
0x45ECBA: sub      eax, 0x30
0x45ECBD: mov      dword ptr [ebp - 0x14], eax
0x45ECC0: mov      eax, dword ptr [ebp - 0x14]
0x45ECC3: sub      eax, 0xa
0x45ECC6: jae      0x45ed12
0x45ECC8: mov      edx, dword ptr [ebp - 0x14]
0x45ECCB: add      edx, 0x1e
0x45ECCE: mov      eax, dword ptr [0x4f7d88]
0x45ECD3: mov      eax, dword ptr [eax + 0x308]
0x45ECD9: call     0x45351c
0x45ECDE: mov      dword ptr [ebp - 0x18], eax
0x45ECE1: cmp      dword ptr [ebp - 0x18], 0
0x45ECE5: je       0x45ed66
0x45ECE7: lea      edx, [ebp - 0x2c]
0x45ECEA: mov      eax, dword ptr [ebp - 0x18]
0x45ECED: call     0x44d8ec
0x45ECF2: lea      eax, [ebp - 0x2c]
0x45ECF5: push     eax
0x45ECF6: mov      eax, dword ptr [ebp - 0x18]
0x45ECF9: push     eax
0x45ECFA: push     1
0x45ECFC: mov      edx, dword ptr [ebp - 0x10]
0x45ECFF: shl      edx, 3
0x45ED02: add      edx, dword ptr [ebp - 8]
0x45ED05: mov      ecx, dword ptr [ebp - 0xc]
0x45ED08: mov      eax, dword ptr [ebp - 4]
0x45ED0B: call     0x44d6cc
0x45ED10: jmp      0x45ed66
0x45ED12: mov      eax, dword ptr [ebp + 8]
0x45ED15: mov      edx, dword ptr [ebp - 0x10]
0x45ED18: cmp      byte ptr [eax + edx - 1], 0x2d
0x45ED1D: jne      0x45ed66
0x45ED1F: mov      eax, dword ptr [0x4f7d88]
0x45ED24: mov      eax, dword ptr [eax + 0x308]
0x45ED2A: mov      edx, 0x28
0x45ED2F: call     0x45351c
0x45ED34: mov      dword ptr [ebp - 0x18], eax
0x45ED37: cmp      dword ptr [ebp - 0x18], 0
0x45ED3B: je       0x45ed66
0x45ED3D: lea      edx, [ebp - 0x2c]
0x45ED40: mov      eax, dword ptr [ebp - 0x18]
0x45ED43: call     0x44d8ec
0x45ED48: lea      eax, [ebp - 0x2c]
0x45ED4B: push     eax
0x45ED4C: mov      eax, dword ptr [ebp - 0x18]
0x45ED4F: push     eax
0x45ED50: push     1
0x45ED52: mov      edx, dword ptr [ebp - 0x10]
0x45ED55: shl      edx, 3
0x45ED58: add      edx, dword ptr [ebp - 8]
0x45ED5B: mov      ecx, dword ptr [ebp - 0xc]
0x45ED5E: mov      eax, dword ptr [ebp - 4]
0x45ED61: call     0x44d6cc
0x45ED66: inc      dword ptr [ebp - 0x10]
0x45ED69: dec      dword ptr [ebp - 0x1c]
0x45ED6C: jne      0x45ecaf
0x45ED72: xor      eax, eax
0x45ED74: pop      edx
0x45ED75: pop      ecx
0x45ED76: pop      ecx
0x45ED77: mov      dword ptr fs:[eax], edx
0x45ED7A: push     0x45ed8f
0x45ED7F: lea      eax, [ebp + 8]
0x45ED82: call     0x403a58
0x45ED87: ret      
0x45ED88: jmp      0x403518
0x45ED8D: jmp      0x45ed7f
0x45ED8F: mov      esp, ebp
0x45ED91: pop      ebp
0x45ED92: ret      4
0x45ED95: lea      eax, [eax]
0x45ED98: push     ebp
0x45ED99: mov      ebp, esp
0x45ED9B: add      esp, -8
0x45ED9E: mov      dword ptr [ebp - 4], eax
0x45EDA1: call     0x406154
0x45EDA6: mov      dword ptr [ebp - 8], eax
0x45EDA9: jmp      0x45edb7
0x45EDAB: mov      eax, dword ptr [0x4a4110] ; *0x4A4110->0x4A57A4
0x45EDB0: mov      eax, dword ptr [eax]
0x45EDB2: call     0x443574
0x45EDB7: call     0x406154
0x45EDBC: sub      eax, dword ptr [ebp - 8]
0x45EDBF: cmp      eax, dword ptr [ebp - 4]
0x45EDC2: jb       0x45edab
0x45EDC4: pop      ecx
0x45EDC5: pop      ecx
0x45EDC6: pop      ebp
0x45EDC7: ret      
0x45EDC8: push     ebp
0x45EDC9: mov      ebp, esp
0x45EDCB: add      esp, -0xc
0x45EDCE: push     ebx
0x45EDCF: mov      byte ptr [ebp - 1], al
0x45EDD2: xor      eax, eax
0x45EDD4: mov      al, byte ptr [ebp - 1]
0x45EDD7: mov      edx, dword ptr [0x4f7d88]
0x45EDDD: mov      edx, dword ptr [edx + 0x2f4]
0x45EDE3: mov      cl, byte ptr [edx + eax*4 + 0x90c]
0x45EDEA: xor      eax, eax
0x45EDEC: mov      al, byte ptr [ebp - 1]
0x45EDEF: mov      edx, dword ptr [0x4f7d88]
0x45EDF5: mov      edx, dword ptr [edx + 0x2f4]
0x45EDFB: mov      dl, byte ptr [edx + eax*4 + 0x90d]
0x45EE02: xor      eax, eax
0x45EE04: mov      al, byte ptr [ebp - 1]
0x45EE07: mov      ebx, dword ptr [0x4f7d88]
0x45EE0D: mov      ebx, dword ptr [ebx + 0x2f4]
0x45EE13: mov      al, byte ptr [ebx + eax*4 + 0x90e]
0x45EE1A: call     0x4069dc
0x45EE1F: mov      dword ptr [ebp - 8], eax
0x45EE22: mov      eax, dword ptr [ebp - 8]
0x45EE25: pop      ebx
0x45EE26: mov      esp, ebp
0x45EE28: pop      ebp
0x45EE29: ret      
0x45EE2A: mov      eax, eax
0x45EE2C: push     ebp
0x45EE2D: mov      ebp, esp
0x45EE2F: add      esp, 0xfffffe24
0x45EE35: xor      edx, edx
0x45EE37: mov      dword ptr [ebp - 0x1d8], edx
0x45EE3D: mov      dword ptr [ebp - 0x1dc], edx
0x45EE43: mov      dword ptr [ebp - 8], edx
0x45EE46: mov      dword ptr [ebp - 4], eax
0x45EE49: mov      eax, dword ptr [ebp - 4]
0x45EE4C: call     0x403e88
0x45EE51: xor      eax, eax
0x45EE53: push     ebp
0x45EE54: push     0x45ef44
0x45EE59: push     dword ptr fs:[eax]
0x45EE5C: mov      dword ptr fs:[eax], esp
0x45EE5F: jmp      0x45ef19
0x45EE64: lea      eax, [ebp - 8]
0x45EE67: mov      edx, 0x45ef58 ; ".\!debug.txt"
0x45EE6C: call     0x403af0
0x45EE71: mov      eax, dword ptr [ebp - 8]
0x45EE74: call     0x407f90
0x45EE79: test     al, al
0x45EE7B: je       0x45ee9b
0x45EE7D: mov      edx, dword ptr [ebp - 8]
0x45EE80: lea      eax, [ebp - 0x1d4]
0x45EE86: call     0x40554e
0x45EE8B: lea      eax, [ebp - 0x1d4]
0x45EE91: call     0x405675
0x45EE96: call     0x402784
0x45EE9B: mov      edx, dword ptr [ebp - 8]
0x45EE9E: lea      eax, [ebp - 0x1d4]
0x45EEA4: call     0x40554e
0x45EEA9: lea      eax, [ebp - 0x1d4]
0x45EEAF: call     0x40566e
0x45EEB4: call     0x402784
0x45EEB9: call     0x4090bc
0x45EEBE: add      esp, -8
0x45EEC1: fstp     qword ptr [esp]
0x45EEC4: wait     
0x45EEC5: lea      eax, [ebp - 0x1dc]
0x45EECB: call     0x409c74
0x45EED0: push     dword ptr [ebp - 0x1dc]
0x45EED6: push     0x45ef70 ; " "
0x45EEDB: push     dword ptr [ebp - 4]
0x45EEDE: lea      eax, [ebp - 0x1d8]
0x45EEE4: mov      edx, 3
0x45EEE9: call     0x403d94
0x45EEEE: mov      edx, dword ptr [ebp - 0x1d8]
0x45EEF4: lea      eax, [ebp - 0x1d4]
0x45EEFA: call     0x40400c
0x45EEFF: call     0x405747
0x45EF04: call     0x402784
0x45EF09: lea      eax, [ebp - 0x1d4]
0x45EF0F: call     0x4055f0
0x45EF14: call     0x402784
0x45EF19: xor      eax, eax
0x45EF1B: pop      edx
0x45EF1C: pop      ecx
0x45EF1D: pop      ecx
0x45EF1E: mov      dword ptr fs:[eax], edx
0x45EF21: push     0x45ef4b
0x45EF26: lea      eax, [ebp - 0x1dc]
0x45EF2C: mov      edx, 2
0x45EF31: call     0x403a7c
0x45EF36: lea      eax, [ebp - 8]
0x45EF39: mov      edx, 2
0x45EF3E: call     0x403a7c
0x45EF43: ret      
0x45EF44: jmp      0x403518
0x45EF49: jmp      0x45ef26
0x45EF4B: mov      esp, ebp
0x45EF4D: pop      ebp
0x45EF4E: ret      
0x45EF4F: add      bh, bh
0x45EF53: dec      dword ptr [eax + eax]
0x45EF56: add      byte ptr [eax], al
0x45EF58: pop      esp
0x45EF5A: and      dword ptr [ebp + 0x62], esp
0x45EF5E: jne      0x45efc7
0x45EF60: je       0x45efdb
0x45EF63: je       0x45ef65
0x45EF65: add      byte ptr [eax], al
0x45EF67: add      bh, bh
0x45EF6B: inc      dword ptr [ecx]
0x45EF6D: add      byte ptr [eax], al
0x45EF6F: add      byte ptr [eax], ah
0x45EF71: add      byte ptr [eax], al
0x45EF73: add      byte ptr [ebp - 0x75], dl
0x45EF76: in       al, dx
0x45EF77: push     ecx
0x45EF78: cmp      dword ptr [ebp + 0xc], 9
0x45EF7C: jne      0x45efd1
0x45EF7E: cmp      dword ptr [0x4f9554], 0x12
0x45EF85: jne      0x45efd1
0x45EF87: call     0x406154
0x45EF8C: sub      eax, dword ptr [0x4f9558]
0x45EF92: cmp      eax, 0x1f4
0x45EF97: jae      0x45efd1
0x45EF99: mov      eax, dword ptr [0x4f7d88]
0x45EF9E: cmp      byte ptr [eax + 0x20b], 1
0x45EFA5: je       0x45efb5
0x45EFA7: mov      dl, 1
0x45EFA9: mov      eax, dword ptr [0x4f7d88]
0x45EFAE: call     0x43e690
0x45EFB3: jmp      0x45effd
0x45EFB5: mov      eax, dword ptr [ebp + 0x10]
0x45EFB8: push     eax
0x45EFB9: mov      eax, dword ptr [ebp + 0xc]
0x45EFBC: push     eax
0x45EFBD: mov      eax, dword ptr [ebp + 8]
0x45EFC0: push     eax
0x45EFC1: mov      eax, dword ptr [0x4f9550]
0x45EFC6: push     eax
0x45EFC7: call     0x4064c4
0x45EFCC: mov      dword ptr [ebp - 4], eax
0x45EFCF: jmp      0x45effd
0x45EFD1: mov      eax, dword ptr [ebp + 0xc]
0x45EFD4: mov      dword ptr [0x4f9554], eax
0x45EFD9: call     0x406154
0x45EFDE: mov      dword ptr [0x4f9558], eax
0x45EFE3: mov      eax, dword ptr [ebp + 0x10]
0x45EFE6: push     eax
0x45EFE7: mov      eax, dword ptr [ebp + 0xc]
0x45EFEA: push     eax
0x45EFEB: mov      eax, dword ptr [ebp + 8]
0x45EFEE: push     eax
0x45EFEF: mov      eax, dword ptr [0x4f9550]
0x45EFF4: push     eax
0x45EFF5: call     0x4064c4
0x45EFFA: mov      dword ptr [ebp - 4], eax
0x45EFFD: mov      eax, dword ptr [ebp - 4]
0x45F000: pop      ecx
0x45F001: pop      ebp
0x45F002: ret      0xc
0x45F005: lea      eax, [eax]
0x45F008: mov      al, byte ptr [0x4f9390]
0x45F00D: xor      al, 1
0x45F00F: mov      byte ptr [0x4f9390], al
0x45F014: cmp      byte ptr [0x4f9390], 0
0x45F01B: je       0x45f034
0x45F01D: push     0
0x45F01F: mov      ecx, 0xffffff
0x45F024: mov      edx, 0x45f054 ; "[开]"
0x45F029: mov      eax, dword ptr [0x4f7d8c]
0x45F02E: call     0x479ae0
0x45F033: ret      
0x45F034: push     0
0x45F036: mov      ecx, 0xffffff
0x45F03B: mov      edx, 0x45f064 ; "[关]"
0x45F040: mov      eax, dword ptr [0x4f7d8c]
0x45F045: call     0x479ae0
0x45F04A: ret      
0x45F04B: add      bh, bh
0x45F04F: inc      dword ptr [eax + eax]
0x45F052: add      byte ptr [eax], al
0x45F054: pop      ebx
0x45F055: mov      edi, 0x5daa
0x45F05A: add      byte ptr [eax], al
0x45F064: [STR len=4] "[关]"
0x45F06C: push     ebp
0x45F06D: mov      ebp, esp
0x45F06F: add      esp, -0x28
0x45F072: mov      dword ptr [ebp - 4], eax
0x45F075: mov      eax, dword ptr [ebp - 4]
0x45F078: call     0x403e88
0x45F07D: xor      eax, eax
0x45F07F: push     ebp
0x45F080: push     0x45f188
0x45F085: push     dword ptr fs:[eax]
0x45F088: mov      dword ptr fs:[eax], esp
0x45F08B: xor      eax, eax
0x45F08D: mov      dword ptr [ebp - 8], eax
0x45F090: mov      eax, dword ptr [ebp - 4]
0x45F093: call     0x407f90
0x45F098: test     al, al
0x45F09A: je       0x45f172
0x45F0A0: mov      edx, 0x40
0x45F0A5: mov      eax, dword ptr [ebp - 4]
0x45F0A8: call     0x407e58
0x45F0AD: mov      dword ptr [ebp - 0x14], eax
0x45F0B0: cmp      dword ptr [ebp - 0x14], 0
0x45F0B4: jle      0x45f172
0x45F0BA: mov      ecx, 2
0x45F0BF: xor      edx, edx
0x45F0C1: mov      eax, dword ptr [ebp - 0x14]
0x45F0C4: call     0x407f14
0x45F0C9: mov      dword ptr [ebp - 0x18], eax
0x45F0CC: mov      eax, dword ptr [ebp - 0x18]
0x45F0CF: add      eax, 3
0x45F0D2: test     eax, eax
0x45F0D4: jns      0x45f0d9
0x45F0D6: add      eax, 3
0x45F0D9: sar      eax, 2
0x45F0DC: shl      eax, 2
0x45F0DF: call     0x4026b4
0x45F0E4: mov      dword ptr [ebp - 0xc], eax
0x45F0E7: mov      eax, dword ptr [ebp - 0x18]
0x45F0EA: add      eax, 3
0x45F0ED: test     eax, eax
0x45F0EF: jns      0x45f0f4
0x45F0F1: add      eax, 3
0x45F0F4: sar      eax, 2
0x45F0F7: mov      edx, eax
0x45F0F9: shl      edx, 2
0x45F0FC: mov      eax, dword ptr [ebp - 0xc]
0x45F0FF: xor      ecx, ecx
0x45F101: call     0x402b54
0x45F106: xor      ecx, ecx
0x45F108: xor      edx, edx
0x45F10A: mov      eax, dword ptr [ebp - 0x14]
0x45F10D: call     0x407f14
0x45F112: mov      edx, dword ptr [ebp - 0xc]
0x45F115: mov      ecx, dword ptr [ebp - 0x18]
0x45F118: mov      eax, dword ptr [ebp - 0x14]
0x45F11B: call     0x407ebc
0x45F120: mov      eax, dword ptr [ebp - 0x14]
0x45F123: call     0x407f20
0x45F128: xor      eax, eax
0x45F12A: mov      dword ptr [ebp - 0x20], eax
0x45F12D: mov      eax, dword ptr [ebp - 0x18]
0x45F130: add      eax, 3
0x45F133: test     eax, eax
0x45F135: jns      0x45f13a
0x45F137: add      eax, 3
0x45F13A: sar      eax, 2
0x45F13D: dec      eax
0x45F13E: test     eax, eax
0x45F140: jl       0x45f16c
0x45F142: inc      eax
0x45F143: mov      dword ptr [ebp - 0x24], eax
0x45F146: mov      dword ptr [ebp - 0x10], 0
0x45F14D: mov      eax, dword ptr [ebp - 0xc]
0x45F150: mov      eax, dword ptr [eax]
0x45F152: mov      dword ptr [ebp - 0x1c], eax
0x45F155: mov      eax, dword ptr [ebp - 0xc]
0x45F158: add      eax, 4
0x45F15B: mov      dword ptr [ebp - 0xc], eax
0x45F15E: mov      eax, dword ptr [ebp - 0x1c]
0x45F161: xor      dword ptr [ebp - 0x20], eax
0x45F164: inc      dword ptr [ebp - 0x10]
0x45F167: dec      dword ptr [ebp - 0x24]
0x45F16A: jne      0x45f14d
0x45F16C: mov      eax, dword ptr [ebp - 0x20]
0x45F16F: mov      dword ptr [ebp - 8], eax
0x45F172: xor      eax, eax
0x45F174: pop      edx
0x45F175: pop      ecx
0x45F176: pop      ecx
0x45F177: mov      dword ptr fs:[eax], edx
0x45F17A: push     0x45f18f
0x45F17F: lea      eax, [ebp - 4]
0x45F182: call     0x403a58
0x45F187: ret      
0x45F188: jmp      0x403518
0x45F18D: jmp      0x45f17f
0x45F18F: mov      eax, dword ptr [ebp - 8]
0x45F192: mov      esp, ebp
0x45F194: pop      ebp
0x45F195: ret      
0x45F196: mov      eax, eax
0x45F198: push     ebp
0x45F199: mov      ebp, esp
0x45F19B: add      esp, -0x20
0x45F19E: push     ebx
0x45F19F: xor      ecx, ecx
0x45F1A1: mov      dword ptr [ebp - 0x20], ecx
0x45F1A4: mov      dword ptr [ebp - 8], ecx
0x45F1A7: mov      dword ptr [ebp - 0xc], ecx
0x45F1AA: mov      dword ptr [ebp - 0x1c], edx
0x45F1AD: mov      dword ptr [ebp - 4], eax
0x45F1B0: xor      eax, eax
0x45F1B2: push     ebp
0x45F1B3: push     0x45faee
0x45F1B8: push     dword ptr fs:[eax]
0x45F1BB: mov      dword ptr fs:[eax], esp
0x45F1BE: call     0x40291c
0x45F1C3: mov      eax, 4
0x45F1C8: call     0x4026b4
0x45F1CD: mov      dword ptr [0x4f9580], eax
0x45F1D2: mov      eax, 4
0x45F1D7: call     0x4026b4
0x45F1DC: mov      dword ptr [0x4f9584], eax
0x45F1E1: mov      eax, 4
0x45F1E6: call     0x4026b4
0x45F1EB: mov      dword ptr [0x4f9588], eax
0x45F1F0: mov      eax, 4
0x45F1F5: call     0x4026b4
0x45F1FA: mov      dword ptr [0x4f958c], eax
0x45F1FF: mov      eax, 4
0x45F204: call     0x4026b4
0x45F209: mov      dword ptr [0x4f9598], eax
0x45F20E: mov      eax, 4
0x45F213: call     0x4026b4
0x45F218: mov      dword ptr [0x4f959c], eax
0x45F21D: mov      eax, 4
0x45F222: call     0x4026b4
0x45F227: mov      dword ptr [0x4f95a0], eax
0x45F22C: mov      eax, 4
0x45F231: call     0x4026b4
0x45F236: mov      dword ptr [0x4f95a4], eax
0x45F23B: mov      eax, 4
0x45F240: call     0x4026b4
0x45F245: mov      dword ptr [0x4f95a8], eax
0x45F24A: mov      eax, 4
0x45F24F: call     0x4026b4
0x45F254: mov      dword ptr [0x4f95ac], eax
0x45F259: mov      ecx, 0x45fb04 ; ".\mirsetup.ini"
0x45F25E: mov      dl, 1
0x45F260: mov      eax, dword ptr [0x44628c]
0x45F265: call     0x446334
0x45F26A: mov      dword ptr [ebp - 0x10], eax
0x45F26D: cmp      dword ptr [ebp - 0x10], 0
0x45F271: je       0x45f2d2
0x45F273: mov      eax, dword ptr [0x4a2ce4] ; *0x4A2CE4->0x45E330
0x45F278: push     eax
0x45F279: lea      eax, [ebp - 0x20]
0x45F27C: push     eax
0x45F27D: mov      ecx, 0x45fb1c ; "ServerAddr"
0x45F282: mov      edx, 0x45fb30 ; "Setup"
0x45F287: mov      eax, dword ptr [ebp - 0x10]
0x45F28A: mov      ebx, dword ptr [eax]
0x45F28C: call     dword ptr [ebx]
0x45F28E: mov      edx, dword ptr [ebp - 0x20]
0x45F291: mov      eax, 0x4a2ce4 ; *0x4A2CE4->0x45E330
0x45F296: call     0x403aac
0x45F29B: mov      byte ptr [0x4a2ce0], 2 ; *0x4A2CE0->0x408D0A
0x45F2A2: mov      eax, dword ptr [0x4a2d0c] ; *0x4A2D0C->0x45E3E4
0x45F2A7: push     eax
0x45F2A8: lea      eax, [ebp - 0x20]
0x45F2AB: push     eax
0x45F2AC: mov      ecx, 0x45fb40 ; "FontName"
0x45F2B1: mov      edx, 0x45fb30 ; "Setup"
0x45F2B6: mov      eax, dword ptr [ebp - 0x10]
0x45F2B9: mov      ebx, dword ptr [eax]
0x45F2BB: call     dword ptr [ebx]
0x45F2BD: mov      edx, dword ptr [ebp - 0x20]
0x45F2C0: mov      eax, 0x4a2d0c ; *0x4A2D0C->0x45E3E4
0x45F2C5: call     0x403aac
0x45F2CA: mov      eax, dword ptr [ebp - 0x10]
0x45F2CD: call     0x402eac
0x45F2D2: mov      ecx, 0x45fb54 ; ".\ftp.ini"
0x45F2D7: mov      dl, 1
0x45F2D9: mov      eax, dword ptr [0x44628c]
0x45F2DE: call     0x446334
0x45F2E3: mov      dword ptr [ebp - 0x10], eax
0x45F2E6: cmp      dword ptr [ebp - 0x10], 0
0x45F2EA: je       0x45f3d4
0x45F2F0: push     1
0x45F2F2: mov      ecx, 0x45fb68 ; "ServerCount"
0x45F2F7: mov      edx, 0x45fb7c ; "Server"
0x45F2FC: mov      eax, dword ptr [ebp - 0x10]
0x45F2FF: mov      ebx, dword ptr [eax]
0x45F301: call     dword ptr [ebx + 8]
0x45F304: mov      edx, eax
0x45F306: mov      eax, 0x20
0x45F30B: call     0x452b90
0x45F310: mov      dword ptr [0x4f7dd0], eax
0x45F315: mov      eax, dword ptr [0x4f7dd0]
0x45F31A: dec      eax
0x45F31B: test     eax, eax
0x45F31D: jl       0x45f3d4
0x45F323: inc      eax
0x45F324: mov      dword ptr [ebp - 0x18], eax
0x45F327: mov      dword ptr [ebp - 0x14], 0
0x45F32E: push     0x45fb7c ; "Server"
0x45F333: lea      edx, [ebp - 0x20]
0x45F336: mov      eax, dword ptr [ebp - 0x14]
0x45F339: inc      eax
0x45F33A: call     0x407d64
0x45F33F: push     dword ptr [ebp - 0x20]
0x45F342: push     0x45fb8c ; "Caption"
0x45F347: lea      eax, [ebp - 0xc]
0x45F34A: mov      edx, 3
0x45F34F: call     0x403d94
0x45F354: push     0
0x45F356: lea      eax, [ebp - 0x20]
0x45F359: push     eax
0x45F35A: mov      ecx, dword ptr [ebp - 0xc]
0x45F35D: mov      edx, 0x45fb7c ; "Server"
0x45F362: mov      eax, dword ptr [ebp - 0x10]
0x45F365: mov      ebx, dword ptr [eax]
0x45F367: call     dword ptr [ebx]
0x45F369: mov      edx, dword ptr [ebp - 0x20]
0x45F36C: mov      eax, dword ptr [ebp - 0x14]
0x45F36F: lea      eax, [eax*4 + 0x4f7dd4]
0x45F376: call     0x403aac
0x45F37B: push     0x45fb7c ; "Server"
0x45F380: lea      edx, [ebp - 0x20]
0x45F383: mov      eax, dword ptr [ebp - 0x14]
0x45F386: inc      eax
0x45F387: call     0x407d64
0x45F38C: push     dword ptr [ebp - 0x20]
0x45F38F: push     0x45fb9c ; "Name"
0x45F394: lea      eax, [ebp - 0xc]
0x45F397: mov      edx, 3
0x45F39C: call     0x403d94
0x45F3A1: push     0
0x45F3A3: lea      eax, [ebp - 0x20]
0x45F3A6: push     eax
0x45F3A7: mov      ecx, dword ptr [ebp - 0xc]
0x45F3AA: mov      edx, 0x45fb7c ; "Server"
0x45F3AF: mov      eax, dword ptr [ebp - 0x10]
0x45F3B2: mov      ebx, dword ptr [eax]
0x45F3B4: call     dword ptr [ebx]
0x45F3B6: mov      edx, dword ptr [ebp - 0x20]
0x45F3B9: mov      eax, dword ptr [ebp - 0x14]
0x45F3BC: lea      eax, [eax*4 + 0x4f7e54]
0x45F3C3: call     0x403aac
0x45F3C8: inc      dword ptr [ebp - 0x14]
0x45F3CB: dec      dword ptr [ebp - 0x18]
0x45F3CE: jne      0x45f32e
0x45F3D4: mov      eax, dword ptr [ebp - 4]
0x45F3D7: mov      eax, dword ptr [eax + 0x364]
0x45F3DD: cmp      byte ptr [eax + 0x44], 0
0x45F3E1: je       0x45f402
0x45F3E3: mov      eax, dword ptr [ebp - 4]
0x45F3E6: mov      eax, dword ptr [eax + 0x364]
0x45F3EC: mov      ecx, dword ptr [eax + 0x3c]
0x45F3EF: mov      dl, 1
0x45F3F1: mov      eax, dword ptr [0x45c860]
0x45F3F6: call     0x45d79c
0x45F3FB: mov      dword ptr [0x4f7da8], eax
0x45F400: jmp      0x45f409
0x45F402: xor      eax, eax
0x45F404: mov      dword ptr [0x4f7da8], eax
0x45F409: call     0x4060dc
0x45F40E: push     eax
0x45F40F: push     0
0x45F411: mov      eax, 0x45ef74
0x45F416: push     eax
0x45F417: push     2
0x45F419: call     0x4068e4
0x45F41E: mov      dword ptr [0x4f9550], eax
0x45F423: mov      dl, 1
0x45F425: mov      eax, dword ptr [0x40e37c]
0x45F42A: call     0x402e7c
0x45F42F: mov      dword ptr [0x4f7dc8], eax
0x45F434: lea      eax, [ebp - 8]
0x45F437: mov      edx, 0x45fbac ; ".\wav\sound.lst"
0x45F43C: call     0x403af0
0x45F441: mov      eax, dword ptr [ebp - 8]
0x45F444: call     0x495610
0x45F449: mov      dl, 1
0x45F44B: mov      eax, dword ptr [0x479838]
0x45F450: call     0x479890
0x45F455: mov      dword ptr [0x4f7d8c], eax
0x45F45A: mov      dl, 1
0x45F45C: mov      eax, dword ptr [0x47abc0]
0x45F461: call     0x47ae60
0x45F466: mov      dword ptr [0x4f7d90], eax
0x45F46B: mov      dl, 1
0x45F46D: mov      eax, dword ptr [0x47ac2c]
0x45F472: call     0x47af00
0x45F477: mov      dword ptr [0x4f7d94], eax
0x45F47C: mov      dl, 1
0x45F47E: mov      eax, dword ptr [0x47acb4]
0x45F483: call     0x47e574
0x45F488: mov      dword ptr [0x4f7d98], eax
0x45F48D: mov      dl, 1
0x45F48F: mov      eax, dword ptr [0x46ed6c]
0x45F494: call     0x46edec
0x45F499: mov      dword ptr [0x4f7d9c], eax
0x45F49E: mov      dl, 1
0x45F4A0: mov      eax, dword ptr [0x47ad24]
0x45F4A5: call     0x47fd00
0x45F4AA: mov      dword ptr [0x4f7da0], eax
0x45F4AF: mov      dl, 1
0x45F4B1: mov      eax, dword ptr [0x49ea50]
0x45F4B6: call     0x49eac4
0x45F4BB: mov      dword ptr [0x4f7ef4], eax
0x45F4C0: mov      dl, 1
0x45F4C2: mov      eax, dword ptr [0x40df00]
0x45F4C7: call     0x402e7c
0x45F4CC: mov      dword ptr [0x4f7da4], eax
0x45F4D1: mov      dl, 1
0x45F4D3: mov      eax, dword ptr [0x40df00]
0x45F4D8: call     0x402e7c
0x45F4DD: mov      dword ptr [0x4f9190], eax
0x45F4E2: mov      dl, 1
0x45F4E4: mov      eax, dword ptr [0x40df00]
0x45F4E9: call     0x402e7c
0x45F4EE: mov      dword ptr [0x4f9230], eax
0x45F4F3: mov      dl, 1
0x45F4F5: mov      eax, dword ptr [0x477354]
0x45F4FA: call     0x4776e0
0x45F4FF: mov      dword ptr [0x4f7dcc], eax
0x45F504: mov      dl, 1
0x45F506: mov      eax, dword ptr [0x40df00]
0x45F50B: call     0x402e7c
0x45F510: mov      dword ptr [0x4f7dac], eax
0x45F515: xor      eax, eax
0x45F517: mov      dword ptr [0x4f7ef8], eax
0x45F51C: mov      eax, 0x4f7efc
0x45F521: xor      ecx, ecx
0x45F523: mov      edx, 0x1d4
0x45F528: call     0x402b54
0x45F52D: mov      eax, 0x4f80d0
0x45F532: xor      ecx, ecx
0x45F534: mov      edx, 0xa90
0x45F539: call     0x402b54
0x45F53E: mov      eax, 0x4f8b60
0x45F543: xor      ecx, ecx
0x45F545: mov      edx, 0x208
0x45F54A: call     0x402b54
0x45F54F: mov      eax, 0x4f8d68
0x45F554: xor      ecx, ecx
0x45F556: mov      edx, 0x410
0x45F55B: call     0x402b54
0x45F560: mov      dl, 1
0x45F562: mov      eax, dword ptr [0x40df00]
0x45F567: call     0x402e7c
0x45F56C: mov      dword ptr [0x4f9178], eax
0x45F571: mov      dl, 1
0x45F573: mov      eax, dword ptr [0x40df00]
0x45F578: call     0x402e7c
0x45F57D: mov      dword ptr [0x4f917c], eax
0x45F582: mov      byte ptr [0x4f93dc], 0
0x45F589: mov      byte ptr [0x4f9410], 0
0x45F590: mov      dword ptr [0x4f949c], 0xffffffff
0x45F59A: mov      dword ptr [0x4f94a0], 0xffffffff
0x45F5A4: xor      eax, eax
0x45F5A6: mov      dword ptr [0x4f94a4], eax
0x45F5AB: xor      eax, eax
0x45F5AD: mov      dword ptr [0x4f94a8], eax
0x45F5B2: xor      eax, eax
0x45F5B4: mov      dword ptr [0x4f94b4], eax
0x45F5B9: xor      eax, eax
0x45F5BB: mov      dword ptr [0x4f94ac], eax
0x45F5C0: xor      eax, eax
0x45F5C2: mov      dword ptr [0x4f9544], eax
0x45F5C7: xor      eax, eax
0x45F5C9: mov      dword ptr [0x4f9548], eax
0x45F5CE: xor      eax, eax
0x45F5D0: mov      dword ptr [0x4f954c], eax
0x45F5D5: xor      eax, eax
0x45F5D7: mov      dword ptr [0x4f9530], eax
0x45F5DC: xor      eax, eax
0x45F5DE: mov      dword ptr [0x4f9534], eax
0x45F5E3: mov      byte ptr [0x4f9234], 0
0x45F5EA: mov      byte ptr [0x4f9235], 0
0x45F5F1: mov      byte ptr [0x4f94b0], 0
0x45F5F8: xor      eax, eax
0x45F5FA: mov      dword ptr [0x4f9260], eax
0x45F5FF: xor      eax, eax
0x45F601: mov      dword ptr [0x4f923c], eax
0x45F606: xor      eax, eax
0x45F608: mov      dword ptr [0x4f9238], eax
0x45F60D: xor      eax, eax
0x45F60F: mov      dword ptr [0x4f9264], eax
0x45F614: xor      eax, eax
0x45F616: mov      dword ptr [0x4f9268], eax
0x45F61B: xor      eax, eax
0x45F61D: mov      dword ptr [0x4f926c], eax
0x45F622: xor      eax, eax
0x45F624: mov      dword ptr [0x4f9270], eax
0x45F629: xor      eax, eax
0x45F62B: mov      dword ptr [0x4f9240], eax
0x45F630: xor      eax, eax
0x45F632: mov      dword ptr [0x4f9278], eax
0x45F637: xor      eax, eax
0x45F639: mov      dword ptr [0x4f9274], eax
0x45F63E: xor      eax, eax
0x45F640: mov      dword ptr [0x4f9248], eax
0x45F645: xor      eax, eax
0x45F647: mov      dword ptr [0x4f9254], eax
0x45F64C: xor      eax, eax
0x45F64E: mov      dword ptr [0x4f9258], eax
0x45F653: xor      eax, eax
0x45F655: mov      dword ptr [0x4f925c], eax
0x45F65A: mov      dword ptr [0x4f9384], 3
0x45F664: mov      eax, dword ptr [0x4f9384]
0x45F669: mov      dword ptr [0x4f9590], eax
0x45F66E: mov      eax, dword ptr [0x4f9598]
0x45F673: mov      edx, dword ptr [0x4f9384]
0x45F679: mov      dword ptr [eax], edx
0x45F67B: mov      byte ptr [0x4f9382], 1
0x45F682: mov      eax, dword ptr [0x4a4144] ; *0x4A4144->0x4A7294
0x45F687: xor      edx, edx
0x45F689: mov      dword ptr [eax], edx
0x45F68B: mov      eax, dword ptr [0x4a4144] ; *0x4A4144->0x4A7294
0x45F690: mov      eax, dword ptr [eax]
0x45F692: mov      dword ptr [0x4f9594], eax
0x45F697: mov      eax, dword ptr [0x4a4144] ; *0x4A4144->0x4A7294
0x45F69C: mov      eax, dword ptr [eax]
0x45F69E: mov      edx, dword ptr [0x4f959c]
0x45F6A4: mov      dword ptr [edx], eax
0x45F6A6: xor      eax, eax
0x45F6A8: mov      dword ptr [0x4f9388], eax
0x45F6AD: mov      byte ptr [0x4f937f], 0
0x45F6B4: mov      byte ptr [0x4f9380], 0
0x45F6BB: mov      byte ptr [0x4f9381], 0
0x45F6C2: mov      eax, dword ptr [ebp - 4]
0x45F6C5: add      eax, 0x3c4
0x45F6CA: call     0x403a58
0x45F6CF: mov      eax, dword ptr [ebp - 4]
0x45F6D2: xor      edx, edx
0x45F6D4: mov      dword ptr [eax + 0x3cc], edx
0x45F6DA: mov      eax, dword ptr [ebp - 4]
0x45F6DD: mov      byte ptr [eax + 0x3e0], 0
0x45F6E4: mov      byte ptr [0x4f9478], 0
0x45F6EB: mov      byte ptr [0x4f9479], 0
0x45F6F2: mov      byte ptr [0x4f947a], 0
0x45F6F9: mov      byte ptr [0x4f947b], 0
0x45F700: mov      byte ptr [0x4f947c], 0
0x45F707: xor      eax, eax
0x45F709: mov      dword ptr [0x4f9480], eax
0x45F70E: mov      dword ptr [0x4f9484], 1
0x45F718: mov      eax, dword ptr [ebp - 4]
0x45F71B: xor      edx, edx
0x45F71D: mov      dword ptr [eax + 0x3e8], edx
0x45F723: mov      eax, dword ptr [ebp - 4]
0x45F726: xor      edx, edx
0x45F728: mov      dword ptr [eax + 0x3e4], edx
0x45F72E: call     0x406154
0x45F733: mov      edx, dword ptr [ebp - 4]
0x45F736: mov      dword ptr [edx + 0x3ec], eax
0x45F73C: xor      eax, eax
0x45F73E: mov      dword ptr [0x4f94f4], eax
0x45F743: call     0x406154
0x45F748: mov      dword ptr [0x4f9394], eax
0x45F74D: call     0x406154
0x45F752: mov      dword ptr [0x4f9398], eax
0x45F757: call     0x406154
0x45F75C: mov      dword ptr [0x4f944c], eax
0x45F761: mov      byte ptr [0x4f937e], 1
0x45F768: mov      byte ptr [0x4f939c], 0
0x45F76F: mov      byte ptr [0x4f9469], 0
0x45F776: mov      byte ptr [0x4f9468], 0
0x45F77D: mov      byte ptr [0x4f9470], 0
0x45F784: mov      byte ptr [0x4f9471], 0
0x45F78B: mov      byte ptr [0x4f9472], 0
0x45F792: mov      byte ptr [0x4f955c], 0
0x45F799: mov      byte ptr [0x4f955d], 0
0x45F7A0: mov      byte ptr [0x4f955e], 0
0x45F7A7: mov      byte ptr [0x4f955f], 0
0x45F7AE: mov      byte ptr [0x4f9390], 1
0x45F7B5: mov      byte ptr [0x4f94c1], 0
0x45F7BC: mov      byte ptr [0x4f94c8], 0
0x45F7C3: mov      byte ptr [0x4f9370], 0
0x45F7CA: mov      eax, 0x4f9378
0x45F7CF: call     0x403a58
0x45F7D4: mov      byte ptr [0x4f94f8], 0
0x45F7DB: mov      dl, 1
0x45F7DD: mov      eax, dword ptr [0x40e37c]
0x45F7E2: call     0x402e7c
0x45F7E7: mov      dword ptr [0x4f94fc], eax
0x45F7EC: mov      eax, dword ptr [ebp - 4]
0x45F7EF: mov      eax, dword ptr [eax + 0x2f4]
0x45F7F5: call     0x42d734
0x45F7FA: mov      edx, dword ptr [0x4a3e84] ; *0x4A3E84->0x4F7BA4
0x45F800: mov      dword ptr [edx], eax
0x45F802: mov      byte ptr [0x4f937c], 0
0x45F809: mov      byte ptr [0x4f937d], 0
0x45F810: xor      edx, edx
0x45F812: mov      eax, dword ptr [ebp - 4]
0x45F815: mov      eax, dword ptr [eax + 0x30c]
0x45F81B: call     0x45a950
0x45F820: mov      edx, 0x1b58
0x45F825: mov      eax, dword ptr [ebp - 4]
0x45F828: mov      eax, dword ptr [eax + 0x30c]
0x45F82E: call     0x45ab2c
0x45F833: cmp      dword ptr [0x4f7db0], 0
0x45F83A: jne      0x45f855
0x45F83C: mov      edx, dword ptr [0x4a2ce4] ; *0x4A2CE4->0x45E330
0x45F842: mov      eax, dword ptr [ebp - 4]
0x45F845: mov      eax, dword ptr [eax + 0x30c]
0x45F84B: call     0x45a9ec
0x45F850: jmp      0x45f98a
0x45F855: cmp      dword ptr [0x4f7db0], 0
0x45F85C: je       0x45f87b
0x45F85E: cmp      dword ptr [0x4f7db4], 0
0x45F865: jne      0x45f87b
0x45F867: mov      edx, dword ptr [0x4f7db0]
0x45F86D: mov      eax, dword ptr [ebp - 4]
0x45F870: mov      eax, dword ptr [eax + 0x30c]
0x45F876: call     0x45a9ec
0x45F87B: cmp      dword ptr [0x4f7db4], 0
0x45F882: je       0x45f8bd
0x45F884: cmp      dword ptr [0x4f7db8], 0
0x45F88B: jne      0x45f8bd
0x45F88D: mov      edx, dword ptr [0x4f7db0]
0x45F893: mov      eax, dword ptr [ebp - 4]
0x45F896: mov      eax, dword ptr [eax + 0x30c]
0x45F89C: call     0x45a9ec
0x45F8A1: xor      edx, edx
0x45F8A3: mov      eax, dword ptr [0x4f7db4]
0x45F8A8: call     0x4520e8
0x45F8AD: mov      edx, eax
0x45F8AF: mov      eax, dword ptr [ebp - 4]
0x45F8B2: mov      eax, dword ptr [eax + 0x30c]
0x45F8B8: call     0x45ab2c
0x45F8BD: cmp      dword ptr [0x4f7db8], 0
0x45F8C4: je       0x45f98a
0x45F8CA: mov      edx, 0x45fbc4 ; "/KWG"
0x45F8CF: mov      eax, dword ptr [0x4f7db0]
0x45F8D4: call     0x407b04
0x45F8D9: test     eax, eax
0x45F8DB: jne      0x45f953
0x45F8DD: mov      edx, 0x45fbd4 ; "211.48.62.250"
0x45F8E2: mov      eax, dword ptr [ebp - 4]
0x45F8E5: mov      eax, dword ptr [eax + 0x30c]
0x45F8EB: call     0x45a9ec
0x45F8F0: mov      edx, 0x2328
0x45F8F5: mov      eax, dword ptr [ebp - 4]
0x45F8F8: mov      eax, dword ptr [eax + 0x30c]
0x45F8FE: call     0x45ab2c
0x45F903: mov      byte ptr [0x4f937c], 1
0x45F90A: mov      byte ptr [0x4f937d], 1
0x45F911: mov      eax, 0x4f7ed4
0x45F916: mov      edx, dword ptr [0x4f7db4]
0x45F91C: call     0x403aac
0x45F921: mov      eax, 0x4f7ed8
0x45F926: mov      edx, dword ptr [0x4f7db8]
0x45F92C: call     0x403aac
0x45F931: mov      eax, 0x4f7edc
0x45F936: mov      edx, dword ptr [0x4f7dbc]
0x45F93C: call     0x403aac
0x45F941: mov      eax, 0x4f7ee0
0x45F946: mov      edx, dword ptr [0x4f7dc0]
0x45F94C: call     0x403aac
0x45F951: jmp      0x45f98a
0x45F953: mov      edx, dword ptr [0x4f7db4]
0x45F959: mov      eax, dword ptr [ebp - 4]
0x45F95C: mov      eax, dword ptr [eax + 0x30c]
0x45F962: call     0x45a9ec
0x45F967: xor      edx, edx
0x45F969: mov      eax, dword ptr [0x4f7db8]
0x45F96E: call     0x4520e8
0x45F973: mov      edx, eax
0x45F975: mov      eax, dword ptr [ebp - 4]
0x45F978: mov      eax, dword ptr [eax + 0x30c]
0x45F97E: call     0x45ab2c
0x45F983: mov      byte ptr [0x4f937c], 1
0x45F98A: mov      dl, 1
0x45F98C: mov      eax, dword ptr [ebp - 4]
0x45F98F: mov      eax, dword ptr [eax + 0x30c]
0x45F995: call     0x45a950
0x45F99A: mov      ecx, dword ptr [ebp - 4]
0x45F99D: mov      dl, 1
0x45F99F: mov      eax, dword ptr [0x444e18]
0x45F9A4: call     0x44544c
0x45F9A9: mov      edx, dword ptr [ebp - 4]
0x45F9AC: mov      dword ptr [edx + 0x424], eax
0x45F9B2: mov      edx, 0xfa
0x45F9B7: mov      eax, dword ptr [ebp - 4]
0x45F9BA: mov      eax, dword ptr [eax + 0x424]
0x45F9C0: call     0x4455e4
0x45F9C5: mov      dl, 1
0x45F9C7: mov      eax, dword ptr [ebp - 4]
0x45F9CA: mov      eax, dword ptr [eax + 0x424]
0x45F9D0: call     0x4455d4
0x45F9D5: mov      eax, dword ptr [ebp - 4]
0x45F9D8: push     eax
0x45F9D9: push     0x4673f8
0x45F9DE: mov      eax, dword ptr [ebp - 4]
0x45F9E1: mov      eax, dword ptr [eax + 0x424]
0x45F9E7: call     0x4455f4
0x45F9EC: mov      ecx, dword ptr [ebp - 4]
0x45F9EF: mov      dl, 1
0x45F9F1: mov      eax, dword ptr [0x444e18]
0x45F9F6: call     0x44544c
0x45F9FB: mov      edx, dword ptr [ebp - 4]
0x45F9FE: mov      dword ptr [edx + 0x428], eax
0x45FA04: mov      edx, 0x1388
0x45FA09: mov      eax, dword ptr [ebp - 4]
0x45FA0C: mov      eax, dword ptr [eax + 0x428]
0x45FA12: call     0x4455e4
0x45FA17: mov      dl, 1
0x45FA19: mov      eax, dword ptr [ebp - 4]
0x45FA1C: mov      eax, dword ptr [eax + 0x428]
0x45FA22: call     0x4455d4
0x45FA27: mov      eax, dword ptr [ebp - 4]
0x45FA2A: push     eax
0x45FA2B: push     0x467638
0x45FA30: mov      eax, dword ptr [ebp - 4]
0x45FA33: mov      eax, dword ptr [eax + 0x428]
0x45FA39: call     0x4455f4
0x45FA3E: mov      eax, 0x45fbec ; "----------------------- started ------------------------"
0x45FA43: call     0x45ee2c
0x45FA48: mov      eax, dword ptr [0x4a4110] ; *0x4A4110->0x4A57A4
0x45FA4D: mov      eax, dword ptr [eax]
0x45FA4F: mov      edx, dword ptr [ebp - 4]
0x45FA52: mov      dword ptr [eax + 0xcc], edx
0x45FA58: mov      dword ptr [eax + 0xc8], 0x45fc2c
0x45FA62: mov      eax, dword ptr [0x4a4110] ; *0x4A4110->0x4A57A4
0x45FA67: mov      eax, dword ptr [eax]
0x45FA69: mov      edx, dword ptr [ebp - 4]
0x45FA6C: mov      dword ptr [eax + 0xec], edx
0x45FA72: mov      dword ptr [eax + 0xe8], 0x460c50
0x45FA7C: mov      eax, dword ptr [ebp - 4]
0x45FA7F: mov      eax, dword ptr [eax + 0x2f4]
0x45FA85: test     byte ptr [eax + 0x208], 1
0x45FA8C: jne      0x45fab1
0x45FA8E: mov      eax, dword ptr [ebp - 4]
0x45FA91: mov      eax, dword ptr [eax + 0x2f4]
0x45FA97: mov      edx, dword ptr [eax + 0x208]
0x45FA9D: or       edx, dword ptr [0x45fc28]
0x45FAA3: mov      eax, dword ptr [ebp - 4]
0x45FAA6: mov      eax, dword ptr [eax + 0x2f4]
0x45FAAC: call     0x4518d8
0x45FAB1: lea      edx, [ebp - 0x20]
0x45FAB4: xor      eax, eax
0x45FAB6: call     0x402884
0x45FABB: mov      eax, dword ptr [ebp - 0x20]
0x45FABE: call     0x45f06c
0x45FAC3: mov      edx, dword ptr [0x4f95a0]
0x45FAC9: mov      dword ptr [edx], eax
0x45FACB: xor      eax, eax
0x45FACD: pop      edx
0x45FACE: pop      ecx
0x45FACF: pop      ecx
0x45FAD0: mov      dword ptr fs:[eax], edx
0x45FAD3: push     0x45faf5
0x45FAD8: lea      eax, [ebp - 0x20]
0x45FADB: call     0x403a58
0x45FAE0: lea      eax, [ebp - 0xc]
0x45FAE3: mov      edx, 2
0x45FAE8: call     0x403a7c
0x45FAED: ret      
0x45FAEE: jmp      0x403518
0x45FAF3: jmp      0x45fad8
0x45FAF5: pop      ebx
0x45FAF6: mov      esp, ebp
0x45FAF8: pop      ebp
0x45FAF9: ret      
0x45FAFA: add      byte ptr [eax], al
0x45FB04: [STR len=14] ".\mirsetup.ini"
0x45FB1C: [STR len=10] "ServerAddr"
0x45FB30: [STR len=5] "Setup"
0x45FB40: [STR len=8] "FontName"
0x45FB54: [STR len=9] ".\ftp.ini"
0x45FB68: [STR len=11] "ServerCount"
0x45FB7C: [STR len=6] "Server"
0x45FB8C: [STR len=7] "Caption"
0x45FB9C: [STR len=4] "Name"
0x45FBAC: [STR len=15] ".\wav\sound.lst"
0x45FBC4: [STR len=4] "/KWG"
0x45FBD4: [STR len=13] "211.48.62.250"
0x45FBEC: [STR len=56] "----------------------- started ------------------------"
0x45FC28: add      dword ptr [eax], eax
0x45FC2A: add      byte ptr [eax], al
0x45FC2C: push     ebp
0x45FC2D: mov      ebp, esp
0x45FC2F: add      esp, -0xc
0x45FC32: mov      dword ptr [ebp - 8], ecx
0x45FC35: mov      dword ptr [ebp - 0xc], edx
0x45FC38: mov      dword ptr [ebp - 4], eax
0x45FC3B: mov      eax, dword ptr [ebp - 8]
0x45FC3E: mov      eax, dword ptr [eax + 4]
0x45FC41: call     0x45ee2c
0x45FC46: mov      esp, ebp
0x45FC48: pop      ebp
0x45FC49: ret      
0x45FC4A: mov      eax, eax
0x45FC4C: push     ebp
0x45FC4D: mov      ebp, esp
0x45FC4F: add      esp, -8
0x45FC52: mov      dword ptr [ebp - 8], edx
0x45FC55: mov      dword ptr [ebp - 4], eax
0x45FC58: mov      edx, dword ptr [ebp - 8]
0x45FC5B: mov      eax, dword ptr [ebp - 4]
0x45FC5E: call     0x447a08 ; TDXForm.DMT_274
0x45FC63: pop      ecx
0x45FC64: pop      ecx
0x45FC65: pop      ebp
0x45FC66: ret      
0x45FC67: nop      
0x45FC68: push     ebp
0x45FC69: mov      ebp, esp
0x45FC6B: add      esp, -8
0x45FC6E: push     ebx
0x45FC6F: mov      dword ptr [ebp - 8], edx
0x45FC72: mov      dword ptr [ebp - 4], eax
0x45FC75: cmp      dword ptr [0x4f9550], 0
0x45FC7C: je       0x45fc89
0x45FC7E: mov      eax, dword ptr [0x4f9550]
0x45FC83: push     eax
0x45FC84: call     0x406934
0x45FC89: xor      edx, edx
0x45FC8B: mov      eax, dword ptr [ebp - 4]
0x45FC8E: mov      eax, dword ptr [eax + 0x310]
0x45FC94: call     0x4455d4
0x45FC99: xor      edx, edx
0x45FC9B: mov      eax, dword ptr [ebp - 4]
0x45FC9E: mov      eax, dword ptr [eax + 0x350]
0x45FCA4: call     0x4455d4
0x45FCA9: mov      eax, dword ptr [ebp - 4]
0x45FCAC: mov      eax, dword ptr [eax + 0x2f8]
0x45FCB2: call     0x45320c
0x45FCB7: mov      eax, dword ptr [ebp - 4]
0x45FCBA: mov      eax, dword ptr [eax + 0x2fc]
0x45FCC0: call     0x45320c
0x45FCC5: mov      eax, dword ptr [ebp - 4]
0x45FCC8: mov      eax, dword ptr [eax + 0x374]
0x45FCCE: call     0x45320c
0x45FCD3: mov      eax, dword ptr [ebp - 4]
0x45FCD6: mov      eax, dword ptr [eax + 0x378]
0x45FCDC: call     0x45320c
0x45FCE1: mov      eax, dword ptr [ebp - 4]
0x45FCE4: mov      eax, dword ptr [eax + 0x37c]
0x45FCEA: call     0x45320c
0x45FCEF: mov      eax, dword ptr [ebp - 4]
0x45FCF2: mov      eax, dword ptr [eax + 0x380]
0x45FCF8: call     0x45320c
0x45FCFD: mov      eax, dword ptr [ebp - 4]
0x45FD00: mov      eax, dword ptr [eax + 0x384]
0x45FD06: call     0x45320c
0x45FD0B: mov      eax, dword ptr [ebp - 4]
0x45FD0E: mov      eax, dword ptr [eax + 0x388]
0x45FD14: call     0x45320c
0x45FD19: mov      eax, dword ptr [ebp - 4]
0x45FD1C: mov      eax, dword ptr [eax + 0x300]
0x45FD22: call     0x45320c
0x45FD27: mov      eax, dword ptr [ebp - 4]
0x45FD2A: mov      eax, dword ptr [eax + 0x304]
0x45FD30: call     0x45320c
0x45FD35: mov      eax, dword ptr [ebp - 4]
0x45FD38: mov      eax, dword ptr [eax + 0x320]
0x45FD3E: call     0x45320c
0x45FD43: mov      eax, dword ptr [ebp - 4]
0x45FD46: mov      eax, dword ptr [eax + 0x328]
0x45FD4C: call     0x45320c
0x45FD51: mov      eax, dword ptr [ebp - 4]
0x45FD54: mov      eax, dword ptr [eax + 0x33c]
0x45FD5A: call     0x45320c
0x45FD5F: mov      eax, dword ptr [ebp - 4]
0x45FD62: mov      eax, dword ptr [eax + 0x3bc]
0x45FD68: call     0x45320c
0x45FD6D: mov      eax, dword ptr [ebp - 4]
0x45FD70: mov      eax, dword ptr [eax + 0x348]
0x45FD76: call     0x45320c
0x45FD7B: mov      eax, dword ptr [ebp - 4]
0x45FD7E: mov      eax, dword ptr [eax + 0x318]
0x45FD84: call     0x45320c
0x45FD89: mov      eax, dword ptr [ebp - 4]
0x45FD8C: mov      eax, dword ptr [eax + 0x354]
0x45FD92: call     0x45320c
0x45FD97: mov      eax, dword ptr [ebp - 4]
0x45FD9A: mov      eax, dword ptr [eax + 0x358]
0x45FDA0: call     0x45320c
0x45FDA5: mov      eax, dword ptr [ebp - 4]
0x45FDA8: mov      eax, dword ptr [eax + 0x360]
0x45FDAE: call     0x45320c
0x45FDB3: mov      eax, dword ptr [ebp - 4]
0x45FDB6: mov      eax, dword ptr [eax + 0x368]
0x45FDBC: call     0x45320c
0x45FDC1: mov      eax, dword ptr [ebp - 4]
0x45FDC4: mov      eax, dword ptr [eax + 0x36c]
0x45FDCA: call     0x45320c
0x45FDCF: mov      eax, dword ptr [ebp - 4]
0x45FDD2: mov      eax, dword ptr [eax + 0x38c]
0x45FDD8: call     0x45320c
0x45FDDD: mov      eax, dword ptr [ebp - 4]
0x45FDE0: mov      eax, dword ptr [eax + 0x390]
0x45FDE6: call     0x45320c
0x45FDEB: mov      eax, dword ptr [ebp - 4]
0x45FDEE: mov      eax, dword ptr [eax + 0x394]
0x45FDF4: call     0x45320c
0x45FDF9: mov      eax, dword ptr [ebp - 4]
0x45FDFC: mov      eax, dword ptr [eax + 0x398]
0x45FE02: call     0x45320c
0x45FE07: mov      eax, dword ptr [ebp - 4]
0x45FE0A: mov      eax, dword ptr [eax + 0x39c]
0x45FE10: call     0x45320c
0x45FE15: mov      eax, dword ptr [ebp - 4]
0x45FE18: mov      eax, dword ptr [eax + 0x3a0]
0x45FE1E: call     0x45320c
0x45FE23: mov      eax, dword ptr [ebp - 4]
0x45FE26: mov      eax, dword ptr [eax + 0x3a4]
0x45FE2C: call     0x45320c
0x45FE31: mov      eax, dword ptr [ebp - 4]
0x45FE34: mov      eax, dword ptr [eax + 0x3a8]
0x45FE3A: call     0x45320c
0x45FE3F: mov      eax, dword ptr [ebp - 4]
0x45FE42: mov      eax, dword ptr [eax + 0x3ac]
0x45FE48: call     0x45320c
0x45FE4D: mov      eax, dword ptr [ebp - 4]
0x45FE50: mov      eax, dword ptr [eax + 0x3b0]
0x45FE56: call     0x45320c
0x45FE5B: mov      eax, dword ptr [ebp - 4]
0x45FE5E: mov      eax, dword ptr [eax + 0x3b4]
0x45FE64: call     0x45320c
0x45FE69: mov      eax, dword ptr [ebp - 4]
0x45FE6C: mov      eax, dword ptr [eax + 0x3b8]
0x45FE72: call     0x45320c
0x45FE77: mov      eax, dword ptr [ebp - 4]
0x45FE7A: mov      eax, dword ptr [eax + 0x344]
0x45FE80: call     0x45320c
0x45FE85: mov      eax, dword ptr [ebp - 4]
0x45FE88: mov      eax, dword ptr [eax + 0x370]
0x45FE8E: call     0x45320c
0x45FE93: mov      eax, dword ptr [ebp - 4]
0x45FE96: mov      eax, dword ptr [eax + 0x308]
0x45FE9C: call     0x45320c
0x45FEA1: mov      eax, dword ptr [ebp - 4]
0x45FEA4: mov      eax, dword ptr [eax + 0x3c0]
0x45FEAA: call     0x45320c
0x45FEAF: mov      eax, dword ptr [ebp - 4]
0x45FEB2: mov      eax, dword ptr [eax + 0x34c]
0x45FEB8: call     0x45320c
0x45FEBD: mov      eax, dword ptr [ebp - 4]
0x45FEC0: mov      eax, dword ptr [eax + 0x35c]
0x45FEC6: call     0x45320c
0x45FECB: mov      eax, dword ptr [ebp - 4]
0x45FECE: mov      eax, dword ptr [eax + 0x324]
0x45FED4: call     0x45320c
0x45FED9: mov      eax, dword ptr [ebp - 4]
0x45FEDC: mov      eax, dword ptr [eax + 0x32c]
0x45FEE2: call     0x45320c
0x45FEE7: mov      eax, dword ptr [ebp - 4]
0x45FEEA: mov      eax, dword ptr [eax + 0x330]
0x45FEF0: call     0x45320c
0x45FEF5: mov      eax, dword ptr [0x4f7d8c]
0x45FEFA: call     0x4799a0
0x45FEFF: mov      eax, dword ptr [0x4f7d9c]
0x45FF04: mov      bx, 0xfffe
0x45FF08: call     0x403080
0x45FF0D: mov      eax, dword ptr [0x4f7da0]
0x45FF12: mov      bx, 0xfffe
0x45FF16: call     0x403080
0x45FF1B: mov      eax, dword ptr [0x4f7d8c]
0x45FF20: call     0x402eac
0x45FF25: mov      eax, dword ptr [0x4f7d90]
0x45FF2A: call     0x402eac
0x45FF2F: mov      eax, dword ptr [0x4f7d94]
0x45FF34: call     0x402eac
0x45FF39: mov      eax, dword ptr [0x4f7d98]
0x45FF3E: call     0x402eac
0x45FF43: mov      eax, dword ptr [0x4f7d9c]
0x45FF48: call     0x402eac
0x45FF4D: mov      eax, dword ptr [0x4f7da0]
0x45FF52: call     0x402eac
0x45FF57: mov      eax, dword ptr [0x4f9178]
0x45FF5C: call     0x402eac
0x45FF61: mov      eax, dword ptr [0x4f917c]
0x45FF66: call     0x402eac
0x45FF6B: mov      eax, 0x45ffd4 ; "----------------------- closed -------------------------"
0x45FF70: call     0x45ee2c
0x45FF75: mov      eax, dword ptr [0x4f7ef4]
0x45FF7A: call     0x402eac
0x45FF7F: mov      eax, dword ptr [0x4f7da4]
0x45FF84: call     0x402eac
0x45FF89: mov      eax, dword ptr [0x4f9190]
0x45FF8E: call     0x402eac
0x45FF93: mov      eax, dword ptr [0x4f9230]
0x45FF98: call     0x402eac
0x45FF9D: mov      eax, dword ptr [0x4f7dac]
0x45FFA2: call     0x402eac
0x45FFA7: mov      eax, dword ptr [0x4f7da8]
0x45FFAC: call     0x402eac
0x45FFB1: mov      eax, dword ptr [0x4f7dc8]
0x45FFB6: call     0x402eac
0x45FFBB: mov      eax, dword ptr [0x4f7dcc]
0x45FFC0: call     0x402eac
0x45FFC5: pop      ebx
0x45FFC6: pop      ecx
0x45FFC7: pop      ecx
0x45FFC8: pop      ebp
0x45FFC9: ret      
0x45FFCA: add      byte ptr [eax], al
0x45FFD4: [STR len=56] "----------------------- closed -------------------------"
0x460010: push     ebp
0x460011: mov      ebp, esp
0x460013: add      esp, -8
0x460016: push     ebx
0x460017: push     esi
0x460018: push     edi
0x460019: mov      dword ptr [ebp - 8], edx
0x46001C: mov      dword ptr [ebp - 4], eax
0x46001F: cmp      byte ptr [0x4f937e], 0
0x460026: je       0x4609bb
0x46002C: mov      byte ptr [0x4f937e], 0
0x460033: mov      edx, 0x320
0x460038: mov      eax, dword ptr [ebp - 4]
0x46003B: mov      eax, dword ptr [eax + 0x2f4]
0x460041: call     0x451cbc
0x460046: mov      edx, 0x258
0x46004B: mov      eax, dword ptr [ebp - 4]
0x46004E: mov      eax, dword ptr [eax + 0x2f4]
0x460054: call     0x451c6c
0x460059: mov      eax, dword ptr [0x4f7d88]
0x46005E: mov      eax, dword ptr [eax + 0x58]
0x460061: mov      edx, dword ptr [0x4a2d0c] ; *0x4A2D0C->0x45E3E4
0x460067: call     0x417618
0x46006C: mov      eax, dword ptr [0x4f7d88]
0x460071: call     0x43e1f0
0x460076: mov      eax, dword ptr [eax + 0xc]
0x460079: mov      edx, dword ptr [0x4a2d0c] ; *0x4A2D0C->0x45E3E4
0x46007F: call     0x417618
0x460084: mov      eax, dword ptr [ebp - 4]
0x460087: mov      eax, dword ptr [eax + 0x2f4]
0x46008D: mov      eax, dword ptr [eax + 0x294]
0x460093: call     0x44d8b4
0x460098: mov      eax, dword ptr [eax + 0xc]
0x46009B: mov      edx, dword ptr [0x4a2d0c] ; *0x4A2D0C->0x45E3E4
0x4600A1: call     0x417618
0x4600A6: mov      eax, dword ptr [0x4f7d9c]
0x4600AB: mov      eax, dword ptr [eax + 0x5a850]
0x4600B1: mov      eax, dword ptr [eax + 0x58]
0x4600B4: mov      edx, dword ptr [0x4a2d0c] ; *0x4A2D0C->0x45E3E4
0x4600BA: call     0x417618
0x4600BF: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x4600C4: mov      eax, dword ptr [eax]
0x4600C6: mov      eax, dword ptr [eax + 0x58]
0x4600C9: mov      edx, dword ptr [0x4a2d0c] ; *0x4A2D0C->0x45E3E4
0x4600CF: call     0x417618
0x4600D4: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x4600D9: mov      eax, dword ptr [eax]
0x4600DB: call     0x43e1f0
0x4600E0: mov      eax, dword ptr [eax + 0xc]
0x4600E3: mov      edx, dword ptr [0x4a2d0c] ; *0x4A2D0C->0x45E3E4
0x4600E9: call     0x417618
0x4600EE: mov      eax, dword ptr [0x4f7d88]
0x4600F3: mov      eax, dword ptr [eax + 0x58]
0x4600F6: mov      edx, 9
0x4600FB: call     0x417690
0x460100: mov      eax, dword ptr [0x4f7d88]
0x460105: call     0x43e1f0
0x46010A: mov      eax, dword ptr [eax + 0xc]
0x46010D: mov      edx, 9
0x460112: call     0x417690
0x460117: mov      eax, dword ptr [ebp - 4]
0x46011A: mov      eax, dword ptr [eax + 0x2f4]
0x460120: mov      eax, dword ptr [eax + 0x294]
0x460126: call     0x44d8b4
0x46012B: mov      eax, dword ptr [eax + 0xc]
0x46012E: mov      edx, 9
0x460133: call     0x417690
0x460138: mov      eax, dword ptr [0x4f7d9c]
0x46013D: mov      eax, dword ptr [eax + 0x5a850]
0x460143: mov      eax, dword ptr [eax + 0x58]
0x460146: mov      edx, 9
0x46014B: call     0x417690
0x460150: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x460155: mov      eax, dword ptr [eax]
0x460157: mov      eax, dword ptr [eax + 0x58]
0x46015A: mov      edx, 9
0x46015F: call     0x417690
0x460164: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x460169: mov      eax, dword ptr [eax]
0x46016B: call     0x43e1f0
0x460170: mov      eax, dword ptr [eax + 0xc]
0x460173: mov      edx, 9
0x460178: call     0x417690
0x46017D: mov      eax, dword ptr [ebp - 4]
0x460180: mov      eax, dword ptr [eax + 0x2f4]
0x460186: mov      eax, dword ptr [eax + 0x268]
0x46018C: mov      edx, dword ptr [ebp - 4]
0x46018F: mov      edx, dword ptr [edx + 0x2f8]
0x460195: mov      dword ptr [edx + 0x34], eax
0x460198: mov      eax, dword ptr [ebp - 4]
0x46019B: mov      eax, dword ptr [eax + 0x2f4]
0x4601A1: mov      eax, dword ptr [eax + 0x268]
0x4601A7: mov      edx, dword ptr [ebp - 4]
0x4601AA: mov      edx, dword ptr [edx + 0x2fc]
0x4601B0: mov      dword ptr [edx + 0x34], eax
0x4601B3: mov      eax, dword ptr [ebp - 4]
0x4601B6: mov      eax, dword ptr [eax + 0x2f4]
0x4601BC: mov      eax, dword ptr [eax + 0x268]
0x4601C2: mov      edx, dword ptr [ebp - 4]
0x4601C5: mov      edx, dword ptr [edx + 0x374]
0x4601CB: mov      dword ptr [edx + 0x34], eax
0x4601CE: mov      eax, dword ptr [ebp - 4]
0x4601D1: mov      eax, dword ptr [eax + 0x2f4]
0x4601D7: mov      eax, dword ptr [eax + 0x268]
0x4601DD: mov      edx, dword ptr [ebp - 4]
0x4601E0: mov      edx, dword ptr [edx + 0x378]
0x4601E6: mov      dword ptr [edx + 0x34], eax
0x4601E9: mov      eax, dword ptr [ebp - 4]
0x4601EC: mov      eax, dword ptr [eax + 0x2f4]
0x4601F2: mov      eax, dword ptr [eax + 0x268]
0x4601F8: mov      edx, dword ptr [ebp - 4]
0x4601FB: mov      edx, dword ptr [edx + 0x37c]
0x460201: mov      dword ptr [edx + 0x34], eax
0x460204: mov      eax, dword ptr [ebp - 4]
0x460207: mov      eax, dword ptr [eax + 0x2f4]
0x46020D: mov      eax, dword ptr [eax + 0x268]
0x460213: mov      edx, dword ptr [ebp - 4]
0x460216: mov      edx, dword ptr [edx + 0x380]
0x46021C: mov      dword ptr [edx + 0x34], eax
0x46021F: mov      eax, dword ptr [ebp - 4]
0x460222: mov      eax, dword ptr [eax + 0x2f4]
0x460228: mov      eax, dword ptr [eax + 0x268]
0x46022E: mov      edx, dword ptr [ebp - 4]
0x460231: mov      edx, dword ptr [edx + 0x384]
0x460237: mov      dword ptr [edx + 0x34], eax
0x46023A: mov      eax, dword ptr [ebp - 4]
0x46023D: mov      eax, dword ptr [eax + 0x2f4]
0x460243: mov      eax, dword ptr [eax + 0x268]
0x460249: mov      edx, dword ptr [ebp - 4]
0x46024C: mov      edx, dword ptr [edx + 0x388]
0x460252: mov      dword ptr [edx + 0x34], eax
0x460255: mov      eax, dword ptr [ebp - 4]
0x460258: mov      eax, dword ptr [eax + 0x2f4]
0x46025E: mov      eax, dword ptr [eax + 0x268]
0x460264: mov      edx, dword ptr [ebp - 4]
0x460267: mov      edx, dword ptr [edx + 0x300]
0x46026D: mov      dword ptr [edx + 0x34], eax
0x460270: mov      eax, dword ptr [ebp - 4]
0x460273: mov      eax, dword ptr [eax + 0x2f4]
0x460279: mov      eax, dword ptr [eax + 0x268]
0x46027F: mov      edx, dword ptr [ebp - 4]
0x460282: mov      edx, dword ptr [edx + 0x308]
0x460288: mov      dword ptr [edx + 0x34], eax
0x46028B: mov      eax, dword ptr [ebp - 4]
0x46028E: mov      eax, dword ptr [eax + 0x2f4]
0x460294: mov      eax, dword ptr [eax + 0x268]
0x46029A: mov      edx, dword ptr [ebp - 4]
0x46029D: mov      edx, dword ptr [edx + 0x3c0]
0x4602A3: mov      dword ptr [edx + 0x34], eax
0x4602A6: mov      eax, dword ptr [ebp - 4]
0x4602A9: mov      eax, dword ptr [eax + 0x2f4]
0x4602AF: mov      eax, dword ptr [eax + 0x268]
0x4602B5: mov      edx, dword ptr [ebp - 4]
0x4602B8: mov      edx, dword ptr [edx + 0x34c]
0x4602BE: mov      dword ptr [edx + 0x34], eax
0x4602C1: mov      eax, dword ptr [ebp - 4]
0x4602C4: mov      eax, dword ptr [eax + 0x2f4]
0x4602CA: mov      eax, dword ptr [eax + 0x268]
0x4602D0: mov      edx, dword ptr [ebp - 4]
0x4602D3: mov      edx, dword ptr [edx + 0x35c]
0x4602D9: mov      dword ptr [edx + 0x34], eax
0x4602DC: mov      eax, dword ptr [ebp - 4]
0x4602DF: mov      eax, dword ptr [eax + 0x2f4]
0x4602E5: mov      eax, dword ptr [eax + 0x268]
0x4602EB: mov      edx, dword ptr [ebp - 4]
0x4602EE: mov      edx, dword ptr [edx + 0x324]
0x4602F4: mov      dword ptr [edx + 0x34], eax
0x4602F7: mov      eax, dword ptr [ebp - 4]
0x4602FA: mov      eax, dword ptr [eax + 0x2f4]
0x460300: mov      eax, dword ptr [eax + 0x268]
0x460306: mov      edx, dword ptr [ebp - 4]
0x460309: mov      edx, dword ptr [edx + 0x32c]
0x46030F: mov      dword ptr [edx + 0x34], eax
0x460312: mov      eax, dword ptr [ebp - 4]
0x460315: mov      eax, dword ptr [eax + 0x2f4]
0x46031B: mov      eax, dword ptr [eax + 0x268]
0x460321: mov      edx, dword ptr [ebp - 4]
0x460324: mov      edx, dword ptr [edx + 0x330]
0x46032A: mov      dword ptr [edx + 0x34], eax
0x46032D: mov      eax, dword ptr [ebp - 4]
0x460330: mov      eax, dword ptr [eax + 0x2f4]
0x460336: mov      eax, dword ptr [eax + 0x268]
0x46033C: mov      edx, dword ptr [ebp - 4]
0x46033F: mov      edx, dword ptr [edx + 0x304]
0x460345: mov      dword ptr [edx + 0x34], eax
0x460348: mov      eax, dword ptr [ebp - 4]
0x46034B: mov      eax, dword ptr [eax + 0x2f4]
0x460351: mov      eax, dword ptr [eax + 0x268]
0x460357: mov      edx, dword ptr [ebp - 4]
0x46035A: mov      edx, dword ptr [edx + 0x320]
0x460360: mov      dword ptr [edx + 0x34], eax
0x460363: mov      eax, dword ptr [ebp - 4]
0x460366: mov      eax, dword ptr [eax + 0x2f4]
0x46036C: mov      eax, dword ptr [eax + 0x268]
0x460372: mov      edx, dword ptr [ebp - 4]
0x460375: mov      edx, dword ptr [edx + 0x328]
0x46037B: mov      dword ptr [edx + 0x34], eax
0x46037E: mov      eax, dword ptr [ebp - 4]
0x460381: mov      eax, dword ptr [eax + 0x2f4]
0x460387: mov      eax, dword ptr [eax + 0x268]
0x46038D: mov      edx, dword ptr [ebp - 4]
0x460390: mov      edx, dword ptr [edx + 0x33c]
0x460396: mov      dword ptr [edx + 0x34], eax
0x460399: mov      eax, dword ptr [ebp - 4]
0x46039C: mov      eax, dword ptr [eax + 0x2f4]
0x4603A2: mov      eax, dword ptr [eax + 0x268]
0x4603A8: mov      edx, dword ptr [ebp - 4]
0x4603AB: mov      edx, dword ptr [edx + 0x3bc]
0x4603B1: mov      dword ptr [edx + 0x34], eax
0x4603B4: mov      eax, dword ptr [ebp - 4]
0x4603B7: mov      eax, dword ptr [eax + 0x2f4]
0x4603BD: mov      eax, dword ptr [eax + 0x268]
0x4603C3: mov      edx, dword ptr [ebp - 4]
0x4603C6: mov      edx, dword ptr [edx + 0x348]
0x4603CC: mov      dword ptr [edx + 0x34], eax
0x4603CF: mov      eax, dword ptr [ebp - 4]
0x4603D2: mov      eax, dword ptr [eax + 0x2f4]
0x4603D8: mov      eax, dword ptr [eax + 0x268]
0x4603DE: mov      edx, dword ptr [ebp - 4]
0x4603E1: mov      edx, dword ptr [edx + 0x318]
0x4603E7: mov      dword ptr [edx + 0x34], eax
0x4603EA: mov      eax, dword ptr [ebp - 4]
0x4603ED: mov      eax, dword ptr [eax + 0x2f4]
0x4603F3: mov      eax, dword ptr [eax + 0x268]
0x4603F9: mov      edx, dword ptr [ebp - 4]
0x4603FC: mov      edx, dword ptr [edx + 0x354]
0x460402: mov      dword ptr [edx + 0x34], eax
0x460405: mov      eax, dword ptr [ebp - 4]
0x460408: mov      eax, dword ptr [eax + 0x2f4]
0x46040E: mov      eax, dword ptr [eax + 0x268]
0x460414: mov      edx, dword ptr [ebp - 4]
0x460417: mov      edx, dword ptr [edx + 0x358]
0x46041D: mov      dword ptr [edx + 0x34], eax
0x460420: mov      eax, dword ptr [ebp - 4]
0x460423: mov      eax, dword ptr [eax + 0x2f4]
0x460429: mov      eax, dword ptr [eax + 0x268]
0x46042F: mov      edx, dword ptr [ebp - 4]
0x460432: mov      edx, dword ptr [edx + 0x360]
0x460438: mov      dword ptr [edx + 0x34], eax
0x46043B: mov      eax, dword ptr [ebp - 4]
0x46043E: mov      eax, dword ptr [eax + 0x2f4]
0x460444: mov      eax, dword ptr [eax + 0x268]
0x46044A: mov      edx, dword ptr [ebp - 4]
0x46044D: mov      edx, dword ptr [edx + 0x368]
0x460453: mov      dword ptr [edx + 0x34], eax
0x460456: mov      eax, dword ptr [ebp - 4]
0x460459: mov      eax, dword ptr [eax + 0x2f4]
0x46045F: mov      eax, dword ptr [eax + 0x268]
0x460465: mov      edx, dword ptr [ebp - 4]
0x460468: mov      edx, dword ptr [edx + 0x36c]
0x46046E: mov      dword ptr [edx + 0x34], eax
0x460471: mov      eax, dword ptr [ebp - 4]
0x460474: mov      eax, dword ptr [eax + 0x2f4]
0x46047A: mov      eax, dword ptr [eax + 0x268]
0x460480: mov      edx, dword ptr [ebp - 4]
0x460483: mov      edx, dword ptr [edx + 0x38c]
0x460489: mov      dword ptr [edx + 0x34], eax
0x46048C: mov      eax, dword ptr [ebp - 4]
0x46048F: mov      eax, dword ptr [eax + 0x2f4]
0x460495: mov      eax, dword ptr [eax + 0x268]
0x46049B: mov      edx, dword ptr [ebp - 4]
0x46049E: mov      edx, dword ptr [edx + 0x390]
0x4604A4: mov      dword ptr [edx + 0x34], eax
0x4604A7: mov      eax, dword ptr [ebp - 4]
0x4604AA: mov      eax, dword ptr [eax + 0x2f4]
0x4604B0: mov      eax, dword ptr [eax + 0x268]
0x4604B6: mov      edx, dword ptr [ebp - 4]
0x4604B9: mov      edx, dword ptr [edx + 0x394]
0x4604BF: mov      dword ptr [edx + 0x34], eax
0x4604C2: mov      eax, dword ptr [ebp - 4]
0x4604C5: mov      eax, dword ptr [eax + 0x2f4]
0x4604CB: mov      eax, dword ptr [eax + 0x268]
0x4604D1: mov      edx, dword ptr [ebp - 4]
0x4604D4: mov      edx, dword ptr [edx + 0x398]
0x4604DA: mov      dword ptr [edx + 0x34], eax
0x4604DD: mov      eax, dword ptr [ebp - 4]
0x4604E0: mov      eax, dword ptr [eax + 0x2f4]
0x4604E6: mov      eax, dword ptr [eax + 0x268]
0x4604EC: mov      edx, dword ptr [ebp - 4]
0x4604EF: mov      edx, dword ptr [edx + 0x39c]
0x4604F5: mov      dword ptr [edx + 0x34], eax
0x4604F8: mov      eax, dword ptr [ebp - 4]
0x4604FB: mov      eax, dword ptr [eax + 0x2f4]
0x460501: mov      eax, dword ptr [eax + 0x268]
0x460507: mov      edx, dword ptr [ebp - 4]
0x46050A: mov      edx, dword ptr [edx + 0x3a0]
0x460510: mov      dword ptr [edx + 0x34], eax
0x460513: mov      eax, dword ptr [ebp - 4]
0x460516: mov      eax, dword ptr [eax + 0x2f4]
0x46051C: mov      eax, dword ptr [eax + 0x268]
0x460522: mov      edx, dword ptr [ebp - 4]
0x460525: mov      edx, dword ptr [edx + 0x3a4]
0x46052B: mov      dword ptr [edx + 0x34], eax
0x46052E: mov      eax, dword ptr [ebp - 4]
0x460531: mov      eax, dword ptr [eax + 0x2f4]
0x460537: mov      eax, dword ptr [eax + 0x268]
0x46053D: mov      edx, dword ptr [ebp - 4]
0x460540: mov      edx, dword ptr [edx + 0x3a8]
0x460546: mov      dword ptr [edx + 0x34], eax
0x460549: mov      eax, dword ptr [ebp - 4]
0x46054C: mov      eax, dword ptr [eax + 0x2f4]
0x460552: mov      eax, dword ptr [eax + 0x268]
0x460558: mov      edx, dword ptr [ebp - 4]
0x46055B: mov      edx, dword ptr [edx + 0x3ac]
0x460561: mov      dword ptr [edx + 0x34], eax
0x460564: mov      eax, dword ptr [ebp - 4]
0x460567: mov      eax, dword ptr [eax + 0x2f4]
0x46056D: mov      eax, dword ptr [eax + 0x268]
0x460573: mov      edx, dword ptr [ebp - 4]
0x460576: mov      edx, dword ptr [edx + 0x3b0]
0x46057C: mov      dword ptr [edx + 0x34], eax
0x46057F: mov      eax, dword ptr [ebp - 4]
0x460582: mov      eax, dword ptr [eax + 0x2f4]
0x460588: mov      eax, dword ptr [eax + 0x268]
0x46058E: mov      edx, dword ptr [ebp - 4]
0x460591: mov      edx, dword ptr [edx + 0x3b4]
0x460597: mov      dword ptr [edx + 0x34], eax
0x46059A: mov      eax, dword ptr [ebp - 4]
0x46059D: mov      eax, dword ptr [eax + 0x2f4]
0x4605A3: mov      eax, dword ptr [eax + 0x268]
0x4605A9: mov      edx, dword ptr [ebp - 4]
0x4605AC: mov      edx, dword ptr [edx + 0x3b8]
0x4605B2: mov      dword ptr [edx + 0x34], eax
0x4605B5: mov      eax, dword ptr [ebp - 4]
0x4605B8: mov      eax, dword ptr [eax + 0x2f4]
0x4605BE: mov      eax, dword ptr [eax + 0x268]
0x4605C4: mov      edx, dword ptr [ebp - 4]
0x4605C7: mov      edx, dword ptr [edx + 0x344]
0x4605CD: mov      dword ptr [edx + 0x34], eax
0x4605D0: mov      eax, dword ptr [ebp - 4]
0x4605D3: mov      eax, dword ptr [eax + 0x2f4]
0x4605D9: mov      eax, dword ptr [eax + 0x268]
0x4605DF: mov      edx, dword ptr [ebp - 4]
0x4605E2: mov      edx, dword ptr [edx + 0x370]
0x4605E8: mov      dword ptr [edx + 0x34], eax
0x4605EB: mov      eax, dword ptr [ebp - 4]
0x4605EE: mov      eax, dword ptr [eax + 0x2f8]
0x4605F4: call     0x452f48
0x4605F9: mov      eax, dword ptr [ebp - 4]
0x4605FC: mov      eax, dword ptr [eax + 0x2fc]
0x460602: call     0x452f48
0x460607: mov      eax, dword ptr [ebp - 4]
0x46060A: mov      eax, dword ptr [eax + 0x374]
0x460610: call     0x452f48
0x460615: mov      eax, dword ptr [ebp - 4]
0x460618: mov      eax, dword ptr [eax + 0x378]
0x46061E: call     0x452f48
0x460623: mov      eax, dword ptr [ebp - 4]
0x460626: mov      eax, dword ptr [eax + 0x37c]
0x46062C: call     0x452f48
0x460631: mov      eax, dword ptr [ebp - 4]
0x460634: mov      eax, dword ptr [eax + 0x380]
0x46063A: call     0x452f48
0x46063F: mov      eax, dword ptr [ebp - 4]
0x460642: mov      eax, dword ptr [eax + 0x384]
0x460648: call     0x452f48
0x46064D: mov      eax, dword ptr [ebp - 4]
0x460650: mov      eax, dword ptr [eax + 0x388]
0x460656: call     0x452f48
0x46065B: mov      eax, dword ptr [ebp - 4]
0x46065E: mov      eax, dword ptr [eax + 0x300]
0x460664: call     0x452f48
0x460669: mov      eax, dword ptr [ebp - 4]
0x46066C: mov      eax, dword ptr [eax + 0x308]
0x460672: call     0x452f48
0x460677: mov      eax, dword ptr [ebp - 4]
0x46067A: mov      eax, dword ptr [eax + 0x3c0]
0x460680: call     0x452f48
0x460685: mov      eax, dword ptr [ebp - 4]
0x460688: mov      eax, dword ptr [eax + 0x34c]
0x46068E: call     0x452f48
0x460693: mov      eax, dword ptr [ebp - 4]
0x460696: mov      eax, dword ptr [eax + 0x35c]
0x46069C: call     0x452f48
0x4606A1: mov      eax, dword ptr [ebp - 4]
0x4606A4: mov      eax, dword ptr [eax + 0x324]
0x4606AA: call     0x452f48
0x4606AF: mov      eax, dword ptr [ebp - 4]
0x4606B2: mov      eax, dword ptr [eax + 0x32c]
0x4606B8: call     0x452f48
0x4606BD: mov      eax, dword ptr [ebp - 4]
0x4606C0: mov      eax, dword ptr [eax + 0x330]
0x4606C6: call     0x452f48
0x4606CB: mov      eax, dword ptr [ebp - 4]
0x4606CE: mov      eax, dword ptr [eax + 0x304]
0x4606D4: call     0x452f48
0x4606D9: mov      eax, dword ptr [ebp - 4]
0x4606DC: mov      eax, dword ptr [eax + 0x320]
0x4606E2: call     0x452f48
0x4606E7: mov      eax, dword ptr [ebp - 4]
0x4606EA: mov      eax, dword ptr [eax + 0x328]
0x4606F0: call     0x452f48
0x4606F5: mov      eax, dword ptr [ebp - 4]
0x4606F8: mov      eax, dword ptr [eax + 0x33c]
0x4606FE: call     0x452f48
0x460703: mov      eax, dword ptr [ebp - 4]
0x460706: mov      eax, dword ptr [eax + 0x3bc]
0x46070C: call     0x452f48
0x460711: mov      eax, dword ptr [ebp - 4]
0x460714: mov      eax, dword ptr [eax + 0x348]
0x46071A: call     0x452f48
0x46071F: mov      eax, dword ptr [ebp - 4]
0x460722: mov      eax, dword ptr [eax + 0x318]
0x460728: call     0x452f48
0x46072D: mov      eax, dword ptr [ebp - 4]
0x460730: mov      eax, dword ptr [eax + 0x354]
0x460736: call     0x452f48
0x46073B: mov      eax, dword ptr [ebp - 4]
0x46073E: mov      eax, dword ptr [eax + 0x358]
0x460744: call     0x452f48
0x460749: mov      eax, dword ptr [ebp - 4]
0x46074C: mov      eax, dword ptr [eax + 0x360]
0x460752: call     0x452f48
0x460757: mov      eax, dword ptr [ebp - 4]
0x46075A: mov      eax, dword ptr [eax + 0x368]
0x460760: call     0x452f48
0x460765: mov      eax, dword ptr [ebp - 4]
0x460768: mov      eax, dword ptr [eax + 0x36c]
0x46076E: call     0x452f48
0x460773: mov      eax, dword ptr [ebp - 4]
0x460776: mov      eax, dword ptr [eax + 0x38c]
0x46077C: call     0x452f48
0x460781: mov      eax, dword ptr [ebp - 4]
0x460784: mov      eax, dword ptr [eax + 0x390]
0x46078A: call     0x452f48
0x46078F: mov      eax, dword ptr [ebp - 4]
0x460792: mov      eax, dword ptr [eax + 0x394]
0x460798: call     0x452f48
0x46079D: mov      eax, dword ptr [ebp - 4]
0x4607A0: mov      eax, dword ptr [eax + 0x398]
0x4607A6: call     0x452f48
0x4607AB: mov      eax, dword ptr [ebp - 4]
0x4607AE: mov      eax, dword ptr [eax + 0x39c]
0x4607B4: call     0x452f48
0x4607B9: mov      eax, dword ptr [ebp - 4]
0x4607BC: mov      eax, dword ptr [eax + 0x3a0]
0x4607C2: call     0x452f48
0x4607C7: mov      eax, dword ptr [ebp - 4]
0x4607CA: mov      eax, dword ptr [eax + 0x3a4]
0x4607D0: call     0x452f48
0x4607D5: mov      eax, dword ptr [ebp - 4]
0x4607D8: mov      eax, dword ptr [eax + 0x3a8]
0x4607DE: call     0x452f48
0x4607E3: mov      eax, dword ptr [ebp - 4]
0x4607E6: mov      eax, dword ptr [eax + 0x3ac]
0x4607EC: call     0x452f48
0x4607F1: mov      eax, dword ptr [ebp - 4]
0x4607F4: mov      eax, dword ptr [eax + 0x3b0]
0x4607FA: call     0x452f48
0x4607FF: mov      eax, dword ptr [ebp - 4]
0x460802: mov      eax, dword ptr [eax + 0x3b4]
0x460808: call     0x452f48
0x46080D: mov      eax, dword ptr [ebp - 4]
0x460810: mov      eax, dword ptr [eax + 0x3b8]
0x460816: call     0x452f48
0x46081B: mov      eax, dword ptr [ebp - 4]
0x46081E: mov      eax, dword ptr [eax + 0x344]
0x460824: call     0x452f48
0x460829: mov      eax, dword ptr [ebp - 4]
0x46082C: mov      eax, dword ptr [eax + 0x370]
0x460832: call     0x452f48
0x460837: mov      eax, dword ptr [ebp - 4]
0x46083A: mov      eax, dword ptr [eax + 0x308]
0x460840: mov      edx, dword ptr [ebp - 4]
0x460843: mov      edx, dword ptr [edx + 0x2f4]
0x460849: lea      esi, [eax + 0x54]
0x46084C: lea      edi, [edx + 0x90c]
0x460852: mov      ecx, 0x100
0x460857: rep movsd dword ptr es:[edi], dword ptr [esi]
0x460859: mov      eax, dword ptr [ebp - 4]
0x46085C: mov      eax, dword ptr [eax + 0x2f4]
0x460862: mov      edx, dword ptr [ebp - 4]
0x460865: mov      edx, dword ptr [edx + 0x2f4]
0x46086B: lea      esi, [eax + 0x90c]
0x460871: lea      edi, [edx + 0x50c]
0x460877: mov      ecx, 0x100
0x46087C: rep movsd dword ptr es:[edi], dword ptr [esi]
0x46087E: mov      eax, dword ptr [ebp - 4]
0x460881: mov      eax, dword ptr [eax + 0x2f4]
0x460887: call     0x451dd0
0x46088C: mov      eax, 0x4609cc ; "Data\npal.idx"
0x460891: call     0x4550d4
0x460896: test     al, al
0x460898: jne      0x4608b7
0x46089A: mov      eax, dword ptr [ebp - 4]
0x46089D: mov      eax, dword ptr [eax + 0x2f4]
0x4608A3: add      eax, 0x50c
0x4608A8: call     0x4546c4
0x4608AD: mov      eax, 0x4609cc ; "Data\npal.idx"
0x4608B2: call     0x454fb8
0x4608B7: mov      eax, dword ptr [ebp - 4]
0x4608BA: mov      eax, dword ptr [eax + 0x2f4]
0x4608C0: add      eax, 0x50c
0x4608C5: call     0x4546fc
0x4608CA: mov      eax, dword ptr [0x4f7d8c]
0x4608CF: call     0x479994
0x4608D4: mov      eax, dword ptr [0x4f7d9c]
0x4608D9: mov      bx, 0xffff
0x4608DD: call     0x403080
0x4608E2: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x4608E7: mov      eax, dword ptr [eax]
0x4608E9: call     0x481dd4
0x4608EE: mov      eax, dword ptr [ebp - 4]
0x4608F1: mov      eax, dword ptr [eax + 0x2f4]
0x4608F7: test     byte ptr [eax + 0x208], 1
0x4608FE: jne      0x46093d
0x460900: xor      edx, edx
0x460902: mov      eax, dword ptr [ebp - 4]
0x460905: call     0x42718c
0x46090A: xor      edx, edx
0x46090C: mov      eax, dword ptr [ebp - 4]
0x46090F: call     0x4271b0
0x460914: mov      edx, 0x320
0x460919: mov      eax, dword ptr [ebp - 4]
0x46091C: call     0x4271d8
0x460921: mov      edx, 0x258
0x460926: mov      eax, dword ptr [ebp - 4]
0x460929: call     0x4271fc
0x46092E: mov      byte ptr [0x4f94c1], 1
0x460935: mov      eax, dword ptr [0x4a3e4c] ; *0x4A3E4C->0x4A2CBC
0x46093A: mov      byte ptr [eax], 1
0x46093D: mov      eax, dword ptr [0x4f7d88]
0x460942: mov      eax, dword ptr [eax + 0x2f4]
0x460948: mov      ecx, dword ptr [eax + 0x268]
0x46094E: mov      dl, 1
0x460950: mov      eax, dword ptr [0x44b6a0]
0x460955: call     0x44d190
0x46095A: mov      dword ptr [0x4f94d4], eax
0x46095F: mov      eax, dword ptr [0x4f94d4]
0x460964: mov      byte ptr [eax + 0x18], 1
0x460968: mov      ecx, 0x15e
0x46096D: mov      edx, 0x12c
0x460972: mov      eax, dword ptr [0x4f94d4]
0x460977: call     0x44e480
0x46097C: mov      eax, dword ptr [0x4f7d88]
0x460981: mov      eax, dword ptr [eax + 0x2f4]
0x460987: mov      ecx, dword ptr [eax + 0x268]
0x46098D: mov      dl, 1
0x46098F: mov      eax, dword ptr [0x44b6a0]
0x460994: call     0x44d190
0x460999: mov      dword ptr [0x4f94d8], eax
0x46099E: mov      eax, dword ptr [0x4f94d8]
0x4609A3: mov      byte ptr [eax + 0x18], 1
0x4609A7: mov      ecx, 0x168
0x4609AC: mov      edx, 0x21c
0x4609B1: mov      eax, dword ptr [0x4f94d8]
0x4609B6: call     0x44e480
0x4609BB: pop      edi
0x4609BC: pop      esi
0x4609BD: pop      ebx
0x4609BE: pop      ecx
0x4609BF: pop      ecx
0x4609C0: pop      ebp
0x4609C1: ret      
0x4609C2: add      byte ptr [eax], al
0x4609CC: [STR len=13] "Data\npal.idx"
0x4609DC: push     ebp
0x4609DD: mov      ebp, esp
0x4609DF: add      esp, -8
0x4609E2: mov      dword ptr [ebp - 8], edx
0x4609E5: mov      dword ptr [ebp - 4], eax
0x4609E8: pop      ecx
0x4609E9: pop      ecx
0x4609EA: pop      ebp
0x4609EB: ret      
0x4609EC: push     ebp
0x4609ED: mov      ebp, esp
0x4609EF: add      esp, -0xc
0x4609F2: mov      dword ptr [ebp - 0xc], ecx
0x4609F5: mov      dword ptr [ebp - 8], edx
0x4609F8: mov      dword ptr [ebp - 4], eax
0x4609FB: mov      esp, ebp
0x4609FD: pop      ebp
0x4609FE: ret      
0x4609FF: nop      
0x460A00: push     ebp
0x460A01: mov      ebp, esp
0x460A03: add      esp, -0x10
0x460A06: mov      dword ptr [ebp - 0xc], ecx
0x460A09: mov      dword ptr [ebp - 8], edx
0x460A0C: mov      dword ptr [ebp - 4], eax
0x460A0F: mov      eax, dword ptr [ebp - 8]
0x460A12: cmp      eax, 6
0x460A15: ja       0x460ade
0x460A1B: jmp      dword ptr [eax*4 + 0x460a22]
0x460A22: or       al, byte ptr ds:[esi]
0x460A26: push     edi
0x460A27: or       al, byte ptr [esi]
0x460A2A: jo       0x460a36
0x460A2C: inc      esi
0x460A2D: add      byte ptr [esi - 0x63ffb9f6], al
0x460A33: or       al, byte ptr [esi]
0x460A36: mov      dl, 0xa
0x460A38: inc      esi
0x460A39: add      al, cl
0x460A3B: or       al, byte ptr [esi]
0x460A3E: mov      edx, dword ptr [ebp - 0xc]
0x460A41: mov      eax, dword ptr [ebp - 4]
0x460A44: mov      eax, dword ptr [eax + 0x2fc]
0x460A4A: call     0x45351c
0x460A4F: mov      dword ptr [ebp - 0x10], eax
0x460A52: jmp      0x460af2
0x460A57: mov      edx, dword ptr [ebp - 0xc]
0x460A5A: mov      eax, dword ptr [ebp - 4]
0x460A5D: mov      eax, dword ptr [eax + 0x374]
0x460A63: call     0x45351c
0x460A68: mov      dword ptr [ebp - 0x10], eax
0x460A6B: jmp      0x460af2
0x460A70: mov      edx, dword ptr [ebp - 0xc]
0x460A73: mov      eax, dword ptr [ebp - 4]
0x460A76: mov      eax, dword ptr [eax + 0x378]
0x460A7C: call     0x45351c
0x460A81: mov      dword ptr [ebp - 0x10], eax
0x460A84: jmp      0x460af2
0x460A86: mov      edx, dword ptr [ebp - 0xc]
0x460A89: mov      eax, dword ptr [ebp - 4]
0x460A8C: mov      eax, dword ptr [eax + 0x37c]
0x460A92: call     0x45351c
0x460A97: mov      dword ptr [ebp - 0x10], eax
0x460A9A: jmp      0x460af2
0x460A9C: mov      edx, dword ptr [ebp - 0xc]
0x460A9F: mov      eax, dword ptr [ebp - 4]
0x460AA2: mov      eax, dword ptr [eax + 0x380]
0x460AA8: call     0x45351c
0x460AAD: mov      dword ptr [ebp - 0x10], eax
0x460AB0: jmp      0x460af2
0x460AB2: mov      edx, dword ptr [ebp - 0xc]
0x460AB5: mov      eax, dword ptr [ebp - 4]
0x460AB8: mov      eax, dword ptr [eax + 0x384]
0x460ABE: call     0x45351c
0x460AC3: mov      dword ptr [ebp - 0x10], eax
0x460AC6: jmp      0x460af2
0x460AC8: mov      edx, dword ptr [ebp - 0xc]
0x460ACB: mov      eax, dword ptr [ebp - 4]
0x460ACE: mov      eax, dword ptr [eax + 0x388]
0x460AD4: call     0x45351c
0x460AD9: mov      dword ptr [ebp - 0x10], eax
0x460ADC: jmp      0x460af2
0x460ADE: mov      edx, dword ptr [ebp - 0xc]
0x460AE1: mov      eax, dword ptr [ebp - 4]
0x460AE4: mov      eax, dword ptr [eax + 0x2fc]
0x460AEA: call     0x45351c
0x460AEF: mov      dword ptr [ebp - 0x10], eax
0x460AF2: mov      eax, dword ptr [ebp - 0x10]
0x460AF5: mov      esp, ebp
0x460AF7: pop      ebp
0x460AF8: ret      
0x460AF9: lea      eax, [eax]
0x460AFC: push     ebp
0x460AFD: mov      ebp, esp
0x460AFF: add      esp, -0x10
0x460B02: mov      dword ptr [ebp - 0xc], ecx
0x460B05: mov      dword ptr [ebp - 8], edx
0x460B08: mov      dword ptr [ebp - 4], eax
0x460B0B: mov      eax, dword ptr [ebp - 8]
0x460B0E: cmp      eax, 6
0x460B11: ja       0x460c0e
0x460B17: jmp      dword ptr [eax*4 + 0x460b1e]
0x460B1E: cmp      cl, byte ptr [ebx]
0x460B20: inc      esi
0x460B21: add      byte ptr [edx + 0xb], bl
0x460B24: inc      esi
0x460B25: add      byte ptr [edx + 0xb], bh
0x460B28: inc      esi
0x460B29: add      byte ptr [edx - 0x48ffb9f5], bl
0x460B2F: or       eax, dword ptr [esi]
0x460B32: aam      0xb
0x460B34: inc      esi
0x460B35: add      cl, dh
0x460B37: or       eax, dword ptr [esi]
0x460B3A: mov      eax, dword ptr [ebp + 8]
0x460B3D: push     eax
0x460B3E: mov      ecx, dword ptr [ebp + 0xc]
0x460B41: mov      edx, dword ptr [ebp - 0xc]
0x460B44: mov      eax, dword ptr [ebp - 4]
0x460B47: mov      eax, dword ptr [eax + 0x2fc]
0x460B4D: call     0x453a48
0x460B52: mov      dword ptr [ebp - 0x10], eax
0x460B55: jmp      0x460c29
0x460B5A: mov      eax, dword ptr [ebp + 8]
0x460B5D: push     eax
0x460B5E: mov      ecx, dword ptr [ebp + 0xc]
0x460B61: mov      edx, dword ptr [ebp - 0xc]
0x460B64: mov      eax, dword ptr [ebp - 4]
0x460B67: mov      eax, dword ptr [eax + 0x374]
0x460B6D: call     0x453a48
0x460B72: mov      dword ptr [ebp - 0x10], eax
0x460B75: jmp      0x460c29
0x460B7A: mov      eax, dword ptr [ebp + 8]
0x460B7D: push     eax
0x460B7E: mov      ecx, dword ptr [ebp + 0xc]
0x460B81: mov      edx, dword ptr [ebp - 0xc]
0x460B84: mov      eax, dword ptr [ebp - 4]
0x460B87: mov      eax, dword ptr [eax + 0x378]
0x460B8D: call     0x453a48
0x460B92: mov      dword ptr [ebp - 0x10], eax
0x460B95: jmp      0x460c29
0x460B9A: mov      eax, dword ptr [ebp + 8]
0x460B9D: push     eax
0x460B9E: mov      ecx, dword ptr [ebp + 0xc]
0x460BA1: mov      edx, dword ptr [ebp - 0xc]
0x460BA4: mov      eax, dword ptr [ebp - 4]
0x460BA7: mov      eax, dword ptr [eax + 0x37c]
0x460BAD: call     0x453a48
0x460BB2: mov      dword ptr [ebp - 0x10], eax
0x460BB5: jmp      0x460c29
0x460BB7: mov      eax, dword ptr [ebp + 8]
0x460BBA: push     eax
0x460BBB: mov      ecx, dword ptr [ebp + 0xc]
0x460BBE: mov      edx, dword ptr [ebp - 0xc]
0x460BC1: mov      eax, dword ptr [ebp - 4]
0x460BC4: mov      eax, dword ptr [eax + 0x380]
0x460BCA: call     0x453a48
0x460BCF: mov      dword ptr [ebp - 0x10], eax
0x460BD2: jmp      0x460c29
0x460BD4: mov      eax, dword ptr [ebp + 8]
0x460BD7: push     eax
0x460BD8: mov      ecx, dword ptr [ebp + 0xc]
0x460BDB: mov      edx, dword ptr [ebp - 0xc]
0x460BDE: mov      eax, dword ptr [ebp - 4]
0x460BE1: mov      eax, dword ptr [eax + 0x384]
0x460BE7: call     0x453a48
0x460BEC: mov      dword ptr [ebp - 0x10], eax
0x460BEF: jmp      0x460c29
0x460BF1: mov      eax, dword ptr [ebp + 8]
0x460BF4: push     eax
0x460BF5: mov      ecx, dword ptr [ebp + 0xc]
0x460BF8: mov      edx, dword ptr [ebp - 0xc]
0x460BFB: mov      eax, dword ptr [ebp - 4]
0x460BFE: mov      eax, dword ptr [eax + 0x388]
0x460C04: call     0x453a48
0x460C09: mov      dword ptr [ebp - 0x10], eax
0x460C0C: jmp      0x460c29
0x460C0E: mov      eax, dword ptr [ebp + 8]
0x460C11: push     eax
0x460C12: mov      ecx, dword ptr [ebp + 0xc]
0x460C15: mov      edx, dword ptr [ebp - 0xc]
0x460C18: mov      eax, dword ptr [ebp - 4]
0x460C1B: mov      eax, dword ptr [eax + 0x2fc]
0x460C21: call     0x453a48
0x460C26: mov      dword ptr [ebp - 0x10], eax
0x460C29: mov      eax, dword ptr [ebp - 0x10]
0x460C2C: mov      esp, ebp
0x460C2E: pop      ebp
0x460C2F: ret      8
0x460C32: mov      eax, eax
0x460C34: push     ebp
0x460C35: mov      ebp, esp
0x460C37: add      esp, -8
0x460C3A: mov      dword ptr [ebp - 4], eax
0x460C3D: lea      ecx, [ebp - 5]
0x460C40: mov      edx, dword ptr [ebp - 4]
0x460C43: mov      eax, dword ptr [ebp - 4]
0x460C46: call     0x460c50
0x460C4B: pop      ecx
0x460C4C: pop      ecx
0x460C4D: pop      ebp
0x460C4E: ret      
0x460C4F: nop      
0x460C50: push     ebp
0x460C51: mov      ebp, esp
0x460C53: add      esp, 0xffffff30
0x460C59: push     ebx
0x460C5A: xor      ebx, ebx
0x460C5C: mov      dword ptr [ebp - 0xb8], ebx
0x460C62: mov      dword ptr [ebp - 0xbc], ebx
0x460C68: mov      dword ptr [ebp - 0xc0], ebx
0x460C6E: mov      dword ptr [ebp - 0xc4], ebx
0x460C74: mov      dword ptr [ebp - 0xc8], ebx
0x460C7A: mov      dword ptr [ebp - 0xcc], ebx
0x460C80: mov      dword ptr [ebp - 0xd0], ebx
0x460C86: mov      dword ptr [ebp - 8], ecx
0x460C89: mov      dword ptr [ebp - 0x18], edx
0x460C8C: mov      dword ptr [ebp - 4], eax
0x460C8F: xor      eax, eax
0x460C91: push     ebp
0x460C92: push     0x461116
0x460C97: push     dword ptr fs:[eax]
0x460C9A: mov      dword ptr fs:[eax], esp
0x460C9D: mov      eax, dword ptr [ebp - 8]
0x460CA0: mov      byte ptr [eax], 1
0x460CA3: mov      eax, dword ptr [ebp - 4]
0x460CA6: mov      eax, dword ptr [eax + 0x2f4]
0x460CAC: call     0x450e84
0x460CB1: test     al, al
0x460CB3: je       0x4610f8
0x460CB9: mov      eax, dword ptr [ebp - 4]
0x460CBC: call     0x461850
0x460CC1: mov      eax, dword ptr [ebp - 4]
0x460CC4: call     0x4618b0
0x460CC9: mov      eax, dword ptr [ebp - 4]
0x460CCC: mov      eax, dword ptr [eax + 0x2f4]
0x460CD2: mov      edx, dword ptr [eax + 0x294]
0x460CD8: mov      eax, dword ptr [0x4f7d8c]
0x460CDD: call     0x479fec
0x460CE2: mov      eax, dword ptr [ebp - 4]
0x460CE5: mov      eax, dword ptr [eax + 0x2f4]
0x460CEB: mov      edx, dword ptr [eax + 0x294]
0x460CF1: mov      eax, dword ptr [ebp - 4]
0x460CF4: mov      eax, dword ptr [eax + 0x31c]
0x460CFA: call     0x458bc8
0x460CFF: mov      eax, dword ptr [ebp - 4]
0x460D02: mov      eax, dword ptr [eax + 0x2f4]
0x460D08: mov      edx, dword ptr [eax + 0x294]
0x460D0E: mov      eax, dword ptr [0x4f7d8c]
0x460D13: call     0x47a5a0
0x460D18: mov      eax, dword ptr [ebp - 4]
0x460D1B: mov      eax, dword ptr [eax + 0x2f4]
0x460D21: mov      edx, dword ptr [eax + 0x294]
0x460D27: mov      eax, dword ptr [0x4f7d8c]
0x460D2C: call     0x47a6c4
0x460D31: cmp      byte ptr [0x4f939c], 0
0x460D38: je       0x460e08
0x460D3E: mov      eax, 0x4f93a4
0x460D43: mov      edx, 0x461124
0x460D48: xor      ecx, ecx
0x460D4A: mov      cl, byte ptr [eax]
0x460D4C: inc      ecx
0x460D4D: call     0x402ae4
0x460D52: je       0x460d70
0x460D54: movzx    edx, word ptr [0x4f93ba]
0x460D5B: mov      eax, dword ptr [0x4f7d88]
0x460D60: mov      eax, dword ptr [eax + 0x324]
0x460D66: call     0x45351c
0x460D6B: mov      dword ptr [ebp - 0x14], eax
0x460D6E: jmp      0x460d88
0x460D70: mov      eax, dword ptr [0x4f7d88]
0x460D75: mov      eax, dword ptr [eax + 0x324]
0x460D7B: mov      edx, 0x73
0x460D80: call     0x45351c
0x460D85: mov      dword ptr [ebp - 0x14], eax
0x460D88: cmp      dword ptr [ebp - 0x14], 0
0x460D8C: je       0x460e08
0x460D8E: lea      eax, [ebp - 0x10]
0x460D91: push     eax
0x460D92: call     0x4065fc
0x460D97: lea      edx, [ebp - 0x94]
0x460D9D: mov      eax, dword ptr [ebp - 0x14]
0x460DA0: call     0x44d8ec
0x460DA5: lea      eax, [ebp - 0x94]
0x460DAB: push     eax
0x460DAC: mov      eax, dword ptr [ebp - 0x14]
0x460DAF: push     eax
0x460DB0: push     1
0x460DB2: lea      edx, [ebp - 0xa4]
0x460DB8: mov      eax, dword ptr [ebp - 0x14]
0x460DBB: call     0x44d8ec
0x460DC0: mov      eax, dword ptr [ebp - 0x98]
0x460DC6: sar      eax, 1
0x460DC8: jns      0x460dcd
0x460DCA: adc      eax, 0
0x460DCD: mov      edx, dword ptr [ebp - 0xc]
0x460DD0: sub      edx, eax
0x460DD2: push     edx
0x460DD3: lea      edx, [ebp - 0xb4]
0x460DD9: mov      eax, dword ptr [ebp - 0x14]
0x460DDC: call     0x44d8ec
0x460DE1: mov      eax, dword ptr [ebp - 0xac]
0x460DE7: sar      eax, 1
0x460DE9: jns      0x460dee
0x460DEB: adc      eax, 0
0x460DEE: mov      edx, dword ptr [ebp - 0x10]
0x460DF1: sub      edx, eax
0x460DF3: mov      eax, dword ptr [ebp - 4]
0x460DF6: mov      eax, dword ptr [eax + 0x2f4]
0x460DFC: mov      eax, dword ptr [eax + 0x294]
0x460E02: pop      ecx
0x460E03: call     0x44d6cc
0x460E08: cmp      byte ptr [0x4f9468], 0
0x460E0F: je       0x460e5f
0x460E11: cmp      dword ptr [0x4f946c], 1
0x460E18: jge      0x460e24
0x460E1A: mov      dword ptr [0x4f946c], 1
0x460E24: mov      eax, dword ptr [ebp - 4]
0x460E27: mov      eax, dword ptr [eax + 0x2f4]
0x460E2D: mov      eax, dword ptr [eax + 0x294]
0x460E33: mov      edx, dword ptr [0x4f946c]
0x460E39: call     0x455420
0x460E3E: cmp      dword ptr [0x4f946c], 1
0x460E45: jg       0x460e53
0x460E47: mov      byte ptr [0x4f9468], 0
0x460E4E: jmp      0x460ef6
0x460E53: sub      dword ptr [0x4f946c], 2
0x460E5A: jmp      0x460ef6
0x460E5F: cmp      byte ptr [0x4f9469], 0
0x460E66: je       0x460eb0
0x460E68: cmp      dword ptr [0x4f946c], 0x1d
0x460E6F: jle      0x460e7b
0x460E71: mov      dword ptr [0x4f946c], 0x1d
0x460E7B: mov      eax, dword ptr [ebp - 4]
0x460E7E: mov      eax, dword ptr [eax + 0x2f4]
0x460E84: mov      eax, dword ptr [eax + 0x294]
0x460E8A: mov      edx, dword ptr [0x4f946c]
0x460E90: call     0x455420
0x460E95: cmp      dword ptr [0x4f946c], 0x1d
0x460E9C: jl       0x460ea7
0x460E9E: mov      byte ptr [0x4f9469], 0
0x460EA5: jmp      0x460ef6
0x460EA7: add      dword ptr [0x4f946c], 2
0x460EAE: jmp      0x460ef6
0x460EB0: cmp      byte ptr [0x4f9470], 0
0x460EB7: je       0x460ef6
0x460EB9: cmp      dword ptr [0x4f946c], 1
0x460EC0: jge      0x460ecc
0x460EC2: mov      dword ptr [0x4f946c], 1
0x460ECC: mov      eax, dword ptr [ebp - 4]
0x460ECF: mov      eax, dword ptr [eax + 0x2f4]
0x460ED5: mov      eax, dword ptr [eax + 0x294]
0x460EDB: mov      edx, dword ptr [0x4f946c]
0x460EE1: call     0x455420
0x460EE6: cmp      dword ptr [0x4f946c], 1
0x460EED: jle      0x460ef6
0x460EEF: sub      dword ptr [0x4f946c], 4
0x460EF6: lea      edx, [ebp - 0x94]
0x460EFC: mov      eax, dword ptr [ebp - 4]
0x460EFF: mov      eax, dword ptr [eax + 0x2f4]
0x460F05: mov      eax, dword ptr [eax + 0x294]
0x460F0B: call     0x44d8ec
0x460F10: lea      eax, [ebp - 0x94]
0x460F16: push     eax
0x460F17: mov      eax, dword ptr [ebp - 4]
0x460F1A: mov      eax, dword ptr [eax + 0x2f4]
0x460F20: mov      eax, dword ptr [eax + 0x294]
0x460F26: push     eax
0x460F27: push     0
0x460F29: mov      eax, dword ptr [ebp - 4]
0x460F2C: mov      eax, dword ptr [eax + 0x2f4]
0x460F32: mov      eax, dword ptr [eax + 0x28c]
0x460F38: xor      ecx, ecx
0x460F3A: xor      edx, edx
0x460F3C: call     0x44d6cc
0x460F41: cmp      dword ptr [0x4f7ef8], 0
0x460F48: je       0x4610f8
0x460F4E: mov      eax, dword ptr [0x4f7ef8]
0x460F53: cmp      byte ptr [eax + 0x20], 0
0x460F57: jne      0x46109f
0x460F5D: call     0x406154
0x460F62: sub      eax, dword ptr [0x4f924c]
0x460F68: cmp      eax, 0x3e8
0x460F6D: jbe      0x46109f
0x460F73: call     0x406154
0x460F78: mov      dword ptr [0x4f924c], eax
0x460F7D: mov      eax, dword ptr [0x4f9384]
0x460F82: cmp      eax, dword ptr [0x4f9590]
0x460F88: jne      0x460fbd
0x460F8A: mov      eax, dword ptr [0x4f9598]
0x460F8F: mov      eax, dword ptr [eax]
0x460F91: cmp      eax, dword ptr [0x4f9384]
0x460F97: jne      0x460fbd
0x460F99: mov      eax, dword ptr [0x4a4144] ; *0x4A4144->0x4A7294
0x460F9E: mov      eax, dword ptr [eax]
0x460FA0: cmp      eax, dword ptr [0x4f9594]
0x460FA6: jne      0x460fbd
0x460FA8: mov      eax, dword ptr [0x4a4144] ; *0x4A4144->0x4A7294
0x460FAD: mov      eax, dword ptr [eax]
0x460FAF: mov      edx, dword ptr [0x4f959c]
0x460FB5: cmp      eax, dword ptr [edx]
0x460FB7: je       0x461082
0x460FBD: lea      edx, [ebp - 0xbc]
0x460FC3: mov      eax, dword ptr [0x4f9384]
0x460FC8: call     0x407d64
0x460FCD: push     dword ptr [ebp - 0xbc]
0x460FD3: push     0x461134 ; "="
0x460FD8: lea      edx, [ebp - 0xc0]
0x460FDE: mov      eax, dword ptr [0x4f9590]
0x460FE3: call     0x407d64
0x460FE8: push     dword ptr [ebp - 0xc0]
0x460FEE: push     0x461134 ; "="
0x460FF3: lea      edx, [ebp - 0xc4]
0x460FF9: mov      eax, dword ptr [0x4f9598]
0x460FFE: mov      eax, dword ptr [eax]
0x461000: call     0x407d64
0x461005: push     dword ptr [ebp - 0xc4]
0x46100B: push     0x461140 ; ", "
0x461010: lea      edx, [ebp - 0xc8]
0x461016: mov      eax, dword ptr [0x4a4144] ; *0x4A4144->0x4A7294
0x46101B: mov      eax, dword ptr [eax]
0x46101D: call     0x407d64
0x461022: push     dword ptr [ebp - 0xc8]
0x461028: push     0x461134 ; "="
0x46102D: lea      edx, [ebp - 0xcc]
0x461033: mov      eax, dword ptr [0x4a4144] ; *0x4A4144->0x4A7294
0x461038: mov      eax, dword ptr [eax]
0x46103A: call     0x407d64
0x46103F: push     dword ptr [ebp - 0xcc]
0x461045: push     0x461134 ; "="
0x46104A: lea      edx, [ebp - 0xd0]
0x461050: mov      eax, dword ptr [0x4f959c]
0x461055: mov      eax, dword ptr [eax]
0x461057: call     0x407d64
0x46105C: push     dword ptr [ebp - 0xd0]
0x461062: lea      eax, [ebp - 0xb8]
0x461068: mov      edx, 0xb
0x46106D: call     0x403d94
0x461072: mov      edx, dword ptr [ebp - 0xb8]
0x461078: mov      eax, dword ptr [0x4f7d8c]
0x46107D: call     0x479a68
0x461082: mov      eax, dword ptr [0x4a4144] ; *0x4A4144->0x4A7294
0x461087: cmp      dword ptr [eax], 0
0x46108A: je       0x46109f
0x46108C: cmp      byte ptr [0x4f9382], 0
0x461093: jne      0x46109f
0x461095: mov      eax, dword ptr [0x4f7d88]
0x46109A: nop      
0x46109B: nop      
0x46109C: nop      
0x46109D: nop      
0x46109E: nop      
0x46109F: call     0x406154
0x4610A4: sub      eax, dword ptr [0x4f9250]
0x4610AA: cmp      eax, 0x1388
0x4610AF: jbe      0x4610f8
0x4610B1: call     0x406154
0x4610B6: mov      dword ptr [0x4f9250], eax
0x4610BB: mov      eax, dword ptr [0x4f95a0]
0x4610C0: mov      eax, dword ptr [eax]
0x4610C2: mov      edx, dword ptr [0x4f95a4]
0x4610C8: cmp      eax, dword ptr [edx]
0x4610CA: je       0x4610f8
0x4610CC: mov      eax, dword ptr [0x4f95a0]
0x4610D1: mov      eax, dword ptr [eax]
0x4610D3: mov      edx, dword ptr [0x4f95a8]
0x4610D9: cmp      eax, dword ptr [edx]
0x4610DB: je       0x4610f8
0x4610DD: mov      eax, dword ptr [0x4f95a0]
0x4610E2: mov      eax, dword ptr [eax]
0x4610E4: mov      edx, dword ptr [0x4f95ac]
0x4610EA: cmp      eax, dword ptr [edx]
0x4610EC: je       0x4610f8
0x4610EE: mov      eax, dword ptr [0x4f7d88]
0x4610F3: nop      
0x4610F4: nop      
0x4610F5: nop      
0x4610F6: nop      
0x4610F7: nop      
0x4610F8: xor      eax, eax
0x4610FA: pop      edx
0x4610FB: pop      ecx
0x4610FC: pop      ecx
0x4610FD: mov      dword ptr fs:[eax], edx
0x461100: push     0x46111d
0x461105: lea      eax, [ebp - 0xd0]
0x46110B: mov      edx, 7
0x461110: call     0x403a7c
0x461115: ret      
0x461116: jmp      0x403518
0x46111B: jmp      0x461105
0x46111D: pop      ebx
0x46111E: mov      esp, ebp
0x461120: pop      ebp
0x461121: ret      
0x461122: add      byte ptr [eax], al
0x461124: add      al, 0xbd
0x461127: mov      cl, 0xd2
0x461129: add      byte ptr [eax], al
0x46112B: add      bh, bh
0x46112F: inc      dword ptr [ecx]
0x461131: add      byte ptr [eax], al
0x461133: add      byte ptr [0xff000000], bh
0x46113B: inc      dword ptr [edx]
0x46113D: add      byte ptr [eax], al
0x46113F: add      byte ptr [eax], ch
0x461142: add      byte ptr [eax], al
0x461144: push     ebp
0x461145: mov      ebp, esp
0x461147: add      esp, -8
0x46114A: xor      edx, edx
0x46114C: mov      dword ptr [ebp - 8], edx
0x46114F: mov      dword ptr [ebp - 4], eax
0x461152: xor      eax, eax
0x461154: push     ebp
0x461155: push     0x46122a
0x46115A: push     dword ptr fs:[eax]
0x46115D: mov      dword ptr fs:[eax], esp
0x461160: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x461165: mov      eax, dword ptr [eax]
0x461167: mov      cx, word ptr [0x461238]
0x46116E: mov      edx, 0x461244 ; "你想退出吗 ?"
0x461173: call     0x486df0
0x461178: dec      eax
0x461179: jne      0x461214
0x46117F: push     0
0x461181: push     0
0x461183: push     0
0x461185: xor      ecx, ecx
0x461187: mov      edx, 0x3f1
0x46118C: mov      eax, dword ptr [ebp - 4]
0x46118F: call     0x464b18
0x461194: mov      eax, dword ptr [0x4f7d9c]
0x461199: call     0x4735dc
0x46119E: mov      eax, dword ptr [ebp - 4]
0x4611A1: call     0x4641d4
0x4611A6: cmp      byte ptr [0x4f937c], 0
0x4611AD: jne      0x4611c2
0x4611AF: mov      byte ptr [0x4f94c8], 1
0x4611B6: xor      edx, edx
0x4611B8: mov      eax, dword ptr [ebp - 4]
0x4611BB: call     0x463fe8
0x4611C0: jmp      0x4611cc
0x4611C2: mov      dl, 1
0x4611C4: mov      eax, dword ptr [ebp - 4]
0x4611C7: call     0x463fe8
0x4611CC: cmp      byte ptr [0x4f9235], 0
0x4611D3: je       0x46120d
0x4611D5: push     0x46125c ; ".\Data\"
0x4611DA: push     dword ptr [0x4f7ee4]
0x4611E0: push     0x46126c ; "."
0x4611E5: mov      eax, dword ptr [ebp - 4]
0x4611E8: push     dword ptr [eax + 0x418]
0x4611EE: push     0x461278 ; ".itm"
0x4611F3: lea      eax, [ebp - 8]
0x4611F6: mov      edx, 5
0x4611FB: call     0x403d94
0x461200: mov      eax, dword ptr [ebp - 8]
0x461203: mov      edx, 0x4f80d0
0x461208: call     0x49f744
0x46120D: mov      byte ptr [0x4f9235], 0
0x461214: xor      eax, eax
0x461216: pop      edx
0x461217: pop      ecx
0x461218: pop      ecx
0x461219: mov      dword ptr fs:[eax], edx
0x46121C: push     0x461231
0x461221: lea      eax, [ebp - 8]
0x461224: call     0x403a58
0x461229: ret      
0x46122A: jmp      0x403518
0x46122F: jmp      0x461221
0x461231: pop      ecx
0x461232: pop      ecx
0x461233: pop      ebp
0x461234: ret      
0x461235: add      byte ptr [eax], al
0x461237: add      byte ptr [eax + eax], cl
0x46123A: add      byte ptr [eax], al
0x461244: [STR len=12] "你想退出吗 ?"
0x46125C: [STR len=7] ".\Data\"
0x46126C: [STR len=1] "."
0x461278: [STR len=4] ".itm"
0x461280: push     ebp
0x461281: mov      ebp, esp
0x461283: add      esp, -8
0x461286: xor      edx, edx
0x461288: mov      dword ptr [ebp - 8], edx
0x46128B: mov      dword ptr [ebp - 4], eax
0x46128E: xor      eax, eax
0x461290: push     ebp
0x461291: push     0x46131f
0x461296: push     dword ptr fs:[eax]
0x461299: mov      dword ptr fs:[eax], esp
0x46129C: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x4612A1: mov      eax, dword ptr [eax]
0x4612A3: mov      cx, word ptr [0x46132c]
0x4612AA: mov      edx, 0x461338 ; "你想退出传奇吗？"
0x4612AF: call     0x486df0
0x4612B4: dec      eax
0x4612B5: jne      0x461309
0x4612B7: cmp      byte ptr [0x4f9235], 0
0x4612BE: je       0x4612f8
0x4612C0: push     0x461354 ; ".\Data\"
0x4612C5: push     dword ptr [0x4f7ee4]
0x4612CB: push     0x461364 ; "."
0x4612D0: mov      eax, dword ptr [ebp - 4]
0x4612D3: push     dword ptr [eax + 0x418]
0x4612D9: push     0x461370 ; ".itm"
0x4612DE: lea      eax, [ebp - 8]
0x4612E1: mov      edx, 5
0x4612E6: call     0x403d94
0x4612EB: mov      eax, dword ptr [ebp - 8]
0x4612EE: mov      edx, 0x4f80d0
0x4612F3: call     0x49f744
0x4612F8: mov      byte ptr [0x4f9235], 0
0x4612FF: mov      eax, dword ptr [0x4f7d88]
0x461304: call     0x4405bc
0x461309: xor      eax, eax
0x46130B: pop      edx
0x46130C: pop      ecx
0x46130D: pop      ecx
0x46130E: mov      dword ptr fs:[eax], edx
0x461311: push     0x461326
0x461316: lea      eax, [ebp - 8]
0x461319: call     0x403a58
0x46131E: ret      
0x46131F: jmp      0x403518
0x461324: jmp      0x461316
0x461326: pop      ecx
0x461327: pop      ecx
0x461328: pop      ebp
0x461329: ret      
0x46132A: add      byte ptr [eax], al
0x46132C: or       al, 0
0x46132E: add      byte ptr [eax], al
0x461338: [STR len=16] "你想退出传奇吗？"
0x461354: [STR len=7] ".\Data\"
0x461364: [STR len=1] "."
0x461370: [STR len=4] ".itm"
0x461378: push     ebp
0x461379: mov      ebp, esp
0x46137B: add      esp, -0xc
0x46137E: xor      ecx, ecx
0x461380: mov      dword ptr [ebp - 0xc], ecx
0x461383: mov      dword ptr [ebp - 8], edx
0x461386: mov      dword ptr [ebp - 4], eax
0x461389: xor      eax, eax
0x46138B: push     ebp
0x46138C: push     0x4613db
0x461391: push     dword ptr fs:[eax]
0x461394: mov      dword ptr fs:[eax], esp
0x461397: cmp      dword ptr [ebp - 4], 0xa
0x46139B: jge      0x4613ba
0x46139D: lea      edx, [ebp - 0xc]
0x4613A0: mov      eax, dword ptr [ebp - 4]
0x4613A3: call     0x407d64
0x4613A8: mov      ecx, dword ptr [ebp - 0xc]
0x4613AB: mov      eax, dword ptr [ebp - 8]
0x4613AE: mov      edx, 0x4613f0 ; "0"
0x4613B3: call     0x403d20
0x4613B8: jmp      0x4613c5
0x4613BA: mov      edx, dword ptr [ebp - 8]
0x4613BD: mov      eax, dword ptr [ebp - 4]
0x4613C0: call     0x407d64
0x4613C5: xor      eax, eax
0x4613C7: pop      edx
0x4613C8: pop      ecx
0x4613C9: pop      ecx
0x4613CA: mov      dword ptr fs:[eax], edx
0x4613CD: push     0x4613e2
0x4613D2: lea      eax, [ebp - 0xc]
0x4613D5: call     0x403a58
0x4613DA: ret      
0x4613DB: jmp      0x403518
0x4613E0: jmp      0x4613d2
0x4613E2: mov      esp, ebp
0x4613E4: pop      ebp
0x4613E5: ret      
0x4613E6: add      byte ptr [eax], al
0x4613F0: [STR len=1] "0"
0x4613F4: push     ebp
0x4613F5: mov      ebp, esp
0x4613F7: add      esp, 0xffffff64
0x4613FD: push     esi
0x4613FE: push     edi
0x4613FF: xor      edx, edx
0x461401: mov      dword ptr [ebp - 0x98], edx
0x461407: mov      dword ptr [ebp - 0x9c], edx
0x46140D: mov      dword ptr [ebp - 0x18], edx
0x461410: mov      dword ptr [ebp - 4], eax
0x461413: xor      eax, eax
0x461415: push     ebp
0x461416: push     0x461803
0x46141B: push     dword ptr fs:[eax]
0x46141E: mov      dword ptr fs:[eax], esp
0x461421: mov      eax, dword ptr [ebp - 4]
0x461424: mov      eax, dword ptr [eax + 0x2f4]
0x46142A: call     0x450e84
0x46142F: test     al, al
0x461431: je       0x4617dd
0x461437: push     0x461818 ; "Images"
0x46143C: push     ebp
0x46143D: lea      edx, [ebp - 0x98]
0x461443: mov      eax, dword ptr [0x4f9524]
0x461448: call     0x461378
0x46144D: pop      ecx
0x46144E: push     dword ptr [ebp - 0x98]
0x461454: push     0x461828 ; ".bmp"
0x461459: lea      eax, [ebp - 0x18]
0x46145C: mov      edx, 3
0x461461: call     0x403d94
0x461466: mov      eax, dword ptr [ebp - 0x18]
0x461469: call     0x407f90
0x46146E: test     al, al
0x461470: je       0x46147a
0x461472: inc      dword ptr [0x4f9524]
0x461478: jmp      0x461437
0x46147A: mov      dl, 1
0x46147C: mov      eax, dword ptr [0x447be8]
0x461481: call     0x449758
0x461486: mov      dword ptr [ebp - 0x1c], eax
0x461489: mov      edx, 8
0x46148E: mov      eax, dword ptr [ebp - 0x1c]
0x461491: call     0x44a404
0x461496: mov      edx, 0x320
0x46149B: mov      eax, dword ptr [ebp - 0x1c]
0x46149E: mov      ecx, dword ptr [eax]
0x4614A0: call     dword ptr [ecx + 0x40]
0x4614A3: mov      edx, 0x258
0x4614A8: mov      eax, dword ptr [ebp - 0x1c]
0x4614AB: mov      ecx, dword ptr [eax]
0x4614AD: call     dword ptr [ecx + 0x34]
0x4614B0: mov      eax, dword ptr [ebp - 4]
0x4614B3: mov      eax, dword ptr [eax + 0x308]
0x4614B9: mov      edx, dword ptr [ebp - 0x1c]
0x4614BC: lea      esi, [eax + 0x54]
0x4614BF: lea      edi, [edx + 0x84]
0x4614C5: mov      ecx, 0x100
0x4614CA: rep movsd dword ptr es:[edi], dword ptr [esi]
0x4614CC: mov      eax, dword ptr [ebp - 0x1c]
0x4614CF: call     0x44a7c4
0x4614D4: mov      dword ptr [ebp - 0x94], 0x6c
0x4614DE: xor      eax, eax
0x4614E0: mov      dword ptr [ebp - 0x14], eax
0x4614E3: xor      eax, eax
0x4614E5: push     ebp
0x4614E6: push     0x46157d
0x4614EB: push     dword ptr fs:[eax]
0x4614EE: mov      dword ptr fs:[eax], esp
0x4614F1: lea      ecx, [ebp - 0x94]
0x4614F7: xor      eax, eax
0x4614F9: mov      edx, eax
0x4614FB: mov      eax, dword ptr [ebp - 4]
0x4614FE: mov      eax, dword ptr [eax + 0x2f4]
0x461504: mov      eax, dword ptr [eax + 0x28c]
0x46150A: call     0x44de90
0x46150F: mov      dword ptr [ebp - 8], 0x1e0
0x461516: mov      eax, 0x257
0x46151B: sub      eax, dword ptr [ebp - 8]
0x46151E: imul     dword ptr [ebp - 0x84]
0x461524: add      eax, dword ptr [ebp - 0x70]
0x461527: add      eax, 0xc8
0x46152C: mov      dword ptr [ebp - 0x20], eax
0x46152F: xor      eax, eax
0x461531: mov      dword ptr [ebp - 0xc], eax
0x461534: mov      eax, dword ptr [ebp - 0x20]
0x461537: add      eax, dword ptr [ebp - 0xc]
0x46153A: movzx    eax, byte ptr [eax]
0x46153D: add      dword ptr [ebp - 0x14], eax
0x461540: inc      dword ptr [ebp - 0xc]
0x461543: cmp      dword ptr [ebp - 0xc], 0x190
0x46154A: jne      0x461534
0x46154C: inc      dword ptr [ebp - 8]
0x46154F: cmp      dword ptr [ebp - 8], 0x24f
0x461556: jne      0x461516
0x461558: xor      eax, eax
0x46155A: pop      edx
0x46155B: pop      ecx
0x46155C: pop      ecx
0x46155D: mov      dword ptr fs:[eax], edx
0x461560: push     0x461584
0x461565: mov      eax, dword ptr [ebp - 4]
0x461568: mov      eax, dword ptr [eax + 0x2f4]
0x46156E: mov      eax, dword ptr [eax + 0x28c]
0x461574: mov      edx, dword ptr [ebp - 0x70]
0x461577: call     0x44e5d8
0x46157C: ret      
0x46157D: jmp      0x403518
0x461582: jmp      0x461565
0x461584: xor      eax, eax
0x461586: push     ebp
0x461587: push     0x4617bb
0x46158C: push     dword ptr fs:[eax]
0x46158F: mov      dword ptr fs:[eax], esp
0x461592: push     1
0x461594: mov      eax, dword ptr [ebp - 4]
0x461597: mov      eax, dword ptr [eax + 0x2f4]
0x46159D: mov      eax, dword ptr [eax + 0x28c]
0x4615A3: call     0x44d8b4
0x4615A8: call     0x41834c
0x4615AD: push     eax
0x4615AE: call     0x406434
0x4615B3: mov      eax, dword ptr [ebp - 4]
0x4615B6: mov      eax, dword ptr [eax + 0x2f4]
0x4615BC: mov      eax, dword ptr [eax + 0x28c]
0x4615C2: call     0x44d8b4
0x4615C7: mov      eax, dword ptr [eax + 0xc]
0x4615CA: mov      edx, 0xffffff
0x4615CF: call     0x4173fc
0x4615D4: xor      eax, eax
0x4615D6: mov      dword ptr [ebp - 0x10], eax
0x4615D9: cmp      dword ptr [0x4f7ef8], 0
0x4615E0: je       0x46162c
0x4615E2: push     dword ptr [0x4f7ee4]
0x4615E8: push     0x461838 ; " "
0x4615ED: mov      eax, dword ptr [0x4f7ef8]
0x4615F2: push     dword ptr [eax + 0x28]
0x4615F5: lea      eax, [ebp - 0x98]
0x4615FB: mov      edx, 3
0x461600: call     0x403d94
0x461605: mov      eax, dword ptr [ebp - 0x98]
0x46160B: push     eax
0x46160C: mov      eax, dword ptr [ebp - 4]
0x46160F: mov      eax, dword ptr [eax + 0x2f4]
0x461615: mov      eax, dword ptr [eax + 0x28c]
0x46161B: call     0x44d8b4
0x461620: xor      ecx, ecx
0x461622: xor      edx, edx
0x461624: call     0x4180dc
0x461629: inc      dword ptr [ebp - 0x10]
0x46162C: lea      edx, [ebp - 0x9c]
0x461632: mov      eax, dword ptr [ebp - 0x14]
0x461635: call     0x407d64
0x46163A: mov      ecx, dword ptr [ebp - 0x9c]
0x461640: lea      eax, [ebp - 0x98]
0x461646: mov      edx, 0x461844 ; "CheckSum="
0x46164B: call     0x403d20
0x461650: mov      eax, dword ptr [ebp - 0x98]
0x461656: push     eax
0x461657: mov      eax, dword ptr [ebp - 4]
0x46165A: mov      eax, dword ptr [eax + 0x2f4]
0x461660: mov      eax, dword ptr [eax + 0x28c]
0x461666: call     0x44d8b4
0x46166B: mov      ecx, dword ptr [ebp - 0x10]
0x46166E: shl      ecx, 2
0x461671: lea      ecx, [ecx + ecx*2]
0x461674: xor      edx, edx
0x461676: call     0x4180dc
0x46167B: call     0x409090
0x461680: add      esp, -8
0x461683: fstp     qword ptr [esp]
0x461686: wait     
0x461687: lea      eax, [ebp - 0x98]
0x46168D: call     0x409c5c
0x461692: mov      eax, dword ptr [ebp - 0x98]
0x461698: push     eax
0x461699: mov      eax, dword ptr [ebp - 4]
0x46169C: mov      eax, dword ptr [eax + 0x2f4]
0x4616A2: mov      eax, dword ptr [eax + 0x28c]
0x4616A8: call     0x44d8b4
0x4616AD: mov      edx, dword ptr [ebp - 0x10]
0x4616B0: inc      edx
0x4616B1: mov      ecx, edx
0x4616B3: shl      ecx, 2
0x4616B6: lea      ecx, [ecx + ecx*2]
0x4616B9: xor      edx, edx
0x4616BB: call     0x4180dc
0x4616C0: call     0x4090bc
0x4616C5: add      esp, -8
0x4616C8: fstp     qword ptr [esp]
0x4616CB: wait     
0x4616CC: lea      eax, [ebp - 0x98]
0x4616D2: call     0x409c74
0x4616D7: mov      eax, dword ptr [ebp - 0x98]
0x4616DD: push     eax
0x4616DE: mov      eax, dword ptr [ebp - 4]
0x4616E1: mov      eax, dword ptr [eax + 0x2f4]
0x4616E7: mov      eax, dword ptr [eax + 0x28c]
0x4616ED: call     0x44d8b4
0x4616F2: mov      edx, dword ptr [ebp - 0x10]
0x4616F5: add      edx, 2
0x4616F8: mov      ecx, edx
0x4616FA: shl      ecx, 2
0x4616FD: lea      ecx, [ecx + ecx*2]
0x461700: xor      edx, edx
0x461702: call     0x4180dc
0x461707: mov      eax, dword ptr [ebp - 4]
0x46170A: mov      eax, dword ptr [eax + 0x2f4]
0x461710: mov      eax, dword ptr [eax + 0x28c]
0x461716: call     0x44d8b4
0x46171B: call     0x44d104
0x461720: lea      ecx, [ebp - 0x94]
0x461726: xor      eax, eax
0x461728: mov      edx, eax
0x46172A: mov      eax, dword ptr [ebp - 4]
0x46172D: mov      eax, dword ptr [eax + 0x2f4]
0x461733: mov      eax, dword ptr [eax + 0x28c]
0x461739: call     0x44de90
0x46173E: mov      eax, dword ptr [ebp - 0x1c]
0x461741: mov      eax, dword ptr [eax + 0x38]
0x461744: dec      eax
0x461745: test     eax, eax
0x461747: jl       0x461796
0x461749: inc      eax
0x46174A: mov      dword ptr [ebp - 0x28], eax
0x46174D: mov      dword ptr [ebp - 8], 0
0x461754: mov      eax, dword ptr [ebp - 0x1c]
0x461757: mov      eax, dword ptr [eax + 0x38]
0x46175A: dec      eax
0x46175B: sub      eax, dword ptr [ebp - 8]
0x46175E: imul     dword ptr [ebp - 0x84]
0x461764: add      eax, dword ptr [ebp - 0x70]
0x461767: mov      dword ptr [ebp - 0x20], eax
0x46176A: mov      eax, dword ptr [ebp - 0x1c]
0x46176D: call     0x449edc
0x461772: imul     edx, dword ptr [ebp - 8], 0x320
0x461779: add      eax, edx
0x46177B: mov      dword ptr [ebp - 0x24], eax
0x46177E: mov      edx, dword ptr [ebp - 0x24]
0x461781: mov      eax, dword ptr [ebp - 0x20]
0x461784: mov      ecx, 0x320
0x461789: call     0x4027cc
0x46178E: inc      dword ptr [ebp - 8]
0x461791: dec      dword ptr [ebp - 0x28]
0x461794: jne      0x461754
0x461796: xor      eax, eax
0x461798: pop      edx
0x461799: pop      ecx
0x46179A: pop      ecx
0x46179B: mov      dword ptr fs:[eax], edx
0x46179E: push     0x4617c2
0x4617A3: mov      eax, dword ptr [ebp - 4]
0x4617A6: mov      eax, dword ptr [eax + 0x2f4]
0x4617AC: mov      eax, dword ptr [eax + 0x28c]
0x4617B2: mov      edx, dword ptr [ebp - 0x70]
0x4617B5: call     0x44e5d8
0x4617BA: ret      
0x4617BB: jmp      0x403518
0x4617C0: jmp      0x4617a3
0x4617C2: mov      edx, dword ptr [ebp - 0x18]
0x4617C5: mov      eax, dword ptr [ebp - 0x1c]
0x4617C8: mov      ecx, dword ptr [eax]
0x4617CA: call     dword ptr [ecx + 0x4c]
0x4617CD: mov      eax, dword ptr [ebp - 0x1c]
0x4617D0: mov      edx, dword ptr [eax]
0x4617D2: call     dword ptr [edx + 0x60]
0x4617D5: mov      eax, dword ptr [ebp - 0x1c]
0x4617D8: call     0x402eac
0x4617DD: xor      eax, eax
0x4617DF: pop      edx
0x4617E0: pop      ecx
0x4617E1: pop      ecx
0x4617E2: mov      dword ptr fs:[eax], edx
0x4617E5: push     0x46180a
0x4617EA: lea      eax, [ebp - 0x9c]
0x4617F0: mov      edx, 2
0x4617F5: call     0x403a7c
0x4617FA: lea      eax, [ebp - 0x18]
0x4617FD: call     0x403a58
0x461802: ret      
0x461803: jmp      0x403518
0x461808: jmp      0x4617ea
0x46180A: pop      edi
0x46180B: pop      esi
0x46180C: mov      esp, ebp
0x46180E: pop      ebp
0x46180F: ret      
0x461818: [STR len=6] "Images"
0x461828: [STR len=4] ".bmp"
0x461838: [STR len=1] " "
0x461844: [STR len=9] "CheckSum="
0x461850: push     ebp
0x461851: mov      ebp, esp
0x461853: push     ecx
0x461854: mov      dword ptr [ebp - 4], eax
0x461857: mov      eax, dword ptr [ebp - 4]
0x46185A: mov      ax, word ptr [eax + 0x3f0]
0x461861: add      eax, -0x70
0x461864: sub      ax, 8
0x461868: jae      0x4618ad
0x46186A: mov      eax, dword ptr [ebp - 4]
0x46186D: mov      dx, word ptr [eax + 0x3f0]
0x461874: sub      dl, 0x70
0x461877: add      dl, 0x31
0x46187A: mov      eax, dword ptr [ebp - 4]
0x46187D: call     0x46263c
0x461882: push     eax
0x461883: mov      ecx, dword ptr [0x4f9498]
0x461889: mov      edx, dword ptr [0x4f9494]
0x46188F: mov      eax, dword ptr [ebp - 4]
0x461892: call     0x462698
0x461897: mov      eax, dword ptr [ebp - 4]
0x46189A: mov      word ptr [eax + 0x3f0], 0
0x4618A3: mov      dword ptr [0x4f949c], 0xffffffff
0x4618AD: pop      ecx
0x4618AE: pop      ebp
0x4618AF: ret      
0x4618B0: push     ebp
0x4618B1: mov      ebp, esp
0x4618B3: add      esp, -0x20
0x4618B6: mov      dword ptr [ebp - 4], eax
0x4618B9: cmp      dword ptr [0x4f7ef8], 0
0x4618C0: je       0x461de5
0x4618C6: cmp      dword ptr [0x4f949c], 0
0x4618CD: jl       0x461ca4
0x4618D3: mov      eax, dword ptr [ebp - 4]
0x4618D6: call     0x466f60
0x4618DB: test     al, al
0x4618DD: je       0x461ca4
0x4618E3: mov      eax, dword ptr [ebp - 4]
0x4618E6: call     0x466f1c
0x4618EB: test     al, al
0x4618ED: je       0x461ca4
0x4618F3: mov      eax, dword ptr [0x4f7ef8]
0x4618F8: movzx    eax, word ptr [eax + 8]
0x4618FC: cmp      eax, dword ptr [0x4f949c]
0x461902: jne      0x461919
0x461904: mov      eax, dword ptr [0x4f7ef8]
0x461909: movzx    eax, word ptr [eax + 0xa]
0x46190D: cmp      eax, dword ptr [0x4f94a0]
0x461913: je       0x461ca4
0x461919: mov      eax, dword ptr [0x4f7ef8]
0x46191E: movzx    eax, word ptr [eax + 8]
0x461922: mov      dword ptr [ebp - 8], eax
0x461925: mov      eax, dword ptr [0x4f7ef8]
0x46192A: movzx    eax, word ptr [eax + 0xa]
0x46192E: mov      dword ptr [ebp - 0xc], eax
0x461931: mov      eax, dword ptr [0x4f949c]
0x461936: mov      dword ptr [ebp - 0x10], eax
0x461939: mov      eax, dword ptr [0x4f94a0]
0x46193E: mov      dword ptr [ebp - 0x14], eax
0x461941: mov      eax, dword ptr [ebp - 0x14]
0x461944: push     eax
0x461945: mov      ecx, dword ptr [ebp - 0x10]
0x461948: mov      edx, dword ptr [ebp - 0xc]
0x46194B: mov      eax, dword ptr [ebp - 8]
0x46194E: call     0x4a0104
0x461953: mov      byte ptr [ebp - 0x19], al
0x461956: mov      al, byte ptr [0x4f94c0]
0x46195B: sub      al, 1
0x46195D: jb       0x46196a
0x46195F: je       0x461bb2
0x461965: jmp      0x461ca4
0x46196A: mov      eax, dword ptr [0x4f7ef8]
0x46196F: call     0x497560
0x461974: mov      dword ptr [ebp - 0x18], eax
0x461977: xor      ecx, ecx
0x461979: mov      cl, byte ptr [ebp - 0x19]
0x46197C: mov      edx, 0xbc3
0x461981: mov      eax, dword ptr [ebp - 4]
0x461984: call     0x467094
0x461989: test     al, al
0x46198B: je       0x461ba3
0x461991: cmp      dword ptr [ebp - 0x18], 0
0x461995: jle      0x461ba3
0x46199B: lea      ecx, [ebp - 0xc]
0x46199E: lea      edx, [ebp - 8]
0x4619A1: mov      al, byte ptr [ebp - 0x19]
0x4619A4: call     0x49ff80
0x4619A9: mov      byte ptr [ebp - 0x1c], 1
0x4619AD: mov      byte ptr [ebp - 0x1d], 0
0x4619B1: mov      ecx, dword ptr [ebp - 0xc]
0x4619B4: mov      edx, dword ptr [ebp - 8]
0x4619B7: mov      eax, dword ptr [0x4f7d9c]
0x4619BC: call     0x472ce0
0x4619C1: test     al, al
0x4619C3: jne      0x461b71
0x4619C9: mov      byte ptr [ebp - 0x1c], 0
0x4619CD: mov      byte ptr [ebp - 0x1a], 0
0x4619D1: cmp      byte ptr [ebp - 0x1c], 0
0x4619D5: jne      0x461a13
0x4619D7: mov      eax, dword ptr [0x4f7ef8]
0x4619DC: movzx    eax, word ptr [eax + 8]
0x4619E0: mov      dword ptr [ebp - 8], eax
0x4619E3: mov      eax, dword ptr [0x4f7ef8]
0x4619E8: movzx    eax, word ptr [eax + 0xa]
0x4619EC: mov      dword ptr [ebp - 0xc], eax
0x4619EF: lea      ecx, [ebp - 0xc]
0x4619F2: lea      edx, [ebp - 8]
0x4619F5: mov      al, byte ptr [ebp - 0x19]
0x4619F8: call     0x49ff80
0x4619FD: mov      ecx, dword ptr [ebp - 0xc]
0x461A00: mov      edx, dword ptr [ebp - 8]
0x461A03: mov      eax, dword ptr [ebp - 4]
0x461A06: call     0x463c2c
0x461A0B: test     al, al
0x461A0D: je       0x461a13
0x461A0F: mov      byte ptr [ebp - 0x1d], 1
0x461A13: cmp      byte ptr [ebp - 0x1d], 0
0x461A17: jne      0x461acd
0x461A1D: mov      ecx, dword ptr [ebp - 0xc]
0x461A20: mov      edx, dword ptr [ebp - 8]
0x461A23: mov      eax, dword ptr [0x4f7d9c]
0x461A28: call     0x472d24
0x461A2D: test     al, al
0x461A2F: jne      0x461acd
0x461A35: mov      eax, dword ptr [0x4f7ef8]
0x461A3A: movzx    eax, word ptr [eax + 8]
0x461A3E: mov      dword ptr [ebp - 8], eax
0x461A41: mov      eax, dword ptr [0x4f7ef8]
0x461A46: movzx    eax, word ptr [eax + 0xa]
0x461A4A: mov      dword ptr [ebp - 0xc], eax
0x461A4D: xor      eax, eax
0x461A4F: mov      al, byte ptr [ebp - 0x19]
0x461A52: call     0x4a0950
0x461A57: mov      byte ptr [ebp - 0x1a], al
0x461A5A: lea      ecx, [ebp - 0xc]
0x461A5D: lea      edx, [ebp - 8]
0x461A60: mov      al, byte ptr [ebp - 0x1a]
0x461A63: call     0x49ff80
0x461A68: mov      ecx, dword ptr [ebp - 0xc]
0x461A6B: mov      edx, dword ptr [ebp - 8]
0x461A6E: mov      eax, dword ptr [0x4f7ef4]
0x461A73: call     0x49f120
0x461A78: test     al, al
0x461A7A: jne      0x461ac9
0x461A7C: mov      eax, dword ptr [0x4f7ef8]
0x461A81: movzx    eax, word ptr [eax + 8]
0x461A85: mov      dword ptr [ebp - 8], eax
0x461A88: mov      eax, dword ptr [0x4f7ef8]
0x461A8D: movzx    eax, word ptr [eax + 0xa]
0x461A91: mov      dword ptr [ebp - 0xc], eax
0x461A94: xor      eax, eax
0x461A96: mov      al, byte ptr [ebp - 0x19]
0x461A99: call     0x4a0978
0x461A9E: mov      byte ptr [ebp - 0x1a], al
0x461AA1: lea      ecx, [ebp - 0xc]
0x461AA4: lea      edx, [ebp - 8]
0x461AA7: mov      al, byte ptr [ebp - 0x1a]
0x461AAA: call     0x49ff80
0x461AAF: mov      ecx, dword ptr [ebp - 0xc]
0x461AB2: mov      edx, dword ptr [ebp - 8]
0x461AB5: mov      eax, dword ptr [0x4f7ef4]
0x461ABA: call     0x49f120
0x461ABF: test     al, al
0x461AC1: je       0x461acd
0x461AC3: mov      byte ptr [ebp - 0x1c], 1
0x461AC7: jmp      0x461acd
0x461AC9: mov      byte ptr [ebp - 0x1c], 1
0x461ACD: cmp      byte ptr [ebp - 0x1c], 0
0x461AD1: je       0x461b05
0x461AD3: mov      eax, dword ptr [ebp - 0xc]
0x461AD6: push     eax
0x461AD7: xor      eax, eax
0x461AD9: mov      al, byte ptr [ebp - 0x1a]
0x461ADC: push     eax
0x461ADD: push     0
0x461ADF: push     0
0x461AE1: push     0
0x461AE3: push     0
0x461AE5: mov      ecx, dword ptr [ebp - 8]
0x461AE8: mov      dx, 0xbc3
0x461AEC: mov      eax, dword ptr [0x4f7ef8]
0x461AF1: call     0x496604
0x461AF6: call     0x406154
0x461AFB: mov      dword ptr [0x4f9398], eax
0x461B00: jmp      0x461ca4
0x461B05: mov      eax, dword ptr [ebp - 0x14]
0x461B08: push     eax
0x461B09: mov      eax, dword ptr [0x4f7ef8]
0x461B0E: movzx    edx, word ptr [eax + 0xa]
0x461B12: mov      eax, dword ptr [0x4f7ef8]
0x461B17: movzx    eax, word ptr [eax + 8]
0x461B1B: mov      ecx, dword ptr [ebp - 0x10]
0x461B1E: call     0x4a0104
0x461B23: mov      byte ptr [ebp - 0x1b], al
0x461B26: mov      eax, dword ptr [0x4f7ef8]
0x461B2B: mov      al, byte ptr [eax + 0xc]
0x461B2E: cmp      al, byte ptr [ebp - 0x1b]
0x461B31: je       0x461b62
0x461B33: mov      eax, dword ptr [0x4f7ef8]
0x461B38: movzx    eax, word ptr [eax + 0xa]
0x461B3C: push     eax
0x461B3D: xor      eax, eax
0x461B3F: mov      al, byte ptr [ebp - 0x1b]
0x461B42: push     eax
0x461B43: push     0
0x461B45: push     0
0x461B47: push     0
0x461B49: push     0
0x461B4B: mov      eax, dword ptr [0x4f7ef8]
0x461B50: movzx    ecx, word ptr [eax + 8]
0x461B54: mov      dx, 0xbc2
0x461B58: mov      eax, dword ptr [0x4f7ef8]
0x461B5D: call     0x496548
0x461B62: mov      dword ptr [0x4f949c], 0xffffffff
0x461B6C: jmp      0x461ca4
0x461B71: mov      eax, dword ptr [ebp - 0xc]
0x461B74: push     eax
0x461B75: xor      eax, eax
0x461B77: mov      al, byte ptr [ebp - 0x19]
0x461B7A: push     eax
0x461B7B: push     0
0x461B7D: push     0
0x461B7F: push     0
0x461B81: push     0
0x461B83: mov      ecx, dword ptr [ebp - 8]
0x461B86: mov      dx, 0xbc3
0x461B8A: mov      eax, dword ptr [0x4f7ef8]
0x461B8F: call     0x496604
0x461B94: call     0x406154
0x461B99: mov      dword ptr [0x4f9398], eax
0x461B9E: jmp      0x461ca4
0x461BA3: mov      dword ptr [0x4f949c], 0xffffffff
0x461BAD: jmp      0x461ca4
0x461BB2: cmp      dword ptr [0x4f94c4], 1
0x461BB9: jl       0x461c99
0x461BBF: mov      eax, dword ptr [0x4f7ef8]
0x461BC4: call     0x497598
0x461BC9: mov      dword ptr [ebp - 0x18], eax
0x461BCC: mov      eax, dword ptr [ebp - 0x14]
0x461BCF: push     eax
0x461BD0: mov      ecx, dword ptr [ebp - 0x10]
0x461BD3: mov      edx, dword ptr [ebp - 0xc]
0x461BD6: mov      eax, dword ptr [ebp - 8]
0x461BD9: call     0x49ff48
0x461BDE: cmp      eax, 2
0x461BE1: jl       0x46196a
0x461BE7: cmp      dword ptr [ebp - 0x18], 0
0x461BEB: jle      0x46196a
0x461BF1: xor      ecx, ecx
0x461BF3: mov      cl, byte ptr [ebp - 0x19]
0x461BF6: mov      edx, 0xbc5
0x461BFB: mov      eax, dword ptr [ebp - 4]
0x461BFE: call     0x467094
0x461C03: test     al, al
0x461C05: je       0x461c8d
0x461C0B: lea      ecx, [ebp - 0xc]
0x461C0E: lea      edx, [ebp - 8]
0x461C11: mov      al, byte ptr [ebp - 0x19]
0x461C14: call     0x4a003c
0x461C19: mov      eax, dword ptr [ebp - 8]
0x461C1C: push     eax
0x461C1D: mov      eax, dword ptr [ebp - 0xc]
0x461C20: push     eax
0x461C21: mov      eax, dword ptr [0x4f7ef8]
0x461C26: movzx    ecx, word ptr [eax + 0xa]
0x461C2A: mov      eax, dword ptr [0x4f7ef8]
0x461C2F: movzx    edx, word ptr [eax + 8]
0x461C33: mov      eax, dword ptr [0x4f7d9c]
0x461C38: call     0x472c64
0x461C3D: test     al, al
0x461C3F: je       0x461c70
0x461C41: mov      eax, dword ptr [ebp - 0xc]
0x461C44: push     eax
0x461C45: xor      eax, eax
0x461C47: mov      al, byte ptr [ebp - 0x19]
0x461C4A: push     eax
0x461C4B: push     0
0x461C4D: push     0
0x461C4F: push     0
0x461C51: push     0
0x461C53: mov      ecx, dword ptr [ebp - 8]
0x461C56: mov      dx, 0xbc5
0x461C5A: mov      eax, dword ptr [0x4f7ef8]
0x461C5F: call     0x496604
0x461C64: call     0x406154
0x461C69: mov      dword ptr [0x4f9398], eax
0x461C6E: jmp      0x461ca4
0x461C70: mov      eax, dword ptr [0x4f7ef8]
0x461C75: movzx    eax, word ptr [eax + 8]
0x461C79: mov      dword ptr [ebp - 8], eax
0x461C7C: mov      eax, dword ptr [0x4f7ef8]
0x461C81: movzx    eax, word ptr [eax + 0xa]
0x461C85: mov      dword ptr [ebp - 0xc], eax
0x461C88: jmp      0x46196a
0x461C8D: mov      dword ptr [0x4f949c], 0xffffffff
0x461C97: jmp      0x461ca4
0x461C99: inc      dword ptr [0x4f94c4]
0x461C9F: jmp      0x46196a
0x461CA4: mov      dword ptr [0x4f949c], 0xffffffff
0x461CAE: mov      eax, dword ptr [0x4f7ef8]
0x461CB3: cmp      dword ptr [eax + 0x224], 0
0x461CBA: jle      0x461de5
0x461CC0: mov      eax, dword ptr [0x4f7ef8]
0x461CC5: mov      eax, dword ptr [eax + 0x224]
0x461CCB: mov      edx, dword ptr [ebp - 4]
0x461CCE: mov      dword ptr [edx + 0x3e4], eax
0x461CD4: mov      eax, dword ptr [0x4f7ef8]
0x461CD9: mov      eax, dword ptr [eax + 0x230]
0x461CDF: mov      edx, dword ptr [ebp - 4]
0x461CE2: mov      dword ptr [edx + 0x3e8], eax
0x461CE8: call     0x406154
0x461CED: mov      edx, dword ptr [ebp - 4]
0x461CF0: mov      dword ptr [edx + 0x3ec], eax
0x461CF6: mov      eax, dword ptr [0x4f7ef8]
0x461CFB: cmp      dword ptr [eax + 0x224], 0xbc9
0x461D05: jne      0x461d4b
0x461D07: mov      eax, dword ptr [0x4f7ef8]
0x461D0C: mov      eax, dword ptr [eax + 0x22c]
0x461D12: push     eax
0x461D13: mov      eax, dword ptr [0x4f7ef8]
0x461D18: mov      eax, dword ptr [eax + 0x230]
0x461D1E: push     eax
0x461D1F: mov      eax, dword ptr [0x4f7ef8]
0x461D24: mov      eax, dword ptr [eax + 0x238]
0x461D2A: push     eax
0x461D2B: mov      eax, dword ptr [0x4f7ef8]
0x461D30: mov      ecx, dword ptr [eax + 0x228]
0x461D36: mov      eax, dword ptr [0x4f7ef8]
0x461D3B: mov      edx, dword ptr [eax + 0x224]
0x461D41: mov      eax, dword ptr [ebp - 4]
0x461D44: call     0x465784
0x461D49: jmp      0x461d81
0x461D4B: mov      eax, dword ptr [0x4f7ef8]
0x461D50: mov      eax, dword ptr [eax + 0x22c]
0x461D56: push     eax
0x461D57: mov      eax, dword ptr [0x4f7ef8]
0x461D5C: mov      eax, dword ptr [eax + 0x230]
0x461D62: push     eax
0x461D63: mov      eax, dword ptr [0x4f7ef8]
0x461D68: mov      ecx, dword ptr [eax + 0x228]
0x461D6E: mov      eax, dword ptr [0x4f7ef8]
0x461D73: mov      edx, dword ptr [eax + 0x224]
0x461D79: mov      eax, dword ptr [ebp - 4]
0x461D7C: call     0x4656e4
0x461D81: mov      eax, dword ptr [0x4f7ef8]
0x461D86: xor      edx, edx
0x461D88: mov      dword ptr [eax + 0x224], edx
0x461D8E: cmp      dword ptr [0x4f94e0], -1
0x461D95: je       0x461de5
0x461D97: mov      eax, dword ptr [0x4f7ef8]
0x461D9C: movzx    eax, word ptr [eax + 8]
0x461DA0: push     eax
0x461DA1: mov      eax, dword ptr [0x4f94e0]
0x461DA6: pop      edx
0x461DA7: sub      eax, edx
0x461DA9: cdq      
0x461DAA: xor      eax, edx
0x461DAC: sub      eax, edx
0x461DAE: cmp      eax, 8
0x461DB1: jge      0x461dcf
0x461DB3: mov      eax, dword ptr [0x4f7ef8]
0x461DB8: movzx    eax, word ptr [eax + 0xa]
0x461DBC: push     eax
0x461DBD: mov      eax, dword ptr [0x4f94e4]
0x461DC2: pop      edx
0x461DC3: sub      eax, edx
0x461DC5: cdq      
0x461DC6: xor      eax, edx
0x461DC8: sub      eax, edx
0x461DCA: cmp      eax, 8
0x461DCD: jl       0x461de5
0x461DCF: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x461DD4: mov      eax, dword ptr [eax]
0x461DD6: call     0x48e9d0
0x461DDB: mov      dword ptr [0x4f94e0], 0xffffffff
0x461DE5: mov      esp, ebp
0x461DE7: pop      ebp
0x461DE8: ret      
0x461DE9: lea      eax, [eax]
0x461DEC: push     ebp
0x461DED: mov      ebp, esp
0x461DEF: add      esp, -0x24
0x461DF2: push     ebx
0x461DF3: mov      dword ptr [ebp - 8], ecx
0x461DF6: mov      dword ptr [ebp - 0x18], edx
0x461DF9: mov      dword ptr [ebp - 4], eax
0x461DFC: mov      eax, dword ptr [ebp - 8]
0x461DFF: mov      ax, word ptr [eax]
0x461E02: sub      ax, 0x13
0x461E06: jne      0x461e18
0x461E08: mov      eax, dword ptr [ebp - 8]
0x461E0B: mov      word ptr [eax], 0
0x461E10: mov      eax, dword ptr [ebp - 4]
0x461E13: call     0x4613f4
0x461E18: mov      edx, dword ptr [ebp - 8]
0x461E1B: mov      cl, byte ptr [ebp + 8]
0x461E1E: mov      eax, dword ptr [ebp - 4]
0x461E21: mov      eax, dword ptr [eax + 0x31c]
0x461E27: call     0x4585e4
0x461E2C: test     al, al
0x461E2E: jne      0x462263
0x461E34: cmp      dword ptr [0x4f7ef8], 0
0x461E3B: je       0x462263
0x461E41: mov      eax, dword ptr [0x4f7d8c]
0x461E46: mov      eax, dword ptr [eax + 0x14]
0x461E49: cmp      eax, dword ptr [0x4f7d9c]
0x461E4F: jne      0x462263
0x461E55: mov      eax, dword ptr [0x4f7ef8]
0x461E5A: movzx    eax, word ptr [eax + 8]
0x461E5E: mov      dword ptr [ebp - 0xc], eax
0x461E61: mov      eax, dword ptr [0x4f7ef8]
0x461E66: movzx    eax, word ptr [eax + 0xa]
0x461E6A: mov      dword ptr [ebp - 0x10], eax
0x461E6D: mov      eax, dword ptr [ebp - 8]
0x461E70: movzx    eax, word ptr [eax]
0x461E73: cmp      eax, 0x58
0x461E76: jg       0x461ea7
0x461E78: je       0x462090
0x461E7E: sub      eax, 0x41
0x461E81: je       0x461f63
0x461E87: sub      eax, 5
0x461E8A: je       0x461f7f
0x461E90: sub      eax, 2
0x461E93: je       0x461f47
0x461E99: sub      eax, 9
0x461E9C: je       0x462110
0x461EA2: jmp      0x462187
0x461EA7: add      eax, -0x70
0x461EAA: sub      eax, 8
0x461EAD: jb       0x461ebc
0x461EAF: je       0x461ef4
0x461EB1: dec      eax
0x461EB2: je       0x461f05
0x461EB4: dec      eax
0x461EB5: je       0x461f25
0x461EB7: jmp      0x462187
0x461EBC: call     0x406154
0x461EC1: sub      eax, dword ptr [0x4f944c]
0x461EC7: mov      edx, dword ptr [0x4f94b8]
0x461ECD: add      edx, 0x1f4
0x461ED3: cmp      eax, edx
0x461ED5: jbe      0x461ee7
0x461ED7: mov      eax, dword ptr [ebp - 8]
0x461EDA: mov      ax, word ptr [eax]
0x461EDD: mov      edx, dword ptr [ebp - 4]
0x461EE0: mov      word ptr [edx + 0x3f0], ax
0x461EE7: mov      eax, dword ptr [ebp - 8]
0x461EEA: mov      word ptr [eax], 0
0x461EEF: jmp      0x462187
0x461EF4: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x461EF9: mov      eax, dword ptr [eax]
0x461EFB: call     0x4864fc
0x461F00: jmp      0x462187
0x461F05: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x461F0A: mov      eax, dword ptr [eax]
0x461F0C: xor      edx, edx
0x461F0E: mov      dword ptr [eax + 0x618], edx
0x461F14: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x461F19: mov      eax, dword ptr [eax]
0x461F1B: call     0x48647c
0x461F20: jmp      0x462187
0x461F25: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x461F2A: mov      eax, dword ptr [eax]
0x461F2C: mov      dword ptr [eax + 0x618], 3
0x461F36: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x461F3B: mov      eax, dword ptr [eax]
0x461F3D: call     0x48647c
0x461F42: jmp      0x462187
0x461F47: test     byte ptr [ebp + 8], 4
0x461F4B: je       0x462187
0x461F51: mov      edx, 0x462274 ; "@AttackMode"
0x461F56: mov      eax, dword ptr [ebp - 4]
0x461F59: call     0x46549c
0x461F5E: jmp      0x462187
0x461F63: test     byte ptr [ebp + 8], 4
0x461F67: je       0x462187
0x461F6D: mov      edx, 0x462288 ; "@Rest"
0x461F72: mov      eax, dword ptr [ebp - 4]
0x461F75: call     0x46549c
0x461F7A: jmp      0x462187
0x461F7F: test     byte ptr [ebp + 8], 4
0x461F83: je       0x462187
0x461F89: cmp      dword ptr [0x4a2d08], 3 ; *0x4A2D08->0x000000
0x461F90: jge      0x461f9a
0x461F92: inc      dword ptr [0x4a2d08] ; *0x4A2D08->0x000000
0x461F98: jmp      0x461fa1
0x461F9A: xor      eax, eax
0x461F9C: mov      dword ptr [0x4a2d08], eax ; *0x4A2D08->0x000000
0x461FA1: mov      eax, 0x4a2d0c ; *0x4A2D0C->0x45E3E4
0x461FA6: mov      edx, dword ptr [0x4a2d08] ; *0x4A2D08->0x000000
0x461FAC: mov      edx, dword ptr [edx*4 + 0x4a2cf8] ; *0x4A2CF8->0x45E388
0x461FB3: call     0x403aac
0x461FB8: mov      eax, dword ptr [0x4f7d88]
0x461FBD: mov      eax, dword ptr [eax + 0x58]
0x461FC0: mov      edx, dword ptr [0x4a2d0c] ; *0x4A2D0C->0x45E3E4
0x461FC6: call     0x417618
0x461FCB: mov      eax, dword ptr [0x4f7d88]
0x461FD0: call     0x43e1f0
0x461FD5: mov      eax, dword ptr [eax + 0xc]
0x461FD8: mov      edx, dword ptr [0x4a2d0c] ; *0x4A2D0C->0x45E3E4
0x461FDE: call     0x417618
0x461FE3: mov      eax, dword ptr [ebp - 4]
0x461FE6: mov      eax, dword ptr [eax + 0x2f4]
0x461FEC: mov      eax, dword ptr [eax + 0x294]
0x461FF2: call     0x44d8b4
0x461FF7: mov      eax, dword ptr [eax + 0xc]
0x461FFA: mov      edx, dword ptr [0x4a2d0c] ; *0x4A2D0C->0x45E3E4
0x462000: call     0x417618
0x462005: mov      eax, dword ptr [0x4f7d9c]
0x46200A: mov      eax, dword ptr [eax + 0x5a850]
0x462010: mov      eax, dword ptr [eax + 0x58]
0x462013: mov      edx, dword ptr [0x4a2d0c] ; *0x4A2D0C->0x45E3E4
0x462019: call     0x417618
0x46201E: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x462023: mov      eax, dword ptr [eax]
0x462025: mov      eax, dword ptr [eax + 0x58]
0x462028: mov      edx, dword ptr [0x4a2d0c] ; *0x4A2D0C->0x45E3E4
0x46202E: call     0x417618
0x462033: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x462038: mov      eax, dword ptr [eax]
0x46203A: call     0x43e1f0
0x46203F: mov      eax, dword ptr [eax + 0xc]
0x462042: mov      edx, dword ptr [0x4a2d0c] ; *0x4A2D0C->0x45E3E4
0x462048: call     0x417618
0x46204D: mov      ecx, 0x462298 ; ".\mirsetup.ini"
0x462052: mov      dl, 1
0x462054: mov      eax, dword ptr [0x44628c]
0x462059: call     0x446334
0x46205E: mov      dword ptr [ebp - 0x14], eax
0x462061: cmp      dword ptr [ebp - 0x14], 0
0x462065: je       0x462187
0x46206B: mov      eax, dword ptr [0x4a2d0c] ; *0x4A2D0C->0x45E3E4
0x462070: push     eax
0x462071: mov      ecx, 0x4622b0 ; "FontName"
0x462076: mov      edx, 0x4622c4 ; "Setup"
0x46207B: mov      eax, dword ptr [ebp - 0x14]
0x46207E: mov      ebx, dword ptr [eax]
0x462080: call     dword ptr [ebx + 4]
0x462083: mov      eax, dword ptr [ebp - 0x14]
0x462086: call     0x402eac
0x46208B: jmp      0x462187
0x462090: cmp      dword ptr [0x4f7ef8], 0
0x462097: je       0x462263
0x46209D: test     byte ptr [ebp + 8], 2
0x4620A1: je       0x462187
0x4620A7: call     0x406154
0x4620AC: sub      eax, dword ptr [0x4f9448]
0x4620B2: cmp      eax, 0x2710
0x4620B7: jbe      0x4620dd
0x4620B9: call     0x406154
0x4620BE: sub      eax, dword ptr [0x4f945c]
0x4620C4: cmp      eax, 0x2710
0x4620C9: jbe      0x4620dd
0x4620CB: call     0x406154
0x4620D0: sub      eax, dword ptr [0x4f9458]
0x4620D6: cmp      eax, 0x2710
0x4620DB: ja       0x4620e8
0x4620DD: mov      eax, dword ptr [0x4f7ef8]
0x4620E2: cmp      byte ptr [eax + 0x20], 0
0x4620E6: je       0x4620f5
0x4620E8: mov      eax, dword ptr [ebp - 4]
0x4620EB: call     0x461144
0x4620F0: jmp      0x462187
0x4620F5: push     0xff
0x4620FA: mov      ecx, 0xffff
0x4620FF: mov      edx, 0x4622d4 ; "在战斗的时候你不能退出游戏."
0x462104: mov      eax, dword ptr [0x4f7d8c]
0x462109: call     0x479ae0
0x46210E: jmp      0x462187
0x462110: cmp      dword ptr [0x4f7ef8], 0
0x462117: je       0x462263
0x46211D: test     byte ptr [ebp + 8], 2
0x462121: je       0x462187
0x462123: call     0x406154
0x462128: sub      eax, dword ptr [0x4f9448]
0x46212E: cmp      eax, 0x2710
0x462133: jbe      0x462159
0x462135: call     0x406154
0x46213A: sub      eax, dword ptr [0x4f945c]
0x462140: cmp      eax, 0x2710
0x462145: jbe      0x462159
0x462147: call     0x406154
0x46214C: sub      eax, dword ptr [0x4f9458]
0x462152: cmp      eax, 0x2710
0x462157: ja       0x462164
0x462159: mov      eax, dword ptr [0x4f7ef8]
0x46215E: cmp      byte ptr [eax + 0x20], 0
0x462162: je       0x46216e
0x462164: mov      eax, dword ptr [ebp - 4]
0x462167: call     0x461280
0x46216C: jmp      0x462187
0x46216E: push     0xff
0x462173: mov      ecx, 0xffff
0x462178: mov      edx, 0x4622d4 ; "在战斗的时候你不能退出游戏."
0x46217D: mov      eax, dword ptr [0x4f7d8c]
0x462182: call     0x479ae0
0x462187: mov      eax, dword ptr [ebp - 8]
0x46218A: mov      ax, word ptr [eax]
0x46218D: sub      ax, 0x21
0x462191: je       0x4621ec
0x462193: dec      ax
0x462196: je       0x46220e
0x462198: sub      ax, 4
0x46219C: je       0x4621a9
0x46219E: sub      ax, 2
0x4621A2: je       0x4621c5
0x4621A4: jmp      0x462263
0x4621A9: mov      eax, dword ptr [0x4f7d8c]
0x4621AE: cmp      dword ptr [eax + 0x20], 0
0x4621B2: jle      0x462263
0x4621B8: mov      eax, dword ptr [0x4f7d8c]
0x4621BD: dec      dword ptr [eax + 0x20]
0x4621C0: jmp      0x462263
0x4621C5: mov      eax, dword ptr [0x4f7d8c]
0x4621CA: mov      eax, dword ptr [eax + 0x18]
0x4621CD: mov      edx, dword ptr [eax]
0x4621CF: call     dword ptr [edx + 0x14]
0x4621D2: dec      eax
0x4621D3: mov      edx, dword ptr [0x4f7d8c]
0x4621D9: cmp      eax, dword ptr [edx + 0x20]
0x4621DC: jle      0x462263
0x4621E2: mov      eax, dword ptr [0x4f7d8c]
0x4621E7: inc      dword ptr [eax + 0x20]
0x4621EA: jmp      0x462263
0x4621EC: mov      eax, dword ptr [0x4f7d8c]
0x4621F1: cmp      dword ptr [eax + 0x20], 9
0x4621F5: jle      0x462202
0x4621F7: mov      eax, dword ptr [0x4f7d8c]
0x4621FC: sub      dword ptr [eax + 0x20], 9
0x462200: jmp      0x462263
0x462202: mov      eax, dword ptr [0x4f7d8c]
0x462207: xor      edx, edx
0x462209: mov      dword ptr [eax + 0x20], edx
0x46220C: jmp      0x462263
0x46220E: mov      eax, dword ptr [0x4f7d8c]
0x462213: mov      eax, dword ptr [eax + 0x18]
0x462216: mov      edx, dword ptr [eax]
0x462218: call     dword ptr [edx + 0x14]
0x46221B: dec      eax
0x46221C: mov      edx, dword ptr [0x4f7d8c]
0x462222: mov      edx, dword ptr [edx + 0x20]
0x462225: add      edx, 9
0x462228: cmp      eax, edx
0x46222A: jle      0x462237
0x46222C: mov      eax, dword ptr [0x4f7d8c]
0x462231: add      dword ptr [eax + 0x20], 9
0x462235: jmp      0x46224e
0x462237: mov      eax, dword ptr [0x4f7d8c]
0x46223C: mov      eax, dword ptr [eax + 0x18]
0x46223F: mov      edx, dword ptr [eax]
0x462241: call     dword ptr [edx + 0x14]
0x462244: dec      eax
0x462245: mov      edx, dword ptr [0x4f7d8c]
0x46224B: mov      dword ptr [edx + 0x20], eax
0x46224E: mov      eax, dword ptr [0x4f7d8c]
0x462253: cmp      dword ptr [eax + 0x20], 0
0x462257: jge      0x462263
0x462259: mov      eax, dword ptr [0x4f7d8c]
0x46225E: xor      edx, edx
0x462260: mov      dword ptr [eax + 0x20], edx
0x462263: pop      ebx
0x462264: mov      esp, ebp
0x462266: pop      ebp
0x462267: ret      4
0x46226A: add      byte ptr [eax], al
0x462274: [STR len=11] "@AttackMode"
0x462288: [STR len=5] "@Rest"
0x462298: [STR len=14] ".\mirsetup.ini"
0x4622B0: [STR len=8] "FontName"
0x4622C4: [STR len=5] "Setup"
0x4622D4: [STR len=27] "在战斗的时候你不能退出游戏."
0x4622F0: push     ebp
0x4622F1: mov      ebp, esp
0x4622F3: add      esp, -0x14
0x4622F6: push     ebx
0x4622F7: xor      ebx, ebx
0x4622F9: mov      dword ptr [ebp - 0x14], ebx
0x4622FC: mov      dword ptr [ebp - 0x10], ebx
0x4622FF: mov      dword ptr [ebp - 8], ecx
0x462302: mov      dword ptr [ebp - 0xc], edx
0x462305: mov      dword ptr [ebp - 4], eax
0x462308: xor      eax, eax
0x46230A: push     ebp
0x46230B: push     0x46260b
0x462310: push     dword ptr fs:[eax]
0x462313: mov      dword ptr fs:[eax], esp
0x462316: mov      edx, dword ptr [ebp - 8]
0x462319: mov      eax, dword ptr [ebp - 4]
0x46231C: mov      eax, dword ptr [eax + 0x31c]
0x462322: call     0x45855c
0x462327: test     al, al
0x462329: jne      0x4625ed
0x46232F: mov      eax, dword ptr [0x4f7d8c]
0x462334: mov      eax, dword ptr [eax + 0x14]
0x462337: cmp      eax, dword ptr [0x4f7d9c]
0x46233D: jne      0x4625ed
0x462343: mov      eax, dword ptr [0x4f7d9c]
0x462348: mov      eax, dword ptr [eax + 0x5a850]
0x46234E: cmp      byte ptr [eax + 0x47], 0
0x462352: jne      0x4625ed
0x462358: mov      eax, dword ptr [ebp - 8]
0x46235B: movzx    eax, byte ptr [eax]
0x46235E: cmp      eax, 0x2f
0x462361: jg       0x46237f
0x462363: je       0x462471
0x462369: sub      eax, 0xd
0x46236C: je       0x4623ab
0x46236E: sub      eax, 0x13
0x462371: je       0x4623ab
0x462373: dec      eax
0x462374: je       0x462471
0x46237A: jmp      0x4625e7
0x46237F: add      eax, -0x31
0x462382: sub      eax, 6
0x462385: jb       0x462395
0x462387: sub      eax, 9
0x46238A: je       0x462471
0x462390: jmp      0x4625e7
0x462395: mov      edx, dword ptr [ebp - 8]
0x462398: movzx    edx, byte ptr [edx]
0x46239B: sub      edx, 0x31
0x46239E: mov      eax, dword ptr [ebp - 4]
0x4623A1: call     0x462b1c
0x4623A6: jmp      0x4625e7
0x4623AB: mov      eax, dword ptr [0x4f7d9c]
0x4623B0: mov      eax, dword ptr [eax + 0x5a850]
0x4623B6: mov      dl, 1
0x4623B8: call     0x4278c0
0x4623BD: mov      eax, dword ptr [0x4f7d9c]
0x4623C2: mov      eax, dword ptr [eax + 0x5a850]
0x4623C8: mov      edx, dword ptr [eax]
0x4623CA: call     dword ptr [edx + 0xb4]
0x4623D0: mov      eax, dword ptr [0x4f7d9c]
0x4623D5: mov      eax, dword ptr [eax + 0x5a850]
0x4623DB: call     0x42d734
0x4623E0: mov      dl, byte ptr [0x4a2ce0] ; *0x4A2CE0->0x408D0A
0x4623E6: call     0x431bb0
0x4623EB: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x4623F0: mov      eax, dword ptr [eax]
0x4623F2: cmp      byte ptr [eax + 0x9bc], 0
0x4623F9: je       0x46245a
0x4623FB: mov      eax, dword ptr [0x4f7d9c]
0x462400: mov      eax, dword ptr [eax + 0x5a850]
0x462406: mov      edx, 0x462620 ; "!~"
0x46240B: call     0x427a00
0x462410: lea      edx, [ebp - 0x10]
0x462413: mov      eax, dword ptr [0x4f7d9c]
0x462418: mov      eax, dword ptr [eax + 0x5a850]
0x46241E: call     0x4279d0
0x462423: mov      eax, dword ptr [ebp - 0x10]
0x462426: call     0x403cd4
0x46242B: mov      edx, eax
0x46242D: mov      eax, dword ptr [0x4f7d9c]
0x462432: mov      eax, dword ptr [eax + 0x5a850]
0x462438: mov      ecx, dword ptr [eax]
0x46243A: call     dword ptr [ecx + 0xcc]
0x462440: mov      eax, dword ptr [0x4f7d9c]
0x462445: mov      eax, dword ptr [eax + 0x5a850]
0x46244B: xor      edx, edx
0x46244D: mov      ecx, dword ptr [eax]
0x46244F: call     dword ptr [ecx + 0xc8]
0x462455: jmp      0x4625e7
0x46245A: mov      eax, dword ptr [0x4f7d9c]
0x46245F: mov      eax, dword ptr [eax + 0x5a850]
0x462465: xor      edx, edx
0x462467: call     0x427a00
0x46246C: jmp      0x4625e7
0x462471: mov      eax, dword ptr [0x4f7d9c]
0x462476: mov      eax, dword ptr [eax + 0x5a850]
0x46247C: mov      dl, 1
0x46247E: call     0x4278c0
0x462483: mov      eax, dword ptr [0x4f7d9c]
0x462488: mov      eax, dword ptr [eax + 0x5a850]
0x46248E: mov      edx, dword ptr [eax]
0x462490: call     dword ptr [edx + 0xb4]
0x462496: mov      eax, dword ptr [0x4f7d9c]
0x46249B: mov      eax, dword ptr [eax + 0x5a850]
0x4624A1: call     0x42d734
0x4624A6: mov      dl, byte ptr [0x4a2ce0] ; *0x4A2CE0->0x408D0A
0x4624AC: call     0x431bb0
0x4624B1: mov      eax, dword ptr [ebp - 8]
0x4624B4: cmp      byte ptr [eax], 0x2f
0x4624B7: jne      0x46259a
0x4624BD: mov      eax, dword ptr [ebp - 4]
0x4624C0: cmp      dword ptr [eax + 0x40c], 0
0x4624C7: jne      0x4624eb
0x4624C9: lea      eax, [ebp - 0x14]
0x4624CC: mov      edx, dword ptr [ebp - 8]
0x4624CF: mov      dl, byte ptr [edx]
0x4624D1: call     0x403bfc
0x4624D6: mov      edx, dword ptr [ebp - 0x14]
0x4624D9: mov      eax, dword ptr [0x4f7d9c]
0x4624DE: mov      eax, dword ptr [eax + 0x5a850]
0x4624E4: call     0x427a00
0x4624E9: jmp      0x462553
0x4624EB: mov      eax, dword ptr [ebp - 4]
0x4624EE: mov      eax, dword ptr [eax + 0x40c]
0x4624F4: call     0x403cd4
0x4624F9: cmp      eax, 2
0x4624FC: jle      0x462533
0x4624FE: push     0x46262c ; "/"
0x462503: mov      eax, dword ptr [ebp - 4]
0x462506: push     dword ptr [eax + 0x40c]
0x46250C: push     0x462638 ; " "
0x462511: lea      eax, [ebp - 0x14]
0x462514: mov      edx, 3
0x462519: call     0x403d94
0x46251E: mov      edx, dword ptr [ebp - 0x14]
0x462521: mov      eax, dword ptr [0x4f7d9c]
0x462526: mov      eax, dword ptr [eax + 0x5a850]
0x46252C: call     0x427a00
0x462531: jmp      0x462553
0x462533: lea      eax, [ebp - 0x14]
0x462536: mov      edx, dword ptr [ebp - 8]
0x462539: mov      dl, byte ptr [edx]
0x46253B: call     0x403bfc
0x462540: mov      edx, dword ptr [ebp - 0x14]
0x462543: mov      eax, dword ptr [0x4f7d9c]
0x462548: mov      eax, dword ptr [eax + 0x5a850]
0x46254E: call     0x427a00
0x462553: lea      edx, [ebp - 0x10]
0x462556: mov      eax, dword ptr [0x4f7d9c]
0x46255B: mov      eax, dword ptr [eax + 0x5a850]
0x462561: call     0x4279d0
0x462566: mov      eax, dword ptr [ebp - 0x10]
0x462569: call     0x403cd4
0x46256E: mov      edx, eax
0x462570: mov      eax, dword ptr [0x4f7d9c]
0x462575: mov      eax, dword ptr [eax + 0x5a850]
0x46257B: mov      ecx, dword ptr [eax]
0x46257D: call     dword ptr [ecx + 0xcc]
0x462583: mov      eax, dword ptr [0x4f7d9c]
0x462588: mov      eax, dword ptr [eax + 0x5a850]
0x46258E: xor      edx, edx
0x462590: mov      ecx, dword ptr [eax]
0x462592: call     dword ptr [ecx + 0xc8]
0x462598: jmp      0x4625e7
0x46259A: lea      eax, [ebp - 0x14]
0x46259D: mov      edx, dword ptr [ebp - 8]
0x4625A0: mov      dl, byte ptr [edx]
0x4625A2: call     0x403bfc
0x4625A7: mov      edx, dword ptr [ebp - 0x14]
0x4625AA: mov      eax, dword ptr [0x4f7d9c]
0x4625AF: mov      eax, dword ptr [eax + 0x5a850]
0x4625B5: call     0x427a00
0x4625BA: mov      eax, dword ptr [0x4f7d9c]
0x4625BF: mov      eax, dword ptr [eax + 0x5a850]
0x4625C5: mov      edx, 1
0x4625CA: mov      ecx, dword ptr [eax]
0x4625CC: call     dword ptr [ecx + 0xcc]
0x4625D2: mov      eax, dword ptr [0x4f7d9c]
0x4625D7: mov      eax, dword ptr [eax + 0x5a850]
0x4625DD: xor      edx, edx
0x4625DF: mov      ecx, dword ptr [eax]
0x4625E1: call     dword ptr [ecx + 0xc8]
0x4625E7: mov      eax, dword ptr [ebp - 8]
0x4625EA: mov      byte ptr [eax], 0
0x4625ED: xor      eax, eax
0x4625EF: pop      edx
0x4625F0: pop      ecx
0x4625F1: pop      ecx
0x4625F2: mov      dword ptr fs:[eax], edx
0x4625F5: push     0x462612
0x4625FA: lea      eax, [ebp - 0x14]
0x4625FD: call     0x403a58
0x462602: lea      eax, [ebp - 0x10]
0x462605: call     0x403a58
0x46260A: ret      
0x46260B: jmp      0x403518
0x462610: jmp      0x4625fa
0x462612: pop      ebx
0x462613: mov      esp, ebp
0x462615: pop      ebp
0x462616: ret      
0x462617: add      bh, bh
0x46261B: inc      dword ptr [edx]
0x46261D: add      byte ptr [eax], al
0x46261F: add      byte ptr [ecx], ah
0x462621: jle      0x462623
0x462623: add      bh, bh
0x462627: inc      dword ptr [ecx]
0x462629: add      byte ptr [eax], al
0x46262B: add      byte ptr [edi], ch
0x46262D: add      byte ptr [eax], al
0x46262F: add      bh, bh
0x462633: inc      dword ptr [ecx]
0x462635: add      byte ptr [eax], al
0x462637: add      byte ptr [eax], ah
0x462639: add      byte ptr [eax], al
0x46263B: add      byte ptr [ebp - 0x75], dl
0x46263E: in       al, dx
0x46263F: add      esp, -0x18
0x462642: mov      byte ptr [ebp - 5], dl
0x462645: mov      dword ptr [ebp - 4], eax
0x462648: xor      eax, eax
0x46264A: mov      dword ptr [ebp - 0xc], eax
0x46264D: mov      eax, dword ptr [0x4f9190]
0x462652: mov      eax, dword ptr [eax + 8]
0x462655: dec      eax
0x462656: test     eax, eax
0x462658: jl       0x46268f
0x46265A: inc      eax
0x46265B: mov      dword ptr [ebp - 0x18], eax
0x46265E: mov      dword ptr [ebp - 0x10], 0
0x462665: mov      edx, dword ptr [ebp - 0x10]
0x462668: mov      eax, dword ptr [0x4f9190]
0x46266D: call     0x40f6a0
0x462672: mov      dword ptr [ebp - 0x14], eax
0x462675: mov      eax, dword ptr [ebp - 0x14]
0x462678: mov      al, byte ptr [eax]
0x46267A: cmp      al, byte ptr [ebp - 5]
0x46267D: jne      0x462687
0x46267F: mov      eax, dword ptr [ebp - 0x14]
0x462682: mov      dword ptr [ebp - 0xc], eax
0x462685: jmp      0x46268f
0x462687: inc      dword ptr [ebp - 0x10]
0x46268A: dec      dword ptr [ebp - 0x18]
0x46268D: jne      0x462665
0x46268F: mov      eax, dword ptr [ebp - 0xc]
0x462692: mov      esp, ebp
0x462694: pop      ebp
0x462695: ret      
0x462696: mov      eax, eax
0x462698: push     ebp
0x462699: mov      ebp, esp
0x46269B: add      esp, -0x20
0x46269E: mov      dword ptr [ebp - 0xc], ecx
0x4626A1: mov      dword ptr [ebp - 8], edx
0x4626A4: mov      dword ptr [ebp - 4], eax
0x4626A7: cmp      dword ptr [ebp + 8], 0
0x4626AB: je       0x462929
0x4626B1: mov      eax, dword ptr [ebp + 8]
0x4626B4: movzx    eax, word ptr [eax + 0x1a]
0x4626B8: mov      edx, dword ptr [ebp + 8]
0x4626BB: movzx    edx, byte ptr [edx + 0x3c]
0x4626BF: add      eax, edx
0x4626C1: mov      edx, dword ptr [0x4f7ef8]
0x4626C7: movzx    edx, word ptr [edx + 0x3e]
0x4626CB: cmp      eax, edx
0x4626CD: jbe      0x4626dc
0x4626CF: mov      eax, dword ptr [ebp + 8]
0x4626D2: cmp      byte ptr [eax + 0x17], 0
0x4626D6: jne      0x46291a
0x4626DC: mov      eax, dword ptr [ebp + 8]
0x4626DF: cmp      byte ptr [eax + 0x17], 0
0x4626E3: jne      0x462778
0x4626E9: mov      eax, dword ptr [ebp + 8]
0x4626EC: cmp      word ptr [eax + 8], 0x1a
0x4626F1: jne      0x462709
0x4626F3: call     0x406154
0x4626F8: sub      eax, dword ptr [0x4f9450]
0x4626FE: cmp      eax, 0x2710
0x462703: jb       0x462929
0x462709: mov      eax, dword ptr [ebp + 8]
0x46270C: cmp      word ptr [eax + 8], 0x1b
0x462711: jne      0x462729
0x462713: call     0x406154
0x462718: sub      eax, dword ptr [0x4f9454]
0x46271E: cmp      eax, 0xbb8
0x462723: jb       0x462929
0x462729: call     0x406154
0x46272E: sub      eax, dword ptr [0x4f944c]
0x462734: cmp      eax, 0x1f4
0x462739: jbe      0x462929
0x46273F: call     0x406154
0x462744: mov      dword ptr [0x4f944c], eax
0x462749: xor      eax, eax
0x46274B: mov      dword ptr [0x4f94b8], eax
0x462750: push     0
0x462752: mov      eax, dword ptr [ebp + 8]
0x462755: movzx    eax, word ptr [eax + 8]
0x462759: push     eax
0x46275A: push     0
0x46275C: mov      eax, dword ptr [0x4f7ef8]
0x462761: xor      ecx, ecx
0x462763: mov      cl, byte ptr [eax + 0xc]
0x462766: mov      edx, 0xbc9
0x46276B: mov      eax, dword ptr [ebp - 4]
0x46276E: call     0x465784
0x462773: jmp      0x462929
0x462778: mov      eax, dword ptr [ebp - 0xc]
0x46277B: push     eax
0x46277C: mov      ecx, dword ptr [ebp - 8]
0x46277F: mov      edx, 0xaf
0x462784: mov      eax, 0x186
0x462789: call     0x4a0404
0x46278E: mov      dword ptr [ebp - 0x10], eax
0x462791: mov      eax, dword ptr [0x4f94a8]
0x462796: mov      dword ptr [0x4f94ac], eax
0x46279B: mov      edx, dword ptr [0x4f94ac]
0x4627A1: mov      eax, dword ptr [0x4f7d9c]
0x4627A6: call     0x472ec0
0x4627AB: test     al, al
0x4627AD: jne      0x4627b6
0x4627AF: xor      eax, eax
0x4627B1: mov      dword ptr [0x4f94ac], eax
0x4627B6: cmp      dword ptr [0x4f94ac], 0
0x4627BD: jne      0x4627de
0x4627BF: lea      eax, [ebp - 0x14]
0x4627C2: push     eax
0x4627C3: lea      eax, [ebp - 0x18]
0x4627C6: push     eax
0x4627C7: mov      ecx, dword ptr [ebp - 0xc]
0x4627CA: mov      edx, dword ptr [ebp - 8]
0x4627CD: mov      eax, dword ptr [0x4f7d9c]
0x4627D2: call     0x472518
0x4627D7: xor      eax, eax
0x4627D9: mov      dword ptr [ebp - 0x1c], eax
0x4627DC: jmp      0x462801
0x4627DE: mov      eax, dword ptr [0x4f94ac]
0x4627E3: movzx    eax, word ptr [eax + 8]
0x4627E7: mov      dword ptr [ebp - 0x14], eax
0x4627EA: mov      eax, dword ptr [0x4f94ac]
0x4627EF: movzx    eax, word ptr [eax + 0xa]
0x4627F3: mov      dword ptr [ebp - 0x18], eax
0x4627F6: mov      eax, dword ptr [0x4f94ac]
0x4627FB: mov      eax, dword ptr [eax + 4]
0x4627FE: mov      dword ptr [ebp - 0x1c], eax
0x462801: mov      eax, dword ptr [ebp - 4]
0x462804: call     0x466f60
0x462809: test     al, al
0x46280B: je       0x462929
0x462811: mov      eax, dword ptr [ebp - 4]
0x462814: call     0x466f1c
0x462819: test     al, al
0x46281B: je       0x462929
0x462821: call     0x406154
0x462826: mov      dword ptr [0x4f944c], eax
0x46282B: mov      eax, 0x24
0x462830: call     0x4026b4
0x462835: mov      dword ptr [ebp - 0x20], eax
0x462838: mov      eax, dword ptr [ebp - 0x20]
0x46283B: xor      ecx, ecx
0x46283D: mov      edx, 0x24
0x462842: call     0x402b54
0x462847: mov      eax, dword ptr [ebp + 8]
0x46284A: movzx    eax, byte ptr [eax + 0x18]
0x46284E: mov      edx, dword ptr [ebp - 0x20]
0x462851: mov      dword ptr [edx + 0x10], eax
0x462854: mov      eax, dword ptr [ebp + 8]
0x462857: movzx    eax, word ptr [eax + 8]
0x46285B: mov      edx, dword ptr [ebp - 0x20]
0x46285E: mov      dword ptr [edx + 4], eax
0x462861: mov      eax, dword ptr [ebp - 0x20]
0x462864: xor      edx, edx
0x462866: mov      dword ptr [eax], edx
0x462868: mov      eax, dword ptr [ebp + 8]
0x46286B: mov      eax, dword ptr [eax + 0x38]
0x46286E: add      eax, 0xc8
0x462873: mov      dword ptr [0x4f94b8], eax
0x462878: mov      eax, dword ptr [ebp - 0x20]
0x46287B: mov      eax, dword ptr [eax + 4]
0x46287E: cmp      eax, 0x1f
0x462881: ja       0x4628ba
0x462883: mov      al, byte ptr [eax + 0x462890]
0x462889: jmp      dword ptr [eax*4 + 0x4628b0]
0x462890: add      byte ptr [eax], al
0x462892: add      dword ptr [eax], eax
0x462894: add      byte ptr [eax], al
0x462896: add      byte ptr [eax], al
0x462898: add      byte ptr [eax], al
0x46289A: add      byte ptr [eax], al
0x46289C: add      dword ptr [eax], eax
0x46289E: add      dword ptr [ecx], eax
0x4628A0: add      dword ptr [ecx], eax
0x4628A2: add      dword ptr [ecx], eax
0x4628A4: add      byte ptr [ecx], al
0x4628A6: add      byte ptr [eax], al
0x4628A8: add      byte ptr [ecx], al
0x4628AA: add      dword ptr [eax], eax
0x4628AC: add      dword ptr [ecx], eax
0x4628AE: add      dword ptr [ecx], eax
0x4628B0: mov      edx, 0xc4004628
0x4628B5: sub      byte ptr [esi], al
0x4628B8: jmp      0x4628c4
0x4628BA: call     0x406154
0x4628BF: mov      dword ptr [0x4f945c], eax
0x4628C4: xor      eax, eax
0x4628C6: mov      dword ptr [0x4f94bc], eax
0x4628CB: cmp      dword ptr [0x4f94ac], 0
0x4628D2: je       0x4628f3
0x4628D4: mov      eax, dword ptr [0x4f94ac]
0x4628D9: cmp      byte ptr [eax + 0xe], 0
0x4628DD: jne      0x4628f3
0x4628DF: mov      eax, 0x44c
0x4628E4: call     0x402b74
0x4628E9: add      eax, 0x12c
0x4628EE: mov      dword ptr [0x4f94bc], eax
0x4628F3: mov      eax, dword ptr [ebp - 0x18]
0x4628F6: push     eax
0x4628F7: mov      eax, dword ptr [ebp - 0x10]
0x4628FA: push     eax
0x4628FB: mov      eax, dword ptr [ebp - 0x20]
0x4628FE: push     eax
0x4628FF: mov      eax, dword ptr [ebp - 0x1c]
0x462902: push     eax
0x462903: push     0
0x462905: push     0
0x462907: mov      ecx, dword ptr [ebp - 0x14]
0x46290A: mov      dx, 0xbc9
0x46290E: mov      eax, dword ptr [0x4f7ef8]
0x462913: call     0x496548
0x462918: jmp      0x462929
0x46291A: mov      edx, 0x462938 ; "没有足够的魔法点数."
0x46291F: mov      eax, dword ptr [0x4f7d8c]
0x462924: call     0x479a68
0x462929: mov      esp, ebp
0x46292B: pop      ebp
0x46292C: ret      4
0x46292F: add      bh, bh
0x462933: call     dword ptr [ebx]
0x462935: add      byte ptr [eax], al
0x462937: add      bl, al
0x462939: mov      ebx, 0xe3d7d0d3
0x46293E: mov      ecx, 0xc4c4b5bb
0x462943: cmpsd    dword ptr [esi], dword ptr es:[edi]
0x462944: mov      bh, 0xa8
0x462946: mov      ch, 0xe3
0x462948: retf     0x2efd
0x46294B: add      byte ptr [ebp - 0x75], dl
0x46294E: in       al, dx
0x46294F: add      esp, -0x18
0x462952: mov      dword ptr [ebp - 0xc], ecx
0x462955: mov      dword ptr [ebp - 8], edx
0x462958: mov      dword ptr [ebp - 4], eax
0x46295B: mov      edx, dword ptr [ebp - 8]
0x46295E: mov      eax, dword ptr [0x4f7d9c]
0x462963: call     0x472db0
0x462968: mov      dword ptr [ebp - 0x10], eax
0x46296B: cmp      dword ptr [ebp - 0x10], 0
0x46296F: je       0x4629e9
0x462971: mov      eax, dword ptr [ebp + 0xc]
0x462974: push     eax
0x462975: mov      eax, dword ptr [ebp - 0x10]
0x462978: movzx    edx, word ptr [eax + 0xa]
0x46297C: mov      eax, dword ptr [ebp - 0x10]
0x46297F: movzx    eax, word ptr [eax + 8]
0x462983: mov      ecx, dword ptr [ebp + 0x10]
0x462986: call     0x4a0404
0x46298B: mov      dword ptr [ebp - 0x14], eax
0x46298E: mov      eax, 0x24
0x462993: call     0x4026b4
0x462998: mov      dword ptr [ebp - 0x18], eax
0x46299B: mov      eax, dword ptr [ebp - 0x18]
0x46299E: xor      ecx, ecx
0x4629A0: mov      edx, 0x24
0x4629A5: call     0x402b54
0x4629AA: mov      eax, dword ptr [ebp - 0xc]
0x4629AD: mov      edx, dword ptr [ebp - 0x18]
0x4629B0: mov      dword ptr [edx + 0x10], eax
0x4629B3: mov      eax, dword ptr [ebp - 0x18]
0x4629B6: xor      edx, edx
0x4629B8: mov      dword ptr [eax], edx
0x4629BA: mov      eax, dword ptr [ebp + 8]
0x4629BD: mov      edx, dword ptr [ebp - 0x18]
0x4629C0: mov      dword ptr [edx + 4], eax
0x4629C3: push     0
0x4629C5: mov      eax, dword ptr [ebp - 0x14]
0x4629C8: push     eax
0x4629C9: mov      eax, dword ptr [ebp - 0x18]
0x4629CC: push     eax
0x4629CD: push     0
0x4629CF: push     0
0x4629D1: push     0
0x4629D3: xor      ecx, ecx
0x4629D5: mov      dx, 0x11
0x4629D9: mov      eax, dword ptr [ebp - 0x10]
0x4629DC: call     0x496548
0x4629E1: inc      dword ptr [0x4f9538]
0x4629E7: jmp      0x4629ef
0x4629E9: inc      dword ptr [0x4f953c]
0x4629EF: mov      esp, ebp
0x4629F1: pop      ebp
0x4629F2: ret      0xc
0x4629F5: lea      eax, [eax]
0x4629F8: push     ebp
0x4629F9: mov      ebp, esp
0x4629FB: add      esp, -0x1c
0x4629FE: mov      dword ptr [ebp - 0xc], ecx
0x462A01: mov      dword ptr [ebp - 8], edx
0x462A04: mov      dword ptr [ebp - 4], eax
0x462A07: mov      edx, dword ptr [ebp - 8]
0x462A0A: mov      eax, dword ptr [0x4f7d9c]
0x462A0F: call     0x472db0
0x462A14: mov      dword ptr [ebp - 0x10], eax
0x462A17: cmp      dword ptr [ebp - 0x10], 0
0x462A1B: je       0x462a55
0x462A1D: mov      eax, dword ptr [ebp - 0xc]
0x462A20: push     eax
0x462A21: mov      eax, dword ptr [ebp + 0x14]
0x462A24: push     eax
0x462A25: mov      eax, dword ptr [ebp + 0x10]
0x462A28: push     eax
0x462A29: mov      eax, dword ptr [ebp + 0xc]
0x462A2C: push     eax
0x462A2D: push     0
0x462A2F: mov      eax, dword ptr [ebp - 0x14]
0x462A32: push     eax
0x462A33: mov      ecx, dword ptr [ebp + 8]
0x462A36: mov      dx, 0x27e
0x462A3A: mov      eax, dword ptr [ebp - 0x10]
0x462A3D: call     0x496548
0x462A42: mov      eax, dword ptr [0x4f9540]
0x462A47: cmp      eax, dword ptr [0x4f9538]
0x462A4D: jge      0x462a55
0x462A4F: inc      dword ptr [0x4f9540]
0x462A55: xor      eax, eax
0x462A57: mov      dword ptr [0x4f94ac], eax
0x462A5C: mov      esp, ebp
0x462A5E: pop      ebp
0x462A5F: ret      0x10
0x462A62: mov      eax, eax
0x462A64: push     ebp
0x462A65: mov      ebp, esp
0x462A67: add      esp, -0xc
0x462A6A: mov      dword ptr [ebp - 8], edx
0x462A6D: mov      dword ptr [ebp - 4], eax
0x462A70: mov      edx, dword ptr [ebp - 8]
0x462A73: mov      eax, dword ptr [0x4f7d9c]
0x462A78: call     0x472db0
0x462A7D: mov      dword ptr [ebp - 0xc], eax
0x462A80: cmp      dword ptr [ebp - 0xc], 0
0x462A84: je       0x462aa0
0x462A86: push     0
0x462A88: push     0
0x462A8A: push     0
0x462A8C: push     0
0x462A8E: push     0
0x462A90: push     0
0x462A92: xor      ecx, ecx
0x462A94: mov      dx, 0x27f
0x462A98: mov      eax, dword ptr [ebp - 0xc]
0x462A9B: call     0x496548
0x462AA0: xor      eax, eax
0x462AA2: mov      dword ptr [0x4f94ac], eax
0x462AA7: mov      esp, ebp
0x462AA9: pop      ebp
0x462AAA: ret      
0x462AAB: nop      
0x462AAC: push     ebp
0x462AAD: mov      ebp, esp
0x462AAF: add      esp, -0x10
0x462AB2: mov      dword ptr [ebp - 0xc], ecx
0x462AB5: mov      dword ptr [ebp - 8], edx
0x462AB8: mov      dword ptr [ebp - 4], eax
0x462ABB: xor      eax, eax
0x462ABD: mov      dword ptr [ebp - 0x10], eax
0x462AC0: mov      eax, dword ptr [ebp - 8]
0x462AC3: dec      eax
0x462AC4: jne      0x462af1
0x462AC6: mov      eax, dword ptr [ebp + 8]
0x462AC9: push     eax
0x462ACA: mov      eax, dword ptr [ebp - 4]
0x462ACD: mov      eax, dword ptr [eax + 0x3a8]
0x462AD3: push     eax
0x462AD4: push     0x19a
0x462AD9: push     6
0x462ADB: push     0x78
0x462ADD: push     0
0x462ADF: mov      ecx, dword ptr [ebp - 0xc]
0x462AE2: mov      dl, 1
0x462AE4: mov      eax, dword ptr [0x49c550]
0x462AE9: call     0x49e834
0x462AEE: mov      dword ptr [ebp - 0x10], eax
0x462AF1: cmp      dword ptr [ebp - 0x10], 0
0x462AF5: je       0x462b15
0x462AF7: mov      eax, dword ptr [0x4f7ef8]
0x462AFC: mov      edx, dword ptr [ebp - 0x10]
0x462AFF: mov      dword ptr [edx + 0xc], eax
0x462B02: mov      eax, dword ptr [0x4f7d9c]
0x462B07: mov      eax, dword ptr [eax + 0x5a860]
0x462B0D: mov      edx, dword ptr [ebp - 0x10]
0x462B10: call     0x40f588
0x462B15: mov      esp, ebp
0x462B17: pop      ebp
0x462B18: ret      4
0x462B1B: nop      
0x462B1C: push     ebp
0x462B1D: mov      ebp, esp
0x462B1F: add      esp, 0xffffff5c
0x462B25: push     esi
0x462B26: push     edi
0x462B27: xor      ecx, ecx
0x462B29: mov      dword ptr [ebp - 0xc], ecx
0x462B2C: mov      dword ptr [ebp - 8], edx
0x462B2F: mov      dword ptr [ebp - 4], eax
0x462B32: xor      eax, eax
0x462B34: push     ebp
0x462B35: push     0x462e91
0x462B3A: push     dword ptr fs:[eax]
0x462B3D: mov      dword ptr fs:[eax], esp
0x462B40: mov      eax, dword ptr [ebp - 8]
0x462B43: sub      eax, 0x34
0x462B46: jae      0x462d09
0x462B4C: cmp      byte ptr [0x4f9410], 0
0x462B53: je       0x462b6e
0x462B55: call     0x406154
0x462B5A: sub      eax, dword ptr [0x4f9444]
0x462B60: cmp      eax, 0x1388
0x462B65: jbe      0x462b6e
0x462B67: mov      byte ptr [0x4f9410], 0
0x462B6E: cmp      byte ptr [0x4f9410], 0
0x462B75: jne      0x462e7b
0x462B7B: imul     eax, dword ptr [ebp - 8], 0xd
0x462B7F: cmp      byte ptr [eax*4 + 0x4f80d0], 0
0x462B87: je       0x462e7b
0x462B8D: imul     eax, dword ptr [ebp - 8], 0xd
0x462B91: cmp      byte ptr [eax*4 + 0x4f80df], 3
0x462B99: ja       0x462e7b
0x462B9F: imul     eax, dword ptr [ebp - 8], 0xd
0x462BA3: lea      esi, [eax*4 + 0x4f80d0]
0x462BAA: mov      edi, 0x4f9410
0x462BAF: mov      ecx, 0xd
0x462BB4: rep movsd dword ptr es:[edi], dword ptr [esi]
0x462BB6: imul     eax, dword ptr [ebp - 8], 0xd
0x462BBA: mov      byte ptr [eax*4 + 0x4f80d0], 0
0x462BC2: imul     eax, dword ptr [ebp - 8], 0xd
0x462BC6: cmp      byte ptr [eax*4 + 0x4f80df], 4
0x462BCE: jne      0x462cc0
0x462BD4: imul     eax, dword ptr [ebp - 8], 0xd
0x462BD8: cmp      byte ptr [eax*4 + 0x4f80e0], 0x64
0x462BE0: jae      0x462cc0
0x462BE6: imul     eax, dword ptr [ebp - 8], 0xd
0x462BEA: cmp      byte ptr [eax*4 + 0x4f80e0], 0x32
0x462BF2: jae      0x462c5c
0x462BF4: imul     eax, dword ptr [ebp - 8], 0xd
0x462BF8: lea      edx, [eax*4 + 0x4f80d0]
0x462BFF: lea      eax, [ebp - 0x30]
0x462C02: call     0x402a14
0x462C07: mov      edx, 0x462ea0
0x462C0C: lea      eax, [ebp - 0x30]
0x462C0F: mov      cl, 0x21
0x462C11: call     0x4029e4
0x462C16: lea      edx, [ebp - 0x30]
0x462C19: lea      eax, [ebp - 0xc]
0x462C1C: call     0x403c78
0x462C21: mov      edx, dword ptr [ebp - 0xc]
0x462C24: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x462C29: mov      eax, dword ptr [eax]
0x462C2B: mov      cx, word ptr [0x462eb4]
0x462C32: call     0x486df0
0x462C37: cmp      eax, 6
0x462C3A: je       0x462cc0
0x462C40: imul     eax, dword ptr [ebp - 8], 0xd
0x462C44: lea      edi, [eax*4 + 0x4f80d0]
0x462C4B: mov      esi, 0x4f9410
0x462C50: mov      ecx, 0xd
0x462C55: rep movsd dword ptr es:[edi], dword ptr [esi]
0x462C57: jmp      0x462e7b
0x462C5C: imul     eax, dword ptr [ebp - 8], 0xd
0x462C60: lea      edx, [eax*4 + 0x4f80d0]
0x462C67: lea      eax, [ebp - 0x4c]
0x462C6A: call     0x402a14
0x462C6F: mov      edx, 0x462eb8
0x462C74: lea      eax, [ebp - 0x4c]
0x462C77: mov      cl, 0x1a
0x462C79: call     0x4029e4
0x462C7E: lea      edx, [ebp - 0x4c]
0x462C81: lea      eax, [ebp - 0xc]
0x462C84: call     0x403c78
0x462C89: mov      edx, dword ptr [ebp - 0xc]
0x462C8C: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x462C91: mov      eax, dword ptr [eax]
0x462C93: mov      cx, word ptr [0x462eb4]
0x462C9A: call     0x486df0
0x462C9F: cmp      eax, 6
0x462CA2: je       0x462cc0
0x462CA4: imul     eax, dword ptr [ebp - 8], 0xd
0x462CA8: lea      edi, [eax*4 + 0x4f80d0]
0x462CAF: mov      esi, 0x4f9410
0x462CB4: mov      ecx, 0xd
0x462CB9: rep movsd dword ptr es:[edi], dword ptr [esi]
0x462CBB: jmp      0x462e7b
0x462CC0: call     0x406154
0x462CC5: mov      dword ptr [0x4f9444], eax
0x462CCA: lea      eax, [ebp - 0xc]
0x462CCD: imul     edx, dword ptr [ebp - 8], 0xd
0x462CD1: lea      edx, [edx*4 + 0x4f80d0]
0x462CD8: call     0x403c78
0x462CDD: mov      ecx, dword ptr [ebp - 0xc]
0x462CE0: imul     eax, dword ptr [ebp - 8], 0xd
0x462CE4: mov      edx, dword ptr [eax*4 + 0x4f80fc]
0x462CEB: mov      eax, dword ptr [ebp - 4]
0x462CEE: call     0x465b14
0x462CF3: imul     eax, dword ptr [ebp - 8], 0xd
0x462CF7: movzx    eax, byte ptr [eax*4 + 0x4f80df]
0x462CFF: call     0x495a50
0x462D04: jmp      0x462e7b
0x462D09: cmp      dword ptr [ebp - 8], -1
0x462D0D: sete     al
0x462D10: and      al, byte ptr [0x4f939c]
0x462D16: je       0x462e7b
0x462D1C: mov      byte ptr [0x4f939c], 0
0x462D23: mov      esi, 0x4f93a4
0x462D28: mov      edi, 0x4f9410
0x462D2D: mov      ecx, 0xd
0x462D32: rep movsd dword ptr es:[edi], dword ptr [esi]
0x462D34: mov      byte ptr [0x4f93a4], 0
0x462D3B: cmp      byte ptr [0x4f941f], 4
0x462D42: jne      0x462e47
0x462D48: cmp      byte ptr [0x4f9420], 0x64
0x462D4F: jae      0x462e47
0x462D55: cmp      byte ptr [0x4f9420], 0x32
0x462D5C: jae      0x462dd6
0x462D5E: mov      edx, 0x462ec8 ; """
0x462D63: lea      eax, [ebp - 0x5c]
0x462D66: call     0x402a14
0x462D6B: mov      edx, 0x4f9410
0x462D70: lea      eax, [ebp - 0x5c]
0x462D73: mov      cl, 0xf
0x462D75: call     0x4029e4
0x462D7A: lea      edx, [ebp - 0x5c]
0x462D7D: lea      eax, [ebp - 0x84]
0x462D83: call     0x402a14
0x462D88: mov      edx, 0x462ecc
0x462D8D: lea      eax, [ebp - 0x84]
0x462D93: mov      cl, 0x25
0x462D95: call     0x4029e4
0x462D9A: lea      edx, [ebp - 0x84]
0x462DA0: lea      eax, [ebp - 0xc]
0x462DA3: call     0x403c78
0x462DA8: mov      edx, dword ptr [ebp - 0xc]
0x462DAB: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x462DB0: mov      eax, dword ptr [eax]
0x462DB2: mov      cx, word ptr [0x462eb4]
0x462DB9: call     0x486df0
0x462DBE: cmp      eax, 6
0x462DC1: je       0x462e47
0x462DC7: mov      eax, 0x4f9410
0x462DCC: call     0x49f874
0x462DD1: jmp      0x462e7b
0x462DD6: mov      edx, 0x462ec8 ; """
0x462DDB: lea      eax, [ebp - 0x5c]
0x462DDE: call     0x402a14
0x462DE3: mov      edx, 0x4f9410
0x462DE8: lea      eax, [ebp - 0x5c]
0x462DEB: mov      cl, 0xf
0x462DED: call     0x4029e4
0x462DF2: lea      edx, [ebp - 0x5c]
0x462DF5: lea      eax, [ebp - 0xa4]
0x462DFB: call     0x402a14
0x462E00: mov      edx, 0x462ee4
0x462E05: lea      eax, [ebp - 0xa4]
0x462E0B: mov      cl, 0x1f
0x462E0D: call     0x4029e4
0x462E12: lea      edx, [ebp - 0xa4]
0x462E18: lea      eax, [ebp - 0xc]
0x462E1B: call     0x403c78
0x462E20: mov      edx, dword ptr [ebp - 0xc]
0x462E23: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x462E28: mov      eax, dword ptr [eax]
0x462E2A: mov      cx, word ptr [0x462eb4]
0x462E31: call     0x486df0
0x462E36: cmp      eax, 6
0x462E39: je       0x462e47
0x462E3B: mov      eax, 0x4f9410
0x462E40: call     0x49f874
0x462E45: jmp      0x462e7b
0x462E47: call     0x406154
0x462E4C: mov      dword ptr [0x4f9444], eax
0x462E51: lea      eax, [ebp - 0xc]
0x462E54: mov      edx, 0x4f9410
0x462E59: call     0x403c78
0x462E5E: mov      ecx, dword ptr [ebp - 0xc]
0x462E61: mov      edx, dword ptr [0x4f943c]
0x462E67: mov      eax, dword ptr [ebp - 4]
0x462E6A: call     0x465b14
0x462E6F: xor      eax, eax
0x462E71: mov      al, byte ptr [0x4f941f]
0x462E76: call     0x495a50
0x462E7B: xor      eax, eax
0x462E7D: pop      edx
0x462E7E: pop      ecx
0x462E7F: pop      ecx
0x462E80: mov      dword ptr fs:[eax], edx
0x462E83: push     0x462e98
0x462E88: lea      eax, [ebp - 0xc]
0x462E8B: call     0x403a58
0x462E90: ret      
0x462E91: jmp      0x403518
0x462E96: jmp      0x462e88
0x462E98: pop      edi
0x462E99: pop      esi
0x462E9A: mov      esp, ebp
0x462E9C: pop      ebp
0x462E9D: ret      
0x462E9E: add      byte ptr [eax], al
0x462EA0: adc      esp, dword ptr [eax]
0x462EA2: cmp      ah, byte ptr [eax]
0x462EA5: jecxz    0x462e79
0x462EA7: stosb    byte ptr es:[edi], al
0x462EA8: mov      edi, 0xd1bccaaa
0x462EAD: cmpsd    dword ptr [esi], dword ptr es:[edi]
0x462EAE: iretd    
0x462EAF: mov      al, 0xc2
0x462EB2: mov      dword ptr [0x3bf], eax
0x462EB7: add      byte ptr [esp + eax*8], cl
0x462EBA: jecxz    0x462e8e
0x462EBC: stosb    byte ptr es:[edi], al
0x462EBD: retf     0xd3b9
0x462EC0: ret      
0x462EC1: ret      0xa3f0
0x462EC4: mov      edi, 0x1000000
0x462EC9: and      al, byte ptr [eax]
0x462ECB: add      byte ptr [esi], dl
0x462ECD: and      ah, byte ptr [eax]
0x462ECF: cmp      ah, byte ptr [eax]
0x462ED2: jecxz    0x462ea3
0x462ED4: jmp      0x462ea8
0x462ED6: stosb    byte ptr es:[edi], al
0x462ED7: mov      edi, 0xd1bccaaa
0x462EDC: mov      ch, 0xc1
0x462EDE: mov      bh, 0xc2
0x462EE1: mov      dword ptr [0x221000bf], eax
0x462EE7: jecxz    0x462eb1
0x462EE9: mov      bh, 0xb6
0x462EEB: test     al, 0xd2
0x462EED: stosb    byte ptr es:[edi], al
0x462EEE: retf     0xd3b9
0x462EF1: ret      
0x462EF2: ret      0x3ff0
0x462EF5: add      byte ptr [eax], al
0x462EF7: add      byte ptr [ebp - 0x75], dl
0x462EFA: in       al, dx
0x462EFB: add      esp, -0x18
0x462EFE: mov      dword ptr [ebp - 8], edx
0x462F01: mov      dword ptr [ebp - 4], eax
0x462F04: mov      byte ptr [ebp - 9], 0
0x462F08: lea      eax, [ebp - 0x10]
0x462F0B: push     eax
0x462F0C: lea      eax, [ebp - 0x14]
0x462F0F: push     eax
0x462F10: mov      eax, dword ptr [0x4f7ef8]
0x462F15: movzx    edx, word ptr [eax + 0xa]
0x462F19: mov      eax, dword ptr [0x4f7ef8]
0x462F1E: movzx    eax, word ptr [eax + 8]
0x462F22: mov      ecx, dword ptr [ebp - 8]
0x462F25: call     0x4a0364
0x462F2A: lea      eax, [ebp - 0x10]
0x462F2D: push     eax
0x462F2E: lea      eax, [ebp - 0x14]
0x462F31: push     eax
0x462F32: mov      ecx, dword ptr [ebp - 8]
0x462F35: mov      edx, dword ptr [ebp - 0x14]
0x462F38: mov      eax, dword ptr [ebp - 0x10]
0x462F3B: call     0x4a0364
0x462F40: mov      eax, dword ptr [0x4f7ef8]
0x462F45: movzx    eax, word ptr [eax + 8]
0x462F49: sub      eax, dword ptr [ebp - 0x10]
0x462F4C: cdq      
0x462F4D: xor      eax, edx
0x462F4F: sub      eax, edx
0x462F51: cmp      eax, 2
0x462F54: je       0x462f6c
0x462F56: mov      eax, dword ptr [0x4f7ef8]
0x462F5B: movzx    eax, word ptr [eax + 0xa]
0x462F5F: sub      eax, dword ptr [ebp - 0x14]
0x462F62: cdq      
0x462F63: xor      eax, edx
0x462F65: sub      eax, edx
0x462F67: cmp      eax, 2
0x462F6A: jne      0x462f92
0x462F6C: mov      ecx, dword ptr [ebp - 0x14]
0x462F6F: mov      edx, dword ptr [ebp - 0x10]
0x462F72: mov      eax, dword ptr [0x4f7d9c]
0x462F77: call     0x472e1c
0x462F7C: mov      dword ptr [ebp - 0x18], eax
0x462F7F: cmp      dword ptr [ebp - 0x18], 0
0x462F83: je       0x462f92
0x462F85: mov      eax, dword ptr [ebp - 0x18]
0x462F88: cmp      byte ptr [eax + 0x20], 0
0x462F8C: jne      0x462f92
0x462F8E: mov      byte ptr [ebp - 9], 1
0x462F92: mov      al, byte ptr [ebp - 9]
0x462F95: mov      esp, ebp
0x462F97: pop      ebp
0x462F98: ret      
0x462F99: lea      eax, [eax]
0x462F9C: push     ebp
0x462F9D: mov      ebp, esp
0x462F9F: add      esp, -0x28
0x462FA2: mov      dword ptr [ebp - 8], edx
0x462FA5: mov      dword ptr [ebp - 4], eax
0x462FA8: mov      byte ptr [ebp - 9], 0
0x462FAC: lea      eax, [ebp - 0x10]
0x462FAF: push     eax
0x462FB0: lea      eax, [ebp - 0x14]
0x462FB3: push     eax
0x462FB4: mov      eax, dword ptr [0x4f7ef8]
0x462FB9: movzx    edx, word ptr [eax + 0xa]
0x462FBD: mov      eax, dword ptr [0x4f7ef8]
0x462FC2: movzx    eax, word ptr [eax + 8]
0x462FC6: mov      ecx, dword ptr [ebp - 8]
0x462FC9: call     0x4a0364
0x462FCE: mov      ecx, dword ptr [ebp - 0x14]
0x462FD1: mov      edx, dword ptr [ebp - 0x10]
0x462FD4: mov      eax, dword ptr [0x4f7d9c]
0x462FD9: call     0x472e1c
0x462FDE: mov      dword ptr [ebp - 0x24], eax
0x462FE1: mov      eax, dword ptr [ebp - 8]
0x462FE4: inc      eax
0x462FE5: and      eax, 0x80000007
0x462FEA: jns      0x462ff1
0x462FEC: dec      eax
0x462FED: or       eax, 0xfffffff8
0x462FF0: inc      eax
0x462FF1: mov      dword ptr [ebp - 0x20], eax
0x462FF4: lea      eax, [ebp - 0x18]
0x462FF7: push     eax
0x462FF8: lea      eax, [ebp - 0x1c]
0x462FFB: push     eax
0x462FFC: mov      eax, dword ptr [0x4f7ef8]
0x463001: movzx    edx, word ptr [eax + 0xa]
0x463005: mov      eax, dword ptr [0x4f7ef8]
0x46300A: movzx    eax, word ptr [eax + 8]
0x46300E: mov      ecx, dword ptr [ebp - 0x20]
0x463011: call     0x4a0364
0x463016: mov      ecx, dword ptr [ebp - 0x1c]
0x463019: mov      edx, dword ptr [ebp - 0x18]
0x46301C: mov      eax, dword ptr [0x4f7d9c]
0x463021: call     0x472e1c
0x463026: mov      dword ptr [ebp - 0x28], eax
0x463029: cmp      dword ptr [ebp - 0x28], 0
0x46302D: jne      0x463079
0x46302F: mov      eax, dword ptr [ebp - 8]
0x463032: add      eax, 2
0x463035: and      eax, 0x80000007
0x46303A: jns      0x463041
0x46303C: dec      eax
0x46303D: or       eax, 0xfffffff8
0x463040: inc      eax
0x463041: mov      dword ptr [ebp - 0x20], eax
0x463044: lea      eax, [ebp - 0x18]
0x463047: push     eax
0x463048: lea      eax, [ebp - 0x1c]
0x46304B: push     eax
0x46304C: mov      eax, dword ptr [0x4f7ef8]
0x463051: movzx    edx, word ptr [eax + 0xa]
0x463055: mov      eax, dword ptr [0x4f7ef8]
0x46305A: movzx    eax, word ptr [eax + 8]
0x46305E: mov      ecx, dword ptr [ebp - 0x20]
0x463061: call     0x4a0364
0x463066: mov      ecx, dword ptr [ebp - 0x1c]
0x463069: mov      edx, dword ptr [ebp - 0x18]
0x46306C: mov      eax, dword ptr [0x4f7d9c]
0x463071: call     0x472e1c
0x463076: mov      dword ptr [ebp - 0x28], eax
0x463079: cmp      dword ptr [ebp - 0x28], 0
0x46307D: jne      0x4630c9
0x46307F: mov      eax, dword ptr [ebp - 8]
0x463082: add      eax, 7
0x463085: and      eax, 0x80000007
0x46308A: jns      0x463091
0x46308C: dec      eax
0x46308D: or       eax, 0xfffffff8
0x463090: inc      eax
0x463091: mov      dword ptr [ebp - 0x20], eax
0x463094: lea      eax, [ebp - 0x18]
0x463097: push     eax
0x463098: lea      eax, [ebp - 0x1c]
0x46309B: push     eax
0x46309C: mov      eax, dword ptr [0x4f7ef8]
0x4630A1: movzx    edx, word ptr [eax + 0xa]
0x4630A5: mov      eax, dword ptr [0x4f7ef8]
0x4630AA: movzx    eax, word ptr [eax + 8]
0x4630AE: mov      ecx, dword ptr [ebp - 0x20]
0x4630B1: call     0x4a0364
0x4630B6: mov      ecx, dword ptr [ebp - 0x1c]
0x4630B9: mov      edx, dword ptr [ebp - 0x18]
0x4630BC: mov      eax, dword ptr [0x4f7d9c]
0x4630C1: call     0x472e1c
0x4630C6: mov      dword ptr [ebp - 0x28], eax
0x4630C9: cmp      dword ptr [ebp - 0x24], 0
0x4630CD: je       0x4630eb
0x4630CF: cmp      dword ptr [ebp - 0x28], 0
0x4630D3: je       0x4630eb
0x4630D5: mov      eax, dword ptr [ebp - 0x24]
0x4630D8: cmp      byte ptr [eax + 0x20], 0
0x4630DC: jne      0x4630eb
0x4630DE: mov      eax, dword ptr [ebp - 0x28]
0x4630E1: cmp      byte ptr [eax + 0x20], 0
0x4630E5: jne      0x4630eb
0x4630E7: mov      byte ptr [ebp - 9], 1
0x4630EB: mov      al, byte ptr [ebp - 9]
0x4630EE: mov      esp, ebp
0x4630F0: pop      ebp
0x4630F1: ret      
0x4630F2: mov      eax, eax
0x4630F4: push     ebp
0x4630F5: mov      ebp, esp
0x4630F7: add      esp, -0x2c
0x4630FA: push     ebx
0x4630FB: xor      ebx, ebx
0x4630FD: mov      dword ptr [ebp - 0x1c], ebx
0x463100: mov      byte ptr [ebp - 5], cl
0x463103: mov      dword ptr [ebp - 0x20], edx
0x463106: mov      dword ptr [ebp - 4], eax
0x463109: xor      eax, eax
0x46310B: push     ebp
0x46310C: push     0x4632ff
0x463111: push     dword ptr fs:[eax]
0x463114: mov      dword ptr fs:[eax], esp
0x463117: mov      eax, dword ptr [ebp + 8]
0x46311A: push     eax
0x46311B: mov      eax, dword ptr [ebp - 4]
0x46311E: mov      eax, dword ptr [eax + 0x31c]
0x463124: mov      ecx, dword ptr [ebp + 0xc]
0x463127: mov      dl, byte ptr [ebp - 5]
0x46312A: call     0x45866c
0x46312F: test     al, al
0x463131: jne      0x4632e9
0x463137: cmp      dword ptr [0x4f7ef8], 0
0x46313E: je       0x4632e9
0x463144: mov      eax, dword ptr [0x4f7d8c]
0x463149: mov      eax, dword ptr [eax + 0x14]
0x46314C: cmp      eax, dword ptr [0x4f7d9c]
0x463152: jne      0x4632e9
0x463158: mov      ecx, dword ptr [ebp + 8]
0x46315B: mov      edx, dword ptr [ebp + 0xc]
0x46315E: mov      eax, dword ptr [0x4f7d9c]
0x463163: call     0x4729a0
0x463168: mov      byte ptr [0x4f94b1], al
0x46316D: mov      eax, dword ptr [0x4f94f4]
0x463172: push     eax
0x463173: lea      eax, [ebp - 0x14]
0x463176: push     eax
0x463177: push     0
0x463179: mov      ecx, dword ptr [ebp + 8]
0x46317C: mov      edx, dword ptr [ebp + 0xc]
0x46317F: mov      eax, dword ptr [0x4f7d9c]
0x463184: call     0x472768
0x463189: mov      dword ptr [ebp - 0x18], eax
0x46318C: mov      eax, dword ptr [0x4f94f4]
0x463191: cmp      eax, dword ptr [ebp - 0x14]
0x463194: je       0x46319d
0x463196: xor      eax, eax
0x463198: mov      dword ptr [0x4f94f4], eax
0x46319D: cmp      dword ptr [ebp - 0x18], 0
0x4631A1: je       0x4631f6
0x4631A3: mov      eax, dword ptr [ebp - 0x18]
0x4631A6: cmp      dword ptr [eax + 0x28], 0
0x4631AA: jne      0x4631ec
0x4631AC: call     0x406154
0x4631B1: mov      edx, dword ptr [ebp - 0x18]
0x4631B4: sub      eax, dword ptr [edx + 0x16c]
0x4631BA: cmp      eax, 0x2710
0x4631BF: jbe      0x4631ec
0x4631C1: call     0x406154
0x4631C6: mov      edx, dword ptr [ebp - 0x18]
0x4631C9: mov      dword ptr [edx + 0x16c], eax
0x4631CF: mov      eax, dword ptr [ebp - 0x18]
0x4631D2: movzx    eax, word ptr [eax + 0xa]
0x4631D6: push     eax
0x4631D7: mov      eax, dword ptr [ebp - 0x18]
0x4631DA: movzx    ecx, word ptr [eax + 8]
0x4631DE: mov      eax, dword ptr [ebp - 0x18]
0x4631E1: mov      edx, dword ptr [eax + 4]
0x4631E4: mov      eax, dword ptr [ebp - 4]
0x4631E7: call     0x465830
0x4631EC: mov      eax, dword ptr [ebp - 0x18]
0x4631EF: mov      dword ptr [0x4f94a8], eax
0x4631F4: jmp      0x4631fd
0x4631F6: xor      eax, eax
0x4631F8: mov      dword ptr [0x4f94a8], eax
0x4631FD: lea      eax, [ebp - 0x1c]
0x463200: push     eax
0x463201: mov      ecx, dword ptr [ebp + 8]
0x463204: mov      edx, dword ptr [ebp + 0xc]
0x463207: mov      eax, dword ptr [0x4f7d9c]
0x46320C: call     0x472abc
0x463211: mov      dword ptr [0x4f94b4], eax
0x463216: cmp      dword ptr [0x4f94b4], 0
0x46321D: je       0x463266
0x46321F: lea      eax, [ebp - 0xc]
0x463222: push     eax
0x463223: lea      eax, [ebp - 0x10]
0x463226: push     eax
0x463227: mov      eax, dword ptr [0x4f94b4]
0x46322C: movzx    ecx, word ptr [eax + 6]
0x463230: mov      eax, dword ptr [0x4f94b4]
0x463235: movzx    edx, word ptr [eax + 4]
0x463239: mov      eax, dword ptr [0x4f7d9c]
0x46323E: call     0x472494
0x463243: mov      eax, dword ptr [ebp - 0x1c]
0x463246: push     eax
0x463247: push     0xffffff
0x46324C: push     1
0x46324E: mov      ecx, dword ptr [ebp - 0x10]
0x463251: sub      ecx, 0xa
0x463254: mov      edx, dword ptr [ebp - 0xc]
0x463257: sub      edx, 0x14
0x46325A: mov      eax, dword ptr [0x4f7d8c]
0x46325F: call     0x479d34
0x463264: jmp      0x463270
0x463266: mov      eax, dword ptr [0x4f7d8c]
0x46326B: call     0x479ebc
0x463270: push     0x4f948c
0x463275: push     0x4f9490
0x46327A: mov      ecx, dword ptr [ebp + 8]
0x46327D: mov      edx, dword ptr [ebp + 0xc]
0x463280: mov      eax, dword ptr [0x4f7d9c]
0x463285: call     0x472518
0x46328A: mov      eax, dword ptr [ebp + 0xc]
0x46328D: mov      dword ptr [0x4f9494], eax
0x463292: mov      eax, dword ptr [ebp + 8]
0x463295: mov      dword ptr [0x4f9498], eax
0x46329A: mov      byte ptr [0x4f9194], 0
0x4632A1: mov      byte ptr [0x4f91c8], 0
0x4632A8: mov      byte ptr [0x4f91fc], 0
0x4632AF: test     byte ptr [ebp - 5], 8
0x4632B3: jne      0x4632bb
0x4632B5: test     byte ptr [ebp - 5], 0x10
0x4632B9: je       0x4632e9
0x4632BB: call     0x406154
0x4632C0: mov      edx, dword ptr [ebp - 4]
0x4632C3: sub      eax, dword ptr [edx + 0x3f8]
0x4632C9: cmp      eax, 0x12c
0x4632CE: jbe      0x4632e9
0x4632D0: mov      al, byte ptr [ebp - 5]
0x4632D3: push     eax
0x4632D4: mov      eax, dword ptr [ebp + 0xc]
0x4632D7: push     eax
0x4632D8: mov      eax, dword ptr [ebp + 8]
0x4632DB: push     eax
0x4632DC: xor      ecx, ecx
0x4632DE: mov      edx, dword ptr [ebp - 4]
0x4632E1: mov      eax, dword ptr [ebp - 4]
0x4632E4: call     0x463518
0x4632E9: xor      eax, eax
0x4632EB: pop      edx
0x4632EC: pop      ecx
0x4632ED: pop      ecx
0x4632EE: mov      dword ptr fs:[eax], edx
0x4632F1: push     0x463306
0x4632F6: lea      eax, [ebp - 0x1c]
0x4632F9: call     0x403a58
0x4632FE: ret      
0x4632FF: jmp      0x403518
0x463304: jmp      0x4632f6
0x463306: pop      ebx
0x463307: mov      esp, ebp
0x463309: pop      ebp
0x46330A: ret      8
0x46330D: lea      eax, [eax]
0x463310: push     ebp
0x463311: mov      ebp, esp
0x463313: add      esp, -0xc
0x463316: mov      byte ptr [ebp - 9], cl
0x463319: mov      dword ptr [ebp - 8], edx
0x46331C: mov      dword ptr [ebp - 4], eax
0x46331F: call     0x406154
0x463324: mov      edx, dword ptr [ebp - 4]
0x463327: mov      dword ptr [edx + 0x3f8], eax
0x46332D: xor      eax, eax
0x46332F: mov      dword ptr [0x4f94c4], eax
0x463334: mov      al, byte ptr [ebp + 0x10]
0x463337: push     eax
0x463338: mov      eax, dword ptr [ebp + 0xc]
0x46333B: push     eax
0x46333C: mov      eax, dword ptr [ebp + 8]
0x46333F: push     eax
0x463340: mov      cl, byte ptr [ebp - 9]
0x463343: mov      edx, dword ptr [ebp - 8]
0x463346: mov      eax, dword ptr [ebp - 4]
0x463349: call     0x463518
0x46334E: mov      esp, ebp
0x463350: pop      ebp
0x463351: ret      0xc
0x463354: push     ebp
0x463355: mov      ebp, esp
0x463357: add      esp, -0x18
0x46335A: mov      dword ptr [ebp - 8], edx
0x46335D: mov      dword ptr [ebp - 4], eax
0x463360: mov      dword ptr [ebp - 0x18], 0xbc6
0x463367: cmp      byte ptr [0x4f7f3f], 6
0x46336E: jne      0x463377
0x463370: mov      dword ptr [ebp - 0x18], 0xbc7
0x463377: mov      eax, dword ptr [ebp - 8]
0x46337A: movzx    eax, word ptr [eax + 0xa]
0x46337E: push     eax
0x46337F: mov      eax, dword ptr [ebp - 8]
0x463382: movzx    ecx, word ptr [eax + 8]
0x463386: mov      eax, dword ptr [0x4f7ef8]
0x46338B: movzx    edx, word ptr [eax + 0xa]
0x46338F: mov      eax, dword ptr [0x4f7ef8]
0x463394: movzx    eax, word ptr [eax + 8]
0x463398: call     0x4a0104
0x46339D: and      eax, 0xff
0x4633A2: mov      dword ptr [ebp - 0xc], eax
0x4633A5: mov      eax, dword ptr [0x4f7ef8]
0x4633AA: movzx    eax, word ptr [eax + 8]
0x4633AE: mov      edx, dword ptr [ebp - 8]
0x4633B1: movzx    edx, word ptr [edx + 8]
0x4633B5: sub      eax, edx
0x4633B7: cdq      
0x4633B8: xor      eax, edx
0x4633BA: sub      eax, edx
0x4633BC: dec      eax
0x4633BD: jg       0x4634dc
0x4633C3: mov      eax, dword ptr [0x4f7ef8]
0x4633C8: movzx    eax, word ptr [eax + 0xa]
0x4633CC: mov      edx, dword ptr [ebp - 8]
0x4633CF: movzx    edx, word ptr [edx + 0xa]
0x4633D3: sub      eax, edx
0x4633D5: cdq      
0x4633D6: xor      eax, edx
0x4633D8: sub      eax, edx
0x4633DA: dec      eax
0x4633DB: jg       0x4634dc
0x4633E1: mov      eax, dword ptr [ebp - 8]
0x4633E4: cmp      byte ptr [eax + 0x20], 0
0x4633E8: jne      0x4634dc
0x4633EE: mov      eax, dword ptr [ebp - 4]
0x4633F1: call     0x466f60
0x4633F6: test     al, al
0x4633F8: je       0x4634d0
0x4633FE: mov      eax, dword ptr [ebp - 4]
0x463401: call     0x466f1c
0x463406: test     al, al
0x463408: je       0x4634d0
0x46340E: mov      eax, dword ptr [ebp - 4]
0x463411: call     0x466fbc
0x463416: test     al, al
0x463418: je       0x4634d0
0x46341E: cmp      byte ptr [0x4f955f], 0
0x463425: je       0x463443
0x463427: mov      eax, dword ptr [0x4f7ef8]
0x46342C: cmp      word ptr [eax + 0x3e], 7
0x463431: jb       0x463443
0x463433: mov      byte ptr [0x4f955f], 0
0x46343A: mov      dword ptr [ebp - 0x18], 0xbd1
0x463441: jmp      0x463499
0x463443: cmp      byte ptr [0x4f955c], 0
0x46344A: je       0x46345c
0x46344C: mov      byte ptr [0x4f955c], 0
0x463453: mov      dword ptr [ebp - 0x18], 0xbca
0x46345A: jmp      0x463499
0x46345C: cmp      byte ptr [0x4f955e], 0
0x463463: je       0x46347a
0x463465: mov      eax, dword ptr [0x4f7ef8]
0x46346A: cmp      word ptr [eax + 0x3e], 3
0x46346F: jb       0x46347a
0x463471: mov      dword ptr [ebp - 0x18], 0xbd0
0x463478: jmp      0x463499
0x46347A: cmp      byte ptr [0x4f955d], 0
0x463481: je       0x463499
0x463483: mov      edx, dword ptr [ebp - 0xc]
0x463486: mov      eax, dword ptr [ebp - 4]
0x463489: call     0x462ef8
0x46348E: test     al, al
0x463490: je       0x463499
0x463492: mov      dword ptr [ebp - 0x18], 0xbcb
0x463499: mov      eax, dword ptr [0x4f7ef8]
0x46349E: movzx    eax, word ptr [eax + 0xa]
0x4634A2: push     eax
0x4634A3: mov      eax, dword ptr [ebp - 0xc]
0x4634A6: push     eax
0x4634A7: push     0
0x4634A9: push     0
0x4634AB: push     0
0x4634AD: push     0
0x4634AF: mov      eax, dword ptr [0x4f7ef8]
0x4634B4: movzx    ecx, word ptr [eax + 8]
0x4634B8: mov      dx, word ptr [ebp - 0x18]
0x4634BC: mov      eax, dword ptr [0x4f7ef8]
0x4634C1: call     0x496548
0x4634C6: call     0x406154
0x4634CB: mov      dword ptr [0x4f9458], eax
0x4634D0: call     0x406154
0x4634D5: mov      dword ptr [0x4f9394], eax
0x4634DA: jmp      0x463511
0x4634DC: mov      byte ptr [0x4f94c0], 0
0x4634E3: lea      eax, [ebp - 0x10]
0x4634E6: push     eax
0x4634E7: lea      eax, [ebp - 0x14]
0x4634EA: push     eax
0x4634EB: mov      eax, dword ptr [ebp - 8]
0x4634EE: movzx    edx, word ptr [eax + 0xa]
0x4634F2: mov      eax, dword ptr [ebp - 8]
0x4634F5: movzx    eax, word ptr [eax + 8]
0x4634F9: mov      ecx, dword ptr [ebp - 0xc]
0x4634FC: call     0x4a02c4
0x463501: mov      eax, dword ptr [ebp - 0x10]
0x463504: mov      dword ptr [0x4f949c], eax
0x463509: mov      eax, dword ptr [ebp - 0x14]
0x46350C: mov      dword ptr [0x4f94a0], eax
0x463511: mov      esp, ebp
0x463513: pop      ebp
0x463514: ret      
0x463515: lea      eax, [eax]
0x463518: push     ebp
0x463519: mov      ebp, esp
0x46351B: add      esp, -0x24
0x46351E: mov      byte ptr [ebp - 5], cl
0x463521: mov      dword ptr [ebp - 0x24], edx
0x463524: mov      dword ptr [ebp - 4], eax
0x463527: mov      eax, dword ptr [ebp - 4]
0x46352A: mov      word ptr [eax + 0x3f0], 0
0x463533: mov      eax, dword ptr [ebp + 0xc]
0x463536: mov      dword ptr [0x4f9494], eax
0x46353B: mov      eax, dword ptr [ebp + 8]
0x46353E: mov      dword ptr [0x4f9498], eax
0x463543: mov      byte ptr [0x4f94b0], 0
0x46354A: cmp      byte ptr [ebp - 5], 1
0x46354E: sete     al
0x463551: and      al, byte ptr [0x4f939c]
0x463557: je       0x46356a
0x463559: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46355E: mov      eax, dword ptr [eax]
0x463560: call     0x486564
0x463565: jmp      0x463be4
0x46356A: mov      eax, dword ptr [ebp + 0xc]
0x46356D: push     eax
0x46356E: mov      eax, dword ptr [ebp + 8]
0x463571: push     eax
0x463572: mov      cl, byte ptr [ebp + 0x10]
0x463575: mov      dl, byte ptr [ebp - 5]
0x463578: mov      eax, dword ptr [ebp - 4]
0x46357B: mov      eax, dword ptr [eax + 0x31c]
0x463581: call     0x458780
0x463586: test     al, al
0x463588: jne      0x463be4
0x46358E: cmp      dword ptr [0x4f7ef8], 0
0x463595: je       0x463be4
0x46359B: mov      eax, dword ptr [0x4f7d8c]
0x4635A0: mov      eax, dword ptr [eax + 0x14]
0x4635A3: cmp      eax, dword ptr [0x4f7d9c]
0x4635A9: jne      0x463be4
0x4635AF: test     byte ptr [ebp + 0x10], 0x10
0x4635B3: je       0x46373c
0x4635B9: mov      al, byte ptr [0x463bec]
0x4635BE: cmp      al, byte ptr [ebp + 0x10]
0x4635C1: jne      0x4635c9
0x4635C3: inc      dword ptr [0x4f94f4]
0x4635C9: mov      eax, dword ptr [0x4f94f4]
0x4635CE: push     eax
0x4635CF: lea      eax, [ebp - 0x1c]
0x4635D2: push     eax
0x4635D3: push     0
0x4635D5: mov      ecx, dword ptr [ebp + 8]
0x4635D8: mov      edx, dword ptr [ebp + 0xc]
0x4635DB: mov      eax, dword ptr [0x4f7d9c]
0x4635E0: call     0x472768
0x4635E5: mov      dword ptr [ebp - 0x20], eax
0x4635E8: mov      eax, dword ptr [0x4f94f4]
0x4635ED: cmp      eax, dword ptr [ebp - 0x1c]
0x4635F0: je       0x4635f9
0x4635F2: xor      eax, eax
0x4635F4: mov      dword ptr [0x4f94f4], eax
0x4635F9: cmp      dword ptr [ebp - 0x20], 0
0x4635FD: je       0x463653
0x4635FF: test     byte ptr [ebp + 0x10], 4
0x463603: je       0x46365a
0x463605: call     0x406154
0x46360A: sub      eax, dword ptr [0x4f9398]
0x463610: cmp      eax, 0x3e8
0x463615: jbe      0x46365a
0x463617: mov      eax, dword ptr [ebp - 0x20]
0x46361A: cmp      byte ptr [eax + 0xe], 0
0x46361E: jne      0x46365a
0x463620: mov      eax, dword ptr [ebp - 0x20]
0x463623: cmp      byte ptr [eax + 0x20], 0
0x463627: jne      0x46365a
0x463629: mov      eax, dword ptr [ebp - 0x20]
0x46362C: movzx    eax, word ptr [eax + 8]
0x463630: push     eax
0x463631: mov      eax, dword ptr [ebp - 0x20]
0x463634: movzx    eax, word ptr [eax + 0xa]
0x463638: push     eax
0x463639: push     0
0x46363B: mov      eax, dword ptr [ebp - 0x20]
0x46363E: mov      ecx, dword ptr [eax + 4]
0x463641: mov      edx, 0x52
0x463646: mov      eax, dword ptr [ebp - 4]
0x463649: call     0x464b18
0x46364E: jmp      0x463be4
0x463653: xor      eax, eax
0x463655: mov      dword ptr [0x4f94f4], eax
0x46365A: push     0x4f948c
0x46365F: push     0x4f9490
0x463664: mov      ecx, dword ptr [ebp + 8]
0x463667: mov      edx, dword ptr [ebp + 0xc]
0x46366A: mov      eax, dword ptr [0x4f7d9c]
0x46366F: call     0x472518
0x463674: mov      eax, dword ptr [0x4f7ef8]
0x463679: movzx    eax, word ptr [eax + 8]
0x46367D: sub      eax, dword ptr [0x4f948c]
0x463683: cdq      
0x463684: xor      eax, edx
0x463686: sub      eax, edx
0x463688: cmp      eax, 2
0x46368B: jg       0x46371c
0x463691: mov      eax, dword ptr [0x4f7ef8]
0x463696: movzx    eax, word ptr [eax + 0xa]
0x46369A: sub      eax, dword ptr [0x4f9490]
0x4636A0: cdq      
0x4636A1: xor      eax, edx
0x4636A3: sub      eax, edx
0x4636A5: cmp      eax, 2
0x4636A8: jg       0x46371c
0x4636AA: mov      eax, dword ptr [0x4f9490]
0x4636AF: push     eax
0x4636B0: mov      eax, dword ptr [0x4f7ef8]
0x4636B5: movzx    edx, word ptr [eax + 0xa]
0x4636B9: mov      eax, dword ptr [0x4f7ef8]
0x4636BE: movzx    eax, word ptr [eax + 8]
0x4636C2: mov      ecx, dword ptr [0x4f948c]
0x4636C8: call     0x4a0104
0x4636CD: and      eax, 0xff
0x4636D2: mov      dword ptr [ebp - 0xc], eax
0x4636D5: mov      eax, dword ptr [ebp - 4]
0x4636D8: call     0x466f60
0x4636DD: test     al, al
0x4636DF: je       0x46373c
0x4636E1: mov      eax, dword ptr [ebp - 4]
0x4636E4: call     0x466f1c
0x4636E9: test     al, al
0x4636EB: je       0x46373c
0x4636ED: mov      eax, dword ptr [0x4f7ef8]
0x4636F2: movzx    eax, word ptr [eax + 0xa]
0x4636F6: push     eax
0x4636F7: mov      eax, dword ptr [ebp - 0xc]
0x4636FA: push     eax
0x4636FB: push     0
0x4636FD: push     0
0x4636FF: push     0
0x463701: push     0
0x463703: mov      eax, dword ptr [0x4f7ef8]
0x463708: movzx    ecx, word ptr [eax + 8]
0x46370C: mov      dx, 0xbc2
0x463710: mov      eax, dword ptr [0x4f7ef8]
0x463715: call     0x496548
0x46371A: jmp      0x46373c
0x46371C: mov      byte ptr [0x4f94c0], 1
0x463723: mov      eax, dword ptr [0x4f948c]
0x463728: mov      dword ptr [0x4f949c], eax
0x46372D: mov      eax, dword ptr [0x4f9490]
0x463732: mov      dword ptr [0x4f94a0], eax
0x463737: jmp      0x463be4
0x46373C: test     byte ptr [ebp + 0x10], 8
0x463740: je       0x463be4
0x463746: mov      eax, dword ptr [0x4f94f4]
0x46374B: push     eax
0x46374C: lea      eax, [ebp - 0x1c]
0x46374F: push     eax
0x463750: push     1
0x463752: mov      ecx, dword ptr [ebp + 8]
0x463755: mov      edx, dword ptr [ebp + 0xc]
0x463758: mov      eax, dword ptr [0x4f7d9c]
0x46375D: call     0x472768
0x463762: mov      dword ptr [ebp - 0x20], eax
0x463765: push     0x4f948c
0x46376A: push     0x4f9490
0x46376F: mov      ecx, dword ptr [ebp + 8]
0x463772: mov      edx, dword ptr [ebp + 0xc]
0x463775: mov      eax, dword ptr [0x4f7d9c]
0x46377A: call     0x472518
0x46377F: xor      eax, eax
0x463781: mov      dword ptr [0x4f94a4], eax
0x463786: cmp      byte ptr [0x4f7f30], 0
0x46378D: je       0x46386e
0x463793: cmp      dword ptr [ebp - 0x20], 0
0x463797: jne      0x46386e
0x46379D: cmp      byte ptr [0x4f7f40], 0x13
0x4637A4: jne      0x46386e
0x4637AA: mov      eax, dword ptr [0x4f9490]
0x4637AF: push     eax
0x4637B0: mov      eax, dword ptr [0x4f7ef8]
0x4637B5: movzx    edx, word ptr [eax + 0xa]
0x4637B9: mov      eax, dword ptr [0x4f7ef8]
0x4637BE: movzx    eax, word ptr [eax + 8]
0x4637C2: mov      ecx, dword ptr [0x4f948c]
0x4637C8: call     0x4a0104
0x4637CD: and      eax, 0xff
0x4637D2: mov      dword ptr [ebp - 0xc], eax
0x4637D5: lea      eax, [ebp - 0x10]
0x4637D8: push     eax
0x4637D9: lea      eax, [ebp - 0x14]
0x4637DC: push     eax
0x4637DD: mov      eax, dword ptr [0x4f7ef8]
0x4637E2: movzx    edx, word ptr [eax + 0xa]
0x4637E6: mov      eax, dword ptr [0x4f7ef8]
0x4637EB: movzx    eax, word ptr [eax + 8]
0x4637EF: mov      ecx, dword ptr [ebp - 0xc]
0x4637F2: call     0x4a0364
0x4637F7: mov      ecx, dword ptr [ebp - 0x14]
0x4637FA: mov      edx, dword ptr [ebp - 0x10]
0x4637FD: mov      eax, dword ptr [0x4f7ef4]
0x463802: call     0x49f120
0x463807: test     al, al
0x463809: je       0x463811
0x46380B: test     byte ptr [ebp + 0x10], 1
0x46380F: je       0x46386e
0x463811: mov      eax, dword ptr [ebp - 4]
0x463814: call     0x466f60
0x463819: test     al, al
0x46381B: je       0x463862
0x46381D: mov      eax, dword ptr [ebp - 4]
0x463820: call     0x466f1c
0x463825: test     al, al
0x463827: je       0x463862
0x463829: mov      eax, dword ptr [ebp - 4]
0x46382C: call     0x466fbc
0x463831: test     al, al
0x463833: je       0x463862
0x463835: mov      eax, dword ptr [0x4f7ef8]
0x46383A: movzx    eax, word ptr [eax + 0xa]
0x46383E: push     eax
0x46383F: mov      eax, dword ptr [ebp - 0xc]
0x463842: push     eax
0x463843: push     0
0x463845: push     0
0x463847: push     0
0x463849: push     0
0x46384B: mov      eax, dword ptr [0x4f7ef8]
0x463850: movzx    ecx, word ptr [eax + 8]
0x463854: mov      dx, 0xbc7
0x463858: mov      eax, dword ptr [0x4f7ef8]
0x46385D: call     0x496548
0x463862: mov      byte ptr [0x4f94b0], 1
0x463869: jmp      0x463be4
0x46386E: test     byte ptr [ebp + 0x10], 2
0x463872: je       0x46396f
0x463878: mov      eax, dword ptr [0x4f9490]
0x46387D: push     eax
0x46387E: mov      eax, dword ptr [0x4f7ef8]
0x463883: movzx    edx, word ptr [eax + 0xa]
0x463887: mov      eax, dword ptr [0x4f7ef8]
0x46388C: movzx    eax, word ptr [eax + 8]
0x463890: mov      ecx, dword ptr [0x4f948c]
0x463896: call     0x4a0104
0x46389B: and      eax, 0xff
0x4638A0: mov      dword ptr [ebp - 0xc], eax
0x4638A3: mov      eax, dword ptr [ebp - 4]
0x4638A6: call     0x466f60
0x4638AB: test     al, al
0x4638AD: je       0x463960
0x4638B3: mov      eax, dword ptr [ebp - 4]
0x4638B6: call     0x466f1c
0x4638BB: test     al, al
0x4638BD: je       0x463960
0x4638C3: mov      ecx, dword ptr [0x4f9490]
0x4638C9: mov      edx, dword ptr [0x4f948c]
0x4638CF: mov      eax, dword ptr [0x4f7d9c]
0x4638D4: call     0x47385c
0x4638D9: mov      dword ptr [ebp - 0x20], eax
0x4638DC: cmp      dword ptr [ebp - 0x20], 0
0x4638E0: je       0x463933
0x4638E2: mov      eax, dword ptr [ebp - 0xc]
0x4638E5: push     eax
0x4638E6: mov      eax, dword ptr [ebp - 0x20]
0x4638E9: mov      eax, dword ptr [eax + 4]
0x4638EC: push     eax
0x4638ED: mov      ecx, dword ptr [0x4f9490]
0x4638F3: mov      edx, dword ptr [0x4f948c]
0x4638F9: mov      eax, dword ptr [ebp - 4]
0x4638FC: call     0x465bb8
0x463901: mov      eax, dword ptr [0x4f7ef8]
0x463906: movzx    eax, word ptr [eax + 0xa]
0x46390A: push     eax
0x46390B: mov      eax, dword ptr [ebp - 0xc]
0x46390E: push     eax
0x46390F: push     0
0x463911: push     0
0x463913: push     0
0x463915: push     0
0x463917: mov      eax, dword ptr [0x4f7ef8]
0x46391C: movzx    ecx, word ptr [eax + 8]
0x463920: mov      dx, 0xbc4
0x463924: mov      eax, dword ptr [0x4f7ef8]
0x463929: call     0x496548
0x46392E: jmp      0x463be4
0x463933: mov      eax, dword ptr [0x4f7ef8]
0x463938: movzx    eax, word ptr [eax + 0xa]
0x46393C: push     eax
0x46393D: mov      eax, dword ptr [ebp - 0xc]
0x463940: push     eax
0x463941: push     0
0x463943: push     0
0x463945: push     0
0x463947: push     0
0x463949: mov      eax, dword ptr [0x4f7ef8]
0x46394E: movzx    ecx, word ptr [eax + 8]
0x463952: mov      dx, 0xbc4
0x463956: mov      eax, dword ptr [0x4f7ef8]
0x46395B: call     0x496548
0x463960: mov      dword ptr [0x4f949c], 0xffffffff
0x46396A: jmp      0x463be4
0x46396F: cmp      dword ptr [ebp - 0x20], 0
0x463973: jne      0x46397f
0x463975: test     byte ptr [ebp + 0x10], 1
0x463979: je       0x463b39
0x46397F: mov      dword ptr [0x4f949c], 0xffffffff
0x463989: cmp      dword ptr [ebp - 0x20], 0
0x46398D: je       0x463a46
0x463993: call     0x406154
0x463998: sub      eax, dword ptr [0x4f9398]
0x46399E: cmp      eax, 0x5dc
0x4639A3: jbe      0x4639cc
0x4639A5: mov      eax, dword ptr [ebp - 0x20]
0x4639A8: cmp      byte ptr [eax + 0xe], 0x32
0x4639AC: jne      0x4639cc
0x4639AE: push     0
0x4639B0: push     0
0x4639B2: push     0
0x4639B4: mov      eax, dword ptr [ebp - 0x20]
0x4639B7: mov      ecx, dword ptr [eax + 4]
0x4639BA: mov      edx, 0x3f2
0x4639BF: mov      eax, dword ptr [ebp - 4]
0x4639C2: call     0x464b18
0x4639C7: jmp      0x463be4
0x4639CC: mov      eax, dword ptr [ebp - 0x20]
0x4639CF: cmp      byte ptr [eax + 0x20], 0
0x4639D3: jne      0x463be4
0x4639D9: mov      eax, dword ptr [ebp - 0x20]
0x4639DC: mov      dword ptr [0x4f94a4], eax
0x4639E1: mov      eax, dword ptr [ebp - 0x20]
0x4639E4: cmp      byte ptr [eax + 0xe], 0
0x4639E8: je       0x463a19
0x4639EA: mov      eax, dword ptr [ebp - 0x20]
0x4639ED: cmp      byte ptr [eax + 0xe], 0xc
0x4639F1: je       0x463a19
0x4639F3: mov      eax, dword ptr [ebp - 0x20]
0x4639F6: cmp      byte ptr [eax + 0xe], 0x18
0x4639FA: je       0x463a19
0x4639FC: mov      eax, dword ptr [ebp - 0x20]
0x4639FF: cmp      byte ptr [eax + 0xe], 0x32
0x463A03: je       0x463a19
0x463A05: mov      eax, dword ptr [ebp - 0x20]
0x463A08: mov      edx, dword ptr [eax + 0x28]
0x463A0B: mov      eax, 0x463bf8 ; "("
0x463A10: call     0x403f60
0x463A15: test     eax, eax
0x463A17: je       0x463a2c
0x463A19: test     byte ptr [ebp + 0x10], 1
0x463A1D: jne      0x463a2c
0x463A1F: mov      eax, dword ptr [ebp - 0x20]
0x463A22: cmp      dword ptr [eax + 0x2c], 0x45
0x463A26: jne      0x463be4
0x463A2C: mov      edx, dword ptr [ebp - 0x20]
0x463A2F: mov      eax, dword ptr [ebp - 4]
0x463A32: call     0x463354
0x463A37: call     0x406154
0x463A3C: mov      dword ptr [0x4f9458], eax
0x463A41: jmp      0x463be4
0x463A46: mov      eax, dword ptr [0x4f9490]
0x463A4B: push     eax
0x463A4C: mov      eax, dword ptr [0x4f7ef8]
0x463A51: movzx    edx, word ptr [eax + 0xa]
0x463A55: mov      eax, dword ptr [0x4f7ef8]
0x463A5A: movzx    eax, word ptr [eax + 8]
0x463A5E: mov      ecx, dword ptr [0x4f948c]
0x463A64: call     0x4a0104
0x463A69: and      eax, 0xff
0x463A6E: mov      dword ptr [ebp - 0xc], eax
0x463A71: mov      eax, dword ptr [ebp - 4]
0x463A74: call     0x466f60
0x463A79: test     al, al
0x463A7B: je       0x463b2a
0x463A81: mov      eax, dword ptr [ebp - 4]
0x463A84: call     0x466f1c
0x463A89: test     al, al
0x463A8B: je       0x463b2a
0x463A91: mov      eax, dword ptr [ebp - 4]
0x463A94: call     0x466fbc
0x463A99: test     al, al
0x463A9B: je       0x463b2a
0x463AA1: mov      eax, 3
0x463AA6: call     0x402b74
0x463AAB: add      eax, 0xbc6
0x463AB0: mov      dword ptr [ebp - 0x18], eax
0x463AB3: cmp      byte ptr [0x4f955d], 0
0x463ABA: je       0x463ad2
0x463ABC: mov      edx, dword ptr [ebp - 0xc]
0x463ABF: mov      eax, dword ptr [ebp - 4]
0x463AC2: call     0x462ef8
0x463AC7: test     al, al
0x463AC9: je       0x463ad2
0x463ACB: mov      dword ptr [ebp - 0x18], 0xbcb
0x463AD2: cmp      byte ptr [0x4f955e], 0
0x463AD9: je       0x463afd
0x463ADB: mov      eax, dword ptr [0x4f7ef8]
0x463AE0: cmp      word ptr [eax + 0x3e], 3
0x463AE5: jb       0x463afd
0x463AE7: mov      edx, dword ptr [ebp - 0xc]
0x463AEA: mov      eax, dword ptr [ebp - 4]
0x463AED: call     0x462f9c
0x463AF2: test     al, al
0x463AF4: je       0x463afd
0x463AF6: mov      dword ptr [ebp - 0x18], 0xbd0
0x463AFD: mov      eax, dword ptr [0x4f7ef8]
0x463B02: movzx    eax, word ptr [eax + 0xa]
0x463B06: push     eax
0x463B07: mov      eax, dword ptr [ebp - 0xc]
0x463B0A: push     eax
0x463B0B: push     0
0x463B0D: push     0
0x463B0F: push     0
0x463B11: push     0
0x463B13: mov      eax, dword ptr [0x4f7ef8]
0x463B18: movzx    ecx, word ptr [eax + 8]
0x463B1C: mov      dx, word ptr [ebp - 0x18]
0x463B20: mov      eax, dword ptr [0x4f7ef8]
0x463B25: call     0x496548
0x463B2A: call     0x406154
0x463B2F: mov      dword ptr [0x4f9394], eax
0x463B34: jmp      0x463be4
0x463B39: mov      eax, dword ptr [0x4f7ef8]
0x463B3E: movzx    eax, word ptr [eax + 8]
0x463B42: cmp      eax, dword ptr [0x4f948c]
0x463B48: jne      0x463ba8
0x463B4A: mov      eax, dword ptr [0x4f7ef8]
0x463B4F: movzx    eax, word ptr [eax + 0xa]
0x463B53: cmp      eax, dword ptr [0x4f9490]
0x463B59: jne      0x463ba8
0x463B5B: mov      eax, dword ptr [0x4f9490]
0x463B60: push     eax
0x463B61: mov      eax, dword ptr [0x4f7ef8]
0x463B66: movzx    edx, word ptr [eax + 0xa]
0x463B6A: mov      eax, dword ptr [0x4f7ef8]
0x463B6F: movzx    eax, word ptr [eax + 8]
0x463B73: mov      ecx, dword ptr [0x4f948c]
0x463B79: call     0x4a0104
0x463B7E: and      eax, 0xff
0x463B83: mov      dword ptr [ebp - 0xc], eax
0x463B86: mov      eax, dword ptr [ebp - 4]
0x463B89: call     0x466f60
0x463B8E: test     al, al
0x463B90: je       0x463be4
0x463B92: mov      eax, dword ptr [ebp - 4]
0x463B95: call     0x466f1c
0x463B9A: test     al, al
0x463B9C: je       0x463be4
0x463B9E: mov      eax, dword ptr [ebp - 4]
0x463BA1: call     0x46594c
0x463BA6: jmp      0x463be4
0x463BA8: call     0x406154
0x463BAD: sub      eax, dword ptr [0x4f9394]
0x463BB3: cmp      eax, 0x3e8
0x463BB8: jbe      0x463be4
0x463BBA: test     byte ptr [ebp + 0x10], 4
0x463BBE: je       0x463bc9
0x463BC0: mov      byte ptr [0x4f94c0], 1
0x463BC7: jmp      0x463bd0
0x463BC9: mov      byte ptr [0x4f94c0], 0
0x463BD0: mov      eax, dword ptr [0x4f948c]
0x463BD5: mov      dword ptr [0x4f949c], eax
0x463BDA: mov      eax, dword ptr [0x4f9490]
0x463BDF: mov      dword ptr [0x4f94a0], eax
0x463BE4: mov      esp, ebp
0x463BE6: pop      ebp
0x463BE7: ret      0xc
0x463BEA: add      byte ptr [eax], al
0x463BEC: adc      byte ptr [eax], al
0x463BEE: add      byte ptr [eax], al
0x463BF8: [STR len=1] "("
0x463BFC: push     ebp
0x463BFD: mov      ebp, esp
0x463BFF: add      esp, -0x10
0x463C02: mov      dword ptr [ebp - 0x10], edx
0x463C05: mov      dword ptr [ebp - 4], eax
0x463C08: lea      eax, [ebp - 0xc]
0x463C0B: push     eax
0x463C0C: call     0x4065fc
0x463C11: mov      ecx, dword ptr [ebp - 8]
0x463C14: mov      edx, dword ptr [ebp - 0xc]
0x463C17: mov      eax, dword ptr [ebp - 4]
0x463C1A: mov      eax, dword ptr [eax + 0x31c]
0x463C20: call     0x4589b8
0x463C25: test     al, al
0x463C27: mov      esp, ebp
0x463C29: pop      ebp
0x463C2A: ret      
0x463C2B: nop      
0x463C2C: push     ebp
0x463C2D: mov      ebp, esp
0x463C2F: add      esp, -0x20
0x463C32: mov      dword ptr [ebp - 0xc], ecx
0x463C35: mov      dword ptr [ebp - 8], edx
0x463C38: mov      dword ptr [ebp - 4], eax
0x463C3B: mov      byte ptr [ebp - 0xd], 0
0x463C3F: mov      ecx, dword ptr [ebp - 0xc]
0x463C42: mov      edx, dword ptr [ebp - 8]
0x463C45: mov      eax, dword ptr [0x4f7ef4]
0x463C4A: call     0x49f204
0x463C4F: mov      dword ptr [ebp - 0x14], eax
0x463C52: cmp      dword ptr [ebp - 0x14], 0
0x463C56: jle      0x463c8a
0x463C58: mov      ecx, dword ptr [ebp - 0xc]
0x463C5B: mov      edx, dword ptr [ebp - 8]
0x463C5E: mov      eax, dword ptr [0x4f7ef4]
0x463C63: call     0x49f284
0x463C68: test     al, al
0x463C6A: jne      0x463c8a
0x463C6C: mov      eax, dword ptr [ebp - 8]
0x463C6F: push     eax
0x463C70: mov      eax, dword ptr [ebp - 0xc]
0x463C73: push     eax
0x463C74: push     0
0x463C76: mov      ecx, dword ptr [ebp - 0x14]
0x463C79: mov      edx, 0x3ea
0x463C7E: mov      eax, dword ptr [ebp - 4]
0x463C81: call     0x464b18
0x463C86: mov      byte ptr [ebp - 0xd], 1
0x463C8A: mov      al, byte ptr [ebp - 0xd]
0x463C8D: mov      esp, ebp
0x463C8F: pop      ebp
0x463C90: ret      
0x463C91: lea      eax, [eax]
0x463C94: push     ebp
0x463C95: mov      ebp, esp
0x463C97: add      esp, -0xc
0x463C9A: mov      byte ptr [ebp - 5], cl
0x463C9D: mov      dword ptr [ebp - 0xc], edx
0x463CA0: mov      dword ptr [ebp - 4], eax
0x463CA3: mov      eax, dword ptr [ebp + 0xc]
0x463CA6: push     eax
0x463CA7: mov      eax, dword ptr [ebp + 8]
0x463CAA: push     eax
0x463CAB: mov      cl, byte ptr [ebp + 0x10]
0x463CAE: mov      dl, byte ptr [ebp - 5]
0x463CB1: mov      eax, dword ptr [ebp - 4]
0x463CB4: mov      eax, dword ptr [eax + 0x31c]
0x463CBA: call     0x45889c
0x463CBF: test     al, al
0x463CC1: jne      0x463ccd
0x463CC3: mov      dword ptr [0x4f949c], 0xffffffff
0x463CCD: mov      esp, ebp
0x463CCF: pop      ebp
0x463CD0: ret      0xc
0x463CD3: nop      
0x463CD4: push     ebp
0x463CD5: mov      ebp, esp
0x463CD7: add      esp, -0x10
0x463CDA: mov      dword ptr [ebp - 0x10], edx
0x463CDD: mov      dword ptr [ebp - 4], eax
0x463CE0: lea      eax, [ebp - 0xc]
0x463CE3: push     eax
0x463CE4: call     0x4065fc
0x463CE9: mov      ecx, dword ptr [ebp - 8]
0x463CEC: mov      edx, dword ptr [ebp - 0xc]
0x463CEF: mov      eax, dword ptr [ebp - 4]
0x463CF2: mov      eax, dword ptr [eax + 0x31c]
0x463CF8: call     0x458ac0
0x463CFD: test     al, al
0x463CFF: mov      esp, ebp
0x463D01: pop      ebp
0x463D02: ret      
0x463D03: nop      
0x463D04: push     ebp
0x463D05: mov      ebp, esp
0x463D07: add      esp, 0xfffffeec
0x463D0D: mov      dword ptr [ebp - 0x14], edx
0x463D10: mov      dword ptr [ebp - 4], eax
0x463D13: lea      eax, [ebp - 0xc]
0x463D16: push     eax
0x463D17: call     0x4065fc
0x463D1C: mov      eax, dword ptr [ebp - 8]
0x463D1F: push     eax
0x463D20: mov      eax, dword ptr [ebp - 0xc]
0x463D23: push     eax
0x463D24: call     0x40686c
0x463D29: cmp      dword ptr [0x4f94a4], 0
0x463D30: je       0x463e34
0x463D36: mov      eax, dword ptr [ebp - 4]
0x463D39: cmp      word ptr [eax + 0x3f0], 0
0x463D41: jbe      0x463d50
0x463D43: mov      eax, dword ptr [ebp - 4]
0x463D46: call     0x461850
0x463D4B: jmp      0x463e34
0x463D50: mov      eax, dword ptr [0x4f94a4]
0x463D55: cmp      byte ptr [eax + 0x20], 0
0x463D59: jne      0x463e2d
0x463D5F: mov      edx, dword ptr [0x4f94a4]
0x463D65: mov      eax, dword ptr [0x4f7d9c]
0x463D6A: call     0x472ec0
0x463D6F: test     al, al
0x463D71: je       0x463e2d
0x463D77: lea      eax, [ebp - 0x114]
0x463D7D: xor      ecx, ecx
0x463D7F: mov      edx, 0x100
0x463D84: call     0x402b54
0x463D89: lea      eax, [ebp - 0x114]
0x463D8F: push     eax
0x463D90: call     0x406654
0x463D95: test     eax, eax
0x463D97: je       0x463e34
0x463D9D: mov      al, byte ptr [0x463e98]
0x463DA2: mov      byte ptr [ebp - 0xd], al
0x463DA5: test     byte ptr [ebp - 0x104], 0x80
0x463DAC: je       0x463db9
0x463DAE: mov      al, byte ptr [0x463e9c]
0x463DB3: or       al, byte ptr [ebp - 0xd]
0x463DB6: mov      byte ptr [ebp - 0xd], al
0x463DB9: mov      eax, dword ptr [0x4f94a4]
0x463DBE: cmp      byte ptr [eax + 0xe], 0
0x463DC2: je       0x463dfb
0x463DC4: mov      eax, dword ptr [0x4f94a4]
0x463DC9: cmp      byte ptr [eax + 0xe], 0xc
0x463DCD: je       0x463dfb
0x463DCF: mov      eax, dword ptr [0x4f94a4]
0x463DD4: cmp      byte ptr [eax + 0xe], 0x18
0x463DD8: je       0x463dfb
0x463DDA: mov      eax, dword ptr [0x4f94a4]
0x463DDF: cmp      byte ptr [eax + 0xe], 0x32
0x463DE3: je       0x463dfb
0x463DE5: mov      eax, dword ptr [0x4f94a4]
0x463DEA: mov      edx, dword ptr [eax + 0x28]
0x463DED: mov      eax, 0x463ea8 ; "("
0x463DF2: call     0x403f60
0x463DF7: test     eax, eax
0x463DF9: je       0x463e1d
0x463DFB: mov      eax, dword ptr [0x4f94a4]
0x463E00: cmp      dword ptr [eax + 0x2c], 0x45
0x463E04: je       0x463e1d
0x463E06: test     byte ptr [ebp - 0xd], 1
0x463E0A: je       0x463e34
0x463E0C: mov      eax, dword ptr [0x4f7d9c]
0x463E11: mov      eax, dword ptr [eax + 0x5a850]
0x463E17: cmp      byte ptr [eax + 0x47], 0
0x463E1B: jne      0x463e34
0x463E1D: mov      edx, dword ptr [0x4f94a4]
0x463E23: mov      eax, dword ptr [ebp - 4]
0x463E26: call     0x463354
0x463E2B: jmp      0x463e34
0x463E2D: xor      eax, eax
0x463E2F: mov      dword ptr [0x4f94a4], eax
0x463E34: cmp      byte ptr [0x4f94b0], 0
0x463E3B: je       0x463e94
0x463E3D: mov      eax, dword ptr [ebp - 4]
0x463E40: call     0x466f60
0x463E45: test     al, al
0x463E47: je       0x463e94
0x463E49: mov      eax, dword ptr [ebp - 4]
0x463E4C: call     0x466f1c
0x463E51: test     al, al
0x463E53: je       0x463e94
0x463E55: mov      eax, dword ptr [ebp - 4]
0x463E58: call     0x466fbc
0x463E5D: test     al, al
0x463E5F: je       0x463e94
0x463E61: mov      eax, dword ptr [0x4f7ef8]
0x463E66: movzx    eax, word ptr [eax + 0xa]
0x463E6A: push     eax
0x463E6B: mov      eax, dword ptr [0x4f7ef8]
0x463E70: movzx    eax, byte ptr [eax + 0xc]
0x463E74: push     eax
0x463E75: push     0
0x463E77: push     0
0x463E79: push     0
0x463E7B: push     0
0x463E7D: mov      eax, dword ptr [0x4f7ef8]
0x463E82: movzx    ecx, word ptr [eax + 8]
0x463E86: mov      dx, 0xbc7
0x463E8A: mov      eax, dword ptr [0x4f7ef8]
0x463E8F: call     0x496548
0x463E94: mov      esp, ebp
0x463E96: pop      ebp
0x463E97: ret      
0x463E98: add      byte ptr [eax], al
0x463E9A: add      byte ptr [eax], al
0x463E9C: add      dword ptr [eax], eax
0x463E9E: add      byte ptr [eax], al
0x463EA8: [STR len=1] "("
0x463EAC: push     ebp
0x463EAD: mov      ebp, esp
0x463EAF: add      esp, -8
0x463EB2: mov      dword ptr [ebp - 8], edx
0x463EB5: mov      dword ptr [ebp - 4], eax
0x463EB8: cmp      dword ptr [0x4f7ef8], 0
0x463EBF: je       0x463fbc
0x463EC5: mov      eax, dword ptr [0x4f7ef8]
0x463ECA: call     0x497524
0x463ECF: test     al, al
0x463ED1: je       0x463fbc
0x463ED7: xor      edx, edx
0x463ED9: mov      eax, dword ptr [ebp - 4]
0x463EDC: mov      eax, dword ptr [eax + 0x334]
0x463EE2: call     0x4455d4
0x463EE7: mov      eax, dword ptr [ebp - 4]
0x463EEA: mov      ax, word ptr [eax + 0x400]
0x463EF1: sub      ax, 0x27a
0x463EF5: jne      0x463fbc
0x463EFB: mov      byte ptr [0x4f9479], 0
0x463F02: mov      byte ptr [0x4f9478], 0
0x463F09: cmp      dword ptr [0x4f94e0], -1
0x463F10: je       0x463f28
0x463F12: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x463F17: mov      eax, dword ptr [eax]
0x463F19: call     0x48e9d0
0x463F1E: mov      dword ptr [0x4f94e0], 0xffffffff
0x463F28: mov      eax, dword ptr [ebp - 4]
0x463F2B: call     0x46431c
0x463F30: mov      eax, dword ptr [0x4f7d9c]
0x463F35: call     0x46f350
0x463F3A: mov      eax, 0x4f7ee8
0x463F3F: call     0x403a58
0x463F44: mov      eax, dword ptr [ebp - 4]
0x463F47: movzx    eax, word ptr [eax + 0x402]
0x463F4E: push     eax
0x463F4F: mov      eax, dword ptr [ebp - 4]
0x463F52: movzx    eax, word ptr [eax + 0x404]
0x463F59: push     eax
0x463F5A: mov      eax, dword ptr [ebp - 4]
0x463F5D: movzx    eax, word ptr [eax + 0x406]
0x463F64: push     eax
0x463F65: push     0
0x463F67: push     0
0x463F69: push     0
0x463F6B: mov      eax, dword ptr [ebp - 4]
0x463F6E: mov      eax, dword ptr [eax + 0x408]
0x463F74: push     eax
0x463F75: xor      ecx, ecx
0x463F77: mov      edx, 0x27a
0x463F7C: mov      eax, dword ptr [0x4f7d9c]
0x463F81: call     0x4738f0
0x463F86: mov      eax, dword ptr [ebp - 4]
0x463F89: movzx    ecx, word ptr [eax + 0x404]
0x463F90: mov      eax, dword ptr [ebp - 4]
0x463F93: movzx    edx, word ptr [eax + 0x402]
0x463F9A: mov      eax, dword ptr [0x4f7ef8]
0x463F9F: call     0x49a028
0x463FA4: mov      dword ptr [0x4f949c], 0xffffffff
0x463FAE: xor      eax, eax
0x463FB0: mov      dword ptr [0x4f94a4], eax
0x463FB5: xor      eax, eax
0x463FB7: mov      dword ptr [0x4f94a8], eax
0x463FBC: pop      ecx
0x463FBD: pop      ecx
0x463FBE: pop      ebp
0x463FBF: ret      
0x463FC0: push     ebp
0x463FC1: mov      ebp, esp
0x463FC3: add      esp, -8
0x463FC6: mov      dword ptr [ebp - 8], edx
0x463FC9: mov      dword ptr [ebp - 4], eax
0x463FCC: xor      edx, edx
0x463FCE: mov      eax, dword ptr [ebp - 4]
0x463FD1: mov      eax, dword ptr [eax + 0x338]
0x463FD7: call     0x4455d4
0x463FDC: mov      eax, dword ptr [ebp - 4]
0x463FDF: call     0x465168
0x463FE4: pop      ecx
0x463FE5: pop      ecx
0x463FE6: pop      ebp
0x463FE7: ret      
0x463FE8: push     ebp
0x463FE9: mov      ebp, esp
0x463FEB: add      esp, -8
0x463FEE: mov      byte ptr [ebp - 5], dl
0x463FF1: mov      dword ptr [ebp - 4], eax
0x463FF4: mov      dl, 1
0x463FF6: mov      eax, dword ptr [ebp - 4]
0x463FF9: mov      eax, dword ptr [eax + 0x340]
0x463FFF: call     0x4455d4
0x464004: mov      al, byte ptr [ebp - 5]
0x464007: mov      edx, dword ptr [ebp - 4]
0x46400A: mov      byte ptr [edx + 0x3d0], al
0x464010: pop      ecx
0x464011: pop      ecx
0x464012: pop      ebp
0x464013: ret      
0x464014: push     ebp
0x464015: mov      ebp, esp
0x464017: add      esp, -0x14
0x46401A: xor      ecx, ecx
0x46401C: mov      dword ptr [ebp - 0x10], ecx
0x46401F: mov      dword ptr [ebp - 0x14], ecx
0x464022: mov      dword ptr [ebp - 0xc], edx
0x464025: mov      dword ptr [ebp - 4], eax
0x464028: xor      eax, eax
0x46402A: push     ebp
0x46402B: push     0x4641a3
0x464030: push     dword ptr fs:[eax]
0x464033: mov      dword ptr fs:[eax], esp
0x464036: xor      edx, edx
0x464038: mov      eax, dword ptr [ebp - 4]
0x46403B: mov      eax, dword ptr [eax + 0x340]
0x464041: call     0x4455d4
0x464046: mov      edx, 0x3e8
0x46404B: mov      eax, dword ptr [ebp - 4]
0x46404E: mov      eax, dword ptr [eax + 0x340]
0x464054: call     0x4455e4
0x464059: mov      eax, dword ptr [ebp - 4]
0x46405C: mov      al, byte ptr [eax + 0x3d0]
0x464062: sub      al, 1
0x464064: jb       0x464075
0x464066: je       0x46409e
0x464068: dec      al
0x46406A: je       0x464180
0x464070: jmp      0x464188
0x464075: xor      edx, edx
0x464077: mov      eax, dword ptr [ebp - 4]
0x46407A: mov      eax, dword ptr [eax + 0x340]
0x464080: call     0x4455d4
0x464085: mov      eax, dword ptr [ebp - 4]
0x464088: mov      eax, dword ptr [eax + 0x30c]
0x46408E: mov      eax, dword ptr [eax + 0x80]
0x464094: call     0x459aa8
0x464099: jmp      0x464188
0x46409E: mov      eax, dword ptr [ebp - 4]
0x4640A1: call     0x464370
0x4640A6: mov      dl, 3
0x4640A8: mov      eax, dword ptr [0x4f7d8c]
0x4640AD: call     0x4799ac
0x4640B2: mov      byte ptr [0x4f937f], 2
0x4640B9: cmp      byte ptr [0x4f937c], 0
0x4640C0: jne      0x464103
0x4640C2: mov      eax, dword ptr [ebp - 4]
0x4640C5: mov      eax, dword ptr [eax + 0x30c]
0x4640CB: mov      dword ptr [ebp - 8], eax
0x4640CE: xor      edx, edx
0x4640D0: mov      eax, dword ptr [ebp - 8]
0x4640D3: call     0x45a950
0x4640D8: mov      edx, dword ptr [0x4f94cc]
0x4640DE: mov      eax, dword ptr [ebp - 8]
0x4640E1: call     0x45a9ec
0x4640E6: mov      edx, dword ptr [0x4f94d0]
0x4640EC: mov      eax, dword ptr [ebp - 8]
0x4640EF: call     0x45ab2c
0x4640F4: mov      dl, 1
0x4640F6: mov      eax, dword ptr [ebp - 8]
0x4640F9: call     0x45a950
0x4640FE: jmp      0x464188
0x464103: mov      eax, dword ptr [ebp - 4]
0x464106: mov      eax, dword ptr [eax + 0x30c]
0x46410C: mov      eax, dword ptr [eax + 0x80]
0x464112: cmp      byte ptr [eax + 8], 0
0x464116: je       0x464161
0x464118: push     0x4641b8 ; "$S"
0x46411D: push     dword ptr [0x4f94cc]
0x464123: push     0x4641c4 ; "/"
0x464128: lea      edx, [ebp - 0x14]
0x46412B: mov      eax, dword ptr [0x4f94d0]
0x464130: call     0x407d64
0x464135: push     dword ptr [ebp - 0x14]
0x464138: push     0x4641d0 ; "%"
0x46413D: lea      eax, [ebp - 0x10]
0x464140: mov      edx, 5
0x464145: call     0x403d94
0x46414A: mov      edx, dword ptr [ebp - 0x10]
0x46414D: mov      eax, dword ptr [ebp - 4]
0x464150: mov      eax, dword ptr [eax + 0x30c]
0x464156: mov      eax, dword ptr [eax + 0x80]
0x46415C: call     0x45a204
0x464161: mov      edx, 1
0x464166: mov      eax, dword ptr [ebp - 4]
0x464169: mov      eax, dword ptr [eax + 0x340]
0x46416F: call     0x4455e4
0x464174: mov      dl, 2
0x464176: mov      eax, dword ptr [ebp - 4]
0x464179: call     0x463fe8
0x46417E: jmp      0x464188
0x464180: mov      eax, dword ptr [ebp - 4]
0x464183: call     0x465168
0x464188: xor      eax, eax
0x46418A: pop      edx
0x46418B: pop      ecx
0x46418C: pop      ecx
0x46418D: mov      dword ptr fs:[eax], edx
0x464190: push     0x4641aa
0x464195: lea      eax, [ebp - 0x14]
0x464198: mov      edx, 2
0x46419D: call     0x403a7c
0x4641A2: ret      
0x4641A3: jmp      0x403518
0x4641A8: jmp      0x464195
0x4641AA: mov      esp, ebp
0x4641AC: pop      ebp
0x4641AD: ret      
0x4641AE: add      byte ptr [eax], al
0x4641B8: [STR len=2] "$S"
0x4641C4: [STR len=1] "/"
0x4641D0: [STR len=1] "%"
0x4641D4: push     ebp
0x4641D5: mov      ebp, esp
0x4641D7: push     ecx
0x4641D8: mov      dword ptr [ebp - 4], eax
0x4641DB: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x4641E0: mov      eax, dword ptr [eax]
0x4641E2: mov      eax, dword ptr [eax + 0x2cc]
0x4641E8: xor      edx, edx
0x4641EA: call     0x458214
0x4641EF: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x4641F4: mov      eax, dword ptr [eax]
0x4641F6: mov      eax, dword ptr [eax + 0x368]
0x4641FC: xor      edx, edx
0x4641FE: call     0x458214
0x464203: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x464208: mov      eax, dword ptr [eax]
0x46420A: mov      eax, dword ptr [eax + 0x2c4]
0x464210: xor      edx, edx
0x464212: call     0x458214
0x464217: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46421C: mov      eax, dword ptr [eax]
0x46421E: mov      eax, dword ptr [eax + 0x3c8]
0x464224: xor      edx, edx
0x464226: call     0x458214
0x46422B: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x464230: mov      eax, dword ptr [eax]
0x464232: mov      eax, dword ptr [eax + 0x3e4]
0x464238: xor      edx, edx
0x46423A: call     0x458214
0x46423F: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x464244: mov      eax, dword ptr [eax]
0x464246: mov      eax, dword ptr [eax + 0x3d0]
0x46424C: xor      edx, edx
0x46424E: call     0x458214
0x464253: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x464258: mov      eax, dword ptr [eax]
0x46425A: mov      eax, dword ptr [eax + 0x408]
0x464260: xor      edx, edx
0x464262: call     0x458214
0x464267: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46426C: mov      eax, dword ptr [eax]
0x46426E: mov      eax, dword ptr [eax + 0x434]
0x464274: xor      edx, edx
0x464276: call     0x458214
0x46427B: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x464280: mov      eax, dword ptr [eax]
0x464282: mov      eax, dword ptr [eax + 0x464]
0x464288: xor      edx, edx
0x46428A: call     0x458214
0x46428F: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x464294: mov      eax, dword ptr [eax]
0x464296: mov      eax, dword ptr [eax + 0x460]
0x46429C: xor      edx, edx
0x46429E: call     0x458214
0x4642A3: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x4642A8: mov      eax, dword ptr [eax]
0x4642AA: mov      eax, dword ptr [eax + 0x4c4]
0x4642B0: xor      edx, edx
0x4642B2: call     0x458214
0x4642B7: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x4642BC: mov      eax, dword ptr [eax]
0x4642BE: mov      eax, dword ptr [eax + 0x500]
0x4642C4: xor      edx, edx
0x4642C6: call     0x458214
0x4642CB: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x4642D0: mov      eax, dword ptr [eax]
0x4642D2: mov      eax, dword ptr [eax + 0x490]
0x4642D8: xor      edx, edx
0x4642DA: call     0x458214
0x4642DF: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x4642E4: mov      eax, dword ptr [eax]
0x4642E6: mov      eax, dword ptr [eax + 0x518]
0x4642EC: xor      edx, edx
0x4642EE: call     0x458214
0x4642F3: cmp      dword ptr [0x4f94e0], -1
0x4642FA: je       0x464312
0x4642FC: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x464301: mov      eax, dword ptr [eax]
0x464303: call     0x48e9d0
0x464308: mov      dword ptr [0x4f94e0], 0xffffffff
0x464312: mov      byte ptr [0x4f939c], 0
0x464319: pop      ecx
0x46431A: pop      ebp
0x46431B: ret      
0x46431C: push     ebp
0x46431D: mov      ebp, esp
0x46431F: add      esp, -0xc
0x464322: push     ebx
0x464323: mov      dword ptr [ebp - 4], eax
0x464326: mov      eax, dword ptr [0x4f7da4]
0x46432B: mov      eax, dword ptr [eax + 8]
0x46432E: dec      eax
0x46432F: test     eax, eax
0x464331: jl       0x46435d
0x464333: inc      eax
0x464334: mov      dword ptr [ebp - 0xc], eax
0x464337: mov      dword ptr [ebp - 8], 0
0x46433E: mov      edx, dword ptr [ebp - 8]
0x464341: mov      eax, dword ptr [0x4f7da4]
0x464346: call     0x40f6a0
0x46434B: mov      edx, 0x2c
0x464350: call     0x4026cc
0x464355: inc      dword ptr [ebp - 8]
0x464358: dec      dword ptr [ebp - 0xc]
0x46435B: jne      0x46433e
0x46435D: mov      eax, dword ptr [0x4f7da4]
0x464362: mov      bx, 0xffff
0x464366: call     0x403080
0x46436B: pop      ebx
0x46436C: mov      esp, ebp
0x46436E: pop      ebp
0x46436F: ret      
0x464370: push     ebp
0x464371: mov      ebp, esp
0x464373: add      esp, -0xc
0x464376: push     ebx
0x464377: mov      dword ptr [ebp - 4], eax
0x46437A: mov      eax, dword ptr [ebp - 4]
0x46437D: call     0x4641d4
0x464382: mov      eax, dword ptr [ebp - 4]
0x464385: call     0x46431c
0x46438A: mov      eax, dword ptr [0x4f9190]
0x46438F: mov      eax, dword ptr [eax + 8]
0x464392: dec      eax
0x464393: test     eax, eax
0x464395: jl       0x4643c1
0x464397: inc      eax
0x464398: mov      dword ptr [ebp - 0xc], eax
0x46439B: mov      dword ptr [ebp - 8], 0
0x4643A2: mov      edx, dword ptr [ebp - 8]
0x4643A5: mov      eax, dword ptr [0x4f9190]
0x4643AA: call     0x40f6a0
0x4643AF: mov      edx, 0x54
0x4643B4: call     0x4026cc
0x4643B9: inc      dword ptr [ebp - 8]
0x4643BC: dec      dword ptr [ebp - 0xc]
0x4643BF: jne      0x4643a2
0x4643C1: mov      eax, dword ptr [0x4f9190]
0x4643C6: mov      bx, 0xffff
0x4643CA: call     0x403080
0x4643CF: mov      byte ptr [0x4f939c], 0
0x4643D6: mov      byte ptr [0x4f93dc], 0
0x4643DD: mov      byte ptr [0x4f9410], 0
0x4643E4: mov      byte ptr [0x4f93a4], 0
0x4643EB: mov      dword ptr [0x4f949c], 0xffffffff
0x4643F5: xor      eax, eax
0x4643F7: mov      dword ptr [0x4f94a4], eax
0x4643FC: xor      eax, eax
0x4643FE: mov      dword ptr [0x4f94a8], eax
0x464403: xor      eax, eax
0x464405: mov      dword ptr [0x4f94ac], eax
0x46440A: mov      eax, dword ptr [ebp - 4]
0x46440D: mov      byte ptr [eax + 0x420], 0
0x464414: mov      eax, dword ptr [0x4f94fc]
0x464419: mov      edx, dword ptr [eax]
0x46441B: call     dword ptr [edx + 0x40]
0x46441E: mov      eax, 0x4f7ef0
0x464423: call     0x403a58
0x464428: mov      eax, 0x4f7eec
0x46442D: call     0x403a58
0x464432: mov      byte ptr [0x4f9478], 0
0x464439: mov      eax, dword ptr [ebp - 4]
0x46443C: mov      eax, dword ptr [eax + 0x334]
0x464442: xor      edx, edx
0x464444: call     0x4455d4
0x464449: mov      byte ptr [0x4f9479], 0
0x464450: mov      eax, dword ptr [0x4f7d8c]
0x464455: xor      edx, edx
0x464457: mov      dword ptr [eax + 0x20], edx
0x46445A: mov      byte ptr [0x4f955c], 0
0x464461: mov      byte ptr [0x4f955d], 0
0x464468: mov      byte ptr [0x4f955e], 0
0x46446F: mov      byte ptr [0x4f955f], 0
0x464476: mov      eax, 0x4f7efc
0x46447B: xor      ecx, ecx
0x46447D: mov      edx, 0x1d4
0x464482: call     0x402b54
0x464487: mov      eax, 0x4f80d0
0x46448C: xor      ecx, ecx
0x46448E: mov      edx, 0xa90
0x464493: call     0x402b54
0x464498: mov      eax, dword ptr [0x4f7d98]
0x46449D: add      eax, 0x18
0x4644A0: xor      ecx, ecx
0x4644A2: mov      edx, 0x70
0x4644A7: call     0x402b54
0x4644AC: mov      eax, dword ptr [0x4f7d98]
0x4644B1: mov      byte ptr [eax + 0x35], 1
0x4644B5: mov      eax, dword ptr [0x4f7d98]
0x4644BA: mov      byte ptr [eax + 0x6d], 1
0x4644BE: mov      eax, dword ptr [0x4f7d9c]
0x4644C3: call     0x4735dc
0x4644C8: mov      eax, dword ptr [ebp - 4]
0x4644CB: call     0x46431c
0x4644D0: mov      eax, dword ptr [0x4f7dcc]
0x4644D5: call     0x47779c
0x4644DA: mov      eax, dword ptr [0x4f7d9c]
0x4644DF: call     0x46f350
0x4644E4: xor      eax, eax
0x4644E6: mov      dword ptr [0x4f7ef8], eax
0x4644EB: pop      ebx
0x4644EC: mov      esp, ebp
0x4644EE: pop      ebp
0x4644EF: ret      
0x4644F0: push     ebp
0x4644F1: mov      ebp, esp
0x4644F3: add      esp, -0xc
0x4644F6: push     ebx
0x4644F7: mov      dword ptr [ebp - 4], eax
0x4644FA: mov      eax, dword ptr [ebp - 4]
0x4644FD: call     0x4641d4
0x464502: mov      eax, dword ptr [ebp - 4]
0x464505: call     0x46431c
0x46450A: mov      eax, dword ptr [0x4f9190]
0x46450F: mov      eax, dword ptr [eax + 8]
0x464512: dec      eax
0x464513: test     eax, eax
0x464515: jl       0x464541
0x464517: inc      eax
0x464518: mov      dword ptr [ebp - 0xc], eax
0x46451B: mov      dword ptr [ebp - 8], 0
0x464522: mov      edx, dword ptr [ebp - 8]
0x464525: mov      eax, dword ptr [0x4f9190]
0x46452A: call     0x40f6a0
0x46452F: mov      edx, 0x54
0x464534: call     0x4026cc
0x464539: inc      dword ptr [ebp - 8]
0x46453C: dec      dword ptr [ebp - 0xc]
0x46453F: jne      0x464522
0x464541: mov      eax, dword ptr [0x4f9190]
0x464546: mov      bx, 0xffff
0x46454A: call     0x403080
0x46454F: mov      byte ptr [0x4f939c], 0
0x464556: mov      byte ptr [0x4f93dc], 0
0x46455D: mov      byte ptr [0x4f9410], 0
0x464564: mov      dword ptr [0x4f949c], 0xffffffff
0x46456E: xor      eax, eax
0x464570: mov      dword ptr [0x4f94a4], eax
0x464575: xor      eax, eax
0x464577: mov      dword ptr [0x4f94a8], eax
0x46457C: xor      eax, eax
0x46457E: mov      dword ptr [0x4f94ac], eax
0x464583: mov      eax, dword ptr [ebp - 4]
0x464586: mov      byte ptr [eax + 0x420], 0
0x46458D: mov      eax, dword ptr [0x4f94fc]
0x464592: mov      edx, dword ptr [eax]
0x464594: call     dword ptr [edx + 0x40]
0x464597: mov      eax, 0x4f7ef0
0x46459C: call     0x403a58
0x4645A1: mov      eax, 0x4f7eec
0x4645A6: call     0x403a58
0x4645AB: mov      byte ptr [0x4f9478], 0
0x4645B2: mov      eax, dword ptr [ebp - 4]
0x4645B5: mov      eax, dword ptr [eax + 0x334]
0x4645BB: xor      edx, edx
0x4645BD: call     0x4455d4
0x4645C2: mov      byte ptr [0x4f9479], 0
0x4645C9: mov      byte ptr [0x4f955c], 0
0x4645D0: mov      byte ptr [0x4f955d], 0
0x4645D7: mov      byte ptr [0x4f955e], 0
0x4645DE: mov      eax, dword ptr [ebp - 4]
0x4645E1: call     0x46431c
0x4645E6: mov      eax, dword ptr [0x4f7dcc]
0x4645EB: call     0x47779c
0x4645F0: mov      eax, dword ptr [0x4f7d9c]
0x4645F5: call     0x46f350
0x4645FA: pop      ebx
0x4645FB: mov      esp, ebp
0x4645FD: pop      ebp
0x4645FE: ret      
0x4645FF: nop      
0x464600: push     ebp
0x464601: mov      ebp, esp
0x464603: add      esp, 0xfffffdf0
0x464609: push     ebx
0x46460A: xor      ebx, ebx
0x46460C: mov      dword ptr [ebp - 0xc], ebx
0x46460F: mov      dword ptr [ebp - 8], ecx
0x464612: mov      dword ptr [ebp - 0x10], edx
0x464615: mov      dword ptr [ebp - 4], eax
0x464618: xor      eax, eax
0x46461A: push     ebp
0x46461B: push     0x464878
0x464620: push     dword ptr fs:[eax]
0x464623: mov      dword ptr fs:[eax], esp
0x464626: mov      byte ptr [0x4f9381], 1
0x46462D: cmp      byte ptr [0x4f937f], 0
0x464634: jne      0x4647c0
0x46463A: cmp      byte ptr [0x4f937d], 1
0x464641: jne      0x4647b4
0x464647: lea      eax, [ebp - 0x110]
0x46464D: xor      ecx, ecx
0x46464F: mov      edx, 0x100
0x464654: call     0x402b54
0x464659: lea      eax, [ebp - 0xc]
0x46465C: mov      edx, 0x46488c ; "KwGwMGS"
0x464661: call     0x403af0
0x464666: lea      eax, [ebp - 0x210]
0x46466C: mov      edx, dword ptr [ebp - 0xc]
0x46466F: call     0x40841c
0x464674: mov      eax, dword ptr [ebp - 0xc]
0x464677: call     0x403cd4
0x46467C: mov      ecx, eax
0x46467E: lea      edx, [ebp - 0x110]
0x464684: lea      eax, [ebp - 0x210]
0x46468A: call     0x4027cc
0x46468F: lea      eax, [ebp - 0xc]
0x464692: mov      edx, 0x46489c ; "CONNECT"
0x464697: call     0x403af0
0x46469C: lea      eax, [ebp - 0x210]
0x4646A2: mov      edx, dword ptr [ebp - 0xc]
0x4646A5: call     0x40841c
0x4646AA: mov      eax, dword ptr [ebp - 0xc]
0x4646AD: call     0x403cd4
0x4646B2: mov      ecx, eax
0x4646B4: lea      edx, [ebp - 0x108]
0x4646BA: lea      eax, [ebp - 0x210]
0x4646C0: call     0x4027cc
0x4646C5: lea      eax, [ebp - 0xc]
0x4646C8: mov      edx, dword ptr [0x4f7ed4]
0x4646CE: call     0x403af0
0x4646D3: lea      eax, [ebp - 0x210]
0x4646D9: mov      edx, dword ptr [ebp - 0xc]
0x4646DC: call     0x40841c
0x4646E1: mov      eax, dword ptr [ebp - 0xc]
0x4646E4: call     0x403cd4
0x4646E9: mov      ecx, eax
0x4646EB: lea      edx, [ebp - 0x100]
0x4646F1: lea      eax, [ebp - 0x210]
0x4646F7: call     0x4027cc
0x4646FC: lea      eax, [ebp - 0xc]
0x4646FF: mov      edx, dword ptr [0x4f7ed8]
0x464705: call     0x403af0
0x46470A: lea      eax, [ebp - 0x210]
0x464710: mov      edx, dword ptr [ebp - 0xc]
0x464713: call     0x40841c
0x464718: mov      eax, dword ptr [ebp - 0xc]
0x46471B: call     0x403cd4
0x464720: mov      ecx, eax
0x464722: lea      edx, [ebp - 0xf0]
0x464728: lea      eax, [ebp - 0x210]
0x46472E: call     0x4027cc
0x464733: lea      eax, [ebp - 0xc]
0x464736: mov      edx, dword ptr [0x4f7edc]
0x46473C: call     0x403af0
0x464741: lea      eax, [ebp - 0x210]
0x464747: mov      edx, dword ptr [ebp - 0xc]
0x46474A: call     0x40841c
0x46474F: mov      eax, dword ptr [ebp - 0xc]
0x464752: call     0x403cd4
0x464757: mov      ecx, eax
0x464759: lea      edx, [ebp - 0xe0]
0x46475F: lea      eax, [ebp - 0x210]
0x464765: call     0x4027cc
0x46476A: lea      eax, [ebp - 0xc]
0x46476D: mov      edx, dword ptr [0x4f7ee0]
0x464773: call     0x403af0
0x464778: lea      eax, [ebp - 0x210]
0x46477E: mov      edx, dword ptr [ebp - 0xc]
0x464781: call     0x40841c
0x464786: mov      eax, dword ptr [ebp - 0xc]
0x464789: call     0x403cd4
0x46478E: mov      ecx, eax
0x464790: lea      edx, [ebp - 0xd0]
0x464796: lea      eax, [ebp - 0x210]
0x46479C: call     0x4027cc
0x4647A1: lea      edx, [ebp - 0x110]
0x4647A7: mov      ecx, 0x100
0x4647AC: mov      eax, dword ptr [ebp - 8]
0x4647AF: call     0x45a3f8
0x4647B4: mov      dl, 1
0x4647B6: mov      eax, dword ptr [0x4f7d8c]
0x4647BB: call     0x4799ac
0x4647C0: cmp      byte ptr [0x4f937f], 1
0x4647C7: jne      0x4647e3
0x4647C9: mov      eax, dword ptr [0x4f7d94]
0x4647CE: call     0x47d69c
0x4647D3: mov      dl, 1
0x4647D5: mov      eax, dword ptr [ebp - 4]
0x4647D8: mov      eax, dword ptr [eax + 0x338]
0x4647DE: call     0x4455d4
0x4647E3: cmp      byte ptr [0x4f937f], 2
0x4647EA: jne      0x464809
0x4647EC: mov      edx, 1
0x4647F1: mov      eax, dword ptr [ebp - 4]
0x4647F4: mov      eax, dword ptr [eax + 0x340]
0x4647FA: call     0x4455e4
0x4647FF: mov      dl, 2
0x464801: mov      eax, dword ptr [ebp - 4]
0x464804: call     0x463fe8
0x464809: cmp      byte ptr [0x4f937f], 3
0x464810: jne      0x464848
0x464812: cmp      byte ptr [0x4f9234], 0
0x464819: jne      0x464838
0x46481B: call     0x49f850
0x464820: mov      eax, dword ptr [0x4f7d8c]
0x464825: call     0x479ed4
0x46482A: mov      dl, 6
0x46482C: mov      eax, dword ptr [0x4f7d8c]
0x464831: call     0x4799ac
0x464836: jmp      0x464840
0x464838: mov      eax, dword ptr [ebp - 4]
0x46483B: call     0x4644f0
0x464840: mov      eax, dword ptr [ebp - 4]
0x464843: call     0x4653a8
0x464848: mov      eax, dword ptr [ebp - 4]
0x46484B: add      eax, 0x3c4
0x464850: call     0x403a58
0x464855: mov      eax, dword ptr [ebp - 4]
0x464858: add      eax, 0x3c8
0x46485D: call     0x403a58
0x464862: xor      eax, eax
0x464864: pop      edx
0x464865: pop      ecx
0x464866: pop      ecx
0x464867: mov      dword ptr fs:[eax], edx
0x46486A: push     0x46487f
0x46486F: lea      eax, [ebp - 0xc]
0x464872: call     0x403a58
0x464877: ret      
0x464878: jmp      0x403518
0x46487D: jmp      0x46486f
0x46487F: pop      ebx
0x464880: mov      esp, ebp
0x464882: pop      ebp
0x464883: ret      
0x46488C: [STR len=7] "KwGwMGS"
0x46489C: [STR len=7] "CONNECT"
0x4648A4: push     ebp
0x4648A5: mov      ebp, esp
0x4648A7: add      esp, -0xc
0x4648AA: mov      dword ptr [ebp - 0xc], ecx
0x4648AD: mov      dword ptr [ebp - 8], edx
0x4648B0: mov      dword ptr [ebp - 4], eax
0x4648B3: mov      byte ptr [0x4f9381], 0
0x4648BA: cmp      byte ptr [0x4f937f], 0
0x4648C1: jne      0x4648ec
0x4648C3: cmp      byte ptr [0x4f9380], 0
0x4648CA: jne      0x4648ec
0x4648CC: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x4648D1: mov      eax, dword ptr [eax]
0x4648D3: mov      cx, word ptr [0x46490c]
0x4648DA: mov      edx, 0x464918 ; "Connection closed..."
0x4648DF: call     0x486df0
0x4648E4: mov      eax, dword ptr [ebp - 4]
0x4648E7: call     0x4405bc
0x4648EC: cmp      byte ptr [0x4f94c8], 0
0x4648F3: je       0x464906
0x4648F5: mov      byte ptr [0x4f94c8], 0
0x4648FC: mov      dl, 1
0x4648FE: mov      eax, dword ptr [ebp - 4]
0x464901: call     0x463fe8
0x464906: mov      esp, ebp
0x464908: pop      ebp
0x464909: ret      
0x46490A: add      byte ptr [eax], al
0x46490C: add      al, 0
0x46490E: add      byte ptr [eax], al
0x464918: [STR len=20] "Connection closed..."
0x464930: push     ebp
0x464931: mov      ebp, esp
0x464933: add      esp, -0xc
0x464936: mov      dword ptr [ebp - 8], ecx
0x464939: mov      dword ptr [ebp - 0xc], edx
0x46493C: mov      dword ptr [ebp - 4], eax
0x46493F: mov      eax, dword ptr [ebp + 8]
0x464942: xor      edx, edx
0x464944: mov      dword ptr [eax], edx
0x464946: mov      eax, dword ptr [ebp - 8]
0x464949: call     0x459aa8
0x46494E: mov      esp, ebp
0x464950: pop      ebp
0x464951: ret      8
0x464954: push     ebp
0x464955: mov      ebp, esp
0x464957: add      esp, -0x1c
0x46495A: push     ebx
0x46495B: xor      ebx, ebx
0x46495D: mov      dword ptr [ebp - 0x1c], ebx
0x464960: mov      dword ptr [ebp - 0x10], ebx
0x464963: mov      dword ptr [ebp - 0x14], ebx
0x464966: mov      dword ptr [ebp - 8], ecx
0x464969: mov      dword ptr [ebp - 0x18], edx
0x46496C: mov      dword ptr [ebp - 4], eax
0x46496F: xor      eax, eax
0x464971: push     ebp
0x464972: push     0x464a27
0x464977: push     dword ptr fs:[eax]
0x46497A: mov      dword ptr fs:[eax], esp
0x46497D: lea      edx, [ebp - 0x10]
0x464980: mov      eax, dword ptr [ebp - 8]
0x464983: call     0x45a748
0x464988: mov      edx, dword ptr [ebp - 0x10]
0x46498B: mov      eax, 0x464a3c ; "*"
0x464990: call     0x403f60
0x464995: mov      dword ptr [ebp - 0xc], eax
0x464998: cmp      dword ptr [ebp - 0xc], 0
0x46499C: jle      0x4649f4
0x46499E: lea      eax, [ebp - 0x14]
0x4649A1: push     eax
0x4649A2: mov      ecx, dword ptr [ebp - 0xc]
0x4649A5: dec      ecx
0x4649A6: mov      edx, 1
0x4649AB: mov      eax, dword ptr [ebp - 0x10]
0x4649AE: call     0x403ed8
0x4649B3: lea      eax, [ebp - 0x1c]
0x4649B6: push     eax
0x4649B7: mov      eax, dword ptr [ebp - 0x10]
0x4649BA: call     0x403cd4
0x4649BF: mov      ecx, eax
0x4649C1: mov      edx, dword ptr [ebp - 0xc]
0x4649C4: inc      edx
0x4649C5: mov      eax, dword ptr [ebp - 0x10]
0x4649C8: call     0x403ed8
0x4649CD: mov      ecx, dword ptr [ebp - 0x1c]
0x4649D0: lea      eax, [ebp - 0x10]
0x4649D3: mov      edx, dword ptr [ebp - 0x14]
0x4649D6: call     0x403d20
0x4649DB: mov      eax, dword ptr [ebp - 4]
0x4649DE: mov      eax, dword ptr [eax + 0x30c]
0x4649E4: mov      eax, dword ptr [eax + 0x80]
0x4649EA: mov      edx, 0x464a3c ; "*"
0x4649EF: call     0x45a204
0x4649F4: mov      eax, dword ptr [ebp - 4]
0x4649F7: add      eax, 0x3c4
0x4649FC: mov      edx, dword ptr [ebp - 0x10]
0x4649FF: call     0x403cdc
0x464A04: xor      eax, eax
0x464A06: pop      edx
0x464A07: pop      ecx
0x464A08: pop      ecx
0x464A09: mov      dword ptr fs:[eax], edx
0x464A0C: push     0x464a2e
0x464A11: lea      eax, [ebp - 0x1c]
0x464A14: call     0x403a58
0x464A19: lea      eax, [ebp - 0x14]
0x464A1C: mov      edx, 2
0x464A21: call     0x403a7c
0x464A26: ret      
0x464A27: jmp      0x403518
0x464A2C: jmp      0x464a11
0x464A2E: pop      ebx
0x464A2F: mov      esp, ebp
0x464A31: pop      ebp
0x464A32: ret      
0x464A33: add      bh, bh
0x464A37: inc      dword ptr [ecx]
0x464A39: add      byte ptr [eax], al
0x464A3B: add      byte ptr [edx], ch
0x464A3D: add      byte ptr [eax], al
0x464A3F: add      byte ptr [ebp - 0x75], dl
0x464A42: in       al, dx
0x464A43: add      esp, -0x10
0x464A46: xor      ecx, ecx
0x464A48: mov      dword ptr [ebp - 0xc], ecx
0x464A4B: mov      dword ptr [ebp - 0x10], ecx
0x464A4E: mov      dword ptr [ebp - 8], edx
0x464A51: mov      dword ptr [ebp - 4], eax
0x464A54: mov      eax, dword ptr [ebp - 8]
0x464A57: call     0x403e88
0x464A5C: xor      eax, eax
0x464A5E: push     ebp
0x464A5F: push     0x464af3
0x464A64: push     dword ptr fs:[eax]
0x464A67: mov      dword ptr fs:[eax], esp
0x464A6A: mov      eax, dword ptr [ebp - 4]
0x464A6D: mov      eax, dword ptr [eax + 0x30c]
0x464A73: mov      eax, dword ptr [eax + 0x80]
0x464A79: cmp      byte ptr [eax + 8], 0
0x464A7D: je       0x464ad8
0x464A7F: push     0x464b08 ; "#"
0x464A84: lea      edx, [ebp - 0x10]
0x464A87: xor      eax, eax
0x464A89: mov      al, byte ptr [0x4a2d14] ; *0x4A2D14->0x408D01
0x464A8E: call     0x407d64
0x464A93: push     dword ptr [ebp - 0x10]
0x464A96: push     dword ptr [ebp - 8]
0x464A99: push     0x464b14 ; "!"
0x464A9E: lea      eax, [ebp - 0xc]
0x464AA1: mov      edx, 4
0x464AA6: call     0x403d94
0x464AAB: mov      edx, dword ptr [ebp - 0xc]
0x464AAE: mov      eax, dword ptr [ebp - 4]
0x464AB1: mov      eax, dword ptr [eax + 0x30c]
0x464AB7: mov      eax, dword ptr [eax + 0x80]
0x464ABD: call     0x45a204
0x464AC2: inc      byte ptr [0x4a2d14] ; *0x4A2D14->0x408D01
0x464AC8: cmp      byte ptr [0x4a2d14], 0xa ; *0x4A2D14->0x408D01
0x464ACF: jb       0x464ad8
0x464AD1: mov      byte ptr [0x4a2d14], 1 ; *0x4A2D14->0x408D01
0x464AD8: xor      eax, eax
0x464ADA: pop      edx
0x464ADB: pop      ecx
0x464ADC: pop      ecx
0x464ADD: mov      dword ptr fs:[eax], edx
0x464AE0: push     0x464afa
0x464AE5: lea      eax, [ebp - 0x10]
0x464AE8: mov      edx, 3
0x464AED: call     0x403a7c
0x464AF2: ret      
0x464AF3: jmp      0x403518
0x464AF8: jmp      0x464ae5
0x464AFA: mov      esp, ebp
0x464AFC: pop      ebp
0x464AFD: ret      
0x464AFE: add      byte ptr [eax], al
0x464B08: [STR len=1] "#"
0x464B14: [STR len=1] "!"
0x464B18: push     ebp
0x464B19: mov      ebp, esp
0x464B1B: add      esp, -0x1c
0x464B1E: push     ebx
0x464B1F: xor      ebx, ebx
0x464B21: mov      dword ptr [ebp - 0x1c], ebx
0x464B24: mov      dword ptr [ebp - 0xc], ecx
0x464B27: mov      dword ptr [ebp - 8], edx
0x464B2A: mov      dword ptr [ebp - 4], eax
0x464B2D: xor      eax, eax
0x464B2F: push     ebp
0x464B30: push     0x464b85
0x464B35: push     dword ptr fs:[eax]
0x464B38: mov      dword ptr fs:[eax], esp
0x464B3B: mov      ax, word ptr [ebp + 0xc]
0x464B3F: push     eax
0x464B40: mov      ax, word ptr [ebp + 8]
0x464B44: push     eax
0x464B45: lea      eax, [ebp - 0x18]
0x464B48: push     eax
0x464B49: mov      cx, word ptr [ebp + 0x10]
0x464B4D: mov      edx, dword ptr [ebp - 0xc]
0x464B50: mov      ax, word ptr [ebp - 8]
0x464B54: call     0x458d3c
0x464B59: lea      edx, [ebp - 0x1c]
0x464B5C: lea      eax, [ebp - 0x18]
0x464B5F: call     0x45b41c
0x464B64: mov      edx, dword ptr [ebp - 0x1c]
0x464B67: mov      eax, dword ptr [ebp - 4]
0x464B6A: call     0x464a40
0x464B6F: xor      eax, eax
0x464B71: pop      edx
0x464B72: pop      ecx
0x464B73: pop      ecx
0x464B74: mov      dword ptr fs:[eax], edx
0x464B77: push     0x464b8c
0x464B7C: lea      eax, [ebp - 0x1c]
0x464B7F: call     0x403a58
0x464B84: ret      
0x464B85: jmp      0x403518
0x464B8A: jmp      0x464b7c
0x464B8C: pop      ebx
0x464B8D: mov      esp, ebp
0x464B8F: pop      ebp
0x464B90: ret      0xc
0x464B93: nop      
0x464B94: push     ebp
0x464B95: mov      ebp, esp
0x464B97: add      esp, -0x24
0x464B9A: push     ebx
0x464B9B: xor      ebx, ebx
0x464B9D: mov      dword ptr [ebp - 0x1c], ebx
0x464BA0: mov      dword ptr [ebp - 0x20], ebx
0x464BA3: mov      dword ptr [ebp - 0x24], ebx
0x464BA6: mov      dword ptr [ebp - 0xc], ecx
0x464BA9: mov      dword ptr [ebp - 8], edx
0x464BAC: mov      dword ptr [ebp - 4], eax
0x464BAF: mov      eax, dword ptr [ebp - 8]
0x464BB2: call     0x403e88
0x464BB7: mov      eax, dword ptr [ebp - 0xc]
0x464BBA: call     0x403e88
0x464BBF: xor      eax, eax
0x464BC1: push     ebp
0x464BC2: push     0x464c77
0x464BC7: push     dword ptr fs:[eax]
0x464BCA: mov      dword ptr fs:[eax], esp
0x464BCD: mov      eax, dword ptr [ebp - 4]
0x464BD0: add      eax, 0x410
0x464BD5: mov      edx, dword ptr [ebp - 8]
0x464BD8: call     0x403aac
0x464BDD: mov      eax, dword ptr [ebp - 4]
0x464BE0: add      eax, 0x414
0x464BE5: mov      edx, dword ptr [ebp - 0xc]
0x464BE8: call     0x403aac
0x464BED: push     0
0x464BEF: push     0
0x464BF1: lea      eax, [ebp - 0x18]
0x464BF4: push     eax
0x464BF5: xor      ecx, ecx
0x464BF7: xor      edx, edx
0x464BF9: mov      ax, 0x7d1
0x464BFD: call     0x458d3c
0x464C02: lea      edx, [ebp - 0x1c]
0x464C05: lea      eax, [ebp - 0x18]
0x464C08: call     0x45b41c
0x464C0D: lea      eax, [ebp - 0x1c]
0x464C10: push     eax
0x464C11: push     dword ptr [ebp - 8]
0x464C14: push     0x464c8c ; "/"
0x464C19: push     dword ptr [ebp - 0xc]
0x464C1C: lea      eax, [ebp - 0x24]
0x464C1F: mov      edx, 3
0x464C24: call     0x403d94
0x464C29: mov      eax, dword ptr [ebp - 0x24]
0x464C2C: lea      edx, [ebp - 0x20]
0x464C2F: call     0x45b4a8
0x464C34: mov      edx, dword ptr [ebp - 0x20]
0x464C37: pop      eax
0x464C38: call     0x403cdc
0x464C3D: mov      edx, dword ptr [ebp - 0x1c]
0x464C40: mov      eax, dword ptr [ebp - 4]
0x464C43: call     0x464a40
0x464C48: mov      byte ptr [0x4f9380], 1
0x464C4F: xor      eax, eax
0x464C51: pop      edx
0x464C52: pop      ecx
0x464C53: pop      ecx
0x464C54: mov      dword ptr fs:[eax], edx
0x464C57: push     0x464c7e
0x464C5C: lea      eax, [ebp - 0x24]
0x464C5F: mov      edx, 3
0x464C64: call     0x403a7c
0x464C69: lea      eax, [ebp - 0xc]
0x464C6C: mov      edx, 2
0x464C71: call     0x403a7c
0x464C76: ret      
0x464C77: jmp      0x403518
0x464C7C: jmp      0x464c5c
0x464C7E: pop      ebx
0x464C7F: mov      esp, ebp
0x464C81: pop      ebp
0x464C82: ret      
0x464C83: add      bh, bh
0x464C87: inc      dword ptr [ecx]
0x464C89: add      byte ptr [eax], al
0x464C8B: add      byte ptr [edi], ch
0x464C8D: add      byte ptr [eax], al
0x464C8F: add      byte ptr [ebp - 0x75], dl
0x464C92: in       al, dx
0x464C93: add      esp, 0xfffffee4
0x464C99: push     ebx
0x464C9A: push     esi
0x464C9B: push     edi
0x464C9C: xor      ebx, ebx
0x464C9E: mov      dword ptr [ebp - 0x110], ebx
0x464CA4: mov      dword ptr [ebp - 0x114], ebx
0x464CAA: mov      dword ptr [ebp - 0x118], ebx
0x464CB0: mov      dword ptr [ebp - 0x11c], ebx
0x464CB6: mov      esi, ecx
0x464CB8: lea      edi, [ebp - 0xfd]
0x464CBE: mov      ecx, 0x19
0x464CC3: rep movsd dword ptr es:[edi], dword ptr [esi]
0x464CC5: movsb    byte ptr es:[edi], byte ptr [esi]
0x464CC6: mov      esi, edx
0x464CC8: lea      edi, [ebp - 0x98]
0x464CCE: mov      ecx, 0x25
0x464CD3: rep movsd dword ptr es:[edi], dword ptr [esi]
0x464CD5: mov      dword ptr [ebp - 4], eax
0x464CD8: xor      eax, eax
0x464CDA: push     ebp
0x464CDB: push     0x464d9c
0x464CE0: push     dword ptr fs:[eax]
0x464CE3: mov      dword ptr fs:[eax], esp
0x464CE6: mov      eax, dword ptr [ebp - 4]
0x464CE9: add      eax, 0x3d4
0x464CEE: lea      edx, [ebp - 0x98]
0x464CF4: call     0x403c78
0x464CF9: push     0
0x464CFB: push     0
0x464CFD: lea      eax, [ebp - 0x10c]
0x464D03: push     eax
0x464D04: xor      ecx, ecx
0x464D06: xor      edx, edx
0x464D08: mov      ax, 0x7d2
0x464D0C: call     0x458d3c
0x464D11: lea      edx, [ebp - 0x114]
0x464D17: lea      eax, [ebp - 0x10c]
0x464D1D: call     0x45b41c
0x464D22: push     dword ptr [ebp - 0x114]
0x464D28: lea      ecx, [ebp - 0x118]
0x464D2E: lea      eax, [ebp - 0x98]
0x464D34: mov      edx, 0x94
0x464D39: call     0x45b554
0x464D3E: push     dword ptr [ebp - 0x118]
0x464D44: lea      ecx, [ebp - 0x11c]
0x464D4A: lea      eax, [ebp - 0xfd]
0x464D50: mov      edx, 0x65
0x464D55: call     0x45b554
0x464D5A: push     dword ptr [ebp - 0x11c]
0x464D60: lea      eax, [ebp - 0x110]
0x464D66: mov      edx, 3
0x464D6B: call     0x403d94
0x464D70: mov      edx, dword ptr [ebp - 0x110]
0x464D76: mov      eax, dword ptr [ebp - 4]
0x464D79: call     0x464a40
0x464D7E: xor      eax, eax
0x464D80: pop      edx
0x464D81: pop      ecx
0x464D82: pop      ecx
0x464D83: mov      dword ptr fs:[eax], edx
0x464D86: push     0x464da3
0x464D8B: lea      eax, [ebp - 0x11c]
0x464D91: mov      edx, 4
0x464D96: call     0x403a7c
0x464D9B: ret      
0x464D9C: jmp      0x403518
0x464DA1: jmp      0x464d8b
0x464DA3: pop      edi
0x464DA4: pop      esi
0x464DA5: pop      ebx
0x464DA6: mov      esp, ebp
0x464DA8: pop      ebp
0x464DA9: ret      
0x464DAA: mov      eax, eax
0x464DAC: push     ebp
0x464DAD: mov      ebp, esp
0x464DAF: add      esp, 0xfffffee4
0x464DB5: push     ebx
0x464DB6: push     esi
0x464DB7: push     edi
0x464DB8: xor      ebx, ebx
0x464DBA: mov      dword ptr [ebp - 0x110], ebx
0x464DC0: mov      dword ptr [ebp - 0x114], ebx
0x464DC6: mov      dword ptr [ebp - 0x118], ebx
0x464DCC: mov      dword ptr [ebp - 0x11c], ebx
0x464DD2: mov      esi, ecx
0x464DD4: lea      edi, [ebp - 0xfd]
0x464DDA: mov      ecx, 0x19
0x464DDF: rep movsd dword ptr es:[edi], dword ptr [esi]
0x464DE1: movsb    byte ptr es:[edi], byte ptr [esi]
0x464DE2: mov      esi, edx
0x464DE4: lea      edi, [ebp - 0x98]
0x464DEA: mov      ecx, 0x25
0x464DEF: rep movsd dword ptr es:[edi], dword ptr [esi]
0x464DF1: mov      dword ptr [ebp - 4], eax
0x464DF4: xor      eax, eax
0x464DF6: push     ebp
0x464DF7: push     0x464eb8
0x464DFC: push     dword ptr fs:[eax]
0x464DFF: mov      dword ptr fs:[eax], esp
0x464E02: mov      eax, dword ptr [ebp - 4]
0x464E05: add      eax, 0x3d4
0x464E0A: lea      edx, [ebp - 0x98]
0x464E10: call     0x403c78
0x464E15: push     0
0x464E17: push     0
0x464E19: lea      eax, [ebp - 0x10c]
0x464E1F: push     eax
0x464E20: xor      ecx, ecx
0x464E22: xor      edx, edx
0x464E24: mov      ax, 0x7d4
0x464E28: call     0x458d3c
0x464E2D: lea      edx, [ebp - 0x114]
0x464E33: lea      eax, [ebp - 0x10c]
0x464E39: call     0x45b41c
0x464E3E: push     dword ptr [ebp - 0x114]
0x464E44: lea      ecx, [ebp - 0x118]
0x464E4A: lea      eax, [ebp - 0x98]
0x464E50: mov      edx, 0x94
0x464E55: call     0x45b554
0x464E5A: push     dword ptr [ebp - 0x118]
0x464E60: lea      ecx, [ebp - 0x11c]
0x464E66: lea      eax, [ebp - 0xfd]
0x464E6C: mov      edx, 0x65
0x464E71: call     0x45b554
0x464E76: push     dword ptr [ebp - 0x11c]
0x464E7C: lea      eax, [ebp - 0x110]
0x464E82: mov      edx, 3
0x464E87: call     0x403d94
0x464E8C: mov      edx, dword ptr [ebp - 0x110]
0x464E92: mov      eax, dword ptr [ebp - 4]
0x464E95: call     0x464a40
0x464E9A: xor      eax, eax
0x464E9C: pop      edx
0x464E9D: pop      ecx
0x464E9E: pop      ecx
0x464E9F: mov      dword ptr fs:[eax], edx
0x464EA2: push     0x464ebf
0x464EA7: lea      eax, [ebp - 0x11c]
0x464EAD: mov      edx, 4
0x464EB2: call     0x403a7c
0x464EB7: ret      
0x464EB8: jmp      0x403518
0x464EBD: jmp      0x464ea7
0x464EBF: pop      edi
0x464EC0: pop      esi
0x464EC1: pop      ebx
0x464EC2: mov      esp, ebp
0x464EC4: pop      ebp
0x464EC5: ret      
0x464EC6: mov      eax, eax
0x464EC8: push     ebp
0x464EC9: mov      ebp, esp
0x464ECB: add      esp, -0x1c
0x464ECE: xor      ecx, ecx
0x464ED0: mov      dword ptr [ebp - 0x18], ecx
0x464ED3: mov      dword ptr [ebp - 0x1c], ecx
0x464ED6: mov      dword ptr [ebp - 8], edx
0x464ED9: mov      dword ptr [ebp - 4], eax
0x464EDC: mov      eax, dword ptr [ebp - 8]
0x464EDF: call     0x403e88
0x464EE4: xor      eax, eax
0x464EE6: push     ebp
0x464EE7: push     0x464f58
0x464EEC: push     dword ptr fs:[eax]
0x464EEF: mov      dword ptr fs:[eax], esp
0x464EF2: push     0
0x464EF4: push     0
0x464EF6: lea      eax, [ebp - 0x14]
0x464EF9: push     eax
0x464EFA: xor      ecx, ecx
0x464EFC: xor      edx, edx
0x464EFE: mov      ax, 0x68
0x464F02: call     0x458d3c
0x464F07: lea      edx, [ebp - 0x18]
0x464F0A: lea      eax, [ebp - 0x14]
0x464F0D: call     0x45b41c
0x464F12: lea      eax, [ebp - 0x18]
0x464F15: push     eax
0x464F16: lea      edx, [ebp - 0x1c]
0x464F19: mov      eax, dword ptr [ebp - 8]
0x464F1C: call     0x45b4a8
0x464F21: mov      edx, dword ptr [ebp - 0x1c]
0x464F24: pop      eax
0x464F25: call     0x403cdc
0x464F2A: mov      edx, dword ptr [ebp - 0x18]
0x464F2D: mov      eax, dword ptr [ebp - 4]
0x464F30: call     0x464a40
0x464F35: xor      eax, eax
0x464F37: pop      edx
0x464F38: pop      ecx
0x464F39: pop      ecx
0x464F3A: mov      dword ptr fs:[eax], edx
0x464F3D: push     0x464f5f
0x464F42: lea      eax, [ebp - 0x1c]
0x464F45: mov      edx, 2
0x464F4A: call     0x403a7c
0x464F4F: lea      eax, [ebp - 8]
0x464F52: call     0x403a58
0x464F57: ret      
0x464F58: jmp      0x403518
0x464F5D: jmp      0x464f42
0x464F5F: mov      esp, ebp
0x464F61: pop      ebp
0x464F62: ret      
0x464F63: nop      
0x464F64: push     ebp
0x464F65: mov      ebp, esp
0x464F67: add      esp, -0x24
0x464F6A: push     ebx
0x464F6B: xor      ebx, ebx
0x464F6D: mov      dword ptr [ebp - 0x1c], ebx
0x464F70: mov      dword ptr [ebp - 0x20], ebx
0x464F73: mov      dword ptr [ebp - 0x24], ebx
0x464F76: mov      dword ptr [ebp - 0xc], ecx
0x464F79: mov      dword ptr [ebp - 8], edx
0x464F7C: mov      dword ptr [ebp - 4], eax
0x464F7F: mov      eax, dword ptr [ebp - 8]
0x464F82: call     0x403e88
0x464F87: mov      eax, dword ptr [ebp - 0xc]
0x464F8A: call     0x403e88
0x464F8F: mov      eax, dword ptr [ebp + 8]
0x464F92: call     0x403e88
0x464F97: xor      eax, eax
0x464F99: push     ebp
0x464F9A: push     0x465038
0x464F9F: push     dword ptr fs:[eax]
0x464FA2: mov      dword ptr fs:[eax], esp
0x464FA5: push     0
0x464FA7: push     0
0x464FA9: lea      eax, [ebp - 0x18]
0x464FAC: push     eax
0x464FAD: xor      ecx, ecx
0x464FAF: xor      edx, edx
0x464FB1: mov      ax, 0x7d3
0x464FB5: call     0x458d3c
0x464FBA: lea      edx, [ebp - 0x1c]
0x464FBD: lea      eax, [ebp - 0x18]
0x464FC0: call     0x45b41c
0x464FC5: lea      eax, [ebp - 0x1c]
0x464FC8: push     eax
0x464FC9: push     dword ptr [ebp - 8]
0x464FCC: push     0x465050 ; "	"
0x464FD1: push     dword ptr [ebp - 0xc]
0x464FD4: push     0x465050 ; "	"
0x464FD9: push     dword ptr [ebp + 8]
0x464FDC: lea      eax, [ebp - 0x24]
0x464FDF: mov      edx, 5
0x464FE4: call     0x403d94
0x464FE9: mov      eax, dword ptr [ebp - 0x24]
0x464FEC: lea      edx, [ebp - 0x20]
0x464FEF: call     0x45b4a8
0x464FF4: mov      edx, dword ptr [ebp - 0x20]
0x464FF7: pop      eax
0x464FF8: call     0x403cdc
0x464FFD: mov      edx, dword ptr [ebp - 0x1c]
0x465000: mov      eax, dword ptr [ebp - 4]
0x465003: call     0x464a40
0x465008: xor      eax, eax
0x46500A: pop      edx
0x46500B: pop      ecx
0x46500C: pop      ecx
0x46500D: mov      dword ptr fs:[eax], edx
0x465010: push     0x46503f
0x465015: lea      eax, [ebp - 0x24]
0x465018: mov      edx, 3
0x46501D: call     0x403a7c
0x465022: lea      eax, [ebp - 0xc]
0x465025: mov      edx, 2
0x46502A: call     0x403a7c
0x46502F: lea      eax, [ebp + 8]
0x465032: call     0x403a58
0x465037: ret      
0x465038: jmp      0x403518
0x46503D: jmp      0x465015
0x46503F: pop      ebx
0x465040: mov      esp, ebp
0x465042: pop      ebp
0x465043: ret      4
0x465046: add      byte ptr [eax], al
0x465050: [STR len=1] "	"
0x465054: push     ebp
0x465055: mov      ebp, esp
0x465057: add      esp, -0x24
0x46505A: push     ebx
0x46505B: xor      ebx, ebx
0x46505D: mov      dword ptr [ebp - 0x1c], ebx
0x465060: mov      dword ptr [ebp - 0x20], ebx
0x465063: mov      dword ptr [ebp - 0x24], ebx
0x465066: mov      dword ptr [ebp - 0xc], ecx
0x465069: mov      dword ptr [ebp - 8], edx
0x46506C: mov      dword ptr [ebp - 4], eax
0x46506F: mov      eax, dword ptr [ebp - 8]
0x465072: call     0x403e88
0x465077: mov      eax, dword ptr [ebp - 0xc]
0x46507A: call     0x403e88
0x46507F: mov      eax, dword ptr [ebp + 0x10]
0x465082: call     0x403e88
0x465087: mov      eax, dword ptr [ebp + 0xc]
0x46508A: call     0x403e88
0x46508F: mov      eax, dword ptr [ebp + 8]
0x465092: call     0x403e88
0x465097: xor      eax, eax
0x465099: push     ebp
0x46509A: push     0x46514d
0x46509F: push     dword ptr fs:[eax]
0x4650A2: mov      dword ptr fs:[eax], esp
0x4650A5: push     0
0x4650A7: push     0
0x4650A9: lea      eax, [ebp - 0x18]
0x4650AC: push     eax
0x4650AD: xor      ecx, ecx
0x4650AF: xor      edx, edx
0x4650B1: mov      ax, 0x65
0x4650B5: call     0x458d3c
0x4650BA: lea      edx, [ebp - 0x1c]
0x4650BD: lea      eax, [ebp - 0x18]
0x4650C0: call     0x45b41c
0x4650C5: lea      eax, [ebp - 0x1c]
0x4650C8: push     eax
0x4650C9: push     dword ptr [ebp - 8]
0x4650CC: push     0x465164 ; "/"
0x4650D1: push     dword ptr [ebp - 0xc]
0x4650D4: push     0x465164 ; "/"
0x4650D9: push     dword ptr [ebp + 0x10]
0x4650DC: push     0x465164 ; "/"
0x4650E1: push     dword ptr [ebp + 0xc]
0x4650E4: push     0x465164 ; "/"
0x4650E9: push     dword ptr [ebp + 8]
0x4650EC: lea      eax, [ebp - 0x24]
0x4650EF: mov      edx, 9
0x4650F4: call     0x403d94
0x4650F9: mov      eax, dword ptr [ebp - 0x24]
0x4650FC: lea      edx, [ebp - 0x20]
0x4650FF: call     0x45b4a8
0x465104: mov      edx, dword ptr [ebp - 0x20]
0x465107: pop      eax
0x465108: call     0x403cdc
0x46510D: mov      edx, dword ptr [ebp - 0x1c]
0x465110: mov      eax, dword ptr [ebp - 4]
0x465113: call     0x464a40
0x465118: xor      eax, eax
0x46511A: pop      edx
0x46511B: pop      ecx
0x46511C: pop      ecx
0x46511D: mov      dword ptr fs:[eax], edx
0x465120: push     0x465154
0x465125: lea      eax, [ebp - 0x24]
0x465128: mov      edx, 3
0x46512D: call     0x403a7c
0x465132: lea      eax, [ebp - 0xc]
0x465135: mov      edx, 2
0x46513A: call     0x403a7c
0x46513F: lea      eax, [ebp + 8]
0x465142: mov      edx, 3
0x465147: call     0x403a7c
0x46514C: ret      
0x46514D: jmp      0x403518
0x465152: jmp      0x465125
0x465154: pop      ebx
0x465155: mov      esp, ebp
0x465157: pop      ebp
0x465158: ret      0xc
0x46515B: add      bh, bh
0x46515F: inc      dword ptr [ecx]
0x465161: add      byte ptr [eax], al
0x465163: add      byte ptr [edi], ch
0x465165: add      byte ptr [eax], al
0x465167: add      byte ptr [ebp - 0x75], dl
0x46516A: in       al, dx
0x46516B: add      esp, -0x20
0x46516E: xor      edx, edx
0x465170: mov      dword ptr [ebp - 0x14], edx
0x465173: mov      dword ptr [ebp - 0x18], edx
0x465176: mov      dword ptr [ebp - 0x1c], edx
0x465179: mov      dword ptr [ebp - 0x20], edx
0x46517C: mov      dword ptr [ebp - 4], eax
0x46517F: xor      eax, eax
0x465181: push     ebp
0x465182: push     0x46521a
0x465187: push     dword ptr fs:[eax]
0x46518A: mov      dword ptr fs:[eax], esp
0x46518D: push     0
0x46518F: push     0
0x465191: lea      eax, [ebp - 0x10]
0x465194: push     eax
0x465195: xor      ecx, ecx
0x465197: xor      edx, edx
0x465199: mov      ax, 0x64
0x46519D: call     0x458d3c
0x4651A2: lea      edx, [ebp - 0x14]
0x4651A5: lea      eax, [ebp - 0x10]
0x4651A8: call     0x45b41c
0x4651AD: lea      eax, [ebp - 0x14]
0x4651B0: push     eax
0x4651B1: mov      eax, dword ptr [ebp - 4]
0x4651B4: push     dword ptr [eax + 0x410]
0x4651BA: push     0x465230 ; "/"
0x4651BF: lea      edx, [ebp - 0x20]
0x4651C2: mov      eax, dword ptr [ebp - 4]
0x4651C5: mov      eax, dword ptr [eax + 0x41c]
0x4651CB: call     0x407d64
0x4651D0: push     dword ptr [ebp - 0x20]
0x4651D3: lea      eax, [ebp - 0x1c]
0x4651D6: mov      edx, 3
0x4651DB: call     0x403d94
0x4651E0: mov      eax, dword ptr [ebp - 0x1c]
0x4651E3: lea      edx, [ebp - 0x18]
0x4651E6: call     0x45b4a8
0x4651EB: mov      edx, dword ptr [ebp - 0x18]
0x4651EE: pop      eax
0x4651EF: call     0x403cdc
0x4651F4: mov      edx, dword ptr [ebp - 0x14]
0x4651F7: mov      eax, dword ptr [ebp - 4]
0x4651FA: call     0x464a40
0x4651FF: xor      eax, eax
0x465201: pop      edx
0x465202: pop      ecx
0x465203: pop      ecx
0x465204: mov      dword ptr fs:[eax], edx
0x465207: push     0x465221
0x46520C: lea      eax, [ebp - 0x20]
0x46520F: mov      edx, 4
0x465214: call     0x403a7c
0x465219: ret      
0x46521A: jmp      0x403518
0x46521F: jmp      0x46520c
0x465221: mov      esp, ebp
0x465223: pop      ebp
0x465224: ret      
0x465225: add      byte ptr [eax], al
0x465227: add      bh, bh
0x46522B: inc      dword ptr [ecx]
0x46522D: add      byte ptr [eax], al
0x46522F: add      byte ptr [edi], ch
0x465231: add      byte ptr [eax], al
0x465233: add      byte ptr [ebp - 0x75], dl
0x465236: in       al, dx
0x465237: add      esp, -0x1c
0x46523A: xor      ecx, ecx
0x46523C: mov      dword ptr [ebp - 0x18], ecx
0x46523F: mov      dword ptr [ebp - 0x1c], ecx
0x465242: mov      dword ptr [ebp - 8], edx
0x465245: mov      dword ptr [ebp - 4], eax
0x465248: mov      eax, dword ptr [ebp - 8]
0x46524B: call     0x403e88
0x465250: xor      eax, eax
0x465252: push     ebp
0x465253: push     0x4652c4
0x465258: push     dword ptr fs:[eax]
0x46525B: mov      dword ptr fs:[eax], esp
0x46525E: push     0
0x465260: push     0
0x465262: lea      eax, [ebp - 0x14]
0x465265: push     eax
0x465266: xor      ecx, ecx
0x465268: xor      edx, edx
0x46526A: mov      ax, 0x66
0x46526E: call     0x458d3c
0x465273: lea      edx, [ebp - 0x18]
0x465276: lea      eax, [ebp - 0x14]
0x465279: call     0x45b41c
0x46527E: lea      eax, [ebp - 0x18]
0x465281: push     eax
0x465282: lea      edx, [ebp - 0x1c]
0x465285: mov      eax, dword ptr [ebp - 8]
0x465288: call     0x45b4a8
0x46528D: mov      edx, dword ptr [ebp - 0x1c]
0x465290: pop      eax
0x465291: call     0x403cdc
0x465296: mov      edx, dword ptr [ebp - 0x18]
0x465299: mov      eax, dword ptr [ebp - 4]
0x46529C: call     0x464a40
0x4652A1: xor      eax, eax
0x4652A3: pop      edx
0x4652A4: pop      ecx
0x4652A5: pop      ecx
0x4652A6: mov      dword ptr fs:[eax], edx
0x4652A9: push     0x4652cb
0x4652AE: lea      eax, [ebp - 0x1c]
0x4652B1: mov      edx, 2
0x4652B6: call     0x403a7c
0x4652BB: lea      eax, [ebp - 8]
0x4652BE: call     0x403a58
0x4652C3: ret      
0x4652C4: jmp      0x403518
0x4652C9: jmp      0x4652ae
0x4652CB: mov      esp, ebp
0x4652CD: pop      ebp
0x4652CE: ret      
0x4652CF: nop      
0x4652D0: push     ebp
0x4652D1: mov      ebp, esp
0x4652D3: add      esp, -0x20
0x4652D6: xor      ecx, ecx
0x4652D8: mov      dword ptr [ebp - 0x18], ecx
0x4652DB: mov      dword ptr [ebp - 0x1c], ecx
0x4652DE: mov      dword ptr [ebp - 0x20], ecx
0x4652E1: mov      dword ptr [ebp - 8], edx
0x4652E4: mov      dword ptr [ebp - 4], eax
0x4652E7: mov      eax, dword ptr [ebp - 8]
0x4652EA: call     0x403e88
0x4652EF: xor      eax, eax
0x4652F1: push     ebp
0x4652F2: push     0x465391
0x4652F7: push     dword ptr fs:[eax]
0x4652FA: mov      dword ptr fs:[eax], esp
0x4652FD: mov      eax, dword ptr [ebp - 4]
0x465300: add      eax, 0x418
0x465305: mov      edx, dword ptr [ebp - 8]
0x465308: call     0x403aac
0x46530D: push     0
0x46530F: push     0
0x465311: lea      eax, [ebp - 0x14]
0x465314: push     eax
0x465315: xor      ecx, ecx
0x465317: xor      edx, edx
0x465319: mov      ax, 0x67
0x46531D: call     0x458d3c
0x465322: lea      edx, [ebp - 0x18]
0x465325: lea      eax, [ebp - 0x14]
0x465328: call     0x45b41c
0x46532D: lea      eax, [ebp - 0x18]
0x465330: push     eax
0x465331: mov      eax, dword ptr [ebp - 4]
0x465334: push     dword ptr [eax + 0x410]
0x46533A: push     0x4653a4 ; "/"
0x46533F: push     dword ptr [ebp - 8]
0x465342: lea      eax, [ebp - 0x20]
0x465345: mov      edx, 3
0x46534A: call     0x403d94
0x46534F: mov      eax, dword ptr [ebp - 0x20]
0x465352: lea      edx, [ebp - 0x1c]
0x465355: call     0x45b4a8
0x46535A: mov      edx, dword ptr [ebp - 0x1c]
0x46535D: pop      eax
0x46535E: call     0x403cdc
0x465363: mov      edx, dword ptr [ebp - 0x18]
0x465366: mov      eax, dword ptr [ebp - 4]
0x465369: call     0x464a40
0x46536E: xor      eax, eax
0x465370: pop      edx
0x465371: pop      ecx
0x465372: pop      ecx
0x465373: mov      dword ptr fs:[eax], edx
0x465376: push     0x465398
0x46537B: lea      eax, [ebp - 0x20]
0x46537E: mov      edx, 3
0x465383: call     0x403a7c
0x465388: lea      eax, [ebp - 8]
0x46538B: call     0x403a58
0x465390: ret      
0x465391: jmp      0x403518
0x465396: jmp      0x46537b
0x465398: mov      esp, ebp
0x46539A: pop      ebp
0x46539B: ret      
0x4653A4: [STR len=1] "/"
0x4653A8: push     ebp
0x4653A9: mov      ebp, esp
0x4653AB: add      esp, -0x1c
0x4653AE: xor      edx, edx
0x4653B0: mov      dword ptr [ebp - 0x18], edx
0x4653B3: mov      dword ptr [ebp - 0x1c], edx
0x4653B6: mov      dword ptr [ebp - 8], edx
0x4653B9: mov      dword ptr [ebp - 4], eax
0x4653BC: xor      eax, eax
0x4653BE: push     ebp
0x4653BF: push     0x46546c
0x4653C4: push     dword ptr fs:[eax]
0x4653C7: mov      dword ptr fs:[eax], esp
0x4653CA: push     0x465480 ; "**"
0x4653CF: mov      eax, dword ptr [ebp - 4]
0x4653D2: push     dword ptr [eax + 0x410]
0x4653D8: push     0x46548c ; "/"
0x4653DD: mov      eax, dword ptr [ebp - 4]
0x4653E0: push     dword ptr [eax + 0x418]
0x4653E6: push     0x46548c ; "/"
0x4653EB: lea      edx, [ebp - 0x18]
0x4653EE: mov      eax, dword ptr [ebp - 4]
0x4653F1: mov      eax, dword ptr [eax + 0x41c]
0x4653F7: call     0x407d64
0x4653FC: push     dword ptr [ebp - 0x18]
0x4653FF: push     0x46548c ; "/"
0x465404: lea      edx, [ebp - 0x1c]
0x465407: mov      eax, 0x1317d2a
0x46540C: call     0x407d64
0x465411: push     dword ptr [ebp - 0x1c]
0x465414: push     0x46548c ; "/"
0x465419: lea      eax, [ebp - 8]
0x46541C: mov      edx, 9
0x465421: call     0x403d94
0x465426: lea      eax, [ebp - 8]
0x465429: mov      edx, 0x465498 ; "0"
0x46542E: call     0x403cdc
0x465433: lea      edx, [ebp - 0x18]
0x465436: mov      eax, dword ptr [ebp - 8]
0x465439: call     0x45b4a8
0x46543E: mov      edx, dword ptr [ebp - 0x18]
0x465441: mov      eax, dword ptr [ebp - 4]
0x465444: call     0x464a40
0x465449: xor      eax, eax
0x46544B: pop      edx
0x46544C: pop      ecx
0x46544D: pop      ecx
0x46544E: mov      dword ptr fs:[eax], edx
0x465451: push     0x465473
0x465456: lea      eax, [ebp - 0x1c]
0x465459: mov      edx, 2
0x46545E: call     0x403a7c
0x465463: lea      eax, [ebp - 8]
0x465466: call     0x403a58
0x46546B: ret      
0x46546C: jmp      0x403518
0x465471: jmp      0x465456
0x465473: mov      esp, ebp
0x465475: pop      ebp
0x465476: ret      
0x465477: add      bh, bh
0x46547B: inc      dword ptr [edx]
0x46547D: add      byte ptr [eax], al
0x46547F: add      byte ptr [edx], ch
0x465481: sub      al, byte ptr [eax]
0x465483: add      bh, bh
0x465487: inc      dword ptr [ecx]
0x465489: add      byte ptr [eax], al
0x46548B: add      byte ptr [edi], ch
0x46548D: add      byte ptr [eax], al
0x46548F: add      bh, bh
0x465493: inc      dword ptr [ecx]
0x465495: add      byte ptr [eax], al
0x465497: add      byte ptr [eax], dh
0x465499: add      byte ptr [eax], al
0x46549B: add      byte ptr [ebp - 0x75], dl
0x46549E: in       al, dx
0x46549F: add      esp, -0x20
0x4654A2: xor      ecx, ecx
0x4654A4: mov      dword ptr [ebp - 0x18], ecx
0x4654A7: mov      dword ptr [ebp - 0x1c], ecx
0x4654AA: mov      dword ptr [ebp - 8], edx
0x4654AD: mov      dword ptr [ebp - 4], eax
0x4654B0: mov      eax, dword ptr [ebp - 8]
0x4654B3: call     0x403e88
0x4654B8: xor      eax, eax
0x4654BA: push     ebp
0x4654BB: push     0x465672
0x4654C0: push     dword ptr fs:[eax]
0x4654C3: mov      dword ptr fs:[eax], esp
0x4654C6: cmp      dword ptr [ebp - 8], 0
0x4654CA: je       0x46564f
0x4654D0: mov      eax, dword ptr [ebp - 8]
0x4654D3: mov      edx, 0x465688 ; "/check debug screen"
0x4654D8: call     0x403de4
0x4654DD: jne      0x46551c
0x4654DF: mov      al, byte ptr [0x4f947a]
0x4654E4: xor      al, 1
0x4654E6: mov      byte ptr [0x4f947a], al
0x4654EB: cmp      byte ptr [0x4f947a], 0
0x4654F2: je       0x465508
0x4654F4: mov      edx, 0x4656a4 ; "On"
0x4654F9: mov      eax, dword ptr [0x4f7d8c]
0x4654FE: call     0x479a68
0x465503: jmp      0x46564f
0x465508: mov      edx, 0x4656b0 ; "Off"
0x46550D: mov      eax, dword ptr [0x4f7d8c]
0x465512: call     0x479a68
0x465517: jmp      0x46564f
0x46551C: mov      eax, dword ptr [ebp - 8]
0x46551F: mov      edx, 0x4656bc ; "/check speedhack"
0x465524: call     0x403de4
0x465529: jne      0x46553c
0x46552B: mov      al, byte ptr [0x4f947b]
0x465530: xor      al, 1
0x465532: mov      byte ptr [0x4f947b], al
0x465537: jmp      0x46564f
0x46553C: mov      eax, dword ptr [ebp - 8]
0x46553F: mov      edx, 0x4656d8 ; "@password"
0x465544: call     0x403de4
0x465549: jne      0x46558d
0x46554B: mov      eax, dword ptr [0x4f7d9c]
0x465550: mov      eax, dword ptr [eax + 0x5a850]
0x465556: cmp      byte ptr [eax + 0x1f1], 0
0x46555D: jne      0x465576
0x46555F: mov      eax, dword ptr [0x4f7d9c]
0x465564: mov      eax, dword ptr [eax + 0x5a850]
0x46556A: mov      dl, 0x2a
0x46556C: call     0x421b94
0x465571: jmp      0x46564f
0x465576: mov      eax, dword ptr [0x4f7d9c]
0x46557B: mov      eax, dword ptr [eax + 0x5a850]
0x465581: xor      edx, edx
0x465583: call     0x421b94
0x465588: jmp      0x46564f
0x46558D: push     0
0x46558F: push     0
0x465591: lea      eax, [ebp - 0x14]
0x465594: push     eax
0x465595: xor      ecx, ecx
0x465597: xor      edx, edx
0x465599: mov      ax, 0xbd6
0x46559D: call     0x458d3c
0x4655A2: lea      edx, [ebp - 0x18]
0x4655A5: lea      eax, [ebp - 0x14]
0x4655A8: call     0x45b41c
0x4655AD: lea      eax, [ebp - 0x18]
0x4655B0: push     eax
0x4655B1: lea      edx, [ebp - 0x1c]
0x4655B4: mov      eax, dword ptr [ebp - 8]
0x4655B7: call     0x45b4a8
0x4655BC: mov      edx, dword ptr [ebp - 0x1c]
0x4655BF: pop      eax
0x4655C0: call     0x403cdc
0x4655C5: mov      edx, dword ptr [ebp - 0x18]
0x4655C8: mov      eax, dword ptr [ebp - 4]
0x4655CB: call     0x464a40
0x4655D0: mov      eax, dword ptr [ebp - 8]
0x4655D3: cmp      byte ptr [eax], 0x2f
0x4655D6: jne      0x46562d
0x4655D8: push     0xffffff
0x4655DD: mov      al, 0xb4
0x4655DF: call     0x45edc8
0x4655E4: mov      ecx, eax
0x4655E6: mov      edx, dword ptr [ebp - 8]
0x4655E9: mov      eax, dword ptr [0x4f7d8c]
0x4655EE: call     0x479ae0
0x4655F3: push     0
0x4655F5: lea      eax, [ebp - 0x18]
0x4655F8: push     eax
0x4655F9: lea      eax, [ebp - 0x1c]
0x4655FC: push     eax
0x4655FD: mov      eax, dword ptr [ebp - 8]
0x465600: call     0x403cd4
0x465605: mov      ecx, eax
0x465607: dec      ecx
0x465608: mov      edx, 2
0x46560D: mov      eax, dword ptr [ebp - 8]
0x465610: call     0x403ed8
0x465615: mov      eax, dword ptr [ebp - 0x1c]
0x465618: mov      byte ptr [ebp - 0x20], 0x20
0x46561C: lea      ecx, [ebp - 0x20]
0x46561F: mov      edx, dword ptr [ebp - 4]
0x465622: add      edx, 0x40c
0x465628: call     0x452530
0x46562D: cmp      byte ptr [0x4f9236], 0
0x465634: je       0x46564f
0x465636: mov      byte ptr [0x4f9236], 0
0x46563D: mov      eax, dword ptr [0x4f7d9c]
0x465642: mov      eax, dword ptr [eax + 0x5a850]
0x465648: xor      edx, edx
0x46564A: call     0x421b94
0x46564F: xor      eax, eax
0x465651: pop      edx
0x465652: pop      ecx
0x465653: pop      ecx
0x465654: mov      dword ptr fs:[eax], edx
0x465657: push     0x465679
0x46565C: lea      eax, [ebp - 0x1c]
0x46565F: mov      edx, 2
0x465664: call     0x403a7c
0x465669: lea      eax, [ebp - 8]
0x46566C: call     0x403a58
0x465671: ret      
0x465672: jmp      0x403518
0x465677: jmp      0x46565c
0x465679: mov      esp, ebp
0x46567B: pop      ebp
0x46567C: ret      
0x46567D: add      byte ptr [eax], al
0x46567F: add      bh, bh
0x465683: call     dword ptr [ebx]
0x465685: add      byte ptr [eax], al
0x465687: add      byte ptr [edi], ch
0x465689: arpl     word ptr [eax + 0x65], bp
0x46568C: arpl     word ptr [ebx + 0x20], bp
0x46568F: bound    esi, qword ptr gs:[ebp + 0x67]
0x465694: and      byte ptr [ebx + 0x63], dh
0x465697: jb       0x4656fe
0x465699: outsb    dx, byte ptr gs:[esi]
0x46569B: add      bh, bh
0x46569F: inc      dword ptr [edx]
0x4656A1: add      byte ptr [eax], al
0x4656A3: add      byte ptr [edi + 0x6e], cl
0x4656A6: add      byte ptr [eax], al
0x4656B0: [STR len=3] "Off"
0x4656BC: [STR len=16] "/check speedhack"
0x4656D8: [STR len=9] "@password"
0x4656E4: push     ebp
0x4656E5: mov      ebp, esp
0x4656E7: add      esp, -0x1c
0x4656EA: push     ebx
0x4656EB: xor      ebx, ebx
0x4656ED: mov      dword ptr [ebp - 0x1c], ebx
0x4656F0: mov      dword ptr [ebp - 0xc], ecx
0x4656F3: mov      dword ptr [ebp - 8], edx
0x4656F6: mov      dword ptr [ebp - 4], eax
0x4656F9: xor      eax, eax
0x4656FB: push     ebp
0x4656FC: push     0x465776
0x465701: push     dword ptr fs:[eax]
0x465704: mov      dword ptr fs:[eax], esp
0x465707: mov      ax, word ptr [ebp + 8]
0x46570B: push     eax
0x46570C: push     0
0x46570E: lea      eax, [ebp - 0x18]
0x465711: push     eax
0x465712: mov      dx, word ptr [ebp + 0xc]
0x465716: mov      ax, word ptr [ebp - 0xc]
0x46571A: call     0x406964
0x46571F: mov      edx, eax
0x465721: xor      ecx, ecx
0x465723: mov      ax, word ptr [ebp - 8]
0x465727: call     0x458d3c
0x46572C: lea      edx, [ebp - 0x1c]
0x46572F: lea      eax, [ebp - 0x18]
0x465732: call     0x45b41c
0x465737: mov      edx, dword ptr [ebp - 0x1c]
0x46573A: mov      eax, dword ptr [ebp - 4]
0x46573D: call     0x464a40
0x465742: mov      eax, dword ptr [ebp - 4]
0x465745: mov      byte ptr [eax + 0x420], 1
0x46574C: call     0x406154
0x465751: mov      edx, dword ptr [ebp - 4]
0x465754: mov      dword ptr [edx + 0x3d8], eax
0x46575A: inc      dword ptr [0x4f9528]
0x465760: xor      eax, eax
0x465762: pop      edx
0x465763: pop      ecx
0x465764: pop      ecx
0x465765: mov      dword ptr fs:[eax], edx
0x465768: push     0x46577d
0x46576D: lea      eax, [ebp - 0x1c]
0x465770: call     0x403a58
0x465775: ret      
0x465776: jmp      0x403518
0x46577B: jmp      0x46576d
0x46577D: pop      ebx
0x46577E: mov      esp, ebp
0x465780: pop      ebp
0x465781: ret      8
0x465784: push     ebp
0x465785: mov      ebp, esp
0x465787: add      esp, -0x1c
0x46578A: push     ebx
0x46578B: xor      ebx, ebx
0x46578D: mov      dword ptr [ebp - 0x1c], ebx
0x465790: mov      dword ptr [ebp - 0xc], ecx
0x465793: mov      dword ptr [ebp - 8], edx
0x465796: mov      dword ptr [ebp - 4], eax
0x465799: xor      eax, eax
0x46579B: push     ebp
0x46579C: push     0x46581f
0x4657A1: push     dword ptr fs:[eax]
0x4657A4: mov      dword ptr fs:[eax], esp
0x4657A7: mov      ax, word ptr [ebp + 0xc]
0x4657AB: push     eax
0x4657AC: mov      eax, dword ptr [ebp + 8]
0x4657AF: call     0x406970
0x4657B4: push     eax
0x4657B5: lea      eax, [ebp - 0x18]
0x4657B8: push     eax
0x4657B9: mov      dx, word ptr [ebp + 0x10]
0x4657BD: mov      ax, word ptr [ebp - 0xc]
0x4657C1: call     0x406964
0x4657C6: mov      edx, eax
0x4657C8: mov      cx, word ptr [ebp + 8]
0x4657CC: mov      ax, word ptr [ebp - 8]
0x4657D0: call     0x458d3c
0x4657D5: lea      edx, [ebp - 0x1c]
0x4657D8: lea      eax, [ebp - 0x18]
0x4657DB: call     0x45b41c
0x4657E0: mov      edx, dword ptr [ebp - 0x1c]
0x4657E3: mov      eax, dword ptr [ebp - 4]
0x4657E6: call     0x464a40
0x4657EB: mov      eax, dword ptr [ebp - 4]
0x4657EE: mov      byte ptr [eax + 0x420], 1
0x4657F5: call     0x406154
0x4657FA: mov      edx, dword ptr [ebp - 4]
0x4657FD: mov      dword ptr [edx + 0x3d8], eax
0x465803: inc      dword ptr [0x4f9528]
0x465809: xor      eax, eax
0x46580B: pop      edx
0x46580C: pop      ecx
0x46580D: pop      ecx
0x46580E: mov      dword ptr fs:[eax], edx
0x465811: push     0x465826
0x465816: lea      eax, [ebp - 0x1c]
0x465819: call     0x403a58
0x46581E: ret      
0x46581F: jmp      0x403518
0x465824: jmp      0x465816
0x465826: pop      ebx
0x465827: mov      esp, ebp
0x465829: pop      ebp
0x46582A: ret      0xc
0x46582D: lea      eax, [eax]
0x465830: push     ebp
0x465831: mov      ebp, esp
0x465833: add      esp, -0x1c
0x465836: push     ebx
0x465837: xor      ebx, ebx
0x465839: mov      dword ptr [ebp - 0x1c], ebx
0x46583C: mov      dword ptr [ebp - 0xc], ecx
0x46583F: mov      dword ptr [ebp - 8], edx
0x465842: mov      dword ptr [ebp - 4], eax
0x465845: xor      eax, eax
0x465847: push     ebp
0x465848: push     0x46589a
0x46584D: push     dword ptr fs:[eax]
0x465850: mov      dword ptr fs:[eax], esp
0x465853: mov      ax, word ptr [ebp + 8]
0x465857: push     eax
0x465858: push     0
0x46585A: lea      eax, [ebp - 0x18]
0x46585D: push     eax
0x46585E: mov      cx, word ptr [ebp - 0xc]
0x465862: mov      edx, dword ptr [ebp - 8]
0x465865: mov      ax, 0x50
0x465869: call     0x458d3c
0x46586E: lea      edx, [ebp - 0x1c]
0x465871: lea      eax, [ebp - 0x18]
0x465874: call     0x45b41c
0x465879: mov      edx, dword ptr [ebp - 0x1c]
0x46587C: mov      eax, dword ptr [ebp - 4]
0x46587F: call     0x464a40
0x465884: xor      eax, eax
0x465886: pop      edx
0x465887: pop      ecx
0x465888: pop      ecx
0x465889: mov      dword ptr fs:[eax], edx
0x46588C: push     0x4658a1
0x465891: lea      eax, [ebp - 0x1c]
0x465894: call     0x403a58
0x465899: ret      
0x46589A: jmp      0x403518
0x46589F: jmp      0x465891
0x4658A1: pop      ebx
0x4658A2: mov      esp, ebp
0x4658A4: pop      ebp
0x4658A5: ret      4
0x4658A8: push     ebp
0x4658A9: mov      ebp, esp
0x4658AB: add      esp, -0x20
0x4658AE: push     ebx
0x4658AF: xor      ebx, ebx
0x4658B1: mov      dword ptr [ebp - 0x1c], ebx
0x4658B4: mov      dword ptr [ebp - 0x20], ebx
0x4658B7: mov      dword ptr [ebp - 0xc], ecx
0x4658BA: mov      dword ptr [ebp - 8], edx
0x4658BD: mov      dword ptr [ebp - 4], eax
0x4658C0: mov      eax, dword ptr [ebp - 8]
0x4658C3: call     0x403e88
0x4658C8: xor      eax, eax
0x4658CA: push     ebp
0x4658CB: push     0x46593d
0x4658D0: push     dword ptr fs:[eax]
0x4658D3: mov      dword ptr fs:[eax], esp
0x4658D6: push     0
0x4658D8: push     0
0x4658DA: lea      eax, [ebp - 0x18]
0x4658DD: push     eax
0x4658DE: xor      ecx, ecx
0x4658E0: mov      edx, dword ptr [ebp - 0xc]
0x4658E3: mov      ax, 0x3e8
0x4658E7: call     0x458d3c
0x4658EC: lea      edx, [ebp - 0x1c]
0x4658EF: lea      eax, [ebp - 0x18]
0x4658F2: call     0x45b41c
0x4658F7: lea      eax, [ebp - 0x1c]
0x4658FA: push     eax
0x4658FB: lea      edx, [ebp - 0x20]
0x4658FE: mov      eax, dword ptr [ebp - 8]
0x465901: call     0x45b4a8
0x465906: mov      edx, dword ptr [ebp - 0x20]
0x465909: pop      eax
0x46590A: call     0x403cdc
0x46590F: mov      edx, dword ptr [ebp - 0x1c]
0x465912: mov      eax, dword ptr [ebp - 4]
0x465915: call     0x464a40
0x46591A: xor      eax, eax
0x46591C: pop      edx
0x46591D: pop      ecx
0x46591E: pop      ecx
0x46591F: mov      dword ptr fs:[eax], edx
0x465922: push     0x465944
0x465927: lea      eax, [ebp - 0x20]
0x46592A: mov      edx, 2
0x46592F: call     0x403a7c
0x465934: lea      eax, [ebp - 8]
0x465937: call     0x403a58
0x46593C: ret      
0x46593D: jmp      0x403518
0x465942: jmp      0x465927
0x465944: pop      ebx
0x465945: mov      esp, ebp
0x465947: pop      ebp
0x465948: ret      
0x465949: lea      eax, [eax]
0x46594C: push     ebp
0x46594D: mov      ebp, esp
0x46594F: add      esp, -0x14
0x465952: xor      edx, edx
0x465954: mov      dword ptr [ebp - 0x14], edx
0x465957: mov      dword ptr [ebp - 4], eax
0x46595A: xor      eax, eax
0x46595C: push     ebp
0x46595D: push     0x4659b8
0x465962: push     dword ptr fs:[eax]
0x465965: mov      dword ptr fs:[eax], esp
0x465968: mov      eax, dword ptr [0x4f7ef8]
0x46596D: mov      ax, word ptr [eax + 0xa]
0x465971: push     eax
0x465972: push     0
0x465974: lea      eax, [ebp - 0x10]
0x465977: push     eax
0x465978: mov      eax, dword ptr [0x4f7ef8]
0x46597D: mov      cx, word ptr [eax + 8]
0x465981: xor      edx, edx
0x465983: mov      ax, 0x3e9
0x465987: call     0x458d3c
0x46598C: lea      edx, [ebp - 0x14]
0x46598F: lea      eax, [ebp - 0x10]
0x465992: call     0x45b41c
0x465997: mov      edx, dword ptr [ebp - 0x14]
0x46599A: mov      eax, dword ptr [ebp - 4]
0x46599D: call     0x464a40
0x4659A2: xor      eax, eax
0x4659A4: pop      edx
0x4659A5: pop      ecx
0x4659A6: pop      ecx
0x4659A7: mov      dword ptr fs:[eax], edx
0x4659AA: push     0x4659bf
0x4659AF: lea      eax, [ebp - 0x14]
0x4659B2: call     0x403a58
0x4659B7: ret      
0x4659B8: jmp      0x403518
0x4659BD: jmp      0x4659af
0x4659BF: mov      esp, ebp
0x4659C1: pop      ebp
0x4659C2: ret      
0x4659C3: nop      
0x4659C4: push     ebp
0x4659C5: mov      ebp, esp
0x4659C7: add      esp, -0x20
0x4659CA: push     ebx
0x4659CB: xor      ebx, ebx
0x4659CD: mov      dword ptr [ebp - 0x1c], ebx
0x4659D0: mov      dword ptr [ebp - 0x20], ebx
0x4659D3: mov      dword ptr [ebp - 0xc], ecx
0x4659D6: mov      byte ptr [ebp - 5], dl
0x4659D9: mov      dword ptr [ebp - 4], eax
0x4659DC: mov      eax, dword ptr [ebp + 8]
0x4659DF: call     0x403e88
0x4659E4: xor      eax, eax
0x4659E6: push     ebp
0x4659E7: push     0x465a5c
0x4659EC: push     dword ptr fs:[eax]
0x4659EF: mov      dword ptr fs:[eax], esp
0x4659F2: push     0
0x4659F4: push     0
0x4659F6: lea      eax, [ebp - 0x18]
0x4659F9: push     eax
0x4659FA: xor      ecx, ecx
0x4659FC: mov      cl, byte ptr [ebp - 5]
0x4659FF: mov      edx, dword ptr [ebp - 0xc]
0x465A02: mov      ax, 0x3eb
0x465A06: call     0x458d3c
0x465A0B: lea      edx, [ebp - 0x1c]
0x465A0E: lea      eax, [ebp - 0x18]
0x465A11: call     0x45b41c
0x465A16: lea      eax, [ebp - 0x1c]
0x465A19: push     eax
0x465A1A: lea      edx, [ebp - 0x20]
0x465A1D: mov      eax, dword ptr [ebp + 8]
0x465A20: call     0x45b4a8
0x465A25: mov      edx, dword ptr [ebp - 0x20]
0x465A28: pop      eax
0x465A29: call     0x403cdc
0x465A2E: mov      edx, dword ptr [ebp - 0x1c]
0x465A31: mov      eax, dword ptr [ebp - 4]
0x465A34: call     0x464a40
0x465A39: xor      eax, eax
0x465A3B: pop      edx
0x465A3C: pop      ecx
0x465A3D: pop      ecx
0x465A3E: mov      dword ptr fs:[eax], edx
0x465A41: push     0x465a63
0x465A46: lea      eax, [ebp - 0x20]
0x465A49: mov      edx, 2
0x465A4E: call     0x403a7c
0x465A53: lea      eax, [ebp + 8]
0x465A56: call     0x403a58
0x465A5B: ret      
0x465A5C: jmp      0x403518
0x465A61: jmp      0x465a46
0x465A63: pop      ebx
0x465A64: mov      esp, ebp
0x465A66: pop      ebp
0x465A67: ret      4
0x465A6A: mov      eax, eax
0x465A6C: push     ebp
0x465A6D: mov      ebp, esp
0x465A6F: add      esp, -0x20
0x465A72: push     ebx
0x465A73: xor      ebx, ebx
0x465A75: mov      dword ptr [ebp - 0x1c], ebx
0x465A78: mov      dword ptr [ebp - 0x20], ebx
0x465A7B: mov      dword ptr [ebp - 0xc], ecx
0x465A7E: mov      byte ptr [ebp - 5], dl
0x465A81: mov      dword ptr [ebp - 4], eax
0x465A84: mov      eax, dword ptr [ebp + 8]
0x465A87: call     0x403e88
0x465A8C: xor      eax, eax
0x465A8E: push     ebp
0x465A8F: push     0x465b04
0x465A94: push     dword ptr fs:[eax]
0x465A97: mov      dword ptr fs:[eax], esp
0x465A9A: push     0
0x465A9C: push     0
0x465A9E: lea      eax, [ebp - 0x18]
0x465AA1: push     eax
0x465AA2: xor      ecx, ecx
0x465AA4: mov      cl, byte ptr [ebp - 5]
0x465AA7: mov      edx, dword ptr [ebp - 0xc]
0x465AAA: mov      ax, 0x3ec
0x465AAE: call     0x458d3c
0x465AB3: lea      edx, [ebp - 0x1c]
0x465AB6: lea      eax, [ebp - 0x18]
0x465AB9: call     0x45b41c
0x465ABE: lea      eax, [ebp - 0x1c]
0x465AC1: push     eax
0x465AC2: lea      edx, [ebp - 0x20]
0x465AC5: mov      eax, dword ptr [ebp + 8]
0x465AC8: call     0x45b4a8
0x465ACD: mov      edx, dword ptr [ebp - 0x20]
0x465AD0: pop      eax
0x465AD1: call     0x403cdc
0x465AD6: mov      edx, dword ptr [ebp - 0x1c]
0x465AD9: mov      eax, dword ptr [ebp - 4]
0x465ADC: call     0x464a40
0x465AE1: xor      eax, eax
0x465AE3: pop      edx
0x465AE4: pop      ecx
0x465AE5: pop      ecx
0x465AE6: mov      dword ptr fs:[eax], edx
0x465AE9: push     0x465b0b
0x465AEE: lea      eax, [ebp - 0x20]
0x465AF1: mov      edx, 2
0x465AF6: call     0x403a7c
0x465AFB: lea      eax, [ebp + 8]
0x465AFE: call     0x403a58
0x465B03: ret      
0x465B04: jmp      0x403518
0x465B09: jmp      0x465aee
0x465B0B: pop      ebx
0x465B0C: mov      esp, ebp
0x465B0E: pop      ebp
0x465B0F: ret      4
0x465B12: mov      eax, eax
0x465B14: push     ebp
0x465B15: mov      ebp, esp
0x465B17: add      esp, -0x20
0x465B1A: push     ebx
0x465B1B: xor      ebx, ebx
0x465B1D: mov      dword ptr [ebp - 0x1c], ebx
0x465B20: mov      dword ptr [ebp - 0x20], ebx
0x465B23: mov      dword ptr [ebp - 0xc], ecx
0x465B26: mov      dword ptr [ebp - 8], edx
0x465B29: mov      dword ptr [ebp - 4], eax
0x465B2C: mov      eax, dword ptr [ebp - 0xc]
0x465B2F: call     0x403e88
0x465B34: xor      eax, eax
0x465B36: push     ebp
0x465B37: push     0x465ba9
0x465B3C: push     dword ptr fs:[eax]
0x465B3F: mov      dword ptr fs:[eax], esp
0x465B42: push     0
0x465B44: push     0
0x465B46: lea      eax, [ebp - 0x18]
0x465B49: push     eax
0x465B4A: xor      ecx, ecx
0x465B4C: mov      edx, dword ptr [ebp - 8]
0x465B4F: mov      ax, 0x3ee
0x465B53: call     0x458d3c
0x465B58: lea      edx, [ebp - 0x1c]
0x465B5B: lea      eax, [ebp - 0x18]
0x465B5E: call     0x45b41c
0x465B63: lea      eax, [ebp - 0x1c]
0x465B66: push     eax
0x465B67: lea      edx, [ebp - 0x20]
0x465B6A: mov      eax, dword ptr [ebp - 0xc]
0x465B6D: call     0x45b4a8
0x465B72: mov      edx, dword ptr [ebp - 0x20]
0x465B75: pop      eax
0x465B76: call     0x403cdc
0x465B7B: mov      edx, dword ptr [ebp - 0x1c]
0x465B7E: mov      eax, dword ptr [ebp - 4]
0x465B81: call     0x464a40
0x465B86: xor      eax, eax
0x465B88: pop      edx
0x465B89: pop      ecx
0x465B8A: pop      ecx
0x465B8B: mov      dword ptr fs:[eax], edx
0x465B8E: push     0x465bb0
0x465B93: lea      eax, [ebp - 0x20]
0x465B96: mov      edx, 2
0x465B9B: call     0x403a7c
0x465BA0: lea      eax, [ebp - 0xc]
0x465BA3: call     0x403a58
0x465BA8: ret      
0x465BA9: jmp      0x403518
0x465BAE: jmp      0x465b93
0x465BB0: pop      ebx
0x465BB1: mov      esp, ebp
0x465BB3: pop      ebp
0x465BB4: ret      
0x465BB5: lea      eax, [eax]
0x465BB8: push     ebp
0x465BB9: mov      ebp, esp
0x465BBB: add      esp, -0x1c
0x465BBE: push     ebx
0x465BBF: xor      ebx, ebx
0x465BC1: mov      dword ptr [ebp - 0x1c], ebx
0x465BC4: mov      dword ptr [ebp - 0xc], ecx
0x465BC7: mov      dword ptr [ebp - 8], edx
0x465BCA: mov      dword ptr [ebp - 4], eax
0x465BCD: xor      eax, eax
0x465BCF: push     ebp
0x465BD0: push     0x465c25
0x465BD5: push     dword ptr fs:[eax]
0x465BD8: mov      dword ptr fs:[eax], esp
0x465BDB: mov      ax, word ptr [ebp - 0xc]
0x465BDF: push     eax
0x465BE0: mov      ax, word ptr [ebp + 0xc]
0x465BE4: push     eax
0x465BE5: lea      eax, [ebp - 0x18]
0x465BE8: push     eax
0x465BE9: mov      cx, word ptr [ebp - 8]
0x465BED: mov      edx, dword ptr [ebp + 8]
0x465BF0: mov      ax, 0x3ef
0x465BF4: call     0x458d3c
0x465BF9: lea      edx, [ebp - 0x1c]
0x465BFC: lea      eax, [ebp - 0x18]
0x465BFF: call     0x45b41c
0x465C04: mov      edx, dword ptr [ebp - 0x1c]
0x465C07: mov      eax, dword ptr [ebp - 4]
0x465C0A: call     0x464a40
0x465C0F: xor      eax, eax
0x465C11: pop      edx
0x465C12: pop      ecx
0x465C13: pop      ecx
0x465C14: mov      dword ptr fs:[eax], edx
0x465C17: push     0x465c2c
0x465C1C: lea      eax, [ebp - 0x1c]
0x465C1F: call     0x403a58
0x465C24: ret      
0x465C25: jmp      0x403518
0x465C2A: jmp      0x465c1c
0x465C2C: pop      ebx
0x465C2D: mov      esp, ebp
0x465C2F: pop      ebp
0x465C30: ret      8
0x465C33: nop      
0x465C34: push     ebp
0x465C35: mov      ebp, esp
0x465C37: add      esp, -0x1c
0x465C3A: push     ebx
0x465C3B: xor      ebx, ebx
0x465C3D: mov      dword ptr [ebp - 0x1c], ebx
0x465C40: mov      byte ptr [ebp - 9], cl
0x465C43: mov      dword ptr [ebp - 8], edx
0x465C46: mov      dword ptr [ebp - 4], eax
0x465C49: xor      eax, eax
0x465C4B: push     ebp
0x465C4C: push     0x465c9c
0x465C51: push     dword ptr fs:[eax]
0x465C54: mov      dword ptr fs:[eax], esp
0x465C57: push     0
0x465C59: push     0
0x465C5B: lea      eax, [ebp - 0x18]
0x465C5E: push     eax
0x465C5F: xor      ecx, ecx
0x465C61: mov      cl, byte ptr [ebp - 9]
0x465C64: mov      edx, dword ptr [ebp - 8]
0x465C67: mov      ax, 0x3f0
0x465C6B: call     0x458d3c
0x465C70: lea      edx, [ebp - 0x1c]
0x465C73: lea      eax, [ebp - 0x18]
0x465C76: call     0x45b41c
0x465C7B: mov      edx, dword ptr [ebp - 0x1c]
0x465C7E: mov      eax, dword ptr [ebp - 4]
0x465C81: call     0x464a40
0x465C86: xor      eax, eax
0x465C88: pop      edx
0x465C89: pop      ecx
0x465C8A: pop      ecx
0x465C8B: mov      dword ptr fs:[eax], edx
0x465C8E: push     0x465ca3
0x465C93: lea      eax, [ebp - 0x1c]
0x465C96: call     0x403a58
0x465C9B: ret      
0x465C9C: jmp      0x403518
0x465CA1: jmp      0x465c93
0x465CA3: pop      ebx
0x465CA4: mov      esp, ebp
0x465CA6: pop      ebp
0x465CA7: ret      
0x465CA8: push     ebp
0x465CA9: mov      ebp, esp
0x465CAB: add      esp, -0x24
0x465CAE: push     ebx
0x465CAF: xor      ebx, ebx
0x465CB1: mov      dword ptr [ebp - 0x20], ebx
0x465CB4: mov      dword ptr [ebp - 0x24], ebx
0x465CB7: mov      dword ptr [ebp - 0x10], ebx
0x465CBA: mov      dword ptr [ebp - 0xc], ecx
0x465CBD: mov      dword ptr [ebp - 8], edx
0x465CC0: mov      dword ptr [ebp - 4], eax
0x465CC3: mov      eax, dword ptr [ebp - 0xc]
0x465CC6: call     0x403e88
0x465CCB: xor      eax, eax
0x465CCD: push     ebp
0x465CCE: push     0x465dd1
0x465CD3: push     dword ptr fs:[eax]
0x465CD6: mov      dword ptr fs:[eax], esp
0x465CD9: mov      eax, dword ptr [ebp - 0xc]
0x465CDC: call     0x403cd4
0x465CE1: cmp      eax, 2
0x465CE4: jl       0x465d65
0x465CE6: mov      eax, dword ptr [ebp - 0xc]
0x465CE9: cmp      byte ptr [eax], 0x40
0x465CEC: jne      0x465d65
0x465CEE: mov      eax, dword ptr [ebp - 0xc]
0x465CF1: cmp      byte ptr [eax + 1], 0x40
0x465CF5: jne      0x465d65
0x465CF7: mov      eax, dword ptr [ebp - 0xc]
0x465CFA: mov      edx, 0x465de8 ; "@@buildguildnow"
0x465CFF: call     0x403de4
0x465D04: jne      0x465d20
0x465D06: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x465D0B: mov      eax, dword ptr [eax]
0x465D0D: mov      cx, word ptr [0x465df8]
0x465D14: mov      edx, 0x465e04 ; "请键入你想建立的行会的名字。."
0x465D19: call     0x486df0
0x465D1E: jmp      0x465d38
0x465D20: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x465D25: mov      eax, dword ptr [eax]
0x465D27: mov      cx, word ptr [0x465df8]
0x465D2E: mov      edx, 0x465e2c ; "请输入。"
0x465D33: call     0x486df0
0x465D38: lea      edx, [ebp - 0x10]
0x465D3B: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x465D40: mov      eax, dword ptr [eax]
0x465D42: mov      eax, dword ptr [eax + 0x784]
0x465D48: call     0x407be4
0x465D4D: push     dword ptr [ebp - 0xc]
0x465D50: push     0x465e40 ; "\r"
0x465D55: push     dword ptr [ebp - 0x10]
0x465D58: lea      eax, [ebp - 0xc]
0x465D5B: mov      edx, 3
0x465D60: call     0x403d94
0x465D65: push     0
0x465D67: push     0
0x465D69: lea      eax, [ebp - 0x1c]
0x465D6C: push     eax
0x465D6D: xor      ecx, ecx
0x465D6F: mov      edx, dword ptr [ebp - 8]
0x465D72: mov      ax, 0x3f3
0x465D76: call     0x458d3c
0x465D7B: lea      edx, [ebp - 0x20]
0x465D7E: lea      eax, [ebp - 0x1c]
0x465D81: call     0x45b41c
0x465D86: lea      eax, [ebp - 0x20]
0x465D89: push     eax
0x465D8A: lea      edx, [ebp - 0x24]
0x465D8D: mov      eax, dword ptr [ebp - 0xc]
0x465D90: call     0x45b4a8
0x465D95: mov      edx, dword ptr [ebp - 0x24]
0x465D98: pop      eax
0x465D99: call     0x403cdc
0x465D9E: mov      edx, dword ptr [ebp - 0x20]
0x465DA1: mov      eax, dword ptr [ebp - 4]
0x465DA4: call     0x464a40
0x465DA9: xor      eax, eax
0x465DAB: pop      edx
0x465DAC: pop      ecx
0x465DAD: pop      ecx
0x465DAE: mov      dword ptr fs:[eax], edx
0x465DB1: push     0x465dd8
0x465DB6: lea      eax, [ebp - 0x24]
0x465DB9: mov      edx, 2
0x465DBE: call     0x403a7c
0x465DC3: lea      eax, [ebp - 0x10]
0x465DC6: mov      edx, 2
0x465DCB: call     0x403a7c
0x465DD0: ret      
0x465DD1: jmp      0x403518
0x465DD6: jmp      0x465db6
0x465DD8: pop      ebx
0x465DD9: mov      esp, ebp
0x465DDB: pop      ebp
0x465DDC: ret      
0x465DDD: add      byte ptr [eax], al
0x465DDF: add      bh, bh
0x465DE3: dec      dword ptr [edi]
0x465DE5: add      byte ptr [eax], al
0x465DE7: add      byte ptr [eax + 0x40], al
0x465DEA: bound    esi, qword ptr [ebp + 0x69]
0x465DED: insb     byte ptr es:[edi], dx
0x465DEE: jne      0x465e5b
0x465DF2: insb     byte ptr es:[edi], dx
0x465DF3: outsb    dx, byte ptr fs:[esi]
0x465DF5: outsd    dx, dword ptr [esi]
0x465DF6: ja       0x465df8
0x465DF8: adc      al, 0
0x465DFA: add      byte ptr [eax], al
0x465E04: [STR len=29] "请键入你想建立的行会的名字。."
0x465E2C: [STR len=8] "请输入。"
0x465E40: [STR len=1] "\r"
0x465E44: push     ebp
0x465E45: mov      ebp, esp
0x465E47: add      esp, -0x20
0x465E4A: push     ebx
0x465E4B: xor      ebx, ebx
0x465E4D: mov      dword ptr [ebp - 0x1c], ebx
0x465E50: mov      dword ptr [ebp - 0x20], ebx
0x465E53: mov      dword ptr [ebp - 0xc], ecx
0x465E56: mov      dword ptr [ebp - 8], edx
0x465E59: mov      dword ptr [ebp - 4], eax
0x465E5C: mov      eax, dword ptr [ebp + 8]
0x465E5F: call     0x403e88
0x465E64: xor      eax, eax
0x465E66: push     ebp
0x465E67: push     0x465ee2
0x465E6C: push     dword ptr fs:[eax]
0x465E6F: mov      dword ptr fs:[eax], esp
0x465E72: mov      eax, dword ptr [ebp - 0xc]
0x465E75: call     0x406970
0x465E7A: push     eax
0x465E7B: push     0
0x465E7D: lea      eax, [ebp - 0x18]
0x465E80: push     eax
0x465E81: mov      cx, word ptr [ebp - 0xc]
0x465E85: mov      edx, dword ptr [ebp - 8]
0x465E88: mov      ax, 0x3f4
0x465E8C: call     0x458d3c
0x465E91: lea      edx, [ebp - 0x1c]
0x465E94: lea      eax, [ebp - 0x18]
0x465E97: call     0x45b41c
0x465E9C: lea      eax, [ebp - 0x1c]
0x465E9F: push     eax
0x465EA0: lea      edx, [ebp - 0x20]
0x465EA3: mov      eax, dword ptr [ebp + 8]
0x465EA6: call     0x45b4a8
0x465EAB: mov      edx, dword ptr [ebp - 0x20]
0x465EAE: pop      eax
0x465EAF: call     0x403cdc
0x465EB4: mov      edx, dword ptr [ebp - 0x1c]
0x465EB7: mov      eax, dword ptr [ebp - 4]
0x465EBA: call     0x464a40
0x465EBF: xor      eax, eax
0x465EC1: pop      edx
0x465EC2: pop      ecx
0x465EC3: pop      ecx
0x465EC4: mov      dword ptr fs:[eax], edx
0x465EC7: push     0x465ee9
0x465ECC: lea      eax, [ebp - 0x20]
0x465ECF: mov      edx, 2
0x465ED4: call     0x403a7c
0x465ED9: lea      eax, [ebp + 8]
0x465EDC: call     0x403a58
0x465EE1: ret      
0x465EE2: jmp      0x403518
0x465EE7: jmp      0x465ecc
0x465EE9: pop      ebx
0x465EEA: mov      esp, ebp
0x465EEC: pop      ebp
0x465EED: ret      4
0x465EF0: push     ebp
0x465EF1: mov      ebp, esp
0x465EF3: add      esp, -0x20
0x465EF6: push     ebx
0x465EF7: xor      ebx, ebx
0x465EF9: mov      dword ptr [ebp - 0x1c], ebx
0x465EFC: mov      dword ptr [ebp - 0x20], ebx
0x465EFF: mov      dword ptr [ebp - 0xc], ecx
0x465F02: mov      dword ptr [ebp - 8], edx
0x465F05: mov      dword ptr [ebp - 4], eax
0x465F08: mov      eax, dword ptr [ebp + 8]
0x465F0B: call     0x403e88
0x465F10: xor      eax, eax
0x465F12: push     ebp
0x465F13: push     0x465f8e
0x465F18: push     dword ptr fs:[eax]
0x465F1B: mov      dword ptr fs:[eax], esp
0x465F1E: mov      eax, dword ptr [ebp - 0xc]
0x465F21: call     0x406970
0x465F26: push     eax
0x465F27: push     0
0x465F29: lea      eax, [ebp - 0x18]
0x465F2C: push     eax
0x465F2D: mov      cx, word ptr [ebp - 0xc]
0x465F31: mov      edx, dword ptr [ebp - 8]
0x465F34: mov      ax, 0x400
0x465F38: call     0x458d3c
0x465F3D: lea      edx, [ebp - 0x1c]
0x465F40: lea      eax, [ebp - 0x18]
0x465F43: call     0x45b41c
0x465F48: lea      eax, [ebp - 0x1c]
0x465F4B: push     eax
0x465F4C: lea      edx, [ebp - 0x20]
0x465F4F: mov      eax, dword ptr [ebp + 8]
0x465F52: call     0x45b4a8
0x465F57: mov      edx, dword ptr [ebp - 0x20]
0x465F5A: pop      eax
0x465F5B: call     0x403cdc
0x465F60: mov      edx, dword ptr [ebp - 0x1c]
0x465F63: mov      eax, dword ptr [ebp - 4]
0x465F66: call     0x464a40
0x465F6B: xor      eax, eax
0x465F6D: pop      edx
0x465F6E: pop      ecx
0x465F6F: pop      ecx
0x465F70: mov      dword ptr fs:[eax], edx
0x465F73: push     0x465f95
0x465F78: lea      eax, [ebp - 0x20]
0x465F7B: mov      edx, 2
0x465F80: call     0x403a7c
0x465F85: lea      eax, [ebp + 8]
0x465F88: call     0x403a58
0x465F8D: ret      
0x465F8E: jmp      0x403518
0x465F93: jmp      0x465f78
0x465F95: pop      ebx
0x465F96: mov      esp, ebp
0x465F98: pop      ebp
0x465F99: ret      4
0x465F9C: push     ebp
0x465F9D: mov      ebp, esp
0x465F9F: add      esp, -0x20
0x465FA2: push     ebx
0x465FA3: xor      ebx, ebx
0x465FA5: mov      dword ptr [ebp - 0x1c], ebx
0x465FA8: mov      dword ptr [ebp - 0x20], ebx
0x465FAB: mov      dword ptr [ebp - 0xc], ecx
0x465FAE: mov      dword ptr [ebp - 8], edx
0x465FB1: mov      dword ptr [ebp - 4], eax
0x465FB4: mov      eax, dword ptr [ebp + 8]
0x465FB7: call     0x403e88
0x465FBC: xor      eax, eax
0x465FBE: push     ebp
0x465FBF: push     0x46603a
0x465FC4: push     dword ptr fs:[eax]
0x465FC7: mov      dword ptr fs:[eax], esp
0x465FCA: mov      eax, dword ptr [ebp - 0xc]
0x465FCD: call     0x406970
0x465FD2: push     eax
0x465FD3: push     0
0x465FD5: lea      eax, [ebp - 0x18]
0x465FD8: push     eax
0x465FD9: mov      cx, word ptr [ebp - 0xc]
0x465FDD: mov      edx, dword ptr [ebp - 8]
0x465FE0: mov      ax, 0x3f5
0x465FE4: call     0x458d3c
0x465FE9: lea      edx, [ebp - 0x1c]
0x465FEC: lea      eax, [ebp - 0x18]
0x465FEF: call     0x45b41c
0x465FF4: lea      eax, [ebp - 0x1c]
0x465FF7: push     eax
0x465FF8: lea      edx, [ebp - 0x20]
0x465FFB: mov      eax, dword ptr [ebp + 8]
0x465FFE: call     0x45b4a8
0x466003: mov      edx, dword ptr [ebp - 0x20]
0x466006: pop      eax
0x466007: call     0x403cdc
0x46600C: mov      edx, dword ptr [ebp - 0x1c]
0x46600F: mov      eax, dword ptr [ebp - 4]
0x466012: call     0x464a40
0x466017: xor      eax, eax
0x466019: pop      edx
0x46601A: pop      ecx
0x46601B: pop      ecx
0x46601C: mov      dword ptr fs:[eax], edx
0x46601F: push     0x466041
0x466024: lea      eax, [ebp - 0x20]
0x466027: mov      edx, 2
0x46602C: call     0x403a7c
0x466031: lea      eax, [ebp + 8]
0x466034: call     0x403a58
0x466039: ret      
0x46603A: jmp      0x403518
0x46603F: jmp      0x466024
0x466041: pop      ebx
0x466042: mov      esp, ebp
0x466044: pop      ebp
0x466045: ret      4
0x466048: push     ebp
0x466049: mov      ebp, esp
0x46604B: add      esp, -0x20
0x46604E: push     ebx
0x46604F: xor      ebx, ebx
0x466051: mov      dword ptr [ebp - 0x1c], ebx
0x466054: mov      dword ptr [ebp - 0x20], ebx
0x466057: mov      dword ptr [ebp - 0xc], ecx
0x46605A: mov      dword ptr [ebp - 8], edx
0x46605D: mov      dword ptr [ebp - 4], eax
0x466060: mov      eax, dword ptr [ebp + 8]
0x466063: call     0x403e88
0x466068: xor      eax, eax
0x46606A: push     ebp
0x46606B: push     0x4660e6
0x466070: push     dword ptr fs:[eax]
0x466073: mov      dword ptr fs:[eax], esp
0x466076: mov      eax, dword ptr [ebp - 0xc]
0x466079: call     0x406970
0x46607E: push     eax
0x46607F: push     0
0x466081: lea      eax, [ebp - 0x18]
0x466084: push     eax
0x466085: mov      cx, word ptr [ebp - 0xc]
0x466089: mov      edx, dword ptr [ebp - 8]
0x46608C: mov      ax, 0x3ff
0x466090: call     0x458d3c
0x466095: lea      edx, [ebp - 0x1c]
0x466098: lea      eax, [ebp - 0x18]
0x46609B: call     0x45b41c
0x4660A0: lea      eax, [ebp - 0x1c]
0x4660A3: push     eax
0x4660A4: lea      edx, [ebp - 0x20]
0x4660A7: mov      eax, dword ptr [ebp + 8]
0x4660AA: call     0x45b4a8
0x4660AF: mov      edx, dword ptr [ebp - 0x20]
0x4660B2: pop      eax
0x4660B3: call     0x403cdc
0x4660B8: mov      edx, dword ptr [ebp - 0x1c]
0x4660BB: mov      eax, dword ptr [ebp - 4]
0x4660BE: call     0x464a40
0x4660C3: xor      eax, eax
0x4660C5: pop      edx
0x4660C6: pop      ecx
0x4660C7: pop      ecx
0x4660C8: mov      dword ptr fs:[eax], edx
0x4660CB: push     0x4660ed
0x4660D0: lea      eax, [ebp - 0x20]
0x4660D3: mov      edx, 2
0x4660D8: call     0x403a7c
0x4660DD: lea      eax, [ebp + 8]
0x4660E0: call     0x403a58
0x4660E5: ret      
0x4660E6: jmp      0x403518
0x4660EB: jmp      0x4660d0
0x4660ED: pop      ebx
0x4660EE: mov      esp, ebp
0x4660F0: pop      ebp
0x4660F1: ret      4
0x4660F4: push     ebp
0x4660F5: mov      ebp, esp
0x4660F7: add      esp, -0x20
0x4660FA: push     ebx
0x4660FB: xor      ebx, ebx
0x4660FD: mov      dword ptr [ebp - 0x1c], ebx
0x466100: mov      dword ptr [ebp - 0x20], ebx
0x466103: mov      dword ptr [ebp - 0xc], ecx
0x466106: mov      dword ptr [ebp - 8], edx
0x466109: mov      dword ptr [ebp - 4], eax
0x46610C: mov      eax, dword ptr [ebp + 8]
0x46610F: call     0x403e88
0x466114: xor      eax, eax
0x466116: push     ebp
0x466117: push     0x466192
0x46611C: push     dword ptr fs:[eax]
0x46611F: mov      dword ptr fs:[eax], esp
0x466122: mov      eax, dword ptr [ebp - 0xc]
0x466125: call     0x406970
0x46612A: push     eax
0x46612B: push     0
0x46612D: lea      eax, [ebp - 0x18]
0x466130: push     eax
0x466131: mov      cx, word ptr [ebp - 0xc]
0x466135: mov      edx, dword ptr [ebp - 8]
0x466138: mov      ax, 0x407
0x46613C: call     0x458d3c
0x466141: lea      edx, [ebp - 0x1c]
0x466144: lea      eax, [ebp - 0x18]
0x466147: call     0x45b41c
0x46614C: lea      eax, [ebp - 0x1c]
0x46614F: push     eax
0x466150: lea      edx, [ebp - 0x20]
0x466153: mov      eax, dword ptr [ebp + 8]
0x466156: call     0x45b4a8
0x46615B: mov      edx, dword ptr [ebp - 0x20]
0x46615E: pop      eax
0x46615F: call     0x403cdc
0x466164: mov      edx, dword ptr [ebp - 0x1c]
0x466167: mov      eax, dword ptr [ebp - 4]
0x46616A: call     0x464a40
0x46616F: xor      eax, eax
0x466171: pop      edx
0x466172: pop      ecx
0x466173: pop      ecx
0x466174: mov      dword ptr fs:[eax], edx
0x466177: push     0x466199
0x46617C: lea      eax, [ebp - 0x20]
0x46617F: mov      edx, 2
0x466184: call     0x403a7c
0x466189: lea      eax, [ebp + 8]
0x46618C: call     0x403a58
0x466191: ret      
0x466192: jmp      0x403518
0x466197: jmp      0x46617c
0x466199: pop      ebx
0x46619A: mov      esp, ebp
0x46619C: pop      ebp
0x46619D: ret      4
0x4661A0: push     ebp
0x4661A1: mov      ebp, esp
0x4661A3: add      esp, -0x20
0x4661A6: push     ebx
0x4661A7: xor      ebx, ebx
0x4661A9: mov      dword ptr [ebp - 0x1c], ebx
0x4661AC: mov      dword ptr [ebp - 0x20], ebx
0x4661AF: mov      dword ptr [ebp - 0xc], ecx
0x4661B2: mov      dword ptr [ebp - 8], edx
0x4661B5: mov      dword ptr [ebp - 4], eax
0x4661B8: mov      eax, dword ptr [ebp + 8]
0x4661BB: call     0x403e88
0x4661C0: xor      eax, eax
0x4661C2: push     ebp
0x4661C3: push     0x466237
0x4661C8: push     dword ptr fs:[eax]
0x4661CB: mov      dword ptr fs:[eax], esp
0x4661CE: push     0
0x4661D0: push     0
0x4661D2: lea      eax, [ebp - 0x18]
0x4661D5: push     eax
0x4661D6: mov      cx, word ptr [ebp - 0xc]
0x4661DA: mov      edx, dword ptr [ebp - 8]
0x4661DD: mov      ax, 0x3f7
0x4661E1: call     0x458d3c
0x4661E6: lea      edx, [ebp - 0x1c]
0x4661E9: lea      eax, [ebp - 0x18]
0x4661EC: call     0x45b41c
0x4661F1: lea      eax, [ebp - 0x1c]
0x4661F4: push     eax
0x4661F5: lea      edx, [ebp - 0x20]
0x4661F8: mov      eax, dword ptr [ebp + 8]
0x4661FB: call     0x45b4a8
0x466200: mov      edx, dword ptr [ebp - 0x20]
0x466203: pop      eax
0x466204: call     0x403cdc
0x466209: mov      edx, dword ptr [ebp - 0x1c]
0x46620C: mov      eax, dword ptr [ebp - 4]
0x46620F: call     0x464a40
0x466214: xor      eax, eax
0x466216: pop      edx
0x466217: pop      ecx
0x466218: pop      ecx
0x466219: mov      dword ptr fs:[eax], edx
0x46621C: push     0x46623e
0x466221: lea      eax, [ebp - 0x20]
0x466224: mov      edx, 2
0x466229: call     0x403a7c
0x46622E: lea      eax, [ebp + 8]
0x466231: call     0x403a58
0x466236: ret      
0x466237: jmp      0x403518
0x46623C: jmp      0x466221
0x46623E: pop      ebx
0x46623F: mov      esp, ebp
0x466241: pop      ebp
0x466242: ret      4
0x466245: lea      eax, [eax]
0x466248: push     ebp
0x466249: mov      ebp, esp
0x46624B: add      esp, -0x20
0x46624E: push     ebx
0x46624F: xor      ebx, ebx
0x466251: mov      dword ptr [ebp - 0x1c], ebx
0x466254: mov      dword ptr [ebp - 0x20], ebx
0x466257: mov      dword ptr [ebp - 0xc], ecx
0x46625A: mov      dword ptr [ebp - 8], edx
0x46625D: mov      dword ptr [ebp - 4], eax
0x466260: mov      eax, dword ptr [ebp + 8]
0x466263: call     0x403e88
0x466268: xor      eax, eax
0x46626A: push     ebp
0x46626B: push     0x4662e6
0x466270: push     dword ptr fs:[eax]
0x466273: mov      dword ptr fs:[eax], esp
0x466276: mov      eax, dword ptr [ebp - 0xc]
0x466279: call     0x406970
0x46627E: push     eax
0x46627F: push     0
0x466281: lea      eax, [ebp - 0x18]
0x466284: push     eax
0x466285: mov      cx, word ptr [ebp - 0xc]
0x466289: mov      edx, dword ptr [ebp - 8]
0x46628C: mov      ax, 0x3f6
0x466290: call     0x458d3c
0x466295: lea      edx, [ebp - 0x1c]
0x466298: lea      eax, [ebp - 0x18]
0x46629B: call     0x45b41c
0x4662A0: lea      eax, [ebp - 0x1c]
0x4662A3: push     eax
0x4662A4: lea      edx, [ebp - 0x20]
0x4662A7: mov      eax, dword ptr [ebp + 8]
0x4662AA: call     0x45b4a8
0x4662AF: mov      edx, dword ptr [ebp - 0x20]
0x4662B2: pop      eax
0x4662B3: call     0x403cdc
0x4662B8: mov      edx, dword ptr [ebp - 0x1c]
0x4662BB: mov      eax, dword ptr [ebp - 4]
0x4662BE: call     0x464a40
0x4662C3: xor      eax, eax
0x4662C5: pop      edx
0x4662C6: pop      ecx
0x4662C7: pop      ecx
0x4662C8: mov      dword ptr fs:[eax], edx
0x4662CB: push     0x4662ed
0x4662D0: lea      eax, [ebp - 0x20]
0x4662D3: mov      edx, 2
0x4662D8: call     0x403a7c
0x4662DD: lea      eax, [ebp + 8]
0x4662E0: call     0x403a58
0x4662E5: ret      
0x4662E6: jmp      0x403518
0x4662EB: jmp      0x4662d0
0x4662ED: pop      ebx
0x4662EE: mov      esp, ebp
0x4662F0: pop      ebp
0x4662F1: ret      4
0x4662F4: push     ebp
0x4662F5: mov      ebp, esp
0x4662F7: add      esp, -0x20
0x4662FA: push     ebx
0x4662FB: xor      ebx, ebx
0x4662FD: mov      dword ptr [ebp - 0x1c], ebx
0x466300: mov      dword ptr [ebp - 0x20], ebx
0x466303: mov      dword ptr [ebp - 0xc], ecx
0x466306: mov      dword ptr [ebp - 8], edx
0x466309: mov      dword ptr [ebp - 4], eax
0x46630C: mov      eax, dword ptr [ebp + 8]
0x46630F: call     0x403e88
0x466314: xor      eax, eax
0x466316: push     ebp
0x466317: push     0x466392
0x46631C: push     dword ptr fs:[eax]
0x46631F: mov      dword ptr fs:[eax], esp
0x466322: mov      eax, dword ptr [ebp - 0xc]
0x466325: call     0x406970
0x46632A: push     eax
0x46632B: push     0
0x46632D: lea      eax, [ebp - 0x18]
0x466330: push     eax
0x466331: mov      cx, word ptr [ebp - 0xc]
0x466335: mov      edx, dword ptr [ebp - 8]
0x466338: mov      ax, 0x408
0x46633C: call     0x458d3c
0x466341: lea      edx, [ebp - 0x1c]
0x466344: lea      eax, [ebp - 0x18]
0x466347: call     0x45b41c
0x46634C: lea      eax, [ebp - 0x1c]
0x46634F: push     eax
0x466350: lea      edx, [ebp - 0x20]
0x466353: mov      eax, dword ptr [ebp + 8]
0x466356: call     0x45b4a8
0x46635B: mov      edx, dword ptr [ebp - 0x20]
0x46635E: pop      eax
0x46635F: call     0x403cdc
0x466364: mov      edx, dword ptr [ebp - 0x1c]
0x466367: mov      eax, dword ptr [ebp - 4]
0x46636A: call     0x464a40
0x46636F: xor      eax, eax
0x466371: pop      edx
0x466372: pop      ecx
0x466373: pop      ecx
0x466374: mov      dword ptr fs:[eax], edx
0x466377: push     0x466399
0x46637C: lea      eax, [ebp - 0x20]
0x46637F: mov      edx, 2
0x466384: call     0x403a7c
0x466389: lea      eax, [ebp + 8]
0x46638C: call     0x403a58
0x466391: ret      
0x466392: jmp      0x403518
0x466397: jmp      0x46637c
0x466399: pop      ebx
0x46639A: mov      esp, ebp
0x46639C: pop      ebp
0x46639D: ret      4
0x4663A0: push     ebp
0x4663A1: mov      ebp, esp
0x4663A3: add      esp, -0x20
0x4663A6: push     ebx
0x4663A7: xor      ebx, ebx
0x4663A9: mov      dword ptr [ebp - 0x1c], ebx
0x4663AC: mov      dword ptr [ebp - 0x20], ebx
0x4663AF: mov      dword ptr [ebp - 0xc], ecx
0x4663B2: mov      dword ptr [ebp - 8], edx
0x4663B5: mov      dword ptr [ebp - 4], eax
0x4663B8: mov      eax, dword ptr [ebp - 0xc]
0x4663BB: call     0x403e88
0x4663C0: xor      eax, eax
0x4663C2: push     ebp
0x4663C3: push     0x466435
0x4663C8: push     dword ptr fs:[eax]
0x4663CB: mov      dword ptr fs:[eax], esp
0x4663CE: push     0
0x4663D0: push     0
0x4663D2: lea      eax, [ebp - 0x18]
0x4663D5: push     eax
0x4663D6: xor      ecx, ecx
0x4663D8: mov      edx, dword ptr [ebp - 8]
0x4663DB: mov      ax, 0x40a
0x4663DF: call     0x458d3c
0x4663E4: lea      edx, [ebp - 0x1c]
0x4663E7: lea      eax, [ebp - 0x18]
0x4663EA: call     0x45b41c
0x4663EF: lea      eax, [ebp - 0x1c]
0x4663F2: push     eax
0x4663F3: lea      edx, [ebp - 0x20]
0x4663F6: mov      eax, dword ptr [ebp - 0xc]
0x4663F9: call     0x45b4a8
0x4663FE: mov      edx, dword ptr [ebp - 0x20]
0x466401: pop      eax
0x466402: call     0x403cdc
0x466407: mov      edx, dword ptr [ebp - 0x1c]
0x46640A: mov      eax, dword ptr [ebp - 4]
0x46640D: call     0x464a40
0x466412: xor      eax, eax
0x466414: pop      edx
0x466415: pop      ecx
0x466416: pop      ecx
0x466417: mov      dword ptr fs:[eax], edx
0x46641A: push     0x46643c
0x46641F: lea      eax, [ebp - 0x20]
0x466422: mov      edx, 2
0x466427: call     0x403a7c
0x46642C: lea      eax, [ebp - 0xc]
0x46642F: call     0x403a58
0x466434: ret      
0x466435: jmp      0x403518
0x46643A: jmp      0x46641f
0x46643C: pop      ebx
0x46643D: mov      esp, ebp
0x46643F: pop      ebp
0x466440: ret      
0x466441: lea      eax, [eax]
0x466444: push     ebp
0x466445: mov      ebp, esp
0x466447: add      esp, -0x18
0x46644A: xor      ecx, ecx
0x46644C: mov      dword ptr [ebp - 0x18], ecx
0x46644F: mov      dword ptr [ebp - 8], edx
0x466452: mov      dword ptr [ebp - 4], eax
0x466455: xor      eax, eax
0x466457: push     ebp
0x466458: push     0x4664a5
0x46645D: push     dword ptr fs:[eax]
0x466460: mov      dword ptr fs:[eax], esp
0x466463: push     0
0x466465: push     0
0x466467: lea      eax, [ebp - 0x14]
0x46646A: push     eax
0x46646B: xor      ecx, ecx
0x46646D: mov      edx, dword ptr [ebp - 8]
0x466470: mov      ax, 0x3f8
0x466474: call     0x458d3c
0x466479: lea      edx, [ebp - 0x18]
0x46647C: lea      eax, [ebp - 0x14]
0x46647F: call     0x45b41c
0x466484: mov      edx, dword ptr [ebp - 0x18]
0x466487: mov      eax, dword ptr [ebp - 4]
0x46648A: call     0x464a40
0x46648F: xor      eax, eax
0x466491: pop      edx
0x466492: pop      ecx
0x466493: pop      ecx
0x466494: mov      dword ptr fs:[eax], edx
0x466497: push     0x4664ac
0x46649C: lea      eax, [ebp - 0x18]
0x46649F: call     0x403a58
0x4664A4: ret      
0x4664A5: jmp      0x403518
0x4664AA: jmp      0x46649c
0x4664AC: mov      esp, ebp
0x4664AE: pop      ebp
0x4664AF: ret      
0x4664B0: push     ebp
0x4664B1: mov      ebp, esp
0x4664B3: add      esp, -0x18
0x4664B6: xor      ecx, ecx
0x4664B8: mov      dword ptr [ebp - 0x18], ecx
0x4664BB: mov      byte ptr [ebp - 5], dl
0x4664BE: mov      dword ptr [ebp - 4], eax
0x4664C1: xor      eax, eax
0x4664C3: push     ebp
0x4664C4: push     0x46652f
0x4664C9: push     dword ptr fs:[eax]
0x4664CC: mov      dword ptr fs:[eax], esp
0x4664CF: cmp      byte ptr [ebp - 5], 0
0x4664D3: je       0x4664ee
0x4664D5: push     0
0x4664D7: push     0
0x4664D9: lea      eax, [ebp - 0x14]
0x4664DC: push     eax
0x4664DD: mov      cx, 1
0x4664E1: xor      edx, edx
0x4664E3: mov      ax, 0x3fb
0x4664E7: call     0x458d3c
0x4664EC: jmp      0x466503
0x4664EE: push     0
0x4664F0: push     0
0x4664F2: lea      eax, [ebp - 0x14]
0x4664F5: push     eax
0x4664F6: xor      ecx, ecx
0x4664F8: xor      edx, edx
0x4664FA: mov      ax, 0x3fb
0x4664FE: call     0x458d3c
0x466503: lea      edx, [ebp - 0x18]
0x466506: lea      eax, [ebp - 0x14]
0x466509: call     0x45b41c
0x46650E: mov      edx, dword ptr [ebp - 0x18]
0x466511: mov      eax, dword ptr [ebp - 4]
0x466514: call     0x464a40
0x466519: xor      eax, eax
0x46651B: pop      edx
0x46651C: pop      ecx
0x46651D: pop      ecx
0x46651E: mov      dword ptr fs:[eax], edx
0x466521: push     0x466536
0x466526: lea      eax, [ebp - 0x18]
0x466529: call     0x403a58
0x46652E: ret      
0x46652F: jmp      0x403518
0x466534: jmp      0x466526
0x466536: mov      esp, ebp
0x466538: pop      ebp
0x466539: ret      
0x46653A: mov      eax, eax
0x46653C: push     ebp
0x46653D: mov      ebp, esp
0x46653F: add      esp, -0x1c
0x466542: xor      ecx, ecx
0x466544: mov      dword ptr [ebp - 0x18], ecx
0x466547: mov      dword ptr [ebp - 0x1c], ecx
0x46654A: mov      dword ptr [ebp - 8], edx
0x46654D: mov      dword ptr [ebp - 4], eax
0x466550: mov      eax, dword ptr [ebp - 8]
0x466553: call     0x403e88
0x466558: xor      eax, eax
0x46655A: push     ebp
0x46655B: push     0x4665d2
0x466560: push     dword ptr fs:[eax]
0x466563: mov      dword ptr fs:[eax], esp
0x466566: cmp      dword ptr [ebp - 8], 0
0x46656A: je       0x4665af
0x46656C: push     0
0x46656E: push     0
0x466570: lea      eax, [ebp - 0x14]
0x466573: push     eax
0x466574: xor      ecx, ecx
0x466576: xor      edx, edx
0x466578: mov      ax, 0x3fc
0x46657C: call     0x458d3c
0x466581: lea      edx, [ebp - 0x18]
0x466584: lea      eax, [ebp - 0x14]
0x466587: call     0x45b41c
0x46658C: lea      eax, [ebp - 0x18]
0x46658F: push     eax
0x466590: lea      edx, [ebp - 0x1c]
0x466593: mov      eax, dword ptr [ebp - 8]
0x466596: call     0x45b4a8
0x46659B: mov      edx, dword ptr [ebp - 0x1c]
0x46659E: pop      eax
0x46659F: call     0x403cdc
0x4665A4: mov      edx, dword ptr [ebp - 0x18]
0x4665A7: mov      eax, dword ptr [ebp - 4]
0x4665AA: call     0x464a40
0x4665AF: xor      eax, eax
0x4665B1: pop      edx
0x4665B2: pop      ecx
0x4665B3: pop      ecx
0x4665B4: mov      dword ptr fs:[eax], edx
0x4665B7: push     0x4665d9
0x4665BC: lea      eax, [ebp - 0x1c]
0x4665BF: mov      edx, 2
0x4665C4: call     0x403a7c
0x4665C9: lea      eax, [ebp - 8]
0x4665CC: call     0x403a58
0x4665D1: ret      
0x4665D2: jmp      0x403518
0x4665D7: jmp      0x4665bc
0x4665D9: mov      esp, ebp
0x4665DB: pop      ebp
0x4665DC: ret      
0x4665DD: lea      eax, [eax]
0x4665E0: push     ebp
0x4665E1: mov      ebp, esp
0x4665E3: add      esp, -0x14
0x4665E6: xor      edx, edx
0x4665E8: mov      dword ptr [ebp - 0x14], edx
0x4665EB: mov      dword ptr [ebp - 4], eax
0x4665EE: xor      eax, eax
0x4665F0: push     ebp
0x4665F1: push     0x46663d
0x4665F6: push     dword ptr fs:[eax]
0x4665F9: mov      dword ptr fs:[eax], esp
0x4665FC: push     0
0x4665FE: push     0
0x466600: lea      eax, [ebp - 0x10]
0x466603: push     eax
0x466604: xor      ecx, ecx
0x466606: xor      edx, edx
0x466608: mov      ax, 0x409
0x46660C: call     0x458d3c
0x466611: lea      edx, [ebp - 0x14]
0x466614: lea      eax, [ebp - 0x10]
0x466617: call     0x45b41c
0x46661C: mov      edx, dword ptr [ebp - 0x14]
0x46661F: mov      eax, dword ptr [ebp - 4]
0x466622: call     0x464a40
0x466627: xor      eax, eax
0x466629: pop      edx
0x46662A: pop      ecx
0x46662B: pop      ecx
0x46662C: mov      dword ptr fs:[eax], edx
0x46662F: push     0x466644
0x466634: lea      eax, [ebp - 0x14]
0x466637: call     0x403a58
0x46663C: ret      
0x46663D: jmp      0x403518
0x466642: jmp      0x466634
0x466644: mov      esp, ebp
0x466646: pop      ebp
0x466647: ret      
0x466648: push     ebp
0x466649: mov      ebp, esp
0x46664B: add      esp, -0x30
0x46664E: xor      edx, edx
0x466650: mov      dword ptr [ebp - 0x2c], edx
0x466653: mov      dword ptr [ebp - 0x30], edx
0x466656: mov      dword ptr [ebp - 8], edx
0x466659: mov      dword ptr [ebp - 4], eax
0x46665C: xor      eax, eax
0x46665E: push     ebp
0x46665F: push     0x4666d0
0x466664: push     dword ptr fs:[eax]
0x466667: mov      dword ptr fs:[eax], esp
0x46666A: push     0
0x46666C: push     0
0x46666E: lea      eax, [ebp - 0x14]
0x466671: push     eax
0x466672: xor      ecx, ecx
0x466674: xor      edx, edx
0x466676: mov      ax, 0x401
0x46667A: call     0x458d3c
0x46667F: lea      edx, [ebp - 0x2c]
0x466682: lea      eax, [ebp - 0x14]
0x466685: call     0x45b41c
0x46668A: lea      eax, [ebp - 0x2c]
0x46668D: push     eax
0x46668E: lea      edx, [ebp - 0x30]
0x466691: mov      eax, dword ptr [ebp - 8]
0x466694: call     0x45b4a8
0x466699: mov      edx, dword ptr [ebp - 0x30]
0x46669C: pop      eax
0x46669D: call     0x403cdc
0x4666A2: mov      edx, dword ptr [ebp - 0x2c]
0x4666A5: mov      eax, dword ptr [ebp - 4]
0x4666A8: call     0x464a40
0x4666AD: xor      eax, eax
0x4666AF: pop      edx
0x4666B0: pop      ecx
0x4666B1: pop      ecx
0x4666B2: mov      dword ptr fs:[eax], edx
0x4666B5: push     0x4666d7
0x4666BA: lea      eax, [ebp - 0x30]
0x4666BD: mov      edx, 2
0x4666C2: call     0x403a7c
0x4666C7: lea      eax, [ebp - 8]
0x4666CA: call     0x403a58
0x4666CF: ret      
0x4666D0: jmp      0x403518
0x4666D5: jmp      0x4666ba
0x4666D7: mov      esp, ebp
0x4666D9: pop      ebp
0x4666DA: ret      
0x4666DB: nop      
0x4666DC: push     ebp
0x4666DD: mov      ebp, esp
0x4666DF: add      esp, -0x14
0x4666E2: xor      edx, edx
0x4666E4: mov      dword ptr [ebp - 0x14], edx
0x4666E7: mov      dword ptr [ebp - 4], eax
0x4666EA: xor      eax, eax
0x4666EC: push     ebp
0x4666ED: push     0x466739
0x4666F2: push     dword ptr fs:[eax]
0x4666F5: mov      dword ptr fs:[eax], esp
0x4666F8: push     0
0x4666FA: push     0
0x4666FC: lea      eax, [ebp - 0x10]
0x4666FF: push     eax
0x466700: xor      ecx, ecx
0x466702: xor      edx, edx
0x466704: mov      ax, 0x40b
0x466708: call     0x458d3c
0x46670D: lea      edx, [ebp - 0x14]
0x466710: lea      eax, [ebp - 0x10]
0x466713: call     0x45b41c
0x466718: mov      edx, dword ptr [ebp - 0x14]
0x46671B: mov      eax, dword ptr [ebp - 4]
0x46671E: call     0x464a40
0x466723: xor      eax, eax
0x466725: pop      edx
0x466726: pop      ecx
0x466727: pop      ecx
0x466728: mov      dword ptr fs:[eax], edx
0x46672B: push     0x466740
0x466730: lea      eax, [ebp - 0x14]
0x466733: call     0x403a58
0x466738: ret      
0x466739: jmp      0x403518
0x46673E: jmp      0x466730
0x466740: mov      esp, ebp
0x466742: pop      ebp
0x466743: ret      
0x466744: push     ebp
0x466745: mov      ebp, esp
0x466747: add      esp, -0x14
0x46674A: xor      edx, edx
0x46674C: mov      dword ptr [ebp - 0x14], edx
0x46674F: mov      dword ptr [ebp - 4], eax
0x466752: xor      eax, eax
0x466754: push     ebp
0x466755: push     0x4667a1
0x46675A: push     dword ptr fs:[eax]
0x46675D: mov      dword ptr fs:[eax], esp
0x466760: push     0
0x466762: push     0
0x466764: lea      eax, [ebp - 0x10]
0x466767: push     eax
0x466768: xor      ecx, ecx
0x46676A: xor      edx, edx
0x46676C: mov      ax, 0x404
0x466770: call     0x458d3c
0x466775: lea      edx, [ebp - 0x14]
0x466778: lea      eax, [ebp - 0x10]
0x46677B: call     0x45b41c
0x466780: mov      edx, dword ptr [ebp - 0x14]
0x466783: mov      eax, dword ptr [ebp - 4]
0x466786: call     0x464a40
0x46678B: xor      eax, eax
0x46678D: pop      edx
0x46678E: pop      ecx
0x46678F: pop      ecx
0x466790: mov      dword ptr fs:[eax], edx
0x466793: push     0x4667a8
0x466798: lea      eax, [ebp - 0x14]
0x46679B: call     0x403a58
0x4667A0: ret      
0x4667A1: jmp      0x403518
0x4667A6: jmp      0x466798
0x4667A8: mov      esp, ebp
0x4667AA: pop      ebp
0x4667AB: ret      
0x4667AC: push     ebp
0x4667AD: mov      ebp, esp
0x4667AF: add      esp, -0x50
0x4667B2: push     esi
0x4667B3: push     edi
0x4667B4: xor      ecx, ecx
0x4667B6: mov      dword ptr [ebp - 0x48], ecx
0x4667B9: mov      dword ptr [ebp - 0x4c], ecx
0x4667BC: mov      dword ptr [ebp - 0x50], ecx
0x4667BF: mov      esi, edx
0x4667C1: lea      edi, [ebp - 0x38]
0x4667C4: mov      ecx, 0xd
0x4667C9: rep movsd dword ptr es:[edi], dword ptr [esi]
0x4667CB: mov      dword ptr [ebp - 4], eax
0x4667CE: xor      eax, eax
0x4667D0: push     ebp
0x4667D1: push     0x466846
0x4667D6: push     dword ptr fs:[eax]
0x4667D9: mov      dword ptr fs:[eax], esp
0x4667DC: push     0
0x4667DE: push     0
0x4667E0: lea      eax, [ebp - 0x44]
0x4667E3: push     eax
0x4667E4: xor      ecx, ecx
0x4667E6: mov      edx, dword ptr [ebp - 0xc]
0x4667E9: mov      ax, 0x402
0x4667ED: call     0x458d3c
0x4667F2: lea      edx, [ebp - 0x48]
0x4667F5: lea      eax, [ebp - 0x44]
0x4667F8: call     0x45b41c
0x4667FD: lea      eax, [ebp - 0x48]
0x466800: push     eax
0x466801: lea      eax, [ebp - 0x50]
0x466804: lea      edx, [ebp - 0x38]
0x466807: call     0x403c78
0x46680C: mov      eax, dword ptr [ebp - 0x50]
0x46680F: lea      edx, [ebp - 0x4c]
0x466812: call     0x45b4a8
0x466817: mov      edx, dword ptr [ebp - 0x4c]
0x46681A: pop      eax
0x46681B: call     0x403cdc
0x466820: mov      edx, dword ptr [ebp - 0x48]
0x466823: mov      eax, dword ptr [ebp - 4]
0x466826: call     0x464a40
0x46682B: xor      eax, eax
0x46682D: pop      edx
0x46682E: pop      ecx
0x46682F: pop      ecx
0x466830: mov      dword ptr fs:[eax], edx
0x466833: push     0x46684d
0x466838: lea      eax, [ebp - 0x50]
0x46683B: mov      edx, 3
0x466840: call     0x403a7c
0x466845: ret      
0x466846: jmp      0x403518
0x46684B: jmp      0x466838
0x46684D: pop      edi
0x46684E: pop      esi
0x46684F: mov      esp, ebp
0x466851: pop      ebp
0x466852: ret      
0x466853: nop      
0x466854: push     ebp
0x466855: mov      ebp, esp
0x466857: add      esp, -0x50
0x46685A: push     esi
0x46685B: push     edi
0x46685C: xor      ecx, ecx
0x46685E: mov      dword ptr [ebp - 0x48], ecx
0x466861: mov      dword ptr [ebp - 0x4c], ecx
0x466864: mov      dword ptr [ebp - 0x50], ecx
0x466867: mov      esi, edx
0x466869: lea      edi, [ebp - 0x38]
0x46686C: mov      ecx, 0xd
0x466871: rep movsd dword ptr es:[edi], dword ptr [esi]
0x466873: mov      dword ptr [ebp - 4], eax
0x466876: xor      eax, eax
0x466878: push     ebp
0x466879: push     0x4668ee
0x46687E: push     dword ptr fs:[eax]
0x466881: mov      dword ptr fs:[eax], esp
0x466884: push     0
0x466886: push     0
0x466888: lea      eax, [ebp - 0x44]
0x46688B: push     eax
0x46688C: xor      ecx, ecx
0x46688E: mov      edx, dword ptr [ebp - 0xc]
0x466891: mov      ax, 0x403
0x466895: call     0x458d3c
0x46689A: lea      edx, [ebp - 0x48]
0x46689D: lea      eax, [ebp - 0x44]
0x4668A0: call     0x45b41c
0x4668A5: lea      eax, [ebp - 0x48]
0x4668A8: push     eax
0x4668A9: lea      eax, [ebp - 0x50]
0x4668AC: lea      edx, [ebp - 0x38]
0x4668AF: call     0x403c78
0x4668B4: mov      eax, dword ptr [ebp - 0x50]
0x4668B7: lea      edx, [ebp - 0x4c]
0x4668BA: call     0x45b4a8
0x4668BF: mov      edx, dword ptr [ebp - 0x4c]
0x4668C2: pop      eax
0x4668C3: call     0x403cdc
0x4668C8: mov      edx, dword ptr [ebp - 0x48]
0x4668CB: mov      eax, dword ptr [ebp - 4]
0x4668CE: call     0x464a40
0x4668D3: xor      eax, eax
0x4668D5: pop      edx
0x4668D6: pop      ecx
0x4668D7: pop      ecx
0x4668D8: mov      dword ptr fs:[eax], edx
0x4668DB: push     0x4668f5
0x4668E0: lea      eax, [ebp - 0x50]
0x4668E3: mov      edx, 3
0x4668E8: call     0x403a7c
0x4668ED: ret      
0x4668EE: jmp      0x403518
0x4668F3: jmp      0x4668e0
0x4668F5: pop      edi
0x4668F6: pop      esi
0x4668F7: mov      esp, ebp
0x4668F9: pop      ebp
0x4668FA: ret      
0x4668FB: nop      
0x4668FC: push     ebp
0x4668FD: mov      ebp, esp
0x4668FF: add      esp, -0x18
0x466902: xor      ecx, ecx
0x466904: mov      dword ptr [ebp - 0x18], ecx
0x466907: mov      dword ptr [ebp - 8], edx
0x46690A: mov      dword ptr [ebp - 4], eax
0x46690D: xor      eax, eax
0x46690F: push     ebp
0x466910: push     0x46695d
0x466915: push     dword ptr fs:[eax]
0x466918: mov      dword ptr fs:[eax], esp
0x46691B: push     0
0x46691D: push     0
0x46691F: lea      eax, [ebp - 0x14]
0x466922: push     eax
0x466923: xor      ecx, ecx
0x466925: mov      edx, dword ptr [ebp - 8]
0x466928: mov      ax, 0x405
0x46692C: call     0x458d3c
0x466931: lea      edx, [ebp - 0x18]
0x466934: lea      eax, [ebp - 0x14]
0x466937: call     0x45b41c
0x46693C: mov      edx, dword ptr [ebp - 0x18]
0x46693F: mov      eax, dword ptr [ebp - 4]
0x466942: call     0x464a40
0x466947: xor      eax, eax
0x466949: pop      edx
0x46694A: pop      ecx
0x46694B: pop      ecx
0x46694C: mov      dword ptr fs:[eax], edx
0x46694F: push     0x466964
0x466954: lea      eax, [ebp - 0x18]
0x466957: call     0x403a58
0x46695C: ret      
0x46695D: jmp      0x403518
0x466962: jmp      0x466954
0x466964: mov      esp, ebp
0x466966: pop      ebp
0x466967: ret      
0x466968: push     ebp
0x466969: mov      ebp, esp
0x46696B: add      esp, -0x14
0x46696E: xor      edx, edx
0x466970: mov      dword ptr [ebp - 0x14], edx
0x466973: mov      dword ptr [ebp - 4], eax
0x466976: xor      eax, eax
0x466978: push     ebp
0x466979: push     0x4669c5
0x46697E: push     dword ptr fs:[eax]
0x466981: mov      dword ptr fs:[eax], esp
0x466984: push     0
0x466986: push     0
0x466988: lea      eax, [ebp - 0x10]
0x46698B: push     eax
0x46698C: xor      ecx, ecx
0x46698E: xor      edx, edx
0x466990: mov      ax, 0x406
0x466994: call     0x458d3c
0x466999: lea      edx, [ebp - 0x14]
0x46699C: lea      eax, [ebp - 0x10]
0x46699F: call     0x45b41c
0x4669A4: mov      edx, dword ptr [ebp - 0x14]
0x4669A7: mov      eax, dword ptr [ebp - 4]
0x4669AA: call     0x464a40
0x4669AF: xor      eax, eax
0x4669B1: pop      edx
0x4669B2: pop      ecx
0x4669B3: pop      ecx
0x4669B4: mov      dword ptr fs:[eax], edx
0x4669B7: push     0x4669cc
0x4669BC: lea      eax, [ebp - 0x14]
0x4669BF: call     0x403a58
0x4669C4: ret      
0x4669C5: jmp      0x403518
0x4669CA: jmp      0x4669bc
0x4669CC: mov      esp, ebp
0x4669CE: pop      ebp
0x4669CF: ret      
0x4669D0: push     ebp
0x4669D1: mov      ebp, esp
0x4669D3: add      esp, -0x1c
0x4669D6: xor      ecx, ecx
0x4669D8: mov      dword ptr [ebp - 0x18], ecx
0x4669DB: mov      dword ptr [ebp - 0x1c], ecx
0x4669DE: mov      dword ptr [ebp - 8], edx
0x4669E1: mov      dword ptr [ebp - 4], eax
0x4669E4: mov      eax, dword ptr [ebp - 8]
0x4669E7: call     0x403e88
0x4669EC: xor      eax, eax
0x4669EE: push     ebp
0x4669EF: push     0x466a66
0x4669F4: push     dword ptr fs:[eax]
0x4669F7: mov      dword ptr fs:[eax], esp
0x4669FA: cmp      dword ptr [ebp - 8], 0
0x4669FE: je       0x466a43
0x466A00: push     0
0x466A02: push     0
0x466A04: lea      eax, [ebp - 0x14]
0x466A07: push     eax
0x466A08: xor      ecx, ecx
0x466A0A: xor      edx, edx
0x466A0C: mov      ax, 0x3fd
0x466A10: call     0x458d3c
0x466A15: lea      edx, [ebp - 0x18]
0x466A18: lea      eax, [ebp - 0x14]
0x466A1B: call     0x45b41c
0x466A20: lea      eax, [ebp - 0x18]
0x466A23: push     eax
0x466A24: lea      edx, [ebp - 0x1c]
0x466A27: mov      eax, dword ptr [ebp - 8]
0x466A2A: call     0x45b4a8
0x466A2F: mov      edx, dword ptr [ebp - 0x1c]
0x466A32: pop      eax
0x466A33: call     0x403cdc
0x466A38: mov      edx, dword ptr [ebp - 0x18]
0x466A3B: mov      eax, dword ptr [ebp - 4]
0x466A3E: call     0x464a40
0x466A43: xor      eax, eax
0x466A45: pop      edx
0x466A46: pop      ecx
0x466A47: pop      ecx
0x466A48: mov      dword ptr fs:[eax], edx
0x466A4B: push     0x466a6d
0x466A50: lea      eax, [ebp - 0x1c]
0x466A53: mov      edx, 2
0x466A58: call     0x403a7c
0x466A5D: lea      eax, [ebp - 8]
0x466A60: call     0x403a58
0x466A65: ret      
0x466A66: jmp      0x403518
0x466A6B: jmp      0x466a50
0x466A6D: mov      esp, ebp
0x466A6F: pop      ebp
0x466A70: ret      
0x466A71: lea      eax, [eax]
0x466A74: push     ebp
0x466A75: mov      ebp, esp
0x466A77: add      esp, -0x1c
0x466A7A: xor      ecx, ecx
0x466A7C: mov      dword ptr [ebp - 0x18], ecx
0x466A7F: mov      dword ptr [ebp - 0x1c], ecx
0x466A82: mov      dword ptr [ebp - 8], edx
0x466A85: mov      dword ptr [ebp - 4], eax
0x466A88: mov      eax, dword ptr [ebp - 8]
0x466A8B: call     0x403e88
0x466A90: xor      eax, eax
0x466A92: push     ebp
0x466A93: push     0x466b0a
0x466A98: push     dword ptr fs:[eax]
0x466A9B: mov      dword ptr fs:[eax], esp
0x466A9E: cmp      dword ptr [ebp - 8], 0
0x466AA2: je       0x466ae7
0x466AA4: push     0
0x466AA6: push     0
0x466AA8: lea      eax, [ebp - 0x14]
0x466AAB: push     eax
0x466AAC: xor      ecx, ecx
0x466AAE: xor      edx, edx
0x466AB0: mov      ax, 0x3fe
0x466AB4: call     0x458d3c
0x466AB9: lea      edx, [ebp - 0x18]
0x466ABC: lea      eax, [ebp - 0x14]
0x466ABF: call     0x45b41c
0x466AC4: lea      eax, [ebp - 0x18]
0x466AC7: push     eax
0x466AC8: lea      edx, [ebp - 0x1c]
0x466ACB: mov      eax, dword ptr [ebp - 8]
0x466ACE: call     0x45b4a8
0x466AD3: mov      edx, dword ptr [ebp - 0x1c]
0x466AD6: pop      eax
0x466AD7: call     0x403cdc
0x466ADC: mov      edx, dword ptr [ebp - 0x18]
0x466ADF: mov      eax, dword ptr [ebp - 4]
0x466AE2: call     0x464a40
0x466AE7: xor      eax, eax
0x466AE9: pop      edx
0x466AEA: pop      ecx
0x466AEB: pop      ecx
0x466AEC: mov      dword ptr fs:[eax], edx
0x466AEF: push     0x466b11
0x466AF4: lea      eax, [ebp - 0x1c]
0x466AF7: mov      edx, 2
0x466AFC: call     0x403a7c
0x466B01: lea      eax, [ebp - 8]
0x466B04: call     0x403a58
0x466B09: ret      
0x466B0A: jmp      0x403518
0x466B0F: jmp      0x466af4
0x466B11: mov      esp, ebp
0x466B13: pop      ebp
0x466B14: ret      
0x466B15: lea      eax, [eax]
0x466B18: push     ebp
0x466B19: mov      ebp, esp
0x466B1B: add      esp, -0x14
0x466B1E: xor      edx, edx
0x466B20: mov      dword ptr [ebp - 0x14], edx
0x466B23: mov      dword ptr [ebp - 4], eax
0x466B26: xor      eax, eax
0x466B28: push     ebp
0x466B29: push     0x466b75
0x466B2E: push     dword ptr fs:[eax]
0x466B31: mov      dword ptr fs:[eax], esp
0x466B34: push     0
0x466B36: push     0
0x466B38: lea      eax, [ebp - 0x10]
0x466B3B: push     eax
0x466B3C: xor      ecx, ecx
0x466B3E: xor      edx, edx
0x466B40: mov      ax, 0x40c
0x466B44: call     0x458d3c
0x466B49: lea      edx, [ebp - 0x14]
0x466B4C: lea      eax, [ebp - 0x10]
0x466B4F: call     0x45b41c
0x466B54: mov      edx, dword ptr [ebp - 0x14]
0x466B57: mov      eax, dword ptr [ebp - 4]
0x466B5A: call     0x464a40
0x466B5F: xor      eax, eax
0x466B61: pop      edx
0x466B62: pop      ecx
0x466B63: pop      ecx
0x466B64: mov      dword ptr fs:[eax], edx
0x466B67: push     0x466b7c
0x466B6C: lea      eax, [ebp - 0x14]
0x466B6F: call     0x403a58
0x466B74: ret      
0x466B75: jmp      0x403518
0x466B7A: jmp      0x466b6c
0x466B7C: mov      esp, ebp
0x466B7E: pop      ebp
0x466B7F: ret      
0x466B80: push     ebp
0x466B81: mov      ebp, esp
0x466B83: add      esp, -0x14
0x466B86: xor      edx, edx
0x466B88: mov      dword ptr [ebp - 0x14], edx
0x466B8B: mov      dword ptr [ebp - 4], eax
0x466B8E: xor      eax, eax
0x466B90: push     ebp
0x466B91: push     0x466bdd
0x466B96: push     dword ptr fs:[eax]
0x466B99: mov      dword ptr fs:[eax], esp
0x466B9C: push     0
0x466B9E: push     0
0x466BA0: lea      eax, [ebp - 0x10]
0x466BA3: push     eax
0x466BA4: xor      ecx, ecx
0x466BA6: xor      edx, edx
0x466BA8: mov      ax, 0x40d
0x466BAC: call     0x458d3c
0x466BB1: lea      edx, [ebp - 0x14]
0x466BB4: lea      eax, [ebp - 0x10]
0x466BB7: call     0x45b41c
0x466BBC: mov      edx, dword ptr [ebp - 0x14]
0x466BBF: mov      eax, dword ptr [ebp - 4]
0x466BC2: call     0x464a40
0x466BC7: xor      eax, eax
0x466BC9: pop      edx
0x466BCA: pop      ecx
0x466BCB: pop      ecx
0x466BCC: mov      dword ptr fs:[eax], edx
0x466BCF: push     0x466be4
0x466BD4: lea      eax, [ebp - 0x14]
0x466BD7: call     0x403a58
0x466BDC: ret      
0x466BDD: jmp      0x403518
0x466BE2: jmp      0x466bd4
0x466BE4: mov      esp, ebp
0x466BE6: pop      ebp
0x466BE7: ret      
0x466BE8: push     ebp
0x466BE9: mov      ebp, esp
0x466BEB: add      esp, -0x1c
0x466BEE: xor      ecx, ecx
0x466BF0: mov      dword ptr [ebp - 0x18], ecx
0x466BF3: mov      dword ptr [ebp - 0x1c], ecx
0x466BF6: mov      dword ptr [ebp - 8], edx
0x466BF9: mov      dword ptr [ebp - 4], eax
0x466BFC: mov      eax, dword ptr [ebp - 8]
0x466BFF: call     0x403e88
0x466C04: xor      eax, eax
0x466C06: push     ebp
0x466C07: push     0x466c89
0x466C0C: push     dword ptr fs:[eax]
0x466C0F: mov      dword ptr fs:[eax], esp
0x466C12: lea      edx, [ebp - 0x18]
0x466C15: mov      eax, dword ptr [ebp - 8]
0x466C18: call     0x407be4
0x466C1D: cmp      dword ptr [ebp - 0x18], 0
0x466C21: je       0x466c66
0x466C23: push     0
0x466C25: push     0
0x466C27: lea      eax, [ebp - 0x14]
0x466C2A: push     eax
0x466C2B: xor      ecx, ecx
0x466C2D: xor      edx, edx
0x466C2F: mov      ax, 0x40e
0x466C33: call     0x458d3c
0x466C38: lea      edx, [ebp - 0x18]
0x466C3B: lea      eax, [ebp - 0x14]
0x466C3E: call     0x45b41c
0x466C43: lea      eax, [ebp - 0x18]
0x466C46: push     eax
0x466C47: lea      edx, [ebp - 0x1c]
0x466C4A: mov      eax, dword ptr [ebp - 8]
0x466C4D: call     0x45b4a8
0x466C52: mov      edx, dword ptr [ebp - 0x1c]
0x466C55: pop      eax
0x466C56: call     0x403cdc
0x466C5B: mov      edx, dword ptr [ebp - 0x18]
0x466C5E: mov      eax, dword ptr [ebp - 4]
0x466C61: call     0x464a40
0x466C66: xor      eax, eax
0x466C68: pop      edx
0x466C69: pop      ecx
0x466C6A: pop      ecx
0x466C6B: mov      dword ptr fs:[eax], edx
0x466C6E: push     0x466c90
0x466C73: lea      eax, [ebp - 0x1c]
0x466C76: mov      edx, 2
0x466C7B: call     0x403a7c
0x466C80: lea      eax, [ebp - 8]
0x466C83: call     0x403a58
0x466C88: ret      
0x466C89: jmp      0x403518
0x466C8E: jmp      0x466c73
0x466C90: mov      esp, ebp
0x466C92: pop      ebp
0x466C93: ret      
0x466C94: push     ebp
0x466C95: mov      ebp, esp
0x466C97: add      esp, -0x1c
0x466C9A: xor      ecx, ecx
0x466C9C: mov      dword ptr [ebp - 0x18], ecx
0x466C9F: mov      dword ptr [ebp - 0x1c], ecx
0x466CA2: mov      dword ptr [ebp - 8], edx
0x466CA5: mov      dword ptr [ebp - 4], eax
0x466CA8: mov      eax, dword ptr [ebp - 8]
0x466CAB: call     0x403e88
0x466CB0: xor      eax, eax
0x466CB2: push     ebp
0x466CB3: push     0x466d35
0x466CB8: push     dword ptr fs:[eax]
0x466CBB: mov      dword ptr fs:[eax], esp
0x466CBE: lea      edx, [ebp - 0x18]
0x466CC1: mov      eax, dword ptr [ebp - 8]
0x466CC4: call     0x407be4
0x466CC9: cmp      dword ptr [ebp - 0x18], 0
0x466CCD: je       0x466d12
0x466CCF: push     0
0x466CD1: push     0
0x466CD3: lea      eax, [ebp - 0x14]
0x466CD6: push     eax
0x466CD7: xor      ecx, ecx
0x466CD9: xor      edx, edx
0x466CDB: mov      ax, 0x40f
0x466CDF: call     0x458d3c
0x466CE4: lea      edx, [ebp - 0x18]
0x466CE7: lea      eax, [ebp - 0x14]
0x466CEA: call     0x45b41c
0x466CEF: lea      eax, [ebp - 0x18]
0x466CF2: push     eax
0x466CF3: lea      edx, [ebp - 0x1c]
0x466CF6: mov      eax, dword ptr [ebp - 8]
0x466CF9: call     0x45b4a8
0x466CFE: mov      edx, dword ptr [ebp - 0x1c]
0x466D01: pop      eax
0x466D02: call     0x403cdc
0x466D07: mov      edx, dword ptr [ebp - 0x18]
0x466D0A: mov      eax, dword ptr [ebp - 4]
0x466D0D: call     0x464a40
0x466D12: xor      eax, eax
0x466D14: pop      edx
0x466D15: pop      ecx
0x466D16: pop      ecx
0x466D17: mov      dword ptr fs:[eax], edx
0x466D1A: push     0x466d3c
0x466D1F: lea      eax, [ebp - 0x1c]
0x466D22: mov      edx, 2
0x466D27: call     0x403a7c
0x466D2C: lea      eax, [ebp - 8]
0x466D2F: call     0x403a58
0x466D34: ret      
0x466D35: jmp      0x403518
0x466D3A: jmp      0x466d1f
0x466D3C: mov      esp, ebp
0x466D3E: pop      ebp
0x466D3F: ret      
0x466D40: push     ebp
0x466D41: mov      ebp, esp
0x466D43: add      esp, -0x1c
0x466D46: xor      ecx, ecx
0x466D48: mov      dword ptr [ebp - 0x18], ecx
0x466D4B: mov      dword ptr [ebp - 0x1c], ecx
0x466D4E: mov      dword ptr [ebp - 8], edx
0x466D51: mov      dword ptr [ebp - 4], eax
0x466D54: mov      eax, dword ptr [ebp - 8]
0x466D57: call     0x403e88
0x466D5C: xor      eax, eax
0x466D5E: push     ebp
0x466D5F: push     0x466dd0
0x466D64: push     dword ptr fs:[eax]
0x466D67: mov      dword ptr fs:[eax], esp
0x466D6A: push     0
0x466D6C: push     0
0x466D6E: lea      eax, [ebp - 0x14]
0x466D71: push     eax
0x466D72: xor      ecx, ecx
0x466D74: xor      edx, edx
0x466D76: mov      ax, 0x410
0x466D7A: call     0x458d3c
0x466D7F: lea      edx, [ebp - 0x18]
0x466D82: lea      eax, [ebp - 0x14]
0x466D85: call     0x45b41c
0x466D8A: lea      eax, [ebp - 0x18]
0x466D8D: push     eax
0x466D8E: lea      edx, [ebp - 0x1c]
0x466D91: mov      eax, dword ptr [ebp - 8]
0x466D94: call     0x45b4a8
0x466D99: mov      edx, dword ptr [ebp - 0x1c]
0x466D9C: pop      eax
0x466D9D: call     0x403cdc
0x466DA2: mov      edx, dword ptr [ebp - 0x18]
0x466DA5: mov      eax, dword ptr [ebp - 4]
0x466DA8: call     0x464a40
0x466DAD: xor      eax, eax
0x466DAF: pop      edx
0x466DB0: pop      ecx
0x466DB1: pop      ecx
0x466DB2: mov      dword ptr fs:[eax], edx
0x466DB5: push     0x466dd7
0x466DBA: lea      eax, [ebp - 0x1c]
0x466DBD: mov      edx, 2
0x466DC2: call     0x403a7c
0x466DC7: lea      eax, [ebp - 8]
0x466DCA: call     0x403a58
0x466DCF: ret      
0x466DD0: jmp      0x403518
0x466DD5: jmp      0x466dba
0x466DD7: mov      esp, ebp
0x466DD9: pop      ebp
0x466DDA: ret      
0x466DDB: nop      
0x466DDC: push     ebp
0x466DDD: mov      ebp, esp
0x466DDF: add      esp, -0x1c
0x466DE2: xor      ecx, ecx
0x466DE4: mov      dword ptr [ebp - 0x18], ecx
0x466DE7: mov      dword ptr [ebp - 0x1c], ecx
0x466DEA: mov      dword ptr [ebp - 8], edx
0x466DED: mov      dword ptr [ebp - 4], eax
0x466DF0: mov      eax, dword ptr [ebp - 8]
0x466DF3: call     0x403e88
0x466DF8: xor      eax, eax
0x466DFA: push     ebp
0x466DFB: push     0x466e6c
0x466E00: push     dword ptr fs:[eax]
0x466E03: mov      dword ptr fs:[eax], esp
0x466E06: push     0
0x466E08: push     0
0x466E0A: lea      eax, [ebp - 0x14]
0x466E0D: push     eax
0x466E0E: xor      ecx, ecx
0x466E10: xor      edx, edx
0x466E12: mov      ax, 0x411
0x466E16: call     0x458d3c
0x466E1B: lea      edx, [ebp - 0x18]
0x466E1E: lea      eax, [ebp - 0x14]
0x466E21: call     0x45b41c
0x466E26: lea      eax, [ebp - 0x18]
0x466E29: push     eax
0x466E2A: lea      edx, [ebp - 0x1c]
0x466E2D: mov      eax, dword ptr [ebp - 8]
0x466E30: call     0x45b4a8
0x466E35: mov      edx, dword ptr [ebp - 0x1c]
0x466E38: pop      eax
0x466E39: call     0x403cdc
0x466E3E: mov      edx, dword ptr [ebp - 0x18]
0x466E41: mov      eax, dword ptr [ebp - 4]
0x466E44: call     0x464a40
0x466E49: xor      eax, eax
0x466E4B: pop      edx
0x466E4C: pop      ecx
0x466E4D: pop      ecx
0x466E4E: mov      dword ptr fs:[eax], edx
0x466E51: push     0x466e73
0x466E56: lea      eax, [ebp - 0x1c]
0x466E59: mov      edx, 2
0x466E5E: call     0x403a7c
0x466E63: lea      eax, [ebp - 8]
0x466E66: call     0x403a58
0x466E6B: ret      
0x466E6C: jmp      0x403518
0x466E71: jmp      0x466e56
0x466E73: mov      esp, ebp
0x466E75: pop      ebp
0x466E76: ret      
0x466E77: nop      
0x466E78: push     ebp
0x466E79: mov      ebp, esp
0x466E7B: add      esp, -0x30
0x466E7E: push     ebx
0x466E7F: push     esi
0x466E80: push     edi
0x466E81: xor      ebx, ebx
0x466E83: mov      dword ptr [ebp - 0x2c], ebx
0x466E86: mov      dword ptr [ebp - 0x30], ebx
0x466E89: mov      esi, ecx
0x466E8B: lea      edi, [ebp - 0x1c]
0x466E8E: mov      ecx, 5
0x466E93: rep movsd dword ptr es:[edi], dword ptr [esi]
0x466E95: mov      dword ptr [ebp - 8], edx
0x466E98: mov      dword ptr [ebp - 4], eax
0x466E9B: xor      eax, eax
0x466E9D: push     ebp
0x466E9E: push     0x466f0d
0x466EA3: push     dword ptr fs:[eax]
0x466EA6: mov      dword ptr fs:[eax], esp
0x466EA9: push     0
0x466EAB: push     0
0x466EAD: lea      eax, [ebp - 0x28]
0x466EB0: push     eax
0x466EB1: xor      ecx, ecx
0x466EB3: mov      edx, dword ptr [ebp - 8]
0x466EB6: mov      ax, 0x413
0x466EBA: call     0x458d3c
0x466EBF: lea      edx, [ebp - 0x2c]
0x466EC2: lea      eax, [ebp - 0x28]
0x466EC5: call     0x45b41c
0x466ECA: lea      eax, [ebp - 0x2c]
0x466ECD: push     eax
0x466ECE: lea      ecx, [ebp - 0x30]
0x466ED1: lea      eax, [ebp - 0x1c]
0x466ED4: mov      edx, 0x14
0x466ED9: call     0x45b554
0x466EDE: mov      edx, dword ptr [ebp - 0x30]
0x466EE1: pop      eax
0x466EE2: call     0x403cdc
0x466EE7: mov      edx, dword ptr [ebp - 0x2c]
0x466EEA: mov      eax, dword ptr [ebp - 4]
0x466EED: call     0x464a40
0x466EF2: xor      eax, eax
0x466EF4: pop      edx
0x466EF5: pop      ecx
0x466EF6: pop      ecx
0x466EF7: mov      dword ptr fs:[eax], edx
0x466EFA: push     0x466f14
0x466EFF: lea      eax, [ebp - 0x30]
0x466F02: mov      edx, 2
0x466F07: call     0x403a7c
0x466F0C: ret      
0x466F0D: jmp      0x403518
0x466F12: jmp      0x466eff
0x466F14: pop      edi
0x466F15: pop      esi
0x466F16: pop      ebx
0x466F17: mov      esp, ebp
0x466F19: pop      ebp
0x466F1A: ret      
0x466F1B: nop      
0x466F1C: push     ebp
0x466F1D: mov      ebp, esp
0x466F1F: add      esp, -8
0x466F22: mov      dword ptr [ebp - 4], eax
0x466F25: mov      byte ptr [ebp - 5], 1
0x466F29: mov      eax, dword ptr [ebp - 4]
0x466F2C: cmp      byte ptr [eax + 0x420], 0
0x466F33: je       0x466f58
0x466F35: call     0x406154
0x466F3A: mov      edx, dword ptr [ebp - 4]
0x466F3D: sub      eax, dword ptr [edx + 0x3d8]
0x466F43: cmp      eax, 0x2710
0x466F48: jbe      0x466f54
0x466F4A: mov      eax, dword ptr [ebp - 4]
0x466F4D: mov      byte ptr [eax + 0x420], 0
0x466F54: mov      byte ptr [ebp - 5], 0
0x466F58: mov      al, byte ptr [ebp - 5]
0x466F5B: pop      ecx
0x466F5C: pop      ecx
0x466F5D: pop      ebp
0x466F5E: ret      
0x466F5F: nop      
0x466F60: push     ebp
0x466F61: mov      ebp, esp
0x466F63: add      esp, -8
0x466F66: mov      dword ptr [ebp - 4], eax
0x466F69: mov      eax, dword ptr [0x4f7ef8]
0x466F6E: call     0x4974ec
0x466F73: test     al, al
0x466F75: je       0x466fb0
0x466F77: mov      eax, dword ptr [0x4f7ef8]
0x466F7C: test     byte ptr [eax + 0x1f], 4
0x466F80: jne      0x466fb0
0x466F82: call     0x406154
0x466F87: sub      eax, dword ptr [0x4f9460]
0x466F8D: xor      edx, edx
0x466F8F: push     edx
0x466F90: push     eax
0x466F91: mov      eax, dword ptr [0x4f9464]
0x466F96: cdq      
0x466F97: cmp      edx, dword ptr [esp + 4]
0x466F9B: jne      0x466fa6
0x466F9D: cmp      eax, dword ptr [esp]
0x466FA0: pop      edx
0x466FA1: pop      eax
0x466FA2: jae      0x466fb0
0x466FA4: jmp      0x466faa
0x466FA6: pop      edx
0x466FA7: pop      eax
0x466FA8: jge      0x466fb0
0x466FAA: mov      byte ptr [ebp - 5], 1
0x466FAE: jmp      0x466fb4
0x466FB0: mov      byte ptr [ebp - 5], 0
0x466FB4: mov      al, byte ptr [ebp - 5]
0x466FB7: pop      ecx
0x466FB8: pop      ecx
0x466FB9: pop      ebp
0x466FBA: ret      
0x466FBB: nop      
0x466FBC: push     ebp
0x466FBD: mov      ebp, esp
0x466FBF: add      esp, -0x10
0x466FC2: mov      dword ptr [ebp - 4], eax
0x466FC5: mov      eax, dword ptr [0x4f7ef8]
0x466FCA: movzx    eax, byte ptr [eax + 0x30]
0x466FCE: imul     edx, eax, 0xe
0x466FD1: mov      eax, 0x172
0x466FD6: call     0x452b90
0x466FDB: mov      dword ptr [ebp - 0x10], eax
0x466FDE: mov      eax, dword ptr [0x4f7ef8]
0x466FE3: movsx    eax, byte ptr [eax + 0x5c]
0x466FE7: imul     edx, eax, 0x3c
0x466FEA: add      edx, dword ptr [ebp - 0x10]
0x466FED: mov      eax, 0x320
0x466FF2: call     0x452b90
0x466FF7: mov      dword ptr [ebp - 0x10], eax
0x466FFA: cmp      byte ptr [0x4f9471], 0
0x467001: je       0x467015
0x467003: mov      eax, 0x578
0x467008: sub      eax, dword ptr [ebp - 0x10]
0x46700B: add      eax, 0x5dc
0x467010: mov      dword ptr [ebp - 0xc], eax
0x467013: jmp      0x467020
0x467015: mov      eax, 0x578
0x46701A: sub      eax, dword ptr [ebp - 0x10]
0x46701D: mov      dword ptr [ebp - 0xc], eax
0x467020: cmp      dword ptr [ebp - 0xc], 0
0x467024: jge      0x46702b
0x467026: xor      eax, eax
0x467028: mov      dword ptr [ebp - 0xc], eax
0x46702B: call     0x406154
0x467030: mov      edx, dword ptr [ebp - 4]
0x467033: sub      eax, dword ptr [edx + 0x3dc]
0x467039: cmp      eax, dword ptr [ebp - 0xc]
0x46703C: jbe      0x467052
0x46703E: call     0x406154
0x467043: mov      edx, dword ptr [ebp - 4]
0x467046: mov      dword ptr [edx + 0x3dc], eax
0x46704C: mov      byte ptr [ebp - 5], 1
0x467050: jmp      0x467056
0x467052: mov      byte ptr [ebp - 5], 0
0x467056: mov      al, byte ptr [ebp - 5]
0x467059: mov      esp, ebp
0x46705B: pop      ebp
0x46705C: ret      
0x46705D: lea      eax, [eax]
0x467060: push     ebp
0x467061: mov      ebp, esp
0x467063: push     ecx
0x467064: mov      dword ptr [ebp - 4], eax
0x467067: mov      dword ptr [0x4f949c], 0xffffffff
0x467071: mov      dword ptr [0x4f94a0], 0xffffffff
0x46707B: mov      eax, dword ptr [ebp - 4]
0x46707E: mov      byte ptr [eax + 0x3e0], 1
0x467085: mov      eax, dword ptr [0x4f7ef8]
0x46708A: call     0x499f74
0x46708F: pop      ecx
0x467090: pop      ebp
0x467091: ret      
0x467092: mov      eax, eax
0x467094: push     ebp
0x467095: mov      ebp, esp
0x467097: add      esp, -0x10
0x46709A: mov      dword ptr [ebp - 0xc], ecx
0x46709D: mov      dword ptr [ebp - 8], edx
0x4670A0: mov      dword ptr [ebp - 4], eax
0x4670A3: mov      eax, dword ptr [ebp - 4]
0x4670A6: cmp      byte ptr [eax + 0x3e0], 0
0x4670AD: je       0x4670e0
0x4670AF: mov      eax, dword ptr [ebp - 8]
0x4670B2: mov      edx, dword ptr [ebp - 4]
0x4670B5: cmp      eax, dword ptr [edx + 0x3e4]
0x4670BB: jne      0x4670e0
0x4670BD: mov      eax, dword ptr [ebp - 0xc]
0x4670C0: mov      edx, dword ptr [ebp - 4]
0x4670C3: cmp      eax, dword ptr [edx + 0x3e8]
0x4670C9: jne      0x4670e0
0x4670CB: call     0x406154
0x4670D0: mov      edx, dword ptr [ebp - 4]
0x4670D3: sub      eax, dword ptr [edx + 0x3ec]
0x4670D9: cmp      eax, 0x3e8
0x4670DE: jb       0x4670f2
0x4670E0: cmp      byte ptr [0x4f9478], 0
0x4670E7: jne      0x4670f2
0x4670E9: cmp      byte ptr [0x4f9234], 0
0x4670F0: je       0x4670f8
0x4670F2: mov      byte ptr [ebp - 0xd], 0
0x4670F6: jmp      0x467106
0x4670F8: mov      eax, dword ptr [ebp - 4]
0x4670FB: mov      byte ptr [eax + 0x3e0], 0
0x467102: mov      byte ptr [ebp - 0xd], 1
0x467106: mov      al, byte ptr [ebp - 0xd]
0x467109: mov      esp, ebp
0x46710B: pop      ebp
0x46710C: ret      
0x46710D: lea      eax, [eax]
0x467110: push     ebp
0x467111: mov      ebp, esp
0x467113: add      esp, -0x18
0x467116: push     ebx
0x467117: xor      ecx, ecx
0x467119: mov      dword ptr [ebp - 0x18], ecx
0x46711C: mov      dword ptr [ebp - 8], edx
0x46711F: mov      dword ptr [ebp - 4], eax
0x467122: mov      eax, dword ptr [ebp - 8]
0x467125: call     0x403e88
0x46712A: xor      eax, eax
0x46712C: push     ebp
0x46712D: push     0x46719f
0x467132: push     dword ptr fs:[eax]
0x467135: mov      dword ptr fs:[eax], esp
0x467138: mov      byte ptr [ebp - 9], 0
0x46713C: mov      eax, dword ptr [0x4f94fc]
0x467141: mov      edx, dword ptr [eax]
0x467143: call     dword ptr [edx + 0x14]
0x467146: dec      eax
0x467147: test     eax, eax
0x467149: jl       0x467181
0x46714B: inc      eax
0x46714C: mov      dword ptr [ebp - 0x14], eax
0x46714F: mov      dword ptr [ebp - 0x10], 0
0x467156: lea      ecx, [ebp - 0x18]
0x467159: mov      edx, dword ptr [ebp - 0x10]
0x46715C: mov      eax, dword ptr [0x4f94fc]
0x467161: mov      ebx, dword ptr [eax]
0x467163: call     dword ptr [ebx + 0xc]
0x467166: mov      eax, dword ptr [ebp - 0x18]
0x467169: mov      edx, dword ptr [ebp - 8]
0x46716C: call     0x403de4
0x467171: jne      0x467179
0x467173: mov      byte ptr [ebp - 9], 1
0x467177: jmp      0x467181
0x467179: inc      dword ptr [ebp - 0x10]
0x46717C: dec      dword ptr [ebp - 0x14]
0x46717F: jne      0x467156
0x467181: xor      eax, eax
0x467183: pop      edx
0x467184: pop      ecx
0x467185: pop      ecx
0x467186: mov      dword ptr fs:[eax], edx
0x467189: push     0x4671a6
0x46718E: lea      eax, [ebp - 0x18]
0x467191: call     0x403a58
0x467196: lea      eax, [ebp - 8]
0x467199: call     0x403a58
0x46719E: ret      
0x46719F: jmp      0x403518
0x4671A4: jmp      0x46718e
0x4671A6: mov      al, byte ptr [ebp - 9]
0x4671A9: pop      ebx
0x4671AA: mov      esp, ebp
0x4671AC: pop      ebp
0x4671AD: ret      
0x4671AE: mov      eax, eax
0x4671B0: push     ebp
0x4671B1: mov      ebp, esp
0x4671B3: add      esp, -0x24
0x4671B6: xor      ecx, ecx
0x4671B8: mov      dword ptr [ebp - 0x24], ecx
0x4671BB: mov      dword ptr [ebp - 0x14], ecx
0x4671BE: mov      dword ptr [ebp - 8], ecx
0x4671C1: mov      dword ptr [ebp - 0x10], edx
0x4671C4: mov      dword ptr [ebp - 4], eax
0x4671C7: xor      eax, eax
0x4671C9: push     ebp
0x4671CA: push     0x4673d4
0x4671CF: push     dword ptr fs:[eax]
0x4671D2: mov      dword ptr fs:[eax], esp
0x4671D5: cmp      byte ptr [0x4a2d18], 0 ; *0x4A2D18->0x408D00
0x4671DC: jne      0x4673ae
0x4671E2: mov      byte ptr [0x4a2d18], 1 ; *0x4A2D18->0x408D00
0x4671E9: xor      eax, eax
0x4671EB: push     ebp
0x4671EC: push     0x4672db
0x4671F1: push     dword ptr fs:[eax]
0x4671F4: mov      dword ptr fs:[eax], esp
0x4671F7: mov      eax, dword ptr [ebp - 4]
0x4671FA: add      eax, 0x3c8
0x4671FF: mov      edx, dword ptr [ebp - 4]
0x467202: mov      edx, dword ptr [edx + 0x3c4]
0x467208: call     0x403cdc
0x46720D: mov      eax, dword ptr [ebp - 4]
0x467210: add      eax, 0x3c4
0x467215: call     0x403a58
0x46721A: mov      eax, dword ptr [ebp - 4]
0x46721D: cmp      dword ptr [eax + 0x3c8], 0
0x467224: je       0x4672c6
0x46722A: xor      eax, eax
0x46722C: mov      dword ptr [ebp - 0xc], eax
0x46722F: jmp      0x4672af
0x467231: cmp      byte ptr [0x4f9479], 0
0x467238: jne      0x4672c6
0x46723E: mov      eax, dword ptr [ebp - 4]
0x467241: mov      edx, dword ptr [eax + 0x3c8]
0x467247: mov      eax, 0x4673e8 ; "!"
0x46724C: call     0x403f60
0x467251: test     eax, eax
0x467253: jle      0x4672c6
0x467255: lea      eax, [ebp - 8]
0x467258: push     eax
0x467259: lea      eax, [ebp - 0x24]
0x46725C: push     eax
0x46725D: mov      eax, dword ptr [ebp - 4]
0x467260: mov      eax, dword ptr [eax + 0x3c8]
0x467266: mov      ecx, 0x4673e8 ; "!"
0x46726B: mov      edx, 0x4673f4 ; "#"
0x467270: call     0x4529b8
0x467275: mov      edx, dword ptr [ebp - 0x24]
0x467278: mov      eax, dword ptr [ebp - 4]
0x46727B: add      eax, 0x3c8
0x467280: call     0x403aac
0x467285: cmp      dword ptr [ebp - 8], 0
0x467289: je       0x467298
0x46728B: mov      edx, dword ptr [ebp - 8]
0x46728E: mov      eax, dword ptr [ebp - 4]
0x467291: call     0x467920
0x467296: jmp      0x4672af
0x467298: mov      eax, dword ptr [ebp - 4]
0x46729B: mov      edx, dword ptr [eax + 0x3c8]
0x4672A1: mov      eax, 0x4673e8 ; "!"
0x4672A6: call     0x403f60
0x4672AB: test     eax, eax
0x4672AD: je       0x4672c6
0x4672AF: mov      eax, dword ptr [ebp - 4]
0x4672B2: mov      eax, dword ptr [eax + 0x3c8]
0x4672B8: call     0x403cd4
0x4672BD: cmp      eax, 2
0x4672C0: jge      0x467231
0x4672C6: xor      eax, eax
0x4672C8: pop      edx
0x4672C9: pop      ecx
0x4672CA: pop      ecx
0x4672CB: mov      dword ptr fs:[eax], edx
0x4672CE: push     0x4672e2
0x4672D3: mov      byte ptr [0x4a2d18], 0 ; *0x4A2D18->0x408D00
0x4672DA: ret      
0x4672DB: jmp      0x403518
0x4672E0: jmp      0x4672d3
0x4672E2: mov      eax, dword ptr [ebp - 4]
0x4672E5: cmp      dword ptr [eax + 0x3cc], 0x1e
0x4672EC: jle      0x4672f8
0x4672EE: mov      eax, dword ptr [0x4f7d88]
0x4672F3: call     0x4405bc
0x4672F8: cmp      byte ptr [0x4f9370], 0
0x4672FF: je       0x46737b
0x467301: call     0x406154
0x467306: sub      eax, dword ptr [0x4f9374]
0x46730C: cmp      eax, 0x1f4
0x467311: jbe      0x46737b
0x467313: mov      byte ptr [0x4f9370], 0
0x46731A: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46731F: mov      eax, dword ptr [eax]
0x467321: mov      al, byte ptr [eax + 0x760]
0x467327: sub      al, 1
0x467329: jb       0x46732f
0x46732B: je       0x467356
0x46732D: jmp      0x46737b
0x46732F: lea      eax, [ebp - 0x24]
0x467332: mov      edx, 0x4f92d4
0x467337: call     0x403c78
0x46733C: mov      eax, dword ptr [ebp - 0x24]
0x46733F: push     eax
0x467340: mov      ecx, dword ptr [0x4f9300]
0x467346: mov      edx, dword ptr [0x4f94dc]
0x46734C: mov      eax, dword ptr [ebp - 4]
0x46734F: call     0x465e44
0x467354: jmp      0x46737b
0x467356: lea      eax, [ebp - 0x24]
0x467359: mov      edx, 0x4f92d4
0x46735E: call     0x403c78
0x467363: mov      eax, dword ptr [ebp - 0x24]
0x467366: push     eax
0x467367: mov      ecx, dword ptr [0x4f9300]
0x46736D: mov      edx, dword ptr [0x4f94dc]
0x467373: mov      eax, dword ptr [ebp - 4]
0x467376: call     0x465ef0
0x46737B: cmp      dword ptr [0x4f927c], 0
0x467382: jle      0x46739a
0x467384: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x467389: mov      eax, dword ptr [eax]
0x46738B: mov      eax, dword ptr [eax + 0x56c]
0x467391: mov      byte ptr [eax + 0x24c], 1
0x467398: jmp      0x4673ae
0x46739A: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46739F: mov      eax, dword ptr [eax]
0x4673A1: mov      eax, dword ptr [eax + 0x56c]
0x4673A7: mov      byte ptr [eax + 0x24c], 0
0x4673AE: xor      eax, eax
0x4673B0: pop      edx
0x4673B1: pop      ecx
0x4673B2: pop      ecx
0x4673B3: mov      dword ptr fs:[eax], edx
0x4673B6: push     0x4673db
0x4673BB: lea      eax, [ebp - 0x24]
0x4673BE: call     0x403a58
0x4673C3: lea      eax, [ebp - 0x14]
0x4673C6: call     0x403a58
0x4673CB: lea      eax, [ebp - 8]
0x4673CE: call     0x403a58
0x4673D3: ret      
0x4673D4: jmp      0x403518
0x4673D9: jmp      0x4673bb
0x4673DB: mov      esp, ebp
0x4673DD: pop      ebp
0x4673DE: ret      
0x4673DF: add      bh, bh
0x4673E3: inc      dword ptr [ecx]
0x4673E5: add      byte ptr [eax], al
0x4673E7: add      byte ptr [ecx], ah
0x4673E9: add      byte ptr [eax], al
0x4673EB: add      bh, bh
0x4673EF: inc      dword ptr [ecx]
0x4673F1: add      byte ptr [eax], al
0x4673F3: add      byte ptr [ebx], ah
0x4673F5: add      byte ptr [eax], al
0x4673F7: add      byte ptr [ebp - 0x75], dl
0x4673FA: in       al, dx
0x4673FB: add      esp, -0x2c
0x4673FE: xor      ecx, ecx
0x467400: mov      dword ptr [ebp - 0x1c], ecx
0x467403: mov      dword ptr [ebp - 0x20], ecx
0x467406: mov      dword ptr [ebp - 0x24], ecx
0x467409: mov      dword ptr [ebp - 0x28], ecx
0x46740C: mov      dword ptr [ebp - 0x2c], ecx
0x46740F: mov      dword ptr [ebp - 0x18], edx
0x467412: mov      dword ptr [ebp - 4], eax
0x467415: xor      eax, eax
0x467417: push     ebp
0x467418: push     0x4675b1
0x46741D: push     dword ptr fs:[eax]
0x467420: mov      dword ptr fs:[eax], esp
0x467423: call     0x4090bc
0x467428: add      esp, -8
0x46742B: fstp     qword ptr [esp]
0x46742E: wait     
0x46742F: lea      eax, [ebp - 0x14]
0x467432: push     eax
0x467433: lea      ecx, [ebp - 0x12]
0x467436: lea      edx, [ebp - 0x10]
0x467439: lea      eax, [ebp - 0xe]
0x46743C: call     0x408d70
0x467441: movzx    eax, word ptr [ebp - 0xe]
0x467445: imul     eax, eax, 0x3e8
0x46744B: imul     eax, eax, 0x3c
0x46744E: imul     eax, eax, 0x3c
0x467451: movzx    edx, word ptr [ebp - 0x10]
0x467455: imul     edx, edx, 0x3e8
0x46745B: imul     edx, edx, 0x3c
0x46745E: add      eax, edx
0x467460: movzx    edx, word ptr [ebp - 0x12]
0x467464: imul     edx, edx, 0x3e8
0x46746A: add      eax, edx
0x46746C: movzx    edx, word ptr [ebp - 0x14]
0x467470: add      eax, edx
0x467472: mov      dword ptr [ebp - 0xc], eax
0x467475: call     0x406154
0x46747A: mov      dword ptr [ebp - 8], eax
0x46747D: cmp      dword ptr [0x4f9254], 0
0x467484: jbe      0x467586
0x46748A: mov      eax, dword ptr [ebp - 8]
0x46748D: sub      eax, dword ptr [0x4f9254]
0x467493: mov      edx, dword ptr [ebp - 0xc]
0x467496: sub      edx, dword ptr [0x4f9258]
0x46749C: sub      eax, edx
0x46749E: cdq      
0x46749F: xor      eax, edx
0x4674A1: sub      eax, edx
0x4674A3: cmp      eax, 0x46
0x4674A6: jle      0x4674b0
0x4674A8: inc      dword ptr [0x4f925c]
0x4674AE: jmp      0x4674b7
0x4674B0: xor      eax, eax
0x4674B2: mov      dword ptr [0x4f925c], eax
0x4674B7: cmp      dword ptr [0x4f925c], 4
0x4674BE: jle      0x4674e2
0x4674C0: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x4674C5: mov      eax, dword ptr [eax]
0x4674C7: mov      cx, word ptr [0x4675bc]
0x4674CE: mov      edx, 0x4675c8 ; "Terminate the program. CODE=10001\请联系游戏管理员."
0x4674D3: call     0x486df0
0x4674D8: mov      eax, dword ptr [0x4f7d88]
0x4674DD: call     0x4405bc
0x4674E2: cmp      byte ptr [0x4f947b], 0
0x4674E9: je       0x467586
0x4674EF: push     0x467604 ; "->"
0x4674F4: mov      eax, dword ptr [ebp - 8]
0x4674F7: sub      eax, dword ptr [0x4f9254]
0x4674FD: xor      edx, edx
0x4674FF: push     edx
0x467500: push     eax
0x467501: lea      eax, [ebp - 0x20]
0x467504: call     0x407d94
0x467509: push     dword ptr [ebp - 0x20]
0x46750C: push     0x467610 ; " - "
0x467511: mov      eax, dword ptr [ebp - 0xc]
0x467514: sub      eax, dword ptr [0x4f9258]
0x46751A: xor      edx, edx
0x46751C: push     edx
0x46751D: push     eax
0x46751E: lea      eax, [ebp - 0x24]
0x467521: call     0x407d94
0x467526: push     dword ptr [ebp - 0x24]
0x467529: push     0x46761c ; " = "
0x46752E: mov      eax, dword ptr [ebp - 8]
0x467531: sub      eax, dword ptr [0x4f9254]
0x467537: mov      edx, dword ptr [ebp - 0xc]
0x46753A: sub      edx, dword ptr [0x4f9258]
0x467540: sub      eax, edx
0x467542: cdq      
0x467543: xor      eax, edx
0x467545: sub      eax, edx
0x467547: lea      edx, [ebp - 0x28]
0x46754A: call     0x407d64
0x46754F: push     dword ptr [ebp - 0x28]
0x467552: push     0x467628 ; " ("
0x467557: lea      edx, [ebp - 0x2c]
0x46755A: mov      eax, dword ptr [0x4f925c]
0x46755F: call     0x407d64
0x467564: push     dword ptr [ebp - 0x2c]
0x467567: push     0x467634 ; ")"
0x46756C: lea      eax, [ebp - 0x1c]
0x46756F: mov      edx, 9
0x467574: call     0x403d94
0x467579: mov      edx, dword ptr [ebp - 0x1c]
0x46757C: mov      eax, dword ptr [0x4f7d8c]
0x467581: call     0x479a68
0x467586: mov      eax, dword ptr [ebp - 8]
0x467589: mov      dword ptr [0x4f9254], eax
0x46758E: mov      eax, dword ptr [ebp - 0xc]
0x467591: mov      dword ptr [0x4f9258], eax
0x467596: xor      eax, eax
0x467598: pop      edx
0x467599: pop      ecx
0x46759A: pop      ecx
0x46759B: mov      dword ptr fs:[eax], edx
0x46759E: push     0x4675b8
0x4675A3: lea      eax, [ebp - 0x2c]
0x4675A6: mov      edx, 5
0x4675AB: call     0x403a7c
0x4675B0: ret      
0x4675B1: jmp      0x403518
0x4675B6: jmp      0x4675a3
0x4675B8: mov      esp, ebp
0x4675BA: pop      ebp
0x4675BB: ret      
0x4675BC: add      al, 0
0x4675BE: add      byte ptr [eax], al
0x4675C8: [STR len=51] "Terminate the program. CODE=10001\请联系游戏管理员."
0x467604: [STR len=2] "->"
0x467610: [STR len=3] " - "
0x46761C: [STR len=3] " = "
0x467628: [STR len=2] " ("
0x467634: [STR len=1] ")"
0x467638: push     ebp
0x467639: mov      ebp, esp
0x46763B: add      esp, -0x18
0x46763E: mov      dword ptr [ebp - 0x18], edx
0x467641: mov      dword ptr [ebp - 4], eax
0x467644: cmp      dword ptr [0x4f7ef8], 0
0x46764B: je       0x467712
0x467651: mov      eax, dword ptr [0x4f7ef8]
0x467656: movsx    eax, byte ptr [eax + 0x5c]
0x46765A: cdq      
0x46765B: xor      eax, edx
0x46765D: sub      eax, edx
0x46765F: imul     eax, eax, 0x3c
0x467662: mov      edx, dword ptr [0x4f7ef8]
0x467668: movzx    edx, byte ptr [edx + 0x30]
0x46766C: imul     edx, edx, 0xe
0x46766F: add      eax, edx
0x467671: mov      edx, dword ptr [0x4f7ef8]
0x467677: movzx    edx, word ptr [edx + 0x50]
0x46767B: add      eax, edx
0x46767D: mov      edx, dword ptr [0x4f7ef8]
0x467683: movzx    edx, word ptr [edx + 0x52]
0x467687: add      eax, edx
0x467689: mov      edx, dword ptr [0x4f7ef8]
0x46768F: movzx    edx, byte ptr [edx + 0x54]
0x467693: add      eax, edx
0x467695: mov      edx, dword ptr [0x4f7ef8]
0x46769B: movzx    edx, byte ptr [edx + 0x55]
0x46769F: add      eax, edx
0x4676A1: mov      edx, dword ptr [0x4f7ef8]
0x4676A7: movzx    edx, byte ptr [edx + 0x56]
0x4676AB: add      eax, edx
0x4676AD: mov      edx, dword ptr [0x4f7ef8]
0x4676B3: movzx    edx, byte ptr [edx + 0x57]
0x4676B7: add      eax, edx
0x4676B9: add      eax, dword ptr [0x4a2d10] ; *0x4A2D10->0x000BB8
0x4676BF: mov      dword ptr [ebp - 8], eax
0x4676C2: mov      eax, dword ptr [0x4f9570]
0x4676C7: mov      dword ptr [ebp - 0xc], eax
0x4676CA: mov      eax, dword ptr [0x4f9584]
0x4676CF: mov      eax, dword ptr [eax]
0x4676D1: xor      eax, 0xffffffff
0x4676D4: shr      eax, 2
0x4676D7: mov      dword ptr [ebp - 0x10], eax
0x4676DA: mov      eax, dword ptr [0x4f958c]
0x4676DF: mov      eax, dword ptr [eax]
0x4676E1: xor      eax, 0xffffffff
0x4676E4: mov      ecx, 0x14
0x4676E9: xor      edx, edx
0x4676EB: div      ecx
0x4676ED: mov      dword ptr [ebp - 0x14], eax
0x4676F0: mov      eax, dword ptr [ebp - 8]
0x4676F3: cmp      eax, dword ptr [ebp - 0xc]
0x4676F6: jne      0x467708
0x4676F8: mov      eax, dword ptr [ebp - 8]
0x4676FB: cmp      eax, dword ptr [ebp - 0x10]
0x4676FE: jne      0x467708
0x467700: mov      eax, dword ptr [ebp - 8]
0x467703: cmp      eax, dword ptr [ebp - 0x14]
0x467706: je       0x467712
0x467708: mov      eax, dword ptr [0x4f7d88]
0x46770D: call     0x4405bc
0x467712: mov      esp, ebp
0x467714: pop      ebp
0x467715: ret      
0x467716: mov      eax, eax
0x467718: push     ebp
0x467719: mov      ebp, esp
0x46771B: mov      ecx, 4
0x467720: push     0
0x467722: push     0
0x467724: dec      ecx
0x467725: jne      0x467720
0x467727: push     ecx
0x467728: mov      dword ptr [ebp - 8], edx
0x46772B: mov      dword ptr [ebp - 4], eax
0x46772E: xor      eax, eax
0x467730: push     ebp
0x467731: push     0x467879
0x467736: push     dword ptr fs:[eax]
0x467739: mov      dword ptr fs:[eax], esp
0x46773C: cmp      dword ptr [0x4f9238], 0
0x467743: jbe      0x46784c
0x467749: call     0x406154
0x46774E: sub      eax, dword ptr [0x4f923c]
0x467754: cmp      eax, 0x36ee80
0x467759: jbe      0x46776d
0x46775B: mov      eax, dword ptr [ebp - 8]
0x46775E: mov      dword ptr [0x4f9238], eax
0x467763: call     0x406154
0x467768: mov      dword ptr [0x4f923c], eax
0x46776D: call     0x406154
0x467772: sub      eax, dword ptr [0x4f923c]
0x467778: mov      dword ptr [ebp - 0xc], eax
0x46777B: mov      eax, dword ptr [ebp - 8]
0x46777E: sub      eax, dword ptr [0x4f9238]
0x467784: mov      dword ptr [ebp - 0x10], eax
0x467787: mov      eax, dword ptr [ebp - 0x10]
0x46778A: add      eax, 0xbb8
0x46778F: cmp      eax, dword ptr [ebp - 0xc]
0x467792: jge      0x4677d4
0x467794: inc      dword ptr [0x4f9240]
0x46779A: cmp      dword ptr [0x4f9240], 5
0x4677A1: jle      0x4677e8
0x4677A3: lea      eax, [ebp - 0x14]
0x4677A6: mov      edx, 0x46788c ; "Bad"
0x4677AB: call     0x403af0
0x4677B0: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x4677B5: mov      eax, dword ptr [eax]
0x4677B7: mov      cx, word ptr [0x467890]
0x4677BE: mov      edx, 0x46789c ; "terminate the program. CODE=10000\连接情况很差或系统不稳定。\请联系游戏管理员."
0x4677C3: call     0x486df0
0x4677C8: mov      eax, dword ptr [0x4f7d88]
0x4677CD: call     0x4405bc
0x4677D2: jmp      0x4677e8
0x4677D4: lea      eax, [ebp - 0x14]
0x4677D7: mov      edx, 0x4678f4 ; "Good"
0x4677DC: call     0x403af0
0x4677E1: xor      eax, eax
0x4677E3: mov      dword ptr [0x4f9240], eax
0x4677E8: cmp      byte ptr [0x4f947b], 0
0x4677EF: je       0x46785e
0x4677F1: lea      edx, [ebp - 0x1c]
0x4677F4: mov      eax, dword ptr [ebp - 0x10]
0x4677F7: call     0x407d64
0x4677FC: push     dword ptr [ebp - 0x1c]
0x4677FF: push     0x467904 ; " - "
0x467804: lea      edx, [ebp - 0x20]
0x467807: mov      eax, dword ptr [ebp - 0xc]
0x46780A: call     0x407d64
0x46780F: push     dword ptr [ebp - 0x20]
0x467812: push     0x467910 ; " = "
0x467817: lea      edx, [ebp - 0x24]
0x46781A: mov      eax, dword ptr [ebp - 0x10]
0x46781D: sub      eax, dword ptr [ebp - 0xc]
0x467820: call     0x407d64
0x467825: push     dword ptr [ebp - 0x24]
0x467828: push     0x46791c ; " "
0x46782D: push     dword ptr [ebp - 0x14]
0x467830: lea      eax, [ebp - 0x18]
0x467833: mov      edx, 7
0x467838: call     0x403d94
0x46783D: mov      edx, dword ptr [ebp - 0x18]
0x467840: mov      eax, dword ptr [0x4f7d8c]
0x467845: call     0x479a68
0x46784A: jmp      0x46785e
0x46784C: mov      eax, dword ptr [ebp - 8]
0x46784F: mov      dword ptr [0x4f9238], eax
0x467854: call     0x406154
0x467859: mov      dword ptr [0x4f923c], eax
0x46785E: xor      eax, eax
0x467860: pop      edx
0x467861: pop      ecx
0x467862: pop      ecx
0x467863: mov      dword ptr fs:[eax], edx
0x467866: push     0x467880
0x46786B: lea      eax, [ebp - 0x24]
0x46786E: mov      edx, 5
0x467873: call     0x403a7c
0x467878: ret      
0x467879: jmp      0x403518
0x46787E: jmp      0x46786b
0x467880: mov      esp, ebp
0x467882: pop      ebp
0x467883: ret      
0x46788C: [STR len=3] "Bad"
0x467890: add      al, 0
0x467892: add      byte ptr [eax], al
0x46789C: [STR len=78] "terminate the program. CODE=10000\连接情况很差或系统不稳定。\请联系游戏管理员."
0x4678F4: [STR len=4] "Good"
0x467904: [STR len=3] " - "
0x467910: [STR len=3] " = "
0x46791C: [STR len=1] " "
0x467920: push     ebp
0x467921: mov      ebp, esp
0x467923: add      esp, 0xffffff6c
0x467929: push     ebx
0x46792A: push     esi
0x46792B: push     edi
0x46792C: xor      ecx, ecx
0x46792E: mov      dword ptr [ebp - 0x8c], ecx
0x467934: mov      dword ptr [ebp - 0x94], ecx
0x46793A: mov      dword ptr [ebp - 0xc], ecx
0x46793D: mov      dword ptr [ebp - 0x10], ecx
0x467940: mov      dword ptr [ebp - 0x14], ecx
0x467943: mov      dword ptr [ebp - 0x18], ecx
0x467946: mov      dword ptr [ebp - 0x1c], ecx
0x467949: mov      dword ptr [ebp - 0x48], ecx
0x46794C: mov      dword ptr [ebp - 0x20], ecx
0x46794F: mov      dword ptr [ebp - 8], edx
0x467952: mov      dword ptr [ebp - 4], eax
0x467955: mov      eax, dword ptr [ebp - 8]
0x467958: call     0x403e88
0x46795D: xor      eax, eax
0x46795F: push     ebp
0x467960: push     0x46af42
0x467965: push     dword ptr fs:[eax]
0x467968: mov      dword ptr fs:[eax], esp
0x46796B: mov      eax, dword ptr [ebp - 8]
0x46796E: cmp      byte ptr [eax], 0x2b
0x467971: jne      0x467acf
0x467977: lea      eax, [ebp - 0x1c]
0x46797A: push     eax
0x46797B: mov      eax, dword ptr [ebp - 8]
0x46797E: call     0x403cd4
0x467983: mov      ecx, eax
0x467985: dec      ecx
0x467986: mov      edx, 2
0x46798B: mov      eax, dword ptr [ebp - 8]
0x46798E: call     0x403ed8
0x467993: push     0
0x467995: lea      eax, [ebp - 0x8c]
0x46799B: push     eax
0x46799C: mov      byte ptr [ebp - 0x90], 0x2f
0x4679A3: lea      ecx, [ebp - 0x90]
0x4679A9: lea      edx, [ebp - 0x18]
0x4679AC: mov      eax, dword ptr [ebp - 0x1c]
0x4679AF: call     0x452530
0x4679B4: mov      edx, dword ptr [ebp - 0x8c]
0x4679BA: lea      eax, [ebp - 0x1c]
0x4679BD: call     0x403af0
0x4679C2: mov      eax, dword ptr [ebp - 0x18]
0x4679C5: mov      edx, 0x46af58 ; "PWR"
0x4679CA: call     0x403de4
0x4679CF: jne      0x4679d8
0x4679D1: mov      byte ptr [0x4f955c], 1
0x4679D8: mov      eax, dword ptr [ebp - 0x18]
0x4679DB: mov      edx, 0x46af64 ; "LNG"
0x4679E0: call     0x403de4
0x4679E5: jne      0x4679ee
0x4679E7: mov      byte ptr [0x4f955d], 1
0x4679EE: mov      eax, dword ptr [ebp - 0x18]
0x4679F1: mov      edx, 0x46af70 ; "ULNG"
0x4679F6: call     0x403de4
0x4679FB: jne      0x467a04
0x4679FD: mov      byte ptr [0x4f955d], 0
0x467A04: mov      eax, dword ptr [ebp - 0x18]
0x467A07: mov      edx, 0x46af80 ; "WID"
0x467A0C: call     0x403de4
0x467A11: jne      0x467a1a
0x467A13: mov      byte ptr [0x4f955e], 1
0x467A1A: mov      eax, dword ptr [ebp - 0x18]
0x467A1D: mov      edx, 0x46af8c ; "UWID"
0x467A22: call     0x403de4
0x467A27: jne      0x467a30
0x467A29: mov      byte ptr [0x4f955e], 0
0x467A30: mov      eax, dword ptr [ebp - 0x18]
0x467A33: mov      edx, 0x46af9c ; "FIR"
0x467A38: call     0x403de4
0x467A3D: jne      0x467a50
0x467A3F: mov      byte ptr [0x4f955f], 1
0x467A46: call     0x406154
0x467A4B: mov      dword ptr [0x4f9450], eax
0x467A50: mov      eax, dword ptr [ebp - 0x18]
0x467A53: mov      edx, 0x46afa8 ; "UFIR"
0x467A58: call     0x403de4
0x467A5D: jne      0x467a66
0x467A5F: mov      byte ptr [0x4f955f], 0
0x467A66: mov      eax, dword ptr [ebp - 0x18]
0x467A69: mov      edx, 0x46afb8 ; "GOOD"
0x467A6E: call     0x403de4
0x467A73: jne      0x467a85
0x467A75: mov      eax, dword ptr [ebp - 4]
0x467A78: mov      byte ptr [eax + 0x420], 0
0x467A7F: inc      dword ptr [0x4f952c]
0x467A85: mov      eax, dword ptr [ebp - 0x18]
0x467A88: mov      edx, 0x46afc8 ; "FAIL"
0x467A8D: call     0x403de4
0x467A92: jne      0x467aac
0x467A94: mov      eax, dword ptr [ebp - 4]
0x467A97: call     0x467060
0x467A9C: mov      eax, dword ptr [ebp - 4]
0x467A9F: mov      byte ptr [eax + 0x420], 0
0x467AA6: inc      dword ptr [0x4f952c]
0x467AAC: cmp      dword ptr [ebp - 0x1c], 0
0x467AB0: je       0x46af09
0x467AB6: xor      edx, edx
0x467AB8: mov      eax, dword ptr [ebp - 0x1c]
0x467ABB: call     0x4520e8
0x467AC0: mov      edx, eax
0x467AC2: mov      eax, dword ptr [ebp - 4]
0x467AC5: call     0x467718
0x467ACA: jmp      0x46af09
0x467ACF: mov      eax, dword ptr [ebp - 8]
0x467AD2: call     0x403cd4
0x467AD7: cmp      eax, 0x10
0x467ADA: jge      0x467b28
0x467ADC: mov      eax, dword ptr [ebp - 8]
0x467ADF: cmp      byte ptr [eax], 0x3d
0x467AE2: jne      0x46af09
0x467AE8: lea      eax, [ebp - 0x1c]
0x467AEB: push     eax
0x467AEC: mov      eax, dword ptr [ebp - 8]
0x467AEF: call     0x403cd4
0x467AF4: mov      ecx, eax
0x467AF6: dec      ecx
0x467AF7: mov      edx, 2
0x467AFC: mov      eax, dword ptr [ebp - 8]
0x467AFF: call     0x403ed8
0x467B04: mov      eax, dword ptr [ebp - 0x1c]
0x467B07: mov      edx, 0x46afd8 ; "DIG"
0x467B0C: call     0x403de4
0x467B11: jne      0x46af09
0x467B17: mov      eax, dword ptr [0x4f7ef8]
0x467B1C: mov      byte ptr [eax + 0xdc], 1
0x467B23: jmp      0x46af09
0x467B28: lea      eax, [ebp - 0xc]
0x467B2B: push     eax
0x467B2C: mov      ecx, 0x10
0x467B31: mov      edx, 1
0x467B36: mov      eax, dword ptr [ebp - 8]
0x467B39: call     0x403ed8
0x467B3E: lea      eax, [ebp - 0x10]
0x467B41: push     eax
0x467B42: mov      eax, dword ptr [ebp - 8]
0x467B45: call     0x403cd4
0x467B4A: mov      ecx, eax
0x467B4C: sub      ecx, 0x10
0x467B4F: mov      edx, 0x11
0x467B54: mov      eax, dword ptr [ebp - 8]
0x467B57: call     0x403ed8
0x467B5C: lea      edx, [ebp - 0x54]
0x467B5F: mov      eax, dword ptr [ebp - 0xc]
0x467B62: call     0x45b22c
0x467B67: cmp      word ptr [ebp - 0x50], 0x2e
0x467B6C: jne      0x467b80
0x467B6E: movzx    eax, word ptr [ebp - 0x4e]
0x467B72: mov      dword ptr [0x4f9590], eax
0x467B77: movzx    eax, word ptr [ebp - 0x4c]
0x467B7B: mov      dword ptr [0x4f9594], eax
0x467B80: cmp      dword ptr [0x4f7ef8], 0
0x467B87: jne      0x4682a8
0x467B8D: movzx    eax, word ptr [ebp - 0x50]
0x467B91: cmp      eax, 0x20d
0x467B96: jg       0x467c21
0x467B9C: je       0x468236
0x467BA2: cmp      eax, 0x1fb
0x467BA7: jg       0x467bf8
0x467BA9: je       0x4681ad
0x467BAF: cmp      eax, 0x1f8
0x467BB4: jg       0x467be1
0x467BB6: je       0x467c94
0x467BBC: add      eax, -0x32
0x467BBF: sub      eax, 2
0x467BC2: jb       0x4682a8
0x467BC8: sub      eax, 0x1c1
0x467BCD: je       0x468277
0x467BD3: sub      eax, 2
0x467BD6: je       0x467d4a
0x467BDC: jmp      0x46af09
0x467BE1: sub      eax, 0x1f9
0x467BE6: je       0x467cb1
0x467BEC: dec      eax
0x467BED: je       0x468190
0x467BF3: jmp      0x46af09
0x467BF8: add      eax, 0xfffffdf8
0x467BFD: cmp      eax, 4
0x467C00: ja       0x46af09
0x467C06: jmp      dword ptr [eax*4 + 0x467c0d]
0x467C0D: mov      ch, 0x80
0x467C0F: inc      esi
0x467C10: add      bh, bh
0x467C12: add      byte ptr [esi], 0xc
0x467C16: add      dword ptr [esi], 0x46820f
0x467C1D: sbb      al, 0x82
0x467C1F: inc      esi
0x467C20: add      byte ptr [0x213], bh
0x467C26: jg       0x467c57
0x467C28: je       0x467e20
0x467C2E: add      eax, 0xfffffdf2
0x467C33: cmp      eax, 4
0x467C36: ja       0x46af09
0x467C3C: jmp      dword ptr [eax*4 + 0x467c43]
0x467C43: inc      esi
0x467C44: add      byte ptr [esi], 0xc5
0x467C48: add      byte ptr [esi], 0xa8
0x467C4C: add      byte ptr [esi], 0x7a
0x467C50: jle      0x467c98
0x467C52: add      byte ptr [ebp + 0x3d004680], ah
0x467C58: xchg     edx, eax
0x467C59: add      al, byte ptr [eax]
0x467C5B: add      byte ptr [edi + 0x1d], bh
0x467C5E: je       0x4682a8
0x467C64: sub      eax, 0x214
0x467C69: je       0x467e30
0x467C6F: dec      eax
0x467C70: je       0x467e55
0x467C76: jmp      0x46af09
0x467C7B: sub      eax, 0x304
0x467C80: je       0x4682a8
0x467C86: sub      eax, 0x1e
0x467C89: je       0x4682a8
0x467C8F: jmp      0x46af09
0x467C94: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x467C99: mov      eax, dword ptr [eax]
0x467C9B: mov      cx, word ptr [0x46afdc]
0x467CA2: mov      edx, 0x46afe8 ; "你的帐号已经建立了.\ 请妥善保管你的帐户和密码，\并且不要因任何原因把它们告诉任何其他人。\ 如果忘记了密码,\你可以通过我们的主页重新找回它。\(http://www.legendofmir.com.cn)"
0x467CA7: call     0x486df0
0x467CAC: jmp      0x4682a8
0x467CB1: mov      eax, dword ptr [ebp - 0x54]
0x467CB4: sub      eax, -2
0x467CB7: je       0x467d10
0x467CB9: sub      eax, 2
0x467CBC: jne      0x467d2d
0x467CBE: push     0x46b09c ; """
0x467CC3: mov      eax, dword ptr [ebp - 4]
0x467CC6: push     dword ptr [eax + 0x3d4]
0x467CCC: push     0x46b0a8 ; ""这个帐号正在被其他的玩家使用了。\"
0x467CD1: push     0x46b0d4 ; "请使用一个不同的名字。"
0x467CD6: lea      eax, [ebp - 0x8c]
0x467CDC: mov      edx, 4
0x467CE1: call     0x403d94
0x467CE6: mov      edx, dword ptr [ebp - 0x8c]
0x467CEC: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x467CF1: mov      eax, dword ptr [eax]
0x467CF3: mov      cx, word ptr [0x46afdc]
0x467CFA: call     0x486df0
0x467CFF: xor      edx, edx
0x467D01: mov      eax, dword ptr [0x4f7d94]
0x467D06: call     0x47db54
0x467D0B: jmp      0x4682a8
0x467D10: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x467D15: mov      eax, dword ptr [eax]
0x467D17: mov      cx, word ptr [0x46afdc]
0x467D1E: mov      edx, 0x46b0f4 ; "在本服务器下, 这个新名字禁止使用。\请联系我们。"
0x467D23: call     0x486df0
0x467D28: jmp      0x4682a8
0x467D2D: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x467D32: mov      eax, dword ptr [eax]
0x467D34: mov      cx, word ptr [0x46afdc]
0x467D3B: mov      edx, 0x46b12c ; "建立ID失败，请确认它没有包含空格,\特殊字符，或难以辨认的字符。"
0x467D40: call     0x486df0
0x467D45: jmp      0x4682a8
0x467D4A: mov      eax, dword ptr [ebp - 0x54]
0x467D4D: add      eax, 5
0x467D50: cmp      eax, 4
0x467D53: ja       0x467df9
0x467D59: jmp      dword ptr [eax*4 + 0x467d60]
0x467D60: fistp    qword ptr [ebp + 0x46]
0x467D63: add      ch, al
0x467D65: jge      0x467dad
0x467D67: add      byte ptr [ebx - 0x6effb983], ch
0x467D6D: jge      0x467db5
0x467D6F: add      byte ptr [ebp + edi*2 + 0x46], dh
0x467D73: add      byte ptr [ecx + 0x4a4310], ah ; *0x4A4310->0x4F95CC
0x467D79: mov      eax, dword ptr [eax]
0x467D7B: mov      cx, word ptr [0x46afdc]
0x467D82: mov      edx, 0x46b174 ; "密码错误."
0x467D87: call     0x486df0
0x467D8C: jmp      0x467e11
0x467D91: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x467D96: mov      eax, dword ptr [eax]
0x467D98: mov      cx, word ptr [0x46afdc]
0x467D9F: mov      edx, 0x46b188 ; "连续三次密码错误。\你将在一段时间内无法再次连接。."
0x467DA4: call     0x486df0
0x467DA9: jmp      0x467e11
0x467DAB: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x467DB0: mov      eax, dword ptr [eax]
0x467DB2: mov      cx, word ptr [0x46afdc]
0x467DB9: mov      edx, 0x46b1c4 ; "这个帐号正在使用，或者是被异常的终止锁定了,\请稍后再试。"
0x467DBE: call     0x486df0
0x467DC3: jmp      0x467e11
0x467DC5: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x467DCA: mov      eax, dword ptr [eax]
0x467DCC: mov      cx, word ptr [0x46afdc]
0x467DD3: mov      edx, 0x46b208 ; "这个帐户不能正确访问。\请改变帐户,\或者申请付费注册。"
0x467DD8: call     0x486df0
0x467DDD: jmp      0x467e11
0x467DDF: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x467DE4: mov      eax, dword ptr [eax]
0x467DE6: mov      cx, word ptr [0x46afdc]
0x467DED: mov      edx, 0x46b248 ; "这个帐户被禁止了."
0x467DF2: call     0x486df0
0x467DF7: jmp      0x467e11
0x467DF9: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x467DFE: mov      eax, dword ptr [eax]
0x467E00: mov      cx, word ptr [0x46afdc]
0x467E07: mov      edx, 0x46b264 ; "ID不存在或未知错误."
0x467E0C: call     0x486df0
0x467E11: mov      eax, dword ptr [0x4f7d94]
0x467E16: call     0x47c374
0x467E1B: jmp      0x4682a8
0x467E20: mov      edx, dword ptr [ebp - 0x10]
0x467E23: mov      eax, dword ptr [ebp - 4]
0x467E26: call     0x46c108
0x467E2B: jmp      0x4682a8
0x467E30: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x467E35: mov      eax, dword ptr [eax]
0x467E37: mov      cx, word ptr [0x46afdc]
0x467E3E: mov      edx, 0x46b280
0x467E43: call     0x486df0
0x467E48: mov      eax, dword ptr [ebp - 4]
0x467E4B: call     0x46c0b4
0x467E50: jmp      0x4682a8
0x467E55: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x467E5A: mov      eax, dword ptr [eax]
0x467E5C: mov      cx, word ptr [0x46afdc]
0x467E63: mov      edx, 0x46b3b0 ; "更新帐户失败."
0x467E68: call     0x486df0
0x467E6D: mov      eax, dword ptr [ebp - 4]
0x467E70: call     0x46c0b4
0x467E75: jmp      0x4682a8
0x467E7A: mov      ax, word ptr [ebp - 0x54]
0x467E7E: mov      word ptr [0x4f951c], ax
0x467E84: mov      eax, dword ptr [ebp - 0x54]
0x467E87: call     0x406970
0x467E8C: mov      word ptr [0x4f951e], ax
0x467E92: mov      ax, word ptr [ebp - 0x4e]
0x467E96: mov      word ptr [0x4f9520], ax
0x467E9C: mov      ax, word ptr [ebp - 0x4c]
0x467EA0: mov      word ptr [0x4f9522], ax
0x467EA6: cmp      word ptr [0x4f951c], 0
0x467EAE: jbe      0x467f39
0x467EB4: cmp      word ptr [0x4f951c], 1
0x467EBC: jne      0x467edb
0x467EBE: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x467EC3: mov      eax, dword ptr [eax]
0x467EC5: mov      cx, word ptr [0x46afdc]
0x467ECC: mov      edx, 0x46b3c8 ; "你的付费到今天为止。"
0x467ED1: call     0x486df0
0x467ED6: jmp      0x468086
0x467EDB: cmp      word ptr [0x4f951c], 3
0x467EE3: ja       0x468086
0x467EE9: push     0x46b3e8 ; "你的个人帐户的期限:"
0x467EEE: lea      edx, [ebp - 0x94]
0x467EF4: movzx    eax, word ptr [0x4f951c]
0x467EFB: call     0x407d64
0x467F00: push     dword ptr [ebp - 0x94]
0x467F06: push     0x46b404 ; "剩余天数."
0x467F0B: lea      eax, [ebp - 0x8c]
0x467F11: mov      edx, 3
0x467F16: call     0x403d94
0x467F1B: mov      edx, dword ptr [ebp - 0x8c]
0x467F21: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x467F26: mov      eax, dword ptr [eax]
0x467F28: mov      cx, word ptr [0x46afdc]
0x467F2F: call     0x486df0
0x467F34: jmp      0x468086
0x467F39: cmp      word ptr [0x4f9520], 0
0x467F41: jbe      0x467fcc
0x467F47: cmp      word ptr [0x4f9520], 1
0x467F4F: jne      0x467f6e
0x467F51: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x467F56: mov      eax, dword ptr [eax]
0x467F58: mov      cx, word ptr [0x46afdc]
0x467F5F: mov      edx, 0x46b418 ; "当前在用的IP的剩余时间将在今天结束。"
0x467F64: call     0x486df0
0x467F69: jmp      0x468086
0x467F6E: cmp      word ptr [0x4f9520], 3
0x467F76: ja       0x468086
0x467F7C: push     0x46b448 ; "当前IP的周期  "
0x467F81: lea      edx, [ebp - 0x94]
0x467F87: movzx    eax, word ptr [0x4f9520]
0x467F8E: call     0x407d64
0x467F93: push     dword ptr [ebp - 0x94]
0x467F99: push     0x46b404 ; "剩余天数."
0x467F9E: lea      eax, [ebp - 0x8c]
0x467FA4: mov      edx, 3
0x467FA9: call     0x403d94
0x467FAE: mov      edx, dword ptr [ebp - 0x8c]
0x467FB4: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x467FB9: mov      eax, dword ptr [eax]
0x467FBB: mov      cx, word ptr [0x46afdc]
0x467FC2: call     0x486df0
0x467FC7: jmp      0x468086
0x467FCC: cmp      word ptr [0x4f9522], 0
0x467FD4: jbe      0x468031
0x467FD6: cmp      word ptr [0x4f9522], 0x64
0x467FDE: ja       0x468086
0x467FE4: push     0x46b460 ; "IP的周期 "
0x467FE9: lea      edx, [ebp - 0x94]
0x467FEF: movzx    eax, word ptr [0x4f9522]
0x467FF6: call     0x407d64
0x467FFB: push     dword ptr [ebp - 0x94]
0x468001: push     0x46b474 ; "剩余 小时."
0x468006: lea      eax, [ebp - 0x8c]
0x46800C: mov      edx, 3
0x468011: call     0x403d94
0x468016: mov      edx, dword ptr [ebp - 0x8c]
0x46801C: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x468021: mov      eax, dword ptr [eax]
0x468023: mov      cx, word ptr [0x46afdc]
0x46802A: call     0x486df0
0x46802F: jmp      0x468086
0x468031: cmp      word ptr [0x4f951e], 0
0x468039: jbe      0x468086
0x46803B: push     0x46b488 ; "个人帐户的期限"
0x468040: lea      edx, [ebp - 0x94]
0x468046: movzx    eax, word ptr [0x4f951e]
0x46804D: call     0x407d64
0x468052: push     dword ptr [ebp - 0x94]
0x468058: push     0x46b474 ; "剩余 小时."
0x46805D: lea      eax, [ebp - 0x8c]
0x468063: mov      edx, 3
0x468068: call     0x403d94
0x46806D: mov      edx, dword ptr [ebp - 0x8c]
0x468073: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x468078: mov      eax, dword ptr [eax]
0x46807A: mov      cx, word ptr [0x46afdc]
0x468081: call     0x486df0
0x468086: mov      eax, dword ptr [0x4f7d94]
0x46808B: cmp      byte ptr [eax + 0x164], 0
0x468092: jne      0x4682a8
0x468098: mov      eax, dword ptr [ebp - 4]
0x46809B: call     0x46c0b4
0x4680A0: jmp      0x4682a8
0x4680A5: mov      edx, dword ptr [ebp - 0x10]
0x4680A8: mov      eax, dword ptr [ebp - 4]
0x4680AB: call     0x46be6c
0x4680B0: jmp      0x4682a8
0x4680B5: mov      edx, dword ptr [ebp - 0x10]
0x4680B8: mov      eax, dword ptr [ebp - 4]
0x4680BB: call     0x46c174
0x4680C0: jmp      0x4682a8
0x4680C5: mov      byte ptr [0x4f9470], 0
0x4680CC: mov      byte ptr [0x4f9469], 0
0x4680D3: mov      byte ptr [0x4f9468], 0
0x4680DA: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x4680DF: mov      eax, dword ptr [eax]
0x4680E1: mov      cx, word ptr [0x46afdc]
0x4680E8: mov      edx, 0x46b4a0 ; "这个帐号不可用,服务器认正失败。"
0x4680ED: call     0x486df0
0x4680F2: mov      eax, dword ptr [ebp - 4]
0x4680F5: call     0x4405bc
0x4680FA: jmp      0x4682a8
0x4680FF: mov      eax, dword ptr [ebp - 4]
0x468102: call     0x465168
0x468107: jmp      0x4682a8
0x46810C: mov      eax, dword ptr [ebp - 0x54]
0x46810F: sub      eax, 2
0x468112: je       0x46811c
0x468114: dec      eax
0x468115: je       0x468139
0x468117: dec      eax
0x468118: je       0x468156
0x46811A: jmp      0x468173
0x46811C: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x468121: mov      eax, dword ptr [eax]
0x468123: mov      cx, word ptr [0x46afdc]
0x46812A: mov      edx, 0x46b4c8 ; "[failure] 这个名字已经存在。"
0x46812F: call     0x486df0
0x468134: jmp      0x4682a8
0x468139: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46813E: mov      eax, dword ptr [eax]
0x468140: mov      cx, word ptr [0x46afdc]
0x468147: mov      edx, 0x46b4f0 ; "[Failure] 你只能为一个帐户设两个角色.\请和游戏管理员联系。"
0x46814C: call     0x486df0
0x468151: jmp      0x4682a8
0x468156: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46815B: mov      eax, dword ptr [eax]
0x46815D: mov      cx, word ptr [0x46afdc]
0x468164: mov      edx, 0x46b534 ; "[failure] 角色建立失败. Error=4"
0x468169: call     0x486df0
0x46816E: jmp      0x4682a8
0x468173: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x468178: mov      eax, dword ptr [eax]
0x46817A: mov      cx, word ptr [0x46afdc]
0x468181: mov      edx, 0x46b55c ; "[Failure] 未知的错误. 请写信给: support@legendofmir.net"
0x468186: call     0x486df0
0x46818B: jmp      0x4682a8
0x468190: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x468195: mov      eax, dword ptr [eax]
0x468197: mov      cx, word ptr [0x46afdc]
0x46819E: mov      edx, 0x46b59c ; "密码变更成功."
0x4681A3: call     0x486df0
0x4681A8: jmp      0x4682a8
0x4681AD: mov      eax, dword ptr [ebp - 0x54]
0x4681B0: sub      eax, -2
0x4681B3: je       0x4681d5
0x4681B5: dec      eax
0x4681B6: jne      0x4681f2
0x4681B8: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x4681BD: mov      eax, dword ptr [eax]
0x4681BF: mov      cx, word ptr [0x46afdc]
0x4681C6: mov      edx, 0x46b5b4 ; "密码错误。不能进行密码变更。"
0x4681CB: call     0x486df0
0x4681D0: jmp      0x4682a8
0x4681D5: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x4681DA: mov      eax, dword ptr [eax]
0x4681DC: mov      cx, word ptr [0x46afdc]
0x4681E3: mov      edx, 0x46b5dc ; "帐户被锁定 .请稍后再试。"
0x4681E8: call     0x486df0
0x4681ED: jmp      0x4682a8
0x4681F2: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x4681F7: mov      eax, dword ptr [eax]
0x4681F9: mov      cx, word ptr [0x46afdc]
0x468200: mov      edx, 0x46b600 ; "密码少于4位,你不能改变它。"
0x468205: call     0x486df0
0x46820A: jmp      0x4682a8
0x46820F: mov      eax, dword ptr [ebp - 4]
0x468212: call     0x465168
0x468217: jmp      0x4682a8
0x46821C: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x468221: mov      eax, dword ptr [eax]
0x468223: mov      cx, word ptr [0x46afdc]
0x46822A: mov      edx, 0x46b624 ; "[Failure] 删除角色失败, Email: support@legendofmir.com.cn"
0x46822F: call     0x486df0
0x468234: jmp      0x4682a8
0x468236: mov      edx, dword ptr [ebp - 0x10]
0x468239: mov      eax, dword ptr [ebp - 4]
0x46823C: call     0x46c370
0x468241: jmp      0x46af09
0x468246: mov      eax, dword ptr [0x4f7d94]
0x46824B: call     0x47d688
0x468250: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x468255: mov      eax, dword ptr [eax]
0x468257: mov      cx, word ptr [0x46afdc]
0x46825E: mov      edx, 0x46b668 ; "您选择的服务器用户满员。"
0x468263: call     0x486df0
0x468268: mov      eax, dword ptr [0x4f7d88]
0x46826D: call     0x4405bc
0x468272: jmp      0x46af09
0x468277: mov      eax, dword ptr [0x4f7d94]
0x46827C: call     0x47d688
0x468281: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x468286: mov      eax, dword ptr [eax]
0x468288: mov      cx, word ptr [0x46afdc]
0x46828F: mov      edx, 0x46b68c ; "版本错误。请下载最新版本。 (http://www.legendofmir.com.cn)"
0x468294: call     0x486df0
0x468299: mov      eax, dword ptr [0x4f7d88]
0x46829E: call     0x4405bc
0x4682A3: jmp      0x46af09
0x4682A8: cmp      byte ptr [0x4f9478], 0
0x4682AF: je       0x468318
0x4682B1: cmp      word ptr [ebp - 0x50], 0x27a
0x4682B7: jne      0x46af09
0x4682BD: mov      eax, dword ptr [ebp - 4]
0x4682C0: mov      edx, dword ptr [ebp - 0x54]
0x4682C3: mov      dword ptr [eax + 0x3fc], edx
0x4682C9: mov      edx, dword ptr [ebp - 0x50]
0x4682CC: mov      dword ptr [eax + 0x400], edx
0x4682D2: mov      edx, dword ptr [ebp - 0x4c]
0x4682D5: mov      dword ptr [eax + 0x404], edx
0x4682DB: lea      edx, [ebp - 0x8c]
0x4682E1: mov      eax, dword ptr [ebp - 0x10]
0x4682E4: call     0x45b2e0
0x4682E9: mov      edx, dword ptr [ebp - 0x8c]
0x4682EF: mov      eax, dword ptr [ebp - 4]
0x4682F2: add      eax, 0x408
0x4682F7: call     0x403aac
0x4682FC: mov      byte ptr [0x4f9479], 1
0x468303: mov      dl, 1
0x468305: mov      eax, dword ptr [ebp - 4]
0x468308: mov      eax, dword ptr [eax + 0x334]
0x46830E: call     0x4455d4
0x468313: jmp      0x46af09
0x468318: cmp      word ptr [ebp - 0x50], 0x2e
0x46831D: jne      0x468337
0x46831F: movzx    eax, word ptr [ebp - 0x4e]
0x468323: mov      edx, dword ptr [0x4f9598]
0x468329: mov      dword ptr [edx], eax
0x46832B: movzx    eax, word ptr [ebp - 0x4c]
0x46832F: mov      edx, dword ptr [0x4f959c]
0x468335: mov      dword ptr [edx], eax
0x468337: movzx    eax, word ptr [ebp - 0x50]
0x46833B: cmp      eax, 0x290
0x468340: jg       0x4685f9
0x468346: je       0x46988a
0x46834C: cmp      eax, 0xd2
0x468351: jg       0x4684b8
0x468357: je       0x469d71
0x46835D: cmp      eax, 0x1f
0x468360: jg       0x4683f8
0x468366: je       0x46955b
0x46836C: cmp      eax, 0x1e
0x46836F: ja       0x46aeae
0x468375: jmp      dword ptr [eax*4 + 0x46837c]
0x46837C: scasb    al, byte ptr es:[edi]
0x46837D: scasb    al, byte ptr es:[edi]
0x46837E: inc      esi
0x46837F: add      byte ptr [eax - 0x51ffb95e], bh
0x468385: scasb    al, byte ptr es:[edi]
0x468386: inc      esi
0x468387: add      byte ptr [esi - 0x51ffb952], ch
0x46838D: scasb    al, byte ptr es:[edi]
0x46838E: inc      esi
0x46838F: add      byte ptr [esi + 0x1b0046ae], ch
0x468395: mov      es, word ptr [esi]
0x468398: sbb      ecx, dword ptr [esi - 0x6fcaffba]
0x46839E: inc      esi
0x46839F: add      byte ptr [ecx - 0x75], ah
0x4683A2: inc      esi
0x4683A3: add      byte ptr [ecx], bh
0x4683A5: mov      al, byte ptr [esi]
0x4683A8: sbb      ecx, dword ptr [esi - 0x702cffba]
0x4683AE: inc      esi
0x4683AF: add      byte ptr [esi + ecx*4 - 0x6fcaffba], cl
0x4683B6: inc      esi
0x4683B7: add      byte ptr [0x35004690], dh
0x4683BD: nop      
0x4683BE: inc      esi
0x4683BF: add      byte ptr [eax + 0x35004691], ch
0x4683C5: nop      
0x4683C6: inc      esi
0x4683C7: add      byte ptr [0xf3004690], dh
0x4683CD: cwde     
0x4683CE: inc      esi
0x4683CF: add      byte ptr [esi - 0x67], dh
0x4683D2: inc      esi
0x4683D3: add      byte ptr [esi + 0x27004690], ah
0x4683D9: xchg     ecx, eax
0x4683DA: inc      esi
0x4683DB: add      byte ptr [0xae004690], dh
0x4683E1: scasb    al, byte ptr es:[edi]
0x4683E2: inc      esi
0x4683E3: add      byte ptr [esi + 0x450046ae], ch
0x4683E9: xchg     ebx, eax
0x4683EA: inc      esi
0x4683EB: add      byte ptr [edi], ah
0x4683ED: pop      dword ptr [esi]
0x4683F0: mov      bh, 0x98
0x4683F2: inc      esi
0x4683F3: add      byte ptr [edi - 0x7cffb968], dh
0x4683F9: clc      
0x4683FA: xor      bh, byte ptr [edi + 0x55]
0x4683FD: je       0x468865
0x468403: add      eax, -0x20
0x468406: cmp      eax, 0xe
0x468409: ja       0x46aeae
0x46840F: jmp      dword ptr [eax*4 + 0x468416]
0x468416: ja       0x4683aa
0x468418: inc      esi
0x468419: add      byte ptr [ebx], al
0x46841B: xchg     ebx, eax
0x46841C: inc      esi
0x46841D: add      byte ptr [edi - 0x6e], dh
0x468420: inc      esi
0x468421: add      byte ptr [esi - 0x51ffb952], ch
0x468427: scasb    al, byte ptr es:[edi]
0x468428: inc      esi
0x468429: add      byte ptr [esi - 0x51ffb952], ch
0x46842F: scasb    al, byte ptr es:[edi]
0x468430: inc      esi
0x468431: add      byte ptr [esi - 0x3dffb952], ch
0x468437: xchg     edi, eax
0x468438: inc      esi
0x468439: add      bl, bl
0x46843B: pushfd   
0x46843C: inc      esi
0x46843D: add      byte ptr [0xae004698], bl
0x468443: scasb    al, byte ptr es:[edi]
0x468444: inc      esi
0x468445: add      byte ptr [ecx + 0x7004694], dh
0x46844B: xchg     ebp, eax
0x46844C: inc      esi
0x46844D: add      byte ptr [edx - 0x6c], bh
0x468450: inc      esi
0x468451: add      byte ptr [ebx + 0x237d64f8], al
0x468457: sub      eax, 0x33
0x46845A: je       0x468821
0x468460: dec      eax
0x468461: je       0x469387
0x468467: dec      eax
0x468468: je       0x46951b
0x46846E: dec      eax
0x46846F: je       0x468a08
0x468475: jmp      0x46aeae
0x46847A: cmp      eax, 0xc9
0x46847F: jg       0x4684a1
0x468481: je       0x469a18
0x468487: add      eax, -0x64
0x46848A: sub      eax, 5
0x46848D: jb       0x469772
0x468493: sub      eax, 0x5f
0x468496: je       0x469a08
0x46849C: jmp      0x46aeae
0x4684A1: sub      eax, 0xca
0x4684A6: je       0x469a38
0x4684AC: dec      eax
0x4684AD: je       0x469a28
0x4684B3: jmp      0x46aeae
0x4684B8: cmp      eax, 0x27b
0x4684BD: jg       0x468594
0x4684C3: je       0x469d4a
0x4684C9: cmp      eax, 0x266
0x4684CE: jg       0x468533
0x4684D0: je       0x469b14
0x4684D6: cmp      eax, 0x259
0x4684DB: jg       0x46850e
0x4684DD: je       0x469a79
0x4684E3: sub      eax, 0xd3
0x4684E8: je       0x469d8b
0x4684EE: dec      eax
0x4684EF: je       0x469da5
0x4684F5: sub      eax, 0x13c
0x4684FA: je       0x46923d
0x468500: sub      eax, 0x48
0x468503: je       0x469a58
0x468509: jmp      0x46aeae
0x46850E: sub      eax, 0x262
0x468513: je       0x469a9d
0x468519: dec      eax
0x46851A: je       0x469ad0
0x468520: dec      eax
0x468521: je       0x469ae9
0x468527: dec      eax
0x468528: je       0x469b00
0x46852E: jmp      0x46aeae
0x468533: add      eax, 0xfffffd99
0x468538: cmp      eax, 0x12
0x46853B: ja       0x46aeae
0x468541: jmp      dword ptr [eax*4 + 0x468548]
0x468548: sub      ebx, dword ptr [ebx - 0x648bffba]
0x46854E: inc      esi
0x46854F: add      bl, ch
0x468551: scasb    al, byte ptr es:[edi]
0x468552: inc      esi
0x468553: add      bl, ch
0x468555: scasb    al, byte ptr es:[edi]
0x468556: inc      esi
0x468557: add      byte ptr [edx - 0x50ffb965], cl
0x46855D: wait     
0x46855E: inc      esi
0x46855F: add      dh, ah
0x468561: wait     
0x468562: inc      esi
0x468563: add      dh, dh
0x468565: wait     
0x468566: inc      esi
0x468567: add      byte ptr [esi - 0x51ffb952], ch
0x46856D: scasb    al, byte ptr es:[edi]
0x46856E: inc      esi
0x46856F: add      byte ptr [esi - 0x51ffb952], ch
0x468575: scasb    al, byte ptr es:[edi]
0x468576: inc      esi
0x468577: add      byte ptr [esi - 0x51ffb952], ch
0x46857D: scasb    al, byte ptr es:[edi]
0x46857E: inc      esi
0x46857F: add      byte ptr [esi - 0x51ffb952], ch
0x468585: scasb    al, byte ptr es:[edi]
0x468586: inc      esi
0x468587: add      byte ptr [esi - 0x51ffb952], ch
0x46858D: scasb    al, byte ptr es:[edi]
0x46858E: inc      esi
0x46858F: add      byte ptr [esi], bh
0x468591: popfd    
0x468592: inc      esi
0x468593: add      byte ptr [0xfffffd84], al
0x468599: cmp      eax, 0x13
0x46859C: ja       0x46aeae
0x4685A2: jmp      dword ptr [eax*4 + 0x4685a9]
0x4685A9: pop      ebx
0x4685AA: popfd    
0x4685AB: inc      esi
0x4685AC: add      byte ptr [ecx - 0x71], dh
0x4685AF: inc      esi
0x4685B0: add      cl, dl
0x4685B2: xchg     ecx, eax
0x4685B3: inc      esi
0x4685B4: add      byte ptr [ebx], dl
0x4685B6: xchg     edx, eax
0x4685B7: inc      esi
0x4685B8: add      byte ptr [ebp - 0x51ffb963], dh
0x4685BE: scasb    al, byte ptr es:[edi]
0x4685BF: inc      esi
0x4685C0: add      bh, dl
0x4685C2: popfd    
0x4685C3: inc      esi
0x4685C4: add      cl, bh
0x4685C6: popfd    
0x4685C7: inc      esi
0x4685C8: add      byte ptr [edx], ah
0x4685CA: sahf     
0x4685CB: inc      esi
0x4685CC: add      byte ptr [ebx], dh
0x4685CE: sahf     
0x4685CF: inc      esi
0x4685D0: add      byte ptr [esi - 0x62], bl
0x4685D3: inc      esi
0x4685D4: add      byte ptr [esi - 0x62], bh
0x4685D7: inc      esi
0x4685D8: add      ch, cl
0x4685DA: sahf     
0x4685DB: inc      esi
0x4685DC: add      bh, dh
0x4685DE: sahf     
0x4685DF: inc      esi
0x4685E0: add      byte ptr [ecx], cl
0x4685E3: inc      esi
0x4685E4: add      byte ptr [edx - 0x5f], cl
0x4685E7: inc      esi
0x4685E8: add      byte ptr [ebx + 0x7b0046a2], bl
0x4685EE: pushfd   
0x4685EF: inc      esi
0x4685F0: add      bh, bl
0x4685F2: mov      es, word ptr [esi]
0x4685F5: or       ecx, dword ptr [edi - 0x33c2ffba]
0x4685FB: add      al, byte ptr [eax]
0x4685FD: add      byte ptr [edi], cl
0x4685FF: pop      dword ptr [ebx]
0x468601: add      dword ptr [eax], eax
0x468603: add      byte ptr [edi], cl
0x468605: test     byte ptr [ecx], bl
0x468607: or       al, 0
0x468609: add      byte ptr [0xfffffd6f], al
0x46860F: cmp      eax, 0x39
0x468612: ja       0x46aeae
0x468618: jmp      dword ptr [eax*4 + 0x46861f]
0x46861F: or       ebx, dword ptr [ebp - 0x5d3cffba]
0x468625: inc      esi
0x468626: add      bl, dl
0x468628: mov      byte ptr [0xa2f90046], al
0x46862D: inc      esi
0x46862E: add      byte ptr [edi], cl
0x468630: mov      dword ptr [0xa3a20046], eax
0x468635: inc      esi
0x468636: add      byte ptr [esi - 0x5c], dh
0x468639: inc      esi
0x46863A: add      byte ptr [ecx - 0x7affb95d], dh
0x468640: movsb    byte ptr es:[edi], byte ptr [esi]
0x468641: inc      esi
0x468642: add      al, bh
0x468644: movsb    byte ptr es:[edi], byte ptr [esi]
0x468645: inc      esi
0x468646: add      byte ptr [edi], al
0x468648: movsd    dword ptr es:[edi], dword ptr [esi]
0x468649: inc      esi
0x46864A: add      byte ptr [esi - 0x62], ch
0x46864D: inc      esi
0x46864E: add      byte ptr [edi - 0x23ffb961], al
0x468654: lahf     
0x468655: inc      esi
0x468656: add      byte ptr [eax], bh
0x468658: lahf     
0x468659: inc      esi
0x46865A: add      byte ptr [esi - 0x55ffb952], ch
0x468660: movsd    dword ptr es:[edi], dword ptr [esi]
0x468661: inc      esi
0x468662: add      byte ptr [ebx + 0x310046a5], al
0x468668: cmpsb    byte ptr [esi], byte ptr es:[edi]
0x468669: inc      esi
0x46866A: add      byte ptr [esi - 0x5a], bl
0x46866D: inc      esi
0x46866E: add      byte ptr [ebx - 0x51ffb95a], cl
0x468674: cmpsb    byte ptr [esi], byte ptr es:[edi]
0x468675: inc      esi
0x468676: add      byte ptr [esi - 0x51ffb952], ch
0x46867C: scasb    al, byte ptr es:[edi]
0x46867D: inc      esi
0x46867E: add      bl, ah
0x468680: movsd    dword ptr es:[edi], dword ptr [esi]
0x468681: inc      esi
0x468682: add      byte ptr [0x470046a7], ch
0x468688: cmpsd    dword ptr [esi], dword ptr es:[edi]
0x468689: inc      esi
0x46868A: add      byte ptr [ecx - 0x59], ah
0x46868D: inc      esi
0x46868E: add      byte ptr [esi - 0x44ffb959], cl
0x468694: cmpsd    dword ptr [esi], dword ptr es:[edi]
0x468695: inc      esi
0x468696: add      dl, dl
0x468698: cmpsd    dword ptr [esi], dword ptr es:[edi]
0x468699: inc      esi
0x46869A: add      byte ptr [esi - 0x51ffb952], ch
0x4686A0: scasb    al, byte ptr es:[edi]
0x4686A1: inc      esi
0x4686A2: add      byte ptr [esi - 0x51ffb952], ch
0x4686A8: scasb    al, byte ptr es:[edi]
0x4686A9: inc      esi
0x4686AA: add      byte ptr [esi - 0x51ffb952], ch
0x4686B0: scasb    al, byte ptr es:[edi]
0x4686B1: inc      esi
0x4686B2: add      byte ptr [esi - 0x51ffb952], ch
0x4686B8: scasb    al, byte ptr es:[edi]
0x4686B9: inc      esi
0x4686BA: add      byte ptr [esi - 0x51ffb952], ch
0x4686C0: scasb    al, byte ptr es:[edi]
0x4686C1: inc      esi
0x4686C2: add      byte ptr [esi - 0x51ffb952], ch
0x4686C8: scasb    al, byte ptr es:[edi]
0x4686C9: inc      esi
0x4686CA: add      bl, ah
0x4686CC: cmpsd    dword ptr [esi], dword ptr es:[edi]
0x4686CD: inc      esi
0x4686CE: add      byte ptr [0x1d0046a0], bl
0x4686D4: mov      al, byte ptr [0xa01d0046]
0x4686D9: inc      esi
0x4686DA: add      byte ptr [eax - 0x64ffb960], cl
0x4686E0: mov      al, byte ptr [0xa09b0046]
0x4686E5: inc      esi
0x4686E6: add      byte ptr [ebx - 0x4ffb960], bl
0x4686EC: mov      dword ptr [esi], eax
0x4686EF: dec      eax
0x4686F0: lcall    0x46, 0xa7f30046
0x4686F7: push     cs
0x4686F8: test     al, 0x46
0x4686FA: add      byte ptr [ebx - 0x62], cl
0x4686FD: inc      esi
0x4686FE: add      ch, al
0x468700: mov      eax, dword ptr [0xa2000046]
0x468705: inc      esi
0x468706: add      byte ptr [0x304], bh
0x46870C: jg       0x468781
0x46870E: je       0x46ac88
0x468714: add      eax, 0xfffffd12
0x468719: cmp      eax, 0x15
0x46871C: ja       0x46aeae
0x468722: jmp      dword ptr [eax*4 + 0x468729]
0x468729: test     al, 0x46
0x46872C: add      byte ptr [edi - 0x58], dl
0x46872F: inc      esi
0x468730: add      byte ptr [esp + edx*4], dl
0x468733: inc      esi
0x468734: add      byte ptr [eax], ch
0x468736: movsd    dword ptr es:[edi], dword ptr [esi]
0x468737: inc      esi
0x468738: add      byte ptr [ebp + 0x46], bl
0x46873C: add      byte ptr [esi + 0x420046ae], ch
0x468742: movsd    dword ptr es:[edi], dword ptr [esi]
0x468743: inc      esi
0x468744: add      byte ptr [edi - 0x58], ah
0x468747: inc      esi
0x468748: add      byte ptr [eax + ebp*4 + 0x46], dh
0x46874C: add      byte ptr [eax], dh
0x46874E: test     eax, 0xa93d0046
0x468753: inc      esi
0x468754: add      ch, al
0x468756: test     eax, 0xab930046
0x46875B: inc      esi
0x46875C: add      bl, al
0x46875E: stosd    dword ptr es:[edi], eax
0x46875F: inc      esi
0x468760: add      dh, dl
0x468762: lodsb    al, byte ptr [esi]
0x468763: inc      esi
0x468764: add      byte ptr [esi + 0x2b0046ac], bh
0x46876A: mov      al, byte ptr [esi]
0x46876D: aas      
0x46876E: lodsb    al, byte ptr [esi]
0x46876F: inc      esi
0x468770: add      byte ptr [ecx - 0x5effb956], ah
0x468776: stosb    byte ptr es:[edi], al
0x468777: inc      esi
0x468778: add      byte ptr [edx], ch
0x46877A: stosd    dword ptr es:[edi], eax
0x46877B: inc      esi
0x46877C: add      byte ptr [edx], ch
0x46877E: stosd    dword ptr es:[edi], eax
0x46877F: inc      esi
0x468780: add      byte ptr [0x32b], bh
0x468786: jg       0x4687cf
0x468788: je       0x468a18
0x46878E: add      eax, 0xfffffce0
0x468793: cmp      eax, 0xa
0x468796: ja       0x46aeae
0x46879C: jmp      dword ptr [eax*4 + 0x4687a3]
0x4687A3: mov      dword ptr [esi + eax*2 + 0x468cc400], ecx
0x4687AA: add      bl, bl
0x4687AC: mov      dword ptr [esi], eax
0x4687AF: mov      bh, 0x98
0x4687B1: inc      esi
0x4687B2: add      byte ptr [ecx - 0x9ffb967], ah
0x4687B8: cdq      
0x4687B9: inc      esi
0x4687BA: add      byte ptr [ecx - 0x3bffb974], cl
0x4687C0: mov      word ptr [esi], es
0x4687C3: scasb    al, byte ptr es:[edi]
0x4687C4: scasb    al, byte ptr es:[edi]
0x4687C5: inc      esi
0x4687C6: add      byte ptr [esi - 0x14ffb952], ch
0x4687CC: mov      dword ptr [esi], eax
0x4687CF: cmp      eax, 0x450
0x4687D4: jg       0x468801
0x4687D6: je       0x4695fd
0x4687DC: sub      eax, 0x44c
0x4687E1: je       0x469668
0x4687E7: dec      eax
0x4687E8: je       0x4696b2
0x4687EE: dec      eax
0x4687EF: je       0x469735
0x4687F5: dec      eax
0x4687F6: je       0x4696db
0x4687FC: jmp      0x46aeae
0x468801: sub      eax, 0x451
0x468806: je       0x46acee
0x46880C: dec      eax
0x46880D: je       0x468992
0x468813: sub      eax, 0x5e
0x468816: je       0x46ad20
0x46881C: jmp      0x46aeae
0x468821: mov      eax, 0x4f7ee8
0x468826: call     0x403a58
0x46882B: lea      edx, [ebp - 0x20]
0x46882E: mov      eax, dword ptr [ebp - 0x10]
0x468831: call     0x45b2e0
0x468836: movzx    eax, word ptr [ebp - 0x4e]
0x46883A: push     eax
0x46883B: movzx    eax, word ptr [ebp - 0x4c]
0x46883F: push     eax
0x468840: movzx    eax, word ptr [ebp - 0x4a]
0x468844: push     eax
0x468845: push     0
0x468847: push     0
0x468849: push     0
0x46884B: mov      eax, dword ptr [ebp - 0x20]
0x46884E: push     eax
0x46884F: xor      ecx, ecx
0x468851: mov      edx, 0x33
0x468856: mov      eax, dword ptr [0x4f7d9c]
0x46885B: call     0x4738f0
0x468860: jmp      0x46aeeb
0x468865: xor      eax, eax
0x468867: mov      dword ptr [0x4f9238], eax
0x46886C: xor      eax, eax
0x46886E: mov      dword ptr [0x4f923c], eax
0x468873: lea      edx, [ebp - 0x64]
0x468876: mov      ecx, 0x10
0x46887B: mov      eax, dword ptr [ebp - 0x10]
0x46887E: call     0x45b37c
0x468883: movzx    eax, word ptr [ebp - 0x4e]
0x468887: push     eax
0x468888: movzx    eax, word ptr [ebp - 0x4c]
0x46888C: push     eax
0x46888D: movzx    eax, word ptr [ebp - 0x4a]
0x468891: push     eax
0x468892: mov      eax, dword ptr [ebp - 0x64]
0x468895: push     eax
0x468896: mov      eax, dword ptr [ebp - 0x60]
0x468899: push     eax
0x46889A: push     0
0x46889C: push     0
0x46889E: mov      ecx, dword ptr [ebp - 0x54]
0x4688A1: mov      edx, 0x32
0x4688A6: mov      eax, dword ptr [0x4f7d9c]
0x4688AB: call     0x4738f0
0x4688B0: mov      dl, 7
0x4688B2: mov      eax, dword ptr [0x4f7d8c]
0x4688B7: call     0x4799ac
0x4688BC: push     0
0x4688BE: push     0
0x4688C0: push     0
0x4688C2: xor      ecx, ecx
0x4688C4: mov      edx, 0x51
0x4688C9: mov      eax, dword ptr [ebp - 4]
0x4688CC: call     0x464b18
0x4688D1: cmp      byte ptr [ebp - 0x5c], 1
0x4688D5: jne      0x4688e0
0x4688D7: mov      byte ptr [0x4f94f8], 1
0x4688DE: jmp      0x4688e7
0x4688E0: mov      byte ptr [0x4f94f8], 0
0x4688E7: mov      byte ptr [0x4f9234], 0
0x4688EE: cmp      word ptr [0x4f951c], 0
0x4688F6: jbe      0x468916
0x4688F8: push     0xffffff
0x4688FD: mov      ecx, 0x8000
0x468902: mov      edx, 0x46b6d0 ; "充值卡充值成功。"
0x468907: mov      eax, dword ptr [0x4f7d8c]
0x46890C: call     0x479ae0
0x468911: jmp      0x46aeeb
0x468916: cmp      word ptr [0x4f9520], 0
0x46891E: jbe      0x46893e
0x468920: push     0xffffff
0x468925: mov      ecx, 0x8000
0x46892A: mov      edx, 0x46b6ec ; "你被通过固定数量IP连接。"
0x46892F: mov      eax, dword ptr [0x4f7d8c]
0x468934: call     0x479ae0
0x468939: jmp      0x46aeeb
0x46893E: cmp      word ptr [0x4f9522], 0
0x468946: jbe      0x468966
0x468948: push     0xffffff
0x46894D: mov      ecx, 0x8000
0x468952: mov      edx, 0x46b710 ; "你被通过固定时间IP连接。"
0x468957: mov      eax, dword ptr [0x4f7d8c]
0x46895C: call     0x479ae0
0x468961: jmp      0x46aeeb
0x468966: cmp      word ptr [0x4f951e], 0
0x46896E: jbe      0x46aeeb
0x468974: push     0xffffff
0x468979: mov      ecx, 0x8000
0x46897E: mov      edx, 0x46b734 ; "你被通过固定时间帐户充值。"
0x468983: mov      eax, dword ptr [0x4f7d8c]
0x468988: call     0x479ae0
0x46898D: jmp      0x46aeeb
0x468992: lea      edx, [ebp - 0x24]
0x468995: mov      ecx, 4
0x46899A: mov      eax, dword ptr [ebp - 0x10]
0x46899D: call     0x45b37c
0x4689A2: mov      eax, dword ptr [0x4f95a4]
0x4689A7: mov      edx, dword ptr [ebp - 0x54]
0x4689AA: mov      dword ptr [eax], edx
0x4689AC: mov      dx, word ptr [ebp - 0x4c]
0x4689B0: mov      ax, word ptr [ebp - 0x4e]
0x4689B4: call     0x406964
0x4689B9: mov      edx, dword ptr [0x4f95a8]
0x4689BF: mov      dword ptr [edx], eax
0x4689C1: mov      dx, word ptr [ebp - 0x22]
0x4689C5: mov      ax, word ptr [ebp - 0x24]
0x4689C9: call     0x406964
0x4689CE: mov      edx, dword ptr [0x4f95ac]
0x4689D4: mov      dword ptr [edx], eax
0x4689D6: jmp      0x46aeeb
0x4689DB: mov      edx, dword ptr [ebp - 0x10]
0x4689DE: mov      eax, dword ptr [ebp - 4]
0x4689E1: call     0x46c518
0x4689E6: jmp      0x46aeeb
0x4689EB: mov      edx, dword ptr [ebp - 0x54]
0x4689EE: mov      eax, dword ptr [ebp - 4]
0x4689F1: call     0x467718
0x4689F6: jmp      0x46aeeb
0x4689FB: mov      eax, dword ptr [ebp - 0x54]
0x4689FE: mov      dword ptr [0x4f9388], eax
0x468A03: jmp      0x46aeeb
0x468A08: mov      edx, dword ptr [ebp - 0x10]
0x468A0B: mov      eax, dword ptr [ebp - 4]
0x468A0E: call     0x46c7f8
0x468A13: jmp      0x46aeeb
0x468A18: mov      ecx, dword ptr [ebp - 0x10]
0x468A1B: mov      edx, dword ptr [ebp - 0x54]
0x468A1E: mov      eax, dword ptr [ebp - 4]
0x468A21: call     0x46c890
0x468A26: jmp      0x46aeeb
0x468A2B: movzx    eax, word ptr [ebp - 0x4e]
0x468A2F: mov      dword ptr [0x4f938c], eax
0x468A34: jmp      0x46aeeb
0x468A39: push     0x40255555
0x468A3E: push     0x55555555
0x468A43: call     0x458d88
0x468A48: mov      ebx, eax
0x468A4A: mov      eax, dword ptr [ebp - 0x10]
0x468A4D: call     0x403cd4
0x468A52: cmp      ebx, eax
0x468A54: jge      0x468aa9
0x468A56: lea      eax, [ebp - 0x14]
0x468A59: push     eax
0x468A5A: push     0x40255555
0x468A5F: push     0x55555555
0x468A64: call     0x458d88
0x468A69: inc      eax
0x468A6A: push     eax
0x468A6B: mov      eax, dword ptr [ebp - 0x10]
0x468A6E: call     0x403cd4
0x468A73: mov      ecx, eax
0x468A75: mov      eax, dword ptr [ebp - 0x10]
0x468A78: pop      edx
0x468A79: call     0x403ed8
0x468A7E: lea      edx, [ebp - 0x1c]
0x468A81: mov      eax, dword ptr [ebp - 0x14]
0x468A84: call     0x45b2e0
0x468A89: push     0
0x468A8B: lea      eax, [ebp - 0x20]
0x468A8E: push     eax
0x468A8F: mov      byte ptr [ebp - 0x90], 0x2f
0x468A96: lea      ecx, [ebp - 0x90]
0x468A9C: lea      edx, [ebp - 0x1c]
0x468A9F: mov      eax, dword ptr [ebp - 0x1c]
0x468AA2: call     0x452530
0x468AA7: jmp      0x468ab1
0x468AA9: lea      eax, [ebp - 0x1c]
0x468AAC: call     0x403a58
0x468AB1: lea      edx, [ebp - 0x34]
0x468AB4: mov      ecx, 8
0x468AB9: mov      eax, dword ptr [ebp - 0x10]
0x468ABC: call     0x45b37c
0x468AC1: movzx    eax, word ptr [ebp - 0x4e]
0x468AC5: push     eax
0x468AC6: movzx    eax, word ptr [ebp - 0x4c]
0x468ACA: push     eax
0x468ACB: movzx    eax, word ptr [ebp - 0x4a]
0x468ACF: push     eax
0x468AD0: mov      eax, dword ptr [ebp - 0x34]
0x468AD3: push     eax
0x468AD4: mov      eax, dword ptr [ebp - 0x30]
0x468AD7: push     eax
0x468AD8: push     0
0x468ADA: push     0
0x468ADC: mov      ecx, dword ptr [ebp - 0x54]
0x468ADF: mov      edx, 0xa
0x468AE4: mov      eax, dword ptr [0x4f7d9c]
0x468AE9: call     0x4738f0
0x468AEE: cmp      dword ptr [ebp - 0x1c], 0
0x468AF2: je       0x46aeeb
0x468AF8: mov      edx, dword ptr [ebp - 0x54]
0x468AFB: mov      eax, dword ptr [0x4f7d9c]
0x468B00: call     0x472db0
0x468B05: mov      dword ptr [ebp - 0x40], eax
0x468B08: cmp      dword ptr [ebp - 0x40], 0
0x468B0C: je       0x46aeeb
0x468B12: push     0
0x468B14: lea      eax, [ebp - 0x8c]
0x468B1A: push     eax
0x468B1B: mov      byte ptr [ebp - 0x90], 0x5c
0x468B22: lea      ecx, [ebp - 0x90]
0x468B28: mov      eax, dword ptr [ebp - 0x40]
0x468B2B: lea      edx, [eax + 0x28]
0x468B2E: mov      eax, dword ptr [ebp - 0x1c]
0x468B31: call     0x452530
0x468B36: mov      edx, dword ptr [ebp - 0x8c]
0x468B3C: mov      eax, dword ptr [ebp - 0x40]
0x468B3F: add      eax, 0x24
0x468B42: call     0x403aac
0x468B47: xor      edx, edx
0x468B49: mov      eax, dword ptr [ebp - 0x20]
0x468B4C: call     0x4520e8
0x468B51: call     0x45edc8
0x468B56: mov      edx, dword ptr [ebp - 0x40]
0x468B59: mov      dword ptr [edx + 0x2c], eax
0x468B5C: jmp      0x46aeeb
0x468B61: push     0x40255555
0x468B66: push     0x55555555
0x468B6B: call     0x458d88
0x468B70: mov      ebx, eax
0x468B72: mov      eax, dword ptr [ebp - 0x10]
0x468B75: call     0x403cd4
0x468B7A: cmp      ebx, eax
0x468B7C: jge      0x468bd1
0x468B7E: lea      eax, [ebp - 0x14]
0x468B81: push     eax
0x468B82: push     0x40255555
0x468B87: push     0x55555555
0x468B8C: call     0x458d88
0x468B91: inc      eax
0x468B92: push     eax
0x468B93: mov      eax, dword ptr [ebp - 0x10]
0x468B96: call     0x403cd4
0x468B9B: mov      ecx, eax
0x468B9D: mov      eax, dword ptr [ebp - 0x10]
0x468BA0: pop      edx
0x468BA1: call     0x403ed8
0x468BA6: lea      edx, [ebp - 0x1c]
0x468BA9: mov      eax, dword ptr [ebp - 0x14]
0x468BAC: call     0x45b2e0
0x468BB1: push     0
0x468BB3: lea      eax, [ebp - 0x20]
0x468BB6: push     eax
0x468BB7: mov      byte ptr [ebp - 0x90], 0x2f
0x468BBE: lea      ecx, [ebp - 0x90]
0x468BC4: lea      edx, [ebp - 0x1c]
0x468BC7: mov      eax, dword ptr [ebp - 0x1c]
0x468BCA: call     0x452530
0x468BCF: jmp      0x468bd9
0x468BD1: lea      eax, [ebp - 0x1c]
0x468BD4: call     0x403a58
0x468BD9: lea      edx, [ebp - 0x34]
0x468BDC: mov      ecx, 8
0x468BE1: mov      eax, dword ptr [ebp - 0x10]
0x468BE4: call     0x45b37c
0x468BE9: movzx    eax, word ptr [ebp - 0x4e]
0x468BED: push     eax
0x468BEE: movzx    eax, word ptr [ebp - 0x4c]
0x468BF2: push     eax
0x468BF3: movzx    eax, word ptr [ebp - 0x4a]
0x468BF7: push     eax
0x468BF8: mov      eax, dword ptr [ebp - 0x34]
0x468BFB: push     eax
0x468BFC: mov      eax, dword ptr [ebp - 0x30]
0x468BFF: push     eax
0x468C00: push     0
0x468C02: push     0
0x468C04: mov      ecx, dword ptr [ebp - 0x54]
0x468C07: mov      edx, 9
0x468C0C: mov      eax, dword ptr [0x4f7d9c]
0x468C11: call     0x4738f0
0x468C16: cmp      dword ptr [ebp - 0x1c], 0
0x468C1A: je       0x46aeeb
0x468C20: mov      edx, dword ptr [ebp - 0x54]
0x468C23: mov      eax, dword ptr [0x4f7d9c]
0x468C28: call     0x472db0
0x468C2D: mov      dword ptr [ebp - 0x40], eax
0x468C30: cmp      dword ptr [ebp - 0x40], 0
0x468C34: je       0x46aeeb
0x468C3A: push     0
0x468C3C: lea      eax, [ebp - 0x8c]
0x468C42: push     eax
0x468C43: mov      byte ptr [ebp - 0x90], 0x5c
0x468C4A: lea      ecx, [ebp - 0x90]
0x468C50: mov      eax, dword ptr [ebp - 0x40]
0x468C53: lea      edx, [eax + 0x28]
0x468C56: mov      eax, dword ptr [ebp - 0x1c]
0x468C59: call     0x452530
0x468C5E: mov      edx, dword ptr [ebp - 0x8c]
0x468C64: mov      eax, dword ptr [ebp - 0x40]
0x468C67: add      eax, 0x24
0x468C6A: call     0x403aac
0x468C6F: xor      edx, edx
0x468C71: mov      eax, dword ptr [ebp - 0x20]
0x468C74: call     0x4520e8
0x468C79: call     0x45edc8
0x468C7E: mov      edx, dword ptr [ebp - 0x40]
0x468C81: mov      dword ptr [edx + 0x2c], eax
0x468C84: jmp      0x46aeeb
0x468C89: mov      eax, dword ptr [0x4f7ef8]
0x468C8E: mov      eax, dword ptr [eax + 4]
0x468C91: cmp      eax, dword ptr [ebp - 0x54]
0x468C94: je       0x46aeeb
0x468C9A: movzx    eax, word ptr [ebp - 0x4e]
0x468C9E: push     eax
0x468C9F: movzx    eax, word ptr [ebp - 0x4c]
0x468CA3: push     eax
0x468CA4: push     0
0x468CA6: push     0
0x468CA8: push     0
0x468CAA: push     0
0x468CAC: push     0
0x468CAE: movzx    edx, word ptr [ebp - 0x50]
0x468CB2: mov      ecx, dword ptr [ebp - 0x54]
0x468CB5: mov      eax, dword ptr [0x4f7d9c]
0x468CBA: call     0x4738f0
0x468CBF: jmp      0x46aeeb
0x468CC4: push     0x40255555
0x468CC9: push     0x55555555
0x468CCE: call     0x458d88
0x468CD3: mov      ebx, eax
0x468CD5: mov      eax, dword ptr [ebp - 0x10]
0x468CD8: call     0x403cd4
0x468CDD: cmp      ebx, eax
0x468CDF: jge      0x468d34
0x468CE1: lea      eax, [ebp - 0x14]
0x468CE4: push     eax
0x468CE5: push     0x40255555
0x468CEA: push     0x55555555
0x468CEF: call     0x458d88
0x468CF4: inc      eax
0x468CF5: push     eax
0x468CF6: mov      eax, dword ptr [ebp - 0x10]
0x468CF9: call     0x403cd4
0x468CFE: mov      ecx, eax
0x468D00: mov      eax, dword ptr [ebp - 0x10]
0x468D03: pop      edx
0x468D04: call     0x403ed8
0x468D09: lea      edx, [ebp - 0x1c]
0x468D0C: mov      eax, dword ptr [ebp - 0x14]
0x468D0F: call     0x45b2e0
0x468D14: push     0
0x468D16: lea      eax, [ebp - 0x20]
0x468D19: push     eax
0x468D1A: mov      byte ptr [ebp - 0x90], 0x2f
0x468D21: lea      ecx, [ebp - 0x90]
0x468D27: lea      edx, [ebp - 0x1c]
0x468D2A: mov      eax, dword ptr [ebp - 0x1c]
0x468D2D: call     0x452530
0x468D32: jmp      0x468d3c
0x468D34: lea      eax, [ebp - 0x1c]
0x468D37: call     0x403a58
0x468D3C: lea      edx, [ebp - 0x34]
0x468D3F: mov      ecx, 8
0x468D44: mov      eax, dword ptr [ebp - 0x10]
0x468D47: call     0x45b37c
0x468D4C: mov      eax, dword ptr [0x4f7ef8]
0x468D51: mov      eax, dword ptr [eax + 4]
0x468D54: cmp      eax, dword ptr [ebp - 0x54]
0x468D57: je       0x468d7c
0x468D59: mov      ax, word ptr [ebp - 0x4c]
0x468D5D: push     eax
0x468D5E: mov      ax, word ptr [ebp - 0x4a]
0x468D62: push     eax
0x468D63: mov      eax, dword ptr [ebp - 0x34]
0x468D66: push     eax
0x468D67: mov      eax, dword ptr [ebp - 0x30]
0x468D6A: push     eax
0x468D6B: mov      cx, word ptr [ebp - 0x4e]
0x468D6F: mov      edx, dword ptr [ebp - 0x54]
0x468D72: mov      eax, dword ptr [0x4f7d9c]
0x468D77: call     0x472f18
0x468D7C: movzx    eax, word ptr [ebp - 0x4e]
0x468D80: push     eax
0x468D81: movzx    eax, word ptr [ebp - 0x4c]
0x468D85: push     eax
0x468D86: movzx    eax, word ptr [ebp - 0x4a]
0x468D8A: push     eax
0x468D8B: mov      eax, dword ptr [ebp - 0x34]
0x468D8E: push     eax
0x468D8F: mov      eax, dword ptr [ebp - 0x30]
0x468D92: push     eax
0x468D93: push     0
0x468D95: push     0
0x468D97: movzx    edx, word ptr [ebp - 0x50]
0x468D9B: mov      ecx, dword ptr [ebp - 0x54]
0x468D9E: mov      eax, dword ptr [0x4f7d9c]
0x468DA3: call     0x4738f0
0x468DA8: cmp      dword ptr [ebp - 0x1c], 0
0x468DAC: je       0x46aeeb
0x468DB2: mov      edx, dword ptr [ebp - 0x54]
0x468DB5: mov      eax, dword ptr [0x4f7d9c]
0x468DBA: call     0x472db0
0x468DBF: mov      dword ptr [ebp - 0x40], eax
0x468DC2: cmp      dword ptr [ebp - 0x40], 0
0x468DC6: je       0x46aeeb
0x468DCC: push     0
0x468DCE: lea      eax, [ebp - 0x8c]
0x468DD4: push     eax
0x468DD5: mov      byte ptr [ebp - 0x90], 0x5c
0x468DDC: lea      ecx, [ebp - 0x90]
0x468DE2: mov      eax, dword ptr [ebp - 0x40]
0x468DE5: lea      edx, [eax + 0x28]
0x468DE8: mov      eax, dword ptr [ebp - 0x1c]
0x468DEB: call     0x452530
0x468DF0: mov      edx, dword ptr [ebp - 0x8c]
0x468DF6: mov      eax, dword ptr [ebp - 0x40]
0x468DF9: add      eax, 0x24
0x468DFC: call     0x403aac
0x468E01: xor      edx, edx
0x468E03: mov      eax, dword ptr [ebp - 0x20]
0x468E06: call     0x4520e8
0x468E0B: call     0x45edc8
0x468E10: mov      edx, dword ptr [ebp - 0x40]
0x468E13: mov      dword ptr [edx + 0x2c], eax
0x468E16: jmp      0x46aeeb
0x468E1B: lea      edx, [ebp - 0x34]
0x468E1E: mov      ecx, 8
0x468E23: mov      eax, dword ptr [ebp - 0x10]
0x468E26: call     0x45b37c
0x468E2B: mov      eax, dword ptr [0x4f7ef8]
0x468E30: mov      eax, dword ptr [eax + 4]
0x468E33: cmp      eax, dword ptr [ebp - 0x54]
0x468E36: jne      0x468e46
0x468E38: cmp      word ptr [ebp - 0x50], 6
0x468E3D: je       0x468e46
0x468E3F: cmp      word ptr [ebp - 0x50], 7
0x468E44: jne      0x468e72
0x468E46: movzx    eax, word ptr [ebp - 0x4e]
0x468E4A: push     eax
0x468E4B: movzx    eax, word ptr [ebp - 0x4c]
0x468E4F: push     eax
0x468E50: movzx    eax, word ptr [ebp - 0x4a]
0x468E54: push     eax
0x468E55: mov      eax, dword ptr [ebp - 0x34]
0x468E58: push     eax
0x468E59: mov      eax, dword ptr [ebp - 0x30]
0x468E5C: push     eax
0x468E5D: push     0
0x468E5F: push     0
0x468E61: movzx    edx, word ptr [ebp - 0x50]
0x468E65: mov      ecx, dword ptr [ebp - 0x54]
0x468E68: mov      eax, dword ptr [0x4f7d9c]
0x468E6D: call     0x4738f0
0x468E72: cmp      word ptr [ebp - 0x50], 6
0x468E77: jne      0x46aeeb
0x468E7D: call     0x406154
0x468E82: mov      dword ptr [0x4f9454], eax
0x468E87: jmp      0x46aeeb
0x468E8C: lea      edx, [ebp - 0x34]
0x468E8F: mov      ecx, 8
0x468E94: mov      eax, dword ptr [ebp - 0x10]
0x468E97: call     0x45b37c
0x468E9C: mov      eax, dword ptr [0x4f7ef8]
0x468EA1: mov      eax, dword ptr [eax + 4]
0x468EA4: cmp      eax, dword ptr [ebp - 0x54]
0x468EA7: je       0x46aeeb
0x468EAD: movzx    eax, word ptr [ebp - 0x4e]
0x468EB1: push     eax
0x468EB2: movzx    eax, word ptr [ebp - 0x4c]
0x468EB6: push     eax
0x468EB7: movzx    eax, word ptr [ebp - 0x4a]
0x468EBB: push     eax
0x468EBC: mov      eax, dword ptr [ebp - 0x34]
0x468EBF: push     eax
0x468EC0: mov      eax, dword ptr [ebp - 0x30]
0x468EC3: push     eax
0x468EC4: push     0
0x468EC6: push     0
0x468EC8: mov      ecx, dword ptr [ebp - 0x54]
0x468ECB: mov      edx, 0xd
0x468ED0: mov      eax, dword ptr [0x4f7d9c]
0x468ED5: call     0x4738f0
0x468EDA: jmp      0x46aeeb
0x468EDF: mov      edx, dword ptr [ebp - 0x54]
0x468EE2: mov      eax, dword ptr [0x4f7d9c]
0x468EE7: call     0x472db0
0x468EEC: mov      dword ptr [ebp - 0x40], eax
0x468EEF: cmp      dword ptr [ebp - 0x40], 0
0x468EF3: je       0x46aeeb
0x468EF9: movzx    eax, word ptr [ebp - 0x4e]
0x468EFD: mov      edx, dword ptr [ebp - 0x40]
0x468F00: mov      dword ptr [edx + 0x154], eax
0x468F06: jmp      0x46aeeb
0x468F0B: cmp      byte ptr [0x4f7f64], 0
0x468F12: je       0x46aeeb
0x468F18: mov      ax, word ptr [ebp - 0x54]
0x468F1C: mov      word ptr [0x4f7f94], ax
0x468F22: jmp      0x46aeeb
0x468F27: mov      eax, dword ptr [ebp - 4]
0x468F2A: call     0x467060
0x468F2F: lea      edx, [ebp - 0x34]
0x468F32: mov      ecx, 8
0x468F37: mov      eax, dword ptr [ebp - 0x10]
0x468F3A: call     0x45b37c
0x468F3F: movzx    eax, word ptr [ebp - 0x4e]
0x468F43: push     eax
0x468F44: movzx    eax, word ptr [ebp - 0x4c]
0x468F48: push     eax
0x468F49: movzx    eax, word ptr [ebp - 0x4a]
0x468F4D: push     eax
0x468F4E: mov      eax, dword ptr [ebp - 0x34]
0x468F51: push     eax
0x468F52: mov      eax, dword ptr [ebp - 0x30]
0x468F55: push     eax
0x468F56: push     0
0x468F58: push     0
0x468F5A: mov      ecx, dword ptr [ebp - 0x54]
0x468F5D: mov      edx, 0xa
0x468F62: mov      eax, dword ptr [0x4f7d9c]
0x468F67: call     0x4738f0
0x468F6C: jmp      0x46aeeb
0x468F71: lea      edx, [ebp - 0x34]
0x468F74: mov      ecx, 8
0x468F79: mov      eax, dword ptr [ebp - 0x10]
0x468F7C: call     0x45b37c
0x468F81: mov      eax, dword ptr [0x4f7ef8]
0x468F86: mov      eax, dword ptr [eax + 4]
0x468F89: cmp      eax, dword ptr [ebp - 0x54]
0x468F8C: je       0x46aeeb
0x468F92: mov      edx, dword ptr [ebp - 0x54]
0x468F95: mov      eax, dword ptr [0x4f7d9c]
0x468F9A: call     0x472db0
0x468F9F: mov      dword ptr [ebp - 0x40], eax
0x468FA2: cmp      dword ptr [ebp - 0x40], 0
0x468FA6: je       0x46aeeb
0x468FAC: movzx    eax, word ptr [ebp - 0x4c]
0x468FB0: push     eax
0x468FB1: movzx    eax, word ptr [ebp - 0x4a]
0x468FB5: push     eax
0x468FB6: push     0
0x468FB8: push     0
0x468FBA: push     0
0x468FBC: push     0
0x468FBE: movzx    ecx, word ptr [ebp - 0x4e]
0x468FC2: mov      dx, 0xc
0x468FC6: mov      eax, dword ptr [ebp - 0x40]
0x468FC9: call     0x496548
0x468FCE: jmp      0x46aeeb
0x468FD3: lea      edx, [ebp - 0x34]
0x468FD6: mov      ecx, 8
0x468FDB: mov      eax, dword ptr [ebp - 0x10]
0x468FDE: call     0x45b37c
0x468FE3: mov      eax, dword ptr [0x4f7ef8]
0x468FE8: mov      eax, dword ptr [eax + 4]
0x468FEB: cmp      eax, dword ptr [ebp - 0x54]
0x468FEE: je       0x46aeeb
0x468FF4: mov      edx, dword ptr [ebp - 0x54]
0x468FF7: mov      eax, dword ptr [0x4f7d9c]
0x468FFC: call     0x472db0
0x469001: mov      dword ptr [ebp - 0x40], eax
0x469004: cmp      dword ptr [ebp - 0x40], 0
0x469008: je       0x46aeeb
0x46900E: movzx    eax, word ptr [ebp - 0x4c]
0x469012: push     eax
0x469013: movzx    eax, word ptr [ebp - 0x4a]
0x469017: push     eax
0x469018: push     0
0x46901A: push     0
0x46901C: push     0
0x46901E: push     0
0x469020: movzx    ecx, word ptr [ebp - 0x4e]
0x469024: mov      dx, 0xc
0x469028: mov      eax, dword ptr [ebp - 0x40]
0x46902B: call     0x496548
0x469030: jmp      0x46aeeb
0x469035: mov      eax, dword ptr [0x4f7ef8]
0x46903A: mov      eax, dword ptr [eax + 4]
0x46903D: cmp      eax, dword ptr [ebp - 0x54]
0x469040: je       0x46aeeb
0x469046: mov      edx, dword ptr [ebp - 0x54]
0x469049: mov      eax, dword ptr [0x4f7d9c]
0x46904E: call     0x472db0
0x469053: mov      dword ptr [ebp - 0x40], eax
0x469056: cmp      dword ptr [ebp - 0x40], 0
0x46905A: je       0x46aeeb
0x469060: movzx    eax, word ptr [ebp - 0x4c]
0x469064: push     eax
0x469065: movzx    eax, word ptr [ebp - 0x4a]
0x469069: push     eax
0x46906A: push     0
0x46906C: push     0
0x46906E: push     0
0x469070: push     0
0x469072: movzx    ecx, word ptr [ebp - 0x4e]
0x469076: mov      dx, word ptr [ebp - 0x50]
0x46907A: mov      eax, dword ptr [ebp - 0x40]
0x46907D: call     0x496548
0x469082: cmp      word ptr [ebp - 0x50], 0xf
0x469087: jne      0x46aeeb
0x46908D: cmp      dword ptr [ebp - 0x10], 0
0x469091: je       0x46aeeb
0x469097: mov      eax, dword ptr [ebp - 0x40]
0x46909A: mov      byte ptr [eax + 0xdc], 1
0x4690A1: jmp      0x46aeeb
0x4690A6: lea      edx, [ebp - 0x2c]
0x4690A9: mov      ecx, 8
0x4690AE: mov      eax, dword ptr [ebp - 0x10]
0x4690B1: call     0x45b37c
0x4690B6: mov      edx, dword ptr [ebp - 0x54]
0x4690B9: mov      eax, dword ptr [0x4f7d9c]
0x4690BE: call     0x472db0
0x4690C3: mov      dword ptr [ebp - 0x40], eax
0x4690C6: cmp      dword ptr [ebp - 0x40], 0
0x4690CA: je       0x46aeeb
0x4690D0: movzx    eax, word ptr [ebp - 0x4c]
0x4690D4: push     eax
0x4690D5: movzx    eax, word ptr [ebp - 0x4a]
0x4690D9: push     eax
0x4690DA: push     0
0x4690DC: push     0
0x4690DE: push     0
0x4690E0: push     0
0x4690E2: movzx    ecx, word ptr [ebp - 0x4e]
0x4690E6: mov      dx, word ptr [ebp - 0x50]
0x4690EA: mov      eax, dword ptr [ebp - 0x40]
0x4690ED: call     0x496548
0x4690F2: movzx    eax, word ptr [ebp - 0x2c]
0x4690F6: mov      edx, dword ptr [ebp - 0x40]
0x4690F9: mov      dword ptr [edx + 0xc4], eax
0x4690FF: movzx    eax, word ptr [ebp - 0x2a]
0x469103: mov      edx, dword ptr [ebp - 0x40]
0x469106: mov      dword ptr [edx + 0xc8], eax
0x46910C: mov      dx, word ptr [ebp - 0x26]
0x469110: mov      ax, word ptr [ebp - 0x28]
0x469114: call     0x406964
0x469119: mov      edx, dword ptr [ebp - 0x40]
0x46911C: mov      dword ptr [edx + 0xcc], eax
0x469122: jmp      0x46aeeb
0x469127: lea      edx, [ebp - 0x64]
0x46912A: mov      ecx, 0x10
0x46912F: mov      eax, dword ptr [ebp - 0x10]
0x469132: call     0x45b37c
0x469137: mov      edx, dword ptr [ebp - 0x54]
0x46913A: mov      eax, dword ptr [0x4f7d9c]
0x46913F: call     0x472db0
0x469144: mov      dword ptr [ebp - 0x40], eax
0x469147: cmp      dword ptr [ebp - 0x40], 0
0x46914B: je       0x46aeeb
0x469151: movzx    eax, word ptr [ebp - 0x4c]
0x469155: push     eax
0x469156: movzx    eax, word ptr [ebp - 0x4a]
0x46915A: push     eax
0x46915B: push     0
0x46915D: push     0
0x46915F: push     0
0x469161: push     0
0x469163: movzx    ecx, word ptr [ebp - 0x4e]
0x469167: mov      dx, word ptr [ebp - 0x50]
0x46916B: mov      eax, dword ptr [ebp - 0x40]
0x46916E: call     0x496548
0x469173: mov      eax, dword ptr [ebp - 0x64]
0x469176: mov      edx, dword ptr [ebp - 0x40]
0x469179: mov      dword ptr [edx + 0xc4], eax
0x46917F: mov      eax, dword ptr [ebp - 0x60]
0x469182: mov      edx, dword ptr [ebp - 0x40]
0x469185: mov      dword ptr [edx + 0xc8], eax
0x46918B: mov      eax, dword ptr [ebp - 0x5c]
0x46918E: mov      edx, dword ptr [ebp - 0x40]
0x469191: mov      dword ptr [edx + 0xcc], eax
0x469197: mov      eax, dword ptr [ebp - 0x58]
0x46919A: mov      edx, dword ptr [ebp - 0x40]
0x46919D: mov      dword ptr [edx + 0xd4], eax
0x4691A3: jmp      0x46aeeb
0x4691A8: movzx    eax, word ptr [ebp - 0x4e]
0x4691AC: push     eax
0x4691AD: movzx    eax, word ptr [ebp - 0x4c]
0x4691B1: push     eax
0x4691B2: xor      edx, edx
0x4691B4: mov      eax, dword ptr [ebp - 0x10]
0x4691B7: call     0x4520e8
0x4691BC: push     eax
0x4691BD: movzx    ecx, word ptr [ebp - 0x4a]
0x4691C1: mov      edx, dword ptr [ebp - 0x54]
0x4691C4: mov      eax, dword ptr [ebp - 4]
0x4691C7: call     0x46294c
0x4691CC: jmp      0x46aeeb
0x4691D1: lea      edx, [ebp - 0x3c]
0x4691D4: mov      ecx, 4
0x4691D9: mov      eax, dword ptr [ebp - 0x10]
0x4691DC: call     0x45b37c
0x4691E1: mov      ax, word ptr [ebp - 0x4a]
0x4691E5: call     0x406974
0x4691EA: and      eax, 0xff
0x4691EF: push     eax
0x4691F0: movzx    eax, word ptr [ebp - 0x4e]
0x4691F4: push     eax
0x4691F5: movzx    eax, word ptr [ebp - 0x4c]
0x4691F9: push     eax
0x4691FA: mov      eax, dword ptr [ebp - 0x3c]
0x4691FD: push     eax
0x4691FE: xor      ecx, ecx
0x469200: mov      cl, byte ptr [ebp - 0x4a]
0x469203: mov      edx, dword ptr [ebp - 0x54]
0x469206: mov      eax, dword ptr [ebp - 4]
0x469209: call     0x4629f8
0x46920E: jmp      0x46aeeb
0x469213: mov      edx, dword ptr [ebp - 0x54]
0x469216: mov      eax, dword ptr [ebp - 4]
0x469219: call     0x462a64
0x46921E: jmp      0x46aeeb
0x469223: movzx    eax, word ptr [ebp - 0x4c]
0x469227: push     eax
0x469228: movzx    ecx, word ptr [ebp - 0x4e]
0x46922C: movzx    edx, word ptr [ebp - 0x4a]
0x469230: mov      eax, dword ptr [ebp - 4]
0x469233: call     0x462aac
0x469238: jmp      0x46aeeb
0x46923D: mov      byte ptr [0x4f9470], 0
0x469244: mov      byte ptr [0x4f9469], 0
0x46924B: mov      byte ptr [0x4f9468], 0
0x469252: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x469257: mov      eax, dword ptr [eax]
0x469259: mov      cx, word ptr [0x46afdc]
0x469260: mov      edx, 0x46b758 ; "服务器连接被强行中断。\连接时间可能超过限制，\或者用户请求重新连接。"
0x469265: call     0x486df0
0x46926A: mov      eax, dword ptr [ebp - 4]
0x46926D: call     0x4405bc
0x469272: jmp      0x46aeeb
0x469277: lea      edx, [ebp - 0x34]
0x46927A: mov      ecx, 8
0x46927F: mov      eax, dword ptr [ebp - 0x10]
0x469282: call     0x45b37c
0x469287: mov      edx, dword ptr [ebp - 0x54]
0x46928A: mov      eax, dword ptr [0x4f7d9c]
0x46928F: call     0x472db0
0x469294: mov      dword ptr [ebp - 0x40], eax
0x469297: cmp      dword ptr [ebp - 0x40], 0
0x46929B: je       0x4692d1
0x46929D: movzx    eax, word ptr [ebp - 0x4c]
0x4692A1: push     eax
0x4692A2: movzx    eax, word ptr [ebp - 0x4a]
0x4692A6: push     eax
0x4692A7: mov      eax, dword ptr [ebp - 0x34]
0x4692AA: push     eax
0x4692AB: mov      eax, dword ptr [ebp - 0x30]
0x4692AE: push     eax
0x4692AF: push     0
0x4692B1: push     0
0x4692B3: movzx    ecx, word ptr [ebp - 0x4e]
0x4692B7: mov      dx, word ptr [ebp - 0x50]
0x4692BB: mov      eax, dword ptr [ebp - 0x40]
0x4692BE: call     0x496548
0x4692C3: mov      eax, dword ptr [ebp - 0x40]
0x4692C6: mov      word ptr [eax + 0x3c], 0
0x4692CC: jmp      0x46aeeb
0x4692D1: movzx    eax, word ptr [ebp - 0x4e]
0x4692D5: push     eax
0x4692D6: movzx    eax, word ptr [ebp - 0x4c]
0x4692DA: push     eax
0x4692DB: movzx    eax, word ptr [ebp - 0x4a]
0x4692DF: push     eax
0x4692E0: mov      eax, dword ptr [ebp - 0x34]
0x4692E3: push     eax
0x4692E4: mov      eax, dword ptr [ebp - 0x30]
0x4692E7: push     eax
0x4692E8: push     0
0x4692EA: push     0
0x4692EC: mov      ecx, dword ptr [ebp - 0x54]
0x4692EF: mov      edx, 0x20
0x4692F4: mov      eax, dword ptr [0x4f7d9c]
0x4692F9: call     0x4738f0
0x4692FE: jmp      0x46aeeb
0x469303: lea      edx, [ebp - 0x34]
0x469306: mov      ecx, 8
0x46930B: mov      eax, dword ptr [ebp - 0x10]
0x46930E: call     0x45b37c
0x469313: movzx    eax, word ptr [ebp - 0x4e]
0x469317: push     eax
0x469318: movzx    eax, word ptr [ebp - 0x4c]
0x46931C: push     eax
0x46931D: movzx    eax, word ptr [ebp - 0x4a]
0x469321: push     eax
0x469322: mov      eax, dword ptr [ebp - 0x34]
0x469325: push     eax
0x469326: mov      eax, dword ptr [ebp - 0x30]
0x469329: push     eax
0x46932A: push     0
0x46932C: push     0
0x46932E: mov      ecx, dword ptr [ebp - 0x54]
0x469331: mov      edx, 0x21
0x469336: mov      eax, dword ptr [0x4f7d9c]
0x46933B: call     0x4738f0
0x469340: jmp      0x46aeeb
0x469345: lea      edx, [ebp - 0x34]
0x469348: mov      ecx, 8
0x46934D: mov      eax, dword ptr [ebp - 0x10]
0x469350: call     0x45b37c
0x469355: movzx    eax, word ptr [ebp - 0x4e]
0x469359: push     eax
0x46935A: movzx    eax, word ptr [ebp - 0x4c]
0x46935E: push     eax
0x46935F: movzx    eax, word ptr [ebp - 0x4a]
0x469363: push     eax
0x469364: mov      eax, dword ptr [ebp - 0x34]
0x469367: push     eax
0x469368: mov      eax, dword ptr [ebp - 0x30]
0x46936B: push     eax
0x46936C: push     0
0x46936E: push     0
0x469370: mov      ecx, dword ptr [ebp - 0x54]
0x469373: mov      edx, 0x1b
0x469378: mov      eax, dword ptr [0x4f7d9c]
0x46937D: call     0x4738f0
0x469382: jmp      0x46aeeb
0x469387: mov      eax, dword ptr [0x4f7ef8]
0x46938C: mov      edx, dword ptr [ebp - 0x54]
0x46938F: mov      dword ptr [eax + 0x58], edx
0x469392: mov      eax, dword ptr [0x4f7ef8]
0x469397: mov      dl, byte ptr [ebp - 0x4e]
0x46939A: mov      byte ptr [eax + 0x12], dl
0x46939D: mov      eax, dword ptr [0x4f7ef8]
0x4693A2: lea      edx, [eax + 0x30]
0x4693A5: mov      ecx, 0x28
0x4693AA: mov      eax, dword ptr [ebp - 0x10]
0x4693AD: call     0x45b37c
0x4693B2: mov      eax, dword ptr [0x4a2d10] ; *0x4A2D10->0x000BB8
0x4693B7: push     eax
0x4693B8: mov      eax, dword ptr [0x4f7ef8]
0x4693BD: movzx    ecx, word ptr [eax + 0x50]
0x4693C1: mov      eax, dword ptr [0x4f7ef8]
0x4693C6: movzx    eax, word ptr [eax + 0x52]
0x4693CA: add      ecx, eax
0x4693CC: mov      eax, dword ptr [0x4f7ef8]
0x4693D1: movzx    eax, byte ptr [eax + 0x54]
0x4693D5: add      ecx, eax
0x4693D7: mov      eax, dword ptr [0x4f7ef8]
0x4693DC: movzx    eax, byte ptr [eax + 0x55]
0x4693E0: add      ecx, eax
0x4693E2: mov      eax, dword ptr [0x4f7ef8]
0x4693E7: movzx    eax, byte ptr [eax + 0x56]
0x4693EB: add      ecx, eax
0x4693ED: mov      eax, dword ptr [0x4f7ef8]
0x4693F2: movzx    eax, byte ptr [eax + 0x57]
0x4693F6: add      ecx, eax
0x4693F8: mov      eax, dword ptr [0x4f7ef8]
0x4693FD: movsx    edx, byte ptr [eax + 0x5c]
0x469401: mov      eax, dword ptr [0x4f7ef8]
0x469406: movzx    eax, byte ptr [eax + 0x30]
0x46940A: call     0x45eb00
0x46940F: jmp      0x46aeeb
0x469414: xor      eax, eax
0x469416: mov      al, byte ptr [ebp - 0x4e]
0x469419: mov      dword ptr [0x4f9504], eax
0x46941E: mov      ax, word ptr [ebp - 0x4e]
0x469422: call     0x406974
0x469427: and      eax, 0xff
0x46942C: mov      dword ptr [0x4f9500], eax
0x469431: xor      eax, eax
0x469433: mov      al, byte ptr [ebp - 0x4c]
0x469436: mov      dword ptr [0x4f9508], eax
0x46943B: mov      ax, word ptr [ebp - 0x4c]
0x46943F: call     0x406974
0x469444: and      eax, 0xff
0x469449: mov      dword ptr [0x4f950c], eax
0x46944E: xor      eax, eax
0x469450: mov      al, byte ptr [ebp - 0x4a]
0x469453: mov      dword ptr [0x4f9510], eax
0x469458: mov      ax, word ptr [ebp - 0x4a]
0x46945C: call     0x406974
0x469461: and      eax, 0xff
0x469466: mov      dword ptr [0x4f9514], eax
0x46946B: xor      eax, eax
0x46946D: mov      al, byte ptr [ebp - 0x54]
0x469470: mov      dword ptr [0x4f9518], eax
0x469475: jmp      0x46aeeb
0x46947A: movzx    eax, word ptr [ebp - 0x4e]
0x46947E: mov      dword ptr [0x4f9384], eax
0x469483: movzx    eax, word ptr [ebp - 0x4c]
0x469487: mov      edx, dword ptr [0x4a4144] ; *0x4A4144->0x4A7294
0x46948D: mov      dword ptr [edx], eax
0x46948F: mov      eax, dword ptr [0x4a4144] ; *0x4A4144->0x4A7294
0x469494: cmp      dword ptr [eax], 0
0x469497: jne      0x4694a5
0x469499: mov      byte ptr [0x4f9382], 0
0x4694A0: jmp      0x46aeeb
0x4694A5: mov      byte ptr [0x4f9382], 1
0x4694AC: jmp      0x46aeeb
0x4694B1: mov      eax, dword ptr [0x4f7ef8]
0x4694B6: mov      edx, dword ptr [ebp - 0x54]
0x4694B9: mov      dword ptr [eax + 0x48], edx
0x4694BC: push     0xff
0x4694C1: push     0
0x4694C3: lea      edx, [ebp - 0x94]
0x4694C9: movzx    eax, word ptr [ebp - 0x4e]
0x4694CD: call     0x407d64
0x4694D2: push     dword ptr [ebp - 0x94]
0x4694D8: push     0x46b7a8 ; " 经验值增加."
0x4694DD: lea      eax, [ebp - 0x8c]
0x4694E3: mov      edx, 3
0x4694E8: call     0x403d94
0x4694ED: mov      edx, dword ptr [ebp - 0x8c]
0x4694F3: mov      ecx, 0xffffff
0x4694F8: mov      eax, dword ptr [0x4f7d8c]
0x4694FD: call     0x479ae0
0x469502: jmp      0x46aeeb
0x469507: mov      edx, 0x46b7c0 ; "升级!"
0x46950C: mov      eax, dword ptr [0x4f7d8c]
0x469511: call     0x479a68
0x469516: jmp      0x46aeeb
0x46951B: mov      edx, dword ptr [ebp - 0x54]
0x46951E: mov      eax, dword ptr [0x4f7d9c]
0x469523: call     0x472db0
0x469528: mov      dword ptr [ebp - 0x40], eax
0x46952B: cmp      dword ptr [ebp - 0x40], 0
0x46952F: je       0x46aeeb
0x469535: mov      ax, word ptr [ebp - 0x4e]
0x469539: mov      edx, dword ptr [ebp - 0x40]
0x46953C: mov      word ptr [edx + 0x3c], ax
0x469540: mov      ax, word ptr [ebp - 0x4c]
0x469544: mov      edx, dword ptr [ebp - 0x40]
0x469547: mov      word ptr [edx + 0x3e], ax
0x46954B: mov      ax, word ptr [ebp - 0x4a]
0x46954F: mov      edx, dword ptr [ebp - 0x40]
0x469552: mov      word ptr [edx + 0x40], ax
0x469556: jmp      0x46aeeb
0x46955B: lea      edx, [ebp - 0x64]
0x46955E: mov      ecx, 0x10
0x469563: mov      eax, dword ptr [ebp - 0x10]
0x469566: call     0x45b37c
0x46956B: mov      edx, dword ptr [ebp - 0x54]
0x46956E: mov      eax, dword ptr [0x4f7d9c]
0x469573: call     0x472db0
0x469578: mov      dword ptr [ebp - 0x40], eax
0x46957B: cmp      dword ptr [ebp - 0x40], 0
0x46957F: je       0x46aeeb
0x469585: mov      eax, dword ptr [ebp - 0x40]
0x469588: cmp      eax, dword ptr [0x4f7ef8]
0x46958E: jne      0x4695aa
0x469590: mov      eax, dword ptr [0x4f7ef8]
0x469595: cmp      dword ptr [eax + 0x2c], 0xf9
0x46959C: jne      0x4695be
0x46959E: call     0x406154
0x4695A3: mov      dword ptr [0x4f9448], eax
0x4695A8: jmp      0x4695be
0x4695AA: mov      eax, dword ptr [ebp - 0x40]
0x4695AD: call     0x499fd8
0x4695B2: test     al, al
0x4695B4: je       0x4695be
0x4695B6: mov      eax, dword ptr [ebp - 0x40]
0x4695B9: call     0x49a008
0x4695BE: push     0
0x4695C0: movzx    eax, word ptr [ebp - 0x4a]
0x4695C4: push     eax
0x4695C5: mov      eax, dword ptr [ebp - 0x64]
0x4695C8: push     eax
0x4695C9: mov      eax, dword ptr [ebp - 0x60]
0x4695CC: push     eax
0x4695CD: push     0
0x4695CF: mov      eax, dword ptr [ebp - 0x5c]
0x4695D2: push     eax
0x4695D3: mov      ecx, dword ptr [ebp - 0x58]
0x4695D6: mov      dx, 0x1f
0x4695DA: mov      eax, dword ptr [ebp - 0x40]
0x4695DD: call     0x496604
0x4695E2: mov      ax, word ptr [ebp - 0x4e]
0x4695E6: mov      edx, dword ptr [ebp - 0x40]
0x4695E9: mov      word ptr [edx + 0x3c], ax
0x4695ED: mov      ax, word ptr [ebp - 0x4c]
0x4695F1: mov      edx, dword ptr [ebp - 0x40]
0x4695F4: mov      word ptr [edx + 0x40], ax
0x4695F8: jmp      0x46aeeb
0x4695FD: mov      edx, dword ptr [ebp - 0x54]
0x469600: mov      eax, dword ptr [0x4f7d9c]
0x469605: call     0x472db0
0x46960A: mov      dword ptr [ebp - 0x40], eax
0x46960D: cmp      dword ptr [ebp - 0x40], 0
0x469611: je       0x46aeeb
0x469617: lea      edx, [ebp - 0x34]
0x46961A: mov      ecx, 8
0x46961F: mov      eax, dword ptr [ebp - 0x10]
0x469622: call     0x45b37c
0x469627: mov      dx, word ptr [ebp - 0x4c]
0x46962B: mov      ax, word ptr [ebp - 0x4e]
0x46962F: call     0x406964
0x469634: mov      edx, dword ptr [ebp - 0x40]
0x469637: mov      dword ptr [edx + 0xf8], eax
0x46963D: mov      eax, dword ptr [ebp - 0x34]
0x469640: mov      edx, dword ptr [ebp - 0x40]
0x469643: mov      dword ptr [edx + 0xfc], eax
0x469649: mov      eax, dword ptr [ebp - 0x30]
0x46964C: mov      edx, dword ptr [ebp - 0x40]
0x46964F: mov      dword ptr [edx + 0x100], eax
0x469655: mov      eax, dword ptr [ebp - 0x40]
0x469658: mov      eax, dword ptr [eax + 0xf8]
0x46965E: call     0x4a0b8c
0x469663: jmp      0x46aeeb
0x469668: mov      edx, dword ptr [ebp - 0x54]
0x46966B: mov      eax, dword ptr [0x4f7d9c]
0x469670: call     0x472db0
0x469675: mov      dword ptr [ebp - 0x40], eax
0x469678: cmp      dword ptr [ebp - 0x40], 0
0x46967C: je       0x46aeeb
0x469682: mov      eax, dword ptr [ebp - 0x40]
0x469685: cmp      eax, dword ptr [0x4f7ef8]
0x46968B: je       0x4696a3
0x46968D: mov      ax, word ptr [ebp - 0x4e]
0x469691: mov      edx, dword ptr [ebp - 0x40]
0x469694: mov      word ptr [edx + 0x3c], ax
0x469698: mov      ax, word ptr [ebp - 0x4c]
0x46969C: mov      edx, dword ptr [ebp - 0x40]
0x46969F: mov      word ptr [edx + 0x40], ax
0x4696A3: mov      eax, dword ptr [ebp - 0x40]
0x4696A6: mov      byte ptr [eax + 0x134], 1
0x4696AD: jmp      0x46aeeb
0x4696B2: mov      edx, dword ptr [ebp - 0x54]
0x4696B5: mov      eax, dword ptr [0x4f7d9c]
0x4696BA: call     0x472db0
0x4696BF: mov      dword ptr [ebp - 0x40], eax
0x4696C2: cmp      dword ptr [ebp - 0x40], 0
0x4696C6: je       0x46aeeb
0x4696CC: mov      eax, dword ptr [ebp - 0x40]
0x4696CF: mov      byte ptr [eax + 0x134], 0
0x4696D6: jmp      0x46aeeb
0x4696DB: mov      edx, dword ptr [ebp - 0x54]
0x4696DE: mov      eax, dword ptr [0x4f7d9c]
0x4696E3: call     0x472db0
0x4696E8: mov      dword ptr [ebp - 0x40], eax
0x4696EB: cmp      dword ptr [ebp - 0x40], 0
0x4696EF: je       0x46aeeb
0x4696F5: mov      ax, word ptr [ebp - 0x4e]
0x4696F9: mov      edx, dword ptr [ebp - 0x40]
0x4696FC: mov      word ptr [edx + 0x3c], ax
0x469700: mov      ax, word ptr [ebp - 0x4c]
0x469704: mov      edx, dword ptr [ebp - 0x40]
0x469707: mov      word ptr [edx + 0x40], ax
0x46970B: mov      eax, dword ptr [ebp - 0x40]
0x46970E: mov      byte ptr [eax + 0x135], 1
0x469715: mov      eax, dword ptr [ebp - 0x40]
0x469718: mov      dword ptr [eax + 0x13c], 0x7d0
0x469722: call     0x406154
0x469727: mov      edx, dword ptr [ebp - 0x40]
0x46972A: mov      dword ptr [edx + 0x138], eax
0x469730: jmp      0x46aeeb
0x469735: mov      edx, dword ptr [ebp - 0x54]
0x469738: mov      eax, dword ptr [0x4f7d9c]
0x46973D: call     0x472db0
0x469742: mov      dword ptr [ebp - 0x40], eax
0x469745: cmp      dword ptr [ebp - 0x40], 0
0x469749: je       0x46aeeb
0x46974F: mov      eax, dword ptr [ebp - 0x40]
0x469752: mov      edx, dword ptr [0x495c18]
0x469758: call     0x40301c
0x46975D: test     al, al
0x46975F: je       0x46aeeb
0x469765: mov      eax, dword ptr [ebp - 0x40]
0x469768: call     0x49b484
0x46976D: jmp      0x46aeeb
0x469772: lea      edx, [ebp - 0x20]
0x469775: mov      eax, dword ptr [ebp - 0x10]
0x469778: call     0x45b2e0
0x46977D: mov      ax, word ptr [ebp - 0x4e]
0x469781: call     0x406974
0x469786: call     0x45edc8
0x46978B: push     eax
0x46978C: mov      al, byte ptr [ebp - 0x4e]
0x46978F: call     0x45edc8
0x469794: mov      ecx, eax
0x469796: mov      edx, dword ptr [ebp - 0x20]
0x469799: mov      eax, dword ptr [0x4f7d8c]
0x46979E: call     0x479ae0
0x4697A3: cmp      word ptr [ebp - 0x50], 0x68
0x4697A8: jne      0x46aeeb
0x4697AE: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x4697B3: mov      eax, dword ptr [eax]
0x4697B5: mov      edx, dword ptr [ebp - 0x20]
0x4697B8: call     0x4940b0
0x4697BD: jmp      0x46aeeb
0x4697C2: lea      edx, [ebp - 0x20]
0x4697C5: mov      eax, dword ptr [ebp - 0x10]
0x4697C8: call     0x45b2e0
0x4697CD: mov      ax, word ptr [ebp - 0x4e]
0x4697D1: call     0x406974
0x4697D6: call     0x45edc8
0x4697DB: push     eax
0x4697DC: mov      al, byte ptr [ebp - 0x4e]
0x4697DF: call     0x45edc8
0x4697E4: mov      ecx, eax
0x4697E6: mov      edx, dword ptr [ebp - 0x20]
0x4697E9: mov      eax, dword ptr [0x4f7d8c]
0x4697EE: call     0x479ae0
0x4697F3: mov      edx, dword ptr [ebp - 0x54]
0x4697F6: mov      eax, dword ptr [0x4f7d9c]
0x4697FB: call     0x472db0
0x469800: mov      dword ptr [ebp - 0x40], eax
0x469803: cmp      dword ptr [ebp - 0x40], 0
0x469807: je       0x46aeeb
0x46980D: mov      edx, dword ptr [ebp - 0x20]
0x469810: mov      eax, dword ptr [ebp - 0x40]
0x469813: call     0x49a0b4
0x469818: jmp      0x46aeeb
0x46981D: lea      edx, [ebp - 0x20]
0x469820: mov      eax, dword ptr [ebp - 0x10]
0x469823: call     0x45b2e0
0x469828: mov      edx, dword ptr [ebp - 0x54]
0x46982B: mov      eax, dword ptr [0x4f7d9c]
0x469830: call     0x472db0
0x469835: mov      dword ptr [ebp - 0x40], eax
0x469838: cmp      dword ptr [ebp - 0x40], 0
0x46983C: je       0x46aeeb
0x469842: push     0
0x469844: lea      eax, [ebp - 0x8c]
0x46984A: push     eax
0x46984B: mov      byte ptr [ebp - 0x90], 0x5c
0x469852: lea      ecx, [ebp - 0x90]
0x469858: mov      eax, dword ptr [ebp - 0x40]
0x46985B: lea      edx, [eax + 0x28]
0x46985E: mov      eax, dword ptr [ebp - 0x20]
0x469861: call     0x452530
0x469866: mov      edx, dword ptr [ebp - 0x8c]
0x46986C: mov      eax, dword ptr [ebp - 0x40]
0x46986F: add      eax, 0x24
0x469872: call     0x403aac
0x469877: mov      al, byte ptr [ebp - 0x4e]
0x46987A: call     0x45edc8
0x46987F: mov      edx, dword ptr [ebp - 0x40]
0x469882: mov      dword ptr [edx + 0x2c], eax
0x469885: jmp      0x46aeeb
0x46988A: mov      edx, dword ptr [ebp - 0x54]
0x46988D: mov      eax, dword ptr [0x4f7d9c]
0x469892: call     0x472db0
0x469897: mov      dword ptr [ebp - 0x40], eax
0x46989A: cmp      dword ptr [ebp - 0x40], 0
0x46989E: je       0x46aeeb
0x4698A4: mov      al, byte ptr [ebp - 0x4e]
0x4698A7: call     0x45edc8
0x4698AC: mov      edx, dword ptr [ebp - 0x40]
0x4698AF: mov      dword ptr [edx + 0x2c], eax
0x4698B2: jmp      0x46aeeb
0x4698B7: mov      eax, dword ptr [0x4f7ef8]
0x4698BC: mov      eax, dword ptr [eax + 4]
0x4698BF: cmp      eax, dword ptr [ebp - 0x54]
0x4698C2: je       0x46aeeb
0x4698C8: movzx    eax, word ptr [ebp - 0x4e]
0x4698CC: push     eax
0x4698CD: movzx    eax, word ptr [ebp - 0x4c]
0x4698D1: push     eax
0x4698D2: push     0
0x4698D4: push     0
0x4698D6: push     0
0x4698D8: push     0
0x4698DA: push     0
0x4698DC: mov      ecx, dword ptr [ebp - 0x54]
0x4698DF: mov      edx, 0x1d
0x4698E4: mov      eax, dword ptr [0x4f7d9c]
0x4698E9: call     0x4738f0
0x4698EE: jmp      0x46aeeb
0x4698F3: lea      edx, [ebp - 0x64]
0x4698F6: mov      ecx, 0x10
0x4698FB: mov      eax, dword ptr [ebp - 0x10]
0x4698FE: call     0x45b37c
0x469903: mov      edx, dword ptr [ebp - 0x54]
0x469906: mov      eax, dword ptr [0x4f7d9c]
0x46990B: call     0x472db0
0x469910: mov      dword ptr [ebp - 0x40], eax
0x469913: cmp      dword ptr [ebp - 0x40], 0
0x469917: jne      0x46993f
0x469919: mov      ax, word ptr [ebp - 0x4c]
0x46991D: push     eax
0x46991E: mov      ax, word ptr [ebp - 0x4a]
0x469922: push     eax
0x469923: mov      eax, dword ptr [ebp - 0x64]
0x469926: push     eax
0x469927: mov      eax, dword ptr [ebp - 0x60]
0x46992A: push     eax
0x46992B: mov      cx, word ptr [ebp - 0x4e]
0x46992F: mov      edx, dword ptr [ebp - 0x54]
0x469932: mov      eax, dword ptr [0x4f7d9c]
0x469937: call     0x472f18
0x46993C: mov      dword ptr [ebp - 0x40], eax
0x46993F: mov      eax, dword ptr [ebp - 0x5c]
0x469942: mov      edx, dword ptr [ebp - 0x40]
0x469945: mov      dword ptr [edx + 0xd8], eax
0x46994B: movzx    eax, word ptr [ebp - 0x4c]
0x46994F: push     eax
0x469950: movzx    eax, word ptr [ebp - 0x4a]
0x469954: push     eax
0x469955: mov      eax, dword ptr [ebp - 0x64]
0x469958: push     eax
0x469959: mov      eax, dword ptr [ebp - 0x60]
0x46995C: push     eax
0x46995D: push     0
0x46995F: push     0
0x469961: movzx    ecx, word ptr [ebp - 0x4e]
0x469965: mov      dx, 0x14
0x469969: mov      eax, dword ptr [ebp - 0x40]
0x46996C: call     0x496548
0x469971: jmp      0x46aeeb
0x469976: movzx    eax, word ptr [ebp - 0x4e]
0x46997A: push     eax
0x46997B: movzx    eax, word ptr [ebp - 0x4c]
0x46997F: push     eax
0x469980: push     0
0x469982: push     0
0x469984: push     0
0x469986: push     0
0x469988: push     0
0x46998A: mov      ecx, dword ptr [ebp - 0x54]
0x46998D: mov      edx, 0x15
0x469992: mov      eax, dword ptr [0x4f7d9c]
0x469997: call     0x4738f0
0x46999C: jmp      0x46aeeb
0x4699A1: lea      edx, [ebp - 0x24]
0x4699A4: mov      ecx, 4
0x4699A9: mov      eax, dword ptr [ebp - 0x10]
0x4699AC: call     0x45b37c
0x4699B1: movzx    eax, word ptr [ebp - 0x4c]
0x4699B5: push     eax
0x4699B6: movzx    eax, word ptr [ebp - 0x4a]
0x4699BA: push     eax
0x4699BB: movzx    eax, word ptr [ebp - 0x4e]
0x4699BF: push     eax
0x4699C0: mov      ecx, dword ptr [ebp - 0x54]
0x4699C3: mov      dl, 1
0x4699C5: mov      eax, dword ptr [0x4772f0]
0x4699CA: call     0x4773b0
0x4699CF: mov      dword ptr [ebp - 0x44], eax
0x4699D2: mov      eax, dword ptr [ebp - 0x44]
0x4699D5: xor      edx, edx
0x4699D7: mov      dword ptr [eax + 0xc], edx
0x4699DA: movzx    eax, word ptr [ebp - 0x24]
0x4699DE: mov      edx, dword ptr [ebp - 0x44]
0x4699E1: mov      dword ptr [edx + 0x1c], eax
0x4699E4: mov      edx, dword ptr [ebp - 0x44]
0x4699E7: mov      eax, dword ptr [0x4f7dcc]
0x4699EC: call     0x4777f0
0x4699F1: jmp      0x46aeeb
0x4699F6: mov      edx, dword ptr [ebp - 0x54]
0x4699F9: mov      eax, dword ptr [0x4f7dcc]
0x4699FE: call     0x477874
0x469A03: jmp      0x46aeeb
0x469A08: mov      edx, dword ptr [ebp - 0x10]
0x469A0B: mov      eax, dword ptr [ebp - 4]
0x469A0E: call     0x46c978
0x469A13: jmp      0x46aeeb
0x469A18: mov      edx, dword ptr [ebp - 0x10]
0x469A1B: mov      eax, dword ptr [ebp - 4]
0x469A1E: call     0x46ce78
0x469A23: jmp      0x46aeeb
0x469A28: mov      edx, dword ptr [ebp - 0x10]
0x469A2B: mov      eax, dword ptr [ebp - 4]
0x469A2E: call     0x46ca24
0x469A33: jmp      0x46aeeb
0x469A38: mov      edx, dword ptr [ebp - 0x10]
0x469A3B: mov      eax, dword ptr [ebp - 4]
0x469A3E: call     0x46cad8
0x469A43: jmp      0x46aeeb
0x469A48: mov      edx, dword ptr [ebp - 0x10]
0x469A4B: mov      eax, dword ptr [ebp - 4]
0x469A4E: call     0x46cb98
0x469A53: jmp      0x46aeeb
0x469A58: lea      edx, [ebp - 0x8c]
0x469A5E: mov      eax, dword ptr [ebp - 0x10]
0x469A61: call     0x45b2e0
0x469A66: mov      eax, dword ptr [ebp - 0x8c]
0x469A6C: mov      edx, dword ptr [ebp - 0x54]
0x469A6F: call     0x49fd14
0x469A74: jmp      0x46aeeb
0x469A79: lea      edx, [ebp - 0x8c]
0x469A7F: mov      eax, dword ptr [ebp - 0x10]
0x469A82: call     0x45b2e0
0x469A87: mov      edx, dword ptr [ebp - 0x8c]
0x469A8D: mov      ecx, dword ptr [ebp - 0x54]
0x469A90: mov      eax, dword ptr [ebp - 4]
0x469A93: call     0x46d00c
0x469A98: jmp      0x46aeeb
0x469A9D: movzx    eax, word ptr [ebp - 0x4c]
0x469AA1: push     eax
0x469AA2: movzx    eax, word ptr [ebp - 0x4a]
0x469AA6: push     eax
0x469AA7: lea      edx, [ebp - 0x8c]
0x469AAD: mov      eax, dword ptr [ebp - 0x10]
0x469AB0: call     0x45b2e0
0x469AB5: mov      eax, dword ptr [ebp - 0x8c]
0x469ABB: push     eax
0x469ABC: movzx    ecx, word ptr [ebp - 0x4e]
0x469AC0: mov      edx, dword ptr [ebp - 0x54]
0x469AC3: mov      eax, dword ptr [ebp - 4]
0x469AC6: call     0x46d07c
0x469ACB: jmp      0x46aeeb
0x469AD0: movzx    eax, word ptr [ebp - 0x4c]
0x469AD4: push     eax
0x469AD5: movzx    ecx, word ptr [ebp - 0x4e]
0x469AD9: mov      edx, dword ptr [ebp - 0x54]
0x469ADC: mov      eax, dword ptr [ebp - 4]
0x469ADF: call     0x46d188
0x469AE4: jmp      0x46aeeb
0x469AE9: movzx    ecx, word ptr [ebp - 0x4c]
0x469AED: movzx    edx, word ptr [ebp - 0x4e]
0x469AF1: mov      eax, dword ptr [0x4f7ef4]
0x469AF6: call     0x49f2fc
0x469AFB: jmp      0x46aeeb
0x469B00: mov      edx, 0x46b7d0 ; "门被锁住了。"
0x469B05: mov      eax, dword ptr [0x4f7d8c]
0x469B0A: call     0x479a68
0x469B0F: jmp      0x46aeeb
0x469B14: movzx    ecx, word ptr [ebp - 0x4c]
0x469B18: movzx    edx, word ptr [ebp - 0x4e]
0x469B1C: mov      eax, dword ptr [0x4f7ef4]
0x469B21: call     0x49f420
0x469B26: jmp      0x46aeeb
0x469B2B: mov      eax, dword ptr [0x4f7ef8]
0x469B30: mov      edx, dword ptr [ebp - 0x54]
0x469B33: mov      dword ptr [eax + 0x18], edx
0x469B36: mov      eax, dword ptr [0x4f7ef8]
0x469B3B: mov      bx, 0xfff5
0x469B3F: call     0x403080
0x469B44: mov      eax, dword ptr [0x4f93d8]
0x469B49: sub      eax, 9
0x469B4C: jae      0x469b68
0x469B4E: imul     eax, dword ptr [0x4f93d8], 0xd
0x469B55: lea      edi, [eax*4 + 0x4f7efc]
0x469B5C: mov      esi, 0x4f93dc
0x469B61: mov      ecx, 0xd
0x469B66: rep movsd dword ptr es:[edi], dword ptr [esi]
0x469B68: mov      byte ptr [0x4f93dc], 0
0x469B6F: jmp      0x46aeeb
0x469B74: mov      eax, 0x4f93dc
0x469B79: call     0x49f874
0x469B7E: mov      byte ptr [0x4f93dc], 0
0x469B85: jmp      0x46aeeb
0x469B8A: mov      eax, dword ptr [0x4f7ef8]
0x469B8F: mov      edx, dword ptr [ebp - 0x54]
0x469B92: mov      dword ptr [eax + 0x18], edx
0x469B95: mov      eax, dword ptr [0x4f7ef8]
0x469B9A: mov      bx, 0xfff5
0x469B9E: call     0x403080
0x469BA3: mov      byte ptr [0x4f93dc], 0
0x469BAA: jmp      0x46aeeb
0x469BAF: cmp      dword ptr [0x4f93d8], 0
0x469BB6: jge      0x469bda
0x469BB8: mov      eax, dword ptr [0x4f93d8]
0x469BBD: inc      eax
0x469BBE: neg      eax
0x469BC0: mov      dword ptr [ebp - 0x38], eax
0x469BC3: imul     eax, dword ptr [ebp - 0x38], 0xd
0x469BC7: lea      edi, [eax*4 + 0x4f7efc]
0x469BCE: mov      esi, 0x4f93dc
0x469BD3: mov      ecx, 0xd
0x469BD8: rep movsd dword ptr es:[edi], dword ptr [esi]
0x469BDA: mov      byte ptr [0x4f93dc], 0
0x469BE1: jmp      0x46aeeb
0x469BE6: mov      edx, dword ptr [ebp - 0x10]
0x469BE9: mov      eax, dword ptr [ebp - 4]
0x469BEC: call     0x46d1f8
0x469BF1: jmp      0x46aeeb
0x469BF6: mov      eax, dword ptr [0x4f7ef8]
0x469BFB: mov      dx, word ptr [ebp - 0x54]
0x469BFF: mov      word ptr [eax + 0x50], dx
0x469C03: mov      eax, dword ptr [0x4f7ef8]
0x469C08: mov      dl, byte ptr [ebp - 0x4e]
0x469C0B: mov      byte ptr [eax + 0x54], dl
0x469C0E: mov      eax, dword ptr [0x4f7ef8]
0x469C13: mov      dl, byte ptr [ebp - 0x4c]
0x469C16: mov      byte ptr [eax + 0x56], dl
0x469C19: mov      eax, dword ptr [0x4a2d10] ; *0x4A2D10->0x000BB8
0x469C1E: push     eax
0x469C1F: mov      eax, dword ptr [0x4f7ef8]
0x469C24: movzx    ecx, word ptr [eax + 0x50]
0x469C28: mov      eax, dword ptr [0x4f7ef8]
0x469C2D: movzx    eax, word ptr [eax + 0x52]
0x469C31: add      ecx, eax
0x469C33: mov      eax, dword ptr [0x4f7ef8]
0x469C38: movzx    eax, byte ptr [eax + 0x54]
0x469C3C: add      ecx, eax
0x469C3E: mov      eax, dword ptr [0x4f7ef8]
0x469C43: movzx    eax, byte ptr [eax + 0x55]
0x469C47: add      ecx, eax
0x469C49: mov      eax, dword ptr [0x4f7ef8]
0x469C4E: movzx    eax, byte ptr [eax + 0x56]
0x469C52: add      ecx, eax
0x469C54: mov      eax, dword ptr [0x4f7ef8]
0x469C59: movzx    eax, byte ptr [eax + 0x57]
0x469C5D: add      ecx, eax
0x469C5F: mov      eax, dword ptr [0x4f7ef8]
0x469C64: movsx    edx, byte ptr [eax + 0x5c]
0x469C68: mov      eax, dword ptr [0x4f7ef8]
0x469C6D: movzx    eax, byte ptr [eax + 0x30]
0x469C71: call     0x45eb00
0x469C76: jmp      0x46aeeb
0x469C7B: mov      eax, 0x6a
0x469C80: call     0x495794
0x469C85: mov      eax, dword ptr [0x4f7ef8]
0x469C8A: mov      eax, dword ptr [eax + 0x58]
0x469C8D: cmp      eax, dword ptr [ebp - 0x54]
0x469C90: jge      0x469ccb
0x469C92: mov      eax, dword ptr [0x4f7ef8]
0x469C97: push     dword ptr [eax + 0x58]
0x469C9A: mov      eax, dword ptr [ebp - 0x54]
0x469C9D: pop      edx
0x469C9E: sub      eax, edx
0x469CA0: lea      edx, [ebp - 0x8c]
0x469CA6: call     0x407d64
0x469CAB: lea      eax, [ebp - 0x8c]
0x469CB1: mov      edx, 0x46b7e8 ; " 金币增加."
0x469CB6: call     0x403cdc
0x469CBB: mov      edx, dword ptr [ebp - 0x8c]
0x469CC1: mov      eax, dword ptr [0x4f7d8c]
0x469CC6: call     0x479a68
0x469CCB: mov      eax, dword ptr [0x4f7ef8]
0x469CD0: mov      edx, dword ptr [ebp - 0x54]
0x469CD3: mov      dword ptr [eax + 0x58], edx
0x469CD6: jmp      0x46aeeb
0x469CDB: push     0
0x469CDD: push     0
0x469CDF: push     0
0x469CE1: mov      dx, word ptr [ebp - 0x4c]
0x469CE5: mov      ax, word ptr [ebp - 0x4e]
0x469CE9: call     0x406964
0x469CEE: push     eax
0x469CEF: push     0
0x469CF1: push     0
0x469CF3: push     0
0x469CF5: movzx    edx, word ptr [ebp - 0x50]
0x469CF9: mov      ecx, dword ptr [ebp - 0x54]
0x469CFC: mov      eax, dword ptr [0x4f7d9c]
0x469D01: call     0x4738f0
0x469D06: jmp      0x46aeeb
0x469D0B: push     0
0x469D0D: push     0
0x469D0F: push     0
0x469D11: mov      dx, word ptr [ebp - 0x4c]
0x469D15: mov      ax, word ptr [ebp - 0x4e]
0x469D19: call     0x406964
0x469D1E: push     eax
0x469D1F: movzx    eax, word ptr [ebp - 0x4a]
0x469D23: push     eax
0x469D24: push     0
0x469D26: push     0
0x469D28: movzx    edx, word ptr [ebp - 0x50]
0x469D2C: mov      ecx, dword ptr [ebp - 0x54]
0x469D2F: mov      eax, dword ptr [0x4f7d9c]
0x469D34: call     0x4738f0
0x469D39: jmp      0x46aeeb
0x469D3E: mov      byte ptr [0x4f9478], 1
0x469D45: jmp      0x46aeeb
0x469D4A: mov      byte ptr [0x4f9410], 0
0x469D51: call     0x49faa4
0x469D56: jmp      0x46aeeb
0x469D5B: mov      eax, 0x4f9410
0x469D60: call     0x49f874
0x469D65: mov      byte ptr [0x4f9410], 0
0x469D6C: jmp      0x46aeeb
0x469D71: cmp      dword ptr [ebp - 0x10], 0
0x469D75: je       0x46aeeb
0x469D7B: mov      edx, dword ptr [ebp - 0x10]
0x469D7E: mov      eax, dword ptr [ebp - 4]
0x469D81: call     0x46d300
0x469D86: jmp      0x46aeeb
0x469D8B: cmp      dword ptr [ebp - 0x10], 0
0x469D8F: je       0x46aeeb
0x469D95: mov      edx, dword ptr [ebp - 0x10]
0x469D98: mov      eax, dword ptr [ebp - 4]
0x469D9B: call     0x46d3d8
0x469DA0: jmp      0x46aeeb
0x469DA5: mov      edx, dword ptr [ebp - 0x54]
0x469DA8: mov      eax, dword ptr [ebp - 4]
0x469DAB: call     0x46d370
0x469DB0: jmp      0x46aeeb
0x469DB5: mov      dx, word ptr [ebp - 0x4a]
0x469DB9: mov      ax, word ptr [ebp - 0x4c]
0x469DBD: call     0x406964
0x469DC2: push     eax
0x469DC3: movzx    ecx, word ptr [ebp - 0x4e]
0x469DC7: mov      edx, dword ptr [ebp - 0x54]
0x469DCA: mov      eax, dword ptr [ebp - 4]
0x469DCD: call     0x46d4d8
0x469DD2: jmp      0x46aeeb
0x469DD7: mov      dx, word ptr [ebp - 0x4a]
0x469DDB: mov      ax, word ptr [ebp - 0x4c]
0x469DDF: call     0x406964
0x469DE4: push     eax
0x469DE5: movzx    edx, word ptr [ebp - 0x4e]
0x469DE9: mov      ecx, dword ptr [ebp - 0x54]
0x469DEC: mov      eax, dword ptr [ebp - 4]
0x469DEF: call     0x46d548
0x469DF4: jmp      0x46aeeb
0x469DF9: lea      edx, [ebp - 0x8c]
0x469DFF: mov      eax, dword ptr [ebp - 0x10]
0x469E02: call     0x45b2e0
0x469E07: mov      eax, dword ptr [ebp - 0x8c]
0x469E0D: push     eax
0x469E0E: movzx    ecx, word ptr [ebp - 0x4e]
0x469E12: mov      edx, dword ptr [ebp - 0x54]
0x469E15: mov      eax, dword ptr [ebp - 4]
0x469E18: call     0x46d594
0x469E1D: jmp      0x46aeeb
0x469E22: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x469E27: mov      eax, dword ptr [eax]
0x469E29: call     0x48e9d0
0x469E2E: jmp      0x46aeeb
0x469E33: mov      eax, dword ptr [ebp - 0x10]
0x469E36: push     eax
0x469E37: movzx    ecx, word ptr [ebp - 0x4e]
0x469E3B: mov      edx, dword ptr [ebp - 0x54]
0x469E3E: mov      eax, dword ptr [ebp - 4]
0x469E41: call     0x46d674
0x469E46: jmp      0x46aeeb
0x469E4B: mov      ecx, dword ptr [ebp - 0x10]
0x469E4E: mov      edx, dword ptr [ebp - 0x54]
0x469E51: mov      eax, dword ptr [ebp - 4]
0x469E54: call     0x46d86c
0x469E59: jmp      0x46aeeb
0x469E5E: mov      edx, dword ptr [ebp - 0x54]
0x469E61: mov      eax, dword ptr [ebp - 4]
0x469E64: call     0x46da68
0x469E69: jmp      0x46aeeb
0x469E6E: mov      edx, dword ptr [ebp - 0x54]
0x469E71: mov      eax, dword ptr [ebp - 4]
0x469E74: call     0x46daa8
0x469E79: jmp      0x46aeeb
0x469E7E: cmp      byte ptr [0x4f92d4], 0
0x469E85: je       0x46aeeb
0x469E8B: cmp      dword ptr [ebp - 0x54], 0
0x469E8F: jle      0x469eb9
0x469E91: lea      edx, [ebp - 0x8c]
0x469E97: mov      eax, dword ptr [ebp - 0x54]
0x469E9A: call     0x407d64
0x469E9F: mov      edx, dword ptr [ebp - 0x8c]
0x469EA5: mov      eax, 0x4f9378
0x469EAA: mov      ecx, 0x46b7fc ; "金币"
0x469EAF: call     0x403d20
0x469EB4: jmp      0x46aeeb
0x469EB9: mov      eax, 0x4f9378
0x469EBE: mov      edx, 0x46b80c ; "????金币"
0x469EC3: call     0x403aac
0x469EC8: jmp      0x46aeeb
0x469ECD: call     0x406154
0x469ED2: mov      edx, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x469ED8: mov      edx, dword ptr [edx]
0x469EDA: mov      dword ptr [edx + 0x75c], eax
0x469EE0: mov      eax, dword ptr [0x4f7ef8]
0x469EE5: mov      edx, dword ptr [ebp - 0x54]
0x469EE8: mov      dword ptr [eax + 0x58], edx
0x469EEB: mov      byte ptr [0x4f9308], 0
0x469EF2: jmp      0x46aeeb
0x469EF7: call     0x406154
0x469EFC: mov      edx, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x469F02: mov      edx, dword ptr [edx]
0x469F04: mov      dword ptr [edx + 0x75c], eax
0x469F0A: mov      eax, 0x4f9308
0x469F0F: call     0x49f874
0x469F14: mov      byte ptr [0x4f9308], 0
0x469F1B: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x469F20: mov      eax, dword ptr [eax]
0x469F22: mov      cx, word ptr [0x46afdc]
0x469F29: mov      edx, 0x46b820 ; "你不能卖这个物品"
0x469F2E: call     0x486df0
0x469F33: jmp      0x46aeeb
0x469F38: cmp      byte ptr [0x4f92d4], 0
0x469F3F: je       0x46aeeb
0x469F45: cmp      dword ptr [ebp - 0x54], 0
0x469F49: jl       0x469f73
0x469F4B: lea      edx, [ebp - 0x8c]
0x469F51: mov      eax, dword ptr [ebp - 0x54]
0x469F54: call     0x407d64
0x469F59: mov      edx, dword ptr [ebp - 0x8c]
0x469F5F: mov      eax, 0x4f9378
0x469F64: mov      ecx, 0x46b7fc ; "金币"
0x469F69: call     0x403d20
0x469F6E: jmp      0x46aeeb
0x469F73: mov      eax, 0x4f9378
0x469F78: mov      edx, 0x46b80c ; "????金币"
0x469F7D: call     0x403aac
0x469F82: jmp      0x46aeeb
0x469F87: cmp      byte ptr [0x4f9308], 0
0x469F8E: je       0x46aeeb
0x469F94: call     0x406154
0x469F99: mov      edx, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x469F9F: mov      edx, dword ptr [edx]
0x469FA1: mov      dword ptr [edx + 0x75c], eax
0x469FA7: mov      eax, dword ptr [0x4f7ef8]
0x469FAC: mov      edx, dword ptr [ebp - 0x54]
0x469FAF: mov      dword ptr [eax + 0x58], edx
0x469FB2: mov      ax, word ptr [ebp - 0x4e]
0x469FB6: mov      word ptr [0x4f9338], ax
0x469FBC: mov      ax, word ptr [ebp - 0x4c]
0x469FC0: mov      word ptr [0x4f933a], ax
0x469FC6: mov      eax, 0x4f9308
0x469FCB: call     0x49f874
0x469FD0: mov      byte ptr [0x4f9308], 0
0x469FD7: jmp      0x46aeeb
0x469FDC: call     0x406154
0x469FE1: mov      edx, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x469FE7: mov      edx, dword ptr [edx]
0x469FE9: mov      dword ptr [edx + 0x75c], eax
0x469FEF: mov      eax, 0x4f9308
0x469FF4: call     0x49f874
0x469FF9: mov      byte ptr [0x4f9308], 0
0x46A000: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A005: mov      eax, dword ptr [eax]
0x46A007: mov      cx, word ptr [0x46afdc]
0x46A00E: mov      edx, 0x46b83c ; "你不能修理这个物品 ."
0x46A013: call     0x486df0
0x46A018: jmp      0x46aeeb
0x46A01D: call     0x406154
0x46A022: mov      edx, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A028: mov      edx, dword ptr [edx]
0x46A02A: mov      dword ptr [edx + 0x75c], eax
0x46A030: cmp      word ptr [ebp - 0x50], 0x2bd
0x46A036: je       0x46a07c
0x46A038: cmp      word ptr [ebp - 0x50], 0x2be
0x46A03E: jne      0x46a05a
0x46A040: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A045: mov      eax, dword ptr [eax]
0x46A047: mov      cx, word ptr [0x46afdc]
0x46A04E: mov      edx, 0x46b85c ; "你的个人包袱已经满了，你不能再寄存任何东西了。"
0x46A053: call     0x486df0
0x46A058: jmp      0x46a072
0x46A05A: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A05F: mov      eax, dword ptr [eax]
0x46A061: mov      cx, word ptr [0x46afdc]
0x46A068: mov      edx, 0x46b894 ; "你不能寄存。"
0x46A06D: call     0x486df0
0x46A072: mov      eax, 0x4f9308
0x46A077: call     0x49f874
0x46A07C: mov      byte ptr [0x4f9308], 0
0x46A083: jmp      0x46aeeb
0x46A088: mov      ecx, dword ptr [ebp - 0x10]
0x46A08B: mov      edx, dword ptr [ebp - 0x54]
0x46A08E: mov      eax, dword ptr [ebp - 4]
0x46A091: call     0x46db28
0x46A096: jmp      0x46aeeb
0x46A09B: call     0x406154
0x46A0A0: mov      edx, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A0A6: mov      edx, dword ptr [edx]
0x46A0A8: mov      dword ptr [edx + 0x75c], eax
0x46A0AE: cmp      word ptr [ebp - 0x50], 0x2c1
0x46A0B4: je       0x46a0f8
0x46A0B6: cmp      word ptr [ebp - 0x50], 0x2c3
0x46A0BC: jne      0x46a0db
0x46A0BE: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A0C3: mov      eax, dword ptr [eax]
0x46A0C5: mov      cx, word ptr [0x46afdc]
0x46A0CC: mov      edx, 0x46b8ac ; "你不能再携带更多东西了."
0x46A0D1: call     0x486df0
0x46A0D6: jmp      0x46aeeb
0x46A0DB: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A0E0: mov      eax, dword ptr [eax]
0x46A0E2: mov      cx, word ptr [0x46afdc]
0x46A0E9: mov      edx, 0x46b8cc ; "你不能取回。"
0x46A0EE: call     0x486df0
0x46A0F3: jmp      0x46aeeb
0x46A0F8: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A0FD: mov      eax, dword ptr [eax]
0x46A0FF: mov      edx, dword ptr [ebp - 0x54]
0x46A102: call     0x48fdfc
0x46A107: jmp      0x46aeeb
0x46A10C: call     0x406154
0x46A111: mov      edx, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A117: mov      edx, dword ptr [edx]
0x46A119: mov      dword ptr [edx + 0x75c], eax
0x46A11F: mov      eax, dword ptr [0x4f7ef8]
0x46A124: mov      edx, dword ptr [ebp - 0x54]
0x46A127: mov      dword ptr [eax + 0x58], edx
0x46A12A: mov      dx, word ptr [ebp - 0x4c]
0x46A12E: mov      ax, word ptr [ebp - 0x4e]
0x46A132: call     0x406964
0x46A137: mov      edx, eax
0x46A139: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A13E: mov      eax, dword ptr [eax]
0x46A140: call     0x48fd2c
0x46A145: jmp      0x46aeeb
0x46A14A: call     0x406154
0x46A14F: mov      edx, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A155: mov      edx, dword ptr [edx]
0x46A157: mov      dword ptr [edx + 0x75c], eax
0x46A15D: mov      eax, dword ptr [ebp - 0x54]
0x46A160: dec      eax
0x46A161: je       0x46a16e
0x46A163: dec      eax
0x46A164: je       0x46a18b
0x46A166: dec      eax
0x46A167: je       0x46a1a8
0x46A169: jmp      0x46aeeb
0x46A16E: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A173: mov      eax, dword ptr [eax]
0x46A175: mov      cx, word ptr [0x46afdc]
0x46A17C: mov      edx, 0x46b8e4 ; "物品被卖出。"
0x46A181: call     0x486df0
0x46A186: jmp      0x46aeeb
0x46A18B: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A190: mov      eax, dword ptr [eax]
0x46A192: mov      cx, word ptr [0x46afdc]
0x46A199: mov      edx, 0x46b8fc ; "没有更多物品可以携带了"
0x46A19E: call     0x486df0
0x46A1A3: jmp      0x46aeeb
0x46A1A8: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A1AD: mov      eax, dword ptr [eax]
0x46A1AF: mov      cx, word ptr [0x46afdc]
0x46A1B6: mov      edx, 0x46b91c ; "你没有足够的钱来购买物品."
0x46A1BB: call     0x486df0
0x46A1C0: jmp      0x46aeeb
0x46A1C5: call     0x406154
0x46A1CA: mov      edx, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A1D0: mov      edx, dword ptr [edx]
0x46A1D2: mov      dword ptr [edx + 0x75c], eax
0x46A1D8: mov      eax, dword ptr [0x4f7ef8]
0x46A1DD: mov      edx, dword ptr [ebp - 0x54]
0x46A1E0: mov      dword ptr [eax + 0x58], edx
0x46A1E3: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A1E8: mov      eax, dword ptr [eax]
0x46A1EA: mov      cx, word ptr [0x46afdc]
0x46A1F1: mov      edx, 0x46b940 ; "物品已经被正确打造。"
0x46A1F6: call     0x486df0
0x46A1FB: jmp      0x46aeeb
0x46A200: call     0x406154
0x46A205: mov      edx, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A20B: mov      edx, dword ptr [edx]
0x46A20D: mov      dword ptr [edx + 0x75c], eax
0x46A213: mov      eax, dword ptr [ebp - 0x54]
0x46A216: dec      eax
0x46A217: je       0x46a227
0x46A219: dec      eax
0x46A21A: je       0x46a244
0x46A21C: dec      eax
0x46A21D: je       0x46a261
0x46A21F: dec      eax
0x46A220: je       0x46a27e
0x46A222: jmp      0x46aeeb
0x46A227: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A22C: mov      eax, dword ptr [eax]
0x46A22E: mov      cx, word ptr [0x46afdc]
0x46A235: mov      edx, 0x46b960 ; "发生了错误"
0x46A23A: call     0x486df0
0x46A23F: jmp      0x46aeeb
0x46A244: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A249: mov      eax, dword ptr [eax]
0x46A24B: mov      cx, word ptr [0x46afdc]
0x46A252: mov      edx, 0x46b8fc ; "没有更多物品可以携带了"
0x46A257: call     0x486df0
0x46A25C: jmp      0x46aeeb
0x46A261: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A266: mov      eax, dword ptr [eax]
0x46A268: mov      cx, word ptr [0x46afdc]
0x46A26F: mov      edx, 0x46b974 ; "金币不足."
0x46A274: call     0x486df0
0x46A279: jmp      0x46aeeb
0x46A27E: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A283: mov      eax, dword ptr [eax]
0x46A285: mov      cx, word ptr [0x46afdc]
0x46A28C: mov      edx, 0x46b988 ; "你缺乏所必需的物品。"
0x46A291: call     0x486df0
0x46A296: jmp      0x46aeeb
0x46A29B: movzx    eax, word ptr [ebp - 0x4c]
0x46A29F: push     eax
0x46A2A0: mov      eax, dword ptr [ebp - 0x10]
0x46A2A3: push     eax
0x46A2A4: movzx    ecx, word ptr [ebp - 0x4e]
0x46A2A8: mov      edx, dword ptr [ebp - 0x54]
0x46A2AB: mov      eax, dword ptr [ebp - 4]
0x46A2AE: call     0x46dd2c
0x46A2B3: jmp      0x46aeeb
0x46A2B8: inc      dword ptr [0x4f9534]
0x46A2BE: jmp      0x46aeeb
0x46A2C3: mov      edx, dword ptr [ebp - 0x10]
0x46A2C6: mov      eax, dword ptr [ebp - 4]
0x46A2C9: call     0x46df14
0x46A2CE: jmp      0x46aeeb
0x46A2D3: cmp      word ptr [ebp - 0x4e], 0
0x46A2D8: jbe      0x46a2e3
0x46A2DA: mov      byte ptr [0x4f94f8], 1
0x46A2E1: jmp      0x46a2ea
0x46A2E3: mov      byte ptr [0x4f94f8], 0
0x46A2EA: call     0x406154
0x46A2EF: mov      dword ptr [0x4f94e8], eax
0x46A2F4: jmp      0x46aeeb
0x46A2F9: call     0x406154
0x46A2FE: mov      dword ptr [0x4f94e8], eax
0x46A303: mov      byte ptr [0x4f94f8], 1
0x46A30A: jmp      0x46aeeb
0x46A30F: call     0x406154
0x46A314: mov      dword ptr [0x4f94e8], eax
0x46A319: mov      eax, dword ptr [ebp - 0x54]
0x46A31C: sub      eax, -4
0x46A31F: je       0x46a385
0x46A321: dec      eax
0x46A322: je       0x46a368
0x46A324: dec      eax
0x46A325: je       0x46a34b
0x46A327: dec      eax
0x46A328: jne      0x46aeeb
0x46A32E: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A333: mov      eax, dword ptr [eax]
0x46A335: mov      cx, word ptr [0x46afdc]
0x46A33C: mov      edx, 0x46b9a8 ; "已经加入编组."
0x46A341: call     0x486df0
0x46A346: jmp      0x46aeeb
0x46A34B: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A350: mov      eax, dword ptr [eax]
0x46A352: mov      cx, word ptr [0x46afdc]
0x46A359: mov      edx, 0x46b9c0 ; "这个被加进编组的名字是不正确的。"
0x46A35E: call     0x486df0
0x46A363: jmp      0x46aeeb
0x46A368: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A36D: mov      eax, dword ptr [eax]
0x46A36F: mov      cx, word ptr [0x46afdc]
0x46A376: mov      edx, 0x46b9ec ; "你想邀请加入编组的这个用户已经是其他组的成员了。"
0x46A37B: call     0x486df0
0x46A380: jmp      0x46aeeb
0x46A385: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A38A: mov      eax, dword ptr [eax]
0x46A38C: mov      cx, word ptr [0x46afdc]
0x46A393: mov      edx, 0x46ba28 ; "对方不允许编组。"
0x46A398: call     0x486df0
0x46A39D: jmp      0x46aeeb
0x46A3A2: call     0x406154
0x46A3A7: mov      dword ptr [0x4f94e8], eax
0x46A3AC: jmp      0x46aeeb
0x46A3B1: call     0x406154
0x46A3B6: mov      dword ptr [0x4f94e8], eax
0x46A3BB: mov      eax, dword ptr [ebp - 0x54]
0x46A3BE: add      eax, 5
0x46A3C1: cmp      eax, 4
0x46A3C4: ja       0x46aeeb
0x46A3CA: jmp      dword ptr [eax*4 + 0x46a3d1]
0x46A3D1: pop      ecx
0x46A3D2: movsb    byte ptr es:[edi], byte ptr [esi]
0x46A3D3: inc      esi
0x46A3D4: add      byte ptr [esp], bh
0x46A3D7: inc      esi
0x46A3D8: add      byte ptr [edi], bl
0x46A3DA: movsb    byte ptr es:[edi], byte ptr [esi]
0x46A3DB: inc      esi
0x46A3DC: add      byte ptr [edx], al
0x46A3DE: movsb    byte ptr es:[edi], byte ptr [esi]
0x46A3DF: inc      esi
0x46A3E0: add      ch, ah
0x46A3E2: mov      dword ptr [0x10a10046], eax
0x46A3E7: inc      ebx
0x46A3E8: dec      edx
0x46A3E9: add      byte ptr [ebx + 0xd8b6600], cl
0x46A3EF: fsubr    qword ptr [edi + 0x44ba0046]
0x46A3F5: mov      edx, 0xf3e80046
0x46A3FA: leave    
0x46A3FB: add      dword ptr [eax], eax
0x46A3FD: jmp      0x46aeeb
0x46A402: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A407: mov      eax, dword ptr [eax]
0x46A409: mov      cx, word ptr [0x46afdc]
0x46A410: mov      edx, 0x46b9c0 ; "这个被加进编组的名字是不正确的。"
0x46A415: call     0x486df0
0x46A41A: jmp      0x46aeeb
0x46A41F: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A424: mov      eax, dword ptr [eax]
0x46A426: mov      cx, word ptr [0x46afdc]
0x46A42D: mov      edx, 0x46ba70 ; "已经加入编组。"
0x46A432: call     0x486df0
0x46A437: jmp      0x46aeeb
0x46A43C: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A441: mov      eax, dword ptr [eax]
0x46A443: mov      cx, word ptr [0x46afdc]
0x46A44A: mov      edx, 0x46ba28 ; "对方不允许编组。"
0x46A44F: call     0x486df0
0x46A454: jmp      0x46aeeb
0x46A459: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A45E: mov      eax, dword ptr [eax]
0x46A460: mov      cx, word ptr [0x46afdc]
0x46A467: mov      edx, 0x46ba88 ; "成员上限已经达到。"
0x46A46C: call     0x486df0
0x46A471: jmp      0x46aeeb
0x46A476: call     0x406154
0x46A47B: mov      dword ptr [0x4f94e8], eax
0x46A480: jmp      0x46aeeb
0x46A485: call     0x406154
0x46A48A: mov      dword ptr [0x4f94e8], eax
0x46A48F: mov      eax, dword ptr [ebp - 0x54]
0x46A492: sub      eax, -3
0x46A495: je       0x46a4db
0x46A497: dec      eax
0x46A498: je       0x46a4be
0x46A49A: dec      eax
0x46A49B: jne      0x46aeeb
0x46A4A1: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A4A6: mov      eax, dword ptr [eax]
0x46A4A8: mov      cx, word ptr [0x46afdc]
0x46A4AF: mov      edx, 0x46ba44 ; "编组还未成立或者你还不够等级创建。"
0x46A4B4: call     0x486df0
0x46A4B9: jmp      0x46aeeb
0x46A4BE: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A4C3: mov      eax, dword ptr [eax]
0x46A4C5: mov      cx, word ptr [0x46afdc]
0x46A4CC: mov      edx, 0x46b9c0 ; "这个被加进编组的名字是不正确的。"
0x46A4D1: call     0x486df0
0x46A4D6: jmp      0x46aeeb
0x46A4DB: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A4E0: mov      eax, dword ptr [eax]
0x46A4E2: mov      cx, word ptr [0x46afdc]
0x46A4E9: mov      edx, 0x46baa4 ; "你仍旧不在编组中。"
0x46A4EE: call     0x486df0
0x46A4F3: jmp      0x46aeeb
0x46A4F8: mov      eax, dword ptr [0x4f94fc]
0x46A4FD: mov      edx, dword ptr [eax]
0x46A4FF: call     dword ptr [edx + 0x40]
0x46A502: jmp      0x46aeeb
0x46A507: lea      edx, [ebp - 0x8c]
0x46A50D: mov      eax, dword ptr [ebp - 0x10]
0x46A510: call     0x45b2e0
0x46A515: mov      edx, dword ptr [ebp - 0x8c]
0x46A51B: mov      eax, dword ptr [ebp - 4]
0x46A51E: call     0x46e020
0x46A523: jmp      0x46aeeb
0x46A528: call     0x406154
0x46A52D: mov      dword ptr [0x4f94f0], eax
0x46A532: mov      edx, dword ptr [ebp - 0x10]
0x46A535: mov      eax, dword ptr [ebp - 4]
0x46A538: call     0x46e0b8
0x46A53D: jmp      0x46aeeb
0x46A542: call     0x406154
0x46A547: mov      dword ptr [0x4f94f0], eax
0x46A54C: mov      edx, dword ptr [ebp - 0x10]
0x46A54F: mov      eax, dword ptr [ebp - 4]
0x46A552: call     0x46e560
0x46A557: jmp      0x46aeeb
0x46A55C: call     0x406154
0x46A561: mov      dword ptr [0x4f94f0], eax
0x46A566: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A56B: mov      eax, dword ptr [eax]
0x46A56D: mov      cx, word ptr [0x46afdc]
0x46A574: mov      edx, 0x46bac0 ; "你仍旧没有加入行会"
0x46A579: call     0x486df0
0x46A57E: jmp      0x46aeeb
0x46A583: call     0x406154
0x46A588: mov      dword ptr [0x4f94f0], eax
0x46A58D: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A592: mov      eax, dword ptr [eax]
0x46A594: mov      cx, word ptr [0x46afdc]
0x46A59B: mov      edx, 0x46badc ; "交易被取消。\要正确交易你必须和对方面对面。"
0x46A5A0: call     0x486df0
0x46A5A5: jmp      0x46aeeb
0x46A5AA: call     0x406154
0x46A5AF: mov      dword ptr [0x4f94f0], eax
0x46A5B4: lea      edx, [ebp - 0x8c]
0x46A5BA: mov      eax, dword ptr [ebp - 0x10]
0x46A5BD: call     0x45b2e0
0x46A5C2: mov      edx, dword ptr [ebp - 0x8c]
0x46A5C8: mov      eax, 0x4f918c
0x46A5CD: call     0x403aac
0x46A5D2: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A5D7: mov      eax, dword ptr [eax]
0x46A5D9: call     0x4915ec
0x46A5DE: jmp      0x46aeeb
0x46A5E3: call     0x49fe38
0x46A5E8: cmp      byte ptr [0x4f933c], 0
0x46A5EF: je       0x46a602
0x46A5F1: mov      eax, 0x4f933c
0x46A5F6: call     0x49f874
0x46A5FB: mov      byte ptr [0x4f933c], 0
0x46A602: cmp      dword ptr [0x4f9180], 0
0x46A609: jle      0x46a620
0x46A60B: mov      eax, dword ptr [0x4f7ef8]
0x46A610: mov      edx, dword ptr [0x4f9180]
0x46A616: add      dword ptr [eax + 0x58], edx
0x46A619: xor      eax, eax
0x46A61B: mov      dword ptr [0x4f9180], eax
0x46A620: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A625: mov      eax, dword ptr [eax]
0x46A627: call     0x4916e0
0x46A62C: jmp      0x46aeeb
0x46A631: call     0x406154
0x46A636: mov      dword ptr [0x4f94ec], eax
0x46A63B: cmp      byte ptr [0x4f933c], 0
0x46A642: je       0x46aeeb
0x46A648: mov      eax, 0x4f933c
0x46A64D: call     0x49fde4
0x46A652: mov      byte ptr [0x4f933c], 0
0x46A659: jmp      0x46aeeb
0x46A65E: call     0x406154
0x46A663: mov      dword ptr [0x4f94ec], eax
0x46A668: cmp      byte ptr [0x4f933c], 0
0x46A66F: je       0x46aeeb
0x46A675: mov      eax, 0x4f933c
0x46A67A: call     0x49f874
0x46A67F: mov      byte ptr [0x4f933c], 0
0x46A686: jmp      0x46aeeb
0x46A68B: call     0x406154
0x46A690: mov      dword ptr [0x4f94ec], eax
0x46A695: cmp      byte ptr [0x4f933c], 0
0x46A69C: je       0x46aeeb
0x46A6A2: mov      byte ptr [0x4f933c], 0
0x46A6A9: jmp      0x46aeeb
0x46A6AE: call     0x406154
0x46A6B3: mov      dword ptr [0x4f94ec], eax
0x46A6B8: cmp      byte ptr [0x4f933c], 0
0x46A6BF: je       0x46aeeb
0x46A6C5: lea      eax, [ebp - 0x8c]
0x46A6CB: mov      edx, 0x4f933c
0x46A6D0: call     0x403c78
0x46A6D5: mov      eax, dword ptr [ebp - 0x8c]
0x46A6DB: mov      edx, dword ptr [0x4f9368]
0x46A6E1: call     0x49f9e4
0x46A6E6: mov      eax, 0x4f933c
0x46A6EB: call     0x49fde4
0x46A6F0: mov      eax, dword ptr [0x4f93d0]
0x46A6F5: cmp      eax, dword ptr [0x4f9368]
0x46A6FB: jne      0x46a721
0x46A6FD: mov      eax, 0x4f93a4
0x46A702: mov      edx, 0x4f933c
0x46A707: xor      ecx, ecx
0x46A709: mov      cl, byte ptr [eax]
0x46A70B: inc      ecx
0x46A70C: call     0x402ae4
0x46A711: jne      0x46a721
0x46A713: mov      byte ptr [0x4f939c], 0
0x46A71A: mov      byte ptr [0x4f93a4], 0
0x46A721: mov      byte ptr [0x4f933c], 0
0x46A728: jmp      0x46aeeb
0x46A72D: mov      edx, dword ptr [ebp - 0x10]
0x46A730: mov      eax, dword ptr [ebp - 4]
0x46A733: call     0x46e988
0x46A738: mov      eax, 0x7d
0x46A73D: call     0x495794
0x46A742: jmp      0x46aeeb
0x46A747: mov      edx, dword ptr [ebp - 0x10]
0x46A74A: mov      eax, dword ptr [ebp - 4]
0x46A74D: call     0x46e9ec
0x46A752: mov      eax, 0x7e
0x46A757: call     0x495794
0x46A75C: jmp      0x46aeeb
0x46A761: mov      eax, dword ptr [ebp - 0x54]
0x46A764: mov      dword ptr [0x4f9180], eax
0x46A769: mov      dx, word ptr [ebp - 0x4c]
0x46A76D: mov      ax, word ptr [ebp - 0x4e]
0x46A771: call     0x406964
0x46A776: mov      edx, dword ptr [0x4f7ef8]
0x46A77C: mov      dword ptr [edx + 0x58], eax
0x46A77F: call     0x406154
0x46A784: mov      dword ptr [0x4f94ec], eax
0x46A789: jmp      0x46aeeb
0x46A78E: mov      eax, dword ptr [ebp - 0x54]
0x46A791: mov      dword ptr [0x4f9180], eax
0x46A796: mov      dx, word ptr [ebp - 0x4c]
0x46A79A: mov      ax, word ptr [ebp - 0x4e]
0x46A79E: call     0x406964
0x46A7A3: mov      edx, dword ptr [0x4f7ef8]
0x46A7A9: mov      dword ptr [edx + 0x58], eax
0x46A7AC: call     0x406154
0x46A7B1: mov      dword ptr [0x4f94ec], eax
0x46A7B6: jmp      0x46aeeb
0x46A7BB: mov      eax, dword ptr [ebp - 0x54]
0x46A7BE: mov      dword ptr [0x4f9184], eax
0x46A7C3: mov      eax, 0x6a
0x46A7C8: call     0x495794
0x46A7CD: jmp      0x46aeeb
0x46A7D2: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A7D7: mov      eax, dword ptr [eax]
0x46A7D9: call     0x4916e0
0x46A7DE: jmp      0x46aeeb
0x46A7E3: mov      edx, dword ptr [ebp - 0x54]
0x46A7E6: mov      eax, dword ptr [ebp - 4]
0x46A7E9: call     0x46dae8
0x46A7EE: jmp      0x46aeeb
0x46A7F3: call     0x406154
0x46A7F8: mov      dword ptr [0x4f94f0], eax
0x46A7FD: movzx    edx, word ptr [ebp - 0x4e]
0x46A801: mov      eax, dword ptr [ebp - 4]
0x46A804: call     0x46ea50
0x46A809: jmp      0x46aeeb
0x46A80E: call     0x406154
0x46A813: mov      dword ptr [0x4f94f0], eax
0x46A818: push     0xff
0x46A81D: mov      ecx, 0xffffff
0x46A822: mov      edx, 0x46bb10 ; "没有可用的地图"
0x46A827: mov      eax, dword ptr [0x4f7d8c]
0x46A82C: call     0x479ae0
0x46A831: jmp      0x46aeeb
0x46A836: lea      edx, [ebp - 0x8c]
0x46A83C: mov      eax, dword ptr [ebp - 0x10]
0x46A83F: call     0x45b2e0
0x46A844: mov      edx, dword ptr [ebp - 0x8c]
0x46A84A: mov      eax, dword ptr [ebp - 4]
0x46A84D: call     0x46ea8c
0x46A852: jmp      0x46aeeb
0x46A857: mov      edx, dword ptr [ebp - 0x10]
0x46A85A: mov      eax, dword ptr [ebp - 4]
0x46A85D: call     0x46eb18
0x46A862: jmp      0x46aeeb
0x46A867: mov      eax, dword ptr [ebp - 4]
0x46A86A: call     0x466b80
0x46A86F: jmp      0x46aeeb
0x46A874: mov      eax, dword ptr [ebp - 0x54]
0x46A877: cmp      eax, 5
0x46A87A: ja       0x46aeeb
0x46A880: jmp      dword ptr [eax*4 + 0x46a887]
0x46A887: jmp      0x46a837
0x46A889: inc      esi
0x46A88A: add      byte ptr [edi - 0x43ffb958], bl
0x46A890: test     al, 0x46
0x46A892: add      cl, bl
0x46A894: test     al, 0x46
0x46A896: add      dh, dh
0x46A898: test     al, 0x46
0x46A89A: add      byte ptr [ebx], dl
0x46A89C: test     eax, 0x10a10046
0x46A8A1: inc      ebx
0x46A8A2: dec      edx
0x46A8A3: add      byte ptr [ebx + 0xd8b6600], cl
0x46A8A9: fsubr    qword ptr [edi + 0x28ba0046]
0x46A8AF: mov      ebx, 0x39e80046
0x46A8B4: lds      eax, ptr [ecx]
0x46A8B6: add      cl, ch
0x46A8B8: das      
0x46A8B9: push     es
0x46A8BA: add      byte ptr [eax], al
0x46A8BC: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A8C1: mov      eax, dword ptr [eax]
0x46A8C3: mov      cx, word ptr [0x46afdc]
0x46A8CA: mov      edx, 0x46bb4c ; "想加入进来的成员应该来面对掌门人。"
0x46A8CF: call     0x486df0
0x46A8D4: jmp      0x46aeeb
0x46A8D9: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A8DE: mov      eax, dword ptr [eax]
0x46A8E0: mov      cx, word ptr [0x46afdc]
0x46A8E7: mov      edx, 0x46bb78 ; "他已经加入我们的行会。"
0x46A8EC: call     0x486df0
0x46A8F1: jmp      0x46aeeb
0x46A8F6: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A8FB: mov      eax, dword ptr [eax]
0x46A8FD: mov      cx, word ptr [0x46afdc]
0x46A904: mov      edx, 0x46bb98 ; "已经加入其他行会。"
0x46A909: call     0x486df0
0x46A90E: jmp      0x46aeeb
0x46A913: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A918: mov      eax, dword ptr [eax]
0x46A91A: mov      cx, word ptr [0x46afdc]
0x46A921: mov      edx, 0x46bbb4 ; "对方不允许加入行会。"
0x46A926: call     0x486df0
0x46A92B: jmp      0x46aeeb
0x46A930: mov      eax, dword ptr [ebp - 4]
0x46A933: call     0x466b80
0x46A938: jmp      0x46aeeb
0x46A93D: mov      eax, dword ptr [ebp - 0x54]
0x46A940: dec      eax
0x46A941: je       0x46a951
0x46A943: dec      eax
0x46A944: je       0x46a96e
0x46A946: dec      eax
0x46A947: je       0x46a98b
0x46A949: dec      eax
0x46A94A: je       0x46a9a8
0x46A94C: jmp      0x46aeeb
0x46A951: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A956: mov      eax, dword ptr [eax]
0x46A958: mov      cx, word ptr [0x46afdc]
0x46A95F: mov      edx, 0x46bb28 ; "你没有权利使用这个命令。."
0x46A964: call     0x486df0
0x46A969: jmp      0x46aeeb
0x46A96E: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A973: mov      eax, dword ptr [eax]
0x46A975: mov      cx, word ptr [0x46afdc]
0x46A97C: mov      edx, 0x46bbd4 ; " 不是我们的行会成员。"
0x46A981: call     0x486df0
0x46A986: jmp      0x46aeeb
0x46A98B: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A990: mov      eax, dword ptr [eax]
0x46A992: mov      cx, word ptr [0x46afdc]
0x46A999: mov      edx, 0x46bbf4 ; "行会掌门人不能撤消他自己。"
0x46A99E: call     0x486df0
0x46A9A3: jmp      0x46aeeb
0x46A9A8: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46A9AD: mov      eax, dword ptr [eax]
0x46A9AF: mov      cx, word ptr [0x46afdc]
0x46A9B6: mov      edx, 0x46bc18 ; "不能使用命令。"
0x46A9BB: call     0x486df0
0x46A9C0: jmp      0x46aeeb
0x46A9C5: mov      eax, dword ptr [ebp - 0x54]
0x46A9C8: add      eax, 7
0x46A9CB: cmp      eax, 5
0x46A9CE: ja       0x46aeeb
0x46A9D4: jmp      dword ptr [eax*4 + 0x46a9db]
0x46A9DB: test     byte ptr [edx - 0x5598ffba], ch
0x46A9E1: inc      esi
0x46A9E2: add      byte ptr [edx - 0x56], cl
0x46A9E5: inc      esi
0x46A9E6: add      byte ptr [0x100046aa], ch
0x46A9EC: stosb    byte ptr es:[edi], al
0x46A9ED: inc      esi
0x46A9EE: add      bl, dh
0x46A9F0: test     eax, 0x10a10046
0x46A9F5: inc      ebx
0x46A9F6: dec      edx
0x46A9F7: add      byte ptr [ebx + 0xd8b6600], cl
0x46A9FD: fsubr    qword ptr [edi + 0x30ba0046]
0x46AA03: mov      esp, 0xe5e80046
0x46AA08: ret      
0x46AA09: add      dword ptr [eax], eax
0x46AA0B: jmp      0x46aeeb
0x46AA10: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46AA15: mov      eax, dword ptr [eax]
0x46AA17: mov      cx, word ptr [0x46afdc]
0x46AA1E: mov      edx, 0x46bc58 ; "[任命错误] 掌门人的职位是空的。"
0x46AA23: call     0x486df0
0x46AA28: jmp      0x46aeeb
0x46AA2D: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46AA32: mov      eax, dword ptr [eax]
0x46AA34: mov      cx, word ptr [0x46afdc]
0x46AA3B: mov      edx, 0x46bc80 ; "[任命错误]一个行会只能有两个掌门人"
0x46AA40: call     0x486df0
0x46AA45: jmp      0x46aeeb
0x46AA4A: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46AA4F: mov      eax, dword ptr [eax]
0x46AA51: mov      cx, word ptr [0x46afdc]
0x46AA58: mov      edx, 0x46bcac ; "[任命错误] 新的行会掌门人已经被传位。"
0x46AA5D: call     0x486df0
0x46AA62: jmp      0x46aeeb
0x46AA67: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46AA6C: mov      eax, dword ptr [eax]
0x46AA6E: mov      cx, word ptr [0x46afdc]
0x46AA75: mov      edx, 0x46bcdc ; "[任命错误] 不能添加成员/删除成员"
0x46AA7A: call     0x486df0
0x46AA7F: jmp      0x46aeeb
0x46AA84: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46AA89: mov      eax, dword ptr [eax]
0x46AA8B: mov      cx, word ptr [0x46afdc]
0x46AA92: mov      edx, 0x46bd08 ; "[任命错误] 职位重复或者出错。"
0x46AA97: call     0x486df0
0x46AA9C: jmp      0x46aeeb
0x46AAA1: mov      eax, dword ptr [ebp - 0x54]
0x46AAA4: sub      eax, -4
0x46AAA7: je       0x46ab0d
0x46AAA9: dec      eax
0x46AAAA: je       0x46aaf0
0x46AAAC: dec      eax
0x46AAAD: je       0x46aad3
0x46AAAF: dec      eax
0x46AAB0: jne      0x46aeeb
0x46AAB6: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46AABB: mov      eax, dword ptr [eax]
0x46AABD: mov      cx, word ptr [0x46afdc]
0x46AAC4: mov      edx, 0x46bd30 ; "你没有权利。"
0x46AAC9: call     0x486df0
0x46AACE: jmp      0x46aeeb
0x46AAD3: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46AAD8: mov      eax, dword ptr [eax]
0x46AADA: mov      cx, word ptr [0x46afdc]
0x46AAE1: mov      edx, 0x46bd48 ; "联盟失败。"
0x46AAE6: call     0x486df0
0x46AAEB: jmp      0x46aeeb
0x46AAF0: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46AAF5: mov      eax, dword ptr [eax]
0x46AAF7: mov      cx, word ptr [0x46afdc]
0x46AAFE: mov      edx, 0x46bd5c ; "你应该面对你想要结盟的行会掌门人。"
0x46AB03: call     0x486df0
0x46AB08: jmp      0x46aeeb
0x46AB0D: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46AB12: mov      eax, dword ptr [eax]
0x46AB14: mov      cx, word ptr [0x46afdc]
0x46AB1B: mov      edx, 0x46bd88 ; "对方行会掌门人不允许结盟。"
0x46AB20: call     0x486df0
0x46AB25: jmp      0x46aeeb
0x46AB2A: mov      eax, dword ptr [ebp - 0x54]
0x46AB2D: sub      eax, -3
0x46AB30: je       0x46ab76
0x46AB32: dec      eax
0x46AB33: je       0x46ab59
0x46AB35: dec      eax
0x46AB36: jne      0x46aeeb
0x46AB3C: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46AB41: mov      eax, dword ptr [eax]
0x46AB43: mov      cx, word ptr [0x46afdc]
0x46AB4A: mov      edx, 0x46bd30 ; "你没有权利。"
0x46AB4F: call     0x486df0
0x46AB54: jmp      0x46aeeb
0x46AB59: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46AB5E: mov      eax, dword ptr [eax]
0x46AB60: mov      cx, word ptr [0x46afdc]
0x46AB67: mov      edx, 0x46bdac ; "你不在行会联盟中。"
0x46AB6C: call     0x486df0
0x46AB71: jmp      0x46aeeb
0x46AB76: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46AB7B: mov      eax, dword ptr [eax]
0x46AB7D: mov      cx, word ptr [0x46afdc]
0x46AB84: mov      edx, 0x46bdc8 ; "这不是一个现有的行会。"
0x46AB89: call     0x486df0
0x46AB8E: jmp      0x46aeeb
0x46AB93: call     0x406154
0x46AB98: mov      edx, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46AB9E: mov      edx, dword ptr [edx]
0x46ABA0: mov      dword ptr [edx + 0x75c], eax
0x46ABA6: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46ABAB: mov      eax, dword ptr [eax]
0x46ABAD: mov      cx, word ptr [0x46afdc]
0x46ABB4: mov      edx, 0x46bde8 ; "行会正确建立。"
0x46ABB9: call     0x486df0
0x46ABBE: jmp      0x46aeeb
0x46ABC3: call     0x406154
0x46ABC8: mov      edx, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46ABCE: mov      edx, dword ptr [edx]
0x46ABD0: mov      dword ptr [edx + 0x75c], eax
0x46ABD6: mov      eax, dword ptr [ebp - 0x54]
0x46ABD9: sub      eax, -3
0x46ABDC: je       0x46ac22
0x46ABDE: dec      eax
0x46ABDF: je       0x46ac05
0x46ABE1: dec      eax
0x46ABE2: jne      0x46aeeb
0x46ABE8: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46ABED: mov      eax, dword ptr [eax]
0x46ABEF: mov      cx, word ptr [0x46afdc]
0x46ABF6: mov      edx, 0x46be00 ; "已经加入行会。"
0x46ABFB: call     0x486df0
0x46AC00: jmp      0x46aeeb
0x46AC05: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46AC0A: mov      eax, dword ptr [eax]
0x46AC0C: mov      cx, word ptr [0x46afdc]
0x46AC13: mov      edx, 0x46be18 ; "缺少创建费用。"
0x46AC18: call     0x486df0
0x46AC1D: jmp      0x46aeeb
0x46AC22: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46AC27: mov      eax, dword ptr [eax]
0x46AC29: mov      cx, word ptr [0x46afdc]
0x46AC30: mov      edx, 0x46be30 ; "你没有准备好需要的全部物品。"
0x46AC35: call     0x486df0
0x46AC3A: jmp      0x46aeeb
0x46AC3F: call     0x406154
0x46AC44: mov      edx, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46AC4A: mov      edx, dword ptr [edx]
0x46AC4C: mov      dword ptr [edx + 0x75c], eax
0x46AC52: cmp      dword ptr [ebp - 0x10], 0
0x46AC56: je       0x46aeeb
0x46AC5C: lea      edx, [ebp - 0x8c]
0x46AC62: mov      eax, dword ptr [ebp - 0x10]
0x46AC65: call     0x45b2e0
0x46AC6A: mov      edx, dword ptr [ebp - 0x8c]
0x46AC70: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46AC75: mov      eax, dword ptr [eax]
0x46AC77: mov      cx, word ptr [0x46afdc]
0x46AC7E: call     0x486df0
0x46AC83: jmp      0x46aeeb
0x46AC88: cmp      dword ptr [ebp - 0x10], 0
0x46AC8C: je       0x46aeeb
0x46AC92: lea      edx, [ebp - 0x8c]
0x46AC98: mov      eax, dword ptr [ebp - 0x10]
0x46AC9B: call     0x45b2e0
0x46ACA0: mov      edx, dword ptr [ebp - 0x8c]
0x46ACA6: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46ACAB: mov      eax, dword ptr [eax]
0x46ACAD: mov      cx, word ptr [0x46afdc]
0x46ACB4: call     0x486df0
0x46ACB9: jmp      0x46aeeb
0x46ACBE: call     0x406154
0x46ACC3: mov      edx, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46ACC9: mov      edx, dword ptr [edx]
0x46ACCB: mov      dword ptr [edx + 0x75c], eax
0x46ACD1: jmp      0x46aeeb
0x46ACD6: call     0x406154
0x46ACDB: mov      edx, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46ACE1: mov      edx, dword ptr [edx]
0x46ACE3: mov      dword ptr [edx + 0x75c], eax
0x46ACE9: jmp      0x46aeeb
0x46ACEE: mov      eax, dword ptr [0x4f7d9c]
0x46ACF3: mov      eax, dword ptr [eax + 0x5a850]
0x46ACF9: cmp      byte ptr [eax + 0x1f1], 0
0x46AD00: jne      0x46ad14
0x46AD02: mov      eax, dword ptr [0x4f7d9c]
0x46AD07: mov      eax, dword ptr [eax + 0x5a850]
0x46AD0D: mov      dl, 0x2a
0x46AD0F: call     0x421b94
0x46AD14: mov      byte ptr [0x4f9236], 1
0x46AD1B: jmp      0x46aeeb
0x46AD20: lea      eax, [ebp - 0x14]
0x46AD23: push     eax
0x46AD24: push     0x40355555
0x46AD29: push     0x55555555
0x46AD2E: call     0x458d88
0x46AD33: inc      eax
0x46AD34: push     eax
0x46AD35: mov      eax, dword ptr [ebp - 0x10]
0x46AD38: call     0x403cd4
0x46AD3D: mov      ecx, eax
0x46AD3F: mov      eax, dword ptr [ebp - 0x10]
0x46AD42: pop      edx
0x46AD43: call     0x403ed8
0x46AD48: lea      edx, [ebp - 0x64]
0x46AD4B: mov      ecx, 0x10
0x46AD50: mov      eax, dword ptr [ebp - 0x10]
0x46AD53: call     0x45b37c
0x46AD58: lea      edx, [ebp - 0x20]
0x46AD5B: mov      eax, dword ptr [ebp - 0x14]
0x46AD5E: call     0x45b2e0
0x46AD63: movzx    eax, word ptr [ebp - 0x4e]
0x46AD67: mov      edx, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46AD6D: mov      edx, dword ptr [edx]
0x46AD6F: mov      dword ptr [edx + 0x624], eax
0x46AD75: xor      eax, eax
0x46AD77: mov      al, byte ptr [ebp - 0x64]
0x46AD7A: mov      edx, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46AD80: mov      edx, dword ptr [edx]
0x46AD82: mov      dword ptr [edx + 0x62c], eax
0x46AD88: mov      ax, word ptr [ebp - 0x64]
0x46AD8C: call     0x406974
0x46AD91: and      eax, 0xff
0x46AD96: mov      edx, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46AD9C: mov      edx, dword ptr [edx]
0x46AD9E: mov      dword ptr [edx + 0x648], eax
0x46ADA4: mov      eax, dword ptr [ebp - 0x64]
0x46ADA7: call     0x406970
0x46ADAC: and      eax, 0xff
0x46ADB1: mov      edx, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46ADB7: mov      edx, dword ptr [edx]
0x46ADB9: mov      dword ptr [edx + 0x664], eax
0x46ADBF: mov      eax, dword ptr [ebp - 0x64]
0x46ADC2: call     0x406970
0x46ADC7: call     0x406974
0x46ADCC: and      eax, 0xff
0x46ADD1: mov      edx, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46ADD7: mov      edx, dword ptr [edx]
0x46ADD9: mov      dword ptr [edx + 0x680], eax
0x46ADDF: xor      eax, eax
0x46ADE1: mov      al, byte ptr [ebp - 0x60]
0x46ADE4: mov      edx, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46ADEA: mov      edx, dword ptr [edx]
0x46ADEC: mov      dword ptr [edx + 0x69c], eax
0x46ADF2: mov      ax, word ptr [ebp - 0x60]
0x46ADF6: call     0x406974
0x46ADFB: and      eax, 0xff
0x46AE00: mov      edx, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46AE06: mov      edx, dword ptr [edx]
0x46AE08: mov      dword ptr [edx + 0x6b8], eax
0x46AE0E: mov      eax, dword ptr [ebp - 0x60]
0x46AE11: call     0x406970
0x46AE16: and      eax, 0xff
0x46AE1B: mov      edx, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46AE21: mov      edx, dword ptr [edx]
0x46AE23: mov      dword ptr [edx + 0x6d4], eax
0x46AE29: mov      eax, dword ptr [ebp - 0x60]
0x46AE2C: call     0x406970
0x46AE31: call     0x406974
0x46AE36: and      eax, 0xff
0x46AE3B: mov      edx, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46AE41: mov      edx, dword ptr [edx]
0x46AE43: mov      dword ptr [edx + 0x6f0], eax
0x46AE49: xor      eax, eax
0x46AE4B: mov      al, byte ptr [ebp - 0x5c]
0x46AE4E: mov      edx, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46AE54: mov      edx, dword ptr [edx]
0x46AE56: mov      dword ptr [edx + 0x70c], eax
0x46AE5C: mov      ax, word ptr [ebp - 0x5c]
0x46AE60: call     0x406974
0x46AE65: and      eax, 0xff
0x46AE6A: mov      edx, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46AE70: mov      edx, dword ptr [edx]
0x46AE72: mov      dword ptr [edx + 0x728], eax
0x46AE78: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46AE7D: mov      eax, dword ptr [eax]
0x46AE7F: xor      edx, edx
0x46AE81: mov      dword ptr [eax + 0x620], edx
0x46AE87: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46AE8C: mov      eax, dword ptr [eax]
0x46AE8E: mov      cx, word ptr [0x46be50]
0x46AE95: xor      edx, edx
0x46AE97: call     0x486df0
0x46AE9C: mov      ecx, dword ptr [ebp - 0x20]
0x46AE9F: mov      edx, dword ptr [ebp - 0x54]
0x46AEA2: mov      eax, dword ptr [0x4f7d88]
0x46AEA7: call     0x465ca8
0x46AEAC: jmp      0x46aeeb
0x46AEAE: lea      edx, [ebp - 0x94]
0x46AEB4: movzx    eax, word ptr [ebp - 0x50]
0x46AEB8: call     0x407d64
0x46AEBD: push     dword ptr [ebp - 0x94]
0x46AEC3: push     0x46be5c ; " : "
0x46AEC8: push     dword ptr [ebp - 0x10]
0x46AECB: lea      eax, [ebp - 0x8c]
0x46AED1: mov      edx, 3
0x46AED6: call     0x403d94
0x46AEDB: mov      edx, dword ptr [ebp - 0x8c]
0x46AEE1: mov      eax, dword ptr [0x4f7d8c]
0x46AEE6: call     0x479a68
0x46AEEB: mov      edx, dword ptr [ebp - 8]
0x46AEEE: mov      eax, 0x46be68 ; "#"
0x46AEF3: call     0x403f60
0x46AEF8: test     eax, eax
0x46AEFA: jle      0x46af09
0x46AEFC: mov      edx, dword ptr [ebp - 8]
0x46AEFF: mov      eax, dword ptr [0x4f7d8c]
0x46AF04: call     0x479a68
0x46AF09: xor      eax, eax
0x46AF0B: pop      edx
0x46AF0C: pop      ecx
0x46AF0D: pop      ecx
0x46AF0E: mov      dword ptr fs:[eax], edx
0x46AF11: push     0x46af49
0x46AF16: lea      eax, [ebp - 0x94]
0x46AF1C: call     0x403a58
0x46AF21: lea      eax, [ebp - 0x8c]
0x46AF27: call     0x403a58
0x46AF2C: lea      eax, [ebp - 0x48]
0x46AF2F: call     0x403a58
0x46AF34: lea      eax, [ebp - 0x20]
0x46AF37: mov      edx, 7
0x46AF3C: call     0x403a7c
0x46AF41: ret      
0x46AF42: jmp      0x403518
0x46AF47: jmp      0x46af16
0x46AF49: pop      edi
0x46AF4A: pop      esi
0x46AF4B: pop      ebx
0x46AF4C: mov      esp, ebp
0x46AF4E: pop      ebp
0x46AF4F: ret      
0x46AF58: [STR len=3] "PWR"
0x46AF64: [STR len=3] "LNG"
0x46AF70: [STR len=4] "ULNG"
0x46AF80: [STR len=3] "WID"
0x46AF8C: [STR len=4] "UWID"
0x46AF9C: [STR len=3] "FIR"
0x46AFA8: [STR len=4] "UFIR"
0x46AFB8: [STR len=4] "GOOD"
0x46AFC8: [STR len=4] "FAIL"
0x46AFD8: [STR len=3] "DIG"
0x46AFDC: add      al, 0
0x46AFDE: add      byte ptr [eax], al
0x46AFE8: [STR len=170] "你的帐号已经建立了.\ 请妥善保管你的帐户和密码，\并且不要因任何原因把它们告诉任何其他人。\ 如果忘记了密码,\你可以通过我们的主页重新找回它。\(http://www.legendofmir.com.cn)"
0x46B09C: [STR len=1] """
0x46B0A8: [STR len=34] ""这个帐号正在被其他的玩家使用了。\"
0x46B0D4: [STR len=22] "请使用一个不同的名字。"
0x46B0F4: [STR len=47] "在本服务器下, 这个新名字禁止使用。\请联系我们。"
0x46B12C: [STR len=62] "建立ID失败，请确认它没有包含空格,\特殊字符，或难以辨认的字符。"
0x46B174: [STR len=9] "密码错误."
0x46B188: [STR len=50] "连续三次密码错误。\你将在一段时间内无法再次连接。."
0x46B1C4: [STR len=56] "这个帐号正在使用，或者是被异常的终止锁定了,\请稍后再试。"
0x46B208: [STR len=53] "这个帐户不能正确访问。\请改变帐户,\或者申请付费注册。"
0x46B248: [STR len=17] "这个帐户被禁止了."
0x46B264: [STR len=19] "ID不存在或未知错误."
0x46B280: [STR len=294] "你的帐户现在已经更新.\请妥善保管你的帐户和密码。\a并且不要因任何原因把它们告诉任何其他人。.\如果忘记了密码，你可以通过我们的主页重新找回它。\(http://www.legendofmir.com.cn)并且不要因任何原因把它们告诉任何其他人。\ 如果忘记了密码,\你可以通过我们的主页重新找回它。\(http://www.legendofmir.com.cn)"
0x46B3B0: [STR len=13] "更新帐户失败."
0x46B3C8: [STR len=20] "你的付费到今天为止。"
0x46B3E8: [STR len=19] "你的个人帐户的期限:"
0x46B404: [STR len=9] "剩余天数."
0x46B418: [STR len=36] "当前在用的IP的剩余时间将在今天结束。"
0x46B448: [STR len=14] "当前IP的周期  "
0x46B460: [STR len=9] "IP的周期 "
0x46B474: [STR len=10] "剩余 小时."
0x46B488: [STR len=14] "个人帐户的期限"
0x46B4A0: [STR len=31] "这个帐号不可用,服务器认正失败。"
0x46B4C8: [STR len=28] "[failure] 这个名字已经存在。"
0x46B4F0: [STR len=58] "[Failure] 你只能为一个帐户设两个角色.\请和游戏管理员联系。"
0x46B534: [STR len=31] "[failure] 角色建立失败. Error=4"
0x46B55C: [STR len=55] "[Failure] 未知的错误. 请写信给: support@legendofmir.net"
0x46B59C: [STR len=13] "密码变更成功."
0x46B5B4: [STR len=28] "密码错误。不能进行密码变更。"
0x46B5DC: [STR len=24] "帐户被锁定 .请稍后再试。"
0x46B600: [STR len=26] "密码少于4位,你不能改变它。"
0x46B624: [STR len=57] "[Failure] 删除角色失败, Email: support@legendofmir.com.cn"
0x46B668: [STR len=24] "您选择的服务器用户满员。"
0x46B68C: [STR len=58] "版本错误。请下载最新版本。 (http://www.legendofmir.com.cn)"
0x46B6D0: [STR len=16] "充值卡充值成功。"
0x46B6EC: [STR len=24] "你被通过固定数量IP连接。"
0x46B710: [STR len=24] "你被通过固定时间IP连接。"
0x46B734: [STR len=26] "你被通过固定时间帐户充值。"
0x46B758: [STR len=68] "服务器连接被强行中断。\连接时间可能超过限制，\或者用户请求重新连接。"
0x46B7A8: [STR len=12] " 经验值增加."
0x46B7C0: [STR len=5] "升级!"
0x46B7D0: [STR len=12] "门被锁住了。"
0x46B7E8: [STR len=10] " 金币增加."
0x46B7FC: [STR len=4] "金币"
0x46B80C: [STR len=8] "????金币"
0x46B820: [STR len=16] "你不能卖这个物品"
0x46B83C: [STR len=20] "你不能修理这个物品 ."
0x46B85C: [STR len=46] "你的个人包袱已经满了，你不能再寄存任何东西了。"
0x46B894: [STR len=12] "你不能寄存。"
0x46B8AC: [STR len=23] "你不能再携带更多东西了."
0x46B8CC: [STR len=12] "你不能取回。"
0x46B8E4: [STR len=12] "物品被卖出。"
0x46B8FC: [STR len=22] "没有更多物品可以携带了"
0x46B91C: [STR len=25] "你没有足够的钱来购买物品."
0x46B940: [STR len=20] "物品已经被正确打造。"
0x46B960: [STR len=10] "发生了错误"
0x46B974: [STR len=9] "金币不足."
0x46B988: [STR len=20] "你缺乏所必需的物品。"
0x46B9A8: [STR len=13] "已经加入编组."
0x46B9C0: [STR len=32] "这个被加进编组的名字是不正确的。"
0x46B9EC: [STR len=48] "你想邀请加入编组的这个用户已经是其他组的成员了。"
0x46BA28: [STR len=16] "对方不允许编组。"
0x46BA44: [STR len=34] "编组还未成立或者你还不够等级创建。"
0x46BA70: [STR len=14] "已经加入编组。"
0x46BA88: [STR len=18] "成员上限已经达到。"
0x46BAA4: [STR len=18] "你仍旧不在编组中。"
0x46BAC0: [STR len=18] "你仍旧没有加入行会"
0x46BADC: [STR len=43] "交易被取消。\要正确交易你必须和对方面对面。"
0x46BB10: [STR len=14] "没有可用的地图"
0x46BB28: [STR len=25] "你没有权利使用这个命令。."
0x46BB4C: [STR len=34] "想加入进来的成员应该来面对掌门人。"
0x46BB78: [STR len=22] "他已经加入我们的行会。"
0x46BB98: [STR len=18] "已经加入其他行会。"
0x46BBB4: [STR len=20] "对方不允许加入行会。"
0x46BBD4: [STR len=21] " 不是我们的行会成员。"
0x46BBF4: [STR len=26] "行会掌门人不能撤消他自己。"
0x46BC18: [STR len=14] "不能使用命令。"
0x46BC30: [STR len=30] "[任命错误]  掌门人位置是空的。"
0x46BC58: [STR len=31] "[任命错误] 掌门人的职位是空的。"
0x46BC80: [STR len=34] "[任命错误]一个行会只能有两个掌门人"
0x46BCAC: [STR len=37] "[任命错误] 新的行会掌门人已经被传位。"
0x46BCDC: [STR len=32] "[任命错误] 不能添加成员/删除成员"
0x46BD08: [STR len=29] "[任命错误] 职位重复或者出错。"
0x46BD30: [STR len=12] "你没有权利。"
0x46BD48: [STR len=10] "联盟失败。"
0x46BD5C: [STR len=34] "你应该面对你想要结盟的行会掌门人。"
0x46BD88: [STR len=26] "对方行会掌门人不允许结盟。"
0x46BDAC: [STR len=18] "你不在行会联盟中。"
0x46BDC8: [STR len=22] "这不是一个现有的行会。"
0x46BDE8: [STR len=14] "行会正确建立。"
0x46BE00: [STR len=14] "已经加入行会。"
0x46BE18: [STR len=14] "缺少创建费用。"
0x46BE30: [STR len=28] "你没有准备好需要的全部物品。"
0x46BE50: add      byte ptr [eax], al
0x46BE52: add      byte ptr [eax], al
0x46BE5C: [STR len=3] " : "
0x46BE68: [STR len=1] "#"
0x46BE6C: push     ebp
0x46BE6D: mov      ebp, esp
0x46BE6F: mov      ecx, 5
0x46BE74: push     0
0x46BE76: push     0
0x46BE78: dec      ecx
0x46BE79: jne      0x46be74
0x46BE7B: mov      dword ptr [ebp - 8], edx
0x46BE7E: mov      dword ptr [ebp - 4], eax
0x46BE81: mov      eax, dword ptr [ebp - 8]
0x46BE84: call     0x403e88
0x46BE89: xor      eax, eax
0x46BE8B: push     ebp
0x46BE8C: push     0x46c084
0x46BE91: push     dword ptr fs:[eax]
0x46BE94: mov      dword ptr fs:[eax], esp
0x46BE97: lea      edx, [ebp - 0xc]
0x46BE9A: mov      eax, dword ptr [ebp - 8]
0x46BE9D: call     0x45b2e0
0x46BEA2: push     0
0x46BEA4: lea      eax, [ebp - 0x24]
0x46BEA7: push     eax
0x46BEA8: mov      byte ptr [ebp - 0x28], 0x2f
0x46BEAC: lea      ecx, [ebp - 0x28]
0x46BEAF: lea      edx, [ebp - 0x10]
0x46BEB2: mov      eax, dword ptr [ebp - 0xc]
0x46BEB5: call     0x452530
0x46BEBA: mov      edx, dword ptr [ebp - 0x24]
0x46BEBD: lea      eax, [ebp - 0xc]
0x46BEC0: call     0x403af0
0x46BEC5: push     0
0x46BEC7: lea      eax, [ebp - 0x24]
0x46BECA: push     eax
0x46BECB: mov      byte ptr [ebp - 0x28], 0x2f
0x46BECF: lea      ecx, [ebp - 0x28]
0x46BED2: lea      edx, [ebp - 0x14]
0x46BED5: mov      eax, dword ptr [ebp - 0xc]
0x46BED8: call     0x452530
0x46BEDD: mov      edx, dword ptr [ebp - 0x24]
0x46BEE0: lea      eax, [ebp - 0xc]
0x46BEE3: call     0x403af0
0x46BEE8: push     0
0x46BEEA: lea      eax, [ebp - 0x24]
0x46BEED: push     eax
0x46BEEE: mov      byte ptr [ebp - 0x28], 0x2f
0x46BEF2: lea      ecx, [ebp - 0x28]
0x46BEF5: lea      edx, [ebp - 0x18]
0x46BEF8: mov      eax, dword ptr [ebp - 0xc]
0x46BEFB: call     0x452530
0x46BF00: mov      edx, dword ptr [ebp - 0x24]
0x46BF03: lea      eax, [ebp - 0xc]
0x46BF06: call     0x403af0
0x46BF0B: xor      edx, edx
0x46BF0D: mov      eax, dword ptr [ebp - 0x18]
0x46BF10: call     0x4520e8
0x46BF15: mov      edx, dword ptr [ebp - 4]
0x46BF18: mov      dword ptr [edx + 0x41c], eax
0x46BF1E: cmp      byte ptr [0x4f937c], 0
0x46BF25: jne      0x46bfb3
0x46BF2B: xor      edx, edx
0x46BF2D: mov      eax, dword ptr [ebp - 4]
0x46BF30: mov      eax, dword ptr [eax + 0x30c]
0x46BF36: call     0x45a950
0x46BF3B: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46BF40: mov      eax, dword ptr [eax]
0x46BF42: mov      eax, dword ptr [eax + 0x480]
0x46BF48: xor      edx, edx
0x46BF4A: call     0x458214
0x46BF4F: mov      eax, 0x1f4
0x46BF54: call     0x45ed98
0x46BF59: mov      byte ptr [0x4f937f], 1
0x46BF60: mov      eax, dword ptr [ebp - 4]
0x46BF63: mov      eax, dword ptr [eax + 0x30c]
0x46BF69: mov      dword ptr [ebp - 0x1c], eax
0x46BF6C: mov      eax, 0x4f94cc
0x46BF71: mov      edx, dword ptr [ebp - 0x10]
0x46BF74: call     0x403aac
0x46BF79: xor      edx, edx
0x46BF7B: mov      eax, dword ptr [ebp - 0x14]
0x46BF7E: call     0x4520e8
0x46BF83: mov      dword ptr [0x4f94d0], eax
0x46BF88: mov      edx, dword ptr [0x4f94cc]
0x46BF8E: mov      eax, dword ptr [ebp - 0x1c]
0x46BF91: call     0x45a9ec
0x46BF96: mov      edx, dword ptr [0x4f94d0]
0x46BF9C: mov      eax, dword ptr [ebp - 0x1c]
0x46BF9F: call     0x45ab2c
0x46BFA4: mov      dl, 1
0x46BFA6: mov      eax, dword ptr [ebp - 0x1c]
0x46BFA9: call     0x45a950
0x46BFAE: jmp      0x46c05c
0x46BFB3: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46BFB8: mov      eax, dword ptr [eax]
0x46BFBA: mov      eax, dword ptr [eax + 0x480]
0x46BFC0: xor      edx, edx
0x46BFC2: call     0x458214
0x46BFC7: mov      eax, 0x4f94cc
0x46BFCC: mov      edx, dword ptr [ebp - 0x10]
0x46BFCF: call     0x403aac
0x46BFD4: xor      edx, edx
0x46BFD6: mov      eax, dword ptr [ebp - 0x14]
0x46BFD9: call     0x4520e8
0x46BFDE: mov      dword ptr [0x4f94d0], eax
0x46BFE3: mov      eax, dword ptr [ebp - 4]
0x46BFE6: mov      eax, dword ptr [eax + 0x30c]
0x46BFEC: mov      eax, dword ptr [eax + 0x80]
0x46BFF2: cmp      byte ptr [eax + 8], 0
0x46BFF6: je       0x46c031
0x46BFF8: push     0x46c098 ; "$S"
0x46BFFD: push     dword ptr [ebp - 0x10]
0x46C000: push     0x46c0a4 ; "/"
0x46C005: push     dword ptr [ebp - 0x14]
0x46C008: push     0x46c0b0 ; "%"
0x46C00D: lea      eax, [ebp - 0x24]
0x46C010: mov      edx, 5
0x46C015: call     0x403d94
0x46C01A: mov      edx, dword ptr [ebp - 0x24]
0x46C01D: mov      eax, dword ptr [ebp - 4]
0x46C020: mov      eax, dword ptr [eax + 0x30c]
0x46C026: mov      eax, dword ptr [eax + 0x80]
0x46C02C: call     0x45a204
0x46C031: mov      eax, 0x1f4
0x46C036: call     0x45ed98
0x46C03B: mov      byte ptr [0x4f937f], 1
0x46C042: mov      eax, dword ptr [0x4f7d94]
0x46C047: call     0x47d69c
0x46C04C: mov      dl, 1
0x46C04E: mov      eax, dword ptr [ebp - 4]
0x46C051: mov      eax, dword ptr [eax + 0x338]
0x46C057: call     0x4455d4
0x46C05C: xor      eax, eax
0x46C05E: pop      edx
0x46C05F: pop      ecx
0x46C060: pop      ecx
0x46C061: mov      dword ptr fs:[eax], edx
0x46C064: push     0x46c08b
0x46C069: lea      eax, [ebp - 0x24]
0x46C06C: mov      edx, 2
0x46C071: call     0x403a7c
0x46C076: lea      eax, [ebp - 0x18]
0x46C079: mov      edx, 5
0x46C07E: call     0x403a7c
0x46C083: ret      
0x46C084: jmp      0x403518
0x46C089: jmp      0x46c069
0x46C08B: mov      esp, ebp
0x46C08D: pop      ebp
0x46C08E: ret      
0x46C08F: add      bh, bh
0x46C093: inc      dword ptr [edx]
0x46C095: add      byte ptr [eax], al
0x46C097: add      byte ptr [ebx + edx*2], ah
0x46C09A: add      byte ptr [eax], al
0x46C0A4: [STR len=1] "/"
0x46C0B0: [STR len=1] "%"
0x46C0B4: push     ebp
0x46C0B5: mov      ebp, esp
0x46C0B7: add      esp, -8
0x46C0BA: xor      edx, edx
0x46C0BC: mov      dword ptr [ebp - 8], edx
0x46C0BF: mov      dword ptr [ebp - 4], eax
0x46C0C2: xor      eax, eax
0x46C0C4: push     ebp
0x46C0C5: push     0x46c0fc
0x46C0CA: push     dword ptr fs:[eax]
0x46C0CD: mov      dword ptr fs:[eax], esp
0x46C0D0: mov      eax, dword ptr [0x4f7d94]
0x46C0D5: call     0x47d688
0x46C0DA: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46C0DF: mov      eax, dword ptr [eax]
0x46C0E1: call     0x487bd0
0x46C0E6: xor      eax, eax
0x46C0E8: pop      edx
0x46C0E9: pop      ecx
0x46C0EA: pop      ecx
0x46C0EB: mov      dword ptr fs:[eax], edx
0x46C0EE: push     0x46c103
0x46C0F3: lea      eax, [ebp - 8]
0x46C0F6: call     0x403a58
0x46C0FB: ret      
0x46C0FC: jmp      0x403518
0x46C101: jmp      0x46c0f3
0x46C103: pop      ecx
0x46C104: pop      ecx
0x46C105: pop      ebp
0x46C106: ret      
0x46C107: nop      
0x46C108: push     ebp
0x46C109: mov      ebp, esp
0x46C10B: add      esp, 0xffffff64
0x46C111: mov      dword ptr [ebp - 8], edx
0x46C114: mov      dword ptr [ebp - 4], eax
0x46C117: mov      eax, dword ptr [ebp - 8]
0x46C11A: call     0x403e88
0x46C11F: xor      eax, eax
0x46C121: push     ebp
0x46C122: push     0x46c166
0x46C127: push     dword ptr fs:[eax]
0x46C12A: mov      dword ptr fs:[eax], esp
0x46C12D: lea      edx, [ebp - 0x9c]
0x46C133: mov      ecx, 0x94
0x46C138: mov      eax, dword ptr [ebp - 8]
0x46C13B: call     0x45b37c
0x46C140: lea      edx, [ebp - 0x9c]
0x46C146: mov      eax, dword ptr [0x4f7d94]
0x46C14B: call     0x47dd18
0x46C150: xor      eax, eax
0x46C152: pop      edx
0x46C153: pop      ecx
0x46C154: pop      ecx
0x46C155: mov      dword ptr fs:[eax], edx
0x46C158: push     0x46c16d
0x46C15D: lea      eax, [ebp - 8]
0x46C160: call     0x403a58
0x46C165: ret      
0x46C166: jmp      0x403518
0x46C16B: jmp      0x46c15d
0x46C16D: mov      esp, ebp
0x46C16F: pop      ebp
0x46C170: ret      
0x46C171: lea      eax, [eax]
0x46C174: push     ebp
0x46C175: mov      ebp, esp
0x46C177: mov      ecx, 6
0x46C17C: push     0
0x46C17E: push     0
0x46C180: dec      ecx
0x46C181: jne      0x46c17c
0x46C183: mov      dword ptr [ebp - 8], edx
0x46C186: mov      dword ptr [ebp - 4], eax
0x46C189: mov      eax, dword ptr [ebp - 8]
0x46C18C: call     0x403e88
0x46C191: xor      eax, eax
0x46C193: push     ebp
0x46C194: push     0x46c363
0x46C199: push     dword ptr fs:[eax]
0x46C19C: mov      dword ptr fs:[eax], esp
0x46C19F: mov      eax, dword ptr [0x4f7d98]
0x46C1A4: call     0x47ec60
0x46C1A9: lea      edx, [ebp - 0x14]
0x46C1AC: mov      eax, dword ptr [ebp - 8]
0x46C1AF: call     0x45b2e0
0x46C1B4: xor      eax, eax
0x46C1B6: mov      dword ptr [ebp - 0xc], eax
0x46C1B9: push     0
0x46C1BB: lea      eax, [ebp - 0x2c]
0x46C1BE: push     eax
0x46C1BF: mov      byte ptr [ebp - 0x30], 0x2f
0x46C1C3: lea      ecx, [ebp - 0x30]
0x46C1C6: lea      edx, [ebp - 0x18]
0x46C1C9: mov      eax, dword ptr [ebp - 0x14]
0x46C1CC: call     0x452530
0x46C1D1: mov      edx, dword ptr [ebp - 0x2c]
0x46C1D4: lea      eax, [ebp - 0x14]
0x46C1D7: call     0x403af0
0x46C1DC: push     0
0x46C1DE: lea      eax, [ebp - 0x2c]
0x46C1E1: push     eax
0x46C1E2: mov      byte ptr [ebp - 0x30], 0x2f
0x46C1E6: lea      ecx, [ebp - 0x30]
0x46C1E9: lea      edx, [ebp - 0x1c]
0x46C1EC: mov      eax, dword ptr [ebp - 0x14]
0x46C1EF: call     0x452530
0x46C1F4: mov      edx, dword ptr [ebp - 0x2c]
0x46C1F7: lea      eax, [ebp - 0x14]
0x46C1FA: call     0x403af0
0x46C1FF: push     0
0x46C201: lea      eax, [ebp - 0x2c]
0x46C204: push     eax
0x46C205: mov      byte ptr [ebp - 0x30], 0x2f
0x46C209: lea      ecx, [ebp - 0x30]
0x46C20C: lea      edx, [ebp - 0x20]
0x46C20F: mov      eax, dword ptr [ebp - 0x14]
0x46C212: call     0x452530
0x46C217: mov      edx, dword ptr [ebp - 0x2c]
0x46C21A: lea      eax, [ebp - 0x14]
0x46C21D: call     0x403af0
0x46C222: push     0
0x46C224: lea      eax, [ebp - 0x2c]
0x46C227: push     eax
0x46C228: mov      byte ptr [ebp - 0x30], 0x2f
0x46C22C: lea      ecx, [ebp - 0x30]
0x46C22F: lea      edx, [ebp - 0x24]
0x46C232: mov      eax, dword ptr [ebp - 0x14]
0x46C235: call     0x452530
0x46C23A: mov      edx, dword ptr [ebp - 0x2c]
0x46C23D: lea      eax, [ebp - 0x14]
0x46C240: call     0x403af0
0x46C245: push     0
0x46C247: lea      eax, [ebp - 0x2c]
0x46C24A: push     eax
0x46C24B: mov      byte ptr [ebp - 0x30], 0x2f
0x46C24F: lea      ecx, [ebp - 0x30]
0x46C252: lea      edx, [ebp - 0x28]
0x46C255: mov      eax, dword ptr [ebp - 0x14]
0x46C258: call     0x452530
0x46C25D: mov      edx, dword ptr [ebp - 0x2c]
0x46C260: lea      eax, [ebp - 0x14]
0x46C263: call     0x403af0
0x46C268: xor      eax, eax
0x46C26A: mov      dword ptr [ebp - 0x10], eax
0x46C26D: cmp      dword ptr [ebp - 0x18], 0
0x46C271: je       0x46c2e3
0x46C273: cmp      dword ptr [ebp - 0x24], 0
0x46C277: je       0x46c2e3
0x46C279: cmp      dword ptr [ebp - 0x28], 0
0x46C27D: je       0x46c2e3
0x46C27F: mov      eax, dword ptr [ebp - 0x18]
0x46C282: cmp      byte ptr [eax], 0x2a
0x46C285: jne      0x46c2a9
0x46C287: mov      eax, dword ptr [ebp - 0xc]
0x46C28A: mov      dword ptr [ebp - 0x10], eax
0x46C28D: lea      eax, [ebp - 0x18]
0x46C290: push     eax
0x46C291: mov      eax, dword ptr [ebp - 0x18]
0x46C294: call     0x403cd4
0x46C299: mov      ecx, eax
0x46C29B: dec      ecx
0x46C29C: mov      edx, 2
0x46C2A1: mov      eax, dword ptr [ebp - 0x18]
0x46C2A4: call     0x403ed8
0x46C2A9: xor      edx, edx
0x46C2AB: mov      eax, dword ptr [ebp - 0x20]
0x46C2AE: call     0x4520e8
0x46C2B3: push     eax
0x46C2B4: xor      edx, edx
0x46C2B6: mov      eax, dword ptr [ebp - 0x24]
0x46C2B9: call     0x4520e8
0x46C2BE: push     eax
0x46C2BF: xor      edx, edx
0x46C2C1: mov      eax, dword ptr [ebp - 0x28]
0x46C2C4: call     0x4520e8
0x46C2C9: push     eax
0x46C2CA: xor      edx, edx
0x46C2CC: mov      eax, dword ptr [ebp - 0x1c]
0x46C2CF: call     0x4520e8
0x46C2D4: mov      ecx, eax
0x46C2D6: mov      edx, dword ptr [ebp - 0x18]
0x46C2D9: mov      eax, dword ptr [0x4f7d98]
0x46C2DE: call     0x47eca8
0x46C2E3: cmp      dword ptr [ebp - 0x10], 0
0x46C2E7: jne      0x46c30f
0x46C2E9: mov      eax, dword ptr [0x4f7d98]
0x46C2EE: mov      byte ptr [eax + 0x35], 0
0x46C2F2: mov      eax, dword ptr [0x4f7d98]
0x46C2F7: mov      byte ptr [eax + 0x34], 1
0x46C2FB: mov      eax, dword ptr [0x4f7d98]
0x46C300: mov      byte ptr [eax + 0x6d], 1
0x46C304: mov      eax, dword ptr [0x4f7d98]
0x46C309: mov      byte ptr [eax + 0x6c], 0
0x46C30D: jmp      0x46c333
0x46C30F: mov      eax, dword ptr [0x4f7d98]
0x46C314: mov      byte ptr [eax + 0x35], 1
0x46C318: mov      eax, dword ptr [0x4f7d98]
0x46C31D: mov      byte ptr [eax + 0x34], 0
0x46C321: mov      eax, dword ptr [0x4f7d98]
0x46C326: mov      byte ptr [eax + 0x6d], 0
0x46C32A: mov      eax, dword ptr [0x4f7d98]
0x46C32F: mov      byte ptr [eax + 0x6c], 1
0x46C333: inc      dword ptr [ebp - 0xc]
0x46C336: cmp      dword ptr [ebp - 0xc], 2
0x46C33A: jne      0x46c1b9
0x46C340: xor      eax, eax
0x46C342: pop      edx
0x46C343: pop      ecx
0x46C344: pop      ecx
0x46C345: mov      dword ptr fs:[eax], edx
0x46C348: push     0x46c36a
0x46C34D: lea      eax, [ebp - 0x2c]
0x46C350: mov      edx, 7
0x46C355: call     0x403a7c
0x46C35A: lea      eax, [ebp - 8]
0x46C35D: call     0x403a58
0x46C362: ret      
0x46C363: jmp      0x403518
0x46C368: jmp      0x46c34d
0x46C36A: mov      esp, ebp
0x46C36C: pop      ebp
0x46C36D: ret      
0x46C36E: mov      eax, eax
0x46C370: push     ebp
0x46C371: mov      ebp, esp
0x46C373: add      esp, -0x20
0x46C376: xor      ecx, ecx
0x46C378: mov      dword ptr [ebp - 0x20], ecx
0x46C37B: mov      dword ptr [ebp - 0xc], ecx
0x46C37E: mov      dword ptr [ebp - 0x10], ecx
0x46C381: mov      dword ptr [ebp - 0x14], ecx
0x46C384: mov      dword ptr [ebp - 8], edx
0x46C387: mov      dword ptr [ebp - 4], eax
0x46C38A: mov      eax, dword ptr [ebp - 8]
0x46C38D: call     0x403e88
0x46C392: xor      eax, eax
0x46C394: push     ebp
0x46C395: push     0x46c4e6
0x46C39A: push     dword ptr fs:[eax]
0x46C39D: mov      dword ptr fs:[eax], esp
0x46C3A0: lea      edx, [ebp - 0xc]
0x46C3A3: mov      eax, dword ptr [ebp - 8]
0x46C3A6: call     0x45b2e0
0x46C3AB: push     0
0x46C3AD: lea      eax, [ebp - 0x14]
0x46C3B0: push     eax
0x46C3B1: mov      byte ptr [ebp - 0x1c], 0x2f
0x46C3B5: lea      ecx, [ebp - 0x1c]
0x46C3B8: lea      edx, [ebp - 0x10]
0x46C3BB: mov      eax, dword ptr [ebp - 0xc]
0x46C3BE: call     0x452530
0x46C3C3: cmp      byte ptr [0x4f937c], 0
0x46C3CA: jne      0x46c427
0x46C3CC: xor      edx, edx
0x46C3CE: mov      eax, dword ptr [ebp - 4]
0x46C3D1: mov      eax, dword ptr [eax + 0x30c]
0x46C3D7: call     0x45a950
0x46C3DC: mov      eax, 0x1f4
0x46C3E1: call     0x45ed98
0x46C3E6: mov      byte ptr [0x4f937f], 3
0x46C3ED: mov      eax, dword ptr [ebp - 4]
0x46C3F0: mov      eax, dword ptr [eax + 0x30c]
0x46C3F6: mov      dword ptr [ebp - 0x18], eax
0x46C3F9: mov      edx, dword ptr [ebp - 0x10]
0x46C3FC: mov      eax, dword ptr [ebp - 0x18]
0x46C3FF: call     0x45a9ec
0x46C404: xor      edx, edx
0x46C406: mov      eax, dword ptr [ebp - 0x14]
0x46C409: call     0x4520e8
0x46C40E: mov      edx, eax
0x46C410: mov      eax, dword ptr [ebp - 0x18]
0x46C413: call     0x45ab2c
0x46C418: mov      dl, 1
0x46C41A: mov      eax, dword ptr [ebp - 0x18]
0x46C41D: call     0x45a950
0x46C422: jmp      0x46c4c3
0x46C427: mov      eax, dword ptr [ebp - 4]
0x46C42A: add      eax, 0x3c4
0x46C42F: call     0x403a58
0x46C434: mov      eax, dword ptr [ebp - 4]
0x46C437: add      eax, 0x3c8
0x46C43C: call     0x403a58
0x46C441: mov      eax, dword ptr [ebp - 4]
0x46C444: mov      eax, dword ptr [eax + 0x30c]
0x46C44A: mov      eax, dword ptr [eax + 0x80]
0x46C450: cmp      byte ptr [eax + 8], 0
0x46C454: je       0x46c48f
0x46C456: push     0x46c4fc ; "$R"
0x46C45B: push     dword ptr [ebp - 0x10]
0x46C45E: push     0x46c508 ; "/"
0x46C463: push     dword ptr [ebp - 0x14]
0x46C466: push     0x46c514 ; "%"
0x46C46B: lea      eax, [ebp - 0x20]
0x46C46E: mov      edx, 5
0x46C473: call     0x403d94
0x46C478: mov      edx, dword ptr [ebp - 0x20]
0x46C47B: mov      eax, dword ptr [ebp - 4]
0x46C47E: mov      eax, dword ptr [eax + 0x30c]
0x46C484: mov      eax, dword ptr [eax + 0x80]
0x46C48A: call     0x45a204
0x46C48F: mov      byte ptr [0x4f937f], 3
0x46C496: call     0x49f850
0x46C49B: mov      eax, dword ptr [0x4f7d8c]
0x46C4A0: call     0x479ed4
0x46C4A5: mov      dl, 6
0x46C4A7: mov      eax, dword ptr [0x4f7d8c]
0x46C4AC: call     0x4799ac
0x46C4B1: mov      eax, 0x1f4
0x46C4B6: call     0x45ed98
0x46C4BB: mov      eax, dword ptr [ebp - 4]
0x46C4BE: call     0x4653a8
0x46C4C3: xor      eax, eax
0x46C4C5: pop      edx
0x46C4C6: pop      ecx
0x46C4C7: pop      ecx
0x46C4C8: mov      dword ptr fs:[eax], edx
0x46C4CB: push     0x46c4ed
0x46C4D0: lea      eax, [ebp - 0x20]
0x46C4D3: call     0x403a58
0x46C4D8: lea      eax, [ebp - 0x14]
0x46C4DB: mov      edx, 4
0x46C4E0: call     0x403a7c
0x46C4E5: ret      
0x46C4E6: jmp      0x403518
0x46C4EB: jmp      0x46c4d0
0x46C4ED: mov      esp, ebp
0x46C4EF: pop      ebp
0x46C4F0: ret      
0x46C4F1: add      byte ptr [eax], al
0x46C4F3: add      bh, bh
0x46C4F7: inc      dword ptr [edx]
0x46C4F9: add      byte ptr [eax], al
0x46C4FB: add      byte ptr [edx + edx*2], ah
0x46C4FE: add      byte ptr [eax], al
0x46C508: [STR len=1] "/"
0x46C514: [STR len=1] "%"
0x46C518: push     ebp
0x46C519: mov      ebp, esp
0x46C51B: add      esp, -0x20
0x46C51E: xor      ecx, ecx
0x46C520: mov      dword ptr [ebp - 0x20], ecx
0x46C523: mov      dword ptr [ebp - 0xc], ecx
0x46C526: mov      dword ptr [ebp - 0x10], ecx
0x46C529: mov      dword ptr [ebp - 0x14], ecx
0x46C52C: mov      dword ptr [ebp - 8], edx
0x46C52F: mov      dword ptr [ebp - 4], eax
0x46C532: mov      eax, dword ptr [ebp - 8]
0x46C535: call     0x403e88
0x46C53A: xor      eax, eax
0x46C53C: push     ebp
0x46C53D: push     0x46c790
0x46C542: push     dword ptr fs:[eax]
0x46C545: mov      dword ptr fs:[eax], esp
0x46C548: lea      edx, [ebp - 0xc]
0x46C54B: mov      eax, dword ptr [ebp - 8]
0x46C54E: call     0x45b2e0
0x46C553: push     0
0x46C555: lea      eax, [ebp - 0x14]
0x46C558: push     eax
0x46C559: mov      byte ptr [ebp - 0x1c], 0x2f
0x46C55D: lea      ecx, [ebp - 0x1c]
0x46C560: lea      edx, [ebp - 0x10]
0x46C563: mov      eax, dword ptr [ebp - 0xc]
0x46C566: call     0x452530
0x46C56B: cmp      byte ptr [0x4f937c], 0
0x46C572: jne      0x46c622
0x46C578: cmp      byte ptr [0x4f9235], 0
0x46C57F: je       0x46c5b9
0x46C581: push     0x46c7a4 ; ".\Data\"
0x46C586: push     dword ptr [0x4f7ee4]
0x46C58C: push     0x46c7b4 ; "."
0x46C591: mov      eax, dword ptr [ebp - 4]
0x46C594: push     dword ptr [eax + 0x418]
0x46C59A: push     0x46c7c0 ; ".itm"
0x46C59F: lea      eax, [ebp - 0x20]
0x46C5A2: mov      edx, 5
0x46C5A7: call     0x403d94
0x46C5AC: mov      eax, dword ptr [ebp - 0x20]
0x46C5AF: mov      edx, 0x4f80d0
0x46C5B4: call     0x49f744
0x46C5B9: mov      byte ptr [0x4f9235], 0
0x46C5C0: mov      byte ptr [0x4f9234], 1
0x46C5C7: xor      edx, edx
0x46C5C9: mov      eax, dword ptr [ebp - 4]
0x46C5CC: mov      eax, dword ptr [eax + 0x30c]
0x46C5D2: call     0x45a950
0x46C5D7: mov      eax, 0x1f4
0x46C5DC: call     0x45ed98
0x46C5E1: mov      byte ptr [0x4f937f], 3
0x46C5E8: mov      eax, dword ptr [ebp - 4]
0x46C5EB: mov      eax, dword ptr [eax + 0x30c]
0x46C5F1: mov      dword ptr [ebp - 0x18], eax
0x46C5F4: mov      edx, dword ptr [ebp - 0x10]
0x46C5F7: mov      eax, dword ptr [ebp - 0x18]
0x46C5FA: call     0x45a9ec
0x46C5FF: xor      edx, edx
0x46C601: mov      eax, dword ptr [ebp - 0x14]
0x46C604: call     0x4520e8
0x46C609: mov      edx, eax
0x46C60B: mov      eax, dword ptr [ebp - 0x18]
0x46C60E: call     0x45ab2c
0x46C613: mov      dl, 1
0x46C615: mov      eax, dword ptr [ebp - 0x18]
0x46C618: call     0x45a950
0x46C61D: jmp      0x46c76d
0x46C622: cmp      byte ptr [0x4f9235], 0
0x46C629: je       0x46c663
0x46C62B: push     0x46c7a4 ; ".\Data\"
0x46C630: push     dword ptr [0x4f7ee4]
0x46C636: push     0x46c7b4 ; "."
0x46C63B: mov      eax, dword ptr [ebp - 4]
0x46C63E: push     dword ptr [eax + 0x418]
0x46C644: push     0x46c7c0 ; ".itm"
0x46C649: lea      eax, [ebp - 0x20]
0x46C64C: mov      edx, 5
0x46C651: call     0x403d94
0x46C656: mov      eax, dword ptr [ebp - 0x20]
0x46C659: mov      edx, 0x4f80d0
0x46C65E: call     0x49f744
0x46C663: mov      byte ptr [0x4f9235], 0
0x46C66A: mov      eax, dword ptr [ebp - 4]
0x46C66D: add      eax, 0x3c4
0x46C672: call     0x403a58
0x46C677: mov      eax, dword ptr [ebp - 4]
0x46C67A: add      eax, 0x3c8
0x46C67F: call     0x403a58
0x46C684: mov      byte ptr [0x4f9234], 1
0x46C68B: mov      eax, dword ptr [ebp - 4]
0x46C68E: mov      eax, dword ptr [eax + 0x30c]
0x46C694: mov      eax, dword ptr [eax + 0x80]
0x46C69A: cmp      byte ptr [eax + 8], 0
0x46C69E: je       0x46c6d9
0x46C6A0: push     0x46c7d0 ; "$C"
0x46C6A5: push     dword ptr [ebp - 0x10]
0x46C6A8: push     0x46c7dc ; "/"
0x46C6AD: push     dword ptr [ebp - 0x14]
0x46C6B0: push     0x46c7e8 ; "%"
0x46C6B5: lea      eax, [ebp - 0x20]
0x46C6B8: mov      edx, 5
0x46C6BD: call     0x403d94
0x46C6C2: mov      edx, dword ptr [ebp - 0x20]
0x46C6C5: mov      eax, dword ptr [ebp - 4]
0x46C6C8: mov      eax, dword ptr [eax + 0x30c]
0x46C6CE: mov      eax, dword ptr [eax + 0x80]
0x46C6D4: call     0x45a204
0x46C6D9: mov      eax, 0x1f4
0x46C6DE: call     0x45ed98
0x46C6E3: mov      eax, dword ptr [ebp - 4]
0x46C6E6: mov      eax, dword ptr [eax + 0x30c]
0x46C6EC: mov      eax, dword ptr [eax + 0x80]
0x46C6F2: cmp      byte ptr [eax + 8], 0
0x46C6F6: je       0x46c731
0x46C6F8: push     0x46c7f4 ; "$R"
0x46C6FD: push     dword ptr [ebp - 0x10]
0x46C700: push     0x46c7dc ; "/"
0x46C705: push     dword ptr [ebp - 0x14]
0x46C708: push     0x46c7e8 ; "%"
0x46C70D: lea      eax, [ebp - 0x20]
0x46C710: mov      edx, 5
0x46C715: call     0x403d94
0x46C71A: mov      edx, dword ptr [ebp - 0x20]
0x46C71D: mov      eax, dword ptr [ebp - 4]
0x46C720: mov      eax, dword ptr [eax + 0x30c]
0x46C726: mov      eax, dword ptr [eax + 0x80]
0x46C72C: call     0x45a204
0x46C731: mov      byte ptr [0x4f937f], 3
0x46C738: call     0x49f850
0x46C73D: mov      eax, dword ptr [0x4f7d8c]
0x46C742: call     0x479ed4
0x46C747: mov      dl, 6
0x46C749: mov      eax, dword ptr [0x4f7d8c]
0x46C74E: call     0x4799ac
0x46C753: mov      eax, 0x12c
0x46C758: call     0x45ed98
0x46C75D: mov      eax, dword ptr [ebp - 4]
0x46C760: call     0x4644f0
0x46C765: mov      eax, dword ptr [ebp - 4]
0x46C768: call     0x4653a8
0x46C76D: xor      eax, eax
0x46C76F: pop      edx
0x46C770: pop      ecx
0x46C771: pop      ecx
0x46C772: mov      dword ptr fs:[eax], edx
0x46C775: push     0x46c797
0x46C77A: lea      eax, [ebp - 0x20]
0x46C77D: call     0x403a58
0x46C782: lea      eax, [ebp - 0x14]
0x46C785: mov      edx, 4
0x46C78A: call     0x403a7c
0x46C78F: ret      
0x46C790: jmp      0x403518
0x46C795: jmp      0x46c77a
0x46C797: mov      esp, ebp
0x46C799: pop      ebp
0x46C79A: ret      
0x46C79B: add      bh, bh
0x46C79F: inc      dword ptr [edi]
0x46C7A1: add      byte ptr [eax], al
0x46C7A3: add      byte ptr [esi], ch
0x46C7A5: pop      esp
0x46C7A6: inc      esp
0x46C7A7: popal    
0x46C7A8: je       0x46c80b
0x46C7AA: pop      esp
0x46C7AB: add      bh, bh
0x46C7AF: inc      dword ptr [ecx]
0x46C7B1: add      byte ptr [eax], al
0x46C7B3: add      byte ptr [esi], ch
0x46C7B5: add      byte ptr [eax], al
0x46C7B7: add      bh, bh
0x46C7BB: inc      dword ptr [eax + eax]
0x46C7BE: add      byte ptr [eax], al
0x46C7C0: imul     esi, dword ptr cs:[ebp + ebp*2], 0xff000000
0x46C7CB: inc      dword ptr [edx]
0x46C7CD: add      byte ptr [eax], al
0x46C7CF: add      byte ptr [ebx + eax*2], ah
0x46C7D2: add      byte ptr [eax], al
0x46C7DC: [STR len=1] "/"
0x46C7E8: [STR len=1] "%"
0x46C7F4: [STR len=2] "$R"
0x46C7F8: push     ebp
0x46C7F9: mov      ebp, esp
0x46C7FB: add      esp, -0x14
0x46C7FE: xor      ecx, ecx
0x46C800: mov      dword ptr [ebp - 0x10], ecx
0x46C803: mov      dword ptr [ebp - 0xc], ecx
0x46C806: mov      dword ptr [ebp - 8], edx
0x46C809: mov      dword ptr [ebp - 4], eax
0x46C80C: mov      eax, dword ptr [ebp - 8]
0x46C80F: call     0x403e88
0x46C814: xor      eax, eax
0x46C816: push     ebp
0x46C817: push     0x46c883
0x46C81C: push     dword ptr fs:[eax]
0x46C81F: mov      dword ptr fs:[eax], esp
0x46C822: lea      edx, [ebp - 0x10]
0x46C825: mov      eax, dword ptr [ebp - 8]
0x46C828: call     0x45b2e0
0x46C82D: mov      edx, dword ptr [ebp - 0x10]
0x46C830: lea      eax, [ebp - 8]
0x46C833: call     0x403af0
0x46C838: push     0
0x46C83A: lea      eax, [ebp - 0x10]
0x46C83D: push     eax
0x46C83E: mov      byte ptr [ebp - 0x14], 0xd
0x46C842: lea      ecx, [ebp - 0x14]
0x46C845: lea      edx, [ebp - 0xc]
0x46C848: mov      eax, dword ptr [ebp - 8]
0x46C84B: call     0x452530
0x46C850: mov      edx, dword ptr [ebp - 0x10]
0x46C853: lea      eax, [ebp - 8]
0x46C856: call     0x403af0
0x46C85B: mov      eax, 0x4f7ee8
0x46C860: mov      edx, dword ptr [ebp - 0xc]
0x46C863: call     0x403aac
0x46C868: xor      eax, eax
0x46C86A: pop      edx
0x46C86B: pop      ecx
0x46C86C: pop      ecx
0x46C86D: mov      dword ptr fs:[eax], edx
0x46C870: push     0x46c88a
0x46C875: lea      eax, [ebp - 0x10]
0x46C878: mov      edx, 3
0x46C87D: call     0x403a7c
0x46C882: ret      
0x46C883: jmp      0x403518
0x46C888: jmp      0x46c875
0x46C88A: mov      esp, ebp
0x46C88C: pop      ebp
0x46C88D: ret      
0x46C88E: mov      eax, eax
0x46C890: push     ebp
0x46C891: mov      ebp, esp
0x46C893: add      esp, -0x20
0x46C896: push     ebx
0x46C897: xor      ebx, ebx
0x46C899: mov      dword ptr [ebp - 0x1c], ebx
0x46C89C: mov      dword ptr [ebp - 0x10], ebx
0x46C89F: mov      dword ptr [ebp - 0x14], ebx
0x46C8A2: mov      dword ptr [ebp - 0x18], ebx
0x46C8A5: mov      dword ptr [ebp - 0xc], ecx
0x46C8A8: mov      dword ptr [ebp - 8], edx
0x46C8AB: mov      dword ptr [ebp - 4], eax
0x46C8AE: mov      eax, dword ptr [ebp - 0xc]
0x46C8B1: call     0x403e88
0x46C8B6: xor      eax, eax
0x46C8B8: push     ebp
0x46C8B9: push     0x46c969
0x46C8BE: push     dword ptr fs:[eax]
0x46C8C1: mov      dword ptr fs:[eax], esp
0x46C8C4: mov      eax, dword ptr [ebp - 8]
0x46C8C7: mov      dword ptr [0x4f927c], eax
0x46C8CC: push     0
0x46C8CE: lea      eax, [ebp - 0x1c]
0x46C8D1: push     eax
0x46C8D2: mov      byte ptr [ebp - 0x20], 0x2f
0x46C8D6: lea      ecx, [ebp - 0x20]
0x46C8D9: lea      edx, [ebp - 0x10]
0x46C8DC: mov      eax, dword ptr [ebp - 0xc]
0x46C8DF: call     0x452530
0x46C8E4: mov      edx, dword ptr [ebp - 0x1c]
0x46C8E7: lea      eax, [ebp - 0xc]
0x46C8EA: call     0x403af0
0x46C8EF: push     0
0x46C8F1: lea      eax, [ebp - 0x18]
0x46C8F4: push     eax
0x46C8F5: mov      byte ptr [ebp - 0x20], 0x2f
0x46C8F9: lea      ecx, [ebp - 0x20]
0x46C8FC: lea      edx, [ebp - 0x14]
0x46C8FF: mov      eax, dword ptr [ebp - 0xc]
0x46C902: call     0x452530
0x46C907: mov      edx, 0x4f9284
0x46C90C: mov      ecx, 0x14
0x46C911: mov      eax, dword ptr [ebp - 0x10]
0x46C914: call     0x45b37c
0x46C919: mov      edx, 0x4f9298
0x46C91E: mov      ecx, 0x14
0x46C923: mov      eax, dword ptr [ebp - 0x14]
0x46C926: call     0x45b37c
0x46C92B: mov      edx, 0x4f92ac
0x46C930: mov      ecx, 0x14
0x46C935: mov      eax, dword ptr [ebp - 0x18]
0x46C938: call     0x45b37c
0x46C93D: mov      eax, 0x4f92c0
0x46C942: xor      ecx, ecx
0x46C944: mov      edx, 0x14
0x46C949: call     0x402b54
0x46C94E: xor      eax, eax
0x46C950: pop      edx
0x46C951: pop      ecx
0x46C952: pop      ecx
0x46C953: mov      dword ptr fs:[eax], edx
0x46C956: push     0x46c970
0x46C95B: lea      eax, [ebp - 0x1c]
0x46C95E: mov      edx, 5
0x46C963: call     0x403a7c
0x46C968: ret      
0x46C969: jmp      0x403518
0x46C96E: jmp      0x46c95b
0x46C970: pop      ebx
0x46C971: mov      esp, ebp
0x46C973: pop      ebp
0x46C974: ret      
0x46C975: lea      eax, [eax]
0x46C978: push     ebp
0x46C979: mov      ebp, esp
0x46C97B: add      esp, -0x58
0x46C97E: xor      ecx, ecx
0x46C980: mov      dword ptr [ebp - 0x40], ecx
0x46C983: mov      dword ptr [ebp - 8], edx
0x46C986: mov      dword ptr [ebp - 4], eax
0x46C989: mov      eax, dword ptr [ebp - 8]
0x46C98C: call     0x403e88
0x46C991: xor      eax, eax
0x46C993: push     ebp
0x46C994: push     0x46ca0d
0x46C999: push     dword ptr fs:[eax]
0x46C99C: mov      dword ptr fs:[eax], esp
0x46C99F: cmp      dword ptr [ebp - 8], 0
0x46C9A3: je       0x46c9ef
0x46C9A5: lea      edx, [ebp - 0x3c]
0x46C9A8: mov      ecx, 0x34
0x46C9AD: mov      eax, dword ptr [ebp - 8]
0x46C9B0: call     0x45b37c
0x46C9B5: lea      eax, [ebp - 0x3c]
0x46C9B8: call     0x49f874
0x46C9BD: lea      edx, [ebp - 0x3c]
0x46C9C0: lea      eax, [ebp - 0x58]
0x46C9C3: call     0x402a14
0x46C9C8: mov      edx, 0x46ca18
0x46C9CD: lea      eax, [ebp - 0x58]
0x46C9D0: mov      cl, 0x16
0x46C9D2: call     0x4029e4
0x46C9D7: lea      edx, [ebp - 0x58]
0x46C9DA: lea      eax, [ebp - 0x40]
0x46C9DD: call     0x403c78
0x46C9E2: mov      edx, dword ptr [ebp - 0x40]
0x46C9E5: mov      eax, dword ptr [0x4f7d8c]
0x46C9EA: call     0x479a68
0x46C9EF: xor      eax, eax
0x46C9F1: pop      edx
0x46C9F2: pop      ecx
0x46C9F3: pop      ecx
0x46C9F4: mov      dword ptr fs:[eax], edx
0x46C9F7: push     0x46ca14
0x46C9FC: lea      eax, [ebp - 0x40]
0x46C9FF: call     0x403a58
0x46CA04: lea      eax, [ebp - 8]
0x46CA07: call     0x403a58
0x46CA0C: ret      
0x46CA0D: jmp      0x403518
0x46CA12: jmp      0x46c9fc
0x46CA14: mov      esp, ebp
0x46CA16: pop      ebp
0x46CA17: ret      
0x46CA18: or       byte ptr [eax], ah
0x46CA1A: mov      cl, 0xbb
0x46CA1C: mov      bh, 0xa2
0x46CA1E: iretd    
0x46CA1F: salc     
0x46CA20: add      byte ptr cs:[eax], al
0x46CA23: add      byte ptr [ebp - 0x75], dl
0x46CA26: in       al, dx
0x46CA27: add      esp, -0x40
0x46CA2A: push     esi
0x46CA2B: push     edi
0x46CA2C: mov      dword ptr [ebp - 8], edx
0x46CA2F: mov      dword ptr [ebp - 4], eax
0x46CA32: mov      eax, dword ptr [ebp - 8]
0x46CA35: call     0x403e88
0x46CA3A: xor      eax, eax
0x46CA3C: push     ebp
0x46CA3D: push     0x46cac9
0x46CA42: push     dword ptr fs:[eax]
0x46CA45: mov      dword ptr fs:[eax], esp
0x46CA48: cmp      dword ptr [ebp - 8], 0
0x46CA4C: je       0x46cab3
0x46CA4E: lea      edx, [ebp - 0x40]
0x46CA51: mov      ecx, 0x34
0x46CA56: mov      eax, dword ptr [ebp - 8]
0x46CA59: call     0x45b37c
0x46CA5E: lea      eax, [ebp - 0x40]
0x46CA61: call     0x49f964
0x46CA66: xor      eax, eax
0x46CA68: mov      dword ptr [ebp - 0xc], eax
0x46CA6B: imul     eax, dword ptr [ebp - 0xc], 0xd
0x46CA6F: lea      eax, [eax*4 + 0x4f7efc]
0x46CA76: lea      edx, [ebp - 0x40]
0x46CA79: xor      ecx, ecx
0x46CA7B: mov      cl, byte ptr [eax]
0x46CA7D: inc      ecx
0x46CA7E: call     0x402ae4
0x46CA83: jne      0x46caaa
0x46CA85: imul     eax, dword ptr [ebp - 0xc], 0xd
0x46CA89: mov      eax, dword ptr [eax*4 + 0x4f7f28]
0x46CA90: cmp      eax, dword ptr [ebp - 0x14]
0x46CA93: jne      0x46caaa
0x46CA95: imul     eax, dword ptr [ebp - 0xc], 0xd
0x46CA99: lea      edi, [eax*4 + 0x4f7efc]
0x46CAA0: lea      esi, [ebp - 0x40]
0x46CAA3: mov      ecx, 0xd
0x46CAA8: rep movsd dword ptr es:[edi], dword ptr [esi]
0x46CAAA: inc      dword ptr [ebp - 0xc]
0x46CAAD: cmp      dword ptr [ebp - 0xc], 9
0x46CAB1: jne      0x46ca6b
0x46CAB3: xor      eax, eax
0x46CAB5: pop      edx
0x46CAB6: pop      ecx
0x46CAB7: pop      ecx
0x46CAB8: mov      dword ptr fs:[eax], edx
0x46CABB: push     0x46cad0
0x46CAC0: lea      eax, [ebp - 8]
0x46CAC3: call     0x403a58
0x46CAC8: ret      
0x46CAC9: jmp      0x403518
0x46CACE: jmp      0x46cac0
0x46CAD0: pop      edi
0x46CAD1: pop      esi
0x46CAD2: mov      esp, ebp
0x46CAD4: pop      ebp
0x46CAD5: ret      
0x46CAD6: mov      eax, eax
0x46CAD8: push     ebp
0x46CAD9: mov      ebp, esp
0x46CADB: add      esp, -0x44
0x46CADE: xor      ecx, ecx
0x46CAE0: mov      dword ptr [ebp - 0x44], ecx
0x46CAE3: mov      dword ptr [ebp - 8], edx
0x46CAE6: mov      dword ptr [ebp - 4], eax
0x46CAE9: mov      eax, dword ptr [ebp - 8]
0x46CAEC: call     0x403e88
0x46CAF1: xor      eax, eax
0x46CAF3: push     ebp
0x46CAF4: push     0x46cb8d
0x46CAF9: push     dword ptr fs:[eax]
0x46CAFC: mov      dword ptr fs:[eax], esp
0x46CAFF: cmp      dword ptr [ebp - 8], 0
0x46CB03: je       0x46cb6f
0x46CB05: lea      edx, [ebp - 0x40]
0x46CB08: mov      ecx, 0x34
0x46CB0D: mov      eax, dword ptr [ebp - 8]
0x46CB10: call     0x45b37c
0x46CB15: lea      eax, [ebp - 0x44]
0x46CB18: lea      edx, [ebp - 0x40]
0x46CB1B: call     0x403c78
0x46CB20: mov      eax, dword ptr [ebp - 0x44]
0x46CB23: mov      edx, dword ptr [ebp - 0x14]
0x46CB26: call     0x49f9e4
0x46CB2B: xor      eax, eax
0x46CB2D: mov      dword ptr [ebp - 0xc], eax
0x46CB30: imul     eax, dword ptr [ebp - 0xc], 0xd
0x46CB34: lea      eax, [eax*4 + 0x4f7efc]
0x46CB3B: lea      edx, [ebp - 0x40]
0x46CB3E: xor      ecx, ecx
0x46CB40: mov      cl, byte ptr [eax]
0x46CB42: inc      ecx
0x46CB43: call     0x402ae4
0x46CB48: jne      0x46cb66
0x46CB4A: imul     eax, dword ptr [ebp - 0xc], 0xd
0x46CB4E: mov      eax, dword ptr [eax*4 + 0x4f7f28]
0x46CB55: cmp      eax, dword ptr [ebp - 0x14]
0x46CB58: jne      0x46cb66
0x46CB5A: imul     eax, dword ptr [ebp - 0xc], 0xd
0x46CB5E: mov      byte ptr [eax*4 + 0x4f7efc], 0
0x46CB66: inc      dword ptr [ebp - 0xc]
0x46CB69: cmp      dword ptr [ebp - 0xc], 9
0x46CB6D: jne      0x46cb30
0x46CB6F: xor      eax, eax
0x46CB71: pop      edx
0x46CB72: pop      ecx
0x46CB73: pop      ecx
0x46CB74: mov      dword ptr fs:[eax], edx
0x46CB77: push     0x46cb94
0x46CB7C: lea      eax, [ebp - 0x44]
0x46CB7F: call     0x403a58
0x46CB84: lea      eax, [ebp - 8]
0x46CB87: call     0x403a58
0x46CB8C: ret      
0x46CB8D: jmp      0x403518
0x46CB92: jmp      0x46cb7c
0x46CB94: mov      esp, ebp
0x46CB96: pop      ebp
0x46CB97: ret      
0x46CB98: push     ebp
0x46CB99: mov      ebp, esp
0x46CB9B: add      esp, -0x54
0x46CB9E: xor      ecx, ecx
0x46CBA0: mov      dword ptr [ebp - 0x50], ecx
0x46CBA3: mov      dword ptr [ebp - 0x14], ecx
0x46CBA6: mov      dword ptr [ebp - 0x18], ecx
0x46CBA9: mov      dword ptr [ebp - 8], edx
0x46CBAC: mov      dword ptr [ebp - 4], eax
0x46CBAF: mov      eax, dword ptr [ebp - 8]
0x46CBB2: call     0x403e88
0x46CBB7: xor      eax, eax
0x46CBB9: push     ebp
0x46CBBA: push     0x46ccc9
0x46CBBF: push     dword ptr fs:[eax]
0x46CBC2: mov      dword ptr fs:[eax], esp
0x46CBC5: lea      edx, [ebp - 0x50]
0x46CBC8: mov      eax, dword ptr [ebp - 8]
0x46CBCB: call     0x45b2e0
0x46CBD0: mov      edx, dword ptr [ebp - 0x50]
0x46CBD3: lea      eax, [ebp - 8]
0x46CBD6: call     0x403af0
0x46CBDB: jmp      0x46cc94
0x46CBE0: push     0
0x46CBE2: lea      eax, [ebp - 0x50]
0x46CBE5: push     eax
0x46CBE6: mov      byte ptr [ebp - 0x54], 0x2f
0x46CBEA: lea      ecx, [ebp - 0x54]
0x46CBED: lea      edx, [ebp - 0x18]
0x46CBF0: mov      eax, dword ptr [ebp - 8]
0x46CBF3: call     0x452530
0x46CBF8: mov      edx, dword ptr [ebp - 0x50]
0x46CBFB: lea      eax, [ebp - 8]
0x46CBFE: call     0x403af0
0x46CC03: push     0
0x46CC05: lea      eax, [ebp - 0x50]
0x46CC08: push     eax
0x46CC09: mov      byte ptr [ebp - 0x54], 0x2f
0x46CC0D: lea      ecx, [ebp - 0x54]
0x46CC10: lea      edx, [ebp - 0x14]
0x46CC13: mov      eax, dword ptr [ebp - 8]
0x46CC16: call     0x452530
0x46CC1B: mov      edx, dword ptr [ebp - 0x50]
0x46CC1E: lea      eax, [ebp - 8]
0x46CC21: call     0x403af0
0x46CC26: cmp      dword ptr [ebp - 0x18], 0
0x46CC2A: je       0x46cc9e
0x46CC2C: cmp      dword ptr [ebp - 0x14], 0
0x46CC30: je       0x46cc9e
0x46CC32: xor      edx, edx
0x46CC34: mov      eax, dword ptr [ebp - 0x14]
0x46CC37: call     0x4520e8
0x46CC3C: mov      dword ptr [ebp - 0x10], eax
0x46CC3F: mov      edx, dword ptr [ebp - 0x10]
0x46CC42: mov      eax, dword ptr [ebp - 0x18]
0x46CC45: call     0x49f9e4
0x46CC4A: xor      eax, eax
0x46CC4C: mov      dword ptr [ebp - 0xc], eax
0x46CC4F: lea      eax, [ebp - 0x50]
0x46CC52: imul     edx, dword ptr [ebp - 0xc], 0xd
0x46CC56: lea      edx, [edx*4 + 0x4f7efc]
0x46CC5D: call     0x403c78
0x46CC62: mov      eax, dword ptr [ebp - 0x50]
0x46CC65: mov      edx, dword ptr [ebp - 0x18]
0x46CC68: call     0x403de4
0x46CC6D: jne      0x46cc8b
0x46CC6F: imul     eax, dword ptr [ebp - 0xc], 0xd
0x46CC73: mov      eax, dword ptr [eax*4 + 0x4f7f28]
0x46CC7A: cmp      eax, dword ptr [ebp - 0x10]
0x46CC7D: jne      0x46cc8b
0x46CC7F: imul     eax, dword ptr [ebp - 0xc], 0xd
0x46CC83: mov      byte ptr [eax*4 + 0x4f7efc], 0
0x46CC8B: inc      dword ptr [ebp - 0xc]
0x46CC8E: cmp      dword ptr [ebp - 0xc], 9
0x46CC92: jne      0x46cc4f
0x46CC94: cmp      dword ptr [ebp - 8], 0
0x46CC98: jne      0x46cbe0
0x46CC9E: xor      eax, eax
0x46CCA0: pop      edx
0x46CCA1: pop      ecx
0x46CCA2: pop      ecx
0x46CCA3: mov      dword ptr fs:[eax], edx
0x46CCA6: push     0x46ccd0
0x46CCAB: lea      eax, [ebp - 0x50]
0x46CCAE: call     0x403a58
0x46CCB3: lea      eax, [ebp - 0x18]
0x46CCB6: mov      edx, 2
0x46CCBB: call     0x403a7c
0x46CCC0: lea      eax, [ebp - 8]
0x46CCC3: call     0x403a58
0x46CCC8: ret      
0x46CCC9: jmp      0x403518
0x46CCCE: jmp      0x46ccab
0x46CCD0: mov      esp, ebp
0x46CCD2: pop      ebp
0x46CCD3: ret      
0x46CCD4: push     ebp
0x46CCD5: mov      ebp, esp
0x46CCD7: add      esp, -0x10
0x46CCDA: mov      byte ptr [ebp - 0xd], 1
0x46CCDE: xor      eax, eax
0x46CCE0: mov      dword ptr [ebp - 8], eax
0x46CCE3: imul     eax, dword ptr [ebp - 8], 0xd
0x46CCE7: mov      edx, dword ptr [ebp + 8]
0x46CCEA: cmp      byte ptr [edx + eax*4 - 0xa90], 0
0x46CCF2: je       0x46cd94
0x46CCF8: mov      byte ptr [ebp - 0xd], 0
0x46CCFC: xor      eax, eax
0x46CCFE: mov      dword ptr [ebp - 0xc], eax
0x46CD01: imul     eax, dword ptr [ebp - 0xc], 0xd
0x46CD05: lea      eax, [eax*4 + 0x4f80d0]
0x46CD0C: imul     edx, dword ptr [ebp - 8], 0xd
0x46CD10: mov      ecx, dword ptr [ebp + 8]
0x46CD13: lea      edx, [ecx + edx*4 - 0xa90]
0x46CD1A: xor      ecx, ecx
0x46CD1C: mov      cl, byte ptr [eax]
0x46CD1E: inc      ecx
0x46CD1F: call     0x402ae4
0x46CD24: jne      0x46cd81
0x46CD26: imul     eax, dword ptr [ebp - 8], 0xd
0x46CD2A: mov      edx, dword ptr [ebp + 8]
0x46CD2D: mov      eax, dword ptr [edx + eax*4 - 0xa64]
0x46CD34: imul     edx, dword ptr [ebp - 0xc], 0xd
0x46CD38: cmp      eax, dword ptr [edx*4 + 0x4f80fc]
0x46CD3F: jne      0x46cd81
0x46CD41: imul     eax, dword ptr [ebp - 8], 0xd
0x46CD45: mov      edx, dword ptr [ebp + 8]
0x46CD48: mov      ax, word ptr [edx + eax*4 - 0xa60]
0x46CD50: imul     edx, dword ptr [ebp - 0xc], 0xd
0x46CD54: cmp      ax, word ptr [edx*4 + 0x4f8100]
0x46CD5C: jne      0x46cd8e
0x46CD5E: imul     eax, dword ptr [ebp - 8], 0xd
0x46CD62: mov      edx, dword ptr [ebp + 8]
0x46CD65: mov      ax, word ptr [edx + eax*4 - 0xa5e]
0x46CD6D: imul     edx, dword ptr [ebp - 0xc], 0xd
0x46CD71: cmp      ax, word ptr [edx*4 + 0x4f8102]
0x46CD79: jne      0x46cd8e
0x46CD7B: mov      byte ptr [ebp - 0xd], 1
0x46CD7F: jmp      0x46cd8e
0x46CD81: inc      dword ptr [ebp - 0xc]
0x46CD84: cmp      dword ptr [ebp - 0xc], 0x34
0x46CD88: jne      0x46cd01
0x46CD8E: cmp      byte ptr [ebp - 0xd], 0
0x46CD92: je       0x46cda1
0x46CD94: inc      dword ptr [ebp - 8]
0x46CD97: cmp      dword ptr [ebp - 8], 0x34
0x46CD9B: jne      0x46cce3
0x46CDA1: cmp      byte ptr [ebp - 0xd], 0
0x46CDA5: je       0x46ce6b
0x46CDAB: xor      eax, eax
0x46CDAD: mov      dword ptr [ebp - 8], eax
0x46CDB0: imul     eax, dword ptr [ebp - 8], 0xd
0x46CDB4: cmp      byte ptr [eax*4 + 0x4f80d0], 0
0x46CDBC: je       0x46ce5e
0x46CDC2: mov      byte ptr [ebp - 0xd], 0
0x46CDC6: xor      eax, eax
0x46CDC8: mov      dword ptr [ebp - 0xc], eax
0x46CDCB: imul     eax, dword ptr [ebp - 8], 0xd
0x46CDCF: lea      eax, [eax*4 + 0x4f80d0]
0x46CDD6: imul     edx, dword ptr [ebp - 0xc], 0xd
0x46CDDA: mov      ecx, dword ptr [ebp + 8]
0x46CDDD: lea      edx, [ecx + edx*4 - 0xa90]
0x46CDE4: xor      ecx, ecx
0x46CDE6: mov      cl, byte ptr [eax]
0x46CDE8: inc      ecx
0x46CDE9: call     0x402ae4
0x46CDEE: jne      0x46ce4b
0x46CDF0: imul     eax, dword ptr [ebp - 0xc], 0xd
0x46CDF4: mov      edx, dword ptr [ebp + 8]
0x46CDF7: mov      eax, dword ptr [edx + eax*4 - 0xa64]
0x46CDFE: imul     edx, dword ptr [ebp - 8], 0xd
0x46CE02: cmp      eax, dword ptr [edx*4 + 0x4f80fc]
0x46CE09: jne      0x46ce4b
0x46CE0B: imul     eax, dword ptr [ebp - 0xc], 0xd
0x46CE0F: mov      edx, dword ptr [ebp + 8]
0x46CE12: mov      ax, word ptr [edx + eax*4 - 0xa60]
0x46CE1A: imul     edx, dword ptr [ebp - 8], 0xd
0x46CE1E: cmp      ax, word ptr [edx*4 + 0x4f8100]
0x46CE26: jne      0x46ce58
0x46CE28: imul     eax, dword ptr [ebp - 0xc], 0xd
0x46CE2C: mov      edx, dword ptr [ebp + 8]
0x46CE2F: mov      ax, word ptr [edx + eax*4 - 0xa5e]
0x46CE37: imul     edx, dword ptr [ebp - 8], 0xd
0x46CE3B: cmp      ax, word ptr [edx*4 + 0x4f8102]
0x46CE43: jne      0x46ce58
0x46CE45: mov      byte ptr [ebp - 0xd], 1
0x46CE49: jmp      0x46ce58
0x46CE4B: inc      dword ptr [ebp - 0xc]
0x46CE4E: cmp      dword ptr [ebp - 0xc], 0x34
0x46CE52: jne      0x46cdcb
0x46CE58: cmp      byte ptr [ebp - 0xd], 0
0x46CE5C: je       0x46ce6b
0x46CE5E: inc      dword ptr [ebp - 8]
0x46CE61: cmp      dword ptr [ebp - 8], 0x34
0x46CE65: jne      0x46cdb0
0x46CE6B: mov      al, byte ptr [ebp - 0xd]
0x46CE6E: mov      byte ptr [ebp - 1], al
0x46CE71: mov      al, byte ptr [ebp - 1]
0x46CE74: mov      esp, ebp
0x46CE76: pop      ebp
0x46CE77: ret      
0x46CE78: push     ebp
0x46CE79: mov      ebp, esp
0x46CE7B: add      esp, 0xfffff528
0x46CE81: xor      ecx, ecx
0x46CE83: mov      dword ptr [ebp - 0xad4], ecx
0x46CE89: mov      dword ptr [ebp - 0xa9c], ecx
0x46CE8F: mov      dword ptr [ebp - 0xa98], edx
0x46CE95: mov      dword ptr [ebp - 0xa94], eax
0x46CE9B: mov      eax, dword ptr [ebp - 0xa98]
0x46CEA1: call     0x403e88
0x46CEA6: xor      eax, eax
0x46CEA8: push     ebp
0x46CEA9: push     0x46cfd2
0x46CEAE: push     dword ptr fs:[eax]
0x46CEB1: mov      dword ptr fs:[eax], esp
0x46CEB4: mov      eax, 0x4f80d0
0x46CEB9: xor      ecx, ecx
0x46CEBB: mov      edx, 0xa90
0x46CEC0: call     0x402b54
0x46CEC5: cmp      dword ptr [ebp - 0xa98], 0
0x46CECC: je       0x46cf29
0x46CECE: push     0
0x46CED0: lea      eax, [ebp - 0xad4]
0x46CED6: push     eax
0x46CED7: mov      byte ptr [ebp - 0xad8], 0x2f
0x46CEDE: lea      ecx, [ebp - 0xad8]
0x46CEE4: lea      edx, [ebp - 0xa9c]
0x46CEEA: mov      eax, dword ptr [ebp - 0xa98]
0x46CEF0: call     0x452530
0x46CEF5: mov      edx, dword ptr [ebp - 0xad4]
0x46CEFB: lea      eax, [ebp - 0xa98]
0x46CF01: call     0x403af0
0x46CF06: lea      edx, [ebp - 0xad0]
0x46CF0C: mov      ecx, 0x34
0x46CF11: mov      eax, dword ptr [ebp - 0xa9c]
0x46CF17: call     0x45b37c
0x46CF1C: lea      eax, [ebp - 0xad0]
0x46CF22: call     0x49f874
0x46CF27: jmp      0x46cec5
0x46CF29: lea      eax, [ebp - 0xa90]
0x46CF2F: xor      ecx, ecx
0x46CF31: mov      edx, 0xa90
0x46CF36: call     0x402b54
0x46CF3B: push     0x46cfe8 ; ".\Data\"
0x46CF40: push     dword ptr [0x4f7ee4]
0x46CF46: push     0x46cff8 ; "."
0x46CF4B: mov      eax, dword ptr [ebp - 0xa94]
0x46CF51: push     dword ptr [eax + 0x418]
0x46CF57: push     0x46d004 ; ".itm"
0x46CF5C: lea      eax, [ebp - 0xad4]
0x46CF62: mov      edx, 5
0x46CF67: call     0x403d94
0x46CF6C: mov      eax, dword ptr [ebp - 0xad4]
0x46CF72: lea      edx, [ebp - 0xa90]
0x46CF78: call     0x49f7d0
0x46CF7D: push     ebp
0x46CF7E: call     0x46ccd4
0x46CF83: pop      ecx
0x46CF84: test     al, al
0x46CF86: je       0x46cf9d
0x46CF88: mov      edx, 0x4f80d0
0x46CF8D: lea      eax, [ebp - 0xa90]
0x46CF93: mov      ecx, 0xa90
0x46CF98: call     0x4027cc
0x46CF9D: call     0x49faa4
0x46CFA2: mov      byte ptr [0x4f9235], 1
0x46CFA9: xor      eax, eax
0x46CFAB: pop      edx
0x46CFAC: pop      ecx
0x46CFAD: pop      ecx
0x46CFAE: mov      dword ptr fs:[eax], edx
0x46CFB1: push     0x46cfd9
0x46CFB6: lea      eax, [ebp - 0xad4]
0x46CFBC: call     0x403a58
0x46CFC1: lea      eax, [ebp - 0xa9c]
0x46CFC7: mov      edx, 2
0x46CFCC: call     0x403a7c
0x46CFD1: ret      
0x46CFD2: jmp      0x403518
0x46CFD7: jmp      0x46cfb6
0x46CFD9: mov      esp, ebp
0x46CFDB: pop      ebp
0x46CFDC: ret      
0x46CFDD: add      byte ptr [eax], al
0x46CFDF: add      bh, bh
0x46CFE3: inc      dword ptr [edi]
0x46CFE5: add      byte ptr [eax], al
0x46CFE7: add      byte ptr [esi], ch
0x46CFE9: pop      esp
0x46CFEA: inc      esp
0x46CFEB: popal    
0x46CFEC: je       0x46d04f
0x46CFEE: pop      esp
0x46CFEF: add      bh, bh
0x46CFF3: inc      dword ptr [ecx]
0x46CFF5: add      byte ptr [eax], al
0x46CFF7: add      byte ptr [esi], ch
0x46CFF9: add      byte ptr [eax], al
0x46CFFB: add      bh, bh
0x46CFFF: inc      dword ptr [eax + eax]
0x46D002: add      byte ptr [eax], al
0x46D004: imul     esi, dword ptr cs:[ebp + ebp*2], 0x55000000
0x46D00D: mov      ebp, esp
0x46D00F: add      esp, -0x10
0x46D012: mov      dword ptr [ebp - 0xc], ecx
0x46D015: mov      dword ptr [ebp - 8], edx
0x46D018: mov      dword ptr [ebp - 4], eax
0x46D01B: mov      eax, dword ptr [ebp - 8]
0x46D01E: call     0x403e88
0x46D023: xor      eax, eax
0x46D025: push     ebp
0x46D026: push     0x46d06e
0x46D02B: push     dword ptr fs:[eax]
0x46D02E: mov      dword ptr fs:[eax], esp
0x46D031: mov      edx, dword ptr [ebp - 0xc]
0x46D034: mov      eax, dword ptr [ebp - 8]
0x46D037: call     0x49fc50
0x46D03C: mov      dword ptr [ebp - 0x10], eax
0x46D03F: cmp      dword ptr [ebp - 0x10], 0
0x46D043: je       0x46d058
0x46D045: mov      eax, dword ptr [ebp - 0x10]
0x46D048: call     0x49f874
0x46D04D: mov      edx, dword ptr [ebp - 0xc]
0x46D050: mov      eax, dword ptr [ebp - 8]
0x46D053: call     0x49fd14
0x46D058: xor      eax, eax
0x46D05A: pop      edx
0x46D05B: pop      ecx
0x46D05C: pop      ecx
0x46D05D: mov      dword ptr fs:[eax], edx
0x46D060: push     0x46d075
0x46D065: lea      eax, [ebp - 8]
0x46D068: call     0x403a58
0x46D06D: ret      
0x46D06E: jmp      0x403518
0x46D073: jmp      0x46d065
0x46D075: mov      esp, ebp
0x46D077: pop      ebp
0x46D078: ret      
0x46D079: lea      eax, [eax]
0x46D07C: push     ebp
0x46D07D: mov      ebp, esp
0x46D07F: add      esp, 0xfffffee8
0x46D085: mov      dword ptr [ebp - 0xc], ecx
0x46D088: mov      dword ptr [ebp - 8], edx
0x46D08B: mov      dword ptr [ebp - 4], eax
0x46D08E: mov      eax, dword ptr [ebp + 8]
0x46D091: call     0x403e88
0x46D096: xor      eax, eax
0x46D098: push     ebp
0x46D099: push     0x46d17b
0x46D09E: push     dword ptr fs:[eax]
0x46D0A1: mov      dword ptr fs:[eax], esp
0x46D0A4: mov      eax, dword ptr [0x4f7da4]
0x46D0A9: mov      eax, dword ptr [eax + 8]
0x46D0AC: dec      eax
0x46D0AD: test     eax, eax
0x46D0AF: jl       0x46d0dc
0x46D0B1: inc      eax
0x46D0B2: mov      dword ptr [ebp - 0x18], eax
0x46D0B5: mov      dword ptr [ebp - 0x10], 0
0x46D0BC: mov      edx, dword ptr [ebp - 0x10]
0x46D0BF: mov      eax, dword ptr [0x4f7da4]
0x46D0C4: call     0x40f6a0
0x46D0C9: mov      eax, dword ptr [eax]
0x46D0CB: cmp      eax, dword ptr [ebp - 8]
0x46D0CE: je       0x46d165
0x46D0D4: inc      dword ptr [ebp - 0x10]
0x46D0D7: dec      dword ptr [ebp - 0x18]
0x46D0DA: jne      0x46d0bc
0x46D0DC: mov      eax, 0x2c
0x46D0E1: call     0x4026b4
0x46D0E6: mov      dword ptr [ebp - 0x14], eax
0x46D0E9: mov      eax, dword ptr [ebp - 8]
0x46D0EC: mov      edx, dword ptr [ebp - 0x14]
0x46D0EF: mov      dword ptr [edx], eax
0x46D0F1: mov      ax, word ptr [ebp - 0xc]
0x46D0F5: mov      edx, dword ptr [ebp - 0x14]
0x46D0F8: mov      word ptr [edx + 4], ax
0x46D0FC: mov      ax, word ptr [ebp + 0x10]
0x46D100: mov      edx, dword ptr [ebp - 0x14]
0x46D103: mov      word ptr [edx + 6], ax
0x46D107: mov      ax, word ptr [ebp + 0xc]
0x46D10B: mov      edx, dword ptr [ebp - 0x14]
0x46D10E: mov      word ptr [edx + 8], ax
0x46D112: lea      eax, [ebp - 0x118]
0x46D118: mov      edx, dword ptr [ebp + 8]
0x46D11B: mov      ecx, 0xff
0x46D120: call     0x403cb0
0x46D125: lea      edx, [ebp - 0x118]
0x46D12B: mov      eax, dword ptr [ebp - 0x14]
0x46D12E: add      eax, 0x1c
0x46D131: mov      cl, 0xe
0x46D133: call     0x402a30
0x46D138: mov      eax, 0xbb8
0x46D13D: call     0x402b74
0x46D142: push     eax
0x46D143: call     0x406154
0x46D148: pop      edx
0x46D149: sub      eax, edx
0x46D14B: mov      edx, dword ptr [ebp - 0x14]
0x46D14E: mov      dword ptr [edx + 0xc], eax
0x46D151: mov      eax, dword ptr [ebp - 0x14]
0x46D154: mov      byte ptr [eax + 0x10], 0
0x46D158: mov      edx, dword ptr [ebp - 0x14]
0x46D15B: mov      eax, dword ptr [0x4f7da4]
0x46D160: call     0x40f588
0x46D165: xor      eax, eax
0x46D167: pop      edx
0x46D168: pop      ecx
0x46D169: pop      ecx
0x46D16A: mov      dword ptr fs:[eax], edx
0x46D16D: push     0x46d182
0x46D172: lea      eax, [ebp + 8]
0x46D175: call     0x403a58
0x46D17A: ret      
0x46D17B: jmp      0x403518
0x46D180: jmp      0x46d172
0x46D182: mov      esp, ebp
0x46D184: pop      ebp
0x46D185: ret      0xc
0x46D188: push     ebp
0x46D189: mov      ebp, esp
0x46D18B: add      esp, -0x18
0x46D18E: mov      dword ptr [ebp - 0x14], ecx
0x46D191: mov      dword ptr [ebp - 8], edx
0x46D194: mov      dword ptr [ebp - 4], eax
0x46D197: mov      eax, dword ptr [0x4f7da4]
0x46D19C: mov      eax, dword ptr [eax + 8]
0x46D19F: dec      eax
0x46D1A0: test     eax, eax
0x46D1A2: jl       0x46d1f1
0x46D1A4: inc      eax
0x46D1A5: mov      dword ptr [ebp - 0x10], eax
0x46D1A8: mov      dword ptr [ebp - 0xc], 0
0x46D1AF: mov      edx, dword ptr [ebp - 0xc]
0x46D1B2: mov      eax, dword ptr [0x4f7da4]
0x46D1B7: call     0x40f6a0
0x46D1BC: mov      eax, dword ptr [eax]
0x46D1BE: cmp      eax, dword ptr [ebp - 8]
0x46D1C1: jne      0x46d1e9
0x46D1C3: mov      edx, dword ptr [ebp - 0xc]
0x46D1C6: mov      eax, dword ptr [0x4f7da4]
0x46D1CB: call     0x40f6a0
0x46D1D0: mov      edx, 0x2c
0x46D1D5: call     0x4026cc
0x46D1DA: mov      edx, dword ptr [ebp - 0xc]
0x46D1DD: mov      eax, dword ptr [0x4f7da4]
0x46D1E2: call     0x40f5c4
0x46D1E7: jmp      0x46d1f1
0x46D1E9: inc      dword ptr [ebp - 0xc]
0x46D1EC: dec      dword ptr [ebp - 0x10]
0x46D1EF: jne      0x46d1af
0x46D1F1: mov      esp, ebp
0x46D1F3: pop      ebp
0x46D1F4: ret      4
0x46D1F7: nop      
0x46D1F8: push     ebp
0x46D1F9: mov      ebp, esp
0x46D1FB: add      esp, -0x50
0x46D1FE: push     esi
0x46D1FF: push     edi
0x46D200: xor      ecx, ecx
0x46D202: mov      dword ptr [ebp - 0x4c], ecx
0x46D205: mov      dword ptr [ebp - 0x10], ecx
0x46D208: mov      dword ptr [ebp - 0x14], ecx
0x46D20B: mov      dword ptr [ebp - 8], edx
0x46D20E: mov      dword ptr [ebp - 4], eax
0x46D211: mov      eax, dword ptr [ebp - 8]
0x46D214: call     0x403e88
0x46D219: xor      eax, eax
0x46D21B: push     ebp
0x46D21C: push     0x46d2f3
0x46D221: push     dword ptr fs:[eax]
0x46D224: mov      dword ptr fs:[eax], esp
0x46D227: mov      eax, 0x4f7efc
0x46D22C: xor      ecx, ecx
0x46D22E: mov      edx, 0x1d4
0x46D233: call     0x402b54
0x46D238: cmp      dword ptr [ebp - 8], 0
0x46D23C: je       0x46d2c8
0x46D242: push     0
0x46D244: lea      eax, [ebp - 0x4c]
0x46D247: push     eax
0x46D248: mov      byte ptr [ebp - 0x50], 0x2f
0x46D24C: lea      ecx, [ebp - 0x50]
0x46D24F: lea      edx, [ebp - 0x10]
0x46D252: mov      eax, dword ptr [ebp - 8]
0x46D255: call     0x452530
0x46D25A: mov      edx, dword ptr [ebp - 0x4c]
0x46D25D: lea      eax, [ebp - 8]
0x46D260: call     0x403af0
0x46D265: push     0
0x46D267: lea      eax, [ebp - 0x4c]
0x46D26A: push     eax
0x46D26B: mov      byte ptr [ebp - 0x50], 0x2f
0x46D26F: lea      ecx, [ebp - 0x50]
0x46D272: lea      edx, [ebp - 0x14]
0x46D275: mov      eax, dword ptr [ebp - 8]
0x46D278: call     0x452530
0x46D27D: mov      edx, dword ptr [ebp - 0x4c]
0x46D280: lea      eax, [ebp - 8]
0x46D283: call     0x403af0
0x46D288: or       edx, 0xffffffff
0x46D28B: mov      eax, dword ptr [ebp - 0x10]
0x46D28E: call     0x4520e8
0x46D293: mov      dword ptr [ebp - 0xc], eax
0x46D296: mov      eax, dword ptr [ebp - 0xc]
0x46D299: sub      eax, 9
0x46D29C: jae      0x46d238
0x46D29E: lea      edx, [ebp - 0x48]
0x46D2A1: mov      ecx, 0x34
0x46D2A6: mov      eax, dword ptr [ebp - 0x14]
0x46D2A9: call     0x45b37c
0x46D2AE: imul     eax, dword ptr [ebp - 0xc], 0xd
0x46D2B2: lea      edi, [eax*4 + 0x4f7efc]
0x46D2B9: lea      esi, [ebp - 0x48]
0x46D2BC: mov      ecx, 0xd
0x46D2C1: rep movsd dword ptr es:[edi], dword ptr [esi]
0x46D2C3: jmp      0x46d238
0x46D2C8: xor      eax, eax
0x46D2CA: pop      edx
0x46D2CB: pop      ecx
0x46D2CC: pop      ecx
0x46D2CD: mov      dword ptr fs:[eax], edx
0x46D2D0: push     0x46d2fa
0x46D2D5: lea      eax, [ebp - 0x4c]
0x46D2D8: call     0x403a58
0x46D2DD: lea      eax, [ebp - 0x14]
0x46D2E0: mov      edx, 2
0x46D2E5: call     0x403a7c
0x46D2EA: lea      eax, [ebp - 8]
0x46D2ED: call     0x403a58
0x46D2F2: ret      
0x46D2F3: jmp      0x403518
0x46D2F8: jmp      0x46d2d5
0x46D2FA: pop      edi
0x46D2FB: pop      esi
0x46D2FC: mov      esp, ebp
0x46D2FE: pop      ebp
0x46D2FF: ret      
0x46D300: push     ebp
0x46D301: mov      ebp, esp
0x46D303: add      esp, -0xc
0x46D306: mov      dword ptr [ebp - 8], edx
0x46D309: mov      dword ptr [ebp - 4], eax
0x46D30C: mov      eax, dword ptr [ebp - 8]
0x46D30F: call     0x403e88
0x46D314: xor      eax, eax
0x46D316: push     ebp
0x46D317: push     0x46d362
0x46D31C: push     dword ptr fs:[eax]
0x46D31F: mov      dword ptr fs:[eax], esp
0x46D322: mov      eax, 0x54
0x46D327: call     0x4026b4
0x46D32C: mov      dword ptr [ebp - 0xc], eax
0x46D32F: mov      edx, dword ptr [ebp - 0xc]
0x46D332: mov      ecx, 0x54
0x46D337: mov      eax, dword ptr [ebp - 8]
0x46D33A: call     0x45b37c
0x46D33F: mov      edx, dword ptr [ebp - 0xc]
0x46D342: mov      eax, dword ptr [0x4f9190]
0x46D347: call     0x40f588
0x46D34C: xor      eax, eax
0x46D34E: pop      edx
0x46D34F: pop      ecx
0x46D350: pop      ecx
0x46D351: mov      dword ptr fs:[eax], edx
0x46D354: push     0x46d369
0x46D359: lea      eax, [ebp - 8]
0x46D35C: call     0x403a58
0x46D361: ret      
0x46D362: jmp      0x403518
0x46D367: jmp      0x46d359
0x46D369: mov      esp, ebp
0x46D36B: pop      ebp
0x46D36C: ret      
0x46D36D: lea      eax, [eax]
0x46D370: push     ebp
0x46D371: mov      ebp, esp
0x46D373: add      esp, -0xc
0x46D376: mov      dword ptr [ebp - 8], edx
0x46D379: mov      dword ptr [ebp - 4], eax
0x46D37C: mov      eax, dword ptr [0x4f9190]
0x46D381: mov      eax, dword ptr [eax + 8]
0x46D384: dec      eax
0x46D385: cmp      eax, 0
0x46D388: jl       0x46d3d2
0x46D38A: mov      dword ptr [ebp - 0xc], eax
0x46D38D: mov      edx, dword ptr [ebp - 0xc]
0x46D390: mov      eax, dword ptr [0x4f9190]
0x46D395: call     0x40f6a0
0x46D39A: movzx    eax, word ptr [eax + 8]
0x46D39E: cmp      eax, dword ptr [ebp - 8]
0x46D3A1: jne      0x46d3c9
0x46D3A3: mov      edx, dword ptr [ebp - 0xc]
0x46D3A6: mov      eax, dword ptr [0x4f9190]
0x46D3AB: call     0x40f6a0
0x46D3B0: mov      edx, 0x54
0x46D3B5: call     0x4026cc
0x46D3BA: mov      edx, dword ptr [ebp - 0xc]
0x46D3BD: mov      eax, dword ptr [0x4f9190]
0x46D3C2: call     0x40f5c4
0x46D3C7: jmp      0x46d3d2
0x46D3C9: dec      dword ptr [ebp - 0xc]
0x46D3CC: cmp      dword ptr [ebp - 0xc], -1
0x46D3D0: jne      0x46d38d
0x46D3D2: mov      esp, ebp
0x46D3D4: pop      ebp
0x46D3D5: ret      
0x46D3D6: mov      eax, eax
0x46D3D8: push     ebp
0x46D3D9: mov      ebp, esp
0x46D3DB: add      esp, -0x20
0x46D3DE: push     ebx
0x46D3DF: xor      ecx, ecx
0x46D3E1: mov      dword ptr [ebp - 0x1c], ecx
0x46D3E4: mov      dword ptr [ebp - 0x10], ecx
0x46D3E7: mov      dword ptr [ebp - 8], edx
0x46D3EA: mov      dword ptr [ebp - 4], eax
0x46D3ED: mov      eax, dword ptr [ebp - 8]
0x46D3F0: call     0x403e88
0x46D3F5: xor      eax, eax
0x46D3F7: push     ebp
0x46D3F8: push     0x46d4c9
0x46D3FD: push     dword ptr fs:[eax]
0x46D400: mov      dword ptr fs:[eax], esp
0x46D403: mov      eax, dword ptr [0x4f9190]
0x46D408: mov      eax, dword ptr [eax + 8]
0x46D40B: dec      eax
0x46D40C: test     eax, eax
0x46D40E: jl       0x46d43a
0x46D410: inc      eax
0x46D411: mov      dword ptr [ebp - 0x18], eax
0x46D414: mov      dword ptr [ebp - 0xc], 0
0x46D41B: mov      edx, dword ptr [ebp - 0xc]
0x46D41E: mov      eax, dword ptr [0x4f9190]
0x46D423: call     0x40f6a0
0x46D428: mov      edx, 0x54
0x46D42D: call     0x4026cc
0x46D432: inc      dword ptr [ebp - 0xc]
0x46D435: dec      dword ptr [ebp - 0x18]
0x46D438: jne      0x46d41b
0x46D43A: mov      eax, dword ptr [0x4f9190]
0x46D43F: mov      bx, 0xffff
0x46D443: call     0x403080
0x46D448: cmp      dword ptr [ebp - 8], 0
0x46D44C: je       0x46d4a3
0x46D44E: push     0
0x46D450: lea      eax, [ebp - 0x1c]
0x46D453: push     eax
0x46D454: mov      byte ptr [ebp - 0x20], 0x2f
0x46D458: lea      ecx, [ebp - 0x20]
0x46D45B: lea      edx, [ebp - 0x10]
0x46D45E: mov      eax, dword ptr [ebp - 8]
0x46D461: call     0x452530
0x46D466: mov      edx, dword ptr [ebp - 0x1c]
0x46D469: lea      eax, [ebp - 8]
0x46D46C: call     0x403af0
0x46D471: cmp      dword ptr [ebp - 0x10], 0
0x46D475: je       0x46d4a3
0x46D477: mov      eax, 0x54
0x46D47C: call     0x4026b4
0x46D481: mov      dword ptr [ebp - 0x14], eax
0x46D484: mov      edx, dword ptr [ebp - 0x14]
0x46D487: mov      ecx, 0x54
0x46D48C: mov      eax, dword ptr [ebp - 0x10]
0x46D48F: call     0x45b37c
0x46D494: mov      edx, dword ptr [ebp - 0x14]
0x46D497: mov      eax, dword ptr [0x4f9190]
0x46D49C: call     0x40f588
0x46D4A1: jmp      0x46d448
0x46D4A3: xor      eax, eax
0x46D4A5: pop      edx
0x46D4A6: pop      ecx
0x46D4A7: pop      ecx
0x46D4A8: mov      dword ptr fs:[eax], edx
0x46D4AB: push     0x46d4d0
0x46D4B0: lea      eax, [ebp - 0x1c]
0x46D4B3: call     0x403a58
0x46D4B8: lea      eax, [ebp - 0x10]
0x46D4BB: call     0x403a58
0x46D4C0: lea      eax, [ebp - 8]
0x46D4C3: call     0x403a58
0x46D4C8: ret      
0x46D4C9: jmp      0x403518
0x46D4CE: jmp      0x46d4b0
0x46D4D0: pop      ebx
0x46D4D1: mov      esp, ebp
0x46D4D3: pop      ebp
0x46D4D4: ret      
0x46D4D5: lea      eax, [eax]
0x46D4D8: push     ebp
0x46D4D9: mov      ebp, esp
0x46D4DB: add      esp, -0x10
0x46D4DE: mov      dword ptr [ebp - 0xc], ecx
0x46D4E1: mov      dword ptr [ebp - 8], edx
0x46D4E4: mov      dword ptr [ebp - 4], eax
0x46D4E7: mov      eax, dword ptr [0x4f9190]
0x46D4EC: mov      eax, dword ptr [eax + 8]
0x46D4EF: dec      eax
0x46D4F0: cmp      eax, 0
0x46D4F3: jl       0x46d53f
0x46D4F5: mov      dword ptr [ebp - 0x10], eax
0x46D4F8: mov      edx, dword ptr [ebp - 0x10]
0x46D4FB: mov      eax, dword ptr [0x4f9190]
0x46D500: call     0x40f6a0
0x46D505: movzx    eax, word ptr [eax + 8]
0x46D509: cmp      eax, dword ptr [ebp - 8]
0x46D50C: jne      0x46d536
0x46D50E: mov      edx, dword ptr [ebp - 0x10]
0x46D511: mov      eax, dword ptr [0x4f9190]
0x46D516: call     0x40f6a0
0x46D51B: mov      dl, byte ptr [ebp - 0xc]
0x46D51E: mov      byte ptr [eax + 1], dl
0x46D521: mov      edx, dword ptr [ebp - 0x10]
0x46D524: mov      eax, dword ptr [0x4f9190]
0x46D529: call     0x40f6a0
0x46D52E: mov      edx, dword ptr [ebp + 8]
0x46D531: mov      dword ptr [eax + 4], edx
0x46D534: jmp      0x46d53f
0x46D536: dec      dword ptr [ebp - 0x10]
0x46D539: cmp      dword ptr [ebp - 0x10], -1
0x46D53D: jne      0x46d4f8
0x46D53F: mov      esp, ebp
0x46D541: pop      ebp
0x46D542: ret      4
0x46D545: lea      eax, [eax]
0x46D548: push     ebp
0x46D549: mov      ebp, esp
0x46D54B: add      esp, -0xc
0x46D54E: mov      dword ptr [ebp - 0xc], ecx
0x46D551: mov      dword ptr [ebp - 8], edx
0x46D554: mov      dword ptr [ebp - 4], eax
0x46D557: mov      eax, dword ptr [ebp - 8]
0x46D55A: sub      eax, 9
0x46D55D: jae      0x46d58d
0x46D55F: imul     eax, dword ptr [ebp - 8], 0xd
0x46D563: cmp      byte ptr [eax*4 + 0x4f7efc], 0
0x46D56B: je       0x46d58d
0x46D56D: imul     eax, dword ptr [ebp - 8], 0xd
0x46D571: mov      dx, word ptr [ebp - 0xc]
0x46D575: mov      word ptr [eax*4 + 0x4f7f2c], dx
0x46D57D: imul     eax, dword ptr [ebp - 8], 0xd
0x46D581: mov      dx, word ptr [ebp + 8]
0x46D585: mov      word ptr [eax*4 + 0x4f7f2e], dx
0x46D58D: mov      esp, ebp
0x46D58F: pop      ebp
0x46D590: ret      4
0x46D593: nop      
0x46D594: push     ebp
0x46D595: mov      ebp, esp
0x46D597: add      esp, -0x18
0x46D59A: push     ebx
0x46D59B: xor      ebx, ebx
0x46D59D: mov      dword ptr [ebp - 0x14], ebx
0x46D5A0: mov      dword ptr [ebp - 0x10], ebx
0x46D5A3: mov      dword ptr [ebp - 0xc], ecx
0x46D5A6: mov      dword ptr [ebp - 8], edx
0x46D5A9: mov      dword ptr [ebp - 4], eax
0x46D5AC: mov      eax, dword ptr [ebp + 8]
0x46D5AF: call     0x403e88
0x46D5B4: xor      eax, eax
0x46D5B6: push     ebp
0x46D5B7: push     0x46d664
0x46D5BC: push     dword ptr fs:[eax]
0x46D5BF: mov      dword ptr fs:[eax], esp
0x46D5C2: mov      eax, dword ptr [0x4f7ef8]
0x46D5C7: movzx    eax, word ptr [eax + 8]
0x46D5CB: mov      dword ptr [0x4f94e0], eax
0x46D5D0: mov      eax, dword ptr [0x4f7ef8]
0x46D5D5: movzx    eax, word ptr [eax + 0xa]
0x46D5D9: mov      dword ptr [0x4f94e4], eax
0x46D5DE: mov      eax, dword ptr [0x4f94dc]
0x46D5E3: cmp      eax, dword ptr [ebp - 8]
0x46D5E6: je       0x46d608
0x46D5E8: mov      eax, dword ptr [ebp - 8]
0x46D5EB: mov      dword ptr [0x4f94dc], eax
0x46D5F0: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46D5F5: mov      eax, dword ptr [eax]
0x46D5F7: call     0x48e750
0x46D5FC: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46D601: mov      eax, dword ptr [eax]
0x46D603: call     0x48e9d0
0x46D608: push     0
0x46D60A: lea      eax, [ebp - 0x14]
0x46D60D: push     eax
0x46D60E: mov      byte ptr [ebp - 0x18], 0x2f
0x46D612: lea      ecx, [ebp - 0x18]
0x46D615: lea      edx, [ebp - 0x10]
0x46D618: mov      eax, dword ptr [ebp + 8]
0x46D61B: call     0x452530
0x46D620: mov      edx, dword ptr [ebp - 0x14]
0x46D623: lea      eax, [ebp + 8]
0x46D626: call     0x403af0
0x46D62B: mov      eax, dword ptr [ebp + 8]
0x46D62E: push     eax
0x46D62F: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46D634: mov      eax, dword ptr [eax]
0x46D636: mov      ecx, dword ptr [ebp - 0x10]
0x46D639: mov      edx, dword ptr [ebp - 0xc]
0x46D63C: call     0x48e60c
0x46D641: xor      eax, eax
0x46D643: pop      edx
0x46D644: pop      ecx
0x46D645: pop      ecx
0x46D646: mov      dword ptr fs:[eax], edx
0x46D649: push     0x46d66b
0x46D64E: lea      eax, [ebp - 0x14]
0x46D651: mov      edx, 2
0x46D656: call     0x403a7c
0x46D65B: lea      eax, [ebp + 8]
0x46D65E: call     0x403a58
0x46D663: ret      
0x46D664: jmp      0x403518
0x46D669: jmp      0x46d64e
0x46D66B: pop      ebx
0x46D66C: mov      esp, ebp
0x46D66E: pop      ebp
0x46D66F: ret      4
0x46D672: mov      eax, eax
0x46D674: push     ebp
0x46D675: mov      ebp, esp
0x46D677: add      esp, 0xfffffed0
0x46D67D: push     ebx
0x46D67E: xor      ebx, ebx
0x46D680: mov      dword ptr [ebp - 0x2c], ebx
0x46D683: mov      dword ptr [ebp - 0x28], ebx
0x46D686: mov      dword ptr [ebp - 0xc], ebx
0x46D689: mov      dword ptr [ebp - 0x10], ebx
0x46D68C: mov      dword ptr [ebp - 0x14], ebx
0x46D68F: mov      dword ptr [ebp - 0x18], ebx
0x46D692: mov      dword ptr [ebp - 0x20], ecx
0x46D695: mov      dword ptr [ebp - 8], edx
0x46D698: mov      dword ptr [ebp - 4], eax
0x46D69B: mov      eax, dword ptr [ebp + 8]
0x46D69E: call     0x403e88
0x46D6A3: xor      eax, eax
0x46D6A5: push     ebp
0x46D6A6: push     0x46d85d
0x46D6AB: push     dword ptr fs:[eax]
0x46D6AE: mov      dword ptr fs:[eax], esp
0x46D6B1: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46D6B6: mov      eax, dword ptr [eax]
0x46D6B8: call     0x48e750
0x46D6BD: mov      eax, dword ptr [ebp - 8]
0x46D6C0: mov      dword ptr [0x4f94dc], eax
0x46D6C5: lea      edx, [ebp - 0x2c]
0x46D6C8: mov      eax, dword ptr [ebp + 8]
0x46D6CB: call     0x45b2e0
0x46D6D0: mov      edx, dword ptr [ebp - 0x2c]
0x46D6D3: lea      eax, [ebp + 8]
0x46D6D6: call     0x403af0
0x46D6DB: jmp      0x46d806
0x46D6E0: push     0
0x46D6E2: lea      eax, [ebp - 0x2c]
0x46D6E5: push     eax
0x46D6E6: mov      byte ptr [ebp - 0x30], 0x2f
0x46D6EA: lea      ecx, [ebp - 0x30]
0x46D6ED: lea      edx, [ebp - 0xc]
0x46D6F0: mov      eax, dword ptr [ebp + 8]
0x46D6F3: call     0x452530
0x46D6F8: mov      edx, dword ptr [ebp - 0x2c]
0x46D6FB: lea      eax, [ebp + 8]
0x46D6FE: call     0x403af0
0x46D703: push     0
0x46D705: lea      eax, [ebp - 0x2c]
0x46D708: push     eax
0x46D709: mov      byte ptr [ebp - 0x30], 0x2f
0x46D70D: lea      ecx, [ebp - 0x30]
0x46D710: lea      edx, [ebp - 0x10]
0x46D713: mov      eax, dword ptr [ebp + 8]
0x46D716: call     0x452530
0x46D71B: mov      edx, dword ptr [ebp - 0x2c]
0x46D71E: lea      eax, [ebp + 8]
0x46D721: call     0x403af0
0x46D726: push     0
0x46D728: lea      eax, [ebp - 0x2c]
0x46D72B: push     eax
0x46D72C: mov      byte ptr [ebp - 0x30], 0x2f
0x46D730: lea      ecx, [ebp - 0x30]
0x46D733: lea      edx, [ebp - 0x14]
0x46D736: mov      eax, dword ptr [ebp + 8]
0x46D739: call     0x452530
0x46D73E: mov      edx, dword ptr [ebp - 0x2c]
0x46D741: lea      eax, [ebp + 8]
0x46D744: call     0x403af0
0x46D749: push     0
0x46D74B: lea      eax, [ebp - 0x2c]
0x46D74E: push     eax
0x46D74F: mov      byte ptr [ebp - 0x30], 0x2f
0x46D753: lea      ecx, [ebp - 0x30]
0x46D756: lea      edx, [ebp - 0x18]
0x46D759: mov      eax, dword ptr [ebp + 8]
0x46D75C: call     0x452530
0x46D761: mov      edx, dword ptr [ebp - 0x2c]
0x46D764: lea      eax, [ebp + 8]
0x46D767: call     0x403af0
0x46D76C: cmp      dword ptr [ebp - 0xc], 0
0x46D770: je       0x46d810
0x46D776: cmp      dword ptr [ebp - 0x14], 0
0x46D77A: je       0x46d810
0x46D780: cmp      dword ptr [ebp - 0x18], 0
0x46D784: je       0x46d810
0x46D78A: mov      eax, 0x1c
0x46D78F: call     0x4026b4
0x46D794: mov      dword ptr [ebp - 0x1c], eax
0x46D797: lea      eax, [ebp - 0x130]
0x46D79D: mov      edx, dword ptr [ebp - 0xc]
0x46D7A0: mov      ecx, 0xff
0x46D7A5: call     0x403cb0
0x46D7AA: lea      edx, [ebp - 0x130]
0x46D7B0: mov      eax, dword ptr [ebp - 0x1c]
0x46D7B3: mov      cl, 0xe
0x46D7B5: call     0x402a30
0x46D7BA: xor      edx, edx
0x46D7BC: mov      eax, dword ptr [ebp - 0x10]
0x46D7BF: call     0x4520e8
0x46D7C4: mov      edx, dword ptr [ebp - 0x1c]
0x46D7C7: mov      byte ptr [edx + 0xf], al
0x46D7CA: xor      edx, edx
0x46D7CC: mov      eax, dword ptr [ebp - 0x14]
0x46D7CF: call     0x4520e8
0x46D7D4: mov      edx, dword ptr [ebp - 0x1c]
0x46D7D7: mov      dword ptr [edx + 0x10], eax
0x46D7DA: xor      edx, edx
0x46D7DC: mov      eax, dword ptr [ebp - 0x18]
0x46D7DF: call     0x4520e8
0x46D7E4: mov      edx, dword ptr [ebp - 0x1c]
0x46D7E7: mov      dword ptr [edx + 0x14], eax
0x46D7EA: mov      eax, dword ptr [ebp - 0x1c]
0x46D7ED: mov      byte ptr [eax + 0x18], 0xff
0x46D7F1: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46D7F6: mov      eax, dword ptr [eax]
0x46D7F8: mov      eax, dword ptr [eax + 0x764]
0x46D7FE: mov      edx, dword ptr [ebp - 0x1c]
0x46D801: call     0x40f588
0x46D806: cmp      dword ptr [ebp + 8], 0
0x46D80A: jne      0x46d6e0
0x46D810: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46D815: mov      eax, dword ptr [eax]
0x46D817: call     0x48e85c
0x46D81C: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46D821: mov      eax, dword ptr [eax]
0x46D823: add      eax, 0x76c
0x46D828: call     0x403a58
0x46D82D: xor      eax, eax
0x46D82F: pop      edx
0x46D830: pop      ecx
0x46D831: pop      ecx
0x46D832: mov      dword ptr fs:[eax], edx
0x46D835: push     0x46d864
0x46D83A: lea      eax, [ebp - 0x2c]
0x46D83D: mov      edx, 2
0x46D842: call     0x403a7c
0x46D847: lea      eax, [ebp - 0x18]
0x46D84A: mov      edx, 4
0x46D84F: call     0x403a7c
0x46D854: lea      eax, [ebp + 8]
0x46D857: call     0x403a58
0x46D85C: ret      
0x46D85D: jmp      0x403518
0x46D862: jmp      0x46d83a
0x46D864: pop      ebx
0x46D865: mov      esp, ebp
0x46D867: pop      ebp
0x46D868: ret      4
0x46D86B: nop      
0x46D86C: push     ebp
0x46D86D: mov      ebp, esp
0x46D86F: add      esp, 0xfffffed0
0x46D875: push     ebx
0x46D876: xor      ebx, ebx
0x46D878: mov      dword ptr [ebp - 0x2c], ebx
0x46D87B: mov      dword ptr [ebp - 0x28], ebx
0x46D87E: mov      dword ptr [ebp - 0x10], ebx
0x46D881: mov      dword ptr [ebp - 0x14], ebx
0x46D884: mov      dword ptr [ebp - 0x18], ebx
0x46D887: mov      dword ptr [ebp - 0x1c], ebx
0x46D88A: mov      dword ptr [ebp - 0xc], ecx
0x46D88D: mov      dword ptr [ebp - 8], edx
0x46D890: mov      dword ptr [ebp - 4], eax
0x46D893: mov      eax, dword ptr [ebp - 0xc]
0x46D896: call     0x403e88
0x46D89B: xor      eax, eax
0x46D89D: push     ebp
0x46D89E: push     0x46da5b
0x46D8A3: push     dword ptr fs:[eax]
0x46D8A6: mov      dword ptr fs:[eax], esp
0x46D8A9: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46D8AE: mov      eax, dword ptr [eax]
0x46D8B0: call     0x48e750
0x46D8B5: mov      eax, dword ptr [ebp - 8]
0x46D8B8: mov      dword ptr [0x4f94dc], eax
0x46D8BD: lea      edx, [ebp - 0x2c]
0x46D8C0: mov      eax, dword ptr [ebp - 0xc]
0x46D8C3: call     0x45b2e0
0x46D8C8: mov      edx, dword ptr [ebp - 0x2c]
0x46D8CB: lea      eax, [ebp - 0xc]
0x46D8CE: call     0x403af0
0x46D8D3: jmp      0x46d9fe
0x46D8D8: push     0
0x46D8DA: lea      eax, [ebp - 0x2c]
0x46D8DD: push     eax
0x46D8DE: mov      byte ptr [ebp - 0x30], 0x2f
0x46D8E2: lea      ecx, [ebp - 0x30]
0x46D8E5: lea      edx, [ebp - 0x10]
0x46D8E8: mov      eax, dword ptr [ebp - 0xc]
0x46D8EB: call     0x452530
0x46D8F0: mov      edx, dword ptr [ebp - 0x2c]
0x46D8F3: lea      eax, [ebp - 0xc]
0x46D8F6: call     0x403af0
0x46D8FB: push     0
0x46D8FD: lea      eax, [ebp - 0x2c]
0x46D900: push     eax
0x46D901: mov      byte ptr [ebp - 0x30], 0x2f
0x46D905: lea      ecx, [ebp - 0x30]
0x46D908: lea      edx, [ebp - 0x14]
0x46D90B: mov      eax, dword ptr [ebp - 0xc]
0x46D90E: call     0x452530
0x46D913: mov      edx, dword ptr [ebp - 0x2c]
0x46D916: lea      eax, [ebp - 0xc]
0x46D919: call     0x403af0
0x46D91E: push     0
0x46D920: lea      eax, [ebp - 0x2c]
0x46D923: push     eax
0x46D924: mov      byte ptr [ebp - 0x30], 0x2f
0x46D928: lea      ecx, [ebp - 0x30]
0x46D92B: lea      edx, [ebp - 0x18]
0x46D92E: mov      eax, dword ptr [ebp - 0xc]
0x46D931: call     0x452530
0x46D936: mov      edx, dword ptr [ebp - 0x2c]
0x46D939: lea      eax, [ebp - 0xc]
0x46D93C: call     0x403af0
0x46D941: push     0
0x46D943: lea      eax, [ebp - 0x2c]
0x46D946: push     eax
0x46D947: mov      byte ptr [ebp - 0x30], 0x2f
0x46D94B: lea      ecx, [ebp - 0x30]
0x46D94E: lea      edx, [ebp - 0x1c]
0x46D951: mov      eax, dword ptr [ebp - 0xc]
0x46D954: call     0x452530
0x46D959: mov      edx, dword ptr [ebp - 0x2c]
0x46D95C: lea      eax, [ebp - 0xc]
0x46D95F: call     0x403af0
0x46D964: cmp      dword ptr [ebp - 0x10], 0
0x46D968: je       0x46da08
0x46D96E: cmp      dword ptr [ebp - 0x18], 0
0x46D972: je       0x46da08
0x46D978: cmp      dword ptr [ebp - 0x1c], 0
0x46D97C: je       0x46da08
0x46D982: mov      eax, 0x1c
0x46D987: call     0x4026b4
0x46D98C: mov      dword ptr [ebp - 0x20], eax
0x46D98F: lea      eax, [ebp - 0x130]
0x46D995: mov      edx, dword ptr [ebp - 0x10]
0x46D998: mov      ecx, 0xff
0x46D99D: call     0x403cb0
0x46D9A2: lea      edx, [ebp - 0x130]
0x46D9A8: mov      eax, dword ptr [ebp - 0x20]
0x46D9AB: mov      cl, 0xe
0x46D9AD: call     0x402a30
0x46D9B2: xor      edx, edx
0x46D9B4: mov      eax, dword ptr [ebp - 0x14]
0x46D9B7: call     0x4520e8
0x46D9BC: mov      edx, dword ptr [ebp - 0x20]
0x46D9BF: mov      byte ptr [edx + 0xf], al
0x46D9C2: xor      edx, edx
0x46D9C4: mov      eax, dword ptr [ebp - 0x18]
0x46D9C7: call     0x4520e8
0x46D9CC: mov      edx, dword ptr [ebp - 0x20]
0x46D9CF: mov      dword ptr [edx + 0x10], eax
0x46D9D2: xor      edx, edx
0x46D9D4: mov      eax, dword ptr [ebp - 0x1c]
0x46D9D7: call     0x4520e8
0x46D9DC: mov      edx, dword ptr [ebp - 0x20]
0x46D9DF: mov      dword ptr [edx + 0x14], eax
0x46D9E2: mov      eax, dword ptr [ebp - 0x20]
0x46D9E5: mov      byte ptr [eax + 0x18], 0xff
0x46D9E9: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46D9EE: mov      eax, dword ptr [eax]
0x46D9F0: mov      eax, dword ptr [eax + 0x764]
0x46D9F6: mov      edx, dword ptr [ebp - 0x20]
0x46D9F9: call     0x40f588
0x46D9FE: cmp      dword ptr [ebp - 0xc], 0
0x46DA02: jne      0x46d8d8
0x46DA08: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46DA0D: mov      eax, dword ptr [eax]
0x46DA0F: call     0x48e85c
0x46DA14: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46DA19: mov      eax, dword ptr [eax]
0x46DA1B: add      eax, 0x76c
0x46DA20: call     0x403a58
0x46DA25: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46DA2A: mov      eax, dword ptr [eax]
0x46DA2C: mov      byte ptr [eax + 0x777], 1
0x46DA33: xor      eax, eax
0x46DA35: pop      edx
0x46DA36: pop      ecx
0x46DA37: pop      ecx
0x46DA38: mov      dword ptr fs:[eax], edx
0x46DA3B: push     0x46da62
0x46DA40: lea      eax, [ebp - 0x2c]
0x46DA43: mov      edx, 2
0x46DA48: call     0x403a7c
0x46DA4D: lea      eax, [ebp - 0x1c]
0x46DA50: mov      edx, 5
0x46DA55: call     0x403a7c
0x46DA5A: ret      
0x46DA5B: jmp      0x403518
0x46DA60: jmp      0x46da40
0x46DA62: pop      ebx
0x46DA63: mov      esp, ebp
0x46DA65: pop      ebp
0x46DA66: ret      
0x46DA67: nop      
0x46DA68: push     ebp
0x46DA69: mov      ebp, esp
0x46DA6B: add      esp, -8
0x46DA6E: mov      dword ptr [ebp - 8], edx
0x46DA71: mov      dword ptr [ebp - 4], eax
0x46DA74: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46DA79: mov      eax, dword ptr [eax]
0x46DA7B: call     0x48ea88
0x46DA80: mov      eax, dword ptr [ebp - 8]
0x46DA83: mov      dword ptr [0x4f94dc], eax
0x46DA88: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46DA8D: mov      eax, dword ptr [eax]
0x46DA8F: mov      byte ptr [eax + 0x760], 0
0x46DA96: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46DA9B: mov      eax, dword ptr [eax]
0x46DA9D: call     0x48e934
0x46DAA2: pop      ecx
0x46DAA3: pop      ecx
0x46DAA4: pop      ebp
0x46DAA5: ret      
0x46DAA6: mov      eax, eax
0x46DAA8: push     ebp
0x46DAA9: mov      ebp, esp
0x46DAAB: add      esp, -8
0x46DAAE: mov      dword ptr [ebp - 8], edx
0x46DAB1: mov      dword ptr [ebp - 4], eax
0x46DAB4: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46DAB9: mov      eax, dword ptr [eax]
0x46DABB: call     0x48ea88
0x46DAC0: mov      eax, dword ptr [ebp - 8]
0x46DAC3: mov      dword ptr [0x4f94dc], eax
0x46DAC8: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46DACD: mov      eax, dword ptr [eax]
0x46DACF: mov      byte ptr [eax + 0x760], 1
0x46DAD6: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46DADB: mov      eax, dword ptr [eax]
0x46DADD: call     0x48e934
0x46DAE2: pop      ecx
0x46DAE3: pop      ecx
0x46DAE4: pop      ebp
0x46DAE5: ret      
0x46DAE6: mov      eax, eax
0x46DAE8: push     ebp
0x46DAE9: mov      ebp, esp
0x46DAEB: add      esp, -8
0x46DAEE: mov      dword ptr [ebp - 8], edx
0x46DAF1: mov      dword ptr [ebp - 4], eax
0x46DAF4: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46DAF9: mov      eax, dword ptr [eax]
0x46DAFB: call     0x48ea88
0x46DB00: mov      eax, dword ptr [ebp - 8]
0x46DB03: mov      dword ptr [0x4f94dc], eax
0x46DB08: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46DB0D: mov      eax, dword ptr [eax]
0x46DB0F: mov      byte ptr [eax + 0x760], 2
0x46DB16: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46DB1B: mov      eax, dword ptr [eax]
0x46DB1D: call     0x48e934
0x46DB22: pop      ecx
0x46DB23: pop      ecx
0x46DB24: pop      ebp
0x46DB25: ret      
0x46DB26: mov      eax, eax
0x46DB28: push     ebp
0x46DB29: mov      ebp, esp
0x46DB2B: add      esp, -0x2c
0x46DB2E: push     ebx
0x46DB2F: xor      ebx, ebx
0x46DB31: mov      dword ptr [ebp - 0x24], ebx
0x46DB34: mov      dword ptr [ebp - 0x14], ebx
0x46DB37: mov      dword ptr [ebp - 0xc], ecx
0x46DB3A: mov      dword ptr [ebp - 8], edx
0x46DB3D: mov      dword ptr [ebp - 4], eax
0x46DB40: mov      eax, dword ptr [ebp - 0xc]
0x46DB43: call     0x403e88
0x46DB48: xor      eax, eax
0x46DB4A: push     ebp
0x46DB4B: push     0x46dd1a
0x46DB50: push     dword ptr fs:[eax]
0x46DB53: mov      dword ptr fs:[eax], esp
0x46DB56: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46DB5B: mov      eax, dword ptr [eax]
0x46DB5D: call     0x48e750
0x46DB62: mov      eax, dword ptr [0x4f9178]
0x46DB67: mov      eax, dword ptr [eax + 8]
0x46DB6A: dec      eax
0x46DB6B: test     eax, eax
0x46DB6D: jl       0x46db99
0x46DB6F: inc      eax
0x46DB70: mov      dword ptr [ebp - 0x20], eax
0x46DB73: mov      dword ptr [ebp - 0x10], 0
0x46DB7A: mov      edx, dword ptr [ebp - 0x10]
0x46DB7D: mov      eax, dword ptr [0x4f9178]
0x46DB82: call     0x40f6a0
0x46DB87: mov      edx, 0x34
0x46DB8C: call     0x4026cc
0x46DB91: inc      dword ptr [ebp - 0x10]
0x46DB94: dec      dword ptr [ebp - 0x20]
0x46DB97: jne      0x46db7a
0x46DB99: mov      eax, dword ptr [0x4f9178]
0x46DB9E: mov      bx, 0xffff
0x46DBA2: call     0x403080
0x46DBA7: cmp      dword ptr [ebp - 0xc], 0
0x46DBAB: je       0x46dc02
0x46DBAD: push     0
0x46DBAF: lea      eax, [ebp - 0x24]
0x46DBB2: push     eax
0x46DBB3: mov      byte ptr [ebp - 0x28], 0x2f
0x46DBB7: lea      ecx, [ebp - 0x28]
0x46DBBA: lea      edx, [ebp - 0x14]
0x46DBBD: mov      eax, dword ptr [ebp - 0xc]
0x46DBC0: call     0x452530
0x46DBC5: mov      edx, dword ptr [ebp - 0x24]
0x46DBC8: lea      eax, [ebp - 0xc]
0x46DBCB: call     0x403af0
0x46DBD0: cmp      dword ptr [ebp - 0x14], 0
0x46DBD4: je       0x46dc02
0x46DBD6: mov      eax, 0x34
0x46DBDB: call     0x4026b4
0x46DBE0: mov      dword ptr [ebp - 0x18], eax
0x46DBE3: mov      edx, dword ptr [ebp - 0x18]
0x46DBE6: mov      ecx, 0x34
0x46DBEB: mov      eax, dword ptr [ebp - 0x14]
0x46DBEE: call     0x45b37c
0x46DBF3: mov      edx, dword ptr [ebp - 0x18]
0x46DBF6: mov      eax, dword ptr [0x4f9178]
0x46DBFB: call     0x40f588
0x46DC00: jmp      0x46dba7
0x46DC02: mov      eax, dword ptr [ebp - 8]
0x46DC05: mov      dword ptr [0x4f94dc], eax
0x46DC0A: mov      eax, dword ptr [0x4f9178]
0x46DC0F: mov      eax, dword ptr [eax + 8]
0x46DC12: dec      eax
0x46DC13: test     eax, eax
0x46DC15: jl       0x46dcda
0x46DC1B: inc      eax
0x46DC1C: mov      dword ptr [ebp - 0x20], eax
0x46DC1F: mov      dword ptr [ebp - 0x10], 0
0x46DC26: mov      eax, 0x1c
0x46DC2B: call     0x4026b4
0x46DC30: mov      dword ptr [ebp - 0x1c], eax
0x46DC33: mov      edx, dword ptr [ebp - 0x10]
0x46DC36: mov      eax, dword ptr [0x4f9178]
0x46DC3B: call     0x40f6a0
0x46DC40: mov      edx, eax
0x46DC42: mov      eax, dword ptr [ebp - 0x1c]
0x46DC45: mov      cl, 0xe
0x46DC47: call     0x402a30
0x46DC4C: mov      eax, dword ptr [ebp - 0x1c]
0x46DC4F: mov      byte ptr [eax + 0xf], 0
0x46DC53: mov      edx, dword ptr [ebp - 0x10]
0x46DC56: mov      eax, dword ptr [0x4f9178]
0x46DC5B: call     0x40f6a0
0x46DC60: mov      eax, dword ptr [eax + 0x2c]
0x46DC63: mov      edx, dword ptr [ebp - 0x1c]
0x46DC66: mov      dword ptr [edx + 0x10], eax
0x46DC69: mov      edx, dword ptr [ebp - 0x10]
0x46DC6C: mov      eax, dword ptr [0x4f9178]
0x46DC71: call     0x40f6a0
0x46DC76: movzx    eax, word ptr [eax + 0x30]
0x46DC7A: mov      dword ptr [ebp - 0x2c], eax
0x46DC7D: fild     dword ptr [ebp - 0x2c]
0x46DC80: fdiv     dword ptr [0x46dd28]
0x46DC86: call     0x4029a0
0x46DC8B: mov      edx, dword ptr [ebp - 0x1c]
0x46DC8E: mov      dword ptr [edx + 0x14], eax
0x46DC91: mov      edx, dword ptr [ebp - 0x10]
0x46DC94: mov      eax, dword ptr [0x4f9178]
0x46DC99: call     0x40f6a0
0x46DC9E: movzx    eax, word ptr [eax + 0x32]
0x46DCA2: mov      dword ptr [ebp - 0x2c], eax
0x46DCA5: fild     dword ptr [ebp - 0x2c]
0x46DCA8: fdiv     dword ptr [0x46dd28]
0x46DCAE: call     0x4029a0
0x46DCB3: mov      edx, dword ptr [ebp - 0x1c]
0x46DCB6: mov      byte ptr [edx + 0x18], al
0x46DCB9: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46DCBE: mov      eax, dword ptr [eax]
0x46DCC0: mov      eax, dword ptr [eax + 0x764]
0x46DCC6: mov      edx, dword ptr [ebp - 0x1c]
0x46DCC9: call     0x40f588
0x46DCCE: inc      dword ptr [ebp - 0x10]
0x46DCD1: dec      dword ptr [ebp - 0x20]
0x46DCD4: jne      0x46dc26
0x46DCDA: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46DCDF: mov      eax, dword ptr [eax]
0x46DCE1: call     0x48e85c
0x46DCE6: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46DCEB: mov      eax, dword ptr [eax]
0x46DCED: mov      byte ptr [eax + 0x775], 1
0x46DCF4: xor      eax, eax
0x46DCF6: pop      edx
0x46DCF7: pop      ecx
0x46DCF8: pop      ecx
0x46DCF9: mov      dword ptr fs:[eax], edx
0x46DCFC: push     0x46dd21
0x46DD01: lea      eax, [ebp - 0x24]
0x46DD04: call     0x403a58
0x46DD09: lea      eax, [ebp - 0x14]
0x46DD0C: call     0x403a58
0x46DD11: lea      eax, [ebp - 0xc]
0x46DD14: call     0x403a58
0x46DD19: ret      
0x46DD1A: jmp      0x403518
0x46DD1F: jmp      0x46dd01
0x46DD21: pop      ebx
0x46DD22: mov      esp, ebp
0x46DD24: pop      ebp
0x46DD25: ret      
0x46DD26: add      byte ptr [eax], al
0x46DD28: add      byte ptr [eax], al
0x46DD2A: jp       0x46dd70
0x46DD2C: push     ebp
0x46DD2D: mov      ebp, esp
0x46DD2F: add      esp, -0x40
0x46DD32: push     ebx
0x46DD33: xor      ebx, ebx
0x46DD35: mov      dword ptr [ebp - 0x38], ebx
0x46DD38: mov      dword ptr [ebp - 0x24], ebx
0x46DD3B: mov      dword ptr [ebp - 0x10], ebx
0x46DD3E: mov      dword ptr [ebp - 0x28], ebx
0x46DD41: mov      dword ptr [ebp - 0x2c], ebx
0x46DD44: mov      dword ptr [ebp - 0x30], ebx
0x46DD47: mov      dword ptr [ebp - 0x34], ebx
0x46DD4A: mov      dword ptr [ebp - 0x20], ecx
0x46DD4D: mov      dword ptr [ebp - 8], edx
0x46DD50: mov      dword ptr [ebp - 4], eax
0x46DD53: mov      eax, dword ptr [ebp + 8]
0x46DD56: call     0x403e88
0x46DD5B: xor      eax, eax
0x46DD5D: push     ebp
0x46DD5E: push     0x46df02
0x46DD63: push     dword ptr fs:[eax]
0x46DD66: mov      dword ptr fs:[eax], esp
0x46DD69: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46DD6E: mov      eax, dword ptr [eax]
0x46DD70: call     0x48e750
0x46DD75: mov      eax, dword ptr [ebp - 8]
0x46DD78: mov      dword ptr [0x4f94dc], eax
0x46DD7D: lea      edx, [ebp - 0x38]
0x46DD80: mov      eax, dword ptr [ebp + 8]
0x46DD83: call     0x45b2e0
0x46DD88: mov      edx, dword ptr [ebp - 0x38]
0x46DD8B: lea      eax, [ebp + 8]
0x46DD8E: call     0x403af0
0x46DD93: cmp      dword ptr [ebp + 8], 0
0x46DD97: je       0x46ddee
0x46DD99: push     0
0x46DD9B: lea      eax, [ebp - 0x38]
0x46DD9E: push     eax
0x46DD9F: mov      byte ptr [ebp - 0x3c], 0x2f
0x46DDA3: lea      ecx, [ebp - 0x3c]
0x46DDA6: lea      edx, [ebp - 0x10]
0x46DDA9: mov      eax, dword ptr [ebp + 8]
0x46DDAC: call     0x452530
0x46DDB1: mov      edx, dword ptr [ebp - 0x38]
0x46DDB4: lea      eax, [ebp + 8]
0x46DDB7: call     0x403af0
0x46DDBC: cmp      dword ptr [ebp - 0x10], 0
0x46DDC0: je       0x46ddee
0x46DDC2: mov      eax, 0x34
0x46DDC7: call     0x4026b4
0x46DDCC: mov      dword ptr [ebp - 0x18], eax
0x46DDCF: mov      edx, dword ptr [ebp - 0x18]
0x46DDD2: mov      ecx, 0x34
0x46DDD7: mov      eax, dword ptr [ebp - 0x10]
0x46DDDA: call     0x45b37c
0x46DDDF: mov      edx, dword ptr [ebp - 0x18]
0x46DDE2: mov      eax, dword ptr [0x4f917c]
0x46DDE7: call     0x40f588
0x46DDEC: jmp      0x46dd93
0x46DDEE: mov      eax, dword ptr [0x4f917c]
0x46DDF3: mov      eax, dword ptr [eax + 8]
0x46DDF6: dec      eax
0x46DDF7: test     eax, eax
0x46DDF9: jl       0x46dead
0x46DDFF: inc      eax
0x46DE00: mov      dword ptr [ebp - 0x1c], eax
0x46DE03: mov      dword ptr [ebp - 0xc], 0
0x46DE0A: mov      eax, 0x1c
0x46DE0F: call     0x4026b4
0x46DE14: mov      dword ptr [ebp - 0x14], eax
0x46DE17: mov      edx, dword ptr [ebp - 0xc]
0x46DE1A: mov      eax, dword ptr [0x4f917c]
0x46DE1F: call     0x40f6a0
0x46DE24: mov      edx, eax
0x46DE26: mov      eax, dword ptr [ebp - 0x14]
0x46DE29: mov      cl, 0xe
0x46DE2B: call     0x402a30
0x46DE30: mov      eax, dword ptr [ebp - 0x14]
0x46DE33: mov      byte ptr [eax + 0xf], 0
0x46DE37: mov      edx, dword ptr [ebp - 0xc]
0x46DE3A: mov      eax, dword ptr [0x4f917c]
0x46DE3F: call     0x40f6a0
0x46DE44: movzx    eax, word ptr [eax + 0x32]
0x46DE48: mov      edx, dword ptr [ebp - 0x14]
0x46DE4B: mov      dword ptr [edx + 0x10], eax
0x46DE4E: mov      edx, dword ptr [ebp - 0xc]
0x46DE51: mov      eax, dword ptr [0x4f917c]
0x46DE56: call     0x40f6a0
0x46DE5B: mov      eax, dword ptr [eax + 0x2c]
0x46DE5E: mov      edx, dword ptr [ebp - 0x14]
0x46DE61: mov      dword ptr [edx + 0x14], eax
0x46DE64: mov      edx, dword ptr [ebp - 0xc]
0x46DE67: mov      eax, dword ptr [0x4f917c]
0x46DE6C: call     0x40f6a0
0x46DE71: movzx    eax, word ptr [eax + 0x30]
0x46DE75: mov      dword ptr [ebp - 0x40], eax
0x46DE78: fild     dword ptr [ebp - 0x40]
0x46DE7B: fdiv     dword ptr [0x46df10]
0x46DE81: call     0x4029a0
0x46DE86: mov      edx, dword ptr [ebp - 0x14]
0x46DE89: mov      byte ptr [edx + 0x18], al
0x46DE8C: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46DE91: mov      eax, dword ptr [eax]
0x46DE93: mov      eax, dword ptr [eax + 0x764]
0x46DE99: mov      edx, dword ptr [ebp - 0x14]
0x46DE9C: call     0x40f588
0x46DEA1: inc      dword ptr [ebp - 0xc]
0x46DEA4: dec      dword ptr [ebp - 0x1c]
0x46DEA7: jne      0x46de0a
0x46DEAD: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46DEB2: mov      eax, dword ptr [eax]
0x46DEB4: call     0x48e85c
0x46DEB9: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46DEBE: mov      eax, dword ptr [eax]
0x46DEC0: mov      byte ptr [eax + 0x774], 1
0x46DEC7: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46DECC: mov      eax, dword ptr [eax]
0x46DECE: mov      edx, dword ptr [ebp + 0xc]
0x46DED1: mov      dword ptr [eax + 0x770], edx
0x46DED7: xor      eax, eax
0x46DED9: pop      edx
0x46DEDA: pop      ecx
0x46DEDB: pop      ecx
0x46DEDC: mov      dword ptr fs:[eax], edx
0x46DEDF: push     0x46df09
0x46DEE4: lea      eax, [ebp - 0x38]
0x46DEE7: mov      edx, 6
0x46DEEC: call     0x403a7c
0x46DEF1: lea      eax, [ebp - 0x10]
0x46DEF4: call     0x403a58
0x46DEF9: lea      eax, [ebp + 8]
0x46DEFC: call     0x403a58
0x46DF01: ret      
0x46DF02: jmp      0x403518
0x46DF07: jmp      0x46dee4
0x46DF09: pop      ebx
0x46DF0A: mov      esp, ebp
0x46DF0C: pop      ebp
0x46DF0D: ret      8
0x46DF10: add      byte ptr [eax], al
0x46DF12: jp       0x46df58
0x46DF14: push     ebp
0x46DF15: mov      ebp, esp
0x46DF17: add      esp, -0x18
0x46DF1A: xor      ecx, ecx
0x46DF1C: mov      dword ptr [ebp - 0x14], ecx
0x46DF1F: mov      dword ptr [ebp - 0xc], ecx
0x46DF22: mov      dword ptr [ebp - 0x10], ecx
0x46DF25: mov      dword ptr [ebp - 8], edx
0x46DF28: mov      dword ptr [ebp - 4], eax
0x46DF2B: mov      eax, dword ptr [ebp - 8]
0x46DF2E: call     0x403e88
0x46DF33: xor      eax, eax
0x46DF35: push     ebp
0x46DF36: push     0x46e003
0x46DF3B: push     dword ptr fs:[eax]
0x46DF3E: mov      dword ptr fs:[eax], esp
0x46DF41: mov      byte ptr [0x4f9470], 0
0x46DF48: lea      eax, [ebp - 0x10]
0x46DF4B: call     0x403a58
0x46DF50: lea      edx, [ebp - 0x14]
0x46DF53: mov      eax, dword ptr [ebp - 8]
0x46DF56: call     0x45b2e0
0x46DF5B: mov      edx, dword ptr [ebp - 0x14]
0x46DF5E: lea      eax, [ebp - 8]
0x46DF61: call     0x403af0
0x46DF66: cmp      dword ptr [ebp - 8], 0
0x46DF6A: je       0x46dfa9
0x46DF6C: push     0
0x46DF6E: lea      eax, [ebp - 0x14]
0x46DF71: push     eax
0x46DF72: mov      byte ptr [ebp - 0x18], 0x1b
0x46DF76: lea      ecx, [ebp - 0x18]
0x46DF79: lea      edx, [ebp - 0xc]
0x46DF7C: mov      eax, dword ptr [ebp - 8]
0x46DF7F: call     0x452530
0x46DF84: mov      edx, dword ptr [ebp - 0x14]
0x46DF87: lea      eax, [ebp - 8]
0x46DF8A: call     0x403af0
0x46DF8F: push     dword ptr [ebp - 0x10]
0x46DF92: push     dword ptr [ebp - 0xc]
0x46DF95: push     0x46e018 ; "\"
0x46DF9A: lea      eax, [ebp - 0x10]
0x46DF9D: mov      edx, 3
0x46DFA2: call     0x403d94
0x46DFA7: jmp      0x46df66
0x46DFA9: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46DFAE: mov      eax, dword ptr [eax]
0x46DFB0: mov      dword ptr [eax + 0x620], 2
0x46DFBA: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46DFBF: mov      eax, dword ptr [eax]
0x46DFC1: mov      cx, word ptr [0x46e01c]
0x46DFC8: mov      edx, dword ptr [ebp - 0x10]
0x46DFCB: call     0x486df0
0x46DFD0: dec      eax
0x46DFD1: jne      0x46dfe8
0x46DFD3: push     0
0x46DFD5: push     0
0x46DFD7: push     0
0x46DFD9: xor      ecx, ecx
0x46DFDB: mov      edx, 0x3fa
0x46DFE0: mov      eax, dword ptr [ebp - 4]
0x46DFE3: call     0x464b18
0x46DFE8: xor      eax, eax
0x46DFEA: pop      edx
0x46DFEB: pop      ecx
0x46DFEC: pop      ecx
0x46DFED: mov      dword ptr fs:[eax], edx
0x46DFF0: push     0x46e00a
0x46DFF5: lea      eax, [ebp - 0x14]
0x46DFF8: mov      edx, 4
0x46DFFD: call     0x403a7c
0x46E002: ret      
0x46E003: jmp      0x403518
0x46E008: jmp      0x46dff5
0x46E00A: mov      esp, ebp
0x46E00C: pop      ebp
0x46E00D: ret      
0x46E00E: add      byte ptr [eax], al
0x46E018: [STR len=1] "\"
0x46E01C: add      al, 0
0x46E01E: add      byte ptr [eax], al
0x46E020: push     ebp
0x46E021: mov      ebp, esp
0x46E023: add      esp, -0x14
0x46E026: xor      ecx, ecx
0x46E028: mov      dword ptr [ebp - 0x10], ecx
0x46E02B: mov      dword ptr [ebp - 0xc], ecx
0x46E02E: mov      dword ptr [ebp - 8], edx
0x46E031: mov      dword ptr [ebp - 4], eax
0x46E034: mov      eax, dword ptr [ebp - 8]
0x46E037: call     0x403e88
0x46E03C: xor      eax, eax
0x46E03E: push     ebp
0x46E03F: push     0x46e0ad
0x46E044: push     dword ptr fs:[eax]
0x46E047: mov      dword ptr fs:[eax], esp
0x46E04A: mov      eax, dword ptr [0x4f94fc]
0x46E04F: mov      edx, dword ptr [eax]
0x46E051: call     dword ptr [edx + 0x40]
0x46E054: cmp      dword ptr [ebp - 8], 0
0x46E058: je       0x46e092
0x46E05A: push     0
0x46E05C: lea      eax, [ebp - 0x10]
0x46E05F: push     eax
0x46E060: mov      byte ptr [ebp - 0x14], 0x2f
0x46E064: lea      ecx, [ebp - 0x14]
0x46E067: lea      edx, [ebp - 0xc]
0x46E06A: mov      eax, dword ptr [ebp - 8]
0x46E06D: call     0x452530
0x46E072: mov      edx, dword ptr [ebp - 0x10]
0x46E075: lea      eax, [ebp - 8]
0x46E078: call     0x403af0
0x46E07D: cmp      dword ptr [ebp - 0xc], 0
0x46E081: je       0x46e092
0x46E083: mov      edx, dword ptr [ebp - 0xc]
0x46E086: mov      eax, dword ptr [0x4f94fc]
0x46E08B: mov      ecx, dword ptr [eax]
0x46E08D: call     dword ptr [ecx + 0x34]
0x46E090: jmp      0x46e054
0x46E092: xor      eax, eax
0x46E094: pop      edx
0x46E095: pop      ecx
0x46E096: pop      ecx
0x46E097: mov      dword ptr fs:[eax], edx
0x46E09A: push     0x46e0b4
0x46E09F: lea      eax, [ebp - 0x10]
0x46E0A2: mov      edx, 3
0x46E0A7: call     0x403a7c
0x46E0AC: ret      
0x46E0AD: jmp      0x403518
0x46E0B2: jmp      0x46e09f
0x46E0B4: mov      esp, ebp
0x46E0B6: pop      ebp
0x46E0B7: ret      
0x46E0B8: push     ebp
0x46E0B9: mov      ebp, esp
0x46E0BB: mov      ecx, 4
0x46E0C0: push     0
0x46E0C2: push     0
0x46E0C4: dec      ecx
0x46E0C5: jne      0x46e0c0
0x46E0C7: push     ecx
0x46E0C8: push     ebx
0x46E0C9: mov      dword ptr [ebp - 8], edx
0x46E0CC: mov      dword ptr [ebp - 4], eax
0x46E0CF: mov      eax, dword ptr [ebp - 8]
0x46E0D2: call     0x403e88
0x46E0D7: xor      eax, eax
0x46E0D9: push     ebp
0x46E0DA: push     0x46e499
0x46E0DF: push     dword ptr fs:[eax]
0x46E0E2: mov      dword ptr fs:[eax], esp
0x46E0E5: lea      edx, [ebp - 0xc]
0x46E0E8: mov      eax, dword ptr [ebp - 8]
0x46E0EB: call     0x45b2e0
0x46E0F0: push     0
0x46E0F2: lea      eax, [ebp - 0x20]
0x46E0F5: push     eax
0x46E0F6: mov      byte ptr [ebp - 0x24], 0xd
0x46E0FA: lea      ecx, [ebp - 0x24]
0x46E0FD: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E102: mov      eax, dword ptr [eax]
0x46E104: lea      edx, [eax + 0x994]
0x46E10A: mov      eax, dword ptr [ebp - 0xc]
0x46E10D: call     0x452530
0x46E112: mov      edx, dword ptr [ebp - 0x20]
0x46E115: lea      eax, [ebp - 0xc]
0x46E118: call     0x403af0
0x46E11D: push     0
0x46E11F: lea      eax, [ebp - 0x20]
0x46E122: push     eax
0x46E123: mov      byte ptr [ebp - 0x24], 0xd
0x46E127: lea      ecx, [ebp - 0x24]
0x46E12A: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E12F: mov      eax, dword ptr [eax]
0x46E131: lea      edx, [eax + 0x998]
0x46E137: mov      eax, dword ptr [ebp - 0xc]
0x46E13A: call     0x452530
0x46E13F: mov      edx, dword ptr [ebp - 0x20]
0x46E142: lea      eax, [ebp - 0xc]
0x46E145: call     0x403af0
0x46E14A: push     0
0x46E14C: lea      eax, [ebp - 0x20]
0x46E14F: push     eax
0x46E150: mov      byte ptr [ebp - 0x24], 0xd
0x46E154: lea      ecx, [ebp - 0x24]
0x46E157: lea      edx, [ebp - 0x10]
0x46E15A: mov      eax, dword ptr [ebp - 0xc]
0x46E15D: call     0x452530
0x46E162: mov      edx, dword ptr [ebp - 0x20]
0x46E165: lea      eax, [ebp - 0xc]
0x46E168: call     0x403af0
0x46E16D: mov      eax, dword ptr [ebp - 0x10]
0x46E170: mov      edx, 0x46e4b0 ; "1"
0x46E175: call     0x403de4
0x46E17A: jne      0x46e18c
0x46E17C: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E181: mov      eax, dword ptr [eax]
0x46E183: mov      byte ptr [eax + 0x99c], 1
0x46E18A: jmp      0x46e19a
0x46E18C: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E191: mov      eax, dword ptr [eax]
0x46E193: mov      byte ptr [eax + 0x99c], 0
0x46E19A: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E19F: mov      eax, dword ptr [eax]
0x46E1A1: mov      eax, dword ptr [eax + 0x9a0]
0x46E1A7: mov      edx, dword ptr [eax]
0x46E1A9: call     dword ptr [edx + 0x40]
0x46E1AC: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E1B1: mov      eax, dword ptr [eax]
0x46E1B3: mov      eax, dword ptr [eax + 0x9a8]
0x46E1B9: mov      edx, dword ptr [eax]
0x46E1BB: call     dword ptr [edx + 0x40]
0x46E1BE: xor      eax, eax
0x46E1C0: mov      dword ptr [ebp - 0x1c], eax
0x46E1C3: cmp      dword ptr [ebp - 0xc], 0
0x46E1C7: je       0x46e44f
0x46E1CD: push     0
0x46E1CF: lea      eax, [ebp - 0x20]
0x46E1D2: push     eax
0x46E1D3: mov      byte ptr [ebp - 0x24], 0xd
0x46E1D7: lea      ecx, [ebp - 0x24]
0x46E1DA: lea      edx, [ebp - 0x10]
0x46E1DD: mov      eax, dword ptr [ebp - 0xc]
0x46E1E0: call     0x452530
0x46E1E5: mov      edx, dword ptr [ebp - 0x20]
0x46E1E8: lea      eax, [ebp - 0xc]
0x46E1EB: call     0x403af0
0x46E1F0: mov      eax, dword ptr [ebp - 0x10]
0x46E1F3: mov      edx, 0x46e4bc ; "<Notice>"
0x46E1F8: call     0x403de4
0x46E1FD: jne      0x46e23b
0x46E1FF: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E204: mov      eax, dword ptr [eax]
0x46E206: mov      eax, dword ptr [eax + 0x9a0]
0x46E20C: mov      ecx, 0xffffff
0x46E211: mov      edx, 0x46e4d0 ; "公告"
0x46E216: mov      ebx, dword ptr [eax]
0x46E218: call     dword ptr [ebx + 0x38]
0x46E21B: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E220: mov      eax, dword ptr [eax]
0x46E222: mov      eax, dword ptr [eax + 0x9a0]
0x46E228: mov      edx, 0x46e4e0 ; " "
0x46E22D: mov      ecx, dword ptr [eax]
0x46E22F: call     dword ptr [ecx + 0x34]
0x46E232: mov      dword ptr [ebp - 0x1c], 1
0x46E239: jmp      0x46e1c3
0x46E23B: mov      eax, dword ptr [ebp - 0x10]
0x46E23E: mov      edx, 0x46e4ec ; "<KillGuilds>"
0x46E243: call     0x403de4
0x46E248: jne      0x46e2a8
0x46E24A: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E24F: mov      eax, dword ptr [eax]
0x46E251: mov      eax, dword ptr [eax + 0x9a0]
0x46E257: mov      edx, 0x46e4e0 ; " "
0x46E25C: mov      ecx, dword ptr [eax]
0x46E25E: call     dword ptr [ecx + 0x34]
0x46E261: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E266: mov      eax, dword ptr [eax]
0x46E268: mov      eax, dword ptr [eax + 0x9a0]
0x46E26E: mov      ecx, 0xffffff
0x46E273: mov      edx, 0x46e504 ; "敌对行会"
0x46E278: mov      ebx, dword ptr [eax]
0x46E27A: call     dword ptr [ebx + 0x38]
0x46E27D: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E282: mov      eax, dword ptr [eax]
0x46E284: mov      eax, dword ptr [eax + 0x9a0]
0x46E28A: mov      edx, 0x46e4e0 ; " "
0x46E28F: mov      ecx, dword ptr [eax]
0x46E291: call     dword ptr [ecx + 0x34]
0x46E294: mov      dword ptr [ebp - 0x1c], 2
0x46E29B: lea      eax, [ebp - 0x14]
0x46E29E: call     0x403a58
0x46E2A3: jmp      0x46e1c3
0x46E2A8: mov      eax, dword ptr [ebp - 0x10]
0x46E2AB: mov      edx, 0x46e518 ; "<AllyGuilds>"
0x46E2B0: call     0x403de4
0x46E2B5: jne      0x46e330
0x46E2B7: cmp      dword ptr [ebp - 0x14], 0
0x46E2BB: je       0x46e2d2
0x46E2BD: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E2C2: mov      eax, dword ptr [eax]
0x46E2C4: mov      eax, dword ptr [eax + 0x9a0]
0x46E2CA: mov      edx, dword ptr [ebp - 0x14]
0x46E2CD: mov      ecx, dword ptr [eax]
0x46E2CF: call     dword ptr [ecx + 0x34]
0x46E2D2: lea      eax, [ebp - 0x14]
0x46E2D5: call     0x403a58
0x46E2DA: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E2DF: mov      eax, dword ptr [eax]
0x46E2E1: mov      eax, dword ptr [eax + 0x9a0]
0x46E2E7: mov      edx, 0x46e4e0 ; " "
0x46E2EC: mov      ecx, dword ptr [eax]
0x46E2EE: call     dword ptr [ecx + 0x34]
0x46E2F1: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E2F6: mov      eax, dword ptr [eax]
0x46E2F8: mov      eax, dword ptr [eax + 0x9a0]
0x46E2FE: mov      ecx, 0xffffff
0x46E303: mov      edx, 0x46e530 ; "联盟行会"
0x46E308: mov      ebx, dword ptr [eax]
0x46E30A: call     dword ptr [ebx + 0x38]
0x46E30D: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E312: mov      eax, dword ptr [eax]
0x46E314: mov      eax, dword ptr [eax + 0x9a0]
0x46E31A: mov      edx, 0x46e4e0 ; " "
0x46E31F: mov      ecx, dword ptr [eax]
0x46E321: call     dword ptr [ecx + 0x34]
0x46E324: mov      dword ptr [ebp - 0x1c], 3
0x46E32B: jmp      0x46e1c3
0x46E330: cmp      dword ptr [ebp - 0x1c], 1
0x46E334: jne      0x46e34b
0x46E336: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E33B: mov      eax, dword ptr [eax]
0x46E33D: mov      eax, dword ptr [eax + 0x9a8]
0x46E343: mov      edx, dword ptr [ebp - 0x10]
0x46E346: mov      ecx, dword ptr [eax]
0x46E348: call     dword ptr [ecx + 0x34]
0x46E34B: cmp      dword ptr [ebp - 0x10], 0
0x46E34F: je       0x46e3da
0x46E355: mov      eax, dword ptr [ebp - 0x10]
0x46E358: cmp      byte ptr [eax], 0x3c
0x46E35B: jne      0x46e3da
0x46E35D: lea      eax, [ebp - 0x18]
0x46E360: push     eax
0x46E361: lea      eax, [ebp - 0x20]
0x46E364: push     eax
0x46E365: mov      ecx, 0x46e544 ; ">"
0x46E36A: mov      edx, 0x46e550 ; "<"
0x46E36F: mov      eax, dword ptr [ebp - 0x10]
0x46E372: call     0x4529b8
0x46E377: cmp      dword ptr [ebp - 0x18], 0
0x46E37B: je       0x46e3da
0x46E37D: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E382: mov      eax, dword ptr [eax]
0x46E384: mov      eax, dword ptr [eax + 0x9a0]
0x46E38A: mov      edx, 0x46e4e0 ; " "
0x46E38F: mov      ecx, dword ptr [eax]
0x46E391: call     dword ptr [ecx + 0x34]
0x46E394: lea      eax, [ebp - 0x20]
0x46E397: mov      ecx, dword ptr [ebp - 0x18]
0x46E39A: mov      edx, 0x46e55c ; ""
0x46E39F: call     0x403d20
0x46E3A4: mov      edx, dword ptr [ebp - 0x20]
0x46E3A7: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E3AC: mov      eax, dword ptr [eax]
0x46E3AE: mov      eax, dword ptr [eax + 0x9a0]
0x46E3B4: mov      ecx, 0xffffff
0x46E3B9: mov      ebx, dword ptr [eax]
0x46E3BB: call     dword ptr [ebx + 0x38]
0x46E3BE: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E3C3: mov      eax, dword ptr [eax]
0x46E3C5: mov      eax, dword ptr [eax + 0x9a0]
0x46E3CB: mov      edx, 0x46e4e0 ; " "
0x46E3D0: mov      ecx, dword ptr [eax]
0x46E3D2: call     dword ptr [ecx + 0x34]
0x46E3D5: jmp      0x46e1c3
0x46E3DA: cmp      dword ptr [ebp - 0x1c], 2
0x46E3DE: je       0x46e3e6
0x46E3E0: cmp      dword ptr [ebp - 0x1c], 3
0x46E3E4: jne      0x46e435
0x46E3E6: mov      eax, dword ptr [ebp - 0x14]
0x46E3E9: call     0x403cd4
0x46E3EE: cmp      eax, 0x50
0x46E3F1: jle      0x46e415
0x46E3F3: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E3F8: mov      eax, dword ptr [eax]
0x46E3FA: mov      eax, dword ptr [eax + 0x9a0]
0x46E400: mov      edx, dword ptr [ebp - 0x14]
0x46E403: mov      ecx, dword ptr [eax]
0x46E405: call     dword ptr [ecx + 0x34]
0x46E408: lea      eax, [ebp - 0x14]
0x46E40B: call     0x403a58
0x46E410: jmp      0x46e1c3
0x46E415: lea      ecx, [ebp - 0x20]
0x46E418: mov      edx, 0x12
0x46E41D: mov      eax, dword ptr [ebp - 0x10]
0x46E420: call     0x49f570
0x46E425: mov      edx, dword ptr [ebp - 0x20]
0x46E428: lea      eax, [ebp - 0x14]
0x46E42B: call     0x403cdc
0x46E430: jmp      0x46e1c3
0x46E435: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E43A: mov      eax, dword ptr [eax]
0x46E43C: mov      eax, dword ptr [eax + 0x9a0]
0x46E442: mov      edx, dword ptr [ebp - 0x10]
0x46E445: mov      ecx, dword ptr [eax]
0x46E447: call     dword ptr [ecx + 0x34]
0x46E44A: jmp      0x46e1c3
0x46E44F: cmp      dword ptr [ebp - 0x14], 0
0x46E453: je       0x46e46a
0x46E455: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E45A: mov      eax, dword ptr [eax]
0x46E45C: mov      eax, dword ptr [eax + 0x9a0]
0x46E462: mov      edx, dword ptr [ebp - 0x14]
0x46E465: mov      ecx, dword ptr [eax]
0x46E467: call     dword ptr [ecx + 0x34]
0x46E46A: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E46F: mov      eax, dword ptr [eax]
0x46E471: call     0x4930a0
0x46E476: xor      eax, eax
0x46E478: pop      edx
0x46E479: pop      ecx
0x46E47A: pop      ecx
0x46E47B: mov      dword ptr fs:[eax], edx
0x46E47E: push     0x46e4a0
0x46E483: lea      eax, [ebp - 0x20]
0x46E486: call     0x403a58
0x46E48B: lea      eax, [ebp - 0x18]
0x46E48E: mov      edx, 5
0x46E493: call     0x403a7c
0x46E498: ret      
0x46E499: jmp      0x403518
0x46E49E: jmp      0x46e483
0x46E4A0: pop      ebx
0x46E4A1: mov      esp, ebp
0x46E4A3: pop      ebp
0x46E4A4: ret      
0x46E4A5: add      byte ptr [eax], al
0x46E4A7: add      bh, bh
0x46E4AB: inc      dword ptr [ecx]
0x46E4AD: add      byte ptr [eax], al
0x46E4AF: add      byte ptr [ecx], dh
0x46E4B1: add      byte ptr [eax], al
0x46E4B3: add      bh, bh
0x46E4B7: dec      dword ptr [eax]
0x46E4B9: add      byte ptr [eax], al
0x46E4BB: add      byte ptr [esi + ecx*2], bh
0x46E4BE: outsd    dx, dword ptr [esi]
0x46E4BF: je       0x46e52a
0x46E4C1: arpl     word ptr [ebp + 0x3e], sp
0x46E4C4: add      byte ptr [eax], al
0x46E4C6: add      byte ptr [eax], al
0x46E4D0: [STR len=5] "公告"
0x46E4E0: [STR len=1] " "
0x46E4EC: [STR len=12] "<KillGuilds>"
0x46E504: [STR len=9] "敌对行会"
0x46E518: [STR len=12] "<AllyGuilds>"
0x46E530: [STR len=9] "联盟行会"
0x46E544: [STR len=1] ">"
0x46E550: [STR len=1] "<"
0x46E55C: [STR len=1] ""
0x46E560: push     ebp
0x46E561: mov      ebp, esp
0x46E563: mov      ecx, 6
0x46E568: push     0
0x46E56A: push     0
0x46E56C: dec      ecx
0x46E56D: jne      0x46e568
0x46E56F: push     ebx
0x46E570: mov      dword ptr [ebp - 8], edx
0x46E573: mov      dword ptr [ebp - 4], eax
0x46E576: mov      eax, dword ptr [ebp - 8]
0x46E579: call     0x403e88
0x46E57E: xor      eax, eax
0x46E580: push     ebp
0x46E581: push     0x46e82b
0x46E586: push     dword ptr fs:[eax]
0x46E589: mov      dword ptr fs:[eax], esp
0x46E58C: lea      edx, [ebp - 0xc]
0x46E58F: mov      eax, dword ptr [ebp - 8]
0x46E592: call     0x45b2e0
0x46E597: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E59C: mov      eax, dword ptr [eax]
0x46E59E: mov      eax, dword ptr [eax + 0x9a0]
0x46E5A4: mov      edx, dword ptr [eax]
0x46E5A6: call     dword ptr [edx + 0x40]
0x46E5A9: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E5AE: mov      eax, dword ptr [eax]
0x46E5B0: mov      eax, dword ptr [eax + 0x9ac]
0x46E5B6: mov      edx, dword ptr [eax]
0x46E5B8: call     dword ptr [edx + 0x40]
0x46E5BB: xor      eax, eax
0x46E5BD: mov      dword ptr [ebp - 0x1c], eax
0x46E5C0: cmp      dword ptr [ebp - 0xc], 0
0x46E5C4: je       0x46e7e0
0x46E5CA: push     0
0x46E5CC: lea      eax, [ebp - 0x20]
0x46E5CF: push     eax
0x46E5D0: mov      byte ptr [ebp - 0x24], 0x2f
0x46E5D4: lea      ecx, [ebp - 0x24]
0x46E5D7: lea      edx, [ebp - 0x10]
0x46E5DA: mov      eax, dword ptr [ebp - 0xc]
0x46E5DD: call     0x452530
0x46E5E2: mov      edx, dword ptr [ebp - 0x20]
0x46E5E5: lea      eax, [ebp - 0xc]
0x46E5E8: call     0x403af0
0x46E5ED: cmp      dword ptr [ebp - 0x10], 0
0x46E5F1: je       0x46e5c0
0x46E5F3: mov      eax, dword ptr [ebp - 0x10]
0x46E5F6: cmp      byte ptr [eax], 0x23
0x46E5F9: jne      0x46e626
0x46E5FB: lea      eax, [ebp - 0x20]
0x46E5FE: push     eax
0x46E5FF: mov      eax, dword ptr [ebp - 0x10]
0x46E602: call     0x403cd4
0x46E607: mov      ecx, eax
0x46E609: dec      ecx
0x46E60A: mov      edx, 2
0x46E60F: mov      eax, dword ptr [ebp - 0x10]
0x46E612: call     0x403ed8
0x46E617: mov      eax, dword ptr [ebp - 0x20]
0x46E61A: xor      edx, edx
0x46E61C: call     0x4520e8
0x46E621: mov      dword ptr [ebp - 0x1c], eax
0x46E624: jmp      0x46e5c0
0x46E626: mov      eax, dword ptr [ebp - 0x10]
0x46E629: cmp      byte ptr [eax], 0x2a
0x46E62C: jne      0x46e781
0x46E632: cmp      dword ptr [ebp - 0x18], 0
0x46E636: je       0x46e64d
0x46E638: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E63D: mov      eax, dword ptr [eax]
0x46E63F: mov      eax, dword ptr [eax + 0x9a0]
0x46E645: mov      edx, dword ptr [ebp - 0x18]
0x46E648: mov      ecx, dword ptr [eax]
0x46E64A: call     dword ptr [ecx + 0x34]
0x46E64D: lea      eax, [ebp - 0x14]
0x46E650: push     eax
0x46E651: mov      eax, dword ptr [ebp - 0x10]
0x46E654: call     0x403cd4
0x46E659: mov      ecx, eax
0x46E65B: dec      ecx
0x46E65C: mov      edx, 2
0x46E661: mov      eax, dword ptr [ebp - 0x10]
0x46E664: call     0x403ed8
0x46E669: lea      eax, [ebp - 0x18]
0x46E66C: call     0x403a58
0x46E671: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E676: mov      eax, dword ptr [eax]
0x46E678: mov      eax, dword ptr [eax + 0x9a0]
0x46E67E: mov      edx, 0x46e840 ; " "
0x46E683: mov      ecx, dword ptr [eax]
0x46E685: call     dword ptr [ecx + 0x34]
0x46E688: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E68D: mov      eax, dword ptr [eax]
0x46E68F: cmp      byte ptr [eax + 0x99c], 0
0x46E696: je       0x46e706
0x46E698: push     0x46e84c ; "("
0x46E69D: lea      edx, [ebp - 0x30]
0x46E6A0: mov      eax, dword ptr [ebp - 0x1c]
0x46E6A3: call     0x407d64
0x46E6A8: push     dword ptr [ebp - 0x30]
0x46E6AB: push     0x46e858 ; ")"
0x46E6B0: lea      eax, [ebp - 0x2c]
0x46E6B3: mov      edx, 3
0x46E6B8: call     0x403d94
0x46E6BD: mov      eax, dword ptr [ebp - 0x2c]
0x46E6C0: lea      ecx, [ebp - 0x28]
0x46E6C3: mov      edx, 3
0x46E6C8: call     0x49f570
0x46E6CD: push     dword ptr [ebp - 0x28]
0x46E6D0: push     0x46e864 ; "<"
0x46E6D5: push     dword ptr [ebp - 0x14]
0x46E6D8: push     0x46e870 ; ">"
0x46E6DD: lea      eax, [ebp - 0x20]
0x46E6E0: mov      edx, 4
0x46E6E5: call     0x403d94
0x46E6EA: mov      edx, dword ptr [ebp - 0x20]
0x46E6ED: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E6F2: mov      eax, dword ptr [eax]
0x46E6F4: mov      eax, dword ptr [eax + 0x9a0]
0x46E6FA: mov      ecx, 0xffffff
0x46E6FF: mov      ebx, dword ptr [eax]
0x46E701: call     dword ptr [ebx + 0x38]
0x46E704: jmp      0x46e73a
0x46E706: push     0x46e864 ; "<"
0x46E70B: push     dword ptr [ebp - 0x14]
0x46E70E: push     0x46e870 ; ">"
0x46E713: lea      eax, [ebp - 0x20]
0x46E716: mov      edx, 3
0x46E71B: call     0x403d94
0x46E720: mov      edx, dword ptr [ebp - 0x20]
0x46E723: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E728: mov      eax, dword ptr [eax]
0x46E72A: mov      eax, dword ptr [eax + 0x9a0]
0x46E730: mov      ecx, 0xffffff
0x46E735: mov      ebx, dword ptr [eax]
0x46E737: call     dword ptr [ebx + 0x38]
0x46E73A: push     0x46e87c ; "#"
0x46E73F: lea      edx, [ebp - 0x28]
0x46E742: mov      eax, dword ptr [ebp - 0x1c]
0x46E745: call     0x407d64
0x46E74A: push     dword ptr [ebp - 0x28]
0x46E74D: push     0x46e888 ; " <"
0x46E752: push     dword ptr [ebp - 0x14]
0x46E755: push     0x46e870 ; ">"
0x46E75A: lea      eax, [ebp - 0x20]
0x46E75D: mov      edx, 5
0x46E762: call     0x403d94
0x46E767: mov      edx, dword ptr [ebp - 0x20]
0x46E76A: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E76F: mov      eax, dword ptr [eax]
0x46E771: mov      eax, dword ptr [eax + 0x9ac]
0x46E777: mov      ecx, dword ptr [eax]
0x46E779: call     dword ptr [ecx + 0x34]
0x46E77C: jmp      0x46e5c0
0x46E781: mov      eax, dword ptr [ebp - 0x18]
0x46E784: call     0x403cd4
0x46E789: cmp      eax, 0x50
0x46E78C: jle      0x46e7ab
0x46E78E: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E793: mov      eax, dword ptr [eax]
0x46E795: mov      eax, dword ptr [eax + 0x9a0]
0x46E79B: mov      edx, dword ptr [ebp - 0x18]
0x46E79E: mov      ecx, dword ptr [eax]
0x46E7A0: call     dword ptr [ecx + 0x34]
0x46E7A3: lea      eax, [ebp - 0x18]
0x46E7A6: call     0x403a58
0x46E7AB: lea      ecx, [ebp - 0x20]
0x46E7AE: mov      edx, 0x12
0x46E7B3: mov      eax, dword ptr [ebp - 0x10]
0x46E7B6: call     0x49f570
0x46E7BB: mov      edx, dword ptr [ebp - 0x20]
0x46E7BE: lea      eax, [ebp - 0x18]
0x46E7C1: call     0x403cdc
0x46E7C6: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E7CB: mov      eax, dword ptr [eax]
0x46E7CD: mov      eax, dword ptr [eax + 0x9ac]
0x46E7D3: mov      edx, dword ptr [ebp - 0x10]
0x46E7D6: mov      ecx, dword ptr [eax]
0x46E7D8: call     dword ptr [ecx + 0x34]
0x46E7DB: jmp      0x46e5c0
0x46E7E0: cmp      dword ptr [ebp - 0x18], 0
0x46E7E4: je       0x46e7fb
0x46E7E6: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46E7EB: mov      eax, dword ptr [eax]
0x46E7ED: mov      eax, dword ptr [eax + 0x9a0]
0x46E7F3: mov      edx, dword ptr [ebp - 0x18]
0x46E7F6: mov      ecx, dword ptr [eax]
0x46E7F8: call     dword ptr [ecx + 0x34]
0x46E7FB: xor      eax, eax
0x46E7FD: pop      edx
0x46E7FE: pop      ecx
0x46E7FF: pop      ecx
0x46E800: mov      dword ptr fs:[eax], edx
0x46E803: push     0x46e832
0x46E808: lea      eax, [ebp - 0x30]
0x46E80B: mov      edx, 3
0x46E810: call     0x403a7c
0x46E815: lea      eax, [ebp - 0x20]
0x46E818: call     0x403a58
0x46E81D: lea      eax, [ebp - 0x18]
0x46E820: mov      edx, 5
0x46E825: call     0x403a7c
0x46E82A: ret      
0x46E82B: jmp      0x403518
0x46E830: jmp      0x46e808
0x46E832: pop      ebx
0x46E833: mov      esp, ebp
0x46E835: pop      ebp
0x46E836: ret      
0x46E837: add      bh, bh
0x46E83B: inc      dword ptr [ecx]
0x46E83D: add      byte ptr [eax], al
0x46E83F: add      byte ptr [eax], ah
0x46E841: add      byte ptr [eax], al
0x46E843: add      bh, bh
0x46E847: inc      dword ptr [ecx]
0x46E849: add      byte ptr [eax], al
0x46E84B: add      byte ptr [eax], ch
0x46E84D: add      byte ptr [eax], al
0x46E84F: add      bh, bh
0x46E853: inc      dword ptr [ecx]
0x46E855: add      byte ptr [eax], al
0x46E857: add      byte ptr [ecx], ch
0x46E859: add      byte ptr [eax], al
0x46E85B: add      bh, bh
0x46E85F: inc      dword ptr [ecx]
0x46E861: add      byte ptr [eax], al
0x46E863: add      byte ptr [eax + eax], bh
0x46E866: add      byte ptr [eax], al
0x46E870: [STR len=1] ">"
0x46E87C: [STR len=1] "#"
0x46E888: [STR len=2] " <"
0x46E88C: push     ebp
0x46E88D: mov      ebp, esp
0x46E88F: add      esp, -0x14
0x46E892: mov      dword ptr [ebp - 0x10], edx
0x46E895: mov      dword ptr [ebp - 4], eax
0x46E898: mov      eax, dword ptr [0x4f7d9c]
0x46E89D: mov      eax, dword ptr [eax + 0x5a854]
0x46E8A3: mov      eax, dword ptr [eax + 8]
0x46E8A6: dec      eax
0x46E8A7: test     eax, eax
0x46E8A9: jl       0x46e916
0x46E8AB: inc      eax
0x46E8AC: mov      dword ptr [ebp - 0xc], eax
0x46E8AF: mov      dword ptr [ebp - 8], 0
0x46E8B6: mov      eax, dword ptr [0x4f7d9c]
0x46E8BB: mov      eax, dword ptr [eax + 0x5a854]
0x46E8C1: mov      edx, dword ptr [ebp - 8]
0x46E8C4: call     0x40f6a0
0x46E8C9: mov      edx, dword ptr [eax + 0x28]
0x46E8CC: mov      eax, dword ptr [ebp - 4]
0x46E8CF: call     0x467110
0x46E8D4: test     al, al
0x46E8D6: je       0x46e8f4
0x46E8D8: mov      eax, dword ptr [0x4f7d9c]
0x46E8DD: mov      eax, dword ptr [eax + 0x5a854]
0x46E8E3: mov      edx, dword ptr [ebp - 8]
0x46E8E6: call     0x40f6a0
0x46E8EB: mov      byte ptr [eax + 0x144], 1
0x46E8F2: jmp      0x46e90e
0x46E8F4: mov      eax, dword ptr [0x4f7d9c]
0x46E8F9: mov      eax, dword ptr [eax + 0x5a854]
0x46E8FF: mov      edx, dword ptr [ebp - 8]
0x46E902: call     0x40f6a0
0x46E907: mov      byte ptr [eax + 0x144], 0
0x46E90E: inc      dword ptr [ebp - 8]
0x46E911: dec      dword ptr [ebp - 0xc]
0x46E914: jne      0x46e8b6
0x46E916: mov      eax, dword ptr [0x4f9230]
0x46E91B: mov      eax, dword ptr [eax + 8]
0x46E91E: dec      eax
0x46E91F: cmp      eax, 0
0x46E922: jl       0x46e971
0x46E924: mov      dword ptr [ebp - 8], eax
0x46E927: mov      edx, dword ptr [ebp - 8]
0x46E92A: mov      eax, dword ptr [0x4f9230]
0x46E92F: call     0x40f6a0
0x46E934: push     dword ptr [eax + 0x170]
0x46E93A: call     0x406154
0x46E93F: pop      edx
0x46E940: sub      eax, edx
0x46E942: cmp      eax, 0xea60
0x46E947: jbe      0x46e968
0x46E949: mov      edx, dword ptr [ebp - 8]
0x46E94C: mov      eax, dword ptr [0x4f9230]
0x46E951: call     0x40f6a0
0x46E956: call     0x402eac
0x46E95B: mov      edx, dword ptr [ebp - 8]
0x46E95E: mov      eax, dword ptr [0x4f9230]
0x46E963: call     0x40f5c4
0x46E968: dec      dword ptr [ebp - 8]
0x46E96B: cmp      dword ptr [ebp - 8], -1
0x46E96F: jne      0x46e927
0x46E971: mov      esp, ebp
0x46E973: pop      ebp
0x46E974: ret      
0x46E975: lea      eax, [eax]
0x46E978: push     ebp
0x46E979: mov      ebp, esp
0x46E97B: add      esp, -0x18
0x46E97E: mov      dword ptr [ebp - 8], edx
0x46E981: mov      dword ptr [ebp - 4], eax
0x46E984: mov      esp, ebp
0x46E986: pop      ebp
0x46E987: ret      
0x46E988: push     ebp
0x46E989: mov      ebp, esp
0x46E98B: add      esp, -0x3c
0x46E98E: mov      dword ptr [ebp - 8], edx
0x46E991: mov      dword ptr [ebp - 4], eax
0x46E994: mov      eax, dword ptr [ebp - 8]
0x46E997: call     0x403e88
0x46E99C: xor      eax, eax
0x46E99E: push     ebp
0x46E99F: push     0x46e9de
0x46E9A4: push     dword ptr fs:[eax]
0x46E9A7: mov      dword ptr fs:[eax], esp
0x46E9AA: cmp      dword ptr [ebp - 8], 0
0x46E9AE: je       0x46e9c8
0x46E9B0: lea      edx, [ebp - 0x3c]
0x46E9B3: mov      ecx, 0x34
0x46E9B8: mov      eax, dword ptr [ebp - 8]
0x46E9BB: call     0x45b37c
0x46E9C0: lea      eax, [ebp - 0x3c]
0x46E9C3: call     0x49fe80
0x46E9C8: xor      eax, eax
0x46E9CA: pop      edx
0x46E9CB: pop      ecx
0x46E9CC: pop      ecx
0x46E9CD: mov      dword ptr fs:[eax], edx
0x46E9D0: push     0x46e9e5
0x46E9D5: lea      eax, [ebp - 8]
0x46E9D8: call     0x403a58
0x46E9DD: ret      
0x46E9DE: jmp      0x403518
0x46E9E3: jmp      0x46e9d5
0x46E9E5: mov      esp, ebp
0x46E9E7: pop      ebp
0x46E9E8: ret      
0x46E9E9: lea      eax, [eax]
0x46E9EC: push     ebp
0x46E9ED: mov      ebp, esp
0x46E9EF: add      esp, -0x3c
0x46E9F2: mov      dword ptr [ebp - 8], edx
0x46E9F5: mov      dword ptr [ebp - 4], eax
0x46E9F8: mov      eax, dword ptr [ebp - 8]
0x46E9FB: call     0x403e88
0x46EA00: xor      eax, eax
0x46EA02: push     ebp
0x46EA03: push     0x46ea42
0x46EA08: push     dword ptr fs:[eax]
0x46EA0B: mov      dword ptr fs:[eax], esp
0x46EA0E: cmp      dword ptr [ebp - 8], 0
0x46EA12: je       0x46ea2c
0x46EA14: lea      edx, [ebp - 0x3c]
0x46EA17: mov      ecx, 0x34
0x46EA1C: mov      eax, dword ptr [ebp - 8]
0x46EA1F: call     0x45b37c
0x46EA24: lea      eax, [ebp - 0x3c]
0x46EA27: call     0x49fed4
0x46EA2C: xor      eax, eax
0x46EA2E: pop      edx
0x46EA2F: pop      ecx
0x46EA30: pop      ecx
0x46EA31: mov      dword ptr fs:[eax], edx
0x46EA34: push     0x46ea49
0x46EA39: lea      eax, [ebp - 8]
0x46EA3C: call     0x403a58
0x46EA41: ret      
0x46EA42: jmp      0x403518
0x46EA47: jmp      0x46ea39
0x46EA49: mov      esp, ebp
0x46EA4B: pop      ebp
0x46EA4C: ret      
0x46EA4D: lea      eax, [eax]
0x46EA50: push     ebp
0x46EA51: mov      ebp, esp
0x46EA53: add      esp, -8
0x46EA56: mov      dword ptr [ebp - 8], edx
0x46EA59: mov      dword ptr [ebp - 4], eax
0x46EA5C: cmp      dword ptr [ebp - 8], 1
0x46EA60: jl       0x46ea88
0x46EA62: cmp      dword ptr [0x4f9484], 1
0x46EA69: jge      0x46ea75
0x46EA6B: mov      dword ptr [0x4f9484], 1
0x46EA75: mov      eax, dword ptr [0x4f9484]
0x46EA7A: mov      dword ptr [0x4f9480], eax
0x46EA7F: mov      eax, dword ptr [ebp - 8]
0x46EA82: dec      eax
0x46EA83: mov      dword ptr [0x4f9488], eax
0x46EA88: pop      ecx
0x46EA89: pop      ecx
0x46EA8A: pop      ebp
0x46EA8B: ret      
0x46EA8C: push     ebp
0x46EA8D: mov      ebp, esp
0x46EA8F: add      esp, -0x14
0x46EA92: xor      ecx, ecx
0x46EA94: mov      dword ptr [ebp - 0x14], ecx
0x46EA97: mov      dword ptr [ebp - 0xc], ecx
0x46EA9A: mov      dword ptr [ebp - 8], edx
0x46EA9D: mov      dword ptr [ebp - 4], eax
0x46EAA0: mov      eax, dword ptr [ebp - 8]
0x46EAA3: call     0x403e88
0x46EAA8: xor      eax, eax
0x46EAAA: push     ebp
0x46EAAB: push     0x46eb0b
0x46EAB0: push     dword ptr fs:[eax]
0x46EAB3: mov      dword ptr fs:[eax], esp
0x46EAB6: push     0
0x46EAB8: lea      eax, [ebp - 0xc]
0x46EABB: push     eax
0x46EABC: mov      byte ptr [ebp - 0x10], 0x2f
0x46EAC0: lea      ecx, [ebp - 0x10]
0x46EAC3: mov      edx, 0x4f7eec
0x46EAC8: mov      eax, dword ptr [ebp - 8]
0x46EACB: call     0x452530
0x46EAD0: lea      edx, [ebp - 0x14]
0x46EAD3: mov      eax, dword ptr [ebp - 0xc]
0x46EAD6: call     0x407be4
0x46EADB: mov      edx, dword ptr [ebp - 0x14]
0x46EADE: mov      eax, 0x4f7ef0
0x46EAE3: call     0x403aac
0x46EAE8: xor      eax, eax
0x46EAEA: pop      edx
0x46EAEB: pop      ecx
0x46EAEC: pop      ecx
0x46EAED: mov      dword ptr fs:[eax], edx
0x46EAF0: push     0x46eb12
0x46EAF5: lea      eax, [ebp - 0x14]
0x46EAF8: call     0x403a58
0x46EAFD: lea      eax, [ebp - 0xc]
0x46EB00: mov      edx, 2
0x46EB05: call     0x403a7c
0x46EB0A: ret      
0x46EB0B: jmp      0x403518
0x46EB10: jmp      0x46eaf5
0x46EB12: mov      esp, ebp
0x46EB14: pop      ebp
0x46EB15: ret      
0x46EB16: mov      eax, eax
0x46EB18: push     ebp
0x46EB19: mov      ebp, esp
0x46EB1B: add      esp, 0xfffffdec
0x46EB21: mov      dword ptr [ebp - 8], edx
0x46EB24: mov      dword ptr [ebp - 4], eax
0x46EB27: mov      eax, dword ptr [ebp - 8]
0x46EB2A: call     0x403e88
0x46EB2F: xor      eax, eax
0x46EB31: push     ebp
0x46EB32: push     0x46eb89
0x46EB37: push     dword ptr fs:[eax]
0x46EB3A: mov      dword ptr fs:[eax], esp
0x46EB3D: lea      edx, [ebp - 0x214]
0x46EB43: mov      ecx, 0x20c
0x46EB48: mov      eax, dword ptr [ebp - 8]
0x46EB4B: call     0x45b37c
0x46EB50: mov      al, byte ptr [ebp - 0x200]
0x46EB56: call     0x45edc8
0x46EB5B: mov      dword ptr [ebp - 0x200], eax
0x46EB61: mov      eax, dword ptr [0x4a4310] ; *0x4A4310->0x4F95CC
0x46EB66: mov      eax, dword ptr [eax]
0x46EB68: lea      edx, [ebp - 0x214]
0x46EB6E: call     0x4864b0
0x46EB73: xor      eax, eax
0x46EB75: pop      edx
0x46EB76: pop      ecx
0x46EB77: pop      ecx
0x46EB78: mov      dword ptr fs:[eax], edx
0x46EB7B: push     0x46eb90
0x46EB80: lea      eax, [ebp - 8]
0x46EB83: call     0x403a58
0x46EB88: ret      
0x46EB89: jmp      0x403518
0x46EB8E: jmp      0x46eb80
0x46EB90: mov      esp, ebp
0x46EB92: pop      ebp
0x46EB93: ret      
0x46EB94: push     ebp
0x46EB95: mov      ebp, esp
0x46EB97: add      esp, -0xc
0x46EB9A: mov      dword ptr [ebp - 8], edx
0x46EB9D: mov      dword ptr [ebp - 4], eax
0x46EBA0: mov      esp, ebp
0x46EBA2: pop      ebp
0x46EBA3: ret      
0x46EBA4: push     ebp
0x46EBA5: mov      ebp, esp
0x46EBA7: xor      eax, eax
0x46EBA9: push     ebp
0x46EBAA: push     0x46ecc9
0x46EBAF: push     dword ptr fs:[eax]
0x46EBB2: mov      dword ptr fs:[eax], esp
0x46EBB5: inc      dword ptr [0x4f95b0]
0x46EBBB: jne      0x46ecbb
0x46EBC1: mov      eax, 0x4f94cc
0x46EBC6: call     0x403a58
0x46EBCB: mov      eax, 0x4f9378
0x46EBD0: call     0x403a58
0x46EBD5: mov      eax, 0x4f918c
0x46EBDA: call     0x403a58
0x46EBDF: mov      eax, 0x4f7ef0
0x46EBE4: call     0x403a58
0x46EBE9: mov      eax, 0x4f7eec
0x46EBEE: call     0x403a58
0x46EBF3: mov      eax, 0x4f7ee8
0x46EBF8: call     0x403a58
0x46EBFD: mov      eax, 0x4f7ee4
0x46EC02: call     0x403a58
0x46EC07: mov      eax, 0x4f7ed4
0x46EC0C: mov      edx, dword ptr [0x45e3ec]
0x46EC12: call     0x4042b8
0x46EC17: mov      eax, 0x4f7e54
0x46EC1C: mov      ecx, 0x20
0x46EC21: mov      edx, dword ptr [0x401094]
0x46EC27: call     0x4042e4
0x46EC2C: mov      eax, 0x4f7dd4
0x46EC31: mov      ecx, 0x20
0x46EC36: mov      edx, dword ptr [0x401094]
0x46EC3C: call     0x4042e4
0x46EC41: mov      eax, 0x4f7dc4
0x46EC46: call     0x403a58
0x46EC4B: mov      eax, 0x4f7dc0
0x46EC50: call     0x403a58
0x46EC55: mov      eax, 0x4f7dbc
0x46EC5A: call     0x403a58
0x46EC5F: mov      eax, 0x4f7db8
0x46EC64: call     0x403a58
0x46EC69: mov      eax, 0x4f7db4
0x46EC6E: call     0x403a58
0x46EC73: mov      eax, 0x4f7db0
0x46EC78: call     0x403a58
0x46EC7D: mov      eax, 0x4a2d0c ; *0x4A2D0C->0x45E3E4
0x46EC82: call     0x403a58
0x46EC87: mov      eax, 0x4a2cf8 ; *0x4A2CF8->0x45E388
0x46EC8C: mov      ecx, 4
0x46EC91: mov      edx, dword ptr [0x401094]
0x46EC97: call     0x4042e4
0x46EC9C: mov      eax, 0x4a2ce8 ; *0x4A2CE8->0x45E348
0x46ECA1: mov      ecx, 4
0x46ECA6: mov      edx, dword ptr [0x401094]
0x46ECAC: call     0x4042e4
0x46ECB1: mov      eax, 0x4a2ce4 ; *0x4A2CE4->0x45E330
0x46ECB6: call     0x403a58
0x46ECBB: xor      eax, eax
0x46ECBD: pop      edx
0x46ECBE: pop      ecx
0x46ECBF: pop      ecx
0x46ECC0: mov      dword ptr fs:[eax], edx
0x46ECC3: push     0x46ecd0
0x46ECC8: ret      
0x46ECC9: jmp      0x403518
0x46ECCE: jmp      0x46ecc8
0x46ECD0: pop      ebp
0x46ECD1: ret      
0x46ECD2: mov      eax, eax
0x46ECD4: sub      dword ptr [0x4f95b0], 1
0x46ECDB: ret      
0x46ECE4: [STR len=14] "Data\lig0a.dat"
0x46ECFC: [STR len=14] "Data\lig0b.dat"
0x46ED14: [STR len=14] "Data\lig0c.dat"
0x46ED2C: [STR len=14] "Data\lig0d.dat"
0x46ED44: [STR len=14] "Data\lig0e.dat"
0x46ED5C: [STR len=14] "Data\lig0f.dat"
0x46ED6C: mov      eax, 0x46ed
0x46ED71: add      byte ptr [eax], al
0x46ED73: add      byte ptr [eax], al
0x46ED75: add      byte ptr [eax], al
0x46ED77: add      byte ptr [eax], al
0x46ED79: add      byte ptr [eax], al
0x46ED7B: add      byte ptr [eax], al
0x46ED7D: add      byte ptr [eax], al
0x46ED7F: add      byte ptr [eax], al
0x46ED81: add      byte ptr [eax], al
0x46ED83: add      byte ptr [eax], al
0x46ED85: add      byte ptr [eax], al
0x46ED87: add      byte ptr [eax - 0x21ffb913], bh
0x46ED8D: in       eax, dx
0x46ED8E: inc      esi
0x46ED8F: add      byte ptr [eax - 0x58], dh
0x46ED92: add      eax, 0x47ab2c00
0x46ED97: add      al, cl
0x46ED99: xor      byte ptr [eax], al
0x46ED9C: aam      0x30
0x46ED9E: inc      eax
0x46ED9F: add      al, bl
0x46EDA1: xor      byte ptr [eax], al
0x46EDA4: fdiv     qword ptr [eax]
0x46EDA6: inc      eax
0x46EDA7: add      al, dl
0x46EDA9: xor      byte ptr [eax], al
0x46EDAC: inc      eax
0x46EDAD: inc      eax
0x46EDAF: add      byte ptr [esi + ebp + 0x40], dl
0x46EDB3: add      byte ptr [eax + esi*8], cl
0x46EDB6: inc      esi
