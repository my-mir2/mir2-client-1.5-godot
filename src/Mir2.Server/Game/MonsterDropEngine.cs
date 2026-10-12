using System;
using System.Collections.Generic;
using Mir2.Shared.Models;
using Mir2.Shared.Rules;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 死亡掉落权威引擎 (MonsterDropEngine)
/// 严格 1:1 对齐官方 M2Server 物理反编译代码:
/// - 死亡爆背包: TCreature.ScatterBagItems @ 0x4BB44C (白名 1/3, 红名 100%)
/// - 死亡爆穿戴: TCreature.DropUseItems @ 0x4BB6C8 (白名 1/30, 红名 1/15, 严格遍历 9 大槽位)
/// - MonItems 官方怪物掉落文件规则解析与概率投掷 (1/5 金币, 1/50 乌木剑, 1/2000 裁决之杖)
/// </summary>
public static class MonsterDropEngine
{
    public readonly record struct DroppedItem(
        int ItemUid,
        string ItemName,
        int OriginSlot,
        bool FromEquipment // true=穿戴装备, false=背包物品
    );

    public sealed class MonItemRule
    {
        public int Numerator { get; init; } = 1;
        public int Denominator { get; init; } = 1;
        public string ItemName { get; init; } = string.Empty;
        public int Count { get; init; } = 1;
        public bool IsGold => ItemName.Contains("金币");
    }

