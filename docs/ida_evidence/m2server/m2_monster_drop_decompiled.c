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


// ==================== [0x485ABC] sub_485ABC (size: 791) ====================
int __fastcall sub_485ABC(int a1, int a2, Classes::TList **a3)
{
  int v3; // ecx
  int v4; // edx
  int v5; // eax
  int v6; // eax
  int v7; // edx
  int v8; // eax
  int v9; // ecx
  unsigned int v11[2]; // [esp-Ch] [ebp-168h] BYREF
  int *v12; // [esp-4h] [ebp-160h]
  _BYTE v13[256]; // [esp+4h] [ebp-158h] BYREF
  int v14; // [esp+104h] [ebp-58h] BYREF
  int v15; // [esp+108h] [ebp-54h] BYREF
  char v16; // [esp+10Ch] [ebp-50h]
  char v17; // [esp+10Dh] [ebp-4Fh]
  int v18; // [esp+110h] [ebp-4Ch] BYREF
  _DWORD v19[2]; // [esp+114h] [ebp-48h] BYREF
  int v20; // [esp+11Ch] [ebp-40h] BYREF
  int v21; // [esp+120h] [ebp-3Ch]
  void *Mem; // [esp+124h] [ebp-38h]
  System::TObject *v23; // [esp+128h] [ebp-34h]
  _BYTE *v24; // [esp+12Ch] [ebp-30h] BYREF
  int v25; // [esp+130h] [ebp-2Ch] BYREF
  _BYTE *v26; // [esp+134h] [ebp-28h] BYREF
  int v27; // [esp+138h] [ebp-24h] BYREF
  int v28; // [esp+13Ch] [ebp-20h]
  int v29; // [esp+140h] [ebp-1Ch]
  int v30; // [esp+144h] [ebp-18h]
  int v31; // [esp+148h] [ebp-14h]
  int v32; // [esp+14Ch] [ebp-10h]
  Classes::TList **v33; // [esp+150h] [ebp-Ch]
  _DWORD v34[2]; // [esp+154h] [ebp-8h] BYREF
  int savedregs; // [esp+15Ch] [ebp+0h] BYREF

  v14 = 0;
  v15 = 0;
  v18 = 0;
  v19[0] = 0;
  v20 = 0;
  v27 = 0;
  v26 = nullptr;
  v25 = 0;
  v24 = nullptr;
  v33 = a3;
  v34[0] = a2;
  v34[1] = a1;
  unknown_libname_44(a2);
  v12 = &savedregs;
  v11[1] = (unsigned int)&loc_485DC4;
  v11[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v11);
  v32 = 0;
  System::__linkproc__ LStrCatN(&v27, 4, v3);
  if ( (unsigned __int8)Sysutils::FileExists(v27) != 0 )
  {
    if ( *v33 != nullptr )
    {
      if ( *((_DWORD *)*v33 + 2) - 1 >= 0 )
      {
        v21 = *((_DWORD *)*v33 + 2);
        v31 = 0;
        do
        {
          v5 = Classes::TList::Get(*v33, v31);
          System::__linkproc__ FreeMem(v5, 28);
          ++v31;
          --v21;
        }
        while ( v21 != 0 );
      }
      (*(void (__fastcall **)(Classes::TList *))(*(_DWORD *)*v33 + 8))(*v33);
    }
    LOBYTE(v4) = 1;
    v23 = (System::TObject *)unknown_libname_22(off_410028, v4);
    (*(void (__fastcall **)(System::TObject *, int))(*(_DWORD *)v23 + 88))(v23, v27);
    v6 = (*(int (__fastcall **)(System::TObject *))(*(_DWORD *)v23 + 20))(v23) - 1;
    if ( v6 >= 0 )
    {
      v21 = v6 + 1;
      v31 = 0;
      do
      {
        (*(void (__fastcall **)(System::TObject *, int, _BYTE **))(*(_DWORD *)v23 + 12))(v23, v31, &v26);
        if ( v26 != nullptr && *v26 != 59 )
        {
          qmemcpy(&v19[1], " /\t", 3);
          sub_453DBC(&v20, 2);
          unknown_libname_37(&v26, v20);
          v30 = sub_453B20(v24, -1);
          qmemcpy(&v19[1], " /\t", 3);
          sub_453DBC(v19, 2);
          unknown_libname_37(&v26, v19[0]);
          v29 = sub_453B20(v24, -1);
          v16 = 32;
          v17 = 9;
          sub_453F94(&v18, 1);
          unknown_libname_37(&v26, v18);
          if ( v24 != nullptr && *v24 == 34 )
            sub_4540F0(&v15, &v24);
          unknown_libname_37(&v25, v24);
          v16 = 32;
          v17 = 9;
          sub_453DBC(&v14, 1);
          unknown_libname_37(&v26, v14);
          v28 = sub_453B20(v24, 1);
          if ( v30 > 0 && v29 > 0 && v25 != 0 )
          {
            if ( *v33 == nullptr )
            {
              LOBYTE(v7) = 1;
              v8 = unknown_libname_22(off_40FAFC, v7);
              *v33 = (Classes::TList *)v8;
            }
            Mem = (void *)System::__linkproc__ GetMem(28);
            *(_DWORD *)Mem = v30 - 1;
            *((_DWORD *)Mem + 1) = v29;
            System::__linkproc__ LStrToString(v13, v25, 255);
            LOBYTE(v9) = 14;
            System::__linkproc__ PStrNCpy((char *)Mem + 8, v13, v9);
            *((_DWORD *)Mem + 6) = v28;
            Classes::TList::Add(*v33, Mem);
            ++v32;
          }
        }
        ++v31;
        --v21;
      }
      while ( v21 != 0 );
    }
    System::TObject::Free(v23);
  }
  __writefsdword(0, v11[0]);
  v12 = (int *)&loc_485DCB;
  unknown_libname_35(&v14, 2);
  unknown_libname_35(&v18, 2);
  unknown_libname_34(&v20);
  unknown_libname_35(&v24, 4);
  unknown_libname_34(v34);
  return v32;
}


