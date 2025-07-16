// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Workbooks.Models;
using AzureMcp.Areas.Workbooks.Options.Workbook;
using AzureMcp.Areas.Workbooks.Services;
using AzureMcp.Commands.Subscription;
using AzureMcp.Commands;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Workbooks.Commands.Workbook;

public sealed class ListWorkbooksCommand(ILogger<ListWorkbooksCommand> logger) : SubscriptionCommand<ListWorkbooksOptions>
{
    private const string CommandTitle = "List Workbooks";
    private readonly ILogger<ListWorkbooksCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        """
        List all workbooks in a specific resource group. This command retrieves all workbooks available
        in the specified resource group within the given subscription. Resource group is required.
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_resourceGroupOption);
    }

    protected override ListWorkbooksOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption);
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

            var workbooksService = context.GetService<IWorkbooksService>();
            var workbooks = await workbooksService.ListWorkbooks(options.Subscription, options.ResourceGroup, options.RetryPolicy);

            context.Response.Results = workbooks?.Count > 0
                ? ResponseResult.Create(new ListWorkbooksCommandResult(workbooks), WorkbooksJsonContext.Default.ListWorkbooksCommandResult)
                : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing workbooks for subscription: {Subscription}", options.Subscription);
            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record ListWorkbooksCommandResult(List<WorkbookInfo> Workbooks);
}
