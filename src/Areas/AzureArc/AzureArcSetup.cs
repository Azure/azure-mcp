// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.AzureArc.Services;
using AzureMcp.Commands;
using AzureMcp.Commands.Arc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.AzureArc;

public class AzureArcSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IArcServices, ArcService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        // Create Azure Arc command group
        var arc = new CommandGroup("arc", "Azure Arc operations - Commands for managing Azure Arc-enabled Kubernetes clusters and AKS Edge Essentials.");
        rootGroup.AddSubGroup(arc);

        // Register Azure Arc commands
        arc.AddCommand("onboard-cluster-to-arc", new OnboardClusterToArcCommand(loggerFactory.CreateLogger<OnboardClusterToArcCommand>()));
        arc.AddCommand("disconnect-from-azure-arc", new DisconnectFromAzureArcCommand(loggerFactory.CreateLogger<DisconnectFromAzureArcCommand>()));
        arc.AddCommand("remove-Aks-Edge-installation", new RemoveAksEdgeCommand(loggerFactory.CreateLogger<RemoveAksEdgeCommand>()));
        arc.AddCommand("validate-prerequisites-aksee-cluster", new ValidatePrerequisitesForAksEdgeClusterCommand(loggerFactory.CreateLogger<ValidatePrerequisitesForAksEdgeClusterCommand>()));
        arc.AddCommand("validate-system-requirements-hyperv", new ValidateSystemRequirementsAndSetupHyperVCommand(loggerFactory.CreateLogger<ValidateSystemRequirementsAndSetupHyperVCommand>()));
        arc.AddCommand("validate-install-software-requirements", new ValidateAndInstallSwRequirementCommand(loggerFactory.CreateLogger<ValidateAndInstallSwRequirementCommand>()));
        arc.AddCommand("quick-deploy-aks-edge-essentials", new QuickDeployAksEdgeEssentialsCommand(loggerFactory.CreateLogger<QuickDeployAksEdgeEssentialsCommand>()));
    }
}
