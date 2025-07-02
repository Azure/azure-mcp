// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.Server.Commands.Discovery;

public sealed class CompositeDiscoveryStrategy(IEnumerable<IMcpDiscoveryStrategy> strategies) : BaseDiscoveryStrategy()
{
    private readonly List<IMcpDiscoveryStrategy> _strategies = InitializeStrategies(strategies);

    private static List<IMcpDiscoveryStrategy> InitializeStrategies(IEnumerable<IMcpDiscoveryStrategy> strategies)
    {
        ArgumentNullException.ThrowIfNull(strategies);

        var strategyList = new List<IMcpDiscoveryStrategy>(strategies);

        if (strategyList.Count == 0)
        {
            throw new ArgumentException("At least one discovery strategy must be provided.", nameof(strategies));
        }

        return strategyList;
    }

    public override async Task<IEnumerable<IMcpServerProvider>> DiscoverServersAsync()
    {
        var tasks = _strategies.Select(strategy => strategy.DiscoverServersAsync());
        var results = await Task.WhenAll(tasks);

        return results.SelectMany(result => result);
    }
}
