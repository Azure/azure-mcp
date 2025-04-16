// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.Monitor;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.CommandLine.Parsing;

namespace AzureMcp.Commands.Monitor.Workspace;

public sealed class WorkspaceListCommand : SubscriptionCommand<WorkspaceListArguments>
{
    private readonly ILogger<WorkspaceListCommand> _logger;

    public WorkspaceListCommand(ILogger<WorkspaceListCommand> logger) : base()
    {
        _logger = logger;
    }

    protected override string GetCommandName() => "list";

    protected override string GetCommandDescription() =>
        """
        List Log Analytics workspaces in a subscription. This command retrieves all Log Analytics workspaces 
        available in the specified Azure subscription, displaying their names, IDs, and other key properties. 
        Use this command to identify workspaces before querying their logs or tables.
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

            var monitorService = context.GetService<IMonitorService>();
            var workspaces = await monitorService.ListWorkspaces(
                args.Subscription!,
                args.Tenant,
                args.RetryPolicy);

            context.Response.Results = workspaces?.Count > 0 ?
                new { workspaces } :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing workspaces.");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }
}