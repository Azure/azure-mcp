// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Areas.Storage.Options;
using AzureMcp.Areas.Subscription.Commands;
using AzureMcp.Commands;
using AzureMcp.Models.Option;

namespace AzureMcp.Areas.Storage.Commands;

public abstract class BaseStorageCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] T>
    : SubscriptionCommand<T>
    where T : BaseStorageOptions, new()
{
    protected readonly Option<string> _accountOption = OptionDefinitions.Storage.Account;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_accountOption);
    }

    protected override T BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Account = parseResult.GetValueForOption(_accountOption);
        return options;
    }
}
