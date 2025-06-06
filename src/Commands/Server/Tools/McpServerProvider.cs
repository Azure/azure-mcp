using ModelContextProtocol.Client;

namespace AzureMcp.Commands.Server.Tools;

public interface IMcpClientProvider
{
    ClientMetadata CreateMetadata();
    Task<IMcpClient> CreateClientAsync();
}

public class ClientMetadata
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class McpClientProvider : IDisposable
{
    public McpClientProvider(CommandFactory commandFactory)
    {
        _commandFactory = commandFactory ?? throw new ArgumentNullException(nameof(commandFactory));

        var commandGroups = ListCommandGroupProviders();
        foreach (var provider in commandGroups)
        {
            var meta = provider.CreateMetadata();
            _providerMap[meta.Id] = provider;
        }
    }

    public readonly CommandFactory _commandFactory;

    private readonly Dictionary<string, IMcpClientProvider> _providerMap = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, IMcpClient> _clientCache = new(StringComparer.OrdinalIgnoreCase);
    private bool _disposed = false;

    private List<IMcpClientProvider> ListCommandGroupProviders()
    {
        var results = new List<IMcpClientProvider>();
        foreach (var commandGroup in _commandFactory.RootGroup.SubGroup)
        {
            results.Add(new McpCommandGroup(commandGroup));
        }

        return results;
    }

    public List<ClientMetadata> ListProviderMetadata()
    {
        var result = new List<ClientMetadata>();
        foreach (var provider in _providerMap.Values)
        {
            result.Add(provider.CreateMetadata());
        }
        return result;
    }

    public async Task<IMcpClient?> GetProviderClientAsync(string name)
    {
        if (_clientCache.TryGetValue(name, out var cached))
        {
            return cached;
        }

        if (_providerMap.TryGetValue(name, out var provider))
        {
            var client = await provider.CreateClientAsync();
            _clientCache[name] = client;
            return client;
        }

        throw new KeyNotFoundException($"No provider found for name '{name}'.");
    }

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
}
