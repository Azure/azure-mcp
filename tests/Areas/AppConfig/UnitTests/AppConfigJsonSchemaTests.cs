// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Areas.Server.Commands.ToolLoading;
using AzureMcp.Areas.Server.Options;
using AzureMcp.Tests.Areas.Server.UnitTests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Areas.AppConfig.UnitTests;

public class AppConfigJsonSchemaTests
{
    [Fact]
    public async Task AppConfigKvSetTool_ShouldHaveCorrectJsonSchemaForTagsParameter()
    {
        // Arrange
        var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
        var commandFactory = CommandFactoryHelpers.CreateCommandFactory(serviceProvider);
        var logger = Substitute.For<ILogger<CommandFactoryToolLoader>>();
        var server = Substitute.For<IMcpServer>();
        var serviceOptions = Microsoft.Extensions.Options.Options.Create(new ServiceStartOptions());
        var telemetryService = new CommandFactoryHelpers.NoOpTelemetryService();

        var operations = new CommandFactoryToolLoader(serviceProvider, commandFactory, serviceOptions, telemetryService, logger);
        var requestContext = new RequestContext<ListToolsRequestParams>(server);

        // Act
        var result = await operations.ListToolsHandler(requestContext, CancellationToken.None);

        // Find the azmcp_appconfig_kv_set tool
        var appConfigSetTool = result.Tools.FirstOrDefault(t => t.Name == "azmcp_appconfig_kv_set");

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
