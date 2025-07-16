// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Workbooks.Models;
using AzureMcp.Areas.Workbooks.Options;
using AzureMcp.Areas.Workbooks.Options.Workbook;
using AzureMcp.Areas.Workbooks.Services;
using AzureMcp.Commands.Subscription;
using AzureMcp.Commands;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Workbooks.Commands.Workbook;

public sealed class CreateWorkbooksCommand(ILogger<CreateWorkbooksCommand> logger) : SubscriptionCommand<CreateWorkbookOptions>
{
    private const string CommandTitle = "Create Workbook";
    private readonly ILogger<CreateWorkbooksCommand> _logger = logger;

    private static readonly Option<string> _titleOption = new(
        $"--{WorkbooksOptionDefinitions.TitleText}",
        "The display name/title of the workbook.")
    {
        IsRequired = true
    };
    
    private static readonly Option<string> _serializedContentOption = new(
        $"--{WorkbooksOptionDefinitions.SerializedContentText}",
        "The serialized content/data of the workbook.")
    {
        IsRequired = true
    };
    
    private static readonly Option<string> _sourceIdOption = new(
        $"--{WorkbooksOptionDefinitions.SourceIdText}",
        "The source ID for the workbook.")
    {
        IsRequired = false
    };

    public override string Name => "create";

    public override string Description =>
        """
        Create a new workbook in the specified resource group and subscription. 
        You can set the title and serialized data JSON content for the workbook.
        Returns the created workbook information upon successful completion.
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_resourceGroupOption);
        command.AddOption(_titleOption);
        command.AddOption(_serializedContentOption);
        command.AddOption(_sourceIdOption);
        command.AddOption(_tenantOption);
    }

    protected override CreateWorkbookOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption);
        options.Title = parseResult.GetValueForOption(_titleOption);
        options.SerializedContent = parseResult.GetValueForOption(_serializedContentOption);
        options.SourceId = parseResult.GetValueForOption(_sourceIdOption);
        options.Tenant = parseResult.GetValueForOption(_tenantOption);
        return options;
    }

    [McpServerTool(Destructive = false, ReadOnly = false, Title = CommandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            if (string.IsNullOrEmpty(options.Subscription))
            {
                context.Response.Status = 400;
                context.Response.Message = "Subscription is required";
                return context.Response;
            }

            if (string.IsNullOrEmpty(options.ResourceGroup))
            {
                context.Response.Status = 400;
                context.Response.Message = "Resource group is required";
                return context.Response;
            }

            if (string.IsNullOrEmpty(options.Title))
            {
                context.Response.Status = 400;
                context.Response.Message = "Title is required";
                return context.Response;
            }

            if (string.IsNullOrEmpty(options.SerializedContent))
            {
                context.Response.Status = 400;
                context.Response.Message = "Serialized content is required";
                return context.Response;
            }

            var workbooksService = context.GetService<IWorkbooksService>();
            var createdWorkbook = await workbooksService.CreateWorkbook(
                options.Subscription,
                options.ResourceGroup,
                options.Title,
                options.SerializedContent,
                /**
                 * The source ID is optional, defaulting to "azure monitor" if not provided.
                 * "azure monitor" is the default for workbooks created in the Azure Monitor extension,
                 * otherwise the workbook will display an error when opening.
                 */
                options.SourceId ?? "azure monitor",
                options.RetryPolicy);

            if (createdWorkbook == null)
            {
                context.Response.Status = 500;
                context.Response.Message = "Failed to create workbook";
                return context.Response;
            }

            context.Response.Results = ResponseResult.Create(
                new CreateWorkbooksCommandResult(createdWorkbook), 
                WorkbooksJsonContext.Default.CreateWorkbooksCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating workbook '{Title}' in resource group '{ResourceGroup}'", options.Title, options.ResourceGroup);
            HandleException(context, ex);
        }

        return context.Response;
    }

    public sealed record CreateWorkbooksCommandResult(WorkbookInfo Workbook);
}
