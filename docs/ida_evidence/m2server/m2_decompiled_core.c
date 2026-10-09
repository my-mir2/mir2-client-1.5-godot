// ==================== @System@@TRUNC$qqrv ====================
__int64 __usercall System::__linkproc__ TRUNC@<edx:eax>(double a1@<st0>)
{
  return (__int64)a1;
}


// ==================== CheckMenuItem ====================
// attributes: thunk
DWORD __stdcall CheckMenuItem(HMENU hMenu, UINT uIDCheckItem, UINT uCheck)
{
  return __imp_CheckMenuItem(hMenu, uIDCheckItem, uCheck);
}


// ==================== EnableMenuItem ====================
// attributes: thunk
BOOL __stdcall EnableMenuItem(HMENU hMenu, UINT uIDEnableItem, UINT uEnable)
{
  return __imp_EnableMenuItem(hMenu, uIDEnableItem, uEnable);
}


// ==================== GetMenuItemCount ====================
// attributes: thunk
int __stdcall GetMenuItemCount(HMENU hMenu)
{
  return __imp_GetMenuItemCount(hMenu);
}


// ==================== GetMenuItemID ====================
// attributes: thunk
UINT __stdcall GetMenuItemID(HMENU hMenu, int nPos)
{
  return __imp_GetMenuItemID(hMenu, nPos);
}


// ==================== GetMenuItemInfoA ====================
// attributes: thunk
BOOL __stdcall GetMenuItemInfoA(HMENU a1, UINT a2, BOOL a3, LPMENUITEMINFOA a4)
{
  return __imp_GetMenuItemInfoA(a1, a2, a3, a4);
}


// ==================== InsertMenuItemA ====================
// attributes: thunk
BOOL __stdcall InsertMenuItemA(HMENU a1, UINT a2, BOOL a3, LPCMENUITEMINFOA a4)
{
  return __imp_InsertMenuItemA(a1, a2, a3, a4);
}


// ==================== KillTimer ====================
// attributes: thunk
BOOL __stdcall KillTimer(HWND hWnd, UINT_PTR uIDEvent)
{
  return __imp_KillTimer(hWnd, uIDEvent);
}


// ==================== SetMenuItemInfoA ====================
// attributes: thunk
BOOL __stdcall SetMenuItemInfoA(HMENU a1, UINT a2, BOOL a3, LPCMENUITEMINFOA a4)
{
  return __imp_SetMenuItemInfoA(a1, a2, a3, a4);
}


// ==================== SetTimer ====================
// attributes: thunk
UINT_PTR __stdcall SetTimer(HWND hWnd, UINT_PTR nIDEvent, UINT uElapse, TIMERPROC lpTimerFunc)
{
  return __imp_SetTimer(hWnd, nIDEvent, uElapse, lpTimerFunc);
}


// ==================== @Classes@TCollectionItem@Changed$qqro ====================
int __fastcall Classes::TCollectionItem::Changed(int this, bool a2)
{
  _DWORD *v2; // esi

  v2 = *(_DWORD **)(this + 4);
  if ( v2 != nullptr && v2[3] == 0 )
    return (*(int (**)(void))(*v2 + 16))();
  return this;
}


// ==================== @Classes@TCollectionItem@GetIndex$qqrv ====================
int __fastcall Classes::TCollectionItem::GetIndex(Classes::TCollectionItem *this)
{
  int v1; // edx

  v1 = *((_DWORD *)this + 1);
  if ( v1 != 0 )
    return unknown_libname_140(*(_DWORD *)(v1 + 8), this);
  else
    return -1;
}


// ==================== @Classes@TCollectionItem@SetCollection$qqrp19Classes@TCollection ====================
Classes::TCollection *__fastcall Classes::TCollectionItem::SetCollection(
        Classes::TCollectionItem *this,
        Classes::TCollection *a2)
{
  Classes::TCollection *result; // eax

  result = *((Classes::TCollection **)this + 1);
  if ( a2 != result )
  {
    if ( result != nullptr )
      result = (Classes::TCollection *)unknown_libname_145(result, this);
    if ( a2 != nullptr )
      return (Classes::TCollection *)Classes::TCollection::InsertItem(a2, this);
  }
  return result;
}


// ==================== @Classes@TCollectionItem@SetIndex$qqri ====================
int __fastcall Classes::TCollectionItem::SetIndex(Classes::TCollectionItem *this, int a2)
{
  int result; // eax

  result = Classes::TCollectionItem::GetIndex(this);
  if ( result >= 0 && a2 != result )
  {
    Classes::TList::Move(*(Classes::TList **)(*((_DWORD *)this + 1) + 8), result, a2);
    return Classes::TCollectionItem::Changed((int)this, true);
  }
  return result;
}


// ==================== @Classes@TCollection@GetItemAttr$qqrii ====================
int __fastcall Classes::TCollection::GetItemAttr(Classes::TCollection *this, int a2, int a3, int a4)
{
  int v4; // eax

  v4 = sub_412284((int)this, a3);
  return (*(int (__fastcall **)(int, int))(*(_DWORD *)v4 + 12))(v4, a4);
}


// ==================== @Classes@TCollection@InsertItem$qqrp23Classes@TCollectionItem ====================
int __fastcall Classes::TCollection::InsertItem(Classes::TCollection *this, Classes::TCollectionItem *a2)
{
  if ( (unsigned __int8)System::__linkproc__ IsClass(a2, *((_DWORD *)this + 1)) == 0 )
    Classes::TList::Error(off_40FAFC, off_4EA3D4);
  Classes::TList::Add(*((Classes::TList **)this + 2), a2);
  *((_DWORD *)a2 + 1) = this;
  *((_DWORD *)a2 + 2) = (*((_DWORD *)this + 4))++;
  (*(void (__fastcall **)(Classes::TCollection *, Classes::TCollectionItem *))(*(_DWORD *)this + 12))(this, a2);
  Classes::TCollection::Changed(this);
  return sub_412514(this, a2, 0);
}


