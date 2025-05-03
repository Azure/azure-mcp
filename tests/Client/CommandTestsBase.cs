// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Tests.Client.Helpers;
using ModelContextProtocol.Client;
using Xunit;

namespace AzureMcp.Tests.Client;

public abstract class CommandTestsBase(McpClientFixture mcpClient, LiveTestSettingsFixture liveTestSettings, ITestOutputHelper output)
{
    protected IMcpClient Client { get; } = mcpClient.Client;
    protected LiveTestSettings Settings { get; } = liveTestSettings.Settings;
    protected ITestOutputHelper Output { get; } = output;

    protected async Task<JsonElement> CallToolAsync(string command, Dictionary<string, object?> parameters)
    {
        Output.WriteLine($"request: {JsonSerializer.Serialize(new { command, parameters })}");

        var result = await Client.CallToolAsync(command, parameters);

        var content = result.Content.FirstOrDefault(c => c.MimeType == "application/json")?.Text;
        if (string.IsNullOrWhiteSpace(content))
        {
            Output.WriteLine($"response: {JsonSerializer.Serialize(result)}");
            throw new Exception("No JSON content found in the response.");
        }

        Output.WriteLine($"response content: {content}");

        var root = JsonSerializer.Deserialize<JsonElement>(content!);
        return root.GetProperty("results");
    }
}
