using System;
using System.Text;
using Xunit;
using Mir2Client.Game;
using Mir2Client.Net;

namespace Mir2Client.Tests;

/// <summary>
/// 不依赖 Rust 服务端的发包矩阵锁定测试：
///
/// - 行会 8 发包 opcode/参数位（CM_GUILD_* 对齐网关 1035/1037-1041/1044/1045）
/// - 交易 6 发包 opcode（CM_DEALTRY 1025 .. CM_ENDDEAL 1030，对齐网关 1025-1030）
/// - SAY/WHISPER 同号 3030 但 Extra 语义不同（官方复用设计）
/// - 野蛮冲撞走 CM_SPELL(3017, magicId=27)，挖矿走 CM_BIGHIT(3015)；
///   CM_MOTAEBO/CM_MAKEMINEITEM 无发送点，禁作真源（M2 联调前停用）。
/// 包结构：[1B序号]+[2B ident LE]+[4B recog]+[2B param]+[2B tag]+[extra]+[0x0A]
/// </summary>
public sealed class TestGuildTradeEncoderMatrix
{
    private static short Ident(byte[] pkt) => (short)(pkt[1] | (pkt[2] << 8));
    private static int Recog(byte[] pkt) => pkt[3] | (pkt[4] << 8) | (pkt[5] << 16) | (pkt[6] << 24);
    private static short Param(byte[] pkt) => (short)(pkt[7] | (pkt[8] << 8));

    private static void AssertWellformed(byte[] pkt, short ident)
    {
        Assert.True(pkt.Length >= 12);
        Assert.Equal(ident, Ident(pkt));
        Assert.Equal(0x0A, pkt[^1]);
        Assert.InRange(pkt[0], (byte)0x30, (byte)0x39);
    }

    [Fact]
    public void GuildSenders_CarryOfficialOpcodes()
    {
        // 对齐 Rust bridge 网关真号（1035/1037/1038ADDMEMBER/1039DEL/1040NOTICE/1041RANK/1044ALLY/1045BREAK）。
        AssertWellformed(PacketEncoder.GuildOpenPage(0), ProtocolConsts.CM_GUILD_OPEN_PAGE);
        AssertWellformed(PacketEncoder.GuildMemberList(1), ProtocolConsts.CM_GUILD_MEMBER_LIST);
        Assert.Equal((short)1, Param(PacketEncoder.GuildMemberList(1)));
        AssertWellformed(PacketEncoder.GuildNoticeUpdate("abc"), ProtocolConsts.CM_GUILD_NOTICE_UPDATE);
        // 宣战无网关 CM 入口：走 SAY @guildwar（1038=官方 ADDMEMBER 禁发）。
        var war = PacketEncoder.GuildApplyWar("昔日同盟");
        AssertWellformed(war, ProtocolConsts.CM_SAY);
        AssertWellformed(PacketEncoder.GuildAllyRequest("兄弟盟"), ProtocolConsts.CM_GUILD_ALLY_REQUEST);
        AssertWellformed(PacketEncoder.GuildBreakAlly("兄弟盟"), ProtocolConsts.CM_GUILD_BREAK_ALLY);
        AssertWellformed(PacketEncoder.GuildKickMember("卧底"), ProtocolConsts.CM_GUILD_KICK_MEMBER);
        var rank = PacketEncoder.GuildUpdateRank("张三", (byte)1);
        AssertWellformed(rank, ProtocolConsts.CM_GUILD_UPDATE_RANK);
        Assert.Equal((short)1, Recog(rank));
        Assert.Equal(ProtocolConsts.CM_GUILD_OPEN_PAGE, (short)1035);
        Assert.Equal(ProtocolConsts.CM_GUILD_MEMBER_LIST, (short)1037);
        Assert.Equal(ProtocolConsts.CM_GUILD_KICK_MEMBER, (short)1039);
        Assert.Equal(ProtocolConsts.CM_GUILD_NOTICE_UPDATE, (short)1040);
        Assert.Equal(ProtocolConsts.CM_GUILD_UPDATE_RANK, (short)1041);
        Assert.Equal(ProtocolConsts.CM_GUILD_ALLY_REQUEST, (short)1044);
        Assert.Equal(ProtocolConsts.CM_GUILD_BREAK_ALLY, (short)1045);
    }

