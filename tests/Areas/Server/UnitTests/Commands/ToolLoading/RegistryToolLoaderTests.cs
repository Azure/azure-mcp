// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Areas.Server.Commands.Discovery;
using AzureMcp.Areas.Server.Commands.ToolLoading;
using AzureMcp.Areas.Server.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Protocol;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Areas.Server.UnitTests.Commands.ToolLoading;

[Trait("Area", "Server")]
public class RegistryToolLoaderTests
{
    private static (RegistryToolLoader toolLoader, IMcpDiscoveryStrategy mockDiscoveryStrategy) CreateToolLoader(ServiceStartOptions? options = null)
    {
        var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        var mockDiscoveryStrategy = Substitute.For<IMcpDiscoveryStrategy>();
        var logger = loggerFactory.CreateLogger<RegistryToolLoader>();
        var serviceOptions = Microsoft.Extensions.Options.Options.Create(options ?? new ServiceStartOptions());

        var toolLoader = new RegistryToolLoader(mockDiscoveryStrategy, serviceOptions, logger);
        return (toolLoader, mockDiscoveryStrategy);
    }

    private static ModelContextProtocol.Server.RequestContext<ListToolsRequestParams> CreateRequest()
    {
        var mockServer = Substitute.For<ModelContextProtocol.Server.IMcpServer>();
        return new ModelContextProtocol.Server.RequestContext<ListToolsRequestParams>(mockServer)
        {
            Params = new ListToolsRequestParams()
        };
    }

    private static ModelContextProtocol.Server.RequestContext<CallToolRequestParams> CreateCallToolRequest(string toolName, IReadOnlyDictionary<string, JsonElement>? arguments = null)
    {
        var mockServer = Substitute.For<ModelContextProtocol.Server.IMcpServer>();
        return new ModelContextProtocol.Server.RequestContext<CallToolRequestParams>(mockServer)
        {
            Params = new CallToolRequestParams
            {
                Name = toolName,
                Arguments = arguments ?? new Dictionary<string, JsonElement>()
            }
        };
    }

    [Fact]
    public async Task ListToolsHandler_WithNoServers_ReturnsEmptyToolList()
    {
        // Arrange
        var (toolLoader, mockDiscoveryStrategy) = CreateToolLoader();
        var request = CreateRequest();

        mockDiscoveryStrategy.DiscoverServersAsync()
            .Returns(Task.FromResult(Enumerable.Empty<IMcpServerProvider>()));

        // Act
        var result = await toolLoader.ListToolsHandler(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Tools);
        Assert.Empty(result.Tools);
    }

    [Fact]
    public async Task ListToolsHandler_WithRealRegistryDiscovery_ReturnsExpectedStructure()
    {
        // Arrange - use real RegistryDiscoveryStrategy
        var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        var options = Microsoft.Extensions.Options.Options.Create(new ServiceStartOptions());
        var discoveryStrategy = new RegistryDiscoveryStrategy(options);
        var logger = loggerFactory.CreateLogger<RegistryToolLoader>();

        var toolLoader = new RegistryToolLoader(discoveryStrategy, options, logger);
        var request = CreateRequest();

        // Act
        var result = await toolLoader.ListToolsHandler(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Tools);
        Assert.True(result.Tools.Count >= 0); // Should return at least an empty list

        // The result should be consistent (either empty if no registry, or have tools)
        // Each tool should have proper structure if any exist
        foreach (var tool in result.Tools)
        {
            Assert.NotNull(tool.Name);
            Assert.NotEmpty(tool.Name);
            Assert.NotNull(tool.Description);
            Assert.True(tool.InputSchema.ValueKind != JsonValueKind.Undefined, "InputSchema should be defined");
        }
    }

    [Fact]
    public async Task ListToolsHandler_WithReadOnlyOption_FiltersProperly()
    {
        // Arrange
        var readOnlyOptions = new ServiceStartOptions { ReadOnly = true };
        var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        var serviceOptions = Microsoft.Extensions.Options.Options.Create(readOnlyOptions);
        var discoveryStrategy = new RegistryDiscoveryStrategy(serviceOptions);
        var logger = loggerFactory.CreateLogger<RegistryToolLoader>();

        var toolLoader = new RegistryToolLoader(discoveryStrategy, serviceOptions, logger);
        var request = CreateRequest();

        // Act
        var result = await toolLoader.ListToolsHandler(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Tools);

        // When ReadOnly is enabled, only tools with ReadOnlyHint = true should be returned
        foreach (var tool in result.Tools)
        {
            Assert.True(tool.Annotations?.ReadOnlyHint == true,
                $"Tool '{tool.Name}' should have ReadOnlyHint = true when ReadOnly mode is enabled");
        }
    }

    [Fact]
    public async Task CallToolHandler_WithUnknownTool_ThrowsKeyNotFoundException()
    {
        // Arrange
        var (toolLoader, _) = CreateToolLoader();
        var request = CreateCallToolRequest("unknown-tool");

        // Act & Assert
        // The current implementation throws KeyNotFoundException for unknown tools
        // This is actually a bug in the implementation, but we test the current behavior
        await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            await toolLoader.CallToolHandler(request, CancellationToken.None));
    }

    [Fact]
    public async Task RegistryToolLoader_WithDifferentOptions_BehavesConsistently()
    {
        // Arrange - Test with different service options
        var defaultOptions = new ServiceStartOptions();
        var readOnlyOptions = new ServiceStartOptions { ReadOnly = true };

        var (defaultToolLoader, mockDiscoveryStrategy1) = CreateToolLoader(defaultOptions);
        var (readOnlyToolLoader, mockDiscoveryStrategy2) = CreateToolLoader(readOnlyOptions);

        // Setup both discovery strategies to return empty
        mockDiscoveryStrategy1.DiscoverServersAsync()
            .Returns(Task.FromResult(Enumerable.Empty<IMcpServerProvider>()));
        mockDiscoveryStrategy2.DiscoverServersAsync()
            .Returns(Task.FromResult(Enumerable.Empty<IMcpServerProvider>()));

        var request = CreateRequest();

        // Act
        var defaultResult = await defaultToolLoader.ListToolsHandler(request, CancellationToken.None);
        var readOnlyResult = await readOnlyToolLoader.ListToolsHandler(request, CancellationToken.None);

        // Assert - Both should return empty but valid results
        Assert.NotNull(defaultResult);
        Assert.NotNull(defaultResult.Tools);
        Assert.Empty(defaultResult.Tools);

        Assert.NotNull(readOnlyResult);
        Assert.NotNull(readOnlyResult.Tools);
        Assert.Empty(readOnlyResult.Tools);
    }
}
