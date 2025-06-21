// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Areas.Server.Commands.Tools;
using AzureMcp.Commands.Server;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Commands.Server;

public class ProxyToolOperationsTests
{
    [Fact]
    public async Task ListToolsHandler_ShouldReturnToolsFromProviderMetadata()
    {
        // Arrange
        var mockMcpClientService = Substitute.For<IMcpClientService>();
        var mockLogger = Substitute.For<ILogger<ProxyToolOperations>>();
        var mockServer = Substitute.For<IMcpServer>();

        var expectedMetadata = new List<McpServerMetadata>
        {
            new() { Id = "storage", Name = "storage", Description = "Azure Storage operations" },
            new() { Id = "keyvault", Name = "keyvault", Description = "Azure Key Vault operations" }
        };

        mockMcpClientService.ListProviderMetadata().Returns(expectedMetadata);

        var proxyToolOperations = new ProxyToolOperations(mockMcpClientService, mockLogger);
        var request = new RequestContext<ListToolsRequestParams>(mockServer)
        {
            Params = new ListToolsRequestParams()
        };

        // Act
        var result = await proxyToolOperations.ListToolsHandler(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Tools.Count);

        var storageToolResult = result.Tools.FirstOrDefault(t => t.Name == "storage");
        Assert.NotNull(storageToolResult);
        Assert.Equal("storage", storageToolResult.Name);
        Assert.Contains("Azure Storage operations", storageToolResult.Description);

        var keyvaultToolResult = result.Tools.FirstOrDefault(t => t.Name == "keyvault");
        Assert.NotNull(keyvaultToolResult);
        Assert.Equal("keyvault", keyvaultToolResult.Name);
        Assert.Contains("Azure Key Vault operations", keyvaultToolResult.Description);

        // Verify that the mock was called
        mockMcpClientService.Received(1).ListProviderMetadata();
    }

    [Fact]
    public async Task ListToolsHandler_WithEmptyMetadata_ShouldReturnEmptyToolsList()
    {
        // Arrange
        var mockMcpClientService = Substitute.For<IMcpClientService>();
        var mockLogger = Substitute.For<ILogger<ProxyToolOperations>>();
        var mockServer = Substitute.For<IMcpServer>();

        mockMcpClientService.ListProviderMetadata().Returns(new List<McpServerMetadata>());

        var proxyToolOperations = new ProxyToolOperations(mockMcpClientService, mockLogger);
        var request = new RequestContext<ListToolsRequestParams>(mockServer)
        {
            Params = new ListToolsRequestParams()
        };

        // Act
        var result = await proxyToolOperations.ListToolsHandler(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Tools);

        // Verify that the mock was called
        mockMcpClientService.Received(1).ListProviderMetadata();
    }

    [Fact]
    public async Task CallToolHandler_WithNullToolName_ShouldThrowArgumentNullException()
    {
        // Arrange
        var mockMcpClientService = Substitute.For<IMcpClientService>();
        var mockLogger = Substitute.For<ILogger<ProxyToolOperations>>();
        var mockServer = Substitute.For<IMcpServer>();

        var proxyToolOperations = new ProxyToolOperations(mockMcpClientService, mockLogger);
        var request = new RequestContext<CallToolRequestParams>(mockServer)
        {
            Params = new CallToolRequestParams
            {
                Name = null!,
                Arguments = new Dictionary<string, JsonElement>()
            }
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            proxyToolOperations.CallToolHandler(request, CancellationToken.None).AsTask());
    }

