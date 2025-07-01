// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using ModelContextProtocol.Client;

namespace AzureMcp.Areas.Server.Commands.Discovery;

public abstract class BaseDiscoveryStrategy() : IMcpDiscoveryStrategy
{
    protected readonly Dictionary<string, IMcpClient> _clientCache = new();

    public abstract Task<IEnumerable<IMcpServerProvider>> DiscoverServersAsync();

    public IMcpServerProvider FindServerProvider(string name)
    {
        var serverProviders = DiscoverServersAsync().GetAwaiter().GetResult();
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

    public async Task<IMcpClient> GetOrCreateClientAsync(string name, McpClientOptions? clientOptions = null)
    {
        if (_clientCache.TryGetValue(name, out var client))
        {
            return client;
        }

        var serverProvider = FindServerProvider(name);
        client = await serverProvider.CreateClientAsync(clientOptions ?? new McpClientOptions());
        _clientCache[name] = client;

        return client;
    }
}
