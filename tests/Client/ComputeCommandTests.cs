using AzureMcp.Tests.Client.Helpers;
using System.Text.Json;
using Xunit;

namespace AzureMcp.Tests.Client
{
    public class ComputeCommandTests(
        McpClientFixture mcpClient,
        LiveTestSettingsFixture liveTestSettings,
        ITestOutputHelper output)
        : CommandTestsBase(mcpClient, liveTestSettings, output),
            IClassFixture<McpClientFixture>, IClassFixture<LiveTestSettingsFixture>
    {
        [Fact]
        [Trait("Category", "Live")]
        public async Task Should_list_compute_vms_by_subscription_id()
        {
            var result = await CallToolAsync(
                "azmcp-compute-vms-list",
                new() { { "subscription", Settings.SubscriptionId } });

            Assert.True(result.TryGetProperty("vms", out var vms));
            Assert.Equal(JsonValueKind.Array, vms.ValueKind);
            Assert.NotEmpty(vms.EnumerateArray());
        }
    }
}