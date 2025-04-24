// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using ModelContextProtocol.Client;
using System.Text.Json;
using Xunit;

namespace AzureMcp.Tests.Client.Helpers
{
    public class CosmosLocalAccessFixture
    {
        private bool _isInitialized = false;

        public async Task EnsureLocalAccess(LiveTestSettings settings, IMcpClient client, ITestOutputHelper output)
        {
            if (_isInitialized)
                return;

            var result = await client.CallToolAsync(
                "azmcp-extension-az",
                new Dictionary<string, object?>()
                {
                    { "command", $"resource update --subscription {settings.SubscriptionId} --resource-group {settings.ResourceGroupName} --name {settings.CosmosAccountName} --resource-type Microsoft.DocumentDB/databaseAccounts --set properties.disableLocalAuth=false" },
                });

            var content = result.Content.FirstOrDefault(c => c.MimeType == "application/json")?.Text;
            Assert.False(string.IsNullOrWhiteSpace(content));

            output.WriteLine($"azmcp-extension-az: {content}");

            var root = JsonSerializer.Deserialize<JsonElement>(content!);
            Assert.Equal(JsonValueKind.Object, root.ValueKind);

            var error = root.TryGetProperty("Error", out var property) ? property.ToString() : default;
            Assert.Null(error);
            _isInitialized = true;
        }
    }
}