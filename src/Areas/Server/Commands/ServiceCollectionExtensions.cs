using System.Reflection;
using AzureMcp.Areas.Server.Commands.Discovery;
using AzureMcp.Areas.Server.Commands.Runtime;
using AzureMcp.Areas.Server.Commands.ToolLoading;
using AzureMcp.Areas.Server.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ModelContextProtocol.Protocol;

namespace Microsoft.Extensions.DependencyInjection;

public static class AzureMcpServiceCollectionExtensions
{
    /// <summary>
    /// Adds the Azure MCP server services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="serviceStartOptions"></param>
    /// <returns></returns>
    public static IServiceCollection AddAzureMcpServer(this IServiceCollection services, ServiceStartOptions serviceStartOptions)
    {
        // Register options for service start
        services.AddSingleton(serviceStartOptions);
        services.AddSingleton(Options.Options.Create(serviceStartOptions));

        // Register tool loader strategies
        services.AddSingleton<CommandFactoryToolLoader>();
        services.AddSingleton(sp =>
        {
            return new RegistryToolLoader(
                sp.GetRequiredService<RegistryDiscoveryStrategy>(),
                sp.GetRequiredService<IOptions<ServiceStartOptions>>(),
                sp.GetRequiredService<ILogger<RegistryToolLoader>>()
            );
        });

        services.AddSingleton<SingleProxyToolLoader>();
        services.AddSingleton<CompositeToolLoader>();

        // Register server discovery strategies
        services.AddSingleton<CommandGroupDiscoveryStrategy>();
        services.AddSingleton<CompositeDiscoveryStrategy>();
        services.AddSingleton<RegistryDiscoveryStrategy>();

        // Register server providers
        services.AddSingleton<CommandGroupServerProvider>();
        services.AddSingleton<RegistryServerProvider>();

        // Register MCP runtimes
        services.AddSingleton<IMcpRuntime, McpRuntime>();

        var firstService = serviceStartOptions.Service?.FirstOrDefault();

        if (firstService == "azure")
        {
            services.AddSingleton<IMcpDiscoveryStrategy>(sp =>
            {
                var toolLoaders = new List<IMcpDiscoveryStrategy>
                {
                    sp.GetRequiredService<CommandGroupDiscoveryStrategy>(),
                    sp.GetRequiredService<RegistryDiscoveryStrategy>(),
                };

                return new CompositeDiscoveryStrategy(toolLoaders);
            });

            services.AddSingleton<IToolLoader, SingleProxyToolLoader>();
        }
        else if (firstService == "proxy")
        {
            services.AddSingleton<IMcpDiscoveryStrategy>(sp =>
            {
                var discoveryStrategies = new List<IMcpDiscoveryStrategy>
                {
                    sp.GetRequiredService<CommandGroupDiscoveryStrategy>(),
                    sp.GetRequiredService<RegistryDiscoveryStrategy>(),
                };

                return new CompositeDiscoveryStrategy(discoveryStrategies);
            });

            services.AddSingleton<IToolLoader, ServerToolLoader>();
        }
        else
        {
            services.AddSingleton<IToolLoader>(sp =>
            {
                var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
                var toolLoaders = new List<IToolLoader>
                {
                    sp.GetRequiredService<CommandFactoryToolLoader>(),
                    sp.GetRequiredService<RegistryToolLoader>(),
                };

                return new CompositeToolLoader(toolLoaders, loggerFactory.CreateLogger<CompositeToolLoader>());
            });
        }

        var mcpServerOptions = services
            .AddOptions<McpServerOptions>()
            .Configure<IMcpRuntime>((mcpServerOptions, mcpRuntime) =>
            {
                var mcpServerOptionsBuilder = services.AddOptions<McpServerOptions>();
                var entryAssembly = Assembly.GetEntryAssembly();
                var assemblyName = entryAssembly?.GetName();
                var serverName = entryAssembly?.GetCustomAttribute<AssemblyTitleAttribute>()?.Title ?? "Azure MCP Server";

                mcpServerOptions.ProtocolVersion = "2024-11-05";
                mcpServerOptions.ServerInfo = new Implementation
                {
                    Name = serverName,
                    Version = assemblyName?.Version?.ToString() ?? "1.0.0-beta"
                };

                mcpServerOptions.Capabilities = new ServerCapabilities
                {
                    Tools = new ToolsCapability()
                    {
                        CallToolHandler = mcpRuntime.CallToolHandler,
                        ListToolsHandler = mcpRuntime.ListToolsHandler,
                    }
                };
            });

        var mcpServerBuilder = services.AddMcpServer();

        if (serviceStartOptions.Transport != TransportTypes.Sse)
        {
            mcpServerBuilder.WithStdioServerTransport();
        }
        else
        {
            mcpServerBuilder.WithHttpTransport();
        }

        return services;
    }
}
