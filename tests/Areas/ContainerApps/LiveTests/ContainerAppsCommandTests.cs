// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Areas.ContainerApps.Services;
using AzureMcp.Services.Azure.Subscription;
using AzureMcp.Services.Azure.Tenant;
using AzureMcp.Services.Caching;
using AzureMcp.Tests.Client;
using AzureMcp.Tests.Client.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Xunit;

namespace AzureMcp.Tests.Areas.ContainerApps.LiveTests;

[Trait("Area", "ContainerApps")]
[Trait("Category", "Live")]
public class ContainerAppsCommandTests : CommandTestsBase,
    IClassFixture<LiveTestFixture>
{
    private readonly string _subscriptionId;
    private readonly string _resourceGroupName;

    public ContainerAppsCommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output) : base(liveTestFixture, output)
    {
        _subscriptionId = Settings.SubscriptionId;
        _resourceGroupName = Settings.ResourceGroupName;
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_create_container_registry()
    {
        // arrange
        var registryName = $"testreg{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}";
        var location = "eastus";
        var sku = "Basic";

        try
        {
            // act
            var result = await CallToolAsync(
                "azmcp-containerapps-containerregistry-create",
                new()
                {
                    { "subscription", _subscriptionId },
                    { "resource-group", _resourceGroupName },
                    { "registry-name", registryName },
                    { "location", location },
                    { "sku-name", sku }
                });

            // assert
            var registryProperty = result.AssertProperty("registry");
            Assert.Equal(JsonValueKind.Object, registryProperty.ValueKind);

            var nameProperty = registryProperty.AssertProperty("name");
            Assert.Equal(registryName, nameProperty.GetString());

            var locationProperty = registryProperty.AssertProperty("location");
            Assert.Equal(location, locationProperty.GetString());

            var skuProperty = registryProperty.AssertProperty("sku");
            Assert.Equal(sku, skuProperty.GetString());
        }
        catch (Exception ex)
        {
            // If the test fails, we might want to clean up, but for now just rethrow
            throw new Exception($"Container Registry creation test failed: {ex.Message}", ex);
        }
    }
}
