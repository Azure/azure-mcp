// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Foundry.Options;
using AzureMcp.Areas.Foundry.Options.Models;
using AzureMcp.Areas.Foundry.Services;
using AzureMcp.Commands.Subscription;

namespace AzureMcp.Areas.Foundry.Commands.Models;

public sealed class ModelDeploymentCommand : SubscriptionCommand<ModelDeploymentOptions>
{
    private const string CommandTitle = "Deploy Model to Azure AI Services";
    private readonly Option<string> _deploymentNameOption = FoundryOptionDefinitions.DeploymentNameOption;
    private readonly Option<string> _modelNameOption = FoundryOptionDefinitions.ModelNameOption;
    private readonly Option<string> _modelFormatOption = FoundryOptionDefinitions.ModelFormatOption;
    private readonly Option<string> _azureAiServicesNameOption = FoundryOptionDefinitions.AzureAiServicesNameOption;
    private readonly Option<string> _modelVersionOption = FoundryOptionDefinitions.ModelVersionOption;
    private readonly Option<string> _modelSourceOption = FoundryOptionDefinitions.ModelSourceOption;
    private readonly Option<string> _skuNameOption = FoundryOptionDefinitions.SkuNameOption;
    private readonly Option<int> _skuCapacityOption = FoundryOptionDefinitions.SkuCapacityOption;
    private readonly Option<string> _scaleTypeOption = FoundryOptionDefinitions.ScaleTypeOption;
    private readonly Option<int> _scaleCapacityOption = FoundryOptionDefinitions.ScaleCapacityOption;

    public override string Name => "deploy";

    public override string Description =>
        """
        Deploy a model to Azure AI Foundry.

        This function is used to deploy a model on Azure AI Services, allowing users to integrate the model into their applications and utilize its capabilities. This command should not be used for Azure OpenAI Services to deploy OpenAI models.
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_deploymentNameOption);
        command.AddOption(_modelNameOption);
        command.AddOption(_modelFormatOption);
        command.AddOption(_azureAiServicesNameOption);
        command.AddOption(_resourceGroupOption);
        command.AddOption(_modelVersionOption);
        command.AddOption(_modelSourceOption);
        command.AddOption(_skuNameOption);
        command.AddOption(_skuCapacityOption);
        command.AddOption(_scaleTypeOption);
        command.AddOption(_scaleCapacityOption);
    }

    protected override ModelDeploymentOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.DeploymentName = parseResult.GetValueForOption(_deploymentNameOption);
        options.ModelName = parseResult.GetValueForOption(_modelNameOption);
        options.ModelFormat = parseResult.GetValueForOption(_modelFormatOption);
        options.AzureAiServicesName = parseResult.GetValueForOption(_azureAiServicesNameOption);
        options.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption);
        options.ModelVersion = parseResult.GetValueForOption(_modelVersionOption);
        options.ModelSource = parseResult.GetValueForOption(_modelSourceOption);
        options.SkuName = parseResult.GetValueForOption(_skuNameOption);
        options.SkuCapacity = parseResult.GetValueForOption(_skuCapacityOption);
        options.ScaleType = parseResult.GetValueForOption(_scaleTypeOption);
        options.ScaleCapacity = parseResult.GetValueForOption(_scaleCapacityOption);

        return options;
    }

    [McpServerTool(Destructive = false, ReadOnly = true, Title = CommandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            var service = context.GetService<IFoundryService>();
            var deploymentResource = await service.DeployModel(
                options.DeploymentName!,
                options.ModelName!,
                options.ModelFormat!,
                options.AzureAiServicesName!,
                options.ResourceGroup!,
                options.Subscription!,
                options.ModelVersion,
                options.ModelSource,
                options.SkuName,
                options.SkuCapacity,
                options.ScaleType,
                options.ScaleCapacity,
                options.RetryPolicy);

            context.Response.Results =
                ResponseResult.Create(
                    new ModelDeploymentCommandResult(deploymentResource),
                    FoundryJsonContext.Default.ModelDeploymentCommandResult);
        }
        catch (Exception ex)
        {
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record ModelDeploymentCommandResult(Dictionary<string, object> DeploymentData);
}