    [Fact]
    public async Task CallToolHandler_WithEmptyToolName_ShouldThrowArgumentNullException()
    {
        // Arrange
        var mockMcpClientService = Substitute.For<IMcpClientService>();
        var mockLogger = Substitute.For<ILogger<ProxyToolOperations>>();
        var mockServer = Substitute.For<IMcpServer>();

        var proxyToolOperations = new ProxyToolOperations(mockMcpClientService, mockLogger);
        var request = new RequestContext<CallToolRequestParams>(mockServer)
        {
            Params = new CallToolRequestParams
            {
                Name = "",
                Arguments = new Dictionary<string, JsonElement>()
            }
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            proxyToolOperations.CallToolHandler(request, CancellationToken.None).AsTask());
    }
    [Fact]
    public async Task CallToolHandler_WithLearnFlag_ShouldReturnLearnResponse()
    {
        // Arrange
        var mockMcpClientService = Substitute.For<IMcpClientService>();
        var mockLogger = Substitute.For<ILogger<ProxyToolOperations>>();
        var mockServer = Substitute.For<IMcpServer>();
        var mockClient = Substitute.For<IMcpClient>();

        // Mock the JSON-RPC SendRequestAsync method for tools/list
        mockClient
            .SendRequestAsync(
                Arg.Is<JsonRpcRequest>(req => req.Method == "tools/list"),
                Arg.Any<CancellationToken>())
            .Returns(callInfo =>
            {
                var result = new ListToolsResult
                {
                    Tools = [
                        new() { Name = "account-list", Description = "List storage accounts" },
                        new() { Name = "container-list", Description = "List containers" }
                    ]
                };
                var json = JsonSerializer.SerializeToNode(result);
                return Task.FromResult(new JsonRpcResponse { Result = json });
            });

        mockMcpClientService.GetProviderClientAsync("storage", Arg.Any<McpClientOptions>())
            .Returns(mockClient);

        var proxyToolOperations = new ProxyToolOperations(mockMcpClientService, mockLogger);
        var request = new RequestContext<CallToolRequestParams>(mockServer)
        {
            Params = new CallToolRequestParams
            {
                Name = "storage",
                Arguments = new Dictionary<string, JsonElement>
                {
                    ["learn"] = JsonDocument.Parse("true").RootElement
                }
            }
        };

        // Act
        var result = await proxyToolOperations.CallToolHandler(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result.Content);
        Assert.Equal("text", result.Content[0].Type);
        Assert.Contains("account-list", (result.Content[0] as TextContentBlock)?.Text);
        Assert.Contains("container-list", (result.Content[0] as TextContentBlock)?.Text);
    }

    [Fact]
    public async Task CallToolHandler_WithIntentButNoCommand_ShouldTriggerLearnMode()
    {
        // Arrange
        var mockMcpClientService = Substitute.For<IMcpClientService>();
        var mockLogger = Substitute.For<ILogger<ProxyToolOperations>>();
        var mockServer = Substitute.For<IMcpServer>();
        var mockClient = Substitute.For<IMcpClient>();

        // Mock the JSON-RPC SendRequestAsync method for tools/list
        mockClient
            .SendRequestAsync(
                Arg.Is<JsonRpcRequest>(req => req.Method == "tools/list"),
                Arg.Any<CancellationToken>())
            .Returns(callInfo =>
            {
                var result = new ListToolsResult
                {
                    Tools = [
                        new() { Name = "account-list", Description = "List storage accounts" }
                    ]
                };
                var json = JsonSerializer.SerializeToNode(result);
                return Task.FromResult(new JsonRpcResponse { Result = json });
            });

        mockMcpClientService.GetProviderClientAsync("storage", Arg.Any<McpClientOptions>())
            .Returns(mockClient);

        var proxyToolOperations = new ProxyToolOperations(mockMcpClientService, mockLogger);
        var request = new RequestContext<CallToolRequestParams>(mockServer)
        {
            Params = new CallToolRequestParams
            {
                Name = "storage",
                Arguments = new Dictionary<string, JsonElement>
                {
                    ["intent"] = JsonDocument.Parse("\"list my storage accounts\"").RootElement
                }
            }
        };

        // Act
        var result = await proxyToolOperations.CallToolHandler(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result.Content);
        Assert.Equal("text", result.Content[0].Type);
        Assert.Contains("account-list", (result.Content[0] as TextContentBlock)?.Text);
    }

