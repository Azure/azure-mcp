// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands.Subscription;
using AzureMcp.Workbooks.Models;
using AzureMcp.Workbooks.Options;
using AzureMcp.Workbooks.Options.Workbook;
using AzureMcp.Workbooks.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Workbooks.Commands.Workbooks;

public sealed class ListWorkbooksCommand(ILogger<ListWorkbooksCommand> logger) : SubscriptionCommand<ListWorkbooksOptions>
{
    private const string CommandTitle = "List Workbooks";
    private readonly ILogger<ListWorkbooksCommand> _logger = logger;

    private readonly Option<string> _kindOption = WorkbooksOptionDefinitions.Kind;
    private readonly Option<string> _categoryOption = WorkbooksOptionDefinitions.Category;
    private readonly Option<string> _sourceIdOption = WorkbooksOptionDefinitions.SourceIdFilter;

    public override string Name => "list";

    public override string Description =>
        """
        List all workbooks in a specific resource group. This command retrieves all workbooks available
        in the specified resource group within the given subscription. Resource group is required.
        Optionally filter by kind (shared/user), category (workbook/sentinel/etc), or source resource ID.
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_resourceGroupOption);
        command.AddOption(_kindOption);
        command.AddOption(_categoryOption);
        command.AddOption(_sourceIdOption);
    }

    protected override ListWorkbooksOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption);
        options.Kind = parseResult.GetValueForOption(_kindOption);
        options.Category = parseResult.GetValueForOption(_categoryOption);
        options.SourceId = parseResult.GetValueForOption(_sourceIdOption);
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
            var filters = options.ToFilters();
            var workbooks = await workbooksService.ListWorkbooks(
                options.Subscription!,
                options.ResourceGroup!,
                filters,
                options.RetryPolicy,
                options.Tenant);

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
