// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.ResourceManager;
using AzureMcp.Options;
using AzureMcp.Services.Azure;
using AzureMcp.Services.Azure.Tenant;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Services.Azure;

[Trait("Area", "Core")]
public class BaseAzureServiceTests
{
    private const string TenantId = "test-tenant-id";
    private const string TenantName = "test-tenant-name";

    private readonly ITenantService _tenantService = Substitute.For<ITenantService>();
    private readonly AzureClientService _azureClientService = Substitute.For<AzureClientService>();
    private readonly TestAzureService _azureService;

    public BaseAzureServiceTests()
    {
        _azureService = new TestAzureService(_azureClientService);
        _tenantService.GetTenantId(TenantName).Returns(TenantId);
    }

    [Fact]
    public async Task CreateArmClientAsync_CreatesAndUsesCachedClient()
    {
        // Arrange
        var mockArmClient = Substitute.For<ArmClient>();
        var mockArmClient2 = Substitute.For<ArmClient>();
        
        _azureClientService.GetArmClient(Arg.Any<Azure.Core.TokenCredential>(), Arg.Any<ArmClientOptions>())
            .Returns(mockArmClient, mockArmClient2);

        // Act
        var tenantName2 = "Other-Tenant-Name";
        var tenantId2 = "Other-Tenant-Id";

        _tenantService.GetTenantId(tenantName2).Returns(tenantId2);

        var retryPolicyArgs = new RetryPolicyOptions
        {
            DelaySeconds = 5,
            MaxDelaySeconds = 15,
            MaxRetries = 3
        };

        var client = await _azureService.GetArmClientAsync(TenantName, retryPolicyArgs);
        var client2 = await _azureService.GetArmClientAsync(TenantName, retryPolicyArgs);

        Assert.Equal(client, client2);

        var otherClient = await _azureService.GetArmClientAsync(tenantName2, retryPolicyArgs);

        Assert.NotEqual(client, otherClient);
    }

    [Fact]
    public async Task ResolveTenantIdAsync_ReturnsValueNoService()
    {
        var testAzureService = new TestAzureService(_azureClientService, null);

        string? actual = await testAzureService.ResolveTenantId(TenantName);
        Assert.Equal(TenantName, actual);

        string? actual2 = await testAzureService.ResolveTenantId(null);
        Assert.Null(actual2);
    }

    private sealed class TestAzureService(AzureClientService azureClientService, ITenantService? tenantService = null) : BaseAzureService(azureClientService, tenantService)
    {
        public Task<ArmClient> GetArmClientAsync(string? tenant = null, RetryPolicyOptions? retryPolicy = null) =>
            CreateArmClientAsync(tenant, retryPolicy);

        public Task<string?> ResolveTenantId(string? tenant) => ResolveTenantIdAsync(tenant);
    }
}