    [Fact]
    public async Task CallToolHandler_WithNoCommandOrLearn_ShouldReturnHelpMessage()
    {
        // Arrange
        var mockMcpClientService = Substitute.For<IMcpClientService>();
        var mockLogger = Substitute.For<ILogger<ProxyToolOperations>>();
        var mockServer = Substitute.For<IMcpServer>();

        var proxyToolOperations = new ProxyToolOperations(mockMcpClientService, mockLogger);
        var request = new RequestContext<CallToolRequestParams>(mockServer)
        {
            Params = new CallToolRequestParams
            {
                Name = "storage",
                Arguments = new Dictionary<string, JsonElement>()
            }
        };

        // Act
        var result = await proxyToolOperations.CallToolHandler(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result.Content);
        Assert.Equal("text", result.Content[0].Type);
        Assert.Contains("command", (result.Content[0] as TextContentBlock)?.Text);
        Assert.Contains("learn", (result.Content[0] as TextContentBlock)?.Text);
    }
    [Fact]
    public async Task CallToolHandler_WithValidCommand_ShouldCallProviderClient()
    {
        // Arrange
        var mockMcpClientService = Substitute.For<IMcpClientService>();
        var mockLogger = Substitute.For<ILogger<ProxyToolOperations>>();
        var mockServer = Substitute.For<IMcpServer>();
        var mockClient = Substitute.For<IMcpClient>();

        // Mock the JSON-RPC SendRequestAsync method for tools/list
        mockClient
            .SendRequestAsync(
                Arg.Is<JsonRpcRequest>(req => req.Method == "tools/list"),
                Arg.Any<CancellationToken>())
            .Returns(callInfo =>
            {
                var result = new ListToolsResult
                {
                    Tools = [
                        new() { Name = "account-list", Description = "List storage accounts" }
                    ]
                };
                var json = JsonSerializer.SerializeToNode(result);
                return Task.FromResult(new JsonRpcResponse { Result = json });
            });

        // Mock the JSON-RPC SendRequestAsync method for tools/call
        mockClient
            .SendRequestAsync(
                Arg.Is<JsonRpcRequest>(req => req.Method == "tools/call"),
                Arg.Any<CancellationToken>())
            .Returns(callInfo =>
            {
                var result = new CallToolResult
                {
                    Content = [new TextContentBlock { Text = "Storage accounts listed successfully" }]
                };
                var json = JsonSerializer.SerializeToNode(result);
                return Task.FromResult(new JsonRpcResponse { Result = json });
            });

        mockMcpClientService.GetProviderClientAsync("storage", Arg.Any<McpClientOptions>())
            .Returns(mockClient);

        var proxyToolOperations = new ProxyToolOperations(mockMcpClientService, mockLogger);
        var request = new RequestContext<CallToolRequestParams>(mockServer)
        {
            Params = new CallToolRequestParams
            {
                Name = "storage",
                Arguments = new Dictionary<string, JsonElement>
                {
                    ["command"] = JsonDocument.Parse("\"account-list\"").RootElement,
                    ["parameters"] = JsonDocument.Parse("{}").RootElement
                }
            }
        };

        // Act
        var result = await proxyToolOperations.CallToolHandler(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result.Content);
        Assert.Equal("text", result.Content[0].Type);
        Assert.Contains("Storage accounts listed successfully", (result.Content[0] as TextContentBlock)?.Text);

        // Verify the client was called for both tools/list and tools/call
        await mockClient.Received().SendRequestAsync(
            Arg.Is<JsonRpcRequest>(req => req.Method == "tools/list"),
            Arg.Any<CancellationToken>());
        await mockClient.Received().SendRequestAsync(
            Arg.Is<JsonRpcRequest>(req => req.Method == "tools/call"),
            Arg.Any<CancellationToken>());
    }
    [Fact]
    public async Task CallToolHandler_WhenProviderClientReturnsNull_ShouldFallbackToLearn()
    {
        // Arrange
        var mockMcpClientService = Substitute.For<IMcpClientService>();
        var mockLogger = Substitute.For<ILogger<ProxyToolOperations>>();
        var mockServer = Substitute.For<IMcpServer>();

        mockMcpClientService.GetProviderClientAsync("storage", Arg.Any<McpClientOptions>())
            .Returns(Task.FromResult<IMcpClient?>(null));

        var proxyToolOperations = new ProxyToolOperations(mockMcpClientService, mockLogger);
        var request = new RequestContext<CallToolRequestParams>(mockServer)
        {
            Params = new CallToolRequestParams
            {
                Name = "storage",
                Arguments = new Dictionary<string, JsonElement>
                {
                    ["command"] = JsonDocument.Parse("\"account-list\"").RootElement
                }
            }
        };

        // Act
        var result = await proxyToolOperations.CallToolHandler(request, CancellationToken.None);        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result.Content);
        Assert.Equal("text", result.Content[0].Type);
        // Should fallback to learn mode since client provider failed
        Assert.Contains("available command", (result.Content[0] as TextContentBlock)?.Text, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task CallToolHandler_LearnMode_ReadOnly_FiltersToolsByReadOnlyHint()
    {
        // Arrange
        var mockMcpClientService = Substitute.For<IMcpClientService>();
        var mockLogger = Substitute.For<ILogger<ProxyToolOperations>>();
        var mockServer = Substitute.For<IMcpServer>();
        var mockClient = Substitute.For<IMcpClient>();

        // Mock the JSON-RPC SendRequestAsync method for tools/list with a mix of tools
        mockClient
            .SendRequestAsync(
                Arg.Is<JsonRpcRequest>(req => req.Method == "tools/list"),
                Arg.Any<CancellationToken>())
            .Returns(callInfo =>
            {
                var result = new ListToolsResult
                {
                    Tools = [
                        new() { Name = "readonly-tool", Description = "ReadOnly Tool", Annotations = new ToolAnnotations { ReadOnlyHint = true } },
                        new() { Name = "write-tool", Description = "Write Tool", Annotations = new ToolAnnotations { ReadOnlyHint = false } },
                        new() { Name = "no-annotation-tool", Description = "No Annotation Tool", Annotations = null }
                    ]
                };
                var json = JsonSerializer.SerializeToNode(result);
                return Task.FromResult(new JsonRpcResponse { Result = json });
            });

        mockMcpClientService.GetProviderClientAsync("test-tool", Arg.Any<McpClientOptions>())
            .Returns(mockClient);

        var request = new RequestContext<CallToolRequestParams>(mockServer)
        {
            Params = new CallToolRequestParams
            {
                Name = "test-tool",
                Arguments = new Dictionary<string, JsonElement>
                {
                    ["learn"] = JsonDocument.Parse("true").RootElement
                }
            }
        };

        // Act: ReadOnly = false (should return all tools)
        var proxyToolOperationsAll = new ProxyToolOperations(mockMcpClientService, mockLogger)
        {
            ReadOnly = false
        };
        var resultAll = await proxyToolOperationsAll.CallToolHandler(request, CancellationToken.None);
        Assert.NotNull(resultAll);
        Assert.NotEmpty(resultAll.Content);
        var textAll = (resultAll.Content[0] as TextContentBlock)?.Text;
        Assert.Contains("readonly-tool", textAll);
        Assert.Contains("write-tool", textAll);
        Assert.Contains("no-annotation-tool", textAll);

        // Act: ReadOnly = true (should return only readonly tools)
        var proxyToolOperationsReadonly = new ProxyToolOperations(mockMcpClientService, mockLogger)
        {
            ReadOnly = true
        };
        var resultReadonly = await proxyToolOperationsReadonly.CallToolHandler(request, CancellationToken.None);
        Assert.NotNull(resultReadonly);
        Assert.NotEmpty(resultReadonly.Content);
        var textReadonly = (resultReadonly.Content[0] as TextContentBlock)?.Text;
        Assert.Contains("readonly-tool", textReadonly);
        Assert.DoesNotContain("write-tool", textReadonly);
        Assert.DoesNotContain("no-annotation-tool", textReadonly);

        // There should be fewer tools in readonly mode
        int countAll = new[] { "readonly-tool", "write-tool", "no-annotation-tool" }.Count(t => (textAll ?? string.Empty).Contains(t));
        int countReadonly = new[] { "readonly-tool", "write-tool", "no-annotation-tool" }.Count(t => (textReadonly ?? string.Empty).Contains(t));
        Assert.True(countReadonly < countAll, "Readonly mode should return fewer tools.");
    }
}
