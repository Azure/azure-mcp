// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Core.Commands;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.MySql.Commands;
using AzureMcp.MySql.Json;
using AzureMcp.MySql.Options.Database;
using AzureMcp.MySql.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.MySql.Commands.Database;

public sealed class DatabaseListCommand(ILogger<DatabaseListCommand> logger) : BaseServerCommand<DatabaseListOptions>(logger)
{
    private const string CommandTitle = "List MySQL Databases";

    public override string Name => "list";

    public override string Description => "Lists all databases in the MySQL server.";

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = true };

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
            List<string> databases = await mysqlService.ListDatabasesAsync(options.Subscription!, options.ResourceGroup!, options.User!, options.Server!);
            context.Response.Results = databases?.Count > 0 ?
                ResponseResult.Create(
                    new DatabaseListCommandResult(databases),
                    MySqlJsonContext.Default.DatabaseListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred listing databases.");
            HandleException(context, ex);
        }
        return context.Response;
    }

public record DatabaseListCommandResult(List<string> Databases);
}