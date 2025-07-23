// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Deploy.Options;
using AzureMcp.Areas.Deploy.Services.Util;
using AzureMcp.Commands.Subscription;
using AzureMcp.Services.Telemetry;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Deploy.Commands;

public sealed class PipelineGenerateCommand(ILogger<PipelineGenerateCommand> logger)
    : SubscriptionCommand<PipelineGenerateOptions>()
{
    private const string CommandTitle = "Get Azure Deployment CICD Pipeline Guidance";
    private readonly ILogger<PipelineGenerateCommand> _logger = logger;

    private readonly Option<bool> _useAZDPipelineConfigOption = DeployOptionDefinitions.PipelineGenerateOptions.UseAZDPipelineConfig;
    private readonly Option<string> _organizationNameOption = DeployOptionDefinitions.PipelineGenerateOptions.OrganizationName;
    private readonly Option<string> _repositoryNameOption = DeployOptionDefinitions.PipelineGenerateOptions.RepositoryName;
    private readonly Option<string> _githubEnvironmentNameOption = DeployOptionDefinitions.PipelineGenerateOptions.GithubEnvironmentName;

    public override string Name => "cicd-pipeline-guidance-get";

    public override string Description =>
        """
        Guidance to create a CI/CD pipeline which provision Azure resources and build and deploy applications to Azure. Use this tool BEFORE generating/creating a Github actions workflow file for DEPLOYMENT on Azure. Infrastructure files should be ready and the application should be ready to be containerized.
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_useAZDPipelineConfigOption);
        command.AddOption(_organizationNameOption);
        command.AddOption(_repositoryNameOption);
        command.AddOption(_githubEnvironmentNameOption);
    }

    protected override PipelineGenerateOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.UseAZDPipelineConfig = parseResult.GetValueForOption(_useAZDPipelineConfigOption);
        options.OrganizationName = parseResult.GetValueForOption(_organizationNameOption);
        options.RepositoryName = parseResult.GetValueForOption(_repositoryNameOption);
        options.GithubEnvironmentName = parseResult.GetValueForOption(_githubEnvironmentNameOption);
        return options;
    }

    [McpServerTool(
        Destructive = false,
        ReadOnly = false,
        Title = CommandTitle)]
    public override Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return Task.FromResult(context.Response);
            }
            context.Activity?.WithSubscriptionTag(options);
            var result = PipelineGenerationUtil.GeneratePipelineGuidelines(options);

            context.Response.Message = result;
            context.Response.Status = 200;
        }
        catch (Exception ex)
        {
            HandleException(context, ex);
        }
        return Task.FromResult(context.Response);
    }

}
