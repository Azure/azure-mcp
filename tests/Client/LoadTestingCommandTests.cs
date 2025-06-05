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

    public LoadTestingCommandTests(LiveTestFixture fixture, ITestOutputHelper output)
        : base(fixture, output)
    {
        _subscriptionId = Settings.SubscriptionId;
    }

    [Theory]
    [InlineData(AuthMethod.Credential)]
    [InlineData(AuthMethod.Key)] 
    [Trait("Category", "Live")]
    public async Task Should_List_LoadTests_WithAuth(AuthMethod authMethod)
    {
        // Arrange
        var result = await CallToolAsync(
            "azmcp-loadtesting-loadtest-list",
            new() 
            {
                { "subscription", _subscriptionId },
                { "tenant", Settings.TenantId },
                { "auth-method", authMethod.ToString().ToLowerInvariant() }
            });

        // Assert
        var items = result.AssertProperty("items");
        Assert.Equal(JsonValueKind.Array, items.ValueKind);

        // Check results format
        foreach (var item in items.EnumerateArray())
        {
            Assert.True(item.TryGetProperty("name", out _));
            Assert.True(item.TryGetProperty("id", out _));
        }
    }
}