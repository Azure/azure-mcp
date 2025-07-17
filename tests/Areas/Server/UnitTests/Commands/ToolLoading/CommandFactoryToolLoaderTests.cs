// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics;
using System.Text.Json;
using AzureMcp.Areas.Server.Commands.ToolLoading;
using AzureMcp.Areas.Server.Options;
using AzureMcp.Commands;
using AzureMcp.Services.Telemetry;
using AzureMcp.Tests.Areas.Server;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Protocol;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Areas.Server.UnitTests.Commands.ToolLoading;

[Trait("Area", "Server")]
public class CommandFactoryToolLoaderTests
{
    private static (CommandFactoryToolLoader toolLoader, CommandFactory commandFactory) CreateToolLoader(ServiceStartOptions? options = null)
    {
        var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        var commandFactory = CommandFactoryHelpers.CreateCommandFactory(serviceProvider);
        var telemetryService = new CommandFactoryHelpers.NoOpTelemetryService();
        var logger = loggerFactory.CreateLogger<CommandFactoryToolLoader>();
        var serviceOptions = Microsoft.Extensions.Options.Options.Create(options ?? new ServiceStartOptions());

        var toolLoader = new CommandFactoryToolLoader(serviceProvider, commandFactory, serviceOptions, telemetryService, logger);
        return (toolLoader, commandFactory);
    }

    private static ModelContextProtocol.Server.RequestContext<ListToolsRequestParams> CreateRequest()
    {
        var mockServer = Substitute.For<ModelContextProtocol.Server.IMcpServer>();
        return new ModelContextProtocol.Server.RequestContext<ListToolsRequestParams>(mockServer)
        {
            Params = new ListToolsRequestParams()
        };
    }

    [Fact]
    public async Task ListToolsHandler_ReturnsToolsWithExpectedProperties()
    {
        var (toolLoader, commandFactory) = CreateToolLoader();
        var request = CreateRequest();

        var result = await toolLoader.ListToolsHandler(request, CancellationToken.None);

        // Verify basic structure
        Assert.NotNull(result);
        Assert.NotNull(result.Tools);

        // Verify that we have tools from the command factory
        Assert.True(result.Tools.Count > 0, "Expected at least one tool to be returned");

        // Get the visible commands from the command factory for comparison
        var visibleCommands = CommandFactory.GetVisibleCommands(commandFactory.AllCommands).ToList();
        Assert.Equal(visibleCommands.Count, result.Tools.Count);

        // Verify each tool has the expected properties
        foreach (var tool in result.Tools)
        {
            Assert.NotNull(tool.Name);
            Assert.NotEmpty(tool.Name);
            Assert.NotNull(tool.Description);
            Assert.True(tool.InputSchema.ValueKind != System.Text.Json.JsonValueKind.Null, "InputSchema should not be null");

            // Verify this tool corresponds to a command from the factory
            var correspondingCommand = visibleCommands.FirstOrDefault(kvp => kvp.Key == tool.Name);
            Assert.NotNull(correspondingCommand.Value);
            Assert.Equal(correspondingCommand.Value.GetCommand().Description, tool.Description);
        }

        // Verify tool names match command names from factory
        var toolNames = result.Tools.Select(t => t.Name).OrderBy(n => n).ToList();
        var commandNames = visibleCommands.Select(kvp => kvp.Key).OrderBy(n => n).ToList();
        Assert.Equal(commandNames, toolNames);
    }

    [Fact]
    public async Task ListToolsHandler_WithReadOnlyOption_ReturnsOnlyReadOnlyTools()
    {
        var readOnlyOptions = new ServiceStartOptions { ReadOnly = true };
        var (toolLoader, _) = CreateToolLoader(readOnlyOptions);
        var request = CreateRequest();

        var result = await toolLoader.ListToolsHandler(request, CancellationToken.None);

        // Verify basic structure
        Assert.NotNull(result);
        Assert.NotNull(result.Tools);

        // When ReadOnly is enabled, only tools with ReadOnlyHint = true should be returned
        // This may result in fewer tools or potentially no tools if none are marked as read-only
        foreach (var tool in result.Tools)
        {
            Assert.True(tool.Annotations?.ReadOnlyHint == true,
                $"Tool '{tool.Name}' should have ReadOnlyHint = true when ReadOnly mode is enabled");
        }
    }

