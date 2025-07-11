// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using AzureMcp.Tests.Client.Helpers;
using ModelContextProtocol.Client;
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

        var content = McpTestUtilities.GetFirstText(result.Content);
        if (string.IsNullOrWhiteSpace(content))
        {
            writeOutput($"response: {JsonSerializer.Serialize(result)}");
            throw new Exception("No JSON content found in the response.");
        }

        JsonElement root;
        try
        {
            root = JsonSerializer.Deserialize<JsonElement>(content!);
            if (root.ValueKind != JsonValueKind.Object)
            {
                throw new Exception("Invalid JSON response.");
            }

            // Remove the `args` property and log the content
            var trimmed = root.Deserialize<JsonObject>()!;
            trimmed.Remove("args");
            writeOutput($"response: {trimmed.ToJsonString(new JsonSerializerOptions { WriteIndented = true })}");
        }
        catch (Exception ex)
        {
            // If we can't json parse the content as a JsonObject, log the content and throw an exception
            writeOutput($"response: {content}");
            throw new Exception("Failed to deserialize JSON response.", ex);
        }

        return root.TryGetProperty("results", out var property) ? property : null;
    }

    public void Dispose()
    {
        // Failure output may contain request and response details that should be output for failed tests.
        if (TestContext.Current.TestState?.Result == TestResult.Failed && FailureOutput.Length > 0)
        {
            Output.WriteLine(FailureOutput.ToString());
        }
    }
}
