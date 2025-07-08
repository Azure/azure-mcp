// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Postgres.Options.Server;
using AzureMcp.Areas.Postgres.Services;
using AzureMcp.Commands.Postgres;
using AzureMcp.Services.Telemetry;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Postgres.Commands.Server;

public sealed class ServerListCommand(ILogger<ServerListCommand> logger) : BasePostgresCommand<ServerListOptions>(logger)
{
    private const string CommandTitle = "List PostgreSQL Servers";

    public override string Name => "list";

    public override string Description =>
        "Lists all PostgreSQL servers in the specified subscription.";

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
            List<string> servers = await pgService.ListServersAsync(options.Subscription!, options.ResourceGroup!, options.User!);
            context.Response.Results = servers?.Count > 0 ?
                ResponseResult.Create(
                    new ServerListCommandResult(servers),
                    PostgresJsonContext.Default.ServerListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred while listing servers");
            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record ServerListCommandResult(List<string> Servers);
}
