// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.Server.Commands.Discovery;

public sealed class CompositeDiscoveryStrategy(IEnumerable<IMcpDiscoveryStrategy> strategies) : BaseDiscoveryStrategy()
{
    private readonly List<IMcpDiscoveryStrategy> _strategies = new(strategies ?? throw new ArgumentNullException(nameof(strategies)));

    public override async Task<IEnumerable<IMcpServerProvider>> DiscoverServersAsync()
    {
        var tasks = _strategies.Select(strategy => strategy.DiscoverServersAsync());
        var results = await Task.WhenAll(tasks);

        return results.SelectMany(result => result);
    }
}
