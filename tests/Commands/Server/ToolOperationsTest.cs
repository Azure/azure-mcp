// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Commands;
using AzureMcp.Commands.Server;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Commands.Server;
public class ToolOperationsTest
{
    // https://json-schema.org/understanding-json-schema/reference/type
    private static readonly HashSet<string> JsonSchemaDataTypes = new()
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
    private readonly ILogger<ToolOperations> _logger;
    private readonly ILogger<CommandFactory> _commandFactoryLogger;
    private readonly IMcpServer _server;

    public ToolOperationsTest()
    {
        _logger = Substitute.For<ILogger<ToolOperations>>();
        _commandFactoryLogger = Substitute.For<ILogger<CommandFactory>>();
        _server = Substitute.For<IMcpServer>();
        _serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
        _commandFactory = new CommandFactory(_serviceProvider, _commandFactoryLogger);
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
                    Assert.Contains(value, JsonSchemaDataTypes);
                }
            }
        }
    }
}
