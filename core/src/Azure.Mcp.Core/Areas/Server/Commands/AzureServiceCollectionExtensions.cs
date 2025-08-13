// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Mcp.Core.Areas.Server.Commands;
using Microsoft.Mcp.Core.Areas.Server.Options;

namespace Azure.Mcp.Core.Areas.Server.Commands;

/// <summary>
/// Extension methods for adding Azure MCP server services.
/// </summary>
public static class AzureServiceCollectionExtensions
{
    /// <summary>
    /// Adds Azure MCP server services to the service collection.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="options">The server configuration options.</param>
    /// <returns>The service collection with Azure MCP services added.</returns>
    public static IServiceCollection AddAzureMcpServer(this IServiceCollection services, ServiceStartOptions options)
    {
        // Add the base MCP server services
        services.AddMcpServer(options);
        
        // Add Azure-specific services here
        // TODO: Add Azure-specific telemetry, authentication, etc.
        
        return services;
    }
}