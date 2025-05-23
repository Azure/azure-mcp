// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Arguments;
using AzureMcp.Models.Argument;

namespace AzureMcp.Commands;

public abstract class SubscriptionCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TArgs> : GlobalCommand<TArgs>
    where TArgs : SubscriptionArguments, new()
{
    protected readonly Option<string> _subscriptionOption = ArgumentDefinitions.Common.Subscription;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_subscriptionOption);
    }

    protected override TArgs BindOptions(ParseResult parseResult)
    {
        var args = base.BindOptions(parseResult);
        args.Subscription = parseResult.GetValueForOption(_subscriptionOption);
        return args;
    }
}
