using System;
using System.Collections.Generic;
using Godot;
using Mir2Client.UI;

namespace Mir2Client.Render;

/// <summary>
/// 端到端 (E2E) 与 UI 像素级实证调试专用：1 像素高对比度细线框审计层
/// 彻底修复全局单 Z 序穿透坏账：
/// 不再采用全局顶层 CanvasLayer (Layer 128) 统揽绘制，而是为每个被审计的活动 Control
/// 动态挂载与其自身 Z-Index 与树结构严格绑定的自绘装饰子节点 (WireframeDrawer)，
/// 确保上层弹窗、浮窗与模态框能依据其本身的 Z 序完全遮蔽下层线框，
/// 严格 100% 杜绝 Z 序穿透与重叠污染。
/// </summary>
public partial class DebugGridOverlay : Node
{
    // 用户铁律实锤：统一使用单一高对比度青色线框，消除杂色冲突
    private static readonly Color UnifiedBorderColor = new(0.2f, 0.8f, 1f, 0.9f);

    private const string OverlayNodeName = "__AuditWireframeOverlay__";

    public override void _Ready()
    {
        GD.Print("[DebugGridOverlay] 1 像素控件边框审计模式已启用（统一单一青色线框与智能遮挡绑定架构）！");
    }

    public override void _Process(double delta)
    {
        var root = GetTree()?.Root;
        if (root == null) return;

        UpdateOverlaysRecursive(root);
    }

    private void UpdateOverlaysRecursive(Node node)
    {
        if (node == this) return;

        if (node is Control ctrl && !ctrl.Name.ToString().StartsWith("__Audit"))
        {
            // 纯背景大贴图不绘制边框，但特定小图标（如血球 HudOrb、金币图标、技能设置槽等）需要审计框出
            if (ctrl is TextureRect tr)
            {
                string trName = tr.Name.ToString();
                if ((trName is "HudOrb" or "GoldIcon" or "RemoteGoldIcon" or "MyGoldIcon" or "StorageGoldIcon" or "KeySelectSkillIcon") && tr.IsVisibleInTree())
                {
                    EnsureOverlay(ctrl);
                }
                else
                {
                    RemoveOverlayIfPresent(ctrl);
                }
            }
            // 对 Label / RichTextLabel 增加判空：无实质文本或空白串时不画框，彻底消除登录面板等处多出的空框
            // 对 Label / RichTextLabel 增加判空：无实质文本或空白串时不画框，且忽略药水槽内部计数标签（杜绝物品栏内多出小框）
            else if (ctrl is Label lbl)
            {
                if (lbl.Name.ToString() != "CountLbl" && lbl.IsVisibleInTree() && !string.IsNullOrWhiteSpace(lbl.Text))
                {
                    var size = lbl.Size;
                    if (size.X > 1 && size.Y > 1 && (size.X < 800 || size.Y < 600))
                    {
                        EnsureOverlay(lbl);
                    }
                    else
                    {
                        RemoveOverlayIfPresent(lbl);
                    }
                }
                else
                {
                    RemoveOverlayIfPresent(lbl);
                }
            }
            else if (ctrl is RichTextLabel rtl)
            {
                if (rtl.IsVisibleInTree() && !string.IsNullOrWhiteSpace(rtl.Text))
                {
                    var size = rtl.Size;
                    if (size.X > 1 && size.Y > 1 && (size.X < 800 || size.Y < 600))
                    {
                        EnsureOverlay(rtl);
                    }
                    else
                    {
                        RemoveOverlayIfPresent(rtl);
                    }
                }
                else
                {
                    RemoveOverlayIfPresent(rtl);
                }
            }
            // 经验条、负重条进度条控件纳入审计画框
            else if (ctrl is TextureProgressBar tpb)
            {
                if (tpb.IsVisibleInTree())
                {
                    EnsureOverlay(tpb);
                }
                else
                {
                    RemoveOverlayIfPresent(tpb);
                }
            }
            // 仅对具有实质表现或交互的实体控件（Panel、Button、输入框等）绘制，排除全屏黑暗遮罩等全局效果
            else if (ctrl is Panel or Button or BaseButton or LineEdit or TextEdit)
            {
                string ctrlName = ctrl.Name.ToString();
                // 排除全屏遮罩、大世界原生窗口外壳Panel底板，避免异形外边缘与镂空区域产生悬空青色矩形框
                bool isShellPanel = ctrl is Panel && (
                    ctrlName is "DarknessFogMask" or "BagPanel" or "EquipPanel" or "StatusBg" or "Panel"
                             or "RemoteTradePanel" or "MyTradePanel" or "StorageGoldBox"
                    || ctrl.GetParent() is IManagedWindow or SkillPanelView or StoragePanelView or ShopPanelView
                             or RepairPanelView or GroupPanelView or GuildPanelView or AdjustAbilPanelView
                             or KeySelectDialogView or NpcDialogView or DeathScreenView
                );

                // HUD 右下角弧形 4 按键：底板 Prguse_0001 已烘焙暗色按键！平时未激活不高亮时不绘制激活框，按下或对应面板激活（TextureNormal != null）才画框
                bool isInactiveArcButton = ctrl is TextureButton tb &&
                    ctrlName is "BtnHudBag" or "BtnHudEquip" or "BtnHudSkill" or "BtnHudGroup" &&
                    tb.TextureNormal == null;

                if (isShellPanel || isInactiveArcButton || ctrlName is "DarknessFogMask" or "BagPanel" or "EquipPanel" or "StatusBg")
                {
                    RemoveOverlayIfPresent(ctrl);
                }
                else if (ctrl.IsVisibleInTree())
                {
                    var size = ctrl.Size;
                    if (size.X > 1 && size.Y > 1 && (size.X < 800 || size.Y < 600))
                    {
                        EnsureOverlay(ctrl);
                    }
                    else
                    {
                        RemoveOverlayIfPresent(ctrl);
                    }
                }
                else
                {
                    RemoveOverlayIfPresent(ctrl);
                }
            }
            else
            {
                RemoveOverlayIfPresent(ctrl);
            }
        }

        foreach (Node child in node.GetChildren())
        {
            UpdateOverlaysRecursive(child);
        }
    }