// ==================== @Classes@TStringList@ExchangeItems$qqrii ====================
int *__fastcall Classes::TStringList::ExchangeItems(Classes::TStringList *this, int a2, int a3)
{
  int *v3; // edx
  int *result; // eax
  int v5; // ecx
  int v6; // ecx

  v3 = (int *)(*((_DWORD *)this + 3) + 8 * a2);
  result = (int *)(*((_DWORD *)this + 3) + 8 * a3);
  v5 = *v3;
  *v3 = *result;
  *result = v5;
  v6 = v3[1];
  v3[1] = result[1];
  result[1] = v6;
  return result;
}


// ==================== @Classes@TStringList@InsertItem$qqrix17System@AnsiString ====================
int __fastcall Classes::TStringList::InsertItem(Classes::TStringList *this, int a2, int System::AnsiString)
{
  int v6; // eax
  _DWORD *v7; // esi

  (*(void (__fastcall **)(Classes::TStringList *))(*(_DWORD *)this + 116))(this);
  if ( *((_DWORD *)this + 4) == *((_DWORD *)this + 5) )
    Classes::TStringList::Grow(this);
  v6 = *((_DWORD *)this + 4);
  if ( a2 < v6 )
    System::Move(
      (const void *)(*((_DWORD *)this + 3) + 8 * a2),
      (void *)(*((_DWORD *)this + 3) + 8 * a2 + 8),
      8 * (v6 - a2));
  v7 = (_DWORD *)(*((_DWORD *)this + 3) + 8 * a2);
  *v7 = 0;
  v7[1] = 0;
  unknown_libname_36(v7, System::AnsiString);
  ++*((_DWORD *)this + 4);
  return (*(int (__fastcall **)(Classes::TStringList *))(*(_DWORD *)this + 112))(this);
}


// ==================== @Classes@TComponent@Notification$qqrp18Classes@TComponent18Classes@TOperation ====================
int __fastcall Classes::TComponent::Notification(Classes::TList **this, Classes::TComponent *a2, char a3)
{
  int result; // eax
  int v5; // ebx
  int v6; // edi
  int v7; // eax
  int v8; // ecx

  if ( a3 == 1 && a2 != nullptr )
    Classes::TComponent::RemoveFreeNotification((Classes::TComponent *)this, a2);
  result = (int)*(this + 4);
  if ( result != 0 && *(_DWORD *)(result + 8) - 1 >= 0 )
  {
    v5 = *(_DWORD *)(result + 8);
    v6 = 0;
    do
    {
      v7 = Classes::TList::Get(*(this + 4), v6);
      LOBYTE(v8) = a3;
      result = (*(int (__fastcall **)(int, Classes::TComponent *, int))(*(_DWORD *)v7 + 16))(v7, a2, v8);
      ++v6;
      --v5;
    }
    while ( v5 != 0 );
  }
  return result;
}


// ==================== @Idcontext@TIdContext@BeforeRun$qqrv ====================
int __fastcall Idcontext::TIdContext::BeforeRun(int this)
{
  if ( *(_WORD *)(this + 42) != 0 )
    return (*(int (__fastcall **)(_DWORD, int))(this + 40))(*(_DWORD *)(this + 44), this);
  return this;
}


// ==================== @Idcontext@TIdContext@BeforeRun$qqrv_0 ====================
int __fastcall Idcontext::TIdContext::BeforeRun(int this)
{
  if ( *(_WORD *)(this + 42) != 0 )
    return (*(int (__fastcall **)(_DWORD, int))(this + 40))(*(_DWORD *)(this + 44), this);
  return this;
}


// ==================== @Contnrs@TOrderedList@PeekItem$qqrv ====================
int __fastcall Contnrs::TOrderedList::PeekItem(Classes::TList **this)
{
  return Classes::TList::Get(*(this + 1), *((_DWORD *)*(this + 1) + 2) - 1);
}


// ==================== @Contnrs@TOrderedList@PopItem$qqrv ====================
int __fastcall Contnrs::TOrderedList::PopItem(Classes::TList **this)
{
  int v2; // esi

  v2 = (*((int (__fastcall **)(Classes::TList **))*this + 2))(this);
  Classes::TList::Delete(*(this + 1), *((_DWORD *)*(this + 1) + 2) - 1);
  return v2;
}


// ==================== @Stdctrls@TCustomLabel@Notification$qqrp18Classes@TComponent18Classes@TOperation ====================
int __fastcall Stdctrls::TCustomLabel::Notification(Controls::TControl *this, int a2, char a3)
{
  int result; // eax

  result = Controls::TControl::Notification(this);
  if ( a3 == 1 && a2 == *((_DWORD *)this + 74) )
  {
    *((_DWORD *)this + 74) = 0;
    return 0;
  }
  return result;
}


// ==================== @Controls@TControl@Notification$qqrp18Classes@TComponent18Classes@TOperation ====================
Classes::TComponent *__fastcall Controls::TControl::Notification(
        Classes::TList **this,
        Classes::TComponent *a2,
        char a3)
{
  Classes::TComponent *result; // eax

  result = (Classes::TComponent *)Classes::TComponent::Notification(this, a2, a3);
  if ( a3 == 1 )
  {
    if ( a2 == *(this + 27) )
    {
      return (Classes::TComponent *)Controls::TControl::SetPopupMenu((Classes::TComponent *)this, nullptr);
    }
    else
    {
      result = (Classes::TComponent *)Controls::TControl::GetAction((Controls::TControl *)this);
      if ( a2 == result )
        return (Classes::TComponent *)Controls::TControl::SetAction((Controls::TControl *)this, nullptr);
    }
  }
  return result;
}


