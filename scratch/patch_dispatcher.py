path = r'src/Mir2.Server/Game/PacketDispatcher.cs'
with open(path, 'r', encoding='utf-8') as f:
    content = f.read()

old_snippet = """    private unsafe void HandleBinaryCreateChar(ClientSession session, ReadOnlySpan<byte> payload)
    {
        if (payload.Length < sizeof(BinaryProtocol.CreateCharIntentPayload)) return;
        fixed (byte* p = payload)
        {
            var intent = *(BinaryProtocol.CreateCharIntentPayload*)p;
            string charName = intent.ReadCharName();
            if (string.IsNullOrEmpty(charName))
            {
                charName = $"{session.AccountName}_新角色";
            }
            session.EnqueuePacket(ProtocolConsts.SM_NEWID_SUCCESS, session.SessionId, 0, 0, 0, charName);
            HandleQueryCharInfo(session);
        }
    }

    private unsafe void HandleBinaryDeleteChar(ClientSession session, ReadOnlySpan<byte> payload)
    {
        if (payload.Length < sizeof(BinaryProtocol.DeleteCharIntentPayload)) return;
        fixed (byte* p = payload)
        {
            var intent = *(BinaryProtocol.DeleteCharIntentPayload*)p;
            session.EnqueuePacket(ProtocolConsts.SM_DELCHAR_DELPHI, session.SessionId, 0, 0, 0);
            HandleQueryCharInfo(session);
        }
    }"""

new_snippet = """    private unsafe void HandleBinaryCreateChar(ClientSession session, ReadOnlySpan<byte> payload)
    {
        if (payload.Length < sizeof(BinaryProtocol.CreateCharIntentPayload)) return;
        fixed (byte* p = payload)
        {
            var intent = *(BinaryProtocol.CreateCharIntentPayload*)p;
            string charName = intent.ReadCharName();
            if (string.IsNullOrEmpty(charName))
            {
                charName = $"{session.AccountName}_新角色";
            }
            HandleNewCharInternal(session, charName, intent.Job, intent.Sex);
        }
    }

    private unsafe void HandleBinaryDeleteChar(ClientSession session, ReadOnlySpan<byte> payload)
    {
        if (payload.Length < sizeof(BinaryProtocol.DeleteCharIntentPayload)) return;
        fixed (byte* p = payload)
        {
            var intent = *(BinaryProtocol.DeleteCharIntentPayload*)p;
            HandleDelCharInternal(session, intent.CharIndex.ToString());
        }
    }"""

norm_content = content.replace('\r\n', '\n')
norm_old = old_snippet.replace('\r\n', '\n')
norm_new = new_snippet.replace('\r\n', '\n')

if norm_old in norm_content:
    norm_content = norm_content.replace(norm_old, norm_new, 1)
    with open(path, 'w', encoding='utf-8', newline='') as f:
        f.write(norm_content.replace('\n', '\r\n'))
    print('PYTHON REPLACE BINARY CHAR SUCCESS')
else:
    print('PYTHON NOT FOUND')