    [Fact]
    public void TradeSenders_CarryOfficialOpcodes()
    {
        AssertWellformed(PacketEncoder.DealTry(7002), ProtocolConsts.CM_DEALTRY);
        Assert.Equal(7002, Recog(PacketEncoder.DealTry(7002)));
        AssertWellformed(PacketEncoder.DealAddItem(0, "裁决之杖"), ProtocolConsts.CM_DEALADDITEM);
        AssertWellformed(PacketEncoder.DealDelItem(1), ProtocolConsts.CM_DEALDELITEM);
        AssertWellformed(PacketEncoder.DealChangeGold(88888), ProtocolConsts.CM_DEALCHGGOLD);
        Assert.Equal(88888, Recog(PacketEncoder.DealChangeGold(88888)));
        AssertWellformed(PacketEncoder.DealConfirm(), ProtocolConsts.CM_DEALEND);
        AssertWellformed(PacketEncoder.DealCancel(), ProtocolConsts.CM_TRADECANCEL);
        // 对齐网关 1025-1030（旧 60-65 号网关无 arm，直接丢包）。
        Assert.Equal((short)1025, ProtocolConsts.CM_DEALTRY);
        Assert.Equal((short)1026, ProtocolConsts.CM_DEALADDITEM);
        Assert.Equal((short)1027, ProtocolConsts.CM_DEALDELITEM);
        Assert.Equal((short)1029, ProtocolConsts.CM_DEALCHGGOLD);
        Assert.Equal((short)1030, ProtocolConsts.CM_DEALEND);
        Assert.Equal((short)1028, ProtocolConsts.CM_TRADECANCEL);
    }

    [Fact]
    public void SayWhisper_Share3030_WithDifferentExtra()
    {
        var say = PacketEncoder.Say("hello");
        var whisper = PacketEncoder.Whisper("无敌法神", "hello");
        AssertWellformed(say, 3030);
        AssertWellformed(whisper, 3030);
        Assert.NotEqual(
            Convert.ToHexString(say[11..^1]),
            Convert.ToHexString(whisper[11..^1]));
    }

    [Fact]
    public void MoveAndCombatOpcodes_MatchBridge()
    {
        // 移动战斗系对齐 bridge：走 3011/跑 3013/转向 3010（param=dir&7，坐标服务端推导）；
        // 普攻 recog=目标 UID（bridge 823）；扔金 1016（旧 1002=开门）。
        Assert.Equal((short)3011, ProtocolConsts.CM_WALK);
        Assert.Equal((short)3013, ProtocolConsts.CM_RUN);
        Assert.Equal((short)3010, ProtocolConsts.CM_TURNDIRECTION);
        var hit = PacketEncoder.Hit(2, 9001);
        AssertWellformed(hit, ProtocolConsts.CM_HIT);
        Assert.Equal(9001, Recog(hit));
        Assert.Equal((short)2, Param(hit));
        AssertWellformed(PacketEncoder.DropGold(1000), ProtocolConsts.CM_DROPGOLD);
        Assert.Equal((short)1016, Ident(PacketEncoder.DropGold(1000)));
        AssertWellformed(PacketEncoder.PowerHit(2), ProtocolConsts.CM_POWERHIT);
        AssertWellformed(PacketEncoder.FireHit(9001, 2), ProtocolConsts.CM_FIREHIT);
    }

    [Fact]
    public void SpellPayload_MatchesBridge()
    {
        // 对齐 bridge 1159：recog=目标 UID，param=技能 id，extra="x\ty"（tag 位网关忽略）。
        // 技能 id 与官方表一致（1 火球 … 26 烈火 27 野蛮 31 魔法盾，见 Rust skill_ui.rs）。
        var spell = PacketEncoder.Spell(11, 332, 270, 9001);
        AssertWellformed(spell, ProtocolConsts.CM_SPELL);
        Assert.Equal(9001, Recog(spell));
        Assert.Equal((short)11, Param(spell));
    }

