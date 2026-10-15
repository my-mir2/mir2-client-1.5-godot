using System;
using Mir2Client.UI;
using Xunit;

namespace Mir2Client.Tests;

public class MockWindow : IManagedWindow
{
    public string WindowName { get; }
    public bool IsVisible { get; set; }
    public int ZIndex { get; set; }
    public (int x, int y, int width, int height) Bounds { get; set; }

    public MockWindow(string name, int x, int y, int w, int h)
    {
        WindowName = name;
        Bounds = (x, y, w, h);
    }
}

/// <summary>
/// 官方标准 xUnit 窗体管理栈、ESC 轮转与防点击穿透测试套件
/// 独立于游戏引擎渲染主循环，直接由 dotnet test 高速执行，绝不挂起
/// </summary>
public class TestUiWindowStack
{
    [Fact]
    public void TestWindowStack_LifoPushAndPop()
    {
        var mgr = new WindowStackManager();
        var bag = new MockWindow("BagPanel", 100, 100, 200, 200);
        var equip = new MockWindow("EquipPanel", 300, 100, 200, 200);

        mgr.PushWindow(bag);
        Assert.Equal(1, mgr.Count);
        Assert.True(bag.IsVisible);
        Assert.Equal(10, bag.ZIndex);

        mgr.PushWindow(equip);
        Assert.Equal(2, mgr.Count);
        Assert.True(equip.IsVisible);
        Assert.Equal(10, bag.ZIndex);
        Assert.Equal(11, equip.ZIndex); // 最顶层拥有更高 ZIndex

        // ESC 关闭顶层窗口 (LIFO)
        var closed = mgr.CloseTopDialog();
        Assert.Equal("EquipPanel", closed?.WindowName);
        Assert.False(equip.IsVisible);
        Assert.Equal(1, mgr.Count);

        var closed2 = mgr.CloseTopDialog();
        Assert.Equal("BagPanel", closed2?.WindowName);
        Assert.False(bag.IsVisible);
        Assert.Equal(0, mgr.Count);
    }

    [Fact]
    public void TestIsMouseOverUi_BlockingWorldClick()
    {
        var mgr = new WindowStackManager();
        var bag = new MockWindow("BagPanel", 100, 100, 200, 200);
        mgr.PushWindow(bag);

        // 1. 点在底栏 HUD (y >= 430) -> 必须被阻断
        Assert.True(mgr.IsMouseOverUi(50, 450));

        // 2. 点在背包窗口内 (150, 150) -> 必须被阻断
        Assert.True(mgr.IsMouseOverUi(150, 150));

        // 3. 点在大世界空白处 (50, 50) -> 不阻断
        Assert.False(mgr.IsMouseOverUi(50, 50));
    }
}
