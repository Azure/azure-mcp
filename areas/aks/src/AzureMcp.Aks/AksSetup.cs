// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Aks.Commands.Cluster;
using AzureMcp.Aks.Services;
using AzureMcp.Core.Areas;
using AzureMcp.Core.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Aks;

public class AksSetup : IAreaSetup
{
    public string Name => "aks";

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IAksService, AksService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        // Create AKS command group
        var aks = new CommandGroup(Name, "Azure Kubernetes Service operations - Commands for managing Azure Kubernetes Service (AKS) cluster resources. Includes operations for listing clusters, retrieving cluster configurations, and managing Kubernetes environments in Azure.");
        rootGroup.AddSubGroup(aks);

        // Create AKS subgroups
        var cluster = new CommandGroup("cluster", "AKS cluster operations - Commands for listing and managing AKS clusters in your Azure subscription.");
        aks.AddSubGroup(cluster);

        // Register AKS commands
        cluster.AddCommand("list", new ClusterListCommand(loggerFactory.CreateLogger<ClusterListCommand>()));
        cluster.AddCommand("get", new ClusterGetCommand(loggerFactory.CreateLogger<ClusterGetCommand>()));
    }
}
