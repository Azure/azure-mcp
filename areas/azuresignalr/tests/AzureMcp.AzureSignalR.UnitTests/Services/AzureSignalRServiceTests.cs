// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Services.Azure.Subscription;
using AzureMcp.Core.Services.Azure.Tenant;
using AzureMcp.AzureSignalR.Services;
using NSubstitute;
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
    public async Task ListSignalRServicesAsync_ThrowsArgumentNullException_WhenSubscriptionIdIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _service.ListSignalRServicesAsync(null!));
    }

    [Fact]
    public async Task ListCustomDomainsAsync_ThrowsArgumentNullException_WhenSubscriptionIdIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _service.ListCustomDomainsAsync(null!, _knownResourceGroup, _knownSignalRName));
    }

    [Fact]
    public async Task ListCustomDomainsAsync_ThrowsArgumentNullException_WhenResourceGroupIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _service.ListCustomDomainsAsync(_knownSubscriptionId, null!, _knownSignalRName));
    }

    [Fact]
    public async Task ListCustomDomainsAsync_ThrowsArgumentNullException_WhenSignalRNameIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _service.ListCustomDomainsAsync(_knownSubscriptionId, _knownResourceGroup, null!));
    }

    [Fact]
    public async Task ListCertificatesAsync_ThrowsArgumentNullException_WhenSubscriptionIdIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _service.ListCertificatesAsync(null!, _knownResourceGroup, _knownSignalRName));
    }

    [Fact]
    public async Task ListCertificatesAsync_ThrowsArgumentNullException_WhenResourceGroupIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _service.ListCertificatesAsync(_knownSubscriptionId, null!, _knownSignalRName));
    }

    [Fact]
    public async Task ListCertificatesAsync_ThrowsArgumentNullException_WhenSignalRNameIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _service.ListCertificatesAsync(_knownSubscriptionId, _knownResourceGroup, null!));
    }

    [Fact]
    public async Task GetCustomDomainAsync_ThrowsArgumentNullException_WhenSubscriptionIdIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _service.GetCustomDomainAsync(null!, _knownResourceGroup, _knownSignalRName, "domain"));
    }

    [Fact]
    public async Task GetCustomDomainAsync_ThrowsArgumentNullException_WhenResourceGroupIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _service.GetCustomDomainAsync(_knownSubscriptionId, null!, _knownSignalRName, "domain"));
    }

    [Fact]
    public async Task GetCustomDomainAsync_ThrowsArgumentNullException_WhenSignalRNameIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _service.GetCustomDomainAsync(_knownSubscriptionId, _knownResourceGroup, null!, "domain"));
    }

    [Fact]
    public async Task GetCustomDomainAsync_ThrowsArgumentNullException_WhenCustomDomainNameIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _service.GetCustomDomainAsync(_knownSubscriptionId, _knownResourceGroup, _knownSignalRName, null!));
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
            _service.GetSignalRServiceAsync(null!, _knownResourceGroup, _knownSignalRName));
    }

    [Fact]
    public async Task GetSignalRServiceAsync_ThrowsArgumentNullException_WhenResourceGroupIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _service.GetSignalRServiceAsync(_knownSubscriptionId, null!, _knownSignalRName));
    }

    [Fact]
    public async Task GetSignalRServiceAsync_ThrowsArgumentNullException_WhenSignalRNameIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _service.GetSignalRServiceAsync(_knownSubscriptionId, _knownResourceGroup, null!));
    }
}
