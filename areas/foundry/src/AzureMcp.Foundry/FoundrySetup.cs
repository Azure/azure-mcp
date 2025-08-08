// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Areas;
using AzureMcp.Core.Commands;
using AzureMcp.Foundry.Commands;
using AzureMcp.Foundry.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Foundry;

public class FoundrySetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IFoundryService, FoundryService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        var foundry = new CommandGroup("foundry", "Foundry service operations - Commands for listing and managing Azure AI Foundry services and resources including AI models, endpoints, deployments, and workspace configurations. Use this tool when you need to work with Azure AI Foundry projects, list available AI models, manage model deployments, configure AI endpoints, or query AI workspace resources and settings. This tool is designed for AI and machine learning workflow management within Azure AI Foundry environments. Do not use this tool for general Azure AI services outside of Foundry, Azure Machine Learning studio operations, or Azure OpenAI direct management - this tool specifically targets Azure AI Foundry service operations. This tool is a hierarchical MCP command router where sub-commands are routed to MCP servers that require specific fields inside the \"parameters\" object. To invoke a command, set \"command\" and wrap its arguments in \"parameters\". Set \"learn=true\" to discover available sub-commands for different AI Foundry operations and resource types.");
        rootGroup.AddSubGroup(foundry);

        var models = new CommandGroup("models", "Foundry models operations - Commands for listing and managing models in AI Foundry.");
        foundry.AddSubGroup(models);

        var deployments = new CommandGroup("deployments", "Foundry models deployments operations - Commands for listing and managing models deployments in AI Foundry.");
        models.AddSubGroup(deployments);

        deployments.AddCommand("list", new DeploymentsListCommand());

        models.AddCommand("list", new ModelsListCommand());
        models.AddCommand("deploy", new ModelDeploymentCommand());
    }
}
