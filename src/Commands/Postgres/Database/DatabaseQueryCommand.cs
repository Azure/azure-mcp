// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.Postgres.Database;
using AzureMcp.Models.Argument;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Postgres.Database;

public sealed class DatabaseQueryCommand(ILogger<DatabaseQueryCommand> logger) : BaseDatabaseCommand<DatabaseQueryArguments>(logger)
{
    private const string _commandTitle = "Query PostgreSQL Database";
    private readonly Option<string> _queryOption = ArgumentDefinitions.Postgres.Query;
    public override string Name => "query";

    public override string Description => "Executes a query on the PostgreSQL database.";

    public override string Title => _commandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_queryOption);
    }

    protected override DatabaseQueryArguments BindOptions(ParseResult parseResult)
    {
        var args = base.BindOptions(parseResult);
        args.Query = parseResult.GetValueForOption(_queryOption);
        return args;
    }


    [McpServerTool(Destructive = false, ReadOnly = true, Title = _commandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        try
        {
            var args = BindOptions(parseResult);
            var validationResult = Validate(parseResult.CommandResult);

            if (!validationResult.IsValid)
            {
                context.Response.Status = 400;
                context.Response.Message = validationResult.ErrorMessage!;
                return context.Response;
            }

            IPostgresService pgService = context.GetService<IPostgresService>() ?? throw new InvalidOperationException("PostgreSQL service is not available.");
            List<string> queryResult = await pgService.ExecuteQueryAsync(args.Subscription!, args.ResourceGroup!, args.User!, args.Server!, args.Database!, args.Query!);
            context.Response.Results = queryResult?.Count > 0 ?
                ResponseResult.Create(
                    new DatabaseQueryCommandResult(queryResult),
                    PostgresJsonContext.Default.DatabaseQueryCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred while executing the query.");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record DatabaseQueryCommandResult(List<string> QueryResult);
}
