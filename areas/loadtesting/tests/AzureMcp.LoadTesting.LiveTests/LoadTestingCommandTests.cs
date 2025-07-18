// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Core.UnitTests.Client;
using AzureMcp.Core.UnitTests.Client.Helpers;
using AzureMcp.Tests;
using Xunit;
public class LoadTestingCommandTests : CommandTestsBase,
    IClassFixture<LiveTestFixture>
{
    private readonly string _subscriptionId;
    private const string TestResourceName = "TestResourceName";
    private const string TestRunId = "TestRunId";
    public LoadTestingCommandTests(LiveTestFixture fixture, ITestOutputHelper output)
        : base(fixture, output)
    {
        _subscriptionId = Settings.SubscriptionId;
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_loadtests()
    {
        // Arrange
        var result = await CallToolAsync(
            "azmcp-loadtesting-testresource-list",
            new()
            {
                { "subscription", _subscriptionId },
                { "tenant", Settings.TenantId },
                { "resource-group", Settings.ResourceGroupName }
            });

        // Assert
        var items = result.AssertProperty("LoadTests");
        Assert.Equal(JsonValueKind.Array, items.ValueKind);
        Assert.NotEmpty(items.EnumerateArray());
        foreach (var item in items.EnumerateArray())
        {
            Assert.NotNull(item.GetProperty("Id").GetString());
        }
    }
}
