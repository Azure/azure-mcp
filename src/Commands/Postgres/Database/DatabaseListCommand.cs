// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Arguments.Postgres.Database;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.Postgres.Database;

public sealed class DatabaseListCommand(ILogger<DatabaseListCommand> logger) : BaseServerCommand<DatabaseListArguments>(logger)
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
            var args = BindArguments(parseResult);
            if (!await ProcessArguments(context, args))
            {
                return context.Response;
            }

            IPostgresService pgService = context.GetService<IPostgresService>() ?? throw new InvalidOperationException("PostgreSQL service is not available.");
            List<string> databases = await pgService.ListDatabasesAsync(args.Subscription!, args.ResourceGroup!, args.User!, args.Server!);
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
