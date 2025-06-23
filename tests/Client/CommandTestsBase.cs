// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using AzureMcp.Tests.Client.Helpers;
using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol;
using Xunit;

namespace AzureMcp.Tests.Client;

public abstract class CommandTestsBase(LiveTestFixture liveTestFixture, ITestOutputHelper output) : IDisposable
{
    protected const string TenantNameReason = "Service principals cannot use TenantName for lookup";

    protected IMcpClient Client { get; } = liveTestFixture.Client;
    protected LiveTestSettings Settings { get; } = liveTestFixture.Settings;
    protected StringBuilder FailureOutput { get; } = new();
    protected ITestOutputHelper Output { get; } = output;

    protected async Task<JsonElement?> CallToolAsync(string command, Dictionary<string, object?> parameters)
    {
        // Output will be streamed, so if we're not in debug mode, hold the debug output for logging in the failure case
        Action<string> writeOutput = Settings.DebugOutput
            ? s => Output.WriteLine(s)
            : s => FailureOutput.AppendLine(s);

        writeOutput($"request: {JsonSerializer.Serialize(new { command, parameters })}");

        var result = await Client.CallToolAsync(command, parameters);

        var content = GetApplicationJsonText(result.Content);
        if (string.IsNullOrWhiteSpace(content))
        {
            Output.WriteLine($"response: {JsonSerializer.Serialize(result)}");
            throw new Exception("No JSON content found in the response.");
        }

        var root = JsonSerializer.Deserialize<JsonElement>(content!);
        if (root.ValueKind != JsonValueKind.Object)
        {
            Output.WriteLine($"response: {JsonSerializer.Serialize(result)}");
            throw new Exception("Invalid JSON response.");
        }

        // Remove the `args` property and log the content
        var trimmed = root.Deserialize<JsonObject>()!;
        trimmed.Remove("args");
        writeOutput($"response content: {trimmed.ToJsonString(new JsonSerializerOptions { WriteIndented = true })}");

        return root.TryGetProperty("results", out var property) ? property : null;
    }

    public void Dispose()
    {
        if (!Settings.DebugOutput && TestContext.Current.TestState?.Result == TestResult.Failed && FailureOutput.Length > 0)
        {
            Output.WriteLine(FailureOutput.ToString());
        }
    }

    private static string? GetApplicationJsonText(IList<ContentBlock> contents)
    {
        foreach (var c in contents)
        {
            if (c is EmbeddedResourceBlock { Resource: TextResourceContents { MimeType: "application/json" } text })
            {
                return text.Text;
            }
        }

        return null;
    }
}