    private static void EnsureOverlay(Control target)
    {
        var existing = target.GetNodeOrNull<WireframeDrawer>(OverlayNodeName);
        if (existing == null)
        {
            var drawer = new WireframeDrawer(target);
            drawer.Name = OverlayNodeName;
            target.AddChild(drawer);
        }
        else
        {
            existing.QueueRedraw();
        }
    }

    private static void RemoveOverlayIfPresent(Control target)
    {
        var existing = target.GetNodeOrNull<WireframeDrawer>(OverlayNodeName);
        if (existing != null)
        {
            existing.QueueFree();
        }
    }

    private sealed partial class WireframeDrawer : Control
    {
        private readonly Control _target;

        public WireframeDrawer(Control target)
        {
            _target = target;
            MouseFilter = MouseFilterEnum.Ignore;
            // 紧贴父控件并在其正上方一层绘制（继承父控件 Z 序，杜绝全局穿透）
            SetAnchorsPreset(LayoutPreset.FullRect);
            ZIndex = 0;
            TopLevel = false;
        }

        public override void _Draw()
        {
            if (!_target.IsVisibleInTree()) return;

            // 盛大原版独占模态窗阻断：若场景中存在活动的全屏阻断模态框（如 MessageBoxDialog / DeathScreenView），
            // 且当前控件不属于该模态窗体子树，则下层所有背景 UI（如 HUD 按键、聊天框、大底栏）绝对不绘制线框
            if (IsOccludedByActiveModal(_target)) return;

            // 智能遮挡剔除：若该控件所在区域被处于更高显示层级、且完全不透明的活动面板完全覆盖或被部分遮盖，则不绘制线框
            if (IsTargetOccludedByHigherPanel(_target)) return;

            // 统一为高对比度单一青色，杜绝杂色干扰
            var rect = new Rect2(Vector2.Zero, _target.Size);
            DrawRect(rect, UnifiedBorderColor, filled: false, width: 1.0f);
        }

        private static bool IsOccludedByActiveModal(Control target)
        {
            var root = target.GetTree()?.Root;
            if (root == null) return false;

            return CheckActiveModalRecursive(root, target);
        }

