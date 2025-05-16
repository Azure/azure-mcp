// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Arguments.AppConfig.KeyValue;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.AppConfig.KeyValue;

public sealed class KeyValueDeleteCommand(ILogger<KeyValueDeleteCommand> logger) : BaseKeyValueCommand<KeyValueDeleteArguments>()
{
    private const string _commandTitle = "Delete App Configuration Key-Value Setting";
    private readonly ILogger<KeyValueDeleteCommand> _logger = logger;

    public override string Name => "delete";

    public override string Description =>
        """
        Delete a key-value pair from an App Configuration store. This command removes the specified key-value pair from the store.
        If a label is specified, only the labeled version is deleted. If no label is specified, the key-value with the matching
        key and the default label will be deleted.
        """;

    public override string Title => _commandTitle;

    [McpServerTool(Destructive = true, ReadOnly = false, Title = _commandTitle)]
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
            await appConfigService.DeleteKeyValue(
                args.Account!,
                args.Key!,
                args.Subscription!,
                args.Tenant,
                args.RetryPolicy,
                args.Label);

            var result = new KeyValueDeleteCommandResult(args.Key, args.Label);
            context.Response.Results = ResponseResult.Create(result, AppConfigJsonContext.Default.KeyValueDeleteCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred deleting value. Key: {Key}.", args.Key);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record KeyValueDeleteCommandResult(string? Key, string? Label);
}
