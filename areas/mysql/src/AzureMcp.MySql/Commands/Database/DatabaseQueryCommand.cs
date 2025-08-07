// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using AzureMcp.Core.Commands;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.MySql.Commands;
using AzureMcp.MySql.Json;
using AzureMcp.MySql.Options;
using AzureMcp.MySql.Options.Database;
using AzureMcp.MySql.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.MySql.Commands.Database;

public sealed class DatabaseQueryCommand(ILogger<DatabaseQueryCommand> logger) : BaseDatabaseCommand<DatabaseQueryOptions>(logger)
{
    private const string CommandTitle = "Query MySQL Database";
    private readonly Option<string> _queryOption = MySqlOptionDefinitions.Query;

    public override string Name => "query";

    public override string Description => "Executes SQL queries against a MySQL database to search, retrieve, filter, and manipulate data. Supports SELECT queries to find items containing specific search terms, pattern matching, data filtering, as well as INSERT, UPDATE, DELETE operations and stored procedure execution.";

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = true };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_queryOption);
    }

    protected override DatabaseQueryOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Query = parseResult.GetValueForOption(_queryOption);
        return options;
    }

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

            IMySqlService mysqlService = context.GetService<IMySqlService>() ?? throw new InvalidOperationException("MySQL service is not available.");
            List<string> result = await mysqlService.ExecuteQueryAsync(options.Subscription!, options.ResourceGroup!, options.User!, options.Server!, options.Database!, options.Query!);
            context.Response.Results = result?.Count > 0 ?
                ResponseResult.Create(
                    new DatabaseQueryCommandResult(result),
                    MySqlJsonContext.Default.DatabaseQueryCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred executing query.");
            HandleException(context, ex);
        }
        return context.Response;
    }

    public record DatabaseQueryCommandResult(List<string> Results);
}
