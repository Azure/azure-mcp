// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.AppConfig.KeyValue;
using AzureMcp.Models.AppConfig;
using AzureMcp.Models.Argument;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.AppConfig.KeyValue;

public sealed class KeyValueListCommand(ILogger<KeyValueListCommand> logger) : BaseAppConfigCommand<KeyValueListArguments>()
{
    private const string _commandTitle = "List App Configuration Key-Value Settings";
    private readonly ILogger<KeyValueListCommand> _logger = logger;

    // KeyValueList has different key and label descriptions, which is why we are defining here instead of using BaseKeyValueCommand
    private readonly Option<string> _keyOption = ArgumentDefinitions.AppConfig.KeyValueList.Key;
    private readonly Option<string> _labelOption = ArgumentDefinitions.AppConfig.KeyValueList.Label;

    public override string Name => "list";

    public override string Description =>
        """
        List all key-values in an App Configuration store. This command retrieves and displays all key-value pairs
        from the specified store. Each key-value includes its key, value, label, content type, ETag, last modified
        time, and lock status.
        """;

    public override string Title => _commandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_keyOption);
        command.AddOption(_labelOption);
    }

    protected override KeyValueListArguments BindOptions(ParseResult parseResult)
    {
        var args = base.BindOptions(parseResult);
        args.Key = parseResult.GetValueForOption(_keyOption);
        args.Label = parseResult.GetValueForOption(_labelOption);
        return args;
    }

    [McpServerTool(Destructive = false, ReadOnly = true, Title = _commandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindOptions(parseResult);

        try
        {
            var validationResult = Validate(parseResult.CommandResult);

            if (!validationResult.IsValid)
            {
                context.Response.Status = 400;
                context.Response.Message = validationResult.ErrorMessage!;
                return context.Response;
            }

            var appConfigService = context.GetService<IAppConfigService>();
            var settings = await appConfigService.ListKeyValues(
                args.Account!,
                args.Subscription!,
                args.Key,
                args.Label,
                args.Tenant,
                args.RetryPolicy);

            context.Response.Results = settings?.Count > 0 ?
                ResponseResult.Create(
                    new KeyValueListCommandResult(settings),
                    AppConfigJsonContext.Default.KeyValueListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError("An exception occurred processing command. Exception: {Exception}", ex);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record KeyValueListCommandResult(List<KeyValueSetting> Settings);
}
