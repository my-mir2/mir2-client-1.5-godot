using System;
using System.Text;

namespace Mir2Client.Net;

/// <summary>
/// 盛大官方 1.5 原版 CM 数据包序列化器 (IDA sub_458D3C + sub_45AF90 铁证)
/// 包结构:
///   [1B 序号 '0'..'9'] + [2B ident] + [4B recog] + [2B param] + [2B tag] + [nB +60 编码 Extra] + [1B '\n']
/// </summary>
public static class PacketEncoder
{
    private static int _seq = 0;

    public static byte[] MakePacket(short cmCode, int recog = 0, short param = 0, short tag = 0, ReadOnlySpan<byte> extraRaw = default)
    {
        byte[] encodedExtra = extraRaw.IsEmpty ? Array.Empty<byte>() : PacketCodec.EncodePlus60(extraRaw);

        int totalLen = 1 + 10 + encodedExtra.Length + 1; // 序号(1) + 头部(10) + extra + '\n'(1)
        byte[] outBuf = new byte[totalLen];

        // 1. 包序号字符 ('0' + seq)
        outBuf[0] = (byte)(0x30 + _seq);
        _seq = (_seq + 1) % 10;

        // 2. 头部 10 字节 (LE 序)
        outBuf[1] = (byte)(cmCode & 0xFF);
        outBuf[2] = (byte)((cmCode >> 8) & 0xFF);

        outBuf[3] = (byte)(recog & 0xFF);
        outBuf[4] = (byte)((recog >> 8) & 0xFF);
        outBuf[5] = (byte)((recog >> 16) & 0xFF);
        outBuf[6] = (byte)((recog >> 24) & 0xFF);

        outBuf[7] = (byte)(param & 0xFF);
        outBuf[8] = (byte)((param >> 8) & 0xFF);

        outBuf[9] = (byte)(tag & 0xFF);
        outBuf[10] = (byte)((tag >> 8) & 0xFF);

        // 3. 附加数据
        if (encodedExtra.Length > 0)
        {
            Array.Copy(encodedExtra, 0, outBuf, 11, encodedExtra.Length);
        }

        // 4. 终止符 '\n'
        outBuf[totalLen - 1] = 0x0A;

        return outBuf;
    }

    // ============ 高频常用 CM 协议打包快捷方法 ============
    public static byte[] LoginNoticeOk() => MakePacket(ProtocolConsts.CM_LOGINNOTICEOK);

    public static byte[] Login(string user, string pass)
    {
        byte[] payload = Encoding.UTF8.GetBytes($"{user}\t{pass}");
        return MakePacket(ProtocolConsts.CM_LOGIN, 0, 0, 0, payload);
    }

    public static byte[] GameLogin(string account, int sessionId, int certKey)
    {
        byte[] payload = Encoding.UTF8.GetBytes(account);
        return MakePacket(ProtocolConsts.CM_GAMELOGIN, sessionId, (short)certKey, 0, payload);
    }

    public static byte[] Walk(short x, short y, short dir) =>
        MakePacket(ProtocolConsts.CM_WALK, (x << 16) | (y & 0xFFFF), dir, 0);

    public static byte[] Run(short x, short y, short dir) =>
        MakePacket(ProtocolConsts.CM_RUN, (x << 16) | (y & 0xFFFF), dir, 0);

    public static byte[] TurnDirection(short x, short y, short dir) =>
        MakePacket(ProtocolConsts.CM_TURNDIRECTION, (x << 16) | (y & 0xFFFF), dir, 0);

    public static byte[] Hit(short dir, short targetX, short targetY) =>
        MakePacket(ProtocolConsts.CM_HIT, (targetX << 16) | (targetY & 0xFFFF), dir, 0);

    public static byte[] Spell(short magicId, short targetX, short targetY, int targetUid = 0) =>
        MakePacket(ProtocolConsts.CM_SPELL, targetUid, magicId, (short)((targetX << 8) | (targetY & 0xFF)));

    public static byte[] Say(string text) =>
        MakePacket(ProtocolConsts.CM_SAY, 0, 0, 0, Encoding.UTF8.GetBytes(text));

    public static byte[] Whisper(string target, string msg) =>
        MakePacket(ProtocolConsts.CM_WHISPER, 0, 0, 0, Encoding.UTF8.GetBytes($"{target}\t{msg}"));

