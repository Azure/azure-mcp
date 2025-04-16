// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.Subscription;
using System.Diagnostics.CodeAnalysis;

namespace AzureMcp.Commands.Subscription;

public abstract class BaseSubscriptionCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TArgs> : GlobalCommand<TArgs>
    where TArgs : BaseSubscriptionArguments, new()
{
    protected BaseSubscriptionCommand()
    {
    }
}