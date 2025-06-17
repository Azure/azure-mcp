// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Reflection;
using AzureMcp.Commands.Server.Tools;
using AzureMcp.Models.Option;
using AzureMcp.Options.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Protocol;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace AzureMcp.Commands.Server;

[HiddenCommand]
public sealed class ServiceStartCommand : BaseCommand
{
    public const string DefaultName = "Azure.Mcp.Server";

    private const string CommandTitle = "Start MCP Server";
    private const string DefaultServerName = "Azure MCP Server";

    private readonly Option<string> _transportOption = OptionDefinitions.Service.Transport;
    private readonly Option<int> _portOption = OptionDefinitions.Service.Port;
    private readonly Option<string?> _serviceTypeOption = OptionDefinitions.Service.ServiceType;

    public override string Name => "start";
    public override string Description => "Starts Azure MCP Server.";
    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_transportOption);
        command.AddOption(_portOption);
        command.AddOption(_serviceTypeOption);
    }

    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var port = parseResult.GetValueForOption(_portOption) == default
            ? OptionDefinitions.Service.Port.GetDefaultValue()
            : parseResult.GetValueForOption(_portOption);

        var service = parseResult.GetValueForOption(_serviceTypeOption) == default
            ? OptionDefinitions.Service.ServiceType.GetDefaultValue()
            : parseResult.GetValueForOption(_serviceTypeOption);

        var serverOptions = new ServiceStartOptions
        {
            Transport = parseResult.GetValueForOption(_transportOption) ?? TransportTypes.StdIo,
            Port = port,
            Service = service,
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
        services.AddSingleton<IMcpClientService, McpClientService>();

        if (options.Service == "azure")
        {
            services.AddSingleton<McpServerTool, AzureProxyTool>();
        }

        services.AddOptions<McpServerOptions>()
            .Configure<ToolOperations>((mcpServerOptions, toolOperations) =>
            {
                var serverName = entryAssembly?.GetCustomAttribute<AssemblyTitleAttribute>()?.Title ?? DefaultServerName;

                mcpServerOptions.ServerInfo = new Implementation
                {
                    Name = serverName,
                    Version = assemblyVersion
                };

                if (options.Service != "azure")
                {
                    toolOperations.CommandGroup = options.Service;
                    mcpServerOptions.Capabilities = new ServerCapabilities
                    {
                        Tools = toolOperations.ToolsCapability
                    };
                }

                mcpServerOptions.ProtocolVersion = "2024-11-05";
            });

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