    [Fact]
    public void NpcSelect_MatchesBridge()
    {
        // NPC 对话对齐 bridge：点击 60|1010（recog/名）；选项 61|1011（recog=NPC id，param=序号）。
        var sel = PacketEncoder.NpcSelect(7001, "村长", 2);
        AssertWellformed(sel, ProtocolConsts.CM_NPCSELECT);
        Assert.Equal(7001, Recog(sel));
        Assert.Equal((short)2, Param(sel));
    }

    [Fact]
    public void LoginCharOpcodes_MatchBridge()
    {
        // 登录建角对齐 bridge：查角 119（旧 100 无 arm）；建角 121/122；删角 123/124；
        // 旧 519/524/525 与 Delphi 登录流号段撞车（524=维护中，525=STARTPLAY），已废。
        Assert.Equal((short)2, ProtocolConsts.CM_LOGIN);
        Assert.Equal((short)119, ProtocolConsts.CM_QUERYCHRINFO);
        Assert.Equal((short)101, ProtocolConsts.CM_NEWCHAR);
        Assert.Equal((short)102, ProtocolConsts.CM_DELCHAR);
        Assert.Equal((short)103, ProtocolConsts.CM_SELECTCHAR);
        Assert.Equal((short)104, ProtocolConsts.CM_SELECTSERVER);
        Assert.Equal((short)121, ProtocolConsts.SM_NEWCHR_SUCCESS);
        Assert.Equal((short)122, ProtocolConsts.SM_NEWCHR_FAIL);
        Assert.Equal((short)123, ProtocolConsts.SM_DELCHR_SUCCESS);
        Assert.Equal((short)124, ProtocolConsts.SM_DELCHR_FAIL);
        var q = PacketEncoder.QueryChr("test");
        AssertWellformed(q, ProtocolConsts.CM_QUERYCHRINFO);
        var nc = PacketEncoder.NewChar("英雄", 0, 0);
        AssertWellformed(nc, ProtocolConsts.CM_NEWCHAR);
    }

    [Fact]
    public void SpecialRings_MatchRust()
    {
        // 护身 1.5x 扣蓝；麻痹 Random(AP+5)==0；虹魔 db=dmg/100*suite≥2 回 Trunc。
        var p = SpecialRings.CalculateProtectRingDamage(true, 100, 200, 500);
        Assert.Equal(150, p.mpCost);
        Assert.Equal(0, p.remainingDamage);
        var par = SpecialRings.TriggerParalysisRing(true, 0, false, 0);
        Assert.True(par.isParalyzed);
        Assert.Equal(5, par.denom);
        var nopar = SpecialRings.TriggerParalysisRing(true, 0, false, 1);
        Assert.False(nopar.isParalyzed);
        double pool = 0;
        Assert.Equal(0, SpecialRings.AccumulateLifesteal(ref pool, 10, 10));
        Assert.Equal(2, SpecialRings.AccumulateLifesteal(ref pool, 10, 10));
    }

    [Fact]
    public void RulesPkUpgrade_MatchRust()
    {
        // 红名：杀人 +100（Rust add_pk）；衰减 50 秒/点（Rust decay sub_4BF520）。
        Assert.Equal(0, PkRuleEngine.DecayPkPoint(100, 5000));
        Assert.Equal(90, PkRuleEngine.DecayPkPoint(100, 500));
        Assert.Equal(2, PkRuleEngine.GetPkLevel(200));
        // 升武：纯度因子 ROUND(n+sum_top5/5)；9..15 档 roll<6 掉 1000 否则不变；≥18 档 1000/2000/4000。
        Assert.Equal(15, WeaponUpgradeEngine.CalcBlackIronDuraFactor(new[] { 10, 10, 10, 10, 10 }));
        Assert.Equal(-3000, WeaponUpgradeEngine.CalcWeaponDuraChange(8, 0, 0));
        Assert.Equal(-1000, WeaponUpgradeEngine.CalcWeaponDuraChange(10, 5, 0));
        Assert.Equal(0, WeaponUpgradeEngine.CalcWeaponDuraChange(10, 6, 0));
        Assert.Equal(1000, WeaponUpgradeEngine.CalcWeaponDuraChange(18, 0, 1));
        Assert.Equal(2000, WeaponUpgradeEngine.CalcWeaponDuraChange(18, 0, 5));
        Assert.Equal(4000, WeaponUpgradeEngine.CalcWeaponDuraChange(18, 0, 8));
    }

