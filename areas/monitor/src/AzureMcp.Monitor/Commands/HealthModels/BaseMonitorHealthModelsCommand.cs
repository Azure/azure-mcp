// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Core.Commands;
using AzureMcp.Core.Commands.Subscription;
using AzureMcp.Core.Options;
using AzureMcp.Monitor.Options;

namespace AzureMcp.Monitor.Commands.HealthModels;

public abstract class BaseMonitorHealthModelsCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>
    : SubscriptionCommand<TOptions>
    where TOptions : SubscriptionOptions, new()
{
    protected readonly Option<string> _entityOption = MonitorOptionDefinitions.Health.Entity;
    protected readonly Option<string> _healthModelOption = MonitorOptionDefinitions.Health.HealthModel;

    protected BaseMonitorHealthModelsCommand() : base()
    {
    }
}