    public static byte[] SelectServer(int index) =>
        MakePacket(ProtocolConsts.CM_SELECTSERVER, index);

    public static byte[] QueryChr(string account = "") =>
        MakePacket(ProtocolConsts.CM_QUERYCHRINFO, 0, 0, 0, Encoding.UTF8.GetBytes(account));

    public static byte[] NewChar(string name, int job, int gender) =>
        MakePacket(ProtocolConsts.CM_NEWCHAR, 0, 0, 0, Encoding.UTF8.GetBytes($"{name}\t{job}\t{gender}"));

    public static byte[] DelChar(int index) =>
        MakePacket(ProtocolConsts.CM_DELCHAR, index);

    public static byte[] SelectChar(int index) =>
        MakePacket(ProtocolConsts.CM_SELECTCHAR, index);

    public static byte[] PowerHit(short dir, int targetUid = 0) =>
        MakePacket(ProtocolConsts.CM_POWERHIT, targetUid, dir);

    public static byte[] LongHit(short dir) =>
        MakePacket(ProtocolConsts.CM_LONGHIT, 0, dir);

    public static byte[] WideHit(short dir) =>
        MakePacket(ProtocolConsts.CM_WIDEHIT, 0, dir);

    public static byte[] FireHit(int targetUid, short dir) =>
        MakePacket(ProtocolConsts.CM_FIREHIT, targetUid, dir);

    public static byte[] Motaebo(short dir) =>
        MakePacket(ProtocolConsts.CM_SPELL, 0, 27, dir);

    public static byte[] HeavyHit(short dir, int targetUid = 0) =>
        MakePacket(ProtocolConsts.CM_HEAVYHIT, targetUid, dir);

    public static byte[] BigHit(short dir, int targetUid = 0) =>
        MakePacket(ProtocolConsts.CM_BIGHIT, targetUid, dir);

    public static byte[] SpreadHit(short dir) =>
        MakePacket(ProtocolConsts.CM_SPREADHIT, 0, dir);

    public static byte[] CrsHit(short dir) =>
        MakePacket(ProtocolConsts.CM_CRSHIT, 0, dir);

    public static byte[] TwoHandHit(short dir) =>
        MakePacket(ProtocolConsts.CM_TWOHANDHIT, 0, dir);

    public static byte[] CloackHit(short dir) =>
        MakePacket(ProtocolConsts.CM_CLOACKHIT, 0, dir);

    public static byte[] MakeMine(short dir = 0, int resourceUid = 0) =>
        MakePacket(ProtocolConsts.CM_BIGHIT, resourceUid, dir);

    public static byte[] Pickup(short tx = -1, short ty = -1) =>
        (tx >= 0 && ty >= 0) ? MakePacket(ProtocolConsts.CM_PICKUP, 0, tx, ty) : MakePacket(ProtocolConsts.CM_PICKUP, 0, 0, 0);

    public static byte[] ButchItem(int targetUid, short tx = 0, short ty = 0) =>
        MakePacket(ProtocolConsts.CM_BUTCHITEM, targetUid, tx, ty);

    public static byte[] DropItem(int uid, short count) =>
        MakePacket(ProtocolConsts.CM_DROPITEM, uid, count);

    public static byte[] DropGold(int amount) =>
        MakePacket(ProtocolConsts.CM_DROPGOLD, amount);

    public static byte[] EatItem(int slotOrUid, string itemName = "") =>
        string.IsNullOrEmpty(itemName)
            ? MakePacket(ProtocolConsts.CM_EATITEM, slotOrUid)
            : MakePacket(ProtocolConsts.CM_EATITEM, slotOrUid, 0, 0, Encoding.UTF8.GetBytes(itemName));

    public static byte[] WearItem(short slot, short equipPos) =>
        MakePacket(ProtocolConsts.CM_WEARITEM, slot, equipPos);

    public static byte[] TakeOffItem(short slot) =>
        MakePacket(ProtocolConsts.CM_TAKEOFFITEM, 0, slot);

    public static byte[] BuyFromShop(int npcUid, short count, short itemIndex) =>
        MakePacket(ProtocolConsts.CM_USERBUYITEM, npcUid, count, itemIndex);