    [Fact]
    public void ExpAndWeightTables_MatchClassic()
    {
        // 经验表对齐 Rust g_dwOldNeedExps；负重上限经典二次式（服务端 Rust 为简化线性式，联调以经典为准）。
        Assert.Equal(100L, ExpTable.ExpForLevel(1));
        Assert.Equal(6000L, ExpTable.ExpForLevel(10));
        Assert.Equal(4000000L, ExpTable.ExpForLevel(35));
        Assert.Equal(0.5, ExpTable.ExpProgress(10, 3000));
        Assert.Equal(12 + (int)Math.Round(40 / 13.0 * 40), WeightConstraintEngine.CalcMaxHandWeight(40, 0));
        Assert.Equal(15 + (int)Math.Round(40 / 20.0 * 40), WeightConstraintEngine.CalcMaxWearWeight(40, 0));
        Assert.Equal(50 + (int)Math.Round(40 / 3.0 * 40), WeightConstraintEngine.CalcMaxBagWeight(40, 0));
    }

    [Fact]
    public void MageFormulas_MatchRust()
    {
        // 雷电：不死 1.5x、怪物 1.2x，命中减 MAC。
        Assert.Equal(150, SkillFormulas.ThunderboltBoostedPower(100, true, false));
        Assert.Equal(180, SkillFormulas.ThunderboltBoostedPower(100, true, true));
        Assert.Equal(120, SkillFormulas.ThunderboltDamage(150, 30));
        // 火墙：跳伤 min+rand%spread+sl*3-MAC（不死×1.2）；持续 Round(10/(train+1)*(sl+1))+def+MC/2。
        Assert.Equal((uint)(10 + 0 + 9 - 5), SkillFormulas.FireWallTickDamage(10, 20, 3, 5, false, 0));
        Assert.Equal((ulong)(10 + 0 + 12), SkillFormulas.FireWallDurationSeconds(3, 3, 0, 24));
        // 疾光：min+rand%spread+sl*4+12，×3/2 后减 MAC。
        Assert.Equal((uint)((10 + 0 + 12 + 12) * 3 / 2 - 5), SkillFormulas.LightningDamage(10, 20, 3, 5, 0));
        // 盾：时长 10+5*(sl+1)+roll；MAC 加成 (sl+1)*2+SC/5。
        Assert.Equal(30, SkillFormulas.MagicShieldDurationSec(3, 0));
        Assert.Equal(13, SkillFormulas.MagicShieldMacBonus(3, 25));
    }

    [Fact]
    public void PoisonFormulas_MatchRust()
    {
        // 施毒：持续 base=Round(def+(a1-a1/3)/(train+1)*(sl+1)+a1/3)+2*SC（Rust calc_poison_*）。
        // 满级(train3,sl3,def0)：绿 30+2SC，红 40+2SC；0 级绿：15+2SC。
        Assert.Equal(30 + 2 * 24, TaoistCombatEngine.CalculatePoisonDurationSec(TaoistCombatEngine.PoisonType.Green, 24, 3, 3, 0));
        Assert.Equal(40 + 2 * 24, TaoistCombatEngine.CalculatePoisonDurationSec(TaoistCombatEngine.PoisonType.Red, 24, 3, 3, 0));
        Assert.Equal(15, TaoistCombatEngine.CalculatePoisonDurationSec(TaoistCombatEngine.PoisonType.Green, 0, 0, 3, 0));
        // 绿毒跳伤：(sl+1)+(SC/10)+1。
        Assert.Equal(7, TaoistCombatEngine.CalculateGreenPoisonTickDamage(3, 24));
        Assert.Equal(2, TaoistCombatEngine.CalculateGreenPoisonTickDamage(0, 0));
        // 命中：RandInt(AP+7)<=6，0 抗必中。
        Assert.True(TaoistCombatEngine.CheckPoisonHit(0, new Random(1)));
    }

