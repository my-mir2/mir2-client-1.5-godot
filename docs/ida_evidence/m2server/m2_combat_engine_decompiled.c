// ==================== [0x4A0218] sub_4A0218 (size: 1745) ====================
int __userpurge sub_4A0218@<eax>(
        Classes::TList *a1@<eax>,
        _BYTE *a2@<edx>,
        _BYTE *a3@<ecx>,
        _BYTE *a4,
        _BYTE *a5,
        int a6)
{
  int v6; // ecx
  int v7; // eax
  char v8; // zf
  void *v9; // eax
  int v10; // edx
  _DWORD *v11; // eax
  int v12; // eax
  int v13; // eax
  int v14; // eax
  int v15; // eax
  int v16; // edx
  int v17; // eax
  unsigned __int8 v18; // al
  int v19; // eax
  unsigned __int8 v20; // al
  int v21; // eax
  unsigned __int8 v22; // al
  unsigned __int8 v23; // al
  int v24; // eax
  unsigned __int8 v25; // al
  int v26; // eax
  unsigned __int8 v27; // al
  _DWORD *v28; // eax
  int v29; // ecx
  int v30; // ecx
  int v31; // eax
  int *v32; // eax
  int v33; // ecx
  int v34; // eax
  int v35; // eax
  int v36; // ebx
  int v37; // eax
  int v38; // ecx
  unsigned int v40[2]; // [esp-Ch] [ebp-FCh] BYREF
  int *v41; // [esp-4h] [ebp-F4h]
  int v42; // [esp+Ch] [ebp-E4h]
  _TBYTE v43; // [esp+10h] [ebp-E0h]
  int v44; // [esp+1Ch] [ebp-D4h] BYREF
  _DWORD v45[4]; // [esp+20h] [ebp-D0h] BYREF
  _BYTE v46[24]; // [esp+30h] [ebp-C0h] BYREF
  _BYTE v47[20]; // [esp+48h] [ebp-A8h] BYREF
  int v48; // [esp+5Ch] [ebp-94h] BYREF
  int v49; // [esp+60h] [ebp-90h] BYREF
  int v50; // [esp+64h] [ebp-8Ch]
  int v51; // [esp+68h] [ebp-88h]
  int v52; // [esp+6Ch] [ebp-84h] BYREF
  _WORD v53[22]; // [esp+70h] [ebp-80h] BYREF
  int v54; // [esp+9Ch] [ebp-54h]
  int v55; // [esp+A0h] [ebp-50h]
  System::TObject *v56; // [esp+A4h] [ebp-4Ch]
  int v57; // [esp+A8h] [ebp-48h]
  const void *v58; // [esp+ACh] [ebp-44h]
  int v59; // [esp+B0h] [ebp-40h]
  int v60; // [esp+B4h] [ebp-3Ch]
  int v61; // [esp+B8h] [ebp-38h]
  int v62; // [esp+BCh] [ebp-34h]
  int v63; // [esp+C0h] [ebp-30h]
  int v64; // [esp+C4h] [ebp-2Ch]
  int v65; // [esp+C8h] [ebp-28h]
  int v66; // [esp+CCh] [ebp-24h]
  int v67; // [esp+D0h] [ebp-20h]
  int v68; // [esp+D4h] [ebp-1Ch]
  int v69; // [esp+D8h] [ebp-18h]
  int v70; // [esp+DCh] [ebp-14h]
  int v71; // [esp+E0h] [ebp-10h]
  _BYTE *v72; // [esp+E4h] [ebp-Ch]
  _BYTE *v73; // [esp+E8h] [ebp-8h]
  Classes::TList *v74; // [esp+ECh] [ebp-4h]
  int savedregs; // [esp+F0h] [ebp+0h] BYREF

  v49 = 0;
  v48 = 0;
  memset(v45, 0, sizeof(v45));
  v44 = 0;
  v50 = 0;
  v52 = 0;
  v72 = a3;
  v73 = a2;
  v74 = a1;
  v41 = &savedregs;
  v40[1] = (unsigned int)&loc_4A08D9;
  v40[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v40);
  v66 = 0;
  v65 = 0;
  v64 = 0;
  v63 = 0;
  v62 = 0;
  v61 = 0;
  v60 = 0;
  v59 = 0;
  v57 = 0;
  LOBYTE(a2) = 1;
  v56 = (System::TObject *)unknown_libname_22(off_40FAFC, a2);
  if ( *((_DWORD *)v74 + 2) - 1 >= 0 )
  {
    v71 = *((_DWORD *)v74 + 2) - 1;
    do
    {
      v7 = Classes::TList::Get(v74, v71);
      sub_4AC1AC(*off_4EA490, *(unsigned __int16 *)(v7 + 4), &v52);
      System::__linkproc__ LStrCmp(v52, *off_4EA028);
      if ( v8 )
      {
        v51 = *(unsigned __int16 *)(Classes::TList::Get(v74, v71) + 6);
        v9 = (void *)System::__linkproc__ ROUND((double)v51 / flt_4A08EC);
        Classes::TList::Add(v56, v9);
        if ( v57 == 0 )
        {
          LOBYTE(v10) = 1;
          v57 = unknown_libname_22(off_410028, v10);
        }
        v11 = (_DWORD *)Classes::TList::Get(v74, v71);
        (*(void (__fastcall **)(int, _DWORD, _DWORD))(*(_DWORD *)v57 + 56))(v57, *off_4EA028, *v11);
        v12 = Classes::TList::Get(v74, v71);
        System::__linkproc__ FreeMem(v12, 24);
        Classes::TList::Delete(v74, v71);
      }
      else
      {
        v13 = Classes::TList::Get(v74, v71);
        if ( (unsigned __int8)sub_4B2E94(*(unsigned __int16 *)(v13 + 4)) != 0 )
        {
          v14 = Classes::TList::Get(v74, v71);
          v58 = (const void *)sub_4AC2F8(*off_4EA490, *(unsigned __int16 *)(v14 + 4));
          if ( v58 != nullptr )
          {
            qmemcpy(v53, v58, sizeof(v53));
            v15 = Classes::TList::Get(v74, v71);
            sub_495974(*off_4EA324, v15, v53);
            v69 = 0;
            v68 = 0;
            v67 = 0;
            v17 = (unsigned __int8)(HIBYTE(v53[7]) - 19);
            if ( (unsigned __int8)v17 < 3u || (unsigned __int8)(HIBYTE(v53[7]) - 22) < 2u )
            {
              LOWORD(v17) = v53[15];
              v18 = sub_407B28(v17);
              v19 = LOBYTE(v53[15]) + v18;
              v69 = v19;
              LOWORD(v19) = v53[17];
              v20 = sub_407B28(v19);
              v21 = LOBYTE(v53[17]) + v20;
              v68 = v21;
              LOWORD(v21) = v53[16];
              v22 = sub_407B28(v21);
              v16 = LOBYTE(v53[16]);
              v67 = LOBYTE(v53[16]) + v22;
            }
            else if ( HIBYTE(v53[7]) == 24 || HIBYTE(v53[7]) == 26 )
            {
              LOWORD(v17) = v53[15];
              v23 = sub_407B28(v17);
              v24 = LOBYTE(v53[15]) + v23 + 1;
              v69 = v24;
              LOWORD(v24) = v53[17];
              v25 = sub_407B28(v24);
              v26 = LOBYTE(v53[17]) + v25 + 1;
              v68 = v26;
              LOWORD(v26) = v53[16];
              v27 = sub_407B28(v26);
              v16 = LOBYTE(v53[16]);
              v67 = LOBYTE(v53[16]) + v27 + 1;
            }
            if ( v66 >= v69 )
            {
              if ( v65 < v69 )
                v65 = v69;
            }
            else
            {
              v65 = v66;
              v66 = v69;
            }
            if ( v64 >= v68 )
            {
              if ( v63 < v68 )
                v63 = v68;
            }
            else
            {
              v63 = v64;
              v64 = v68;
            }
            if ( v62 >= v67 )
            {
              if ( v61 < v67 )
                v61 = v67;
            }
            else
            {
              v61 = v62;
              v62 = v67;
            }
            if ( v57 == 0 )
            {
              LOBYTE(v16) = 1;
              v57 = unknown_libname_22(off_410028, v16);
            }
            v28 = (_DWORD *)Classes::TList::Get(v74, v71);
            unknown_libname_41(*v28);
            (*(void (__fastcall **)(int, int))(*(_DWORD *)v57 + 56))(v57, v50);
            unknown_libname_16(v47, &dword_4A08F0);
            LOBYTE(v29) = 19;
            System::__linkproc__ PStrNCat(v47, *(_DWORD *)(a6 - 4) + 4, v29);
            unknown_libname_16(v46, v47);
            LOBYTE(v30) = 20;
            System::__linkproc__ PStrNCat(v46, dword_4A08F4, v30);
            unknown_libname_41(v40[0]);
            Sysutils::IntToStr(*(_DWORD *)(*(_DWORD *)(a6 - 4) + 36));
            Sysutils::IntToStr(*(_DWORD *)(*(_DWORD *)(a6 - 4) + 40));
            unknown_libname_41(dword_4A0900);
            v31 = Classes::TList::Get(v74, v71);
            sub_4AC1AC(*off_4EA490, *(unsigned __int16 *)(v31 + 4), v45);
            v32 = (int *)Classes::TList::Get(v74, v71);
            Sysutils::IntToStr(*v32);
            System::__linkproc__ LStrCatN(&v49, 13, v33, dword_4A090C, &dword_4A0918);
            sub_4E42F8(v49);
            v34 = Classes::TList::Get(v74, v71);
            System::__linkproc__ FreeMem(v34, 24);
            Classes::TList::Delete(v74, v71);
          }
        }
      }
      --v71;
    }
    while ( v71 != -1 );
  }
  if ( *((_DWORD *)v56 + 2) - 1 >= 0 )
  {
    v55 = *((_DWORD *)v56 + 2);
    v71 = 0;
    do
    {
      v35 = *((_DWORD *)v56 + 2) - 1;
      if ( v71 + 1 <= v35 )
      {
        v54 = v71 - v35;
        v70 = v35;
        do
        {
          v36 = Classes::TList::Get(v56, v70);
          if ( v36 > Classes::TList::Get(v56, v70 - 1) )
            Classes::TList::Exchange(v56, v70, v70 - 1);
          --v70;
          ++v54;
        }
        while ( v54 != 0 );
      }
      ++v71;
      --v55;
    }
    while ( v55 != 0 );
  }
  if ( *((_DWORD *)v56 + 2) - 1 >= 0 )
  {
    v55 = *((_DWORD *)v56 + 2);
    v71 = 0;
    do
    {
      v37 = Classes::TList::Get(v56, v71);
      v60 += v37;
      if ( ++v59 >= 5 )
        break;
      ++v71;
      --v55;
    }
    while ( v55 != 0 );
  }
  v51 = Ibutils::Min((Ibutils *)5, v59, v6);
  *(double *)&v43 = (double)v51;
  v42 = Ibutils::Min((Ibutils *)5, v59, v38);
  *a4 = System::__linkproc__ ROUND((double)v42 * ((double)v60 / (double)v59 / flt_4A091C) + *(double *)&v43);
  *v73 = v65 / 3 + v66 + v66 / 5;
  *v72 = v63 / 3 + v64 + v64 / 5;
  *a5 = v61 / 3 + v62 + v62 / 5;
  if ( v57 != 0 )
    sub_4B865C(0, 0, 0, v57, 0);
  if ( v56 != nullptr )
    System::TObject::Free(v56);
  __writefsdword(0, v40[0]);
  v41 = (int *)&loc_4A08E0;
  unknown_libname_35(&v44, 5);
  unknown_libname_35(&v48, 3);
  return unknown_libname_34(&v52);
}


// ==================== [0x4A0920] sub_4A0920 (size: 794) ====================
int __usercall sub_4A0920@<eax>(int a1@<eax>, int a2@<edx>, double a3@<st0>)
{
  int v3; // eax
  char v4; // zf
  int v5; // ecx
  int v6; // ecx
  int v7; // ecx
  int v8; // ecx
  int v9; // edx
  int v10; // ecx
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-Ch] [ebp-78h] BYREF
  void *v13; // [esp-8h] [ebp-74h]
  int *v14; // [esp-4h] [ebp-70h]
  int v15; // [esp+Ch] [ebp-60h] BYREF
  _DWORD v16[4]; // [esp+10h] [ebp-5Ch] BYREF
  _BYTE v17[24]; // [esp+20h] [ebp-4Ch] BYREF
  _BYTE v18[20]; // [esp+38h] [ebp-34h] BYREF
  int v19; // [esp+4Ch] [ebp-20h] BYREF
  int v20; // [esp+50h] [ebp-1Ch] BYREF
  int v21; // [esp+54h] [ebp-18h]
  void *Mem; // [esp+58h] [ebp-14h]
  char v23; // [esp+5Fh] [ebp-Dh]
  int v24; // [esp+60h] [ebp-Ch]
  int v25; // [esp+64h] [ebp-8h]
  int v26; // [esp+68h] [ebp-4h]
  int savedregs; // [esp+6Ch] [ebp+0h] BYREF

  v20 = 0;
  v19 = 0;
  memset(v16, 0, sizeof(v16));
  v15 = 0;
  v26 = a2;
  v25 = a1;
  v14 = &savedregs;
  v13 = &loc_4A0C2C;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  v23 = 0;
  v3 = *(_DWORD *)(*(_DWORD *)(a1 + 1424) + 8) - 1;
  if ( v3 < 0 )
  {
LABEL_6:
    if ( *(_WORD *)(v26 + 1084) != 0 && *(int *)(v26 + 48) >= 10000 && sub_4C4AB0(v26, *off_4EA028) != 0 )
    {
      sub_4BF6A8(v26, 10000);
      if ( *(_BYTE *)(v25 + 1397) != 0 )
        sub_4904C4(*off_4EA080[0], 10000);
      sub_4C49F4(v26);
      Mem = (void *)System::__linkproc__ GetMem(64);
      LOBYTE(v5) = 14;
      System::__linkproc__ PStrNCpy(Mem, v26 + 21, v5);
      qmemcpy((char *)Mem + 16, (const void *)(v26 + 1080), 0x18u);
      unknown_libname_16(v18, &dword_4A0C58);
      LOBYTE(v6) = 19;
      System::__linkproc__ PStrNCat(v18, v26 + 4, v6);
      unknown_libname_16(v17, v18);
      LOBYTE(v7) = 20;
      System::__linkproc__ PStrNCat(v17, dword_4A0C5C, v7);
      unknown_libname_41(ExceptionList);
      Sysutils::IntToStr(*(_DWORD *)(v26 + 36));
      Sysutils::IntToStr(*(_DWORD *)(v26 + 40));
      unknown_libname_41(dword_4A0C68);
      sub_4AC1AC(*off_4EA490, *(unsigned __int16 *)(v26 + 1084), v16);
      Sysutils::IntToStr(*(_DWORD *)(v26 + 1080));
      System::__linkproc__ LStrCatN(&v20, 13, v8, dword_4A0C74, dword_4A0C80);
      sub_4E42F8(v20);
      sub_4D0BDC(v26, v26 + 1080);
      *(_WORD *)(v26 + 1084) = 0;
      unknown_libname_25(v26, v9, v10);
      sub_4BA58C(v26);
      sub_4B865C(0, 0, 0, 0, 0);
      sub_4A0218(
        *(Classes::TList **)(v26 + 1036),
        (_BYTE *)Mem + 40,
        (_BYTE *)Mem + 41,
        (_BYTE *)Mem + 43,
        (_BYTE *)Mem + 42,
        (int)&savedregs);
      Sysutils::Now();
      *((double *)Mem + 6) = a3;
      *((_DWORD *)Mem + 14) = GetTickCount();
      Classes::TList::Add(*(Classes::TList **)(v25 + 1424), Mem);
      sub_49FF84(v25);
      v23 = 1;
    }
    if ( v23 != 0 )
      sub_49E584(v25, v26, aUpgradenowOk);
    else
      sub_49E584(v25, v26, &str___upgradenow_fa[1]);
  }
  else
  {
    v21 = v3 + 1;
    v24 = 0;
    while ( 1 )
    {
      Classes::TList::Get(*(Classes::TList **)(v25 + 1424), v24);
      System::__linkproc__ AStrCmp(ExceptionList, v13, v14);
      if ( v4 )
        break;
      ++v24;
      if ( --v21 == 0 )
        goto LABEL_6;
    }
    sub_49E584(v25, v26, &str___upgradenow_in[1]);
  }
  __writefsdword(0, (unsigned int)ExceptionList);
  v14 = (int *)&loc_4A0C33;
  unknown_libname_35(&v15, 5);
  return unknown_libname_35(&v19, 2);
}


// ==================== [0x4A0CB8] sub_4A0CB8 (size: 1546) ====================
int __fastcall sub_4A0CB8(int a1, int a2)
{
  int v2; // ecx
  char v3; // zf
  unsigned __int8 v4; // al
  bool v5; // cf
  unsigned __int8 v6; // al
  char v7; // al
  unsigned int v8; // eax
  unsigned int v9; // eax
  unsigned int v10; // eax
  int v11; // eax
  int v12; // ecx
  int v13; // eax
  int v14; // ecx
  int v15; // eax
  int v16; // ecx
  int v17; // ecx
  int v18; // ecx
  int v19; // ecx
  int v21; // [esp-10h] [ebp-88h]
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-Ch] [ebp-84h] BYREF
  void *v23; // [esp-8h] [ebp-80h]
  int *v24; // [esp-4h] [ebp-7Ch]
  int v25; // [esp+8h] [ebp-70h] BYREF
  _DWORD v26[4]; // [esp+Ch] [ebp-6Ch] BYREF
  _BYTE v27[24]; // [esp+1Ch] [ebp-5Ch] BYREF
  _BYTE v28[20]; // [esp+34h] [ebp-44h] BYREF
  int v29; // [esp+48h] [ebp-30h] BYREF
  int v30; // [esp+4Ch] [ebp-2Ch] BYREF
  int v31; // [esp+50h] [ebp-28h]
  unsigned __int16 *Mem; // [esp+54h] [ebp-24h]
  int v33; // [esp+58h] [ebp-20h]
  int v34; // [esp+5Ch] [ebp-1Ch]
  int v35; // [esp+60h] [ebp-18h]
  int v36; // [esp+64h] [ebp-14h]
  int v37; // [esp+68h] [ebp-10h]
  int v38; // [esp+6Ch] [ebp-Ch]
  int v39; // [esp+70h] [ebp-8h]
  int v40; // [esp+74h] [ebp-4h]
  int savedregs; // [esp+78h] [ebp+0h] BYREF

  v30 = 0;
  v29 = 0;
  memset(v26, 0, sizeof(v26));
  v25 = 0;
  v39 = a2;
  v40 = a1;
  v24 = &savedregs;
  v23 = &loc_4A12B5;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  v35 = 0;
  v33 = 0;
  if ( (unsigned __int8)sub_4C4D90(a2) != 0 )
  {
    if ( *(_DWORD *)(*(_DWORD *)(v40 + 1424) + 8) - 1 >= 0 )
    {
      v31 = *(_DWORD *)(*(_DWORD *)(v40 + 1424) + 8);
      v38 = 0;
      while ( 1 )
      {
        Classes::TList::Get(*(Classes::TList **)(v40 + 1424), v38);
        System::__linkproc__ AStrCmp(ExceptionList, v23, v24);
        if ( v3 )
        {
          v35 = 1;
          v21 = *(_DWORD *)(Classes::TList::Get(*(Classes::TList **)(v40 + 1424), v38) + 56);
          if ( GetTickCount() - v21 > 0x1B7740 || *(_BYTE *)(v39 + 621) >= 3u )
            break;
        }
        ++v38;
        if ( --v31 == 0 )
          goto LABEL_9;
      }
      v33 = Classes::TList::Get(*(Classes::TList **)(v40 + 1424), v38);
      Classes::TList::Delete(*(Classes::TList **)(v40 + 1424), v38);
      sub_49FF84(v40);
      v35 = 2;
    }
LABEL_9:
    if ( v33 != 0 )
    {
      v4 = *(_BYTE *)(v33 + 43);
      v5 = v4 < 9u;
      v6 = v4 - 9;
      if ( v5 )
      {
        v36 = Ibutils::Max((Ibutils *)0xBB8, *(unsigned __int16 *)(v33 + 24) >> 1, v2);
        if ( *(_WORD *)(v33 + 24) <= 0xBB8u )
          *(_WORD *)(v33 + 24) >>= 1;
        else
          *(_WORD *)(v33 + 24) -= 3000;
        if ( *(_WORD *)(v33 + 22) > *(_WORD *)(v33 + 24) )
          *(_WORD *)(v33 + 22) = *(_WORD *)(v33 + 24);
      }
      else
      {
        v5 = v6 < 7u;
        v7 = v6 - 7;
        if ( v5 )
        {
          if ( System::__linkproc__ RandInt(*(unsigned __int8 *)(v33 + 43)) < 6 )
            *(_WORD *)(v33 + 24) -= 1000;
        }
        else if ( (unsigned __int8)(v7 - 2) < 0xEEu )
        {
          v8 = System::__linkproc__ RandInt(*(unsigned __int8 *)(v33 + 43) - 18) - 1;
          v5 = v8 < 4;
          v9 = v8 - 4;
          if ( v5 )
          {
            *(_WORD *)(v33 + 24) += 1000;
          }
          else
          {
            v5 = v9 < 3;
            v10 = v9 - 3;
            if ( v5 )
            {
              *(_WORD *)(v33 + 24) += 2000;
            }
            else if ( v10 < 0xF8 )
            {
              *(_WORD *)(v33 + 24) += 4000;
            }
          }
        }
      }
      if ( *(_BYTE *)(v33 + 40) == *(_BYTE *)(v33 + 42) && *(_BYTE *)(v33 + 42) == *(_BYTE *)(v33 + 41) )
        v34 = System::__linkproc__ RandInt(3);
      else
        v34 = -1;
      if ( *(_BYTE *)(v33 + 40) >= *(_BYTE *)(v33 + 42) && *(_BYTE *)(v33 + 40) >= *(_BYTE *)(v33 + 41) || v34 == 0 )
      {
        v11 = Ibutils::Min((Ibutils *)0xB, *(unsigned __int8 *)(v33 + 40), v2);
        v37 = Ibutils::Min(
                (Ibutils *)0x55,
                *(_DWORD *)(v39 + 264) + *(unsigned __int8 *)(v33 + 29) + 7 * v11 + 10 - *(unsigned __int8 *)(v33 + 30),
                v12);
        if ( System::__linkproc__ RandInt(100) >= v37 )
        {
          *(_BYTE *)(v33 + 36) = 1;
        }
        else
        {
          *(_BYTE *)(v33 + 36) = 10;
          if ( v37 > 63 && System::__linkproc__ RandInt(30) == 0 )
            *(_BYTE *)(v33 + 36) = 11;
          if ( v37 > 79 && System::__linkproc__ RandInt(200) == 0 )
            *(_BYTE *)(v33 + 36) = 12;
        }
      }
      if ( *(_BYTE *)(v33 + 42) >= *(_BYTE *)(v33 + 40) && *(_BYTE *)(v33 + 42) >= *(_BYTE *)(v33 + 41) || v34 == 1 )
      {
        v13 = Ibutils::Min((Ibutils *)0xB, *(unsigned __int8 *)(v33 + 42), v2);
        v37 = Ibutils::Min(
                (Ibutils *)0x55,
                *(_DWORD *)(v39 + 264) + *(unsigned __int8 *)(v33 + 29) + 7 * v13 + 10 - *(unsigned __int8 *)(v33 + 30),
                v14);
        if ( System::__linkproc__ RandInt(100) >= v37 )
        {
          *(_BYTE *)(v33 + 36) = 1;
        }
        else
        {
          *(_BYTE *)(v33 + 36) = 20;
          if ( v37 > 63 && System::__linkproc__ RandInt(30) == 0 )
            *(_BYTE *)(v33 + 36) = 21;
          if ( v37 > 79 && System::__linkproc__ RandInt(200) == 0 )
            *(_BYTE *)(v33 + 36) = 22;
        }
      }
      if ( *(_BYTE *)(v33 + 41) >= *(_BYTE *)(v33 + 42) && *(_BYTE *)(v33 + 41) >= *(_BYTE *)(v33 + 40) || v34 == 2 )
      {
        v15 = Ibutils::Min((Ibutils *)0xB, *(unsigned __int8 *)(v33 + 41), v2);
        v37 = Ibutils::Min(
                (Ibutils *)0x55,
                *(_DWORD *)(v39 + 264) + *(unsigned __int8 *)(v33 + 29) + 7 * v15 + 10 - *(unsigned __int8 *)(v33 + 30),
                v16);
        if ( System::__linkproc__ RandInt(100) >= v37 )
        {
          *(_BYTE *)(v33 + 36) = 1;
        }
        else
        {
          *(_BYTE *)(v33 + 36) = 30;
          if ( v37 > 63 && System::__linkproc__ RandInt(30) == 0 )
            *(_BYTE *)(v33 + 36) = 31;
          if ( v37 > 79 && System::__linkproc__ RandInt(200) == 0 )
            *(_BYTE *)(v33 + 36) = 32;
        }
      }
      Mem = (unsigned __int16 *)System::__linkproc__ GetMem(24);
      qmemcpy(Mem, (const void *)(v33 + 16), 0x18u);
      System::__linkproc__ FreeMem(v33, 64);
      unknown_libname_16(v28, &dword_4A12C4);
      LOBYTE(v17) = 19;
      System::__linkproc__ PStrNCat(v28, v39 + 4, v17);
      unknown_libname_16(v27, v28);
      LOBYTE(v18) = 20;
      System::__linkproc__ PStrNCat(v27, dword_4A12C8, v18);
      unknown_libname_41(ExceptionList);
      Sysutils::IntToStr(*(_DWORD *)(v39 + 36));
      Sysutils::IntToStr(*(_DWORD *)(v39 + 40));
      unknown_libname_41(dword_4A12D4);
      sub_4AC1AC(*off_4EA490, Mem[2], v26);
      Sysutils::IntToStr(*(_DWORD *)Mem);
      System::__linkproc__ LStrCatN(&v30, 13, v19, dword_4A12E0, dword_4A12EC);
      sub_4E42F8(v30);
      sub_4C4DB8(v39, Mem);
      sub_4D0824(v39, Mem);
    }
    if ( v35 != 0 )
    {
      if ( v35 == 1 )
      {
        sub_49E584(v40, v39, &str___getbackupgnow[1]);
      }
      else if ( v35 == 2 )
      {
        sub_49E584(v40, v39, aGetbackupgnowO);
      }
    }
    else
    {
      sub_49E584(v40, v39, &str___getbackupgnow_0[1]);
    }
  }
  else
  {
    sub_4BB124(v39, &str______________[1], 0);
    sub_49E584(v40, v39, &str__exit[1]);
  }
  __writefsdword(0, (unsigned int)ExceptionList);
  v24 = (int *)&loc_4A12BC;
  unknown_libname_35(&v25, 5);
  return unknown_libname_35(&v29, 2);
}


// ==================== [0x4A1378] sub_4A1378 (size: 446) ====================
int __usercall sub_4A1378@<eax>(int a1@<eax>, int a2@<edx>, int a3)
{
  int v3; // edx
  bool v4; // cc
  int v5; // edx
  int v6; // ecx
  int v7; // eax
  int v8; // ecx
  unsigned int v10[2]; // [esp-Ch] [ebp-5Ch] BYREF
  int *v11; // [esp-4h] [ebp-54h]
  _DWORD v12[4]; // [esp+0h] [ebp-50h] BYREF
  char v13[15]; // [esp+10h] [ebp-40h] BYREF
  _BYTE v14[5]; // [esp+1Fh] [ebp-31h]
  int v15; // [esp+24h] [ebp-2Ch]
  int v16; // [esp+2Ch] [ebp-24h]
  int v17; // [esp+30h] [ebp-20h] BYREF
  int v18; // [esp+34h] [ebp-1Ch]
  int v19; // [esp+38h] [ebp-18h]
  Classes::TList *v20; // [esp+3Ch] [ebp-14h]
  int v21; // [esp+40h] [ebp-10h]
  int v22; // [esp+44h] [ebp-Ch]
  int v23; // [esp+48h] [ebp-8h]
  int v24; // [esp+4Ch] [ebp-4h]
  int savedregs; // [esp+50h] [ebp+0h] BYREF

  memset(v12, 0, sizeof(v12));
  v17 = 0;
  v23 = a2;
  v24 = a1;
  v11 = &savedregs;
  v10[1] = (unsigned int)&loc_4A152B;
  v10[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v10);
  unknown_libname_34(&v17);
  v21 = 0;
  v3 = *(_DWORD *)(*(_DWORD *)(*(_DWORD *)(a3 - 4) + 1416) + 8) - 1;
  v4 = v3 < v23;
  v5 = v3 - v23;
  if ( !v4 )
  {
    v16 = v5 + 1;
    v22 = v23;
    do
    {
      v20 = (Classes::TList *)Classes::TList::Get(*(Classes::TList **)(*(_DWORD *)(a3 - 4) + 1416), v22);
      v18 = Classes::TList::Get(v20, 0);
      v19 = sub_4AC2F8(*off_4EA490, *(unsigned __int16 *)(v18 + 4));
      if ( v19 != 0 )
      {
        LOBYTE(v6) = 14;
        System::__linkproc__ PStrNCpy(v13, v19, v6);
        v7 = sub_49F374(*(_DWORD *)(a3 - 4), *(unsigned __int16 *)(v18 + 4));
        *(_DWORD *)&v14[1] = sub_49F6E0(*(_DWORD *)(a3 - 4), v24, v7);
        v15 = *((_DWORD *)v20 + 2);
        v14[0] = *(_BYTE *)(v19 + 15) > 4u && *(_BYTE *)(v19 + 15) != 42 && *(_BYTE *)(v19 + 15) != 31;
        unknown_libname_41(v17);
        Sysutils::IntToStr(v14[0]);
        Sysutils::IntToStr(*(int *)&v14[1]);
        Sysutils::IntToStr(v15);
        System::__linkproc__ LStrCatN(&v17, 9, v8, v12[0], &str___56[1]);
        ++v21;
      }
      ++v22;
      --v16;
    }
    while ( v16 != 0 );
  }
  sub_4B865C(v17, 0, v21, *(_DWORD *)(a3 - 4), 0);
  __writefsdword(0, v10[0]);
  v11 = (int *)&loc_4A1532;
  unknown_libname_35(v12, 4);
  return unknown_libname_34(&v17);
}


// ==================== [0x4A16A0] sub_4A16A0 (size: 371) ====================
int __usercall sub_4A16A0@<eax>(int a1@<eax>, int a2)
{
  int v2; // ecx
  int v3; // ecx
  unsigned int v5[2]; // [esp-Ch] [ebp-54h] BYREF
  int *v6; // [esp-4h] [ebp-4Ch]
  _DWORD v7[4]; // [esp+0h] [ebp-48h] BYREF
  _BYTE v8[15]; // [esp+10h] [ebp-38h] BYREF
  _BYTE v9[5]; // [esp+1Fh] [ebp-29h]
  int v10; // [esp+24h] [ebp-24h]
  int v11; // [esp+2Ch] [ebp-1Ch]
  Classes::TList *v12; // [esp+30h] [ebp-18h]
  int v13; // [esp+34h] [ebp-14h]
  int v14; // [esp+38h] [ebp-10h]
  int v15; // [esp+3Ch] [ebp-Ch] BYREF
  int v16; // [esp+40h] [ebp-8h]
  int v17; // [esp+44h] [ebp-4h]
  int savedregs; // [esp+48h] [ebp+0h] BYREF

  memset(v7, 0, sizeof(v7));
  v15 = 0;
  v17 = a1;
  v6 = &savedregs;
  v5[1] = (unsigned int)&loc_4A1808;
  v5[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v5);
  unknown_libname_34(&v15);
  if ( *(_DWORD *)(*(_DWORD *)(*(_DWORD *)(a2 - 4) + 1416) + 8) - 1 >= 0 )
  {
    v11 = *(_DWORD *)(*(_DWORD *)(*(_DWORD *)(a2 - 4) + 1416) + 8);
    v16 = 0;
    do
    {
      v12 = (Classes::TList *)Classes::TList::Get(*(Classes::TList **)(*(_DWORD *)(a2 - 4) + 1416), v16);
      v14 = Classes::TList::Get(v12, 0);
      v13 = sub_4AC2F8(*off_4EA490, *(unsigned __int16 *)(v14 + 4));
      if ( v13 != 0 )
      {
        LOBYTE(v2) = 14;
        System::__linkproc__ PStrNCpy(v8, v13, v2);
        *(_DWORD *)&v9[1] = 100;
        v10 = 1;
        v9[0] = 0;
        unknown_libname_41(v15);
        Sysutils::IntToStr(v9[0]);
        Sysutils::IntToStr(*(int *)&v9[1]);
        Sysutils::IntToStr(v10);
        System::__linkproc__ LStrCatN(&v15, 9, v3, v7[0], &str___57[1]);
      }
      ++v16;
      --v11;
    }
    while ( v11 != 0 );
  }
  if ( v15 != 0 )
    sub_4B865C(v15, 0, 0, *(_DWORD *)(a2 - 4), 0);
  __writefsdword(0, v5[0]);
  v6 = (int *)&loc_4A180F;
  unknown_libname_35(v7, 4);
  return unknown_libname_34(&v15);
}


// ==================== [0x4A1820] sub_4A1820 (size: 613) ====================
int __usercall sub_4A1820@<eax>(int a1@<eax>, int a2@<edx>, _BYTE *a3@<ecx>, double a4@<st0>)
{
  unsigned int v5[3]; // [esp-18h] [ebp-40h] BYREF
  unsigned int v6[2]; // [esp-Ch] [ebp-34h] BYREF
  int *v7; // [esp-4h] [ebp-2Ch]
  char v8; // [esp+Ch] [ebp-1Ch]
  int v9; // [esp+10h] [ebp-18h] BYREF
  int v10; // [esp+14h] [ebp-14h] BYREF
  int v11; // [esp+18h] [ebp-10h]
  _BYTE *v12; // [esp+1Ch] [ebp-Ch]
  int v13; // [esp+20h] [ebp-8h]
  int v14; // [esp+24h] [ebp-4h]
  int savedregs; // [esp+28h] [ebp+0h] BYREF

  v11 = 0;
  v10 = 0;
  v9 = 0;
  v12 = a3;
  v13 = a2;
  v14 = a1;
  unknown_libname_44(a3);
  v7 = &savedregs;
  v6[1] = (unsigned int)&loc_4A1A77;
  v6[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v6);
  v5[2] = (unsigned int)&savedregs;
  v5[1] = (unsigned int)&loc_4A1A48;
  v5[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v5);
  if ( (*(_BYTE *)(v14 + 1397) == 0 || *(_BYTE *)(*off_4EA080[0] + 65) == 0)
    && *(_BYTE *)(v13 + 772) == 0
    && v12 != nullptr
    && *v12 == 64 )
  {
    v8 = 13;
    sub_453DBC(&v10, 0);
    unknown_libname_34(&v9);
    unknown_libname_36(v13 + 812, v12);
    if ( unknown_libname_84(v11, &str__s_repair[1]) != 0 )
    {
      sub_49E584(v14, v13, v11);
      if ( unknown_libname_84(v11, &str__buy[1]) != 0 )
      {
        if ( unknown_libname_84(v11, &str__sell[1]) != 0 )
        {
          if ( unknown_libname_84(v11, &str__repair[1]) != 0 )
          {
            if ( unknown_libname_84(v11, &str__makedrug[1]) != 0 )
            {
              if ( unknown_libname_84(v11, &str__prices[1]) != 0 )
              {
                if ( unknown_libname_84(v11, &str__storage[1]) != 0 )
                {
                  if ( unknown_libname_84(v11, &str__getback[1]) != 0 )
                  {
                    if ( unknown_libname_84(v11, &str__upgradenow[1]) != 0 )
                    {
                      if ( unknown_libname_84(v11, &str__getbackupgnow[1]) != 0 )
                      {
                        if ( unknown_libname_84(v11, &str__exit_0[1]) == 0 )
                          sub_4B865C(0, 0, 0, v14, 0);
                      }
                      else
                      {
                        sub_4A0CB8(v14, v13);
                      }
                    }
                    else
                    {
                      sub_4A0920(v14, v13, a4);
                    }
                  }
                  else
                  {
                    sub_4A1674(&savedregs);
                  }
                }
                else
                {
                  sub_4A1648(&savedregs);
                }
              }
            }
            else
            {
              sub_4A16A0(v13, (int)&savedregs);
            }
          }
          else
          {
            sub_4A1570(&savedregs);
          }
        }
        else
        {
          sub_4A1544(&savedregs);
        }
      }
      else
      {
        sub_4A1378(v13, 0, (int)&savedregs);
      }
    }
    else
    {
      sub_4A159C(&savedregs);
    }
  }
  __writefsdword(0, v5[0]);
  __writefsdword(0, v6[0]);
  v7 = (int *)&loc_4A1A7E;
  return unknown_libname_35(&v9, 4);
}


// ==================== [0x4A1CD8] sub_4A1CD8 (size: 565) ====================
char __fastcall sub_4A1CD8(int a1, int *a2, const void *a3)
{
  int v3; // eax
  int v4; // ecx
  int v5; // ecx
  int v6; // ecx
  int v8; // [esp-3Ch] [ebp-C0h]
  unsigned int v9[2]; // [esp-Ch] [ebp-90h] BYREF
  int *v10; // [esp-4h] [ebp-88h]
  _DWORD v11[2]; // [esp+Ch] [ebp-78h] BYREF
  _DWORD v12[4]; // [esp+14h] [ebp-70h] BYREF
  _BYTE v13[24]; // [esp+24h] [ebp-60h] BYREF
  _BYTE v14[20]; // [esp+3Ch] [ebp-48h] BYREF
  int v15; // [esp+50h] [ebp-34h] BYREF
  int v16; // [esp+54h] [ebp-30h] BYREF
  int v17[6]; // [esp+5Ch] [ebp-28h] BYREF
  int v18; // [esp+74h] [ebp-10h]
  char v19; // [esp+7Bh] [ebp-9h]
  int *v20; // [esp+7Ch] [ebp-8h]
  int v21; // [esp+80h] [ebp-4h]
  int savedregs; // [esp+84h] [ebp+0h] BYREF

  v16 = 0;
  v15 = 0;
  memset(v12, 0, sizeof(v12));
  v11[1] = 0;
  v11[0] = 0;
  qmemcpy(v17, a3, sizeof(v17));
  v20 = a2;
  v21 = a1;
  v10 = &savedregs;
  v9[1] = (unsigned int)&loc_4A1EFC;
  v9[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v9);
  v19 = 0;
  v3 = sub_49F428(a1, v17);
  v18 = sub_49F7A4(v21, v3);
  if ( v18 >= 0
    && *(_BYTE *)(v21 + 1396) == 0
    && (unsigned __int8)sub_4A1C84(&savedregs) != 0
    && (unsigned __int8)sub_4BF64C(v20, v18) != 0 )
  {
    if ( *(_BYTE *)(v21 + 1397) != 0 )
      sub_4904C4(*off_4EA080[0], v18);
    sub_4B865C(0, 0, 0, v20[12], 0);
    sub_4A1BF8(v21, v17);
    unknown_libname_16(v14, &dword_4A1F10);
    LOBYTE(v4) = 19;
    System::__linkproc__ PStrNCat(v14, v20 + 1, v4);
    unknown_libname_16(v13, v14);
    LOBYTE(v5) = 20;
    System::__linkproc__ PStrNCat(v13, dword_4A1F14, v5);
    unknown_libname_41(v9[0]);
    Sysutils::IntToStr(v20[9]);
    Sysutils::IntToStr(v20[10]);
    unknown_libname_41(dword_4A1F20);
    sub_4AC1AC(*off_4EA490, LOWORD(v17[1]), v12);
    Sysutils::IntToStr(v17[0]);
    unknown_libname_41(&dword_4A1F2C);
    System::__linkproc__ LStrCatN(&v16, 13, v6, v8, v11[0]);
    sub_4E42F8(v16);
    v19 = 1;
  }
  else
  {
    sub_4B865C(0, 0, 0, 0, 0);
  }
  __writefsdword(0, v9[0]);
  v10 = (int *)&loc_4A1F03;
  unknown_libname_35(v11, 6);
  unknown_libname_35(&v15, 2);
  return v19;
}


// ==================== [0x4A2024] sub_4A2024 (size: 591) ====================
char __fastcall sub_4A2024(int a1, int a2, unsigned __int16 *a3)
{
  char v3; // zf
  int v4; // eax
  char v5; // al
  int v7; // [esp+10h] [ebp-1Ch]
  int v8; // [esp+14h] [ebp-18h]
  int v9; // [esp+18h] [ebp-14h]
  char v10; // [esp+1Fh] [ebp-Dh]

  v10 = 0;
  System::__linkproc__ LStrCmp(*(_DWORD *)(a2 + 812), &str__fail_s_repair[1]);
  if ( v3 )
  {
    Webadapt::TCustomAdapterGotoPageAction::ImplGetAdapterItemRequestParams(a1, a2, &str_Sorry__I_cant_s[1]);
    sub_4B865C(0, 0, 0, 0, 0);
  }
  else
  {
    v4 = sub_49F428(a1, a3);
    v9 = sub_49F6E0(a1, a2, v4);
    System::__linkproc__ LStrCmp(*(_DWORD *)(a2 + 812), &str__s_repair_1[1]);
    if ( v3 )
      v9 *= 3;
    v7 = sub_4AC2F8(*off_4EA490, a3[2]);
    if ( v7 != 0 )
    {
      if ( v9 <= 0
        || *(_BYTE *)(v7 + 15) == 43
        || (a3[4] == 0
          ? (v8 = v9, v5 = sub_4BF6A8(a2, v9))
          : (v8 = System::__linkproc__ ROUND((double)(v9 / 3) / (double)a3[4] * (double)(a3[4] - a3[3])),
             v5 = sub_4BF6A8(a2, v8)),
            v5 == 0) )
      {
        sub_4B865C(0, 0, 0, 0, 0);
      }
      else
      {
        if ( *(_BYTE *)(a1 + 1397) != 0 )
          sub_4904C4(*off_4EA080[0], v8);
        System::__linkproc__ LStrCmp(*(_DWORD *)(a2 + 812), &str__s_repair_1[1]);
        if ( v3 )
        {
          a3[3] = a3[4];
          sub_4B865C(0, a3[4], a3[3], *(_DWORD *)(a2 + 48), 0);
          Webadapt::TCustomAdapterGotoPageAction::ImplGetAdapterItemRequestParams(a1, a2, &str_The_item_has_be[1]);
        }
        else
        {
          a3[4] -= (a3[4] - a3[3]) / 30;
          a3[3] = a3[4];
          sub_4B865C(0, a3[4], a3[3], *(_DWORD *)(a2 + 48), 0);
          sub_49E584(a1, a2, &str___repair[1]);
        }
        return 1;
      }
    }
  }
  return v10;
}


// ==================== [0x4A2334] sub_4A2334 (size: 921) ====================
int __fastcall sub_4A2334(int a1, int *a2, int a3, int a4)
{
  char v4; // zf
  int v5; // eax
  int v6; // ecx
  int v7; // ecx
  int v8; // ecx
  int v10; // [esp-3Ch] [ebp-C0h]
  unsigned int v11[2]; // [esp-Ch] [ebp-90h] BYREF
  int *v12; // [esp-4h] [ebp-88h]
  _DWORD v13[2]; // [esp+4h] [ebp-80h] BYREF
  _DWORD v14[4]; // [esp+Ch] [ebp-78h] BYREF
  _BYTE v15[20]; // [esp+1Ch] [ebp-68h] BYREF
  _BYTE v16[20]; // [esp+30h] [ebp-54h] BYREF
  int v17; // [esp+44h] [ebp-40h] BYREF
  int v18; // [esp+48h] [ebp-3Ch] BYREF
  int v19; // [esp+4Ch] [ebp-38h]
  int v20; // [esp+50h] [ebp-34h]
  int v21; // [esp+54h] [ebp-30h]
  char v22; // [esp+5Bh] [ebp-29h]
  int v23; // [esp+5Ch] [ebp-28h]
  int v24; // [esp+60h] [ebp-24h]
  System::TObject *v25; // [esp+64h] [ebp-20h]
  int v26; // [esp+68h] [ebp-1Ch]
  int v27; // [esp+6Ch] [ebp-18h]
  int v28; // [esp+70h] [ebp-14h]
  int v29; // [esp+74h] [ebp-10h]
  int v30; // [esp+78h] [ebp-Ch] BYREF
  int *v31; // [esp+7Ch] [ebp-8h]
  int v32; // [esp+80h] [ebp-4h]
  int savedregs; // [esp+84h] [ebp+0h] BYREF

  v18 = 0;
  v17 = 0;
  memset(v14, 0, sizeof(v14));
  v13[1] = 0;
  v13[0] = 0;
  v19 = 0;
  v30 = a3;
  v31 = a2;
  v32 = a1;
  unknown_libname_44(a3);
  v12 = &savedregs;
  v11[1] = (unsigned int)&loc_4A26BF;
  v11[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v11);
  v22 = 0;
  v26 = 1;
  if ( *(_DWORD *)(*(_DWORD *)(v32 + 1416) + 8) - 1 >= 0 )
  {
    v21 = *(_DWORD *)(*(_DWORD *)(v32 + 1416) + 8);
    v29 = 0;
    do
    {
      if ( v22 != 0 || *(_BYTE *)(v32 + 1396) != 0 )
        break;
      v25 = (System::TObject *)Classes::TList::Get(*(Classes::TList **)(v32 + 1416), v29);
      v23 = Classes::TList::Get(v25, 0);
      v24 = sub_4AC2F8(*off_4EA490, *(unsigned __int16 *)(v23 + 4));
      if ( v24 != 0 )
      {
        if ( (unsigned __int8)sub_4C4A78(v31, *(unsigned __int8 *)(v24 + 17)) != 0 )
        {
          unknown_libname_41(v11[0]);
          System::__linkproc__ LStrCmp(v19, v30);
          if ( v4 && *((_DWORD *)v25 + 2) - 1 >= 0 )
          {
            v20 = *((_DWORD *)v25 + 2);
            v28 = 0;
            while ( 1 )
            {
              v23 = Classes::TList::Get(v25, v28);
              if ( *(_BYTE *)(v24 + 15) <= 4u
                || *(_BYTE *)(v24 + 15) == 42
                || *(_BYTE *)(v24 + 15) == 31
                || *(_DWORD *)v23 == a4 )
              {
                break;
              }
              ++v28;
              if ( --v20 == 0 )
                goto LABEL_28;
            }
            v5 = sub_49F428(v32, v23);
            v27 = sub_49F6E0(v32, v31, v5);
            if ( v31[12] < v27 || v27 <= 0 )
            {
              v26 = 3;
            }
            else if ( (unsigned __int8)sub_4C4DB8(v31, v23) != 0 )
            {
              v31[12] -= v27;
              if ( *(_BYTE *)(v32 + 1397) != 0 )
                sub_4904C4(*off_4EA080[0], v27);
              sub_4D0824(v31, v23);
              unknown_libname_16(v16, &dword_4A26D0);
              LOBYTE(v6) = 18;
              System::__linkproc__ PStrNCat(v16, v31 + 1, v6);
              unknown_libname_16(v15, v16);
              LOBYTE(v7) = 19;
              System::__linkproc__ PStrNCat(v15, dword_4A26D4, v7);
              unknown_libname_41(v11[0]);
              Sysutils::IntToStr(v31[9]);
              Sysutils::IntToStr(v31[10]);
              unknown_libname_41(dword_4A26E0);
              sub_4AC1AC(*off_4EA490, *(unsigned __int16 *)(v23 + 4), v14);
              Sysutils::IntToStr(*(_DWORD *)v23);
              unknown_libname_41(&dword_4A26EC);
              System::__linkproc__ LStrCatN(&v18, 13, v8, v10, v13[0]);
              sub_4E42F8(v18);
              Classes::TList::Delete(v25, v28);
              if ( *((_DWORD *)v25 + 2) == 0 )
              {
                System::TObject::Free(v25);
                Classes::TList::Delete(*(Classes::TList **)(v32 + 1416), v29);
              }
              v26 = 0;
            }
            else
            {
              v26 = 2;
            }
            v22 = 1;
          }
        }
        else
        {
          v26 = 2;
        }
      }
LABEL_28:
      ++v29;
      --v21;
    }
    while ( v21 != 0 );
  }
  if ( v26 != 0 )
    sub_4B865C(0, 0, 0, v26, 0);
  else
    sub_4B865C(0, 0, a4, v31[12], 0);
  __writefsdword(0, v11[0]);
  v12 = (int *)&loc_4A26C6;
  unknown_libname_35(v13, 6);
  unknown_libname_35(&v17, 3);
  return unknown_libname_34(&v30);
}


// ==================== [0x4A26F0] sub_4A26F0 (size: 512) ====================
int __fastcall sub_4A26F0(int a1, int a2, int a3, int a4)
{
  int v4; // ecx
  char v5; // zf
  int v6; // edx
  bool v7; // cc
  int v8; // edx
  int v9; // eax
  int v10; // ecx
  unsigned int v12[2]; // [esp-Ch] [ebp-B0h] BYREF
  int *v13; // [esp-4h] [ebp-A8h]
  int v14; // [esp+Ch] [ebp-98h] BYREF
  int v15; // [esp+10h] [ebp-94h]
  _DWORD v16[12]; // [esp+14h] [ebp-90h] BYREF
  __int16 v17; // [esp+44h] [ebp-60h]
  __int16 v18; // [esp+46h] [ebp-5Eh]
  int v19; // [esp+74h] [ebp-30h]
  int v20; // [esp+78h] [ebp-2Ch]
  int v21; // [esp+7Ch] [ebp-28h]
  const void *v22; // [esp+80h] [ebp-24h]
  Classes::TList *v23; // [esp+84h] [ebp-20h]
  int v24; // [esp+88h] [ebp-1Ch] BYREF
  int v25; // [esp+8Ch] [ebp-18h]
  int v26; // [esp+90h] [ebp-14h]
  int v27; // [esp+94h] [ebp-10h]
  int v28; // [esp+98h] [ebp-Ch] BYREF
  int v29; // [esp+9Ch] [ebp-8h]
  int v30; // [esp+A0h] [ebp-4h]
  int savedregs; // [esp+A4h] [ebp+0h] BYREF

  v14 = 0;
  v15 = 0;
  v24 = 0;
  v28 = a3;
  v29 = a2;
  v30 = a1;
  unknown_libname_44(a3);
  v13 = &savedregs;
  v12[1] = (unsigned int)&loc_4A28E0;
  v12[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v12);
  v25 = 0;
  if ( *(_DWORD *)(*(_DWORD *)(v30 + 1416) + 8) - 1 >= 0 )
  {
    v20 = *(_DWORD *)(*(_DWORD *)(v30 + 1416) + 8);
    v27 = 0;
    while ( 1 )
    {
      v23 = (Classes::TList *)Classes::TList::Get(*(Classes::TList **)(v30 + 1416), v27);
      v21 = Classes::TList::Get(v23, 0);
      v22 = (const void *)sub_4AC2F8(*off_4EA490, *(unsigned __int16 *)(v21 + 4));
      if ( v22 != nullptr )
      {
        unknown_libname_41(v12[0]);
        System::__linkproc__ LStrCmp(v15, v28);
        if ( v5 )
          break;
      }
      ++v27;
      if ( --v20 == 0 )
        goto LABEL_13;
    }
    if ( *((_DWORD *)v23 + 2) - 1 < a4 )
      a4 = Ibutils::Max(nullptr, *((_DWORD *)v23 + 2) - 10, v4);
    v6 = *((_DWORD *)v23 + 2) - 1;
    v7 = v6 < a4;
    v8 = v6 - a4;
    if ( !v7 )
    {
      v19 = v8 + 1;
      v26 = a4;
      do
      {
        v21 = Classes::TList::Get(v23, v26);
        qmemcpy(v16, v22, 0x2Cu);
        v17 = *(_WORD *)(v21 + 6);
        v9 = sub_49F428(v30, v21);
        v18 = sub_49F6E0(v30, v29, v9);
        v16[11] = *(_DWORD *)v21;
        sub_4565B0(v16, 52, &v14, v24);
        System::__linkproc__ LStrCatN(&v24, 3, v10, v14, &str___58[1]);
        if ( ++v25 >= 10 )
          break;
        ++v26;
        --v19;
      }
      while ( v19 != 0 );
    }
  }
LABEL_13:
  sub_4B865C(v24, a4, v25, v30, 0);
  __writefsdword(0, v12[0]);
  v13 = (int *)&loc_4A28E7;
  unknown_libname_35(&v14, 2);
  unknown_libname_34(&v24);
  return unknown_libname_34(&v28);
}


// ==================== [0x4A28FC] sub_4A28FC (size: 621) ====================
char __fastcall sub_4A28FC(int a1, int a2)
{
  int v2; // eax
  int v3; // eax
  char v4; // zf
  int v5; // eax
  int v6; // eax
  int v7; // edx
  _DWORD *v8; // eax
  int v9; // eax
  unsigned int v11[2]; // [esp-Ch] [ebp-48h] BYREF
  int *v12; // [esp-4h] [ebp-40h]
  int v13; // [esp+4h] [ebp-38h] BYREF
  int v14; // [esp+8h] [ebp-34h] BYREF
  int v15; // [esp+Ch] [ebp-30h]
  int v16; // [esp+10h] [ebp-2Ch]
  int v17; // [esp+14h] [ebp-28h]
  char v18; // [esp+1Bh] [ebp-21h]
  int v19; // [esp+1Ch] [ebp-20h] BYREF
  int v20; // [esp+20h] [ebp-1Ch]
  int v21; // [esp+24h] [ebp-18h]
  int v22; // [esp+28h] [ebp-14h]
  int v23; // [esp+2Ch] [ebp-10h]
  char v24; // [esp+33h] [ebp-9h]
  int v25; // [esp+34h] [ebp-8h] BYREF
  int v26; // [esp+38h] [ebp-4h]
  int savedregs; // [esp+3Ch] [ebp+0h] BYREF

  v13 = 0;
  v14 = 0;
  v19 = 0;
  v25 = a2;
  v26 = a1;
  unknown_libname_44(a2);
  v12 = &savedregs;
  v11[1] = (unsigned int)&loc_4A2B5A;
  v11[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v11);
  v18 = 0;
  v23 = sub_4B2ED0(v25);
  if ( v23 != 0 )
  {
    v18 = 1;
    v2 = (*(int (__fastcall **)(int))(*(_DWORD *)v23 + 20))(v23) - 1;
    if ( v2 >= 0 )
    {
      v16 = v2 + 1;
      v22 = 0;
      while ( 1 )
      {
        (*(void (__fastcall **)(int, int, int *))(*(_DWORD *)v23 + 12))(v23, v22, &v19);
        v20 = (*(int (__fastcall **)(int, int))(*(_DWORD *)v23 + 24))(v23, v22);
        if ( *(_DWORD *)(*(_DWORD *)(v26 + 1036) + 8) - 1 >= 0 )
        {
          v15 = *(_DWORD *)(*(_DWORD *)(v26 + 1036) + 8);
          v21 = 0;
          do
          {
            v3 = Classes::TList::Get(*(Classes::TList **)(v26 + 1036), v21);
            sub_4AC1AC(*off_4EA490, *(unsigned __int16 *)(v3 + 4), &v14);
            System::__linkproc__ LStrCmp(v19, v14);
            if ( v4 )
              --v20;
            ++v21;
            --v15;
          }
          while ( v15 != 0 );
        }
        if ( v20 > 0 )
          break;
        ++v22;
        if ( --v16 == 0 )
          goto LABEL_12;
      }
      v18 = 0;
    }
LABEL_12:
    if ( v18 != 0 )
    {
      v17 = 0;
      v5 = (*(int (__fastcall **)(int))(*(_DWORD *)v23 + 20))(v23) - 1;
      if ( v5 >= 0 )
      {
        v16 = v5 + 1;
        v22 = 0;
        do
        {
          (*(void (__fastcall **)(int, int, int *))(*(_DWORD *)v23 + 12))(v23, v22, &v19);
          v20 = (*(int (__fastcall **)(int, int))(*(_DWORD *)v23 + 24))(v23, v22);
          if ( *(_DWORD *)(*(_DWORD *)(v26 + 1036) + 8) - 1 >= 0 )
          {
            v21 = *(_DWORD *)(*(_DWORD *)(v26 + 1036) + 8) - 1;
            do
            {
              if ( v20 <= 0 )
                break;
              v6 = Classes::TList::Get(*(Classes::TList **)(v26 + 1036), v21);
              sub_4AC1AC(*off_4EA490, *(unsigned __int16 *)(v6 + 4), &v13);
              System::__linkproc__ LStrCmp(v19, v13);
              if ( v4 )
              {
                if ( v17 == 0 )
                {
                  LOBYTE(v7) = 1;
                  v17 = unknown_libname_22(off_410028, v7);
                }
                v8 = (_DWORD *)Classes::TList::Get(*(Classes::TList **)(v26 + 1036), v21);
                (*(void (__fastcall **)(int, int, _DWORD))(*(_DWORD *)v17 + 56))(v17, v19, *v8);
                v9 = Classes::TList::Get(*(Classes::TList **)(v26 + 1036), v21);
                System::__linkproc__ FreeMem(v9, 24);
                Classes::TList::Delete(*(Classes::TList **)(v26 + 1036), v21);
                --v20;
              }
              --v21;
            }
            while ( v21 != -1 );
          }
          ++v22;
          --v16;
        }
        while ( v16 != 0 );
      }
      if ( v17 != 0 )
        sub_4B865C(0, 0, 0, v17, 0);
    }
  }
  v24 = v18;
  __writefsdword(0, v11[0]);
  v12 = (int *)&loc_4A2B61;
  unknown_libname_35(&v13, 2);
  unknown_libname_34(&v19);
  unknown_libname_34(&v25);
  return v24;
}


// ==================== [0x4A2B6C] sub_4A2B6C (size: 728) ====================
int __fastcall sub_4A2B6C(int a1, int *a2, int a3)
{
  char v3; // zf
  int v4; // ecx
  int v5; // ecx
  int v6; // ecx
  int v8; // [esp-3Ch] [ebp-B8h]
  unsigned int v9[2]; // [esp-Ch] [ebp-88h] BYREF
  int *v10; // [esp-4h] [ebp-80h]
  _DWORD v11[2]; // [esp+4h] [ebp-78h] BYREF
  _DWORD v12[4]; // [esp+Ch] [ebp-70h] BYREF
  _BYTE v13[20]; // [esp+1Ch] [ebp-60h] BYREF
  _BYTE v14[20]; // [esp+30h] [ebp-4Ch] BYREF
  int v15; // [esp+44h] [ebp-38h] BYREF
  int v16; // [esp+48h] [ebp-34h] BYREF
  int v17; // [esp+4Ch] [ebp-30h]
  int v18; // [esp+50h] [ebp-2Ch]
  int v19; // [esp+54h] [ebp-28h]
  int Mem; // [esp+58h] [ebp-24h]
  int v21; // [esp+5Ch] [ebp-20h]
  Classes::TList *v22; // [esp+60h] [ebp-1Ch]
  char v23; // [esp+67h] [ebp-15h]
  int v24; // [esp+68h] [ebp-14h]
  int v25; // [esp+6Ch] [ebp-10h]
  int v26; // [esp+70h] [ebp-Ch] BYREF
  int *v27; // [esp+74h] [ebp-8h]
  int v28; // [esp+78h] [ebp-4h]
  int savedregs; // [esp+7Ch] [ebp+0h] BYREF

  v16 = 0;
  v15 = 0;
  memset(v12, 0, sizeof(v12));
  v11[1] = 0;
  v11[0] = 0;
  v17 = 0;
  v26 = a3;
  v27 = a2;
  v28 = a1;
  unknown_libname_44(a3);
  v10 = &savedregs;
  v9[1] = (unsigned int)&loc_4A2E38;
  v9[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v9);
  v23 = 0;
  v24 = 1;
  if ( *(_DWORD *)(*(_DWORD *)(v28 + 1416) + 8) - 1 >= 0 )
  {
    v18 = *(_DWORD *)(*(_DWORD *)(v28 + 1416) + 8);
    v25 = 0;
    do
    {
      if ( v23 != 0 )
        break;
      v22 = (Classes::TList *)Classes::TList::Get(*(Classes::TList **)(v28 + 1416), v25);
      v21 = Classes::TList::Get(v22, 0);
      v19 = sub_4AC2F8(*off_4EA490, *(unsigned __int16 *)(v21 + 4));
      if ( v19 != 0 )
      {
        unknown_libname_41(v9[0]);
        System::__linkproc__ LStrCmp(v17, v26);
        if ( v3 )
        {
          if ( v27[12] <= 100 )
          {
            v24 = 3;
          }
          else if ( sub_4A28FC((int)v27, v26) != 0 )
          {
            Mem = System::__linkproc__ GetMem(24);
            sub_4AC404(*off_4EA490, v26, Mem);
            if ( (unsigned __int8)sub_4C4DB8(v27, Mem) != 0 )
            {
              v27[12] -= 100;
              sub_4D0824(v27, Mem);
              unknown_libname_16(v14, &dword_4A2E44);
              LOBYTE(v4) = 18;
              System::__linkproc__ PStrNCat(v14, v27 + 1, v4);
              unknown_libname_16(v13, v14);
              LOBYTE(v5) = 19;
              System::__linkproc__ PStrNCat(v13, dword_4A2E48, v5);
              unknown_libname_41(v9[0]);
              Sysutils::IntToStr(v27[9]);
              Sysutils::IntToStr(v27[10]);
              unknown_libname_41(dword_4A2E54);
              sub_4AC1AC(*off_4EA490, *(unsigned __int16 *)(Mem + 4), v12);
              Sysutils::IntToStr(*(_DWORD *)Mem);
              unknown_libname_41(&dword_4A2E60);
              System::__linkproc__ LStrCatN(&v16, 13, v6, v8, v11[0]);
              sub_4E42F8(v16);
              v24 = 0;
            }
            else
            {
              System::__linkproc__ FreeMem(Mem, 24);
              v24 = 2;
            }
          }
          else
          {
            v24 = 4;
          }
        }
      }
      ++v25;
      --v18;
    }
    while ( v18 != 0 );
  }
  if ( v24 != 0 )
    sub_4B865C(0, 0, 0, v24, 0);
  else
    sub_4B865C(0, 0, 0, v27[12], 0);
  __writefsdword(0, v9[0]);
  v10 = (int *)&loc_4A2E3F;
  unknown_libname_35(v11, 6);
  unknown_libname_35(&v15, 3);
  return unknown_libname_34(&v26);
}


// ==================== [0x4A2ECC] sub_4A2ECC (size: 373) ====================
int __fastcall sub_4A2ECC(Inpop3::TInPOP3 *a1)
{
  int v1; // eax
  unsigned int v3[6]; // [esp-Ch] [ebp-1Ch] BYREF
  Inpop3::TInPOP3 *v4; // [esp+Ch] [ebp-4h]
  int savedregs; // [esp+10h] [ebp+0h] BYREF

  v4 = a1;
  v3[2] = (unsigned int)&savedregs;
  v3[1] = (unsigned int)&loc_4A301F;
  v3[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v3);
  if ( GetTickCount() - *((_DWORD *)a1 + 350) > 0x7530 )
  {
    *((_DWORD *)v4 + 350) = GetTickCount();
    Inpop3::TInPOP3::Login(v4);
  }
  if ( GetTickCount() - *((_DWORD *)v4 + 351) > 0x927C0 )
  {
    *((_DWORD *)v4 + 351) = GetTickCount();
    sub_4A01A0(v4);
  }
  if ( System::__linkproc__ RandInt(50) != 0 )
  {
    if ( System::__linkproc__ RandInt(80) == 0 )
      sub_4B8EBC(0, 0, *((_DWORD *)v4 + 10), *((_DWORD *)v4 + 9));
  }
  else
  {
    v1 = System::__linkproc__ RandInt(8);
    sub_4BB048(v4, v1);
  }
  if ( *((_BYTE *)v4 + 1397) != 0 && *(_BYTE *)(*off_4EA080[0] + 65) != 0 )
  {
    if ( *((_BYTE *)v4 + 701) == 0 )
    {
      sub_4B8EBC(0, 0, 0, 0);
      *((_BYTE *)v4 + 701) = 1;
    }
  }
  else if ( *((_BYTE *)v4 + 701) != 0 )
  {
    *((_BYTE *)v4 + 701) = 0;
    sub_4B8EBC(0, 0, *((_DWORD *)v4 + 10), *((_DWORD *)v4 + 9));
  }
  __writefsdword(0, v3[0]);
  return sub_49ABCC(v4);
}


// ==================== [0x4A3124] sub_4A3124 (size: 566) ====================
int __fastcall sub_4A3124(int a1, int a2, int a3)
{
  int v3; // ecx
  int v4; // eax
  int v6; // [esp-14h] [ebp-48h]
  unsigned int v7[2]; // [esp-Ch] [ebp-40h] BYREF
  int *v8; // [esp-4h] [ebp-38h]
  int v9; // [esp+4h] [ebp-30h] BYREF
  int v10; // [esp+8h] [ebp-2Ch]
  int v11; // [esp+Ch] [ebp-28h]
  int v12; // [esp+10h] [ebp-24h] BYREF
  int v13; // [esp+14h] [ebp-20h]
  int v14; // [esp+18h] [ebp-1Ch] BYREF
  _DWORD *v15; // [esp+20h] [ebp-14h]
  int v16; // [esp+24h] [ebp-10h]
  int v17; // [esp+28h] [ebp-Ch] BYREF
  int v18; // [esp+2Ch] [ebp-8h]
  int v19; // [esp+30h] [ebp-4h]
  int savedregs; // [esp+34h] [ebp+0h] BYREF

  v9 = 0;
  v10 = 0;
  v12 = 0;
  v11 = 0;
  v13 = 0;
  v14 = 0;
  v17 = a3;
  v18 = a2;
  v19 = a1;
  unknown_libname_44(a3);
  v8 = &savedregs;
  v7[1] = (unsigned int)&loc_4A334B;
  v7[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v7);
  v16 = 0;
  sub_408F0C(v17, &v14);
  unknown_libname_37(&v17, v14);
  v15 = nullptr;
  if ( v17 == 0 )
    v16 = -4;
  if ( *(_DWORD *)(v18 + 800) != 0 )
  {
    v16 = -1;
  }
  else if ( *(int *)(v18 + 48) < 1000000 )
  {
    v16 = -2;
  }
  else
  {
    v15 = (_DWORD *)sub_4C4AB0(v18, *off_4EA3F4);
    if ( v15 == nullptr )
      v16 = -3;
  }
  if ( v16 == 0 )
  {
    unknown_libname_41(v7[0]);
    if ( (unsigned __int8)sub_49A4A4(*off_4EA07C[0], v17, v13) != 0 )
    {
      unknown_libname_41(&str___59[1]);
      System::__linkproc__ LStrCatN(&v12, 3, v3, v6, v11);
      sub_4AF728(v12);
      sub_4D0BDC(v18, v15);
      Indnsresolver::TInDNSResolver::ParseAnswers(v18, *v15, *off_4EA3F4);
      sub_4BF6A8(v18, 1000000);
      sub_4C49F4(v18);
      unknown_libname_41(v7[0]);
      v4 = sub_49A408(*off_4EA07C[0], v10);
      *(_DWORD *)(v18 + 800) = v4;
      if ( *(_DWORD *)(v18 + 800) != 0 )
      {
        sub_4987F0(&v9);
        unknown_libname_36(v18 + 808, v9);
      }
    }
    else
    {
      v16 = -4;
    }
  }
  if ( v16 != 0 )
    sub_4B865C(0, 0, 0, v16, 0);
  else
    sub_4B865C(0, 0, 0, 0, 0);
  __writefsdword(0, v7[0]);
  v8 = (int *)&loc_4A3352;
  unknown_libname_35(&v9, 6);
  unknown_libname_34(&v17);
  return v16;
}


// ==================== [0x4A35E8] sub_4A35E8 (size: 355) ====================
int __fastcall sub_4A35E8(int a1, int a2, _BYTE *a3)
{
  struct _EXCEPTION_REGISTRATION_RECORD *v4; // [esp-18h] [ebp-3Ch] BYREF
  void *v5; // [esp-14h] [ebp-38h]
  int *v6; // [esp-10h] [ebp-34h]
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-Ch] [ebp-30h] BYREF
  void *v8; // [esp-8h] [ebp-2Ch]
  int *v9; // [esp-4h] [ebp-28h]
  char v10; // [esp+Ch] [ebp-18h]
  int v11; // [esp+10h] [ebp-14h] BYREF
  int v12; // [esp+14h] [ebp-10h]
  _BYTE *v13; // [esp+18h] [ebp-Ch]
  int v14; // [esp+1Ch] [ebp-8h]
  int v15; // [esp+20h] [ebp-4h]
  int savedregs; // [esp+24h] [ebp+0h] BYREF

  v12 = 0;
  v11 = 0;
  v13 = a3;
  v14 = a2;
  v15 = a1;
  unknown_libname_44(a3);
  v9 = &savedregs;
  v8 = &loc_4A373D;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  v6 = &savedregs;
  v5 = &loc_4A370E;
  v4 = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&v4);
  if ( v13 != nullptr && *v13 == 64 )
  {
    v10 = 13;
    sub_453DBC(&v11, 0);
    sub_49E584(v15, v14, v12);
    if ( unknown_libname_84(v12, &str___buildguildnow[1]) == 0 )
      sub_4A3124(v15, v14, v11);
    if ( unknown_libname_84(v12, &str___guildwar[1]) == 0 )
      Inreply::TIdReplies::Add(v15, v14, v11, v9, v8, ExceptionList, v6, v5, v4);
    if ( unknown_libname_84(v12, &str___donate[1]) == 0 )
      sub_4A346C(v15, v14);
    if ( unknown_libname_84(v12, &str__requestcastlew[1]) == 0 )
      sub_4A3498(v15, v14);
    if ( unknown_libname_84(v12, &str__exit_1[1]) == 0 )
      sub_4B865C(0, 0, 0, v15, 0);
  }
  __writefsdword(0, (unsigned int)v4);
  __writefsdword(0, (unsigned int)ExceptionList);
  v9 = (int *)&loc_4A3744;
  return unknown_libname_35(&v11, 3);
}


// ==================== [0x4A3B7C] sub_4A3B7C (size: 678) ====================
int __fastcall sub_4A3B7C(__int32 a1, int a2, __int32 a3, int a4)
{
  char v4; // zf
  unsigned int v6[2]; // [esp-Ch] [ebp-58h] BYREF
  int *v7; // [esp-4h] [ebp-50h]
  int v8; // [esp+4h] [ebp-48h] BYREF
  _DWORD v9[2]; // [esp+8h] [ebp-44h] BYREF
  _DWORD v10[2]; // [esp+10h] [ebp-3Ch] BYREF
  _DWORD v11[2]; // [esp+18h] [ebp-34h] BYREF
  int v12; // [esp+20h] [ebp-2Ch] BYREF
  _DWORD v13[2]; // [esp+24h] [ebp-28h] BYREF
  _DWORD v14[2]; // [esp+2Ch] [ebp-20h] BYREF
  int v15; // [esp+34h] [ebp-18h] BYREF
  int v16; // [esp+38h] [ebp-14h]
  int v17; // [esp+3Ch] [ebp-10h] BYREF
  __int32 v18; // [esp+40h] [ebp-Ch]
  int v19; // [esp+44h] [ebp-8h]
  __int32 v20; // [esp+48h] [ebp-4h] BYREF
  int savedregs; // [esp+4Ch] [ebp+0h] BYREF

  v18 = _InterlockedExchange(&v20, a3);
  v19 = a2;
  v20 = a1;
  unknown_libname_44(a4);
  v7 = &savedregs;
  v6[1] = (unsigned int)&loc_4A3E14;
  v6[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v6);
  sub_49FD04(a4);
  System::__linkproc__ LStrCmp(a4, &str__CASTLEGOLD[1]);
  if ( v4 )
  {
    Sysutils::IntToStr(*(_DWORD *)(*off_4EA080[0] + 576));
    sub_49ADB8(&v15, v14[1]);
    unknown_libname_36(v18, v15);
  }
  System::__linkproc__ LStrCmp(a4, &str__TODAYINCOME[1]);
  if ( v4 )
  {
    Sysutils::IntToStr(*(_DWORD *)(*off_4EA080[0] + 580));
    sub_49ADB8(v14, v13[1]);
    unknown_libname_36(v18, v14[0]);
  }
  System::__linkproc__ LStrCmp(a4, &str__CASTLEDOORSTAT[1]);
  if ( v4 )
  {
    v16 = *(_DWORD *)(*off_4EA080[0] + 104);
    if ( *(_BYTE *)(v16 + 772) != 0 )
    {
      unknown_libname_37(&v17, &str_Broken[1]);
    }
    else if ( *(_BYTE *)(v16 + 1380) != 0 )
    {
      unknown_libname_37(&v17, &str_Open[1]);
    }
    else
    {
      unknown_libname_37(&v17, &str_Shut[1]);
    }
    sub_49ADB8(v13, v17);
    unknown_libname_36(v18, v13[0]);
  }
  System::__linkproc__ LStrCmp(a4, &str__REPAIRDOORGOLD[1]);
  if ( v4 )
  {
    Sysutils::IntToStr(2000000);
    sub_49ADB8(&v12, v11[1]);
    unknown_libname_36(v18, v12);
  }
  System::__linkproc__ LStrCmp(a4, &str__REPAIRWALLGOLD[1]);
  if ( v4 )
  {
    Sysutils::IntToStr(500000);
    sub_49ADB8(v11, v10[1]);
    unknown_libname_36(v18, v11[0]);
  }
  System::__linkproc__ LStrCmp(a4, &str__GUARDFEE[1]);
  if ( v4 )
  {
    Sysutils::IntToStr(300000);
    sub_49ADB8(v10, v9[1]);
    unknown_libname_36(v18, v10[0]);
  }
  System::__linkproc__ LStrCmp(a4, &str__ARCHERFEE[1]);
  if ( v4 )
  {
    Sysutils::IntToStr(300000);
    sub_49ADB8(v9, v8);
    unknown_libname_36(v18, v9[0]);
  }
  System::__linkproc__ LStrCmp(a4, &str__GUARDRULE[1]);
  __writefsdword(0, v6[0]);
  v7 = (int *)&loc_4A3E1B;
  unknown_libname_35(&v8, 13);
  unknown_libname_34(&v17);
  return unknown_libname_34(&a4);
}


// ==================== [0x4A413C] sub_4A413C (size: 394) ====================
int __fastcall sub_4A413C(int a1, int a2, int a3)
{
  int v3; // eax
  unsigned int v5[2]; // [esp-Ch] [ebp-24h] BYREF
  int *v6; // [esp-4h] [ebp-1Ch]
  _DWORD *v7; // [esp+4h] [ebp-14h]
  unsigned int v8; // [esp+8h] [ebp-10h]
  int v9; // [esp+Ch] [ebp-Ch]
  _DWORD v10[2]; // [esp+10h] [ebp-8h] BYREF
  int savedregs; // [esp+18h] [ebp+0h] BYREF

  v9 = a3;
  v10[0] = a2;
  v10[1] = a1;
  unknown_libname_44(a2);
  v6 = &savedregs;
  v5[1] = (unsigned int)&loc_4A42BB;
  v5[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v5);
  if ( *(int *)(*off_4EA080[0] + 576) < 300000 )
  {
    sub_4BB124(v9, aNeedMoreGold_0, 0);
    goto LABEL_13;
  }
  v8 = sub_453B20(v10[0], 0) - 1;
  if ( v8 >= 4 )
  {
    sub_4BB124(v9, &str____________0[1], 0);
    goto LABEL_13;
  }
  if ( *(_DWORD *)(*off_4EA080[0] + 24 * v8 + 200) != 0 )
  {
    if ( *(_BYTE *)(*(_DWORD *)(*off_4EA080[0] + 24 * v8 + 200) + 772) == 0 )
    {
      sub_4BB124(v9, &str_Already_Hired__[1], 0);
      goto LABEL_13;
    }
  }
  else if ( *(_BYTE *)(*off_4EA080[0] + 65) == 0 )
  {
    v7 = (_DWORD *)(*off_4EA080[0] + 24 * v8 + 180);
    v3 = sub_4ADE3C(v7[2], v7[1]);
    v7[5] = v3;
    if ( v7[5] != 0 )
    {
      *(_DWORD *)(*off_4EA080[0] + 576) -= 300000;
      sub_4BB124(v9, &str_Hired___[1], 1);
    }
LABEL_13:
    __writefsdword(0, v5[0]);
    v6 = (int *)&loc_4A42C2;
    return unknown_libname_34(v10);
  }
  sub_4BB124(v9, aCannotHire, 0);
  goto LABEL_13;
}


// ==================== [0x4A433C] sub_4A433C (size: 468) ====================
int __fastcall sub_4A433C(int a1, int a2, int a3)
{
  int v3; // eax
  unsigned int v5[2]; // [esp-Ch] [ebp-24h] BYREF
  int *v6; // [esp-4h] [ebp-1Ch]
  _DWORD *v7; // [esp+4h] [ebp-14h]
  unsigned int v8; // [esp+8h] [ebp-10h]
  int v9; // [esp+Ch] [ebp-Ch]
  _DWORD v10[2]; // [esp+10h] [ebp-8h] BYREF
  int savedregs; // [esp+18h] [ebp+0h] BYREF

  v9 = a3;
  v10[0] = a2;
  v10[1] = a1;
  unknown_libname_44(a2);
  v6 = &savedregs;
  v5[1] = (unsigned int)&loc_4A4505;
  v5[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v5);
  if ( *(int *)(*off_4EA080[0] + 576) < 300000 )
  {
    sub_4BB124(v9, aNeedMoreGold_1, 0);
    goto LABEL_13;
  }
  v8 = sub_453B20(v10[0], 0) - 1;
  if ( v8 >= 0xC )
  {
    sub_4BB124(v9, &str____________1[1], 0);
    goto LABEL_13;
  }
  if ( *(_DWORD *)(*off_4EA080[0] + 24 * v8 + 296) != 0 )
  {
    if ( *(_BYTE *)(*(_DWORD *)(*off_4EA080[0] + 24 * v8 + 296) + 772) == 0 )
    {
      sub_4BB124(v9, &str_Already_Hired___0[1], 0);
      goto LABEL_13;
    }
  }
  else if ( *(_BYTE *)(*off_4EA080[0] + 65) == 0 )
  {
    v7 = (_DWORD *)(*off_4EA080[0] + 24 * v8 + 276);
    v3 = sub_4ADE3C(v7[2], v7[1]);
    v7[5] = v3;
    if ( v7[5] != 0 )
    {
      *(_DWORD *)(*off_4EA080[0] + 576) -= 300000;
      *(_DWORD *)(v7[5] + 684) = *off_4EA080[0];
      *(_DWORD *)(v7[5] + 1360) = *v7;
      *(_DWORD *)(v7[5] + 1364) = v7[1];
      *(_DWORD *)(v7[5] + 1368) = 3;
      sub_4BB124(v9, aHiredGuard, 1);
    }
LABEL_13:
    __writefsdword(0, v5[0]);
    v6 = (int *)&loc_4A450C;
    return unknown_libname_34(v10);
  }
  sub_4BB124(v9, &str_Cant_Repair_[1], 0);
  goto LABEL_13;
}


// ==================== [0x4A45BC] sub_4A45BC (size: 1178) ====================
int __fastcall sub_4A45BC(__int32 a1, int a2, __int32 a3)
{
  int v3; // eax
  int v4; // eax
  int v5; // eax
  int v6; // eax
  int v7; // eax
  int v8; // eax
  int v9; // eax
  int v10; // eax
  int v11; // edx
  struct _EXCEPTION_REGISTRATION_RECORD *v13; // [esp-18h] [ebp-48h] BYREF
  void *v14; // [esp-14h] [ebp-44h]
  int *v15; // [esp-10h] [ebp-40h]
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-Ch] [ebp-3Ch] BYREF
  void *v17; // [esp-8h] [ebp-38h]
  int *v18; // [esp-4h] [ebp-34h]
  int v19; // [esp+Ch] [ebp-24h] BYREF
  int v20; // [esp+10h] [ebp-20h] BYREF
  char v21; // [esp+14h] [ebp-1Ch]
  int v22; // [esp+18h] [ebp-18h] BYREF
  int v23; // [esp+1Ch] [ebp-14h]
  int v24; // [esp+20h] [ebp-10h] BYREF
  _BYTE *v25; // [esp+24h] [ebp-Ch]
  int v26; // [esp+28h] [ebp-8h]
  __int32 v27; // [esp+2Ch] [ebp-4h] BYREF
  int savedregs; // [esp+30h] [ebp+0h] BYREF

  v25 = (_BYTE *)_InterlockedExchange(&v27, a3);
  v26 = a2;
  v27 = a1;
  unknown_libname_44(v25);
  v18 = &savedregs;
  v17 = &loc_4A4A48;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  v15 = &savedregs;
  v14 = &loc_4A4A0C;
  v13 = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&v13);
  if ( v25 != nullptr && *v25 == 64 )
  {
    v21 = 13;
    sub_453DBC(&v24, 0);
    unknown_libname_34(&v22);
    unknown_libname_36(v26 + 812, v25);
    sub_49E584(v27, v26, v23);
    if ( (unsigned __int8)sub_490400(*off_4EA080[0], *(_DWORD *)(v26 + 800)) == 0
      || (unsigned __int8)sub_4BF4A0(v26) == 0 )
    {
      sub_4B865C(&str_Access_Denied_[1], 0, 0, v27, 0);
      goto LABEL_45;
    }
    if ( unknown_libname_84(v23, &str___withdrawal[1]) == 0 )
    {
      v3 = sub_453B20(v24, 0);
      v4 = sub_49066C(*off_4EA080[0], v26, v3, v18, v17, ExceptionList, v15, v14, v13) + 3;
      if ( v4 == 0 )
      {
        unknown_libname_37(&v22, &str_______________1[1]);
        goto LABEL_15;
      }
      v5 = v4 - 1;
      if ( v5 == 0 )
      {
        unknown_libname_37(&v22, &str_More_gold_is_ne[1]);
        goto LABEL_15;
      }
      v6 = v5 - 1;
      if ( v6 != 0 )
      {
        if ( v6 != 2 )
          goto LABEL_15;
        goto LABEL_14;
      }
LABEL_11:
      System::__linkproc__ LStrCat3((int)&v22, *(void **)(*off_4EA080[0] + 28), &str_______________0[1]);
LABEL_15:
      sub_4B865C(v22, 0, 0, v27, 0);
      goto LABEL_45;
    }
    if ( unknown_libname_84(v23, &str___receipts[1]) == 0 )
    {
      v7 = sub_453B20(v24, 0);
      v8 = sub_490864(*off_4EA080[0], v26, v7, v18, v17, ExceptionList, v15, v14, v13) + 3;
      if ( v8 == 0 )
      {
        unknown_libname_37(&v22, &str____________[1]);
        goto LABEL_15;
      }
      v9 = v8 - 1;
      if ( v9 == 0 )
      {
        unknown_libname_37(&v22, &str_______________2[1]);
        goto LABEL_15;
      }
      v10 = v9 - 1;
      if ( v10 != 0 )
      {
        if ( v10 != 2 )
          goto LABEL_15;
LABEL_14:
        unknown_libname_25(v27, v26, &str__main_3[1]);
        goto LABEL_15;
      }
      goto LABEL_11;
    }
    if ( unknown_libname_84(v23, &str__openmaindoor[1]) != 0 )
    {
      if ( unknown_libname_84(v23, &str__closemaindoor[1]) != 0 )
      {
        if ( unknown_libname_84(v23, &str__repairdoornow[1]) != 0 )
        {
          if ( unknown_libname_84(v23, &str__repairwallnow1[1]) != 0 )
          {
            if ( unknown_libname_84(v23, &str__repairwallnow2[1]) != 0 )
            {
              if ( unknown_libname_84(v23, &str__repairwallnow3[1]) != 0 )
              {
                if ( (unsigned __int8)sub_4543B0(v23, &str__hireguardnow[1], 13, v18, v17, ExceptionList, v15, v14, v13) != 0 )
                {
                  unknown_libname_43(v23);
                  System::__linkproc__ LStrCopy(&v20);
                  sub_4A413C(v27, v20, v26);
                  unknown_libname_25(v27, v26, &str__hireguards[1]);
                }
                if ( (unsigned __int8)sub_4543B0(
                                        v23,
                                        &str__hirearchernow[1],
                                        14,
                                        v18,
                                        v17,
                                        ExceptionList,
                                        v15,
                                        v14,
                                        v13) != 0 )
                {
                  unknown_libname_43(v23);
                  System::__linkproc__ LStrCopy(&v19);
                  sub_4A433C(v27, v19, v26);
                  sub_4B865C(0, 0, 0, v27, 0);
                }
                if ( unknown_libname_84(v23, &str__exit_2[1]) == 0 )
                  sub_4B865C(0, 0, 0, v27, 0);
              }
              else
              {
                sub_4A4074(v27, 3, v26, v18, v17, ExceptionList, v15, v14, v13);
                unknown_libname_25(v27, v26, &str__main_3[1]);
              }
            }
            else
            {
              sub_4A4074(v27, 2, v26, v18, v17, ExceptionList, v15, v14, v13);
              unknown_libname_25(v27, v26, &str__main_3[1]);
            }
          }
          else
          {
            sub_4A4074(v27, 1, v26, v18, v17, ExceptionList, v15, v14, v13);
            unknown_libname_25(v27, v26, &str__main_3[1]);
          }
        }
        else
        {
          sub_4A3FB8(v27, v26);
          unknown_libname_25(v27, v26, &str__main_3[1]);
        }
      }
      else
      {
        LOBYTE(v11) = 1;
        sub_490460(*off_4EA080[0], v11);
      }
    }
    else
    {
      sub_490460(*off_4EA080[0], 0);
    }
  }
LABEL_45:
  __writefsdword(0, (unsigned int)v13);
  __writefsdword(0, (unsigned int)ExceptionList);
  v18 = (int *)&loc_4A4A4F;
  unknown_libname_35(&v19, 2);
  return unknown_libname_35(&v22, 4);
}


// ==================== [0x4A5614] sub_4A5614 (size: 314) ====================
int __fastcall sub_4A5614(int a1)
{
  int v1; // ecx
  int v2; // eax
  int v3; // ecx
  int v4; // edx
  __int64 v5; // rax

  if ( *(_BYTE *)(a1 + 764) == 0
    && *(_BYTE *)(a1 + 772) == 0
    && *(_WORD *)(a1 + 106) == 0
    && sub_407B30() - *(_DWORD *)(a1 + 1020) > *(_DWORD *)(a1 + 1276) )
  {
    *(_DWORD *)(a1 + 1020) = sub_407B30();
    if ( *(_BYTE *)(a1 + 701) != 0 )
    {
      unknown_libname_25(a1, a1, v1);
    }
    else
    {
      v2 = sub_407B30();
      v4 = a1;
      if ( v2 - *(_DWORD *)(a1 + 1016) > *(_DWORD *)(a1 + 1300) )
        sub_4C94B4(a1);
      if ( *(_DWORD *)(a1 + 888) != 0 )
      {
        v5 = *(_DWORD *)(*(_DWORD *)(a1 + 888) + 36) - *(_DWORD *)(a1 + 36);
        LODWORD(v5) = (HIDWORD(v5) ^ v5) - HIDWORD(v5);
        v4 = a1;
        if ( (int)v5 <= *(_DWORD *)(a1 + 1368) )
          v4 = a1;
      }
      unknown_libname_25(a1, v4, v3);
    }
  }
  return sub_4C936C(a1);
}


// ==================== [0x4A5BC0] sub_4A5BC0 (size: 484) ====================
char __fastcall sub_4A5BC0(int a1)
{
  int v1; // eax
  char v2; // al
  int v3; // ecx
  __int64 v4; // rax
  __int64 v5; // rax
  DWORD TickCount; // eax
  int v7; // edx
  int v8; // eax
  int v10; // [esp+8h] [ebp-1Ch]
  int v11; // [esp+10h] [ebp-14h]
  int v12; // [esp+14h] [ebp-10h]
  int v13; // [esp+18h] [ebp-Ch]
  char v14; // [esp+1Fh] [ebp-5h]

  v14 = 0;
  if ( (unsigned __int8)sub_4A5B0C(a1) != 0 )
  {
    if ( sub_407B30() - *(_DWORD *)(a1 + 1016) > *(_DWORD *)(a1 + 1300) )
    {
      *(_DWORD *)(a1 + 1016) = sub_407B30();
      sub_4C2E0C(*(_DWORD *)(a1 + 40), *(_DWORD *)(a1 + 36));
      v1 = a1 + 408;
      LOWORD(v1) = *(_WORD *)(a1 + 414);
      v2 = sub_407B28(v1);
      v12 = *(unsigned __int8 *)(a1 + 414) + System::__linkproc__ RandInt((char)(v2 - *(_BYTE *)(a1 + 414)) + 1);
      if ( *(_DWORD *)(*(_DWORD *)(a1 + 1032) + 8) - 1 >= 0 )
      {
        v10 = *(_DWORD *)(*(_DWORD *)(a1 + 1032) + 8);
        v13 = 0;
        do
        {
          v11 = *(_DWORD *)(Classes::TList::Get(*(Classes::TList **)(a1 + 1032), v13) + 4);
          if ( *(_BYTE *)(v11 + 772) == 0 && (unsigned __int8)unknown_libname_25(a1, v11, v3) != 0 )
          {
            v4 = *(_DWORD *)(a1 + 36) - *(_DWORD *)(v11 + 36);
            if ( (signed int)((HIDWORD(v4) ^ v4) - HIDWORD(v4)) <= *(_DWORD *)(a1 + 484) )
            {
              v5 = *(_DWORD *)(a1 + 40) - *(_DWORD *)(v11 + 40);
              if ( (signed int)((HIDWORD(v5) ^ v5) - HIDWORD(v5)) <= *(_DWORD *)(a1 + 484) )
              {
                TickCount = GetTickCount();
                HIWORD(v7) = HIWORD(a1);
                *(_DWORD *)(a1 + 892) = TickCount;
                LOWORD(v7) = *(_WORD *)(v11 + 40);
                HIWORD(TickCount) = HIWORD(v11);
                LOWORD(TickCount) = *(_WORD *)(v11 + 36);
                v8 = unknown_libname_78(TickCount, v7);
                sub_4B87C4(600, 0, v11, 2, v8, v12);
                if ( System::__linkproc__ RandInt(4) == 0 )
                {
                  if ( System::__linkproc__ RandInt(3) != 0 )
                    sub_4C35A8(3);
                  else
                    sub_4C35A8(0);
                }
                *(_DWORD *)(a1 + 888) = v11;
              }
            }
          }
          ++v13;
          --v10;
        }
        while ( v10 != 0 );
      }
    }
    return 1;
  }
  return v14;
}


// ==================== [0x4A5DCC] sub_4A5DCC (size: 455) ====================
int __fastcall sub_4A5DCC(int a1)
{
  int v1; // ecx
  int v2; // ecx
  __int64 v3; // rax
  __int64 v4; // rax
  int v5; // edx
  int v6; // ecx
  DWORD TickCount; // eax
  int v9; // [esp+10h] [ebp-10h]
  int v10; // [esp+14h] [ebp-Ch]
  int i; // [esp+18h] [ebp-8h]

  if ( *(_BYTE *)(a1 + 764) == 0
    && *(_BYTE *)(a1 + 772) == 0
    && *(_WORD *)(a1 + 106) == 0
    && sub_407B30() - *(_DWORD *)(a1 + 1020) > *(_DWORD *)(a1 + 1276) )
  {
    *(_DWORD *)(a1 + 1020) = sub_407B30();
    if ( *(_BYTE *)(a1 + 701) != 0 )
    {
      if ( GetTickCount() - *(_DWORD *)(a1 + 1376) > 0x2710 && *(_DWORD *)(*(_DWORD *)(a1 + 1032) + 8) - 1 >= 0 )
      {
        v9 = *(_DWORD *)(*(_DWORD *)(a1 + 1032) + 8);
        for ( i = 0; ; ++i )
        {
          v10 = *(_DWORD *)(Classes::TList::Get(*(Classes::TList **)(a1 + 1032), i) + 4);
          if ( *(_BYTE *)(v10 + 772) == 0
            && (unsigned __int8)unknown_libname_25(a1, v10, v1) != 0
            && (*(_BYTE *)(v10 + 836) == 0 || *(_BYTE *)(a1 + 838) != 0) )
          {
            v3 = *(_DWORD *)(a1 + 36) - *(_DWORD *)(v10 + 36);
            if ( (signed int)((HIDWORD(v3) ^ v3) - HIDWORD(v3)) <= *(_DWORD *)(a1 + 1364) )
            {
              v4 = *(_DWORD *)(a1 + 40) - *(_DWORD *)(v10 + 40);
              LODWORD(v4) = (HIDWORD(v4) ^ v4) - HIDWORD(v4);
              v5 = a1;
              if ( (int)v4 <= *(_DWORD *)(a1 + 1364) )
                break;
            }
          }
          if ( --v9 == 0 )
            return sub_4A5614(a1);
        }
        goto LABEL_20;
      }
    }
    else if ( GetTickCount() - *(_DWORD *)(a1 + 1376) > 0xBB8 && (unsigned __int8)unknown_libname_25(a1, a1, v6) == 0 )
    {
      TickCount = GetTickCount();
      v5 = a1;
      if ( TickCount - *(_DWORD *)(a1 + 1376) > 0x2710 )
      {
LABEL_20:
        unknown_libname_25(a1, v5, v2);
        *(_DWORD *)(a1 + 1376) = GetTickCount();
      }
    }
  }
  return sub_4A5614(a1);
}


// ==================== [0x4A5FEC] sub_4A5FEC (size: 399) ====================
char __fastcall sub_4A5FEC(int a1)
{
  int v1; // eax
  char v2; // al
  int v3; // ecx
  __int64 v4; // rax
  __int64 v5; // rax
  int v6; // eax
  int v8; // [esp+8h] [ebp-1Ch]
  int v9; // [esp+10h] [ebp-14h]
  int v10; // [esp+14h] [ebp-10h]
  int v11; // [esp+18h] [ebp-Ch]
  char v12; // [esp+1Fh] [ebp-5h]

  v12 = 0;
  if ( sub_407B30() - *(_DWORD *)(a1 + 1016) > *(_DWORD *)(a1 + 1300) )
  {
    *(_DWORD *)(a1 + 1016) = sub_407B30();
    sub_4C2E0C(*(_DWORD *)(a1 + 40), *(_DWORD *)(a1 + 36));
    v1 = a1 + 408;
    LOWORD(v1) = *(_WORD *)(a1 + 414);
    v2 = sub_407B28(v1);
    v10 = *(unsigned __int8 *)(a1 + 414) + System::__linkproc__ RandInt((char)(v2 - *(_BYTE *)(a1 + 414)) + 1);
    if ( *(_DWORD *)(*(_DWORD *)(a1 + 1032) + 8) - 1 >= 0 )
    {
      v8 = *(_DWORD *)(*(_DWORD *)(a1 + 1032) + 8);
      v11 = 0;
      do
      {
        v9 = *(_DWORD *)(Classes::TList::Get(*(Classes::TList **)(a1 + 1032), v11) + 4);
        if ( *(_BYTE *)(v9 + 772) == 0 && (unsigned __int8)unknown_libname_25(a1, v9, v3) != 0 )
        {
          v4 = *(_DWORD *)(a1 + 36) - *(_DWORD *)(v9 + 36);
          if ( (signed int)((HIDWORD(v4) ^ v4) - HIDWORD(v4)) <= *(_DWORD *)(a1 + 484) )
          {
            v5 = *(_DWORD *)(a1 + 40) - *(_DWORD *)(v9 + 40);
            LODWORD(v5) = (HIDWORD(v5) ^ v5) - HIDWORD(v5);
            HIWORD(v5) = HIWORD(a1);
            if ( (int)v5 <= *(_DWORD *)(a1 + 484) )
            {
              WORD2(v5) = *(_WORD *)(v9 + 40);
              WORD1(v5) = HIWORD(v9);
              LOWORD(v5) = *(_WORD *)(v9 + 36);
              v6 = unknown_libname_78(v5, HIDWORD(v5));
              sub_4B87C4(200, 0, v9, 1, v6, v10);
              sub_4B8EBC(0, 1, *(_DWORD *)(v9 + 40), *(_DWORD *)(v9 + 36));
            }
          }
        }
        ++v11;
        --v8;
      }
      while ( v8 != 0 );
    }
    return 1;
  }
  return v12;
}


// ==================== [0x4A65C4] sub_4A65C4 (size: 352) ====================
int __fastcall sub_4A65C4(int a1)
{
  int v1; // eax
  char v2; // al
  int result; // eax
  int v4; // ecx
  __int64 v5; // rax
  bool v6; // cc
  __int64 v7; // rax
  int v8; // [esp+4h] [ebp-1Ch]
  int v9; // [esp+8h] [ebp-18h]
  int v10; // [esp+Ch] [ebp-14h]
  int v11; // [esp+10h] [ebp-10h]
  int v12; // [esp+10h] [ebp-10h]
  int v13; // [esp+14h] [ebp-Ch]
  int v14; // [esp+18h] [ebp-8h]

  *(_WORD *)(a1 + 420) = 0;
  v1 = a1 + 408;
  v9 = v1;
  LOWORD(v1) = *(_WORD *)(a1 + 414);
  v2 = sub_407B28(v1);
  v13 = *(unsigned __int8 *)(v9 + 6) + System::__linkproc__ RandInt((char)(v2 - *(_BYTE *)(v9 + 6)) + 1);
  result = *(_DWORD *)(*(_DWORD *)(a1 + 1032) + 8) - 1;
  if ( result >= 0 )
  {
    v8 = *(_DWORD *)(*(_DWORD *)(a1 + 1032) + 8);
    v14 = 0;
    do
    {
      v10 = *(_DWORD *)(Classes::TList::Get(*(Classes::TList **)(a1 + 1032), v14) + 4);
      v5 = *(_DWORD *)(v10 + 36) - *(_DWORD *)(a1 + 36);
      LODWORD(v5) = (HIDWORD(v5) ^ v5) - HIDWORD(v5);
      v6 = (int)v5 <= 1;
      result = v5 - 1;
      if ( v6 )
      {
        v7 = *(_DWORD *)(v10 + 40) - *(_DWORD *)(a1 + 40);
        LODWORD(v7) = (HIDWORD(v7) ^ v7) - HIDWORD(v7);
        v6 = (int)v7 <= 1;
        result = v7 - 1;
        if ( v6 )
        {
          result = v10;
          if ( *(_BYTE *)(v10 + 772) == 0 )
          {
            result = unknown_libname_25(a1, v10, v4);
            if ( (_BYTE)result != 0 )
            {
              v11 = sub_4BDD20(v10, a1, v13 / 2);
              result = sub_4BDDEC(v10, a1, v13 / 2);
              v12 = result + v11;
              if ( v12 > 0 )
              {
                sub_4BDEB8(v10, v12);
                result = sub_4B87C4(700, 0, a1, *(unsigned __int16 *)(v10 + 424), *(unsigned __int16 *)(v10 + 420), v12);
              }
            }
          }
        }
      }
      ++v14;
      --v8;
    }
    while ( v8 != 0 );
  }
  return result;
}


// ==================== [0x4A6890] sub_4A6890 (size: 545) ====================
bool __fastcall sub_4A6890(int a1, int a2)
{
  bool v3; // [esp+3h] [ebp-9h]

  if ( *(_DWORD *)(a1 + 684) != 0 )
  {
    v3 = *(_DWORD *)(a1 + 896) == a2;
    if ( *(_BYTE *)(a2 + 688) != 0 )
    {
      if ( GetTickCount() - *(_DWORD *)(a2 + 692) >= 0x1D4C0 )
        *(_BYTE *)(a2 + 688) = 0;
      else
        v3 = true;
      if ( *(_DWORD *)(a2 + 684) != 0 )
      {
        *(_BYTE *)(a2 + 688) = 0;
        v3 = false;
      }
    }
    if ( *(_BYTE *)(*(_DWORD *)(a1 + 684) + 65) != 0 )
      v3 = true;
    if ( *(_DWORD *)(*(_DWORD *)(a1 + 684) + 32) != 0 )
    {
      if ( *(_DWORD *)(a2 + 648) != 0 )
      {
        if ( (*(_DWORD *)(*(_DWORD *)(a1 + 684) + 32) == *(_DWORD *)(*(_DWORD *)(a2 + 648) + 800)
           || (unsigned __int8)sub_499BD8(
                                 *(_DWORD *)(*(_DWORD *)(a1 + 684) + 32),
                                 *(_DWORD *)(*(_DWORD *)(a2 + 648) + 800)) != 0)
          && *(_DWORD *)(a1 + 896) != *(_DWORD *)(a2 + 648)
          && *(_DWORD *)(a1 + 896) != a2 )
        {
          v3 = false;
        }
      }
      else if ( (*(_DWORD *)(*(_DWORD *)(a1 + 684) + 32) == *(_DWORD *)(a2 + 800)
              || (unsigned __int8)sub_499BD8(*(_DWORD *)(*(_DWORD *)(a1 + 684) + 32), *(_DWORD *)(a2 + 800)) != 0)
             && *(_DWORD *)(a1 + 896) != a2 )
      {
        v3 = false;
      }
    }
    if ( *(_BYTE *)(a2 + 841) != 0
      || *(_BYTE *)(a2 + 837) != 0
      || *(_BYTE *)(a2 + 570) >= 0xAu && *(_BYTE *)(a2 + 570) < 0x32u
      || a2 == a1
      || *(_DWORD *)(a2 + 684) == *(_DWORD *)(a1 + 684) )
    {
      return false;
    }
  }
  else
  {
    v3 = *(_DWORD *)(a1 + 896) == a2;
    if ( *(_DWORD *)(a2 + 888) != 0 && *(_BYTE *)(*(_DWORD *)(a2 + 888) + 570) == 112 )
      v3 = true;
    if ( sub_4BF0A0(a2) >= 2 )
      v3 = true;
    if ( *(_BYTE *)(a2 + 841) != 0 || *(_BYTE *)(a2 + 837) != 0 || a2 == a1 )
      return false;
  }
  return v3;
}


// ==================== [0x4A6B30] sub_4A6B30 (size: 308) ====================
int __fastcall sub_4A6B30(int a1, int a2)
{
  int v2; // eax
  char v3; // al
  __int64 v4; // rax
  __int64 v5; // rax
  int v6; // ecx
  int v7; // eax
  int v9; // [esp-18h] [ebp-2Ch]
  int v10; // [esp-Ch] [ebp-20h]
  int v11; // [esp-8h] [ebp-1Ch]
  int v12; // [esp+8h] [ebp-Ch]

  *(_BYTE *)(a1 + 44) = sub_4B2C38(*(_DWORD *)(a2 + 40));
  v2 = a1 + 408;
  LOWORD(v2) = *(_WORD *)(a1 + 414);
  v3 = sub_407B28(v2);
  v12 = *(unsigned __int8 *)(a1 + 414) + System::__linkproc__ RandInt((char)(v3 - *(_BYTE *)(a1 + 414)) + 1);
  if ( v12 > 0 )
    v12 = sub_4BDD20(a2, a1, v12);
  if ( v12 > 0 )
  {
    sub_4BC74C(a2, a1);
    *(_DWORD *)(a2 + 904) = 0;
    sub_4BDEB8(a2, v12);
    v11 = *(unsigned __int16 *)(a2 + 420);
    v10 = *(unsigned __int16 *)(a2 + 424);
    v4 = *(_DWORD *)(a1 + 40) - *(_DWORD *)(a2 + 40);
    v9 = (HIDWORD(v4) ^ v4) - HIDWORD(v4);
    v5 = *(_DWORD *)(a1 + 36) - *(_DWORD *)(a2 + 36);
    v7 = Ibutils::Max((Ibutils *)((HIDWORD(v5) ^ v5) - HIDWORD(v5)), v9, v6);
    sub_4B87C4(50 * v7 + 600, 0, a1, v10, v11, v12);
  }
  return sub_4B8EBC(0, a2, *(_DWORD *)(a1 + 40), *(_DWORD *)(a1 + 36));
}


// ==================== [0x4A6C64] sub_4A6C64 (size: 431) ====================
int __fastcall sub_4A6C64(int a1)
{
  unsigned __int64 v1; // rax
  int v2; // ecx
  __int64 v3; // rax
  int v4; // ecx
  int v5; // edx
  int v7; // [esp+4h] [ebp-1Ch]
  int v8; // [esp+8h] [ebp-18h]
  int v9; // [esp+Ch] [ebp-14h]
  int v10; // [esp+10h] [ebp-10h]
  int v11; // [esp+18h] [ebp-8h]

  v10 = 9999;
  v8 = 0;
  if ( *(_BYTE *)(a1 + 772) == 0 && *(_BYTE *)(a1 + 764) == 0 && *(_WORD *)(a1 + 106) == 0 )
  {
    if ( sub_407B30() - *(_DWORD *)(a1 + 1020) > *(_DWORD *)(a1 + 1276) )
    {
      v1 = __PAIR64__(a1, sub_407B30());
      *(_DWORD *)(a1 + 1020) = v1;
      if ( *(_DWORD *)(*(_DWORD *)(a1 + 1032) + 8) - 1 >= 0 )
      {
        v7 = *(_DWORD *)(*(_DWORD *)(a1 + 1032) + 8);
        v11 = 0;
        do
        {
          v9 = *(_DWORD *)(Classes::TList::Get(*(Classes::TList **)(a1 + 1032), v11) + 4);
          if ( *(_BYTE *)(v9 + 772) == 0 && (unsigned __int8)unknown_libname_25(a1, v9, v2) != 0 )
          {
            v3 = *(_DWORD *)(a1 + 36) - *(_DWORD *)(v9 + 36);
            v4 = (HIDWORD(v3) ^ v3) - HIDWORD(v3);
            v1 = *(_DWORD *)(a1 + 40) - *(_DWORD *)(v9 + 40);
            v2 = (HIDWORD(v1) ^ v1) - HIDWORD(v1) + v4;
            if ( v2 < v10 )
            {
              v10 = v2;
              v8 = v9;
            }
          }
          ++v11;
          --v7;
        }
        while ( v7 != 0 );
      }
      if ( v8 != 0 )
        unknown_libname_25(a1, v8, v2);
      else
        unknown_libname_25(a1, HIDWORD(v1), v2);
    }
    if ( *(_DWORD *)(a1 + 888) != 0 )
    {
      if ( sub_407B30() - *(_DWORD *)(a1 + 1016) > *(_DWORD *)(a1 + 1300) )
      {
        *(_DWORD *)(a1 + 1016) = sub_407B30();
        sub_4A6B30(a1, *(_DWORD *)(a1 + 888));
      }
    }
    else if ( *(int *)(a1 + 1368) >= 0 )
    {
      v5 = a1;
      if ( *(unsigned __int8 *)(a1 + 44) != *(_DWORD *)(a1 + 1368) )
      {
        LOBYTE(v5) = *(_BYTE *)(a1 + 1368);
        sub_4BB048(a1, v5);
      }
    }
  }
  return sub_4C936C(a1);
}


// ==================== [0x4A6FB4] sub_4A6FB4 (size: 512) ====================
int __fastcall sub_4A6FB4(int a1, char a2)
{
  int result; // eax

  sub_4B5E8C(1);
  sub_4B5E8C(1);
  sub_4B5E8C(1);
  sub_4B5E8C(a2 != 1);
  sub_4B5E8C(a2 != 1);
  sub_4B5E8C(a2 != 1);
  sub_4B5E8C(a2 != 1);
  sub_4B5E8C(a2 != 1);
  sub_4B5E8C(a2 != 1);
  sub_4B5E8C(a2 != 1);
  sub_4B5E8C(a2 != 1);
  result = sub_4B5E8C(a2 != 1);
  if ( a2 == 0 )
  {
    sub_4B5E8C(0);
    sub_4B5E8C(0);
    return sub_4B5E8C(0);
  }
  return result;
}


// ==================== [0x4A7518] sub_4A7518 (size: 302) ====================
int __fastcall sub_4A7518(int a1)
{
  int v2; // [esp+Ch] [ebp-8h]

  if ( *(_BYTE *)(a1 + 772) != 0 )
  {
    *(_DWORD *)(a1 + 776) = GetTickCount();
    if ( *(_BYTE *)(a1 + 1380) != 0 )
    {
      sub_4B5E8C(1);
      *(_BYTE *)(a1 + 1380) = 0;
    }
  }
  else
  {
    *(_DWORD *)(a1 + 880) = 0;
    if ( *(_BYTE *)(a1 + 1380) == 0 )
    {
      sub_4B5E8C(0);
      *(_BYTE *)(a1 + 1380) = 1;
    }
  }
  if ( *(_WORD *)(a1 + 420) != 0 )
    v2 = 3
       - System::__linkproc__ ROUND((double)*(unsigned __int16 *)(a1 + 420) / (double)*(unsigned __int16 *)(a1 + 424) * flt_4A7648);
  else
    v2 = 4;
  if ( *(unsigned __int8 *)(a1 + 44) != v2 && v2 < 5 )
  {
    *(_BYTE *)(a1 + 44) = v2;
    sub_4B8EBC(0, 0, *(_DWORD *)(a1 + 40), *(_DWORD *)(a1 + 36));
  }
  return sub_4C936C(a1);
}


// ==================== [0x4A776C] sub_4A776C (size: 404) ====================
int __fastcall sub_4A776C(int a1, int a2, int a3)
{
  int v3; // eax
  char v5; // [esp+17h] [ebp-Dh]
  int v6; // [esp+18h] [ebp-Ch] BYREF
  int v7; // [esp+1Ch] [ebp-8h] BYREF
  int v8; // [esp+20h] [ebp-4h]

  v8 = a1;
  if ( *(int *)(a1 + 1360) <= 0 )
  {
    *(_DWORD *)(v8 + 1336) = -1;
  }
  else
  {
    v3 = sub_4B2A6C(&v6, &v7, 1, *(unsigned __int8 *)(v8 + 44));
    if ( (_BYTE)v3 != 0 )
    {
      LOBYTE(v3) = v5;
      if ( (unsigned __int8)sub_4B5ED0(v3) == 0 )
      {
        switch ( *(_BYTE *)(v8 + 44) )
        {
          case 0:
            *(_BYTE *)(v8 + 44) = 4;
            break;
          case 1:
            *(_BYTE *)(v8 + 44) = 7;
            break;
          case 2:
            *(_BYTE *)(v8 + 44) = 6;
            break;
          case 3:
            *(_BYTE *)(v8 + 44) = 5;
            break;
          case 4:
            *(_BYTE *)(v8 + 44) = 0;
            break;
          case 5:
            *(_BYTE *)(v8 + 44) = 3;
            break;
          case 6:
            *(_BYTE *)(v8 + 44) = 2;
            break;
          case 7:
            *(_BYTE *)(v8 + 44) = 1;
            break;
          default:
            break;
        }
        sub_4B2A6C(&v6, &v7, *(_DWORD *)(v8 + 1360), *(unsigned __int8 *)(v8 + 44));
        *(_DWORD *)(v8 + 1336) = v7;
        a2 = v8;
        *(_DWORD *)(v8 + 1340) = v6;
      }
    }
  }
  if ( *(_DWORD *)(v8 + 1336) != -1 )
  {
    unknown_libname_25(v8, a2, a3);
    if ( *(_DWORD *)(v8 + 1336) == *(_DWORD *)(v8 + 36) && *(_DWORD *)(v8 + 1340) == *(_DWORD *)(v8 + 40) )
      *(_DWORD *)(v8 + 1360) = 0;
  }
  return sub_4C936C(v8);
}


// ==================== [0x4A8C58] @Mxarrays@TStringArray@Find$qqr17System@AnsiStringri (size: 402) ====================
void *__fastcall Mxarrays::TStringArray::Find(int a1, int a2, int a3)
{
  int v3; // ecx
  void *v4; // edx
  unsigned int v6[2]; // [esp-Ch] [ebp-2Ch] BYREF
  int *v7; // [esp-4h] [ebp-24h]
  void *v8; // [esp+Ch] [ebp-14h]
  void *v9; // [esp+10h] [ebp-10h]
  int v10; // [esp+14h] [ebp-Ch]
  _DWORD v11[2]; // [esp+18h] [ebp-8h] BYREF
  int savedregs; // [esp+20h] [ebp+0h] BYREF

  v10 = a3;
  v11[0] = a2;
  v11[1] = a1;
  unknown_libname_44(a2);
  v7 = &savedregs;
  v6[1] = (unsigned int)&loc_4A8DD9;
  v6[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v6);
  v9 = nullptr;
  v8 = (void *)sub_4ADE3C(v11[0], *(_DWORD *)(v10 + 40));
  if ( v8 != nullptr )
  {
    *((_DWORD *)v8 + 162) = *(_DWORD *)(v10 + 648);
    *((_DWORD *)v8 + 163) = *(_DWORD *)(v10 + 652);
    *((_BYTE *)v8 + 665) = *(_BYTE *)(v10 + 665);
    v4 = v8;
    *((_BYTE *)v8 + 664) = *(_BYTE *)(v10 + 664);
    unknown_libname_25(v8, v4, v3);
    sub_4BF124(v8);
    if ( *(_DWORD *)(v10 + 648) != 0 )
      Classes::TList::Add(*(Classes::TList **)(*(_DWORD *)(v10 + 648) + 668), v8);
    qmemcpy((char *)v8 + 408, (const void *)(v10 + 408), 0x28u);
    System::Move((const void *)(v10 + 96), (char *)v8 + 96, 24);
    *((_DWORD *)v8 + 222) = *(_DWORD *)(v10 + 888);
    *((_DWORD *)v8 + 223) = *(_DWORD *)(v10 + 892);
    *((_DWORD *)v8 + 224) = *(_DWORD *)(v10 + 896);
    *((_DWORD *)v8 + 225) = *(_DWORD *)(v10 + 900);
    *((_BYTE *)v8 + 44) = *(_BYTE *)(v10 + 44);
    v9 = v8;
  }
  __writefsdword(0, v6[0]);
  v7 = (int *)&loc_4A8DE0;
  unknown_libname_34(v11);
  return v9;
}


// ==================== [0x4A9020] sub_4A9020 (size: 965) ====================
int __fastcall sub_4A9020(unsigned int a1)
{
  __int64 v2; // rax
  int v3; // ecx
  unsigned __int64 v4; // rax
  __int64 v5; // rax
  DWORD v6; // [esp+0h] [ebp-20h]
  int v7; // [esp+14h] [ebp-Ch] BYREF
  int v8; // [esp+18h] [ebp-8h] BYREF
  unsigned int v9; // [esp+1Ch] [ebp-4h]

  v9 = a1;
  if ( *(_BYTE *)(a1 + 764) != 0
    || *(_BYTE *)(v9 + 772) != 0
    || *(_BYTE *)(v9 + 701) != 0
    || *(_BYTE *)(v9 + 837) != 0
    || *(_WORD *)(v9 + 106) != 0 )
  {
    return sub_4C936C(v9);
  }
  if ( (unsigned __int8)sub_4A8E54(v9) != 0 )
    return sub_4C936C(v9);
  if ( *(_BYTE *)(v9 + 1296) != 0 )
  {
    v6 = GetTickCount() - *(_DWORD *)(v9 + 1292);
    v2 = *(int *)(v9 + 1288);
    if ( *(int *)(v9 + 1288) < 0 )
    {
      if ( v2 < 0 )
LABEL_13:
        *(_BYTE *)(v9 + 1296) = 0;
    }
    else if ( (unsigned int)v2 < v6 )
    {
      goto LABEL_13;
    }
  }
  if ( *(_BYTE *)(v9 + 1296) == 0 && sub_407B30() - *(_DWORD *)(v9 + 1020) > *(_DWORD *)(v9 + 1276) )
  {
    *(_DWORD *)(v9 + 1020) = sub_407B30();
    ++*(_DWORD *)(v9 + 1284);
    HIDWORD(v4) = v9;
    if ( *(_DWORD *)(v9 + 1284) > *(_DWORD *)(v9 + 1280) )
    {
      *(_DWORD *)(v9 + 1284) = 0;
      *(_BYTE *)(v9 + 1296) = 1;
      v4 = __PAIR64__(v9, GetTickCount());
      *(_DWORD *)(v9 + 1292) = v4;
    }
    if ( *(_BYTE *)(v9 + 1344) != 0 )
    {
      if ( *(int *)(v9 + 1352) > 0 )
      {
        v4 = __PAIR64__(v9, GetTickCount());
        if ( (unsigned int)(v4 - *(_DWORD *)(v9 + 1348)) > *(_DWORD *)(v9 + 1352) )
        {
          *(_BYTE *)(v9 + 1344) = 0;
          HIDWORD(v4) = 0;
          *(_DWORD *)(v9 + 1352) = 0;
        }
      }
    }
    else
    {
      if ( *(_BYTE *)(v9 + 704) == 0 )
      {
        if ( *(_DWORD *)(v9 + 888) != 0 )
        {
          if ( (unsigned __int8)unknown_libname_25(v9, HIDWORD(v4), v3) != 0 )
            return sub_4C936C(v9);
        }
        else
        {
          *(_DWORD *)(v9 + 1336) = -1;
          if ( *(_BYTE *)(v9 + 824) != 0 )
          {
            *(_DWORD *)(v9 + 1336) = *(_DWORD *)(v9 + 828);
            HIDWORD(v4) = v9;
            *(_DWORD *)(v9 + 1340) = *(_DWORD *)(v9 + 832);
          }
        }
      }
      if ( *(_DWORD *)(v9 + 648) != 0 )
      {
        if ( *(_DWORD *)(v9 + 888) == 0 )
        {
          sub_4B2900(*(_DWORD *)(v9 + 648), &v8, &v7);
          if ( (int)abs32(*(_DWORD *)(v9 + 1336) - v8) > 1
            || (v4 = *(_DWORD *)(v9 + 1340) - v8, (int)abs32(*(_DWORD *)(v9 + 1340) - v8) > 1) )
          {
            *(_DWORD *)(v9 + 1336) = v8;
            *(_DWORD *)(v9 + 1340) = v7;
            v4 = *(_DWORD *)(v9 + 36) - v8;
            if ( (int)abs32(*(_DWORD *)(v9 + 36) - v8) <= 2 )
            {
              v4 = *(_DWORD *)(v9 + 40) - v7;
              if ( (int)abs32(*(_DWORD *)(v9 + 40) - v7) <= 2 && sub_4B5838(1) != 0 )
              {
                *(_DWORD *)(v9 + 1336) = *(_DWORD *)(v9 + 36);
                HIDWORD(v4) = v9;
                *(_DWORD *)(v9 + 1340) = *(_DWORD *)(v9 + 40);
              }
            }
          }
        }
        if ( *(_BYTE *)(*(_DWORD *)(v9 + 648) + 672) == 0 )
        {
          if ( *(_DWORD *)(*(_DWORD *)(v9 + 648) + 760) != *(_DWORD *)(v9 + 760)
            || (v5 = *(_DWORD *)(v9 + 36) - *(_DWORD *)(*(_DWORD *)(v9 + 648) + 36),
                (int)((HIDWORD(v5) ^ v5) - HIDWORD(v5)) > 20)
            || (v4 = *(_DWORD *)(v9 + 40) - *(_DWORD *)(*(_DWORD *)(v9 + 648) + 40),
                (int)((HIDWORD(v4) ^ v4) - HIDWORD(v4)) > 20) )
          {
            sub_4BCD1C(1, *(_DWORD *)(v9 + 1340));
          }
        }
      }
    }
    if ( (*(_DWORD *)(v9 + 648) == 0 || *(_BYTE *)(*(_DWORD *)(v9 + 648) + 672) == 0)
      && (*(_DWORD *)(v9 + 1336) != -1 || *(_DWORD *)(v9 + 888) == 0) )
    {
      unknown_libname_25(v9, HIDWORD(v4), v3);
    }
  }
  return sub_4C936C(v9);
}


// ==================== [0x4A9438] sub_4A9438 (size: 599) ====================
int __fastcall sub_4A9438(unsigned int a1)
{
  int v1; // ecx
  __int64 v2; // rax
  int v3; // ecx
  __int64 v4; // rax
  __int64 v5; // rax
  __int64 v6; // rax
  int v8; // [esp+Ch] [ebp-20h]
  int v9; // [esp+10h] [ebp-1Ch]
  int v10; // [esp+14h] [ebp-18h]
  int v11; // [esp+18h] [ebp-14h]
  signed int v12; // [esp+1Ch] [ebp-10h]
  int v13; // [esp+24h] [ebp-8h]

  v12 = 9999;
  v9 = 0;
  if ( *(_BYTE *)(a1 + 772) == 0 && *(_BYTE *)(a1 + 1364) == 0 && *(_BYTE *)(a1 + 764) == 0 && *(_WORD *)(a1 + 106) == 0 )
  {
    if ( sub_407B30() - *(_DWORD *)(a1 + 1020) > *(_DWORD *)(a1 + 1276) )
    {
      if ( *(_DWORD *)(*(_DWORD *)(a1 + 1032) + 8) - 1 >= 0 )
      {
        v8 = *(_DWORD *)(*(_DWORD *)(a1 + 1032) + 8);
        v13 = 0;
        do
        {
          v10 = *(_DWORD *)(Classes::TList::Get(*(Classes::TList **)(a1 + 1032), v13) + 4);
          if ( *(_BYTE *)(v10 + 772) == 0
            && (unsigned __int8)unknown_libname_25(a1, v10, v1) != 0
            && (*(_BYTE *)(v10 + 836) == 0 || *(_BYTE *)(a1 + 838) != 0) )
          {
            v2 = *(_DWORD *)(a1 + 36) - *(_DWORD *)(v10 + 36);
            v3 = (HIDWORD(v2) ^ v2) - HIDWORD(v2);
            v4 = *(_DWORD *)(a1 + 40) - *(_DWORD *)(v10 + 40);
            if ( (int)((HIDWORD(v4) ^ v4) - HIDWORD(v4) + v3) < v12 )
            {
              v12 = (HIDWORD(v4) ^ v4) - HIDWORD(v4) + v3;
              v9 = v10;
            }
          }
          ++v13;
          --v8;
        }
        while ( v8 != 0 );
      }
      if ( v9 != 0 )
      {
        *(_BYTE *)(a1 + 1344) = 1;
        *(_DWORD *)(a1 + 888) = v9;
      }
      else
      {
        *(_BYTE *)(a1 + 1344) = 0;
        *(_DWORD *)(a1 + 888) = 0;
      }
    }
    if ( *(_BYTE *)(a1 + 1344) != 0
      && *(_DWORD *)(a1 + 888) != 0
      && sub_407B30() - *(_DWORD *)(a1 + 1020) > *(_DWORD *)(a1 + 1276) )
    {
      v5 = *(_DWORD *)(a1 + 36) - *(_DWORD *)(*(_DWORD *)(a1 + 888) + 36);
      if ( (int)((HIDWORD(v5) ^ v5) - HIDWORD(v5)) <= 6 )
      {
        v6 = *(_DWORD *)(a1 + 40) - *(_DWORD *)(*(_DWORD *)(a1 + 888) + 40);
        if ( (int)((HIDWORD(v6) ^ v6) - HIDWORD(v6)) <= 6 )
        {
          v11 = (unsigned __int8)sub_4B2C38(*(_DWORD *)(a1 + 40));
          sub_4B2A6C(a1 + 1340, a1 + 1336, 5, v11);
        }
      }
    }
  }
  return sub_4A9020(a1);
}


// ==================== [0x4A98AC] sub_4A98AC (size: 475) ====================
char *__fastcall sub_4A98AC(char *a1, unsigned __int8 a2)
{
  char *v2; // eax
  char v3; // al
  char *result; // eax
  int v5; // ecx
  char *v6; // [esp+8h] [ebp-24h]
  char *v7; // [esp+Ch] [ebp-20h]
  char *v8; // [esp+10h] [ebp-1Ch]
  int j; // [esp+1Ch] [ebp-10h]
  int i; // [esp+20h] [ebp-Ch]

  a1[44] = a2;
  v2 = a1 + 408;
  v6 = v2;
  LOWORD(v2) = *((_WORD *)a1 + 207);
  v3 = sub_407B28(v2);
  result = (char *)((unsigned __int8)v6[6] + System::__linkproc__ RandInt((char)(v3 - v6[6]) + 1));
  v8 = result;
  if ( (int)result > 0 )
  {
    sub_4B8EBC(0, 0, *((_DWORD *)a1 + 10), *((_DWORD *)a1 + 9));
    for ( i = 0; i != 5; ++i )
    {
      for ( j = 0; j != 5; ++j )
      {
        result = (char *)off_4EA228 + 25 * a2 + 5 * i;
        if ( result[j] == 1 )
        {
          result = (char *)sub_4B5838(1);
          v7 = result;
          if ( result != nullptr && result != a1 )
          {
            result = (char *)unknown_libname_25(a1, result, v5);
            if ( (_BYTE)result != 0 )
            {
              result = (char *)System::__linkproc__ RandInt((unsigned __int8)v7[620]);
              if ( (int)result < (unsigned __int8)a1[572] )
              {
                result = (char *)sub_4BDDEC(v7, a1, v8);
                v8 = result;
                if ( (int)result > 0 )
                {
                  sub_4BDEB8(v7, result);
                  sub_4B87C4(300, 0, a1, *((unsigned __int16 *)v7 + 212), *((unsigned __int16 *)v7 + 210), (__int16)v8);
                  result = a1;
                  if ( a1[1368] != 0 )
                  {
                    result = (char *)System::__linkproc__ RandInt((unsigned __int8)v7[586] + 20);
                    if ( result == nullptr )
                      result = (char *)sub_4C35A8(1);
                  }
                }
              }
            }
          }
        }
      }
    }
  }
  return result;
}


// ==================== [0x4A9C78] sub_4A9C78 (size: 347) ====================
int __fastcall sub_4A9C78(int a1, char a2)
{
  int v2; // eax
  char v3; // al
  int v4; // ecx
  int v6; // [esp+18h] [ebp-18h]
  int v7; // [esp+1Ch] [ebp-14h]
  int v8; // [esp+20h] [ebp-10h]
  int v9; // [esp+20h] [ebp-10h]
  int v10; // [esp+24h] [ebp-Ch]

  v10 = 0;
  *(_BYTE *)(a1 + 44) = a2;
  v2 = a1 + 408;
  v6 = v2;
  LOWORD(v2) = *(_WORD *)(a1 + 414);
  v3 = sub_407B28(v2);
  v8 = *(unsigned __int8 *)(v6 + 6) + System::__linkproc__ RandInt((char)(v3 - *(_BYTE *)(v6 + 6)) + 1);
  if ( v8 > 0 )
  {
    sub_4B8EBC(0, 0, *(_DWORD *)(a1 + 40), *(_DWORD *)(a1 + 36));
    v7 = sub_4C368C(a1);
    if ( v7 != 0
      && (unsigned __int8)unknown_libname_25(a1, v7, v4) != 0
      && System::__linkproc__ RandInt(*(unsigned __int8 *)(v7 + 620)) < *(unsigned __int8 *)(a1 + 572) )
    {
      v9 = sub_4BDDEC(v7, a1, v8);
      if ( v9 > 0 )
      {
        sub_4BDEB8(v7, v9);
        sub_4B87C4(300, 0, a1, *(unsigned __int16 *)(v7 + 424), *(unsigned __int16 *)(v7 + 420), v9);
        if ( System::__linkproc__ RandInt(*(unsigned __int8 *)(v7 + 586) + 20) == 0 )
          sub_4C35A8(0);
        return v7;
      }
    }
  }
  return v10;
}


// ==================== [0x4AA294] sub_4AA294 (size: 544) ====================
int __fastcall sub_4AA294(int a1)
{
  int v2; // [esp+8h] [ebp-10h]
  int v3; // [esp+Ch] [ebp-Ch] BYREF
  int v4; // [esp+10h] [ebp-8h] BYREF
  int v5; // [esp+14h] [ebp-4h]

  v5 = a1;
  if ( *(_BYTE *)(a1 + 772) != 0 || *(_BYTE *)(v5 + 1364) != 0 || *(_BYTE *)(v5 + 764) != 0 )
    return sub_4A9720(v5);
  if ( GetTickCount() - *(_DWORD *)(v5 + 1368) <= 0x7530
    || (*(_DWORD *)(v5 + 1368) = GetTickCount(), *(_DWORD *)(v5 + 888) == 0)
    || sub_4C3538(v5) < 5 )
  {
    v2 = *(_DWORD *)(v5 + 1376);
    *(_DWORD *)(v5 + 1376) = 7 - *(unsigned __int16 *)(v5 + 420) / (*(unsigned __int16 *)(v5 + 424) / 7u);
    if ( *(int *)(v5 + 1376) >= 2 && *(_DWORD *)(v5 + 1376) != v2 )
    {
      *(_BYTE *)(v5 + 1372) = 1;
      *(_DWORD *)(v5 + 1380) = GetTickCount();
    }
    if ( *(_BYTE *)(v5 + 1372) != 0 )
    {
      if ( GetTickCount() - *(_DWORD *)(v5 + 1380) >= 0x1F40 )
      {
        *(_BYTE *)(v5 + 1372) = 0;
        *(_BYTE *)(v5 + 1373) = 1;
        *(_DWORD *)(v5 + 1384) = GetTickCount();
      }
      else
      {
        *(_DWORD *)(v5 + 1300) = 10000;
      }
    }
    if ( *(_BYTE *)(v5 + 1373) != 0 )
    {
      if ( GetTickCount() - *(_DWORD *)(v5 + 1384) >= 0x1F40 )
      {
        *(_BYTE *)(v5 + 1373) = 0;
        *(_DWORD *)(v5 + 1300) = *(_DWORD *)(v5 + 1388);
        *(_DWORD *)(v5 + 1276) = *(_DWORD *)(v5 + 1392);
      }
      else
      {
        *(_DWORD *)(v5 + 1300) = 500;
        *(_DWORD *)(v5 + 1276) = 400;
      }
    }
    return sub_4A9720(v5);
  }
  sub_4B2900(*(_DWORD *)(v5 + 888), &v4, &v3);
  if ( (unsigned __int8)sub_4B5ED0(0) != 0 )
    return sub_4BCD1C(0, v3);
  else
    return sub_4BCB54(v5, *(_DWORD *)(*(_DWORD *)(v5 + 760) + 4), 0);
}


// ==================== [0x4AA604] sub_4AA604 (size: 579) ====================
int __fastcall sub_4AA604(unsigned int a1)
{
  __int64 v1; // rax
  __int64 v2; // rax
  __int64 v3; // rax
  __int64 v4; // rax
  __int64 v5; // rax
  __int64 v6; // rax
  int v8; // [esp+10h] [ebp-Ch]

  if ( *(_BYTE *)(a1 + 772) == 0 && *(_BYTE *)(a1 + 1364) == 0 && *(_BYTE *)(a1 + 764) == 0 && *(_WORD *)(a1 + 106) == 0 )
  {
    if ( GetTickCount() - *(_DWORD *)(a1 + 1024) > 0x1F40
      || GetTickCount() - *(_DWORD *)(a1 + 1024) > 0x3E8 && *(_DWORD *)(a1 + 888) == 0 )
    {
      *(_DWORD *)(a1 + 1024) = GetTickCount();
      sub_4C94B4(a1);
    }
    if ( sub_407B30() - *(_DWORD *)(a1 + 1020) > *(_DWORD *)(a1 + 1276) && *(_DWORD *)(a1 + 888) != 0 )
    {
      v1 = *(_DWORD *)(a1 + 36) - *(_DWORD *)(*(_DWORD *)(a1 + 888) + 36);
      if ( (int)((HIDWORD(v1) ^ v1) - HIDWORD(v1)) <= 4 )
      {
        v2 = *(_DWORD *)(a1 + 40) - *(_DWORD *)(*(_DWORD *)(a1 + 888) + 40);
        if ( (int)((HIDWORD(v2) ^ v2) - HIDWORD(v2)) <= 4 )
        {
          v3 = *(_DWORD *)(a1 + 36) - *(_DWORD *)(*(_DWORD *)(a1 + 888) + 36);
          if ( (int)((HIDWORD(v3) ^ v3) - HIDWORD(v3)) <= 2 )
          {
            v4 = *(_DWORD *)(a1 + 40) - *(_DWORD *)(*(_DWORD *)(a1 + 888) + 40);
            if ( (int)((HIDWORD(v4) ^ v4) - HIDWORD(v4)) <= 2 && System::__linkproc__ RandInt(3) != 0 )
              return sub_4A9020(a1);
          }
          sub_4B2900(a1, a1 + 1336, a1 + 1340);
        }
      }
    }
    if ( *(_DWORD *)(a1 + 888) != 0 )
    {
      v5 = *(_DWORD *)(a1 + 36) - *(_DWORD *)(*(_DWORD *)(a1 + 888) + 36);
      if ( (int)((HIDWORD(v5) ^ v5) - HIDWORD(v5)) < 6 )
      {
        v6 = *(_DWORD *)(a1 + 40) - *(_DWORD *)(*(_DWORD *)(a1 + 888) + 40);
        if ( (int)((HIDWORD(v6) ^ v6) - HIDWORD(v6)) < 6
          && sub_407B30() - *(_DWORD *)(a1 + 1016) > *(_DWORD *)(a1 + 1300) )
        {
          *(_DWORD *)(a1 + 1016) = sub_407B30();
          v8 = (unsigned __int8)sub_4B2C38(*(_DWORD *)(*(_DWORD *)(a1 + 888) + 40));
          sub_4AA510(a1, v8);
        }
      }
    }
  }
  return sub_4A9020(a1);
}


// ==================== [0x4AA95C] sub_4AA95C (size: 392) ====================
int __fastcall sub_4AA95C(unsigned int a1)
{
  int v1; // ecx
  __int64 v2; // rax
  __int64 v3; // rax
  int v4; // eax
  int v6; // [esp+10h] [ebp-14h]
  int v7; // [esp+18h] [ebp-Ch]
  int i; // [esp+1Ch] [ebp-8h]

  if ( *(_BYTE *)(a1 + 764) == 0
    && *(_BYTE *)(a1 + 772) == 0
    && *(_WORD *)(a1 + 106) == 0
    && ((int (*)(void))sub_407B30)() - *(_DWORD *)(a1 + 1020) > *(_DWORD *)(a1 + 1276) )
  {
    if ( *(_BYTE *)(a1 + 701) != 0 )
    {
      if ( *(_DWORD *)(*(_DWORD *)(a1 + 1032) + 8) - 1 >= 0 )
      {
        v6 = *(_DWORD *)(*(_DWORD *)(a1 + 1032) + 8);
        for ( i = 0; ; ++i )
        {
          v7 = *(_DWORD *)(Classes::TList::Get(*(Classes::TList **)(a1 + 1032), i) + 4);
          if ( *(_BYTE *)(v7 + 772) == 0
            && (unsigned __int8)unknown_libname_25(a1, v7, v1) != 0
            && (*(_BYTE *)(v7 + 836) == 0 || *(_BYTE *)(a1 + 838) != 0) )
          {
            v2 = *(_DWORD *)(a1 + 36) - *(_DWORD *)(v7 + 36);
            if ( (int)((HIDWORD(v2) ^ v2) - HIDWORD(v2)) <= 3 )
            {
              v3 = *(_DWORD *)(a1 + 40) - *(_DWORD *)(v7 + 40);
              if ( (int)((HIDWORD(v3) ^ v3) - HIDWORD(v3)) <= 3 )
                break;
            }
          }
          if ( --v6 == 0 )
            return sub_4A9020(a1);
        }
        v4 = sub_4AA8DC(a1);
        *(_DWORD *)(a1 + 1020) = sub_407B30(v4) + 1000;
      }
    }
    else if ( GetTickCount() - *(_DWORD *)(a1 + 1024) > 0x1F40
           || GetTickCount() - *(_DWORD *)(a1 + 1024) > 0x3E8 && *(_DWORD *)(a1 + 888) == 0 )
    {
      *(_DWORD *)(a1 + 1024) = GetTickCount();
      sub_4C94B4(a1);
    }
  }
  return sub_4A9020(a1);
}


// ==================== [0x4AAF98] sub_4AAF98 (size: 392) ====================
int __fastcall sub_4AAF98(unsigned int a1)
{
  int v1; // ecx
  __int64 v2; // rax
  __int64 v3; // rax
  int v4; // eax
  int v6; // [esp+10h] [ebp-14h]
  int v7; // [esp+18h] [ebp-Ch]
  int i; // [esp+1Ch] [ebp-8h]

  if ( *(_BYTE *)(a1 + 764) == 0
    && *(_BYTE *)(a1 + 772) == 0
    && *(_WORD *)(a1 + 106) == 0
    && sub_407B30(a1) - *(_DWORD *)(a1 + 1020) > *(_DWORD *)(a1 + 1276) )
  {
    if ( *(_BYTE *)(a1 + 837) != 0 )
    {
      if ( *(_DWORD *)(*(_DWORD *)(a1 + 1032) + 8) - 1 >= 0 )
      {
        v6 = *(_DWORD *)(*(_DWORD *)(a1 + 1032) + 8);
        for ( i = 0; ; ++i )
        {
          v7 = *(_DWORD *)(Classes::TList::Get(*(Classes::TList **)(a1 + 1032), i) + 4);
          if ( *(_BYTE *)(v7 + 772) == 0
            && (unsigned __int8)unknown_libname_25(a1, v7, v1) != 0
            && (*(_BYTE *)(v7 + 836) == 0 || *(_BYTE *)(a1 + 838) != 0) )
          {
            v2 = *(_DWORD *)(a1 + 36) - *(_DWORD *)(v7 + 36);
            if ( (int)((HIDWORD(v2) ^ v2) - HIDWORD(v2)) <= 2 )
            {
              v3 = *(_DWORD *)(a1 + 40) - *(_DWORD *)(v7 + 40);
              if ( (int)((HIDWORD(v3) ^ v3) - HIDWORD(v3)) <= 2 )
                break;
            }
          }
          if ( --v6 == 0 )
            return sub_4A9020(a1);
        }
        v4 = sub_4AAEF4(a1);
        *(_DWORD *)(a1 + 1020) = sub_407B30(v4) + 1000;
      }
    }
    else if ( GetTickCount() - *(_DWORD *)(a1 + 1024) > 0x1F40
           || GetTickCount() - *(_DWORD *)(a1 + 1024) > 0x3E8 && *(_DWORD *)(a1 + 888) == 0 )
    {
      *(_DWORD *)(a1 + 1024) = GetTickCount();
      sub_4C94B4(a1);
    }
  }
  return sub_4A9020(a1);
}


// ==================== [0x4AB29C] sub_4AB29C (size: 332) ====================
int __fastcall sub_4AB29C(int a1)
{
  int v1; // eax
  int v3; // [esp-14h] [ebp-48h]
  int v4; // [esp-10h] [ebp-44h]
  unsigned int v5[2]; // [esp-Ch] [ebp-40h] BYREF
  int *v6; // [esp-4h] [ebp-38h]
  int v7; // [esp+0h] [ebp-34h] BYREF
  int v8; // [esp+4h] [ebp-30h] BYREF
  _BYTE v9[4]; // [esp+8h] [ebp-2Ch] BYREF
  _BYTE v10[4]; // [esp+Ch] [ebp-28h] BYREF
  _BYTE v11[4]; // [esp+10h] [ebp-24h] BYREF
  int v12; // [esp+14h] [ebp-20h]
  void *v13; // [esp+18h] [ebp-1Ch]
  int v14; // [esp+1Ch] [ebp-18h] BYREF
  int v15; // [esp+20h] [ebp-14h] BYREF
  int v16; // [esp+24h] [ebp-10h] BYREF
  int v17; // [esp+28h] [ebp-Ch]
  int v18; // [esp+2Ch] [ebp-8h]
  int v19; // [esp+30h] [ebp-4h]
  int savedregs; // [esp+34h] [ebp+0h] BYREF

  v14 = 0;
  v19 = a1;
  System::__linkproc__ InitializeArray(&v8, `__DPdsc__'[System::String], 4, 0);
  v6 = &savedregs;
  v5[1] = (unsigned int)&loc_4AB3DD;
  v5[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v5);
  v17 = System::__linkproc__ RandInt(6) + 6;
  sub_4B2790(v19, &v16, &v15);
  unknown_libname_37(&v8, *off_4EA468);
  unknown_libname_37(v9, *(_DWORD *)off_4EA4AC);
  unknown_libname_37(v10, *off_4EA20C);
  unknown_libname_37(v11, *off_4EA020);
  if ( v17 > 0 )
  {
    v12 = v17;
    v18 = 1;
    do
    {
      if ( *(int *)(*(_DWORD *)(v19 + 1372) + 8) < 30 )
      {
        v1 = System::__linkproc__ RandInt(4);
        unknown_libname_37(&v14, *(_DWORD *)&v9[4 * v1 - 4]);
        v4 = v15;
        unknown_libname_41(v14);
        v13 = (void *)sub_4ADE3C(v3, v4);
        if ( v13 != nullptr )
          Classes::TList::Add(*(Classes::TList **)(v19 + 1372), v13);
      }
      ++v18;
      --v12;
    }
    while ( v12 != 0 );
  }
  __writefsdword(0, v5[0]);
  v6 = (int *)&loc_4AB3E4;
  unknown_libname_34(&v7);
  System::__linkproc__ FinalizeArray(&v8, `__DPdsc__'[System::String], 4, v6);
  return unknown_libname_34(&v14);
}


// ==================== [0x4AB444] sub_4AB444 (size: 623) ====================
int __fastcall sub_4AB444(unsigned int a1)
{
  int v1; // ecx
  __int64 v2; // rax
  __int64 v3; // rax
  int v4; // eax
  int v6; // [esp+18h] [ebp-14h]
  int v7; // [esp+20h] [ebp-Ch]
  int v8; // [esp+24h] [ebp-8h]
  int v9; // [esp+24h] [ebp-8h]

  if ( *(_BYTE *)(a1 + 764) == 0
    && *(_BYTE *)(a1 + 772) == 0
    && *(_WORD *)(a1 + 106) == 0
    && sub_407B30(a1) - *(_DWORD *)(a1 + 1020) > *(_DWORD *)(a1 + 1276) )
  {
    if ( *(_BYTE *)(a1 + 837) != 0 )
    {
      if ( *(_DWORD *)(*(_DWORD *)(a1 + 1032) + 8) - 1 >= 0 )
      {
        v6 = *(_DWORD *)(*(_DWORD *)(a1 + 1032) + 8);
        v8 = 0;
        while ( 1 )
        {
          v7 = *(_DWORD *)(Classes::TList::Get(*(Classes::TList **)(a1 + 1032), v8) + 4);
          if ( *(_BYTE *)(v7 + 772) == 0
            && (unsigned __int8)unknown_libname_25(a1, v7, v1) != 0
            && (*(_BYTE *)(v7 + 836) == 0 || *(_BYTE *)(a1 + 838) != 0) )
          {
            v2 = *(_DWORD *)(a1 + 36) - *(_DWORD *)(v7 + 36);
            if ( (int)((HIDWORD(v2) ^ v2) - HIDWORD(v2)) <= 2 )
            {
              v3 = *(_DWORD *)(a1 + 40) - *(_DWORD *)(v7 + 40);
              if ( (int)((HIDWORD(v3) ^ v3) - HIDWORD(v3)) <= 2 )
                break;
            }
          }
          ++v8;
          if ( --v6 == 0 )
            goto LABEL_26;
        }
        v4 = sub_4AB208(a1);
        *(_DWORD *)(a1 + 1020) = sub_407B30(v4) + 2000;
      }
    }
    else
    {
      if ( GetTickCount() - *(_DWORD *)(a1 + 1024) > 0x1F40
        || GetTickCount() - *(_DWORD *)(a1 + 1024) > 0x3E8 && *(_DWORD *)(a1 + 888) == 0 )
      {
        *(_DWORD *)(a1 + 1024) = GetTickCount();
        sub_4C94B4(a1);
      }
      if ( (double)*(int *)(a1 + 1368) > (double)*(unsigned __int16 *)(a1 + 420)
                                       / (double)*(unsigned __int16 *)(a1 + 424)
                                       * flt_4AB6B4
        && *(int *)(a1 + 1368) > 0 )
      {
        --*(_DWORD *)(a1 + 1368);
        sub_4AB29C(a1);
      }
      if ( *(_WORD *)(a1 + 420) == *(_WORD *)(a1 + 424) )
        *(_DWORD *)(a1 + 1368) = 5;
    }
LABEL_26:
    if ( *(_DWORD *)(*(_DWORD *)(a1 + 1372) + 8) - 1 >= 0 )
    {
      v9 = *(_DWORD *)(*(_DWORD *)(a1 + 1372) + 8) - 1;
      do
      {
        if ( *(_BYTE *)(Classes::TList::Get(*(Classes::TList **)(a1 + 1372), v9) + 772) != 0
          || *(_BYTE *)(Classes::TList::Get(*(Classes::TList **)(a1 + 1372), v9) + 764) != 0 )
        {
          Classes::TList::Delete(*(Classes::TList **)(a1 + 1372), v9);
        }
        --v9;
      }
      while ( v9 != -1 );
    }
  }
  return sub_4A9020(a1);
}


// ==================== [0x4ABBD0] sub_4ABBD0 (size: 406) ====================
int __fastcall sub_4ABBD0(int a1)
{
  bool v2; // [esp+3h] [ebp-9h]
  void *v3; // [esp+4h] [ebp-8h]

  if ( *(_BYTE *)(a1 + 1376) != 0 )
  {
    *(_BYTE *)(a1 + 1376) = 0;
    *(_BYTE *)(a1 + 701) = 0;
    sub_4B8EBC(0, 0, *(_DWORD *)(a1 + 40), *(_DWORD *)(a1 + 36));
    sub_4ABB08(a1);
  }
  if ( *(_BYTE *)(a1 + 772) != 0 )
  {
    if ( GetTickCount() - *(_DWORD *)(a1 + 776) > 0x7D0 )
      sub_4BB300(a1);
  }
  else
  {
    v2 = *(_DWORD *)(a1 + 888) == 0;
    if ( *(_DWORD *)(a1 + 648) != 0
      && (*(_DWORD *)(*(_DWORD *)(a1 + 648) + 888) != 0 || *(_DWORD *)(*(_DWORD *)(a1 + 648) + 896) != 0) )
    {
      v2 = false;
    }
    if ( v2 )
    {
      if ( GetTickCount() - *(_DWORD *)(a1 + 1380) > 0xEA60 )
      {
        v3 = Mxarrays::TStringArray::Find(a1, (int)&str_Dogz[1], a1);
        if ( v3 != nullptr )
        {
          sub_4B8EBC(0, 0, *(_DWORD *)(a1 + 40), *(_DWORD *)(a1 + 36));
          sub_4B8EBC(0, 0, v3, a1);
          if ( (unsigned __int8)System::__linkproc__ IsClass(v3, off_4A8A9C) != 0 )
            sub_4AB908(v3);
          *(_DWORD *)(a1 + 648) = 0;
          sub_4BA6A8(a1);
        }
      }
    }
    else
    {
      *(_DWORD *)(a1 + 1380) = GetTickCount();
    }
  }
  return sub_4A9720(a1);
}


// ==================== [0x4ABE08] @Iwdatamodulepool@TObjectPool@$bctr$qqrv (size: 491) ====================
Iwdatamodulepool::TObjectPool *__fastcall Iwdatamodulepool::TObjectPool::TObjectPool(
        Iwdatamodulepool::TObjectPool *this,
        char a2)
{
  char v3; // [esp+3h] [ebp-5h]
  Iwdatamodulepool::TObjectPool *v4; // [esp+4h] [ebp-4h]

  if ( a2 != 0 )
    this = (Iwdatamodulepool::TObjectPool *)unknown_libname_26();
  v3 = a2;
  v4 = this;
  *((_DWORD *)this + 2) = unknown_libname_22(off_410028, 1);
  *((_DWORD *)v4 + 3) = unknown_libname_22(off_410028, 1);
  *((_DWORD *)v4 + 4) = unknown_libname_22(off_40FAFC, 1);
  *((_DWORD *)v4 + 5) = unknown_libname_22(off_40FAFC, 1);
  *((_DWORD *)v4 + 23) = unknown_libname_22(off_40FAFC, 1);
  *((_DWORD *)v4 + 22) = unknown_libname_22(off_40FAFC, 1);
  *((_DWORD *)v4 + 1) = unknown_libname_22(off_410028, 1);
  *((_DWORD *)v4 + 21) = unknown_libname_22(off_40FAFC, 1);
  *((_DWORD *)v4 + 24) = unknown_libname_22(off_40FAFC, 1);
  *((_DWORD *)v4 + 25) = unknown_libname_22(off_410028, 1);
  *((_DWORD *)v4 + 26) = unknown_libname_22(off_40FAFC, 1);
  *((_DWORD *)v4 + 27) = unknown_libname_22(off_40FAFC, 1);
  *((_DWORD *)v4 + 28) = unknown_libname_22(off_40FAFC, 1);
  *((_DWORD *)v4 + 29) = unknown_libname_22(off_40FAFC, 1);
  *((_DWORD *)v4 + 30) = unknown_libname_22(off_40FAFC, 1);
  *((_DWORD *)v4 + 6) = GetTickCount();
  *((_DWORD *)v4 + 7) = GetTickCount();
  *((_DWORD *)v4 + 8) = GetTickCount();
  *((_DWORD *)v4 + 9) = GetTickCount();
  *((_DWORD *)v4 + 10) = GetTickCount();
  *((_DWORD *)v4 + 12) = GetTickCount();
  *((_DWORD *)v4 + 14) = 0;
  *((_DWORD *)v4 + 15) = 0;
  *((_DWORD *)v4 + 16) = 0;
  *((_DWORD *)v4 + 17) = 0;
  *((_DWORD *)v4 + 18) = 0;
  *((_DWORD *)v4 + 19) = 0;
  *((_DWORD *)v4 + 20) = 0;
  *((_BYTE *)v4 + 140) = 0;
  *((_DWORD *)v4 + 36) = 1800000;
  *((_DWORD *)v4 + 13) = GetTickCount();
  unknown_libname_22(v4, 0);
  if ( v3 != 0 )
    unknown_libname_27(v4);
  return v4;
}


// ==================== [0x4ABFF4] sub_4ABFF4 (size: 439) ====================
int __fastcall sub_4ABFF4(int a1)
{
  __int64 v1; // rax
  int v2; // eax
  System::TObject *v3; // eax
  int v4; // eax
  int v5; // eax
  int v6; // eax
  int v7; // edx
  int result; // eax
  int v9; // [esp+0h] [ebp-10h]
  int v10; // [esp+0h] [ebp-10h]
  int v11; // [esp+0h] [ebp-10h]
  int v12; // [esp+0h] [ebp-10h]
  int v13; // [esp+4h] [ebp-Ch]
  int v14; // [esp+4h] [ebp-Ch]
  int v15; // [esp+4h] [ebp-Ch]
  int v16; // [esp+4h] [ebp-Ch]
  char v17; // [esp+Bh] [ebp-5h]
  System::TObject **v18; // [esp+Ch] [ebp-4h]

  v1 = unknown_libname_28(a1);
  v17 = BYTE4(v1);
  v18 = (System::TObject **)v1;
  v2 = (*(int (__fastcall **)(_DWORD))(**(_DWORD **)(v1 + 8) + 20))(*(_DWORD *)(v1 + 8)) - 1;
  if ( v2 >= 0 )
  {
    v9 = v2 + 1;
    v13 = 0;
    do
    {
      v3 = (System::TObject *)(*(int (__fastcall **)(System::TObject *, int))(*(_DWORD *)v18[2] + 24))(v18[2], v13);
      System::TObject::Free(v3);
      ++v13;
      --v9;
    }
    while ( v9 != 0 );
  }
  System::TObject::Free(v18[2]);
  System::TObject::Free(v18[3]);
  System::TObject::Free(v18[4]);
  System::TObject::Free(v18[5]);
  if ( *((_DWORD *)v18[23] + 2) - 1 >= 0 )
  {
    v10 = *((_DWORD *)v18[23] + 2);
    v14 = 0;
    do
    {
      v4 = Classes::TList::Get(v18[23], v14);
      System::__linkproc__ FreeMem(v4, 68);
      ++v14;
      --v10;
    }
    while ( v10 != 0 );
  }
  System::TObject::Free(v18[23]);
  System::TObject::Free(v18[22]);
  if ( *((_DWORD *)v18[24] + 2) - 1 >= 0 )
  {
    v11 = *((_DWORD *)v18[24] + 2);
    v15 = 0;
    do
    {
      v5 = Classes::TList::Get(v18[24], v15);
      System::__linkproc__ FreeMem(v5, 76);
      ++v15;
      --v11;
    }
    while ( v11 != 0 );
  }
  System::TObject::Free(v18[24]);
  System::TObject::Free(v18[1]);
  if ( *((_DWORD *)v18[21] + 2) - 1 >= 0 )
  {
    v12 = *((_DWORD *)v18[21] + 2);
    v16 = 0;
    do
    {
      v6 = Classes::TList::Get(v18[21], v16);
      System::__linkproc__ FreeMem(v6, 44);
      ++v16;
      --v12;
    }
    while ( v12 != 0 );
  }
  System::TObject::Free(v18[21]);
  System::TObject::Free(v18[25]);
  System::TObject::Free(v18[26]);
  System::TObject::Free(v18[27]);
  System::TObject::Free(v18[28]);
  System::TObject::Free(v18[29]);
  System::TObject::Free(v18[30]);
  LOBYTE(v7) = v17 & 0xFC;
  result = sub_402EF0(v18, v7);
  if ( v17 > 0 )
    return System::__linkproc__ ClassDestroy(v18);
  return result;
}


// ==================== [0x4AC518] sub_4AC518 (size: 382) ====================
int __fastcall sub_4AC518(int a1, int a2, int a3, int a4, __int16 a5, __int16 a6, int a7, int a8, int a9, int a10)
{
  unsigned int v11[2]; // [esp-Ch] [ebp-4Ch] BYREF
  int *v12; // [esp-4h] [ebp-44h]
  int v13; // [esp+0h] [ebp-40h]
  int v14; // [esp+4h] [ebp-3Ch]
  int v15; // [esp+8h] [ebp-38h]
  char v16; // [esp+Fh] [ebp-31h]
  int v17; // [esp+10h] [ebp-30h] BYREF
  int v18; // [esp+14h] [ebp-2Ch]
  int v19; // [esp+18h] [ebp-28h]
  int v20; // [esp+1Ch] [ebp-24h]
  int v21; // [esp+20h] [ebp-20h]
  int v22; // [esp+24h] [ebp-1Ch]
  int v23; // [esp+28h] [ebp-18h]
  int v24; // [esp+2Ch] [ebp-14h]
  int v25; // [esp+30h] [ebp-10h]
  int v26; // [esp+34h] [ebp-Ch]
  int v27; // [esp+38h] [ebp-8h]
  int v28; // [esp+3Ch] [ebp-4h]
  int savedregs; // [esp+40h] [ebp+0h] BYREF

  v26 = a3;
  v27 = a2;
  v28 = a1;
  unknown_libname_44(a10);
  v12 = &savedregs;
  v11[1] = (unsigned int)&loc_4AC689;
  v11[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v11);
  v22 = v26 - 12;
  v20 = v26 + 12;
  v21 = a4 - 12;
  v19 = a4 + 12;
  if ( !__OFSUB__(v26 + 12, v26 - 12) )
  {
    v15 = 25;
    v25 = v26 - 12;
    do
    {
      if ( v19 >= v21 )
      {
        v14 = v19 - v21 + 1;
        v24 = v21;
        do
        {
          v16 = sub_4B57D8(&v17);
          if ( v16 != 0 && *(_DWORD *)(v17 + 4) != 0 && *(_DWORD *)(*(_DWORD *)(v17 + 4) + 8) - 1 >= 0 )
          {
            v13 = *(_DWORD *)(*(_DWORD *)(v17 + 4) + 8);
            v23 = 0;
            do
            {
              if ( Classes::TList::Get(*(Classes::TList **)(v17 + 4), v23) != 0
                && *(_BYTE *)Classes::TList::Get(*(Classes::TList **)(v17 + 4), v23) == 1 )
              {
                v18 = *(_DWORD *)(Classes::TList::Get(*(Classes::TList **)(v17 + 4), v23) + 4);
                if ( v18 != 0 && *(_BYTE *)(v18 + 764) == 0 && *(_BYTE *)(v18 + 570) == 0 )
                  sub_4B865C(a10, a9, a8, a7, a6);
              }
              ++v23;
              --v13;
            }
            while ( v13 != 0 );
          }
          ++v24;
          --v14;
        }
        while ( v14 != 0 );
      }
      ++v25;
      --v15;
    }
    while ( v15 != 0 );
  }
  __writefsdword(0, v11[0]);
  v12 = (int *)&loc_4AC690;
  return unknown_libname_34(&a10);
}


// ==================== [0x4AC96C] sub_4AC96C (size: 368) ====================
int __fastcall sub_4AC96C(int a1)
{
  int v1; // eax
  int v2; // eax
  int v3; // ecx
  System::TObject *v4; // edx
  int v5; // ecx
  int v6; // ecx
  unsigned int v8[2]; // [esp-Ch] [ebp-90h] BYREF
  int *v9; // [esp-4h] [ebp-88h]
  _BYTE v10[60]; // [esp+4h] [ebp-80h] BYREF
  int v11; // [esp+40h] [ebp-44h] BYREF
  _BYTE v12[44]; // [esp+44h] [ebp-40h] BYREF
  int v13; // [esp+70h] [ebp-14h] BYREF
  int v14; // [esp+74h] [ebp-10h]
  System::TObject *v15; // [esp+78h] [ebp-Ch]
  int i; // [esp+7Ch] [ebp-8h]
  int v17; // [esp+80h] [ebp-4h]
  int savedregs; // [esp+84h] [ebp+0h] BYREF

  v11 = 0;
  v13 = 0;
  v14 = 0;
  v17 = a1;
  v9 = &savedregs;
  v8[1] = (unsigned int)&loc_4ACAD0;
  v8[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v8);
  v1 = *(_DWORD *)(*(_DWORD *)(a1 + 104) + 8) - 1;
  if ( v1 >= 0 )
  {
    for ( i = v1; i != -1; --i )
    {
      v15 = (System::TObject *)Classes::TList::Get(*(Classes::TList **)(v17 + 104), i);
      unknown_libname_41(v8[0]);
      v2 = sub_4B7350(*off_4EA22C, v14);
      v4 = v15;
      *((_DWORD *)v15 + 190) = v2;
      if ( *((_DWORD *)v15 + 190) != 0 )
      {
        unknown_libname_25(v15, v4, v3);
        if ( *((_BYTE *)v15 + 789) != 0 )
        {
          unknown_libname_16(v12, aMerchantInital);
          LOBYTE(v5) = 41;
          System::__linkproc__ PStrNCat(v12, (char *)v15 + 21, v5);
          unknown_libname_41(v8[0]);
          sub_4E4274(v13);
          System::TObject::Free(v15);
          Classes::TList::Delete(*(Classes::TList **)(v17 + 104), i);
        }
        else
        {
          sub_49EF7C(v15);
          sub_49F044(v15);
        }
      }
      else
      {
        unknown_libname_16(v10, &aMerchantInital[28]);
        LOBYTE(v6) = 56;
        System::__linkproc__ PStrNCat(v10, (char *)v15 + 21, v6);
        unknown_libname_41(v8[0]);
        sub_4E4274(v11);
        System::TObject::Free(v15);
        Classes::TList::Delete(*(Classes::TList **)(v17 + 104), i);
      }
    }
  }
  __writefsdword(0, v8[0]);
  v9 = (int *)&loc_4ACAD7;
  unknown_libname_34(&v11);
  return unknown_libname_35(&v13, 2);
}


// ==================== [0x4ACC24] sub_4ACC24 (size: 372) ====================
int __fastcall sub_4ACC24(int a1)
{
  int v1; // eax
  int v2; // eax
  int v3; // ecx
  System::TObject *v4; // edx
  int v5; // ecx
  int v6; // ecx
  unsigned int v8[2]; // [esp-Ch] [ebp-88h] BYREF
  int *v9; // [esp-4h] [ebp-80h]
  _BYTE v10[56]; // [esp+4h] [ebp-78h] BYREF
  int v11; // [esp+3Ch] [ebp-40h] BYREF
  _BYTE v12[40]; // [esp+40h] [ebp-3Ch] BYREF
  int v13; // [esp+68h] [ebp-14h] BYREF
  int v14; // [esp+6Ch] [ebp-10h]
  System::TObject *v15; // [esp+70h] [ebp-Ch]
  int i; // [esp+74h] [ebp-8h]
  int v17; // [esp+78h] [ebp-4h]
  int savedregs; // [esp+7Ch] [ebp+0h] BYREF

  v11 = 0;
  v13 = 0;
  v14 = 0;
  v17 = a1;
  v9 = &savedregs;
  v8[1] = (unsigned int)&loc_4ACD8C;
  v8[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v8);
  v1 = *(_DWORD *)(*(_DWORD *)(a1 + 108) + 8) - 1;
  if ( v1 >= 0 )
  {
    for ( i = v1; i != -1; --i )
    {
      v15 = (System::TObject *)Classes::TList::Get(*(Classes::TList **)(v17 + 108), i);
      unknown_libname_41(v8[0]);
      v2 = sub_4B7350(*off_4EA22C, v14);
      v4 = v15;
      *((_DWORD *)v15 + 190) = v2;
      if ( *((_DWORD *)v15 + 190) != 0 )
      {
        unknown_libname_25(v15, v4, v3);
        if ( *((_BYTE *)v15 + 789) == 0 || *((_BYTE *)v15 + 1372) != 0 )
        {
          sub_49EAF0(v15);
        }
        else
        {
          unknown_libname_16(v12, &unk_4ACD98);
          LOBYTE(v5) = 36;
          System::__linkproc__ PStrNCat(v12, (char *)v15 + 21, v5);
          unknown_libname_41(v8[0]);
          sub_4E4274(v13);
          System::TObject::Free(v15);
          Classes::TList::Delete(*(Classes::TList **)(v17 + 108), i);
        }
      }
      else
      {
        unknown_libname_16(v10, dword_4ACDB0);
        LOBYTE(v6) = 53;
        System::__linkproc__ PStrNCat(v10, (char *)v15 + 21, v6);
        unknown_libname_41(v8[0]);
        sub_4E4274(v11);
        System::TObject::Free(v15);
        Classes::TList::Delete(*(Classes::TList **)(v17 + 108), i);
      }
    }
  }
  __writefsdword(0, v8[0]);
  v9 = (int *)&loc_4ACD93;
  unknown_libname_34(&v11);
  return unknown_libname_35(&v13, 2);
}


// ==================== [0x4ACE94] @Indnsresolver@TInDNSResolver@ParseAnswers$qqrp22Indnscommon@TDNSHeader25System@%DynamicArray$tuc%uio (size: 563) ====================
int __fastcall Indnsresolver::TInDNSResolver::ParseAnswers(int a1, int a2, int a3)
{
  int v3; // edx
  int v4; // eax
  __int16 v5; // ax
  int v6; // edx
  int v7; // eax
  __int16 v8; // ax
  int v9; // edx
  int v10; // eax
  __int16 v11; // ax
  int v12; // edx
  int v13; // eax
  __int16 v14; // ax
  int v15; // edx
  int v16; // eax
  __int16 v17; // ax
  unsigned int v19[2]; // [esp-Ch] [ebp-2Ch] BYREF
  int *v20; // [esp-4h] [ebp-24h]
  int v21; // [esp+4h] [ebp-1Ch] BYREF
  int v22; // [esp+8h] [ebp-18h]
  int v23; // [esp+Ch] [ebp-14h]
  int v24; // [esp+10h] [ebp-10h]
  int v25; // [esp+14h] [ebp-Ch] BYREF
  int v26; // [esp+18h] [ebp-8h]
  int v27; // [esp+1Ch] [ebp-4h]
  int savedregs; // [esp+20h] [ebp+0h] BYREF

  v21 = 0;
  v25 = a3;
  v26 = a2;
  v27 = a1;
  unknown_libname_44(a3);
  v20 = &savedregs;
  v19[1] = (unsigned int)&loc_4AD0BB;
  v19[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v19);
  if ( *(_DWORD *)(*(_DWORD *)(v27 + 88) + 8) - 1 >= 0 )
  {
    v22 = *(_DWORD *)(*(_DWORD *)(v27 + 88) + 8);
    v24 = 0;
    while ( 1 )
    {
      Classes::TList::Get(*(Classes::TList **)(v27 + 88), v24);
      unknown_libname_41(v19[0]);
      if ( unknown_libname_84(v21, v25) == 0 )
        break;
      ++v24;
      if ( --v22 == 0 )
        goto LABEL_6;
    }
    v23 = Classes::TList::Get(*(Classes::TList **)(v27 + 88), v24);
    *(_BYTE *)(v26 + 570) = *(_BYTE *)(v23 + 15);
    *(_BYTE *)(v26 + 571) = *(_BYTE *)(v23 + 16);
    *(_WORD *)(v26 + 568) = *(_WORD *)(v23 + 18);
    *(_BYTE *)(v26 + 52) = *(_BYTE *)(v23 + 20);
    *(_BYTE *)(v26 + 623) = *(_BYTE *)(v23 + 21);
    *(_BYTE *)(v26 + 624) = *(_BYTE *)(v23 + 22);
    *(_DWORD *)(v26 + 404) = *(unsigned __int16 *)(v23 + 24);
    *(_WORD *)(v26 + 64) = *(_WORD *)(v23 + 26);
    *(_WORD *)(v26 + 68) = *(_WORD *)(v23 + 26);
    *(_WORD *)(v26 + 66) = *(_WORD *)(v23 + 28);
    v3 = v26;
    *(_WORD *)(v26 + 70) = *(_WORD *)(v23 + 28);
    LOBYTE(v3) = *(_BYTE *)(v23 + 30);
    v4 = v23;
    LOBYTE(v4) = v3;
    v5 = unknown_libname_77(v4, v3);
    v6 = v26;
    *(_WORD *)(v26 + 54) = v5;
    LOBYTE(v6) = *(_BYTE *)(v23 + 31);
    v7 = v23;
    LOBYTE(v7) = v6;
    v8 = unknown_libname_77(v7, v6);
    v9 = v26;
    *(_WORD *)(v26 + 56) = v8;
    LOBYTE(v9) = *(_BYTE *)(v23 + 33);
    v10 = v23;
    LOBYTE(v10) = *(_BYTE *)(v23 + 32);
    v11 = unknown_libname_77(v10, v9);
    v12 = v26;
    *(_WORD *)(v26 + 58) = v11;
    LOBYTE(v12) = *(_BYTE *)(v23 + 34);
    v13 = v23;
    LOBYTE(v13) = v12;
    v14 = unknown_libname_77(v13, v12);
    v15 = v26;
    *(_WORD *)(v26 + 60) = v14;
    LOBYTE(v15) = *(_BYTE *)(v23 + 35);
    v16 = v23;
    LOBYTE(v16) = v15;
    v17 = unknown_libname_77(v16, v15);
    *(_WORD *)(v26 + 62) = v17;
    *(_BYTE *)(v26 + 620) = *(_BYTE *)(v23 + 36);
    *(_BYTE *)(v26 + 572) = *(_BYTE *)(v23 + 37);
    *(_DWORD *)(v26 + 1276) = *(unsigned __int16 *)(v23 + 38);
    *(_DWORD *)(v26 + 1280) = *(unsigned __int16 *)(v23 + 40);
    *(_DWORD *)(v26 + 1288) = *(unsigned __int16 *)(v23 + 42);
    *(_DWORD *)(v26 + 1300) = *(unsigned __int16 *)(v23 + 44);
  }
LABEL_6:
  __writefsdword(0, v19[0]);
  v20 = (int *)&loc_4AD0C2;
  unknown_libname_34(&v21);
  return unknown_libname_34(&v25);
}


// ==================== [0x4AD2E8] sub_4AD2E8 (size: 623) ====================
int __fastcall sub_4AD2E8(int a1, int a2)
{
  int v2; // eax
  int v3; // eax
  int v4; // eax
  __int16 v5; // ax
  int v7; // [esp-10h] [ebp-50h]
  unsigned int v8[2]; // [esp-Ch] [ebp-4Ch] BYREF
  int *v9; // [esp-4h] [ebp-44h]
  int v10; // [esp+0h] [ebp-40h]
  int v11; // [esp+4h] [ebp-3Ch]
  int v12; // [esp+8h] [ebp-38h] BYREF
  int v13; // [esp+Ch] [ebp-34h]
  int v14; // [esp+10h] [ebp-30h]
  int v15; // [esp+18h] [ebp-28h]
  int v16; // [esp+1Ch] [ebp-24h]
  void *Mem; // [esp+20h] [ebp-20h]
  int *v18; // [esp+24h] [ebp-1Ch]
  int v19; // [esp+28h] [ebp-18h] BYREF
  Classes::TList *v20; // [esp+2Ch] [ebp-14h]
  int v21; // [esp+30h] [ebp-10h]
  int v22; // [esp+34h] [ebp-Ch]
  int v23; // [esp+38h] [ebp-8h]
  int v24; // [esp+3Ch] [ebp-4h]
  int savedregs; // [esp+40h] [ebp+0h] BYREF

  v12 = 0;
  v14 = 0;
  v13 = 0;
  v19 = 0;
  v23 = a2;
  v24 = a1;
  v9 = &savedregs;
  v8[1] = (unsigned int)&loc_4AD549;
  v8[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v8);
  v20 = nullptr;
  v2 = *(_DWORD *)(*(_DWORD *)(a1 + 88) + 8) - 1;
  if ( v2 >= 0 )
  {
    v15 = v2 + 1;
    v21 = 0;
    while ( 1 )
    {
      Classes::TList::Get(*(Classes::TList **)(v24 + 88), v21);
      unknown_libname_41(v8[0]);
      unknown_libname_41(v14);
      if ( unknown_libname_84(v7, v13) == 0 )
        break;
      ++v21;
      if ( --v15 == 0 )
        goto LABEL_6;
    }
    v20 = *(Classes::TList **)(Classes::TList::Get(*(Classes::TList **)(v24 + 88), v21) + 48);
  }
LABEL_6:
  if ( v20 != nullptr && *((_DWORD *)v20 + 2) - 1 >= 0 )
  {
    v15 = *((_DWORD *)v20 + 2);
    v21 = 0;
    do
    {
      v18 = (int *)Classes::TList::Get(v20, v21);
      v3 = System::__linkproc__ RandInt(v18[1]);
      if ( v3 <= *v18 )
      {
        unknown_libname_41(v8[0]);
        if ( unknown_libname_84(v12, &str_Gold_1[1]) != 0 )
        {
          unknown_libname_34(&v19);
          if ( v19 == 0 )
            unknown_libname_41(v8[0]);
          Mem = (void *)System::__linkproc__ GetMem(24);
          if ( (unsigned __int8)sub_4AC404(v24, v19, Mem) != 0 )
          {
            v11 = System::__linkproc__ RandInt(80) + 20;
            v10 = *((unsigned __int16 *)Mem + 4);
            v5 = System::__linkproc__ ROUND((double)v11 * ((double)v10 / flt_4AD568));
            *((_WORD *)Mem + 3) = v5;
            v16 = sub_4AC2F8(v24, *((unsigned __int16 *)Mem + 2));
            if ( System::__linkproc__ RandInt(15) == 0 )
              sub_4AD0C8(v24, Mem);
            if ( (*(_BYTE *)(v16 + 15) == 15
               || (unsigned __int8)(*(_BYTE *)(v16 + 15) - 19) < 6u
               || *(_BYTE *)(v16 + 15) == 26)
              && (*(_BYTE *)(v16 + 16) == 0x82 || *(_BYTE *)(v16 + 16) == 0x83 || *(_BYTE *)(v16 + 16) == 0x84) )
            {
              sub_4AD1D4(*off_4EA490, Mem);
            }
            Classes::TList::Add(*(Classes::TList **)(v23 + 1036), Mem);
          }
          else
          {
            System::__linkproc__ FreeMem(Mem, 24);
          }
        }
        else
        {
          v4 = System::__linkproc__ RandInt(v18[6]);
          *(_DWORD *)(v23 + 48) += v18[6] / 2 + v4;
        }
      }
      ++v21;
      --v15;
    }
    while ( v15 != 0 );
  }
  v22 = 1;
  __writefsdword(0, v8[0]);
  v9 = (int *)&loc_4AD550;
  unknown_libname_35(&v12, 3);
  unknown_libname_34(&v19);
  return v22;
}


// ==================== [0x4AD56C] sub_4AD56C (size: 2256) ====================
System::TObject *__fastcall sub_4AD56C(int a1, int a2, int a3, int a4, char a5, int a6)
{
  int v6; // edx
  int v7; // edx
  int v8; // eax
  int v9; // eax
  int v10; // eax
  int v11; // ecx
  int v12; // ecx
  int v13; // edx
  int v14; // ecx
  unsigned int v16[2]; // [esp-Ch] [ebp-140h] BYREF
  int *v17; // [esp-4h] [ebp-138h]
  _BYTE v18[256]; // [esp+Ch] [ebp-128h] BYREF
  int v19; // [esp+10Ch] [ebp-28h]
  int v20; // [esp+110h] [ebp-24h]
  int v21; // [esp+114h] [ebp-20h]
  int v22; // [esp+118h] [ebp-1Ch]
  System::TObject *v23; // [esp+11Ch] [ebp-18h]
  int v24; // [esp+120h] [ebp-14h]
  System::TObject *v25; // [esp+124h] [ebp-10h]
  int v26; // [esp+128h] [ebp-Ch]
  int v27; // [esp+12Ch] [ebp-8h] BYREF
  int v28; // [esp+130h] [ebp-4h]
  int savedregs; // [esp+134h] [ebp+0h] BYREF

  v26 = a3;
  v27 = a2;
  v28 = a1;
  unknown_libname_44(a2);
  unknown_libname_44(a6);
  v17 = &savedregs;
  v16[1] = (unsigned int)&loc_4ADE29;
  v16[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v16);
  v25 = nullptr;
  v23 = nullptr;
  v24 = sub_4B7350(*off_4EA22C, v27);
  if ( v24 != 0 )
  {
    switch ( a5 )
    {
      case 11:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_4976B0(off_497648, v6);
        break;
      case 20:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_4A6E14(off_4A5020, v6);
        break;
      case 51:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_4A8B74((int *)off_4A82C8, v6);
        *((_BYTE *)v23 + 699) = 1;
        *((_DWORD *)v23 + 177) = System::__linkproc__ RandInt(3500) + 3000;
        *((_DWORD *)v23 + 178) = 50;
        break;
      case 52:
        if ( System::__linkproc__ RandInt(30) != 0 )
        {
          LOBYTE(v7) = 1;
          v23 = (System::TObject *)sub_4A8B74((int *)off_4A82C8, v7);
          *((_BYTE *)v23 + 699) = 1;
          v9 = System::__linkproc__ RandInt(8000);
          *((_DWORD *)v23 + 177) = v9 + 8000;
        }
        else
        {
          v23 = (System::TObject *)Tecanvas::TDarkGrayPen::TDarkGrayPen(off_4A8334);
          *((_BYTE *)v23 + 699) = 1;
          v8 = System::__linkproc__ RandInt(20000);
          *((_DWORD *)v23 + 177) = v8 + 10000;
        }
        *((_DWORD *)v23 + 178) = 150;
        break;
      case 53:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_4A9690(off_4A8398, v6);
        *((_BYTE *)v23 + 699) = 1;
        v10 = System::__linkproc__ RandInt(8000);
        *((_DWORD *)v23 + 177) = v10 + 8000;
        *((_DWORD *)v23 + 178) = 150;
        break;
      case 55:
        v23 = (System::TObject *)Tecanvas::TTeeCanvas3D::TTeeCanvas3D(off_49A964);
        *((_BYTE *)v23 + 570) = 55;
        break;
      case 80:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_4A8B74((int *)off_4A82C8, v6);
        break;
      case 81:
      case 86:
      case 88:
      case 89:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_4A9690(off_4A8398, v6);
        break;
      case 82:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_4A983C(off_4A84AC, v6);
        break;
      case 83:
        v23 = (System::TObject *)Inthreadsafe::TInThreadSafeList::TInThreadSafeList(off_4A83F8);
        break;
      case 84:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_4A97F0(off_4A8454, v6);
        break;
      case 85:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_4A51C0((int *)off_4A4CB0, v6);
        break;
      case 87:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_4598F0(off_4597C8, v6);
        break;
      case 90:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_4A9C14(off_4A85C8, v6);
        break;
      case 91:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_4A9F10(off_4A868C, v6);
        break;
      case 92:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_4AA160((int *)off_4A86F0, v6);
        break;
      case 93:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_459EE4(off_459834, v6);
        break;
      case 94:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_4AA4B4(off_4A8760, v6);
        break;
      case 95:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_4AA848(off_4A87C4, v6);
        if ( System::__linkproc__ RandInt(2) == 0 )
          *((_BYTE *)v23 + 698) = 1;
        break;
      case 96:
        v23 = (System::TObject *)Inhttpheaderinfo::TIdResponseHeaderInfo::TIdResponseHeaderInfo(off_4A8828);
        if ( System::__linkproc__ RandInt(4) == 0 )
          *((_BYTE *)v23 + 698) = 1;
        break;
      case 97:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_4A9EB4(off_4A8634, v6);
        if ( System::__linkproc__ RandInt(2) == 0 )
          *((_BYTE *)v23 + 698) = 1;
        break;
      case 100:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_4AACCC(off_4A8890, v6);
        break;
      case 101:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_4AAE20(off_4A88FC, v6);
        *((_BYTE *)v23 + 698) = 1;
        break;
      case 102:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_4AB120(off_4A8964, v6);
        break;
      case 103:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_4A5750(off_4A4D30, v6);
        break;
      case 104:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_459F3C(off_459894, v6);
        break;
      case 105:
        v23 = (System::TObject *)Tecanvas::TDarkGrayPen::TDarkGrayPen(off_4A89D4);
        break;
      case 106:
        v23 = (System::TObject *)Tecanvas::TDarkGrayPen::TDarkGrayPen(off_4A8A40);
        break;
      case 107:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_4A5A8C((int *)off_4A4D94, v6);
        break;
      case 108:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_4A9690(off_4A8398, v6);
        if ( System::__linkproc__ RandInt(2) == 0 )
          *((_BYTE *)v23 + 698) = 1;
        break;
      case 110:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_4A6E60((int *)off_4A507C, v6);
        break;
      case 111:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_4A73D4((int *)off_4A50E8, v6);
        break;
      case 112:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_4A6AB4(off_4A4FBC, v6);
        break;
      case 113:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_4AB844(off_4A8A9C, v6);
        break;
      case 114:
        v23 = (System::TObject *)Tecanvas::TDarkGrayPen::TDarkGrayPen(off_4A8B04);
        break;
      case 115:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_4A5F94(off_4A4E0C, v6);
        break;
      case 116:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_4A61D0(off_4A4E78, v6);
        break;
      case 117:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_4A6538(off_4A4EE8, v6);
        break;
      case 118:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_4A9B64(off_4A850C, v6);
        break;
      case 119:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_4A9BBC(off_4A8568, v6);
        break;
      case 120:
        LOBYTE(v6) = 1;
        v23 = (System::TObject *)sub_4A764C(off_4A5158, v6);
        break;
      default:
        break;
    }
    if ( v23 != nullptr )
    {
      Indnsresolver::TInDNSResolver::ParseAnswers(v28, (int)v23, a6);
      *((_DWORD *)v23 + 190) = v24;
      System::__linkproc__ LStrToString(v18, v27, 255);
      LOBYTE(v11) = 16;
      System::__linkproc__ PStrNCpy((char *)v23 + 4, v18, v11);
      *((_DWORD *)v23 + 9) = v26;
      *((_DWORD *)v23 + 10) = a4;
      *((_BYTE *)v23 + 44) = System::__linkproc__ RandInt(8);
      System::__linkproc__ LStrToString(v18, a6, 255);
      LOBYTE(v12) = 14;
      System::__linkproc__ PStrNCpy((char *)v23 + 21, v18, v12);
      qmemcpy((char *)v23 + 408, (char *)v23 + 52, 0x28u);
      if ( System::__linkproc__ RandInt(100) < *((unsigned __int8 *)v23 + 624) )
        *((_BYTE *)v23 + 838) = 1;
      sub_4AD2E8(v28, (int)v23);
      unknown_libname_25(v23, v13, v14);
      if ( *((_BYTE *)v23 + 789) != 0 )
      {
        v19 = 0;
        if ( *(int *)(*((_DWORD *)v23 + 190) + 28) >= 50 )
          v21 = 3;
        else
          v21 = 2;
        if ( *(int *)(*((_DWORD *)v23 + 190) + 32) >= 250 )
        {
          v20 = 50;
        }
        else if ( *(int *)(*((_DWORD *)v23 + 190) + 32) >= 30 )
        {
          v20 = 20;
        }
        else
        {
          v20 = 2;
        }
        v22 = 0;
        while ( (unsigned __int8)sub_4B5ED0(0) == 0 )
        {
          if ( *(_DWORD *)(*((_DWORD *)v23 + 190) + 28) - v20 - 1 <= *((_DWORD *)v23 + 9) )
          {
            *((_DWORD *)v23 + 9) = v20 + System::__linkproc__ RandInt(*(_DWORD *)(*((_DWORD *)v23 + 190) + 28) / 2);
            if ( *(_DWORD *)(*((_DWORD *)v23 + 190) + 32) - v20 - 1 <= *((_DWORD *)v23 + 10) )
              *((_DWORD *)v23 + 10) = v20 + System::__linkproc__ RandInt(*(_DWORD *)(*((_DWORD *)v23 + 190) + 32) / 2);
            else
              *((_DWORD *)v23 + 10) += v21;
          }
          else
          {
            *((_DWORD *)v23 + 9) += v21;
          }
          if ( ++v22 == 31 )
            goto LABEL_72;
        }
        v19 = sub_4B63E4(v23, 1);
LABEL_72:
        if ( v19 == 0 )
        {
          System::TObject::Free(v23);
          v23 = nullptr;
        }
      }
    }
    v25 = v23;
  }
  __writefsdword(0, v16[0]);
  v17 = (int *)&loc_4ADE30;
  unknown_libname_34(&v27);
  unknown_libname_34(&a6);
  return v25;
}


// ==================== [0x4ADF04] sub_4ADF04 (size: 613) ====================
char __fastcall sub_4ADF04(int a1, int a2, int a3)
{
  int v3; // eax
  int v4; // eax
  int v6; // [esp-28h] [ebp-70h]
  int v7; // [esp-28h] [ebp-70h]
  int v8; // [esp-24h] [ebp-6Ch]
  int v9; // [esp-24h] [ebp-6Ch]
  char v10; // [esp-20h] [ebp-68h]
  char v11; // [esp-20h] [ebp-68h]
  int v12; // [esp-1Ch] [ebp-64h]
  int v13; // [esp-1Ch] [ebp-64h]
  unsigned int v14[3]; // [esp-18h] [ebp-60h] BYREF
  unsigned int v15[2]; // [esp-Ch] [ebp-54h] BYREF
  int *v16; // [esp-4h] [ebp-4Ch]
  int v17; // [esp+Ch] [ebp-3Ch] BYREF
  int v18; // [esp+10h] [ebp-38h]
  int v19; // [esp+14h] [ebp-34h]
  int v20; // [esp+18h] [ebp-30h]
  int v21; // [esp+1Ch] [ebp-2Ch]
  void *v22; // [esp+20h] [ebp-28h]
  DWORD TickCount; // [esp+24h] [ebp-24h]
  int v24; // [esp+28h] [ebp-20h]
  int v25; // [esp+2Ch] [ebp-1Ch]
  int v26; // [esp+30h] [ebp-18h]
  int v27; // [esp+34h] [ebp-14h]
  char v28; // [esp+3Bh] [ebp-Dh]
  int v29; // [esp+3Ch] [ebp-Ch]
  int v30; // [esp+40h] [ebp-8h]
  int v31; // [esp+44h] [ebp-4h]
  int savedregs; // [esp+48h] [ebp+0h] BYREF

  v18 = 0;
  v17 = 0;
  v20 = 0;
  v19 = 0;
  v29 = a3;
  v30 = a2;
  v31 = a1;
  v16 = &savedregs;
  v15[1] = (unsigned int)&loc_4AE158;
  v15[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v15);
  v28 = 1;
  TickCount = GetTickCount();
  v14[2] = (unsigned int)&savedregs;
  v14[1] = (unsigned int)&loc_4AE129;
  v14[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v14);
  v26 = v29;
  if ( *(int *)(v30 + 40) > 0 )
  {
    if ( System::__linkproc__ RandInt(100) >= *(_DWORD *)(v30 + 64) )
    {
      if ( v26 - 1 >= 0 )
      {
        v21 = v26;
        v27 = 0;
        while ( 1 )
        {
          v13 = *(_DWORD *)(v30 + 20)
              - *(_DWORD *)(v30 + 44)
              + System::__linkproc__ RandInt(2 * *(_DWORD *)(v30 + 44) + 1);
          unknown_libname_41(*(_DWORD *)(v30 + 40));
          v9 = v18;
          v4 = System::__linkproc__ RandInt(2 * *(_DWORD *)(v30 + 44) + 1);
          unknown_libname_41(*(_DWORD *)(v30 + 16) - *(_DWORD *)(v30 + 44) + v4);
          v22 = sub_4AD56C(v31, v17, v7, v13, v11, v9);
          if ( v22 != nullptr )
            Classes::TList::Add(*(Classes::TList **)(v30 + 60), v22);
          if ( GetTickCount() - TickCount > *off_4E9FD4 )
            break;
          ++v27;
          if ( --v21 == 0 )
            goto LABEL_18;
        }
        v28 = 0;
      }
    }
    else
    {
      v25 = *(_DWORD *)(v30 + 16) - *(_DWORD *)(v30 + 44) + System::__linkproc__ RandInt(2 * *(_DWORD *)(v30 + 44) + 1);
      v24 = *(_DWORD *)(v30 + 20) - *(_DWORD *)(v30 + 44) + System::__linkproc__ RandInt(2 * *(_DWORD *)(v30 + 44) + 1);
      if ( v26 - 1 >= 0 )
      {
        v21 = v26;
        v27 = 0;
        while ( 1 )
        {
          v12 = v24 - 10 + System::__linkproc__ RandInt(20);
          unknown_libname_41(*(_DWORD *)(v30 + 40));
          v8 = v20;
          v3 = System::__linkproc__ RandInt(20);
          unknown_libname_41(v25 - 10 + v3);
          v22 = sub_4AD56C(v31, v19, v6, v12, v10, v8);
          if ( v22 != nullptr )
            Classes::TList::Add(*(Classes::TList **)(v30 + 60), v22);
          if ( GetTickCount() - TickCount > *off_4E9FD4 )
            break;
          ++v27;
          if ( --v21 == 0 )
            goto LABEL_18;
        }
        v28 = 0;
      }
    }
  }
LABEL_18:
  __writefsdword(0, v14[0]);
  __writefsdword(0, v15[0]);
  v16 = (int *)&loc_4AE15F;
  unknown_libname_35(&v17, 4);
  return v28;
}


// ==================== [0x4AEF6C] sub_4AEF6C (size: 879) ====================
int __fastcall sub_4AEF6C(int a1, int a2, int a3)
{
  int v3; // ecx
  int v4; // ecx
  int v5; // eax
  int v6; // ecx
  int v7; // eax
  int v8; // ecx
  int v9; // ecx
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-Ch] [ebp-130h] BYREF
  void *v12; // [esp-8h] [ebp-12Ch]
  int *v13; // [esp-4h] [ebp-128h]
  void *v14; // [esp+0h] [ebp-124h]
  int v15; // [esp+4h] [ebp-120h] BYREF
  int v16; // [esp+8h] [ebp-11Ch] BYREF
  _BYTE v17[256]; // [esp+Ch] [ebp-118h] BYREF
  int v18; // [esp+10Ch] [ebp-18h]
  int v19; // [esp+110h] [ebp-14h]
  int i; // [esp+114h] [ebp-10h]
  int v21; // [esp+118h] [ebp-Ch]
  int v22; // [esp+11Ch] [ebp-8h]
  int v23; // [esp+120h] [ebp-4h]
  int savedregs; // [esp+124h] [ebp+0h] BYREF

  v15 = 0;
  v16 = 0;
  v21 = a3;
  v22 = a2;
  v23 = a1;
  v13 = &savedregs;
  v12 = &loc_4AF2CF;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  System::__linkproc__ FillChar(a3, 3708, 0);
  LOBYTE(v3) = 14;
  System::__linkproc__ PStrNCpy(v21, v22 + 21, v3);
  sub_4B3580(v22, v21 + 16);
  *(_DWORD *)(v21 + 3180) = *(_DWORD *)(v22 + 1472);
  if ( *(_DWORD *)(v22 + 628) != 0 )
  {
    LOBYTE(v4) = 14;
    System::__linkproc__ PStrNCpy(v21 + 3184, *(_DWORD *)(v22 + 628) + 21, v4);
    v5 = (*(int (__fastcall **)(_DWORD))(**(_DWORD **)(*(_DWORD *)(v22 + 628) + 632) + 20))(*(_DWORD *)(*(_DWORD *)(v22 + 628) + 632))
       - 1;
    if ( v5 >= 0 )
    {
      v18 = v5 + 1;
      i = 0;
      do
      {
        (*(void (__fastcall **)(_DWORD, int, int *))(**(_DWORD **)(*(_DWORD *)(v22 + 628) + 632) + 12))(
          *(_DWORD *)(*(_DWORD *)(v22 + 628) + 632),
          i,
          &v16);
        System::__linkproc__ LStrToString(v17, v16, 255);
        v6 = v21;
        LOBYTE(v6) = 14;
        System::__linkproc__ PStrNCpy(v21 + 15 * i++ + 3199, v17, v6);
        --v18;
      }
      while ( v18 != 0 );
    }
  }
  *(_BYTE *)(v21 + 3334) = *(_BYTE *)(v22 + 637);
  *(_BYTE *)(v21 + 3335) = *(_BYTE *)(v22 + 636);
  *(_BYTE *)(v21 + 3336) = *(_BYTE *)(v22 + 638);
  *(_BYTE *)(v21 + 3337) = *(_BYTE *)(v22 + 841);
  *(_BYTE *)(v21 + 3338) = *(_BYTE *)(v22 + 842);
  v7 = (*(int (__fastcall **)(_DWORD, _DWORD))(**(_DWORD **)(v22 + 640) + 20))(
         *(_DWORD *)(v22 + 640),
         **(_DWORD **)(v22 + 640))
     - 1;
  if ( v7 >= 0 )
  {
    v18 = v7 + 1;
    i = 0;
    do
    {
      if ( i <= 9 )
      {
        (*(void (__fastcall **)(_DWORD, int, int *))(**(_DWORD **)(v22 + 640) + 12))(*(_DWORD *)(v22 + 640), i, &v15);
        System::__linkproc__ LStrToString(v17, v15, 255);
        v8 = v21;
        LOBYTE(v8) = 14;
        System::__linkproc__ PStrNCpy(v21 + 15 * i + 3339, v17, v8);
      }
      ++i;
      --v18;
    }
    while ( v18 != 0 );
  }
  if ( *(_DWORD *)(*(_DWORD *)(v22 + 668) + 8) - 1 >= 0 )
  {
    v18 = *(_DWORD *)(*(_DWORD *)(v22 + 668) + 8);
    i = 0;
    do
    {
      v19 = Classes::TList::Get(*(Classes::TList **)(v22 + 668), i);
      if ( i <= 4 )
      {
        LOBYTE(v9) = 14;
        System::__linkproc__ PStrNCpy(v21 + 36 * i + 3492, v19 + 21, v9);
        *(_DWORD *)(v21 + 36 * i + 3508) = *(_DWORD *)(v19 + 660);
        *(_BYTE *)(v21 + 36 * i + 3512) = *(_BYTE *)(v19 + 664);
        *(_BYTE *)(v21 + 36 * i + 3513) = *(_BYTE *)(v19 + 665);
        ExceptionList = (struct _EXCEPTION_REGISTRATION_RECORD *)GetTickCount();
        *(_DWORD *)(v21 + 36 * i + 3516) = (*(_DWORD *)(v19 + 652) - (int)ExceptionList) / 0x3E8u;
        *(_DWORD *)(v21 + 36 * i + 3520) = *(unsigned __int16 *)(v19 + 420);
        *(_DWORD *)(v21 + 36 * i + 3524) = *(unsigned __int16 *)(v19 + 422);
      }
      ++i;
      --v18;
    }
    while ( v18 != 0 );
  }
  for ( i = 0; i != 6; ++i )
  {
    *(_BYTE *)(v21 + i + 3676) = *(_BYTE *)(v22 + i + 536);
    *(_DWORD *)(v21 + 4 * i + 3684) = GetTickCount() - *(_DWORD *)(v22 + 4 * i + 544);
  }
  __writefsdword(0, (unsigned int)v12);
  v14 = &loc_4AF2D6;
  return unknown_libname_35(&v15, 2);
}


// ==================== [0x4AF2DC] sub_4AF2DC (size: 454) ====================
int __fastcall sub_4AF2DC(int a1, _BYTE *a2, int a3)
{
  unsigned int v4[2]; // [esp-Ch] [ebp-30h] BYREF
  int *v5; // [esp-4h] [ebp-28h]
  int v6; // [esp+Ch] [ebp-18h] BYREF
  void *Mem; // [esp+10h] [ebp-14h]
  int i; // [esp+14h] [ebp-10h]
  int v9; // [esp+18h] [ebp-Ch]
  _BYTE *v10; // [esp+1Ch] [ebp-8h]
  int v11; // [esp+20h] [ebp-4h]
  int savedregs; // [esp+24h] [ebp+0h] BYREF

  v6 = 0;
  v9 = a3;
  v10 = a2;
  v11 = a1;
  v5 = &savedregs;
  v4[1] = (unsigned int)&loc_4AF494;
  v4[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v4);
  *(_BYTE *)(*(_DWORD *)a3 + 637) = a2[3334];
  *(_BYTE *)(*(_DWORD *)v9 + 636) = v10[3335];
  *(_BYTE *)(*(_DWORD *)v9 + 638) = v10[3336];
  *(_BYTE *)(*(_DWORD *)v9 + 841) = v10[3337];
  *(_BYTE *)(*(_DWORD *)v9 + 842) = v10[3338];
  i = 0;
  while ( v10[15 * i + 3339] == 0 )
  {
    if ( ++i == 10 )
      goto LABEL_5;
  }
  unknown_libname_41(v4[0]);
  (*(void (__fastcall **)(_DWORD, int))(**(_DWORD **)(*(_DWORD *)v9 + 640) + 52))(*(_DWORD *)(*(_DWORD *)v9 + 640), v6);
LABEL_5:
  for ( i = 0; i != 5; ++i )
  {
    if ( v10[36 * i + 3492] != 0 )
    {
      Mem = (void *)System::__linkproc__ GetMem(36);
      qmemcpy(Mem, &v10[36 * i + 3492], 0x24u);
      sub_4B87C4(500, 0, 0, 0, Mem, 0);
    }
  }
  for ( i = 0; i != 6; ++i )
  {
    *(_BYTE *)(*(_DWORD *)v9 + i + 536) = v10[i + 3676];
    *(_DWORD *)(*(_DWORD *)v9 + 4 * i + 544) = *(_DWORD *)&v10[4 * i + 3684] + GetTickCount();
  }
  __writefsdword(0, v4[0]);
  v5 = (int *)&loc_4AF49B;
  return unknown_libname_34(&v6);
}


// ==================== [0x4AF510] sub_4AF510 (size: 339) ====================
int __fastcall sub_4AF510(int a1, const void *a2, int a3)
{
  int v3; // ecx
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-18h] [ebp-4Ch] BYREF
  void *v6; // [esp-14h] [ebp-48h]
  _strings *v7; // [esp-10h] [ebp-44h]
  unsigned int v8[2]; // [esp-Ch] [ebp-40h] BYREF
  int *v9; // [esp-4h] [ebp-38h]
  int v10; // [esp+Ch] [ebp-28h] BYREF
  int v11; // [esp+10h] [ebp-24h]
  void *v12; // [esp+14h] [ebp-20h]
  int Buffer; // [esp+18h] [ebp-1Ch] BYREF
  HANDLE hFile; // [esp+1Ch] [ebp-18h]
  int i; // [esp+20h] [ebp-14h]
  void *v16; // [esp+24h] [ebp-10h] BYREF
  int v17; // [esp+28h] [ebp-Ch]
  LPCVOID lpBuffer; // [esp+2Ch] [ebp-8h]
  int v19; // [esp+30h] [ebp-4h]
  int savedregs; // [esp+34h] [ebp+0h] BYREF

  v10 = 0;
  v12 = nullptr;
  v11 = 0;
  v16 = nullptr;
  v17 = a3;
  lpBuffer = a2;
  v19 = a1;
  v9 = &savedregs;
  v8[1] = (unsigned int)&loc_4AF655;
  v8[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v8);
  unknown_libname_34(a3);
  v7 = &str___[1];
  Sysutils::IntToStr(*off_4EA64C);
  v6 = v12;
  ExceptionList = (struct _EXCEPTION_REGISTRATION_RECORD *)&str____[1];
  Sysutils::IntToStr(*off_4EA118);
  System::__linkproc__ LStrCatN(&v16, 5, v3, v11, &str__shr[1]);
  ++*off_4EA118;
  v7 = (_strings *)&savedregs;
  v6 = &loc_4AF61E;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  Buffer = 0;
  for ( i = 0; i != 3708; ++i )
    Buffer += *((unsigned __int8 *)lpBuffer + i);
  System::__linkproc__ LStrCat3((int)&v10, (void *)*off_4EA580, v16);
  hFile = (HANDLE)sub_4092B8(v10);
  if ( (int)hFile > 0 )
  {
    sub_409308(hFile, lpBuffer, 0xE7Cu);
    sub_409308(hFile, &Buffer, 4u);
    sub_409340(hFile);
    unknown_libname_36(v17, v16);
  }
  __writefsdword(0, (unsigned int)ExceptionList);
  __writefsdword(0, v8[0]);
  v9 = (int *)&loc_4AF65C;
  unknown_libname_35(&v10, 3);
  return unknown_libname_34(&v16);
}


// ==================== [0x4AFD28] sub_4AFD28 (size: 3149) ====================
System::TObject *__usercall sub_4AFD28@<eax>(_DWORD *a1@<eax>, int a2)
{
  int v2; // ecx
  int v3; // ecx
  int v4; // eax
  int v5; // eax
  System::TObject *v6; // edx
  char *v7; // eax
  int v8; // eax
  char *v9; // edx
  int v10; // eax
  char *v11; // edx
  __int16 v12; // ax
  int v13; // eax
  int v14; // ecx
  int v15; // ecx
  int v16; // eax
  int v17; // eax
  int v18; // ecx
  int v19; // ecx
  int v20; // eax
  int v21; // eax
  int v22; // eax
  int v23; // ecx
  int v24; // ecx
  int v25; // ecx
  int v26; // ecx
  int v27; // ecx
  int v28; // ecx
  int v29; // ecx
  int v30; // ecx
  int v31; // ecx
  int v33; // [esp-34h] [ebp-1C8h]
  int v34; // [esp-34h] [ebp-1C8h]
  int v35; // [esp-34h] [ebp-1C8h]
  int v36; // [esp-2Ch] [ebp-1C0h]
  unsigned int v37[3]; // [esp-18h] [ebp-1ACh] BYREF
  unsigned int v38[2]; // [esp-Ch] [ebp-1A0h] BYREF
  int *v39; // [esp-4h] [ebp-198h]
  int v40; // [esp+Ch] [ebp-188h] BYREF
  _DWORD v41[2]; // [esp+1Ch] [ebp-178h] BYREF
  _DWORD v42[3]; // [esp+30h] [ebp-164h] BYREF
  _DWORD v43[2]; // [esp+48h] [ebp-14Ch] BYREF
  _DWORD v44[4]; // [esp+58h] [ebp-13Ch] BYREF
  _DWORD v45[3]; // [esp+68h] [ebp-12Ch] BYREF
  _BYTE v46[256]; // [esp+74h] [ebp-120h] BYREF
  char *v47; // [esp+174h] [ebp-20h]
  _BYTE *v48; // [esp+178h] [ebp-1Ch]
  int v49; // [esp+17Ch] [ebp-18h] BYREF
  System::TObject *v50; // [esp+180h] [ebp-14h] BYREF
  int v51; // [esp+184h] [ebp-10h]
  int i; // [esp+188h] [ebp-Ch]
  System::TObject *v53; // [esp+18Ch] [ebp-8h]
  _DWORD *v54; // [esp+190h] [ebp-4h]
  int savedregs; // [esp+194h] [ebp+0h] BYREF

  v54 = a1;
  v39 = &savedregs;
  v38[1] = (unsigned int)&loc_4B0964;
  v38[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v38);
  v53 = nullptr;
  v37[2] = (unsigned int)&savedregs;
  v37[1] = (unsigned int)&loc_4B092A;
  v37[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v37);
  v50 = (System::TObject *)sub_4C9860((int *)off_4B7774, 1);
  if ( *off_4EA4E4[0] != 0 )
    v48 = nullptr;
  else
    v48 = (_BYTE *)sub_4AEE98(*(_DWORD *)(a2 - 4), *v54, v54[803]);
  if ( v48 != nullptr )
  {
    sub_4B3050(v54 + 1, &v50);
    LOBYTE(v27) = 16;
    System::__linkproc__ PStrNCpy((char *)v50 + 4, v48 + 59, v27);
    *((_DWORD *)v50 + 9) = *((unsigned __int16 *)v48 + 38);
    *((_DWORD *)v50 + 10) = *((unsigned __int16 *)v48 + 39);
    qmemcpy((char *)v50 + 52, v48 + 88, 0x28u);
    qmemcpy((char *)v50 + 408, v48 + 88, 0x28u);
    sub_4AF2DC(*(_DWORD *)(a2 - 4), v48, (int)&v50);
    sub_4AF4A4(*(_DWORD *)(a2 - 4), v48);
    unknown_libname_41(v37[0]);
    v51 = sub_4B7424(*off_4EA22C, *off_4EA64C, v42[1]);
    if ( v51 != 0 )
    {
      if ( (unsigned __int8)sub_4B5ED0(1) == 0 )
      {
        Sysutils::IntToStr(*off_4EA64C);
        Sysutils::IntToStr(*((_DWORD *)v50 + 9));
        Sysutils::IntToStr(*((_DWORD *)v50 + 10));
        unknown_libname_41(&str___[1]);
        System::__linkproc__ LStrCatN(v41, 8, v30, v35, v40);
        sub_4E4274(v41[0]);
        System::__linkproc__ LStrToString(v46, *off_4EA438[0], 255);
        LOBYTE(v31) = 16;
        System::__linkproc__ PStrNCpy((char *)v50 + 4, v46, v31);
        v51 = sub_4B7350(*off_4EA22C, *off_4EA438[0]);
        *((_DWORD *)v50 + 9) = *off_4E9F68;
        *((_DWORD *)v50 + 10) = *off_4EA660[0];
      }
    }
    else
    {
      Sysutils::IntToStr(*off_4EA64C);
      Sysutils::IntToStr(*((_DWORD *)v50 + 9));
      Sysutils::IntToStr(*((_DWORD *)v50 + 10));
      unknown_libname_41(&str___[1]);
      System::__linkproc__ LStrCatN(v42, 8, v28, v34, v41[1]);
      sub_4E4274(v42[0]);
      System::__linkproc__ LStrToString(v46, *off_4EA438[0], 255);
      LOBYTE(v29) = 16;
      System::__linkproc__ PStrNCpy((char *)v50 + 4, v46, v29);
      v51 = sub_4B7350(*off_4EA22C, *off_4EA438[0]);
      *((_DWORD *)v50 + 9) = *off_4E9F68;
      *((_DWORD *)v50 + 10) = *off_4EA660[0];
    }
    sub_4BA494(v50);
    *((_DWORD *)v50 + 190) = v51;
    if ( *((_DWORD *)v50 + 190) == 0 )
      goto LABEL_45;
    *((_BYTE *)v50 + 1468) = 0;
    *((_BYTE *)v50 + 1706) = 1;
    *((_BYTE *)v50 + 1707) = 1;
LABEL_53:
    LOBYTE(v24) = 10;
    System::__linkproc__ PStrNCpy((char *)v50 + 1420, v54 + 792, v24);
    unknown_libname_41(v37[0]);
    *((_DWORD *)v50 + 359) = v54[807];
    *((_DWORD *)v50 + 360) = v54[808];
    *((_DWORD *)v50 + 362) = v54[809];
    *((_DWORD *)v50 + 368) = v54[803];
    *((_DWORD *)v50 + 369) = v54[804];
    *((_DWORD *)v50 + 370) = v54[805];
    *((_DWORD *)v50 + 371) = v54[810];
    *((_DWORD *)v50 + 363) = v54[806];
    v53 = v50;
    __writefsdword(0, v37[0]);
  }
  else
  {
    sub_4B3050(v54 + 1, &v50);
    *((_BYTE *)v50 + 570) = 0;
    if ( *((_BYTE *)v50 + 120) != 0 )
      goto LABEL_8;
    while ( 1 )
    {
      sub_4B1E50((char *)v50 + 144);
      System::__linkproc__ LStrToString(v46, v49, 255);
      LOBYTE(v2) = 16;
      System::__linkproc__ PStrNCpy((char *)v50 + 120, v46, v2);
      LOBYTE(v3) = 16;
      System::__linkproc__ PStrNCpy((char *)v50 + 4, (char *)v50 + 120, v3);
      v4 = sub_4DA6DC(v50);
      *((_DWORD *)v50 + 9) = v4;
      v5 = sub_4DA708(v50);
      v6 = v50;
      *((_DWORD *)v50 + 10) = v5;
      if ( *((_BYTE *)v50 + 52) == 0 )
      {
        v47 = (char *)v50 + 52;
        *((_BYTE *)v50 + 52) = 1;
        *((_WORD *)v47 + 1) = 0;
        v7 = v47;
        *((_WORD *)v47 + 2) = 0;
        LOBYTE(v6) = 2;
        LOBYTE(v7) = 1;
        v8 = unknown_libname_77(v7, v6);
        v9 = v47;
        *((_WORD *)v47 + 3) = v8;
        LOBYTE(v9) = 2;
        LOBYTE(v8) = 1;
        v10 = unknown_libname_77(v8, v9);
        v11 = v47;
        *((_WORD *)v47 + 4) = v10;
        LOBYTE(v11) = 2;
        LOBYTE(v10) = 1;
        v12 = unknown_libname_77(v10, v11);
        *((_WORD *)v47 + 5) = v12;
        *((_WORD *)v47 + 7) = 15;
        *((_WORD *)v47 + 6) = 15;
        *((_WORD *)v47 + 8) = 15;
        *((_WORD *)v47 + 9) = 15;
        *((_DWORD *)v47 + 6) = 0;
        *((_DWORD *)v47 + 7) = 100;
        *((_WORD *)v47 + 16) = 0;
        *((_WORD *)v47 + 17) = 30;
        *((_BYTE *)v50 + 1704) = 1;
      }
LABEL_8:
      unknown_libname_41(v37[0]);
      v51 = sub_4B7424(*off_4EA22C, *off_4EA64C, v45[2]);
      if ( v51 != 0 )
      {
        if ( *(_BYTE *)(v51 + 47) != 0 )
        {
          if ( *((_WORD *)v50 + 32) == 0 && *((int *)v50 + 48) < 3 )
          {
            *((_WORD *)v50 + 32) = *((_WORD *)v50 + 34);
            *((_WORD *)v50 + 33) = *((_WORD *)v50 + 35);
            *((_BYTE *)v50 + 1576) = 1;
          }
        }
        else
        {
          *((_DWORD *)v50 + 48) = 0;
        }
      }
      unknown_libname_41(v37[0]);
      v13 = sub_49A408(*off_4EA07C[0], v45[1]);
      *((_DWORD *)v50 + 200) = v13;
      if ( v51 != 0
        && (*(_DWORD *)(*off_4EA080[0] + 8) == v51
         || *(_BYTE *)(*off_4EA080[0] + 65) != 0 && (unsigned __int8)sub_4910F4(*((_DWORD *)v50 + 10)) != 0) )
      {
        if ( (unsigned __int8)sub_490438(*off_4EA080[0], v50) != 0 )
        {
          if ( *(_DWORD *)(*off_4EA080[0] + 8) == v51 )
          {
            sub_490290(*off_4EA080[0], v45);
            System::__linkproc__ LStrToString(v46, v45[0], 255);
            LOBYTE(v15) = 16;
            System::__linkproc__ PStrNCpy((char *)v50 + 4, v46, v15);
            v16 = sub_4902B0(*off_4EA080[0]);
            *((_DWORD *)v50 + 9) = v16;
            v17 = sub_4902D8(*off_4EA080[0]);
            *((_DWORD *)v50 + 10) = v17;
          }
        }
        else
        {
          LOBYTE(v14) = 16;
          System::__linkproc__ PStrNCpy((char *)v50 + 4, (char *)v50 + 120, v14);
          *((_DWORD *)v50 + 9) = *((_DWORD *)v50 + 35) - 2 + System::__linkproc__ RandInt(5);
          *((_DWORD *)v50 + 10) = *((_DWORD *)v50 + 36) - 2 + System::__linkproc__ RandInt(5);
        }
      }
      if ( *((int *)v50 + 49) <= 1 && *((_BYTE *)v50 + 52) != 0 )
        *((_DWORD *)v50 + 49) = 2;
      unknown_libname_41(v37[0]);
      if ( sub_4B7350(*off_4EA22C, v44[3]) == 0 )
        *((_WORD *)v50 + 32) = 0;
      if ( *((_WORD *)v50 + 32) == 0 )
      {
        sub_4C9C08(v50);
        if ( sub_4BF0A0(v50) >= 2 )
        {
          *((_WORD *)v50 + 2) = 14593;
          *((_DWORD *)v50 + 9) = System::__linkproc__ RandInt(13) + 27;
          *((_DWORD *)v50 + 10) = System::__linkproc__ RandInt(13) + 27;
        }
        else if ( *(_BYTE *)(*off_4EA080[0] + 65) != 0 && (unsigned __int8)sub_490438(*off_4EA080[0], v50) != 0 )
        {
          System::__linkproc__ LStrToString(v46, *(_DWORD *)(*off_4EA080[0] + 36), 255);
          LOBYTE(v19) = 16;
          System::__linkproc__ PStrNCpy((char *)v50 + 4, v46, v19);
          v20 = sub_4902B0(*off_4EA080[0]);
          *((_DWORD *)v50 + 9) = v20;
          v21 = sub_4902D8(*off_4EA080[0]);
          *((_DWORD *)v50 + 10) = v21;
        }
        else
        {
          LOBYTE(v18) = 16;
          System::__linkproc__ PStrNCpy((char *)v50 + 4, (char *)v50 + 120, v18);
          *((_DWORD *)v50 + 9) = *((_DWORD *)v50 + 35) - 2 + System::__linkproc__ RandInt(5);
          *((_DWORD *)v50 + 10) = *((_DWORD *)v50 + 36) - 2 + System::__linkproc__ RandInt(5);
        }
        *((_WORD *)v50 + 32) = 14;
      }
      sub_4BA494(v50);
      unknown_libname_41(v37[0]);
      v51 = sub_4B7424(*off_4EA22C, *off_4EA64C, v44[2]);
      if ( v51 == 0 )
        break;
      for ( i = 0; i != 5; ++i )
      {
        if ( (unsigned __int8)sub_4B5ED0(1) != 0 )
          break;
        *((_DWORD *)v50 + 9) = *((_DWORD *)v50 + 9) - 3 + System::__linkproc__ RandInt(6);
        *((_DWORD *)v50 + 10) = *((_DWORD *)v50 + 10) - 3 + System::__linkproc__ RandInt(6);
      }
      if ( (unsigned __int8)sub_4B5ED0(1) == 0 )
      {
        Sysutils::IntToStr(*off_4EA64C);
        Sysutils::IntToStr(*((_DWORD *)v50 + 9));
        Sysutils::IntToStr(*((_DWORD *)v50 + 10));
        unknown_libname_41(&str___[1]);
        System::__linkproc__ LStrCatN(v43, 8, v25, v33, v42[2]);
        sub_4E4274(v43[0]);
        System::__linkproc__ LStrToString(v46, *off_4EA438[0], 255);
        LOBYTE(v26) = 16;
        System::__linkproc__ PStrNCpy((char *)v50 + 4, v46, v26);
        v51 = sub_4B7350(*off_4EA22C, *off_4EA438[0]);
        *((_DWORD *)v50 + 9) = *off_4E9F68;
        *((_DWORD *)v50 + 10) = *off_4EA660[0];
      }
      *((_DWORD *)v50 + 190) = v51;
      if ( *((_DWORD *)v50 + 190) != 0 )
      {
        *((_BYTE *)v50 + 1468) = 0;
        goto LABEL_53;
      }
LABEL_45:
      sub_4E4274(&str__Error__hum_PEn[1]);
    }
    *((_DWORD *)v50 + 368) = v54[803];
    *((_DWORD *)v50 + 359) = v54[807];
    *((_DWORD *)v50 + 362) = v54[809];
    *((_DWORD *)v50 + 360) = v54[808];
    qmemcpy((char *)v50 + 408, (char *)v50 + 52, 0x28u);
    unknown_libname_41(v37[0]);
    v22 = sub_4B7510(*off_4EA22C, v44[1]);
    *((_DWORD *)v50 + 372) = v22;
    if ( *((_WORD *)v50 + 32) != 14 )
    {
      Sysutils::IntToStr(*off_4EA64C);
      Sysutils::IntToStr(*((_DWORD *)v50 + 372));
      unknown_libname_41(&str____[1]);
      System::__linkproc__ LStrCatN(v44, 6, v23, v36, v43[1]);
      sub_4E4274(v44[0]);
    }
    sub_4AF85C(*(_DWORD *)(a2 - 4), v50, *((_DWORD *)v50 + 372));
    sub_4AF988(*(_DWORD *)(a2 - 4), v50, *((_DWORD *)v50 + 372));
    System::TObject::Free(v50);
    __writefsdword(0, v37[0]);
  }
  __writefsdword(0, v38[0]);
  v39 = (int *)&loc_4B096B;
  unknown_libname_35(&v40, 26);
  unknown_libname_34(&v49);
  return v53;
}


// ==================== [0x4B0A88] sub_4B0A88 (size: 2411) ====================
int __fastcall sub_4B0A88(int a1)
{
  int v1; // ecx
  int v2; // eax
  int v3; // edx
  int v4; // edx
  int v5; // edx
  int v6; // eax
  int v7; // eax
  int v8; // eax
  int v9; // eax
  System::TObject *v10; // eax
  int v11; // eax
  int v12; // ecx
  int v13; // edx
  int v14; // eax
  int v15; // eax
  int v16; // edx
  int v17; // ecx
  struct _EXCEPTION_REGISTRATION_RECORD *v19; // [esp-24h] [ebp-98h] BYREF
  void *v20; // [esp-20h] [ebp-94h]
  int *v21; // [esp-1Ch] [ebp-90h]
  struct _EXCEPTION_REGISTRATION_RECORD *v22; // [esp-18h] [ebp-8Ch] BYREF
  void *v23; // [esp-14h] [ebp-88h]
  int *v24; // [esp-10h] [ebp-84h]
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-Ch] [ebp-80h] BYREF
  void *v26; // [esp-8h] [ebp-7Ch]
  int *v27; // [esp-4h] [ebp-78h]
  void *v28; // [esp+0h] [ebp-74h]
  int v29; // [esp+Ch] [ebp-68h] BYREF
  _DWORD v30[3]; // [esp+20h] [ebp-54h] BYREF
  _DWORD v31[2]; // [esp+2Ch] [ebp-48h] BYREF
  int v32; // [esp+34h] [ebp-40h] BYREF
  int v33; // [esp+38h] [ebp-3Ch] BYREF
  int v34; // [esp+3Ch] [ebp-38h]
  char v35; // [esp+43h] [ebp-31h]
  int v36; // [esp+44h] [ebp-30h]
  System::TObject *v37; // [esp+48h] [ebp-2Ch]
  System::TObject *v38; // [esp+4Ch] [ebp-28h]
  System::TObject *v39; // [esp+50h] [ebp-24h]
  void *v40; // [esp+54h] [ebp-20h]
  int v41; // [esp+58h] [ebp-1Ch]
  _DWORD *v42; // [esp+5Ch] [ebp-18h]
  int v43; // [esp+60h] [ebp-14h]
  DWORD TickCount; // [esp+64h] [ebp-10h]
  signed int v45; // [esp+68h] [ebp-Ch]
  int v46; // [esp+6Ch] [ebp-8h]
  int v47; // [esp+70h] [ebp-4h]
  int savedregs; // [esp+74h] [ebp+0h] BYREF

  v47 = a1;
  v27 = &savedregs;
  v26 = &loc_4B13E5;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  v36 = 0;
  TickCount = GetTickCount();
  if ( GetTickCount() - *(_DWORD *)(v47 + 48) > 0xC8 )
  {
    v24 = &savedregs;
    v23 = &loc_4B0E0A;
    v22 = NtCurrentTeb()->NtTib.ExceptionList;
    __writefsdword(0, (unsigned int)&v22);
    *(_DWORD *)(v47 + 48) = GetTickCount();
    v39 = nullptr;
    v38 = nullptr;
    v37 = nullptr;
    v21 = &savedregs;
    v20 = &loc_4B0D43;
    v19 = NtCurrentTeb()->NtTib.ExceptionList;
    __writefsdword(0, (unsigned int)&v19);
    sub_450080(*off_4EA440[0]);
    v2 = (*(int (__fastcall **)(_DWORD, _DWORD, int, void *, struct _EXCEPTION_REGISTRATION_RECORD *, int *, void *, struct _EXCEPTION_REGISTRATION_RECORD *))(**(_DWORD **)(v47 + 4) + 20))(
           *(_DWORD *)(v47 + 4),
           **(_DWORD **)(v47 + 4),
           v1,
           v23,
           v22,
           v21,
           v20,
           v19)
       - 1;
    if ( v2 >= 0 )
    {
      v34 = v2 + 1;
      v46 = 0;
      do
      {
        if ( (unsigned __int8)sub_4B4988(*off_4EA10C) != 0
          || (ExceptionList = (struct _EXCEPTION_REGISTRATION_RECORD *)&savedregs,
              (*(void (__fastcall **)(_DWORD, int, int *))(**(_DWORD **)(v47 + 4) + 12))(
                *(_DWORD *)(v47 + 4),
                v46,
                &v33),
              (unsigned __int8)sub_4AFC68(ExceptionList) != 0) )
        {
          (*(void (__fastcall **)(_DWORD, int, _DWORD *))(**(_DWORD **)(v47 + 4) + 12))(*(_DWORD *)(v47 + 4), v46, v31);
          sub_4AEB80(v47, v31[0]);
          if ( v38 == nullptr )
          {
            LOBYTE(v4) = 1;
            v38 = (System::TObject *)unknown_libname_22(off_40FAFC, v4);
            LOBYTE(v5) = 1;
            v37 = (System::TObject *)unknown_libname_22(off_40FAFC, v5);
          }
          v6 = (*(int (__fastcall **)(_DWORD, int))(**(_DWORD **)(v47 + 4) + 24))(*(_DWORD *)(v47 + 4), v46);
          Classes::TList::Add(v38, *(void **)(v6 + 1448));
          v7 = (*(int (__fastcall **)(_DWORD, int))(**(_DWORD **)(v47 + 4) + 24))(*(_DWORD *)(v47 + 4), v46);
          Classes::TList::Add(v37, *(void **)(v7 + 1436));
        }
        else
        {
          v42 = (_DWORD *)(*(int (__fastcall **)(_DWORD, int))(**(_DWORD **)(v47 + 4) + 24))(*(_DWORD *)(v47 + 4), v46);
          v40 = sub_4AFD28(v42, (int)&savedregs);
          if ( v40 != nullptr )
          {
            (*(void (__fastcall **)(_DWORD, int, int *))(**(_DWORD **)(v47 + 4) + 12))(*(_DWORD *)(v47 + 4), v46, &v32);
            (*(void (__fastcall **)(_DWORD, int, void *))(**(_DWORD **)(v47 + 8) + 56))(*(_DWORD *)(v47 + 8), v32, v40);
            unknown_libname_41(v26);
            sub_4AF728(v31[1]);
            if ( v39 == nullptr )
            {
              LOBYTE(v3) = 1;
              v39 = (System::TObject *)unknown_libname_22(off_40FAFC, v3);
            }
            Classes::TList::Add(v39, v40);
          }
        }
        v8 = (*(int (__fastcall **)(_DWORD, int))(**(_DWORD **)(v47 + 4) + 24))(*(_DWORD *)(v47 + 4), v46);
        System::__linkproc__ Dispose(v8, off_4B26E4);
        ++v46;
        --v34;
      }
      while ( v34 != 0 );
    }
    (*(void (__fastcall **)(_DWORD))(**(_DWORD **)(v47 + 4) + 64))(*(_DWORD *)(v47 + 4));
    if ( *(_DWORD *)(*(_DWORD *)(v47 + 20) + 8) - 1 >= 0 )
    {
      v34 = *(_DWORD *)(*(_DWORD *)(v47 + 20) + 8);
      v46 = 0;
      do
      {
        v41 = Classes::TList::Get(*(Classes::TList **)(v47 + 20), v46);
        unknown_libname_41(v26);
        v40 = (void *)sub_4AE640(v47, v30[2]);
        if ( v40 != nullptr )
        {
          unknown_libname_41(v26);
          sub_4CD844(v40, v30[1], *(_DWORD *)(v41 + 32));
        }
        System::__linkproc__ FreeMem(v41, 36);
        ++v46;
        --v34;
      }
      while ( v34 != 0 );
    }
    (*(void (__fastcall **)(_DWORD))(**(_DWORD **)(v47 + 20) + 8))(*(_DWORD *)(v47 + 20));
    __writefsdword(0, (unsigned int)v26);
    v28 = &loc_4B0D4A;
    Webadapt::TBaseAdapterErrorsList::GetErrorCount(*(Webadapt::TBaseAdapterErrorsList **)off_4EA440[0]);
    if ( v39 != nullptr )
    {
      if ( *((_DWORD *)v39 + 2) - 1 >= 0 )
      {
        v34 = *((_DWORD *)v39 + 2);
        v46 = 0;
        do
        {
          v40 = (void *)Classes::TList::Get(v39, v46);
          sub_4E0CEC(v40);
          ++v46;
          --v34;
        }
        while ( v34 != 0 );
      }
      System::TObject::Free(v39);
    }
    if ( v38 != nullptr )
    {
      if ( *((_DWORD *)v38 + 2) - 1 >= 0 )
      {
        v34 = *((_DWORD *)v38 + 2);
        v46 = 0;
        do
        {
          v21 = (int *)Classes::TList::Get(v37, v46);
          v9 = Classes::TList::Get(v38, v46);
          sub_4E0490(*off_4EA628[0], v9);
          ++v46;
          --v34;
        }
        while ( v34 != 0 );
      }
      System::TObject::Free(v38);
      System::TObject::Free(v37);
    }
    __writefsdword(0, (unsigned int)v22);
  }
  v24 = &savedregs;
  v23 = &loc_4B0F7D;
  v22 = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&v22);
  if ( *(_DWORD *)(*(_DWORD *)(v47 + 16) + 8) - 1 >= 0 )
  {
    v34 = *(_DWORD *)(*(_DWORD *)(v47 + 16) + 8);
    v46 = 0;
    while ( 1 )
    {
      v40 = (void *)Classes::TList::Get(*(Classes::TList **)(v47 + 16), v46);
      if ( GetTickCount() - *((_DWORD *)v40 + 192) > 0x493E0 )
        break;
      if ( *((_BYTE *)v40 + 1497) != 0 && *((_BYTE *)v40 + 1496) != 0 )
      {
        if ( (unsigned __int8)sub_4AF85C(v47, v40, *((_DWORD *)v40 + 372)) != 0 || *((int *)v40 + 375) > 20 )
        {
          *((_BYTE *)v40 + 1497) = 0;
          *((_BYTE *)v40 + 1516) = 1;
          *((_DWORD *)v40 + 380) = GetTickCount();
        }
        else
        {
          ++*((_DWORD *)v40 + 375);
        }
      }
      if ( *((_BYTE *)v40 + 1516) != 0 && GetTickCount() - *((_DWORD *)v40 + 380) > 0x64 )
      {
        *((_BYTE *)v40 + 1516) = 0;
        sub_4AF988(v47, v40, *((_DWORD *)v40 + 372));
      }
      ++v46;
      if ( --v34 == 0 )
        goto LABEL_44;
    }
    v21 = &savedregs;
    v20 = &loc_4B0E9A;
    v19 = NtCurrentTeb()->NtTib.ExceptionList;
    __writefsdword(0, (unsigned int)&v19);
    v10 = (System::TObject *)Classes::TList::Get(*(Classes::TList **)(v47 + 16), v46);
    System::TObject::Free(v10);
    __writefsdword(0, (unsigned int)v19);
    Classes::TList::Delete(*(Classes::TList **)(v47 + 16), v46);
  }
LABEL_44:
  __writefsdword(0, (unsigned int)v22);
  v35 = 0;
  v24 = &savedregs;
  v23 = &loc_4B1324;
  v22 = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&v22);
  v43 = sub_407B30(0);
  v46 = *(_DWORD *)(v47 + 68);
  while ( 1 )
  {
    v11 = (*(int (__fastcall **)(_DWORD))(**(_DWORD **)(v47 + 8) + 20))(*(_DWORD *)(v47 + 8));
    if ( v11 <= v46 )
      break;
    v40 = (void *)(*(int (__fastcall **)(_DWORD, int))(**(_DWORD **)(v47 + 8) + 24))(*(_DWORD *)(v47 + 8), v46);
    if ( v43 - *((_DWORD *)v40 + 218) <= *((_DWORD *)v40 + 219) )
    {
LABEL_64:
      ++v46;
      if ( GetTickCount() - TickCount > *off_4EA48C[0] )
      {
        v35 = 1;
        *(_DWORD *)(v47 + 68) = v46;
        break;
      }
    }
    else
    {
      v13 = v43;
      *((_DWORD *)v40 + 218) = v43;
      if ( *((_BYTE *)v40 + 764) == 0 )
      {
        if ( *((_BYTE *)v40 + 1706) != 0 )
        {
          v21 = &savedregs;
          v20 = &loc_4B11AC;
          v19 = NtCurrentTeb()->NtTib.ExceptionList;
          __writefsdword(0, (unsigned int)&v19);
          if ( *((_BYTE *)v40 + 1468) != 0 )
          {
            if ( GetTickCount() - *((_DWORD *)v40 + 217) > *((_DWORD *)v40 + 216) )
            {
              *((_DWORD *)v40 + 217) = GetTickCount();
              sub_4B966C(v40);
              sub_4D6710(v40);
            }
            if ( GetTickCount() - *((_DWORD *)v40 + 428) > 0x2BF20 )
            {
              *((_DWORD *)v40 + 428) = GetTickCount();
              v14 = (*(int (__fastcall **)(_DWORD))(*(_DWORD *)*off_4EA44C + 20))(*off_4EA44C);
              if ( v14 > *((_DWORD *)v40 + 429) )
              {
                (*(void (__fastcall **)(_DWORD, _DWORD, _DWORD *, int *, void *, struct _EXCEPTION_REGISTRATION_RECORD *))(*(_DWORD *)*off_4EA44C + 12))(
                  *off_4EA44C,
                  *((_DWORD *)v40 + 429),
                  v30,
                  v21,
                  v20,
                  v19);
                sub_4BB124(v40, v30[0], 2);
              }
              ++*((_DWORD *)v40 + 429);
              v15 = (*(int (__fastcall **)(_DWORD))(*(_DWORD *)*off_4EA44C + 20))(*off_4EA44C);
              if ( v15 <= *((_DWORD *)v40 + 429) )
                *((_DWORD *)v40 + 429) = 0;
            }
            sub_4D68D0(v40);
            if ( (unsigned __int8)sub_4B4988(*off_4EA10C) == 0 && GetTickCount() - *((_DWORD *)v40 + 424) > 0x927C0 )
            {
              *((_DWORD *)v40 + 424) = GetTickCount();
              sub_4D6758(v40);
              sub_4AE488(v47, v40);
            }
          }
          else
          {
            *((_BYTE *)v40 + 1468) = 1;
            unknown_libname_25(v40, v13, v12);
          }
          __writefsdword(0, (unsigned int)v19);
        }
        else
        {
          v21 = &savedregs;
          v20 = &loc_4B103F;
          v19 = NtCurrentTeb()->NtTib.ExceptionList;
          __writefsdword(0, (unsigned int)&v19);
          sub_4DA588(v40);
          __writefsdword(0, (unsigned int)v19);
        }
        goto LABEL_64;
      }
      v21 = &savedregs;
      v20 = &loc_4B1205;
      v19 = NtCurrentTeb()->NtTib.ExceptionList;
      __writefsdword(0, (unsigned int)&v19);
      (*(void (__fastcall **)(_DWORD, int))(**(_DWORD **)(v47 + 8) + 68))(*(_DWORD *)(v47 + 8), v46);
      v36 = 2;
      unknown_libname_25(v40, v16, v17);
      v36 = 3;
      __writefsdword(0, (unsigned int)v19);
      v21 = &savedregs;
      v20 = &loc_4B128D;
      v19 = NtCurrentTeb()->NtTib.ExceptionList;
      __writefsdword(0, (unsigned int)&v19);
      sub_4AE45C(v47, v40);
      v36 = 4;
      sub_4D6758(v40);
      sub_4AE488(v47, v40);
      sub_4E0490(*off_4EA628[0], *((_DWORD *)v40 + 362));
      __writefsdword(0, (unsigned int)v19);
      unknown_libname_41(v22);
      sub_4AF728(v29);
    }
  }
  if ( v35 == 0 )
    *(_DWORD *)(v47 + 68) = 0;
  __writefsdword(0, (unsigned int)v22);
  ++*(_DWORD *)(v47 + 72);
  if ( *(_DWORD *)(v47 + 68) == 0 )
  {
    *(_DWORD *)(v47 + 72) = 0;
    *off_4EA0D8 = *(_DWORD *)(v47 + 72);
    v45 = GetTickCount() - *off_4EA620;
    *off_4EA2A0[0] = v45;
    *off_4EA620 = GetTickCount();
    if ( *off_4EA4FC[0] < v45 )
      *off_4EA4FC[0] = v45;
  }
  *off_4E9FF0 = GetTickCount() - TickCount;
  if ( *off_4EA240[0] < *off_4E9FF0 )
    *off_4EA240[0] = *off_4E9FF0;
  __writefsdword(0, (unsigned int)ExceptionList);
  v27 = (int *)&loc_4B13EC;
  return unknown_libname_35(&v29, 12);
}


// ==================== [0x4B1638] sub_4B1638 (size: 1306) ====================
int __fastcall sub_4B1638(int a1)
{
  DWORD v1; // ebx
  int v2; // ecx
  int v3; // ecx
  int v4; // ecx
  int v5; // eax
  int v6; // edx
  bool v7; // cc
  int v8; // edx
  DWORD v9; // eax
  int v10; // ecx
  System::TObject *v11; // edx
  int v12; // ecx
  int v13; // ecx
  int v14; // ecx
  unsigned int v16[3]; // [esp-18h] [ebp-17Ch] BYREF
  unsigned int v17[2]; // [esp-Ch] [ebp-170h] BYREF
  int *v18; // [esp-4h] [ebp-168h]
  _DWORD v19[3]; // [esp+Ch] [ebp-158h] BYREF
  int v20; // [esp+18h] [ebp-14Ch] BYREF
  int v21; // [esp+1Ch] [ebp-148h]
  int v22; // [esp+20h] [ebp-144h]
  _BYTE v23[16]; // [esp+24h] [ebp-140h] BYREF
  int v24; // [esp+34h] [ebp-130h] BYREF
  int v25; // [esp+38h] [ebp-12Ch] BYREF
  _BYTE v26[256]; // [esp+3Ch] [ebp-128h] BYREF
  int v27; // [esp+13Ch] [ebp-28h]
  char v28; // [esp+142h] [ebp-22h]
  char v29; // [esp+143h] [ebp-21h]
  int v30; // [esp+144h] [ebp-20h]
  System::TObject *v31; // [esp+148h] [ebp-1Ch]
  int v32; // [esp+14Ch] [ebp-18h]
  DWORD TickCount; // [esp+150h] [ebp-14h]
  int v34; // [esp+154h] [ebp-10h]
  int v35; // [esp+158h] [ebp-Ch]
  int v36; // [esp+15Ch] [ebp-8h]
  int v37; // [esp+160h] [ebp-4h]
  int savedregs; // [esp+164h] [ebp+0h] BYREF

  v20 = 0;
  memset(v19, 0, sizeof(v19));
  v25 = 0;
  v24 = 0;
  v22 = 0;
  v21 = 0;
  v37 = a1;
  v18 = &savedregs;
  v17[1] = (unsigned int)&loc_4B1B44;
  v17[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v17);
  TickCount = GetTickCount();
  v16[2] = (unsigned int)&savedregs;
  v16[1] = (unsigned int)&loc_4B1AD2;
  v16[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v16);
  v29 = 0;
  v32 = sub_407B30(0);
  v30 = 0;
  if ( GetTickCount() - *(_DWORD *)(v37 + 40) > 0xC8 )
  {
    *(_DWORD *)(v37 + 40) = GetTickCount();
    if ( *(_DWORD *)(v37 + 56) < *(_DWORD *)(*(_DWORD *)(v37 + 92) + 8) )
      v30 = Classes::TList::Get(*(Classes::TList **)(v37 + 92), *(_DWORD *)(v37 + 56));
    if ( *(_DWORD *)(v37 + 56) >= *(_DWORD *)(*(_DWORD *)(v37 + 92) + 8) - 1 )
      *(_DWORD *)(v37 + 56) = 0;
    else
      ++*(_DWORD *)(v37 + 56);
    if ( v30 != 0 && *(_BYTE *)(v30 + 24) != 0 && *off_4EA4E4[0] == 0 )
    {
      if ( *(_DWORD *)(v30 + 56) == 0 || (v1 = GetTickCount() - *(_DWORD *)(v30 + 56)) > sub_4B1590(&savedregs) )
      {
        v34 = sub_4AE19C(v37, v30);
        v28 = 1;
        if ( *(_DWORD *)(v30 + 48) > v34 )
          v28 = sub_4ADF04(v37, v30, *(_DWORD *)(v30 + 48) - v34);
        if ( v28 != 0 )
          *(_DWORD *)(v30 + 56) = GetTickCount();
      }
      unknown_libname_16(v23, v30 + 24);
      LOBYTE(v2) = 15;
      System::__linkproc__ PStrNCat(v23, dword_4B1B54, v2);
      unknown_libname_41(v16[0]);
      Sysutils::IntToStr(*(_DWORD *)(v37 + 56));
      Sysutils::IntToStr(*(_DWORD *)(*(_DWORD *)(v37 + 92) + 8));
      System::__linkproc__ LStrCatN(&v25, 4, v3, &dword_4B1B60, v21);
      System::__linkproc__ LStrToString(v26, v25, 255);
      LOBYTE(v4) = 30;
      System::__linkproc__ PStrNCpy((int *)off_4EA2BC, v26, v4);
    }
  }
  *(_DWORD *)(v37 + 136) = 0;
  v5 = *(_DWORD *)(v37 + 60);
  v6 = *(_DWORD *)(*(_DWORD *)(v37 + 92) + 8) - 1;
  v7 = v6 < v5;
  v8 = v6 - v5;
  if ( !v7 )
  {
    v27 = v8 + 1;
    v36 = v5;
    while ( 1 )
    {
      v30 = Classes::TList::Get(*(Classes::TList **)(v37 + 92), v36);
      if ( *(_DWORD *)(v37 + 64) >= *(_DWORD *)(*(_DWORD *)(v30 + 60) + 8) )
        v35 = 0;
      else
        v35 = *(_DWORD *)(v37 + 64);
      *(_DWORD *)(v37 + 64) = 0;
      while ( *(_DWORD *)(*(_DWORD *)(v30 + 60) + 8) > v35 )
      {
        v31 = (System::TObject *)Classes::TList::Get(*(Classes::TList **)(v30 + 60), v35);
        if ( *((_BYTE *)v31 + 764) == 0 )
        {
          if ( v32 - *((_DWORD *)v31 + 218) > *((_DWORD *)v31 + 219) )
          {
            *((_DWORD *)v31 + 218) = v32;
            v9 = GetTickCount();
            v11 = v31;
            if ( v9 - *((_DWORD *)v31 + 217) > *((_DWORD *)v31 + 216) )
            {
              *((_DWORD *)v31 + 217) = GetTickCount();
              sub_4B966C(v31);
            }
            unknown_libname_25(v31, v11, v10);
            ++*(_DWORD *)(v37 + 136);
          }
          ++*(_DWORD *)(v37 + 128);
          goto LABEL_32;
        }
        if ( GetTickCount() - *((_DWORD *)v31 + 192) <= 0x493E0 )
        {
LABEL_32:
          ++v35;
          if ( GetTickCount() - TickCount > *off_4EA058 )
          {
            unknown_libname_16(v23, (char *)v31 + 21);
            LOBYTE(v12) = 15;
            System::__linkproc__ PStrNCat(v23, dword_4B1B64, v12);
            unknown_libname_41(v16[0]);
            Sysutils::IntToStr(v36);
            Sysutils::IntToStr(v35);
            System::__linkproc__ LStrCatN(&v20, 4, v13, &dword_4B1B60, v19[0]);
            System::__linkproc__ LStrToString(v26, v20, 255);
            LOBYTE(v14) = 30;
            System::__linkproc__ PStrNCpy((int *)off_4EA374, v26, v14);
            v29 = 1;
            *(_DWORD *)(v37 + 64) = v35;
            break;
          }
        }
        else
        {
          Classes::TList::Delete(*(Classes::TList **)(v30 + 60), v35);
          System::TObject::Free(v31);
        }
      }
      if ( v29 == 0 )
      {
        ++v36;
        if ( --v27 != 0 )
          continue;
      }
      break;
    }
  }
  if ( *(_DWORD *)(*(_DWORD *)(v37 + 92) + 8) <= v36 )
  {
    *(_DWORD *)(v37 + 60) = 0;
    *(_DWORD *)(v37 + 124) = *(_DWORD *)(v37 + 128);
    *(_DWORD *)(v37 + 128) = 0;
    *(_DWORD *)(v37 + 132) = (*(_DWORD *)(v37 + 136) + *(_DWORD *)(v37 + 132)) / 2;
  }
  if ( v29 != 0 )
    *(_DWORD *)(v37 + 60) = v36;
  else
    *(_DWORD *)(v37 + 60) = 0;
  __writefsdword(0, v16[0]);
  *off_4EA384[0] = GetTickCount() - TickCount;
  if ( *off_4EA5C4 < *off_4EA384[0] )
    *off_4EA5C4 = *off_4EA384[0];
  __writefsdword(0, v17[0]);
  v18 = (int *)&loc_4B1B4B;
  unknown_libname_35(v19, 6);
  return unknown_libname_35(&v24, 2);
}


// ==================== [0x4B1B8C] sub_4B1B8C (size: 319) ====================
int __fastcall sub_4B1B8C(int a1)
{
  int v1; // eax
  int v2; // edx
  bool v3; // cc
  int v4; // edx
  int v5; // ecx
  int v6; // edx
  unsigned int v8[6]; // [esp-Ch] [ebp-34h] BYREF
  int v9; // [esp+Ch] [ebp-1Ch]
  char v10; // [esp+13h] [ebp-15h]
  int v11; // [esp+14h] [ebp-14h]
  int v12; // [esp+18h] [ebp-10h]
  DWORD TickCount; // [esp+1Ch] [ebp-Ch]
  int v14; // [esp+20h] [ebp-8h]
  int v15; // [esp+24h] [ebp-4h]
  int savedregs; // [esp+28h] [ebp+0h] BYREF

  v15 = a1;
  TickCount = GetTickCount();
  v10 = 0;
  v8[2] = (unsigned int)&savedregs;
  v8[1] = (unsigned int)&loc_4B1CB0;
  v8[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v8);
  v12 = sub_407B30(0);
  v1 = *(_DWORD *)(v15 + 76);
  v2 = *(_DWORD *)(*(_DWORD *)(v15 + 104) + 8) - 1;
  v3 = v2 < v1;
  v4 = v2 - v1;
  if ( !v3 )
  {
    v9 = v4 + 1;
    v14 = v1;
    while ( 1 )
    {
      v11 = Classes::TList::Get(*(Classes::TList **)(v15 + 104), v14);
      if ( *(_BYTE *)(v11 + 764) == 0 && v12 - *(_DWORD *)(v11 + 872) > *(_DWORD *)(v11 + 876) )
      {
        if ( GetTickCount() - *(_DWORD *)(v11 + 868) > *(_DWORD *)(v11 + 864) )
        {
          *(_DWORD *)(v11 + 868) = GetTickCount();
          sub_4B966C(v11);
        }
        if ( v12 - *(_DWORD *)(v11 + 872) > *(_DWORD *)(v11 + 876) )
        {
          v6 = v12;
          *(_DWORD *)(v11 + 872) = v12;
          unknown_libname_25(v11, v6, v5);
        }
      }
      if ( GetTickCount() - TickCount > *(_DWORD *)off_4EA4A8[0] )
        break;
      ++v14;
      if ( --v9 == 0 )
        goto LABEL_12;
    }
    *(_DWORD *)(v15 + 76) = v14;
    v10 = 1;
  }
LABEL_12:
  if ( v10 == 0 )
    *(_DWORD *)(v15 + 76) = 0;
  __writefsdword(0, v8[0]);
  return 0;
}


// ==================== [0x4B1CF0] sub_4B1CF0 (size: 319) ====================
int __fastcall sub_4B1CF0(int a1)
{
  int v1; // eax
  int v2; // edx
  bool v3; // cc
  int v4; // edx
  int v5; // ecx
  int v6; // edx
  unsigned int v8[6]; // [esp-Ch] [ebp-34h] BYREF
  int v9; // [esp+Ch] [ebp-1Ch]
  char v10; // [esp+13h] [ebp-15h]
  int v11; // [esp+14h] [ebp-14h]
  DWORD TickCount; // [esp+18h] [ebp-10h]
  int v13; // [esp+1Ch] [ebp-Ch]
  int v14; // [esp+20h] [ebp-8h]
  int v15; // [esp+24h] [ebp-4h]
  int savedregs; // [esp+28h] [ebp+0h] BYREF

  v15 = a1;
  TickCount = GetTickCount();
  v10 = 0;
  v8[2] = (unsigned int)&savedregs;
  v8[1] = (unsigned int)&loc_4B1E14;
  v8[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v8);
  v13 = sub_407B30(0);
  v1 = *(_DWORD *)(v15 + 80);
  v2 = *(_DWORD *)(*(_DWORD *)(v15 + 108) + 8) - 1;
  v3 = v2 < v1;
  v4 = v2 - v1;
  if ( !v3 )
  {
    v9 = v4 + 1;
    v14 = v1;
    while ( 1 )
    {
      v11 = Classes::TList::Get(*(Classes::TList **)(v15 + 108), v14);
      if ( *(_BYTE *)(v11 + 764) == 0 && v13 - *(_DWORD *)(v11 + 872) > *(_DWORD *)(v11 + 876) )
      {
        if ( GetTickCount() - *(_DWORD *)(v11 + 868) > *(_DWORD *)(v11 + 864) )
        {
          *(_DWORD *)(v11 + 868) = GetTickCount();
          sub_4B966C(v11);
        }
        if ( v13 - *(_DWORD *)(v11 + 872) > *(_DWORD *)(v11 + 876) )
        {
          v6 = v13;
          *(_DWORD *)(v11 + 872) = v13;
          unknown_libname_25(v11, v6, v5);
        }
      }
      if ( GetTickCount() - TickCount > *(_DWORD *)off_4EA4A8[0] )
        break;
      ++v14;
      if ( --v9 == 0 )
        goto LABEL_12;
    }
    *(_DWORD *)(v15 + 80) = v14;
    v10 = 1;
  }
LABEL_12:
  if ( v10 == 0 )
    *(_DWORD *)(v15 + 80) = 0;
  __writefsdword(0, v8[0]);
  return 0;
}


// ==================== [0x4B20B8] sub_4B20B8 (size: 570) ====================
int __usercall sub_4B20B8@<eax>(_DWORD *a1@<eax>, int a2@<ebx>, int a3@<edi>, int a4@<esi>, unsigned __int64 a5@<st0>)
{
  unsigned int v5; // eax
  DWORD v6; // eax
  int v7; // eax
  int v8; // ecx
  int v9; // eax
  int v10; // eax
  int v11; // ebx
  DWORD TickCount; // [esp+4h] [ebp-3Ch]
  unsigned int v14[3]; // [esp+8h] [ebp-38h] BYREF
  unsigned int v15[2]; // [esp+14h] [ebp-2Ch] BYREF
  int *v16; // [esp+1Ch] [ebp-24h]
  int v17; // [esp+20h] [ebp-20h]
  int v18; // [esp+24h] [ebp-1Ch]
  int v19; // [esp+28h] [ebp-18h]
  _DWORD v20[2]; // [esp+2Ch] [ebp-14h] BYREF
  int v21; // [esp+34h] [ebp-Ch] BYREF
  int i; // [esp+38h] [ebp-8h]
  _DWORD *v23; // [esp+3Ch] [ebp-4h]
  int savedregs; // [esp+40h] [ebp+0h] BYREF

  v21 = 0;
  v20[1] = 0;
  v20[0] = 0;
  v19 = a2;
  v18 = a4;
  v17 = a3;
  v23 = a1;
  v16 = &savedregs;
  v15[1] = (unsigned int)&loc_4B22E4;
  v15[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v15);
  a1[11] = GetTickCount();
  v14[2] = (unsigned int)&savedregs;
  v14[1] = (unsigned int)&loc_4B2282;
  v14[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v14);
  sub_4B0A88((int)v23);
  sub_4B1638((int)v23);
  sub_4B1B8C((int)v23);
  sub_4B1CF0((int)v23);
  if ( GetTickCount() - v23[9] > 0x3E8 )
  {
    v23[9] = GetTickCount();
    sub_4B1F44(v23);
    sub_4AECFC(v23);
    sub_4AED70(v23);
  }
  TickCount = GetTickCount();
  v5 = TickCount - v23[8];
  if ( v5 > 0x1F4 )
  {
    v23[8] = v5;
    sub_4AC78C(v23);
  }
  v6 = TickCount;
  if ( byte_4EBC79 != 0 )
    goto LABEL_8;
  if ( TickCount - v23[6] > 0x493E0 )
  {
    v6 = GetTickCount();
LABEL_8:
    v23[6] = v6;
    sub_491D54(*off_4EA414[0]);
    Sysutils::Time();
    unknown_libname_104(a5, HIDWORD(a5));
    v7 = sub_4AE7C0(v23);
    Sysutils::IntToStr(v7);
    System::__linkproc__ LStrCatN(&v21, 3, v8, &str__User_[1], v20[0]);
    sub_4E4274(v21);
    Grids::_16634(*off_4EA080[0]);
  }
  if ( GetTickCount() - v23[7] > 0x2710 )
  {
    v23[7] = GetTickCount();
    v9 = unknown_libname_674(v23);
    sub_48D630(*off_4EA3DC, v9);
    sub_49A61C(*off_4EA07C[0]);
    sub_48FE4C(*off_4EA080[0]);
    v10 = (*(int (__fastcall **)(_DWORD))(**(_DWORD **)off_4EA318[0] + 20))(*off_4EA318[0]) - 1;
    if ( v10 >= 0 )
    {
      for ( i = v10; i != -1; --i )
      {
        v11 = (*(int (__fastcall **)(_DWORD, int))(**(_DWORD **)off_4EA318[0] + 24))(*off_4EA318[0], i);
        if ( v11 < sub_407B30(v11) )
          (*(void (__fastcall **)(_DWORD, int))(**(_DWORD **)off_4EA318[0] + 68))(*off_4EA318[0], i);
      }
    }
  }
  __writefsdword(0, v14[0]);
  *off_4EA21C = GetTickCount() - v23[11];
  if ( *off_4EA200 < *off_4EA21C )
    *off_4EA200 = *off_4EA21C;
  __writefsdword(0, v15[0]);
  v16 = (int *)&loc_4B22EB;
  return unknown_libname_35(v20, 3);
}


// ==================== [0x4B232C] @DBConnAdmin@_16392 (size: 810) ====================
int __userpurge DBConnAdmin::_16392@<eax>(
        int a1@<eax>,
        int a2@<edx>,
        _DWORD *a3@<ecx>,
        int a4@<ebx>,
        int a5@<edi>,
        int a6@<esi>,
        int a7)
{
  unsigned int v7; // eax
  unsigned int v8; // eax
  unsigned int v9; // eax
  unsigned int v10; // eax
  bool v11; // cf
  unsigned int v12; // eax
  unsigned int v13; // eax
  unsigned int v14; // eax
  unsigned int v15; // eax
  unsigned int v16; // eax
  unsigned int v17; // eax
  unsigned int v18; // eax
  unsigned int v19; // eax
  unsigned int v20; // eax
  unsigned int v21; // eax
  unsigned int v22; // eax
  unsigned int v23; // eax
  unsigned __int16 v24; // ax
  int v25; // edx
  int v26; // eax
  int v27; // eax
  int v29; // [esp-34h] [ebp-54h]
  int v30; // [esp-30h] [ebp-50h]
  int v31; // [esp-30h] [ebp-50h]
  int v32; // [esp-2Ch] [ebp-4Ch]
  int v33; // [esp-2Ch] [ebp-4Ch]
  int v34; // [esp-2Ch] [ebp-4Ch]
  __int16 v35; // [esp-28h] [ebp-48h]
  __int16 v36; // [esp-28h] [ebp-48h]
  __int16 v37; // [esp-28h] [ebp-48h]
  unsigned int v38[3]; // [esp-24h] [ebp-44h] BYREF
  unsigned int v39[2]; // [esp-18h] [ebp-38h] BYREF
  int *v40; // [esp-10h] [ebp-30h]
  int v41; // [esp-Ch] [ebp-2Ch]
  int v42; // [esp-8h] [ebp-28h]
  int v43; // [esp-4h] [ebp-24h]
  int v44; // [esp+0h] [ebp-20h] BYREF
  _DWORD v45[3]; // [esp+4h] [ebp-1Ch] BYREF
  int v46; // [esp+10h] [ebp-10h] BYREF
  _DWORD *v47; // [esp+14h] [ebp-Ch]
  int v48; // [esp+18h] [ebp-8h]
  int v49; // [esp+1Ch] [ebp-4h]
  int savedregs; // [esp+20h] [ebp+0h] BYREF

  v46 = 0;
  memset(v45, 0, sizeof(v45));
  v44 = 0;
  v43 = a4;
  v42 = a6;
  v41 = a5;
  v47 = a3;
  v48 = a2;
  v49 = a1;
  v40 = &savedregs;
  v39[1] = (unsigned int)&loc_4B2646;
  v39[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v39);
  v38[2] = (unsigned int)&savedregs;
  v38[1] = (unsigned int)&loc_4B2617;
  v38[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v38);
  if ( a3 == nullptr )
    goto LABEL_38;
  if ( a7 != 0 )
    sub_409790(a7, &v46);
  else
    unknown_libname_34(&v46);
  v7 = *((unsigned __int16 *)v47 + 2);
  if ( v7 >= 0x40E )
  {
    if ( *((unsigned __int16 *)v47 + 2) > 0xBC9u )
    {
      v20 = v7 - 3018;
      v11 = v20 < 2;
      v21 = v20 - 2;
      if ( !v11 )
      {
        v22 = v21 - 4;
        v11 = v22 < 2;
        v23 = v22 - 2;
        if ( !v11 )
        {
          if ( v23 == 4 )
          {
            sub_45633C(v46, v45);
            sub_4B865C(v45[0], 0, 0, 0, 0);
            goto LABEL_33;
          }
          goto LABEL_32;
        }
      }
    }
    else
    {
      if ( v7 == 3017 )
      {
        v36 = *((_WORD *)v47 + 4);
        v33 = *(unsigned __int16 *)v47;
        v30 = (unsigned __int16)sub_407B24(*v47);
        LOWORD(v25) = *((_WORD *)v47 + 5);
        HIWORD(v26) = HIWORD(v47);
        LOWORD(v26) = *((_WORD *)v47 + 3);
        v27 = unknown_libname_78(v26, v25);
        sub_4B865C(0, v27, v30, v33, v36);
        goto LABEL_33;
      }
      v17 = v7 - 1038;
      v11 = v17 < 4;
      v18 = v17 - 4;
      if ( v11 )
      {
LABEL_30:
        v37 = *((_WORD *)v47 + 5);
        v34 = *v47;
        v31 = *((unsigned __int16 *)v47 + 3);
        v29 = *((unsigned __int16 *)v47 + 4);
        sub_45633C(v46, &v44);
        sub_4B865C(v44, v29, v31, v34, v37);
        goto LABEL_33;
      }
      v19 = v18 - 1;
      if ( v19 == 0 )
      {
        sub_4B865C(v46, *((unsigned __int16 *)v47 + 4), *((unsigned __int16 *)v47 + 3), *v47, *((_WORD *)v47 + 5));
        goto LABEL_33;
      }
      if ( v19 - 1967 >= 7 )
      {
LABEL_32:
        sub_4B865C(0, *((unsigned __int16 *)v47 + 4), *((unsigned __int16 *)v47 + 3), *v47, *((_WORD *)v47 + 5));
        goto LABEL_33;
      }
    }
    v35 = *((_WORD *)v47 + 4);
    v32 = *(unsigned __int16 *)v47;
    v24 = sub_407B24(*v47);
    sub_4B865C(0, 0, v24, v32, v35);
    goto LABEL_33;
  }
  if ( *((unsigned __int16 *)v47 + 2) >= 0x3F3u )
  {
    v10 = v7 - 1011;
    v11 = v10 < 5;
    v12 = v10 - 5;
    if ( !v11 )
    {
      v13 = v12 - 4;
      v11 = v13 < 8;
      v14 = v13 - 8;
      if ( !v11 )
      {
        v15 = v14 - 3;
        v11 = v15 < 2;
        v16 = v15 - 2;
        if ( !v11 && v16 != 1 )
          goto LABEL_32;
      }
    }
    goto LABEL_30;
  }
  v8 = v7 - 80;
  if ( v8 != 0 )
  {
    v9 = v8 - 920;
    if ( v9 != 0 && v9 - 3 >= 3 )
      goto LABEL_32;
    goto LABEL_30;
  }
  sub_4B865C(0, *((unsigned __int16 *)v47 + 4), *((unsigned __int16 *)v47 + 3), *v47, 0);
LABEL_33:
  if ( *(_BYTE *)(v48 + 1468) != 0
    && ((unsigned __int16)(*((_WORD *)v47 + 2) - 3010) < 7u
     || (unsigned __int16)(*((_WORD *)v47 + 2) - 3018) < 2u
     || (unsigned __int16)(*((_WORD *)v47 + 2) - 3024) < 2u) )
  {
    *(_DWORD *)(v48 + 872) -= 100;
  }
LABEL_38:
  __writefsdword(0, v38[0]);
  __writefsdword(0, v39[0]);
  v40 = (int *)&loc_4B264D;
  return unknown_libname_35(&v44, 5);
}


// ==================== [0x4B2790] sub_4B2790 (size: 365) ====================
char __fastcall sub_4B2790(int a1, int *a2, int *a3)
{
  int v4; // [esp+0h] [ebp-14h]

  v4 = *(_DWORD *)(a1 + 760);
  *a2 = *(_DWORD *)(a1 + 36);
  *a3 = *(_DWORD *)(a1 + 40);
  switch ( *(_BYTE *)(a1 + 44) )
  {
    case 0:
      if ( *a3 > 0 )
        --*a3;
      break;
    case 1:
      if ( *a2 > 0 && *a3 < *(_DWORD *)(v4 + 32) - 1 )
      {
        ++*a2;
        --*a3;
      }
      break;
    case 2:
      if ( *a2 < *(_DWORD *)(v4 + 28) - 1 )
        ++*a2;
      break;
    case 3:
      if ( *a2 < *(_DWORD *)(v4 + 28) - 1 && *a3 < *(_DWORD *)(v4 + 32) - 1 )
      {
        ++*a2;
        ++*a3;
      }
      break;
    case 4:
      if ( *a3 < *(_DWORD *)(v4 + 32) - 1 )
        ++*a3;
      break;
    case 5:
      if ( *a2 < *(_DWORD *)(v4 + 28) - 1 && *a3 > 0 )
      {
        --*a2;
        ++*a3;
      }
      break;
    case 6:
      if ( *a2 > 0 )
        --*a2;
      break;
    case 7:
      if ( *a2 > 0 && *a3 > 0 )
      {
        --*a2;
        --*a3;
      }
      break;
    default:
      return 1;
  }
  return 1;
}


// ==================== [0x4B2900] sub_4B2900 (size: 361) ====================
char __fastcall sub_4B2900(int a1, int *a2, int *a3)
{
  int v4; // [esp+0h] [ebp-14h]

  v4 = *(_DWORD *)(a1 + 760);
  *a2 = *(_DWORD *)(a1 + 36);
  *a3 = *(_DWORD *)(a1 + 40);
  switch ( *(_BYTE *)(a1 + 44) )
  {
    case 0:
      if ( *a3 < *(_DWORD *)(v4 + 32) - 1 )
        ++*a3;
      break;
    case 1:
      if ( *a2 < *(_DWORD *)(v4 + 28) - 1 && *a3 > 0 )
      {
        --*a2;
        ++*a3;
      }
      break;
    case 2:
      if ( *a2 > 0 )
        --*a2;
      break;
    case 3:
      if ( *a2 > 0 && *a3 > 0 )
      {
        --*a2;
        --*a3;
      }
      break;
    case 4:
      if ( *a3 > 0 )
        --*a3;
      break;
    case 5:
      if ( *a2 > 0 && *a3 < *(_DWORD *)(v4 + 32) - 1 )
      {
        ++*a2;
        --*a3;
      }
      break;
    case 6:
      if ( *a2 < *(_DWORD *)(v4 + 28) - 1 )
        ++*a2;
      break;
    case 7:
      if ( *a2 < *(_DWORD *)(v4 + 28) - 1 && *a3 < *(_DWORD *)(v4 + 32) - 1 )
      {
        ++*a2;
        ++*a3;
      }
      break;
    default:
      return 1;
  }
  return 1;
}


// ==================== [0x4B2A6C] sub_4B2A6C (size: 460) ====================
bool __fastcall sub_4B2A6C(int a1, int a2, int a3, int a4, int a5, _DWORD *a6, _DWORD *a7)
{
  *a6 = a2;
  *a7 = a3;
  switch ( a4 )
  {
    case 0:
      if ( *a7 > a5 - 1 )
        *a7 -= a5;
      break;
    case 1:
      if ( *a6 > a5 - 1 && *a7 < *(_DWORD *)(a1 + 32) - a5 )
      {
        *a6 += a5;
        *a7 -= a5;
      }
      break;
    case 2:
      if ( *a6 < *(_DWORD *)(a1 + 28) - a5 )
        *a6 += a5;
      break;
    case 3:
      if ( *a6 < *(_DWORD *)(a1 + 28) - a5 && *a7 < *(_DWORD *)(a1 + 32) - a5 )
      {
        *a6 += a5;
        *a7 += a5;
      }
      break;
    case 4:
      if ( *a7 < *(_DWORD *)(a1 + 32) - a5 )
        *a7 += a5;
      break;
    case 5:
      if ( *a6 < *(_DWORD *)(a1 + 28) - a5 && *a7 > a5 - 1 )
      {
        *a6 -= a5;
        *a7 += a5;
      }
      break;
    case 6:
      if ( *a6 > a5 - 1 )
        *a6 -= a5;
      break;
    case 7:
      if ( *a6 > a5 - 1 && *a7 > a5 - 1 )
      {
        *a6 -= a5;
        *a7 -= a5;
      }
      break;
    default:
      return *a6 != a2 || *a7 != a3;
  }
  return *a6 != a2 || *a7 != a3;
}


// ==================== [0x4B2C38] sub_4B2C38 (size: 312) ====================
char __fastcall sub_4B2C38(int a1, int a2, int a3, int a4)
{
  int v5; // [esp+0h] [ebp-18h]
  int v6; // [esp+4h] [ebp-14h]
  char v7; // [esp+Bh] [ebp-Dh]

  v7 = 4;
  if ( a1 >= a3 )
  {
    if ( a1 == a3 )
      v6 = 0;
    else
      v6 = -1;
  }
  else
  {
    v6 = 1;
  }
  if ( (int)abs32(a2 - a4) > 2 && a3 - 1 <= a1 && a3 + 1 >= a1 )
    v6 = 0;
  if ( a2 >= a4 )
  {
    if ( a2 == a4 )
      v5 = 0;
    else
      v5 = -1;
  }
  else
  {
    v5 = 1;
  }
  if ( (int)abs32(a1 - a3) > 2 && a4 - 1 < a2 && a4 + 1 >= a2 )
    v5 = 0;
  if ( v6 == 0 && v5 == -1 )
    v7 = 0;
  if ( v6 == 1 && v5 == -1 )
    v7 = 1;
  if ( v6 == 1 && v5 == 0 )
    v7 = 2;
  if ( v6 == 1 && v5 == 1 )
    v7 = 3;
  if ( v6 == 0 && v5 == 1 )
    v7 = 4;
  if ( v6 == -1 && v5 == 1 )
    v7 = 5;
  if ( v6 == -1 && v5 == 0 )
    v7 = 6;
  if ( v6 == -1 && v5 == -1 )
    return 7;
  return v7;
}


// ==================== [0x4B3050] sub_4B3050 (size: 1325) ====================
int __fastcall sub_4B3050(int a1, int a2)
{
  int result; // eax
  void *v3; // edi
  int v4; // [esp+8h] [ebp-28h]
  int v5; // [esp+Ch] [ebp-24h]
  int v6; // [esp+10h] [ebp-20h]
  int v7; // [esp+14h] [ebp-1Ch]
  int v8; // [esp+18h] [ebp-18h]
  _WORD *v9; // [esp+1Ch] [ebp-14h]
  void *Mem; // [esp+20h] [ebp-10h]
  int i; // [esp+24h] [ebp-Ch]
  int j; // [esp+24h] [ebp-Ch]
  int k; // [esp+24h] [ebp-Ch]

  v7 = a1 + 28;
  System::__linkproc__ PStrNCpy(*(_DWORD *)a2 + 21, a1 + 28, 14);
  System::__linkproc__ PStrNCpy(*(_DWORD *)a2 + 4, v7 + 15, 16);
  *(_DWORD *)(*(_DWORD *)a2 + 36) = *(unsigned __int16 *)(v7 + 32);
  *(_DWORD *)(*(_DWORD *)a2 + 40) = *(unsigned __int16 *)(v7 + 34);
  *(_BYTE *)(*(_DWORD *)a2 + 44) = *(_BYTE *)(v7 + 36);
  *(_BYTE *)(*(_DWORD *)a2 + 46) = *(_BYTE *)(v7 + 37);
  *(_BYTE *)(*(_DWORD *)a2 + 45) = *(_BYTE *)(v7 + 38);
  *(_BYTE *)(*(_DWORD *)a2 + 47) = *(_BYTE *)(v7 + 39);
  *(_DWORD *)(*(_DWORD *)a2 + 48) = *(_DWORD *)(v7 + 40);
  qmemcpy((void *)(*(_DWORD *)a2 + 52), (const void *)(v7 + 44), 0x28u);
  System::Move((const void *)(v7 + 84), (void *)(*(_DWORD *)a2 + 96), 24);
  System::__linkproc__ PStrNCpy(*(_DWORD *)a2 + 120, v7 + 108, 16);
  *(_DWORD *)(*(_DWORD *)a2 + 140) = *(unsigned __int16 *)(v7 + 126);
  *(_DWORD *)(*(_DWORD *)a2 + 144) = *(unsigned __int16 *)(v7 + 128);
  *(_DWORD *)(*(_DWORD *)a2 + 172) = *(_DWORD *)(v7 + 216);
  *(_BYTE *)(*(_DWORD *)a2 + 176) = *(_BYTE *)(v7 + 220) != 0;
  *(_BYTE *)(*(_DWORD *)a2 + 178) = *(_BYTE *)(v7 + 221);
  *(_BYTE *)(*(_DWORD *)a2 + 673) = *(_BYTE *)(v7 + 222);
  *(_DWORD *)(*(_DWORD *)a2 + 180) = *(unsigned __int8 *)(v7 + 223);
  *(_DWORD *)(*(_DWORD *)a2 + 184) = *(unsigned __int8 *)(v7 + 224);
  *(_DWORD *)(*(_DWORD *)a2 + 188) = *(unsigned __int8 *)(v7 + 225);
  *(_DWORD *)(*(_DWORD *)a2 + 192) = *(unsigned __int8 *)(v7 + 226);
  System::__linkproc__ PStrNCpy(*(_DWORD *)a2 + 1420, v7 + 227, 10);
  *(_DWORD *)(*(_DWORD *)a2 + 196) = *(unsigned __int8 *)(v7 + 238);
  *(_BYTE *)(*(_DWORD *)a2 + 200) = *(_BYTE *)(v7 + 239);
  *(_DWORD *)(*(_DWORD *)a2 + 248) = *(_DWORD *)(v7 + 284);
  *(_BYTE *)(*(_DWORD *)a2 + 252) = *(_BYTE *)(v7 + 288);
  *(_WORD *)(*(_DWORD *)a2 + 268) = *(_WORD *)(v7 + 290);
  *(double *)(*(_DWORD *)a2 + 256) = sub_453994(v7 + 292);
  *(_BYTE *)(*(_DWORD *)a2 + 270) = *(_BYTE *)(v7 + 300);
  System::Move((const void *)(v7 + 329), (void *)(*(_DWORD *)a2 + 271), 13);
  System::Move((const void *)(v7 + 342), (void *)(*(_DWORD *)a2 + 284), 13);
  System::Move((const void *)(v7 + 355), (void *)(*(_DWORD *)a2 + 297), 100);
  v6 = a1 + 484;
  qmemcpy((void *)(*(_DWORD *)a2 + 1056), (const void *)(a1 + 484), 0x18u);
  qmemcpy((void *)(*(_DWORD *)a2 + 1080), (const void *)(a1 + 508), 0x18u);
  qmemcpy((void *)(*(_DWORD *)a2 + 1104), (const void *)(a1 + 532), 0x18u);
  qmemcpy((void *)(*(_DWORD *)a2 + 1128), (const void *)(a1 + 580), 0x18u);
  qmemcpy((void *)(*(_DWORD *)a2 + 1152), (const void *)(a1 + 556), 0x18u);
  qmemcpy((void *)(*(_DWORD *)a2 + 1176), (const void *)(a1 + 604), 0x18u);
  qmemcpy((void *)(*(_DWORD *)a2 + 1200), (const void *)(a1 + 628), 0x18u);
  qmemcpy((void *)(*(_DWORD *)a2 + 1224), (const void *)(a1 + 652), 0x18u);
  qmemcpy((void *)(*(_DWORD *)a2 + 1248), (const void *)(a1 + 676), 0x18u);
  for ( i = 0; i != 46; ++i )
  {
    if ( *(_WORD *)(v6 + 24 * i + 220) != 0 )
    {
      Mem = (void *)System::__linkproc__ GetMem(24);
      qmemcpy(Mem, (const void *)(v6 + 24 * i + 216), 0x18u);
      Classes::TList::Add(*(Classes::TList **)(*(_DWORD *)a2 + 1036), Mem);
    }
  }
  v5 = a1 + 1804;
  for ( j = 0; j != 20; ++j )
  {
    if ( *(_WORD *)(v5 + 8 * j) == 0 )
      break;
    v8 = sub_4AE398(*off_4EA490, *(unsigned __int16 *)(v5 + 8 * j));
    if ( v8 != 0 )
    {
      v9 = (_WORD *)System::__linkproc__ GetMem(12);
      *(_DWORD *)v9 = v8;
      v9[2] = *(_WORD *)(v5 + 8 * j);
      *((_BYTE *)v9 + 6) = *(_BYTE *)(v5 + 8 * j + 2);
      *((_BYTE *)v9 + 7) = *(_BYTE *)(v5 + 8 * j + 3);
      *((_DWORD *)v9 + 2) = *(_DWORD *)(v5 + 8 * j + 4);
      Classes::TList::Add(*(Classes::TList **)(*(_DWORD *)a2 + 1052), v9);
    }
  }
  v4 = a1 + 1964;
  for ( k = 0; k != 50; ++k )
  {
    result = 3 * k;
    if ( *(_WORD *)(v4 + 24 * k + 4) != 0 )
    {
      v3 = (void *)System::__linkproc__ GetMem(24);
      qmemcpy(v3, (const void *)(v4 + 24 * k), 0x18u);
      result = Classes::TList::Add(*(Classes::TList **)(*(_DWORD *)a2 + 1272), v3);
    }
  }
  return result;
}


// ==================== [0x4B3580] sub_4B3580 (size: 1253) ====================
char *__fastcall sub_4B3580(int a1, int a2)
{
  char *result; // eax
  int v3; // [esp+8h] [ebp-2Ch]
  int v4; // [esp+Ch] [ebp-28h]
  int v5; // [esp+14h] [ebp-20h]
  char *v6; // [esp+14h] [ebp-20h]
  int v7; // [esp+1Ch] [ebp-18h]
  int v8; // [esp+20h] [ebp-14h]
  int v9; // [esp+24h] [ebp-10h]
  int v10; // [esp+28h] [ebp-Ch]
  int i; // [esp+28h] [ebp-Ch]
  int v12; // [esp+28h] [ebp-Ch]

  v7 = a2 + 28;
  System::__linkproc__ PStrNCpy(a2 + 28, a1 + 21, 14);
  System::__linkproc__ PStrNCpy(v7 + 15, a1 + 4, 16);
  *(_WORD *)(v7 + 32) = *(_WORD *)(a1 + 36);
  *(_WORD *)(v7 + 34) = *(_WORD *)(a1 + 40);
  *(_BYTE *)(v7 + 36) = *(_BYTE *)(a1 + 44);
  *(_BYTE *)(v7 + 37) = *(_BYTE *)(a1 + 46);
  *(_BYTE *)(v7 + 38) = *(_BYTE *)(a1 + 45);
  *(_BYTE *)(v7 + 39) = *(_BYTE *)(a1 + 47);
  *(_DWORD *)(v7 + 40) = *(_DWORD *)(a1 + 48);
  qmemcpy((void *)(v7 + 44), (const void *)(a1 + 52), 0x28u);
  *(_WORD *)(v7 + 56) = *(_WORD *)(a1 + 420);
  *(_WORD *)(v7 + 58) = *(_WORD *)(a1 + 422);
  System::Move((const void *)(a1 + 96), (void *)(v7 + 84), 24);
  System::__linkproc__ PStrNCpy(v7 + 108, a1 + 120, 16);
  *(_WORD *)(v7 + 126) = *(_WORD *)(a1 + 140);
  *(_WORD *)(v7 + 128) = *(_WORD *)(a1 + 144);
  *(_DWORD *)(v7 + 216) = *(_DWORD *)(a1 + 172);
  *(_BYTE *)(v7 + 220) = *(_BYTE *)(a1 + 176) != 0;
  *(_BYTE *)(v7 + 221) = *(_BYTE *)(a1 + 178);
  *(_BYTE *)(v7 + 222) = *(_BYTE *)(a1 + 673);
  *(_BYTE *)(v7 + 223) = *(_BYTE *)(a1 + 180);
  *(_BYTE *)(v7 + 224) = *(_BYTE *)(a1 + 184);
  *(_BYTE *)(v7 + 225) = *(_BYTE *)(a1 + 188);
  *(_BYTE *)(v7 + 226) = *(_BYTE *)(a1 + 192);
  System::__linkproc__ PStrNCpy(v7 + 227, a1 + 1420, 10);
  *(_BYTE *)(v7 + 238) = *(_BYTE *)(a1 + 196);
  *(_BYTE *)(v7 + 239) = *(_BYTE *)(a1 + 200);
  *(_DWORD *)(v7 + 284) = *(_DWORD *)(a1 + 248);
  *(_BYTE *)(v7 + 288) = *(_BYTE *)(a1 + 252);
  *(_WORD *)(v7 + 290) = *(_WORD *)(a1 + 268);
  sub_453978(*(_DWORD *)(a1 + 256), *(_DWORD *)(a1 + 260));
  *(_DWORD *)(v7 + 292) = v3;
  *(_DWORD *)(v7 + 296) = v4;
  *(_BYTE *)(v7 + 300) = *(_BYTE *)(a1 + 270);
  System::Move((const void *)(a1 + 271), (void *)(v7 + 329), 13);
  System::Move((const void *)(a1 + 284), (void *)(v7 + 342), 13);
  System::Move((const void *)(a1 + 297), (void *)(v7 + 355), 100);
  qmemcpy((void *)(a2 + 484), (const void *)(a1 + 1056), 0x18u);
  qmemcpy((void *)(a2 + 508), (const void *)(a1 + 1080), 0x18u);
  qmemcpy((void *)(a2 + 532), (const void *)(a1 + 1104), 0x18u);
  qmemcpy((void *)(a2 + 580), (const void *)(a1 + 1128), 0x18u);
  qmemcpy((void *)(a2 + 556), (const void *)(a1 + 1152), 0x18u);
  qmemcpy((void *)(a2 + 604), (const void *)(a1 + 1176), 0x18u);
  qmemcpy((void *)(a2 + 628), (const void *)(a1 + 1200), 0x18u);
  qmemcpy((void *)(a2 + 652), (const void *)(a1 + 1224), 0x18u);
  qmemcpy((void *)(a2 + 676), (const void *)(a1 + 1248), 0x18u);
  if ( *(_DWORD *)(*(_DWORD *)(a1 + 1036) + 8) - 1 >= 0 )
  {
    v5 = *(_DWORD *)(*(_DWORD *)(a1 + 1036) + 8);
    v10 = 0;
    do
    {
      if ( v10 >= 46 )
        break;
      qmemcpy(
        (void *)(a2 + 484 + 24 * v10 + 216),
        (const void *)Classes::TList::Get(*(Classes::TList **)(a1 + 1036), v10),
        0x18u);
      ++v10;
      --v5;
    }
    while ( v5 != 0 );
  }
  for ( i = 0; i != 20; ++i )
  {
    if ( *(_DWORD *)(*(_DWORD *)(a1 + 1052) + 8) <= i )
      break;
    v8 = *(_DWORD *)(Classes::TList::Get(*(Classes::TList **)(a1 + 1052), i) + 4);
    v9 = *(_DWORD *)(Classes::TList::Get(*(Classes::TList **)(a1 + 1052), i) + 8);
    *(_DWORD *)(a2 + 8 * i + 1804) = v8;
    *(_DWORD *)(a2 + 8 * i + 1808) = v9;
  }
  result = (char *)(*(_DWORD *)(*(_DWORD *)(a1 + 1272) + 8) - 1);
  if ( (int)result >= 0 )
  {
    result = *(char **)(*(_DWORD *)(a1 + 1272) + 8);
    v6 = result;
    v12 = 0;
    do
    {
      if ( v12 < 50 )
      {
        result = (char *)Classes::TList::Get(*(Classes::TList **)(a1 + 1272), v12);
        qmemcpy((void *)(a2 + 1964 + 24 * v12), result, 0x18u);
      }
      ++v12;
      --v6;
    }
    while ( v6 != nullptr );
  }
  return result;
}


// ==================== [0x4B3BEC] sub_4B3BEC (size: 308) ====================
int __fastcall sub_4B3BEC(int a1, int a2)
{
  int v2; // eax
  int v3; // edx
  int v4; // ecx
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-18h] [ebp-34h] BYREF
  void *v7; // [esp-14h] [ebp-30h]
  _strings *v8; // [esp-10h] [ebp-2Ch]
  unsigned int v9[2]; // [esp-Ch] [ebp-28h] BYREF
  int *v10; // [esp-4h] [ebp-20h]
  void *v11; // [esp+0h] [ebp-1Ch] BYREF
  int v12; // [esp+4h] [ebp-18h] BYREF
  __int16 v13; // [esp+Ah] [ebp-12h]
  char *v14; // [esp+Ch] [ebp-10h] BYREF
  int v15; // [esp+10h] [ebp-Ch] BYREF
  int v16; // [esp+14h] [ebp-8h]
  int v17; // [esp+18h] [ebp-4h]
  int savedregs; // [esp+1Ch] [ebp+0h] BYREF

  v11 = nullptr;
  v15 = 0;
  v14 = nullptr;
  v16 = a2;
  v17 = a1;
  unknown_libname_44(a2);
  v10 = &savedregs;
  v9[1] = (unsigned int)&loc_4B3D15;
  v9[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v9);
  if ( (unsigned __int8)sub_4E65B8() != 0 )
  {
    v8 = (_strings *)&savedregs;
    v7 = &loc_4B3C64;
    ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
    __writefsdword(0, (unsigned int)&ExceptionList);
    sub_450080(*off_4EA034);
    unknown_libname_34(off_4EA2FC);
    __writefsdword(0, (unsigned int)ExceptionList);
    v8 = (_strings *)&loc_4B3C6B;
    Webadapt::TBaseAdapterErrorsList::GetErrorCount((Webadapt::TBaseAdapterErrorsList *)*off_4EA034);
    v2 = unknown_libname_43(v16);
    v13 = v2 + 6;
    LOWORD(v2) = v17 ^ 0xAA;
    LOWORD(v3) = v13;
    v12 = unknown_libname_78(v2, v3);
    sub_4565B0(&v12, 4, &v15, v9[0]);
    v8 = &str___64[1];
    Sysutils::IntToStr(v17);
    v7 = v11;
    ExceptionList = (struct _EXCEPTION_REGISTRATION_RECORD *)&str___65[1];
    System::__linkproc__ LStrCatN(&v14, 6, v4, v15, &str___66[1]);
    *off_4EA698 = 1;
    sub_451C48(*(Scktcomp::TCustomWinSocket **)(*(_DWORD *)(*off_4EA298 + 736) + 128), v14);
  }
  __writefsdword(0, v9[0]);
  v10 = (int *)&loc_4B3D1C;
  unknown_libname_34(&v11);
  return unknown_libname_35(&v14, 3);
}


// ==================== [0x4B3D44] sub_4B3D44 (size: 771) ====================
char __userpurge sub_4B3D44@<al>(
        int a1@<eax>,
        _DWORD *a2@<edx>,
        _DWORD *a3@<ecx>,
        unsigned __int64 a4@<st0>,
        DWORD a5,
        int a6)
{
  int v6; // eax
  int v7; // eax
  int v8; // edx
  int v9; // eax
  unsigned int v11[2]; // [esp-4h] [ebp-78h] BYREF
  int *v12; // [esp+4h] [ebp-70h]
  unsigned int v13[2]; // [esp+8h] [ebp-6Ch] BYREF
  int *v14; // [esp+10h] [ebp-64h]
  void *v15; // [esp+18h] [ebp-5Ch] BYREF
  int v16; // [esp+1Ch] [ebp-58h] BYREF
  char v17; // [esp+20h] [ebp-54h]
  int v18; // [esp+24h] [ebp-50h] BYREF
  int v19; // [esp+28h] [ebp-4Ch] BYREF
  int v20; // [esp+2Ch] [ebp-48h] BYREF
  unsigned __int16 v21; // [esp+30h] [ebp-44h]
  char v22; // [esp+3Bh] [ebp-39h]
  int v23; // [esp+3Ch] [ebp-38h] BYREF
  int v24; // [esp+40h] [ebp-34h] BYREF
  int v25; // [esp+44h] [ebp-30h] BYREF
  int v26; // [esp+48h] [ebp-2Ch]
  int v27; // [esp+4Ch] [ebp-28h] BYREF
  int v28; // [esp+50h] [ebp-24h] BYREF
  __int16 v29; // [esp+54h] [ebp-20h]
  __int16 v30; // [esp+56h] [ebp-1Eh]
  int v31; // [esp+58h] [ebp-1Ch] BYREF
  int v32; // [esp+5Ch] [ebp-18h]
  int v33; // [esp+60h] [ebp-14h]
  char v34; // [esp+67h] [ebp-Dh]
  _DWORD *v35; // [esp+68h] [ebp-Ch]
  _DWORD *v36; // [esp+6Ch] [ebp-8h]
  int v37; // [esp+70h] [ebp-4h]
  int savedregs; // [esp+74h] [ebp+0h] BYREF

  v16 = 0;
  v15 = nullptr;
  v18 = 0;
  v19 = 0;
  v28 = 0;
  v27 = 0;
  v26 = 0;
  v25 = 0;
  v24 = 0;
  v23 = 0;
  v35 = a3;
  v36 = a2;
  v37 = a1;
  v14 = &savedregs;
  v13[1] = (unsigned int)&loc_4B4036;
  v13[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v13);
  v22 = 0;
  v34 = 0;
  v33 = sub_407B30(0);
  while ( 1 )
  {
    if ( GetTickCount() - v33 > a5 )
    {
      dword_4EBB6C = dword_4EBB68;
      goto LABEL_18;
    }
    unknown_libname_34(&v28);
    v12 = &savedregs;
    v11[1] = (unsigned int)&loc_4B3E1A;
    v11[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
    __writefsdword(0, (unsigned int)v11);
    sub_450080(*off_4EA034);
    if ( System::__linkproc__ LStrPos(&str___67[1], *off_4EA2FC) > 0 )
    {
      unknown_libname_37(&v28, *off_4EA2FC);
      unknown_libname_34(off_4EA2FC);
    }
    __writefsdword(0, v11[0]);
    v12 = (int *)&loc_4B3E21;
    Webadapt::TBaseAdapterErrorsList::GetErrorCount((Webadapt::TBaseAdapterErrorsList *)*off_4EA034);
    if ( v28 != 0 )
      break;
    Sleep(0);
  }
  unknown_libname_34(&v27);
  sub_4540F0(&v19, &v27);
  unknown_libname_37(&v28, v19);
  if ( v27 != 0 )
  {
    v17 = 47;
    sub_453DBC(&v18, 0);
    unknown_libname_37(&v27, v18);
    v32 = unknown_libname_43(v27);
    if ( v32 >= 16 && (v6 = sub_453B20(v26, 0)) == v37 )
    {
      v7 = sub_453B20(v26, 0);
      LOWORD(v7) = v7 ^ 0xAA;
      v30 = v7;
      v29 = v32;
      LOWORD(v8) = v32;
      v31 = unknown_libname_78(v7, v8);
      sub_4565B0(&v31, 4, &v25, v13[0]);
      v9 = unknown_libname_43(v25);
      if ( (unsigned __int8)sub_454474(v27, v25, v9) != 0 )
      {
        if ( v32 == 16 )
        {
          unknown_libname_37(&v24, v27);
          unknown_libname_34(&v23);
        }
        else
        {
          System::__linkproc__ LStrCopy(&v24);
          v12 = &v23;
          unknown_libname_43(v27);
          System::__linkproc__ LStrCopy(v12);
        }
        sub_456288(v24, &v20);
        *v36 = v21;
        *v35 = v20;
        unknown_libname_36(a6, v23);
        v22 = 1;
        v34 = 1;
      }
      else
      {
        ++*off_4EA218[0];
      }
    }
    else
    {
      ++*off_4EA218[0];
    }
  }
LABEL_18:
  if ( v22 == 0 )
  {
    Sysutils::Time();
    unknown_libname_104(a4, HIDWORD(a4));
    System::__linkproc__ LStrCat3((int)&v16, &str__RunDB__DB_Wait[1], v15);
    sub_4E4274(v16);
  }
  if ( GetTickCount() - v33 > *off_4EA444 )
    *off_4EA444 = GetTickCount() - v33;
  *off_4EA698 = 0;
  __writefsdword(0, v13[0]);
  v14 = (int *)&loc_4B403D;
  unknown_libname_35(&v15, 2);
  unknown_libname_35(&v18, 2);
  unknown_libname_35(&v23, 6);
  return v34;
}


// ==================== [0x4B4080] sub_4B4080 (size: 582) ====================
char __userpurge sub_4B4080@<al>(int a1@<eax>, int a2@<edx>, int a3@<ecx>, unsigned __int64 a4@<st0>, int a5, int a6)
{
  int v6; // ecx
  int v7; // ecx
  int v8; // ecx
  char v9; // zf
  int v10; // ebx
  void *v12; // [esp-10h] [ebp-190h]
  unsigned int v13[2]; // [esp-Ch] [ebp-18Ch] BYREF
  int *v14; // [esp-4h] [ebp-184h]
  char v15; // [esp+4h] [ebp-17Ch]
  int v16; // [esp+8h] [ebp-178h] BYREF
  void *v17; // [esp+Ch] [ebp-174h] BYREF
  void *v18; // [esp+10h] [ebp-170h] BYREF
  _BYTE v19[256]; // [esp+14h] [ebp-16Ch] BYREF
  _BYTE v20[13]; // [esp+114h] [ebp-6Ch] BYREF
  _BYTE v21[15]; // [esp+121h] [ebp-5Fh] BYREF
  _BYTE v22[16]; // [esp+130h] [ebp-50h] BYREF
  int v23; // [esp+140h] [ebp-40h]
  _BYTE v24[12]; // [esp+144h] [ebp-3Ch] BYREF
  _DWORD v25[2]; // [esp+150h] [ebp-30h] BYREF
  int v26; // [esp+158h] [ebp-28h] BYREF
  int v27; // [esp+15Ch] [ebp-24h] BYREF
  int v28; // [esp+160h] [ebp-20h] BYREF
  int v29; // [esp+164h] [ebp-1Ch]
  int v30; // [esp+168h] [ebp-18h] BYREF
  int v31; // [esp+16Ch] [ebp-14h] BYREF
  char v32; // [esp+173h] [ebp-Dh]
  int v33; // [esp+174h] [ebp-Ch] BYREF
  int v34; // [esp+178h] [ebp-8h]
  int v35; // [esp+17Ch] [ebp-4h]
  int savedregs; // [esp+180h] [ebp+0h] BYREF

  v16 = 0;
  v18 = nullptr;
  v17 = nullptr;
  v28 = 0;
  v27 = 0;
  v25[1] = 0;
  v26 = 0;
  v25[0] = 0;
  v33 = a3;
  v34 = a2;
  v35 = a1;
  unknown_libname_44(a1);
  unknown_libname_44(v34);
  unknown_libname_44(v33);
  v14 = &savedregs;
  v13[1] = (unsigned int)&loc_4B42B5;
  v13[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v13);
  v29 = sub_4E3E04();
  sub_4B3004(0, 0, 0);
  System::__linkproc__ LStrToString(v19, v35, 255);
  LOBYTE(v6) = 12;
  System::__linkproc__ PStrNCpy(v20, v19, v6);
  System::__linkproc__ LStrToString(v19, v34, 255);
  LOBYTE(v7) = 14;
  System::__linkproc__ PStrNCpy(v21, v19, v7);
  System::__linkproc__ LStrToString(v19, v33, 255);
  LOBYTE(v8) = 15;
  System::__linkproc__ PStrNCpy(v22, v19, v8);
  v23 = a6;
  sub_4565B0(v20, 48, &v18, v13[0]);
  v12 = v18;
  sub_456478(v24, &v17);
  System::__linkproc__ LStrCat3((int)&v27, v17, v12);
  dword_4EBB68 = 100;
  sub_4B3BEC(v29, v27);
  if ( sub_4B3D44(v29, &v31, &v30, a4, 0x1388u, (int)&v28) != 0 )
  {
    v32 = 0;
    if ( v31 == 1100 )
    {
      if ( v30 == 1 )
      {
        v15 = 47;
        sub_453DBC(&v16, 0);
        unknown_libname_37(&v28, v16);
        sub_45633C(v27, &v26);
        System::__linkproc__ LStrCmp(v26, v34);
        if ( v9 && (v10 = sub_454744(4218.666666666667)) == unknown_libname_43(v28) )
        {
          sub_4563D8(v28, a5, 3164);
          v32 = 1;
        }
        else
        {
          v32 = 0;
        }
      }
      else
      {
        v32 = 0;
      }
    }
    else
    {
      v32 = 0;
    }
  }
  else
  {
    v32 = 0;
  }
  __writefsdword(0, v13[0]);
  v14 = (int *)&loc_4B42BC;
  unknown_libname_35(&v16, 3);
  unknown_libname_35(v25, 5);
  unknown_libname_35(&v33, 3);
  return v32;
}


// ==================== [0x4B42C8] sub_4B42C8 (size: 414) ====================
char __userpurge sub_4B42C8@<al>(int a1@<eax>, int a2@<edx>, int a3@<ecx>, unsigned __int64 a4@<st0>, const void *a5)
{
  int v5; // ecx
  int v7; // [esp-20h] [ebp-CCCh]
  unsigned int v8[2]; // [esp-Ch] [ebp-CB8h] BYREF
  int *v9; // [esp-4h] [ebp-CB0h]
  int v10; // [esp+Ch] [ebp-CA0h] BYREF
  int v11; // [esp+10h] [ebp-C9Ch] BYREF
  int v12; // [esp+14h] [ebp-C98h] BYREF
  int v13; // [esp+18h] [ebp-C94h] BYREF
  int v14; // [esp+1Ch] [ebp-C90h] BYREF
  _BYTE v15[12]; // [esp+20h] [ebp-C8Ch] BYREF
  int v16; // [esp+2Ch] [ebp-C80h] BYREF
  _BYTE v17[3164]; // [esp+30h] [ebp-C7Ch] BYREF
  int v18; // [esp+C8Ch] [ebp-20h] BYREF
  int v19; // [esp+C90h] [ebp-1Ch]
  int v20; // [esp+C94h] [ebp-18h] BYREF
  int v21; // [esp+C98h] [ebp-14h] BYREF
  char v22; // [esp+C9Fh] [ebp-Dh]
  int v23; // [esp+CA0h] [ebp-Ch]
  int v24; // [esp+CA4h] [ebp-8h] BYREF
  int v25; // [esp+CA8h] [ebp-4h]
  int savedregs; // [esp+CACh] [ebp+0h] BYREF

  v14 = 0;
  v13 = 0;
  v12 = 0;
  v11 = 0;
  v10 = 0;
  v18 = 0;
  v16 = 0;
  qmemcpy(v17, a5, sizeof(v17));
  v23 = a3;
  v24 = a2;
  v25 = a1;
  unknown_libname_44(a1);
  unknown_libname_44(v24);
  v9 = &savedregs;
  v8[1] = (unsigned int)&loc_4B4453;
  v8[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v8);
  v19 = sub_4E3E04();
  v22 = 0;
  dword_4EBB68 = 101;
  sub_4B3004(0, 0, 0);
  sub_456478(v15, &v13);
  sub_456504(v25, &v12);
  sub_456504(v24, &v11);
  sub_4565B0(v17, 3164, &v10, &str___69[1]);
  System::__linkproc__ LStrCatN(&v14, 6, v5, v7, v10);
  sub_4B3BEC(v19, v14);
  if ( sub_4B3D44(v19, &v21, &v20, a4, 0x1388u, (int)&v18) != 0 && v21 == 1101 && v20 == 1 )
    v22 = 1;
  __writefsdword(0, v8[0]);
  v9 = (int *)&loc_4B445A;
  unknown_libname_35(&v10, 5);
  unknown_libname_34(&v16);
  unknown_libname_34(&v18);
  unknown_libname_35(&v24, 2);
  return v22;
}


// ==================== [0x4B46A0] sub_4B46A0 (size: 391) ====================
// bad sp value at call has been detected, the output may be wrong!
int __fastcall sub_4B46A0(
        int a1,
        unsigned int a2,
        int a3,
        int a4,
        char a5,
        int a6,
        int a7,
        int a8,
        int a9,
        int a10,
        int a11,
        int a12)
{
  int v12; // ecx
  int v13; // ecx
  int v14; // ecx
  unsigned int v16[2]; // [esp-18h] [ebp-128h] BYREF
  void **v17; // [esp-10h] [ebp-120h]
  _DWORD v18[3]; // [esp-Ch] [ebp-11Ch] BYREF
  _BYTE v19[256]; // [esp+0h] [ebp-110h] BYREF
  void *Mem; // [esp+100h] [ebp-10h]
  int v21; // [esp+104h] [ebp-Ch] BYREF
  unsigned int v22; // [esp+108h] [ebp-8h]
  int v23; // [esp+10Ch] [ebp-4h]
  void *savedregs; // [esp+110h] [ebp+0h] BYREF

  v21 = a3;
  v22 = a2;
  v23 = a1;
  unknown_libname_44(a2);
  unknown_libname_44(v21);
  unknown_libname_44(a4);
  v18[2] = &savedregs;
  v18[1] = &loc_4B481A;
  v18[0] = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v18);
  Mem = (void *)System::__linkproc__ GetMem(80);
  System::__linkproc__ LStrToString(v19, v22, 255);
  LOBYTE(v12) = 10;
  System::__linkproc__ PStrNCpy(Mem, v19, v12);
  System::__linkproc__ LStrToString(v19, v21, 255);
  LOBYTE(v13) = 14;
  System::__linkproc__ PStrNCpy((char *)Mem + 11, v19, v13);
  System::__linkproc__ LStrToString(v19, a4, 255);
  LOBYTE(v14) = 16;
  System::__linkproc__ PStrNCpy((char *)Mem + 26, v19, v14);
  *((_BYTE *)Mem + 43) = a5;
  *((_DWORD *)Mem + 11) = a6;
  *((_DWORD *)Mem + 14) = a9;
  *((_DWORD *)Mem + 12) = a7;
  *((_DWORD *)Mem + 13) = a8;
  *((_DWORD *)Mem + 15) = a10;
  *((_DWORD *)Mem + 16) = a11;
  *((_DWORD *)Mem + 17) = a12;
  *((_DWORD *)Mem + 18) = GetTickCount();
  *((_BYTE *)Mem + 76) = 0;
  v17 = &savedregs;
  v16[1] = (unsigned int)&loc_4B47F0;
  v16[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v16);
  sub_450080(*off_4EA010[0]);
  Classes::TList::Add(*(Classes::TList **)(v23 + 48), Mem);
  __writefsdword(0, v16[0]);
  v17 = (void **)&loc_4B47F7;
  Webadapt::TBaseAdapterErrorsList::GetErrorCount(*(Webadapt::TBaseAdapterErrorsList **)off_4EA010[0]);
  __writefsdword(0, v22);
  savedregs = &loc_4B4821;
  unknown_libname_35(&v21, 2);
  return unknown_libname_34(&a4);
}


// ==================== [0x4B4B10] sub_4B4B10 (size: 312) ====================
char __fastcall sub_4B4B10(int a1, _DWORD *a2)
{
  int v3; // [esp-14h] [ebp-C94h]
  int v4; // [esp-10h] [ebp-C90h]
  unsigned int v5[2]; // [esp-Ch] [ebp-C8Ch] BYREF
  int *v6; // [esp-4h] [ebp-C84h]
  int v7; // [esp+8h] [ebp-C78h] BYREF
  int v8; // [esp+Ch] [ebp-C74h]
  int v9; // [esp+10h] [ebp-C70h]
  _BYTE v10[3164]; // [esp+14h] [ebp-C6Ch] BYREF
  int v11; // [esp+C70h] [ebp-10h]
  char v12; // [esp+C77h] [ebp-9h]
  const void *v13; // [esp+C78h] [ebp-8h]
  int v14; // [esp+C7Ch] [ebp-4h]
  int savedregs; // [esp+C80h] [ebp+0h] BYREF

  v9 = 0;
  v8 = 0;
  v7 = 0;
  v13 = a2;
  v14 = a1;
  v6 = &savedregs;
  v5[1] = (unsigned int)&loc_4B4C38;
  v5[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v5);
  v12 = 0;
  v4 = a2[11];
  unknown_libname_41(v10);
  unknown_libname_41(v9);
  unknown_libname_41(v8);
  if ( (unsigned __int8)sub_4B3A68(v3, v4) != 0 )
  {
    v11 = System::__linkproc__ New(3248, off_4B26E4);
    unknown_libname_41(v5[0]);
    qmemcpy((void *)(v11 + 3168), v13, 0x50u);
    qmemcpy((void *)(v11 + 4), v10, 3164);
    sub_4AE3FC(*off_4EA490, v11);
    v12 = 1;
  }
  else
  {
    sub_4E08E4(*off_4EA628[0], *((_DWORD *)v13 + 17), *((_DWORD *)v13 + 15));
  }
  __writefsdword(0, v5[0]);
  v6 = (int *)&loc_4B4C3F;
  unknown_libname_35(&v7, 3);
  return v12;
}


// ==================== [0x4B4D58] sub_4B4D58 (size: 851) ====================
int __fastcall sub_4B4D58(Classes::TList **a1)
{
  int v1; // edx
  void *v2; // eax
  int result; // eax
  int v4; // eax
  int v5; // eax
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-Ch] [ebp-40h] BYREF
  void *v7; // [esp-8h] [ebp-3Ch]
  int *v8; // [esp-4h] [ebp-38h]
  int v9; // [esp+8h] [ebp-2Ch]
  int v10; // [esp+Ch] [ebp-28h]
  void *v11; // [esp+10h] [ebp-24h]
  int v12; // [esp+14h] [ebp-20h]
  System::TObject *v13; // [esp+18h] [ebp-1Ch]
  System::TObject *v14; // [esp+1Ch] [ebp-18h]
  System::TObject *v15; // [esp+20h] [ebp-14h]
  void *v16; // [esp+24h] [ebp-10h]
  int v17; // [esp+28h] [ebp-Ch]
  int v18; // [esp+2Ch] [ebp-8h]
  Classes::TList **v19; // [esp+30h] [ebp-4h]
  int savedregs; // [esp+34h] [ebp+0h] BYREF

  v19 = a1;
  v15 = nullptr;
  v14 = nullptr;
  v13 = nullptr;
  v8 = &savedregs;
  v7 = &loc_4B4EBE;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  sub_450080(*off_4EA010[0]);
  if ( *((int *)v19[13] + 2) > 0 )
  {
    LOBYTE(v1) = 1;
    v14 = (System::TObject *)unknown_libname_22(off_40FAFC, v1);
    if ( *((_DWORD *)v19[13] + 2) - 1 >= 0 )
    {
      v10 = *((_DWORD *)v19[13] + 2);
      v18 = 0;
      do
      {
        v2 = (void *)Classes::TList::Get(v19[13], v18);
        Classes::TList::Add(v14, v2);
        ++v18;
        --v10;
      }
      while ( v10 != 0 );
    }
  }
  if ( *((int *)v19[12] + 2) > 0 )
  {
    LOBYTE(v1) = 1;
    v15 = (System::TObject *)unknown_libname_22(off_40FAFC, v1);
    if ( *((_DWORD *)v19[12] + 2) - 1 >= 0 )
    {
      v10 = *((_DWORD *)v19[12] + 2);
      v18 = 0;
      do
      {
        v16 = (void *)Classes::TList::Get(v19[12], v18);
        Classes::TList::Add(v15, v16);
        ++v18;
        --v10;
      }
      while ( v10 != 0 );
    }
    (*(void (__fastcall **)(Classes::TList *))(*(_DWORD *)v19[12] + 8))(v19[12]);
  }
  if ( *((int *)v19[14] + 2) > 0 )
  {
    LOBYTE(v1) = 1;
    v13 = (System::TObject *)unknown_libname_22(off_40FAFC, v1);
    if ( *((_DWORD *)v19[14] + 2) - 1 >= 0 )
    {
      v10 = *((_DWORD *)v19[14] + 2);
      v18 = 0;
      do
      {
        v11 = (void *)Classes::TList::Get(v19[14], v18);
        Classes::TList::Add(v13, v11);
        ++v18;
        --v10;
      }
      while ( v10 != 0 );
    }
    (*(void (__fastcall **)(Classes::TList *))(*(_DWORD *)v19[14] + 8))(v19[14]);
  }
  __writefsdword(0, (unsigned int)ExceptionList);
  v8 = (int *)&loc_4B4EC5;
  result = Webadapt::TBaseAdapterErrorsList::GetErrorCount(*(Webadapt::TBaseAdapterErrorsList **)off_4EA010[0]);
  if ( v14 != nullptr )
  {
    if ( *((_DWORD *)v14 + 2) - 1 >= 0 )
    {
      v10 = *((_DWORD *)v14 + 2);
      v18 = 0;
      do
      {
        v12 = Classes::TList::Get(v14, v18);
        if ( (unsigned __int8)sub_4B3B5C(v12 + 20) != 0 || *(int *)(v12 + 12) > 50 )
        {
          if ( *(_DWORD *)(v12 + 16) != 0 )
            *(_BYTE *)(*(_DWORD *)(v12 + 16) + 1496) = 1;
          v8 = &savedregs;
          v7 = &loc_4B4FC1;
          ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
          __writefsdword(0, (unsigned int)&ExceptionList);
          sub_450080(*off_4EA010[0]);
          if ( *((_DWORD *)v19[13] + 2) - 1 >= 0 )
          {
            v9 = *((_DWORD *)v19[13] + 2);
            v17 = 0;
            while ( 1 )
            {
              v4 = Classes::TList::Get(v19[13], v17);
              if ( v4 == v12 )
                break;
              ++v17;
              if ( --v9 == 0 )
                goto LABEL_27;
            }
            Classes::TList::Delete(v19[13], v17);
            System::__linkproc__ Dispose(v12, off_4B26BC);
          }
LABEL_27:
          __writefsdword(0, (unsigned int)ExceptionList);
          v8 = (int *)&loc_4B4FCE;
          Webadapt::TBaseAdapterErrorsList::GetErrorCount(*(Webadapt::TBaseAdapterErrorsList **)off_4EA010[0]);
        }
        else
        {
          ++*(_DWORD *)(v12 + 12);
        }
        ++v18;
        --v10;
      }
      while ( v10 != 0 );
    }
    result = System::TObject::Free(v14);
  }
  if ( v15 != nullptr )
  {
    if ( *((_DWORD *)v15 + 2) - 1 >= 0 )
    {
      v10 = *((_DWORD *)v15 + 2);
      v18 = 0;
      do
      {
        v16 = (void *)Classes::TList::Get(v15, v18);
        if ( sub_4B4B10((int)v19, v16) == 0 )
          sub_4E0490(*off_4EA628[0], *((_DWORD *)v16 + 17));
        v5 = Classes::TList::Get(v15, v18);
        System::__linkproc__ FreeMem(v5, 0);
        ++v18;
        --v10;
      }
      while ( v10 != 0 );
    }
    result = System::TObject::Free(v15);
  }
  if ( v13 != nullptr )
  {
    if ( *((_DWORD *)v13 + 2) - 1 >= 0 )
    {
      v10 = *((_DWORD *)v13 + 2);
      v18 = 0;
      do
      {
        v11 = (void *)Classes::TList::Get(v13, v18);
        sub_4B4C48(v19, v11);
        System::__linkproc__ FreeMem(v11, 36);
        ++v18;
        --v10;
      }
      while ( v10 != 0 );
    }
    return System::TObject::Free(v13);
  }
  return result;
}


// ==================== [0x4B54E0] sub_4B54E0 (size: 757) ====================
char __fastcall sub_4B54E0(int a1, int a2)
{
  _DWORD *v2; // eax
  int v3; // eax
  __int64 v4; // rax
  int v5; // eax
  int v6; // eax
  unsigned int v8[2]; // [esp-Ch] [ebp-84h] BYREF
  int *v9; // [esp-4h] [ebp-7Ch]
  _WORD Buffer[26]; // [esp+4h] [ebp-74h] BYREF
  int v11; // [esp+38h] [ebp-40h]
  int v12; // [esp+3Ch] [ebp-3Ch]
  int v13; // [esp+40h] [ebp-38h]
  void *Mem; // [esp+44h] [ebp-34h]
  _BYTE *v15; // [esp+48h] [ebp-30h]
  LPVOID lpBuffer; // [esp+4Ch] [ebp-2Ch]
  int v17; // [esp+50h] [ebp-28h]
  int v18; // [esp+54h] [ebp-24h]
  DWORD nNumberOfBytesToRead; // [esp+58h] [ebp-20h]
  HANDLE hFile; // [esp+5Ch] [ebp-1Ch]
  int v21; // [esp+60h] [ebp-18h]
  int v22; // [esp+64h] [ebp-14h]
  int v23; // [esp+68h] [ebp-10h]
  char v24; // [esp+6Fh] [ebp-9h]
  int v25; // [esp+70h] [ebp-8h] BYREF
  int v26; // [esp+74h] [ebp-4h]
  int savedregs; // [esp+78h] [ebp+0h] BYREF

  v25 = a2;
  v26 = a1;
  unknown_libname_44(a2);
  v9 = &savedregs;
  v8[1] = (unsigned int)&loc_4B57C7;
  v8[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v8);
  v24 = 0;
  if ( (unsigned __int8)Sysutils::FileExists(v25) != 0 )
  {
    hFile = (HANDLE)sub_409278(v25, 16);
    if ( (int)hFile > 0 )
    {
      sub_4092DC(hFile, Buffer, 0x34u);
      *(_DWORD *)(v26 + 28) = Buffer[0];
      *(_DWORD *)(v26 + 32) = Buffer[1];
      sub_4B53FC(v26, *(_DWORD *)(v26 + 28), *(_DWORD *)(v26 + 32));
      nNumberOfBytesToRead = *(_DWORD *)(v26 + 32) * 12 * *(_DWORD *)(v26 + 28);
      lpBuffer = (LPVOID)Sysutils::AllocMem(nNumberOfBytesToRead);
      sub_4092DC(hFile, lpBuffer, nNumberOfBytesToRead);
      if ( *(_DWORD *)(v26 + 28) - 1 >= 0 )
      {
        v13 = *(_DWORD *)(v26 + 28);
        v23 = 0;
        do
        {
          v18 = *(_DWORD *)(v26 + 32) * v23;
          if ( *(_DWORD *)(v26 + 32) - 1 >= 0 )
          {
            v12 = *(_DWORD *)(v26 + 32);
            v22 = 0;
            do
            {
              if ( *((char *)lpBuffer + 12 * v22 + 12 * v18 + 1) < 0 )
              {
                v15 = (_BYTE *)(*(_DWORD *)(v26 + 12) + 8 * (v22 + v18));
                *v15 = 1;
              }
              if ( *((char *)lpBuffer + 12 * v22 + 12 * v18 + 5) < 0 )
              {
                v15 = (_BYTE *)(*(_DWORD *)(v26 + 12) + 8 * (v22 + v18));
                *v15 = 2;
              }
              if ( *((char *)lpBuffer + 12 * v22 + 12 * v18 + 6) < 0 )
              {
                v17 = *((_BYTE *)lpBuffer + 12 * v22 + 12 * v18 + 6) & 0x7F;
                if ( v17 != 0 )
                {
                  Mem = (void *)System::__linkproc__ GetMem(16);
                  *(_DWORD *)Mem = v23;
                  *((_DWORD *)Mem + 1) = v22;
                  *((_DWORD *)Mem + 2) = v17;
                  *((_DWORD *)Mem + 3) = 0;
                  if ( *(_DWORD *)(*(_DWORD *)(v26 + 40) + 8) - 1 >= 0 )
                  {
                    v11 = *(_DWORD *)(*(_DWORD *)(v26 + 40) + 8);
                    v21 = 0;
                    while ( 1 )
                    {
                      v2 = (_DWORD *)Classes::TList::Get(*(Classes::TList **)(v26 + 40), v21);
                      if ( (int)abs32(*(_DWORD *)Mem - *v2) <= 10 )
                      {
                        v3 = Classes::TList::Get(*(Classes::TList **)(v26 + 40), v21);
                        v4 = *((_DWORD *)Mem + 1) - *(_DWORD *)(v3 + 4);
                        if ( (int)((HIDWORD(v4) ^ v4) - HIDWORD(v4)) <= 10 )
                        {
                          v5 = Classes::TList::Get(*(Classes::TList **)(v26 + 40), v21);
                          if ( *(_DWORD *)(v5 + 8) == v17 )
                            break;
                        }
                      }
                      ++v21;
                      if ( --v11 == 0 )
                        goto LABEL_20;
                    }
                    v6 = Classes::TList::Get(*(Classes::TList **)(v26 + 40), v21);
                    *((_DWORD *)Mem + 3) = *(_DWORD *)(v6 + 12);
                  }
LABEL_20:
                  if ( *((_DWORD *)Mem + 3) == 0 )
                  {
                    *((_DWORD *)Mem + 3) = System::__linkproc__ GetMem(12);
                    **((_BYTE **)Mem + 3) = 0;
                    *(_BYTE *)(*((_DWORD *)Mem + 3) + 1) = 0;
                    *(_DWORD *)(*((_DWORD *)Mem + 3) + 4) = 0;
                    *(_DWORD *)(*((_DWORD *)Mem + 3) + 8) = 0;
                  }
                  Classes::TList::Add(*(Classes::TList **)(v26 + 40), Mem);
                }
              }
              ++v22;
              --v12;
            }
            while ( v12 != 0 );
          }
          ++v23;
          --v13;
        }
        while ( v13 != 0 );
      }
      System::__linkproc__ FreeMem(lpBuffer, 1610612736);
      sub_409340(hFile);
      v24 = 1;
    }
  }
  __writefsdword(0, v8[0]);
  v9 = (int *)&loc_4B57CE;
  unknown_libname_34(&v25);
  return v24;
}


// ==================== [0x4B612C] sub_4B612C (size: 641) ====================
int __fastcall sub_4B612C(int a1, int a2, int a3, int a4, int a5, int a6, char a7)
{
  int v7; // edx
  int v8; // eax
  unsigned int v10[6]; // [esp-Ch] [ebp-40h] BYREF
  int v11; // [esp+Ch] [ebp-28h]
  _BYTE *v12; // [esp+10h] [ebp-24h]
  int v13; // [esp+14h] [ebp-20h]
  char v14; // [esp+1Ah] [ebp-1Ah]
  char v15; // [esp+1Bh] [ebp-19h]
  void *Mem; // [esp+1Ch] [ebp-18h]
  int v17; // [esp+20h] [ebp-14h] BYREF
  int v18; // [esp+24h] [ebp-10h]
  int v19; // [esp+28h] [ebp-Ch]
  int v20; // [esp+2Ch] [ebp-8h]
  int v21; // [esp+30h] [ebp-4h]
  int savedregs; // [esp+34h] [ebp+0h] BYREF

  v19 = a3;
  v20 = a2;
  v21 = a1;
  v18 = 0;
  v10[2] = (unsigned int)&savedregs;
  v10[1] = (unsigned int)&loc_4B638D;
  v10[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v10);
  v14 = 1;
  if ( a7 == 0 )
  {
    v15 = sub_4B57D8(&v17);
    if ( v15 != 0 )
    {
      if ( *(_BYTE *)v17 != 0 )
      {
        v18 = -1;
        v14 = 0;
      }
      else if ( *(_DWORD *)(v17 + 4) != 0 && *(_DWORD *)(*(_DWORD *)(v17 + 4) + 8) - 1 >= 0 )
      {
        v11 = *(_DWORD *)(*(_DWORD *)(v17 + 4) + 8);
        v13 = 0;
        while ( 1 )
        {
          if ( *(_BYTE *)Classes::TList::Get(*(Classes::TList **)(v17 + 4), v13) == 1 )
          {
            v12 = *(_BYTE **)(Classes::TList::Get(*(Classes::TList **)(v17 + 4), v13) + 4);
            if ( v12 != nullptr && v12[764] == 0 && v12[697] != 0 && v12[772] == 0 && v12[701] == 0 && v12[842] == 0 )
              break;
          }
          ++v13;
          if ( --v11 == 0 )
            goto LABEL_18;
        }
        v14 = 0;
      }
    }
  }
LABEL_18:
  if ( v14 != 0 )
  {
    v15 = sub_4B57D8(&v17);
    if ( v15 != 0 && *(_BYTE *)v17 == 0 )
    {
      v15 = sub_4B57D8(&v17);
      if ( v15 != 0 && *(_DWORD *)(v17 + 4) != 0 )
      {
        v13 = 0;
        while ( *(_DWORD *)(*(_DWORD *)(v17 + 4) + 8) > v13 )
        {
          Mem = (void *)Classes::TList::Get(*(Classes::TList **)(v17 + 4), v13);
          if ( *(_BYTE *)Mem == 1 && *((_DWORD *)Mem + 1) == a4 )
          {
            Classes::TList::Delete(*(Classes::TList **)(v17 + 4), v13);
            System::__linkproc__ FreeMem(Mem, 12);
            if ( *(int *)(*(_DWORD *)(v17 + 4) + 8) <= 0 )
            {
              System::TObject::Free(*(System::TObject **)(v17 + 4));
              *(_DWORD *)(v17 + 4) = 0;
              break;
            }
          }
          else
          {
            ++v13;
          }
        }
      }
      v15 = sub_4B57D8(&v17);
      if ( v15 != 0 )
      {
        if ( *(_DWORD *)(v17 + 4) == 0 )
        {
          LOBYTE(v7) = 1;
          v8 = unknown_libname_22(off_40FAFC, v7);
          *(_DWORD *)(v17 + 4) = v8;
        }
        Mem = (void *)System::__linkproc__ GetMem(12);
        *(_BYTE *)Mem = 1;
        *((_DWORD *)Mem + 1) = a4;
        *((_DWORD *)Mem + 2) = GetTickCount();
        Classes::TList::Add(*(Classes::TList **)(v17 + 4), Mem);
        v18 = 1;
      }
    }
    else
    {
      v18 = -1;
    }
  }
  __writefsdword(0, v10[0]);
  return v18;
}


// ==================== [0x4B63E4] sub_4B63E4 (size: 485) ====================
int __fastcall sub_4B63E4(int a1, int a2, int a3, char a4, int a5)
{
  int v5; // edx
  int v6; // eax
  char v7; // zf
  __int16 v8; // ax
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-Ch] [ebp-44h] BYREF
  void *v11; // [esp-8h] [ebp-40h]
  int *v12; // [esp-4h] [ebp-3Ch]
  int v13; // [esp+Ch] [ebp-2Ch]
  int v14; // [esp+10h] [ebp-28h]
  int v15; // [esp+14h] [ebp-24h]
  char v16; // [esp+1Ah] [ebp-1Eh]
  char v17; // [esp+1Bh] [ebp-1Dh]
  int v18; // [esp+1Ch] [ebp-1Ch]
  void *Mem; // [esp+20h] [ebp-18h]
  int v20; // [esp+24h] [ebp-14h] BYREF
  int v21; // [esp+28h] [ebp-10h]
  int v22; // [esp+2Ch] [ebp-Ch]
  int v23; // [esp+30h] [ebp-8h]
  int v24; // [esp+34h] [ebp-4h]
  int savedregs; // [esp+38h] [ebp+0h] BYREF

  v22 = a3;
  v23 = a2;
  v24 = a1;
  v21 = 0;
  v12 = &savedregs;
  v11 = &loc_4B65A9;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  v17 = sub_4B57D8(&v20);
  v16 = 0;
  if ( v17 != 0 && *(_BYTE *)v20 == 0 )
  {
    if ( *(_DWORD *)(v20 + 4) != 0 )
    {
      if ( a4 == 2 )
      {
        System::__linkproc__ AStrCmp(ExceptionList, v11, v12);
        if ( v7 && *(_DWORD *)(*(_DWORD *)(v20 + 4) + 8) - 1 >= 0 )
        {
          v13 = *(_DWORD *)(*(_DWORD *)(v20 + 4) + 8);
          v15 = 0;
          do
          {
            Mem = (void *)Classes::TList::Get(*(Classes::TList **)(v20 + 4), v15);
            if ( *(_BYTE *)Mem == 2 )
            {
              v18 = *(_DWORD *)(Classes::TList::Get(*(Classes::TList **)(v20 + 4), v15) + 4);
              System::__linkproc__ AStrCmp(ExceptionList, v11, v12);
              if ( v7 )
              {
                v14 = *(_DWORD *)(a5 + 44) + *(_DWORD *)(v18 + 44);
                if ( v14 <= (int)&loc_4C4B3F + 1 )
                {
                  *(_DWORD *)(v18 + 44) = v14;
                  v8 = sub_455E98(v14);
                  *(_WORD *)(v18 + 40) = v8;
                  *(_BYTE *)(v18 + 42) = 0;
                  *(_BYTE *)(v18 + 43) = 0;
                  *((_DWORD *)Mem + 2) = GetTickCount();
                  v21 = v18;
                  v16 = 1;
                }
              }
            }
            ++v15;
            --v13;
          }
          while ( v13 != 0 );
        }
        if ( v16 == 0 && *(int *)(*(_DWORD *)(v20 + 4) + 8) >= 5 )
        {
          v21 = 0;
          v16 = 1;
        }
      }
    }
    else
    {
      LOBYTE(v5) = 1;
      v6 = unknown_libname_22(off_40FAFC, v5);
      *(_DWORD *)(v20 + 4) = v6;
    }
    if ( v16 == 0 )
    {
      Mem = (void *)System::__linkproc__ GetMem(12);
      *(_BYTE *)Mem = a4;
      *((_DWORD *)Mem + 1) = a5;
      *((_DWORD *)Mem + 2) = GetTickCount();
      Classes::TList::Add(*(Classes::TList **)(v20 + 4), Mem);
      v21 = a5;
    }
  }
  __writefsdword(0, (unsigned int)ExceptionList);
  return v21;
}


// ==================== [0x4B6710] sub_4B6710 (size: 520) ====================
int __fastcall sub_4B6710(int a1, int a2, int a3, char a4, int a5)
{
  unsigned int v6[3]; // [esp-24h] [ebp-60h] BYREF
  unsigned int v7[3]; // [esp-18h] [ebp-54h] BYREF
  unsigned int v8[2]; // [esp-Ch] [ebp-48h] BYREF
  int *v9; // [esp-4h] [ebp-40h]
  void *v10[4]; // [esp+Ch] [ebp-30h] BYREF
  char v11; // [esp+1Fh] [ebp-1Dh]
  int v12; // [esp+20h] [ebp-1Ch]
  int v13; // [esp+24h] [ebp-18h]
  int v14; // [esp+28h] [ebp-14h] BYREF
  int v15; // [esp+2Ch] [ebp-10h]
  int v16; // [esp+30h] [ebp-Ch]
  int v17; // [esp+34h] [ebp-8h]
  int v18; // [esp+38h] [ebp-4h]
  int savedregs; // [esp+3Ch] [ebp+0h] BYREF

  memset(v10, 0, sizeof(v10));
  v16 = a3;
  v17 = a2;
  v18 = a1;
  v9 = &savedregs;
  v8[1] = (unsigned int)&loc_4B6905;
  v8[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v8);
  v15 = -1;
  v7[2] = (unsigned int)&savedregs;
  v7[1] = (unsigned int)&loc_4B68BB;
  v7[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v7);
  v11 = sub_4B57D8(&v14);
  if ( v11 != 0 )
  {
    if ( v14 != 0 )
    {
      v6[2] = (unsigned int)&savedregs;
      v6[1] = (unsigned int)&loc_4B686D;
      v6[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
      __writefsdword(0, (unsigned int)v6);
      if ( *(_DWORD *)(v14 + 4) != 0 )
      {
        v13 = 0;
        while ( *(_DWORD *)(*(_DWORD *)(v14 + 4) + 8) > v13 )
        {
          v12 = Classes::TList::Get(*(Classes::TList **)(v14 + 4), v13);
          if ( v12 != 0 )
          {
            if ( *(_BYTE *)v12 == a4 && *(_DWORD *)(v12 + 4) == a5 )
            {
              Classes::TList::Delete(*(Classes::TList **)(v14 + 4), v13);
              System::__linkproc__ FreeMem(v12, 12);
              v15 = 1;
              if ( *(int *)(*(_DWORD *)(v14 + 4) + 8) <= 0 )
                goto LABEL_10;
            }
            else
            {
              ++v13;
            }
          }
          else
          {
            Classes::TList::Delete(*(Classes::TList **)(v14 + 4), v13);
            if ( *(int *)(*(_DWORD *)(v14 + 4) + 8) <= 0 )
            {
LABEL_10:
              System::TObject::Free(*(System::TObject **)(v14 + 4));
              *(_DWORD *)(v14 + 4) = 0;
              break;
            }
          }
        }
      }
      else
      {
        v15 = -2;
      }
      __writefsdword(0, v6[0]);
    }
    else
    {
      v15 = -3;
    }
  }
  else
  {
    v15 = 0;
  }
  __writefsdword(0, v7[0]);
  __writefsdword(0, v8[0]);
  v9 = (int *)&loc_4B690C;
  unknown_libname_35(v10, 4);
  return v15;
}


// ==================== [0x4B6C3C] sub_4B6C3C (size: 451) ====================
char __fastcall sub_4B6C3C(int a1, int a2, int a3, int a4, int a5, int a6, char a7)
{
  char v7; // zf
  void *v8; // edx
  int v9; // ecx
  unsigned int v11[2]; // [esp-Ch] [ebp-124h] BYREF
  int *v12; // [esp-4h] [ebp-11Ch]
  _BYTE v13[256]; // [esp+0h] [ebp-118h] BYREF
  void *v14; // [esp+100h] [ebp-18h]
  void *v15; // [esp+104h] [ebp-14h]
  char v16; // [esp+10Bh] [ebp-Dh]
  int v17; // [esp+10Ch] [ebp-Ch]
  int v18; // [esp+110h] [ebp-8h]
  int v19; // [esp+114h] [ebp-4h]
  int savedregs; // [esp+118h] [ebp+0h] BYREF

  v17 = a3;
  v18 = a2;
  v19 = a1;
  unknown_libname_44(a4);
  unknown_libname_44(a5);
  unknown_libname_44(a6);
  v12 = &savedregs;
  v11[1] = (unsigned int)&loc_4B6DEF;
  v11[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v11);
  v16 = 0;
  if ( v18 >= 0 )
  {
    v15 = (void *)System::__linkproc__ New(24, off_4B5208);
    *(_DWORD *)v15 = v18;
    if ( v17 > 1 )
      v17 = 1;
    *((_DWORD *)v15 + 1) = v17;
    System::__linkproc__ LStrCmp(a4, &str___70[1]);
    if ( v7 )
      unknown_libname_34(&a4);
    unknown_libname_36((char *)v15 + 8, a4);
    System::__linkproc__ LStrCmp(a5, &str___70[1]);
    if ( v7 )
      unknown_libname_34(&a5);
    unknown_libname_36((char *)v15 + 12, a5);
    System::__linkproc__ LStrCmp(a6, &str___70[1]);
    if ( v7 )
      unknown_libname_34(&a6);
    v8 = v15;
    *((_BYTE *)v15 + 16) = a7;
    LOBYTE(v8) = 1;
    v14 = (void *)sub_49EC70((int *)off_49A86C, v8);
    *((_WORD *)v14 + 2) = 12289;
    *((_DWORD *)v14 + 9) = 0;
    *((_DWORD *)v14 + 10) = 0;
    System::__linkproc__ LStrToString(v13, a6, 255);
    LOBYTE(v9) = 14;
    System::__linkproc__ PStrNCpy((char *)v14 + 21, v13, v9);
    *((_BYTE *)v14 + 1360) = 0;
    *((_WORD *)v14 + 284) = 0;
    unknown_libname_36((char *)v14 + 1368, &str_MapQuest_def__0[1]);
    *((_BYTE *)v14 + 1372) = 1;
    *((_BYTE *)v14 + 1373) = 0;
    Classes::TList::Add(*(Classes::TList **)(*off_4EA490 + 108), v14);
    *((_DWORD *)v15 + 5) = v14;
    Classes::TList::Add(*(Classes::TList **)(v19 + 72), v15);
    v16 = 1;
  }
  __writefsdword(0, v11[0]);
  v12 = (int *)&loc_4B6DF6;
  unknown_libname_35(&a6, 3);
  return v16;
}


// ==================== [0x4B6E4C] sub_4B6E4C (size: 363) ====================
int __fastcall sub_4B6E4C(int a1, int a2, int a3, int a4, char a5)
{
  char v5; // zf
  unsigned int v7[2]; // [esp-Ch] [ebp-30h] BYREF
  int *v8; // [esp-4h] [ebp-28h]
  int v9; // [esp+0h] [ebp-24h]
  char v10; // [esp+7h] [ebp-1Dh]
  int v11; // [esp+8h] [ebp-1Ch]
  int v12; // [esp+Ch] [ebp-18h]
  int v13; // [esp+10h] [ebp-14h]
  int v14; // [esp+14h] [ebp-10h]
  int v15; // [esp+18h] [ebp-Ch] BYREF
  int v16; // [esp+1Ch] [ebp-8h]
  int v17; // [esp+20h] [ebp-4h]
  int savedregs; // [esp+24h] [ebp+0h] BYREF

  v15 = a3;
  v16 = a2;
  v17 = a1;
  unknown_libname_44(a3);
  unknown_libname_44(a4);
  v8 = &savedregs;
  v7[1] = (unsigned int)&loc_4B6FA7;
  v7[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v7);
  v14 = 0;
  if ( *(_DWORD *)(*(_DWORD *)(v17 + 72) + 8) - 1 >= 0 )
  {
    v9 = *(_DWORD *)(*(_DWORD *)(v17 + 72) + 8);
    v13 = 0;
    while ( 1 )
    {
      v11 = Classes::TList::Get(*(Classes::TList **)(v17 + 72), v13);
      v12 = sub_4C1490(v16, *(_DWORD *)v11);
      if ( v12 == *(_DWORD *)(v11 + 4) && (a5 == *(_BYTE *)(v11 + 16) || a5 == 0) )
      {
        v10 = 0;
        if ( *(_DWORD *)(v11 + 8) != 0 && *(_DWORD *)(v11 + 12) != 0 )
        {
          System::__linkproc__ LStrCmp(*(_DWORD *)(v11 + 8), v15);
          if ( v5 )
          {
            System::__linkproc__ LStrCmp(*(_DWORD *)(v11 + 12), a4);
            if ( v5 )
              v10 = 1;
          }
        }
        if ( *(_DWORD *)(v11 + 8) != 0 && *(_DWORD *)(v11 + 12) == 0 )
        {
          System::__linkproc__ LStrCmp(*(_DWORD *)(v11 + 8), v15);
          if ( v5 )
          {
            if ( a4 == 0 )
              v10 = 1;
          }
        }
        if ( *(_DWORD *)(v11 + 8) == 0 && *(_DWORD *)(v11 + 12) != 0 )
        {
          System::__linkproc__ LStrCmp(*(_DWORD *)(v11 + 12), a4);
          if ( v5 )
            v10 = 1;
        }
        if ( v10 != 0 )
          break;
      }
      ++v13;
      if ( --v9 == 0 )
        goto LABEL_23;
    }
    v14 = *(_DWORD *)(v11 + 20);
  }
LABEL_23:
  __writefsdword(0, v7[0]);
  v8 = (int *)&loc_4B6FAE;
  unknown_libname_34(&v15);
  unknown_libname_34(&a4);
  return v14;
}


// ==================== [0x4B7038] sub_4B7038 (size: 540) ====================
void *__fastcall sub_4B7038(
        Classes::TList *a1,
        int a2,
        int a3,
        int a4,
        int a5,
        char a6,
        char a7,
        char a8,
        char a9,
        char a10,
        char a11,
        char a12,
        char a13,
        char a14,
        char a15,
        char a16,
        char a17,
        char a18,
        int a19,
        int a20,
        int a21,
        int a22)
{
  int v22; // edx
  int v23; // eax
  int v24; // ecx
  int v25; // eax
  int v26; // ecx
  int v27; // edx
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-Ch] [ebp-38h] BYREF
  void *v30; // [esp-8h] [ebp-34h]
  int *v31; // [esp-4h] [ebp-30h]
  void *v32; // [esp+0h] [ebp-2Ch]
  Dialogs *v33; // [esp+4h] [ebp-28h] BYREF
  int v34; // [esp+8h] [ebp-24h] BYREF
  int v35; // [esp+Ch] [ebp-20h] BYREF
  int v36; // [esp+10h] [ebp-1Ch]
  int v37; // [esp+14h] [ebp-18h]
  void *v38; // [esp+18h] [ebp-14h]
  void *v39; // [esp+1Ch] [ebp-10h]
  int v40; // [esp+20h] [ebp-Ch] BYREF
  int v41; // [esp+24h] [ebp-8h]
  Classes::TList *v42; // [esp+28h] [ebp-4h]
  int savedregs; // [esp+2Ch] [ebp+0h] BYREF

  v33 = nullptr;
  v34 = 0;
  v35 = 0;
  v40 = a3;
  v41 = a2;
  v42 = a1;
  unknown_libname_44(a2);
  unknown_libname_44(v40);
  unknown_libname_44(a19);
  v31 = &savedregs;
  v30 = &loc_4B7243;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  v39 = nullptr;
  LOBYTE(v22) = 1;
  v38 = (void *)sub_4B5318((int *)off_4B5234, v22);
  unknown_libname_36((char *)v38 + 4, v41);
  unknown_libname_36((char *)v38 + 8, v40);
  *((_DWORD *)v38 + 5) = a4;
  *((_BYTE *)v38 + 45) = a6;
  *((_BYTE *)v38 + 46) = a7;
  *((_BYTE *)v38 + 47) = a8;
  *((_BYTE *)v38 + 36) = a9;
  *((_BYTE *)v38 + 37) = a10;
  *((_BYTE *)v38 + 48) = a11;
  *((_BYTE *)v38 + 49) = a12;
  *((_BYTE *)v38 + 50) = a13;
  *((_BYTE *)v38 + 51) = a14;
  *((_BYTE *)v38 + 52) = a15;
  *((_BYTE *)v38 + 53) = a16;
  *((_BYTE *)v38 + 54) = a17;
  *((_BYTE *)v38 + 55) = a18;
  unknown_libname_36((char *)v38 + 56, a19);
  *((_DWORD *)v38 + 15) = a20;
  *((_DWORD *)v38 + 16) = a21;
  *((_DWORD *)v38 + 17) = a22;
  v23 = (*(int (__fastcall **)(_DWORD, _DWORD))(**(_DWORD **)off_4EA140 + 20))(
          *(_DWORD *)off_4EA140,
          **(_DWORD **)off_4EA140)
      - 1;
  if ( v23 >= 0 )
  {
    v36 = v23 + 1;
    v37 = 0;
    while ( 1 )
    {
      (*(void (__fastcall **)(_DWORD, int, int *))(**(_DWORD **)off_4EA140 + 12))(*(_DWORD *)off_4EA140, v37, &v35);
      if ( unknown_libname_84(v35, v41) == 0 )
        break;
      ++v37;
      if ( --v36 == 0 )
        goto LABEL_6;
    }
    v25 = (*(int (__fastcall **)(_DWORD, int))(**(_DWORD **)off_4EA140 + 24))(*(_DWORD *)off_4EA140, v37);
    *((_DWORD *)v38 + 4) = v25;
  }
LABEL_6:
  ExceptionList = (struct _EXCEPTION_REGISTRATION_RECORD *)*off_4EA3F0[0];
  System::__linkproc__ LStrCatN(&v34, 3, v24, v41, &str__map[1]);
  if ( sub_4B54E0((int)v38, v34) != 0 )
  {
    v39 = v38;
    Classes::TList::Add(v42, v38);
  }
  else
  {
    ExceptionList = (struct _EXCEPTION_REGISTRATION_RECORD *)&str_file_not_found_[1];
    System::__linkproc__ LStrCatN(&v33, 4, v26, v41, &str__map[1]);
    Dialogs::ShowMessage(v33, v27);
  }
  __writefsdword(0, (unsigned int)v30);
  v32 = &loc_4B724A;
  unknown_libname_35(&v33, 3);
  unknown_libname_35(&v40, 2);
  unknown_libname_34(&a19);
  return v39;
}


// ==================== [0x4B780C] sub_4B780C (size: 2226) ====================
int __fastcall sub_4B780C(int a1, char a2)
{
  int v2; // edx
  int v3; // eax
  int v4; // edx
  int v5; // eax
  int v6; // edx
  int v7; // eax
  int v8; // edx
  int v9; // eax
  int v10; // edx
  int v11; // eax
  int v12; // edx
  int v13; // eax
  int v14; // edx
  int v15; // eax
  int v16; // edx
  int v17; // eax
  int v18; // edx
  int v19; // eax
  int v20; // edx
  int v21; // eax
  int v22; // edx
  int v23; // eax
  int v24; // edx
  int v25; // eax
  int v26; // edx
  int v27; // eax
  int v28; // edx
  int v29; // ebx
  char v31; // [esp+Bh] [ebp-5h]
  int v32; // [esp+Ch] [ebp-4h]

  if ( a2 != 0 )
    a1 = unknown_libname_26();
  v31 = a2;
  v32 = a1;
  *(_BYTE *)(a1 + 764) = 0;
  *(_DWORD *)(a1 + 768) = 0;
  *(_BYTE *)(a1 + 772) = 0;
  *(_DWORD *)(a1 + 776) = 0;
  *(_DWORD *)(a1 + 996) = GetTickCount();
  *(_BYTE *)(v32 + 44) = 4;
  *(_BYTE *)(v32 + 570) = 50;
  *(_BYTE *)(v32 + 571) = 0;
  *(_BYTE *)(v32 + 46) = 0;
  *(_BYTE *)(v32 + 47) = 0;
  *(_DWORD *)(v32 + 48) = 0;
  *(_WORD *)(v32 + 568) = 0;
  *(_BYTE *)(v32 + 697) = 1;
  *(_DWORD *)(v32 + 484) = 5;
  *(_WORD *)(v32 + 120) = 12289;
  *(_BYTE *)(v32 + 148) = 0;
  *(_BYTE *)(v32 + 621) = 0;
  *(_DWORD *)(v32 + 676) = 0;
  *(_BYTE *)(v32 + 674) = -1;
  *(_BYTE *)(v32 + 573) = 0;
  *(_BYTE *)(v32 + 574) = 0;
  *(_DWORD *)(v32 + 256) = 0;
  *(_DWORD *)(v32 + 260) = 0;
  *(_WORD *)(v32 + 268) = 0;
  *(_DWORD *)(v32 + 576) = GetTickCount();
  *(_BYTE *)(v32 + 580) = 0;
  *(_BYTE *)(v32 + 581) = 0;
  *(_BYTE *)(v32 + 582) = 0;
  *(_BYTE *)(v32 + 583) = 0;
  *(_BYTE *)(v32 + 698) = 0;
  *(_BYTE *)(v32 + 859) = 0;
  *(_BYTE *)(v32 + 1324) = 0;
  *(_BYTE *)(v32 + 1325) = 0;
  *(_BYTE *)(v32 + 1326) = 0;
  *(_BYTE *)(v32 + 1327) = 0;
  *(_BYTE *)(v32 + 572) = 5;
  *(_BYTE *)(v32 + 620) = 15;
  *(_BYTE *)(v32 + 622) = 0;
  *(_BYTE *)(v32 + 623) = 0;
  *(_BYTE *)(v32 + 586) = 0;
  *(_BYTE *)(v32 + 587) = 0;
  *(_BYTE *)(v32 + 584) = 0;
  *(_BYTE *)(v32 + 585) = 0;
  *(_BYTE *)(v32 + 588) = 0;
  *(_DWORD *)(v32 + 592) = 0;
  *(_DWORD *)(v32 + 184) = 0;
  *(_DWORD *)(v32 + 180) = 0;
  *(_DWORD *)(v32 + 188) = 0;
  *(_DWORD *)(v32 + 596) = 5;
  *(_DWORD *)(v32 + 600) = 5;
  *(_DWORD *)(v32 + 604) = 5;
  *(_DWORD *)(v32 + 608) = GetTickCount();
  *(_BYTE *)(v32 + 612) = 0;
  *(_DWORD *)(v32 + 192) = 0;
  *(_DWORD *)(v32 + 616) = 10000000;
  *(_DWORD *)(v32 + 92) = 0;
  *(_DWORD *)(v32 + 400) = 0;
  System::__linkproc__ FillChar(v32 + 96, 24, 0);
  System::__linkproc__ FillChar(v32 + 202, 20, 0);
  System::__linkproc__ FillChar(v32 + 222, 20, 0);
  System::__linkproc__ FillChar(v32 + 536, 6, 0);
  System::__linkproc__ FillChar(v32 + 544, 24, 0);
  *(_BYTE *)(v32 + 176) = 0;
  *(_BYTE *)(v32 + 177) = 0;
  *(_BYTE *)(v32 + 178) = 0;
  *(_BYTE *)(v32 + 673) = 0;
  *(_BYTE *)(v32 + 1000) = 0;
  *(_BYTE *)(v32 + 680) = 0;
  *(_BYTE *)(v32 + 688) = 0;
  *(_BYTE *)(v32 + 696) = 0;
  *(_BYTE *)(v32 + 706) = 0;
  *(_BYTE *)(v32 + 703) = 0;
  *(_BYTE *)(v32 + 716) = 0;
  *(_BYTE *)(v32 + 728) = 0;
  *(_BYTE *)(v32 + 740) = 0;
  *(_BYTE *)(v32 + 752) = 0;
  *(_BYTE *)(v32 + 699) = 0;
  *(_BYTE *)(v32 + 700) = 0;
  *(_DWORD *)(v32 + 712) = 50;
  *(_BYTE *)(v32 + 701) = 0;
  *(_BYTE *)(v32 + 702) = 0;
  *(_BYTE *)(v32 + 704) = 0;
  *(_BYTE *)(v32 + 705) = 0;
  *(_BYTE *)(v32 + 924) = 0;
  *(_DWORD *)(v32 + 932) = 0;
  *(_DWORD *)(v32 + 936) = 0;
  *(_DWORD *)(v32 + 944) = 0;
  *(_DWORD *)(v32 + 948) = 0;
  System::__linkproc__ FillChar(v32 + 448, 34, 0);
  LOBYTE(v2) = 1;
  v3 = unknown_libname_22(off_40FAFC, v2);
  v4 = v32;
  *(_DWORD *)(v32 + 980) = v3;
  LOBYTE(v4) = 1;
  v5 = unknown_libname_22(off_410028, v4);
  v6 = v32;
  *(_DWORD *)(v32 + 984) = v5;
  LOBYTE(v6) = 1;
  v7 = unknown_libname_22(off_40FAFC, v6);
  v8 = v32;
  *(_DWORD *)(v32 + 1004) = v7;
  LOBYTE(v8) = 1;
  v9 = unknown_libname_22(off_40FAFC, v8);
  v10 = v32;
  *(_DWORD *)(v32 + 1032) = v9;
  LOBYTE(v10) = 1;
  v11 = unknown_libname_22(off_40FAFC, v10);
  v12 = v32;
  *(_DWORD *)(v32 + 988) = v11;
  LOBYTE(v12) = 1;
  v13 = unknown_libname_22(off_40FAFC, v12);
  v14 = v32;
  *(_DWORD *)(v32 + 992) = v13;
  LOBYTE(v14) = 1;
  v15 = unknown_libname_22(off_40FAFC, v14);
  v16 = v32;
  *(_DWORD *)(v32 + 1036) = v15;
  LOBYTE(v16) = 1;
  *(_DWORD *)(v32 + 1040) = unknown_libname_22(off_40FAFC, v16);
  *(_DWORD *)(v32 + 1044) = 0;
  v17 = unknown_libname_22(off_40FAFC, 1);
  v18 = v32;
  *(_DWORD *)(v32 + 1052) = v17;
  LOBYTE(v18) = 1;
  *(_DWORD *)(v32 + 1272) = unknown_libname_22(off_40FAFC, v18);
  System::__linkproc__ FillChar(v32 + 1056, 216, 0);
  *(_DWORD *)(v32 + 1304) = 0;
  *(_DWORD *)(v32 + 1308) = 0;
  *(_DWORD *)(v32 + 1312) = 0;
  *(_DWORD *)(v32 + 1316) = 0;
  *(_DWORD *)(v32 + 1320) = 0;
  *(_DWORD *)(v32 + 628) = 0;
  *(_DWORD *)(v32 + 684) = 0;
  *(_DWORD *)(v32 + 648) = 0;
  *(_DWORD *)(v32 + 660) = 0;
  *(_BYTE *)(v32 + 664) = 0;
  *(_BYTE *)(v32 + 672) = 0;
  v19 = unknown_libname_22(off_410028, 1);
  v20 = v32;
  *(_DWORD *)(v32 + 632) = v19;
  *(_BYTE *)(v32 + 636) = 1;
  *(_BYTE *)(v32 + 637) = 1;
  *(_BYTE *)(v32 + 638) = 1;
  *(_BYTE *)(v32 + 639) = 1;
  *(_BYTE *)(v32 + 270) = 0;
  LOBYTE(v20) = 1;
  v21 = unknown_libname_22(off_410028, v20);
  v22 = v32;
  *(_DWORD *)(v32 + 640) = v21;
  LOBYTE(v22) = 1;
  *(_DWORD *)(v32 + 668) = unknown_libname_22(off_40FAFC, v22);
  System::__linkproc__ FillChar(v32 + 297, 100, 0);
  System::__linkproc__ FillChar(v32 + 271, 13, 0);
  System::__linkproc__ FillChar(v32 + 284, 13, 0);
  *(_BYTE *)(v32 + 52) = 1;
  *(_WORD *)(v32 + 54) = 0;
  v23 = v32 + 52;
  *(_WORD *)(v32 + 56) = 0;
  LOBYTE(v24) = 4;
  LOBYTE(v23) = 1;
  v25 = unknown_libname_77(v23, v24);
  v26 = v32 + 52;
  *(_WORD *)(v32 + 58) = v25;
  LOBYTE(v26) = 2;
  LOBYTE(v25) = 1;
  v27 = unknown_libname_77(v25, v26);
  v28 = v32 + 52;
  *(_WORD *)(v32 + 60) = v27;
  LOBYTE(v28) = 2;
  LOBYTE(v27) = 1;
  *(_WORD *)(v32 + 62) = unknown_libname_77(v27, v28);
  *(_WORD *)(v32 + 66) = 15;
  *(_WORD *)(v32 + 64) = 15;
  *(_WORD *)(v32 + 68) = 15;
  *(_WORD *)(v32 + 70) = 15;
  *(_DWORD *)(v32 + 76) = 0;
  *(_DWORD *)(v32 + 80) = 50;
  *(_WORD *)(v32 + 84) = 0;
  *(_WORD *)(v32 + 86) = 100;
  *(_BYTE *)(v32 + 788) = 0;
  *(_BYTE *)(v32 + 791) = 0;
  *(_DWORD *)(v32 + 796) = 0;
  *(_DWORD *)(v32 + 800) = 0;
  *(_DWORD *)(v32 + 804) = 0;
  unknown_libname_34(v32 + 808);
  unknown_libname_34(v32 + 812);
  *(_BYTE *)(v32 + 824) = 0;
  *(_BYTE *)(v32 + 836) = 0;
  *(_BYTE *)(v32 + 837) = 0;
  *(_BYTE *)(v32 + 838) = 0;
  *(_BYTE *)(v32 + 839) = 0;
  *(_BYTE *)(v32 + 840) = 0;
  *(_BYTE *)(v32 + 841) = 0;
  *(_BYTE *)(v32 + 842) = 0;
  v29 = System::__linkproc__ RandInt(1500);
  *(_DWORD *)(v32 + 872) = sub_407B30(v29) + v29;
  *(_DWORD *)(v32 + 876) = 250;
  *(_DWORD *)(v32 + 864) = System::__linkproc__ RandInt(2000) + 2000;
  *(_DWORD *)(v32 + 868) = GetTickCount();
  *(_DWORD *)(v32 + 956) = GetTickCount();
  *(_DWORD *)(v32 + 960) = GetTickCount();
  *(_DWORD *)(v32 + 952) = GetTickCount();
  *(_DWORD *)(v32 + 964) = GetTickCount();
  *(_DWORD *)(v32 + 968) = GetTickCount();
  *(_DWORD *)(v32 + 972) = GetTickCount();
  *(_DWORD *)(v32 + 976) = GetTickCount();
  *(_DWORD *)(v32 + 644) = 0;
  *(_DWORD *)(v32 + 912) = 0;
  *(_DWORD *)(v32 + 916) = 0;
  *(_DWORD *)(v32 + 920) = GetTickCount();
  *(_DWORD *)(v32 + 656) = 0;
  *(_DWORD *)(v32 + 1276) = 1400;
  *(_DWORD *)(v32 + 1300) = 3000;
  *(_DWORD *)(v32 + 1284) = 0;
  *(_DWORD *)(v32 + 1292) = GetTickCount();
  *(_BYTE *)(v32 + 1296) = 0;
  *(_DWORD *)(v32 + 880) = 0;
  *(_DWORD *)(v32 + 884) = 0;
  *(_DWORD *)(v32 + 888) = 0;
  *(_DWORD *)(v32 + 896) = 0;
  *(_DWORD *)(v32 + 904) = 0;
  if ( v31 != 0 )
    unknown_libname_27(v32);
  return v32;
}


// ==================== [0x4B80C0] sub_4B80C0 (size: 990) ====================
int __fastcall sub_4B80C0(int a1)
{
  __int64 v1; // rax
  int v2; // eax
  int v3; // eax
  int v4; // eax
  int v5; // eax
  int v6; // edx
  int v7; // eax
  int v8; // eax
  int v9; // eax
  int v10; // eax
  int v11; // eax
  int v12; // eax
  int v13; // eax
  int v14; // eax
  unsigned int v15; // edx
  int result; // eax
  unsigned int v17[6]; // [esp-Ch] [ebp-28h] BYREF
  int v18; // [esp+Ch] [ebp-10h]
  int v19; // [esp+10h] [ebp-Ch]
  char v20; // [esp+17h] [ebp-5h]
  System::TObject **v21; // [esp+18h] [ebp-4h]
  int savedregs; // [esp+1Ch] [ebp+0h] BYREF

  v1 = unknown_libname_28(a1);
  v20 = BYTE4(v1);
  v21 = (System::TObject **)v1;
  v17[2] = (unsigned int)&savedregs;
  v17[1] = (unsigned int)&loc_4B8467;
  v17[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v17);
  v2 = *(_DWORD *)(*(_DWORD *)(v1 + 980) + 8) - 1;
  if ( v2 >= 0 )
  {
    v18 = v2 + 1;
    v19 = 0;
    do
    {
      if ( *(_WORD *)Classes::TList::Get(v21[245], v19) == 10148
        && *(_DWORD *)(Classes::TList::Get(v21[245], v19) + 4) != 0 )
      {
        v3 = Classes::TList::Get(v21[245], v19);
        System::TObject::Free(*(System::TObject **)(v3 + 4));
      }
      if ( *(_WORD *)Classes::TList::Get(v21[245], v19) == 10401
        && *(_DWORD *)(Classes::TList::Get(v21[245], v19) + 4) != 0 )
      {
        v4 = Classes::TList::Get(v21[245], v19);
        System::__linkproc__ FreeMem(*(_DWORD *)(v4 + 4), 36);
      }
      if ( *(_DWORD *)(Classes::TList::Get(v21[245], v19) + 24) != 0 )
      {
        v5 = Classes::TList::Get(v21[245], v19);
        System::__linkproc__ FreeMem(*(_DWORD *)(v5 + 24), v6);
      }
      v7 = Classes::TList::Get(v21[245], v19);
      System::__linkproc__ FreeMem(v7, 28);
      ++v19;
      --v18;
    }
    while ( v18 != 0 );
  }
  System::TObject::Free(v21[245]);
  System::TObject::Free(v21[246]);
  if ( *((_DWORD *)v21[251] + 2) - 1 >= 0 )
  {
    v18 = *((_DWORD *)v21[251] + 2);
    v19 = 0;
    do
    {
      v8 = Classes::TList::Get(v21[251], v19);
      System::__linkproc__ FreeMem(v8, 8);
      ++v19;
      --v18;
    }
    while ( v18 != 0 );
  }
  System::TObject::Free(v21[251]);
  if ( *((_DWORD *)v21[258] + 2) - 1 >= 0 )
  {
    v18 = *((_DWORD *)v21[258] + 2);
    v19 = 0;
    do
    {
      v9 = Classes::TList::Get(v21[258], v19);
      System::__linkproc__ FreeMem(v9, 8);
      ++v19;
      --v18;
    }
    while ( v18 != 0 );
  }
  System::TObject::Free(v21[258]);
  if ( *((_DWORD *)v21[247] + 2) - 1 >= 0 )
  {
    v18 = *((_DWORD *)v21[247] + 2);
    v19 = 0;
    do
    {
      v10 = Classes::TList::Get(v21[247], v19);
      System::__linkproc__ FreeMem(v10, 32);
      ++v19;
      --v18;
    }
    while ( v18 != 0 );
  }
  System::TObject::Free(v21[247]);
  System::TObject::Free(v21[248]);
  if ( *((_DWORD *)v21[259] + 2) - 1 >= 0 )
  {
    v18 = *((_DWORD *)v21[259] + 2);
    v19 = 0;
    do
    {
      v11 = Classes::TList::Get(v21[259], v19);
      System::__linkproc__ FreeMem(v11, 24);
      ++v19;
      --v18;
    }
    while ( v18 != 0 );
  }
  System::TObject::Free(v21[259]);
  if ( *((_DWORD *)v21[260] + 2) - 1 >= 0 )
  {
    v18 = *((_DWORD *)v21[260] + 2);
    v19 = 0;
    do
    {
      v12 = Classes::TList::Get(v21[260], v19);
      System::__linkproc__ FreeMem(v12, 24);
      ++v19;
      --v18;
    }
    while ( v18 != 0 );
  }
  System::TObject::Free(v21[260]);
  if ( *((_DWORD *)v21[263] + 2) - 1 >= 0 )
  {
    v18 = *((_DWORD *)v21[263] + 2);
    v19 = 0;
    do
    {
      v13 = Classes::TList::Get(v21[263], v19);
      System::__linkproc__ FreeMem(v13, 12);
      ++v19;
      --v18;
    }
    while ( v18 != 0 );
  }
  System::TObject::Free(v21[263]);
  if ( *((_DWORD *)v21[318] + 2) - 1 >= 0 )
  {
    v18 = *((_DWORD *)v21[318] + 2);
    v19 = 0;
    do
    {
      v14 = Classes::TList::Get(v21[318], v19);
      System::__linkproc__ FreeMem(v14, 24);
      ++v19;
      --v18;
    }
    while ( v18 != 0 );
  }
  System::TObject::Free(v21[318]);
  System::TObject::Free(v21[158]);
  System::TObject::Free(v21[160]);
  System::TObject::Free(v21[167]);
  v15 = v17[0];
  __writefsdword(0, v17[0]);
  LOBYTE(v15) = v20 & 0xFC;
  result = sub_402EF0(v21, v15);
  if ( v20 > 0 )
    return System::__linkproc__ ClassDestroy(v21);
  return result;
}


// ==================== [0x4B84FC] sub_4B84FC (size: 351) ====================
int __fastcall sub_4B84FC(int a1, int a2, __int16 a3, __int16 a4, int a5, int a6, int a7, int a8)
{
  int v8; // eax
  int v9; // eax
  const void *v10; // eax
  int v12; // [esp-28h] [ebp-44h]
  unsigned int v13[3]; // [esp-24h] [ebp-40h] BYREF
  unsigned int v14[2]; // [esp-18h] [ebp-34h] BYREF
  int *v15; // [esp-10h] [ebp-2Ch]
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-Ch] [ebp-28h] BYREF
  void *v17; // [esp-8h] [ebp-24h]
  int *v18; // [esp-4h] [ebp-20h]
  void *v19; // [esp+0h] [ebp-1Ch]
  void *Mem; // [esp+Ch] [ebp-10h]
  __int16 v21; // [esp+12h] [ebp-Ah]
  int v22; // [esp+14h] [ebp-8h]
  int v23; // [esp+18h] [ebp-4h]
  int savedregs; // [esp+1Ch] [ebp+0h] BYREF

  v21 = a3;
  v22 = a2;
  v23 = a1;
  unknown_libname_44(a8);
  v18 = &savedregs;
  v17 = &loc_4B864B;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  v15 = &savedregs;
  v14[1] = (unsigned int)&loc_4B862E;
  v14[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v14);
  sub_450080(*off_4EA2A4);
  if ( *(_BYTE *)(v23 + 764) == 0 )
  {
    Mem = (void *)System::__linkproc__ GetMem(28);
    *(_WORD *)Mem = v21;
    *((_WORD *)Mem + 1) = a4;
    *((_DWORD *)Mem + 1) = a5;
    *((_DWORD *)Mem + 2) = a6;
    *((_DWORD *)Mem + 3) = a7;
    *((_DWORD *)Mem + 4) = v22;
    if ( a8 != 0 )
    {
      v13[2] = (unsigned int)&savedregs;
      v13[1] = (unsigned int)&loc_4B85E5;
      v13[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
      __writefsdword(0, (unsigned int)v13);
      v8 = unknown_libname_43(a8);
      v9 = System::__linkproc__ GetMem(v8 + 1);
      *((_DWORD *)Mem + 6) = v9;
      v12 = unknown_libname_43(a8) + 1;
      v10 = (const void *)System::UniqueString(&a8);
      System::Move(v10, *((void **)Mem + 6), v12);
      __writefsdword(0, v13[0]);
    }
    else
    {
      *((_DWORD *)Mem + 6) = 0;
    }
    Classes::TList::Insert(*(Classes::TList **)(v23 + 980), 0, Mem);
  }
  __writefsdword(0, v14[0]);
  v15 = (int *)&loc_4B8635;
  Webadapt::TBaseAdapterErrorsList::GetErrorCount((Webadapt::TBaseAdapterErrorsList *)*off_4EA2A4);
  __writefsdword(0, (unsigned int)v17);
  v19 = &loc_4B8652;
  return unknown_libname_34(&a8);
}


// ==================== [0x4B865C] sub_4B865C (size: 357) ====================
int __fastcall sub_4B865C(int a1, int a2, __int16 a3, __int16 a4, int a5, int a6, int a7, int a8)
{
  int v8; // eax
  int v9; // eax
  const void *v10; // eax
  int v12; // [esp-28h] [ebp-44h]
  unsigned int v13[3]; // [esp-24h] [ebp-40h] BYREF
  unsigned int v14[2]; // [esp-18h] [ebp-34h] BYREF
  int *v15; // [esp-10h] [ebp-2Ch]
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-Ch] [ebp-28h] BYREF
  void *v17; // [esp-8h] [ebp-24h]
  int *v18; // [esp-4h] [ebp-20h]
  void *v19; // [esp+0h] [ebp-1Ch]
  void *Mem; // [esp+Ch] [ebp-10h]
  __int16 v21; // [esp+12h] [ebp-Ah]
  int v22; // [esp+14h] [ebp-8h]
  int v23; // [esp+18h] [ebp-4h]
  int savedregs; // [esp+1Ch] [ebp+0h] BYREF

  v21 = a3;
  v22 = a2;
  v23 = a1;
  unknown_libname_44(a8);
  v18 = &savedregs;
  v17 = &loc_4B87B1;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  v15 = &savedregs;
  v14[1] = (unsigned int)&loc_4B8794;
  v14[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v14);
  sub_450080(*off_4EA2A4);
  if ( *(_BYTE *)(v23 + 764) == 0 )
  {
    Mem = (void *)System::__linkproc__ GetMem(28);
    *(_WORD *)Mem = v21;
    *((_WORD *)Mem + 1) = a4;
    *((_DWORD *)Mem + 1) = a5;
    *((_DWORD *)Mem + 2) = a6;
    *((_DWORD *)Mem + 3) = a7;
    *((_DWORD *)Mem + 5) = 0;
    *((_DWORD *)Mem + 4) = v22;
    if ( a8 != 0 )
    {
      v13[2] = (unsigned int)&savedregs;
      v13[1] = (unsigned int)&loc_4B874D;
      v13[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
      __writefsdword(0, (unsigned int)v13);
      v8 = unknown_libname_43(a8);
      v9 = System::__linkproc__ GetMem(v8 + 1);
      *((_DWORD *)Mem + 6) = v9;
      v12 = unknown_libname_43(a8) + 1;
      v10 = (const void *)System::UniqueString(&a8);
      System::Move(v10, *((void **)Mem + 6), v12);
      __writefsdword(0, v13[0]);
    }
    else
    {
      *((_DWORD *)Mem + 6) = 0;
    }
    Classes::TList::Add(*(Classes::TList **)(v23 + 980), Mem);
  }
  __writefsdword(0, v14[0]);
  v15 = (int *)&loc_4B879B;
  Webadapt::TBaseAdapterErrorsList::GetErrorCount((Webadapt::TBaseAdapterErrorsList *)*off_4EA2A4);
  __writefsdword(0, (unsigned int)v17);
  v19 = &loc_4B87B8;
  return unknown_libname_34(&a8);
}


// ==================== [0x4B87C4] sub_4B87C4 (size: 363) ====================
int __fastcall sub_4B87C4(int a1, int a2, __int16 a3, __int16 a4, int a5, int a6, int a7, int a8, int a9)
{
  int v9; // eax
  int v10; // eax
  const void *v11; // eax
  int v13; // [esp-28h] [ebp-44h]
  unsigned int v14[3]; // [esp-24h] [ebp-40h] BYREF
  unsigned int v15[2]; // [esp-18h] [ebp-34h] BYREF
  int *v16; // [esp-10h] [ebp-2Ch]
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-Ch] [ebp-28h] BYREF
  void *v18; // [esp-8h] [ebp-24h]
  int *v19; // [esp-4h] [ebp-20h]
  void *v20; // [esp+0h] [ebp-1Ch]
  void *Mem; // [esp+Ch] [ebp-10h]
  __int16 v22; // [esp+12h] [ebp-Ah]
  int v23; // [esp+14h] [ebp-8h]
  int v24; // [esp+18h] [ebp-4h]
  int savedregs; // [esp+1Ch] [ebp+0h] BYREF

  v22 = a3;
  v23 = a2;
  v24 = a1;
  unknown_libname_44(a8);
  v19 = &savedregs;
  v18 = &loc_4B891F;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  v16 = &savedregs;
  v15[1] = (unsigned int)&loc_4B8902;
  v15[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v15);
  sub_450080(*off_4EA2A4);
  if ( *(_BYTE *)(v24 + 764) == 0 )
  {
    Mem = (void *)System::__linkproc__ GetMem(28);
    *(_WORD *)Mem = v22;
    *((_WORD *)Mem + 1) = a4;
    *((_DWORD *)Mem + 1) = a5;
    *((_DWORD *)Mem + 2) = a6;
    *((_DWORD *)Mem + 3) = a7;
    *((_DWORD *)Mem + 5) = a9 + GetTickCount();
    *((_DWORD *)Mem + 4) = v23;
    if ( a8 != 0 )
    {
      v14[2] = (unsigned int)&savedregs;
      v14[1] = (unsigned int)&loc_4B88BB;
      v14[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
      __writefsdword(0, (unsigned int)v14);
      v9 = unknown_libname_43(a8);
      v10 = System::__linkproc__ GetMem(v9 + 1);
      *((_DWORD *)Mem + 6) = v10;
      v13 = unknown_libname_43(a8) + 1;
      v11 = (const void *)System::UniqueString(&a8);
      System::Move(v11, *((void **)Mem + 6), v13);
      __writefsdword(0, v14[0]);
    }
    else
    {
      *((_DWORD *)Mem + 6) = 0;
    }
    Classes::TList::Add(*(Classes::TList **)(v24 + 980), Mem);
  }
  __writefsdword(0, v15[0]);
  v16 = (int *)&loc_4B8909;
  Webadapt::TBaseAdapterErrorsList::GetErrorCount((Webadapt::TBaseAdapterErrorsList *)*off_4EA2A4);
  __writefsdword(0, (unsigned int)v18);
  v20 = &loc_4B8926;
  return unknown_libname_34(&a8);
}


// ==================== [0x4B8930] sub_4B8930 (size: 332) ====================
int __fastcall sub_4B8930(int a1, unsigned int a2, __int16 a3, __int16 a4, int a5, int a6, int a7, int a8, int a9)
{
  _WORD *v9; // eax
  int v10; // eax
  int v11; // edx
  __int16 v12; // eax^2
  unsigned int v14[2]; // [esp-18h] [ebp-2Ch] BYREF
  void **v15; // [esp-10h] [ebp-24h]
  _DWORD v16[3]; // [esp-Ch] [ebp-20h] BYREF
  int v17; // [esp+0h] [ebp-14h]
  int v18; // [esp+4h] [ebp-10h]
  __int16 v19; // [esp+Ah] [ebp-Ah]
  unsigned int v20; // [esp+Ch] [ebp-8h]
  int v21; // [esp+10h] [ebp-4h]
  void *savedregs; // [esp+14h] [ebp+0h] BYREF

  v19 = a3;
  v20 = a2;
  v21 = a1;
  unknown_libname_44(a8);
  v16[2] = &savedregs;
  v16[1] = &loc_4B8A6F;
  v16[0] = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v16);
  v15 = &savedregs;
  v14[1] = (unsigned int)&loc_4B8A2A;
  v14[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v14);
  sub_450080(*off_4EA2A4);
  v18 = 0;
  while ( *(_DWORD *)(*(_DWORD *)(v21 + 980) + 8) > v18 )
  {
    v9 = (_WORD *)Classes::TList::Get(*(Classes::TList **)(v21 + 980), v18);
    if ( *v9 == v19 && *(_DWORD *)((v10 = Classes::TList::Get(*(Classes::TList **)(v21 + 980), v18)) + 4) == a5 )
    {
      v17 = Classes::TList::Get(*(Classes::TList **)(v21 + 980), v18);
      Classes::TList::Delete(*(Classes::TList **)(v21 + 980), v18);
      if ( *(_DWORD *)(v17 + 24) != 0 )
        System::__linkproc__ FreeMem(*(_DWORD *)(v17 + 24), v11);
      System::__linkproc__ FreeMem(v17, 28);
    }
    else
    {
      ++v18;
    }
  }
  __writefsdword(0, v14[0]);
  v15 = (void **)&loc_4B8A31;
  v12 = Webadapt::TBaseAdapterErrorsList::GetErrorCount((Webadapt::TBaseAdapterErrorsList *)*off_4EA2A4) >> 16;
  sub_4B87C4(v21, v20, v12, a4, a5, a6, a7, a8, a9);
  __writefsdword(0, v20);
  savedregs = &loc_4B8A76;
  return unknown_libname_34(&a8);
}


// ==================== [0x4B8BA4] sub_4B8BA4 (size: 390) ====================
// bad sp value at call has been detected, the output may be wrong!
char __fastcall sub_4B8BA4(int a1, unsigned int a2)
{
  int v2; // edx
  unsigned int v4[2]; // [esp-18h] [ebp-30h] BYREF
  void **v5; // [esp-10h] [ebp-28h]
  _DWORD v6[3]; // [esp-Ch] [ebp-24h] BYREF
  int v7; // [esp+0h] [ebp-18h] BYREF
  int v8; // [esp+4h] [ebp-14h]
  int v9; // [esp+8h] [ebp-10h]
  char v10; // [esp+Fh] [ebp-9h]
  unsigned int v11; // [esp+10h] [ebp-8h]
  int v12; // [esp+14h] [ebp-4h]
  void *savedregs; // [esp+18h] [ebp+0h] BYREF

  v7 = 0;
  v11 = a2;
  v12 = a1;
  v6[2] = &savedregs;
  v6[1] = &loc_4B8D1C;
  v6[0] = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v6);
  v10 = 0;
  v5 = &savedregs;
  v4[1] = (unsigned int)&loc_4B8CFF;
  v4[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v4);
  sub_450080(*off_4EA2A4);
  v8 = 0;
  *(_WORD *)v11 = 0;
  while ( *(_DWORD *)(*(_DWORD *)(v12 + 980) + 8) > v8 )
  {
    v9 = Classes::TList::Get(*(Classes::TList **)(v12 + 980), v8);
    if ( *(_DWORD *)(v9 + 20) == 0 || GetTickCount() >= *(_DWORD *)(v9 + 20) )
    {
      Classes::TList::Delete(*(Classes::TList **)(v12 + 980), v8);
      *(_WORD *)v11 = *(_WORD *)v9;
      *(_WORD *)(v11 + 2) = *(_WORD *)(v9 + 2);
      *(_DWORD *)(v11 + 4) = *(_DWORD *)(v9 + 4);
      *(_DWORD *)(v11 + 8) = *(_DWORD *)(v9 + 8);
      *(_DWORD *)(v11 + 12) = *(_DWORD *)(v9 + 12);
      *(_DWORD *)(v11 + 16) = *(_DWORD *)(v9 + 16);
      if ( *(_DWORD *)(v9 + 24) != 0 )
      {
        sub_409790(*(_DWORD *)(v9 + 24), &v7);
        unknown_libname_36(v11 + 24, v7);
        System::__linkproc__ FreeMem(*(_DWORD *)(v9 + 24), v2);
      }
      else
      {
        unknown_libname_34(v11 + 24);
      }
      System::__linkproc__ FreeMem(v9, 28);
      v10 = 1;
      break;
    }
    ++v8;
  }
  __writefsdword(0, v4[0]);
  v5 = (void **)&loc_4B8D06;
  Webadapt::TBaseAdapterErrorsList::GetErrorCount((Webadapt::TBaseAdapterErrorsList *)*off_4EA2A4);
  __writefsdword(0, v11);
  savedregs = &loc_4B8D23;
  unknown_libname_34(&v7);
  return v10;
}


// ==================== [0x4B8D2C] sub_4B8D2C (size: 349) ====================
char __fastcall sub_4B8D2C(int a1, unsigned int a2, int a3, int a4, int a5, Classes::TList *a6)
{
  unsigned int v7[7]; // [esp-Ch] [ebp-58h] BYREF
  int v8; // [esp+10h] [ebp-3Ch]
  int v9; // [esp+14h] [ebp-38h]
  char v10; // [esp+1Bh] [ebp-31h]
  int v11; // [esp+1Ch] [ebp-30h] BYREF
  void *v12; // [esp+20h] [ebp-2Ch]
  int v13; // [esp+24h] [ebp-28h]
  int v14; // [esp+28h] [ebp-24h]
  int v15; // [esp+2Ch] [ebp-20h]
  int v16; // [esp+30h] [ebp-1Ch]
  int v17; // [esp+34h] [ebp-18h]
  int v18; // [esp+38h] [ebp-14h]
  int v19; // [esp+3Ch] [ebp-10h]
  char v20; // [esp+43h] [ebp-9h]
  int v21; // [esp+44h] [ebp-8h]
  int v22; // [esp+48h] [ebp-4h]
  int savedregs; // [esp+4Ch] [ebp+0h] BYREF

  v21 = a3;
  v7[6] = a2;
  v22 = a1;
  v20 = 0;
  if ( a6 != nullptr )
  {
    v7[2] = (unsigned int)&savedregs;
    v7[1] = (unsigned int)&loc_4B8E65;
    v7[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
    __writefsdword(0, (unsigned int)v7);
    v16 = v21 - a5;
    v14 = a5 + v21;
    v15 = a4 - a5;
    v13 = a5 + a4;
    if ( ((a5 & 0x40000000) != 0) == __OFSUB__(a5 + v21, v21 - a5) )
    {
      v9 = 2 * a5 + 1;
      v19 = v21 - a5;
      do
      {
        if ( v13 >= v15 )
        {
          v8 = v13 - v15 + 1;
          v18 = v15;
          do
          {
            v10 = sub_4B57D8(&v11);
            if ( v10 != 0 && *(_DWORD *)(v11 + 4) != 0 && *(_DWORD *)(*(_DWORD *)(v11 + 4) + 8) - 1 >= 0 )
            {
              v17 = *(_DWORD *)(*(_DWORD *)(v11 + 4) + 8) - 1;
              do
              {
                if ( Classes::TList::Get(*(Classes::TList **)(v11 + 4), v17) != 0
                  && *(_BYTE *)Classes::TList::Get(*(Classes::TList **)(v11 + 4), v17) == 1 )
                {
                  v12 = *(void **)(Classes::TList::Get(*(Classes::TList **)(v11 + 4), v17) + 4);
                  if ( v12 != nullptr && *((_BYTE *)v12 + 764) == 0 )
                    Classes::TList::Add(a6, v12);
                }
                --v17;
              }
              while ( v17 != -1 );
            }
            ++v18;
            --v8;
          }
          while ( v8 != 0 );
        }
        ++v19;
        --v9;
      }
      while ( v9 != 0 );
    }
    __writefsdword(0, v7[0]);
    return 1;
  }
  return v20;
}


// ==================== [0x4B8EBC] sub_4B8EBC (size: 1205) ====================
int __fastcall sub_4B8EBC(int a1, __int16 a2, __int16 a3, int a4, int a5, int a6, int a7)
{
  int v7; // ecx
  int v8; // ecx
  int v9; // eax
  int v10; // eax
  char v11; // zf
  __int64 v12; // rax
  __int64 v13; // rax
  struct _EXCEPTION_REGISTRATION_RECORD *v15; // [esp-18h] [ebp-5Ch] BYREF
  struct _EXCEPTION_REGISTRATION_RECORD *v16; // [esp-14h] [ebp-58h] BYREF
  int *v17; // [esp-10h] [ebp-54h]
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-Ch] [ebp-50h] BYREF
  void *v19; // [esp-8h] [ebp-4Ch]
  int *v20; // [esp-4h] [ebp-48h]
  void *v21; // [esp+0h] [ebp-44h]
  int v22; // [esp+Ch] [ebp-38h]
  int v23; // [esp+10h] [ebp-34h]
  char v24; // [esp+17h] [ebp-2Dh]
  int v25; // [esp+18h] [ebp-2Ch] BYREF
  int v26; // [esp+1Ch] [ebp-28h]
  int v27; // [esp+20h] [ebp-24h]
  int v28; // [esp+24h] [ebp-20h]
  int v29; // [esp+28h] [ebp-1Ch]
  int v30; // [esp+2Ch] [ebp-18h]
  int v31; // [esp+30h] [ebp-14h]
  int v32; // [esp+34h] [ebp-10h]
  int v33; // [esp+38h] [ebp-Ch]
  __int16 v34; // [esp+3Ch] [ebp-8h]
  __int16 v35; // [esp+3Eh] [ebp-6h]
  int v36; // [esp+40h] [ebp-4h]
  int savedregs; // [esp+44h] [ebp+0h] BYREF

  v34 = a3;
  v35 = a2;
  v36 = a1;
  unknown_libname_44(a7);
  v20 = &savedregs;
  v19 = &loc_4B9361;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  if ( *(_BYTE *)(v36 + 842) == 0 && *(_BYTE *)(v36 + 701) == 0 )
  {
    v17 = &savedregs;
    v16 = (struct _EXCEPTION_REGISTRATION_RECORD *)&loc_4B9344;
    v15 = NtCurrentTeb()->NtTib.ExceptionList;
    __writefsdword(0, (unsigned int)&v15);
    sub_450080(*off_4EA2A4);
    if ( GetTickCount() - *(_DWORD *)(v36 + 996) < 0x1F4
      && (*(int (__fastcall **)(_DWORD))(**(_DWORD **)(v36 + 984) + 20))(*(_DWORD *)(v36 + 984)) != 0 )
    {
      if ( (*(int (__fastcall **)(_DWORD, _DWORD, int, struct _EXCEPTION_REGISTRATION_RECORD *, struct _EXCEPTION_REGISTRATION_RECORD *))(**(_DWORD **)(v36 + 984) + 20))(
             *(_DWORD *)(v36 + 984),
             **(_DWORD **)(v36 + 984),
             v7,
             v16,
             v15) > 0 )
      {
        v10 = (*(int (__fastcall **)(_DWORD))(**(_DWORD **)(v36 + 984) + 20))(*(_DWORD *)(v36 + 984)) - 1;
        if ( v10 >= 0 )
        {
          v23 = v10 + 1;
          v33 = 0;
          do
          {
            v26 = (*(int (__fastcall **)(_DWORD, int))(**(_DWORD **)(v36 + 984) + 24))(*(_DWORD *)(v36 + 984), v33);
            if ( *(_BYTE *)(v26 + 764) == 0 )
            {
              System::__linkproc__ AStrCmp(v19, v20, v21);
              if ( v11 )
              {
                v12 = *(_DWORD *)(v26 + 36) - *(_DWORD *)(v36 + 36);
                if ( (int)((HIDWORD(v12) ^ v12) - HIDWORD(v12)) <= 11 )
                {
                  v13 = *(_DWORD *)(v26 + 40) - *(_DWORD *)(v36 + 40);
                  if ( (int)((HIDWORD(v13) ^ v13) - HIDWORD(v13)) <= 11
                    && (*(_BYTE *)(v26 + 570) == 0
                     || *(_BYTE *)(v26 + 788) != 0 && (v35 == 10020 || v35 == 10030 || v35 == 10021)) )
                  {
                    sub_4B865C(v26, v36, v35, v34, a4, a5, a6, a7);
                  }
                }
              }
            }
            ++v33;
            --v23;
          }
          while ( v23 != 0 );
        }
      }
    }
    else
    {
      *(_DWORD *)(v36 + 996) = GetTickCount();
      (*(void (__fastcall **)(_DWORD, _DWORD, int, struct _EXCEPTION_REGISTRATION_RECORD *, struct _EXCEPTION_REGISTRATION_RECORD *))(**(_DWORD **)(v36 + 984) + 64))(
        *(_DWORD *)(v36 + 984),
        **(_DWORD **)(v36 + 984),
        v8,
        v16,
        v15);
      v30 = *(_DWORD *)(v36 + 36) - 12;
      v28 = *(_DWORD *)(v36 + 36) + 12;
      v29 = *(_DWORD *)(v36 + 40) - 12;
      v27 = *(_DWORD *)(v36 + 40) + 12;
      if ( v28 >= v30 )
      {
        v23 = v28 - v30 + 1;
        v33 = v30;
        do
        {
          if ( v27 >= v29 )
          {
            v22 = v27 - v29 + 1;
            v32 = v29;
            do
            {
              v24 = sub_4B57D8(&v25);
              if ( v24 != 0 && *(_DWORD *)(v25 + 4) != 0 && *(_DWORD *)(*(_DWORD *)(v25 + 4) + 8) - 1 >= 0 )
              {
                v31 = *(_DWORD *)(*(_DWORD *)(v25 + 4) + 8) - 1;
                do
                {
                  if ( Classes::TList::Get(*(Classes::TList **)(v25 + 4), v31) != 0
                    && *(_BYTE *)Classes::TList::Get(*(Classes::TList **)(v25 + 4), v31) == 1 )
                  {
                    ExceptionList = *(struct _EXCEPTION_REGISTRATION_RECORD **)(Classes::TList::Get(
                                                                                  *(Classes::TList **)(v25 + 4),
                                                                                  v31)
                                                                              + 8);
                    if ( GetTickCount() - (unsigned int)ExceptionList < 0xEA60 )
                    {
                      ExceptionList = (struct _EXCEPTION_REGISTRATION_RECORD *)&savedregs;
                      v17 = (int *)&loc_4B918A;
                      v16 = NtCurrentTeb()->NtTib.ExceptionList;
                      __writefsdword(0, (unsigned int)&v16);
                      v26 = *(_DWORD *)(Classes::TList::Get(*(Classes::TList **)(v25 + 4), v31) + 4);
                      if ( v26 != 0
                        && *(_BYTE *)(v26 + 764) == 0
                        && (*(_BYTE *)(v26 + 570) == 0
                         || *(_BYTE *)(v26 + 788) != 0 && (v35 == 10020 || v35 == 10030 || v35 == 10021)) )
                      {
                        sub_4B865C(v26, v36, v35, v34, a4, a5, a6, a7);
                        (*(void (__fastcall **)(_DWORD, _DWORD, int))(**(_DWORD **)(v36 + 984) + 56))(
                          *(_DWORD *)(v36 + 984),
                          0,
                          v26);
                      }
                      __writefsdword(0, (unsigned int)v16);
                    }
                    else
                    {
                      v9 = Classes::TList::Get(*(Classes::TList **)(v25 + 4), v31);
                      System::__linkproc__ FreeMem(v9, 12);
                      Classes::TList::Delete(*(Classes::TList **)(v25 + 4), v31);
                      if ( *(int *)(*(_DWORD *)(v25 + 4) + 8) <= 0 )
                      {
                        System::TObject::Free(*(System::TObject **)(v25 + 4));
                        *(_DWORD *)(v25 + 4) = 0;
                        break;
                      }
                    }
                  }
                  --v31;
                }
                while ( v31 != -1 );
              }
              ++v32;
              --v22;
            }
            while ( v22 != 0 );
          }
          ++v33;
          --v23;
        }
        while ( v23 != 0 );
      }
    }
    __writefsdword(0, (unsigned int)v19);
    v21 = &loc_4B934B;
    Webadapt::TBaseAdapterErrorsList::GetErrorCount((Webadapt::TBaseAdapterErrorsList *)*off_4EA2A4);
  }
  __writefsdword(0, (unsigned int)ExceptionList);
  v20 = (int *)&loc_4B9368;
  return unknown_libname_34(&a7);
}


// ==================== [0x4B966C] sub_4B966C (size: 2861) ====================
int __fastcall sub_4B966C(int a1)
{
  int v1; // eax
  __int64 v2; // rax
  __int64 v3; // rax
  int v4; // eax
  int v5; // edx
  int v6; // eax
  int v7; // eax
  int v9; // [esp-28h] [ebp-128h]
  int v10; // [esp-24h] [ebp-124h]
  int v11; // [esp-20h] [ebp-120h]
  void *v12; // [esp-20h] [ebp-120h]
  int v13; // [esp-1Ch] [ebp-11Ch]
  int v14; // [esp-1Ch] [ebp-11Ch]
  __int16 v15; // [esp-1Ch] [ebp-11Ch]
  __int16 v16; // [esp-1Ch] [ebp-11Ch]
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-18h] [ebp-118h] BYREF
  void *v18; // [esp-14h] [ebp-114h]
  int *v19; // [esp-10h] [ebp-110h]
  unsigned int v20[2]; // [esp-Ch] [ebp-10Ch] BYREF
  int *v21; // [esp-4h] [ebp-104h]
  int v22[5]; // [esp+Ch] [ebp-F4h] BYREF
  int v23; // [esp+3Ch] [ebp-C4h] BYREF
  int v24; // [esp+A4h] [ebp-5Ch] BYREF
  int v25; // [esp+B4h] [ebp-4Ch]
  int v26; // [esp+B8h] [ebp-48h]
  int v27; // [esp+BCh] [ebp-44h] BYREF
  char v28; // [esp+C3h] [ebp-3Dh]
  int v29; // [esp+C4h] [ebp-3Ch]
  void *v30; // [esp+C8h] [ebp-38h]
  _DWORD *v31; // [esp+CCh] [ebp-34h]
  int v32; // [esp+D0h] [ebp-30h]
  int v33; // [esp+D4h] [ebp-2Ch]
  int v34; // [esp+D8h] [ebp-28h] BYREF
  int v35; // [esp+DCh] [ebp-24h]
  int v36; // [esp+E0h] [ebp-20h]
  int v37; // [esp+E4h] [ebp-1Ch]
  int v38; // [esp+E8h] [ebp-18h]
  int v39; // [esp+ECh] [ebp-14h]
  int v40; // [esp+F0h] [ebp-10h]
  int v41; // [esp+F4h] [ebp-Ch]
  int v42; // [esp+F8h] [ebp-8h]
  int v43; // [esp+FCh] [ebp-4h]
  int savedregs; // [esp+100h] [ebp+0h] BYREF

  v43 = a1;
  v21 = &savedregs;
  v20[1] = (unsigned int)&loc_4BA18B;
  v20[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v20);
  v35 = 0;
  if ( *(_DWORD *)(a1 + 760) == 0 )
  {
    sub_4E4274(&str_nil_PEnvir[1]);
    goto LABEL_91;
  }
  v19 = &savedregs;
  v18 = &loc_4B9772;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  if ( *(_DWORD *)(*(_DWORD *)(v43 + 988) + 8) - 1 >= 0 )
  {
    v26 = *(_DWORD *)(*(_DWORD *)(v43 + 988) + 8);
    v38 = 0;
    do
    {
      *(_BYTE *)Classes::TList::Get(*(Classes::TList **)(v43 + 988), v38++) = 0;
      --v26;
    }
    while ( v26 != 0 );
  }
  if ( *(_DWORD *)(*(_DWORD *)(v43 + 992) + 8) - 1 >= 0 )
  {
    v26 = *(_DWORD *)(*(_DWORD *)(v43 + 992) + 8);
    v38 = 0;
    do
    {
      *(_DWORD *)(Classes::TList::Get(*(Classes::TList **)(v43 + 992), v38++) + 4) = 0;
      --v26;
    }
    while ( v26 != 0 );
  }
  if ( *(_DWORD *)(*(_DWORD *)(v43 + 1032) + 8) - 1 >= 0 )
  {
    v26 = *(_DWORD *)(*(_DWORD *)(v43 + 1032) + 8);
    v38 = 0;
    do
    {
      *(_BYTE *)Classes::TList::Get(*(Classes::TList **)(v43 + 1032), v38++) = 0;
      --v26;
    }
    while ( v26 != 0 );
  }
  __writefsdword(0, (unsigned int)ExceptionList);
  v42 = *(_DWORD *)(v43 + 36) - *(_DWORD *)(v43 + 484);
  v41 = *(_DWORD *)(v43 + 484) + *(_DWORD *)(v43 + 36);
  v40 = *(_DWORD *)(v43 + 40) - *(_DWORD *)(v43 + 484);
  v39 = *(_DWORD *)(v43 + 484) + *(_DWORD *)(v43 + 40);
  v19 = &savedregs;
  v18 = &loc_4B9BAE;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  if ( v41 >= v42 )
  {
    v26 = v41 - v42 + 1;
    v38 = v42;
    while ( v39 < v40 )
    {
LABEL_58:
      ++v38;
      if ( --v26 == 0 )
        goto LABEL_59;
    }
    v25 = v39 - v40 + 1;
    v37 = v40;
    while ( 1 )
    {
      v28 = sub_4B57D8(&v34);
      if ( v28 != 0 && *(_DWORD *)(v34 + 4) != 0 )
        break;
LABEL_57:
      ++v37;
      if ( --v25 == 0 )
        goto LABEL_58;
    }
    v35 = 1;
    v36 = 0;
    while ( 1 )
    {
      while ( 1 )
      {
        while ( 1 )
        {
          if ( *(_DWORD *)(*(_DWORD *)(v34 + 4) + 8) <= v36 )
            goto LABEL_57;
          if ( Classes::TList::Get(*(Classes::TList **)(v34 + 4), v36) != 0 )
            break;
LABEL_56:
          ++v36;
        }
        if ( *(_BYTE *)Classes::TList::Get(*(Classes::TList **)(v34 + 4), v36) == 1 )
          break;
LABEL_38:
        if ( *(_BYTE *)(v43 + 570) != 0 )
          goto LABEL_56;
        v35 = 6;
        if ( *(_BYTE *)Classes::TList::Get(*(Classes::TList **)(v34 + 4), v36) != 2 )
          goto LABEL_53;
        v35 = 7;
        v14 = *(_DWORD *)(Classes::TList::Get(*(Classes::TList **)(v34 + 4), v36) + 8);
        if ( GetTickCount() - v14 <= 0x493E0 )
        {
          v35 = 10;
          v31 = *(_DWORD **)(Classes::TList::Get(*(Classes::TList **)(v34 + 4), v36) + 4);
          sub_4B94FC(v31);
          if ( v31[12] != 0 || v31[14] != 0 )
          {
            if ( GetTickCount() - v31[13] <= 0x1D4C0 )
            {
              if ( v31[12] != 0 && *(_BYTE *)(v31[12] + 764) != 0 )
                v31[12] = 0;
              if ( v31[14] != 0 && *(_BYTE *)(v31[14] + 764) != 0 )
                v31[14] = 0;
            }
            else
            {
              v31[12] = 0;
              v31[14] = 0;
            }
          }
LABEL_53:
          v35 = 11;
          if ( *(_BYTE *)Classes::TList::Get(*(Classes::TList **)(v34 + 4), v36) == 3 )
          {
            v30 = *(void **)(Classes::TList::Get(*(Classes::TList **)(v34 + 4), v36) + 4);
            if ( *((_BYTE *)v30 + 60) != 0 )
              sub_4B95D0(v30);
          }
          goto LABEL_56;
        }
        v4 = Classes::TList::Get(*(Classes::TList **)(v34 + 4), v36);
        System::__linkproc__ FreeMem(v4, 12);
        Classes::TList::Delete(*(Classes::TList **)(v34 + 4), v36);
        v35 = 8;
        if ( *(int *)(*(_DWORD *)(v34 + 4) + 8) <= 0 )
        {
          v35 = 9;
          System::TObject::Free(*(System::TObject **)(v34 + 4));
          *(_DWORD *)(v34 + 4) = 0;
          goto LABEL_57;
        }
      }
      v13 = *(_DWORD *)(Classes::TList::Get(*(Classes::TList **)(v34 + 4), v36) + 8);
      if ( GetTickCount() - v13 < 0xEA60 )
      {
        v29 = *(_DWORD *)(Classes::TList::Get(*(Classes::TList **)(v34 + 4), v36) + 4);
        v35 = 4;
        if ( v29 != 0 && *(_BYTE *)(v29 + 764) == 0 && *(_BYTE *)(v29 + 701) == 0 && *(_BYTE *)(v29 + 842) == 0 )
        {
          if ( (v35 = 5, *(_BYTE *)(v43 + 570) < 0x32u)
            || *(_DWORD *)(v43 + 648) != 0
            || *(_BYTE *)(v43 + 728) != 0
            || *(_BYTE *)(v43 + 788) != 0
            || *(_DWORD *)(v29 + 648) != 0
            && (v2 = *(_DWORD *)(v29 + 36) - *(_DWORD *)(v43 + 36), (int)((HIDWORD(v2) ^ v2) - HIDWORD(v2)) <= 3)
            && (v3 = *(_DWORD *)(v29 + 40) - *(_DWORD *)(v43 + 40), (int)((HIDWORD(v3) ^ v3) - HIDWORD(v3)) <= 3)
            || *(_BYTE *)(v29 + 570) == 0 )
          {
            sub_4B939C(v43, v29);
          }
        }
        goto LABEL_38;
      }
      v1 = Classes::TList::Get(*(Classes::TList **)(v34 + 4), v36);
      System::__linkproc__ FreeMem(v1, 12);
      Classes::TList::Delete(*(Classes::TList **)(v34 + 4), v36);
      v35 = 2;
      if ( *(int *)(*(_DWORD *)(v34 + 4) + 8) <= 0 )
      {
        v35 = 3;
        System::TObject::Free(*(System::TObject **)(v34 + 4));
        *(_DWORD *)(v34 + 4) = 0;
        goto LABEL_57;
      }
    }
  }
LABEL_59:
  __writefsdword(0, (unsigned int)ExceptionList);
  v19 = &savedregs;
  v18 = &loc_4B9E1E;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  v38 = 0;
  while ( *(_DWORD *)(*(_DWORD *)(v43 + 1032) + 8) > v38 )
  {
    v32 = Classes::TList::Get(*(Classes::TList **)(v43 + 1032), v38);
    if ( *(_BYTE *)v32 != 0 )
    {
      if ( *(_BYTE *)(v43 + 570) == 0 && *(_BYTE *)v32 == 2 )
      {
        v29 = *(_DWORD *)(v32 + 4);
        if ( v29 != v43 )
        {
          if ( *(_BYTE *)(v29 + 772) != 0 )
          {
            if ( *(_BYTE *)(v29 + 706) != 0 )
              sub_4B865C(
                v43,
                v29,
                10024,
                *(unsigned __int8 *)(v29 + 44),
                *(_DWORD *)(v29 + 36),
                *(_DWORD *)(v29 + 40),
                0,
                0);
            else
              sub_4B865C(
                v43,
                v29,
                10021,
                *(unsigned __int8 *)(v29 + 44),
                *(_DWORD *)(v29 + 36),
                *(_DWORD *)(v29 + 40),
                0,
                0);
          }
          else
          {
            sub_4C129C(v29, &v27);
            sub_4B865C(
              v43,
              v29,
              10001,
              *(unsigned __int8 *)(v29 + 44),
              *(_DWORD *)(v29 + 36),
              *(_DWORD *)(v29 + 40),
              0,
              v27);
          }
        }
      }
      ++v38;
    }
    else
    {
      if ( *(_BYTE *)(v43 + 570) == 0 )
      {
        v29 = *(_DWORD *)(v32 + 4);
        if ( *(_BYTE *)(v29 + 701) == 0 )
          sub_4B865C(v43, v29, 10022, 0, 0, 0, 0, 0);
      }
      Classes::TList::Delete(*(Classes::TList **)(v43 + 1032), v38);
      System::__linkproc__ FreeMem(v32, 8);
    }
  }
  __writefsdword(0, (unsigned int)ExceptionList);
  v19 = &savedregs;
  v18 = &loc_4BA0A9;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  if ( *(_BYTE *)(v43 + 570) == 0 )
  {
    v38 = 0;
    while ( *(_DWORD *)(*(_DWORD *)(v43 + 988) + 8) > v38 )
    {
      if ( *(_BYTE *)Classes::TList::Get(*(Classes::TList **)(v43 + 988), v38) != 0 )
      {
        if ( *(_BYTE *)Classes::TList::Get(*(Classes::TList **)(v43 + 988), v38) == 2 )
        {
          v33 = Classes::TList::Get(*(Classes::TList **)(v43 + 988), v38);
          v15 = *(_WORD *)(v33 + 28);
          v11 = *(_DWORD *)(v33 + 8);
          v10 = *(unsigned __int16 *)(v33 + 2);
          unknown_libname_41(*(unsigned __int16 *)(v33 + 4));
          sub_4B865C(v43, v43, 10110, v15, v11, v10, v9, v22[4]);
        }
        ++v38;
      }
      else
      {
        v33 = Classes::TList::Get(*(Classes::TList **)(v43 + 988), v38);
        sub_4B865C(
          v43,
          v43,
          10111,
          0,
          *(_DWORD *)(v33 + 8),
          *(unsigned __int16 *)(v33 + 2),
          *(unsigned __int16 *)(v33 + 4),
          0);
        Classes::TList::Delete(*(Classes::TList **)(v43 + 988), v38);
        System::__linkproc__ FreeMem(v33, 32);
      }
    }
    v38 = 0;
    while ( *(_DWORD *)(*(_DWORD *)(v43 + 992) + 8) > v38 )
    {
      v30 = (void *)Classes::TList::Get(*(Classes::TList **)(v43 + 992), v38);
      if ( *((_DWORD *)v30 + 1) != 0 )
      {
        if ( *((_DWORD *)v30 + 1) == 2 )
        {
          v16 = *((_WORD *)v30 + 10);
          v12 = v30;
          LOWORD(v5) = *((_WORD *)v30 + 12);
          HIWORD(v6) = HIWORD(v30);
          LOWORD(v6) = *((_WORD *)v30 + 6);
          v7 = unknown_libname_78(v6, v5);
          sub_4B865C(v43, v43, 10334, v16, (int)v12, v7, *((_DWORD *)v30 + 4), 0);
        }
        ++v38;
      }
      else
      {
        sub_4B865C(v43, v43, 10333, 0, (int)v30, *((_DWORD *)v30 + 3), *((_DWORD *)v30 + 4), 0);
        Classes::TList::Delete(*(Classes::TList **)(v43 + 992), v38);
      }
    }
  }
  __writefsdword(0, (unsigned int)ExceptionList);
LABEL_91:
  __writefsdword(0, v20[0]);
  v21 = (int *)&loc_4BA192;
  unknown_libname_35(v22, 7);
  unknown_libname_35(&v23, 5);
  unknown_libname_35(&v24, 2);
  return unknown_libname_34(&v27);
}


// ==================== [0x4BA25C] sub_4BA25C (size: 479) ====================
int __fastcall sub_4BA25C(int a1, int a2)
{
  unsigned int v2; // eax
  int v3; // edx
  __int16 v4; // cx
  bool v6; // [esp+3h] [ebp-25h]
  unsigned int v7; // [esp+8h] [ebp-20h]
  char v8; // [esp+Ch] [ebp-1Ch]
  int v10; // [esp+24h] [ebp-4h]

  v10 = a1;
  if ( *(_BYTE *)(a1 + 570) != 0 )
  {
    v6 = false;
    if ( a2 != 0 )
      v6 = *(_BYTE *)(a2 + 581) != 0;
    if ( v6 )
    {
      v8 = *(_BYTE *)(a1 + 571);
      v7 = *(unsigned __int16 *)(a1 + 568);
      v2 = v7;
      if ( v7 > 0xA1 )
      {
        v2 = v7 - 162;
        if ( v7 == 162 )
        {
          v8 = 11;
          LOWORD(v7) = 6;
        }
        else
        {
          v2 = v7 - 163;
          if ( v7 == 163 )
          {
            v8 = 11;
            LOWORD(v7) = 3;
          }
        }
      }
      else if ( v7 == 161 )
      {
        v8 = 10;
        LOWORD(v7) = 1;
      }
      else
      {
        v2 = v7 - 1;
        if ( *(_WORD *)(v10 + 568) != 0 )
        {
          if ( v7 == 1 )
          {
            v8 = 11;
            LOWORD(v7) = 9;
          }
          else
          {
            v2 = v7 - 160;
            if ( v7 == 160 )
            {
              v8 = 10;
              v2 = 0;
              LOWORD(v7) = 0;
            }
          }
        }
        else
        {
          v8 = 12;
          LOWORD(v7) = 5;
        }
      }
      LOBYTE(v2) = v8;
      v3 = v10;
      LOBYTE(v3) = *(_BYTE *)(v10 + 780);
      return sub_4546C8(v2, v3, (_WORD)v7);
    }
    else
    {
      v4 = *(_WORD *)(a1 + 568);
      LOBYTE(a2) = *(_BYTE *)(a1 + 780);
      LOBYTE(a1) = *(_BYTE *)(a1 + 571);
      return sub_4546C8(a1, a2, v4);
    }
  }
  else
  {
    if ( *(_WORD *)(a1 + 1060) != 0 )
      sub_4AC2F8(*off_4EA490, *(unsigned __int16 *)(a1 + 1060));
    if ( *(_WORD *)(v10 + 1084) != 0 )
      sub_4AC2F8(*off_4EA490, *(unsigned __int16 *)(v10 + 1084));
    return sub_454690(*(_BYTE *)(v10 + 45) + 2 * *(_BYTE *)(v10 + 46));
  }
}


// ==================== [0x4BA724] sub_4BA724 (size: 905) ====================
char __fastcall sub_4BA724(int a1, int a2)
{
  int v2; // ecx
  unsigned int v4[3]; // [esp-18h] [ebp-C4h] BYREF
  unsigned int v5[2]; // [esp-Ch] [ebp-B8h] BYREF
  int *v6; // [esp-4h] [ebp-B0h]
  _DWORD v7[2]; // [esp+Ch] [ebp-A0h] BYREF
  _DWORD v8[4]; // [esp+70h] [ebp-3Ch] BYREF
  int v9; // [esp+80h] [ebp-2Ch]
  int v10; // [esp+84h] [ebp-28h]
  int v11; // [esp+88h] [ebp-24h]
  char v12; // [esp+8Fh] [ebp-1Dh]
  _DWORD *v13; // [esp+90h] [ebp-1Ch]
  int v14; // [esp+94h] [ebp-18h]
  int v15; // [esp+98h] [ebp-14h] BYREF
  int v16; // [esp+9Ch] [ebp-10h]
  char v17; // [esp+A3h] [ebp-9h]
  int v18; // [esp+A4h] [ebp-8h]
  int v19; // [esp+A8h] [ebp-4h]
  int savedregs; // [esp+ACh] [ebp+0h] BYREF

  memset(v8, 0, 12);
  v7[1] = 0;
  v7[0] = 0;
  v18 = a2;
  v19 = a1;
  v6 = &savedregs;
  v5[1] = (unsigned int)&loc_4BAA9C;
  v5[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v5);
  v17 = 1;
  v4[2] = (unsigned int)&savedregs;
  v4[1] = (unsigned int)&loc_4BA9D1;
  v4[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v4);
  v12 = sub_4B57D8(&v15);
  v13 = nullptr;
  v10 = 0;
  if ( v12 != 0 && *(_DWORD *)(v15 + 4) != 0 && *(_DWORD *)(*(_DWORD *)(v15 + 4) + 8) - 1 >= 0 )
  {
    v9 = *(_DWORD *)(*(_DWORD *)(v15 + 4) + 8);
    v16 = 0;
    do
    {
      v14 = Classes::TList::Get(*(Classes::TList **)(v15 + 4), v16);
      if ( *(_BYTE *)v14 == 4 )
        v13 = *(_DWORD **)(v14 + 4);
      if ( *(_BYTE *)v14 == 3 && *(_DWORD *)(*(_DWORD *)(v14 + 4) + 48) != 0 )
        v10 = *(_DWORD *)(v14 + 4);
      ++v16;
      --v9;
    }
    while ( v9 != 0 );
  }
  if ( v10 != 0 && (unsigned __int8)unknown_libname_25(*(_DWORD *)(v10 + 48), v19, v2) != 0 )
    sub_4B865C(v19, *(_DWORD *)(v10 + 48), 10028, 0, *(_DWORD *)(v10 + 44), 0, 0, 0);
  if ( v17 != 0 && v13 != nullptr )
  {
    if ( *(_BYTE *)(v19 + 570) != 0 )
    {
      v17 = 0;
    }
    else if ( (unsigned __int8)sub_4B6B48(*(_DWORD *)(v19 + 760), *(_DWORD *)(v19 + 36), *(_DWORD *)(v19 + 40)) != 0
           && (*(_BYTE *)(v13[1] + 50) == 0 || sub_4A810C(1, *(_DWORD *)(v19 + 40)) != 0) )
    {
      if ( *off_4EA64C == *(_DWORD *)(v13[1] + 20) )
      {
        if ( (unsigned __int8)sub_4BAADC(v13[3]) == 0 )
          v17 = 0;
      }
      else
      {
        sub_4BA65C(v19);
        *(_BYTE *)(v19 + 790) = 1;
        v11 = v19;
        unknown_libname_36(v19 + 1504, *(_DWORD *)(v13[1] + 4));
        *(_DWORD *)(v11 + 1508) = v13[2];
        *(_DWORD *)(v11 + 1512) = v13[3];
        *(_BYTE *)(v11 + 1497) = 1;
        *(_DWORD *)(v11 + 1488) = *(_DWORD *)(v13[1] + 20);
        *(_BYTE *)(v11 + 1492) = 1;
        *(_BYTE *)(v11 + 1495) = 1;
      }
    }
  }
  else if ( v17 != 0 )
  {
    sub_4B8EBC(v19, v18, *(unsigned __int8 *)(v19 + 44), *(_DWORD *)(v19 + 36), *(_DWORD *)(v19 + 40), 0, 0);
  }
  __writefsdword(0, v4[0]);
  __writefsdword(0, v5[0]);
  v6 = (int *)&loc_4BAAA3;
  unknown_libname_35(v7, 2);
  unknown_libname_35(v8, 3);
  return v17;
}


// ==================== [0x4BAADC] sub_4BAADC (size: 1085) ====================
char __fastcall sub_4BAADC(int a1, int *a2, int a3, int a4)
{
  int v4; // eax
  int v5; // eax
  int v6; // eax
  int v7; // ecx
  int v8; // ecx
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-18h] [ebp-14Ch] BYREF
  void *v11; // [esp-14h] [ebp-148h]
  int *v12; // [esp-10h] [ebp-144h]
  unsigned int v13[6]; // [esp-Ch] [ebp-140h] BYREF
  _BYTE v14[256]; // [esp+Ch] [ebp-128h] BYREF
  int v15; // [esp+10Ch] [ebp-28h]
  int v16; // [esp+110h] [ebp-24h]
  _BYTE v17[4]; // [esp+114h] [ebp-20h] BYREF
  int v18; // [esp+118h] [ebp-1Ch]
  int v19; // [esp+11Ch] [ebp-18h]
  int v20; // [esp+120h] [ebp-14h]
  char v21; // [esp+127h] [ebp-Dh]
  int v22; // [esp+128h] [ebp-Ch]
  int *v23; // [esp+12Ch] [ebp-8h]
  int v24; // [esp+130h] [ebp-4h]
  int savedregs; // [esp+134h] [ebp+0h] BYREF

  v22 = a3;
  v23 = a2;
  v24 = a1;
  v21 = 0;
  v13[2] = (unsigned int)&savedregs;
  v13[1] = (unsigned int)&loc_4BAEF9;
  v13[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v13);
  if ( *(unsigned __int8 *)(a1 + 52) >= a2[6] )
  {
    if ( v23[15] != 0 )
      unknown_libname_25(v23[15], v24, a3);
    if ( (v23[16] < 0 || (v4 = sub_4C1490(v24, v23[16])) == v23[17])
      && (unsigned __int8)sub_4B57D8(v17) != 0
      && (*(int **)(*off_4EA080[0] + 8) != v23 || *(_BYTE *)(v24 + 570) != 0 || (unsigned __int8)sub_490300(v24) != 0) )
    {
      v16 = *(_DWORD *)(v24 + 760);
      v19 = *(_DWORD *)(v24 + 36);
      v18 = *(_DWORD *)(v24 + 40);
      sub_4BA65C(v24);
      v12 = &savedregs;
      v11 = &loc_4BAC1D;
      ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
      __writefsdword(0, (unsigned int)&ExceptionList);
      (*(void (__fastcall **)(_DWORD))(**(_DWORD **)(v24 + 984) + 64))(*(_DWORD *)(v24 + 984));
      __writefsdword(0, (unsigned int)ExceptionList);
      v12 = &savedregs;
      v11 = &loc_4BAC88;
      ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
      __writefsdword(0, (unsigned int)&ExceptionList);
      if ( *(_DWORD *)(*(_DWORD *)(v24 + 988) + 8) - 1 >= 0 )
      {
        v15 = *(_DWORD *)(*(_DWORD *)(v24 + 988) + 8);
        v20 = 0;
        do
        {
          v5 = Classes::TList::Get(*(Classes::TList **)(v24 + 988), v20);
          System::__linkproc__ FreeMem(v5, 32);
          ++v20;
          --v15;
        }
        while ( v15 != 0 );
      }
      __writefsdword(0, (unsigned int)ExceptionList);
      v12 = &savedregs;
      v11 = &loc_4BACC2;
      ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
      __writefsdword(0, (unsigned int)&ExceptionList);
      (*(void (__fastcall **)(_DWORD))(**(_DWORD **)(v24 + 988) + 8))(*(_DWORD *)(v24 + 988));
      __writefsdword(0, (unsigned int)ExceptionList);
      v12 = &savedregs;
      v11 = &loc_4BACFC;
      ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
      __writefsdword(0, (unsigned int)&ExceptionList);
      (*(void (__fastcall **)(_DWORD))(**(_DWORD **)(v24 + 992) + 8))(*(_DWORD *)(v24 + 992));
      __writefsdword(0, (unsigned int)ExceptionList);
      v12 = &savedregs;
      v11 = &loc_4BAD67;
      ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
      __writefsdword(0, (unsigned int)&ExceptionList);
      if ( *(_DWORD *)(*(_DWORD *)(v24 + 1032) + 8) - 1 >= 0 )
      {
        v15 = *(_DWORD *)(*(_DWORD *)(v24 + 1032) + 8);
        v20 = 0;
        do
        {
          v6 = Classes::TList::Get(*(Classes::TList **)(v24 + 1032), v20);
          System::__linkproc__ FreeMem(v6, 8);
          ++v20;
          --v15;
        }
        while ( v15 != 0 );
      }
      __writefsdword(0, (unsigned int)ExceptionList);
      v12 = &savedregs;
      v11 = &loc_4BADA1;
      ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
      __writefsdword(0, (unsigned int)&ExceptionList);
      (*(void (__fastcall **)(_DWORD))(**(_DWORD **)(v24 + 1032) + 8))(*(_DWORD *)(v24 + 1032));
      __writefsdword(0, (unsigned int)ExceptionList);
      sub_4B865C(v24, v24, 10117, 0, 0, 0, 0, 0);
      *(_DWORD *)(v24 + 760) = v23;
      System::__linkproc__ LStrToString(v14, v23[1], 255);
      LOBYTE(v7) = 16;
      System::__linkproc__ PStrNCpy(v24 + 4, v14, v7);
      *(_DWORD *)(v24 + 36) = v22;
      *(_DWORD *)(v24 + 40) = a4;
      sub_4B865C(v24, v24, 10118, 0, 0, 0, 0, v23[1]);
      if ( (unsigned __int8)sub_4BA5E4(v24) != 0 )
      {
        *(_DWORD *)(v24 + 920) = GetTickCount();
        *(_BYTE *)(v24 + 790) = 1;
        v21 = 1;
      }
      else
      {
        System::__linkproc__ LStrToString(v14, *(_DWORD *)(v16 + 4), 255);
        LOBYTE(v8) = 16;
        System::__linkproc__ PStrNCpy(v24 + 4, v14, v8);
        *(_DWORD *)(v24 + 760) = v16;
        *(_DWORD *)(v24 + 36) = v19;
        *(_DWORD *)(v24 + 40) = v18;
        sub_4B63E4(*(_DWORD *)(v24 + 760), *(_DWORD *)(v24 + 36), *(_DWORD *)(v24 + 40), 1, v24);
      }
      if ( *(_BYTE *)(*(_DWORD *)(v24 + 760) + 47) != 0
        && *(_BYTE *)(*(_DWORD *)(v24 + 760) + 47) != *(_BYTE *)(v16 + 47) )
      {
        sub_4BF0C4(v24);
      }
    }
  }
  __writefsdword(0, v13[0]);
  return v21;
}


// ==================== [0x4BB44C] sub_4BB44C (size: 446) ====================
int __fastcall sub_4BB44C(int a1, int a2)
{
  int v2; // edx
  int v3; // eax
  int v5; // [esp-24h] [ebp-50h]
  int v6; // [esp-20h] [ebp-4Ch]
  struct _EXCEPTION_REGISTRATION_RECORD *v7; // [esp-18h] [ebp-44h] BYREF
  void *v8; // [esp-14h] [ebp-40h]
  int *v9; // [esp-10h] [ebp-3Ch]
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-Ch] [ebp-38h] BYREF
  void *v11; // [esp-8h] [ebp-34h]
  int *v12; // [esp-4h] [ebp-30h]
  int v13; // [esp+Ch] [ebp-20h] BYREF
  bool v14; // [esp+13h] [ebp-19h]
  int v15; // [esp+14h] [ebp-18h]
  int v16; // [esp+18h] [ebp-14h]
  int v17; // [esp+1Ch] [ebp-10h]
  int v18; // [esp+20h] [ebp-Ch]
  int v19; // [esp+24h] [ebp-8h]
  int v20; // [esp+28h] [ebp-4h]
  int savedregs; // [esp+2Ch] [ebp+0h] BYREF

  v13 = 0;
  v19 = a2;
  v20 = a1;
  v12 = &savedregs;
  v11 = &loc_4BB5FC;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  v15 = 0;
  v14 = true;
  if ( *(_BYTE *)(a1 + 570) != 0 )
  {
    v17 = 3;
  }
  else
  {
    v17 = 2;
    v14 = sub_4BF0A0(v20) >= 2;
  }
  v9 = &savedregs;
  v8 = &loc_4BB5D2;
  v7 = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&v7);
  if ( *(_DWORD *)(*(_DWORD *)(v20 + 1036) + 8) - 1 >= 0 )
  {
    v18 = *(_DWORD *)(*(_DWORD *)(v20 + 1036) + 8) - 1;
    do
    {
      if ( (v14 | (System::__linkproc__ RandInt(3) == 0)) != 0 )
      {
        v6 = v19;
        v5 = v20;
        Classes::TList::Get(*(Classes::TList **)(v20 + 1036), v18);
        if ( (unsigned __int8)sub_4C5478(v5, v6, 1) != 0 )
        {
          v16 = Classes::TList::Get(*(Classes::TList **)(v20 + 1036), v18);
          if ( *(_BYTE *)(v20 + 570) == 0 )
          {
            if ( v15 == 0 )
            {
              LOBYTE(v2) = 1;
              v15 = unknown_libname_22(off_410028, v2);
            }
            sub_4AC1AC(*off_4EA490, *(unsigned __int16 *)(v16 + 4), &v13);
            (*(void (__fastcall **)(int, int, _DWORD, int *, void *, struct _EXCEPTION_REGISTRATION_RECORD *, int *, void *, struct _EXCEPTION_REGISTRATION_RECORD *))(*(_DWORD *)v15 + 56))(
              v15,
              v13,
              *(_DWORD *)v16,
              v12,
              v11,
              ExceptionList,
              v9,
              v8,
              v7);
          }
          v3 = Classes::TList::Get(*(Classes::TList **)(v20 + 1036), v18);
          System::__linkproc__ FreeMem(v3, 24);
          Classes::TList::Delete(*(Classes::TList **)(v20 + 1036), v18);
        }
      }
      --v18;
    }
    while ( v18 != -1 );
  }
  if ( v15 != 0 )
    sub_4B865C(v20, v20, 10148, 0, v15, 0, 0, 0);
  __writefsdword(0, (unsigned int)v7);
  __writefsdword(0, (unsigned int)ExceptionList);
  v12 = (int *)&loc_4BB603;
  return unknown_libname_34(&v13);
}


// ==================== [0x4BB6C8] sub_4BB6C8 (size: 867) ====================
int __fastcall sub_4BB6C8(int a1, int a2)
{
  int v2; // edx
  int v3; // ecx
  int v4; // ecx
  int v5; // eax
  int v6; // eax
  int v7; // ecx
  int v8; // edx
  unsigned int v10[3]; // [esp-18h] [ebp-90h] BYREF
  unsigned int v11[2]; // [esp-Ch] [ebp-84h] BYREF
  int *v12; // [esp-4h] [ebp-7Ch]
  _DWORD v13[7]; // [esp+Ch] [ebp-6Ch] BYREF
  _BYTE v14[24]; // [esp+28h] [ebp-50h] BYREF
  _BYTE v15[20]; // [esp+40h] [ebp-38h] BYREF
  int v16; // [esp+54h] [ebp-24h] BYREF
  int v17; // [esp+58h] [ebp-20h] BYREF
  int v18; // [esp+5Ch] [ebp-1Ch]
  int v19; // [esp+60h] [ebp-18h]
  int v20; // [esp+64h] [ebp-14h]
  int v21; // [esp+68h] [ebp-10h]
  int i; // [esp+6Ch] [ebp-Ch]
  int v23; // [esp+70h] [ebp-8h]
  int v24; // [esp+74h] [ebp-4h]
  int savedregs; // [esp+78h] [ebp+0h] BYREF

  memset(v13, 0, sizeof(v13));
  v17 = 0;
  v16 = 0;
  v18 = 0;
  v23 = a2;
  v24 = a1;
  v12 = &savedregs;
  v11[1] = (unsigned int)&loc_4BBA1D;
  v11[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v11);
  v20 = 0;
  v10[2] = (unsigned int)&savedregs;
  v10[1] = (unsigned int)&loc_4BB9E1;
  v10[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v10);
  if ( *(_BYTE *)(a1 + 570) == 0 )
  {
    for ( i = 0; i != 9; ++i )
    {
      v19 = sub_4AC2F8(*off_4EA490, *(unsigned __int16 *)(v24 + 24 * i + 1060));
      if ( v19 != 0 && (*(_BYTE *)(v19 + 20) & 8) != 0 )
      {
        if ( v20 == 0 )
        {
          LOBYTE(v2) = 1;
          v20 = unknown_libname_22(off_410028, v2);
        }
        (*(void (__fastcall **)(int, int, _DWORD))(*(_DWORD *)v20 + 56))(v20, v18, *(_DWORD *)(v24 + 24 * i + 1056));
        unknown_libname_16(v15, &dword_4BBA2C);
        LOBYTE(v3) = 19;
        System::__linkproc__ PStrNCat(v15, v24 + 4, v3);
        unknown_libname_16(v14, v15);
        LOBYTE(v4) = 20;
        System::__linkproc__ PStrNCat(v14, dword_4BBA30, v4);
        unknown_libname_41(v10[0]);
        Sysutils::IntToStr(*(_DWORD *)(v24 + 36));
        Sysutils::IntToStr(*(_DWORD *)(v24 + 40));
        unknown_libname_41(dword_4BBA3C);
        unknown_libname_41(dword_4BBA3C);
        Sysutils::IntToStr(*(_DWORD *)(v24 + 24 * i + 1056));
        v5 = v24;
        LOBYTE(v5) = *(_BYTE *)(v24 + 570) == 0;
        v6 = sub_454558(v5);
        Sysutils::IntToStr(v6);
        System::__linkproc__ LStrCatN(&v17, 14, v7, dword_4BBA3C, dword_4BBA48);
        sub_4E42F8(v17);
        *(_WORD *)(v24 + 24 * i + 1060) = 0;
      }
    }
  }
  if ( sub_4BF0A0(v24) <= 2 )
    v21 = 30;
  else
    v21 = 15;
  for ( i = 0; i != 9; ++i )
  {
    if ( System::__linkproc__ RandInt(v21) == 0 && (unsigned __int8)sub_4C5478(v24, v23, 1) != 0 )
    {
      v19 = sub_4AC2F8(*off_4EA490, *(unsigned __int16 *)(v24 + 24 * i + 1060));
      if ( v19 != 0 && (*(_BYTE *)(v19 + 20) & 0x10) == 0 )
      {
        if ( *(_BYTE *)(v24 + 570) == 0 )
        {
          if ( v20 == 0 )
          {
            LOBYTE(v8) = 1;
            v20 = unknown_libname_22(off_410028, v8);
          }
          sub_4AC1AC(*off_4EA490, *(unsigned __int16 *)(v24 + 24 * i + 1060), v13);
          (*(void (__fastcall **)(int, _DWORD, _DWORD))(*(_DWORD *)v20 + 56))(
            v20,
            v13[0],
            *(_DWORD *)(v24 + 24 * i + 1056));
        }
        *(_WORD *)(v24 + 24 * i + 1060) = 0;
      }
    }
  }
  if ( v20 != 0 )
    sub_4B865C(v24, v24, 10148, 0, v20, 0, 0, 0);
  __writefsdword(0, v10[0]);
  __writefsdword(0, v11[0]);
  v12 = (int *)&loc_4BBA24;
  unknown_libname_35(v13, 7);
  return unknown_libname_35(&v16, 3);
}


// ==================== [0x4BBA98] sub_4BBA98 (size: 2869) ====================
int __fastcall sub_4BBA98(int a1)
{
  int v1; // eax
  int v2; // eax
  int v3; // eax
  __int64 v4; // rax
  __int64 v5; // rax
  unsigned int v6; // eax
  int v7; // ecx
  int v8; // ecx
  int v9; // ecx
  int v10; // ecx
  int v11; // ecx
  int v12; // ecx
  int v13; // ecx
  int v14; // ecx
  int v15; // eax
  int v16; // eax
  int v17; // ecx
  __int64 v19; // [esp+0h] [ebp-F4h]
  char v20; // [esp+0h] [ebp-F4h]
  int v21; // [esp+0h] [ebp-F4h]
  int v22; // [esp+4h] [ebp-F0h]
  struct _EXCEPTION_REGISTRATION_RECORD *v23; // [esp+8h] [ebp-ECh] BYREF
  void *v24; // [esp+Ch] [ebp-E8h]
  int *v25; // [esp+10h] [ebp-E4h]
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp+14h] [ebp-E0h] BYREF
  void *v27; // [esp+18h] [ebp-DCh]
  int *v28; // [esp+1Ch] [ebp-D8h]
  int v29; // [esp+2Ch] [ebp-C8h] BYREF
  int v30; // [esp+30h] [ebp-C4h] BYREF
  _BYTE v31[24]; // [esp+40h] [ebp-B4h] BYREF
  int v32; // [esp+58h] [ebp-9Ch] BYREF
  int v33; // [esp+5Ch] [ebp-98h] BYREF
  _BYTE v34[16]; // [esp+60h] [ebp-94h] BYREF
  int v35[5]; // [esp+70h] [ebp-84h] BYREF
  int v36; // [esp+84h] [ebp-70h]
  _BYTE v37[32]; // [esp+88h] [ebp-6Ch] BYREF
  _BYTE v38[20]; // [esp+A8h] [ebp-4Ch] BYREF
  int v39[4]; // [esp+BCh] [ebp-38h] BYREF
  int v40; // [esp+CCh] [ebp-28h]
  int v41; // [esp+D0h] [ebp-24h]
  bool v42; // [esp+D6h] [ebp-1Eh]
  char v43; // [esp+D7h] [ebp-1Dh]
  int v44; // [esp+D8h] [ebp-1Ch]
  int v45; // [esp+DCh] [ebp-18h]
  void *v46; // [esp+E0h] [ebp-14h] BYREF
  bool v47; // [esp+E7h] [ebp-Dh]
  int v48; // [esp+E8h] [ebp-Ch]
  int v49; // [esp+ECh] [ebp-8h]
  int v50; // [esp+F0h] [ebp-4h]
  int savedregs; // [esp+F4h] [ebp+0h] BYREF

  v50 = a1;
  v28 = &savedregs;
  v27 = &loc_4BC5BF;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  if ( *(_BYTE *)(a1 + 696) == 0 )
  {
    *(_BYTE *)(v50 + 772) = 1;
    *(_DWORD *)(v50 + 776) = GetTickCount();
    sub_4BC87C(v50);
    if ( *(_DWORD *)(v50 + 648) != 0 )
    {
      *(_DWORD *)(v50 + 904) = 0;
      *(_DWORD *)(v50 + 896) = 0;
    }
    *(_DWORD *)(v50 + 184) = 0;
    *(_DWORD *)(v50 + 180) = 0;
    *(_DWORD *)(v50 + 188) = 0;
    v25 = &savedregs;
    v24 = &loc_4BBE36;
    v23 = NtCurrentTeb()->NtTib.ExceptionList;
    __writefsdword(0, (unsigned int)&v23);
    if ( *(_BYTE *)(v50 + 570) != 0 && *(_DWORD *)(v50 + 896) != 0 )
    {
      if ( *(_DWORD *)(v50 + 904) != 0 )
      {
        if ( *(_BYTE *)(*(_DWORD *)(v50 + 904) + 570) != 0 )
        {
          if ( *(_DWORD *)(*(_DWORD *)(v50 + 904) + 648) != 0 )
          {
            sub_4BE8BC(*(_DWORD *)(v50 + 904), *(unsigned __int8 *)(v50 + 52));
            v48 = sub_4BE590(
                    *(_DWORD *)(*(_DWORD *)(v50 + 904) + 648),
                    *(unsigned __int8 *)(v50 + 52),
                    *(_DWORD *)(v50 + 404),
                    v28,
                    v27,
                    ExceptionList,
                    v25,
                    v24,
                    v23);
            if ( *off_4EA4E4[0] == 0 )
              sub_4BE600(*(_DWORD *)(*(_DWORD *)(v50 + 904) + 648), v48);
          }
        }
        else
        {
          v48 = sub_4BE590(
                  *(_DWORD *)(v50 + 904),
                  *(unsigned __int8 *)(v50 + 52),
                  *(_DWORD *)(v50 + 404),
                  v28,
                  v27,
                  ExceptionList,
                  v25,
                  v24,
                  v23);
          if ( *off_4EA4E4[0] == 0 )
            sub_4BE600(*(_DWORD *)(v50 + 904), v48);
          if ( (unsigned __int8)sub_4B6E24(*(_DWORD *)(v50 + 760)) != 0 )
          {
            if ( *(_DWORD *)(*(_DWORD *)(v50 + 904) + 628) != 0 )
            {
              v1 = *(_DWORD *)(*(_DWORD *)(*(_DWORD *)(v50 + 904) + 628) + 632);
              v2 = (*(int (__fastcall **)(int))(*(_DWORD *)v1 + 20))(v1) - 1;
              if ( v2 >= 0 )
              {
                v40 = v2 + 1;
                v49 = 0;
                do
                {
                  v3 = *(_DWORD *)(*(_DWORD *)(*(_DWORD *)(v50 + 904) + 628) + 632);
                  v44 = (*(int (__fastcall **)(int, int))(*(_DWORD *)v3 + 24))(v3, v49);
                  if ( *(_BYTE *)(v44 + 772) == 0 && *(_DWORD *)(*(_DWORD *)(v50 + 904) + 760) == *(_DWORD *)(v44 + 760) )
                  {
                    v4 = *(_DWORD *)(*(_DWORD *)(v50 + 904) + 36) - *(_DWORD *)(v44 + 36);
                    if ( (int)((HIDWORD(v4) ^ v4) - HIDWORD(v4)) <= 12 )
                    {
                      v5 = *(_DWORD *)(*(_DWORD *)(v50 + 904) + 40) - *(_DWORD *)(v44 + 40);
                      if ( (int)((HIDWORD(v5) ^ v5) - HIDWORD(v5)) <= 12 )
                      {
                        v6 = *(_DWORD *)(v50 + 904);
                        v42 = v6 != v44;
                        LOBYTE(v6) = v6 != v44;
                        v19 = v6;
                        unknown_libname_41(v6);
                        v41 = sub_4B6E4C(*(_DWORD *)(v50 + 760), v44, v39[2], SHIDWORD(v19), v19);
                        if ( v41 != 0 )
                          unknown_libname_25(v41, v44, v7);
                      }
                    }
                  }
                  ++v49;
                  --v40;
                }
                while ( v40 != 0 );
              }
            }
            else
            {
              unknown_libname_41(0);
              v41 = sub_4B6E4C(*(_DWORD *)(v50 + 760), *(_DWORD *)(v50 + 904), v39[1], 0, v20);
              if ( v41 != 0 )
                unknown_libname_25(v41, *(_DWORD *)(v50 + 904), v8);
            }
          }
        }
      }
      else if ( *(_BYTE *)(*(_DWORD *)(v50 + 896) + 570) == 0 )
      {
        v48 = sub_4BE590(
                *(_DWORD *)(v50 + 896),
                *(unsigned __int8 *)(v50 + 52),
                *(_DWORD *)(v50 + 404),
                v28,
                v27,
                ExceptionList,
                v25,
                v24,
                v23);
        if ( *off_4EA4E4[0] == 0 )
          sub_4BE600(*(_DWORD *)(v50 + 896), v48);
      }
    }
    *(_DWORD *)(v50 + 648) = 0;
    __writefsdword(0, (unsigned int)v23);
    v25 = &savedregs;
    v24 = &loc_4BC0AD;
    v23 = NtCurrentTeb()->NtTib.ExceptionList;
    __writefsdword(0, (unsigned int)&v23);
    v43 = 0;
    if ( *off_4EA4E4[0] == 0
      && *(_BYTE *)(*(_DWORD *)(v50 + 760) + 46) == 0
      && *(_BYTE *)(*(_DWORD *)(v50 + 760) + 47) == 0
      && *(_BYTE *)(v50 + 570) == 0
      && *(_DWORD *)(v50 + 896) != 0
      && sub_4BF0A0(v50) < 2 )
    {
      if ( *(_BYTE *)(*(_DWORD *)(v50 + 896) + 570) == 0 )
        v43 = 1;
      if ( *(_DWORD *)(*(_DWORD *)(v50 + 896) + 648) != 0
        && *(_BYTE *)(*(_DWORD *)(*(_DWORD *)(v50 + 896) + 648) + 570) == 0 )
      {
        *(_DWORD *)(v50 + 896) = *(_DWORD *)(*(_DWORD *)(v50 + 896) + 648);
        v43 = 1;
      }
    }
    if ( v43 != 0 && *(_DWORD *)(v50 + 896) != 0 )
    {
      v47 = false;
      if ( *(_DWORD *)(v50 + 800) != 0 && *(_DWORD *)(*(_DWORD *)(v50 + 896) + 800) != 0 )
        v47 = sub_4BF380(v50, v50, *(_DWORD *)(v50 + 896), v25, v24, v23) == 2;
      if ( *(_BYTE *)(*off_4EA080[0] + 65) != 0
        && (*(_BYTE *)(v50 + 1000) != 0 || (unsigned __int8)sub_4910F4(*(_DWORD *)(v50 + 40)) != 0) )
      {
        v47 = true;
      }
      if ( !v47 )
      {
        if ( (unsigned __int8)sub_4BC8D8(*(_DWORD *)(v50 + 896), v50) != 0 )
        {
          sub_4BB124(*(_DWORD *)(v50 + 896), aProtectedByLaw, 1);
        }
        else
        {
          sub_4BF4D4(*(_DWORD *)(v50 + 896), 100);
          sub_4BB124(*(_DWORD *)(v50 + 896), &str_You_murdered[1], 0);
          unknown_libname_16(v38, dword_4BC60C);
          LOBYTE(v9) = 19;
          System::__linkproc__ PStrNCat(v38, *(_DWORD *)(v50 + 896) + 21, v9);
          unknown_libname_16(v37, v38);
          LOBYTE(v10) = 28;
          System::__linkproc__ PStrNCat(v37, aKilledU, v10);
          unknown_libname_41(v23);
          sub_4BB124(v50, v39[0], 0);
          sub_4BF580(-500.0);
          if ( sub_4BF0A0(v50) < 1 && System::__linkproc__ RandInt(5) == 0 )
            sub_4C1198(*(_DWORD *)(v50 + 896));
        }
      }
    }
    __writefsdword(0, (unsigned int)v23);
    v25 = &savedregs;
    v24 = &loc_4BC558;
    v23 = NtCurrentTeb()->NtTib.ExceptionList;
    __writefsdword(0, (unsigned int)&v23);
    if ( *(_BYTE *)(*(_DWORD *)(v50 + 760) + 46) == 0
      && *(_BYTE *)(*(_DWORD *)(v50 + 760) + 47) == 0
      && *(_BYTE *)(v50 + 699) == 0 )
    {
      v45 = *(_DWORD *)(v50 + 904);
      if ( *(_DWORD *)(v50 + 904) != 0 && *(_DWORD *)(*(_DWORD *)(v50 + 904) + 648) != 0 )
        v45 = *(_DWORD *)(*(_DWORD *)(v50 + 904) + 648);
      if ( *(_BYTE *)(v50 + 570) != 0 )
      {
        sub_4BB6C8(v50, v45);
        if ( *(_DWORD *)(v50 + 648) == 0 && *(_BYTE *)(v50 + 700) == 0 )
          sub_4BB44C(v50, v45);
        if ( *(_BYTE *)(v50 + 570) >= 0x32u && *(_DWORD *)(v50 + 648) == 0 && *(_BYTE *)(v50 + 700) == 0 )
          sub_4BB63C(v50, v45);
      }
      else
      {
        if ( v45 == 0 || *(_BYTE *)(v45 + 570) != 0 )
          sub_4BB6C8(v50, 0);
        sub_4BB44C(v50, 0);
        v36 = -5 * *(unsigned __int8 *)(v50 + 52);
        sub_4BF580((double)v36);
      }
    }
    if ( *(_BYTE *)(*(_DWORD *)(v50 + 760) + 47) != 0 )
    {
      ++*(_DWORD *)(v50 + 192);
      if ( *(_DWORD *)(v50 + 800) != 0 )
      {
        unknown_libname_41(v23);
        Iniohandlerstack::TInIOHandlerStack::WriteDirect(*(_DWORD *)(v50 + 800), v35[4]);
      }
      if ( *(_DWORD *)(v50 + 896) != 0 && *(_DWORD *)(*(_DWORD *)(v50 + 896) + 800) != 0 && *(_DWORD *)(v50 + 800) != 0 )
      {
        unknown_libname_41(v23);
        sub_499DE4(*(_DWORD *)(*(_DWORD *)(v50 + 896) + 800), v35[3], 100);
        Sysutils::IntToStr(*(_DWORD *)(*(_DWORD *)(*(_DWORD *)(v50 + 896) + 800) + 24));
        Sysutils::IntToStr(*(_DWORD *)(*(_DWORD *)(v50 + 800) + 24));
        System::__linkproc__ LStrCatN(&v46, 7, v11, &str___0[1], v35[1]);
        v22 = *(_DWORD *)(v50 + 36);
        v21 = *(_DWORD *)(v50 + 40);
        System::__linkproc__ LStrCat3((int)v35, &str____7[1], v46);
        sub_4AEA00(v35[0], 10000, v21, v22);
      }
    }
    if ( *(_BYTE *)(v50 + 570) == 0 )
    {
      if ( *(_DWORD *)(v50 + 896) != 0 )
      {
        if ( *(_BYTE *)(*(_DWORD *)(v50 + 896) + 570) != 0 )
        {
          unknown_libname_16(v34, dword_4BC690);
          LOBYTE(v12) = 15;
          System::__linkproc__ PStrNCat(v34, *(_DWORD *)(v50 + 896) + 21, v12);
        }
        unknown_libname_41(v23);
      }
      else
      {
        unknown_libname_37(&v46, asc_4BC69C);
      }
      unknown_libname_16(v38, &dword_4BC6A4);
      LOBYTE(v13) = 19;
      System::__linkproc__ PStrNCat(v38, v50 + 4, v13);
      unknown_libname_16(v31, v38);
      LOBYTE(v14) = 20;
      System::__linkproc__ PStrNCat(v31, dword_4BC6A8, v14);
      unknown_libname_41(v23);
      Sysutils::IntToStr(*(_DWORD *)(v50 + 36));
      Sysutils::IntToStr(*(_DWORD *)(v50 + 40));
      unknown_libname_41(dword_4BC6B4);
      v15 = sub_4BBA78(&savedregs);
      unknown_libname_38(&v30, v15);
      v16 = sub_4BBA78(&savedregs);
      unknown_libname_38(&v29, v16);
      System::__linkproc__ LStrCatN(&v33, 15, v17, &str_1__1[1], v46);
      sub_4E42F8(v33);
    }
    sub_4B8EBC(v50, 10021, *(unsigned __int8 *)(v50 + 44), *(_DWORD *)(v50 + 36), *(_DWORD *)(v50 + 40), 1, 0);
    __writefsdword(0, (unsigned int)v23);
  }
  __writefsdword(0, (unsigned int)ExceptionList);
  v28 = (int *)&loc_4BC5C6;
  unknown_libname_35(&v29, 5);
  unknown_libname_35(&v32, 2);
  unknown_libname_35(v35, 5);
  unknown_libname_35(v39, 3);
  return unknown_libname_34(&v46);
}


// ==================== [0x4BCD1C] sub_4BCD1C (size: 935) ====================
int __fastcall sub_4BCD1C(int a1, int a2, int a3, int a4, int a5)
{
  int v5; // eax
  int v6; // eax
  int v7; // ecx
  int v9; // [esp-1Ch] [ebp-15Ch]
  unsigned int v10[2]; // [esp-Ch] [ebp-14Ch] BYREF
  int *v11; // [esp-4h] [ebp-144h]
  int v12; // [esp+4h] [ebp-13Ch] BYREF
  _BYTE v13[268]; // [esp+8h] [ebp-138h] BYREF
  int v14; // [esp+114h] [ebp-2Ch]
  int v15; // [esp+118h] [ebp-28h]
  char v16; // [esp+11Fh] [ebp-21h]
  int v17; // [esp+120h] [ebp-20h]
  int v18; // [esp+124h] [ebp-1Ch]
  int v19; // [esp+128h] [ebp-18h]
  int v20; // [esp+12Ch] [ebp-14h]
  int v21; // [esp+130h] [ebp-10h]
  int v22; // [esp+134h] [ebp-Ch]
  int v23; // [esp+138h] [ebp-8h] BYREF
  int v24; // [esp+13Ch] [ebp-4h]
  int savedregs; // [esp+140h] [ebp+0h] BYREF

  v12 = 0;
  v22 = a3;
  v23 = a2;
  v24 = a1;
  unknown_libname_44(a2);
  v11 = &savedregs;
  v10[1] = (unsigned int)&loc_4BD0B5;
  v10[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v10);
  v18 = sub_4B7350(*off_4EA22C, v23);
  if ( v18 != 0 )
  {
    if ( *off_4EA64C == *(_DWORD *)(v18 + 20) )
    {
      v17 = *(_DWORD *)(v24 + 760);
      v20 = *(_DWORD *)(v24 + 36);
      v19 = *(_DWORD *)(v24 + 40);
      v16 = 0;
      sub_4B6710(*(_DWORD *)(v24 + 760), *(_DWORD *)(v24 + 36), *(_DWORD *)(v24 + 40), 1, v24);
      (*(void (__fastcall **)(_DWORD))(**(_DWORD **)(v24 + 984) + 64))(*(_DWORD *)(v24 + 984));
      if ( *(_DWORD *)(*(_DWORD *)(v24 + 988) + 8) - 1 >= 0 )
      {
        v14 = *(_DWORD *)(*(_DWORD *)(v24 + 988) + 8);
        v21 = 0;
        do
        {
          v5 = Classes::TList::Get(*(Classes::TList **)(v24 + 988), v21);
          System::__linkproc__ FreeMem(v5, 32);
          ++v21;
          --v14;
        }
        while ( v14 != 0 );
      }
      (*(void (__fastcall **)(_DWORD))(**(_DWORD **)(v24 + 988) + 8))(*(_DWORD *)(v24 + 988));
      if ( *(_DWORD *)(*(_DWORD *)(v24 + 1032) + 8) - 1 >= 0 )
      {
        v14 = *(_DWORD *)(*(_DWORD *)(v24 + 1032) + 8);
        v21 = 0;
        do
        {
          v6 = Classes::TList::Get(*(Classes::TList **)(v24 + 1032), v21);
          System::__linkproc__ FreeMem(v6, 8);
          ++v21;
          --v14;
        }
        while ( v14 != 0 );
      }
      (*(void (__fastcall **)(_DWORD))(**(_DWORD **)(v24 + 1032) + 8))(*(_DWORD *)(v24 + 1032));
      *(_DWORD *)(v24 + 760) = v18;
      System::__linkproc__ LStrToString(v13, *(_DWORD *)(v18 + 4), 255);
      LOBYTE(v7) = 16;
      System::__linkproc__ PStrNCpy(v24 + 4, v13, v7);
      *(_DWORD *)(v24 + 36) = v22;
      *(_DWORD *)(v24 + 40) = a4;
      if ( (unsigned __int8)sub_4BCC2C(&savedregs) != 0 )
      {
        sub_4B63E4(*(_DWORD *)(v24 + 760), *(_DWORD *)(v24 + 36), *(_DWORD *)(v24 + 40), 1, v24);
        sub_4B865C(v24, v24, 10117, 0, 0, 0, 0, 0);
        unknown_libname_41(0);
        sub_4B865C(v24, v24, 10118, 0, 0, 0, v9, v12);
        if ( a5 == 1 )
          sub_4B8EBC(v24, 10336, *(unsigned __int8 *)(v24 + 44), *(_DWORD *)(v24 + 36), *(_DWORD *)(v24 + 40), 0, 0);
        else
          sub_4B8EBC(v24, 10331, *(unsigned __int8 *)(v24 + 44), *(_DWORD *)(v24 + 36), *(_DWORD *)(v24 + 40), 0, 0);
        *(_DWORD *)(v24 + 920) = GetTickCount();
        *(_BYTE *)(v24 + 790) = 1;
        v16 = 1;
      }
      if ( v16 == 0 )
      {
        *(_DWORD *)(v24 + 760) = v17;
        *(_DWORD *)(v24 + 36) = v20;
        *(_DWORD *)(v24 + 40) = v19;
        sub_4B63E4(*(_DWORD *)(v24 + 760), *(_DWORD *)(v24 + 36), *(_DWORD *)(v24 + 40), 1, v24);
      }
    }
    else if ( (unsigned __int8)sub_4BCC2C(&savedregs) != 0 )
    {
      if ( *(_BYTE *)(v24 + 570) != 0 )
      {
        sub_4BA6A8(v24);
      }
      else
      {
        sub_4BA65C(v24);
        *(_BYTE *)(v24 + 790) = 1;
        v15 = v24;
        unknown_libname_36(v24 + 1504, *(_DWORD *)(v18 + 4));
        *(_DWORD *)(v15 + 1508) = v22;
        *(_DWORD *)(v15 + 1512) = a4;
        *(_BYTE *)(v15 + 1497) = 1;
        *(_DWORD *)(v15 + 1488) = *(_DWORD *)(v18 + 20);
        *(_BYTE *)(v15 + 1492) = 1;
        *(_BYTE *)(v15 + 1495) = 1;
      }
    }
  }
  __writefsdword(0, v10[0]);
  v11 = (int *)&loc_4BD0BC;
  unknown_libname_34(&v12);
  return unknown_libname_34(&v23);
}


// ==================== [0x4BD1BC] sub_4BD1BC (size: 681) ====================
bool __fastcall sub_4BD1BC(int a1, int a2)
{
  int v2; // eax
  int v3; // eax
  int v5; // [esp-10h] [ebp-40h]
  int v6; // [esp-10h] [ebp-40h]
  int v7; // [esp-10h] [ebp-40h]
  unsigned int v8[2]; // [esp-Ch] [ebp-3Ch] BYREF
  int *v9; // [esp-4h] [ebp-34h]
  _DWORD v10[7]; // [esp+0h] [ebp-30h] BYREF
  bool v11; // [esp+27h] [ebp-9h]
  int v12; // [esp+28h] [ebp-8h]
  int v13; // [esp+2Ch] [ebp-4h]
  int savedregs; // [esp+30h] [ebp+0h] BYREF

  v12 = a2;
  v13 = a1;
  v9 = &savedregs;
  v8[1] = (unsigned int)&loc_4BD457;
  v8[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v8);
  v11 = false;
  switch ( a2 )
  {
    case 1:
      goto LABEL_2;
    case 2:
      if ( *(_BYTE *)(*(_DWORD *)(v13 + 760) + 52) == 0 )
      {
LABEL_2:
        sub_4B8EBC(v13, 10330, 0, 0, 0, 0, 0);
        unknown_libname_41(0);
        sub_4BD0C4(v5);
        v11 = true;
      }
      break;
    case 3:
      sub_4B8EBC(v13, 10330, 0, 0, 0, 0, 0);
      if ( sub_4BF0A0(v13) >= 2 )
      {
        Sysutils::IntToStr(33);
        v6 = v10[3];
        Sysutils::IntToStr(33);
      }
      else
      {
        Sysutils::IntToStr(*(_DWORD *)(v13 + 144));
        v6 = v10[6];
        Sysutils::IntToStr(*(_DWORD *)(v13 + 140));
        unknown_libname_41(v10[5]);
      }
      sub_4BD0C4(v6);
      v11 = true;
      break;
    case 4:
      v11 = (unsigned __int8)sub_4BD4A0(v13) != 0;
      break;
    case 5:
      if ( *(_DWORD *)(v13 + 800) != 0 )
      {
        if ( *(_BYTE *)(v13 + 1000) != 0 )
        {
          sub_4BB124(v13, aCannotUseHere, 0);
        }
        else
        {
          if ( (unsigned __int8)sub_490400(*off_4EA080[0], *(_DWORD *)(v13 + 800)) != 0 )
          {
            v2 = sub_4902D8(*off_4EA080[0]);
            Sysutils::IntToStr(v2);
            v7 = v10[1];
            v3 = sub_4902B0(*off_4EA080[0]);
            Sysutils::IntToStr(v3);
            sub_4BD0C4(v7);
          }
          else
          {
            sub_4BB124(v13, &str_Nowt[1], 0);
          }
          v11 = true;
        }
      }
      break;
    case 9:
      v11 = (unsigned __int8)sub_4BD69C(v13) != 0;
      break;
    case 10:
      v11 = (unsigned __int8)sub_4BD768(v13) != 0;
      break;
    case 11:
      v11 = (unsigned __int8)sub_4BD7F8(v13) != 0;
      break;
    default:
      break;
  }
  __writefsdword(0, v8[0]);
  v9 = (int *)&loc_4BD45E;
  unknown_libname_35(v10, 9);
  return v11;
}


// ==================== [0x4BD4A0] sub_4BD4A0 (size: 461) ====================
char __fastcall sub_4BD4A0(int a1)
{
  int v1; // eax
  __int64 v2; // rax
  int v3; // edx
  int v4; // ecx
  int v6; // [esp+4h] [ebp-14h]
  char v7; // [esp+Bh] [ebp-Dh]
  int v8; // [esp+Ch] [ebp-Ch]
  char v9; // [esp+13h] [ebp-5h]

  v9 = 0;
  if ( *(_WORD *)(a1 + 1084) != 0 )
  {
    v8 = 0;
    v6 = sub_4AC2F8(*off_4EA490, *(unsigned __int16 *)(a1 + 1084));
    if ( v6 != 0 )
    {
      HIWORD(v1) = HIWORD(v6);
      LOWORD(v1) = *(_WORD *)(v6 + 30);
      v2 = (unsigned __int8)sub_407B28(v1) - *(unsigned __int8 *)(v6 + 30);
      v8 = (int)((HIDWORD(v2) ^ v2) - HIDWORD(v2)) / 5;
    }
    if ( System::__linkproc__ RandInt(20) == 1 )
    {
      sub_4C1198(a1);
    }
    else
    {
      v7 = 0;
      if ( *(_BYTE *)(a1 + 1094) != 0 )
      {
        --*(_BYTE *)(a1 + 1094);
        sub_4BB124(a1, aWeaponGainsLuc, 1);
        v7 = 1;
      }
      else if ( *(_BYTE *)(a1 + 1093) == 0
             || *(_BYTE *)(a1 + 1093) < 3u && System::__linkproc__ RandInt(v8 + 6) == 1
             || *(_BYTE *)(a1 + 1093) < 7u && System::__linkproc__ RandInt(5 * v8 + 30) == 1 )
      {
        ++*(_BYTE *)(a1 + 1093);
        sub_4BB124(a1, aWeaponGainsLuc, 1);
        v7 = 1;
      }
      if ( *(_BYTE *)(a1 + 570) == 0 )
      {
        unknown_libname_25(a1, v3, v4);
        sub_4B865C(a1, a1, 10051, 0, 0, 0, 0, 0);
        sub_4B865C(a1, a1, 10302, 0, 0, 0, 0, 0);
      }
      if ( v7 == 0 )
        sub_4BB124(a1, &str_Nowt_0[1], 1);
    }
    return 1;
  }
  return v9;
}


// ==================== [0x4BD7F8] sub_4BD7F8 (size: 683) ====================
char __fastcall sub_4BD7F8(int a1)
{
  int v1; // eax
  bool v2; // cf
  int v3; // eax
  unsigned int v4; // eax
  unsigned int v5; // eax
  unsigned int v6; // eax
  unsigned int v7; // eax
  unsigned int v8; // eax
  int v10; // [esp+0h] [ebp-10h]
  int v11; // [esp+4h] [ebp-Ch]

  v11 = 0;
  v10 = 0;
  v1 = System::__linkproc__ RandInt(30000);
  if ( v1 >= 16150 )
  {
    v6 = v1 - 16150;
    v2 = v6 < 0x14;
    v7 = v6 - 20;
    if ( v2 )
    {
      if ( *off_4EA434[0] < *off_4EA664 )
      {
        v11 = 100000;
        v10 = 3;
        ++*off_4EA454;
      }
    }
    else
    {
      v2 = v7 < 0xA;
      v8 = v7 - 10;
      if ( v2 )
      {
        if ( *off_4EA434[0] < *off_4EA664 )
        {
          v11 = 200000;
          v10 = 2;
          ++*off_4EA6DC;
        }
      }
      else if ( v8 == 1820 && *off_4EA434[0] < *off_4EA664 )
      {
        v11 = 1000000;
        v10 = 1;
        ++*off_4EA66C[0];
      }
    }
  }
  else
  {
    v2 = (unsigned int)v1 < 0x1388;
    v3 = v1 - 5000;
    if ( v2 )
    {
      if ( *off_4EA434[0] < *off_4EA664 )
      {
        v11 = 500;
        v10 = 6;
        ++*off_4EA504[0];
      }
    }
    else
    {
      v4 = v3 - 9000;
      v2 = v4 < 0x7D0;
      v5 = v4 - 2000;
      if ( v2 )
      {
        if ( *off_4EA434[0] < *off_4EA664 )
        {
          v11 = 1000;
          v10 = 5;
          ++*off_4EA588;
        }
      }
      else if ( v5 < 0x96 && *off_4EA434[0] < *off_4EA664 )
      {
        v11 = 10000;
        v10 = 4;
        ++*off_4EA25C[0];
      }
    }
  }
  if ( v11 <= 0 )
  {
    *off_4EA664 += 500;
    sub_4BB124(a1, &str_Won_nothing___[1], 0);
  }
  else
  {
    *off_4EA434[0] += v11;
    switch ( v10 )
    {
      case 1:
        sub_4BB124(a1, &str_You_Won_1ST_Pri[1], 1);
        break;
      case 2:
        sub_4BB124(a1, &str_You_Won_2nd_Pri[1], 1);
        break;
      case 3:
        sub_4BB124(a1, &str_You_Won_3rd_Pri[1], 1);
        break;
      case 4:
        sub_4BB124(a1, &str_You_Won_4th_Pri[1], 1);
        break;
      case 5:
        sub_4BB124(a1, &str_You_Won_5th_Pri[1], 1);
        break;
      case 6:
        sub_4BB124(a1, &str_You_Won_6th_Pri[1], 1);
        break;
      default:
        break;
    }
    if ( (unsigned __int8)sub_4BF64C(a1, v11) != 0 )
      sub_4C49F4(a1);
    else
      sub_4C5794(0, 0);
  }
  return 1;
}


// ==================== [0x4BDEB8] sub_4BDEB8 (size: 1297) ====================
int __fastcall sub_4BDEB8(int a1, int a2)
{
  int v2; // ecx
  int v3; // ecx
  int v4; // ecx
  int v5; // eax
  int v6; // eax
  int v7; // ecx
  __int64 v8; // rax
  int v9; // edx
  int v10; // ecx
  int v11; // ecx
  int v12; // eax
  int v13; // eax
  int v14; // ecx
  __int64 v15; // rax
  bool v16; // zf
  unsigned int v18[2]; // [esp+8h] [ebp-98h] BYREF
  int *v19; // [esp+10h] [ebp-90h]
  int v20; // [esp+18h] [ebp-88h] BYREF
  int v21; // [esp+20h] [ebp-80h] BYREF
  int v22; // [esp+34h] [ebp-6Ch] BYREF
  int v23; // [esp+40h] [ebp-60h] BYREF
  _BYTE v24[20]; // [esp+50h] [ebp-50h] BYREF
  _BYTE v25[20]; // [esp+64h] [ebp-3Ch] BYREF
  int v26; // [esp+78h] [ebp-28h] BYREF
  int v27; // [esp+7Ch] [ebp-24h] BYREF
  int v28; // [esp+80h] [ebp-20h]
  char v29; // [esp+87h] [ebp-19h]
  int v30; // [esp+88h] [ebp-18h]
  int v31; // [esp+8Ch] [ebp-14h]
  int v32; // [esp+90h] [ebp-10h]
  int i; // [esp+94h] [ebp-Ch]
  int v34; // [esp+98h] [ebp-8h]
  int v35; // [esp+9Ch] [ebp-4h]
  int savedregs; // [esp+A0h] [ebp+0h] BYREF

  v34 = a2;
  v35 = a1;
  v19 = &savedregs;
  v18[1] = (unsigned int)&loc_4BE3BD;
  v18[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v18);
  if ( a2 > 0 )
  {
    v32 = System::__linkproc__ RandInt(10) + 5;
    if ( *(_WORD *)(v35 + 98) != 0 )
    {
      v32 = System::__linkproc__ ROUND((double)v32 * *(double *)&tbyte_4BE3CC);
      v34 = System::__linkproc__ ROUND((double)v34 * *(double *)&tbyte_4BE3CC);
    }
    v29 = 0;
    if ( *(_WORD *)(v35 + 1060) != 0 )
    {
      v31 = *(unsigned __int16 *)(v35 + 1062);
      v30 = System::__linkproc__ ROUND((double)v31 / flt_4BE3D8);
      v31 -= v32;
      if ( v31 > 0 )
      {
        *(_WORD *)(v35 + 1062) = v31;
      }
      else
      {
        if ( *(_BYTE *)(v35 + 570) == 0 )
        {
          v28 = v35;
          sub_4D0BDC(v35, v35 + 1056);
          unknown_libname_16(v25, &dword_4BE3DC);
          LOBYTE(v3) = 18;
          System::__linkproc__ PStrNCat(v25, v35 + 4, v3);
          unknown_libname_16(v24, v25);
          LOBYTE(v4) = 19;
          System::__linkproc__ PStrNCat(v24, dword_4BE3E0, v4);
          unknown_libname_41(v18[0]);
          Sysutils::IntToStr(*(_DWORD *)(v35 + 36));
          Sysutils::IntToStr(*(_DWORD *)(v35 + 40));
          unknown_libname_41(dword_4BE3EC);
          sub_4AC1AC(*off_4EA490, *(unsigned __int16 *)(v35 + 1060), &v23);
          Sysutils::IntToStr(*(_DWORD *)(v35 + 1056));
          v5 = v35;
          LOBYTE(v5) = *(_BYTE *)(v35 + 570) == 0;
          v6 = sub_454558(v5);
          Sysutils::IntToStr(v6);
          System::__linkproc__ LStrCatN(&v27, 14, v7, dword_4BE3EC, &dword_4BE3F8);
          sub_4E42F8(v27);
          *(_WORD *)(v35 + 1060) = 0;
          sub_4BA58C(v28);
        }
        *(_WORD *)(v35 + 1062) = 0;
        *(_WORD *)(v35 + 1060) = 0;
        v29 = 1;
      }
      LODWORD(v8) = System::__linkproc__ ROUND((double)v31 / flt_4BE3D8);
      if ( v30 != v8 )
        sub_4B865C(v35, v35, 10125, 0, v31, *(unsigned __int16 *)(v35 + 1064), 0, 0);
    }
    for ( i = 1; i != 9; ++i )
    {
      v9 = v35;
      if ( *(_WORD *)(v35 + 24 * i + 1060) != 0 && System::__linkproc__ RandInt(8) == 0 )
      {
        v31 = *(unsigned __int16 *)(v35 + 24 * i + 1062);
        v30 = System::__linkproc__ ROUND((double)v31 / flt_4BE3D8);
        v31 -= v32;
        if ( v31 > 0 )
        {
          *(_WORD *)(v35 + 24 * i + 1062) = v31;
        }
        else
        {
          if ( *(_BYTE *)(v35 + 570) == 0 )
          {
            v28 = v35;
            sub_4D0BDC(v35, v35 + 24 * i + 1056);
            unknown_libname_16(v25, &dword_4BE3DC);
            LOBYTE(v10) = 18;
            System::__linkproc__ PStrNCat(v25, v35 + 4, v10);
            unknown_libname_16(v24, v25);
            LOBYTE(v11) = 19;
            System::__linkproc__ PStrNCat(v24, dword_4BE3E0, v11);
            unknown_libname_41(v18[0]);
            Sysutils::IntToStr(*(_DWORD *)(v35 + 36));
            Sysutils::IntToStr(*(_DWORD *)(v35 + 40));
            unknown_libname_41(dword_4BE3EC);
            sub_4AC1AC(*off_4EA490, *(unsigned __int16 *)(v35 + 24 * i + 1060), &v21);
            Sysutils::IntToStr(*(_DWORD *)(v35 + 24 * i + 1056));
            v12 = v35;
            LOBYTE(v12) = *(_BYTE *)(v35 + 570) == 0;
            v13 = sub_454558(v12);
            Sysutils::IntToStr(v13);
            System::__linkproc__ LStrCatN(&v22, 14, v14, dword_4BE3EC, &dword_4BE3F8);
            sub_4E42F8(v22);
            *(_WORD *)(v35 + 24 * i + 1060) = 0;
            sub_4BA58C(v28);
          }
          *(_WORD *)(v35 + 24 * i + 1062) = 0;
          *(_WORD *)(v35 + 24 * i + 1060) = 0;
          v29 = 1;
        }
        LODWORD(v15) = System::__linkproc__ ROUND((double)v31 / flt_4BE3D8);
        v16 = v30 == v15;
        v9 = v15;
        if ( !v16 )
          sub_4B865C(v35, v35, 10125, i, v31, *(unsigned __int16 *)(v35 + 24 * i + 1064), 0, 0);
      }
    }
    if ( v29 != 0 )
      unknown_libname_25(v35, v9, v2);
    sub_4BE3FC(v35, v34);
  }
  __writefsdword(0, v18[0]);
  v19 = (int *)&loc_4BE3C4;
  unknown_libname_35(&v20, 14);
  return unknown_libname_35(&v26, 2);
}


// ==================== [0x4BE600] sub_4BE600 (size: 571) ====================
int __fastcall sub_4BE600(_DWORD *a1, unsigned int a2)
{
  int v2; // eax
  __int64 v3; // rax
  __int64 v4; // rax
  int v5; // eax
  __int64 v6; // rax
  __int64 v7; // rax
  unsigned int v9[6]; // [esp-Ch] [ebp-48h] BYREF
  int v10; // [esp+Ch] [ebp-30h]
  __int64 v11; // [esp+10h] [ebp-2Ch]
  int v12; // [esp+18h] [ebp-24h]
  int v13; // [esp+1Ch] [ebp-20h]
  unsigned int v14; // [esp+20h] [ebp-1Ch]
  int v15; // [esp+24h] [ebp-18h]
  int v16; // [esp+28h] [ebp-14h]
  int v17; // [esp+2Ch] [ebp-10h]
  int v18; // [esp+30h] [ebp-Ch]
  unsigned int v19; // [esp+34h] [ebp-8h]
  _DWORD *v20; // [esp+38h] [ebp-4h]
  int savedregs; // [esp+3Ch] [ebp+0h] BYREF

  v19 = a2;
  v20 = a1;
  v9[2] = (unsigned int)&savedregs;
  v9[1] = (unsigned int)&loc_4BE820;
  v9[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v9);
  if ( a1[157] == 0 )
    goto LABEL_25;
  v16 = 0;
  v17 = 0;
  v2 = (*(int (__fastcall **)(_DWORD))(**(_DWORD **)(v20[157] + 632) + 20))(*(_DWORD *)(v20[157] + 632)) - 1;
  if ( v2 >= 0 )
  {
    v12 = v2 + 1;
    v18 = 0;
    do
    {
      v13 = (*(int (__fastcall **)(_DWORD, int))(**(_DWORD **)(v20[157] + 632) + 24))(*(_DWORD *)(v20[157] + 632), v18);
      if ( *(_BYTE *)(v13 + 772) == 0 && v20[190] == *(_DWORD *)(v13 + 760) )
      {
        v3 = v20[9] - *(_DWORD *)(v13 + 36);
        if ( (int)((HIDWORD(v3) ^ v3) - HIDWORD(v3)) <= 12 )
        {
          v4 = v20[10] - *(_DWORD *)(v13 + 40);
          if ( (int)((HIDWORD(v4) ^ v4) - HIDWORD(v4)) <= 12 )
          {
            v16 += *(unsigned __int8 *)(v13 + 52);
            ++v17;
          }
        }
      }
      ++v18;
      --v12;
    }
    while ( v12 != 0 );
  }
  if ( v16 > 0 && v17 > 1 )
  {
    v15 = 0;
    if ( (unsigned int)v17 < 0xC )
    {
      v11 = v19;
      v15 = System::__linkproc__ ROUND((double)v19 * dbl_4E9EA4[v17]);
    }
    v5 = (*(int (__fastcall **)(_DWORD))(**(_DWORD **)(v20[157] + 632) + 20))(*(_DWORD *)(v20[157] + 632)) - 1;
    if ( v5 >= 0 )
    {
      v12 = v5 + 1;
      v18 = 0;
      do
      {
        v13 = (*(int (__fastcall **)(_DWORD, int))(**(_DWORD **)(v20[157] + 632) + 24))(
                *(_DWORD *)(v20[157] + 632),
                v18);
        if ( *(_BYTE *)(v13 + 772) == 0 && v20[190] == *(_DWORD *)(v13 + 760) )
        {
          v6 = v20[9] - *(_DWORD *)(v13 + 36);
          if ( (int)((HIDWORD(v6) ^ v6) - HIDWORD(v6)) <= 12 )
          {
            v7 = v20[10] - *(_DWORD *)(v13 + 40);
            if ( (int)((HIDWORD(v7) ^ v7) - HIDWORD(v7)) <= 12 )
            {
              v11 = (unsigned int)v15;
              v10 = *(unsigned __int8 *)(v13 + 52);
              v14 = System::__linkproc__ ROUND((double)(unsigned int)v15 / (double)v16 * (double)v10);
              if ( v14 > v19 )
                v14 = v19;
              sub_4BEB74(v13, v14);
            }
          }
        }
        ++v18;
        --v12;
      }
      while ( v12 != 0 );
    }
  }
  else
  {
LABEL_25:
    sub_4BEB74(v20, v19);
  }
  __writefsdword(0, v9[0]);
  return 0;
}


// ==================== [0x4BE934] sub_4BE934 (size: 536) ====================
int __fastcall sub_4BE934(int a1)
{
  int v1; // edx
  int v2; // eax
  int v3; // eax
  int v4; // edx
  int v5; // eax
  int v6; // eax
  unsigned __int8 v7; // al
  int v8; // edx
  int v9; // eax
  Ibutils *v10; // eax
  int v11; // ecx
  int v13; // [esp+8h] [ebp-10h]
  int v14; // [esp+10h] [ebp-8h]
  int v15; // [esp+10h] [ebp-8h]
  int v16; // [esp+10h] [ebp-8h]
  int v17; // [esp+14h] [ebp-4h]

  v17 = a1;
  LOWORD(a1) = *(_WORD *)(a1 + 58);
  v1 = sub_407B28(a1);
  v2 = v17;
  LOBYTE(v2) = *(_BYTE *)(v17 + 414);
  *(_WORD *)(v17 + 414) = unknown_libname_77(v2, v1);
  if ( *(_BYTE *)(v17 + 570) == 100 || *(_BYTE *)(v17 + 570) == 113 || *(_BYTE *)(v17 + 570) == 114 )
  {
    HIWORD(v3) = HIWORD(v17);
    LOWORD(v3) = *(_WORD *)(v17 + 414);
    v13 = (unsigned __int8)sub_407B28(v3);
    v4 = System::__linkproc__ ROUND(
           (double)v13
         + ((double)*(unsigned __int8 *)(v17 + 664) * *(double *)&tbyte_4BEB4C + *(double *)&tbyte_4BEB58)
         * flt_4BEB64
         * (double)*(unsigned __int8 *)(v17 + 664));
    v5 = v17;
    LOBYTE(v5) = *(_BYTE *)(v17 + 414);
    *(_WORD *)(v17 + 414) = unknown_libname_77(v5, v4);
    v14 = *(unsigned __int16 *)(v17 + 68)
        + *(unsigned __int8 *)(v17 + 664)
        * System::__linkproc__ ROUND(
            ((double)*(unsigned __int8 *)(v17 + 664) * *(double *)&tbyte_4BEB4C + *(double *)&tbyte_4BEB58)
          * (double)*(unsigned __int16 *)(v17 + 68));
    if ( *(_BYTE *)(v17 + 664) != 0 )
      *(_WORD *)(v17 + 424) = v14;
    else
      *(_WORD *)(v17 + 424) = *(_WORD *)(v17 + 68);
  }
  else
  {
    v15 = *(unsigned __int16 *)(v17 + 68);
    HIWORD(v6) = HIWORD(v17);
    LOWORD(v6) = *(_WORD *)(v17 + 414);
    v7 = sub_407B28(v6);
    v8 = System::__linkproc__ ROUND((double)(2 * *(unsigned __int8 *)(v17 + 664) + v7));
    v9 = v17;
    LOBYTE(v9) = *(_BYTE *)(v17 + 414);
    *(_WORD *)(v17 + 414) = unknown_libname_77(v9, v8);
    v16 = *(unsigned __int8 *)(v17 + 664)
        * System::__linkproc__ ROUND((double)*(unsigned __int16 *)(v17 + 68) * *(double *)&tbyte_4BEB68)
        + v15;
    v10 = (Ibutils *)System::__linkproc__ ROUND((double)(60 * *(unsigned __int8 *)(v17 + 664)
                                                       + *(unsigned __int16 *)(v17 + 68)));
    *(_WORD *)(v17 + 424) = Ibutils::Min(v10, v16, v11);
    *(_WORD *)(v17 + 412) = 0;
  }
  *(_BYTE *)(v17 + 572) = 15;
  return v17;
}


// ==================== [0x4BEB74] sub_4BEB74 (size: 423) ====================
int __fastcall sub_4BEB74(int a1, int a2)
{
  int v2; // eax
  int v3; // ecx
  int v4; // ecx
  int v5; // ecx
  unsigned int v7[2]; // [esp+8h] [ebp-5Ch] BYREF
  int *v8; // [esp+10h] [ebp-54h]
  _DWORD v9[3]; // [esp+14h] [ebp-50h] BYREF
  _BYTE v10[24]; // [esp+20h] [ebp-44h] BYREF
  _BYTE v11[20]; // [esp+38h] [ebp-2Ch] BYREF
  int v12; // [esp+4Ch] [ebp-18h] BYREF
  int v13; // [esp+50h] [ebp-14h] BYREF
  __int64 v14; // [esp+54h] [ebp-10h]
  unsigned int v15; // [esp+5Ch] [ebp-8h]
  int v16; // [esp+60h] [ebp-4h]
  int savedregs; // [esp+64h] [ebp+0h] BYREF

  v13 = 0;
  v12 = 0;
  memset(v9, 0, sizeof(v9));
  v15 = a2;
  v16 = a1;
  v8 = &savedregs;
  v7[1] = (unsigned int)j_unknown_libname_32_21;
  v7[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v7);
  v2 = a2;
  if ( *(_BYTE *)(v16 + 854) == 1 )
    v2 = 2 * a2;
  *(_DWORD *)(v16 + 76) += v2;
  v15 = v2;
  v14 = v15;
  sub_4BF580((double)v15 * *(double *)&tbyte_4BED28);
  sub_4B865C(v16, v16, 10044, 0, v15, 0, 0, 0);
  if ( *(_DWORD *)(v16 + 76) >= *(_DWORD *)(v16 + 80) )
  {
    *(_DWORD *)(v16 + 76) -= *(_DWORD *)(v16 + 80);
    sub_4BED6C(v16, (unsigned __int8)++*(_BYTE *)(v16 + 52) - 1);
    sub_4BF580(100.0);
    unknown_libname_16(v11, &dword_4BED34);
    LOBYTE(v3) = 19;
    System::__linkproc__ PStrNCat(v11, v16 + 4, v3);
    unknown_libname_16(v10, v11);
    LOBYTE(v4) = 20;
    System::__linkproc__ PStrNCat(v10, dword_4BED38, v4);
    unknown_libname_41(v7[0]);
    Sysutils::IntToStr(*(unsigned __int8 *)(v16 + 52));
    unknown_libname_91(*(_DWORD *)(v16 + 76), 0);
    unknown_libname_41(dword_4BED44);
    System::__linkproc__ LStrCatN(&v13, 11, v5, dword_4BED5C, &dword_4BED68);
    sub_4E42F8(v13);
    sub_4BCAA4(v16, 2000, 2000);
  }
  __writefsdword(0, v7[0]);
  v8 = (int *)&loc_4BED22;
  unknown_libname_35(v9, 3);
  return unknown_libname_35(&v12, 2);
}


// ==================== [0x4BEE20] sub_4BEE20 (size: 351) ====================
char __fastcall sub_4BEE20(_DWORD *a1)
{
  char v1; // zf
  bool v2; // al
  int v3; // eax
  int v4; // eax
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-Ch] [ebp-2Ch] BYREF
  void *v7; // [esp-8h] [ebp-28h]
  int *v8; // [esp-4h] [ebp-24h]
  int v9; // [esp+4h] [ebp-1Ch]
  int v10; // [esp+8h] [ebp-18h]
  int v11; // [esp+Ch] [ebp-14h]
  int v12; // [esp+10h] [ebp-10h]
  int v13; // [esp+14h] [ebp-Ch] BYREF
  char v14; // [esp+1Bh] [ebp-5h]
  _DWORD *v15; // [esp+1Ch] [ebp-4h]
  int savedregs; // [esp+20h] [ebp+0h] BYREF

  v13 = 0;
  v15 = a1;
  v8 = &savedregs;
  v7 = &loc_4BEF70;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  v14 = *(_BYTE *)(a1[190] + 45);
  if ( v14 == 0 )
  {
    System::__linkproc__ LStrCmp(*(_DWORD *)(v15[190] + 4), &str_9_0[1]);
    v2 = v1 && (int)abs32(v15[9] - 33) <= 10 && (int)abs32(v15[10] - 33) <= 10;
    v14 = v2;
    if ( !v2 )
    {
      v3 = (*(int (__fastcall **)(_DWORD))(*(_DWORD *)*off_4EA5EC + 20))(*off_4EA5EC) - 1;
      if ( v3 >= 0 )
      {
        v9 = v3 + 1;
        v12 = 0;
        while ( 1 )
        {
          (*(void (__fastcall **)(_DWORD, int, int *, int *, void *, struct _EXCEPTION_REGISTRATION_RECORD *))(*(_DWORD *)*off_4EA5EC + 12))(
            *off_4EA5EC,
            v12,
            &v13,
            v8,
            v7,
            ExceptionList);
          v11 = (*(unsigned __int16 (__fastcall **)(_DWORD, int))(*(_DWORD *)*off_4EA5EC + 24))(*off_4EA5EC, v12);
          v4 = (*(int (__fastcall **)(_DWORD, int))(*(_DWORD *)*off_4EA5EC + 24))(*off_4EA5EC, v12);
          v10 = (unsigned __int16)sub_407B24(v4);
          System::__linkproc__ LStrCmp(v13, *(_DWORD *)(v15[190] + 4));
          if ( v1 )
          {
            if ( (int)abs32(v15[9] - v11) <= 10 && (int)abs32(v15[10] - v10) <= 10 )
              break;
          }
          ++v12;
          if ( --v9 == 0 )
            goto LABEL_15;
        }
        v14 = 1;
      }
    }
  }
LABEL_15:
  __writefsdword(0, (unsigned int)ExceptionList);
  v8 = (int *)&loc_4BEF77;
  unknown_libname_34(&v13);
  return v14;
}


// ==================== [0x4BF180] sub_4BF180 (size: 512) ====================
char __fastcall sub_4BF180(int a1, int a2)
{
  int v2; // eax
  int v3; // eax
  char v5; // [esp+Bh] [ebp-9h]

  v5 = sub_4BF144(a2);
  if ( *(_BYTE *)(a2 + 570) == 0 )
  {
    if ( sub_4BF0A0(a1) >= 2 )
    {
LABEL_14:
      if ( *(_BYTE *)(*off_4EA080[0] + 65) != 0 && *(_BYTE *)(a1 + 1000) != 0 && *(_BYTE *)(a2 + 1000) != 0 )
      {
        v5 = -35;
        *(_BYTE *)(a1 + 680) = 1;
        if ( (unsigned __int8)sub_490400(*off_4EA080[0], *(_DWORD *)(a1 + 800)) != 0 )
        {
          if ( *(_DWORD *)(a1 + 800) == *(_DWORD *)(a2 + 800)
            || (unsigned __int8)sub_499BD8(*(_DWORD *)(a1 + 800), *(_DWORD *)(a2 + 800)) != 0 )
          {
            return -76;
          }
          else if ( (unsigned __int8)sub_491160(*off_4EA080[0], *(_DWORD *)(a2 + 800)) != 0 )
          {
            return 69;
          }
        }
        else if ( (unsigned __int8)sub_491160(*off_4EA080[0], *(_DWORD *)(a1 + 800)) != 0 )
        {
          if ( *(_DWORD *)(a1 + 800) == *(_DWORD *)(a2 + 800)
            || (unsigned __int8)sub_499BD8(*(_DWORD *)(a1 + 800), *(_DWORD *)(a2 + 800)) != 0 )
          {
            return -76;
          }
          else if ( (unsigned __int8)sub_490438(*off_4EA080[0], a2) != 0 )
          {
            return 69;
          }
        }
      }
      return v5;
    }
    if ( *(_BYTE *)(a2 + 924) != 0 )
      v5 = 47;
    v2 = sub_4BF380(a1, a1, a2) - 1;
    if ( v2 != 0 )
    {
      v3 = v2 - 1;
      if ( v3 == 0 )
      {
        v5 = 69;
LABEL_10:
        if ( *(_BYTE *)(*(_DWORD *)(a2 + 760) + 47) != 0 )
        {
          if ( *(_DWORD *)(a1 + 800) == *(_DWORD *)(a2 + 800) )
            v5 = -76;
          else
            v5 = 69;
        }
        goto LABEL_14;
      }
      if ( v3 != 1 )
        goto LABEL_10;
    }
    v5 = -76;
    goto LABEL_10;
  }
  if ( *(_BYTE *)(a2 + 664) < 8u )
    v5 = byte_4E9F20[*(unsigned __int8 *)(a2 + 664)];
  if ( *(_BYTE *)(a2 + 728) != 0 )
    v5 = -7;
  if ( *(_BYTE *)(a2 + 716) != 0 )
    return 125;
  return v5;
}


// ==================== [0x4BF7DC] sub_4BF7DC (size: 1308) ====================
unsigned __int16 __fastcall sub_4BF7DC(int a1)
{
  char v1; // al
  bool v2; // cf
  char v3; // al
  int v4; // eax
  int v5; // ecx
  int v6; // eax
  int v7; // ecx
  int v8; // ecx
  int v9; // eax
  int v10; // ecx
  int v11; // eax
  int v12; // ecx
  int v13; // ecx
  int v14; // eax
  int v15; // edx
  unsigned __int16 result; // ax
  int v17; // [esp-4h] [ebp-18h]
  int v18; // [esp-4h] [ebp-18h]
  int v19; // [esp-4h] [ebp-18h]
  int v20; // [esp-4h] [ebp-18h]
  int v21; // [esp-4h] [ebp-18h]
  unsigned int v22; // [esp+Ch] [ebp-8h]
  unsigned int v23; // [esp+Ch] [ebp-8h]
  int v24; // [esp+Ch] [ebp-8h]

  v1 = *(_BYTE *)(a1 + 47);
  v2 = v1 == 0;
  v3 = v1 - 1;
  if ( v2 )
  {
    *(_WORD *)(a1 + 68) = System::__linkproc__ ROUND(
                            ((double)*(unsigned __int8 *)(a1 + 52) / flt_4BFCF8
                           + flt_4BFCFC
                           + (double)*(unsigned __int8 *)(a1 + 52) / flt_4BFD00)
                          * (double)*(unsigned __int8 *)(a1 + 52))
                        + 14;
    *(_WORD *)(a1 + 70) = System::__linkproc__ ROUND((double)*(unsigned __int8 *)(a1 + 52) * flt_4BFD04) + 11;
    *(_WORD *)(a1 + 86) = System::__linkproc__ ROUND((double)*(unsigned __int8 *)(a1 + 52) / flt_4BFD08 * (double)*(unsigned __int8 *)(a1 + 52))
                        + 50;
    *(_BYTE *)(a1 + 89) = System::__linkproc__ ROUND((double)*(unsigned __int8 *)(a1 + 52) / flt_4BFD00 * (double)*(unsigned __int8 *)(a1 + 52))
                        + 15;
    *(_BYTE *)(a1 + 91) = System::__linkproc__ ROUND((double)*(unsigned __int8 *)(a1 + 52) / flt_4BFD0C * (double)*(unsigned __int8 *)(a1 + 52))
                        + 12;
    v17 = Ibutils::Max((Ibutils *)1, *(unsigned __int8 *)(a1 + 52) / 5u, 5);
    v4 = Ibutils::Max((Ibutils *)(*(unsigned __int8 *)(a1 + 52) / 5u - 1), 1, 5);
    *(_WORD *)(a1 + 58) = unknown_libname_77(v4, v17);
    *(_WORD *)(a1 + 62) = 0;
    *(_WORD *)(a1 + 60) = 0;
    *(_WORD *)(a1 + 54) = unknown_libname_77(0, *(unsigned __int8 *)(a1 + 52) / 7u);
    *(_WORD *)(a1 + 56) = 0;
  }
  else if ( v3 != 0 )
  {
    if ( v3 == 1 )
    {
      *(_WORD *)(a1 + 68) = System::__linkproc__ ROUND(
                              ((double)*(unsigned __int8 *)(a1 + 52) / flt_4BFD3C + flt_4BFD40)
                            * (double)*(unsigned __int8 *)(a1 + 52))
                          + 14;
      *(_WORD *)(a1 + 70) = System::__linkproc__ ROUND(
                              (double)*(unsigned __int8 *)(a1 + 52)
                            / flt_4BFD44
                            * *(double *)&tbyte_4BFD28
                            * (double)*(unsigned __int8 *)(a1 + 52))
                          + 13;
      *(_WORD *)(a1 + 86) = System::__linkproc__ ROUND((double)*(unsigned __int8 *)(a1 + 52) / flt_4BFCF8 * (double)*(unsigned __int8 *)(a1 + 52))
                          + 50;
      *(_BYTE *)(a1 + 89) = System::__linkproc__ ROUND((double)*(unsigned __int8 *)(a1 + 52) / flt_4BFD48 * (double)*(unsigned __int8 *)(a1 + 52))
                          + 15;
      *(_BYTE *)(a1 + 91) = System::__linkproc__ ROUND((double)*(unsigned __int8 *)(a1 + 52) / flt_4BFD4C * (double)*(unsigned __int8 *)(a1 + 52))
                          + 12;
      v23 = *(unsigned __int8 *)(a1 + 52) / 7u;
      v20 = Ibutils::Max((Ibutils *)1, v23, 7);
      v11 = Ibutils::Max((Ibutils *)(v23 - 1), 0, v10);
      *(_WORD *)(a1 + 58) = unknown_libname_77(v11, v20);
      *(_WORD *)(a1 + 60) = 0;
      v21 = Ibutils::Max((Ibutils *)1, v23, v12);
      v14 = Ibutils::Max((Ibutils *)(v23 - 1), 0, v13);
      *(_WORD *)(a1 + 62) = unknown_libname_77(v14, v21);
      *(_WORD *)(a1 + 54) = 0;
      v24 = System::__linkproc__ ROUND((double)*(unsigned __int8 *)(a1 + 52) / flt_4BFD3C);
      LOBYTE(v15) = v24;
      *(_WORD *)(a1 + 56) = unknown_libname_77(v24 / 2, v15 + 1);
    }
  }
  else
  {
    *(_WORD *)(a1 + 68) = System::__linkproc__ ROUND(
                            ((double)*(unsigned __int8 *)(a1 + 52) / flt_4BFD10 + *(double *)&tbyte_4BFD14)
                          * (double)*(unsigned __int8 *)(a1 + 52))
                        + 14;
    *(_WORD *)(a1 + 70) = System::__linkproc__ ROUND(
                            ((double)*(unsigned __int8 *)(a1 + 52) / flt_4BFD20 + flt_4BFD24)
                          * *(double *)&tbyte_4BFD28
                          * (double)*(unsigned __int8 *)(a1 + 52))
                        + 13;
    *(_WORD *)(a1 + 86) = System::__linkproc__ ROUND((double)*(unsigned __int8 *)(a1 + 52) / flt_4BFD20 * (double)*(unsigned __int8 *)(a1 + 52))
                        + 50;
    *(_BYTE *)(a1 + 89) = System::__linkproc__ ROUND((double)*(unsigned __int8 *)(a1 + 52) / flt_4BFD34 * (double)*(unsigned __int8 *)(a1 + 52))
                        + 15;
    *(_BYTE *)(a1 + 91) = System::__linkproc__ ROUND((double)*(unsigned __int8 *)(a1 + 52) / flt_4BFD38 * (double)*(unsigned __int8 *)(a1 + 52))
                        + 12;
    v22 = *(unsigned __int8 *)(a1 + 52) / 7u;
    v18 = Ibutils::Max((Ibutils *)1, v22, 7);
    v6 = Ibutils::Max((Ibutils *)(v22 - 1), 0, v5);
    *(_WORD *)(a1 + 58) = unknown_libname_77(v6, v18);
    v19 = Ibutils::Max((Ibutils *)1, v22, v7);
    v9 = Ibutils::Max((Ibutils *)(v22 - 1), 0, v8);
    *(_WORD *)(a1 + 60) = unknown_libname_77(v9, v19);
    *(_WORD *)(a1 + 62) = 0;
    *(_WORD *)(a1 + 54) = 0;
    *(_WORD *)(a1 + 56) = 0;
  }
  if ( *(_WORD *)(a1 + 64) > *(_WORD *)(a1 + 68) )
    *(_WORD *)(a1 + 64) = *(_WORD *)(a1 + 68);
  result = *(_WORD *)(a1 + 66);
  if ( result > *(_WORD *)(a1 + 70) )
  {
    result = *(_WORD *)(a1 + 70);
    *(_WORD *)(a1 + 66) = result;
  }
  if ( *(_BYTE *)(a1 + 52) > 0x38u )
    *(_DWORD *)(a1 + 89) = 16711935;
  return result;
}


// ==================== [0x4BFD50] sub_4BFD50 (size: 620) ====================
char __fastcall sub_4BFD50(int a1)
{
  char v1; // al
  int v2; // eax
  int v4; // [esp+4h] [ebp-14h]
  int v5; // [esp+Ch] [ebp-Ch]
  int v6; // [esp+10h] [ebp-8h]

  *(_BYTE *)(a1 + 572) = *(_BYTE *)(a1 + 216) + 5;
  *(_BYTE *)(a1 + 573) = 0;
  *(_BYTE *)(a1 + 574) = 0;
  if ( *(_BYTE *)(a1 + 47) == 2 )
    v1 = *(_BYTE *)(a1 + 218) + 18;
  else
    v1 = *(_BYTE *)(a1 + 218) + 15;
  *(_BYTE *)(a1 + 620) = v1;
  *(_DWORD *)(a1 + 1304) = 0;
  *(_DWORD *)(a1 + 1308) = 0;
  *(_DWORD *)(a1 + 1312) = 0;
  *(_DWORD *)(a1 + 1316) = 0;
  *(_DWORD *)(a1 + 1320) = 0;
  v2 = *(_DWORD *)(*(_DWORD *)(a1 + 1052) + 8) - 1;
  if ( v2 >= 0 )
  {
    v4 = *(_DWORD *)(*(_DWORD *)(a1 + 1052) + 8);
    v6 = 0;
    do
    {
      v5 = Classes::TList::Get(*(Classes::TList **)(a1 + 1052), v6);
      LOWORD(v2) = *(_WORD *)(v5 + 4);
      switch ( (char)v2 )
      {
        case 3:
          *(_DWORD *)(a1 + 1304) = v5;
          LOBYTE(v2) = v5;
          if ( *(_BYTE *)(v5 + 6) != 0 )
          {
            LOBYTE(v2) = System::__linkproc__ ROUND((double)*(unsigned __int8 *)(v5 + 6) * flt_4BFFBC);
            *(_BYTE *)(a1 + 572) += v2;
          }
          break;
        case 4:
          *(_DWORD *)(a1 + 1304) = v5;
          LOBYTE(v2) = v5;
          if ( *(_BYTE *)(v5 + 6) != 0 )
          {
            LOBYTE(v2) = System::__linkproc__ ROUND((double)*(unsigned __int8 *)(v5 + 6) * *(double *)&tbyte_4BFFC4);
            *(_BYTE *)(a1 + 572) += v2;
          }
          break;
        case 7:
          *(_DWORD *)(a1 + 1308) = v5;
          if ( *(_BYTE *)(v5 + 6) != 0 )
            *(_BYTE *)(a1 + 572) += System::__linkproc__ ROUND((double)*(unsigned __int8 *)(v5 + 6) * flt_4BFFC0);
          *(_BYTE *)(a1 + 573) = *(_BYTE *)(v5 + 6) + 5;
          *(_DWORD *)(a1 + 816) = 7 - *(unsigned __int8 *)(*(_DWORD *)(a1 + 1308) + 6);
          v2 = System::__linkproc__ RandInt(*(_DWORD *)(a1 + 816));
          *(_DWORD *)(a1 + 820) = v2;
          break;
        case 12:
          LOBYTE(v2) = v5;
          *(_DWORD *)(a1 + 1312) = v5;
          break;
        case 25:
          LOBYTE(v2) = v5;
          *(_DWORD *)(a1 + 1316) = v5;
          break;
        case 26:
          *(_DWORD *)(a1 + 1320) = v5;
          LOBYTE(v2) = 4 * *(_BYTE *)(v5 + 6) + 4;
          *(_BYTE *)(a1 + 574) = v2;
          break;
        default:
          break;
      }
      ++v6;
      --v4;
    }
    while ( v4 != 0 );
  }
  return v2;
}


// ==================== [0x4C022C] sub_4C022C (size: 381) ====================
int __fastcall sub_4C022C(int a1)
{
  int result; // eax
  int v2; // ecx
  __int64 v3; // rax
  bool v4; // zf
  int v5; // [esp+14h] [ebp-10h]
  int v6; // [esp+18h] [ebp-Ch]
  int v7; // [esp+18h] [ebp-Ch]
  int v8; // [esp+1Ch] [ebp-8h]

  v8 = 0;
  while ( 1 )
  {
    result = 3 * v8;
    if ( *(_WORD *)(a1 + 24 * v8 + 1060) != 0 )
    {
      result = sub_4AC2F8(*off_4EA490, *(unsigned __int16 *)(a1 + 24 * v8 + 1060));
      if ( result != 0 && (v8 == 8 || v8 == 7) && *(_BYTE *)(result + 16) == 114 )
        break;
    }
    if ( ++v8 == 9 )
      return result;
  }
  v6 = *(unsigned __int16 *)(a1 + 24 * v8 + 1062);
  v5 = System::__linkproc__ ROUND((double)v6 / flt_4C03AC);
  v7 = v6 - 1000;
  if ( v7 > 0 )
  {
    *(_WORD *)(a1 + 24 * v8 + 1062) = v7;
  }
  else
  {
    v7 = 0;
    LOWORD(v2) = 0;
    *(_WORD *)(a1 + 24 * v8 + 1062) = 0;
    if ( *(_BYTE *)(a1 + 570) == 0 )
      sub_4D0BDC(a1, a1 + 24 * v8 + 1056);
    *(_WORD *)(a1 + 24 * v8 + 1060) = 0;
    unknown_libname_25(a1, a1, v2);
  }
  LODWORD(v3) = System::__linkproc__ ROUND((double)v7 / flt_4C03AC);
  v4 = v5 == v3;
  result = HIDWORD(v3);
  if ( !v4 )
    return sub_4B865C(a1, a1, 10125, v8, v7, *(unsigned __int16 *)(a1 + 24 * v8 + 1064), 0, 0);
  return result;
}


// ==================== [0x4C03B0] sub_4C03B0 (size: 324) ====================
int __fastcall sub_4C03B0(int a1)
{
  int v1; // eax
  __int16 v2; // ax
  int v3; // eax
  int v4; // eax
  int v5; // eax
  int v6; // ebx
  int v7; // eax
  int v8; // eax
  __int16 v9; // ax
  int v10; // eax
  int v11; // ebx
  int v12; // eax
  int v13; // eax
  __int16 v14; // ax
  int v15; // eax
  int v16; // ebx
  int v17; // eax
  int v18; // eax
  __int16 v19; // ax
  int v20; // eax
  int v21; // ebx
  int v22; // eax
  int v23; // eax
  __int16 v24; // ax
  int v25; // eax
  int v26; // ebx
  int v27; // eax
  int v28; // eax
  __int16 v29; // ax
  int v30; // eax
  int v31; // ecx
  int v32; // eax
  __int16 v33; // ax
  int v34; // eax
  int v35; // ecx
  int v36; // eax
  __int16 v37; // ax
  int v38; // eax
  int v39; // edx
  int v40; // eax
  __int16 v41; // ax
  int v42; // eax
  int v43; // edx
  int v44; // eax
  __int16 v45; // ax
  int v46; // eax
  int v47; // edx
  int v48; // eax
  __int16 v49; // ax
  int result; // eax
  _BYTE *v51; // eax
  int v52; // eax
  int v53; // eax
  int v54; // eax
  int v55; // eax
  int v56; // eax
  int v57; // eax
  int v58; // eax
  int v59; // eax
  int v60; // eax
  int v61; // eax
  _WORD v62[20]; // [esp+Ch] [ebp-4Ch] BYREF
  char v63; // [esp+3Dh] [ebp-1Bh]
  char v64; // [esp+3Eh] [ebp-1Ah]
  char v65; // [esp+3Fh] [ebp-19h]
  char v66; // [esp+40h] [ebp-18h]
  char v67; // [esp+41h] [ebp-17h]
  char v68; // [esp+42h] [ebp-16h]
  char v69; // [esp+43h] [ebp-15h]
  _BYTE *v70; // [esp+44h] [ebp-14h]
  char v71; // [esp+48h] [ebp-10h]
  char v72; // [esp+49h] [ebp-Fh]
  char v73; // [esp+4Ah] [ebp-Eh]
  char v74; // [esp+4Bh] [ebp-Dh]
  int v75; // [esp+4Ch] [ebp-Ch]
  int i; // [esp+50h] [ebp-8h]
  int v77; // [esp+54h] [ebp-4h]

  v77 = a1;
  System::__linkproc__ FillChar(a1 + 448, 34, 0);
  qmemcpy(v62, (const void *)(v77 + 408), sizeof(v62));
  qmemcpy((void *)(v77 + 408), (const void *)(v77 + 52), 0x28u);
  *(_WORD *)(v77 + 420) = v62[6];
  *(_WORD *)(v77 + 422) = v62[7];
  *(_WORD *)(v77 + 440) = 0;
  *(_BYTE *)(v77 + 444) = 0;
  *(_BYTE *)(v77 + 446) = 0;
  *(_BYTE *)(v77 + 586) = 0;
  *(_BYTE *)(v77 + 587) = 0;
  *(_BYTE *)(v77 + 584) = 0;
  *(_BYTE *)(v77 + 585) = 0;
  *(_BYTE *)(v77 + 588) = 1;
  *(_DWORD *)(v77 + 592) = 0;
  *(_BYTE *)(v77 + 622) = 0;
  v69 = *(_BYTE *)(v77 + 836);
  *(_BYTE *)(v77 + 836) = 0;
  *(_BYTE *)(v77 + 843) = 0;
  *(_BYTE *)(v77 + 844) = 0;
  *(_BYTE *)(v77 + 845) = 0;
  *(_BYTE *)(v77 + 852) = 0;
  *(_BYTE *)(v77 + 853) = 0;
  *(_BYTE *)(v77 + 854) = 0;
  *(_BYTE *)(v77 + 855) = 0;
  *(_BYTE *)(v77 + 856) = 0;
  *(_BYTE *)(v77 + 857) = 0;
  *(_BYTE *)(v77 + 858) = 0;
  if ( *(_BYTE *)(v77 + 621) < 2u )
    *(_BYTE *)(v77 + 696) = 0;
  *(_DWORD *)(v77 + 932) = 0;
  v68 = 0;
  v67 = 0;
  v66 = 0;
  *(_DWORD *)(v77 + 936) = 0;
  v1 = v77;
  *(_DWORD *)(v77 + 944) = 0;
  *(_DWORD *)(v1 + 948) = 0;
  v65 = 0;
  v64 = 0;
  v63 = 0;
  *(_BYTE *)(v77 + 580) = 0;
  v71 = 0;
  v72 = 0;
  v73 = 0;
  v74 = 0;
  for ( i = 0; i != 9; ++i )
  {
    if ( *(_WORD *)(v77 + 24 * i + 1060) != 0 && *(_WORD *)(v77 + 24 * i + 1062) != 0 )
    {
      sub_4C0EBC(v77, v77 + 24 * i + 1056, v77 + 448);
      v70 = (_BYTE *)sub_4AC2F8(*off_4EA490, *(unsigned __int16 *)(v77 + 24 * i + 1060));
      if ( v70 != nullptr )
      {
        if ( i == 1 || i == 2 )
          *(_BYTE *)(v77 + 446) += v70[17];
        else
          *(_BYTE *)(v77 + 444) += v70[17];
        *(_WORD *)(v77 + 440) += (unsigned __int8)v70[17];
        if ( i == 1 )
        {
          if ( (unsigned __int8)(v70[19] - 1) < 0xAu )
            *(_BYTE *)(v77 + 478) = v70[19];
          if ( v70[19] >= 0xCEu )
            *(_BYTE *)(v77 + 479) -= v70[19];
          if ( (char)v70[19] <= -51 && (char)v70[19] >= -100 )
            *(_BYTE *)(v77 + 479) += v70[19] + 50;
        }
        if ( i == 2 )
        {
          if ( v70[16] == 112 )
            *(_BYTE *)(v77 + 843) = 1;
          if ( v70[16] == 113 )
            *(_BYTE *)(v77 + 844) = 1;
          if ( v70[16] == 115 )
            *(_BYTE *)(v77 + 852) = 1;
          if ( v70[16] == 116 )
            *(_BYTE *)(v77 + 853) = 1;
          if ( v70[16] == 0xA1 )
            *(_BYTE *)(v77 + 854) = 1;
          if ( v70[16] == 118 )
            *(_BYTE *)(v77 + 855) = 1;
          if ( v70[16] == 119 )
            *(_BYTE *)(v77 + 856) = 1;
          if ( v70[16] == 0x96 )
          {
            v52 = v77;
            *(_BYTE *)(v77 + 844) = 1;
            *(_BYTE *)(v52 + 855) = 1;
          }
          if ( v70[16] == 0x97 )
          {
            v53 = v77;
            *(_BYTE *)(v77 + 844) = 1;
            *(_BYTE *)(v53 + 852) = 1;
          }
          if ( v70[16] == 0x98 )
          {
            v54 = v77;
            *(_BYTE *)(v77 + 844) = 1;
            *(_BYTE *)(v54 + 853) = 1;
          }
          if ( v70[16] == 0x99 )
          {
            v55 = v77;
            *(_BYTE *)(v77 + 844) = 1;
            *(_BYTE *)(v55 + 856) = 1;
          }
          if ( v70[16] == 0x9A )
          {
            v56 = v77;
            *(_BYTE *)(v77 + 855) = 1;
            *(_BYTE *)(v56 + 852) = 1;
          }
          if ( v70[16] == 0x9B )
          {
            v57 = v77;
            *(_BYTE *)(v77 + 855) = 1;
            *(_BYTE *)(v57 + 853) = 1;
          }
          if ( v70[16] == 0x9C )
          {
            v58 = v77;
            *(_BYTE *)(v77 + 855) = 1;
            *(_BYTE *)(v58 + 856) = 1;
          }
          if ( v70[16] == 0x9D )
          {
            v59 = v77;
            *(_BYTE *)(v77 + 843) = 1;
            *(_BYTE *)(v59 + 844) = 1;
          }
          if ( v70[16] == 0x9E )
          {
            v60 = v77;
            *(_BYTE *)(v77 + 843) = 1;
            *(_BYTE *)(v60 + 855) = 1;
          }
          if ( v70[16] == 0x9F )
          {
            v61 = v77;
            *(_BYTE *)(v77 + 843) = 1;
            *(_BYTE *)(v61 + 858) = 1;
          }
          if ( v70[16] == 0xA0 )
            *(_BYTE *)(v77 + 696) = 1;
          if ( v70[16] == 111 )
          {
            *(_WORD *)(v77 + 112) = -5536;
            *(_BYTE *)(v77 + 836) = 1;
          }
        }
        if ( i == 1 )
        {
          if ( v70[19] == 14 )
            *(_BYTE *)(v77 + 844) = 1;
          if ( v70[19] == 13 )
          {
            v65 = 1;
            *(_DWORD *)(v77 + 936) += (unsigned __int8)v70[17];
          }
        }
        if ( i == 3 )
        {
          if ( v70[16] == 120 )
            *(_BYTE *)(v77 + 857) = 1;
          if ( v70[16] == 121 )
            *(_BYTE *)(v77 + 858) = 1;
          if ( v70[16] == 123 )
            v72 = 1;
          if ( v70[16] == 0x87 )
          {
            v66 = 1;
            *(_DWORD *)(v77 + 932) += (unsigned __int8)v70[18];
          }
          if ( v70[16] == 0x8A )
          {
            v63 = 1;
            *(_DWORD *)(v77 + 936) += (unsigned __int8)v70[18];
          }
        }
        if ( i == 8 || i == 7 )
        {
          if ( v70[16] == 111 )
          {
            *(_WORD *)(v77 + 112) = -5536;
            *(_BYTE *)(v77 + 836) = 1;
          }
          if ( v70[16] == 112 )
            *(_BYTE *)(v77 + 843) = 1;
          if ( v70[16] == 113 )
            *(_BYTE *)(v77 + 844) = 1;
          if ( v70[16] == 114 )
            *(_BYTE *)(v77 + 845) = 1;
          if ( v70[16] == 115 )
            *(_BYTE *)(v77 + 852) = 1;
          if ( v70[16] == 116 )
            *(_BYTE *)(v77 + 853) = 1;
          if ( v70[16] == 118 )
            *(_BYTE *)(v77 + 855) = 1;
          if ( v70[16] == 119 )
            *(_BYTE *)(v77 + 856) = 1;
          if ( v70[16] == 122 )
            v71 = 1;
          if ( v70[16] == 0x85 )
          {
            v68 = 1;
            *(_DWORD *)(v77 + 932) += (unsigned __int8)v70[18];
          }
          if ( v70[16] == 0x88 )
          {
            v65 = 1;
            *(_DWORD *)(v77 + 936) += (unsigned __int8)v70[18];
          }
        }
        if ( i == 5 || i == 6 )
        {
          if ( v70[16] == 124 )
            v73 = 1;
          if ( v70[16] == 0x86 )
          {
            v67 = 1;
            *(_DWORD *)(v77 + 932) += (unsigned __int8)v70[18];
          }
          if ( v70[16] == 0x89 )
          {
            v64 = 1;
            *(_DWORD *)(v77 + 936) += (unsigned __int8)v70[18];
          }
        }
        if ( i == 4 )
        {
          if ( v70[16] == 0x8B )
          {
            if ( *(_BYTE *)(v77 + 852) == 1 )
              *(_BYTE *)(v77 + 852) = 3;
            else
              *(_BYTE *)(v77 + 852) = 2;
          }
          if ( v70[16] == 0x8C )
          {
            if ( *(_BYTE *)(v77 + 853) == 1 )
              *(_BYTE *)(v77 + 853) = 3;
            else
              *(_BYTE *)(v77 + 853) = 2;
          }
          if ( v70[16] == 125 )
            v74 = 1;
        }
      }
    }
  }
  if ( v71 != 0 && v72 != 0 && v73 != 0 && v74 != 0 )
    *(_BYTE *)(v77 + 580) = 1;
  if ( ((unsigned __int8)v67 & (unsigned __int8)v66) != 0 && v68 != 0 )
    *(_DWORD *)(v77 + 932) += 50;
  if ( ((unsigned __int8)v64 & (unsigned __int8)v63) != 0 && v65 != 0 )
    *(_WORD *)(v77 + 452) += 2;
  v2 = sub_4BF6F0(v77);
  *(_WORD *)(v77 + 440) = v2;
  if ( *(_BYTE *)(v77 + 840) != 0 && *(_WORD *)(v77 + 112) != 0 )
    *(_BYTE *)(v77 + 836) = 1;
  if ( *(_BYTE *)(v77 + 836) != 0 )
  {
    if ( v69 == 0 )
    {
      v3 = sub_4BA43C(v77);
      *(_DWORD *)(v77 + 92) = v3;
      sub_4BA5B4(v77);
    }
  }
  else if ( v69 != 0 )
  {
    *(_WORD *)(v77 + 112) = 0;
    v4 = sub_4BA43C(v77);
    *(_DWORD *)(v77 + 92) = v4;
    sub_4BA5B4(v77);
  }
  sub_4BFD50(v77);
  v75 = *(_DWORD *)(v77 + 676);
  if ( *(_WORD *)(v77 + 1108) != 0 && *(_WORD *)(v77 + 1110) != 0 )
    *(_DWORD *)(v77 + 676) = 3;
  else
    *(_DWORD *)(v77 + 676) = 0;
  if ( v75 != *(_DWORD *)(v77 + 676) )
    sub_4B8EBC(v77, 10137, 0, 0, 0, 0, 0);
  *(_BYTE *)(v77 + 620) += *(_BYTE *)(v77 + 454);
  *(_BYTE *)(v77 + 572) += *(_BYTE *)(v77 + 452);
  *(_BYTE *)(v77 + 586) += *(_BYTE *)(v77 + 466);
  *(_BYTE *)(v77 + 587) += *(_BYTE *)(v77 + 468);
  *(_BYTE *)(v77 + 584) += *(_BYTE *)(v77 + 470);
  *(_BYTE *)(v77 + 585) += *(_BYTE *)(v77 + 472);
  *(_BYTE *)(v77 + 588) += *(_BYTE *)(v77 + 474);
  *(_DWORD *)(v77 + 592) += *(unsigned __int8 *)(v77 + 476);
  *(_DWORD *)(v77 + 592) -= *(unsigned __int8 *)(v77 + 477);
  *(_BYTE *)(v77 + 622) = *(_BYTE *)(v77 + 480);
  *(_WORD *)(v77 + 424) = *(_WORD *)(v77 + 448) + *(_WORD *)(v77 + 68);
  *(_WORD *)(v77 + 426) = *(_WORD *)(v77 + 450) + *(_WORD *)(v77 + 70);
  HIWORD(v5) = HIWORD(v77);
  LOWORD(v5) = *(_WORD *)(v77 + 456);
  v6 = sub_407B28(v5);
  HIWORD(v7) = HIWORD(v77);
  LOWORD(v7) = *(_WORD *)(v77 + 54);
  LOBYTE(v6) = sub_407B28(v7) + v6;
  v8 = v77;
  LOBYTE(v8) = *(_BYTE *)(v77 + 54) + *(_BYTE *)(v77 + 456);
  v9 = unknown_libname_77(v8, v6);
  *(_WORD *)(v77 + 410) = v9;
  HIWORD(v10) = HIWORD(v77);
  LOWORD(v10) = *(_WORD *)(v77 + 458);
  v11 = sub_407B28(v10);
  HIWORD(v12) = HIWORD(v77);
  LOWORD(v12) = *(_WORD *)(v77 + 56);
  LOBYTE(v11) = sub_407B28(v12) + v11;
  v13 = v77;
  LOBYTE(v13) = *(_BYTE *)(v77 + 56) + *(_BYTE *)(v77 + 458);
  v14 = unknown_libname_77(v13, v11);
  *(_WORD *)(v77 + 412) = v14;
  HIWORD(v15) = HIWORD(v77);
  LOWORD(v15) = *(_WORD *)(v77 + 460);
  v16 = sub_407B28(v15);
  HIWORD(v17) = HIWORD(v77);
  LOWORD(v17) = *(_WORD *)(v77 + 58);
  LOBYTE(v16) = sub_407B28(v17) + v16;
  v18 = v77;
  LOBYTE(v18) = *(_BYTE *)(v77 + 58) + *(_BYTE *)(v77 + 460);
  v19 = unknown_libname_77(v18, v16);
  *(_WORD *)(v77 + 414) = v19;
  HIWORD(v20) = HIWORD(v77);
  LOWORD(v20) = *(_WORD *)(v77 + 462);
  v21 = sub_407B28(v20);
  HIWORD(v22) = HIWORD(v77);
  LOWORD(v22) = *(_WORD *)(v77 + 60);
  LOBYTE(v21) = sub_407B28(v22) + v21;
  v23 = v77;
  LOBYTE(v23) = *(_BYTE *)(v77 + 60) + *(_BYTE *)(v77 + 462);
  v24 = unknown_libname_77(v23, v21);
  *(_WORD *)(v77 + 416) = v24;
  HIWORD(v25) = HIWORD(v77);
  LOWORD(v25) = *(_WORD *)(v77 + 464);
  v26 = sub_407B28(v25);
  HIWORD(v27) = HIWORD(v77);
  LOWORD(v27) = *(_WORD *)(v77 + 62);
  LOBYTE(v26) = sub_407B28(v27) + v26;
  v28 = v77;
  LOBYTE(v28) = *(_BYTE *)(v77 + 62) + *(_BYTE *)(v77 + 464);
  v29 = unknown_libname_77(v28, v26);
  *(_WORD *)(v77 + 418) = v29;
  v51 = (_BYTE *)v77;
  if ( *(_BYTE *)(v77 + 53) == 1 )
  {
    if ( *(_BYTE *)(v77 + 47) != 0 )
    {
      if ( *(_BYTE *)(v77 + 47) == 1 )
      {
        *(_BYTE *)(v77 + 416) += 10;
        v51[417] += 11;
      }
      else
      {
        *(_BYTE *)(v77 + 418) += 10;
        v51[419] += 11;
      }
    }
    else
    {
      *(_BYTE *)(v77 + 414) += 10;
      v51[415] += 11;
    }
    v51[410] += 5;
    v51[412] += 5;
    v51[411] += 6;
    v51[413] += 6;
  }
  if ( *(_WORD *)(v77 + 114) != 0 )
  {
    HIWORD(v30) = HIWORD(v77);
    LOWORD(v30) = *(_WORD *)(v77 + 410);
    v31 = sub_407B28(v30);
    LOBYTE(v31) = *(_BYTE *)(v77 + 52) / 7u + v31 + 2;
    v32 = v77;
    LOBYTE(v32) = *(_BYTE *)(v77 + 410);
    v33 = unknown_libname_77(v32, v31);
    *(_WORD *)(v77 + 410) = v33;
  }
  if ( *(_WORD *)(v77 + 116) != 0 )
  {
    HIWORD(v34) = HIWORD(v77);
    LOWORD(v34) = *(_WORD *)(v77 + 412);
    v35 = sub_407B28(v34);
    LOBYTE(v35) = *(_BYTE *)(v77 + 52) / 7u + v35 + 2;
    v36 = v77;
    LOBYTE(v36) = *(_BYTE *)(v77 + 412);
    v37 = unknown_libname_77(v36, v35);
    *(_WORD *)(v77 + 412) = v37;
  }
  if ( *(_BYTE *)(v77 + 536) != 0 )
  {
    HIWORD(v38) = HIWORD(v77);
    LOWORD(v38) = *(_WORD *)(v77 + 414);
    v39 = sub_407B28(v38);
    LOBYTE(v39) = *(_BYTE *)(v77 + 536) + v39;
    v40 = v77;
    LOBYTE(v40) = *(_BYTE *)(v77 + 414);
    v41 = unknown_libname_77(v40, v39);
    *(_WORD *)(v77 + 414) = v41;
  }
  if ( *(_BYTE *)(v77 + 537) != 0 )
  {
    HIWORD(v42) = HIWORD(v77);
    LOWORD(v42) = *(_WORD *)(v77 + 416);
    v43 = sub_407B28(v42);
    LOBYTE(v43) = *(_BYTE *)(v77 + 537) + v43;
    v44 = v77;
    LOBYTE(v44) = *(_BYTE *)(v77 + 416);
    v45 = unknown_libname_77(v44, v43);
    *(_WORD *)(v77 + 416) = v45;
  }
  if ( *(_BYTE *)(v77 + 538) != 0 )
  {
    HIWORD(v46) = HIWORD(v77);
    LOWORD(v46) = *(_WORD *)(v77 + 418);
    v47 = sub_407B28(v46);
    LOBYTE(v47) = *(_BYTE *)(v77 + 538) + v47;
    v48 = v77;
    LOBYTE(v48) = *(_BYTE *)(v77 + 418);
    v49 = unknown_libname_77(v48, v47);
    *(_WORD *)(v77 + 418) = v49;
  }
  if ( *(_BYTE *)(v77 + 539) != 0 )
    *(_BYTE *)(v77 + 622) += *(_BYTE *)(v77 + 539);
  if ( *(_BYTE *)(v77 + 540) != 0 )
    *(_WORD *)(v77 + 424) += *(unsigned __int8 *)(v77 + 540);
  if ( *(_BYTE *)(v77 + 541) != 0 )
    *(_WORD *)(v77 + 426) += *(unsigned __int8 *)(v77 + 541);
  if ( *(_BYTE *)(v77 + 852) == 0 )
    goto LABEL_177;
  if ( *(_BYTE *)(v77 + 852) == 1 || (sub_4BFFD0(v77, 3), *(_BYTE *)(v77 + 852) != 2) )
  {
    sub_4BFFD0(v77, 1);
    if ( *(_BYTE *)(v77 + 852) == 3 )
      goto LABEL_179;
LABEL_177:
    sub_4C01B8(v77, 3);
    if ( *(_BYTE *)(v77 + 852) == 1 )
      goto LABEL_179;
  }
  sub_4C01B8(v77, 1);
LABEL_179:
  if ( *(_BYTE *)(v77 + 853) == 0 )
    goto LABEL_183;
  if ( *(_BYTE *)(v77 + 853) == 1 || (sub_4BFFD0(v77, 4), *(_BYTE *)(v77 + 853) != 2) )
  {
    sub_4BFFD0(v77, 2);
    if ( *(_BYTE *)(v77 + 853) == 3 )
      goto LABEL_105;
LABEL_183:
    sub_4C01B8(v77, 4);
    if ( *(_BYTE *)(v77 + 853) == 1 )
      goto LABEL_105;
  }
  sub_4C01B8(v77, 2);
LABEL_105:
  if ( *(_BYTE *)(v77 + 856) != 0 )
  {
    *(_WORD *)(v77 + 442) *= 2;
    *(_BYTE *)(v77 + 445) *= 2;
    *(_BYTE *)(v77 + 447) *= 2;
  }
  if ( *(int *)(v77 + 932) > 0 )
  {
    if ( *(unsigned __int16 *)(v77 + 426) <= *(int *)(v77 + 932) )
      *(_DWORD *)(v77 + 932) = *(unsigned __int16 *)(v77 + 426) - 1;
    *(_WORD *)(v77 + 426) -= *(_WORD *)(v77 + 932);
    *(_WORD *)(v77 + 424) += *(_WORD *)(v77 + 932);
  }
  if ( *(_BYTE *)(v77 + 570) == 0 )
    sub_4B8A7C(0, 0, 0, *(_DWORD *)(v77 + 92), *(char *)(v77 + 622));
  result = v77;
  if ( *(_BYTE *)(v77 + 570) >= 0x32u )
    return sub_4BE934(v77);
  return result;
}


// ==================== [0x4C0EBC] sub_4C0EBC (size: 729) ====================
unsigned __int8 *__fastcall sub_4C0EBC(int a1, const void *a2, int a3)
{
  unsigned __int8 *result; // eax
  int v4; // eax
  int v5; // eax
  int v6; // eax
  char v7; // al
  char v8; // al
  unsigned __int8 v9; // al
  int v10; // eax
  char v11; // al
  int v12; // eax
  unsigned __int8 v13; // al
  int v14; // eax
  unsigned __int8 v15; // al
  int v16; // eax
  unsigned __int8 v17; // al
  int v18; // eax
  int v19; // eax
  int v20; // ebx
  int v21; // eax
  __int16 v22; // ax
  int v23; // eax
  int v24; // eax
  int v25; // ebx
  int v26; // eax
  __int16 v27; // ax
  int v28; // eax
  int v29; // eax
  int v30; // ebx
  int v31; // eax
  __int16 v32; // ax
  int v33; // eax
  int v34; // eax
  int v35; // ebx
  int v36; // eax
  __int16 v37; // ax
  int v38; // eax
  int v39; // eax
  int v40; // ebx
  int v41; // eax
  _WORD v42[22]; // [esp+Ch] [ebp-50h] BYREF
  _WORD v43[12]; // [esp+38h] [ebp-24h] BYREF
  unsigned __int8 *v44; // [esp+50h] [ebp-Ch]
  int v45; // [esp+54h] [ebp-8h]
  int v46; // [esp+58h] [ebp-4h]

  qmemcpy(v43, a2, sizeof(v43));
  v45 = a3;
  v46 = a1;
  result = (unsigned __int8 *)sub_4AC2F8(*off_4EA490, v43[2]);
  v44 = result;
  if ( result != nullptr )
  {
    qmemcpy(v42, v44, sizeof(v42));
    sub_495974(*off_4EA324, v43, v42);
    v4 = v44[15] - 5;
    switch ( v44[15] )
    {
      case 5u:
      case 6u:
        LOWORD(v4) = v42[13];
        v5 = (unsigned __int8)sub_407B28(v4);
        *(_WORD *)(v45 + 4) += v5;
        LOWORD(v5) = v42[14];
        v6 = sub_407B28(v5);
        if ( (unsigned __int8)v6 <= 0xAu )
        {
          LOWORD(v6) = v42[14];
          v8 = sub_407B28(v6);
          *(_BYTE *)(v45 + 32) -= v8;
        }
        else
        {
          LOWORD(v6) = v42[14];
          v7 = sub_407B28(v6);
          *(_BYTE *)(v45 + 32) += v7 - 10;
        }
        *(_BYTE *)(v45 + 28) += LOBYTE(v42[13]);
        *(_BYTE *)(v45 + 29) += LOBYTE(v42[14]);
        break;
      case 0x13u:
        LOWORD(v4) = v42[13];
        v9 = sub_407B28(v4);
        *(_WORD *)(v45 + 26) += v9;
        HIWORD(v10) = HIWORD(v45);
        *(_BYTE *)(v45 + 29) += LOBYTE(v42[14]);
        LOWORD(v10) = v42[14];
        v11 = sub_407B28(v10);
        *(_BYTE *)(v45 + 28) += v11;
        break;
      case 0x14u:
      case 0x18u:
        LOWORD(v4) = v42[13];
        v12 = (unsigned __int8)sub_407B28(v4);
        *(_WORD *)(v45 + 4) += v12;
        LOWORD(v12) = v42[14];
        v13 = sub_407B28(v12);
        *(_WORD *)(v45 + 6) += v13;
        break;
      case 0x15u:
        LOWORD(v4) = v42[13];
        v14 = (unsigned __int8)sub_407B28(v4);
        *(_WORD *)(v45 + 22) += v14;
        LOWORD(v14) = v42[14];
        v15 = sub_407B28(v14);
        *(_WORD *)(v45 + 24) += v15;
        *(_BYTE *)(v45 + 32) += LOBYTE(v42[13]);
        *(_BYTE *)(v45 + 32) -= LOBYTE(v42[14]);
        break;
      case 0x17u:
        LOWORD(v4) = v42[13];
        v16 = (unsigned __int8)sub_407B28(v4);
        *(_WORD *)(v45 + 18) += v16;
        LOWORD(v16) = v42[14];
        v17 = sub_407B28(v16);
        *(_WORD *)(v45 + 20) += v17;
        *(_BYTE *)(v45 + 32) += LOBYTE(v42[13]);
        *(_BYTE *)(v45 + 32) -= LOBYTE(v42[14]);
        break;
      default:
        HIWORD(v18) = HIWORD(v45);
        LOWORD(v18) = *(_WORD *)(v45 + 8);
        v19 = sub_407B28(v18);
        v20 = v19;
        LOWORD(v19) = v42[13];
        LOBYTE(v20) = sub_407B28(v19) + v20;
        v21 = v45;
        LOBYTE(v21) = LOBYTE(v42[13]) + *(_BYTE *)(v45 + 8);
        v22 = unknown_libname_77(v21, v20);
        *(_WORD *)(v45 + 8) = v22;
        HIWORD(v23) = HIWORD(v45);
        LOWORD(v23) = *(_WORD *)(v45 + 10);
        v24 = sub_407B28(v23);
        v25 = v24;
        LOWORD(v24) = v42[14];
        LOBYTE(v25) = sub_407B28(v24) + v25;
        v26 = v45;
        LOBYTE(v26) = LOBYTE(v42[14]) + *(_BYTE *)(v45 + 10);
        v27 = unknown_libname_77(v26, v25);
        *(_WORD *)(v45 + 10) = v27;
        break;
    }
    HIWORD(v28) = HIWORD(v45);
    LOWORD(v28) = *(_WORD *)(v45 + 12);
    v29 = sub_407B28(v28);
    v30 = v29;
    LOWORD(v29) = v42[15];
    LOBYTE(v30) = sub_407B28(v29) + v30;
    v31 = v45;
    LOBYTE(v31) = LOBYTE(v42[15]) + *(_BYTE *)(v45 + 12);
    v32 = unknown_libname_77(v31, v30);
    *(_WORD *)(v45 + 12) = v32;
    HIWORD(v33) = HIWORD(v45);
    LOWORD(v33) = *(_WORD *)(v45 + 14);
    v34 = sub_407B28(v33);
    v35 = v34;
    LOWORD(v34) = v42[16];
    LOBYTE(v35) = sub_407B28(v34) + v35;
    v36 = v45;
    LOBYTE(v36) = LOBYTE(v42[16]) + *(_BYTE *)(v45 + 14);
    v37 = unknown_libname_77(v36, v35);
    *(_WORD *)(v45 + 14) = v37;
    HIWORD(v38) = HIWORD(v45);
    LOWORD(v38) = *(_WORD *)(v45 + 16);
    v39 = sub_407B28(v38);
    v40 = v39;
    LOWORD(v39) = v42[17];
    LOBYTE(v40) = sub_407B28(v39) + v40;
    v41 = v45;
    LOBYTE(v41) = LOBYTE(v42[17]) + *(_BYTE *)(v45 + 16);
    result = (unsigned __int8 *)unknown_libname_77(v41, v40);
    *(_WORD *)(v45 + 16) = (_WORD)result;
  }
  return result;
}


// ==================== [0x4C129C] sub_4C129C (size: 414) ====================
int __fastcall sub_4C129C(int a1, _DWORD *a2)
{
  int v2; // ecx
  int v3; // ecx
  int v4; // ecx
  int v5; // ecx
  unsigned int v7[2]; // [esp-Ch] [ebp-20h] BYREF
  int *v8; // [esp-4h] [ebp-18h]
  int v9; // [esp+0h] [ebp-14h] BYREF
  int v10; // [esp+4h] [ebp-10h]
  int v11; // [esp+8h] [ebp-Ch] BYREF
  _DWORD *v12; // [esp+Ch] [ebp-8h]
  int v13; // [esp+10h] [ebp-4h]
  int savedregs; // [esp+14h] [ebp+0h] BYREF

  v11 = 0;
  v10 = 0;
  v9 = 0;
  v12 = a2;
  v13 = a1;
  v8 = &savedregs;
  v7[1] = (unsigned int)&loc_4C142F;
  v7[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v7);
  if ( *(_BYTE *)(a1 + 570) != 0 )
  {
    unknown_libname_41(v7[0]);
    sub_4542C4(v10, v12, &v11);
    if ( *(_DWORD *)(v13 + 648) != 0 && *(_BYTE *)(*(_DWORD *)(v13 + 648) + 842) == 0 )
    {
      unknown_libname_41(&str___5[1]);
      System::__linkproc__ LStrCatN(v12, 4, v2, v9, &str___5[1]);
    }
  }
  else
  {
    unknown_libname_41(v7[0]);
    if ( *(_DWORD *)(v13 + 800) != 0 )
    {
      if ( (unsigned __int8)sub_490400(*off_4EA080[0], *(_DWORD *)(v13 + 800)) != 0 )
        System::__linkproc__ LStrCatN(v12, 6, v4, *(_DWORD *)(*off_4EA080[0] + 24), &str___5[1]);
      else
        System::__linkproc__ LStrCatN(v12, 3, v4, &str___43[1], *(_DWORD *)(*(_DWORD *)(v13 + 800) + 4));
    }
    if ( *(_BYTE *)(v13 + 53) != 0 )
    {
      if ( *(_BYTE *)(v13 + 47) != 0 )
      {
        if ( *(_BYTE *)(v13 + 47) == 1 )
          System::__linkproc__ LStrCatN(v12, 2, v3, &byte_50A017, *v12);
        else
          System::__linkproc__ LStrCatN(v12, 2, v3, &byte_50A027, *v12);
      }
      else
      {
        System::__linkproc__ LStrCatN(v12, 2, v3, &byte_50A007, *v12);
      }
    }
    if ( *(_BYTE *)(v13 + 271) != 0 )
    {
      unknown_libname_41(*v12);
      if ( *(_BYTE *)(v13 + 45) != 0 )
        System::__linkproc__ LStrCatN(v12, 5, v5, v11, aSWife);
      else
        System::__linkproc__ LStrCatN(v12, 5, v5, v11, aSMan);
    }
  }
  __writefsdword(0, v7[0]);
  v8 = (int *)&loc_4C1436;
  return unknown_libname_35(&v9, 3);
}


// ==================== [0x4C167F] sub_4C167F (size: 308) ====================
int __fastcall sub_4C167F(int result)
{
  int v1; // ecx
  int v2; // ecx
  int v3; // ecx
  int v4; // [esp+2Ch] [ebp-14h] BYREF
  int v5; // [esp+38h] [ebp-8h]
  int v6; // [esp+3Ch] [ebp-4h]

  v6 = result;
  if ( *(_BYTE *)(result + 271) != 0 )
  {
    unknown_libname_41();
    v5 = sub_4AE640(*off_4EA490);
    if ( v5 != 0 )
    {
      unknown_libname_41();
      Sysutils::IntToStr(*(_DWORD *)(v5 + 36));
      Sysutils::IntToStr(*(_DWORD *)(v5 + 40));
      System::__linkproc__ LStrCatN(&v4, 8, v2);
      sub_4BB124(v6, v4, 2);
      System::__linkproc__ LStrCatN(&v4, 3, v3);
      return sub_4BB124(v5, v4, 2);
    }
    else
    {
      System::__linkproc__ LStrCatN(&v4, 3, v1);
      return sub_4BB124(v6, v4, 2);
    }
  }
  return result;
}


// ==================== [0x4C17B4] sub_4C17B4 (size: 631) ====================
int __fastcall sub_4C17B4(int a1, int a2)
{
  int v2; // ecx
  int v3; // edx
  int v4; // ecx
  int v5; // ecx
  int v6; // eax
  int v7; // eax
  int v8; // ecx
  __int64 v9; // rax
  unsigned int v11[2]; // [esp+8h] [ebp-6Ch] BYREF
  int *v12; // [esp+10h] [ebp-64h]
  _DWORD v13[2]; // [esp+18h] [ebp-5Ch] BYREF
  _DWORD v14[4]; // [esp+20h] [ebp-54h] BYREF
  _BYTE v15[20]; // [esp+30h] [ebp-44h] BYREF
  _BYTE v16[20]; // [esp+44h] [ebp-30h] BYREF
  int v17; // [esp+58h] [ebp-1Ch] BYREF
  _DWORD v18[2]; // [esp+5Ch] [ebp-18h] BYREF
  int v19; // [esp+64h] [ebp-10h]
  int v20; // [esp+68h] [ebp-Ch]
  int v21; // [esp+6Ch] [ebp-8h]
  int v22; // [esp+70h] [ebp-4h]
  int savedregs; // [esp+74h] [ebp+0h] BYREF

  v18[0] = 0;
  v17 = 0;
  memset(v14, 0, sizeof(v14));
  v13[1] = 0;
  v13[0] = 0;
  v21 = a2;
  v22 = a1;
  v12 = &savedregs;
  v11[1] = (unsigned int)&loc_4C1A1F;
  v11[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v11);
  if ( *(_WORD *)(a1 + 1084) != 0 )
  {
    v19 = *(unsigned __int16 *)(v22 + 1086);
    v20 = System::__linkproc__ ROUND((double)v19 / flt_4C1A2C);
    v19 -= v21;
    if ( v19 > 0 )
    {
      *(_WORD *)(v22 + 1086) = v19;
    }
    else
    {
      v19 = 0;
      v3 = v22;
      *(_WORD *)(v22 + 1086) = 0;
      if ( *(_BYTE *)(v22 + 570) == 0 )
      {
        v18[1] = v22;
        sub_4D0BDC(v22, v22 + 1080);
        unknown_libname_16(v16, &dword_4C1A30);
        LOBYTE(v4) = 18;
        System::__linkproc__ PStrNCat(v16, v22 + 4, v4);
        unknown_libname_16(v15, v16);
        LOBYTE(v5) = 19;
        System::__linkproc__ PStrNCat(v15, dword_4C1A34, v5);
        unknown_libname_41();
        Sysutils::IntToStr(*(_DWORD *)(v22 + 36));
        Sysutils::IntToStr(*(_DWORD *)(v22 + 40));
        unknown_libname_41();
        sub_4AC1AC(*off_4EA490, *(unsigned __int16 *)(v22 + 1084), v14);
        Sysutils::IntToStr(*(_DWORD *)(v22 + 1080));
        v6 = v22;
        LOBYTE(v6) = *(_BYTE *)(v22 + 570) == 0;
        v7 = sub_454558(v6);
        Sysutils::IntToStr(v7);
        System::__linkproc__ LStrCatN(v18, 14, v8);
        sub_4E42F8(v18[0]);
      }
      *(_WORD *)(v22 + 1084) = 0;
      unknown_libname_25(v22, v3, v2);
      sub_4B865C(v22, v22, 10125, 1, v19, *(unsigned __int16 *)(v22 + 1088), 0, 0);
    }
    LODWORD(v9) = System::__linkproc__ ROUND((double)v19 / flt_4C1A2C);
    if ( v20 != v9 )
      sub_4B865C(v22, v22, 10125, 1, *(unsigned __int16 *)(v22 + 1086), *(unsigned __int16 *)(v22 + 1088), 0, 0);
  }
  __writefsdword(0, v11[0]);
  v12 = (int *)&loc_4C1A26;
  unknown_libname_35(v13, 6);
  return unknown_libname_35(&v17, 2);
}


// ==================== [0x4C1EF4] sub_4C1EF4 (size: 2202) ====================
char __fastcall sub_4C1EF4(unsigned int a1, __int16 a2, int a3)
{
  unsigned int v3; // eax
  char v4; // al
  int v5; // ecx
  int v6; // eax
  unsigned int v7; // eax
  char v8; // al
  int v9; // eax
  int v10; // eax
  int v12; // [esp-10h] [ebp-64h]
  int v13; // [esp-10h] [ebp-64h]
  int v14; // [esp-10h] [ebp-64h]
  unsigned int v15[6]; // [esp-Ch] [ebp-60h] BYREF
  int v16; // [esp+Ch] [ebp-48h]
  int v17; // [esp+10h] [ebp-44h]
  unsigned int v18; // [esp+24h] [ebp-30h]
  unsigned int v19; // [esp+28h] [ebp-2Ch]
  unsigned int v20; // [esp+2Ch] [ebp-28h]
  char v21; // [esp+33h] [ebp-21h]
  int v22; // [esp+34h] [ebp-20h]
  int v23; // [esp+38h] [ebp-1Ch]
  int v24; // [esp+3Ch] [ebp-18h]
  int v25; // [esp+40h] [ebp-14h]
  char v26; // [esp+47h] [ebp-Dh]
  int v27; // [esp+48h] [ebp-Ch]
  __int16 v28; // [esp+4Eh] [ebp-6h]
  unsigned int v29; // [esp+50h] [ebp-4h]
  int savedregs; // [esp+54h] [ebp+0h] BYREF

  v27 = a3;
  v28 = a2;
  v29 = a1;
  v26 = 0;
  v15[2] = (unsigned int)&savedregs;
  v15[1] = (unsigned int)&loc_4C2770;
  v15[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v15);
  v21 = 0;
  v23 = 0;
  v25 = 0;
  v24 = 0;
  if ( a3 != 0 )
  {
    v20 = v29 + 408;
    v3 = v29 + 408;
    LOWORD(v3) = *(_WORD *)(v29 + 414);
    v4 = sub_407B28(v3);
    v25 = sub_4C1A50(v29, *(unsigned __int8 *)(v20 + 6), (char)(v4 - *(_BYTE *)(v20 + 6)));
    if ( v28 == 3 && *(_BYTE *)(v29 + 1324) != 0 )
    {
      *(_BYTE *)(v29 + 1324) = 0;
      v25 += *(unsigned __int8 *)(v29 + 573);
      v21 = 1;
    }
    if ( v28 == 7 && *(_BYTE *)(v29 + 1327) != 0 )
    {
      *(_BYTE *)(v29 + 1327) = 0;
      v19 = v29 + 408;
      v17 = 10 * *(unsigned __int8 *)(v29 + 574);
      v6 = System::__linkproc__ ROUND((double)v25 / flt_4C2790 * (double)v17);
      v25 += v6;
      v21 = 1;
    }
  }
  else
  {
    v18 = v29 + 408;
    v7 = v29 + 408;
    LOWORD(v7) = *(_WORD *)(v29 + 414);
    v8 = sub_407B28(v7);
    v25 = sub_4C1A50(v29, *(unsigned __int8 *)(v18 + 6), (char)(v8 - *(_BYTE *)(v18 + 6)));
    if ( v28 == 3 && *(_BYTE *)(v29 + 1324) != 0 )
    {
      *(_BYTE *)(v29 + 1324) = 0;
      v25 += *(unsigned __int8 *)(v29 + 573);
      v21 = 1;
    }
  }
  if ( v28 == 4 )
  {
    v24 = 0;
    if ( *(_BYTE *)(v29 + 570) != 0 )
    {
      v24 = v25;
    }
    else if ( *(_DWORD *)(v29 + 1312) != 0 )
    {
      v17 = *(unsigned __int8 *)(**(_DWORD **)(v29 + 1312) + 44) + 2;
      v16 = *(unsigned __int8 *)(*(_DWORD *)(v29 + 1312) + 6) + 2;
      v24 = System::__linkproc__ ROUND((double)v25 / (double)v17 * (double)v16);
    }
    if ( v24 > 0 )
    {
      sub_4C1C24(&savedregs);
      v5 = v12;
    }
  }
  if ( v28 == 5 )
  {
    v24 = 0;
    if ( *(_BYTE *)(v29 + 570) != 0 )
    {
      v24 = v25;
    }
    else if ( *(_DWORD *)(v29 + 1316) != 0 )
    {
      v17 = *(unsigned __int8 *)(**(_DWORD **)(v29 + 1316) + 44) + 10;
      v16 = *(unsigned __int8 *)(*(_DWORD *)(v29 + 1316) + 6) + 2;
      v24 = System::__linkproc__ ROUND((double)v25 / (double)v17 * (double)v16);
    }
    if ( v24 > 0 )
    {
      sub_4C1CDC(&savedregs);
      v5 = v13;
    }
  }
  if ( v28 == 6 )
  {
    v24 = 0;
    if ( *(_BYTE *)(v29 + 570) != 0 )
      v24 = v25;
    if ( v24 > 0 )
    {
      sub_4C1E5C(&savedregs);
      v5 = v14;
    }
  }
  if ( v27 != 0 )
  {
    if ( (unsigned __int8)unknown_libname_25(v29, v27, v5) != 0 )
    {
      if ( System::__linkproc__ RandInt(*(unsigned __int8 *)(v27 + 620)) >= *(unsigned __int8 *)(v29 + 572) )
        v25 = 0;
    }
    else
    {
      v25 = 0;
    }
    if ( v25 > 0 )
    {
      v25 = sub_4BDD20(v27, v29, v25);
      v23 = System::__linkproc__ RandInt(5) + 2 - *(unsigned __int8 *)(v29 + 478);
    }
    if ( v25 > 0 )
    {
      sub_4BDEB8(v27, v25);
      sub_4B87C4(
        v27,
        10020,
        10101,
        v25,
        *(unsigned __int16 *)(v27 + 420),
        *(unsigned __int16 *)(v27 + 424),
        v29,
        0,
        200);
      if ( *(_BYTE *)(v29 + 844) != 0 && System::__linkproc__ RandInt(*(unsigned __int8 *)(v27 + 586) + 5) == 0 )
        sub_4C35A8(0);
      if ( *(int *)(v29 + 936) > 0 )
      {
        *(double *)(v29 + 944) = (double)v25 / flt_4C2790 * (double)*(int *)(v29 + 936) + *(double *)(v29 + 944);
        if ( *(double *)(v29 + 944) >= flt_4C2794 )
        {
          v22 = System::__linkproc__ TRUNC(*(double *)(v29 + 944));
          *(double *)(v29 + 944) = *(double *)(v29 + 944) - (double)v22;
          sub_4BE3FC(v29, -v22);
        }
      }
      if ( *(_DWORD *)(v29 + 1304) != 0
        && *(_BYTE *)(v27 + 570) >= 0x32u
        && *(_BYTE *)(*(_DWORD *)(v29 + 1304) + 6) < 3u
        && *(_BYTE *)(**(_DWORD **)(v29 + 1304) + *(unsigned __int8 *)(*(_DWORD *)(v29 + 1304) + 6) + 22) <= *(_BYTE *)(v29 + 52) )
      {
        v9 = System::__linkproc__ RandInt(3);
        sub_4C1268(v29, *(_DWORD *)(v29 + 1304), v9 + 1);
        if ( (unsigned __int8)sub_4C7054(v29, *(_DWORD *)(v29 + 1304)) == 0 )
          sub_4B87C4(
            v29,
            v29,
            10123,
            0,
            ***(unsigned __int16 ***)(v29 + 1304),
            *(unsigned __int8 *)(*(_DWORD *)(v29 + 1304) + 6),
            *(_DWORD *)(*(_DWORD *)(v29 + 1304) + 8),
            0,
            3000);
      }
      if ( v21 != 0
        && *(_DWORD *)(v29 + 1308) != 0
        && *(_BYTE *)(v27 + 570) >= 0x32u
        && *(_BYTE *)(*(_DWORD *)(v29 + 1308) + 6) < 3u
        && *(_BYTE *)(**(_DWORD **)(v29 + 1308) + *(unsigned __int8 *)(*(_DWORD *)(v29 + 1308) + 6) + 22) <= *(_BYTE *)(v29 + 52) )
      {
        v10 = System::__linkproc__ RandInt(3);
        sub_4C1268(v29, *(_DWORD *)(v29 + 1308), v10 + 1);
        if ( (unsigned __int8)sub_4C7054(v29, *(_DWORD *)(v29 + 1308)) == 0 )
          sub_4B87C4(
            v29,
            v29,
            10123,
            0,
            ***(unsigned __int16 ***)(v29 + 1308),
            *(unsigned __int8 *)(*(_DWORD *)(v29 + 1308) + 6),
            *(_DWORD *)(*(_DWORD *)(v29 + 1308) + 8),
            0,
            3000);
      }
      if ( v28 == 4
        && *(_DWORD *)(v29 + 1312) != 0
        && *(_BYTE *)(v27 + 570) >= 0x32u
        && *(_BYTE *)(*(_DWORD *)(v29 + 1312) + 6) < 3u
        && *(_BYTE *)(**(_DWORD **)(v29 + 1312) + *(unsigned __int8 *)(*(_DWORD *)(v29 + 1312) + 6) + 22) <= *(_BYTE *)(v29 + 52) )
      {
        sub_4C1268(v29, *(_DWORD *)(v29 + 1312), 1);
        if ( (unsigned __int8)sub_4C7054(v29, *(_DWORD *)(v29 + 1312)) == 0 )
          sub_4B8930(
            v29,
            v29,
            10123,
            0,
            ***(unsigned __int16 ***)(v29 + 1312),
            *(unsigned __int8 *)(*(_DWORD *)(v29 + 1312) + 6),
            *(_DWORD *)(*(_DWORD *)(v29 + 1312) + 8),
            0,
            3000);
      }
      if ( v28 == 5
        && *(_DWORD *)(v29 + 1316) != 0
        && *(_BYTE *)(v27 + 570) >= 0x32u
        && *(_BYTE *)(*(_DWORD *)(v29 + 1316) + 6) < 3u
        && *(_BYTE *)(**(_DWORD **)(v29 + 1316) + *(unsigned __int8 *)(*(_DWORD *)(v29 + 1316) + 6) + 22) <= *(_BYTE *)(v29 + 52) )
      {
        sub_4C1268(v29, *(_DWORD *)(v29 + 1316), 1);
        if ( (unsigned __int8)sub_4C7054(v29, *(_DWORD *)(v29 + 1316)) == 0 )
          sub_4B8930(
            v29,
            v29,
            10123,
            0,
            ***(unsigned __int16 ***)(v29 + 1316),
            *(unsigned __int8 *)(*(_DWORD *)(v29 + 1316) + 6),
            *(_DWORD *)(*(_DWORD *)(v29 + 1316) + 8),
            0,
            3000);
      }
      if ( v28 == 7
        && *(_DWORD *)(v29 + 1320) != 0
        && *(_BYTE *)(v27 + 570) >= 0x32u
        && *(_BYTE *)(*(_DWORD *)(v29 + 1320) + 6) < 3u
        && *(_BYTE *)(**(_DWORD **)(v29 + 1320) + *(unsigned __int8 *)(*(_DWORD *)(v29 + 1320) + 6) + 22) <= *(_BYTE *)(v29 + 52) )
      {
        sub_4C1268(v29, *(_DWORD *)(v29 + 1320), 1);
        if ( (unsigned __int8)sub_4C7054(v29, *(_DWORD *)(v29 + 1320)) == 0 )
          sub_4B8930(
            v29,
            v29,
            10123,
            0,
            ***(unsigned __int16 ***)(v29 + 1320),
            *(unsigned __int8 *)(*(_DWORD *)(v29 + 1320) + 6),
            *(_DWORD *)(*(_DWORD *)(v29 + 1320) + 8),
            0,
            3000);
      }
      v26 = 1;
    }
    if ( v23 > 0 && *(_WORD *)(v29 + 1084) != 0 )
      sub_4C17B4(v29, v23);
    if ( *(_BYTE *)(v27 + 570) != 0 )
      sub_4B865C(v27, v27, 10020, v25, *(unsigned __int16 *)(v27 + 420), *(unsigned __int16 *)(v27 + 424), v29, 0);
  }
  __writefsdword(0, v15[0]);
  return v26;
}


// ==================== [0x4C2854] sub_4C2854 (size: 840) ====================
int __cdecl sub_4C2854(int a1)
{
  int v1; // ecx
  int v2; // ecx
  int v3; // ecx
  int v4; // ecx
  int v5; // ecx
  int v6; // ecx
  int v7; // edx
  int v8; // ecx
  unsigned int v10[2]; // [esp-Ch] [ebp-98h] BYREF
  int *v11; // [esp-4h] [ebp-90h]
  int v12; // [esp+Ch] [ebp-80h] BYREF
  int v13; // [esp+10h] [ebp-7Ch] BYREF
  int v14; // [esp+24h] [ebp-68h] BYREF
  int v15; // [esp+2Ch] [ebp-60h] BYREF
  _BYTE v16[24]; // [esp+3Ch] [ebp-50h] BYREF
  _BYTE v17[20]; // [esp+54h] [ebp-38h] BYREF
  int v18; // [esp+68h] [ebp-24h] BYREF
  int v19; // [esp+6Ch] [ebp-20h] BYREF
  int v20[6]; // [esp+70h] [ebp-1Ch] BYREF
  int v21; // [esp+88h] [ebp-4h]
  int savedregs; // [esp+8Ch] [ebp+0h] BYREF

  v11 = &savedregs;
  v10[1] = (unsigned int)&loc_4C2B8E;
  v10[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v10);
  if ( *(_BYTE *)(*(_DWORD *)(a1 - 4) + 1100) != 0 )
  {
    qmemcpy(v20, (const void *)(*(_DWORD *)(a1 - 4) + 1080), sizeof(v20));
    sub_4C27C0(a1);
    if ( *(_WORD *)(*(_DWORD *)(a1 - 4) + 1084) != 0 )
    {
      sub_4BB124(*(_DWORD *)(a1 - 4), aRefineSuccessf, 1);
      v21 = *(_DWORD *)(a1 - 4);
      sub_4D0A10(v21, *(_DWORD *)(a1 - 4) + 1080);
      unknown_libname_16(v17, sub_4C2C00);
      LOBYTE(v4) = 19;
      System::__linkproc__ PStrNCat(v17, *(_DWORD *)(a1 - 4) + 4, v4);
      unknown_libname_16(v16, v17);
      LOBYTE(v5) = 20;
      System::__linkproc__ PStrNCat(v16, dword_4C2BBC, v5);
      unknown_libname_41();
      Sysutils::IntToStr(*(_DWORD *)(*(_DWORD *)(a1 - 4) + 36));
      Sysutils::IntToStr(*(_DWORD *)(*(_DWORD *)(a1 - 4) + 40));
      unknown_libname_41();
      sub_4AC1AC(*off_4EA490, *(unsigned __int16 *)(*(_DWORD *)(a1 - 4) + 1084), &v13);
      Sysutils::IntToStr(*(_DWORD *)(*(_DWORD *)(a1 - 4) + 1080));
      System::__linkproc__ LStrCatN(&v14, 13, v6);
      sub_4E42F8(v14);
      unknown_libname_25(*(_DWORD *)(a1 - 4), v7, v8);
      sub_4B865C(*(_DWORD *)(a1 - 4), *(_DWORD *)(a1 - 4), 10051, 0, 0, 0, 0, 0);
      sub_4B865C(*(_DWORD *)(a1 - 4), *(_DWORD *)(a1 - 4), 10302, 0, 0, 0, 0, 0);
    }
    else
    {
      sub_4BB124(*(_DWORD *)(a1 - 4), &str_The_weapon_brok[1], 0);
      v21 = *(_DWORD *)(a1 - 4);
      sub_4D0BDC(v21, v20);
      sub_4B8EBC(*(_DWORD *)(a1 - 4), 10413, 0, 0, 0, 0, 0);
      unknown_libname_16(v17, &dword_4C2BB8);
      LOBYTE(v1) = 19;
      System::__linkproc__ PStrNCat(v17, *(_DWORD *)(a1 - 4) + 4, v1);
      unknown_libname_16(v16, v17);
      LOBYTE(v2) = 20;
      System::__linkproc__ PStrNCat(v16, dword_4C2BBC, v2);
      unknown_libname_41();
      Sysutils::IntToStr(*(_DWORD *)(*(_DWORD *)(a1 - 4) + 36));
      Sysutils::IntToStr(*(_DWORD *)(*(_DWORD *)(a1 - 4) + 40));
      unknown_libname_41();
      sub_4AC1AC(*off_4EA490, LOWORD(v20[1]), &v15);
      Sysutils::IntToStr(v20[0]);
      System::__linkproc__ LStrCatN(&v19, 13, v3);
      sub_4E42F8(v19);
      sub_4BA58C(*(_DWORD *)(a1 - 4));
    }
  }
  __writefsdword(0, v10[0]);
  v11 = (int *)&loc_4C2B95;
  unknown_libname_35(&v12, 12);
  return unknown_libname_35(&v18, 2);
}


// ==================== [0x4C2C50] sub_4C2C50 (size: 443) ====================
int __fastcall sub_4C2C50(unsigned int a1, int a2, __int16 a3, char a4)
{
  int v4; // eax
  int v5; // ecx
  int v7; // [esp+10h] [ebp-14h]
  __int16 v8; // [esp+1Ah] [ebp-Ah]
  int savedregs; // [esp+24h] [ebp+0h] BYREF

  v8 = a3;
  if ( a3 == 5 && *(_DWORD *)(a1 + 1316) != 0 )
  {
    if ( *(_WORD *)(a1 + 422) != 0 )
    {
      v4 = sub_4C2C04(&savedregs);
      sub_4BE50C(a1, *(unsigned __int8 *)(**(_DWORD **)(a1 + 1316) + 52) + v4);
      sub_4C4A24(a1);
    }
    else
    {
      v8 = 10004;
    }
  }
  *(_BYTE *)(a1 + 44) = a4;
  if ( a2 != 0 )
    v7 = a2;
  else
    v7 = sub_4C368C(a1);
  if ( v7 != 0 && *(_WORD *)(a1 + 1084) != 0 )
    sub_4C2854((int)&savedregs);
  if ( sub_4C1EF4(a1, v8, v7) != 0 )
    unknown_libname_25(a1, v7, v5);
  return sub_4C2E0C(*(_DWORD *)(a1 + 40), *(_DWORD *)(a1 + 36));
}


// ==================== [0x4C2E40] sub_4C2E40 (size: 334) ====================
int __fastcall sub_4C2E40(int a1, int a2, int a3, int a4, int a5)
{
  char v5; // al
  int v6; // edx
  int v7; // ecx
  int v9; // [esp+4h] [ebp-20h]
  int v10; // [esp+8h] [ebp-1Ch]
  System::TObject *v11; // [esp+Ch] [ebp-18h]
  int v12; // [esp+10h] [ebp-14h]
  int v13; // [esp+10h] [ebp-14h]
  int v14; // [esp+14h] [ebp-10h]

  v5 = sub_4B2C38(*(_DWORD *)(a1 + 36), *(_DWORD *)(a1 + 40), *(_DWORD *)(a2 + 36), *(_DWORD *)(a2 + 40));
  v6 = a1;
  *(_BYTE *)(a1 + 44) = v5;
  LOBYTE(v6) = 1;
  v11 = (System::TObject *)unknown_libname_22(off_40FAFC, v6);
  sub_4B58F8(v11, 1);
  if ( *((_DWORD *)v11 + 2) - 1 >= 0 )
  {
    v9 = *((_DWORD *)v11 + 2);
    v14 = 0;
    do
    {
      v10 = Classes::TList::Get(v11, v14);
      if ( (unsigned __int8)unknown_libname_25(a1, v10, v7) != 0 )
      {
        v12 = sub_4BDD20(v10, a1, a3);
        v13 = sub_4BDDEC(v10, a1, a4) + v12;
        if ( v13 > 0 )
        {
          sub_4BDEB8(v10, v13);
          sub_4B87C4(
            v10,
            10020,
            10101,
            v13,
            *(unsigned __int16 *)(v10 + 420),
            *(unsigned __int16 *)(v10 + 424),
            a1,
            0,
            200);
        }
      }
      ++v14;
      --v9;
    }
    while ( v9 != 0 );
  }
  System::TObject::Free(v11);
  return sub_4B8EBC(a1, 10004, *(unsigned __int8 *)(a1 + 44), *(_DWORD *)(a1 + 36), *(_DWORD *)(a1 + 40), 0, 0);
}


// ==================== [0x4C3130] sub_4C3130 (size: 1005) ====================
char __fastcall sub_4C3130(int a1, int a2, int a3)
{
  int v3; // ecx
  int v4; // eax
  int v5; // eax
  int v6; // eax
  int v7; // eax
  int v9; // [esp+0h] [ebp-3Ch]
  int v10; // [esp+0h] [ebp-3Ch]
  char v11; // [esp+7h] [ebp-35h]
  int v12; // [esp+8h] [ebp-34h]
  int v13; // [esp+Ch] [ebp-30h]
  int v14; // [esp+10h] [ebp-2Ch]
  int v15; // [esp+14h] [ebp-28h]
  int v16; // [esp+18h] [ebp-24h]
  int v17; // [esp+1Ch] [ebp-20h]
  int v18; // [esp+1Ch] [ebp-20h]
  int v19; // [esp+20h] [ebp-1Ch] BYREF
  int v20; // [esp+24h] [ebp-18h] BYREF
  int v21; // [esp+28h] [ebp-14h]
  char v22; // [esp+2Fh] [ebp-Dh]
  int v23; // [esp+30h] [ebp-Ch]
  int v24; // [esp+34h] [ebp-8h]
  int v25; // [esp+38h] [ebp-4h]
  int savedregs; // [esp+3Ch] [ebp+0h] BYREF

  v24 = a3;
  v23 = a2;
  v25 = a1;
  v22 = 0;
  v11 = 1;
  *(_BYTE *)(a1 + 44) = a2;
  v12 = 0;
  v16 = v24 + 1;
  v15 = v24 + 1;
  if ( sub_4C368C(v25) != 0 )
  {
    v4 = Ibutils::Max((Ibutils *)2, v24 + 1, v3);
    if ( v4 >= 0 )
    {
      v9 = v4 + 1;
      v21 = 0;
      do
      {
        v14 = sub_4C368C(v25);
        if ( v14 != 0 )
        {
          v15 = 0;
          if ( (unsigned __int8)sub_4C30B0(&savedregs) == 0 )
            break;
          if ( v24 >= 3
            && sub_4B2A6C(
                 *(_DWORD *)(v25 + 760),
                 *(_DWORD *)(v25 + 36),
                 *(_DWORD *)(v25 + 40),
                 *(unsigned __int8 *)(v25 + 44),
                 2,
                 &v20,
                 &v19) )
          {
            v13 = sub_4B5838(1);
            if ( v13 != 0 && (unsigned __int8)sub_4C30B0(&savedregs) != 0 )
              sub_4C2F90(v13, *(unsigned __int8 *)(v25 + 44), 1);
          }
          v12 = v14;
          if ( sub_4C2F90(v14, *(unsigned __int8 *)(v25 + 44), 1) != 1 )
            break;
          sub_4B2790(v25, &v20, &v19);
          if ( sub_4B612C(*(_DWORD *)(v25 + 760), *(_DWORD *)(v25 + 36), *(_DWORD *)(v25 + 40), v25, v20, v19, 0) > 0 )
          {
            *(_DWORD *)(v25 + 36) = v20;
            *(_DWORD *)(v25 + 40) = v19;
            sub_4B8EBC(v25, 10015, v23, *(_DWORD *)(v25 + 36), *(_DWORD *)(v25 + 40), 0, 0);
            v11 = 0;
            v22 = 1;
          }
          --v16;
        }
        ++v21;
        --v9;
      }
      while ( v9 != 0 );
    }
  }
  else
  {
    v11 = 0;
    v5 = Ibutils::Max((Ibutils *)2, v24 + 1, v3);
    if ( v5 >= 0 )
    {
      v10 = v5 + 1;
      v21 = 0;
      while ( 1 )
      {
        sub_4B2790(v25, &v20, &v19);
        if ( sub_4B612C(*(_DWORD *)(v25 + 760), *(_DWORD *)(v25 + 36), *(_DWORD *)(v25 + 40), v25, v20, v19, 0) <= 0 )
          break;
        *(_DWORD *)(v25 + 36) = v20;
        *(_DWORD *)(v25 + 40) = v19;
        sub_4B8EBC(v25, 10015, v23, *(_DWORD *)(v25 + 36), *(_DWORD *)(v25 + 40), 0, 0);
        --v15;
        ++v21;
        if ( --v10 == 0 )
          goto LABEL_25;
      }
      if ( (unsigned __int8)sub_4B5ED0(1) != 0 )
        v15 = 0;
      else
        v11 = 1;
    }
  }
LABEL_25:
  if ( v12 != 0 )
  {
    if ( v16 < 0 )
      v16 = 0;
    v6 = System::__linkproc__ RandInt(5 * (v16 + 1));
    v17 = sub_4BDD20(v12, v25, 4 * (v16 + 1) + v6);
    sub_4BDEB8(v12, v17);
    sub_4B8EBC(v12, 10020, v17, *(unsigned __int16 *)(v12 + 420), *(unsigned __int16 *)(v12 + 424), v25, 0);
    if ( *(_BYTE *)(v12 + 570) != 0 )
      sub_4B865C(v12, v12, 10020, v17, *(unsigned __int16 *)(v12 + 420), *(unsigned __int16 *)(v12 + 424), v25, 0);
  }
  if ( v11 != 0 )
  {
    sub_4B2790(v25, &v20, &v19);
    sub_4B8EBC(v25, 10016, *(unsigned __int8 *)(v25 + 44), v20, v19, 0, 0);
    sub_4BB124(v25, aTooWeak, 0);
  }
  if ( v15 > 0 )
  {
    if ( v16 < 0 )
      v16 = 0;
    v7 = System::__linkproc__ RandInt(5 * (v16 + 1));
    v18 = sub_4BDD20(v25, v25, 5 * (v16 + 1) + v7);
    sub_4BDEB8(v25, v18);
    sub_4B8EBC(v25, 10020, v18, *(unsigned __int16 *)(v25 + 420), *(unsigned __int16 *)(v25 + 424), 0, 0);
  }
  return v22;
}


// ==================== [0x4C37C0] sub_4C37C0 (size: 328) ====================
void *__fastcall sub_4C37C0(int a1, int a2, int a3, int a4, int a5)
{
  void *v5; // edx
  int v6; // ecx
  int v7; // eax
  char v8; // cf
  int v9; // eax
  unsigned int v11[2]; // [esp-Ch] [ebp-2Ch] BYREF
  int *v12; // [esp-4h] [ebp-24h]
  void *v13; // [esp+4h] [ebp-1Ch]
  int v14; // [esp+8h] [ebp-18h] BYREF
  int v15; // [esp+Ch] [ebp-14h] BYREF
  void *v16; // [esp+10h] [ebp-10h]
  int v17; // [esp+14h] [ebp-Ch]
  int v18; // [esp+18h] [ebp-8h] BYREF
  int v19; // [esp+1Ch] [ebp-4h]
  int savedregs; // [esp+20h] [ebp+0h] BYREF

  v17 = a3;
  v18 = a2;
  v19 = a1;
  unknown_libname_44(a2);
  v12 = &savedregs;
  v11[1] = (unsigned int)&loc_4C38F7;
  v11[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v11);
  v16 = nullptr;
  if ( *(_DWORD *)(*(_DWORD *)(v19 + 668) + 8) < a4 )
  {
    sub_4B2790(v19, &v15, &v14);
    v13 = (void *)sub_4ADE3C(v18, v14);
    if ( v13 != nullptr )
    {
      *((_DWORD *)v13 + 162) = v19;
      *((_DWORD *)v13 + 163) = 1000 * a5 + GetTickCount();
      *((_BYTE *)v13 + 665) = v17;
      v5 = v13;
      *((_BYTE *)v13 + 664) = v17;
      unknown_libname_25(v13, v5, v6);
      if ( *((_WORD *)v13 + 210) < *((_WORD *)v13 + 212) )
      {
        v7 = *((unsigned __int16 *)v13 + 212) - *((unsigned __int16 *)v13 + 210);
        v8 = v7 & 1;
        v9 = v7 >> 1;
        if ( v9 < 0 )
          LOWORD(v9) = v8 + (_WORD)v9;
        *((_WORD *)v13 + 210) += v9;
      }
      sub_4BF124(v13);
      Classes::TList::Add(*(Classes::TList **)(v19 + 668), v13);
      v16 = v13;
    }
  }
  __writefsdword(0, v11[0]);
  v12 = (int *)&loc_4C38FE;
  unknown_libname_34(&v18);
  return v16;
}


// ==================== [0x4C3CA0] sub_4C3CA0 (size: 456) ====================
char __fastcall sub_4C3CA0(int a1, int a2, _BYTE *a3)
{
  char v4; // [esp+3h] [ebp-Dh]

  v4 = 0;
  if ( *(_DWORD *)(a1 + 36) - 1 <= *(_DWORD *)(a2 + 36)
    && *(_DWORD *)(a1 + 36) + 1 >= *(_DWORD *)(a2 + 36)
    && *(_DWORD *)(a1 + 40) - 1 <= *(_DWORD *)(a2 + 40)
    && *(_DWORD *)(a1 + 40) + 1 >= *(_DWORD *)(a2 + 40)
    && (*(_DWORD *)(a2 + 36) != *(_DWORD *)(a1 + 36) || *(_DWORD *)(a2 + 40) != *(_DWORD *)(a1 + 40)) )
  {
    v4 = 1;
    if ( *(_DWORD *)(a1 + 36) - 1 == *(_DWORD *)(a2 + 36) && *(_DWORD *)(a2 + 40) == *(_DWORD *)(a1 + 40) )
    {
      *a3 = 6;
    }
    else if ( *(_DWORD *)(a1 + 36) + 1 == *(_DWORD *)(a2 + 36) && *(_DWORD *)(a2 + 40) == *(_DWORD *)(a1 + 40) )
    {
      *a3 = 2;
    }
    else if ( *(_DWORD *)(a2 + 36) == *(_DWORD *)(a1 + 36) && *(_DWORD *)(a1 + 40) - 1 == *(_DWORD *)(a2 + 40) )
    {
      *a3 = 0;
    }
    else if ( *(_DWORD *)(a2 + 36) == *(_DWORD *)(a1 + 36) && *(_DWORD *)(a1 + 40) + 1 == *(_DWORD *)(a2 + 40) )
    {
      *a3 = 4;
    }
    else if ( *(_DWORD *)(a1 + 36) - 1 == *(_DWORD *)(a2 + 36) && *(_DWORD *)(a1 + 40) - 1 == *(_DWORD *)(a2 + 40) )
    {
      *a3 = 7;
    }
    else if ( *(_DWORD *)(a1 + 36) + 1 == *(_DWORD *)(a2 + 36) && *(_DWORD *)(a1 + 40) - 1 == *(_DWORD *)(a2 + 40) )
    {
      *a3 = 1;
    }
    else if ( *(_DWORD *)(a1 + 36) - 1 == *(_DWORD *)(a2 + 36) && *(_DWORD *)(a1 + 40) + 1 == *(_DWORD *)(a2 + 40) )
    {
      *a3 = 5;
    }
    else if ( *(_DWORD *)(a1 + 36) + 1 == *(_DWORD *)(a2 + 36) && *(_DWORD *)(a1 + 40) + 1 == *(_DWORD *)(a2 + 40) )
    {
      *a3 = 3;
    }
    else
    {
      *a3 = 0;
    }
  }
  return v4;
}


// ==================== [0x4C3F64] sub_4C3F64 (size: 822) ====================
char __fastcall sub_4C3F64(int a1, int a2, char a3)
{
  unsigned int v4[7]; // [esp-Ch] [ebp-48h] BYREF
  bool v5; // [esp+13h] [ebp-29h]
  int v6; // [esp+14h] [ebp-28h]
  int v7; // [esp+18h] [ebp-24h] BYREF
  int v8; // [esp+1Ch] [ebp-20h] BYREF
  int v9; // [esp+20h] [ebp-1Ch]
  int v10; // [esp+24h] [ebp-18h]
  int v11; // [esp+28h] [ebp-14h]
  int v12; // [esp+2Ch] [ebp-10h]
  char v13; // [esp+32h] [ebp-Ah]
  char v14; // [esp+33h] [ebp-9h]
  int v15; // [esp+34h] [ebp-8h]
  int v16; // [esp+38h] [ebp-4h]
  int savedregs; // [esp+3Ch] [ebp+0h] BYREF

  v14 = a3;
  v15 = a2;
  v16 = a1;
  v13 = 0;
  if ( *(_BYTE *)(a1 + 716) == 0 )
  {
    v4[2] = (unsigned int)&savedregs;
    v4[1] = (unsigned int)&loc_4C427C;
    v4[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
    __writefsdword(0, (unsigned int)v4);
    v12 = *(_DWORD *)(v16 + 36);
    v11 = *(_DWORD *)(v16 + 40);
    v6 = *(_DWORD *)(v16 + 760);
    *(_BYTE *)(v16 + 44) = v15;
    v10 = 0;
    v9 = 0;
    switch ( v15 )
    {
      case 0:
        v10 = *(_DWORD *)(v16 + 36);
        v9 = *(_DWORD *)(v16 + 40) - 1;
        break;
      case 1:
        v10 = *(_DWORD *)(v16 + 36) + 1;
        v9 = *(_DWORD *)(v16 + 40) - 1;
        break;
      case 2:
        v10 = *(_DWORD *)(v16 + 36) + 1;
        v9 = *(_DWORD *)(v16 + 40);
        break;
      case 3:
        v10 = *(_DWORD *)(v16 + 36) + 1;
        v9 = *(_DWORD *)(v16 + 40) + 1;
        break;
      case 4:
        v10 = *(_DWORD *)(v16 + 36);
        v9 = *(_DWORD *)(v16 + 40) + 1;
        break;
      case 5:
        v10 = *(_DWORD *)(v16 + 36) - 1;
        v9 = *(_DWORD *)(v16 + 40) + 1;
        break;
      case 6:
        v10 = *(_DWORD *)(v16 + 36) - 1;
        v9 = *(_DWORD *)(v16 + 40);
        break;
      case 7:
        v10 = *(_DWORD *)(v16 + 36) - 1;
        v9 = *(_DWORD *)(v16 + 40) - 1;
        break;
      default:
        break;
    }
    if ( v10 >= 0
      && *(_DWORD *)(*(_DWORD *)(v16 + 760) + 28) - 1 >= v10
      && v9 >= 0
      && *(_DWORD *)(*(_DWORD *)(v16 + 760) + 32) - 1 >= v9 )
    {
      v5 = true;
      if ( *(_BYTE *)(v16 + 698) != 0 )
        v5 = (unsigned __int8)sub_4B609C(*(_DWORD *)(v16 + 760), v10, v9) != 0;
      if ( *(_DWORD *)(v16 + 648) != 0 )
      {
        sub_4B2A6C(
          *(_DWORD *)(*(_DWORD *)(v16 + 648) + 760),
          *(_DWORD *)(*(_DWORD *)(v16 + 648) + 36),
          *(_DWORD *)(*(_DWORD *)(v16 + 648) + 40),
          *(unsigned __int8 *)(*(_DWORD *)(v16 + 648) + 44),
          1,
          &v8,
          &v7);
        if ( v10 == v8 && v9 == v7 )
          v5 = false;
      }
      if ( v5 && sub_4B612C(*(_DWORD *)(v16 + 760), *(_DWORD *)(v16 + 36), *(_DWORD *)(v16 + 40), v16, v10, v9, v14) > 0 )
      {
        *(_DWORD *)(v16 + 36) = v10;
        *(_DWORD *)(v16 + 40) = v9;
      }
    }
    if ( *(_DWORD *)(v16 + 36) != v12 || *(_DWORD *)(v16 + 40) != v11 )
    {
      if ( sub_4BA724(v16, 10002) != 0 )
      {
        if ( *(_BYTE *)(v16 + 840) != 0 && *(_BYTE *)(v16 + 836) != 0 )
          *(_WORD *)(v16 + 112) = 1;
        v13 = 1;
      }
      else
      {
        sub_4B6710(*(_DWORD *)(v16 + 760), *(_DWORD *)(v16 + 36), *(_DWORD *)(v16 + 40), 1, v16);
        *(_DWORD *)(v16 + 760) = v6;
        *(_DWORD *)(v16 + 36) = v12;
        *(_DWORD *)(v16 + 40) = v11;
        sub_4B63E4(*(_DWORD *)(v16 + 760), *(_DWORD *)(v16 + 36), *(_DWORD *)(v16 + 40), 1, v16);
      }
    }
    __writefsdword(0, v4[0]);
  }
  return v13;
}


// ==================== [0x4C42C0] sub_4C42C0 (size: 1705) ====================
char __fastcall sub_4C42C0(int *a1, int a2)
{
  unsigned int v3[6]; // [esp-Ch] [ebp-2Ch] BYREF
  int v4; // [esp+Ch] [ebp-14h]
  int v5; // [esp+10h] [ebp-10h]
  char v6; // [esp+17h] [ebp-9h]
  int v7; // [esp+18h] [ebp-8h]
  int *v8; // [esp+1Ch] [ebp-4h]
  int savedregs; // [esp+20h] [ebp+0h] BYREF

  v7 = a2;
  v8 = a1;
  v6 = 0;
  v3[2] = (unsigned int)&savedregs;
  v3[1] = (unsigned int)&loc_4C494B;
  v3[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v3);
  v5 = a1[9];
  v4 = a1[10];
  *((_BYTE *)a1 + 44) = a2;
  switch ( v7 )
  {
    case 0:
      if ( v8[10] > 1
        && (unsigned __int8)sub_4B5ED0(1) != 0
        && (unsigned __int8)sub_4B5ED0(1) != 0
        && sub_4B612C(v8[190], v8[9], v8[10], (int)v8, v8[9], v8[10] - 2, 1) > 0 )
      {
        v8[10] -= 2;
      }
      break;
    case 1:
      if ( v8[9] < *(_DWORD *)(v8[190] + 28) - 2
        && v8[10] > 1
        && (unsigned __int8)sub_4B5ED0(1) != 0
        && (unsigned __int8)sub_4B5ED0(1) != 0
        && sub_4B612C(v8[190], v8[9], v8[10], (int)v8, v8[9] + 2, v8[10] - 2, 1) > 0 )
      {
        v8[9] += 2;
        v8[10] -= 2;
      }
      break;
    case 2:
      if ( v8[9] < *(_DWORD *)(v8[190] + 28) - 2
        && (unsigned __int8)sub_4B5ED0(1) != 0
        && (unsigned __int8)sub_4B5ED0(1) != 0
        && sub_4B612C(v8[190], v8[9], v8[10], (int)v8, v8[9] + 2, v8[10], 1) > 0 )
      {
        v8[9] += 2;
      }
      break;
    case 3:
      if ( v8[9] < *(_DWORD *)(v8[190] + 28) - 2
        && v8[10] < *(_DWORD *)(v8[190] + 32) - 2
        && (unsigned __int8)sub_4B5ED0(1) != 0
        && (unsigned __int8)sub_4B5ED0(1) != 0
        && sub_4B612C(v8[190], v8[9], v8[10], (int)v8, v8[9] + 2, v8[10] + 2, 1) > 0 )
      {
        v8[9] += 2;
        v8[10] += 2;
      }
      break;
    case 4:
      if ( v8[10] < *(_DWORD *)(v8[190] + 32) - 2
        && (unsigned __int8)sub_4B5ED0(1) != 0
        && (unsigned __int8)sub_4B5ED0(1) != 0
        && sub_4B612C(v8[190], v8[9], v8[10], (int)v8, v8[9], v8[10] + 2, 1) > 0 )
      {
        v8[10] += 2;
      }
      break;
    case 5:
      if ( v8[9] > 1
        && v8[10] < *(_DWORD *)(v8[190] + 32) - 2
        && (unsigned __int8)sub_4B5ED0(1) != 0
        && (unsigned __int8)sub_4B5ED0(1) != 0
        && sub_4B612C(v8[190], v8[9], v8[10], (int)v8, v8[9] - 2, v8[10] + 2, 1) > 0 )
      {
        v8[9] -= 2;
        v8[10] += 2;
      }
      break;
    case 6:
      if ( v8[9] > 1
        && (unsigned __int8)sub_4B5ED0(1) != 0
        && (unsigned __int8)sub_4B5ED0(1) != 0
        && sub_4B612C(v8[190], v8[9], v8[10], (int)v8, v8[9] - 2, v8[10], 1) > 0 )
      {
        v8[9] -= 2;
      }
      break;
    case 7:
      if ( v8[9] > 1
        && v8[10] > 1
        && (unsigned __int8)sub_4B5ED0(1) != 0
        && (unsigned __int8)sub_4B5ED0(1) != 0
        && sub_4B612C(v8[190], v8[9], v8[10], (int)v8, v8[9] - 2, v8[10] - 2, 1) > 0 )
      {
        v8[9] -= 2;
        v8[10] -= 2;
      }
      break;
    default:
      break;
  }
  if ( v8[9] != v5 || v8[10] != v4 )
  {
    if ( sub_4BA724((int)v8, 10003) != 0 )
    {
      v6 = 1;
    }
    else
    {
      v8[9] = v5;
      v8[10] = v4;
      sub_4B612C(v8[190], v5, v4, (int)v8, v8[9], v8[10], 1);
    }
  }
  __writefsdword(0, v3[0]);
  return v6;
}


// ==================== [0x4C4B78] sub_4C4B78 (size: 346) ====================
int __fastcall sub_4C4B78(int a1, int a2, _DWORD *a3, _DWORD *a4, int *a5)
{
  int v5; // eax
  unsigned int v7[2]; // [esp-Ch] [ebp-2Ch] BYREF
  int *v8; // [esp-4h] [ebp-24h]
  int v9; // [esp+4h] [ebp-1Ch] BYREF
  int v10; // [esp+8h] [ebp-18h]
  int v11; // [esp+Ch] [ebp-14h]
  int v12; // [esp+10h] [ebp-10h]
  _DWORD *v13; // [esp+14h] [ebp-Ch]
  int v14; // [esp+18h] [ebp-8h] BYREF
  int v15; // [esp+1Ch] [ebp-4h]
  int savedregs; // [esp+20h] [ebp+0h] BYREF

  v9 = 0;
  v13 = a3;
  v14 = a2;
  v15 = a1;
  unknown_libname_44(a2);
  v8 = &savedregs;
  v7[1] = (unsigned int)&loc_4C4CC1;
  v7[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v7);
  v12 = 0;
  *a4 = 0;
  *a5 = 0;
  *v13 = 0;
  if ( *(_DWORD *)(*(_DWORD *)(v15 + 1036) + 8) - 1 >= 0 )
  {
    v10 = *(_DWORD *)(*(_DWORD *)(v15 + 1036) + 8);
    v11 = 0;
    do
    {
      v5 = Classes::TList::Get(*(Classes::TList **)(v15 + 1036), v11);
      sub_4AC1AC(*off_4EA490, *(unsigned __int16 *)(v5 + 4), &v9);
      if ( unknown_libname_84(v9, v14) == 0 )
      {
        if ( *(unsigned __int16 *)(Classes::TList::Get(*(Classes::TList **)(v15 + 1036), v11) + 6) > *a5 )
        {
          *a5 = *(unsigned __int16 *)(Classes::TList::Get(*(Classes::TList **)(v15 + 1036), v11) + 6);
          v12 = Classes::TList::Get(*(Classes::TList **)(v15 + 1036), v11);
        }
        *a4 += *(unsigned __int16 *)(Classes::TList::Get(*(Classes::TList **)(v15 + 1036), v11) + 6);
        if ( v12 == 0 )
          v12 = Classes::TList::Get(*(Classes::TList **)(v15 + 1036), v11);
        ++*v13;
      }
      ++v11;
      --v10;
    }
    while ( v10 != 0 );
  }
  __writefsdword(0, v7[0]);
  v8 = (int *)&loc_4C4CC8;
  unknown_libname_34(&v9);
  unknown_libname_34(&v14);
  return v12;
}


// ==================== [0x4C5238] sub_4C5238 (size: 312) ====================
char __fastcall sub_4C5238(int a1, int a2, int a3, int a4, int *a5, int *a6)
{
  int v7; // [esp+4h] [ebp-38h]
  int v8; // [esp+8h] [ebp-34h]
  int v9; // [esp+Ch] [ebp-30h]
  int v10; // [esp+10h] [ebp-2Ch]
  int v11; // [esp+14h] [ebp-28h]
  int v12; // [esp+18h] [ebp-24h]
  int v13; // [esp+1Ch] [ebp-20h] BYREF
  int v14; // [esp+20h] [ebp-1Ch]
  int v15; // [esp+24h] [ebp-18h]
  int v16; // [esp+28h] [ebp-14h]
  char v17; // [esp+2Fh] [ebp-Dh]
  int v18; // [esp+30h] [ebp-Ch]
  int v19; // [esp+34h] [ebp-8h]
  int v20; // [esp+38h] [ebp-4h]

  v18 = a3;
  v19 = a2;
  v20 = a1;
  v12 = 999;
  v17 = 0;
  if ( a4 > 0 )
  {
    v9 = a4;
    v14 = 1;
    do
    {
      if ( ((v14 & 0x40000000) != 0) == __OFSUB__(v14, -v14) )
      {
        v8 = 2 * v14 + 1;
        v15 = -v14;
        do
        {
          if ( ((v14 & 0x40000000) != 0) == __OFSUB__(v14, -v14) )
          {
            v7 = 2 * v14 + 1;
            v16 = -v14;
            do
            {
              *a5 = v16 + v19;
              *a6 = v15 + v18;
              if ( sub_4B5C10(&v13) != 0 )
              {
                if ( *(_BYTE *)(*(_DWORD *)(v20 + 760) + 44) != 0 && v12 > v13 )
                {
                  v12 = v13;
                  v11 = *a5;
                  v10 = *a6;
                }
              }
              else if ( *(_BYTE *)(*(_DWORD *)(v20 + 760) + 44) != 0 )
              {
                v17 = 1;
                break;
              }
              ++v16;
              --v7;
            }
            while ( v7 != 0 );
          }
          if ( v17 != 0 )
            break;
          ++v15;
          --v8;
        }
        while ( v8 != 0 );
      }
      if ( v17 != 0 )
        break;
      ++v14;
      --v9;
    }
    while ( v9 != 0 );
  }
  if ( v17 == 0 )
  {
    if ( v12 >= 8 )
    {
      *a5 = v19;
      *a6 = v18;
    }
    else
    {
      *a5 = v11;
      *a6 = v10;
    }
  }
  return v17;
}


// ==================== [0x4C5478] sub_4C5478 (size: 748) ====================
char __fastcall sub_4C5478(int a1, const void *a2, int a3, char a4, int a5, int a6)
{
  int v6; // eax
  int v7; // eax
  int v8; // ecx
  int v10; // [esp-18h] [ebp-88h]
  int v11; // [esp-14h] [ebp-84h]
  int v12; // [esp-10h] [ebp-80h]
  unsigned int v13[2]; // [esp-Ch] [ebp-7Ch] BYREF
  int *v14; // [esp-4h] [ebp-74h]
  _DWORD v15[2]; // [esp+Ch] [ebp-64h] BYREF
  _DWORD v16[5]; // [esp+14h] [ebp-5Ch] BYREF
  int v17; // [esp+28h] [ebp-48h] BYREF
  int v18; // [esp+2Ch] [ebp-44h]
  int v19[6]; // [esp+30h] [ebp-40h] BYREF
  int v20; // [esp+48h] [ebp-28h] BYREF
  int v21; // [esp+4Ch] [ebp-24h]
  int v22; // [esp+50h] [ebp-20h]
  int Mem; // [esp+54h] [ebp-1Ch]
  int v24; // [esp+58h] [ebp-18h]
  int v25; // [esp+5Ch] [ebp-14h] BYREF
  int v26; // [esp+60h] [ebp-10h] BYREF
  char v27; // [esp+67h] [ebp-9h]
  int v28; // [esp+68h] [ebp-8h]
  int v29; // [esp+6Ch] [ebp-4h]
  int savedregs; // [esp+70h] [ebp+0h] BYREF

  v17 = 0;
  memset(v16, 0, sizeof(v16));
  v15[1] = 0;
  v15[0] = 0;
  v18 = 0;
  v20 = 0;
  qmemcpy(v19, a2, sizeof(v19));
  v28 = a3;
  v29 = a1;
  v14 = &savedregs;
  v13[1] = (unsigned int)&loc_4C5751;
  v13[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v13);
  v27 = 0;
  v21 = sub_4AC2F8(*off_4EA490, LOWORD(v19[1]));
  if ( v21 != 0 )
  {
    if ( *(_BYTE *)(v21 + 15) == 40 )
    {
      v24 = HIWORD(v19[1]) - 2000;
      if ( HIWORD(v19[1]) < 0x7D0u )
        v24 = 0;
      HIWORD(v19[1]) = v24;
    }
    Mem = System::__linkproc__ GetMem(60);
    qmemcpy((void *)Mem, v19, 24);
    System::__linkproc__ PStrNCpy(Mem + 24, v21, 14);
    *(_WORD *)(Mem + 40) = *(_WORD *)(v21 + 22);
    if ( *(_BYTE *)(v21 + 15) == 45 )
      *(_WORD *)(Mem + 40) = sub_455EEC(*(unsigned __int16 *)(Mem + 40), *(unsigned __int8 *)(v21 + 16));
    *(_BYTE *)(Mem + 42) = *(_BYTE *)(v21 + 18);
    *(_BYTE *)(Mem + 43) = 0;
    *(_DWORD *)(Mem + 44) = 1;
    *(_DWORD *)(Mem + 48) = a5;
    *(_DWORD *)(Mem + 52) = GetTickCount();
    *(_DWORD *)(Mem + 56) = a6;
    sub_4C5238(v29, *(_DWORD *)(v29 + 36), *(_DWORD *)(v29 + 40), v28, &v26, &v25);
    v22 = sub_4B63E4(*(_DWORD *)(v29 + 760), v26, v25, 2, Mem);
    if ( v22 == Mem )
    {
      v12 = Mem;
      v11 = v26;
      v10 = v25;
      unknown_libname_41();
      sub_4B8EBC(v29, 10110, *(_WORD *)(Mem + 40), v12, v11, v10, v18);
      if ( a4 != 0 )
        unknown_libname_37(&v20, &str_15_[1]);
      else
        unknown_libname_37(&v20, &str_7_[1]);
      if ( (unsigned __int8)sub_4B2FA8(*(unsigned __int8 *)(v21 + 15)) == 0 )
      {
        unknown_libname_41();
        Sysutils::IntToStr(*(_DWORD *)(v29 + 36));
        Sysutils::IntToStr(*(_DWORD *)(v29 + 40));
        unknown_libname_41();
        sub_4AC1AC(*off_4EA490, LOWORD(v19[1]), v16);
        Sysutils::IntToStr(v19[0]);
        v6 = v29;
        LOBYTE(v6) = *(_BYTE *)(v29 + 570) == 0;
        v7 = sub_454558(v6);
        Sysutils::IntToStr(v7);
        System::__linkproc__ LStrCatN(&v17, 16, v8);
        sub_4E42F8(v17);
      }
      v27 = 1;
    }
    else
    {
      System::__linkproc__ FreeMem(Mem, 60);
    }
  }
  __writefsdword(0, v13[0]);
  v14 = (int *)&loc_4C5758;
  unknown_libname_35(v15, 9);
  unknown_libname_34(&v20);
  return v27;
}


// ==================== [0x4C5794] sub_4C5794 (size: 591) ====================
char __fastcall sub_4C5794(int a1, int a2, char a3, int a4, int a5)
{
  int v5; // edi
  __int16 v6; // ax
  int v7; // eax
  int v8; // eax
  int v9; // ecx
  unsigned int v11[2]; // [esp-Ch] [ebp-58h] BYREF
  int *v12; // [esp-4h] [ebp-50h]
  _DWORD v13[6]; // [esp+Ch] [ebp-40h] BYREF
  int v14; // [esp+24h] [ebp-28h] BYREF
  int v15; // [esp+2Ch] [ebp-20h] BYREF
  int v16; // [esp+30h] [ebp-1Ch]
  int Mem; // [esp+34h] [ebp-18h]
  int v18; // [esp+38h] [ebp-14h] BYREF
  int v19; // [esp+3Ch] [ebp-10h] BYREF
  char v20; // [esp+42h] [ebp-Ah]
  char v21; // [esp+43h] [ebp-9h]
  int v22; // [esp+44h] [ebp-8h]
  int v23; // [esp+48h] [ebp-4h]
  int savedregs; // [esp+4Ch] [ebp+0h] BYREF

  v14 = 0;
  memset(v13, 0, sizeof(v13));
  v15 = 0;
  v21 = a3;
  v22 = a2;
  v23 = a1;
  v12 = &savedregs;
  v11[1] = (unsigned int)&loc_4C59D0;
  v11[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v11);
  v20 = 0;
  Mem = System::__linkproc__ GetMem(60);
  System::__linkproc__ FillChar(Mem, 60, 0);
  v5 = Mem + 24;
  *(_DWORD *)(Mem + 24) = *(_DWORD *)aGold_2;
  *(_BYTE *)(v5 + 4) = aGold_2[4];
  *(_DWORD *)(Mem + 44) = v22;
  v6 = sub_455E98(v22);
  *(_WORD *)(Mem + 40) = v6;
  *(_DWORD *)(Mem + 48) = a4;
  *(_DWORD *)(Mem + 52) = GetTickCount();
  *(_DWORD *)(Mem + 56) = a5;
  sub_4C5238(v23, *(_DWORD *)(v23 + 36), *(_DWORD *)(v23 + 40), 3, &v19, &v18);
  v16 = sub_4B63E4(*(_DWORD *)(v23 + 760), v19, v18, 2, Mem);
  if ( v16 != 0 )
  {
    if ( v16 != Mem )
    {
      System::__linkproc__ FreeMem(Mem, 60);
      Mem = v16;
    }
    sub_4B8EBC(v23, 10110, *(_WORD *)(Mem + 40), Mem, v19, v18, (int)&str_Gold_2[1]);
    if ( *(_BYTE *)(v23 + 570) == 0 )
    {
      if ( v21 != 0 )
        unknown_libname_37(&v15, &str_15__0[1]);
      else
        unknown_libname_37(&v15, &str_7__0[1]);
      unknown_libname_41();
      Sysutils::IntToStr(*(_DWORD *)(v23 + 36));
      Sysutils::IntToStr(*(_DWORD *)(v23 + 40));
      unknown_libname_41();
      Sysutils::IntToStr(v22);
      v7 = v23;
      LOBYTE(v7) = *(_BYTE *)(v23 + 570) == 0;
      v8 = sub_454558(v7);
      Sysutils::IntToStr(v8);
      System::__linkproc__ LStrCatN(&v14, 16, v9);
      sub_4E42F8(v14);
    }
    v20 = 1;
  }
  else
  {
    System::__linkproc__ FreeMem(Mem, 60);
  }
  __writefsdword(0, v11[0]);
  v12 = (int *)&loc_4C59D7;
  unknown_libname_35(v13, 7);
  unknown_libname_34(&v15);
  return v20;
}


// ==================== [0x4C5A2C] sub_4C5A2C (size: 374) ====================
char __fastcall sub_4C5A2C(int a1, int a2, int a3)
{
  int v3; // eax
  unsigned int v5[2]; // [esp-Ch] [ebp-38h] BYREF
  int *v6; // [esp-4h] [ebp-30h]
  int v7; // [esp+4h] [ebp-28h] BYREF
  char v8; // [esp+8h] [ebp-24h]
  int v9; // [esp+Ch] [ebp-20h] BYREF
  int v10; // [esp+10h] [ebp-1Ch]
  unsigned __int16 *v11; // [esp+14h] [ebp-18h]
  int v12; // [esp+18h] [ebp-14h]
  char v13; // [esp+1Fh] [ebp-Dh]
  int v14; // [esp+20h] [ebp-Ch]
  int v15; // [esp+24h] [ebp-8h] BYREF
  int v16; // [esp+28h] [ebp-4h]
  int savedregs; // [esp+2Ch] [ebp+0h] BYREF

  v7 = 0;
  v9 = 0;
  v14 = a3;
  v15 = a2;
  v16 = a1;
  unknown_libname_44(a2);
  v6 = &savedregs;
  v5[1] = (unsigned int)&loc_4C5B93;
  v5[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v5);
  v13 = 0;
  if ( System::__linkproc__ LStrPos(&str___74[1], v15) >= 0 )
  {
    v8 = 32;
    sub_453DBC(&v9, 0);
  }
  if ( GetTickCount() - *(_DWORD *)(v16 + 792) > 0xBB8 )
  {
    if ( *(_DWORD *)(*(_DWORD *)(v16 + 1036) + 8) - 1 >= 0 )
    {
      v10 = *(_DWORD *)(*(_DWORD *)(v16 + 1036) + 8);
      v12 = 0;
      while ( 1 )
      {
        v11 = (unsigned __int16 *)Classes::TList::Get(*(Classes::TList **)(v16 + 1036), v12);
        if ( *(_DWORD *)v11 == v14 )
        {
          sub_4AC1AC(*off_4EA490, v11[2], &v7);
          if ( unknown_libname_84(v7, v15) == 0 && sub_4C5478(v16, v11, 1, 0, 0, v16) != 0 )
            break;
        }
        ++v12;
        if ( --v10 == 0 )
          goto LABEL_11;
      }
      v3 = Classes::TList::Get(*(Classes::TList **)(v16 + 1036), v12);
      System::__linkproc__ FreeMem(v3, 24);
      Classes::TList::Delete(*(Classes::TList **)(v16 + 1036), v12);
      v13 = 1;
    }
LABEL_11:
    if ( v13 != 0 )
      sub_4C49BC(v16);
  }
  __writefsdword(0, v5[0]);
  v6 = (int *)&loc_4C5B9A;
  unknown_libname_34(&v7);
  unknown_libname_34(&v9);
  unknown_libname_34(&v15);
  return v13;
}


// ==================== [0x4C5CB0] sub_4C5CB0 (size: 1306) ====================
char __fastcall sub_4C5CB0(int a1)
{
  int v1; // ecx
  int v2; // ecx
  int v3; // ecx
  int v4; // eax
  int v5; // ecx
  int v6; // ecx
  int v7; // ecx
  int v8; // ecx
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-Ch] [ebp-A0h] BYREF
  void *v11; // [esp-8h] [ebp-9Ch]
  int *v12; // [esp-4h] [ebp-98h]
  int v13; // [esp+Ch] [ebp-88h] BYREF
  int v14; // [esp+10h] [ebp-84h] BYREF
  int v15[2]; // [esp+24h] [ebp-70h] BYREF
  _BYTE v16[20]; // [esp+3Ch] [ebp-58h] BYREF
  _BYTE v17[20]; // [esp+50h] [ebp-44h] BYREF
  int v18; // [esp+64h] [ebp-30h] BYREF
  _DWORD v19[3]; // [esp+68h] [ebp-2Ch] BYREF
  int v20; // [esp+74h] [ebp-20h] BYREF
  int v21; // [esp+78h] [ebp-1Ch]
  int v22; // [esp+7Ch] [ebp-18h]
  int v23; // [esp+80h] [ebp-14h]
  _DWORD *v24; // [esp+84h] [ebp-10h]
  unsigned __int16 *Mem; // [esp+88h] [ebp-Ch]
  char v26; // [esp+8Fh] [ebp-5h]
  int v27; // [esp+90h] [ebp-4h]
  int savedregs; // [esp+94h] [ebp+0h] BYREF

  v27 = a1;
  v12 = &savedregs;
  v11 = &loc_4C61B9;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  v26 = 0;
  if ( *(_BYTE *)(a1 + 791) == 0 )
  {
    v24 = (_DWORD *)sub_4B5B0C(
                      *(_DWORD *)(v27 + 760),
                      *(_DWORD *)(v27 + 36),
                      *(_DWORD *)(v27 + 40),
                      v12,
                      v11,
                      ExceptionList);
    if ( v24 != nullptr )
    {
      if ( GetTickCount() - v24[13] > 0x13880 )
        v24[12] = 0;
      if ( (unsigned __int8)sub_4C5C04(&savedregs) != 0 || (unsigned __int8)sub_4C5C30(&savedregs) != 0 )
      {
        unknown_libname_41();
        if ( unknown_libname_84(v19[1], &str_Gold_3[1]) != 0 )
        {
          if ( (unsigned __int8)sub_4C4990(v27) != 0
            && sub_4B6710(*(_DWORD *)(v27 + 760), *(_DWORD *)(v27 + 36), *(_DWORD *)(v27 + 40), 2, (int)v24) == 1 )
          {
            Mem = (unsigned __int16 *)System::__linkproc__ GetMem(24);
            qmemcpy(Mem, v24, 0x18u);
            v23 = sub_4AC2F8(*off_4EA490, Mem[2]);
            if ( v23 != 0 && (v4 = sub_4AC2B0(*off_4EA490, Mem[2]), (unsigned __int8)sub_4C4A78(v27, v4) != 0) )
            {
              sub_4B865C(v27, v27, 10111, 0, (int)v24, *(_DWORD *)(v27 + 36), *(_DWORD *)(v27 + 40), 0);
              sub_4C4DB8(v27, Mem);
              if ( (unsigned __int8)sub_4B6E24(*(_DWORD *)(v27 + 760)) != 0 )
              {
                unknown_libname_34(&v20);
                if ( v24[14] != 0 )
                  unknown_libname_41();
                unknown_libname_41();
                v21 = sub_4B6E4C(*(_DWORD *)(v27 + 760), v27, v20, v15[1], 0);
                if ( v21 != 0 )
                  unknown_libname_25(v21, v27, v5);
              }
              if ( (unsigned __int8)sub_4B2FA8(*(unsigned __int8 *)(v23 + 15)) == 0 )
              {
                unknown_libname_16(v17, &dword_4C61DC);
                LOBYTE(v6) = 18;
                System::__linkproc__ PStrNCat(v17, v27 + 4, v6);
                unknown_libname_16(v16, v17);
                LOBYTE(v7) = 19;
                System::__linkproc__ PStrNCat(v16, dword_4C61E0, v7);
                unknown_libname_41();
                Sysutils::IntToStr(*(_DWORD *)(v27 + 36));
                Sysutils::IntToStr(*(_DWORD *)(v27 + 40));
                unknown_libname_41();
                sub_4AC1AC(*off_4EA490, Mem[2], &v14);
                Sysutils::IntToStr(*(_DWORD *)Mem);
                System::__linkproc__ LStrCatN(v15, 13, v8);
                sub_4E42F8(v15[0]);
              }
              System::__linkproc__ FreeMem(v24, 60);
              if ( *(_BYTE *)(v27 + 570) == 0
                && (unsigned __int8)System::__linkproc__ IsClass(v27, (int *)off_4B7774) != 0 )
              {
                v22 = v27;
                sub_4D0824(v27, Mem);
              }
              v26 = 1;
            }
            else
            {
              System::__linkproc__ FreeMem(Mem, 24);
              sub_4B63E4(*(_DWORD *)(v27 + 760), *(_DWORD *)(v27 + 36), *(_DWORD *)(v27 + 40), 2, (int)v24);
            }
          }
        }
        else if ( sub_4B6710(*(_DWORD *)(v27 + 760), *(_DWORD *)(v27 + 36), *(_DWORD *)(v27 + 40), 2, (int)v24) == 1 )
        {
          if ( (unsigned __int8)sub_4BF64C(v27, v24[11]) != 0 )
          {
            sub_4B8EBC(v27, 10111, 0, (int)v24, *(_DWORD *)(v27 + 36), *(_DWORD *)(v27 + 40), 0);
            unknown_libname_16(v17, &dword_4C61DC);
            LOBYTE(v1) = 18;
            System::__linkproc__ PStrNCat(v17, v27 + 4, v1);
            unknown_libname_16(v16, v17);
            LOBYTE(v2) = 19;
            System::__linkproc__ PStrNCat(v16, dword_4C61E0, v2);
            unknown_libname_41();
            Sysutils::IntToStr(*(_DWORD *)(v27 + 36));
            Sysutils::IntToStr(*(_DWORD *)(v27 + 40));
            unknown_libname_41();
            Sysutils::IntToStr(v24[11]);
            System::__linkproc__ LStrCatN(v19, 12, v3);
            sub_4E42F8(v19[0]);
            sub_4C49F4(v27);
            System::__linkproc__ FreeMem(v24, 60);
          }
          else
          {
            sub_4B63E4(*(_DWORD *)(v27 + 760), *(_DWORD *)(v27 + 36), *(_DWORD *)(v27 + 40), 2, (int)v24);
          }
        }
      }
      else
      {
        sub_4BB124(v27, &str_Unable_to_pick_[1], 0);
      }
    }
  }
  __writefsdword(0, (unsigned int)ExceptionList);
  v12 = (int *)&loc_4C61C0;
  unknown_libname_35(&v13, 12);
  unknown_libname_35(&v18, 3);
  unknown_libname_34(&v20);
  return v26;
}


// ==================== [0x4C6238] sub_4C6238 (size: 1162) ====================
bool __fastcall sub_4C6238(__int32 a1, const void *a2, __int32 a3)
{
  int v3; // eax
  int v4; // ebx
  DWORD v5; // ebx
  int v6; // eax
  unsigned __int8 v7; // al
  int v8; // ecx
  int v9; // ebx
  DWORD v10; // ebx
  int v11; // eax
  unsigned __int8 v12; // al
  int v13; // ecx
  int v14; // ebx
  DWORD v15; // ebx
  int v16; // eax
  unsigned __int8 v17; // al
  int v18; // ecx
  int v19; // eax
  int v20; // edx
  int v21; // ecx
  int v22; // eax
  int v23; // ebx
  DWORD v24; // ebx
  int v25; // eax
  unsigned __int8 v26; // al
  int v27; // ecx
  int v28; // ebx
  DWORD v29; // ebx
  int v30; // eax
  unsigned __int8 v31; // al
  int v32; // ecx
  int v33; // ebx
  DWORD v34; // ebx
  int v35; // eax
  unsigned __int8 v36; // al
  int v37; // ecx
  unsigned int v39[2]; // [esp-Ch] [ebp-80h] BYREF
  int *v40; // [esp-4h] [ebp-78h]
  int v41; // [esp+Ch] [ebp-68h] BYREF
  int v42; // [esp+10h] [ebp-64h] BYREF
  int v43; // [esp+18h] [ebp-5Ch] BYREF
  int v44; // [esp+20h] [ebp-54h] BYREF
  int v45; // [esp+28h] [ebp-4Ch] BYREF
  int v46; // [esp+30h] [ebp-44h] BYREF
  _DWORD v47[2]; // [esp+38h] [ebp-3Ch] BYREF
  _WORD v48[22]; // [esp+40h] [ebp-34h] BYREF
  char v49; // [esp+6Eh] [ebp-6h]
  bool v50; // [esp+6Fh] [ebp-5h]
  __int32 v51; // [esp+70h] [ebp-4h] BYREF
  int savedregs; // [esp+74h] [ebp+0h] BYREF

  v40 = (int *)_InterlockedExchange(&v51, a3);
  qmemcpy(v48, a2, sizeof(v48));
  v47[1] = v40;
  v51 = a1;
  v40 = &savedregs;
  v39[1] = (unsigned int)&loc_4C66B1;
  v39[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v39);
  v50 = false;
  v3 = *(_DWORD *)(a1 + 760);
  if ( *(_BYTE *)(v3 + 53) != 0 )
  {
    sub_4BB124(v51, aCannotUseHere_0, 0);
  }
  else if ( HIBYTE(v48[7]) != 0 )
  {
    switch ( HIBYTE(v48[7]) )
    {
      case 1:
        v50 = true;
        break;
      case 2:
        v50 = true;
        break;
      case 3:
        if ( LOBYTE(v48[8]) == 12 )
        {
          v49 = 0;
          if ( LOBYTE(v48[15]) != 0 )
          {
            *(_BYTE *)(v51 + 536) = v48[15];
            LOWORD(v3) = v48[14];
            v4 = 1000 * (unsigned __int8)sub_407B28(v3);
            v5 = GetTickCount() + v4;
            HIWORD(v6) = HIWORD(v51);
            *(_DWORD *)(v51 + 544) = v5;
            LOWORD(v6) = v48[14];
            v7 = sub_407B28(v6);
            Sysutils::IntToStr(v7);
            System::__linkproc__ LStrCatN(v47, 3, v8);
            HIWORD(v3) = (unsigned int)sub_4BB124(v51, v47[0], 1) >> 16;
            v49 = 1;
          }
          if ( LOBYTE(v48[16]) != 0 )
          {
            *(_BYTE *)(v51 + 537) = v48[16];
            LOWORD(v3) = v48[14];
            v9 = 1000 * (unsigned __int8)sub_407B28(v3);
            v10 = GetTickCount() + v9;
            HIWORD(v11) = HIWORD(v51);
            *(_DWORD *)(v51 + 548) = v10;
            LOWORD(v11) = v48[14];
            v12 = sub_407B28(v11);
            Sysutils::IntToStr(v12);
            System::__linkproc__ LStrCatN(&v46, 3, v13);
            HIWORD(v3) = (unsigned int)sub_4BB124(v51, v46, 1) >> 16;
            v49 = 1;
          }
          if ( LOBYTE(v48[17]) != 0 )
          {
            *(_BYTE *)(v51 + 538) = v48[17];
            LOWORD(v3) = v48[14];
            v14 = 1000 * (unsigned __int8)sub_407B28(v3);
            v15 = GetTickCount() + v14;
            HIWORD(v16) = HIWORD(v51);
            *(_DWORD *)(v51 + 552) = v15;
            LOWORD(v16) = v48[14];
            v17 = sub_407B28(v16);
            Sysutils::IntToStr(v17);
            System::__linkproc__ LStrCatN(&v45, 3, v18);
            HIWORD(v3) = (unsigned int)sub_4BB124(v51, v45, 1) >> 16;
            v49 = 1;
          }
          LOWORD(v3) = v48[13];
          v19 = sub_407B28(v3);
          if ( (_BYTE)v19 != 0 )
          {
            LOWORD(v19) = v48[13];
            v22 = sub_407B28(v19);
            *(_BYTE *)(v51 + 539) = v22;
            LOWORD(v22) = v48[14];
            v23 = 1000 * (unsigned __int8)sub_407B28(v22);
            v24 = GetTickCount() + v23;
            HIWORD(v25) = HIWORD(v51);
            *(_DWORD *)(v51 + 556) = v24;
            LOWORD(v25) = v48[14];
            v26 = sub_407B28(v25);
            Sysutils::IntToStr(v26);
            System::__linkproc__ LStrCatN(&v44, 3, v27);
            HIWORD(v19) = (unsigned int)sub_4BB124(v51, v44, 1) >> 16;
            v49 = 1;
          }
          if ( LOBYTE(v48[13]) != 0 )
          {
            *(_BYTE *)(v51 + 540) = v48[13];
            LOWORD(v19) = v48[14];
            v28 = 1000 * (unsigned __int8)sub_407B28(v19);
            v29 = GetTickCount() + v28;
            HIWORD(v30) = HIWORD(v51);
            *(_DWORD *)(v51 + 560) = v29;
            LOWORD(v30) = v48[14];
            v31 = sub_407B28(v30);
            Sysutils::IntToStr(v31);
            System::__linkproc__ LStrCatN(&v43, 3, v32);
            HIWORD(v19) = (unsigned int)sub_4BB124(v51, v43, 1) >> 16;
            v49 = 1;
          }
          if ( LOBYTE(v48[14]) != 0 )
          {
            *(_BYTE *)(v51 + 541) = v48[14];
            LOWORD(v19) = v48[14];
            v33 = 1000 * (unsigned __int8)sub_407B28(v19);
            v34 = GetTickCount() + v33;
            HIWORD(v35) = HIWORD(v51);
            *(_DWORD *)(v51 + 564) = v34;
            LOWORD(v35) = v48[14];
            v36 = sub_407B28(v35);
            Sysutils::IntToStr(v36);
            System::__linkproc__ LStrCatN(&v42, 3, v37);
            sub_4BB124(v51, v42, 1);
            v49 = 1;
          }
          if ( v49 != 0 )
          {
            unknown_libname_25(v51, v20, v21);
            sub_4B865C(v51, v51, 10051, 0, 0, 0, 0, 0);
          }
          v50 = true;
        }
        else
        {
          v50 = sub_4BD1BC(v51, LOBYTE(v48[8]));
        }
        break;
      default:
        break;
    }
  }
  else if ( LOBYTE(v48[8]) == 1 )
  {
    sub_4BCAA4(v51, v48[13], v48[14]);
    v50 = true;
  }
  else if ( LOBYTE(v48[8]) == 2 )
  {
    *(_BYTE *)(v51 + 839) = 1;
    v50 = true;
  }
  else
  {
    if ( *(_DWORD *)(v51 + 180) + v48[13] < 500 && v48[13] != 0 )
      *(_DWORD *)(v51 + 180) += v48[13];
    if ( *(_DWORD *)(v51 + 184) + v48[14] < 500 && v48[14] != 0 )
      *(_DWORD *)(v51 + 184) += v48[14];
    v50 = true;
  }
  __writefsdword(0, v39[0]);
  v40 = (int *)&loc_4C66B8;
  unknown_libname_35(&v41, 12);
  return v50;
}


// ==================== [0x4C67DC] sub_4C67DC (size: 306) ====================
char __fastcall sub_4C67DC(int a1, const void *a2)
{
  int v2; // edx
  int v3; // ecx
  unsigned int v5[2]; // [esp-Ch] [ebp-5Ch] BYREF
  int *v6; // [esp-4h] [ebp-54h]
  _DWORD v7[13]; // [esp+Ch] [ebp-44h] BYREF
  void *Mem; // [esp+40h] [ebp-10h]
  unsigned __int16 *v9; // [esp+44h] [ebp-Ch]
  char v10; // [esp+4Bh] [ebp-5h]
  int v11; // [esp+4Ch] [ebp-4h]
  int savedregs; // [esp+50h] [ebp+0h] BYREF

  v7[0] = 0;
  qmemcpy(&v7[1], a2, 0x2Cu);
  v11 = a1;
  v6 = &savedregs;
  v5[1] = (unsigned int)&loc_4C68FD;
  v5[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v5);
  v10 = 0;
  unknown_libname_41();
  v9 = (unsigned __int16 *)Idfinger::TIdFinger::SetCompleteQuery(*off_4EA490, v7[0]);
  if ( v9 != nullptr
    && (unsigned __int8)sub_4C6780(v11, *v9) == 0
    && (*((_BYTE *)v9 + 45) == 99 || *((_BYTE *)v9 + 45) == *(_BYTE *)(v11 + 47))
    && *(_BYTE *)(v11 + 52) >= *((_BYTE *)v9 + 22) )
  {
    Mem = (void *)System::__linkproc__ GetMem(12);
    *(_DWORD *)Mem = v9;
    *((_WORD *)Mem + 2) = *v9;
    *((_BYTE *)Mem + 7) = 0;
    *((_BYTE *)Mem + 6) = 0;
    *((_DWORD *)Mem + 2) = 0;
    Classes::TList::Add(*(Classes::TList **)(v11 + 1052), Mem);
    unknown_libname_25(v11, v2, v3);
    if ( *(_BYTE *)(v11 + 570) == 0 )
    {
      v7[12] = v11;
      sub_4D12F4(v11, Mem);
    }
    v10 = 1;
  }
  __writefsdword(0, v5[0]);
  v6 = (int *)&loc_4C6904;
  unknown_libname_34(v7);
  return v10;
}


// ==================== [0x4C6F04] sub_4C6F04 (size: 335) ====================
int __fastcall sub_4C6F04(int a1, int a2, int a3, int a4, int a5, char a6)
{
  int v6; // eax
  int v7; // edx
  bool v8; // cc
  int v9; // edx
  int v10; // ecx
  int v12; // [esp+4h] [ebp-48h]
  int v13; // [esp+8h] [ebp-44h]
  int v14; // [esp+Ch] [ebp-40h]
  int v15; // [esp+10h] [ebp-3Ch]
  int v16; // [esp+18h] [ebp-34h] BYREF
  int v17; // [esp+1Ch] [ebp-30h]
  int v18; // [esp+20h] [ebp-2Ch]
  int v19; // [esp+24h] [ebp-28h]
  int v20; // [esp+28h] [ebp-24h]
  int v21; // [esp+2Ch] [ebp-20h]
  int v22; // [esp+30h] [ebp-1Ch]
  int v23; // [esp+34h] [ebp-18h]
  int v24; // [esp+38h] [ebp-14h]
  int v25; // [esp+40h] [ebp-Ch]
  int v26; // [esp+44h] [ebp-8h]
  int v27; // [esp+48h] [ebp-4h]

  v25 = a3;
  v26 = a2;
  v27 = a1;
  v17 = 0;
  v21 = a2 - a4;
  v19 = a4 + a2;
  v20 = a3 - a4;
  v18 = a4 + a3;
  v6 = a2 - a4;
  v7 = a4 + a2;
  v8 = v7 < v6;
  v9 = v7 - v6;
  if ( !v8 )
  {
    v14 = v9 + 1;
    v24 = v6;
    do
    {
      if ( v18 >= v20 )
      {
        v13 = v18 - v20 + 1;
        v23 = v20;
        do
        {
          if ( (unsigned __int8)sub_4B57D8(&v16) != 0
            && *(_DWORD *)(v16 + 4) != 0
            && *(_DWORD *)(*(_DWORD *)(v16 + 4) + 8) - 1 >= 0 )
          {
            v12 = *(_DWORD *)(*(_DWORD *)(v16 + 4) + 8);
            v22 = 0;
            do
            {
              if ( *(_BYTE *)Classes::TList::Get(*(Classes::TList **)(v16 + 4), v22) == 1 )
              {
                v15 = *(_DWORD *)(Classes::TList::Get(*(Classes::TList **)(v16 + 4), v22) + 4);
                if ( v15 != 0 && *(_BYTE *)(v15 + 764) == 0 && (unsigned __int8)unknown_libname_25(v27, v15, v10) != 0 )
                {
                  if ( a6 != 0 )
                    sub_4C6D38(v15, a5);
                  else
                    sub_4C6C28(v15, a5);
                  ++v17;
                }
              }
              ++v22;
              --v12;
            }
            while ( v12 != 0 );
          }
          ++v23;
          --v13;
        }
        while ( v13 != 0 );
      }
      ++v24;
      --v14;
    }
    while ( v14 != 0 );
  }
  return v17;
}


// ==================== [0x4C716C] sub_4C716C (size: 1060) ====================
int __fastcall sub_4C716C(int a1, const void *a2)
{
  int v2; // ecx
  int v3; // ecx
  int v4; // ecx
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-Ch] [ebp-60h] BYREF
  void *v7; // [esp-8h] [ebp-5Ch]
  int *v8; // [esp-4h] [ebp-58h]
  int v9; // [esp+10h] [ebp-44h] BYREF
  int v10[7]; // [esp+14h] [ebp-40h] BYREF
  int v11; // [esp+30h] [ebp-24h]
  int v12; // [esp+34h] [ebp-20h]
  signed int v13; // [esp+38h] [ebp-1Ch]
  int v14; // [esp+3Ch] [ebp-18h]
  int v15; // [esp+40h] [ebp-14h]
  int v16; // [esp+44h] [ebp-10h]
  int v17; // [esp+48h] [ebp-Ch]
  int v18; // [esp+4Ch] [ebp-8h]
  int v19; // [esp+50h] [ebp-4h]
  int savedregs; // [esp+54h] [ebp+0h] BYREF

  v9 = 0;
  qmemcpy(v10, a2, sizeof(v10));
  v19 = a1;
  unknown_libname_53(v10, off_45464C);
  v8 = &savedregs;
  v7 = &loc_4C7582;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  if ( LOWORD(v10[0]) > 0x27AAu )
  {
    switch ( LOWORD(v10[0]) )
    {
      case 0x27ABu:
        sub_4BCB54(v19, v10[6], HIWORD(v10[0]));
        break;
      case 0x283Cu:
        v12 = v10[2];
        if ( v10[2] != 0 && (unsigned __int8)unknown_libname_25(v19, v12, v2) != 0 )
        {
          unknown_libname_25(v19, v12, v4);
          if ( *(_BYTE *)(v19 + 570) == 0 && *(_BYTE *)(v12 + 570) == 0 )
            sub_4BC7BC(v19, v12);
          sub_4BC74C(v19, v12);
        }
        sub_4C35A8(v10[3]);
        break;
      case 0x2844u:
        sub_4930E8(*off_4EA674, v19, v10[1], v8, v7, ExceptionList);
        break;
      case 0x28ACu:
        sub_4BDC7C(v19);
        break;
      default:
        break;
    }
  }
  else
  {
    switch ( LOWORD(v10[0]) )
    {
      case 0x27AAu:
        v14 = HIWORD(v10[0]);
        v16 = LOWORD(v10[1]);
        v15 = (unsigned __int16)sub_407B24(v10[1]);
        v13 = v10[2];
        v11 = v10[3];
        if ( v10[3] != 0 )
        {
          v18 = sub_4BDDEC(v11, v19, v14);
          if ( v18 > 0 )
          {
            unknown_libname_25(v19, v11, v3);
            if ( *(_BYTE *)(v11 + 570) >= 0x32u )
              v14 = System::__linkproc__ ROUND((double)v14 * *(double *)&tbyte_4C7590);
            if ( (int)abs32(v16 - *(_DWORD *)(v11 + 36)) <= v13 && (int)abs32(v15 - *(_DWORD *)(v11 + 40)) <= v13 )
              sub_4B865C(v11, v19, 10025, 0, v14, 0, 0, 0);
          }
        }
        break;
      case 0x2729u:
        goto LABEL_26;
      case 0x272Au:
        if ( v10[1] + *(_DWORD *)(v19 + 188) >= 300 )
        {
          *(_DWORD *)(v19 + 188) = 300;
        }
        else
        {
          *(_DWORD *)(v19 + 188) += v10[1];
          *(_DWORD *)(v19 + 600) = 5;
        }
        break;
      case 0x272Cu:
LABEL_26:
        if ( LOWORD(v10[0]) == 10025
          && *(_BYTE *)(v19 + 570) >= 0x32u
          && *(_BYTE *)(v19 + 703) == 0
          && *(_BYTE *)(v19 + 52) < 0x32u )
        {
          *(_DWORD *)(v19 + 1020) += 800 + System::__linkproc__ RandInt(1000);
        }
        v17 = sub_4BDDEC(v19, 0, v10[1]);
        if ( v17 > 0 )
        {
          sub_4BDEB8(v19, v17);
          sub_4C4A24(v19);
          sub_4B8EBC(v19, 10027, v17, *(unsigned __int16 *)(v19 + 420), *(unsigned __int16 *)(v19 + 422), v10[4], 0);
          if ( *(_BYTE *)(v19 + 570) != 0 )
          {
            if ( *(_BYTE *)(v19 + 699) != 0 )
              *(_DWORD *)(v19 + 708) -= 1000 * v17;
            sub_4B865C(
              v19,
              v19,
              10020,
              v17,
              *(unsigned __int16 *)(v19 + 420),
              *(unsigned __int16 *)(v19 + 422),
              v10[4],
              0);
          }
        }
        break;
      case 0x2775u:
        sub_4B8EBC(v19, v10[4], SHIWORD(v10[0]), v10[1], v10[2], v10[3], v10[6]);
        if ( v10[4] == 10020 && *(_BYTE *)(v19 + 570) != 0 )
          sub_4B865C(v19, v19, v10[4], SHIWORD(v10[0]), v10[1], v10[2], v10[3], v10[6]);
        break;
      default:
        break;
    }
  }
  __writefsdword(0, (unsigned int)ExceptionList);
  v8 = (int *)&loc_4C7589;
  unknown_libname_34(&v9);
  return unknown_libname_51(v10, off_45464C);
}


// ==================== [0x4C759C] sub_4C759C (size: 343) ====================
int __fastcall sub_4C759C(int a1)
{
  __int64 v1; // rax
  unsigned int v3[6]; // [esp+8h] [ebp-2Ch] BYREF
  int v4; // [esp+20h] [ebp-14h]
  int v5; // [esp+24h] [ebp-10h]
  int v6; // [esp+28h] [ebp-Ch]
  int v7; // [esp+2Ch] [ebp-8h]
  int v8; // [esp+30h] [ebp-4h]
  int savedregs; // [esp+34h] [ebp+0h] BYREF

  v8 = a1;
  v3[2] = (unsigned int)&savedregs;
  v3[1] = (unsigned int)&loc_4C76D8;
  v3[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v3);
  if ( *(_WORD *)(a1 + 1108) != 0 )
  {
    v4 = *(unsigned __int16 *)(v8 + 1110);
    v7 = System::__linkproc__ ROUND((double)v4 / flt_4C76F4);
    v6 = *(unsigned __int16 *)(v8 + 1110) - 1;
    if ( v6 > 0 )
    {
      *(_WORD *)(v8 + 1110) = v6;
    }
    else
    {
      *(_WORD *)(v8 + 1110) = 0;
      if ( *(_BYTE *)(v8 + 570) == 0 )
      {
        v5 = v8;
        sub_4D0BDC(v8, v8 + 1104);
      }
      *(_WORD *)(v8 + 1108) = 0;
      *(_DWORD *)(v8 + 676) = 0;
      sub_4B8EBC(v8, 10137, 0, 0, 0, 0, 0);
      sub_4B865C(v8, v8, 10138, 0, *(unsigned __int16 *)(v8 + 1110), 0, 0, 0);
    }
    LODWORD(v1) = System::__linkproc__ ROUND((double)v6 / flt_4C76F4);
    if ( v7 != v1 )
      sub_4B865C(v8, v8, 10138, 0, *(unsigned __int16 *)(v8 + 1110), 0, 0, 0);
  }
  __writefsdword(0, v3[0]);
  return 0;
}


// ==================== [0x4C7720] sub_4C7720 (size: 3848) ====================
int __fastcall sub_4C7720(int a1)
{
  int v1; // ecx
  int v2; // ecx
  int v3; // ecx
  int v4; // edx
  DWORD TickCount; // eax
  int v6; // ecx
  DWORD v7; // eax
  int v8; // ecx
  __int64 v9; // rax
  __int64 v10; // rax
  int v11; // eax
  int v12; // eax
  int v13; // eax
  int v14; // ecx
  DWORD v15; // eax
  int v16; // edx
  DWORD v17; // eax
  int v18; // eax
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-18h] [ebp-70h] BYREF
  void *v21; // [esp-14h] [ebp-6Ch]
  int *v22; // [esp-10h] [ebp-68h]
  unsigned int v23[2]; // [esp-Ch] [ebp-64h] BYREF
  int *v24; // [esp-4h] [ebp-5Ch]
  _BYTE v25[30]; // [esp+18h] [ebp-40h] BYREF
  char v26; // [esp+36h] [ebp-22h]
  char v27; // [esp+37h] [ebp-21h]
  int v28; // [esp+38h] [ebp-20h]
  unsigned int v29; // [esp+3Ch] [ebp-1Ch]
  unsigned int v30; // [esp+40h] [ebp-18h]
  int v31; // [esp+44h] [ebp-14h]
  int v32; // [esp+48h] [ebp-10h]
  DWORD v33; // [esp+4Ch] [ebp-Ch]
  int i; // [esp+50h] [ebp-8h]
  int v35; // [esp+54h] [ebp-4h]
  int savedregs; // [esp+58h] [ebp+0h] BYREF

  v35 = a1;
  unknown_libname_49(v25, off_45464C);
  v24 = &savedregs;
  v23[1] = (unsigned int)&loc_4C861A;
  v23[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v23);
  v22 = &savedregs;
  v21 = &loc_4C7784;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  while ( (unsigned __int8)unknown_libname_25(v35, v25, v1) != 0 )
    unknown_libname_25(v35, v25, v2);
  __writefsdword(0, (unsigned int)ExceptionList);
  v22 = &savedregs;
  v21 = &loc_4C7A20;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  if ( *(_BYTE *)(v35 + 696) != 0 )
  {
    *(_WORD *)(v35 + 420) = *(_WORD *)(v35 + 424);
    *(_WORD *)(v35 + 422) = *(_WORD *)(v35 + 426);
  }
  v33 = (GetTickCount() - *(_DWORD *)(v35 + 976)) / 0x14;
  *(_DWORD *)(v35 + 976) = GetTickCount();
  *(_DWORD *)(v35 + 880) += v33;
  *(_DWORD *)(v35 + 884) += v33;
  if ( *(_BYTE *)(v35 + 772) != 0 )
  {
    if ( GetTickCount() - *(_DWORD *)(v35 + 776) > 0x7530 )
      sub_4BB300(v35);
  }
  else
  {
    if ( *(_WORD *)(v35 + 420) < *(_WORD *)(v35 + 424) && *(_DWORD *)(v35 + 880) >= dword_4E9E9C )
    {
      v30 = *(unsigned __int16 *)(v35 + 424) / 0x4Bu + 1;
      if ( v30 + *(unsigned __int16 *)(v35 + 420) >= *(unsigned __int16 *)(v35 + 424) )
        *(_WORD *)(v35 + 420) = *(_WORD *)(v35 + 424);
      else
        *(_WORD *)(v35 + 420) += v30;
      sub_4C4A24(v35);
    }
    v4 = v35;
    if ( *(_WORD *)(v35 + 422) < *(_WORD *)(v35 + 426) && *(_DWORD *)(v35 + 884) >= dword_4E9EA0 )
    {
      v30 = *(unsigned __int16 *)(v35 + 426) / 0x12u + 1;
      if ( v30 + *(unsigned __int16 *)(v35 + 422) >= *(unsigned __int16 *)(v35 + 426) )
        *(_WORD *)(v35 + 422) = *(_WORD *)(v35 + 426);
      else
        *(_WORD *)(v35 + 422) += v30;
      sub_4C4A24(v35);
    }
    if ( *(_WORD *)(v35 + 420) == 0 )
    {
      if ( *(_BYTE *)(v35 + 845) != 0 )
      {
        TickCount = GetTickCount();
        v4 = v35;
        if ( TickCount - *(_DWORD *)(v35 + 848) > 0x7530 )
        {
          *(_DWORD *)(v35 + 848) = GetTickCount();
          sub_4C022C(v35);
          *(_WORD *)(v35 + 420) = *(_WORD *)(v35 + 424);
          sub_4C4A24(v35);
          sub_4BB124(v35, &str_The_Ring_made_y[1], 1);
        }
      }
      if ( *(_WORD *)(v35 + 420) == 0 )
        unknown_libname_25(v35, v4, v3);
    }
    if ( *(_DWORD *)(v35 + 880) >= dword_4E9E9C )
      *(_DWORD *)(v35 + 880) = 0;
    if ( *(_DWORD *)(v35 + 884) >= dword_4E9EA0 )
      *(_DWORD *)(v35 + 884) = 0;
  }
  v6 = (int)v22;
  __writefsdword(0, (unsigned int)ExceptionList);
  v22 = &savedregs;
  v21 = &loc_4C7D45;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  if ( *(_BYTE *)(v35 + 772) != 0 || *(int *)(v35 + 184) <= 0 && *(int *)(v35 + 180) <= 0 && *(int *)(v35 + 188) <= 0 )
  {
    *(_DWORD *)(v35 + 608) = GetTickCount();
  }
  else
  {
    v29 = 600 - Ibutils::Min((Ibutils *)0x190, 10 * *(unsigned __int8 *)(v35 + 52), v6);
    if ( GetTickCount() - *(_DWORD *)(v35 + 608) >= v29 && *(_BYTE *)(v35 + 772) == 0 )
    {
      v7 = GetTickCount();
      v33 = Ibutils::Min((Ibutils *)0xC8, v7 - *(_DWORD *)(v35 + 608) - v29, v8);
      *(_DWORD *)(v35 + 608) = v33 + GetTickCount();
      if ( *(int *)(v35 + 180) > 0 || *(int *)(v35 + 184) > 0 || *(int *)(v35 + 600) > 0 )
      {
        if ( *(int *)(v35 + 596) <= 0 )
          *(_DWORD *)(v35 + 596) = 1;
        if ( *(int *)(v35 + 604) <= 0 )
          *(_DWORD *)(v35 + 604) = 1;
        if ( *(int *)(v35 + 600) <= 0 )
          *(_DWORD *)(v35 + 600) = 1;
        if ( *(_DWORD *)(v35 + 180) >= *(_DWORD *)(v35 + 596) )
        {
          v32 = *(_DWORD *)(v35 + 596);
          *(_DWORD *)(v35 + 180) -= *(_DWORD *)(v35 + 596);
        }
        else
        {
          v32 = *(_DWORD *)(v35 + 180);
          *(_DWORD *)(v35 + 180) = 0;
        }
        if ( *(_DWORD *)(v35 + 184) >= *(_DWORD *)(v35 + 604) )
        {
          v31 = *(_DWORD *)(v35 + 604);
          *(_DWORD *)(v35 + 184) -= *(_DWORD *)(v35 + 604);
        }
        else
        {
          v31 = *(_DWORD *)(v35 + 184);
          *(_DWORD *)(v35 + 184) = 0;
        }
        if ( *(_DWORD *)(v35 + 188) >= *(_DWORD *)(v35 + 600) )
        {
          v32 += *(_DWORD *)(v35 + 600);
          *(_DWORD *)(v35 + 188) -= *(_DWORD *)(v35 + 600);
        }
        else
        {
          v32 += *(_DWORD *)(v35 + 188);
          *(_DWORD *)(v35 + 188) = 0;
        }
        *(_DWORD *)(v35 + 596) = *(unsigned __int8 *)(v35 + 52) / 0xAu + 5;
        *(_DWORD *)(v35 + 604) = *(unsigned __int8 *)(v35 + 52) / 0xAu + 5;
        *(_DWORD *)(v35 + 600) = 5;
        sub_4BCAA4(v35, v32, v31);
        if ( *(_WORD *)(v35 + 420) == *(_WORD *)(v35 + 424) )
        {
          *(_DWORD *)(v35 + 180) = 0;
          *(_DWORD *)(v35 + 188) = 0;
        }
        if ( *(_WORD *)(v35 + 422) == *(_WORD *)(v35 + 426) )
          *(_DWORD *)(v35 + 184) = 0;
      }
    }
  }
  if ( *(_DWORD *)(v35 + 880) < -dword_4E9E9C && *(_WORD *)(v35 + 420) > 1u )
  {
    --*(_WORD *)(v35 + 420);
    *(_DWORD *)(v35 + 880) += dword_4E9E9C;
    sub_4C4A24(v35);
  }
  __writefsdword(0, (unsigned int)ExceptionList);
  v22 = &savedregs;
  v21 = &loc_4C801B;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  if ( *(_DWORD *)(v35 + 888) != 0 )
  {
    if ( GetTickCount() - *(_DWORD *)(v35 + 892) > 0x7530
      || *(_BYTE *)(*(_DWORD *)(v35 + 888) + 772) != 0
      || *(_BYTE *)(*(_DWORD *)(v35 + 888) + 764) != 0
      || (v9 = *(_DWORD *)(*(_DWORD *)(v35 + 888) + 36) - *(_DWORD *)(v35 + 36),
          (int)((HIDWORD(v9) ^ v9) - HIDWORD(v9)) > 15)
      || (v10 = *(_DWORD *)(*(_DWORD *)(v35 + 888) + 40) - *(_DWORD *)(v35 + 40),
          (int)((HIDWORD(v10) ^ v10) - HIDWORD(v10)) > 15) )
    {
      *(_DWORD *)(v35 + 888) = 0;
    }
  }
  if ( *(_DWORD *)(v35 + 896) != 0
    && (GetTickCount() - *(_DWORD *)(v35 + 900) > 0x7530
     || *(_BYTE *)(*(_DWORD *)(v35 + 896) + 772) != 0
     || *(_BYTE *)(*(_DWORD *)(v35 + 896) + 764) != 0) )
  {
    *(_DWORD *)(v35 + 896) = 0;
  }
  if ( *(_DWORD *)(v35 + 904) != 0
    && (GetTickCount() - *(_DWORD *)(v35 + 908) > 0x1770
     || *(_BYTE *)(*(_DWORD *)(v35 + 904) + 772) != 0
     || *(_BYTE *)(*(_DWORD *)(v35 + 904) + 764) != 0) )
  {
    *(_DWORD *)(v35 + 904) = 0;
  }
  if ( *(_DWORD *)(v35 + 648) != 0 )
  {
    if ( (*(_BYTE *)(v35 + 700) = 1, *(_BYTE *)(*(_DWORD *)(v35 + 648) + 772) != 0)
      && GetTickCount() - *(_DWORD *)(*(_DWORD *)(v35 + 648) + 776) > 0x3E8
      || *(_BYTE *)(*(_DWORD *)(v35 + 648) + 764) != 0
      && GetTickCount() - *(_DWORD *)(*(_DWORD *)(v35 + 648) + 768) > 0x3E8 )
    {
      *(_WORD *)(v35 + 420) = 0;
    }
  }
  if ( *(_DWORD *)(*(_DWORD *)(v35 + 668) + 8) - 1 >= 0 )
  {
    i = *(_DWORD *)(*(_DWORD *)(v35 + 668) + 8) - 1;
    do
    {
      if ( *(_BYTE *)(Classes::TList::Get(*(Classes::TList **)(v35 + 668), i) + 772) != 0
        || *(_BYTE *)(Classes::TList::Get(*(Classes::TList **)(v35 + 668), i) + 764) != 0
        || *(_DWORD *)((v11 = Classes::TList::Get(*(Classes::TList **)(v35 + 668), i)) + 648) != v35 )
      {
        Classes::TList::Delete(*(Classes::TList **)(v35 + 668), i);
      }
      --i;
    }
    while ( i != -1 );
  }
  if ( *(_BYTE *)(v35 + 716) != 0 && GetTickCount() - *(_DWORD *)(v35 + 720) > *(_DWORD *)(v35 + 724) )
    sub_4BDBF8(v35);
  if ( *(_BYTE *)(v35 + 728) != 0 && GetTickCount() - *(_DWORD *)(v35 + 732) > *(_DWORD *)(v35 + 736) )
    sub_4BDC54(v35);
  if ( *(_BYTE *)(v35 + 740) != 0 && GetTickCount() - *(_DWORD *)(v35 + 744) > *(_DWORD *)(v35 + 748) )
    sub_4BDCD0(v35);
  __writefsdword(0, (unsigned int)ExceptionList);
  v22 = &savedregs;
  v21 = &loc_4C82FF;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  if ( GetTickCount() - *(_DWORD *)(v35 + 956) > 0xC350 )
  {
    *(_DWORD *)(v35 + 956) = GetTickCount();
    if ( *(int *)(v35 + 172) > 0 )
      sub_4BF520(v35, 1);
  }
  if ( GetTickCount() - *(_DWORD *)(v35 + 960) > 0x1F4 )
  {
    *(_DWORD *)(v35 + 960) += 500;
    if ( *(_BYTE *)(v35 + 570) == 0 )
    {
      sub_4C759C(v35);
      sub_4BC83C(v35);
    }
  }
  if ( GetTickCount() - *(_DWORD *)(v35 + 972) > 0x1388 )
    *(_DWORD *)(v35 + 972) = GetTickCount();
  if ( GetTickCount() - *(_DWORD *)(v35 + 968) > 0x2710 )
  {
    *(_DWORD *)(v35 + 968) = GetTickCount();
    if ( *(_DWORD *)(v35 + 648) != 0 )
    {
      if ( GetTickCount() > *(_DWORD *)(v35 + 652) )
      {
        if ( *(_DWORD *)(*(_DWORD *)(*(_DWORD *)(v35 + 648) + 668) + 8) - 1 >= 0 )
        {
          i = *(_DWORD *)(*(_DWORD *)(*(_DWORD *)(v35 + 648) + 668) + 8) - 1;
          while ( 1 )
          {
            v12 = Classes::TList::Get(*(Classes::TList **)(*(_DWORD *)(v35 + 648) + 668), i);
            if ( v12 == v35 )
              break;
            if ( --i == -1 )
              goto LABEL_118;
          }
          Classes::TList::Delete(*(Classes::TList **)(*(_DWORD *)(v35 + 648) + 668), i);
        }
LABEL_118:
        *(_DWORD *)(v35 + 648) = 0;
        *(_WORD *)(v35 + 420) /= 0xAu;
        sub_4BF0C4(v35);
      }
      if ( *(_DWORD *)(v35 + 656) != 0 && GetTickCount() - *(_DWORD *)(v35 + 656) > 0x2932E00 )
        *(_WORD *)(v35 + 420) = 0;
    }
  }
  if ( GetTickCount() - *(_DWORD *)(v35 + 964) > 0x7530 )
  {
    *(_DWORD *)(v35 + 964) = GetTickCount();
    if ( *(_DWORD *)(v35 + 628) != 0
      && (*(_BYTE *)(*(_DWORD *)(v35 + 628) + 772) != 0 || *(_BYTE *)(*(_DWORD *)(v35 + 628) + 764) != 0) )
    {
      *(_DWORD *)(v35 + 628) = 0;
    }
    if ( *(_DWORD *)(v35 + 628) == v35 )
    {
      v13 = (*(int (__fastcall **)(_DWORD))(**(_DWORD **)(v35 + 632) + 20))(*(_DWORD *)(v35 + 632)) - 1;
      if ( v13 >= 0 )
      {
        for ( i = v13; i != -1; --i )
        {
          v28 = (*(int (__fastcall **)(_DWORD, int))(**(_DWORD **)(*(_DWORD *)(v35 + 628) + 632) + 24))(
                  *(_DWORD *)(*(_DWORD *)(v35 + 628) + 632),
                  i);
          if ( *(_BYTE *)(v28 + 772) != 0 || *(_BYTE *)(v28 + 764) != 0 )
            (*(void (__fastcall **)(_DWORD, int))(**(_DWORD **)(v35 + 632) + 68))(*(_DWORD *)(v35 + 632), i);
        }
      }
    }
    if ( *(_DWORD *)(v35 + 796) != 0 && *(_BYTE *)(*(_DWORD *)(v35 + 796) + 764) != 0 )
      *(_DWORD *)(v35 + 796) = 0;
    sub_4B6980(v35);
  }
  v14 = (int)v22;
  __writefsdword(0, (unsigned int)ExceptionList);
  v22 = &savedregs;
  v21 = &loc_4C8546;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  v27 = 0;
  v26 = 0;
  for ( i = 0; i != 12; ++i )
  {
    if ( *(_WORD *)(v35 + 2 * i + 96) != 0 && *(_WORD *)(v35 + 2 * i + 96) < 0xEA60u )
    {
      v15 = GetTickCount();
      v14 = i;
      if ( v15 - *(_DWORD *)(v35 + 4 * i + 488) > 0x3E8 )
      {
        --*(_WORD *)(v35 + 2 * i + 96);
        *(_DWORD *)(v35 + 4 * i + 488) += 1000;
        if ( *(_WORD *)(v35 + 2 * i + 96) == 0 )
        {
          v27 = 1;
          switch ( i )
          {
            case 8:
              *(_BYTE *)(v35 + 836) = 0;
              break;
            case 9:
              v26 = 1;
              sub_4BB124(v35, aAcReturned, 1);
              break;
            case 10:
              v26 = 1;
              sub_4BB124(v35, aAmcReturned, 1);
              break;
            case 11:
              *(_BYTE *)(v35 + 860) = 0;
              break;
            default:
              break;
          }
        }
      }
    }
  }
  for ( i = 0; i != 6; ++i )
  {
    v16 = i;
    if ( *(_BYTE *)(v35 + i + 536) != 0 )
    {
      v17 = GetTickCount();
      v16 = v35;
      v14 = i;
      if ( v17 > *(_DWORD *)(v35 + 4 * i + 544) )
      {
        v16 = i;
        *(_BYTE *)(v35 + i + 536) = 0;
        v26 = 1;
        switch ( i )
        {
          case 0:
            sub_4BB124(v35, &str_DC_Returned___[1], 1);
            break;
          case 1:
            sub_4BB124(v35, &str_MC_Returned_[1], 1);
            break;
          case 2:
            sub_4BB124(v35, &str_SC_Returned_[1], 1);
            break;
          case 3:
            sub_4BB124(v35, &str_Aspeed_Returned[1], 1);
            break;
          case 4:
            sub_4BB124(v35, &str_HP_Returned_[1], 1);
            break;
          case 5:
            sub_4BB124(v35, &str_MP_Returned___[1], 1);
            break;
          default:
            continue;
        }
      }
    }
  }
  if ( v27 != 0 )
  {
    v18 = sub_4BA43C(v35);
    *(_DWORD *)(v35 + 92) = v18;
    sub_4BA5B4(v35);
  }
  if ( v26 != 0 )
  {
    unknown_libname_25(v35, v16, v14);
    sub_4B865C(v35, v35, 10051, 0, 0, 0, 0, 0);
  }
  __writefsdword(0, (unsigned int)ExceptionList);
  v22 = &savedregs;
  v21 = &loc_4C85EA;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  if ( GetTickCount() - *(_DWORD *)(v35 + 952) > 0x9C4 )
  {
    *(_DWORD *)(v35 + 952) = GetTickCount();
    if ( *(_WORD *)(v35 + 96) != 0 )
    {
      if ( *(_BYTE *)(v35 + 699) != 0 )
        *(_DWORD *)(v35 + 708) -= 1000;
      sub_4BE3FC(v35, *(unsigned __int8 *)(v35 + 612) + 1);
      *(_DWORD *)(v35 + 880) = 0;
      *(_DWORD *)(v35 + 884) = 0;
      sub_4C4A24(v35);
    }
  }
  __writefsdword(0, (unsigned int)ExceptionList);
  __writefsdword(0, v23[0]);
  v24 = (int *)&loc_4C8621;
  return unknown_libname_51(v25, off_45464C);
}


// ==================== [0x4C89D0] sub_4C89D0 (size: 1120) ====================
char __fastcall sub_4C89D0(int a1, _DWORD *a2)
{
  unsigned int v3[2]; // [esp-Ch] [ebp-1Ch] BYREF
  int *v4; // [esp-4h] [ebp-14h]
  int v5; // [esp+0h] [ebp-10h] BYREF
  char v6; // [esp+7h] [ebp-9h]
  int v7; // [esp+8h] [ebp-8h]
  _DWORD *v8; // [esp+Ch] [ebp-4h]
  int savedregs; // [esp+10h] [ebp+0h] BYREF

  v5 = 0;
  v8 = a2;
  v7 = a1;
  v4 = &savedregs;
  v3[1] = (unsigned int)&loc_4C8E22;
  v3[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v3);
  v6 = 0;
  if ( a2 != nullptr && v8 != (_DWORD *)v7 )
  {
    if ( *(_BYTE *)(v7 + 570) < 0x32u )
    {
      if ( *(_BYTE *)(v7 + 570) != 0 )
      {
        v6 = 1;
      }
      else
      {
        switch ( *(_BYTE *)(v7 + 673) )
        {
          case 0:
            if ( *((_BYTE *)v8 + 570) < 0xAu || *((_BYTE *)v8 + 570) > 0xFu )
              v6 = 1;
            if ( *off_4EA0A0 != 0 )
              goto LABEL_75;
            break;
          case 1:
            v6 = *((_BYTE *)v8 + 570) >= 0x32u;
            break;
          case 2:
            if ( *((_BYTE *)v8 + 570) < 0xAu || *((_BYTE *)v8 + 570) > 0xFu )
              v6 = 1;
            if ( *((_BYTE *)v8 + 570) == 0 && (unsigned __int8)sub_4C3908(v7, v8) != 0 )
              v6 = 0;
            if ( *off_4EA0A0 != 0 )
              goto LABEL_75;
            break;
          case 3:
            if ( *((_BYTE *)v8 + 570) < 0xAu || *((_BYTE *)v8 + 570) > 0xFu )
              v6 = 1;
            if ( *((_BYTE *)v8 + 570) == 0 && *(_DWORD *)(v7 + 800) != 0 )
            {
              unknown_libname_41();
              if ( (unsigned __int8)sub_498714(*(_DWORD *)(v7 + 800), v5) != 0 )
                v6 = 0;
              if ( *(_BYTE *)(v7 + 680) != 0
                && v8[200] != 0
                && (unsigned __int8)sub_499BD8(*(_DWORD *)(v7 + 800), v8[200]) != 0 )
              {
                v6 = 0;
              }
            }
            if ( *off_4EA0A0 != 0 )
              goto LABEL_75;
            break;
          case 4:
            if ( *((_BYTE *)v8 + 570) < 0xAu || *((_BYTE *)v8 + 570) > 0xFu )
              v6 = 1;
            if ( *((_BYTE *)v8 + 570) == 0 )
            {
              if ( sub_4BF0A0(v7) < 2 )
                v6 = sub_4BF0A0(v8) >= 2;
              else
                v6 = sub_4BF0A0(v8) < 2;
            }
            if ( *off_4EA0A0 != 0 )
LABEL_75:
              v6 = sub_4C88E4(&savedregs);
            break;
          default:
            break;
        }
      }
    }
    else
    {
      if ( *(_DWORD *)(v7 + 648) != 0 )
      {
        if ( *(_DWORD **)(*(_DWORD *)(v7 + 648) + 896) == v8
          || *(_DWORD **)(*(_DWORD *)(v7 + 648) + 904) == v8
          || *(_DWORD **)(*(_DWORD *)(v7 + 648) + 888) == v8 )
        {
          v6 = 1;
        }
        if ( v8[222] != 0
          && (v8[222] == *(_DWORD *)(v7 + 648)
           || *(_DWORD *)(v8[222] + 648) == *(_DWORD *)(v7 + 648) && *((_BYTE *)v8 + 570) != 0) )
        {
          v6 = 1;
        }
        if ( v8[222] == v7 && *((_BYTE *)v8 + 570) >= 0x32u )
          v6 = 1;
        if ( v8[162] != 0
          && (v8[162] == *(_DWORD *)(*(_DWORD *)(v7 + 648) + 896) || v8[162] == *(_DWORD *)(*(_DWORD *)(v7 + 648) + 888)) )
        {
          v6 = 1;
        }
        if ( v8[162] == *(_DWORD *)(v7 + 648) )
          v6 = 0;
        if ( *((_BYTE *)v8 + 716) != 0 )
          v6 = 0;
        if ( *(_BYTE *)(*(_DWORD *)(v7 + 648) + 672) != 0 )
          v6 = 0;
        if ( *((_BYTE *)v8 + 570) == 0 && sub_4BEE20(v8) != 0 )
          v6 = 0;
        sub_4BDC54(v7);
      }
      else
      {
        v6 = *((_BYTE *)v8 + 570) == 0;
        if ( *(_BYTE *)(v7 + 570) > 0xFu && *(_BYTE *)(v7 + 570) < 0x32u )
          v6 = 1;
        if ( v8[162] != 0 )
          v6 = 1;
      }
      if ( *(_BYTE *)(v7 + 728) != 0 )
        v6 = 1;
    }
    if ( *((_BYTE *)v8 + 841) != 0 || *((_BYTE *)v8 + 837) != 0 )
      v6 = 0;
  }
  __writefsdword(0, v3[0]);
  v4 = (int *)&loc_4C8E29;
  unknown_libname_34(&v5);
  return v6;
}


// ==================== [0x4C8F08] sub_4C8F08 (size: 403) ====================
bool __usercall sub_4C8F08@<al>(int a1@<eax>, int a2)
{
  unsigned int v3[2]; // [esp-Ch] [ebp-18h] BYREF
  int *v4; // [esp-4h] [ebp-10h]
  int v5; // [esp+0h] [ebp-Ch] BYREF
  bool v6; // [esp+7h] [ebp-5h]
  int v7; // [esp+8h] [ebp-4h]
  int savedregs; // [esp+Ch] [ebp+0h] BYREF

  v5 = 0;
  v7 = a1;
  v4 = &savedregs;
  v3[1] = (unsigned int)&loc_4C908D;
  v3[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v3);
  v6 = false;
  if ( *(_BYTE *)(a1 + 570) == 0 )
  {
    switch ( *(_BYTE *)(*(_DWORD *)(a2 - 4) + 673) )
    {
      case 0:
        v6 = true;
        break;
      case 1:
        v6 = true;
        break;
      case 2:
        v6 = *(_DWORD *)(a2 - 4) == v7;
        if ( (unsigned __int8)sub_4C3908(*(_DWORD *)(a2 - 4), v7) != 0 )
          v6 = true;
        break;
      case 3:
        v6 = *(_DWORD *)(a2 - 4) == v7;
        if ( *(_DWORD *)(*(_DWORD *)(a2 - 4) + 800) != 0 )
        {
          unknown_libname_41();
          if ( (unsigned __int8)sub_498714(*(_DWORD *)(*(_DWORD *)(a2 - 4) + 800), v5) != 0 )
            v6 = true;
          if ( *(_BYTE *)(*(_DWORD *)(a2 - 4) + 680) != 0
            && *(_DWORD *)(v7 + 800) != 0
            && (unsigned __int8)sub_499BD8(*(_DWORD *)(*(_DWORD *)(a2 - 4) + 800), *(_DWORD *)(v7 + 800)) != 0 )
          {
            v6 = true;
          }
        }
        break;
      case 4:
        v6 = *(_DWORD *)(a2 - 4) == v7;
        if ( sub_4BF0A0(*(_DWORD *)(a2 - 4)) < 2 )
        {
          if ( sub_4BF0A0(v7) < 2 )
            v6 = true;
        }
        else if ( sub_4BF0A0(v7) >= 2 )
        {
          v6 = true;
        }
        break;
      default:
        break;
    }
  }
  __writefsdword(0, v3[0]);
  v4 = (int *)&loc_4C9094;
  unknown_libname_34(&v5);
  return v6;
}


// ==================== [0x4C9694] sub_4C9694 (size: 379) ====================
char __fastcall sub_4C9694(_DWORD *a1)
{
  int v1; // eax
  int v3; // [esp+0h] [ebp-20h]
  int v4; // [esp+4h] [ebp-1Ch]
  int v5; // [esp+8h] [ebp-18h]
  int v6; // [esp+Ch] [ebp-14h]
  int v7; // [esp+10h] [ebp-10h]
  int i; // [esp+14h] [ebp-Ch]
  int v9; // [esp+18h] [ebp-8h]

  if ( a1[9] != a1[334] || (v1 = a1[10]) != a1[335] )
  {
    v7 = a1[334];
    v6 = a1[335];
    a1[253] = sub_407B30(v6);
    v9 = 4;
    if ( v7 <= a1[9] )
    {
      if ( v7 >= a1[9] )
      {
        if ( v6 <= a1[10] )
        {
          if ( v6 < a1[10] )
            v9 = 0;
        }
        else
        {
          v9 = 4;
        }
      }
      else
      {
        v9 = 6;
        if ( v6 > a1[10] )
          v9 = 5;
        if ( v6 < a1[10] )
          v9 = 7;
      }
    }
    else
    {
      v9 = 2;
      if ( v6 > a1[10] )
        v9 = 3;
      if ( v6 < a1[10] )
        v9 = 1;
    }
    v5 = a1[9];
    v4 = a1[10];
    sub_4C3F64((int)a1, v9, 0);
    v3 = System::__linkproc__ RandInt(3);
    for ( i = 1; i != 8; ++i )
    {
      LOBYTE(v1) = v5;
      if ( v5 != a1[9] )
        break;
      LOBYTE(v1) = v4;
      if ( v4 != a1[10] )
        break;
      if ( v3 != 0 )
      {
        ++v9;
      }
      else if ( v9 <= 0 )
      {
        v9 = 7;
      }
      else
      {
        --v9;
      }
      if ( v9 > 7 )
        v9 = 0;
      LOBYTE(v1) = sub_4C3F64((int)a1, v9, 0);
    }
  }
  return v1;
}


// ==================== [0x4C9860] sub_4C9860 (size: 756) ====================
Tecanvas::TDarkGrayPen *__usercall sub_4C9860@<eax>(Tecanvas::TDarkGrayPen *a1@<eax>, char a2@<dl>, double a3@<st0>)
{
  char v4; // [esp+3h] [ebp-5h]
  Tecanvas::TDarkGrayPen *v5; // [esp+4h] [ebp-4h]

  if ( a2 != 0 )
    a1 = (Tecanvas::TDarkGrayPen *)unknown_libname_26();
  v4 = a2;
  v5 = a1;
  Tecanvas::TDarkGrayPen::TDarkGrayPen(a1);
  *((_BYTE *)v5 + 570) = 0;
  *((_BYTE *)v5 + 1492) = 0;
  *((_BYTE *)v5 + 1497) = 0;
  *((_BYTE *)v5 + 1495) = 0;
  *((_BYTE *)v5 + 1494) = 0;
  *((_BYTE *)v5 + 1493) = 0;
  *((_BYTE *)v5 + 1468) = 0;
  *((_BYTE *)v5 + 1688) = 0;
  *((_DWORD *)v5 + 423) = 0;
  *((_DWORD *)v5 + 424) = GetTickCount();
  *((_BYTE *)v5 + 788) = 1;
  *((_BYTE *)v5 + 1496) = 0;
  *((_BYTE *)v5 + 1576) = 0;
  *((_DWORD *)v5 + 395) = 0;
  *((_BYTE *)v5 + 1668) = 0;
  unknown_libname_34((char *)v5 + 1676);
  *((_DWORD *)v5 + 420) = 0;
  *((_DWORD *)v5 + 421) = 0;
  *((_DWORD *)v5 + 218) = sub_407B30(v5);
  *((_DWORD *)v5 + 219) = 250;
  *((_DWORD *)v5 + 216) = 1000;
  *((_DWORD *)v5 + 217) = GetTickCount();
  *((_DWORD *)v5 + 121) = 12;
  *((_BYTE *)v5 + 1704) = 0;
  *((_BYTE *)v5 + 1706) = 0;
  *((_BYTE *)v5 + 1707) = 0;
  *((_BYTE *)v5 + 1708) = 0;
  *((_BYTE *)v5 + 1705) = 0;
  *((_DWORD *)v5 + 349) = GetTickCount();
  *((_DWORD *)v5 + 350) = GetTickCount();
  *((_DWORD *)v5 + 351) = GetTickCount();
  *((_BYTE *)v5 + 1408) = 0;
  *((_DWORD *)v5 + 382) = 0;
  *((_DWORD *)v5 + 383) = 0;
  *((_DWORD *)v5 + 384) = GetTickCount();
  *((_DWORD *)v5 + 385) = 0;
  *((_DWORD *)v5 + 386) = GetTickCount();
  *((_DWORD *)v5 + 387) = GetTickCount();
  *((_DWORD *)v5 + 388) = 0;
  *((_DWORD *)v5 + 389) = 0;
  *((_DWORD *)v5 + 390) = 0;
  *((_DWORD *)v5 + 391) = 0;
  *((_DWORD *)v5 + 392) = 0;
  *((_DWORD *)v5 + 393) = 0;
  *((_DWORD *)v5 + 343) = unknown_libname_22(off_40FAFC, 1);
  unknown_libname_34((char *)v5 + 1376);
  *((_DWORD *)v5 + 345) = 0;
  *((_DWORD *)v5 + 346) = GetTickCount();
  *((_BYTE *)v5 + 1388) = 0;
  *((_DWORD *)v5 + 348) = GetTickCount();
  Sysutils::Now();
  *((double *)v5 + 182) = a3;
  *((_DWORD *)v5 + 366) = GetTickCount();
  *((_DWORD *)v5 + 353) = 0;
  *((_DWORD *)v5 + 354) = 0;
  *((_BYTE *)v5 + 1497) = 0;
  *((_BYTE *)v5 + 1516) = 0;
  *((_DWORD *)v5 + 375) = 0;
  *((_DWORD *)v5 + 428) = GetTickCount();
  *((_DWORD *)v5 + 429) = 0;
  if ( v4 != 0 )
    unknown_libname_27(v5);
  return v5;
}


// ==================== [0x4C9C24] sub_4C9C24 (size: 2467) ====================
int __usercall sub_4C9C24@<eax>(int a1@<eax>, double a2@<st0>)
{
  char v2; // al
  int v3; // eax
  int v4; // eax
  int v5; // eax
  int v6; // eax
  char v7; // zf
  int v8; // eax
  int v9; // edx
  int v10; // ecx
  int v11; // eax
  int v12; // eax
  int v13; // eax
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-Ch] [ebp-68h] BYREF
  void *v16; // [esp-8h] [ebp-64h]
  int *v17; // [esp-4h] [ebp-60h]
  void *v18; // [esp+0h] [ebp-5Ch]
  int v19; // [esp+Ch] [ebp-50h] BYREF
  int v20; // [esp+10h] [ebp-4Ch] BYREF
  int v21; // [esp+14h] [ebp-48h]
  int v22; // [esp+18h] [ebp-44h] BYREF
  int v23; // [esp+1Ch] [ebp-40h] BYREF
  int v24; // [esp+20h] [ebp-3Ch]
  int v25; // [esp+3Ch] [ebp-20h]
  int v26; // [esp+40h] [ebp-1Ch]
  void *Mem; // [esp+44h] [ebp-18h]
  int v28; // [esp+48h] [ebp-14h] BYREF
  int v29; // [esp+4Ch] [ebp-10h]
  int v30; // [esp+50h] [ebp-Ch]
  int i; // [esp+54h] [ebp-8h]
  int v32; // [esp+58h] [ebp-4h]
  int savedregs; // [esp+5Ch] [ebp+0h] BYREF

  v19 = 0;
  v20 = 0;
  v21 = 0;
  v22 = 0;
  v23 = 0;
  v24 = 0;
  v28 = 0;
  v32 = a1;
  v17 = &savedregs;
  v16 = &loc_4CA5B9;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  if ( *off_4EA41C[0] != 0 )
  {
    if ( *(unsigned __int8 *)(v32 + 52) < *off_4EA1E8 )
      *(_BYTE *)(v32 + 52) = *(_BYTE *)off_4EA1E8;
    if ( *off_4EA214[0] > *(_DWORD *)(v32 + 48) )
      *(_DWORD *)(v32 + 48) = *off_4EA214[0];
  }
  if ( *off_4EA41C[0] != 0 || *off_4EA3F8 != 0 )
    *(_DWORD *)(v32 + 1476) = 3;
  *(_DWORD *)(v32 + 920) = GetTickCount();
  Sysutils::Now();
  *(double *)(v32 + 1456) = a2;
  *(_DWORD *)(v32 + 1464) = GetTickCount();
  sub_4BA4B8(v32);
  sub_4B865C(v32, v32, 10050, 0, 0, 0, 0, 0);
  if ( *(_BYTE *)(v32 + 52) <= 7u && sub_4C9BD0(v32) >= 80 )
    sub_4BCB54(v32, *(_DWORD *)(*(_DWORD *)(v32 + 760) + 4), 0);
  if ( *(_BYTE *)(v32 + 1576) != 0 )
    sub_4BCB54(v32, *(_DWORD *)(*(_DWORD *)(v32 + 760) + 4), 0);
  unknown_libname_41();
  v2 = Iniohandlerstack::TInIOHandlerStack::WriteDirect(*off_4EA490, v24);
  *(_BYTE *)(v32 + 621) = v2;
  sub_4DA734(v32);
  if ( *(_DWORD *)(*(_DWORD *)(v32 + 1052) + 8) - 1 >= 0 )
  {
    v25 = *(_DWORD *)(*(_DWORD *)(v32 + 1052) + 8);
    i = 0;
    do
    {
      v3 = Classes::TList::Get(*(Classes::TList **)(v32 + 1052), i);
      sub_4C713C(v32, v3);
      ++i;
      --v25;
    }
    while ( v25 != 0 );
  }
  if ( *(_BYTE *)(v32 + 1704) != 0 )
  {
    Mem = (void *)System::__linkproc__ GetMem(24);
    if ( (unsigned __int8)sub_4AC404(*off_4EA490, *off_4EA090, Mem) != 0 )
      Classes::TList::Add(*(Classes::TList **)(v32 + 1036), Mem);
    else
      System::__linkproc__ FreeMem(Mem, 24);
    Mem = (void *)System::__linkproc__ GetMem(24);
    if ( (unsigned __int8)sub_4AC404(*off_4EA490, *off_4EA0F8, Mem) != 0 )
      Classes::TList::Add(*(Classes::TList **)(v32 + 1036), Mem);
    else
      System::__linkproc__ FreeMem(Mem, 24);
    Mem = (void *)System::__linkproc__ GetMem(24);
    if ( (unsigned __int8)sub_4AC404(*off_4EA490, *off_4EA548, Mem) != 0 )
      Classes::TList::Add(*(Classes::TList **)(v32 + 1036), Mem);
    else
      System::__linkproc__ FreeMem(Mem, 24);
    if ( *(_BYTE *)(v32 + 45) != 0 )
    {
      Mem = (void *)System::__linkproc__ GetMem(24);
      if ( (unsigned __int8)sub_4AC404(*off_4EA490, *off_4EA43C[0], Mem) != 0 )
        goto LABEL_29;
    }
    else
    {
      Mem = (void *)System::__linkproc__ GetMem(24);
      if ( (unsigned __int8)sub_4AC404(*off_4EA490, *off_4EA634, Mem) != 0 )
      {
LABEL_29:
        Classes::TList::Add(*(Classes::TList **)(v32 + 1036), Mem);
        goto LABEL_33;
      }
    }
    System::__linkproc__ FreeMem(Mem, 24);
  }
LABEL_33:
  if ( *(_DWORD *)(*(_DWORD *)(v32 + 1036) + 8) - 1 >= 0 )
  {
    i = *(_DWORD *)(*(_DWORD *)(v32 + 1036) + 8) - 1;
    do
    {
      v4 = Classes::TList::Get(*(Classes::TList **)(v32 + 1036), i);
      sub_4AC1AC(*off_4EA490, *(unsigned __int16 *)(v4 + 4), &v23);
      if ( v23 == 0 )
      {
        v5 = Classes::TList::Get(*(Classes::TList **)(v32 + 1036), i);
        System::__linkproc__ FreeMem(v5, 24);
        Classes::TList::Delete(*(Classes::TList **)(v32 + 1036), i);
      }
      --i;
    }
    while ( i != -1 );
  }
  for ( i = 0; i != 9; ++i )
  {
    if ( *(_WORD *)(v32 + 24 * i + 1060) != 0 )
    {
      v26 = sub_4AC2F8(*off_4EA490, *(unsigned __int16 *)(v32 + 24 * i + 1060));
      if ( v26 != 0 )
      {
        if ( (unsigned __int8)sub_4B2D70(i, v26) == 0 )
        {
          Mem = (void *)System::__linkproc__ GetMem(24);
          qmemcpy(Mem, (const void *)(v32 + 24 * i + 1056), 0x18u);
          sub_4C4DB8(v32, Mem);
          *(_WORD *)(v32 + 24 * i + 1060) = 0;
        }
      }
      else
      {
        *(_WORD *)(v32 + 24 * i + 1060) = 0;
      }
    }
  }
  if ( *(_DWORD *)(*(_DWORD *)(v32 + 1036) + 8) - 1 >= 0 )
  {
    i = *(_DWORD *)(*(_DWORD *)(v32 + 1036) + 8) - 1;
    do
    {
      v6 = Classes::TList::Get(*(Classes::TList **)(v32 + 1036), i);
      sub_4AC1AC(*off_4EA490, *(unsigned __int16 *)(v6 + 4), &v28);
      v29 = *(_DWORD *)Classes::TList::Get(*(Classes::TList **)(v32 + 1036), i);
      if ( i - 1 >= 0 )
      {
        v30 = i - 1;
        while ( 1 )
        {
          Mem = (void *)Classes::TList::Get(*(Classes::TList **)(v32 + 1036), v30);
          sub_4AC1AC(*off_4EA490, *((unsigned __int16 *)Mem + 2), &v22);
          System::__linkproc__ LStrCmp(v28, v22);
          if ( v7 )
          {
            if ( *(_DWORD *)Mem == v29 )
              break;
          }
          if ( --v30 == -1 )
            goto LABEL_53;
        }
        System::__linkproc__ FreeMem(Mem, 24);
        Classes::TList::Delete(*(Classes::TList **)(v32 + 1036), v30);
      }
LABEL_53:
      --i;
    }
    while ( i != -1 );
  }
  for ( i = 0; i != 12; ++i )
  {
    if ( *(_WORD *)(v32 + 2 * i + 96) != 0 )
      *(_DWORD *)(v32 + 4 * i + 488) = GetTickCount();
  }
  v8 = sub_4BA43C(v32);
  *(_DWORD *)(v32 + 92) = v8;
  sub_4BF7DC(v32);
  unknown_libname_25(v32, v9, v10);
  v11 = sub_4BEDC8(v32, *(unsigned __int8 *)(v32 + 52));
  *(_DWORD *)(v32 + 80) = v11;
  if ( *(_BYTE *)(v32 + 178) == 0 )
  {
    *(_DWORD *)(v32 + 172) = 0;
    ++*(_BYTE *)(v32 + 178);
  }
  if ( *(int *)(v32 + 48) > 10000000 )
    *(_DWORD *)(v32 + 48) = 10000000;
  if ( *(_BYTE *)(v32 + 1707) == 0 )
  {
    if ( *(int *)(v32 + 1452) < 20020522 )
    {
      sub_4BB124(v32, &str_Verson_Error[1], 0);
      sub_4BB124(v32, dword_4CA5E8, 0);
      sub_4BB124(v32, dword_4CA610, 0);
      *(_BYTE *)(v32 + 1492) = 1;
      goto LABEL_95;
    }
    switch ( *(_BYTE *)(v32 + 673) )
    {
      case 0:
        sub_4BB124(v32, aModeAttackAll, 1);
        break;
      case 1:
        sub_4BB124(v32, aModePeaceful, 1);
        break;
      case 2:
        sub_4BB124(v32, aModeGroupAttac, 1);
        break;
      case 3:
        sub_4BB124(v32, aModeGuildAttac, 1);
        break;
      case 4:
        sub_4BB124(v32, aModeRedWhite, 1);
        break;
      default:
        break;
    }
    sub_4BB124(v32, &str_To_change_do__C[1], 1);
    if ( *off_4EA41C[0] != 0 )
    {
      sub_4BB124(v32, aWelcomeToOurSe, 1);
      if ( sub_4AE7C0(*off_4EA490) > *off_4EA0C8 && *(_BYTE *)(v32 + 621) < 2u )
      {
        sub_4BB124(v32, &str________________[1], 0);
        sub_4BB124(v32, dword_4CA610, 0);
        *(_BYTE *)(v32 + 1492) = 1;
      }
    }
  }
  ExceptionList = (struct _EXCEPTION_REGISTRATION_RECORD *)v32;
  if ( *(_DWORD *)(v32 + 800) != 0 && (unsigned __int8)sub_490400(*off_4EA080[0], *(_DWORD *)(v32 + 800)) != 0 )
  {
    BYTE2(ExceptionList[84].Next) = 21;
  }
  else if ( BYTE1(ExceptionList[6].Handler) == 1 )
  {
    BYTE2(ExceptionList[84].Next) = -110;
  }
  *(_DWORD *)(v32 + 1700) = *off_4E9F7C;
  sub_4B865C(v32, v32, 10051, 0, 0, 0, 0, 0);
  sub_4B865C(v32, v32, 10302, 0, 0, 0, 0, 0);
  sub_4B865C(v32, v32, 10053, 0, 0, 0, 0, 0);
  sub_4B865C(v32, v32, 10114, 0, 0, 0, 0, 0);
  sub_4B865C(v32, v32, 10122, 0, 0, 0, 0, 0);
  unknown_libname_41();
  v12 = sub_49A408(*off_4EA07C[0], v21);
  *(_DWORD *)(v32 + 800) = v12;
  if ( *(_DWORD *)(v32 + 800) != 0 )
  {
    sub_4987F0(&v20);
    unknown_libname_36(v32 + 808, v20);
    v13 = (*(int (__fastcall **)(_DWORD))(**(_DWORD **)(*(_DWORD *)(v32 + 800) + 12) + 20))(*(_DWORD *)(*(_DWORD *)(v32 + 800) + 12))
        - 1;
    if ( v13 >= 0 )
    {
      v25 = v13 + 1;
      i = 0;
      do
      {
        (*(void (__fastcall **)(_DWORD, int, int *, int *, void *))(**(_DWORD **)(*(_DWORD *)(v32 + 800) + 12) + 12))(
          *(_DWORD *)(*(_DWORD *)(v32 + 800) + 12),
          i,
          &v19,
          v17,
          v16);
        System::__linkproc__ LStrCat((int)&v19, aIsOnAGuildWarW);
        sub_4BB124(v32, v19, 1);
        ++i;
        --v25;
      }
      while ( v25 != 0 );
    }
  }
  if ( *(_DWORD *)(v32 + 1476) == 1 )
  {
    if ( *(_BYTE *)(v32 + 1707) == 0 )
      sub_4BB124(v32, dword_4CA790, 1);
    *(_DWORD *)(v32 + 616) = 100000;
    if ( *(_BYTE *)(v32 + 52) > 7u )
    {
      sub_4BB124(v32, dword_4CA7D8, 0);
      sub_4BB124(v32, dword_4CA7FC, 0);
      *(_BYTE *)(v32 + 1492) = 1;
    }
  }
  if ( *(_DWORD *)(v32 + 1476) == 3 && *(_BYTE *)(v32 + 1707) == 0 )
    sub_4BB124(v32, dword_4CA84C, 1);
  if ( *off_4EA4E4[0] != 0 )
    sub_4BB124(v32, &str_________________0[1], 1);
  if ( *(_DWORD *)(v32 + 1312) != 0 && *(_BYTE *)(v32 + 1325) == 0 )
  {
    *(_BYTE *)(v32 + 1325) = 1;
    sub_4CAB38(v32, 0, &str__LNG[1]);
  }
  if ( *(_BYTE *)(*(_DWORD *)(v32 + 760) + 49) != 0 )
    sub_4BCB54(v32, *(_DWORD *)(*(_DWORD *)(v32 + 760) + 56), 0);
LABEL_95:
  __writefsdword(0, (unsigned int)v16);
  v18 = &loc_4CA5C0;
  unknown_libname_35(&v19, 6);
  return unknown_libname_34(&v28);
}


// ==================== [0x4CA994] sub_4CA994 (size: 379) ====================
int __usercall sub_4CA994@<eax>(_DWORD *a1@<eax>, unsigned __int64 a2@<st0>)
{
  int v2; // ecx
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp+30h] [ebp-34h] BYREF
  void *v5; // [esp+34h] [ebp-30h]
  int *v6; // [esp+38h] [ebp-2Ch]
  int v7; // [esp+3Ch] [ebp-28h] BYREF
  int v8; // [esp+58h] [ebp-Ch] BYREF
  DWORD v9; // [esp+5Ch] [ebp-8h]
  _DWORD *v10; // [esp+60h] [ebp-4h]
  int savedregs; // [esp+64h] [ebp+0h] BYREF

  v10 = a1;
  v6 = &savedregs;
  v5 = &loc_4CAB04;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  if ( a1[369] == 2 || *off_4EA41C[0] != 0 )
    v9 = (GetTickCount() - v10[366]) / 0x3E8;
  else
    v9 = 0;
  unknown_libname_41();
  unknown_libname_41();
  unknown_libname_91(v9, 0);
  unknown_libname_105(v10[364], v10[365]);
  Sysutils::Now();
  unknown_libname_105(a2, HIDWORD(a2));
  Sysutils::IntToStr(v10[370]);
  System::__linkproc__ LStrCatN(&v8, 13, v2);
  sub_4E437C(v8);
  if ( v10[370] == 2 )
  {
    unknown_libname_41();
    sub_48D53C(*off_4EA3DC, v7, v9 / 0x3C, v6, v5, ExceptionList);
  }
  __writefsdword(0, (unsigned int)ExceptionList);
  v6 = (int *)&loc_4CAB0B;
  return unknown_libname_35(&v7, 8);
}


// ==================== [0x4CAB38] sub_4CAB38 (size: 530) ====================
int __fastcall sub_4CAB38(int a1, void *a2, int a3)
{
  int v3; // eax
  const void *v4; // eax
  const void *v5; // eax
  int v7; // [esp-1Ch] [ebp-50h]
  int v8; // [esp-1Ch] [ebp-50h]
  unsigned int v9[3]; // [esp-18h] [ebp-4Ch] BYREF
  unsigned int v10[2]; // [esp-Ch] [ebp-40h] BYREF
  int *v11; // [esp-4h] [ebp-38h]
  _DWORD v12[2]; // [esp+Ch] [ebp-28h] BYREF
  __int16 v13; // [esp+14h] [ebp-20h]
  __int16 v14; // [esp+16h] [ebp-1Eh]
  int v15; // [esp+1Ch] [ebp-18h]
  void *Mem; // [esp+20h] [ebp-14h]
  unsigned int v17; // [esp+24h] [ebp-10h] BYREF
  int v18; // [esp+28h] [ebp-Ch] BYREF
  void *v19; // [esp+2Ch] [ebp-8h]
  int v20; // [esp+30h] [ebp-4h]
  int savedregs; // [esp+34h] [ebp+0h] BYREF

  v18 = a3;
  v19 = a2;
  v20 = a1;
  unknown_libname_44(a3);
  v11 = &savedregs;
  v10[1] = (unsigned int)&loc_4CAD3C;
  v10[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v10);
  Mem = nullptr;
  v9[2] = (unsigned int)&savedregs;
  v9[1] = (unsigned int)&loc_4CAD12;
  v9[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v9);
  v12[0] = -1437226411;
  v12[1] = *(_DWORD *)(v20 + 1436);
  v13 = *(_WORD *)(v20 + 1440);
  v14 = 5;
  if ( v19 != nullptr )
  {
    if ( v18 != 0 )
    {
      v3 = unknown_libname_43(v18);
      v15 = v3 + 13;
      v17 = v3 + 33;
      Mem = (void *)System::__linkproc__ GetMem(v3 + 37);
      System::Move(&v17, Mem, 4);
      System::Move(v12, (char *)Mem + 4, 20);
      System::Move(v19, (char *)Mem + 24, 12);
      v7 = unknown_libname_43(v18) + 1;
      v4 = (const void *)System::UniqueString(&v18);
      System::Move(v4, (char *)Mem + 36, v7);
    }
    else
    {
      v15 = 12;
      v17 = 32;
      Mem = (void *)System::__linkproc__ GetMem(36);
      System::Move(&v17, Mem, 4);
      System::Move(v12, (char *)Mem + 4, 20);
      System::Move(v19, (char *)Mem + 24, 12);
    }
  }
  else if ( v18 != 0 )
  {
    v15 = -(unknown_libname_43(v18) + 1);
    v17 = abs32(v15) + 20;
    Mem = (void *)System::__linkproc__ GetMem(v17 + 4);
    System::Move(&v17, Mem, 4);
    System::Move(v12, (char *)Mem + 4, 20);
    v8 = unknown_libname_43(v18) + 1;
    v5 = (const void *)System::UniqueString(&v18);
    System::Move(v5, (char *)Mem + 24, v8);
  }
  sub_4E0C1C(*off_4EA628[0], *(_DWORD *)(v20 + 1448), Mem);
  __writefsdword(0, v9[0]);
  __writefsdword(0, v10[0]);
  v11 = (int *)&loc_4CAD43;
  return unknown_libname_34(&v18);
}


// ==================== [0x4CAF08] sub_4CAF08 (size: 511) ====================
char __usercall sub_4CAF08@<al>(int a1@<eax>, int a2@<edx>, int a3@<ecx>, unsigned __int64 a4@<st0>)
{
  int v4; // ecx
  int v5; // ecx
  unsigned int v7[2]; // [esp+Ch] [ebp-74h] BYREF
  int *v8; // [esp+14h] [ebp-6Ch]
  void *v9; // [esp+1Ch] [ebp-64h] BYREF
  _BYTE v10[32]; // [esp+20h] [ebp-60h] BYREF
  _BYTE v11[28]; // [esp+40h] [ebp-40h] BYREF
  _DWORD v12[4]; // [esp+5Ch] [ebp-24h] BYREF
  int v13; // [esp+6Ch] [ebp-14h]
  char v14; // [esp+73h] [ebp-Dh]
  int v15; // [esp+74h] [ebp-Ch]
  int v16; // [esp+78h] [ebp-8h]
  int v17; // [esp+7Ch] [ebp-4h]
  int savedregs; // [esp+80h] [ebp+0h] BYREF

  v12[0] = 0;
  v9 = nullptr;
  v15 = a3;
  v16 = a2;
  v17 = a1;
  v8 = &savedregs;
  v7[1] = (unsigned int)&loc_4CB0F8;
  v7[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v7);
  v14 = 0;
  if ( GetTickCount() - *(_DWORD *)(a1 + 1548) >= 0x258 )
  {
    *(_DWORD *)(v17 + 1564) = 0;
    if ( *(int *)(v17 + 1568) > 0 )
      --*(_DWORD *)(v17 + 1568);
  }
  else
  {
    ++*(_DWORD *)(v17 + 1564);
    ++*(_DWORD *)(v17 + 1568);
  }
  *(_DWORD *)(v17 + 1548) = GetTickCount();
  if ( *(int *)(v17 + 1564) >= 4 || *(int *)(v17 + 1568) >= 6 )
  {
    if ( (int)++*(_DWORD *)(v17 + 1572) > 0 )
      *(_BYTE *)(v17 + 1492) = 0;
    if ( *off_4EA04C != 1 )
    {
      unknown_libname_16(v11, a11002Walk);
      LOBYTE(v4) = 27;
      System::__linkproc__ PStrNCat(v11, v17 + 21, v4);
      unknown_libname_16(v10, v11);
      LOBYTE(v5) = 28;
      System::__linkproc__ PStrNCat(v10, &dword_4CB118, v5);
      unknown_libname_41();
      Sysutils::Time();
      unknown_libname_104(a4, HIDWORD(a4));
      System::__linkproc__ LStrCat((int)v12, v9);
      sub_4E4274(v12[0]);
    }
  }
  else
  {
    *(_BYTE *)(v17 + 790) = 0;
    v12[3] = *(_DWORD *)(v17 + 36);
    v12[2] = *(_DWORD *)(v17 + 40);
    v13 = (unsigned __int8)sub_4B2C38(*(_DWORD *)(v17 + 36), *(_DWORD *)(v17 + 40), v16, v15);
    if ( sub_4C3F64(v17, v13, 0) != 0 )
    {
      if ( *(_BYTE *)(v17 + 790) != 0 || *(_DWORD *)(v17 + 36) == v16 && *(_DWORD *)(v17 + 40) == v15 )
        v14 = 1;
      *(_DWORD *)(v17 + 880) -= 10;
    }
    else
    {
      *(_DWORD *)(v17 + 1564) = 0;
      *(_DWORD *)(v17 + 1568) = 0;
    }
  }
  __writefsdword(0, v7[0]);
  v8 = (int *)&loc_4CB0FF;
  unknown_libname_34(&v9);
  unknown_libname_34(v12);
  return v14;
}


// ==================== [0x4CB11C] sub_4CB11C (size: 578) ====================
char __usercall sub_4CB11C@<al>(int *a1@<eax>, int a2@<edx>, int a3@<ecx>, unsigned __int64 a4@<st0>)
{
  DWORD TickCount; // edx
  int *v5; // eax
  int v6; // ecx
  int v7; // eax
  int v8; // ecx
  int v9; // ecx
  unsigned int v11[2]; // [esp+Ch] [ebp-64h] BYREF
  int *v12; // [esp+14h] [ebp-5Ch]
  void *v13; // [esp+1Ch] [ebp-54h] BYREF
  _BYTE v14[28]; // [esp+20h] [ebp-50h] BYREF
  _BYTE v15[28]; // [esp+3Ch] [ebp-34h] BYREF
  _DWORD v16[2]; // [esp+58h] [ebp-18h] BYREF
  unsigned __int8 v17; // [esp+62h] [ebp-Eh]
  char v18; // [esp+63h] [ebp-Dh]
  int v19; // [esp+64h] [ebp-Ch]
  int v20; // [esp+68h] [ebp-8h]
  int *v21; // [esp+6Ch] [ebp-4h]
  int savedregs; // [esp+70h] [ebp+0h] BYREF

  v16[0] = 0;
  v13 = nullptr;
  v19 = a3;
  v20 = a2;
  v21 = a1;
  v12 = &savedregs;
  v11[1] = (unsigned int)&loc_4CB34F;
  v11[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v11);
  v18 = 0;
  if ( GetTickCount() - a1[387] >= 0x258 )
  {
    v21[391] = 0;
    if ( v21[392] > 0 )
      --v21[392];
  }
  else
  {
    ++v21[391];
    ++v21[392];
  }
  TickCount = GetTickCount();
  v5 = v21;
  v21[387] = TickCount;
  if ( v5[391] >= 4 || v5[392] > 6 || (*((_BYTE *)v5 + 790) = 0, (v5[23] & 0x4000000) != 0) )
  {
    if ( ++v21[393] > 0 )
      *((_BYTE *)v21 + 1492) = 0;
    if ( *off_4EA04C != 1 )
    {
      unknown_libname_16(v15, dword_4CB360);
      LOBYTE(v8) = 26;
      System::__linkproc__ PStrNCat(v15, (char *)v21 + 21, v8);
      unknown_libname_16(v14, v15);
      LOBYTE(v9) = 27;
      System::__linkproc__ PStrNCat(v14, &dword_4CB370, v9);
      unknown_libname_41();
      Sysutils::Time();
      unknown_libname_104(a4, HIDWORD(a4));
      System::__linkproc__ LStrCat((int)v16, v13);
      sub_4E4274(v16[0]);
    }
  }
  else
  {
    v17 = sub_4B2C38(v21[9], v21[10], v20, v19);
    if ( sub_4C42C0(v21, v17) != 0 )
    {
      if ( *((_BYTE *)v21 + 840) != 0 && *((_BYTE *)v21 + 836) != 0 )
        *((_WORD *)v21 + 56) = 1;
      if ( *((_BYTE *)v21 + 790) != 0 || v21[9] == v20 && v21[10] == v19 )
        v18 = 1;
      v21[220] -= 60;
      v21[221] -= 10;
      v7 = Ibutils::Max(nullptr, v21[221], v6);
      v21[221] = v7;
      --v21[149];
      --v21[151];
    }
    else
    {
      v21[391] = 0;
      v21[392] = 0;
    }
  }
  __writefsdword(0, v11[0]);
  v12 = (int *)&loc_4CB356;
  unknown_libname_34(&v13);
  unknown_libname_34(v16);
  return v18;
}


// ==================== [0x4CB3AC] sub_4CB3AC (size: 635) ====================
int __fastcall sub_4CB3AC(int a1)
{
  int result; // eax
  unsigned int v2; // eax
  bool v3; // cf
  unsigned int v4; // eax
  unsigned int v5; // eax
  unsigned int v6; // eax
  _WORD *Mem; // [esp+0h] [ebp-8h]
  int savedregs; // [esp+8h] [ebp+0h] BYREF

  result = *(_DWORD *)(a1 + 1036);
  if ( *(int *)(result + 8) < 46 )
  {
    v2 = System::__linkproc__ RandInt(120) - 1;
    v3 = v2 < 2;
    v4 = v2 - 2;
    if ( v3 )
    {
      Mem = (_WORD *)System::__linkproc__ GetMem(24);
      if ( (unsigned __int8)sub_4AC404(*off_4EA490, *off_4EA088, Mem) != 0 )
        goto LABEL_18;
    }
    else
    {
      v3 = v4 < 0x12;
      v5 = v4 - 18;
      if ( v3 )
      {
        Mem = (_WORD *)System::__linkproc__ GetMem(24);
        if ( (unsigned __int8)sub_4AC404(*off_4EA490, *off_4EA62C, Mem) != 0 )
          goto LABEL_18;
      }
      else
      {
        v3 = v5 < 0x19;
        v6 = v5 - 25;
        if ( v3 )
        {
          Mem = (_WORD *)System::__linkproc__ GetMem(24);
          if ( (unsigned __int8)sub_4AC404(*off_4EA490, *off_4EA2F0[0], Mem) != 0 )
            goto LABEL_18;
        }
        else
        {
          if ( v6 >= 0xB )
          {
            Mem = (_WORD *)System::__linkproc__ GetMem(24);
            if ( (unsigned __int8)sub_4AC404(*off_4EA490, &str_Copp[1], Mem) != 0 )
              goto LABEL_18;
            return System::__linkproc__ FreeMem(Mem, 24);
          }
          Mem = (_WORD *)System::__linkproc__ GetMem(24);
          if ( (unsigned __int8)sub_4AC404(*off_4EA490, &str_BlackOre[1], Mem) != 0 )
          {
LABEL_18:
            Mem[3] = sub_4CB374(&savedregs);
            Classes::TList::Add(*(Classes::TList **)(a1 + 1036), Mem);
            sub_4C49BC(a1);
            return sub_4D0824(a1, Mem);
          }
        }
      }
    }
    return System::__linkproc__ FreeMem(Mem, 24);
  }
  return result;
}


// ==================== [0x4CB64C] sub_4CB64C (size: 413) ====================
char __fastcall sub_4CB64C(int a1, int a2, int a3)
{
  int v3; // eax
  unsigned int v5[2]; // [esp-Ch] [ebp-2Ch] BYREF
  int *v6; // [esp-4h] [ebp-24h]
  int v7; // [esp+4h] [ebp-1Ch] BYREF
  int v8; // [esp+8h] [ebp-18h]
  _DWORD *v9; // [esp+Ch] [ebp-14h]
  char v10; // [esp+13h] [ebp-Dh]
  int v11; // [esp+14h] [ebp-Ch]
  int v12; // [esp+18h] [ebp-8h]
  int v13; // [esp+1Ch] [ebp-4h]
  int savedregs; // [esp+20h] [ebp+0h] BYREF

  v7 = 0;
  v11 = a3;
  v12 = a2;
  v13 = a1;
  v6 = &savedregs;
  v5[1] = (unsigned int)&loc_4CB7DA;
  v5[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v5);
  v10 = 0;
  unknown_libname_34(&v7);
  v9 = (_DWORD *)sub_4B5D24(*(_DWORD *)(v13 + 760), v12, v11);
  if ( v9 != nullptr && v9[5] == 2 )
  {
    if ( (int)v9[16] <= 0 )
    {
      if ( GetTickCount() - v9[18] > 0x927C0 )
        sub_4A7DB8(v9);
    }
    else
    {
      --v9[16];
      if ( System::__linkproc__ RandInt(4) == 0 )
      {
        v8 = sub_4B5D24(*(_DWORD *)(v13 + 760), *(_DWORD *)(v13 + 36), *(_DWORD *)(v13 + 40));
        if ( v8 != 0 )
        {
          if ( *(_DWORD *)(v8 + 20) == 3 )
            sub_4A7E44(v8);
        }
        else
        {
          v8 = sub_4A7DDC(1, 300000, 3, *(_DWORD *)(v13 + 40), *(_DWORD *)(v13 + 36));
          sub_4A80EC(*off_4EA388[0], v8);
        }
        if ( System::__linkproc__ RandInt(12) == 0 )
          sub_4CB3AC(v13);
        unknown_libname_37(&v7, &str_1_3[1]);
        v3 = System::__linkproc__ RandInt(15);
        sub_4C17B4(v13, v3 + 5);
        v10 = 1;
      }
    }
  }
  sub_4B8EBC(v13, 10005, *(unsigned __int8 *)(v13 + 44), *(_DWORD *)(v13 + 36), *(_DWORD *)(v13 + 40), 0, v7);
  __writefsdword(0, v5[0]);
  v6 = (int *)&loc_4CB7E1;
  unknown_libname_34(&v7);
  return v10;
}


// ==================== [0x4CB7F8] sub_4CB7F8 (size: 1104) ====================
char __userpurge sub_4CB7F8@<al>(
        unsigned int a1@<eax>,
        int a2@<edx>,
        int a3@<ecx>,
        unsigned __int64 a4@<st0>,
        __int16 a5,
        int a6)
{
  __int64 v6; // rax
  int v7; // ecx
  int v8; // ecx
  int v9; // eax
  int v10; // eax
  int v11; // eax
  int v12; // ecx
  int v13; // ecx
  int v15; // [esp+0h] [ebp-78h]
  DWORD v16; // [esp+4h] [ebp-74h]
  unsigned int v17[2]; // [esp+Ch] [ebp-6Ch] BYREF
  int *v18; // [esp+14h] [ebp-64h]
  void *v19; // [esp+1Ch] [ebp-5Ch] BYREF
  _BYTE v20[28]; // [esp+20h] [ebp-58h] BYREF
  _BYTE v21[28]; // [esp+3Ch] [ebp-3Ch] BYREF
  int v22; // [esp+58h] [ebp-20h] BYREF
  int v23; // [esp+5Ch] [ebp-1Ch]
  int v24; // [esp+60h] [ebp-18h] BYREF
  int v25; // [esp+64h] [ebp-14h] BYREF
  char v26; // [esp+6Bh] [ebp-Dh]
  int v27; // [esp+6Ch] [ebp-Ch]
  int v28; // [esp+70h] [ebp-8h]
  unsigned int v29; // [esp+74h] [ebp-4h]
  int savedregs; // [esp+78h] [ebp+0h] BYREF

  v22 = 0;
  v19 = nullptr;
  v27 = a3;
  v28 = a2;
  v29 = a1;
  v18 = &savedregs;
  v17[1] = (unsigned int)&loc_4CBC37;
  v17[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v17);
  v26 = 0;
  v16 = GetTickCount() - *(_DWORD *)(a1 + 1544);
  v15 = 60 * *(char *)(v29 + 622);
  v6 = 900 - v15;
  if ( 900 - v15 < 0 )
  {
    if ( v6 >= 0 && 900 - v15 < 0 )
    {
LABEL_5:
      ++*(_DWORD *)(v29 + 1552);
      ++*(_DWORD *)(v29 + 1556);
      goto LABEL_8;
    }
  }
  else if ( (unsigned int)v6 > v16 )
  {
    goto LABEL_5;
  }
  *(_DWORD *)(v29 + 1552) = 0;
  if ( *(int *)(v29 + 1556) > 0 )
    --*(_DWORD *)(v29 + 1556);
LABEL_8:
  if ( *(int *)(v29 + 1552) >= 4 || *(int *)(v29 + 1556) >= 6 )
  {
    *(_DWORD *)(v29 + 1544) = GetTickCount();
    if ( (int)++*(_DWORD *)(v29 + 1572) > 8 )
      *(_BYTE *)(v29 + 1492) = 1;
    if ( *off_4EA04C != 0 )
    {
      unknown_libname_16(v21, dword_4CBC68);
      LOBYTE(v12) = 26;
      System::__linkproc__ PStrNCat(v21, v29 + 21, v12);
      unknown_libname_16(v20, v21);
      LOBYTE(v13) = 27;
      System::__linkproc__ PStrNCat(v20, &dword_4CBC78, v13);
      unknown_libname_41();
      Sysutils::Time();
      unknown_libname_104(a4, HIDWORD(a4));
      System::__linkproc__ LStrCat((int)&v22, v19);
      sub_4E4274(v22);
    }
  }
  else if ( *(_BYTE *)(v29 + 772) == 0 )
  {
    if ( v27 == *(_DWORD *)(v29 + 36) && a6 == *(_DWORD *)(v29 + 40) )
    {
      v26 = 1;
      *(_DWORD *)(v29 + 1544) = GetTickCount();
      if ( v28 == 3015
        && *(_WORD *)(v29 + 1084) != 0
        && sub_4B2790(v29, &v25, &v24) != 0
        && (unsigned __int8)sub_4B5ED0(0) == 0
        && (v23 = sub_4AC2F8(*off_4EA490, *(unsigned __int16 *)(v29 + 1084))) != 0
        && *(_BYTE *)(v23 + 16) == 19 )
      {
        if ( sub_4CB64C(v29, v25, v24) != 0 )
          sub_4CAB38(v29, nullptr, (int)&str__DIG[1]);
        *(_DWORD *)(v29 + 880) -= 30;
        *(_DWORD *)(v29 + 884) -= 50;
        v9 = Ibutils::Max(nullptr, *(_DWORD *)(v29 + 884), v8);
        *(_DWORD *)(v29 + 884) = v9;
        *(_DWORD *)(v29 + 596) -= 2;
        *(_DWORD *)(v29 + 604) -= 2;
      }
      else
      {
        if ( v28 == 3014 )
          sub_4C2C50(v29, 0, 0, a5);
        if ( v28 == 3015 )
          sub_4C2C50(v29, 0, 1, a5);
        if ( v28 == 3016 )
          sub_4C2C50(v29, 0, 2, a5);
        if ( v28 == 3018 )
          sub_4C2C50(v29, 0, 3, a5);
        if ( v28 == 3019 )
          sub_4C2C50(v29, 0, 4, a5);
        if ( v28 == 3024 )
          sub_4C2C50(v29, 0, 5, a5);
        if ( v28 == 3025 )
          sub_4C2C50(v29, 0, 7, a5);
        if ( *(_DWORD *)(v29 + 1308) != 0 && *(_WORD *)(v29 + 1084) != 0 )
        {
          if ( *(_DWORD *)(v29 + 820) == --*(_DWORD *)(v29 + 816) )
          {
            *(_BYTE *)(v29 + 1324) = 1;
            sub_4CAB38(v29, nullptr, (int)&str__PWR[1]);
          }
          if ( *(int *)(v29 + 816) <= 0 )
          {
            *(_DWORD *)(v29 + 816) = 7 - *(unsigned __int8 *)(*(_DWORD *)(v29 + 1308) + 6);
            v10 = System::__linkproc__ RandInt(*(_DWORD *)(v29 + 816));
            *(_DWORD *)(v29 + 820) = v10;
          }
        }
        *(_DWORD *)(v29 + 880) -= 30;
        *(_DWORD *)(v29 + 884) -= 100;
        v11 = Ibutils::Max(nullptr, *(_DWORD *)(v29 + 884), v7);
        *(_DWORD *)(v29 + 884) = v11;
        *(_DWORD *)(v29 + 596) -= 2;
        *(_DWORD *)(v29 + 604) -= 2;
      }
    }
    else
    {
      v26 = 0;
    }
  }
  __writefsdword(0, v17[0]);
  v18 = (int *)&loc_4CBC3E;
  unknown_libname_34(&v19);
  unknown_libname_34(&v22);
  return v26;
}


// ==================== [0x4CBCEC] sub_4CBCEC (size: 1272) ====================
char __userpurge sub_4CBCEC@<al>(int a1@<eax>, int a2@<edx>, int a3@<ecx>, unsigned __int64 a4@<st0>, int a5, int a6)
{
  int v6; // ecx
  int v7; // eax
  DWORD TickCount; // eax
  int v9; // edx
  int v10; // eax
  unsigned __int8 v11; // al
  char v12; // al
  int v13; // ecx
  int v14; // ecx
  unsigned int v16[2]; // [esp+Ch] [ebp-7Ch] BYREF
  int *v17; // [esp+14h] [ebp-74h]
  void *v18; // [esp+1Ch] [ebp-6Ch] BYREF
  _BYTE v19[28]; // [esp+20h] [ebp-68h] BYREF
  _BYTE v20[28]; // [esp+3Ch] [ebp-4Ch] BYREF
  _DWORD v21[4]; // [esp+58h] [ebp-30h] BYREF
  unsigned __int16 **v22; // [esp+68h] [ebp-20h]
  int v23; // [esp+6Ch] [ebp-1Ch]
  int v24; // [esp+70h] [ebp-18h]
  int v25; // [esp+74h] [ebp-14h]
  char v26; // [esp+7Bh] [ebp-Dh]
  int v27; // [esp+7Ch] [ebp-Ch]
  int v28; // [esp+80h] [ebp-8h]
  int v29; // [esp+84h] [ebp-4h]
  int savedregs; // [esp+88h] [ebp+0h] BYREF

  v21[0] = 0;
  v18 = nullptr;
  v27 = a3;
  v28 = a2;
  v29 = a1;
  v17 = &savedregs;
  v16[1] = (unsigned int)&loc_4CC1D3;
  v16[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v16);
  v26 = 0;
  if ( *(_WORD *)(a1 + 106) == 0 )
  {
    if ( GetTickCount() - *(_DWORD *)(v29 + 1536) <= *(_DWORD *)(v29 + 1540) )
      ++*(_DWORD *)(v29 + 1560);
    else
      *(_DWORD *)(v29 + 1560) = 0;
    if ( *(int *)(v29 + 1560) >= 1 )
    {
      v22 = (unsigned __int16 **)sub_4CBC7C(v29, v28);
      if ( v22 != nullptr && (unsigned __int8)sub_492190(*off_4EA674, *((unsigned __int16 *)v22 + 2)) != 0 )
      {
        *(_DWORD *)(v29 + 1560) = 0;
      }
      else
      {
        *(_DWORD *)(v29 + 1536) = GetTickCount();
        if ( (int)++*(_DWORD *)(v29 + 1572) > 8 )
          *(_BYTE *)(v29 + 1492) = 1;
        if ( *off_4EA04C != 0 )
        {
          unknown_libname_16(v20, dword_4CC234);
          LOBYTE(v13) = 26;
          System::__linkproc__ PStrNCat(v20, v29 + 21, v13);
          unknown_libname_16(v19, v20);
          LOBYTE(v14) = 27;
          System::__linkproc__ PStrNCat(v19, &dword_4CC244, v14);
          unknown_libname_41();
          Sysutils::Time();
          unknown_libname_104(a4, HIDWORD(a4));
          System::__linkproc__ LStrCat((int)v21, v18);
          sub_4E4274(v21[0]);
        }
      }
    }
    else
    {
      v22 = nullptr;
      *(_DWORD *)(v29 + 884) -= 450;
      v7 = Ibutils::Max(nullptr, *(_DWORD *)(v29 + 884), v6);
      *(_DWORD *)(v29 + 884) = v7;
      v22 = (unsigned __int16 **)sub_4CBC7C(v29, v28);
      if ( v22 != nullptr )
      {
        if ( (unsigned __int8)sub_492190(*off_4EA674, *((unsigned __int16 *)v22 + 2)) != 0 )
          *(_DWORD *)(v29 + 1540) = 0;
        else
          *(_DWORD *)(v29 + 1540) = *((_DWORD *)*v22 + 12) + 800;
        TickCount = GetTickCount();
        v9 = v29;
        *(_DWORD *)(v29 + 1536) = TickCount;
        switch ( *((_WORD *)v22 + 2) )
        {
          case 0xC:
            if ( *(_DWORD *)(v29 + 1312) != 0 )
            {
              if ( *(_BYTE *)(v29 + 1325) != 0 )
              {
                sub_4BC900(v29, 0);
                sub_4CAB38(v29, nullptr, (int)&str__ULNG[1]);
              }
              else
              {
                LOBYTE(v9) = 1;
                sub_4BC900(v29, v9);
                sub_4CAB38(v29, nullptr, (int)&str__LNG_0[1]);
              }
            }
            v26 = 1;
            break;
          case 0x19:
            if ( *(_DWORD *)(v29 + 1316) != 0 )
            {
              if ( *(_BYTE *)(v29 + 1326) != 0 )
              {
                sub_4BC980(v29, 0);
                sub_4CAB38(v29, nullptr, (int)&str__UWID[1]);
              }
              else
              {
                LOBYTE(v9) = 1;
                sub_4BC980(v29, v9);
                sub_4CAB38(v29, nullptr, (int)&str__WID[1]);
              }
            }
            v26 = 1;
            break;
          case 0x1A:
            if ( *(_DWORD *)(v29 + 1320) != 0 )
            {
              if ( (unsigned __int8)sub_4BCA00(v29) != 0 )
              {
                v24 = sub_4C6910(v29, v22);
                if ( *(unsigned __int16 *)(v29 + 422) >= v24 )
                {
                  if ( v24 > 0 )
                  {
                    sub_4BE50C(v29, v24);
                    sub_4C4A24(v29);
                  }
                  sub_4CAB38(v29, nullptr, (int)&str__FIR[1]);
                }
              }
              v26 = 1;
            }
            break;
          case 0x1B:
            v26 = 1;
            if ( GetTickCount() - *(_DWORD *)(v29 + 1332) > 0xBB8 )
            {
              *(_DWORD *)(v29 + 1332) = GetTickCount();
              *(_BYTE *)(v29 + 44) = v27;
              v24 = sub_4C6910(v29, v22);
              if ( v24 > 0 )
              {
                if ( *(unsigned __int16 *)(v29 + 422) < v24 )
                  break;
                sub_4BE50C(v29, v24);
                sub_4C4A24(v29);
              }
              if ( sub_4C3130(v29, *(unsigned __int8 *)(v29 + 44), *((unsigned __int8 *)v22 + 6)) != 0
                && *((_BYTE *)v22 + 6) < 3u
                && *((_BYTE *)*v22 + *((unsigned __int8 *)v22 + 6) + 22) <= *(_BYTE *)(v29 + 52) )
              {
                v10 = System::__linkproc__ RandInt(3);
                sub_4C1268(v29, v22, v10 + 1);
                if ( (unsigned __int8)sub_4C7054(v29, v22) == 0 )
                  sub_4B87C4(v29, v29, 10123, 0, **v22, *((unsigned __int8 *)v22 + 6), (int)v22[2], 0, 1000);
              }
            }
            break;
          default:
            v11 = sub_4B2C38(*(_DWORD *)(v29 + 36), *(_DWORD *)(v29 + 40), v27, a6);
            v25 = v11;
            *(_BYTE *)(v29 + 44) = v11;
            v23 = 0;
            v12 = sub_4C36CC(a6);
            if ( v12 != 0 )
            {
              v23 = a5;
              v27 = *(_DWORD *)(a5 + 36);
              a6 = *(_DWORD *)(a5 + 40);
            }
            if ( (unsigned __int8)sub_4C6968(v23, a6) == 0 )
              sub_4B8EBC(v29, 10121, 0, 0, 0, 0, 0);
            v26 = 1;
            break;
        }
      }
    }
  }
  __writefsdword(0, v16[0]);
  v17 = (int *)&loc_4CC1DA;
  unknown_libname_34(&v18);
  unknown_libname_34(v21);
  return v26;
}


// ==================== [0x4CC27C] sub_4CC27C (size: 367) ====================
int __fastcall sub_4CC27C(int a1, int a2, char a3)
{
  int v3; // ecx
  int v4; // eax
  int v5; // eax
  int v6; // ecx
  int v8; // [esp-18h] [ebp-38h]
  int v9; // [esp-14h] [ebp-34h]
  unsigned int v10[2]; // [esp-Ch] [ebp-2Ch] BYREF
  int *v11; // [esp-4h] [ebp-24h]
  int v12; // [esp+4h] [ebp-1Ch] BYREF
  int v13; // [esp+8h] [ebp-18h] BYREF
  int v14; // [esp+Ch] [ebp-14h]
  int v15; // [esp+10h] [ebp-10h]
  _BYTE v16[5]; // [esp+17h] [ebp-9h] BYREF
  int v17; // [esp+1Ch] [ebp-4h]
  int savedregs; // [esp+20h] [ebp+0h] BYREF

  v13 = 0;
  v12 = 0;
  v14 = 0;
  v16[0] = a3;
  *(_DWORD *)&v16[1] = a2;
  v17 = a1;
  unknown_libname_44(a2);
  v11 = &savedregs;
  v10[1] = (unsigned int)&loc_4CC3DF;
  v10[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v10);
  v16[0] = Ibutils::Min((Ibutils *)3, v16[0], v3);
  if ( *(_DWORD *)(*(_DWORD *)(v17 + 1052) + 8) - 1 >= 0 )
  {
    v15 = *(_DWORD *)(*(_DWORD *)(v17 + 1052) + 8) - 1;
    do
    {
      Classes::TList::Get(*(Classes::TList **)(v17 + 1052), v15);
      unknown_libname_41();
      if ( unknown_libname_84(v14, *(_DWORD *)&v16[1]) == 0 )
      {
        v4 = Classes::TList::Get(*(Classes::TList **)(v17 + 1052), v15);
        *(_BYTE *)(v4 + 6) = v16[0];
        v9 = **(unsigned __int16 **)Classes::TList::Get(*(Classes::TList **)(v17 + 1052), v15);
        v8 = *(unsigned __int8 *)(Classes::TList::Get(*(Classes::TList **)(v17 + 1052), v15) + 6);
        v5 = Classes::TList::Get(*(Classes::TList **)(v17 + 1052), v15);
        sub_4B865C(v17, v17, 10123, 0, v9, v8, *(_DWORD *)(v5 + 8), 0);
        Sysutils::IntToStr(v16[0]);
        System::__linkproc__ LStrCatN(&v13, 4, v6);
        sub_4BB124(v17, v13, 1);
      }
      --v15;
    }
    while ( v15 != -1 );
  }
  __writefsdword(0, v10[0]);
  v11 = (int *)&loc_4CC3E6;
  unknown_libname_35(&v12, 3);
  return unknown_libname_34(&v16[1]);
}


// ==================== [0x4CC8C4] sub_4CC8C4 (size: 322) ====================
int __fastcall sub_4CC8C4(int a1, int a2, int a3, unsigned __int8 a4)
{
  int v5; // [esp-14h] [ebp-40h]
  int v6; // [esp-10h] [ebp-3Ch]
  unsigned int v7[2]; // [esp-Ch] [ebp-38h] BYREF
  int *v8; // [esp-4h] [ebp-30h]
  int v9; // [esp+4h] [ebp-28h] BYREF
  int v10; // [esp+8h] [ebp-24h]
  void *v11; // [esp+Ch] [ebp-20h]
  int v12; // [esp+10h] [ebp-1Ch]
  int v13; // [esp+14h] [ebp-18h]
  int v14; // [esp+18h] [ebp-14h] BYREF
  int v15; // [esp+1Ch] [ebp-10h] BYREF
  int v16; // [esp+20h] [ebp-Ch] BYREF
  int v17; // [esp+24h] [ebp-8h]
  int v18; // [esp+28h] [ebp-4h]
  int savedregs; // [esp+2Ch] [ebp+0h] BYREF

  v9 = 0;
  v16 = a3;
  v17 = a2;
  v18 = a1;
  unknown_libname_44(a2);
  unknown_libname_44(v16);
  v8 = &savedregs;
  v7[1] = (unsigned int)&loc_4CC9F8;
  v7[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v7);
  v12 = sub_453B20(v16, 1);
  if ( a4 >= 8u )
    a4 = 0;
  if ( v12 - 1 >= 0 )
  {
    v10 = v12;
    v13 = 0;
    do
    {
      if ( *(int *)(*(_DWORD *)(v18 + 668) + 8) < 7 )
      {
        sub_4B2790(v18, &v15, &v14);
        v6 = v14;
        v5 = v17;
        unknown_libname_41();
        v11 = (void *)sub_4ADE3C(v5, v6);
        if ( v11 != nullptr )
        {
          *((_DWORD *)v11 + 162) = v18;
          *((_DWORD *)v11 + 163) = GetTickCount() + 86400000;
          *((_BYTE *)v11 + 665) = 3;
          *((_BYTE *)v11 + 664) = a4;
          sub_4BF0C4(v11);
          Classes::TList::Add(*(Classes::TList **)(v18 + 668), v11);
        }
      }
      ++v13;
      --v10;
    }
    while ( v10 != 0 );
  }
  __writefsdword(0, v7[0]);
  v8 = (int *)&loc_4CC9FF;
  unknown_libname_34(&v9);
  return unknown_libname_35(&v16, 2);
}


// ==================== [0x4CCA08] sub_4CCA08 (size: 363) ====================
int __fastcall sub_4CCA08(int a1, int a2, int a3)
{
  int v3; // ecx
  int v4; // ecx
  int v5; // ecx
  unsigned int v7[2]; // [esp-Ch] [ebp-84h] BYREF
  int *v8; // [esp-4h] [ebp-7Ch]
  _DWORD v9[2]; // [esp+4h] [ebp-74h] BYREF
  _BYTE v10[40]; // [esp+Ch] [ebp-6Ch] BYREF
  _BYTE v11[40]; // [esp+34h] [ebp-44h] BYREF
  int v12; // [esp+5Ch] [ebp-1Ch] BYREF
  int v13; // [esp+60h] [ebp-18h] BYREF
  int v14; // [esp+64h] [ebp-14h]
  int v15; // [esp+68h] [ebp-10h]
  int v16; // [esp+6Ch] [ebp-Ch] BYREF
  int v17; // [esp+70h] [ebp-8h]
  int v18; // [esp+74h] [ebp-4h]
  int savedregs; // [esp+78h] [ebp+0h] BYREF

  v13 = 0;
  v12 = 0;
  v9[1] = 0;
  v9[0] = 0;
  v16 = a3;
  v17 = a2;
  v18 = a1;
  unknown_libname_44(a2);
  unknown_libname_44(v16);
  v8 = &savedregs;
  v7[1] = (unsigned int)&loc_4CCB67;
  v7[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v7);
  if ( v17 != 0 )
  {
    v15 = sub_453B20(v17, 0);
    v14 = sub_453B20(v16, 0);
    *(_BYTE *)off_4EA5F8 = 1;
    unknown_libname_41();
    *off_4EA544[0] = v15;
    *off_4EA4C8 = v14;
    unknown_libname_16(v11, dword_4CCB8C);
    LOBYTE(v3) = 38;
    System::__linkproc__ PStrNCat(v11, v18 + 4, v3);
    unknown_libname_16(v10, v11);
    LOBYTE(v4) = 39;
    System::__linkproc__ PStrNCat(v10, dword_4CCBA4, v4);
    unknown_libname_41();
    Sysutils::IntToStr(v15);
    Sysutils::IntToStr(v14);
    System::__linkproc__ LStrCatN(&v13, 4, v5);
    sub_4BB124(v18, v13, 1);
  }
  else
  {
    *(_BYTE *)off_4EA5F8 = 0;
    sub_4BB124(v18, &str_Mission_Failed[1], 1);
  }
  __writefsdword(0, v7[0]);
  v8 = (int *)&loc_4CCB6E;
  unknown_libname_35(v9, 2);
  unknown_libname_35(&v12, 2);
  return unknown_libname_35(&v16, 2);
}


// ==================== [0x4CCBB4] sub_4CCBB4 (size: 529) ====================
int __fastcall sub_4CCBB4(int a1, int a2, int a3, int a4, int a5)
{
  int v5; // eax
  int v6; // ecx
  int v7; // ecx
  unsigned int v9[2]; // [esp-Ch] [ebp-48h] BYREF
  int *v10; // [esp-4h] [ebp-40h]
  _DWORD v11[3]; // [esp+4h] [ebp-38h] BYREF
  int v12; // [esp+10h] [ebp-2Ch] BYREF
  int v13; // [esp+14h] [ebp-28h]
  int v14; // [esp+18h] [ebp-24h]
  int v15; // [esp+1Ch] [ebp-20h]
  int v16; // [esp+20h] [ebp-1Ch]
  int v17; // [esp+24h] [ebp-18h]
  int v18; // [esp+28h] [ebp-14h]
  int v19; // [esp+2Ch] [ebp-10h]
  int v20; // [esp+30h] [ebp-Ch] BYREF
  int v21; // [esp+34h] [ebp-8h]
  int v22; // [esp+38h] [ebp-4h]
  int savedregs; // [esp+3Ch] [ebp+0h] BYREF

  v12 = 0;
  memset(v11, 0, sizeof(v11));
  v20 = a3;
  v21 = a2;
  v22 = a1;
  unknown_libname_44(a2);
  unknown_libname_44(v20);
  unknown_libname_44(a4);
  unknown_libname_44(a5);
  v10 = &savedregs;
  v9[1] = (unsigned int)&loc_4CCDB7;
  v9[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v9);
  if ( *(_BYTE *)off_4EA5F8 != 0 )
  {
    v5 = sub_453B20(a5, 0);
    v18 = Ibutils::Min((Ibutils *)0x1F4, v5, v6);
    v17 = sub_453B20(v21, 0);
    v16 = sub_453B20(v20, 0);
    v15 = sub_4B7350(*off_4EA22C, *off_4EA27C);
    if ( v15 != 0 && v18 > 0 && v17 > 0 && v16 > 0 )
    {
      v13 = v18;
      v19 = 0;
      do
      {
        v14 = sub_4ADE3C(a4, v16);
        if ( v14 != 0 && *(_BYTE *)off_4EA5F8 != 0 )
        {
          *(_BYTE *)(v14 + 824) = 1;
          *(_DWORD *)(v14 + 828) = *off_4EA544[0];
          *(_DWORD *)(v14 + 832) = *off_4EA4C8;
        }
        ++v19;
        --v13;
      }
      while ( v13 != 0 );
      Sysutils::IntToStr(v17);
      Sysutils::IntToStr(v16);
      Sysutils::IntToStr(v18);
      System::__linkproc__ LStrCatN(&v12, 10, v7);
      sub_4BB124(v22, v12, 1);
    }
    else
    {
      sub_4BB124(v22, &str___________X_Y__[1], 0);
    }
  }
  else
  {
    sub_4BB124(v22, &str____________2[1], 0);
  }
  __writefsdword(0, v9[0]);
  v10 = (int *)&loc_4CCDBE;
  unknown_libname_35(v11, 4);
  unknown_libname_35(&v20, 2);
  return unknown_libname_35(&a5, 2);
}


// ==================== [0x4CCE34] sub_4CCE34 (size: 839) ====================
int __fastcall sub_4CCE34(int a1, int a2, int a3)
{
  int v3; // ecx
  int v4; // ecx
  int v5; // ecx
  int v6; // ecx
  int v7; // ecx
  int v8; // ecx
  unsigned int v10[2]; // [esp-Ch] [ebp-B4h] BYREF
  int *v11; // [esp-4h] [ebp-ACh]
  int v12; // [esp+4h] [ebp-A4h] BYREF
  _DWORD v13[4]; // [esp+8h] [ebp-A0h] BYREF
  _BYTE v14[20]; // [esp+18h] [ebp-90h] BYREF
  _BYTE v15[20]; // [esp+2Ch] [ebp-7Ch] BYREF
  int v16; // [esp+40h] [ebp-68h] BYREF
  _DWORD v17[2]; // [esp+44h] [ebp-64h] BYREF
  _BYTE v18[28]; // [esp+4Ch] [ebp-5Ch] BYREF
  _BYTE v19[28]; // [esp+68h] [ebp-40h] BYREF
  int v20; // [esp+84h] [ebp-24h] BYREF
  int v21; // [esp+88h] [ebp-20h] BYREF
  int v22; // [esp+8Ch] [ebp-1Ch]
  int v23; // [esp+90h] [ebp-18h]
  void *Mem; // [esp+94h] [ebp-14h]
  int v25; // [esp+98h] [ebp-10h]
  int v26; // [esp+9Ch] [ebp-Ch]
  int v27; // [esp+A0h] [ebp-8h] BYREF
  int v28; // [esp+A4h] [ebp-4h]
  int savedregs; // [esp+A8h] [ebp+0h] BYREF

  v17[0] = 0;
  v16 = 0;
  memset(v13, 0, sizeof(v13));
  v12 = 0;
  v21 = 0;
  v20 = 0;
  v17[1] = 0;
  v26 = a3;
  v27 = a2;
  v28 = a1;
  unknown_libname_44(a2);
  v11 = &savedregs;
  v10[1] = (unsigned int)&loc_4CD16F;
  v10[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v10);
  if ( v26 - 1 >= 0 )
  {
    v22 = v26;
    v25 = 0;
    while ( *(int *)(*(_DWORD *)(v28 + 1036) + 8) < 46 )
    {
      Mem = (void *)System::__linkproc__ GetMem(24);
      if ( (unsigned __int8)sub_4AC404(*off_4EA490, v27, Mem) == 0
        || *(int *)((v23 = sub_4AC2F8(*off_4EA490, *((unsigned __int16 *)Mem + 2))) + 40) >= 15000
        && *off_4EA41C[0] == 0
        && *(_BYTE *)(v28 + 621) < 5u )
      {
        System::__linkproc__ FreeMem(Mem, 24);
        break;
      }
      if ( System::__linkproc__ RandInt(15) == 0 )
        sub_4AD0C8(*off_4EA490, Mem);
      if ( (*(_BYTE *)(v23 + 15) == 15 || (unsigned __int8)(*(_BYTE *)(v23 + 15) - 19) < 6u
                                       || *(_BYTE *)(v23 + 15) == 26)
        && (*(_BYTE *)(v23 + 16) == 0x82 || *(_BYTE *)(v23 + 16) == 0x83 || *(_BYTE *)(v23 + 16) == 0x84) )
      {
        sub_4AD1D4(*off_4EA490, Mem);
      }
      Classes::TList::Add(*(Classes::TList **)(v28 + 1036), Mem);
      sub_4D0824(v28, Mem);
      unknown_libname_16(v19, aMakeitem);
      LOBYTE(v3) = 24;
      System::__linkproc__ PStrNCat(v19, v28 + 21, v3);
      unknown_libname_16(v18, v19);
      LOBYTE(v4) = 27;
      System::__linkproc__ PStrNCat(v18, dword_4CD188, v4);
      unknown_libname_41();
      Sysutils::IntToStr(*(_DWORD *)Mem);
      System::__linkproc__ LStrCatN(&v21, 4, v5);
      sub_4E4274(v21);
      unknown_libname_16(v15, &dword_4CD198);
      LOBYTE(v6) = 18;
      System::__linkproc__ PStrNCat(v15, v28 + 4, v6);
      unknown_libname_16(v14, v15);
      LOBYTE(v7) = 19;
      System::__linkproc__ PStrNCat(v14, dword_4CD19C, v7);
      unknown_libname_41();
      Sysutils::IntToStr(*(_DWORD *)(v28 + 36));
      Sysutils::IntToStr(*(_DWORD *)(v28 + 40));
      unknown_libname_41();
      sub_4AC1AC(*off_4EA490, *((unsigned __int16 *)Mem + 2), v13);
      Sysutils::IntToStr(*(_DWORD *)Mem);
      System::__linkproc__ LStrCatN(v17, 13, v8);
      sub_4E42F8(v17[0]);
      ++v25;
      if ( --v22 == 0 )
        break;
    }
  }
  __writefsdword(0, v10[0]);
  v11 = (int *)&loc_4CD176;
  unknown_libname_35(&v12, 5);
  unknown_libname_35(&v16, 3);
  unknown_libname_35(&v20, 2);
  return unknown_libname_34(&v27);
}


// ==================== [0x4CD27C] @Xdrschema@TXDRImportTranslator@ParseGroup$qqr43System@%DelphiInterface$t15Xmldom@IDOMNode%60System@%DelphiInterface$t32Xmlschema@IXMLElementCompositors% (size: 587) ====================
int __fastcall Xdrschema::TXDRImportTranslator::ParseGroup(__int32 a1, int a2, __int32 a3)
{
  int v3; // ecx
  int v4; // ecx
  int v5; // ecx
  int v6; // ecx
  int v7; // ecx
  int v9; // [esp-10h] [ebp-84h]
  unsigned int v10[2]; // [esp-Ch] [ebp-80h] BYREF
  int *v11; // [esp-4h] [ebp-78h]
  int v12; // [esp+4h] [ebp-70h] BYREF
  int v13; // [esp+Ch] [ebp-68h] BYREF
  _BYTE v14[24]; // [esp+20h] [ebp-54h] BYREF
  _BYTE v15[20]; // [esp+38h] [ebp-3Ch] BYREF
  int v16; // [esp+4Ch] [ebp-28h] BYREF
  int v17; // [esp+50h] [ebp-24h] BYREF
  int v18; // [esp+58h] [ebp-1Ch] BYREF
  int v19; // [esp+5Ch] [ebp-18h] BYREF
  int v20; // [esp+60h] [ebp-14h]
  int v21; // [esp+64h] [ebp-10h]
  __int32 v22; // [esp+68h] [ebp-Ch] BYREF
  int v23; // [esp+6Ch] [ebp-8h]
  __int32 v24; // [esp+70h] [ebp-4h] BYREF
  int savedregs; // [esp+74h] [ebp+0h] BYREF

  v22 = _InterlockedExchange(&v24, a3);
  v23 = a2;
  v24 = a1;
  unknown_libname_44(a2);
  unknown_libname_44(v22);
  v11 = &savedregs;
  v10[1] = (unsigned int)&loc_4CD4BB;
  v10[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v10);
  v21 = sub_4AE640(*off_4EA490);
  v20 = sub_453B20(v22, 0);
  if ( v20 > 0 )
  {
    if ( v21 != 0 )
    {
      if ( *(_DWORD *)(v21 + 48) <= v20 )
      {
        v20 = *(_DWORD *)(v21 + 48);
        *(_DWORD *)(v21 + 48) = 0;
      }
      else
      {
        *(_DWORD *)(v21 + 48) -= v20;
      }
      sub_4C49F4(v21);
      Sysutils::IntToStr(v20);
      System::__linkproc__ LStrCatN(&v18, 4, v3);
      sub_4BB124(v24, v18, 1);
      unknown_libname_16(v15, &dword_4CD4EC);
      LOBYTE(v4) = 19;
      System::__linkproc__ PStrNCat(v15, v24 + 4, v4);
      unknown_libname_16(v14, v15);
      LOBYTE(v5) = 20;
      System::__linkproc__ PStrNCat(v14, dword_4CD4F0, v5);
      unknown_libname_41();
      Sysutils::IntToStr(*(_DWORD *)(v24 + 36));
      Sysutils::IntToStr(*(_DWORD *)(v24 + 40));
      unknown_libname_41();
      Sysutils::IntToStr(v20);
      System::__linkproc__ LStrCatN(&v17, 12, v6);
      sub_4E42F8(v17);
    }
    else if ( (unsigned __int8)sub_4AE708(*off_4EA490, v23, &v19) != 0 )
    {
      Sysutils::IntToStr(v19);
      System::__linkproc__ LStrCatN(&v13, 4, v7);
      sub_4BB124(v24, v13, 1);
    }
    else
    {
      v9 = -v20;
      unknown_libname_41();
      sub_4B4828(v9);
    }
  }
  __writefsdword(0, v10[0]);
  v11 = (int *)&loc_4CD4C2;
  unknown_libname_35(&v12, 7);
  unknown_libname_35(&v16, 4);
  return unknown_libname_35(&v22, 2);
}


// ==================== [0x4CD550] sub_4CD550 (size: 610) ====================
int __fastcall sub_4CD550(int *a1, int a2, __int32 a3)
{
  int v3; // ecx
  int v4; // ecx
  int v5; // ecx
  int v6; // ecx
  int v7; // ecx
  int v9; // [esp-10h] [ebp-84h]
  unsigned int v10[2]; // [esp-Ch] [ebp-80h] BYREF
  int *v11; // [esp-4h] [ebp-78h]
  int v12; // [esp+4h] [ebp-70h] BYREF
  int v13; // [esp+Ch] [ebp-68h] BYREF
  _BYTE v14[24]; // [esp+20h] [ebp-54h] BYREF
  _BYTE v15[20]; // [esp+38h] [ebp-3Ch] BYREF
  int v16; // [esp+4Ch] [ebp-28h] BYREF
  int v17; // [esp+50h] [ebp-24h] BYREF
  int v18; // [esp+58h] [ebp-1Ch] BYREF
  int v19; // [esp+5Ch] [ebp-18h] BYREF
  int v20; // [esp+60h] [ebp-14h]
  int v21; // [esp+64h] [ebp-10h]
  __int32 v22; // [esp+68h] [ebp-Ch] BYREF
  int v23; // [esp+6Ch] [ebp-8h]
  int *v24; // [esp+70h] [ebp-4h] BYREF
  int savedregs; // [esp+74h] [ebp+0h] BYREF

  v22 = _InterlockedExchange((volatile __int32 *)&v24, a3);
  v23 = a2;
  v24 = a1;
  unknown_libname_44(a2);
  unknown_libname_44(v22);
  v11 = &savedregs;
  v10[1] = (unsigned int)&loc_4CD7A6;
  v10[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v10);
  v21 = sub_4AE640(*off_4EA490);
  v20 = sub_453B20(v22, 0);
  if ( v20 > 0 )
  {
    if ( v21 != 0 )
    {
      if ( v20 + *(_DWORD *)(v21 + 48) >= v24[154] )
      {
        v20 = v24[154] - *(_DWORD *)(v21 + 48);
        *(_DWORD *)(v21 + 48) = v24[154];
      }
      else
      {
        *(_DWORD *)(v21 + 48) += v20;
      }
      sub_4C49F4(v21);
      Sysutils::IntToStr(v20);
      System::__linkproc__ LStrCatN(&v18, 4, v3);
      sub_4BB124(v24, v18, 1);
      unknown_libname_16(v15, &dword_4CD7E0);
      LOBYTE(v4) = 19;
      System::__linkproc__ PStrNCat(v15, v24 + 1, v4);
      unknown_libname_16(v14, v15);
      LOBYTE(v5) = 20;
      System::__linkproc__ PStrNCat(v14, dword_4CD7E4, v5);
      unknown_libname_41();
      Sysutils::IntToStr(v24[9]);
      Sysutils::IntToStr(v24[10]);
      unknown_libname_41();
      Sysutils::IntToStr(v20);
      System::__linkproc__ LStrCatN(&v17, 12, v6);
      sub_4E42F8(v17);
    }
    else if ( (unsigned __int8)sub_4AE708(*off_4EA490, v23, &v19) != 0 )
    {
      Sysutils::IntToStr(v19);
      System::__linkproc__ LStrCatN(&v13, 4, v7);
      sub_4BB124(v24, v13, 1);
    }
    else
    {
      v9 = v20;
      unknown_libname_41();
      sub_4B4828(v9);
    }
  }
  __writefsdword(0, v10[0]);
  v11 = (int *)&loc_4CD7AD;
  unknown_libname_35(&v12, 7);
  unknown_libname_35(&v16, 4);
  return unknown_libname_35(&v22, 2);
}


// ==================== [0x4CD844] sub_4CD844 (size: 364) ====================
int __fastcall sub_4CD844(__int32 a1, int a2, __int32 a3)
{
  int v3; // ecx
  int v4; // ecx
  unsigned int v6[2]; // [esp-Ch] [ebp-44h] BYREF
  int *v7; // [esp-4h] [ebp-3Ch]
  int v8; // [esp+4h] [ebp-34h] BYREF
  int v9; // [esp+18h] [ebp-20h] BYREF
  int v10; // [esp+20h] [ebp-18h] BYREF
  int v11; // [esp+24h] [ebp-14h] BYREF
  int v12; // [esp+28h] [ebp-10h] BYREF
  int v13; // [esp+2Ch] [ebp-Ch]
  int v14; // [esp+30h] [ebp-8h] BYREF
  __int32 v15; // [esp+34h] [ebp-4h] BYREF
  int savedregs; // [esp+38h] [ebp+0h] BYREF

  v13 = _InterlockedExchange(&v15, a3);
  v14 = a2;
  v15 = a1;
  unknown_libname_44(a2);
  v7 = &savedregs;
  v6[1] = (unsigned int)&loc_4CD9A4;
  v6[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v6);
  if ( v13 <= 0 )
  {
    unknown_libname_37(&v12, &str_13_[1]);
    unknown_libname_37(&v11, &str_______0[1]);
    v13 = -v13;
  }
  else
  {
    unknown_libname_37(&v12, &str_14_[1]);
    unknown_libname_37(&v11, &str__________0[1]);
  }
  Sysutils::IntToStr(v13);
  System::__linkproc__ LStrCatN(&v10, 5, v3);
  sub_4BB124(v15, v10, 1);
  unknown_libname_41();
  Sysutils::IntToStr(*(_DWORD *)(v15 + 36));
  Sysutils::IntToStr(*(_DWORD *)(v15 + 40));
  unknown_libname_41();
  Sysutils::IntToStr(v13);
  System::__linkproc__ LStrCatN(&v9, 14, v4);
  sub_4E42F8(v9);
  __writefsdword(0, v6[0]);
  v7 = (int *)&loc_4CD9AB;
  unknown_libname_35(&v8, 10);
  return unknown_libname_34(&v14);
}


// ==================== [0x4CDB4C] sub_4CDB4C (size: 441) ====================
int __fastcall sub_4CDB4C(__int32 a1, int a2, __int32 a3)
{
  int v3; // ecx
  int v4; // ecx
  int v5; // ecx
  unsigned int v7[2]; // [esp-Ch] [ebp-50h] BYREF
  int *v8; // [esp-4h] [ebp-48h]
  int v9; // [esp+4h] [ebp-40h] BYREF
  int v10; // [esp+Ch] [ebp-38h] BYREF
  int v11; // [esp+1Ch] [ebp-28h] BYREF
  int v12; // [esp+2Ch] [ebp-18h] BYREF
  int v13; // [esp+30h] [ebp-14h]
  int v14; // [esp+34h] [ebp-10h]
  int v15; // [esp+38h] [ebp-Ch]
  int v16; // [esp+3Ch] [ebp-8h] BYREF
  __int32 v17; // [esp+40h] [ebp-4h] BYREF
  int savedregs; // [esp+44h] [ebp+0h] BYREF

  v15 = _InterlockedExchange(&v17, a3);
  v16 = a2;
  v17 = a1;
  unknown_libname_44(a2);
  v8 = &savedregs;
  v7[1] = (unsigned int)&loc_4CDCF9;
  v7[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v7);
  v13 = sub_4AE640(*off_4EA490);
  if ( v13 != 0 )
  {
    Sysutils::IntToStr(*(unsigned __int8 *)(v13 + 52));
    Sysutils::IntToStr(v15);
    unknown_libname_41();
    System::__linkproc__ LStrCatN(&v12, 8, v3);
    sub_4E4274(v12);
    v14 = *(unsigned __int8 *)(v13 + 52);
    sub_4BEDFC(v13, v15);
    sub_4BED6C(v13, v14);
    Sysutils::IntToStr(v14);
    Sysutils::IntToStr(v15);
    unknown_libname_41();
    System::__linkproc__ LStrCatN(&v11, 13, v4);
    sub_4E42F8(v11);
    Sysutils::IntToStr(v14);
    Sysutils::IntToStr(v15);
    System::__linkproc__ LStrCatN(&v10, 6, v5);
    sub_4BB124(v17, v10, 1);
  }
  __writefsdword(0, v7[0]);
  v8 = (int *)&loc_4CDD00;
  unknown_libname_35(&v9, 11);
  return unknown_libname_34(&v16);
}


// ==================== [0x4CDDAC] unknown_libname_677 (size: 426) ====================
// Borland Visual Component Library & Packages
int __fastcall unknown_libname_677(__int32 a1, int a2, __int32 a3)
{
  int v3; // ecx
  int v4; // ecx
  int v5; // ecx
  unsigned int v7[2]; // [esp-Ch] [ebp-4Ch] BYREF
  int *v8; // [esp-4h] [ebp-44h]
  int v9; // [esp+4h] [ebp-3Ch] BYREF
  int v10; // [esp+8h] [ebp-38h] BYREF
  int v11; // [esp+18h] [ebp-28h] BYREF
  int v12; // [esp+28h] [ebp-18h] BYREF
  int v13; // [esp+2Ch] [ebp-14h]
  int v14; // [esp+30h] [ebp-10h]
  int v15; // [esp+34h] [ebp-Ch]
  int v16; // [esp+38h] [ebp-8h] BYREF
  __int32 v17; // [esp+3Ch] [ebp-4h] BYREF
  int savedregs; // [esp+40h] [ebp+0h] BYREF

  v15 = _InterlockedExchange(&v17, a3);
  v16 = a2;
  v17 = a1;
  unknown_libname_44(a2);
  v8 = &savedregs;
  v7[1] = (unsigned int)&loc_4CDF4A;
  v7[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v7);
  v14 = sub_4AE640(*off_4EA490);
  if ( v14 != 0 )
  {
    unknown_libname_91(*(_DWORD *)(v14 + 76), 0);
    Sysutils::IntToStr(v15);
    unknown_libname_41();
    System::__linkproc__ LStrCatN(&v12, 8, v3);
    sub_4E4274(v12);
    v13 = *(_DWORD *)(v14 + 76);
    *(_DWORD *)(v14 + 76) = v15;
    sub_4BED6C(v14, *(unsigned __int8 *)(v17 + 52));
    Sysutils::IntToStr(v13);
    Sysutils::IntToStr(v15);
    unknown_libname_41();
    System::__linkproc__ LStrCatN(&v11, 13, v4);
    sub_4E42F8(v11);
    Sysutils::IntToStr(v15);
    System::__linkproc__ LStrCatN(&v10, 4, v5);
    sub_4BB124(v17, v10, 1);
  }
  __writefsdword(0, v7[0]);
  v8 = (int *)&loc_4CDF51;
  unknown_libname_35(&v9, 10);
  return unknown_libname_34(&v16);
}


// ==================== [0x4CDFF8] sub_4CDFF8 (size: 553) ====================
int __fastcall sub_4CDFF8(int a1, int a2, int a3)
{
  int v3; // eax
  int v4; // ecx
  int v5; // ecx
  int v6; // ecx
  unsigned int v8[2]; // [esp-Ch] [ebp-7Ch] BYREF
  int *v9; // [esp-4h] [ebp-74h]
  int v10; // [esp+4h] [ebp-6Ch] BYREF
  _DWORD v11[4]; // [esp+8h] [ebp-68h] BYREF
  _BYTE v12[20]; // [esp+18h] [ebp-58h] BYREF
  _BYTE v13[20]; // [esp+2Ch] [ebp-44h] BYREF
  int v14; // [esp+40h] [ebp-30h] BYREF
  int v15; // [esp+44h] [ebp-2Ch] BYREF
  int v16; // [esp+48h] [ebp-28h] BYREF
  int v17; // [esp+4Ch] [ebp-24h]
  int v18; // [esp+50h] [ebp-20h]
  int v19; // [esp+54h] [ebp-1Ch]
  int v20; // [esp+58h] [ebp-18h]
  int v21; // [esp+5Ch] [ebp-14h]
  int v22; // [esp+60h] [ebp-10h]
  int v23; // [esp+64h] [ebp-Ch] BYREF
  int v24; // [esp+68h] [ebp-8h]
  int v25; // [esp+6Ch] [ebp-4h]
  int savedregs; // [esp+70h] [ebp+0h] BYREF

  v15 = 0;
  v14 = 0;
  memset(v11, 0, sizeof(v11));
  v10 = 0;
  v16 = 0;
  v23 = a3;
  v24 = a2;
  v25 = a1;
  unknown_libname_44(a2);
  unknown_libname_44(v23);
  v9 = &savedregs;
  v8[1] = (unsigned int)&loc_4CE215;
  v8[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v8);
  v3 = sub_453B20(v23, 1);
  v20 = v3;
  if ( v3 > 0 )
  {
    v18 = v3;
    v21 = 1;
    do
    {
      if ( *(_DWORD *)(*(_DWORD *)(v25 + 1036) + 8) - 1 >= 0 )
      {
        v17 = *(_DWORD *)(*(_DWORD *)(v25 + 1036) + 8);
        v22 = 0;
        while ( 1 )
        {
          v19 = Classes::TList::Get(*(Classes::TList **)(v25 + 1036), v22);
          sub_4AC1AC(*off_4EA490, *(unsigned __int16 *)(v19 + 4), &v16);
          if ( unknown_libname_84(v16, v24) == 0 )
            break;
          ++v22;
          if ( --v17 == 0 )
            goto LABEL_8;
        }
        unknown_libname_16(v13, &dword_4CE224);
        LOBYTE(v4) = 18;
        System::__linkproc__ PStrNCat(v13, v25 + 4, v4);
        unknown_libname_16(v12, v13);
        LOBYTE(v5) = 19;
        System::__linkproc__ PStrNCat(v12, dword_4CE228, v5);
        unknown_libname_41();
        Sysutils::IntToStr(*(_DWORD *)(v25 + 36));
        Sysutils::IntToStr(*(_DWORD *)(v25 + 40));
        unknown_libname_41();
        sub_4AC1AC(*off_4EA490, *(unsigned __int16 *)(v19 + 4), v11);
        Sysutils::IntToStr(*(_DWORD *)v19);
        System::__linkproc__ LStrCatN(&v15, 13, v6);
        sub_4E42F8(v15);
        sub_4D0BDC(v25, v19);
        System::__linkproc__ FreeMem(v19, 24);
        Classes::TList::Delete(*(Classes::TList **)(v25 + 1036), v22);
      }
LABEL_8:
      ++v21;
      --v18;
    }
    while ( v18 != 0 );
  }
  __writefsdword(0, v8[0]);
  v9 = (int *)&loc_4CE21C;
  unknown_libname_35(&v10, 5);
  unknown_libname_35(&v14, 3);
  return unknown_libname_35(&v23, 2);
}


// ==================== [0x4CE6E4] sub_4CE6E4 (size: 327) ====================
int __fastcall sub_4CE6E4(int a1, void *a2, int a3)
{
  Ibutils *v3; // eax
  int v4; // ecx
  int v5; // edx
  unsigned int v7[2]; // [esp-Ch] [ebp-38h] BYREF
  int *v8; // [esp-4h] [ebp-30h]
  int v9; // [esp+4h] [ebp-28h] BYREF
  int v10; // [esp+8h] [ebp-24h]
  int v11; // [esp+Ch] [ebp-20h]
  System::TObject *v12; // [esp+10h] [ebp-1Ch]
  int v13; // [esp+14h] [ebp-18h]
  int v14; // [esp+18h] [ebp-14h]
  int v15; // [esp+1Ch] [ebp-10h]
  int v16; // [esp+20h] [ebp-Ch] BYREF
  void *v17; // [esp+24h] [ebp-8h]
  int v18; // [esp+28h] [ebp-4h]
  int savedregs; // [esp+2Ch] [ebp+0h] BYREF

  v9 = 0;
  v10 = 0;
  v16 = a3;
  v17 = a2;
  v18 = a1;
  unknown_libname_44(a2);
  unknown_libname_44(v16);
  v8 = &savedregs;
  v7[1] = (unsigned int)&loc_4CE81F;
  v7[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v7);
  v13 = sub_4AE640(*off_4EA490);
  v3 = (Ibutils *)sub_453B20(v16, 2);
  v14 = Ibutils::Min(v3, 10, v4);
  if ( v13 != 0 )
  {
    LOBYTE(v5) = 1;
    v12 = (System::TObject *)unknown_libname_22(off_40FAFC, v5);
    sub_4AE8AC(v12, v14, *(_DWORD *)(v13 + 40));
    if ( *((_DWORD *)v12 + 2) - 1 >= 0 )
    {
      v11 = *((_DWORD *)v12 + 2);
      v15 = 0;
      do
      {
        v13 = Classes::TList::Get(v12, v15);
        unknown_libname_41();
        sub_4BCB54(v13, v10, 0);
        ++v15;
        --v11;
      }
      while ( v11 != 0 );
    }
    System::TObject::Free(v12);
  }
  else
  {
    System::__linkproc__ LStrCat3((int)&v9, v17, &str___not_found_4[1]);
    sub_4BB124(v18, v9, 0);
  }
  __writefsdword(0, v7[0]);
  v8 = (int *)&loc_4CE826;
  unknown_libname_35(&v9, 2);
  return unknown_libname_35(&v16, 2);
}


// ==================== [0x4CE9F0] sub_4CE9F0 (size: 340) ====================
int __fastcall sub_4CE9F0(int a1, void *a2)
{
  unsigned int v3[2]; // [esp-Ch] [ebp-24h] BYREF
  int *v4; // [esp-4h] [ebp-1Ch]
  int v5; // [esp+0h] [ebp-18h] BYREF
  char v6; // [esp+7h] [ebp-11h]
  int v7; // [esp+8h] [ebp-10h]
  int v8; // [esp+Ch] [ebp-Ch]
  void *v9; // [esp+10h] [ebp-8h] BYREF
  int v10; // [esp+14h] [ebp-4h]
  int savedregs; // [esp+18h] [ebp+0h] BYREF

  v5 = 0;
  v9 = a2;
  v10 = a1;
  unknown_libname_44(a2);
  v4 = &savedregs;
  v3[1] = (unsigned int)&loc_4CEB39;
  v3[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v3);
  if ( (unsigned __int8)sub_4BF4A0(v10) != 0 )
  {
    if ( *off_4EA64C != 0 )
    {
      sub_4BB124(v10, &str______________1[1], 0);
    }
    else
    {
      v8 = sub_49A36C(*off_4EA07C[0], v9);
      if ( v8 != 0 )
      {
        v6 = 0;
        v7 = sub_49997C(*(_DWORD *)(v10 + 800), v8);
        if ( v7 != 0 )
        {
          if ( sub_49997C(v8, *(_DWORD *)(v10 + 800)) != 0 )
            v6 = 1;
          else
            *(_DWORD *)(v7 + 4) = 0;
        }
        if ( v6 != 0 )
        {
          sub_4AF728(*(_DWORD *)(*(_DWORD *)(v10 + 800) + 4));
          sub_4AF728(v9);
        }
      }
      else
      {
        System::__linkproc__ LStrCat3((int)&v5, v9, &str_________5[1]);
        sub_4BB124(v10, v5, 0);
      }
    }
  }
  else
  {
    sub_4BB124(v10, &str______________2[1], 0);
  }
  __writefsdword(0, v3[0]);
  v4 = (int *)&loc_4CEB40;
  unknown_libname_34(&v5);
  return unknown_libname_34(&v9);
}


// ==================== [0x4CEBA0] @Mxcommon@TBinData@AddBinValues$qqr17System@AnsiStringpx14System@TVarRecxi (size: 445) ====================
int __fastcall Mxcommon::TBinData::AddBinValues(int a1, int a2, int a3)
{
  int v3; // ecx
  int v4; // ecx
  int v5; // eax
  unsigned int v7[2]; // [esp-Ch] [ebp-34h] BYREF
  int *v8; // [esp-4h] [ebp-2Ch]
  int v9; // [esp+4h] [ebp-24h] BYREF
  int v10; // [esp+8h] [ebp-20h]
  int v11; // [esp+Ch] [ebp-1Ch] BYREF
  int v12; // [esp+10h] [ebp-18h] BYREF
  char v13; // [esp+17h] [ebp-11h]
  int v14; // [esp+18h] [ebp-10h]
  int v15; // [esp+1Ch] [ebp-Ch] BYREF
  int v16; // [esp+20h] [ebp-8h]
  int v17; // [esp+24h] [ebp-4h]
  int savedregs; // [esp+28h] [ebp+0h] BYREF

  v9 = 0;
  v10 = 0;
  v11 = 0;
  v12 = 0;
  v15 = a3;
  v16 = a2;
  v17 = a1;
  unknown_libname_44(a2);
  unknown_libname_44(v15);
  v8 = &savedregs;
  v7[1] = (unsigned int)&loc_4CED51;
  v7[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v7);
  if ( *off_4EA64C != 0 )
  {
    sub_4BB124(v17, &str_______________5[1], 0);
  }
  else
  {
    v13 = 0;
    v14 = sub_4AE640(*off_4EA490);
    if ( v14 != 0 )
    {
      if ( sub_49A408(*off_4EA07C[0], v15) == 0 && (unsigned __int8)sub_49A4A4(*off_4EA07C[0], v16, v15) != 0 )
      {
        System::__linkproc__ LStrCatN(&v12, 3, v3);
        sub_4AF728(v12);
        System::__linkproc__ LStrCatN(&v11, 6, v4);
        sub_4BB124(v17, v11, 0);
        v13 = 1;
      }
      unknown_libname_41();
      v5 = sub_49A408(*off_4EA07C[0], v10);
      *(_DWORD *)(v14 + 800) = v5;
      if ( *(_DWORD *)(v14 + 800) != 0 )
      {
        sub_4987F0(&v9);
        unknown_libname_36(v14 + 808, v9);
      }
    }
    if ( v13 == 0 )
      sub_4BB124(v17, &str_You_Screwed_Up[1], 0);
  }
  __writefsdword(0, v7[0]);
  v8 = (int *)&loc_4CED58;
  unknown_libname_35(&v9, 4);
  return unknown_libname_35(&v15, 2);
}


// ==================== [0x4CF008] sub_4CF008 (size: 720) ====================
int __fastcall sub_4CF008(int a1)
{
  int v1; // edx
  int v2; // edx
  int v3; // eax
  int v4; // eax
  int v5; // eax
  int v6; // ecx
  int v8; // [esp-14h] [ebp-44h]
  int v9; // [esp-10h] [ebp-40h]
  int v10; // [esp-10h] [ebp-40h]
  unsigned int v11[2]; // [esp-Ch] [ebp-3Ch] BYREF
  int *v12; // [esp-4h] [ebp-34h]
  int v13; // [esp+0h] [ebp-30h] BYREF
  int v14; // [esp+4h] [ebp-2Ch]
  int v15; // [esp+8h] [ebp-28h]
  int v16; // [esp+Ch] [ebp-24h]
  void *v17; // [esp+10h] [ebp-20h] BYREF
  char v18; // [esp+17h] [ebp-19h]
  int v19; // [esp+18h] [ebp-18h]
  System::TObject *v20; // [esp+1Ch] [ebp-14h]
  System::TObject *v21; // [esp+20h] [ebp-10h]
  int v22; // [esp+24h] [ebp-Ch]
  int v23; // [esp+28h] [ebp-8h]
  int v24; // [esp+2Ch] [ebp-4h]
  int savedregs; // [esp+30h] [ebp+0h] BYREF

  v1 = 0;
  v13 = 0;
  v14 = 0;
  v17 = nullptr;
  v24 = a1;
  v12 = &savedregs;
  v11[1] = (unsigned int)&loc_4CF2CD;
  v11[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v11);
  if ( *(_BYTE *)(*(_DWORD *)(a1 + 760) + 47) != 0 )
  {
    LOBYTE(v1) = 1;
    v21 = (System::TObject *)unknown_libname_22(off_40FAFC, v1);
    LOBYTE(v2) = 1;
    v20 = (System::TObject *)unknown_libname_22(off_40FAFC, v2);
    sub_4AE8AC(v21, 1000, *(_DWORD *)(v24 + 40));
    if ( *((_DWORD *)v21 + 2) - 1 >= 0 )
    {
      v16 = *((_DWORD *)v21 + 2);
      v23 = 0;
      do
      {
        v19 = Classes::TList::Get(v21, v23);
        if ( *(_BYTE *)(v19 + 842) == 0 && *(_BYTE *)(v19 + 841) == 0 )
        {
          *(_DWORD *)(v19 + 192) = 0;
          if ( *(_DWORD *)(v19 + 800) != 0 )
          {
            v18 = 0;
            if ( *((_DWORD *)v20 + 2) - 1 >= 0 )
            {
              v15 = *((_DWORD *)v20 + 2);
              v22 = 0;
              while ( 1 )
              {
                v3 = Classes::TList::Get(v20, v22);
                if ( v3 == *(_DWORD *)(v19 + 800) )
                  break;
                ++v22;
                if ( --v15 == 0 )
                  goto LABEL_12;
              }
              v18 = 1;
            }
LABEL_12:
            if ( v18 == 0 )
              Classes::TList::Add(v20, *(void **)(v19 + 800));
          }
        }
        ++v23;
        --v16;
      }
      while ( v16 != 0 );
    }
    sub_4BB124(v24, &str_Guild_Contest_O[1], 1);
    sub_4AEA00(&str___Guild_War_Sta[1], 10000, *(_DWORD *)(v24 + 40), *(_DWORD *)(v24 + 36));
    unknown_libname_34(&v17);
    if ( *((_DWORD *)v20 + 2) - 1 >= 0 )
    {
      v16 = *((_DWORD *)v20 + 2);
      v23 = 0;
      do
      {
        v4 = Classes::TList::Get(v20, v23);
        sub_499D58(v4);
        if ( *((_DWORD *)v21 + 2) - 1 >= 0 )
        {
          v15 = *((_DWORD *)v21 + 2);
          v22 = 0;
          do
          {
            v19 = Classes::TList::Get(v21, v22);
            v5 = Classes::TList::Get(v20, v23);
            if ( v5 == *(_DWORD *)(v19 + 800) )
            {
              v9 = Classes::TList::Get(v20, v23);
              unknown_libname_41();
              sub_499D90(v9, v14);
            }
            ++v22;
            --v15;
          }
          while ( v15 != 0 );
        }
        Classes::TList::Get(v20, v23);
        System::__linkproc__ LStrCatN(&v17, 3, v6);
        ++v23;
        --v16;
      }
      while ( v16 != 0 );
    }
    v10 = *(_DWORD *)(v24 + 36);
    v8 = *(_DWORD *)(v24 + 40);
    System::__linkproc__ LStrCat3((int)&v13, &str___Guilds_Are__[1], v17);
    sub_4AEA00(v13, 10000, v8, v10);
    System::TObject::Free(v21);
    System::TObject::Free(v20);
  }
  else
  {
    sub_4BB124(v24, &str_this_command_is[1], 0);
  }
  __writefsdword(0, v11[0]);
  v12 = (int *)&loc_4CF2D4;
  unknown_libname_35(&v13, 2);
  return unknown_libname_34(&v17);
}


// ==================== [0x4CF364] sub_4CF364 (size: 474) ====================
int __fastcall sub_4CF364(int a1)
{
  int v1; // edx
  int v2; // edx
  int v3; // eax
  int v4; // eax
  int v5; // ecx
  int v7; // [esp-14h] [ebp-3Ch]
  int v8; // [esp-10h] [ebp-38h]
  unsigned int v9[2]; // [esp-Ch] [ebp-34h] BYREF
  int *v10; // [esp-4h] [ebp-2Ch]
  int v11; // [esp+0h] [ebp-28h] BYREF
  int v12; // [esp+4h] [ebp-24h]
  int v13; // [esp+8h] [ebp-20h]
  char v14; // [esp+Fh] [ebp-19h]
  int v15; // [esp+10h] [ebp-18h]
  System::TObject *v16; // [esp+14h] [ebp-14h]
  System::TObject *v17; // [esp+18h] [ebp-10h]
  int v18; // [esp+1Ch] [ebp-Ch]
  int v19; // [esp+20h] [ebp-8h]
  int v20; // [esp+24h] [ebp-4h]
  int savedregs; // [esp+28h] [ebp+0h] BYREF

  v1 = 0;
  v11 = 0;
  v20 = a1;
  v10 = &savedregs;
  v9[1] = (unsigned int)&loc_4CF533;
  v9[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v9);
  if ( *(_BYTE *)(*(_DWORD *)(a1 + 760) + 47) != 0 )
  {
    LOBYTE(v1) = 1;
    v17 = (System::TObject *)unknown_libname_22(off_40FAFC, v1);
    LOBYTE(v2) = 1;
    v16 = (System::TObject *)unknown_libname_22(off_40FAFC, v2);
    sub_4AE8AC(v17, 1000, *(_DWORD *)(v20 + 40));
    if ( *((_DWORD *)v17 + 2) - 1 >= 0 )
    {
      v13 = *((_DWORD *)v17 + 2);
      v19 = 0;
      do
      {
        v15 = Classes::TList::Get(v17, v19);
        if ( *(_BYTE *)(v15 + 842) == 0 && *(_BYTE *)(v15 + 841) == 0 && *(_DWORD *)(v15 + 800) != 0 )
        {
          v14 = 0;
          if ( *((_DWORD *)v16 + 2) - 1 >= 0 )
          {
            v12 = *((_DWORD *)v16 + 2);
            v18 = 0;
            while ( 1 )
            {
              v3 = Classes::TList::Get(v16, v18);
              if ( v3 == *(_DWORD *)(v15 + 800) )
                break;
              ++v18;
              if ( --v12 == 0 )
                goto LABEL_12;
            }
            v14 = 1;
          }
LABEL_12:
          if ( v14 == 0 )
            Classes::TList::Add(v16, *(void **)(v15 + 800));
        }
        ++v19;
        --v13;
      }
      while ( v13 != 0 );
    }
    if ( *((_DWORD *)v16 + 2) - 1 >= 0 )
    {
      v13 = *((_DWORD *)v16 + 2);
      v19 = 0;
      do
      {
        v4 = Classes::TList::Get(v16, v19);
        sub_499D7C(v4);
        v8 = *(_DWORD *)(v20 + 36);
        v7 = *(_DWORD *)(v20 + 40);
        Classes::TList::Get(v16, v19);
        System::__linkproc__ LStrCatN(&v11, 3, v5);
        sub_4AEA00(v11, 10000, v7, v8);
        ++v19;
        --v13;
      }
      while ( v13 != 0 );
    }
    System::TObject::Free(v17);
    System::TObject::Free(v16);
  }
  __writefsdword(0, v9[0]);
  v10 = (int *)&loc_4CF53A;
  return unknown_libname_34(&v11);
}


// ==================== [0x4CF564] sub_4CF564 (size: 588) ====================
int __fastcall sub_4CF564(_DWORD *a1, int a2)
{
  int v2; // ecx
  int v3; // eax
  unsigned __int16 v4; // ax
  int v5; // ecx
  int v6; // ecx
  int v8; // [esp-14h] [ebp-58h]
  int v9; // [esp-14h] [ebp-58h]
  int v10; // [esp-14h] [ebp-58h]
  int v11; // [esp-10h] [ebp-54h]
  int v12; // [esp-10h] [ebp-54h]
  int v13; // [esp-10h] [ebp-54h]
  unsigned int v14[2]; // [esp-Ch] [ebp-50h] BYREF
  int *v15; // [esp-4h] [ebp-48h]
  int v16; // [esp+4h] [ebp-40h] BYREF
  _DWORD v17[3]; // [esp+8h] [ebp-3Ch] BYREF
  int v18; // [esp+14h] [ebp-30h] BYREF
  int v19; // [esp+18h] [ebp-2Ch] BYREF
  _DWORD v20[4]; // [esp+1Ch] [ebp-28h] BYREF
  int v21; // [esp+2Ch] [ebp-18h]
  int v22; // [esp+30h] [ebp-14h]
  int v23; // [esp+34h] [ebp-10h]
  int v24; // [esp+38h] [ebp-Ch]
  int v25; // [esp+3Ch] [ebp-8h] BYREF
  _DWORD *v26; // [esp+40h] [ebp-4h]
  int savedregs; // [esp+44h] [ebp+0h] BYREF

  memset(v17, 0, sizeof(v17));
  v16 = 0;
  v19 = 0;
  v18 = 0;
  v20[0] = 0;
  v25 = a2;
  v26 = a1;
  unknown_libname_44(a2);
  v15 = &savedregs;
  v14[1] = (unsigned int)&loc_4CF7A4;
  v14[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v14);
  if ( *(_BYTE *)(v26[190] + 47) != 0 )
  {
    v22 = sub_49A36C(*off_4EA07C[0], v25);
    if ( v22 != 0 )
    {
      v11 = v26[9];
      v8 = v26[10];
      System::__linkproc__ LStrCatN(v20, 3, v2);
      sub_4AEA00(v20[0], 10000, v8, v11);
      v3 = (*(int (__fastcall **)(_DWORD))(**(_DWORD **)(v22 + 32) + 20))(*(_DWORD *)(v22 + 32)) - 1;
      if ( v3 >= 0 )
      {
        v21 = v3 + 1;
        v24 = 0;
        do
        {
          v23 = (*(int (__fastcall **)(_DWORD, int))(**(_DWORD **)(v22 + 32) + 24))(*(_DWORD *)(v22 + 32), v24);
          v12 = v26[9];
          v9 = v26[10];
          (*(void (__fastcall **)(_DWORD, int, int *, _strings *))(**(_DWORD **)(v22 + 32) + 12))(
            *(_DWORD *)(v22 + 32),
            v24,
            &v18,
            &str____11[1]);
          v4 = sub_407B24(v23);
          Sysutils::IntToStr(v4);
          Sysutils::IntToStr((unsigned __int16)v23);
          System::__linkproc__ LStrCatN(&v19, 7, v5);
          sub_4AEA00(v19, 10000, v9, v12);
          ++v24;
          --v21;
        }
        while ( v21 != 0 );
      }
      v13 = v26[9];
      v10 = v26[10];
      Sysutils::IntToStr(*(_DWORD *)(v22 + 24));
      System::__linkproc__ LStrCatN(v17, 4, v6);
      sub_4AEA00(v17[0], 10000, v10, v13);
      sub_4AEA00(&str_________________1[1], 10000, v26[10], v26[9]);
    }
  }
  else
  {
    sub_4BB124(v26, &str_this_command_is_0[1], 0);
  }
  __writefsdword(0, v14[0]);
  v15 = (int *)&loc_4CF7AB;
  unknown_libname_35(&v16, 7);
  return unknown_libname_34(&v25);
}


// ==================== [0x4CF87C] sub_4CF87C (size: 853) ====================
int __fastcall sub_4CF87C(__int32 a1, int a2, __int32 a3)
{
  int v3; // ecx
  int v4; // ecx
  int v5; // ecx
  int v6; // eax
  unsigned __int8 v7; // al
  int v8; // eax
  unsigned __int8 v9; // al
  int v10; // eax
  unsigned __int8 v11; // al
  int v12; // eax
  unsigned __int8 v13; // al
  int v14; // eax
  unsigned __int8 v15; // al
  int v16; // ecx
  unsigned int v18[2]; // [esp-Ch] [ebp-D0h] BYREF
  int *v19; // [esp-4h] [ebp-C8h]
  int v20; // [esp+4h] [ebp-C0h] BYREF
  _BYTE v21[40]; // [esp+54h] [ebp-70h] BYREF
  _BYTE v22[36]; // [esp+7Ch] [ebp-48h] BYREF
  _BYTE v23[20]; // [esp+A0h] [ebp-24h] BYREF
  int v24; // [esp+B4h] [ebp-10h] BYREF
  __int32 v25; // [esp+B8h] [ebp-Ch]
  int v26; // [esp+BCh] [ebp-8h]
  __int32 v27; // [esp+C0h] [ebp-4h] BYREF
  int savedregs; // [esp+C4h] [ebp+0h] BYREF

  v25 = _InterlockedExchange(&v27, a3);
  v26 = a2;
  v27 = a1;
  v19 = &savedregs;
  v18[1] = (unsigned int)&loc_4CFBC5;
  v18[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v18);
  unknown_libname_16(v23, a2 + 21);
  LOBYTE(v3) = 18;
  System::__linkproc__ PStrNCat(v23, dword_4CFBD4, v3);
  unknown_libname_16(v22, v23);
  LOBYTE(v4) = 34;
  System::__linkproc__ PStrNCat(v22, v26 + 4, v4);
  unknown_libname_16(v21, v22);
  LOBYTE(v5) = 36;
  System::__linkproc__ PStrNCat(v21, dword_4CFBDC, v5);
  unknown_libname_41();
  Sysutils::IntToStr(*(_DWORD *)(v26 + 36));
  Sysutils::IntToStr(*(_DWORD *)(v26 + 40));
  Sysutils::IntToStr(*(unsigned __int8 *)(v26 + 52));
  unknown_libname_91(*(_DWORD *)(v26 + 76), 0);
  Sysutils::IntToStr(*(unsigned __int16 *)(v26 + 420));
  Sysutils::IntToStr(*(unsigned __int16 *)(v26 + 424));
  Sysutils::IntToStr(*(unsigned __int16 *)(v26 + 422));
  Sysutils::IntToStr(*(unsigned __int16 *)(v26 + 426));
  Sysutils::IntToStr(*(unsigned __int8 *)(v26 + 414));
  HIWORD(v6) = HIWORD(v26);
  LOWORD(v6) = *(_WORD *)(v26 + 414);
  v7 = sub_407B28(v6);
  Sysutils::IntToStr(v7);
  Sysutils::IntToStr(*(unsigned __int8 *)(v26 + 416));
  HIWORD(v8) = HIWORD(v26);
  LOWORD(v8) = *(_WORD *)(v26 + 416);
  v9 = sub_407B28(v8);
  Sysutils::IntToStr(v9);
  Sysutils::IntToStr(*(unsigned __int8 *)(v26 + 418));
  HIWORD(v10) = HIWORD(v26);
  LOWORD(v10) = *(_WORD *)(v26 + 418);
  v11 = sub_407B28(v10);
  Sysutils::IntToStr(v11);
  Sysutils::IntToStr(*(unsigned __int8 *)(v26 + 410));
  HIWORD(v12) = HIWORD(v26);
  LOWORD(v12) = *(_WORD *)(v26 + 410);
  v13 = sub_407B28(v12);
  Sysutils::IntToStr(v13);
  Sysutils::IntToStr(*(unsigned __int8 *)(v26 + 412));
  HIWORD(v14) = HIWORD(v26);
  LOWORD(v14) = *(_WORD *)(v26 + 412);
  v15 = sub_407B28(v14);
  Sysutils::IntToStr(v15);
  Sysutils::IntToStr(*(unsigned __int8 *)(v26 + 572));
  Sysutils::IntToStr(*(unsigned __int8 *)(v26 + 620));
  System::__linkproc__ LStrCatN(v25, 40, v16);
  __writefsdword(0, v18[0]);
  v19 = (int *)&loc_4CFBCC;
  unknown_libname_35(&v20, 20);
  return unknown_libname_34(&v24);
}


// ==================== [0x4CFE1C] sub_4CFE1C (size: 346) ====================
int __fastcall sub_4CFE1C(int a1, void *a2, char a3)
{
  int v3; // ecx
  int v4; // ecx
  unsigned int v6[2]; // [esp-Ch] [ebp-30h] BYREF
  int *v7; // [esp-4h] [ebp-28h]
  int v8; // [esp+4h] [ebp-20h] BYREF
  _DWORD v9[2]; // [esp+8h] [ebp-1Ch] BYREF
  int v10; // [esp+10h] [ebp-14h] BYREF
  int v11; // [esp+14h] [ebp-10h]
  char v12; // [esp+1Bh] [ebp-9h]
  void *v13; // [esp+1Ch] [ebp-8h] BYREF
  int v14; // [esp+20h] [ebp-4h]
  int savedregs; // [esp+24h] [ebp+0h] BYREF

  v8 = 0;
  v9[0] = 0;
  v10 = 0;
  v9[1] = 0;
  v12 = a3;
  v13 = a2;
  v14 = a1;
  unknown_libname_44(a2);
  v7 = &savedregs;
  v6[1] = (unsigned int)&loc_4CFF6A;
  v6[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v6);
  v11 = sub_49A36C(*off_4EA07C[0], v13);
  if ( v11 != 0 )
  {
    unknown_libname_41();
    System::__linkproc__ LStrCatN(&v10, 12, v3);
    sub_4E42F8(v10);
    sub_491290(*off_4EA080[0], v11);
    if ( v12 != 0 )
      sub_4AF728(v13);
    System::__linkproc__ LStrCatN(v9, 3, v4);
    sub_4BB124(v14, v9[0], 1);
  }
  else
  {
    System::__linkproc__ LStrCat3((int)&v8, v13, &str__Guild_Name_Wro[1]);
    sub_4BB124(v14, v8, 0);
  }
  __writefsdword(0, v6[0]);
  v7 = (int *)&loc_4CFF71;
  unknown_libname_35(&v8, 4);
  return unknown_libname_34(&v13);
}


// ==================== [0x4CFFF8] sub_4CFFF8 (size: 735) ====================
int __fastcall sub_4CFFF8(int a1, int a2)
{
  int v2; // edx
  int v3; // eax
  int v4; // eax
  int v5; // eax
  int v6; // eax
  int v7; // eax
  int v8; // eax
  int v9; // eax
  int v10; // ecx
  int v11; // eax
  int v12; // eax
  int v13; // eax
  int v14; // ecx
  unsigned int v16[2]; // [esp-Ch] [ebp-50h] BYREF
  int *v17; // [esp-4h] [ebp-48h]
  int v18; // [esp+0h] [ebp-44h] BYREF
  _BYTE v19[28]; // [esp+4h] [ebp-40h] BYREF
  int v20; // [esp+20h] [ebp-24h] BYREF
  void *v21; // [esp+24h] [ebp-20h]
  int v22; // [esp+28h] [ebp-1Ch] BYREF
  int v23; // [esp+2Ch] [ebp-18h]
  System::TObject *v24; // [esp+30h] [ebp-14h]
  int v25; // [esp+34h] [ebp-10h]
  int v26; // [esp+38h] [ebp-Ch]
  int v27; // [esp+3Ch] [ebp-8h] BYREF
  int v28; // [esp+40h] [ebp-4h]
  int savedregs; // [esp+44h] [ebp+0h] BYREF

  v18 = 0;
  v20 = 0;
  v22 = 0;
  v21 = nullptr;
  v27 = a2;
  v28 = a1;
  unknown_libname_44(a2);
  v17 = &savedregs;
  v16[1] = (unsigned int)&loc_4D02CC;
  v16[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v16);
  if ( unknown_libname_84(v27, &str_all[1]) != 0 )
  {
    LOBYTE(v2) = 1;
    v24 = (System::TObject *)unknown_libname_22(off_40FAFC, v2);
    sub_4ACB84(v24, 9, *(_DWORD *)(v28 + 40));
    if ( *((_DWORD *)v24 + 2) - 1 >= 0 )
    {
      v23 = *((_DWORD *)v24 + 2);
      v26 = 0;
      do
      {
        v7 = Classes::TList::Get(v24, v26);
        sub_49E914(v7);
        v8 = Classes::TList::Get(v24, v26);
        sub_49EAF0(v8);
        v9 = Classes::TList::Get(v24, v26);
        unknown_libname_16(v19, v9 + 21);
        LOBYTE(v10) = 26;
        System::__linkproc__ PStrNCat(v19, dword_4D0314, v10);
        unknown_libname_41();
        sub_4BB124(v28, v20, 1);
        ++v26;
        --v23;
      }
      while ( v23 != 0 );
    }
    (*(void (__fastcall **)(System::TObject *))(*(_DWORD *)v24 + 8))(v24);
    sub_4AC8B8(v24, 9, *(_DWORD *)(v28 + 40));
    if ( *((_DWORD *)v24 + 2) - 1 >= 0 )
    {
      v23 = *((_DWORD *)v24 + 2);
      v26 = 0;
      do
      {
        v11 = Classes::TList::Get(v24, v26);
        sub_49EF0C(v11);
        v12 = Classes::TList::Get(v24, v26);
        sub_49EF7C(v12);
        v13 = Classes::TList::Get(v24, v26);
        unknown_libname_16(v19, v13 + 21);
        LOBYTE(v14) = 26;
        System::__linkproc__ PStrNCat(v19, dword_4D0314, v14);
        unknown_libname_41();
        sub_4BB124(v28, v18, 1);
        ++v26;
        --v23;
      }
      while ( v23 != 0 );
    }
    System::TObject::Free(v24);
  }
  else
  {
    sub_488504(*off_4EA6B8[0]);
    sub_487BD8(*off_4EA6B8[0]);
    v25 = 0;
    if ( *(_DWORD *)(*(_DWORD *)(*off_4EA490 + 108) + 8) - 1 >= 0 )
    {
      v23 = *(_DWORD *)(*(_DWORD *)(*off_4EA490 + 108) + 8);
      v26 = 0;
      do
      {
        v3 = Classes::TList::Get(*(Classes::TList **)(*off_4EA490 + 108), v26);
        sub_49E914(v3);
        v4 = Classes::TList::Get(*(Classes::TList **)(*off_4EA490 + 108), v26);
        sub_49EAF0(v4);
        ++v25;
        ++v26;
        --v23;
      }
      while ( v23 != 0 );
    }
    if ( *(_DWORD *)(*(_DWORD *)(*off_4EA490 + 104) + 8) - 1 >= 0 )
    {
      v23 = *(_DWORD *)(*(_DWORD *)(*off_4EA490 + 104) + 8);
      v26 = 0;
      do
      {
        v5 = Classes::TList::Get(*(Classes::TList **)(*off_4EA490 + 104), v26);
        sub_49EF0C(v5);
        v6 = Classes::TList::Get(*(Classes::TList **)(*off_4EA490 + 104), v26);
        sub_49EF7C(v6);
        ++v25;
        ++v26;
        --v23;
      }
      while ( v23 != 0 );
    }
    Sysutils::IntToStr(v25);
    System::__linkproc__ LStrCat3((int)&v22, &str_Reload_npc_info[1], v21);
    sub_4BB124(v28, v22, 1);
  }
  __writefsdword(0, v16[0]);
  v17 = (int *)&loc_4D02D3;
  unknown_libname_34(&v18);
  unknown_libname_35(&v20, 3);
  return unknown_libname_34(&v27);
}


// ==================== [0x4D0404] sub_4D0404 (size: 404) ====================
int __fastcall sub_4D0404(int a1, void *a2, int a3, char a4)
{
  int v4; // eax
  int v5; // eax
  int v6; // eax
  int v7; // ecx
  int v8; // ecx
  unsigned int v10[2]; // [esp-Ch] [ebp-38h] BYREF
  int *v11; // [esp-4h] [ebp-30h]
  int v12[2]; // [esp+4h] [ebp-28h] BYREF
  _DWORD v13[2]; // [esp+Ch] [ebp-20h] BYREF
  int v14; // [esp+14h] [ebp-18h] BYREF
  int v15; // [esp+18h] [ebp-14h]
  int v16; // [esp+1Ch] [ebp-10h]
  int v17; // [esp+20h] [ebp-Ch] BYREF
  void *v18; // [esp+24h] [ebp-8h]
  int v19; // [esp+28h] [ebp-4h]
  int savedregs; // [esp+2Ch] [ebp+0h] BYREF

  v12[0] = 0;
  v13[0] = 0;
  v12[1] = 0;
  v14 = 0;
  v13[1] = 0;
  v17 = a3;
  v18 = a2;
  v19 = a1;
  unknown_libname_44(a2);
  unknown_libname_44(v17);
  v11 = &savedregs;
  v10[1] = (unsigned int)&loc_4D058A;
  v10[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v10);
  v15 = sub_453B20(v17, 5);
  if ( v18 != nullptr )
  {
    v4 = sub_45593C(*off_4EA318[0], v18);
    v16 = v4;
    if ( v4 < 0 )
    {
      v6 = sub_407B30(v4);
      sub_455574(*off_4EA318[0], v18, 60000 * v15 + v6);
    }
    else
    {
      v5 = (*(int (__fastcall **)(_DWORD, int))(**(_DWORD **)off_4EA318[0] + 24))(*off_4EA318[0], v16);
      (*(void (__fastcall **)(_DWORD, int, int))(**(_DWORD **)off_4EA318[0] + 36))(
        *off_4EA318[0],
        v16,
        60000 * v15 + v5);
    }
    if ( a4 != 0 )
    {
      Sysutils::IntToStr(v15);
      System::__linkproc__ LStrCatN(&v14, 3, v7);
      sub_4AF728(v14);
    }
    Sysutils::IntToStr(v15);
    System::__linkproc__ LStrCatN(v13, 4, v8);
    sub_4BB124(v19, v13[0], 1);
  }
  else
  {
    System::__linkproc__ LStrCat3((int)v12, nullptr, &str___not_found_7[1]);
    sub_4BB124(v19, v12[0], 0);
  }
  __writefsdword(0, v10[0]);
  v11 = (int *)&loc_4D0591;
  unknown_libname_35(v12, 5);
  return unknown_libname_35(&v17, 2);
}


// ==================== [0x4D0824] sub_4D0824 (size: 486) ====================
int __fastcall sub_4D0824(int a1, const void *a2)
{
  int v2; // ecx
  int v3; // ecx
  int v4; // eax
  unsigned int v6[2]; // [esp-Ch] [ebp-1C0h] BYREF
  int *v7; // [esp-4h] [ebp-1B8h]
  int v8; // [esp+8h] [ebp-1ACh] BYREF
  _DWORD v9[3]; // [esp+Ch] [ebp-1A8h] BYREF
  void *v10; // [esp+18h] [ebp-19Ch] BYREF
  _BYTE v11[20]; // [esp+1Ch] [ebp-198h] BYREF
  int v12; // [esp+30h] [ebp-184h] BYREF
  _BYTE v13[256]; // [esp+34h] [ebp-180h] BYREF
  _BYTE v14[44]; // [esp+134h] [ebp-80h] BYREF
  _BYTE v15[16]; // [esp+160h] [ebp-54h] BYREF
  char v16; // [esp+170h] [ebp-44h]
  int v17; // [esp+18Ch] [ebp-28h]
  __int16 v18; // [esp+190h] [ebp-24h]
  __int16 v19; // [esp+192h] [ebp-22h]
  _DWORD v20[6]; // [esp+194h] [ebp-20h] BYREF
  const void *v21; // [esp+1ACh] [ebp-8h]
  int v22; // [esp+1B0h] [ebp-4h]
  int savedregs; // [esp+1B4h] [ebp+0h] BYREF

  v8 = 0;
  v12 = 0;
  v10 = nullptr;
  qmemcpy(v20, a2, sizeof(v20));
  v22 = a1;
  v7 = &savedregs;
  v6[1] = (unsigned int)&loc_4D09FD;
  v6[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v6);
  v21 = (const void *)sub_4AC2F8(*off_4EA490, LOWORD(v20[1]));
  if ( v21 != nullptr )
  {
    qmemcpy(v14, v21, sizeof(v14));
    sub_495974(*off_4EA324, v20, v14);
    System::Move(v14, v15, 44);
    v17 = v20[0];
    v18 = HIWORD(v20[1]);
    v19 = v20[2];
    if ( v14[15] == 50 )
    {
      unknown_libname_16(v11, v15);
      LOBYTE(v2) = 16;
      System::__linkproc__ PStrNCat(v11, sub_4D0A0C, v2);
      unknown_libname_41();
      Sysutils::IntToStr(HIWORD(v20[1]));
      System::__linkproc__ LStrCat((int)&v12, v10);
      System::__linkproc__ LStrToString(v13, v12, 255);
      LOBYTE(v3) = 14;
      System::__linkproc__ PStrNCpy(v15, v13, v3);
    }
    if ( v14[15] == 15 || (unsigned __int8)(v14[15] - 19) < 6u || v14[15] == 26 )
    {
      if ( BYTE2(v20[4]) != 0 )
        v16 = -126;
      else
        v16 = 0;
    }
    sub_4546F8(v9, 1, 0);
    v4 = v22;
    *(_DWORD *)(v22 + 1360) = v9[0];
    *(_DWORD *)(v4 + 1364) = v9[1];
    *(_DWORD *)(v4 + 1368) = v9[2];
    sub_4565B0(v15, 52, &v8, v6[0]);
    sub_4CAB38(v22, (void *)(v22 + 1360), v8);
  }
  __writefsdword(0, v6[0]);
  v7 = (int *)&loc_4D0A04;
  unknown_libname_34(&v8);
  unknown_libname_34(&v10);
  return unknown_libname_34(&v12);
}


// ==================== [0x4D0A10] sub_4D0A10 (size: 453) ====================
int __fastcall sub_4D0A10(int a1, const void *a2)
{
  int v2; // ecx
  int v3; // ecx
  int v4; // eax
  unsigned int v6[2]; // [esp-Ch] [ebp-1C0h] BYREF
  int *v7; // [esp-4h] [ebp-1B8h]
  int v8; // [esp+8h] [ebp-1ACh] BYREF
  _DWORD v9[3]; // [esp+Ch] [ebp-1A8h] BYREF
  void *v10; // [esp+18h] [ebp-19Ch] BYREF
  _BYTE v11[20]; // [esp+1Ch] [ebp-198h] BYREF
  int v12; // [esp+30h] [ebp-184h] BYREF
  _BYTE v13[256]; // [esp+34h] [ebp-180h] BYREF
  _BYTE v14[44]; // [esp+134h] [ebp-80h] BYREF
  _BYTE v15[44]; // [esp+160h] [ebp-54h] BYREF
  int v16; // [esp+18Ch] [ebp-28h]
  __int16 v17; // [esp+190h] [ebp-24h]
  __int16 v18; // [esp+192h] [ebp-22h]
  _DWORD v19[6]; // [esp+194h] [ebp-20h] BYREF
  const void *v20; // [esp+1ACh] [ebp-8h]
  int v21; // [esp+1B0h] [ebp-4h]
  int savedregs; // [esp+1B4h] [ebp+0h] BYREF

  v8 = 0;
  v12 = 0;
  v10 = nullptr;
  qmemcpy(v19, a2, sizeof(v19));
  v21 = a1;
  v7 = &savedregs;
  v6[1] = (unsigned int)&loc_4D0BC8;
  v6[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v6);
  v20 = (const void *)sub_4AC2F8(*off_4EA490, LOWORD(v19[1]));
  if ( v20 != nullptr )
  {
    qmemcpy(v14, v20, sizeof(v14));
    sub_495974(*off_4EA324, v19, v14);
    System::Move(v14, v15, 44);
    v16 = v19[0];
    v17 = HIWORD(v19[1]);
    v18 = v19[2];
    if ( v14[15] == 50 )
    {
      unknown_libname_16(v11, v15);
      LOBYTE(v2) = 16;
      System::__linkproc__ PStrNCat(v11, sub_4D0BD8, v2);
      unknown_libname_41();
      Sysutils::IntToStr(HIWORD(v19[1]));
      System::__linkproc__ LStrCat((int)&v12, v10);
      System::__linkproc__ LStrToString(v13, v12, 255);
      LOBYTE(v3) = 14;
      System::__linkproc__ PStrNCpy(v15, v13, v3);
    }
    sub_4546F8(v9, 1, 0);
    v4 = v21;
    *(_DWORD *)(v21 + 1360) = v9[0];
    *(_DWORD *)(v4 + 1364) = v9[1];
    *(_DWORD *)(v4 + 1368) = v9[2];
    sub_4565B0(v15, 52, &v8, v6[0]);
    sub_4CAB38(v21, (void *)(v21 + 1360), v8);
  }
  __writefsdword(0, v6[0]);
  v7 = (int *)&loc_4D0BCF;
  unknown_libname_34(&v8);
  unknown_libname_34(&v10);
  return unknown_libname_34(&v12);
}


// ==================== [0x4D0BDC] sub_4D0BDC (size: 459) ====================
int __fastcall sub_4D0BDC(int a1, const void *a2)
{
  int v2; // ecx
  int v3; // ecx
  int v4; // eax
  unsigned int v6[2]; // [esp-Ch] [ebp-1C0h] BYREF
  int *v7; // [esp-4h] [ebp-1B8h]
  int v8; // [esp+8h] [ebp-1ACh] BYREF
  _DWORD v9[3]; // [esp+Ch] [ebp-1A8h] BYREF
  void *v10; // [esp+18h] [ebp-19Ch] BYREF
  _BYTE v11[20]; // [esp+1Ch] [ebp-198h] BYREF
  int v12; // [esp+30h] [ebp-184h] BYREF
  _BYTE v13[256]; // [esp+34h] [ebp-180h] BYREF
  _BYTE v14[44]; // [esp+134h] [ebp-80h] BYREF
  _BYTE v15[44]; // [esp+160h] [ebp-54h] BYREF
  int v16; // [esp+18Ch] [ebp-28h]
  __int16 v17; // [esp+190h] [ebp-24h]
  __int16 v18; // [esp+192h] [ebp-22h]
  _DWORD v19[6]; // [esp+194h] [ebp-20h] BYREF
  const void *v20; // [esp+1ACh] [ebp-8h]
  int v21; // [esp+1B0h] [ebp-4h]
  int savedregs; // [esp+1B4h] [ebp+0h] BYREF

  v8 = 0;
  v12 = 0;
  v10 = nullptr;
  qmemcpy(v19, a2, sizeof(v19));
  v21 = a1;
  v7 = &savedregs;
  v6[1] = (unsigned int)&loc_4D0D9A;
  v6[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v6);
  v20 = (const void *)sub_4AC2F8(*off_4EA490, LOWORD(v19[1]));
  if ( v20 != nullptr )
  {
    qmemcpy(v14, v20, sizeof(v14));
    sub_495974(*off_4EA324, v19, v14);
    System::Move(v14, v15, 44);
    v17 = HIWORD(v19[1]);
    v18 = v19[2];
    v16 = v19[0];
    if ( v14[15] == 50 )
    {
      unknown_libname_16(v11, v15);
      LOBYTE(v2) = 16;
      System::__linkproc__ PStrNCat(v11, sub_4D0DA8, v2);
      unknown_libname_41();
      Sysutils::IntToStr(HIWORD(v19[1]));
      System::__linkproc__ LStrCat((int)&v12, v10);
      System::__linkproc__ LStrToString(v13, v12, 255);
      LOBYTE(v3) = 14;
      System::__linkproc__ PStrNCpy(v15, v13, v3);
    }
    sub_4546F8(v9, 1, 0);
    v4 = v21;
    *(_DWORD *)(v21 + 1360) = v9[0];
    *(_DWORD *)(v4 + 1364) = v9[1];
    *(_DWORD *)(v4 + 1368) = v9[2];
    sub_4565B0(v15, 52, &v8, v6[0]);
    sub_4CAB38(v21, (void *)(v21 + 1360), v8);
  }
  __writefsdword(0, v6[0]);
  v7 = (int *)&loc_4D0DA1;
  unknown_libname_34(&v8);
  unknown_libname_34(&v10);
  return unknown_libname_34(&v12);
}


// ==================== [0x4D0EDC] sub_4D0EDC (size: 574) ====================
int __fastcall sub_4D0EDC(int a1)
{
  int v1; // ecx
  int v2; // ecx
  int v3; // ecx
  int v4; // eax
  unsigned int v6[2]; // [esp-Ch] [ebp-1B8h] BYREF
  int *v7; // [esp-4h] [ebp-1B0h]
  _DWORD v8[3]; // [esp+8h] [ebp-1A4h] BYREF
  int v9; // [esp+14h] [ebp-198h] BYREF
  void *v10; // [esp+18h] [ebp-194h]
  _BYTE v11[20]; // [esp+1Ch] [ebp-190h] BYREF
  int v12; // [esp+30h] [ebp-17Ch] BYREF
  _BYTE v13[256]; // [esp+34h] [ebp-178h] BYREF
  _BYTE v14[44]; // [esp+134h] [ebp-78h] BYREF
  _BYTE v15[44]; // [esp+160h] [ebp-4Ch] BYREF
  int v16; // [esp+18Ch] [ebp-20h]
  __int16 v17; // [esp+190h] [ebp-1Ch]
  __int16 v18; // [esp+192h] [ebp-1Ah]
  int v19; // [esp+194h] [ebp-18h]
  int v20; // [esp+198h] [ebp-14h] BYREF
  const void *v21; // [esp+19Ch] [ebp-10h]
  int *v22; // [esp+1A0h] [ebp-Ch]
  int v23; // [esp+1A4h] [ebp-8h]
  int v24; // [esp+1A8h] [ebp-4h]
  int savedregs; // [esp+1ACh] [ebp+0h] BYREF

  v9 = 0;
  v12 = 0;
  v10 = nullptr;
  v20 = 0;
  v24 = a1;
  v7 = &savedregs;
  v6[1] = (unsigned int)&loc_4D110D;
  v6[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v6);
  unknown_libname_34(&v20);
  if ( *(_DWORD *)(*(_DWORD *)(v24 + 1036) + 8) - 1 >= 0 )
  {
    v19 = *(_DWORD *)(*(_DWORD *)(v24 + 1036) + 8);
    v23 = 0;
    do
    {
      v22 = (int *)Classes::TList::Get(*(Classes::TList **)(v24 + 1036), v23);
      v21 = (const void *)sub_4AC2F8(*off_4EA490, *((unsigned __int16 *)v22 + 2));
      if ( v21 != nullptr )
      {
        qmemcpy(v14, v21, sizeof(v14));
        sub_495974(*off_4EA324, v22, v14);
        System::Move(v14, v15, 44);
        v17 = *((_WORD *)v22 + 3);
        v18 = *((_WORD *)v22 + 4);
        v16 = *v22;
        if ( v14[15] == 50 )
        {
          unknown_libname_16(v11, v15);
          LOBYTE(v1) = 16;
          System::__linkproc__ PStrNCat(v11, dword_4D111C, v1);
          unknown_libname_41();
          Sysutils::IntToStr(*((unsigned __int16 *)v22 + 3));
          System::__linkproc__ LStrCat((int)&v12, v10);
          System::__linkproc__ LStrToString(v13, v12, 255);
          LOBYTE(v2) = 14;
          System::__linkproc__ PStrNCpy(v15, v13, v2);
        }
        sub_4565B0(v15, 52, &v9, v20);
        System::__linkproc__ LStrCatN(&v20, 3, v3);
      }
      ++v23;
      --v19;
    }
    while ( v19 != 0 );
  }
  if ( v20 != 0 )
  {
    sub_4546F8(v8, *(_WORD *)(*(_DWORD *)(v24 + 1036) + 8), 0);
    v4 = v24;
    *(_DWORD *)(v24 + 1360) = v8[0];
    *(_DWORD *)(v4 + 1364) = v8[1];
    *(_DWORD *)(v4 + 1368) = v8[2];
    sub_4CAB38(v24, (void *)(v24 + 1360), v20);
  }
  __writefsdword(0, v6[0]);
  v7 = (int *)&loc_4D1114;
  unknown_libname_35(&v9, 2);
  unknown_libname_34(&v12);
  return unknown_libname_34(&v20);
}


// ==================== [0x4D112C] sub_4D112C (size: 444) ====================
int __fastcall sub_4D112C(int a1)
{
  int v1; // ecx
  int v2; // eax
  unsigned int v4[2]; // [esp-Ch] [ebp-98h] BYREF
  int *v5; // [esp-4h] [ebp-90h]
  _DWORD v6[3]; // [esp+8h] [ebp-84h] BYREF
  _DWORD v7[2]; // [esp+14h] [ebp-78h] BYREF
  _BYTE v8[44]; // [esp+1Ch] [ebp-70h] BYREF
  _BYTE v9[44]; // [esp+48h] [ebp-44h] BYREF
  int v10; // [esp+74h] [ebp-18h]
  __int16 v11; // [esp+78h] [ebp-14h]
  __int16 v12; // [esp+7Ah] [ebp-12h]
  int v13; // [esp+7Ch] [ebp-10h] BYREF
  const void *v14; // [esp+80h] [ebp-Ch]
  int i; // [esp+84h] [ebp-8h]
  int v16; // [esp+88h] [ebp-4h]
  int savedregs; // [esp+8Ch] [ebp+0h] BYREF

  v7[1] = 0;
  v7[0] = 0;
  v13 = 0;
  v16 = a1;
  v5 = &savedregs;
  v4[1] = (unsigned int)&loc_4D12DB;
  v4[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v4);
  unknown_libname_34(&v13);
  for ( i = 0; i != 9; ++i )
  {
    if ( *(_WORD *)(v16 + 24 * i + 1060) != 0 )
    {
      v14 = (const void *)sub_4AC2F8(*off_4EA490, *(unsigned __int16 *)(v16 + 24 * i + 1060));
      if ( v14 != nullptr )
      {
        qmemcpy(v8, v14, sizeof(v8));
        sub_495974(*off_4EA324, v16 + 24 * i + 1056, v8);
        System::Move(v8, v9, 44);
        v11 = *(_WORD *)(v16 + 24 * i + 1062);
        v12 = *(_WORD *)(v16 + 24 * i + 1064);
        v10 = *(_DWORD *)(v16 + 24 * i + 1056);
        Sysutils::IntToStr(i);
        sub_4565B0(v9, 52, v7, &str___93[1]);
        System::__linkproc__ LStrCatN(&v13, 5, v1);
      }
    }
  }
  if ( v13 != 0 )
  {
    sub_4546F8(v6, 0, 0);
    v2 = v16;
    *(_DWORD *)(v16 + 1360) = v6[0];
    *(_DWORD *)(v2 + 1364) = v6[1];
    *(_DWORD *)(v2 + 1368) = v6[2];
    sub_4CAB38(v16, (void *)(v16 + 1360), v13);
  }
  __writefsdword(0, v4[0]);
  v5 = (int *)&loc_4D12E2;
  unknown_libname_35(v7, 2);
  return unknown_libname_34(&v13);
}


// ==================== [0x4D1418] sub_4D1418 (size: 307) ====================
int __fastcall sub_4D1418(int a1)
{
  int v1; // ecx
  int v2; // eax
  unsigned int v4[2]; // [esp-Ch] [ebp-8Ch] BYREF
  int *v5; // [esp-4h] [ebp-84h]
  _DWORD v6[3]; // [esp+8h] [ebp-78h] BYREF
  int v7; // [esp+14h] [ebp-6Ch] BYREF
  _BYTE v8[4]; // [esp+18h] [ebp-68h] BYREF
  int v9; // [esp+1Ch] [ebp-64h]
  _BYTE v10[76]; // [esp+20h] [ebp-60h] BYREF
  int v11; // [esp+6Ch] [ebp-14h]
  int v12; // [esp+70h] [ebp-10h]
  int v13; // [esp+74h] [ebp-Ch] BYREF
  int v14; // [esp+78h] [ebp-8h]
  int v15; // [esp+7Ch] [ebp-4h]
  int savedregs; // [esp+80h] [ebp+0h] BYREF

  v7 = 0;
  v13 = 0;
  v15 = a1;
  v5 = &savedregs;
  v4[1] = (unsigned int)&loc_4D153E;
  v4[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v4);
  unknown_libname_34(&v13);
  if ( *(_DWORD *)(*(_DWORD *)(v15 + 1052) + 8) - 1 >= 0 )
  {
    v11 = *(_DWORD *)(*(_DWORD *)(v15 + 1052) + 8);
    v14 = 0;
    do
    {
      v12 = Classes::TList::Get(*(Classes::TList **)(v15 + 1052), v14);
      v8[0] = *(_BYTE *)(v12 + 7);
      v8[1] = *(_BYTE *)(v12 + 6);
      v9 = *(_DWORD *)(v12 + 8);
      qmemcpy(v10, *(const void **)v12, sizeof(v10));
      sub_4565B0(v8, 84, &v7, v13);
      System::__linkproc__ LStrCatN(&v13, 3, v1);
      ++v14;
      --v11;
    }
    while ( v11 != 0 );
  }
  sub_4546F8(v6, *(_WORD *)(*(_DWORD *)(v15 + 1052) + 8), 0);
  v2 = v15;
  *(_DWORD *)(v15 + 1360) = v6[0];
  *(_DWORD *)(v2 + 1364) = v6[1];
  *(_DWORD *)(v2 + 1368) = v6[2];
  sub_4CAB38(v15, (void *)(v15 + 1360), v13);
  __writefsdword(0, v4[0]);
  v5 = (int *)&loc_4D1545;
  unknown_libname_34(&v7);
  return unknown_libname_34(&v13);
}


// ==================== [0x4D1558] sub_4D1558 (size: 486) ====================
int __fastcall sub_4D1558(int a1, void *a2, void *a3)
{
  int v3; // ecx
  int v4; // ecx
  unsigned int v6[2]; // [esp-Ch] [ebp-54h] BYREF
  int *v7; // [esp-4h] [ebp-4Ch]
  int v8[2]; // [esp+4h] [ebp-44h] BYREF
  int v9; // [esp+Ch] [ebp-3Ch] BYREF
  _BYTE v10[20]; // [esp+10h] [ebp-38h] BYREF
  int v11; // [esp+24h] [ebp-24h] BYREF
  int v12; // [esp+28h] [ebp-20h] BYREF
  int v13; // [esp+2Ch] [ebp-1Ch]
  int v14; // [esp+30h] [ebp-18h] BYREF
  int v15; // [esp+34h] [ebp-14h] BYREF
  int v16; // [esp+38h] [ebp-10h]
  void *v17; // [esp+3Ch] [ebp-Ch] BYREF
  void *v18; // [esp+40h] [ebp-8h]
  int v19; // [esp+44h] [ebp-4h]
  int savedregs; // [esp+48h] [ebp+0h] BYREF

  v8[0] = 0;
  v9 = 0;
  v8[1] = 0;
  v11 = 0;
  v12 = 0;
  v13 = 0;
  v14 = 0;
  v17 = a3;
  v18 = a2;
  v19 = a1;
  unknown_libname_44(a2);
  unknown_libname_44(v17);
  v7 = &savedregs;
  v6[1] = (unsigned int)&loc_4D1732;
  v6[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v6);
  v16 = sub_4AE640(*off_4EA490);
  if ( v16 != 0 )
  {
    if ( *(_BYTE *)(v16 + 1468) != 0 )
    {
      if ( *(_BYTE *)(v16 + 636) != 0
        && (unknown_libname_41(), (unsigned __int8)Iniohandlerstack::TInIOHandlerStack::WriteDirect(v16, v13) == 0) )
      {
        unknown_libname_16(v10, v19 + 21);
        LOBYTE(v3) = 17;
        System::__linkproc__ PStrNCat(v10, dword_4D176C, v3);
        unknown_libname_41();
        System::__linkproc__ LStrCat((int)&v11, v17);
        sub_4B865C(v16, v19, 10031, 0, 0, 0, 0, v11);
      }
      else
      {
        System::__linkproc__ LStrCat3((int)&v12, v18, &str__Rejects[1]);
        sub_4BB124(v19, v12, 0);
      }
    }
    else
    {
      System::__linkproc__ LStrCat3((int)&v14, v18, &str____________3[1]);
      sub_4BB124(v19, v14, 0);
    }
  }
  else if ( (unsigned __int8)sub_4AE708(*off_4EA490, v18, &v15) != 0 )
  {
    unknown_libname_41();
    System::__linkproc__ LStrCatN(&v9, 5, v4);
    sub_4AF728(v9);
  }
  else
  {
    System::__linkproc__ LStrCat3((int)v8, v18, aNotFound_11);
    sub_4BB124(v19, v8[0], 0);
  }
  __writefsdword(0, v6[0]);
  v7 = (int *)&loc_4D1739;
  unknown_libname_35(v8, 3);
  unknown_libname_35(&v11, 4);
  return unknown_libname_35(&v17, 2);
}


// ==================== [0x4D1E54] sub_4D1E54 (size: 5178) ====================
int __fastcall sub_4D1E54(int *a1, _BYTE *a2)
{
  char v2; // zf
  DWORD TickCount; // eax
  int v4; // ecx
  DWORD v5; // eax
  int v6; // eax
  int v7; // ecx
  int v8; // ecx
  _strings *v9; // edx
  unsigned __int8 v10; // al
  void *v11; // ecx
  _strings *v12; // edx
  char *v13; // eax
  _BYTE *v14; // ecx
  int v15; // ebx
  int *v16; // ecx
  char *v17; // eax
  int v18; // eax
  int v19; // eax
  int v20; // ecx
  int v21; // eax
  int v22; // ecx
  int v23; // ecx
  int v24; // ecx
  int v25; // eax
  int v26; // ecx
  char v27; // al
  int v28; // ecx
  int v29; // ecx
  int v30; // ecx
  int v31; // ecx
  int v32; // ecx
  int v33; // ecx
  unsigned __int8 v34; // al
  int v35; // ecx
  int v36; // ecx
  int v37; // eax
  double v38; // st7
  int v39; // eax
  char v40; // al
  int v41; // eax
  char v42; // al
  int v43; // eax
  int v44; // eax
  int v45; // ecx
  char v46; // al
  int v47; // ecx
  int v48; // ecx
  int v49; // ecx
  int v50; // ecx
  int v51; // eax
  int v52; // ecx
  int v53; // eax
  int v54; // ecx
  int v55; // eax
  int v56; // ecx
  int v57; // ecx
  int v58; // eax
  int v59; // ecx
  int v60; // eax
  int v61; // ecx
  int v62; // ecx
  int v63; // ecx
  __int32 v64; // eax
  __int32 v65; // eax
  int v66; // eax
  int v67; // eax
  int v68; // eax
  int v69; // eax
  int v70; // eax
  int v71; // eax
  int v72; // eax
  int v73; // eax
  int v74; // eax
  int v75; // ecx
  int v76; // eax
  int v77; // ecx
  int v78; // eax
  int v79; // ecx
  char v80; // al
  char v81; // al
  int v82; // eax
  int v83; // ecx
  int v84; // eax
  int v85; // ecx
  int v86; // eax
  int v87; // ecx
  int v88; // ecx
  int v89; // ecx
  int v90; // ecx
  int v91; // ecx
  DWORD v92; // eax
  _strings *v94; // [esp-4h] [ebp-378h]
  void *v95; // [esp+0h] [ebp-374h]
  char v96; // [esp+14h] [ebp-360h]
  int v97; // [esp+14h] [ebp-360h]
  int v98; // [esp+18h] [ebp-35Ch]
  void *v99; // [esp+18h] [ebp-35Ch]
  int v100; // [esp+18h] [ebp-35Ch]
  int v101; // [esp+18h] [ebp-35Ch]
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp+1Ch] [ebp-358h] BYREF
  int (__cdecl *v103)(int, int); // [esp+20h] [ebp-354h]
  int *v104; // [esp+24h] [ebp-350h]
  int v105; // [esp+2Ch] [ebp-348h] BYREF
  _BYTE v106[20]; // [esp+30h] [ebp-344h] BYREF
  int v107; // [esp+44h] [ebp-330h] BYREF
  int v108; // [esp+4Ch] [ebp-328h] BYREF
  _BYTE v109[16]; // [esp+50h] [ebp-324h] BYREF
  int v110; // [esp+60h] [ebp-314h] BYREF
  _BYTE v111[20]; // [esp+64h] [ebp-310h] BYREF
  int v112; // [esp+78h] [ebp-2FCh] BYREF
  char v113; // [esp+7Ch] [ebp-2F8h]
  void *v114[2]; // [esp+80h] [ebp-2F4h] BYREF
  int v115[2]; // [esp+88h] [ebp-2ECh] BYREF
  int v116[2]; // [esp+90h] [ebp-2E4h] BYREF
  void *v117[2]; // [esp+9Ch] [ebp-2D8h] BYREF
  int v118; // [esp+A4h] [ebp-2D0h] BYREF
  int v119; // [esp+ACh] [ebp-2C8h] BYREF
  int v120; // [esp+B0h] [ebp-2C4h] BYREF
  int v121; // [esp+B4h] [ebp-2C0h] BYREF
  int v122; // [esp+B8h] [ebp-2BCh] BYREF
  int v123; // [esp+BCh] [ebp-2B8h] BYREF
  int v124; // [esp+C0h] [ebp-2B4h] BYREF
  int v125; // [esp+C4h] [ebp-2B0h] BYREF
  int v126; // [esp+C8h] [ebp-2ACh] BYREF
  int v127; // [esp+CCh] [ebp-2A8h] BYREF
  _BYTE v128[28]; // [esp+D0h] [ebp-2A4h] BYREF
  _BYTE v129[24]; // [esp+ECh] [ebp-288h] BYREF
  int v130; // [esp+104h] [ebp-270h] BYREF
  int v131; // [esp+108h] [ebp-26Ch] BYREF
  _BYTE v132[28]; // [esp+10Ch] [ebp-268h] BYREF
  _BYTE v133[28]; // [esp+128h] [ebp-24Ch] BYREF
  int v134[2]; // [esp+144h] [ebp-230h] BYREF
  int v135[2]; // [esp+14Ch] [ebp-228h] BYREF
  int v136[2]; // [esp+154h] [ebp-220h] BYREF
  int v137[2]; // [esp+15Ch] [ebp-218h] BYREF
  int v138[2]; // [esp+164h] [ebp-210h] BYREF
  int v139[2]; // [esp+16Ch] [ebp-208h] BYREF
  int v140[2]; // [esp+174h] [ebp-200h] BYREF
  int v141[12]; // [esp+17Ch] [ebp-1F8h] BYREF
  char v142[56]; // [esp+1ACh] [ebp-1C8h] BYREF
  char v143[8]; // [esp+1E4h] [ebp-190h] BYREF
  int v144; // [esp+1F4h] [ebp-180h] BYREF
  int v145; // [esp+200h] [ebp-174h] BYREF
  int v146; // [esp+224h] [ebp-150h] BYREF
  int v147[13]; // [esp+234h] [ebp-140h] BYREF
  int v148; // [esp+270h] [ebp-104h] BYREF
  _BYTE v149[20]; // [esp+278h] [ebp-FCh] BYREF
  int v150; // [esp+28Ch] [ebp-E8h] BYREF
  int v151; // [esp+290h] [ebp-E4h] BYREF
  int v152; // [esp+29Ch] [ebp-D8h] BYREF
  _BYTE v153[24]; // [esp+2A0h] [ebp-D4h] BYREF
  int v154; // [esp+2B8h] [ebp-BCh] BYREF
  int v155; // [esp+2C0h] [ebp-B4h] BYREF
  int v156; // [esp+2C8h] [ebp-ACh] BYREF
  char v157[4]; // [esp+2CCh] [ebp-A8h] BYREF
  int v158; // [esp+2D8h] [ebp-9Ch] BYREF
  int v159; // [esp+2E0h] [ebp-94h] BYREF
  int v160; // [esp+2E4h] [ebp-90h] BYREF
  int v161; // [esp+2E8h] [ebp-8Ch] BYREF
  int v162; // [esp+2ECh] [ebp-88h] BYREF
  int v163; // [esp+2F0h] [ebp-84h] BYREF
  int v164; // [esp+2F4h] [ebp-80h] BYREF
  int v165; // [esp+300h] [ebp-74h] BYREF
  int v166; // [esp+304h] [ebp-70h] BYREF
  int v167; // [esp+308h] [ebp-6Ch] BYREF
  int v168; // [esp+30Ch] [ebp-68h] BYREF
  int v169; // [esp+310h] [ebp-64h] BYREF
  int v170; // [esp+314h] [ebp-60h] BYREF
  int v171; // [esp+318h] [ebp-5Ch] BYREF
  int v172; // [esp+31Ch] [ebp-58h] BYREF
  _DWORD v173[2]; // [esp+320h] [ebp-54h] BYREF
  _DWORD v174[2]; // [esp+328h] [ebp-4Ch] BYREF
  int v175; // [esp+330h] [ebp-44h]
  char v176; // [esp+337h] [ebp-3Dh]
  signed int v177; // [esp+338h] [ebp-3Ch]
  int v178; // [esp+33Ch] [ebp-38h]
  int v179; // [esp+340h] [ebp-34h]
  int v180; // [esp+344h] [ebp-30h]
  char v181[4]; // [esp+348h] [ebp-2Ch] BYREF
  void *v182; // [esp+350h] [ebp-24h]
  void *v183; // [esp+354h] [ebp-20h]
  void *v184; // [esp+358h] [ebp-1Ch]
  void *v185; // [esp+35Ch] [ebp-18h]
  void *v186; // [esp+360h] [ebp-14h]
  void *v187; // [esp+364h] [ebp-10h]
  void *v188; // [esp+368h] [ebp-Ch] BYREF
  _BYTE *v189; // [esp+36Ch] [ebp-8h]
  int *v190; // [esp+370h] [ebp-4h]
  int savedregs; // [esp+374h] [ebp+0h] BYREF

  v189 = a2;
  v190 = a1;
  unknown_libname_44(a2);
  v104 = &savedregs;
  v103 = j_unknown_libname_32_31;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  if ( v189 == nullptr )
    goto LABEL_391;
  if ( *((_BYTE *)v190 + 582) != 0 )
  {
    *((_BYTE *)v190 + 582) = 0;
    if ( sub_453B20(v189, 0) == 19800315 )
    {
      *((_BYTE *)v190 + 621) = 0;
      sub_4BB124(v190, dword_4D53A0, 0);
    }
    goto LABEL_391;
  }
  if ( *((_BYTE *)v190 + 583) != 0 )
  {
    *((_BYTE *)v190 + 583) = 0;
    System::__linkproc__ LStrCmp(v189, dword_4D53B0);
    if ( v2 )
      *((_BYTE *)v190 + 621) = 0;
    else
      sub_4BB124(v190, &str_Fail_1[1], 0);
    goto LABEL_391;
  }
  if ( *v189 == 64 )
  {
    unknown_libname_43(v189);
    System::__linkproc__ LStrCopy(&v188);
    qmemcpy(&v173[1], " ,:", 3);
    sub_453DBC(v174, 2);
    unknown_libname_37(&v188, v174[0]);
    qmemcpy(&v173[1], " ,:", 3);
    sub_453DBC(v173, 2);
    unknown_libname_37(&v188, v173[0]);
    if ( v188 != nullptr )
    {
      qmemcpy(&v173[1], " ,:", 3);
      sub_453DBC(&v172, 2);
      unknown_libname_37(&v188, v172);
    }
    if ( v188 != nullptr )
    {
      qmemcpy(&v173[1], " ,:", 3);
      sub_453DBC(&v171, 2);
      unknown_libname_37(&v188, v171);
    }
    if ( v188 != nullptr )
    {
      qmemcpy(&v173[1], " ,:", 3);
      sub_453DBC(&v170, 2);
      unknown_libname_37(&v188, v170);
    }
    if ( v188 != nullptr )
    {
      qmemcpy(&v173[1], " ,:", 3);
      sub_453DBC(&v169, 2);
      unknown_libname_37(&v188, v169);
    }
    if ( v188 != nullptr )
    {
      qmemcpy(&v173[1], " ,:", 3);
      sub_453DBC(&v168, 2);
      unknown_libname_37(&v188, v168);
    }
    if ( v188 != nullptr )
    {
      qmemcpy(&v173[1], " ,:", 3);
      sub_453DBC(&v167, 2);
      unknown_libname_37(&v188, v167);
    }
    if ( unknown_libname_84(v187, aRejectwhisper) == 0 || unknown_libname_84(v187, aRejectwhisper) == 0 )
    {
      *((_BYTE *)v190 + 636) ^= 1u;
      if ( *((_BYTE *)v190 + 636) != 0 )
        sub_4BB124(v190, aAllowpriv, 1);
      else
        sub_4BB124(v190, &str_RejectPriv[1], 1);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, aAllowwhisper) == 0 || unknown_libname_84(v187, aAllowwhisper) == 0 )
    {
      *((_BYTE *)v190 + 636) = 1;
      sub_4BB124(v190, aAllowpriv, 1);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, aPriv) == 0 )
    {
      if ( v186 != nullptr )
        sub_4D1844(v190, v186);
      if ( v185 != nullptr )
        sub_4D1844(v190, v185);
      if ( v184 != nullptr )
        sub_4D1844(v190, v184);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_BanShout[1]) == 0 || unknown_libname_84(v187, &str_AllowShout[1]) == 0 )
    {
      *((_BYTE *)v190 + 637) ^= 1u;
      if ( *((_BYTE *)v190 + 637) != 0 )
        sub_4BB124(v190, &str__LetShout_[1], 1);
      else
        sub_4BB124(v190, &str__BanShout_[1], 1);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_BanTrade[1]) == 0 )
    {
      *((_BYTE *)v190 + 639) ^= 1u;
      if ( *((_BYTE *)v190 + 639) != 0 )
        sub_4BB124(v190, &str__LetTrade_[1], 1);
      else
        sub_4BB124(v190, &str__BanTrade_[1], 1);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_AllowGuild[1]) == 0 )
    {
      *((_BYTE *)v190 + 177) ^= 1u;
      if ( *((_BYTE *)v190 + 177) != 0 )
        sub_4BB124(v190, aAllowguild_0, 1);
      else
        sub_4BB124(v190, aRejectguild, 1);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_AuthAlly[1]) == 0 )
    {
      if ( (unsigned __int8)sub_4BF4A0(v190) != 0 )
      {
        *(_BYTE *)(v190[200] + 36) ^= 1u;
        if ( *(_BYTE *)(v190[200] + 36) != 0 )
          sub_4BB124(v190, aAllowally, 1);
        else
          sub_4BB124(v190, &str__RejectAlly_[1], 1);
      }
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, aAlliance) == 0 )
    {
      if ( (unsigned __int8)sub_4BF4A0(v190) != 0 )
        sub_4DF3AC(v190);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, aEndalliance) == 0 )
    {
      if ( (unsigned __int8)sub_4BF4A0(v190) != 0 )
        sub_4DF604(v190, v186);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_LeaveGuild[1]) == 0 )
    {
      sub_4D1A44(v190);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_AllowGuildChat[1]) == 0 || unknown_libname_84(v187, &str_RejectGuildChat[1]) == 0 )
    {
      *((_BYTE *)v190 + 638) ^= 1u;
      if ( *((_BYTE *)v190 + 638) != 0 )
        sub_4BB124(v190, aAllowingGuildC, 1);
      else
        sub_4BB124(v190, aRejectingGuild, 1);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_dear[1]) == 0 )
    {
      sub_4C167F((int)v190);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_AttackMode[1]) == 0 )
    {
      if ( *((_BYTE *)v190 + 673) >= 4u )
        *((_BYTE *)v190 + 673) = 0;
      else
        ++*((_BYTE *)v190 + 673);
      switch ( *((_BYTE *)v190 + 673) )
      {
        case 0:
          sub_4BB124(v190, aAttackAll, 1);
          break;
        case 1:
          sub_4BB124(v190, aPeaceful, 1);
          break;
        case 2:
          sub_4BB124(v190, &str__Group_Attack_[1], 1);
          break;
        case 3:
          sub_4BB124(v190, &str__Guild_Attack_[1], 1);
          break;
        case 4:
          sub_4BB124(v190, aRedWhite, 1);
          break;
        default:
          goto LABEL_391;
      }
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_Rest[1]) == 0 )
    {
      if ( *(int *)(v190[167] + 8) > 0 )
      {
        *((_BYTE *)v190 + 672) ^= 1u;
        if ( *((_BYTE *)v190 + 672) != 0 )
          sub_4BB124(v190, aPetRest, 1);
        else
          sub_4BB124(v190, aPetAttack, 1);
      }
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, dword_4D56CC) == 0 )
    {
      sub_4B865C((int)v190, (int)v190, 10416, 0, 0, 0, 0, 0);
      sub_4BB124(v190, dword_4D56DC, 1);
      *((_BYTE *)v190 + 582) = 1;
      goto LABEL_391;
    }
    if ( *(_DWORD *)(*off_4EA080[0] + 32) == v190[200] && v190[200] != 0 && unknown_libname_84(v187, dword_4D56F0) == 0 )
    {
      sub_4D0324(v190, v186);
      goto LABEL_391;
    }
    if ( *((_BYTE *)v190 + 843) != 0 )
    {
      if ( *(_BYTE *)(v190[190] + 55) != 0 )
      {
        sub_4BB124(v190, aCannotUseHere_1, 0);
        goto LABEL_391;
      }
      if ( unknown_libname_84(v187, &str_Move[1]) == 0 )
      {
        if ( GetTickCount() - v190[228] <= 0x2710 )
        {
          TickCount = GetTickCount();
          unknown_libname_91(10 - (TickCount - v190[228]) / 0x3E8, 0);
          System::__linkproc__ LStrCat((int)&v166, &str____Seconds_till[1]);
          sub_4BB124(v190, v166, 0);
        }
        else
        {
          v190[228] = GetTickCount();
          sub_4B8EBC((int)v190, 10330, 0, 0, 0, 0, 0);
          sub_4BD0C4(v185);
        }
        goto LABEL_391;
      }
    }
    if ( (*((_BYTE *)v190 + 858) != 0 || *((_BYTE *)v190 + 621) >= 3u)
      && unknown_libname_84(v187, &str_Searching[1]) == 0 )
    {
      if ( GetTickCount() - v190[229] <= 0x2710 && *((_BYTE *)v190 + 621) < 3u )
      {
        v5 = GetTickCount();
        unknown_libname_91(10 - (v5 - v190[229]) / 0x3E8, 0);
        System::__linkproc__ LStrCat((int)&v162, &str____Seconds_till[1]);
        sub_4BB124(v190, v162, 0);
      }
      else
      {
        v190[229] = GetTickCount();
        v180 = sub_4AE640(*off_4EA490);
        if ( v180 != 0 )
        {
          if ( *(_DWORD *)(v180 + 760) == v190[190] )
          {
            Sysutils::IntToStr(*(_DWORD *)(v180 + 36));
            Sysutils::IntToStr(*(_DWORD *)(v180 + 40));
            System::__linkproc__ LStrCatN(&v165, 6, v4);
            sub_4BB124(v190, v165, 1);
          }
          else
          {
            System::__linkproc__ LStrCat3((int)&v164, v186, &str__Not_on_this_ma[1]);
            sub_4BB124(v190, v164, 1);
          }
        }
        else
        {
          System::__linkproc__ LStrCat3((int)&v163, v186, &str__Is_not_found__[1]);
          sub_4BB124(v190, v163, 1);
        }
      }
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_allowgrecall[1]) == 0 || unknown_libname_84(v187, &str_AllowRecall[1]) == 0 )
    {
      *((_BYTE *)v190 + 270) ^= 1u;
      if ( *((_BYTE *)v190 + 270) != 0 )
        sub_4BB124(v190, &str_Recall_Allowed[1], 1);
      else
        sub_4BB124(v190, &str_Recall_Denied_[1], 1);
    }
    if ( (*((_BYTE *)v190 + 580) != 0 || *((_BYTE *)v190 + 621) >= 4u)
      && unknown_libname_84(v187, &str_grouprecall[1]) == 0 )
    {
      if ( *(_BYTE *)(v190[190] + 51) != 0 )
      {
        sub_4BB124(v190, aCannotUseHere_1, 0);
      }
      else
      {
        v177 = (GetTickCount() - v190[144]) / 0x3E8;
        v190[144] += 1000 * v177;
        *((_WORD *)v190 + 134) = 0;
        if ( *((_WORD *)v190 + 134) != 0 )
        {
          Sysutils::IntToStr(*((unsigned __int16 *)v190 + 134));
          System::__linkproc__ LStrCatN(&v159, 3, v7);
          sub_4BB124(v190, v159, 0);
        }
        else if ( (int *)v190[157] == v190 )
        {
          v6 = (*(int (__fastcall **)(int))(*(_DWORD *)v190[158] + 20))(v190[158]) - 1;
          if ( v6 > 0 )
          {
            v175 = v6;
            v179 = 1;
            do
            {
              if ( *(_BYTE *)((*(int (__fastcall **)(_DWORD, int))(**(_DWORD **)(v190[157] + 632) + 24))(
                                *(_DWORD *)(v190[157] + 632),
                                v179)
                            + 270) != 0 )
              {
                (*(void (__fastcall **)(int, int, int *))(*(_DWORD *)v190[158] + 12))(v190[158], v179, &v161);
                sub_4CE250(v190, v161);
              }
              else
              {
                (*(void (__fastcall **)(int, int, int *, int *, int (__cdecl *)(int, int), struct _EXCEPTION_REGISTRATION_RECORD *))(*(_DWORD *)v190[158] + 12))(
                  v190[158],
                  v179,
                  &v160,
                  v104,
                  v103,
                  ExceptionList);
                System::__linkproc__ LStrCat((int)&v160, aDidntAllow);
                sub_4BB124(v190, v160, 0);
              }
              ++v179;
              --v175;
            }
            while ( v175 != 0 );
          }
          v190[144] = GetTickCount();
          *((_WORD *)v190 + 134) = 180;
        }
      }
    }
    if ( *((_BYTE *)v190 + 621) >= 2u && unknown_libname_43(v189) > 2 && v189[1] == 33 )
    {
      v98 = v8;
      *((_BYTE *)v190 + 21) ^= 0x80u;
      unknown_libname_43(v189);
      System::__linkproc__ LStrCopy(&v188);
      v95 = v188;
      v94 = v9;
      v10 = *((_BYTE *)v190 + 21);
      if ( v10 > 0x80u )
      {
        *((_BYTE *)v190 + 21) = v10 ^ 0x80;
        v13 = (char *)VirtualAlloc(nullptr, 0x2Cu, 0x1000u, 4u);
        v16 = v190;
        v17 = v13 + 20;
        *((_DWORD *)v17 - 1) = -1;
        v14 = (char *)v16 + 21;
        *v17 = *v14 + 6;
        *((_DWORD *)v17 + 1) = 1529424424;
        v15 = (unsigned __int8)*v14;
        do
        {
          v17[v15 + 7] = v14[v15];
          --v15;
        }
        while ( v15 != 0 );
        *(_WORD *)&v17[(unsigned __int8)*v14 + 8] = 8285;
        v12 = v94;
        v11 = v95;
      }
      else
      {
        v11 = v188;
        v12 = &str____[1];
      }
      System::__linkproc__ LStrCat3((int)&v158, v12, v11);
      VirtualFree((LPVOID)(v98 - 24), 0, 0x8000u);
      sub_4AEAF0(*off_4EA490, v158);
      goto LABEL_391;
    }
    if ( *((_BYTE *)v190 + 621) >= 3u )
    {
      if ( unknown_libname_84(v187, aSoulball) == 0 )
      {
        if ( sub_4B7350(*off_4EA22C, v186) != 0 )
        {
          sub_4B8EBC((int)v190, 10330, 0, 0, 0, 0, 0);
          sub_4BCB54(v190, v186, 0);
        }
        goto LABEL_391;
      }
      if ( unknown_libname_84(v187, &str_PositionMove[1]) == 0 )
      {
        sub_4CDA38(v184);
        goto LABEL_391;
      }
      if ( unknown_libname_84(v187, &str_Info[1]) == 0 )
      {
        sub_4CFC98(v190, v186);
        goto LABEL_391;
      }
      if ( unknown_libname_84(v187, &str_MobLevel[1]) == 0 )
      {
        sub_4CFD5C(v190);
        goto LABEL_391;
      }
      if ( unknown_libname_84(v187, &str_MobCount[1]) == 0 )
      {
        v99 = v186;
        v18 = sub_4B7350(*off_4EA22C, v186);
        v19 = sub_4AE20C(*off_4EA490, v18, 0, v99, &str_Mob_Count__[1]);
        Sysutils::IntToStr(v19);
        System::__linkproc__ LStrCatN(&v156, 3, v20);
        sub_4BB124(v190, v156, 1);
        goto LABEL_391;
      }
      if ( unknown_libname_84(v187, &str_Human[1]) == 0 )
      {
        v21 = sub_4AE954(*off_4EA490, v186);
        Sysutils::IntToStr(v21);
        System::__linkproc__ LStrCatN(&v155, 3, v22);
        sub_4BB124(v190, v155, 1);
      }
      if ( unknown_libname_84(v187, &str_Map[1]) == 0 )
      {
        unknown_libname_16(v153, dword_4D5928);
        LOBYTE(v23) = 22;
        System::__linkproc__ PStrNCat(v153, v190 + 1, v23);
        unknown_libname_41();
        sub_4BB124(v190, v154, 0);
        goto LABEL_391;
      }
      if ( unknown_libname_84(v187, aKick) == 0 || unknown_libname_84(v187, aKick) == 0 )
      {
        sub_4CE5BC(v190, v186);
        goto LABEL_391;
      }
      if ( unknown_libname_84(v187, &str_Ting[1]) == 0 )
      {
        sub_4CE624(v190, v186);
        goto LABEL_391;
      }
      if ( unknown_libname_84(v187, &str_SuperTing[1]) == 0 )
      {
        sub_4CE6E4((int)v190, v186, (int)v185);
        goto LABEL_391;
      }
      if ( unknown_libname_84(v187, &str_Shutup[1]) == 0 )
      {
        sub_4D0404((int)v190, v186, (int)v185, 1);
        goto LABEL_391;
      }
      if ( unknown_libname_84(v187, &str_ReleaseShutup[1]) == 0 )
      {
        LOBYTE(v24) = 1;
        sub_4D05D8(v190, v186, v24);
        goto LABEL_391;
      }
      if ( unknown_libname_84(v187, &str_ShutupList[1]) == 0 )
      {
        sub_4D0734(v190);
        goto LABEL_391;
      }
      if ( unknown_libname_84(v187, &str_GameMaster[1]) == 0 )
      {
        *((_BYTE *)v190 + 841) ^= 1u;
        if ( *((_BYTE *)v190 + 841) != 0 )
          sub_4BB124(v190, &str_Game_master_mod[1], 1);
        else
          sub_4BB124(v190, &str_Release__game_m[1], 1);
        goto LABEL_391;
      }
      if ( unknown_libname_84(v187, &str_Observer[1]) == 0 )
      {
        *((_BYTE *)v190 + 842) ^= 1u;
        if ( *((_BYTE *)v190 + 842) != 0 )
          sub_4BB124(v190, &str_Observer_mode[1], 1);
        else
          sub_4BB124(v190, &str_Release__observ[1], 1);
        goto LABEL_391;
      }
      if ( unknown_libname_84(v187, &str_Superman[1]) == 0 )
      {
        *((_BYTE *)v190 + 696) ^= 1u;
        if ( *((_BYTE *)v190 + 696) != 0 )
          sub_4BB124(v190, &str_Invincible_Mode[1], 1);
        else
          sub_4BB124(v190, &str_Normal_mode[1], 1);
        goto LABEL_391;
      }
      if ( unknown_libname_84(v187, &str_Level[1]) == 0 )
      {
        v25 = sub_453B20(v186, 1);
        v27 = Ibutils::Min((Ibutils *)0xC7, v25, v26);
        *((_BYTE *)v190 + 52) = v27;
        sub_4BED6C(v190, 1);
        goto LABEL_391;
      }
      if ( unknown_libname_84(v187, &str_SabukWallGold[1]) == 0 )
      {
        Sysutils::IntToStr(*(_DWORD *)(*off_4EA080[0] + 576));
        Sysutils::IntToStr(*(_DWORD *)(*off_4EA080[0] + 580));
        System::__linkproc__ LStrCatN(&v152, 4, v28);
        sub_4BB124(v190, v152, 1);
        goto LABEL_391;
      }
      if ( unknown_libname_84(v187, &str_Recall[1]) == 0 )
      {
        sub_4CE250(v190, v186);
        goto LABEL_391;
      }
      if ( unknown_libname_84(v187, &str_flag[1]) == 0 )
      {
        v180 = sub_4AE640(*off_4EA490);
        if ( v180 != 0 )
        {
          v178 = sub_453B20(v185, 0);
          if ( sub_4C1490(v180, v178) == 1 )
          {
            unknown_libname_16(v149, v180 + 21);
            LOBYTE(v29) = 18;
            System::__linkproc__ PStrNCat(v149, dword_4D5B00, v29);
            unknown_libname_41();
            Sysutils::IntToStr(v178);
            System::__linkproc__ LStrCatN(&v151, 3, v30);
            sub_4BB124(v190, v151, 1);
          }
          else
          {
            unknown_libname_16(v149, v180 + 21);
            LOBYTE(v31) = 18;
            System::__linkproc__ PStrNCat(v149, dword_4D5B00, v31);
            unknown_libname_41();
            Sysutils::IntToStr(v178);
            System::__linkproc__ LStrCatN(&v148, 3, v32);
            sub_4BB124(v190, v148, 1);
          }
        }
        else
        {
          sub_4BB124(v190, &str__flag_user_name[1], 0);
        }
      }
    }
    if ( *((_BYTE *)v190 + 621) < 4u )
      goto LABEL_391;
    if ( unknown_libname_84(v187, &str_Attack[1]) == 0 )
    {
      v180 = sub_4AE640(*off_4EA490);
      if ( v180 != 0 )
        unknown_libname_25(v190, v180, v33);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_Mob[1]) == 0 )
    {
      sub_4CC7F4(v190, v186, v185);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_RecallMob[1]) == 0 )
    {
      v34 = sub_453B20(v184, 0);
      sub_4CC8C4((int)v190, (int)v186, (int)v185, v34);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_LuckyPoint[1]) == 0 )
    {
      v180 = sub_4AE640(*off_4EA490);
      if ( v180 != 0 )
      {
        Sysutils::IntToStr(*(_DWORD *)(v180 + 264));
        FloatToStr(*(double *)(v180 + 256));
        Sysutils::IntToStr(*(_DWORD *)(v180 + 592));
        System::__linkproc__ LStrCatN(v147, 7, v35);
        sub_4BB124(v190, v147[0], 1);
      }
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_____5[1]) == 0 )
    {
      Sysutils::IntToStr(*off_4EA434[0]);
      Sysutils::IntToStr(*off_4EA664);
      Sysutils::IntToStr(*off_4EA66C[0]);
      Sysutils::IntToStr(*off_4EA6DC);
      Sysutils::IntToStr(*off_4EA454);
      Sysutils::IntToStr(*off_4EA25C[0]);
      Sysutils::IntToStr(*off_4EA588);
      Sysutils::IntToStr(*off_4EA504[0]);
      System::__linkproc__ LStrCatN(&v146, 23, v36);
      sub_4BB124(v190, v146, 1);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_ReloadGuild[1]) == 0 )
    {
      sub_4CE428(v190, v186);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_ReloadLineNotic[1]) == 0 )
    {
      if ( (unsigned __int8)sub_4E40B0(&str_Notice_LineNoti[1]) != 0 )
        sub_4BB124(v190, &str_Notice_LineNoti_0[1], 1);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_ReadAbuseInform[1]) == 0 )
    {
      sub_455F10(&str__Abuse_txt[1]);
      sub_4BB124(v190, &str__________1[1], 1);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_Backstep[1]) == 0 )
    {
      v37 = sub_4B2708(*((unsigned __int8 *)v190 + 44));
      sub_4C2F90(v190, v37, 1);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, aFreepenalty) == 0 )
    {
      sub_4CC528(v190, v186);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_PKpoint[1]) == 0 )
    {
      sub_4CC61C(v190, v186);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_IncPkPoint[1]) == 0 )
    {
      sub_4BF4D4(v190, 100);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_ChangeLuck[1]) == 0 )
    {
      v38 = sub_453BD0(v186);
      *((double *)v190 + 32) = v38;
      sub_4BF580(0.0);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_Hunger[1]) == 0 )
    {
      v39 = sub_453B20(v186, 0);
      v190[62] = v39;
      sub_4B865C((int)v190, (int)v190, 10307, 0, 0, 0, 0, 0);
      goto LABEL_391;
    }
    System::__linkproc__ LStrCmp(v187, &str_hair[1]);
    if ( v2 )
    {
      v40 = sub_453B20(v186, 0);
      *((_BYTE *)v190 + 46) = v40;
      sub_4BA58C(v190);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_Training[1]) == 0 )
    {
      v41 = sub_453B20(v185, 1);
      sub_4CC414(v190, v186, v41);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_DeleteSkill[1]) == 0 )
    {
      Idmessage::TIdMIMEBoundary::Push(v190, v186);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_ChangeJob[1]) == 0 )
    {
      sub_4CC714(v190, v186);
      sub_4BB124(v190, v187, 1);
      sub_4BED6C(v190, 1);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_ChangeGender[1]) == 0 )
    {
      sub_4CC7D0(v190);
      sub_4BB124(v190, v187, 1);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_NameColor[1]) == 0 )
    {
      v42 = sub_453B20(v186, 255);
      *((_BYTE *)v190 + 674) = v42;
      sub_4BF124(v190);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_Mission[1]) == 0 )
      sub_4CCA08((int)v190, (int)v186, (int)v185);
    if ( unknown_libname_84(v187, &str_MobPlace[1]) == 0 )
    {
      sub_4CCBB4((int)v190, (int)v186, (int)v185, (int)v184, (int)v183);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_Transparency[1]) == 0 )
    {
      *((_BYTE *)v190 + 836) ^= 1u;
      if ( *((_BYTE *)v190 + 836) != 0 )
        *((_WORD *)v190 + 56) = 3600;
      else
        *((_WORD *)v190 + 56) = 0;
      v43 = sub_4BA43C(v190);
      v190[23] = v43;
      sub_4BA5B4(v190);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_DeleteItem[1]) == 0 )
    {
      sub_4CDFF8((int)v190, (int)v186, (int)v185);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_Level0[1]) == 0 )
    {
      v44 = sub_453B20(v186, 1);
      v46 = Ibutils::Min((Ibutils *)0xFF, v44, v45);
      *((_BYTE *)v190 + 52) = v46;
      sub_4BED6C(v190, 0);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_QuestBegin[1]) == 0 )
    {
      System::__linkproc__ FillChar((char *)v190 + 297, 100, 0);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_setflag[1]) == 0 )
    {
      v180 = sub_4AE640(*off_4EA490);
      if ( v180 != 0 )
      {
        v178 = sub_453B20(v185, 0);
        v177 = sub_453B20(v184, 0);
        sub_4C1508(v180, v178, v177);
        if ( sub_4C1490(v180, v178) == 1 )
        {
          unknown_libname_16(v149, v180 + 21);
          LOBYTE(v47) = 18;
          System::__linkproc__ PStrNCat(v149, dword_4D5B00, v47);
          unknown_libname_41();
          Sysutils::IntToStr(v178);
          System::__linkproc__ LStrCatN(&v145, 3, v48);
          sub_4BB124(v190, v145, 1);
        }
        else
        {
          unknown_libname_16(v149, v180 + 21);
          LOBYTE(v49) = 18;
          System::__linkproc__ PStrNCat(v149, dword_4D5B00, v49);
          unknown_libname_41();
          Sysutils::IntToStr(v178);
          System::__linkproc__ LStrCatN(&v144, 3, v50);
          sub_4BB124(v190, v144, 1);
        }
      }
      else
      {
        sub_4BB124(v190, &str__setflag_user_n[1], 0);
      }
    }
    if ( unknown_libname_84(v187, &str_Reconnection[1]) == 0 )
      sub_4CE380(v190, v186, v185);
    if ( unknown_libname_84(v187, &str_DisableFilter[1]) == 0 )
    {
      *off_4EA294[0] ^= 1u;
      if ( *off_4EA294[0] != 0 )
        sub_4BB124(v190, &str__Filter_Disable[1], 1);
      else
        sub_4BB124(v190, &str__Filter_Enabled[1], 1);
    }
    System::__linkproc__ LStrCmp(v187, &str_CHGUSERFULL[1]);
    if ( v2 )
    {
      v51 = sub_453B20(v186, 0);
      *off_4EA340 = Ibutils::Max((Ibutils *)0xFA, v51, v52);
      Sysutils::IntToStr(*off_4EA340);
      System::__linkproc__ LStrCat3((int)v141, &str_USERFULL_[1], (void *)v140[1]);
      sub_4BB124(v190, v141[0], 1);
      goto LABEL_391;
    }
    System::__linkproc__ LStrCmp(v187, &str_CHGZENFASTSTEP[1]);
    if ( v2 )
    {
      v53 = sub_453B20(v186, 0);
      *off_4EA3A0 = Ibutils::Max((Ibutils *)0x64, v53, v54);
      Sysutils::IntToStr(*off_4EA3A0);
      System::__linkproc__ LStrCat3((int)v140, &str_ZENFASTSTEP_[1], (void *)v139[1]);
      sub_4BB124(v190, v140[0], 1);
      goto LABEL_391;
    }
    if ( sub_453B20(v187, 0) == 231490500 )
    {
      Sysutils::IntToStr(*off_4EA134);
      System::__linkproc__ LStrCat3((int)v139, &str_current_monthly[1], (void *)v138[1]);
      sub_4BB124(v190, v139[0], 1);
      Sysutils::IntToStr(*off_4EA1B8);
      System::__linkproc__ LStrCat3((int)v138, &str_total_timeusage[1], (void *)v137[1]);
      sub_4BB124(v190, v138[0], 1);
      Sysutils::IntToStr(*off_4EA39C[0]);
      System::__linkproc__ LStrCat3((int)v137, &str_last_mon_totalu[1], (void *)v136[1]);
      sub_4BB124(v190, v137[0], 1);
      Sysutils::IntToStr(*off_4EA50C);
      System::__linkproc__ LStrCat3((int)v136, &str_gross_total_cnt[1], (void *)v135[1]);
      sub_4BB124(v190, v136[0], 1);
      Sysutils::IntToStr(*off_4EA38C);
      System::__linkproc__ LStrCat3((int)v135, &str_gross_reset_cnt[1], (void *)v134[1]);
      sub_4BB124(v190, v135[0], 1);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_ContestPoint[1]) == 0 )
    {
      sub_4CEF08(v190, v186);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_StartContest[1]) == 0 )
    {
      sub_4CF008((int)v190);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_EndContest[1]) == 0 )
    {
      sub_4CF364((int)v190);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_Announcement[1]) == 0 )
      sub_4CF564(v190, (int)v186);
    unknown_libname_84(v187, &str_OXQuizRoom[1]);
    if ( unknown_libname_84(v187, &dword_4D61D8) == 0 )
    {
      sub_4B865C((int)v190, (int)v190, 10416, 0, 0, 0, 0, 0);
      sub_4BB124(v190, dword_4D56DC, 1);
      *((_BYTE *)v190 + 583) = 1;
      goto LABEL_391;
    }
    if ( *((_BYTE *)v190 + 621) < 5u && *off_4EA41C[0] == 0 )
      goto LABEL_356;
    if ( unknown_libname_84(v187, &str_Make[1]) == 0 )
    {
      v55 = sub_453B20(v185, 1);
      sub_4CCE34((int)v190, (int)v186, v55);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_DelGold[1]) == 0 )
    {
      Xdrschema::TXDRImportTranslator::ParseGroup((__int32)v190, (int)v186, (__int32)v185);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_AddGold[1]) == 0 )
    {
      sub_4CD550(v190, (int)v186, (__int32)v185);
      goto LABEL_391;
    }
    System::__linkproc__ LStrCmp(v187, &str_Test_GOLD_Chang[1]);
    if ( v2 )
    {
      unknown_libname_16(v133, dword_4D6228);
      LOBYTE(v56) = 25;
      System::__linkproc__ PStrNCat(v133, (char *)v190 + 21, v56);
      unknown_libname_16(v132, v133);
      LOBYTE(v57) = 26;
      System::__linkproc__ PStrNCat(v132, dword_4D6234, v57);
      unknown_libname_41();
      System::__linkproc__ LStrCat((int)v134, v186);
      sub_4E4274(v134[0]);
      v58 = sub_453B20(v186, 0);
      v60 = Ibutils::Min((Ibutils *)((char *)&loc_4C4B3F + 1), v58, v59);
      v190[12] = v60;
      sub_4C49F4(v190);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, aMakeweap) == 0 )
    {
      v100 = sub_453B20(v184, 0);
      v96 = sub_453B20(v183, 0);
      sub_453B20(v185, 0);
      sub_453B20(v186, 0);
      sub_4CD1C4(v96, v100);
      unknown_libname_16(v129, aRefine);
      LOBYTE(v61) = 23;
      System::__linkproc__ PStrNCat(v129, (char *)v190 + 21, v61);
      unknown_libname_16(v128, v129);
      LOBYTE(v62) = 24;
      System::__linkproc__ PStrNCat(v128, dword_4D6234, v62);
      unknown_libname_41();
      System::__linkproc__ LStrCatN(&v131, 8, v63);
      sub_4E4274(v131);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_ReloadAdmin[1]) == 0 )
    {
      sub_487630(*off_4EA6B8[0]);
      sub_4AF728(0);
      System::__linkproc__ LStrCat3((int)&v127, v187, &str_______________7[1]);
      sub_4BB124(v190, v127, 1);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_ReloadNpc[1]) == 0 )
    {
      sub_4CFFF8((int)v190, (int)v186);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_ReloadMonItems[1]) == 0 )
    {
      sub_4AD248(*off_4EA490);
      sub_4BB124(v190, &str_Monster_drop_fi[1], 1);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_ReloadDiary[1]) == 0 )
    {
      if ( sub_489840(*off_4EA6B8[0]) >= 0 )
        sub_4BB124(v190, &str_QuestDiarys_rel_0[1], 1);
      else
        sub_4BB124(v190, &str_QuestDiarys_rel[1], 0);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_AdjustLevel[1]) == 0 )
    {
      v64 = sub_453B20(v185, 1);
      sub_4CDB4C((__int32)v190, (int)v186, v64);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_AdjustExp[1]) == 0 )
    {
      v65 = sub_453B20(v185, 1);
      unknown_libname_677((__int32)v190, (int)v186, v65);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_AddGuild[1]) == 0 )
    {
      Mxcommon::TBinData::AddBinValues((int)v190, (int)v186, (int)v185);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_DelGuild[1]) == 0 )
    {
      sub_4CEDEC(v190, v186);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_ChangeSabukLord[1]) == 0 )
    {
      sub_4CFE1C((int)v190, v186, 1);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_ForcedWallconqu[1]) == 0 )
    {
      *(_BYTE *)(*off_4EA080[0] + 65) ^= 1u;
      if ( *(_BYTE *)(*off_4EA080[0] + 65) != 0 )
      {
        *(_DWORD *)(*off_4EA080[0] + 68) = GetTickCount();
        sub_491074(*off_4EA080[0]);
      }
      else
      {
        sub_491408(*off_4EA080[0]);
      }
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_AddToItemEvent[1]) == 0 )
    {
      if ( v186 != nullptr )
      {
        v66 = (*(int (__fastcall **)(_DWORD))(**(_DWORD **)off_4EA31C[0] + 20))(*off_4EA31C[0]);
        (*(void (__fastcall **)(_DWORD, void *, int))(**(_DWORD **)off_4EA31C[0] + 56))(
          *off_4EA31C[0],
          v186,
          *off_4EA464[0] + v66);
        System::__linkproc__ LStrCat3((int)&v126, &str_AddToItemEvent_0[1], v186);
        sub_4BB124(v190, v126, 1);
      }
      if ( v185 != nullptr )
      {
        v67 = (*(int (__fastcall **)(_DWORD))(**(_DWORD **)off_4EA31C[0] + 20))(*off_4EA31C[0]);
        (*(void (__fastcall **)(_DWORD, void *, int))(**(_DWORD **)off_4EA31C[0] + 56))(
          *off_4EA31C[0],
          v185,
          *off_4EA464[0] + v67);
        System::__linkproc__ LStrCat3((int)&v125, &str_AddToItemEvent_0[1], v185);
        sub_4BB124(v190, v125, 1);
      }
      if ( v184 != nullptr )
      {
        v68 = (*(int (__fastcall **)(_DWORD))(**(_DWORD **)off_4EA31C[0] + 20))(*off_4EA31C[0]);
        (*(void (__fastcall **)(_DWORD, void *, int))(**(_DWORD **)off_4EA31C[0] + 56))(
          *off_4EA31C[0],
          v184,
          *off_4EA464[0] + v68);
        System::__linkproc__ LStrCat3((int)&v124, &str_AddToItemEvent_0[1], v184);
        sub_4BB124(v190, v124, 1);
      }
      if ( v183 != nullptr )
      {
        v69 = (*(int (__fastcall **)(_DWORD))(**(_DWORD **)off_4EA31C[0] + 20))(*off_4EA31C[0]);
        (*(void (__fastcall **)(_DWORD, void *, int))(**(_DWORD **)off_4EA31C[0] + 56))(
          *off_4EA31C[0],
          v183,
          *off_4EA464[0] + v69);
        System::__linkproc__ LStrCat3((int)&v123, &str_AddToItemEvent_0[1], v183);
        sub_4BB124(v190, v123, 1);
      }
      if ( v182 != nullptr )
      {
        v70 = (*(int (__fastcall **)(_DWORD))(**(_DWORD **)off_4EA31C[0] + 20))(*off_4EA31C[0]);
        (*(void (__fastcall **)(_DWORD, void *, int))(**(_DWORD **)off_4EA31C[0] + 56))(
          *off_4EA31C[0],
          v182,
          *off_4EA464[0] + v70);
        System::__linkproc__ LStrCat3((int)&v122, &str_AddToItemEvent_0[1], v182);
        sub_4BB124(v190, v122, 1);
      }
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_AddToItemEventA[1]) == 0 )
    {
      v71 = sub_453B20(v185, 1);
      v177 = v71;
      if ( v71 > 0 )
      {
        v175 = v71;
        v179 = 1;
        do
        {
          v72 = (*(int (__fastcall **)(_DWORD))(**(_DWORD **)off_4EA31C[0] + 20))(*off_4EA31C[0]);
          (*(void (__fastcall **)(_DWORD, void *, int))(**(_DWORD **)off_4EA31C[0] + 56))(
            *off_4EA31C[0],
            v186,
            *off_4EA464[0] + v72);
          System::__linkproc__ LStrCat3((int)&v121, &str_AddToItemEvent_0[1], v186);
          sub_4BB124(v190, v121, 1);
          ++v179;
          --v175;
        }
        while ( v175 != 0 );
      }
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_ItemEventList[1]) == 0 )
    {
      sub_4BB124(v190, &str__Item_event_lis[1], 1);
      v73 = (*(int (__fastcall **)(_DWORD))(**(_DWORD **)off_4EA31C[0] + 20))(*off_4EA31C[0]) - 1;
      if ( v73 >= 0 )
      {
        v175 = v73 + 1;
        v179 = 0;
        do
        {
          (*(void (__fastcall **)(_DWORD, int, int *))(**(_DWORD **)off_4EA31C[0] + 12))(*off_4EA31C[0], v179, &v119);
          v74 = (*(int (__fastcall **)(_DWORD, int))(**(_DWORD **)off_4EA31C[0] + 24))(*off_4EA31C[0], v179);
          Sysutils::IntToStr(v74);
          System::__linkproc__ LStrCatN(&v120, 3, v75);
          sub_4BB124(v190, v120, 1);
          ++v179;
          --v175;
        }
        while ( v175 != 0 );
      }
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_StartingGiftNo[1]) == 0 )
    {
      *off_4EA464[0] = sub_453B20(v186, 0);
      Sysutils::IntToStr(*off_4EA464[0]);
      System::__linkproc__ LStrCat3((int)&v118, &str_Starting_no__of[1], v117[1]);
      sub_4BB124(v190, v118, 1);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_DeleteAllItemEv[1]) == 0 )
    {
      (*(void (__fastcall **)(_DWORD))(**(_DWORD **)off_4EA31C[0] + 64))(*off_4EA31C[0]);
      sub_4BB124(v190, &str_DeleteAllItemOf[1], 1);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_StartItemEvent[1]) == 0 )
    {
      *(_BYTE *)(*off_4EA490 + 140) ^= 1u;
      if ( *(_BYTE *)(*off_4EA490 + 140) != 0 )
        sub_4BB124(v190, &str_start_of_item_e[1], 1);
      else
        sub_4BB124(v190, &str_end_of_item_eve[1], 1);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_ItemEventTerm[1]) == 0 )
    {
      *(_DWORD *)(*off_4EA490 + 144) = 60000 * sub_453B20(v186, 30);
      v76 = sub_453B20(v186, 30);
      Sysutils::IntToStr(v76);
      System::__linkproc__ LStrCatN(v117, 3, v77);
      sub_4BB124(v190, v117[0], 1);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_AdjustTestLevel[1]) == 0 )
    {
      v78 = sub_453B20(v186, 1);
      v80 = Ibutils::Min((Ibutils *)0xFF, v78, v79);
      *((_BYTE *)v190 + 52) = v80;
      sub_4BED6C(v190, 1);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_OPTraining[1]) == 0 )
    {
      v81 = sub_453B20(v184, 1);
      sub_4CC468(v81);
      goto LABEL_391;
    }
    if ( unknown_libname_84(v187, &str_OPDeleteSkill[1]) == 0 )
      sub_4CE938(v190, v186, v185);
    if ( unknown_libname_84(v187, &str_ChangeWeaponDur[1]) != 0 )
    {
LABEL_356:
      if ( unknown_libname_84(v187, &str_ReloadGuildAll[1]) == 0 )
        sub_4CE530(v190, v186);
    }
    else
    {
      v82 = sub_453B20(v186, 0);
      v84 = Ibutils::Max(nullptr, v82, v83);
      v177 = Ibutils::Min((Ibutils *)0x41, v84, v85);
      if ( *((_WORD *)v190 + 542) != 0 && v177 > 0 )
      {
        *((_WORD *)v190 + 544) = 1000 * v177;
        sub_4B865C(
          (int)v190,
          (int)v190,
          10125,
          1,
          *((unsigned __int16 *)v190 + 543),
          *((unsigned __int16 *)v190 + 544),
          0,
          0);
      }
    }
  }
  else
  {
    if ( GetTickCount() - v190[346] >= 0xBB8 )
    {
      unknown_libname_36(v190 + 344, v189);
      v190[346] = GetTickCount();
      v190[345] = 0;
    }
    else if ( ++v190[345] >= 2 )
    {
      *((_BYTE *)v190 + 1388) = 1;
      v190[348] = GetTickCount() + 60000;
      sub_4BB124(v190, &str__Spamming_Rule_[1], 0);
    }
    if ( GetTickCount() > v190[348] )
      *((_BYTE *)v190 + 1388) = 0;
    v176 = *((_BYTE *)v190 + 1388);
    unknown_libname_41();
    if ( sub_45593C(*off_4EA318[0], v116[1]) >= 0 )
      v176 = 1;
    if ( v176 != 0 )
    {
      sub_4BB124(v190, &str_No_Chat_[1], 0);
      goto LABEL_391;
    }
    if ( *v189 == 47 )
    {
      unknown_libname_43(v189);
      System::__linkproc__ LStrCopy(&v188);
      if ( *((_BYTE *)v190 + 621) >= 3u )
      {
        if ( unknown_libname_84(v188, &str_who_[1]) == 0 )
        {
          v86 = sub_4AE7C0(*off_4EA490);
          Sysutils::IntToStr(v86);
          System::__linkproc__ LStrCat3((int)v116, &str_Users_On__[1], (void *)v115[1]);
          sub_4BB2A0(v190, v116[0]);
          goto LABEL_391;
        }
        if ( *((_BYTE *)v190 + 621) >= 5u && unknown_libname_84(v188, &str_total_[1]) == 0 )
        {
          Sysutils::IntToStr(*off_4EA244);
          System::__linkproc__ LStrCat3((int)v115, aTotalOverServe, v114[1]);
          sub_4BB2A0(v190, v115[0]);
          goto LABEL_391;
        }
      }
      v113 = 32;
      sub_453DBC(v114, 0);
      unknown_libname_37(&v188, v114[0]);
      sub_4D1558((int)v190, v186, v188);
      goto LABEL_391;
    }
    if ( *v189 != 33 )
    {
      Mxcommon::AddToQuerySelect(v190, v189);
      goto LABEL_391;
    }
    if ( unknown_libname_43(v189) < 2 )
      goto LABEL_382;
    if ( v189[1] == 33 )
    {
      unknown_libname_43(v189);
      System::__linkproc__ LStrCopy(&v188);
      unknown_libname_16(v111, (char *)v190 + 21);
      LOBYTE(v87) = 16;
      System::__linkproc__ PStrNCat(v111, &dword_4D667C, v87);
      unknown_libname_41();
      System::__linkproc__ LStrCat((int)&v112, v188);
      sub_4BB1CC(v190, v112);
      goto LABEL_391;
    }
    if ( v189[1] == 126 )
    {
      if ( v190[200] != 0 )
      {
        unknown_libname_43(v189);
        System::__linkproc__ LStrCopy(&v188);
        unknown_libname_16(v109, (char *)v190 + 21);
        LOBYTE(v88) = 15;
        System::__linkproc__ PStrNCat(v109, dword_4D6680, v88);
        unknown_libname_41();
        System::__linkproc__ LStrCat((int)&v110, v188);
        sub_498FF0(v190[200], v110);
        unknown_libname_41();
        System::__linkproc__ LStrCatN(&v108, 5, v89);
        sub_4AF728(v108);
      }
    }
    else
    {
LABEL_382:
      if ( *(_BYTE *)(v190[190] + 48) != 0 )
      {
        sub_4BB124(v190, &str________5[1], 0);
      }
      else if ( GetTickCount() - v190[161] <= 0x2710 )
      {
        v92 = GetTickCount();
        unknown_libname_91(10 - (v92 - v190[161]) / 0x3E8, 0);
        System::__linkproc__ LStrCat((int)&v105, aSecondsTillYou);
        sub_4BB124(v190, v105, 0);
      }
      else if ( *((_BYTE *)v190 + 52) > 0x24u )
      {
        v190[161] = GetTickCount();
        unknown_libname_43(v189);
        System::__linkproc__ LStrCopy(&v188);
        v101 = v190[9];
        v97 = v190[10];
        unknown_libname_16(v106, dword_4D66BC);
        LOBYTE(v90) = 17;
        System::__linkproc__ PStrNCat(v106, (char *)v190 + 21, v90);
        unknown_libname_16(v149, v106);
        LOBYTE(v91) = 18;
        System::__linkproc__ PStrNCat(v149, dword_4D6680, v91);
        unknown_libname_41();
        System::__linkproc__ LStrCat((int)&v107, v188);
        sub_4AEA00(v107, 50, v97, v101);
      }
      else
      {
        sub_4BB124(v190, aYouCannotShout, 0);
      }
    }
  }
LABEL_391:
  __writefsdword(0, (unsigned int)ExceptionList);
  v104 = (int *)&loc_4D5393;
  unknown_libname_34(&v105);
  unknown_libname_35(&v107, 3);
  unknown_libname_34(&v110);
  unknown_libname_34(&v112);
  unknown_libname_35(v114, 20);
  unknown_libname_35(&v130, 2);
  unknown_libname_35(v134, 19);
  unknown_libname_35(v142, 6);
  unknown_libname_35(v143, 37);
  unknown_libname_35(&v150, 5);
  unknown_libname_35(&v154, 5);
  unknown_libname_35(v157, 22);
  unknown_libname_34(v174);
  return unknown_libname_35(v181, 10);
}


// ==================== [0x4D68D0] sub_4D68D0 (size: 14987) ====================
int __usercall sub_4D68D0@<eax>(int a1@<eax>, double a2@<st0>)
{
  int v2; // eax
  char v3; // zf
  int v4; // ecx
  int v5; // ecx
  int v6; // eax
  int v7; // eax
  int v8; // eax
  int v9; // eax
  int *v10; // ecx
  int v11; // eax
  char v12; // al
  char v13; // al
  char v14; // al
  char v15; // al
  char v16; // al
  char v17; // al
  char v18; // al
  char v19; // al
  char v20; // al
  char v21; // al
  char v22; // al
  char v23; // al
  char TickCount; // al
  char v25; // al
  char v26; // al
  int v27; // eax
  int v28; // eax
  int v29; // eax
  int v30; // eax
  int v31; // eax
  int v32; // eax
  int v33; // eax
  int v34; // ecx
  int v35; // eax
  __int16 v36; // ax
  int v37; // eax
  int v38; // eax
  int v39; // eax
  int v40; // eax
  int v41; // edx
  int v42; // eax
  int v43; // edx
  int v44; // eax
  int v45; // eax
  int v46; // eax
  __int16 v47; // ax
  int v48; // eax
  int v49; // eax
  int v50; // eax
  __int16 v51; // ax
  int v52; // eax
  int v53; // ecx
  int v54; // eax
  __int16 v55; // ax
  int v56; // eax
  int v57; // eax
  __int16 v58; // ax
  int v59; // eax
  int v60; // eax
  int v61; // eax
  int v62; // eax
  __int16 v63; // ax
  int v64; // eax
  int v65; // eax
  int v66; // eax
  int v67; // eax
  int v68; // eax
  __int16 v69; // ax
  int v70; // eax
  int v71; // eax
  __int16 v72; // ax
  int v73; // eax
  int v74; // ecx
  int v75; // eax
  int v76; // eax
  __int16 v77; // ax
  int v78; // eax
  int v79; // eax
  int v80; // eax
  int v81; // eax
  int v82; // eax
  int v83; // eax
  int v84; // eax
  int v85; // eax
  int v86; // edx
  int v87; // eax
  int v88; // edx
  int v89; // eax
  int v90; // edx
  int v91; // eax
  int v92; // eax
  int v93; // eax
  int v94; // eax
  int v95; // eax
  int *v96; // eax
  int v97; // eax
  int *v98; // eax
  int v99; // eax
  int *v100; // eax
  int v101; // eax
  int *v102; // eax
  int v103; // eax
  int *v104; // eax
  int v105; // eax
  int *v106; // eax
  int v107; // eax
  int v108; // eax
  __int16 v109; // ax
  __int16 v110; // ax
  __int16 v111; // ax
  __int16 v112; // ax
  __int16 v113; // ax
  int v114; // eax
  int v115; // edx
  int v116; // ecx
  __int16 v118; // [esp-4h] [ebp-318h]
  __int16 v119; // [esp-4h] [ebp-318h]
  __int16 v120; // [esp+0h] [ebp-314h]
  __int16 v121; // [esp+0h] [ebp-314h]
  __int16 v122; // [esp+0h] [ebp-314h]
  __int16 v123; // [esp+0h] [ebp-314h]
  int v124; // [esp+4h] [ebp-310h]
  int v125; // [esp+4h] [ebp-310h]
  int v126; // [esp+4h] [ebp-310h]
  int v127; // [esp+4h] [ebp-310h]
  int v128; // [esp+4h] [ebp-310h]
  int v129; // [esp+4h] [ebp-310h]
  int v130; // [esp+4h] [ebp-310h]
  int v131; // [esp+4h] [ebp-310h]
  __int16 v132; // [esp+4h] [ebp-310h]
  __int16 v133; // [esp+4h] [ebp-310h]
  __int16 v134; // [esp+4h] [ebp-310h]
  __int16 v135; // [esp+4h] [ebp-310h]
  __int16 v136; // [esp+4h] [ebp-310h]
  __int16 v137; // [esp+4h] [ebp-310h]
  __int16 v138; // [esp+4h] [ebp-310h]
  __int16 v139; // [esp+4h] [ebp-310h]
  __int16 v140; // [esp+4h] [ebp-310h]
  __int16 v141; // [esp+4h] [ebp-310h]
  __int16 v142; // [esp+4h] [ebp-310h]
  __int16 v143; // [esp+4h] [ebp-310h]
  struct _EXCEPTION_REGISTRATION_RECORD *v144; // [esp+8h] [ebp-30Ch] BYREF
  void *v145; // [esp+Ch] [ebp-308h]
  int *v146; // [esp+10h] [ebp-304h]
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp+14h] [ebp-300h] BYREF
  void *v148; // [esp+18h] [ebp-2FCh]
  int *v149; // [esp+1Ch] [ebp-2F8h]
  int v150; // [esp+2Ch] [ebp-2E8h] BYREF
  _DWORD v151[3]; // [esp+70h] [ebp-2A4h] BYREF
  _BYTE v152[256]; // [esp+7Ch] [ebp-298h] BYREF
  void *v153; // [esp+17Ch] [ebp-198h] BYREF
  int v154; // [esp+180h] [ebp-194h] BYREF
  int v155; // [esp+184h] [ebp-190h] BYREF
  int v156; // [esp+188h] [ebp-18Ch] BYREF
  int v157; // [esp+190h] [ebp-184h] BYREF
  void *v158; // [esp+194h] [ebp-180h] BYREF
  int v159; // [esp+198h] [ebp-17Ch] BYREF
  int v160; // [esp+19Ch] [ebp-178h] BYREF
  int v161; // [esp+1A0h] [ebp-174h] BYREF
  int v162; // [esp+1A4h] [ebp-170h] BYREF
  int v163; // [esp+1A8h] [ebp-16Ch] BYREF
  int v164[2]; // [esp+1ACh] [ebp-168h] BYREF
  int v165; // [esp+1B4h] [ebp-160h] BYREF
  int v166; // [esp+1B8h] [ebp-15Ch] BYREF
  int v167; // [esp+1C0h] [ebp-154h] BYREF
  void *v168; // [esp+1C4h] [ebp-150h] BYREF
  int v169; // [esp+1C8h] [ebp-14Ch] BYREF
  int v170; // [esp+1CCh] [ebp-148h] BYREF
  int v171[2]; // [esp+1D0h] [ebp-144h] BYREF
  int v172; // [esp+1D8h] [ebp-13Ch] BYREF
  int v173; // [esp+1DCh] [ebp-138h] BYREF
  int v174; // [esp+1E0h] [ebp-134h]
  int v175; // [esp+1E4h] [ebp-130h]
  _BYTE v176[40]; // [esp+1E8h] [ebp-12Ch] BYREF
  int v177; // [esp+210h] [ebp-104h] BYREF
  int v178; // [esp+214h] [ebp-100h] BYREF
  int v179; // [esp+218h] [ebp-FCh] BYREF
  int v180; // [esp+21Ch] [ebp-F8h] BYREF
  void *v181[2]; // [esp+220h] [ebp-F4h] BYREF
  int v182[2]; // [esp+228h] [ebp-ECh] BYREF
  int v183[2]; // [esp+230h] [ebp-E4h] BYREF
  int v184[2]; // [esp+238h] [ebp-DCh] BYREF
  int v185[2]; // [esp+240h] [ebp-D4h] BYREF
  int v186[2]; // [esp+248h] [ebp-CCh] BYREF
  int v187[2]; // [esp+250h] [ebp-C4h] BYREF
  int v188[2]; // [esp+258h] [ebp-BCh] BYREF
  int v189[2]; // [esp+260h] [ebp-B4h] BYREF
  int v190[2]; // [esp+268h] [ebp-ACh] BYREF
  int v191[2]; // [esp+270h] [ebp-A4h] BYREF
  int v192[2]; // [esp+278h] [ebp-9Ch] BYREF
  int v193[2]; // [esp+280h] [ebp-94h] BYREF
  int v194[2]; // [esp+288h] [ebp-8Ch] BYREF
  int v195[2]; // [esp+290h] [ebp-84h] BYREF
  int v196[9]; // [esp+298h] [ebp-7Ch] BYREF
  System::TObject *v197; // [esp+2BCh] [ebp-58h] BYREF
  int v198; // [esp+2C0h] [ebp-54h]
  int v199; // [esp+2C4h] [ebp-50h]
  int v200; // [esp+2C8h] [ebp-4Ch]
  unsigned __int16 v201; // [esp+2CCh] [ebp-48h] BYREF
  unsigned __int16 v202; // [esp+2CEh] [ebp-46h]
  System::TObject *v203; // [esp+2D0h] [ebp-44h]
  int v204; // [esp+2D4h] [ebp-40h]
  int v205; // [esp+2D8h] [ebp-3Ch] BYREF
  int v206; // [esp+2DCh] [ebp-38h]
  int v207; // [esp+2E4h] [ebp-30h]
  char v208; // [esp+2EBh] [ebp-29h]
  int v209; // [esp+2ECh] [ebp-28h]
  int v210[2]; // [esp+2F0h] [ebp-24h] BYREF
  int v211; // [esp+2F8h] [ebp-1Ch] BYREF
  __int16 v212; // [esp+2FCh] [ebp-18h] BYREF
  __int16 v213; // [esp+2FEh] [ebp-16h]
  int v214; // [esp+300h] [ebp-14h] BYREF
  __int16 v215; // [esp+304h] [ebp-10h]
  __int16 v216; // [esp+306h] [ebp-Eh]
  int Package; // [esp+308h] [ebp-Ch] BYREF
  int v218; // [esp+30Ch] [ebp-8h]
  int v219; // [esp+310h] [ebp-4h]
  int savedregs; // [esp+314h] [ebp+0h] BYREF

  v219 = a1;
  unknown_libname_49(&v201, off_45464C);
  v149 = &savedregs;
  v148 = &loc_4DA34D;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  v146 = &savedregs;
  v145 = &loc_4D6D7C;
  v144 = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&v144);
  if ( *(_BYTE *)(v219 + 791) != 0 )
  {
    v6 = sub_4C368C(v219);
    if ( v6 != *(_DWORD *)(v219 + 796) || *(_DWORD *)(v219 + 796) == v219 || *(_DWORD *)(v219 + 796) == 0 )
      sub_4DD394(v219);
  }
  v7 = v219;
  if ( *(_BYTE *)(v219 + 1708) != 0 )
  {
    v124 = v219;
    sub_4BB124(v219, &str_____________0[1], 0);
    v7 = v124;
    *(_BYTE *)(v124 + 1492) = 1;
    *(_BYTE *)(v124 + 1708) = 0;
  }
  if ( *(_DWORD *)(v7 + 1528) == 0 )
  {
    ++*(_DWORD *)(v7 + 1528);
    v125 = v7;
    if ( *(_BYTE *)(v7 + 271) != 0 )
    {
      unknown_libname_41();
      v2 = sub_4AE640(*off_4EA490);
      v218 = v2;
      if ( v2 != 0 )
      {
        System::__linkproc__ SetEq(v219 + 21, v2 + 271, 15);
        if ( v3 )
        {
          unknown_libname_41();
          System::__linkproc__ LStrCatN(&v214, 3, v4);
          sub_4BB124(v219, v214, 2);
          System::__linkproc__ LStrCatN(&v214, 3, v5);
          sub_4BB124(v218, v214, 2);
        }
        else
        {
          *(_BYTE *)(v219 + 271) = 0;
        }
      }
    }
    v7 = v125;
  }
  if ( *(_BYTE *)(v7 + 1327) != 0 && GetTickCount() - *(_DWORD *)(v219 + 1328) > 0x4E20 )
  {
    *(_BYTE *)(v219 + 1327) = 0;
    sub_4BB124(v219, &str_Spirits_lost[1], 0);
    sub_4CAB38(v219, nullptr, (int)&str__UFIR[1]);
  }
  if ( *(_BYTE *)(v219 + 1668) != 0 && GetTickCount() > *(_DWORD *)(v219 + 1672) )
  {
    *(_BYTE *)(v219 + 1668) = 0;
    sub_4BCD1C(v219, *(_DWORD *)(v219 + 1676), *(_DWORD *)(v219 + 1680), *(_DWORD *)(v219 + 1684), 0);
  }
  if ( GetTickCount() - *(_DWORD *)(v219 + 1396) > 0xBB8 )
  {
    *(_DWORD *)(v219 + 1396) = GetTickCount();
    sub_4DA734(v219);
    v209 = sub_4B5DB0(
             *(_DWORD *)(v219 + 760),
             *(_DWORD *)(v219 + 36),
             *(_DWORD *)(v219 + 40),
             v149,
             v148,
             ExceptionList,
             v146,
             v145,
             v144);
    if ( v209 < 2 )
    {
      *(_BYTE *)(v219 + 752) = 0;
    }
    else if ( *(_BYTE *)(v219 + 752) == 0 )
    {
      *(_BYTE *)(v219 + 752) = 1;
      *(_DWORD *)(v219 + 756) = GetTickCount();
    }
    if ( (v209 >= 3 && GetTickCount() - *(_DWORD *)(v219 + 756) > 0xBB8
       || v209 == 2 && GetTickCount() - *(_DWORD *)(v219 + 756) > 0x2710)
      && GetTickCount() - *(_DWORD *)(v219 + 756) < 0x4E20 )
    {
      v8 = System::__linkproc__ RandInt(8);
      sub_4C2F90(v219, v8, 1);
    }
  }
  if ( *(_BYTE *)(*off_4EA080[0] + 65) != 0 )
  {
    v9 = sub_4910F4(*(_DWORD *)(v219 + 40));
    sub_4B84C8(v219, v9);
  }
  if ( GetTickCount() - *(_DWORD *)(v219 + 1400) > 0x3E8 )
  {
    *(_DWORD *)(v219 + 1400) = GetTickCount();
    Sysutils::Time();
    unknown_libname_94((int)v210, a2);
    if ( *off_4EA614 != 0
      && (HIWORD(v210[1]) == *off_4EA6B4[0] || HIWORD(v210[1]) == *off_4EA284)
      && *(int *)((char *)v210 + 2) <= 0x1Eu
      && GetTickCount() - *(_DWORD *)(v219 + 1464) > 0xEA60 )
    {
      sub_4CA994((_DWORD *)v219, *(unsigned __int64 *)&a2);
      *(_DWORD *)(v219 + 1464) = GetTickCount();
      Sysutils::Now();
      *(double *)(v219 + 1456) = a2;
    }
    if ( *(_DWORD *)(v219 + 800) != 0
      && (*(int (__fastcall **)(_DWORD))(**(_DWORD **)(*(_DWORD *)(v219 + 800) + 12) + 20))(*(_DWORD *)(*(_DWORD *)(v219 + 800) + 12)) > 0 )
    {
      v208 = sub_4BEF8C(v219);
      if ( *(_BYTE *)(v219 + 1408) != v208 )
      {
        *(_BYTE *)(v219 + 1408) = v208;
        sub_4BF124(v219);
      }
    }
    if ( *(_BYTE *)(*off_4EA080[0] + 65) != 0 )
    {
      if ( *(_DWORD *)(v219 + 760) == *(_DWORD *)(*off_4EA080[0] + 8)
        && *(_DWORD *)(v219 + 800) != 0
        && (unsigned __int8)sub_490438(*off_4EA080[0], v219) == 0
        && (unsigned __int8)sub_491160(*off_4EA080[0], *(_DWORD *)(v219 + 800)) != 0
        && (unsigned __int8)sub_4911D0(*off_4EA080[0], *(_DWORD *)(v219 + 800)) != 0 )
      {
        sub_491290(*off_4EA080[0], *(_DWORD *)(v219 + 800));
        sub_4AF728(*(_DWORD *)(*(_DWORD *)(v219 + 800) + 4));
        if ( (int)Forms::TScreen::GetCustomFormCount(*(Forms::TScreen **)off_4EA080[0]) <= 1 )
          sub_491408(*off_4EA080[0]);
      }
    }
    else
    {
      sub_4B84C8(v219, 0);
    }
    if ( *(_BYTE *)(v219 + 1028) != 0 )
    {
      *(_BYTE *)(v219 + 1028) = 0;
      sub_4D6870(v219);
      sub_4BF0C4(v219);
    }
  }
  if ( GetTickCount() - *(_DWORD *)(v219 + 1404) >= 0x1F4 )
    *(_DWORD *)(v219 + 1404) = GetTickCount();
  v10 = v146;
  __writefsdword(0, (unsigned int)v144);
  v146 = &savedregs;
  v145 = &loc_4DA1CC;
  v144 = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&v144);
  while ( (unsigned __int8)unknown_libname_25(v219, &v201, v10) != 0 )
  {
    if ( v201 > 0x273Cu )
    {
      if ( v201 > 0x279Eu )
      {
        if ( v201 > 0x2841u )
        {
          if ( v201 > 0x2860u )
          {
            if ( v201 > 0x28ADu )
            {
              switch ( v201 )
              {
                case 0x28AEu:
                  goto LABEL_331;
                case 0x28AFu:
                  if ( v203 != nullptr && v204 != 0 )
                  {
                    v69 = sub_407B24(v204);
                    sub_4546F8(&v173, 0, v69);
                    v70 = v219;
                    *(_DWORD *)(v219 + 1360) = v173;
                    *(_DWORD *)(v70 + 1364) = v174;
                    *(_DWORD *)(v70 + 1368) = v175;
                    Package = sub_4BA25C(v204, v219);
                    v218 = *(_DWORD *)(v204 + 92);
                    sub_4565B0(&Package, 8, &v159, v144);
                    sub_4CAB38(v219, (void *)(v219 + 1360), v159);
                  }
                  break;
                case 0x28B0u:
                  goto LABEL_364;
                case 0x2904u:
                  v197 = v203;
                  v198 = v204;
                  v199 = v205;
                  sub_4546F8(&v173, 0, 0);
                  v114 = v219;
                  *(_DWORD *)(v219 + 1360) = v173;
                  *(_DWORD *)(v114 + 1364) = v174;
                  *(_DWORD *)(v114 + 1368) = v175;
                  sub_4565B0(&v197, 16, &v154, v144);
                  sub_456504(v207, &v153);
                  System::__linkproc__ LStrCat((int)&v154, v153);
                  sub_4CAB38(v219, (void *)(v219 + 1360), v154);
                  break;
                default:
LABEL_386:
                  sub_4C9280(v219, &v201);
                  break;
              }
            }
            else
            {
              switch ( v201 )
              {
                case 0x28ADu:
                  goto LABEL_364;
                case 0x28A0u:
                  sub_4DA9E4(v219);
                  break;
                case 0x28A1u:
                  if ( v203 != nullptr )
                  {
                    sub_4DF84C(v219, v203);
                    System::__linkproc__ FreeMem(v203, 36);
                  }
                  break;
                case 0x28AAu:
LABEL_331:
                  sub_4CAD6C(0, 0, *(_WORD *)(v206 + 424), *(_WORD *)(v206 + 420));
                  break;
                case 0x28ABu:
                  goto LABEL_364;
                default:
                  goto LABEL_386;
              }
            }
          }
          else if ( v201 == 10336 )
          {
LABEL_318:
            v136 = v204;
            v71 = v206;
            LOBYTE(v115) = *(_BYTE *)(v206 + 676);
            LOBYTE(v71) = v202;
            v72 = unknown_libname_77(v71, v115);
            sub_4546F8(&v173, v72, v136);
            v73 = v219;
            *(_DWORD *)(v219 + 1360) = v173;
            *(_DWORD *)(v73 + 1364) = v174;
            *(_DWORD *)(v73 + 1368) = v175;
            Package = sub_4BA25C(v206, v219);
            v218 = *(_DWORD *)(v206 + 92);
            sub_4565B0(&Package, 8, &v211, v144);
            v209 = (unsigned __int8)sub_4BF180(v219, v206);
            if ( v207 != 0 )
            {
              Sysutils::IntToStr(v209);
              System::__linkproc__ LStrCatN(&v157, 3, v74);
              sub_456504(v157, &v158);
              System::__linkproc__ LStrCat((int)&v211, v158);
            }
LABEL_272:
            sub_4CAB38(v219, (void *)(v219 + 1360), v211);
          }
          else
          {
            switch ( v201 )
            {
              case 0x2842u:
                goto LABEL_364;
              case 0x2843u:
                v47 = sub_4C145C(v219);
                sub_4CAD6C(0, 0, 0, v47);
                break;
              case 0x2845u:
                goto LABEL_173;
              case 0x285Au:
              case 0x285Fu:
                goto LABEL_321;
              case 0x285Bu:
                goto LABEL_318;
              case 0x285Cu:
                *(_BYTE *)(v219 + 1495) = 1;
                sub_4CAD6C(v207, 0, 0, 0);
                break;
              case 0x285Du:
                sub_4CAD6C(0, v205, v204, v202);
                break;
              case 0x285Eu:
                v212 = sub_407B24(v204);
                v213 = 0;
                sub_4546F8(&v173, v205, v204);
                v80 = v219;
                *(_DWORD *)(v219 + 1360) = v173;
                *(_DWORD *)(v80 + 1364) = v174;
                *(_DWORD *)(v80 + 1368) = v175;
                sub_4565B0(&v212, 4, &v211, v144);
                sub_4CAB38(v219, (void *)(v219 + 1360), v211);
                break;
              default:
                goto LABEL_386;
            }
          }
        }
        else
        {
          if ( v201 == 10305 )
            goto LABEL_364;
          if ( v201 > 0x27A9u )
          {
            if ( v201 > 0x27DDu )
            {
              if ( v201 == 10301 )
              {
                sub_4DE5A4(v219);
              }
              else if ( v201 == 10302 )
              {
                LOBYTE(v115) = *(_BYTE *)(v219 + 620);
                v40 = v219;
                LOBYTE(v40) = *(_BYTE *)(v219 + 572);
                v132 = unknown_libname_77(v40, v115);
                LOBYTE(v41) = *(_BYTE *)(v219 + 587);
                v42 = v219;
                LOBYTE(v42) = *(_BYTE *)(v219 + 586);
                v120 = unknown_libname_77(v42, v41);
                LOBYTE(v43) = *(_BYTE *)(v219 + 585);
                v44 = v219;
                LOBYTE(v44) = *(_BYTE *)(v219 + 584);
                v118 = unknown_libname_77(v44, v43);
                v45 = v219;
                LOBYTE(v45) = *(_BYTE *)(v219 + 588);
                v46 = unknown_libname_77(v45, 0);
                unknown_libname_78(v46, 0);
                sub_4CAD6C(0, v118, v120, v132);
              }
              else
              {
                if ( (unsigned int)v201 - 10303 >= 2 )
                  goto LABEL_386;
LABEL_364:
                sub_4CAD6C(0, 0, 0, 0);
              }
            }
            else
            {
              switch ( v201 )
              {
                case 0x27DDu:
                  sub_4CAD6C(0, v205, v204, (__int16)v203);
                  break;
                case 0x27D8u:
                  v137 = v204;
                  v76 = v206;
                  LOBYTE(v115) = *(_BYTE *)(v206 + 676);
                  LOBYTE(v76) = v202;
                  v77 = unknown_libname_77(v76, v115);
                  sub_4546F8(&v173, v77, v137);
                  v78 = v219;
                  *(_DWORD *)(v219 + 1360) = v173;
                  *(_DWORD *)(v78 + 1364) = v174;
                  *(_DWORD *)(v78 + 1368) = v175;
                  v197 = (System::TObject *)sub_4BA25C(v206, v219);
                  v198 = *(_DWORD *)(v206 + 92);
                  v199 = 0;
                  sub_4565B0(&v197, 16, &v211, v144);
                  sub_4CAB38(v219, (void *)(v219 + 1360), v211);
                  break;
                case 0x27D9u:
                  sub_4546F8(&v173, 0, v204);
                  v79 = v219;
                  *(_DWORD *)(v219 + 1360) = v173;
                  *(_DWORD *)(v79 + 1364) = v174;
                  *(_DWORD *)(v79 + 1368) = v175;
                  sub_4CAB38(v219, (void *)(v219 + 1360), 0);
                  break;
                case 0x27DAu:
                  if ( v205 != 0 )
                  {
                    LOWORD(v214) = *(_WORD *)(v205 + 36);
                    HIWORD(v214) = *(_WORD *)(v205 + 40);
                    v215 = v205;
                    v216 = sub_407B24(v205);
                    sub_4546F8(&v173, v202, v204);
                    v81 = v219;
                    *(_DWORD *)(v219 + 1360) = v173;
                    *(_DWORD *)(v81 + 1364) = v174;
                    *(_DWORD *)(v81 + 1368) = v175;
                    sub_4565B0(&v214, 8, &v211, v144);
                    sub_4CAB38(v219, (void *)(v219 + 1360), v211);
                  }
                  break;
                case 0x27DCu:
                  if ( v205 != 0 )
                  {
                    v197 = *(System::TObject **)(v205 + 36);
                    v198 = *(_DWORD *)(v205 + 40);
                    v199 = v205;
                    v200 = v202;
                    sub_4546F8(&v173, *(unsigned __int8 *)(v206 + 44), v204);
                    v82 = v219;
                    *(_DWORD *)(v219 + 1360) = v173;
                    *(_DWORD *)(v82 + 1364) = v174;
                    *(_DWORD *)(v82 + 1368) = v175;
                    sub_4565B0(&v197, 16, &v211, v144);
                    sub_4CAB38(v219, (void *)(v219 + 1360), v211);
                  }
                  break;
                default:
                  goto LABEL_386;
              }
            }
          }
          else if ( v201 == 10153 )
          {
            sub_4546F8(&v173, v202, v204);
            v68 = v219;
            *(_DWORD *)(v219 + 1360) = v173;
            *(_DWORD *)(v68 + 1364) = v174;
            *(_DWORD *)(v68 + 1368) = v175;
            Package = sub_4BA25C(v206, v219);
            v218 = *(_DWORD *)(v206 + 92);
            sub_4565B0(&Package, 8, &v160, v144);
            sub_4CAB38(v219, (void *)(v219 + 1360), v160);
          }
          else
          {
            switch ( v201 )
            {
              case 0x279Fu:
LABEL_367:
                sub_4CAD6C(0, 0, v205, v204);
                break;
              case 0x27A0u:
              case 0x27A6u:
              case 0x27A7u:
                goto LABEL_364;
              case 0x27A2u:
              case 0x27A8u:
LABEL_362:
                sub_4CAD6C(0, 0, 0, v204);
                break;
              case 0x27A3u:
                sub_4DC120(v219, v203);
                break;
              case 0x27A4u:
                if ( v203 != nullptr )
                {
                  sub_4D0DAC(v219, v203);
                  System::TObject::Free(v203);
                }
                break;
              case 0x27A5u:
LABEL_361:
                sub_4CAD6C(v207, 0, 0, v204);
                break;
              default:
                goto LABEL_386;
            }
          }
        }
      }
      else
      {
        if ( v201 == 10142 )
          goto LABEL_364;
        switch ( v201 )
        {
          case 0x273Du:
            sub_4546F8(&v173, 0, 0);
            v84 = v219;
            *(_DWORD *)(v219 + 1360) = v173;
            *(_DWORD *)(v84 + 1364) = v174;
            *(_DWORD *)(v84 + 1368) = v175;
            sub_4CAB38(v219, (void *)(v219 + 1360), 0);
            sub_4546F8(&v173, 0, *(_WORD *)(v219 + 48));
            v85 = v219;
            *(_DWORD *)(v219 + 1360) = v173;
            *(_DWORD *)(v85 + 1364) = v174;
            *(_DWORD *)(v85 + 1368) = v175;
            sub_4565B0(v219 + 408, 40, &v155, v144);
            sub_4CAB38(v219, (void *)(v219 + 1360), v155);
            LOBYTE(v86) = *(_BYTE *)(v219 + 620);
            v87 = v219;
            LOBYTE(v87) = *(_BYTE *)(v219 + 572);
            v138 = unknown_libname_77(v87, v86);
            LOBYTE(v88) = *(_BYTE *)(v219 + 587);
            v89 = v219;
            LOBYTE(v89) = *(_BYTE *)(v219 + 586);
            v121 = unknown_libname_77(v89, v88);
            LOBYTE(v90) = *(_BYTE *)(v219 + 585);
            v91 = v219;
            LOBYTE(v91) = *(_BYTE *)(v219 + 584);
            v119 = unknown_libname_77(v91, v90);
            v92 = v219;
            LOBYTE(v92) = *(_BYTE *)(v219 + 588);
            v93 = unknown_libname_77(v92, 0);
            unknown_libname_78(v93, 0);
            sub_4CAD6C(0, v119, v121, v138);
            break;
          case 0x273Eu:
            v209 = (unsigned __int8)sub_4BF180(v219, v206);
            sub_4CAD6C(0, 0, 0, v209);
            break;
          case 0x2742u:
            if ( *(_BYTE *)(*(_DWORD *)(v219 + 760) + 36) != 0 )
            {
              v209 = 1;
            }
            else if ( *(_DWORD *)(v219 + 1700) == 1 )
            {
              v209 = 0;
            }
            else if ( *(_DWORD *)(v219 + 1700) == 3 )
            {
              v209 = 1;
            }
            else
            {
              v209 = 2;
            }
            if ( *(_BYTE *)(*(_DWORD *)(v219 + 760) + 37) != 0 )
              v209 = 0;
            sub_4546F8(&v173, v209, *(_WORD *)(v219 + 40));
            v35 = v219;
            *(_DWORD *)(v219 + 1360) = v173;
            *(_DWORD *)(v35 + 1364) = v174;
            *(_DWORD *)(v35 + 1368) = v175;
            unknown_libname_41();
            sub_456504(v171[1], &v172);
            sub_4CAB38(v219, (void *)(v219 + 1360), v172);
            sub_4D677C(v219);
            sub_4DA8E8(*(_DWORD *)(v219 + 40));
            sub_4D6870(v219);
            sub_4CAD6C(*(_DWORD *)(*(_DWORD *)(v219 + 760) + 8), 0, 0, 0);
            v36 = sub_407B24(*off_4EA160);
            sub_4546F8(&v173, 0, v36);
            v37 = v219;
            *(_DWORD *)(v219 + 1360) = v173;
            *(_DWORD *)(v37 + 1364) = v174;
            *(_DWORD *)(v37 + 1368) = v175;
            v212 = *(_WORD *)off_4EA694;
            v213 = sub_407B24(*off_4EA694);
            sub_4565B0(&v212, 4, v171, v144);
            sub_4CAB38(v219, (void *)(v219 + 1360), v171[0]);
            break;
          case 0x2743u:
            sub_4546F8(&v173, 0, 0);
            v39 = v219;
            *(_DWORD *)(v219 + 1360) = v173;
            *(_DWORD *)(v39 + 1364) = v174;
            *(_DWORD *)(v39 + 1368) = v175;
            sub_4565B0(v219 + 408, 40, &v170, v144);
            sub_4CAB38(v219, (void *)(v219 + 1360), v170);
            break;
          case 0x2744u:
            sub_4546F8(&v173, *(_WORD *)(v206 + 424), *(_WORD *)(v206 + 422));
            v48 = v219;
            *(_DWORD *)(v219 + 1360) = v173;
            *(_DWORD *)(v48 + 1364) = v174;
            *(_DWORD *)(v48 + 1368) = v175;
            sub_4CAB38(v219, (void *)(v219 + 1360), 0);
            break;
          case 0x2745u:
            if ( *(_BYTE *)(*(_DWORD *)(v219 + 760) + 36) != 0 )
            {
              v209 = 1;
            }
            else if ( *(_DWORD *)(v219 + 1700) == 1 )
            {
              v209 = 0;
            }
            else if ( *(_DWORD *)(v219 + 1700) == 3 )
            {
              v209 = 1;
            }
            else
            {
              v209 = 2;
            }
            if ( *(_BYTE *)(*(_DWORD *)(v219 + 760) + 37) != 0 )
              v209 = 0;
            sub_4546F8(&v173, 0, v209);
            v38 = v219;
            *(_DWORD *)(v219 + 1360) = v173;
            *(_DWORD *)(v38 + 1364) = v174;
            *(_DWORD *)(v38 + 1368) = v175;
            sub_4CAB38(v219, (void *)(v219 + 1360), 0);
            break;
          case 0x2774u:
          case 0x2776u:
          case 0x2777u:
          case 0x2778u:
          case 0x2779u:
          case 0x278Eu:
LABEL_339:
            if ( v201 > 0x2776u )
            {
              switch ( v201 )
              {
                case 0x2777u:
                  v100 = &v173;
                  LOBYTE(v115) = -1;
                  LOBYTE(v100) = -37;
                  unknown_libname_77(v100, v115);
                  sub_4546F8(&v173, 1, 0);
                  v101 = v219;
                  *(_DWORD *)(v219 + 1360) = v173;
                  *(_DWORD *)(v101 + 1364) = v174;
                  *(_DWORD *)(v101 + 1368) = v175;
                  break;
                case 0x2778u:
                  v106 = &v173;
                  LOBYTE(v115) = -1;
                  LOBYTE(v106) = -44;
                  unknown_libname_77(v106, v115);
                  sub_4546F8(&v173, 1, 0);
                  v107 = v219;
                  *(_DWORD *)(v219 + 1360) = v173;
                  *(_DWORD *)(v107 + 1364) = v174;
                  *(_DWORD *)(v107 + 1368) = v175;
                  break;
                case 0x2779u:
                  v102 = &v173;
                  LOBYTE(v115) = -4;
                  LOBYTE(v102) = -1;
                  unknown_libname_77(v102, v115);
                  sub_4546F8(&v173, 1, 0);
                  v103 = v219;
                  *(_DWORD *)(v219 + 1360) = v173;
                  *(_DWORD *)(v103 + 1364) = v174;
                  *(_DWORD *)(v103 + 1368) = v175;
                  break;
                case 0x278Eu:
                  sub_4546F8(&v173, 1, 0);
                  v108 = v219;
                  *(_DWORD *)(v219 + 1360) = v173;
                  *(_DWORD *)(v108 + 1364) = v174;
                  *(_DWORD *)(v108 + 1368) = v175;
                  break;
                default:
                  break;
              }
            }
            else
            {
              switch ( v201 )
              {
                case 0x2776u:
                  v104 = &v173;
                  LOBYTE(v115) = -1;
                  LOBYTE(v104) = -60;
                  unknown_libname_77(v104, v115);
                  sub_4546F8(&v173, 1, 0);
                  v105 = v219;
                  *(_DWORD *)(v219 + 1360) = v173;
                  *(_DWORD *)(v105 + 1364) = v174;
                  *(_DWORD *)(v105 + 1368) = v175;
                  break;
                case 0x272Eu:
                  LOBYTE(v115) = -1;
                  unknown_libname_77(0, v115);
                  sub_4546F8(&v173, 1, 0);
                  v94 = v219;
                  *(_DWORD *)(v219 + 1360) = v173;
                  *(_DWORD *)(v94 + 1364) = v174;
                  *(_DWORD *)(v94 + 1368) = v175;
                  break;
                case 0x272Fu:
                  v96 = &v173;
                  LOBYTE(v115) = -1;
                  LOBYTE(v96) = -4;
                  unknown_libname_77(v96, v115);
                  sub_4546F8(&v173, 1, 0);
                  v97 = v219;
                  *(_DWORD *)(v219 + 1360) = v173;
                  *(_DWORD *)(v97 + 1364) = v174;
                  *(_DWORD *)(v97 + 1368) = v175;
                  break;
                case 0x2730u:
                  LOBYTE(v115) = -105;
                  unknown_libname_77(0, v115);
                  sub_4546F8(&v173, 1, 0);
                  v95 = v219;
                  *(_DWORD *)(v219 + 1360) = v173;
                  *(_DWORD *)(v95 + 1364) = v174;
                  *(_DWORD *)(v95 + 1368) = v175;
                  break;
                case 0x2774u:
                  v98 = &v173;
                  LOBYTE(v115) = 56;
                  LOBYTE(v98) = -1;
                  unknown_libname_77(v98, v115);
                  sub_4546F8(&v173, 1, 0);
                  v99 = v219;
                  *(_DWORD *)(v219 + 1360) = v173;
                  *(_DWORD *)(v99 + 1364) = v174;
                  *(_DWORD *)(v99 + 1368) = v175;
                  break;
                default:
                  break;
              }
            }
            sub_456504(v207, &v211);
            sub_4CAB38(v219, (void *)(v219 + 1360), v211);
            break;
          case 0x277Eu:
            sub_4CAD6C(v207, v202, v205, v204);
            break;
          case 0x277Fu:
            goto LABEL_367;
          case 0x2780u:
          case 0x2781u:
            sub_4CAD6C(0, 0, v204, (__int16)v203);
            break;
          case 0x2782u:
            sub_4D112C(v219);
            break;
          case 0x2783u:
            sub_4CAD6C(0, 0, *(unsigned __int8 *)(v219 + 446), *(unsigned __int8 *)(v219 + 444));
            break;
          case 0x2784u:
            v140 = (__int16)v203;
            v110 = sub_407B24(v203);
            sub_4CAD6C(0, 0, v110, v140);
            break;
          case 0x2785u:
          case 0x2789u:
          case 0x278Fu:
          case 0x2792u:
          case 0x2793u:
          case 0x2794u:
          case 0x2796u:
          case 0x2798u:
          case 0x279Au:
          case 0x279Cu:
            goto LABEL_364;
          case 0x2786u:
            if ( *(_BYTE *)(*(_DWORD *)(v219 + 760) + 36) != 0 )
            {
              v209 = 1;
            }
            else if ( *(_DWORD *)(v219 + 1700) == 1 )
            {
              v209 = 0;
            }
            else if ( *(_DWORD *)(v219 + 1700) == 3 )
            {
              v209 = 1;
            }
            else
            {
              v209 = 2;
            }
            if ( *(_BYTE *)(*(_DWORD *)(v219 + 760) + 37) != 0 )
              v209 = 0;
            sub_4CAD6C(v207, v209, *(_DWORD *)(v219 + 40), *(_DWORD *)(v219 + 36));
            sub_4D6870(v219);
            sub_4CAD6C(*(_DWORD *)(*(_DWORD *)(v219 + 760) + 8), 0, 0, 0);
            break;
          case 0x2787u:
            if ( v206 != v219 )
            {
LABEL_292:
              sub_4546F8(&v173, v202, v204);
              v61 = v219;
              *(_DWORD *)(v219 + 1360) = v173;
              *(_DWORD *)(v61 + 1364) = v174;
              *(_DWORD *)(v61 + 1368) = v175;
              sub_4CAB38(v219, (void *)(v219 + 1360), 0);
            }
            break;
          case 0x2788u:
            v63 = sub_407B24(v204);
            sub_4546F8(&v173, (__int16)v203, v63);
            v64 = v219;
            *(_DWORD *)(v219 + 1360) = v173;
            *(_DWORD *)(v64 + 1364) = v174;
            *(_DWORD *)(v64 + 1368) = v175;
            sub_4565B0(&v205, 4, v164, v144);
            sub_4CAB38(v219, (void *)(v219 + 1360), v164[0]);
            break;
          case 0x278Au:
            sub_4D1418(v219);
            break;
          case 0x278Bu:
            v142 = v204;
            v122 = v205;
            v112 = sub_407B24(v205);
            sub_4CAD6C(0, v112, v122, v142);
            break;
          case 0x278Cu:
            continue;
          case 0x278Du:
            v143 = v202;
            v123 = v204;
            v113 = sub_407B24(v204);
            sub_4CAD6C(0, v113, v123, v143);
            break;
          case 0x2790u:
            goto LABEL_361;
          case 0x2791u:
          case 0x279Du:
            goto LABEL_362;
          case 0x2795u:
            v139 = v204;
            v109 = sub_407B24(v204);
            sub_4CAD6C(0, 0, v109, v139);
            break;
          case 0x2797u:
            sub_4CAD6C(v207, 0, v205, v204);
            break;
          case 0x2799u:
            sub_4CAD6C(0, 0, 0, *(_DWORD *)(v206 + 676));
            break;
          case 0x279Bu:
            v141 = (__int16)v203;
            v111 = sub_407B24(v203);
            sub_4CAD6C(0, v202, v111, v141);
            break;
          default:
            goto LABEL_386;
        }
      }
    }
    else if ( v201 == 10044 )
    {
LABEL_321:
      sub_4546F8(&v173, 0, 0);
      v75 = v219;
      *(_DWORD *)(v219 + 1360) = v173;
      *(_DWORD *)(v75 + 1364) = v174;
      *(_DWORD *)(v75 + 1368) = v175;
      sub_4CAB38(v219, (void *)(v219 + 1360), 0);
    }
    else if ( v201 > 0x40Eu )
    {
      if ( v201 > 0x2712u )
      {
        switch ( v201 )
        {
          case 0x2713u:
            if ( v206 != v219 )
            {
              v135 = v204;
              v57 = v206;
              LOBYTE(v115) = *(_BYTE *)(v206 + 676);
              LOBYTE(v57) = v202;
              v58 = unknown_libname_77(v57, v115);
              sub_4546F8(&v173, v58, v135);
              v59 = v219;
              *(_DWORD *)(v219 + 1360) = v173;
              *(_DWORD *)(v59 + 1364) = v174;
              *(_DWORD *)(v59 + 1368) = v175;
              Package = sub_4BA25C(v206, v219);
              v218 = *(_DWORD *)(v206 + 92);
              sub_4565B0(&Package, 8, &v165, v144);
              sub_4CAB38(v219, (void *)(v219 + 1360), v165);
            }
            break;
          case 0x2714u:
            if ( v206 != v219 )
              goto LABEL_292;
            break;
          case 0x2715u:
            if ( v206 != v219 )
            {
              sub_4546F8(&v173, v202, v204);
              v60 = v219;
              *(_DWORD *)(v219 + 1360) = v173;
              *(_DWORD *)(v60 + 1364) = v174;
              *(_DWORD *)(v60 + 1368) = v175;
              sub_4CAB38(v219, (void *)(v219 + 1360), v207);
            }
            break;
          case 0x2716u:
            if ( v206 != v219 )
              goto LABEL_292;
            break;
          case 0x2717u:
            if ( v206 != v219 )
            {
              sub_4546F8(&v173, v202, v204);
              v62 = v219;
              *(_DWORD *)(v219 + 1360) = v173;
              *(_DWORD *)(v62 + 1364) = v174;
              *(_DWORD *)(v62 + 1368) = v175;
              Sysutils::IntToStr(v205);
              sub_4CAB38(v219, (void *)(v219 + 1360), v164[1]);
            }
            break;
          case 0x2718u:
            if ( v206 != v219 )
              goto LABEL_292;
            break;
          case 0x271Au:
            sub_4546F8(&v173, *(unsigned __int8 *)(v219 + 44), *(_WORD *)(v219 + 40));
            v49 = v219;
            *(_DWORD *)(v219 + 1360) = v173;
            *(_DWORD *)(v49 + 1364) = v174;
            *(_DWORD *)(v49 + 1368) = v175;
            Package = Sysutils::LoadPackage(v219);
            v218 = *(_DWORD *)(v219 + 92);
            sub_4565B0(&Package, 8, &v169, v144);
            sub_4CAB38(v219, (void *)(v219 + 1360), v169);
            break;
          case 0x271Bu:
            if ( v206 != v219 )
              goto LABEL_292;
            break;
          case 0x271Cu:
            if ( v206 != v219 )
              goto LABEL_292;
            break;
          case 0x271Du:
          case 0x271Fu:
          case 0x2720u:
            goto LABEL_266;
          case 0x271Eu:
            if ( v206 != v219 )
              goto LABEL_292;
            break;
          case 0x2724u:
          case 0x272Bu:
            if ( v202 != 0 )
            {
              if ( v206 == v219 )
              {
                if ( v205 != 0 )
                {
                  if ( *(_BYTE *)(v205 + 570) == 0 )
                    sub_4BC7BC(v219, v205);
                  sub_4BC74C(v219, v205);
                }
                if ( sub_4BF0A0(v219) >= 2 )
                  *(_DWORD *)(v219 + 1524) = GetTickCount();
                if ( (unsigned __int8)sub_490400(*off_4EA080[0], *(_DWORD *)(v219 + 800)) != 0 && v205 != 0 )
                {
                  *(_BYTE *)(v205 + 688) = 1;
                  *(_DWORD *)(v205 + 692) = GetTickCount();
                }
                *(_DWORD *)(v219 + 880) = 0;
                *(_DWORD *)(v219 + 884) = 0;
                --*(_DWORD *)(v219 + 596);
                --*(_DWORD *)(v219 + 604);
              }
              if ( v206 != 0 )
              {
                sub_4546F8(&v173, v202, *(_WORD *)(v206 + 424));
                v65 = v219;
                *(_DWORD *)(v219 + 1360) = v173;
                *(_DWORD *)(v65 + 1364) = v174;
                *(_DWORD *)(v65 + 1368) = v175;
                v197 = (System::TObject *)sub_4BA25C(v206, v219);
                v198 = *(_DWORD *)(v206 + 92);
                v199 = v205;
              }
              v200 = v201 == 10027;
              sub_4565B0(&v197, 16, &v163, v144);
              sub_4CAB38(v219, (void *)(v219 + 1360), v163);
            }
            break;
          case 0x2725u:
            sub_4546F8(&v173, v202, v204);
            v66 = v219;
            *(_DWORD *)(v219 + 1360) = v173;
            *(_DWORD *)(v66 + 1364) = v174;
            *(_DWORD *)(v66 + 1368) = v175;
            Package = sub_4BA25C(v206, v219);
            v218 = *(_DWORD *)(v206 + 92);
            sub_4565B0(&Package, 8, &v162, v144);
            sub_4CAB38(v219, (void *)(v219 + 1360), v162);
            break;
          case 0x2726u:
            goto LABEL_321;
          case 0x2728u:
            sub_4546F8(&v173, v202, v204);
            v67 = v219;
            *(_DWORD *)(v219 + 1360) = v173;
            *(_DWORD *)(v67 + 1364) = v174;
            *(_DWORD *)(v67 + 1368) = v175;
            Package = sub_4BA25C(v206, v219);
            v218 = *(_DWORD *)(v206 + 92);
            sub_4565B0(&Package, 8, &v161, v144);
            sub_4CAB38(v219, (void *)(v219 + 1360), v161);
            break;
          case 0x272Eu:
          case 0x272Fu:
          case 0x2730u:
            goto LABEL_339;
          case 0x273Bu:
            sub_4BF180(v219, v206);
            sub_4546F8(&v173, 0, 0);
            v83 = v219;
            *(_DWORD *)(v219 + 1360) = v173;
            *(_DWORD *)(v83 + 1364) = v174;
            *(_DWORD *)(v83 + 1368) = v175;
            sub_456504(v207, &v156);
            sub_4CAB38(v219, (void *)(v219 + 1360), v156);
            break;
          default:
            goto LABEL_386;
        }
      }
      else if ( v201 == 10002 )
      {
        if ( v206 != v219 )
        {
          v134 = v204;
          v54 = v206;
          LOBYTE(v115) = *(_BYTE *)(v206 + 676);
          LOBYTE(v54) = v202;
          v55 = unknown_libname_77(v54, v115);
          sub_4546F8(&v173, v55, v134);
          v56 = v219;
          *(_DWORD *)(v219 + 1360) = v173;
          *(_DWORD *)(v56 + 1364) = v174;
          *(_DWORD *)(v56 + 1368) = v175;
          Package = sub_4BA25C(v206, v219);
          v218 = *(_DWORD *)(v206 + 92);
          sub_4565B0(&Package, 8, &v166, v144);
          sub_4CAB38(v219, (void *)(v219 + 1360), v166);
        }
      }
      else if ( v201 > 0xBC2u )
      {
        if ( v201 > 0xBC9u )
        {
          if ( (unsigned int)v201 - 3018 < 2 || (unsigned int)v201 - 3024 < 2 )
            goto LABEL_155;
          if ( v201 == 3030 )
          {
            if ( v207 != 0 )
              unknown_libname_25(v219, v207, v10);
          }
          else
          {
            if ( v201 != 10001 )
              goto LABEL_386;
LABEL_266:
            if ( v206 != v219 || v201 == 10013 || v201 == 10015 || v201 == 10016 )
            {
              v133 = v204;
              v50 = v206;
              LOBYTE(v115) = *(_BYTE *)(v206 + 676);
              LOBYTE(v50) = v202;
              v51 = unknown_libname_77(v50, v115);
              sub_4546F8(&v173, v51, v133);
              v52 = v219;
              *(_DWORD *)(v219 + 1360) = v173;
              *(_DWORD *)(v52 + 1364) = v174;
              *(_DWORD *)(v52 + 1368) = v175;
              Package = sub_4BA25C(v206, v219);
              v218 = *(_DWORD *)(v206 + 92);
              sub_4565B0(&Package, 8, &v211, v144);
              v209 = (unsigned __int8)sub_4BF180(v219, v206);
              if ( v207 != 0 )
              {
                Sysutils::IntToStr(v209);
                System::__linkproc__ LStrCatN(&v167, 3, v53);
                sub_456504(v167, &v168);
                System::__linkproc__ LStrCat((int)&v211, v168);
              }
              goto LABEL_272;
            }
          }
        }
        else
        {
          switch ( v201 )
          {
            case 0xBC9u:
              if ( *(_BYTE *)(v219 + 772) != 0 )
              {
                TickCount = GetTickCount();
                unknown_libname_91(TickCount, 0);
                System::__linkproc__ LStrCat3((int)v184, &str[1], (void *)v183[1]);
                sub_4CAB38(v219, nullptr, v184[0]);
              }
              else if ( sub_4CBCEC(v219, v202, (int)v203, *(unsigned __int64 *)&a2, v205, v204) != 0 )
              {
                v22 = GetTickCount();
                unknown_libname_91(v22, 0);
                System::__linkproc__ LStrCat3((int)v186, &str__GOOD_[1], (void *)v185[1]);
                sub_4CAB38(v219, nullptr, v186[0]);
                ++*(_DWORD *)(v219 + 1528);
              }
              else
              {
                v23 = GetTickCount();
                unknown_libname_91(v23, 0);
                System::__linkproc__ LStrCat3((int)v185, &str[1], (void *)v184[1]);
                sub_4CAB38(v219, nullptr, v185[0]);
              }
              break;
            case 0xBC3u:
              if ( *(_BYTE *)(v219 + 772) == 0 && sub_4CAF08(v219, (int)v203, v204, *(unsigned __int64 *)&a2) != 0 )
              {
                v15 = GetTickCount();
                unknown_libname_91(v15, 0);
                System::__linkproc__ LStrCat3((int)v193, &str__GOOD_[1], (void *)v192[1]);
                sub_4CAB38(v219, nullptr, v193[0]);
                ++*(_DWORD *)(v219 + 1528);
              }
              else
              {
                v14 = GetTickCount();
                unknown_libname_91(v14, 0);
                System::__linkproc__ LStrCat3((int)v194, &str[1], (void *)v193[1]);
                sub_4CAB38(v219, nullptr, v194[0]);
              }
              break;
            case 0xBC4u:
              if ( *(_BYTE *)(v219 + 772) != 0 )
              {
                v26 = GetTickCount();
                unknown_libname_91(v26, 0);
                System::__linkproc__ LStrCat3((int)v182, &str[1], v181[1]);
                sub_4CAB38(v219, nullptr, v182[0]);
              }
              else
              {
                sub_4CC248(v202);
                v25 = GetTickCount();
                unknown_libname_91(v25, 0);
                System::__linkproc__ LStrCat3((int)v183, &str__GOOD_[1], (void *)v182[1]);
                sub_4CAB38(v219, nullptr, v183[0]);
              }
              break;
            case 0xBC5u:
              if ( *(_BYTE *)(v219 + 772) == 0
                && sub_4CB11C((int *)v219, (int)v203, v204, *(unsigned __int64 *)&a2) != 0 )
              {
                v17 = GetTickCount();
                unknown_libname_91(v17, 0);
                System::__linkproc__ LStrCat3((int)v191, &str__GOOD_[1], (void *)v190[1]);
                sub_4CAB38(v219, nullptr, v191[0]);
                ++*(_DWORD *)(v219 + 1528);
              }
              else
              {
                v16 = GetTickCount();
                unknown_libname_91(v16, 0);
                System::__linkproc__ LStrCat3((int)v192, &str[1], (void *)v191[1]);
                sub_4CAB38(v219, nullptr, v192[0]);
              }
              break;
            default:
              if ( (unsigned int)v201 - 3014 >= 3 )
                goto LABEL_386;
LABEL_155:
              if ( *(_BYTE *)(v219 + 772) != 0 )
              {
                v20 = GetTickCount();
                unknown_libname_91(v20, 0);
                System::__linkproc__ LStrCat3((int)v188, &str[1], (void *)v187[1]);
                sub_4CAB38(v219, nullptr, v188[0]);
              }
              else if ( sub_4CB7F8(v219, v201, (int)v203, *(unsigned __int64 *)&a2, v202, v204) != 0 )
              {
                v18 = GetTickCount();
                unknown_libname_91(v18, 0);
                System::__linkproc__ LStrCat3((int)v190, &str__GOOD_[1], (void *)v189[1]);
                sub_4CAB38(v219, nullptr, v190[0]);
                ++*(_DWORD *)(v219 + 1528);
              }
              else
              {
                v19 = GetTickCount();
                unknown_libname_91(v19, 0);
                System::__linkproc__ LStrCat3((int)v189, &str[1], (void *)v188[1]);
                sub_4CAB38(v219, nullptr, v189[0]);
              }
              break;
          }
        }
      }
      else if ( v201 == 3010 )
      {
        if ( *(_BYTE *)(v219 + 772) == 0 && (unsigned __int8)sub_4CAEB8(v202) != 0 )
        {
          v13 = GetTickCount();
          unknown_libname_91(v13, 0);
          System::__linkproc__ LStrCat3((int)v195, &str__GOOD_[1], (void *)v194[1]);
          sub_4CAB38(v219, nullptr, v195[0]);
        }
        else
        {
          v12 = GetTickCount();
          unknown_libname_91(v12, 0);
          System::__linkproc__ LStrCat3((int)v196, &str[1], (void *)v195[1]);
          sub_4CAB38(v219, nullptr, v196[0]);
        }
      }
      else if ( v201 > 0x413u )
      {
        switch ( v201 )
        {
          case 0x414u:
            sub_4DF3AC(v219);
            break;
          case 0x415u:
            sub_4DF604(v219, v207);
            break;
          case 0x44Cu:
            break;
          case 0xBBDu:
            if ( *(_BYTE *)(v219 + 772) == 0 )
            {
              v21 = GetTickCount();
              unknown_libname_91(v21, 0);
              System::__linkproc__ LStrCat3((int)v187, &str__GOOD_[1], (void *)v186[1]);
              sub_4CAB38(v219, nullptr, v187[0]);
            }
            break;
          default:
            goto LABEL_386;
        }
      }
      else
      {
        switch ( v201 )
        {
          case 0x413u:
            sub_4DF804(v219, v203, v207);
            break;
          case 0x40Fu:
            Iwserver::TIWServer::DetermineBrowser(v219, v207);
            break;
          case 0x410u:
            sub_4DF1EC(v219, v207);
            break;
          case 0x411u:
            sub_4DF2E8(v219, v207);
            break;
          case 0x412u:
            unknown_libname_16(v176, dword_4DA3FC);
            LOBYTE(v34) = 37;
            System::__linkproc__ PStrNCat(v176, v219 + 21, v34);
            unknown_libname_41();
            sub_4E4274(v177);
            break;
          default:
            goto LABEL_386;
        }
      }
    }
    else if ( v201 == 1038 )
    {
      sub_4DEDB4(v219, v207);
    }
    else if ( v201 > 0x3F9u )
    {
      switch ( v201 )
      {
        case 0x3FBu:
          if ( v204 != 0 )
            *(_BYTE *)(v219 + 176) = 1;
          else
            sub_4C3C10(v219);
          if ( *(_BYTE *)(v219 + 176) == 0 )
            goto LABEL_364;
          sub_4CAD6C(0, 0, 0, 1);
          break;
        case 0x3FCu:
          sub_408F0C(v207, v181);
          Idmessage::TIdMIMEBoundary::Push(v219, v181[0]);
          break;
        case 0x3FDu:
          sub_408F0C(v207, &v180);
          sub_4DCE48(v219, v180);
          break;
        case 0x3FEu:
          sub_408F0C(v207, &v179);
          sub_4DCFB8(v219, v179);
          break;
        case 0x3FFu:
          HIWORD(v30) = HIWORD(v207);
          v129 = v207;
          LOWORD(v115) = v205;
          LOWORD(v30) = v204;
          unknown_libname_78(v30, v115);
          sub_4DBFC0(v129);
          break;
        case 0x400u:
          HIWORD(v28) = HIWORD(v207);
          v127 = v207;
          LOWORD(v115) = v205;
          LOWORD(v28) = v204;
          unknown_libname_78(v28, v115);
          sub_4DBCCC(v127);
          break;
        case 0x401u:
          sub_408F0C(v207, &v178);
          sub_4DD0A8(v219, v178);
          break;
        case 0x402u:
          sub_4DD79C(v219, v203, v207);
          break;
        case 0x403u:
          sub_4DD958(v219, v203, v207);
          break;
        case 0x404u:
          sub_4DD450(v219);
          break;
        case 0x405u:
          sub_4DDB04(v219, v203);
          break;
        case 0x406u:
          sub_4DDC8C(v219);
          break;
        case 0x407u:
          HIWORD(v31) = HIWORD(v207);
          v130 = v207;
          LOWORD(v115) = v205;
          LOWORD(v31) = v204;
          unknown_libname_78(v31, v115);
          sub_4DC2B8(v130);
          break;
        case 0x408u:
          HIWORD(v33) = HIWORD(v207);
          v131 = v207;
          LOWORD(v115) = v205;
          LOWORD(v33) = v204;
          unknown_libname_78(v33, v115);
          sub_4DC664(v131);
          break;
        case 0x409u:
          sub_4DE550(v219);
          break;
        case 0x40Au:
          sub_4DCAF8(v219, v203, v207);
          break;
        case 0x40Bu:
          sub_4DE8E0(v219);
          break;
        case 0x40Cu:
          sub_4DEBDC(v219);
          break;
        case 0x40Du:
          sub_4DEBF0(v219);
          break;
        default:
          goto LABEL_386;
      }
    }
    else
    {
      if ( v201 == 1017 )
        goto LABEL_364;
      if ( v201 > 0x3EFu )
      {
        v11 = v201 - 1008;
        switch ( v201 )
        {
          case 0x3F0u:
            sub_4DB9A4(v219, v203, v204);
            break;
          case 0x3F1u:
            *(_BYTE *)(v219 + 1495) = 1;
            *(_BYTE *)(v219 + 1493) = 1;
            break;
          case 0x3F2u:
            sub_4DBA10(v219, v203);
            break;
          case 0x3F3u:
            sub_4DBAA4(v219, v203, v207);
            break;
          case 0x3F4u:
            HIWORD(v27) = HIWORD(v207);
            v126 = v207;
            LOWORD(v115) = v205;
            LOWORD(v27) = v204;
            unknown_libname_78(v27, v115);
            sub_4DBB7C(v126);
            break;
          case 0x3F5u:
            HIWORD(v29) = HIWORD(v207);
            v128 = v207;
            LOWORD(v115) = v205;
            LOWORD(v29) = v204;
            unknown_libname_78(v29, v115);
            sub_4DBE1C(v128);
            break;
          case 0x3F6u:
            LOWORD(v115) = v205;
            LOWORD(v11) = v204;
            v32 = unknown_libname_78(v11, v115);
            sub_4DCA10(v207, 0, v32);
            break;
          case 0x3F7u:
            sub_4DCA10(v207, v204, 0);
            break;
          case 0x3F8u:
            if ( (int)v203 > 0 )
              sub_4C5BB0(v219, v203);
            break;
          default:
            goto LABEL_386;
        }
      }
      else if ( v201 == 1007 )
      {
        sub_4DB7E0(v202, v205);
      }
      else if ( v201 > 0x3EAu )
      {
        switch ( v201 )
        {
          case 0x3EBu:
            sub_4DAC70(v207);
            break;
          case 0x3ECu:
            sub_4DB01C(v207);
            break;
          case 0x3EDu:
            break;
          case 0x3EEu:
            sub_4DB3B0(v219, v203, v207);
            break;
          default:
            goto LABEL_386;
        }
      }
      else if ( v201 == 1002 )
      {
        sub_4DABD4(v219, v204, v205);
      }
      else if ( v201 > 0x52u )
      {
        if ( v201 == 1000 )
        {
          sub_4C5A2C(v219, v207, (int)v203);
LABEL_173:
          sub_4CAD6C(v207, 0, 0, 0);
        }
        else
        {
          if ( v201 != 1001 )
            goto LABEL_386;
          if ( *(_DWORD *)(v219 + 36) == v204 && *(_DWORD *)(v219 + 40) == v205 )
            sub_4C5CB0(v219);
        }
      }
      else
      {
        switch ( v201 )
        {
          case 'R':
            sub_4DE654(v205);
            break;
          case 'P':
            sub_4DA8E8(v205);
            break;
          case 'Q':
            sub_4D0EDC(v219);
            break;
          default:
            goto LABEL_386;
        }
      }
    }
  }
  if ( *(_BYTE *)(v219 + 1492) != 0 || *(_BYTE *)(v219 + 1494) != 0 || *(_BYTE *)(v219 + 1493) != 0 )
  {
    if ( *(_BYTE *)(v219 + 1497) != 0 )
    {
      System::__linkproc__ LStrToString(v152, *(_DWORD *)(v219 + 1504), 255);
      LOBYTE(v116) = 16;
      System::__linkproc__ PStrNCpy(v219 + 4, v152, v116);
      *(_DWORD *)(v219 + 36) = *(_DWORD *)(v219 + 1508);
      *(_DWORD *)(v219 + 40) = *(_DWORD *)(v219 + 1512);
    }
    sub_4BB300(v219);
    if ( *(_BYTE *)(v219 + 1494) != 0 )
      sub_4CAD6C(0, 0, 0, 0);
    if ( *(_BYTE *)(v219 + 1495) == 0 && *(_BYTE *)(v219 + 1493) != 0 )
    {
      unknown_libname_41();
      sub_48D448(*off_4EA3DC, v151[2], *(_DWORD *)(v219 + 1472));
    }
    __writefsdword(0, (unsigned int)v144);
  }
  else
  {
    __writefsdword(0, (unsigned int)v144);
    sub_4C936C(v219);
  }
  __writefsdword(0, (unsigned int)ExceptionList);
  v149 = (int *)&loc_4DA354;
  unknown_libname_35(&v150, 6);
  unknown_libname_35(v151, 3);
  unknown_libname_35(&v153, 24);
  unknown_libname_35(&v177, 35);
  unknown_libname_51(&v201, off_45464C);
  return unknown_libname_34(&v211);
}


// ==================== [0x4DA734] sub_4DA734 (size: 433) ====================
int __fastcall sub_4DA734(int a1)
{
  int v1; // eax
  char v2; // zf
  int v3; // eax
  int v4; // ecx
  unsigned __int16 v5; // ax
  int v6; // eax
  unsigned __int16 v7; // ax
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-Ch] [ebp-128h] BYREF
  void *v10; // [esp-8h] [ebp-124h]
  int *v11; // [esp-4h] [ebp-120h]
  void *v12; // [esp+0h] [ebp-11Ch]
  int v13; // [esp+4h] [ebp-118h] BYREF
  _BYTE v14[256]; // [esp+8h] [ebp-114h] BYREF
  int v15; // [esp+108h] [ebp-14h] BYREF
  int v16; // [esp+10Ch] [ebp-10h]
  char v17; // [esp+113h] [ebp-9h]
  int v18; // [esp+114h] [ebp-8h]
  int v19; // [esp+118h] [ebp-4h]
  int savedregs; // [esp+11Ch] [ebp+0h] BYREF

  v13 = 0;
  v15 = 0;
  v19 = a1;
  v11 = &savedregs;
  v10 = &loc_4DA8D9;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  v17 = 0;
  v1 = (*(int (__fastcall **)(_DWORD, _DWORD))(*(_DWORD *)*off_4EA5EC + 20))(*off_4EA5EC, *(_DWORD *)*off_4EA5EC) - 1;
  if ( v1 >= 0 )
  {
    v16 = v1 + 1;
    v18 = 0;
    do
    {
      (*(void (__fastcall **)(_DWORD, int, int *))(*(_DWORD *)*off_4EA5EC + 12))(*off_4EA5EC, v18, &v15);
      System::__linkproc__ LStrCmp(*(_DWORD *)(*(_DWORD *)(v19 + 760) + 4), v15);
      if ( v2 )
      {
        ExceptionList = (struct _EXCEPTION_REGISTRATION_RECORD *)(*(unsigned __int16 (__fastcall **)(_DWORD, int))(*(_DWORD *)*off_4EA5EC + 24))(
                                                                   *off_4EA5EC,
                                                                   v18);
        if ( (int)abs32(*(_DWORD *)(v19 + 36) - (_DWORD)ExceptionList) < 50 )
        {
          v3 = (*(int (__fastcall **)(_DWORD, int))(*(_DWORD *)*off_4EA5EC + 24))(*off_4EA5EC, v18);
          ExceptionList = (struct _EXCEPTION_REGISTRATION_RECORD *)(unsigned __int16)sub_407B24(v3);
          if ( (int)abs32(*(_DWORD *)(v19 + 40) - (_DWORD)ExceptionList) < 50 )
          {
            (*(void (__fastcall **)(_DWORD, int, int *))(*(_DWORD *)*off_4EA5EC + 12))(*off_4EA5EC, v18, &v13);
            System::__linkproc__ LStrToString(v14, v13, 255);
            LOBYTE(v4) = 16;
            System::__linkproc__ PStrNCpy(v19 + 120, v14, v4);
            v5 = (*(int (__fastcall **)(_DWORD, int))(*(_DWORD *)*off_4EA5EC + 24))(*off_4EA5EC, v18);
            *(_DWORD *)(v19 + 140) = v5;
            v6 = (*(int (__fastcall **)(_DWORD, int))(*(_DWORD *)*off_4EA5EC + 24))(*off_4EA5EC, v18);
            v7 = sub_407B24(v6);
            *(_DWORD *)(v19 + 144) = v7;
            v17 = 1;
          }
        }
      }
      ++v18;
      --v16;
    }
    while ( v16 != 0 );
  }
  if ( sub_4BF0A0(v19) >= 2 )
  {
    *(_WORD *)(v19 + 120) = 14593;
    *(_DWORD *)(v19 + 140) = 33;
    *(_DWORD *)(v19 + 144) = 33;
  }
  __writefsdword(0, (unsigned int)v10);
  v12 = &loc_4DA8E0;
  unknown_libname_34(&v13);
  return unknown_libname_34(&v15);
}


// ==================== [0x4DA9E4] sub_4DA9E4 (size: 481) ====================
int __fastcall sub_4DA9E4(int a1)
{
  int v1; // eax
  char v2; // al
  bool v3; // cf
  char v4; // al
  int v5; // ecx
  int v6; // ecx
  int v7; // ecx
  unsigned int v9[2]; // [esp-Ch] [ebp-60h] BYREF
  int *v10; // [esp-4h] [ebp-58h]
  int v11; // [esp+8h] [ebp-4Ch] BYREF
  int v12; // [esp+Ch] [ebp-48h] BYREF
  int v13; // [esp+10h] [ebp-44h] BYREF
  int v14; // [esp+14h] [ebp-40h] BYREF
  int v15; // [esp+18h] [ebp-3Ch] BYREF
  int v16; // [esp+1Ch] [ebp-38h] BYREF
  int v17; // [esp+20h] [ebp-34h] BYREF
  int v18; // [esp+24h] [ebp-30h] BYREF
  int v19; // [esp+28h] [ebp-2Ch] BYREF
  _DWORD v20[3]; // [esp+2Ch] [ebp-28h] BYREF
  _BYTE v21[20]; // [esp+38h] [ebp-1Ch] BYREF
  int v22; // [esp+4Ch] [ebp-8h] BYREF
  int v23; // [esp+50h] [ebp-4h]
  int savedregs; // [esp+54h] [ebp+0h] BYREF

  v23 = a1;
  v10 = &savedregs;
  v9[1] = (unsigned int)&loc_4DABB8;
  v9[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v9);
  sub_4546F8(v20, 0, 0);
  v1 = v23;
  *(_DWORD *)(v23 + 1360) = v20[0];
  *(_DWORD *)(v1 + 1364) = v20[1];
  *(_DWORD *)(v1 + 1368) = v20[2];
  unknown_libname_34(&v22);
  qmemcpy(v21, (const void *)(v23 + 202), sizeof(v21));
  v2 = *(_BYTE *)(v23 + 47);
  v3 = v2 == 0;
  v4 = v2 - 1;
  if ( v3 )
  {
    sub_4565B0((int *)off_4EA3B0, 20, &v19, v9[0]);
    sub_4565B0(v23 + 222, 20, &v18, &str___97[1]);
    sub_4565B0(v21, 20, &v17, &str___97[1]);
    System::__linkproc__ LStrCatN(&v22, 5, v5);
  }
  else if ( v4 != 0 )
  {
    if ( v4 == 1 )
    {
      sub_4565B0((int *)off_4EA4B0, 20, &v13, v9[0]);
      sub_4565B0(v23 + 222, 20, &v12, &str___97[1]);
      sub_4565B0(v21, 20, &v11, &str___97[1]);
      System::__linkproc__ LStrCatN(&v22, 5, v7);
    }
  }
  else
  {
    sub_4565B0((int *)off_4EA278, 20, &v16, v9[0]);
    sub_4565B0(v23 + 222, 20, &v15, &str___97[1]);
    sub_4565B0(v21, 20, &v14, &str___97[1]);
    System::__linkproc__ LStrCatN(&v22, 5, v6);
  }
  sub_4CAB38(v23, (void *)(v23 + 1360), v22);
  __writefsdword(0, v9[0]);
  v10 = (int *)&loc_4DABBF;
  unknown_libname_35(&v11, 9);
  return unknown_libname_34(&v22);
}


// ==================== [0x4DAC70] sub_4DAC70 (size: 908) ====================
int __fastcall sub_4DAC70(int a1, unsigned __int8 a2, int a3, int a4)
{
  _DWORD *v4; // eax
  int v5; // eax
  int v6; // edx
  int v7; // ecx
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-Ch] [ebp-74h] BYREF
  void *v10; // [esp-8h] [ebp-70h]
  int *v11; // [esp-4h] [ebp-6Ch]
  unsigned int v12; // [esp+0h] [ebp-68h]
  void *v13; // [esp+8h] [ebp-60h]
  int v14; // [esp+Ch] [ebp-5Ch] BYREF
  _BYTE v15[44]; // [esp+10h] [ebp-58h] BYREF
  int v16; // [esp+3Ch] [ebp-2Ch]
  void *Mem; // [esp+40h] [ebp-28h]
  _BYTE *v18; // [esp+44h] [ebp-24h]
  int v19; // [esp+48h] [ebp-20h]
  const void *v20; // [esp+4Ch] [ebp-1Ch]
  int v21; // [esp+50h] [ebp-18h]
  int v22; // [esp+54h] [ebp-14h]
  int v23; // [esp+58h] [ebp-10h]
  int v24; // [esp+5Ch] [ebp-Ch]
  unsigned __int8 v25; // [esp+63h] [ebp-5h]
  int v26; // [esp+64h] [ebp-4h]
  int savedregs; // [esp+68h] [ebp+0h] BYREF

  v14 = 0;
  v24 = a3;
  v25 = a2;
  v26 = a1;
  unknown_libname_44(a4);
  v11 = &savedregs;
  v10 = &loc_4DAFEC;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  v20 = nullptr;
  v18 = nullptr;
  v22 = -1;
  if ( *(_DWORD *)(*(_DWORD *)(v26 + 1036) + 8) - 1 >= 0 )
  {
    v16 = *(_DWORD *)(*(_DWORD *)(v26 + 1036) + 8);
    v23 = 0;
    while ( 1 )
    {
      v4 = (_DWORD *)Classes::TList::Get(*(Classes::TList **)(v26 + 1036), v23);
      if ( *v4 == v24 )
      {
        v5 = Classes::TList::Get(*(Classes::TList **)(v26 + 1036), v23);
        v20 = (const void *)sub_4AC2F8(*off_4EA490, *(unsigned __int16 *)(v5 + 4));
        if ( v20 != nullptr )
        {
          unknown_libname_41();
          if ( unknown_libname_84(v14, a4) == 0 )
            break;
        }
      }
      ++v23;
      if ( --v16 == 0 )
        goto LABEL_8;
    }
    v22 = v23;
    v18 = (_BYTE *)Classes::TList::Get(*(Classes::TList **)(v26 + 1036), v23);
  }
LABEL_8:
  v21 = 0;
  if ( v20 != nullptr && v18 != nullptr )
  {
    if ( (unsigned __int8)sub_4B2D70(v25, v20) != 0 )
    {
      qmemcpy(v15, v20, sizeof(v15));
      sub_495974(*off_4EA324, v18, v15);
      if ( (unsigned __int8)sub_4C5084(ExceptionList, v10, v11) != 0 )
      {
        Mem = nullptr;
        if ( *(_WORD *)(v26 + 24 * v25 + 1060) != 0 )
        {
          v19 = sub_4AC2F8(*off_4EA490, *(unsigned __int16 *)(v26 + 24 * v25 + 1060));
          if ( (*(_BYTE *)(v19 + 15) == 15
             || (unsigned __int8)(*(_BYTE *)(v19 + 15) - 19) < 6u
             || *(_BYTE *)(v19 + 15) == 26)
            && *(_BYTE *)(v26 + 839) == 0
            && *(_BYTE *)(v26 + 24 * v25 + 1073) != 0
            || *(_BYTE *)(v26 + 839) == 0 && (*(_BYTE *)(v19 + 20) & 2) != 0
            || (*(_BYTE *)(v19 + 20) & 4) != 0 )
          {
            sub_4BB124(v26, &str_You_can_not_tak[1], 0);
            v21 = -4;
            goto LABEL_34;
          }
          Mem = (void *)System::__linkproc__ GetMem(24);
          qmemcpy(Mem, (const void *)(v26 + 24 * v25 + 1056), 0x18u);
        }
        if ( (*((_BYTE *)v20 + 15) == 15
           || (unsigned __int8)(*((_BYTE *)v20 + 15) - 19) < 6u
           || *((_BYTE *)v20 + 15) == 26)
          && v18[18] != 0 )
        {
          v18[18] = 0;
        }
        qmemcpy((void *)(v26 + 24 * v25 + 1056), v18, 0x18u);
        sub_4C4F10(v26, v22);
        if ( Mem != nullptr )
        {
          sub_4C4DB8(v26, Mem);
          sub_4D0824(v26, Mem);
        }
        unknown_libname_25(v26, v6, v7);
        sub_4B865C(v26, v26, 10051, 0, 0, 0, 0, 0);
        sub_4B865C(v26, v26, 10302, 0, 0, 0, 0, 0);
        v11 = nullptr;
        v10 = nullptr;
        ExceptionList = nullptr;
        Sysutils::LoadPackage(v26);
        sub_4CAD6C(0, (__int16)ExceptionList, (__int16)v10, (__int16)v11);
        sub_4BA58C(v26);
        v21 = 1;
      }
      else
      {
        v21 = -1;
      }
    }
    else
    {
      v21 = -2;
    }
  }
LABEL_34:
  if ( v21 <= 0 )
    sub_4CAD6C(0, 0, 0, 0);
  __writefsdword(0, v12);
  v13 = &loc_4DAFF3;
  unknown_libname_34(&v14);
  return unknown_libname_34(&a4);
}


// ==================== [0x4DB01C] sub_4DB01C (size: 671) ====================
int __fastcall sub_4DB01C(int a1, unsigned __int8 a2, int a3, int a4)
{
  int v4; // edx
  int v5; // ecx
  unsigned int v7[2]; // [esp-Ch] [ebp-34h] BYREF
  int *v8; // [esp-4h] [ebp-2Ch]
  int v9; // [esp+Ch] [ebp-1Ch] BYREF
  void *Mem; // [esp+10h] [ebp-18h]
  int v11; // [esp+14h] [ebp-14h]
  int v12; // [esp+18h] [ebp-10h]
  int v13; // [esp+1Ch] [ebp-Ch]
  unsigned __int8 v14; // [esp+23h] [ebp-5h]
  int v15; // [esp+24h] [ebp-4h]
  int savedregs; // [esp+28h] [ebp+0h] BYREF

  v9 = 0;
  v13 = a3;
  v14 = a2;
  v15 = a1;
  unknown_libname_44(a4);
  v8 = &savedregs;
  v7[1] = (unsigned int)&loc_4DB2AB;
  v7[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v7);
  v12 = 0;
  if ( *(_BYTE *)(v15 + 791) != 0 || v14 >= 9u )
  {
    v12 = -1;
  }
  else if ( *(_WORD *)(v15 + 24 * v14 + 1060) != 0 )
  {
    if ( *(_DWORD *)(v15 + 24 * v14 + 1056) == v13 )
    {
      v11 = sub_4AC2F8(*off_4EA490, *(unsigned __int16 *)(v15 + 24 * v14 + 1060));
      if ( (*(_BYTE *)(v11 + 15) == 15 || (unsigned __int8)(*(_BYTE *)(v11 + 15) - 19) < 6u
                                       || *(_BYTE *)(v11 + 15) == 26)
        && *(_BYTE *)(v15 + 839) == 0
        && *(_BYTE *)(v15 + 24 * v14 + 1073) != 0
        || *(_BYTE *)(v15 + 839) == 0 && (*(_BYTE *)(v11 + 20) & 2) != 0
        || (*(_BYTE *)(v11 + 20) & 4) != 0 )
      {
        sub_4BB124(v15, &str_You_can_not_tak_0[1], 0);
        v12 = -4;
      }
      else
      {
        unknown_libname_41();
        if ( unknown_libname_84(v9, a4) == 0 )
        {
          Mem = (void *)System::__linkproc__ GetMem(24);
          qmemcpy(Mem, (const void *)(v15 + 24 * v14 + 1056), 0x18u);
          if ( (unsigned __int8)sub_4C4DB8(v15, Mem) != 0 )
          {
            *(_WORD *)(v15 + 24 * v14 + 1060) = 0;
            Sysutils::LoadPackage(v15);
            sub_4CAD6C(0, 0, 0, 0);
            sub_4D0824(v15, Mem);
            unknown_libname_25(v15, v4, v5);
            sub_4B865C(v15, v15, 10051, 0, 0, 0, 0, 0);
            sub_4B865C(v15, v15, 10302, 0, 0, 0, 0, 0);
            sub_4BA58C(v15);
          }
          else
          {
            System::__linkproc__ FreeMem(Mem, 24);
            v12 = -3;
          }
        }
      }
    }
  }
  else
  {
    v12 = -2;
  }
  if ( v12 <= 0 )
    sub_4CAD6C(0, 0, 0, 0);
  __writefsdword(0, v7[0]);
  v8 = (int *)&loc_4DB2B2;
  unknown_libname_34(&v9);
  return unknown_libname_34(&a4);
}


// ==================== [0x4DB3B0] sub_4DB3B0 (size: 1006) ====================
int __fastcall sub_4DB3B0(__int32 a1, int a2, int a3)
{
  _DWORD *v3; // eax
  int v4; // eax
  unsigned __int8 v5; // al
  bool v6; // cf
  char v7; // al
  __int32 v8; // eax
  int v9; // eax
  int v10; // eax
  int v11; // edx
  int v12; // eax
  int v13; // ecx
  int v14; // ecx
  __int32 v15; // eax
  int v16; // eax
  int v17; // ecx
  unsigned int v19[2]; // [esp-Ch] [ebp-9Ch] BYREF
  int *v20; // [esp-4h] [ebp-94h]
  _DWORD v21[2]; // [esp+Ch] [ebp-84h] BYREF
  _DWORD v22[4]; // [esp+14h] [ebp-7Ch] BYREF
  _BYTE v23[24]; // [esp+24h] [ebp-6Ch] BYREF
  _BYTE v24[20]; // [esp+3Ch] [ebp-54h] BYREF
  int v25; // [esp+50h] [ebp-40h] BYREF
  int v26; // [esp+54h] [ebp-3Ch] BYREF
  int v27; // [esp+58h] [ebp-38h] BYREF
  int v28[6]; // [esp+5Ch] [ebp-34h] BYREF
  int v29; // [esp+74h] [ebp-1Ch] BYREF
  int v30; // [esp+78h] [ebp-18h]
  _BYTE *v31; // [esp+7Ch] [ebp-14h]
  char v32; // [esp+83h] [ebp-Dh]
  int v33; // [esp+84h] [ebp-Ch]
  int v34; // [esp+88h] [ebp-8h]
  __int32 v35; // [esp+8Ch] [ebp-4h]
  int savedregs; // [esp+90h] [ebp+0h] BYREF

  v26 = 0;
  v25 = 0;
  memset(v22, 0, sizeof(v22));
  v21[1] = 0;
  v21[0] = 0;
  v27 = 0;
  v29 = a3;
  v34 = a2;
  v35 = a1;
  unknown_libname_44(a3);
  v20 = &savedregs;
  v19[1] = (unsigned int)&loc_4DB790;
  v19[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v19);
  v32 = 0;
  if ( *(_BYTE *)(v35 + 772) == 0 && *(_DWORD *)(*(_DWORD *)(v35 + 1036) + 8) - 1 >= 0 )
  {
    v30 = *(_DWORD *)(*(_DWORD *)(v35 + 1036) + 8);
    v33 = 0;
    while ( 1 )
    {
      v3 = (_DWORD *)Classes::TList::Get(*(Classes::TList **)(v35 + 1036), v33);
      if ( *v3 == v34 )
        break;
      ++v33;
      if ( --v30 == 0 )
        goto LABEL_21;
    }
    v4 = Classes::TList::Get(*(Classes::TList **)(v35 + 1036), v33);
    v31 = (_BYTE *)sub_4AC2F8(*off_4EA490, *(unsigned __int16 *)(v4 + 4));
    qmemcpy(v28, (const void *)Classes::TList::Get(*(Classes::TList **)(v35 + 1036), v33), sizeof(v28));
    v5 = v31[15];
    v6 = v5 < 4u;
    v7 = v5 - 4;
    if ( v6 )
    {
      v8 = Classes::TList::Get(*(Classes::TList **)(v35 + 1036), v33);
      if ( sub_4C6238(v35, v31, v8) )
      {
        v9 = Classes::TList::Get(*(Classes::TList **)(v35 + 1036), v33);
        System::__linkproc__ FreeMem(v9, 24);
        Classes::TList::Delete(*(Classes::TList **)(v35 + 1036), v33);
        v32 = 1;
      }
    }
    else if ( v7 != 0 )
    {
      if ( v7 == 27 && *(_DWORD *)(*(_DWORD *)(v35 + 1036) + 8) + 5 <= 46 )
      {
        v12 = Classes::TList::Get(*(Classes::TList **)(v35 + 1036), v33);
        System::__linkproc__ FreeMem(v12, 24);
        Classes::TList::Delete(*(Classes::TList **)(v35 + 1036), v33);
        sub_4E4214((unsigned __int8)v31[16], &v27);
        sub_4DB2DC(&savedregs);
        v32 = 1;
      }
    }
    else if ( sub_4C67DC(v35, v31) != 0 )
    {
      v10 = Classes::TList::Get(*(Classes::TList **)(v35 + 1036), v33);
      System::__linkproc__ FreeMem(v10, 24);
      Classes::TList::Delete(*(Classes::TList **)(v35 + 1036), v33);
      v32 = 1;
      if ( *(_DWORD *)(v35 + 1312) != 0 && *(_BYTE *)(v35 + 1325) == 0 )
      {
        LOBYTE(v11) = 1;
        sub_4BC900(v35, v11);
        sub_4CAB38(v35, nullptr, (int)&str__LNG_1[1]);
      }
      if ( *(_DWORD *)(v35 + 1316) != 0 && *(_BYTE *)(v35 + 1326) == 0 )
      {
        LOBYTE(v11) = 1;
        sub_4BC980(v35, v11);
        sub_4CAB38(v35, nullptr, (int)&str__WID_0[1]);
      }
    }
  }
LABEL_21:
  if ( v32 != 0 )
  {
    sub_4C49BC(v35);
    sub_4CAD6C(0, 0, 0, 0);
    unknown_libname_16(v24, &dword_4DB7C0);
    LOBYTE(v13) = 19;
    System::__linkproc__ PStrNCat(v24, v35 + 4, v13);
    unknown_libname_16(v23, v24);
    LOBYTE(v14) = 20;
    System::__linkproc__ PStrNCat(v23, dword_4DB7C4, v14);
    unknown_libname_41();
    Sysutils::IntToStr(*(_DWORD *)(v35 + 36));
    Sysutils::IntToStr(*(_DWORD *)(v35 + 40));
    unknown_libname_41();
    sub_4AC1AC(*off_4EA490, LOWORD(v28[1]), v22);
    Sysutils::IntToStr(v28[0]);
    v15 = v35;
    LOBYTE(v15) = *(_BYTE *)(v35 + 570) == 0;
    v16 = sub_454558(v15);
    Sysutils::IntToStr(v16);
    System::__linkproc__ LStrCatN(&v26, 14, v17);
    sub_4E42F8(v26);
  }
  else
  {
    sub_4CAD6C(0, 0, 0, 0);
  }
  __writefsdword(0, v19[0]);
  v20 = (int *)&loc_4DB797;
  unknown_libname_35(v21, 6);
  unknown_libname_35(&v25, 3);
  return unknown_libname_34(&v29);
}


// ==================== [0x4DB7E0] sub_4DB7E0 (size: 424) ====================
int __fastcall sub_4DB7E0(int a1, int a2, int a3, int a4, char a5)
{
  int v6; // [esp+0h] [ebp-14h]
  int v7; // [esp+4h] [ebp-10h]

  if ( (int)abs32(a3 - *(_DWORD *)(a1 + 36)) <= 2 && (int)abs32(a4 - *(_DWORD *)(a1 + 40)) <= 2 )
  {
    if ( (unsigned __int8)sub_4B5A3C(a2, 2) != 0
      && *(_BYTE *)(a2 + 772) != 0
      && *(_BYTE *)(a2 + 706) == 0
      && *(_BYTE *)(a2 + 699) != 0 )
    {
      v7 = System::__linkproc__ RandInt(16) + 5;
      v6 = System::__linkproc__ RandInt(201) + 100;
      *(_DWORD *)(a2 + 712) -= v7;
      *(_DWORD *)(a2 + 708) -= v6;
      if ( *(int *)(a2 + 708) < 0 )
        *(_DWORD *)(a2 + 708) = 0;
      if ( *(int *)(a2 + 712) <= 0 )
      {
        if ( *(_BYTE *)(a2 + 570) >= 0x32u && *(_BYTE *)(a2 + 570) < 0x50u )
        {
          *(_BYTE *)(a2 + 706) = 1;
          sub_4BB32C(a2);
          sub_4B8EBC(a2, 10024, *(unsigned __int8 *)(a2 + 44), *(_DWORD *)(a2 + 36), *(_DWORD *)(a2 + 40), 0, 0);
        }
        if ( (unsigned __int8)sub_4BB3B0(a1, a2) == 0 )
          sub_4BB124(a1, aYouFoundNothin, 0);
        *(_DWORD *)(a2 + 712) = 50;
      }
      *(_DWORD *)(a1 + 776) = GetTickCount();
    }
    *(_BYTE *)(a1 + 44) = a5;
  }
  return sub_4B8EBC(a1, 10119, *(unsigned __int8 *)(a1 + 44), *(_DWORD *)(a1 + 36), *(_DWORD *)(a1 + 40), 0, 0);
}


// ==================== [0x4DBB7C] sub_4DBB7C (size: 335) ====================
int __fastcall sub_4DBB7C(int a1, int a2, int a3, int a4)
{
  _DWORD *v4; // eax
  int v5; // eax
  __int64 v6; // rax
  __int64 v7; // rax
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-Ch] [ebp-30h] BYREF
  void *v10; // [esp-8h] [ebp-2Ch]
  int *v11; // [esp-4h] [ebp-28h]
  int v12; // [esp+4h] [ebp-20h] BYREF
  int v13; // [esp+8h] [ebp-1Ch]
  int v14; // [esp+Ch] [ebp-18h]
  _DWORD *v15; // [esp+10h] [ebp-14h]
  int v16; // [esp+14h] [ebp-10h]
  int v17; // [esp+18h] [ebp-Ch]
  int v18; // [esp+1Ch] [ebp-8h]
  int v19; // [esp+20h] [ebp-4h]
  int savedregs; // [esp+24h] [ebp+0h] BYREF

  v12 = 0;
  v17 = a3;
  v18 = a2;
  v19 = a1;
  unknown_libname_44(a4);
  v11 = &savedregs;
  v10 = &loc_4DBCBD;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  v14 = 0;
  if ( *(_DWORD *)(*(_DWORD *)(v19 + 1036) + 8) - 1 >= 0 )
  {
    v13 = *(_DWORD *)(*(_DWORD *)(v19 + 1036) + 8);
    v16 = 0;
    while ( 1 )
    {
      v4 = (_DWORD *)Classes::TList::Get(*(Classes::TList **)(v19 + 1036), v16);
      if ( *v4 == v17 )
      {
        v5 = Classes::TList::Get(*(Classes::TList **)(v19 + 1036), v16);
        sub_4AC1AC(*off_4EA490, *(unsigned __int16 *)(v5 + 4), &v12);
        if ( unknown_libname_84(v12, a4) == 0 )
          break;
      }
      ++v16;
      if ( --v13 == 0 )
        goto LABEL_7;
    }
    v14 = Classes::TList::Get(*(Classes::TList **)(v19 + 1036), v16);
  }
LABEL_7:
  if ( v14 != 0 )
  {
    v15 = (_DWORD *)sub_4AC858(*off_4EA490, v18);
    if ( v15 != nullptr && v15[190] == *(_DWORD *)(v19 + 760) )
    {
      v6 = v15[9] - *(_DWORD *)(v19 + 36);
      if ( (int)((HIDWORD(v6) ^ v6) - HIDWORD(v6)) <= 15 )
      {
        v7 = v15[10] - *(_DWORD *)(v19 + 40);
        if ( (int)((HIDWORD(v7) ^ v7) - HIDWORD(v7)) <= 15 )
          sub_4A1B84(v15, v19, v14, v11, v10, ExceptionList);
      }
    }
  }
  __writefsdword(0, (unsigned int)ExceptionList);
  v11 = (int *)&loc_4DBCC4;
  unknown_libname_34(&v12);
  return unknown_libname_34(&a4);
}


// ==================== [0x4DBCCC] sub_4DBCCC (size: 335) ====================
int __fastcall sub_4DBCCC(int a1, int a2, int a3, int a4)
{
  _DWORD *v4; // eax
  int v5; // eax
  __int64 v6; // rax
  __int64 v7; // rax
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-Ch] [ebp-30h] BYREF
  void *v10; // [esp-8h] [ebp-2Ch]
  int *v11; // [esp-4h] [ebp-28h]
  int v12; // [esp+4h] [ebp-20h] BYREF
  int v13; // [esp+8h] [ebp-1Ch]
  int v14; // [esp+Ch] [ebp-18h]
  _DWORD *v15; // [esp+10h] [ebp-14h]
  int v16; // [esp+14h] [ebp-10h]
  int v17; // [esp+18h] [ebp-Ch]
  int v18; // [esp+1Ch] [ebp-8h]
  int v19; // [esp+20h] [ebp-4h]
  int savedregs; // [esp+24h] [ebp+0h] BYREF

  v12 = 0;
  v17 = a3;
  v18 = a2;
  v19 = a1;
  unknown_libname_44(a4);
  v11 = &savedregs;
  v10 = &loc_4DBE0D;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  v14 = 0;
  if ( *(_DWORD *)(*(_DWORD *)(v19 + 1036) + 8) - 1 >= 0 )
  {
    v13 = *(_DWORD *)(*(_DWORD *)(v19 + 1036) + 8);
    v16 = 0;
    while ( 1 )
    {
      v4 = (_DWORD *)Classes::TList::Get(*(Classes::TList **)(v19 + 1036), v16);
      if ( *v4 == v17 )
      {
        v5 = Classes::TList::Get(*(Classes::TList **)(v19 + 1036), v16);
        sub_4AC1AC(*off_4EA490, *(unsigned __int16 *)(v5 + 4), &v12);
        if ( unknown_libname_84(v12, a4) == 0 )
          break;
      }
      ++v16;
      if ( --v13 == 0 )
        goto LABEL_7;
    }
    v14 = Classes::TList::Get(*(Classes::TList **)(v19 + 1036), v16);
  }
LABEL_7:
  if ( v14 != 0 )
  {
    v15 = (_DWORD *)sub_4AC858(*off_4EA490, v18);
    if ( v15 != nullptr && v15[190] == *(_DWORD *)(v19 + 760) )
    {
      v6 = v15[9] - *(_DWORD *)(v19 + 36);
      if ( (int)((HIDWORD(v6) ^ v6) - HIDWORD(v6)) <= 15 )
      {
        v7 = v15[10] - *(_DWORD *)(v19 + 40);
        if ( (int)((HIDWORD(v7) ^ v7) - HIDWORD(v7)) <= 15 )
          sub_4A1F30(v15, v19, v14, v11, v10, ExceptionList);
      }
    }
  }
  __writefsdword(0, (unsigned int)ExceptionList);
  v11 = (int *)&loc_4DBE14;
  unknown_libname_34(&v12);
  return unknown_libname_34(&a4);
}


// ==================== [0x4DBE1C] sub_4DBE1C (size: 419) ====================
int __fastcall sub_4DBE1C(int *a1, int a2, int a3, int a4)
{
  _DWORD *v4; // eax
  int v5; // eax
  __int64 v6; // rax
  __int64 v7; // rax
  int v8; // eax
  unsigned int v10[2]; // [esp-Ch] [ebp-30h] BYREF
  int *v11; // [esp-4h] [ebp-28h]
  int v12; // [esp+4h] [ebp-20h] BYREF
  int v13; // [esp+8h] [ebp-1Ch]
  const void *v14; // [esp+Ch] [ebp-18h]
  _DWORD *v15; // [esp+10h] [ebp-14h]
  int v16; // [esp+14h] [ebp-10h]
  int v17; // [esp+18h] [ebp-Ch]
  int v18; // [esp+1Ch] [ebp-8h]
  int *v19; // [esp+20h] [ebp-4h]
  int savedregs; // [esp+24h] [ebp+0h] BYREF

  v12 = 0;
  v17 = a3;
  v18 = a2;
  v19 = a1;
  unknown_libname_44(a4);
  v11 = &savedregs;
  v10[1] = (unsigned int)&loc_4DBFB1;
  v10[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v10);
  v14 = nullptr;
  if ( *(_DWORD *)(v19[259] + 8) - 1 >= 0 )
  {
    v13 = *(_DWORD *)(v19[259] + 8);
    v16 = 0;
    while ( 1 )
    {
      v4 = (_DWORD *)Classes::TList::Get((Classes::TList *)v19[259], v16);
      if ( *v4 == v17 )
      {
        v5 = Classes::TList::Get((Classes::TList *)v19[259], v16);
        sub_4AC1AC(*off_4EA490, *(unsigned __int16 *)(v5 + 4), &v12);
        if ( unknown_libname_84(v12, a4) == 0 )
          break;
      }
      ++v16;
      if ( --v13 == 0 )
        goto LABEL_13;
    }
    v14 = (const void *)Classes::TList::Get((Classes::TList *)v19[259], v16);
    v15 = (_DWORD *)sub_4AC858(*off_4EA490, v18);
    if ( v15 != nullptr && v14 != nullptr && v15[190] == v19[190] )
    {
      v6 = v15[9] - v19[9];
      if ( (int)((HIDWORD(v6) ^ v6) - HIDWORD(v6)) <= 15 )
      {
        v7 = v15[10] - v19[10];
        if ( (int)((HIDWORD(v7) ^ v7) - HIDWORD(v7)) <= 15 && sub_4A1CD8((int)v15, v19, v14) != 0 )
        {
          v8 = Classes::TList::Get((Classes::TList *)v19[259], v16);
          System::__linkproc__ FreeMem(v8, 24);
          Classes::TList::Delete((Classes::TList *)v19[259], v16);
          sub_4C49BC(v19);
        }
      }
    }
  }
LABEL_13:
  __writefsdword(0, v10[0]);
  v11 = (int *)&loc_4DBFB8;
  unknown_libname_34(&v12);
  return unknown_libname_34(&a4);
}


// ==================== [0x4DBFC0] sub_4DBFC0 (size: 351) ====================
int __fastcall sub_4DBFC0(int a1, int a2, int a3, int a4)
{
  _DWORD *v4; // eax
  int v5; // eax
  __int64 v6; // rax
  __int64 v7; // rax
  unsigned int v9[2]; // [esp-Ch] [ebp-30h] BYREF
  int *v10; // [esp-4h] [ebp-28h]
  int v11; // [esp+4h] [ebp-20h] BYREF
  int v12; // [esp+8h] [ebp-1Ch]
  unsigned __int16 *v13; // [esp+Ch] [ebp-18h]
  _DWORD *v14; // [esp+10h] [ebp-14h]
  int v15; // [esp+14h] [ebp-10h]
  int v16; // [esp+18h] [ebp-Ch]
  int v17; // [esp+1Ch] [ebp-8h]
  int v18; // [esp+20h] [ebp-4h]
  int savedregs; // [esp+24h] [ebp+0h] BYREF

  v11 = 0;
  v16 = a3;
  v17 = a2;
  v18 = a1;
  unknown_libname_44(a4);
  v10 = &savedregs;
  v9[1] = (unsigned int)&loc_4DC111;
  v9[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v9);
  v13 = nullptr;
  if ( *(_DWORD *)(*(_DWORD *)(v18 + 1036) + 8) - 1 >= 0 )
  {
    v12 = *(_DWORD *)(*(_DWORD *)(v18 + 1036) + 8);
    v15 = 0;
    while ( 1 )
    {
      v4 = (_DWORD *)Classes::TList::Get(*(Classes::TList **)(v18 + 1036), v15);
      if ( *v4 == v16 )
      {
        v5 = Classes::TList::Get(*(Classes::TList **)(v18 + 1036), v15);
        sub_4AC1AC(*off_4EA490, *(unsigned __int16 *)(v5 + 4), &v11);
        if ( unknown_libname_84(v11, a4) == 0 )
          break;
      }
      ++v15;
      if ( --v12 == 0 )
        goto LABEL_12;
    }
    v13 = (unsigned __int16 *)Classes::TList::Get(*(Classes::TList **)(v18 + 1036), v15);
    v14 = (_DWORD *)sub_4AC858(*off_4EA490, v17);
    if ( v14 != nullptr && v13 != nullptr && v14[190] == *(_DWORD *)(v18 + 760) )
    {
      v6 = v14[9] - *(_DWORD *)(v18 + 36);
      if ( (int)((HIDWORD(v6) ^ v6) - HIDWORD(v6)) <= 15 )
      {
        v7 = v14[10] - *(_DWORD *)(v18 + 40);
        if ( (int)((HIDWORD(v7) ^ v7) - HIDWORD(v7)) <= 15 )
          sub_4A2024((int)v14, v18, v13);
      }
    }
  }
LABEL_12:
  __writefsdword(0, v9[0]);
  v10 = (int *)&loc_4DC118;
  unknown_libname_34(&v11);
  return unknown_libname_34(&a4);
}


// ==================== [0x4DC120] sub_4DC120 (size: 394) ====================
int __fastcall sub_4DC120(int a1, int a2)
{
  int v2; // ecx
  int v3; // eax
  unsigned int v5[2]; // [esp-Ch] [ebp-A0h] BYREF
  int *v6; // [esp-4h] [ebp-98h]
  _DWORD v7[3]; // [esp+8h] [ebp-8Ch] BYREF
  int v8; // [esp+14h] [ebp-80h] BYREF
  _DWORD v9[12]; // [esp+18h] [ebp-7Ch] BYREF
  __int16 v10; // [esp+48h] [ebp-4Ch]
  __int16 v11; // [esp+4Ah] [ebp-4Ah]
  _BYTE v12[44]; // [esp+4Ch] [ebp-48h] BYREF
  int v13; // [esp+78h] [ebp-1Ch]
  const void *v14; // [esp+7Ch] [ebp-18h]
  int v15; // [esp+80h] [ebp-14h]
  int v16; // [esp+84h] [ebp-10h] BYREF
  int v17; // [esp+88h] [ebp-Ch]
  int v18; // [esp+8Ch] [ebp-8h]
  int v19; // [esp+90h] [ebp-4h]
  int savedregs; // [esp+94h] [ebp+0h] BYREF

  v8 = 0;
  v16 = 0;
  v18 = a2;
  v19 = a1;
  v6 = &savedregs;
  v5[1] = (unsigned int)&loc_4DC29D;
  v5[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v5);
  unknown_libname_34(&v16);
  if ( *(_DWORD *)(*(_DWORD *)(v19 + 1272) + 8) - 1 >= 0 )
  {
    v13 = *(_DWORD *)(*(_DWORD *)(v19 + 1272) + 8);
    v17 = 0;
    do
    {
      v15 = Classes::TList::Get(*(Classes::TList **)(v19 + 1272), v17);
      v14 = (const void *)sub_4AC2F8(*off_4EA490, *(unsigned __int16 *)(v15 + 4));
      if ( v14 != nullptr )
      {
        qmemcpy(v12, v14, sizeof(v12));
        sub_495974(*off_4EA324, v15, v12);
        qmemcpy(v9, v12, 44);
        v10 = *(_WORD *)(v15 + 6);
        v11 = *(_WORD *)(v15 + 8);
        v9[11] = *(_DWORD *)v15;
        sub_4565B0(v9, 52, &v8, v16);
        System::__linkproc__ LStrCatN(&v16, 3, v2);
      }
      ++v17;
      --v13;
    }
    while ( v13 != 0 );
  }
  sub_4546F8(v7, *(_WORD *)(*(_DWORD *)(v19 + 1272) + 8), 0);
  v3 = v19;
  *(_DWORD *)(v19 + 1360) = v7[0];
  *(_DWORD *)(v3 + 1364) = v7[1];
  *(_DWORD *)(v3 + 1368) = v7[2];
  sub_4CAB38(v19, (void *)(v19 + 1360), v16);
  __writefsdword(0, v5[0]);
  v6 = (int *)&loc_4DC2A4;
  unknown_libname_34(&v8);
  return unknown_libname_34(&v16);
}


// ==================== [0x4DC2B8] sub_4DC2B8 (size: 842) ====================
int __fastcall sub_4DC2B8(int a1, int a2, int a3, int a4)
{
  _DWORD *v4; // eax
  int v5; // eax
  __int64 v6; // rax
  __int64 v7; // rax
  int v8; // ecx
  int v9; // ecx
  int v10; // ecx
  unsigned int v12[2]; // [esp-Ch] [ebp-80h] BYREF
  int *v13; // [esp-4h] [ebp-78h]
  int v14; // [esp+4h] [ebp-70h] BYREF
  _DWORD v15[4]; // [esp+8h] [ebp-6Ch] BYREF
  _BYTE v16[20]; // [esp+18h] [ebp-5Ch] BYREF
  _BYTE v17[20]; // [esp+2Ch] [ebp-48h] BYREF
  int v18; // [esp+40h] [ebp-34h] BYREF
  int v19; // [esp+44h] [ebp-30h] BYREF
  int v20; // [esp+48h] [ebp-2Ch] BYREF
  char v21; // [esp+4Ch] [ebp-28h]
  int v22; // [esp+50h] [ebp-24h] BYREF
  int v23; // [esp+54h] [ebp-20h]
  char v24; // [esp+5Bh] [ebp-19h]
  void *v25; // [esp+5Ch] [ebp-18h]
  _DWORD *v26; // [esp+60h] [ebp-14h]
  int v27; // [esp+64h] [ebp-10h]
  int v28; // [esp+68h] [ebp-Ch]
  int v29; // [esp+6Ch] [ebp-8h]
  int v30; // [esp+70h] [ebp-4h]
  int savedregs; // [esp+74h] [ebp+0h] BYREF

  v19 = 0;
  v18 = 0;
  memset(v15, 0, sizeof(v15));
  v14 = 0;
  v20 = 0;
  v22 = 0;
  v28 = a3;
  v29 = a2;
  v30 = a1;
  unknown_libname_44(a4);
  v13 = &savedregs;
  v12[1] = (unsigned int)&loc_4DC5F4;
  v12[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v12);
  v25 = nullptr;
  v24 = 0;
  if ( System::__linkproc__ LStrPos(&str___99[1], a4) >= 0 )
  {
    v21 = 32;
    sub_453DBC(&v22, 0);
  }
  if ( *(_DWORD *)(v30 + 1476) == 1 )
  {
    sub_4BB124(v30, dword_4DC644, 0);
  }
  else if ( *(_DWORD *)(*(_DWORD *)(v30 + 1036) + 8) - 1 >= 0 )
  {
    v23 = *(_DWORD *)(*(_DWORD *)(v30 + 1036) + 8);
    v27 = 0;
    while ( 1 )
    {
      v4 = (_DWORD *)Classes::TList::Get(*(Classes::TList **)(v30 + 1036), v27);
      if ( *v4 == v28 )
      {
        v5 = Classes::TList::Get(*(Classes::TList **)(v30 + 1036), v27);
        sub_4AC1AC(*off_4EA490, *(unsigned __int16 *)(v5 + 4), &v20);
        if ( unknown_libname_84(v20, a4) == 0 )
          break;
      }
      ++v27;
      if ( --v23 == 0 )
        goto LABEL_20;
    }
    v25 = (void *)Classes::TList::Get(*(Classes::TList **)(v30 + 1036), v27);
    v26 = (_DWORD *)sub_4AC858(*off_4EA490, v29);
    if ( v26 != nullptr && v25 != nullptr && v26[190] == *(_DWORD *)(v30 + 760) )
    {
      v6 = v26[9] - *(_DWORD *)(v30 + 36);
      if ( (int)((HIDWORD(v6) ^ v6) - HIDWORD(v6)) <= 15 )
      {
        v7 = v26[10] - *(_DWORD *)(v30 + 40);
        if ( (int)((HIDWORD(v7) ^ v7) - HIDWORD(v7)) <= 15 )
        {
          if ( *(int *)(*(_DWORD *)(v30 + 1272) + 8) >= 50 )
          {
            sub_4CAD6C(0, 0, 0, 0);
          }
          else
          {
            Classes::TList::Add(*(Classes::TList **)(v30 + 1272), v25);
            Classes::TList::Delete(*(Classes::TList **)(v30 + 1036), v27);
            sub_4C49BC(v30);
            sub_4CAD6C(0, 0, 0, 0);
            unknown_libname_16(v17, &dword_4DC610);
            LOBYTE(v8) = 18;
            System::__linkproc__ PStrNCat(v17, v30 + 4, v8);
            unknown_libname_16(v16, v17);
            LOBYTE(v9) = 19;
            System::__linkproc__ PStrNCat(v16, dword_4DC614, v9);
            unknown_libname_41();
            Sysutils::IntToStr(*(_DWORD *)(v30 + 36));
            Sysutils::IntToStr(*(_DWORD *)(v30 + 40));
            unknown_libname_41();
            sub_4AC1AC(*off_4EA490, *((unsigned __int16 *)v25 + 2), v15);
            Sysutils::IntToStr(*(_DWORD *)v25);
            System::__linkproc__ LStrCatN(&v19, 13, v10);
            sub_4E42F8(v19);
          }
          v24 = 1;
        }
      }
    }
  }
LABEL_20:
  if ( v24 == 0 )
    sub_4CAD6C(0, 0, 0, 0);
  __writefsdword(0, v12[0]);
  v13 = (int *)&loc_4DC5FB;
  unknown_libname_35(&v14, 5);
  unknown_libname_35(&v18, 3);
  unknown_libname_34(&v22);
  return unknown_libname_34(&a4);
}


// ==================== [0x4DC664] sub_4DC664 (size: 828) ====================
int __fastcall sub_4DC664(int a1, int a2, int a3, int a4)
{
  _DWORD *v4; // eax
  int v5; // eax
  int v6; // eax
  __int64 v7; // rax
  __int64 v8; // rax
  int v9; // ecx
  int v10; // ecx
  int v11; // ecx
  unsigned int v13[2]; // [esp-Ch] [ebp-78h] BYREF
  int *v14; // [esp-4h] [ebp-70h]
  int v15; // [esp+4h] [ebp-68h] BYREF
  _DWORD v16[4]; // [esp+8h] [ebp-64h] BYREF
  _BYTE v17[20]; // [esp+18h] [ebp-54h] BYREF
  _BYTE v18[20]; // [esp+2Ch] [ebp-40h] BYREF
  int v19; // [esp+40h] [ebp-2Ch] BYREF
  int v20; // [esp+44h] [ebp-28h] BYREF
  int v21; // [esp+48h] [ebp-24h] BYREF
  int v22; // [esp+4Ch] [ebp-20h]
  _DWORD *v23; // [esp+50h] [ebp-1Ch]
  unsigned __int16 *v24; // [esp+54h] [ebp-18h]
  char v25; // [esp+5Bh] [ebp-11h]
  int v26; // [esp+5Ch] [ebp-10h]
  int v27; // [esp+60h] [ebp-Ch]
  int v28; // [esp+64h] [ebp-8h]
  int v29; // [esp+68h] [ebp-4h]
  int savedregs; // [esp+6Ch] [ebp+0h] BYREF

  v20 = 0;
  v19 = 0;
  memset(v16, 0, sizeof(v16));
  v15 = 0;
  v21 = 0;
  v27 = a3;
  v28 = a2;
  v29 = a1;
  unknown_libname_44(a4);
  v14 = &savedregs;
  v13[1] = (unsigned int)&loc_4DC992;
  v13[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v13);
  v25 = 0;
  if ( *(_DWORD *)(v29 + 1476) == 1 )
  {
    sub_4BB124(v29, dword_4DC9F0, 0);
  }
  else if ( *(_DWORD *)(*(_DWORD *)(v29 + 1272) + 8) - 1 >= 0 )
  {
    v22 = *(_DWORD *)(*(_DWORD *)(v29 + 1272) + 8);
    v26 = 0;
    while ( 1 )
    {
      v4 = (_DWORD *)Classes::TList::Get(*(Classes::TList **)(v29 + 1272), v26);
      if ( *v4 == v27 )
      {
        v5 = Classes::TList::Get(*(Classes::TList **)(v29 + 1272), v26);
        sub_4AC1AC(*off_4EA490, *(unsigned __int16 *)(v5 + 4), &v21);
        if ( unknown_libname_84(v21, a4) == 0 )
          break;
      }
      ++v26;
      if ( --v22 == 0 )
        goto LABEL_20;
    }
    v24 = (unsigned __int16 *)Classes::TList::Get(*(Classes::TList **)(v29 + 1272), v26);
    v23 = (_DWORD *)sub_4AC858(*off_4EA490, v28);
    if ( v23 != nullptr && v24 != nullptr )
    {
      v6 = sub_4AC2B0(*off_4EA490, v24[2]);
      if ( (unsigned __int8)sub_4C4A78(v29, v6) != 0 )
      {
        if ( v23[190] == *(_DWORD *)(v29 + 760) )
        {
          v7 = v23[9] - *(_DWORD *)(v29 + 36);
          if ( (int)((HIDWORD(v7) ^ v7) - HIDWORD(v7)) <= 15 )
          {
            v8 = v23[10] - *(_DWORD *)(v29 + 40);
            if ( (int)((HIDWORD(v8) ^ v8) - HIDWORD(v8)) <= 15 )
            {
              if ( (unsigned __int8)sub_4C4DB8(v29, v24) != 0 )
              {
                sub_4D0824(v29, v24);
                Classes::TList::Delete(*(Classes::TList **)(v29 + 1272), v26);
                sub_4CAD6C(0, 0, 0, 0);
                unknown_libname_16(v18, &dword_4DC9A0);
                LOBYTE(v9) = 18;
                System::__linkproc__ PStrNCat(v18, v29 + 4, v9);
                unknown_libname_16(v17, v18);
                LOBYTE(v10) = 19;
                System::__linkproc__ PStrNCat(v17, dword_4DC9A4, v10);
                unknown_libname_41();
                Sysutils::IntToStr(*(_DWORD *)(v29 + 36));
                Sysutils::IntToStr(*(_DWORD *)(v29 + 40));
                unknown_libname_41();
                sub_4AC1AC(*off_4EA490, v24[2], v16);
                Sysutils::IntToStr(*(_DWORD *)v24);
                System::__linkproc__ LStrCatN(&v20, 13, v11);
                sub_4E42F8(v20);
              }
              else
              {
                sub_4CAD6C(0, 0, 0, 0);
              }
              v25 = 1;
            }
          }
        }
      }
      else
      {
        sub_4BB124(v29, dword_4DC9D4, 0);
      }
    }
  }
LABEL_20:
  if ( v25 == 0 )
    sub_4CAD6C(0, 0, 0, 0);
  __writefsdword(0, v13[0]);
  v14 = (int *)&loc_4DC999;
  unknown_libname_35(&v15, 5);
  unknown_libname_35(&v19, 3);
  return unknown_libname_34(&a4);
}


// ==================== [0x4DCCB4] @Idmessage@TIdMIMEBoundary@Push$qqr17System@AnsiString_0 (size: 403) ====================
int __fastcall Idmessage::TIdMIMEBoundary::Push(int a1, int a2)
{
  unsigned int v3[2]; // [esp-Ch] [ebp-20h] BYREF
  int *v4; // [esp-4h] [ebp-18h]
  int v5; // [esp+4h] [ebp-10h] BYREF
  int v6; // [esp+8h] [ebp-Ch]
  int v7; // [esp+Ch] [ebp-8h] BYREF
  int v8; // [esp+10h] [ebp-4h]
  int savedregs; // [esp+14h] [ebp+0h] BYREF

  v5 = 0;
  v7 = a2;
  v8 = a1;
  unknown_libname_44(a2);
  v4 = &savedregs;
  v3[1] = (unsigned int)&loc_4DCE3B;
  v3[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v3);
  v6 = sub_4AE640(*off_4EA490);
  if ( *(_DWORD *)(v8 + 628) != 0 || v6 == 0 || v6 == v8 || *(_DWORD *)(v6 + 628) != 0 || *(_BYTE *)(v6 + 176) == 0 )
  {
    sub_4CAD6C(0, 0, 0, 0);
  }
  else
  {
    (*(void (__fastcall **)(_DWORD))(**(_DWORD **)(v8 + 632) + 64))(*(_DWORD *)(v8 + 632));
    unknown_libname_41();
    (*(void (__fastcall **)(_DWORD, int, int))(**(_DWORD **)(v8 + 632) + 56))(*(_DWORD *)(v8 + 632), v5, v8);
    (*(void (__fastcall **)(_DWORD, int, int))(**(_DWORD **)(v8 + 632) + 56))(*(_DWORD *)(v8 + 632), v7, v6);
    sub_4C3AE4(v8, v8);
    sub_4C3AE4(v6, v8);
    *(_BYTE *)(v8 + 176) = 1;
    sub_4CAD6C(0, 0, 0, 0);
    sub_4DCBA4(v8);
  }
  __writefsdword(0, v3[0]);
  v4 = (int *)&loc_4DCE42;
  unknown_libname_34(&v5);
  return unknown_libname_34(&v7);
}


// ==================== [0x4DCE48] sub_4DCE48 (size: 367) ====================
int __fastcall sub_4DCE48(int a1, int a2)
{
  unsigned int v3[2]; // [esp-Ch] [ebp-1Ch] BYREF
  int *v4; // [esp-4h] [ebp-14h]
  int v5; // [esp+4h] [ebp-Ch]
  int v6; // [esp+8h] [ebp-8h] BYREF
  int v7; // [esp+Ch] [ebp-4h]
  int savedregs; // [esp+10h] [ebp+0h] BYREF

  v6 = a2;
  v7 = a1;
  unknown_libname_44(a2);
  v4 = &savedregs;
  v3[1] = (unsigned int)&loc_4DCFAB;
  v3[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v3);
  v5 = sub_4AE640(*off_4EA490);
  if ( *(_DWORD *)(v7 + 628) == v7
    && (*(int (__fastcall **)(_DWORD))(**(_DWORD **)(v7 + 632) + 20))(*(_DWORD *)(v7 + 632)) < 11
    && v5 != 0
    && v5 != v7
    && *(_DWORD *)(v5 + 628) == 0
    && *(_BYTE *)(v5 + 176) != 0 )
  {
    (*(void (__fastcall **)(_DWORD, int, int))(**(_DWORD **)(v7 + 632) + 56))(*(_DWORD *)(v7 + 632), v6, v5);
    sub_4C3AE4(v5, v7);
    sub_4CAD6C(0, 0, 0, 0);
    sub_4DCBA4(v7);
  }
  else
  {
    sub_4CAD6C(0, 0, 0, 0);
  }
  __writefsdword(0, v3[0]);
  v4 = (int *)&loc_4DCFB2;
  return unknown_libname_34(&v6);
}


// ==================== [0x4DD0A8] sub_4DD0A8 (size: 406) ====================
int __fastcall sub_4DD0A8(_BYTE *a1, int a2)
{
  int v2; // eax
  int v3; // ecx
  int v4; // ecx
  unsigned int v6[2]; // [esp-Ch] [ebp-44h] BYREF
  int *v7; // [esp-4h] [ebp-3Ch]
  int v8; // [esp+0h] [ebp-38h] BYREF
  _BYTE v9[36]; // [esp+4h] [ebp-34h] BYREF
  _DWORD v10[2]; // [esp+28h] [ebp-10h] BYREF
  _BYTE *v11; // [esp+30h] [ebp-8h]
  _BYTE *v12; // [esp+34h] [ebp-4h]
  int savedregs; // [esp+38h] [ebp+0h] BYREF

  v8 = 0;
  v10[0] = 0;
  v10[1] = a2;
  v12 = a1;
  unknown_libname_44(a2);
  v7 = &savedregs;
  v6[1] = (unsigned int)&loc_4DD233;
  v6[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v6);
  if ( v12[791] == 0 )
  {
    v11 = (_BYTE *)sub_4C368C(v12);
    if ( v11 == nullptr || v11 == v12 || (_BYTE *)(v2 = sub_4C368C(v11)) != v12 || v11[791] != 0 )
    {
      sub_4CAD6C(0, 0, 0, 0);
    }
    else if ( v11[570] == 0 )
    {
      if ( v11[639] != 0 )
      {
        unknown_libname_16(v9, v12 + 21);
        LOBYTE(v3) = 32;
        System::__linkproc__ PStrNCat(v9, dword_4DD240, v3);
        unknown_libname_41();
        sub_4BB124(v11, v10[0], 1);
        unknown_libname_16(v9, v11 + 21);
        LOBYTE(v4) = 32;
        System::__linkproc__ PStrNCat(v9, dword_4DD240, v4);
        unknown_libname_41();
        sub_4BB124(v12, v8, 1);
        sub_4DD300(v12, v11);
        sub_4DD300(v11, v12);
      }
      else
      {
        sub_4BB124(v12, &str_____________1[1], 1);
      }
    }
  }
  __writefsdword(0, v6[0]);
  v7 = (int *)&loc_4DD23A;
  unknown_libname_34(&v8);
  return unknown_libname_35(v10, 2);
}


// ==================== [0x4DD464] sub_4DD464 (size: 364) ====================
int __fastcall sub_4DD464(_DWORD *a1, const void *a2)
{
  _DWORD *v2; // eax
  unsigned int v4[2]; // [esp-Ch] [ebp-A4h] BYREF
  int *v5; // [esp-4h] [ebp-9Ch]
  int v6; // [esp+8h] [ebp-90h] BYREF
  _DWORD v7[3]; // [esp+Ch] [ebp-8Ch] BYREF
  _BYTE v8[44]; // [esp+18h] [ebp-80h] BYREF
  _DWORD v9[12]; // [esp+44h] [ebp-54h] BYREF
  __int16 v10; // [esp+74h] [ebp-24h]
  __int16 v11; // [esp+76h] [ebp-22h]
  _DWORD v12[6]; // [esp+78h] [ebp-20h] BYREF
  const void *v13; // [esp+90h] [ebp-8h]
  _DWORD *v14; // [esp+94h] [ebp-4h]
  int savedregs; // [esp+98h] [ebp+0h] BYREF

  v6 = 0;
  qmemcpy(v12, a2, sizeof(v12));
  v14 = a1;
  v5 = &savedregs;
  v4[1] = (unsigned int)&loc_4DD5C3;
  v4[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v4);
  sub_4CAD6C(0, 0, 0, 0);
  if ( v14[199] != 0 )
  {
    v13 = (const void *)sub_4AC2F8(*off_4EA490, LOWORD(v12[1]));
    if ( v13 != nullptr )
    {
      qmemcpy(v8, v13, sizeof(v8));
      sub_495974(*off_4EA324, v12, v8);
      qmemcpy(v9, v8, 44);
      v9[11] = v12[0];
      v10 = HIWORD(v12[1]);
      v11 = v12[2];
    }
    sub_4546F8(v7, 1, 0);
    v2 = v14;
    v14[340] = v7[0];
    v2[341] = v7[1];
    v2[342] = v7[2];
    sub_4565B0(v9, 52, &v6, v4[0]);
    sub_4CAB38(v14[199], v14 + 340, v6);
    *(_DWORD *)(v14[199] + 792) = GetTickCount();
    v14[198] = GetTickCount();
  }
  __writefsdword(0, v4[0]);
  v5 = (int *)&loc_4DD5CA;
  return unknown_libname_34(&v6);
}


// ==================== [0x4DD5D0] sub_4DD5D0 (size: 306) ====================
int __fastcall sub_4DD5D0(_DWORD *a1, const void *a2)
{
  _DWORD *v2; // eax
  unsigned int v4[2]; // [esp-Ch] [ebp-78h] BYREF
  int *v5; // [esp-4h] [ebp-70h]
  int v6; // [esp+8h] [ebp-64h] BYREF
  _DWORD v7[3]; // [esp+Ch] [ebp-60h] BYREF
  _DWORD v8[12]; // [esp+18h] [ebp-54h] BYREF
  __int16 v9; // [esp+48h] [ebp-24h]
  __int16 v10; // [esp+4Ah] [ebp-22h]
  _DWORD v11[6]; // [esp+4Ch] [ebp-20h] BYREF
  const void *v12; // [esp+64h] [ebp-8h]
  _DWORD *v13; // [esp+68h] [ebp-4h]
  int savedregs; // [esp+6Ch] [ebp+0h] BYREF

  v6 = 0;
  qmemcpy(v11, a2, sizeof(v11));
  v13 = a1;
  v5 = &savedregs;
  v4[1] = (unsigned int)&loc_4DD6F5;
  v4[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v4);
  sub_4CAD6C(0, 0, 0, 0);
  if ( v13[199] != 0 )
  {
    v12 = (const void *)sub_4AC2F8(*off_4EA490, LOWORD(v11[1]));
    if ( v12 != nullptr )
    {
      qmemcpy(v8, v12, 0x2Cu);
      v8[11] = v11[0];
      v9 = HIWORD(v11[1]);
      v10 = v11[2];
    }
    sub_4546F8(v7, 1, 0);
    v2 = v13;
    v13[340] = v7[0];
    v2[341] = v7[1];
    v2[342] = v7[2];
    sub_4565B0(v8, 52, &v6, v4[0]);
    sub_4CAB38(v13[199], v13 + 340, v6);
    *(_DWORD *)(v13[199] + 792) = GetTickCount();
    v13[198] = GetTickCount();
  }
  __writefsdword(0, v4[0]);
  v5 = (int *)&loc_4DD6FC;
  return unknown_libname_34(&v6);
}


// ==================== [0x4DD79C] sub_4DD79C (size: 429) ====================
int __fastcall sub_4DD79C(_DWORD *a1, int a2, int a3)
{
  _DWORD *v3; // eax
  int v4; // eax
  void *v5; // eax
  const void *v6; // eax
  unsigned int v8[2]; // [esp-Ch] [ebp-34h] BYREF
  int *v9; // [esp-4h] [ebp-2Ch]
  int v10; // [esp+4h] [ebp-24h] BYREF
  char v11; // [esp+8h] [ebp-20h]
  int v12; // [esp+Ch] [ebp-1Ch] BYREF
  int v13; // [esp+10h] [ebp-18h]
  char v14; // [esp+17h] [ebp-11h]
  int v15; // [esp+18h] [ebp-10h]
  int v16; // [esp+1Ch] [ebp-Ch] BYREF
  int v17; // [esp+20h] [ebp-8h]
  _DWORD *v18; // [esp+24h] [ebp-4h]
  int savedregs; // [esp+28h] [ebp+0h] BYREF

  v10 = 0;
  v12 = 0;
  v16 = a3;
  v17 = a2;
  v18 = a1;
  unknown_libname_44(a3);
  v9 = &savedregs;
  v8[1] = (unsigned int)&loc_4DD93D;
  v8[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v8);
  if ( v18[199] != 0 )
  {
    if ( System::__linkproc__ LStrPos(&str___101[1], v16) >= 0 )
    {
      v11 = 32;
      sub_453DBC(&v12, 0);
    }
    v14 = 0;
    if ( *(_BYTE *)(v18[199] + 1048) == 0 && *(_DWORD *)(v18[259] + 8) - 1 >= 0 )
    {
      v13 = *(_DWORD *)(v18[259] + 8);
      v15 = 0;
      while ( 1 )
      {
        v3 = (_DWORD *)Classes::TList::Get((Classes::TList *)v18[259], v15);
        if ( *v3 == v17 )
        {
          v4 = Classes::TList::Get((Classes::TList *)v18[259], v15);
          sub_4AC1AC(*off_4EA490, *(unsigned __int16 *)(v4 + 4), &v10);
          if ( unknown_libname_84(v10, v16) == 0 && *(int *)(v18[260] + 8) < 12 )
            break;
        }
        ++v15;
        if ( --v13 == 0 )
          goto LABEL_12;
      }
      v5 = (void *)Classes::TList::Get((Classes::TList *)v18[259], v15);
      Classes::TList::Add((Classes::TList *)v18[260], v5);
      v6 = (const void *)Classes::TList::Get((Classes::TList *)v18[259], v15);
      sub_4DD464(v18, v6);
      Classes::TList::Delete((Classes::TList *)v18[259], v15);
      v14 = 1;
    }
LABEL_12:
    if ( v14 == 0 )
      sub_4CAD6C(0, 0, 0, 0);
  }
  __writefsdword(0, v8[0]);
  v9 = (int *)&loc_4DD944;
  unknown_libname_34(&v10);
  unknown_libname_34(&v12);
  return unknown_libname_34(&v16);
}


// ==================== [0x4DD958] sub_4DD958 (size: 414) ====================
int __fastcall sub_4DD958(_DWORD *a1, int a2, int a3)
{
  _DWORD *v3; // eax
  int v4; // eax
  void *v5; // eax
  const void *v6; // eax
  unsigned int v8[2]; // [esp-Ch] [ebp-34h] BYREF
  int *v9; // [esp-4h] [ebp-2Ch]
  int v10; // [esp+4h] [ebp-24h] BYREF
  char v11; // [esp+8h] [ebp-20h]
  int v12; // [esp+Ch] [ebp-1Ch] BYREF
  int v13; // [esp+10h] [ebp-18h]
  char v14; // [esp+17h] [ebp-11h]
  int v15; // [esp+18h] [ebp-10h]
  int v16; // [esp+1Ch] [ebp-Ch] BYREF
  int v17; // [esp+20h] [ebp-8h]
  _DWORD *v18; // [esp+24h] [ebp-4h]
  int savedregs; // [esp+28h] [ebp+0h] BYREF

  v10 = 0;
  v12 = 0;
  v16 = a3;
  v17 = a2;
  v18 = a1;
  unknown_libname_44(a3);
  v9 = &savedregs;
  v8[1] = (unsigned int)&loc_4DDAEA;
  v8[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v8);
  if ( v18[199] != 0 )
  {
    if ( System::__linkproc__ LStrPos(&str___102[1], v16) >= 0 )
    {
      v11 = 32;
      sub_453DBC(&v12, 0);
    }
    v14 = 0;
    if ( *(_BYTE *)(v18[199] + 1048) == 0 && *(_DWORD *)(v18[260] + 8) - 1 >= 0 )
    {
      v13 = *(_DWORD *)(v18[260] + 8);
      v15 = 0;
      while ( 1 )
      {
        v3 = (_DWORD *)Classes::TList::Get((Classes::TList *)v18[260], v15);
        if ( *v3 == v17 )
        {
          v4 = Classes::TList::Get((Classes::TList *)v18[260], v15);
          sub_4AC1AC(*off_4EA490, *(unsigned __int16 *)(v4 + 4), &v10);
          if ( unknown_libname_84(v10, v16) == 0 )
            break;
        }
        ++v15;
        if ( --v13 == 0 )
          goto LABEL_11;
      }
      v5 = (void *)Classes::TList::Get((Classes::TList *)v18[260], v15);
      Classes::TList::Add((Classes::TList *)v18[259], v5);
      v6 = (const void *)Classes::TList::Get((Classes::TList *)v18[260], v15);
      sub_4DD5D0(v18, v6);
      Classes::TList::Delete((Classes::TList *)v18[260], v15);
      v14 = 1;
    }
LABEL_11:
    if ( v14 == 0 )
      sub_4CAD6C(0, 0, 0, 0);
  }
  __writefsdword(0, v8[0]);
  v9 = (int *)&loc_4DDAF1;
  unknown_libname_34(&v10);
  unknown_libname_34(&v12);
  return unknown_libname_34(&v16);
}


// ==================== [0x4DDB04] sub_4DDB04 (size: 391) ====================
signed int __fastcall sub_4DDB04(_DWORD *a1, int a2)
{
  __int16 v2; // ax
  signed int result; // eax
  __int16 v4; // ax
  __int16 v5; // ax
  __int16 v6; // [esp-4h] [ebp-10h]
  __int16 v7; // [esp-4h] [ebp-10h]
  __int16 v8; // [esp-4h] [ebp-10h]
  char v9; // [esp+3h] [ebp-9h]

  if ( a2 >= 0 )
  {
    v9 = 0;
    result = sub_4C368C(a1);
    if ( result == a1[199] )
    {
      result = (signed int)a1;
      if ( a1[199] != 0 )
      {
        result = a1[199];
        if ( *(_BYTE *)(result + 1048) == 0 )
        {
          result = a1[261] + a1[12];
          if ( result >= a2 )
          {
            a1[12] = a1[261] + a1[12] - a2;
            a1[261] = a2;
            v7 = *((_WORD *)a1 + 24);
            v4 = sub_407B24(a1[12]);
            sub_4CAD6C(0, 0, v4, v7);
            if ( a1[199] != 0 )
            {
              sub_4CAD6C(0, 0, 0, 0);
              *(_DWORD *)(a1[199] + 792) = GetTickCount();
            }
            v9 = 1;
            result = GetTickCount();
            a1[198] = result;
          }
        }
      }
    }
    if ( v9 == 0 )
    {
      v8 = *((_WORD *)a1 + 24);
      v5 = sub_407B24(a1[12]);
      return sub_4CAD6C(0, 0, v5, v8);
    }
  }
  else
  {
    v6 = *((_WORD *)a1 + 24);
    v2 = sub_407B24(a1[12]);
    return sub_4CAD6C(0, 0, v2, v6);
  }
  return result;
}


// ==================== [0x4DDC8C] sub_4DDC8C (size: 2061) ====================
int __fastcall sub_4DDC8C(int a1)
{
  int v1; // ecx
  int v2; // ecx
  int v3; // ecx
  int v4; // ecx
  int v5; // ecx
  int v6; // ecx
  int v7; // ecx
  int v8; // ecx
  int v9; // ecx
  int v10; // ecx
  int v11; // ecx
  int v12; // ecx
  unsigned int v14[2]; // [esp-Ch] [ebp-C8h] BYREF
  int *v15; // [esp-4h] [ebp-C0h]
  int v16; // [esp+0h] [ebp-BCh] BYREF
  int v17; // [esp+18h] [ebp-A4h] BYREF
  int v18; // [esp+24h] [ebp-98h] BYREF
  int v19; // [esp+38h] [ebp-84h] BYREF
  int v20; // [esp+54h] [ebp-68h] BYREF
  int v21; // [esp+60h] [ebp-5Ch] BYREF
  _BYTE v22[20]; // [esp+70h] [ebp-4Ch] BYREF
  _BYTE v23[20]; // [esp+84h] [ebp-38h] BYREF
  int v24; // [esp+98h] [ebp-24h] BYREF
  int v25; // [esp+9Ch] [ebp-20h] BYREF
  int v26; // [esp+A0h] [ebp-1Ch]
  int v27; // [esp+A4h] [ebp-18h]
  bool v28; // [esp+ABh] [ebp-11h]
  int v29; // [esp+ACh] [ebp-10h]
  unsigned __int16 *v30; // [esp+B0h] [ebp-Ch]
  int v31; // [esp+B4h] [ebp-8h]
  int v32; // [esp+B8h] [ebp-4h]
  int savedregs; // [esp+BCh] [ebp+0h] BYREF

  v32 = a1;
  v15 = &savedregs;
  v14[1] = (unsigned int)&loc_4DE48E;
  v14[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v14);
  *(_BYTE *)(a1 + 1048) = 1;
  if ( *(_DWORD *)(v32 + 796) != 0 )
  {
    if ( GetTickCount() - *(_DWORD *)(v32 + 792) >= 0x3E8
      && GetTickCount() - *(_DWORD *)(*(_DWORD *)(v32 + 796) + 792) >= 0x3E8 )
    {
      if ( *(_BYTE *)(*(_DWORD *)(v32 + 796) + 1048) != 0 )
      {
        v28 = 46 - *(_DWORD *)(*(_DWORD *)(v32 + 1036) + 8) >= *(_DWORD *)(*(_DWORD *)(*(_DWORD *)(v32 + 796) + 1040) + 8);
        if ( *(_DWORD *)(v32 + 616) - *(_DWORD *)(v32 + 48) < *(_DWORD *)(*(_DWORD *)(v32 + 796) + 1044) )
          v28 = false;
        if ( 46 - *(_DWORD *)(*(_DWORD *)(*(_DWORD *)(v32 + 796) + 1036) + 8) < *(_DWORD *)(*(_DWORD *)(v32 + 1040) + 8) )
          v28 = false;
        if ( *(_DWORD *)(*(_DWORD *)(v32 + 796) + 616) - *(_DWORD *)(*(_DWORD *)(v32 + 796) + 48) < *(_DWORD *)(v32 + 1044) )
          v28 = false;
        if ( v28 )
        {
          if ( *(_DWORD *)(*(_DWORD *)(v32 + 1040) + 8) - 1 >= 0 )
          {
            v27 = *(_DWORD *)(*(_DWORD *)(v32 + 1040) + 8);
            v31 = 0;
            do
            {
              v30 = (unsigned __int16 *)Classes::TList::Get(*(Classes::TList **)(v32 + 1040), v31);
              sub_4C4DB8(*(_DWORD *)(v32 + 796), v30);
              sub_4D0824(*(_DWORD *)(v32 + 796), v30);
              v29 = sub_4AC2F8(*off_4EA490, v30[2]);
              if ( v29 != 0 && (unsigned __int8)sub_4B2FA8(*(unsigned __int8 *)(v29 + 15)) == 0 )
              {
                unknown_libname_16(v23, &dword_4DE4B8);
                LOBYTE(v1) = 18;
                System::__linkproc__ PStrNCat(v23, v32 + 4, v1);
                unknown_libname_16(v22, v23);
                LOBYTE(v2) = 19;
                System::__linkproc__ PStrNCat(v22, dword_4DE4BC, v2);
                unknown_libname_41();
                Sysutils::IntToStr(*(_DWORD *)(v32 + 36));
                Sysutils::IntToStr(*(_DWORD *)(v32 + 40));
                unknown_libname_41();
                sub_4AC1AC(*off_4EA490, v30[2], &v21);
                Sysutils::IntToStr(*(_DWORD *)v30);
                unknown_libname_41();
                System::__linkproc__ LStrCatN(&v25, 13, v3);
                sub_4E42F8(v25);
              }
              ++v31;
              --v27;
            }
            while ( v27 != 0 );
          }
          if ( *(int *)(v32 + 1044) > 0 )
          {
            *(_DWORD *)(*(_DWORD *)(v32 + 796) + 48) += *(_DWORD *)(v32 + 1044);
            sub_4C49F4(*(_DWORD *)(v32 + 796));
            unknown_libname_16(v23, &dword_4DE4B8);
            LOBYTE(v4) = 18;
            System::__linkproc__ PStrNCat(v23, v32 + 4, v4);
            unknown_libname_16(v22, v23);
            LOBYTE(v5) = 19;
            System::__linkproc__ PStrNCat(v22, dword_4DE4BC, v5);
            unknown_libname_41();
            Sysutils::IntToStr(*(_DWORD *)(v32 + 36));
            Sysutils::IntToStr(*(_DWORD *)(v32 + 40));
            unknown_libname_41();
            Sysutils::IntToStr(*(_DWORD *)(v32 + 1044));
            unknown_libname_41();
            System::__linkproc__ LStrCatN(&v20, 12, v6);
            sub_4E42F8(v20);
          }
          if ( *(_DWORD *)(*(_DWORD *)(*(_DWORD *)(v32 + 796) + 1040) + 8) - 1 >= 0 )
          {
            v27 = *(_DWORD *)(*(_DWORD *)(*(_DWORD *)(v32 + 796) + 1040) + 8);
            v31 = 0;
            do
            {
              v30 = (unsigned __int16 *)Classes::TList::Get(*(Classes::TList **)(*(_DWORD *)(v32 + 796) + 1040), v31);
              sub_4C4DB8(v32, v30);
              sub_4D0824(v32, v30);
              v29 = sub_4AC2F8(*off_4EA490, v30[2]);
              if ( v29 != 0 && (unsigned __int8)sub_4B2FA8(*(unsigned __int8 *)(v29 + 15)) == 0 )
              {
                unknown_libname_16(v23, &dword_4DE4B8);
                LOBYTE(v7) = 18;
                System::__linkproc__ PStrNCat(v23, *(_DWORD *)(v32 + 796) + 4, v7);
                unknown_libname_16(v22, v23);
                LOBYTE(v8) = 19;
                System::__linkproc__ PStrNCat(v22, dword_4DE4BC, v8);
                unknown_libname_41();
                Sysutils::IntToStr(*(_DWORD *)(*(_DWORD *)(v32 + 796) + 36));
                Sysutils::IntToStr(*(_DWORD *)(*(_DWORD *)(v32 + 796) + 40));
                unknown_libname_41();
                sub_4AC1AC(*off_4EA490, v30[2], &v18);
                Sysutils::IntToStr(*(_DWORD *)v30);
                unknown_libname_41();
                System::__linkproc__ LStrCatN(&v19, 13, v9);
                sub_4E42F8(v19);
              }
              ++v31;
              --v27;
            }
            while ( v27 != 0 );
          }
          if ( *(int *)(*(_DWORD *)(v32 + 796) + 1044) > 0 )
          {
            *(_DWORD *)(v32 + 48) += *(_DWORD *)(*(_DWORD *)(v32 + 796) + 1044);
            sub_4C49F4(v32);
            unknown_libname_16(v23, &dword_4DE4B8);
            LOBYTE(v10) = 18;
            System::__linkproc__ PStrNCat(v23, *(_DWORD *)(v32 + 796) + 4, v10);
            unknown_libname_16(v22, v23);
            LOBYTE(v11) = 19;
            System::__linkproc__ PStrNCat(v22, dword_4DE4BC, v11);
            unknown_libname_41();
            Sysutils::IntToStr(*(_DWORD *)(*(_DWORD *)(v32 + 796) + 36));
            Sysutils::IntToStr(*(_DWORD *)(*(_DWORD *)(v32 + 796) + 40));
            unknown_libname_41();
            Sysutils::IntToStr(*(_DWORD *)(*(_DWORD *)(v32 + 796) + 1044));
            unknown_libname_41();
            System::__linkproc__ LStrCatN(&v17, 12, v12);
            sub_4E42F8(v17);
          }
          v26 = *(_DWORD *)(v32 + 796);
          sub_4CAD6C(0, 0, 0, 0);
          sub_4BB124(v26, &str_Successful[1], 1);
          *(_DWORD *)(v26 + 796) = 0;
          *(_BYTE *)(v26 + 791) = 0;
          (*(void (__fastcall **)(_DWORD))(**(_DWORD **)(v26 + 1040) + 8))(*(_DWORD *)(v26 + 1040));
          *(_DWORD *)(v26 + 1044) = 0;
          sub_4CAD6C(0, 0, 0, 0);
          sub_4BB124(v32, &str_Successful[1], 1);
          *(_DWORD *)(v32 + 796) = 0;
          *(_BYTE *)(v32 + 791) = 0;
          (*(void (__fastcall **)(_DWORD))(**(_DWORD **)(v32 + 1040) + 8))(*(_DWORD *)(v32 + 1040));
          *(_DWORD *)(v32 + 1044) = 0;
        }
        else
        {
          sub_4DD394(v32);
        }
      }
      else
      {
        sub_4BB124(v32, aYouAskForConfi, 1);
        sub_4BB124(*(_DWORD *)(v32 + 796), aPlayerAsksForC, 1);
      }
    }
    else
    {
      sub_4BB124(v32, &str_Hit_Deal_Too_Fa[1], 0);
      sub_4DD394(v32);
    }
  }
  __writefsdword(0, v14[0]);
  v15 = (int *)&loc_4DE495;
  unknown_libname_35(&v16, 28);
  return unknown_libname_35(&v24, 2);
}


// ==================== [0x4DE654] sub_4DE654 (size: 649) ====================
int __fastcall sub_4DE654(int a1, int a2, int a3, int a4)
{
  int v4; // ecx
  int v5; // ecx
  int v6; // ecx
  int v7; // eax
  unsigned int v9[2]; // [esp-Ch] [ebp-3A8h] BYREF
  int *v10; // [esp-4h] [ebp-3A0h]
  int v11; // [esp+Ch] [ebp-390h] BYREF
  _DWORD v12[3]; // [esp+10h] [ebp-38Ch] BYREF
  _BYTE v13[256]; // [esp+1Ch] [ebp-380h] BYREF
  _DWORD v14[13]; // [esp+11Ch] [ebp-280h] BYREF
  _BYTE v15[44]; // [esp+150h] [ebp-24Ch] BYREF
  int v16; // [esp+17Ch] [ebp-220h] BYREF
  _BYTE v17[16]; // [esp+180h] [ebp-21Ch] BYREF
  int v18; // [esp+190h] [ebp-20Ch]
  _BYTE v19[15]; // [esp+194h] [ebp-208h] BYREF
  _BYTE v20[485]; // [esp+1A3h] [ebp-1F9h] BYREF
  const void *v21; // [esp+388h] [ebp-14h]
  int i; // [esp+38Ch] [ebp-10h]
  int v23; // [esp+390h] [ebp-Ch]
  int v24; // [esp+394h] [ebp-8h]
  int v25; // [esp+398h] [ebp-4h]
  int savedregs; // [esp+39Ch] [ebp+0h] BYREF

  v11 = 0;
  v23 = a3;
  v24 = a2;
  v25 = a1;
  v10 = &savedregs;
  v9[1] = (unsigned int)&loc_4DE8CD;
  v9[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v9);
  if ( (unsigned __int8)sub_4C36CC(a4) != 0 )
  {
    System::__linkproc__ FillChar(&v16, 524, 0);
    v16 = sub_4BA25C(v24, v25);
    LOBYTE(v4) = 14;
    System::__linkproc__ PStrNCpy(v17, v24 + 21, v4);
    v18 = (unsigned __int8)sub_4BF180(v25, v24);
    if ( *(_DWORD *)(v24 + 800) != 0 )
    {
      System::__linkproc__ LStrToString(v13, *(_DWORD *)(*(_DWORD *)(v24 + 800) + 4), 255);
      LOBYTE(v5) = 14;
      System::__linkproc__ PStrNCpy(v19, v13, v5);
    }
    System::__linkproc__ LStrToString(v13, *(_DWORD *)(v24 + 808), 255);
    LOBYTE(v6) = 14;
    System::__linkproc__ PStrNCpy(v20, v13, v6);
    for ( i = 0; i != 9; ++i )
    {
      if ( *(_WORD *)(v24 + 24 * i + 1060) != 0 )
      {
        v21 = (const void *)sub_4AC2F8(*off_4EA490, *(unsigned __int16 *)(v24 + 24 * i + 1060));
        if ( v21 != nullptr )
        {
          qmemcpy(v15, v21, sizeof(v15));
          sub_495974(*off_4EA324, v24 + 24 * i + 1056, v15);
          System::Move(v15, v14, 44);
          v14[11] = *(_DWORD *)(v24 + 24 * i + 1056);
          v14[12] = *(_DWORD *)(v24 + 24 * i + 1062);
          qmemcpy(&v20[52 * i + 17], v14, 52);
        }
      }
    }
    sub_4546F8(v12, 1, 0);
    v7 = v25;
    *(_DWORD *)(v25 + 1360) = v12[0];
    *(_DWORD *)(v7 + 1364) = v12[1];
    *(_DWORD *)(v7 + 1368) = v12[2];
    sub_4565B0(&v16, 524, &v11, v9[0]);
    sub_4CAB38(v25, (void *)(v25 + 1360), v11);
  }
  __writefsdword(0, v9[0]);
  v10 = (int *)&loc_4DE8D4;
  return unknown_libname_34(&v11);
}


// ==================== [0x4DE8E0] sub_4DE8E0 (size: 645) ====================
int __fastcall sub_4DE8E0(_DWORD *a1)
{
  int v1; // eax
  int v2; // ecx
  int v3; // eax
  int v4; // ecx
  int v5; // eax
  int v6; // ecx
  _DWORD *v7; // eax
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-Ch] [ebp-3Ch] BYREF
  void *v10; // [esp-8h] [ebp-38h]
  int *v11; // [esp-4h] [ebp-34h]
  int v12; // [esp+4h] [ebp-2Ch] BYREF
  _DWORD v13[3]; // [esp+8h] [ebp-28h] BYREF
  int v14; // [esp+14h] [ebp-1Ch] BYREF
  int v15; // [esp+18h] [ebp-18h] BYREF
  int v16; // [esp+1Ch] [ebp-14h] BYREF
  int v17; // [esp+20h] [ebp-10h]
  int v18; // [esp+24h] [ebp-Ch] BYREF
  int v19; // [esp+28h] [ebp-8h]
  _DWORD *v20; // [esp+2Ch] [ebp-4h]
  int savedregs; // [esp+30h] [ebp+0h] BYREF

  v12 = 0;
  v14 = 0;
  v15 = 0;
  v16 = 0;
  v18 = 0;
  v20 = a1;
  v11 = &savedregs;
  v10 = &loc_4DEB59;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  if ( a1[200] != 0 )
  {
    System::__linkproc__ LStrCat3((int)&v18, *(void **)(v20[200] + 4), &str___104[1]);
    System::__linkproc__ LStrCat((int)&v18, &str____16[1]);
    if ( v20[201] == 1 )
      System::__linkproc__ LStrCat((int)&v18, &str_1__9[1]);
    else
      System::__linkproc__ LStrCat((int)&v18, &str_0__5[1]);
    System::__linkproc__ LStrCat((int)&v18, &str__Notice__[1]);
    v1 = (*(int (__fastcall **)(_DWORD))(**(_DWORD **)(v20[200] + 8) + 20))(*(_DWORD *)(v20[200] + 8)) - 1;
    if ( v1 >= 0 )
    {
      v17 = v1 + 1;
      v19 = 0;
      do
      {
        if ( unknown_libname_43(v18) > 5000 )
          break;
        (*(void (__fastcall **)(_DWORD, int, int *, int *, void *, struct _EXCEPTION_REGISTRATION_RECORD *, int))(**(_DWORD **)(v20[200] + 8) + 12))(
          *(_DWORD *)(v20[200] + 8),
          v19,
          &v16,
          v11,
          v10,
          ExceptionList,
          v18);
        System::__linkproc__ LStrCatN(&v18, 3, v2);
        ++v19;
        --v17;
      }
      while ( v17 != 0 );
    }
    System::__linkproc__ LStrCat((int)&v18, &str__KillGuilds__[1]);
    v3 = (*(int (__fastcall **)(_DWORD))(**(_DWORD **)(v20[200] + 12) + 20))(*(_DWORD *)(v20[200] + 12)) - 1;
    if ( v3 >= 0 )
    {
      v17 = v3 + 1;
      v19 = 0;
      do
      {
        if ( unknown_libname_43(v18) > 5000 )
          break;
        (*(void (__fastcall **)(_DWORD, int, int *, int))(**(_DWORD **)(v20[200] + 12) + 12))(
          *(_DWORD *)(v20[200] + 12),
          v19,
          &v15,
          v18);
        System::__linkproc__ LStrCatN(&v18, 3, v4);
        ++v19;
        --v17;
      }
      while ( v17 != 0 );
    }
    System::__linkproc__ LStrCat((int)&v18, &str__AllyGuilds__[1]);
    v5 = (*(int (__fastcall **)(_DWORD))(**(_DWORD **)(v20[200] + 16) + 20))(*(_DWORD *)(v20[200] + 16)) - 1;
    if ( v5 >= 0 )
    {
      v17 = v5 + 1;
      v19 = 0;
      do
      {
        if ( unknown_libname_43(v18) > 5000 )
          break;
        (*(void (__fastcall **)(_DWORD, int, int *, int))(**(_DWORD **)(v20[200] + 16) + 12))(
          *(_DWORD *)(v20[200] + 16),
          v19,
          &v14,
          v18);
        System::__linkproc__ LStrCatN(&v18, 3, v6);
        ++v19;
        --v17;
      }
      while ( v17 != 0 );
    }
    sub_4546F8(v13, 1, 0);
    v7 = v20;
    v20[340] = v13[0];
    v7[341] = v13[1];
    v7[342] = v13[2];
    sub_456504(v18, &v12);
    sub_4CAB38((int)v20, v20 + 340, v12);
  }
  else
  {
    sub_4CAD6C(0, 0, 0, 0);
  }
  __writefsdword(0, (unsigned int)ExceptionList);
  v11 = (int *)&loc_4DEB60;
  unknown_libname_34(&v12);
  unknown_libname_35(&v14, 3);
  return unknown_libname_34(&v18);
}


// ==================== [0x4DEBF0] sub_4DEBF0 (size: 413) ====================
int __fastcall sub_4DEBF0(int a1)
{
  int v1; // ecx
  int v2; // eax
  int v3; // ecx
  int v4; // eax
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-Ch] [ebp-44h] BYREF
  void *v7; // [esp-8h] [ebp-40h]
  int *v8; // [esp-4h] [ebp-3Ch]
  int v9; // [esp+4h] [ebp-34h] BYREF
  _DWORD v10[3]; // [esp+8h] [ebp-30h] BYREF
  _DWORD v11[2]; // [esp+14h] [ebp-24h] BYREF
  int v12; // [esp+1Ch] [ebp-1Ch]
  int v13; // [esp+20h] [ebp-18h]
  int v14; // [esp+24h] [ebp-14h]
  int v15; // [esp+28h] [ebp-10h] BYREF
  int v16; // [esp+2Ch] [ebp-Ch]
  int v17; // [esp+30h] [ebp-8h]
  int v18; // [esp+34h] [ebp-4h]
  int savedregs; // [esp+38h] [ebp+0h] BYREF

  v9 = 0;
  v11[0] = 0;
  v11[1] = 0;
  v15 = 0;
  v18 = a1;
  v8 = &savedregs;
  v7 = &loc_4DED81;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  if ( *(_DWORD *)(a1 + 800) != 0 )
  {
    unknown_libname_34(&v15);
    if ( *(_DWORD *)(*(_DWORD *)(*(_DWORD *)(v18 + 800) + 20) + 8) - 1 >= 0 )
    {
      v13 = *(_DWORD *)(*(_DWORD *)(*(_DWORD *)(v18 + 800) + 20) + 8);
      v17 = 0;
      do
      {
        v14 = Classes::TList::Get(*(Classes::TList **)(*(_DWORD *)(v18 + 800) + 20), v17);
        Sysutils::IntToStr(*(_DWORD *)v14);
        System::__linkproc__ LStrCatN(&v15, 6, v1);
        v2 = (*(int (__fastcall **)(_DWORD))(**(_DWORD **)(v14 + 8) + 20))(*(_DWORD *)(v14 + 8)) - 1;
        if ( v2 >= 0 )
        {
          v12 = v2 + 1;
          v16 = 0;
          do
          {
            if ( unknown_libname_43(v15) > 5000 )
              break;
            (*(void (__fastcall **)(_DWORD, int, _DWORD *, int *, void *, struct _EXCEPTION_REGISTRATION_RECORD *, int))(**(_DWORD **)(v14 + 8) + 12))(
              *(_DWORD *)(v14 + 8),
              v16,
              v11,
              v8,
              v7,
              ExceptionList,
              v15);
            System::__linkproc__ LStrCatN(&v15, 3, v3);
            ++v16;
            --v12;
          }
          while ( v12 != 0 );
        }
        ++v17;
        --v13;
      }
      while ( v13 != 0 );
    }
    sub_4546F8(v10, 1, 0);
    v4 = v18;
    *(_DWORD *)(v18 + 1360) = v10[0];
    *(_DWORD *)(v4 + 1364) = v10[1];
    *(_DWORD *)(v4 + 1368) = v10[2];
    sub_456504(v15, &v9);
    sub_4CAB38(v18, (void *)(v18 + 1360), v9);
  }
  __writefsdword(0, (unsigned int)ExceptionList);
  v8 = (int *)&loc_4DED88;
  unknown_libname_34(&v9);
  unknown_libname_35(v11, 2);
  return unknown_libname_34(&v15);
}


// ==================== [0x4DEDB4] sub_4DEDB4 (size: 465) ====================
int __fastcall sub_4DEDB4(int a1, int a2)
{
  int v2; // eax
  unsigned int v4[2]; // [esp-Ch] [ebp-20h] BYREF
  int *v5; // [esp-4h] [ebp-18h]
  int v6; // [esp+0h] [ebp-14h] BYREF
  int v7; // [esp+4h] [ebp-10h]
  int v8; // [esp+8h] [ebp-Ch]
  int v9; // [esp+Ch] [ebp-8h] BYREF
  int v10; // [esp+10h] [ebp-4h]
  int savedregs; // [esp+14h] [ebp+0h] BYREF

  v6 = 0;
  v9 = a2;
  v10 = a1;
  unknown_libname_44(a2);
  v5 = &savedregs;
  v4[1] = (unsigned int)&loc_4DEF7A;
  v4[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v4);
  v8 = 1;
  if ( (unsigned __int8)sub_4BF4A0(v10) != 0 )
  {
    v7 = sub_4AE640(*off_4EA490);
    if ( v7 != 0 )
    {
      v2 = sub_4C368C(v7);
      if ( v2 == v10 )
      {
        if ( *(_BYTE *)(v7 + 177) != 0 )
        {
          if ( (unsigned __int8)sub_498714(*(_DWORD *)(v10 + 800), v9) != 0 )
          {
            v8 = 3;
          }
          else if ( *(_DWORD *)(v7 + 800) != 0 || *(int *)(*(_DWORD *)(*(_DWORD *)(v10 + 800) + 20) + 8) >= 400 )
          {
            v8 = 4;
          }
          else
          {
            sub_498CA8(*(_DWORD *)(v10 + 800), v7);
            sub_4AF728(*(_DWORD *)(*(_DWORD *)(v10 + 800) + 4));
            *(_DWORD *)(v7 + 800) = *(_DWORD *)(v10 + 800);
            sub_4987F0(&v6);
            unknown_libname_36(v7 + 808, v6);
            v8 = 0;
          }
        }
        else
        {
          v8 = 5;
          sub_4BB124(v7, &str_______________8[1], 0);
        }
      }
      else
      {
        v8 = 2;
      }
    }
    else
    {
      v8 = 2;
    }
  }
  sub_4CAD6C(0, 0, 0, 0);
  __writefsdword(0, v4[0]);
  v5 = (int *)&loc_4DEF81;
  unknown_libname_34(&v6);
  return unknown_libname_34(&v9);
}


// ==================== [0x4DEFB8] @Iwserver@TIWServer@DetermineBrowser$qqrp17System@TMetaClass17System@AnsiString (size: 521) ====================
int __fastcall Iwserver::TIWServer::DetermineBrowser(int a1, int a2)
{
  char v2; // zf
  int v3; // ecx
  unsigned int v5[2]; // [esp-Ch] [ebp-28h] BYREF
  int *v6; // [esp-4h] [ebp-20h]
  int v7; // [esp+0h] [ebp-1Ch] BYREF
  int v8; // [esp+4h] [ebp-18h]
  int v9; // [esp+8h] [ebp-14h] BYREF
  int v10; // [esp+Ch] [ebp-10h]
  int v11; // [esp+10h] [ebp-Ch]
  int v12; // [esp+14h] [ebp-8h] BYREF
  int v13; // [esp+18h] [ebp-4h]
  int savedregs; // [esp+1Ch] [ebp+0h] BYREF

  v7 = 0;
  v8 = 0;
  v9 = 0;
  v12 = a2;
  v13 = a1;
  unknown_libname_44(a2);
  v6 = &savedregs;
  v5[1] = (unsigned int)&loc_4DF1B6;
  v5[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v5);
  v11 = 1;
  if ( (unsigned __int8)sub_4BF4A0(v13) != 0 )
  {
    if ( (unsigned __int8)sub_498714(*(_DWORD *)(v13 + 800), v12) != 0 )
    {
      unknown_libname_41();
      System::__linkproc__ LStrCmp(v8, v12);
      if ( v2 )
      {
        v11 = 3;
        unknown_libname_37(&v9, *(_DWORD *)(*(_DWORD *)(v13 + 800) + 4));
        if ( (unsigned __int8)sub_498A50(*(_DWORD *)(v13 + 800), v12) != 0 )
        {
          sub_49A550(*off_4EA07C[0], v9);
          sub_4AF728(v9);
          *(_DWORD *)(v13 + 800) = 0;
          sub_4CAE3C(v13, 0, 0);
          System::__linkproc__ LStrCatN(&v7, 3, v3);
          sub_4BB124(v13, v7, 0);
          v11 = 0;
        }
      }
      else if ( (unsigned __int8)sub_498DCC(*(_DWORD *)(v13 + 800), v12) != 0 )
      {
        v10 = sub_4AE640(*off_4EA490);
        if ( v10 != 0 )
        {
          *(_DWORD *)(v10 + 800) = 0;
          sub_4CAE3C(v10, 0, 0);
        }
        sub_4AF728(*(_DWORD *)(*(_DWORD *)(v13 + 800) + 4));
        v11 = 0;
      }
      else
      {
        v11 = 4;
      }
    }
    else
    {
      v11 = 2;
    }
  }
  sub_4CAD6C(0, 0, 0, 0);
  __writefsdword(0, v5[0]);
  v6 = (int *)&loc_4DF1BD;
  unknown_libname_35(&v7, 3);
  return unknown_libname_34(&v12);
}


// ==================== [0x4DF3AC] sub_4DF3AC (size: 564) ====================
int __fastcall sub_4DF3AC(int a1)
{
  int v1; // eax
  unsigned int v3[2]; // [esp-Ch] [ebp-20h] BYREF
  int *v4; // [esp-4h] [ebp-18h]
  int v5; // [esp+0h] [ebp-14h] BYREF
  int v6; // [esp+4h] [ebp-10h] BYREF
  int v7; // [esp+8h] [ebp-Ch]
  int v8; // [esp+Ch] [ebp-8h]
  int v9; // [esp+10h] [ebp-4h]
  int savedregs; // [esp+14h] [ebp+0h] BYREF

  v5 = 0;
  v6 = 0;
  v9 = a1;
  v4 = &savedregs;
  v3[1] = (unsigned int)&loc_4DF5D5;
  v3[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v3);
  v8 = -1;
  v7 = sub_4C368C(a1);
  if ( v7 != 0 && *(_BYTE *)(v7 + 570) == 0 )
  {
    v1 = sub_4C368C(v7);
    if ( v1 == v9 )
    {
      if ( *(_BYTE *)(*(_DWORD *)(v7 + 800) + 36) != 0 )
      {
        if ( (unsigned __int8)sub_4BF4A0(v9) != 0 && (unsigned __int8)sub_4BF4A0(v7) != 0 )
        {
          if ( (unsigned __int8)sub_499C98(*(_DWORD *)(v9 + 800), *(_DWORD *)(v7 + 800)) != 0
            && (unsigned __int8)sub_499C98(*(_DWORD *)(v7 + 800), *(_DWORD *)(v9 + 800)) != 0 )
          {
            sub_499C2C(*(_DWORD *)(v9 + 800), *(_DWORD *)(v7 + 800));
            sub_499C2C(*(_DWORD *)(v7 + 800), *(_DWORD *)(v9 + 800));
            System::__linkproc__ LStrCat3((int)&v6, *(void **)(*(_DWORD *)(v7 + 800) + 4), &str_______________9[1]);
            sub_498FF0(*(_DWORD *)(v9 + 800), v6);
            System::__linkproc__ LStrCat3((int)&v5, *(void **)(*(_DWORD *)(v9 + 800) + 4), &str_______________9[1]);
            sub_498FF0(*(_DWORD *)(v7 + 800), v5);
            sub_498F60(*(_DWORD *)(v9 + 800));
            sub_498F60(*(_DWORD *)(v7 + 800));
            sub_4AF728(*(_DWORD *)(*(_DWORD *)(v9 + 800) + 4));
            sub_4AF728(*(_DWORD *)(*(_DWORD *)(v7 + 800) + 4));
            v8 = 0;
          }
          else
          {
            v8 = -2;
          }
        }
        else
        {
          v8 = -3;
        }
      }
      else
      {
        v8 = -4;
      }
    }
  }
  sub_4CAD6C(0, 0, 0, 0);
  __writefsdword(0, v3[0]);
  v4 = (int *)&loc_4DF5DC;
  return unknown_libname_35(&v5, 2);
}


// ==================== [0x4DF604] sub_4DF604 (size: 440) ====================
int __fastcall sub_4DF604(int a1, int a2)
{
  unsigned int v3[2]; // [esp-Ch] [ebp-24h] BYREF
  int *v4; // [esp-4h] [ebp-1Ch]
  int v5; // [esp+0h] [ebp-18h] BYREF
  int v6; // [esp+4h] [ebp-14h] BYREF
  int v7; // [esp+8h] [ebp-10h]
  int v8; // [esp+Ch] [ebp-Ch]
  int v9; // [esp+10h] [ebp-8h] BYREF
  int v10; // [esp+14h] [ebp-4h]
  int savedregs; // [esp+18h] [ebp+0h] BYREF

  v5 = 0;
  v6 = 0;
  v9 = a2;
  v10 = a1;
  unknown_libname_44(a2);
  v4 = &savedregs;
  v3[1] = (unsigned int)&loc_4DF7B1;
  v3[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v3);
  v7 = -1;
  if ( (unsigned __int8)sub_4BF4A0(v10) != 0 )
  {
    v8 = sub_49A36C(*off_4EA07C[0], v9);
    if ( v8 != 0 )
    {
      if ( (unsigned __int8)sub_499BD8(*(_DWORD *)(v10 + 800), v8) != 0 )
      {
        sub_499CEC(*(_DWORD *)(v10 + 800), v8);
        sub_499CEC(v8, *(_DWORD *)(v10 + 800));
        System::__linkproc__ LStrCat3((int)&v6, *(void **)(v8 + 4), &str_______________10[1]);
        sub_498FF0(*(_DWORD *)(v10 + 800), v6);
        System::__linkproc__ LStrCat3((int)&v5, *(void **)(*(_DWORD *)(v10 + 800) + 4), &str_______________[1]);
        sub_498FF0(v8, v5);
        sub_498F60(*(_DWORD *)(v10 + 800));
        sub_498F60(v8);
        sub_4AF728(*(_DWORD *)(*(_DWORD *)(v10 + 800) + 4));
        sub_4AF728(*(_DWORD *)(v8 + 4));
        v7 = 0;
      }
      else
      {
        v7 = -2;
      }
    }
    else
    {
      v7 = -3;
    }
  }
  sub_4CAD6C(0, 0, 0, 0);
  __writefsdword(0, v3[0]);
  v4 = (int *)&loc_4DF7B8;
  unknown_libname_35(&v5, 2);
  return unknown_libname_34(&v9);
}


// ==================== [0x4DF84C] sub_4DF84C (size: 341) ====================
int __fastcall sub_4DF84C(int a1, int a2)
{
  int v2; // ecx
  int v3; // edx
  int v5; // [esp-14h] [ebp-2Ch]
  int v6; // [esp-10h] [ebp-28h]
  unsigned int v7[2]; // [esp-Ch] [ebp-24h] BYREF
  int *v8; // [esp-4h] [ebp-1Ch]
  int v9; // [esp+4h] [ebp-14h] BYREF
  int v10; // [esp+8h] [ebp-10h]
  _DWORD *v11; // [esp+Ch] [ebp-Ch]
  int v12; // [esp+10h] [ebp-8h]
  int v13; // [esp+14h] [ebp-4h]
  int savedregs; // [esp+18h] [ebp+0h] BYREF

  v9 = 0;
  v12 = a2;
  v13 = a1;
  v8 = &savedregs;
  v7[1] = (unsigned int)&loc_4DF995;
  v7[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v7);
  if ( *(_BYTE *)(a1 + 47) == 2 )
    v10 = 1;
  else
    v10 = 5;
  v6 = v10;
  v5 = *(_DWORD *)(v12 + 24);
  unknown_libname_41();
  v11 = sub_4C37C0(v13, v9, *(unsigned __int8 *)(v12 + 21), v6, v5);
  if ( v11 != nullptr )
  {
    v11[165] = *(_DWORD *)(v12 + 16);
    *((_BYTE *)v11 + 664) = *(_BYTE *)(v12 + 20);
    *((_WORD *)v11 + 210) = *(_WORD *)(v12 + 28);
    *((_WORD *)v11 + 211) = *(_WORD *)(v12 + 32);
    if ( 1500 - 200 * *(unsigned __int8 *)(v12 + 21) < v11[319] )
      v11[319] = 1500 - 200 * *(unsigned __int8 *)(v12 + 21);
    v3 = 2000 - 200 * *(unsigned __int8 *)(v12 + 21);
    if ( v3 < v11[325] )
    {
      v3 = 2000 - 200 * *(unsigned __int8 *)(v12 + 21);
      v11[325] = v3;
    }
    unknown_libname_25(v11, v3, v2);
  }
  __writefsdword(0, v7[0]);
  v8 = (int *)&loc_4DF99C;
  return unknown_libname_34(&v9);
}


// ==================== [0x4DFC94] sub_4DFC94 (size: 624) ====================
int __fastcall sub_4DFC94(int a1, Scktcomp::TCustomWinSocket *a2)
{
  int v2; // ecx
  int v3; // edx
  int v4; // eax
  int v5; // edx
  int v6; // eax
  int v7; // ecx
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-Ch] [ebp-130h] BYREF
  void *v10; // [esp-8h] [ebp-12Ch]
  int *v11; // [esp-4h] [ebp-128h]
  void *v12; // [esp+0h] [ebp-124h] BYREF
  int v13[2]; // [esp+4h] [ebp-120h] BYREF
  int v14; // [esp+Ch] [ebp-118h] BYREF
  int v15; // [esp+10h] [ebp-114h] BYREF
  _BYTE v16[256]; // [esp+14h] [ebp-110h] BYREF
  int v17; // [esp+114h] [ebp-10h] BYREF
  int v18; // [esp+118h] [ebp-Ch]
  Scktcomp::TCustomWinSocket *v19; // [esp+11Ch] [ebp-8h]
  int v20; // [esp+120h] [ebp-4h]
  int savedregs; // [esp+124h] [ebp+0h] BYREF

  v13[0] = 0;
  v12 = nullptr;
  v14 = 0;
  v13[1] = 0;
  v15 = 0;
  v17 = 0;
  v19 = a2;
  v20 = a1;
  v11 = &savedregs;
  v10 = &loc_4DFEF9;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  if ( *off_4EA344 != 0 )
  {
    sub_4516F8(v19, &v17);
    v18 = 0;
    while ( *(_BYTE *)(v20 + 100 * v18 + 1280) != 0 )
    {
      if ( ++v18 == 20 )
        goto LABEL_8;
    }
    *(_BYTE *)(v20 + 100 * v18 + 1280) = 1;
    *(_DWORD *)(v20 + 100 * v18 + 1284) = v19;
    sub_4DFBE0(v20, v17, &v15, v13[0], v12, v11, v10, ExceptionList);
    System::__linkproc__ LStrToString(v16, v15, 255);
    v2 = v20;
    LOBYTE(v2) = 20;
    System::__linkproc__ PStrNCpy(v20 + 100 * v18 + 1288, v16, v2);
    v3 = v20;
    *(_DWORD *)(v20 + 100 * v18 + 1312) = 1;
    LOBYTE(v3) = 1;
    v4 = unknown_libname_22(off_40FAFC, v3);
    *(_DWORD *)(v20 + 100 * v18 + 1328) = v4;
    *(_DWORD *)(v20 + 100 * v18 + 1316) = 0;
    v5 = v20;
    *(_DWORD *)(v20 + 100 * v18 + 1320) = 0;
    LOBYTE(v5) = 1;
    v6 = unknown_libname_22(off_40FAFC, v5);
    *(_DWORD *)(v20 + 100 * v18 + 1324) = v6;
    *(_BYTE *)(v20 + 100 * v18 + 1332) = 0;
    *(_DWORD *)(v20 + 100 * v18 + 1336) = 0;
    *(_DWORD *)(v20 + 100 * v18 + 1344) = 0;
    *(_DWORD *)(v20 + 100 * v18 + 1348) = GetTickCount();
    Sysutils::IntToStr(v18);
    System::__linkproc__ LStrCatN(&v14, 3, v7);
    sub_4E4274(v14);
  }
  else
  {
    sub_4516F8(v19, &v12);
    System::__linkproc__ LStrCat3((int)v13, &str_Not_ready_[1], v12);
    sub_4E4274(v13[0]);
    Scktcomp::TCustomWinSocket::Close(v19);
  }
LABEL_8:
  __writefsdword(0, (unsigned int)ExceptionList);
  v11 = (int *)&loc_4DFF00;
  unknown_libname_35(&v12, 5);
  return unknown_libname_34(&v17);
}


// ==================== [0x4E00B4] sub_4E00B4 (size: 650) ====================
// bad sp value at call has been detected, the output may be wrong!
int __fastcall sub_4E00B4(int a1, unsigned int a2)
{
  int v2; // eax
  int v3; // edx
  int v4; // ecx
  unsigned int v6[2]; // [esp-18h] [ebp-40h] BYREF
  void **v7; // [esp-10h] [ebp-38h]
  _DWORD v8[3]; // [esp-Ch] [ebp-34h] BYREF
  int v9; // [esp+0h] [ebp-28h] BYREF
  int v10; // [esp+4h] [ebp-24h] BYREF
  int v11; // [esp+8h] [ebp-20h]
  int v12; // [esp+Ch] [ebp-1Ch]
  int v13; // [esp+10h] [ebp-18h]
  Classes::TList *v14; // [esp+14h] [ebp-14h]
  int v15; // [esp+18h] [ebp-10h]
  int v16; // [esp+1Ch] [ebp-Ch]
  unsigned int v17; // [esp+20h] [ebp-8h]
  int v18; // [esp+24h] [ebp-4h]
  void *savedregs; // [esp+28h] [ebp+0h] BYREF

  v10 = 0;
  v9 = 0;
  v11 = 0;
  v17 = a2;
  v18 = a1;
  v8[2] = &savedregs;
  v8[1] = &loc_4E0333;
  v8[0] = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v8);
  v7 = &savedregs;
  v6[1] = (unsigned int)&loc_4E0311;
  v6[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v6);
  sub_450080(*off_4E9F84);
  v16 = 0;
  while ( *(_DWORD *)(v18 + 100 * v16 + 1284) != v17 )
  {
    if ( ++v16 == 20 )
      goto LABEL_18;
  }
  v14 = *(Classes::TList **)(v18 + 100 * v16 + 1328);
  if ( *((_DWORD *)v14 + 2) - 1 >= 0 )
  {
    v12 = *((_DWORD *)v14 + 2);
    v15 = 0;
    do
    {
      v13 = Classes::TList::Get(v14, v15);
      if ( v13 != 0 )
      {
        if ( *(_DWORD *)(v13 + 76) != 0 )
        {
          *(_BYTE *)(*(_DWORD *)(v13 + 76) + 1492) = 1;
          if ( *(_BYTE *)(*(_DWORD *)(v13 + 76) + 1495) == 0 )
          {
            unknown_libname_41();
            sub_48D448(*off_4EA3DC, v11, *(_DWORD *)(v13 + 60));
          }
        }
        System::__linkproc__ FreeMem(v13, 88);
        Classes::TList::Put(v14, v15, nullptr);
      }
      ++v15;
      --v12;
    }
    while ( v12 != 0 );
  }
  if ( *(_DWORD *)(v18 + 100 * v16 + 1316) != 0 )
    System::__linkproc__ FreeMem(*(_DWORD *)(v18 + 100 * v16 + 1316), v18);
  *(_DWORD *)(v18 + 100 * v16 + 1316) = 0;
  *(_DWORD *)(v18 + 100 * v16 + 1320) = 0;
  if ( *(_DWORD *)(*(_DWORD *)(v18 + 100 * v16 + 1324) + 8) - 1 >= 0 )
  {
    v12 = *(_DWORD *)(*(_DWORD *)(v18 + 100 * v16 + 1324) + 8);
    v15 = 0;
    do
    {
      v2 = Classes::TList::Get(*(Classes::TList **)(v18 + 100 * v16 + 1324), v15);
      System::__linkproc__ FreeMem(v2, v3);
      ++v15;
      --v12;
    }
    while ( v12 != 0 );
  }
  System::TObject::Free(*(System::TObject **)(v18 + 100 * v16 + 1324));
  *(_DWORD *)(v18 + 100 * v16 + 1324) = 0;
  *(_BYTE *)(v18 + 100 * v16 + 1280) = 0;
  *(_DWORD *)(v18 + 100 * v16 + 1284) = 0;
  Sysutils::IntToStr(v16);
  System::__linkproc__ LStrCatN(&v10, 3, v4);
  sub_4E4274(v10);
LABEL_18:
  __writefsdword(0, v6[0]);
  v7 = (void **)&loc_4E0318;
  Webadapt::TBaseAdapterErrorsList::GetErrorCount((Webadapt::TBaseAdapterErrorsList *)*off_4E9F84);
  __writefsdword(0, v17);
  savedregs = &loc_4E033A;
  return unknown_libname_35(&v9, 3);
}


// ==================== [0x4E0490] sub_4E0490 (size: 714) ====================
int __fastcall sub_4E0490(int a1, unsigned int a2, int a3)
{
  int v3; // eax
  struct _EXCEPTION_REGISTRATION_RECORD *v5; // [esp-30h] [ebp-58h] BYREF
  void *v6; // [esp-2Ch] [ebp-54h]
  int *v7; // [esp-28h] [ebp-50h]
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-24h] [ebp-4Ch] BYREF
  void *v9; // [esp-20h] [ebp-48h]
  int *v10; // [esp-1Ch] [ebp-44h]
  unsigned int v11[2]; // [esp-18h] [ebp-40h] BYREF
  int *v12; // [esp-10h] [ebp-38h]
  unsigned int v13[2]; // [esp-Ch] [ebp-34h] BYREF
  int *v14; // [esp-4h] [ebp-2Ch]
  int v15; // [esp+Ch] [ebp-1Ch] BYREF
  int v16; // [esp+10h] [ebp-18h]
  _DWORD *v17; // [esp+14h] [ebp-14h]
  int v18; // [esp+18h] [ebp-10h]
  int v19; // [esp+1Ch] [ebp-Ch]
  unsigned int v20; // [esp+20h] [ebp-8h]
  int v21; // [esp+24h] [ebp-4h]
  int savedregs; // [esp+28h] [ebp+0h] BYREF

  v15 = 0;
  v19 = a3;
  v20 = a2;
  v21 = a1;
  v14 = &savedregs;
  v13[1] = (unsigned int)&loc_4E074C;
  v13[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v13);
  if ( a2 < 0x14 && *(_DWORD *)(v21 + 100 * v20 + 1328) != 0 )
  {
    v12 = &savedregs;
    v11[1] = (unsigned int)&loc_4E072F;
    v11[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
    __writefsdword(0, (unsigned int)v11);
    sub_450080(*off_4E9F84);
    v10 = &savedregs;
    v9 = &loc_4E0701;
    ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
    __writefsdword(0, (unsigned int)&ExceptionList);
    if ( *(_DWORD *)(*(_DWORD *)(v21 + 100 * v20 + 1328) + 8) - 1 >= 0 )
    {
      v16 = *(_DWORD *)(*(_DWORD *)(v21 + 100 * v20 + 1328) + 8);
      v18 = 0;
      while ( 1 )
      {
        if ( Classes::TList::Get(*(Classes::TList **)(v21 + 100 * v20 + 1328), v18) != 0 )
        {
          v3 = Classes::TList::Get(*(Classes::TList **)(v21 + 100 * v20 + 1328), v18);
          if ( *(_DWORD *)(v3 + 52) == v19 )
            break;
        }
        ++v18;
        if ( --v16 == 0 )
          goto LABEL_17;
      }
      v17 = (_DWORD *)Classes::TList::Get(*(Classes::TList **)(v21 + 100 * v20 + 1328), v18);
      v7 = &savedregs;
      v6 = &loc_4E05CA;
      v5 = NtCurrentTeb()->NtTib.ExceptionList;
      __writefsdword(0, (unsigned int)&v5);
      if ( v17[18] != 0 )
        sub_4B45EC(v17[18], v20, v17[13], v10, v9, ExceptionList, v7, v6, v5);
      __writefsdword(0, (unsigned int)v5);
      v7 = &savedregs;
      v6 = &loc_4E060C;
      v5 = NtCurrentTeb()->NtTib.ExceptionList;
      __writefsdword(0, (unsigned int)&v5);
      if ( v17[19] != 0 )
        *(_BYTE *)(v17[19] + 1493) = 1;
      __writefsdword(0, (unsigned int)v5);
      v7 = &savedregs;
      v6 = &loc_4E067F;
      v5 = NtCurrentTeb()->NtTib.ExceptionList;
      __writefsdword(0, (unsigned int)&v5);
      if ( v17[19] != 0 && *(_BYTE *)(v17[19] + 764) != 0 && *(_BYTE *)(v17[19] + 1495) == 0 )
      {
        unknown_libname_41();
        sub_48D448(*off_4EA3DC, v15, v17[15]);
      }
      __writefsdword(0, (unsigned int)v5);
      v7 = &savedregs;
      v6 = &loc_4E06D5;
      v5 = NtCurrentTeb()->NtTib.ExceptionList;
      __writefsdword(0, (unsigned int)&v5);
      System::__linkproc__ FreeMem(v17, 88);
      Classes::TList::Put(*(Classes::TList **)(v21 + 100 * v20 + 1328), v18, nullptr);
      __writefsdword(0, (unsigned int)v5);
    }
LABEL_17:
    __writefsdword(0, (unsigned int)ExceptionList);
    __writefsdword(0, v11[0]);
    v12 = (int *)&loc_4E0736;
    Webadapt::TBaseAdapterErrorsList::GetErrorCount((Webadapt::TBaseAdapterErrorsList *)*off_4E9F84);
  }
  __writefsdword(0, v13[0]);
  v14 = (int *)&loc_4E0753;
  return unknown_libname_34(&v15);
}


// ==================== [0x4E0A2C] unknown_libname_678 (size: 494) ====================
// Borland Visual Component Library & Packages
int __fastcall unknown_libname_678(int a1, int a2, int a3)
{
  char v3; // zf
  struct _EXCEPTION_REGISTRATION_RECORD *v5; // [esp-18h] [ebp-3Ch] BYREF
  void *v6; // [esp-14h] [ebp-38h]
  int *v7; // [esp-10h] [ebp-34h]
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-Ch] [ebp-30h] BYREF
  void *v9; // [esp-8h] [ebp-2Ch]
  int *v10; // [esp-4h] [ebp-28h]
  int v11; // [esp+4h] [ebp-20h] BYREF
  int v12; // [esp+8h] [ebp-1Ch]
  _DWORD *v13; // [esp+Ch] [ebp-18h]
  int v14; // [esp+10h] [ebp-14h]
  int i; // [esp+14h] [ebp-10h]
  int v16; // [esp+18h] [ebp-Ch]
  int v17; // [esp+1Ch] [ebp-8h] BYREF
  int v18; // [esp+20h] [ebp-4h]
  int savedregs; // [esp+24h] [ebp+0h] BYREF

  v11 = 0;
  v16 = a3;
  v17 = a2;
  v18 = a1;
  unknown_libname_44(a2);
  v10 = &savedregs;
  v9 = &loc_4E0C0E;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  for ( i = 0; i != 20; ++i )
  {
    if ( *(_BYTE *)(v18 + 100 * i + 1280) != 0
      && *(_DWORD *)(v18 + 100 * i + 1284) != 0
      && *(_DWORD *)(v18 + 100 * i + 1328) != 0 )
    {
      v7 = &savedregs;
      v6 = &loc_4E0BDC;
      v5 = NtCurrentTeb()->NtTib.ExceptionList;
      __writefsdword(0, (unsigned int)&v5);
      sub_450080(*off_4E9F84);
      if ( *(_DWORD *)(*(_DWORD *)(v18 + 100 * i + 1328) + 8) - 1 >= 0 )
      {
        v12 = *(_DWORD *)(*(_DWORD *)(v18 + 100 * i + 1328) + 8);
        v14 = 0;
        while ( 1 )
        {
          v13 = (_DWORD *)Classes::TList::Get(*(Classes::TList **)(v18 + 100 * i + 1328), v14);
          if ( v13 != nullptr )
          {
            unknown_libname_41();
            System::__linkproc__ LStrCmp(v11, v17);
            if ( v3 || v13[15] == v16 )
              break;
          }
          ++v14;
          if ( --v12 == 0 )
            goto LABEL_16;
        }
        if ( v13[18] != 0 )
          sub_4B45EC(v13[18], i, v13[13], v10, v9, ExceptionList, v7, v6, v5);
        if ( v13[19] != 0 )
        {
          *(_BYTE *)(v13[19] + 1492) = 1;
          *(_BYTE *)(v13[19] + 1493) = 1;
          sub_4E08E4(v18, i, v13[13]);
        }
        System::__linkproc__ FreeMem(v13, 88);
        Classes::TList::Put(*(Classes::TList **)(v18 + 100 * i + 1328), v14, nullptr);
      }
LABEL_16:
      __writefsdword(0, (unsigned int)v5);
      v7 = (int *)&loc_4E0BE3;
      Webadapt::TBaseAdapterErrorsList::GetErrorCount((Webadapt::TBaseAdapterErrorsList *)*off_4E9F84);
    }
  }
  __writefsdword(0, (unsigned int)ExceptionList);
  v10 = (int *)&loc_4E0C15;
  unknown_libname_34(&v11);
  return unknown_libname_34(&v17);
}


// ==================== [0x4E0DE0] sub_4E0DE0 (size: 489) ====================
char __fastcall sub_4E0DE0(__int32 a1, _DWORD *a2, __int32 a3, int *a4, _DWORD *a5, _BYTE *a6)
{
  char v6; // zf
  unsigned int v8[3]; // [esp-18h] [ebp-58h] BYREF
  unsigned int v9[2]; // [esp-Ch] [ebp-4Ch] BYREF
  int *v10; // [esp-4h] [ebp-44h]
  int v11; // [esp+Ch] [ebp-34h] BYREF
  int v12; // [esp+10h] [ebp-30h] BYREF
  int v13; // [esp+14h] [ebp-2Ch] BYREF
  char v14; // [esp+18h] [ebp-28h]
  int v15; // [esp+1Ch] [ebp-24h] BYREF
  _DWORD v16[3]; // [esp+20h] [ebp-20h] BYREF
  _BYTE *v17; // [esp+2Ch] [ebp-14h] BYREF
  char v18; // [esp+33h] [ebp-Dh]
  _DWORD *v19; // [esp+34h] [ebp-Ch]
  _DWORD *v20; // [esp+38h] [ebp-8h]
  __int32 v21; // [esp+3Ch] [ebp-4h] BYREF
  int savedregs; // [esp+40h] [ebp+0h] BYREF

  v19 = (_DWORD *)_InterlockedExchange(&v21, a3);
  v20 = a2;
  v21 = a1;
  unknown_libname_44(a1);
  v10 = &savedregs;
  v9[1] = (unsigned int)&loc_4E0FB6;
  v9[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v9);
  v18 = 0;
  v8[2] = (unsigned int)&savedregs;
  v8[1] = (unsigned int)&loc_4E0F72;
  v8[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v8);
  sub_45633C(v21, &v17);
  if ( unknown_libname_43(v17) > 2 && *v17 == 42 && v17[1] == 42 )
  {
    unknown_libname_43(v17);
    System::__linkproc__ LStrCopy(&v17);
    v14 = 47;
    sub_453DBC(&v15, 0);
    unknown_libname_37(&v17, v15);
    v14 = 47;
    sub_453DBC(&v13, 0);
    unknown_libname_37(&v17, v13);
    v14 = 47;
    sub_453DBC(&v12, 0);
    unknown_libname_37(&v17, v12);
    v14 = 47;
    sub_453DBC(&v11, 0);
    unknown_libname_37(&v17, v11);
    unknown_libname_37(v16, v17);
    *a4 = sub_453B20(v16[2], 0);
    System::__linkproc__ LStrCmp(v16[0], &str_0_15[1]);
    if ( v6 )
      *a6 = 1;
    else
      *a6 = 0;
    if ( *v20 != 0 && *v19 != 0 && *a4 >= 2 )
    {
      *a5 = sub_453B20(v16[1], 0);
      v18 = 1;
    }
  }
  __writefsdword(0, v8[0]);
  __writefsdword(0, v9[0]);
  v10 = (int *)&loc_4E0FBD;
  unknown_libname_35(&v11, 3);
  unknown_libname_35(&v15, 5);
  unknown_libname_34(&v21);
  return v18;
}


// ==================== [0x4E1028] sub_4E1028 (size: 785) ====================
int __fastcall sub_4E1028(int a1, unsigned int a2, int a3, int a4, __int32 a5)
{
  int v5; // edx
  int v6; // ecx
  int v7; // ecx
  _DWORD *v8; // edi
  _DWORD *v9; // edi
  unsigned int v11[2]; // [esp-24h] [ebp-178h] BYREF
  int *v12; // [esp-1Ch] [ebp-170h]
  unsigned int v13[3]; // [esp-18h] [ebp-16Ch] BYREF
  unsigned int v14[2]; // [esp-Ch] [ebp-160h] BYREF
  int *v15; // [esp-4h] [ebp-158h]
  void *v16[2]; // [esp+Ch] [ebp-148h] BYREF
  int v17; // [esp+14h] [ebp-140h]
  int v18; // [esp+18h] [ebp-13Ch] BYREF
  int v19; // [esp+1Ch] [ebp-138h] BYREF
  _BYTE v20[256]; // [esp+20h] [ebp-134h] BYREF
  int v21; // [esp+120h] [ebp-34h] BYREF
  int v22; // [esp+124h] [ebp-30h] BYREF
  char v23; // [esp+12Bh] [ebp-29h] BYREF
  int v24; // [esp+12Ch] [ebp-28h] BYREF
  int v25; // [esp+130h] [ebp-24h]
  int v26; // [esp+134h] [ebp-20h]
  int v27; // [esp+138h] [ebp-1Ch] BYREF
  int *v28; // [esp+13Ch] [ebp-18h] BYREF
  int v29; // [esp+140h] [ebp-14h] BYREF
  unsigned int v30; // [esp+144h] [ebp-10h] BYREF
  int v31; // [esp+148h] [ebp-Ch]
  unsigned int v32; // [esp+14Ch] [ebp-8h]
  int v33; // [esp+150h] [ebp-4h]
  int savedregs; // [esp+154h] [ebp+0h] BYREF

  v16[1] = nullptr;
  v16[0] = nullptr;
  v17 = 0;
  v18 = 0;
  v19 = 0;
  v21 = 0;
  v22 = 0;
  v30 = 0;
  v29 = 0;
  v31 = a3;
  v32 = a2;
  v33 = a1;
  unknown_libname_44(a5);
  v15 = &savedregs;
  v14[1] = (unsigned int)&loc_4E1329;
  v14[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v14);
  v26 = 0;
  v13[2] = (unsigned int)&savedregs;
  v13[1] = (unsigned int)&loc_4E12B0;
  v13[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v13);
  if ( *(_BYTE *)v31 == 0 )
  {
    LOBYTE(v5) = 33;
    if ( sub_455CCC(a5, v5) >= 1 )
    {
      sub_4540F0(&v22, &a5);
      unknown_libname_43(a5);
      System::__linkproc__ LStrCopy(&a5);
      v26 = 1;
      v12 = &savedregs;
      if ( sub_4E0DE0(a5, &v30, (__int32)&v29, (int *)&v28, &v27, &v23) != 0 )
      {
        v12 = v28;
        unknown_libname_41();
        v25 = sub_48DE80(&v24, v12);
        if ( v25 <= 0 )
        {
          v26 = 2;
          v8 = (_DWORD *)v31;
          *(_DWORD *)v31 = dword_4E13A0[0];
          *++v8 = dword_4E13A0[1];
          v8[1] = dword_4E13A0[2];
          *(_BYTE *)(v31 + 84) = 0;
          sub_4E0490(v33, v32, a4);
          v26 = 3;
        }
        else
        {
          *(_BYTE *)(v31 + 84) = 1;
          sub_408F0C(v30, &v19);
          System::__linkproc__ LStrToString(v20, v19, 255);
          LOBYTE(v6) = 12;
          System::__linkproc__ PStrNCpy(v31, v20, v6);
          sub_408F0C(v29, &v18);
          System::__linkproc__ LStrToString(v20, v18, 255);
          LOBYTE(v7) = 14;
          System::__linkproc__ PStrNCpy(v31 + 13, v20, v7);
          *(_DWORD *)(v31 + 60) = v28;
          *(_DWORD *)(v31 + 64) = v27;
          v12 = &savedregs;
          v11[1] = (unsigned int)&loc_4E122E;
          v11[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
          __writefsdword(0, (unsigned int)v11);
          unknown_libname_41();
          sub_4B46A0(*off_4EA10C, v30, v29, v17, v23, (int)v28, v25, v24, v27, a4, *(_DWORD *)(v31 + 56), v32);
          __writefsdword(0, v11[0]);
        }
      }
      else
      {
        v26 = 4;
        v9 = (_DWORD *)v31;
        *(_DWORD *)v31 = dword_4E13A0[0];
        *++v9 = dword_4E13A0[1];
        v9[1] = dword_4E13A0[2];
        *(_BYTE *)(v31 + 84) = 0;
        sub_4E0490(v33, v32, a4);
        v26 = 5;
      }
    }
  }
  __writefsdword(0, v13[0]);
  __writefsdword(0, v14[0]);
  v15 = (int *)&loc_4E1330;
  unknown_libname_35(v16, 5);
  unknown_libname_35(&v21, 2);
  unknown_libname_35(&v29, 2);
  return unknown_libname_34(&a5);
}


// ==================== [0x4E13EC] sub_4E13EC (size: 722) ====================
int __userpurge sub_4E13EC@<eax>(
        int a1@<eax>,
        unsigned int a2@<edx>,
        int a3@<ecx>,
        int a4@<edi>,
        int a5@<esi>,
        int a6,
        _DWORD *a7,
        int a8)
{
  int v9; // [esp-1Ch] [ebp-58h]
  unsigned int v10[3]; // [esp-18h] [ebp-54h] BYREF
  unsigned int v11[2]; // [esp-Ch] [ebp-48h] BYREF
  int *v12; // [esp-4h] [ebp-40h]
  void *v13[2]; // [esp+Ch] [ebp-30h] BYREF
  __int32 v14; // [esp+14h] [ebp-28h] BYREF
  int v15; // [esp+18h] [ebp-24h] BYREF
  int v16; // [esp+1Ch] [ebp-20h]
  int v17; // [esp+20h] [ebp-1Ch]
  int v18; // [esp+24h] [ebp-18h]
  int v19; // [esp+28h] [ebp-14h]
  int v20; // [esp+2Ch] [ebp-10h]
  int v21; // [esp+30h] [ebp-Ch]
  unsigned int v22; // [esp+34h] [ebp-8h]
  int v23; // [esp+38h] [ebp-4h]
  int savedregs; // [esp+3Ch] [ebp+0h] BYREF

  v13[1] = nullptr;
  v13[0] = nullptr;
  v14 = 0;
  v15 = 0;
  v21 = a3;
  v22 = a2;
  v23 = a1;
  v12 = &savedregs;
  v11[1] = (unsigned int)&loc_4E16AE;
  v11[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v11);
  v18 = 0;
  v10[2] = (unsigned int)&savedregs;
  v10[1] = (unsigned int)&loc_4E1666;
  v10[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v10);
  switch ( *(_WORD *)(a8 + 10) )
  {
    case 1:
      v18 = 1;
      sub_409790(a7, &v15);
      v19 = sub_4E0364(*(Classes::TList **)(v21 + 48), v15);
      sub_4E09D0(v19 + 1, *(_WORD *)(a8 + 8));
      break;
    case 2:
      v18 = 2;
      sub_4E0490(v23, v22, *(_DWORD *)(a8 + 4));
      break;
    case 4:
      v18 = 3;
      *(_BYTE *)(v21 + 52) = 1;
      break;
    case 5:
      v18 = 5;
      v17 = 0;
      if ( *(_WORD *)(a8 + 12) != 0 )
      {
        v19 = *(unsigned __int16 *)(a8 + 12) - 1;
        if ( *(_DWORD *)(*(_DWORD *)(v21 + 48) + 8) > v19 )
        {
          v17 = Classes::TList::Get(*(Classes::TList **)(v21 + 48), v19);
          if ( v17 != 0 && *(_DWORD *)(v17 + 52) != *(_DWORD *)(a8 + 4) )
            v17 = 0;
        }
      }
      if ( v17 == 0 && *(_DWORD *)(*(_DWORD *)(v21 + 48) + 8) - 1 >= 0 )
      {
        v16 = *(_DWORD *)(*(_DWORD *)(v21 + 48) + 8);
        v20 = 0;
        while ( Classes::TList::Get(*(Classes::TList **)(v21 + 48), v20) == 0
             || *(_DWORD *)(Classes::TList::Get(*(Classes::TList **)(v21 + 48), v20) + 52) != *(_DWORD *)(a8 + 4) )
        {
          ++v20;
          if ( --v16 == 0 )
            goto LABEL_18;
        }
        v17 = Classes::TList::Get(*(Classes::TList **)(v21 + 48), v20);
      }
LABEL_18:
      v18 = 6;
      if ( v17 != 0 )
      {
        if ( *(_DWORD *)(v17 + 76) != 0 && *(_DWORD *)(v17 + 68) != 0 )
        {
          if ( *(_BYTE *)(v17 + 84) != 0 && a6 >= 12 )
          {
            if ( a6 == 12 )
              DBConnAdmin::_16392(*off_4EA490, *(_DWORD *)(v17 + 76), a7, 0, a4, a5, 0);
            else
              DBConnAdmin::_16392(*off_4EA490, *(_DWORD *)(v17 + 76), a7, 0, a4, a5, (int)(a7 + 3));
          }
        }
        else
        {
          v9 = *(_DWORD *)(a8 + 4);
          sub_409790(a7, &v14);
          sub_4E1028(v23, v22, v17, v9, v14);
        }
      }
      break;
    case 7:
      v18 = 4;
      *(_DWORD *)(v21 + 56) = 0;
      *(_DWORD *)(v21 + 64) = 0;
      break;
    default:
      break;
  }
  __writefsdword(0, v10[0]);
  __writefsdword(0, v11[0]);
  v12 = (int *)&loc_4E16B5;
  return unknown_libname_35(v13, 4);
}


// ==================== [0x4E16E4] sub_4E16E4 (size: 443) ====================
int __userpurge sub_4E16E4@<eax>(
        int a1@<eax>,
        unsigned int a2@<edx>,
        int a3@<ecx>,
        int a4@<edi>,
        int a5@<esi>,
        int a6,
        void *a7)
{
  struct _EXCEPTION_REGISTRATION_RECORD *v7; // edx
  int v8; // edx
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-Ch] [ebp-38h] BYREF
  void *v11; // [esp-8h] [ebp-34h]
  int *v12; // [esp-4h] [ebp-30h]
  _DWORD *v13; // [esp+Ch] [ebp-20h]
  void *Mem; // [esp+10h] [ebp-1Ch]
  char *v15; // [esp+14h] [ebp-18h]
  void *v16; // [esp+18h] [ebp-14h]
  int v17; // [esp+1Ch] [ebp-10h]
  int v18; // [esp+20h] [ebp-Ch]
  unsigned int v19; // [esp+24h] [ebp-8h]
  int v20; // [esp+28h] [ebp-4h]
  int savedregs; // [esp+2Ch] [ebp+0h] BYREF

  v18 = a3;
  v19 = a2;
  v20 = a1;
  v12 = &savedregs;
  v11 = &loc_4E1741;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  if ( a7 != nullptr )
  {
    unknown_libname_15(v18 + 36, a6 + *(_DWORD *)(v18 + 40));
    System::Move(a7, (void *)(*(_DWORD *)(v18 + 40) + *(_DWORD *)(v18 + 36)), a6);
  }
  __writefsdword(0, (unsigned int)ExceptionList);
  v12 = &savedregs;
  v11 = &loc_4E17FD;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  v17 = a6 + *(_DWORD *)(v18 + 40);
  v16 = *(void **)(v18 + 36);
  while ( v17 >= 20 )
  {
    v13 = v16;
    if ( *(_DWORD *)v16 == -1437226411 )
    {
      if ( v13[4] + 20 > v17 )
        break;
      v15 = (char *)v16 + 20;
      sub_4E13EC(v20, v19, v18, a4, a5, v13[4], (_DWORD *)v16 + 5, (int)v13);
      v16 = (char *)v16 + v13[4] + 20;
      v17 -= v13[4] + 20;
    }
    else
    {
      v16 = (char *)v16 + 1;
      --v17;
    }
  }
  v7 = ExceptionList;
  __writefsdword(0, (unsigned int)ExceptionList);
  v12 = &savedregs;
  v11 = &loc_4E1882;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  if ( v17 <= 0 )
  {
    System::__linkproc__ FreeMem(*(_DWORD *)(v18 + 36), v7);
    *(_DWORD *)(v18 + 36) = 0;
    *(_DWORD *)(v18 + 40) = 0;
  }
  else
  {
    Mem = (void *)System::__linkproc__ GetMem(v17);
    System::Move(v16, Mem, v17);
    System::__linkproc__ FreeMem(*(_DWORD *)(v18 + 36), v8);
    *(_DWORD *)(v18 + 36) = Mem;
    *(_DWORD *)(v18 + 40) = v17;
  }
  __writefsdword(0, (unsigned int)ExceptionList);
  return 0;
}


// ==================== [0x4E1930] sub_4E1930 (size: 891) ====================
char __fastcall sub_4E1930(int a1, int a2, int a3, Classes::TList *a4)
{
  int v4; // edx
  int v5; // edx
  int v6; // edx
  char *v7; // edx
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-Ch] [ebp-54h] BYREF
  void *v10; // [esp-8h] [ebp-50h]
  int *v11; // [esp-4h] [ebp-4Ch]
  int v12; // [esp+14h] [ebp-34h]
  DWORD TickCount; // [esp+18h] [ebp-30h]
  char *buf; // [esp+1Ch] [ebp-2Ch]
  void *Mem; // [esp+20h] [ebp-28h]
  void *v16; // [esp+24h] [ebp-24h]
  int len; // [esp+28h] [ebp-20h] BYREF
  int v18; // [esp+2Ch] [ebp-1Ch] BYREF
  int v19; // [esp+30h] [ebp-18h] BYREF
  int v20; // [esp+34h] [ebp-14h] BYREF
  int v21; // [esp+38h] [ebp-10h]
  char v22; // [esp+3Fh] [ebp-9h]
  int v23; // [esp+40h] [ebp-8h]
  int v24; // [esp+44h] [ebp-4h]
  int savedregs; // [esp+48h] [ebp+0h] BYREF

  v23 = a3;
  v12 = a2;
  v24 = a1;
  v22 = 1;
  if ( *((_DWORD *)a4 + 2) != 0 )
  {
    TickCount = GetTickCount();
    if ( *(int *)(v23 + 56) <= 0 )
    {
      v11 = &savedregs;
      v10 = &loc_4E1AA7;
      ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
      __writefsdword(0, (unsigned int)&ExceptionList);
      v21 = 0;
      v16 = (void *)Classes::TList::Get(a4, 0);
      while ( v21 + 1 < *((_DWORD *)a4 + 2) )
      {
        buf = (char *)Classes::TList::Get(a4, v21 + 1);
        System::Move(v16, &v20, 4);
        System::Move(buf, &v19, 4);
        if ( v19 + v20 >= *off_4EA100[0] )
        {
          ++v21;
          v16 = buf;
        }
        else
        {
          Classes::TList::Delete(a4, v21 + 1);
          Mem = (void *)System::__linkproc__ GetMem(v19 + v20 + 4);
          v18 = v19 + v20;
          System::Move(&v18, Mem, 4);
          System::Move((char *)v16 + 4, (char *)Mem + 4, v20);
          System::Move(buf + 4, (char *)Mem + v20 + 4, v19);
          System::__linkproc__ FreeMem(v16, v4);
          System::__linkproc__ FreeMem(buf, v5);
          v16 = Mem;
          Classes::TList::Put(a4, v21, Mem);
        }
      }
      __writefsdword(0, (unsigned int)ExceptionList);
      v11 = &savedregs;
      v10 = &loc_4E1C8B;
      ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
      __writefsdword(0, (unsigned int)&ExceptionList);
      while ( *((int *)a4 + 2) > 0 )
      {
        v16 = (void *)Classes::TList::Get(a4, 0);
        if ( v16 != nullptr )
        {
          System::Move(v16, &len, 4);
          if ( *(_DWORD *)(v23 + 56) == 0 && len + *(_DWORD *)(v23 + 64) >= *off_4EA2AC )
          {
            if ( *(_DWORD *)(v23 + 64) != 0 || *off_4EA2AC > len )
            {
              sub_4E0984(v24, *(_DWORD *)(v23 + 4), 7);
              *(_DWORD *)(v23 + 56) = 1;
              *(_DWORD *)(v23 + 60) = GetTickCount();
            }
            else
            {
              Classes::TList::Delete(a4, 0);
              System::__linkproc__ FreeMem(v16, v6);
            }
            break;
          }
          Classes::TList::Delete(a4, 0);
          buf = (char *)v16 + 4;
          if ( len > 0 )
          {
            while ( *off_4EA100[0] <= len )
            {
              if ( *(_DWORD *)(v23 + 4) != 0 )
              {
                if ( *(_BYTE *)(*(_DWORD *)(v23 + 4) + 8) != 0 )
                  Scktcomp::TCustomWinSocket::SendBuf(*(Scktcomp::TCustomWinSocket **)(v23 + 4), buf, *off_4EA100[0]);
                ++*(_DWORD *)(v23 + 92);
                *(_DWORD *)(v23 + 84) += *off_4EA100[0];
              }
              *(_DWORD *)(v23 + 64) += *off_4EA100[0];
              v7 = buf;
              buf += *off_4EA100[0];
              len -= *off_4EA100[0];
              if ( len <= 0 )
                goto LABEL_32;
            }
            if ( *(_DWORD *)(v23 + 4) != 0 )
            {
              if ( *(_BYTE *)(*(_DWORD *)(v23 + 4) + 8) != 0 )
                Scktcomp::TCustomWinSocket::SendBuf(*(Scktcomp::TCustomWinSocket **)(v23 + 4), buf, len);
              ++*(_DWORD *)(v23 + 92);
              *(_DWORD *)(v23 + 84) += len;
              v7 = (char *)len;
              *(_DWORD *)(v23 + 64) += len;
            }
            len = 0;
          }
LABEL_32:
          System::__linkproc__ FreeMem(v16, v7);
          if ( GetTickCount() - TickCount > *off_4EA1C0 )
          {
            v22 = 0;
            break;
          }
        }
        else
        {
          Classes::TList::Delete(a4, 0);
        }
      }
      __writefsdword(0, (unsigned int)ExceptionList);
    }
    else if ( GetTickCount() - *(_DWORD *)(v23 + 60) > 0x7D0 )
    {
      *(_DWORD *)(v23 + 56) = 0;
      *(_DWORD *)(v23 + 64) = 0;
    }
  }
  return v22;
}


// ==================== [0x4E1CD0] sub_4E1CD0 (size: 593) ====================
int __fastcall sub_4E1CD0(int a1)
{
  int result; // eax
  unsigned int v2[8]; // [esp-Ch] [ebp-3Ch] BYREF
  int v3; // [esp+14h] [ebp-1Ch]
  char v4; // [esp+1Bh] [ebp-15h]
  int v5; // [esp+1Ch] [ebp-14h]
  DWORD TickCount; // [esp+20h] [ebp-10h]
  int v7; // [esp+24h] [ebp-Ch]
  int i; // [esp+28h] [ebp-8h]
  int v9; // [esp+2Ch] [ebp-4h]
  int savedregs; // [esp+30h] [ebp+0h] BYREF

  v9 = a1;
  TickCount = GetTickCount();
  v4 = 0;
  if ( *off_4EA344 == 0 )
    goto LABEL_24;
  v2[2] = (unsigned int)&savedregs;
  v2[1] = (unsigned int)&loc_4E1ED6;
  v2[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v2);
  if ( *off_4EA040 > 0 && GetTickCount() - *(_DWORD *)(v9 + 1276) >= 0x64 )
  {
    *(_DWORD *)(v9 + 1276) = GetTickCount();
    for ( i = 0; i != 20; ++i )
    {
      v5 = v9 + 100 * i + 1280;
      if ( *(_DWORD *)(v9 + 100 * i + 1324) != 0 && *off_4EA040 - 1 >= 0 )
      {
        v3 = *off_4EA040;
        v7 = 0;
        do
        {
          sub_4E0860(v9, i);
          ++v7;
          --v3;
        }
        while ( v3 != 0 );
      }
    }
  }
  i = 0;
  while ( 1 )
  {
    v5 = v9 + 100 * i + 1280;
    if ( *(_DWORD *)(v9 + 100 * i + 1324) != 0 )
      break;
LABEL_15:
    if ( ++i == 20 )
      goto LABEL_16;
  }
  *(_DWORD *)(v5 + 72) = *(_DWORD *)(*(_DWORD *)(v5 + 44) + 8);
  if ( sub_4E1930(v9, i, v5, *(Classes::TList **)(v5 + 44)) != 0 )
  {
    *(_DWORD *)(v5 + 76) = *(_DWORD *)(*(_DWORD *)(v5 + 44) + 8);
    goto LABEL_15;
  }
  *(_DWORD *)(v5 + 76) = *(_DWORD *)(*(_DWORD *)(v5 + 44) + 8);
LABEL_16:
  for ( i = 0; i != 20; ++i )
  {
    if ( *(_DWORD *)(v9 + 100 * i + 1284) != 0 )
    {
      v5 = v9 + 100 * i + 1280;
      if ( GetTickCount() - *(_DWORD *)(v5 + 80) >= 0x3E8 )
      {
        *(_DWORD *)(v5 + 80) = GetTickCount();
        *(_DWORD *)(v5 + 88) = *(_DWORD *)(v5 + 84);
        *(_DWORD *)(v5 + 96) = *(_DWORD *)(v5 + 92);
        *(_DWORD *)(v5 + 84) = 0;
        *(_DWORD *)(v5 + 92) = 0;
      }
      if ( *(_BYTE *)(v9 + 100 * i + 1332) != 0 )
      {
        *(_BYTE *)(v9 + 100 * i + 1332) = 0;
        sub_4E0984(v9, *(_DWORD *)(v9 + 100 * i + 1284), 3);
      }
    }
  }
  __writefsdword(0, v2[0]);
LABEL_24:
  *off_4EA500[0] = GetTickCount() - TickCount;
  result = *off_4EA500[0];
  if ( *off_4EA500[0] > *off_4E9FA0 )
  {
    result = *off_4EA500[0];
    *off_4E9FA0 = *off_4EA500[0];
  }
  return result;
}


// ==================== [0x4E25B4] _TFrmMain_FormCreate (size: 4018) ====================
int __usercall TFrmMain_FormCreate@<eax>(Controls::TControl *a1@<eax>, int a2@<edx>, double a3@<st0>)
{
  int v3; // edx
  int v4; // edx
  int v5; // edx
  int v6; // edx
  int v7; // edx
  int v8; // edx
  int v9; // edx
  int v10; // edx
  int v11; // edx
  int v12; // edx
  int v13; // edx
  int v14; // edx
  int v15; // edx
  int v16; // edx
  int v17; // edx
  int v18; // edx
  int v19; // edx
  int v20; // edx
  int v21; // edx
  int v22; // edx
  int v23; // edx
  int v24; // edx
  int v25; // edx
  int v26; // edx
  int v27; // edx
  int v28; // edx
  int v29; // ecx
  int v30; // eax
  int v31; // eax
  int v32; // edx
  int v33; // eax
  int v34; // ecx
  int v35; // eax
  int v36; // edx
  int v37; // eax
  int v38; // edx
  int v39; // edx
  int ExceptionList; // [esp+18h] [ebp-2ACh] BYREF
  void *v42; // [esp+1Ch] [ebp-2A8h]
  int *v43; // [esp+20h] [ebp-2A4h]
  void *v44; // [esp+24h] [ebp-2A0h]
  int v45; // [esp+28h] [ebp-29Ch] BYREF
  int v46; // [esp+30h] [ebp-294h] BYREF
  int v47; // [esp+34h] [ebp-290h] BYREF
  int v48; // [esp+38h] [ebp-28Ch] BYREF
  int v49; // [esp+3Ch] [ebp-288h] BYREF
  int v50; // [esp+40h] [ebp-284h] BYREF
  int v51; // [esp+44h] [ebp-280h] BYREF
  int v52; // [esp+48h] [ebp-27Ch] BYREF
  int v53; // [esp+4Ch] [ebp-278h] BYREF
  int v54; // [esp+50h] [ebp-274h] BYREF
  int v55; // [esp+54h] [ebp-270h] BYREF
  int v56; // [esp+58h] [ebp-26Ch] BYREF
  int v57; // [esp+5Ch] [ebp-268h] BYREF
  int v58; // [esp+60h] [ebp-264h] BYREF
  int v59; // [esp+64h] [ebp-260h] BYREF
  int v60; // [esp+68h] [ebp-25Ch] BYREF
  int v61; // [esp+6Ch] [ebp-258h] BYREF
  int v62; // [esp+70h] [ebp-254h] BYREF
  int v63; // [esp+74h] [ebp-250h] BYREF
  int v64; // [esp+78h] [ebp-24Ch] BYREF
  int v65; // [esp+7Ch] [ebp-248h] BYREF
  int v66; // [esp+80h] [ebp-244h] BYREF
  int v67; // [esp+84h] [ebp-240h] BYREF
  int v68; // [esp+88h] [ebp-23Ch] BYREF
  int v69; // [esp+8Ch] [ebp-238h] BYREF
  int v70; // [esp+90h] [ebp-234h] BYREF
  int v71; // [esp+94h] [ebp-230h] BYREF
  int v72; // [esp+98h] [ebp-22Ch] BYREF
  int v73; // [esp+9Ch] [ebp-228h] BYREF
  int v74; // [esp+A0h] [ebp-224h] BYREF
  int v75; // [esp+A4h] [ebp-220h] BYREF
  int v76; // [esp+A8h] [ebp-21Ch] BYREF
  int v77; // [esp+ACh] [ebp-218h] BYREF
  int v78; // [esp+B0h] [ebp-214h] BYREF
  int v79; // [esp+B4h] [ebp-210h] BYREF
  int v80; // [esp+B8h] [ebp-20Ch] BYREF
  int v81; // [esp+BCh] [ebp-208h] BYREF
  int v82; // [esp+C0h] [ebp-204h] BYREF
  int v83; // [esp+C4h] [ebp-200h] BYREF
  _BYTE v84[460]; // [esp+D0h] [ebp-1F4h] BYREF
  int v85; // [esp+29Ch] [ebp-28h]
  Scktcomp::TAbstractSocket *v86; // [esp+2A0h] [ebp-24h]
  int v87; // [esp+2A6h] [ebp-1Eh] BYREF
  unsigned __int16 v88; // [esp+2AAh] [ebp-1Ah]
  unsigned __int16 v89; // [esp+2ACh] [ebp-18h]
  unsigned __int16 v90; // [esp+2AEh] [ebp-16h]
  unsigned __int16 v91; // [esp+2B0h] [ebp-14h]
  unsigned __int16 v92; // [esp+2B2h] [ebp-12h]
  System::TObject *v93; // [esp+2B4h] [ebp-10h]
  int v94; // [esp+2B8h] [ebp-Ch] BYREF
  int v95; // [esp+2BCh] [ebp-8h] BYREF
  Controls::TControl *v96; // [esp+2C0h] [ebp-4h]
  int savedregs; // [esp+2C4h] [ebp+0h] BYREF

  v85 = a2;
  v96 = a1;
  v43 = &savedregs;
  v42 = &loc_4E355A;
  ExceptionList = (int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  System::Randomize();
  dword_4EBC04 = 0;
  LOBYTE(v3) = 1;
  dword_4EBB84 = (System::TObject *)sub_4DFA34(off_4DF9DC, v3);
  LOBYTE(v4) = 1;
  dword_4EBC60 = (System::TObject *)unknown_libname_22(off_410028, v4);
  LOBYTE(v5) = 1;
  dword_4EBC64 = (System::TObject *)unknown_libname_22(off_410028, v5);
  LOBYTE(v6) = 1;
  dword_4EBC68 = (System::TObject *)unknown_libname_22(off_410028, v6);
  dword_4EBB90 = (System::TObject *)Inthreadsafe::TInThreadSafeList::TInThreadSafeList(off_4B52B0);
  LOBYTE(v7) = 1;
  dword_4EBB94 = (System::TObject *)unknown_libname_22(off_4946F4, v7);
  dword_4EBB98 = (System::TObject *)Inthreadsafe::TInThreadSafeList::TInThreadSafeList(off_4920F0);
  LOBYTE(v8) = 1;
  dword_4EBB9C = (System::TObject *)sub_491C8C((int *)off_491C1C, v8);
  LOBYTE(v9) = 1;
  dword_4EBBA0 = (System::TObject *)sub_499F9C(off_497AA8, v9);
  dword_4EBBA4 = (System::TObject *)Invokeregistry::THeaderList::THeaderList(off_4A7B0C);
  LOBYTE(v10) = 1;
  dword_4EBBA8 = (System::TObject *)sub_48E438((int *)off_48E384, v10);
  LOBYTE(v11) = 1;
  dword_4EBB88 = (System::TObject *)sub_4B4510(off_4B44AC, v11);
  dword_4EBB8C = Iwdatamodulepool::TObjectPool::TObjectPool(off_4ABDB0, 1);
  LOBYTE(v12) = 1;
  dword_4EBBAC = (System::TObject *)unknown_libname_22(off_410028, v12);
  LOBYTE(v13) = 1;
  dword_4EBBB0 = (System::TObject *)unknown_libname_22(off_410028, v13);
  LOBYTE(v14) = 1;
  dword_4EBBB4 = (System::TObject *)unknown_libname_22(off_40FAFC, v14);
  LOBYTE(v15) = 1;
  dword_4EBBB8 = sub_45552C(off_455424, v15);
  LOBYTE(v16) = 1;
  dword_4EBBBC = unknown_libname_22(off_410028, v16);
  LOBYTE(v17) = 1;
  dword_4EBBC0 = unknown_libname_22(off_410028, v17);
  LOBYTE(v18) = 1;
  dword_4EBBC4 = unknown_libname_22(off_410028, v18);
  LOBYTE(v19) = 1;
  dword_4EBBC8 = unknown_libname_22(off_40FAFC, v19);
  LOBYTE(v20) = 1;
  dword_4EBBCC = (System::TObject *)unknown_libname_22(off_410028, v20);
  dword_4EBBD0 = 0;
  LOBYTE(v21) = 1;
  dword_4EBC40 = (System::TObject *)sub_44FFF8(off_44FEBC, v21);
  LOBYTE(v22) = 1;
  dword_4EBC44 = (System::TObject *)sub_44FFF8(off_44FEBC, v22);
  LOBYTE(v23) = 1;
  dword_4EBC48 = (System::TObject *)sub_44FFF8(off_44FEBC, v23);
  LOBYTE(v24) = 1;
  dword_4EBC4C = (System::TObject *)sub_44FFF8(off_44FEBC, v24);
  LOBYTE(v25) = 1;
  dword_4EBC50 = sub_44FFF8(off_44FEBC, v25);
  LOBYTE(v26) = 1;
  dword_4EBC54 = sub_44FFF8(off_44FEBC, v26);
  LOBYTE(v27) = 1;
  dword_4EBC58 = sub_44FFF8(off_44FEBC, v27);
  LOBYTE(v28) = 1;
  dword_4EBC5C = sub_44FFF8(off_44FEBC, v28);
  unknown_libname_34(&dword_4EBC84);
  byte_4EBC88 = 0;
  dword_4EBC8C = 0;
  dword_4EBC90 = 0;
  dword_4EBC94 = 0;
  dword_4EBC7C = 0;
  dword_4EBC80 = 0;
  byte_4EBC78 = 0;
  byte_4EBC79 = 0;
  byte_4EBC9C = 1;
  dword_4EBCA0 = 0;
  dword_4EBCA4 = 0;
  dword_4EBCA8 = 0;
  dword_4EBCAC = 0;
  dword_4EBCB0 = 0;
  dword_4EBCB4 = 0;
  dword_4EBCB8 = 0;
  dword_4EBCBC = 0;
  System::__linkproc__ FillChar(dword_4EBBD4, 40, 0);
  Sysutils::Date();
  unknown_libname_96(a3);
  Sysutils::Time();
  unknown_libname_94((int)&v87, a3);
  Sysutils::IntToStr(v92);
  Sysutils::IntToStr(v91);
  sub_4E240C(v90, &v83);
  sub_4E240C(v89, &v82);
  sub_4E240C(v88, &v81);
  System::__linkproc__ LStrCatN(&System::AnsiString, 11, v29);
  ASSIGN(v84, System::AnsiString);
  v30 = REWRITTEXT(v84);
  __linkproc__ _IOTest(v30);
  v31 = CLOSE(v84);
  __linkproc__ _IOTest(v31);
  dword_4EBD20 = 99999;
  dword_4EBD28 = 0;
  dword_4EBD30 = 0;
  dword_4EBD3C = 0;
  dword_4EBD44 = 0;
  dword_4EBD4C = 0;
  dword_4EBD50 = 0;
  dword_4EBD54 = 0;
  dword_4EBD98 = 30;
  dword_4EBD9C = 30;
  dword_4EBDA0 = 5;
  dword_4EBDA4 = 5;
  dword_4EBDA8 = 10;
  dword_4EBDAC = 20;
  (*(void (__fastcall **)(_DWORD, _strings *, _DWORD))(**(_DWORD **)(*((_DWORD *)v96 + 181) + 520) + 52))(
    *(_DWORD *)(*((_DWORD *)v96 + 181) + 520),
    &str_Ready_to_load_i[1],
    **(_DWORD **)(*((_DWORD *)v96 + 181) + 520));
  unknown_libname_37(&v95, &str____setup_txt_0[1]);
  if ( (unsigned __int8)Sysutils::FileExists(v95) != 0 )
  {
    LOBYTE(v32) = 1;
    v93 = (System::TObject *)sub_45491C(off_454874, v32, v95);
    if ( v93 != nullptr )
    {
      ExceptionList = 0;
      dword_4EBC04 = (*(int (__fastcall **)(System::TObject *, _strings *, _strings *, _DWORD))(*(_DWORD *)v93 + 8))(
                       v93,
                       &str_Server_0[1],
                       &str_ServerIndex[1],
                       0);
      ExceptionList = 0;
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _DWORD, int *))v93)(
        v93,
        &str_Server_0[1],
        &str_ServerName[1],
        0,
        &v80);
      unknown_libname_36(&dword_4EBC08, v80);
      ExceptionList = 0;
      dword_4EBC0C = (*(int (__fastcall **)(System::TObject *, _strings *, _strings *, _DWORD))(*(_DWORD *)v93 + 8))(
                       v93,
                       &str_Server_0[1],
                       &str_ServerNumber[1],
                       0);
      ExceptionList = (int)&str_FALSE_0[1];
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _strings *, int *))v93)(
        v93,
        &str_Server_0[1],
        &str_VentureServer[1],
        &str_FALSE_0[1],
        &v94);
      byte_4EBC10 = unknown_libname_84(v94, &str_TRUE[1]) == 0;
      ExceptionList = (int)&str_FALSE_0[1];
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _strings *, int *))v93)(
        v93,
        &str_Server_0[1],
        &str_TestServer[1],
        &str_FALSE_0[1],
        &v94);
      byte_4EBC11 = unknown_libname_84(v94, &str_TRUE[1]) == 0;
      ExceptionList = 1;
      dword_4EBC14 = (*(int (__fastcall **)(System::TObject *, _strings *, _strings *, int))(*(_DWORD *)v93 + 8))(
                       v93,
                       &str_Server_0[1],
                       &str_TestLevel[1],
                       1);
      ExceptionList = 0;
      dword_4EBC18 = (*(int (__fastcall **)(System::TObject *, _strings *, _strings *, _DWORD))(*(_DWORD *)v93 + 8))(
                       v93,
                       &str_Server_0[1],
                       &str_TestGold[1],
                       0);
      ExceptionList = 5000;
      dword_4EBC1C = (*(int (__fastcall **)(System::TObject *, _strings *, _strings *, int))(*(_DWORD *)v93 + 8))(
                       v93,
                       &str_Server_0[1],
                       &str_TestServerUserL[1],
                       5000);
      ExceptionList = (int)&str_FALSE_0[1];
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _strings *, int *))v93)(
        v93,
        &str_Server_0[1],
        &str_ServiceMode[1],
        &str_FALSE_0[1],
        &v94);
      byte_4EBC20 = unknown_libname_84(v94, &str_TRUE[1]) == 0;
      ExceptionList = (int)&str_FALSE_0[1];
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _strings *, int *))v93)(
        v93,
        &str_Server_0[1],
        &str_NonPKServer[1],
        &str_FALSE_0[1],
        &v94);
      byte_4EBC21 = unknown_libname_84(v94, &str_TRUE[1]) == 0;
      ExceptionList = (int)&str_TRUE[1];
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _strings *, int *))v93)(
        v93,
        &str_Server_0[1],
        &str_ViewHackMessage[1],
        &str_TRUE[1],
        &v94);
      byte_4EBC22 = unknown_libname_84(v94, &str_TRUE[1]) == 0;
      ExceptionList = (int)&str_FALSE_0[1];
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _strings *, int *))v93)(
        v93,
        &str_Server_0[1],
        &str_ViewAdmissionFa[1],
        &str_FALSE_0[1],
        &v94);
      byte_4EBC23 = unknown_libname_84(v94, &str_TRUE[1]) == 0;
      ExceptionList = (int)&str_0_17[1];
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _strings *, int *))v93)(
        v93,
        &str_Server_0[1],
        &str_HomeMap[1],
        &str_0_17[1],
        &v79);
      unknown_libname_36(&dword_4EBCE0, v79);
      ExceptionList = 289;
      dword_4EBCE4 = (*(int (__fastcall **)(System::TObject *, _strings *, _strings *, int))(*(_DWORD *)v93 + 8))(
                       v93,
                       &str_Server_0[1],
                       &str_HomeX[1],
                       289);
      ExceptionList = 618;
      dword_4EBCE8 = (*(int (__fastcall **)(System::TObject *, _strings *, _strings *, int))(*(_DWORD *)v93 + 8))(
                       v93,
                       &str_Server_0[1],
                       &str_HomeY[1],
                       618);
      v86 = *((Scktcomp::TAbstractSocket **)v96 + 184);
      ExceptionList = (int)a127001_0;
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, char *, int *))v93)(
        v93,
        &str_Server_0[1],
        &str_DBAddr[1],
        a127001_0,
        &v78);
      Scktcomp::TAbstractSocket::SetAddress(v86, v78);
      ExceptionList = 6000;
      v33 = (*(int (__fastcall **)(System::TObject *, _strings *, _strings *, int))(*(_DWORD *)v93 + 8))(
              v93,
              &str_Server_0[1],
              &str_DBPort[1],
              6000);
      Scktcomp::TAbstractSocket::SetPort(v86, v33);
      Scktcomp::TAbstractSocket::SetActive(v86, true);
      ExceptionList = 0;
      dword_4EBC80 = (*(int (__fastcall **)(System::TObject *, _strings *, _strings *, _DWORD))(*(_DWORD *)v93 + 8))(
                       v93,
                       &str_Setup[1],
                       &str_ItemNumber[1],
                       0);
      ExceptionList = dword_4EBD98;
      dword_4EBD98 = (*(int (__fastcall **)(System::TObject *, _strings *, _strings *, int))(*(_DWORD *)v93 + 8))(
                       v93,
                       &str_Server_0[1],
                       &str_HumLimit[1],
                       dword_4EBD98);
      ExceptionList = dword_4EBD9C;
      dword_4EBD9C = (*(int (__fastcall **)(System::TObject *, _strings *, _strings *, int))(*(_DWORD *)v93 + 8))(
                       v93,
                       &str_Server_0[1],
                       &str_MonLimit[1],
                       dword_4EBD9C);
      ExceptionList = dword_4EBDA0;
      dword_4EBDA0 = (*(int (__fastcall **)(System::TObject *, _strings *, _strings *, int))(*(_DWORD *)v93 + 8))(
                       v93,
                       &str_Server_0[1],
                       &str_ZenLimit[1],
                       dword_4EBDA0);
      ExceptionList = dword_4EBDA4;
      dword_4EBDA4 = (*(int (__fastcall **)(System::TObject *, _strings *, _strings *, int))(*(_DWORD *)v93 + 8))(
                       v93,
                       &str_Server_0[1],
                       &str_NpcLimit[1],
                       dword_4EBDA4);
      ExceptionList = dword_4EBDA8;
      dword_4EBDA8 = (*(int (__fastcall **)(System::TObject *, _strings *, _strings *, int))(*(_DWORD *)v93 + 8))(
                       v93,
                       &str_Server_0[1],
                       &str_SocLimit[1],
                       dword_4EBDA8);
      ExceptionList = dword_4EBDAC;
      dword_4EBDAC = (*(int (__fastcall **)(System::TObject *, _strings *, _strings *, int))(*(_DWORD *)v93 + 8))(
                       v93,
                       &str_Server_0[1],
                       &str_DecLimit[1],
                       dword_4EBDAC);
      ExceptionList = dword_4E9F34;
      dword_4E9F34 = (*(int (__fastcall **)(System::TObject *, _strings *, _strings *, int))(*(_DWORD *)v93 + 8))(
                       v93,
                       &str_Server_0[1],
                       &str_SendBlock[1],
                       dword_4E9F34);
      ExceptionList = dword_4E9F38;
      dword_4E9F38 = (*(int (__fastcall **)(System::TObject *, _strings *, _strings *, int))(*(_DWORD *)v93 + 8))(
                       v93,
                       &str_Server_0[1],
                       &str_CheckBlock[1],
                       dword_4E9F38);
      ExceptionList = dword_4E9F3C;
      dword_4E9F3C = (*(int (__fastcall **)(System::TObject *, _strings *, _strings *, int))(*(_DWORD *)v93 + 8))(
                       v93,
                       &str_Server_0[1],
                       &str_AvailableBlock[1],
                       dword_4E9F3C);
      ExceptionList = dword_4E9F40;
      dword_4E9F40 = (*(int (__fastcall **)(System::TObject *, _strings *, _strings *, int))(*(_DWORD *)v93 + 8))(
                       v93,
                       &str_Server_0[1],
                       &str_GateLoad[1],
                       dword_4E9F40);
      ExceptionList = 500;
      dword_4EBC24 = (*(int (__fastcall **)(System::TObject *, _strings *, _strings *, int))(*(_DWORD *)v93 + 8))(
                       v93,
                       &str_Server_0[1],
                       &str_UserFull[1],
                       500);
      ExceptionList = 300;
      dword_4EBC28 = (*(int (__fastcall **)(System::TObject *, _strings *, _strings *, int))(*(_DWORD *)v93 + 8))(
                       v93,
                       &str_Server_0[1],
                       &str_ZenFastStep[1],
                       300);
      ExceptionList = (int)a127001_0;
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, char *, int *))v93)(
        v93,
        &str_Server_0[1],
        &str_MsgSrvAddr[1],
        a127001_0,
        &v77);
      unknown_libname_36(&dword_4EBCC0, v77);
      ExceptionList = 4900;
      dword_4EBCC4 = (*(int (__fastcall **)(System::TObject *, _strings *, _strings *, int))(*(_DWORD *)v93 + 8))(
                       v93,
                       &str_Server_0[1],
                       &str_MsgSrvPort[1],
                       4900);
      ExceptionList = (int)&str_192_168_0_152[1];
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _strings *, int *))v93)(
        v93,
        &str_Server_0[1],
        &str_LogServerAddr[1],
        &str_192_168_0_152[1],
        &v76);
      unknown_libname_36(&dword_4EBCC8, v76);
      ExceptionList = 10000;
      dword_4EBCCC = (*(int (__fastcall **)(System::TObject *, _strings *, _strings *, int))(*(_DWORD *)v93 + 8))(
                       v93,
                       &str_Server_0[1],
                       &str_LogServerPort[1],
                       10000);
      ExceptionList = 0;
      byte_4EBC6C = (*(int (__fastcall **)(System::TObject *, _strings *, _strings *, _DWORD))(*(_DWORD *)v93 + 16))(
                      v93,
                      &str_Server_0[1],
                      &str_DiscountForNigh[1],
                      0);
      ExceptionList = 2;
      dword_4EBC70 = (*(int (__fastcall **)(System::TObject *, _strings *, _strings *, int))(*(_DWORD *)v93 + 8))(
                       v93,
                       &str_Server_0[1],
                       &str_HalfFeeStart[1],
                       2);
      ExceptionList = 10;
      dword_4EBC74 = (*(int (__fastcall **)(System::TObject *, _strings *, _strings *, int))(*(_DWORD *)v93 + 8))(
                       v93,
                       &str_Server_0[1],
                       &str_HalfFeeEnd[1],
                       10);
      ExceptionList = (int)&str_D__[1];
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _strings *, int *))v93)(
        v93,
        &str_Share[1],
        &str_BaseDir[1],
        &str_D__[1],
        &v75);
      unknown_libname_36(&dword_4EBCD0, v75);
      dword_4EBCD8 = 1;
      ExceptionList = (int)&str_D__[1];
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _strings *, int *))v93)(
        v93,
        &str_Share[1],
        &str_GuildDir[1],
        &str_D__[1],
        &v74);
      unknown_libname_36(&dword_4EBCEC, v74);
      ExceptionList = (int)&str_D__[1];
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _strings *, int *))v93)(
        v93,
        &str_Share[1],
        &str_GuildFile[1],
        &str_D__[1],
        &v73);
      unknown_libname_36(&dword_4EBCF0, v73);
      ExceptionList = (int)&str_D__[1];
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _strings *, int *))v93)(
        v93,
        &str_Share[1],
        &str_VentureDir[1],
        &str_D__[1],
        &v72);
      unknown_libname_36(&dword_4EBCD4, v72);
      ExceptionList = (int)&str_D__[1];
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _strings *, int *))v93)(
        v93,
        &str_Share[1],
        &str_ConLogDir[1],
        &str_D__[1],
        &v71);
      unknown_libname_36(&dword_4EBCDC, v71);
      ExceptionList = (int)&str_D__[1];
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _strings *, int *))v93)(
        v93,
        &str_Share[1],
        &str_CastleDir[1],
        &str_D__[1],
        &v70);
      unknown_libname_36(&dword_4EBCF4, v70);
      ExceptionList = (int)&str___Envir_[1];
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _strings *, int *))v93)(
        v93,
        &str_Share[1],
        &str_EnvirDir[1],
        &str___Envir_[1],
        &v69);
      unknown_libname_36(&dword_4EBCF8, v69);
      ExceptionList = (int)&str___Map_[1];
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _strings *, int *))v93)(
        v93,
        &str_Share[1],
        &str_MapDir[1],
        &str___Map_[1],
        &v68);
      unknown_libname_36(&dword_4EBCFC, v68);
      ExceptionList = 0;
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _DWORD, int *))v93)(
        v93,
        &str_Setup[1],
        &str_ClientFile1[1],
        0,
        &v67);
      unknown_libname_36(&dword_4EBDF8, v67);
      ExceptionList = 0;
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _DWORD, int *))v93)(
        v93,
        &str_Setup[1],
        &str_ClientFile2[1],
        0,
        &v66);
      unknown_libname_36(&dword_4EBDFC, v66);
      ExceptionList = 0;
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _DWORD, int *))v93)(
        v93,
        &str_Setup[1],
        &str_ClientFile3[1],
        0,
        &v65);
      unknown_libname_36(&dword_4EBE00, v65);
      ExceptionList = 0;
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _DWORD, int *))v93)(
        v93,
        &str_Names[1],
        &str_ClothsMan[1],
        0,
        &v64);
      unknown_libname_36(&dword_4EBDB0, v64);
      ExceptionList = 0;
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _DWORD, int *))v93)(
        v93,
        &str_Names[1],
        &str_ClothsWoman[1],
        0,
        &v63);
      unknown_libname_36(&dword_4EBDB4, v63);
      ExceptionList = 0;
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _DWORD, int *))v93)(
        v93,
        &str_Names[1],
        &str_WoodenSword[1],
        0,
        &v62);
      unknown_libname_36(&dword_4EBDB8, v62);
      ExceptionList = 0;
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _DWORD, int *))v93)(
        v93,
        &str_Names[1],
        &str_Candle[1],
        0,
        &v61);
      unknown_libname_36(&dword_4EBDBC, v61);
      ExceptionList = 0;
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _DWORD, int *))v93)(
        v93,
        &str_Names[1],
        &str_BasicDrug[1],
        0,
        &v60);
      unknown_libname_36(&dword_4EBDC0, v60);
      ExceptionList = 0;
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _DWORD, int *))v93)(
        v93,
        &str_Names[1],
        &str_GoldStone[1],
        0,
        &v59);
      unknown_libname_36(&dword_4EBDC4, v59);
      ExceptionList = 0;
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _DWORD, int *))v93)(
        v93,
        &str_Names[1],
        &str_SilverStone[1],
        0,
        &v58);
      unknown_libname_36(&dword_4EBDC8, v58);
      ExceptionList = 0;
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _DWORD, int *))v93)(
        v93,
        &str_Names[1],
        &str_SteelStone[1],
        0,
        &v57);
      unknown_libname_36(&dword_4EBDCC, v57);
      ExceptionList = 0;
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _DWORD, int *))v93)(
        v93,
        &str_Names[1],
        &str_CopperStone[1],
        0,
        &v56);
      unknown_libname_36(&dword_4EBDD0, v56);
      ExceptionList = 0;
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _DWORD, int *))v93)(
        v93,
        &str_Names[1],
        &str_BlackStone[1],
        0,
        &v55);
      unknown_libname_36(&dword_4EBDD4, v55);
      ExceptionList = 0;
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _DWORD, int *))v93)(
        v93,
        &str_Names[1],
        &str_Zuma1[1],
        0,
        &v54);
      unknown_libname_36(&dword_4EBDD8, v54);
      ExceptionList = 0;
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _DWORD, int *))v93)(
        v93,
        &str_Names[1],
        &str_Zuma2[1],
        0,
        &v53);
      unknown_libname_36(&dword_4EBDDC, v53);
      ExceptionList = 0;
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _DWORD, int *))v93)(
        v93,
        &str_Names[1],
        &str_Zuma3[1],
        0,
        &v52);
      unknown_libname_36(&dword_4EBDE0, v52);
      ExceptionList = 0;
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _DWORD, int *))v93)(
        v93,
        &str_Names[1],
        &str_Zuma4[1],
        0,
        &v51);
      unknown_libname_36(&dword_4EBDE4, v51);
      ExceptionList = 0;
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _DWORD, int *))v93)(
        v93,
        &str_Names[1],
        &str_Bee[1],
        0,
        &v50);
      unknown_libname_36(&dword_4EBDE8, v50);
      ExceptionList = 0;
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _DWORD, int *))v93)(
        v93,
        &str_Names[1],
        &str_Spider[1],
        0,
        &v49);
      unknown_libname_36(&dword_4EBDEC, v49);
      ExceptionList = 0;
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _DWORD, int *))v93)(
        v93,
        &str_Names[1],
        &str_WomaHorn[1],
        0,
        &v48);
      unknown_libname_36(&dword_4EBDF0, v48);
      ExceptionList = 0;
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _DWORD, int *))v93)(
        v93,
        &str_Names[1],
        &str_ZumaPiece[1],
        0,
        &v47);
      unknown_libname_36(&dword_4EBDF4, v47);
      System::TObject::Free(v93);
    }
    (*(void (__fastcall **)(_DWORD, _strings *))(**(_DWORD **)(*((_DWORD *)v96 + 181) + 520) + 52))(
      *(_DWORD *)(*((_DWORD *)v96 + 181) + 520),
      &str__setup_txt_load[1]);
  }
  else
  {
    Dialogs::ShowMessage((Dialogs *)&str_Error__Cannot_f[1], v32);
  }
  ExceptionList = dword_4EBC08;
  Sysutils::Date();
  unknown_libname_103(LODWORD(a3), HIDWORD(*(unsigned __int64 *)&a3));
  Sysutils::Time();
  unknown_libname_104(LODWORD(a3), HIDWORD(*(unsigned __int64 *)&a3));
  System::__linkproc__ LStrCatN(&v46, 5, v34);
  v35 = Controls::TControl::SetText(v96, v46);
  sub_4E3E64(v35);
  unknown_libname_36(*((_DWORD *)v96 + 198) + 2092, dword_4EBCC8);
  v36 = dword_4EBCCC;
  *(_DWORD *)(*((_DWORD *)v96 + 198) + 2096) = dword_4EBCCC;
  LOBYTE(v36) = 1;
  unknown_libname_436(*((_DWORD *)v96 + 185), v36);
  v37 = *off_4EA42C;
  *(_DWORD *)(v37 + 188) = v96;
  *(_DWORD *)(v37 + 184) = sub_4E3C24;
  dword_4EBC98 = GetTickCount();
  dword_4EBD14 = GetTickCount();
  LOBYTE(v38) = 1;
  unknown_libname_436(*((_DWORD *)v96 + 186), v38);
  LOBYTE(v39) = 1;
  unknown_libname_436(*((_DWORD *)v96 + 182), v39);
  __writefsdword(0, (unsigned int)v42);
  v44 = &loc_4E3561;
  unknown_libname_35(&v45, 42);
  return unknown_libname_35(&v94, 2);
}


// ==================== [0x4E3E64] sub_4E3E64 (size: 510) ====================
int sub_4E3E64()
{
  System::TObject *v0; // eax
  int v1; // edx
  int v2; // eax
  int v3; // edx
  int v4; // eax
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-Ch] [ebp-48h] BYREF
  void *v7; // [esp-8h] [ebp-44h]
  int *v8; // [esp-4h] [ebp-40h]
  void *v9; // [esp+0h] [ebp-3Ch]
  int v10; // [esp+4h] [ebp-38h] BYREF
  int v11; // [esp+8h] [ebp-34h] BYREF
  char v12; // [esp+Ch] [ebp-30h]
  char v13; // [esp+Dh] [ebp-2Fh]
  int v14; // [esp+10h] [ebp-2Ch] BYREF
  int v15; // [esp+14h] [ebp-28h] BYREF
  int v16; // [esp+18h] [ebp-24h]
  void *v17; // [esp+1Ch] [ebp-20h]
  int v18; // [esp+20h] [ebp-1Ch]
  int v19; // [esp+24h] [ebp-18h] BYREF
  int v20; // [esp+28h] [ebp-14h]
  _BYTE *v21; // [esp+30h] [ebp-Ch] BYREF
  int i; // [esp+34h] [ebp-8h]
  int v23; // [esp+38h] [ebp-4h]
  int savedregs; // [esp+3Ch] [ebp+0h] BYREF

  v8 = &savedregs;
  v7 = &loc_4E4056;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  if ( *((_DWORD *)dword_4EBBB4 + 2) - 1 >= 0 )
  {
    v16 = *((_DWORD *)dword_4EBBB4 + 2);
    v23 = 0;
    do
    {
      v0 = (System::TObject *)Classes::TList::Get(dword_4EBBB4, v23);
      System::TObject::Free(v0);
      ++v23;
      --v16;
    }
    while ( v16 != 0 );
  }
  (*(void (__fastcall **)(System::TObject *, _DWORD))(*(_DWORD *)dword_4EBBB4 + 8))(
    dword_4EBBB4,
    *(_DWORD *)dword_4EBBB4);
  if ( (unsigned __int8)Sysutils::FileExists((const int)&str__servertable_tx[1]) != 0 )
  {
    LOBYTE(v1) = 1;
    v18 = unknown_libname_22(off_410028, v1);
    (*(void (__fastcall **)(int, _strings *))(*(_DWORD *)v18 + 88))(v18, &str__servertable_tx[1]);
    v2 = (*(int (__fastcall **)(int))(*(_DWORD *)v18 + 20))(v18) - 1;
    if ( v2 >= 0 )
    {
      v16 = v2 + 1;
      v23 = 0;
      do
      {
        (*(void (__fastcall **)(int, int, int *, int *, void *))(*(_DWORD *)v18 + 12))(v18, v23, &v15, v8, v7);
        sub_408F0C(v15, &v21);
        if ( v21 != nullptr && *v21 != 59 )
        {
          v12 = 32;
          v13 = 9;
          sub_453DBC(&v14, 1);
          unknown_libname_37(&v21, v14);
          if ( v21 != nullptr )
          {
            LOBYTE(v3) = 1;
            v17 = (void *)unknown_libname_22(off_410028, v3);
            for ( i = 0; i != 31; ++i )
            {
              if ( v21 == nullptr )
                break;
              v12 = 32;
              v13 = 9;
              sub_453DBC(&v11, 1);
              unknown_libname_37(&v21, v11);
              v12 = 32;
              v13 = 9;
              sub_453DBC(&v10, 1);
              unknown_libname_37(&v21, v10);
              if ( v20 != 0 && v19 != 0 )
              {
                v4 = sub_453B20(v19, 0);
                (*(void (__fastcall **)(void *, int, int))(*(_DWORD *)v17 + 56))(v17, v20, v4);
              }
            }
            Classes::TList::Add(dword_4EBBB4, v17);
          }
        }
        ++v23;
        --v16;
      }
      while ( v16 != 0 );
    }
  }
  else
  {
    Dialogs::ShowMessage((Dialogs *)&str_File_not_found_[1], v1);
  }
  __writefsdword(0, (unsigned int)v7);
  v9 = &loc_4E405D;
  unknown_libname_35(&v10, 2);
  unknown_libname_35(&v14, 2);
  return unknown_libname_35(&v19, 4);
}


// ==================== [0x4E4400] sub_4E4400 (size: 645) ====================
int __usercall sub_4E4400@<eax>(_DWORD *a1@<eax>, double a2@<st0>)
{
  int v2; // ecx
  int v3; // ecx
  int v4; // eax
  int v5; // eax
  int v6; // ecx
  int v7; // eax
  int v8; // eax
  int v9; // eax
  double v11; // [esp+4h] [ebp-324h] BYREF
  void *v12; // [esp+Ch] [ebp-31Ch]
  int *v13; // [esp+10h] [ebp-318h]
  void *v14; // [esp+14h] [ebp-314h]
  int v15; // [esp+18h] [ebp-310h] BYREF
  int v16; // [esp+1Ch] [ebp-30Ch] BYREF
  int v17; // [esp+20h] [ebp-308h] BYREF
  _DWORD v18[2]; // [esp+24h] [ebp-304h] BYREF
  int v19; // [esp+2Ch] [ebp-2FCh] BYREF
  _DWORD v20[2]; // [esp+30h] [ebp-2F8h] BYREF
  CHAR v21[460]; // [esp+38h] [ebp-2F0h] BYREF
  CHAR PathName[256]; // [esp+204h] [ebp-124h] BYREF
  int v23; // [esp+304h] [ebp-24h]
  int v24; // [esp+308h] [ebp-20h]
  int v25; // [esp+30Ch] [ebp-1Ch] BYREF
  int v26; // [esp+310h] [ebp-18h] BYREF
  int v27; // [esp+316h] [ebp-12h] BYREF
  unsigned __int16 v28; // [esp+31Ah] [ebp-Eh]
  unsigned __int16 v29; // [esp+31Ch] [ebp-Ch]
  unsigned __int16 v30; // [esp+31Eh] [ebp-Ah]
  unsigned __int16 v31; // [esp+320h] [ebp-8h]
  unsigned __int16 v32; // [esp+322h] [ebp-6h]
  _DWORD *v33; // [esp+324h] [ebp-4h]
  int savedregs; // [esp+328h] [ebp+0h] BYREF

  v16 = 0;
  v15 = 0;
  v18[1] = 0;
  v18[0] = 0;
  v17 = 0;
  v20[1] = 0;
  v20[0] = 0;
  v19 = 0;
  v26 = 0;
  v25 = 0;
  v33 = a1;
  v13 = &savedregs;
  v12 = &loc_4E4679;
  HIDWORD(v11) = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&v11 + 4);
  if ( (*(int (__fastcall **)(_DWORD *, _DWORD))(*a1 + 20))(a1, *a1) != 0 )
  {
    Sysutils::Date();
    unknown_libname_96(a2);
    Sysutils::Time();
    unknown_libname_94((int)&v27, a2);
    HIDWORD(v11) = dword_4EBCDC;
    Sysutils::IntToStr(v32);
    sub_4E240C(v31, v20);
    sub_4E240C(v30, &v19);
    System::__linkproc__ LStrCatN(&v26, 6, v2);
    if ( (unsigned __int8)Sysutils::FileExists(v26) == 0 )
    {
      sub_409610(PathName);
      CreateDirectoryA(PathName, nullptr);
    }
    HIDWORD(v11) = v26;
    Sysutils::IntToStr(dword_4EBC04);
    sub_4E240C(v29, v18);
    sub_4E240C(10 * (v28 / 0xAu), &v17);
    System::__linkproc__ LStrCatN(&v25, 8, v3);
    ASSIGN(v21, v25);
    if ( (unsigned __int8)Sysutils::FileExists(v25) != 0 )
      v4 = APPEND(v21);
    else
      v4 = REWRITTEXT(v21);
    __linkproc__ _IOTest(v4);
    v5 = (*(int (__fastcall **)(_DWORD *))(*v33 + 20))(v33) - 1;
    if ( v5 >= 0 )
    {
      v23 = v5 + 1;
      v24 = 0;
      do
      {
        HIDWORD(v11) = &str_1__10[1];
        (*(void (__fastcall **)(_DWORD *, int, int *, _strings *))(*v33 + 12))(v33, v24, &v15, &str_1__10[1]);
        System::__linkproc__ LStrCatN(&v16, 4, v6);
        v7 = sub_404264(v21, v16);
        v8 = WRITELN(v7);
        __linkproc__ _IOTest(v8);
        ++v24;
        --v23;
      }
      while ( v23 != 0 );
    }
    v9 = CLOSE(v21);
    __linkproc__ _IOTest(v9);
  }
  __writefsdword(0, (unsigned int)v12);
  v14 = &loc_4E4680;
  unknown_libname_35(&v15, 8);
  return unknown_libname_35(&v25, 2);
}


// ==================== [0x4E4848] _TFrmMain_StartTimerTimer (size: 1693) ====================
int __fastcall TFrmMain_StartTimerTimer(Forms::TCustomForm *a1, Dialogs *a2)
{
  int v2; // edx
  int v3; // edx
  int v4; // edx
  int v5; // edx
  int v6; // edx
  int v7; // edx
  int v8; // edx
  int v9; // edx
  int v10; // edx
  int v11; // edx
  int v12; // edx
  unsigned int v14[3]; // [esp-18h] [ebp-7Ch] BYREF
  unsigned int v15[2]; // [esp-Ch] [ebp-70h] BYREF
  int *v16; // [esp-4h] [ebp-68h]
  void *v17; // [esp+Ch] [ebp-58h] BYREF
  Dialogs *v18[2]; // [esp+10h] [ebp-54h] BYREF
  Dialogs *v19[2]; // [esp+18h] [ebp-4Ch] BYREF
  Dialogs *v20[2]; // [esp+20h] [ebp-44h] BYREF
  Dialogs *v21[2]; // [esp+28h] [ebp-3Ch] BYREF
  Dialogs *v22[2]; // [esp+30h] [ebp-34h] BYREF
  Dialogs *v23[2]; // [esp+38h] [ebp-2Ch] BYREF
  Dialogs *v24[2]; // [esp+40h] [ebp-24h] BYREF
  Dialogs *v25[2]; // [esp+48h] [ebp-1Ch] BYREF
  Dialogs *v26[3]; // [esp+50h] [ebp-14h] BYREF
  int v27; // [esp+5Ch] [ebp-8h]
  Forms::TCustomForm *v28; // [esp+60h] [ebp-4h]
  int savedregs; // [esp+64h] [ebp+0h] BYREF

  v26[1] = a2;
  v28 = a1;
  v16 = &savedregs;
  v15[1] = (unsigned int)&loc_4E4ED7;
  v15[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v15);
  unknown_libname_436(*((_DWORD *)a1 + 186), 0);
  v14[2] = (unsigned int)&savedregs;
  v14[1] = (unsigned int)&loc_4E4EA8;
  v14[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v14);
  v26[2] = (Dialogs *)3164;
  if ( (unsigned __int8)sub_4E46E0(v28) != 0 )
  {
    (*(void (__fastcall **)(_DWORD, _strings *))(**(_DWORD **)(*((_DWORD *)v28 + 181) + 520) + 52))(
      *(_DWORD *)(*((_DWORD *)v28 + 181) + 520),
      &str_Loading_StdItem[1]);
    v27 = sub_4855E0(*off_4EA6B8[0]);
    if ( v27 >= 0 )
    {
      (*(void (__fastcall **)(_DWORD, _strings *))(**(_DWORD **)(*((_DWORD *)v28 + 181) + 520) + 52))(
        *(_DWORD *)(*((_DWORD *)v28 + 181) + 520),
        &str_StdItem_DB_load[1]);
      (*(void (__fastcall **)(_DWORD, _strings *))(**(_DWORD **)(*((_DWORD *)v28 + 181) + 520) + 52))(
        *(_DWORD *)(*((_DWORD *)v28 + 181) + 520),
        &str_Loading_MiniMap[1]);
      v27 = sub_4890C0(*off_4EA6B8[0]);
      if ( v27 >= 0 )
      {
        (*(void (__fastcall **)(_DWORD, _strings *))(**(_DWORD **)(*((_DWORD *)v28 + 181) + 520) + 52))(
          *(_DWORD *)(*((_DWORD *)v28 + 181) + 520),
          &str_MiniMap_informa[1]);
        (*(void (__fastcall **)(_DWORD, _strings *))(**(_DWORD **)(*((_DWORD *)v28 + 181) + 520) + 52))(
          *(_DWORD *)(*((_DWORD *)v28 + 181) + 520),
          &str_Loading_MapFile[1]);
        v27 = sub_486D1C(*off_4EA6B8[0]);
        if ( v27 >= 0 )
        {
          (*(void (__fastcall **)(_DWORD, _strings *))(**(_DWORD **)(*((_DWORD *)v28 + 181) + 520) + 52))(
            *(_DWORD *)(*((_DWORD *)v28 + 181) + 520),
            &str_Mapfile_loaded_[1]);
          (*(void (__fastcall **)(_DWORD, _strings *))(**(_DWORD **)(*((_DWORD *)v28 + 181) + 520) + 52))(
            *(_DWORD *)(*((_DWORD *)v28 + 181) + 520),
            &str_Loading_Monster[1]);
          v27 = sub_485E04(*off_4EA6B8[0]);
          if ( v27 > 0 )
          {
            (*(void (__fastcall **)(_DWORD, _strings *))(**(_DWORD **)(*((_DWORD *)v28 + 181) + 520) + 52))(
              *(_DWORD *)(*((_DWORD *)v28 + 181) + 520),
              &str_Monster_DB_load[1]);
            (*(void (__fastcall **)(_DWORD, _strings *))(**(_DWORD **)(*((_DWORD *)v28 + 181) + 520) + 52))(
              *(_DWORD *)(*((_DWORD *)v28 + 181) + 520),
              &str_Loading_Magic_D[1]);
            v27 = sub_486330(*off_4EA6B8[0]);
            if ( v27 > 0 )
            {
              (*(void (__fastcall **)(_DWORD, _strings *))(**(_DWORD **)(*((_DWORD *)v28 + 181) + 520) + 52))(
                *(_DWORD *)(*((_DWORD *)v28 + 181) + 520),
                &str_Magic_DB_loaded[1]);
              (*(void (__fastcall **)(_DWORD, _strings *))(**(_DWORD **)(*((_DWORD *)v28 + 181) + 520) + 52))(
                *(_DWORD *)(*((_DWORD *)v28 + 181) + 520),
                &str_Loading_MonGen_[1]);
              v27 = sub_4867F4(*off_4EA6B8[0]);
              if ( v27 > 0 )
              {
                (*(void (__fastcall **)(_DWORD, _strings *))(**(_DWORD **)(*((_DWORD *)v28 + 181) + 520) + 52))(
                  *(_DWORD *)(*((_DWORD *)v28 + 181) + 520),
                  &str_MonGen_txt_load[1]);
                (*(void (__fastcall **)(_DWORD, _strings *))(**(_DWORD **)(*((_DWORD *)v28 + 181) + 520) + 52))(
                  *(_DWORD *)(*((_DWORD *)v28 + 181) + 520),
                  &str_Loading_UnbindL[1]);
                v27 = sub_489240(*off_4EA6B8[0]);
                if ( v27 >= 0 )
                {
                  (*(void (__fastcall **)(_DWORD, _strings *))(**(_DWORD **)(*((_DWORD *)v28 + 181) + 520) + 52))(
                    *(_DWORD *)(*((_DWORD *)v28 + 181) + 520),
                    &str_UnbindList_info[1]);
                  (*(void (__fastcall **)(_DWORD, _strings *))(**(_DWORD **)(*((_DWORD *)v28 + 181) + 520) + 52))(
                    *(_DWORD *)(*((_DWORD *)v28 + 181) + 520),
                    &str_Loading_MapQues[1]);
                  v27 = sub_489414(*off_4EA6B8[0]);
                  if ( v27 >= 0 )
                  {
                    (*(void (__fastcall **)(_DWORD, _strings *))(**(_DWORD **)(*((_DWORD *)v28 + 181) + 520) + 52))(
                      *(_DWORD *)(*((_DWORD *)v28 + 181) + 520),
                      &str_MapQuest_inform[1]);
                    (*(void (__fastcall **)(_DWORD, _strings *))(**(_DWORD **)(*((_DWORD *)v28 + 181) + 520) + 52))(
                      *(_DWORD *)(*((_DWORD *)v28 + 181) + 520),
                      &str_Loading_QuestDi[1]);
                    v27 = sub_489840(*off_4EA6B8[0]);
                    if ( v27 >= 0 )
                    {
                      (*(void (__fastcall **)(_DWORD, _strings *))(**(_DWORD **)(*((_DWORD *)v28 + 181) + 520) + 52))(
                        *(_DWORD *)(*((_DWORD *)v28 + 181) + 520),
                        &str_QuestDiary_info[1]);
                      if ( (unsigned __int8)sub_455F10(&str__Abuse_txt_0[1]) != 0 )
                        (*(void (__fastcall **)(_DWORD, _strings *))(**(_DWORD **)(*((_DWORD *)v28 + 181) + 520) + 52))(
                          *(_DWORD *)(*((_DWORD *)v28 + 181) + 520),
                          &str__Abuse_txt_load[1]);
                      if ( (unsigned __int8)sub_4E40B0(&str_Notice_LineNoti_1[1]) != 0 )
                        (*(void (__fastcall **)(_DWORD, _strings *))(**(_DWORD **)(*((_DWORD *)v28 + 181) + 520) + 52))(
                          *(_DWORD *)(*((_DWORD *)v28 + 181) + 520),
                          &str_Notice_LineNoti_2[1]);
                      else
                        (*(void (__fastcall **)(_DWORD, char *))(**(_DWORD **)(*((_DWORD *)v28 + 181) + 520) + 52))(
                          *(_DWORD *)(*((_DWORD *)v28 + 181) + 520),
                          aNoticeLinenoti_3);
                      sub_487630(*off_4EA6B8[0]);
                      (*(void (__fastcall **)(_DWORD, _strings *))(**(_DWORD **)(*((_DWORD *)v28 + 181) + 520) + 52))(
                        *(_DWORD *)(*((_DWORD *)v28 + 181) + 520),
                        &str_AdminList_loade[1]);
                      sub_49A078(dword_4EBBA0);
                      (*(void (__fastcall **)(_DWORD, _strings *))(**(_DWORD **)(*((_DWORD *)v28 + 181) + 520) + 52))(
                        *(_DWORD *)(*((_DWORD *)v28 + 181) + 520),
                        &str_GuildList_loade[1]);
                      sub_48E564(dword_4EBBA8);
                      (*(void (__fastcall **)(_DWORD, _strings *))(**(_DWORD **)(*((_DWORD *)v28 + 181) + 520) + 52))(
                        *(_DWORD *)(*((_DWORD *)v28 + 181) + 520),
                        &str_Castle_initiali[1]);
                      if ( dword_4EBC04 != 0 )
                        sub_495E68(*off_4EA16C[0]);
                      else
                        sub_4966B0(*off_4EA014);
                      sub_4E5F2C(v28);
                      byte_4EBC78 = 1;
                      Sleep(0x1F4u);
                      LOBYTE(v11) = 1;
                      unknown_libname_436(*((_DWORD *)v28 + 185), v11);
                      dword_4EBD18 = GetTickCount();
                      dword_4EBD1C = 0;
                      dword_4EBD48 = GetTickCount();
                      LOBYTE(v12) = 1;
                      unknown_libname_436(*((_DWORD *)v28 + 183), v12);
                      __writefsdword(0, v14[0]);
                    }
                    else
                    {
                      Sysutils::IntToStr(v27);
                      System::__linkproc__ LStrCat3((int)v18, &str_QuestDiary___tx[1], v17);
                      Dialogs::ShowMessage(v18[0], v10);
                      Forms::TCustomForm::Close(v28);
                      __writefsdword(0, v14[0]);
                    }
                  }
                  else
                  {
                    Sysutils::IntToStr(v27);
                    System::__linkproc__ LStrCat3((int)v19, &str_MapQuest_txt___[1], v18[1]);
                    Dialogs::ShowMessage(v19[0], v9);
                    Forms::TCustomForm::Close(v28);
                    __writefsdword(0, v14[0]);
                  }
                }
                else
                {
                  Sysutils::IntToStr(v27);
                  System::__linkproc__ LStrCat3((int)v20, &str_UnbindList_txt_[1], v19[1]);
                  Dialogs::ShowMessage(v20[0], v8);
                  Forms::TCustomForm::Close(v28);
                  __writefsdword(0, v14[0]);
                }
              }
              else
              {
                Sysutils::IntToStr(v27);
                System::__linkproc__ LStrCat3((int)v21, &str_MonGen_txt____[1], v20[1]);
                Dialogs::ShowMessage(v21[0], v7);
                Forms::TCustomForm::Close(v28);
                __writefsdword(0, v14[0]);
              }
            }
            else
            {
              Sysutils::IntToStr(v27);
              System::__linkproc__ LStrCat3((int)v22, &str_Magic_DB_______[1], v21[1]);
              Dialogs::ShowMessage(v22[0], v6);
              Forms::TCustomForm::Close(v28);
              __writefsdword(0, v14[0]);
            }
          }
          else
          {
            Sysutils::IntToStr(v27);
            System::__linkproc__ LStrCat3((int)v23, &str_Monster_DB_____[1], v22[1]);
            Dialogs::ShowMessage(v23[0], v5);
            Forms::TCustomForm::Close(v28);
            __writefsdword(0, v14[0]);
          }
        }
        else
        {
          Sysutils::IntToStr(v27);
          System::__linkproc__ LStrCat3((int)v24, &str________________0[1], v23[1]);
          Dialogs::ShowMessage(v24[0], v4);
          Forms::TCustomForm::Close(v28);
          __writefsdword(0, v14[0]);
        }
      }
      else
      {
        Sysutils::IntToStr(v27);
        System::__linkproc__ LStrCat3((int)v25, &str_MiniMap_txt___M[1], v24[1]);
        Dialogs::ShowMessage(v25[0], v3);
        Forms::TCustomForm::Close(v28);
        __writefsdword(0, v14[0]);
      }
    }
    else
    {
      Sysutils::IntToStr(v27);
      System::__linkproc__ LStrCat3((int)v26, &str_StdItems_DB___I[1], v25[1]);
      Dialogs::ShowMessage(v26[0], v2);
      Forms::TCustomForm::Close(v28);
      __writefsdword(0, v14[0]);
    }
  }
  else
  {
    Forms::TCustomForm::Close(v28);
    __writefsdword(0, v14[0]);
  }
  __writefsdword(0, v15[0]);
  v16 = (int *)&loc_4E4EDE;
  return unknown_libname_35(&v17, 18);
}


// ==================== [0x4E5418] _TFrmMain_Timer1Timer (size: 2307) ====================
int __usercall TFrmMain_Timer1Timer@<eax>(int a1@<eax>, int a2@<edx>, double a3@<st0>)
{
  int v3; // ecx
  int v4; // eax
  int v5; // eax
  int v6; // eax
  int v7; // eax
  int v8; // eax
  int v9; // eax
  int v10; // edx
  int v11; // ecx
  int v12; // eax
  int v13; // ecx
  int v14; // eax
  int v15; // eax
  int v16; // ecx
  int v17; // ecx
  int v18; // ecx
  int v19; // ecx
  int v20; // ecx
  int v21; // ecx
  int v22; // ecx
  _strings *v24; // [esp-18h] [ebp-36Ch] BYREF
  _strings *v25; // [esp-14h] [ebp-368h] BYREF
  _strings *v26; // [esp-10h] [ebp-364h]
  _strings *ExceptionList; // [esp-Ch] [ebp-360h] BYREF
  void *v28; // [esp-8h] [ebp-35Ch]
  int *v29; // [esp-4h] [ebp-358h]
  void *v30; // [esp+0h] [ebp-354h]
  void *v31[6]; // [esp+Ch] [ebp-348h] BYREF
  int v32; // [esp+24h] [ebp-330h] BYREF
  __int64 TickCount; // [esp+28h] [ebp-32Ch]
  _BYTE v34[68]; // [esp+30h] [ebp-324h] BYREF
  _BYTE v35[64]; // [esp+74h] [ebp-2E0h] BYREF
  _BYTE v36[36]; // [esp+B4h] [ebp-2A0h] BYREF
  int v37[8]; // [esp+D8h] [ebp-27Ch] BYREF
  int v38[7]; // [esp+F8h] [ebp-25Ch] BYREF
  int v39[4]; // [esp+114h] [ebp-240h] BYREF
  int v40[4]; // [esp+124h] [ebp-230h] BYREF
  int v41; // [esp+134h] [ebp-220h] BYREF
  _DWORD v42[2]; // [esp+138h] [ebp-21Ch] BYREF
  int v43; // [esp+144h] [ebp-210h] BYREF
  int v44; // [esp+148h] [ebp-20Ch] BYREF
  _BYTE v45[460]; // [esp+14Ch] [ebp-208h] BYREF
  int v46; // [esp+318h] [ebp-3Ch]
  int v47; // [esp+31Ch] [ebp-38h]
  System::TObject *v48; // [esp+320h] [ebp-34h]
  _DWORD *v49; // [esp+324h] [ebp-30h]
  int v50; // [esp+328h] [ebp-2Ch] BYREF
  int v51; // [esp+32Ch] [ebp-28h] BYREF
  int v52; // [esp+330h] [ebp-24h] BYREF
  unsigned __int16 v53; // [esp+336h] [ebp-1Eh]
  unsigned __int16 v54; // [esp+338h] [ebp-1Ch]
  unsigned __int16 v55; // [esp+33Ah] [ebp-1Ah]
  double v56; // [esp+33Ch] [ebp-18h]
  char v57; // [esp+347h] [ebp-Dh]
  signed int v58; // [esp+348h] [ebp-Ch]
  int i; // [esp+34Ch] [ebp-8h]
  int v60; // [esp+350h] [ebp-4h]
  int savedregs; // [esp+354h] [ebp+0h] BYREF

  v46 = a2;
  v60 = a1;
  v29 = &savedregs;
  v28 = &loc_4E5D0D;
  ExceptionList = (_strings *)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  v26 = (_strings *)&savedregs;
  v25 = (_strings *)&loc_4E5709;
  v24 = (_strings *)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&v24);
  sub_450080(dword_4EBC40);
  if ( (*(int (__fastcall **)(_DWORD, _DWORD, int, _strings *, _strings *))(**(_DWORD **)(*(_DWORD *)(v60 + 724) + 520)
                                                                          + 20))(
         *(_DWORD *)(*(_DWORD *)(v60 + 724) + 520),
         **(_DWORD **)(*(_DWORD *)(v60 + 724) + 520),
         v3,
         v25,
         v24) > 500 )
    (*(void (__fastcall **)(_DWORD))(**(_DWORD **)(*(_DWORD *)(v60 + 724) + 520) + 64))(*(_DWORD *)(*(_DWORD *)(v60 + 724)
                                                                                                  + 520));
  v57 = 1;
  if ( (*(int (__fastcall **)(System::TObject *))(*(_DWORD *)dword_4EBC60 + 20))(dword_4EBC60) > 0 )
  {
    ExceptionList = (_strings *)&savedregs;
    v26 = (_strings *)&loc_4E550D;
    v25 = (_strings *)NtCurrentTeb()->NtTib.ExceptionList;
    __writefsdword(0, (unsigned int)&v25);
    if ( (unsigned __int8)Sysutils::FileExists(System::AnsiString) != 0 )
    {
      ASSIGN(v45, System::AnsiString);
      v4 = APPEND(v45);
    }
    else
    {
      ASSIGN(v45, System::AnsiString);
      v4 = REWRITTEXT(v45);
    }
    __linkproc__ _IOTest(v4);
    v57 = 0;
    __writefsdword(0, (unsigned int)v25);
  }
  v5 = (*(int (__fastcall **)(System::TObject *))(*(_DWORD *)dword_4EBC60 + 20))(dword_4EBC60) - 1;
  if ( v5 >= 0 )
  {
    v47 = v5 + 1;
    i = 0;
    do
    {
      (*(void (__fastcall **)(System::TObject *, int, int *))(*(_DWORD *)dword_4EBC60 + 12))(dword_4EBC60, i, &v44);
      (*(void (__fastcall **)(_DWORD, int))(**(_DWORD **)(*(_DWORD *)(v60 + 724) + 520) + 52))(
        *(_DWORD *)(*(_DWORD *)(v60 + 724) + 520),
        v44);
      if ( v57 == 0 )
      {
        (*(void (__fastcall **)(System::TObject *, int, int *))(*(_DWORD *)dword_4EBC60 + 12))(dword_4EBC60, i, &v43);
        v6 = sub_404264(v45, v43);
        v7 = WRITELN(v6);
        __linkproc__ _IOTest(v7);
      }
      ++i;
      --v47;
    }
    while ( v47 != 0 );
  }
  (*(void (__fastcall **)(System::TObject *))(*(_DWORD *)dword_4EBC60 + 64))(dword_4EBC60);
  if ( v57 == 0 )
  {
    v8 = CLOSE(v45);
    __linkproc__ _IOTest(v8);
  }
  v9 = (*(int (__fastcall **)(System::TObject *))(*(_DWORD *)dword_4EBC64 + 20))(dword_4EBC64) - 1;
  if ( v9 >= 0 )
  {
    v47 = v9 + 1;
    i = 0;
    do
    {
      LOBYTE(v10) = 1;
      v48 = (System::TObject *)unknown_libname_22(off_4102D4, v10);
      ExceptionList = (_strings *)&savedregs;
      v26 = (_strings *)&loc_4E56B2;
      v25 = (_strings *)NtCurrentTeb()->NtTib.ExceptionList;
      __writefsdword(0, (unsigned int)&v25);
      v24 = &str_1__11[1];
      Sysutils::IntToStr(dword_4EBC0C);
      Sysutils::IntToStr(dword_4EBC04);
      (*(void (__fastcall **)(System::TObject *, int, _DWORD *, _strings *, _DWORD, _strings *))(*(_DWORD *)dword_4EBC64
                                                                                               + 12))(
        dword_4EBC64,
        i,
        v42,
        &str___114[1],
        v42[1],
        &str___114[1]);
      System::__linkproc__ LStrCatN(&v51, 6, v11);
      ExceptionList = (_strings *)unknown_libname_43(v51);
      v12 = System::UniqueString(&v51);
      (*(void (__fastcall **)(System::TObject *, int))(*(_DWORD *)v48 + 8))(v48, v12);
      sub_45C49C(*(_DWORD *)(v60 + 792), v48);
      __writefsdword(0, (unsigned int)v28);
      v30 = &loc_4E56B9;
      System::TObject::Free(v48);
      ++i;
      --v47;
    }
    while ( v47 != 0 );
  }
  (*(void (__fastcall **)(System::TObject *))(*(_DWORD *)dword_4EBC64 + 64))(dword_4EBC64);
  if ( (*(int (__fastcall **)(System::TObject *))(*(_DWORD *)dword_4EBC68 + 20))(dword_4EBC68) > 0 )
  {
    sub_4E4400(dword_4EBC68, a3);
    (*(void (__fastcall **)(System::TObject *))(*(_DWORD *)dword_4EBC68 + 64))(dword_4EBC68);
  }
  __writefsdword(0, (unsigned int)v28);
  v30 = &loc_4E5710;
  Webadapt::TBaseAdapterErrorsList::GetErrorCount(dword_4EBC40);
  if ( dword_4EBC04 != 0 )
  {
    if ( *(_BYTE *)(*(_DWORD *)(*(_DWORD *)(*off_4EA16C[0] + 720) + 128) + 8) != 0 )
      unknown_libname_37(&v52, &str__S_[1]);
    else
      unknown_libname_37(&v52, &str____[1]);
  }
  else
  {
    unknown_libname_37(&v52, &str__M_[1]);
  }
  v58 = (GetTickCount() - dword_4EBD14) / 0x3E8;
  v55 = v58 / 3600;
  v54 = v58 % 3600 / 60;
  v53 = v58 % 60;
  Sysutils::IntToStr(v55);
  ExceptionList = (_strings *)v40[3];
  v26 = &str___3[1];
  Sysutils::IntToStr(v54);
  v25 = (_strings *)v40[2];
  v24 = &str___3[1];
  Sysutils::IntToStr(v53);
  System::__linkproc__ LStrCatN(&v41, 7, v13);
  Controls::TControl::SetText(*(Controls::TControl **)(v60 + 756), v41);
  ExceptionList = &str___10[1];
  Sysutils::IntToStr(*((_DWORD *)dword_4EBB8C + 31));
  v26 = (_strings *)v39[3];
  v25 = &str_____[1];
  v14 = unknown_libname_674(dword_4EBB8C);
  Sysutils::IntToStr(v14);
  v24 = (_strings *)v39[2];
  v15 = sub_4AE7C0(dword_4EBB8C);
  Sysutils::IntToStr(v15);
  System::__linkproc__ LStrCatN(v40, 6, v16);
  Controls::TControl::SetText(*(Controls::TControl **)(v60 + 764), v40[0]);
  ExceptionList = &str_Run[1];
  Sysutils::IntToStr(dword_4EBD24);
  v26 = (_strings *)v38[6];
  v25 = &str___116[1];
  Sysutils::IntToStr(dword_4EBD20);
  v24 = (_strings *)v38[5];
  Sysutils::IntToStr(dword_4EBD2C);
  Sysutils::IntToStr(dword_4EBD28);
  Sysutils::IntToStr(dword_4EBD34);
  Sysutils::IntToStr(dword_4EBD30);
  System::__linkproc__ LStrCatN(v39, 14, v17);
  Controls::TControl::SetText(*(Controls::TControl **)(v60 + 768), v39[0]);
  ExceptionList = &str_Hum[1];
  Sysutils::IntToStr(dword_4EBD38);
  v26 = (_strings *)v37[7];
  v25 = &str___116[1];
  Sysutils::IntToStr(dword_4EBD3C);
  v24 = (_strings *)v37[6];
  Sysutils::IntToStr(dword_4EBD40);
  Sysutils::IntToStr(dword_4EBD44);
  Sysutils::IntToStr(dword_4EBD4C);
  Sysutils::IntToStr(dword_4EBD50);
  Sysutils::IntToStr(dword_4EBD54);
  System::__linkproc__ LStrCatN(v38, 17, v18);
  Controls::TControl::SetText(*(Controls::TControl **)(v60 + 772), v38[0]);
  unknown_libname_16(v36, dword_4EBD58);
  LOBYTE(v19) = 33;
  System::__linkproc__ PStrNCat(v36, &dword_4E5E14, v19);
  unknown_libname_16(v35, v36);
  LOBYTE(v20) = 63;
  System::__linkproc__ PStrNCat(v35, dword_4EBD78, v20);
  unknown_libname_16(v34, v35);
  LOBYTE(v21) = 67;
  System::__linkproc__ PStrNCat(v34, dword_4E5E18, v21);
  unknown_libname_41();
  Controls::TControl::SetText(*(Controls::TControl **)(v60 + 800), v37[0]);
  TickCount = GetTickCount();
  v56 = (double)TickCount / flt_4E5E20;
  if ( v56 >= flt_4E5E24 )
    Graphics::TFont::SetColor(*(_DWORD *)(*(_DWORD *)(v60 + 796) + 88), 255);
  sub_453C60(v56);
  System::__linkproc__ LStrCat((int)&v32, &str_Day[1]);
  Controls::TControl::SetText(*(Controls::TControl **)(v60 + 796), v32);
  unknown_libname_34(&v51);
  for ( i = 0; i != 20; ++i )
  {
    v49 = (_DWORD *)((char *)dword_4EBB84 + 100 * i + 1280);
    if ( (System::TObject *)((char *)dword_4EBB84 + 100 * i) != (System::TObject *)-1280 && v49[1] != 0 )
    {
      if ( (int)v49[22] >= 1024 )
      {
        Sysutils::IntToStr(v49[22] / 1024);
        System::__linkproc__ LStrCat3((int)&v50, v31[4], &str_kb_[1]);
      }
      else
      {
        Sysutils::IntToStr(v49[22]);
        System::__linkproc__ LStrCat3((int)&v50, v31[5], &str_b_[1]);
      }
      ExceptionList = (_strings *)v51;
      v26 = &str__G[1];
      Sysutils::IntToStr(i + 1);
      v25 = (_strings *)v31[3];
      v24 = &str___[1];
      Sysutils::IntToStr(v49[18]);
      Sysutils::IntToStr(v49[19]);
      Sysutils::IntToStr(v49[24]);
      System::__linkproc__ LStrCatN(&v51, 11, v22);
    }
  }
  Controls::TControl::SetText(*(Controls::TControl **)(v60 + 776), v51);
  ++dword_4EBD20;
  --dword_4EBD28;
  --dword_4EBD30;
  --dword_4EBD3C;
  --dword_4EBD44;
  --dword_4EBD50;
  __writefsdword(0, (unsigned int)v28);
  v30 = &loc_4E5D14;
  unknown_libname_35(v31, 7);
  unknown_libname_35(v37, 29);
  return unknown_libname_35(&v50, 3);
}


// ==================== [0x4E5E70] _TFrmMain_SaveVariableTimerTimer (size: 24) ====================
int __fastcall TFrmMain_SaveVariableTimerTimer(int a1, int a2)
{
  return sub_4E3D44(a2, a1);
}


// ==================== [0x4E5F2C] sub_4E5F2C (size: 786) ====================
int __fastcall sub_4E5F2C(Forms::TCustomForm *a1)
{
  int v1; // edx
  int v2; // edx
  int v3; // edx
  int v4; // edx
  int v5; // edx
  struct _EXCEPTION_REGISTRATION_RECORD *v7; // [esp-18h] [ebp-54h] BYREF
  void *v8; // [esp-14h] [ebp-50h]
  int *v9; // [esp-10h] [ebp-4Ch]
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-Ch] [ebp-48h] BYREF
  void *v11; // [esp-8h] [ebp-44h]
  int *v12; // [esp-4h] [ebp-40h]
  void *v13; // [esp+0h] [ebp-3Ch]
  void *v14; // [esp+Ch] [ebp-30h] BYREF
  Dialogs *v15[2]; // [esp+10h] [ebp-2Ch] BYREF
  Dialogs *v16[2]; // [esp+18h] [ebp-24h] BYREF
  Dialogs *v17[2]; // [esp+20h] [ebp-1Ch] BYREF
  Dialogs *v18[2]; // [esp+28h] [ebp-14h] BYREF
  Dialogs *v19; // [esp+30h] [ebp-Ch] BYREF
  int v20; // [esp+34h] [ebp-8h]
  Forms::TCustomForm *v21; // [esp+38h] [ebp-4h]
  int savedregs; // [esp+3Ch] [ebp+0h] BYREF

  v21 = a1;
  v12 = &savedregs;
  v11 = &loc_4E6230;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  v9 = &savedregs;
  v8 = &loc_4E6201;
  v7 = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&v7);
  sub_48D3F8(*off_4EA3DC);
  (*(void (__fastcall **)(_DWORD, _strings *, _DWORD))(**(_DWORD **)(*((_DWORD *)v21 + 181) + 520) + 52))(
    *(_DWORD *)(*((_DWORD *)v21 + 181) + 520),
    &str_IDSoc_initializ[1],
    **(_DWORD **)(*((_DWORD *)v21 + 181) + 520));
  Soapattachintf::TSoapDataList::Clear(dword_4EBB90);
  (*(void (__fastcall **)(_DWORD, _strings *))(**(_DWORD **)(*((_DWORD *)v21 + 181) + 520) + 52))(
    *(_DWORD *)(*((_DWORD *)v21 + 181) + 520),
    &str_GlobalEnvir_loa[1]);
  sub_4E5E88(v21);
  (*(void (__fastcall **)(_DWORD, _strings *))(**(_DWORD **)(*((_DWORD *)v21 + 181) + 520) + 52))(
    *(_DWORD *)(*((_DWORD *)v21 + 181) + 520),
    &str_MakeStoneMines_[1]);
  v20 = sub_48784C(*off_4EA6B8[0]);
  if ( v20 >= 0 )
  {
    (*(void (__fastcall **)(_DWORD, _strings *))(**(_DWORD **)(*((_DWORD *)v21 + 181) + 520) + 52))(
      *(_DWORD *)(*((_DWORD *)v21 + 181) + 520),
      &str_Merchant_loaded[1]);
    if ( byte_4EBC10 != 0 || (v20 = sub_488A68(*off_4EA6B8[0])) >= 0 )
    {
      v20 = sub_488178(*off_4EA6B8[0]);
      if ( v20 >= 0 )
      {
        (*(void (__fastcall **)(_DWORD, _strings *))(**(_DWORD **)(*((_DWORD *)v21 + 181) + 520) + 52))(
          *(_DWORD *)(*((_DWORD *)v21 + 181) + 520),
          &str_Npc_loaded__[1]);
        v20 = sub_488CDC(*off_4EA6B8[0]);
        if ( v20 >= 0 )
        {
          (*(void (__fastcall **)(_DWORD, _strings *))(**(_DWORD **)(*((_DWORD *)v21 + 181) + 520) + 52))(
            *(_DWORD *)(*((_DWORD *)v21 + 181) + 520),
            &str_MakeItem_loaded[1]);
          v20 = sub_488EF0(*off_4EA6B8[0]);
          if ( v20 >= 0 )
          {
            (*(void (__fastcall **)(_DWORD, _strings *))(**(_DWORD **)(*((_DWORD *)v21 + 181) + 520) + 52))(
              *(_DWORD *)(*((_DWORD *)v21 + 181) + 520),
              &str_StartPoints_loa[1]);
            Classes::TThread::Resume(dword_4EBB88);
            (*(void (__fastcall **)(_DWORD, _strings *))(**(_DWORD **)(*((_DWORD *)v21 + 181) + 520) + 52))(
              *(_DWORD *)(*((_DWORD *)v21 + 181) + 520),
              &str_F_Engine_Resume[1]);
            VarCmplx::_16424(dword_4EBB8C);
            (*(void (__fastcall **)(_DWORD, _strings *))(**(_DWORD **)(*((_DWORD *)v21 + 181) + 520) + 52))(
              *(_DWORD *)(*((_DWORD *)v21 + 181) + 520),
              &str_U_Engine_initia[1]);
          }
          else
          {
            Sysutils::IntToStr(v20);
            System::__linkproc__ LStrCat3((int)v15, &str_StartPoint_txt_[1], v14);
            Dialogs::ShowMessage(v15[0], v5);
            Forms::TCustomForm::Close(v21);
          }
          __writefsdword(0, (unsigned int)v8);
        }
        else
        {
          Sysutils::IntToStr(v20);
          System::__linkproc__ LStrCat3((int)v16, &str_MakeItem_txt___[1], v15[1]);
          Dialogs::ShowMessage(v16[0], v4);
          Forms::TCustomForm::Close(v21);
          __writefsdword(0, (unsigned int)v8);
        }
      }
      else
      {
        Sysutils::IntToStr(v20);
        System::__linkproc__ LStrCat3((int)v17, &str_Npc_txt_______[1], v16[1]);
        Dialogs::ShowMessage(v17[0], v3);
        Forms::TCustomForm::Close(v21);
        __writefsdword(0, (unsigned int)v8);
      }
    }
    else
    {
      Sysutils::IntToStr(v20);
      System::__linkproc__ LStrCat3((int)v18, &str_Guardlist_txt__[1], v17[1]);
      Dialogs::ShowMessage(v18[0], v2);
      Forms::TCustomForm::Close(v21);
      __writefsdword(0, (unsigned int)v8);
    }
  }
  else
  {
    Sysutils::IntToStr(v20);
    System::__linkproc__ LStrCat3((int)&v19, &str_merchant_txt___[1], v18[1]);
    Dialogs::ShowMessage(v19, v1);
    Forms::TCustomForm::Close(v21);
    __writefsdword(0, (unsigned int)v8);
  }
  __writefsdword(0, (unsigned int)v11);
  v13 = &loc_4E6237;
  return unknown_libname_35(&v14, 10);
}


// ==================== [0x4E6450] _TFrmMain_ConnectTimerTimer (size: 47) ====================
int __fastcall TFrmMain_ConnectTimerTimer(int a1)
{
  int result; // eax

  result = *(_DWORD *)(a1 + 736);
  if ( *(_BYTE *)(result + 36) == 0 )
    return Scktcomp::TAbstractSocket::SetActive(*(Scktcomp::TAbstractSocket **)(a1 + 736), true);
  return result;
}


// ==================== [0x4E6480] _TFrmMain_RunTimerTimer (size: 176) ====================
DWORD __usercall TFrmMain_RunTimerTimer@<eax>(int a1@<ebx>, int a2@<edi>, int a3@<esi>)
{
  unsigned __int64 v3; // st7
  DWORD result; // eax

  if ( byte_4EBC78 != 0 )
  {
    sub_4E1CD0((int)dword_4EBB84);
    *(double *)&v3 = sub_48D724(*off_4EA3DC);
    sub_4B20B8(dword_4EBB8C, a1, a2, a3, v3);
    sub_4A8190(dword_4EBBA4);
    if ( dword_4EBC04 != 0 )
      sub_496360(*off_4EA16C[0]);
    else
      sub_4975C8(*off_4EA014);
  }
  ++dword_4EBD1C;
  result = GetTickCount() - dword_4EBD18;
  if ( result > 0xFA )
  {
    dword_4EBD18 = GetTickCount();
    dword_4EBD24 = dword_4EBD1C;
    if ( dword_4EBD20 > dword_4EBD1C )
      dword_4EBD20 = dword_4EBD24;
    dword_4EBD1C = 0;
    return 0;
  }
  return result;
}


// ==================== [0x4E676C] _TFrmMain_TCloseTimerTimer (size: 52) ====================
int __fastcall TFrmMain_TCloseTimerTimer(Forms::TCustomForm *a1)
{
  int result; // eax

  result = unknown_libname_674(dword_4EBB8C);
  if ( result == 0 )
  {
    result = sub_4B4928(dword_4EBB88);
    if ( (_BYTE)result != 0 )
      return Forms::TCustomForm::Close(a1);
  }
  return result;
}


// ==================== [0x4E67A0] _TFrmMain_Panel1DblClick (size: 469) ====================
int __fastcall TFrmMain_Panel1DblClick(int a1, int a2)
{
  int v2; // edx
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-Ch] [ebp-24h] BYREF
  struct _EXCEPTION_REGISTRATION_RECORD *v5; // [esp-8h] [ebp-20h]
  int *v6; // [esp-4h] [ebp-1Ch]
  int v7; // [esp+4h] [ebp-14h]
  struct _EXCEPTION_REGISTRATION_RECORD *v8; // [esp+8h] [ebp-10h] BYREF
  int v9; // [esp+Ch] [ebp-Ch] BYREF
  int v10; // [esp+10h] [ebp-8h]
  int v11; // [esp+14h] [ebp-4h]
  int savedregs; // [esp+18h] [ebp+0h] BYREF

  v9 = 0;
  v8 = nullptr;
  v7 = a2;
  v11 = a1;
  v6 = &savedregs;
  v5 = (struct _EXCEPTION_REGISTRATION_RECORD *)&loc_4E6969;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  if ( (unsigned __int8)sub_49186C(*off_4EA6A8) != 0 )
  {
    unknown_libname_37(&v9, &str____setup_txt_2[1]);
    LOBYTE(v2) = 1;
    v10 = sub_45491C(off_454874, v2, v9);
    if ( v10 != 0 )
    {
      (*(void (__fastcall **)(int, _strings *, _strings *, struct _EXCEPTION_REGISTRATION_RECORD *, struct _EXCEPTION_REGISTRATION_RECORD *, int))(*(_DWORD *)v10 + 12))(
        v10,
        &str_Server_1[1],
        &str_HumLimit_0[1],
        v5,
        ExceptionList,
        dword_4EBD98);
      v5 = (struct _EXCEPTION_REGISTRATION_RECORD *)dword_4EBD9C;
      (*(void (__fastcall **)(int, _strings *, _strings *, int))(*(_DWORD *)v10 + 12))(
        v10,
        &str_Server_1[1],
        &str_MonLimit_0[1],
        dword_4EBD9C);
      v5 = (struct _EXCEPTION_REGISTRATION_RECORD *)dword_4EBDA0;
      (*(void (__fastcall **)(int, _strings *, _strings *, int))(*(_DWORD *)v10 + 12))(
        v10,
        &str_Server_1[1],
        &str_ZenLimit_0[1],
        dword_4EBDA0);
      v5 = (struct _EXCEPTION_REGISTRATION_RECORD *)dword_4EBDA8;
      (*(void (__fastcall **)(int, _strings *, _strings *, int))(*(_DWORD *)v10 + 12))(
        v10,
        &str_Server_1[1],
        &str_SocLimit_0[1],
        dword_4EBDA8);
      v5 = (struct _EXCEPTION_REGISTRATION_RECORD *)dword_4EBDAC;
      (*(void (__fastcall **)(int, _strings *, _strings *, int))(*(_DWORD *)v10 + 12))(
        v10,
        &str_Server_1[1],
        &str_DecLimit_0[1],
        dword_4EBDAC);
      v5 = (struct _EXCEPTION_REGISTRATION_RECORD *)dword_4EBDA4;
      (*(void (__fastcall **)(int, _strings *, _strings *, int))(*(_DWORD *)v10 + 12))(
        v10,
        &str_Server_1[1],
        &str_NpcLimit_0[1],
        dword_4EBDA4);
      v5 = (struct _EXCEPTION_REGISTRATION_RECORD *)dword_4E9F34;
      (*(void (__fastcall **)(int, _strings *, _strings *, int))(*(_DWORD *)v10 + 12))(
        v10,
        &str_Server_1[1],
        &str_SendBlock_0[1],
        dword_4E9F34);
      v5 = (struct _EXCEPTION_REGISTRATION_RECORD *)dword_4E9F38;
      (*(void (__fastcall **)(int, _strings *, _strings *, int))(*(_DWORD *)v10 + 12))(
        v10,
        &str_Server_1[1],
        &str_CheckBlock_0[1],
        dword_4E9F38);
      v5 = (struct _EXCEPTION_REGISTRATION_RECORD *)dword_4E9F40;
      (*(void (__fastcall **)(int, _strings *, _strings *, int))(*(_DWORD *)v10 + 12))(
        v10,
        &str_Server_1[1],
        &str_GateLoad_0[1],
        dword_4E9F40);
      if ( byte_4EBC22 != 0 )
        unknown_libname_37(&v8, &str_TRUE_0[1]);
      else
        unknown_libname_37(&v8, &str_FALSE_1[1]);
      v5 = v8;
      (*(void (__fastcall **)(int, _strings *, _strings *, struct _EXCEPTION_REGISTRATION_RECORD *))(*(_DWORD *)v10 + 4))(
        v10,
        &str_Server_1[1],
        &str_ViewHackMessage_0[1],
        v8);
      if ( byte_4EBC23 != 0 )
        unknown_libname_37(&v8, &str_TRUE_0[1]);
      else
        unknown_libname_37(&v8, &str_FALSE_1[1]);
      ExceptionList = v8;
      (*(void (__fastcall **)(int, _strings *, _strings *))(*(_DWORD *)v10 + 4))(
        v10,
        &str_Server_1[1],
        &str_ViewAdmissionFa_0[1]);
    }
  }
  __writefsdword(0, (unsigned int)ExceptionList);
  v6 = (int *)&loc_4E6970;
  return unknown_libname_35(&v8, 2);
}


// ==================== [0x4E6AAC] _TFrmMain_SpeedButton1Click (size: 461) ====================
int __usercall TFrmMain_SpeedButton1Click@<eax>(int a1@<eax>, int a2@<edx>, int a3@<ebx>)
{
  int v3; // edx
  unsigned int v5[2]; // [esp-10h] [ebp-30h] BYREF
  int *v6; // [esp-8h] [ebp-28h]
  int v7; // [esp-4h] [ebp-24h]
  int v8; // [esp+0h] [ebp-20h] BYREF
  int v9; // [esp+4h] [ebp-1Ch] BYREF
  int v10; // [esp+8h] [ebp-18h] BYREF
  _DWORD v11[2]; // [esp+Ch] [ebp-14h] BYREF
  int v12; // [esp+14h] [ebp-Ch] BYREF
  System::TObject *v13; // [esp+18h] [ebp-8h]
  int v14; // [esp+1Ch] [ebp-4h]
  int savedregs; // [esp+20h] [ebp+0h] BYREF

  v12 = 0;
  v11[0] = 0;
  v10 = 0;
  v9 = 0;
  v8 = 0;
  v7 = a3;
  v11[1] = a2;
  v14 = a1;
  v6 = &savedregs;
  v5[1] = (unsigned int)&loc_4E6C6D;
  v5[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v5);
  TFrmIDSoc_Timer1Timer(*off_4EA3DC, a1);
  if ( dword_4EBC04 != 0 && *(_BYTE *)(*(_DWORD *)(*(_DWORD *)(*off_4EA16C[0] + 720) + 128) + 8) == 0 )
    Scktcomp::TAbstractSocket::SetActive(*(Scktcomp::TAbstractSocket **)(*off_4EA16C[0] + 720), true);
  unknown_libname_37(&v12, &str____setup_txt_3[1]);
  if ( (unsigned __int8)Sysutils::FileExists(v12) != 0 )
  {
    LOBYTE(v3) = 1;
    v13 = (System::TObject *)sub_45491C(off_454874, v3, v12);
    if ( v13 != nullptr )
    {
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _strings *, _DWORD *))v13)(
        v13,
        &str_Server_2[1],
        &str_LogServerAddr_0[1],
        &str_192_168_0_152_0[1],
        v11);
      unknown_libname_36(&dword_4EBCC8, v11[0]);
      dword_4EBCCC = (*(int (__fastcall **)(System::TObject *, _strings *, _strings *, int))(*(_DWORD *)v13 + 8))(
                       v13,
                       &str_Server_2[1],
                       &str_LogServerPort_0[1],
                       10000);
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _DWORD, int *))v13)(
        v13,
        &str_Setup_1[1],
        &str_ClientFile1_0[1],
        0,
        &v10);
      unknown_libname_36(&dword_4EBDF8, v10);
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _DWORD, int *))v13)(
        v13,
        &str_Setup_1[1],
        &str_ClientFile2_0[1],
        0,
        &v9);
      unknown_libname_36(&dword_4EBDFC, v9);
      (**(void (__fastcall ***)(System::TObject *, _strings *, _strings *, _DWORD, int *))v13)(
        v13,
        &str_Setup_1[1],
        &str_ClientFile3_0[1],
        0,
        &v8);
      unknown_libname_36(&dword_4EBE00, v8);
    }
    System::TObject::Free(v13);
  }
  unknown_libname_36(*(_DWORD *)(v14 + 792) + 2092, dword_4EBCC8);
  *(_DWORD *)(*(_DWORD *)(v14 + 792) + 2096) = dword_4EBCCC;
  sub_4E3E64();
  sub_48D290(*off_4EA3DC);
  sub_4E46E0(v14);
  __writefsdword(0, v5[0]);
  v6 = (int *)&loc_4E6C74;
  unknown_libname_35(&v8, 4);
  return unknown_libname_34(&v12);
}


// ==================== [0x4E6D38] sub_4E6D38 (size: 412) ====================
int sub_4E6D38()
{
  unsigned int v1[3]; // [esp-Ch] [ebp-Ch] BYREF
  int savedregs; // [esp+0h] [ebp+0h] BYREF

  v1[2] = (unsigned int)&savedregs;
  v1[1] = (unsigned int)&loc_4E6ECB;
  v1[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v1);
  if ( ++dword_4EBE10 == 0 )
  {
    unknown_libname_34(&dword_4EBE00);
    unknown_libname_34(&dword_4EBDFC);
    unknown_libname_34(&dword_4EBDF8);
    unknown_libname_34(&dword_4EBDF4);
    unknown_libname_34(&dword_4EBDF0);
    unknown_libname_34(&dword_4EBDEC);
    unknown_libname_34(&dword_4EBDE8);
    unknown_libname_34(&dword_4EBDE4);
    unknown_libname_34(&dword_4EBDE0);
    unknown_libname_34(&dword_4EBDDC);
    unknown_libname_34(&dword_4EBDD8);
    unknown_libname_34(&dword_4EBDD4);
    unknown_libname_34(&dword_4EBDD0);
    unknown_libname_34(&dword_4EBDCC);
    unknown_libname_34(&dword_4EBDC8);
    unknown_libname_34(&dword_4EBDC4);
    unknown_libname_34(&dword_4EBDC0);
    unknown_libname_34(&dword_4EBDBC);
    unknown_libname_34(&dword_4EBDB8);
    unknown_libname_34(&dword_4EBDB4);
    unknown_libname_34(&dword_4EBDB0);
    unknown_libname_34(&dword_4EBCFC);
    unknown_libname_34(&dword_4EBCF8);
    unknown_libname_34(&dword_4EBCF4);
    unknown_libname_34(&dword_4EBCF0);
    unknown_libname_34(&dword_4EBCEC);
    unknown_libname_34(&dword_4EBCE0);
    unknown_libname_34(&dword_4EBCDC);
    unknown_libname_34(&dword_4EBCD4);
    unknown_libname_34(&dword_4EBCD0);
    unknown_libname_34(&dword_4EBCC8);
    unknown_libname_34(&dword_4EBCC0);
    unknown_libname_34(&dword_4EBC84);
    unknown_libname_34(&dword_4EBC30);
    unknown_libname_34(&dword_4EBC08);
    unknown_libname_34(&System::AnsiString);
  }
  __writefsdword(0, v1[0]);
  return 0;
}


