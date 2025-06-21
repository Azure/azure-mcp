// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Areas.Monitor.Options;
using AzureMcp.Commands;
using AzureMcp.Commands.Subscription;
using AzureMcp.Options;

namespace AzureMcp.Areas.Monitor.Commands.HealthModels;

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