    [Fact]
    public void DashFormulas_MatchRust()
    {
        // 野蛮：阈值 diff+3*sl+6（roll20<阈值成功）；距离 Max(2,sl+1)；硬直 0.8s/格；撞墙自伤 5*(sl+1)+roll。
        // diff=5,sl=0 → 阈值 11：roll=10 成功，roll=11 失败。
        var ok = WarriorCombatEngine.ShoulderDash.ExecuteDash(35, 100, 100, 2, (true, 30, 101, 100), (x, y) => false, 0, 10);
        Assert.True(ok.TargetPushed);
        var no = WarriorCombatEngine.ShoulderDash.ExecuteDash(35, 100, 100, 2, (true, 30, 101, 100), (x, y) => false, 0, 11);
        Assert.False(no.Success);
        // sl=3 → 4 格：目标到 104，自身到 103。
        var far = WarriorCombatEngine.ShoulderDash.ExecuteDash(35, 100, 100, 2, (true, 30, 101, 100), (x, y) => false, 3, 0);
        Assert.Equal(104, far.TargetFinalX);
        Assert.Equal(103, far.SelfFinalX);
        // 撞墙：自伤 5*(0+1)+2=7，硬直 0.8。
        var wall = WarriorCombatEngine.ShoulderDash.ExecuteDash(35, 100, 100, 2, null, (x, y) => x == 101 && y == 100, 0, 0, 2);
        Assert.False(wall.Success);
        Assert.Equal(7, wall.SelfDamage);
        Assert.Equal(0.8f, wall.StunSec);
    }

    [Fact]
    public void CombatFormulas_MatchRust()
    {
        // 烈火：hitDouble=4*sl+4，加成 hitDouble*10%（Rust calc_fire_hit_bonus_by_level）。
        Assert.Equal(140, SkillFormulas.CalculateFireSwordDamage(100, 0));
        Assert.Equal(180, SkillFormulas.CalculateFireSwordDamage(100, 1));
        Assert.Equal(220, SkillFormulas.CalculateFireSwordDamage(100, 2));
        Assert.Equal(260, SkillFormulas.CalculateFireSwordDamage(100, 3));
        // 刺杀隔位：破防破盾但按 (skillLv+2)/(trainLv+2) 缩放（Rust calc_stabbing_sec_power）。
        var th0 = WarriorCombatEngine.StabbingTracker.CalculateDamage(
            100, WarriorCombatEngine.StabbingTracker.HitDistanceType.StabbingFar, true, 50, 3, 0);
        Assert.Equal(40, th0.finalDamage);
        Assert.True(th0.isArmorPiercing);
        var th3 = WarriorCombatEngine.StabbingTracker.CalculateDamage(
            100, WarriorCombatEngine.StabbingTracker.HitDistanceType.StabbingFar, true, 50, 3, 3);
        Assert.Equal(100, th3.finalDamage);
        // 常规：最小 0（Rust calc_hit_struck_damage）。
        var norm = WarriorCombatEngine.StabbingTracker.CalculateDamage(
            10, WarriorCombatEngine.StabbingTracker.HitDistanceType.MeleeClose, true, 50);
        Assert.Equal(0, norm.finalDamage);
    }

