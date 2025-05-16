// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using AzureMcp.Arguments.Postgres.Table;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.Postgres.Table;

public sealed class GetSchemaCommand(ILogger<GetSchemaCommand> logger) : BaseDatabaseCommand<GetSchemaArguments>(logger)
{
    private const string _commandTitle = "Get PostgreSQL Table Schema";
    private readonly Option<string> _tableOption = ArgumentDefinitions.Postgres.Table.ToOption();

    public override string Name => "schema";
    public override string Description => "Retrieves the schema of a specified table in a PostgreSQL database.";
    public override string Title => _commandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_tableOption);
    }

    protected override void RegisterArguments()
    {
        base.RegisterArguments();
        AddArgument(CreateTableArgument());
    }

    protected override GetSchemaArguments BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.Table = parseResult.GetValueForOption(_tableOption);
        return args;
    }

    [McpServerTool(Destructive = false, ReadOnly = true)]
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
            List<string> schema = await pgService.GetTableSchemaAsync(args.Subscription!, args.ResourceGroup!, args.User!, args.Server!, args.Database!, args.Table!);
            context.Response.Results = schema?.Count > 0 ?
                ResponseResult.Create(
                    new GetSchemaCommandResult(schema),
                    PostgresJsonContext.Default.GetSchemaCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred retrieving the schema for table");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    private static ArgumentBuilder<GetSchemaArguments> CreateTableArgument() =>
        ArgumentBuilder<GetSchemaArguments>
            .Create(ArgumentDefinitions.Postgres.Table.Name, ArgumentDefinitions.Postgres.Table.Description)
            .WithValueAccessor(args => args.Table ?? string.Empty)
            .WithIsRequired(true);

    internal record GetSchemaCommandResult(List<string> Schema);
}
