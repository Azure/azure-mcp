// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Areas.Subscription.Options;
using AzureMcp.Commands;

namespace AzureMcp.Areas.Subscription.Commands;

public abstract class BaseSubscriptionCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions> : GlobalCommand<TOptions>
    where TOptions : BaseSubscriptionOptions, new()
{
    protected BaseSubscriptionCommand()
    {
    }
}
