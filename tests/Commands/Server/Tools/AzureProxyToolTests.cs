// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Areas.Server.Commands.Tools;
using AzureMcp.Commands.Server.Tools;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Commands.Server.Tools
{
    public class AzureProxyToolTests
    {
        private readonly ILogger<AzureProxyTool> _logger = Substitute.For<ILogger<AzureProxyTool>>();
        private readonly IMcpClientService _mcpClientService;
        private readonly AzureProxyTool _azureProxyTool;

        public AzureProxyToolTests()
        {
            // Setup a fake IMcpClientService
            var fakeClient = Substitute.For<IMcpClient>();

            // Mock SendRequestAsync for 'tools/list' to return a fake tool list
            fakeClient
                .SendRequestAsync(
                    Arg.Is<JsonRpcRequest>(req => req.Method == "tools/list"),
                    Arg.Any<CancellationToken>())
                .Returns(callInfo =>
                {
                    var result = new ListToolsResult
                    {
                        Tools = [
                            new() { Name = "account_list", Description = "Lists storage accounts" },
                            new() { Name = "container_list", Description = "Lists containers in storage account" }
                        ]
                    };
                    // Use default serialization for test
                    var json = JsonSerializer.SerializeToNode(result);
                    return Task.FromResult(new JsonRpcResponse { Result = json });
                });

            // Mock SendRequestAsync for 'tools/call' to return a fake command result
            fakeClient
                .SendRequestAsync(
                    Arg.Is<JsonRpcRequest>(req => req.Method == "tools/call"),
                    Arg.Any<CancellationToken>())
                .Returns(callInfo =>
                {
                    var result = new CallToolResult
                    {
                        Content = [
                            new TextContentBlock {
                                Text = """
                                    storage account 1
                                    storage account 2
                                    """ }
                        ]
                    };
                    var json = JsonSerializer.SerializeToNode(result);
                    return Task.FromResult(new JsonRpcResponse { Result = json });
                });

            var fakeService = Substitute.For<IMcpClientService>();
            fakeService.ListProviderMetadata().Returns([
                new McpServerMetadata { Id = "storage", Name = "storage", Description = "Contains tools for storage" },
                new McpServerMetadata { Id = "keyvault", Name = "keyvault", Description = "Contains tools for keyvault" }
            ]);

            fakeService.GetProviderClientAsync(Arg.Any<string>(), Arg.Any<McpClientOptions>()).Returns(callInfo =>
            {
                var name = callInfo.Arg<string>();
                return name == "storage" ? Task.FromResult<IMcpClient?>(fakeClient) : Task.FromResult<IMcpClient?>(null);
            });

            _mcpClientService = fakeService;
            _azureProxyTool = new AzureProxyTool(_logger, _mcpClientService);
        }

        [Fact]
        public void ProtocolTool_HasExpectedNameAndSchema()
        {
            var protocolTool = _azureProxyTool.ProtocolTool;
            Assert.Equal("azure", protocolTool.Name);
            Assert.Equal(JsonValueKind.Object, protocolTool.InputSchema.ValueKind);
        }

        [Fact]
        public async Task InvokeAsync_RootLearnFlow_ReturnsRootToolList()
        {
            var fakeServer = Substitute.For<IMcpServer>();
            var request = new RequestContext<CallToolRequestParams>(fakeServer)
            {
                Params = new CallToolRequestParams
                {
                    Name = "root",
                    Arguments = new Dictionary<string, JsonElement>
                    {
                        ["learn"] = JsonDocument.Parse("true").RootElement
                    }
                }
            };

            var result = await _azureProxyTool.InvokeAsync(request, CancellationToken.None);
            Assert.Contains("list of tools", (result.Content[0] as TextContentBlock)?.Text, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("storage", (result.Content[0] as TextContentBlock)?.Text, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("keyvault", (result.Content[0] as TextContentBlock)?.Text, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task InvokeAsync_ToolLearnFlow_ReturnsChildToolList()
        {
            var fakeServer = Substitute.For<IMcpServer>();
            var request = new RequestContext<CallToolRequestParams>(fakeServer)
            {
                Params = new CallToolRequestParams
                {
                    Name = "child",
                    Arguments = new Dictionary<string, JsonElement>
                    {
                        ["learn"] = JsonDocument.Parse("true").RootElement,
                        ["tool"] = JsonDocument.Parse("\"storage\"").RootElement
                    }
                }
            };
            var result = await _azureProxyTool.InvokeAsync(request, CancellationToken.None);
            Assert.Contains("account_list", (result.Content[0] as TextContentBlock)?.Text, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("container_list", (result.Content[0] as TextContentBlock)?.Text, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task InvokeAsync_CommandFlow_ReturnsCommandResultOrError()
        {
            var fakeServer = Substitute.For<IMcpServer>();
            var request = new RequestContext<CallToolRequestParams>(fakeServer)
            {
                Params = new CallToolRequestParams
                {
                    Name = "command",
                    Arguments = new Dictionary<string, JsonElement>
                    {
                        ["tool"] = JsonDocument.Parse("\"storage\"").RootElement,
                        ["command"] = JsonDocument.Parse("\"account_list\"").RootElement,
                        ["parameters"] = JsonDocument.Parse("{} ").RootElement
                    }
                }
            };
            var result = await _azureProxyTool.InvokeAsync(request, CancellationToken.None);
            Assert.Contains("storage account 1", (result.Content[0] as TextContentBlock)?.Text, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("storage account 2", (result.Content[0] as TextContentBlock)?.Text, StringComparison.OrdinalIgnoreCase);
        }
    }
}
