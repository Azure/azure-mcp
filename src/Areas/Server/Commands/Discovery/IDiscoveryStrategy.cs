// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using ModelContextProtocol.Client;

namespace AzureMcp.Areas.Server.Commands.Discovery;

public interface IMcpDiscoveryStrategy
{
    /// <summary>
    /// Discovers available MCP servers via this strategy.
    /// </summary>
    /// <returns>A collection of discovered MCP servers.</returns>
    Task<IEnumerable<IMcpServerProvider>> DiscoverServersAsync();

    /// <summary>
    /// Gets an MCP client for the specified server.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="clientOptions"></param>
    /// <returns></returns>
    Task<IMcpClient> GetOrCreateClientAsync(string name, McpClientOptions? clientOptions = null);
}
