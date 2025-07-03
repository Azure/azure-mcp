// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Reflection;
using AzureMcp.Areas.Server.Commands.Tools;
using AzureMcp.Areas.Server.Options;
using AzureMcp.Commands;
using AzureMcp.Commands.Server;
using AzureMcp.Commands.Server.Tools;
using AzureMcp.Models.Option;
using AzureMcp.Services.Telemetry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Protocol;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace AzureMcp.Areas.Server.Commands;

[HiddenCommand]
public sealed class ServiceStartCommand : BaseCommand
{
    private const string CommandTitle = "Start MCP Server";
    private const string DefaultServerName = "Azure MCP Server";

    private readonly Option<string> _transportOption = OptionDefinitions.Service.Transport;
    private readonly Option<int> _portOption = OptionDefinitions.Service.Port;
    private readonly Option<string[]?> _serviceTypeOption = OptionDefinitions.Service.ServiceType;

    private readonly Option<bool?> _readOnlyOption = OptionDefinitions.Service.ReadOnly;

    public override string Name => "start";
    public override string Description => "Starts Azure MCP Server.";
    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_transportOption);
        command.AddOption(_portOption);
        command.AddOption(_serviceTypeOption);
        command.AddOption(_readOnlyOption);
    }

    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var port = parseResult.GetValueForOption(_portOption) == default
            ? OptionDefinitions.Service.Port.GetDefaultValue()
            : parseResult.GetValueForOption(_portOption);

        var serviceArray = parseResult.GetValueForOption(_serviceTypeOption) ?? OptionDefinitions.Service.ServiceType.GetDefaultValue();

        var readOnly = parseResult.GetValueForOption(_readOnlyOption) == default
            ? OptionDefinitions.Service.ReadOnly.GetDefaultValue()
            : parseResult.GetValueForOption(_readOnlyOption);

        var serverOptions = new ServiceStartOptions
        {
            Transport = parseResult.GetValueForOption(_transportOption) ?? TransportTypes.StdIo,
            Port = port,
            Service = serviceArray,
            ReadOnly = readOnly,
        };

        using var host = CreateHost(serverOptions);
        await host.StartAsync(CancellationToken.None);
        await host.WaitForShutdownAsync(CancellationToken.None);

        return context.Response;
    }

    private IHost CreateHost(ServiceStartOptions serverOptions)
    {
        if (serverOptions.Transport == TransportTypes.Sse)
        {
            var builder = WebApplication.CreateBuilder([]);
            Program.ConfigureServices(builder.Services);
            ConfigureMcpServer(builder.Services, serverOptions);

            builder.WebHost
                .ConfigureKestrel(server => server.ListenAnyIP(serverOptions.Port))
                .ConfigureLogging(logging =>
                {
                    logging.AddEventSourceLogger();
                });

            var application = builder.Build();

            application.MapMcp();

            return application;
        }
        else
        {
            return Host.CreateDefaultBuilder()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddEventSourceLogger();
                })
                .ConfigureServices(services =>
                {
                    Program.ConfigureServices(services);
                    ConfigureMcpServer(services, serverOptions);
                })
                .Build();
        }
    }

    private static void ConfigureMcpServer(IServiceCollection services, ServiceStartOptions options)
    {
        var entryAssembly = Assembly.GetEntryAssembly();
        var assemblyName = entryAssembly?.GetName() ?? new AssemblyName();
        var assemblyVersion = assemblyName?.Version?.ToString() ?? "1.0.0-beta";

        services.AddSingleton<ToolOperations>();
        services.AddSingleton<ProxyToolOperations>();
        services.AddSingleton<IMcpClientService, McpClientService>();

        var mcpServerOptionsBuilder = services.AddOptions<McpServerOptions>();
        var serverName = entryAssembly?.GetCustomAttribute<AssemblyTitleAttribute>()?.Title ?? DefaultServerName;

        mcpServerOptionsBuilder.Configure<ITelemetryService>((mcpServerOptions, telemetryService) =>
        {
            mcpServerOptions.ProtocolVersion = "2024-11-05";
            mcpServerOptions.ServerInfo = new Implementation
            {
                Name = serverName,
                Version = assemblyName?.Version?.ToString() ?? "1.0.0-beta"
            };

            if (mcpServerOptions.Capabilities == null)
            {
                mcpServerOptions.Capabilities = new ServerCapabilities();
            }
        });

        var serviceArray = options.Service;

        // The "azure" mode contains a single "azure" tools that performs internal tool discovery and proxying.
        if (serviceArray != null && serviceArray.Length == 1 && serviceArray[0] == "azure")
        {
            services.AddSingleton<McpServerTool, AzureProxyTool>();
        }
        // The "proxy" mode exposes a single tool per service/namespace and performs internal tool discovery and proxying.
        else if (serviceArray != null && serviceArray.Length == 1 && serviceArray[0] == "proxy")
        {
            mcpServerOptionsBuilder.Configure<ProxyToolOperations>((mcpServerOptions, toolOperations) =>
            {
                toolOperations.ReadOnly = options.ReadOnly ?? false;

                if (mcpServerOptions.Capabilities == null)
                {
                    mcpServerOptions.Capabilities = new ServerCapabilities();
                }

                mcpServerOptions.Capabilities.Tools = new ToolsCapability()
                {
                    CallToolHandler = toolOperations.CallToolHandler,
                    ListToolsHandler = toolOperations.ListToolsHandler,
                };
            });
        }
        // The default mode loads all tools from the default ToolOperations service.
        else
        {
            mcpServerOptionsBuilder.Configure<ToolOperations>((mcpServerOptions, toolOperations) =>
            {
                toolOperations.ReadOnly = options.ReadOnly ?? false;
                toolOperations.CommandGroup = serviceArray;

                if (mcpServerOptions.Capabilities == null)
                {
                    mcpServerOptions.Capabilities = new ServerCapabilities();
                }

                mcpServerOptions.Capabilities.Tools = toolOperations.ToolsCapability;
            });
        }

        var mcpServerBuilder = services.AddMcpServer();

        if (options.Transport != TransportTypes.Sse)
        {
            mcpServerBuilder.WithStdioServerTransport();
        }
        else
        {
            mcpServerBuilder.WithHttpTransport();
        }
    }

    private sealed class StdioMcpServerHostedService(IMcpServer session) : BackgroundService
    {
        /// <inheritdoc />
        protected override Task ExecuteAsync(CancellationToken stoppingToken) => session.RunAsync(stoppingToken);
    }
}
