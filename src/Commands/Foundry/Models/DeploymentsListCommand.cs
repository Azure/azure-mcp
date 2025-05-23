// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.AI.Projects;
using AzureMcp.Options.Foundry.Models;
using AzureMcp.Services.Interfaces;

namespace AzureMcp.Commands.Foundry.Models;

public sealed class DeploymentsListCommand : GlobalCommand<DeploymentsListOptions>
{
    private const string _commandTitle = "List Deployments from Azure AI Services";

    public override string Name => "list";

    public override string Description =>
        """
        Retrieves a list of deployments from Azure AI Services.

        This function is used when a user requests information about the available deployments in Azure AI Services. It provides an overview of the models and services that are currently deployed and available for use.

        Usage:
            Use this function when a user wants to explore the available deployments in Azure AI Services. This can help users understand what models and services are currently operational and how they can be utilized.

        Notes:
            - The deployments listed may include various models and services that are part of Azure AI Services.
            - The list may change frequently as new deployments are added or existing ones are updated.
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
            var deployments = await service.ListDeployments(
                options.Endpoint,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = deployments?.Count > 0 ?
                ResponseResult.Create(
                    new DeploymentsListCommandResult(deployments),
                    FoundryJsonContext.Default.DeploymentsListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record DeploymentsListCommandResult(IEnumerable<Deployment> Deployments);
}
