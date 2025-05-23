// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Arguments.AppConfig;
using AzureMcp.Models.Argument;

namespace AzureMcp.Commands.AppConfig;

public abstract class BaseAppConfigCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] T>
    : SubscriptionCommand<T> where T : BaseAppConfigArguments, new()
{
    protected readonly Option<string> _accountOption = ArgumentDefinitions.AppConfig.Account;

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
