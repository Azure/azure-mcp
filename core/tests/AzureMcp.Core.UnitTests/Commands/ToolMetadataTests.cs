// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using Xunit;

namespace AzureMcp.Core.UnitTests.Commands;

public class ToolMetadataTests
{
    [Fact]
    public void ToolMetadata_DefaultValues_ShouldBeCorrect()
    {
        // Arrange & Act
        var metadata = new ToolMetadata();

        // Assert
        Assert.True(metadata.Destructive); // Default should be true
        Assert.False(metadata.Idempotent); // Default should be false
        Assert.True(metadata.OpenWorld); // Default should be true
        Assert.False(metadata.ReadOnly); // Default should be false
        Assert.False(metadata.Secret); // Default should be false
    }

    [Fact]
    public void ToolMetadata_CanSetAllProperties()
    {
        // Arrange & Act
        var metadata = new ToolMetadata
        {
            Destructive = false,
            Idempotent = true,
            OpenWorld = false,
            ReadOnly = true,
            Secret = true
        };

        // Assert
        Assert.False(metadata.Destructive);
        Assert.True(metadata.Idempotent);
        Assert.False(metadata.OpenWorld);
        Assert.True(metadata.ReadOnly);
        Assert.True(metadata.Secret);
    }

    [Fact]
    public void ToolMetadata_InitSyntax_ShouldWork()
    {
        // Arrange & Act
        var metadata = new ToolMetadata
        {
            Destructive = false,
            ReadOnly = true,
            Secret = false
        };

        // Assert
        Assert.False(metadata.Destructive);
        Assert.False(metadata.Idempotent); // Default value
        Assert.True(metadata.OpenWorld); // Default value
        Assert.True(metadata.ReadOnly);
        Assert.False(metadata.Secret);
    }
}
