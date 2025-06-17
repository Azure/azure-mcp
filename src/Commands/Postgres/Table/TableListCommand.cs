// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Options.Postgres.Table;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;


namespace AzureMcp.Commands.Postgres.Table;

public sealed class TableListCommand(ILogger<TableListCommand> logger) : BaseDatabaseCommand<TableListOptions>(logger)
{
    private const string CommandTitle = "List PostgreSQL Tables";

    public override string Name => "list";
    public override string Description => "Lists all tables in the PostgreSQL database.";
    public override string Title => CommandTitle;

    [McpServerTool(Destructive = false, ReadOnly = true, Title = CommandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        try
        {
            var options = BindOptions(parseResult);
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            IPostgresService pgService = context.GetService<IPostgresService>() ?? throw new InvalidOperationException("PostgreSQL service is not available.");
            List<string> tables = await pgService.ListTablesAsync(options.Subscription!, options.ResourceGroup!, options.User!, options.Server!, options.Database!);
            context.Response.Results = ResponseResult.Create(
                new TableListCommandResult(tables ?? []),
                PostgresJsonContext.Default.TableListCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred listing tables.");
            HandleException(context.Response, ex);
        }
        return context.Response;
    }

    internal record TableListCommandResult(List<string> Tables);
}