    [Fact]
    public void BagPayloads_MatchBridge()
    {
        // 背包系对齐 bridge：捡物 extra="x\ty"（872）；穿/丢 extra 带名（957/977）；
        // 脱下 param=装备位（服务端只认 equip_pos）；吃已有名重载。
        var pickup = PacketEncoder.Pickup(330, 270);
        AssertWellformed(pickup, ProtocolConsts.CM_PICKUP);
        var wear = PacketEncoder.WearItem(3, 1, "裁决之杖");
        AssertWellformed(wear, ProtocolConsts.CM_WEARITEM);
        Assert.Equal(3, Recog(wear));
        var drop = PacketEncoder.DropItem(5, "金创药", 1);
        AssertWellformed(drop, ProtocolConsts.CM_DROPITEM);
        var eat = PacketEncoder.EatItem(10, "金创药(中量)");
        AssertWellformed(eat, ProtocolConsts.CM_EATITEM);
    }

    [Fact]
    public void MotaeboAndMakeMine_DoNotUseDuplicateCodes()
    {
        var motaebo = PacketEncoder.Motaebo(2);
        AssertWellformed(motaebo, ProtocolConsts.CM_SPELL);
        var mine = PacketEncoder.MakeMine();
        AssertWellformed(mine, ProtocolConsts.CM_BIGHIT);
    }

    [Fact]
    public void ShopAndStorageOpcodes_AreStable()
    {
        // 对齐 bridge：45 买 / 46 卖 / 1023 修 / 48 特修 / 1024 查修价 / 1012 查卖价；买入 extra 带名。
        AssertWellformed(PacketEncoder.BuyFromShop("金创药", 3), ProtocolConsts.CM_USERBUYITEM);
        Assert.Equal((short)45, Ident(PacketEncoder.BuyFromShop("金创药", 3)));
        var buy = PacketEncoder.BuyFromShop("金创药", 3);
        Assert.Equal((short)3, Param(buy));
        AssertWellformed(PacketEncoder.SellToShop(0, 202), ProtocolConsts.CM_USERSELLITEM);
        Assert.Equal((short)46, Ident(PacketEncoder.SellToShop(0, 202)));
        AssertWellformed(PacketEncoder.RepairItem(0, 202), ProtocolConsts.CM_USERREPAIRITEM);
        Assert.Equal((short)1023, Ident(PacketEncoder.RepairItem(0, 202)));
        AssertWellformed(PacketEncoder.RepairSpecial(0, 202), ProtocolConsts.CM_REPAIRSPECIAL);
        AssertWellformed(PacketEncoder.QueryRepairCost(1), ProtocolConsts.CM_QUERYREPAIRCOST);
        Assert.Equal((short)1024, Ident(PacketEncoder.QueryRepairCost(1)));
        AssertWellformed(PacketEncoder.QuerySellPrice(1), ProtocolConsts.CM_QUERYSELLPRICE);
        Assert.Equal((short)1012, Ident(PacketEncoder.QuerySellPrice(1)));
        // 组队：1019 模式 / 1020 建组 / 1021 加人 / 1022 踢人（bridge 1038-1052；旧 70-74 号无 arm 已废）。
        AssertWellformed(PacketEncoder.GroupModeToggle(true), ProtocolConsts.CM_GROUPMODE);
        Assert.Equal((short)1019, Ident(PacketEncoder.GroupModeToggle(true)));
        AssertWellformed(PacketEncoder.GroupCreate(), ProtocolConsts.CM_CREATEGROUP);
        Assert.Equal((short)1020, Ident(PacketEncoder.GroupCreate()));
        AssertWellformed(PacketEncoder.GroupAddMember("无敌法神"), ProtocolConsts.CM_ADDGROUPMEMBER);
        Assert.Equal((short)1021, Ident(PacketEncoder.GroupAddMember("无敌法神")));
        AssertWellformed(PacketEncoder.GroupDelMember("无敌法神"), ProtocolConsts.CM_DELGROUPMEMBER);
        Assert.Equal((short)1022, Ident(PacketEncoder.GroupDelMember("无敌法神")));
        // 仓库：1031 存 / 1032 取（bridge 1012/1017；旧 1016/1017 无 arm 已废）。
        AssertWellformed(PacketEncoder.DepositStorage(1, "裁决之杖"), ProtocolConsts.CM_USERSTORAGEDEPOSIT);
        Assert.Equal((short)1031, Ident(PacketEncoder.DepositStorage(1, "裁决之杖")));
        AssertWellformed(PacketEncoder.WithdrawStorage(0), ProtocolConsts.CM_USERSTORAGEWITHDRAW);
        Assert.Equal((short)1032, Ident(PacketEncoder.WithdrawStorage(0)));
    }