// ==================== @Controls@TControl@WMNCLButtonDown$qqrr24Messages@TWMNCHitMessage ====================
int __fastcall Controls::TControl::WMNCLButtonDown(Controls::TControl *a1)
{
  Controls::TControl::SendCancelMode(a1, a1);
  return (*(int (**)(void))(*(_DWORD *)a1 - 16))();
}


// ==================== @Controls@TWinControl@WMNCHitTest$qqrr21Messages@TWMNCHitTest ====================
int __fastcall Controls::TWinControl::WMNCHitTest(int result, int a2)
{
  if ( (*(_BYTE *)(result + 32) & 0x10) == 0 || *(_DWORD *)(result + 36) == 0 )
    return (*(int (**)(void))(*(_DWORD *)result - 16))();
  *(_DWORD *)(a2 + 12) = 1;
  return result;
}


// ==================== @Controls@TWinControl@CMDesignHitTest$qqrr17Messages@TWMMouse ====================
int __fastcall Controls::TWinControl::CMDesignHitTest(Controls::TWinControl *this, int a2)
{
  int result; // eax

  result = Controls::TWinControl::IsControlMouseMsg(this);
  if ( (_BYTE)result == 0 )
    return Shadowwnd::TShadowWindow::WMEraseBkgnd(this, a2);
  return result;
}


// ==================== @Controls@TDockTree@InternalHitTest$qqrrx14Windows@TPointri ====================
int __fastcall Controls::TDockTree::InternalHitTest(Controls::TDockTree *a1, int a2, _DWORD *a3)
{
  Controls::TControl *v3; // eax
  Controls::TControl *v4; // ebx
  int ControlZone; // [esp+8h] [ebp-10h]
  int savedregs; // [esp+18h] [ebp+0h] BYREF

  ControlZone = 0;
  *a3 = 0;
  v3 = (Controls::TControl *)Controls::_17049(&savedregs);
  v4 = v3;
  if ( v3 != nullptr && *((_DWORD *)a1 + 5) == *((_DWORD *)v3 + 36) )
  {
    ControlZone = Controls::TDockTree::FindControlZone(a1, v3);
    if ( ControlZone != 0 )
      *a3 = 1;
  }
  else if ( *(_DWORD *)(*((_DWORD *)a1 + 25) + 8) != 0
         && unknown_libname_323(*((_DWORD *)a1 + 25)) >= 1
         && v4 == nullptr )
  {
    unknown_libname_330(&savedregs);
  }
  return ControlZone;
}


// ==================== @Controls@TDockTree@PruneZone$qqrp18Controls@TDockZone ====================
int __fastcall Controls::TDockTree::PruneZone(int this, Controls::TDockZone *a2)
{
  int v3; // esi
  int v4; // eax
  int v5; // eax
  int v6; // eax
  int savedregs; // [esp+8h] [ebp+0h] BYREF

  v3 = this;
  if ( a2 != nullptr )
  {
    if ( *((_DWORD *)a2 + 2) != 0 )
      unknown_libname_331(&savedregs);
    v4 = *((_DWORD *)a2 + 6);
    if ( v4 != 0 )
    {
      *(_DWORD *)(v4 + 12) = *((_DWORD *)a2 + 3);
    }
    else
    {
      v5 = *((_DWORD *)a2 + 5);
      if ( v5 != 0 )
        *(_DWORD *)(v5 + 8) = *((_DWORD *)a2 + 3);
    }
    v6 = *((_DWORD *)a2 + 3);
    if ( v6 != 0 )
      *(_DWORD *)(v6 + 24) = *((_DWORD *)a2 + 6);
    if ( a2 == *(Controls::TDockZone **)(v3 + 100) )
      *(_DWORD *)(v3 + 100) = 0;
    return System::TObject::Free(a2);
  }
  return this;
}


// ==================== @Actnlist@TCustomActionList@Notification$qqrp18Classes@TComponent18Classes@TOperation ====================
int __fastcall Actnlist::TCustomActionList::Notification(Classes::TList **this, Classes::TComponent *a2, char a3)
{
  int result; // eax

  result = Classes::TComponent::Notification(this, a2, a3);
  if ( a3 == 1 )
  {
    if ( a2 == *(this + 11) )
    {
      return Actnlist::TCustomActionList::SetImages((Actnlist::TCustomActionList *)this, nullptr);
    }
    else
    {
      result = System::__linkproc__ IsClass(a2, off_437F7C);
      if ( (_BYTE)result != 0 )
        return Actnlist::TCustomActionList::RemoveAction((Actnlist::TCustomActionList *)this, a2);
    }
  }
  return result;
}


