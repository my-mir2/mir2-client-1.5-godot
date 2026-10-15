
; === VMT_TMagicEff @ 0x49C124 ===
0x49C124: add      eax, dword ptr [eax]
0x49C12A: std      
0x49C12B: dec      dword ptr [eax]
0x49C12D: ror      dword ptr [ecx], cl
0x49C130: dec      esp
0x49C131: retf     
0x49C132: dec      ecx
0x49C133: add      byte ptr [eax - 0x2d], dl
0x49C136: dec      ecx
0x49C137: add      byte ptr [ecx], cl
0x49C139: push     esp
0x49C13A: dec      ebp
0x49C13B: popal    
0x49C13C: imul     esp, dword ptr [bp + di + 0x45], 0xc08b6666
0x49C144: nop      
0x49C145: ror      dword ptr [ecx], 0
0x49C149: add      byte ptr [eax], al
0x49C14B: add      byte ptr [eax], al
0x49C14D: add      byte ptr [eax], al
0x49C14F: add      byte ptr [eax], al
0x49C151: add      byte ptr [eax], al
0x49C153: add      byte ptr [eax], al
0x49C155: add      byte ptr [eax], al
0x49C157: add      byte ptr [eax], al
0x49C159: add      byte ptr [eax], al
0x49C15B: add      byte ptr [eax], al
0x49C15D: add      byte ptr [eax], al
0x49C15F: add      byte ptr [eax - 0x67ffb63f], dl
0x49C165: ror      dword ptr [ecx], 0xc8
0x49C169: add      byte ptr [eax], al
0x49C16B: add      al, bl
0x49C16D: ror      byte ptr [ecx], 0xc8
0x49C171: xor      byte ptr [eax], al
0x49C174: aam      0x30
0x49C176: inc      eax
0x49C177: add      al, bl
0x49C179: xor      byte ptr [eax], al
0x49C17C: fdiv     qword ptr [eax]
0x49C17E: inc      eax
0x49C17F: add      al, dl
0x49C181: xor      byte ptr [eax], al
0x49C184: inc      eax
0x49C185: inc      eax
0x49C187: add      byte ptr [esi + ebp + 0x40], dl
0x49C18B: add      al, dl
0x49C18D: retf     0x49
0x49C190: add      dword ptr [eax], eax
0x49C192: std      
0x49C193: push     dword ptr [ebp + edx*8 + 0x49]
0x49C197: add      byte ptr [edx], cl
0x49C199: push     esp
0x49C19A: inc      esi
0x49C19B: insb     byte ptr es:[edi], dx
0x49C19C: jns      0x49c207
0x49C19E: outsb    dx, byte ptr [esi]
0x49C19F: inc      ecx
0x49C1A1: js       0x49c208
0x49C1A3: nop      
0x49C1A5: ror      dword ptr [ecx], 0
0x49C1A9: add      byte ptr [eax], al
0x49C1AB: add      byte ptr [eax], al
0x49C1AD: add      byte ptr [eax], al
0x49C1AF: add      byte ptr [eax], al
0x49C1B1: add      byte ptr [eax], al
0x49C1B3: add      byte ptr [eax], al
0x49C1B5: add      byte ptr [eax], al
0x49C1B7: add      byte ptr [eax], al
0x49C1B9: add      byte ptr [eax], al
0x49C1BB: add      byte ptr [eax], al
0x49C1BD: add      byte ptr [eax], al
0x49C1BF: add      al, dh
0x49C1C1: ror      dword ptr [ecx], 0xf8
0x49C1C5: ror      dword ptr [ecx], 0xc8
0x49C1C9: add      byte ptr [eax], al
0x49C1CB: add      byte ptr [ecx + eax*8 + 0x49], al
0x49C1CF: add      al, cl
0x49C1D1: xor      byte ptr [eax], al
0x49C1D4: aam      0x30
0x49C1D6: inc      eax
0x49C1D7: add      al, bl
0x49C1D9: xor      byte ptr [eax], al
0x49C1DC: fdiv     qword ptr [eax]
0x49C1DE: inc      eax
0x49C1DF: add      al, dl
0x49C1E1: xor      byte ptr [eax], al
0x49C1E4: inc      eax
0x49C1E5: inc      eax
0x49C1E7: add      byte ptr [esi + ebp + 0x40], dl
0x49C1EB: add      al, dl
0x49C1ED: retf     0x49
0x49C1F0: add      dword ptr [eax], eax
0x49C1F2: std      
0x49C1F3: inc      eax
0x49C1F5: salc     
0x49C1F6: dec      ecx
0x49C1F7: add      byte ptr [esp + edx*2], cl
0x49C1FA: inc      esi
0x49C1FB: insb     byte ptr es:[edi], dx
0x49C1FC: jns      0x49c267
0x49C1FE: outsb    dx, byte ptr [esi]
0x49C1FF: inc      ecx
0x49C201: jb       0x49c275
0x49C203: outsd    dx, dword ptr [esi]
0x49C204: ja       0x49c193
0x49C206: inc      eax
0x49C207: add      byte ptr [edx + eax*8 + 0x49], dl
0x49C20B: add      byte ptr [eax], al
0x49C20D: add      byte ptr [eax], al
0x49C20F: add      byte ptr [eax], al
0x49C211: add      byte ptr [eax], al
0x49C213: add      byte ptr [eax], al
0x49C215: add      byte ptr [eax], al
0x49C217: add      byte ptr [eax], al
0x49C219: add      byte ptr [eax], al
0x49C21B: add      byte ptr [eax], al
0x49C21D: add      byte ptr [eax], al
0x49C21F: add      byte ptr [eax], al
0x49C221: add      byte ptr [eax], al
0x49C223: add      byte ptr [edx + eax*8 + 0x49], dl
0x49C227: add      byte ptr [edx - 0x3e], ah
0x49C22A: dec      ecx
0x49C22B: add      al, al
0x49C22D: add      byte ptr [eax], al
0x49C22F: add      al, bl
0x49C231: ror      byte ptr [ecx], 0xc8
0x49C235: xor      byte ptr [eax], al
0x49C238: aam      0x30
0x49C23A: inc      eax
0x49C23B: add      al, bl
0x49C23D: xor      byte ptr [eax], al
0x49C240: fdiv     qword ptr [eax]
0x49C242: inc      eax
0x49C243: add      al, dl
0x49C245: xor      byte ptr [eax], al
0x49C248: inc      eax
0x49C249: inc      eax
0x49C24B: add      byte ptr [esi + ebp + 0x40], dl
0x49C24F: add      al, dl
0x49C251: retf     0x49
0x49C254: add      al, byte ptr [eax]
0x49C258: std      
0x49C259: call     dword ptr [eax + 0xc0049d8]
0x49C260: dec      ecx
0x49C261: add      byte ptr [ebx], cl
0x49C263: push     esp
0x49C264: inc      ebx
0x49C265: push     0x66457261
0x49C26A: arpl     word ptr gs:[ebx + ecx*4 - 0x40], si
0x49C270: mov      esp, 0x49c2
0x49C275: add      byte ptr [eax], al
0x49C277: add      byte ptr [eax], al
0x49C279: add      byte ptr [eax], al
0x49C27B: add      byte ptr [eax], al
0x49C27D: add      byte ptr [eax], al
0x49C27F: add      byte ptr [eax], al
0x49C281: add      byte ptr [eax], al
0x49C283: add      byte ptr [eax], al
0x49C285: add      byte ptr [eax], al
0x49C287: add      byte ptr [eax], al
0x49C289: add      byte ptr [eax], al
0x49C28B: add      byte ptr [edx + eax*8 - 0x3d35ffb7], bh
0x49C292: dec      ecx
0x49C293: add      ah, al
0x49C295: add      byte ptr [eax], al
0x49C297: add      al, bl
0x49C299: ror      byte ptr [ecx], 0xc8
0x49C29D: xor      byte ptr [eax], al
0x49C2A0: aam      0x30
0x49C2A2: inc      eax
0x49C2A3: add      al, bl
0x49C2A5: xor      byte ptr [eax], al
0x49C2A8: fdiv     qword ptr [eax]
0x49C2AA: inc      eax
0x49C2AB: add      al, dl
0x49C2AD: xor      byte ptr [eax], al
0x49C2B0: inc      eax
0x49C2B1: inc      eax
0x49C2B3: add      byte ptr [esi + ebp + 0x40], dl
0x49C2B7: add      al, dl
0x49C2B9: retf     0x49
0x49C2BC: add      al, byte ptr [eax]
0x49C2C0: std      
0x49C2C1: inc      dword ptr [edx + ebx*8 - 0x24dbffb7]
0x49C2C8: dec      ecx
0x49C2C9: add      byte ptr [edx], cl
0x49C2CB: push     esp
0x49C2CC: dec      ebp
0x49C2CD: popal    
0x49C2CE: jo       0x49c315
0x49C2D0: arpl     word ptr gs:[ebp + ecx*4 + 0x40], si
0x49C2D7: add      byte ptr [ebx + eax*8], ah
0x49C2DA: dec      ecx
0x49C2DB: add      byte ptr [eax], al
0x49C2DD: add      byte ptr [eax], al
0x49C2DF: add      byte ptr [eax], al
0x49C2E1: add      byte ptr [eax], al
0x49C2E3: add      byte ptr [eax], al
0x49C2E5: add      byte ptr [eax], al
0x49C2E7: add      byte ptr [eax], al
0x49C2E9: add      byte ptr [eax], al
0x49C2EB: add      byte ptr [eax], al
0x49C2ED: add      byte ptr [eax], al
0x49C2EF: add      byte ptr [eax], al
0x49C2F1: add      byte ptr [eax], al
0x49C2F3: add      byte ptr [ebx + eax*8], ah
0x49C2F6: dec      ecx
0x49C2F7: add      byte ptr [ebx + eax*8], ch
0x49C2FA: dec      ecx
0x49C2FB: add      ah, al
0x49C2FD: add      byte ptr [eax], al
0x49C2FF: add      byte ptr [eax - 0x3e], dh
0x49C302: dec      ecx
0x49C303: add      al, cl
0x49C305: xor      byte ptr [eax], al
0x49C308: aam      0x30
0x49C30A: inc      eax
0x49C30B: add      al, bl
0x49C30D: xor      byte ptr [eax], al
0x49C310: fdiv     qword ptr [eax]
0x49C312: inc      eax
0x49C313: add      al, dl
0x49C315: xor      byte ptr [eax], al
0x49C318: inc      eax
0x49C319: inc      eax
0x49C31B: add      byte ptr [esi + ebp + 0x40], dl
0x49C31F: add      al, dl
0x49C321: retf     0x49
0x49C324: add      dword ptr [eax], eax
0x49C327: push     dword ptr [eax]
0x49C329: fmul     qword ptr [ecx]
0x49C32C: adc      dword ptr [ebx + edx*2 + 0x63], edx
0x49C330: jb       0x49c3a1
0x49C332: insb     byte ptr es:[edi], dx
0x49C333: insb     byte ptr es:[edi], dx
0x49C334: dec      eax
0x49C335: imul     esp, dword ptr [ebp + 0x45], 0x63656666
0x49C33D: je       0x49c2ca ; "TMapEffect"
0x49C33F: ror      byte ptr [ebx + eax*8 + 0x49], 0
0x49C347: add      byte ptr [eax], al
0x49C349: add      byte ptr [eax], al
0x49C34B: add      byte ptr [eax], al
0x49C34D: add      byte ptr [eax], al
0x49C34F: add      byte ptr [eax], al
0x49C351: add      byte ptr [eax], al
0x49C353: add      byte ptr [eax], al
0x49C355: add      byte ptr [eax], al
0x49C357: add      byte ptr [eax], al
0x49C359: add      byte ptr [eax], al
0x49C35B: add      byte ptr [ebx + eax*8 - 0x3c65ffb7], cl
0x49C362: dec      ecx
0x49C363: add      byte ptr [eax], dl
0x49C365: add      dword ptr [eax], eax
0x49C367: add      al, bl
0x49C369: ror      byte ptr [ecx], 0xc8
0x49C36D: xor      byte ptr [eax], al
0x49C370: aam      0x30
0x49C372: inc      eax
0x49C373: add      al, bl
0x49C375: xor      byte ptr [eax], al
0x49C378: fdiv     qword ptr [eax]
0x49C37A: inc      eax
0x49C37B: add      al, dl
0x49C37D: xor      byte ptr [eax], al
0x49C380: inc      eax
0x49C381: inc      eax
0x49C383: add      byte ptr [esi + ebp + 0x40], dl
0x49C387: add      al, dl
0x49C389: retf     0x49
0x49C38C: add      al, byte ptr [eax]
0x49C390: std      
0x49C391: call     dword ptr [ebx*8 - 0x2123ffb7]
0x49C398: dec      ecx
0x49C399: add      byte ptr [esi], cl
0x49C39B: push     esp
0x49C39C: inc      esi
0x49C39D: imul     esi, dword ptr [edx + 0x65], 0x456e7547
0x49C3A4: arpl     word ptr gs:[ebp + ecx*4 + 0x40], si
0x49C3AB: add      al, bh
0x49C3AD: ret      
0x49C3AE: dec      ecx
0x49C3AF: add      byte ptr [eax], al
0x49C3B1: add      byte ptr [eax], al
0x49C3B3: add      byte ptr [eax], al
0x49C3B5: add      byte ptr [eax], al
0x49C3B7: add      byte ptr [eax], al
0x49C3B9: add      byte ptr [eax], al
0x49C3BB: add      byte ptr [eax], al
0x49C3BD: add      byte ptr [eax], al
0x49C3BF: add      byte ptr [eax], al
0x49C3C1: add      byte ptr [eax], al
0x49C3C3: add      byte ptr [eax], al
0x49C3C5: add      byte ptr [eax], al
0x49C3C7: add      al, bh
0x49C3C9: ret      
0x49C3CA: dec      ecx
0x49C3CB: add      byte ptr [eax], al
0x49C3CD: les      ecx, ptr [ecx]
0x49C3D0: rol      byte ptr [eax], 0
0x49C3D3: add      al, bl
0x49C3D5: ror      byte ptr [ecx], 0xc8
0x49C3D9: xor      byte ptr [eax], al
0x49C3DC: aam      0x30
0x49C3DE: inc      eax
0x49C3DF: add      al, bl
0x49C3E1: xor      byte ptr [eax], al
0x49C3E4: fdiv     qword ptr [eax]
0x49C3E6: inc      eax
0x49C3E7: add      al, dl
0x49C3E9: xor      byte ptr [eax], al
0x49C3EC: inc      eax
0x49C3ED: inc      eax
0x49C3EF: add      byte ptr [esi + ebp + 0x40], dl
0x49C3F3: add      al, dl
0x49C3F5: retf     0x49
0x49C3F8: add      dword ptr [eax], eax
0x49C3FA: std      
0x49C3FB: push     dword ptr [eax + 0xd0049e0]
0x49C401: push     esp
0x49C402: push     esp
0x49C403: push     0x72656475
0x49C408: inc      ebp
0x49C409: arpl     word ptr gs:[ebx + ecx*4 - 0x40], si
0x49C410: pop      esp
0x49C411: les      ecx, ptr [ecx]
0x49C414: add      byte ptr [eax], al
0x49C416: add      byte ptr [eax], al
0x49C418: add      byte ptr [eax], al
0x49C41A: add      byte ptr [eax], al
0x49C41C: add      byte ptr [eax], al
0x49C41E: add      byte ptr [eax], al
0x49C420: add      byte ptr [eax], al
0x49C422: add      byte ptr [eax], al
0x49C424: add      byte ptr [eax], al
0x49C426: add      byte ptr [eax], al
0x49C428: add      byte ptr [eax], al
0x49C42A: add      byte ptr [eax], al
0x49C42C: pop      esp
0x49C42D: les      ecx, ptr [ecx]
0x49C430: les      ecx, ptr fs:[ecx]
0x49C434: rol      byte ptr [eax], 0
0x49C437: add      al, bl
0x49C439: ror      byte ptr [ecx], 0xc8
0x49C43D: xor      byte ptr [eax], al
0x49C440: aam      0x30
0x49C442: inc      eax
0x49C443: add      al, bl
0x49C445: xor      byte ptr [eax], al
0x49C448: fdiv     qword ptr [eax]
0x49C44A: inc      eax
0x49C44B: add      al, dl
0x49C44D: xor      byte ptr [eax], al
0x49C450: inc      eax
0x49C451: inc      eax
0x49C453: add      byte ptr [esi + ebp + 0x40], dl
0x49C457: add      al, dl
0x49C459: retf     0x49
0x49C45C: add      dword ptr [eax], eax
0x49C45E: std      
0x49C45F: dec      dword ptr [eax + 0x100049e1]
0x49C465: push     esp
0x49C466: dec      esp
0x49C467: imul     esp, dword ptr [edi + 0x68], 0x676e6974
0x49C46E: push     esp
0x49C46F: push     0x65646e75
0x49C474: jb       0x49c403
0x49C476: inc      eax
0x49C477: add      ah, al
0x49C479: les      ecx, ptr [ecx]
0x49C47C: add      byte ptr [eax], al
0x49C47E: add      byte ptr [eax], al
0x49C480: add      byte ptr [eax], al
0x49C482: add      byte ptr [eax], al
0x49C484: add      byte ptr [eax], al
0x49C486: add      byte ptr [eax], al
0x49C488: add      byte ptr [eax], al
0x49C48A: add      byte ptr [eax], al
0x49C48C: add      byte ptr [eax], al
0x49C48E: add      byte ptr [eax], al
0x49C490: add      byte ptr [eax], al
0x49C492: add      byte ptr [eax], al
0x49C495: les      ecx, ptr [ecx]
0x49C498: int3     
0x49C499: les      ecx, ptr [ecx]
0x49C49C: rol      byte ptr [eax], 0
0x49C49F: add      al, bl
0x49C4A1: ror      byte ptr [ecx], 0xc8
0x49C4A5: xor      byte ptr [eax], al
0x49C4A8: aam      0x30
0x49C4AA: inc      eax
0x49C4AB: add      al, bl
0x49C4AD: xor      byte ptr [eax], al
0x49C4B0: fdiv     qword ptr [eax]
0x49C4B2: inc      eax
0x49C4B3: add      al, dl
0x49C4B5: xor      byte ptr [eax], al
0x49C4B8: inc      eax
0x49C4B9: inc      eax
0x49C4BB: add      byte ptr [esi + ebp + 0x40], dl
0x49C4BF: add      al, dl
0x49C4C1: retf     0x49
0x49C4C4: add      dword ptr [eax], eax
0x49C4C6: std      
0x49C4C7: call     dword ptr [eax]
0x49C4C9: jecxz    0x49c514
0x49C4CB: add      byte ptr [edx], dl
0x49C4CD: push     esp
0x49C4CE: inc      ebp
0x49C4CF: js       0x49c541
0x49C4D1: insb     byte ptr es:[edi], dx
0x49C4D2: outsd    dx, dword ptr [esi]
0x49C4D3: inc      edx
0x49C4D4: jne      0x49c540
0x49C4D6: popal    
0x49C4D7: jne      0x49c544
0x49C4D9: inc      ebp
0x49C4DA: arpl     word ptr gs:[eax + edx*4 + 0x2c], si
0x49C4E1: lds      ecx, ptr [ecx]
0x49C4E4: add      byte ptr [eax], al
0x49C4E6: add      byte ptr [eax], al
0x49C4E8: add      byte ptr [eax], al
0x49C4EA: add      byte ptr [eax], al
0x49C4EC: add      byte ptr [eax], al
0x49C4EE: add      byte ptr [eax], al
0x49C4F0: add      byte ptr [eax], al
0x49C4F2: add      byte ptr [eax], al
0x49C4F4: add      byte ptr [eax], al
0x49C4F6: add      byte ptr [eax], al
0x49C4F8: add      byte ptr [eax], al
0x49C4FA: add      byte ptr [eax], al
0x49C4FC: sub      al, 0xc5
0x49C4FE: dec      ecx
0x49C4FF: add      byte ptr [edx], bh
0x49C501: lds      ecx, ptr [ecx]
0x49C504: enter    0, 0
0x49C508: fadd     st(0)
0x49C50A: dec      ecx
0x49C50B: add      al, cl
0x49C50D: xor      byte ptr [eax], al
0x49C510: aam      0x30
0x49C512: inc      eax
0x49C513: add      al, bl
0x49C515: xor      byte ptr [eax], al
0x49C518: fdiv     qword ptr [eax]
0x49C51A: inc      eax
0x49C51B: add      al, dl
0x49C51D: xor      byte ptr [eax], al
0x49C520: inc      eax
0x49C521: inc      eax
0x49C523: add      byte ptr [esi + ebp + 0x40], dl
0x49C527: add      al, dl
0x49C529: retf     0x49
0x49C52C: add      al, byte ptr [eax]
0x49C530: std      
0x49C531: call     dword ptr [eax - 0x1b]
0x49C534: dec      ecx
0x49C535: add      byte ptr [esi + 0x49], bl
0x49C539: add      byte ptr [ebx], dl
0x49C53B: push     esp
0x49C53C: inc      edx
0x49C53D: jne      0x49c5a9
0x49C53F: popal    
0x49C540: jne      0x49c5ad
0x49C542: inc      edi
0x49C543: jb       0x49c5b4
0x49C545: jne      0x49c5b5
0x49C547: inc      ebp
0x49C549: arpl     word ptr gs:[ebx + ecx*4 - 0x40], si
0x49C550: pushfd   
0x49C551: lds      ecx, ptr [ecx]
0x49C554: add      byte ptr [eax], al
0x49C556: add      byte ptr [eax], al
0x49C558: add      byte ptr [eax], al
0x49C55A: add      byte ptr [eax], al
0x49C55C: add      byte ptr [eax], al
0x49C55E: add      byte ptr [eax], al
0x49C560: add      byte ptr [eax], al
0x49C562: add      byte ptr [eax], al
0x49C564: add      byte ptr [eax], al
0x49C566: add      byte ptr [eax], al
0x49C568: add      byte ptr [eax], al
0x49C56A: add      byte ptr [eax], al
0x49C56C: pushfd   
0x49C56D: lds      ecx, ptr [ecx]
0x49C570: stosb    byte ptr es:[edi], al
0x49C571: lds      ecx, ptr [ecx]
0x49C574: les      eax, ptr [eax]
0x49C576: add      byte ptr [eax], al
0x49C578: fadd     st(0)
0x49C57A: dec      ecx
0x49C57B: add      al, cl
0x49C57D: xor      byte ptr [eax], al
0x49C580: aam      0x30
0x49C582: inc      eax
0x49C583: add      al, bl
0x49C585: xor      byte ptr [eax], al
0x49C588: fdiv     qword ptr [eax]
0x49C58A: inc      eax
0x49C58B: add      al, dl
0x49C58D: xor      byte ptr [eax], al
0x49C590: inc      eax
0x49C591: inc      eax
0x49C593: add      byte ptr [esi + ebp + 0x40], dl
0x49C597: add      al, dl
0x49C599: retf     0x49
0x49C59C: add      al, byte ptr [eax]
0x49C5A0: std      
0x49C5A1: jmp      eax
0x49C5A3: call     0xe9a1c5f1
0x49C5A8: dec      ecx
0x49C5A9: add      byte ptr [ecx], dl
0x49C5AB: push     esp
0x49C5AC: dec      esi
0x49C5AD: outsd    dx, dword ptr [esi]
0x49C5AE: jb       0x49c61d
0x49C5B0: popal    
0x49C5B1: insb     byte ptr es:[edi], dx
0x49C5B2: inc      esp
0x49C5B3: jb       0x49c616
0x49C5B5: ja       0x49c5fc
0x49C5B7: arpl     word ptr gs:[ebp + edx*2 - 0x75], si
0x49C5BE: in       al, dx
0x49C5BF: add      esp, -0xc
0x49C5C2: mov      dword ptr [ebp - 0xc], ecx
0x49C5C5: mov      dword ptr [ebp - 8], edx
0x49C5C8: mov      dword ptr [ebp - 4], eax
0x49C5CB: mov      eax, dword ptr [ebp - 0xc]
0x49C5CE: xor      edx, edx
0x49C5D0: mov      dword ptr [eax], edx
0x49C5D2: mov      eax, dword ptr [ebp + 8]
0x49C5D5: xor      edx, edx
0x49C5D7: mov      dword ptr [eax], edx
0x49C5D9: mov      eax, dword ptr [ebp - 8]
0x49C5DC: sub      eax, 1
0x49C5DF: jb       0x49c5e8
0x49C5E1: je       0x49c64b
0x49C5E3: jmp      0x49c674
0x49C5E8: mov      eax, dword ptr [ebp - 4]
0x49C5EB: sub      eax, 8
0x49C5EE: je       0x49c5f5
0x49C5F0: sub      eax, 0x13
0x49C5F3: jne      0x49c620
0x49C5F5: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x49C5FA: mov      eax, dword ptr [eax]
0x49C5FC: mov      eax, dword ptr [eax + 0x3bc]
0x49C602: mov      edx, dword ptr [ebp - 0xc]
0x49C605: mov      dword ptr [edx], eax
0x49C607: mov      eax, dword ptr [ebp - 4]
0x49C60A: sub      eax, 0x1f
0x49C60D: jae      0x49c674
0x49C60F: mov      eax, dword ptr [ebp - 4]
0x49C612: mov      eax, dword ptr [eax*4 + 0x4a3ce4] ; *0x4A3CE4->0x000000
0x49C619: mov      edx, dword ptr [ebp + 8]
0x49C61C: mov      dword ptr [edx], eax
0x49C61E: jmp      0x49c674
0x49C620: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x49C625: mov      eax, dword ptr [eax]
0x49C627: mov      eax, dword ptr [eax + 0x33c]
0x49C62D: mov      edx, dword ptr [ebp - 0xc]
0x49C630: mov      dword ptr [edx], eax
0x49C632: mov      eax, dword ptr [ebp - 4]
0x49C635: sub      eax, 0x1f
0x49C638: jae      0x49c674
0x49C63A: mov      eax, dword ptr [ebp - 4]
0x49C63D: mov      eax, dword ptr [eax*4 + 0x4a3ce4] ; *0x4A3CE4->0x000000
0x49C644: mov      edx, dword ptr [ebp + 8]
0x49C647: mov      dword ptr [edx], eax
0x49C649: jmp      0x49c674
0x49C64B: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x49C650: mov      eax, dword ptr [eax]
0x49C652: mov      eax, dword ptr [eax + 0x33c]
0x49C658: mov      edx, dword ptr [ebp - 0xc]
0x49C65B: mov      dword ptr [edx], eax
0x49C65D: mov      eax, dword ptr [ebp - 4]
0x49C660: sub      eax, 5
0x49C663: jae      0x49c674
0x49C665: mov      eax, dword ptr [ebp - 4]
0x49C668: mov      eax, dword ptr [eax*4 + 0x4a3d60] ; *0x4A3D60->0x000320
0x49C66F: mov      edx, dword ptr [ebp + 8]
0x49C672: mov      dword ptr [edx], eax
0x49C674: mov      esp, ebp
0x49C676: pop      ebp
0x49C677: ret      4
0x49C67A: mov      eax, eax
0x49C67C: push     ebp
0x49C67D: mov      ebp, esp
0x49C67F: add      esp, -0x18
0x49C682: test     dl, dl
0x49C684: je       0x49c68e
0x49C686: add      esp, -0x10
0x49C689: call     0x4031a4
0x49C68E: mov      dword ptr [ebp - 0xc], ecx
0x49C691: mov      byte ptr [ebp - 5], dl
0x49C694: mov      dword ptr [ebp - 4], eax
0x49C697: mov      eax, dword ptr [0x4a3fe8] ; *0x4A3FE8->0x4F7D88
0x49C69C: mov      eax, dword ptr [eax]
0x49C69E: mov      eax, dword ptr [eax + 0x33c]
0x49C6A4: mov      edx, dword ptr [ebp - 4]
0x49C6A7: mov      dword ptr [edx + 0x14], eax
0x49C6AA: xor      eax, eax
0x49C6AC: mov      al, byte ptr [ebp + 0x10]
0x49C6AF: cmp      eax, 0xb
0x49C6B2: ja       0x49c7f3
0x49C6B8: jmp      dword ptr [eax*4 + 0x49c6bf]
0x49C6C1: dec      ecx
0x49C6C2: add      ah, dh
0x49C6C5: dec      ecx
0x49C6C6: add      byte ptr [edi], dh
0x49C6C9: dec      ecx
0x49C6CA: add      byte ptr [ebp - 0x39], dh
0x49C6CD: dec      ecx
0x49C6CE: add      bl, dh
0x49C6D1: dec      ecx
0x49C6D2: add      bl, dh
0x49C6D5: dec      ecx
0x49C6D6: add      byte ptr [edi], dh
0x49C6D9: dec      ecx
0x49C6DA: add      byte ptr [edi], dh
0x49C6DD: dec      ecx
0x49C6DE: add      ah, dh
0x49C6E1: dec      ecx
0x49C6E2: add      ah, dh
0x49C6E5: dec      ecx
0x49C6E6: add      bl, dh
0x49C6E9: dec      ecx
0x49C6EA: add      byte ptr [ebp - 0x16ffb639], dh
0x49C6F0: inc      dword ptr [eax]
0x49C6F2: add      byte ptr [eax], al
0x49C6F4: mov      eax, dword ptr [ebp - 4]
0x49C6F7: xor      edx, edx
0x49C6F9: mov      dword ptr [eax + 0x7c], edx
0x49C6FC: mov      eax, dword ptr [ebp - 4]
0x49C6FF: mov      dword ptr [eax + 0x84], 6
0x49C709: mov      eax, dword ptr [ebp - 4]
0x49C70C: mov      eax, dword ptr [eax + 0x7c]
0x49C70F: mov      edx, dword ptr [ebp - 4]
0x49C712: mov      dword ptr [edx + 0x80], eax
0x49C718: mov      eax, dword ptr [ebp - 4]
0x49C71B: mov      byte ptr [eax + 0x65], 0
0x49C71F: mov      al, byte ptr [ebp + 0xc]
0x49C722: mov      edx, dword ptr [ebp - 4]
0x49C725: mov      byte ptr [edx + 0x64], al
0x49C728: mov      eax, dword ptr [ebp - 4]
0x49C72B: mov      dword ptr [eax + 0x70], 0xa
0x49C732: jmp      0x49c7f3
0x49C737: mov      eax, dword ptr [ebp - 4]
0x49C73A: xor      edx, edx
0x49C73C: mov      dword ptr [eax + 0x7c], edx
0x49C73F: mov      eax, dword ptr [ebp - 4]
0x49C742: mov      dword ptr [eax + 0x84], 0xffffffff
0x49C74C: mov      eax, dword ptr [ebp - 4]
0x49C74F: mov      dword ptr [eax + 0x70], 0xa
0x49C756: mov      eax, dword ptr [ebp - 4]
0x49C759: mov      eax, dword ptr [eax + 0x7c]
0x49C75C: mov      edx, dword ptr [ebp - 4]
0x49C75F: mov      dword ptr [edx + 0x80], eax
0x49C765: mov      eax, dword ptr [ebp - 4]
0x49C768: mov      byte ptr [eax + 0x65], 1
0x49C76C: mov      eax, dword ptr [ebp - 4]
0x49C76F: mov      byte ptr [eax + 0x64], 0
0x49C773: jmp      0x49c7f3
0x49C775: mov      eax, dword ptr [ebp - 4]
0x49C778: xor      edx, edx
0x49C77A: mov      dword ptr [eax + 0x7c], edx
0x49C77D: mov      eax, dword ptr [ebp - 4]
0x49C780: mov      dword ptr [eax + 0x84], 3
0x49C78A: mov      eax, dword ptr [ebp - 4]
0x49C78D: mov      eax, dword ptr [eax + 0x7c]
0x49C790: mov      edx, dword ptr [ebp - 4]
0x49C793: mov      dword ptr [edx + 0x80], eax
0x49C799: mov      eax, dword ptr [ebp - 4]
0x49C79C: mov      byte ptr [eax + 0x65], 0
0x49C7A0: mov      al, byte ptr [ebp + 0xc]
0x49C7A3: mov      edx, dword ptr [ebp - 4]
0x49C7A6: mov      byte ptr [edx + 0x64], al
0x49C7A9: mov      eax, dword ptr [ebp - 4]
0x49C7AC: mov      dword ptr [eax + 0x70], 3
0x49C7B3: jmp      0x49c7f3
0x49C7B5: mov      eax, dword ptr [ebp - 4]
0x49C7B8: xor      edx, edx
0x49C7BA: mov      dword ptr [eax + 0x7c], edx
0x49C7BD: mov      eax, dword ptr [ebp - 4]
0x49C7C0: mov      dword ptr [eax + 0x84], 1
0x49C7CA: mov      eax, dword ptr [ebp - 4]
0x49C7CD: mov      eax, dword ptr [eax + 0x7c]
0x49C7D0: mov      edx, dword ptr [ebp - 4]
0x49C7D3: mov      dword ptr [edx + 0x80], eax
0x49C7D9: mov      eax, dword ptr [ebp - 4]
0x49C7DC: mov      byte ptr [eax + 0x65], 0
0x49C7E0: mov      al, byte ptr [ebp + 0xc]
0x49C7E3: mov      edx, dword ptr [ebp - 4]
0x49C7E6: mov      byte ptr [edx + 0x64], al
0x49C7E9: mov      eax, dword ptr [ebp - 4]
0x49C7EC: mov      dword ptr [eax + 0x70], 1
0x49C7F3: mov      eax, dword ptr [ebp - 0xc]
0x49C7F6: mov      edx, dword ptr [ebp - 4]
0x49C7F9: mov      dword ptr [edx + 8], eax
0x49C7FC: mov      eax, dword ptr [ebp + 0x24]
0x49C7FF: mov      edx, dword ptr [ebp - 4]
0x49C802: mov      dword ptr [edx + 0x18], eax
0x49C805: mov      eax, dword ptr [ebp + 0x18]
0x49C808: mov      edx, dword ptr [ebp - 4]
0x49C80B: mov      dword ptr [edx + 0x34], eax
0x49C80E: mov      eax, dword ptr [ebp + 0x14]
0x49C811: mov      edx, dword ptr [ebp - 4]
0x49C814: mov      dword ptr [edx + 0x38], eax
0x49C817: mov      eax, dword ptr [ebp + 0x20]
0x49C81A: mov      edx, dword ptr [ebp - 4]
0x49C81D: mov      dword ptr [edx + 0x98], eax
0x49C823: mov      eax, dword ptr [ebp + 0x1c]
0x49C826: mov      edx, dword ptr [ebp - 4]
0x49C829: mov      dword ptr [edx + 0x9c], eax
0x49C82F: mov      eax, dword ptr [ebp + 0x20]
0x49C832: mov      edx, dword ptr [ebp - 4]
0x49C835: mov      dword ptr [edx + 0x44], eax
0x49C838: mov      eax, dword ptr [ebp + 0x1c]
0x49C83B: mov      edx, dword ptr [ebp - 4]
0x49C83E: mov      dword ptr [edx + 0x48], eax
0x49C841: mov      eax, dword ptr [ebp + 0x20]
0x49C844: mov      edx, dword ptr [ebp - 4]
0x49C847: mov      dword ptr [edx + 0x4c], eax
0x49C84A: mov      eax, dword ptr [ebp + 0x1c]
0x49C84D: mov      edx, dword ptr [ebp - 4]
0x49C850: mov      dword ptr [edx + 0x50], eax
0x49C853: fild     dword ptr [ebp + 0x20]
0x49C856: mov      eax, dword ptr [ebp - 4]
0x49C859: fstp     qword ptr [eax + 0x54]
0x49C85C: wait     
0x49C85D: fild     dword ptr [ebp + 0x1c]
0x49C860: mov      eax, dword ptr [ebp - 4]
0x49C863: fstp     qword ptr [eax + 0x5c]
0x49C866: wait     
0x49C867: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49C86C: mov      eax, dword ptr [eax]
0x49C86E: mov      eax, dword ptr [eax + 0xb8]
0x49C874: shl      eax, 4
0x49C877: lea      eax, [eax + eax*2]
0x49C87A: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49C880: mov      edx, dword ptr [edx]
0x49C882: add      eax, dword ptr [edx + 0x98]
0x49C888: mov      edx, dword ptr [ebp - 4]
0x49C88B: mov      dword ptr [edx + 0xb0], eax
0x49C891: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49C896: mov      eax, dword ptr [eax]
0x49C898: mov      eax, dword ptr [eax + 0xbc]
0x49C89E: shl      eax, 5
0x49C8A1: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49C8A7: mov      edx, dword ptr [edx]
0x49C8A9: add      eax, dword ptr [edx + 0x9c]
0x49C8AF: mov      edx, dword ptr [ebp - 4]
0x49C8B2: mov      dword ptr [edx + 0xb4], eax
0x49C8B8: mov      eax, dword ptr [ebp - 4]
0x49C8BB: mov      eax, dword ptr [eax + 0x18]
0x49C8BE: add      eax, 0xaa
0x49C8C3: mov      edx, dword ptr [ebp - 4]
0x49C8C6: mov      dword ptr [edx + 0x1c], eax
0x49C8C9: mov      eax, dword ptr [ebp - 4]
0x49C8CC: mov      dword ptr [eax + 0x78], 1
0x49C8D3: mov      eax, dword ptr [ebp - 4]
0x49C8D6: mov      eax, dword ptr [eax + 0x98]
0x49C8DC: mov      edx, dword ptr [ebp - 4]
0x49C8DF: cmp      eax, dword ptr [edx + 0x34]
0x49C8E2: je       0x49c8fd
0x49C8E4: mov      eax, dword ptr [ebp - 4]
0x49C8E7: mov      eax, dword ptr [eax + 0x34]
0x49C8EA: mov      edx, dword ptr [ebp - 4]
0x49C8ED: sub      eax, dword ptr [edx + 0x98]
0x49C8F3: cdq      
0x49C8F4: xor      eax, edx
0x49C8F6: sub      eax, edx
0x49C8F8: mov      dword ptr [ebp - 0x10], eax
0x49C8FB: jmp      0x49c904
0x49C8FD: mov      dword ptr [ebp - 0x10], 1
0x49C904: mov      eax, dword ptr [ebp - 4]
0x49C907: mov      eax, dword ptr [eax + 0x9c]
0x49C90D: mov      edx, dword ptr [ebp - 4]
0x49C910: cmp      eax, dword ptr [edx + 0x38]
0x49C913: je       0x49c92e
0x49C915: mov      eax, dword ptr [ebp - 4]
0x49C918: mov      eax, dword ptr [eax + 0x38]
0x49C91B: mov      edx, dword ptr [ebp - 4]
0x49C91E: sub      eax, dword ptr [edx + 0x9c]
0x49C924: cdq      
0x49C925: xor      eax, edx
0x49C927: sub      eax, edx
0x49C929: mov      dword ptr [ebp - 0x14], eax
0x49C92C: jmp      0x49c935
0x49C92E: mov      dword ptr [ebp - 0x14], 1
0x49C935: mov      eax, dword ptr [ebp - 4]
0x49C938: mov      eax, dword ptr [eax + 0x98]
0x49C93E: mov      edx, dword ptr [ebp - 4]
0x49C941: sub      eax, dword ptr [edx + 0x34]
0x49C944: cdq      
0x49C945: xor      eax, edx
0x49C947: sub      eax, edx
0x49C949: mov      ecx, eax
0x49C94B: mov      eax, dword ptr [ebp - 4]
0x49C94E: mov      eax, dword ptr [eax + 0x9c]
0x49C954: mov      edx, dword ptr [ebp - 4]
0x49C957: sub      eax, dword ptr [edx + 0x38]
0x49C95A: cdq      
0x49C95B: xor      eax, edx
0x49C95D: sub      eax, edx
0x49C95F: cmp      ecx, eax
0x49C961: jle      0x49c9c1
0x49C963: fild     dword ptr [ebp - 0x10]
0x49C966: fdivr    dword ptr [0x49cacc]
0x49C96C: mov      eax, dword ptr [ebp - 4]
0x49C96F: mov      eax, dword ptr [eax + 0x34]
0x49C972: mov      edx, dword ptr [ebp - 4]
0x49C975: sub      eax, dword ptr [edx + 0x98]
0x49C97B: mov      dword ptr [ebp - 0x18], eax
0x49C97E: fild     dword ptr [ebp - 0x18]
0x49C981: fmulp    st(1)
0x49C983: call     0x4029a0
0x49C988: mov      edx, dword ptr [ebp - 4]
0x49C98B: mov      dword ptr [edx + 0xa0], eax
0x49C991: fild     dword ptr [ebp - 0x10]
0x49C994: fdivr    dword ptr [0x49cacc]
0x49C99A: mov      eax, dword ptr [ebp - 4]
0x49C99D: mov      eax, dword ptr [eax + 0x38]
0x49C9A0: mov      edx, dword ptr [ebp - 4]
0x49C9A3: sub      eax, dword ptr [edx + 0x9c]
0x49C9A9: mov      dword ptr [ebp - 0x18], eax
0x49C9AC: fild     dword ptr [ebp - 0x18]
0x49C9AF: fmulp    st(1)
0x49C9B1: call     0x4029a0
0x49C9B6: mov      edx, dword ptr [ebp - 4]
0x49C9B9: mov      dword ptr [edx + 0xa4], eax
0x49C9BF: jmp      0x49ca1d
0x49C9C1: fild     dword ptr [ebp - 0x14]
0x49C9C4: fdivr    dword ptr [0x49cacc]
0x49C9CA: mov      eax, dword ptr [ebp - 4]
0x49C9CD: mov      eax, dword ptr [eax + 0x34]
0x49C9D0: mov      edx, dword ptr [ebp - 4]
0x49C9D3: sub      eax, dword ptr [edx + 0x98]
0x49C9D9: mov      dword ptr [ebp - 0x18], eax
0x49C9DC: fild     dword ptr [ebp - 0x18]
0x49C9DF: fmulp    st(1)
0x49C9E1: call     0x4029a0
0x49C9E6: mov      edx, dword ptr [ebp - 4]
0x49C9E9: mov      dword ptr [edx + 0xa0], eax
0x49C9EF: fild     dword ptr [ebp - 0x14]
0x49C9F2: fdivr    dword ptr [0x49cacc]
0x49C9F8: mov      eax, dword ptr [ebp - 4]
0x49C9FB: mov      eax, dword ptr [eax + 0x38]
0x49C9FE: mov      edx, dword ptr [ebp - 4]
0x49CA01: sub      eax, dword ptr [edx + 0x9c]
0x49CA07: mov      dword ptr [ebp - 0x18], eax
0x49CA0A: fild     dword ptr [ebp - 0x18]
0x49CA0D: fmulp    st(1)
0x49CA0F: call     0x4029a0
0x49CA14: mov      edx, dword ptr [ebp - 4]
0x49CA17: mov      dword ptr [edx + 0xa4], eax
0x49CA1D: mov      eax, dword ptr [ebp - 4]
0x49CA20: mov      dword ptr [eax + 0x74], 0x32
0x49CA27: call     0x406154
0x49CA2C: mov      edx, dword ptr [ebp - 4]
0x49CA2F: mov      dword ptr [edx + 0x88], eax
0x49CA35: call     0x406154
0x49CA3A: mov      edx, dword ptr [ebp - 4]
0x49CA3D: mov      dword ptr [edx + 0x8c], eax
0x49CA43: call     0x406154
0x49CA48: mov      edx, dword ptr [ebp - 4]
0x49CA4B: mov      dword ptr [edx + 0x94], eax
0x49CA51: mov      eax, dword ptr [ebp + 8]
0x49CA54: mov      edx, dword ptr [ebp - 4]
0x49CA57: mov      dword ptr [edx + 0x90], eax
0x49CA5D: mov      eax, dword ptr [ebp + 0x14]
0x49CA60: push     eax
0x49CA61: mov      ecx, dword ptr [ebp + 0x18]
0x49CA64: mov      edx, dword ptr [ebp + 0x1c]
0x49CA67: mov      eax, dword ptr [ebp + 0x20]
0x49CA6A: call     0x4a0630
0x49CA6F: mov      edx, dword ptr [ebp - 4]
0x49CA72: mov      byte ptr [edx + 0x30], al
0x49CA75: mov      eax, dword ptr [ebp - 4]
0x49CA78: mov      al, byte ptr [eax + 0x30]
0x49CA7B: mov      edx, dword ptr [ebp - 4]
0x49CA7E: mov      byte ptr [edx + 0x31], al
0x49CA81: mov      eax, dword ptr [ebp - 4]
0x49CA84: xor      edx, edx
0x49CA86: mov      dword ptr [eax + 0x6c], edx
0x49CA89: mov      eax, dword ptr [ebp - 4]
0x49CA8C: mov      byte ptr [eax + 4], 1
0x49CA90: mov      eax, dword ptr [ebp - 4]
0x49CA93: mov      dword ptr [eax + 0xb8], 0x1869f
0x49CA9D: mov      eax, dword ptr [ebp - 4]
0x49CAA0: mov      dword ptr [eax + 0xbc], 0x1869f
0x49CAAA: mov      eax, dword ptr [ebp - 4]
0x49CAAD: cmp      byte ptr [ebp - 5], 0
0x49CAB1: je       0x49cac2
0x49CAB3: call     0x4031fc
0x49CAB8: pop      dword ptr fs:[0]
0x49CABF: add      esp, 0xc
0x49CAC2: mov      eax, dword ptr [ebp - 4]
0x49CAC5: mov      esp, ebp
0x49CAC7: pop      ebp
0x49CAC8: ret      0x20
0x49CACB: add      byte ptr [eax], al
0x49CACD: add      dl, bh
0x49CACF: inc      ebx
0x49CAD0: push     ebp
0x49CAD1: mov      ebp, esp
0x49CAD3: add      esp, -8
0x49CAD6: call     0x403204
0x49CADB: mov      byte ptr [ebp - 5], dl
0x49CADE: mov      dword ptr [ebp - 4], eax
0x49CAE1: mov      dl, byte ptr [ebp - 5]
0x49CAE4: and      dl, 0xfc
0x49CAE7: mov      eax, dword ptr [ebp - 4]
0x49CAEA: call     0x402e9c
0x49CAEF: cmp      byte ptr [ebp - 5], 0
0x49CAF3: jle      0x49cafd
0x49CAF5: mov      eax, dword ptr [ebp - 4]
0x49CAF8: call     0x4031f4
0x49CAFD: pop      ecx
0x49CAFE: pop      ecx
0x49CAFF: pop      ebp
0x49CB00: ret      
0x49CB01: lea      eax, [eax]
0x49CB04: push     ebp
0x49CB05: mov      ebp, esp
0x49CB07: add      esp, -0xc
0x49CB0A: mov      dword ptr [ebp - 8], edx
0x49CB0D: mov      dword ptr [ebp - 4], eax
0x49CB10: mov      byte ptr [ebp - 9], 0
0x49CB14: mov      eax, dword ptr [ebp - 4]
0x49CB17: sub      eax, dword ptr [ebp - 8]
0x49CB1A: cdq      
0x49CB1B: xor      eax, edx
0x49CB1D: sub      eax, edx
0x49CB1F: cmp      eax, 2
0x49CB22: jl       0x49cb44
0x49CB24: mov      byte ptr [ebp - 9], 1
0x49CB28: cmp      dword ptr [ebp - 4], 0
0x49CB2C: jne      0x49cb34
0x49CB2E: cmp      dword ptr [ebp - 8], 0xf
0x49CB32: je       0x49cb40
0x49CB34: cmp      dword ptr [ebp - 4], 0xf
0x49CB38: jne      0x49cb44
0x49CB3A: cmp      dword ptr [ebp - 8], 0
0x49CB3E: jne      0x49cb44
0x49CB40: mov      byte ptr [ebp - 9], 0
0x49CB44: mov      al, byte ptr [ebp - 9]
0x49CB47: mov      esp, ebp
0x49CB49: pop      ebp
0x49CB4A: ret      
0x49CB4B: nop      
0x49CB4C: push     ebp
0x49CB4D: mov      ebp, esp
0x49CB4F: add      esp, -0x58
0x49CB52: mov      dword ptr [ebp - 4], eax
0x49CB55: mov      byte ptr [ebp - 5], 1
0x49CB59: mov      eax, dword ptr [ebp - 4]
0x49CB5C: cmp      byte ptr [eax + 0x64], 0
0x49CB60: je       0x49cbc3
0x49CB62: call     0x406154
0x49CB67: mov      edx, dword ptr [ebp - 4]
0x49CB6A: sub      eax, dword ptr [edx + 0x94]
0x49CB70: mov      edx, dword ptr [ebp - 4]
0x49CB73: cmp      eax, dword ptr [edx + 0x74]
0x49CB76: jbe      0x49cc34
0x49CB7C: call     0x406154
0x49CB81: mov      edx, dword ptr [ebp - 4]
0x49CB84: mov      dword ptr [edx + 0x94], eax
0x49CB8A: mov      eax, dword ptr [ebp - 4]
0x49CB8D: inc      dword ptr [eax + 0x80]
0x49CB93: mov      eax, dword ptr [ebp - 4]
0x49CB96: mov      eax, dword ptr [eax + 0x7c]
0x49CB99: mov      edx, dword ptr [ebp - 4]
0x49CB9C: add      eax, dword ptr [edx + 0x84]
0x49CBA2: dec      eax
0x49CBA3: mov      edx, dword ptr [ebp - 4]
0x49CBA6: cmp      eax, dword ptr [edx + 0x80]
0x49CBAC: jge      0x49cc34
0x49CBB2: mov      eax, dword ptr [ebp - 4]
0x49CBB5: mov      eax, dword ptr [eax + 0x7c]
0x49CBB8: mov      edx, dword ptr [ebp - 4]
0x49CBBB: mov      dword ptr [edx + 0x80], eax
0x49CBC1: jmp      0x49cc34
0x49CBC3: mov      eax, dword ptr [ebp - 4]
0x49CBC6: cmp      dword ptr [eax + 0x84], 0
0x49CBCD: jle      0x49cc34
0x49CBCF: call     0x406154
0x49CBD4: mov      edx, dword ptr [ebp - 4]
0x49CBD7: sub      eax, dword ptr [edx + 0x94]
0x49CBDD: mov      edx, dword ptr [ebp - 4]
0x49CBE0: cmp      eax, dword ptr [edx + 0x74]
0x49CBE3: jbe      0x49cc34
0x49CBE5: call     0x406154
0x49CBEA: mov      edx, dword ptr [ebp - 4]
0x49CBED: mov      dword ptr [edx + 0x94], eax
0x49CBF3: mov      eax, dword ptr [ebp - 4]
0x49CBF6: inc      dword ptr [eax + 0x80]
0x49CBFC: mov      eax, dword ptr [ebp - 4]
0x49CBFF: mov      eax, dword ptr [eax + 0x7c]
0x49CC02: mov      edx, dword ptr [ebp - 4]
0x49CC05: add      eax, dword ptr [edx + 0x84]
0x49CC0B: dec      eax
0x49CC0C: mov      edx, dword ptr [ebp - 4]
0x49CC0F: cmp      eax, dword ptr [edx + 0x80]
0x49CC15: jge      0x49cc34
0x49CC17: mov      eax, dword ptr [ebp - 4]
0x49CC1A: mov      eax, dword ptr [eax + 0x7c]
0x49CC1D: mov      edx, dword ptr [ebp - 4]
0x49CC20: add      eax, dword ptr [edx + 0x84]
0x49CC26: dec      eax
0x49CC27: mov      edx, dword ptr [ebp - 4]
0x49CC2A: mov      dword ptr [edx + 0x80], eax
0x49CC30: mov      byte ptr [ebp - 5], 0
0x49CC34: mov      eax, dword ptr [ebp - 4]
0x49CC37: cmp      byte ptr [eax + 0x65], 0
0x49CC3B: jne      0x49d1bb
0x49CC41: mov      byte ptr [ebp - 0x31], 0
0x49CC45: mov      eax, dword ptr [ebp - 4]
0x49CC48: cmp      dword ptr [eax + 0x10], 0
0x49CC4C: je       0x49d0b0
0x49CC52: call     0x406154
0x49CC57: mov      edx, dword ptr [ebp - 4]
0x49CC5A: sub      eax, dword ptr [edx + 0x88]
0x49CC60: mov      dword ptr [ebp - 0x14], eax
0x49CC63: call     0x406154
0x49CC68: mov      edx, dword ptr [ebp - 4]
0x49CC6B: mov      dword ptr [edx + 0x88], eax
0x49CC71: mov      eax, dword ptr [ebp - 4]
0x49CC74: add      eax, 0x34
0x49CC77: push     eax
0x49CC78: mov      eax, dword ptr [ebp - 4]
0x49CC7B: add      eax, 0x38
0x49CC7E: push     eax
0x49CC7F: mov      eax, dword ptr [ebp - 4]
0x49CC82: mov      eax, dword ptr [eax + 0x10]
0x49CC85: mov      ecx, dword ptr [eax + 0xbc]
0x49CC8B: mov      eax, dword ptr [ebp - 4]
0x49CC8E: mov      eax, dword ptr [eax + 0x10]
0x49CC91: mov      edx, dword ptr [eax + 0xb8]
0x49CC97: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x49CC9C: mov      eax, dword ptr [eax]
0x49CC9E: call     0x472494
0x49CCA3: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49CCA8: mov      eax, dword ptr [eax]
0x49CCAA: mov      eax, dword ptr [eax + 0xb8]
0x49CCB0: shl      eax, 4
0x49CCB3: lea      eax, [eax + eax*2]
0x49CCB6: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49CCBC: mov      edx, dword ptr [edx]
0x49CCBE: add      eax, dword ptr [edx + 0x98]
0x49CCC4: mov      edx, dword ptr [ebp - 4]
0x49CCC7: sub      eax, dword ptr [edx + 0xb0]
0x49CCCD: mov      dword ptr [ebp - 0x28], eax
0x49CCD0: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49CCD5: mov      eax, dword ptr [eax]
0x49CCD7: mov      eax, dword ptr [eax + 0xbc]
0x49CCDD: shl      eax, 5
0x49CCE0: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49CCE6: mov      edx, dword ptr [edx]
0x49CCE8: add      eax, dword ptr [edx + 0x9c]
0x49CCEE: mov      edx, dword ptr [ebp - 4]
0x49CCF1: sub      eax, dword ptr [edx + 0xb4]
0x49CCF7: mov      dword ptr [ebp - 0x2c], eax
0x49CCFA: mov      eax, dword ptr [ebp - 0x28]
0x49CCFD: mov      edx, dword ptr [ebp - 4]
0x49CD00: add      dword ptr [edx + 0x34], eax
0x49CD03: mov      eax, dword ptr [ebp - 0x2c]
0x49CD06: mov      edx, dword ptr [ebp - 4]
0x49CD09: add      dword ptr [edx + 0x38], eax
0x49CD0C: mov      eax, dword ptr [ebp - 4]
0x49CD0F: mov      eax, dword ptr [eax + 0x44]
0x49CD12: mov      edx, dword ptr [ebp - 4]
0x49CD15: cmp      eax, dword ptr [edx + 0x34]
0x49CD18: je       0x49cd30
0x49CD1A: mov      eax, dword ptr [ebp - 4]
0x49CD1D: mov      eax, dword ptr [eax + 0x34]
0x49CD20: mov      edx, dword ptr [ebp - 4]
0x49CD23: sub      eax, dword ptr [edx + 0x44]
0x49CD26: cdq      
0x49CD27: xor      eax, edx
0x49CD29: sub      eax, edx
0x49CD2B: mov      dword ptr [ebp - 0x20], eax
0x49CD2E: jmp      0x49cd37
0x49CD30: mov      dword ptr [ebp - 0x20], 1
0x49CD37: mov      eax, dword ptr [ebp - 4]
0x49CD3A: mov      eax, dword ptr [eax + 0x48]
0x49CD3D: mov      edx, dword ptr [ebp - 4]
0x49CD40: cmp      eax, dword ptr [edx + 0x38]
0x49CD43: je       0x49cd5b
0x49CD45: mov      eax, dword ptr [ebp - 4]
0x49CD48: mov      eax, dword ptr [eax + 0x38]
0x49CD4B: mov      edx, dword ptr [ebp - 4]
0x49CD4E: sub      eax, dword ptr [edx + 0x48]
0x49CD51: cdq      
0x49CD52: xor      eax, edx
0x49CD54: sub      eax, edx
0x49CD56: mov      dword ptr [ebp - 0x24], eax
0x49CD59: jmp      0x49cd62
0x49CD5B: mov      dword ptr [ebp - 0x24], 1
0x49CD62: mov      eax, dword ptr [ebp - 4]
0x49CD65: mov      eax, dword ptr [eax + 0x44]
0x49CD68: mov      edx, dword ptr [ebp - 4]
0x49CD6B: sub      eax, dword ptr [edx + 0x34]
0x49CD6E: cdq      
0x49CD6F: xor      eax, edx
0x49CD71: sub      eax, edx
0x49CD73: mov      ecx, eax
0x49CD75: mov      eax, dword ptr [ebp - 4]
0x49CD78: mov      eax, dword ptr [eax + 0x48]
0x49CD7B: mov      edx, dword ptr [ebp - 4]
0x49CD7E: sub      eax, dword ptr [edx + 0x38]
0x49CD81: cdq      
0x49CD82: xor      eax, edx
0x49CD84: sub      eax, edx
0x49CD86: cmp      ecx, eax
0x49CD88: jle      0x49cde2
0x49CD8A: fild     dword ptr [ebp - 0x20]
0x49CD8D: fdivr    dword ptr [0x49d2fc]
0x49CD93: mov      eax, dword ptr [ebp - 4]
0x49CD96: mov      eax, dword ptr [eax + 0x34]
0x49CD99: mov      edx, dword ptr [ebp - 4]
0x49CD9C: sub      eax, dword ptr [edx + 0x44]
0x49CD9F: mov      dword ptr [ebp - 0x58], eax
0x49CDA2: fild     dword ptr [ebp - 0x58]
0x49CDA5: fmulp    st(1)
0x49CDA7: call     0x4029a0
0x49CDAC: mov      edx, dword ptr [ebp - 4]
0x49CDAF: mov      dword ptr [edx + 0xa8], eax
0x49CDB5: fild     dword ptr [ebp - 0x20]
0x49CDB8: fdivr    dword ptr [0x49d2fc]
0x49CDBE: mov      eax, dword ptr [ebp - 4]
0x49CDC1: mov      eax, dword ptr [eax + 0x38]
0x49CDC4: mov      edx, dword ptr [ebp - 4]
0x49CDC7: sub      eax, dword ptr [edx + 0x48]
0x49CDCA: mov      dword ptr [ebp - 0x58], eax
0x49CDCD: fild     dword ptr [ebp - 0x58]
0x49CDD0: fmulp    st(1)
0x49CDD2: call     0x4029a0
0x49CDD7: mov      edx, dword ptr [ebp - 4]
0x49CDDA: mov      dword ptr [edx + 0xac], eax
0x49CDE0: jmp      0x49ce38
0x49CDE2: fild     dword ptr [ebp - 0x24]
0x49CDE5: fdivr    dword ptr [0x49d2fc]
0x49CDEB: mov      eax, dword ptr [ebp - 4]
0x49CDEE: mov      eax, dword ptr [eax + 0x34]
0x49CDF1: mov      edx, dword ptr [ebp - 4]
0x49CDF4: sub      eax, dword ptr [edx + 0x44]
0x49CDF7: mov      dword ptr [ebp - 0x58], eax
0x49CDFA: fild     dword ptr [ebp - 0x58]
0x49CDFD: fmulp    st(1)
0x49CDFF: call     0x4029a0
0x49CE04: mov      edx, dword ptr [ebp - 4]
0x49CE07: mov      dword ptr [edx + 0xa8], eax
0x49CE0D: fild     dword ptr [ebp - 0x24]
0x49CE10: fdivr    dword ptr [0x49d2fc]
0x49CE16: mov      eax, dword ptr [ebp - 4]
0x49CE19: mov      eax, dword ptr [eax + 0x38]
0x49CE1C: mov      edx, dword ptr [ebp - 4]
0x49CE1F: sub      eax, dword ptr [edx + 0x48]
0x49CE22: mov      dword ptr [ebp - 0x58], eax
0x49CE25: fild     dword ptr [ebp - 0x58]
0x49CE28: fmulp    st(1)
0x49CE2A: call     0x4029a0
0x49CE2F: mov      edx, dword ptr [ebp - 4]
0x49CE32: mov      dword ptr [edx + 0xac], eax
0x49CE38: mov      eax, dword ptr [ebp - 4]
0x49CE3B: mov      eax, dword ptr [eax + 0xa0]
0x49CE41: mov      edx, dword ptr [ebp - 4]
0x49CE44: cmp      eax, dword ptr [edx + 0xa8]
0x49CE4A: jge      0x49ce7b
0x49CE4C: mov      eax, dword ptr [ebp - 4]
0x49CE4F: mov      eax, dword ptr [eax + 0xa8]
0x49CE55: mov      edx, dword ptr [ebp - 4]
0x49CE58: sub      eax, dword ptr [edx + 0xa0]
0x49CE5E: mov      ecx, 0xa
0x49CE63: cdq      
0x49CE64: idiv     ecx
0x49CE66: mov      edx, eax
0x49CE68: mov      eax, 1
0x49CE6D: call     0x452bbc
0x49CE72: mov      edx, dword ptr [ebp - 4]
0x49CE75: add      dword ptr [edx + 0xa0], eax
0x49CE7B: mov      eax, dword ptr [ebp - 4]
0x49CE7E: mov      eax, dword ptr [eax + 0xa0]
0x49CE84: mov      edx, dword ptr [ebp - 4]
0x49CE87: cmp      eax, dword ptr [edx + 0xa8]
0x49CE8D: jle      0x49cebe
0x49CE8F: mov      eax, dword ptr [ebp - 4]
0x49CE92: mov      eax, dword ptr [eax + 0xa0]
0x49CE98: mov      edx, dword ptr [ebp - 4]
0x49CE9B: sub      eax, dword ptr [edx + 0xa8]
0x49CEA1: mov      ecx, 0xa
0x49CEA6: cdq      
0x49CEA7: idiv     ecx
0x49CEA9: mov      edx, eax
0x49CEAB: mov      eax, 1
0x49CEB0: call     0x452bbc
0x49CEB5: mov      edx, dword ptr [ebp - 4]
0x49CEB8: sub      dword ptr [edx + 0xa0], eax
0x49CEBE: mov      eax, dword ptr [ebp - 4]
0x49CEC1: mov      eax, dword ptr [eax + 0xa4]
0x49CEC7: mov      edx, dword ptr [ebp - 4]
0x49CECA: cmp      eax, dword ptr [edx + 0xac]
0x49CED0: jge      0x49cf01
0x49CED2: mov      eax, dword ptr [ebp - 4]
0x49CED5: mov      eax, dword ptr [eax + 0xac]
0x49CEDB: mov      edx, dword ptr [ebp - 4]
0x49CEDE: sub      eax, dword ptr [edx + 0xa4]
0x49CEE4: mov      ecx, 0xa
0x49CEE9: cdq      
0x49CEEA: idiv     ecx
0x49CEEC: mov      edx, eax
0x49CEEE: mov      eax, 1
0x49CEF3: call     0x452bbc
0x49CEF8: mov      edx, dword ptr [ebp - 4]
0x49CEFB: add      dword ptr [edx + 0xa4], eax
0x49CF01: mov      eax, dword ptr [ebp - 4]
0x49CF04: mov      eax, dword ptr [eax + 0xa4]
0x49CF0A: mov      edx, dword ptr [ebp - 4]
0x49CF0D: cmp      eax, dword ptr [edx + 0xac]
0x49CF13: jle      0x49cf44
0x49CF15: mov      eax, dword ptr [ebp - 4]
0x49CF18: mov      eax, dword ptr [eax + 0xa4]
0x49CF1E: mov      edx, dword ptr [ebp - 4]
0x49CF21: sub      eax, dword ptr [edx + 0xac]
0x49CF27: mov      ecx, 0xa
0x49CF2C: cdq      
0x49CF2D: idiv     ecx
0x49CF2F: mov      edx, eax
0x49CF31: mov      eax, 1
0x49CF36: call     0x452bbc
0x49CF3B: mov      edx, dword ptr [ebp - 4]
0x49CF3E: sub      dword ptr [edx + 0xa4], eax
0x49CF44: mov      eax, dword ptr [ebp - 4]
0x49CF47: fild     dword ptr [eax + 0xa0]
0x49CF4D: fdiv     dword ptr [0x49d300]
0x49CF53: fild     dword ptr [ebp - 0x14]
0x49CF56: fmulp    st(1)
0x49CF58: fstp     qword ptr [ebp - 0x3c]
0x49CF5B: wait     
0x49CF5C: mov      eax, dword ptr [ebp - 4]
0x49CF5F: fild     dword ptr [eax + 0xa4]
0x49CF65: fdiv     dword ptr [0x49d300]
0x49CF6B: fild     dword ptr [ebp - 0x14]
0x49CF6E: fmulp    st(1)
0x49CF70: fstp     qword ptr [ebp - 0x44]
0x49CF73: wait     
0x49CF74: mov      eax, dword ptr [ebp - 4]
0x49CF77: fld      qword ptr [eax + 0x54]
0x49CF7A: fadd     qword ptr [ebp - 0x3c]
0x49CF7D: mov      eax, dword ptr [ebp - 4]
0x49CF80: fstp     qword ptr [eax + 0x54]
0x49CF83: wait     
0x49CF84: mov      eax, dword ptr [ebp - 4]
0x49CF87: fld      qword ptr [eax + 0x5c]
0x49CF8A: fadd     qword ptr [ebp - 0x44]
0x49CF8D: mov      eax, dword ptr [ebp - 4]
0x49CF90: fstp     qword ptr [eax + 0x5c]
0x49CF93: wait     
0x49CF94: mov      eax, dword ptr [ebp - 4]
0x49CF97: fld      qword ptr [eax + 0x54]
0x49CF9A: call     0x4029a0
0x49CF9F: mov      edx, dword ptr [ebp - 4]
0x49CFA2: mov      dword ptr [edx + 0x44], eax
0x49CFA5: mov      eax, dword ptr [ebp - 4]
0x49CFA8: fld      qword ptr [eax + 0x5c]
0x49CFAB: call     0x4029a0
0x49CFB0: mov      edx, dword ptr [ebp - 4]
0x49CFB3: mov      dword ptr [edx + 0x48], eax
0x49CFB6: mov      eax, dword ptr [ebp - 4]
0x49CFB9: mov      eax, dword ptr [eax + 0x44]
0x49CFBC: mov      edx, dword ptr [ebp - 4]
0x49CFBF: mov      dword ptr [edx + 0x4c], eax
0x49CFC2: mov      eax, dword ptr [ebp - 4]
0x49CFC5: mov      eax, dword ptr [eax + 0x48]
0x49CFC8: mov      edx, dword ptr [ebp - 4]
0x49CFCB: mov      dword ptr [edx + 0x50], eax
0x49CFCE: mov      eax, dword ptr [ebp - 4]
0x49CFD1: mov      eax, dword ptr [eax + 0x38]
0x49CFD4: push     eax
0x49CFD5: mov      eax, dword ptr [ebp - 4]
0x49CFD8: mov      ecx, dword ptr [eax + 0x34]
0x49CFDB: mov      eax, dword ptr [ebp - 4]
0x49CFDE: mov      edx, dword ptr [eax + 0x48]
0x49CFE1: mov      eax, dword ptr [ebp - 4]
0x49CFE4: mov      eax, dword ptr [eax + 0x44]
0x49CFE7: call     0x4a0630
0x49CFEC: mov      dword ptr [ebp - 0x30], eax
0x49CFEF: mov      eax, dword ptr [ebp - 4]
0x49CFF2: mov      eax, dword ptr [eax + 0x34]
0x49CFF5: mov      edx, dword ptr [ebp - 4]
0x49CFF8: sub      eax, dword ptr [edx + 0x44]
0x49CFFB: cdq      
0x49CFFC: xor      eax, edx
0x49CFFE: sub      eax, edx
0x49D000: cmp      eax, 0xf
0x49D003: jg       0x49d01b
0x49D005: mov      eax, dword ptr [ebp - 4]
0x49D008: mov      eax, dword ptr [eax + 0x38]
0x49D00B: mov      edx, dword ptr [ebp - 4]
0x49D00E: sub      eax, dword ptr [edx + 0x48]
0x49D011: cdq      
0x49D012: xor      eax, edx
0x49D014: sub      eax, edx
0x49D016: cmp      eax, 0xf
0x49D019: jle      0x49d068
0x49D01B: mov      eax, dword ptr [ebp - 4]
0x49D01E: mov      eax, dword ptr [eax + 0x34]
0x49D021: mov      edx, dword ptr [ebp - 4]
0x49D024: sub      eax, dword ptr [edx + 0x44]
0x49D027: cdq      
0x49D028: xor      eax, edx
0x49D02A: sub      eax, edx
0x49D02C: mov      edx, dword ptr [ebp - 4]
0x49D02F: cmp      eax, dword ptr [edx + 0xb8]
0x49D035: jl       0x49d053
0x49D037: mov      eax, dword ptr [ebp - 4]
0x49D03A: mov      eax, dword ptr [eax + 0x38]
0x49D03D: mov      edx, dword ptr [ebp - 4]
0x49D040: sub      eax, dword ptr [edx + 0x48]
0x49D043: cdq      
0x49D044: xor      eax, edx
0x49D046: sub      eax, edx
0x49D048: mov      edx, dword ptr [ebp - 4]
0x49D04B: cmp      eax, dword ptr [edx + 0xbc]
0x49D051: jge      0x49d068
0x49D053: push     ebp
0x49D054: mov      eax, dword ptr [ebp - 4]
0x49D057: movzx    eax, byte ptr [eax + 0x31]
0x49D05B: mov      edx, dword ptr [ebp - 0x30]
0x49D05E: call     0x49cb04
0x49D063: pop      ecx
0x49D064: test     al, al
0x49D066: je       0x49d06e
0x49D068: mov      byte ptr [ebp - 0x31], 1
0x49D06C: jmp      0x49d0a2
0x49D06E: mov      eax, dword ptr [ebp - 4]
0x49D071: mov      eax, dword ptr [eax + 0x34]
0x49D074: mov      edx, dword ptr [ebp - 4]
0x49D077: sub      eax, dword ptr [edx + 0x44]
0x49D07A: cdq      
0x49D07B: xor      eax, edx
0x49D07D: sub      eax, edx
0x49D07F: mov      edx, dword ptr [ebp - 4]
0x49D082: mov      dword ptr [edx + 0xb8], eax
0x49D088: mov      eax, dword ptr [ebp - 4]
0x49D08B: mov      eax, dword ptr [eax + 0x38]
0x49D08E: mov      edx, dword ptr [ebp - 4]
0x49D091: sub      eax, dword ptr [edx + 0x48]
0x49D094: cdq      
0x49D095: xor      eax, edx
0x49D097: sub      eax, edx
0x49D099: mov      edx, dword ptr [ebp - 4]
0x49D09C: mov      dword ptr [edx + 0xbc], eax
0x49D0A2: mov      al, byte ptr [ebp - 0x30]
0x49D0A5: mov      edx, dword ptr [ebp - 4]
0x49D0A8: mov      byte ptr [edx + 0x31], al
0x49D0AB: jmp      0x49d141
0x49D0B0: call     0x406154
0x49D0B5: mov      edx, dword ptr [ebp - 4]
0x49D0B8: sub      eax, dword ptr [edx + 0x88]
0x49D0BE: mov      dword ptr [ebp - 0x14], eax
0x49D0C1: mov      eax, dword ptr [ebp - 4]
0x49D0C4: mov      eax, dword ptr [eax + 0x34]
0x49D0C7: mov      edx, dword ptr [ebp - 4]
0x49D0CA: sub      eax, dword ptr [edx + 0x98]
0x49D0D0: mov      dword ptr [ebp - 0xc], eax
0x49D0D3: mov      eax, dword ptr [ebp - 4]
0x49D0D6: mov      eax, dword ptr [eax + 0x38]
0x49D0D9: mov      edx, dword ptr [ebp - 4]
0x49D0DC: sub      eax, dword ptr [edx + 0x9c]
0x49D0E2: mov      dword ptr [ebp - 0x10], eax
0x49D0E5: mov      eax, dword ptr [ebp - 4]
0x49D0E8: fild     dword ptr [eax + 0xa0]
0x49D0EE: fdiv     dword ptr [0x49d304]
0x49D0F4: fild     dword ptr [ebp - 0x14]
0x49D0F7: fmulp    st(1)
0x49D0F9: call     0x4029a0
0x49D0FE: mov      dword ptr [ebp - 0x18], eax
0x49D101: mov      eax, dword ptr [ebp - 4]
0x49D104: fild     dword ptr [eax + 0xa4]
0x49D10A: fdiv     dword ptr [0x49d304]
0x49D110: fild     dword ptr [ebp - 0x14]
0x49D113: fmulp    st(1)
0x49D115: call     0x4029a0
0x49D11A: mov      dword ptr [ebp - 0x1c], eax
0x49D11D: mov      eax, dword ptr [ebp - 4]
0x49D120: mov      eax, dword ptr [eax + 0x98]
0x49D126: add      eax, dword ptr [ebp - 0x18]
0x49D129: mov      edx, dword ptr [ebp - 4]
0x49D12C: mov      dword ptr [edx + 0x44], eax
0x49D12F: mov      eax, dword ptr [ebp - 4]
0x49D132: mov      eax, dword ptr [eax + 0x9c]
0x49D138: add      eax, dword ptr [ebp - 0x1c]
0x49D13B: mov      edx, dword ptr [ebp - 4]
0x49D13E: mov      dword ptr [edx + 0x48], eax
0x49D141: mov      eax, dword ptr [ebp - 4]
0x49D144: add      eax, 0x28
0x49D147: push     eax
0x49D148: mov      eax, dword ptr [ebp - 4]
0x49D14B: add      eax, 0x2c
0x49D14E: push     eax
0x49D14F: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x49D154: mov      eax, dword ptr [eax]
0x49D156: mov      edx, dword ptr [ebp - 4]
0x49D159: mov      ecx, dword ptr [edx + 0x48]
0x49D15C: mov      edx, dword ptr [ebp - 4]
0x49D15F: mov      edx, dword ptr [edx + 0x44]
0x49D162: call     0x472518
0x49D167: cmp      byte ptr [ebp - 0x31], 0
0x49D16B: je       0x49d1bb
0x49D16D: mov      eax, dword ptr [ebp - 4]
0x49D170: cmp      dword ptr [eax + 0x10], 0
0x49D174: je       0x49d1bb
0x49D176: mov      eax, dword ptr [ebp - 4]
0x49D179: mov      byte ptr [eax + 0x65], 1
0x49D17D: mov      eax, dword ptr [ebp - 4]
0x49D180: xor      edx, edx
0x49D182: mov      dword ptr [eax + 0x7c], edx
0x49D185: mov      eax, dword ptr [ebp - 4]
0x49D188: mov      eax, dword ptr [eax + 0x70]
0x49D18B: mov      edx, dword ptr [ebp - 4]
0x49D18E: mov      dword ptr [edx + 0x84], eax
0x49D194: mov      eax, dword ptr [ebp - 4]
0x49D197: mov      eax, dword ptr [eax + 0x7c]
0x49D19A: mov      edx, dword ptr [ebp - 4]
0x49D19D: mov      dword ptr [edx + 0x80], eax
0x49D1A3: mov      eax, dword ptr [ebp - 4]
0x49D1A6: mov      byte ptr [eax + 0x64], 0
0x49D1AA: mov      eax, dword ptr [ebp - 4]
0x49D1AD: mov      eax, dword ptr [eax + 0xc]
0x49D1B0: mov      eax, dword ptr [eax + 0x1ac]
0x49D1B6: call     0x495794
0x49D1BB: mov      eax, dword ptr [ebp - 4]
0x49D1BE: cmp      byte ptr [eax + 0x65], 0
0x49D1C2: je       0x49d2f3
0x49D1C8: mov      eax, dword ptr [ebp - 4]
0x49D1CB: cmp      dword ptr [eax + 0x84], -1
0x49D1D2: jne      0x49d1e3
0x49D1D4: mov      eax, dword ptr [ebp - 4]
0x49D1D7: mov      eax, dword ptr [eax + 0x70]
0x49D1DA: mov      edx, dword ptr [ebp - 4]
0x49D1DD: mov      dword ptr [edx + 0x84], eax
0x49D1E3: mov      eax, dword ptr [ebp - 4]
0x49D1E6: cmp      dword ptr [eax + 0x10], 0
0x49D1EA: jne      0x49d285
0x49D1F0: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49D1F5: mov      eax, dword ptr [eax]
0x49D1F7: mov      eax, dword ptr [eax + 0xb8]
0x49D1FD: shl      eax, 4
0x49D200: lea      eax, [eax + eax*2]
0x49D203: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49D209: mov      edx, dword ptr [edx]
0x49D20B: add      eax, dword ptr [edx + 0x98]
0x49D211: mov      edx, dword ptr [ebp - 4]
0x49D214: sub      eax, dword ptr [edx + 0xb0]
0x49D21A: mov      edx, dword ptr [ebp - 4]
0x49D21D: mov      edx, dword ptr [edx + 0x34]
0x49D220: sub      edx, eax
0x49D222: mov      eax, dword ptr [ebp - 4]
0x49D225: mov      dword ptr [eax + 0x44], edx
0x49D228: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49D22D: mov      eax, dword ptr [eax]
0x49D22F: mov      eax, dword ptr [eax + 0xbc]
0x49D235: shl      eax, 5
0x49D238: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49D23E: mov      edx, dword ptr [edx]
0x49D240: add      eax, dword ptr [edx + 0x9c]
0x49D246: mov      edx, dword ptr [ebp - 4]
0x49D249: sub      eax, dword ptr [edx + 0xb4]
0x49D24F: mov      edx, dword ptr [ebp - 4]
0x49D252: mov      edx, dword ptr [edx + 0x38]
0x49D255: sub      edx, eax
0x49D257: mov      eax, dword ptr [ebp - 4]
0x49D25A: mov      dword ptr [eax + 0x48], edx
0x49D25D: mov      eax, dword ptr [ebp - 4]
0x49D260: add      eax, 0x28
0x49D263: push     eax
0x49D264: mov      eax, dword ptr [ebp - 4]
0x49D267: add      eax, 0x2c
0x49D26A: push     eax
0x49D26B: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x49D270: mov      eax, dword ptr [eax]
0x49D272: mov      edx, dword ptr [ebp - 4]
0x49D275: mov      ecx, dword ptr [edx + 0x48]
0x49D278: mov      edx, dword ptr [ebp - 4]
0x49D27B: mov      edx, dword ptr [edx + 0x44]
0x49D27E: call     0x472518
0x49D283: jmp      0x49d2f3
0x49D285: mov      eax, dword ptr [ebp - 4]
0x49D288: mov      eax, dword ptr [eax + 0x10]
0x49D28B: mov      eax, dword ptr [eax + 0xb8]
0x49D291: mov      edx, dword ptr [ebp - 4]
0x49D294: mov      dword ptr [edx + 0x28], eax
0x49D297: mov      eax, dword ptr [ebp - 4]
0x49D29A: mov      eax, dword ptr [eax + 0x10]
0x49D29D: mov      eax, dword ptr [eax + 0xbc]
0x49D2A3: mov      edx, dword ptr [ebp - 4]
0x49D2A6: mov      dword ptr [edx + 0x2c], eax
0x49D2A9: mov      eax, dword ptr [ebp - 4]
0x49D2AC: add      eax, 0x44
0x49D2AF: push     eax
0x49D2B0: mov      eax, dword ptr [ebp - 4]
0x49D2B3: add      eax, 0x48
0x49D2B6: push     eax
0x49D2B7: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x49D2BC: mov      eax, dword ptr [eax]
0x49D2BE: mov      edx, dword ptr [ebp - 4]
0x49D2C1: mov      ecx, dword ptr [edx + 0x2c]
0x49D2C4: mov      edx, dword ptr [ebp - 4]
0x49D2C7: mov      edx, dword ptr [edx + 0x28]
0x49D2CA: call     0x472494
0x49D2CF: mov      eax, dword ptr [ebp - 4]
0x49D2D2: mov      eax, dword ptr [eax + 0x10]
0x49D2D5: mov      eax, dword ptr [eax + 0x98]
0x49D2DB: mov      edx, dword ptr [ebp - 4]
0x49D2DE: add      dword ptr [edx + 0x44], eax
0x49D2E1: mov      eax, dword ptr [ebp - 4]
0x49D2E4: mov      eax, dword ptr [eax + 0x10]
0x49D2E7: mov      eax, dword ptr [eax + 0x9c]
0x49D2ED: mov      edx, dword ptr [ebp - 4]
0x49D2F0: add      dword ptr [edx + 0x48], eax
0x49D2F3: mov      al, byte ptr [ebp - 5]
0x49D2F6: mov      esp, ebp
0x49D2F8: pop      ebp
0x49D2F9: ret      
0x49D2FA: add      byte ptr [eax], al
0x49D2FC: add      byte ptr [eax], al
0x49D2FE: cli      
0x49D2FF: inc      ebx
0x49D300: add      byte ptr [eax], al
0x49D302: das      
0x49D303: inc      esp
0x49D304: add      byte ptr [eax], al
0x49D306: popal    
0x49D307: inc      esp
0x49D308: push     ebp
0x49D309: mov      ebp, esp
0x49D30B: add      esp, -8
0x49D30E: push     ebx
0x49D30F: mov      dword ptr [ebp - 4], eax
0x49D312: mov      eax, dword ptr [ebp - 4]
0x49D315: mov      bx, 0xfffe
0x49D319: call     0x403080
0x49D31E: mov      byte ptr [ebp - 5], al
0x49D321: cmp      byte ptr [ebp - 5], 0
0x49D325: je       0x49d346
0x49D327: call     0x406154
0x49D32C: mov      edx, dword ptr [ebp - 4]
0x49D32F: sub      eax, dword ptr [edx + 0x8c]
0x49D335: cmp      eax, 0x2710
0x49D33A: jbe      0x49d342
0x49D33C: mov      byte ptr [ebp - 5], 0
0x49D340: jmp      0x49d346
0x49D342: mov      byte ptr [ebp - 5], 1
0x49D346: mov      al, byte ptr [ebp - 5]
0x49D349: pop      ebx
0x49D34A: pop      ecx
0x49D34B: pop      ecx
0x49D34C: pop      ebp
0x49D34D: ret      
0x49D34E: mov      eax, eax
0x49D350: push     ebp
0x49D351: mov      ebp, esp
0x49D353: add      esp, -0x18
0x49D356: mov      dword ptr [ebp - 8], edx
0x49D359: mov      dword ptr [ebp - 4], eax
0x49D35C: mov      eax, dword ptr [ebp - 4]
0x49D35F: cmp      byte ptr [eax + 4], 0
0x49D363: je       0x49d4e9
0x49D369: mov      eax, dword ptr [ebp - 4]
0x49D36C: mov      eax, dword ptr [eax + 0x44]
0x49D36F: mov      edx, dword ptr [ebp - 4]
0x49D372: sub      eax, dword ptr [edx + 0x98]
0x49D378: cdq      
0x49D379: xor      eax, edx
0x49D37B: sub      eax, edx
0x49D37D: cmp      eax, 0xf
0x49D380: jg       0x49d3a8
0x49D382: mov      eax, dword ptr [ebp - 4]
0x49D385: mov      eax, dword ptr [eax + 0x48]
0x49D388: mov      edx, dword ptr [ebp - 4]
0x49D38B: sub      eax, dword ptr [edx + 0x9c]
0x49D391: cdq      
0x49D392: xor      eax, edx
0x49D394: sub      eax, edx
0x49D396: cmp      eax, 0xf
0x49D399: jg       0x49d3a8
0x49D39B: mov      eax, dword ptr [ebp - 4]
0x49D39E: cmp      byte ptr [eax + 0x65], 0
0x49D3A2: je       0x49d4e9
0x49D3A8: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49D3AD: mov      eax, dword ptr [eax]
0x49D3AF: mov      eax, dword ptr [eax + 0xb8]
0x49D3B5: shl      eax, 4
0x49D3B8: lea      eax, [eax + eax*2]
0x49D3BB: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49D3C1: mov      edx, dword ptr [edx]
0x49D3C3: add      eax, dword ptr [edx + 0x98]
0x49D3C9: mov      edx, dword ptr [ebp - 4]
0x49D3CC: sub      eax, dword ptr [edx + 0xb0]
0x49D3D2: mov      dword ptr [ebp - 0x14], eax
0x49D3D5: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49D3DA: mov      eax, dword ptr [eax]
0x49D3DC: mov      eax, dword ptr [eax + 0xbc]
0x49D3E2: shl      eax, 5
0x49D3E5: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49D3EB: mov      edx, dword ptr [edx]
0x49D3ED: add      eax, dword ptr [edx + 0x9c]
0x49D3F3: mov      edx, dword ptr [ebp - 4]
0x49D3F6: sub      eax, dword ptr [edx + 0xb4]
0x49D3FC: mov      dword ptr [ebp - 0x18], eax
0x49D3FF: mov      eax, dword ptr [ebp - 4]
0x49D402: cmp      byte ptr [eax + 0x65], 0
0x49D406: jne      0x49d487
0x49D408: mov      eax, dword ptr [ebp - 4]
0x49D40B: mov      eax, dword ptr [eax + 0x18]
0x49D40E: add      eax, 0xa
0x49D411: mov      edx, dword ptr [ebp - 4]
0x49D414: movzx    edx, byte ptr [edx + 0x30]
0x49D418: add      edx, edx
0x49D41A: lea      edx, [edx + edx*4]
0x49D41D: add      eax, edx
0x49D41F: mov      dword ptr [ebp - 0xc], eax
0x49D422: mov      eax, dword ptr [ebp - 4]
0x49D425: add      eax, 0x24
0x49D428: push     eax
0x49D429: mov      eax, dword ptr [ebp - 4]
0x49D42C: lea      ecx, [eax + 0x20]
0x49D42F: mov      edx, dword ptr [ebp - 0xc]
0x49D432: mov      eax, dword ptr [ebp - 4]
0x49D435: add      edx, dword ptr [eax + 0x80]
0x49D43B: mov      eax, dword ptr [ebp - 4]
0x49D43E: mov      eax, dword ptr [eax + 0x14]
0x49D441: call     0x453a48
0x49D446: mov      dword ptr [ebp - 0x10], eax
0x49D449: cmp      dword ptr [ebp - 0x10], 0
0x49D44D: je       0x49d4e9
0x49D453: mov      eax, dword ptr [ebp - 0x10]
0x49D456: push     eax
0x49D457: push     1
0x49D459: mov      eax, dword ptr [ebp - 4]
0x49D45C: mov      ecx, dword ptr [eax + 0x48]
0x49D45F: mov      eax, dword ptr [ebp - 4]
0x49D462: add      ecx, dword ptr [eax + 0x24]
0x49D465: sub      ecx, 0x10
0x49D468: sub      ecx, dword ptr [ebp - 0x18]
0x49D46B: mov      eax, dword ptr [ebp - 4]
0x49D46E: mov      edx, dword ptr [eax + 0x44]
0x49D471: mov      eax, dword ptr [ebp - 4]
0x49D474: add      edx, dword ptr [eax + 0x20]
0x49D477: sub      edx, 0x18
0x49D47A: sub      edx, dword ptr [ebp - 0x14]
0x49D47D: mov      eax, dword ptr [ebp - 8]
0x49D480: call     0x455578
0x49D485: jmp      0x49d4e9
0x49D487: mov      eax, dword ptr [ebp - 4]
0x49D48A: mov      eax, dword ptr [eax + 0x1c]
0x49D48D: mov      edx, dword ptr [ebp - 4]
0x49D490: add      eax, dword ptr [edx + 0x80]
0x49D496: mov      dword ptr [ebp - 0xc], eax
0x49D499: mov      eax, dword ptr [ebp - 4]
0x49D49C: add      eax, 0x24
0x49D49F: push     eax
0x49D4A0: mov      eax, dword ptr [ebp - 4]
0x49D4A3: lea      ecx, [eax + 0x20]
0x49D4A6: mov      edx, dword ptr [ebp - 0xc]
0x49D4A9: mov      eax, dword ptr [ebp - 4]
0x49D4AC: mov      eax, dword ptr [eax + 0x14]
0x49D4AF: call     0x453a48
0x49D4B4: mov      dword ptr [ebp - 0x10], eax
0x49D4B7: cmp      dword ptr [ebp - 0x10], 0
0x49D4BB: je       0x49d4e9
0x49D4BD: mov      eax, dword ptr [ebp - 0x10]
0x49D4C0: push     eax
0x49D4C1: push     1
0x49D4C3: mov      eax, dword ptr [ebp - 4]
0x49D4C6: mov      ecx, dword ptr [eax + 0x48]
0x49D4C9: mov      eax, dword ptr [ebp - 4]
0x49D4CC: add      ecx, dword ptr [eax + 0x24]
0x49D4CF: sub      ecx, 0x10
0x49D4D2: mov      eax, dword ptr [ebp - 4]
0x49D4D5: mov      edx, dword ptr [eax + 0x44]
0x49D4D8: mov      eax, dword ptr [ebp - 4]
0x49D4DB: add      edx, dword ptr [eax + 0x20]
0x49D4DE: sub      edx, 0x18
0x49D4E1: mov      eax, dword ptr [ebp - 8]
0x49D4E4: call     0x455578
0x49D4E9: mov      esp, ebp
0x49D4EB: pop      ebp
0x49D4EC: ret      
0x49D4ED: lea      eax, [eax]
0x49D4F0: push     ebp
0x49D4F1: mov      ebp, esp
0x49D4F3: add      esp, -0xc
0x49D4F6: test     dl, dl
0x49D4F8: je       0x49d502
0x49D4FA: add      esp, -0x10
0x49D4FD: call     0x4031a4
0x49D502: mov      dword ptr [ebp - 0xc], ecx
0x49D505: mov      byte ptr [ebp - 5], dl
0x49D508: mov      dword ptr [ebp - 4], eax
0x49D50B: mov      eax, dword ptr [ebp + 0x24]
0x49D50E: push     eax
0x49D50F: mov      eax, dword ptr [ebp + 0x20]
0x49D512: push     eax
0x49D513: mov      eax, dword ptr [ebp + 0x1c]
0x49D516: push     eax
0x49D517: mov      eax, dword ptr [ebp + 0x18]
0x49D51A: push     eax
0x49D51B: mov      eax, dword ptr [ebp + 0x14]
0x49D51E: push     eax
0x49D51F: mov      al, byte ptr [ebp + 0x10]
0x49D522: push     eax
0x49D523: mov      al, byte ptr [ebp + 0xc]
0x49D526: push     eax
0x49D527: mov      eax, dword ptr [ebp + 8]
0x49D52A: push     eax
0x49D52B: mov      ecx, dword ptr [ebp - 0xc]
0x49D52E: xor      edx, edx
0x49D530: mov      eax, dword ptr [ebp - 4]
0x49D533: call     0x49c67c
0x49D538: mov      eax, dword ptr [ebp - 4]
0x49D53B: mov      dword ptr [eax + 0xc0], 0x1bf
0x49D545: mov      eax, dword ptr [ebp - 4]
0x49D548: mov      dword ptr [eax + 0xc4], 0x41
0x49D552: mov      eax, dword ptr [ebp - 4]
0x49D555: cmp      byte ptr [ebp - 5], 0
0x49D559: je       0x49d56a
0x49D55B: call     0x4031fc
0x49D560: pop      dword ptr fs:[0]
0x49D567: add      esp, 0xc
0x49D56A: mov      eax, dword ptr [ebp - 4]
0x49D56D: mov      esp, ebp
0x49D56F: pop      ebp
0x49D570: ret      0x20
0x49D573: nop      
0x49D574: push     ebp
0x49D575: mov      ebp, esp
0x49D577: add      esp, -0x28
0x49D57A: mov      dword ptr [ebp - 8], edx
0x49D57D: mov      dword ptr [ebp - 4], eax
0x49D580: mov      eax, dword ptr [ebp - 4]
0x49D583: cmp      byte ptr [eax + 4], 0
0x49D587: je       0x49d6b9
0x49D58D: mov      eax, dword ptr [ebp - 4]
0x49D590: mov      eax, dword ptr [eax + 0x44]
0x49D593: mov      edx, dword ptr [ebp - 4]
0x49D596: sub      eax, dword ptr [edx + 0x98]
0x49D59C: cdq      
0x49D59D: xor      eax, edx
0x49D59F: sub      eax, edx
0x49D5A1: mov      edx, dword ptr [ebp - 4]
0x49D5A4: cmp      eax, dword ptr [edx + 0xc4]
0x49D5AA: jg       0x49d5cf
0x49D5AC: mov      eax, dword ptr [ebp - 4]
0x49D5AF: mov      eax, dword ptr [eax + 0x48]
0x49D5B2: mov      edx, dword ptr [ebp - 4]
0x49D5B5: sub      eax, dword ptr [edx + 0x9c]
0x49D5BB: cdq      
0x49D5BC: xor      eax, edx
0x49D5BE: sub      eax, edx
0x49D5C0: mov      edx, dword ptr [ebp - 4]
0x49D5C3: cmp      eax, dword ptr [edx + 0xc4]
0x49D5C9: jle      0x49d6b9
0x49D5CF: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49D5D4: mov      eax, dword ptr [eax]
0x49D5D6: mov      eax, dword ptr [eax + 0xb8]
0x49D5DC: shl      eax, 4
0x49D5DF: lea      eax, [eax + eax*2]
0x49D5E2: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49D5E8: mov      edx, dword ptr [edx]
0x49D5EA: add      eax, dword ptr [edx + 0x98]
0x49D5F0: mov      edx, dword ptr [ebp - 4]
0x49D5F3: sub      eax, dword ptr [edx + 0xb0]
0x49D5F9: mov      dword ptr [ebp - 0x14], eax
0x49D5FC: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49D601: mov      eax, dword ptr [eax]
0x49D603: mov      eax, dword ptr [eax + 0xbc]
0x49D609: shl      eax, 5
0x49D60C: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49D612: mov      edx, dword ptr [edx]
0x49D614: add      eax, dword ptr [edx + 0x9c]
0x49D61A: mov      edx, dword ptr [ebp - 4]
0x49D61D: sub      eax, dword ptr [edx + 0xb4]
0x49D623: mov      dword ptr [ebp - 0x18], eax
0x49D626: mov      eax, dword ptr [ebp - 4]
0x49D629: cmp      byte ptr [eax + 0x65], 0
0x49D62D: jne      0x49d6b9
0x49D633: mov      eax, dword ptr [ebp - 4]
0x49D636: movzx    eax, byte ptr [eax + 0x30]
0x49D63A: add      eax, eax
0x49D63C: lea      eax, [eax + eax*4]
0x49D63F: mov      edx, dword ptr [ebp - 4]
0x49D642: add      eax, dword ptr [edx + 0xc0]
0x49D648: mov      dword ptr [ebp - 0xc], eax
0x49D64B: mov      eax, dword ptr [ebp - 4]
0x49D64E: add      eax, 0x24
0x49D651: push     eax
0x49D652: mov      eax, dword ptr [ebp - 4]
0x49D655: lea      ecx, [eax + 0x20]
0x49D658: mov      edx, dword ptr [ebp - 0xc]
0x49D65B: mov      eax, dword ptr [ebp - 4]
0x49D65E: add      edx, dword ptr [eax + 0x80]
0x49D664: mov      eax, dword ptr [ebp - 4]
0x49D667: mov      eax, dword ptr [eax + 0x14]
0x49D66A: call     0x453a48
0x49D66F: mov      dword ptr [ebp - 0x10], eax
0x49D672: cmp      dword ptr [ebp - 0x10], 0
0x49D676: je       0x49d6b9
0x49D678: lea      edx, [ebp - 0x28]
0x49D67B: mov      eax, dword ptr [ebp - 0x10]
0x49D67E: call     0x44d8ec
0x49D683: lea      eax, [ebp - 0x28]
0x49D686: push     eax
0x49D687: mov      eax, dword ptr [ebp - 0x10]
0x49D68A: push     eax
0x49D68B: push     1
0x49D68D: mov      eax, dword ptr [ebp - 4]
0x49D690: mov      ecx, dword ptr [eax + 0x48]
0x49D693: mov      eax, dword ptr [ebp - 4]
0x49D696: add      ecx, dword ptr [eax + 0x24]
0x49D699: sub      ecx, 0x10
0x49D69C: sub      ecx, dword ptr [ebp - 0x18]
0x49D69F: mov      eax, dword ptr [ebp - 4]
0x49D6A2: mov      edx, dword ptr [eax + 0x44]
0x49D6A5: mov      eax, dword ptr [ebp - 4]
0x49D6A8: add      edx, dword ptr [eax + 0x20]
0x49D6AB: sub      edx, 0x18
0x49D6AE: sub      edx, dword ptr [ebp - 0x14]
0x49D6B1: mov      eax, dword ptr [ebp - 8]
0x49D6B4: call     0x44d6cc
0x49D6B9: mov      esp, ebp
0x49D6BB: pop      ebp
0x49D6BC: ret      
0x49D6BD: lea      eax, [eax]
0x49D6C0: push     ebp
0x49D6C1: mov      ebp, esp
0x49D6C3: add      esp, -0x28
0x49D6C6: mov      dword ptr [ebp - 8], edx
0x49D6C9: mov      dword ptr [ebp - 4], eax
0x49D6CC: mov      eax, dword ptr [ebp - 4]
0x49D6CF: cmp      byte ptr [eax + 4], 0
0x49D6D3: je       0x49d7f7
0x49D6D9: mov      eax, dword ptr [ebp - 4]
0x49D6DC: mov      eax, dword ptr [eax + 0x44]
0x49D6DF: mov      edx, dword ptr [ebp - 4]
0x49D6E2: sub      eax, dword ptr [edx + 0x98]
0x49D6E8: cdq      
0x49D6E9: xor      eax, edx
0x49D6EB: sub      eax, edx
0x49D6ED: cmp      eax, 0x28
0x49D6F0: jg       0x49d70f
0x49D6F2: mov      eax, dword ptr [ebp - 4]
0x49D6F5: mov      eax, dword ptr [eax + 0x48]
0x49D6F8: mov      edx, dword ptr [ebp - 4]
0x49D6FB: sub      eax, dword ptr [edx + 0x9c]
0x49D701: cdq      
0x49D702: xor      eax, edx
0x49D704: sub      eax, edx
0x49D706: cmp      eax, 0x28
0x49D709: jle      0x49d7f7
0x49D70F: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49D714: mov      eax, dword ptr [eax]
0x49D716: mov      eax, dword ptr [eax + 0xb8]
0x49D71C: shl      eax, 4
0x49D71F: lea      eax, [eax + eax*2]
0x49D722: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49D728: mov      edx, dword ptr [edx]
0x49D72A: add      eax, dword ptr [edx + 0x98]
0x49D730: mov      edx, dword ptr [ebp - 4]
0x49D733: sub      eax, dword ptr [edx + 0xb0]
0x49D739: mov      dword ptr [ebp - 0x14], eax
0x49D73C: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49D741: mov      eax, dword ptr [eax]
0x49D743: mov      eax, dword ptr [eax + 0xbc]
0x49D749: shl      eax, 5
0x49D74C: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49D752: mov      edx, dword ptr [edx]
0x49D754: add      eax, dword ptr [edx + 0x9c]
0x49D75A: mov      edx, dword ptr [ebp - 4]
0x49D75D: sub      eax, dword ptr [edx + 0xb4]
0x49D763: mov      dword ptr [ebp - 0x18], eax
0x49D766: mov      eax, dword ptr [ebp - 4]
0x49D769: cmp      byte ptr [eax + 0x65], 0
0x49D76D: jne      0x49d7f7
0x49D773: mov      eax, dword ptr [ebp - 4]
0x49D776: movzx    eax, byte ptr [eax + 0x30]
0x49D77A: mov      edx, dword ptr [ebp - 4]
0x49D77D: add      eax, dword ptr [edx + 0xc0]
0x49D783: mov      dword ptr [ebp - 0xc], eax
0x49D786: mov      eax, dword ptr [ebp - 4]
0x49D789: add      eax, 0x24
0x49D78C: push     eax
0x49D78D: mov      eax, dword ptr [ebp - 4]
0x49D790: lea      ecx, [eax + 0x20]
0x49D793: mov      edx, dword ptr [ebp - 0xc]
0x49D796: mov      eax, dword ptr [ebp - 4]
0x49D799: add      edx, dword ptr [eax + 0x80]
0x49D79F: mov      eax, dword ptr [ebp - 4]
0x49D7A2: mov      eax, dword ptr [eax + 0x14]
0x49D7A5: call     0x453a48
0x49D7AA: mov      dword ptr [ebp - 0x10], eax
0x49D7AD: cmp      dword ptr [ebp - 0x10], 0
0x49D7B1: je       0x49d7f7
0x49D7B3: lea      edx, [ebp - 0x28]
0x49D7B6: mov      eax, dword ptr [ebp - 0x10]
0x49D7B9: call     0x44d8ec
0x49D7BE: lea      eax, [ebp - 0x28]
0x49D7C1: push     eax
0x49D7C2: mov      eax, dword ptr [ebp - 0x10]
0x49D7C5: push     eax
0x49D7C6: push     1
0x49D7C8: mov      eax, dword ptr [ebp - 4]
0x49D7CB: mov      ecx, dword ptr [eax + 0x48]
0x49D7CE: mov      eax, dword ptr [ebp - 4]
0x49D7D1: add      ecx, dword ptr [eax + 0x24]
0x49D7D4: sub      ecx, 0x10
0x49D7D7: sub      ecx, dword ptr [ebp - 0x18]
0x49D7DA: sub      ecx, 0x2e
0x49D7DD: mov      eax, dword ptr [ebp - 4]
0x49D7E0: mov      edx, dword ptr [eax + 0x44]
0x49D7E3: mov      eax, dword ptr [ebp - 4]
0x49D7E6: add      edx, dword ptr [eax + 0x20]
0x49D7E9: sub      edx, 0x18
0x49D7EC: sub      edx, dword ptr [ebp - 0x14]
0x49D7EF: mov      eax, dword ptr [ebp - 8]
0x49D7F2: call     0x44d6cc
0x49D7F7: mov      esp, ebp
0x49D7F9: pop      ebp
0x49D7FA: ret      
0x49D7FB: nop      
0x49D7FC: push     ebp
0x49D7FD: mov      ebp, esp
0x49D7FF: add      esp, -0xc
0x49D802: test     dl, dl
0x49D804: je       0x49d80e
0x49D806: add      esp, -0x10
0x49D809: call     0x4031a4
0x49D80E: mov      dword ptr [ebp - 0xc], ecx
0x49D811: mov      byte ptr [ebp - 5], dl
0x49D814: mov      dword ptr [ebp - 4], eax
0x49D817: mov      eax, dword ptr [ebp - 0xc]
0x49D81A: push     eax
0x49D81B: mov      eax, dword ptr [ebp + 8]
0x49D81E: movzx    eax, word ptr [eax + 8]
0x49D822: push     eax
0x49D823: mov      eax, dword ptr [ebp + 8]
0x49D826: movzx    eax, word ptr [eax + 0xa]
0x49D82A: push     eax
0x49D82B: mov      eax, dword ptr [ebp + 8]
0x49D82E: movzx    eax, word ptr [eax + 8]
0x49D832: push     eax
0x49D833: mov      eax, dword ptr [ebp + 8]
0x49D836: movzx    eax, word ptr [eax + 0xa]
0x49D83A: push     eax
0x49D83B: push     2
0x49D83D: push     0
0x49D83F: push     0
0x49D841: mov      ecx, 0x6f
0x49D846: xor      edx, edx
0x49D848: mov      eax, dword ptr [ebp - 4]
0x49D84B: call     0x49c67c
0x49D850: mov      eax, dword ptr [ebp + 8]
0x49D853: mov      edx, dword ptr [ebp - 4]
0x49D856: mov      dword ptr [edx + 0x10], eax
0x49D859: mov      eax, dword ptr [ebp + 0xc]
0x49D85C: mov      edx, dword ptr [ebp - 4]
0x49D85F: mov      dword ptr [edx + 0x84], eax
0x49D865: mov      eax, dword ptr [ebp - 4]
0x49D868: mov      dword ptr [eax + 0x74], 0x1e
0x49D86F: mov      eax, dword ptr [ebp - 4]
0x49D872: cmp      byte ptr [ebp - 5], 0
0x49D876: je       0x49d887
0x49D878: call     0x4031fc
0x49D87D: pop      dword ptr fs:[0]
0x49D884: add      esp, 0xc
0x49D887: mov      eax, dword ptr [ebp - 4]
0x49D88A: mov      esp, ebp
0x49D88C: pop      ebp
0x49D88D: ret      8

