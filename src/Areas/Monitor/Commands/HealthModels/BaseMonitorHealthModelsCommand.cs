// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Option;
using AzureMcp.Options;

namespace AzureMcp.Commands.Monitor.HealthModels;

public abstract class BaseMonitorHealthModelsCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>
    : SubscriptionCommand<TOptions>
    where TOptions : SubscriptionOptions, new()
{
    protected readonly Option<string> _entityOption = OptionDefinitions.Monitor.Health.Entity;
    protected readonly Option<string> _healthModelOption = OptionDefinitions.Monitor.Health.HealthModel;

    protected BaseMonitorHealthModelsCommand() : base()
    {
    }
}
