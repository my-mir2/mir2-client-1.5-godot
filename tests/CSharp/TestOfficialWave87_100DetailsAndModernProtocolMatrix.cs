using System;
using System.IO;
using System.Text;
using Mir2.Shared.Models;
using Mir2.Shared.Network.HPSocket;
using Mir2.Shared.Protocols.Modern;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 盛大官方 1.5 细节 851 ~ 950 与现代 Direct-Blit 二进制协议全闭环测试矩阵
/// 涵盖：网络协议Direct-Blit分帧解包、纯血Dumb Client无私算权威仲裁、特戒与套装边界、经济与单机沙盒物理抹除
/// </summary>
public class TestOfficialWave87_100DetailsAndModernProtocolMatrix
{
    private readonly Random _rng = new(950);

    // =========================================================================
    // 细节 851 ~ 870: 现代强类型 Direct-Blit 协议与网络状态机
    // =========================================================================

    [Fact]
    public void Test851_ToggleGroupAllowed()
    {
        bool status = false;
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.ToggleGroupAllowed(ref status));
        Assert.True(status);
        Assert.False(OfficialSovereignPinnacle100DetailsEngine.ToggleGroupAllowed(ref status));
        Assert.False(status);
    }

    [Fact]
    public void Test852_ValidateGroupInvite()
    {
        var (ok1, _) = OfficialSovereignPinnacle100DetailsEngine.ValidateGroupInvite(targetAllowGroup: false, targetTeamId: 0);
        Assert.False(ok1);
        var (ok2, _) = OfficialSovereignPinnacle100DetailsEngine.ValidateGroupInvite(targetAllowGroup: true, targetTeamId: 101);
        Assert.False(ok2);
        var (ok3, _) = OfficialSovereignPinnacle100DetailsEngine.ValidateGroupInvite(targetAllowGroup: true, targetTeamId: 0);
        Assert.True(ok3);
    }

    [Fact]
    public void Test853_ValidateGuildJoin()
    {
        var (ok1, msg1) = OfficialSovereignPinnacle100DetailsEngine.ValidateGuildJoin(200);
        Assert.False(ok1);
        Assert.Contains("200人", msg1);
        var (ok2, _) = OfficialSovereignPinnacle100DetailsEngine.ValidateGuildJoin(199);
        Assert.True(ok2);
    }

    [Fact]
    public void Test854_ValidateGuildNoticePermission()
    {
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.ValidateGuildNoticePermission(1)); // 掌门
        Assert.False(OfficialSovereignPinnacle100DetailsEngine.ValidateGuildNoticePermission(2)); // 副掌门
        Assert.False(OfficialSovereignPinnacle100DetailsEngine.ValidateGuildNoticePermission(5)); // 普通成员
    }

    [Fact]
    public void Test855_TruncateGuildMemberTitle()
    {
        string raw = "沙巴克霸主无敌天下第一大将军"; // 超过 16 字节
        string truncated = OfficialSovereignPinnacle100DetailsEngine.TruncateGuildMemberTitle(raw);
        Assert.True(Encoding.UTF8.GetByteCount(truncated) <= 16);
    }

    [Fact]
    public void Test856_CheckStoragePasswordAttempts()
    {
        long now = 1000000;
        var (locked1, remain) = OfficialSovereignPinnacle100DetailsEngine.CheckStoragePasswordAttempts(3, now - 100000, now);
        Assert.True(locked1);
        Assert.Equal(500, remain); // 600 - 100 = 500秒

        var (locked2, _) = OfficialSovereignPinnacle100DetailsEngine.CheckStoragePasswordAttempts(2, now - 100000, now);
        Assert.False(locked2);
    }

    [Fact]
    public void Test857_ValidateStoragePasswordFormat()
    {
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.ValidateStoragePasswordFormat("123456"));
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.ValidateStoragePasswordFormat("abc123"));
        Assert.False(OfficialSovereignPinnacle100DetailsEngine.ValidateStoragePasswordFormat("12345")); // 长度不足
        Assert.False(OfficialSovereignPinnacle100DetailsEngine.ValidateStoragePasswordFormat("12345@")); // 非法符号
    }

    [Fact]
    public void Test858_IsInLocalChatRange()
    {
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.IsInLocalChatRange(300, 300, 308, 308));
        Assert.False(OfficialSovereignPinnacle100DetailsEngine.IsInLocalChatRange(300, 300, 310, 300));
    }

    [Fact]
    public void Test859_ValidateShoutChat()
    {
        var (ok1, err1) = OfficialSovereignPinnacle100DetailsEngine.ValidateShoutChat(5);
        Assert.False(ok1);
        Assert.Contains("7 级", err1);
        var (ok2, _) = OfficialSovereignPinnacle100DetailsEngine.ValidateShoutChat(7);
        Assert.True(ok2);
    }

    [Fact]
    public void Test860_ValidateWhisper()
    {
        var (ok1, err1) = OfficialSovereignPinnacle100DetailsEngine.ValidateWhisper(isOnline: false, isRejectWhisper: false);
        Assert.False(ok1);
        Assert.Contains("不在线", err1);
        var (ok2, err2) = OfficialSovereignPinnacle100DetailsEngine.ValidateWhisper(isOnline: true, isRejectWhisper: true);
        Assert.False(ok2);
        Assert.Contains("拒绝私聊", err2);
        var (ok3, _) = OfficialSovereignPinnacle100DetailsEngine.ValidateWhisper(isOnline: true, isRejectWhisper: false);
        Assert.True(ok3);
    }

    [Fact]
    public void Test861_864_EntityAppearMoveActionDirectBlit()
    {
        // 861: EntityAppearPayload
        var appear = BinaryProtocol.EntityAppearPayload.Create(1001, 1, 330, 270, 2, 10, 100, 100, "白野猪", 1, 2, 3, 0);
        Assert.Equal("白野猪", appear.ReadName());
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.ValidateEntityAppearance(appear.Appr, appear.Dress, appear.Weapon));

        // 862: ShouldPurgeEntityOnDisappear
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.ShouldPurgeEntityOnDisappear(1001));

        // 863: EntityMovePayload & IsTeleportMovement
        var move = new BinaryProtocol.EntityMovePayload { Uid = 1001, FromX = 330, FromY = 270, ToX = 331, ToY = 271, Direction = 2, IsRun = 0 };
        Span<byte> moveBytes = stackalloc byte[14];
        move.Write(moveBytes);
        var readMove = BinaryProtocol.EntityMovePayload.Read(moveBytes);
        Assert.Equal(1001, readMove.Uid);
        Assert.False(OfficialSovereignPinnacle100DetailsEngine.IsTeleportMovement(readMove.FromX, readMove.FromY, readMove.ToX, readMove.ToY));
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.IsTeleportMovement(330, 270, 335, 270));

        // 864: EntityActionPayload & GetActionSoundId
        var action = new BinaryProtocol.EntityActionPayload { Uid = 1001, ActionType = 0, Direction = 2, SpellId = 0, TargetUid = 2001 };
        Span<byte> actionBytes = stackalloc byte[12];
        action.Write(actionBytes);
        var readAction = BinaryProtocol.EntityActionPayload.Read(actionBytes);
        Assert.Equal(0, readAction.ActionType);
        Assert.Equal(120, OfficialSovereignPinnacle100DetailsEngine.GetActionSoundId(readAction.ActionType, defaultHitSound: 120));
        Assert.Equal(102, OfficialSovereignPinnacle100DetailsEngine.GetActionSoundId(2)); // 死亡音效
    }

    [Fact]
    public void Test865_870_InventoryEquipNameColorStatusMaskDirectBlit()
    {
        // 865: InventoryItemSyncPayload
        var item = BinaryProtocol.InventoryItemSyncPayload.Create(0, 0, 101, 8001, 1, 100, 100, "裁决之杖", dc: 30);
        Assert.Equal("裁决之杖", item.ReadItemName());
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.IsValidInventoryOp(item.OpType));

        // 866: EquipItemSyncPayload
        var equip = BinaryProtocol.EquipItemSyncPayload.Create(1, 101, 8001, 100, 100, "裁决之杖", dc: 30);
        Assert.Equal("裁决之杖", equip.ReadItemName());
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.IsValidEquipSlot(equip.EquipSlot));

        // 867: NameColorSyncPayload
        var colorPayload = new BinaryProtocol.NameColorSyncPayload { Uid = 5001, ColorCode = 2 };
        Span<byte> colBuf = stackalloc byte[6];
        colorPayload.Write(colBuf);
        var readCol = BinaryProtocol.NameColorSyncPayload.Read(colBuf);
        Assert.Equal(2, readCol.ColorCode);
        Assert.Equal(2, OfficialSovereignPinnacle100DetailsEngine.CalculateNameColor(150)); // 红名
        Assert.Equal(1, OfficialSovereignPinnacle100DetailsEngine.CalculateNameColor(50));  // 黄名
        Assert.Equal(0, OfficialSovereignPinnacle100DetailsEngine.CalculateNameColor(0));   // 白名

        // 868: StatusMaskSyncPayload
        var status = new BinaryProtocol.StatusMaskSyncPayload { Uid = 5001, StatusMask = 1 | 4, DurationMs = 5000 };
        Span<byte> stBuf = stackalloc byte[12];
        status.Write(stBuf);
        var readSt = BinaryProtocol.StatusMaskSyncPayload.Read(stBuf);
        Assert.Equal(5u, readSt.StatusMask);
        Assert.Equal(5u, OfficialSovereignPinnacle100DetailsEngine.ApplyStatusBit(1, 4));

        // 869: CanCompleteTrade
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.CanCompleteTrade(myFreeSlots: 5, myReceivingCount: 2, targetFreeSlots: 3, targetReceivingCount: 1));
        Assert.False(OfficialSovereignPinnacle100DetailsEngine.CanCompleteTrade(myFreeSlots: 1, myReceivingCount: 2, targetFreeSlots: 3, targetReceivingCount: 1));

        // 870: ValidatePacketHeader & HpPackStreamDecoder Direct-Blit 分帧测试
        var decoder = new HpPackStreamDecoder();
        int decodedPackets = 0;
        var header = new BinaryProtocol.PacketHeader { Magic = BinaryProtocol.DefaultMagic, MsgId = (ushort)BinaryProtocol.MsgType.IntentMove, PayloadLength = 6, Sequence = 1 };
        byte[] rawStream = new byte[8 + 6];
        header.Write(rawStream.AsSpan(0, 8));
        new BinaryProtocol.MoveIntentPayload { TargetX = 330, TargetY = 270, Direction = 2, IsRun = 1 }.Write(rawStream.AsSpan(8, 6));

        decoder.FeedData(rawStream, (in BinaryProtocol.PacketHeader hdr, ReadOnlySpan<byte> payload) =>
        {
            decodedPackets++;
            Assert.Equal((ushort)BinaryProtocol.MsgType.IntentMove, hdr.MsgId);
            Assert.Equal(6, payload.Length);
            Assert.True(OfficialSovereignPinnacle100DetailsEngine.ValidatePacketHeader(hdr));
        });
        Assert.Equal(1, decodedPackets);
    }

    // =========================================================================
    // 细节 871 ~ 890: 纯血 Dumb Client 架构与服务端权威计算
    // =========================================================================

    [Fact]
    public void Test871_875_AuthoritativeCombatAndResources()
    {
        // 871: CalcAuthoritativeMeleeDamage
        int dmg = OfficialSovereignPinnacle100DetailsEngine.CalcAuthoritativeMeleeDamage(attackerDc: 35, targetAc: 10, skillMultiplier: 1.5f);
        Assert.Equal((int)(35 * 1.5f) - 10, dmg);

        // 872: RollSpellEvade
        Assert.False(OfficialSovereignPinnacle100DetailsEngine.RollSpellEvade(0, _rng));

        // 873: DeductAuthoritativeMp
        var (ok1, newMp1) = OfficialSovereignPinnacle100DetailsEngine.DeductAuthoritativeMp(10, 15);
        Assert.False(ok1);
        Assert.Equal(10, newMp1);
        var (ok2, newMp2) = OfficialSovereignPinnacle100DetailsEngine.DeductAuthoritativeMp(20, 15);
        Assert.True(ok2);
        Assert.Equal(5, newMp2);

        // 874: RollMonsterDrop
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.RollMonsterDrop(1, _rng));

        // 875: CalcMeatQuality
        Assert.Equal(15, OfficialSovereignPinnacle100DetailsEngine.CalcMeatQuality(15, 0));
        Assert.Equal(13, OfficialSovereignPinnacle100DetailsEngine.CalcMeatQuality(15, 25)); // 25秒衰减2点
    }

    [Fact]
    public void Test876_880_PotionEquipRequirementAndRepair()
    {
        // 876: TickAuthoritativePotion
        var (hp1, pool1) = OfficialSovereignPinnacle100DetailsEngine.TickAuthoritativePotion(curHp: 80, maxHp: 100, pool: 12, tickHeal: 5);
        Assert.Equal(85, hp1);
        Assert.Equal(7, pool1);

        // 877: CheckAuthoritativeEquipRequirement
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.CheckAuthoritativeEquipRequirement(playerLevel: 30, playerStat: 0, ItemPropertyCompendiumEngine.EquipNeedType.Level, needValue: 30));
        Assert.False(OfficialSovereignPinnacle100DetailsEngine.CheckAuthoritativeEquipRequirement(playerLevel: 29, playerStat: 0, ItemPropertyCompendiumEngine.EquipNeedType.Level, needValue: 30));

        // 878: GetSkillTrainMax
        Assert.Equal(100, OfficialSovereignPinnacle100DetailsEngine.GetSkillTrainMax(0));
        Assert.Equal(200, OfficialSovereignPinnacle100DetailsEngine.GetSkillTrainMax(1));
        Assert.Equal(400, OfficialSovereignPinnacle100DetailsEngine.GetSkillTrainMax(2));

        // 879: CalcWeaponUpgradeDuraLoss
        var (lost, amount) = OfficialSovereignPinnacle100DetailsEngine.CalcWeaponUpgradeDuraLoss(totalPurity: 60, _rng);
        Assert.False(lost);
        Assert.Equal(0, amount);

        // 880: RepairEquipment
        var (specDura, specMax) = OfficialSovereignPinnacle100DetailsEngine.RepairEquipment(10, 20, isSpecialRepair: true, _rng);
        Assert.Equal(20, specDura);
        Assert.Equal(20, specMax);
    }

    [Fact]
    public void Test881_890_RulesAndWorldAuthority()
    {
        // 881: DecayPkPoints
        Assert.Equal(8, OfficialSovereignPinnacle100DetailsEngine.DecayPkPoints(10, 120)); // 120秒衰减2点

        // 882: CheckSabukPalaceConquest
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.CheckSabukPalaceConquest(defenderCount: 0, attackerCount: 5));
        Assert.False(OfficialSovereignPinnacle100DetailsEngine.CheckSabukPalaceConquest(defenderCount: 1, attackerCount: 5));

        // 883: CalcSabukTradeTax
        var (net, tax) = OfficialSovereignPinnacle100DetailsEngine.CalcSabukTradeTax(10000, 0.05f);
        Assert.Equal(9500, net);
        Assert.Equal(500, tax);

        // 884: GetUnknownHallDeerHp
        Assert.Equal(1200, OfficialSovereignPinnacle100DetailsEngine.GetUnknownHallDeerHp(isRealBoss: true));
        Assert.Equal(15, OfficialSovereignPinnacle100DetailsEngine.GetUnknownHallDeerHp(isRealBoss: false));

        // 885: HolyArmorDurationSeconds
        Assert.Equal(60, OfficialSovereignPinnacle100DetailsEngine.HolyArmorDurationSeconds);

        // 886: IsPrayerSetComplete
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.IsPrayerSetComplete(true, true, true, true, true));
        Assert.False(OfficialSovereignPinnacle100DetailsEngine.IsPrayerSetComplete(true, true, false, true, true));

        // 887: MemorySetRecallCooldownSec
        Assert.Equal(180, OfficialSovereignPinnacle100DetailsEngine.MemorySetRecallCooldownSec);

        // 888: IsOverweightBlocked
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.IsOverweightBlocked(65, 60));
        Assert.False(OfficialSovereignPinnacle100DetailsEngine.IsOverweightBlocked(55, 60));

        // 889: RollBlackIronOrePurity
        int purity = OfficialSovereignPinnacle100DetailsEngine.RollBlackIronOrePurity(_rng);
        Assert.InRange(purity, 1, 30);

        // 890: GuardInstantKillDamage
        Assert.Equal(9999, OfficialSovereignPinnacle100DetailsEngine.GuardInstantKillDamage);
    }

    // =========================================================================
    // 细节 891 ~ 920: 战斗、特戒与套装深水边界
    // =========================================================================

    [Fact]
    public void Test891_895_SpecialRingsAndSpells()
    {
        // 892: ApplyProtectRingDamage
        var (hp1, mp1) = OfficialSovereignPinnacle100DetailsEngine.ApplyProtectRingDamage(currentHp: 100, currentMp: 50, damage: 30);
        Assert.Equal(100, hp1);
        Assert.Equal(20, mp1);
        var (hp2, mp2) = OfficialSovereignPinnacle100DetailsEngine.ApplyProtectRingDamage(currentHp: 100, currentMp: 20, damage: 50);
        Assert.Equal(70, hp2); // 溢出 30 扣 HP
        Assert.Equal(0, mp2);

        // 893: TriggerReviveRing
        var (revived1, newDura1) = OfficialSovereignPinnacle100DetailsEngine.TriggerReviveRing(5);
        Assert.True(revived1);
        Assert.Equal(4, newDura1);
        var (revived2, newDura2) = OfficialSovereignPinnacle100DetailsEngine.TriggerReviveRing(0);
        Assert.False(revived2);
        Assert.Equal(0, newDura2);

        // 894: ShouldBreakInvisibilityOnAttack
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.ShouldBreakInvisibilityOnAttack());

        // 895: DefenseRingHealMpCost
        Assert.Equal(3, OfficialSovereignPinnacle100DetailsEngine.DefenseRingHealMpCost);
    }

    [Fact]
    public void Test896_905_WarriorAndMageCombatBoundaries()
    {
        // 896: FlameRingFireballMpCost
        Assert.Equal(5, OfficialSovereignPinnacle100DetailsEngine.FlameRingFireballMpCost);

        // 897: IsTeamRecallCommand
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.IsTeamRecallCommand("@天地合一"));
        Assert.False(OfficialSovereignPinnacle100DetailsEngine.IsTeamRecallCommand("@集结"));

        // 899: GhostSuitAgilityBonus
        Assert.Equal(2, OfficialSovereignPinnacle100DetailsEngine.GhostSuitAgilityBonus);

        // 900: CalcRainbowMagicLifesteal
        Assert.Equal(10, OfficialSovereignPinnacle100DetailsEngine.CalcRainbowMagicLifesteal(100, 0.10f));

        // 901: MagicBloodSuitHpBonus
        Assert.Equal(150, OfficialSovereignPinnacle100DetailsEngine.MagicBloodSuitHpBonus(3));

        // 902: GaleWindAttackIntervalMs
        Assert.Equal(550, OfficialSovereignPinnacle100DetailsEngine.GaleWindAttackIntervalMs);

        // 903: WildSlamHitWallPenalty
        var (stunned, rebound) = OfficialSovereignPinnacle100DetailsEngine.WildSlamHitWallPenalty;
        Assert.True(stunned);
        Assert.Equal(10, rebound);

        // 904: CrescentSlashMpCost
        Assert.Equal(3, OfficialSovereignPinnacle100DetailsEngine.CrescentSlashMpCost);

        // 905: FireHitExpireMs
        Assert.Equal(10000, OfficialSovereignPinnacle100DetailsEngine.FireHitExpireMs);
    }

    [Fact]
    public void Test906_920_SpellsAndTaoistBoundaries()
    {
        // 906: ResolveOverlappingFireWallDamage
        Assert.Equal(45, OfficialSovereignPinnacle100DetailsEngine.ResolveOverlappingFireWallDamage(new[] { 30, 45, 20 }));

        // 907: CalcHellThunderDamage
        Assert.Equal(150, OfficialSovereignPinnacle100DetailsEngine.CalcHellThunderDamage(100, isUndead: true));
        Assert.Equal(100, OfficialSovereignPinnacle100DetailsEngine.CalcHellThunderDamage(100, isUndead: false));

        // 908: RollHolyCurseKill
        Assert.False(OfficialSovereignPinnacle100DetailsEngine.RollHolyCurseKill(mageLevel: 30, monLevel: 35, _rng)); // 越级必失败

        // 909: IceStormIgnoresMagicAvoid
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.IceStormIgnoresMagicAvoid);

        // 910: AbsorbShieldDamage
        var (broken1, overflow1) = OfficialSovereignPinnacle100DetailsEngine.AbsorbShieldDamage(100, 40);
        Assert.False(broken1);
        Assert.Equal(0, overflow1);
        var (broken2, overflow2) = OfficialSovereignPinnacle100DetailsEngine.AbsorbShieldDamage(50, 70);
        Assert.True(broken2);
        Assert.Equal(20, overflow2);

        // 911: HolyArmorAcBonus & GhostShieldMacBonus
        Assert.Equal(3, OfficialSovereignPinnacle100DetailsEngine.HolyArmorAcBonus);
        Assert.Equal(3, OfficialSovereignPinnacle100DetailsEngine.GhostShieldMacBonus);

        // 913: ShouldBreakTrappedCurseOnStruck
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.ShouldBreakTrappedCurseOnStruck());

        // 914: HasTrueSight
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.HasTrueSight("月魔蜘蛛"));
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.HasTrueSight("锲蛾"));
        Assert.False(OfficialSovereignPinnacle100DetailsEngine.HasTrueSight("稻草人"));

        // 915: GroupInvisibilityBreakOnAttack
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.GroupInvisibilityBreakOnAttack);

        // 916: SoulFireTalismanCost
        Assert.Equal(1, OfficialSovereignPinnacle100DetailsEngine.SoulFireTalismanCost);

        // 917: RedPoisonVulnerabilityMultiplier
        Assert.Equal(1.20f, OfficialSovereignPinnacle100DetailsEngine.RedPoisonVulnerabilityMultiplier);

        // 918-920: Skeleton & Shinsu max level and Revelation duration
        Assert.Equal(7, OfficialSovereignPinnacle100DetailsEngine.SkeletonMaxLevel);
        Assert.Equal(7, OfficialSovereignPinnacle100DetailsEngine.ShinsuMaxLevel);
        Assert.Equal(30, OfficialSovereignPinnacle100DetailsEngine.RevelationDurationSec);
    }

    // =========================================================================
    // 细节 921 ~ 950: 经济、极端边界与单机沙盒物理抹除
    // =========================================================================

    [Fact]
    public void Test921_935_EconomyAndSiegeBoundaries()
    {
        // 921: CanAffordShopItem
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.CanAffordShopItem(1000, 500));
        Assert.False(OfficialSovereignPinnacle100DetailsEngine.CanAffordShopItem(400, 500));

        // 922: PlayerMaxGoldLimit
        Assert.Equal(50_000_000, OfficialSovereignPinnacle100DetailsEngine.PlayerMaxGoldLimit);

        // 924: SafeUpgradeTotalPurity
        Assert.Equal(60, OfficialSovereignPinnacle100DetailsEngine.SafeUpgradeTotalPurity);

        // 925: WeaponUpgradeMaxBonus
        Assert.Equal(7, OfficialSovereignPinnacle100DetailsEngine.WeaponUpgradeMaxBonus);

        // 927: IsLuck9Set
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.IsLuck9Set(9));
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.IsLuck9Set(10));
        Assert.False(OfficialSovereignPinnacle100DetailsEngine.IsLuck9Set(8));

        // 928: SunWaterInstantHeal
        var (hp, mp) = OfficialSovereignPinnacle100DetailsEngine.SunWaterInstantHeal;
        Assert.Equal(50, hp);
        Assert.Equal(50, mp);

        // 929: CanUnbundlePotions
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.CanUnbundlePotions(6));
        Assert.False(OfficialSovereignPinnacle100DetailsEngine.CanUnbundlePotions(5));

        // 930: ResolveGroundItemConflict
        var (nx, ny) = OfficialSovereignPinnacle100DetailsEngine.ResolveGroundItemConflict(300, 300, (x, y) => x == 300 && y == 300);
        Assert.True(nx != 300 || ny != 300); // 必须弹开到相邻格

        // 931: GroundItemExpirySeconds
        Assert.Equal(180, OfficialSovereignPinnacle100DetailsEngine.GroundItemExpirySeconds);

        // 932: RollRedNameDropCount
        int drops = OfficialSovereignPinnacle100DetailsEngine.RollRedNameDropCount(_rng);
        Assert.InRange(drops, 1, 3);

        // 933: IsSelfDefense
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.IsSelfDefense(targetIsYellow: true));

        // 934: IsSabukSiegeTime
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.IsSabukSiegeTime(20));
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.IsSabukSiegeTime(22));
        Assert.False(OfficialSovereignPinnacle100DetailsEngine.IsSabukSiegeTime(23));
        Assert.False(OfficialSovereignPinnacle100DetailsEngine.IsSabukSiegeTime(19));

        // 935: SabukDailyWithdrawLimit
        Assert.Equal(10_000_000, OfficialSovereignPinnacle100DetailsEngine.SabukDailyWithdrawLimit);
    }

    [Fact]
    public void Test936_950_BossMechanicsVisionAndSandboxReleaseIsolation()
    {
        // 936: UnknownHallShinsuHp
        Assert.Equal(2000, OfficialSovereignPinnacle100DetailsEngine.UnknownHallShinsuHp(isReal: true));
        Assert.Equal(100, OfficialSovereignPinnacle100DetailsEngine.UnknownHallShinsuHp(isReal: false));

        // 937: IsWomaLeaderEnraged
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.IsWomaLeaderEnraged(300, 1000));
        Assert.False(OfficialSovereignPinnacle100DetailsEngine.IsWomaLeaderEnraged(350, 1000));

        // 938: ZumaLeaderWakeupDistance
        Assert.Equal(3, OfficialSovereignPinnacle100DetailsEngine.ZumaLeaderWakeupDistance);

        // 939: EvilCentipedeAttackIntervalMs
        Assert.Equal(2000, OfficialSovereignPinnacle100DetailsEngine.EvilCentipedeAttackIntervalMs);

        // 940: IsDragonLordImmuneToLightning
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.IsDragonLordImmuneToLightning);

        // 941: DefaultSafeZoneCoord
        Assert.Equal((330, 270), OfficialSovereignPinnacle100DetailsEngine.DefaultSafeZoneCoord);

        // 942: CanInspectPlayer
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.CanInspectPlayer(1001));

        // 943: FilterMinimapRadarEntity
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.FilterMinimapRadarEntity(isDead: false));
        Assert.False(OfficialSovereignPinnacle100DetailsEngine.FilterMinimapRadarEntity(isDead: true));

        // 944: GetVisionRadius
        Assert.Equal(15, OfficialSovereignPinnacle100DetailsEngine.GetVisionRadius(isNight: false, hasTorch: false));
        Assert.Equal(3, OfficialSovereignPinnacle100DetailsEngine.GetVisionRadius(isNight: true, hasTorch: false));
        Assert.Equal(7, OfficialSovereignPinnacle100DetailsEngine.GetVisionRadius(isNight: true, hasTorch: true));

        // 947: GuildWarDurationMinutes
        Assert.Equal(180, OfficialSovereignPinnacle100DetailsEngine.GuildWarDurationMinutes);

        // 948: MoonSpiderAttackHasParalyze
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.MoonSpiderAttackHasParalyze);

        // 949: HuWeiRespawnIntervalSeconds
        Assert.Equal(14400, OfficialSovereignPinnacle100DetailsEngine.HuWeiRespawnIntervalSeconds);

        // 950: 客户端 Release 模式下绝对封杀单机沙盒后门验证
        bool removedInRelease = OfficialSovereignPinnacle100DetailsEngine.IsSandboxPhysicallyRemovedInRelease();
        Assert.True(removedInRelease, "在 Release 生产发布版本下，MIR2_SANDBOX_DEV 未定义，单机沙盒必须被物理抹除！");
        Assert.True(OfficialSovereignPinnacle100DetailsEngine.IsSandboxPhysicallyRemovedInRelease());
    }
}
