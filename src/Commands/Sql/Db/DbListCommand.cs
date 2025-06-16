// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Commands.Sql.Server;
using AzureMcp.Options.Sql.Database;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Sql.Db;

public sealed class DbListCommand(ILogger<DbListCommand> logger) : BaseServerCommand<DatabaseListOptions>()
{
    private const string _commandTitle = "List SQL Databases";
    private readonly ILogger<DbListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        """
        List all databases in a SQL server. This command retrieves and displays all databases available
        in the specified SQL server. Results include database names and are returned as a JSON array.
        """;

    public override string Title => _commandTitle;

    [McpServerTool(Destructive = false, ReadOnly = true, Title = _commandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            var sqlService = context.GetService<ISqlService>() ?? throw new InvalidOperationException("SQL service is not available.");
            var databases = await sqlService.ListDatabases(
                options.ServerName!,
                options.ResourceGroup!,
                options.Subscription!,
                options.Tenant,
                options.AuthMethod,
                options.RetryPolicy);
            context.Response.Results = databases?.Count > 0 ?
                ResponseResult.Create(
                    new DbListCommandResult(databases),
                    SqlJsonContext.Default.DbListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred listing databases.");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record DbListCommandResult(List<string> Databases);
}
