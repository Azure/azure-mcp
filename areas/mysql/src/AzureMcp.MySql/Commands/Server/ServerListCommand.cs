// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Core.Commands;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.MySql.Commands;
using AzureMcp.MySql.Json;
using AzureMcp.MySql.Options.Server;
using AzureMcp.MySql.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.MySql.Commands.Server;

public sealed class ServerListCommand(ILogger<ServerListCommand> logger) : BaseMySqlCommand<ServerListOptions>(logger)
{
    private const string CommandTitle = "List MySQL Servers";

    public override string Name => "list";

    public override string Description => "Lists all MySQL servers in the resource group.";

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
            List<string> servers = await mysqlService.ListServersAsync(options.Subscription!, options.ResourceGroup!, options.User!);
            context.Response.Results = servers?.Count > 0 ?
                ResponseResult.Create(
                    new ServerListCommandResult(servers),
                    MySqlJsonContext.Default.ServerListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred listing servers.");
            HandleException(context, ex);
        }
        return context.Response;
    }

public record ServerListCommandResult(List<string> Servers);
}
