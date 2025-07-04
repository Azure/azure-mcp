// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using System.Threading.Tasks;
using AzureMcp.Areas.Server.Commands.ToolLoading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Areas.Server.UnitTests.Commands.ToolLoading;

[Trait("Area", "Server")]
public class CompositeToolLoaderTests
{
    private static IServiceProvider CreateServiceProvider()
    {
        return new ServiceCollection().AddLogging().BuildServiceProvider();
    }

    private static RequestContext<ListToolsRequestParams> CreateListToolsRequest()
    {
        var mockServer = Substitute.For<IMcpServer>();
        return new RequestContext<ListToolsRequestParams>(mockServer)
        {
            Params = new ListToolsRequestParams()
        };
    }

    private static RequestContext<CallToolRequestParams> CreateCallToolRequest(string toolName, IReadOnlyDictionary<string, JsonElement>? arguments = null)
    {
        var mockServer = Substitute.For<IMcpServer>();
        return new RequestContext<CallToolRequestParams>(mockServer)
        {
            Params = new CallToolRequestParams
            {
                Name = toolName,
                Arguments = arguments ?? new Dictionary<string, JsonElement>()
            }
        };
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

    [Fact]
    public void ListToolsHandler_WithEmptyToolLoaderList_ThrowsArgumentException()
    {
        var serviceProvider = CreateServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<CompositeToolLoader>>();
        var toolLoaders = new List<IToolLoader>();

        var exception = Assert.Throws<ArgumentException>(() =>
            new CompositeToolLoader(toolLoaders, logger));

        Assert.Equal("toolLoaders", exception.ParamName);
        Assert.Contains("At least one tool loader must be provided", exception.Message);
    }

    [Fact]
    public async Task ListToolsHandler_WithSingleToolLoader_ReturnsToolsFromLoader()
    {
        var serviceProvider = CreateServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<CompositeToolLoader>>();

        var mockLoader = Substitute.For<IToolLoader>();
        var expectedTools = new List<Tool>
        {
            CreateTestTool("tool1", "First tool"),
            CreateTestTool("tool2", "Second tool")
        };
        mockLoader.ListToolsHandler(Arg.Any<RequestContext<ListToolsRequestParams>>(), Arg.Any<CancellationToken>())
            .Returns(new ListToolsResult { Tools = expectedTools });

        var toolLoaders = new List<IToolLoader> { mockLoader };
        var toolLoader = new CompositeToolLoader(toolLoaders, logger);
        var request = CreateListToolsRequest();

        var result = await toolLoader.ListToolsHandler(request, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotNull(result.Tools);
        Assert.Equal(2, result.Tools.Count);
        Assert.Equal("tool1", result.Tools[0].Name);
        Assert.Equal("tool2", result.Tools[1].Name);
        Assert.Equal("First tool", result.Tools[0].Description);
        Assert.Equal("Second tool", result.Tools[1].Description);
    }

    [Fact]
    public async Task ListToolsHandler_WithMultipleToolLoaders_CombinesAllTools()
    {
        var serviceProvider = CreateServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<CompositeToolLoader>>();

        var mockLoader1 = Substitute.For<IToolLoader>();
        var mockLoader2 = Substitute.For<IToolLoader>();

        mockLoader1.ListToolsHandler(Arg.Any<RequestContext<ListToolsRequestParams>>(), Arg.Any<CancellationToken>())
            .Returns(new ListToolsResult { Tools = new List<Tool> { CreateTestTool("tool1") } });

        mockLoader2.ListToolsHandler(Arg.Any<RequestContext<ListToolsRequestParams>>(), Arg.Any<CancellationToken>())
            .Returns(new ListToolsResult { Tools = new List<Tool> { CreateTestTool("tool2"), CreateTestTool("tool3") } });

        var toolLoaders = new List<IToolLoader> { mockLoader1, mockLoader2 };
        var toolLoader = new CompositeToolLoader(toolLoaders, logger);
        var request = CreateListToolsRequest();

        var result = await toolLoader.ListToolsHandler(request, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotNull(result.Tools);
        Assert.Equal(3, result.Tools.Count);
        Assert.Contains(result.Tools, t => t.Name == "tool1");
        Assert.Contains(result.Tools, t => t.Name == "tool2");
        Assert.Contains(result.Tools, t => t.Name == "tool3");
    }

    [Fact]
    public async Task ListToolsHandler_WithToolLoaderReturningNull_ThrowsInvalidOperationException()
    {
        var serviceProvider = CreateServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<CompositeToolLoader>>();

        var mockLoader = Substitute.For<IToolLoader>();
        mockLoader.ListToolsHandler(Arg.Any<RequestContext<ListToolsRequestParams>>(), Arg.Any<CancellationToken>())
            .Returns((ListToolsResult)null!);

        var toolLoaders = new List<IToolLoader> { mockLoader };
        var toolLoader = new CompositeToolLoader(toolLoaders, logger);
        var request = CreateListToolsRequest();

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await toolLoader.ListToolsHandler(request, CancellationToken.None));

        Assert.Equal("Tool loader returned null response.", exception.Message);
    }

    [Fact]
    public void CallToolHandler_WithEmptyToolLoaderList_ThrowsArgumentException()
    {
        var serviceProvider = CreateServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<CompositeToolLoader>>();
        var toolLoaders = new List<IToolLoader>();

        var exception = Assert.Throws<ArgumentException>(() =>
            new CompositeToolLoader(toolLoaders, logger));

        Assert.Equal("toolLoaders", exception.ParamName);
        Assert.Contains("At least one tool loader must be provided", exception.Message);
    }

    [Fact]
    public async Task CallToolHandler_WithUnknownTool_ReturnsErrorResult()
    {
        var serviceProvider = CreateServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<CompositeToolLoader>>();

        // Setup a loader with a different tool to populate the map
        var mockLoader = Substitute.For<IToolLoader>();
        mockLoader.ListToolsHandler(Arg.Any<RequestContext<ListToolsRequestParams>>(), Arg.Any<CancellationToken>())
            .Returns(new ListToolsResult { Tools = new List<Tool> { CreateTestTool("existing-tool") } });

        var toolLoaders = new List<IToolLoader> { mockLoader };
        var toolLoader = new CompositeToolLoader(toolLoaders, logger);

        // First populate the tool map by calling ListToolsHandler
        var listRequest = CreateListToolsRequest();
        await toolLoader.ListToolsHandler(listRequest, CancellationToken.None);

        // Now try to call an unknown tool
        var callRequest = CreateCallToolRequest("unknown-tool");
        var result = await toolLoader.CallToolHandler(callRequest, CancellationToken.None);

        Assert.NotNull(result);
        Assert.True(result.IsError);
        Assert.NotNull(result.Content);
        Assert.Single(result.Content);
        var textContent = Assert.IsType<TextContentBlock>(result.Content[0]);
        Assert.Equal("The tool ${request.Params.Name} was not found", textContent.Text);
    }

    [Fact]
    public async Task CallToolHandler_WithKnownTool_DelegatesToCorrectLoader()
    {
        var serviceProvider = CreateServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<CompositeToolLoader>>();

        var mockLoader = Substitute.For<IToolLoader>();
        var expectedResult = new CallToolResult
        {
            Content = new List<ContentBlock> { new TextContentBlock { Text = "Tool executed successfully" } },
            IsError = false
        };

        mockLoader.ListToolsHandler(Arg.Any<RequestContext<ListToolsRequestParams>>(), Arg.Any<CancellationToken>())
            .Returns(new ListToolsResult { Tools = new List<Tool> { CreateTestTool("test-tool") } });

        mockLoader.CallToolHandler(Arg.Any<RequestContext<CallToolRequestParams>>(), Arg.Any<CancellationToken>())
            .Returns(expectedResult);

        var toolLoaders = new List<IToolLoader> { mockLoader };
        var toolLoader = new CompositeToolLoader(toolLoaders, logger);

        // First populate the tool map
        var listRequest = CreateListToolsRequest();
        await toolLoader.ListToolsHandler(listRequest, CancellationToken.None);

        // Now call the known tool
        var callRequest = CreateCallToolRequest("test-tool");
        var result = await toolLoader.CallToolHandler(callRequest, CancellationToken.None);

        Assert.NotNull(result);
        Assert.False(result.IsError);
        Assert.Equal(expectedResult.Content, result.Content);

        // Verify the mock loader was called with the correct request
        await mockLoader.Received(1).CallToolHandler(callRequest, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task CallToolHandler_WithMultipleLoaders_DelegatesToCorrectLoader()
    {
        var serviceProvider = CreateServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<CompositeToolLoader>>();

        var mockLoader1 = Substitute.For<IToolLoader>();
        var mockLoader2 = Substitute.For<IToolLoader>();

        var expectedResult = new CallToolResult
        {
            Content = new List<ContentBlock> { new TextContentBlock { Text = "Tool2 executed" } },
            IsError = false
        };

        mockLoader1.ListToolsHandler(Arg.Any<RequestContext<ListToolsRequestParams>>(), Arg.Any<CancellationToken>())
            .Returns(new ListToolsResult { Tools = new List<Tool> { CreateTestTool("tool1") } });

        mockLoader2.ListToolsHandler(Arg.Any<RequestContext<ListToolsRequestParams>>(), Arg.Any<CancellationToken>())
            .Returns(new ListToolsResult { Tools = new List<Tool> { CreateTestTool("tool2") } });

        mockLoader2.CallToolHandler(Arg.Any<RequestContext<CallToolRequestParams>>(), Arg.Any<CancellationToken>())
            .Returns(expectedResult);

        var toolLoaders = new List<IToolLoader> { mockLoader1, mockLoader2 };
        var toolLoader = new CompositeToolLoader(toolLoaders, logger);

        // First populate the tool map
        var listRequest = CreateListToolsRequest();
        await toolLoader.ListToolsHandler(listRequest, CancellationToken.None);

        // Call tool2 which should be handled by mockLoader2
        var callRequest = CreateCallToolRequest("tool2");
        var result = await toolLoader.CallToolHandler(callRequest, CancellationToken.None);

        Assert.NotNull(result);
        Assert.False(result.IsError);
        Assert.Equal(expectedResult.Content, result.Content);

        // Verify only mockLoader2 was called for CallToolHandler
        await mockLoader1.DidNotReceive().CallToolHandler(Arg.Any<RequestContext<CallToolRequestParams>>(), Arg.Any<CancellationToken>());
        await mockLoader2.Received(1).CallToolHandler(callRequest, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task CallToolHandler_WithNullParams_ReturnsErrorResult()
    {
        var serviceProvider = CreateServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<CompositeToolLoader>>();
        var mockToolLoader = Substitute.For<IToolLoader>();
        var toolLoaders = new List<IToolLoader> { mockToolLoader };

        var toolLoader = new CompositeToolLoader(toolLoaders, logger);
        var mockServer = Substitute.For<IMcpServer>();
        var request = new RequestContext<CallToolRequestParams>(mockServer)
        {
            Params = null
        };

        var result = await toolLoader.CallToolHandler(request, CancellationToken.None);

        Assert.NotNull(result);
        Assert.True(result.IsError);
        Assert.NotNull(result.Content);
        Assert.Single(result.Content);
        var textContent = Assert.IsType<TextContentBlock>(result.Content[0]);
        Assert.Equal("Cannot call tools with null parameters.", textContent.Text);
    }

    [Fact]
    public async Task ListToolsHandler_WithSingleEmptyToolLoader_ReturnsEmptyResult()
    {
        var serviceProvider = CreateServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<CompositeToolLoader>>();
        var mockToolLoader = Substitute.For<IToolLoader>();
        mockToolLoader.ListToolsHandler(Arg.Any<RequestContext<ListToolsRequestParams>>(), Arg.Any<CancellationToken>())
            .Returns(new ListToolsResult { Tools = new List<Tool>() });

        var toolLoaders = new List<IToolLoader> { mockToolLoader };
        var toolLoader = new CompositeToolLoader(toolLoaders, logger);
        var request = CreateListToolsRequest();

        var result = await toolLoader.ListToolsHandler(request, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotNull(result.Tools);
        Assert.Empty(result.Tools);
    }
}
