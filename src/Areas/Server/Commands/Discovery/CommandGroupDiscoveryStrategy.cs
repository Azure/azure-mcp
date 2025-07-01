// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Server.Options;
using AzureMcp.Commands;
using Microsoft.Extensions.Options;

namespace AzureMcp.Areas.Server.Commands.Discovery;

public sealed class CommandGroupDiscoveryStrategy(CommandFactory commandFactory, IOptions<ServiceStartOptions> options) : BaseDiscoveryStrategy()
{
    private readonly CommandFactory _commandFactory = commandFactory;
    private readonly IOptions<ServiceStartOptions> _options = options;

    public string? EntryPoint { get; set; } = null;

    public override Task<IEnumerable<IMcpServerProvider>> DiscoverServersAsync()
    {
        var ignoreCommandGroups = new List<string> { "extension", "server", "tools" };

        var providers = _commandFactory.RootGroup.SubGroup
            .Where(group => !ignoreCommandGroups.Contains(group.Name, StringComparer.OrdinalIgnoreCase))
            .Select(group => new CommandGroupServerProvider(group)
            {
                ReadOnly = _options.Value.ReadOnly ?? false,
                EntryPoint = EntryPoint,
            })
            .Cast<IMcpServerProvider>();

        return Task.FromResult(providers);
    }
}
