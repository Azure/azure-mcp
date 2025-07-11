// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Core;
using Azure.ResourceManager;
using Azure.ResourceManager.Datadog;
using Azure.ResourceManager.Datadog.Models;
using AzureMcp.Areas.AzureIsv.Services.Datadog;
using AzureMcp.Services.Azure;
using AzureMcp.Services.Azure.Tenant;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Areas.AzureIsv.UnitTests.Datadog;

[Trait("Area", "AzureIsv")]
public class DatadogServiceTests
{
    private readonly AzureClientService _azureClientService;
    private readonly ITenantService _tenantService;
    private readonly ArmClient _mockArmClient;
    private readonly DatadogService _datadogService;

    public DatadogServiceTests()
    {
        _azureClientService = Substitute.For<AzureClientService>();
        _tenantService = Substitute.For<ITenantService>();
        _mockArmClient = Substitute.For<ArmClient>();
        
        _azureClientService.GetArmClient(Arg.Any<TokenCredential>(), Arg.Any<ArmClientOptions>())
            .Returns(_mockArmClient);

        _datadogService = new DatadogService(_azureClientService, _tenantService);
    }

    [Fact]
    public async Task ListMonitoredResources_ReturnsResourceNames_WhenResourcesExist()
    {
        // Arrange
        var subscription = "test-subscription";
        var resourceGroup = "test-rg";
        var datadogResource = "test-datadog";
        
        var mockDatadogMonitorResource = Substitute.For<DatadogMonitorResource>();
        var mockMonitoredResources = Substitute.For<DatadogMonitoredResourceCollection>();
        
        // Create mock monitored resources
        var mockResourceContent1 = Substitute.For<DatadogMonitoredResourceData>();
        var mockResourceContent2 = Substitute.For<DatadogMonitoredResourceData>();
        
        mockResourceContent1.Id.Returns(new ResourceIdentifier("/subscriptions/test/resourceGroups/rg1/providers/Microsoft.Compute/virtualMachines/vm1"));
        mockResourceContent2.Id.Returns(new ResourceIdentifier("/subscriptions/test/resourceGroups/rg2/providers/Microsoft.Storage/storageAccounts/storage1"));
        
        var mockResources = new List<DatadogMonitoredResourceData> { mockResourceContent1, mockResourceContent2 };
        
        // Mock the ARM client to return our datadog monitor resource
        _mockArmClient.GetDatadogMonitorResource(Arg.Any<ResourceIdentifier>())
            .Returns(mockDatadogMonitorResource);
        
        // Mock the monitored resources collection
        mockDatadogMonitorResource.GetMonitoredResources()
            .Returns(mockMonitoredResources);
        
        // Mock the collection enumeration
        mockMonitoredResources.GetEnumerator()
            .Returns(mockResources.GetEnumerator());

        // Act
        var result = await _datadogService.ListMonitoredResources(resourceGroup, subscription, datadogResource);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Contains("vm1", result);
        Assert.Contains("storage1", result);
        
        // Verify that the ARM client was called with the correct resource ID
        var expectedResourceId = $"/subscriptions/{subscription}/resourceGroups/{resourceGroup}/providers/Microsoft.Datadog/monitors/{datadogResource}";
        _mockArmClient.Received(1).GetDatadogMonitorResource(Arg.Is<ResourceIdentifier>(id => id.ToString() == expectedResourceId));
    }

    [Fact]
    public async Task ListMonitoredResources_ReturnsEmptyList_WhenNoResourcesExist()
    {
        // Arrange
        var subscription = "test-subscription";
        var resourceGroup = "test-rg";
        var datadogResource = "test-datadog";
        
        var mockDatadogMonitorResource = Substitute.For<DatadogMonitorResource>();
        var mockMonitoredResources = Substitute.For<DatadogMonitoredResourceCollection>();
        
        _mockArmClient.GetDatadogMonitorResource(Arg.Any<ResourceIdentifier>())
            .Returns(mockDatadogMonitorResource);
        
        mockDatadogMonitorResource.GetMonitoredResources()
            .Returns(mockMonitoredResources);
        
        // Mock empty collection
        var emptyResources = new List<DatadogMonitoredResourceData>();
        mockMonitoredResources.GetEnumerator()
            .Returns(emptyResources.GetEnumerator());

        // Act
        var result = await _datadogService.ListMonitoredResources(resourceGroup, subscription, datadogResource);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task ListMonitoredResources_ThrowsException_WhenArmClientFails()
    {
        // Arrange
        var subscription = "test-subscription";
        var resourceGroup = "test-rg";
        var datadogResource = "test-datadog";
        
        _mockArmClient.GetDatadogMonitorResource(Arg.Any<ResourceIdentifier>())
            .Throws(new InvalidOperationException("ARM client error"));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => 
            _datadogService.ListMonitoredResources(resourceGroup, subscription, datadogResource));
        
        Assert.Contains("Error listing monitored resources", exception.Message);
        Assert.Contains("ARM client error", exception.Message);
    }
}