// ==================== [0x4867F4] sub_4867F4 (size: 1031) ====================
int __fastcall sub_4867F4(int a1)
{
  int v1; // edx
  int v2; // eax
  int v3; // ecx
  int v4; // eax
  int v5; // eax
  int v6; // ecx
  int v7; // eax
  int v8; // eax
  int v9; // eax
  int v10; // eax
  int v11; // eax
  int v12; // edx
  int v13; // eax
  unsigned int v15[2]; // [esp-Ch] [ebp-17Ch] BYREF
  int *v16; // [esp-4h] [ebp-174h]
  int v17; // [esp+4h] [ebp-16Ch] BYREF
  int v18; // [esp+8h] [ebp-168h] BYREF
  int v19; // [esp+Ch] [ebp-164h] BYREF
  int v20; // [esp+10h] [ebp-160h] BYREF
  int v21; // [esp+14h] [ebp-15Ch] BYREF
  _BYTE v22[4]; // [esp+18h] [ebp-158h] BYREF
  int v23; // [esp+1Ch] [ebp-154h] BYREF
  int v24; // [esp+20h] [ebp-150h] BYREF
  int v25; // [esp+24h] [ebp-14Ch] BYREF
  _BYTE v26[256]; // [esp+28h] [ebp-148h] BYREF
  char v27; // [esp+128h] [ebp-48h]
  char v28; // [esp+129h] [ebp-47h]
  _DWORD v29[8]; // [esp+12Ch] [ebp-44h] BYREF
  int v30; // [esp+14Ch] [ebp-24h]
  System::TObject *v31; // [esp+150h] [ebp-20h]
  void *Mem; // [esp+154h] [ebp-1Ch]
  int v33; // [esp+158h] [ebp-18h] BYREF
  _BYTE *v34; // [esp+15Ch] [ebp-14h] BYREF
  _BYTE *v35; // [esp+160h] [ebp-10h] BYREF
  int v36; // [esp+164h] [ebp-Ch]
  int v37; // [esp+168h] [ebp-8h]
  int v38; // [esp+16Ch] [ebp-4h]
  int savedregs; // [esp+170h] [ebp+0h] BYREF

  v38 = a1;
  v16 = &savedregs;
  v15[1] = (unsigned int)&loc_486BEC;
  v15[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v15);
  v37 = -1;
  System::__linkproc__ LStrCat3((int)&v33, *(void **)off_4EA320[0], &str_MonGen_txt[1]);
  if ( (unsigned __int8)Sysutils::FileExists(v33) != 0 )
  {
    LOBYTE(v1) = 1;
    v31 = (System::TObject *)unknown_libname_22(off_410028, v1);
    (*(void (__fastcall **)(System::TObject *, int))(*(_DWORD *)v31 + 88))(v31, v33);
    v2 = (*(int (__fastcall **)(System::TObject *))(*(_DWORD *)v31 + 20))(v31) - 1;
    if ( v2 >= 0 )
    {
      v30 = v2 + 1;
      v36 = 0;
      do
      {
        (*(void (__fastcall **)(System::TObject *, int, _BYTE **))(*(_DWORD *)v31 + 12))(v31, v36, &v35);
        if ( v35 != nullptr && *v35 != 59 )
        {
          Mem = (void *)System::__linkproc__ GetMem(68);
          v27 = 32;
          v28 = 9;
          sub_453DBC(v29, 1);
          unknown_libname_37(&v35, v29[0]);
          System::__linkproc__ LStrToString(v26, v34, 255);
          LOBYTE(v3) = 14;
          System::__linkproc__ PStrNCpy(Mem, v26, v3);
          v27 = 32;
          v28 = 9;
          sub_453DBC(&v25, 1);
          unknown_libname_37(&v35, v25);
          v4 = sub_453B20(v34, 0);
          *((_DWORD *)Mem + 4) = v4;
          v27 = 32;
          v28 = 9;
          sub_453DBC(&v24, 1);
          unknown_libname_37(&v35, v24);
          v5 = sub_453B20(v34, 0);
          *((_DWORD *)Mem + 5) = v5;
          v27 = 32;
          v28 = 9;
          sub_453F94(&v23, 1);
          unknown_libname_37(&v35, v23);
          if ( v34 != nullptr && *v34 == 34 )
            sub_4540F0(v22, &v34);
          System::__linkproc__ LStrToString(v26, v34, 255);
          LOBYTE(v6) = 14;
          System::__linkproc__ PStrNCpy((char *)Mem + 24, v26, v6);
          v27 = 32;
          v28 = 9;
          sub_453DBC(&v21, 1);
          unknown_libname_37(&v35, v21);
          v7 = sub_453B20(v34, 0);
          *((_DWORD *)Mem + 11) = v7;
          v27 = 32;
          v28 = 9;
          sub_453DBC(&v20, 1);
          unknown_libname_37(&v35, v20);
          v8 = sub_453B20(v34, 0);
          *((_DWORD *)Mem + 12) = v8;
          v27 = 32;
          v28 = 9;
          sub_453DBC(&v19, 1);
          unknown_libname_37(&v35, v19);
          v9 = sub_453B20(v34, -1);
          *((_DWORD *)Mem + 13) = 60000 * v9;
          v27 = 32;
          v28 = 9;
          sub_453DBC(&v18, 1);
          unknown_libname_37(&v35, v18);
          v10 = sub_453B20(v34, 0);
          *((_DWORD *)Mem + 16) = v10;
          if ( *(_BYTE *)Mem != 0 && *((_BYTE *)Mem + 24) != 0 && *((_DWORD *)Mem + 13) != 0 )
          {
            unknown_libname_41();
            if ( sub_4B7424(*off_4EA22C, *off_4EA64C, v17) != 0 )
            {
              *((_DWORD *)Mem + 14) = 0;
              v11 = unknown_libname_22(off_40FAFC, 1);
              *((_DWORD *)Mem + 15) = v11;
              Classes::TList::Add(*(Classes::TList **)(*off_4EA490 + 92), Mem);
            }
          }
        }
        ++v36;
        --v30;
      }
      while ( v30 != 0 );
    }
    Mem = (void *)System::__linkproc__ GetMem(68);
    *(_BYTE *)Mem = 0;
    *((_BYTE *)Mem + 24) = 0;
    LOBYTE(v12) = 1;
    v13 = unknown_libname_22(off_40FAFC, v12);
    *((_DWORD *)Mem + 15) = v13;
    Classes::TList::Add(*(Classes::TList **)(*off_4EA490 + 92), Mem);
    System::TObject::Free(v31);
    v37 = 1;
  }
  __writefsdword(0, v15[0]);
  v16 = (int *)&loc_486BF3;
  unknown_libname_35(&v17, 9);
  unknown_libname_35(v29, 3);
  unknown_libname_35(&v33, 3);
  return v37;
}


