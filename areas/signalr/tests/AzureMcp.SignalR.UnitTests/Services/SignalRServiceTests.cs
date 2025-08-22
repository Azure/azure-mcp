// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Services.Azure.Subscription;
using AzureMcp.Core.Services.Azure.Tenant;
using AzureMcp.SignalR.Services;
using NSubstitute;
using Xunit;

namespace AzureMcp.SignalR.UnitTests.Services;

public class SignalRServiceTests
{
    private readonly ISubscriptionService _subscriptionService;
    private readonly ITenantService _tenantService;
    private readonly SignalRService _service;
    private readonly string _knownSubscriptionId = "sub123";
    private readonly string _knownResourceGroup = "rg123";
    private readonly string _knownSignalRName = "signalr123";

    public SignalRServiceTests()
    {
        _subscriptionService = Substitute.For<ISubscriptionService>();
        _tenantService = Substitute.For<ITenantService>();
        _service = new SignalRService(_subscriptionService, _tenantService);
    }

    [Fact]
    public async Task ListSignalRServicesAsync_ThrowsArgumentNullException_WhenSubscriptionIdIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _service.ListRuntimesAsync(null!));
    }

    [Fact]
    public async Task ListKeysAsync_ThrowsArgumentNullException_WhenSubscriptionIdIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _service.ListKeysAsync(null!, _knownResourceGroup, _knownSignalRName));
    }

    [Fact]
    public async Task ListKeysAsync_ThrowsArgumentNullException_WhenResourceGroupIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _service.ListKeysAsync(_knownSubscriptionId, null!, _knownSignalRName));
    }

    [Fact]
    public async Task ListKeysAsync_ThrowsArgumentNullException_WhenSignalRNameIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _service.ListKeysAsync(_knownSubscriptionId, _knownResourceGroup, null!));
    }

    [Fact]
    public async Task GetSignalRServiceAsync_ThrowsArgumentNullException_WhenSubscriptionIdIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _service.GetRuntimeAsync(null!, _knownResourceGroup, _knownSignalRName));
    }

    [Fact]
    public async Task GetSignalRServiceAsync_ThrowsArgumentNullException_WhenResourceGroupIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _service.GetRuntimeAsync(_knownSubscriptionId, null!, _knownSignalRName));
    }

    [Fact]
    public async Task GetSignalRServiceAsync_ThrowsArgumentNullException_WhenSignalRNameIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _service.GetRuntimeAsync(_knownSubscriptionId, _knownResourceGroup, null!));
    }
}
