// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Option;
using AzureMcp.Options.AppConfig;

namespace AzureMcp.Commands.AppConfig;

public abstract class BaseAppConfigCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] T>
    : SubscriptionCommand<T> where T : BaseAppConfigOptions, new()
{
    protected readonly Option<string> _accountOption = OptionDefinitions.AppConfig.Account;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_accountOption);
    }

    protected override T BindOptions(ParseResult parseResult)
    {
        var args = base.BindOptions(parseResult);
        args.Account = parseResult.GetValueForOption(_accountOption);
        return args;
    }
}
