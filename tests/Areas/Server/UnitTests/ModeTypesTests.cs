// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Server.Options;
using Xunit;

namespace AzureMcp.Tests.Areas.Server.UnitTests;

[Trait("Area", "Server")]
public class ModeTypesTests
{
    [Theory]
    [InlineData("single", "single")]
    [InlineData("namespace", "namespace")]
    [InlineData("unified-tool", "single")]
    [InlineData("per-service", "namespace")]
    [InlineData("unknown-mode", "unknown-mode")]
    public void GetCanonicalMode_ReturnsCorrectMapping(string inputMode, string expectedCanonicalMode)
    {
        // Act
        var result = ModeTypes.GetCanonicalMode(inputMode);

        // Assert
        Assert.Equal(expectedCanonicalMode, result);
    }

    [Fact]
    public void AllModeValues_ContainsAllExpectedModes()
    {
        // Act
        var allModes = ModeTypes.AllModeValues;

        // Assert
        Assert.Contains(ModeTypes.SingleToolProxy, allModes);
        Assert.Contains(ModeTypes.NamespaceProxy, allModes);
        Assert.Contains(ModeTypes.UnifiedTool, allModes);
        Assert.Contains(ModeTypes.PerServiceTool, allModes);
        Assert.Equal(4, allModes.Length);
    }

    [Fact]
    public void Constants_HaveExpectedValues()
    {
        // Assert
        Assert.Equal("single", ModeTypes.SingleToolProxy);
        Assert.Equal("namespace", ModeTypes.NamespaceProxy);
        Assert.Equal("unified-tool", ModeTypes.UnifiedTool);
        Assert.Equal("per-service", ModeTypes.PerServiceTool);
    }

    [Theory]
    [InlineData("unified-tool", true)]
    [InlineData("per-service", true)]
    [InlineData("single", false)]
    [InlineData("namespace", false)]
    public void NewDescriptiveModes_AreDistinguishable(string mode, bool isDescriptiveAlias)
    {
        // Act
        var isNewMode = mode == ModeTypes.UnifiedTool || mode == ModeTypes.PerServiceTool;

        // Assert
        Assert.Equal(isDescriptiveAlias, isNewMode);
    }
}