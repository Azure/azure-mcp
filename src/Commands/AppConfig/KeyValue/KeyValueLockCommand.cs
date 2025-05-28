// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.AppConfig.KeyValue;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.AppConfig.KeyValue;

public sealed class KeyValueLockCommand(ILogger<KeyValueLockCommand> logger) : BaseKeyValueCommand<KeyValueLockArguments>()
{
    private const string _commandTitle = "Lock App Configuration Key-Value Setting";
    private readonly ILogger<KeyValueLockCommand> _logger = logger;

    public override string Name => "lock";

    public override string Description =>
        """
        Lock a key-value in an App Configuration store. This command sets a key-value to read-only mode,
        preventing any modifications to its value. You must specify an account name and key. Optionally,
        you can specify a label to lock a specific labeled version of the key-value.
        """;

    public override string Title => _commandTitle;

    [McpServerTool(Destructive = false, ReadOnly = false, Title = _commandTitle)]
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
            await appConfigService.LockKeyValue(
                args.Account!,
                args.Key!,
                args.Subscription!,
                args.Tenant,
                args.RetryPolicy,
                args.Label);

            context.Response.Results =
                ResponseResult.Create(
                    new KeyValueLockCommandResult(args.Key, args.Label),
                    AppConfigJsonContext.Default.KeyValueLockCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred locking value. Key: {Key}, Label: {Label}", args.Key, args.Label);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record KeyValueLockCommandResult(string? Key, string? Label);
}
