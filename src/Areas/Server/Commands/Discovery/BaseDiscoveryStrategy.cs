// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using ModelContextProtocol.Client;

namespace AzureMcp.Areas.Server.Commands.Discovery;

/// <summary>
/// Base class for MCP server discovery strategies that provides common functionality.
/// Implements client caching and server provider lookup by name.
/// </summary>
public abstract class BaseDiscoveryStrategy() : IMcpDiscoveryStrategy
{
    /// <summary>
    /// Cache of MCP clients created by this discovery strategy, keyed by server name (case-insensitive).
    /// </summary>
    protected readonly Dictionary<string, IMcpClient> _clientCache = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Discovers available MCP servers via the implementing strategy.
    /// </summary>
    /// <returns>A collection of discovered MCP server providers.</returns>
    public abstract Task<IEnumerable<IMcpServerProvider>> DiscoverServersAsync();

    /// <summary>
    /// Finds a server provider by name from the discovered servers.
    /// </summary>
    /// <param name="name">The name of the server to find.</param>
    /// <returns>The server provider if found.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when no server with the specified name is found.</exception>
    public async Task<IMcpServerProvider> FindServerProviderAsync(string name)
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name), "Server name cannot be null or empty.");
        }

        var serverProviders = await DiscoverServersAsync();
        foreach (var serverProvider in serverProviders)
        {
            var metadata = serverProvider.CreateMetadata();
            if (metadata.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                return serverProvider;
            }
        }

        throw new KeyNotFoundException($"No MCP server found with the name '{name}'.");
    }

    /// <summary>
    /// Gets an existing MCP client from the cache or creates a new one if not found.
    /// </summary>
    /// <param name="name">The name of the server to get or create a client for.</param>
    /// <param name="clientOptions">Optional client configuration options. If null, default options are used.</param>
    /// <returns>An MCP client that can communicate with the specified server.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the name parameter is null.</exception>
    /// <exception cref="KeyNotFoundException">Thrown when no server with the specified name is found.</exception>
    public async Task<IMcpClient> GetOrCreateClientAsync(string name, McpClientOptions? clientOptions = null)
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name), "Server name cannot be null or empty.");
        }

        if (_clientCache.TryGetValue(name, out var client))
        {
            return client;
        }

        var serverProvider = await FindServerProviderAsync(name);
        client = await serverProvider.CreateClientAsync(clientOptions ?? new McpClientOptions());
        _clientCache[name] = client;

        return client;
    }
}
