// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Arguments.AppConfig.KeyValue;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.AppConfig.KeyValue;

public sealed class KeyValueUnlockCommand(ILogger<KeyValueUnlockCommand> logger) : BaseKeyValueCommand<KeyValueUnlockArguments>()
{
    private const string _commandTitle = "Unlock App Configuration Key-Value Setting";
    private readonly ILogger<KeyValueUnlockCommand> _logger = logger;

    public override string Name => "unlock";

    public override string Description =>
        """
        Unlock a key-value setting in an App Configuration store. This command removes the read-only mode from a
        key-value setting, allowing modifications to its value. You must specify an account name and key. Optionally,
        you can specify a label to unlock a specific labeled version of the setting, otherwise the setting with the
        default label will be unlocked.
        """;

    public override string Title => _commandTitle;

    [McpServerTool(Destructive = false, ReadOnly = false, Title = _commandTitle)]
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

            context.Response.Results =
                ResponseResult.Create(
                    new KeyValueUnlockResult(args.Key, args.Label),
                    AppConfigJsonContext.Default.KeyValueUnlockResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred unlocking key. Key: {Key}.", args.Key);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record KeyValueUnlockResult(string? Key, string? Label);
}
