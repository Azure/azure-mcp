// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Workbooks.Models;
using AzureMcp.Workbooks.Options;
using AzureMcp.Workbooks.Options.Workbook;
using AzureMcp.Workbooks.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Workbooks.Commands.Workbooks;

public sealed class ShowWorkbooksCommand(ILogger<ShowWorkbooksCommand> logger) : BaseWorkbooksCommand<ShowWorkbooksOptions>
{
    private const string CommandTitle = "Get Workbook";
    private readonly ILogger<ShowWorkbooksCommand> _logger = logger;
    private readonly Option<string> _workbookIdOption = WorkbooksOptionDefinitions.WorkbookId;

    public override string Name => "show";

    public override string Description =>
        """
        Gets information about a specific workbook by its Azure resource ID.
        Returns workbook details including JSON serialized content, display name, description, category,
        location, kind, tags, version, modification time, and other metadata.
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_workbookIdOption);
    }

    protected override ShowWorkbooksOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.WorkbookId = parseResult.GetValueForOption(_workbookIdOption);
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
            var workbook = await workbooksService.GetWorkbook(options.WorkbookId!, options.RetryPolicy, options.Tenant) ?? throw new InvalidOperationException("Failed to retrieve workbook");

            context.Response.Results = ResponseResult.Create(
                new ShowWorkbooksCommandResult(workbook),
                WorkbooksJsonContext.Default.ShowWorkbooksCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving workbook with ID: {WorkbookId}", options.WorkbookId);
            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record ShowWorkbooksCommandResult(WorkbookInfo Workbook);
}
