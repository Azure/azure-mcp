// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.ResourceManager.CognitiveServices;
using AzureMcp.Areas.Foundry.Options.Models;
using AzureMcp.Areas.Foundry.Services;
using AzureMcp.Commands;

namespace AzureMcp.Areas.Foundry.Commands.Models;

public sealed class ModelDeploymentCommand : GlobalCommand<ModelDeploymentOptions>
{
    private const string CommandTitle = "Deploy Model to Azure AI Services";

    public override string Name => "deploy";

    public override string Description =>
        """
        Deploy a model to Azure AI Foundry.

        This function is used to deploy a model on Azure AI Services, allowing users to integrate the model into their applications and utilize its capabilities. This command should not be used for Azure OpenAI Services to deploy OpenAI models.
        """;

    public override string Title => CommandTitle;

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
