0048b2a0: 55               push     ebp
0048b2a1: 8bec             mov      ebp, esp
0048b2a3: 81c468ffffff     add      esp, 0xffffff68
0048b2a9: 53               push     ebx
0048b2aa: 33db             xor      ebx, ebx
0048b2ac: 899d78ffffff     mov      dword ptr [ebp - 0x88], ebx
0048b2b2: 895db4           mov      dword ptr [ebp - 0x4c], ebx
0048b2b5: 894df8           mov      dword ptr [ebp - 8], ecx
0048b2b8: 8955c8           mov      dword ptr [ebp - 0x38], edx
0048b2bb: 8945fc           mov      dword ptr [ebp - 4], eax
0048b2be: 33c0             xor      eax, eax
0048b2c0: 55               push     ebp
0048b2c1: 6864ba4800       push     0x48ba64
0048b2c6: 64ff30           push     dword ptr fs:[eax]
0048b2c9: 648920           mov      dword ptr fs:[eax], esp
0048b2cc: a1e83f4a00       mov      eax, dword ptr [0x4a3fe8]
0048b2d1: 8b00             mov      eax, dword ptr [eax]
0048b2d3: 8b8008030000     mov      eax, dword ptr [eax + 0x308]
0048b2d9: ba01000000       mov      edx, 1
0048b2de: e83982fcff       call     0x45351c
0048b2e3: 8945f4           mov      dword ptr [ebp - 0xc], eax
0048b2e6: 837df400         cmp      dword ptr [ebp - 0xc], 0
0048b2ea: 7435             je       0x48b321
0048b2ec: 8d5584           lea      edx, [ebp - 0x7c]
0048b2ef: 8b45f4           mov      eax, dword ptr [ebp - 0xc]
0048b2f2: e8f525fcff       call     0x44d8ec
0048b2f7: 8d4584           lea      eax, [ebp - 0x7c]
0048b2fa: 50               push     eax
0048b2fb: 8b45f4           mov      eax, dword ptr [ebp - 0xc]
0048b2fe: 50               push     eax
0048b2ff: 6a01             push     1
0048b301: 8b45fc           mov      eax, dword ptr [ebp - 4]
0048b304: 8b80d0020000     mov      eax, dword ptr [eax + 0x2d0]
0048b30a: 8b4834           mov      ecx, dword ptr [eax + 0x34]
0048b30d: 8b45fc           mov      eax, dword ptr [ebp - 4]
0048b310: 8b80d0020000     mov      eax, dword ptr [eax + 0x2d0]
0048b316: 8b5030           mov      edx, dword ptr [eax + 0x30]
0048b319: 8b45f8           mov      eax, dword ptr [ebp - 8]
0048b31c: e8ab23fcff       call     0x44d6cc
0048b321: 33c0             xor      eax, eax
0048b323: 8945f0           mov      dword ptr [ebp - 0x10], eax
0048b326: 837df400         cmp      dword ptr [ebp - 0xc], 0
0048b32a: 0f848b000000     je       0x48b3bb
0048b330: 8d55a4           lea      edx, [ebp - 0x5c]
0048b333: 8b45f4           mov      eax, dword ptr [ebp - 0xc]
0048b336: e8b125fcff       call     0x44d8ec
0048b33b: 8b45a8           mov      eax, dword ptr [ebp - 0x58]
0048b33e: 83c078           add      eax, 0x78
0048b341: 50               push     eax
0048b342: 8d45b8           lea      eax, [ebp - 0x48]
0048b345: 50               push     eax
0048b346: 8b4dac           mov      ecx, dword ptr [ebp - 0x54]
0048b349: 8b55a8           mov      edx, dword ptr [ebp - 0x58]
0048b34c: 8b45a4           mov      eax, dword ptr [ebp - 0x5c]
0048b34f: e86c39f8ff       call     0x40ecc0
0048b354: 8b45f4           mov      eax, dword ptr [ebp - 0xc]
0048b357: e8e825fcff       call     0x44d944
0048b35c: ba58020000       mov      edx, 0x258
0048b361: 2bd0             sub      edx, eax
0048b363: 8955f0           mov      dword ptr [ebp - 0x10], edx
0048b366: 8d45b8           lea      eax, [ebp - 0x48]
0048b369: 50               push     eax
0048b36a: 8b45f4           mov      eax, dword ptr [ebp - 0xc]
0048b36d: 50               push     eax
0048b36e: 6a01             push     1
0048b370: 8b4df0           mov      ecx, dword ptr [ebp - 0x10]
0048b373: 33d2             xor      edx, edx
0048b375: 8b45f8           mov      eax, dword ptr [ebp - 8]
0048b378: e84f23fcff       call     0x44d6cc
0048b37d: 8d5594           lea      edx, [ebp - 0x6c]
0048b380: 8b45f4           mov      eax, dword ptr [ebp - 0xc]
0048b383: e86425fcff       call     0x44d8ec
0048b388: 8b45a0           mov      eax, dword ptr [ebp - 0x60]
0048b38b: 50               push     eax
0048b38c: 8d45b8           lea      eax, [ebp - 0x48]
0048b38f: 50               push     eax
0048b390: 8b5598           mov      edx, dword ptr [ebp - 0x68]
0048b393: 83c278           add      edx, 0x78
0048b396: 8b4d9c           mov      ecx, dword ptr [ebp - 0x64]
0048b399: 8b4594           mov      eax, dword ptr [ebp - 0x6c]
0048b39c: e81f39f8ff       call     0x40ecc0
0048b3a1: 8d45b8           lea      eax, [ebp - 0x48]
0048b3a4: 50               push     eax
0048b3a5: 8b45f4           mov      eax, dword ptr [ebp - 0xc]
0048b3a8: 50               push     eax
0048b3a9: 6a00             push     0
0048b3ab: 8b4df0           mov      ecx, dword ptr [ebp - 0x10]
0048b3ae: 83c178           add      ecx, 0x78
0048b3b1: 33d2             xor      edx, edx
0048b3b3: 8b45f8           mov      eax, dword ptr [ebp - 8]
0048b3b6: e81123fcff       call     0x44d6cc
0048b3bb: 33c0             xor      eax, eax
0048b3bd: 8945f4           mov      dword ptr [ebp - 0xc], eax
0048b3c0: a1d03d4a00       mov      eax, dword ptr [0x4a3dd0]
0048b3c5: 8b00             mov      eax, dword ptr [eax]
0048b3c7: 83e801           sub      eax, 1
0048b3ca: 720a             jb       0x48b3d6
0048b3cc: 7424             je       0x48b3f2
0048b3ce: 48               dec      eax
0048b3cf: 743d             je       0x48b40e
0048b3d1: 48               dec      eax
0048b3d2: 7456             je       0x48b42a
0048b3d4: eb6e             jmp      0x48b444
0048b3d6: a1e83f4a00       mov      eax, dword ptr [0x4a3fe8]
0048b3db: 8b00             mov      eax, dword ptr [eax]
0048b3dd: 8b8008030000     mov      eax, dword ptr [eax + 0x308]
0048b3e3: ba0f000000       mov      edx, 0xf
0048b3e8: e82f81fcff       call     0x45351c
0048b3ed: 8945f4           mov      dword ptr [ebp - 0xc], eax
0048b3f0: eb52             jmp      0x48b444
0048b3f2: a1e83f4a00       mov      eax, dword ptr [0x4a3fe8]
0048b3f7: 8b00             mov      eax, dword ptr [eax]
0048b3f9: 8b8008030000     mov      eax, dword ptr [eax + 0x308]
0048b3ff: ba0c000000       mov      edx, 0xc
0048b404: e81381fcff       call     0x45351c
0048b409: 8945f4           mov      dword ptr [ebp - 0xc], eax
0048b40c: eb36             jmp      0x48b444
0048b40e: a1e83f4a00       mov      eax, dword ptr [0x4a3fe8]
0048b413: 8b00             mov      eax, dword ptr [eax]
0048b415: 8b8008030000     mov      eax, dword ptr [eax + 0x308]
0048b41b: ba0d000000       mov      edx, 0xd
0048b420: e8f780fcff       call     0x45351c
0048b425: 8945f4           mov      dword ptr [ebp - 0xc], eax
0048b428: eb1a             jmp      0x48b444
0048b42a: a1e83f4a00       mov      eax, dword ptr [0x4a3fe8]
0048b42f: 8b00             mov      eax, dword ptr [eax]
0048b431: 8b8008030000     mov      eax, dword ptr [eax + 0x308]
0048b437: ba0e000000       mov      edx, 0xe
0048b43c: e8db80fcff       call     0x45351c
0048b441: 8945f4           mov      dword ptr [ebp - 0xc], eax
0048b444: 837df400         cmp      dword ptr [ebp - 0xc], 0
0048b448: 7431             je       0x48b47b
0048b44a: 8d5584           lea      edx, [ebp - 0x7c]
0048b44d: 8b45f4           mov      eax, dword ptr [ebp - 0xc]
0048b450: e89724fcff       call     0x44d8ec
0048b455: 8d4584           lea      eax, [ebp - 0x7c]
0048b458: 50               push     eax
0048b459: 8b45f4           mov      eax, dword ptr [ebp - 0xc]
0048b45c: 50               push     eax
0048b45d: 6a00             push     0
0048b45f: 8b45fc           mov      eax, dword ptr [ebp - 4]
0048b462: 8b80d0020000     mov      eax, dword ptr [eax + 0x2d0]
0048b468: 8b4834           mov      ecx, dword ptr [eax + 0x34]
0048b46b: 83c14f           add      ecx, 0x4f
0048b46e: baec020000       mov      edx, 0x2ec
0048b473: 8b45f8           mov      eax, dword ptr [ebp - 8]
0048b476: e85122fcff       call     0x44d6cc
0048b47b: a19c3e4a00       mov      eax, dword ptr [0x4a3e9c]
0048b480: 833800           cmp      dword ptr [eax], 0
0048b483: 0f84a7040000     je       0x48b930
0048b489: a19c3e4a00       mov      eax, dword ptr [0x4a3e9c]
0048b48e: 8b00             mov      eax, dword ptr [eax]
0048b490: 6683784000       cmp      word ptr [eax + 0x40], 0
0048b495: 0f8686020000     jbe      0x48b721
0048b49b: a19c3e4a00       mov      eax, dword ptr [0x4a3e9c]
0048b4a0: 8b00             mov      eax, dword ptr [eax]
0048b4a2: 6683784200       cmp      word ptr [eax + 0x42], 0
0048b4a7: 0f8674020000     jbe      0x48b721
0048b4ad: a19c3e4a00       mov      eax, dword ptr [0x4a3e9c]
0048b4b2: 8b00             mov      eax, dword ptr [eax]
0048b4b4: 80781200         cmp      byte ptr [eax + 0x12], 0
0048b4b8: 0f851a010000     jne      0x48b5d8
0048b4be: a19c3e4a00       mov      eax, dword ptr [0x4a3e9c]
0048b4c3: 8b00             mov      eax, dword ptr [eax]
0048b4c5: 8078301c         cmp      byte ptr [eax + 0x30], 0x1c
0048b4c9: 0f8309010000     jae      0x48b5d8
0048b4cf: a1e83f4a00       mov      eax, dword ptr [0x4a3fe8]
0048b4d4: 8b00             mov      eax, dword ptr [eax]
0048b4d6: 8b8008030000     mov      eax, dword ptr [eax + 0x308]
0048b4dc: ba05000000       mov      edx, 5
0048b4e1: e83680fcff       call     0x45351c
0048b4e6: 8945f4           mov      dword ptr [ebp - 0xc], eax
0048b4e9: 837df400         cmp      dword ptr [ebp - 0xc], 0
0048b4ed: 743c             je       0x48b52b
0048b4ef: 8d55b8           lea      edx, [ebp - 0x48]
0048b4f2: 8b45f4           mov      eax, dword ptr [ebp - 0xc]
0048b4f5: e8f223fcff       call     0x44d8ec
0048b4fa: 8d5584           lea      edx, [ebp - 0x7c]
0048b4fd: 8b45f4           mov      eax, dword ptr [ebp - 0xc]
0048b500: e8e723fcff       call     0x44d8ec
0048b505: 8b458c           mov      eax, dword ptr [ebp - 0x74]
0048b508: 83e802           sub      eax, 2
0048b50b: 8945c0           mov      dword ptr [ebp - 0x40], eax
0048b50e: 8d45b8           lea      eax, [ebp - 0x48]
0048b511: 50               push     eax
0048b512: 8b45f4           mov      eax, dword ptr [ebp - 0xc]
0048b515: 50               push     eax
0048b516: 6a00             push     0
0048b518: 8b4df0           mov      ecx, dword ptr [ebp - 0x10]
0048b51b: 83c15a           add      ecx, 0x5a
0048b51e: ba26000000       mov      edx, 0x26
0048b523: 8b45f8           mov      eax, dword ptr [ebp - 8]
0048b526: e8a121fcff       call     0x44d6cc
0048b52b: a1e83f4a00       mov      eax, dword ptr [0x4a3fe8]
0048b530: 8b00             mov      eax, dword ptr [eax]
0048b532: 8b8008030000     mov      eax, dword ptr [eax + 0x308]
0048b538: ba06000000       mov      edx, 6
0048b53d: e8da7ffcff       call     0x45351c
0048b542: 8945f4           mov      dword ptr [ebp - 0xc], eax
0048b545: 837df400         cmp      dword ptr [ebp - 0xc], 0
0048b549: 0f84d2010000     je       0x48b721
0048b54f: 8d55b8           lea      edx, [ebp - 0x48]
0048b552: 8b45f4           mov      eax, dword ptr [ebp - 0xc]
0048b555: e89223fcff       call     0x44d8ec
0048b55a: 8d5584           lea      edx, [ebp - 0x7c]
0048b55d: 8b45f4           mov      eax, dword ptr [ebp - 0xc]
0048b560: e88723fcff       call     0x44d8ec
0048b565: 8b458c           mov      eax, dword ptr [ebp - 0x74]
0048b568: 83e802           sub      eax, 2
0048b56b: 8945c0           mov      dword ptr [ebp - 0x40], eax
0048b56e: db45c4           fild     dword ptr [ebp - 0x3c]
0048b571: a19c3e4a00       mov      eax, dword ptr [0x4a3e9c]
0048b576: 8b00             mov      eax, dword ptr [eax]
0048b578: 0fb74040         movzx    eax, word ptr [eax + 0x40]
0048b57c: 894580           mov      dword ptr [ebp - 0x80], eax
0048b57f: db4580           fild     dword ptr [ebp - 0x80]
0048b582: def9             fdivp    st(1)
0048b584: a19c3e4a00       mov      eax, dword ptr [0x4a3e9c]
0048b589: 8b00             mov      eax, dword ptr [eax]
0048b58b: 0fb74040         movzx    eax, word ptr [eax + 0x40]
0048b58f: 8b159c3e4a00     mov      edx, dword ptr [0x4a3e9c]
0048b595: 8b12             mov      edx, dword ptr [edx]
0048b597: 0fb7523c         movzx    edx, word ptr [edx + 0x3c]
0048b59b: 2bc2             sub      eax, edx
0048b59d: 89857cffffff     mov      dword ptr [ebp - 0x84], eax
0048b5a3: db857cffffff     fild     dword ptr [ebp - 0x84]
0048b5a9: dec9             fmulp    st(1)
0048b5ab: e8f073f7ff       call     0x4029a0
0048b5b0: 8945bc           mov      dword ptr [ebp - 0x44], eax
0048b5b3: 8d45b8           lea      eax, [ebp - 0x48]
0048b5b6: 50               push     eax
0048b5b7: 8b45f4           mov      eax, dword ptr [ebp - 0xc]
0048b5ba: 50               push     eax
0048b5bb: 6a00             push     0
0048b5bd: 8b4df0           mov      ecx, dword ptr [ebp - 0x10]
0048b5c0: 83c15a           add      ecx, 0x5a
0048b5c3: 034dbc           add      ecx, dword ptr [ebp - 0x44]
0048b5c6: ba26000000       mov      edx, 0x26
0048b5cb: 8b45f8           mov      eax, dword ptr [ebp - 8]
0048b5ce: e8f920fcff       call     0x44d6cc
0048b5d3: e949010000       jmp      0x48b721
0048b5d8: a1e83f4a00       mov      eax, dword ptr [0x4a3fe8]
0048b5dd: 8b00             mov      eax, dword ptr [eax]
0048b5df: 8b8008030000     mov      eax, dword ptr [eax + 0x308]
0048b5e5: ba04000000       mov      edx, 4
0048b5ea: e82d7ffcff       call     0x45351c
0048b5ef: 8945f4           mov      dword ptr [ebp - 0xc], eax
0048b5f2: 837df400         cmp      dword ptr [ebp - 0xc], 0
0048b5f6: 0f8425010000     je       0x48b721
0048b5fc: 8d55b8           lea      edx, [ebp - 0x48]
0048b5ff: 8b45f4           mov      eax, dword ptr [ebp - 0xc]
0048b602: e8e522fcff       call     0x44d8ec
0048b607: 8d5584           lea      edx, [ebp - 0x7c]
0048b60a: 8b45f4           mov      eax, dword ptr [ebp - 0xc]
0048b60d: e8da22fcff       call     0x44d8ec
0048b612: 8b458c           mov      eax, dword ptr [ebp - 0x74]
0048b615: d1f8             sar      eax, 1
0048b617: 7903             jns      0x48b61c
0048b619: 83d000           adc      eax, 0
0048b61c: 48               dec      eax
0048b61d: 8945c0           mov      dword ptr [ebp - 0x40], eax
0048b620: db45c4           fild     dword ptr [ebp - 0x3c]
0048b623: a19c3e4a00       mov      eax, dword ptr [0x4a3e9c]
0048b628: 8b00             mov      eax, dword ptr [eax]
0048b62a: 0fb74040         movzx    eax, word ptr [eax + 0x40]
0048b62e: 894580           mov      dword ptr [ebp - 0x80], eax
0048b631: db4580           fild     dword ptr [ebp - 0x80]
0048b634: def9             fdivp    st(1)
0048b636: a19c3e4a00       mov      eax, dword ptr [0x4a3e9c]
0048b63b: 8b00             mov      eax, dword ptr [eax]
0048b63d: 0fb74040         movzx    eax, word ptr [eax + 0x40]
0048b641: 8b159c3e4a00     mov      edx, dword ptr [0x4a3e9c]
0048b647: 8b12             mov      edx, dword ptr [edx]
0048b649: 0fb7523c         movzx    edx, word ptr [edx + 0x3c]
0048b64d: 2bc2             sub      eax, edx
0048b64f: 89857cffffff     mov      dword ptr [ebp - 0x84], eax
0048b655: db857cffffff     fild     dword ptr [ebp - 0x84]
0048b65b: dec9             fmulp    st(1)
0048b65d: e83e73f7ff       call     0x4029a0
0048b662: 8945bc           mov      dword ptr [ebp - 0x44], eax
0048b665: 8d45b8           lea      eax, [ebp - 0x48]
0048b668: 50               push     eax
0048b669: 8b45f4           mov      eax, dword ptr [ebp - 0xc]
0048b66c: 50               push     eax
0048b66d: 6a00             push     0
0048b66f: 8b4df0           mov      ecx, dword ptr [ebp - 0x10]
0048b672: 83c15b           add      ecx, 0x5b
0048b675: 034dbc           add      ecx, dword ptr [ebp - 0x44]
0048b678: ba28000000       mov      edx, 0x28
0048b67d: 8b45f8           mov      eax, dword ptr [ebp - 8]
0048b680: e84720fcff       call     0x44d6cc
0048b685: 8d55b8           lea      edx, [ebp - 0x48]
0048b688: 8b45f4           mov      eax, dword ptr [ebp - 0xc]
0048b68b: e85c22fcff       call     0x44d8ec
0048b690: 8d5584           lea      edx, [ebp - 0x7c]
0048b693: 8b45f4           mov      eax, dword ptr [ebp - 0xc]
0048b696: e85122fcff       call     0x44d8ec
0048b69b: 8b458c           mov      eax, dword ptr [ebp - 0x74]
0048b69e: d1f8             sar      eax, 1
0048b6a0: 7903             jns      0x48b6a5
0048b6a2: 83d000           adc      eax, 0
0048b6a5: 40               inc      eax
0048b6a6: 8945b8           mov      dword ptr [ebp - 0x48], eax
0048b6a9: 8d5584           lea      edx, [ebp - 0x7c]
0048b6ac: 8b45f4           mov      eax, dword ptr [ebp - 0xc]
0048b6af: e83822fcff       call     0x44d8ec
0048b6b4: 8b458c           mov      eax, dword ptr [ebp - 0x74]
0048b6b7: 48               dec      eax
0048b6b8: 8945c0           mov      dword ptr [ebp - 0x40], eax
0048b6bb: db45c4           fild     dword ptr [ebp - 0x3c]
0048b6be: a19c3e4a00       mov      eax, dword ptr [0x4a3e9c]
0048b6c3: 8b00             mov      eax, dword ptr [eax]
0048b6c5: 0fb74042         movzx    eax, word ptr [eax + 0x42]
0048b6c9: 894580           mov      dword ptr [ebp - 0x80], eax
0048b6cc: db4580           fild     dword ptr [ebp - 0x80]
0048b6cf: def9             fdivp    st(1)
0048b6d1: a19c3e4a00       mov      eax, dword ptr [0x4a3e9c]
0048b6d6: 8b00             mov      eax, dword ptr [eax]
0048b6d8: 0fb74042         movzx    eax, word ptr [eax + 0x42]
0048b6dc: 8b159c3e4a00     mov      edx, dword ptr [0x4a3e9c]
0048b6e2: 8b12             mov      edx, dword ptr [edx]
0048b6e4: 0fb7523e         movzx    edx, word ptr [edx + 0x3e]
0048b6e8: 2bc2             sub      eax, edx
0048b6ea: 89857cffffff     mov      dword ptr [ebp - 0x84], eax
0048b6f0: db857cffffff     fild     dword ptr [ebp - 0x84]
0048b6f6: dec9             fmulp    st(1)
0048b6f8: e8a372f7ff       call     0x4029a0
0048b6fd: 8945bc           mov      dword ptr [ebp - 0x44], eax
0048b700: 8d45b8           lea      eax, [ebp - 0x48]
0048b703: 50               push     eax
0048b704: 8b45f4           mov      eax, dword ptr [ebp - 0xc]
0048b707: 50               push     eax
0048b708: 6a00             push     0
0048b70a: 8b4df0           mov      ecx, dword ptr [ebp - 0x10]
0048b70d: 83c15b           add      ecx, 0x5b
0048b710: 034dbc           add      ecx, dword ptr [ebp - 0x44]
0048b713: 8b55b8           mov      edx, dword ptr [ebp - 0x48]
0048b716: 83c228           add      edx, 0x28
0048b719: 8b45f8           mov      eax, dword ptr [ebp - 8]
0048b71c: e8ab1ffcff       call     0x44d6cc
0048b721: 8b45f8           mov      eax, dword ptr [ebp - 8]
0048b724: e88b21fcff       call     0x44d8b4
0048b729: 8945d0           mov      dword ptr [ebp - 0x30], eax
0048b72c: 8d9578ffffff     lea      edx, [ebp - 0x88]
0048b732: a19c3e4a00       mov      eax, dword ptr [0x4a3e9c]
0048b737: 8b00             mov      eax, dword ptr [eax]
0048b739: 0fb64030         movzx    eax, byte ptr [eax + 0x30]
0048b73d: e822c6f7ff       call     0x407d64
0048b742: 8b8578ffffff     mov      eax, dword ptr [ebp - 0x88]
0048b748: 50               push     eax
0048b749: b9f0010000       mov      ecx, 0x1f0
0048b74e: ba94020000       mov      edx, 0x294
0048b753: 8b45f8           mov      eax, dword ptr [ebp - 8]
0048b756: e81535fdff       call     0x45ec70
0048b75b: a19c3e4a00       mov      eax, dword ptr [0x4a3e9c]
0048b760: 8b00             mov      eax, dword ptr [eax]
0048b762: 83784c00         cmp      dword ptr [eax + 0x4c], 0
0048b766: 0f8669010000     jbe      0x48b8d5
0048b76c: a19c3e4a00       mov      eax, dword ptr [0x4a3e9c]
0048b771: 8b00             mov      eax, dword ptr [eax]
0048b773: 6683785200       cmp      word ptr [eax + 0x52], 0
0048b778: 0f8657010000     jbe      0x48b8d5
0048b77e: a1e83f4a00       mov      eax, dword ptr [0x4a3fe8]
0048b783: 8b00             mov      eax, dword ptr [eax]
0048b785: 8b8008030000     mov      eax, dword ptr [eax + 0x308]
0048b78b: ba07000000       mov      edx, 7
0048b790: e8877dfcff       call     0x45351c
0048b795: 8945f4           mov      dword ptr [ebp - 0xc], eax
0048b798: 837df400         cmp      dword ptr [ebp - 0xc], 0
0048b79c: 0f8433010000     je       0x48b8d5
0048b7a2: 8d55b8           lea      edx, [ebp - 0x48]
0048b7a5: 8b45f4           mov      eax, dword ptr [ebp - 0xc]
0048b7a8: e83f21fcff       call     0x44d8ec
0048b7ad: a19c3e4a00       mov      eax, dword ptr [0x4a3e9c]
0048b7b2: 8b00             mov      eax, dword ptr [eax]
0048b7b4: 83784800         cmp      dword ptr [eax + 0x48], 0
0048b7b8: 7644             jbe      0x48b7fe
0048b7ba: a19c3e4a00       mov      eax, dword ptr [0x4a3e9c]
0048b7bf: 8b00             mov      eax, dword ptr [eax]
0048b7c1: 8b404c           mov      eax, dword ptr [eax + 0x4c]
0048b7c4: 898570ffffff     mov      dword ptr [ebp - 0x90], eax
0048b7ca: 33c0             xor      eax, eax
0048b7cc: 898574ffffff     mov      dword ptr [ebp - 0x8c], eax
0048b7d2: dfad70ffffff     fild     qword ptr [ebp - 0x90]
0048b7d8: a19c3e4a00       mov      eax, dword ptr [0x4a3e9c]
0048b7dd: 8b00             mov      eax, dword ptr [eax]
0048b7df: 8b4048           mov      eax, dword ptr [eax + 0x48]
0048b7e2: 898568ffffff     mov      dword ptr [ebp - 0x98], eax
0048b7e8: 33c0             xor      eax, eax
0048b7ea: 89856cffffff     mov      dword ptr [ebp - 0x94], eax
0048b7f0: dfad68ffffff     fild     qword ptr [ebp - 0x98]
0048b7f6: def9             fdivp    st(1)
0048b7f8: dd5dd4           fstp     qword ptr [ebp - 0x2c]
0048b7fb: 9b               wait     
0048b7fc: eb08             jmp      0x48b806
0048b7fe: 33c0             xor      eax, eax
0048b800: 8945d4           mov      dword ptr [ebp - 0x2c], eax
0048b803: 8945d8           mov      dword ptr [ebp - 0x28], eax
0048b806: dd45d4           fld      qword ptr [ebp - 0x2c]
0048b809: d81d70ba4800     fcomp    dword ptr [0x48ba70]
0048b80f: dfe0             fnstsw   ax
0048b811: 9e               sahf     
0048b812: 7610             jbe      0x48b824
0048b814: db45c0           fild     dword ptr [ebp - 0x40]
0048b817: dc75d4           fdiv     qword ptr [ebp - 0x2c]
0048b81a: e88171f7ff       call     0x4029a0
0048b81f: 8945c0           mov      dword ptr [ebp - 0x40], eax
0048b822: eb05             jmp      0x48b829
0048b824: 33c0             xor      eax, eax
0048b826: 8945c0           mov      dword ptr [ebp - 0x40], eax
0048b829: 8d45b8           lea      eax, [ebp - 0x48]
0048b82c: 50               push     eax
0048b82d: 8b45f4           mov      eax, dword ptr [ebp - 0xc]
0048b830: 50               push     eax
0048b831: 6a00             push     0
0048b833: b90f020000       mov      ecx, 0x20f
0048b838: ba9a020000       mov      edx, 0x29a
0048b83d: 8b45f8           mov      eax, dword ptr [ebp - 8]
0048b840: e8871efcff       call     0x44d6cc
0048b845: 8d55b8           lea      edx, [ebp - 0x48]
0048b848: 8b45f4           mov      eax, dword ptr [ebp - 0xc]
0048b84b: e89c20fcff       call     0x44d8ec
0048b850: a19c3e4a00       mov      eax, dword ptr [0x4a3e9c]
0048b855: 8b00             mov      eax, dword ptr [eax]
0048b857: 6683785000       cmp      word ptr [eax + 0x50], 0
0048b85c: 7630             jbe      0x48b88e
0048b85e: a19c3e4a00       mov      eax, dword ptr [0x4a3e9c]
0048b863: 8b00             mov      eax, dword ptr [eax]
0048b865: 0fb74052         movzx    eax, word ptr [eax + 0x52]
0048b869: 894580           mov      dword ptr [ebp - 0x80], eax
0048b86c: db4580           fild     dword ptr [ebp - 0x80]
0048b86f: a19c3e4a00       mov      eax, dword ptr [0x4a3e9c]
0048b874: 8b00             mov      eax, dword ptr [eax]
0048b876: 0fb74050         movzx    eax, word ptr [eax + 0x50]
0048b87a: 89857cffffff     mov      dword ptr [ebp - 0x84], eax
0048b880: db857cffffff     fild     dword ptr [ebp - 0x84]
0048b886: def9             fdivp    st(1)
0048b888: dd5dd4           fstp     qword ptr [ebp - 0x2c]
0048b88b: 9b               wait     
0048b88c: eb08             jmp      0x48b896
0048b88e: 33c0             xor      eax, eax
0048b890: 8945d4           mov      dword ptr [ebp - 0x2c], eax
0048b893: 8945d8           mov      dword ptr [ebp - 0x28], eax
0048b896: dd45d4           fld      qword ptr [ebp - 0x2c]
0048b899: d81d70ba4800     fcomp    dword ptr [0x48ba70]
0048b89f: dfe0             fnstsw   ax
0048b8a1: 9e               sahf     
0048b8a2: 7610             jbe      0x48b8b4
0048b8a4: db45c0           fild     dword ptr [ebp - 0x40]
0048b8a7: dc75d4           fdiv     qword ptr [ebp - 0x2c]
0048b8aa: e8f170f7ff       call     0x4029a0
0048b8af: 8945c0           mov      dword ptr [ebp - 0x40], eax
0048b8b2: eb05             jmp      0x48b8b9
0048b8b4: 33c0             xor      eax, eax
0048b8b6: 8945c0           mov      dword ptr [ebp - 0x40], eax
0048b8b9: 8d45b8           lea      eax, [ebp - 0x48]
0048b8bc: 50               push     eax
0048b8bd: 8b45f4           mov      eax, dword ptr [ebp - 0xc]
0048b8c0: 50               push     eax
0048b8c1: 6a00             push     0
0048b8c3: b930020000       mov      ecx, 0x230
0048b8c8: ba9a020000       mov      edx, 0x29a
0048b8cd: 8b45f8           mov      eax, dword ptr [ebp - 8]
0048b8d0: e8f71dfcff       call     0x44d6cc
0048b8d5: a1883d4a00       mov      eax, dword ptr [0x4a3d88]
0048b8da: 8b00             mov      eax, dword ptr [eax]
0048b8dc: 48               dec      eax
0048b8dd: 83e804           sub      eax, 4
0048b8e0: 734e             jae      0x48b930
0048b8e2: 8b15883d4a00     mov      edx, dword ptr [0x4a3d88]
0048b8e8: 8b12             mov      edx, dword ptr [edx]
0048b8ea: 83c210           add      edx, 0x10
0048b8ed: 4a               dec      edx
0048b8ee: a1e83f4a00       mov      eax, dword ptr [0x4a3fe8]
0048b8f3: 8b00             mov      eax, dword ptr [eax]
0048b8f5: 8b8008030000     mov      eax, dword ptr [eax + 0x308]
0048b8fb: e81c7cfcff       call     0x45351c
0048b900: 8945f4           mov      dword ptr [ebp - 0xc], eax
0048b903: 837df400         cmp      dword ptr [ebp - 0xc], 0
0048b907: 7427             je       0x48b930
0048b909: 8d5584           lea      edx, [ebp - 0x7c]
0048b90c: 8b45f4           mov      eax, dword ptr [ebp - 0xc]
0048b90f: e8d81ffcff       call     0x44d8ec
0048b914: 8d4584           lea      eax, [ebp - 0x7c]
0048b917: 50               push     eax
0048b918: 8b45f4           mov      eax, dword ptr [ebp - 0xc]
0048b91b: 50               push     eax
0048b91c: 6a01             push     1
0048b91e: b929020000       mov      ecx, 0x229
0048b923: baf2020000       mov      edx, 0x2f2
0048b928: 8b45f8           mov      eax, dword ptr [ebp - 8]
0048b92b: e89c1dfcff       call     0x44d6cc
0048b930: c745ecd0000000   mov      dword ptr [ebp - 0x14], 0xd0
0048b937: c745e8d6010000   mov      dword ptr [ebp - 0x18], 0x1d6
0048b93e: 6a02             push     2
0048b940: 8b45f8           mov      eax, dword ptr [ebp - 8]
0048b943: e86c1ffcff       call     0x44d8b4
0048b948: e8ffc9f8ff       call     0x41834c
0048b94d: 50               push     eax
0048b94e: e8e1aaf7ff       call     0x406434
0048b953: a1b83f4a00       mov      eax, dword ptr [0x4a3fb8]
0048b958: 8b00             mov      eax, dword ptr [eax]
0048b95a: 8b4020           mov      eax, dword ptr [eax + 0x20]
0048b95d: 8b15b83f4a00     mov      edx, dword ptr [0x4a3fb8]
0048b963: 8b12             mov      edx, dword ptr [edx]
0048b965: 8b5220           mov      edx, dword ptr [edx + 0x20]
0048b968: 83c209           add      edx, 9
0048b96b: 4a               dec      edx
0048b96c: 2bd0             sub      edx, eax
0048b96e: 0f8cc2000000     jl       0x48ba36
0048b974: 42               inc      edx
0048b975: 8955cc           mov      dword ptr [ebp - 0x34], edx
0048b978: 8945e4           mov      dword ptr [ebp - 0x1c], eax
0048b97b: a1b83f4a00       mov      eax, dword ptr [0x4a3fb8]
0048b980: 8b00             mov      eax, dword ptr [eax]
0048b982: 8b4018           mov      eax, dword ptr [eax + 0x18]
0048b985: 8b10             mov      edx, dword ptr [eax]
0048b987: ff5214           call     dword ptr [edx + 0x14]
0048b98a: 48               dec      eax
0048b98b: 3b45e4           cmp      eax, dword ptr [ebp - 0x1c]
0048b98e: 0f8ca2000000     jl       0x48ba36
0048b994: a1b83f4a00       mov      eax, dword ptr [0x4a3fb8]
0048b999: 8b00             mov      eax, dword ptr [eax]
0048b99b: 8b4018           mov      eax, dword ptr [eax + 0x18]
0048b99e: 8b55e4           mov      edx, dword ptr [ebp - 0x1c]
0048b9a1: 8b08             mov      ecx, dword ptr [eax]
0048b9a3: ff5118           call     dword ptr [ecx + 0x18]
0048b9a6: 8945e0           mov      dword ptr [ebp - 0x20], eax
0048b9a9: a1b83f4a00       mov      eax, dword ptr [0x4a3fb8]
0048b9ae: 8b00             mov      eax, dword ptr [eax]
0048b9b0: 8b401c           mov      eax, dword ptr [eax + 0x1c]
0048b9b3: 8b55e4           mov      edx, dword ptr [ebp - 0x1c]
0048b9b6: e8e53cf8ff       call     0x40f6a0
0048b9bb: 8945dc           mov      dword ptr [ebp - 0x24], eax
0048b9be: 8b45f8           mov      eax, dword ptr [ebp - 8]
0048b9c1: e8ee1efcff       call     0x44d8b4
0048b9c6: 8b400c           mov      eax, dword ptr [eax + 0xc]
0048b9c9: 8b55e0           mov      edx, dword ptr [ebp - 0x20]
0048b9cc: e82bbaf8ff       call     0x4173fc
0048b9d1: 8b45f8           mov      eax, dword ptr [ebp - 8]
0048b9d4: e8db1efcff       call     0x44d8b4
0048b9d9: 8b4014           mov      eax, dword ptr [eax + 0x14]
0048b9dc: 8b55dc           mov      edx, dword ptr [ebp - 0x24]
0048b9df: e8dcc1f8ff       call     0x417bc0
0048b9e4: 8d8d78ffffff     lea      ecx, [ebp - 0x88]
0048b9ea: a1b83f4a00       mov      eax, dword ptr [0x4a3fb8]
0048b9ef: 8b00             mov      eax, dword ptr [eax]
0048b9f1: 8b4018           mov      eax, dword ptr [eax + 0x18]
0048b9f4: 8b55e4           mov      edx, dword ptr [ebp - 0x1c]
0048b9f7: 8b18             mov      ebx, dword ptr [eax]
0048b9f9: ff530c           call     dword ptr [ebx + 0xc]
0048b9fc: 8b8578ffffff     mov      eax, dword ptr [ebp - 0x88]
0048ba02: 50               push     eax
0048ba03: 8b45f8           mov      eax, dword ptr [ebp - 8]
0048ba06: e8a91efcff       call     0x44d8b4
0048ba0b: 8b15b83f4a00     mov      edx, dword ptr [0x4a3fb8]
0048ba11: 8b12             mov      edx, dword ptr [edx]
0048ba13: 8b4de4           mov      ecx, dword ptr [ebp - 0x1c]
0048ba16: 2b4a20           sub      ecx, dword ptr [edx + 0x20]
0048ba19: c1e102           shl      ecx, 2
0048ba1c: 8d0c49           lea      ecx, [ecx + ecx*2]
0048ba1f: 034de8           add      ecx, dword ptr [ebp - 0x18]
0048ba22: 8b55ec           mov      edx, dword ptr [ebp - 0x14]
0048ba25: e8b2c6f8ff       call     0x4180dc
0048ba2a: ff45e4           inc      dword ptr [ebp - 0x1c]
0048ba2d: ff4dcc           dec      dword ptr [ebp - 0x34]
0048ba30: 0f8545ffffff     jne      0x48b97b
0048ba36: 8b45f8           mov      eax, dword ptr [ebp - 8]
0048ba39: e8761efcff       call     0x44d8b4
0048ba3e: e8c116fcff       call     0x44d104
0048ba43: 33c0             xor      eax, eax
0048ba45: 5a               pop      edx
0048ba46: 59               pop      ecx
0048ba47: 59               pop      ecx
0048ba48: 648910           mov      dword ptr fs:[eax], edx
0048ba4b: 686bba4800       push     0x48ba6b
0048ba50: 8d8578ffffff     lea      eax, [ebp - 0x88]
0048ba56: e8fd7ff7ff       call     0x403a58
0048ba5b: 8d45b4           lea      eax, [ebp - 0x4c]
0048ba5e: e8f57ff7ff       call     0x403a58
0048ba63: c3               ret      