// ============================================================================
// Mir2 客户端 - 32 门官方技能视听资源完整性容错校验树测试矩阵
// (TestSkillResourceValidationTreeMatrix)
// 实证验证《工程认知公理 4.0》中揭示的“边缘技能在客户端资源包缺失或格式解析异常时可能产生静默或闪退”坏账的修复效果：
// 1. 1~33 官方技能全量登记覆盖率 100%
// 2. 静态全量扫描精准识别贴图与音效坏块/缺漏
// 3. 运行期自动安全回退降级，保障渲染端 0 异常崩溃
// ============================================================================

using System;
using System.Collections.Generic;
using Mir2Client.Render;
using Xunit;

namespace Mir2Client.Tests;

public class TestSkillResourceValidationTreeMatrix
{
    [Fact]
    public void Test_All33Skills_RegisteredInValidationTree()
    {
        Assert.True(SkillResourceValidationTree.Count >= 33, "必须完整覆盖官方 1~33 技能全量封闭分母");

        for (int magicId = 1; magicId <= 33; magicId++)
        {
            var desc = SkillResourceValidationTree.GetDescriptor(magicId);
            Assert.NotNull(desc);
            Assert.Equal(magicId, desc.MagicId);
            Assert.False(string.IsNullOrWhiteSpace(desc.Name), $"技能 {magicId} 必须具有官方中文名称");
            Assert.True(desc.SpellWil >= 1 && desc.SpellWil <= 2, $"技能 {magicId} WIL 索引必须为 1 或 2");
            Assert.True(desc.FallbackMagicId >= 1 && desc.FallbackMagicId <= 33, $"技能 {magicId} 回退技能 ID 必须合法");
        }
    }

    [Fact]
    public void Test_ValidateAll_DetectsMissingTexturesAndAudio()
    {
        // 模拟外部资源库：16 号困魔咒缺失贴图，24 号地狱雷光缺失音效，其余正常
        var scanResults = SkillResourceValidationTree.ValidateAll(
            isWilFrameValid: (wil, frame) =>
            {
                // 模拟 16 号困魔咒帧 3990 缺失
                if (frame == 3990) return false;
                return true;
            },
            isSfxValid: sfxId =>
            {
                // 模拟 24 号地狱雷光音效 10240 损坏
                if (sfxId == 10240) return false;
                return true;
            }
        );

        Assert.Equal(SkillResourceValidationTree.Count, scanResults.Count);

        // 验证 16 号技能 (困魔咒) 被精准识别为贴图缺陷
        Assert.True(scanResults.ContainsKey(16));
        Assert.False(scanResults[16].IsValid);
        Assert.True(scanResults[16].HasTextureIssue);
        Assert.Contains("3990", scanResults[16].WarningMessage);

        // 验证 24 号技能 (地狱雷光) 被精准识别为音效缺陷
        Assert.True(scanResults.ContainsKey(24));
        Assert.False(scanResults[24].IsValid);
        Assert.True(scanResults[24].HasAudioIssue);
        Assert.Contains("10240", scanResults[24].WarningMessage);

        // 验证 1 号火球术完全正常
        Assert.True(scanResults[1].IsValid);
        Assert.False(scanResults[1].HasTextureIssue);
        Assert.False(scanResults[1].HasAudioIssue);
    }

    [Fact]
    public void Test_ResolveSafeDescriptor_GracefulFallback()
    {
        // 1. 资源正常时，直接返回原技能描述符
        var normalDesc = SkillResourceValidationTree.ResolveSafeDescriptor(
            magicId: 10, // 雷电术
            isWilFrameValid: (wil, frame) => true,
            isSfxValid: sfx => true,
            out bool normalDowngraded
        );
        Assert.False(normalDowngraded);
        Assert.Equal(10, normalDesc.MagicId);
        Assert.Equal("雷电术", normalDesc.Name);

        // 2. 当 10 号雷电术贴图损坏时，必须安全平滑降级至基础技能 (1号火球术)，且不抛异常
        var fallbackDesc = SkillResourceValidationTree.ResolveSafeDescriptor(
            magicId: 10,
            isWilFrameValid: (wil, frame) => false, // 模拟全贴图缺失
            isSfxValid: sfx => true,
            out bool downgraded
        );
        Assert.True(downgraded);
        Assert.Equal(1, fallbackDesc.MagicId);
        Assert.Equal("火球术", fallbackDesc.Name);

        // 3. 当传入非法或未知技能 ID (如 999) 时，安全降级至 1 号技能，杜绝空指针异常
        var unknownDesc = SkillResourceValidationTree.ResolveSafeDescriptor(
            magicId: 999,
            isWilFrameValid: (wil, frame) => true,
            isSfxValid: sfx => true,
            out bool unknownDowngraded
        );
        Assert.True(unknownDowngraded);
        Assert.Equal(1, unknownDesc.MagicId);
    }
}
