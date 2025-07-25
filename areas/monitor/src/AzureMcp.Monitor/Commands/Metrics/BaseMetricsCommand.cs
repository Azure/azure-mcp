// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Core.Commands;
using AzureMcp.Core.Commands.Subscription;
using AzureMcp.Core.Options;
using AzureMcp.Monitor.Options;
using AzureMcp.Monitor.Options.Metrics;

namespace AzureMcp.Monitor.Commands.Metrics;

/// <summary>
/// Base command for all metrics operations
/// </summary>
public abstract class BaseMetricsCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>
    : SubscriptionCommand<TOptions>
    where TOptions : SubscriptionOptions, IMetricsOptions, new()
{
    protected readonly Option<string> _resourceTypeOption = MonitorOptionDefinitions.Metrics.ResourceType;
    protected readonly Option<string> _resourceNameOption = MonitorOptionDefinitions.Metrics.ResourceName;
    protected readonly Option<string> _optionalResourceGroupOption = MonitorOptionDefinitions.Metrics.OptionalResourceGroup;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_resourceTypeOption);
        command.AddOption(_resourceNameOption);
        command.AddOption(_optionalResourceGroupOption);
    }

    protected override TOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.ResourceType = parseResult.GetValueForOption(_resourceTypeOption);
        options.ResourceName = parseResult.GetValueForOption(_resourceNameOption);
        options.ResourceGroup = parseResult.GetValueForOption(_optionalResourceGroupOption);
        return options;
    }
}