// ==================== @Menus@TMenuItem@$bctr$qqrp18Classes@TComponent ====================
Menus::TMenuItem *__fastcall Menus::TMenuItem::TMenuItem(Menus::TMenuItem *this, Classes::TComponent *a2)
{
  Menus::TMenuItem *v2; // ebx
  int v3; // eax
  int v4; // edx
  int v5; // eax
  char v7; // [esp+Bh] [ebp-1h]

  if ( (_BYTE)a2 != 0 )
    this = (Menus::TMenuItem *)unknown_libname_26();
  v7 = (char)a2;
  v2 = this;
  v3 = Classes::TComponent::TComponent(this, nullptr);
  *((_BYTE *)v2 + 50) = 1;
  *((_BYTE *)v2 + 45) = 1;
  *((_BYTE *)v2 + 47) = 2;
  *((_BYTE *)v2 + 48) = 2;
  *((_WORD *)v2 + 34) = Menus::_16415(v3);
  *((_DWORD *)v2 + 13) = -1;
  LOBYTE(v4) = 1;
  v5 = unknown_libname_22(off_438DA4, v4);
  *((_DWORD *)v2 + 27) = v5;
  *(_DWORD *)(v5 + 12) = v2;
  *(_DWORD *)(v5 + 8) = Menus::TMenuItem::ImageListChange;
  if ( v7 != 0 )
    unknown_libname_27(v2);
  return v2;
}


// ==================== @Menus@TMenuItem@$bdtr$qqrv ====================
void __fastcall Menus::TMenuItem::~TMenuItem(Menus::TMenuItem *this)
{
  __int64 v1; // rax
  char v2; // bl
  int v3; // esi
  Menus::TMenuItem *v4; // eax
  System::TObject *Item; // eax
  unsigned __int16 v6; // ax
  System::TObject *v7; // eax

  v1 = unknown_libname_28(this);
  v2 = BYTE4(v1);
  v3 = v1;
  Menus::TMenuItemStack::ClearItem(dword_4EB7C4, (Menus::TMenuItem *)v1);
  v4 = *(Menus::TMenuItem **)(v3 + 88);
  if ( v4 != nullptr )
  {
    Menus::TMenuItem::Remove(v4, (Menus::TMenuItem *)v3);
    *(_DWORD *)(v3 + 88) = 0;
  }
  while ( (int)Menus::TMenuItem::GetCount((Menus::TMenuItem *)v3) > 0 )
  {
    Item = (System::TObject *)Menus::TMenuItem::GetItem((Menus::TMenuItem *)v3, 0);
    System::TObject::Free(Item);
  }
  if ( *(_DWORD *)(v3 + 40) != 0 )
  {
    Menus::TMenuItem::MergeWith((Menus::TMenuItem *)v3, nullptr);
    DestroyMenu(*(HMENU *)(v3 + 40));
  }
  System::TObject::Free(*(System::TObject **)(v3 + 80));
  Sysutils::FreeAndNil((void *)(v3 + 56));
  Sysutils::FreeAndNil((void *)(v3 + 108));
  v6 = *(_WORD *)(v3 + 68);
  if ( v6 != 0 )
    Classes::TBits::SetBit(dword_4EB7CC, v6, false);
  v7 = *(System::TObject **)(v3 + 64);
  if ( v7 != nullptr )
    System::TObject::Free(v7);
  Classes::TComponent::~TComponent((Classes::TComponent *)v3);
  if ( v2 > 0 )
    System::__linkproc__ ClassDestroy(v3);
}


// ==================== @Menus@TMenuItem@AppendTo$qqruio ====================
int __fastcall Menus::TMenuItem::AppendTo(Menus::TMenuItem *this, HMENU hMenu, bool a3)
{
  int Count; // eax
  int v6; // esi
  int v7; // ecx
  int ParentMenu; // eax
  bool v9; // al
  char v10; // zf
  UINT v11; // esi
  UINT_PTR Handle; // eax
  const CHAR *v13; // eax
  const CHAR *v15; // [esp-10h] [ebp-5Ch]
  unsigned int v16[2]; // [esp-Ch] [ebp-58h] BYREF
  int *v17; // [esp-4h] [ebp-50h]
  int v18; // [esp+Ch] [ebp-40h] BYREF
  MENUITEMINFOA v19; // [esp+13h] [ebp-39h] BYREF
  bool v20; // [esp+43h] [ebp-9h]
  int v21; // [esp+44h] [ebp-8h] BYREF
  bool v22; // [esp+4Bh] [ebp-1h]
  int savedregs; // [esp+4Ch] [ebp+0h] BYREF

  v18 = 0;
  v21 = 0;
  v22 = a3;
  v17 = &savedregs;
  v16[1] = (unsigned int)&loc_43C1B7;
  v16[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v16);
  if ( *((_BYTE *)this + 50) != 0 )
  {
    unknown_libname_37(&v21, *((_DWORD *)this + 9));
    Count = Menus::TMenuItem::GetCount(this);
    if ( Count <= 0 )
    {
      if ( *((_WORD *)this + 42) != 0 )
      {
        v6 = *((_DWORD *)this + 22);
        if ( v6 == 0
          || *(_DWORD *)(v6 + 88) != 0
          || (_BYTE)(Count = System::__linkproc__ IsClass(*(_DWORD *)(v6 + 4), off_43AEEC)) == 0 )
        {
          LOWORD(Count) = *((_WORD *)this + 42);
          sub_43B64C(Count, (int)&v18);
          System::__linkproc__ LStrCatN(&v21, 3, v7, &str___6[1], v18);
        }
      }
    }
    else
    {
      v19.hSubMenu = (HMENU)Menus::TMenuItem::GetHandle(this);
    }
    if ( (unsigned __int8)GetVersion() < 4u )
    {
      System::__linkproc__ LStrCmp(*((_DWORD *)this + 9), &str___7[1]);
      v11 = dword_4E8920[*((unsigned __int8 *)this + 45)]
          | dword_4E8918[*((unsigned __int8 *)this + 44)]
          | dword_4E8928[*((unsigned __int8 *)this + 60)]
          | dword_4E8934[v10]
          | 0x400;
      if ( (int)Menus::TMenuItem::GetCount(this) <= 0 )
      {
        v13 = (const CHAR *)unknown_libname_45(v21);
        InsertMenuA(hMenu, 0xFFFFFFFF, v11, *((unsigned __int16 *)this + 34), v13);
      }
      else
      {
        v15 = (const CHAR *)unknown_libname_45(*((_DWORD *)this + 9));
        Handle = Menus::TMenuItem::GetHandle(this);
        InsertMenuA(hMenu, 0xFFFFFFFF, v11 | 0x10, Handle, v15);
      }
    }
    else
    {
      v19.cbSize = 44;
      v19.fMask = 63;
      ParentMenu = Menus::TMenuItem::GetParentMenu(this);
      v9 = ParentMenu != 0 && (*(_BYTE *)(ParentMenu + 52) != 0 || Menus::TMenuItem::GetImageList(this) != 0)
        || *((_DWORD *)this + 16) != 0
        && (*(unsigned __int8 (__fastcall **)(_DWORD))(**((_DWORD **)this + 16) + 28))(*((_DWORD *)this + 16)) == 0;
      v20 = v9;
      System::__linkproc__ LStrCmp(*((_DWORD *)this + 9), &str___7[1]);
      v19.fType = dword_4E8978[v20]
                | dword_4E8970[v22]
                | dword_4E893C[*((unsigned __int8 *)this + 60)]
                | dword_4E8960[*((unsigned __int8 *)this + 49)]
                | dword_4E8968[v10];
      v19.fState = dword_4E8950[*((unsigned __int8 *)this + 46)]
                 | dword_4E8958[*((unsigned __int8 *)this + 45)]
                 | dword_4E8948[*((unsigned __int8 *)this + 44)];
      v19.wID = *((unsigned __int16 *)this + 34);
      memset(&v19.hSubMenu, 0, 12);
      v19.dwTypeData = (LPSTR)unknown_libname_45(v21);
      if ( (int)Menus::TMenuItem::GetCount(this) > 0 )
        v19.hSubMenu = (HMENU)Menus::TMenuItem::GetHandle(this);
      InsertMenuItemA(hMenu, 0xFFFFFFFF, -1, &v19);
    }
  }
  __writefsdword(0, v16[0]);
  v17 = (int *)&loc_43C1BE;
  unknown_libname_34(&v18);
  return unknown_libname_34(&v21);
}


