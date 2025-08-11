// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Areas;
using AzureMcp.Core.Commands;
using AzureMcp.Deploy.Commands.App;
using AzureMcp.Deploy.Commands.Architecture;
using AzureMcp.Deploy.Commands.Infrastructure;
using AzureMcp.Deploy.Commands.Pipeline;
using AzureMcp.Deploy.Commands.Plan;
using AzureMcp.Deploy.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Deploy;

public sealed class DeploySetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IDeployService, DeployService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        var deploy = new CommandGroup("deploy", "Deploy commands for deploying applications to Azure, including sub commands: "
            + "- plan get: generates a deployment plan to construct the infrastructure and deploy the application on Azure. Agent should read its output and generate a deploy plan in '.azure/plan.copilotmd' for execution steps, recommended azure services based on the information agent detected from project. Before calling this tool, please scan this workspace to detect the services to deploy and their dependent services; "
            + "- iac rules get: offers guidelines for creating Bicep/Terraform files to deploy applications on Azure; "
            + "- app logs get: fetch logs from log analytics workspace for Container Apps, App Services, function apps that were deployed through azd; "
            + "- pipeline guidance get: guidance to create a CI/CD pipeline which provision Azure resources and build and deploy applications to Azure; "
            + "- architecture diagram generate: generates an azure service architecture diagram for the application based on the provided app topology; ");
        rootGroup.AddSubGroup(deploy);

        // Application-specific commands
        var appGroup = new CommandGroup("app", "Application-specific deployment tools");
        var logsGroup = new CommandGroup("logs", "Application logs management");
        logsGroup.AddCommand("get", new LogsGetCommand(loggerFactory.CreateLogger<LogsGetCommand>()));
        appGroup.AddSubGroup(logsGroup);
        deploy.AddSubGroup(appGroup);

        // Infrastructure as Code commands
        var iacGroup = new CommandGroup("iac", "Infrastructure as Code operations");
        var rulesGroup = new CommandGroup("rules", "Infrastructure as Code rules and guidelines");
        rulesGroup.AddCommand("get", new RulesGetCommand(loggerFactory.CreateLogger<RulesGetCommand>()));
        iacGroup.AddSubGroup(rulesGroup);
        deploy.AddSubGroup(iacGroup);

        // CI/CD Pipeline commands
        var pipelineGroup = new CommandGroup("pipeline", "CI/CD pipeline operations");
        var guidanceGroup = new CommandGroup("guidance", "CI/CD pipeline guidance");
        guidanceGroup.AddCommand("get", new GuidanceGetCommand(loggerFactory.CreateLogger<GuidanceGetCommand>()));
        pipelineGroup.AddSubGroup(guidanceGroup);
        deploy.AddSubGroup(pipelineGroup);

        // Deployment planning commands
        var planGroup = new CommandGroup("plan", "Deployment planning operations");
        planGroup.AddCommand("get", new GetCommand(loggerFactory.CreateLogger<GetCommand>()));
        deploy.AddSubGroup(planGroup);

        // Architecture diagram commands
        var architectureGroup = new CommandGroup("architecture", "Architecture diagram operations");
        var diagramGroup = new CommandGroup("diagram", "Architecture diagram generation");
        diagramGroup.AddCommand("generate", new DiagramGenerateCommand(loggerFactory.CreateLogger<DiagramGenerateCommand>()));
        architectureGroup.AddSubGroup(diagramGroup);
        deploy.AddSubGroup(architectureGroup);
    }
}
