
; === VMT_TClEvent @ 0x47733C ===
0x47733C: add      al, byte ptr [eax]
0x477341: push     dword ptr [eax + 0x74]
0x477344: inc      edi
0x477345: add      ah, dh
0x477347: je       0x477390
0x477349: add      byte ptr [eax], cl
0x47734B: push     esp
0x47734C: inc      ebx
0x47734D: insb     byte ptr es:[edi], dx
0x47734E: inc      ebp
0x47734F: jbe      0x4773b6
0x477351: outsb    dx, byte ptr [esi]
0x477352: je       0x4772e4
0x477354: mov      al, byte ptr [0x4773]
0x477359: add      byte ptr [eax], al
0x47735B: add      byte ptr [eax], al
0x47735D: add      byte ptr [eax], al
0x47735F: add      byte ptr [eax], al
0x477361: add      byte ptr [eax], al
0x477363: add      byte ptr [eax], al
0x477365: add      byte ptr [eax], al
0x477367: add      byte ptr [eax], al
0x477369: add      byte ptr [eax], al
0x47736B: add      byte ptr [eax], al
0x47736D: add      byte ptr [eax], al
0x47736F: add      byte ptr [eax], al
0x477371: add      byte ptr [eax], al
0x477373: add      byte ptr [eax + 0x8004773], ah
0x477379: add      byte ptr [eax], al
0x47737B: add      byte ptr [eax - 0x37ffbff0], ah
0x477381: xor      byte ptr [eax], al
0x477384: aam      0x30
0x477386: inc      eax
0x477387: add      al, bl
0x477389: xor      byte ptr [eax], al
0x47738C: fdiv     qword ptr [eax]
0x47738E: inc      eax
0x47738F: add      al, dl
0x477391: xor      byte ptr [eax], al
0x477394: inc      eax
0x477395: inc      eax
0x477397: add      byte ptr [esi + ebp + 0x40], dl
0x47739B: add      byte ptr [edi + esi*2], ch
0x47739E: inc      edi
0x47739F: add      byte ptr [edi], cl
0x4773A1: push     esp
0x4773A2: inc      ebx
0x4773A3: insb     byte ptr es:[edi], dx
0x4773A4: inc      ebp
0x4773A5: jbe      0x47740c
0x4773A7: outsb    dx, byte ptr [esi]
0x4773A8: je       0x4773f7
0x4773AA: popal    
0x4773AB: outsb    dx, byte ptr [esi]
0x4773AC: popal    
0x4773AD: jb       0x477406
0x4773B1: mov      ebp, esp
0x4773B3: add      esp, -0xc
0x4773B6: test     dl, dl
0x4773B8: je       0x4773c2
0x4773BA: add      esp, -0x10
0x4773BD: call     0x4031a4
0x4773C2: mov      dword ptr [ebp - 0xc], ecx
0x4773C5: mov      byte ptr [ebp - 5], dl
0x4773C8: mov      dword ptr [ebp - 4], eax
0x4773CB: mov      eax, dword ptr [ebp - 0xc]
0x4773CE: mov      edx, dword ptr [ebp - 4]
0x4773D1: mov      dword ptr [edx + 0x20], eax
0x4773D4: mov      eax, dword ptr [ebp + 0x10]
0x4773D7: mov      edx, dword ptr [ebp - 4]
0x4773DA: mov      dword ptr [edx + 4], eax
0x4773DD: mov      eax, dword ptr [ebp + 0xc]
0x4773E0: mov      edx, dword ptr [ebp - 4]
0x4773E3: mov      dword ptr [edx + 8], eax
0x4773E6: mov      eax, dword ptr [ebp + 8]
0x4773E9: mov      edx, dword ptr [ebp - 4]
0x4773EC: mov      dword ptr [edx + 0x18], eax
0x4773EF: mov      eax, dword ptr [ebp - 4]
0x4773F2: xor      edx, edx
0x4773F4: mov      dword ptr [eax + 0x1c], edx
0x4773F7: mov      eax, dword ptr [ebp - 4]
0x4773FA: mov      byte ptr [eax + 0x28], 0
0x4773FE: call     0x406154
0x477403: mov      edx, dword ptr [ebp - 4]
0x477406: mov      dword ptr [edx + 0x2c], eax
0x477409: mov      eax, dword ptr [ebp - 4]
0x47740C: xor      edx, edx
0x47740E: mov      dword ptr [eax + 0x30], edx
0x477411: mov      eax, dword ptr [ebp - 4]
0x477414: xor      edx, edx
0x477416: mov      dword ptr [eax + 0x34], edx
0x477419: mov      eax, dword ptr [ebp - 4]
0x47741C: cmp      byte ptr [ebp - 5], 0
0x477420: je       0x477431
0x477422: call     0x4031fc
0x477427: pop      dword ptr fs:[0]
0x47742E: add      esp, 0xc
0x477431: mov      eax, dword ptr [ebp - 4]
0x477434: mov      esp, ebp
0x477436: pop      ebp
0x477437: ret      0xc
0x47743A: mov      eax, eax
0x47743C: push     ebp
0x47743D: mov      ebp, esp
0x47743F: add      esp, -8
0x477442: call     0x403204
0x477447: mov      byte ptr [ebp - 5], dl
0x47744A: mov      dword ptr [ebp - 4], eax
0x47744D: mov      dl, byte ptr [ebp - 5]
0x477450: and      dl, 0xfc
0x477453: mov      eax, dword ptr [ebp - 4]
0x477456: call     0x402e9c
0x47745B: cmp      byte ptr [ebp - 5], 0
0x47745F: jle      0x477469
0x477461: mov      eax, dword ptr [ebp - 4]
0x477464: call     0x4031f4
0x477469: pop      ecx
0x47746A: pop      ecx
0x47746B: pop      ebp
0x47746C: ret      
0x47746D: lea      eax, [eax]
0x477470: push     ebp
0x477471: mov      ebp, esp
0x477473: add      esp, -0x1c
0x477476: mov      dword ptr [ebp - 0xc], ecx
0x477479: mov      dword ptr [ebp - 8], edx
0x47747C: mov      dword ptr [ebp - 4], eax
0x47747F: mov      eax, dword ptr [ebp - 4]
0x477482: cmp      dword ptr [eax + 0x24], 0
0x477486: je       0x4774eb
0x477488: mov      eax, dword ptr [ebp - 4]
0x47748B: cmp      byte ptr [eax + 0x28], 0
0x47748F: je       0x4774b6
0x477491: mov      eax, dword ptr [ebp - 4]
0x477494: mov      eax, dword ptr [eax + 0x24]
0x477497: push     eax
0x477498: push     1
0x47749A: mov      ecx, dword ptr [ebp + 8]
0x47749D: mov      eax, dword ptr [ebp - 4]
0x4774A0: add      ecx, dword ptr [eax + 0x14]
0x4774A3: mov      edx, dword ptr [ebp - 0xc]
0x4774A6: mov      eax, dword ptr [ebp - 4]
0x4774A9: add      edx, dword ptr [eax + 0x10]
0x4774AC: mov      eax, dword ptr [ebp - 8]
0x4774AF: call     0x455578
0x4774B4: jmp      0x4774eb
0x4774B6: lea      edx, [ebp - 0x1c]
0x4774B9: mov      eax, dword ptr [ebp - 4]
0x4774BC: mov      eax, dword ptr [eax + 0x24]
0x4774BF: call     0x44d8ec
0x4774C4: lea      eax, [ebp - 0x1c]
0x4774C7: push     eax
0x4774C8: mov      eax, dword ptr [ebp - 4]
0x4774CB: mov      eax, dword ptr [eax + 0x24]
0x4774CE: push     eax
0x4774CF: push     1
0x4774D1: mov      ecx, dword ptr [ebp + 8]
0x4774D4: mov      eax, dword ptr [ebp - 4]
0x4774D7: add      ecx, dword ptr [eax + 0x14]
0x4774DA: mov      edx, dword ptr [ebp - 0xc]
0x4774DD: mov      eax, dword ptr [ebp - 4]
0x4774E0: add      edx, dword ptr [eax + 0x10]
0x4774E3: mov      eax, dword ptr [ebp - 8]
0x4774E6: call     0x44d6cc
0x4774EB: mov      esp, ebp
0x4774ED: pop      ebp
0x4774EE: ret      4
0x4774F1: lea      eax, [eax]
0x4774F4: push     ebp
0x4774F5: mov      ebp, esp
0x4774F7: push     ecx
0x4774F8: mov      dword ptr [ebp - 4], eax
0x4774FB: mov      eax, dword ptr [ebp - 4]
0x4774FE: xor      edx, edx
0x477500: mov      dword ptr [eax + 0x24], edx
0x477503: call     0x406154
0x477508: mov      edx, dword ptr [ebp - 4]
0x47750B: sub      eax, dword ptr [edx + 0x2c]
0x47750E: cmp      eax, 0x14
0x477511: jbe      0x477524
0x477513: call     0x406154
0x477518: mov      edx, dword ptr [ebp - 4]
0x47751B: mov      dword ptr [edx + 0x2c], eax
0x47751E: mov      eax, dword ptr [ebp - 4]
0x477521: inc      dword ptr [eax + 0x30]
0x477524: mov      eax, dword ptr [ebp - 4]
0x477527: mov      eax, dword ptr [eax + 0x18]
0x47752A: cmp      eax, 7
0x47752D: ja       0x4776db
0x477533: jmp      dword ptr [eax*4 + 0x47753a]
0x47753B: jbe      0x477584
0x47753D: add      byte ptr [edx + 0x75], bl
0x477540: inc      edi
0x477541: add      bl, bl
0x477543: jbe      0x47758c
0x477545: add      byte ptr [esi - 0x19ffb88b], cl
0x47754B: jne      0x477594
0x47754D: add      byte ptr [esi + esi*2], dh
0x477550: inc      edi
0x477551: add      byte ptr [esi + esi*2 + 0x76b10047], al
0x477558: inc      edi
0x477559: add      byte ptr [ebx - 0x3f7c03bb], cl
0x47755F: adc      al, 0x50
0x477561: mov      eax, dword ptr [ebp - 4]
0x477564: lea      ecx, [eax + 0x10]
0x477567: mov      eax, dword ptr [ebp - 4]
0x47756A: mov      edx, dword ptr [eax + 0xc]
0x47756D: add      edx, 0x1a4
0x477573: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x477578: mov      eax, dword ptr [eax]
0x47757A: mov      eax, dword ptr [eax + 0x36c]
0x477580: call     0x453a48
0x477585: mov      edx, dword ptr [ebp - 4]
0x477588: mov      dword ptr [edx + 0x24], eax
0x47758B: pop      ecx
0x47758C: pop      ebp
0x47758D: ret      
0x47758E: mov      eax, dword ptr [ebp - 4]
0x477591: cmp      dword ptr [eax + 0x1c], 0
0x477595: jg       0x4775a1
0x477597: mov      eax, dword ptr [ebp - 4]
0x47759A: mov      dword ptr [eax + 0x1c], 1
0x4775A1: mov      eax, dword ptr [ebp - 4]
0x4775A4: cmp      dword ptr [eax + 0x1c], 5
0x4775A8: jle      0x4775b4
0x4775AA: mov      eax, dword ptr [ebp - 4]
0x4775AD: mov      dword ptr [eax + 0x1c], 5
0x4775B4: mov      eax, dword ptr [ebp - 4]
0x4775B7: add      eax, 0x14
0x4775BA: push     eax
0x4775BB: mov      eax, dword ptr [ebp - 4]
0x4775BE: lea      ecx, [eax + 0x10]
0x4775C1: mov      eax, dword ptr [ebp - 4]
0x4775C4: mov      edx, dword ptr [eax + 0x1c]
0x4775C7: dec      edx
0x4775C8: add      edx, 0x40
0x4775CB: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4775D0: mov      eax, dword ptr [eax]
0x4775D2: mov      eax, dword ptr [eax + 0x370]
0x4775D8: call     0x453a48
0x4775DD: mov      edx, dword ptr [ebp - 4]
0x4775E0: mov      dword ptr [edx + 0x24], eax
0x4775E3: pop      ecx
0x4775E4: pop      ebp
0x4775E5: ret      
0x4775E6: mov      eax, dword ptr [ebp - 4]
0x4775E9: add      eax, 0x14
0x4775EC: push     eax
0x4775ED: mov      eax, dword ptr [ebp - 4]
0x4775F0: mov      eax, dword ptr [eax + 0x30]
0x4775F3: mov      ecx, 0xa
0x4775F8: xor      edx, edx
0x4775FA: div      ecx
0x4775FC: add      edx, 0x56e
0x477602: mov      eax, dword ptr [ebp - 4]
0x477605: lea      ecx, [eax + 0x10]
0x477608: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47760D: mov      eax, dword ptr [eax]
0x47760F: mov      eax, dword ptr [eax + 0x33c]
0x477615: call     0x453a48
0x47761A: mov      edx, dword ptr [ebp - 4]
0x47761D: mov      dword ptr [edx + 0x24], eax
0x477620: mov      eax, dword ptr [ebp - 4]
0x477623: mov      byte ptr [eax + 0x28], 1
0x477627: mov      eax, dword ptr [ebp - 4]
0x47762A: mov      dword ptr [eax + 0x34], 1
0x477631: pop      ecx
0x477632: pop      ebp
0x477633: ret      
0x477634: mov      eax, dword ptr [ebp - 4]
0x477637: add      eax, 0x14
0x47763A: push     eax
0x47763B: mov      eax, dword ptr [ebp - 4]
0x47763E: mov      eax, dword ptr [eax + 0x30]
0x477641: shr      eax, 1
0x477643: mov      ecx, 6
0x477648: xor      edx, edx
0x47764A: div      ecx
0x47764C: add      edx, 0x65e
0x477652: mov      eax, dword ptr [ebp - 4]
0x477655: lea      ecx, [eax + 0x10]
0x477658: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x47765D: mov      eax, dword ptr [eax]
0x47765F: mov      eax, dword ptr [eax + 0x33c]
0x477665: call     0x453a48
0x47766A: mov      edx, dword ptr [ebp - 4]
0x47766D: mov      dword ptr [edx + 0x24], eax
0x477670: mov      eax, dword ptr [ebp - 4]
0x477673: mov      byte ptr [eax + 0x28], 1
0x477677: mov      eax, dword ptr [ebp - 4]
0x47767A: mov      dword ptr [eax + 0x34], 1
0x477681: pop      ecx
0x477682: pop      ebp
0x477683: ret      
0x477684: mov      eax, dword ptr [ebp - 4]
0x477687: add      eax, 0x14
0x47768A: push     eax
0x47768B: mov      eax, dword ptr [ebp - 4]
0x47768E: lea      ecx, [eax + 0x10]
0x477691: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x477696: mov      eax, dword ptr [eax]
0x477698: mov      eax, dword ptr [eax + 0x38c]
0x47769E: mov      edx, 0x545
0x4776A3: call     0x453a48
0x4776A8: mov      edx, dword ptr [ebp - 4]
0x4776AB: mov      dword ptr [edx + 0x24], eax
0x4776AE: pop      ecx
0x4776AF: pop      ebp
0x4776B0: ret      
0x4776B1: mov      eax, dword ptr [ebp - 4]
0x4776B4: add      eax, 0x14
0x4776B7: push     eax
0x4776B8: mov      eax, dword ptr [ebp - 4]
0x4776BB: lea      ecx, [eax + 0x10]
0x4776BE: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x4776C3: mov      eax, dword ptr [eax]
0x4776C5: mov      eax, dword ptr [eax + 0x3a8]
0x4776CB: mov      edx, 0x1a4
0x4776D0: call     0x453a48
0x4776D5: mov      edx, dword ptr [ebp - 4]
0x4776D8: mov      dword ptr [edx + 0x24], eax
0x4776DB: pop      ecx
0x4776DC: pop      ebp
0x4776DD: ret      
0x4776DE: mov      eax, eax
0x4776E0: push     ebp
0x4776E1: mov      ebp, esp
0x4776E3: add      esp, -8
0x4776E6: test     dl, dl
0x4776E8: je       0x4776f2
0x4776EA: add      esp, -0x10
0x4776ED: call     0x4031a4
0x4776F2: mov      byte ptr [ebp - 5], dl
0x4776F5: mov      dword ptr [ebp - 4], eax
0x4776F8: mov      dl, 1
0x4776FA: mov      eax, dword ptr [0x40df00]
0x4776FF: call     0x402e7c
0x477704: mov      edx, dword ptr [ebp - 4]
0x477707: mov      dword ptr [edx + 4], eax
0x47770A: mov      eax, dword ptr [ebp - 4]
0x47770D: cmp      byte ptr [ebp - 5], 0
0x477711: je       0x477722
0x477713: call     0x4031fc
0x477718: pop      dword ptr fs:[0]
0x47771F: add      esp, 0xc
0x477722: mov      eax, dword ptr [ebp - 4]
0x477725: pop      ecx
0x477726: pop      ecx
0x477727: pop      ebp
0x477728: ret      
0x477729: lea      eax, [eax]
0x47772C: push     ebp
0x47772D: mov      ebp, esp
0x47772F: add      esp, -0x10
0x477732: call     0x403204
0x477737: mov      byte ptr [ebp - 5], dl
0x47773A: mov      dword ptr [ebp - 4], eax
0x47773D: mov      eax, dword ptr [ebp - 4]
0x477740: mov      eax, dword ptr [eax + 4]
0x477743: mov      eax, dword ptr [eax + 8]
0x477746: dec      eax
0x477747: test     eax, eax
0x477749: jl       0x477771
0x47774B: inc      eax
0x47774C: mov      dword ptr [ebp - 0x10], eax
0x47774F: mov      dword ptr [ebp - 0xc], 0
0x477756: mov      edx, dword ptr [ebp - 0xc]
0x477759: mov      eax, dword ptr [ebp - 4]
0x47775C: mov      eax, dword ptr [eax + 4]
0x47775F: call     0x40f6a0
0x477764: call     0x402eac
0x477769: inc      dword ptr [ebp - 0xc]
0x47776C: dec      dword ptr [ebp - 0x10]
0x47776F: jne      0x477756
0x477771: mov      eax, dword ptr [ebp - 4]
0x477774: mov      eax, dword ptr [eax + 4]
0x477777: call     0x402eac
0x47777C: mov      dl, byte ptr [ebp - 5]
0x47777F: and      dl, 0xfc
0x477782: mov      eax, dword ptr [ebp - 4]
0x477785: call     0x402e9c
0x47778A: cmp      byte ptr [ebp - 5], 0
0x47778E: jle      0x477798
0x477790: mov      eax, dword ptr [ebp - 4]
0x477793: call     0x4031f4
0x477798: mov      esp, ebp
0x47779A: pop      ebp
0x47779B: ret      
0x47779C: push     ebp
0x47779D: mov      ebp, esp
0x47779F: add      esp, -0xc
0x4777A2: push     ebx
0x4777A3: mov      dword ptr [ebp - 4], eax
0x4777A6: mov      eax, dword ptr [ebp - 4]
0x4777A9: mov      eax, dword ptr [eax + 4]
0x4777AC: mov      eax, dword ptr [eax + 8]
0x4777AF: dec      eax
0x4777B0: test     eax, eax
0x4777B2: jl       0x4777da
0x4777B4: inc      eax
0x4777B5: mov      dword ptr [ebp - 0xc], eax
0x4777B8: mov      dword ptr [ebp - 8], 0
0x4777BF: mov      eax, dword ptr [ebp - 4]
0x4777C2: mov      eax, dword ptr [eax + 4]
0x4777C5: mov      edx, dword ptr [ebp - 8]
0x4777C8: call     0x40f6a0
0x4777CD: call     0x402eac
0x4777D2: inc      dword ptr [ebp - 8]
0x4777D5: dec      dword ptr [ebp - 0xc]
0x4777D8: jne      0x4777bf
0x4777DA: mov      eax, dword ptr [ebp - 4]
0x4777DD: mov      eax, dword ptr [eax + 4]
0x4777E0: mov      bx, 0xffff
0x4777E4: call     0x403080
0x4777E9: pop      ebx
0x4777EA: mov      esp, ebp
0x4777EC: pop      ebp
0x4777ED: ret      
0x4777EE: mov      eax, eax
0x4777F0: push     ebp
0x4777F1: mov      ebp, esp
0x4777F3: add      esp, -0x18
0x4777F6: mov      dword ptr [ebp - 8], edx
0x4777F9: mov      dword ptr [ebp - 4], eax
0x4777FC: mov      eax, dword ptr [ebp - 4]
0x4777FF: mov      eax, dword ptr [eax + 4]
0x477802: mov      eax, dword ptr [eax + 8]
0x477805: dec      eax
0x477806: test     eax, eax
0x477808: jl       0x477858
0x47780A: inc      eax
0x47780B: mov      dword ptr [ebp - 0x14], eax
0x47780E: mov      dword ptr [ebp - 0x10], 0
0x477815: mov      eax, dword ptr [ebp - 4]
0x477818: mov      eax, dword ptr [eax + 4]
0x47781B: mov      edx, dword ptr [ebp - 0x10]
0x47781E: call     0x40f6a0
0x477823: cmp      eax, dword ptr [ebp - 8]
0x477826: je       0x477841
0x477828: mov      eax, dword ptr [ebp - 4]
0x47782B: mov      eax, dword ptr [eax + 4]
0x47782E: mov      edx, dword ptr [ebp - 0x10]
0x477831: call     0x40f6a0
0x477836: mov      eax, dword ptr [eax + 0x20]
0x477839: mov      edx, dword ptr [ebp - 8]
0x47783C: cmp      eax, dword ptr [edx + 0x20]
0x47783F: jne      0x477850
0x477841: mov      eax, dword ptr [ebp - 8]
0x477844: call     0x402eac
0x477849: xor      eax, eax
0x47784B: mov      dword ptr [ebp - 0xc], eax
0x47784E: jmp      0x47786c
0x477850: inc      dword ptr [ebp - 0x10]
0x477853: dec      dword ptr [ebp - 0x14]
0x477856: jne      0x477815
0x477858: mov      eax, dword ptr [ebp - 4]
0x47785B: mov      eax, dword ptr [eax + 4]
0x47785E: mov      edx, dword ptr [ebp - 8]
0x477861: call     0x40f588
0x477866: mov      eax, dword ptr [ebp - 8]
0x477869: mov      dword ptr [ebp - 0xc], eax
0x47786C: mov      eax, dword ptr [ebp - 0xc]
0x47786F: mov      esp, ebp
0x477871: pop      ebp
0x477872: ret      
0x477873: nop      
0x477874: push     ebp
0x477875: mov      ebp, esp
0x477877: add      esp, -0x10
0x47787A: mov      dword ptr [ebp - 8], edx
0x47787D: mov      dword ptr [ebp - 4], eax
0x477880: mov      eax, dword ptr [ebp - 4]
0x477883: mov      eax, dword ptr [eax + 4]
0x477886: mov      eax, dword ptr [eax + 8]
0x477889: dec      eax
0x47788A: test     eax, eax
0x47788C: jl       0x4778da
0x47788E: inc      eax
0x47788F: mov      dword ptr [ebp - 0x10], eax
0x477892: mov      dword ptr [ebp - 0xc], 0
0x477899: mov      edx, dword ptr [ebp - 0xc]
0x47789C: mov      eax, dword ptr [ebp - 4]
0x47789F: mov      eax, dword ptr [eax + 4]
0x4778A2: call     0x40f6a0
0x4778A7: mov      eax, dword ptr [eax + 0x20]
0x4778AA: cmp      eax, dword ptr [ebp - 8]
0x4778AD: jne      0x4778d2
0x4778AF: mov      edx, dword ptr [ebp - 0xc]
0x4778B2: mov      eax, dword ptr [ebp - 4]
0x4778B5: mov      eax, dword ptr [eax + 4]
0x4778B8: call     0x40f6a0
0x4778BD: call     0x402eac
0x4778C2: mov      edx, dword ptr [ebp - 0xc]
0x4778C5: mov      eax, dword ptr [ebp - 4]
0x4778C8: mov      eax, dword ptr [eax + 4]
0x4778CB: call     0x40f5c4
0x4778D0: jmp      0x4778da
0x4778D2: inc      dword ptr [ebp - 0xc]
0x4778D5: dec      dword ptr [ebp - 0x10]
0x4778D8: jne      0x477899
0x4778DA: mov      esp, ebp
0x4778DC: pop      ebp
0x4778DD: ret      
0x4778DE: mov      eax, eax
0x4778E0: push     ebp
0x4778E1: mov      ebp, esp
0x4778E3: add      esp, -0x18
0x4778E6: mov      dword ptr [ebp - 0xc], ecx
0x4778E9: mov      dword ptr [ebp - 8], edx
0x4778EC: mov      dword ptr [ebp - 4], eax
0x4778EF: xor      eax, eax
0x4778F1: mov      dword ptr [ebp - 0x10], eax
0x4778F4: mov      eax, dword ptr [ebp - 4]
0x4778F7: mov      eax, dword ptr [eax + 4]
0x4778FA: mov      eax, dword ptr [eax + 8]
0x4778FD: dec      eax
0x4778FE: test     eax, eax
0x477900: jl       0x47796a
0x477902: inc      eax
0x477903: mov      dword ptr [ebp - 0x18], eax
0x477906: mov      dword ptr [ebp - 0x14], 0
0x47790D: mov      edx, dword ptr [ebp - 0x14]
0x477910: mov      eax, dword ptr [ebp - 4]
0x477913: mov      eax, dword ptr [eax + 4]
0x477916: call     0x40f6a0
0x47791B: mov      eax, dword ptr [eax + 4]
0x47791E: cmp      eax, dword ptr [ebp - 8]
0x477921: jne      0x477962
0x477923: mov      edx, dword ptr [ebp - 0x14]
0x477926: mov      eax, dword ptr [ebp - 4]
0x477929: mov      eax, dword ptr [eax + 4]
0x47792C: call     0x40f6a0
0x477931: mov      eax, dword ptr [eax + 8]
0x477934: cmp      eax, dword ptr [ebp - 0xc]
0x477937: jne      0x477962
0x477939: mov      edx, dword ptr [ebp - 0x14]
0x47793C: mov      eax, dword ptr [ebp - 4]
0x47793F: mov      eax, dword ptr [eax + 4]
0x477942: call     0x40f6a0
0x477947: mov      eax, dword ptr [eax + 0x18]
0x47794A: cmp      eax, dword ptr [ebp + 8]
0x47794D: jne      0x477962
0x47794F: mov      edx, dword ptr [ebp - 0x14]
0x477952: mov      eax, dword ptr [ebp - 4]
0x477955: mov      eax, dword ptr [eax + 4]
0x477958: call     0x40f6a0
0x47795D: mov      dword ptr [ebp - 0x10], eax
0x477960: jmp      0x47796a
0x477962: inc      dword ptr [ebp - 0x14]
0x477965: dec      dword ptr [ebp - 0x18]
0x477968: jne      0x47790d
0x47796A: mov      eax, dword ptr [ebp - 0x10]
0x47796D: mov      esp, ebp
0x47796F: pop      ebp
0x477970: ret      4
0x477973: nop      
0x477974: push     ebp
0x477975: mov      ebp, esp
0x477977: add      esp, -0xc
0x47797A: push     ebx
0x47797B: mov      dword ptr [ebp - 4], eax
0x47797E: mov      eax, dword ptr [ebp - 4]
0x477981: mov      eax, dword ptr [eax + 4]
0x477984: mov      eax, dword ptr [eax + 8]
0x477987: dec      eax
0x477988: test     eax, eax
0x47798A: jl       0x4779b6
0x47798C: inc      eax
0x47798D: mov      dword ptr [ebp - 0xc], eax
0x477990: mov      dword ptr [ebp - 8], 0
0x477997: mov      eax, dword ptr [ebp - 4]
0x47799A: mov      eax, dword ptr [eax + 4]
0x47799D: mov      edx, dword ptr [ebp - 8]
0x4779A0: call     0x40f6a0
0x4779A5: mov      bx, 0xfffe
0x4779A9: call     0x403080
0x4779AE: inc      dword ptr [ebp - 8]
0x4779B1: dec      dword ptr [ebp - 0xc]
0x4779B4: jne      0x477997
0x4779B6: pop      ebx
0x4779B7: mov      esp, ebp
0x4779B9: pop      ebp
0x4779BA: ret      
0x4779BB: nop      
0x4779BC: push     ebp
0x4779BD: mov      ebp, esp
0x4779BF: xor      eax, eax
0x4779C1: push     ebp
0x4779C2: push     0x4779e1
0x4779C7: push     dword ptr fs:[eax]
0x4779CA: mov      dword ptr fs:[eax], esp
0x4779CD: inc      dword ptr [0x4f95bc]
0x4779D3: xor      eax, eax
0x4779D5: pop      edx
0x4779D6: pop      ecx
0x4779D7: pop      ecx
0x4779D8: mov      dword ptr fs:[eax], edx
0x4779DB: push     0x4779e8
0x4779E0: ret      
0x4779E1: jmp      0x403518
0x4779E6: jmp      0x4779e0
0x4779E8: pop      ebp
0x4779E9: ret      
0x4779EA: mov      eax, eax
0x4779EC: sub      dword ptr [0x4f95bc], 1
0x4779F3: ret      
0x4779F4: inc      eax
0x4779F5: jp       0x477a3e
0x4779F7: add      byte ptr [eax], al
0x4779F9: add      byte ptr [eax], al
0x4779FB: add      byte ptr [eax], al
0x4779FD: add      byte ptr [eax], al
0x4779FF: add      byte ptr [eax], al
0x477A01: add      byte ptr [eax], al
0x477A03: add      byte ptr [eax], al
0x477A05: add      byte ptr [eax], al
0x477A07: add      byte ptr [eax], al
0x477A09: add      byte ptr [eax], al
0x477A0B: add      byte ptr [eax], al
0x477A0D: add      byte ptr [eax], al
0x477A0F: add      byte ptr [eax + 0x7a], al
0x477A12: inc      edi
0x477A13: add      byte ptr [edx + edi*2 + 0x47], dl
0x477A17: add      byte ptr [edx + eax], al
0x477A1B: add      ah, cl
0x477A1D: pop      edx
0x477A1E: dec      ecx
0x477A1F: add      al, cl
0x477A21: xor      byte ptr [eax], al
0x477A24: aam      0x30
0x477A26: inc      eax
0x477A27: add      al, bl
0x477A29: xor      byte ptr [eax], al
0x477A2C: fdiv     qword ptr [eax]
0x477A2E: inc      eax
0x477A2F: add      al, dl
0x477A31: xor      byte ptr [eax], al
0x477A34: inc      eax
0x477A35: inc      eax
0x477A37: add      byte ptr [esi + ebp + 0x40], dl
0x477A3C: lodsb    al, byte ptr [esi]
0x477A3D: jge      0x477a86