// ==================== @Menus@TMenuItem@PopulateMenu$qqrv ====================
int __fastcall Menus::TMenuItem::PopulateMenu(Menus::TMenuItem **this)
{
  if ( *(this + 25) != nullptr && (unsigned __int8)System::__linkproc__ IsClass(*(this + 25), off_43AEEC) != 0 )
  {
    Menus::TMenuItem::InternalRethinkHotkeys((Menus::TMenuItem *)this, false);
    Menus::TMenuItem::InternalRethinkLines((Menus::TMenuItem *)this, false);
  }
  if ( *(this + 25) != nullptr )
    sub_43F7BC(*(this + 25));
  return Menus::_16431((int)unknown_libname_375, *(this + 23));
}


// ==================== @TMenuItem@ReadShortCutText$qqrp7TReader ====================
int __fastcall TMenuItem::ReadShortCutText(TMenuItem *this, TReader *a2)
{
  unsigned __int16 v3; // ax
  unsigned int v5[2]; // [esp-14h] [ebp-18h] BYREF
  int *v6; // [esp-Ch] [ebp-10h]
  int v7; // [esp+0h] [ebp-4h] BYREF
  int savedregs; // [esp+4h] [ebp+0h] BYREF

  v7 = 0;
  v6 = &savedregs;
  v5[1] = (unsigned int)&loc_43C2B6;
  v5[0] = (unsigned int)NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)v5);
  TReader::ReadString(a2);
  v3 = sub_43B8AC(v7);
  Menus::TMenuItem::SetShortCut(this, v3);
  __writefsdword(0, v5[0]);
  v6 = (int *)&loc_43C2BD;
  return unknown_libname_34(&v7);
}


// ==================== @Menus@TMenuItem@MergeWith$qqrp15Menus@TMenuItem ====================
Menus::TMenuItem *__fastcall Menus::TMenuItem::MergeWith(Menus::TMenuItem *this, Menus::TMenuItem *a2)
{
  Menus::TMenuItem *v2; // ecx

  v2 = *((Menus::TMenuItem **)this + 23);
  if ( a2 != v2 )
  {
    if ( v2 != nullptr )
      *((_DWORD *)v2 + 24) = 0;
    *((_DWORD *)this + 23) = a2;
    if ( a2 != nullptr )
      *((_DWORD *)a2 + 24) = this;
    return (Menus::TMenuItem *)Menus::TMenuItem::RebuildHandle(this);
  }
  return this;
}


// ==================== @Menus@TMenuItem@Loaded$qqrv ====================
int __fastcall Menus::TMenuItem::Loaded(Menus::TMenuItem *this)
{
  int result; // eax
  int Action; // eax
  int v4; // ecx

  Classes::TComponent::Loaded(this);
  result = Menus::TMenuItem::GetAction(this);
  if ( result != 0 )
  {
    Action = Menus::TMenuItem::GetAction(this);
    LOBYTE(v4) = 1;
    result = unknown_libname_25(this, Action, v4);
  }
  if ( *((_BYTE *)this + 104) != 0 )
    return Menus::TMenuItem::RebuildHandle(this);
  return result;
}


