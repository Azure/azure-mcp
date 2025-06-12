// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using ModelContextProtocol.Client;

namespace AzureMcp.Commands.Server.Tools;

/// <summary>
/// Providers metadata and MCP client creation for a discovered MCP server.
/// </summary>
public interface IMcpClientProvider
{
    McpServerMetadata CreateMetadata();
    Task<IMcpClient> CreateClientAsync(McpClientOptions clientOptions);
}

/// <summary>
/// Represents metadata for an MCP server provider.
/// </summary>
public class McpServerMetadata
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

/// <summary>
/// Provides MCP client instances for various server providers.
/// </summary>
public sealed class McpClientService : IMcpClientService, IDisposable
{
    public readonly CommandFactory _commandFactory;
    private readonly Dictionary<string, IMcpClientProvider> _providerMap = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, IMcpClient> _clientCache = new(StringComparer.OrdinalIgnoreCase);
    private bool _disposed = false;

    /// <summary>
    /// Initializes a new instance of the <see cref="McpClientService"/> class.
    /// </summary>
    /// <param name="commandFactory">The command factory used to discover command groups.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="commandFactory"/> is null.</exception>
    public McpClientService(CommandFactory commandFactory, string entryPoint = "")
    {
        _commandFactory = commandFactory ?? throw new ArgumentNullException(nameof(commandFactory));

        var commandGroups = ListCommandGroupProviders(entryPoint);
        foreach (var provider in commandGroups)
        {
            var meta = provider.CreateMetadata();
            _providerMap[meta.Id] = provider;
        }
    }

    /// <summary>
    /// Lists all available MCP server providers with their metadata.
    /// </summary>
    /// <returns>A list of <see cref="McpServerMetadata"/> for all providers.</returns>
    public List<McpServerMetadata> ListProviderMetadata()
    {
        var result = new List<McpServerMetadata>();
        foreach (var provider in _providerMap.Values)
        {
            result.Add(provider.CreateMetadata());
        }
        return result;
    }

    /// <summary>
    /// Retrieves an MCP client for a specific provider by name.
    /// </summary>
    /// <param name="name">The provider name.</param>
    /// <returns>The <see cref="IMcpClient"/> instance, or null if not found.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if no provider is found for the given name.</exception>
    public async Task<IMcpClient?> GetProviderClientAsync(string name, McpClientOptions clientOptions)
    {
        if (_clientCache.TryGetValue(name, out var cached))
        {
            return cached;
        }

        if (_providerMap.TryGetValue(name, out var provider))
        {
            var client = await provider.CreateClientAsync(clientOptions);
            _clientCache[name] = client;
            return client;
        }

        throw new KeyNotFoundException($"No provider found for name '{name}'.");
    }

    /// <summary>
    /// Disposes of the MCP client provider, releasing all cached clients.
    /// </summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        foreach (var client in _clientCache.Values)
        {
            if (client is IDisposable d)
            {
                d.Dispose();
            }
        }
        _disposed = true;
    }

    /// <summary>
    /// Discovers all command group providers from the command factory.
    /// </summary>
    /// <returns>A list of <see cref="IMcpClientProvider"/>.</returns>
    private List<IMcpClientProvider> ListCommandGroupProviders(string entryPoint = "")
    {
        var results = new List<IMcpClientProvider>();
        foreach (var commandGroup in _commandFactory.RootGroup.SubGroup)
        {
            results.Add(new McpCommandGroup(commandGroup, entryPoint));
        }
        return results;
    }
}
