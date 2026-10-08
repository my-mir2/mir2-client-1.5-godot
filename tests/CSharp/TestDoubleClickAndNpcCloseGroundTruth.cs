using System;
using Xunit;
using Mir2Client.UI;
using Mir2Client.Net;
using Mir2Client.Audio;

namespace Mir2Client.Tests;

/// <summary>
/// 盛大官方 1.5 原版双击快捷交互与 NPC 对话关闭/计费到期物理真值核销测试套件
/// 依据 IDA Pro MCP 逆向实证:
/// - sub_48E33C: TFrmDlg_DItemGridDblClick (背包格子双击吃药与穿戴)
/// - sub_48C188: TFrmDlg_DBelt1DblClick (快捷栏双击使用)
/// - sub_463BFC: TFrmMain_DXDraw1DblClick / sub_4886C0 (神龛双击直接进入游戏)
/// - sub_467920 case 644 @ 0x46D65C: handle_sm_merchant_dlg_close (服务端强制关闭 NPC 窗口)
/// - sub_467920 case 529 @ 0x46B3C8: SM_BILLING_NOTICE (计费到期通知)
/// </summary>
public class TestDoubleClickAndNpcCloseGroundTruth
{
    [Fact]
    public void TestSlotButton_DoubleClick_And_ConsumableLogic()
    {
        // 1. 验证物品类型分类算法 (IsConsumableItem)
        Assert.True(HudController.IsConsumableItem("强效金创药"));
        Assert.True(HudController.IsConsumableItem("超级魔法药"));
        Assert.True(HudController.IsConsumableItem("太阳水"));
        Assert.True(HudController.IsConsumableItem("随机传送卷"));
        Assert.True(HudController.IsConsumableItem("回城卷"));
        Assert.True(HudController.IsConsumableItem("祝福油"));
        Assert.True(HudController.IsConsumableItem("鹿肉"));

        Assert.False(HudController.IsConsumableItem("裁决之杖"));
        Assert.False(HudController.IsConsumableItem("战神盔甲"));
        Assert.False(HudController.IsConsumableItem("绿色项链"));
        Assert.False(HudController.IsConsumableItem("骑士手镯"));
        Assert.False(HudController.IsConsumableItem("力量戒指"));
    }

    [Fact]
    public void TestEquipSlotMapping_OfficialDelphiGroundTruth()
    {
        // 2. 验证装备槽位自动映射 (GetBestEquipSlotForItem)
        // 0: 衣服, 1: 武器, 2: 勋章/饰品, 3: 项链, 4: 头盔, 5: 手镯, 7: 戒指
        Assert.Equal(1, HudController.GetBestEquipSlotForItem("井中月"));
        Assert.Equal(1, HudController.GetBestEquipSlotForItem("裁决之杖"));
        Assert.Equal(1, HudController.GetBestEquipSlotForItem("乌木剑"));
        Assert.Equal(1, HudController.GetBestEquipSlotForItem("骨玉权杖"));

        Assert.Equal(0, HudController.GetBestEquipSlotForItem("重盔甲"));
        Assert.Equal(0, HudController.GetBestEquipSlotForItem("魔法长袍"));
        Assert.Equal(0, HudController.GetBestEquipSlotForItem("布衣(男)"));

        Assert.Equal(4, HudController.GetBestEquipSlotForItem("黑铁头盔"));
        Assert.Equal(4, HudController.GetBestEquipSlotForItem("道士头巾"));

        Assert.Equal(3, HudController.GetBestEquipSlotForItem("幽灵项链"));
        Assert.Equal(3, HudController.GetBestEquipSlotForItem("记忆项链"));

        Assert.Equal(5, HudController.GetBestEquipSlotForItem("死神手套"));
        Assert.Equal(5, HudController.GetBestEquipSlotForItem("幽灵手镯"));

        Assert.Equal(7, HudController.GetBestEquipSlotForItem("力量戒指"));
        Assert.Equal(7, HudController.GetBestEquipSlotForItem("生铁戒指"));

        Assert.Equal(2, HudController.GetBestEquipSlotForItem("荣誉勋章43号"));
        Assert.Equal(-1, HudController.GetBestEquipSlotForItem("未知金币"));
    }

    [Fact]
    public void TestSlotButton_TriggerDoubleClick_GlobalDispatch()
    {
        // 3. 验证 SlotButton 双击全局信号广播与订阅链路 (纯托管脱机测试)
        SlotButton.SlotType receivedType = SlotButton.SlotType.Shop;
        int receivedSlot = -1;

        Action<SlotButton.SlotType, int> handler = (type, slot) =>
        {
            receivedType = type;
            receivedSlot = slot;
        };

        SlotButton.SlotDoubleClickedGlobal += handler;
        try
        {
            // 静态分发测试
            SlotButton.RaiseSlotDoubleClicked(SlotButton.SlotType.Bag, 12);

            Assert.Equal(SlotButton.SlotType.Bag, receivedType);
            Assert.Equal(12, receivedSlot);
        }
        finally
        {
            SlotButton.SlotDoubleClickedGlobal -= handler;
        }
    }

    [Fact]
    public void TestServerMessageRouter_NpcClose_And_BillingNotice_Cases()
    {
        var router = new ServerMessageRouter(null);

        // 4. 验证官方 1.5 物理真值常量定义
        Assert.Equal(644, ServerMessageRouter.SM_MERCHANTDLG_CLOSE);
        Assert.Equal(529, ProtocolConsts.SM_BILLING_NOTICE);
        Assert.Equal(526, ProtocolConsts.SM_OUTOFCONNECTION);
        Assert.Equal(527, ProtocolConsts.SM_DISCONNECT_DELPHI);

        // 5. 模拟 644 号 SM_MERCHANTDLG_CLOSE 正常分发不崩溃
        byte[] closePkt = PacketEncoder.MakePacket(ServerMessageRouter.SM_MERCHANTDLG_CLOSE, 0, 0, 0);
        router.OnPacket(ServerMessageRouter.SM_MERCHANTDLG_CLOSE, closePkt.AsSpan(1..^1).ToArray());

        // 6. 模拟 529 号 SM_BILLING_NOTICE 分发
        byte[] billPkt = PacketEncoder.MakePacket(ProtocolConsts.SM_BILLING_NOTICE, 1, 0, 0, System.Text.Encoding.UTF8.GetBytes("您的点卡时间还剩 30 分钟"));
        router.OnPacket(ProtocolConsts.SM_BILLING_NOTICE, billPkt.AsSpan(1..^1).ToArray());

        // 7. 模拟 526 / 527 号断开分发
        byte[] fullPkt = PacketEncoder.MakePacket(ProtocolConsts.SM_OUTOFCONNECTION, 0, 0, 0);
        router.OnPacket(ProtocolConsts.SM_OUTOFCONNECTION, fullPkt.AsSpan(1..^1).ToArray());
    }
}