    public static byte[] SellToShop(int npcUid, int itemUid) =>
        MakePacket(ProtocolConsts.CM_USERSELLITEM, itemUid, (short)npcUid);

    public static byte[] RepairItem(int npcUid, int itemUid) =>
        MakePacket(ProtocolConsts.CM_USERREPAIRITEM, itemUid, (short)npcUid);

    public static byte[] RepairSpecial(int npcUid, int itemUid) =>
        MakePacket(ProtocolConsts.CM_REPAIRSPECIAL, npcUid, (short)itemUid);

    public static byte[] ClickNpc(string npcName) =>
        MakePacket(ProtocolConsts.CM_MERCHANTHOMEPAGE, 0, 0, 0, Encoding.UTF8.GetBytes(npcName));

    public static byte[] NpcSelect(int npcUid, short optionIndex, string actionTag = "") =>
        string.IsNullOrEmpty(actionTag)
            ? MakePacket(ProtocolConsts.CM_NPCSELECT, npcUid, optionIndex)
            : MakePacket(ProtocolConsts.CM_NPCSELECT, npcUid, optionIndex, 0, Encoding.UTF8.GetBytes(actionTag));

    public static byte[] SoftClose() =>
        MakePacket(ProtocolConsts.CM_SOFTCLOSE);

    public static byte[] QueryUsername(int uid) =>
        MakePacket(ProtocolConsts.CM_QUERYUSERNAME, uid);

    public static byte[] SitDown() =>
        MakePacket(ProtocolConsts.CM_SITDOWN, 0);

    public static byte[] QueryVersion() =>
        MakePacket(ProtocolConsts.CM_QUERYVERSION, 0);

    public static byte[] Heartbeat() =>
        MakePacket(ProtocolConsts.CM_HEARTBEAT, 0);

    public static byte[] DepositStorage(int slot) =>
        MakePacket(ProtocolConsts.CM_USERSTORAGEDEPOSIT, slot);

    public static byte[] WithdrawStorage(int slot) =>
        MakePacket(ProtocolConsts.CM_USERSTORAGEWITHDRAW, slot);

    public static byte[] DealTry(int targetUid = 0) =>
        MakePacket(ProtocolConsts.CM_DEALTRY, targetUid);

    public static byte[] DealAddItem(short slot, string itemName = "") =>
        string.IsNullOrEmpty(itemName)
            ? MakePacket(ProtocolConsts.CM_DEALADDITEM, slot)
            : MakePacket(ProtocolConsts.CM_DEALADDITEM, slot, 0, 0, Encoding.UTF8.GetBytes(itemName));

    public static byte[] DealDelItem(short slot) =>
        MakePacket(ProtocolConsts.CM_DEALDELITEM, slot);

    public static byte[] DealChangeGold(int goldVal) =>
        MakePacket(ProtocolConsts.CM_DEALCHGGOLD, goldVal);

    public static byte[] DealConfirm() =>
        MakePacket(ProtocolConsts.CM_DEALEND, 0);

    public static byte[] DealCancel() =>
        MakePacket(ProtocolConsts.CM_TRADECANCEL, 0);

    public static byte[] GroupCreate() =>
        MakePacket(ProtocolConsts.CM_CREATEGROUP, 0);

    public static byte[] GroupAddMember(string targetName) =>
        MakePacket(ProtocolConsts.CM_ADDGROUPMEMBER, 0, 0, 0, Encoding.UTF8.GetBytes(targetName));

    public static byte[] GroupDelMember(string targetName) =>
        MakePacket(ProtocolConsts.CM_DELGROUPMEMBER, 0, 0, 0, Encoding.UTF8.GetBytes(targetName));

    public static byte[] GroupModeToggle(bool allow) =>
        MakePacket(ProtocolConsts.CM_GROUPMODE, allow ? 1 : 0);

    public static byte[] AdjustBonus(short bonusType) =>
        MakePacket(ProtocolConsts.CM_ADJUST_BONUS, bonusType);

    public static byte[] QueryRepairCost(int slot) =>
        MakePacket(ProtocolConsts.CM_QUERYREPAIRCOST, slot);

    public static byte[] QuerySellPrice(int slot) =>
        MakePacket(ProtocolConsts.CM_QUERYSELLPRICE, slot);
}
