// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Reflection;
using AzureMcp.Areas.Server.Models;
using AzureMcp.Models.Server;

namespace AzureMcp.Areas.Server.Commands.Discovery;

public sealed class RegistryDiscoveryStrategy() : BaseDiscoveryStrategy()
{
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
        var registry = await JsonSerializer.DeserializeAsync(stream, RegistryJsonContext.Default.RegistryRoot);

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
