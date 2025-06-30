// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.Server.Commands.Discovery;

public sealed class CompositeDiscoveryStrategy : BaseDiscoveryStrategy
{
    public CompositeDiscoveryStrategy(IEnumerable<IMcpDiscoveryStrategy> strategies)
    {
        ArgumentNullException.ThrowIfNull(strategies);
        _strategies.AddRange(strategies);
    }

    private readonly List<IMcpDiscoveryStrategy> _strategies = new();

    public override async Task<IEnumerable<IMcpServerProvider>> DiscoverServersAsync()
    {
        var tasks = _strategies.Select(strategy => strategy.DiscoverServersAsync());
        var results = await Task.WhenAll(tasks);

        return results.SelectMany(result => result);
    }
}
