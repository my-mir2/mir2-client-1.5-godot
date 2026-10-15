import re
f=r'src/Mir2.Server/Game/PacketDispatcher.cs'
L=open(f,encoding='utf-8-sig',newline='').read().split('\r\n')
# 行 927-964 (1-based) 抽成方法
body=L[926:964]
assert '同步视野内的 NPC' in body[0] and body[-1].strip()=='}', body[-1]
L[926:964]=['        // 8~10. 同步视野内的 NPC / 怪物 / 其他玩家','        SendNearbyEntities(session, map);']
txt='\r\n'.join(L)
helper='    /// <summary>向会话下发当前位置视野内的 NPC(230)、存活怪物(220)与其他玩家(210)；进图与换图（传送门/回城/复活）后都必须调用。</summary>\r\n    private void SendNearbyEntities(ClientSession session, GameMap map)\r\n    {\r\n'+'\r\n'.join(x[0:] for x in body[1:])+'\r\n    }\r\n\r\n'
marker='    public void TriggerMapPassage('
txt=txt.replace(marker,helper+marker,1)
txt=txt.replace('''        BroadcastToNinePatch(session, ProtocolConsts.SM_ENTITYAPPEAR, session.SessionId, passage.ToX, passage.ToY, session.Direction, session.CharacterName, includeSelf: false);
    }''','''        BroadcastToNinePatch(session, ProtocolConsts.SM_ENTITYAPPEAR, session.SessionId, passage.ToX, passage.ToY, session.Direction, session.CharacterName, includeSelf: false);

        // 5. 新地图视野内的 NPC/怪物/玩家（此前缺失，导致换图后视野为空）
        SendNearbyEntities(session, newMap);
        session.Flush();
    }''',1)
open(f,'w',encoding='utf-8',newline='').write(txt)
