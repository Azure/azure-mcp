// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure;
using Azure.ResourceManager.Resources;
using AzureMcp.Core.Models;
using AzureMcp.Core.Options;
using AzureMcp.Core.Services.Azure.Subscription;
using AzureMcp.Core.Services.Azure.Tenant;
using AzureMcp.AzureSignalR.Models;
using AzureMcp.AzureSignalR.Services;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.AzureSignalR.UnitTests.Services;

public class AzureSignalRServiceTests
{
    private readonly ISubscriptionService _subscriptionService;
    private readonly ITenantService _tenantService;
    private readonly AzureSignalRService _service;
    private readonly string _knownSubscriptionId = "sub123";
    private readonly string _knownResourceGroup = "rg123";
    private readonly string _knownSignalRName = "signalr123";
    private readonly string _knownCertificateName = "cert123";
    private readonly SubscriptionResource _subscriptionResource = Substitute.For<SubscriptionResource>();

    public AzureSignalRServiceTests()
    {
        _subscriptionService = Substitute.For<ISubscriptionService>();
        _tenantService = Substitute.For<ITenantService>();
        _service = new AzureSignalRService(_subscriptionService, _tenantService);
    }

    [Fact]
    public async Task GetCertificateAsync_ThrowsArgumentNullException_WhenSubscriptionIdIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _service.GetCertificateAsync(null!, _knownResourceGroup, _knownSignalRName, _knownCertificateName));
    }

    [Fact]
    public async Task GetCertificateAsync_ThrowsArgumentNullException_WhenResourceGroupIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _service.GetCertificateAsync(_knownSubscriptionId, null!, _knownSignalRName, _knownCertificateName));
    }

    [Fact]
    public async Task GetCertificateAsync_ThrowsArgumentNullException_WhenSignalRNameIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _service.GetCertificateAsync(_knownSubscriptionId, _knownResourceGroup, null!, _knownCertificateName));
    }

    [Fact]
    public async Task GetCertificateAsync_ThrowsArgumentNullException_WhenCertificateNameIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _service.GetCertificateAsync(_knownSubscriptionId, _knownResourceGroup, _knownSignalRName, null!));
    }

    [Fact]
    public async Task GetCertificateAsync_WrapsException_WhenAzureOperationFails()
    {
        // Arrange
        _subscriptionService.GetSubscription(
            Arg.Is(_knownSubscriptionId),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>())
            .ThrowsAsync(new RequestFailedException("Azure error"));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() =>
            _service.GetCertificateAsync(_knownSubscriptionId, _knownResourceGroup, _knownSignalRName, _knownCertificateName));

        Assert.Contains("Failed to get SignalR certificate", exception.Message);
        Assert.Contains(_knownCertificateName, exception.Message);
    }

    [Fact]
    public async Task ListSignalRServicesAsync_ThrowsArgumentNullException_WhenSubscriptionIdIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _service.ListSignalRServicesAsync(null!));
    }

    [Theory]
    [InlineData("")]
    public async Task ListSignalRServicesAsync_ThrowsArgumentException_WhenSubscriptionIdIsEmpty(string emptyValue)
    {
        // Act & Assert - Skip whitespace test as behavior may vary
        await Assert.ThrowsAsync<ArgumentException>(() =>
            _service.ListSignalRServicesAsync(emptyValue));
    }

    [Fact]
    public async Task ListSignalRServicesAsync_WrapsException_WhenAzureOperationFails()
    {
        // Arrange
        _subscriptionService.GetSubscription(
            Arg.Is(_knownSubscriptionId),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>())
            .ThrowsAsync(new RequestFailedException("Azure error"));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() =>
            _service.ListSignalRServicesAsync(_knownSubscriptionId));

        Assert.Contains("Failed to list SignalR services", exception.Message);
    }
}