    [Fact]
    public void StorageProtocolMatrix_DecodeAndClear_OfficialAlignment()
    {
        // 1. 验证 704 与 709 官方 1.5 IDA 物理真值常量对齐 (IDA case 704 / case 709 handle_sm_delete_item)
        Assert.Equal((short)704, ProtocolConsts.SM_USERSTORAGE_ITEMS);
        Assert.Equal((short)709, ProtocolConsts.SM_STORAGE_CLEARITEMS_DELPHI);
        Assert.Equal((short)701, ProtocolConsts.SM_SAVEITEM_OK);
        Assert.Equal((short)702, ProtocolConsts.SM_SAVEITEM_FULL);
        Assert.Equal((short)703, ProtocolConsts.SM_SAVEITEM_FAIL);
        Assert.Equal((short)705, ProtocolConsts.SM_TAKEBACKITEM_OK);
        Assert.Equal((short)706, ProtocolConsts.SM_TAKEBACKITEM_FAIL);
        Assert.Equal((short)707, ProtocolConsts.SM_TAKEBACKITEM_FULL);

        // 2. 模拟 Rust bridge 真实下发的 704 Extra 格式: "name\tdura\tmaxDura\titemIndex\n"
        string samplePayload = "屠龙\t35\t35\t100\r\n雷霆战甲(男)\t40\t40\t101\n圣战戒指\t5\t5\t102\n";
        var items = PacketDecoder.DecodeStorageItems(samplePayload);
        Assert.Equal(3, items.Count);

        Assert.Equal("屠龙", items[0].Name);
        Assert.Equal(35, items[0].Dura);
        Assert.Equal(35, items[0].MaxDura);
        Assert.Equal(100, items[0].ItemIndex);

        Assert.Equal("雷霆战甲(男)", items[1].Name);
        Assert.Equal(40, items[1].Dura);
        Assert.Equal(40, items[1].MaxDura);
        Assert.Equal(101, items[1].ItemIndex);

        Assert.Equal("圣战戒指", items[2].Name);
        Assert.Equal(5, items[2].Dura);
        Assert.Equal(5, items[2].MaxDura);
        Assert.Equal(102, items[2].ItemIndex);

        // 3. 边界鲁棒性：空串、仅空格、不完整字段容错
        Assert.Empty(PacketDecoder.DecodeStorageItems(""));
        Assert.Empty(PacketDecoder.DecodeStorageItems("   \r\n\t   "));
        var singleIncomplete = PacketDecoder.DecodeStorageItems("金创药(特)\n");
        Assert.Single(singleIncomplete);
        Assert.Equal("金创药(特)", singleIncomplete[0].Name);
        Assert.Equal(0, singleIncomplete[0].Dura);
        Assert.Equal(0, singleIncomplete[0].MaxDura);

        // 4. 脱机路由分发无崩溃安全断言 (HUD=null 时绝不抛出 NullReferenceException)
        var router = new ServerMessageRouter(null, null);
        var pkt704 = new ServerPacket(ProtocolConsts.SM_USERSTORAGE_ITEMS, 0, 0, 0, Encoding.UTF8.GetBytes(samplePayload));
        var pkt709 = new ServerPacket(ProtocolConsts.SM_STORAGE_CLEARITEMS_DELPHI, 0, 0, 0, Array.Empty<byte>());
        var pkt701 = new ServerPacket(ProtocolConsts.SM_SAVEITEM_OK, 5, 0, 0, Array.Empty<byte>());
        var pkt705 = new ServerPacket(ProtocolConsts.SM_TAKEBACKITEM_OK, 2, 0, 0, Array.Empty<byte>());

        router.OnPacket(pkt704);
        router.OnPacket(pkt709);
        router.OnPacket(pkt701);
        router.OnPacket(pkt705);
    }
}
