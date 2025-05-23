// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.Monitor;
using AzureMcp.Models.Monitor;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Monitor.Workspace;

public sealed class WorkspaceListCommand(ILogger<WorkspaceListCommand> logger) : SubscriptionCommand<WorkspaceListArguments>()
{
    private const string _commandTitle = "List Log Analytics Workspaces";
    private readonly ILogger<WorkspaceListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        """
        List Log Analytics workspaces in a subscription. This command retrieves all Log Analytics workspaces
        available in the specified Azure subscription, displaying their names, IDs, and other key properties.
        Use this command to identify workspaces before querying their logs or tables.
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

            var monitorService = context.GetService<IMonitorService>();
            var workspaces = await monitorService.ListWorkspaces(
                args.Subscription!,
                args.Tenant,
                args.RetryPolicy);

            context.Response.Results = workspaces?.Count > 0 ?
                ResponseResult.Create(
                    new WorkspaceListCommandResult(workspaces),
                    MonitorJsonContext.Default.WorkspaceListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing workspaces.");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record WorkspaceListCommandResult(List<WorkspaceInfo> Workspaces);
}
