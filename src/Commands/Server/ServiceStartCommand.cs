// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Reflection;
using AzureMcp.Arguments.Server;
using AzureMcp.Models;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace AzureMcp.Commands.Server;

[HiddenCommand]
public sealed class ServiceStartCommand : BaseCommand
{
    private const string _commandTitle = "Start MCP Server";
    private readonly Option<string> _transportOption = ArgumentDefinitions.Service.Transport.ToOption();
    private readonly Option<int> _portOption = ArgumentDefinitions.Service.Port.ToOption();

    public override string Name => "start";
    public override string Description => "Starts Azure MCP Server.";
    public override string Title => _commandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_transportOption);
        command.AddOption(_portOption);
    }

    protected override void RegisterArguments()
    {
        base.RegisterArguments();
        AddArgument(GetTransportArgument());
        AddArgument(GetPortArgument());
    }

    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult commandOptions)
    {
        var port = commandOptions.GetValueForOption(_portOption) == default
            ? ArgumentDefinitions.Service.Port.DefaultValue
            : commandOptions.GetValueForOption(_portOption);

        var serverOptions = new ServiceStartArguments
        {
            Transport = commandOptions.GetValueForOption(_transportOption) ?? TransportTypes.StdIo,
            Port = port
        };

        using var host = CreateHost(serverOptions);
        await host.StartAsync(CancellationToken.None);
        await host.WaitForShutdownAsync(CancellationToken.None);

        return context.Response;
    }

    private IHost CreateHost(ServiceStartArguments serverArguments)
    {
        if (serverArguments.Transport == TransportTypes.Sse)
        {
            var builder = WebApplication.CreateBuilder([]);
            Program.ConfigureServices(builder.Services);
            ConfigureMcpServer(builder.Services, serverArguments.Transport);

            builder.WebHost
                .ConfigureKestrel(server => server.ListenAnyIP(serverArguments.Port))
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
                    ConfigureMcpServer(services, serverArguments.Transport);
                })
                .Build();
        }
    }

    private static ArgumentDefinition<string> GetTransportArgument()
    {
        var definition = ArgumentDefinitions.Service.Transport;
        return new ArgumentDefinition<string>(
            definition.Name,
            definition.Description,
            definition.DefaultValue?.ToString() ?? TransportTypes.StdIo);
    }

    private static ArgumentDefinition<string> GetPortArgument()
    {
        var definition = ArgumentDefinitions.Service.Port;
        return new ArgumentDefinition<string>(
            definition.Name,
            definition.Description,
            definition.DefaultValue.ToString());
    }

    private static void ConfigureMcpServer(IServiceCollection services, string transport)
    {
        services.AddSingleton<ToolOperations>();
        services.AddSingleton<AzureEventSourceLogForwarder>();
        services.AddHostedService<OtelService>();

        services.AddOptions<McpServerOptions>()
            .Configure<ToolOperations>((mcpServerOptions, toolOperations) =>
            {
                var entryAssembly = Assembly.GetEntryAssembly();
                var assemblyName = entryAssembly?.GetName();
                var serverName = entryAssembly?.GetCustomAttribute<AssemblyTitleAttribute>()?.Title ?? "Azure MCP Server";

                mcpServerOptions.ServerInfo = new Implementation
                {
                    Name = serverName,
                    Version = assemblyName?.Version?.ToString() ?? "1.0.0-beta"
                };

                mcpServerOptions.Capabilities = new ServerCapabilities
                {
                    Tools = toolOperations.ToolsCapability
                };

                mcpServerOptions.ProtocolVersion = "2024-11-05";

            });

        var mcpServerBuilder = services.AddMcpServer();

        if (transport != TransportTypes.Sse)
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

    /// <summary>
    /// Resolves (and starts) the OpenTelemetry services.
    /// </summary>
    private sealed class OtelService : BackgroundService
    {
        private readonly MeterProvider? _meterProvider;
        private readonly TracerProvider? _tracerProvider;
        private readonly LoggerProvider? _loggerProvider;
        private readonly AzureEventSourceLogForwarder _logForwarder;

        public OtelService(IServiceProvider provider)
        {
            _meterProvider = provider.GetService<MeterProvider>();
            _tracerProvider = provider.GetService<TracerProvider>();
            _loggerProvider = provider.GetService<LoggerProvider>();
            _logForwarder = provider.GetRequiredService<AzureEventSourceLogForwarder>();
            _logForwarder.Start();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken) => Task.CompletedTask;

        public override void Dispose()
        {
            _meterProvider?.Dispose();
            _tracerProvider?.Dispose();
            _loggerProvider?.Dispose();
            _logForwarder.Dispose();
        }
    }
}