// ==================== @Menus@TMenuItem@RebuildHandle$qqrv ====================
int __fastcall Menus::TMenuItem::RebuildHandle(int this)
{
  int v1; // ebx
  Menus::TMenuItem *v2; // eax
  HMENU v3; // eax
  HMENU Handle; // eax

  v1 = this;
  if ( (*(_BYTE *)(this + 32) & 8) == 0 )
  {
    if ( (*(_BYTE *)(this + 32) & 2) != 0 )
    {
      *(_BYTE *)(this + 104) = 1;
    }
    else
    {
      v2 = *(Menus::TMenuItem **)(this + 96);
      if ( v2 != nullptr )
      {
        return Menus::TMenuItem::RebuildHandle(v2);
      }
      else
      {
        while ( 1 )
        {
          Handle = (HMENU)Menus::TMenuItem::GetHandle((Menus::TMenuItem *)v1);
          if ( GetMenuItemCount(Handle) <= 0 )
            break;
          v3 = (HMENU)Menus::TMenuItem::GetHandle((Menus::TMenuItem *)v1);
          RemoveMenu(v3, 0, 0x400u);
        }
        if ( *(_DWORD *)(v1 + 88) != 0
          || (unsigned __int8)System::__linkproc__ IsClass(*(_DWORD *)(v1 + 100), off_43AEEC) == 0 )
        {
          Menus::TMenuItem::PopulateMenu((Menus::TMenuItem **)v1);
        }
        else
        {
          DestroyMenu(*(HMENU *)(v1 + 40));
          *(_DWORD *)(v1 + 40) = 0;
        }
        return (*(int (__fastcall **)(int, _DWORD))(*(_DWORD *)v1 + 56))(v1, 0);
      }
    }
  }
  return this;
}


// ==================== @Menus@TMenuItem@VerifyGroupIndex$qqriuc ====================
// positive sp value has been detected, the output may be wrong!
int __fastcall Menus::TMenuItem::VerifyGroupIndex(Menus::TMenuItem *this, int a2, unsigned __int8 a3)
{
  int result; // eax
  int v6; // esi
  int v7; // ebp
  int v8; // edx
  int v9; // ecx
  int v10; // [esp-10h] [ebp-24h]
  int v11; // [esp-Ch] [ebp-20h]
  int v12; // [esp-8h] [ebp-1Ch]
  int v13; // [esp-4h] [ebp-18h]

  v10 = a2;
  result = Menus::TMenuItem::GetCount(this);
  if ( result - 1 >= 0 )
  {
    v6 = result;
    v7 = 0;
    do
    {
      if ( v7 >= v10 )
      {
        result = Menus::TMenuItem::GetItem(this, v7);
        if ( a3 > *(_BYTE *)(result + 51) )
        {
          result = Menus::TMenuItem::GetItem(this, v7);
          *(_BYTE *)(result + 51) = a3;
        }
      }
      else
      {
        result = Menus::TMenuItem::GetItem(this, v7);
        if ( a3 < *(_BYTE *)(result + 51) )
          result = unknown_libname_371(off_4EA094, v8, v9, v13, v12, v11, v10);
      }
      ++v7;
      --v6;
    }
    while ( v6 != 0 );
  }
  return result;
}


// ==================== @Menus@TMenuItem@GetHandle$qqrv ====================
// positive sp value has been detected, the output may be wrong!
int __fastcall Menus::TMenuItem::GetHandle(Menus::TMenuItem *this)
{
  int v2; // edx
  int v3; // ecx
  int v5; // [esp-10h] [ebp-14h]
  int v6; // [esp-Ch] [ebp-10h]
  int v7; // [esp-8h] [ebp-Ch]
  int v8; // [esp-4h] [ebp-8h]

  if ( *((_DWORD *)this + 10) == 0 )
  {
    if ( (unsigned __int8)System::__linkproc__ IsClass(*((_DWORD *)this + 1), off_43B1C4) != 0 )
      *((_DWORD *)this + 10) = CreatePopupMenu();
    else
      *((_DWORD *)this + 10) = CreateMenu();
    if ( *((_DWORD *)this + 10) == 0 )
      unknown_libname_371(off_4EA044, v2, v3, v8, v7, v6, v5);
    Menus::TMenuItem::PopulateMenu((Menus::TMenuItem **)this);
  }
  return *((_DWORD *)this + 10);
}


// ==================== @Menus@TMenuItem@DefineProperties$qqrp14Classes@TFiler ====================
int __fastcall Menus::TMenuItem::DefineProperties(int a1, int a2)
{
  Classes::TComponent::DefineProperties(a1, a2);
  return (*(int (__fastcall **)(int, _strings *, _DWORD, int, int (__fastcall *)(TMenuItem *, TReader *), _DWORD, _DWORD))(*(_DWORD *)a2 + 4))(
           a2,
           &str_ShortCutText[1],
           0,
           a1,
           TMenuItem::ReadShortCutText,
           0,
           0);
}


// ==================== @Menus@TMenuItem@DrawItem$qqrp16Graphics@TCanvasrx13Windows@TRecto ====================
int __fastcall Menus::TMenuItem::DrawItem(int result, int a2, __int128 *a3, char a4)
{
  __int128 *v4; // ecx
  __int128 v5; // [esp+Ch] [ebp-10h] BYREF

  v5 = *a3;
  if ( *(_WORD *)(result + 138) != 0 )
  {
    v4 = &v5;
    LOBYTE(v4) = a4;
    return (*(int (__fastcall **)(_DWORD, int, int, __int128 *, __int128 *))(result + 136))(
             *(_DWORD *)(result + 140),
             result,
             a2,
             &v5,
             v4);
  }
  return result;
}


