// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.ResourceManager.CognitiveServices;
using AzureMcp.Options.Foundry.Models;
using AzureMcp.Services.Interfaces;

namespace AzureMcp.Commands.Foundry.Models;

public sealed class ModelDeploymentCommand : GlobalCommand<ModelDeploymentOptions>
{
    private const string _commandTitle = "Deploy Model to Azure AI Services";

    public override string Name => "deploy";

    public override string Description =>
        """
        Deploy a model to Azure AI.

        This function is used to deploy a model on Azure AI Services, allowing users to integrate the model into their applications and utilize its capabilities.
        """;

    public override string Title => _commandTitle;

    [McpServerTool(Destructive = false, ReadOnly = true, Title = _commandTitle)]
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
                options.DeploymentName,
                options.ModelName,
                options.ModelFormat,
                options.AzureAiServicesName,
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

    internal record ModelDeploymentCommandResult(CognitiveServicesAccountDeploymentResource DeploymentResource);
}
