// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Options.Postgres.Database;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Postgres.Database;

public sealed class DatabaseListCommand(ILogger<DatabaseListCommand> logger) : BaseServerCommand<DatabaseListOptions>(logger)
{
    private const string _commandTitle = "List PostgreSQL Databases";

    public override string Name => "list";

    public override string Description => "Lists all databases in the PostgreSQL server.";

    public override string Title => _commandTitle;

    [McpServerTool(Destructive = false, ReadOnly = true, Title = _commandTitle)]
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
            List<string> databases = await pgService.ListDatabasesAsync(options.Subscription!, options.ResourceGroup!, options.User!, options.Server!);
            context.Response.Results = databases?.Count > 0 ?
                ResponseResult.Create(
                    new DatabaseListCommandResult(databases),
                    PostgresJsonContext.Default.DatabaseListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred listing databases.");
            HandleException(context.Response, ex);
        }
        return context.Response;
    }

    internal record DatabaseListCommandResult(List<string> Databases);
}
