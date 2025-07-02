// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Areas.Server.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Commands.Server;

public class AppConfigJsonSchemaTests
{
    [Fact]
    public async Task AppConfigKvSetTool_ShouldHaveCorrectJsonSchemaForTagsParameter()
    {
        // Arrange
        var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
        var commandFactory = CommandFactoryHelpers.CreateCommandFactory(serviceProvider);
        var logger = Substitute.For<ILogger<ToolOperations>>();
        var server = Substitute.For<IMcpServer>();

        var operations = new ToolOperations(serviceProvider, commandFactory, logger);
        var requestContext = new RequestContext<ListToolsRequestParams>(server);

        // Act
        var handler = operations.ToolsCapability.ListToolsHandler!;
        var result = await handler(requestContext, CancellationToken.None);

        // Find the azmcp-appconfig-kv-set tool
        var appConfigSetTool = result.Tools.FirstOrDefault(t => t.Name == "azmcp-appconfig-kv-set");

        // Assert
        Assert.NotNull(appConfigSetTool);
        Assert.Equal(JsonValueKind.Object, appConfigSetTool.InputSchema.ValueKind);

        // Check that the tags parameter exists and has correct structure
        var properties = appConfigSetTool.InputSchema.GetProperty("properties");
        Assert.True(properties.TryGetProperty("tags", out var tagsProperty));

        // Verify tags parameter has array type
        Assert.True(tagsProperty.TryGetProperty("type", out var typeProperty));
        Assert.Equal("array", typeProperty.GetString());

        // Verify tags parameter has items property
        Assert.True(tagsProperty.TryGetProperty("items", out var itemsProperty));
        Assert.Equal(JsonValueKind.Object, itemsProperty.ValueKind);

        // Verify items has string type
        Assert.True(itemsProperty.TryGetProperty("type", out var itemTypeProperty));
        Assert.Equal("string", itemTypeProperty.GetString());
    }
}