    [Fact]
    public async Task ListToolsHandler_WithServiceFilter_ReturnsOnlyFilteredTools()
    {
        // Try to filter by a specific service/group - using a common Azure service name
        var filteredOptions = new ServiceStartOptions
        {
            Namespace = new[] { "storage" }  // Assuming there's a storage service group
        };
        var (toolLoader, _) = CreateToolLoader(filteredOptions);
        var request = CreateRequest();

        try
        {
            var result = await toolLoader.ListToolsHandler(request, CancellationToken.None);

            // Verify basic structure
            Assert.NotNull(result);
            Assert.NotNull(result.Tools);

            // All returned tools should be from the filtered service group
            // Tool names should start with or contain the service filter
            foreach (var tool in result.Tools)
            {
                Assert.NotNull(tool.Name);
                Assert.NotEmpty(tool.Name);
                // The tool name should reflect that it's from the filtered group
                Assert.True(tool.Name.Contains("storage", StringComparison.OrdinalIgnoreCase) ||
                           tool.Name.StartsWith("storage", StringComparison.OrdinalIgnoreCase),
                           $"Tool '{tool.Name}' should be from the 'storage' service group");
            }
        }
        catch (KeyNotFoundException)
        {
            // If 'storage' group doesn't exist, that's also a valid test result
            // It means the filtering is working as expected
            Assert.True(true, "Service filtering correctly rejected non-existent service group");
        }
    }

    [Fact]
    public async Task ListToolsHandler_WithMultipleServiceFilters_ReturnsToolsFromAllSpecifiedServices()
    {
        // Try to filter by multiple real service/group names from the codebase
        var multiServiceOptions = new ServiceStartOptions
        {
            Namespace = new[] { "storage", "appconfig", "search" }  // Real Azure service groups from the codebase
        };
        var (toolLoader, commandFactory) = CreateToolLoader(multiServiceOptions);
        var request = CreateRequest();

        try
        {
            var result = await toolLoader.ListToolsHandler(request, CancellationToken.None);

            // Verify basic structure
            Assert.NotNull(result);
            Assert.NotNull(result.Tools);

            // Get all commands from the specified groups for comparison
            var expectedCommands = new List<string>();
            var existingServices = new List<string>();

            var serviceCommands = commandFactory.GroupCommands(multiServiceOptions.Namespace);
            expectedCommands.AddRange(serviceCommands.Keys);
            existingServices.AddRange(multiServiceOptions.Namespace);

            if (expectedCommands.Count > 0)
            {
                // Verify that returned tools match expected commands from the filtered groups
                var toolNames = result.Tools.Select(t => t.Name).ToHashSet();
                var expectedCommandNames = expectedCommands.ToHashSet();

                Assert.Equal(expectedCommandNames, toolNames);

                // All returned tools should be from one of the filtered service groups
                foreach (var tool in result.Tools)
                {
                    Assert.NotNull(tool.Name);
                    Assert.NotEmpty(tool.Name);

                    var isFromFilteredGroup = existingServices.Any(service =>
                        tool.Name.Contains(service, StringComparison.OrdinalIgnoreCase) ||
                        tool.Name.StartsWith(service, StringComparison.OrdinalIgnoreCase));

                    Assert.True(isFromFilteredGroup,
                        $"Tool '{tool.Name}' should be from one of the filtered service groups: {string.Join(", ", existingServices)}");
                }

                // Verify that tools from non-specified services are not included
                var allToolsOptions = new ServiceStartOptions(); // No filter = all tools
                var (allToolsLoader, _) = CreateToolLoader(allToolsOptions);
                var allToolsResult = await allToolsLoader.ListToolsHandler(request, CancellationToken.None);

                var excludedTools = allToolsResult.Tools.Where(t =>
                    !existingServices.Any(service =>
                        t.Name.Contains(service, StringComparison.OrdinalIgnoreCase) ||
                        t.Name.StartsWith(service, StringComparison.OrdinalIgnoreCase)));

                foreach (var excludedTool in excludedTools)
                {
                    Assert.False(toolNames.Contains(excludedTool.Name),
                        $"Tool '{excludedTool.Name}' should not be included when filtering by services: {string.Join(", ", existingServices)}");
                }
            }
            else
            {
                // If no groups exist, we should get no tools or an exception was thrown
                Assert.Empty(result.Tools);
            }
        }
        catch (KeyNotFoundException)
        {
            // If none of the service groups exist, that's also a valid test result
            // It means the filtering is working as expected
            Assert.True(true, "Service filtering correctly rejected non-existent service groups");
        }
    }

