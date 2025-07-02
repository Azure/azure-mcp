// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.Server.Commands.Discovery;

/// <summary>
/// A discovery strategy that combines multiple discovery strategies into one.
/// This allows discovering servers from multiple sources and aggregating the results.
/// </summary>
/// <param name="strategies">The collection of discovery strategies to combine.</param>
public sealed class CompositeDiscoveryStrategy(IEnumerable<IMcpDiscoveryStrategy> strategies) : BaseDiscoveryStrategy()
{
    private readonly List<IMcpDiscoveryStrategy> _strategies = InitializeStrategies(strategies);

    /// <summary>
    /// Initializes the list of discovery strategies, validating that at least one is provided.
    /// </summary>
    /// <param name="strategies">The collection of discovery strategies to initialize.</param>
    /// <returns>A list of initialized discovery strategies.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the strategies parameter is null.</exception>
    /// <exception cref="ArgumentException">Thrown when no discovery strategies are provided.</exception>
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

    /// <summary>
    /// Discovers available MCP servers from all combined discovery strategies.
    /// </summary>
    /// <returns>A collection of all discovered MCP server providers from all strategies.</returns>
    public override async Task<IEnumerable<IMcpServerProvider>> DiscoverServersAsync()
    {
        var tasks = _strategies.Select(strategy => strategy.DiscoverServersAsync());
        var results = await Task.WhenAll(tasks);

        return results.SelectMany(result => result);
    }
}