    /// <summary>
    /// 解析盛大官方经典 MonItems 掉落配置文件文本
    /// </summary>
    public static List<MonItemRule> ParseMonItemRules(string content)
    {
        var rules = new List<MonItemRule>();
        var lines = content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in lines)
        {
            var trimmed = line.Trim();
            if (string.IsNullOrWhiteSpace(trimmed) || trimmed.StartsWith(';') || trimmed.StartsWith("//")) continue;

            int spaceIdx = trimmed.IndexOf(' ');
            if (spaceIdx < 0) spaceIdx = trimmed.IndexOf('\t');
            if (spaceIdx <= 0) continue;

            string probPart = trimmed[..spaceIdx].Trim();
            string namePart = trimmed[(spaceIdx + 1)..].Trim();

            int slashIdx = probPart.IndexOf('/');
            if (slashIdx > 0 && int.TryParse(probPart[..slashIdx], out int num) && int.TryParse(probPart[(slashIdx + 1)..], out int den))
            {
                if (den > 0)
                {
                    rules.Add(new MonItemRule
                    {
                        Numerator = Math.Max(1, num),
                        Denominator = den,
                        ItemName = namePart,
                        Count = namePart.Contains("金币") ? (num * 100) : 1
                    });
                }
            }
        }
        return rules;
    }

    private static readonly Dictionary<string, List<MonItemRule>> _defaultDropRules = new(StringComparer.OrdinalIgnoreCase);

    static MonsterDropEngine()
    {
        // 预置盛大官方 1.5 经典怪物掉落表
        RegisterDropRules("半兽人", "1/3 金币\n1/10 乌木剑\n1/8 金创药(小量)");
        RegisterDropRules("半兽战士", "1/2 金币\n1/15 乌木剑\n1/8 强效金创药\n1/20 青铜斧");
        RegisterDropRules("红野猪", "1/1 金币\n1/5 强效金创药\n1/5 强效魔法药\n1/50 斩马刀\n1/100 降魔");
        RegisterDropRules("黑野猪", "1/1 金币\n1/5 强效金创药\n1/5 强效魔法药\n1/50 偃月\n1/100 降魔");
        RegisterDropRules("白野猪", "1/1 金币\n1/2 强效太阳水\n1/5 疗伤药\n1/30 魔杖\n1/30 炼狱\n1/30 银蛇\n1/150 裁决之杖\n1/150 骨玉权杖\n1/150 龙纹剑\n1/40 红宝石戒指\n1/40 铂金戒指\n1/40 幽灵手套");
        RegisterDropRules("毒蜘蛛", "1/3 金币\n1/5 灰色药粉(大)\n1/5 黄色药粉(大)\n1/10 蜘蛛毒腺");
        RegisterDropRules("角蝇", "1/10 金币\n1/20 蝙蝠粪便");
        RegisterDropRules("角蝇小蝙蝠", "1/5 金币\n1/10 回城卷");
        RegisterDropRules("契蛾", "1/5 金币\n1/10 回城卷\n1/15 强效魔法药");
        RegisterDropRules("洞蛆", "1/5 金币\n1/15 蛆卵");
        RegisterDropRules("祖玛雕像", "1/1 金币\n1/5 强效金创药\n1/40 龙之戒指\n1/40 三眼手镯\n1/40 骑士手镯");
        RegisterDropRules("祖玛卫士", "1/1 金币\n1/4 强效金创药\n1/4 强效魔法药\n1/30 龙之戒指\n1/30 红宝石戒指\n1/100 裁决之杖\n1/100 骨玉权杖\n1/100 龙纹剑");
        RegisterDropRules("祖玛弓箭手", "1/2 金币\n1/6 强效金创药\n1/50 离别钩\n1/60 修罗");
        RegisterDropRules("赤月恶魔", "1/1 金币\n1/1 强效太阳水\n1/2 疗伤药\n1/5 祝福油\n1/20 天尊头盔\n1/20 天尊项链\n1/20 天尊手镯\n1/20 天尊戒指\n1/30 圣战头盔\n1/30 圣战项链\n1/30 圣战手镯\n1/30 圣战戒指\n1/30 法神头盔\n1/30 法神项链\n1/30 法神手镯\n1/30 法神戒指\n1/200 屠龙\n1/100 嗜魂法杖");
    }

    public static void RegisterDropRules(string monsterName, string rulesText)
    {
        _defaultDropRules[monsterName] = ParseMonItemRules(rulesText);
    }

    public static IReadOnlyList<MonItemRule> GetMonsterDropRules(string monsterName)
    {
        if (_defaultDropRules.TryGetValue(monsterName, out var rules))
        {
            return rules;
        }
        return Array.Empty<MonItemRule>();
    }

    /// <summary>
    /// 根据怪物掉落规则与官方随机数精准计算生成掉落实体，并在怪物周边 1~2 格空地上散射
    /// </summary>
    public static List<Mir2.Server.Maps.GroundItemEntity> RollMonsterDrops(
        int monsterX, int monsterY, int killerSessionId, long nowMs,
        IEnumerable<MonItemRule> rules,
        OfficialClientRulesEngine.DelphiRandom rng,
        Func<int, int, bool>? isTileWalkable = null)
    {
        var result = new List<Mir2.Server.Maps.GroundItemEntity>();
        int uidCounter = 60000;

        // 8 方向散射偏移表
        ReadOnlySpan<(int dx, int dy)> scatterOffsets = stackalloc (int, int)[]
        {
            (0, 0),
            (0, -1), (1, -1), (1, 0), (1, 1), (0, 1), (-1, 1), (-1, 0), (-1, -1),
            (0, -2), (1, -2), (2, -2), (2, -1), (2, 0), (2, 1), (2, 2), (1, 2),
            (0, 2), (-1, 2), (-2, 2), (-2, 1), (-2, 0), (-2, -1), (-2, -2), (-1, -2)
        };

        int offsetIdx = 0;

        foreach (var rule in rules)
        {
            if (rng.Next(rule.Denominator) < rule.Numerator)
            {
                int dropX = monsterX;
                int dropY = monsterY;

                // 寻找可行走空位进行散射落物
                if (offsetIdx > 0 && isTileWalkable != null)
                {
                    while (offsetIdx < scatterOffsets.Length)
                    {
                        var (ox, oy) = scatterOffsets[offsetIdx];
                        int testX = monsterX + ox;
                        int testY = monsterY + oy;
                        if (isTileWalkable(testX, testY))
                        {
                            dropX = testX;
                            dropY = testY;
                            offsetIdx++;
                            break;
                        }
                        offsetIdx++;
                    }
                }
                else
                {
                    offsetIdx++;
                }

                result.Add(new Mir2.Server.Maps.GroundItemEntity
                {
                    Uid = uidCounter + rng.Next(10000),
                    Name = rule.ItemName,
                    X = dropX,
                    Y = dropY,
                    Count = rule.Count,
                    IsGold = rule.IsGold,
                    OwnerSessionId = killerSessionId, // 杀手 80 秒拾取权保护
                    DropTickMs = nowMs,
                    Weight = 1
                });
            }
        }
        return result;
    }

    /// <summary>
    /// 执行实体死亡掉落判定
    /// </summary>
    public static List<DroppedItem> ExecuteDeathDrop(PlayerState state, OfficialClientRulesEngine.DelphiRandom rng)
    {
        var dropped = new List<DroppedItem>();
        bool isRedName = state.IsRedName; // PK >= 200

        // 1. 爆背包物品 (0x4BB44C): 白名 1/3 几率，红名 100% 必爆
        for (int i = state.BagItems.Count - 1; i >= 0; i--)
        {
            var item = state.BagItems[i];
            if (isRedName || rng.Next(3) == 0)
            {
                dropped.Add(new DroppedItem(item.Uid, item.Name, i, FromEquipment: false));
                state.BagItems.RemoveAt(i);
            }
        }

        // 2. 爆身上 9 大装备槽位 (0x4BB6C8): 白名 1/30 几率，红名 1/15 几率 (暴增 100%)
        int equipDropRate = isRedName ? 15 : 30;

        for (int slot = 0; slot < 9; slot++)
        {
            var equip = state.Equipment[slot];
            if (equip == null) continue;

            // 盛大官方 1.5 权威规则：祈祷头盔死亡必定碎裂销毁 (不掉落到地表)
            if (slot == (int)OfficialEquipSlot.Helmet && equip.Name.Contains("祈祷头盔"))
            {
                state.TakeOffItem((OfficialEquipSlot)slot);
                continue;
            }

            // 掉落几率命中
            if (rng.Next(equipDropRate) == 0)
            {
                dropped.Add(new DroppedItem(equip.Uid, equip.Name, slot, FromEquipment: true));
                state.TakeOffItem((OfficialEquipSlot)slot);
            }
        }

        return dropped;
    }
}
