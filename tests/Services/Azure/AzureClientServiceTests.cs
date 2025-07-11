// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Core;
using Azure.ResourceManager;
using AzureMcp.Services.Azure;
using AzureMcp.Services.Azure.Tenant;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Services.Azure;

[Trait("Area", "Core")]
public class AzureClientServiceTests
{
    [Fact]
    public void GetArmClient_ReturnsArmClient_WhenCalled()
    {
        // Arrange
        var azureClientService = new AzureClientService();
        var mockCredential = Substitute.For<TokenCredential>();
        var options = new ArmClientOptions();

        // Act
        var result = azureClientService.GetArmClient(mockCredential, options);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ArmClient>(result);
    }

    [Fact]
    public void GetArmClient_CanBeMocked_ForTesting()
    {
        // Arrange
        var mockAzureClientService = Substitute.For<AzureClientService>();
        var mockArmClient = Substitute.For<ArmClient>();
        var mockCredential = Substitute.For<TokenCredential>();
        var options = new ArmClientOptions();

        mockAzureClientService.GetArmClient(mockCredential, options)
            .Returns(mockArmClient);

        // Act
        var result = mockAzureClientService.GetArmClient(mockCredential, options);

        // Assert
        Assert.Equal(mockArmClient, result);
        mockAzureClientService.Received(1).GetArmClient(mockCredential, options);
    }
}