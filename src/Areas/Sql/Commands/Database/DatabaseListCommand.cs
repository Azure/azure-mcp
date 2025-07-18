// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Sql.Models;
using AzureMcp.Areas.Sql.Options.Database;
using AzureMcp.Areas.Sql.Services;
using AzureMcp.Services.Telemetry;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Sql.Commands.Database;

public sealed class DatabaseListCommand(ILogger<DatabaseListCommand> logger)
    : BaseSqlCommand<DatabaseListOptions>(logger)
{
    private const string CommandTitle = "List SQL Server Databases";

    public override string Name => "list";

    public override string Description =>
        """
        Gets a list of databases on an Azure SQL Server. This command retrieves all databases 
        hosted on the specified SQL server, including their configuration, status, performance tier, 
        and other properties. Returns an array of database objects with their detailed information.
        """;

    public override string Title => CommandTitle;

    [McpServerTool(
        Destructive = false,
        ReadOnly = true,
        Title = CommandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            context.Activity?.WithSubscriptionTag(options);

            var sqlService = context.GetService<ISqlService>();

            var databases = await sqlService.ListDatabasesAsync(
                options.Server!,
                options.ResourceGroup!,
                options.Subscription!,
                options.RetryPolicy);

            context.Response.Results = databases?.Count > 0
                ? ResponseResult.Create(
                    new DatabaseListResult(databases),
                    SqlJsonContext.Default.DatabaseListResult)
                : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error listing SQL databases. Server: {Server}, ResourceGroup: {ResourceGroup}, Options: {@Options}",
                options.Server, options.ResourceGroup, options);
            HandleException(context, ex);
        }

        return context.Response;
    }

    protected override string GetErrorMessage(Exception ex) => ex switch
    {
        Azure.RequestFailedException reqEx when reqEx.Status == 404 =>
            "SQL server not found. Verify the server name, resource group, and that you have access.",
        Azure.RequestFailedException reqEx when reqEx.Status == 403 =>
            $"Authorization failed accessing the SQL server. Verify you have appropriate permissions. Details: {reqEx.Message}",
        Azure.RequestFailedException reqEx => reqEx.Message,
        _ => base.GetErrorMessage(ex)
    };

    protected override int GetStatusCode(Exception ex) => ex switch
    {
        Azure.RequestFailedException reqEx => reqEx.Status,
        _ => base.GetStatusCode(ex)
    };

    internal record DatabaseListResult(List<SqlDatabase> Databases);
}
