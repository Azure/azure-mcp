using System.Text;
using System.Text.Json;
using AzureMcp.Models;
using AzureMcp.Tests;
using AzureMcp.Tests.Client;
using AzureMcp.Tests.Client.Helpers;
using Xunit;

public class LoadTestingCommandTests : CommandTestsBase,
    IClassFixture<LiveTestFixture>
{
    private readonly string _subscriptionId;
    private const string LoadTestName = "LoadTestName";
    private const string LoadTestRunId = "LoadTestRunId";


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
            "azmcp-loadtesting-loadtest-list",
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