        private static bool CheckActiveModalRecursive(Node node, Control target)
        {
            if (node is Control ctrl && ctrl.IsVisibleInTree())
            {
                string typeName = ctrl.GetType().Name;
                // 盛大原版官方全屏阻断式模态窗口类型
                if (typeName is "MessageBoxDialog" or "DeathScreenView")
                {
                    // 若 target 不在该模态窗体子树内，则目标处于阻断背景层
                    if (!ctrl.IsAncestorOf(target) && ctrl != target)
                    {
                        return true;
                    }
                }
            }

            foreach (Node child in node.GetChildren())
            {
                if (CheckActiveModalRecursive(child, target))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsTargetOccludedByHigherPanel(Control target)
        {
            // 盛大原版官方 HUD 常驻核心交互组件（血球、小地图切换等）拥有绝对最高渲染优先级，绝不允许被误判遮挡剔除
            string targetName = target.Name.ToString();
            if (targetName is "HudOrb" or "OrbHp" or "OrbMp"
                or "RemoteGoldIcon" or "MyGoldIcon" or "GoldIcon" or "StorageGoldIcon")
            {
                return false;
            }

            var targetRect = target.GetGlobalRect();
            var root = target.GetTree()?.Root;
            if (root == null) return false;

            return CheckOcclusionRecursive(root, target, targetRect);
        }

        private static bool CheckOcclusionRecursive(Node node, Control target, Rect2 targetRect)
        {
            if (node is Control otherCtrl && otherCtrl != target && !otherCtrl.IsAncestorOf(target) && !target.IsAncestorOf(otherCtrl))
            {
                // 仅当另一个控件是可见的实体面板容器（Panel 或各类以 Panel/Dlg/Dialog 命名的窗体面板容器），且面积较大
                if (otherCtrl.IsVisibleInTree() && (otherCtrl is Panel || otherCtrl is IManagedWindow || otherCtrl.GetType().Name.EndsWith("Panel") || otherCtrl.GetType().Name.EndsWith("PanelView") || otherCtrl.GetType().Name.EndsWith("Dialog") || otherCtrl.GetType().Name.EndsWith("DialogView")))
                {
                    var otherRect = otherCtrl.GetGlobalRect();
                    // 面板尺寸必须大于一定阈值（排除小边框修饰物）
                    if (otherRect.Size.X >= 100 && otherRect.Size.Y >= 80)
                    {
                        // 判定是否完全覆盖遮蔽或被弹窗深度压盖（相交面积超过目标尺寸 40% 才视为有效遮挡，杜绝边缘轻微贴靠导致的误杀）
                        if (otherRect.Encloses(targetRect))
                        {
                            if (IsNodeRenderedAbove(otherCtrl, target)) return true;
                        }
                        else if (otherRect.Intersects(targetRect))
                        {
                            var inter = otherRect.Intersection(targetRect);
                            float interArea = inter.Size.X * inter.Size.Y;
                            float targetArea = targetRect.Size.X * targetRect.Size.Y;
                            if (targetArea > 0 && (interArea / targetArea) >= 0.4f)
                            {
                                if (IsNodeRenderedAbove(otherCtrl, target)) return true;
                            }
                        }
                    }
                }
            }

            foreach (Node child in node.GetChildren())
            {
                if (CheckOcclusionRecursive(child, target, targetRect))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 基于 Godot 场景树渲染规则（有效 Z-Index + LCA 树序），精确判定 aboveCandidate 是否渲染在 belowCandidate 之上
        /// </summary>
        private static bool IsNodeRenderedAbove(Control aboveCandidate, Control belowCandidate)
        {
            int zAbove = GetEffectiveZIndex(aboveCandidate);
            int zBelow = GetEffectiveZIndex(belowCandidate);
            if (zAbove > zBelow) return true;
            if (zAbove < zBelow) return false;

            // ZIndex 相同时，通过最近公共祖先 (LCA) 比较兄弟分支的 Tree Index 绘制先后顺序
            var ancestorsBelow = new List<Node>();
            for (Node? n = belowCandidate; n != null; n = n.GetParent())
            {
                ancestorsBelow.Add(n);
            }

            Node? curAbove = aboveCandidate;
            Node? common = null;
            Node? childAbove = null;
            while (curAbove != null)
            {
                int idx = ancestorsBelow.IndexOf(curAbove);
                if (idx != -1)
                {
                    common = curAbove;
                    break;
                }
                childAbove = curAbove;
                curAbove = curAbove.GetParent();
            }

            if (common == null || childAbove == null) return false;

            int commonIdx = ancestorsBelow.IndexOf(common);
            Node childBelow = commonIdx > 0 ? ancestorsBelow[commonIdx - 1] : belowCandidate;

            return childAbove.GetIndex() > childBelow.GetIndex();
        }

        private static int GetEffectiveZIndex(CanvasItem item)
        {
            int z = item.ZIndex;
            Node? parent = item.GetParent();
            while (parent is CanvasItem ci)
            {
                z += ci.ZIndex;
                parent = ci.GetParent();
            }
            return z;
        }
    }
}
