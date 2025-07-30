// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Core.Commands;
using AzureMcp.Core.Commands.Subscription;
using AzureMcp.Core.Options;

namespace AzureMcp.Monitor.Commands.WebTests;

public abstract class BaseMonitorWebTestsCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>
    : SubscriptionCommand<TOptions>
    where TOptions : SubscriptionOptions, new()
{
    protected BaseMonitorWebTestsCommand() : base()
    {
    }

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
    }

    protected override TOptions BindOptions(ParseResult parseResult)
    {
        return base.BindOptions(parseResult);
    }
}
