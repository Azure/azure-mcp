// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Postgres.Options.Server;
using AzureMcp.Areas.Postgres.Services;
using AzureMcp.Commands.Postgres;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Postgres.Commands.Server;

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
            HandleException(context.Response, ex);
        }
        return context.Response;
    }
    internal record GetConfigCommandResult(string Configuration);
}
