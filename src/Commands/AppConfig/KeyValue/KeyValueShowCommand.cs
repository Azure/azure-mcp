// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Arguments.AppConfig.KeyValue;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.AppConfig.KeyValue;

public sealed class KeyValueShowCommand(ILogger<KeyValueShowCommand> logger) : BaseKeyValueCommand<KeyValueShowArguments>()
{
    private const string _commandTitle = "Show App Configuration Key-Value Setting";
    private readonly ILogger<KeyValueShowCommand> _logger = logger;

    public override string Name => "show";

    public override string Description =>
        """
        Show a specific key-value setting in an App Configuration store. This command retrieves and displays the value,
        label, content type, ETag, last modified time, and lock status for a specific setting. You must specify an
        account name and key. Optionally, you can specify a label otherwise the setting with default label will be retrieved.
        """;

    public override string Title => _commandTitle;

    [McpServerTool(Destructive = false, ReadOnly = true, Title = _commandTitle)]
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
            var setting = await appConfigService.GetKeyValue(
                args.Account!,
                args.Key!,
                args.Subscription!,
                args.Tenant,
                args.RetryPolicy,
                args.Label);

            context.Response.Results = ResponseResult.Create(
                new KeyValueShowResult(setting),
                AppConfigJsonContext.Default.KeyValueShowResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred getting value. Key: {Key}.", args.Key);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record KeyValueShowResult(Models.AppConfig.KeyValueSetting Setting);
}
