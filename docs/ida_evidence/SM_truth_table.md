# SM 真值表（IDA ProcessServerMessage 0x467920~0x46AF50，反编译 case 枚举 140 个）
伪代码长度 52704，证据 scratch/ida_processmsg.c

| SM编号 | 真值表 | 代码定义 | 结论 |
|---|---|---|---|
| 6 SM_HEAR | YES | 定义 | 在真值表 |
| 7 SM_SYSTEMMESSAGE | YES | 定义 | 在真值表 |
| 10 SM_ENTITYAPPEAR | YES | 定义 | 在真值表 |
| 10 SM_WALK_DELPHI | YES | 定义 | 在真值表 |
| 11 SM_ENTITYDISAPPEAR | YES | 定义 | 在真值表 |
| 11 SM_RUN_DELPHI | YES | 定义 | 在真值表 |
| 12 SM_HIT_DELPHI | YES | 定义 | 在真值表 |
| 13 SM_ENTITYMOVE | YES | 定义 | 在真值表 |
| 14 SM_ENTITYTURN | YES | 定义 | 在真值表 |
| 15 SM_DIGUP_ACTION_DELPHI | YES | 定义 | 在真值表 |
| 15 SM_BATCHSTRUCK | YES | 定义 | 在真值表 |
| 16 SM_FLYAXE_ACTION_DELPHI | YES | 定义 | 在真值表 |
| 16 SM_BATCHMOVE | YES | 定义 | 在真值表 |
| 17 SM_LIGHTING_DELPHI | YES | 定义 | 在真值表 |
| 17 SM_MOVEFAIL | YES | 定义 | 在真值表 |
| 20 SM_STARTPLAY | YES | 定义 | 在真值表 |
| 21 SM_DEATH | YES | 定义 | 在真值表 |
| 22 SM_ACTION_PARAM_SYNC | YES | 定义 | 在真值表 |
| 23 SM_MAPMOVE | YES | 定义 | 在真值表 |
| 27 SM_SITDOWN | YES | 定义 | 在真值表 |
| 30 SM_SPELLRESULT | YES | 定义 | 在真值表 |
| 31 SM_SPELL2 | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 32 SM_SPELLEFFECT | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 33 SM_THROW | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 35 SM_LIGHTING | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 36 SM_DIGUP | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 37 SM_DIGDOWN | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 40 SM_GOLDCHANGED | YES | 定义 | 在真值表 (IDA case '(' @ 0x46A140) |
| 41 SM_CHANGENAMECOLOR | YES | 定义 | 在真值表 (IDA case ')' @ 0x46A157) |
| 42 SM_GUILDNAME_DELPHI | YES | 定义 | 在真值表 (IDA case '*' @ 0x46A16F) |
| 44 SM_LEVELUP | YES | 定义 | 在真值表 (IDA case ',' @ 0x46A18D) |
| 45 SM_EXPERIENCE | YES | 定义 | 在真值表 (IDA case '-' @ 0x46A1B0) |
| 46 SM_CHANGELIGHT | YES | 定义 | 在真值表 (IDA case '.' @ 0x46A1B8) |
| 50 SM_LOGON_DELPHI | YES | 定义 | 在真值表 (IDA case 50 @ 0x46A1D0) |
| 52 SM_SENDUSERSTATE | YES | 定义 | 在真值表 (IDA case '4' 52 @ 0x46A250) |
| 53 SM_HEALTHSPELLCHANGED | YES | 定义 | 在真值表 (IDA case '5' 53 @ 0x46A275) |
| 54 SM_NPCDIALOG | YES | 定义 | 在真值表 (IDA case '6' 54 @ 0x46A290) |
| 100 SM_SYSMSG_DELPHI | YES | 定义 | 在真值表 (IDA 100~104 @ 0x46AF10) |
| 114 SM_SERVERINDEX | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 120 SM_CHARLIST | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 200 SM_ADDITEM | YES | 定义 | 在真值表 (IDA case 200 @ 0x46A2B5) |
| 201 SM_SENDUSERMAGIC | YES | 定义 | 在真值表 (IDA case 201 @ 0x46A2D0) |
| 202 SM_DELITEM | YES | 定义 | 在真值表 (IDA case 202 @ 0x46A2F0) |
| 203 SM_UPDATEUSERMAGIC | YES | 定义 | 在真值表 (IDA case 203 @ 0x46A310) |
| 203 SM_ALLOWMOVE | YES | 定义 | 在真值表 (IDA case 203 @ 0x46A310) |
| 204 SM_TURNDIRECTION | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 204 SM_TURNDIRECTION_DELPHI | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 211 SM_SENDMYMAGIC | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 212 SM_DELMAGIC | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 220 SM_MONSPAWN | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 221 SM_MONDELETE | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 222 SM_MONDEAD | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 230 SM_NPCSPAWN | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 231 SM_NPCDELETE | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 243 SM_OBJECTSPELL | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 244 SM_OBJECTSTRUCK | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 314 SM_GROUNDITEM | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 315 SM_DELGROUNDITEM | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 501 SM_VERSION_FAIL | YES | 定义 | 在真值表 (IDA 0x46B68C 补丁版本不符) |
| 503 SM_CHANGEPASS_FAIL | YES | 定义 | 在真值表 (IDA ProcessServerMessage 0x467920: -1~-5 密码修改失败) |
| 504 SM_NEWID_SUCCESS | YES | 定义 | 在真值表 (IDA ProcessServerMessage 0x467920: 0x46AFE8 账号注册成功) |
| 505 SM_NEWID_FAIL | YES | 定义 | 在真值表 (IDA ProcessServerMessage 0x467920: 0x46B0A8~0x46B12C 账号注册失败) |
| 506 SM_CHANGEPASS_OK | YES | 定义 | 在真值表 (IDA ProcessServerMessage 0x467920: 0x46B59C 密码修改成功) |
| 507 SM_SELECTSERVER_FAIL | YES | 定义 | 在真值表 (IDA ProcessServerMessage 0x467920: 0x46B5DC~0x46B668 选服失败锁定/满员) |
| 518 SM_QUERYCHR | NO | 定义 | 不在真值表（存疑/变体/错号，官方为 523/528） |
| 519 SM_NEWCHR_SUCCESS | NO | 定义 | 不在真值表（存疑/变体/错号，官方为 521） |
| 520 SM_NEWCHR_FAIL / SM_SERVERLIST | YES | 定义 | 在真值表 (IDA case 520 handle_sm_server_list) |
| 521 SM_NEWCHAR_SUCCESS_DELPHI | YES | 定义 | 在真值表 (IDA case 521 send_query_char 创角成功) |
| 522 SM_NEWCHAR_FAIL_DELPHI | YES | 定义 | 在真值表 (IDA case 522 0x46B4C8~0x46B55C 创角失败) |
| 523 SM_DELCHAR_SUCCESS_DELPHI / SM_QUERYCHAR_OK | YES | 定义 | 在真值表 (IDA case 523 send_query_char 删角成功) |
| 524 SM_DELCHAR_FAIL_DELPHI | YES | 定义 | 在真值表 (IDA case 524 0x46B624 删角失败) |
| 525 SM_DELCHR_SUCCESS | NO | 定义 | 不在真值表（存疑/变体/错号，官方为 523） |
| 526 SM_OUTOFCONNECTION | YES | 定义 | 在真值表 |
| 527 SM_DISCONNECT_DELPHI | YES | 定义 | 在真值表 |
| 528 SM_QUERYCHAR_FAIL_DELPHI | YES | 定义 | 在真值表 (IDA 0x46B758 连接超时断开) |
| 529 SM_BILLING_NOTICE | YES | 定义 | 在真值表 (IDA 0x46B3C8 服务到期) |
| 530 SM_LOGINSUCCESS | YES | 定义 | 在真值表 |
| 532 SM_UPDATEID_SUCCESS / SM_CHANGEPASS_OK | YES | 定义 | 在真值表 (IDA 0x46B59C 密码修改成功) |
| 533 SM_UPDATEID_FAIL | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 600 SM_BAGITEMS / SM_REFRESH | YES | 定义 | 在真值表 (IDA case 0x258u handle_sm_refresh) |
| 601 SM_MINERESULT / SM_REFRESH2 | YES | 定义 | 在真值表 (IDA case 0x259u handle_sm_refresh2) |
| 603 SM_UPDATEITEM | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 604 SM_SENDUSEITEMS | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 606 SM_EAT_OK | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 610 SM_LISTADD_DELPHI | YES | 定义 | 在真值表 (IDA case 0x262u handle_sm_list_add @ 0x469B14) |
| 611 SM_LISTDEL_DELPHI | YES | 定义 | 在真值表 (IDA case 0x263u handle_sm_list_del @ 0x469B2A) |
| 612 SM_MAPMARK_DELPHI | YES | 定义 | 在真值表 (IDA case 0x264u handle_sm_map_mark @ 0x469B38 小地图标记) |
| 613 SM_NOTICESOUND_DELPHI | YES | 定义 | 在真值表 (IDA case 0x265u play_notice_sound @ 0x469B4B 提示音) |
| 614 SM_SPELLEFFECT_DELPHI | YES | 定义 | 在真值表 (IDA case 614 @ 0x469B56 施法光效与受击结算) |
| 615 SM_TAKEON_OK | YES | 定义 | 在真值表 |
| 615 SM_EXCHGTAKEONOK | YES | 定义 | 在真值表 |
| 616 SM_TAKEON_FAIL | YES | 定义 | 在真值表 |
| 616 SM_EXCHGTAKEONFAIL | YES | 定义 | 在真值表 |
| 619 SM_TAKEOFF_OK | YES | 定义 | 在真值表 |
| 621 SM_EQUIPINFO | YES | 定义 | 在真值表 |
| 622 SM_WEIGHTCHANGED | YES | 定义 | 在真值表 |
| 622 SM_SENDDETAILGOODSLIST | YES | 定义 | 在真值表 |
| 623 SM_SENDREPAIRCOST | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 625 SM_ITEMSHOW | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 625 SM_REPAIROK | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 626 SM_REPAIRFAIL | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 629 SM_BUYITEMSUCCESS | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 630 SM_BUYITEMFAIL | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 631 SM_MERCHANTDLG | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 631 SM_SELLITEMOK | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 632 SM_BUYITEM_SUCCESS | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 632 SM_SELLITEMFAIL | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 633 SM_BUYITEM_FAIL | YES | 定义 | 在真值表 |
| 633 SM_MERCHANTSAY | YES | 定义 | 在真值表 |
| 634 SM_COMBAT_BROADCAST | YES | 定义 | 在真值表 (IDA case 634 @ 0x46A74C 战斗消息广播) |
| 635 SM_COMBAT_END | YES | 定义 | 在真值表 (IDA case 635 @ 0x469B80 战斗姿态解除) |
| 636 SM_DLGMSG | YES | 定义 | 在真值表 |
| 637 SM_OBJECTSTRUCK_DELPHI | YES | 定义 | 在真值表 |
| 638 SM_MAGIC_FIRE | YES | 定义 | 在真值表 |
| 640 SM_MAGICLVEXP | YES | 定义 | 在真值表 |
| 644 SM_SKILLLEVELUP | YES | 定义 | 在真值表 |
| 648 SM_ABILITY | YES | 定义 | 在真值表 |
| 659 SM_INSTANCEHEALGAUGE | YES | 定义 | 在真值表 |
| 661 SM_MAPDESCRIPTION | YES | 定义 | 在真值表 |
| 662 SM_SENDNOTICE | YES | 定义 | 在真值表 |
| 667 SM_GROUPMEMBERLIST | YES | 定义 | 在真值表 |
| 670 SM_DEALMENU | YES | 定义 | 在真值表 |
| 671 SM_GUILDMEMBERLIST | YES | 定义 | 在真值表 |
| 672 SM_DEALADDITEM_OK | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 673 SM_DEALDELITEM_OK | YES | 定义 | 在真值表 |
| 673 SM_GUILDALLYINFO | YES | 定义 | 在真值表 |
| 674 SM_DEALCHGGOLD_OK | YES | 定义 | 在真值表 |
| 675 SM_DEALSUCCESS | YES | 定义 | 在真值表 |
| 681 SM_DEALCANCEL_DELPHI | YES | 定义 | 在真值表 |
| 682 SM_STORAGE_ITEMS | YES | 定义 | 在真值表 |
| 690 SM_GROUPMEMBERS | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 700 SM_OPENSTORAGEDLG | YES | 定义 | 在真值表 |
| 751 SM_USERNAME | YES | 定义 | 在真值表 |
| 752 SM_OPENGUILDDLGFAIL | YES | 定义 | 在真值表 |
| 753 SM_GUILDADDMEMBEROK | YES | 定义 | 在真值表 |
| 754 SM_GUILDADDMEMBERFAIL | YES | 定义 | 在真值表 |
| 755 SM_GUILDDELMEMBEROK | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 756 SM_GUILDDELMEMBERFAIL | YES | 定义 | 在真值表 |
| 757 SM_BUILDGUILDOK | YES | 定义 | 在真值表 |
| 758 SM_BUILDGUILDFAIL | YES | 定义 | 在真值表 |
| 759 SM_GUILDMAKEALLYOK | YES | 定义 | 在真值表 |
| 760 SM_GUILDMAKEALLYFAIL | YES | 定义 | 在真值表 |
| 761 SM_GUILDBREAKALLYOK | YES | 定义 | 在真值表 |
| 762 SM_GUILDBREAKALLYFAIL | YES | 定义 | 在真值表 |
| 763 SM_GUILDRANKUPDATEFAIL | YES | 定义 | 在真值表 |
| 764 SM_GUILDMESSAGE | YES | 定义 | 在真值表 |
| 765 SM_CRY | YES | 定义 | 在真值表 |
| 766 SM_ATTACKMODECHANGED | YES | 定义 | 在真值表 |
| 767 SM_DONATEOK | YES | 定义 | 在真值表 |
| 768 SM_DONATEFAIL | YES | 定义 | 在真值表 |
| 800 SM_SPACEMOVEHIDE | YES | 定义 | 在真值表 |
| 801 SM_SPACEMOVESHOW | YES | 定义 | 在真值表 |
| 802 SM_MAGICFIRE | YES | 定义 | 在真值表 |
| 803 SM_MAGICFIREFAIL | YES | 定义 | 在真值表 |
| 810 SM_TIMECHECKMSG | YES | 定义 | 在真值表 |
| 811 SM_ADJUSTBONUS | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 1104 SM_HEALTHBAR_COLOR_DELPHI | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 1500 SM_ITEMUPDATE | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 2014 SM_NEEDPASSWORD | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 2015 SM_PASSWORDSTATUS | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 2016 SM_GETREGINFO | NO | 定义 | 不在真值表（存疑/变体/错号） |
| 2017 SM_PASSWORD | NO | 定义 | 不在真值表（存疑/变体/错号） |