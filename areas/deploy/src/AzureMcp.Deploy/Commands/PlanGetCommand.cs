// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Core.Commands;
using AzureMcp.Deploy.Options;
using AzureMcp.Deploy.Services.Util;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Deploy.Commands.Plan;

public sealed class PlanGetCommand(ILogger<PlanGetCommand> logger)
    : BaseCommand()
{
    private const string CommandTitle = "Generate Azure Deployment Plan";
    private readonly ILogger<PlanGetCommand> _logger = logger;

    private readonly Option<string> _workspaceFolderOption = DeployOptionDefinitions.PlanGet.WorkspaceFolder;
    private readonly Option<string> _projectNameOption = DeployOptionDefinitions.PlanGet.ProjectName;
    private readonly Option<string> _deploymentTargetServiceOption = DeployOptionDefinitions.PlanGet.TargetAppService;
    private readonly Option<string> _provisioningToolOption = DeployOptionDefinitions.PlanGet.ProvisioningTool;
    private readonly Option<string> _azdIacOptionsOption = DeployOptionDefinitions.PlanGet.AzdIacOptions;

    public override string Name => "plan-get";

    public override string Description =>
        """
        Entry point to help the agent deploy a service to the cloud. Agent should read its output and generate a deploy plan in '.azure/plan.copilotmd' for execution steps, recommended azure services based on the information agent detected from project. Before calling this tool, please scan this workspace to detect the services to deploy and their dependent services.
        """;

    public override string Title => CommandTitle;
    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = true };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_workspaceFolderOption);
        command.AddOption(_projectNameOption);
        command.AddOption(_deploymentTargetServiceOption);
        command.AddOption(_provisioningToolOption);
        command.AddOption(_azdIacOptionsOption);
    }

    private PlanGetOptions BindOptions(ParseResult parseResult)
    {
        return new PlanGetOptions
        {
            WorkspaceFolder = parseResult.GetValueForOption(_workspaceFolderOption) ?? string.Empty,
            ProjectName = parseResult.GetValueForOption(_projectNameOption) ?? string.Empty,
            TargetAppService = parseResult.GetValueForOption(_deploymentTargetServiceOption) ?? string.Empty,
            ProvisioningTool = parseResult.GetValueForOption(_provisioningToolOption) ?? string.Empty,
            AzdIacOptions = parseResult.GetValueForOption(_azdIacOptionsOption) ?? string.Empty
        };
    }

    public override Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return Task.FromResult(context.Response);
            }

            var planTemplate = DeploymentPlanTemplateUtil.GetPlanTemplate(options.ProjectName, options.TargetAppService, options.ProvisioningTool, options.AzdIacOptions);

            context.Response.Message = planTemplate;
            context.Response.Status = 200;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating deployment plan");
            HandleException(context, ex);
        }
        return Task.FromResult(context.Response);

    }

}