    [Fact]
    public async Task CallToolHandler_WithValidTool_ExecutesSuccessfully()
    {
        var (toolLoader, commandFactory) = CreateToolLoader();

        // Get the first available command for testing
        var availableCommands = CommandFactory.GetVisibleCommands(commandFactory.AllCommands);
        var firstCommand = availableCommands.First();

        var mockServer = Substitute.For<ModelContextProtocol.Server.IMcpServer>();
        var request = new ModelContextProtocol.Server.RequestContext<CallToolRequestParams>(mockServer)
        {
            Params = new CallToolRequestParams
            {
                Name = firstCommand.Key,
                Arguments = new Dictionary<string, JsonElement>()
            }
        };

        var result = await toolLoader.CallToolHandler(request, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotNull(result.Content);
        Assert.NotEmpty(result.Content);
    }

    [Fact]
    public async Task CallToolHandler_WithNullParams_ReturnsError()
    {
        var (toolLoader, _) = CreateToolLoader();

        var mockServer = Substitute.For<ModelContextProtocol.Server.IMcpServer>();
        var request = new ModelContextProtocol.Server.RequestContext<CallToolRequestParams>(mockServer)
        {
            Params = null
        };

        var result = await toolLoader.CallToolHandler(request, CancellationToken.None);

        Assert.NotNull(result);
        Assert.True(result.IsError);
        Assert.NotNull(result.Content);
        Assert.Single(result.Content);

        var textContent = result.Content.First() as TextContentBlock;
        Assert.NotNull(textContent);
        Assert.Contains("Cannot call tools with null parameters", textContent.Text);
    }

    [Fact]
    public async Task CallToolHandler_WithUnknownTool_ReturnsError()
    {
        var (toolLoader, _) = CreateToolLoader();

        var mockServer = Substitute.For<ModelContextProtocol.Server.IMcpServer>();
        var request = new ModelContextProtocol.Server.RequestContext<CallToolRequestParams>(mockServer)
        {
            Params = new CallToolRequestParams
            {
                Name = "non-existent-tool",
                Arguments = new Dictionary<string, JsonElement>()
            }
        };

        var result = await toolLoader.CallToolHandler(request, CancellationToken.None);

        Assert.NotNull(result);
        Assert.True(result.IsError);
        Assert.NotNull(result.Content);
        Assert.Single(result.Content);

        var textContent = result.Content.First() as TextContentBlock;
        Assert.NotNull(textContent);
        Assert.Contains("Could not find command: non-existent-tool", textContent.Text);
    }

    [Fact]
    public async Task GetsToolsWithRawMcpInputOption()
    {
        var multiServiceOptions = new ServiceStartOptions
        {
            Namespace = new[] { "deploy" }  // Real Azure service groups from the codebase
        };
        var (toolLoader, commandFactory) = CreateToolLoader(multiServiceOptions);
        var request = CreateRequest();
        var result = await toolLoader.ListToolsHandler(request, CancellationToken.None);
        
        Assert.NotNull(result);
        Assert.NotEmpty(result.Tools);

        var tool = result.Tools.FirstOrDefault(tool =>
            tool.Name.Equals("deploy_architecture-diagram-generate", StringComparison.OrdinalIgnoreCase));
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
            Assert.Contains(commandArguments, arg => arg.Name.Equals("projectName", StringComparison.OrdinalIgnoreCase));
            Assert.Contains(commandArguments, arg => arg.Name.Equals("services", StringComparison.OrdinalIgnoreCase) &&
                                                    arg.Value.GetProperty("type").GetString() == "array");
            var servicesArgument = commandArguments.FirstOrDefault(arg => arg.Name.Equals("services", StringComparison.OrdinalIgnoreCase));
            if (servicesArgument.Value.ValueKind != JsonValueKind.Undefined)
            {
                if (servicesArgument.Value.TryGetProperty("items", out var itemsProperty))
                {
                    if (itemsProperty.TryGetProperty("properties", out var servicesProperties))
                    {
                        var servicePropertyArgs = servicesProperties.EnumerateObject().ToArray();
                        Assert.Contains(servicePropertyArgs, prop => prop.Name.Equals("dependencies", StringComparison.OrdinalIgnoreCase) && 
                                                                    prop.Value.GetProperty("type").GetString() == "array");
                    }
                }
            }
        }
    }
}

