// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Server.Options;
using AzureMcp.Commands;
using AzureMcp.Models.Option;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace AzureMcp.Areas.Server.Commands;

/// <summary>
/// Command to start the MCP server with specified configuration options.
/// This command is hidden from the main command list.
/// </summary>
[HiddenCommand]
public sealed class ServiceStartCommand : BaseCommand
{
    private const string CommandTitle = "Start MCP Server";
    private readonly Option<string> _transportOption = OptionDefinitions.Service.Transport;
    private readonly Option<int> _portOption = OptionDefinitions.Service.Port;
    private readonly Option<string[]?> _serviceTypeOption = OptionDefinitions.Service.ServiceType;

    private readonly Option<bool?> _readOnlyOption = OptionDefinitions.Service.ReadOnly;

    /// <summary>
    /// Gets the name of the command.
    /// </summary>
    public override string Name => "start";

    /// <summary>
    /// Gets the description of the command.
    /// </summary>
    public override string Description => "Starts Azure MCP Server.";

    /// <summary>
    /// Gets the title of the command.
    /// </summary>
    public override string Title => CommandTitle;

    /// <summary>
    /// Registers command options for the service start command.
    /// </summary>
    /// <param name="command">The command to register options with.</param>
    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_transportOption);
        command.AddOption(_portOption);
        command.AddOption(_serviceTypeOption);
        command.AddOption(_readOnlyOption);
    }

    /// <summary>
    /// Executes the service start command, creating and starting the MCP server.
    /// </summary>
    /// <param name="context">The command execution context.</param>
    /// <param name="parseResult">The parsed command options.</param>
    /// <returns>A command response indicating the result of the operation.</returns>
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var port = parseResult.GetValueForOption(_portOption) == default
            ? OptionDefinitions.Service.Port.GetDefaultValue()
            : parseResult.GetValueForOption(_portOption);

        var service = parseResult.GetValueForOption(_serviceTypeOption) == default
            ? OptionDefinitions.Service.ServiceType.GetDefaultValue()
            : parseResult.GetValueForOption(_serviceTypeOption);

        var readOnly = parseResult.GetValueForOption(_readOnlyOption) == default
            ? OptionDefinitions.Service.ReadOnly.GetDefaultValue()
            : parseResult.GetValueForOption(_readOnlyOption);

        var serverOptions = new ServiceStartOptions
        {
            Transport = parseResult.GetValueForOption(_transportOption) ?? TransportTypes.StdIo,
            Port = port,
            Service = service,
            ReadOnly = readOnly,
        };

        using var host = CreateHost(serverOptions);
        await host.StartAsync(CancellationToken.None);
        await host.WaitForShutdownAsync(CancellationToken.None);

        return context.Response;
    }

    /// <summary>
    /// Creates the host for the MCP server with the specified options.
    /// </summary>
    /// <param name="serverOptions">The server configuration options.</param>
    /// <returns>An IHost instance configured for the MCP server.</returns>
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
                    logging.AddConsole();
                })
                .ConfigureServices(services =>
                {
                    Program.ConfigureServices(services);
                    ConfigureMcpServer(services, serverOptions);
                })
                .Build();
        }
    }

    /// <summary>
    /// Configures the MCP server services.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="options">The server configuration options.</param>
    private static void ConfigureMcpServer(IServiceCollection services, ServiceStartOptions options)
    {
        services.AddSingleton<AzureEventSourceLogForwarder>();
        services.AddHostedService<OtelService>();
        services.AddAzureMcpServer(options);
    }

    /// <summary>
    /// Hosted service for running the MCP server using standard input/output.
    /// </summary>
    private sealed class StdioMcpServerHostedService(IMcpServer session) : BackgroundService
    {
        /// <inheritdoc />
        protected override Task ExecuteAsync(CancellationToken stoppingToken) => session.RunAsync(stoppingToken);
    }

    /// <summary>
    /// Resolves and starts the OpenTelemetry services.
    /// </summary>
    private sealed class OtelService : BackgroundService
    {
        private readonly MeterProvider? _meterProvider;
        private readonly TracerProvider? _tracerProvider;
        private readonly LoggerProvider? _loggerProvider;
        private readonly AzureEventSourceLogForwarder _logForwarder;

        /// <summary>
        /// Initializes a new instance of the <see cref="OtelService"/> class.
        /// </summary>
        /// <param name="provider">The service provider.</param>
        public OtelService(IServiceProvider provider)
        {
            _meterProvider = provider.GetService<MeterProvider>();
            _tracerProvider = provider.GetService<TracerProvider>();
            _loggerProvider = provider.GetService<LoggerProvider>();
            _logForwarder = provider.GetRequiredService<AzureEventSourceLogForwarder>();
            _logForwarder.Start();
        }

        /// <inheritdoc />
        protected override Task ExecuteAsync(CancellationToken stoppingToken) => Task.CompletedTask;

        /// <inheritdoc />
        public override void Dispose()
        {
            _meterProvider?.Dispose();
            _tracerProvider?.Dispose();
            _loggerProvider?.Dispose();
            _logForwarder.Dispose();
        }
    }
}
