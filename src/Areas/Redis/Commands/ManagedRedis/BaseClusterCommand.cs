// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Option;
using AzureMcp.Options.Redis.ManagedRedis;

namespace AzureMcp.Commands.Redis.ManagedRedis;

public abstract class BaseClusterCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] T>
    : SubscriptionCommand<T> where T : BaseClusterOptions, new()
{
    protected readonly Option<string> _clusterOption = OptionDefinitions.Redis.Cluster;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_clusterOption);
        command.AddOption(_resourceGroupOption);
    }

    protected override T BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Cluster = parseResult.GetValueForOption(_clusterOption);
        options.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption) ?? "";
        return options;
    }
}
