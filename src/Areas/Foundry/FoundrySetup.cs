// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Foundry.Commands.Evaluation;
using AzureMcp.Areas.Foundry.Commands.Models;
using AzureMcp.Areas.Foundry.Services;
using AzureMcp.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Foundry;

public class FoundrySetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IFoundryService, FoundryService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        var foundry = new CommandGroup("foundry", "Foundry service operations - Commands for listing and managing services and resources in AI Foundry.");
        rootGroup.AddSubGroup(foundry);

        var models = new CommandGroup("models", "Foundry models operations - Commands for listing and managing models in AI Foundry.");
        foundry.AddSubGroup(models);

        var deployments = new CommandGroup("deployments", "Foundry models deployments operations - Commands for listing and managing models deployments in AI Foundry.");
        models.AddSubGroup(deployments);

        var evaluation = new CommandGroup("evaluation", "Foundry evaluation evaluations operations - Commands for working with evaluation services and resources in AI Foundry.");
        foundry.AddSubGroup(evaluation);

        var agents = new CommandGroup("agents", "Foundry evaluation agents operations - Commands for listing and managing evaluation agents in AI Foundry.");
        evaluation.AddSubGroup(agents);

        deployments.AddCommand("list", new DeploymentsListCommand());

        models.AddCommand("list", new ModelsListCommand());
        models.AddCommand("deploy", new ModelDeploymentCommand());

        agents.AddCommand("list", new AgentsListCommand());
        agents.AddCommand("connect", new AgentsConnectCommand());
    }
}
