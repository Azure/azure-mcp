// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using AzureMcp.Arguments.Postgres.Database;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.Postgres.Database;

public sealed class DatabaseQueryCommand(ILogger<DatabaseQueryCommand> logger) : BaseDatabaseCommand<DatabaseQueryArguments>(logger)
{
    private const string _commandTitle = "Query PostgreSQL Database";
    private readonly Option<string> _queryOption = ArgumentDefinitions.Postgres.Query.ToOption();
    public override string Name => "query";

    public override string Description => "Executes a query on the PostgreSQL database.";

    public override string Title => _commandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_queryOption);
    }

    protected override void RegisterArguments()
    {
        base.RegisterArguments();
        AddArgument(CreateQueryArgument());
    }

    protected override DatabaseQueryArguments BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.Query = parseResult.GetValueForOption(_queryOption);
        return args;
    }


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

    private static ArgumentBuilder<DatabaseQueryArguments> CreateQueryArgument() =>
        ArgumentBuilder<DatabaseQueryArguments>
            .Create(ArgumentDefinitions.Postgres.Query.Name, ArgumentDefinitions.Postgres.Query.Description)
            .WithValueAccessor(args => args.Query ?? string.Empty)
            .WithIsRequired(true);

    internal record DatabaseQueryCommandResult(List<string> QueryResult);
}
