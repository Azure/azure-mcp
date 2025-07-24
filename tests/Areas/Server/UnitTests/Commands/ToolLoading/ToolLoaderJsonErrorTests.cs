// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Areas.Server.Commands.ToolLoading;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Areas.Server.UnitTests.Commands.ToolLoading;

[Trait("Area", "Server")]
public sealed class ToolLoaderJsonErrorTests
{
    [Fact]
    public async Task CompositeToolLoader_ShouldReturnValidJsonForUnknownTool()
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
                Name = "non_existent_tool",
                Arguments = new Dictionary<string, JsonElement>()
            }
        };

        // Act
        var result = await compositeLoader.CallToolHandler(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsError);
        Assert.NotNull(result.Content);
        Assert.Single(result.Content);

        var textContent = result.Content.First() as TextContentBlock;
        Assert.NotNull(textContent);

        // Verify the response is valid JSON
        Assert.True(IsValidJson(textContent.Text), $"Response should be valid JSON but was: {textContent.Text}");

        // Verify the JSON contains the error message
        using var doc = JsonDocument.Parse(textContent.Text);
        Assert.True(doc.RootElement.TryGetProperty("error", out var errorElement));
        Assert.Contains("non_existent_tool", errorElement.GetString());
        Assert.Contains("was not found", errorElement.GetString());
    }

    private static bool IsValidJson(string? json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return false;

        try
        {
            JsonDocument.Parse(json);
            return true;
        }
        catch (JsonException)
        {
            return false;
        }
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