// ==================== @Menus@TMenuItem@AdvancedDrawItem$qqrp16Graphics@TCanvasrx13Windows@TRect47System@%Set$t18Windows@Windows__1$iuc$0$iuc$12%o ====================
int __fastcall Menus::TMenuItem::AdvancedDrawItem(int a1, int a2, _OWORD *a3, __int16 a4, int a5)
{
  int v5; // eax
  int result; // eax
  _QWORD *v7; // eax
  int ImageList; // [esp+34h] [ebp-3Ch]
  int ParentMenu; // [esp+44h] [ebp-2Ch]
  _QWORD v12[4]; // [esp+50h] [ebp-20h] BYREF
  int savedregs; // [esp+70h] [ebp+0h] BYREF

  *(_OWORD *)v12 = *a3;
  ParentMenu = Menus::TMenuItem::GetParentMenu((Menus::TMenuItem *)a1);
  ImageList = Menus::TMenuItem::GetImageList((Menus::TMenuItem *)a1);
  if ( ParentMenu != 0
    && (*(_BYTE *)(ParentMenu + 52) != 0 || ImageList != 0)
    && ((v5 = a1, *(_WORD *)(a1 + 146) != 0) || (v5 = a1, *(_WORD *)(a1 + 138) != 0)) )
  {
    LOBYTE(v5) = (a4 & 1) != 0;
    (*(void (__fastcall **)(int, int, _QWORD *, int))(*(_DWORD *)a1 + 52))(a1, a2, v12, v5);
    result = a1;
    if ( *(_WORD *)(a1 + 146) != 0 )
    {
      v7 = v12;
      LOWORD(v7) = a4;
      return (*(int (__fastcall **)(_DWORD, int, int, _QWORD *, _QWORD *))(a1 + 144))(
               *(_DWORD *)(a1 + 148),
               a1,
               a2,
               v12,
               v7);
    }
  }
  else if ( ParentMenu == 0 || (unsigned __int8)sub_43F7BC(ParentMenu) != 0 )
  {
    return Menus::_16480((int)&savedregs);
  }
  else
  {
    return Menus::_16479((int)&savedregs);
  }
  return result;
}


// ==================== @Menus@TMenuItem@GetImageList$qqrv ====================
int __fastcall Menus::TMenuItem::GetImageList(Menus::TMenuItem *this)
{
  int v1; // ebx
  int i; // edx
  int ParentMenu; // eax

  v1 = 0;
  for ( i = *((_DWORD *)this + 22); i != 0 && *(_DWORD *)(i + 112) == 0; i = *(_DWORD *)(i + 88) )
    ;
  if ( i != 0 )
    return *(_DWORD *)(i + 112);
  ParentMenu = Menus::TMenuItem::GetParentMenu(this);
  if ( ParentMenu != 0 )
    return *(_DWORD *)(ParentMenu + 60);
  return v1;
}


// ==================== @Menus@TMenuItem@MeasureItem$qqrp16Graphics@TCanvasrit2 ====================
int __fastcall Menus::TMenuItem::MeasureItem(void **this, Graphics::TCanvas *a2, int *a3, int *a4)
{
  int v5; // eax
  char v6; // bl
  int ParentMenu; // edi
  char v8; // zf
  char v9; // al
  unsigned __int8 v10; // bl
  struct _EXCEPTION_REGISTRATION_RECORD *ExceptionList; // [esp-Ch] [ebp-3Ch] BYREF
  void *v13; // [esp-8h] [ebp-38h]
  int *v14; // [esp-4h] [ebp-34h]
  void *v15; // [esp+0h] [ebp-30h]
  void *v16; // [esp+Ch] [ebp-24h] BYREF
  UINT uFormat[4]; // [esp+10h] [ebp-20h] BYREF
  int v18; // [esp+20h] [ebp-10h] BYREF
  int ImageList; // [esp+24h] [ebp-Ch]
  int v20; // [esp+28h] [ebp-8h]
  int *v21; // [esp+2Ch] [ebp-4h]
  int savedregs; // [esp+30h] [ebp+0h] BYREF

  v16 = nullptr;
  v18 = 0;
  v21 = a3;
  v20 = (int)a2;
  v14 = &savedregs;
  v13 = &loc_43DB8E;
  ExceptionList = NtCurrentTeb()->NtTib.ExceptionList;
  __writefsdword(0, (unsigned int)&ExceptionList);
  v5 = unknown_libname_25(this, a2, a3);
  if ( (unsigned __int8)System::__linkproc__ IsClass(v5, off_43AEEC) != 0 )
  {
    v6 = 1;
    unknown_libname_377(&savedregs);
  }
  else
  {
    v6 = 0;
  }
  ParentMenu = Menus::TMenuItem::GetParentMenu((Menus::TMenuItem *)this);
  ImageList = Menus::TMenuItem::GetImageList((Menus::TMenuItem *)this);
  System::__linkproc__ LStrCmp(*(this + 9), &str___12[1]);
  if ( v8 )
  {
    *a4 = 5;
    *v21 = -2;
    v9 = 0;
  }
  else if ( ImageList == 0 || (int)*(this + 13) <= -1 && v6 != 0 )
  {
    if ( *(this + 16) == nullptr
      || (*(unsigned __int8 (__fastcall **)(_DWORD))(*(_DWORD *)*(this + 16) + 28))(*(this + 16)) != 0 )
    {
      *v21 = -7;
      v9 = 0;
    }
    else
    {
      *v21 = 16;
      if ( v6 == 0 )
        *a4 = 16;
      v9 = 1;
    }
  }
  else
  {
    *v21 = *(_DWORD *)(ImageList + 40);
    if ( v6 == 0 )
      *a4 = *(_DWORD *)(ImageList + 36);
    v9 = 1;
  }
  if ( v9 != 0 && v6 == 0 )
    *v21 += 15;
  if ( v6 == 0 )
    *a4 += 3;
  System::__linkproc__ FillChar(uFormat, 16, 0);
  if ( ParentMenu != 0 )
  {
    v10 = 0;
  }
  else if ( (unsigned __int8)System::__linkproc__ IsClass(0, off_43B1C4) != 0 )
  {
    v10 = MEMORY[0x50];
  }
  else
  {
    v10 = 0;
  }
  LOWORD(ParentMenu) = *((_WORD *)this + 42);
  if ( (_WORD)ParentMenu != 0 )
  {
    sub_43B64C(ParentMenu, (int)&v16);
    System::__linkproc__ LStrCat3((int)&v18, *(this + 9), v16);
  }
  else
  {
    unknown_libname_37(&v18, *(this + 9));
  }
  unknown_libname_376(
    (Menus::TMenuItem *)this,
    v20,
    v18,
    (UINT)uFormat,
    0,
    (LPRECT)((unsigned __int16)word_4E8990[v10] | 0x560));
  *v21 += uFormat[2] - uFormat[0] + 7;
  if ( *((_WORD *)this + 77) != 0 )
    ((void (__fastcall *)(_DWORD, void **, int, int *, int *))*(this + 38))(*(this + 39), this, v20, v21, a4);
  __writefsdword(0, (unsigned int)v13);
  v15 = &loc_43DB95;
  unknown_libname_34(&v16);
  return unknown_libname_34(&v18);
}


