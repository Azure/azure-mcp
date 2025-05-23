// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.Group;
using AzureMcp.Models.Argument;
using AzureMcp.Models.ResourceGroup;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Group;

public sealed class GroupListCommand(ILogger<GroupListCommand> logger) : SubscriptionCommand<BaseGroupArguments>()
{
    private const string _commandTitle = "List Resource Groups";
    private readonly ILogger<GroupListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        $"""
        List all resource groups in a subscription. This command retrieves all resource groups available
        in the specified {ArgumentDefinitions.Common.SubscriptionName}. Results include resource group names and IDs,
        returned as a JSON array.
        """;

    public override string Title => _commandTitle;

    [McpServerTool(Destructive = false, ReadOnly = true, Title = _commandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindOptions(parseResult);

        try
        {
            var validationResult = Validate(parseResult.CommandResult);

            if (!validationResult.IsValid)
            {
                context.Response.Status = 400;
                context.Response.Message = validationResult.ErrorMessage!;
                return context.Response;
            }

            var resourceGroupService = context.GetService<IResourceGroupService>();
            var groups = await resourceGroupService.GetResourceGroups(
                args.Subscription!,
                args.Tenant,
                args.RetryPolicy);

            context.Response.Results = groups?.Count > 0 ?
                ResponseResult.Create(new Result(groups), JsonSourceGenerationContext.Default.Result) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred listing resource groups.");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record class Result(List<ResourceGroupInfo> Groups);
}
