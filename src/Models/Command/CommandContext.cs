// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.Extensions.DependencyInjection;
using ModelContextProtocol.Server;
using AzureMcp.Services.Interfaces.MCP;

namespace AzureMcp.Models.Command;

/// <summary>
/// Provides context for command execution including service access and response management
/// </summary>
public class CommandContext
{
    /// <summary>
    /// The service provider for dependency injection
    /// </summary>
    private readonly IServiceProvider _serviceProvider;
    
    /// <summary>
    /// The MCPServer to send real-time results
    /// </summary>
    private readonly IMcpServer? _mcpServer;
    
    /// <summary>
    /// The extensions for MCPServer that add additional capabilities
    /// </summary>
    private readonly IMcpServerExtensions? _mcpServerExtensions;

    /// <summary>
    /// The response object that will be returned to the client
    /// </summary>
    public CommandResponse Response { get; }

    /// <summary>
    /// Creates a new command context
    /// </summary>
    /// <param name="serviceProvider">The service provider for dependency injection</param>
    public CommandContext(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _mcpServer = serviceProvider.GetService<IMcpServer>();
        _mcpServerExtensions = serviceProvider.GetService<IMcpServerExtensions>();
        
        Response = new CommandResponse
        {
            Status = 200,
            Message = "Success",
            Arguments = []
        };
    }

    /// <summary>
    /// Gets a required service from the service provider
    /// </summary>
    /// <typeparam name="T">The type of service to retrieve</typeparam>
    /// <returns>The requested service instance</returns>
    /// <exception cref="InvalidOperationException">Thrown if the service is not registered</exception>
    public T GetService<T>() where T : class
    {
        return _serviceProvider.GetRequiredService<T>();
    }
    
    /// <summary>
    /// Sends real-time results to the client during long-running operations
    /// </summary>
    /// <param name="response">The response containing real-time results to send</param>
    public void SendRealtimeResults(CommandResponse response)
    {
        _mcpServerExtensions?.SendRealtimeResults(response);
    }
}