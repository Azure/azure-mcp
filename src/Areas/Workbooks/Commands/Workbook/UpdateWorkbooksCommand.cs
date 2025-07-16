// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Workbooks.Models;
using AzureMcp.Areas.Workbooks.Options.Workbook;
using AzureMcp.Areas.Workbooks.Services;
using AzureMcp.Commands;
using AzureMcp.Areas.Workbooks.Options;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Workbooks.Commands.Workbook;

public sealed class UpdateWorkbooksCommand(ILogger<UpdateWorkbooksCommand> logger) : BaseWorkbooksCommand<UpdateWorkbooksOptions>
{
    private const string CommandTitle = "Update Workbook";
    private readonly ILogger<UpdateWorkbooksCommand> _logger = logger;
    private readonly Option<string> _workbookIdOption = WorkbooksOptionDefinitions.WorkbookId;
    private readonly Option<string> _titleOption = WorkbooksOptionDefinitions.Title;
    private readonly Option<string> _serializedContentOption = WorkbooksOptionDefinitions.SerializedContent;

    public override string Name => "update";

    public override string Description =>
        """
        Update limited aspects of a workbook including its title (display name) and serialized content.
        At least one property must be provided for the update operation.
        Returns the updated workbook information upon successful completion.
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_workbookIdOption);
        command.AddOption(_titleOption);
        command.AddOption(_serializedContentOption);
    }

    protected override UpdateWorkbooksOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.WorkbookId = parseResult.GetValueForOption(_workbookIdOption);
        options.Title = parseResult.GetValueForOption(_titleOption);
        options.SerializedContent = parseResult.GetValueForOption(_serializedContentOption);
        return options;
    }

    [McpServerTool(Destructive = true, ReadOnly = false, Title = CommandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            if (string.IsNullOrEmpty(options.WorkbookId))
            {
                context.Response.Status = 400;
                context.Response.Message = "Workbook ID is required";
                return context.Response;
            }

            if (string.IsNullOrEmpty(options.Title) && string.IsNullOrEmpty(options.SerializedContent))
            {
                context.Response.Status = 400;
                context.Response.Message = "At least one property (title or serialized-content) must be provided for update";
                return context.Response;
            }

            var workbooksService = context.GetService<IWorkbooksService>();
            var updatedWorkbook = await workbooksService.UpdateWorkbook(
                options.WorkbookId,
                options.Title,
                options.SerializedContent,
                options.RetryPolicy);

            if (updatedWorkbook == null)
            {
                context.Response.Status = 404;
                context.Response.Message = $"Workbook with ID '{options.WorkbookId}' not found";
                return context.Response;
            }

            context.Response.Results = ResponseResult.Create(
                new UpdateWorkbooksCommandResult(updatedWorkbook), 
                WorkbooksJsonContext.Default.UpdateWorkbooksCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating workbook with ID: {WorkbookId}", options.WorkbookId);
            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record UpdateWorkbooksCommandResult(WorkbookInfo Workbook);
}
