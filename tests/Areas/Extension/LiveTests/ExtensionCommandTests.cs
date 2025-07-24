// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Tests.Client;
using AzureMcp.Tests.Client.Helpers;
using Xunit;

namespace AzureMcp.Tests.Areas.Extension.LiveTests;

[Trait("Area", "Extension")]
[Trait("Category", "Live")]
public class ExtensionCommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output)
    : CommandTestsBase(liveTestFixture, output), IClassFixture<LiveTestFixture>
{
    [Fact]
    public async Task Should_GenerateAzCliCommand_Successfully()
    {
        var result = await CallToolAsync(
            "azmcp_extension_az",
            new()
            {
                { "intent", "List all resource groups" }
            });

        Assert.True(result.HasValue, "Tool call did not return a value.");
        Assert.Equal(JsonValueKind.String, result.Value.ValueKind);
        Assert.Contains("az group list", result.Value.GetString(), StringComparison.OrdinalIgnoreCase);
    }
}
