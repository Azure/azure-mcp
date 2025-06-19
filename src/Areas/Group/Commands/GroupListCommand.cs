// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Group.Options;
using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Option;
using AzureMcp.Models.ResourceGroup;
using AzureMcp.Services.Azure.ResourceGroup;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Group.Commands;

public sealed class GroupListCommand(ILogger<GroupListCommand> logger) : SubscriptionCommand<BaseGroupOptions>()
{
    private const string CommandTitle = "List Resource Groups";
    private readonly ILogger<GroupListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        $"""
        List all resource groups in a subscription. This command retrieves all resource groups available
        in the specified {OptionDefinitions.Common.SubscriptionName}. Results include resource group names and IDs,
        returned as a JSON array.
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

            var resourceGroupService = context.GetService<IResourceGroupService>();
            var groups = await resourceGroupService.GetResourceGroups(
                options.Subscription!,
                options.Tenant,
                options.RetryPolicy);

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
