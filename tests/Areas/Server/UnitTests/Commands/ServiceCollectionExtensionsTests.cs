// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Server.Commands.Discovery;
using AzureMcp.Areas.Server.Commands.Runtime;
using AzureMcp.Areas.Server.Commands.ToolLoading;
using AzureMcp.Areas.Server.Options;
using AzureMcp.Commands;
using AzureMcp.Services.Telemetry;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using Xunit;

namespace AzureMcp.Tests.Areas.Server.UnitTests.Commands;

[Trait("Area", "Server")]
public class ServiceCollectionExtensionsTests
{
    // TransportTypes is internal, so we'll use strings directly
    private const string StdioTransport = "stdio";
    private const string SseTransport = "sse";

    private IServiceCollection SetupBaseServices()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddSingleton(CommandFactoryHelpers.CreateCommandFactory);
        services.AddSingleton<ITelemetryService, CommandFactoryHelpers.NoOpTelemetryService>();

        return services;
    }

    [Fact]
    public void AddAzureMcpServer_RegistersCommonServices()
    {
        // Arrange
        var services = SetupBaseServices();
        var options = new ServiceStartOptions
        {
            Transport = StdioTransport
        };

        // Act
        services.AddAzureMcpServer(options);

        // Assert
        // Verify common services are registered
        var provider = services.BuildServiceProvider();

        // Verify base discovery strategies
        Assert.NotNull(provider.GetService<CommandGroupDiscoveryStrategy>());
        Assert.NotNull(provider.GetService<RegistryDiscoveryStrategy>());

        // Verify base tool loaders
        Assert.NotNull(provider.GetService<CommandFactoryToolLoader>());
        Assert.NotNull(provider.GetService<RegistryToolLoader>());

        // Verify runtime
        Assert.NotNull(provider.GetService<IMcpRuntime>());
        Assert.IsType<McpRuntime>(provider.GetService<IMcpRuntime>());
    }

    [Fact]
    public void AddAzureMcpServer_WithAzureService_RegistersSingleProxyToolLoader()
    {
        // Arrange
        var services = SetupBaseServices();
        var options = new ServiceStartOptions
        {
            Transport = StdioTransport,
            Service = new[] { "azure" }
        };

        // Act
        services.AddAzureMcpServer(options);

        // Assert
        var provider = services.BuildServiceProvider();

        // Verify the correct tool loader is registered
        Assert.NotNull(provider.GetService<IToolLoader>());
        Assert.IsType<SingleProxyToolLoader>(provider.GetService<IToolLoader>());

        // Verify discovery strategy is registered
        Assert.NotNull(provider.GetService<IMcpDiscoveryStrategy>());
        Assert.IsType<CompositeDiscoveryStrategy>(provider.GetService<IMcpDiscoveryStrategy>());
    }

    [Fact]
    public void AddAzureMcpServer_WithProxyService_RegistersServerToolLoader()
    {
        // Arrange
        var services = SetupBaseServices();
        var options = new ServiceStartOptions
        {
            Transport = StdioTransport,
            Service = new[] { "proxy" }
        };

        // Act
        services.AddAzureMcpServer(options);

        // Assert
        var provider = services.BuildServiceProvider();

        // Verify the correct tool loader is registered
        Assert.NotNull(provider.GetService<IToolLoader>());
        Assert.IsType<ServerToolLoader>(provider.GetService<IToolLoader>());

        // Verify discovery strategy is registered
        Assert.NotNull(provider.GetService<IMcpDiscoveryStrategy>());
        Assert.IsType<CompositeDiscoveryStrategy>(provider.GetService<IMcpDiscoveryStrategy>());
    }

    [Fact]
    public void AddAzureMcpServer_WithDefaultService_RegistersCompositeToolLoader()
    {
        // Arrange
        var services = SetupBaseServices();
        var options = new ServiceStartOptions
        {
            Transport = StdioTransport,
            // No service specified
        };

        // Act
        services.AddAzureMcpServer(options);

        // Assert
        var provider = services.BuildServiceProvider();

        // Verify the correct tool loader is registered
        Assert.NotNull(provider.GetService<IToolLoader>());
        Assert.IsType<CompositeToolLoader>(provider.GetService<IToolLoader>());
    }

    [Fact]
    public void AddAzureMcpServer_WithStdioTransport_ConfiguresStdioTransport()
    {
        // Arrange
        var services = SetupBaseServices();
        var options = new ServiceStartOptions
        {
            Transport = StdioTransport,
            // Define service as "azure" to prevent CompositeDiscoveryStrategy error
            Service = new[] { "azure" }
        };

        // Act
        services.AddAzureMcpServer(options);

        // Assert
        // Build the provider to verify that service registration succeeded without exceptions
        var provider = services.BuildServiceProvider();

        // Check that appropriate registration was completed
        Assert.NotNull(provider.GetService<IMcpRuntime>());

        // Verify that the service collection contains an IMcpServer registration
        Assert.Contains(services, sd => sd.ServiceType == typeof(IMcpServer));
    }

    [Fact]
    public void AddAzureMcpServer_WithSseTransport_ConfiguresHttpTransport()
    {
        // Arrange
        var services = SetupBaseServices();
        var options = new ServiceStartOptions
        {
            Transport = SseTransport,
            Port = 8080,
            // Define service as "azure" to prevent CompositeDiscoveryStrategy error
            Service = new[] { "azure" }
        };

        // Act
        services.AddAzureMcpServer(options);

        // Assert
        // Build the provider to verify that service registration succeeded without exceptions
        var provider = services.BuildServiceProvider();

        // Check that appropriate registration was completed - we can't directly test
        // IMcpServer as it requires actual HTTP setup, but we can verify related services
        Assert.NotNull(provider.GetService<IMcpRuntime>());

        // Verify that the service collection has MCP-related registrations
        Assert.Contains(services, sd => sd.ServiceType == typeof(IMcpRuntime));
    }

    [Fact]
    public void AddAzureMcpServer_ConfiguresMcpServerOptions()
    {
        // Arrange
        var services = SetupBaseServices();
        var options = new ServiceStartOptions
        {
            Transport = StdioTransport
        };

        // Act
        services.AddAzureMcpServer(options);

        // Assert
        var provider = services.BuildServiceProvider();
        var mcpServerOptions = provider.GetService<IOptions<McpServerOptions>>()?.Value;

        // Verify server options are configured
        Assert.NotNull(mcpServerOptions);
        Assert.Equal("2024-11-05", mcpServerOptions.ProtocolVersion);
        Assert.NotNull(mcpServerOptions.ServerInfo);
        Assert.NotNull(mcpServerOptions.Capabilities);
        Assert.NotNull(mcpServerOptions.Capabilities.Tools);
    }

    [Fact]
    public void AddAzureMcpServer_RegistersOptionsWithSameInstance()
    {
        // Arrange
        var services = SetupBaseServices();
        var options = new ServiceStartOptions
        {
            Transport = StdioTransport,
            ReadOnly = true
        };

        // Act
        services.AddAzureMcpServer(options);

        // Assert
        var provider = services.BuildServiceProvider();
        var registeredOptions = provider.GetService<ServiceStartOptions>();
        var wrappedOptions = provider.GetService<IOptions<ServiceStartOptions>>()?.Value;

        // Verify both registrations point to the same instance
        Assert.NotNull(registeredOptions);
        Assert.NotNull(wrappedOptions);
        Assert.Same(options, registeredOptions);
        Assert.Same(options, wrappedOptions);
        Assert.True(registeredOptions.ReadOnly);
    }

    [Fact]
    public void AddAzureMcpServer_WithReadOnlyOption_RegistersOption()
    {
        // Arrange
        var services = SetupBaseServices();
        var options = new ServiceStartOptions
        {
            Transport = StdioTransport,
            ReadOnly = true
        };

        // Act
        services.AddAzureMcpServer(options);

        // Assert
        var provider = services.BuildServiceProvider();
        var registeredOptions = provider.GetService<ServiceStartOptions>();

        Assert.NotNull(registeredOptions);
        Assert.True(registeredOptions.ReadOnly);

        // Verify the option is also available as IOptions<ServiceStartOptions>
        var optionsMonitor = provider.GetService<IOptions<ServiceStartOptions>>();
        Assert.NotNull(optionsMonitor);
        Assert.True(optionsMonitor.Value.ReadOnly);
    }

    [Fact]
    public void AddAzureMcpServer_WithSpecificServiceAreas_RegistersCompositeToolLoader()
    {
        // Arrange
        var services = SetupBaseServices();
        var options = new ServiceStartOptions
        {
            Transport = StdioTransport,
            Service = new[] { "keyvault", "storage" }
        };

        // Act
        services.AddAzureMcpServer(options);

        // Assert
        var provider = services.BuildServiceProvider();

        // Verify the correct tool loader is registered
        Assert.NotNull(provider.GetService<IToolLoader>());
        Assert.IsType<CompositeToolLoader>(provider.GetService<IToolLoader>());

        // Verify the presence of CommandFactoryToolLoader which is used for service areas
        Assert.NotNull(provider.GetService<CommandFactoryToolLoader>());

        // Verify runtime
        Assert.NotNull(provider.GetService<IMcpRuntime>());
        Assert.IsType<McpRuntime>(provider.GetService<IMcpRuntime>());
    }

    [Theory]
    [InlineData("keyvault")]
    [InlineData("storage")]
    [InlineData("servicebus")]
    public void AddAzureMcpServer_WithSingleServiceArea_RegistersAppropriateServices(string serviceArea)
    {
        // Arrange
        var services = SetupBaseServices();
        var options = new ServiceStartOptions
        {
            Transport = StdioTransport,
            Service = new[] { serviceArea }
        };

        // Act
        services.AddAzureMcpServer(options);

        // Assert
        var provider = services.BuildServiceProvider();

        // Verify the tool loader is registered correctly
        Assert.NotNull(provider.GetService<IToolLoader>());
        Assert.IsType<CompositeToolLoader>(provider.GetService<IToolLoader>());

        // Verify runtime
        Assert.NotNull(provider.GetService<IMcpRuntime>());
        Assert.IsType<McpRuntime>(provider.GetService<IMcpRuntime>());
    }
}
