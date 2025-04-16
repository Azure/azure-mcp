// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.Subscription;

namespace AzureMcp.Commands.Subscription;

public abstract class BaseSubscriptionCommand<TArgs> : GlobalCommand<TArgs>
    where TArgs : BaseSubscriptionArguments, new()
{
    protected BaseSubscriptionCommand()
    {
    }
}