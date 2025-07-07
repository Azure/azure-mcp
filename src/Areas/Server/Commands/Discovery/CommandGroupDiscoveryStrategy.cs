// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Server.Options;
using AzureMcp.Commands;
using Microsoft.Extensions.Options;

namespace AzureMcp.Areas.Server.Commands.Discovery;

/// <summary>
/// Discovery strategy that exposes command groups as MCP servers.
/// This strategy converts Azure CLI command groups into MCP servers, allowing them to be accessed via the MCP protocol.
/// </summary>
/// <param name="commandFactory">The command factory used to access available command groups.</param>
/// <param name="options">Options for configuring the service behavior.</param>
public sealed class CommandGroupDiscoveryStrategy(CommandFactory commandFactory, IOptions<ServiceStartOptions> options) : BaseDiscoveryStrategy()
{
    private readonly CommandFactory _commandFactory = commandFactory;
    private readonly IOptions<ServiceStartOptions> _options = options;

    /// <summary>
    /// Gets or sets the entry point to use for the command group servers.
    /// This can be used to specify a custom entry point for the commands.
    /// </summary>
    public string? EntryPoint { get; set; } = null;

    /// <summary>
    /// Discovers available command groups and converts them to MCP server providers.
    /// </summary>
    /// <returns>A collection of command group server providers.</returns>
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
