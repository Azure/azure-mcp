// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using AzureMcp.Core.Commands;
using AzureMcp.Core.Commands.Subscription;
using AzureMcp.FunctionApp.Options;

namespace AzureMcp.FunctionApp.Commands;

public abstract class BaseFunctionAppCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>(ILogger logger)
    : SubscriptionCommand<TOptions>() where TOptions : BaseFunctionAppOptions, new()
{
    private readonly ILogger _logger = logger;
    private readonly Option<string> _functionAppName = FunctionAppOptionDefinitions.FunctionApp;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_functionAppName);
    }

    protected override TOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.FunctionAppName = parseResult.GetValueForOption(_functionAppName);
        return options;
    }
}
