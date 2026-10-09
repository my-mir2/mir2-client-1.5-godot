using System;
using Mir2.Server.Network;
using Mir2.Shared.Models;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 沙巴克城堡税收分成与金库公款体系 (SabukTreasuryEngine)
/// 严格 1:1 对齐官方 M2Server 经济规则:
/// 1. 全服商业税收自动抽成: 各大主城 NPC 商店买卖流水按 20% 自动流入沙巴克金库 (GoldTreasury)
/// 2. 沙巴克城主独享金库支取特权 (受单人金币上限保护)
/// 3. 沙巴克战备军费支出: 修缮倒塌城门或破损城墙自动扣减城堡金库
/// </summary>
public sealed class SabukTreasuryEngine
{
    public const double CommercialTaxRate = 0.20; // 官方 20% 商业税率
    public long GoldTreasury { get; private set; } = 1_000_000; // 初始金库储备 100 万金币
    public string OwnerGuildName { get; set; } = "沙巴克行会";
    public string LordCharacterName { get; set; } = "沙巴克城主";

    private readonly object _treasuryLock = new();

    /// <summary>
    /// NPC 商店产生交易消费时，自动截留 20% 商业税收汇入沙巴克金库
    /// </summary>
    public int CollectShopTransactionTax(int playerSpentGold)
    {
        if (playerSpentGold <= 0) return 0;
        int tax = (int)(playerSpentGold * CommercialTaxRate);
        if (tax > 0)
        {
            lock (_treasuryLock)
            {
                GoldTreasury += tax;
            }
        }
        return tax;
    }

    /// <summary>
    /// 沙巴克城主从金库支取金币
    /// </summary>
    public (bool success, int withdrawn, string message) WithdrawCastleGold(ClientSession session, int amount)
    {
        if (session.State == null) return (false, 0, "玩家状态异常。");
        if (!session.CharacterName.Equals(LordCharacterName, StringComparison.OrdinalIgnoreCase))
        {
            return (false, 0, "只有沙巴克城主本人方可进入金库支取税收公款！");
        }

        if (amount <= 0) return (false, 0, "请输入有效的支取金额。");

        lock (_treasuryLock)
        {
            if (GoldTreasury < amount)
            {
                return (false, 0, $"金库公款储备不足！当前仅存: {GoldTreasury} 金币。");
            }

            int currentGold = session.State.Gold;
            int maxCanReceive = PlayerState.MaxGoldLimit - currentGold;
            if (maxCanReceive <= 0)
            {
                return (false, 0, "你身上的金币已达到单人携带极限 (10,000,000)，无法继续支取！");
            }

            int actualWithdraw = Math.Min(amount, maxCanReceive);
            GoldTreasury -= actualWithdraw;
            session.State.Gold += actualWithdraw;
            session.State.RecalculateAuthoritativeWeights(session.State.Gold);

            return (true, actualWithdraw, $"成功从沙巴克金库支取了 {actualWithdraw} 金币！金库剩余: {GoldTreasury}。");
        }
    }

    /// <summary>
    /// 修缮城门与城墙自动扣除金库公款
    /// </summary>
    public (bool success, string message) DeductSiegeRepairCost(int repairCost)
    {
        lock (_treasuryLock)
        {
            if (GoldTreasury < repairCost)
            {
                return (false, $"沙巴克金库公款不足！修缮需要 {repairCost} 金币，当前金库仅存 {GoldTreasury} 金币。");
            }

            GoldTreasury -= repairCost;
            return (true, $"修缮资金扣款成功！扣除军费 {repairCost} 金币，金库剩余 {GoldTreasury}。");
        }
    }
}
