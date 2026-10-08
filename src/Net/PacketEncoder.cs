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

    // 转向：param=dir&7（bridge 814：只认 param，坐标由服务端会话维护，recog 置零）。
    public static byte[] TurnDirection(short x, short y, short dir) =>
        MakePacket(ProtocolConsts.CM_TURNDIRECTION, 0, dir, 0);

    // 普攻：recog=目标实体 UID（bridge 823：recog!=0 即 target_id；坐标由服务端会话推导，旧坐标打包已废）。
    public static byte[] Hit(short dir, short targetX, short targetY) =>
        Hit(dir, 0);

    public static byte[] Hit(short dir, int targetUid) =>
        MakePacket(ProtocolConsts.CM_HIT, targetUid, dir, 0);

    public static byte[] Spell(short magicId, short targetX, short targetY, int targetUid = 0) =>
        MakePacket(ProtocolConsts.CM_SPELL, targetUid, magicId, (short)((targetX << 8) | (targetY & 0xFF)), Encoding.UTF8.GetBytes($"{targetX}\t{targetY}"));

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

    // 捡物：extra="x\ty"（bridge 872：只读 extra，param/tag 忽略；无坐标则服务端按脚下拾取）。
    public static byte[] Pickup(short tx = -1, short ty = -1) =>
        (tx >= 0 && ty >= 0)
            ? MakePacket(ProtocolConsts.CM_PICKUP, 0, 0, 0, Encoding.UTF8.GetBytes($"{tx}\t{ty}"))
            : MakePacket(ProtocolConsts.CM_PICKUP, 0, 0, 0);

    public static byte[] ButchItem(int targetUid, short tx = 0, short ty = 0) =>
        MakePacket(ProtocolConsts.CM_BUTCHITEM, targetUid, tx, ty);

    // 丢弃：recog=背包槽位，extra=物品名（bridge 977；count 放 param，服务端当前忽略，保留）。
    public static byte[] DropItem(int uid, short count) =>
        DropItem(uid, string.Empty, count);

    public static byte[] DropItem(int slot, string itemName, short count = 1) =>
        MakePacket(ProtocolConsts.CM_DROPITEM, slot, count, 0, Encoding.UTF8.GetBytes(itemName));

    public static byte[] DropGold(int amount) =>
        MakePacket(ProtocolConsts.CM_DROPGOLD, amount);

    public static byte[] EatItem(int slotOrUid, string itemName = "") =>
        string.IsNullOrEmpty(itemName)
            ? MakePacket(ProtocolConsts.CM_EATITEM, slotOrUid)
            : MakePacket(ProtocolConsts.CM_EATITEM, slotOrUid, 0, 0, Encoding.UTF8.GetBytes(itemName));

    // 穿戴：recog=背包槽位，param=装备位，extra=物品名（bridge 957）。
    public static byte[] WearItem(short slot, short equipPos) =>
        WearItem(slot, equipPos, string.Empty);

    public static byte[] WearItem(short slot, short equipPos, string itemName) =>
        MakePacket(ProtocolConsts.CM_WEARITEM, slot, equipPos, 0, Encoding.UTF8.GetBytes(itemName));

    public static byte[] TakeOffItem(short slot) =>
        MakePacket(ProtocolConsts.CM_TAKEOFFITEM, 0, slot);

    // 商店买入：param=数量，extra=物品名（bridge 1092：param 取数、extra 取名；旧三参版名缺省为索引 URI，待商店面板传真名）。
    public static byte[] BuyFromShop(int npcUid, short count, short itemIndex) =>
        BuyFromShop(ShopIndexToName(npcUid, itemIndex), count);

    public static byte[] BuyFromShop(string itemName, int count = 1) =>
        MakePacket(ProtocolConsts.CM_USERBUYITEM, 0, (short)Math.Max(1, count), 0, Encoding.UTF8.GetBytes(itemName));

    // 商店出售：recog=槽位/物品标识，extra=物品名（bridge 1099）。
    public static byte[] SellToShop(int npcUid, int itemUid) =>
        MakePacket(ProtocolConsts.CM_USERSELLITEM, itemUid, 0, 0);

    // 普通修理：recog=槽位（bridge 1027：1023|47）。
    public static byte[] RepairItem(int npcUid, int itemUid) =>
        MakePacket(ProtocolConsts.CM_USERREPAIRITEM, itemUid, 0, 0);

    // 特殊修理：recog=槽位（bridge 1032：CM_REPAIRSPECIAL=48）。
    public static byte[] RepairSpecial(int npcUid, int itemUid) =>
        MakePacket(ProtocolConsts.CM_REPAIRSPECIAL, itemUid, 0, 0);

    private static string ShopIndexToName(int npcUid, int itemIndex) => $"shop://{npcUid}/{itemIndex}";

    // NPC 选项：recog=NPC id，param=选项序号，extra=NPC 名（bridge 903：61|1011 双栈）。
    public static byte[] NpcSelect(int npcUid, string npcName, int option) =>
        MakePacket(ProtocolConsts.CM_NPCSELECT, npcUid, (short)option, 0, Encoding.UTF8.GetBytes(npcName));

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

    // 仓库存入：recog=背包槽位，extra=物品名（bridge 1012；旧单参版名空，待仓库面板传真名）。
    public static byte[] DepositStorage(int slot) =>
        DepositStorage(slot, string.Empty);

    public static byte[] DepositStorage(int slot, string itemName) =>
        MakePacket(ProtocolConsts.CM_USERSTORAGEDEPOSIT, slot, 0, 0, Encoding.UTF8.GetBytes(itemName));

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

    public static byte[] ThrustingOnOff(bool enabled) =>
        MakePacket(ProtocolConsts.CM_THRUSTINGONOFF, enabled ? 1 : 0);

    public static byte[] HalfMoonOnOff(bool enabled) =>
        MakePacket(ProtocolConsts.CM_HALFMOONONOFF, enabled ? 1 : 0);

    public static byte[] ChangeAttackMode(short mode) =>
        MakePacket(ProtocolConsts.CM_CHANGEATTACKMODE, mode);

    public static byte[] UserGetDetailItem(string itemName) =>
        MakePacket(ProtocolConsts.CM_USERGETDETAILITEM, 0, 0, 0, Encoding.UTF8.GetBytes(itemName));

    public static byte[] AdjustBonus(short bonusType) =>
        MakePacket(ProtocolConsts.CM_ADJUST_BONUS, bonusType);

    public static byte[] QueryRepairCost(int slot) =>
        MakePacket(ProtocolConsts.CM_QUERYREPAIRCOST, slot);

    public static byte[] QuerySellPrice(int slot) =>
        MakePacket(ProtocolConsts.CM_QUERYSELLPRICE, slot);

    public static byte[] GuildOpenPage(short page) =>
        MakePacket(ProtocolConsts.CM_GUILD_OPEN_PAGE, 0, page);

    public static byte[] GuildMemberList(short page = 0) =>
        MakePacket(ProtocolConsts.CM_GUILD_MEMBER_LIST, 0, page);

    public static byte[] GuildNoticeUpdate(string notice) =>
        MakePacket(ProtocolConsts.CM_GUILD_NOTICE_UPDATE, 0, 0, 0, Encoding.UTF8.GetBytes(notice));

    // 宣战无网关 CM 入口（bridge 无映射，仅 GM @guildwar；1038=官方 ADDMEMBER 禁发）：
    // 走 SAY 聊天指令，GM 权限服务端受理，普通玩家服务端拒绝，无害。
    public static byte[] GuildApplyWar(string guildName) =>
        Say($"@guildwar {guildName}");

    public static byte[] GuildAllyRequest(string guildName) =>
        MakePacket(ProtocolConsts.CM_GUILD_ALLY_REQUEST, 0, 0, 0, Encoding.UTF8.GetBytes(guildName));

    public static byte[] GuildBreakAlly(string guildName) =>
        MakePacket(ProtocolConsts.CM_GUILD_BREAK_ALLY, 0, 0, 0, Encoding.UTF8.GetBytes(guildName));

    public static byte[] GuildKickMember(string memberName) =>
        MakePacket(ProtocolConsts.CM_GUILD_KICK_MEMBER, 0, 0, 0, Encoding.UTF8.GetBytes(memberName));

    // 职位任免：recog=职位 id(u8)，extra=成员名（bridge 1153-1156）。
    public static byte[] GuildUpdateRank(string memberName, byte rankId) =>
        MakePacket(ProtocolConsts.CM_GUILD_UPDATE_RANK, rankId, 0, 0, Encoding.UTF8.GetBytes(memberName));

    // 兼容旧签名的字符串封号（服务端不认，仅本地调试用，禁发线上）。
    public static byte[] GuildUpdateRank(string memberName, string rankName) =>
        GuildUpdateRank(memberName, RankNameToId(rankName));

    private static byte RankNameToId(string rankName) => rankName switch
    {
        "掌门人" => 1,
        "副掌门" => 2,
        "堂主" => 3,
        _ => 0,
    };
}