// ==================== @Menus@TMenuItem@SetBreak$qqr16Menus@TMenuBreak ====================
_BYTE *__fastcall Menus::TMenuItem::SetBreak(_BYTE *result, char a2)
{
  if ( a2 != result[60] )
  {
    result[60] = a2;
    return (_BYTE *)(*(int (__fastcall **)(_BYTE *, char))(*(_DWORD *)result + 56))(result, 1);
  }
  return result;
}


// ==================== @Menus@TMenuItem@SetCaption$qqrx17System@AnsiString ====================
int __fastcall Menus::TMenuItem::SetCaption(Menus::TMenuItem *this, int System::AnsiString)
{
  int result; // eax
  char v5; // zf
  int v6; // edx

  result = System::__linkproc__ LStrCmp(*((_DWORD *)this + 9), System::AnsiString);
  if ( !v5 )
  {
    unknown_libname_36((char *)this + 36, System::AnsiString);
    LOBYTE(v6) = 1;
    return (*(int (__fastcall **)(Menus::TMenuItem *, int))(*(_DWORD *)this + 56))(this, v6);
  }
  return result;
}


// ==================== @Menus@TMenuItem@SetChecked$qqro ====================
DWORD __fastcall Menus::TMenuItem::SetChecked(DWORD this, bool a2)
{
  DWORD v3; // esi
  Menus::TMenuItem *v4; // edi
  HMENU Handle; // eax
  UINT v6; // [esp-8h] [ebp-14h]
  UINT v7; // [esp-4h] [ebp-10h]

  v3 = this;
  if ( a2 != *(_BYTE *)(this + 44) )
  {
    *(_BYTE *)(this + 44) = a2;
    v4 = *(Menus::TMenuItem **)(this + 88);
    if ( v4 != nullptr && (*(_BYTE *)(this + 32) & 2) == 0 )
    {
      v7 = dword_4E8918[a2];
      v6 = *(unsigned __int16 *)(this + 68);
      Handle = (HMENU)Menus::TMenuItem::GetHandle(v4);
      this = CheckMenuItem(Handle, v6, v7);
    }
    if ( a2 && *(_BYTE *)(v3 + 49) != 0 )
      return unknown_libname_378(v3);
  }
  return this;
}


// ==================== @Menus@TMenuItem@SetEnabled$qqro ====================
Menus::TMenuItem *__fastcall Menus::TMenuItem::SetEnabled(Menus::TMenuItem *this, bool a2)
{
  Menus::TMenuItem *v3; // esi
  int v4; // edi
  Menus::TMenuItem *v5; // edi
  HMENU Handle; // eax
  UINT v7; // [esp-8h] [ebp-14h]
  UINT v8; // [esp-4h] [ebp-10h]

  v3 = this;
  if ( a2 != *((_BYTE *)this + 45) )
  {
    *((_BYTE *)this + 45) = a2;
    if ( *off_4EA6BC[0] == 2 && Menus::TMenuItem::GetCount(this) != 0 )
      return (Menus::TMenuItem *)(*(int (__fastcall **)(Menus::TMenuItem *, char))(*(_DWORD *)v3 + 56))(v3, 1);
    v4 = *((_DWORD *)v3 + 22);
    if ( v4 != 0 && *(_DWORD *)(v4 + 96) != 0 )
    {
      return (Menus::TMenuItem *)(*(int (__fastcall **)(Menus::TMenuItem *, char))(*(_DWORD *)v3 + 56))(v3, 1);
    }
    else
    {
      v5 = *((Menus::TMenuItem **)v3 + 22);
      if ( v5 != nullptr && (*((_BYTE *)v3 + 32) & 2) == 0 )
      {
        v8 = dword_4E8920[a2];
        v7 = *((unsigned __int16 *)v3 + 34);
        Handle = (HMENU)Menus::TMenuItem::GetHandle(v5);
        EnableMenuItem(Handle, v7, v8);
      }
      return (Menus::TMenuItem *)(*(int (__fastcall **)(Menus::TMenuItem *, _DWORD))(*(_DWORD *)v3 + 56))(v3, 0);
    }
  }
  return this;
}


