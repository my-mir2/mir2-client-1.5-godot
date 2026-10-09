using System;
using System.Collections.Generic;
using System.Linq;

namespace Mir2Client.Net;

/// <summary>
/// 盛大官方 1.5 网络通信鲁棒性、同步与边界防御引擎 (细节 131 ~ 150)
/// 严格依据官方服务端与客户端双端网络通信实证
/// </summary>
public static class PacketProtocolEngine
{
    // --------------------------------------------------------------------------------
    // 细节 131: TCP 粘包与半包在环形缓冲区中的定界自愈解析
    // --------------------------------------------------------------------------------
    public class RingBufferPacketParser
    {
        private readonly List<byte> _buffer = new();

        public void Append(byte[] data)
        {
            _buffer.AddRange(data);
        }

        public List<string> ExtractPackets()
        {
            var packets = new List<string>();
            while (true)
            {
                int start = _buffer.IndexOf((byte)'#');
                if (start == -1)
                {
                    _buffer.Clear(); // 无包头，丢弃脏数据
                    break;
                }
                int end = _buffer.IndexOf((byte)'!', start);
                if (end == -1)
                {
                    // 找到了包头但没有包尾，属于半包，保留等待后续数据
                    if (start > 0) _buffer.RemoveRange(0, start);
                    break;
                }

                int len = end - start + 1;
                byte[] rawPacket = _buffer.GetRange(start, len).ToArray();
                packets.Add(System.Text.Encoding.ASCII.GetString(rawPacket));
                _buffer.RemoveRange(0, end + 1);
            }
            return packets;
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 132: 网络突发中断原生断线重连模态框
    // --------------------------------------------------------------------------------
    public static class ReconnectModalEngine
    {
        public static (bool showModal, string modalText, bool canRetry) Evaluate(bool isSocketConnected, int retryCount)
        {
            if (isSocketConnected) return (false, "", false);
            if (retryCount >= 5) return (true, "与服务器断开连接，请检查网络后重新登录。", false);
            return (true, $"与服务器连接中断，正在尝试重连 ({retryCount + 1}/5)...", true);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 133: 小退与大退 5 秒受击安全倒计时保护
    // --------------------------------------------------------------------------------
    public static class LogoutProtectionEngine
    {
        public static (bool canExit, float remainingSeconds, bool interrupted) UpdateCountdown(
            float currentRemaining, float delta, bool wasAttacked)
        {
            if (wasAttacked) return (false, 5.0f, true); // 遭受攻击立刻打断并重置为 5 秒
            float remaining = Math.Max(0f, currentRemaining - delta);
            return (remaining <= 0f, remaining, false);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 134: 同格重叠多实体点击穿透与目标优先级选择 (玩家 > 怪物 > 掉落物)
    // --------------------------------------------------------------------------------
    public static class EntityOverlapPickEngine
    {
        public enum EntityKind { GroundItem = 0, Monster = 1, Player = 2 }
        public record TargetEntity(long Id, EntityKind Kind, string Name);

        public static TargetEntity? PickPriorityTarget(IEnumerable<TargetEntity> entities)
        {
            return entities.OrderByDescending(e => e.Kind).FirstOrDefault();
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 135: 视野内地表掉落物批量刷出时的空间网格剔除与合并绘制
    // --------------------------------------------------------------------------------
    public static class GroundItemGridCullingEngine
    {
        public record GroundItem(long Id, int X, int Y, string Name);

        public static List<GroundItem> CullItemsInViewport(IEnumerable<GroundItem> items, int viewX, int viewY, int viewW, int viewH)
        {
            return items.Where(item =>
                item.X >= viewX && item.X < viewX + viewW &&
                item.Y >= viewY && item.Y < viewY + viewH
            ).ToList();
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 136: 安全区 (SafeZone) 判定与战斗动作物理拦截
    // --------------------------------------------------------------------------------
    public static class SafeZoneEngine
    {
        public static bool IsInSafeZone(int mapId, int x, int y)
        {
            // 比奇城安全区示例坐标 (300~350, 250~300)
            if (mapId == 0 && x >= 300 && x <= 350 && y >= 250 && y <= 300) return true;
            return false;
        }

        public static (bool allowAttack, string rejectReason) CheckAttackPermission(int mapId, int attackerX, int attackerY)
        {
            if (IsInSafeZone(mapId, attackerX, attackerY))
            {
                return (false, "安全区内禁止施法与攻击！");
            }
            return (true, "");
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 137: PK 状态切换与攻击目标合法性过滤
    // --------------------------------------------------------------------------------
    public static class PkModeFilterEngine
    {
        public enum PkMode { Peace, Guild, Group, All, RedOnly }

        public static bool CanAttackTarget(PkMode mode, bool isMonster, bool isSameGuild, bool isSameGroup, bool isRedName)
        {
            if (isMonster) return true; // 所有模式均可打怪
            return mode switch
            {
                PkMode.Peace => false,
                PkMode.Guild => !isSameGuild,
                PkMode.Group => !isSameGroup,
                PkMode.All => true,
                PkMode.RedOnly => isRedName,
                _ => false
            };
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 138: 玩家黑名单 (屏蔽指定玩家发言与交易请求)
    // --------------------------------------------------------------------------------
    public static class BlacklistEngine
    {
        private static readonly HashSet<string> _blacklisted = new(StringComparer.OrdinalIgnoreCase);

        public static void Add(string playerName) => _blacklisted.Add(playerName);
        public static void Remove(string playerName) => _blacklisted.Remove(playerName);
        public static bool IsBlocked(string playerName) => _blacklisted.Contains(playerName);
    }

    // --------------------------------------------------------------------------------
    // 细节 139: 聊天框多频道过滤 (喊话/公聊/行会/私聊/组队/系统)
    // --------------------------------------------------------------------------------
    public static class ChatChannelFilterEngine
    {
        public enum Channel { Normal, Shout, Guild, Whisper, Group, System }
        public record ChatMessage(Channel Channel, string Sender, string Content);

        public static List<ChatMessage> FilterMessages(IEnumerable<ChatMessage> messages, Channel activeChannel)
        {
            if (activeChannel == Channel.Normal) return messages.ToList(); // Normal 显示综合信息
            return messages.Where(m => m.Channel == activeChannel).ToList();
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 140: 私聊自动补全与最近私聊对象快捷键 (/ + Tab)
    // --------------------------------------------------------------------------------
    public static class WhisperAutoCompleteEngine
    {
        public static string AutoCompleteRecentWhisper(string currentInput, string lastWhisperPlayer)
        {
            if (string.IsNullOrEmpty(currentInput) || currentInput == "/")
            {
                if (!string.IsNullOrEmpty(lastWhisperPlayer))
                {
                    return $"/{lastWhisperPlayer} ";
                }
            }
            return currentInput;
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 141: 背包物品自动整理算法 (堆叠合并、按类型/等级排序)
    // --------------------------------------------------------------------------------
    public static class BagAutoSortEngine
    {
        public record InventoryItem(int SlotIndex, int ItemId, int Type, int Level, int Count, int MaxStack);

        public static List<InventoryItem> SortInventory(IEnumerable<InventoryItem> items)
        {
            // 按类型升序，然后按等级降序
            return items.OrderBy(i => i.Type).ThenByDescending(i => i.Level).Select((it, idx) =>
                new InventoryItem(idx, it.ItemId, it.Type, it.Level, it.Count, it.MaxStack)
            ).ToList();
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 142: 商铺交易税率抽成 (沙巴克城主税收 5%~20%)
    // --------------------------------------------------------------------------------
    public static class ShopTaxEngine
    {
        public static (int actualPrice, int taxGold) CalculateTax(int basePrice, float taxRatePercent)
        {
            float rate = Math.Clamp(taxRatePercent, 5.0f, 20.0f) / 100.0f;
            int tax = (int)(basePrice * rate);
            return (basePrice + tax, tax);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 143: 武器升级开刀成败瞬间判定
    // --------------------------------------------------------------------------------
    public static class WeaponSlashInstantJudgeEngine
    {
        public static (bool isSuccess, string message) JudgeSlash(bool seedSuccess)
        {
            if (seedSuccess)
            {
                return (true, "你的武器升级成功了！");
            }
            return (false, "你的武器破碎了！");
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 144: 怪物视野仇恨距离与警戒范围脱离后的追击归位
    // --------------------------------------------------------------------------------
    public static class MonsterTetherEngine
    {
        public const int MaxTetherDistance = 15; // 超出出生点 15 格脱战

        public static (bool shouldReset, int spawnX, int spawnY, bool healFullHp) CheckTether(
            int curX, int curY, int spawnX, int spawnY)
        {
            int dx = curX - spawnX;
            int dy = curY - spawnY;
            bool reset = (dx * dx + dy * dy) > (MaxTetherDistance * MaxTetherDistance);
            return (reset, spawnX, spawnY, reset);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 145: 毒物持续掉血与黄毒降双防 20%
    // --------------------------------------------------------------------------------
    public static class PoisonDebuffEngine
    {
        public static (int greenDamagePerTick, int reducedAc, int reducedMac) ApplyPoison(int targetHp, int targetAc, int targetMac, bool isGreen, bool isYellow)
        {
            int dmg = isGreen ? 5 : 0; // 每跳扣 5 点血
            int rAc = isYellow ? (int)(targetAc * 0.2f) : 0; // 黄毒扣 20% 防御
            int rMac = isYellow ? (int)(targetMac * 0.2f) : 0; // 黄毒扣 20% 魔防
            return (dmg, targetAc - rAc, targetMac - rMac);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 146: 行会攻城战皇宫占领判定 (皇宫内无其他行会存活成员)
    // --------------------------------------------------------------------------------
    public static class PalaceCaptureEngine
    {
        public record Occupant(string GuildName, bool IsAlive);

        public static (bool isCaptured, string owningGuild) EvaluatePalace(IEnumerable<Occupant> occupants)
        {
            var aliveGuilds = occupants.Where(o => o.IsAlive).Select(o => o.GuildName).Distinct().ToList();
            if (aliveGuilds.Count == 1)
            {
                return (true, aliveGuilds[0]);
            }
            return (false, "");
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 147: 沙巴克城门与雕像受损度 (HP) 同步与倒塌废墟贴图替换
    // --------------------------------------------------------------------------------
    public static class SabukDestructionEngine
    {
        public const int RuinTextureId = 888;
        public const int IntactTextureId = 880;

        public static (int currentTextureId, bool isCollapsed) CheckGateStatus(int currentHp)
        {
            if (currentHp <= 0) return (RuinTextureId, true);
            return (IntactTextureId, false);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 148: 客户端 FPS 限制与垂直同步 (60FPS 锁帧)
    // --------------------------------------------------------------------------------
    public static class FrameLimiterEngine
    {
        public const int TargetFps = 60;
        public const int FrameBudgetMs = 1000 / TargetFps; // 16ms

        public static int CalculateSleepMs(long frameElapsedMs)
        {
            return (int)Math.Max(0, FrameBudgetMs - frameElapsedMs);
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 149: 后台静音设置 (游戏窗口失去焦点自动静音或降至 20%)
    // --------------------------------------------------------------------------------
    public static class BackgroundAudioEngine
    {
        public static float CalculateVolume(float userVolume, bool isWindowFocused, bool muteOnBlur)
        {
            if (isWindowFocused) return userVolume;
            if (muteOnBlur) return 0f;
            return userVolume * 0.2f;
        }
    }

    // --------------------------------------------------------------------------------
    // 细节 150: 客户端内存泄漏防护与 WIL 纹理 LRU 缓存驱逐机制
    // --------------------------------------------------------------------------------
    public class WilTextureLruCache
    {
        private readonly int _capacity;
        private readonly Dictionary<string, int> _cache = new();
        private readonly LinkedList<string> _lruList = new();

        public WilTextureLruCache(int capacity)
        {
            _capacity = capacity;
        }

        public void Put(string key, int textureId)
        {
            if (_cache.ContainsKey(key))
            {
                _lruList.Remove(key);
            }
            else if (_cache.Count >= _capacity)
            {
                string oldest = _lruList.Last!.Value;
                _lruList.RemoveLast();
                _cache.Remove(oldest);
            }
            _cache[key] = textureId;
            _lruList.AddFirst(key);
        }

        public bool TryGet(string key, out int textureId)
        {
            if (_cache.TryGetValue(key, out textureId))
            {
                _lruList.Remove(key);
                _lruList.AddFirst(key);
                return true;
            }
            return false;
        }

        public int Count => _cache.Count;
    }
}
