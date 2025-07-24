// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Areas.Server;
using AzureMcp.Areas.Server.Commands.ToolLoading;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Areas.Server.IntegrationTests.Commands.ToolLoading;

/// <summary>
/// Integration tests to validate that tool loaders return valid JSON when tools are not found.
/// This fixes the issue where test frameworks expect JSON but get plain text error messages.
/// </summary>
[Trait("Area", "Server")]
public sealed class ToolLoaderJsonIntegrationTests
{
    /// <summary>
    /// Validates that CompositeToolLoader returns valid JSON when a tool cannot be found.
    /// This test simulates the scenario from the problem statement where the test framework
    /// tries to parse the response content as JSON.
    /// </summary>
    [Fact]
    public async Task CompositeToolLoader_UnknownTool_ReturnsValidJson()
    {
        // Arrange
        var mockLogger = Substitute.For<ILogger<CompositeToolLoader>>();

        // Create a mock loader with a known tool to satisfy the requirement of at least one loader
        var mockLoader = Substitute.For<IToolLoader>();
        mockLoader.ListToolsHandler(Arg.Any<RequestContext<ListToolsRequestParams>>(), Arg.Any<CancellationToken>())
            .Returns(new ListToolsResult { Tools = new List<Tool> { CreateTestTool("existing-tool") } });

        var mockToolLoaders = new List<IToolLoader> { mockLoader };
        var compositeLoader = new CompositeToolLoader(mockToolLoaders, mockLogger);

        // Initialize the tool map by calling ListToolsHandler
        var mockServer = Substitute.For<IMcpServer>();
        var listRequest = new RequestContext<ListToolsRequestParams>(mockServer)
        {
            Params = new ListToolsRequestParams()
        };
        await compositeLoader.ListToolsHandler(listRequest, CancellationToken.None);

        var request = new RequestContext<CallToolRequestParams>(mockServer)
        {
            Params = new CallToolRequestParams
            {
                Name = "azmcp-appconfig-kv-set", // Tool name from the problem statement
                Arguments = new Dictionary<string, JsonElement>()
            }
        };

        // Act
        var result = await compositeLoader.CallToolHandler(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsError, "Expected error result for unknown tool");
        Assert.NotNull(result.Content);
        Assert.Single(result.Content);

        var textContent = result.Content.First() as TextContentBlock;
        Assert.NotNull(textContent);

        // This is the critical test: the response should be valid JSON
        // Previously this would fail with "JsonException: 'T' is an invalid start of a value"
        // because the response was plain text like "The tool azmcp-appconfig-kv-set was not found"
        JsonElement root;
        var parseException = Record.Exception(() => root = JsonSerializer.Deserialize<JsonElement>(textContent.Text));
        Assert.Null(parseException);

        // Verify the JSON structure
        root = JsonSerializer.Deserialize<JsonElement>(textContent.Text);
        Assert.Equal(JsonValueKind.Object, root.ValueKind);
        Assert.True(root.TryGetProperty("error", out var errorElement));

        var errorMessage = errorElement.GetString();
        Assert.NotNull(errorMessage);
        Assert.Contains("azmcp-appconfig-kv-set", errorMessage);
        Assert.Contains("was not found", errorMessage);
    }

    /// <summary>
    /// Test helper to simulate how the CommandTestsBase.CallToolAsync processes responses.
    /// This mimics the logic in the test framework that was failing.
    /// </summary>
    [Fact]
    public async Task SimulateCommandTestBaseScenario()
    {
        // Arrange - simulate the failing test scenario
        var mockLogger = Substitute.For<ILogger<CompositeToolLoader>>();

        // Create a mock loader with a known tool to satisfy the requirement of at least one loader
        var mockLoader = Substitute.For<IToolLoader>();
        mockLoader.ListToolsHandler(Arg.Any<RequestContext<ListToolsRequestParams>>(), Arg.Any<CancellationToken>())
            .Returns(new ListToolsResult { Tools = new List<Tool> { CreateTestTool("existing-tool") } });

        var mockToolLoaders = new List<IToolLoader> { mockLoader };
        var compositeLoader = new CompositeToolLoader(mockToolLoaders, mockLogger);

        // Initialize the tool map by calling ListToolsHandler
        var mockServer = Substitute.For<IMcpServer>();
        var listRequest = new RequestContext<ListToolsRequestParams>(mockServer)
        {
            Params = new ListToolsRequestParams()
        };
        await compositeLoader.ListToolsHandler(listRequest, CancellationToken.None);

        var request = new RequestContext<CallToolRequestParams>(mockServer)
        {
            Params = new CallToolRequestParams
            {
                Name = "azmcp-appconfig-kv-set",
                Arguments = new Dictionary<string, JsonElement>()
            }
        };

        // Act - simulate the tool call
        var result = await compositeLoader.CallToolHandler(request, CancellationToken.None);

        // Get first text content (similar to McpTestUtilities.GetFirstText)
        var textContent = result.Content.First() as TextContentBlock;
        Assert.NotNull(textContent);
        string? content = textContent.Text;

        // This is what CommandTestsBase.CallToolAsync does that was failing:
        // It tries to deserialize the text content as JSON
        JsonElement root;
        var deserializeException = Record.Exception(() => root = JsonSerializer.Deserialize<JsonElement>(content!));

        // The fix ensures this no longer throws a JsonException
        Assert.Null(deserializeException);

        // Verify we can process it like the test framework expects
        root = JsonSerializer.Deserialize<JsonElement>(content!);
        Assert.Equal(JsonValueKind.Object, root.ValueKind);

        // The test framework looks for a "results" property, but for errors we have "error"
        // This is fine because error responses should have IsError=true
        Assert.True(result.IsError);
        Assert.True(root.TryGetProperty("error", out var errorProperty));
    }

    private static Tool CreateTestTool(string name, string description = "Test tool")
    {
        return new Tool
        {
            Name = name,
            Description = description,
            InputSchema = JsonDocument.Parse("""
                {
                    "type": "object",
                    "properties": {}
                }
                """).RootElement
        };
    }
}
