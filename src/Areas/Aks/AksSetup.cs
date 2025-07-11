// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Commands;
using AzureMcp.Commands.Aks;
using AzureMcp.Services.Azure.Aks;
using AzureMcp.Services.Interfaces;
using AzureMcp.Services.ToolInstallation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Aks;

public class AksSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IAksService, AksService>();
        services.AddSingleton<IToolInstallationService, ToolInstallationService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        var aksGroup = new CommandGroup("aks", "Azure Kubernetes Service (AKS) commands");

        // Add kubectl command
        aksGroup.Commands.Add("kubectl", new KubectlCommand(loggerFactory.CreateLogger<KubectlCommand>()));

        // Add cluster management commands
        var clusterGroup = new CommandGroup("cluster", "AKS cluster management commands");
        clusterGroup.Commands.Add("list", new ClusterListCommand(loggerFactory.CreateLogger<ClusterListCommand>()));
        clusterGroup.Commands.Add("get", new ClusterGetCommand(loggerFactory.CreateLogger<ClusterGetCommand>()));

        aksGroup.SubGroup.Add(clusterGroup);
        rootGroup.SubGroup.Add(aksGroup);
    }
}
