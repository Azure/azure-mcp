// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Options.Sql;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Sql.Server;

public sealed class ServerListCommand(ILogger<ServerListCommand> logger) : BaseSqlCommand<BaseSqlOptions>()
{
    private const string _commandTitle = "List SQL Servers";
    private readonly ILogger<ServerListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        """
        List all SQL servers in your subscription. This command retrieves and displays all SQL servers
        in the specified subscription. Results include server names and are returned as a JSON array.
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
            var sqlService = context.GetService<ISqlService>() ?? throw new InvalidOperationException("Sql service is not available.");
            var servers = await sqlService.ListServers(
                options.Subscription!,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = servers?.Count > 0 ?
                ResponseResult.Create(
                    new ServerListCommandResult(servers),
                    SqlJsonContext.Default.ServerListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred listing servers.");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record ServerListCommandResult(List<string> Servers);
}
