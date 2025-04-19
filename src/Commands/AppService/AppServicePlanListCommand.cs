// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments;
using AzureMcp.Arguments.Group;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.CommandLine.Parsing;

namespace AzureMcp.Commands.AppService;

public sealed class AppServicePlanListCommand : SubscriptionCommand<AppServiceArguments>
{
    private readonly ILogger<AppServicePlanListCommand> _logger;

    public AppServicePlanListCommand(ILogger<AppServicePlanListCommand> logger) : base()
    {
        _logger = logger;
    }

    protected override string GetCommandName() => "list";

    protected override string GetCommandDescription() =>
        $"""
        List all app service plans in a subscription. This command retrieves all app service plans available
        in the specified {ArgumentDefinitions.Common.SubscriptionName}. Results include app service plan names and IDs,
        returned as a JSON array.
        """;

    [McpServerTool(Destructive = false, ReadOnly = true)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindArguments(parseResult);

        try
        {
            if (!await ProcessArguments(context, args))
            {
                return context.Response;
            }

            var appServicePlanService = context.GetService<IAppServiceService>();
            var appServicePlans = await appServicePlanService.ListAppServicePlans(
                args.Subscription!,
                args.Tenant,
                args.RetryPolicy);

            context.Response.Results = appServicePlans?.Count > 0 ?
                new { appServicePlans } :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred listing app service plans.");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }
}