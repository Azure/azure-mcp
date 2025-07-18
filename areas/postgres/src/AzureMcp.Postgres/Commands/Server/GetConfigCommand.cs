// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Services.Telemetry;
using AzureMcp.Postgres.Commands;
using AzureMcp.Postgres.Options.Server;
using AzureMcp.Postgres.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Postgres.Commands.Server;

public sealed class GetConfigCommand(ILogger<GetConfigCommand> logger) : BaseServerCommand<GetConfigOptions>(logger)
{
    private const string CommandTitle = "Get PostgreSQL Server Configuration";
    public override string Name => "config";
    public override string Description =>
        "Retrieve the configuration of a PostgreSQL server.";

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
            var config = await pgService.GetServerConfigAsync(options.Subscription!, options.ResourceGroup!, options.User!, options.Server!);
            context.Response.Results = config?.Length > 0 ?
                ResponseResult.Create(
                    new GetConfigCommandResult(config),
                    PostgresJsonContext.Default.GetConfigCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred retrieving server configuration.");
            HandleException(context, ex);
        }
        return context.Response;
    }
    internal record GetConfigCommandResult(string Configuration);
}
