// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Reflection;
using System.Text.Json;
using AzureMcp.Areas.Server.Models;

namespace AzureMcp.Areas.Server.Commands.Discovery;

/// <summary>
/// Discovers MCP servers from an embedded registry.json resource file.
/// This strategy loads server configurations from a JSON resource bundled with the assembly.
/// </summary>
public sealed class RegistryDiscoveryStrategy() : BaseDiscoveryStrategy()
{
    /// <summary>
    /// Discovers available MCP servers from the embedded registry.
    /// </summary>
    /// <returns>A collection of server providers defined in the registry.</returns>
    public override async Task<IEnumerable<IMcpServerProvider>> DiscoverServersAsync()
    {
        var registryRoot = await LoadRegistryAsync();
        if (registryRoot == null)
        {
            return Enumerable.Empty<IMcpServerProvider>();
        }

        return registryRoot
            .Servers!
            .Select(s => new RegistryServerProvider(s.Key, s.Value))
            .Cast<IMcpServerProvider>();
    }

    /// <summary>
    /// Loads the registry configuration from the embedded resource file.
    /// </summary>
    /// <returns>The deserialized registry root containing server configurations, or null if not found.</returns>
    private async Task<RegistryRoot?> LoadRegistryAsync()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = assembly
            .GetManifestResourceNames()
            .FirstOrDefault(n => n.EndsWith("registry.json", StringComparison.OrdinalIgnoreCase));

        if (resourceName is null)
        {
            return null;
        }

        await using var stream = assembly.GetManifestResourceStream(resourceName)!;
        var registry = await JsonSerializer.DeserializeAsync(stream, ServerJsonContext.Default.RegistryRoot);

        if (registry?.Servers != null)
        {
            foreach (var kvp in registry.Servers)
            {
                if (kvp.Value != null)
                {
                    kvp.Value.Name = kvp.Key;
                }
            }
        }

        return registry;
    }
}
