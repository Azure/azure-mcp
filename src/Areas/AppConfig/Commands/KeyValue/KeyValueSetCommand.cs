// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.AppConfig.Options;
using AzureMcp.Areas.AppConfig.Options.KeyValue;
using AzureMcp.Areas.AppConfig.Services;
using AzureMcp.Commands.AppConfig;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.AppConfig.Commands.KeyValue;

public sealed class KeyValueSetCommand(ILogger<KeyValueSetCommand> logger) : BaseKeyValueCommand<KeyValueSetOptions>()
{
    private const string CommandTitle = "Set App Configuration Key-Value Setting";
    private readonly Option<string> _valueOption = AppConfigOptionDefinitions.Value;
    private readonly ILogger<KeyValueSetCommand> _logger = logger;

    public override string Name => "set";

    public override string Description =>
        """
        Set a key-value setting in an App Configuration store. This command creates or updates a key-value setting
        with the specified value. You must specify an account name, key, and value. Optionally, you can specify a
        label otherwise the default label will be used.
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_valueOption);
    }

    protected override KeyValueSetOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Value = parseResult.GetValueForOption(_valueOption);
        return options;
    }

    [McpServerTool(Destructive = false, ReadOnly = false, Title = CommandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            AddSubscriptionInformation(context.Activity, options);

            var appConfigService = context.GetService<IAppConfigService>();
            await appConfigService.SetKeyValue(
                options.Account!,
                options.Key!,
                options.Value!,
                options.Subscription!,
                options.Tenant,
                options.RetryPolicy,
                options.Label);

            context.Response.Results = ResponseResult.Create(
                new KeyValueSetCommandResult(options.Key, options.Value, options.Label),
                AppConfigJsonContext.Default.KeyValueSetCommandResult
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred setting value. Key: {Key}.", options.Key);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record KeyValueSetCommandResult(string? Key, string? Value, string? Label);
}