; === TCharEffect.DMT_-1 @ 0x49D890 ===
0x49D890: push     ebp
0x49D891: mov      ebp, esp
0x49D893: add      esp, -8
0x49D896: mov      dword ptr [ebp - 4], eax
0x49D899: mov      byte ptr [ebp - 5], 1
0x49D89D: call     0x406154
0x49D8A2: mov      edx, dword ptr [ebp - 4]
0x49D8A5: sub      eax, dword ptr [edx + 0x94]
0x49D8AB: mov      edx, dword ptr [ebp - 4]
0x49D8AE: cmp      eax, dword ptr [edx + 0x74]
0x49D8B1: jbe      0x49d902
0x49D8B3: call     0x406154
0x49D8B8: mov      edx, dword ptr [ebp - 4]
0x49D8BB: mov      dword ptr [edx + 0x94], eax
0x49D8C1: mov      eax, dword ptr [ebp - 4]
0x49D8C4: inc      dword ptr [eax + 0x80]
0x49D8CA: mov      eax, dword ptr [ebp - 4]
0x49D8CD: mov      eax, dword ptr [eax + 0x7c]
0x49D8D0: mov      edx, dword ptr [ebp - 4]
0x49D8D3: add      eax, dword ptr [edx + 0x84]
0x49D8D9: dec      eax
0x49D8DA: mov      edx, dword ptr [ebp - 4]
0x49D8DD: cmp      eax, dword ptr [edx + 0x80]
0x49D8E3: jge      0x49d902
0x49D8E5: mov      eax, dword ptr [ebp - 4]
0x49D8E8: mov      eax, dword ptr [eax + 0x7c]
0x49D8EB: mov      edx, dword ptr [ebp - 4]
0x49D8EE: add      eax, dword ptr [edx + 0x84]
0x49D8F4: dec      eax
0x49D8F5: mov      edx, dword ptr [ebp - 4]
0x49D8F8: mov      dword ptr [edx + 0x80], eax
0x49D8FE: mov      byte ptr [ebp - 5], 0
0x49D902: mov      al, byte ptr [ebp - 5]
0x49D905: pop      ecx
0x49D906: pop      ecx
0x49D907: pop      ebp
0x49D908: ret      
0x49D909: lea      eax, [eax]
0x49D90C: push     ebp
0x49D90D: mov      ebp, esp
0x49D90F: add      esp, -0xc
0x49D912: mov      dword ptr [ebp - 8], edx
0x49D915: mov      dword ptr [ebp - 4], eax
0x49D918: mov      eax, dword ptr [ebp - 4]
0x49D91B: cmp      dword ptr [eax + 0x10], 0
0x49D91F: je       0x49d9ef
0x49D925: mov      eax, dword ptr [ebp - 4]
0x49D928: mov      eax, dword ptr [eax + 0x10]
0x49D92B: mov      eax, dword ptr [eax + 0xb8]
0x49D931: mov      edx, dword ptr [ebp - 4]
0x49D934: mov      dword ptr [edx + 0x28], eax
0x49D937: mov      eax, dword ptr [ebp - 4]
0x49D93A: mov      eax, dword ptr [eax + 0x10]
0x49D93D: mov      eax, dword ptr [eax + 0xbc]
0x49D943: mov      edx, dword ptr [ebp - 4]
0x49D946: mov      dword ptr [edx + 0x2c], eax
0x49D949: mov      eax, dword ptr [ebp - 4]
0x49D94C: add      eax, 0x44
0x49D94F: push     eax
0x49D950: mov      eax, dword ptr [ebp - 4]
0x49D953: add      eax, 0x48
0x49D956: push     eax
0x49D957: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x49D95C: mov      eax, dword ptr [eax]
0x49D95E: mov      edx, dword ptr [ebp - 4]
0x49D961: mov      ecx, dword ptr [edx + 0x2c]
0x49D964: mov      edx, dword ptr [ebp - 4]
0x49D967: mov      edx, dword ptr [edx + 0x28]
0x49D96A: call     0x472494
0x49D96F: mov      eax, dword ptr [ebp - 4]
0x49D972: mov      eax, dword ptr [eax + 0x10]
0x49D975: mov      eax, dword ptr [eax + 0x98]
0x49D97B: mov      edx, dword ptr [ebp - 4]
0x49D97E: add      dword ptr [edx + 0x44], eax
0x49D981: mov      eax, dword ptr [ebp - 4]
0x49D984: mov      eax, dword ptr [eax + 0x10]
0x49D987: mov      eax, dword ptr [eax + 0x9c]
0x49D98D: mov      edx, dword ptr [ebp - 4]
0x49D990: add      dword ptr [edx + 0x48], eax
0x49D993: mov      eax, dword ptr [ebp - 4]
0x49D996: add      eax, 0x24
0x49D999: push     eax
0x49D99A: mov      eax, dword ptr [ebp - 4]
0x49D99D: lea      ecx, [eax + 0x20]
0x49D9A0: mov      eax, dword ptr [ebp - 4]
0x49D9A3: mov      edx, dword ptr [eax + 0x18]
0x49D9A6: mov      eax, dword ptr [ebp - 4]
0x49D9A9: add      edx, dword ptr [eax + 0x80]
0x49D9AF: mov      eax, dword ptr [ebp - 4]
0x49D9B2: mov      eax, dword ptr [eax + 0x14]
0x49D9B5: call     0x453a48
0x49D9BA: mov      dword ptr [ebp - 0xc], eax
0x49D9BD: cmp      dword ptr [ebp - 0xc], 0
0x49D9C1: je       0x49d9ef
0x49D9C3: mov      eax, dword ptr [ebp - 0xc]
0x49D9C6: push     eax
0x49D9C7: push     1
0x49D9C9: mov      eax, dword ptr [ebp - 4]
0x49D9CC: mov      ecx, dword ptr [eax + 0x48]
0x49D9CF: mov      eax, dword ptr [ebp - 4]
0x49D9D2: add      ecx, dword ptr [eax + 0x24]
0x49D9D5: sub      ecx, 0x10
0x49D9D8: mov      eax, dword ptr [ebp - 4]
0x49D9DB: mov      edx, dword ptr [eax + 0x44]
0x49D9DE: mov      eax, dword ptr [ebp - 4]
0x49D9E1: add      edx, dword ptr [eax + 0x20]
0x49D9E4: sub      edx, 0x18
0x49D9E7: mov      eax, dword ptr [ebp - 8]
0x49D9EA: call     0x455578
0x49D9EF: mov      esp, ebp
0x49D9F1: pop      ebp
0x49D9F2: ret      
0x49D9F3: nop      
0x49D9F4: push     ebp
0x49D9F5: mov      ebp, esp
0x49D9F7: add      esp, -0xc
0x49D9FA: test     dl, dl
0x49D9FC: je       0x49da06
0x49D9FE: add      esp, -0x10
0x49DA01: call     0x4031a4
0x49DA06: mov      dword ptr [ebp - 0xc], ecx
0x49DA09: mov      byte ptr [ebp - 5], dl
0x49DA0C: mov      dword ptr [ebp - 4], eax
0x49DA0F: mov      eax, dword ptr [ebp - 0xc]
0x49DA12: push     eax
0x49DA13: mov      eax, dword ptr [ebp + 0xc]
0x49DA16: push     eax
0x49DA17: mov      eax, dword ptr [ebp + 8]
0x49DA1A: push     eax
0x49DA1B: mov      eax, dword ptr [ebp + 0xc]
0x49DA1E: push     eax
0x49DA1F: mov      eax, dword ptr [ebp + 8]
0x49DA22: push     eax
0x49DA23: push     2
0x49DA25: push     0
0x49DA27: push     0
0x49DA29: mov      ecx, 0x6f
0x49DA2E: xor      edx, edx
0x49DA30: mov      eax, dword ptr [ebp - 4]
0x49DA33: call     0x49c67c
0x49DA38: mov      eax, dword ptr [ebp - 4]
0x49DA3B: xor      edx, edx
0x49DA3D: mov      dword ptr [eax + 0x10], edx
0x49DA40: mov      eax, dword ptr [ebp + 0x10]
0x49DA43: mov      edx, dword ptr [ebp - 4]
0x49DA46: mov      dword ptr [edx + 0x84], eax
0x49DA4C: mov      eax, dword ptr [ebp - 4]
0x49DA4F: mov      dword ptr [eax + 0x74], 0x1e
0x49DA56: mov      eax, dword ptr [ebp - 4]
0x49DA59: xor      edx, edx
0x49DA5B: mov      dword ptr [eax + 0xc0], edx
0x49DA61: mov      eax, dword ptr [ebp - 4]
0x49DA64: cmp      byte ptr [ebp - 5], 0
0x49DA68: je       0x49da79
0x49DA6A: call     0x4031fc
0x49DA6F: pop      dword ptr fs:[0]
0x49DA76: add      esp, 0xc
0x49DA79: mov      eax, dword ptr [ebp - 4]
0x49DA7C: mov      esp, ebp
0x49DA7E: pop      ebp
0x49DA7F: ret      0xc
0x49DA82: mov      eax, eax
0x49DA84: push     ebp
0x49DA85: mov      ebp, esp
0x49DA87: add      esp, -8
0x49DA8A: mov      dword ptr [ebp - 4], eax
0x49DA8D: mov      byte ptr [ebp - 5], 1
0x49DA91: call     0x406154
0x49DA96: mov      edx, dword ptr [ebp - 4]
0x49DA99: sub      eax, dword ptr [edx + 0x94]
0x49DA9F: mov      edx, dword ptr [ebp - 4]
0x49DAA2: cmp      eax, dword ptr [edx + 0x74]
0x49DAA5: jbe      0x49db1c
0x49DAA7: call     0x406154
0x49DAAC: mov      edx, dword ptr [ebp - 4]
0x49DAAF: mov      dword ptr [edx + 0x94], eax
0x49DAB5: mov      eax, dword ptr [ebp - 4]
0x49DAB8: inc      dword ptr [eax + 0x80]
0x49DABE: mov      eax, dword ptr [ebp - 4]
0x49DAC1: mov      eax, dword ptr [eax + 0x7c]
0x49DAC4: mov      edx, dword ptr [ebp - 4]
0x49DAC7: add      eax, dword ptr [edx + 0x84]
0x49DACD: dec      eax
0x49DACE: mov      edx, dword ptr [ebp - 4]
0x49DAD1: cmp      eax, dword ptr [edx + 0x80]
0x49DAD7: jge      0x49db1c
0x49DAD9: mov      eax, dword ptr [ebp - 4]
0x49DADC: mov      eax, dword ptr [eax + 0x7c]
0x49DADF: mov      edx, dword ptr [ebp - 4]
0x49DAE2: add      eax, dword ptr [edx + 0x84]
0x49DAE8: dec      eax
0x49DAE9: mov      edx, dword ptr [ebp - 4]
0x49DAEC: mov      dword ptr [edx + 0x80], eax
0x49DAF2: mov      eax, dword ptr [ebp - 4]
0x49DAF5: cmp      dword ptr [eax + 0xc0], 0
0x49DAFC: jle      0x49db18
0x49DAFE: mov      eax, dword ptr [ebp - 4]
0x49DB01: dec      dword ptr [eax + 0xc0]
0x49DB07: mov      eax, dword ptr [ebp - 4]
0x49DB0A: mov      eax, dword ptr [eax + 0x7c]
0x49DB0D: mov      edx, dword ptr [ebp - 4]
0x49DB10: mov      dword ptr [edx + 0x80], eax
0x49DB16: jmp      0x49db1c
0x49DB18: mov      byte ptr [ebp - 5], 0
0x49DB1C: mov      al, byte ptr [ebp - 5]
0x49DB1F: pop      ecx
0x49DB20: pop      ecx
0x49DB21: pop      ebp
0x49DB22: ret      
0x49DB23: nop      
0x49DB24: push     ebp
0x49DB25: mov      ebp, esp
0x49DB27: add      esp, -0xc
0x49DB2A: mov      dword ptr [ebp - 8], edx
0x49DB2D: mov      dword ptr [ebp - 4], eax
0x49DB30: mov      eax, dword ptr [ebp - 4]
0x49DB33: mov      eax, dword ptr [eax + 0x34]
0x49DB36: mov      edx, dword ptr [ebp - 4]
0x49DB39: mov      dword ptr [edx + 0x28], eax
0x49DB3C: mov      eax, dword ptr [ebp - 4]
0x49DB3F: mov      eax, dword ptr [eax + 0x38]
0x49DB42: mov      edx, dword ptr [ebp - 4]
0x49DB45: mov      dword ptr [edx + 0x2c], eax
0x49DB48: mov      eax, dword ptr [ebp - 4]
0x49DB4B: add      eax, 0x44
0x49DB4E: push     eax
0x49DB4F: mov      eax, dword ptr [ebp - 4]
0x49DB52: add      eax, 0x48
0x49DB55: push     eax
0x49DB56: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x49DB5B: mov      eax, dword ptr [eax]
0x49DB5D: mov      edx, dword ptr [ebp - 4]
0x49DB60: mov      ecx, dword ptr [edx + 0x2c]
0x49DB63: mov      edx, dword ptr [ebp - 4]
0x49DB66: mov      edx, dword ptr [edx + 0x28]
0x49DB69: call     0x472494
0x49DB6E: mov      eax, dword ptr [ebp - 4]
0x49DB71: add      eax, 0x24
0x49DB74: push     eax
0x49DB75: mov      eax, dword ptr [ebp - 4]
0x49DB78: lea      ecx, [eax + 0x20]
0x49DB7B: mov      eax, dword ptr [ebp - 4]
0x49DB7E: mov      edx, dword ptr [eax + 0x18]
0x49DB81: mov      eax, dword ptr [ebp - 4]
0x49DB84: add      edx, dword ptr [eax + 0x80]
0x49DB8A: mov      eax, dword ptr [ebp - 4]
0x49DB8D: mov      eax, dword ptr [eax + 0x14]
0x49DB90: call     0x453a48
0x49DB95: mov      dword ptr [ebp - 0xc], eax
0x49DB98: cmp      dword ptr [ebp - 0xc], 0
0x49DB9C: je       0x49dbca
0x49DB9E: mov      eax, dword ptr [ebp - 0xc]
0x49DBA1: push     eax
0x49DBA2: push     1
0x49DBA4: mov      eax, dword ptr [ebp - 4]
0x49DBA7: mov      ecx, dword ptr [eax + 0x48]
0x49DBAA: mov      eax, dword ptr [ebp - 4]
0x49DBAD: add      ecx, dword ptr [eax + 0x24]
0x49DBB0: sub      ecx, 0x10
0x49DBB3: mov      eax, dword ptr [ebp - 4]
0x49DBB6: mov      edx, dword ptr [eax + 0x44]
0x49DBB9: mov      eax, dword ptr [ebp - 4]
0x49DBBC: add      edx, dword ptr [eax + 0x20]
0x49DBBF: sub      edx, 0x18
0x49DBC2: mov      eax, dword ptr [ebp - 8]
0x49DBC5: call     0x455578
0x49DBCA: mov      esp, ebp
0x49DBCC: pop      ebp
0x49DBCD: ret      
0x49DBCE: mov      eax, eax
0x49DBD0: push     ebp
0x49DBD1: mov      ebp, esp
0x49DBD3: add      esp, -0xc
0x49DBD6: test     dl, dl
0x49DBD8: je       0x49dbe2
0x49DBDA: add      esp, -0x10
0x49DBDD: call     0x4031a4
0x49DBE2: mov      dword ptr [ebp - 0xc], ecx
0x49DBE5: mov      byte ptr [ebp - 5], dl
0x49DBE8: mov      dword ptr [ebp - 4], eax
0x49DBEB: mov      eax, dword ptr [ebp + 0x14]
0x49DBEE: push     eax
0x49DBEF: mov      eax, dword ptr [ebp + 0x10]
0x49DBF2: push     eax
0x49DBF3: mov      eax, dword ptr [ebp + 0xc]
0x49DBF6: push     eax
0x49DBF7: mov      ecx, dword ptr [ebp - 0xc]
0x49DBFA: xor      edx, edx
0x49DBFC: mov      eax, dword ptr [ebp - 4]
0x49DBFF: call     0x49d9f4
0x49DC04: mov      eax, dword ptr [0x4a42c0] ; *0x4A42C0->0x4F94A4
0x49DC09: mov      edx, dword ptr [ebp + 8]
0x49DC0C: mov      dword ptr [eax], edx
0x49DC0E: mov      eax, dword ptr [ebp - 4]
0x49DC11: cmp      byte ptr [ebp - 5], 0
0x49DC15: je       0x49dc26
0x49DC17: call     0x4031fc
0x49DC1C: pop      dword ptr fs:[0]
0x49DC23: add      esp, 0xc
0x49DC26: mov      eax, dword ptr [ebp - 4]
0x49DC29: mov      esp, ebp
0x49DC2B: pop      ebp
0x49DC2C: ret      0x10
0x49DC2F: nop      
0x49DC30: push     ebp
0x49DC31: mov      ebp, esp
0x49DC33: add      esp, -8
0x49DC36: mov      dword ptr [ebp - 4], eax
0x49DC39: mov      eax, dword ptr [ebp - 4]
0x49DC3C: call     0x49da84 ; TMapEffect.DMT_-1
0x49DC41: mov      byte ptr [ebp - 5], al
0x49DC44: mov      eax, dword ptr [ebp - 4]
0x49DC47: cmp      dword ptr [eax + 0x84], 7
0x49DC4E: jne      0x49dc70
0x49DC50: mov      eax, dword ptr [0x4a42c0] ; *0x4A42C0->0x4F94A4
0x49DC55: cmp      dword ptr [eax], 0
0x49DC58: je       0x49dc70
0x49DC5A: mov      eax, dword ptr [0x4a42c0] ; *0x4A42C0->0x4F94A4
0x49DC5F: mov      eax, dword ptr [eax]
0x49DC61: mov      edx, dword ptr [eax + 4]
0x49DC64: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x49DC69: mov      eax, dword ptr [eax]
0x49DC6B: call     0x4736a8
0x49DC70: mov      al, byte ptr [ebp - 5]
0x49DC73: pop      ecx
0x49DC74: pop      ecx
0x49DC75: pop      ebp
0x49DC76: ret      
0x49DC77: nop      
0x49DC78: push     ebp
0x49DC79: mov      ebp, esp
0x49DC7B: add      esp, -0xc
0x49DC7E: test     dl, dl
0x49DC80: je       0x49dc8a
0x49DC82: add      esp, -0x10
0x49DC85: call     0x4031a4
0x49DC8A: mov      dword ptr [ebp - 0xc], ecx
0x49DC8D: mov      byte ptr [ebp - 5], dl
0x49DC90: mov      dword ptr [ebp - 4], eax
0x49DC93: mov      eax, dword ptr [ebp - 0xc]
0x49DC96: push     eax
0x49DC97: mov      eax, dword ptr [ebp + 0x14]
0x49DC9A: push     eax
0x49DC9B: mov      eax, dword ptr [ebp + 0x10]
0x49DC9E: push     eax
0x49DC9F: mov      eax, dword ptr [ebp + 0xc]
0x49DCA2: push     eax
0x49DCA3: mov      eax, dword ptr [ebp + 8]
0x49DCA6: push     eax
0x49DCA7: push     5
0x49DCA9: push     1
0x49DCAB: push     0
0x49DCAD: mov      ecx, 0x6f
0x49DCB2: xor      edx, edx
0x49DCB4: mov      eax, dword ptr [ebp - 4]
0x49DCB7: call     0x49c67c
0x49DCBC: mov      eax, dword ptr [ebp - 4]
0x49DCBF: mov      dword ptr [eax + 0x74], 0x32
0x49DCC6: mov      eax, dword ptr [ebp - 4]
0x49DCC9: add      eax, 0xc8
0x49DCCE: xor      ecx, ecx
0x49DCD0: mov      edx, 0x48
0x49DCD5: call     0x402b54
0x49DCDA: mov      eax, dword ptr [ebp - 4]
0x49DCDD: mov      byte ptr [eax + 0xc0], 0
0x49DCE4: call     0x406154
0x49DCE9: mov      edx, dword ptr [ebp - 4]
0x49DCEC: mov      dword ptr [edx + 0xc4], eax
0x49DCF2: mov      eax, dword ptr [ebp - 4]
0x49DCF5: cmp      byte ptr [ebp - 5], 0
0x49DCF9: je       0x49dd0a
0x49DCFB: call     0x4031fc
0x49DD00: pop      dword ptr fs:[0]
0x49DD07: add      esp, 0xc
0x49DD0A: mov      eax, dword ptr [ebp - 4]
0x49DD0D: mov      esp, ebp
0x49DD0F: pop      ebp
0x49DD10: ret      0x10
0x49DD13: nop      
0x49DD14: push     ebp
0x49DD15: mov      ebp, esp
0x49DD17: add      esp, -0x18
0x49DD1A: push     ebx
0x49DD1B: push     esi
0x49DD1C: mov      dword ptr [ebp - 4], eax
0x49DD1F: mov      byte ptr [ebp - 5], 1
0x49DD23: call     0x406154
0x49DD28: mov      edx, dword ptr [ebp - 4]
0x49DD2B: sub      eax, dword ptr [edx + 0x94]
0x49DD31: mov      edx, dword ptr [ebp - 4]
0x49DD34: cmp      eax, dword ptr [edx + 0x74]
0x49DD37: jbe      0x49ded1
0x49DD3D: mov      eax, dword ptr [ebp - 4]
0x49DD40: mov      bx, 0xfffe
0x49DD44: call     0x403080
0x49DD49: call     0x406154
0x49DD4E: mov      edx, dword ptr [ebp - 4]
0x49DD51: mov      dword ptr [edx + 0x94], eax
0x49DD57: mov      eax, dword ptr [ebp - 4]
0x49DD5A: cmp      byte ptr [eax + 0xc0], 0
0x49DD61: jne      0x49de50
0x49DD67: mov      eax, dword ptr [ebp - 4]
0x49DD6A: mov      eax, dword ptr [eax + 0xc]
0x49DD6D: push     dword ptr [eax + 0xb8]
0x49DD73: mov      eax, dword ptr [ebp - 4]
0x49DD76: mov      eax, dword ptr [eax + 0x28]
0x49DD79: pop      edx
0x49DD7A: sub      eax, edx
0x49DD7C: cdq      
0x49DD7D: xor      eax, edx
0x49DD7F: sub      eax, edx
0x49DD81: cmp      eax, 5
0x49DD84: jge      0x49ddba
0x49DD86: mov      eax, dword ptr [ebp - 4]
0x49DD89: mov      eax, dword ptr [eax + 0xc]
0x49DD8C: push     dword ptr [eax + 0xbc]
0x49DD92: mov      eax, dword ptr [ebp - 4]
0x49DD95: mov      eax, dword ptr [eax + 0x2c]
0x49DD98: pop      edx
0x49DD99: sub      eax, edx
0x49DD9B: cdq      
0x49DD9C: xor      eax, edx
0x49DD9E: sub      eax, edx
0x49DDA0: cmp      eax, 5
0x49DDA3: jge      0x49ddba
0x49DDA5: call     0x406154
0x49DDAA: mov      edx, dword ptr [ebp - 4]
0x49DDAD: sub      eax, dword ptr [edx + 0xc4]
0x49DDB3: cmp      eax, 0x320
0x49DDB8: jbe      0x49ddc4
0x49DDBA: mov      eax, dword ptr [ebp - 4]
0x49DDBD: mov      byte ptr [eax + 0xc0], 1
0x49DDC4: mov      dword ptr [ebp - 0xc], 4
0x49DDCB: mov      eax, dword ptr [ebp - 0xc]
0x49DDCE: lea      eax, [eax + eax*2]
0x49DDD1: mov      edx, dword ptr [ebp - 4]
0x49DDD4: inc      dword ptr [edx + eax*4 + 0xd0]
0x49DDDB: mov      eax, dword ptr [ebp - 0xc]
0x49DDDE: lea      eax, [eax + eax*2]
0x49DDE1: mov      edx, dword ptr [ebp - 4]
0x49DDE4: mov      ecx, dword ptr [ebp - 0xc]
0x49DDE7: lea      ecx, [ecx + ecx*2]
0x49DDEA: mov      ebx, dword ptr [ebp - 4]
0x49DDED: mov      esi, dword ptr [ebx + ecx*4 + 0xc8]
0x49DDF4: mov      dword ptr [edx + eax*4 + 0xd4], esi
0x49DDFB: mov      esi, dword ptr [ebx + ecx*4 + 0xcc]
0x49DE02: mov      dword ptr [edx + eax*4 + 0xd8], esi
0x49DE09: mov      esi, dword ptr [ebx + ecx*4 + 0xd0]
0x49DE10: mov      dword ptr [edx + eax*4 + 0xdc], esi
0x49DE17: dec      dword ptr [ebp - 0xc]
0x49DE1A: cmp      dword ptr [ebp - 0xc], -1
0x49DE1E: jne      0x49ddcb
0x49DE20: mov      eax, dword ptr [ebp - 4]
0x49DE23: mov      dword ptr [eax + 0xd0], 1
0x49DE2D: mov      eax, dword ptr [ebp - 4]
0x49DE30: mov      eax, dword ptr [eax + 0x44]
0x49DE33: mov      edx, dword ptr [ebp - 4]
0x49DE36: mov      dword ptr [edx + 0xc8], eax
0x49DE3C: mov      eax, dword ptr [ebp - 4]
0x49DE3F: mov      eax, dword ptr [eax + 0x48]
0x49DE42: mov      edx, dword ptr [ebp - 4]
0x49DE45: mov      dword ptr [edx + 0xcc], eax
0x49DE4B: jmp      0x49ded1
0x49DE50: mov      byte ptr [ebp - 0xd], 1
0x49DE54: mov      dword ptr [ebp - 0xc], 4
0x49DE5B: mov      eax, dword ptr [ebp - 0xc]
0x49DE5E: lea      eax, [eax + eax*2]
0x49DE61: mov      edx, dword ptr [ebp - 4]
0x49DE64: cmp      dword ptr [edx + eax*4 + 0xd0], 6
0x49DE6C: jg       0x49debe
0x49DE6E: mov      eax, dword ptr [ebp - 0xc]
0x49DE71: lea      eax, [eax + eax*2]
0x49DE74: mov      edx, dword ptr [ebp - 4]
0x49DE77: inc      dword ptr [edx + eax*4 + 0xd0]
0x49DE7E: mov      eax, dword ptr [ebp - 0xc]
0x49DE81: lea      eax, [eax + eax*2]
0x49DE84: mov      edx, dword ptr [ebp - 4]
0x49DE87: mov      ecx, dword ptr [ebp - 0xc]
0x49DE8A: lea      ecx, [ecx + ecx*2]
0x49DE8D: mov      ebx, dword ptr [ebp - 4]
0x49DE90: mov      esi, dword ptr [ebx + ecx*4 + 0xc8]
0x49DE97: mov      dword ptr [edx + eax*4 + 0xd4], esi
0x49DE9E: mov      esi, dword ptr [ebx + ecx*4 + 0xcc]
0x49DEA5: mov      dword ptr [edx + eax*4 + 0xd8], esi
0x49DEAC: mov      esi, dword ptr [ebx + ecx*4 + 0xd0]
0x49DEB3: mov      dword ptr [edx + eax*4 + 0xdc], esi
0x49DEBA: mov      byte ptr [ebp - 0xd], 0
0x49DEBE: dec      dword ptr [ebp - 0xc]
0x49DEC1: cmp      dword ptr [ebp - 0xc], -1
0x49DEC5: jne      0x49de5b
0x49DEC7: cmp      byte ptr [ebp - 0xd], 0
0x49DECB: je       0x49ded1
0x49DECD: mov      byte ptr [ebp - 5], 0
0x49DED1: mov      al, byte ptr [ebp - 5]
0x49DED4: pop      esi
0x49DED5: pop      ebx
0x49DED6: mov      esp, ebp
0x49DED8: pop      ebp
0x49DED9: ret      
0x49DEDA: mov      eax, eax
0x49DEDC: push     ebp
0x49DEDD: mov      ebp, esp
0x49DEDF: add      esp, -0x30
0x49DEE2: mov      dword ptr [ebp - 8], edx
0x49DEE5: mov      dword ptr [ebp - 4], eax
0x49DEE8: mov      dword ptr [ebp - 0x20], 0xffffffff
0x49DEEF: mov      dword ptr [ebp - 0x24], 0xffffffff
0x49DEF6: xor      eax, eax
0x49DEF8: mov      dword ptr [ebp - 0xc], eax
0x49DEFB: mov      eax, dword ptr [ebp - 0xc]
0x49DEFE: lea      eax, [eax + eax*2]
0x49DF01: mov      edx, dword ptr [ebp - 4]
0x49DF04: cmp      dword ptr [edx + eax*4 + 0xd0], 6
0x49DF0C: jg       0x49e02c
0x49DF12: mov      eax, dword ptr [ebp - 0xc]
0x49DF15: lea      eax, [eax + eax*2]
0x49DF18: mov      edx, dword ptr [ebp - 4]
0x49DF1B: cmp      dword ptr [edx + eax*4 + 0xd0], 0
0x49DF23: jle      0x49e02c
0x49DF29: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49DF2E: mov      eax, dword ptr [eax]
0x49DF30: mov      eax, dword ptr [eax + 0xb8]
0x49DF36: shl      eax, 4
0x49DF39: lea      eax, [eax + eax*2]
0x49DF3C: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49DF42: mov      edx, dword ptr [edx]
0x49DF44: add      eax, dword ptr [edx + 0x98]
0x49DF4A: mov      edx, dword ptr [ebp - 4]
0x49DF4D: sub      eax, dword ptr [edx + 0xb0]
0x49DF53: mov      dword ptr [ebp - 0x10], eax
0x49DF56: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49DF5B: mov      eax, dword ptr [eax]
0x49DF5D: mov      eax, dword ptr [eax + 0xbc]
0x49DF63: shl      eax, 5
0x49DF66: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49DF6C: mov      edx, dword ptr [edx]
0x49DF6E: add      eax, dword ptr [edx + 0x9c]
0x49DF74: mov      edx, dword ptr [ebp - 4]
0x49DF77: sub      eax, dword ptr [edx + 0xb4]
0x49DF7D: mov      dword ptr [ebp - 0x14], eax
0x49DF80: mov      eax, dword ptr [ebp - 0xc]
0x49DF83: lea      eax, [eax + eax*2]
0x49DF86: mov      edx, dword ptr [ebp - 4]
0x49DF89: mov      eax, dword ptr [edx + eax*4 + 0xd0]
0x49DF90: dec      eax
0x49DF91: mov      edx, dword ptr [ebp - 4]
0x49DF94: add      eax, dword ptr [edx + 0x18]
0x49DF97: mov      dword ptr [ebp - 0x28], eax
0x49DF9A: mov      eax, dword ptr [ebp - 4]
0x49DF9D: add      eax, 0x24
0x49DFA0: push     eax
0x49DFA1: mov      eax, dword ptr [ebp - 4]
0x49DFA4: lea      ecx, [eax + 0x20]
0x49DFA7: mov      edx, dword ptr [ebp - 0x28]
0x49DFAA: mov      eax, dword ptr [ebp - 4]
0x49DFAD: mov      eax, dword ptr [eax + 0x14]
0x49DFB0: call     0x453a48
0x49DFB5: mov      dword ptr [ebp - 0x2c], eax
0x49DFB8: cmp      dword ptr [ebp - 0x2c], 0
0x49DFBC: je       0x49e02c
0x49DFBE: mov      eax, dword ptr [ebp - 0xc]
0x49DFC1: lea      eax, [eax + eax*2]
0x49DFC4: mov      edx, dword ptr [ebp - 4]
0x49DFC7: mov      eax, dword ptr [edx + eax*4 + 0xc8]
0x49DFCE: mov      edx, dword ptr [ebp - 4]
0x49DFD1: add      eax, dword ptr [edx + 0x20]
0x49DFD4: sub      eax, 0x18
0x49DFD7: sub      eax, dword ptr [ebp - 0x10]
0x49DFDA: mov      dword ptr [ebp - 0x18], eax
0x49DFDD: mov      eax, dword ptr [ebp - 0xc]
0x49DFE0: lea      eax, [eax + eax*2]
0x49DFE3: mov      edx, dword ptr [ebp - 4]
0x49DFE6: mov      eax, dword ptr [edx + eax*4 + 0xcc]
0x49DFED: mov      edx, dword ptr [ebp - 4]
0x49DFF0: add      eax, dword ptr [edx + 0x24]
0x49DFF3: sub      eax, 0x10
0x49DFF6: sub      eax, dword ptr [ebp - 0x14]
0x49DFF9: mov      dword ptr [ebp - 0x1c], eax
0x49DFFC: mov      eax, dword ptr [ebp - 0x18]
0x49DFFF: cmp      eax, dword ptr [ebp - 0x20]
0x49E002: jne      0x49e00c
0x49E004: mov      eax, dword ptr [ebp - 0x1c]
0x49E007: cmp      eax, dword ptr [ebp - 0x24]
0x49E00A: je       0x49e02c
0x49E00C: mov      eax, dword ptr [ebp - 0x18]
0x49E00F: mov      dword ptr [ebp - 0x20], eax
0x49E012: mov      eax, dword ptr [ebp - 0x1c]
0x49E015: mov      dword ptr [ebp - 0x24], eax
0x49E018: mov      eax, dword ptr [ebp - 0x2c]
0x49E01B: push     eax
0x49E01C: push     1
0x49E01E: mov      ecx, dword ptr [ebp - 0x1c]
0x49E021: mov      edx, dword ptr [ebp - 0x18]
0x49E024: mov      eax, dword ptr [ebp - 8]
0x49E027: call     0x455578
0x49E02C: inc      dword ptr [ebp - 0xc]
0x49E02F: cmp      dword ptr [ebp - 0xc], 6
0x49E033: jne      0x49defb
0x49E039: mov      esp, ebp
0x49E03B: pop      ebp
0x49E03C: ret      
0x49E03D: lea      eax, [eax]
0x49E040: push     ebp
0x49E041: mov      ebp, esp
0x49E043: add      esp, -0xc
0x49E046: test     dl, dl
0x49E048: je       0x49e052
0x49E04A: add      esp, -0x10
0x49E04D: call     0x4031a4
0x49E052: mov      dword ptr [ebp - 0xc], ecx
0x49E055: mov      byte ptr [ebp - 5], dl
0x49E058: mov      dword ptr [ebp - 4], eax
0x49E05B: mov      eax, dword ptr [ebp - 0xc]
0x49E05E: push     eax
0x49E05F: mov      eax, dword ptr [ebp + 0x10]
0x49E062: push     eax
0x49E063: mov      eax, dword ptr [ebp + 0xc]
0x49E066: push     eax
0x49E067: mov      eax, dword ptr [ebp + 0x10]
0x49E06A: push     eax
0x49E06B: mov      eax, dword ptr [ebp + 0xc]
0x49E06E: push     eax
0x49E06F: push     7
0x49E071: push     0
0x49E073: push     0
0x49E075: mov      ecx, 0x6f
0x49E07A: xor      edx, edx
0x49E07C: mov      eax, dword ptr [ebp - 4]
0x49E07F: call     0x49c67c
0x49E084: mov      eax, dword ptr [ebp + 8]
0x49E087: mov      edx, dword ptr [ebp - 4]
0x49E08A: mov      dword ptr [edx + 0x10], eax
0x49E08D: mov      eax, dword ptr [ebp - 4]
0x49E090: cmp      byte ptr [ebp - 5], 0
0x49E094: je       0x49e0a5
0x49E096: call     0x4031fc
0x49E09B: pop      dword ptr fs:[0]
0x49E0A2: add      esp, 0xc
0x49E0A5: mov      eax, dword ptr [ebp - 4]
0x49E0A8: mov      esp, ebp
0x49E0AA: pop      ebp
0x49E0AB: ret      0xc
0x49E0AE: mov      eax, eax
0x49E0B0: push     ebp
0x49E0B1: mov      ebp, esp
0x49E0B3: add      esp, -0x18
0x49E0B6: mov      dword ptr [ebp - 8], edx
0x49E0B9: mov      dword ptr [ebp - 4], eax
0x49E0BC: mov      eax, dword ptr [ebp - 4]
0x49E0BF: mov      eax, dword ptr [eax + 0x18]
0x49E0C2: mov      dword ptr [ebp - 0xc], eax
0x49E0C5: lea      eax, [ebp - 0x14]
0x49E0C8: push     eax
0x49E0C9: lea      ecx, [ebp - 0x10]
0x49E0CC: mov      edx, dword ptr [ebp - 0xc]
0x49E0CF: mov      eax, dword ptr [ebp - 4]
0x49E0D2: add      edx, dword ptr [eax + 0x80]
0x49E0D8: mov      eax, dword ptr [ebp - 4]
0x49E0DB: mov      eax, dword ptr [eax + 0x14]
0x49E0DE: call     0x453a48
0x49E0E3: mov      dword ptr [ebp - 0x18], eax
0x49E0E6: cmp      dword ptr [ebp - 0x18], 0
0x49E0EA: je       0x49e112
0x49E0EC: mov      eax, dword ptr [ebp - 0x18]
0x49E0EF: push     eax
0x49E0F0: push     1
0x49E0F2: mov      eax, dword ptr [ebp - 4]
0x49E0F5: mov      ecx, dword ptr [eax + 0x48]
0x49E0F8: add      ecx, dword ptr [ebp - 0x14]
0x49E0FB: sub      ecx, 0x10
0x49E0FE: mov      eax, dword ptr [ebp - 4]
0x49E101: mov      edx, dword ptr [eax + 0x44]
0x49E104: add      edx, dword ptr [ebp - 0x10]
0x49E107: sub      edx, 0x18
0x49E10A: mov      eax, dword ptr [ebp - 8]
0x49E10D: call     0x455578
0x49E112: mov      esp, ebp
0x49E114: pop      ebp
0x49E115: ret      
0x49E116: mov      eax, eax
0x49E118: push     ebp
0x49E119: mov      ebp, esp
0x49E11B: add      esp, -0xc
0x49E11E: test     dl, dl
0x49E120: je       0x49e12a
0x49E122: add      esp, -0x10
0x49E125: call     0x4031a4
0x49E12A: mov      dword ptr [ebp - 0xc], ecx
0x49E12D: mov      byte ptr [ebp - 5], dl
0x49E130: mov      dword ptr [ebp - 4], eax
0x49E133: mov      eax, dword ptr [ebp - 0xc]
0x49E136: push     eax
0x49E137: mov      eax, dword ptr [ebp + 0x18]
0x49E13A: push     eax
0x49E13B: mov      eax, dword ptr [ebp + 0x14]
0x49E13E: push     eax
0x49E13F: mov      eax, dword ptr [ebp + 0x10]
0x49E142: push     eax
0x49E143: mov      eax, dword ptr [ebp + 0xc]
0x49E146: push     eax
0x49E147: push     6
0x49E149: push     0
0x49E14B: push     0
0x49E14D: mov      ecx, 0x6f
0x49E152: xor      edx, edx
0x49E154: mov      eax, dword ptr [ebp - 4]
0x49E157: call     0x49c67c
0x49E15C: mov      eax, dword ptr [ebp + 8]
0x49E15F: mov      edx, dword ptr [ebp - 4]
0x49E162: mov      dword ptr [edx + 0x10], eax
0x49E165: mov      eax, dword ptr [ebp - 4]
0x49E168: cmp      byte ptr [ebp - 5], 0
0x49E16C: je       0x49e17d
0x49E16E: call     0x4031fc
0x49E173: pop      dword ptr fs:[0]
0x49E17A: add      esp, 0xc
0x49E17D: mov      eax, dword ptr [ebp - 4]
0x49E180: mov      esp, ebp
0x49E182: pop      ebp
0x49E183: ret      0x14
0x49E186: mov      eax, eax
0x49E188: push     ebp
0x49E189: mov      ebp, esp
0x49E18B: add      esp, -0x28
0x49E18E: mov      dword ptr [ebp - 8], edx
0x49E191: mov      dword ptr [ebp - 4], eax
0x49E194: mov      eax, dword ptr [ebp - 4]
0x49E197: movzx    eax, byte ptr [eax + 0x30]
0x49E19B: add      eax, eax
0x49E19D: lea      eax, [eax + eax*4]
0x49E1A0: mov      edx, dword ptr [ebp - 4]
0x49E1A3: add      eax, dword ptr [edx + 0x18]
0x49E1A6: mov      dword ptr [ebp - 0xc], eax
0x49E1A9: mov      eax, dword ptr [ebp - 4]
0x49E1AC: cmp      dword ptr [eax + 0x80], 6
0x49E1B3: jge      0x49e283
0x49E1B9: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49E1BE: mov      eax, dword ptr [eax]
0x49E1C0: mov      eax, dword ptr [eax + 0xb8]
0x49E1C6: shl      eax, 4
0x49E1C9: lea      eax, [eax + eax*2]
0x49E1CC: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49E1D2: mov      edx, dword ptr [edx]
0x49E1D4: add      eax, dword ptr [edx + 0x98]
0x49E1DA: mov      edx, dword ptr [ebp - 4]
0x49E1DD: sub      eax, dword ptr [edx + 0xb0]
0x49E1E3: mov      dword ptr [ebp - 0x20], eax
0x49E1E6: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49E1EB: mov      eax, dword ptr [eax]
0x49E1ED: mov      eax, dword ptr [eax + 0xbc]
0x49E1F3: shl      eax, 5
0x49E1F6: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49E1FC: mov      edx, dword ptr [edx]
0x49E1FE: add      eax, dword ptr [edx + 0x9c]
0x49E204: mov      edx, dword ptr [ebp - 4]
0x49E207: sub      eax, dword ptr [edx + 0xb4]
0x49E20D: mov      dword ptr [ebp - 0x24], eax
0x49E210: lea      eax, [ebp - 0x1c]
0x49E213: push     eax
0x49E214: lea      ecx, [ebp - 0x18]
0x49E217: mov      edx, dword ptr [ebp - 0xc]
0x49E21A: mov      eax, dword ptr [ebp - 4]
0x49E21D: add      edx, dword ptr [eax + 0x80]
0x49E223: mov      eax, dword ptr [ebp - 4]
0x49E226: mov      eax, dword ptr [eax + 0x14]
0x49E229: call     0x453a48
0x49E22E: mov      dword ptr [ebp - 0x28], eax
0x49E231: cmp      dword ptr [ebp - 0x28], 0
0x49E235: je       0x49e283
0x49E237: lea      eax, [ebp - 0x10]
0x49E23A: push     eax
0x49E23B: lea      eax, [ebp - 0x14]
0x49E23E: push     eax
0x49E23F: mov      eax, dword ptr [ebp - 4]
0x49E242: mov      eax, dword ptr [eax + 0xc]
0x49E245: mov      ecx, dword ptr [eax + 0xbc]
0x49E24B: mov      eax, dword ptr [ebp - 4]
0x49E24E: mov      eax, dword ptr [eax + 0xc]
0x49E251: mov      edx, dword ptr [eax + 0xb8]
0x49E257: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x49E25C: mov      eax, dword ptr [eax]
0x49E25E: call     0x472494
0x49E263: mov      eax, dword ptr [ebp - 0x28]
0x49E266: push     eax
0x49E267: push     1
0x49E269: mov      ecx, dword ptr [ebp - 0x14]
0x49E26C: add      ecx, dword ptr [ebp - 0x1c]
0x49E26F: sub      ecx, 0x10
0x49E272: mov      edx, dword ptr [ebp - 0x10]
0x49E275: add      edx, dword ptr [ebp - 0x18]
0x49E278: sub      edx, 0x18
0x49E27B: mov      eax, dword ptr [ebp - 8]
0x49E27E: call     0x455578
0x49E283: mov      esp, ebp
0x49E285: pop      ebp
0x49E286: ret      
0x49E287: nop      
0x49E288: push     ebp
0x49E289: mov      ebp, esp
0x49E28B: add      esp, -0xc
0x49E28E: test     dl, dl
0x49E290: je       0x49e29a
0x49E292: add      esp, -0x10
0x49E295: call     0x4031a4
0x49E29A: mov      dword ptr [ebp - 0xc], ecx
0x49E29D: mov      byte ptr [ebp - 5], dl
0x49E2A0: mov      dword ptr [ebp - 4], eax
0x49E2A3: mov      eax, dword ptr [ebp - 0xc]
0x49E2A6: push     eax
0x49E2A7: mov      eax, dword ptr [ebp + 0x18]
0x49E2AA: push     eax
0x49E2AB: mov      eax, dword ptr [ebp + 0x14]
0x49E2AE: push     eax
0x49E2AF: mov      eax, dword ptr [ebp + 0x10]
0x49E2B2: push     eax
0x49E2B3: mov      eax, dword ptr [ebp + 0xc]
0x49E2B6: push     eax
0x49E2B7: push     8
0x49E2B9: push     1
0x49E2BB: push     0
0x49E2BD: mov      ecx, 0x6f
0x49E2C2: xor      edx, edx
0x49E2C4: mov      eax, dword ptr [ebp - 4]
0x49E2C7: call     0x49c67c
0x49E2CC: mov      eax, dword ptr [ebp - 4]
0x49E2CF: mov      dword ptr [eax + 0x84], 3
0x49E2D9: mov      eax, dword ptr [ebp + 8]
0x49E2DC: mov      edx, dword ptr [ebp - 4]
0x49E2DF: mov      dword ptr [edx + 0x10], eax
0x49E2E2: mov      eax, dword ptr [ebp - 4]
0x49E2E5: mov      dword ptr [eax + 0x74], 0x32
0x49E2EC: mov      eax, dword ptr [ebp - 4]
0x49E2EF: cmp      byte ptr [ebp - 5], 0
0x49E2F3: je       0x49e304
0x49E2F5: call     0x4031fc
0x49E2FA: pop      dword ptr fs:[0]
0x49E301: add      esp, 0xc
0x49E304: mov      eax, dword ptr [ebp - 4]
0x49E307: mov      esp, ebp
0x49E309: pop      ebp
0x49E30A: ret      0x14
0x49E30D: lea      eax, [eax]
0x49E310: push     ebp
0x49E311: mov      ebp, esp
0x49E313: add      esp, -0x2c
0x49E316: mov      dword ptr [ebp - 8], edx
0x49E319: mov      dword ptr [ebp - 4], eax
0x49E31C: mov      eax, dword ptr [ebp - 4]
0x49E31F: cmp      byte ptr [eax + 4], 0
0x49E323: je       0x49e4b7
0x49E329: mov      eax, dword ptr [ebp - 4]
0x49E32C: mov      eax, dword ptr [eax + 0x44]
0x49E32F: mov      edx, dword ptr [ebp - 4]
0x49E332: sub      eax, dword ptr [edx + 0x98]
0x49E338: cdq      
0x49E339: xor      eax, edx
0x49E33B: sub      eax, edx
0x49E33D: cmp      eax, 0x1e
0x49E340: jg       0x49e368
0x49E342: mov      eax, dword ptr [ebp - 4]
0x49E345: mov      eax, dword ptr [eax + 0x48]
0x49E348: mov      edx, dword ptr [ebp - 4]
0x49E34B: sub      eax, dword ptr [edx + 0x9c]
0x49E351: cdq      
0x49E352: xor      eax, edx
0x49E354: sub      eax, edx
0x49E356: cmp      eax, 0x1e
0x49E359: jg       0x49e368
0x49E35B: mov      eax, dword ptr [ebp - 4]
0x49E35E: cmp      byte ptr [eax + 0x65], 0
0x49E362: je       0x49e4b7
0x49E368: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49E36D: mov      eax, dword ptr [eax]
0x49E36F: mov      eax, dword ptr [eax + 0xb8]
0x49E375: shl      eax, 4
0x49E378: lea      eax, [eax + eax*2]
0x49E37B: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49E381: mov      edx, dword ptr [edx]
0x49E383: add      eax, dword ptr [edx + 0x98]
0x49E389: mov      edx, dword ptr [ebp - 4]
0x49E38C: sub      eax, dword ptr [edx + 0xb0]
0x49E392: mov      dword ptr [ebp - 0x14], eax
0x49E395: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49E39A: mov      eax, dword ptr [eax]
0x49E39C: mov      eax, dword ptr [eax + 0xbc]
0x49E3A2: shl      eax, 5
0x49E3A5: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49E3AB: mov      edx, dword ptr [edx]
0x49E3AD: add      eax, dword ptr [edx + 0x9c]
0x49E3B3: mov      edx, dword ptr [ebp - 4]
0x49E3B6: sub      eax, dword ptr [edx + 0xb4]
0x49E3BC: mov      dword ptr [ebp - 0x18], eax
0x49E3BF: mov      eax, dword ptr [ebp - 4]
0x49E3C2: cmp      byte ptr [eax + 0x65], 0
0x49E3C6: jne      0x49e455
0x49E3CC: mov      eax, dword ptr [ebp - 4]
0x49E3CF: movzx    eax, byte ptr [eax + 0x30]
0x49E3D3: add      eax, eax
0x49E3D5: lea      eax, [eax + eax*4]
0x49E3D8: mov      edx, dword ptr [ebp - 4]
0x49E3DB: add      eax, dword ptr [edx + 0x18]
0x49E3DE: mov      dword ptr [ebp - 0xc], eax
0x49E3E1: mov      eax, dword ptr [ebp - 4]
0x49E3E4: add      eax, 0x24
0x49E3E7: push     eax
0x49E3E8: mov      eax, dword ptr [ebp - 4]
0x49E3EB: lea      ecx, [eax + 0x20]
0x49E3EE: mov      edx, dword ptr [ebp - 0xc]
0x49E3F1: mov      eax, dword ptr [ebp - 4]
0x49E3F4: add      edx, dword ptr [eax + 0x80]
0x49E3FA: mov      eax, dword ptr [ebp - 4]
0x49E3FD: mov      eax, dword ptr [eax + 0x14]
0x49E400: call     0x453a48
0x49E405: mov      dword ptr [ebp - 0x10], eax
0x49E408: cmp      dword ptr [ebp - 0x10], 0
0x49E40C: je       0x49e4b7
0x49E412: lea      edx, [ebp - 0x2c]
0x49E415: mov      eax, dword ptr [ebp - 0x10]
0x49E418: call     0x44d8ec
0x49E41D: lea      eax, [ebp - 0x2c]
0x49E420: push     eax
0x49E421: mov      eax, dword ptr [ebp - 0x10]
0x49E424: push     eax
0x49E425: push     1
0x49E427: mov      eax, dword ptr [ebp - 4]
0x49E42A: mov      ecx, dword ptr [eax + 0x48]
0x49E42D: mov      eax, dword ptr [ebp - 4]
0x49E430: add      ecx, dword ptr [eax + 0x24]
0x49E433: sub      ecx, 0x10
0x49E436: sub      ecx, dword ptr [ebp - 0x18]
0x49E439: mov      eax, dword ptr [ebp - 4]
0x49E43C: mov      edx, dword ptr [eax + 0x44]
0x49E43F: mov      eax, dword ptr [ebp - 4]
0x49E442: add      edx, dword ptr [eax + 0x20]
0x49E445: sub      edx, 0x18
0x49E448: sub      edx, dword ptr [ebp - 0x14]
0x49E44B: mov      eax, dword ptr [ebp - 8]
0x49E44E: call     0x44d6cc
0x49E453: jmp      0x49e4b7
0x49E455: mov      eax, dword ptr [ebp - 4]
0x49E458: mov      eax, dword ptr [eax + 0x1c]
0x49E45B: mov      edx, dword ptr [ebp - 4]
0x49E45E: add      eax, dword ptr [edx + 0x80]
0x49E464: mov      dword ptr [ebp - 0xc], eax
0x49E467: mov      eax, dword ptr [ebp - 4]
0x49E46A: add      eax, 0x24
0x49E46D: push     eax
0x49E46E: mov      eax, dword ptr [ebp - 4]
0x49E471: lea      ecx, [eax + 0x20]
0x49E474: mov      edx, dword ptr [ebp - 0xc]
0x49E477: mov      eax, dword ptr [ebp - 4]
0x49E47A: mov      eax, dword ptr [eax + 0x14]
0x49E47D: call     0x453a48
0x49E482: mov      dword ptr [ebp - 0x10], eax
0x49E485: cmp      dword ptr [ebp - 0x10], 0
0x49E489: je       0x49e4b7
0x49E48B: mov      eax, dword ptr [ebp - 0x10]
0x49E48E: push     eax
0x49E48F: push     1
0x49E491: mov      eax, dword ptr [ebp - 4]
0x49E494: mov      ecx, dword ptr [eax + 0x48]
0x49E497: mov      eax, dword ptr [ebp - 4]
0x49E49A: add      ecx, dword ptr [eax + 0x24]
0x49E49D: sub      ecx, 0x10
0x49E4A0: mov      eax, dword ptr [ebp - 4]
0x49E4A3: mov      edx, dword ptr [eax + 0x44]
0x49E4A6: mov      eax, dword ptr [ebp - 4]
0x49E4A9: add      edx, dword ptr [eax + 0x20]
0x49E4AC: sub      edx, 0x18
0x49E4AF: mov      eax, dword ptr [ebp - 8]
0x49E4B2: call     0x455578
0x49E4B7: mov      esp, ebp
0x49E4B9: pop      ebp
0x49E4BA: ret      
0x49E4BB: nop      
0x49E4BC: push     ebp
0x49E4BD: mov      ebp, esp
0x49E4BF: add      esp, -0xc
0x49E4C2: test     dl, dl
0x49E4C4: je       0x49e4ce
0x49E4C6: add      esp, -0x10
0x49E4C9: call     0x4031a4
0x49E4CE: mov      dword ptr [ebp - 0xc], ecx
0x49E4D1: mov      byte ptr [ebp - 5], dl
0x49E4D4: mov      dword ptr [ebp - 4], eax
0x49E4D7: mov      eax, dword ptr [ebp - 0xc]
0x49E4DA: push     eax
0x49E4DB: mov      eax, dword ptr [ebp + 0x14]
0x49E4DE: push     eax
0x49E4DF: mov      eax, dword ptr [ebp + 0x10]
0x49E4E2: push     eax
0x49E4E3: mov      eax, dword ptr [ebp + 0xc]
0x49E4E6: push     eax
0x49E4E7: mov      eax, dword ptr [ebp + 8]
0x49E4EA: push     eax
0x49E4EB: push     9
0x49E4ED: push     1
0x49E4EF: push     0
0x49E4F1: mov      ecx, 0x6f
0x49E4F6: xor      edx, edx
0x49E4F8: mov      eax, dword ptr [ebp - 4]
0x49E4FB: call     0x49c67c
0x49E500: mov      eax, dword ptr [ebp - 4]
0x49E503: mov      dword ptr [eax + 0x84], 3
0x49E50D: mov      eax, dword ptr [ebp + 0x18]
0x49E510: mov      edx, dword ptr [ebp - 4]
0x49E513: mov      dword ptr [edx + 0xc0], eax
0x49E519: mov      eax, dword ptr [ebp - 4]
0x49E51C: mov      byte ptr [eax + 0xc4], 0
0x49E523: mov      eax, dword ptr [ebp - 4]
0x49E526: mov      dword ptr [eax + 0x74], 0x32
0x49E52D: mov      eax, dword ptr [ebp - 4]
0x49E530: cmp      byte ptr [ebp - 5], 0
0x49E534: je       0x49e545
0x49E536: call     0x4031fc
0x49E53B: pop      dword ptr fs:[0]
0x49E542: add      esp, 0xc
0x49E545: mov      eax, dword ptr [ebp - 4]
0x49E548: mov      esp, ebp
0x49E54A: pop      ebp
0x49E54B: ret      0x14
0x49E54E: mov      eax, eax
0x49E550: push     ebp
0x49E551: mov      ebp, esp
0x49E553: add      esp, -8
0x49E556: mov      dword ptr [ebp - 4], eax
0x49E559: mov      eax, dword ptr [ebp - 4]
0x49E55C: call     0x49d308 ; TMagicEff.DMT_-1
0x49E561: mov      byte ptr [ebp - 5], al
0x49E564: mov      eax, dword ptr [ebp - 4]
0x49E567: cmp      byte ptr [eax + 0x65], 0
0x49E56B: jne      0x49e654
0x49E571: mov      eax, dword ptr [ebp - 4]
0x49E574: mov      eax, dword ptr [eax + 0x34]
0x49E577: mov      edx, dword ptr [ebp - 4]
0x49E57A: sub      eax, dword ptr [edx + 0x44]
0x49E57D: cdq      
0x49E57E: xor      eax, edx
0x49E580: sub      eax, edx
0x49E582: cmp      eax, 0xf
0x49E585: jg       0x49e59d
0x49E587: mov      eax, dword ptr [ebp - 4]
0x49E58A: mov      eax, dword ptr [eax + 0x38]
0x49E58D: mov      edx, dword ptr [ebp - 4]
0x49E590: sub      eax, dword ptr [edx + 0x48]
0x49E593: cdq      
0x49E594: xor      eax, edx
0x49E596: sub      eax, edx
0x49E598: cmp      eax, 0xf
0x49E59B: jle      0x49e5d5
0x49E59D: mov      eax, dword ptr [ebp - 4]
0x49E5A0: mov      eax, dword ptr [eax + 0x34]
0x49E5A3: mov      edx, dword ptr [ebp - 4]
0x49E5A6: sub      eax, dword ptr [edx + 0x44]
0x49E5A9: cdq      
0x49E5AA: xor      eax, edx
0x49E5AC: sub      eax, edx
0x49E5AE: mov      edx, dword ptr [ebp - 4]
0x49E5B1: cmp      eax, dword ptr [edx + 0xb8]
0x49E5B7: jl       0x49e620
0x49E5B9: mov      eax, dword ptr [ebp - 4]
0x49E5BC: mov      eax, dword ptr [eax + 0x38]
0x49E5BF: mov      edx, dword ptr [ebp - 4]
0x49E5C2: sub      eax, dword ptr [edx + 0x48]
0x49E5C5: cdq      
0x49E5C6: xor      eax, edx
0x49E5C8: sub      eax, edx
0x49E5CA: mov      edx, dword ptr [ebp - 4]
0x49E5CD: cmp      eax, dword ptr [edx + 0xbc]
0x49E5D3: jl       0x49e620
0x49E5D5: mov      eax, dword ptr [ebp - 4]
0x49E5D8: mov      byte ptr [eax + 0x65], 1
0x49E5DC: mov      eax, dword ptr [ebp - 4]
0x49E5DF: xor      edx, edx
0x49E5E1: mov      dword ptr [eax + 0x7c], edx
0x49E5E4: mov      eax, dword ptr [ebp - 4]
0x49E5E7: mov      eax, dword ptr [eax + 0x70]
0x49E5EA: mov      edx, dword ptr [ebp - 4]
0x49E5ED: mov      dword ptr [edx + 0x84], eax
0x49E5F3: mov      eax, dword ptr [ebp - 4]
0x49E5F6: mov      eax, dword ptr [eax + 0x7c]
0x49E5F9: mov      edx, dword ptr [ebp - 4]
0x49E5FC: mov      dword ptr [edx + 0x80], eax
0x49E602: mov      eax, dword ptr [ebp - 4]
0x49E605: mov      byte ptr [eax + 0x64], 0
0x49E609: mov      eax, dword ptr [ebp - 4]
0x49E60C: mov      eax, dword ptr [eax + 0xc]
0x49E60F: mov      eax, dword ptr [eax + 0x1ac]
0x49E615: call     0x495794
0x49E61A: mov      byte ptr [ebp - 5], 1
0x49E61E: jmp      0x49e654
0x49E620: mov      eax, dword ptr [ebp - 4]
0x49E623: mov      eax, dword ptr [eax + 0x34]
0x49E626: mov      edx, dword ptr [ebp - 4]
0x49E629: sub      eax, dword ptr [edx + 0x44]
0x49E62C: cdq      
0x49E62D: xor      eax, edx
0x49E62F: sub      eax, edx
0x49E631: mov      edx, dword ptr [ebp - 4]
0x49E634: mov      dword ptr [edx + 0xb8], eax
0x49E63A: mov      eax, dword ptr [ebp - 4]
0x49E63D: mov      eax, dword ptr [eax + 0x38]
0x49E640: mov      edx, dword ptr [ebp - 4]
0x49E643: sub      eax, dword ptr [edx + 0x48]
0x49E646: cdq      
0x49E647: xor      eax, edx
0x49E649: sub      eax, edx
0x49E64B: mov      edx, dword ptr [ebp - 4]
0x49E64E: mov      dword ptr [edx + 0xbc], eax
0x49E654: mov      al, byte ptr [ebp - 5]
0x49E657: pop      ecx
0x49E658: pop      ecx
0x49E659: pop      ebp
0x49E65A: ret      
0x49E65B: nop      
0x49E65C: push     ebp
0x49E65D: mov      ebp, esp
0x49E65F: add      esp, -0x2c
0x49E662: mov      dword ptr [ebp - 8], edx
0x49E665: mov      dword ptr [ebp - 4], eax
0x49E668: mov      eax, dword ptr [ebp - 4]
0x49E66B: cmp      byte ptr [eax + 4], 0
0x49E66F: je       0x49e830
0x49E675: mov      eax, dword ptr [ebp - 4]
0x49E678: mov      eax, dword ptr [eax + 0x44]
0x49E67B: mov      edx, dword ptr [ebp - 4]
0x49E67E: sub      eax, dword ptr [edx + 0x98]
0x49E684: cdq      
0x49E685: xor      eax, edx
0x49E687: sub      eax, edx
0x49E689: cmp      eax, 0x1e
0x49E68C: jg       0x49e6b4
0x49E68E: mov      eax, dword ptr [ebp - 4]
0x49E691: mov      eax, dword ptr [eax + 0x48]
0x49E694: mov      edx, dword ptr [ebp - 4]
0x49E697: sub      eax, dword ptr [edx + 0x9c]
0x49E69D: cdq      
0x49E69E: xor      eax, edx
0x49E6A0: sub      eax, edx
0x49E6A2: cmp      eax, 0x1e
0x49E6A5: jg       0x49e6b4
0x49E6A7: mov      eax, dword ptr [ebp - 4]
0x49E6AA: cmp      byte ptr [eax + 0x65], 0
0x49E6AE: je       0x49e830
0x49E6B4: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49E6B9: mov      eax, dword ptr [eax]
0x49E6BB: mov      eax, dword ptr [eax + 0xb8]
0x49E6C1: shl      eax, 4
0x49E6C4: lea      eax, [eax + eax*2]
0x49E6C7: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49E6CD: mov      edx, dword ptr [edx]
0x49E6CF: add      eax, dword ptr [edx + 0x98]
0x49E6D5: mov      edx, dword ptr [ebp - 4]
0x49E6D8: sub      eax, dword ptr [edx + 0xb0]
0x49E6DE: mov      dword ptr [ebp - 0x14], eax
0x49E6E1: mov      eax, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49E6E6: mov      eax, dword ptr [eax]
0x49E6E8: mov      eax, dword ptr [eax + 0xbc]
0x49E6EE: shl      eax, 5
0x49E6F1: mov      edx, dword ptr [0x4a3e9c] ; *0x4A3E9C->0x4F7EF8
0x49E6F7: mov      edx, dword ptr [edx]
0x49E6F9: add      eax, dword ptr [edx + 0x9c]
0x49E6FF: mov      edx, dword ptr [ebp - 4]
0x49E702: sub      eax, dword ptr [edx + 0xb4]
0x49E708: mov      dword ptr [ebp - 0x18], eax
0x49E70B: mov      eax, dword ptr [ebp - 4]
0x49E70E: cmp      byte ptr [eax + 0x65], 0
0x49E712: jne      0x49e7a4
0x49E718: mov      eax, dword ptr [ebp - 4]
0x49E71B: movzx    eax, byte ptr [eax + 0x30]
0x49E71F: add      eax, eax
0x49E721: lea      eax, [eax + eax*4]
0x49E724: mov      edx, dword ptr [ebp - 4]
0x49E727: add      eax, dword ptr [edx + 0x18]
0x49E72A: mov      dword ptr [ebp - 0xc], eax
0x49E72D: mov      eax, dword ptr [ebp - 4]
0x49E730: add      eax, 0x24
0x49E733: push     eax
0x49E734: mov      eax, dword ptr [ebp - 4]
0x49E737: lea      ecx, [eax + 0x20]
0x49E73A: mov      edx, dword ptr [ebp - 0xc]
0x49E73D: mov      eax, dword ptr [ebp - 4]
0x49E740: add      edx, dword ptr [eax + 0x80]
0x49E746: mov      eax, dword ptr [ebp - 4]
0x49E749: mov      eax, dword ptr [eax + 0x14]
0x49E74C: call     0x453a48
0x49E751: mov      dword ptr [ebp - 0x10], eax
0x49E754: cmp      dword ptr [ebp - 0x10], 0
0x49E758: je       0x49e830
0x49E75E: lea      edx, [ebp - 0x2c]
0x49E761: mov      eax, dword ptr [ebp - 0x10]
0x49E764: call     0x44d8ec
0x49E769: lea      eax, [ebp - 0x2c]
0x49E76C: push     eax
0x49E76D: mov      eax, dword ptr [ebp - 0x10]
0x49E770: push     eax
0x49E771: push     1
0x49E773: mov      eax, dword ptr [ebp - 4]
0x49E776: mov      ecx, dword ptr [eax + 0x48]
0x49E779: mov      eax, dword ptr [ebp - 4]
0x49E77C: add      ecx, dword ptr [eax + 0x24]
0x49E77F: sub      ecx, 0x10
0x49E782: sub      ecx, dword ptr [ebp - 0x18]
0x49E785: mov      eax, dword ptr [ebp - 4]
0x49E788: mov      edx, dword ptr [eax + 0x44]
0x49E78B: mov      eax, dword ptr [ebp - 4]
0x49E78E: add      edx, dword ptr [eax + 0x20]
0x49E791: sub      edx, 0x18
0x49E794: sub      edx, dword ptr [ebp - 0x14]
0x49E797: mov      eax, dword ptr [ebp - 8]
0x49E79A: call     0x44d6cc
0x49E79F: jmp      0x49e830
0x49E7A4: mov      eax, dword ptr [ebp - 4]
0x49E7A7: cmp      dword ptr [eax + 0xc0], 0xb
0x49E7AE: jne      0x49e7c9
0x49E7B0: mov      eax, dword ptr [ebp - 4]
0x49E7B3: mov      eax, dword ptr [eax + 0x18]
0x49E7B6: add      eax, 0xa0
0x49E7BB: mov      edx, dword ptr [ebp - 4]
0x49E7BE: add      eax, dword ptr [edx + 0x80]
0x49E7C4: mov      dword ptr [ebp - 0xc], eax
0x49E7C7: jmp      0x49e7e0
0x49E7C9: mov      eax, dword ptr [ebp - 4]
0x49E7CC: mov      eax, dword ptr [eax + 0x18]
0x49E7CF: add      eax, 0xb4
0x49E7D4: mov      edx, dword ptr [ebp - 4]
0x49E7D7: add      eax, dword ptr [edx + 0x80]
0x49E7DD: mov      dword ptr [ebp - 0xc], eax
0x49E7E0: mov      eax, dword ptr [ebp - 4]
0x49E7E3: add      eax, 0x24
0x49E7E6: push     eax
0x49E7E7: mov      eax, dword ptr [ebp - 4]
0x49E7EA: lea      ecx, [eax + 0x20]
0x49E7ED: mov      edx, dword ptr [ebp - 0xc]
0x49E7F0: mov      eax, dword ptr [ebp - 4]
0x49E7F3: mov      eax, dword ptr [eax + 0x14]
0x49E7F6: call     0x453a48
0x49E7FB: mov      dword ptr [ebp - 0x10], eax
0x49E7FE: cmp      dword ptr [ebp - 0x10], 0
0x49E802: je       0x49e830
0x49E804: mov      eax, dword ptr [ebp - 0x10]
0x49E807: push     eax
0x49E808: push     1
0x49E80A: mov      eax, dword ptr [ebp - 4]
0x49E80D: mov      ecx, dword ptr [eax + 0x48]
0x49E810: mov      eax, dword ptr [ebp - 4]
0x49E813: add      ecx, dword ptr [eax + 0x24]
0x49E816: sub      ecx, 0x10
0x49E819: mov      eax, dword ptr [ebp - 4]
0x49E81C: mov      edx, dword ptr [eax + 0x44]
0x49E81F: mov      eax, dword ptr [ebp - 4]
0x49E822: add      edx, dword ptr [eax + 0x20]
0x49E825: sub      edx, 0x18
0x49E828: mov      eax, dword ptr [ebp - 8]
0x49E82B: call     0x455578
0x49E830: mov      esp, ebp
0x49E832: pop      ebp
0x49E833: ret      
0x49E834: push     ebp
0x49E835: mov      ebp, esp
0x49E837: add      esp, -0xc
0x49E83A: test     dl, dl
0x49E83C: je       0x49e846
0x49E83E: add      esp, -0x10
0x49E841: call     0x4031a4
0x49E846: mov      dword ptr [ebp - 0xc], ecx
0x49E849: mov      byte ptr [ebp - 5], dl
0x49E84C: mov      dword ptr [ebp - 4], eax
0x49E84F: mov      eax, dword ptr [ebp + 0x14]
0x49E852: push     eax
0x49E853: mov      eax, dword ptr [ebp - 0xc]
0x49E856: push     eax
0x49E857: mov      eax, dword ptr [ebp + 0x1c]
0x49E85A: push     eax
0x49E85B: mov      eax, dword ptr [ebp - 0xc]
0x49E85E: push     eax
0x49E85F: mov      eax, dword ptr [ebp + 0x1c]
0x49E862: push     eax
0x49E863: push     0
0x49E865: push     1
0x49E867: push     0
0x49E869: mov      ecx, 0x6f
0x49E86E: xor      edx, edx
0x49E870: mov      eax, dword ptr [ebp - 4]
0x49E873: call     0x49c67c
0x49E878: mov      eax, dword ptr [ebp + 0x18]
0x49E87B: mov      edx, dword ptr [ebp - 4]
0x49E87E: mov      dword ptr [edx + 0x14], eax
0x49E881: mov      eax, dword ptr [ebp + 0x14]
0x49E884: mov      edx, dword ptr [ebp - 4]
0x49E887: mov      dword ptr [edx + 0x18], eax
0x49E88A: mov      eax, dword ptr [ebp - 4]
0x49E88D: xor      edx, edx
0x49E88F: mov      dword ptr [eax + 0x7c], edx
0x49E892: mov      eax, dword ptr [ebp - 4]
0x49E895: xor      edx, edx
0x49E897: mov      dword ptr [eax + 0x80], edx
0x49E89D: mov      eax, dword ptr [ebp + 0x10]
0x49E8A0: mov      edx, dword ptr [ebp - 4]
0x49E8A3: mov      dword ptr [edx + 0x84], eax
0x49E8A9: mov      eax, dword ptr [ebp + 0xc]
0x49E8AC: mov      edx, dword ptr [ebp - 4]
0x49E8AF: mov      dword ptr [edx + 0x74], eax
0x49E8B2: mov      al, byte ptr [ebp + 8]
0x49E8B5: mov      edx, dword ptr [ebp - 4]
0x49E8B8: mov      byte ptr [edx + 0xc0], al
0x49E8BE: mov      eax, dword ptr [ebp - 4]
0x49E8C1: cmp      byte ptr [ebp - 5], 0
0x49E8C5: je       0x49e8d6
0x49E8C7: call     0x4031fc
0x49E8CC: pop      dword ptr fs:[0]
0x49E8D3: add      esp, 0xc
0x49E8D6: mov      eax, dword ptr [ebp - 4]
0x49E8D9: mov      esp, ebp
0x49E8DB: pop      ebp
0x49E8DC: ret      0x18
0x49E8DF: nop      
0x49E8E0: push     ebp
0x49E8E1: mov      ebp, esp
0x49E8E3: add      esp, -8
0x49E8E6: mov      dword ptr [ebp - 4], eax
0x49E8E9: mov      byte ptr [ebp - 5], 1
0x49E8ED: mov      eax, dword ptr [ebp - 4]
0x49E8F0: cmp      byte ptr [eax + 4], 0
0x49E8F4: je       0x49e951
0x49E8F6: call     0x406154
0x49E8FB: mov      edx, dword ptr [ebp - 4]
0x49E8FE: sub      eax, dword ptr [edx + 0x94]
0x49E904: mov      edx, dword ptr [ebp - 4]
0x49E907: cmp      eax, dword ptr [edx + 0x74]
0x49E90A: jbe      0x49e951
0x49E90C: call     0x406154
0x49E911: mov      edx, dword ptr [ebp - 4]
0x49E914: mov      dword ptr [edx + 0x94], eax
0x49E91A: mov      eax, dword ptr [ebp - 4]
0x49E91D: inc      dword ptr [eax + 0x80]
0x49E923: mov      eax, dword ptr [ebp - 4]
0x49E926: mov      eax, dword ptr [eax + 0x7c]
0x49E929: mov      edx, dword ptr [ebp - 4]
0x49E92C: add      eax, dword ptr [edx + 0x84]
0x49E932: dec      eax
0x49E933: mov      edx, dword ptr [ebp - 4]
0x49E936: cmp      eax, dword ptr [edx + 0x80]
0x49E93C: jge      0x49e951
0x49E93E: mov      eax, dword ptr [ebp - 4]
0x49E941: mov      eax, dword ptr [eax + 0x7c]
0x49E944: mov      edx, dword ptr [ebp - 4]
0x49E947: mov      dword ptr [edx + 0x80], eax
0x49E94D: mov      byte ptr [ebp - 5], 0
0x49E951: mov      al, byte ptr [ebp - 5]
0x49E954: pop      ecx
0x49E955: pop      ecx
0x49E956: pop      ebp
0x49E957: ret      

