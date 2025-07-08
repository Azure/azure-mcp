// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Tests.Client;
using AzureMcp.Tests.Client.Helpers;
using Xunit;

namespace AzureMcp.Tests.Areas.Grafana.LiveTests;

[Trait("Area", "Grafana")]
public class GrafanaCommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output)
    : CommandTestsBase(liveTestFixture, output), IClassFixture<LiveTestFixture>
{
    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_grafana_workspaces_by_subscription_id()
    {
        var result = await CallToolAsync(
            "azmcp-grafana-list",
            new()
            {
                { "subscription", Settings.SubscriptionId }
            });

        var workspaces = result.AssertProperty("workspaces");
        Assert.Equal(JsonValueKind.Array, workspaces.ValueKind);
        Assert.NotEmpty(workspaces.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_grafana_workspaces_by_subscription_name()
    {
        var result = await CallToolAsync(
            "azmcp-grafana-list",
            new()
            {
                { "subscription", Settings.SubscriptionName }
            });

        var workspaces = result.AssertProperty("workspaces");
        Assert.Equal(JsonValueKind.Array, workspaces.ValueKind);
        Assert.NotEmpty(workspaces.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_grafana_workspaces_by_subscription_name_with_tenant_id()
    {
        var result = await CallToolAsync(
            "azmcp-grafana-list",
            new()
            {
                { "subscription", Settings.SubscriptionName },
                { "tenant", Settings.TenantId }
            });

        var workspaces = result.AssertProperty("workspaces");
        Assert.Equal(JsonValueKind.Array, workspaces.ValueKind);
        Assert.NotEmpty(workspaces.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_include_test_grafana_workspace_in_list()
    {
        var result = await CallToolAsync(
            "azmcp-grafana-list",
            new()
            {
                { "subscription", Settings.SubscriptionId }
            });

        var workspaces = result.AssertProperty("workspaces").EnumerateArray();
        var testWorkspace = workspaces.FirstOrDefault(w => 
            w.GetProperty("name").GetString()?.StartsWith(Settings.ResourceBaseName) == true);

        Assert.True(testWorkspace.ValueKind != JsonValueKind.Undefined, 
            $"Expected to find test Grafana workspace starting with '{Settings.ResourceBaseName}' in the subscription");
        
        // Verify workspace properties
        Assert.NotNull(testWorkspace.GetProperty("name").GetString());
        Assert.NotNull(testWorkspace.GetProperty("id").GetString());
        Assert.NotNull(testWorkspace.GetProperty("location").GetString());
        Assert.NotNull(testWorkspace.GetProperty("resourceGroupName").GetString());
        
        // Verify it's in the correct resource group
        Assert.Equal(Settings.ResourceGroupName, testWorkspace.GetProperty("resourceGroupName").GetString());
    }
}
