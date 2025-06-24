// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Areas.KeyVault.Services;
using AzureMcp.Areas.Server.Commands;
using AzureMcp.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Areas.Server.UnitTests;

[Trait("Area", "Server")]
public class ToolOperationsTest
{
    // https://json-schema.org/understanding-json-schema/reference/type
    private static readonly HashSet<string> s_jsonSchemaDataTypes = new()
    {
        "string",
        "integer",
        "number",
        "boolean",
        "array",
        "null",
        "object"
    };

    private readonly CommandFactory _commandFactory;
    private readonly IServiceProvider _serviceProvider;
    private readonly IKeyVaultService _keyVaultService;
    private readonly ILogger<ToolOperations> _logger;
    private readonly ILogger<CommandFactory> _commandFactoryLogger;
    private readonly IMcpServer _server;

    public ToolOperationsTest()
    {
        _logger = Substitute.For<ILogger<ToolOperations>>();
        _commandFactoryLogger = Substitute.For<ILogger<CommandFactory>>();
        _server = Substitute.For<IMcpServer>();
        _keyVaultService = Substitute.For<IKeyVaultService>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_ => _keyVaultService);

        _serviceProvider = collection.AddLogging().BuildServiceProvider();
        _commandFactory = CommandFactoryHelpers.CreateCommandFactory(_serviceProvider);
    }

    [Fact]
    public async Task GetsAllTools()
    {
        var operations = new ToolOperations(_serviceProvider, _commandFactory, _logger);
        var requestContext = new RequestContext<ListToolsRequestParams>(_server);

        var handler = operations.ToolsCapability.ListToolsHandler;

        Assert.NotNull(handler);

        var result = await handler(requestContext, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Tools);

        foreach (var tool in result.Tools)
        {
            Assert.NotNull(tool);
            Assert.NotNull(tool.Name);
            Assert.NotNull(tool.Description!);
            Assert.NotNull(tool.Annotations);

            Assert.Equal(JsonValueKind.Object, tool.InputSchema.ValueKind);

            foreach (var properties in tool.InputSchema.EnumerateObject())
            {
                if (properties.NameEquals("type"))
                {
                    Assert.Equal("object", properties.Value.GetString());
                }

                if (!properties.NameEquals("properties"))
                {
                    continue;
                }

                var commandArguments = properties.Value.EnumerateObject().ToArray();
                foreach (var argument in commandArguments)
                {
                    var argumentType = argument.Value.GetProperty("type");
                    var value = argumentType.GetString();

                    Assert.NotNull(value);
                    Assert.Contains(value, s_jsonSchemaDataTypes);
                }
            }
        }
    }

    [Theory]
    [InlineData(null)] // All tools
    [InlineData("storage")]
    [InlineData("keyvault")]
    [InlineData("group")]
    [InlineData("storage,keyvault")]
    public async Task GetsToolsByCommandGroup(string? commandGroup)
    {
        string[]? groupArray = commandGroup == null ? null : commandGroup.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var operations = new ToolOperations(_serviceProvider, _commandFactory, _logger)
        {
            CommandGroup = groupArray
        };
        var requestContext = new RequestContext<ListToolsRequestParams>(_server);
        var handler = operations.ToolsCapability.ListToolsHandler;
        Assert.NotNull(handler);
        var result = await handler(requestContext, CancellationToken.None);
        Assert.NotNull(result);
        Assert.NotEmpty(result.Tools);

        if (groupArray != null && groupArray.Length > 0)
        {
            // If groups are specified, all tool names should start with one of those groups
            foreach (var tool in result.Tools)
            {
                Assert.Contains(groupArray, group => tool.Name.StartsWith($"{group}-"));
            }
        }
        else
        {
            // If no group, ensure we have a mix of tools from different groups
            var toolGroups = result.Tools.Select(t => t.Name.Split('-')[1]).Distinct().ToList();
            Assert.True(toolGroups.Count > 1, "Should return tools from multiple groups when no CommandGroup is set.");
        }
    }

    [Fact]
    public async Task GetsNoToolsForUnknownCommandGroup()
    {
        var ex = await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
        {
            var operations = new ToolOperations(_serviceProvider, _commandFactory, _logger)
            {
                CommandGroup = new[] { "unknown-group" }
            };
            var requestContext = new RequestContext<ListToolsRequestParams>(_server);
            var handler = operations.ToolsCapability.ListToolsHandler;
            Assert.NotNull(handler);
            await handler(requestContext, CancellationToken.None);
        });
        Assert.Contains("unknown-group", ex.Message);
    }

    [Fact]
    public async Task ReadOnlyMode_FiltersToolsByReadOnlyHint()
    {
        // Run with ReadOnly = false
        var operations = new ToolOperations(_serviceProvider, _commandFactory, _logger)
        {
            ReadOnly = false
        };
        var requestContext = new RequestContext<ListToolsRequestParams>(_server);
        var handler = operations.ToolsCapability.ListToolsHandler;
        Assert.NotNull(handler);
        var allToolsResult = await handler(requestContext, CancellationToken.None);
        Assert.NotNull(allToolsResult);
        Assert.NotEmpty(allToolsResult.Tools);

        // Run with ReadOnly = true
        operations.ReadOnly = true;
        var readonlyToolsResult = await handler(requestContext, CancellationToken.None);
        Assert.NotNull(readonlyToolsResult);
        Assert.NotEmpty(readonlyToolsResult.Tools);

        // There should be fewer tools in readonly mode
        Assert.True(readonlyToolsResult.Tools.Count < allToolsResult.Tools.Count, "Readonly mode should return fewer tools.");

        // All tools in readonly mode must have ReadOnlyHint = true
        foreach (var tool in readonlyToolsResult.Tools)
        {
            Assert.NotNull(tool.Annotations);
            Assert.True(tool.Annotations.ReadOnlyHint, $"Tool '{tool.Name}' does not have ReadOnlyHint=true");
        }
    }
}