; === TNormalDrawEffect.DMT_-3 @ 0x49E958 ===
0x49E958: push     ebp
0x49E959: mov      ebp, esp
0x49E95B: add      esp, -0x38
0x49E95E: mov      dword ptr [ebp - 8], edx
0x49E961: mov      dword ptr [ebp - 4], eax
0x49E964: mov      eax, dword ptr [ebp - 4]
0x49E967: mov      eax, dword ptr [eax + 0x18]
0x49E96A: mov      edx, dword ptr [ebp - 4]
0x49E96D: add      eax, dword ptr [edx + 0x80]
0x49E973: mov      dword ptr [ebp - 0xc], eax
0x49E976: lea      eax, [ebp - 0x1c]
0x49E979: push     eax
0x49E97A: lea      ecx, [ebp - 0x18]
0x49E97D: mov      edx, dword ptr [ebp - 0xc]
0x49E980: mov      eax, dword ptr [ebp - 4]
0x49E983: mov      eax, dword ptr [eax + 0x14]
0x49E986: call     0x453a48
0x49E98B: mov      dword ptr [ebp - 0x20], eax
0x49E98E: cmp      dword ptr [ebp - 0x20], 0
0x49E992: je       0x49ea11
0x49E994: lea      eax, [ebp - 0x10]
0x49E997: push     eax
0x49E998: lea      eax, [ebp - 0x14]
0x49E99B: push     eax
0x49E99C: mov      eax, dword ptr [0x4a3dd8] ; *0x4A3DD8->0x4F7D9C
0x49E9A1: mov      eax, dword ptr [eax]
0x49E9A3: mov      edx, dword ptr [ebp - 4]
0x49E9A6: mov      ecx, dword ptr [edx + 0x48]
0x49E9A9: mov      edx, dword ptr [ebp - 4]
0x49E9AC: mov      edx, dword ptr [edx + 0x44]
0x49E9AF: call     0x472494
0x49E9B4: mov      eax, dword ptr [ebp - 4]
0x49E9B7: cmp      byte ptr [eax + 0xc0], 0
0x49E9BE: je       0x49e9e2
0x49E9C0: mov      eax, dword ptr [ebp - 0x20]
0x49E9C3: push     eax
0x49E9C4: push     1
0x49E9C6: mov      ecx, dword ptr [ebp - 0x14]
0x49E9C9: add      ecx, dword ptr [ebp - 0x1c]
0x49E9CC: sub      ecx, 0x10
0x49E9CF: mov      edx, dword ptr [ebp - 0x10]
0x49E9D2: add      edx, dword ptr [ebp - 0x18]
0x49E9D5: sub      edx, 0x18
0x49E9D8: mov      eax, dword ptr [ebp - 8]
0x49E9DB: call     0x455578
0x49E9E0: jmp      0x49ea11
0x49E9E2: lea      edx, [ebp - 0x38]
0x49E9E5: mov      eax, dword ptr [ebp - 0x20]
0x49E9E8: call     0x44d8ec
0x49E9ED: lea      eax, [ebp - 0x38]
0x49E9F0: push     eax
0x49E9F1: mov      eax, dword ptr [ebp - 0x20]
0x49E9F4: push     eax
0x49E9F5: push     1
0x49E9F7: mov      ecx, dword ptr [ebp - 0x14]
0x49E9FA: add      ecx, dword ptr [ebp - 0x1c]
0x49E9FD: sub      ecx, 0x10
0x49EA00: mov      edx, dword ptr [ebp - 0x10]
0x49EA03: add      edx, dword ptr [ebp - 0x18]
0x49EA06: sub      edx, 0x18
0x49EA09: mov      eax, dword ptr [ebp - 8]
0x49EA0C: call     0x44d6cc
0x49EA11: mov      esp, ebp
0x49EA13: pop      ebp
0x49EA14: ret      
0x49EA15: lea      eax, [eax]
0x49EA18: push     ebp
0x49EA19: mov      ebp, esp
0x49EA1B: xor      eax, eax
0x49EA1D: push     ebp
0x49EA1E: push     0x49ea3d
0x49EA23: push     dword ptr fs:[eax]
0x49EA26: mov      dword ptr fs:[eax], esp
0x49EA29: inc      dword ptr [0x4f95dc]
0x49EA2F: xor      eax, eax
0x49EA31: pop      edx
0x49EA32: pop      ecx
0x49EA33: pop      ecx
0x49EA34: mov      dword ptr fs:[eax], edx
0x49EA37: push     0x49ea44
0x49EA3C: ret      
0x49EA3D: jmp      0x403518
0x49EA42: jmp      0x49ea3c
0x49EA44: pop      ebp
0x49EA45: ret      
0x49EA46: mov      eax, eax
0x49EA48: sub      dword ptr [0x4f95dc], 1
0x49EA4F: ret      
0x49EA50: pushfd   
0x49EA51: ljmp     0:0x49
0x49EA58: add      byte ptr [eax], al
0x49EA5A: add      byte ptr [eax], al
0x49EA5C: pushfd   
0x49EA5D: ljmp     0:0x49
0x49EA64: add      byte ptr [eax], al
0x49EA66: add      byte ptr [eax], al
0x49EA68: add      byte ptr [eax], al
0x49EA6A: add      byte ptr [eax], al
0x49EA6C: add      byte ptr [eax], al
0x49EA6E: add      byte ptr [eax], al
0x49EA70: mov      esi, 0xa00049ea
0x49EA75: scasb    al, byte ptr es:[edi]
0x49EA76: add      al, byte ptr [eax]
0x49EA78: mov      al, byte ptr [0xc8004010]
0x49EA7D: xor      byte ptr [eax], al
0x49EA80: aam      0x30
0x49EA82: inc      eax
0x49EA83: add      al, bl
0x49EA85: xor      byte ptr [eax], al
0x49EA88: fdiv     qword ptr [eax]
0x49EA8A: inc      eax
0x49EA8B: add      al, dl
0x49EA8D: xor      byte ptr [eax], al
0x49EA90: inc      eax
0x49EA91: inc      eax
0x49EA93: add      byte ptr [esi + ebp + 0x40], dl
0x49EA98: pushfd   
0x49EA99: inc      eax
