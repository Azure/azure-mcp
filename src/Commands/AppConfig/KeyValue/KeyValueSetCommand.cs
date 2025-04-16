// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.AppConfig.KeyValue;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.CommandLine;
using System.CommandLine.Parsing;

namespace AzureMcp.Commands.AppConfig.KeyValue;

public sealed class KeyValueSetCommand : BaseKeyValueCommand<KeyValueSetArguments>
{
    private readonly Option<string> _valueOption = ArgumentDefinitions.AppConfig.Value.ToOption();
    private readonly ILogger<KeyValueSetCommand> _logger;

    public KeyValueSetCommand(ILogger<KeyValueSetCommand> logger) : base()
    {
        _logger = logger;
    }

    protected override string GetCommandName() => "set";

    protected override string GetCommandDescription() =>
        """
        Set a key-value setting in an App Configuration store. This command creates or updates a key-value setting 
        with the specified value. You must specify an account name, key, and value. Optionally, you can specify a 
        label otherwise the default label will be used.
        """;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_valueOption);
    }

    protected override void RegisterArguments()
    {
        base.RegisterArguments();
        AddArgument(CreateValueArgument());
    }

    protected override KeyValueSetArguments BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.Value = parseResult.GetValueForOption(_valueOption);
        return args;
    }

    private static ArgumentBuilder<KeyValueSetArguments> CreateValueArgument() =>
        ArgumentBuilder<KeyValueSetArguments>
            .Create(ArgumentDefinitions.AppConfig.Value.Name, ArgumentDefinitions.AppConfig.Value.Description)
            .WithValueAccessor(args => args.Value ?? string.Empty)
            .WithIsRequired(ArgumentDefinitions.AppConfig.Value.Required);

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
            await appConfigService.SetKeyValue(
                args.Account!,
                args.Key!,
                args.Value!,
                args.Subscription!,
                args.Tenant,
                args.RetryPolicy,
                args.Label);

            context.Response.Results = new { key = args.Key, value = args.Value, label = args.Label };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred setting value. Key: {Key}.", args.Key);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }
}