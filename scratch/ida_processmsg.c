int __usercall ProcessServerMessage@<eax>(int a1@<eax>, int a2@<edx>)
{
  char v2; // zf
  int v3; // ecx
  int v4; // ecx
  int v5; // ecx
  int v6; // ecx
  int v7; // ecx
  int v8; // ecx
  int v9; // ecx
  int v10; // ecx
  int v11; // ecx
  int v12; // eax
  int v13; // edx
  int v14; // ecx
  int v15; // ebx
  int v16; // eax
  int v17; // ebx
  int v18; // eax
  int v19; // ebx
  int v20; // eax
  int v21; // eax
  unsigned __int8 v22; // al
  int v23; // ecx
  int v24; // ecx
  int v25; // ecx
  int v26; // ecx
  int v27; // eax
  int v28; // eax
  int v29; // eax
  __int16 v30; // ax
  int v31; // ecx
  int v32; // ecx
  int v33; // ecx
  int v34; // ecx
  int v35; // ecx
  int v36; // ecx
  int v37; // ecx
  int v38; // ecx
  int v39; // ecx
  int v40; // ecx
  int v41; // ecx
  int v42; // ecx
  int v43; // eax
  int v44; // ecx
  int v45; // ecx
  int v46; // ecx
  int v48; // [esp-14h] [ebp-B4h]
  __int16 v49; // [esp-14h] [ebp-B4h]
  int v50; // [esp-10h] [ebp-B0h]
  int v51; // [esp-10h] [ebp-B0h]
  int v52; // [esp-10h] [ebp-B0h]
  int v53; // [esp-10h] [ebp-B0h]
  int v54; // [esp-10h] [ebp-B0h]
  __int16 v55; // [esp-10h] [ebp-B0h]
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-Ch] [ebp-ACh] BYREF
  void *v57; // [esp-8h] [ebp-A8h]
  int *v58; // [esp-4h] [ebp-A4h]
  int v59; // [esp+Ch] [ebp-94h] BYREF
  char v60; // [esp+10h] [ebp-90h]
  __int32 v61[10]; // [esp+14h] [ebp-8Ch] BYREF
  int v62; // [esp+3Ch] [ebp-64h] BYREF
  int v63; // [esp+40h] [ebp-60h]
  int v64; // [esp+44h] [ebp-5Ch]
  int v65; // [esp+48h] [ebp-58h]
  int v66; // [esp+4Ch] [ebp-54h]
  int v67; // [esp+50h] [ebp-50h]
  int v68; // [esp+54h] [ebp-4Ch]
  int v69; // [esp+58h] [ebp-48h] BYREF
  int v70; // [esp+5Ch] [ebp-44h]
  int v71; // [esp+60h] [ebp-40h]
  _DWORD v72[2]; // [esp+64h] [ebp-3Ch] BYREF
  int v73; // [esp+6Ch] [ebp-34h] BYREF
  int v74; // [esp+70h] [ebp-30h]
  _WORD v75[2]; // [esp+74h] [ebp-2Ch] BYREF
  unsigned __int16 v76; // [esp+7Ch] [ebp-24h] BYREF
  int v77; // [esp+80h] [ebp-20h] BYREF
  int v78; // [esp+84h] [ebp-1Ch] BYREF
  int v79; // [esp+88h] [ebp-18h]
  int v80; // [esp+8Ch] [ebp-14h] BYREF
  int v81; // [esp+90h] [ebp-10h] BYREF
  int v82; // [esp+94h] [ebp-Ch] BYREF
  _BYTE *v83; // [esp+98h] [ebp-8h]
  int v84; // [esp+9Ch] [ebp-4h]
  int savedregs; // [esp+A0h] [ebp+0h] BYREF

  v61[0] = 0;
  v59 = 0;
  v82 = 0;
  v81 = 0;
  v80 = 0;
  v79 = 0;
  v78 = 0;
  v69 = 0;
  v77 = 0;
  v83 = (_BYTE *)a2;
  v84 = a1;
  Network_Connect(a2);
  v58 = &savedregs;
  v57 = &loc_46AF42;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  if ( *v83 == 43 )
  {
    get_msg_length(&v78);
    read_string(v50);
    v60 = 47;
    read_string_field(v61, 0);
    read_string2(ExceptionList, v57, v58);
    Memory_Compare(v79, dword_46AF58);
    if ( v2 )
      byte_4F955C = 1;
    Memory_Compare(v79, (int *)&off_46AF64);
    if ( v2 )
      byte_4F955D = 1;
    Memory_Compare(v79, (int *)aUlng);
    if ( v2 )
      byte_4F955D = 0;
    Memory_Compare(v79, (int *)&off_46AF80);
    if ( v2 )
      byte_4F955E = 1;
    Memory_Compare(v79, (int *)aUwid);
    if ( v2 )
      byte_4F955E = 0;
    Memory_Compare(v79, dword_46AF9C);
    if ( v2 )
    {
      byte_4F955F = 1;
      dword_4F9450 = GetTickCount();
    }
    Memory_Compare(v79, (int *)aUfir);
    if ( v2 )
      byte_4F955F = 0;
    Memory_Compare(v79, (int *)aGood_0);
    if ( v2 )
    {
      *(_BYTE *)(v84 + 1056) = 0;
      ++dword_4F952C;
    }
    Memory_Compare(v79, (int *)aFail);
    if ( v2 )
    {
      handle_sm_death();
      *(_BYTE *)(v84 + 1056) = 0;
      ++dword_4F952C;
    }
    if ( v78 )
    {
      read_ident();
      handle_sm_reconnect2();
    }
    goto LABEL_488;
  }
  if ( ((int (*)(void))get_msg_length)() < 16 )
  {
    if ( *v83 == 61 )
    {
      get_msg_length(&v78);
      read_string(v51);
      Memory_Compare(v78, (int *)&off_46AFD8);
      if ( v2 )
        *(_BYTE *)(dword_4F7EF8 + 220) = 1;
    }
    goto LABEL_488;
  }
  read_string(&v82);
  ((void (*)(void))get_msg_length)();
  read_string(&v81);
  GetBackMsg();
  if ( (_WORD)v67 == 46 )
  {
    dword_4F9590 = HIWORD(v67);
    dword_4F9594 = (unsigned __int16)v68;
  }
  if ( dword_4F7EF8 )
    goto LABEL_109;
  if ( (unsigned __int16)v67 > 0x20Du )
  {
    if ( (unsigned __int16)v67 <= 0x213u )
    {
      if ( (unsigned __int16)v67 == 531 )
      {
        handle_sm_select_char(ExceptionList, v57, v58);
        goto LABEL_109;
      }
      switch ( (__int16)v67 )
      {
        case 526:
          handle_sm_login(ExceptionList, v57, v58);
          LOWORD(v8) = word_46AFDC;
          show_dialog_message(v8, dword_46B668);
          WindowCloseHandler(dword_4F7D88, v9);
          goto LABEL_488;
        case 527:
          byte_4F9470 = 0;
          byte_4F9469 = 0;
          byte_4F9468 = 0;
          LOWORD(v3) = word_46AFDC;
          show_dialog_message(v3, sub_46B4A0);
          WindowCloseHandler(v84, v7);
          goto LABEL_109;
        case 528:
          goto LABEL_109;
        case 529:
          word_4F951C = v66;
          word_4F951E = GetShort(ExceptionList, v57, v58);
          word_4F9520 = HIWORD(v67);
          word_4F9522 = v68;
          if ( word_4F951C )
          {
            if ( word_4F951C == 1 )
            {
              LOWORD(v3) = word_46AFDC;
              show_dialog_message(v3, dword_46B3C8);
              goto LABEL_86;
            }
            if ( (unsigned __int16)word_4F951C <= 3u )
              goto LABEL_85;
          }
          else
          {
            if ( !word_4F9520 )
            {
              if ( word_4F9522 )
              {
                if ( (unsigned __int16)word_4F9522 > 0x64u )
                  goto LABEL_86;
              }
              else if ( !word_4F951E )
              {
                goto LABEL_86;
              }
              goto LABEL_85;
            }
            if ( word_4F9520 == 1 )
            {
              LOWORD(v3) = word_46AFDC;
              show_dialog_message(v3, dword_46B418);
              goto LABEL_86;
            }
            if ( (unsigned __int16)word_4F9520 <= 3u )
            {
LABEL_85:
              sub_407D64(v3, &v59);
              Socket_SendReceive(v5, 3);
              LOWORD(v6) = word_46AFDC;
              show_dialog_message(v6, v61[0]);
            }
          }
LABEL_86:
          if ( !*(_BYTE *)(dword_4F7D94 + 356) )
            handle_sm_select_char_result(ExceptionList, v57, v58);
          goto LABEL_109;
        case 530:
          handle_sm_select_server(ExceptionList, v57, v58);
          goto LABEL_109;
        default:
          goto LABEL_488;
      }
    }
    if ( (unsigned __int16)v67 > 0x292u )
    {
      if ( (unsigned __int16)v67 != 772 && (unsigned __int16)v67 != 802 )
        goto LABEL_488;
    }
    else if ( (unsigned __int16)v67 != 658 )
    {
      if ( (unsigned __int16)v67 == 532 )
      {
        LOWORD(v3) = word_46AFDC;
        show_dialog_message(v3, &unk_46B280);
        handle_sm_select_char_result(ExceptionList, v57, v58);
      }
      else
      {
        if ( (unsigned __int16)v67 != 533 )
          goto LABEL_488;
        LOWORD(v3) = word_46AFDC;
        show_dialog_message(v3, dword_46B3B0);
        handle_sm_select_char_result(ExceptionList, v57, v58);
      }
    }
LABEL_109:
    if ( byte_4F9478 )
    {
      if ( (_WORD)v67 == 634 )
      {
        v12 = v84;
        *(_DWORD *)(v84 + 1020) = v66;
        *(_DWORD *)(v12 + 1024) = v67;
        *(_DWORD *)(v12 + 1028) = v68;
        ResetReadPtr();
        Ref_Count_Manage((volatile __int32 *)(v84 + 1032), v61[0]);
        byte_4F9479 = 1;
        handle_sm_combat_broadcast();
      }
      goto LABEL_488;
    }
    if ( (_WORD)v67 == 46 )
    {
      *(_DWORD *)dword_4F9598 = HIWORD(v67);
      *(_DWORD *)dword_4F959C = (unsigned __int16)v68;
    }
    if ( (unsigned __int16)v67 > 0x290u )
    {
      if ( (unsigned __int16)v67 <= 0x2CCu )
      {
        if ( (unsigned __int16)v67 == 716 )
        {
          handle_sm_attr2((unsigned __int16)v68);
        }
        else
        {
          switch ( (__int16)v67 )
          {
            case 657:
              v28 = GetInteger();
              show_entity_animation(0, 0, HIWORD(v68), v28, 0, 0, 0);
              goto LABEL_486;
            case 658:
              handle_sm_confirm_dialog();
              goto LABEL_486;
            case 659:
              byte_4F94F8 = HIWORD(v67) != 0;
              dword_4F94E8 = GetTickCount();
              goto LABEL_486;
            case 660:
              dword_4F94E8 = GetTickCount();
              byte_4F94F8 = 1;
              goto LABEL_486;
            case 661:
              dword_4F94E8 = GetTickCount();
              switch ( v66 )
              {
                case -4:
LABEL_388:
                  LOWORD(v37) = word_46AFDC;
                  show_dialog_message(v37, sub_46BA28);
                  break;
                case -3:
                  LOWORD(v37) = word_46AFDC;
                  show_dialog_message(v37, dword_46B9EC);
                  break;
                case -2:
                  goto LABEL_398;
                case -1:
                  LOWORD(v37) = word_46AFDC;
                  show_dialog_message(v37, dword_46B9A8);
                  break;
              }
              break;
            case 662:
            case 663:
              dword_4F94E8 = GetTickCount();
              goto LABEL_486;
            case 664:
              dword_4F94E8 = GetTickCount();
              switch ( v66 )
              {
                case -5:
                  LOWORD(v37) = word_46AFDC;
                  show_dialog_message(v37, dword_46BA88);
                  break;
                case -4:
                  goto LABEL_388;
                case -3:
                  LOWORD(v37) = word_46AFDC;
                  show_dialog_message(v37, sub_46BA70);
                  break;
                case -2:
                  goto LABEL_398;
                case -1:
                  goto LABEL_391;
                default:
                  goto LABEL_486;
              }
              goto LABEL_486;
            case 665:
              dword_4F94E8 = GetTickCount();
              switch ( v66 )
              {
                case -3:
                  LOWORD(v37) = word_46AFDC;
                  show_dialog_message(v37, dword_46BAA4);
                  break;
                case -2:
LABEL_398:
                  LOWORD(v37) = word_46AFDC;
                  show_dialog_message(v37, dword_46B9C0);
                  break;
                case -1:
LABEL_391:
                  LOWORD(v37) = word_46AFDC;
                  show_dialog_message(v37, dword_46BA44);
                  break;
              }
              break;
            case 666:
              (*(void (**)(void))(*(_DWORD *)dword_4F94FC + 64))();
              goto LABEL_486;
            case 667:
              ResetReadPtr();
              trade_callback();
              goto LABEL_486;
            case 668:
              open_shop_repair();
              goto LABEL_486;
            case 669:
              if ( byte_4F9308 )
              {
                *(_DWORD *)(*off_4A4310 + 1884) = GetTickCount();
                *(_DWORD *)(dword_4F7EF8 + 88) = v66;
                word_4F9338 = HIWORD(v67);
                word_4F933A = v68;
                sub_49F874();
                byte_4F9308 = 0;
              }
              goto LABEL_486;
            case 670:
              *(_DWORD *)(*off_4A4310 + 1884) = GetTickCount();
              sub_49F874();
              byte_4F9308 = 0;
              LOWORD(v32) = word_46AFDC;
              show_dialog_message(v32, dword_46B83C);
              goto LABEL_486;
            case 671:
              if ( byte_4F92D4 )
              {
                if ( v66 >= 0 )
                {
LABEL_336:
                  sub_407D64(v3, v61);
                  sub_403D20(dword_46B7FC, v61[0]);
                }
                else
                {
LABEL_342:
                  Ref_Count_Manage(&dword_4F9378, (__int32)dword_46B80C);
                }
              }
              goto LABEL_486;
            case 673:
              dword_4F94F0 = GetTickCount();
              ResetReadPtr();
              Ref_Count_Manage(&dword_4F918C, v61[0]);
              sub_4915EC();
              goto LABEL_486;
            case 674:
              dword_4F94F0 = GetTickCount();
              LOWORD(v39) = word_46AFDC;
              show_dialog_message(v39, dword_46BADC);
              goto LABEL_486;
            case 675:
              dword_4F94EC = GetTickCount();
              if ( byte_4F933C )
              {
                sub_49FDE4();
                byte_4F933C = 0;
              }
              goto LABEL_486;
            case 676:
              dword_4F94EC = GetTickCount();
              if ( byte_4F933C )
              {
                sub_49F874();
                byte_4F933C = 0;
              }
              goto LABEL_486;
            case 677:
              dword_4F94EC = GetTickCount();
              if ( byte_4F933C )
                byte_4F933C = 0;
              goto LABEL_486;
            case 678:
              dword_4F94EC = GetTickCount();
              if ( byte_4F933C )
              {
                sub_403C78(ExceptionList, v57, v58);
                sub_49F9E4();
                sub_49FDE4();
                if ( dword_4F93D0 == dword_4F9368 )
                {
                  sub_402AE4((unsigned __int8)byte_4F93A4 + 1, &byte_4F933C, ExceptionList, v57, v58);
                  if ( v2 )
                  {
                    byte_4F939C = 0;
                    byte_4F93A4 = 0;
                  }
                }
                byte_4F933C = 0;
              }
              goto LABEL_486;
            case 681:
              sub_49FE38();
              if ( byte_4F933C )
              {
                sub_49F874();
                byte_4F933C = 0;
              }
              if ( dword_4F9180 > 0 )
              {
                *(_DWORD *)(dword_4F7EF8 + 88) += dword_4F9180;
                dword_4F9180 = 0;
              }
              goto LABEL_411;
            case 682:
              sub_46E988();
              sub_495794(125);
              goto LABEL_486;
            case 683:
              sub_46E9EC();
              sub_495794(126);
              goto LABEL_486;
            case 684:
            case 685:
              dword_4F9180 = v66;
              *(_DWORD *)(dword_4F7EF8 + 88) = GetInteger();
              dword_4F94EC = GetTickCount();
              goto LABEL_486;
            case 686:
              dword_4F9184 = v66;
              sub_495794(106);
              goto LABEL_486;
            case 687:
LABEL_411:
              sub_4916E0();
              goto LABEL_486;
            case 700:
              handle_sm_open_shop();
              goto LABEL_486;
            case 701:
            case 702:
            case 703:
              *(_DWORD *)(*off_4A4310 + 1884) = GetTickCount();
              if ( (_WORD)v67 != 701 )
              {
                LOWORD(v33) = word_46AFDC;
                if ( (_WORD)v67 == 702 )
                  show_dialog_message(v33, dword_46B85C);
                else
                  show_dialog_message(v33, dword_46B894);
                sub_49F874();
              }
              byte_4F9308 = 0;
              goto LABEL_486;
            case 704:
              handle_sm_shop_items(v81, v66);
              goto LABEL_486;
            case 705:
            case 706:
            case 707:
              *(_DWORD *)(*off_4A4310 + 1884) = GetTickCount();
              if ( (_WORD)v67 == 705 )
              {
                sub_48FDFC();
              }
              else
              {
                LOWORD(v34) = word_46AFDC;
                if ( (_WORD)v67 == 707 )
                  show_dialog_message(v34, dword_46B8AC);
                else
                  show_dialog_message(v34, dword_46B8CC);
              }
              goto LABEL_486;
            case 708:
              dword_4F9388 = v66;
              goto LABEL_486;
            case 709:
              handle_sm_delete_item();
              goto LABEL_486;
            case 710:
              dword_4F94F0 = GetTickCount();
              sub_46EA50();
              goto LABEL_486;
            case 711:
              dword_4F94F0 = GetTickCount();
              play_sound(255);
              goto LABEL_486;
            case 712:
              handle_sm_shop_detail(v81, v66);
              goto LABEL_486;
            case 713:
              *(_DWORD *)(*off_4A4310 + 1884) = GetTickCount();
              *(_DWORD *)(dword_4F7EF8 + 88) = v66;
              LOWORD(v36) = word_46AFDC;
              show_dialog_message(v36, dword_46B940);
              goto LABEL_486;
            case 714:
              *(_DWORD *)(*off_4A4310 + 1884) = GetTickCount();
              switch ( v66 )
              {
                case 1:
                  LOWORD(v35) = word_46AFDC;
                  show_dialog_message(v35, dword_46B960);
                  break;
                case 2:
LABEL_364:
                  LOWORD(v35) = word_46AFDC;
                  show_dialog_message(v35, nullsub_11);
                  break;
                case 3:
                  LOWORD(v35) = word_46AFDC;
                  show_dialog_message(v35, dword_46B974);
                  break;
                case 4:
                  LOWORD(v35) = word_46AFDC;
                  show_dialog_message(v35, dword_46B988);
                  break;
              }
              goto LABEL_486;
            default:
              goto LABEL_485;
          }
        }
        goto LABEL_486;
      }
      if ( (unsigned __int16)v67 <= 0x304u )
      {
        if ( (unsigned __int16)v67 == 772 )
        {
          if ( v81 )
          {
LABEL_477:
            ResetReadPtr();
            LOWORD(v42) = word_46AFDC;
            show_dialog_message(v42, v61[0]);
          }
        }
        else
        {
          switch ( (__int16)v67 )
          {
            case 750:
              ResetReadPtr();
              guild_related();
              goto LABEL_486;
            case 751:
              load_guild_info();
              goto LABEL_486;
            case 752:
              dword_4F9504 = BYTE2(v67);
              dword_4F9500 = (unsigned __int8)GetByte();
              dword_4F9508 = (unsigned __int8)v68;
              dword_4F950C = (unsigned __int8)GetByte();
              dword_4F9510 = BYTE2(v68);
              dword_4F9514 = (unsigned __int8)GetByte();
              dword_4F9518 = (unsigned __int8)v66;
              goto LABEL_486;
            case 753:
              dword_4F94F0 = GetTickCount();
              load_guild_relations();
              goto LABEL_486;
            case 754:
              dword_4F94F0 = GetTickCount();
              LOWORD(v38) = word_46AFDC;
              show_dialog_message(v38, dword_46BAC0);
              goto LABEL_486;
            case 756:
              dword_4F94F0 = GetTickCount();
              load_guild_members();
              goto LABEL_486;
            case 757:
            case 759:
              send_group_response();
              goto LABEL_486;
            case 758:
              switch ( v66 )
              {
                case 1:
                  goto LABEL_434;
                case 2:
                  LOWORD(v3) = word_46AFDC;
                  show_dialog_message(v3, &dword_46BB4C);
                  break;
                case 3:
                  LOWORD(v3) = word_46AFDC;
                  show_dialog_message(v3, nullsub_13);
                  break;
                case 4:
                  LOWORD(v3) = word_46AFDC;
                  show_dialog_message(v3, dword_46BB98);
                  break;
                case 5:
                  LOWORD(v3) = word_46AFDC;
                  show_dialog_message(v3, dword_46BBB4);
                  break;
                default:
                  goto LABEL_486;
              }
              goto LABEL_486;
            case 760:
              switch ( v66 )
              {
                case 1:
LABEL_434:
                  LOWORD(v3) = word_46AFDC;
                  show_dialog_message(v3, dword_46BB28);
                  break;
                case 2:
                  LOWORD(v3) = word_46AFDC;
                  show_dialog_message(v3, dword_46BBD4);
                  break;
                case 3:
                  LOWORD(v3) = word_46AFDC;
                  show_dialog_message(v3, dword_46BBF4);
                  break;
                case 4:
                  LOWORD(v3) = word_46AFDC;
                  show_dialog_message(v3, dword_46BC18);
                  break;
              }
              goto LABEL_486;
            case 761:
              switch ( v66 )
              {
                case -7:
                  LOWORD(v3) = word_46AFDC;
                  show_dialog_message(v3, dword_46BD08);
                  break;
                case -6:
                  LOWORD(v3) = word_46AFDC;
                  show_dialog_message(v3, dword_46BCDC);
                  break;
                case -5:
                  LOWORD(v3) = word_46AFDC;
                  show_dialog_message(v3, dword_46BCAC);
                  break;
                case -4:
                  LOWORD(v3) = word_46AFDC;
                  show_dialog_message(v3, dword_46BC80);
                  break;
                case -3:
                  LOWORD(v3) = word_46AFDC;
                  show_dialog_message(v3, dword_46BC58);
                  break;
                case -2:
                  LOWORD(v3) = word_46AFDC;
                  show_dialog_message(v3, dword_46BC30);
                  break;
                default:
                  goto LABEL_486;
              }
              goto LABEL_486;
            case 762:
              *(_DWORD *)(*off_4A4310 + 1884) = GetTickCount();
              LOWORD(v40) = word_46AFDC;
              show_dialog_message(v40, dword_46BDE8);
              goto LABEL_486;
            case 763:
              *(_DWORD *)(*off_4A4310 + 1884) = GetTickCount();
              switch ( v66 )
              {
                case -3:
                  LOWORD(v41) = word_46AFDC;
                  show_dialog_message(v41, dword_46BE30);
                  break;
                case -2:
                  LOWORD(v41) = word_46AFDC;
                  show_dialog_message(v41, sub_46BE18);
                  break;
                case -1:
                  LOWORD(v41) = word_46AFDC;
                  show_dialog_message(v41, dword_46BE00);
                  break;
              }
              goto LABEL_486;
            case 764:
            case 765:
              *(_DWORD *)(*off_4A4310 + 1884) = GetTickCount();
              goto LABEL_486;
            case 766:
              dword_4F938C = HIWORD(v67);
              goto LABEL_486;
            case 767:
              *(_DWORD *)(*off_4A4310 + 1884) = GetTickCount();
              if ( v81 )
                goto LABEL_477;
              goto LABEL_486;
            case 768:
            case 769:
              switch ( v66 )
              {
                case -4:
                  LOWORD(v3) = word_46AFDC;
                  show_dialog_message(v3, dword_46BD88);
                  goto LABEL_486;
                case -3:
                  LOWORD(v3) = word_46AFDC;
                  show_dialog_message(v3, dword_46BD5C);
                  goto LABEL_486;
                case -2:
                  LOWORD(v3) = word_46AFDC;
                  show_dialog_message(v3, dword_46BD48);
                  goto LABEL_486;
              }
              if ( v66 != -1 )
                goto LABEL_486;
              goto LABEL_459;
            case 770:
            case 771:
              switch ( v66 )
              {
                case -3:
                  LOWORD(v3) = word_46AFDC;
                  show_dialog_message(v3, dword_46BDC8);
                  break;
                case -2:
                  LOWORD(v3) = word_46AFDC;
                  show_dialog_message(v3, dword_46BDAC);
                  break;
                case -1:
LABEL_459:
                  LOWORD(v3) = word_46AFDC;
                  show_dialog_message(v3, dword_46BD30);
                  break;
              }
              break;
            default:
              goto LABEL_485;
          }
        }
        goto LABEL_486;
      }
      if ( (unsigned __int16)v67 <= 0x32Bu )
      {
        if ( (unsigned __int16)v67 == 811 )
        {
          handle_sm_attr(v81, v66);
        }
        else
        {
          switch ( (__int16)v67 )
          {
            case 800:
            case 806:
              if ( *(_DWORD *)(dword_4F7EF8 + 4) != v66 )
                goto LABEL_203;
              goto LABEL_486;
            case 801:
            case 807:
              v19 = sub_458D88(10.66666666666667);
              if ( v19 >= get_msg_length(ExceptionList) )
              {
                ResourceManager_Free(&v78);
              }
              else
              {
                v20 = sub_458D88(10.66666666666667);
                get_msg_length(v20 + 1);
                read_string(&v80);
                ResetReadPtr();
                v60 = 47;
                read_string_field(&v77, 0);
              }
              GetBuffer(8, &v73);
              if ( *(_DWORD *)(dword_4F7EF8 + 4) != v66 )
                create_entity(v74, v73, SBYTE2(v68), v68);
              show_entity_animation(0, 0, v74, v73, HIWORD(v68), (unsigned __int16)v68, HIWORD(v67));
              if ( v78 )
              {
                v71 = get_actor_by_id();
                if ( v71 )
                {
LABEL_211:
                  v60 = 92;
                  read_string_field(v61, 0);
                  Ref_Count_Manage((volatile __int32 *)(v71 + 36), v61[0]);
                  read_ident();
                  *(_DWORD *)(v71 + 44) = read_timestamp();
                }
              }
              goto LABEL_486;
            case 802:
              handle_sm_reconnect();
              goto LABEL_486;
            case 803:
LABEL_284:
              if ( *(_DWORD *)(dword_4F7EF8 + 4) != v66 )
LABEL_203:
                show_entity_animation(0, 0, 0, 0, 0, (unsigned __int16)v68, HIWORD(v67));
              break;
            case 804:
              GetBuffer(4, &v76);
              v70 = handle_sm_teleport(HIWORD(v67), HIWORD(v68), (unsigned __int16)v68);
              *(_DWORD *)(v70 + 12) = 0;
              *(_DWORD *)(v70 + 28) = v76;
              sub_4777F0();
              break;
            case 805:
              handle_sm_teleport2();
              break;
            case 810:
              handle_sm_reconnect2();
              break;
            default:
              goto LABEL_485;
          }
        }
        goto LABEL_486;
      }
      if ( (unsigned __int16)v67 > 0x450u )
      {
        switch ( (unsigned __int16)v67 )
        {
          case 0x451u:
            if ( !*(_BYTE *)(*(_DWORD *)(dword_4F7D9C + 370768) + 497) )
              handle_sm_healthbar();
            byte_4F9236 = 1;
            goto LABEL_486;
          case 0x452u:
            GetBuffer(4, &v76);
            *(_DWORD *)dword_4F95A4 = v66;
            *(_DWORD *)dword_4F95A8 = GetInteger();
            *(_DWORD *)dword_4F95AC = GetInteger();
            goto LABEL_486;
          case 0x4B0u:
            v43 = sub_458D88(21.33333333333333);
            get_msg_length(v43 + 1);
            read_string(&v80);
            GetBuffer(16, &v62);
            ResetReadPtr();
            *(_DWORD *)(*off_4A4310 + 1572) = HIWORD(v67);
            *(_DWORD *)(*off_4A4310 + 1580) = (unsigned __int8)v62;
            *(_DWORD *)(*off_4A4310 + 1608) = (unsigned __int8)GetByte();
            *(_DWORD *)(*off_4A4310 + 1636) = (unsigned __int8)GetShort(ExceptionList, v57, v58);
            GetShort(ExceptionList, v57, v58);
            *(_DWORD *)(*off_4A4310 + 1664) = (unsigned __int8)GetByte();
            *(_DWORD *)(*off_4A4310 + 1692) = (unsigned __int8)v63;
            *(_DWORD *)(*off_4A4310 + 1720) = (unsigned __int8)GetByte();
            *(_DWORD *)(*off_4A4310 + 1748) = (unsigned __int8)GetShort(ExceptionList, v57, v58);
            GetShort(ExceptionList, v57, v58);
            *(_DWORD *)(*off_4A4310 + 1776) = (unsigned __int8)GetByte();
            *(_DWORD *)(*off_4A4310 + 1804) = (unsigned __int8)v64;
            *(_DWORD *)(*off_4A4310 + 1832) = (unsigned __int8)GetByte();
            *(_DWORD *)(*off_4A4310 + 1568) = 0;
            LOWORD(v44) = word_46BE50;
            show_dialog_message(v44, 0);
            send_guild_command(v77, v66);
            goto LABEL_486;
        }
      }
      else
      {
        switch ( (unsigned __int16)v67 )
        {
          case 0x450u:
            v71 = get_actor_by_id();
            if ( v71 )
            {
              GetBuffer(8, &v73);
              *(_DWORD *)(v71 + 248) = GetInteger();
              *(_DWORD *)(v71 + 252) = v73;
              *(_DWORD *)(v71 + 256) = v74;
              sub_4A0B8C();
            }
            goto LABEL_486;
          case 0x44Cu:
            v71 = get_actor_by_id();
            if ( v71 )
            {
              if ( v71 != dword_4F7EF8 )
              {
                *(_WORD *)(v71 + 60) = HIWORD(v67);
                *(_WORD *)(v71 + 64) = v68;
              }
              *(_BYTE *)(v71 + 308) = 1;
            }
            goto LABEL_486;
          case 0x44Du:
            v71 = get_actor_by_id();
            if ( v71 )
              *(_BYTE *)(v71 + 308) = 0;
            goto LABEL_486;
          case 0x44Eu:
            v71 = get_actor_by_id();
            if ( v71 && (unsigned __int8)sub_40301C(v25, (int *)off_495C18) )
              handle_sm_break_weapon();
            goto LABEL_486;
          case 0x44Fu:
            v71 = get_actor_by_id();
            if ( v71 )
            {
              *(_WORD *)(v71 + 60) = HIWORD(v67);
              *(_WORD *)(v71 + 64) = v68;
              *(_BYTE *)(v71 + 309) = 1;
              *(_DWORD *)(v71 + 316) = 2000;
              *(_DWORD *)(v71 + 312) = GetTickCount();
            }
            goto LABEL_486;
        }
      }
    }
    else
    {
      if ( (unsigned __int16)v67 == 656 )
      {
        v71 = get_actor_by_id();
        if ( v71 )
          *(_DWORD *)(v71 + 44) = read_timestamp();
        goto LABEL_486;
      }
      if ( (unsigned __int16)v67 > 0xD2u )
      {
        if ( (unsigned __int16)v67 > 0x27Bu )
        {
          switch ( (__int16)v67 )
          {
            case 636:
              sub_49F874();
              byte_4F9410 = 0;
              goto LABEL_486;
            case 637:
              GetBuffer(8, &v73);
              if ( *(_DWORD *)(dword_4F7EF8 + 4) != v66 )
              {
                v71 = get_actor_by_id();
                if ( v71 )
LABEL_227:
                  entity_action(0, 0, 0, 0, HIWORD(v68), (unsigned __int16)v68);
              }
              goto LABEL_486;
            case 638:
              GetBuffer(4, v72);
              v22 = GetByte();
              handle_sm_magic_fire(v72[0], (unsigned __int16)v68, HIWORD(v67), v22);
              goto LABEL_486;
            case 639:
              handle_sm_magic_effect();
              goto LABEL_486;
            case 640:
              v29 = GetInteger();
              update_item_value(v29);
              goto LABEL_486;
            case 642:
              v30 = GetInteger();
              update_item_durability(v30);
              goto LABEL_486;
            case 643:
              ResetReadPtr();
              open_shop(v61[0]);
              goto LABEL_486;
            case 644:
              handle_sm_merchant_dlg_close();
              goto LABEL_486;
            case 645:
              load_shop_detail(v81);
              goto LABEL_486;
            case 646:
              open_shop_normal();
              goto LABEL_486;
            case 647:
              if ( !byte_4F92D4 )
                goto LABEL_486;
              if ( v66 > 0 )
                goto LABEL_336;
              goto LABEL_342;
            case 648:
              *(_DWORD *)(*off_4A4310 + 1884) = GetTickCount();
              *(_DWORD *)(dword_4F7EF8 + 88) = v66;
              byte_4F9308 = 0;
              goto LABEL_486;
            case 649:
              *(_DWORD *)(*off_4A4310 + 1884) = GetTickCount();
              sub_49F874();
              byte_4F9308 = 0;
              LOWORD(v31) = word_46AFDC;
              show_dialog_message(v31, dword_46B820);
              goto LABEL_486;
            case 650:
              *(_DWORD *)(*off_4A4310 + 1884) = GetTickCount();
              *(_DWORD *)(dword_4F7EF8 + 88) = v66;
              GetInteger();
              sub_48FD2C();
              goto LABEL_486;
            case 651:
              *(_DWORD *)(*off_4A4310 + 1884) = GetTickCount();
              switch ( v66 )
              {
                case 1:
                  LOWORD(v35) = word_46AFDC;
                  show_dialog_message(v35, dword_46B8E4);
                  break;
                case 2:
                  goto LABEL_364;
                case 3:
                  LOWORD(v35) = word_46AFDC;
                  show_dialog_message(v35, dword_46B91C);
                  break;
              }
              break;
            case 652:
              load_shop_sell_list(v81, (unsigned __int16)v68);
              goto LABEL_486;
            case 653:
              sub_495794(106);
              if ( *(_DWORD *)(dword_4F7EF8 + 88) < v66 )
              {
                sub_407D64(v26, v61);
                sub_403CDC();
                play_notice_sound();
              }
              *(_DWORD *)(dword_4F7EF8 + 88) = v66;
              goto LABEL_486;
            case 654:
              v71 = get_actor_by_id();
              if ( v71 )
                *(_DWORD *)(v71 + 340) = HIWORD(v67);
              goto LABEL_486;
            case 655:
              if ( byte_4F7F64 )
                word_4F7F94 = v66;
              goto LABEL_486;
            default:
              goto LABEL_485;
          }
          goto LABEL_486;
        }
        if ( (unsigned __int16)v67 == 635 )
        {
          byte_4F9410 = 0;
          handle_sm_combat_end();
          goto LABEL_486;
        }
        if ( (unsigned __int16)v67 > 0x266u )
        {
          switch ( (__int16)v67 )
          {
            case 615:
              *(_DWORD *)(dword_4F7EF8 + 24) = v66;
              set_shop_state(v3, -11);
              if ( (unsigned int)dword_4F93D8 < 9 )
                qmemcpy((void *)(52 * dword_4F93D8 + 5209852), &byte_4F93DC, 0x34u);
              byte_4F93DC = 0;
              break;
            case 616:
              sub_49F874();
              byte_4F93DC = 0;
              break;
            case 617:
            case 618:
              goto LABEL_486;
            case 619:
              *(_DWORD *)(dword_4F7EF8 + 24) = v66;
              set_shop_state(v3, -11);
              byte_4F93DC = 0;
              break;
            case 620:
              if ( dword_4F93D8 < 0 )
              {
                v72[1] = -(dword_4F93D8 + 1);
                qmemcpy((void *)(-52 * (dword_4F93D8 + 1) + 5209852), &byte_4F93DC, 0x34u);
              }
              byte_4F93DC = 0;
              break;
            case 621:
              handle_sm_bag_update();
              break;
            case 622:
              *(_WORD *)(dword_4F7EF8 + 80) = v66;
              *(_BYTE *)(dword_4F7EF8 + 84) = BYTE2(v67);
              *(_BYTE *)(dword_4F7EF8 + 86) = v68;
              refresh_char_ui(dword_4A2D10);
              break;
            case 633:
              byte_4F9478 = 1;
              break;
            default:
              goto LABEL_485;
          }
          goto LABEL_486;
        }
        if ( (unsigned __int16)v67 == 614 )
        {
          handle_sm_spell_effect((unsigned __int16)v68, HIWORD(v67));
          goto LABEL_486;
        }
        if ( (unsigned __int16)v67 > 0x259u )
        {
          switch ( (unsigned __int16)v67 )
          {
            case 0x262u:
              v55 = v68;
              v49 = HIWORD(v68);
              ResetReadPtr();
              handle_sm_list_add(v61[0], v49, v55);
              goto LABEL_486;
            case 0x263u:
              handle_sm_list_del((unsigned __int16)v68);
              goto LABEL_486;
            case 0x264u:
              handle_sm_map_mark((unsigned __int16)v68, HIWORD(v67));
              goto LABEL_486;
            case 0x265u:
LABEL_253:
              play_notice_sound();
              goto LABEL_486;
          }
        }
        else
        {
          switch ( (unsigned __int16)v67 )
          {
            case 0x259u:
              ResetReadPtr();
              handle_sm_refresh2(v66, v61[0]);
              goto LABEL_486;
            case 0xD3u:
              if ( v81 )
                handle_sm_char_list();
              goto LABEL_486;
            case 0xD4u:
              handle_sm_char_list_del();
              goto LABEL_486;
            case 0x210u:
              byte_4F9470 = 0;
              byte_4F9469 = 0;
              byte_4F9468 = 0;
              LOWORD(v3) = word_46AFDC;
              show_dialog_message(v3, dword_46B758);
              WindowCloseHandler(v84, v23);
              goto LABEL_486;
            case 0x258u:
              ResetReadPtr();
              handle_sm_refresh();
              goto LABEL_486;
          }
        }
      }
      else
      {
        if ( (unsigned __int16)v67 == 210 )
        {
          if ( v81 )
            handle_sm_del_char();
          goto LABEL_486;
        }
        if ( (unsigned __int16)v67 <= 0x1Fu )
        {
          if ( (unsigned __int16)v67 == 31 )
          {
            GetBuffer(16, &v62);
            v71 = get_actor_by_id();
            if ( v71 )
            {
              if ( v71 == dword_4F7EF8 )
              {
                if ( *(_DWORD *)(dword_4F7EF8 + 44) == 249 )
                  dword_4F9448 = GetTickCount();
              }
              else if ( (unsigned __int8)sub_499FD8() )
              {
                sub_49A008();
              }
              entity_move(v64, 0, v63, v62, HIWORD(v68), 0);
              *(_WORD *)(v71 + 60) = HIWORD(v67);
              *(_WORD *)(v71 + 64) = v68;
            }
          }
          else
          {
            switch ( (__int16)v67 )
            {
              case 1:
                ++dword_4F9534;
                break;
              case 6:
              case 7:
              case 11:
                GetBuffer(8, &v73);
                if ( *(_DWORD *)(dword_4F7EF8 + 4) != v66 || (_WORD)v67 == 6 || (_WORD)v67 == 7 )
                  show_entity_animation(0, 0, v74, v73, HIWORD(v68), (unsigned __int16)v68, HIWORD(v67));
                if ( (_WORD)v67 == 6 )
                  dword_4F9454 = GetTickCount();
                break;
              case 8:
              case 14:
              case 15:
              case 16:
              case 18:
              case 19:
              case 24:
                if ( *(_DWORD *)(dword_4F7EF8 + 4) != v66 )
                {
                  v71 = get_actor_by_id();
                  if ( v71 )
                  {
                    entity_action(0, 0, 0, 0, HIWORD(v68), (unsigned __int16)v68);
                    if ( (_WORD)v67 == 15 )
                    {
                      if ( v81 )
                        *(_BYTE *)(v71 + 220) = 1;
                    }
                  }
                }
                break;
              case 9:
                v17 = sub_458D88(10.66666666666667);
                if ( v17 >= get_msg_length(ExceptionList) )
                {
                  ResourceManager_Free(&v78);
                }
                else
                {
                  v18 = sub_458D88(10.66666666666667);
                  get_msg_length(v18 + 1);
                  read_string(&v80);
                  ResetReadPtr();
                  v60 = 47;
                  read_string_field(&v77, 0);
                }
                GetBuffer(8, &v73);
                show_entity_animation(0, 0, v74, v73, HIWORD(v68), (unsigned __int16)v68, HIWORD(v67));
                if ( v78 )
                {
                  v71 = get_actor_by_id();
                  if ( v71 )
                    goto LABEL_211;
                }
                break;
              case 10:
                v15 = sub_458D88(10.66666666666667);
                if ( v15 >= get_msg_length(ExceptionList) )
                {
                  ResourceManager_Free(&v78);
                }
                else
                {
                  v16 = sub_458D88(10.66666666666667);
                  get_msg_length(v16 + 1);
                  read_string(&v80);
                  ResetReadPtr();
                  v60 = 47;
                  read_string_field(&v77, 0);
                }
                GetBuffer(8, &v73);
                show_entity_animation(0, 0, v74, v73, HIWORD(v68), (unsigned __int16)v68, HIWORD(v67));
                if ( v78 )
                {
                  v71 = get_actor_by_id();
                  if ( v71 )
                    goto LABEL_211;
                }
                break;
              case 12:
                GetBuffer(8, &v73);
                if ( *(_DWORD *)(dword_4F7EF8 + 4) != v66 )
                {
                  v71 = get_actor_by_id();
                  if ( v71 )
                    goto LABEL_227;
                }
                break;
              case 13:
                GetBuffer(8, &v73);
                if ( *(_DWORD *)(dword_4F7EF8 + 4) != v66 )
                  goto LABEL_219;
                break;
              case 17:
                v52 = HIWORD(v67);
                v48 = (unsigned __int16)v68;
                v21 = read_ident();
                cast_spell(v21, v48, v52);
                break;
              case 20:
                GetBuffer(16, &v62);
                v71 = get_actor_by_id();
                if ( !v71 )
                  v71 = create_entity(v63, v62, SBYTE2(v68), v68);
                *(_DWORD *)(v71 + 216) = v64;
                entity_action(0, 0, v63, v62, HIWORD(v68), (unsigned __int16)v68);
                break;
              case 21:
                goto LABEL_203;
              case 22:
                GetBuffer(8, v75);
                v71 = get_actor_by_id();
                if ( v71 )
                {
                  entity_action(0, 0, 0, 0, HIWORD(v68), (unsigned __int16)v68);
                  *(_DWORD *)(v71 + 196) = v75[0];
                  *(_DWORD *)(v71 + 200) = v75[1];
                  *(_DWORD *)(v71 + 204) = GetInteger();
                }
                break;
              case 23:
                GetBuffer(16, &v62);
                v71 = get_actor_by_id();
                if ( v71 )
                {
                  entity_action(0, 0, 0, 0, HIWORD(v68), (unsigned __int16)v68);
                  *(_DWORD *)(v71 + 196) = v62;
                  *(_DWORD *)(v71 + 200) = v63;
                  *(_DWORD *)(v71 + 204) = v64;
                  *(_DWORD *)(v71 + 212) = v65;
                }
                break;
              case 27:
                goto LABEL_248;
              case 28:
                handle_sm_death();
                GetBuffer(8, &v73);
                show_entity_animation(0, 0, v74, v73, HIWORD(v68), (unsigned __int16)v68, HIWORD(v67));
                break;
              case 29:
              case 30:
                goto LABEL_284;
              default:
                goto LABEL_485;
            }
          }
          goto LABEL_486;
        }
        if ( (unsigned __int16)v67 <= 0x32u )
        {
          if ( (unsigned __int16)v67 == 50 )
          {
            dword_4F9238 = 0;
            dword_4F923C = 0;
            GetBuffer(16, &v62);
            show_entity_animation(0, 0, v63, v62, HIWORD(v68), (unsigned __int16)v68, HIWORD(v67));
            LOBYTE(v13) = 7;
            sub_4799AC(v14, v13);
            send_generic(0, 0, 0);
            byte_4F94F8 = (_BYTE)v64 == 1;
            byte_4F9234 = 0;
            if ( word_4F951C || word_4F9520 || word_4F9522 || word_4F951E )
              play_sound(0xFFFFFF);
          }
          else
          {
            switch ( (__int16)v67 )
            {
              case ' ':
              case '"':
                GetBuffer(8, &v73);
                v71 = get_actor_by_id();
                if ( v71 )
                {
                  entity_action(0, 0, v74, v73, HIWORD(v68), (unsigned __int16)v68);
                  *(_WORD *)(v71 + 60) = 0;
                }
                else
                {
LABEL_219:
                  show_entity_animation(0, 0, v74, v73, HIWORD(v68), (unsigned __int16)v68, HIWORD(v67));
                }
                break;
              case '!':
LABEL_248:
                GetBuffer(8, &v73);
                show_entity_animation(0, 0, v74, v73, HIWORD(v68), (unsigned __int16)v68, HIWORD(v67));
                break;
              case '(':
                ResetReadPtr();
                GetByte();
                v54 = read_timestamp();
                read_timestamp();
                play_sound(v54);
                v71 = get_actor_by_id();
                if ( v71 )
                  sub_49A0B4();
                break;
              case ')':
                v27 = GetInteger();
                show_entity_animation(0, 0, 0, v27, 0, 0, 0);
                break;
              case '*':
                ResetReadPtr();
                v71 = get_actor_by_id();
                if ( v71 )
                {
                  v60 = 92;
                  read_string_field(v61, 0);
                  Ref_Count_Manage((volatile __int32 *)(v71 + 36), v61[0]);
                  *(_DWORD *)(v71 + 44) = read_timestamp();
                }
                break;
              case ',':
                *(_DWORD *)(dword_4F7EF8 + 72) = v66;
                sub_407D64(v3, &v59);
                Socket_SendReceive(v24, 3);
                play_sound(255);
                break;
              case '-':
                goto LABEL_253;
              case '.':
                dword_4F9384 = HIWORD(v67);
                *off_4A4144 = (unsigned __int16)v68;
                byte_4F9382 = *off_4A4144 != 0;
                break;
              default:
                goto LABEL_485;
            }
          }
          goto LABEL_486;
        }
        if ( (unsigned __int16)v67 >= 0x64u )
        {
          if ( (unsigned __int16)v67 > 0xC9u )
          {
            if ( (unsigned __int16)v67 == 202 )
            {
              delete_item_by_id();
              goto LABEL_486;
            }
            if ( (unsigned __int16)v67 == 203 )
            {
              update_item_by_id();
              goto LABEL_486;
            }
          }
          else
          {
            if ( (unsigned __int16)v67 == 201 )
            {
              load_bag_all();
              goto LABEL_486;
            }
            if ( (unsigned int)(unsigned __int16)v67 - 100 < 5 )
            {
              ResetReadPtr();
              GetByte();
              v53 = read_timestamp();
              read_timestamp();
              play_sound(v53);
              if ( (_WORD)v67 == 104 )
                handle_sm_char_query_extra();
              goto LABEL_486;
            }
            if ( (unsigned __int16)v67 == 200 )
            {
              update_single_item();
              goto LABEL_486;
            }
          }
        }
        else
        {
          switch ( (unsigned __int16)v67 )
          {
            case '3':
              ResourceManager_Free(&dword_4F7EE8);
              ResetReadPtr();
              show_entity_animation(v77, 0, 0, 0, HIWORD(v68), (unsigned __int16)v68, HIWORD(v67));
              goto LABEL_486;
            case '4':
              *(_DWORD *)(dword_4F7EF8 + 88) = v66;
              *(_BYTE *)(dword_4F7EF8 + 18) = BYTE2(v67);
              GetBuffer(40, dword_4F7EF8 + 48);
              refresh_char_ui(dword_4A2D10);
              goto LABEL_486;
            case '5':
              v71 = get_actor_by_id();
              if ( v71 )
              {
                *(_WORD *)(v71 + 60) = HIWORD(v67);
                *(_DWORD *)(v71 + 62) = v68;
              }
              goto LABEL_486;
            case '6':
              handle_sm_state();
              goto LABEL_486;
          }
        }
      }
    }
LABEL_485:
    sub_407D64(v3, &v59);
    Socket_SendReceive(v45, 3);
    play_notice_sound();
LABEL_486:
    if ( sub_403F60() > 0 )
      play_notice_sound();
    goto LABEL_488;
  }
  if ( (unsigned __int16)v67 == 525 )
  {
    handle_sm_select_server2(ExceptionList, v57, v58);
    goto LABEL_488;
  }
  if ( (unsigned __int16)v67 <= 0x1FBu )
  {
    if ( (unsigned __int16)v67 == 507 )
    {
      if ( v66 == -2 )
      {
        LOWORD(v3) = word_46AFDC;
        show_dialog_message(v3, dword_46B5DC);
      }
      else
      {
        LOWORD(v3) = word_46AFDC;
        if ( v66 == -1 )
          show_dialog_message(v3, nullsub_8);
        else
          show_dialog_message(v3, nullsub_9);
      }
    }
    else if ( (unsigned __int16)v67 > 0x1F8u )
    {
      if ( (unsigned __int16)v67 == 505 )
      {
        if ( v66 == -2 )
        {
          LOWORD(v3) = word_46AFDC;
          show_dialog_message(v3, dword_46B0F4);
        }
        else if ( v66 )
        {
          LOWORD(v3) = word_46AFDC;
          show_dialog_message(v3, dword_46B12C);
        }
        else
        {
          Socket_SendReceive(v3, 4);
          LOWORD(v4) = word_46AFDC;
          show_dialog_message(v4, v61[0]);
          sub_47DB54(ExceptionList, v57, v58);
        }
      }
      else
      {
        if ( (unsigned __int16)v67 != 506 )
          goto LABEL_488;
        LOWORD(v3) = word_46AFDC;
        show_dialog_message(v3, nullsub_7);
      }
    }
    else if ( (unsigned __int16)v67 == 504 )
    {
      LOWORD(v3) = word_46AFDC;
      show_dialog_message(v3, &unk_46AFE8);
    }
    else if ( (unsigned int)(unsigned __int16)v67 - 50 >= 2 )
    {
      if ( (unsigned __int16)v67 == 501 )
      {
        handle_sm_login(ExceptionList, v57, v58);
        LOWORD(v10) = word_46AFDC;
        show_dialog_message(v10, &unk_46B68C);
        WindowCloseHandler(dword_4F7D88, v11);
        goto LABEL_488;
      }
      if ( (unsigned __int16)v67 != 503 )
        goto LABEL_488;
      switch ( v66 )
      {
        case -5:
          LOWORD(v3) = word_46AFDC;
          show_dialog_message(v3, sub_46B248);
          break;
        case -4:
          LOWORD(v3) = word_46AFDC;
          show_dialog_message(v3, dword_46B208);
          break;
        case -3:
          LOWORD(v3) = word_46AFDC;
          show_dialog_message(v3, dword_46B1C4);
          break;
        case -2:
          LOWORD(v3) = word_46AFDC;
          show_dialog_message(v3, sub_46B188);
          break;
        case -1:
          LOWORD(v3) = word_46AFDC;
          show_dialog_message(v3, nullsub_6);
          break;
        default:
          LOWORD(v3) = word_46AFDC;
          show_dialog_message(v3, dword_46B264);
          break;
      }
      sub_47C374(ExceptionList, v57, v58);
    }
    goto LABEL_109;
  }
  switch ( (__int16)v67 )
  {
    case 520:
      handle_sm_server_list(ExceptionList, v57, v58);
      goto LABEL_109;
    case 521:
    case 523:
      send_query_char(ExceptionList, v57, v58);
      goto LABEL_109;
    case 522:
      if ( v66 == 2 )
      {
        LOWORD(v3) = word_46AFDC;
        show_dialog_message(v3, dword_46B4C8);
      }
      else if ( v66 == 3 )
      {
        LOWORD(v3) = word_46AFDC;
        show_dialog_message(v3, dword_46B4F0);
      }
      else
      {
        LOWORD(v3) = word_46AFDC;
        if ( v66 == 4 )
          show_dialog_message(v3, dword_46B534);
        else
          show_dialog_message(v3, &unk_46B55C);
      }
      goto LABEL_109;
    case 524:
      LOWORD(v3) = word_46AFDC;
      show_dialog_message(v3, &unk_46B624);
      goto LABEL_109;
    default:
      break;
  }
LABEL_488:
  __writefsdword(0, (unsigned int)ExceptionList);
  v58 = (int *)&loc_46AF49;
  ResourceManager_Free(&v59);
  ResourceManager_Free(v61);
  ResourceManager_Free(&v69);
  return sub_403A7C(v46, 7);
}
