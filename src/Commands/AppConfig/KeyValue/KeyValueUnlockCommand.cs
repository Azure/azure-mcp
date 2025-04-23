// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.AppConfig.KeyValue;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.CommandLine.Parsing;

namespace AzureMcp.Commands.AppConfig.KeyValue;

public sealed class KeyValueUnlockCommand(ILogger<KeyValueUnlockCommand> logger) : BaseKeyValueCommand<KeyValueUnlockArguments>()
{
    private readonly ILogger<KeyValueUnlockCommand> _logger = logger;

    protected override string GetCommandName() => "unlock";

    protected override string GetCommandDescription() =>
        """
        Unlock a key-value setting in an App Configuration store. This command removes the read-only mode from a 
        key-value setting, allowing modifications to its value. You must specify an account name and key. Optionally, 
        you can specify a label to unlock a specific labeled version of the setting, otherwise the setting with the 
        default label will be unlocked.
        """;

    [McpServerTool(Destructive = false, ReadOnly = false)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindArguments(parseResult);

        try
        {
            if (!await ProcessArguments(context, args))
            {
                return context.Response;
            }

            var appConfigService = context.GetService<IAppConfigService>();
            await appConfigService.UnlockKeyValue(
                args.Account!,
                args.Key!,
                args.Subscription!,
                args.Tenant,
                args.RetryPolicy,
                args.Label);

            context.Response.Results = new { key = args.Key, label = args.Label };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred unlocking key. Key: {Key}.", args.Key);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }
}