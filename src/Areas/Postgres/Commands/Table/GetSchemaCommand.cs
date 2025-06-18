// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Option;
using AzureMcp.Options.Postgres.Table;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Postgres.Table;

public sealed class GetSchemaCommand(ILogger<GetSchemaCommand> logger) : BaseDatabaseCommand<GetSchemaOptions>(logger)
{
    private const string CommandTitle = "Get PostgreSQL Table Schema";
    private readonly Option<string> _tableOption = OptionDefinitions.Postgres.Table;

    public override string Name => "schema";
    public override string Description => "Retrieves the schema of a specified table in a PostgreSQL database.";
    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_tableOption);
    }

    protected override GetSchemaOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Table = parseResult.GetValueForOption(_tableOption);
        return options;
    }

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
            List<string> schema = await pgService.GetTableSchemaAsync(options.Subscription!, options.ResourceGroup!, options.User!, options.Server!, options.Database!, options.Table!);
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

    internal record GetSchemaCommandResult(List<string> Schema);
}
