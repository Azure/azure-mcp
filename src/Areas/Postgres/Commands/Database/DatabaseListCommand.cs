// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Postgres.Options.Database;
using AzureMcp.Areas.Postgres.Services;
using AzureMcp.Commands.Postgres;
using AzureMcp.Services.Telemetry;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Postgres.Commands.Database;

public sealed class DatabaseListCommand(ILogger<DatabaseListCommand> logger) : BaseServerCommand<DatabaseListOptions>(logger)
{
    private const string CommandTitle = "List PostgreSQL Databases";

    public override string Name => "list";

    public override string Description => "Lists all databases in the PostgreSQL server.";

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

            context.Activity?.WithSubscriptionTag(options);

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
            HandleException(context, ex);
        }
        return context.Response;
    }

    internal record DatabaseListCommandResult(List<string> Databases);
}
