// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Mcp.Core.Areas.Server.Commands;
using Azure.Mcp.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Mcp.Core.Areas.Server.Commands;
using Microsoft.Mcp.Core.Areas.Server.Options;

namespace Azure.Mcp.Core.Areas.Server.Commands;

/// <summary>
/// Azure-specific MCP server start command with OpenTelemetry support.
/// </summary>
[HiddenCommand]
public sealed class AzureServiceStartCommand : ServiceStartCommand
{
    /// <summary>
    /// Configures Azure-specific logging including OpenTelemetry.
    /// </summary>
    /// <param name="logging">The logging builder to configure.</param>
    protected override void ConfigureLogging(ILoggingBuilder logging)
    {
        logging.ConfigureOpenTelemetryLogger();
    }

    /// <summary>
    /// Configures Azure-specific services for the MCP server.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="options">The server configuration options.</param>
    protected override void ConfigureServices(IServiceCollection services, ServiceStartOptions options)
    {
        // Add Azure-specific HTTP client services
        services.AddHttpClientServices();
        
        // Add the Azure MCP server services
        services.AddAzureMcpServer(options);
        services.AddHostedService<StdioMcpServerHostedService>();
    }
}